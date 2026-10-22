# 📘 Week 8 Day 1: Graph Models & Representations — Engineering Guide

> 🧭 **Navigation:** [← Week Overview](README.md) • [🏠 Master Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_08_Day_02_Breadth_First_Search_Instructional.md)
> 
> 💡 **Staff Mentor Note:** *Every graph problem in a FAANG interview begins with an architectural commitment: how you represent vertices and edges in physical memory. Pick an Adjacency Matrix for a sparse social network with `10^5` users, and your code crashes with a 40 GB `OutOfMemoryException` before line 5 executes. Pick an Adjacency List when the problem demands billions of `O(1)` edge existence checks, and your latency explodes into cache-miss misery. Today, we demystify the physical mechanics of graph memory layouts, master all 4 representations (including high-performance CSR), and lock in production-grade C# and Python graph builders.*

---

## 🎯 Learning Objectives & Cheat-Sheet Blueprint

By the end of this module, you will be able to:

1. **Explain the physical memory trade-offs** of graph representations: Adjacency Lists, Adjacency Matrices, Edge Lists, Compressed Sparse Row (CSR), and Implicit Grids.
2. **Derive the Handshaking Lemma** and compute in-degrees, out-degrees, and densities without memorizing abstract math formulas.
3. **Trace and construct** any graph topology across all 4 representations on a napkin within 90 seconds.
4. **Prevent the Top 5 Rookie Blunders**: Matrix OOMs at `V = 10^5`, one-way undirected edges, `Dictionary` memory bloat over contiguous arrays, and disconnected component blind spots.
5. **Deliver the 45-Minute Interview Talk Track**: The exact 3-sentence verbal script defending representation selection before typing code.
6. **Implement production-grade graph builders** in modern C# (.NET 8/9, Span-aware, zero-allocation patterns) and idiomatic Python (3.11+).

---

## 🎬 PILLAR 1: The 60-Second Hook & Real-World Metaphor

### The 1-Exabyte Disaster: Flight Maps vs. Social Networks

Imagine you are hired as a backend architect at Meta or LinkedIn. Your first sprint ticket says: **"Build the friendship graph data structure in memory."**

You have **1 Billion users** (`V = 10^9`). On average, each user has **500 friends**.

A junior engineer says: *"Hey, why not allocate a 2D boolean grid `matrix[u][v]`? If user `u` is friends with user `v`, store `true`, otherwise `false`. Checking `HasEdge(u, v)` will be blazing-fast `O(1)`!"*

Let's do the napkin math on that suggestion:

```
  Total cells = V * V = 10^9 * 10^9 = 10^18 cells
  In memory (1 byte per boolean):
  10^18 bytes = 1,000,000,000 Gigabytes = 1,000 Petabytes = 1 Exabyte!
```

> [!CAUTION]
> Storing a 1-Billion user social network as an Adjacency Matrix requires **1 Exabyte of RAM**. Even if you pack 8 booleans into 1 byte using bitmasks, you still need **125 Petabytes**. Your cluster crashes before allocating row 0.

Why did this disaster happen? Because social networks are **sparse graphs**. Out of 1 Billion potential connections, each user connects to only 500 people. That means **`99.99995%` of the matrix is completely empty zeros**. Storing an Adjacency Matrix here is like buying an entire empty skyscraper just to store 500 envelopes.

Now look at the alternative: **An Adjacency List**.
Instead of storing all possible pairings, we only store the connections that actually exist:

```
  Total edges stored = 10^9 users * 500 connections = 5 * 10^11 integer IDs
  At 8 bytes per edge entry:
  5 * 10^11 * 8 bytes ≈ 4 Terabytes
```

`4 Terabytes` can easily sit in RAM across a small cluster of commodity Redis/Memcached nodes. We went from an impossible **1 Exabyte** down to **4 Terabytes**—a `250,000x` memory reduction!

```
Dense Graph (Airport Hubs):               Sparse Graph (Social Network / Roads):
E ≈ V^2 (Almost everyone connects)        E ≈ O(V) (Each node has a tiny fraction)
     [ SFO ] === [ JFK ]                        ( User 1 ) ----- ( User 2 )
      |   \     /   |                                |
      |     \ /     |                                |
     [ ORD ] === [ LAX ]                        ( User 3 )       ( User 4 )
-> Adjacency Matrix is optimal!           -> Adjacency List is mandatory!
```

- **Dense Graphs (`E ≈ V^2`):** Flight routes between 50 major hub airports, all-pairs tournament schedules. Use an **Adjacency Matrix**.
- **Sparse Graphs (`E << V^2`, typically `E ≈ O(V)`):** Social networks, road intersections, web links, dependency trees. Use an **Adjacency List** or **CSR**.

---

## 🪜 PILLAR 2: The Scaffolding Ladder & Toy Napkin Trace

To truly internalize how graphs sit in memory, let's take a single 4-node undirected graph with 5 edges and hand-trace it across **all 4 canonical representations**.

### The Toy 4-Node Graph

Nodes: `V = {0, 1, 2, 3}`  
Undirected Edges: `E = { {0, 1}, {0, 2}, {0, 3}, {1, 2}, {2, 3} }`

```
         ( 0 ) ---------- ( 1 )
           |  \             |
           |    \           |
           |      \         |
           |        \       |
         ( 3 ) ---------- ( 2 )
```

---

### Representation 1: The Edge List

An **Edge List** is simply a flat collection of edge pairs or triples: `(from, to, weight)`.

```
Edge List = [
  [0, 1],
  [0, 2],
  [0, 3],
  [1, 2],
  [2, 3]
]
```

- **Memory:** `O(E)`. Packed array, smallest explicit footprint possible.
- **Edge Check `HasEdge(1, 3)`:** We must scan every single edge in the array. Runtime: `O(E)`.
- **Finding Neighbors of `0`:** Scan entire array and collect every edge touching `0`. Runtime: `O(E)`.
- **When FAANG Uses This:** Algorithms that process edges globally by weight:
  - **Kruskal's Minimum Spanning Tree** (sort edges once by weight, then union-find).
  - **Bellman-Ford** shortest path (relax all `E` edges `V - 1` times).

---

### Representation 2: The Adjacency Matrix

A 2D array of size `V * V` where `matrix[u][v]` is `1` (or `true` or edge weight) if an edge connects `u -> v`, and `0` (or `INF`) otherwise.

```
       Col 0   Col 1   Col 2   Col 3
Row 0 [  0   |   1   |   1   |   1   ]  --> Neighbors of 0: {1, 2, 3}
Row 1 [  1   |   0   |   1   |   0   ]  --> Neighbors of 1: {0, 2}
Row 2 [  1   |   1   |   0   |   1   ]  --> Neighbors of 2: {0, 1, 3}
Row 3 [  1   |   0   |   1   |   0   ]  --> Neighbors of 3: {0, 2}
```

- **Symmetry Rule:** Because this graph is **undirected**, the matrix is completely **symmetric across the main diagonal** (`matrix[u][v] == matrix[v][u]`).
- **Memory:** `O(V^2)`. Requires `4 * 4 = 16` cells.
- **Edge Check `HasEdge(1, 3)`:** Direct coordinate lookup: `matrix[1][3]` is `0`. Runtime: `O(1)`!
- **Finding Neighbors of `1`:** Must iterate through all `V` columns of Row 1 (`[1, 0, 1, 0]`). Runtime: `O(V)`.
- **When FAANG Uses This:**
  - `V <= 1,000` and frequent edge checks (e.g., Floyd-Warshall All-Pairs Shortest Path).
  - Dense graphs where `E ≈ V^2`.

---

### Representation 3: The Adjacency List

An array of dynamic buckets (lists, vectors, or arrays) indexed by vertex ID `0 .. V - 1`. Each bucket holds only the immediate outgoing neighbors of that vertex.

Since undirected edges are bidirectional, each edge `{u, v}` appears twice: once in bucket `u` and once in bucket `v`.

```
  Vertex Index     Heap-Allocated Neighbor Bucket
     [ 0 ]  -----> [ 1, 2, 3 ]
     [ 1 ]  -----> [ 0, 2 ]
     [ 2 ]  -----> [ 0, 1, 3 ]
     [ 3 ]  -----> [ 0, 2 ]
```

- **Memory:** `O(V + E)`. We store `V` bucket pointers plus `2 * E` integer entries.
- **Edge Check `HasEdge(1, 3)`:** Scan `Bucket[1] = [0, 2]`. Neither is `3`. Runtime: `O(deg(1))` where `deg(u) <= V`.
- **Finding Neighbors of `1`:** Immediately access `Bucket[1]`. Size is exactly 2. Runtime: `O(deg(1))`!
- **When FAANG Uses This:**
  - The universal default for 95% of LeetCode graph problems (BFS, DFS, Dijkstra, Topological Sort).

---

### Representation 4: Compressed Sparse Row (CSR / Forward Star)

How do production database engines, graph databases (Neo4j, cuGraph), and high-frequency trading platforms store sparse graphs with **zero pointer chasing** and **zero heap fragmentation**?

They flatten the Adjacency List into two contiguous 1D primitive arrays:
1. `neighbors` (col_indices): All neighbor lists concatenated end-to-end. Total size: `2 * E = 10`.
2. `offsets` (row_ptr): Marks the starting index of each vertex's neighbors in the `neighbors` array. Total size: `V + 1 = 5`.

Let's trace our 4-node graph into CSR:

```
Concatenated Neighbors:
Index:          0   1   2  |  3   4  |  5   6   7  |  8   9
neighbors =   [ 1,  2,  3, |  0,  2, |  0,  1,  3, |  0,  2 ]
Vertex:       |--- u=0 ---|  | u=1 -|  |--- u=2 ---|  | u=3 -|

Offset Markers:
Index:          0    1    2    3    4  (Size: V + 1)
offsets   =   [ 0,   3,   5,   8,  10 ]
```

**How to query neighbors of vertex `u` in CSR:**
To get neighbors of node `2`:
- Start index: `start = offsets[2] = 5`
- End index: `end = offsets[3] = 8`
- Slice: `neighbors[5 .. 8]` gives elements at indices `5, 6, 7` -> `[0, 1, 3]`!
- Degree: `offsets[u + 1] - offsets[u] = 8 - 5 = 3` in `O(1)` time!

> [!TIP]
> **Why Systems Architects Love CSR:** In modern CPUs, dynamic list pointers cause L1/L2 cache misses (pointer hopping). CSR stores all edges in a single flat contiguous array, enabling the CPU prefetcher to stream data across 64-byte hardware cache lines at maximum memory bandwidth.

---

### 🧠 Pause & Predict Micro-Drills

#### Drill 1: The Edge-Existence Trade-off
> **Question:** In our 4-node graph, suppose you need to check whether `(1, 3)` exists.
> How many comparisons does this check take in:
> 1. Adjacency Matrix?
> 2. Adjacency List?
> 3. Edge List?

<details>
<summary>👉 Click to Reveal Staff Answer</summary>

1. **Adjacency Matrix:** Exactly **1 comparison** (`matrix[1][3] == 1`). Cost: `O(1)`.
2. **Adjacency List:** **2 comparisons** (scanning `Bucket[1] = [0, 2]`). Cost: `O(deg(1))`.
3. **Edge List:** **5 comparisons** (scanning all 5 edges in `[[0,1], [0,2], [0,3], [1,2], [2,3]]`). Cost: `O(E)`.
</details>

#### Drill 2: Memory Calculation Under Interview Pressure
> **Question:** You are given `V = 200,000` and `E = 300,000`. You allocate `int[,] matrix = new int[V, V]`.
> 1. What is the exact RAM allocation requested from the OS?
> 2. What happens when your code runs on LeetCode or in production?

<details>
<summary>👉 Click to Reveal Staff Answer</summary>

1. **RAM Calculation:**  
   `V * V = (200,000)^2 = 40,000,000,000` integers.  
   Each 32-bit `int` takes 4 bytes:  
   `40 * 10^9 * 4 bytes = 160,000,000,000 bytes = 160 GB`!
2. **Result:** Instant `System.OutOfMemoryException` (in C#) or `MemoryError` (in Python). Competitive programming platforms cap memory at 256 MB or 512 MB, causing an immediate **Memory Limit Exceeded (MLE)** failure.
</details>

---

## 🎨 PILLAR 3: Pragmatic Visual Models

### 1. ASCII Physical Memory Layout Comparison

Look at what happens inside the heap and cache when your code queries the neighbors of vertex `0`:

```
=============================================================================
1. ADJACENCY MATRIX: 2D Contiguous Block (V * V)
=============================================================================
RAM Address:  0x000  0x004  0x008  0x00C  0x010  0x014  0x018  0x01C ...
Matrix:     [   0  |   1  |   1  |   1  |   1  |   0  |   1  |   0  ... ]
              <--------- Row 0 ---------> <--------- Row 1 --------->
Hardware Behavior:
- CPU streams Row 0 sequentially in one cache line fetch.
- Disadvantage: Wastes memory for zeros when graph is sparse.

=============================================================================
2. ADJACENCY LIST: Array of Pointers to Scattered Dynamic Lists
=============================================================================
Stack / Array of Roots (Size V):
Index [0] (0x100) ----> Heap Object [Cap: 4, Len: 3] -> Contiguous: [ 1 | 2 | 3 ]
Index [1] (0x540) ----> Heap Object [Cap: 2, Len: 2] -> Contiguous: [ 0 | 2 ]
Index [2] (0x8A0) ----> Heap Object [Cap: 4, Len: 3] -> Contiguous: [ 0 | 1 | 3 ]
Index [3] (0x3F0) ----> Heap Object [Cap: 2, Len: 2] -> Contiguous: [ 0 | 2 ]
Hardware Behavior:
- Array of references is contiguous; following reference to bucket requires pointer hop.
- Disadvantage: Heap fragmentation and object reference overhead.

=============================================================================
3. COMPRESSED SPARSE ROW (CSR): Two Flat Packed Arrays
=============================================================================
offsets:    [  0  |  3  |  5  |  8  | 10 ]  (Size: V + 1, Contiguous RAM)
               |     |
               v     v
neighbors:  [  1  |  2  |  3  |  0  |  2  |  0  |  1  |  3  |  0  |  2  ]
            <-- Node 0 range ->
Hardware Behavior:
- Zero pointer dereferencing! Maximum L1 cache spatial locality.
=============================================================================
```

---

### 2. Graph Representation Selection Decision Tree

Use this decision logic during an interview to pick the correct representation in under 10 seconds:

```mermaid
%%{init: {"flowchart": {"htmlLabels": true, "padding": 20}, "themeCSS": "foreignObject { overflow: visible !important; }"}}%%
flowchart TD
    Start["❓ What is the problem domain?"] --> Grid{"2D Grid / Maze / Board?"}
    
    Grid -->|Yes| Implicit["⚡ Implicit Graph<br/>(Directional deltas: dr, dc)<br/>Space: O(1)"]
    Grid -->|No| EdgeCentric{"Need to sort edges<br/>globally by weight?"}
    
    EdgeCentric -->|Yes (Kruskal / Bellman-Ford)| EdgeList["📦 Edge List<br/>(Array of Tuples/Structs)<br/>Space: O(E)"]
    EdgeCentric -->|No| Density{"Graph Density?<br/>(V vs E)"}
    
    Density -->|Dense: E ≈ V^2 OR V &le; 1,000| AdjMat["📊 Adjacency Matrix<br/>(2D Array bool / int)<br/>O(1) Edge Lookup<br/>Space: O(V^2)"]
    Density -->|Sparse: E &lt;&lt; V^2 (Standard)| AdjList["🔗 Adjacency List<br/>(Array of Lists: List&lt;int&gt;())<br/>O(deg(u)) Neighbor Scan<br/>Space: O(V + E)"]

    style Start fill:#f3e5f5,stroke:#7b1fa2,color:#4a148c
    style Implicit fill:#e8f5e9,stroke:#388e3c,color:#1b5e20
    style EdgeList fill:#fff3e0,stroke:#f57c00,color:#e65100
    style AdjMat fill:#e1f5fe,stroke:#0288d1,color:#01579b
    style AdjList fill:#e8f5e9,stroke:#2e7d32,color:#1b5e20
```

---

## 🛑 PILLAR 4: The Rookie Trap Zone

### Side-by-Side Architectural Blunders & Fixes

#### Trap 1: The Matrix OOM Crash at `V = 10^5`

```csharp
// ❌ ROOKIE BLUNDER: Allocates 10^10 ints = 40 GB RAM!
public class Solution
{
    public bool ValidPath(int n, int[][] edges, int source, int destination)
    {
        int[,] matrix = new int[n, n]; // CRASH: System.OutOfMemoryException
        foreach (var edge in edges)
        {
            matrix[edge[0], edge[1]] = 1;
            matrix[edge[1], edge[0]] = 1;
        }
        // ...
    }
}
```

```csharp
// ✅ SENIOR ARCHITECT PATTERN: Direct array of lists, O(V + E) memory
public class Solution
{
    public bool ValidPath(int n, int[][] edges, int source, int destination)
    {
        // Fits comfortably in ~5 MB of RAM for V = 10^5, E = 2 * 10^5
        List<int>[] adj = new List<int>[n];
        for (int i = 0; i < n; i++)
        {
            adj[i] = [];
        }

        foreach (var edge in edges)
        {
            adj[edge[0]].Add(edge[1]);
            adj[edge[1]].Add(edge[0]);
        }
        // ...
    }
}
```

---

#### Trap 2: The One-Way Undirected Edge Blunder

```csharp
// ❌ ROOKIE BLUNDER: Forgets that undirected edges are bidirectional
foreach (var edge in edges)
{
    int u = edge[0];
    int v = edge[1];
    adj[u].Add(v); // BUG: Traversal can walk u -> v, but gets trapped and cannot return v -> u!
}
```

```csharp
// ✅ SENIOR ARCHITECT PATTERN: Explicit mutual wiring with self-loop guard
foreach (var edge in edges)
{
    int u = edge[0];
    int v = edge[1];
    adj[u].Add(v);
    if (u != v) // Guard: Don't duplicate self-loops
    {
        adj[v].Add(u);
    }
}
```

---

#### Trap 3: The `Dictionary<int, List<int>>` Memory & Hashing Tax

When vertex IDs are contiguous integers `0 .. V - 1`, beginners frequently reach for `Dictionary<int, List<int>>` because "it feels like a graph map."

```csharp
// ❌ ROOKIE BLUNDER: Dictionary hashing overhead + 3x memory footprint
var graph = new Dictionary<int, List<int>>();
for (int i = 0; i < n; i++)
{
    graph[i] = new List<int>(); // Allocates hash buckets, Entry structs, collision chains
}
```

```csharp
// ✅ SENIOR ARCHITECT PATTERN: Direct array indexing = O(1) pointer math
List<int>[] graph = new List<int>[n];
for (int i = 0; i < n; i++)
{
    graph[i] = []; // Direct RAM offset calculation: base_addr + (i * 8 bytes)
}
```

> [!NOTE]
> `Dictionary<int, List<int>>` requires internal entry tables, integer hashing, collision checks, and memory fragmentation. `List<int>[]` does direct array pointer math (`base + index * pointer_size`) in 1 CPU instruction. Only use `Dictionary` when vertex IDs are sparse strings, GUIDs, or arbitrary non-contiguous IDs.

---

#### Trap 4: Parallel Edge Overwrite in Adjacency Matrices

```csharp
// ❌ ROOKIE BLUNDER: Overwrites an edge with a worse weight!
// Input has two edges between 0 and 1: [0, 1, 5] and [0, 1, 20]
foreach (var edge in edges)
{
    int u = edge[0], v = edge[1], w = edge[2];
    matrix[u, v] = w; // BUG: If [0, 1, 5] came first, [0, 1, 20] overwrites it!
}
```

```csharp
// ✅ SENIOR ARCHITECT PATTERN: Retain minimum weight for shortest path problems
for (int i = 0; i < n; i++)
    for (int j = 0; j < n; j++)
        matrix[i, j] = (i == j) ? 0 : int.MaxValue;

foreach (var edge in edges)
{
    int u = edge[0], v = edge[1], w = edge[2];
    matrix[u, v] = Math.Min(matrix[u, v], w);
    matrix[v, u] = Math.Min(matrix[v, u], w);
}
```

---

#### Trap 5: The Disconnected Component Blind Spot

```csharp
// ❌ ROOKIE BLUNDER: Assumes graph is fully connected and starts only at node 0
var visited = new bool[n];
Dfs(0, adj, visited); // FAILS if vertices {3, 4, 5} form a separate isolated island!
```

```csharp
// ✅ SENIOR ARCHITECT PATTERN: Outer loop over all vertices
var visited = new bool[n];
int componentCount = 0;
for (int i = 0; i < n; i++)
{
    if (!visited[i])
    {
        componentCount++;
        Dfs(i, adj, visited);
    }
}
```

---

## 🎙️ PILLAR 5: 45-Minute Interview Talk Track

In a live FAANG technical interview, the first 60 seconds of verbal communication establish whether you are perceived as a Junior who jumps straight into code or a Senior Architect who deliberately analyzes constraints.

### The Exact 3-Sentence Script to Speak Out Loud

> *"Looking at the constraints, we have `V = 10^5` vertices and `E = 2 * 10^5` edges, which means this is a sparse graph where an Adjacency Matrix would trigger an immediate `40 GB` Out-Of-Memory failure.*  
> *Therefore, I will represent this network using an **Adjacency List** as a direct array of integer lists `List<int>[]`, providing optimal `O(V + E)` space and `O(deg(u))` neighbor iterations.*  
> *Because vertex identifiers are contiguous 0-indexed integers from `0` to `V - 1`, using a flat array gives us `O(1)` direct index access with zero hashing overhead."*

---

### The 45-Minute Interview Delivery Blueprint

```
00:00 - 05:00  Phase 1: Clarification & Bounds
               - Vertex count V and edge count E? (Check V^2 memory limits!)
               - Directed or Undirected? Weighted or Unweighted?
               - Can edges contain parallel edges or self-loops?
               - Are vertex IDs 0-indexed or 1-indexed?

05:00 - 12:00  Phase 2: Representation & Invariant Defense
               - Announce chosen representation: "Adjacency List via List<int>[]"
               - Defend against Adjacency Matrix: cite concrete memory numbers.
               - Clarify handling of bidirectional edges (u -> v AND v -> u).

12:00 - 32:00  Phase 3: Production Code Construction
               - Initialize graph array with empty buckets.
               - Ingest edge list with boundary validation.
               - Execute core algorithm (BFS, DFS, Dijkstra, TopoSort).

32:00 - 40:00  Phase 4: Systematic Edge-Case Dry Run
               - V = 1, E = 0 (single isolated vertex).
               - Disconnected islands (multiple components).
               - Self-loops (u -> u) and parallel edges.
               - Directed vs Undirected symmetry verification.

40:00 - 45:00  Phase 5: Complexity & Production Scale Follow-ups
               - State Time: O(V + E), Auxiliary Space: O(V + E).
               - Explain CSR if asked how to optimize CPU cache locality for billion-scale graphs.
```

---

## ⚡ PILLAR 6: Production Dual-Language Implementations

### Section 1: High-Performance Contest & Interview Graph Builder

This is the fastest, cleanest representation to write during a 45-minute live interview.

#### C# (.NET 8/9 Idiomatic Array of Lists)

```csharp
using System;
using System.Collections.Generic;

public static class FastGraphBuilder
{
    /// <summary>
    /// Builds an unweighted Adjacency List from a 2D edge array.
    /// Time Complexity: O(V + E)
    /// Auxiliary Space: O(V + E)
    /// </summary>
    public static List<int>[] BuildUnweightedList(int n, int[][] edges, bool isDirected = false)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(n);
        ArgumentNullException.ThrowIfNull(edges);

        // Pre-allocate array of size V
        var adj = new List<int>[n];
        for (int i = 0; i < n; i++)
        {
            adj[i] = [];
        }

        foreach (var edge in edges)
        {
            int u = edge[0];
            int v = edge[1];

            // Boundary validation guard
            if ((uint)u >= (uint)n || (uint)v >= (uint)n)
            {
                throw new ArgumentOutOfRangeException(nameof(edges), $"Edge ({u}, {v}) exceeds vertex count {n}.");
            }

            adj[u].Add(v);
            if (!isDirected && u != v)
            {
                adj[v].Add(u);
            }
        }

        return adj;
    }

    /// <summary>
    /// Builds a weighted Adjacency List storing destination and weight pairs.
    /// Time Complexity: O(V + E)
    /// Auxiliary Space: O(V + E)
    /// </summary>
    public static List<(int To, int Weight)>[] BuildWeightedList(int n, int[][] edges, bool isDirected = false)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(n);
        ArgumentNullException.ThrowIfNull(edges);

        var adj = new List<(int To, int Weight)>[n];
        for (int i = 0; i < n; i++)
        {
            adj[i] = [];
        }

        foreach (var edge in edges)
        {
            int u = edge[0];
            int v = edge[1];
            int w = edge[2];

            adj[u].Add((v, w));
            if (!isDirected && u != v)
            {
                adj[v].Add((u, w));
            }
        }

        return adj;
    }
}
```

#### Python (3.11+ Idiomatic Adjacency List)

```python
from typing import List, Tuple

def build_unweighted_list(n: int, edges: List[List[int]], is_directed: bool = False) -> List[List[int]]:
    """
    Builds an unweighted Adjacency List from an edge list.
    Time Complexity: O(V + E)
    Auxiliary Space: O(V + E)
    """
    if n <= 0:
        raise ValueError("Vertex count must be positive.")

    adj: List[List[int]] = [[] for _ in range(n)]

    for u, v in edges:
        if not (0 <= u < n and 0 <= v < n):
            raise IndexError(f"Edge ({u}, {v}) out of bounds [0, {n - 1}].")
        adj[u].append(v)
        if not is_directed and u != v:
            adj[v].append(u)

    return adj


def build_weighted_list(
    n: int, edges: List[List[int]], is_directed: bool = False
) -> List[List[Tuple[int, int]]]:
    """
    Builds a weighted Adjacency List storing (neighbor, weight) tuples.
    Time Complexity: O(V + E)
    Auxiliary Space: O(V + E)
    """
    adj: List[List[Tuple[int, int]]] = [[] for _ in range(n)]

    for u, v, w in edges:
        adj[u].append((v, w))
        if not is_directed and u != v:
            adj[v].append((u, w))

    return adj
```

---

### Section 2: Complete Object-Oriented Graph Class

A fully encapsulated, production-grade graph model implementing all 8 canonical operations:
1. `AddVertex()`
2. `RemoveVertex(v)`
3. `AddEdge(u, v, weight)`
4. `RemoveEdge(u, v)`
5. `HasEdge(u, v)`
6. `GetNeighbors(u)`
7. `InDegree(u)`
8. `OutDegree(u)`

#### C# (.NET 8/9 Complete Class)

```csharp
using System;
using System.Collections.Generic;

public sealed class ProductionGraph
{
    // Adjacency list storing (Destination, Weight) per vertex
    private readonly List<List<(int To, int Weight)>> _adj;
    public bool IsDirected { get; }

    public int VertexCount => _adj.Count;
    public int EdgeCount { get; private set; }

    public ProductionGraph(int initialVertices = 0, bool isDirected = false)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(initialVertices);
        IsDirected = isDirected;
        _adj = new List<List<(int To, int Weight)>>(initialVertices);
        for (int i = 0; i < initialVertices; i++)
        {
            _adj.Add([]);
        }
    }

    // 1. Add Vertex: O(1) amortized
    public int AddVertex()
    {
        _adj.Add([]);
        return _adj.Count - 1;
    }

    // 2. Remove Vertex: O(V + E)
    public void RemoveVertex(int v)
    {
        ValidateVertex(v);

        // Deduct outgoing edges
        EdgeCount -= _adj[v].Count;
        _adj.RemoveAt(v);

        // Scrub all incoming references to v and re-index references > v
        for (int i = 0; i < _adj.Count; i++)
        {
            var edges = _adj[i];
            for (int j = edges.Count - 1; j >= 0; j--)
            {
                if (edges[j].To == v)
                {
                    edges.RemoveAt(j);
                    if (IsDirected) EdgeCount--;
                }
                else if (edges[j].To > v)
                {
                    edges[j] = (edges[j].To - 1, edges[j].Weight);
                }
            }
        }
    }

    // 3. Add Edge: O(1)
    public void AddEdge(int u, int v, int weight = 1)
    {
        ValidateVertex(u);
        ValidateVertex(v);

        _adj[u].Add((v, weight));
        EdgeCount++;

        if (!IsDirected && u != v)
        {
            _adj[v].Add((u, weight));
        }
    }

    // 4. Remove Edge: O(deg(u))
    public bool RemoveEdge(int u, int v)
    {
        ValidateVertex(u);
        ValidateVertex(v);

        int removed = _adj[u].RemoveAll(e => e.To == v);
        if (removed > 0)
        {
            EdgeCount -= removed;
            if (!IsDirected && u != v)
            {
                _adj[v].RemoveAll(e => e.To == u);
            }
            return true;
        }
        return false;
    }

    // 5. Has Edge: O(deg(u))
    public bool HasEdge(int u, int v)
    {
        ValidateVertex(u);
        ValidateVertex(v);

        foreach (var edge in _adj[u])
        {
            if (edge.To == v) return true;
        }
        return false;
    }

    // 6. Get Neighbors: O(1) reference access
    public IReadOnlyList<(int To, int Weight)> GetNeighbors(int u)
    {
        ValidateVertex(u);
        return _adj[u];
    }

    // 7. Out-Degree: O(1)
    public int OutDegree(int u)
    {
        ValidateVertex(u);
        return _adj[u].Count;
    }

    // 8. In-Degree: O(V + E) for directed, O(1) for undirected
    public int InDegree(int u)
    {
        ValidateVertex(u);
        if (!IsDirected) return OutDegree(u);

        int count = 0;
        for (int i = 0; i < _adj.Count; i++)
        {
            foreach (var edge in _adj[i])
            {
                if (edge.To == u) count++;
            }
        }
        return count;
    }

    private void ValidateVertex(int v)
    {
        if ((uint)v >= (uint)_adj.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(v), $"Vertex {v} is outside valid range [0, {_adj.Count - 1}].");
        }
    }
}
```

#### Python (3.11+ Complete Class)

```python
from typing import List, Tuple

class ProductionGraph:
    """Production-grade dynamic Adjacency List supporting all 8 core graph operations."""
    __slots__ = ("_adj", "is_directed", "edge_count")

    def __init__(self, initial_vertices: int = 0, is_directed: bool = False) -> None:
        if initial_vertices < 0:
            raise ValueError("Initial vertices must be non-negative.")
        self.is_directed: bool = is_directed
        self._adj: List[List[Tuple[int, int]]] = [[] for _ in range(initial_vertices)]
        self.edge_count: int = 0

    @property
    def vertex_count(self) -> int:
        return len(self._adj)

    # 1. Add Vertex: O(1) amortized
    def add_vertex(self) -> int:
        self._adj.append([])
        return len(self._adj) - 1

    # 2. Remove Vertex: O(V + E)
    def remove_vertex(self, v: int) -> None:
        self._validate_vertex(v)
        self.edge_count -= len(self._adj[v])
        del self._adj[v]

        for i in range(len(self._adj)):
            new_edges = []
            for dest, weight in self._adj[i]:
                if dest == v:
                    if self.is_directed:
                        self.edge_count -= 1
                    continue
                elif dest > v:
                    new_edges.append((dest - 1, weight))
                else:
                    new_edges.append((dest, weight))
            self._adj[i] = new_edges

    # 3. Add Edge: O(1)
    def add_edge(self, u: int, v: int, weight: int = 1) -> None:
        self._validate_vertex(u)
        self._validate_vertex(v)
        self._adj[u].append((v, weight))
        self.edge_count += 1
        if not self.is_directed and u != v:
            self._adj[v].append((u, weight))

    # 4. Remove Edge: O(deg(u))
    def remove_edge(self, u: int, v: int) -> bool:
        self._validate_vertex(u)
        self._validate_vertex(v)
        orig_len = len(self._adj[u])
        self._adj[u] = [e for e in self._adj[u] if e[0] != v]
        removed = orig_len - len(self._adj[u])

        if removed > 0:
            self.edge_count -= removed
            if not self.is_directed and u != v:
                self._adj[v] = [e for e in self._adj[v] if e[0] != u]
            return True
        return False

    # 5. Has Edge: O(deg(u))
    def has_edge(self, u: int, v: int) -> bool:
        self._validate_vertex(u)
        self._validate_vertex(v)
        return any(dest == v for dest, _ in self._adj[u])

    # 6. Get Neighbors: O(1)
    def get_neighbors(self, u: int) -> List[Tuple[int, int]]:
        self._validate_vertex(u)
        return self._adj[u]

    # 7. Out-Degree: O(1)
    def out_degree(self, u: int) -> int:
        self._validate_vertex(u)
        return len(self._adj[u])

    # 8. In-Degree: O(V + E) for directed, O(1) for undirected
    def in_degree(self, u: int) -> int:
        self._validate_vertex(u)
        if not self.is_directed:
            return self.out_degree(u)
        return sum(1 for edges in self._adj for dest, _ in edges if dest == u)

    def _validate_vertex(self, v: int) -> None:
        if not (0 <= v < len(self._adj)):
            raise IndexError(f"Vertex {v} is outside bounds [0, {len(self._adj) - 1}].")
```

---

### Section 3: High-Performance Compressed Sparse Row (CSR) Implementation

This implementation showcases Senior/Staff engineering awareness of cache locality and zero-allocation memory layouts.

#### C# (.NET 8/9 CSR Graph)

```csharp
using System;

public sealed class CsrGraph
{
    private readonly int[] _offsets;   // Size: V + 1
    private readonly int[] _neighbors; // Size: E (or 2E for undirected)

    public int VertexCount => _offsets.Length - 1;
    public int EdgeCount => _neighbors.Length;

    public CsrGraph(int n, int[][] edges, bool isDirected = false)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(n);

        // Step 1: Count degrees for each vertex
        int[] degrees = new int[n];
        foreach (var edge in edges)
        {
            degrees[edge[0]]++;
            if (!isDirected && edge[0] != edge[1])
            {
                degrees[edge[1]]++;
            }
        }

        // Step 2: Compute prefix sums for offsets array
        _offsets = new int[n + 1];
        for (int i = 0; i < n; i++)
        {
            _offsets[i + 1] = _offsets[i] + degrees[i];
        }

        // Step 3: Fill neighbors array using cursor pointers
        _neighbors = new int[_offsets[n]];
        int[] cursor = (int[])_offsets.Clone();

        foreach (var edge in edges)
        {
            int u = edge[0];
            int v = edge[1];

            _neighbors[cursor[u]++] = v;
            if (!isDirected && u != v)
            {
                _neighbors[cursor[v]++] = u;
            }
        }
    }

    /// <summary>
    /// Returns a zero-allocation Span slice of neighbors for vertex u.
    /// Memory: 0 bytes allocated on heap!
    /// </summary>
    public ReadOnlySpan<int> GetNeighbors(int u)
    {
        if ((uint)u >= (uint)VertexCount)
        {
            throw new ArgumentOutOfRangeException(nameof(u));
        }

        int start = _offsets[u];
        int end = _offsets[u + 1];
        return _neighbors.AsSpan(start, end - start);
    }

    public int GetDegree(int u)
    {
        if ((uint)u >= (uint)VertexCount)
        {
            throw new ArgumentOutOfRangeException(nameof(u));
        }
        return _offsets[u + 1] - _offsets[u];
    }
}
```

#### Python (3.11+ CSR Graph)

```python
from typing import List, Sequence

class CsrGraph:
    """Compressed Sparse Row (CSR) representation for maximum cache locality."""
    __slots__ = ("_offsets", "_neighbors", "_vertex_count")

    def __init__(self, n: int, edges: List[List[int]], is_directed: bool = False) -> None:
        if n <= 0:
            raise ValueError("Vertex count must be positive.")
        self._vertex_count = n

        # Step 1: Count degrees
        degrees = [0] * n
        for u, v in edges:
            degrees[u] += 1
            if not is_directed and u != v:
                degrees[v] += 1

        # Step 2: Prefix sum for offsets
        self._offsets = [0] * (n + 1)
        for i in range(n):
            self._offsets[i + 1] = self._offsets[i] + degrees[i]

        # Step 3: Populate neighbors
        self._neighbors = [0] * self._offsets[n]
        cursor = list(self._offsets)

        for u, v in edges:
            self._neighbors[cursor[u]] = v
            cursor[u] += 1
            if not is_directed and u != v:
                self._neighbors[cursor[v]] = u
                cursor[v] += 1

    def get_neighbors(self, u: int) -> Sequence[int]:
        if not (0 <= u < self._vertex_count):
            raise IndexError("Vertex index out of bounds.")
        start = self._offsets[u]
        end = self._offsets[u + 1]
        return self._neighbors[start:end]

    def get_degree(self, u: int) -> int:
        if not (0 <= u < self._vertex_count):
            raise IndexError("Vertex index out of bounds.")
        return self._offsets[u + 1] - self._offsets[u]
```

---

### Section 4: Implicit Grid Graph Representation

For 2D grid problems (e.g. LeetCode #200 Number of Islands, #994 Rotting Oranges), **never construct an explicit graph**. Calculate valid neighbor coordinates on-the-fly using directional delta vectors.

#### C# (.NET 8/9 Implicit Grid Navigator)

```csharp
using System.Collections.Generic;

public static class GridNavigator
{
    // 4-directional cardinal deltas: Up, Down, Left, Right
    private static readonly (int Dr, int Dc)[] Dirs4 = [(-1, 0), (1, 0), (0, -1), (0, 1)];

    // 8-directional cardinal + diagonal deltas
    private static readonly (int Dr, int Dc)[] Dirs8 = [
        (-1, -1), (-1, 0), (-1, 1),
        ( 0, -1),          ( 0, 1),
        ( 1, -1), ( 1, 0), ( 1, 1)
    ];

    public static IEnumerable<(int Nr, int Nc)> GetNeighbors(
        int r, int c, int rows, int cols, bool allowDiagonals = false)
    {
        var deltas = allowDiagonals ? Dirs8 : Dirs4;
        foreach (var (dr, dc) in deltas)
        {
            int nr = r + dr;
            int nc = c + dc;
            if ((uint)nr < (uint)rows && (uint)nc < (uint)cols)
            {
                yield return (nr, nc);
            }
        }
    }
}
```

#### Python (3.11+ Implicit Grid Navigator)

```python
from typing import Iterator, Tuple

DIRS_4 = ((-1, 0), (1, 0), (0, -1), (0, 1))
DIRS_8 = ((-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1))

def get_grid_neighbors(
    r: int, c: int, rows: int, cols: int, allow_diagonals: bool = False
) -> Iterator[Tuple[int, int]]:
    """Yields valid in-bounds neighbor coordinates without allocating explicit graph memory."""
    deltas = DIRS_8 if allow_diagonals else DIRS_4
    for dr, dc in deltas:
        nr, nc = r + dr, c + dc
        if 0 <= nr < rows and 0 <= nc < cols:
            yield nr, nc
```

---

## 📊 Comprehensive Complexity & Memory Blueprint

### Operations & Asymptotic Trade-Offs

| Operation | Adjacency List (`List<int>[]`) | Adjacency Matrix (`int[,]`) | Edge List (`List<Edge>`) | CSR (`offsets`, `neighbors`) | Implicit Grid (`R * C`) |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **`AddVertex()`** | `O(1)` amortized | `O(V)` dynamic / `O(V^2)` realloc | `O(1)` | `O(V + E)` rebuild | `N/A` (Fixed grid geometry) |
| **`RemoveVertex(v)`** | `O(V + E)` (scrub & shift) | `O(V)` dynamic / `O(V^2)` realloc | `O(E)` (filter & shift) | `O(V + E)` rebuild | `N/A` (Cell state flip) |
| **`AddEdge(u, v)`** | `O(1)` | `O(1)` | `O(1)` | `O(V + E)` rebuild | `N/A` (Implicit coordinate rule) |
| **`RemoveEdge(u, v)`**| `O(deg(u))` | `O(1)` | `O(E)` | `O(deg(u))` shift | `N/A` (Cell state flip) |
| **`HasEdge(u, v)`** | `O(deg(u))` | `O(1)` | `O(E)` | `O(log(deg(u)))` (binary search) | `O(1)` (Delta check) |
| **`GetNeighbors(u)`**| `O(1)` access, `O(deg(u))` read | `O(V)` full row scan | `O(E)` full scan | `O(1)` Span slice | `O(1)` (At most 4 or 8) |
| **`InDegree(u)`** | `O(V + E)` (or `O(1)` cached) | `O(V)` column scan | `O(E)` full scan | `O(V + E)` (or `O(1)` if undirected) | `O(1)` |
| **`OutDegree(u)`** | `O(1)` | `O(V)` row scan | `O(E)` full scan | `O(1)` (`offsets[u+1] - offsets[u]`) | `O(1)` |
| **Auxiliary Space** | `O(V + E)` | `O(V^2)` | `O(E)` | `O(V + E)` (Minimal overhead) | `O(1)` (Zero graph storage) |
| **Cache Locality** | Moderate (Heap hopping) | High (Row streaming) | High (Packed stream) | **Maximum** (Flat packed arrays) | High (2D matrix layout) |

---

## 🏋️ PRACTICE LADDER

Master these problems to solidify your graph representation intuition before proceeding to traversals:

| Problem | LeetCode # | Difficulty | Representation Pattern | Key Invariant / Test Goal |
| :--- | :--- | :--- | :--- | :--- |
| [Find Center of Star Graph](https://leetcode.com/problems/find-center-of-star-graph/) | #1791 | 🟢 Easy | Edge List Inspection | The center node appears in every edge; check first 2 edges only (`O(1)` time). |
| [Find if Path Exists in Graph](https://leetcode.com/problems/find-if-path-exists-in-graph/) | #1971 | 🟢 Easy | Adjacency List Construction | Build undirected `List<int>[]` from `edges` and run BFS/DFS. |
| [Clone Graph](https://leetcode.com/problems/clone-graph/) | #133 | 🟡 Medium | Adjacency List + Node Map | Deep copy graph topology preserving vertex identities via a hash map. |
| [Number of Islands](https://leetcode.com/problems/number-of-islands/) | #200 | 🟡 Medium | Implicit Grid Graph | In-place cell mutation using 4-directional delta vectors (`dr`, `dc`). |
| [Reorder Routes to Make All Paths Lead to the City Zero](https://leetcode.com/problems/reorder-routes-to-make-all-paths-lead-to-the-city-zero/) | #1466 | 🟡 Medium | Directed vs Artificial Reverse Edges | Model original directed edges with weight `1` and artificial reverse with weight `0`. |
| [Minimum Degree of a Connected Trio in a Graph](https://leetcode.com/problems/minimum-degree-of-a-connected-trio-in-a-graph/) | #1761 | 🔴 Hard | Hybrid Matrix + List | Fast `O(1)` edge existence checks via matrix combined with list neighbor scans. |

---

> 🧭 **Navigation:** [← Week Overview](README.md) • [🏠 Master Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_08_Day_02_Breadth_First_Search_Instructional.md)
