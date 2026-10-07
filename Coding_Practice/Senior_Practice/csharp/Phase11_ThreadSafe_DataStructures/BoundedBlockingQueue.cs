namespace SeniorPractice.Phase11;

/// <summary>
/// Bounded Blocking Queue (LeetCode #1188 pattern) using Monitor (Mutex + Condition Variables).
/// Invariant: Producers block when queue is full; Consumers block when queue is empty.
/// All waits defend against spurious wakeups via while loops.
/// </summary>
public class BoundedBlockingQueue<T>
{
    private readonly T[] _buffer;
    private readonly int _capacity;
    private readonly object _lock = new();
    private int _head;
    private int _tail;
    private int _count;

    public int Capacity => _capacity;

    public BoundedBlockingQueue(int capacity)
    {
        if (capacity <= 0)
            throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity must be positive.");

        _capacity = capacity;
        _buffer = new T[capacity];
        _head = 0;
        _tail = 0;
        _count = 0;
    }

    public void Enqueue(T item)
    {
        lock (_lock)
        {
            // Defend against spurious wakeups
            while (_count == _capacity)
            {
                Monitor.Wait(_lock);
            }

            _buffer[_tail] = item;
            _tail = (_tail + 1) % _capacity;
            _count++;

            // Wake waiting consumers
            Monitor.PulseAll(_lock);
        }
    }

    public T Dequeue()
    {
        lock (_lock)
        {
            // Defend against spurious wakeups
            while (_count == 0)
            {
                Monitor.Wait(_lock);
            }

            T item = _buffer[_head];
            _buffer[_head] = default!;
            _head = (_head + 1) % _capacity;
            _count--;

            // Wake waiting producers
            Monitor.PulseAll(_lock);
            return item;
        }
    }

    public int Size()
    {
        lock (_lock)
        {
            return _count;
        }
    }
}
