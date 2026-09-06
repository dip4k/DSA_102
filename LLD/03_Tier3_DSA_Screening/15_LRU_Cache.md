# 15. LRU Cache (DSA + OOP)

## 📌 Context
The LRU (Least Recently Used) Cache is the most frequently asked DSA + OOP hybrid question. It tests if you know how to combine a **Hash Map** with a **Doubly Linked List** to achieve **O(1)** time complexity for both `Get` and `Put` operations.

---

## 1. The Node Definition
To achieve O(1) removals, we need a doubly linked list. A standard C# `LinkedList<T>` works, but interviewers prefer you build the Node class yourself to prove you understand the mechanics.

```csharp
public class LruNode
{
    public int Key { get; }
    public int Value { get; set; }
    public LruNode? Prev { get; set; }
    public LruNode? Next { get; set; }

    public LruNode(int key, int value)
    {
        Key = key;
        Value = value;
    }
}
```

---

## 2. The Cache Implementation

```csharp
public class LruCache
{
    private readonly int _capacity;
    private readonly Dictionary<int, LruNode> _cache;
    
    // Dummy head and tail to avoid null-checking edge cases
    private readonly LruNode _head;
    private readonly LruNode _tail;

    public LruCache(int capacity)
    {
        _capacity = capacity;
        _cache = new Dictionary<int, LruNode>(capacity);
        
        _head = new LruNode(-1, -1);
        _tail = new LruNode(-1, -1);
        _head.Next = _tail;
        _tail.Prev = _head;
    }

    public int Get(int key)
    {
        if (!_cache.TryGetValue(key, out var node))
        {
            return -1;
        }

        // It was accessed, so move it to the front (Most Recently Used)
        RemoveNode(node);
        AddNodeToFront(node);
        
        return node.Value;
    }

    public void Put(int key, int value)
    {
        if (_cache.TryGetValue(key, out var existingNode))
        {
            // Update value and move to front
            existingNode.Value = value;
            RemoveNode(existingNode);
            AddNodeToFront(existingNode);
        }
        else
        {
            if (_cache.Count >= _capacity)
            {
                // Evict the Least Recently Used (the node right before the dummy tail)
                var lruNode = _tail.Prev!;
                RemoveNode(lruNode);
                _cache.Remove(lruNode.Key);
            }

            var newNode = new LruNode(key, value);
            _cache[key] = newNode;
            AddNodeToFront(newNode);
        }
    }

    // --- Linked List Helper Methods ---
    
    private void AddNodeToFront(LruNode node)
    {
        var temp = _head.Next!;
        _head.Next = node;
        node.Prev = _head;
        node.Next = temp;
        temp.Prev = node;
    }

    private void RemoveNode(LruNode node)
    {
        var prevNode = node.Prev!;
        var nextNode = node.Next!;
        prevNode.Next = nextNode;
        nextNode.Prev = prevNode;
    }
}
```

---

## 🗣️ Interviewer Discussion & Tradeoffs

**Interviewer:** *"Why did you use a Doubly Linked List instead of a standard Array or `List<T>` to track the usage history?"*
**You:** "If we use an Array or a standard `List<T>`, removing an item from the middle of the history (when it's accessed) requires shifting all subsequent elements, which takes **O(N)** time. A Doubly Linked List allows us to extract a node and update its neighbors' pointers in exactly **O(1)** time, provided we already have the reference to the node (which we get instantly from the Dictionary)."

**Interviewer:** *"Why do we need dummy `_head` and `_tail` nodes?"*
**You:** "Using dummy boundary nodes eliminates the need for endless `if (head == null)` and `if (tail == null)` edge-case checks inside the `AddNodeToFront` and `RemoveNode` methods. The nodes will always exist between `_head` and `_tail`, making the logic drastically cleaner and less prone to NullReferenceExceptions."

**Interviewer:** *"If we deploy this to a fleet of 5 microservice instances, will the cache still work?"*
**You:** "It will work locally per instance, meaning the caches will quickly become out of sync (Cache Coherency Problem). User A might get data from Instance 1's cache, update it on Instance 2, and then read stale data from Instance 1. In a distributed environment, we should rip out the in-memory LRU and replace it with a **Redis Cache**, relying on Redis's built-in `allkeys-lru` eviction policy."