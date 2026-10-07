namespace LLDPractice.Problem07_CustomCache;

public interface ICache<TKey, TValue> where TKey : notnull
{
    bool TryGet(TKey key, out TValue? value);
    void Put(TKey key, TValue value);
    bool Remove(TKey key);
    int Count { get; }
    int Capacity { get; }
    void Clear();
}

public class LruCache<TKey, TValue> : ICache<TKey, TValue>, IDisposable where TKey : notnull
{
    private class CacheNode
    {
        public TKey Key { get; }
        public TValue Value { get; set; }

        public CacheNode(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }

    private readonly int _capacity;
    private readonly Dictionary<TKey, LinkedListNode<CacheNode>> _map;
    private readonly LinkedList<CacheNode> _lruList;
    private readonly ReaderWriterLockSlim _lock;
    private bool _disposed;

    public int Capacity => _capacity;
    public int Count
    {
        get
        {
            _lock.EnterReadLock();
            try
            {
                return _map.Count;
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }
    }

    public LruCache(int capacity)
    {
        if (capacity <= 0)
            throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity must be greater than zero.");

        _capacity = capacity;
        _map = new Dictionary<TKey, LinkedListNode<CacheNode>>(capacity);
        _lruList = new LinkedList<CacheNode>();
        _lock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
    }

    public bool TryGet(TKey key, out TValue? value)
    {
        ArgumentNullException.ThrowIfNull(key);

        _lock.EnterUpgradeableReadLock();
        try
        {
            if (!_map.TryGetValue(key, out var node))
            {
                value = default;
                return false;
            }

            value = node.Value.Value;

            // Move to most recently used (MRU) position at head
            _lock.EnterWriteLock();
            try
            {
                _lruList.Remove(node);
                _lruList.AddFirst(node);
            }
            finally
            {
                _lock.ExitWriteLock();
            }

            return true;
        }
        finally
        {
            _lock.ExitUpgradeableReadLock();
        }
    }

    public void Put(TKey key, TValue value)
    {
        ArgumentNullException.ThrowIfNull(key);

        _lock.EnterWriteLock();
        try
        {
            if (_map.TryGetValue(key, out var existingNode))
            {
                // Update value and move to MRU
                existingNode.Value.Value = value;
                _lruList.Remove(existingNode);
                _lruList.AddFirst(existingNode);
                return;
            }

            // Evict LRU if at capacity
            if (_map.Count >= _capacity)
            {
                var lruNode = _lruList.Last;
                if (lruNode != null)
                {
                    _map.Remove(lruNode.Value.Key);
                    _lruList.RemoveLast();
                }
            }

            var newNode = new LinkedListNode<CacheNode>(new CacheNode(key, value));
            _lruList.AddFirst(newNode);
            _map[key] = newNode;
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    public bool Remove(TKey key)
    {
        ArgumentNullException.ThrowIfNull(key);

        _lock.EnterWriteLock();
        try
        {
            if (!_map.TryGetValue(key, out var node))
                return false;

            _lruList.Remove(node);
            _map.Remove(key);
            return true;
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    public void Clear()
    {
        _lock.EnterWriteLock();
        try
        {
            _map.Clear();
            _lruList.Clear();
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            _lock.Dispose();
            _disposed = true;
        }
        GC.SuppressFinalize(this);
    }
}
