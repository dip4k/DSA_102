# Layer 1 & 2: OOP & SOLID Principles (Deep Dive)

## 📌 The Golden Rule of OOP
> **"Encapsulate what varies, favor composition over inheritance, and program to interfaces, not implementations."**

### 📚 AlgoMaster OOP Quick-Reference Links
Use these to drill individual concepts before an interview:
| Concept | Link |
|---|---|
| Classes & Objects | [algomaster.io/learn/lld/classes-and-objects](https://algomaster.io/learn/lld/classes-and-objects) |
| Interfaces | [algomaster.io/learn/lld/interfaces](https://algomaster.io/learn/lld/interfaces) |
| Encapsulation | [algomaster.io/learn/lld/encapsulation](https://algomaster.io/learn/lld/encapsulation) |
| Abstraction | [algomaster.io/learn/lld/abstraction](https://algomaster.io/learn/lld/abstraction) |
| Inheritance | [algomaster.io/learn/lld/inheritance](https://algomaster.io/learn/lld/inheritance) |
| Polymorphism | [algomaster.io/learn/lld/polymorphism](https://algomaster.io/learn/lld/polymorphism) |
| Enums | [algomaster.io/learn/lld/enums](https://algomaster.io/learn/lld/enums) |
| 15 Must-Know OOP Concepts | [algomaster.io/learn/lld/15-must-know-oop-concepts](https://algomaster.io/learn/lld/15-must-know-oop-concepts) |

---

## 🔗 Class Relationships Taxonomy

> **Interviewer probe:** *"What's the difference between Composition and Aggregation? When would you use each?"*

Understanding the four relationship types is critical for drawing UML class diagrams and defending your design choices.

### 1. Association ("uses-a" / "knows-a")
Two classes are aware of each other, but neither owns the other. Typically modelled via a method parameter or a short-lived reference.

```csharp
// A Driver uses a Car temporarily — neither owns the other
public class Driver
{
    public void Drive(Car car)  // Association: Car is a parameter, not a field
    {
        car.Accelerate();
    }
}
```
📚 [AlgoMaster: Association](https://algomaster.io/learn/lld/association)

### 2. Aggregation ("has-a" — weak ownership)
One class **has-a** reference to another, but the contained object can **exist independently**. Lifetime is not controlled by the container.

```csharp
// A Department has Employees, but Employees can exist without a Department
public class Department
{
    private readonly List<Employee> _employees;

    public Department(List<Employee> employees)   // Injected externally — not owned
    {
        _employees = employees;
    }
}
```
📚 [AlgoMaster: Aggregation](https://algomaster.io/learn/lld/aggregation)

### 3. Composition ("has-a" — strong ownership / whole-part)
One class **owns** another. The contained object **cannot exist without** the container. Lifetime is controlled by the owner.

```csharp
// An Order owns its OrderItems — if the Order is deleted, all items are deleted
public class Order
{
    // Created inside Order, not injected — tight lifecycle coupling
    private readonly List<OrderItem> _items = new();

    public void AddItem(string product, decimal price, int qty)
        => _items.Add(new OrderItem(product, price, qty));
}
```
📚 [AlgoMaster: Composition](https://algomaster.io/learn/lld/composition)

### 4. Dependency ("depends-on" / "uses transiently")
The weakest relationship. Class A uses Class B only for the duration of a method call — no field reference is stored.

```csharp
public class OrderService
{
    public void ProcessOrder(Order order, IPaymentGateway gateway)
    {
        // gateway is a Dependency — used and discarded within this method
        gateway.Charge(order.TotalAmount);
    }
}
```
📚 [AlgoMaster: Dependency](https://algomaster.io/learn/lld/dependency)

### Relationship Strength Summary

| Relationship | UML Notation | Lifetime Shared? | Example |
|---|---|---|---|
| **Dependency** | Dashed arrow `-->` | No | `OrderService` uses `IPaymentGateway` in method |
| **Association** | Solid arrow `→` | No | `Driver` uses `Car` as a parameter |
| **Aggregation** | Diamond (hollow) `◇→` | No | `Department` has `Employee[]` (injected) |
| **Composition** | Diamond (filled) `◆→` | Yes | `Order` creates its own `OrderItem[]` |

---

## 💡 Design Principles: DRY, YAGNI & KISS

These three principles sit alongside SOLID and are explicitly tested in senior interviews.

### DRY — Don't Repeat Yourself
> *"Every piece of knowledge must have a single, unambiguous, authoritative representation within a system."*

**Violation:** Discount calculation logic duplicated in `OrderService`, `InvoiceService`, and `QuoteService`.
**Fix:** Extract `IDiscountCalculator` service and inject it into all three.

📚 [AlgoMaster: DRY Principle](https://algomaster.io/learn/lld/dry)

### YAGNI — You Aren't Gonna Need It
> *"Implement things when you actually need them, never when you just foresee that you might need them."*

**Violation:** Building a plugin system, multi-currency support, and GraphQL API for a simple internal CRUD tool before any user has asked for them.
**Fix:** Start with the simplest thing that works; refactor only when the requirement materialises.

📚 [AlgoMaster: YAGNI Principle](https://algomaster.io/learn/lld/yagni)

### KISS — Keep It Simple, Stupid
> *"Most systems work best if they are kept simple rather than made complicated."*

**Violation:** Using an Abstract Factory + Builder + Prototype chain to construct a `UserDto` object with 3 fields.
**Fix:** `new UserDto(user.Id, user.Name, user.Email)` — the simplest thing that solves the problem.

📚 [AlgoMaster: KISS Principle](https://algomaster.io/learn/lld/kiss)

### Interview Tip: Applying all three together
*"I'm applying DRY by centralising the fee calculation. I'm applying YAGNI by not building a multi-currency engine yet — the spec only mentions GBP. And I'm applying KISS by using a simple strategy map rather than an Abstract Factory until we have 10+ payment providers."*

---

## 1. Composition over Inheritance

### The Trap: Inheritance Abuse
Inheritance (`class Car : Vehicle`) creates rigid compile-time coupling. If requirements change, class hierarchies explode:
```csharp
// ❌ ANTI-PATTERN: Class Explosion
public class Vehicle { }
public class Car : Vehicle { }
public class ElectricCar : Car { }
public class FlyingCar : Car { }
public class ElectricFlyingCar : ??? // Multiple inheritance is impossible in C#
```

### The Solution: Composition
Model capabilities as composable behaviors:
```csharp
// ✅ PRODUCTION PATTERN: Composable Behaviors
public interface IEngine { void Start(); }
public interface IFlightController { void Fly(); }

public class ElectricEngine : IEngine 
{ 
    public void Start() => Console.WriteLine("Electric motor engaged silently."); 
}

public class JetFlightController : IFlightController 
{ 
    public void Fly() => Console.WriteLine("Jet thrusters deployed."); 
}

public class Vehicle
{
    private readonly IEngine _engine;
    private readonly IFlightController? _flightController;

    public Vehicle(IEngine engine, IFlightController? flightController = null)
    {
        _engine = engine ?? throw new ArgumentNullException(nameof(engine));
        _flightController = flightController;
    }

    public void Drive() => _engine.Start();
    public void TakeOff() => _flightController?.Fly();
}
```

### Senior Interview Takeaway:
* **Inheritance** is strictly for polymorphic substitutability (is-a), never for code reuse.
* If class A needs functionality from class B, inject B into A (has-a / uses-a).

---

## 2. Encapsulation & Rich vs. Anemic Domain Models

### The Anti-Pattern: Anemic Domain Model
All properties have public getters and setters. External services mutate the state arbitrarily, bypassing business invariants:
```csharp
// ❌ ANTI-PATTERN: Anemic Entity (Data Holder Only)
public class Order
{
    public Guid Id { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = "Created";
    public List<OrderItem> Items { get; set; } = new();
}

// Some service miles away:
order.Status = "Shipped"; // Invalid! Shipped before payment, but nothing stopped it.
order.TotalAmount = -50;  // Invalid! Negative total allowed.
```

### The Solution: Rich Domain Model with Invariants
```csharp
// ✅ PRODUCTION PATTERN: Rich Entity Protecting Invariants
public class Order
{
    private readonly List<OrderItem> _items = new();

    public Guid Id { get; private set; }
    public OrderStatus Status { get; private set; }
    public decimal TotalAmount => _items.Sum(i => i.Price * i.Quantity);
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    private Order() { } // Required for EF Core

    public Order(Guid id)
    {
        Id = id;
        Status = OrderStatus.Created;
    }

    public void AddItem(string product, decimal price, int quantity)
    {
        if (Status != OrderStatus.Created)
            throw new InvalidOperationException("Cannot modify items once order is in progress.");
        if (price <= 0)
            throw new ArgumentOutOfRangeException(nameof(price), "Price must be positive.");
        if (quantity <= 0)
            throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero.");

        _items.Add(new OrderItem(product, price, quantity));
    }

    public void MarkPaid()
    {
        if (!_items.Any())
            throw new InvalidOperationException("Cannot pay for an empty order.");
        if (Status != OrderStatus.Created)
            throw new InvalidOperationException($"Cannot pay order in status {Status}.");

        Status = OrderStatus.Paid;
    }
}

public enum OrderStatus { Created, Paid, Shipped, Cancelled }
public record OrderItem(string Product, decimal Price, int Quantity);
```

---

## 3. SOLID Principles: Violations & Refactorings

### S — Single Responsibility Principle (SRP)
> *"A class should have one, and only one, reason to change."*

* **Violation:** `UserService` validates user input, writes to SQL database, generates an activation JWT, and sends a welcome email.
* **Refactoring:** 
  * `UserValidator` (Validation rules)
  * `IUserRepository` (Persistence)
  * `ITokenService` (JWT generation)
  * `IEmailNotificationService` (Email dispatch)

### O — Open/Closed Principle (OCP)
> *"Software entities should be open for extension, but closed for modification."*

* **Violation:**
```csharp
// ❌ Violation: Every new discount type modifies this method
public decimal CalculateDiscount(Order order, string customerTier)
{
    if (customerTier == "Regular") return order.TotalAmount * 0.05m;
    if (customerTier == "Premium") return order.TotalAmount * 0.15m;
    if (customerTier == "VIP") return order.TotalAmount * 0.25m;
    throw new ArgumentException("Unknown tier");
}
```
* **Refactoring:** The **Strategy Pattern**.
```csharp
// ✅ OCP Refactoring: Add new tiers by adding new classes without touching existing ones
public interface IDiscountStrategy
{
    bool IsApplicable(CustomerTier tier);
    decimal Calculate(decimal amount);
}

public class VipDiscountStrategy : IDiscountStrategy
{
    public bool IsApplicable(CustomerTier tier) => tier == CustomerTier.Vip;
    public decimal Calculate(decimal amount) => amount * 0.25m;
}
```

### L — Liskov Substitution Principle (LSP)
> *"Subtypes must be substitutable for their base types without altering program correctness."*

* **Classic Violation:** `Square` inherits from `Rectangle`. Setting `Width` secretly alters `Height`, violating the caller's expectations.
* **Backend Violation:** Derived repository throws `NotSupportedException` on `SaveAsync()` because it is read-only.
* **Refactoring:** Split interfaces into `IReadRepository<T>` and `IWriteRepository<T>`.

### I — Interface Segregation Principle (ISP)
> *"Clients should not be forced to depend on methods they do not use."*

* **Violation:**
```csharp
// ❌ "Fat" Interface
public interface IOrderProcessor
{
    void ValidateOrder();
    void ProcessCreditCardPayment();
    void ProcessCryptoPayment();
    void PrintPhysicalInvoice();
    void DispatchDroneDelivery();
}
```
* **Refactoring:** Segregate into focused interfaces: `IPaymentProcessor`, `IInvoiceGenerator`, `IDeliveryDispatcher`.

### D — Dependency Inversion Principle (DIP)
> *"High-level modules should not depend on low-level modules. Both should depend on abstractions."*

* **Violation:**
```csharp
// ❌ High-level service directly instantiates low-level SQL database
public class PaymentService
{
    private readonly SqlServerGateway _gateway = new SqlServerGateway();
}
```
* **Refactoring:**
```csharp
// ✅ Both depend on abstraction
public class PaymentService
{
    private readonly IPaymentGateway _gateway;
    public PaymentService(IPaymentGateway gateway) => _gateway = gateway;
}
```

---

## 4. Coupling and Cohesion (The Architect's Metrics)

| Metric | Target | Definition |
| :--- | :--- | :--- |
| **Cohesion** | **High** | All responsibilities within a module are closely related to a single, well-defined goal. |
| **Coupling** | **Low** | Modules interact through minimal, stable interfaces, allowing changes in one module without ripple effects in others. |

### Interview Question:
*"How do you know when code is violating low coupling and high cohesion?"*
* **Answer:** "When modifying a database column requires editing 7 files across API, business logic, and UI layers, we have high coupling (shotgun surgery). When a single class handles database queries, HTTP formatting, and PDF generation, we have low cohesion (God object)."