# 📘 Week 09 Day 03: All-Pairs Shortest Paths: Floyd-Warshall — ENGINEERING GUIDE

> 🧭 **Navigation:** [← Previous Day](Week_09_Day_02_Bellman_Ford_Negative_Weights_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_09_Day_04_Minimum_Spanning_Trees_Kruskal_Prim_Instructional.md)
> 
> 💡 **Instructor Note:** *Not all sections or topics are mandatory. Adapt your pace and focus on core mechanics, dual-language implementations, and the 45-minute verbal script based on your interview timeline.*

---

## 🎯 LEARNING OBJECTIVES

*By the end of this chapter, you will be able to:*
- 🎯 **Internalize** the All-Pairs Shortest Path (APSP) problem and why Floyd-Warshall solves it via dynamic programming over an expanding prefix of allowable intermediate vertices.
- ⚙️ **Implement** production-grade Floyd-Warshall in C# (.NET 8/9) and Python (3.11+) featuring negative cycle detection and a 2D routing matrix for full path reconstruction.
- ⚖️ **Evaluate** trade-offs between running Dijkstra `V` times (`O(V * (V + E) log V)`), Bellman-Ford `V` times (`O(V^2 * E)`), and Floyd-Warshall (`O(V^3)`).
- 🏭 **Connect** APSP matrix formulations to real production systems: network diameter analysis, transitive closure in compilers, and dense logistics hub routing.
- 🎙️ **Articulate** why the `k`-loop must be strictly outermost during a 45-minute live technical interview using a structured verbal script.

---

## 📖 CHAPTER 1: CONTEXT & MOTIVATION

### The Problem: Every Source to Every Destination

Days 1 and 2 solved the Single-Source Shortest Path (SSSP) problem: finding minimum costs from *one* designated source to all other vertices. Many engineering systems require the complete distance matrix between *all* pairs of vertices:
- Global air transit: Finding cheapest flights between any pair of international airports.
- Network infrastructure: Calculating network diameter, betweenness centrality, and fault-tolerance rerouting.
- Static analysis & compilers: Computing transitive closure ("can variable `X` affect variable `Y`?").

If you run Dijkstra from all `V` vertices on a dense graph (`E -> V^2`), the runtime is:
`V * O((V + E) log V) = O(V^3 log V)`

Floyd-Warshall (1962) computes all-pairs shortest paths in exactly `O(V^3)` time using dynamic programming, handling positive and negative edge weights alike while remaining dramatically simpler to implement.

---

## 🧠 CHAPTER 2: BUILDING THE MENTAL MODEL

### The Core Analogy: Gradually Unlocking Intermediate Waypoints

Instead of searching paths by length or hop count, Floyd-Warshall builds paths by gradually unlocking intermediate vertices:

```text
Layer k = 0: Paths can use NO intermediate vertices (direct edges only).
Layer k = 1: Paths may use vertex 0 as an intermediate stop.
Layer k = 2: Paths may use vertices {0, 1} as intermediate stops.
...
Layer k = V: Paths may use any vertex in {0, 1, ..., V - 1} as an intermediate stop.
```

At step `k`, we evaluate whether detouring through vertex `k` offers a shorter path between every pair `(i, j)`:

```text
Direct path using intermediates {0 .. k-1}:
    i -----------------------------------------------------> j   (dist[i][j])
                            vs.
Detour through newly unlocked intermediate k:
    i -----------> [ k ] (unlocked stop) -----------> j   (dist[i][k] + dist[k][j])

State Recurrence:
    dist[i][j] = min(dist[i][j], dist[i][k] + dist[k][j])
```

```text
CRITICAL INVARIANT: The k-loop MUST be the outermost loop!
k represents the dynamic programming stage. Iterating i or j outside k corrupts
the recurrence by reading unfinalized DP states.
```

### 🖼 Visualizing the Structure

Consider a 4-vertex directed graph (`0`, `1`, `2`, `3`) with 6 weighted edges:

```text
               (4)
      [ 0 ] ---------> [ 1 ]
       |  \              |
   (2) |   \             | (1)
       v    \ (2)        v
      [ 3 ]  +-------> [ 2 ]
       ^                 |
       |                 | (5)
       +-----------------+
        (Edges 3->0: 7, 3->1: 6)
```

#### Graph Specification
- Vertices: `0, 1, 2, 3` (`V = 4`)
- Directed Edges:
  - `0 -> 1` (weight 4)
  - `0 -> 3` (weight 2)
  - `1 -> 2` (weight 1)
  - `2 -> 3` (weight 5)
  - `3 -> 0` (weight 7)
  - `3 -> 1` (weight 6)

#### Step-by-Step Matrix Evolution Trace

##### Initial State (Direct Edges Only, `k = 0` Setup)
```text
      0    1    2    3
 0 [  0,   4, inf,   2 ]
 1 [ inf,  0,   1, inf ]
 2 [ inf, inf,  0,   5 ]
 3 [  7,   6, inf,   0 ]
```

##### After `k = 0` (Intermediate Candidate: `{0}`)
Checking if path `i -> 0 -> j` improves `dist[i][j]`:
- Path `3 -> 0 -> 1`: `dist[3][0] + dist[0][1] = 7 + 4 = 11` (Current `dist[3][1] = 6`, no change).
- Path `3 -> 0 -> 3`: `dist[3][0] + dist[0][3] = 7 + 2 = 9` (Current `dist[3][3] = 0`, no change).
```text
      0    1    2    3
 0 [  0,   4, inf,   2 ]
 1 [ inf,  0,   1, inf ]
 2 [ inf, inf,  0,   5 ]
 3 [  7,   6, inf,   0 ]  (No entries changed)
```

##### After `k = 1` (Intermediate Candidates: `{0, 1}`)
Checking if path `i -> 1 -> j` improves `dist[i][j]`:
- `0 -> 1 -> 2`: `dist[0][1] + dist[1][2] = 4 + 1 = 5 < inf` -> **Update `dist[0][2] = 5`!**
- `3 -> 1 -> 2`: `dist[3][1] + dist[1][2] = 6 + 1 = 7 < inf` -> **Update `dist[3][2] = 7`!**
```text
      0    1    2    3
 0 [  0,   4,   5,   2 ]   <-- (0, 2) improved to 5 via 0->1->2
 1 [ inf,  0,   1, inf ]
 2 [ inf, inf,  0,   5 ]
 3 [  7,   6,   7,   0 ]   <-- (3, 2) improved to 7 via 3->1->2
```

##### After `k = 2` (Intermediate Candidates: `{0, 1, 2}`)
Checking if path `i -> 2 -> j` improves `dist[i][j]`:
- `1 -> 2 -> 3`: `dist[1][2] + dist[2][3] = 1 + 5 = 6 < inf` -> **Update `dist[1][3] = 6`!**
- `0 -> 2 -> 3`: `dist[0][2] + dist[2][3] = 5 + 5 = 10` (Current `dist[0][3] = 2`, no change).
```text
      0    1    2    3
 0 [  0,   4,   5,   2 ]
 1 [ inf,  0,   1,   6 ]   <-- (1, 3) improved to 6 via 1->2->3
 2 [ inf, inf,  0,   5 ]
 3 [  7,   6,   7,   0 ]
```

##### After `k = 3` (Intermediate Candidates: `{0, 1, 2, 3}`)
Checking if path `i -> 3 -> j` improves `dist[i][j]`:
- `1 -> 3 -> 0`: `dist[1][3] + dist[3][0] = 6 + 7 = 13 < inf` -> **Update `dist[1][0] = 13`!**
- `2 -> 3 -> 0`: `dist[2][3] + dist[3][0] = 5 + 7 = 12 < inf` -> **Update `dist[2][0] = 12`!**
- `2 -> 3 -> 1`: `dist[2][3] + dist[3][1] = 5 + 6 = 11 < inf` -> **Update `dist[2][1] = 11`!**
```text
      0    1    2    3
 0 [  0,   4,   5,   2 ]
 1 [ 13,   0,   1,   6 ]   <-- (1, 0) finalized to 13
 2 [ 12,  11,   0,   5 ]   <-- (2, 0) finalized to 12, (2, 1) finalized to 11
 3 [  7,   6,   7,   0 ]
```

##### Negative Cycle Detection Invariant
If a negative cycle reachable from vertex `i` exists, the algorithm will eventually update `dist[i][i] < 0`. Checking the matrix diagonal in `O(V)` after the triple loop reveals all negative cycles!

---

## ⚙️ CHAPTER 3: MECHANICS & IMPLEMENTATION

### Dual-Language Production Implementations

#### C# (.NET 8/9) Production Implementation

```csharp
using System;
using System.Collections.Generic;

public static class FloydWarshallEngine
{
    /// <summary>
    /// Computes all-pairs shortest paths using Floyd-Warshall algorithm.
    /// Time Complexity: O(V^3) | Auxiliary Space: O(V^2)
    /// </summary>
    /// <param name="vertices">Number of vertices.</param>
    /// <param name="graph">Initial adjacency matrix where graph[i, j] = weight, or int.MaxValue if no edge.</param>
    /// <returns>(distances, nextHop, hasNegativeCycle)</returns>
    public static (int[,] distances, int[,] nextHop, bool hasNegativeCycle) RunFloydWarshall(
        int vertices, 
        int[,] graph)
    {
        if (graph == null || vertices <= 0)
        {
            throw new ArgumentException("Invalid graph matrix.");
        }

        int[,] dist = new int[vertices, vertices];
        int[,] next = new int[vertices, vertices];

        // STEP 1: Initialize Distance and Next Hop matrices
        for (int i = 0; i < vertices; i++)
        {
            for (int j = 0; j < vertices; j++)
            {
                dist[i, j] = graph[i, j];
                if (i == j)
                {
                    dist[i, j] = 0;
                    next[i, j] = j;
                }
                else if (graph[i, j] != int.MaxValue)
                {
                    next[i, j] = j;
                }
                else
                {
                    next[i, j] = -1;
                }
            }
        }

        // STEP 2: Main Dynamic Programming Loops
        // CRITICAL: k must be the outermost loop
        for (int k = 0; k < vertices; k++)
        {
            for (int i = 0; i < vertices; i++)
            {
                for (int j = 0; j < vertices; j++)
                {
                    // Guard against integer overflow when adding int.MaxValue
                    if (dist[i, k] != int.MaxValue && dist[k, j] != int.MaxValue)
                    {
                        int detourDist = dist[i, k] + dist[k, j];
                        if (detourDist < dist[i, j])
                        {
                            dist[i, j] = detourDist;
                            next[i, j] = next[i, k];
                        }
                    }
                }
            }
        }

        // STEP 3: Detect Negative Cycles (Diagonal Check)
        bool hasNegativeCycle = false;
        for (int i = 0; i < vertices; i++)
        {
            if (dist[i, i] < 0)
            {
                hasNegativeCycle = true;
                break;
            }
        }

        return (dist, next, hasNegativeCycle);
    }

    /// <summary>
    /// Reconstructs the shortest path between u and v using the next hop routing matrix.
    /// </summary>
    public static List<int> ReconstructPath(int u, int v, int[,] next)
    {
        if (next[u, v] == -1) return new List<int>();

        var path = new List<int> { u };
        int curr = u;
        while (curr != v)
        {
            curr = next[curr, v];
            path.Add(curr);
        }
        return path;
    }
}
```

#### Python (3.11+) Production Implementation

```python
from typing import List, Tuple

def floyd_warshall(
    vertices: int, 
    graph: List[List[float]]
) -> Tuple[List[List[float]], List[List[int]], bool]:
    """
    Computes all-pairs shortest paths using the Floyd-Warshall algorithm.
    
    Time Complexity: O(V^3)
    Auxiliary Space: O(V^2)
    
    :param vertices: Total number of vertices.
    :param graph: 2D adjacency matrix with weights or float('inf').
    :return: (dist_matrix, next_hop_matrix, has_negative_cycle)
    """
    if vertices <= 0 or not graph:
        raise ValueError("Invalid graph matrix.")

    # Initialize distance matrix copy and next hop routing matrix
    dist: List[List[float]] = [row[:] for row in graph]
    next_hop: List[List[int]] = [[-1] * vertices for _ in range(vertices)]

    for i in range(vertices):
        for j in range(vertices):
            if i == j:
                dist[i][j] = 0.0
                next_hop[i][j] = j
            elif dist[i][j] != float('inf'):
                next_hop[i][j] = j

    # Triple nested DP: k must be outermost
    for k in range(vertices):
        for i in range(vertices):
            for j in range(vertices):
                if dist[i][k] != float('inf') and dist[k][j] != float('inf'):
                    new_dist = dist[i][k] + dist[k][j]
                    if new_dist < dist[i][j]:
                        dist[i][j] = new_dist
                        next_hop[i][j] = next_hop[i][k]

    # Negative cycle detection via diagonal inspection
    has_negative_cycle = any(dist[i][i] < 0.0 for i in range(vertices))

    return dist, next_hop, has_negative_cycle

def reconstruct_path(u: int, v: int, next_hop: List[List[int]]) -> List[int]:
    """Reconstructs the full path from u to v."""
    if next_hop[u][v] == -1:
        return []
    path = [u]
    curr = u
    while curr != v:
        curr = next_hop[curr][v]
        path.append(curr)
    return path
```

---

## ⚖️ CHAPTER 4: PERFORMANCE, TRADE-OFFS & REAL SYSTEMS

### Complexity Deconstruction

| Metric | Complexity | Exact Mathematical Rationale |
| :--- | :--- | :--- |
| **Time (All Cases)** | `O(V^3)` | Three strictly nested loops iterating `V` times each. The inner comparison and assignment are `O(1)`. No heap or list allocation in the hot loop. |
| **Auxiliary Space** | `O(V^2)` | Two contiguous 2D matrices: `dist[V, V]` for shortest distances and `next[V, V]` for path reconstruction pointers. |
| **Output Space** | `O(V^2)` | Full distance matrix `V * V`. Individual path reconstruction takes `O(L)` where `L <= V`. |

### APSP Architecture Comparison

| Approach | Time Complexity | Auxiliary Space | Handles Negatives? | Best Application |
| :--- | :--- | :--- | :--- | :--- |
| **Floyd-Warshall** | `O(V^3)` | `O(V^2)` | ✅ Yes | Small dense graphs (`V <= 500`), all pairs |
| **Dijkstra x V** | `O(V * (V + E) log V)` | `O(V + E)` | ❌ No | Sparse graphs (`E << V^2`), positive weights only |
| **Bellman-Ford x V** | `O(V^2 * E)` | `O(V)` | ✅ Yes | Sparse graphs with negative weights |
| **Johnson's Algorithm** | `O(V * E + V * (V + E) log V)`| `O(V^2)` | ✅ Yes | Large sparse graphs with negative weights |

### Production Systems: Concise Interview Context Callouts

> [!NOTE]
> **System Design Context: Network Diameter & Critical Bottleneck Analysis**  
> In distributed storage clusters and datacenter spine topologies, the **network diameter** is defined as the maximum shortest path between any two server nodes (`max(dist[i][j])`). Running Floyd-Warshall offline across cluster topologies identifies high-latency routing bottlenecks and measures worst-case tail latencies during packet broadcasts.

> [!NOTE]
> **Compiler Context: Transitive Closure & Static Pointer Analysis**  
> Optimizing compilers analyze program call graphs using the Warshall boolean variant (`dist[i][j] = dist[i][j] || (dist[i][k] && dist[k][j])`). By precomputing whether function `A` can transitively invoke function `B`, dead-code elimination and inline caching algorithms determine which code blocks are strictly unreachable.

> [!NOTE]
> **Logistics Context: Regional Hub-and-Spoke Distance Matrix Caching**  
> Delivery logistics providers (FedEx, UPS) operate hundreds of regional sorting centers (`V < 500`). Running Floyd-Warshall once per planning period generates a precomputed lookup table enabling `O(1)` real-time parcel dispatch queries while naturally absorbing negative-cost backhaul incentives.

---

## 🎙️ 45-MINUTE INTERVIEW VERBAL SCRIPT

### Phase 1: Clarification & Constraints (0 - 5 min)
- **Candidate:** *"Are we looking for shortest paths between a single pair, from a single source, or between all possible pairs of vertices? And can edge weights be negative?"*
- **Interviewer:** *"We need all-pairs shortest paths. Edge weights can be negative, but there should not be negative cycles."*
- **Candidate:** *"What are the bounds on the number of vertices `V`?"*
- **Interviewer:** *"V is small, at most 400."*
- **Candidate:** *"With `V <= 400`, `V^3 = 6.4 * 10^7` operations. Floyd-Warshall is the ideal algorithm here: it operates in `O(V^3)` time, handles negative weights cleanly, avoids complex heap allocations, and requires under 10 lines of core logic."*

### Phase 2: High-Level Approach & Intuition (5 - 12 min)
- **Candidate:** *"The dynamic programming state represents: what is the shortest path between `i` and `j` using only intermediate vertices from the subset `{0, 1, ..., k}`?
At each stage `k`, we ask: does routing through vertex `k` yield a cheaper path than our previous best estimate?
`dist[i][j] = min(dist[i][j], dist[i][k] + dist[k][j])`.
The critical architectural requirement is that `k` MUST be the outermost loop. If `k` were nested inside `i` or `j`, we would read partially updated states and violate the subproblem ordering."*

### Phase 3: Coding Walkthrough (12 - 30 min)
- **Candidate:** *"I will now code the implementation. Notice how I manage two key production considerations:
1. **Overflow Safety:** In C# or Python, if `dist[i][k]` is `infinity` (or `int.MaxValue`), adding any weight causes overflow. We guard with `dist[i][k] != int.MaxValue && dist[k][j] != int.MaxValue`.
2. **Path Reconstruction:** We maintain a `next[i, j]` matrix. When detour `k` improves path `(i, j)`, we update `next[i, j] = next[i, k]`, preserving the first step of the optimal trajectory."*

### Phase 4: Dry Run & Edge Cases (30 - 38 min)
- **Candidate:** *"Let's dry-run edge cases:
- **Self-loops:** Initialized to `dist[i][i] = 0`. If any `dist[i][i]` becomes negative after the algorithm completes, a negative cycle exists.
- **Disconnected pairs:** Remain at `int.MaxValue` / `inf`.
- **Negative edges without negative cycles:** Correctly incorporated as intermediate nodes unlock."*

### Phase 5: Complexity & Trade-Offs (38 - 45 min)
- **Candidate:** *"Complexity summary:
- **Time:** `O(V^3)` strictly.
- **Auxiliary Space:** `O(V^2)` for the distance and next-hop matrices.
- **Trade-Offs:** If the graph were sparse (`E = O(V)`) and had only non-negative weights, running Dijkstra `V` times would yield `O(V^2 log V)`, which is faster. However, for dense graphs or graphs with negative weights, Floyd-Warshall's simplicity, cache-friendly array access, and `O(V^3)` bound make it unbeatable."*

---

## ⚔️ SUPPLEMENTARY OUTCOMES

### Practice Problem Ladder

| Problem | Source | Difficulty | Key Concept | Target Time |
| :--- | :--- | :--- | :--- | :--- |
| Find the City With the Smallest Number of Neighbors | LeetCode #1334 | 🟡 Medium | Canonical Floyd-Warshall distance threshold | 20 mins |
| Course Schedule IV | LeetCode #1462 | 🟡 Medium | Transitive closure via Floyd-Warshall | 15 mins |
| Evaluate Division | LeetCode #399 | 🟡 Medium | Multiplicative all-pairs paths | 25 mins |
| Network Delay Time (All-Pairs Context) | LeetCode #743 | 🟡 Medium | SSSP vs APSP trade-off comparison | 15 mins |

---

## 📊 COMPLEXITY ANALYSIS REFERENCE TABLE

| Variant | Time Complexity | Auxiliary Space | Output Space | Negative Weights? |
| :--- | :--- | :--- | :--- | :--- |
| **Floyd-Warshall** | `O(V^3)` | `O(V^2)` | `O(V^2)` | ✅ Yes |
| **Dijkstra x V** | `O(V * (V + E) log V)` | `O(V + E)` | `O(V^2)` | ❌ No |
| **Bellman-Ford x V** | `O(V^2 * E)` | `O(V)` | `O(V^2)` | ✅ Yes |
| **Transitive Closure (Bitset)** | `O(V^3 / 64)` | `O(V^2 / 64)` | `O(V^2)` | N/A (Boolean) |

---

> 🧭 **Navigation:** [← Previous Day](Week_09_Day_02_Bellman_Ford_Negative_Weights_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_09_Day_04_Minimum_Spanning_Trees_Kruskal_Prim_Instructional.md)
