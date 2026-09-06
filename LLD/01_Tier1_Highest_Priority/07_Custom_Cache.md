# 07. Custom Cache (LRU + TTL + Concurrency)

## 📌 Context
Building a custom cache tests your ability to mix **Data Structures (DSA)** with **Senior C# Concurrency Concepts**. You must handle eviction (LRU), expiration (TTL), and ensure that highly concurrent reads/writes do not corrupt the internal state.

---

## 1. Core Interfaces & Generic Types

Use generics so the cache can store any type safely.

```csharp
public interface ICache<TKey, TValue> where TKey : notnull
{
    void Put(TKey key, TValue value, TimeSpan? ttl = null);
    bool TryGet(TKey key, out TValue? value);
    void Remove(TKey key);
}
```

---

## 2. The Cache Entry Node (For O(1) LRU)

To implement an LRU cache efficiently, we need a Doubly Linked List node.

```csharp
internal class CacheNode<TKey, TValue>
{
    public TKey Key { get; }
    public TValue Value { get; set; }
    public DateTimeOffset? ExpiryTime { get; set; }
    
    // Pointers for Doubly Linked List
    public CacheNode<TKey, TValue>? Prev { get; set; }
    public CacheNode<TKey, TValue>? Next { get; set; }

    public CacheNode(TKey key, TValue value, TimeSpan? ttl)
    {
        Key = key;
        Value = value;
        ExpiryTime = ttl.HasValue ? DateTimeOffset.UtcNow.Add(ttl.Value) : null;
    }

    public bool IsExpired => ExpiryTime.HasValue && DateTimeOffset.UtcNow > ExpiryTime.Value;
}
```

---

## 3. Thread-Safe Implementation

We use a `Dictionary` combined with a `ReaderWriterLockSlim`. 
*Why not `ConcurrentDictionary`?* Because updating the Dictionary AND updating the Doubly Linked List (moving a node to the front) must happen **atomically** together. A standard lock over both structures is required. `ReaderWriterLockSlim` allows multiple concurrent readers but exclusive writers.

```csharp
public class LruCache<TKey, TValue> : ICache<TKey, TValue> where TKey : notnull
{
    private readonly int _capacity;
    private readonly Dictionary<TKey, CacheNode<TKey, TValue>> _map;
    
    // Doubly linked list tracking (Head = Most recently used, Tail = LRU)
    private CacheNode<TKey, TValue>? _head;
    private CacheNode<TKey, TValue>? _tail;
    
    // Concurrency primitive
    private readonly ReaderWriterLockSlim _lock = new(LockRecursionPolicy.NoRecursion);

    public LruCache(int capacity)
    {
        if (capacity <= 0) throw new ArgumentOutOfRangeException(nameof(capacity));
        _capacity = capacity;
        _map = new Dictionary<TKey, CacheNode<TKey, TValue>>(capacity);
    }

    public bool TryGet(TKey key, out TValue? value)
    {
        _lock.EnterUpgradeableReadLock();
        try
        {
            if (_map.TryGetValue(key, out var node))
            {
                if (node.IsExpired)
                {
                    // Upgrade lock to write so we can safely remove the expired item
                    _lock.EnterWriteLock();
                    try
                    {
                        RemoveNode(node);
                        _map.Remove(key);
                    }
                    finally
                    {
                        _lock.ExitWriteLock();
                    }
                    
                    value = default;
                    return false;
                }

                // Move to head because it was just accessed
                _lock.EnterWriteLock();
                try
                {
                    MoveToHead(node);
                }
                finally
                {
                    _lock.ExitWriteLock();
                }

                value = node.Value;
                return true;
            }

            value = default;
            return false;
        }
        finally
        {
            _lock.ExitUpgradeableReadLock();
        }
    }

    public void Put(TKey key, TValue value, TimeSpan? ttl = null)
    {
        _lock.EnterWriteLock();
        try
        {
            if (_map.TryGetValue(key, out var existingNode))
            {
                existingNode.Value = value;
                existingNode.ExpiryTime = ttl.HasValue ? DateTimeOffset.UtcNow.Add(ttl.Value) : null;
                MoveToHead(existingNode);
                return;
            }

            if (_map.Count >= _capacity)
            {
                // Evict least recently used (Tail)
                if (_tail != null)
                {
                    _map.Remove(_tail.Key);
                    RemoveNode(_tail);
                }
            }

            var newNode = new CacheNode<TKey, TValue>(key, value, ttl);
            _map[key] = newNode;
            AddToHead(newNode);
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }
    
    public void Remove(TKey key)
    {
        _lock.EnterWriteLock();
        try
        {
            if (_map.TryGetValue(key, out var node))
            {
                RemoveNode(node);
                _map.Remove(key);
            }
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    // --- Linked List Helpers (Assumes Write Lock is currently held!) ---
    private void AddToHead(CacheNode<TKey, TValue> node)
    {
        node.Next = _head;
        node.Prev = null;

        if (_head != null) _head.Prev = node;
        _head = node;

        if (_tail == null) _tail = _head;
    }

    private void RemoveNode(CacheNode<TKey, TValue> node)
    {
        if (node.Prev != null) node.Prev.Next = node.Next;
        else _head = node.Next;

        if (node.Next != null) node.Next.Prev = node.Prev;
        else _tail = node.Prev;
    }

    private void MoveToHead(CacheNode<TKey, TValue> node)
    {
        if (node == _head) return;
        RemoveNode(node);
        AddToHead(node);
    }
}
```
---

## 🗣️ Interviewer Discussion & Tradeoffs

**Interviewer:** *"Why didn't you just use `ConcurrentDictionary` and skip the `ReaderWriterLockSlim`?"*
**You:** "While `ConcurrentDictionary` provides thread-safe inserts and reads, an LRU Cache requires updating **two** data structures simultaneously: the Dictionary (for O(1) access) and the Doubly Linked List (moving the node to the head). We need **Atomicity** across *both* operations. `ConcurrentDictionary` cannot lock the linked list. Therefore, a custom lock is required."

**Interviewer:** *"Why `ReaderWriterLockSlim` instead of a standard `lock` object?"*
**You:** "A cache is a read-heavy system. A standard `lock` prevents two threads from reading at the same time, bottlenecking performance. `ReaderWriterLockSlim` allows an unlimited number of concurrent threads to read (`EnterUpgradeableReadLock`), but ensures that when a thread needs to write (evict an item or add a new one), it gets exclusive access."

**Interviewer:** *"Your cache removes expired items only when they are accessed (`TryGet`). What's the problem with this?"*
**You:** "This is called **Inline / Lazy Eviction**. If an item is never accessed again, it sits in memory forever, causing a memory leak. In a production system, I would supplement this with an `IHostedService` (Background Worker) that wakes up every 5 minutes, acquires the write lock, and sweeps/removes expired nodes from the tail of the list."
