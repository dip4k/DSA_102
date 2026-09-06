# 12. Vending Machine (The State Pattern)

## 📌 Context
The Vending Machine is the definitive test of the **State Pattern**. Junior developers will solve this using a massive `switch` statement (`if status == idle`, `else if status == dispensing`). Senior developers encapsulate the states into discrete classes.

---

## 1. The State Interface
Instead of checking statuses, we delegate actions to the current State object.

```csharp
public interface IVendingMachineState
{
    void InsertCoin(int amount);
    void SelectProduct(string productId);
    void DispenseProduct();
    void CancelAndRefund();
}
```

---

## 2. The Context (The Machine)
The machine maintains the current state and its inventory/balance.

```csharp
public class VendingMachine
{
    // The current State
    public IVendingMachineState CurrentState { get; private set; }

    // Internal data
    public int Balance { get; set; }
    public Dictionary<string, int> Inventory { get; } = new();
    public string? SelectedProduct { get; set; }

    // Pre-instantiated states to avoid memory allocations
    public IVendingMachineState IdleState { get; }
    public IVendingMachineState HasMoneyState { get; }
    public IVendingMachineState DispensingState { get; }

    public VendingMachine()
    {
        IdleState = new IdleState(this);
        HasMoneyState = new HasMoneyState(this);
        DispensingState = new DispensingState(this);
        
        CurrentState = IdleState;
    }

    public void SetState(IVendingMachineState state)
    {
        CurrentState = state;
    }

    // Delegate all actions to the current state!
    public void InsertCoin(int amount) => CurrentState.InsertCoin(amount);
    public void SelectProduct(string productId) => CurrentState.SelectProduct(productId);
    public void DispenseProduct() => CurrentState.DispenseProduct();
    public void CancelAndRefund() => CurrentState.CancelAndRefund();
}
```

---

## 3. Concrete States
Each state explicitly defines what is allowed and what is forbidden.

```csharp
public class IdleState : IVendingMachineState
{
    private readonly VendingMachine _machine;

    public IdleState(VendingMachine machine)
    {
        _machine = machine;
    }

    public void InsertCoin(int amount)
    {
        Console.WriteLine($"Inserted {amount} cents.");
        _machine.Balance += amount;
        
        // Transition to next state!
        _machine.SetState(_machine.HasMoneyState);
    }

    public void SelectProduct(string productId) => throw new InvalidOperationException("Insert money first.");
    public void DispenseProduct() => throw new InvalidOperationException("No product selected.");
    public void CancelAndRefund() => Console.WriteLine("Nothing to refund.");
}

public class HasMoneyState : IVendingMachineState
{
    private readonly VendingMachine _machine;

    public HasMoneyState(VendingMachine machine)
    {
        _machine = machine;
    }

    public void InsertCoin(int amount)
    {
        Console.WriteLine($"Inserted {amount} cents. Total: {_machine.Balance + amount}");
        _machine.Balance += amount;
    }

    public void SelectProduct(string productId)
    {
        if (!_machine.Inventory.ContainsKey(productId) || _machine.Inventory[productId] == 0)
        {
            Console.WriteLine("Out of stock.");
            return;
        }

        _machine.SelectedProduct = productId;
        _machine.SetState(_machine.DispensingState);
    }

    public void DispenseProduct() => throw new InvalidOperationException("Select a product first.");

    public void CancelAndRefund()
    {
        Console.WriteLine($"Refunding {_machine.Balance} cents.");
        _machine.Balance = 0;
        _machine.SetState(_machine.IdleState);
    }
}
```

---

## 🗣️ Interviewer Discussion & Tradeoffs

**Interviewer:** *"Why is this State pattern better than just writing `if (state == Idle)` inside the `VendingMachine` methods?"*
**You:** "The Single Responsibility Principle (SRP) and Open/Closed Principle (OCP). An `if/else` block becomes unmaintainable as we add states. If I need to add an `OutOfOrderState`, I would have to modify 4 different methods in the core class. With the State Pattern, I just create a new `OutOfOrderState` class. The rules for what happens when money is inserted during the 'Idle' state live exclusively in the `IdleState` class."

**Interviewer:** *"Who should be responsible for transitioning the state? The `VendingMachine` or the `State` classes?"*
**You:** "There are two schools of thought. Here, I let the `State` classes orchestrate the transition (`_machine.SetState()`). This is great for workflows where one state naturally leads to the next. The alternative is a central State Machine orchestrator (like the `Stateless` library in C#), where states are dumb and transitions are configured externally. I prefer letting the States manage it for localized problems like a Vending Machine."