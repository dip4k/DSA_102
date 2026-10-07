namespace SeniorPractice.Phase11;

/// <summary>
/// Thread-Safe LRU Cache using ReaderWriterLockSlim.
/// Note: Because Get() mutates LRU recency ordering, promotion requires an upgradeable
/// read lock or write lock. We use a disciplined lock protocol to guarantee thread safety.
/// </summary>
public class ThreadSafeLruCache<TKey, TValue> : IDisposable where TKey : notnull
{
    private class Node
    {
        public TKey Key { get; }
        public TValue Value { get; set; }
        public Node? Prev { get; set; }
        public Node? Next { get; set; }

        public Node(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }

    private readonly int _capacity;
    private readonly Dictionary<TKey, Node> _map;
    private readonly Node _head;
    private readonly Node _tail;
    private readonly ReaderWriterLockSlim _rwLock = new(LockRecursionPolicy.NoRecursion);

    public int Capacity => _capacity;

    public ThreadSafeLruCache(int capacity)
    {
        if (capacity <= 0)
            throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity must be positive.");

        _capacity = capacity;
        _map = new Dictionary<TKey, Node>(capacity);
        _head = new Node(default!, default!);
        _tail = new Node(default!, default!);
        _head.Next = _tail;
        _tail.Prev = _head;
    }

    public int Count
    {
        get
        {
            _rwLock.EnterReadLock();
            try { return _map.Count; }
            finally { _rwLock.ExitReadLock(); }
        }
    }

    public bool TryGet(TKey key, out TValue value)
    {
        _rwLock.EnterWriteLock();
        try
        {
            if (_map.TryGetValue(key, out var node))
            {
                Detach(node);
                AttachToHead(node);
                value = node.Value;
                return true;
            }

            value = default!;
            return false;
        }
        finally
        {
            _rwLock.ExitWriteLock();
        }
    }

    public void Put(TKey key, TValue value)
    {
        _rwLock.EnterWriteLock();
        try
        {
            if (_map.TryGetValue(key, out var existing))
            {
                existing.Value = value;
                Detach(existing);
                AttachToHead(existing);
                return;
            }

            if (_map.Count >= _capacity)
            {
                var lru = _tail.Prev!;
                Detach(lru);
                _map.Remove(lru.Key);
            }

            var newNode = new Node(key, value);
            _map[key] = newNode;
            AttachToHead(newNode);
        }
        finally
        {
            _rwLock.ExitWriteLock();
        }
    }

    private void Detach(Node node)
    {
        node.Prev!.Next = node.Next;
        node.Next!.Prev = node.Prev;
    }

    private void AttachToHead(Node node)
    {
        node.Next = _head.Next;
        node.Prev = _head;
        _head.Next!.Prev = node;
        _head.Next = node;
    }

    public void Dispose()
    {
        _rwLock.Dispose();
        GC.SuppressFinalize(this);
    }
}
