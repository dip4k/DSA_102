# 01. Meeting Room Booking System (C# Senior Focus)

## 📌 Context
This problem tests your ability to model a domain, handle dates/times correctly, and most critically, **prevent double-booking under concurrent load**. It moves beyond classic OOP into practical backend architecture using EF Core and ASP.NET Core concepts.

---

## 1. Core Domain Models
Use encapsulation to protect the invariants (e.g., end time must be after start time).

```csharp
public class Room
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public int Capacity { get; private set; }
    
    // Concurrency token for EF Core
    [Timestamp]
    public byte[] RowVersion { get; private set; }

    public Room(Guid id, string name, int capacity)
    {
        Id = id;
        Name = name;
        Capacity = capacity;
    }
}

public class Booking
{
    public Guid Id { get; private set; }
    public Guid RoomId { get; private set; }
    public string UserId { get; private set; }
    public DateTimeOffset StartTime { get; private set; }
    public DateTimeOffset EndTime { get; private set; }
    public BookingStatus Status { get; private set; }

    public Booking(Guid roomId, string userId, DateTimeOffset start, DateTimeOffset end)
    {
        if (end <= start) throw new ArgumentException("End time must be after start time.");
        
        Id = Guid.NewGuid();
        RoomId = roomId;
        UserId = userId;
        StartTime = start;
        EndTime = end;
        Status = BookingStatus.Active;
    }

    public void Cancel()
    {
        if (StartTime <= DateTimeOffset.UtcNow)
            throw new InvalidOperationException("Cannot cancel past or ongoing meetings.");
        Status = BookingStatus.Cancelled;
    }
}

public enum BookingStatus { Active, Cancelled }
```

---

## 2. Service Interface & DTOs
Define clear inputs and outputs. Do not leak database entities out of your service.

```csharp
public interface IBookingService
{
    Task<Result<Guid>> BookRoomAsync(BookRoomRequest request, CancellationToken cancellationToken);
    Task<Result> CancelBookingAsync(Guid bookingId, string userId, CancellationToken cancellationToken);
    Task<IEnumerable<RoomDto>> GetAvailableRoomsAsync(DateTimeOffset start, DateTimeOffset end, CancellationToken cancellationToken);
}

public record BookRoomRequest(Guid RoomId, string UserId, DateTimeOffset StartTime, DateTimeOffset EndTime);
```

---

## 3. The Concurrency Problem (The "Meat" of the Interview)

**Scenario:** Two users attempt to book Room A for 10:00 - 11:00 simultaneously. 
**Solution in C# (EF Core Focus):**

You must prevent double booking. You can achieve this via:
1. **Pessimistic Locking:** SELECT ... FOR UPDATE (Native SQL / EF Core raw queries).
2. **Optimistic Concurrency Control (OCC):** Modifying the RowVersion on the Room entity, or inserting with a UNIQUE constraint.
3. **Application Level Locking:** A distributed lock (e.g., Redis RedLock) around the RoomId.

### Implementation using Distributed Lock / Semaphore (In-Memory fallback)

```csharp
public class BookingService : IBookingService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<BookingService> _logger;
    // For single-instance only; use Redis/Distributed lock for microservices.
    private static readonly ConcurrentDictionary<Guid, SemaphoreSlim> _locks = new();

    public BookingService(ApplicationDbContext dbContext, ILogger<BookingService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Result<Guid>> BookRoomAsync(BookRoomRequest request, CancellationToken ct)
    {
        var roomLock = _locks.GetOrAdd(request.RoomId, _ => new SemaphoreSlim(1, 1));
        
        await roomLock.WaitAsync(ct);
        try
        {
            // 1. Validate Overlaps in DB
            bool isOverlapping = await _dbContext.Bookings
                .AnyAsync(b => b.RoomId == request.RoomId && 
                               b.Status == BookingStatus.Active &&
                               b.StartTime < request.EndTime && 
                               b.EndTime > request.StartTime, ct);

            if (isOverlapping)
            {
                _logger.LogWarning("Room {RoomId} is already booked for this slot.", request.RoomId);
                return Result.Fail("Room is unavailable.");
            }

            // 2. Create and Save
            var booking = new Booking(request.RoomId, request.UserId, request.StartTime, request.EndTime);
            _dbContext.Bookings.Add(booking);
            await _dbContext.SaveChangesAsync(ct);

            return Result.Ok(booking.Id);
        }
        finally
        {
            roomLock.Release();
        }
    }
}
```

### Advanced Extension: Database-Level Constraint (PostgreSQL example)
If the interviewer pushes for a DB-only solution, mention Exclusion Constraints:
```sql
ALTER TABLE "Bookings" ADD CONSTRAINT "exclude_overlapping_bookings" EXCLUDE USING gist 
(
    "RoomId" WITH =, 
    tsrange("StartTime", "EndTime") WITH &&
) WHERE ("Status" = 0);
```
In EF Core, catching DbUpdateException when this constraint is violated handles the concurrency natively without app-level locks!

---

## 🗣️ Interviewer Discussion & Tradeoffs

**Interviewer:** *"Why did you choose `SemaphoreSlim` here instead of just using a standard `lock` block?"*
**You:** "A standard `lock` in C# blocks the thread synchronously. Since our `BookRoomAsync` method is `async` and does I/O (database calls), we cannot use `lock` because it doesn't support `await` and leads to thread-pool starvation. `SemaphoreSlim(1,1)` allows asynchronous waiting, freeing up the thread while it waits for the lock."

**Interviewer:** *"If we deploy this service to Kubernetes with 5 replicas, will your `SemaphoreSlim` prevent double bookings?"*
**You:** "No, `SemaphoreSlim` is in-memory and only protects concurrent requests within the *same* pod/instance. For a distributed environment, we must rely on either:
1. **Database-level constraints:** Like PostgreSQL Exclusion Constraints or EF Core Optimistic Concurrency Tokens (`RowVersion`). 
2. **Distributed Locking:** Using Redis (RedLock) to acquire a lock on the `RoomId` before querying."

**Interviewer:** *"Which is better: Optimistic (RowVersion) or Pessimistic (Redis Lock) concurrency for this?"*
**You:** "It depends on the contention rate. If booking conflicts are rare, **Optimistic Concurrency** is better because it avoids the overhead of acquiring locks; the second user simply gets a `DbUpdateConcurrencyException` when saving. If conflicts are very high (e.g., selling tickets to a Taylor Swift concert), **Pessimistic Locking** (Redis) is better to prevent the database from doing wasted work."
