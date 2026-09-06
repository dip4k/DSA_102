# Layer 4: Domain-Driven Design (DDD) & Domain Modeling

Domain-Driven Design is the key differentiator between a Mid-Level and Senior/Lead .NET Engineer. In product-ready LLD interviews, interviewers assess whether you model real business rules into domain objects rather than writing procedural scripts disguised as classes.

---

## 1. Entities vs. Value Objects

| Aspect | Entity | Value Object |
| :--- | :--- | :--- |
| **Identity** | Has a distinct, permanent `Id` (e.g., `Guid`, `int`). | Has **NO identity**. Defined solely by the equality of its properties. |
| **Mutability** | State changes over its lifecycle (via methods). | **Strictly Immutable**. Any change produces a new instance. |
| **Equality** | Compared by `Id` (two users with identical names are different if IDs differ). | Structural Equality (two $100 bills or two identical addresses are equivalent). |
| **C# Feature** | Standard class with encapsulated state. | C# 9+ `record` or class overriding `Equals`/`GetHashCode`. |

### C# Value Object Implementation:
```csharp
// Value Objects should be immutable records with validation in constructor
public record Money
{
    public decimal Amount { get; }
    public string Currency { get; }

    public Money(decimal amount, string currency)
    {
        if (amount < 0)
            throw new ArgumentException("Amount cannot be negative.", nameof(amount));
        if (string.IsNullOrWhiteSpace(currency) || currency.Length != 3)
            throw new ArgumentException("Currency must be a 3-letter ISO code.", nameof(currency));

        Amount = amount;
        Currency = currency.ToUpperInvariant();
    }

    public Money Add(Money other)
    {
        if (Currency != other.Currency)
            throw new InvalidOperationException($"Cannot add money of different currencies: {Currency} and {other.Currency}");
        return new Money(Amount + other.Amount, Currency);
    }
}

public record Address(string Street, string City, string PostalCode, string Country);
```

---

## 2. Aggregate Roots & Invariant Protection

An **Aggregate** is a cluster of associated objects treated as a single transactional unit.
An **Aggregate Root** is the single entry point. External code is strictly forbidden from mutating child entities directly.

```
+-------------------------------------------------------------+
|                      ORDER AGGREGATE                        |
|                                                             |
|   +-----------------------------------------------------+   |
|   |             Order (Aggregate Root)                  |   |
|   |  - Id: Guid                                         |   |
|   |  - Status: OrderStatus                              |   |
|   |  - CustomerId: Guid                                 |   |
|   |  + AddItem(productId, price, qty)                   |   |
|   |  + Submit()                                         |   |
|   +-----------------------------------------------------+   |
|                             |                               |
|              Contains private collection                    |
|                             v                               |
|   +-----------------------------------------------------+   |
|   |                   OrderItem                         |   |
|   |  - ProductId: Guid                                  |   |
|   |  - UnitPrice: Money                                 |   |
|   |  - Quantity: int                                    |   |
|   +-----------------------------------------------------+   |
+-------------------------------------------------------------+
```

### Aggregate Root Invariant Rule:
1. External services never query or save `OrderItem` repository. There is only `IOrderRepository`.
2. All modifications to `OrderItem` must pass through methods on the `Order` root (`order.AddItem(...)`).
3. Properties exposing collections must return `IReadOnlyCollection<T>` to prevent callers from calling `.Add()` behind the root's back.

---

## 3. Domain Events (Decoupling Side Effects)

When an important business event occurs inside the aggregate, the aggregate records a **Domain Event**.

```csharp
public interface IDomainEvent
{
    DateTimeOffset OccurredOn { get; }
}

public record OrderSubmittedDomainEvent(Guid OrderId, Guid CustomerId, decimal TotalAmount) : IDomainEvent
{
    public DateTimeOffset OccurredOn { get; } = DateTimeOffset.UtcNow;
}

public abstract class AggregateRoot
{
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
```

### Why not publish Domain Events immediately inside the entity?
* **The Dual Write Hazard:** If you publish an event (e.g. via MediatR) inside `order.Submit()`, an email might be sent immediately. If the subsequent `dbContext.SaveChangesAsync()` fails due to a database deadlock or constraint violation, the email was sent for an order that was never saved!
* **The Correct Pattern:** Staged Domain Events are dispatched by an EF Core `SaveChangesInterceptor` or Unit of Work *only after the database transaction commits successfully*.

---

## 4. The Specification Pattern (Reusable Query Invariants)
Instead of littering LINQ queries like `.Where(o => o.Status == Active && o.Total > 100)` across controllers:

```csharp
public interface ISpecification<T>
{
    Expression<Func<T, bool>> Criteria { get; }
    bool IsSatisfiedBy(T entity);
}

public class HighValueActiveOrdersSpec : ISpecification<Order>
{
    public Expression<Func<T, bool>> Criteria => 
        order => order.Status == OrderStatus.Paid && order.TotalAmount > 1000m;

    public bool IsSatisfiedBy(Order order) =>
        order.Status == OrderStatus.Paid && order.TotalAmount > 1000m;
}
```