# Layer 7: Senior Backend Architecture Patterns

Senior .NET interviews demand a strong grasp of distributed resiliency, event-driven consistency, and architectural boundaries.

---

## 1. The Dual Write Problem & The Transactional Outbox Pattern

### The Problem:
An API endpoint needs to update the local database AND publish an event to Azure Service Bus or Kafka.

```csharp
// ❌ ANTI-PATTERN: Dual Write Hazard
await _dbContext.SaveChangesAsync(); // 1. DB write succeeds
await _serviceBus.PublishAsync(new OrderPlacedEvent(order.Id)); // 2. Network timeout! Message lost.
// Result: Inconsistent system. DB has the order, but downstream services never know.
```

### The Solution: Transactional Outbox Pattern
Write both the business entity AND an `OutboxMessage` record inside the **same ACID database transaction**. A reliable background worker polls or streams the outbox messages to the broker.

```
[ Client Request ] 
       |
       v
[ Web API ]
       |
       v
[ Local SQL DB Transaction ] 
   ├── 1. INSERT INTO Orders (Business Data)
   └── 2. INSERT INTO OutboxMessages (Event Payload)
   COMMIT; (Guaranteed Atomic)
       ^
       | Polls / Tails Transaction Log
[ Outbox Background Worker ]
       |
       v  (At-Least-Once Delivery)
[ Azure Service Bus / RabbitMQ / Kafka ]
```

### EF Core SaveChanges Interceptor for Outbox
Automatically convert domain events into Outbox rows before committing:

```csharp
public class ConvertDomainEventsToOutboxMessagesInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData, 
        InterceptionResult<int> result, 
        CancellationToken cancellationToken = default)
    {
        var dbContext = eventData.Context;
        if (dbContext is null) return base.SavingChangesAsync(eventData, result, cancellationToken);

        var domainEvents = dbContext.ChangeTracker
            .Entries<AggregateRoot>()
            .Select(x => x.Entity)
            .SelectMany(aggregate =>
            {
                var events = aggregate.DomainEvents.ToList();
                aggregate.ClearDomainEvents();
                return events;
            })
            .Select(domainEvent => new OutboxMessage
            {
                Id = Guid.NewGuid(),
                OccurredOnUtc = DateTime.UtcNow,
                Type = domainEvent.GetType().Name,
                Content = JsonSerializer.Serialize(domainEvent, domainEvent.GetType())
            })
            .ToList();

        dbContext.Set<OutboxMessage>().AddRange(domainEvents);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
```

---

## 2. Idempotency (Preventing Duplicate Executions)

In distributed architectures, message brokers guarantee **At-Least-Once** delivery, not Exactly-Once. Your consumers and APIs must be **Idempotent**.

### The Idempotency Key Pattern
1. Client sends an HTTP header: `Idempotency-Key: 8b7d7f76-92c1-4b10-8b1b-7a0e3a47321e`.
2. Server queries an `IdempotentRequests` table inside the transaction:
   * If key exists: Return previously saved response immediately.
   * If key does not exist: Process request, store result in `IdempotentRequests` table, commit transaction.

```csharp
public class IdempotentOrderConsumer
{
    private readonly ApplicationDbContext _dbContext;

    public async Task ProcessAsync(OrderPlacedEvent message, CancellationToken ct)
    {
        // 1. Check if message was already handled
        bool alreadyProcessed = await _dbContext.ProcessedMessages
            .AnyAsync(m => m.MessageId == message.MessageId, ct);

        if (alreadyProcessed)
        {
            // Acknowledge and skip duplicate
            return;
        }

        // 2. Perform business update and record idempotency entry in ONE transaction
        using var tx = await _dbContext.Database.BeginTransactionAsync(ct);

        await _dbContext.Orders.AddAsync(new Order(message.OrderId), ct);
        await _dbContext.ProcessedMessages.AddAsync(new ProcessedMessage(message.MessageId, DateTime.UtcNow), ct);

        await _dbContext.SaveChangesAsync(ct);
        await tx.CommitAsync(ct);
    }
}
```

---

## 3. CQRS (Command Query Responsibility Segregation)

Separates read operations (Queries) from write operations (Commands).

```
[ User Action ]
     │
     ├── Writes (Commands) ──> [ MediatR CommandHandler ] ──> [ EF Core / DDD Aggregate ] ──> [ Write DB ]
     │                                                                                              │
     │                                                                                        Replication / Sync
     │                                                                                              v
     └── Reads (Queries)   ──> [ MediatR QueryHandler ]   ──> [ Dapper / Raw SQL / Read DB ] ──> [ Fast DTOs ]
```

### Tradeoff Discussion:
* **Commands:** Heavily validated, enforce invariants, raise domain events.
* **Queries:** Bypass domain entities completely. Use micro-ORMs like Dapper with `AsNoTracking()` to map SQL directly to flat DTOs for maximum throughput and minimal allocations.

---

## 4. Clean Architecture Directory Structure

```
src/
├── Domain/                 # Enterprise Business Rules (Entities, Value Objects, Domain Events)
│   ├── Common/             # Base Entity, ValueObject, IAggregateRoot
│   ├── Entities/           # Order.cs, Customer.cs
│   └── ValueObjects/       # Money.cs, Address.cs
│
├── Application/            # Application Business Rules (Use Cases, CQRS Handlers)
│   ├── Commands/           # CreateOrderCommand.cs, CreateOrderCommandHandler.cs
│   ├── Queries/            # GetOrderByIdQuery.cs, OrderDto.cs
│   └── Interfaces/         # IOrderRepository.cs, IEmailService.cs
│
├── Infrastructure/         # External Concerns (DB, Cloud, 3rd Party SDKs)
│   ├── Persistence/        # ApplicationDbContext.cs, Migrations/
│   ├── Messaging/          # AzureServiceBusPublisher.cs
│   └── Services/           # SendGridEmailService.cs
│
└── WebApi/                 # Presentation Layer (Controllers, Middlewares, Program.cs)
    ├── Controllers/        # OrdersController.cs
    └── Program.cs          # DI Registrations
```
* **Dependency Rule:** Source code dependencies point strictly inwards: `WebApi -> Infrastructure -> Application -> Domain`. The `Domain` project has ZERO external dependencies.