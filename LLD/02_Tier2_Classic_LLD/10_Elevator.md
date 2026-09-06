# 10. Elevator System (State + Strategy + Concurrency)

## 📌 Context
The Elevator problem tests your ability to combine the **State Pattern** (Idle, Moving, Broken) with the **Strategy Pattern** (Scheduling: SCAN, First Come First Serve) and handle **Concurrency** (users pressing buttons while the elevator is moving).

---

## 1. Domain Modeling: Enums and Core Classes

```csharp
public enum Direction { Up, Down, Idle }
public enum ElevatorState { Working, Maintenance }

public record Request(int Floor, Direction Direction);

public class ElevatorCar
{
    public int Id { get; }
    public int CurrentFloor { get; private set; }
    public Direction CurrentDirection { get; private set; }
    public ElevatorState State { get; private set; }

    // Sorted sets or priority queues are often used for scheduling algorithms like SCAN
    private readonly SortedSet<int> _upRequests = new();
    private readonly SortedSet<int> _downRequests = new();
    
    // Lock for thread-safety when requests are added asynchronously
    private readonly object _lock = new object();

    public ElevatorCar(int id)
    {
        Id = id;
        CurrentFloor = 0; // Ground floor
        CurrentDirection = Direction.Idle;
        State = ElevatorState.Working;
    }

    public void AddRequest(int floor)
    {
        lock (_lock)
        {
            if (floor > CurrentFloor)
                _upRequests.Add(floor);
            else if (floor < CurrentFloor)
                _downRequests.Add(floor);
        }
    }

    public void Move()
    {
        // Simplistic representation of the SCAN (Look) algorithm
        lock (_lock)
        {
            if (CurrentDirection == Direction.Up || CurrentDirection == Direction.Idle)
            {
                if (_upRequests.Any())
                {
                    CurrentFloor = _upRequests.Min;
                    _upRequests.Remove(CurrentFloor);
                    Console.WriteLine($"Elevator {Id} stopped at floor {CurrentFloor}");
                    CurrentDirection = _upRequests.Any() ? Direction.Up : Direction.Idle;
                    return;
                }
                // If no up requests, switch direction
                CurrentDirection = Direction.Down;
            }

            if (CurrentDirection == Direction.Down)
            {
                if (_downRequests.Any())
                {
                    CurrentFloor = _downRequests.Max;
                    _downRequests.Remove(CurrentFloor);
                    Console.WriteLine($"Elevator {Id} stopped at floor {CurrentFloor}");
                    CurrentDirection = _downRequests.Any() ? Direction.Down : Direction.Idle;
                }
            }
        }
    }
}
```

---

## 2. The Strategy Pattern (Dispatching)
When a user presses a hall button, which elevator should answer?

```csharp
public interface IElevatorDispatchStrategy
{
    ElevatorCar? SelectElevator(IEnumerable<ElevatorCar> elevators, Request request);
}

public class NearestElevatorStrategy : IElevatorDispatchStrategy
{
    public ElevatorCar? SelectElevator(IEnumerable<ElevatorCar> elevators, Request request)
    {
        return elevators
            .Where(e => e.State == ElevatorState.Working)
            .OrderBy(e => Math.Abs(e.CurrentFloor - request.Floor))
            .FirstOrDefault();
    }
}
```

---

## 🗣️ Interviewer Discussion & Tradeoffs

**Interviewer:** *"Why use the SCAN (Look) algorithm instead of First-Come, First-Serve (FCFS)?"*
**You:** "FCFS is terribly inefficient for elevators. If user A presses 10, then user B presses 2, then user C presses 9, the elevator zig-zags (0 -> 10 -> 2 -> 9), wasting energy and time. The **SCAN (Look)** algorithm travels in one direction until there are no more requests in that direction, then reverses. It's the same algorithm hard drives use for disk scheduling, and it drastically improves throughput."

**Interviewer:** *"How does your lock handle an internal button press vs an external hall button press?"*
**You:** "From the `ElevatorCar`'s perspective, a destination floor is a destination floor. Both internal buttons and external dispatchers ultimately call `AddRequest(floor)`. The `lock (_lock)` ensures that while the elevator is processing `Move()` and altering its target tracking sets, another thread (representing a new button press) cannot corrupt the `SortedSet`."

**Interviewer:** *"If we scale this to a skyscraper with 100 floors and 15 elevators, how do you optimize dispatching?"*
**You:** "I would change the `IElevatorDispatchStrategy` to a **Zoned Strategy** or **Destination Dispatch**. Zoned assigns elevators 1-5 to floors 1-30, etc. Destination Dispatch requires the user to enter their floor *before* getting in the elevator, allowing the system to group users going to the same floor into the exact same elevator car, minimizing stops."