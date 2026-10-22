# 📘 WEEK 19 DAY 04: MOCK ROUND 4 — MIXED COMPLEX SYSTEMS & IN-MEMORY ARCHITECTURE

> 🧭 **Navigation:** [← Previous Day: Dynamic Programming Mock](Week_19_Day_03_Mock_Dynamic_Programming_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day: Weakness Diagnosis & Calibration →](Week_19_Day_05_Weakness_Diagnosis_And_Strategy_Instructional.md)
> 
> 💡 **Instructor Note:** *In senior and staff interview loops, interviewers intentionally blend algorithm design with systems architecture. Today, we simulate high-stakes complex design rounds: **Design In-Memory File System (LeetCode 588)** — testing hierarchical trie decomposition; **LRU Cache (LeetCode 146)** — testing custom doubly-linked lists with sentinel nodes; and **Median of Two Sorted Arrays (LeetCode 4)** — testing logarithmic binary search partition invariants.*

---

## 🎯 LEARNING OBJECTIVES

*By the end of this module, you will be able to:*

- 🎬 **Simulate** an authentic 45-minute senior interview blending object-oriented system design and algorithmic invariants.
- 🪜 **Master** the sentinel-node Doubly Linked List + HashMap pattern for guaranteed `O(1)` LRU operations with zero edge-case null checks.
- 🎨 **Visualize** the logarithmic partition invariant in Median of Two Sorted Arrays: `L1 <= R2 && L2 <= R1`.
- 🛑 **Avoid** fatal rookie traps: using standard library `LinkedList<T>` with GC wrapper allocations, crashing on root `/` path splits, and binary searching the longer array.
- 🎙️ **Deliver** the exact 3-sentence verbal talk track defending in-memory data layout and concurrency.
- ⚡ **Implement** production-grade, zero-leak implementations in C# (.NET 8/9) and Python (3.11+).

---

## 🎬 PILLAR 1: THE 60-SECOND HOOK & EVERYDAY ANALOGY

### The Librarian's Desk (LRU) & The Shorter Partition (Median)

#### 1. The LRU Cache: The Physical Desk Surface
Imagine you have a small research desk that holds at most **3 books**:
- When you read a book, you place it at the **very front of the desk (Most Recently Used)**.
- If you read a book that is already on the desk, you pick it up and move it to the front.
- When a 4th book arrives, your desk is full! What happens?
  - You don't inspect publishing dates or page counts.
  - You simply push the book sitting at the **very back of the desk (Least Recently Used)** off into the return bin!

```text
The LRU Data Structure Marriage:
1. A Doubly Linked List:
   Allows removing an item from the middle and splicing it to the head in O(1) time.
2. A Hash Map:
   Maps Key -> Direct Node Pointer in O(1) time!
Result: Both Get() and Put() run in strictly O(1) time!
```

---

#### 2. Median of Two Sorted Arrays: The Balance Scale

Suppose you have two sorted arrays of numbers: Array A (length 4) and Array B (length 6).
You want to find the median in `O(log(min(M, N)))` time without merging them (`O(M + N)`).

Think of a **Balance Scale**:
- You must cut Array A at partition `i` and cut Array B at partition `j` such that:
  - Exactly half the total elements lie on the **Left Side**, and half lie on the **Right Side**.
  - Every single element on the Left Side is `<= ` every single element on the Right Side!

```text
Left Side (5 elements):              Right Side (5 elements):
A[0 ... i-1]  (max is L1)            A[i ... M-1]  (min is R1)
B[0 ... j-1]  (max is L2)            B[j ... N-1]  (min is R2)
```

The condition for perfection:
`L1 <= R2` AND `L2 <= R1`!
If `L1 > R2`: your cut in A is too far right! Move `i` left via Binary Search!
If `L2 > R1`: your cut in A is too far left! Move `i` right!
By binary searching strictly on the **shorter array**, the median is found in **`O(log(min(M, N)))` time**!

---

## 🪜 PILLAR 2: THE SCAFFOLDING LADDER & TOY NAPKIN TRACE

### Step 1: LRU Cache Sentinel Node Architecture

The biggest source of bugs in an LRU Cache interview is handling `head` and `tail` null pointer exceptions.
**The Senior Architect Pattern:** Use **two dummy sentinel nodes**: `head` and `tail`!

```text
[Head Sentinel] <---> [Node A] <---> [Node B] <---> [Tail Sentinel]
```

- An empty cache is simply: `head.next = tail; tail.prev = head;`.
- Adding a node `x` to head:
  `x.next = head.next; x.prev = head; head.next.prev = x; head.next = x;`.
- Removing node `x`:
  `x.prev.next = x.next; x.next.prev = x.prev;`.
- **Zero `if (node == null)` checks! Zero edge cases! Flawless in 20 lines!**

---

### Step 2: Median of Two Sorted Arrays Partition Trace

Let `A = [1, 3]` (`M = 2`) and `B = [2]` (`N = 1`).
1. Always binary search on the shorter array: Swap if `A.Length > B.Length` -> Search on `B` (`M = 1`, `N = 2`).
2. Total length = `3`. Left half count = `(1 + 2 + 1) / 2 = 2`.
3. Binary search `low = 0, high = 1`:
   - Cut `i = 0`: Left of B has 0 elements (`L1 = -inf`), Right has `[2]` (`R1 = 2`).
   - Cut `j = 2 - 0 = 2`: Left of A has `[1, 3]` (`L2 = 3`), Right has empty (`R2 = +inf`).
   - Check: `L2 <= R1` (`3 <= 2`)? **FALSE!** `i` is too far left; advance `low = 1`.
   - Next iteration: `i = 1`:
     - `L1 = 2`, `R1 = +inf`.
     - `j = 2 - 1 = 1`: `L2 = 1`, `R2 = 3`.
     - Check: `L1 <= R2` (`2 <= 3`) YES! `L2 <= R1` (`1 <= +inf`) YES!
4. Perfect cut found!
   Total length is odd -> Median = `max(L1, L2) = max(2, 1) = 2.0`!

---

### 🧠 Pause & Predict Micro-Drills

> **Drill 1:** Why do we strictly binary search on the **shorter** array in Median of Two Sorted Arrays?
> *Pause here and think!*
>
> **Answer:** 
> 1. It minimizes search time to `O(log(min(M, N)))`.
> 2. It mathematically guarantees that `j = (M + N + 1) / 2 - i` is always a valid index in `[0 ... N]`, completely preventing array index out-of-bounds exceptions!

> **Drill 2:** In an in-memory file system, how do you handle splitting path `"/a/b/c"` without creating empty string tokens?
> *Pause here and think!*
>
> **Answer:**
> Use `path.Split('/', StringSplitOptions.RemoveEmptyEntries)`.
> Leading, trailing, and duplicate slashes (`"//a///b/"`) are cleanly parsed into `["a", "b"]`!

---

## 🎨 PILLAR 3: PRAGMATIC VISUAL MODELS & DECISION FLOWS

### Modern Hex Flowchart: LRU Cache Mutation Engine

```mermaid
%%{init: {"flowchart": {"htmlLabels": true, "padding": 20}, "themeCSS": "foreignObject { overflow: visible !important; }"}}%%
flowchart TD
    Req{"Operation"} -->|Get(Key)| G1{"Key in HashMap?"}
    G1 -->|No| GFail["Return -1"]
    G1 -->|Yes| GProm["1. Unlink node from list<br/>2. Splice node directly after Head Sentinel<br/>3. Return node.val"]

    Req -->|Put(Key, Val)| P1{"Key already exists?"}
    P1 -->|Yes| PUpdate["1. Update node.val<br/>2. Promote node to Head Sentinel"]
    P1 -->|No: New Node| PCap{"Is Count == Capacity?"}
    PCap -->|Yes: Cache Full| PEvict["1. Target = tail.prev (LRU node)<br/>2. Unlink Target<br/>3. Remove from HashMap<br/>Count--"]
    PEvict --> PInsert
    PCap -->|No| PInsert["1. Create Node(Key, Val)<br/>2. Insert after Head Sentinel<br/>3. Add to HashMap<br/>Count++"]

    style Req fill:#e1f5fe,stroke:#0288d1,color:#01579b
    style G1 fill:#fff3e0,stroke:#ef6c00,color:#e65100
    style GFail fill:#ffebee,stroke:#c62828,color:#b71c1c
    style GProm fill:#e8f5e9,stroke:#2e7d32,color:#1b5e20
    style P1 fill:#fff3e0,stroke:#ef6c00,color:#e65100
    style PUpdate fill:#e8f5e9,stroke:#2e7d32,color:#1b5e20
    style PCap fill:#fff3e0,stroke:#ef6c00,color:#e65100
    style PEvict fill:#ffebee,stroke:#c62828,color:#b71c1c
    style PInsert fill:#e8f5e9,stroke:#2e7d32,color:#1b5e20
```

---

## 🛑 PILLAR 4: THE ROOKIE TRAP ZONE

### Side-by-Side Comparison: Blunders vs. Senior Architect Patterns

| Dimension | ❌ Rookie Blunder | ✅ Senior Architect Pattern |
| :--- | :--- | :--- |
| **LRU List Implementation** | Uses standard C# `LinkedList<T>`, requiring `LinkedListNode<T>` allocations on every write and breaking `O(1)` node lookup. | Codes an internal custom `Node` class with `prev` and `next` pointers, storing raw node references directly in the hash map. |
| **Sentinel Node Hygiene** | Writes complex `if (head == null)` and `if (tail == null)` branches, introducing subtle pointer detachment bugs. | Uses dummy `head` and `tail` sentinel nodes, guaranteeing all active nodes always have non-null neighbors. |
| **Median Array Sizing** | Binary searches on the longer array `A`, causing calculated index `j` to fall out of bounds on array `B`. | Swaps inputs to guarantee `A` is the shorter array (`A.Length <= B.Length`), ensuring `j` is strictly within bounds. |
| **Infinity Boundaries** | Uses `0` or `-1` for empty partitions in Median, corrupting negative number comparisons. | Uses `int.MinValue` for missing left partitions (`L1, L2`) and `int.MaxValue` for missing right partitions (`R1, R2`). |
| **File System Directory vs File**| Treats files and directories with different classes, causing casting errors on shared tree traversal. | Uses a single composite `Node` with an `IsFile` boolean flag and child collection. |

---

## 🎙️ PILLAR 5: 45-MINUTE INTERVIEW TALK TRACK

### The Exact 3-Sentence Verbal Script

Speak these exact three sentences before writing code:

```text
+--------------------------------------------------------------------------------------------------+
|                              THE SENIOR MIXED SYSTEMS TALK TRACK                                 |
+--------------------------------------------------------------------------------------------------+
| 1. The LRU Invariant:                                                                            |
|    "To achieve strictly O(1) time complexity for both Get and Put in LRU Cache, we marry a       |
|    HashMap for constant-time key lookup with a custom Doubly Linked List flanked by dummy head   |
|    and tail sentinels to eliminate all edge-case null checks."                                    |
|                                                                                                  |
| 2. The Logarithmic Partition Invariant:                                                          |
|    "In Median of Two Sorted Arrays, we avoid merging the arrays in O(M + N) by performing a      |
|    binary search on the shorter array's partition line, verifying that the maximum left elements  |
|    do not exceed the minimum right elements in O(log(min(M, N))) time."                          |
|                                                                                                  |
| 3. The Hierarchical Tree Invariant:                                                              |
|    "For the In-Memory File System, we model directories as a Trie-like composite tree, parsing    |
|    paths via delimiter tokenization and guaranteeing sorted directory listings via an internal   |
|    balanced dictionary."                                                                         |
+--------------------------------------------------------------------------------------------------+
```

---

## ⚡ PILLAR 6: PRODUCTION DUAL-LANGUAGE IMPLEMENTATIONS

### C# Primary Implementation (.NET 8/9): Production LRU Cache & Median Solver

```csharp
using System;
using System.Collections.Generic;

namespace AdvancedDataStructures;

/// <summary>
/// Production LRU Cache (LeetCode 146) with O(1) Get and Put.
/// Uses custom Doubly Linked List with dummy head and tail sentinel nodes.
/// </summary>
public sealed class LRUCache
{
    private sealed class Node
    {
        public int Key { get; }
        public int Value { get; set; }
        public Node? Prev { get; set; }
        public Node? Next { get; set; }

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
        _capacity = capacity;
        _map = new Dictionary<int, Node>(capacity);
        _head = new Node();
        _tail = new Node();
        _head.Next = _tail;
        _tail.Prev = _head;
    }

    private void RemoveNode(Node node)
    {
        node.Prev!.Next = node.Next;
        node.Next!.Prev = node.Prev;
    }

    private void AddToHead(Node node)
    {
        node.Next = _head.Next;
        node.Prev = _head;
        _head.Next!.Prev = node;
        _head.Next = node;
    }

    public int Get(int key)
    {
        if (!_map.TryGetValue(key, out Node? node))
            return -1;

        RemoveNode(node);
        AddToHead(node);
        return node.Value;
    }

    public void Put(int key, int value)
    {
        if (_map.TryGetValue(key, out Node? existingNode))
        {
            existingNode.Value = value;
            RemoveNode(existingNode);
            AddToHead(existingNode);
            return;
        }

        if (_map.Count == _capacity)
        {
            Node lru = _tail.Prev!;
            RemoveNode(lru);
            _map.Remove(lru.Key);
        }

        Node newNode = new(key, value);
        _map[key] = newNode;
        AddToHead(newNode);
    }
}

public static class MixedSystemsMockSolver
{
    /// <summary>
    /// Finds Median of Two Sorted Arrays (LeetCode 4) in O(log(min(M, N))) time and O(1) space.
    /// </summary>
    public static double FindMedianSortedArrays(ReadOnlySpan<int> nums1, ReadOnlySpan<int> nums2)
    {
        // Enforce binary search on shorter array
        if (nums1.Length > nums2.Length)
            return FindMedianSortedArrays(nums2, nums1);

        int m = nums1.Length;
        int n = nums2.Length;
        int low = 0;
        int high = m;

        while (low <= high)
        {
            int i = (low + high) / 2;
            int j = (m + n + 1) / 2 - i;

            int l1 = (i == 0) ? int.MinValue : nums1[i - 1];
            int r1 = (i == m) ? int.MaxValue : nums1[i];

            int l2 = (j == 0) ? int.MinValue : nums2[j - 1];
            int r2 = (j == n) ? int.MaxValue : nums2[j];

            if (l1 <= r2 && l2 <= r1)
            {
                // Correct partition achieved
                if ((m + n) % 2 == 1)
                {
                    return Math.Max(l1, l2);
                }
                else
                {
                    return (Math.Max(l1, l2) + Math.Min(r1, r2)) / 2.0;
                }
            }
            else if (l1 > r2)
            {
                high = i - 1; // Partition in nums1 too far right
            }
            else
            {
                low = i + 1; // Partition in nums1 too far left
            }
        }

        throw new ArgumentException("Input arrays are not sorted.");
    }
}
```

---

### Python Secondary Implementation (Python 3.11+): Complete LRU & Median Solvers

```python
from typing import List

class Node:
    __slots__ = ('key', 'value', 'prev', 'next')
    def __init__(self, key: int = 0, value: int = 0):
        self.key: int = key
        self.value: int = value
        self.prev = None
        self.next = None

class LRUCache:
    """
    Production-grade LRU Cache in Python with O(1) Get and Put.
    """
    def __init__(self, capacity: int):
        self.capacity: int = capacity
        self.map = {}
        self.head = Node()
        self.tail = Node()
        self.head.next = self.tail
        self.tail.prev = self.head

    def _remove(self, node: Node) -> None:
        node.prev.next = node.next
        node.next.prev = node.prev

    def _add_to_head(self, node: Node) -> None:
        node.next = self.head.next
        node.prev = self.head
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

        if len(self.map) == self.capacity:
            lru = self.tail.prev
            self._remove(lru)
            del self.map[lru.key]

        new_node = Node(key, value)
        self.map[key] = new_node
        self._add_to_head(new_node)

class MixedSystemsMockSolver:
    @staticmethod
    def find_median_sorted_arrays(nums1: List[int], nums2: List[int]) -> float:
        """Solves Median of Two Sorted Arrays in O(log(min(M, N))) time."""
        if len(nums1) > len(nums2):
            return MixedSystemsMockSolver.find_median_sorted_arrays(nums2, nums1)

        m, n = len(nums1), len(nums2)
        low, high = 0, m

        while low <= high:
            i = (low + high) // 2
            j = (m + n + 1) // 2 - i

            l1 = float('-inf') if i == 0 else nums1[i - 1]
            r1 = float('inf') if i == m else nums1[i]

            l2 = float('-inf') if j == 0 else nums2[j - 1]
            r2 = float('inf') if j == n else nums2[j]

            if l1 <= r2 and l2 <= r1:
                if (m + n) % 2 == 1:
                    return float(max(l1, l2))
                else:
                    return (max(l1, l2) + min(r1, r2)) / 2.0
            elif l1 > r2:
                high = i - 1
            else:
                low = i + 1

        raise ValueError("Inputs are not sorted.")
```

---

## 📊 EXPLICIT COMPLEXITY DECONSTRUCTION

| Problem & Architecture | Time Complexity | Auxiliary Space | Key Invariant |
| :--- | :--- | :--- | :--- |
| **LRU Cache (Get / Put)** | **`O(1)`** | `O(Capacity)` | Sentinel Doubly Linked List + HashMap. |
| **Median of Two Sorted Arrays**| **`O(log(min(M, N)))`**| **`O(1)`** | Binary search cut on shorter array: `L1 <= R2 && L2 <= R1`. |
| **In-Memory File System** | `O(Path Length)` | `O(Total Inodes)` | Composite Node Trie with sorted directory keys. |

---

## 🔍 GRANULAR EDGE-CASE & INVARIANT VERIFICATION TABLE

| Edge Case Scenario | Concrete Input | Expected Behavior | Invariant Verification & Handling |
| :--- | :--- | :--- | :--- |
| **LRU Single Capacity** | `capacity = 1`, `put(1, 1); put(2, 2)` | Evicts `1`, retains only `2`. | Head/tail sentinels handle single-node eviction cleanly. |
| **Median One Array Empty** | `nums1 = []`, `nums2 = [1, 2, 3]` | Returns `2.0`. | `m = 0`, resolves directly against `nums2` median. |
| **Median Even Length Elements** | `nums1 = [1, 2]`, `nums2 = [3, 4]` | Returns `2.5` (`(2 + 3) / 2.0`). | Even parity averages `max(L1, L2)` and `min(R1, R2)`. |
| **Extreme Value Disparity** | `nums1 = [1000]`, `nums2 = [1, 2, 3]` | Returns `2.5`. | Partition cuts adapt smoothly across value gaps. |
| **Path Slashes in File System**| `"/a//b///c"` | Parsed cleanly to `["a", "b", "c"]`. | `RemoveEmptyEntries` ignores redundant slashes. |

---

> 🧭 **Navigation:** [← Previous Day: Dynamic Programming Mock](Week_19_Day_03_Mock_Dynamic_Programming_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day: Weakness Diagnosis & Calibration →](Week_19_Day_05_Weakness_Diagnosis_And_Strategy_Instructional.md)
