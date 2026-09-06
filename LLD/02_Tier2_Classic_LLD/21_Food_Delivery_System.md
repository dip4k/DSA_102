# 21. Food Delivery System (Swiggy / Zomato / DoorDash)

## 📌 Context
Food delivery systems test your ability to coordinate multiple independent domains: **Restaurants & Menus**, **User Carts**, **Discount/Coupon Engines**, and **Order Fulfillment with Delivery Partners**.

---

## 1. Domain Modeling: Cart, Restaurant, and Menu Items

A core business rule of food delivery: **A user cart can only contain items from ONE restaurant at a time.**

```csharp
public record MenuItem(string Id, string Name, decimal Price, bool IsAvailable);

public class CartItem
{
    public MenuItem Item { get; }
    public int Quantity { get; private set; }

    public CartItem(MenuItem item, int quantity)
    {
        if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));
        Item = item;
        Quantity = quantity;
    }

    public void UpdateQuantity(int delta)
    {
        if (Quantity + delta <= 0) throw new InvalidOperationException("Quantity cannot drop to zero or negative.");
        Quantity += delta;
    }
}

public class Cart
{
    public string UserId { get; }
    public string? RestaurantId { get; private set; }
    private readonly List<CartItem> _items = new();

    public IReadOnlyCollection<CartItem> Items => _items.AsReadOnly();
    public decimal Subtotal => _items.Sum(i => i.Item.Price * i.Quantity);

    public Cart(string userId) => UserId = userId;

    public void AddItem(string restaurantId, MenuItem item, int quantity)
    {
        if (!item.IsAvailable)
            throw new InvalidOperationException($"Item '{item.Name}' is currently out of stock.");

        // Rule: Single restaurant per cart
        if (RestaurantId != null && RestaurantId != restaurantId)
        {
            throw new InvalidOperationException("Your cart contains items from a different restaurant. Clear cart to switch.");
        }

        RestaurantId = restaurantId;

        var existing = _items.FirstOrDefault(i => i.Item.Id == item.Id);
        if (existing != null)
        {
            existing.UpdateQuantity(quantity);
        }
        else
        {
            _items.Add(new CartItem(item, quantity));
        }
    }

    public void Clear()
    {
        _items.Clear();
        RestaurantId = null;
    }
}
```

---

## 2. The Strategy Pattern: Discount & Coupon Engine

```csharp
public interface ICouponStrategy
{
    string Code { get; }
    bool IsValid(Cart cart);
    decimal CalculateDiscount(Cart cart);
}

public class FlatDiscountCoupon : ICouponStrategy
{
    public string Code { get; }
    private readonly decimal _discountAmount;
    private readonly decimal _minOrderAmount;

    public FlatDiscountCoupon(string code, decimal discountAmount, decimal minOrderAmount)
    {
        Code = code;
        _discountAmount = discountAmount;
        _minOrderAmount = minOrderAmount;
    }

    public bool IsValid(Cart cart) => cart.Subtotal >= _minOrderAmount;

    public decimal CalculateDiscount(Cart cart) =>
        IsValid(cart) ? Math.Min(_discountAmount, cart.Subtotal) : 0m;
}

public class PercentageDiscountCoupon : ICouponStrategy
{
    public string Code { get; }
    private readonly decimal _percentage;
    private readonly decimal _maxCap;

    public PercentageDiscountCoupon(string code, decimal percentage, decimal maxCap)
    {
        Code = code;
        _percentage = percentage;
        _maxCap = maxCap;
    }

    public bool IsValid(Cart cart) => cart.Subtotal > 0;

    public decimal CalculateDiscount(Cart cart)
    {
        var rawDiscount = cart.Subtotal * (_percentage / 100m);
        return Math.Min(rawDiscount, _maxCap);
    }
}
```

---

## 3. Order Lifecycle State Machine & Observer Pattern

```csharp
public enum OrderState { Placed, AcceptedByRestaurant, FoodPrepared, OutForDelivery, Delivered, Cancelled }

public interface IOrderObserver
{
    Task OnOrderStatusChangedAsync(Guid orderId, OrderState newState);
}

public class CustomerNotificationObserver : IOrderObserver
{
    public Task OnOrderStatusChangedAsync(Guid orderId, OrderState newState)
    {
        Console.WriteLine($"[Push Notification to Customer]: Order {orderId} is now {newState}.");
        return Task.CompletedTask;
    }
}

public class DeliveryPartner
{
    public string Id { get; }
    public string Name { get; }
    public bool IsAvailable { get; set; } = true;

    public DeliveryPartner(string id, string name)
    {
        Id = id;
        Name = name;
    }
}

public class FoodOrder
{
    public Guid Id { get; }
    public string UserId { get; }
    public string RestaurantId { get; }
    public List<CartItem> Items { get; }
    public decimal TotalAmount { get; }
    public OrderState State { get; private set; }
    public DeliveryPartner? AssignedPartner { get; private set; }

    private readonly List<IOrderObserver> _observers = new();

    public FoodOrder(string userId, string restaurantId, List<CartItem> items, decimal totalAmount)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        RestaurantId = restaurantId;
        Items = items;
        TotalAmount = totalAmount;
        State = OrderState.Placed;
    }

    public void AttachObserver(IOrderObserver observer) => _observers.Add(observer);

    public async Task TransitionStateAsync(OrderState nextState)
    {
        // Enforce valid sequential state transitions
        bool isValid = (State, nextState) switch
        {
            (OrderState.Placed, OrderState.AcceptedByRestaurant) => true,
            (OrderState.Placed, OrderState.Cancelled) => true,
            (OrderState.AcceptedByRestaurant, OrderState.FoodPrepared) => true,
            (OrderState.FoodPrepared, OrderState.OutForDelivery) => true,
            (OrderState.OutForDelivery, OrderState.Delivered) => true,
            _ => false
        };

        if (!isValid)
            throw new InvalidOperationException($"Invalid transition from {State} to {nextState}.");

        State = nextState;
        
        // Notify all subscribers
        foreach (var observer in _observers)
        {
            await observer.OnOrderStatusChangedAsync(Id, State);
        }
    }

    public void AssignDeliveryPartner(DeliveryPartner partner)
    {
        AssignedPartner = partner;
        partner.IsAvailable = false;
    }
}
```

---

## 🗣️ Interviewer Discussion & Tradeoffs

**Interviewer:** *"What happens if an item's price changes or it goes out of stock between when the user adds it to their cart and when they click Checkout?"*
**You:** "This is the **Cart Price Drift Problem**. In production:
1. The Cart is merely a draft intent, not a reservation.
2. During the `Checkout()` API call, the server performs a fresh read of the restaurant's live menu from the database.
3. If prices drifted or an item went out of stock, the server rejects checkout with an HTTP 409 Conflict, returning a delta summary: 'Item X price changed from USD 10 to USD 12. Accept to proceed.'"

**Interviewer:** *"Why use the Observer pattern for order tracking instead of polling the database from the mobile app?"*
**You:** "Polling creates unnecessary database read spikes (millions of users querying `/orders/status` every 2 seconds). The Observer pattern decouples the state update. In production, this maps to **WebSockets / SignalR** or **Server-Sent Events (SSE)**. When the delivery driver's GPS updates or the restaurant clicks 'Ready', an event is pushed over an established WebSocket directly to the customer's phone."

**Interviewer:** *"How do you handle assigning delivery partners under heavy rain / high load?"*
**You:** "We use a **Batch Assignment (Bipartite Matching)** algorithm rather than greedy first-come-first-served. Every 30 seconds, a background engine matches a batch of ready orders with nearby available delivery partners using the **Hungarian Algorithm** to minimize total delivery delay."