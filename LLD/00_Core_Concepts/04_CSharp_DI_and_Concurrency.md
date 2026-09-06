# Layer 5 & 6: C# Dependency Injection & Concurrency (Senior Deep Dive)

Senior .NET interviewers frequently scrutinize your understanding of the **ASP.NET Core Dependency Injection lifecycle** and **multi-threaded asynchronous programming**.

---

## 1. The Captive Dependency Trap (DI Lifetimes)

In .NET DI, there are 3 service lifetimes:
* **Transient:** New instance created every time it is requested.
* **Scoped:** One instance created per HTTP request (or per `IServiceScope`).
* **Singleton:** One instance created for the entire lifetime of the process.

### The Problem: Captive Dependency
When a service with a longer lifetime holds a dependency with a shorter lifetime (e.g., a **Singleton** consumes a **Scoped** `DbContext`).

```csharp
// ❌ CRITICAL BUG: DbContext is trapped inside a Singleton!
public class QueueWorker : BackgroundService
{
    private readonly ApplicationDbContext _dbContext; // Captive Dependency!

    public QueueWorker(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext; // DbContext never disposes, caches forever, and leaks memory!
    }
}
```

### The Senior Fix: `IServiceScopeFactory`
```csharp
// ✅ PRODUCTION PATTERN: Create scope per execution cycle
public class QueueWorker : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public QueueWorker(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // Explicit scope ensures scoped services are properly instantiated and disposed
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                await dbContext.ProcessPendingOrdersAsync(stoppingToken);
            }

            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }
    }
}
```

---

## 2. Synchronization Primitives Comparison

| Primitive | Async Friendly? | Primary Use Case | Performance / Overhead |
| :--- | :---: | :--- | :--- |
| **`lock (obj)`** | ❌ **No** | CPU-bound synchronous code blocks. | Ultra-low (fastest in-memory lock). |
| **`SemaphoreSlim`** | ✅ **Yes** (`WaitAsync`) | Asynchronous I/O synchronization or limiting concurrency (e.g. max 5 parallel downloads). | Low overhead, supports cancellation tokens. |
| **`ReaderWriterLockSlim`** | ❌ **No** | Read-heavy in-memory data structures (unlimited readers, 1 exclusive writer). | Moderate. High throughput for 90% read workloads. |
| **`Interlocked`** | ✅ **Yes** | Atomic operations on primitives (`Increment`, `Add`, `CompareExchange`). | Hardware-level atomic instructions (zero thread sleep). |
| **`Channel<T>`** | ✅ **Yes** | High-performance async Producer-Consumer pipelines. | Lock-free, allocation-efficient bounded queues. |

---

## 3. The Golden Rule of Async/Await & Locking

> **NEVER use `lock(obj)` in an async method that awaits I/O.**

```csharp
// ❌ THREAD POOL STARVATION: Synchronous lock around async call
lock (_gate)
{
    // Compiler error in modern C#, or deadlock if using Monitor.Enter!
    await _dbContext.SaveChangesAsync(); 
}

// ✅ ASYNC SYNCHRONIZATION: SemaphoreSlim
private readonly SemaphoreSlim _gate = new(1, 1);

public async Task ProcessAsync(CancellationToken ct)
{
    await _gate.WaitAsync(ct);
    try
    {
        await _dbContext.SaveChangesAsync(ct);
    }
    finally
    {
        _gate.Release();
    }
}
```

---

## 4. Modern Producer-Consumer with `System.Threading.Channels`

In senior backend design, avoid `ConcurrentQueue` polling loops. Use `System.Threading.Channels` for backpressure-aware messaging:

```csharp
public class EventBusChannel
{
    // Bounded channel provides built-in backpressure against fast producers
    private readonly Channel<AppEvent> _channel = Channel.CreateBounded<AppEvent>(new BoundedChannelOptions(1000)
    {
        FullMode = BoundedChannelFullMode.Wait,
        SingleWriter = false,
        SingleReader = true
    });

    public async ValueTask PublishAsync(AppEvent evt, CancellationToken ct = default)
    {
        await _channel.Writer.WriteAsync(evt, ct);
    }

    public IAsyncEnumerable<AppEvent> ReadAllAsync(CancellationToken ct = default)
    {
        return _channel.Reader.ReadAllAsync(ct);
    }
}
```

---

## 5. EF Core Concurrency (Optimistic vs. Pessimistic)

### Optimistic Concurrency Control (OCC)
Assumes conflicts are rare. Entity includes a concurrency token (`RowVersion`):

```csharp
public class BankAccount
{
    public Guid Id { get; set; }
    public decimal Balance { get; set; }

    [Timestamp]
    public byte[] RowVersion { get; set; } = null!;
}

// Handling in Service:
try
{
    account.Balance -= amount;
    await _dbContext.SaveChangesAsync(ct);
}
catch (DbUpdateConcurrencyException)
{
    // Concurrency conflict caught! Another thread updated RowVersion.
    // Fetch latest balance and retry, or return friendly error.
}
```

### Pessimistic Concurrency
Assumes high contention. Rows are locked at query time:
* In SQL Server: `SELECT ... WITH (UPDLOCK, ROWLOCK)`
* In PostgreSQL: `SELECT ... FOR UPDATE`