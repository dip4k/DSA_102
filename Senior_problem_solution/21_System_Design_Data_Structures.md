# Phase 21: System Design Data Structures

> **Focus:** Custom Cache Eviction Policies (LRU / LFU), $O(1)$ Randomized Collections, Stream Rate Limiting & Memory Leak Prevention, Lazy-Evaluating Hierarchical Iterators, Search Engine Inverted Indexing, and High-Throughput Circular Ring Buffers.  
> **Source Curriculum:** [`Senior_dsa_question_list.md`](../Senior_dsa_question_list.md) — Phase 21 (Problems #116–#122)

---


## 116. LRU Cache (LeetCode #146)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#hash-map` `#doubly-linked-list` `#design` `#o(1)-operations` `#lru-eviction` |
| **LeetCode Link** | [LRU Cache](https://leetcode.com/problems/lru-cache/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Design a data structure that follows the constraints of a Least Recently Used (LRU) cache. Implement the `LRUCache` class:
  - `LRUCache(int capacity)`: Initialize the LRU cache with positive size `capacity`.
  - `int Get(int key)`: Return the value of the `key` if the key exists, otherwise return `-1`.
  - `void Put(int key, int value)`: Update the value of the `key` if the `key` exists. Otherwise, add the `key-value` pair to the cache. If the number of keys exceeds the `capacity` from this operation, evict the least recently used key.
- **Assumptions & Contracts:**
  - The functions `Get` and `Put` must each run in $O(1)$ average time complexity.
  - Keys and values are non-negative integers.
  - Calling `Get(key)` on an existing key counts as a recent access, promoting the key to the most recently used (MRU) position.
  - Calling `Put(key, value)` on an existing key updates its value and promotes it to MRU position without increasing the current cache count.
- **Key Constraints:**
  - $1 \le \text{capacity} \le 3000$
  - $0 \le \text{key} \le 10^4$
  - $0 \le \text{value} \le 10^5$
  - At most $2 \times 10^5$ calls will be made to `Get` and `Put`.
- **Senior Edge Cases to Defend:**
  - `capacity = 1`: The cache must be able to evict the single existing item on the very next `Put` of a different key without crashing sentinel pointers.
  - Overwriting an existing key when the cache is currently at maximum capacity: Must update in place and promote to MRU; must **not** trigger an eviction.
  - Querying a non-existent key: Must return `-1` and leave the doubly linked list pointers completely undisturbed.
  - Repeated `Get` or `Put` on the same key: Should repeatedly promote the node to MRU head without creating duplicate list nodes or corrupting self-referencing pointers.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Decouple key resolution from recency sequencing. A hash map (`Dictionary<int, DNode>`) provides $O(1)$ key-to-node pointer lookup. A doubly linked list with dummy sentinel head and tail nodes provides $O(1)$ arbitrary node detachment and head splicing.
- **Sample Execution Trace:**
  - `LRUCache lRUCache = new LRUCache(2);`
  - `lRUCache.Put(1, 1); // cache is {1=1}`
  - `lRUCache.Put(2, 2); // cache is {1=1, 2=2}`
  - `lRUCache.Get(1);    // return 1, cache is {2=2, 1=1} (1 promoted to MRU)`
  - `lRUCache.Put(3, 3); // LRU key was 2, evicts key 2, cache is {1=1, 3=3}`
  - `lRUCache.Get(2);    // returns -1 (not found)`
  - `lRUCache.Put(4, 4); // LRU key was 1, evicts key 1, cache is {3=3, 4=4}`
  - `lRUCache.Get(1);    // return -1 (not found)`
  - `lRUCache.Get(3);    // return 3, cache is {4=4, 3=3}`
  - `lRUCache.Get(4);    // return 4, cache is {3=3, 4=4}`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine a velvet-roped VIP club where the manager has a fixed capacity of seats. Every time a patron orders a drink or enters (`Get` or `Put`), the bouncer escorts them straight to the front of the stage (Head / MRU). As new people arrive and the club is full, the person standing closest to the back exit door (Tail / LRU) is quietly escorted out into the street. The manager keeps an instantaneous Rolodex (hash map) so they can spot any patron and tap them on the shoulder without walking through the entire room.

#### 3.2 The Naive Bottleneck & Redundant Computation
- A single hash map with timestamps requires scanning all entries on eviction to find $\min(\text{timestamp})$, costing $O(N)$ time per eviction.
- An array or singly linked list can maintain order, but removing an arbitrary element in the middle requires an $O(N)$ linear scan to find the preceding node and splice pointers, or $O(N)$ memory shifts in an array.
- Standard libraries often provide `LinkedList<T>`, but calling `.Remove(value)` on a standard linked list requires an $O(N)$ search unless you already hold the direct `LinkedListNode<T>` handle. Storing the node handle directly in a hash map bridges this exact gap.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Dual Data Structure Synergy:**
  - **Associative Mapping:** $\text{Key} \xrightarrow{O(1)} \text{Node Handle}$ (Hash Map).
  - **Temporal Ordering:** $\text{Head} \leftrightarrow \text{Node}_1 \leftrightarrow \text{Node}_2 \leftrightarrow \dots \leftrightarrow \text{Node}_k \leftrightarrow \text{Tail}$ (Doubly Linked List).
- **The $O(1)$ Splicing Invariant:** A doubly linked node stores both `prev` and `next`. Detaching any node $x$ requires exactly four pointer mutations regardless of its position:
  $$x.prev.next = x.next$$
  $$x.next.prev = x.prev$$
- **Sentinel Boundary Invariant:** By allocating dummy `head` and `tail` nodes that are never evicted:
  - `head.next` always points to the Most Recently Used (MRU) node.
  - `tail.prev` always points to the Least Recently Used (LRU) node.
  - No `null` checks are ever needed for edge cases (empty list, singleton list).

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
[ Hash Map: Key -> DNode ]
  │           │           │
  ▼           ▼           ▼
┌────────┐  ┌────────┐  ┌────────┐
│ Key: 1 │  │ Key: 2 │  │ Key: 3 │
└───┬────┘  └───┬────┘  └───┬────┘
    │           │           │
    ▼           ▼           ▼
┌──────────────┐    ┌──────────────┐    ┌──────────────┐    ┌──────────────┐    ┌──────────────┐
│ DUMMY HEAD   │<──>│ NODE (MRU)   │<──>│ NODE         │<──>│ NODE (LRU)   │<──>│ DUMMY TAIL   │
│ [Key:0,Val:0]│    │ [Key:3,Val:C]│    │ [Key:1,Val:A]│    │ [Key:2,Val:B]│    │ [Key:0,Val:0]│
└──────────────┘    └──────────────┘    └──────────────┘    └──────────────┘    └──────────────┘
       ▲                                                           ▲                   ▲
       │                                                           │                   │
   Always MRU                                                Target for Eviction  Boundary Guard
```

- **Invariant 1:** `_nodeMap.Count == DLL.NodeCount`.
- **Invariant 2:** For every node $u$ in DLL between `_head` and `_tail`, `_nodeMap[u.Key] == u`.
- **Invariant 3:** When capacity is exceeded, the node evicted is strictly `_tail.prev`.

#### 3.5 State Transition Triggers & Decision Gates
1. **`Get(key)` Decision Gate:**
   - Query `_nodeMap.TryGetValue(key, out DNode node)`.
   - **Miss:** Return `-1`.
   - **Hit:** Call `MoveToHead(node)`:
     1. Unlink `node` from current position (`RemoveNode(node)`).
     2. Splice `node` directly after `_head` (`AddToHead(node)`).
     3. Return `node.Value`.
2. **`Put(key, value)` Decision Gate:**
   - Query `_nodeMap.TryGetValue(key, out DNode node)`.
   - **Case A (Key Exists - Update):**
     1. Mutate `node.Value = value`.
     2. Call `MoveToHead(node)`.
   - **Case B (Key New - Insert):**
     1. If `_nodeMap.Count == _capacity`:
        - `lru = _tail.prev`
        - Call `RemoveNode(lru)`
        - Remove `_nodeMap.Remove(lru.Key)`
     2. Instantiate `newNode = new DNode(key, value)`.
     3. Add to map: `_nodeMap[key] = newNode`.
     4. Call `AddToHead(newNode)`.

#### 3.6 Concrete Step-by-Step State Trace
Trace with `capacity = 2`:
Operations: `Put(1, 10)`, `Put(2, 20)`, `Get(1)`, `Put(3, 30)`, `Get(2)`, `Put(4, 40)`

| Step | Operation | Map State | DLL Order (`Head.next` $\to$ `Tail.prev`) | Evicted Key | Output |
| :---: | :--- | :--- | :--- | :---: | :---: |
| 1 | `Put(1, 10)` | `{1: [1, 10]}` | `[1]` | None | — |
| 2 | `Put(2, 20)` | `{1: [1, 10], 2: [2, 20]}` | `[2] <-> [1]` | None | — |
| 3 | `Get(1)` | `{1: [1, 10], 2: [2, 20]}` | `[1] <-> [2]` | None | `10` |
| 4 | `Put(3, 30)` | Capacity full (2); evict LRU `[2]`. `{1: [1, 10], 3: [3, 30]}` | `[3] <-> [1]` | `2` | — |
| 5 | `Get(2)` | `{1: [1, 10], 3: [3, 30]}` | `[3] <-> [1]` | None | `-1` |
| 6 | `Put(4, 40)` | Capacity full (2); evict LRU `[1]`. `{3: [3, 30], 4: [4, 40]}` | `[4] <-> [3]` | `1` | — |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1: Custom Doubly Linked List + Dictionary (Gold Standard):**
  - Uses explicit raw node references (`DNode`).
  - Guarantees true zero-allocation promotions (`MoveToHead` reassigns 4 pointers without allocating or boxing).
  - Strictly $O(1)$ worst-case pointer manipulations.
- **Approach 2: .NET `LinkedList<T>` + Dictionary<int, LinkedListNode<T>>:**
  - Uses standard library collections. Functional and clean, but carries slight overhead due to internal validity checks and allocation overhead on `LinkedListNode<T>`.
- **Approach 3: C# `OrderedDictionary` / Java `LinkedHashMap`:**
  - Language-provided abstraction. In interviews, senior candidates are expected to demonstrate knowledge of raw pointer manipulation and sentinel invariants rather than deferring to a built-in black box.

#### 4.2 Step-by-Step Natural Progression Flow
1. **Step 1: Define the Node Primitive:** Class `DNode` with fields `Key`, `Value`, `Prev`, `Next`.
2. **Step 2: Sentinel Setup:** In constructor, link `_head.Next = _tail` and `_tail.Prev = _head`.
3. **Step 3: Atomic Primitives:** Write `RemoveNode(DNode)` and `AddToHead(DNode)` first. Compose `MoveToHead(DNode)` as `RemoveNode` then `AddToHead`.
4. **Step 4: Connect Map to List:** Implement `Get` and `Put` exclusively through these primitives.

#### 4.3 Alternative Approaches Analysis
- **Timestamps + PriorityQueue ($O(\log N)$):** Store entry with last access timestamp in a min-heap. Eviction is $O(\log N)$, and updating recency requires decrease-key or lazy deletion, which bloats heap size to $O(\text{Operations})$.
- **Singly Linked List ($O(N)$):** Cannot delete a node in $O(1)$ without a reference to the preceding node.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: Custom DLL + Hash Map | Approach 2: Min-Heap with Timestamps | Approach 3: Array / List Scan |
| :--- | :--- | :--- | :--- |
| **Time Complexity (Best / Avg / Worst)** | $O(1)$ / $O(1)$ / $O(1)^*$ | $O(1)$ / $O(\log N)$ / $O(\log N)$ | $O(1)$ / $O(N)$ / $O(N)$ |
| **Auxiliary Space** | $O(\text{Capacity})$ | $O(\text{Capacity} + \text{Ops})$ | $O(\text{Capacity})$ |
| **Output Space** | $O(1)$ | $O(1)$ | $O(1)$ |
| **Cache Locality** | Moderate (pointer dereferences) | High (contiguous binary heap) | Optimal (contiguous array) |
| **In-Place Mutability** | High (pointer re-wiring) | Moderate (heap bubble operations) | High (element shifts) |
| **Streaming Suitability** | Optimal (strictly bounded $O(1)$) | Degrades (lazy deletion bloat) | Unusable for high-rate streams |

$^*$*Assuming negligible hash collision degradation.*

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: Problem #116 - LRU Cache
 * ============================================================================
 * Core Pattern      : Hash Map + Doubly Linked List with Sentinel Boundaries
 * Time Complexity   : O(1) amortized for both Get() and Put() operations
 * Space Complexity  : O(Capacity) auxiliary space for dictionary and DLL nodes
 * Selection Rule    : Custom DLL node manipulation provides zero-allocation promotions
 *                     and exact O(1) eviction without standard library overhead.
 * Defensive Traps   : 1. Update value on Put() hit before moving to head.
 *                     2. Check key existence BEFORE testing capacity limits.
 *                     3. Store Key inside DNode so evicted node can remove its
 *                        own key from the Dictionary in O(1).
 * ============================================================================
 */

namespace SeniorDSA.SystemDesign;

public class LRUCache
{
    /// <summary>
    /// Doubly linked list node holding both key and value.
    /// Holding the key is mandatory to allow O(1) reverse lookup into the dictionary upon eviction.
    /// </summary>
    private sealed class DNode
    {
        public int Key { get; }
        public int Value { get; set; }
        public DNode? Prev { get; set; }
        public DNode? Next { get; set; }

        public DNode(int key, int value)
        {
            Key = key;
            Value = value;
        }
    }

    private readonly int _capacity;
    private readonly Dictionary<int, DNode> _nodeMap;
    private readonly DNode _head;
    private readonly DNode _tail;

    /// <summary>
    /// Initializes the LRU cache with the specified maximum capacity.
    /// Sets up dummy sentinel head and tail nodes to eliminate edge-case null checks.
    /// </summary>
    /// <param name="capacity">Maximum number of items the cache can hold.</param>
    public LRUCache(int capacity)
    {
        if (capacity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity must be strictly positive.");
        }

        _capacity = capacity;
        _nodeMap = new Dictionary<int, DNode>(capacity);

        // Dummy sentinel nodes to guard list boundaries
        _head = new DNode(0, 0);
        _tail = new DNode(0, 0);
        _head.Next = _tail;
        _tail.Prev = _head;
    }

    /// <summary>
    /// Retrieves the value of the key if present and promotes the node to MRU (head).
    /// </summary>
    /// <param name="key">The cache lookup key.</param>
    /// <returns>Value if found; otherwise -1.</returns>
    public int Get(int key)
    {
        if (!_nodeMap.TryGetValue(key, out var node))
        {
            return -1;
        }

        // Recency update: promote accessed node to MRU position
        MoveToHead(node);
        return node.Value;
    }

    /// <summary>
    /// Inserts a new key-value pair or updates an existing key.
    /// Evicts the least recently used element if capacity is exceeded.
    /// </summary>
    public void Put(int key, int value)
    {
        if (_nodeMap.TryGetValue(key, out var existingNode))
        {
            // Key exists: update value and promote to MRU. No capacity check needed.
            existingNode.Value = value;
            MoveToHead(existingNode);
            return;
        }

        // Key is new: evict LRU if capacity limit is reached
        if (_nodeMap.Count >= _capacity)
        {
            DNode lru = PopTail();
            _nodeMap.Remove(lru.Key);
        }

        // Allocate and splice new node at MRU position
        var newNode = new DNode(key, value);
        _nodeMap[key] = newNode;
        AddToHead(newNode);
    }

    /// <summary>
    /// Splices an unlinked node directly after the dummy head sentinel.
    /// </summary>
    private void AddToHead(DNode node)
    {
        node.Prev = _head;
        node.Next = _head.Next;
        _head.Next!.Prev = node;
        _head.Next = node;
    }

    /// <summary>
    /// Unlinks an arbitrary node from the doubly linked list in O(1) time.
    /// </summary>
    private static void RemoveNode(DNode node)
    {
        node.Prev!.Next = node.Next;
        node.Next!.Prev = node.Prev;
    }

    /// <summary>
    /// Moves an existing node to the MRU position by unlinking and splicing at head.
    /// </summary>
    private void MoveToHead(DNode node)
    {
        RemoveNode(node);
        AddToHead(node);
    }

    /// <summary>
    /// Unlinks and returns the Least Recently Used node (immediately preceding the dummy tail).
    /// </summary>
    private DNode PopTail()
    {
        DNode lru = _tail.Prev!;
        RemoveNode(lru);
        return lru;
    }
}
```

---

### 6. Senior Pitfalls & Defensive Traps

1. **The Premature Eviction Bug on Update:**
   - *Trap:* Checking `if (_nodeMap.Count >= _capacity)` before checking `if (_nodeMap.ContainsKey(key))`.
   - *Consequence:* If the cache is full and `Put` is called with an existing key, the cache evicts the LRU item first, then updates the existing key, shrinking the cache count to `capacity - 1` and destroying valid data.
   - *Defense:* Always attempt `TryGetValue` first. Only evaluate capacity on genuine inserts.
2. **Missing Key in DNode (Reverse Lookup Trap):**
   - *Trap:* Only storing `Value` inside `DNode`.
   - *Consequence:* When `PopTail()` executes, you have the node reference `lru`, but you cannot remove it from `_nodeMap` without scanning the entire dictionary ($O(N)$ eviction degradation).
   - *Defense:* Store `Key` directly inside the node so eviction is strictly $O(1)$.
3. **GC Allocation Churn in High-Throughput Scenarios:**
   - *Trap:* Allocating a `new DNode` on every single cache miss in a 100,000 req/sec microservice.
   - *Consequence:* High Gen-0 garbage collection pressure and latency spikes.
   - *Defense:* Implement an object pool or node recycler: when evicting `_tail.prev`, reuse the detached node object for the incoming key-value pair instead of allocating a fresh instance.
4. **Thread Safety & Lock Convoy:**
   - *Trap:* Marking methods with naive `lock(_syncLock)` in multi-threaded environments. Because `Get()` also mutates list pointers (`MoveToHead`), reads require exclusive write access, causing severe reader lock contention.
   - *Defense:* Use Striped LRU (sharding keys across $K$ independent LRU buckets via hash partitioning) or an asynchronous lock-free eviction ring (like Java's Caffeine Cache).

---


## 117. Insert Delete GetRandom O(1) (LeetCode #380)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#hash-map` `#dynamic-array` `#design` `#swap-and-pop` `#randomized-algorithms` |
| **LeetCode Link** | [Insert Delete GetRandom O(1)](https://leetcode.com/problems/insert-delete-getrandom-o1/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Implement the `RandomizedSet` class:
  - `RandomizedSet()`: Initializes the `RandomizedSet` object.
  - `bool Insert(int val)`: Inserts an item `val` into the set if not present. Returns `true` if the item was not present, `false` otherwise.
  - `bool Remove(int val)`: Removes an item `val` from the set if present. Returns `true` if the item was present, `false` otherwise.
  - `int GetRandom()`: Returns a random element from the current set of elements. Each element must have the **same probability** ($1/N$) of being returned.
- **Assumptions & Contracts:**
  - You must implement the functions of the class such that each function works in **average $O(1)$ time complexity**.
  - `GetRandom` will only be called when there is at least one element in the data structure.
- **Key Constraints:**
  - $-2^{31} \le val \le 2^{31} - 1$
  - At most $2 \times 10^5$ calls will be made to `Insert`, `Remove`, and `GetRandom`.
- **Senior Edge Cases to Defend:**
  - Removing an element when it is the **only element** in the set.
  - Removing an element that is already located at the **very last index** of the underlying array (self-overwrite risk in swap logic).
  - Rapid alternating insertions and removals of the same value.
  - Uniformity of random sampling: over $10^6$ trials, variance must strictly follow uniform distribution $\chi^2$ tests.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Combine dense contiguous storage (`List<int>`) with an associative index lookup (`Dictionary<int, int>`). A dense array enables $O(1)$ random indexing via `Random.Next(count)`. A hash map provides $O(1)$ membership checks and index resolution. Deletion in $O(1)$ from an array without shifting is achieved via the **Swap-and-Pop** idiom.
- **Sample Execution Trace:**
  - `RandomizedSet randomizedSet = new RandomizedSet();`
  - `randomizedSet.Insert(1);   // Inserts 1. Returns true. Set: [1]`
  - `randomizedSet.Remove(2);   // 2 does not exist. Returns false.`
  - `randomizedSet.Insert(2);   // Inserts 2. Returns true. Set: [1, 2]`
  - `randomizedSet.GetRandom(); // Returns either 1 or 2 with equal 50% probability.`
  - `randomizedSet.Remove(1);   // Removes 1. Returns true. Set: [2]`
  - `randomizedSet.Insert(2);   // 2 already exists. Returns false.`
  - `randomizedSet.GetRandom(); // Always returns 2.`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Think of an auditorium where seats are numbered $0$ to $N - 1$. To pick a random attendee fairly, you draw a seat number between $0$ and $N - 1$ from a lottery machine—instant $O(1)$ random access. But if attendee in seat 3 leaves, you cannot leave an empty gap (or your lottery draw might pick an empty seat), nor can you ask everyone from seat 4 to $N - 1$ to slide over one seat (an expensive $O(N)$ shift).
The solution: Take the person sitting in the very last seat ($N - 1$), move them into seat 3, update the seating chart, and close the last seat. The auditorium remains 100% dense with zero gaps in $O(1)$ time!

#### 3.2 The Naive Bottleneck & Redundant Computation
- **Hash Set Alone:** A standard hash table does not support $O(1)$ random access. Because hash table buckets are sparse and contain empty slots, picking an element requires either generating random keys repeatedly (high collision loop) or converting buckets to an array ($O(N)$).
- **Dynamic Array Alone:** While `list[Random.Next(n)]` is $O(1)$, searching for `val` to remove takes $O(N)$, and calling `list.RemoveAt(index)` shifts subsequent elements, costing $O(N)$ time.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Relaxation of Ordering:** A set does not require ordered elements. We can freely reorder array elements during deletions.
- **The Swap-and-Pop Invariant:**
  To remove `val` residing at index `idx` in array `A` of length $L$:
  1. Let `lastVal = A[L - 1]`.
  2. Overwrite `A[idx] = lastVal`.
  3. Update map: `valToIndex[lastVal] = idx`.
  4. Truncate array: `A.RemoveAt(L - 1)`.
  5. Evict deleted key: `valToIndex.Remove(val)`.
- **Uniform Density Invariant:** The array size is always exactly equal to `valToIndex.Count`, with elements occupying continuous indices $[0 \dots N - 1]$. Thus, every integer $r \in [0, N - 1]$ maps to a valid, distinct element with probability $P = \frac{1}{N}$.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
SWAP-AND-POP DELETION PATTERN (Removing val = 10, located at idx = 1):

Step 0: Initial State
  Index:        0       1       2       3 (Last)
  Array:     [ 42  ,   10  ,   99  ,   77   ]
  Dict:      { 42:0,  10:1 ,  99:2 ,  77:3  }

Step 1: Move last element (77) into target index (1)
  Index:        0       1       2       3 (Last)
  Array:     [ 42  ,   77  ,   99  ,   77   ]
  Dict:      Update 77:1. Remove 10.

Step 2: Truncate / Pop Tail (O(1))
  Index:        0       1       2
  Array:     [ 42  ,   77  ,   99  ]
  Dict:      { 42:0,  77:1 ,  99:2 }
```

#### 3.5 State Transition Triggers & Decision Gates
1. **`Insert(val)` Gate:**
   - If `_valToIndex.ContainsKey(val)`: return `false`.
   - Else: append `val` to `_values`, set `_valToIndex[val] = _values.Count - 1`, return `true`.
2. **`Remove(val)` Gate:**
   - If `!_valToIndex.TryGetValue(val, out int targetIdx)`: return `false`.
   - Let `lastIdx = _values.Count - 1`.
   - Let `lastVal = _values[lastIdx]`.
   - Move `lastVal` into `targetIdx`: `_values[targetIdx] = lastVal; _valToIndex[lastVal] = targetIdx;`.
   - Remove tail: `_values.RemoveAt(lastIdx); _valToIndex.Remove(val);`.
   - Return `true`.
3. **`GetRandom()` Gate:**
   - Generate `randomIndex = Random.Shared.Next(_values.Count)`.
   - Return `_values[randomIndex]`.

#### 3.6 Concrete Step-by-Step State Trace
Trace operations: `Insert(10)`, `Insert(20)`, `Insert(30)`, `Remove(20)`, `GetRandom()`

| Step | Operation | Target Val | Found Index | Last Index / Val | Array State After Step | Map State After Step | Result |
| :---: | :--- | :---: | :---: | :---: | :--- | :--- | :---: |
| 1 | `Insert(10)` | 10 | — | — | `[10]` | `{10: 0}` | `true` |
| 2 | `Insert(20)` | 20 | — | — | `[10, 20]` | `{10: 0, 20: 1}` | `true` |
| 3 | `Insert(30)` | 30 | — | — | `[10, 20, 30]` | `{10: 0, 20: 1, 30: 2}` | `true` |
| 4 | `Remove(20)` | 20 | `1` | `lastIdx=2, val=30` | `[10, 30]` | `{10: 0, 30: 1}` | `true` |
| 5 | `GetRandom()` | — | — | — | `[10, 30]` | `{10: 0, 30: 1}` | `10` or `30` |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1: List + Dictionary + Swap-and-Pop (Optimal & Canonical):**
  - Guaranteed amortized $O(1)$ for `Insert` and `Remove`.
  - Guaranteed strict $O(1)$ for `GetRandom`.
  - Contiguous memory layout provides exceptional CPU cache hits during `GetRandom`.

#### 4.2 Step-by-Step Natural Progression Flow
1. **Initialize Collections:** A `List<int>` for storage and a `Dictionary<int, int>` for tracking `{value -> listIndex}`.
2. **Safe Insert:** Check dictionary first. Append to list and store index.
3. **Defensive Remove:** If element exists, fetch its index. Read the last element of the list. Copy last element to target index, update dictionary for the swapped element, pop tail, and remove deleted value from dictionary.
4. **Uniform Random:** Use thread-safe modern PRNG `Random.Shared.Next(_values.Count)`.

#### 4.3 Alternative Approaches Analysis
- **SkipList or Balanced BST (Order Statistic Tree):**
  - Supports search, insertion, and rank-based random access in $O(\log N)$ time. While theoretically interesting, $O(\log N)$ fails the problem's strict $O(1)$ contract.
- **Hash Table with Linked Buckets:**
  - Can track elements, but random sampling cannot be done in $O(1)$ without a dense indexing array.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: List + Hash Map (Swap-and-Pop) | Approach 2: Order Statistic Tree (BST) | Approach 3: Naive Array |
| :--- | :--- | :--- | :--- |
| **Time: Insert** | $O(1)$ amortized | $O(\log N)$ | $O(1)$ amortized |
| **Time: Remove** | $O(1)$ amortized | $O(\log N)$ | $O(N)$ (shift penalty) |
| **Time: GetRandom** | $O(1)$ strict | $O(\log N)$ | $O(1)$ strict |
| **Auxiliary Space** | $O(N)$ | $O(N)$ (tree nodes) | $O(N)$ |
| **Output Space** | $O(1)$ | $O(1)$ | $O(1)$ |
| **Cache Locality** | Optimal (dense array) | Poor (pointer chasing) | Optimal |
| **Random Quality** | Strictly Uniform ($1/N$) | Strictly Uniform ($1/N$) | Strictly Uniform ($1/N$) |

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: Problem #117 - Insert Delete GetRandom O(1)
 * ============================================================================
 * Core Pattern      : Dense Dynamic Array + Hash Map with Swap-and-Pop Eviction
 * Time Complexity   : O(1) average time for Insert, Remove, and GetRandom
 * Space Complexity  : O(N) auxiliary space where N is the number of active elements
 * Selection Rule    : Combine List<int> for O(1) random indexing with Dictionary
 *                     for O(1) location lookup; Swap-and-Pop eliminates shift costs.
 * Defensive Traps   : 1. When removing the last element, the swap-and-pop logic
 *                        must not resurrect the deleted element in the map.
 *                     2. Use Random.Shared instead of allocating 'new Random()'
 *                        per call to prevent seed collisions and thread contention.
 * ============================================================================
 */

namespace SeniorDSA.SystemDesign;

public class RandomizedSet
{
    private readonly List<int> _values;
    private readonly Dictionary<int, int> _valToIndex;

    /// <summary>
    /// Initializes an empty RandomizedSet with default capacity.
    /// </summary>
    public RandomizedSet()
    {
        _values = new List<int>();
        _valToIndex = new Dictionary<int, int>();
    }

    /// <summary>
    /// Inserts a value into the set if not already present.
    /// </summary>
    /// <param name="val">The integer value to insert.</param>
    /// <returns>True if the item was newly inserted; false if it was already present.</returns>
    public bool Insert(int val)
    {
        if (_valToIndex.ContainsKey(val))
        {
            return false;
        }

        // Dense storage: append to end and record index in dictionary
        _valToIndex[val] = _values.Count;
        _values.Add(val);
        return true;
    }

    /// <summary>
    /// Removes a value from the set if present in average O(1) time using Swap-and-Pop.
    /// </summary>
    /// <param name="val">The integer value to remove.</param>
    /// <returns>True if the item was found and removed; false otherwise.</returns>
    public bool Remove(int val)
    {
        if (!_valToIndex.TryGetValue(val, out int indexToRemove))
        {
            return false;
        }

        int lastIndex = _values.Count - 1;
        int lastVal = _values[lastIndex];

        // Critical Swap-and-Pop: Move the last element into the position of the deleted element
        _values[indexToRemove] = lastVal;
        _valToIndex[lastVal] = indexToRemove;

        // Truncate the list and remove the targeted entry from dictionary
        _values.RemoveAt(lastIndex);
        _valToIndex.Remove(val);

        return true;
    }

    /// <summary>
    /// Returns a random element from the set where each element has equal 1/N probability.
    /// </summary>
    /// <returns>A uniformly random integer from the active set.</returns>
    public int GetRandom()
    {
        if (_values.Count == 0)
        {
            throw new InvalidOperationException("Cannot sample from an empty RandomizedSet.");
        }

        // Random.Shared (.NET 6+) is thread-safe and avoids allocating PRNG instances
        int randomIndex = Random.Shared.Next(_values.Count);
        return _values[randomIndex];
    }
}
```

---

### 6. Senior Pitfalls & Defensive Traps

1. **The Self-Overwrite Trap on Tail Removal:**
   - *Trap:* If `val` is already the last element in `_values`, `indexToRemove == lastIndex`. If you execute `_valToIndex[lastVal] = indexToRemove` *after* deleting `val`, or if map deletion happens in the wrong sequence, you can accidentally re-add the deleted element back into the dictionary.
   - *Defense:* Notice the order of execution in the code: update `_valToIndex[lastVal] = indexToRemove`, then call `_valToIndex.Remove(val)`. When `lastVal == val`, the subsequent `.Remove(val)` cleanly evicts it!
2. **The `new Random()` Allocation Trap:**
   - *Trap:* Instantiating `new Random().Next(...)` inside `GetRandom()`.
   - *Consequence:* In tight loops, `new Random()` instances share the exact same system clock tick seed, producing identical "random" sequences, while creating immense GC allocation churn.
   - *Defense:* Always use `Random.Shared` in modern .NET or a thread-local static `Random` instance.
3. **List Resizing Latency Spikes:**
   - *Trap:* Unbounded insertions triggering underlying dynamic array doubling ($N \to 2N$) with array copy pauses.
   - *Defense:* If the expected working set size $N$ is known, initialize `List<int>(capacity)` and `Dictionary<int, int>(capacity)` upfront.
4. **Modulo Bias in Custom RNG:**
   - *Trap:* Attempting custom math like `rand() % count`. If `rand()` produces values across a range not evenly divisible by `count`, lower indices receive slightly higher probabilities.
   - *Defense:* Rely on `Random.Next(exclusiveUpperBound)` which implements rejection sampling to guarantee true uniform distribution.

---


## 118. LFU Cache (LeetCode #460)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🔴 Hard |
| **Priority** | 💡 Advanced |
| **Pattern Tags** | `#hash-map` `#doubly-linked-list` `#design` `#frequency-buckets` `#lfu-eviction` |
| **LeetCode Link** | [LFU Cache](https://leetcode.com/problems/lfu-cache/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Design and implement a data structure for a Least Frequently Used (LFU) cache. Implement `LFUCache`:
  - `LFUCache(int capacity)`: Initializes the object with the capacity of the data structure.
  - `int Get(int key)`: Gets the value of the `key` if the `key` exists in the cache. Otherwise, returns `-1`.
  - `void Put(int key, int value)`: Update the value of the `key` if present, or inserts the `key` if not already present. When the cache reaches its `capacity`, it should invalidate and remove the **least frequently used** key before inserting a new item. For this problem, when there is a **tie** (i.e., two or more keys with the same frequency), the **least recently used** key should be evicted.
- **Assumptions & Contracts:**
  - Both `Get` and `Put` functions must run in **$O(1)$ average time complexity**.
  - Access count increments by 1 on both `Get(key)` hits and `Put(key, value)` updates.
- **Key Constraints:**
  - $0 \le \text{capacity} \le 10^4$
  - $0 \le \text{key} \le 10^5$
  - $0 \le \text{value} \le 10^9$
  - At most $2 \times 10^5$ calls will be made to `Get` and `Put`.
- **Senior Edge Cases to Defend:**
  - `capacity = 0`: Must immediately reject any `Put` and return `-1` on any `Get` without throwing exceptions.
  - Frequency promotion of the sole node at `minFrequency`: When node frequency increases from $F$ to $F+1$, and bucket $F$ becomes empty, `minFrequency` must automatically advance to $F+1$.
  - Tie-breaking: Multiple keys sharing the same frequency must be evicted in strict LRU order.
  - Updating existing keys: Updates increment frequency, but do **not** increase cache occupancy count.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Two-dimensional indexing. Frequencies are partitioned into discrete frequency buckets, where each bucket is an independent Doubly Linked List ordered by recency. A hash map `keyToNode` provides $O(1)$ node access. A second hash map `freqToList` maps frequency values to their respective doubly linked list. A scalar cursor `minFrequency` tracks the current lowest frequency.
- **Sample Execution Trace:**
  - `LFUCache lfu = new LFUCache(2);`
  - `lfu.Put(1, 1);   // cache=[1,_], cnt(1)=1`
  - `lfu.Put(2, 2);   // cache=[2,1], cnt(2)=1, cnt(1)=1`
  - `lfu.Get(1);      // return 1, cnt(1)=2, cache=[1,2]`
  - `lfu.Put(3, 3);   // 2 and 3 both have cnt=1, but 2 is LRU. Evicts 2. cache=[3,1]`
  - `lfu.Get(2);      // return -1 (not found)`
  - `lfu.Get(3);      // return 3, cnt(3)=2, cache=[3,1]`
  - `lfu.Put(4, 4);   // both 1 and 3 have cnt=2. 1 was accessed before 3 (1 is LRU). Evicts 1.`
  - `lfu.Get(1);      // return -1`
  - `lfu.Get(3);      // return 3`
  - `lfu.Get(4);      // return 4`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine an academic scholarship program. Scholars are grouped into tiers based strictly on how many papers they published (Frequency: Tier 1, Tier 2, Tier 3...). Within each tier, scholars sit in a row ordered by how recently they gave a lecture (Doubly Linked List).
Whenever a scholar publishes a paper (`Get` or `Put`), they stand up, leave their current tier room, and walk up to the next higher tier room, sitting right at the front of the stage.
When funding runs out and someone must be expelled, the dean doesn't search the entire university. The dean goes straight to the lowest non-empty tier room (`minFrequency`) and expels the scholar sitting in the very back row (`tail.prev`).

#### 3.2 The Naive Bottleneck & Redundant Computation
- A Min-Heap of `(frequency, timestamp)` requires $O(\log N)$ on every `Get` and `Put`. For $2 \times 10^5$ operations, this costs tens of millions of heap comparator swaps and fails the strict $O(1)$ requirement.
- Scanning nodes linearly to find $\min(\text{freq})$ costs $O(N)$ time per eviction.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Discrete Step Invariant:** Frequencies only ever increment by exactly $+1$ upon access! They never jump by $+2$ or arbitrary values.
- Because a node only ever moves from bucket $F$ to bucket $F + 1$, we can detach it from DLL $F$ and attach it to DLL $F + 1$ in guaranteed $O(1)$ time.
- **The `minFrequency` Tracking Invariant:**
  1. When a brand-new key is inserted, its frequency is always $1$. Therefore, `minFrequency` is guaranteed to become $1$.
  2. When an existing key is accessed, its frequency increases from $F$ to $F + 1$. If $F == \text{minFrequency}$ and DLL $F$ has become completely empty after this node's departure, then no nodes exist in the cache with frequency $F$ anymore. Thus, `minFrequency` must advance: $\text{minFrequency} = F + 1$.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
[ _keyToNode Map ]
  Key: 1 ──> Node A (Freq: 2)
  Key: 2 ──> Node B (Freq: 1)
  Key: 3 ──> Node C (Freq: 2)

[ _freqToList Map ]
  Freq 1:  [Head] <──> [ Node B (LRU & MRU) ] <──> [Tail]
                          ▲
                          └── minFrequency points here!

  Freq 2:  [Head] <──> [ Node C (MRU) ] <──> [ Node A (LRU) ] <──> [Tail]
                          ▲                     ▲
                       Most Recent           Oldest in Freq 2
```

- **Invariant 1:** Every node resides in exactly one DLL inside `_freqToList[node.Frequency]`.
- **Invariant 2:** Inside any DLL, the node closest to `head` is the most recently accessed of that frequency; the node closest to `tail` is the least recently accessed.
- **Invariant 3:** `minFrequency` strictly matches the lowest key in `_freqToList` containing a non-empty DLL.

#### 3.5 State Transition Triggers & Decision Gates
1. **`IncrementFrequency(node)`:**
   - Let $F = \text{node.Frequency}$.
   - Remove `node` from `_freqToList[F]`.
   - If `_freqToList[F].Count == 0` and $F == \text{minFrequency}$:
     - `minFrequency++`.
   - Increment `node.Frequency++`.
   - Add `node` to head of `_freqToList[node.Frequency]`.
2. **`Get(key)`:**
   - If not found in `_keyToNode`: return `-1`.
   - Fetch `node`, execute `IncrementFrequency(node)`, return `node.Value`.
3. **`Put(key, value)`:**
   - If `_capacity <= 0`: return immediately.
   - If key exists in `_keyToNode`:
     - Update `node.Value = value`.
     - Execute `IncrementFrequency(node)`.
     - Return.
   - If `_keyToNode.Count == _capacity`:
     - Evict LRU from `_freqToList[minFrequency]`: `lru = list.PopTail()`.
     - Remove `_keyToNode.Remove(lru.Key)`.
   - Allocate `newNode = new LFUNode(key, value)`.
   - `_keyToNode[key] = newNode`.
   - `minFrequency = 1`.
   - Add `newNode` to head of `_freqToList[1]`.

#### 3.6 Concrete Step-by-Step State Trace
Trace with `capacity = 2`:
Operations: `Put(1, 10)`, `Put(2, 20)`, `Get(1)`, `Put(3, 30)`

| Step | Operation | Key / Val | Action / Logic | `minFreq` | Freq 1 List | Freq 2 List | Evicted |
| :---: | :--- | :---: | :--- | :---: | :---: | :---: | :---: |
| 1 | `Put(1, 10)` | 1 / 10 | Insert new node at Freq 1 | 1 | `[1]` | `[]` | — |
| 2 | `Put(2, 20)` | 2 / 20 | Insert new node at Freq 1 | 1 | `[2, 1]` | `[]` | — |
| 3 | `Get(1)` | 1 | Promote node 1 from Freq 1 to Freq 2 | 1 | `[2]` | `[1]` | — |
| 4 | `Put(3, 30)` | 3 / 30 | Full! Evict from `minFreq` (1) $\to$ evicts 2. Reset `minFreq=1`, add 3. | 1 | `[3]` | `[1]` | `2` |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1: Frequency Buckets (Hash Map of Doubly Linked Lists):**
  - Only design capable of meeting the strict $O(1)$ amortized/worst-case constraint.
  - Zero search penalty; pointer unlinking is exact $O(1)$.
- **Approach 2: Min-Heap / Balanced Tree with Compound Key `(Frequency, GlobalTick)`:**
  - Conceptually straightforward, but runs in $O(\log N)$ per operation. Fails interviews when strict $O(1)$ is required.

#### 4.2 Step-by-Step Natural Progression Flow
1. **Define `LFUNode`:** Holds `Key`, `Value`, `Frequency`, `Prev`, `Next`.
2. **Define `DoublyLinkedList`:** Encapsulates `Head` and `Tail` sentinels, `Count`, `AddToHead(node)`, `RemoveNode(node)`, and `PopTail()`.
3. **Cache State Setup:**
   - `_keyToNode`: `Dictionary<int, LFUNode>`
   - `_freqToList`: `Dictionary<int, DoublyLinkedList>`
   - `_minFrequency`: integer tracking minimum frequency.
4. **Promotion Routine:** Implement `IncrementFrequency(node)` cleanly to handle list migration and `minFrequency` auto-increment.

#### 4.3 Alternative Approaches Analysis
- **Balanced Binary Search Tree (`SortedSet<LFUNode>`):**
  - Requires defining a custom comparer sorting by `(Frequency, AccessTime)`.
  - Updating frequency requires removing from set, incrementing, and re-inserting $\implies O(\log N)$ time.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: Hash Map of Doubly Linked Lists | Approach 2: Priority Queue / Min-Heap | Approach 3: SortedSet / Balanced BST |
| :--- | :--- | :--- | :--- |
| **Get Time** | $O(1)$ | $O(\log N)$ (or $O(1)$ read with lazy sync) | $O(\log N)$ |
| **Put Time** | $O(1)$ | $O(\log N)$ | $O(\log N)$ |
| **Auxiliary Space** | $O(\text{Capacity})$ | $O(\text{Capacity})$ | $O(\text{Capacity})$ |
| **Output Space** | $O(1)$ | $O(1)$ | $O(1)$ |
| **Cache Locality** | Moderate (multiple pointer links) | Optimal (array-backed binary heap) | Moderate (tree node pointers) |
| **Implementation Complexity**| High (requires nested linked list math) | Moderate | Moderate |

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: Problem #118 - LFU Cache
 * ============================================================================
 * Core Pattern      : Dual Hash Map + Frequency-Partitioned Doubly Linked Lists
 * Time Complexity   : O(1) strictly for both Get() and Put() operations
 * Space Complexity  : O(Capacity) auxiliary space for nodes and frequency buckets
 * Selection Rule    : Frequency-partitioned DLLs enable O(1) bucket promotion
 *                     and exact O(1) LRU tie-breaking at minFrequency.
 * Defensive Traps   : 1. Guard against capacity = 0 immediately.
 *                     2. Increment minFrequency only when the vacated bucket
 *                        equals minFrequency AND is now completely empty.
 *                     3. Reset minFrequency to 1 on brand new key insertions.
 * ============================================================================
 */

namespace SeniorDSA.SystemDesign;

public class LFUCache
{
    private sealed class LFUNode
    {
        public int Key { get; }
        public int Value { get; set; }
        public int Frequency { get; set; }
        public LFUNode? Prev { get; set; }
        public LFUNode? Next { get; set; }

        public LFUNode(int key, int value)
        {
            Key = key;
            Value = value;
            Frequency = 1;
        }
    }

    private sealed class DoublyLinkedList
    {
        private readonly LFUNode _head;
        private readonly LFUNode _tail;
        public int Count { get; private set; }

        public DoublyLinkedList()
        {
            _head = new LFUNode(0, 0);
            _tail = new LFUNode(0, 0);
            _head.Next = _tail;
            _tail.Prev = _head;
            Count = 0;
        }

        public void AddToHead(LFUNode node)
        {
            node.Prev = _head;
            node.Next = _head.Next;
            _head.Next!.Prev = node;
            _head.Next = node;
            Count++;
        }

        public void RemoveNode(LFUNode node)
        {
            node.Prev!.Next = node.Next;
            node.Next!.Prev = node.Prev;
            node.Prev = null;
            node.Next = null;
            Count--;
        }

        public LFUNode PopTail()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("Cannot pop from an empty list.");
            }

            LFUNode lru = _tail.Prev!;
            RemoveNode(lru);
            return lru;
        }
    }

    private readonly int _capacity;
    private int _minFrequency;
    private readonly Dictionary<int, LFUNode> _keyToNode;
    private readonly Dictionary<int, DoublyLinkedList> _freqToList;

    public LFUCache(int capacity)
    {
        _capacity = capacity;
        _minFrequency = 0;
        _keyToNode = new Dictionary<int, LFUNode>(capacity > 0 ? capacity : 1);
        _freqToList = new Dictionary<int, DoublyLinkedList>();
    }

    public int Get(int key)
    {
        if (_capacity <= 0 || !_keyToNode.TryGetValue(key, out var node))
        {
            return -1;
        }

        IncrementFrequency(node);
        return node.Value;
    }

    public void Put(int key, int value)
    {
        if (_capacity <= 0)
        {
            return;
        }

        // Case 1: Key already exists -> Update value and promote frequency
        if (_keyToNode.TryGetValue(key, out var existingNode))
        {
            existingNode.Value = value;
            IncrementFrequency(existingNode);
            return;
        }

        // Case 2: Capacity reached -> Evict LRU from the minFrequency bucket
        if (_keyToNode.Count >= _capacity)
        {
            var minList = _freqToList[_minFrequency];
            LFUNode evictedNode = minList.PopTail();
            _keyToNode.Remove(evictedNode.Key);
        }

        // Case 3: Insert new item with frequency = 1
        var newNode = new LFUNode(key, value);
        _keyToNode[key] = newNode;
        _minFrequency = 1;

        if (!_freqToList.TryGetValue(1, out var list1))
        {
            list1 = new DoublyLinkedList();
            _freqToList[1] = list1;
        }

        list1.AddToHead(newNode);
    }

    /// <summary>
    /// Promotes a node from its current frequency list to frequency + 1 in O(1) time.
    /// </summary>
    private void IncrementFrequency(LFUNode node)
    {
        int oldFreq = node.Frequency;
        var oldList = _freqToList[oldFreq];
        oldList.RemoveNode(node);

        // If the vacated list was the minFrequency list and is now empty, advance minFrequency
        if (oldFreq == _minFrequency && oldList.Count == 0)
        {
            _minFrequency++;
        }

        node.Frequency++;
        int newFreq = node.Frequency;

        if (!_freqToList.TryGetValue(newFreq, out var newList))
        {
            newList = new DoublyLinkedList();
            _freqToList[newFreq] = newList;
        }

        newList.AddToHead(node);
    }
}
```

---

### 6. Senior Pitfalls & Defensive Traps

1. **The `capacity = 0` Crash:**
   - *Trap:* Failing to check `if (_capacity <= 0)` at the beginning of `Put`.
   - *Consequence:* `_keyToNode.Count >= _capacity` evaluates to `0 >= 0` (true), causing the code to attempt `_freqToList[_minFrequency].PopTail()` when `_minFrequency` is 0 and no list exists, throwing a `KeyNotFoundException` or `NullReferenceException`.
   - *Defense:* Guard clause at top of constructor or inside `Put`/`Get`.
2. **Incorrect `minFrequency` Advancement on New Put:**
   - *Trap:* Failing to reset `_minFrequency = 1` when inserting a brand-new key.
   - *Consequence:* If previous accesses elevated `minFrequency` to 5, and a new key with frequency 1 is inserted, eviction will still target bucket 5, completely violating the LFU invariant!
   - *Defense:* Explicitly set `_minFrequency = 1` on every new key insertion.
3. **Ghost Buckets Memory Overhead:**
   - *Trap:* Repeatedly instantiating `DoublyLinkedList` buckets as frequencies climb to high values (e.g., thousands).
   - *Defense:* Reusing empty `DoublyLinkedList` instances or cleaning up empty lists from `_freqToList` if memory footprint is constrained.
4. **Frequency Starvation (LFU Vulnerability):**
   - *Trap:* In production systems, keys accessed 1,000 times during a morning spike become permanently immune to eviction in the afternoon even if they are never accessed again.
   - *Defense:* Real-world caches (like Redis LFU) implement **frequency decay** (halving frequencies periodically or using logarithmic probabilistic counters).

---


## 119. Logger Rate Limiter (LeetCode #359)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟢 Easy |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#hash-map` `#sliding-window` `#design` `#rate-limiter` `#memory-leak-prevention` |
| **LeetCode Link** | [Logger Rate Limiter](https://leetcode.com/problems/logger-rate-limiter/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Design a logger system that receives a stream of messages along with their timestamps. Each unique message should only be printed at most once every 10 seconds (i.e. a message printed at timestamp `t` will prevent the identical message from being printed until timestamp `t + 10`). All timestamps are in seconds granularity.
- **Assumptions & Contracts:**
  - Implement the `Logger` class:
    - `Logger()`: Initializes the logger object.
    - `bool ShouldPrintMessage(int timestamp, string message)`: Returns `true` if the message should be printed in the given timestamp, otherwise returns `false`.
  - Timestamps are delivered in **non-decreasing (monotonic) order**.
  - Several messages may arrive at the exact same timestamp.
- **Key Constraints:**
  - $0 \le \text{timestamp} \le 10^9$
  - The message consists of alphanumeric characters and spaces.
  - At most $10^4$ calls will be made to `ShouldPrintMessage`.
- **Senior Edge Cases to Defend:**
  - **Memory Leak in Long-Running Daemons:** If an unbounded hash map is used, millions of unique error messages will bloat memory until `OutOfMemoryException` occurs.
  - Same timestamp collision: Multiple identical messages arriving at $t = 1$. First returns `true`, subsequent return `false`.
  - Exact window boundary: If printed at $t = 1$, arriving at $t = 11$ must return `true` ($11 - 1 = 10 \ge 10$). Arriving at $t = 10$ must return `false` ($10 - 1 = 9 < 10$).
  - Large time leaps: Timestamps jumping from $t = 10$ to $t = 1,000,000$.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Sliding 10-Second Deduplication Window. Standard LeetCode solutions use an unbounded `Dictionary<string, int>`. Senior systems engineers identify the fatal production memory leak and implement a sliding TTL eviction queue or a dual-bucket rotating time window.
- **Sample Execution Trace:**
  - `Logger logger = new Logger();`
  - `logger.ShouldPrintMessage(1, "foo");  // returns true. Next allowed at t >= 11`
  - `logger.ShouldPrintMessage(2, "bar");  // returns true. Next allowed at t >= 12`
  - `logger.ShouldPrintMessage(3, "foo");  // returns false (3 < 11)`
  - `logger.ShouldPrintMessage(8, "bar");  // returns false (8 < 12)`
  - `logger.ShouldPrintMessage(10, "foo"); // returns false (10 < 11)`
  - `logger.ShouldPrintMessage(11, "foo"); // returns true (11 >= 11). Next allowed at t >= 21`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine a nightclub bouncer with a 10-second hand stamp. When guest "foo" enters at 12:00:01, they receive a stamp marked "Valid from 12:00:11". If "foo" tries to re-enter at 12:00:05, the bouncer checks the stamp: current time < stamp time $\implies$ denied.
However, if the bouncer writes every guest's name into a permanent paper logbook, after a month the logbook fills an entire warehouse. A smart bouncer uses a 10-second conveyor belt: records older than 10 seconds fall off the end into the shredder automatically.

#### 3.2 The Naive Bottleneck & Redundant Computation
```csharp
// The naive junior solution:
Dictionary<string, int> _lastPrinted = new();
public bool ShouldPrintMessage(int timestamp, string message) {
    if (_lastPrinted.TryGetValue(message, out int last) && timestamp - last < 10)
        return false;
    _lastPrinted[message] = timestamp;
    return true;
}
```
- **The Fatal Production Flaw:** Auxiliary space is $O(U)$ where $U$ is the number of *distinct* messages seen since service startup. In production logging microservices processing billions of unique log events (containing request IDs, customer IDs, stack traces), memory leaks linearly over time.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Monotonic Timestamp Exploitation:** Because incoming timestamps $t_k$ are non-decreasing ($t_k \ge t_{k-1}$), messages that expired ($t_{\text{msg}} \le t_{\text{curr}} - 10$) will **never** be relevant again.
- **Sliding Window Expiration Invariant:**
  Maintain a FIFO queue of `(timestamp, message)` tuples paired with a `HashSet<string>` or a bounded dictionary.
  Whenever a new message arrives at $t_{\text{curr}}$:
  $$\text{While } \text{Queue.Peek().Timestamp} \le t_{\text{curr}} - 10 \implies \text{Dequeue and remove from set}$$
  The auxiliary space is strictly bounded to the throughput of distinct messages arriving within any moving 10-second interval:
  $$\text{Space} = O(\text{MaxDistinctMessagesIn10SecWindow}) \ll O(\text{TotalLifetimeMessages})$$

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
SLIDING TTL EVICTION ARCHITECTURE:
Current Timestamp: t = 15. Window threshold: t - 10 = 5.

Queue:
[ Front: (t=1, "alpha") ] ──> [ (t=4, "beta") ] ──> [ (t=11, "gamma") ] ──> [ Back: (t=14, "delta") ]
           │                         │
     Expired! (1 <= 5)         Expired! (4 <= 5)
           ▼                         ▼
   EVICT FROM QUEUE          EVICT FROM QUEUE
  & REMOVE FROM SET         & REMOVE FROM SET

Active Set after eviction: { "gamma", "delta" }
Space is strictly bounded by active messages within the last 10 seconds!
```

#### 3.5 State Transition Triggers & Decision Gates
1. **Eviction Phase:**
   - While `_slidingQueue.Count > 0` and `_slidingQueue.Peek().Timestamp <= timestamp - 10`:
     - Dequeue `(oldTime, oldMsg)`.
     - Remove `oldMsg` from `_activeSet` (only if no newer instance of `oldMsg` exists in window).
2. **Decision Phase:**
   - If `_activeSet.Contains(message)`: return `false`.
   - Else:
     - Enqueue `(timestamp, message)`.
     - Add `message` to `_activeSet`.
     - Return `true`.

#### 3.6 Concrete Step-by-Step State Trace
Trace with sliding queue approach:

| Step | Call `(t, msg)` | Expiry Threshold (`t - 10`) | Dequeued Items | Active Set State | Decision Gate | Return |
| :---: | :--- | :---: | :--- | :--- | :--- | :---: |
| 1 | `(1, "A")` | $-9$ | None | `{"A"}` | Not in set $\implies$ Add | `true` |
| 2 | `(2, "B")` | $-8$ | None | `{"A", "B"}` | Not in set $\implies$ Add | `true` |
| 3 | `(3, "A")` | $-7$ | None | `{"A", "B"}` | "A" in set $\implies$ Reject | `false` |
| 4 | `(11, "A")`| $1$ | Pop `(1, "A")` | `{"B"}` | "A" not in set $\implies$ Add | `true` |
| 5 | `(12, "B")`| $2$ | Pop `(2, "B")` | `{"A"}` | "B" not in set $\implies$ Add | `true` |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1: Standard Unbounded Hash Map (Interview Baseline):**
  - Instant $O(1)$ lookup and write.
  - Suitable only when total lifetime requests are small ($< 10^4$).
- **Approach 2: Sliding Queue + HashSet (Production Grade):**
  - Guarantees bounded memory footprint.
  - Amortized $O(1)$ time per message (each message enqueued and dequeued exactly once).

#### 4.2 Step-by-Step Natural Progression Flow
1. **Step 1:** Demonstrate standard map solution to interviewer in 2 minutes.
2. **Step 2:** Explicitly call out the production memory leak: *"In a service handling 50k logs/sec, this will trigger an OOM within hours."*
3. **Step 3:** Implement the bounded sliding window eviction queue.

#### 4.3 Alternative Approaches Analysis
- **Two-Bucket Rotating Time Windows:**
  - Maintain `currentBucket` (current 10-second block) and `previousBucket`.
  - When timestamp enters a new 10-second block, discard `previousBucket`, promote `currentBucket`, and allocate a new empty set.
  - Space is bounded by $2 \times$ max distinct messages per 10s block; zero queue eviction overhead.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: Unbounded Hash Map | Approach 2: Sliding Queue + HashSet | Approach 3: Two-Bucket Rotating Window |
| :--- | :--- | :--- | :--- |
| **Time Complexity** | $O(1)$ strict | $O(1)$ amortized | $O(1)$ strict |
| **Auxiliary Space** | $O(N)$ lifetime unbounded (**Leak!**) | $O(W)$ strictly bounded ($W \le 10s$) | $O(W)$ strictly bounded |
| **GC Pressure** | High long-term fragmentation | Low (short-lived tuples) | Minimal (periodic bucket drop) |
| **Concurrency Readiness** | Low | High | Optimal |

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: Problem #119 - Logger Rate Limiter
 * ============================================================================
 * Core Pattern      : Sliding Time-Window TTL Eviction / Bounded Ring Queue
 * Time Complexity   : O(1) amortized per message
 * Space Complexity  : O(W) auxiliary space where W is max distinct messages in 10s
 * Selection Rule    : Approach 2 is mandated in senior production systems to
 *                     guarantee zero memory leaks over infinite streaming lifecycles.
 * Defensive Traps   : Boundary check is strictly: timestamp - lastTime >= 10.
 *                     Clean expired entries before evaluating message membership.
 * ============================================================================
 */

namespace SeniorDSA.SystemDesign;

/// <summary>
/// LeetCode Canonical Baseline (Unbounded Hash Map).
/// Optimal for bounded problem constraints (calls <= 10,000).
/// </summary>
public class Logger
{
    private readonly Dictionary<string, int> _lastPrintedTimestamp;

    public Logger()
    {
        _lastPrintedTimestamp = new Dictionary<string, int>();
    }

    public bool ShouldPrintMessage(int timestamp, string message)
    {
        ArgumentNullException.ThrowIfNull(message);

        if (_lastPrintedTimestamp.TryGetValue(message, out int lastTime))
        {
            if (timestamp - lastTime < 10)
            {
                return false;
            }
        }

        _lastPrintedTimestamp[message] = timestamp;
        return true;
    }
}

/// <summary>
/// Production-Grade Memory-Safe Logger.
/// Prevents memory leaks by evicting messages older than 10 seconds via a sliding queue.
/// </summary>
public class ProductionTtlLogger
{
    private const int WindowSizeSeconds = 10;
    private readonly Queue<(int Timestamp, string Message)> _slidingQueue;
    private readonly Dictionary<string, int> _activeWindowMessages;

    public ProductionTtlLogger()
    {
        _slidingQueue = new Queue<(int, string)>();
        _activeWindowMessages = new Dictionary<string, int>();
    }

    /// <summary>
    /// Evaluates if the message should be printed with automatic TTL eviction of expired records.
    /// Runs in amortized O(1) time and strictly O(W) bounded memory.
    /// </summary>
    public bool ShouldPrintMessage(int timestamp, string message)
    {
        ArgumentNullException.ThrowIfNull(message);

        // Phase 1: Evict all expired log events that exited the 10-second sliding window
        while (_slidingQueue.Count > 0 && _slidingQueue.Peek().Timestamp <= timestamp - WindowSizeSeconds)
        {
            var expired = _slidingQueue.Dequeue();
            
            // Only remove from dictionary if the recorded timestamp matches the expired entry
            // (defends against multiple occurrences of the same message in the queue)
            if (_activeWindowMessages.TryGetValue(expired.Message, out int recordedTime) &&
                recordedTime == expired.Timestamp)
            {
                _activeWindowMessages.Remove(expired.Message);
            }
        }

        // Phase 2: Deduplication decision gate
        if (_activeWindowMessages.ContainsKey(message))
        {
            return false;
        }

        // Phase 3: Ingestion
        _slidingQueue.Enqueue((timestamp, message));
        _activeWindowMessages[message] = timestamp;
        return true;
    }
}
```

---

### 6. Senior Pitfalls & Defensive Traps

1. **The Strict vs Inclusive Inequality Trap:**
   - *Trap:* Writing `timestamp - lastTime > 10` instead of `>= 10`.
   - *Consequence:* A message printed at $t = 1$ will be rejected at $t = 11$, failing LeetCode and production specs.
2. **The Multiple Queue Entry De-Sync Trap:**
   - *Trap:* Using a simple `HashSet<string>` with the queue and blindly calling `_set.Remove(expired.Message)`.
   - *Consequence:* If message "foo" was enqueued at $t = 1$, and then enqueued again at $t = 11$, popping $(1, \text{"foo"})$ at $t = 12$ would delete "foo" from the set entirely, allowing an illegal print at $t = 13$!
   - *Defense:* Track timestamp in `_activeWindowMessages` dictionary. Only delete if `recordedTime == expired.Timestamp`.
3. **Clock Skew in Distributed Systems:**
   - *Trap:* Assuming timestamps are always monotonically increasing across distributed application nodes.
   - *Defense:* In real systems, use an atomic cluster-wide sequence ID or distributed lock service (Redis `SET NX EX 10`).

---


## 120. Flatten Nested List Iterator (LeetCode #341)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | 🔥 High |
| **Pattern Tags** | `#stack` `#iterator` `#design` `#lazy-evaluation` `#depth-first-search` |
| **LeetCode Link** | [Flatten Nested List Iterator](https://leetcode.com/problems/flatten-nested-list-iterator/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** You are given a nested list of integers `nestedList`. Each element is either an integer, or a list whose elements may also be integers or other lists. Implement an iterator to flatten it.
  - Implement `NestedIterator`:
    - `NestedIterator(List<NestedInteger> nestedList)`: Initializes the iterator with the nested list `nestedList`.
    - `int Next()`: Returns the next integer in the nested list.
    - `bool HasNext()`: Returns `true` if there are still some integers in the nested list, and `false` otherwise.
- **Assumptions & Contracts:**
  - `Next()` will only be called after `HasNext()` returns `true`.
  - Calling `HasNext()` multiple times consecutively without calling `Next()` must be **idempotent** (must not advance state or skip values).
- **NestedInteger Interface Contract:**
  ```csharp
  public interface NestedInteger {
      bool IsInteger();
      int GetInteger();
      IList<NestedInteger> GetList();
  }
  ```
- **Key Constraints:**
  - $1 \le \text{nestedList.length} \le 500$
  - Total number of integers across all nested levels $\le 10^5$.
  - Values fit inside standard 32-bit signed integers.
- **Senior Edge Cases to Defend:**
  - Arbitrarily nested empty lists: `[[], [[]], [[[]]]]`. Must return `HasNext() == false`.
  - Empty lists positioned between valid integers: `[[1, 1], [], [2], [[]], [3]]`.
  - Top-level empty list: `[]`.
  - Multiple consecutive calls to `HasNext()` before a single `Next()`.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Lazy Tree Traversal via Call-Stack Simulation. Instead of eagerly flattening all elements into a flat list in the constructor (which violates iterator semantics and wastes memory), maintain an active exploration stack of enumerators. `HasNext()` does the heavy lifting of unpacking nested list frames until the top of the stack points directly to a concrete integer.
- **Sample 1:**
  - **Input:** `nestedList = [[1, 1], 2, [1, 1]]`
  - **Output Stream:** `1, 1, 2, 1, 1`
- **Sample 2:**
  - **Input:** `nestedList = [1, [4, [6]]]`
  - **Output Stream:** `1, 4, 6`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine Russian Matryoshka dolls. Some dolls hold a prize (an integer), while others contain smaller dolls, and some are completely empty. An eager worker opens all 10,000 dolls in the warehouse today, takes all the candies out, and puts them in a box—wasting hours if the customer only wanted 1 candy!
A lazy master opens only the outer doll. If it contains a sub-doll, they place the remaining outer dolls on a shelf (call stack) and open the sub-doll. They only stop when their hand touches an actual candy.

#### 3.2 The Naive Bottleneck & Redundant Computation
- **The Eager Precomputation Anti-Pattern:**
  ```csharp
  // Junior implementation:
  List<int> flat = new();
  void Dfs(IList<NestedInteger> list) {
      foreach (var item in list) {
          if (item.IsInteger()) flat.Add(item.GetInteger());
          else Dfs(item.GetList());
      }
  }
  ```
  - **Violates Iterator Contract:** If `nestedList` represents an infinite stream or a massive 10GB dataset, the constructor blocks forever and throws `OutOfMemoryException`.
  - If the consumer only requests the first 3 elements via `Take(3)`, eager precomputation wastes $O(N)$ time and space processing millions of unwanted downstream nodes.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Stack of Enumerators:** Maintain a `Stack<IEnumerator<NestedInteger>>`.
- **The Invariant of `HasNext()`:**
  Upon completion of `HasNext()`, if it returns `true`, the top of the stack is **guaranteed** to be positioned on a `NestedInteger` where `IsInteger() == true`.
- **Idempotence Invariant:**
  Calling `HasNext()` when the stack top is already prepared at a valid integer does zero work and leaves the cursor intact.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
CALL STACK ARCHITECTURE:
Input: [ [1, 2], [3] ]

Stack State Evolution:
┌───────────────────────────────────────┐
│ Stack Top: Enumerator on [1, 2]       │ ──> Current item is '1' (Integer!)
├───────────────────────────────────────┤
│ Frame 0  : Enumerator on Outer List   │ ──> Waiting at index 0 ([1, 2])
└───────────────────────────────────────┘

When [1, 2] is exhausted:
  1. Pop Enumerator on [1, 2]
  2. Frame 0 advances to [3]
  3. Push Enumerator on [3]
```

#### 3.5 State Transition Triggers & Decision Gates
Inside `HasNext()`:
1. Loop while `_stack.Count > 0`:
   - Peek `top = _stack.Peek()`.
   - Call `top.MoveNext()`. If `false` (current level exhausted):
     - `_stack.Pop().Dispose()`.
     - Continue loop to parent frame.
   - If `true`:
     - Let `curr = top.Current`.
     - **Gate 1 (Integer Hit):** If `curr.IsInteger()`, cache `_peeked = curr` and return `true`.
     - **Gate 2 (Sub-List):** If `!curr.IsInteger()`, push `curr.GetList().GetEnumerator()` onto stack.
2. Return `false` (stack completely empty).

#### 3.6 Concrete Step-by-Step State Trace
Trace with `[ [], [3] ]`:

| Step | Method Called | Stack Depth | Action / Evaluation | Return |
| :---: | :--- | :---: | :--- | :---: |
| 1 | `HasNext()` | 1 | Outer list `MoveNext()` $\to$ reaches `[]`. Not integer. Push `[].GetEnumerator()`. | — |
| 2 | (Loop in `HasNext`) | 2 | Inner `[].MoveNext()` $\to$ `false` (empty!). Pop inner list. | — |
| 3 | (Loop in `HasNext`) | 1 | Outer list `MoveNext()` $\to$ reaches `[3]`. Push `[3].GetEnumerator()`. | — |
| 4 | (Loop in `HasNext`) | 2 | Inner `[3].MoveNext()` $\to$ `true`. Value is `3`. `IsInteger() == true`! | `true` |
| 5 | `Next()` | 2 | Return cached integer `3`. Clear cache. | `3` |
| 6 | `HasNext()` | 2 | Inner `[3].MoveNext()` $\to$ `false`. Pop inner list. Outer list exhausted. | `false` |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1: Lazy Stack of Enumerators (Senior Gold Standard):**
  - Strictly adheres to true iterator semantics.
  - $O(D)$ auxiliary space where $D$ is maximum nesting depth.
  - $O(1)$ amortized time per `Next()` call.
- **Approach 2: C# Compiler-Generated Generator (`yield return`):**
  - Beautiful, idiomatic, declarative C#. Internally, the Roslyn compiler builds the exact finite state machine stack for you!

#### 4.2 Step-by-Step Natural Progression Flow
1. Stack initial list enumerator in constructor.
2. Maintain `_peeked` field to make `HasNext()` strictly idempotent.
3. Keep drilling down inside `HasNext()` until an integer is reached or stack empties.

#### 4.3 Alternative Approaches Analysis
- **Approach 2 (Eager Recursive Flattening):**
  - Traverses the entire tree of lists in constructor and saves all integers into `List<int>`.
  - While simple ($O(1)$ `Next` and `HasNext`), it destroys iterator semantics, allocating $O(N)$ upfront memory even if the caller only needs the first element, and completely fails for infinite data streams.
- **Approach 3 (Compiler-Generated `IEnumerable` with `yield return`):**
  - Utilizes C#'s iterator state-machine transformation.
  - While elegant and streaming-compliant, implementing the LeetCode custom `NestedIterator` class interface directly requires wrapping the resulting `IEnumerator<int>`, which carries minor overhead.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: Lazy Stack Iterator | Approach 2: Eager Flattening (DFS) | Approach 3: C# `yield return` |
| :--- | :--- | :--- | :--- |
| **Constructor Time** | $O(1)$ | $O(N)$ (Blocks caller) | $O(1)$ |
| **Time: HasNext / Next** | $O(1)$ amortized | $O(1)$ | $O(1)$ amortized |
| **Auxiliary Space** | $O(D)$ (Depth of nesting) | $O(N)$ (Stores all integers) | $O(D)$ |
| **Streaming Suitability** | Optimal (infinite stream safe)| Zero (crashes on large streams)| Optimal |
| **Memory Locality** | Stack frames on heap | Contiguous array | Generated state machine |

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: Problem #120 - Flatten Nested List Iterator
 * ============================================================================
 * Core Pattern      : Lazy Tree Traversal via Enumerator Call-Stack
 * Time Complexity   : O(1) amortized per Next() and HasNext() call
 * Space Complexity  : O(D) auxiliary space where D is the maximum nesting depth
 * Selection Rule    : True lazy evaluation ensures O(1) constructor overhead
 *                     and zero memory waste when consuming partial streams.
 * Defensive Traps   : 1. Ensure HasNext() is strictly idempotent via peek caching.
 *                     2. Correctly discard empty nested lists [[]] without halting.
 *                     3. Always call Dispose() on exhausted IEnumerator instances.
 * ============================================================================
 */

namespace SeniorDSA.SystemDesign;

public interface NestedInteger
{
    bool IsInteger();
    int GetInteger();
    IList<NestedInteger> GetList();
}

public class NestedIterator
{
    private readonly Stack<IEnumerator<NestedInteger>> _stack;
    private NestedInteger? _preparedItem;

    /// <summary>
    /// Initializes the lazy iterator with the root nested collection.
    /// Runs in O(1) time without reading any elements upfront.
    /// </summary>
    public NestedIterator(IList<NestedInteger> nestedList)
    {
        ArgumentNullException.ThrowIfNull(nestedList);

        _stack = new Stack<IEnumerator<NestedInteger>>();
        _stack.Push(nestedList.GetEnumerator());
        _preparedItem = null;
    }

    /// <summary>
    /// Evaluates if more integers remain by unfolding nested sub-lists on demand.
    /// Guaranteed to be strictly idempotent across repeated calls.
    /// </summary>
    public bool HasNext()
    {
        // If an item is already resolved and waiting, return immediately
        if (_preparedItem != null)
        {
            return true;
        }

        while (_stack.Count > 0)
        {
            var topEnumerator = _stack.Peek();

            // Advance current enumerator
            if (!topEnumerator.MoveNext())
            {
                // Current nested level exhausted; clean up and return to parent frame
                topEnumerator.Dispose();
                _stack.Pop();
                continue;
            }

            var current = topEnumerator.Current;

            if (current.IsInteger())
            {
                // Concrete integer encountered; prepare and halt exploration
                _preparedItem = current;
                return true;
            }

            // Sub-list encountered; push new enumerator frame onto stack
            _stack.Push(current.GetList().GetEnumerator());
        }

        return false;
    }

    /// <summary>
    /// Returns the next integer in the flattened stream.
    /// </summary>
    public int Next()
    {
        if (!HasNext())
        {
            throw new InvalidOperationException("Sequence contains no more elements.");
        }

        int value = _preparedItem!.GetInteger();
        _preparedItem = null; // Consume
        return value;
    }
}
```

---

### 6. Senior Pitfalls & Defensive Traps

1. **The Non-Idempotent `HasNext()` Flaw:**
   - *Trap:* Advancing the enumerator inside `HasNext()` without caching the resolved element.
   - *Consequence:* If a client calls `if (it.HasNext() && it.HasNext()) val = it.Next();`, the iterator advances twice and silently skips an element.
   - *Defense:* The `_preparedItem` guard ensures subsequent calls to `HasNext()` return `true` immediately without touching enumerator cursors.
2. **Pushing Entire Lists in Reverse onto Stack:**
   - *Trap:* Unpacking sublists by pushing elements in reverse order: `for (int i = list.Count - 1; i >= 0; i--) stack.Push(list[i]);`.
   - *Consequence:* Eagerly copies all element references into the stack at each level, ballooning memory from $O(D)$ to $O(N)$ where $N$ is total elements.
   - *Defense:* Pushing an `IEnumerator` takes $O(1)$ space per nesting level.
3. **Resource Leak (Missing `Dispose`):**
   - *Trap:* Forgetting to dispose enumerators when popping them from the stack.
   - *Defense:* Explicitly call `topEnumerator.Dispose()` to satisfy IDisposable contracts.

---


## 121. Shortest Word Distance II (LeetCode #244)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | 🔥 High |
| **Pattern Tags** | `#hash-map` `#two-pointers` `#design` `#inverted-index` `#binary-search` |
| **LeetCode Link** | [Shortest Word Distance II](https://leetcode.com/problems/shortest-word-distance-ii/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Design a data structure that will be initialized with a string array `wordsDict`, and then efficiently answer queries of the shortest distance between two different words that already exist in `wordsDict`.
  - Implement the `WordDistance` class:
    - `WordDistance(String[] wordsDict)`: Initializes the object with the strings array `wordsDict`.
    - `int Shortest(String word1, String word2)`: Returns the shortest distance between the occurrences of `word1` and `word2` in `wordsDict`.
- **Assumptions & Contracts:**
  - `word1` and `word2` are guaranteed to be in `wordsDict`.
  - `word1 != word2`.
  - `Shortest` will be called repeatedly with high frequency.
- **Key Constraints:**
  - $1 \le \text{wordsDict.length} \le 3 \times 10^4$
  - $1 \le \text{wordsDict}[i]\text{.length} \le 10$
  - `wordsDict[i]` consists of lowercase English letters.
  - At most $5000$ calls will be made to `Shortest`.
- **Senior Edge Cases to Defend:**
  - Highly asymmetric occurrence frequencies: `word1` appears 1 time; `word2` appears $10,000$ times.
  - Alternating words: `["a", "b", "a", "b", "a", "b"]`. Minimum distance is 1.
  - Extreme distance: `word1` appears only at index 0; `word2` appears only at index $N - 1$.
  - Repeated identical queries: Querying `Shortest("apple", "banana")` hundreds of times.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Search Engine Inverted Index (Postings List). Preprocess the word corpus into a hash map mapping each word to a sorted list of its occurrence indices (`Dictionary<string, List<int>>`). Each query reduces to finding the minimum absolute difference between two pre-sorted lists, solved optimally via **Two-Pointer Linear Merge Scan** or **Binary Search**.
- **Sample Execution Trace:**
  - `WordDistance wordDistance = new WordDistance(["practice", "makes", "perfect", "coding", "makes"]);`
  - `wordDistance.Shortest("coding", "practice"); // return 3 (indices 3 and 0 -> |3 - 0| = 3)`
  - `wordDistance.Shortest("makes", "coding");    // return 1 (indices 1 or 4 vs 3 -> min(|1-3|, |4-3|) = 1)`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine an encyclopedia index. Instead of reading through the entire 1,000-page book every time someone asks: *"How close is the word 'Einstein' to 'Relativity'?"*, you look at the index at the back of the book. Under 'Einstein', you see page numbers: `[12, 104, 350]`. Under 'Relativity', you see: `[105, 412]`. Now, two fingers march through the two short lists to find the closest pair (`|104 - 105| = 1 page`).

#### 3.2 The Naive Bottleneck & Redundant Computation
- Scanning the raw array `wordsDict` for each query takes $O(N)$ time.
- For $Q = 5000$ queries on $N = 30,000$ words:
  $$\text{Total Ops} = 5000 \times 30,000 = 1.5 \times 10^8 \text{ operations}$$
  This causes severe timeout in high-concurrency search engines.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Inverted Index Invariant:** During a single $O(N)$ preprocessing pass, build `Dictionary<string, List<int>>`. Because indices are appended in sequential traversal order, every postings list is **naturally strictly sorted in ascending order**!
- **Monotonic Two-Pointer Convergence:**
  Given two sorted index lists $L_1$ and $L_2$ with pointers $p_1$ and $p_2$:
  - If $L_1[p_1] < L_2[p_2]$: The distance is $L_2[p_2] - L_1[p_1]$. Incrementing $p_2$ would only *increase* the difference because $L_2$ is ascending! The *only* way to decrease the distance is to advance $p_1 \leftarrow p_1 + 1$.
  - Symmetrically, if $L_1[p_1] > L_2[p_2]$, advance $p_2 \leftarrow p_2 + 1$.
  - Runs in $O(|L_1| + |L_2|)$ time!

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
POSTINGS LIST TWO-POINTER SWEEP:
word1 ("makes")  indices: [ 1, 4 ]
word2 ("coding") indices: [ 3 ]

Step 1: p1 -> idx 1,  p2 -> idx 3
        Diff = |1 - 3| = 2.  minDist = 2.
        Since 1 < 3, advance p1!

Step 2: p1 -> idx 4,  p2 -> idx 3
        Diff = |4 - 3| = 1.  minDist = min(2, 1) = 1.
        Since 4 > 3, advance p2!

Step 3: p2 reaches end. Terminate. Result = 1.
```

#### 3.5 State Transition Triggers & Decision Gates
1. Fetch $L_1 = \text{\_postings}[word1]$ and $L_2 = \text{\_postings}[word2]$.
2. Initialize $p_1 = 0, p_2 = 0, \text{minDist} = \infty$.
3. Loop while $p_1 < L_1.\text{Count} \land p_2 < L_2.\text{Count}$:
   - Compute $\text{currDist} = |L_1[p_1] - L_2[p_2]|$.
   - $\text{minDist} = \min(\text{minDist}, \text{currDist})$.
   - **Early Exit Gate:** If $\text{minDist} == 1$, return 1 immediately (minimum possible distance between distinct words).
   - If $L_1[p_1] < L_2[p_2]$: $p_1++$.
   - Else: $p_2++$.
4. Return $\text{minDist}$.

#### 3.6 Concrete Step-by-Step State Trace
Trace for `L1 = [2, 8, 15]`, `L2 = [5, 12]`:

| Step | $p_1$ (Value) | $p_2$ (Value) | Current Diff | `minDist` | Decision Gate | Pointer Update |
| :---: | :---: | :---: | :---: | :---: | :--- | :---: |
| 1 | 0 (2) | 0 (5) | $\|2 - 5\| = 3$ | 3 | $2 < 5 \implies$ advance $p_1$ | $p_1 \leftarrow 1$ |
| 2 | 1 (8) | 0 (5) | $\|8 - 5\| = 3$ | 3 | $8 > 5 \implies$ advance $p_2$ | $p_2 \leftarrow 1$ |
| 3 | 1 (8) | 1 (12) | $\|8 - 12\| = 4$ | 3 | $8 < 12 \implies$ advance $p_1$ | $p_1 \leftarrow 2$ |
| 4 | 2 (15) | 1 (12) | $\|15 - 12\| = 3$ | 3 | $15 > 12 \implies$ advance $p_2$ | $p_2 \leftarrow 2$ (End) |

Minimum distance = 3.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1: Two Pointers over Postings Lists (Optimal for Similar Frequencies):**
  - Time: $O(K_1 + K_2)$ where $K_1 = |L_1|, K_2 = |L_2|$.
- **Approach 2: Binary Search over Longer List (Optimal for Skewed Frequencies):**
  - If $K_1 \ll K_2$ (e.g., $K_1 = 1, K_2 = 10,000$), binary searching $L_1$'s single index in $L_2$ takes $O(K_1 \log K_2) = 14$ operations, versus $10,000$ operations with two pointers!
- **Approach 3: Query Result Memoization Cache:**
  - High-traffic search engines memoize `(word1, word2)` query results in a cache for $O(1)$ repeat queries.

#### 4.2 Step-by-Step Natural Progression Flow
1. **Index Ingestion:** In constructor, perform a single linear scan $i = 0 \dots N - 1$ over `wordsDict`. Append index $i$ to `_postings[wordsDict[i]]`. Because $i$ increases monotonically, lists are guaranteed sorted.
2. **Query Initialization:** Fetch postings lists for `word1` and `word2`. Initialize two cursors $p_1 = 0, p_2 = 0$, and `minDistance = int.MaxValue`.
3. **Two-Pointer Convergence:** While both cursors are in bounds, compute $|L_1[p_1] - L_2[p_2]|$, update `minDistance`, and advance the pointer pointing to the smaller index. Short-circuit immediately if `minDistance == 1`.
4. **Result Resolution:** Return `minDistance`.

#### 4.3 Alternative Approaches Analysis
- **Approach 2 (Binary Search over Longer List):**
  - If occurrence counts are extremely asymmetric ($K_1 \ll K_2$), iterate through each index $idx \in L_1$ and use binary search (`List.BinarySearch`) to find the nearest elements in $L_2$.
  - Complexity: $O(K_1 \log K_2)$. Outperforms two-pointer scan when ratio $K_2 / K_1 \ge 32$.
- **Approach 3 (All-Pairs Precomputation / Distance Matrix):**
  - Compute shortest distance for all unique word pairs upfront and store in a nested dictionary `Dictionary<string, Dictionary<string, int>>`.
  - Complexity: Query time drops to strict $O(1)$, but constructor blows up to $O(U^2 \cdot K)$ and requires $O(U^2)$ memory where $U$ is distinct words count.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: Two Pointers | Approach 2: Binary Search | Approach 3: Brute Force Scan |
| :--- | :--- | :--- | :--- |
| **Constructor Time** | $O(N)$ | $O(N)$ | $O(1)$ |
| **Query Time** | $O(K_1 + K_2)$ | $O(\min(K_1, K_2) \log(\max(K_1, K_2)))$ | $O(N)$ |
| **Auxiliary Space** | $O(N)$ | $O(N)$ | $O(1)$ |
| **Cache Locality** | Optimal (sequential access) | Moderate (binary search jumps) | Optimal |
| **Best Used When** | $K_1 \approx K_2$ | $K_1 \ll K_2$ or $K_2 \ll K_1$ | Single query only |

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: Problem #121 - Shortest Word Distance II
 * ============================================================================
 * Core Pattern      : Inverted Index Postings List + Two-Pointer Convergence
 * Time Complexity   : O(N) constructor; O(K1 + K2) per query (K1, K2 = word frequencies)
 * Space Complexity  : O(N) auxiliary space for inverted index storage
 * Selection Rule    : Preprocessing indices into ascending lists enables O(K1 + K2)
 *                     linear sweep; early exit at minDist == 1 optimizes queries.
 * Defensive Traps   : 1. Postings lists are naturally sorted; avoid sorting again.
 *                     2. Use canonical string pairing for memoization caches.
 * ============================================================================
 */

namespace SeniorDSA.SystemDesign;

public class WordDistance
{
    private readonly Dictionary<string, List<int>> _postings;

    /// <summary>
    /// Builds an inverted index mapping each unique word to its sorted list of indices.
    /// Runs in O(N) linear time.
    /// </summary>
    public WordDistance(string[] wordsDict)
    {
        ArgumentNullException.ThrowIfNull(wordsDict);

        _postings = new Dictionary<string, List<int>>(wordsDict.Length);

        for (int i = 0; i < wordsDict.Length; i++)
        {
            string word = wordsDict[i];
            if (!_postings.TryGetValue(word, out var indices))
            {
                indices = new List<int>();
                _postings[word] = indices;
            }
            indices.Add(i);
        }
    }

    /// <summary>
    /// Calculates the shortest distance between occurrences of word1 and word2.
    /// Uses two-pointer convergence over the pre-sorted postings lists.
    /// </summary>
    public int Shortest(string word1, string word2)
    {
        if (!_postings.TryGetValue(word1, out var list1) ||
            !_postings.TryGetValue(word2, out var list2))
        {
            throw new ArgumentException("Both words must exist in the corpus.");
        }

        int p1 = 0;
        int p2 = 0;
        int minDistance = int.MaxValue;

        // Two-pointer linear sweep
        while (p1 < list1.Count && p2 < list2.Count)
        {
            int idx1 = list1[p1];
            int idx2 = list2[p2];

            int diff = Math.Abs(idx1 - idx2);
            if (diff < minDistance)
            {
                minDistance = diff;
                // Theoretical absolute minimum distance between distinct words is 1
                if (minDistance == 1)
                {
                    return 1;
                }
            }

            if (idx1 < idx2)
            {
                p1++;
            }
            else
            {
                p2++;
            }
        }

        return minDistance;
    }
}
```

---

### 6. Senior Pitfalls & Defensive Traps

1. **The Postings List Sorting Redundancy:**
   - *Trap:* Calling `list.Sort()` on the postings lists after the constructor finishes.
   - *Defense:* The forward traversal loop $i = 0 \to N - 1$ appends indices monotonically. They are mathematically guaranteed to be sorted.
2. **Frequency Asymmetry Degradation:**
   - *Trap:* When `word1` occurs once and `word2` occurs 20,000 times, the two-pointer loop can iterate up to 20,000 times.
   - *Defense:* Adaptive hybrid execution: if $\max(K_1, K_2) > 32 \times \min(K_1, K_2)$, switch to binary search for each index of the smaller list in the larger list to run in $O(K_{\text{small}} \log K_{\text{large}})$.
3. **Repeated Query Allocations:**
   - *Trap:* Allocating tuples or strings repeatedly in high-traffic APIs without caching.
   - *Defense:* Memoize queries with a canonical key: `var key = string.CompareOrdinal(w1, w2) < 0 ? (w1, w2) : (w2, w1);`.

---


## 122. Moving Average from Data Stream (LeetCode #346)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟢 Easy |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#queue` `#circular-buffer` `#design` `#sliding-window` `#rolling-sum` |
| **LeetCode Link** | [Moving Average from Data Stream](https://leetcode.com/problems/moving-average-from-data-stream/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given a stream of integers and a window size, calculate the moving average of all integers in the sliding window.
  - Implement the `MovingAverage` class:
    - `MovingAverage(int size)`: Initializes the object with the size of the window `size`.
    - `double Next(int val)`: Returns the moving average of the last `size` values of the stream.
- **Assumptions & Contracts:**
  - When the stream contains fewer elements than `size`, compute the average across all elements seen so far.
  - Precision must satisfy standard double precision ($10^{-5}$ tolerance).
- **Key Constraints:**
  - $1 \le \text{size} \le 1000$
  - $-10^5 \le \text{val} \le 10^5$
  - At most $10^4$ calls will be made to `Next`.
- **Senior Edge Cases to Defend:**
  - Initial warm-up phase (stream count $< size$): Denominator is `count`, not `size`.
  - Integer overflow: Running sum of $1000$ values of magnitude $10^5$ exceeds $10^8$. While within 32-bit `int`, in industrial telemetry with larger windows/values, 32-bit sums easily overflow.
  - Floating-point subtraction drift: Accumulating values in `double` and repeatedly subtracting can cause IEEE 754 precision drift. Integer accumulation prevents this completely.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Fixed-Capacity Circular Ring Buffer. Storing elements in a circular array of length $K$ eliminates all heap allocations after constructor initialization. A rolling sum accumulator adds incoming values and subtracts overwritten values in guaranteed $O(1)$ time.
- **Sample Execution Trace:**
  - `MovingAverage m = new MovingAverage(3);`
  - `m.Next(1); // return 1.0 = 1 / 1`
  - `m.Next(10); // return 5.5 = (1 + 10) / 2`
  - `m.Next(3); // return 4.66667 = (1 + 10 + 3) / 3`
  - `m.Next(5); // return 6.0 = (10 + 3 + 5) / 3 (1 is evicted)`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine a mechanical carousel with 3 seats. When the carousel is full, a new rider doesn't cause all 3 seats to be destroyed and rebuilt. The new rider simply takes the seat of the person who has been riding the longest, pushing them off. The operator adjusts the total weight gauge by subtracting the old rider's weight and adding the new rider's weight in one instant operation.

#### 3.2 The Naive Bottleneck & Redundant Computation
- Re-summing the window elements on every `Next(val)` call costs $O(K)$ time per call.
- Over $N$ calls, total time is $O(N \cdot K)$.
- Using `Queue<int>` achieves $O(1)$, but enqueuing and dequeuing creates continuous heap allocations and GC pressure in high-throughput audio/telemetry pipelines.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Circular Buffer Array:** Allocate a single flat primitive array `int[] buffer = new int[size]`.
- **Head Index Wrap-Around Invariant:**
  $$\text{head} = (\text{head} + 1) \pmod{\text{size}}$$
- **Rolling Sum Invariant:**
  $$\text{sum}_{\text{new}} = \text{sum}_{\text{old}} - \text{buffer}[\text{head}] + \text{val}$$
- Exactly zero heap allocations during the entire lifetime of the stream!

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
CIRCULAR RING BUFFER ARCHITECTURE (size = 3):

Initial: [ 0, 0, 0 ], head = 0, count = 0, sum = 0

Next(1):  [ 1, 0, 0 ], head = 1, count = 1, sum = 1   -> Avg = 1 / 1 = 1.0
Next(10): [ 1, 10, 0 ], head = 2, count = 2, sum = 11  -> Avg = 11 / 2 = 5.5
Next(3):  [ 1, 10, 3 ], head = 0, count = 3, sum = 14  -> Avg = 14 / 3 = 4.67

Next(5):  head = 0 holds old value '1'.
          sum = 14 - 1 + 5 = 18
          buffer[0] = 5
          head = (0 + 1) % 3 = 1
          Avg = 18 / 3 = 6.0
```

#### 3.5 State Transition Triggers & Decision Gates
1. If `_count < _capacity`:
   - Increment `_count++`.
2. Else:
   - Subtract oldest value: `_sum -= _buffer[_head]`.
3. Overwrite slot: `_buffer[_head] = val`.
4. Add new value: `_sum += val`.
5. Advance cursor: `_head = (_head + 1) % _capacity`.
6. Return `(double)_sum / _count`.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1: Circular Ring Buffer (High-Performance Production Standard):**
  - Zero heap allocation after constructor.
  - Perfect CPU cache locality (flat contiguous primitive memory).
- **Approach 2: Standard `Queue<int>` (Interview Standard):**
  - Easier to write in 30 seconds, but incurs object allocation overhead.

#### 4.2 Step-by-Step Natural Progression Flow
1. **Initialize Fixed Primitive Buffer:** In constructor, allocate `new int[size]` contiguous array and set `_head = 0, _count = 0, _runningSum = 0`.
2. **Warm-Up vs Full Window Branching:**
   - If `_count < _capacity`: increment `_count`. No element is evicted.
   - Else: subtract the oldest element `_buffer[_head]` from `_runningSum`.
3. **Ingestion & Pointer Advancement:** Overwrite `_buffer[_head] = val`, add `val` to `_runningSum`, and advance `_head = (_head + 1) % _capacity`.
4. **Precision Output:** Return `(double)_runningSum / _count`.

#### 4.3 Alternative Approaches Analysis
- **Approach 2 (Standard `Queue<int>`):**
  - Maintains `runningSum` and pushes/pops from `Queue<int>`.
  - While simple and achieving $O(1)$ amortized time, every `Enqueue` allocates internal queue node structures, creating GC pressure in high-throughput real-time telemetry streams.
- **Approach 3 (Naive Full Window Summation):**
  - Stores stream values in dynamic list and sums the last $K$ elements on every call.
  - Runtime degrades to $O(K)$ per call, which fails under high call volumes ($10^4 \times 1000 = 10^7$ operations).

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: Circular Ring Buffer | Approach 2: Queue<int> | Approach 3: Re-summing Array |
| :--- | :--- | :--- | :--- |
| **Time per Call** | $O(1)$ strict | $O(1)$ amortized | $O(K)$ |
| **Auxiliary Space** | $O(K)$ fixed array | $O(K)$ node queue | $O(K)$ or $O(N)$ |
| **Heap Allocations** | **Zero** after init | Allocations on enqueue | Zero |
| **Cache Hit Rate** | **100% L1 Cache** | Moderate | High |
| **Numeric Drift** | Zero (integer math) | Zero | Zero |

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: Problem #122 - Moving Average from Data Stream
 * ============================================================================
 * Core Pattern      : Zero-Allocation Circular Ring Buffer with Rolling Sum
 * Time Complexity   : O(1) strictly per Next() call
 * Space Complexity  : O(K) strictly bounded primitive array (zero GC churn)
 * Selection Rule    : Circular array buffers eliminate dynamic heap allocations
 *                     and guarantee maximum CPU L1 cache locality.
 * Defensive Traps   : 1. Accumulate sum in a 64-bit 'long' to prevent 32-bit overflow.
 *                     2. Denominator during warm-up is count, NOT capacity.
 *                     3. Guard against size <= 0 in constructor.
 * ============================================================================
 */

namespace SeniorDSA.SystemDesign;

public class MovingAverage
{
    private readonly int[] _buffer;
    private readonly int _capacity;
    private int _head;
    private int _count;
    private long _runningSum; // 64-bit integer prevents signed overflow

    /// <summary>
    /// Initializes the moving average window with fixed capacity.
    /// Allocates contiguous primitive buffer once to ensure zero GC churn.
    /// </summary>
    public MovingAverage(int size)
    {
        if (size <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(size), "Window size must be strictly positive.");
        }

        _capacity = size;
        _buffer = new int[size];
        _head = 0;
        _count = 0;
        _runningSum = 0;
    }

    /// <summary>
    /// Ingests a new stream value and computes the moving average in strict O(1) time.
    /// </summary>
    public double Next(int val)
    {
        if (_count < _capacity)
        {
            _count++;
        }
        else
        {
            // Evict oldest element residing at current head position
            _runningSum -= _buffer[_head];
        }

        // Add incoming value and store in ring buffer
        _runningSum += val;
        _buffer[_head] = val;

        // Advance circular head cursor
        _head = (_head + 1) % _capacity;

        // Accurate division using active count during warm-up phase
        return (double)_runningSum / _count;
    }
}
```

---

### 6. Senior Pitfalls & Defensive Traps

1. **Floating-Point Cumulative Error Drift:**
   - *Trap:* Maintaining `double _sum` and executing `_sum += val; _sum -= oldVal;`.
   - *Consequence:* After $10^6$ operations, IEEE 754 precision rounding errors accumulate, causing the moving average to drift away from the true mathematical average.
   - *Defense:* Accumulate in an integer primitive (`long _runningSum`) and cast to `double` only during final division.
2. **The Warm-Up Denominator Bug:**
   - *Trap:* Writing `return (double)_runningSum / _capacity;`.
   - *Consequence:* During the first $K - 1$ steps, the average is divided by $K$ instead of the number of actual elements ingested, producing artificially tiny values.
   - *Defense:* Divide strictly by `_count`.
3. **Modulo Division Performance Overhead:**
   - *Trap:* Using `% _capacity` on hardware where integer division instructions cost 15–30 CPU cycles.
   - *Optimization:* If $K$ is a power of 2, use bitwise mask: `_head = (_head + 1) & (capacity - 1)`. Otherwise, an `if (++_head == _capacity) _head = 0;` branch predictor handles sequential increments faster than integer modulo.
