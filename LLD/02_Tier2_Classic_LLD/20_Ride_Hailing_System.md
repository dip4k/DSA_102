# 20. Ride-Hailing System (Uber / Ola / Lyft)

## 📌 Context
Designing a Ride-Hailing system is one of the most popular LLD and Machine Coding interview questions (**Uber, Grab, Lyft, Swiggy, Flipkart**). It tests:
1. **The Strategy Pattern:** Driver matching algorithms (nearest vs highest-rated) and dynamic surge pricing.
2. **The State Pattern:** Enforcing strict trip lifecycle transitions (`Requested` -> `Accepted` -> `Arrived` -> `Started` -> `Completed`).
3. **Concurrency:** Preventing race conditions when multiple drivers attempt to accept the same trip simultaneously.

---

## 1. Domain Models & Value Objects

```csharp
public enum TripStatus { Requested, Accepted, DriverArrived, InProgress, Completed, Cancelled }
public enum VehicleType { Sedan, SUV, Bike }

public readonly record struct Location(double Latitude, double Longitude)
{
    public double DistanceTo(Location other)
    {
        // Euclidean approximation for LLD; In production, use Haversine formula
        var dLat = Latitude - other.Latitude;
        var dLon = Longitude - other.Longitude;
        return Math.Sqrt(dLat * dLat + dLon * dLon) * 111.0; // ~111 km per degree
    }
}

public class Driver
{
    public string Id { get; }
    public string Name { get; }
    public double Rating { get; }
    public VehicleType VehicleType { get; }
    public Location CurrentLocation { get; set; }
    public bool IsAvailable { get; set; } = true;

    public Driver(string id, string name, double rating, VehicleType vehicleType, Location location)
    {
        Id = id;
        Name = name;
        Rating = rating;
        VehicleType = vehicleType;
        CurrentLocation = location;
    }
}

public class Rider
{
    public string Id { get; }
    public string Name { get; }
    public Location CurrentLocation { get; set; }

    public Rider(string id, string name, Location location)
    {
        Id = id;
        Name = name;
        CurrentLocation = location;
    }
}
```

---

## 2. Strategies: Matching & Dynamic Surge Pricing

```csharp
// Strategy 1: Matching
public interface IDriverMatchingStrategy
{
    Driver? FindDriver(Rider rider, IEnumerable<Driver> availableDrivers, VehicleType vehicleType);
}

public class NearestDriverMatchingStrategy : IDriverMatchingStrategy
{
    public Driver? FindDriver(Rider rider, IEnumerable<Driver> availableDrivers, VehicleType vehicleType)
    {
        return availableDrivers
            .Where(d => d.IsAvailable && d.VehicleType == vehicleType)
            .OrderBy(d => d.CurrentLocation.DistanceTo(rider.CurrentLocation))
            .FirstOrDefault();
    }
}

// Strategy 2: Pricing
public interface IPricingStrategy
{
    decimal CalculateFare(Location pickup, Location dropoff, double surgeMultiplier);
}

public class StandardPricingStrategy : IPricingStrategy
{
    private const decimal BaseFare = 2.50m;
    private const decimal RatePerKm = 1.25m;

    public decimal CalculateFare(Location pickup, Location dropoff, double surgeMultiplier)
    {
        var distanceKm = (decimal)pickup.DistanceTo(dropoff);
        var fare = (BaseFare + (distanceKm * RatePerKm)) * (decimal)surgeMultiplier;
        return Math.Round(fare, 2);
    }
}
```

---

## 3. The Trip Lifecycle State Machine

```csharp
public class Trip
{
    public Guid Id { get; }
    public Rider Rider { get; }
    public Driver? Driver { get; private set; }
    public Location Pickup { get; }
    public Location Dropoff { get; }
    public decimal EstimatedFare { get; }
    public TripStatus Status { get; private set; }

    // Synchronizes concurrent driver acceptance
    private readonly object _stateLock = new();

    public Trip(Rider rider, Location pickup, Location dropoff, decimal estimatedFare)
    {
        Id = Guid.NewGuid();
        Rider = rider;
        Pickup = pickup;
        Dropoff = dropoff;
        EstimatedFare = estimatedFare;
        Status = TripStatus.Requested;
    }

    public bool TryAssignDriver(Driver driver)
    {
        lock (_stateLock)
        {
            if (Status != TripStatus.Requested)
                return false; // Another driver already accepted or trip was cancelled!

            Driver = driver;
            driver.IsAvailable = false;
            Status = TripStatus.Accepted;
            return true;
        }
    }

    public void NotifyDriverArrived()
    {
        lock (_stateLock)
        {
            if (Status != TripStatus.Accepted)
                throw new InvalidOperationException($"Cannot mark arrived in status {Status}.");
            Status = TripStatus.DriverArrived;
        }
    }

    public void StartTrip()
    {
        lock (_stateLock)
        {
            if (Status != TripStatus.DriverArrived)
                throw new InvalidOperationException($"Cannot start trip from status {Status}.");
            Status = TripStatus.InProgress;
        }
    }

    public void CompleteTrip()
    {
        lock (_stateLock)
        {
            if (Status != TripStatus.InProgress)
                throw new InvalidOperationException($"Cannot complete trip from status {Status}.");
            Status = TripStatus.Completed;
            if (Driver != null) Driver.IsAvailable = true;
        }
    }

    public void Cancel()
    {
        lock (_stateLock)
        {
            if (Status == TripStatus.InProgress || Status == TripStatus.Completed)
                throw new InvalidOperationException("Cannot cancel an active or completed trip.");

            Status = TripStatus.Cancelled;
            if (Driver != null) Driver.IsAvailable = true;
        }
    }
}
```

---

## 4. Ride Hailing Orchestrator Service

```csharp
public class RideHailingService
{
    private readonly List<Driver> _drivers = new();
    private readonly List<Trip> _trips = new();
    private readonly IDriverMatchingStrategy _matchingStrategy;
    private readonly IPricingStrategy _pricingStrategy;

    public RideHailingService(IDriverMatchingStrategy matchingStrategy, IPricingStrategy pricingStrategy)
    {
        _matchingStrategy = matchingStrategy;
        _pricingStrategy = pricingStrategy;
    }

    public void RegisterDriver(Driver driver) => _drivers.Add(driver);

    public Trip? RequestRide(Rider rider, Location dropoff, VehicleType vehicleType, double surge = 1.0)
    {
        var fare = _pricingStrategy.CalculateFare(rider.CurrentLocation, dropoff, surge);
        var trip = new Trip(rider, rider.CurrentLocation, dropoff, fare);

        // Match available driver
        var matchedDriver = _matchingStrategy.FindDriver(rider, _drivers, vehicleType);
        if (matchedDriver == null)
        {
            Console.WriteLine("No available drivers nearby.");
            return null;
        }

        // Attempt assignment
        if (trip.TryAssignDriver(matchedDriver))
        {
            _trips.Add(trip);
            Console.WriteLine($"Trip {trip.Id} assigned to {matchedDriver.Name}. Fare: ${fare}");
            return trip;
        }

        return null;
    }
}
```

---

## 🗣️ Interviewer Discussion & Tradeoffs

**Interviewer:** *"If 100,000 drivers are broadcasting their GPS location every 3 seconds, how do you find the nearest driver efficiently in production?"*
**You:** "Scanning an in-memory list takes O(N) linear time, which is too slow at scale. In production:
1. We index geospatial locations using **Uber H3 (Hexagonal Hierarchical Spatial Index)** or **Google S2 (Hilbert curve space-filling)**.
2. Drivers are partitioned into spatial buckets (hexagons) in **Redis GEO** or an in-memory spatial index.
3. Finding nearby drivers becomes an O(1) or O(K) lookup by checking the rider's hexagon and immediate 6 neighbor cells."

**Interviewer:** *"What happens if a driver is offered a trip, but doesn't accept within 15 seconds?"*
**You:** "We implement a **Driver Offer State Machine** with a countdown timer. When the timer expires without acceptance, the trip transitions back to `Requested`, the driver is placed on temporary cooldown, and the `IDriverMatchingStrategy` selects the next best candidate."

**Interviewer:** *"Why use `lock (_stateLock)` inside `TryAssignDriver` instead of relying on the caller?"*
**You:** "Because of the race condition where Driver A and Driver B both hit 'Accept' at the exact same millisecond. If the lock is not encapsulated inside the `Trip` entity, both calls could evaluate `Status == Requested` as true, assigning two drivers to the same ride. The entity must protect its own invariants atomically."