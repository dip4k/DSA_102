# 📘 Week 19, Day 4: Mock Round 4: Mixed Paradigms & Systems Scaling

> 🧭 **Navigation:** [← Previous Day](Week_19_Day_03_Mock_Dynamic_Programming_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_19_Day_05_Weakness_Diagnosis_And_Strategy_Instructional.md)
> 
> 💡 **Instructor Note:** *This round tests multi-paradigm mastery under live interview conditions: combining monotonic queues for amortized O(1) sliding window extremes and designing custom composite data structures (Doubly Linked List + Hash Map) for true O(1) LRU eviction under strict memory limits.*

---

## 🎯 Learning Objectives

*   **Interview Simulation:** Experience an authentic 45-minute live senior coding interview on composite data structures and monotonic sliding windows.
*   **Dialogue Navigation:** Defend amortized `O(1)` monotonic deque invariance, explain why heap `O(N log K)` fails low-latency SLAs, and defend sentinel nodes for pointer safety.
*   **3-Tiered Hint Recovery:** Practice extracting hints systematically (Gentle Nudge -> Structural Anchor -> Tactical Code Hint) without losing seniority evaluation points.
*   **Senior Rubric Mastery:** Evaluate solutions against top-tier industry criteria: Problem Formulation, Boundary Questioning, Space/Time Trade-offs, and Clean Code.

---

## ⚖️ FAANG Senior / Lead Interview Calibration

In Tier-1 coding rounds (Google, Meta, Apple, Netflix, Stripe), mixed-paradigm and composite structure problems evaluate whether a candidate can synthesize primitives while maintaining strict runtime invariants:

| Paradigm Variant | Time Complexity | Auxiliary Space | Eviction / Step Latency | Common Pitfalls | Senior Calibration Bar |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Sliding Window Naive Scan** | `O(N * K)` | `O(1)` | `O(K)` | Catastrophic TLE on `N = 10^5, K = 5 * 10^4` | Immediate reject. |
| **Sliding Window Max-Heap** | `O(N log K)` | `O(K)` | `O(log K)` (lazy eviction) | Stale root deletion overhead; memory bloat | Mid-level bar (L4). |
| **Monotonic Deque (Sliding Max)** | `O(N)` | `O(K)` | `O(1)` amortized | Storing values instead of indices; off-by-one window bounds | **Senior Bar (L5): Clear domination invariant proof.** |
| **LRU Cache via Array Scan** | `O(N)` get/put | `O(Capacity)` | `O(Capacity)` | Linear scan on cache lookup | Immediate reject. |
| **LRU Cache via LinkedHashMap**| `O(1)` get/put | `O(Capacity)` | `O(1)` | Using language built-ins without understanding internal pointers | Acceptable only with explicit internal walkthrough. |
| **Custom DLL + Hash Map (LRU)** | `O(1)` get/put | `O(Capacity)` | `O(1)` strict | Null pointer exceptions; failing to update map on eviction | **Senior Bar (L5): Sentinel nodes, contract safety, O(1) strict.** |

### The Interviewer's Hidden Rubric: What We Listen For
1. **Domination Principle:** In sliding window maximum, does the candidate recognize that if element `nums[j] >= nums[i]` with `j > i`, element `nums[i]` can **never** be the maximum of any current or future window?
2. **Sentinel Node Pattern:** In custom doubly-linked lists, does the candidate initialize dummy `head` and `tail` nodes to eliminate edge-case null checks during pointer splicing?
3. **Index vs Value Storage:** Does the candidate store array indices inside the deque to achieve `O(1)` lifetime tracking for expired elements?

---

## 📖 Chapter 1: Live Interview Simulation & Dialogue Transcript

### Problem 1: Sliding Window Maximum (LeetCode 239)
> **Interviewer:** *"You are given an array of integers `nums`, there is a sliding window of size `k` which is moving from the very left of the array to the very right. You can only see the `k` numbers in the window. Each time the sliding window moves right by one position, return the max sliding window."*

```text
Contiguous Window Sliding Across Array:
Window: [1  3  -1] -3  5  3  6  7       -> Max = 3
         1 [3  -1  -3] 5  3  6  7       -> Max = 3
         1  3 [-1  -3  5] 3  6  7       -> Max = 5
         1  3  -1 [-3  5  3] 6  7       -> Max = 5
         1  3  -1  -3 [5  3  6] 7       -> Max = 6
         1  3  -1  -3  5 [3  6  7]      -> Max = 7
```

#### Dialogue Transcript: Problem 1

**Candidate:** "Let's first clarify constraints and boundary conditions:
1. What are the constraints on array length `N` and window size `k`? Can `k > N`?
2. Can elements be negative or zero?
3. What should be returned if `nums` is empty or `k <= 0`?"

**Interviewer:** "`1 <= k <= nums.length <= 10^5`. Elements are signed 32-bit integers between `-10^4` and `10^4`. If `k == 1`, return a copy of the array. If `k > N`, clamp to the maximum of the entire array."

**Candidate:** "Understood. Let's analyze the naive and intermediate approaches:
- **Naive Brute Force:** For each of the `N - k + 1` window positions, scan all `k` elements to compute the maximum. This takes `O((N - k + 1) * k) = O(N * k)` time. If `N = 10^5` and `k = 50,000`, operations reach `2.5 * 10^9`, resulting in an interview timeout.
- **Max-Heap / PriorityQueue:** We could maintain a heap of window elements with their indices. Querying the max is `O(1)`, but inserting takes `O(log k)`, and evicting stale elements outside the current window requires either `O(k)` heap removal or lazy eviction, yielding `O(N log N)` time and extra heap allocations.
- **Can we do O(N) total time?** Yes, using a **Monotonic Decreasing Deque**."

**Interviewer:** "Explain the invariant that allows a monotonic deque to run in linear time."

**Candidate:** "Consider two elements in the current window at indices `i` and `j` where `i < j`.
If `nums[i] <= nums[j]`, then `nums[i]` can **never** be the window maximum:
1. `nums[j]` is larger than or equal to `nums[i]`.
2. `nums[j]` entered the window later, so it will expire **after** `nums[i]`.
Therefore, `nums[i]` is completely obsolete. We can permanently discard it!
By maintaining a double-ended queue storing **indices** such that the corresponding array values are strictly decreasing:
- Front of deque always holds the index of the maximum element in the current window.
- When sliding the window to index `i`:
  1. Evict front index if it has fallen out of the window (`front <= i - k`).
  2. While the back of the deque has `nums[back] <= nums[i]`, pop from the back.
  3. Push `i` to the back.
  4. Once `i >= k - 1`, record `nums[front]` as the window maximum."

**Interviewer:** "What is the amortized cost per element?"

**Candidate:** "Each index enters the deque exactly once and is popped at most once (either from the front when expired or from the back when dominated). Across `N` iterations, at most `2 * N` deque operations occur, guaranteeing **strict `O(N)` overall runtime** and `O(k)` auxiliary space."

---

### Problem 2: LRU Cache Architecture (LeetCode 146)
> **Interviewer:** *"Design a data structure that follows the constraints of a Least Recently Used (LRU) cache. Implement the `LRUCache` class with `int get(int key)` and `void put(int key, int value)`. Both operations must run in `O(1)` average time complexity."*

```text
Composite Data Structure Architecture:
                     Hash Map: key -> Node Pointer
               +--------------------------------------+
               | Key 1 -> Node(1) | Key 2 -> Node(2)  |
               +--------------------------------------+
                                  |
                                  v
+-----------+       +-----------+       +-----------+       +-----------+
| DummyHead | <---> |  Node(2)  | <---> |  Node(1)  | <---> | DummyTail |
+-----------+       +-----------+       +-----------+       +-----------+
                     Most Recent         Least Recent
```

#### Dialogue Transcript: Problem 2

**Candidate:** "To achieve `O(1)` time complexity for both `get` and `put`, we need two capabilities simultaneously:
1. Fast lookup by key in `O(1)` time.
2. Fast reordering (move to most recent, evict least recent) in `O(1)` time.
A standard hash table provides `O(1)` lookup but has no concept of element ordering.
A contiguous array maintains ordering, but moving an element to the front or deleting from the middle takes `O(N)` due to element shifting.
A singly linked list allows `O(1)` removal only if we have a pointer to the preceding node.
Therefore, the optimal architecture is a **Doubly Linked List paired with a Hash Map**:
- The **Hash Map** maps `key -> Node*`, giving `O(1)` lookup.
- The **Doubly Linked List** stores `(key, value)` pairs in access order. Because each node has both `prev` and `next` pointers, given a pointer to a node, we can splice it out in `O(1)` operations without scanning."

**Interviewer:** "How do you handle edge cases like adding to an empty cache or evicting the last item?"

**Candidate:** "We use the **Sentinel Pattern**: introduce dummy `head` and dummy `tail` nodes initialized to point to each other (`head.next = tail`, `tail.prev = head`).
All real cache nodes live between `head` and `tail`:
- The most recently used node is always at `head.next`.
- The least recently used node is always at `tail.prev`.
Sentinel nodes guarantee that every real node always has valid non-null `prev` and `next` pointers, completely eliminating null checks and branching during node splicing."

**Interviewer:** "Why must each node store both its key and value, rather than just the value?"

**Candidate:** "When the cache exceeds capacity, we evict the least recently used node (`tail.prev`). To remove this evicted node from the Hash Map in `O(1)`, we must know its key! If the node only stored the value, we would have to search the entire map in `O(N)` time."

**Interviewer:** "Excellent architectural clarity. Proceed with the implementations."

---

## 🧭 Chapter 2: 3-Tiered Hint Progression

### Problem 1: Sliding Window Maximum

```text
+-----------------------------------------------------------------------------+
| TIER 1: GENTLE NUDGE (Contextual Awareness)                                 |
| "If a new element entering the window is strictly greater than older        |
| elements already in the window, can those older elements EVER become the     |
| maximum in any future window? When do elements become permanently useless?"  |
+-----------------------------------------------------------------------------+
                                       |
                                       v
+-----------------------------------------------------------------------------+
| TIER 2: STRUCTURAL ANCHOR (Algorithmic Primitive)                           |
| "Maintain a double-ended queue of array indices where values are strictly   |
| non-increasing. Discard from the back any element smaller than the incoming |
| value. How do you detect when the front element has slid out of the window?"|
+-----------------------------------------------------------------------------+
                                       |
                                       v
+-----------------------------------------------------------------------------+
| TIER 3: TACTICAL CODE HINT (Implementation Mechanics)                        |
| "For each index i:                                                          |
|  1. if (deque.first <= i - k) deque.pop_front()                             |
|  2. while (deque and nums[deque.last] <= nums[i]) deque.pop_back()          |
|  3. deque.push_back(i)                                                      |
|  4. if (i >= k - 1) result.append(nums[deque.first])"                       |
+-----------------------------------------------------------------------------+
```

### Problem 2: LRU Cache Design

```text
+-----------------------------------------------------------------------------+
| TIER 1: GENTLE NUDGE (Contextual Awareness)                                 |
| "A hash table gives O(1) lookup. A linked list gives O(1) insertion.        |
| Why can't a singly linked list remove an arbitrary node in O(1) time?       |
| What additional pointer is missing?"                                        |
+-----------------------------------------------------------------------------+
                                       |
                                       v
+-----------------------------------------------------------------------------+
| TIER 2: STRUCTURAL ANCHOR (Algorithmic Primitive)                           |
| "A doubly linked list allows O(1) node detachment because node.prev is      |
| immediately accessible. Store node pointers directly in the hash map.      |
| How can dummy head and tail sentinel nodes eliminate pointer null checks?"  |
+-----------------------------------------------------------------------------+
                                       |
                                       v
+-----------------------------------------------------------------------------+
| TIER 3: TACTICAL CODE HINT (Implementation Mechanics)                        |
| "Create helper methods: _remove(node) and _add_to_head(node).               |
| In get(key): if present, remove(node), add_to_head(node), return node.val.  |
| In put(key, val): if present, update val and move to head. If new, evict    |
| tail.prev if full (remembering to del map[lru.key]), then insert at head."  |
+-----------------------------------------------------------------------------+
```

---

## 📊 Chapter 3: Senior Evaluation Rubric

| Dimension | Unsatisfactory (Level 1) | Developing (Level 2) | Senior Bar (Level 3) | Staff/Principal Bar (Level 4) |
| :--- | :--- | :--- | :--- | :--- |
| **Problem Formulation** | Proposes quadratic scans; fails to identify monotonic property or DLL requirement. | Recognizes heap or built-in LinkedHashMap after hints; struggles to explain internal mechanics. | Proactively derives monotonic deque and composite DLL + HashMap; articulates amortized `O(1)`. | Formulates domination property immediately; explains cache eviction policies (LRU vs LFU vs ARC vs 2Q). |
| **Boundary Questioning** | Does not ask about `k > N`, empty arrays, negative values, or zero cache capacity. | Asks generic questions without connecting to data types or error states. | Validates `k == 1`, `k == N`, capacity limits, and duplicate key updates before writing code. | Quantifies cache line implications, thread contention, and memory bounds on 64-bit systems. |
| **Space/Time Trade-offs** | Implements `O(N * k)` or `O(N log k)` without realizing linear alternative exists. | Achieves `O(N)` with guidance but cannot defend amortized complexity bounds. | Rigorously proves each element enters and leaves deque at most once; defends `O(k)` space. | Evaluates custom array-backed circular deques to eliminate object pointer indirection in runtime. |
| **Code Cleanliness** | Null reference exceptions; memory leaks on eviction; off-by-one window indices. | Working code but repeats DLL detachment logic; messy variable names. | Modular, clean helper functions (`RemoveNode`, `AddToHead`); sentinel nodes eliminate branching. | Production-grade code with zero unnecessary heap allocations, defensive exception handling, thread-safety notes. |

---

## 💻 Chapter 4: Idiomatic Dual-Language Code

### Problem 1: Sliding Window Maximum (LeetCode 239)

#### C# Primary Implementation (.NET 8/9)

```csharp
using System;

namespace MockInterviews.MixedRounds;

public static class SlidingWindowMaxEngine
{
    /// <summary>
    /// Computes maximum of each sliding window of size k in strict O(N) time and O(k) space.
    /// Uses an array-backed circular buffer to simulate a double-ended queue with zero GC allocations.
    /// </summary>
    public static int[] MaxSlidingWindow(ReadOnlySpan<int> nums, int k)
    {
        if (nums.IsEmpty || k <= 0) return [];
        if (k == 1) return nums.ToArray();

        int n = nums.Length;
        int[] result = new int[n - k + 1];

        // Array-backed deque storing indices: head and tail pointers
        int[] deque = new int[n];
        int head = 0;
        int tail = 0;

        for (int i = 0; i < n; i++)
        {
            // 1. Evict elements outside the current window horizon [i - k + 1, i]
            if (head < tail && deque[head] < i - k + 1)
            {
                head++;
            }

            // 2. Maintain monotonic decreasing order: pop smaller or equal elements from back
            int currentVal = nums[i];
            while (head < tail && nums[deque[tail - 1]] <= currentVal)
            {
                tail--;
            }

            // 3. Append current index to back
            deque[tail++] = i;

            // 4. Record window maximum once the first k elements are processed
            if (i >= k - 1)
            {
                result[i - k + 1] = nums[deque[head]];
            }
        }

        return result;
    }
}
```

#### Python Secondary Implementation (Python 3.11+)

```python
from collections import deque
from typing import Sequence

def max_sliding_window(nums: Sequence[int], k: int) -> list[int]:
    """
    Computes maximum of each sliding window of size k in O(N) time and O(k) auxiliary space.
    Uses collections.deque to store indices in monotonic descending order of corresponding values.
    """
    if not nums or k <= 0:
        return []
    if k == 1:
        return list(nums)

    dq: deque[int] = deque()
    result: list[int] = []

    for i, val in enumerate(nums):
        # 1. Evict indices outside current window horizon [i - k + 1, i]
        if dq and dq[0] < i - k + 1:
            dq.popleft()

        # 2. Maintain monotonic descending property: evict smaller elements from back
        while dq and nums[dq[-1]] <= val:
            dq.pop()

        # 3. Add current index to deque
        dq.append(i)

        # 4. Record front element as maximum once window is fully initialized
        if i >= k - 1:
            result.append(nums[dq[0]])

    return result
```

---

### Problem 2: Production LRU Cache Architecture (LeetCode 146)

#### C# Primary Implementation (.NET 8/9)

```csharp
using System;
using System.Collections.Generic;

namespace MockInterviews.MixedRounds;

/// <summary>
/// Production-grade O(1) LRU Cache using Hash Map + Doubly Linked List with Sentinel Nodes.
/// Guaranteed O(1) Get and Put operations with zero edge-case branching.
/// </summary>
public sealed class LRUCache
{
    private sealed class Node
    {
        public int Key;
        public int Value;
        public Node? Prev;
        public Node? Next;

        public Node(int key = 0, int value = 0)
        {
            Key = key;
            Value = value;
        }
    }

    private readonly int _capacity;
    private readonly Dictionary<int, Node> _map;
    private readonly Node _head;
    private readonly Node _tail;

    public LRUCache(int capacity)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(capacity);
        _capacity = capacity;
        _map = new Dictionary<int, Node>(capacity);

        // Sentinel dummy nodes eliminate null checks during pointer manipulation
        _head = new Node();
        _tail = new Node();
        _head.Next = _tail;
        _tail.Prev = _head;
    }

    public int Get(int key)
    {
        if (!_map.TryGetValue(key, out Node? node))
        {
            return -1;
        }

        // Cache hit: promote node to Most Recently Used (MRU) position
        MoveToHead(node);
        return node.Value;
    }

    public void Put(int key, int value)
    {
        if (_map.TryGetValue(key, out Node? existingNode))
        {
            // Key exists: update value and promote to head
            existingNode.Value = value;
            MoveToHead(existingNode);
            return;
        }

        // Evict least recently used (LRU) node if capacity reached
        if (_map.Count >= _capacity)
        {
            Node lruNode = _tail.Prev!;
            RemoveNode(lruNode);
            _map.Remove(lruNode.Key);
        }

        // Create and insert new MRU node
        var newNode = new Node(key, value);
        _map[key] = newNode;
        AddToHead(newNode);
    }

    private void AddToHead(Node node)
    {
        node.Prev = _head;
        node.Next = _head.Next;
        _head.Next!.Prev = node;
        _head.Next = node;
    }

    private static void RemoveNode(Node node)
    {
        node.Prev!.Next = node.Next;
        node.Next!.Prev = node.Prev;
    }

    private void MoveToHead(Node node)
    {
        RemoveNode(node);
        AddToHead(node);
    }
}
```

#### Python Secondary Implementation (Python 3.11+)

```python
from typing import Optional

class Node:
    __slots__ = ('key', 'value', 'prev', 'next')

    def __init__(self, key: int = 0, value: int = 0):
        self.key: int = key
        self.value: int = value
        self.prev: Optional['Node'] = None
        self.next: Optional['Node'] = None


class LRUCache:
    """
    O(1) LRU Cache via Hash Map + Doubly Linked List with Sentinel Nodes.
    Both get() and put() run in strict O(1) average time.
    """

    def __init__(self, capacity: int):
        if capacity <= 0:
            raise ValueError("Capacity must be strictly positive")
        self.capacity: int = capacity
        self.map: dict[int, Node] = {}

        # Sentinel dummy nodes eliminate boundary checks
        self.head: Node = Node()
        self.tail: Node = Node()
        self.head.next = self.tail
        self.tail.prev = self.head

    def _remove(self, node: Node) -> None:
        """Detaches node from its current position in DLL."""
        node.prev.next = node.next
        node.next.prev = node.prev

    def _add_to_head(self, node: Node) -> None:
        """Inserts node immediately after dummy head (MRU position)."""
        node.prev = self.head
        node.next = self.head.next
        self.head.next.prev = node
        self.head.next = node

    def get(self, key: int) -> int:
        if key not in self.map:
            return -1

        node = self.map[key]
        self._remove(node)
        self._add_to_head(node)
        return node.value

    def put(self, key: int, value: int) -> None:
        if key in self.map:
            node = self.map[key]
            node.value = value
            self._remove(node)
            self._add_to_head(node)
            return

        if len(self.map) >= self.capacity:
            lru = self.tail.prev
            self._remove(lru)
            del self.map[lru.key]

        new_node = Node(key, value)
        self.map[key] = new_node
        self._add_to_head(new_node)
```

---

## 🔬 Chapter 5: Explicit Complexity Deconstruction

| Metric | Sliding Window Maximum | LRU Cache (`Get` / `Put`) | Systems Engineering Grounding |
| :--- | :--- | :--- | :--- |
| **Time Complexity** | `O(N)` total (`O(1)` amortized per element) | `O(1)` strict per operation | Pointer updates and hash lookups avoid any loops or tree traversals. |
| **Auxiliary Memory** | `O(k)` indices | `O(Capacity)` nodes and map entries | Contiguous flat buffer for deque; memory bounded by configured cache size. |
| **Worst-Case Operations** | `2 * N` deque modifications total | `O(1)` pointer adjustments | Zero variable-length search across dynamic collections. |
| **Hardware Cache Locality** | **High sequential read throughput** | Pointer chasing across linked nodes | Deque operations fit into L1 CPU cache; nodes in DLL require RAM pointer dereference. |

### Complexity Proof: Monotonic Deque Amortized Bound
- Let `N` be the length of `nums`.
- In the loop over `i = 0 ... N - 1`, index `i` is added to the back of `deque` exactly once: **`N` total push operations**.
- An index can be removed from the front at most once (when expired): at most **`N` front pops**.
- An index can be removed from the back at most once (when dominated): at most **`N` back pops**.
- Total deque operations across the entire algorithm lifetime cannot exceed `3 * N`.
- Therefore, the amortized cost per element is `3 * N / N = O(1)`, guaranteeing strict **`O(N)` total execution time**.

---

## 🎙️ Chapter 6: 45-Minute Senior / Lead Verbal Interview Script

```text
[00:00 - 05:00] Clarification & Problem Formulation
Candidate: "For Sliding Window Maximum, we have an array of length N and a window of size k.
           If k == 1, every element is its own maximum, so we return a copy of nums in O(N).
           The naive approach re-evaluates all k elements for each window, taking O(N * k).
           With N = 10^5 and k = 5 * 10^4, that requires 2.5 billion operations—guaranteed TLE.
           A heap achieves O(N log k) but has O(log k) per-step eviction overhead and cache misses.
           We can achieve strict O(N) time and O(k) space using a Monotonic Decreasing Deque."

[05:00 - 15:00] Invariant Formulation & Architecture Defense
Candidate: "The key mathematical invariant is Domination:
           If nums[j] >= nums[i] with j > i, nums[i] can never be the maximum of any window containing j,
           because nums[j] is larger and will survive longer in the stream.
           We maintain a deque of indices with strictly decreasing values:
           - Front of deque always holds the index of the current maximum.
           - We evict expired elements from the front (index < i - k + 1).
           - We evict smaller elements from the back before pushing the current index.
           Since each index enters and leaves at most once, total runtime is O(N)."

[15:00 - 30:00] Live Implementation & Sentinel Hygiene
Candidate: "For LRU Cache:
           - A hash map gives O(1) lookup: key -> Node.
           - A doubly linked list gives O(1) arbitrary removal and insertion.
           - We use dummy head and tail sentinel nodes. This eliminates all null-checks when updating
             head.next and tail.prev, ensuring zero edge-case crashes when the cache is empty or full.
           - Crucially, each node stores both key and value so we can delete the key from the hash map
             in O(1) when evicting tail.prev."

[30:00 - 40:00] Edge Cases & Dry Run
Candidate: "Let's trace edge cases:
           - k == nums.Length: Deque computes a single global maximum.
           - All strictly decreasing elements [5, 4, 3, 2, 1]: Deque fills to size k; no back-pops.
           - All strictly increasing elements [1, 2, 3, 4, 5]: Every new element clears all previous elements;
             deque size remains 1 throughout.
           - LRU Put overwrite: Updates existing key's value and moves node to head without increasing size."

[40:00 - 45:00] Systems Scaling & Concurrency Extension
Candidate: "In a multi-threaded production service:
           - Standard LRU Cache has lock contention on Get operations because even reads mutate
             the linked list (MoveToHead).
           - To scale reads, we could partition into sharded LRU stripes, use a ReadWriteLock, or adopt
             W-TinyLFU (Caffeine Cache) which uses lock-free ring buffers to record read events asynchronously."
```

---

## 🔍 Chapter 7: Granular Edge-Case & Invariant Verification Table

| Test Case | Scenario | Expected Outcome | Invariant Handling |
| :--- | :--- | :--- | :--- |
| **`k == 1`** | `nums = [4, -2, 5], k = 1` | `[4, -2, 5]` | Fast-path guard clause returns clone immediately. |
| **`k == nums.Length`** | `nums = [1, 3, 2], k = 3` | `[3]` | Deque processes all elements; emits single result at index `k - 1`. |
| **Strictly Decreasing** | `nums = [9, 8, 7, 6], k = 2` | `[9, 8, 7]` | No back-pops; front advances by expiration. |
| **Strictly Increasing** | `nums = [1, 2, 3, 4], k = 2` | `[2, 3, 4]` | Every new element purges smaller back elements; deque size stays 1. |
| **All Duplicate Elements**| `nums = [5, 5, 5, 5], k = 2` | `[5, 5, 5]` | Handled by `<=` condition: older duplicates purged to save space. |
| **LRU Put Existing Key** | `put(1, 10); put(1, 20)` | Key `1` updated to `20`, promoted to MRU | Node value updated in-place; count does not exceed capacity. |
| **LRU Eviction Order** | `cap = 2; put(1,1); put(2,2); get(1); put(3,3)` | Key `2` evicted (LRU); key `1` retained | `get(1)` promoted node 1 to head; node 2 became `tail.prev`. |
| **LRU Capacity = 1** | `cap = 1; put(1,1); put(2,2); get(1)` | Returns `-1` (key 1 was evicted) | Eviction operates correctly even when head and tail sandwich single node. |

---

> 🧭 **Navigation:** [← Previous Day](Week_19_Day_03_Mock_Dynamic_Programming_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_19_Day_05_Weakness_Diagnosis_And_Strategy_Instructional.md)
