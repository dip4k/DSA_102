# 📘 Week 15, Day 3: Network Flow Fundamentals — Edmonds-Karp & Min-Cut Theorem

> 🧭 **Navigation:** [← Previous Day](Week_15_Day_02_Segment_Trees_Range_Queries_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_15_Day_04_Network_Flow_Applications_Instructional.md)
> 
> 💡 **Instructor Note:** *Not all sections or topics are mandatory. Focus on the physical intuition of residual graphs: forward edges consume capacity; backward edges represent the ability to undo prior flow decisions.*

---

## 🎯 Learning Objectives

*   **Residual Graph Mental Model**: Master how residual graphs model both forward available capacity and backward undo capabilities.
*   **The Edmonds-Karp Guarantee**: Understand why BFS-based augmenting paths guarantee polynomial termination in `O(V * E^2)` time, avoiding the pathological exponential worst-cases of DFS (Ford-Fulkerson).
*   **Max-Flow Min-Cut Duality**: Formulate and prove how the bottleneck capacity across the minimum `s-t` cut equals the maximum flow value, and implement exact cut extraction.
*   **Dual-Language Production Code**: Implement robust C# (.NET 8/9) and Python (3.11+) solvers with full edge-level residual tracking and cut partition extraction.

---

## 📖 Chapter 1: Context & Motivation

### 1. The Engineering Challenge: The Network Routing Bottleneck

Consider a distributed telemetry pipeline or cloud CDN routing video traffic from an ingestion origin (Source `S`) to an edge distribution gateway (Sink `T`) through intermediate routers.
Each link between routers has a bandwidth capacity (Gigabits per second). What is the maximum throughput that can be routed across the network simultaneously without exceeding any link capacity?

A greedy heuristic fails immediately: picking the widest path first can consume critical intermediate edges, starving alternative paths and producing a sub-optimal global throughput.

```text
       [Node A]
     10 /      \ 10
       /   1    \
(Source S) ----> (Sink T)
       \   1    /
     10 \      / 10
       [Node B]
```

If a greedy algorithm pushes flow through a cross-cutting diagonal edge, it blocks two separate high-capacity pathways.
**Network Flow** solves this globally by introducing **residual backward edges**: whenever `f` units of flow are sent along `(u -> v)`, a backward edge `(v -> u)` with residual capacity `f` is added to the residual graph. Future augmenting paths can push flow backward, effectively **redirecting** prior flow to alternative routes.

---

## 🧠 Chapter 2: Mental Model & Mathematical Foundations

### 1. The Three Governing Flow Invariants

A valid flow `f : V x V -> R` in a flow network with capacities `c(u, v)` satisfies three strict invariants:

1. **Capacity Constraint**: Flow on an edge cannot exceed its physical capacity:
   `0 <= f(u, v) <= c(u, v)  for all u, v in V`
2. **Skew Symmetry (Residual Form)**: Net flow is directional; canceling flow in reverse equals negative forward flow:
   `f(u, v) = -f(v, u)`
   Residual capacity `c_f(u, v)` is defined as:
   `c_f(u, v) = c(u, v) - f(u, v)`
   `c_f(v, u) = f(u, v)`
3. **Flow Conservation**: For every intermediate vertex `u != s, t`, total incoming flow equals total outgoing flow:
   `sum_{v} f(v, u) = sum_{w} f(u, w)`

### 2. Max-Flow Min-Cut Theorem

An **`s-t` Cut** is a partition of vertices `V` into two disjoint subsets `S` and `T` such that `s in S` and `t in T`.
The **Capacity of a Cut `(S, T)`** is the sum of capacities of edges originating in `S` and ending in `T`:
`cap(S, T) = sum_{u in S, v in T} c(u, v)`

**The Min-Cut Theorem**:
The maximum flow from source `s` to sink `t` strictly equals the minimum capacity of any `s-t` cut separating `s` and `t`:
`max |f| = min cap(S, T)`

```text
    [ SOURCE SET S ]                   [ SINK SET T ]
   +----------------+                 +----------------+
   |   (Source s)   | === c(s, v1) => |    (Node v1)   |
   |                |                 |                |
   |   (Node u1)    | === c(u1, t) => |    (Sink t)    |
   +----------------+                 +----------------+
              \                              /
               \--- BOTTLENECK CUT EDGES ---/
```

After maximum flow is reached, the residual graph contains no path from `s` to `t`. Running BFS from `s` on the final residual graph identifies:
- Set `S`: all vertices reachable from `s` via edges with `residual_capacity > 0`.
- Set `T`: all unreachable vertices (`V \ S`).
- Cut Edges: all original edges `(u, v)` where `u in S`, `v in T`, and `f(u, v) == c(u, v)` (saturated).

---

## 💻 Chapter 3: Production-Grade Dual-Language Implementations

### 1. Modern C# Implementation (.NET 8/9)

```csharp
using System;
using System.Collections.Generic;

namespace AdvancedFlow;

public class FlowNetworkResult
{
    public int MaxFlow { get; init; }
    public HashSet<int> SourcePartition { get; init; } = new();
    public HashSet<int> SinkPartition { get; init; } = new();
    public List<(int From, int To, int Capacity)> CutEdges { get; init; } = new();
}

public class EdmondsKarpMaxFlow
{
    private readonly int _nodes;
    private readonly int[,] _capacity;
    private readonly int[,] _residual;
    private readonly List<int>[] _adj;

    public EdmondsKarpMaxFlow(int nodes)
    {
        if (nodes <= 0) throw new ArgumentOutOfRangeException(nameof(nodes));
        _nodes = nodes;
        _capacity = new int[nodes, nodes];
        _residual = new int[nodes, nodes];
        _adj = new List<int>[nodes];
        for (int i = 0; i < nodes; i++) _adj[i] = new List<int>();
    }

    /// <summary>
    /// Adds a directed edge with specified capacity from u to v.
    /// </summary>
    public void AddEdge(int u, int v, int cap)
    {
        if (cap < 0) throw new ArgumentException("Capacity must be non-negative.");
        _capacity[u, v] += cap;
        _residual[u, v] += cap;
        _adj[u].Add(v);
        _adj[v].Add(u); // Residual backward link
    }

    /// <summary>
    /// Computes Maximum Flow and extracts the Minimum s-t Cut in O(V * E^2) time.
    /// </summary>
    public FlowNetworkResult ComputeMaxFlowAndMinCut(int source, int sink)
    {
        if (source < 0 || source >= _nodes || sink < 0 || sink >= _nodes || source == sink)
            throw new ArgumentException("Invalid source or sink vertex.");

        int maxFlow = 0;
        int[] parent = new int[_nodes];

        // Step 1: Augment along shortest paths using BFS
        while (BfsAugmentingPath(source, sink, parent))
        {
            int pathFlow = int.MaxValue;

            // Find bottleneck capacity along path
            for (int v = sink; v != source; v = parent[v])
            {
                int u = parent[v];
                pathFlow = Math.Min(pathFlow, _residual[u, v]);
            }

            // Apply residual updates
            for (int v = sink; v != source; v = parent[v])
            {
                int u = parent[v];
                _residual[u, v] -= pathFlow;
                _residual[v, u] += pathFlow;
            }

            maxFlow += pathFlow;
        }

        // Step 2: Extract Minimum Cut via BFS on final residual network
        var sourceSet = new HashSet<int>();
        var queue = new Queue<int>();
        sourceSet.Add(source);
        queue.Enqueue(source);

        while (queue.Count > 0)
        {
            int u = queue.Dequeue();
            foreach (int v in _adj[u])
            {
                if (!sourceSet.Contains(v) && _residual[u, v] > 0)
                {
                    sourceSet.Add(v);
                    queue.Enqueue(v);
                }
            }
        }

        var sinkSet = new HashSet<int>();
        for (int i = 0; i < _nodes; i++)
        {
            if (!sourceSet.Contains(i)) sinkSet.Add(i);
        }

        var cutEdges = new List<(int From, int To, int Capacity)>();
        foreach (int u in sourceSet)
        {
            foreach (int v in _adj[u])
            {
                if (sinkSet.Contains(v) && _capacity[u, v] > 0)
                {
                    cutEdges.Add((u, v, _capacity[u, v]));
                }
            }
        }

        return new FlowNetworkResult
        {
            MaxFlow = maxFlow,
            SourcePartition = sourceSet,
            SinkPartition = sinkSet,
            CutEdges = cutEdges
        };
    }

    private bool BfsAugmentingPath(int source, int sink, int[] parent)
    {
        Array.Fill(parent, -1);
        parent[source] = source;

        var queue = new Queue<int>();
        queue.Enqueue(source);

        while (queue.Count > 0)
        {
            int u = queue.Dequeue();

            foreach (int v in _adj[u])
            {
                if (parent[v] == -1 && _residual[u, v] > 0)
                {
                    parent[v] = u;
                    if (v == sink) return true;
                    queue.Enqueue(v);
                }
            }
        }

        return false;
    }
}
```

---

### 2. Idiomatic Python Implementation (Python 3.11+)

```python
from collections import deque
from dataclasses import dataclass, field
from typing import List, Set, Tuple

@dataclass
class FlowResult:
    max_flow: int
    source_partition: Set[int] = field(default_factory=set)
    sink_partition: Set[int] = field(default_factory=set)
    cut_edges: List[Tuple[int, int, int]] = field(default_factory=list)

class EdmondsKarp:
    """
    Edmonds-Karp Max Flow and Min-Cut solver on residual networks.
    Time Complexity: O(V * E^2) | Space Complexity: O(V^2 + E)
    """
    def __init__(self, nodes: int):
        if nodes <= 0:
            raise ValueError("Node count must be positive.")
        self.n = nodes
        self.capacity = [[0] * nodes for _ in range(nodes)]
        self.residual = [[0] * nodes for _ in range(nodes)]
        self.adj: List[List[int]] = [[] for _ in range(nodes)]

    def add_edge(self, u: int, v: int, cap: int) -> None:
        """Adds directed edge from u to v with specified capacity."""
        if cap < 0:
            raise ValueError("Capacity must be non-negative.")
        self.capacity[u][v] += cap
        self.residual[u][v] += cap
        self.adj[u].append(v)
        self.adj[v].append(u)

    def compute_max_flow_and_min_cut(self, source: int, sink: int) -> FlowResult:
        """Computes Maximum Flow and extracts the bottleneck Minimum Cut."""
        if not (0 <= source < self.n and 0 <= sink < self.n) or source == sink:
            raise ValueError("Invalid source or sink vertex.")

        max_flow = 0
        parent = [-1] * self.n

        def bfs() -> bool:
            parent[:] = [-1] * self.n
            parent[source] = source
            q = deque([source])
            while q:
                u = q.popleft()
                for v in self.adj[u]:
                    if parent[v] == -1 and self.residual[u][v] > 0:
                        parent[v] = u
                        if v == sink:
                            return True
                        q.append(v)
            return False

        # Step 1: Augment flow along shortest path
        while bfs():
            path_flow = float('inf')
            curr = sink
            while curr != source:
                p = parent[curr]
                path_flow = min(path_flow, self.residual[p][curr])
                curr = p

            curr = sink
            while curr != source:
                p = parent[curr]
                self.residual[p][curr] -= path_flow
                self.residual[curr][p] += path_flow
                curr = p

            max_flow += int(path_flow)

        # Step 2: Min-Cut partition extraction
        source_set: Set[int] = set()
        q = deque([source])
        source_set.add(source)
        while q:
            u = q.popleft()
            for v in self.adj[u]:
                if v not in source_set and self.residual[u][v] > 0:
                    source_set.add(v)
                    q.append(v)

        sink_set = {i for i in range(self.n) if i not in source_set}
        cut_edges = []
        for u in source_set:
            for v in self.adj[u]:
                if v in sink_set and self.capacity[u][v] > 0:
                    cut_edges.append((u, v, self.capacity[u][v]))

        return FlowResult(
            max_flow=max_flow,
            source_partition=source_set,
            sink_partition=sink_set,
            cut_edges=cut_edges
        )
```

---

## 📘 Chapter 4: Performance, Invariants & Systems Architecture

### 1. Exact Governing Invariants

*   **Monotonic Distance Invariant (Edmonds-Karp)**: For every vertex `v`, the shortest-path distance `d(s, v)` in the residual network never decreases after any flow augmentation.
*   **Edge Criticality Bound**: An edge `(u, v)` is critical on an augmenting path if it is the bottleneck (`c_f(u, v) == pathFlow`). Between two augmentations where `(u, v)` is critical, `d(s, u)` must increase by at least 2. Because `d(s, u) <= V - 2`, each edge can become critical at most `V / 2` times. Across all `E` edges, total augmentations cannot exceed `O(V * E)`.
*   **Total Runtime Bound**: Each augmentation runs a BFS taking `O(E)` time. Multiplying `O(V * E)` augmentations by `O(E)` per BFS yields strictly bounded polynomial runtime `O(V * E^2)`.
*   **Residual Net-Balance Invariant**: Forward edge `residual[u, v]` plus backward edge `residual[v, u]` strictly equals total original capacity `c(u, v) + c(v, u)` at every state.

---

### 2. Explicit Complexity Deconstruction

| Operation | Time (Best / Avg / Worst) | Auxiliary Space | Output Space | Mathematical Derivation |
| :--- | :--- | :--- | :--- | :--- |
| **Edmonds-Karp Max Flow**| `O(E)` / `O(V * E^2)` / `O(V * E^2)` | `O(V^2 + E)` matrix + adj | `O(1)` | At most `V * E / 2` augmentations; each BFS takes `O(E)`. |
| **BFS Augmentation Step** | `O(1)` / `O(E)` / `O(E)` | `O(V)` queue + parent | `O(1)` | Explores reachable residual edges in breadth-first order. |
| **Min-Cut Extraction** | `O(V + E)` | `O(V)` visited set | `O(V + E)` cut partitions | Single BFS traversal from source on final residual network. |

---

### 3. Senior Interview Context: Hardware, Memory & Trade-Offs

*   **Why Ford-Fulkerson (DFS) Fails in Production**:
    Ford-Fulkerson using DFS takes arbitrary augmenting paths. On networks with edge capacities up to `C = 10^9`, DFS can augment path flows of size 1 alternately, taking `O(E * C)` iterations (billions of steps) and timing out. If edge capacities are irrational numbers, DFS may fail to terminate entirely and converge to an incorrect non-maximal flow value. Edmonds-Karp with BFS enforces shortest paths, guaranteeing polynomial `O(V * E^2)` execution independent of capacity sizes.
*   **Matrix vs Adjacency List Storage**:
    - **Adjacency Matrix (`int[V, V]`)**: `O(V^2)` memory. Ideal for dense networks (`E approx V^2`) and small graph sizes (`V <= 500`). Provides instant `O(1)` residual lookups and cache-line spatial locality.
    - **Adjacency List with Edge Structs**: `O(V + E)` memory. Essential for large sparse networks (`V >= 10^4`, `E <= 10^5`). Each edge points directly to its opposite residual edge struct via an index pointer `edge.reverseEdgeIndex`.

---

## 🎙️ Senior 45-Minute Verbal Talk Track

```text
+-------------------------------------------------------------------------------+
| PHASE 1: Constraints & Flow Modeling          (Minutes 00 - 05)               |
| - Identify Source s, Sink t, and intermediate node capacities.                |
| - Clarify constraints: V <= 500, non-negative integer capacities.             |
| - Verify whether output requires max flow value or minimum cut partitions.   |
+-------------------------------------------------------------------------------+
                                      │
                                      ▼
+-------------------------------------------------------------------------------+
| PHASE 2: Greedy Failure & Residual Graphs     (Minutes 05 - 10)               |
| - Show why a greedy path blocks global optimum (the diagonal choke point).    |
| - Introduce residual graph: backward edges allow future paths to undo choices.|
+-------------------------------------------------------------------------------+
                                      │
                                      ▼
+-------------------------------------------------------------------------------+
| PHASE 3: Mathematical Invariant Articulation   (Minutes 10 - 20)              |
| - Ford-Fulkerson DFS hazard: O(E * MaxFlow) exponential timeout.              |
| - Edmonds-Karp BFS guarantee: shortest augmenting path monotonically increases|
|   d(s, v). Each edge is critical <= V / 2 times => strictly O(V * E^2).       |
| - Prove Max-Flow Min-Cut duality: max flow = min capacity cut.                |
+-------------------------------------------------------------------------------+
                                      │
                                      ▼
+-------------------------------------------------------------------------------+
| PHASE 4: Clean Production Implementation       (Minutes 20 - 35)              |
| - Code EdmondsKarp with BFS parent tracking in idiomatic C# (.NET 8) / Python. |
| - Apply path bottleneck flow: residual[u, v] -= flow, residual[v, u] += flow. |
| - Extract Min-Cut partitions using post-flow residual BFS from source.        |
+-------------------------------------------------------------------------------+
                                      │
                                      ▼
+-------------------------------------------------------------------------------+
| PHASE 5: Verification & Architectural Scaling (Minutes 35 - 45)              |
| - Verify termination on zero-flow graphs, parallel edges, and disconnected T. |
| - Deconstruct Time O(V * E^2), Space O(V^2 + E).                              |
| - Introduce Dinic's Algorithm (O(V^2 * E)) for larger networks.              |
+-------------------------------------------------------------------------------+
```

### Verbal Script Excerpts

*   **Opening (Minutes 00-05)**: *"This problem can be modeled as finding the Maximum Flow in a directed capacity network. Before coding, I'll confirm that capacities are non-negative integers and verify the vertex count `V`. For `V <= 500`, the Edmonds-Karp algorithm provides a reliable, strictly polynomial `O(V * E^2)` solution with `O(V^2)` memory."*
*   **Invariant Articulation (Minutes 10-20)**: *"A naive greedy search fails because an early choice can consume edges needed by other paths. The residual network solves this by maintaining backward edges. When we push `f` flow from `u` to `v`, we add `f` capacity in reverse from `v` to `u`. Pushing flow along a backward edge later cancels out previous flow, redirecting it along a better route. By using BFS rather than DFS to find augmenting paths, the shortest-path distance from source to any node increases monotonically, guaranteeing that no edge can be critical more than `V / 2` times and bounding total augmentations to `O(V * E)`."*
*   **Min-Cut Proof (Minutes 35-45)**: *"Once the BFS fails to reach sink `t`, the residual graph has no augmenting paths. We run a BFS starting from source `s` to find all vertices reachable via non-zero residual edges. This reachable set forms partition `S`, and all remaining nodes form partition `T`. The original capacity of all edges directed from `S` to `T` exactly equals our computed max flow, satisfying the Max-Flow Min-Cut Theorem."*

---

## 📘 Chapter 5: Integration, Problems & Misconceptions

### 1. Progressive Practice Ladder
1.  **Edmonds-Karp Max Flow**: Baseline BFS implementation on directed graphs.
2.  **Minimum Cut Extraction**: Partitioning image pixels into foreground and background.
3.  **Network Delay / Bottleneck Link**: Finding the single critical pipe whose failure causes maximum flow reduction.

### 2. Common Misconceptions & Traps
*   *Incorrect Idea*: Adding backward edges with original capacity `c(v, u) = c(u, v)`.
    *   *Correction*: Backward edges initially have `0` capacity. Their capacity increases only when flow is pushed forward. Adding capacity to backward edges initially creates phantom flow loops.
*   *Incorrect Idea*: Assuming Min-Cut edges are simply edges where `residual[u, v] == 0`.
    *   *Correction*: Many edges have 0 residual capacity without being part of the minimum cut (e.g. saturated edges entirely inside set `S` or inside set `T`). The Min-Cut edges strictly cross the boundary from set `S` to set `T` (`u in S, v in T`).

---

> 🧭 **Navigation:** [← Previous Day](Week_15_Day_02_Segment_Trees_Range_Queries_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_15_Day_04_Network_Flow_Applications_Instructional.md)
