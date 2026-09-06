# 07. Concurrency & Synchronization Patterns (Senior .NET Reference)

In Senior and Lead backend interviews, concurrency is the ultimate differentiator. Junior developers write code that works on a single thread; senior developers write code that survives high-throughput, multi-threaded contention without data corruption, deadlocks, or thread-pool starvation.

---

## 🧭 1. The C# Synchronization Spectrum

Choose the right tool for the job. Using the wrong primitive causes either performance degradation or silent race conditions.

| Synchronization Primitive | Scope | Supports Async? | Reentrant? | Best Used For |
| :--- | :---: | :---: | :---: | :--- |
| **`lock` (`Monitor`)** | In-Process | ❌ No | ✅ Yes | Very fast, short CPU-bound critical sections. **Never use over `await`**. |
| **`SemaphoreSlim`** | In-Process | ✅ Yes (`WaitAsync`) | ❌ No | Asynchronous critical sections involving I/O (DB, HTTP, Disk), resource throttling. |
| **`ReaderWriterLockSlim`** | In-Process | ❌ No | Optional | High read-to-write ratio scenarios (e.g., in-memory read-heavy caches/trie). |
| **`ConcurrentDictionary`** | In-Process | N/A (atomic methods) | N/A | Thread-safe key-value storage. Use atomic factory lambdas (`GetOrAdd`, `AddOrUpdate`). |
| **`Interlocked`** | CPU Atomic | N/A | N/A | High-speed, lock-free counters, flags, and sequence generators (`Increment`, `Exchange`, `CompareExchange`). |
| **`Channel<T>`** | In-Process | ✅ Yes | N/A | High-performance, lock-free asynchronous Producer-Consumer queues. |
| **Distributed Lock (Redis / RedLock)** | Multi-Node | ✅ Yes | Configurable | Cross-instance mutual exclusion across Kubernetes pods / server clusters. |

---

## ⚠️ 2. The Golden Rule of Async: Never `lock` with `await`

```csharp
// ❌ COMPILE ERROR / DEADLOCK TRAP: Cannot await inside lock
public async Task<Order> ProcessOrderAsync(Guid orderId)
{
    lock (_syncRoot)
    {
        // Compiler error CS1996: Cannot await in the body of a lock statement
        await _paymentGateway.ChargeAsync(orderId); 
    }
}

// ✅ SENIOR PATTERN: SemaphoreSlim(1,1) with async wait and try/finally
private readonly SemaphoreSlim _mutex = new(1, 1);

public async Task<Order> ProcessOrderAsync(Guid orderId, CancellationToken ct)
{
    await _mutex.WaitAsync(ct); // Non-blocking thread-pool friendly wait
    try
    {
        return await _paymentGateway.ChargeAsync(orderId, ct);
    }
    finally
    {
        _mutex.Release(); // Guarantee release even if exception or cancellation occurs
    }
}
```

---

## 🔒 3. Deadlock Prevention: Consistent Lock Ordering

A classic senior interview question: *"How do you transfer money between Account A and Account B concurrently without deadlocking?"*

### The Deadlock Scenario (The Dining Philosophers Trap)
* Thread 1 locks Account A and waits for Account B.
* Thread 2 locks Account B and waits for Account A.
* **Result**: Circular wait $\rightarrow$ Process Hang / Deadlock!

### The Solution: Lexicographical / Global ID Lock Ordering
Always acquire locks in a globally deterministic order (e.g., sort by Account ID).

```csharp
public class AccountTransferService
{
    public async Task<Result> TransferFundsAsync(
        Account source, 
        Account target, 
        decimal amount, 
        CancellationToken ct)
    {
        if (source.Id == target.Id)
            return Result.Fail("Cannot transfer to the same account.");

        // Determine deterministic lock order: Lowest ID first
        var (firstLock, secondLock) = source.Id.CompareTo(target.Id) < 0
            ? (source.SyncLock, target.SyncLock)
            : (target.SyncLock, source.SyncLock);

        await firstLock.WaitAsync(ct);
        try
        {
            await secondLock.WaitAsync(ct);
            try
            {
                if (source.Balance < amount)
                    return Result.Fail("Insufficient funds.");

                source.Debit(amount);
                target.Credit(amount);
                return Result.Ok();
            }
            finally
            {
                secondLock.Release();
            }
        }
        finally
        {
            firstLock.Release();
        }
    }
}
```

---

## ⚖️ 4. Optimistic vs Pessimistic Concurrency

```mermaid
flowchart TD
    Q{"Is Contention High or Low?"}
    Q -->|Low Contention (Standard E-Commerce)| OCC["Optimistic Concurrency Control (OCC)"]
    Q -->|High Contention (Flash Sales, Ticket Drops)| PCC["Pessimistic Concurrency Control (PCC)"]

    OCC --> OCC_Details["Use RowVersion / ConcurrencyToken.\nNo blocking locks.\nCatch DbUpdateConcurrencyException and retry."]
    PCC --> PCC_Details["Use SELECT ... FOR UPDATE or Distributed Lock.\nBlock other threads up-front.\nGuarantees first-come-first-served."]
```

### In-Memory Optimistic Concurrency Pattern
```csharp
public class OptimisticEntity
{
    public Guid Id { get; init; }
    public decimal Balance { get; private set; }
    public int Version { get; private set; }

    public bool TryUpdateBalance(decimal newBalance, int expectedVersion)
    {
        // Atomic compare-and-swap using Interlocked or lock
        if (Version != expectedVersion) return false; // Stale write rejected!
        
        Balance = newBalance;
        Version++;
        return true;
    }
}
```

---

## ⚡ 5. High-Throughput Producer-Consumer with `Channel<T>`

When designing systems like an **Asynchronous Logger**, **Order Processing Queue**, or **Metrics Buffer**, avoid `BlockingCollection` or raw locks. Use `System.Threading.Channels`.

```csharp
public class HighThroughputBuffer<T>
{
    private readonly Channel<T> _channel;

    public HighThroughputBuffer(int capacity)
    {
        var options = new BoundedChannelOptions(capacity)
        {
            FullMode = BoundedChannelFullMode.Wait, // Apply backpressure
            SingleWriter = false,
            SingleReader = true
        };
        _channel = Channel.CreateBounded<T>(options);
    }

    public async ValueTask PublishAsync(T item, CancellationToken ct = default)
    {
        await _channel.Writer.WriteAsync(item, ct);
    }

    public async IAsyncEnumerable<T> ConsumeAllAsync([EnumeratorCancellation] CancellationToken ct = default)
    {
        while (await _channel.Reader.WaitToReadAsync(ct))
        {
            while (_channel.Reader.TryRead(out var item))
            {
                yield return item;
            }
        }
    }
}
```

---

## 🗣️ Senior Interviewer Q&A

**Interviewer:** *"Why is `ConcurrentDictionary.GetOrAdd` not always strictly thread-safe?"*
**You:** "`ConcurrentDictionary` guarantees internal memory safety, but its value factory lambda is **not executed atomically**. If 10 threads call `GetOrAdd` for the same missing key at the same instant, the factory lambda may be executed multiple times concurrently, though only one result will be stored in the dictionary. If the factory creates expensive resources (like database connections), you must wrap the value in a `Lazy<T>`: `dict.GetOrAdd(key, k => new Lazy<Resource>(() => new Resource())).Value`."

**Interviewer:** *"How does `ReaderWriterLockSlim` improve performance over standard `lock`?"*
**You:** "`ReaderWriterLockSlim` allows multiple concurrent reader threads to execute simultaneously without blocking each other, only taking an exclusive lock when a writer updates data. In a 95% read / 5% write scenario (e.g., Cache or File System directory lookups), this drastically increases throughput and eliminates thread contention."

