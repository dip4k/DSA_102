# 19. In-Memory Transactional Key-Value Store (Redis-Lite)

## 📌 Context
The In-Memory Transactional Key-Value Store is one of the most famous machine coding and LLD questions asked by top tech firms (**Atlassian, Uber, Stripe, PhonePe, Grab**).

**The Challenge:** Build an in-memory key-value store that supports basic operations (`SET`, `GET`, `DELETE`) as well as **nested transaction blocks** (`BEGIN`, `COMMIT`, `ROLLBACK`) where changes within a transaction are isolated until committed, and rolling back restores the previous state without data loss.

---

## 1. Architectural Strategy: The Transaction Stack

To support nested transactions efficiently without copying the entire database on every `BEGIN`:
* We maintain a **Global Database** (a primary dictionary).
* We maintain a **Stack of Transaction Scopes**. Each scope tracks:
  * Overwritten original values (for rollback).
  * Deleted keys (markers for rollback).
  * New additions.
* When `GET(key)` is called, we inspect the stack from top to bottom (most recent scope to global database).
* When `BEGIN` is called, we push a new empty transaction scope onto the stack.
* When `ROLLBACK` is called, we pop the top scope off the stack.
* When `COMMIT` is called, we collapse/merge all active scopes down to the global store and clear the stack.

```
Stack of Transaction Scopes:
[ Top Scope (Level 2) ]  -> Uncommitted changes in inner BEGIN block
       |
[ Scope (Level 1) ]      -> Uncommitted changes in outer BEGIN block
       |
[ Global Store (Base) ]  -> Committed, permanent state
```

---

## 2. Complete C# Implementation

```csharp
public class TransactionalKeyValueStore
{
    // Permanent committed storage
    private readonly Dictionary<string, string> _globalStore = new();

    // Stack of active transactions. Null value represents a deleted key.
    private readonly Stack<Dictionary<string, string?>> _transactionStack = new();

    // Concurrency control for thread-safe operations
    private readonly object _lock = new object();

    public bool IsInTransaction => _transactionStack.Count > 0;

    public void Set(string key, string value)
    {
        if (string.IsNullOrWhiteSpace(key)) throw new ArgumentException("Key cannot be null or empty.", nameof(key));
        if (value is null) throw new ArgumentNullException(nameof(value));

        lock (_lock)
        {
            if (IsInTransaction)
            {
                // Stage in current transaction scope
                _transactionStack.Peek()[key] = value;
            }
            else
            {
                // Write directly to global store
                _globalStore[key] = value;
            }
        }
    }

    public string? Get(string key)
    {
        if (string.IsNullOrWhiteSpace(key)) throw new ArgumentException("Key cannot be null or empty.", nameof(key));

        lock (_lock)
        {
            // 1. Search transaction stack from top to bottom (newest to oldest)
            foreach (var scope in _transactionStack)
            {
                if (scope.TryGetValue(key, out var stagedValue))
                {
                    // If stagedValue is null, it was deleted in this transaction
                    return stagedValue;
                }
            }

            // 2. Fall back to global committed store
            return _globalStore.TryGetValue(key, out var globalValue) ? globalValue : null;
        }
    }

    public void Delete(string key)
    {
        if (string.IsNullOrWhiteSpace(key)) throw new ArgumentException("Key cannot be null or empty.", nameof(key));

        lock (_lock)
        {
            if (IsInTransaction)
            {
                // Stage deletion as a null sentinel
                _transactionStack.Peek()[key] = null;
            }
            else
            {
                _globalStore.Remove(key);
            }
        }
    }

    public void Begin()
    {
        lock (_lock)
        {
            _transactionStack.Push(new Dictionary<string, string?>());
        }
    }

    public void Commit()
    {
        lock (_lock)
        {
            if (!IsInTransaction)
                throw new InvalidOperationException("No active transaction to commit.");

            // Flatten all transaction scopes from bottom to top into the global store
            // Stack enumeration starts from top, so reverse to apply in chronological order
            var scopesInOrder = _transactionStack.Reverse().ToList();

            foreach (var scope in scopesInOrder)
            {
                foreach (var (key, value) in scope)
                {
                    if (value is null)
                    {
                        _globalStore.Remove(key);
                    }
                    else
                    {
                        _globalStore[key] = value;
                    }
                }
            }

            // All transactions are now permanently committed
            _transactionStack.Clear();
        }
    }

    public void Rollback()
    {
        lock (_lock)
        {
            if (!IsInTransaction)
                throw new InvalidOperationException("No active transaction to rollback.");

            // Simply discard the current top transaction scope
            _transactionStack.Pop();
        }
    }
}
```

---

## 3. Example Execution Trace

```csharp
var db = new TransactionalKeyValueStore();

db.Set("a", "10");
Console.WriteLine(db.Get("a")); // Output: 10

db.Begin();
db.Set("a", "20");
Console.WriteLine(db.Get("a")); // Output: 20 (from active transaction)

db.Begin();
db.Set("a", "30");
db.Set("b", "50");
Console.WriteLine(db.Get("a")); // Output: 30

db.Rollback(); // Discard Level 2 transaction
Console.WriteLine(db.Get("a")); // Output: 20
Console.WriteLine(db.Get("b")); // Output: null

db.Commit(); // Commit Level 1 transaction
Console.WriteLine(db.Get("a")); // Output: 20
```

---

## 🗣️ Interviewer Discussion & Tradeoffs

**Interviewer:** *"Why did you use a stack of scopes with a `null` sentinel for deletion, instead of cloning the full dictionary on every `BEGIN`?"*
**You:** "Cloning the entire dictionary on `BEGIN` takes **O(N)** time and memory, where N is the number of keys. If you have 1,000,000 keys and initiate 5 nested transactions, you would duplicate 5,000,000 items. Using a stack of delta scopes makes `BEGIN` run in **O(1)** time and memory. The memory used is proportional only to the number of keys modified during the transaction."

**Interviewer:** *"What is the time complexity of `Get` with nested transactions?"*
**You:** "If there are D nested transactions, `Get` searches up to D dictionaries. In practice, transaction depth D is small (typically < 5), so `Get` remains **O(1)** average time. However, if transaction depth were very large, we could optimize by using persistent data structures or maintaining a merged view."

**Interviewer:** *"How would you make this thread-safe in a distributed environment with multiple servers?"*
**You:** "This implementation uses an in-memory lock for a single process. In a distributed multi-server architecture:
1. We would back the storage with a partitioned cluster like Redis or DynamoDB.
2. Transactions across distributed nodes require **Two-Phase Commit (2PC)** or a consensus protocol (like Raft).
3. To avoid distributed locking bottlenecks, we can use **Optimistic Concurrency Control** with version vectors on each key."