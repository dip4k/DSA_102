# Layer 3: Design Patterns Master Cheat Sheet (80/20)

Do not attempt to memorize all 23 GoF patterns equally. In Senior LLD interviews, 7 patterns solve 90% of architectural challenges.

---

## 🧭 Pattern Selection Decision Matrix

```
Requirement                                         -> Recommended Pattern
-----------------------------------------------------------------------------
Algorithm or business rule varies dynamically        -> Strategy Pattern
Creation logic complex or depends on runtime inputs  -> Factory / Abstract Factory
Object behavior alters as internal state changes    -> State Pattern
1-to-many event notification across subsystems       -> Observer Pattern
Add cross-cutting concerns (retry, log, cache)       -> Decorator Pattern
Incompatible interface from 3rd party SDK            -> Adapter Pattern
Step-by-step request filtering pipeline              -> Chain of Responsibility
Hierarchical tree structure (e.g. Org chart, Menu)   -> Composite Pattern
Complex multi-step object creation with options      -> Builder Pattern
Encapsulating requests as standalone invokable units -> Command Pattern
```

---

## 📚 Complete GoF Pattern Directory (with AlgoMaster Links)

| Creational Patterns | Structural Patterns | Behavioral Patterns |
|---|---|---|
| [Singleton](https://algomaster.io/learn/lld/singleton) | [Adapter](https://algomaster.io/learn/lld/adapter) | [Strategy](https://algomaster.io/learn/lld/strategy) |
| [Factory Method](https://algomaster.io/learn/lld/factory-method) | [Decorator](https://algomaster.io/learn/lld/decorator) | [Observer](https://algomaster.io/learn/lld/observer) |
| [Abstract Factory](https://algomaster.io/learn/lld/abstract-factory) | [Composite](https://algomaster.io/learn/lld/composite) | [State](https://algomaster.io/learn/lld/state) |
| [Builder](https://algomaster.io/learn/lld/builder) | [Facade](https://algomaster.io/learn/lld/facade) | [Command](https://algomaster.io/learn/lld/command) |
| [Prototype](https://algomaster.io/learn/lld/prototype) | [Bridge](https://algomaster.io/learn/lld/bridge) | [Chain of Responsibility](https://algomaster.io/learn/lld/chain-of-responsibility) |
| | [Proxy](https://algomaster.io/learn/lld/proxy) | [Iterator](https://algomaster.io/learn/lld/iterator) |
| | [Flyweight](https://algomaster.io/learn/lld/flyweight) | [Template Method](https://algomaster.io/learn/lld/template-method) |
| | | [Mediator](https://algomaster.io/learn/lld/mediator) · [Memento](https://algomaster.io/learn/lld/memento) · [Visitor](https://algomaster.io/learn/lld/visitor) |

---

## 1. Strategy Pattern (Behavioral) — 🔥 Tier 1 (Must Master)
* **When to use:** Multiple interchangeable algorithms, fee calculations, routing policies, or sorting strategies that can vary at runtime.
* **When to avoid:** Only one algorithm exists and is unlikely to change (avoids over-engineering).
* **📚 Learn:** [AlgoMaster: Strategy Pattern](https://algomaster.io/learn/lld/strategy)
* **C# Implementation:**
```csharp
public interface IPaymentStrategy
{
    PaymentProvider Provider { get; }
    Task<PaymentResult> ProcessAsync(decimal amount, CancellationToken ct);
}

public class StripePaymentStrategy : IPaymentStrategy
{
    public PaymentProvider Provider => PaymentProvider.Stripe;
    public async Task<PaymentResult> ProcessAsync(decimal amount, CancellationToken ct)
    {
        // Call Stripe SDK
        return new PaymentResult(Success: true, TransactionId: Guid.NewGuid().ToString());
    }
}
```
* **Senior Interview Tip:** Never combine Strategy with an internal `switch` statement in the consumer. Inject `IEnumerable<IPaymentStrategy>` into a Factory or dictionary to resolve dynamically.

---

## 2. Factory Pattern (Creational) — 🔥 Tier 1 (Must Master)
* **When to use:** Creation logic depends on dynamic runtime parameters (e.g. message type, vehicle type, payment mode) without coupling callers to concrete classes.
* **When to avoid:** Simple object creation with `new` is sufficient and no polymorphism is needed.
* **📚 Learn:** [AlgoMaster: Factory Method](https://algomaster.io/learn/lld/factory-method) · [Abstract Factory](https://algomaster.io/learn/lld/abstract-factory)
* **C# Implementation with .NET DI:**
```csharp
public interface IPaymentStrategyFactory
{
    IPaymentStrategy GetStrategy(PaymentProvider provider);
}

public class PaymentStrategyFactory : IPaymentStrategyFactory
{
    private readonly Dictionary<PaymentProvider, IPaymentStrategy> _strategies;

    // Injected automatically by ASP.NET Core DI when multiple implementations are registered
    public PaymentStrategyFactory(IEnumerable<IPaymentStrategy> strategies)
    {
        _strategies = strategies.ToDictionary(s => s.Provider);
    }

    public IPaymentStrategy GetStrategy(PaymentProvider provider)
    {
        if (!_strategies.TryGetValue(provider, out var strategy))
            throw new NotSupportedException($"Provider {provider} is not supported.");
        return strategy;
    }
}
```

---

## 3. State Pattern (Behavioral) — 🔥 Tier 1 (Must Master)
* **When to use:** Object behavior changes drastically depending on its internal status (e.g. Order workflow, Vending Machine, ATM, Room lifecycle).
* **When to avoid:** State transitions are trivial flags (e.g. boolean `IsActive`) with no differing behavior across states.
* **📚 Learn:** [AlgoMaster: State Pattern](https://algomaster.io/learn/lld/state)
* **C# Implementation:**
```csharp
public interface IOrderState
{
    void Cancel(OrderContext context);
    void Ship(OrderContext context);
}

public class PaidOrderState : IOrderState
{
    public void Cancel(OrderContext context)
    {
        Console.WriteLine("Order cancelled. Initiating refund.");
        context.TransitionTo(new CancelledOrderState());
    }

    public void Ship(OrderContext context)
    {
        Console.WriteLine("Dispatching shipment.");
        context.TransitionTo(new ShippedOrderState());
    }
}

public class ShippedOrderState : IOrderState
{
    public void Cancel(OrderContext context) => 
        throw new InvalidOperationException("Cannot cancel an order that has already shipped.");
    public void Ship(OrderContext context) => 
        throw new InvalidOperationException("Order is already shipped.");
}
```

---

## 4. Observer Pattern (Behavioral) — 🔥 Tier 1 (Must Master)
* **When to use:** State changes in one object require decoupled, automatic notifications to one or more subscribers (e.g. Waitlist alerts, Order status broadcasts, Cache invalidations).
* **When to avoid:** Synchronous point-to-point method calls where loose coupling is unnecessary.
* **📚 Learn:** [AlgoMaster: Observer Pattern](https://algomaster.io/learn/lld/observer)
* **Modern C# Application:** In modern .NET, direct raw Observer interfaces are often replaced with `MediatR` (`INotification` / `INotificationHandler<T>`) for in-process decoupling or Azure Service Bus / RabbitMQ for distributed events.

---

## 5. Decorator Pattern (Structural) — 🔥 Tier 1 (Must Master)
* **When to use:** Adding cross-cutting responsibilities (caching, logging, metrics, Polly retries, circuit breakers) dynamically without modifying the underlying class.
* **When to avoid:** Modifying simple logic directly within the class when no separate concern exists.
* **📚 Learn:** [AlgoMaster: Decorator Pattern](https://algomaster.io/learn/lld/decorator)
* **C# Implementation:**
```csharp
public interface ICustomerService
{
    Task<CustomerDto?> GetCustomerAsync(Guid id, CancellationToken ct);
}

public class CachedCustomerServiceDecorator : ICustomerService
{
    private readonly ICustomerService _inner;
    private readonly IMemoryCache _cache;

    public CachedCustomerServiceDecorator(ICustomerService inner, IMemoryCache cache)
    {
        _inner = inner;
        _cache = cache;
    }

    public async Task<CustomerDto?> GetCustomerAsync(Guid id, CancellationToken ct)
    {
        return await _cache.GetOrCreateAsync($"customer:{id}", async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
            return await _inner.GetCustomerAsync(id, ct);
        });
    }
}
```

---

## 6. Adapter Pattern (Structural) — 🟡 Tier 2 (Know Well)
* **When to use:** Integrating third-party SDKs, external APIs, or legacy classes whose interface does not match your clean domain interfaces.
* **When to avoid:** Both interfaces are under your control (refactor to a common interface instead).
* **📚 Learn:** [AlgoMaster: Adapter Pattern](https://algomaster.io/learn/lld/adapter)
* **C# Implementation:**
```csharp
// Third-party SDK (Cannot change this code)
public class SendGridClient 
{ 
    public void SendEmail(string toAddress, string htmlBody) { } 
}

// Our domain abstraction
public interface IEmailSender 
{ 
    Task SendAsync(string recipient, string message, CancellationToken ct); 
}

// Adapter converts the interface
public class SendGridEmailAdapter : IEmailSender
{
    private readonly SendGridClient _client;
    public SendGridEmailAdapter(SendGridClient client) => _client = client;

    public Task SendAsync(string recipient, string message, CancellationToken ct)
    {
        _client.SendEmail(recipient, message);
        return Task.CompletedTask;
    }
}
```

---

## 7. Composite Pattern (Structural) — 🟡 Tier 2 (Know Well)
* **When to use:** Hierarchical tree structures (File systems, Epic/Story/Task trees, Organisation charts) where clients must treat individual objects and compositions uniformly.
* **When to avoid:** Flat collections or non-recursive relationships.
* **📚 Learn:** [AlgoMaster: Composite Pattern](https://algomaster.io/learn/lld/composite)
* **Flagship Problems in Repo:** [In-Memory File System](../02_Tier2_Classic_LLD/24_In_Memory_File_System.md), [Task Management System](../02_Tier2_Classic_LLD/18_Task_Management_System.md).

---

## 8. Command Pattern (Behavioral) — 🟡 Tier 2 (Know Well)
* **When to use:** Encapsulating requests as standalone invokable objects to support Undo/Redo, queuing, logging, or delayed execution.
* **When to avoid:** Direct synchronous execution where no history or rollback is needed.
* **📚 Learn:** [AlgoMaster: Command Pattern](https://algomaster.io/learn/lld/command)
* **Flagship Problems in Repo:** [Board Game Engine](../02_Tier2_Classic_LLD/25_Board_Game_Engine.md), [Task Management System](../02_Tier2_Classic_LLD/18_Task_Management_System.md).

---

## 9. Chain of Responsibility (Behavioral) — 🟡 Tier 2 (Know Well)
* **When to use:** Passing a request along a sequence of potential handlers (ASP.NET Core Middleware, request filters, log routing, validation pipelines).
* **When to avoid:** A request must be handled by exactly one known handler without delegation.
* **📚 Learn:** [AlgoMaster: Chain of Responsibility](https://algomaster.io/learn/lld/chain-of-responsibility)
* **Flagship Problem in Repo:** [Logger](../02_Tier2_Classic_LLD/13_Logger.md).

---

## 10. Builder & Singleton Patterns (Creational)
* **Builder:** Constructing complex objects with numerous optional parameters step-by-step (e.g. `HttpRequestMessageBuilder`, SQL Query Builder).
  * 📚 [AlgoMaster: Builder Pattern](https://algomaster.io/learn/lld/builder)
* **Singleton:** Ensuring a single shared instance with lazy initialization and thread safety (e.g. `MemoryCache`, connection pools).
  * 📚 [AlgoMaster: Singleton Pattern](https://algomaster.io/learn/lld/singleton)
  * *Senior Tip:* In .NET, use `AddSingleton<T>()` in Microsoft DI rather than a static `Instance` property to allow testability.