# 📘 WEEK 12 DAY 5: GREEDY IN SYSTEMS — ENGINEERING GUIDE

> 🧭 **Navigation:** [← Previous Day](Week_12_Day_04_Fractional_Knapsack_And_Scheduling_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Week Playbook →](WEEK_12_FULL_PLAYBOOK.md)
> 
> 💡 **Instructor Note:** *Day 5 transitions greedy algorithms from textbook drills to production architectures: Minimum Spanning Trees (Kruskal and Prim), kernel-level LRU caching, and greedy approximation for NP-hard problems like Set Cover.*

---

## 🎯 LEARNING OBJECTIVES

*By the end of this chapter, you will be able to:*

- 🌐 **Explain** why the Cut Property guarantees that greedy edge selection is safe in Minimum Spanning Trees (MST).
- ⚙️ **Implement** Kruskal's MST (with Disjoint Set Union), Prim's MST (with `PriorityQueue`/`heapq`), and an `O(1)` LRU Cache in C# (.NET 8/9) and Python (3.11+).
- ⚖️ **Contrast** Kruskal's edge-based greedy against Prim's vertex-growing greedy based on graph density (`E << V^2` vs `E ≈ V^2`).
- 🧠 **Demystify** cache eviction heuristics: explain why LRU is an online greedy surrogate for Belady's impossible clairvoyant algorithm.
- 📉 **Evaluate** greedy approximation algorithms for NP-hard problems (e.g., Set Cover's `O(log N)` factor).

---

## 📖 CHAPTER 1: CONTEXT & MOTIVATION

### The Engineering Challenge

In distributed systems and networking, you face problems where computing a globally optimal configuration is either computationally prohibitive or distributed across thousands of independent nodes:

1. **Backbone Fiber Routing (MST):** You must interconnect 10,000 data centers with fiber optic cables. What is the cheapest cable topology that ensures every data center can communicate with every other?
   - **Answer:** A **Minimum Spanning Tree (MST)**. Greedily picking the cheapest valid edges (Kruskal/Prim) guarantees an exact minimum-cost tree.
2. **High-Speed In-Memory Cache (LRU):** A Redis or kernel page cache must evict entries when capacity is full. You cannot predict future memory accesses.
   - **Answer:** **Least Recently Used (LRU)**. A greedy temporal heuristic that assumes past access frequency predicts near-future demand.
3. **Microservice Monitoring (Set Cover):** You have 1,000 metrics and 200 monitoring probes. Each probe tracks a subset of metrics. What is the smallest set of probes to deploy to cover all metrics?
   - **Answer:** NP-hard in the exact case! Greedily picking the probe that covers the most uncovered metrics guarantees an **`O(log N)` approximation**.

```
THE THREE TIERS OF GREEDY IN PRODUCTION:
+-------------------+-----------------------------------+-----------------------------------+
| Tier              | System Example                    | Guarantees                        |
+-------------------+-----------------------------------+-----------------------------------+
| 1. Exact Optimum  | Kruskal / Prim MST, Huffman       | Mathematically 100% Optimal       |
| 2. Online Heuristic| LRU Cache, Geographic Routing     | Fast O(1), No Future Guarantee    |
| 3. Approximation  | Greedy Set Cover                  | Provable O(log N) Bound for NP-hard|
+-------------------+-----------------------------------+-----------------------------------+
```

---

## 🧠 CHAPTER 2: GREEDY IN NETWORKS — MINIMUM SPANNING TREES

### The Cut Property (No Academic Jargon)

Why can we make irrevocable local edge choices without regretting them? Because of the **Cut Property**:

```
Cut Property Visualized:

          Set S (Red Nodes)           Set V \ S (Blue Nodes)
           [ Node 1 ]                    [ Node 3 ]
              |                             |
              |                             |
           [ Node 2 ]                    [ Node 4 ]
              \                             /
               \                           /
          ======\=========================/======  <-- THE CUT (Partition)
                 \                       /
             Edge A: wt = 7          Edge B: wt = 2 (CHEAPEST CROSSING EDGE!)
                 /                       \
          ======/=========================\======

Key Truth:
To connect Set S to Set V \ S, AT LEAST ONE crossing edge must be included.
If you pick Edge B (the cheapest crossing edge, wt = 2), you can NEVER regret it!
Any alternative crossing edge (like Edge A, wt = 7) would make the tree heavier.
Therefore, the lightest edge crossing ANY cut is guaranteed to be in SOME MST.
```

### Kruskal vs Prim: Two Faces of Greed

| Dimension | Kruskal's Algorithm | Prim's Algorithm |
| :--- | :--- | :--- |
| **Strategy** | Global edge greedy: sorts all edges globally | Local frontier greedy: grows a tree from a start vertex |
| **Data Structure** | Disjoint Set Union (DSU / Union-Find) | Min-Priority Queue (Binary Heap) |
| **Time Complexity**| `O(E log E)` = `O(E log V)` | `O(E log V)` with binary heap |
| **Best Used When** | **Sparse graphs** (`E ≈ V`), edge list given | **Dense graphs** (`E ≈ V^2`), adjacency list given |

```
Kruskal Mechanics (Component Merging):
1. Sort all edges: [ (1-2, 2), (2-4, 3), (1-3, 4), (1-4, 5), (2-3, 6) ]
2. Edge (1-2, 2): Connects Comp{1} and Comp{2} -> ACCEPT.
3. Edge (2-4, 3): Connects Comp{1,2} and Comp{4} -> ACCEPT.
4. Edge (1-3, 4): Connects Comp{1,2,4} and Comp{3} -> ACCEPT.
Total edges = 3 = V - 1. Finished!

Prim Mechanics (Frontier Expansion):
Start at Node 1.
Frontier edges from {1}: (1-2, 2), (1-3, 4), (1-4, 5).
1. Min edge is (1-2, 2) -> Add Node 2. MST = {1, 2}.
2. Frontier now includes edges from {1, 2}: (2-4, 3), (1-3, 4), (1-4, 5).
   Min edge is (2-4, 3) -> Add Node 4. MST = {1, 2, 4}.
3. Frontier expands to edges from {1, 2, 4}: Min edge is (1-3, 4) -> Add Node 3.
All vertices visited!
```

---

## 🧠 CHAPTER 3: GREEDY IN CACHING — LRU DESIGN

### Temporal Locality & Belady's Optimal Dilemma

A cache has fixed capacity `C`. When full, which element should be evicted?
- **Belady's Optimal Algorithm (Clairvoyant):** Evict the page that will not be used for the longest time in the future.
  - Provably optimal: minimizes total page faults.
  - **Impossible in production** because systems cannot foresee future requests!
- **LRU (Least Recently Used):** Uses past recency as a greedy proxy for future demand.
  - Assumes that if key `X` was read 1 millisecond ago, it will likely be read again soon.
  - To achieve `O(1)` get and put, we combine a **Hash Map** with a **Doubly-Linked List**.

```
ASCII LRU Cache Pointer Layout:

   [ Hash Map ]
     "user_1" ------> [ Node 1 ]
     "user_2" ------> [ Node 2 ]
     "user_3" ------> [ Node 3 ]

   [ Doubly-Linked List ]
     (Head / MRU)                                     (Tail / LRU)
     +----------+     +----------+     +----------+     +----------+
     |   HEAD   |<--->|  Node 1  |<--->|  Node 2  |<--->|   TAIL   |
     | (Dummy)  |     |("user_1")|     |("user_2")|     | (Dummy)  |
     +----------+     +----------+     +----------+     +----------+
                           ^                                 ^
                     Most Recently                    Least Recently
                         Used                              Used
                                                      (Evict Candidate!)
```

---

## ⚙️ CHAPTER 4: PRODUCTION IMPLEMENTATIONS

### Pattern 1: Kruskal's MST (with Disjoint Set Union)

#### Production C# (.NET 8/9)

```csharp
namespace DsaMastery.Systems;

using System;
using System.Collections.Generic;

public readonly record struct Edge(int U, int V, int Weight);

public sealed class DisjointSet
{
    private readonly int[] _parent;
    private readonly int[] _rank;

    public DisjointSet(int size)
    {
        _parent = new int[size];
        _rank = new int[size];
        for (int i = 0; i < size; i++)
        {
            _parent[i] = i;
        }
    }

    public int Find(int x)
    {
        if (_parent[x] != x)
        {
            _parent[x] = Find(_parent[x]); // Path compression
        }
        return _parent[x];
    }

    public bool Union(int x, int y)
    {
        int rootX = Find(x);
        int rootY = Find(y);
        if (rootX == rootY) return false;

        // Union by rank
        if (_rank[rootX] < _rank[rootY])
        {
            _parent[rootX] = rootY;
        }
        else if (_rank[rootX] > _rank[rootY])
        {
            _parent[rootY] = rootX;
        }
        else
        {
            _parent[rootY] = rootX;
            _rank[rootX]++;
        }

        return true;
    }
}

public static class KruskalAlgorithm
{
    /// <summary>
    /// Computes the Minimum Spanning Tree using Kruskal's greedy edge selection.
    /// Time Complexity: O(E log E)
    /// Space Complexity: O(V)
    /// </summary>
    public static (List<Edge> MstEdges, int TotalWeight) ComputeMst(int vertexCount, Edge[] edges)
    {
        ArgumentNullException.ThrowIfNull(edges);
        if (vertexCount <= 1) return ([], 0);

        // Sort edges by weight ascending
        Array.Sort(edges, static (a, b) => a.Weight.CompareTo(b.Weight));

        var dsu = new DisjointSet(vertexCount);
        var mst = new List<Edge>(vertexCount - 1);
        int totalWeight = 0;

        foreach (var edge in edges)
        {
            if (dsu.Union(edge.U, edge.V))
            {
                mst.Add(edge);
                totalWeight += edge.Weight;

                if (mst.Count == vertexCount - 1)
                {
                    break; // Spanning tree complete
                }
            }
        }

        if (mst.Count != vertexCount - 1)
        {
            throw new InvalidOperationException("Graph is disconnected; MST does not exist.");
        }

        return (mst, totalWeight);
    }
}
```

#### Production Python (3.11+)

```python
from typing import List, Tuple

class DisjointSet:
    def __init__(self, size: int):
        self.parent = list(range(size))
        self.rank = [0] * size

    def find(self, x: int) -> int:
        if self.parent[x] != x:
            self.parent[x] = self.find(self.parent[x])  # Path compression
        return self.parent[x]

    def union(self, x: int, y: int) -> bool:
        rx = self.find(x)
        ry = self.find(y)
        if rx == ry:
            return False

        # Union by rank
        if self.rank[rx] < self.rank[ry]:
            self.parent[rx] = ry
        elif self.rank[rx] > self.rank[ry]:
            self.parent[ry] = rx
        else:
            self.parent[ry] = rx
            self.rank[rx] += 1
        return True


def kruskal_mst(vertex_count: int, edges: List[Tuple[int, int, int]]) -> Tuple[List[Tuple[int, int, int]], int]:
    """
    Computes MST using Kruskal's algorithm.
    edges: list of (u, v, weight)
    Time Complexity: O(E log E)
    Space Complexity: O(V)
    """
    if vertex_count <= 1:
        return [], 0

    # Sort edges by weight ascending
    sorted_edges = sorted(edges, key=lambda x: x[2])
    dsu = DisjointSet(vertex_count)
    mst: List[Tuple[int, int, int]] = []
    total_weight = 0

    for u, v, weight in sorted_edges:
        if dsu.union(u, v):
            mst.append((u, v, weight))
            total_weight += weight
            if len(mst) == vertex_count - 1:
                break

    if len(mst) != vertex_count - 1:
        raise ValueError("Graph is disconnected; MST cannot be formed.")

    return mst, total_weight
```

---

### Pattern 2: Prim's MST (with PriorityQueue / Heap)

#### Production C# (.NET 8/9)

```csharp
namespace DsaMastery.Systems;

using System;
using System.Collections.Generic;

public static class PrimAlgorithm
{
    /// <summary>
    /// Computes MST using Prim's algorithm with PriorityQueue.
    /// Time Complexity: O(E log V)
    /// Space Complexity: O(V + E)
    /// </summary>
    public static (List<Edge> MstEdges, int TotalWeight) ComputeMst(int vertexCount, List<(int To, int Weight)>[] adj)
    {
        ArgumentNullException.ThrowIfNull(adj);
        if (vertexCount <= 1) return ([], 0);

        var visited = new bool[vertexCount];
        var mst = new List<Edge>(vertexCount - 1);
        int totalWeight = 0;

        // Min-heap storing (From, To, Weight) keyed by Weight
        var pq = new PriorityQueue<(int From, int To, int Weight), int>();

        void PushEdges(int u)
        {
            visited[u] = true;
            foreach (var (to, weight) in adj[u])
            {
                if (!visited[to])
                {
                    pq.Enqueue((u, to, weight), weight);
                }
            }
        }

        PushEdges(0); // Start spanning from vertex 0

        while (pq.Count > 0 && mst.Count < vertexCount - 1)
        {
            var edge = pq.Dequeue();
            if (visited[edge.To]) continue;

            mst.Add(new Edge(edge.From, edge.To, edge.Weight));
            totalWeight += edge.Weight;
            PushEdges(edge.To);
        }

        if (mst.Count != vertexCount - 1)
        {
            throw new InvalidOperationException("Graph is disconnected; MST does not exist.");
        }

        return (mst, totalWeight);
    }
}
```

#### Production Python (3.11+)

```python
import heapq
from typing import List, Tuple

def prim_mst(vertex_count: int, adj: List[List[Tuple[int, int]]], start: int = 0) -> Tuple[List[Tuple[int, int, int]], int]:
    """
    Computes MST using Prim's algorithm with min-heap.
    adj: adjacency list where adj[u] contains (v, weight)
    Time Complexity: O(E log V)
    Space Complexity: O(V + E)
    """
    if vertex_count <= 1:
        return [], 0

    visited = [False] * vertex_count
    mst: List[Tuple[int, int, int]] = []
    total_weight = 0

    # Min-heap storing (weight, from_node, to_node)
    min_heap: List[Tuple[int, int, int]] = []

    def push_edges(u: int):
        visited[u] = True
        for v, weight in adj[u]:
            if not visited[v]:
                heapq.heappush(min_heap, (weight, u, v))

    push_edges(start)

    while min_heap and len(mst) < vertex_count - 1:
        weight, u, v = heapq.heappop(min_heap)
        if visited[v]:
            continue

        mst.append((u, v, weight))
        total_weight += weight
        push_edges(v)

    if len(mst) != vertex_count - 1:
        raise ValueError("Graph is disconnected; MST cannot be formed.")

    return mst, total_weight
```

---

### Pattern 3: High-Performance LRU Cache

#### Production C# (.NET 8/9)

```csharp
namespace DsaMastery.Systems;

using System;
using System.Collections.Generic;

public sealed class LruCache<TKey, TValue> where TKey : notnull
{
    private readonly int _capacity;
    private readonly Dictionary<TKey, LinkedListNode<(TKey Key, TValue Value)>> _map;
    private readonly LinkedList<(TKey Key, TValue Value)> _list;

    public LruCache(int capacity)
    {
        if (capacity <= 0) throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity must be positive.");
        _capacity = capacity;
        _map = new Dictionary<TKey, LinkedListNode<(TKey, TValue)>>(capacity);
        _list = new LinkedList<(TKey, TValue)>();
    }

    public bool TryGet(TKey key, out TValue value)
    {
        if (_map.TryGetValue(key, out var node))
        {
            // Move accessed node to front (MRU)
            _list.Remove(node);
            _list.AddFirst(node);
            value = node.Value.Value;
            return true;
        }

        value = default!;
        return false;
    }

    public void Put(TKey key, TValue value)
    {
        if (_map.TryGetValue(key, out var existingNode))
        {
            _list.Remove(existingNode);
            var updatedNode = new LinkedListNode<(TKey, TValue)>((key, value));
            _list.AddFirst(updatedNode);
            _map[key] = updatedNode;
            return;
        }

        if (_map.Count >= _capacity)
        {
            // Evict least recently used (Tail)
            var lru = _list.Last ?? throw new InvalidOperationException("LRU list empty during eviction.");
            _list.RemoveLast();
            _map.Remove(lru.Value.Key);
        }

        var newNode = new LinkedListNode<(TKey, TValue)>((key, value));
        _list.AddFirst(newNode);
        _map[key] = newNode;
    }
}
```

#### Production Python (3.11+)

```python
from collections import OrderedDict
from typing import Generic, Optional, TypeVar

K = TypeVar('K')
V = TypeVar('V')

class LRUCache(Generic[K, V]):
    """
    O(1) LRU Cache using Python OrderedDict.
    OrderedDict maintains key insertion and access order efficiently.
    """
    def __init__(self, capacity: int):
        if capacity <= 0:
            raise ValueError("Capacity must be positive.")
        self.capacity = capacity
        self.cache: OrderedDict[K, V] = OrderedDict()

    def get(self, key: K) -> Optional[V]:
        if key not in self.cache:
            return None
        # Move key to end to denote most recently used (MRU)
        self.cache.move_to_end(key)
        return self.cache[key]

    def put(self, key: K, value: V) -> None:
        if key in self.cache:
            self.cache.move_to_end(key)
        self.cache[key] = value

        if len(self.cache) > self.capacity:
            # Evict first item (least recently used, FIFO pop)
            self.cache.popitem(last=False)
```

---

## ⚖️ CHAPTER 5: PERFORMANCE & COMPLEXITY DECONSTRUCTION

### Systems Performance Matrix

| Component | Time Complexity | Auxiliary Space | Cache Friendliness | Critical Failure Mode |
| :--- | :--- | :--- | :--- | :--- |
| **Kruskal MST** | `O(E log E)` | `O(V)` | High (flat edge array) | Disconnected graphs (check `Count == V - 1`) |
| **Prim MST** | `O(E log V)` | `O(V + E)` | Moderate (pointer chasing) | Dense graph memory explosion |
| **LRU Cache** | `O(1)` per op | `O(Capacity)` | Low (linked node hopping) | Thrashing under looping scan patterns |
| **Greedy Set Cover**| `O(U * S)` | `O(U)` | High (hash set bitsets) | Suboptimal on adversarial subset overlaps |

#### Architectural Takeaways:
1. **Cache Thrashing:** If a sequential access loop reads `K + 1` distinct elements repeatedly into a cache of capacity `K`, LRU achieves a **0% hit rate**! Modern databases (e.g., PostgreSQL buffer pool, Linux page cache) use **2Q** or **LRU-K** to guard against sequential scan thrashing.
2. **Kruskal's DSU Nearly O(1) Operations:** With both Path Compression and Union by Rank, the Inverse Ackermann function `α(V) <= 4` for all practical values of `V < 10^80`. DSU operations are practically instantaneous.

---

## 🎙️ CHAPTER 6: 45-MINUTE INTERVIEW VERBAL SCRIPT

```
[00:00 - 05:00] Clarifying Systems Problem Scoping
"Let's clarify the scenario:
 1. For MST: Is the graph guaranteed connected? Are edge weights positive, negative, or zero?
    (MST works identically with negative weights!).
 2. For LRU Cache: What are our thread-safety requirements?
    What capacity constraints apply? All get and put operations must execute in strict O(1) time."

[05:00 - 12:00] Proving the Correctness of MST Greediness
"Why does greedy edge selection work for MST without backtracking?
 It relies directly on the Cut Property: For any partition of vertices into two non-empty sets S
 and V \ S, the minimum-weight edge crossing the cut must belong to some MST.
 Kruskal iteratively connects disconnected components using the globally cheapest edge.
 Prim iteratively extends a single cut boundary using the locally cheapest frontier edge.
 Neither algorithm ever needs to undo a choice."

[12:00 - 25:00] Architecture Selection: Kruskal vs Prim
"Which algorithm do we choose?
 If the graph is sparse (E ≈ V), Kruskal's edge sorting takes O(E log E) and DSU operations
 run in nearly linear time O(E * α(V)). Kruskal is clean and memory-efficient.
 If the graph is dense (E ≈ V^2), Prim's algorithm runs in O(E log V) or O(V^2) with an
 adjacency matrix, avoiding the need to sort V^2 edges upfront.
 I will implement Kruskal using Disjoint Set Union with Path Compression and Union by Rank."

[25:00 - 35:00] Coding LRU Cache & MST
"For the LRU Cache, we combine a Hash Map with a Doubly-Linked List.
 The map provides O(1) key lookup to node pointers.
 The doubly-linked list provides O(1) node detachment and head insertion without array shifts.
 Let's write the C# / Python implementation..."

[35:00 - 42:00] Complexity Verification
"Kruskal: Edge sort takes O(E log E). DSU Find/Union takes O(E * α(V)). Total time: O(E log E).
 Space: O(V) for DSU parent and rank arrays.
 LRU Cache: get(key) is O(1) map lookup + O(1) node reparenting.
 put(key, val) is O(1) map insert + O(1) eviction of tail node. Space is strictly O(Capacity)."

[42:00 - 45:00] Edge Cases & Systems Defenses
"Edge cases covered:
 - Disconnected Graph: Detected when mst.Count < V - 1; throws descriptive exception.
 - Cache Capacity = 1: Safely evicts previous entry on second insert.
 - Re-inserting existing key: Updates value in place and bumps node to MRU head."
```

---

## ⚔️ SUPPLEMENTARY OUTCOMES

### 🏋️ Practice Problems

| # | Problem | Source | Difficulty | Key Concept |
| :--- | :--- | :--- | :--- | :--- |
| 1 | Min Cost to Connect All Points | LeetCode 1584 | Medium | Kruskal / Prim on complete Manhattan graph |
| 2 | LRU Cache | LeetCode 146 | Medium | Hash Map + Doubly Linked List |
| 3 | LFU Cache | LeetCode 460 | Hard | Dual Hash Map with frequency buckets |
| 4 | Connecting Cities With Minimum Cost | LeetCode 1135 | Medium | Kruskal MST with DSU |
| 5 | Optimize Water Distribution in a Village | LeetCode 1168 | Hard | Virtual source node + MST |

### 🎙️ Interview Questions

1. **Q:** What is the difference between an MST and a Shortest Path Tree (Dijkstra)?
   - *Answer:* An MST minimizes the total weight of *all* edges combined. A Shortest Path Tree minimizes the distance from a *single source* to each individual destination.
2. **Q:** Why does Dijkstra fail with negative edges, but Kruskal and Prim succeed?
   - *Answer:* Dijkstra assumes that extending a path cannot decrease its total distance (greedy stays ahead). MST algorithms only compare individual edge weights across cuts; negative weights simply represent cheaper edges.
3. **Q:** Why does LRU use a Doubly-Linked List instead of a Singly-Linked List?
   - *Answer:* Removing a node in `O(1)` requires access to `node.Prev` to stitch `prev.Next = next`. A singly-linked list would require an `O(N)` scan from the head to find the predecessor.

---

**End of Week 12 Day 05 Instructional File**

---
> 🧭 **Navigation:** [← Previous Day](Week_12_Day_04_Fractional_Knapsack_And_Scheduling_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Week Playbook →](WEEK_12_FULL_PLAYBOOK.md)
