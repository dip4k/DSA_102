# 03. DDD Order Management (DDD + CQRS)

## 📌 Context
Domain-Driven Design (DDD) combined with Command Query Responsibility Segregation (CQRS) is the gold standard for complex .NET backend services.
This problem tests whether you can encapsulate business logic in the Domain model (rich domain) instead of writing "anemic domain models" with logic bleeding into services.

---

## 1. Domain Layer: Value Objects & Aggregates

**Rule:** State changes should only happen via methods on the Aggregate Root. Setters should be private.

```csharp
// Value Object
public record Money(decimal Amount, string Currency)
{
    public static Money Zero(string currency = "USD") => new(0, currency);
    
    public Money Add(Money other)
    {
        if (Currency != other.Currency) throw new InvalidOperationException("Currency mismatch");
        return new Money(Amount + other.Amount, Currency);
    }
}

// Entity / Aggregate Root
public class Order
{
    public Guid Id { get; private set; }
    public string CustomerId { get; private set; }
    public OrderStatus Status { get; private set; }
    
    private readonly List<OrderItem> _items = new();
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();
    
    // Domain Events list (Dispatched during EF Core SaveChanges)
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public Order(string customerId)
    {
        Id = Guid.NewGuid();
        CustomerId = customerId;
        Status = OrderStatus.Draft;
    }

    public void AddItem(string productId, string name, int quantity, Money price)
    {
        if (Status != OrderStatus.Draft)
            throw new InvalidOperationException("Can only add items to draft orders.");

        _items.Add(new OrderItem(Id, productId, name, quantity, price));
    }

    public void Submit()
    {
        if (!_items.Any())
            throw new InvalidOperationException("Cannot submit empty order.");
            
        Status = OrderStatus.Submitted;
        
        // Register Domain Event
        _domainEvents.Add(new OrderSubmittedDomainEvent(Id, CustomerId));
    }
    
    public void ClearDomainEvents() => _domainEvents.Clear();
}

public enum OrderStatus { Draft, Submitted, Shipped, Cancelled }
```

---

## 2. Application Layer: CQRS with MediatR

Use the `MediatR` library to decouple the "what to do" from the "how to do it".

### The Command
```csharp
public record SubmitOrderCommand(Guid OrderId) : IRequest<Result>;

public class SubmitOrderCommandHandler : IRequestHandler<SubmitOrderCommand, Result>
{
    private readonly ApplicationDbContext _dbContext;

    public SubmitOrderCommandHandler(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result> Handle(SubmitOrderCommand request, CancellationToken ct)
    {
        var order = await _dbContext.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == request.OrderId, ct);

        if (order == null) return Result.Fail("Order not found.");

        // The business logic is inside the domain!
        order.Submit();

        // EF Core will save changes. 
        // Note: A custom Interceptor handles DomainEvents before committing.
        await _dbContext.SaveChangesAsync(ct);

        return Result.Ok();
    }
}
```

### The Query
Queries completely bypass the Domain model. They use lightweight DTOs and often raw SQL (e.g., Dapper) for performance.

```csharp
public record GetOrderSummaryQuery(Guid OrderId) : IRequest<OrderSummaryDto>;

public class GetOrderSummaryQueryHandler : IRequestHandler<GetOrderSummaryQuery, OrderSummaryDto>
{
    private readonly IDbConnection _dbConnection;

    public GetOrderSummaryQueryHandler(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<OrderSummaryDto> Handle(GetOrderSummaryQuery request, CancellationToken ct)
    {
        // Use Dapper for fast, read-only queries
        const string sql = "SELECT Id, Status, CustomerId FROM Orders WHERE Id = @Id";
        return await _dbConnection.QueryFirstOrDefaultAsync<OrderSummaryDto>(sql, new { Id = request.OrderId });
    }
}
```
---

## 🗣️ Interviewer Discussion & Tradeoffs

**Interviewer:** *"Why did you make `Items` an `IReadOnlyCollection` and the `Order` setters `private`? Why not just use public properties with `{ get; set; }`?"*
**You:** "This prevents an **Anemic Domain Model**. If I use public setters, any service anywhere in the code can change the order status to `Shipped` without ensuring that payment was collected. By keeping state mutations inside the `Order` entity (e.g., via the `Submit()` method), the `Order` acts as an **Aggregate Root** that strictly enforces its own business rules and invariants."

**Interviewer:** *"Why do you add `DomainEvents` to a list inside the entity instead of just publishing them immediately to MediatR inside the `Submit()` method?"*
**You:** "Because of the **Dual Write / Consistency Problem**. If I publish the event immediately, handlers might send an email or charge a credit card. But what if the subsequent `_dbContext.SaveChangesAsync()` fails due to a database outage? The email was sent, but the order wasn't saved! By storing the events in the entity, an EF Core Interceptor can grab them and publish them *only* as part of a successful database transaction."

**Interviewer:** *"Why split `SubmitOrderCommand` and `GetOrderSummaryQuery` into separate classes? (CQRS)"*
**You:** "Because reads and writes have different scaling and modeling needs. The write side (Command) needs the heavy Entity Framework Domain Model to validate business rules. The read side (Query) just needs to blast data to the UI as fast as possible. Splitting them lets me use EF Core for writes, and a micro-ORM like Dapper for ultra-fast, lock-free reads, completely bypassing the domain model overhead."
