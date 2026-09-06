# 04. DDD + CQRS + Azure Service Bus (The Outbox Pattern)

## 📌 Context
In distributed systems, the **Dual Write Problem** is a common interview topic. 
*Scenario:* You save an Order to SQL Server, and then you publish an `OrderCreated` message to Azure Service Bus. What happens if the database commits, but the network fails before reaching Service Bus?
*Solution:* The Transactional Outbox Pattern.

---

## 1. The Outbox Entity
Create an entity representing a message waiting to be sent.

```csharp
public class OutboxMessage
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Type { get; set; } = string.Empty;
    public string Payload { get; set; } = string.Empty;
    public DateTimeOffset OccurredOn { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? ProcessedOn { get; set; }
    public string? Error { get; set; }
}
```

---

## 2. EF Core SaveChanges Interceptor
We use an EF Core interceptor to grab the in-memory Domain Events from our Aggregate Roots, serialize them, and append them to the `OutboxMessages` table in the *same transaction*.

```csharp
public sealed class ConvertDomainEventsToOutboxMessagesInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        DbContext? dbContext = eventData.Context;
        if (dbContext is null) return base.SavingChangesAsync(eventData, result, cancellationToken);

        // 1. Get entities with uncommitted events
        var entities = dbContext.ChangeTracker
            .Entries<Entity>() // Assuming a base Entity class
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Any())
            .ToList();

        // 2. Convert to OutboxMessage
        var outboxMessages = entities
            .SelectMany(e => 
            {
                var events = e.DomainEvents.ToList();
                e.ClearDomainEvents();
                return events;
            })
            .Select(domainEvent => new OutboxMessage
            {
                Type = domainEvent.GetType().Name,
                Payload = JsonSerializer.Serialize(domainEvent, new JsonSerializerOptions { 
                    ReferenceHandler = ReferenceHandler.IgnoreCycles 
                })
            })
            .ToList();

        // 3. Add to DB context (will be saved in the same transaction)
        dbContext.Set<OutboxMessage>().AddRange(outboxMessages);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
```

---

## 3. The Background Dispatcher (Worker Service)
Now we need a background worker to poll the Outbox table and push to Azure Service Bus.

```csharp
public class OutboxBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<OutboxBackgroundService> _logger;

    public OutboxBackgroundService(IServiceProvider serviceProvider, ILogger<OutboxBackgroundService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await ProcessOutboxMessagesAsync(stoppingToken);
            // Delay to prevent CPU thrashing
            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }

    private async Task ProcessOutboxMessagesAsync(CancellationToken ct)
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var serviceBusSender = scope.ServiceProvider.GetRequiredService<ServiceBusSender>();

        var messages = await dbContext.OutboxMessages
            .Where(m => m.ProcessedOn == null)
            .Take(20)
            .ToListAsync(ct);

        foreach (var outboxMessage in messages)
        {
            try
            {
                var serviceBusMessage = new ServiceBusMessage(outboxMessage.Payload)
                {
                    MessageId = outboxMessage.Id.ToString(), // For Service Bus deduplication
                    Subject = outboxMessage.Type
                };

                await serviceBusSender.SendMessageAsync(serviceBusMessage, ct);

                outboxMessage.ProcessedOn = DateTimeOffset.UtcNow;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send outbox message {Id}", outboxMessage.Id);
                outboxMessage.Error = ex.Message;
            }
        }

        await dbContext.SaveChangesAsync(ct);
    }
}
```
---

## 🗣️ Interviewer Discussion & Tradeoffs

**Interviewer:** *"Why use the Outbox pattern instead of just publishing to Azure Service Bus right after `await dbContext.SaveChangesAsync()`?"*
**You:** "Because the network call to Azure Service Bus could fail (timeout, downtime). If the DB saves successfully but the Service Bus call fails, the system is in an inconsistent state. The Outbox pattern guarantees **Atomicity**: the business data and the event data are saved in the exact same SQL transaction. If one fails, both fail."

**Interviewer:** *"Your background service uses polling (`Take(20)` every 10 seconds). What are the downsides of this?"*
**You:** "Polling introduces latency (up to 10 seconds delay) and puts continuous load on the database even when there are no messages. A more advanced, senior-level approach is **Transaction Log Tailing** (Change Data Capture / CDC) using a tool like Debezium. Debezium reads the SQL Server transaction log directly and streams it to Kafka/Service Bus without polling the database at all."

**Interviewer:** *"What happens if the BackgroundWorker reads the Outbox message, sends it to Service Bus, but crashes right before calling `ProcessedOn = DateTimeOffset.UtcNow`?"*
**You:** "When the worker restarts, it will read that same message again and send a duplicate to Service Bus. This guarantees **At-Least-Once Delivery**. Because of this, the consumer on the other side *must* be designed to be **Idempotent** (e.g., by checking if the `MessageId` has already been processed in its own database before acting)."
