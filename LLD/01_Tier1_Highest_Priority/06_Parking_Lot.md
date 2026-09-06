# 01. Parking Lot (OOP, SOLID, Strategy, Factory)

## 📌 Context
The Parking Lot is the quintessential OOP problem. However, for a Senior .NET role, interviewers don't just want a bunch of classes; they want to see **Composition over Inheritance**, the **Strategy Pattern** (for pricing and slot allocation), and **Thread Safety** (when multiple cars enter simultaneously).

---

## 1. Domain Modeling: Composition over Inheritance

Do not use inheritance (e.g., `Car : Vehicle`). Use properties or enums if behavior doesn't change, or use composition if it does.

```csharp
public enum VehicleType { Compact, Large, Electric }
public enum SpotType { Compact, Large, Electric }

public record Vehicle(string LicensePlate, VehicleType Type);

public class ParkingSpot
{
    public string Id { get; }
    public SpotType Type { get; }
    public bool IsOccupied { get; private set; }
    public Vehicle? CurrentVehicle { get; private set; }

    public ParkingSpot(string id, SpotType type)
    {
        Id = id;
        Type = type;
    }

    public void Park(Vehicle vehicle)
    {
        if (IsOccupied) throw new InvalidOperationException("Spot is already occupied.");
        CurrentVehicle = vehicle;
        IsOccupied = true;
    }

    public void Free()
    {
        CurrentVehicle = null;
        IsOccupied = false;
    }
}
```

---

## 2. The Strategy Pattern (Encapsulate what varies)

Allocation algorithms and pricing change constantly. Isolate them.

```csharp
public interface ISpotAllocationStrategy
{
    ParkingSpot? FindSpot(IEnumerable<ParkingSpot> availableSpots, VehicleType vehicleType);
}

// Concrete Strategy 1
public class NearestSpotAllocationStrategy : ISpotAllocationStrategy
{
    public ParkingSpot? FindSpot(IEnumerable<ParkingSpot> availableSpots, VehicleType vehicleType)
    {
        // Simple mapping: Compact can fit in Compact or Large, etc.
        return availableSpots.FirstOrDefault(s => CanFit(s.Type, vehicleType));
    }

    private bool CanFit(SpotType spot, VehicleType vehicle) =>
        (spot, vehicle) switch
        {
            (SpotType.Large, _) => true,
            (SpotType.Compact, VehicleType.Compact) => true,
            (SpotType.Electric, VehicleType.Electric) => true,
            _ => false
        };
}

public interface IPricingStrategy
{
    decimal CalculatePrice(DateTimeOffset entryTime, DateTimeOffset exitTime);
}
```

---

## 3. Concurrency & The Entry Gate

When two cars arrive at the same time, they shouldn't be assigned the same spot. We must use locking mechanisms (e.g., `lock` for in-memory, or DB transactions).

```csharp
public class ParkingLot
{
    private readonly List<ParkingSpot> _spots;
    private readonly ISpotAllocationStrategy _allocationStrategy;
    
    // Concurrency control for in-memory assignment
    private readonly object _allocationLock = new object();

    public ParkingLot(IEnumerable<ParkingSpot> spots, ISpotAllocationStrategy allocationStrategy)
    {
        _spots = spots.ToList();
        _allocationStrategy = allocationStrategy;
    }

    public Ticket? Enter(Vehicle vehicle)
    {
        // CRITICAL SECTION: Prevent two vehicles getting the same spot
        lock (_allocationLock)
        {
            var availableSpots = _spots.Where(s => !s.IsOccupied);
            var spot = _allocationStrategy.FindSpot(availableSpots, vehicle.Type);

            if (spot == null)
            {
                return null; // Lot is full for this vehicle type
            }

            spot.Park(vehicle);
            return new Ticket(Guid.NewGuid(), vehicle.LicensePlate, spot.Id, DateTimeOffset.UtcNow);
        }
    }

    public void Exit(Ticket ticket)
    {
        lock (_allocationLock)
        {
            var spot = _spots.FirstOrDefault(s => s.Id == ticket.SpotId);
            if (spot == null || !spot.IsOccupied)
                throw new InvalidOperationException("Invalid ticket or spot already free.");

            spot.Free();
            // Pricing calculation would happen here using IPricingStrategy
        }
    }
}

public record Ticket(Guid Id, string LicensePlate, string SpotId, DateTimeOffset EntryTime);
```
---

## 🗣️ Interviewer Discussion & Tradeoffs

**Interviewer:** *"Why didn't you use inheritance, like `class Car : Vehicle` and `class Motorcycle : Vehicle`?"*
**You:** "Because of the **Composition over Inheritance** principle. If we use inheritance, what happens when we introduce an Electric Car? Do we do `class ElectricCar : Car`? What if we have an Electric Motorcycle? The hierarchy explodes. By using `VehicleType` (and potentially composing behavior), we keep the design flat and flexible."

**Interviewer:** *"Why abstract the spot allocation into `ISpotAllocationStrategy`?"*
**You:** "Because the allocation algorithm changes frequently. Today we want the `NearestSpotStrategy`. Tomorrow, management might ask for `FirstAvailableStrategy` or `VIPSpotStrategy`. By using the **Strategy Pattern**, we satisfy the **Open/Closed Principle (OCP)**. We can add new allocation behaviors without modifying the core `ParkingLot` logic."

**Interviewer:** *"How does your lock handle concurrent entries across multiple entrance gates?"*
**You:** "The `lock (_allocationLock)` ensures that only one thread can assign a spot at a time. If Gate A and Gate B both call `Enter()` simultaneously, one will block until the other is assigned a spot. This works perfectly if the system runs on a single server. However, if we scale the ParkingLot software to multiple pods/servers, this memory lock fails. We would need to push the concurrency control to the database (using `RowVersion` or `SELECT FOR UPDATE`)."
