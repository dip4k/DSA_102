# 📘 Week 15, Day 4: Network Flow Applications — Dinic's Algorithm & Bipartite Matching

> 🧭 **Navigation:** [← Previous Day](Week_15_Day_03_Network_Flow_Basics_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_15_Day_05_Design_Patterns_Extensions_Instructional.md)
> 
> 💡 **Instructor Note:** *Not all sections or topics are mandatory. Dinic's algorithm is the industrial standard for maximum flow and bipartite matching. Focus on the duality: BFS builds the level graph DAG, DFS pushes blocking flow with dead-end pointer advance.*

---

## 🎯 Learning Objectives

*   **Master Dinic's Algorithm**: Understand the two-phase architecture: BFS level graph construction followed by DFS blocking flow with dead-end elimination (`ptr`/`work` pointers).
*   **Unit Network Acceleration**: Grasp why Dinic runs in `O(E * sqrt(V))` on unit networks and bipartite graphs, matching Hopcroft-Karp efficiency.
*   **Bipartite Matching Reduction**: Reduce the Maximum Bipartite Matching problem to a Network Flow problem with super-source `S` and super-sink `T`.
*   **Dual-Language Fluency**: Implement complete, runnable C# (.NET 8/9) and Python (3.11+) implementations for both Dinic Max Flow and Maximum Bipartite Matching with matched-pair extraction.

---

## 📖 Chapter 1: Context & Motivation

### 1. The Engineering Challenge: Resource Assignment at Scale

Consider assigning 10,000 workers to 10,000 tasks where each worker is qualified for a subset of tasks. Each worker can take at most 1 task, and each task requires at most 1 worker.
How do we maximize total completed tasks?

A greedy approach (assigning the first available task to each worker) quickly leads to suboptimal assignments.
While we saw that Edmonds-Karp solves general max flow, running Edmonds-Karp on `V = 20,000` nodes and `E = 100,000` edges takes `O(V * E^2) approx. 2 * 10^14` operations—completely impractical.

```text
       [ Left: Workers ]                [ Right: Tasks ]
         (Worker 1) ----------------------> (Task A)
                 \                      /
                  \                    /
         (Worker 2) ----------------------> (Task B)
```

**Dinic's Algorithm** solves this bottleneck by finding **multiple augmenting paths simultaneously** in a single phase via a **blocking flow**.
On bipartite unit networks, Dinic terminates in strictly `O(E * sqrt(V))` time—requiring fewer than `1.4 * 10^7` operations and executing in under 15 milliseconds.

---

## 🧠 Chapter 2: Mental Model & Structural Architecture

### 1. Dinic's Two-Phase Architecture

Dinic's algorithm partitions augmenting path discovery into two alternating phases:

```text
[ Current Residual Network ]
              │
              ▼
   Phase 1: BFS Level Graph
   - Compute shortest distance from source: level[v] = level[u] + 1
   - Discard all edges that do not point to the next level
   - If level[sink] == -1, TERMINATE (Maximum flow reached)
              │
              ▼
   Phase 2: DFS Blocking Flow
   - Push flow strictly along level-graph DAG edges: level[v] == level[u] + 1
   - Maintain current-edge pointer `ptr[u]` to avoid re-examining saturated edges
   - Repeat DFS until no more flow can reach sink (blocking flow achieved)
              │
              ▼
[ Loop back to Phase 1 ]
```

### 2. Maximum Bipartite Matching Reduction

To solve Maximum Bipartite Matching between disjoint sets `L` and `R`:
1. Create a **Super-Source `S`** and connect `S -> u` for each `u in L` with capacity `1`.
2. Direct all original bipartite edges `u -> v` for `u in L, v in R` with capacity `1`.
3. Create a **Super-Sink `T`** and connect `v -> T` for each `v in R` with capacity `1`.
4. Run Dinic's Algorithm from `S` to `T`.
5. The maximum flow value equals the **Maximum Matching Cardinality**.
6. Matched pairs correspond to bipartite edges `(u, v)` carrying `flow == 1`.

---

## 💻 Chapter 3: Production-Grade Dual-Language Implementations

### 1. Pattern 1: Dinic's Maximum Flow Engine

#### A. Modern C# Implementation (.NET 8/9)
```csharp
using System;
using System.Collections.Generic;

namespace AdvancedFlow;

public class DinicMaxFlow
{
    public class Edge
    {
        public int To { get; init; }
        public int Capacity { get; set; }
        public int Flow { get; set; }
        public int RevIndex { get; init; }

        public Edge(int to, int capacity, int revIndex)
        {
            To = to;
            Capacity = capacity;
            RevIndex = revIndex;
            Flow = 0;
        }
    }

    private readonly int _nodes;
    private readonly List<Edge>[] _adj;
    private readonly int[] _level;
    private readonly int[] _ptr;

    public DinicMaxFlow(int nodes)
    {
        if (nodes <= 0) throw new ArgumentOutOfRangeException(nameof(nodes));
        _nodes = nodes;
        _adj = new List<Edge>[nodes];
        for (int i = 0; i < nodes; i++) _adj[i] = new List<Edge>();
        _level = new int[nodes];
        _ptr = new int[nodes];
    }

    public void AddEdge(int from, int to, int capacity)
    {
        if (capacity < 0) throw new ArgumentException("Capacity must be non-negative.");
        var forward = new Edge(to, capacity, _adj[to].Count);
        var backward = new Edge(from, 0, _adj[from].Count);
        _adj[from].Add(forward);
        _adj[to].Add(backward);
    }

    /// <summary>
    /// Computes Maximum Flow from source to sink in O(V^2 * E) general, O(E * sqrt(V)) unit network.
    /// </summary>
    public int ComputeMaxFlow(int source, int sink)
    {
        if (source < 0 || source >= _nodes || sink < 0 || sink >= _nodes || source == sink)
            throw new ArgumentException("Invalid source or sink vertex.");

        int totalFlow = 0;

        // While a path exists from source to sink in residual network
        while (BfsBuildLevelGraph(source, sink))
        {
            Array.Fill(_ptr, 0); // Reset current-edge pointers for this phase

            while (true)
            {
                int pushed = DfsPushBlockingFlow(source, sink, int.MaxValue);
                if (pushed == 0) break;
                totalFlow += pushed;
            }
        }

        return totalFlow;
    }

    public List<(int From, int To)> GetMatchedEdges(HashSet<int> leftSet)
    {
        var matches = new List<(int From, int To)>();
        foreach (int u in leftSet)
        {
            foreach (var edge in _adj[u])
            {
                if (edge.Capacity > 0 && edge.Flow == 1)
                {
                    matches.Add((u, edge.To));
                }
            }
        }
        return matches;
    }

    private bool BfsBuildLevelGraph(int source, int sink)
    {
        Array.Fill(_level, -1);
        _level[source] = 0;

        var queue = new Queue<int>();
        queue.Enqueue(source);

        while (queue.Count > 0)
        {
            int u = queue.Dequeue();

            foreach (var edge in _adj[u])
            {
                if (edge.Capacity - edge.Flow > 0 && _level[edge.To] == -1)
                {
                    _level[edge.To] = _level[u] + 1;
                    queue.Enqueue(edge.To);
                }
            }
        }

        return _level[sink] != -1;
    }

    private int DfsPushBlockingFlow(int u, int sink, int flowIn)
    {
        if (u == sink || flowIn == 0) return flowIn;

        for (; _ptr[u] < _adj[u].Count; _ptr[u]++)
        {
            var edge = _adj[u][_ptr[u]];

            if (_level[edge.To] == _level[u] + 1 && edge.Capacity - edge.Flow > 0)
            {
                int bottleNeck = Math.Min(flowIn, edge.Capacity - edge.Flow);
                int pushed = DfsPushBlockingFlow(edge.To, sink, bottleNeck);

                if (pushed > 0)
                {
                    edge.Flow += pushed;
                    _adj[edge.To][edge.RevIndex].Flow -= pushed;
                    return pushed;
                }
            }
        }

        return 0; // No augmenting path through u in this level graph
    }
}
```

#### B. Idiomatic Python Implementation (Python 3.11+)
```python
from collections import deque
from typing import List, Tuple

class Edge:
    __slots__ = ('to', 'capacity', 'flow', 'rev_index')
    def __init__(self, to: int, capacity: int, rev_index: int):
        self.to = to
        self.capacity = capacity
        self.flow = 0
        self.rev_index = rev_index

class DinicMaxFlow:
    """
    Dinic's Algorithm for Maximum Network Flow.
    Time Complexity: O(V^2 * E) general, O(E * sqrt(V)) unit/bipartite networks.
    """
    def __init__(self, nodes: int):
        if nodes <= 0:
            raise ValueError("Node count must be positive.")
        self.n = nodes
        self.adj: List[List[Edge]] = [[] for _ in range(nodes)]
        self.level = [-1] * nodes
        self.ptr = [0] * nodes

    def add_edge(self, u: int, v: int, capacity: int) -> None:
        """Adds a directed edge with specified capacity."""
        if capacity < 0:
            raise ValueError("Capacity must be non-negative.")
        forward = Edge(v, capacity, len(self.adj[v]))
        backward = Edge(u, 0, len(self.adj[u]))
        self.adj[u].append(forward)
        self.adj[v].append(backward)

    def compute_max_flow(self, source: int, sink: int) -> int:
        """Computes Maximum Flow from source to sink."""
        if not (0 <= source < self.n and 0 <= sink < self.n) or source == sink:
            raise ValueError("Invalid source or sink vertex.")

        total_flow = 0

        while self._bfs(source, sink):
            self.ptr[:] = [0] * self.n  # Reset work pointers
            while True:
                pushed = self._dfs(source, sink, float('inf'))
                if pushed == 0:
                    break
                total_flow += pushed

        return total_flow

    def _bfs(self, source: int, sink: int) -> bool:
        self.level[:] = [-1] * self.n
        self.level[source] = 0
        q = deque([source])

        while q:
            u = q.popleft()
            for edge in self.adj[u]:
                if edge.capacity - edge.flow > 0 and self.level[edge.to] == -1:
                    self.level[edge.to] = self.level[u] + 1
                    q.append(edge.to)

        return self.level[sink] != -1

    def _dfs(self, u: int, sink: int, flow_in: float) -> int:
        if u == sink or flow_in == 0:
            return int(flow_in)

        for i in range(self.ptr[u], len(self.adj[u])):
            self.ptr[u] = i
            edge = self.adj[u][i]

            if self.level[edge.to] == self.level[u] + 1 and edge.capacity - edge.flow > 0:
                bottleneck = min(flow_in, edge.capacity - edge.flow)
                pushed = self._dfs(edge.to, sink, bottleneck)

                if pushed > 0:
                    edge.flow += pushed
                    self.adj[edge.to][edge.rev_index].flow -= pushed
                    return pushed

        return 0
```

---

### 2. Pattern 2: Maximum Bipartite Matching Solver

#### A. Modern C# Implementation (.NET 8/9)
```csharp
using System.Collections.Generic;

namespace AdvancedFlow;

public class BipartiteMatcher
{
    private readonly int _leftCount;
    private readonly int _rightCount;
    private readonly List<(int Left, int Right)> _edges = new();

    public BipartiteMatcher(int leftCount, int rightCount)
    {
        _leftCount = leftCount;
        _rightCount = rightCount;
    }

    public void AddEdge(int leftNode, int rightNode)
    {
        _edges.Add((leftNode, rightNode));
    }

    /// <summary>
    /// Computes Maximum Bipartite Matching in O(E * sqrt(V)) time.
    /// </summary>
    public (int MaxMatching, List<(int Left, int Right)> Matches) Solve()
    {
        int source = 0;
        int sink = _leftCount + _rightCount + 1;
        int totalNodes = sink + 1;

        var dinic = new DinicMaxFlow(totalNodes);
        var leftSet = new HashSet<int>();

        // 1. Connect Source to Left nodes
        for (int l = 1; l <= _leftCount; l++)
        {
            dinic.AddEdge(source, l, 1);
            leftSet.Add(l);
        }

        // 2. Connect Left nodes to Right nodes
        foreach (var (l, r) in _edges)
        {
            int u = l + 1; // 1-based left node
            int v = _leftCount + r + 1; // offset right node
            dinic.AddEdge(u, v, 1);
        }

        // 3. Connect Right nodes to Sink
        for (int r = 1; r <= _rightCount; r++)
        {
            int v = _leftCount + r;
            dinic.AddEdge(v, sink, 1);
        }

        int maxMatching = dinic.ComputeMaxFlow(source, sink);

        var rawMatches = dinic.GetMatchedEdges(leftSet);
        var formattedMatches = new List<(int Left, int Right)>();

        foreach (var (u, v) in rawMatches)
        {
            if (u != source && v != sink)
            {
                formattedMatches.Add((u - 1, v - _leftCount - 1));
            }
        }

        return (maxMatching, formattedMatches);
    }
}
```

#### B. Idiomatic Python Implementation (Python 3.11+)
```python
class BipartiteMatchingSolver:
    """
    Solves Maximum Bipartite Matching via Dinic's Algorithm in O(E * sqrt(V)) time.
    """
    def __init__(self, left_count: int, right_count: int):
        self.left_count = left_count
        self.right_count = right_count
        self.edges: List[Tuple[int, int]] = []

    def add_edge(self, u: int, v: int) -> None:
        """Adds a candidate matching edge between left node u and right node v."""
        self.edges.append((u, v))

    def solve(self) -> Tuple[int, List[Tuple[int, int]]]:
        source = 0
        sink = self.left_count + self.right_count + 1
        total_nodes = sink + 1

        dinic = DinicMaxFlow(total_nodes)

        # Source -> Left
        for l in range(1, self.left_count + 1):
            dinic.add_edge(source, l, 1)

        # Left -> Right
        for u, v in self.edges:
            dinic.add_edge(u + 1, self.left_count + v + 1, 1)

        # Right -> Sink
        for r in range(1, self.right_count + 1):
            dinic.add_edge(self.left_count + r, sink, 1)

        max_matching = dinic.compute_max_flow(source, sink)

        # Extract matched pairs
        matches = []
        for l in range(1, self.left_count + 1):
            for edge in dinic.adj[l]:
                if edge.capacity > 0 and edge.flow == 1 and edge.to != source:
                    right_idx = edge.to - self.left_count - 1
                    matches.append((l - 1, right_idx))

        return max_matching, matches
```

---

## 📘 Chapter 4: Performance, Invariants & Systems Architecture

### 1. Exact Governing Invariants

*   **Level Graph DAG Invariant**: The BFS constructs a Directed Acyclic Graph (DAG) where directed edge `(u, v)` exists if and only if `residual_capacity(u, v) > 0` and `level[v] == level[u] + 1`. All back-edges, cross-edges to equal/lower levels, and zero-residual edges are purged.
*   **Current-Edge Pointer Invariant (`ptr`/`work`)**: During DFS blocking flow, `ptr[u]` tracks the index of the first potentially unblocked edge out of `u`. If an edge is saturated or cannot reach the sink, `ptr[u]` increments monotonically. No edge is traversed more than once per phase unless it successfully pushes flow, bounding DFS traversal time to `O(V * E)` per phase.
*   **Strict Level Distance Growth**: In each phase of Dinic's algorithm, the shortest distance from source to sink `level[sink]` increases strictly monotonically (`level_{k+1}[sink] > level_k[sink]`). Because the maximum possible distance is `V - 1`, the algorithm terminates in at most `V - 1` phases.
*   **Unit Network Invariant (Bipartite Matching)**: On networks where all edge capacities are 1 and every intermediate vertex has either `indegree <= 1` or `outdegree <= 1`:
    - After `sqrt(V)` phases, the shortest path distance is at least `sqrt(V)`.
    - The remaining flow decomposes into paths of length `>= sqrt(V)`.
    - Since vertices are disjoint, there can be at most `V / sqrt(V) = sqrt(V)` remaining paths.
    - Thus, the algorithm requires at most `2 * sqrt(V)` phases total, each taking `O(E)`, proving strictly `O(E * sqrt(V))` total time.

---

### 2. Explicit Complexity Deconstruction

| Algorithm / Workload | Time (Best / Avg / Worst) | Auxiliary Space | Output Space | Mathematical Derivation |
| :--- | :--- | :--- | :--- | :--- |
| **Dinic (General Graph)** | `O(E)` / `O(V^2 * E)` / `O(V^2 * E)` | `O(V + E)` adj list + levels | `O(1)` | At most `V - 1` phases; each phase takes `O(V * E)` DFS blocking flow. |
| **Dinic (Unit Network)** | `O(E * sqrt(V))` | `O(V + E)` | `O(1)` | Number of phases bounded by `2 * sqrt(V)`. |
| **Bipartite Matching** | `O(E * sqrt(V))` | `O(V + E)` | `O(min(L, R))` matches | Reduction introduces `V + 2` nodes; each vertex has unit capacity. |
| **Hopcroft-Karp Equivalent**| `O(E * sqrt(V))` | `O(V + E)` | `O(min(L, R))` | Structurally equivalent to Dinic on bipartite unit flow networks. |

---

### 3. Senior Interview Context: Hardware, Memory & Trade-Offs

```text
+-----------------------+---------------------+---------------------+-----------------------+
| Flow Algorithm        | General Network     | Bipartite Matching  | Memory Allocation     |
+-----------------------+---------------------+---------------------+-----------------------+
| Ford-Fulkerson (DFS)  | O(E * MaxFlow)      | O(V * E)            | O(V + E)              |
| Edmonds-Karp (BFS)    | O(V * E^2)          | O(V * E^2)          | O(V^2) or O(V + E)    |
| Dinic's Algorithm     | O(V^2 * E)          | O(E * sqrt(V))      | O(V + E)              |
| Push-Relabel (FIFO)   | O(V^3)              | O(V^3)              | O(V^2)                |
+-----------------------+---------------------+---------------------+-----------------------+
```

*   **Why Dinic Beats Edmonds-Karp in Practice**:
    Edmonds-Karp executes a full BFS from scratch for *every single augmenting path*, pushing only a trickle of flow each time. Dinic executes BFS *once per phase*, builds a level DAG, and then saturates multiple paths in a single DFS sweep. On typical graphs, Dinic finishes in 5-10 phases, running 100x faster than Edmonds-Karp.
*   **Current-Edge Optimization (`ptr` array)**:
    Without the `ptr` array, the DFS could repeatedly re-explore dead-end paths that have already failed to reach the sink, degrading DFS time from `O(V * E)` to `O(V^2 * E)` per phase. The `ptr` array is what guarantees polynomial bounds.

---

## 🎙️ Senior 45-Minute Verbal Talk Track

```text
+-------------------------------------------------------------------------------+
| PHASE 1: Problem Modeling & Reduction          (Minutes 00 - 05)              |
| - Recognize bipartite constraints: two disjoint sets, non-overlapping pairs.  |
| - Formulate reduction: construct Super-Source S and Super-Sink T with cap = 1.|
| - State target complexity: O(E * sqrt(V)) via Dinic / Hopcroft-Karp.          |
+-------------------------------------------------------------------------------+
                                      │
                                      ▼
+-------------------------------------------------------------------------------+
| PHASE 2: Baseline vs Dinic Architecture        (Minutes 05 - 10)              |
| - Explain why Edmonds-Karp (O(V * E^2)) is too slow for 10^4 vertices.        |
| - Introduce Dinic: BFS level graph + DFS blocking flow with dead-end pruning. |
+-------------------------------------------------------------------------------+
                                      │
                                      ▼
+-------------------------------------------------------------------------------+
| PHASE 3: Mathematical Invariant Articulation   (Minutes 10 - 20)              |
| - Prove level[sink] strictly increases each phase => <= V phases.             |
| - Prove ptr[u] pointer advance guarantees each edge traversed <= 1 time in DFS|
| - Prove unit network phase bound: <= 2 * sqrt(V) phases.                      |
+-------------------------------------------------------------------------------+
                                      │
                                      ▼
+-------------------------------------------------------------------------------+
| PHASE 4: Clean Production Implementation       (Minutes 20 - 35)              |
| - Code DinicMaxFlow with reverse edge index pointers and ptr array.           |
| - Implement BipartiteMatcher wrapper with 1-based index offsets.              |
| - Extract matched pairs cleanly: if capacity > 0 and flow == 1.               |
+-------------------------------------------------------------------------------+
                                      │
                                      ▼
+-------------------------------------------------------------------------------+
| PHASE 5: Verification & System Scaling         (Minutes 35 - 45)              |
| - Verify edge cases: disconnected components, unbalanced bipartition |L| != |R||
| - Deconstruct Time O(E * sqrt(V)) and Auxiliary Space O(V + E).               |
| - Discuss real-world variants: Min-Cost Max-Flow for weighted matching.       |
+-------------------------------------------------------------------------------+
```

### Verbal Script Excerpts

*   **Opening (Minutes 00-05)**: *"This matching problem can be modeled as Maximum Bipartite Matching, which reduces to Maximum Flow. By creating a super-source connecting to all workers with capacity 1 and a super-sink receiving flow from all tasks with capacity 1, the maximum flow value gives the maximum matching. Because all capacities are 1 and nodes have unit degree, this forms a unit network where Dinic's Algorithm runs in `O(E * sqrt(V))` time."*
*   **Invariant Articulation (Minutes 10-20)**: *"Dinic alternates between BFS and DFS. BFS constructs a level graph DAG where edges only advance by `+1` depth, strictly eliminating cycles. Next, DFS pushes blocking flow exclusively along level-graph edges. The critical optimization is the `ptr` array: when an outgoing edge from node `u` is exhausted or cannot reach the sink, `ptr[u]` increments, guaranteeing we never re-evaluate that dead-end again in this phase. This bounds DFS time to `O(V * E)` per phase."*
*   **Unit Network Proof (Minutes 35-45)**: *"Why does Dinic run in `O(E * sqrt(V))` on bipartite networks? After `sqrt(V)` phases, the distance from source to sink is at least `sqrt(V)`. In a unit network where each vertex can carry at most 1 unit of flow, any remaining augmenting path must contain at least `sqrt(V)` vertices. Since all paths are vertex-disjoint, there can be at most `V / sqrt(V) = sqrt(V)` remaining paths. Thus, the algorithm can run at most `sqrt(V)` additional phases, bounding total phases to `2 * sqrt(V)` and total time to `O(E * sqrt(V))`."*

---

## 📘 Chapter 5: Integration, Problems & Misconceptions

### 1. Progressive Practice Ladder
1.  **Maximum Bipartite Matching**: Worker-task assignment via Dinic reduction.
2.  **Maximum Number of Accepted K-Invitations** ([LeetCode 1820](https://leetcode.com/problems/maximum-number-of-accepted-invitations/)): Classic bipartite matching problem.
3.  **Distinct Echo Substrings / Path Routing**: Modeling vertex-disjoint path finding using node-splitting (splitting each node `v` into `v_in` and `v_out` with capacity 1).

### 2. Common Misconceptions & Traps
*   *Incorrect Idea*: Forgetting to reset the `ptr` array between phases.
    *   *Correction*: The `ptr` array tracks dead ends *only for the current level graph*. When BFS discovers new shortest paths and increments `level`, `ptr` must be reset to 0 across all vertices.
*   *Incorrect Idea*: Re-traversing reverse residual edges in BFS.
    *   *Correction*: In BFS, an edge `(u, v)` is only valid if its residual capacity `edge.Capacity - edge.Flow > 0`. Attempting to traverse saturated edges corrupts the level assignments.

---

> 🧭 **Navigation:** [← Previous Day](Week_15_Day_03_Network_Flow_Basics_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_15_Day_05_Design_Patterns_Extensions_Instructional.md)
