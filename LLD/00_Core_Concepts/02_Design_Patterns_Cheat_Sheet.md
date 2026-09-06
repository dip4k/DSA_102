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

## 1. Strategy Pattern (Behavioral) — 🔥 Tier 1 (Must Master)
* **Design Pressure:** Multiple algorithms or policies exist for the same operation (e.g., pricing, payment gateways, spot allocation).
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
* **Design Pressure:** Dynamic instantiation based on runtime data (e.g., message type or customer tier) without coupling the caller to concrete classes.
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
* **Design Pressure:** Object behavior drastically changes based on its status, leading to messy nested `if (status == X)` conditionals (e.g., Vending Machine, Order lifecycle, Elevator).
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
* **Design Pressure:** One-to-many dependency where state change in one object requires automatic updates across decoupled listeners.
* **Modern C# Application:** In modern .NET, direct raw Observer interfaces are often replaced with `MediatR` (`INotification` / `INotificationHandler<T>`) for in-process decoupling or Azure Service Bus / RabbitMQ for distributed events.

---

## 5. Decorator Pattern (Structural) — 🔥 Tier 1 (Must Master)
* **Design Pressure:** Adding responsibilities (caching, logging, metrics, retries) dynamically without modifying the original class or resorting to subclassing.
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
* **Design Pressure:** Integrating a third-party SDK or legacy class whose interface does not match your application's domain interface.
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

## 7. Chain of Responsibility (Behavioral) — 🟡 Tier 2 (Know Well)
* **Design Pressure:** Passing a request along a chain of potential handlers where each handler decides whether to process or forward (e.g., ASP.NET Core Middleware, Logging frameworks, Multi-tier approval workflows).
* **C# Pattern:** Handlers maintain a reference to `_next` and invoke `_next.Handle(request)`.