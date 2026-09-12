# 02. Generic Message Processor — Advanced LLD (Azure + C# Generics)

## 📌 Context & Problem Statement

This document covers the **full production-grade design** of a Generic Message Processor.
It is a common senior-engineer interview topic in Microsoft/Azure ecosystem roles because it
tests **many skills simultaneously**:

| Concept | Covered Here |
|---|---|
| Generics + Interfaces | `IMessage<TBody>`, `IMessageHandler<T>` |
| Polymorphism | Multiple message types from same base |
| Correlation / Tracing | End-to-end `CorrelationId` propagation |
| Ordered Processing | Status-driven sequencing with idempotency |
| Pipeline Pattern | Pre/Post processing middleware per handler |
| Outbox Pattern | Guaranteed API/integration calls on success |
| Open/Closed Principle | Zero-touch dispatcher on new message types |
| Resilience | DLQ, retry, poison-message protection |

> **Business scenario used throughout:** An **Order Management System** that receives different
> message types (`OrderPlaced`, `OrderConfirmed`, `OrderShipped`, `OrderCancelled`) all sharing
> a common base, must process them in the correct business-status order, and must call downstream
> integrations (payment gateway, notification service, warehouse API) **reliably** on success.

---

## 1. Core Contracts — The Base Message Type

All messages share a **rich base interface** that carries correlation and ordering metadata.

```csharp
// ── Base marker interface ──────────────────────────────────────────────────
public interface IMessage
{
    /// <summary>Unique ID for THIS message envelope.</summary>
    Guid MessageId { get; }

    /// <summary>
    /// Ties all messages in ONE business transaction together.
    /// e.g. all messages for Order #42 share the same CorrelationId.
    /// </summary>
    Guid CorrelationId { get; }

    /// <summary>
    /// Logical ordering token within the correlation group.
    /// Lower value = must be processed first.
    /// Allows the processor to detect out-of-order delivery.
    /// </summary>
    int SequenceNumber { get; }

    /// <summary>Discriminator used by the dispatcher registry.</summary>
    string MessageType { get; }

    DateTimeOffset CreatedAt { get; }
}

// ── Typed base with a strongly-typed payload ──────────────────────────────
public abstract record BaseMessage<TBody> : IMessage
{
    public Guid MessageId { get; init; } = Guid.NewGuid();
    public Guid CorrelationId { get; init; }
    public int SequenceNumber { get; init; }
    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;
    public abstract string MessageType { get; }
    public TBody Body { get; init; } = default!;
}
```

### Why a generic `BaseMessage<TBody>`?

> **Interviewer:** *"Why not just put all fields directly on each concrete record?"*  
> **You:** "Using `BaseMessage<TBody>` gives us **three wins simultaneously**:
> 1. Every message is **guaranteed** to carry `CorrelationId` and `SequenceNumber` — no handler
>    author can forget them.
> 2. The `Body` is **strongly typed** — `OrderPlacedMessage.Body` is `OrderPlacedBody`, so
>    handlers never cast or use `dynamic`.
> 3. The dispatcher works on the `IMessage` abstraction for routing, then casts to
>    `BaseMessage<TBody>` for structured logging — zero reflection on the payload."

---

## 2. Concrete Message Types (Same Base, Different Behavior)

All order events share `OrderCorrelationContext` as their body, enriched per status:

```csharp
// ── Shared context traveling with every order message ─────────────────────
public record OrderCorrelationContext
{
    public Guid OrderId { get; init; }
    public string CustomerId { get; init; } = string.Empty;
    public decimal TotalAmount { get; init; }
    public string Currency { get; init; } = "INR";
}

// ── Concrete message types ─────────────────────────────────────────────────
public record OrderPlacedMessage : BaseMessage<OrderCorrelationContext>
{
    public override string MessageType => "order.placed";      // sequence: 10
}

public record OrderConfirmedMessage : BaseMessage<OrderCorrelationContext>
{
    public override string MessageType => "order.confirmed";   // sequence: 20
    public string ConfirmedBy { get; init; } = string.Empty;
}

public record OrderShippedMessage : BaseMessage<OrderCorrelationContext>
{
    public override string MessageType => "order.shipped";     // sequence: 30
    public string TrackingId { get; init; } = string.Empty;
    public string Carrier { get; init; } = string.Empty;
}

public record OrderCancelledMessage : BaseMessage<OrderCorrelationContext>
{
    public override string MessageType => "order.cancelled";   // sequence: 99 (terminal)
    public string Reason { get; init; } = string.Empty;
}
```

---

## 3. Message Ordering — Status-Driven Sequencing

### The Problem
Azure Service Bus **does not guarantee strict ordering** across sessions unless you use
**Session-enabled queues with `SessionId = CorrelationId`**. Even then, network retries can
cause a message to arrive late. We need an application-level ordering guard.

### The Solution: `IOrderGuard`

```csharp
// ── Persisted state per CorrelationId ─────────────────────────────────────
public class MessageFlowState
{
    public Guid CorrelationId { get; set; }
    public string CurrentStatus { get; set; } = string.Empty; // last processed MessageType
    public int LastProcessedSequence { get; set; }
    public DateTimeOffset LastUpdated { get; set; }
}

// ── Allowed transitions (the state machine) ───────────────────────────────
public static class OrderFlowTransitions
{
    // key: current status → value: allowed next statuses
    private static readonly Dictionary<string, HashSet<string>> _allowed = new()
    {
        { "",                  new() { "order.placed" } },
        { "order.placed",      new() { "order.confirmed", "order.cancelled" } },
        { "order.confirmed",   new() { "order.shipped",   "order.cancelled" } },
        { "order.shipped",     new() { /* terminal */ } },
        { "order.cancelled",   new() { /* terminal */ } },
    };

    public static bool IsAllowed(string current, string next)
        => _allowed.TryGetValue(current, out var allowed) && allowed.Contains(next);

    public static bool IsTerminal(string status)
        => status is "order.shipped" or "order.cancelled";
}

// ── Guard contract ─────────────────────────────────────────────────────────
public interface IOrderGuard
{
    /// <summary>
    /// Returns true if the message can be processed now.
    /// Returns false if it arrived out-of-order (caller should defer/park it).
    /// Throws InvalidOperationException for illegal transitions.
    /// </summary>
    Task<bool> CanProcessAsync(IMessage message, CancellationToken ct);

    Task MarkProcessedAsync(IMessage message, CancellationToken ct);
}

public class RedisOrderGuard : IOrderGuard
{
    private readonly IDistributedCache _cache; // Redis-backed
    private readonly ILogger<RedisOrderGuard> _logger;

    public RedisOrderGuard(IDistributedCache cache, ILogger<RedisOrderGuard> logger)
    {
        _cache = cache;
        _logger = logger;
    }

    public async Task<bool> CanProcessAsync(IMessage message, CancellationToken ct)
    {
        var key = $"flow:{message.CorrelationId}";
        var state = await GetStateAsync(key, ct)
                    ?? new MessageFlowState { CorrelationId = message.CorrelationId };

        // ── Guard 1: Duplicate detection ──────────────────────────────────
        if (state.LastProcessedSequence >= message.SequenceNumber)
        {
            _logger.LogWarning(
                "[{CorrelationId}] Duplicate/late message {MessageType} seq={Seq} (last={Last}). Skipping.",
                message.CorrelationId, message.MessageType,
                message.SequenceNumber, state.LastProcessedSequence);
            return false; // idempotent: already processed
        }

        // ── Guard 2: Illegal transition detection ─────────────────────────
        if (!OrderFlowTransitions.IsAllowed(state.CurrentStatus, message.MessageType))
        {
            if (OrderFlowTransitions.IsTerminal(state.CurrentStatus))
                throw new InvalidOperationException(
                    $"[{message.CorrelationId}] Flow is terminal at '{state.CurrentStatus}'. " +
                    $"Cannot accept '{message.MessageType}'.");

            // Out-of-order: park it (caller will defer)
            _logger.LogWarning(
                "[{CorrelationId}] Out-of-order: got '{Next}' but current is '{Current}'.",
                message.CorrelationId, message.MessageType, state.CurrentStatus);
            return false;
        }

        return true;
    }

    public async Task MarkProcessedAsync(IMessage message, CancellationToken ct)
    {
        var key = $"flow:{message.CorrelationId}";
        var state = new MessageFlowState
        {
            CorrelationId = message.CorrelationId,
            CurrentStatus = message.MessageType,
            LastProcessedSequence = message.SequenceNumber,
            LastUpdated = DateTimeOffset.UtcNow
        };
        await SetStateAsync(key, state, ct);
    }

    private async Task<MessageFlowState?> GetStateAsync(string key, CancellationToken ct) { /* Redis get */ throw new NotImplementedException(); }
    private async Task SetStateAsync(string key, MessageFlowState state, CancellationToken ct) { /* Redis set with TTL */ throw new NotImplementedException(); }
}
```

> **Interviewer:** *"Why Redis and not a database for the flow state?"*  
> **You:** "The order guard is on the **hot path** of every message — it must be microsecond-fast.
> Redis gives us O(1) get/set with sub-millisecond latency. The data is also **ephemeral** —
> once an order reaches a terminal state and the TTL expires, it self-cleans. If Redis is
> unavailable, we **fail-open** (allow processing and accept the rare duplicate) or
> **fail-closed** (reject until Redis recovers) depending on the SLA."

---

## 4. Handler Interface & Pipeline Middleware

### 4.1 Handler Contract

```csharp
public interface IMessageHandler<in TMessage> where TMessage : IMessage
{
    Task HandleAsync(TMessage message, CancellationToken ct);
}
```

### 4.2 Pipeline Middleware (Pre/Post Processing)

Each message goes through a **middleware pipeline** before reaching the actual handler.
This is the same pattern as ASP.NET Core middleware.

```csharp
// ── Middleware contract ────────────────────────────────────────────────────
public delegate Task MessageHandlerDelegate(IMessage message, CancellationToken ct);

public interface IMessageMiddleware
{
    Task InvokeAsync(IMessage message, MessageHandlerDelegate next, CancellationToken ct);
}

// ── Middleware 1: Structured logging with CorrelationId ───────────────────
public class CorrelationLoggingMiddleware : IMessageMiddleware
{
    private readonly ILogger<CorrelationLoggingMiddleware> _logger;

    public CorrelationLoggingMiddleware(ILogger<CorrelationLoggingMiddleware> logger)
        => _logger = logger;

    public async Task InvokeAsync(IMessage message, MessageHandlerDelegate next, CancellationToken ct)
    {
        using var scope = _logger.BeginScope(new Dictionary<string, object>
        {
            ["CorrelationId"] = message.CorrelationId,
            ["MessageId"]     = message.MessageId,
            ["MessageType"]   = message.MessageType,
            ["SequenceNumber"]= message.SequenceNumber,
        });

        _logger.LogInformation("[START] Processing {MessageType}", message.MessageType);
        var sw = System.Diagnostics.Stopwatch.StartNew();
        try
        {
            await next(message, ct);
            _logger.LogInformation("[END] {MessageType} processed in {Ms}ms", message.MessageType, sw.ElapsedMilliseconds);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[FAIL] {MessageType} failed after {Ms}ms", message.MessageType, sw.ElapsedMilliseconds);
            throw;
        }
    }
}

// ── Middleware 2: Order Guard ──────────────────────────────────────────────
public class OrderGuardMiddleware : IMessageMiddleware
{
    private readonly IOrderGuard _guard;
    private readonly IMessageDeferralService _deferral;

    public OrderGuardMiddleware(IOrderGuard guard, IMessageDeferralService deferral)
    {
        _guard    = guard;
        _deferral = deferral;
    }

    public async Task InvokeAsync(IMessage message, MessageHandlerDelegate next, CancellationToken ct)
    {
        var canProcess = await _guard.CanProcessAsync(message, ct);
        if (!canProcess)
        {
            // Park the message for later re-delivery instead of dead-lettering it
            await _deferral.DeferAsync(message, ct);
            return;
        }

        await next(message, ct);

        // Only mark processed AFTER the handler succeeds (atomicity guarantee)
        await _guard.MarkProcessedAsync(message, ct);
    }
}

// ── Middleware 3: Outbox trigger (fires integrations after success) ─────────
public class OutboxMiddleware : IMessageMiddleware
{
    private readonly IOutboxService _outbox;

    public OutboxMiddleware(IOutboxService outbox) => _outbox = outbox;

    public async Task InvokeAsync(IMessage message, MessageHandlerDelegate next, CancellationToken ct)
    {
        await next(message, ct); // handler runs first
        // Only reached on success — enqueue all pending outbox entries
        await _outbox.PublishPendingAsync(message.CorrelationId, ct);
    }
}
```

---

## 5. Concrete Handlers — Multiple Types, Same Base

```csharp
// ── Handler: OrderPlaced ──────────────────────────────────────────────────
public class OrderPlacedHandler : IMessageHandler<OrderPlacedMessage>
{
    private readonly IOrderRepository _repo;
    private readonly IOutboxService _outbox;

    public OrderPlacedHandler(IOrderRepository repo, IOutboxService outbox)
    {
        _repo   = repo;
        _outbox = outbox;
    }

    public async Task HandleAsync(OrderPlacedMessage message, CancellationToken ct)
    {
        // 1. Persist the order (idempotent upsert)
        await _repo.UpsertAsync(message.Body.OrderId, "Placed", ct);

        // 2. Schedule outbox entries — these are written to DB in the same transaction
        //    and will be published by OutboxMiddleware AFTER this handler succeeds.
        await _outbox.EnqueueAsync(message.CorrelationId, new OutboxEntry
        {
            IntegrationType = IntegrationType.PaymentGateway,
            Payload         = JsonSerializer.Serialize(new
            {
                orderId      = message.Body.OrderId,
                amount       = message.Body.TotalAmount,
                currency     = message.Body.Currency,
                customerId   = message.Body.CustomerId,
            }),
        }, ct);
    }
}

// ── Handler: OrderConfirmed ───────────────────────────────────────────────
public class OrderConfirmedHandler : IMessageHandler<OrderConfirmedMessage>
{
    private readonly IOrderRepository _repo;
    private readonly IOutboxService _outbox;

    public OrderConfirmedHandler(IOrderRepository repo, IOutboxService outbox)
    {
        _repo   = repo;
        _outbox = outbox;
    }

    public async Task HandleAsync(OrderConfirmedMessage message, CancellationToken ct)
    {
        await _repo.UpsertAsync(message.Body.OrderId, "Confirmed", ct);

        // Trigger warehouse reservation API via outbox
        await _outbox.EnqueueAsync(message.CorrelationId, new OutboxEntry
        {
            IntegrationType = IntegrationType.WarehouseApi,
            Payload         = JsonSerializer.Serialize(new
            {
                orderId    = message.Body.OrderId,
                confirmedBy= message.ConfirmedBy,
            }),
        }, ct);

        // Also trigger notification
        await _outbox.EnqueueAsync(message.CorrelationId, new OutboxEntry
        {
            IntegrationType = IntegrationType.NotificationService,
            Payload         = JsonSerializer.Serialize(new
            {
                customerId = message.Body.CustomerId,
                template   = "order_confirmed",
                orderId    = message.Body.OrderId,
            }),
        }, ct);
    }
}

// ── Handler: OrderShipped ─────────────────────────────────────────────────
public class OrderShippedHandler : IMessageHandler<OrderShippedMessage>
{
    private readonly IOrderRepository _repo;
    private readonly IOutboxService _outbox;

    public OrderShippedHandler(IOrderRepository repo, IOutboxService outbox)
    {
        _repo   = repo;
        _outbox = outbox;
    }

    public async Task HandleAsync(OrderShippedMessage message, CancellationToken ct)
    {
        await _repo.UpsertAsync(message.Body.OrderId, "Shipped", ct);

        await _outbox.EnqueueAsync(message.CorrelationId, new OutboxEntry
        {
            IntegrationType = IntegrationType.NotificationService,
            Payload         = JsonSerializer.Serialize(new
            {
                customerId = message.Body.CustomerId,
                template   = "order_shipped",
                trackingId = message.TrackingId,
                carrier    = message.Carrier,
            }),
        }, ct);
    }
}

// ── Handler: OrderCancelled ───────────────────────────────────────────────
public class OrderCancelledHandler : IMessageHandler<OrderCancelledMessage>
{
    private readonly IOrderRepository _repo;
    private readonly IOutboxService _outbox;

    public OrderCancelledHandler(IOrderRepository repo, IOutboxService outbox)
    {
        _repo   = repo;
        _outbox = outbox;
    }

    public async Task HandleAsync(OrderCancelledMessage message, CancellationToken ct)
    {
        await _repo.UpsertAsync(message.Body.OrderId, "Cancelled", ct);

        // Trigger refund + cancellation notification
        await _outbox.EnqueueAsync(message.CorrelationId, new OutboxEntry
        {
            IntegrationType = IntegrationType.PaymentGateway,
            Payload         = JsonSerializer.Serialize(new
            {
                orderId = message.Body.OrderId,
                action  = "refund",
                reason  = message.Reason,
            }),
        }, ct);

        await _outbox.EnqueueAsync(message.CorrelationId, new OutboxEntry
        {
            IntegrationType = IntegrationType.NotificationService,
            Payload         = JsonSerializer.Serialize(new
            {
                customerId = message.Body.CustomerId,
                template   = "order_cancelled",
                reason     = message.Reason,
            }),
        }, ct);
    }
}
```

---

## 6. Outbox Pattern — Guaranteed Integration Calls

The **Outbox Pattern** prevents the "dual write" problem:
> Without an outbox: handler saves to DB ✅ → calls Payment API ❌ (network failure) →
> message is retried → **Payment is called twice!** 💀

```csharp
// ── Outbox contracts ───────────────────────────────────────────────────────
public enum IntegrationType { PaymentGateway, WarehouseApi, NotificationService }

public class OutboxEntry
{
    public Guid EntryId { get; init; } = Guid.NewGuid();
    public IntegrationType IntegrationType { get; init; }
    public string Payload { get; init; } = string.Empty;
    public bool IsPublished { get; set; }
}

public interface IOutboxService
{
    /// <summary>Enqueues an integration call (written to DB in same txn as handler).</summary>
    Task EnqueueAsync(Guid correlationId, OutboxEntry entry, CancellationToken ct);

    /// <summary>Called by OutboxMiddleware after handler success to fire all pending entries.</summary>
    Task PublishPendingAsync(Guid correlationId, CancellationToken ct);
}

// ── Outbox integration router ──────────────────────────────────────────────
public interface IIntegrationCaller
{
    IntegrationType Type { get; }
    Task CallAsync(string payload, CancellationToken ct);
}

public class PaymentGatewayCaller : IIntegrationCaller
{
    public IntegrationType Type => IntegrationType.PaymentGateway;
    private readonly HttpClient _http;

    public PaymentGatewayCaller(HttpClient http) => _http = http;

    public async Task CallAsync(string payload, CancellationToken ct)
    {
        var response = await _http.PostAsync(
            "/api/payments",
            new StringContent(payload, System.Text.Encoding.UTF8, "application/json"),
            ct);
        response.EnsureSuccessStatusCode();
    }
}

// ── Outbox service implementation ─────────────────────────────────────────
public class OutboxService : IOutboxService
{
    private readonly IOutboxRepository _repo;
    // Key = IntegrationType, resolved from DI
    private readonly Dictionary<IntegrationType, IIntegrationCaller> _callers;
    private readonly ILogger<OutboxService> _logger;

    public OutboxService(
        IOutboxRepository repo,
        IEnumerable<IIntegrationCaller> callers,
        ILogger<OutboxService> logger)
    {
        _repo    = repo;
        _logger  = logger;
        _callers = callers.ToDictionary(c => c.Type);
    }

    public async Task EnqueueAsync(Guid correlationId, OutboxEntry entry, CancellationToken ct)
        => await _repo.InsertAsync(correlationId, entry, ct);

    public async Task PublishPendingAsync(Guid correlationId, CancellationToken ct)
    {
        var pending = await _repo.GetUnpublishedAsync(correlationId, ct);

        foreach (var entry in pending)
        {
            if (!_callers.TryGetValue(entry.IntegrationType, out var caller))
            {
                _logger.LogError("No caller registered for {Type}", entry.IntegrationType);
                continue;
            }

            try
            {
                await caller.CallAsync(entry.Payload, ct);
                await _repo.MarkPublishedAsync(entry.EntryId, ct);
            }
            catch (Exception ex)
            {
                // Don't fail the whole batch; a background job will retry unpublished entries
                _logger.LogWarning(ex, "Integration call failed for {EntryId}. Will retry.", entry.EntryId);
            }
        }
    }
}
```

> **Interviewer:** *"What if `PublishPendingAsync` itself fails halfway — some entries published, some not?"*  
> **You:** "Each entry is individually marked `IsPublished = true` in the DB **after** the HTTP call succeeds.
> A **separate background job** (e.g., a Hangfire recurring job every 30 seconds) re-scans for
> any entries older than N seconds that are still unpublished and retries them. This means
> integrations must be **idempotent** — we design our API calls with idempotency keys
> (`EntryId` is sent as `Idempotency-Key` header) so retries are safe."

---

## 7. The Dispatcher — Pipeline + Registry

```csharp
// ── Message type registry (auto-populated via Assembly scanning) ──────────
public interface IMessageTypeRegistry
{
    Type? Resolve(string messageType);
    void Register(string messageType, Type clrType);
}

public class MessageTypeRegistry : IMessageTypeRegistry
{
    private readonly Dictionary<string, Type> _map = new();

    public void Register(string messageType, Type clrType) => _map[messageType] = clrType;

    public Type? Resolve(string messageType)
        => _map.TryGetValue(messageType, out var t) ? t : null;
}

// ── Pipeline builder ───────────────────────────────────────────────────────
public class MessagePipeline
{
    private readonly IEnumerable<IMessageMiddleware> _middlewares;

    public MessagePipeline(IEnumerable<IMessageMiddleware> middlewares)
        => _middlewares = middlewares;

    public MessageHandlerDelegate Build(MessageHandlerDelegate terminal)
    {
        // Build in reverse so first middleware wraps outermost
        var pipeline = terminal;
        foreach (var middleware in _middlewares.Reverse())
        {
            var current = middleware;
            var next    = pipeline;
            pipeline    = (msg, ct) => current.InvokeAsync(msg, next, ct);
        }
        return pipeline;
    }
}

// ── Dispatcher ────────────────────────────────────────────────────────────
public interface IMessageDispatcher
{
    Task DispatchAsync(string messageType, string jsonPayload, CancellationToken ct);
}

public class MessageDispatcher : IMessageDispatcher
{
    private readonly IMessageTypeRegistry _registry;
    private readonly IServiceProvider _sp;
    private readonly MessagePipeline _pipeline;
    private readonly ILogger<MessageDispatcher> _logger;

    public MessageDispatcher(
        IMessageTypeRegistry registry,
        IServiceProvider sp,
        MessagePipeline pipeline,
        ILogger<MessageDispatcher> logger)
    {
        _registry = registry;
        _sp       = sp;
        _pipeline = pipeline;
        _logger   = logger;
    }

    public async Task DispatchAsync(string messageType, string jsonPayload, CancellationToken ct)
    {
        // ── Step 1: Resolve CLR type ──────────────────────────────────────
        var clrType = _registry.Resolve(messageType);
        if (clrType is null)
        {
            _logger.LogWarning("Unknown message type: {MessageType}. Dead-lettering.", messageType);
            throw new InvalidOperationException($"Unregistered message type: {messageType}");
        }

        // ── Step 2: Deserialize ───────────────────────────────────────────
        var message = (IMessage)JsonSerializer.Deserialize(jsonPayload, clrType)!;

        // ── Step 3: Resolve scoped handler via DI ─────────────────────────
        using var scope       = _sp.CreateScope();
        var handlerType       = typeof(IMessageHandler<>).MakeGenericType(clrType);
        var handler           = scope.ServiceProvider.GetRequiredService(handlerType);

        // ── Step 4: Build terminal delegate (the actual handler call) ─────
        MessageHandlerDelegate terminal = (msg, token) =>
        {
            var method = handlerType.GetMethod("HandleAsync")!;
            return (Task)method.Invoke(handler, new object[] { msg, token })!;
        };

        // ── Step 5: Wrap in pipeline and execute ──────────────────────────
        var executionPipeline = _pipeline.Build(terminal);
        await executionPipeline(message, ct);
    }
}
```

---

## 8. DI Registration & Assembly Scanning

```csharp
public static class MessageProcessorExtensions
{
    public static IServiceCollection AddMessageProcessor(
        this IServiceCollection services,
        params Assembly[] assemblies)
    {
        // ── Registry ──────────────────────────────────────────────────────
        var registry = new MessageTypeRegistry();

        // Scan assemblies for all IMessage implementations and register them
        var messageTypes = assemblies
            .SelectMany(a => a.GetTypes())
            .Where(t => t is { IsAbstract: false, IsInterface: false }
                     && t.IsAssignableTo(typeof(IMessage)));

        foreach (var type in messageTypes)
        {
            // Instantiate a dummy to get the MessageType discriminator string
            // (only works if default ctor is available; alternatively use [MessageTypeAttribute])
            if (Activator.CreateInstance(type) is IMessage sample)
                registry.Register(sample.MessageType, type);
        }

        services.AddSingleton<IMessageTypeRegistry>(registry);

        // ── Handlers ─────────────────────────────────────────────────────
        // Auto-register all IMessageHandler<T> implementations
        var handlerTypes = assemblies
            .SelectMany(a => a.GetTypes())
            .Where(t => t is { IsAbstract: false, IsInterface: false }
                     && t.GetInterfaces().Any(i =>
                            i.IsGenericType &&
                            i.GetGenericTypeDefinition() == typeof(IMessageHandler<>)));

        foreach (var handlerType in handlerTypes)
        {
            var iface = handlerType.GetInterfaces()
                .First(i => i.IsGenericType &&
                            i.GetGenericTypeDefinition() == typeof(IMessageHandler<>));
            services.AddScoped(iface, handlerType);
        }

        // ── Integration Callers ───────────────────────────────────────────
        services.AddScoped<IIntegrationCaller, PaymentGatewayCaller>();
        // Register others similarly...

        // ── Middleware Pipeline ───────────────────────────────────────────
        services.AddScoped<IMessageMiddleware, CorrelationLoggingMiddleware>();
        services.AddScoped<IMessageMiddleware, OrderGuardMiddleware>();
        services.AddScoped<IMessageMiddleware, OutboxMiddleware>();
        services.AddScoped<MessagePipeline>();

        // ── Core Services ─────────────────────────────────────────────────
        services.AddScoped<IOutboxService, OutboxService>();
        services.AddScoped<IOrderGuard, RedisOrderGuard>();
        services.AddScoped<IMessageDispatcher, MessageDispatcher>();

        return services;
    }
}
```

---

## 9. Full Flow Diagram

```
Azure Service Bus
       │
       │  { "messageType": "order.confirmed", "correlationId": "...", "sequenceNumber": 20, ... }
       ▼
┌─────────────────────────────────────────────────────────────────┐
│  Azure Function Trigger (ServiceBusTrigger)                     │
│  → extracts MessageType from UserProperties                     │
│  → calls IMessageDispatcher.DispatchAsync(type, payload, ct)    │
└───────────────────────┬─────────────────────────────────────────┘
                        │
                        ▼
             ┌──────────────────────┐
             │  MessageTypeRegistry │  ← resolves "order.confirmed" → OrderConfirmedMessage
             └──────────┬───────────┘
                        │ deserialize JSON
                        ▼
             ┌──────────────────────┐
             │  MessagePipeline     │
             │  ┌────────────────┐  │
             │  │ Logging MW     │  │  ← opens CorrelationId log scope
             │  ├────────────────┤  │
             │  │ OrderGuard MW  │  │  ← checks Redis: is "order.placed" complete?
             │  │                │  │     YES → allow   NO → defer message, return
             │  ├────────────────┤  │
             │  │ Outbox MW      │  │  ← wraps handler; publishes after success
             │  ├────────────────┤  │
             │  │ Handler        │  │  ← OrderConfirmedHandler:
             │  │ (terminal)     │  │     • upsert DB status="Confirmed"
             │  │                │  │     • enqueue WarehouseApi outbox entry
             │  │                │  │     • enqueue Notification outbox entry
             │  └────────────────┘  │
             └──────────┬───────────┘
                        │ success ✅
                        ▼
             ┌──────────────────────┐
             │  OutboxService       │
             │  PublishPendingAsync │  ← calls WarehouseApi HTTP
             │                      │  ← calls NotificationService HTTP
             │                      │  ← marks each entry IsPublished=true
             └──────────┬───────────┘
                        │
                        ▼
             ┌──────────────────────┐
             │  OrderGuard MW       │
             │  MarkProcessedAsync  │  ← updates Redis: CurrentStatus="order.confirmed"
             └──────────────────────┘
```

---

## 10. Deferral — Handling Out-of-Order Messages

```csharp
public interface IMessageDeferralService
{
    /// <summary>
    /// Parks the message for retry after a delay.
    /// Uses Service Bus message deferral (not dead-letter).
    /// </summary>
    Task DeferAsync(IMessage message, CancellationToken ct);
}

// ── Azure Service Bus implementation ──────────────────────────────────────
public class ServiceBusDeferralService : IMessageDeferralService
{
    private readonly ServiceBusReceiver _receiver;
    private readonly ILogger<ServiceBusDeferralService> _logger;

    public ServiceBusDeferralService(ServiceBusReceiver receiver, ILogger<ServiceBusDeferralService> logger)
    {
        _receiver = receiver;
        _logger   = logger;
    }

    public async Task DeferAsync(IMessage message, CancellationToken ct)
    {
        _logger.LogInformation(
            "[{CorrelationId}] Deferring {MessageType} seq={Seq} for later re-processing.",
            message.CorrelationId, message.MessageType, message.SequenceNumber);

        // Service Bus will hold the message and redeliver it after a scheduled time
        // The message is NOT dead-lettered — it stays in the queue
        // A separate "deferred message sweeper" job reschedules deferred messages
        await _receiver.DeferMessageAsync(
            /* ServiceBusReceivedMessage */ null!, // injected from function trigger context
            cancellationToken: ct);
    }
}
```

> **Interviewer:** *"What's the difference between deferral and dead-lettering?"*  
> **You:** "**Dead-lettering** is permanent removal to a side queue — the message won't be retried automatically.
> **Deferral** keeps the message in the main queue in a 'deferred' state. It won't be delivered
> again until explicitly re-received by its sequence number. We use deferral for out-of-order
> messages because they are *valid* — they just arrived too early. A sweeper job runs every
> N seconds, checks Redis for newly completed predecessor states, and re-schedules deferred messages."

---

## 11. Complete Interviewer Q&A

### Q1: Why does `BaseMessage<TBody>` use a generic body instead of putting all fields flat?

**A:** Single Responsibility. The **envelope** (`BaseMessage`) carries routing/correlation metadata
that belongs to the *infrastructure layer* (`MessageId`, `CorrelationId`, `SequenceNumber`).
The **body** (`TBody`) carries *domain data* that belongs to the *business layer*. Keeping them
separate means the dispatcher, middleware, and order guard can work on `IMessage` without knowing
anything about `OrderCorrelationContext`. They're decoupled.

---

### Q2: How does the Outbox Pattern prevent duplicate payments?

**A:** Instead of calling the payment API directly from the handler:
1. The handler writes an `OutboxEntry` to the **same database transaction** as the order update.
2. If the handler throws, the transaction rolls back — **no outbox entry, no duplicate call**.
3. If the Service Bus message is retried, the handler is idempotent (`UpsertAsync`) and the
   outbox entry is already `IsPublished = true`, so the outbox sweeper skips it.
4. We pass `EntryId` as the `Idempotency-Key` HTTP header so the payment gateway deduplicates
   on its side too.

This gives us **exactly-once semantics** at the application level.

---

### Q3: How does CorrelationId flow end-to-end across services?

**A:** 
- The **producer** (API gateway/order service) generates `CorrelationId = Guid.NewGuid()` when the order is placed.
- It sets it on the Service Bus message's `CorrelationId` property AND in the JSON body.
- Our **dispatcher** extracts it and puts it in the `ILogger.BeginScope` dictionary.
- **All log lines** within that message's processing automatically include `CorrelationId`.
- When the **outbox fires the HTTP calls**, the `CorrelationId` is sent as the `X-Correlation-ID`
  header so downstream services (payment, warehouse, notifications) include it in their logs too.
- This means a single `CorrelationId` can be used to query **every log line** across every service
  in the entire order flow in Application Insights / Splunk.

---

### Q4: What happens if two instances of the Azure Function process the same message concurrently?

**A:** This is the **competing consumers** problem. Azure Service Bus provides a **message lock** —
only one consumer can hold a message at a time. However, if both instances pick up the *same message
type for different orders* with the same `CorrelationId` (shouldn't happen but could due to bugs),
the **Redis `OrderGuard`** acts as a distributed lock. We can add an `IF NOT EXISTS` SET (SETNX)
atomic operation in Redis to ensure only one processor wins the race for a given `(CorrelationId, SequenceNumber)` pair.

---

### Q5: How do you add a new message type like `OrderReturned`?

**A:** Zero changes to the dispatcher, pipeline, or registry loader. Just:
1. Create `OrderReturnedMessage : BaseMessage<OrderCorrelationContext>` with `MessageType = "order.returned"`.
2. Create `OrderReturnedHandler : IMessageHandler<OrderReturnedMessage>`.
3. Add `"order.returned"` to `OrderFlowTransitions._allowed`.
4. Register the handler in DI (or let assembly scanning pick it up).

This is **Open/Closed Principle** in action — the system is open for extension, closed for modification.

---

## 12. Complexity Summary

```
Message arrives
     │
     ├─ Unknown type?           → Log warning, Dead-letter
     ├─ Deserialization fails?  → Throw, Dead-letter after MaxDeliveryCount
     ├─ Duplicate (seen seq#)?  → Idempotent skip, Complete message
     ├─ Out-of-order?           → Defer (park), return
     ├─ Illegal transition?     → Throw InvalidOperation, Dead-letter
     │
     ▼  (valid, in-order, new)
     Handler executes
     │
     ├─ Handler throws?         → Outbox NOT published, OrderGuard NOT updated
     │                            Service Bus retries (up to MaxDeliveryCount)
     │
     ▼  (handler success)
     Outbox publishes integrations (PaymentGateway / Warehouse / Notifications)
     │
     ├─ Integration call fails? → Entry stays unpublished, background sweeper retries
     │                            Idempotency-Key prevents duplicate charges
     │
     ▼  (integrations done)
     OrderGuard marks status in Redis
     Service Bus message Completed ✅
```
