# 08. Movie Ticket Booking (Concurrency + State + TTL)

## 📌 Context
Movie Ticket Booking (like BookMyShow or Ticketmaster) is the ultimate test of **State Management** and **High Concurrency**. Unlike a meeting room where you just block time, here you have thousands of users trying to click the exact same seat ("Seat 10") at the exact same millisecond. 

---

## 1. Domain Modeling: The Seat State Machine
A seat is not just "Free" or "Booked". It has a critical intermediate state: "Locked/Reserved" while the user is entering payment details.

```csharp
public enum SeatStatus { Available, Locked, Booked }

public class Seat
{
    public string Id { get; private set; }
    public SeatStatus Status { get; private set; }
    public string? LockedByUserId { get; private set; }
    public DateTimeOffset? LockExpiry { get; private set; }

    // Concurrency Token for EF Core
    [Timestamp]
    public byte[] RowVersion { get; private set; }

    public Seat(string id)
    {
        Id = id;
        Status = SeatStatus.Available;
    }

    public void Lock(string userId, TimeSpan lockDuration)
    {
        if (Status == SeatStatus.Booked)
            throw new InvalidOperationException("Seat is already booked.");
            
        if (Status == SeatStatus.Locked && LockExpiry > DateTimeOffset.UtcNow)
            throw new InvalidOperationException("Seat is currently locked by another user.");

        Status = SeatStatus.Locked;
        LockedByUserId = userId;
        LockExpiry = DateTimeOffset.UtcNow.Add(lockDuration);
    }

    public void ConfirmBooking(string userId)
    {
        if (Status != SeatStatus.Locked || LockedByUserId != userId)
            throw new InvalidOperationException("Seat is not locked by you.");
            
        if (LockExpiry < DateTimeOffset.UtcNow)
            throw new InvalidOperationException("Lock has expired. Please try booking again.");

        Status = SeatStatus.Booked;
        LockedByUserId = null;
        LockExpiry = null;
    }

    public void ReleaseLock()
    {
        Status = SeatStatus.Available;
        LockedByUserId = null;
        LockExpiry = null;
    }
}
```

---

## 2. Handling the Concurrent Lock Request
When multiple users hit `LockSeatAsync`, EF Core's Optimistic Concurrency handles the race condition.

```csharp
public class BookingService
{
    private readonly ApplicationDbContext _dbContext;

    public BookingService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result> LockSeatAsync(string seatId, string userId, CancellationToken ct)
    {
        var seat = await _dbContext.Seats.SingleOrDefaultAsync(s => s.Id == seatId, ct);
        if (seat == null) return Result.Fail("Seat not found.");

        try
        {
            // The domain entity validates if it can be locked
            seat.Lock(userId, TimeSpan.FromMinutes(10));
            
            // If another thread modified this seat, SaveChangesAsync throws DbUpdateConcurrencyException
            await _dbContext.SaveChangesAsync(ct);
            return Result.Ok();
        }
        catch (DbUpdateConcurrencyException)
        {
            // The race condition was caught! Another user got the seat first.
            return Result.Fail("Sorry, another user just selected this seat.");
        }
    }
}
```

---

## 🗣️ Interviewer Discussion & Tradeoffs

**Interviewer:** *"If the user locks the seat, goes to the payment gateway, and closes their browser, the seat is stuck in `Locked` state forever. How do you fix this?"*
**You:** "The Domain model already sets a `LockExpiry` time. However, to make the seat available for others *immediately* when it expires, we need a background cleanup process. I would implement an `IHostedService` (Background Worker) that runs every 1 minute, queries for `Status == Locked && LockExpiry < UtcNow`, and calls `ReleaseLock()` on them."

**Interviewer:** *"Querying SQL Server every 1 minute for expired seats seems inefficient. Can we do better?"*
**You:** "Yes, we can push the lock state to **Redis** using TTL (Time To Live). When we lock a seat, we do `SET seat:10:lock userId EX 600`. Redis automatically evicts the key after 10 minutes (600 seconds). If the key doesn't exist in Redis, the seat is available. If the user completes payment, we persist the final `Booked` state to SQL Server."

**Interviewer:** *"Why use Optimistic Concurrency (`DbUpdateConcurrencyException`) instead of locking the row (`SELECT FOR UPDATE`)?"*
**You:** "For a high-traffic movie release (e.g., Avengers), hundreds of users might click the same seat. Pessimistic locking forces the database to queue those requests, tying up DB connections and potentially causing deadlocks or timeouts. Optimistic concurrency lets the database process them instantly; one succeeds, and the rest fail fast without locking the table."