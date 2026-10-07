# 📘 Week 09 Day 01: Single-Source Shortest Paths: Dijkstra — ENGINEERING GUIDE

> 🧭 **Navigation:** [← Week Overview](README.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_09_Day_02_Bellman_Ford_Negative_Weights_Instructional.md)
> 
> 💡 **Instructor Note:** *Not all sections or topics are mandatory. Adapt your pace and focus on core mechanics, dual-language implementations, and the 45-minute verbal script based on your interview timeline.*

---

## 🎯 LEARNING OBJECTIVES

*By the end of this chapter, you will be able to:*
- 🎯 **Internalize** the single-source shortest-path problem and Dijkstra's greedy strategy: always expand the globally closest frontier vertex, guaranteed optimal due to non-negative edge weights.
- ⚙️ **Implement** production-grade Dijkstra in modern C# (.NET 8/9 with `PriorityQueue<int, int>`) and Python (3.11+ with `heapq`), managing stale queue entries and path reconstruction.
- ⚖️ **Evaluate** exact performance trade-offs: binary heap `O((V + E) log V)` vs. dense array scan `O(V^2)` vs. unweighted BFS `O(V + E)`.
- 🏭 **Connect** shortest-path graph models to production systems: turn-by-turn routing, OSPF internet routing, and transaction latency optimization.
- 🎙️ **Articulate** full architectural trade-offs during a 45-minute live technical interview using a structured verbal script.

---

## 📖 CHAPTER 1: CONTEXT & MOTIVATION

### The Problem: Navigating Networks with Heterogeneous Costs

In unweighted graphs, Breadth-First Search (BFS) computes the shortest path because every edge has unit cost: minimum hops equals minimum cost. Real-world systems, however, have heterogeneous costs:
- Physical transit: Highway segments take 45 minutes; residential streets take 15 minutes.
- Computer networking: Fiber links carry 2 ms latency; satellite links carry 600 ms latency.
- Financial routing: Payment rails incur differing basis-point fees and settlement delays.

The **Single-Source Shortest Path (SSSP)** problem asks: given a directed or undirected graph `G = (V, E)` with non-negative edge weights `w(u, v) >= 0` and a source vertex `s`, find the minimum cumulative weight path from `s` to every reachable vertex `v`.

### The Tension and Dijkstra's Greedy Solution

A brute-force search over all paths is exponential. We cannot simply use standard FIFO BFS because a path with fewer hops can have higher cumulative weight than a path with more hops.

Edsger Dijkstra (1956) solved this with a greedy frontier expansion. The physical intuition resembles a ripple of water expanding outward from a central drop. The boundary expands continuously, reaching closer vertices before farther ones.

```text
BFS:       Expands outward by hop count (FIFO Queue).
Dijkstra:  Expands outward by cumulative weight (Min-Priority Queue).
```

Because edge weights are non-negative, any path continuing outward from the current frontier can only accumulate equal or greater cost. Therefore, the moment the closest unprocessed vertex is extracted from the priority queue, its tentative distance is guaranteed to be its true, final shortest path distance.

---

## 🧠 CHAPTER 2: BUILDING THE MENTAL MODEL

### The Core Analogy: Expanding Concentric Ripples

Imagine dropping a pebble into a pool. Concentric ripples expand outward at a constant speed. The first landmark the ripple touches is permanently finalized: no future wave can reach that landmark earlier because all remaining ripples have already traveled further or started later, and wave speed cannot be negative.

In graph terms:
1. The source vertex starts at distance `0`. All other vertices start at `infinity`.
2. A min-heap maintains active frontier candidates ordered by cumulative distance.
3. In each step, we pop the vertex with the lowest tentative distance.
4. We relax its outgoing edges, updating neighbor distances if a strictly cheaper path is discovered.
5. Once a vertex is extracted and processed, its distance is finalized and never revisited.

### 🖼 Visualizing the Structure

Consider a 5-vertex directed graph (`A`, `B`, `C`, `D`, `E`) with 7 weighted edges:

```text
               (4)
         +-------------> [ B ] ----------+
         |                 ^             |
         |             (1) |             | (5)
        [ A ]              |             v
         |            +----+          [ D ]
         |            |                  |
         |            |                  | (2)
         +---------> [ C ]               v
              (2)     |  \             [ E ]
                      |   \ (8)          ^
                 (10) |    +-------------+
                      +------------------+ (to D)
```

#### Graph Specification
- Vertices: `A, B, C, D, E` (Source: `A`)
- Directed Edges:
  - `A -> B` (weight 4)
  - `A -> C` (weight 2)
  - `C -> B` (weight 1)
  - `B -> D` (weight 5)
  - `C -> D` (weight 10)
  - `C -> E` (weight 8)
  - `D -> E` (weight 2)

#### The Relaxation Principle

Relaxation is the fundamental primitive of all shortest-path algorithms. For an edge `u -> v` with weight `w`:

```text
Current State:
  dist[u] = 2
  dist[v] = 4
  Edge: u -> v with weight w = 1

Check:
  dist[u] + w = 2 + 1 = 3
  Is 3 < dist[v] (4)? YES!

Update (Relax Edge):
  +---------------------------------------------+
  | dist[v] = dist[u] + w = 3                   |
  | predecessor[v] = u                          |
  | Push (3, v) into Priority Queue             |
  +---------------------------------------------+
```

```text
[ u ] (dist[u]) ---------------- weight w ----------------> [ v ] (dist[v])
      \                                                       ^
       \------------- new candidate: dist[u] + w ------------/
                      If (dist[u] + w < dist[v]), update!
```

#### Step-by-Step Distance Table Execution

| Iteration | Extracted Vertex `u` | Pop Cost | Action & Edge Relaxations | Tentative Distances `[A, B, C, D, E]` | Min-Heap Contents (dist, node) |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Init** | - | - | Initialize source `A = 0`, all others `inf` | `[0, inf, inf, inf, inf]` | `[(0, A)]` |
| **Step 1** | **A** | `0` | Relax `A->B` (4), `A->C` (2) | `[0, 4, 2, inf, inf]` | `[(2, C), (4, B)]` |
| **Step 2** | **C** | `2` | Relax `C->B` (`2+1=3 < 4`), `C->D` (`2+10=12`), `C->E` (`2+8=10`) | `[0, 3, 2, 12, 10]` | `[(3, B), (4, B), (10, E), (12, D)]` |
| **Step 3** | **B** | `3` | Relax `B->D` (`3+5=8 < 12`) | `[0, 3, 2, 8, 10]` | `[(4, B), (8, D), (10, E), (12, D)]` |
| **Step 4** | **B (stale)** | `4` | `visited[B] == true` -> **SKIP** | `[0, 3, 2, 8, 10]` | `[(8, D), (10, E), (12, D)]` |
| **Step 5** | **D** | `8` | Relax `D->E` (`8+2=10 == 10`) -> No strict improvement | `[0, 3, 2, 8, 10]` | `[(10, E), (12, D)]` |
| **Step 6** | **E** | `10` | No outgoing edges | `[0, 3, 2, 8, 10]` | `[(12, D)]` |
| **Step 7** | **D (stale)** | `12` | `visited[D] == true` -> **SKIP** | `[0, 3, 2, 8, 10]` | `[]` |

**Final Shortest Paths from A:**
- `A -> A`: Cost `0` (Path: `A`)
- `A -> B`: Cost `3` (Path: `A -> C -> B`)
- `A -> C`: Cost `2` (Path: `A -> C`)
- `A -> D`: Cost `8` (Path: `A -> C -> B -> D`)
- `A -> E`: Cost `10` (Path: `A -> C -> E` or `A -> C -> B -> D -> E`)

### Invariants & Properties: What Stays True

1. **Greedy Finality (Non-negative Weights):** When vertex `u` is extracted from the priority queue, `dist[u]` is globally optimal. Any alternative unvisited path must pass through some other node `x` currently on the frontier with `dist[x] >= dist[u]`. Since all edge weights are non-negative, extending `x` cannot yield a total cost strictly less than `dist[u]`.
2. **Monotonic Extraction:** Extracted distances form a monotonically non-decreasing sequence: `dist[u_1] <= dist[u_2] <= ... <= dist[u_V]`.
3. **Optimal Substructure:** If path `P = (s, ..., u, ..., v)` is a shortest path from `s` to `v`, then the subpath from `s` to `u` is guaranteed to be a shortest path from `s` to `u`.

---

## ⚙️ CHAPTER 3: MECHANICS & IMPLEMENTATION

### Dual-Language Production Implementations

#### C# (.NET 8/9) Production Implementation

```csharp
using System;
using System.Collections.Generic;

public static class DijkstraEngine
{
    /// <summary>
    /// Computes single-source shortest paths using Dijkstra's Algorithm.
    /// Uses .NET 8/9 PriorityQueue<TElement, TPriority> (Min-Heap).
    /// Time Complexity: O((V + E) log V) | Auxiliary Space: O(V + E)
    /// </summary>
    /// <param name="vertices">Total number of vertices (0 to vertices - 1).</param>
    /// <param name="adj">Adjacency list where adj[u] contains (neighbor, weight) pairs.</param>
    /// <param name="source">Designated start vertex.</param>
    /// <returns>Tuple containing shortest distances array and predecessors array for path reconstruction.</returns>
    public static (int[] distances, int[] predecessors) RunDijkstra(
        int vertices, 
        List<(int to, int weight)>[] adj, 
        int source)
    {
        // Guard clauses
        if (adj == null || source < 0 || source >= vertices || vertices <= 0)
        {
            throw new ArgumentException("Invalid input graph or source parameter.");
        }

        int[] dist = new int[vertices];
        int[] pred = new int[vertices];
        bool[] visited = new bool[vertices];

        Array.Fill(dist, int.MaxValue);
        Array.Fill(pred, -1);

        dist[source] = 0;

        // PriorityQueue in .NET 8/9: element is vertex, priority is tentative distance
        var pq = new PriorityQueue<int, int>();
        pq.Enqueue(source, 0);

        while (pq.Count > 0)
        {
            pq.TryDequeue(out int u, out int currentDist);

            // Lazy deletion check: skip stale frontier entries
            if (visited[u]) continue;
            visited[u] = true;

            // Relax all outgoing edges from finalized vertex u
            foreach (var (v, weight) in adj[u])
            {
                if (visited[v]) continue;

                // Guard against integer overflow when adding weight to int.MaxValue
                if (dist[u] != int.MaxValue && dist[u] + weight < dist[v])
                {
                    dist[v] = dist[u] + weight;
                    pred[v] = u;
                    pq.Enqueue(v, dist[v]);
                }
            }
        }

        return (dist, pred);
    }

    /// <summary>
    /// Reconstructs the shortest path from source to target using predecessor pointers.
    /// </summary>
    public static List<int> ReconstructPath(int target, int[] predecessors)
    {
        var path = new List<int>();
        for (int curr = target; curr != -1; curr = predecessors[curr])
        {
            path.Add(curr);
        }
        path.Reverse();
        return path;
    }
}
```

#### Python (3.11+) Production Implementation

```python
import heapq
from typing import List, Tuple

def dijkstra(
    vertices: int, 
    adj: List[List[Tuple[int, int]]], 
    source: int
) -> Tuple[List[float], List[int]]:
    """
    Computes single-source shortest paths using Dijkstra's Algorithm with heapq.
    
    Time Complexity: O((V + E) log V)
    Auxiliary Space: O(V + E)
    
    :param vertices: Total number of vertices (0 to vertices - 1).
    :param adj: Adjacency list where adj[u] contains (neighbor, weight) tuples.
    :param source: Designated starting node index.
    :return: (distances, predecessors) where predecessors enables path reconstruction.
    """
    if source < 0 or source >= vertices or vertices <= 0:
        raise ValueError("Invalid graph configuration or source vertex.")

    dist: List[float] = [float('inf')] * vertices
    pred: List[int] = [-1] * vertices
    dist[source] = 0.0

    # Priority queue stores tuples of (tentative_distance, vertex)
    pq: List[Tuple[float, int]] = [(0.0, source)]

    while pq:
        current_dist, u = heapq.heappop(pq)

        # Stale entry pruning: if popped distance exceeds best known distance, ignore
        if current_dist > dist[u]:
            continue

        # Relax all incident outgoing edges
        for v, weight in adj[u]:
            new_dist = current_dist + weight
            if new_dist < dist[v]:
                dist[v] = new_dist
                pred[v] = u
                heapq.heappush(pq, (new_dist, v))

    return dist, pred

def reconstruct_path(target: int, predecessors: List[int]) -> List[int]:
    """Reconstructs the vertex path from source to target."""
    path: List[int] = []
    curr = target
    while curr != -1:
        path.append(curr)
        curr = predecessors[curr]
    path.reverse()
    return path
```

---

## ⚖️ CHAPTER 4: PERFORMANCE, TRADE-OFFS & REAL SYSTEMS

### Complexity Deconstruction

| Metric | Complexity | Exact Mathematical Rationale |
| :--- | :--- | :--- |
| **Time (Binary Heap)** | `O((V + E) log V)` | Each vertex is extracted at most once (`V * log V`). Each edge is relaxed at most once, pushing an element into the priority queue (`E * log V`). Sum: `(V + E) log V`. |
| **Time (Dense Array Scan)** | `O(V^2)` | For very dense graphs where `E -> V^2`, scanning an array for the minimum tentative distance takes `O(V)` per vertex, giving `O(V^2 + E) = O(V^2)`, avoiding `log V` heap overhead. |
| **Auxiliary Space** | `O(V + E)` | Distance array (`V`), predecessor array (`V`), visited array (`V`), plus heap storing at most `E` active or stale entries under lazy deletion. |
| **Output Space** | `O(V)` | Array of final distances of length `V`. Path reconstruction requires `O(L)` where `L <= V` is path length. |

### Comparative Algorithm Decision Matrix

| Algorithm | Time Complexity | Negative Weights? | Best Graph Density | Memory Overhead |
| :--- | :--- | :--- | :--- | :--- |
| **BFS** | `O(V + E)` | ❌ (Unweighted only) | Any | Minimal FIFO Queue `O(V)` |
| **0/1 BFS** | `O(V + E)` | ❌ (Weights `0` or `1`) | Sparse/Grid | Deque `O(V)` |
| **Dijkstra (Binary Heap)** | `O((V + E) log V)` | ❌ Non-negative only | Sparse (`E << V^2`) | Min-Heap `O(V + E)` |
| **Dijkstra (Array Scan)** | `O(V^2)` | ❌ Non-negative only | Complete / Dense (`E -> V^2`)| Flat Arrays `O(V)` |
| **Bellman-Ford** | `O(V * E)` | ✅ Detects cycles | Sparse with negatives | Edge List `O(V)` |
| **Floyd-Warshall** | `O(V^3)` | ✅ All-pairs | Small dense (`V <= 500`) | 2D Matrix `O(V^2)` |

### Production Systems: Concise Interview Context Callouts

> [!NOTE]
> **System Design Context: GPS Navigation & Route Planning (Google Maps, Waze)**  
> Real continental road networks contain `5 * 10^7` intersections. A naive Dijkstra query across continental topologies takes multiple seconds. Production engines preprocess graphs using **Contraction Hierarchies** (contracting low-degree residential nodes to form highway shortcuts) and **Bidirectional A\*** (using landmark heuristics to guide bidirectional search cones). These optimizations reduce active search frontiers from millions of nodes down to several hundred, returning sub-10ms queries.

> [!NOTE]
> **Infrastructure Context: OSPF Link-State Internet Routing**  
> Interior Gateway Protocols like Open Shortest Path First (OSPF) run Dijkstra's algorithm inside every core router. Routers flood Link State Advertisements (LSAs) containing interface costs (inversely proportional to bandwidth). Each router builds an identical link-state database and computes a shortest-path tree rooted at itself to populate its line-rate forwarding table (FIB), guaranteeing loop-free packet routing.

> [!NOTE]
> **Production Context: Financial Arbitrage & Multi-Currency Hops**  
> When computing optimal currency conversion routes across banking liquidity pools with transaction basis points, multiplicative fees `(1 - fee)` are converted into additive shortest-path problems via negative logarithms: `-log(rate * (1 - fee))`. If exchange bonuses yield negative edge costs, Dijkstra must be rejected in favor of Bellman-Ford to avoid infinite-gain arbitrage loops.

---

## 🎙️ 45-MINUTE INTERVIEW VERBAL SCRIPT

### Phase 1: Clarification & Constraints (0 - 5 min)
- **Candidate:** *"Before selecting an approach, let me clarify the graph properties: Are edge weights strictly non-negative, or can negative weights exist? If negative weights exist, Dijkstra's greedy choice is invalid, and we must pivot to Bellman-Ford or Floyd-Warshall."*
- **Interviewer:** *"All edge weights are non-negative integers between 0 and 10^4."*
- **Candidate:** *"Understood. What are the constraints on `V` (vertices) and `E` (edges)? Is the graph sparse (`E = O(V)`) or dense (`E = O(V^2)`)?"*
- **Interviewer:** *"V is up to 10^5, and E is up to 3 * 10^5."*
- **Candidate:** *"With `V = 10^5` and `E = 3 * 10^5`, this is a sparse graph. Dijkstra using a Min-Priority Queue with lazy deletion gives `O((V + E) log V)` time and `O(V + E)` auxiliary space, executing in under 150 ms."*

### Phase 2: High-Level Approach & Intuition (5 - 12 min)
- **Candidate:** *"The core mental model is greedy frontier expansion. We maintain a min-heap of `(tentative_distance, vertex)`. We initialize `dist[source] = 0` and all other vertices to `infinity`. At each step, we extract the vertex `u` with the smallest tentative distance. Because all weights are non-negative, `dist[u]` cannot be improved by any unexplored path, making this greedy choice globally optimal. We then relax all incident edges `(u, v)`. If `dist[u] + weight < dist[v]`, we update `dist[v]` and push the updated pair into the heap."*

### Phase 3: Coding Walkthrough (12 - 30 min)
- **Candidate:** *"I will now implement this in C# using .NET's `PriorityQueue<int, int>` (or Python using `heapq`). Notice two critical engineering details:
  1. **Stale Entry Elimination:** Standard heaps do not support `O(log V)` decrease-key operations without complex index-tracking structures. Instead, we allow multiple entries for a vertex in the heap (lazy deletion) and immediately skip popped vertices if `visited[u]` is already true.
  2. **Overflow Prevention:** When initializing distances to `int.MaxValue`, adding a positive edge weight causes integer overflow. We explicitly check `dist[u] != int.MaxValue` before evaluating `dist[u] + weight < dist[v]`."*

### Phase 4: Dry Run & Edge Cases (30 - 38 min)
- **Candidate:** *"Let's dry-run key edge cases:
  - **Disconnected components:** Any vertex unreachable from `source` remains at `int.MaxValue` / `inf`.
  - **Zero-weight edges:** Handled correctly without cycles because relaxation requires strict improvement (`<`).
  - **Parallel edges:** The lighter edge is prioritized by the min-heap; heavier edges become stale entries and are skipped."*

### Phase 5: Complexity & Trade-Offs (38 - 45 min)
- **Candidate:** *"To summarize complexity:
  - **Time:** `O((V + E) log V)` because each vertex is popped once, and each edge generates at most one heap insertion.
  - **Auxiliary Space:** `O(V + E)` for distance arrays, visited bitsets, and heap entries.
  - **Trade-Offs:** If all weights were uniform, standard BFS would be faster at `O(V + E)`. If weights were only 0 or 1, 0/1 BFS with a double-ended queue would achieve `O(V + E)`. For general non-negative weights, Dijkstra with a binary heap is optimal."*

---

## ⚔️ SUPPLEMENTARY OUTCOMES

### Practice Problem Ladder

| Problem | Source | Difficulty | Key Concept | Target Time |
| :--- | :--- | :--- | :--- | :--- |
| Network Delay Time | LeetCode #743 | 🟡 Medium | Canonical Dijkstra SSSP | 15 mins |
| Path with Minimum Effort | LeetCode #1631 | 🟡 Medium | Minimax edge relaxation on 2D grid | 20 mins |
| Cheapest Flights Within K Stops | LeetCode #787 | 🟡 Medium | Constrained Dijkstra / Bellman-Ford | 25 mins |
| Swim in Rising Water | LeetCode #778 | 🟡 Medium | Bottleneck capacity Dijkstra | 20 mins |
| Minimum Cost to Reach Destination in Time | LeetCode #1928 | 🔴 Hard | 2D State Dijkstra `dist[node][time]` | 35 mins |

---

## 📊 COMPLEXITY ANALYSIS REFERENCE TABLE

| Variant | Time Complexity | Auxiliary Space | Output Space | Negative Weights? |
| :--- | :--- | :--- | :--- | :--- |
| **Dijkstra (Binary Heap)** | `O((V + E) log V)` | `O(V + E)` | `O(V)` | ❌ No |
| **Dijkstra (Fibonacci Heap)** | `O(E + V log V)` | `O(V)` | `O(V)` | ❌ No |
| **Dijkstra (Array Scan)** | `O(V^2)` | `O(V)` | `O(V)` | ❌ No |
| **BFS (Unweighted)** | `O(V + E)` | `O(V)` | `O(V)` | ❌ No |
| **0/1 BFS (Weights 0, 1)** | `O(V + E)` | `O(V)` | `O(V)` | ❌ No |

---

> 🧭 **Navigation:** [← Week Overview](README.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_09_Day_02_Bellman_Ford_Negative_Weights_Instructional.md)
