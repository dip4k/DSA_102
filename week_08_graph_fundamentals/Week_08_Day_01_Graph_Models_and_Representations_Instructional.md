# 📘 Week 8 Day 1: Graph Models & Representations — Engineering Guide

> 🧭 **Navigation:** [← Week Overview](README.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_08_Day_02_Breadth_First_Search_Instructional.md)
> 
> 💡 **Instructor Note:** *Master the trade-offs between adjacency lists, adjacency matrices, edge lists, and implicit grid representations. In technical interviews, selecting the wrong representation (e.g., allocating a `V * V` matrix for `10^5` vertices) causes immediate Memory Limit Exceeded (MLE) or Time Limit Exceeded (TLE) failures. Every graph problem starts with an architectural decision on how vertices and edges are stored.*

---

## 🎯 LEARNING OBJECTIVES

By the end of this chapter, you will be able to:

- **Formalize** graph anatomy: vertices, edges, directionality, edge weights, cyclicity (Cyclic vs DAG), and density metrics (sparse vs dense).
- **Implement all canonical operations** across three core explicit representations: `AddVertex`, `RemoveVertex`, `AddEdge`, `RemoveEdge`, `HasEdge`, `GetNeighbors`, `InDegree`, and `OutDegree`.
- **Implement** production-grade Adjacency Lists, Adjacency Matrices, Edge Lists, and Implicit Grid generators in modern C# (.NET 8/9) and idiomatic Python (3.11+).
- **Evaluate** memory footprints and asymptotic complexities across sparse (`E << V^2`) and dense (`E ≈ V^2`) topologies with side-by-side comparative matrices.
- **Diagnose & avoid** beginner pitfalls: disconnected graphs, self-loops, parallel edges, cycle infinite loops, and 0-index vs 1-index offsets.
- **Deliver** a structured 45-minute technical interview verbal script defending representation trade-offs against CPU cache locality and scale.

---

## 📖 CHAPTER 1: CONTEXT & MOTIVATION

### The Graph as a Universal Abstraction

Every interconnected computing problem reduces to a graph `G = (V, E)`:
1. **Social Networks (LinkedIn, Meta):** Vertices are users; edges are connections or follows. Queries require listing neighbors of `u` in `O(deg(u))` time.
2. **Build Systems & Compilers (MSBuild, Cargo, Roslyn):** Vertices are compilation targets; directed edges indicate dependencies. Circular dependencies indicate broken builds (cyclic graphs).
3. **Route Planning & Mapping (Google Maps, OpenStreetMap):** Vertices are physical intersections; weighted edges are road segments with traversal latencies.
4. **State Spaces & Puzzles (Word Ladder, 8-Puzzle, Chess):** States are vertices; valid legal moves are edges generated on-the-fly (**implicit graphs**).

### The Core Engineering Challenge

A graph is an abstract mathematical set of vertices `V` and edges `E`. Hardware memory, however, is a flat addressable byte array. How we project `(V, E)` into physical memory dictates whether neighbor iteration takes `1` microsecond or causes CPU cache starvation.

Choosing the representation is the architectural commitment of every graph algorithm:
- Choose an **Adjacency Matrix** for sparse graphs, and an application with `10^5` vertices immediately crashes with a `40 GB` memory allocation.
- Choose an **Adjacency List** when an algorithm requires constant-time edge existence checks (`HasEdge(u, v)`), and runtime degrades into linear scans over dense neighbor lists.
- Store an **Implicit Graph** explicitly, and memory explodes exploring state spaces with millions of states.

---

## 🧠 CHAPTER 2: BUILDING THE MENTAL MODEL

### 1. Anatomy of a Graph: Core Dimensions

Every graph `G = (V, E)` is defined by its vertex set `V` and edge set `E`. In practice, graphs are classified across five fundamental orthogonal dimensions:

```
Graph Anatomy Taxonomies:
  ├── 1. Directionality:
  │     ├── Undirected: Edge {u, v} is symmetric (two-way highway)
  │     └── Directed (Digraph): Edge (u, v) is asymmetric (one-way street: u -> v)
  ├── 2. Edge Weights:
  │     ├── Unweighted: Unit cost per hop (w = 1) -> BFS guarantees shortest path
  │     └── Weighted: Arbitrary traversal cost w(u, v) -> Dijkstra / Bellman-Ford required
  ├── 3. Cyclicity:
  │     ├── Cyclic: Contains at least one closed walk (v0 -> v1 -> ... -> vk -> v0)
  │     └── Acyclic (DAG): Zero directed cycles -> Pre-requisite for Topological Sorting
  ├── 4. Density Metric:
  │     ├── Sparse: E ≈ O(V) -> Adjacency List optimal (O(V + E) memory)
  │     └── Dense:  E ≈ O(V^2) -> Adjacency Matrix optimal (O(1) edge lookups)
  └── 5. Explicit vs. Implicit:
        ├── Explicit: Adjacency List / Matrix / Edge List stored in memory
        └── Implicit: Vertices and edges generated on-the-fly (Grid deltas, game boards)
```

#### A. Vertices & Edges (Formal Notation & Intuition)
- **Vertex (Node) Set `V`:** The entities in the network. Cardinality is denoted `|V|` or `V`. In software implementations, vertices are typically mapped to contiguous 0-indexed integers `0, 1, ..., V - 1` for `O(1)` array indexing.
- **Edge (Arc) Set `E`:** The links between vertices. Cardinality is denoted `|E|` or `E`.
  - In **undirected graphs**, an edge is an unordered pair `{u, v}`: traversal is bidirectional (`u <-> v`).
  - In **directed graphs (digraphs)**, an edge is an ordered pair `(u, v)`: traversal is unidirectional from source/tail `u` to destination/head `v` (`u -> v`).

#### B. Directed vs. Undirected & The Handshaking Lemma
- **Degree in Undirected Graphs:** The degree of vertex `u`, denoted `deg(u)`, is the number of edges incident to `u`.
  - **Handshaking Lemma:** `sum(deg(v)) = 2 * |E|` for all `v` in `V`.
  - *Engineering consequence:* The sum of degrees in any undirected graph is always even. Every edge contributes exactly `+1` to two vertices (or `+2` for a self-loop).
- **Degrees in Directed Graphs:**
  - **In-Degree `in_deg(u)`:** The number of incoming edges pointing to `u` (`* -> u`).
  - **Out-Degree `out_deg(u)`:** The number of outgoing edges leaving `u` (`u -> *`).
  - **Directed Lemma:** `sum(in_deg(v)) = sum(out_deg(v)) = |E|` for all `v` in `V`.

```
Undirected Graph (Mutual):             Directed Graph (Asymmetric):
       ( 0 )                                  ( 0 )
      /     \                                /     \
     /       \                              v       v
   ( 1 ) <-> ( 2 )                        ( 1 ) --> ( 2 )
Edges: {0,1}, {0,2}, {1,2}             Edges: (0,1), (0,2), (1,2)
deg(0)=2, deg(1)=2, deg(2)=2           out_deg(0)=2, in_deg(0)=0
Sum(deg) = 6 = 2 * 3 edges             out_deg(1)=1, in_deg(1)=1
                                       out_deg(2)=0, in_deg(2)=2
                                       Sum(in) = Sum(out) = 3 edges
```

#### C. Weighted vs. Unweighted Graphs
- **Unweighted Graphs:** Every edge represents a single uniform hop (cost = 1). Breadth-First Search (BFS) explores vertices in strictly non-decreasing order of hop distance, guaranteeing the shortest path in `O(V + E)` time.
- **Weighted Graphs:** Each edge `(u, v)` carries an associated numerical weight `w(u, v)` (real or integer value) representing latency, physical road length, monetary cost, or network capacity.
  - When all weights are non-negative (`w >= 0`), **Dijkstra's Algorithm** is required.
  - When negative weights are present, **Bellman-Ford** or **SPFA** is required.
  - In adjacency matrices, an absent edge is represented by a sentinel (`INF` or `int.MaxValue`), while the distance from a node to itself is `0`.

```
Unweighted (Unit hops: cost 1):       Weighted (Edge costs):
       ( 0 )                                  ( 0 )
      /     \                                /     \
    1/       \1                           10/       \25
    /         \                            /         \
  ( 1 ) ---- ( 2 )                       ( 1 ) ----- ( 2 )
          1                                      5
Shortest 0 -> 2: 1 hop (cost 1)        Shortest 0 -> 2: Path 0->1->2 (cost 10+5=15 < 25)
```

#### D. Cyclic Graphs vs. Directed Acyclic Graphs (DAGs)
- **Cycle:** A closed path `v0 -> v1 -> ... -> vk -> v0` where `k >= 1` and all edges are distinct.
  - In undirected graphs, a cycle requires at least 3 distinct vertices (ignoring trivial backtracks across the same undirected edge).
  - In directed graphs, a cycle can be of length 1 (self-loop: `u -> u`), length 2 (`u -> v -> u`), or more.
  - *Engineering consequence:* Any traversal (BFS or DFS) on a cyclic graph **strictly requires a visited tracking mechanism** (`visited` set or array) to prevent infinite loops.
- **Directed Acyclic Graph (DAG):** A directed graph containing **zero** directed cycles.
  - Models dependency hierarchies, prerequisite schedules, dynamic programming state spaces, and Git commit trees.
  - **Fundamental Theorem of DAGs:** Every finite DAG has at least one source (`in_degree == 0`) and at least one sink (`out_degree == 0`), and admits at least one linear ordering known as a **Topological Sort**.

```
Cyclic Directed Graph:                 Directed Acyclic Graph (DAG):
       ( 0 )                                  ( 0 )
      /     ^                                /     \
     v       \                              v       v
   ( 1 ) --> ( 2 )                        ( 1 ) --> ( 2 )
Cycle: 0 -> 1 -> 2 -> 0                Topological Order: 0 -> 1 -> 2
No topological order exists!           Valid dependency schedule!
```

#### E. Sparse vs. Dense Topologies
The density `D` measures how close the edge count is to the theoretical maximum:
- **Maximum Edges:**
  - Undirected without self-loops: `E_max = V * (V - 1) / 2`
  - Directed without self-loops: `E_max = V * (V - 1)`
- **Density Metric:** `D = E / E_max`.
  - **Sparse Graphs (`D << 1`, typically `E ≈ O(V)`):** Most real-world graphs (social networks, web page links, road networks). For `V = 10^5`, `E ≈ 2 * 10^5`. Adjacency lists are mandatory (`O(V + E)` space ≈ a few megabytes).
  - **Dense Graphs (`D ≈ 1`, typically `E ≈ O(V^2)`):** All-pairs travel networks, tournament pairings, dense feature correlations. Adjacency matrices are optimal (`O(1)` edge lookups, hardware-friendly bitmasks).

```
Sparse Graph (E = 3, V = 4):            Dense Graph (E = 6 = V*(V-1)/2, V = 4):
   ( 0 ) --- ( 1 )                         ( 0 ) ===== ( 1 )
     |                                      | \       / |
     |                                      |   \   /   |
     |                                      |     X     |
     |                                      |   /   \   |
   ( 2 )     ( 3 )                         ( 2 ) ===== ( 3 )
Adjacency List: Minimal memory          Adjacency Matrix: Highly compact
```

---

### 2. Physical Memory Layout Comparison

```
Sample Directed Weighted Graph:
  0 ---> 1 (w: 10)
  0 ---> 2 (w: 20)
  1 ---> 2 (w: 30)
  2 ---> 0 (w: 40)

1. Adjacency List Memory (Array of Pointers to Dynamic Lists):
   Index    Pointer    Heap Block (Contiguous List Per Vertex)
   [0] ---> [ (1, 10) | (2, 20) ]
   [1] ---> [ (2, 30) ]
   [2] ---> [ (0, 40) ]
   Cache Locality: Excellent when iterating through neighbors of a single vertex;
                   pointer hopping between different vertices.

2. Adjacency Matrix Memory (Contiguous V x V Flat Buffer):
   Indices:   Col 0   Col 1   Col 2
   Row 0:   [   0   |  10   |  20   ]
   Row 1:   [  INF  |   0   |  30   ]
   Row 2:   [  40   |  INF  |   0   ]
   Cache Locality: Sequential memory scans across rows; massive wasted space if sparse.

3. Edge List Memory (Packed Struct Array):
   [ (0, 1, 10) | (0, 2, 20) | (1, 2, 30) | (2, 0, 40) ]
   Cache Locality: Perfect linear cache streaming for edge sorting (Kruskal's MST, Bellman-Ford).
```

---

## ⚙️ CHAPTER 3: MECHANICS & DUAL-LANGUAGE IMPLEMENTATIONS

Every comprehensive graph data structure must support the canonical operations:
1. `AddVertex()`: Dynamically registers a new node into the graph.
2. `RemoveVertex(v)`: Removes a vertex and purges all incident incoming and outgoing edges.
3. `AddEdge(u, v, weight)`: Inserts a directed (or undirected) edge between `u` and `v`.
4. `RemoveEdge(u, v)`: Deletes the directed (or undirected) edge between `u` and `v`.
5. `HasEdge(u, v)`: Evaluates whether an edge exists from `u` to `v`.
6. `GetNeighbors(u)`: Returns all adjacent outgoing neighbors of `u`.
7. `InDegree(u)`: Returns the total count of incoming edges entering `u`.
8. `OutDegree(u)`: Returns the total count of outgoing edges leaving `u`.

---

### 1. Adjacency List Representation

The industry standard for sparse graphs (`E << V^2`). In competitive programming and LeetCode interviews where `V` is fixed up front, an array of lists `List<int>[]` is the fastest representation. For production applications requiring dynamic vertex additions and removals, a list of lists `List<List<(int To, int Weight)>>` is preferred.

#### C# (.NET 8/9) Complete Implementation

```csharp
using System;
using System.Collections.Generic;

public sealed class AdjacencyListGraph
{
    // Inner list stores (DestinationVertex, EdgeWeight)
    private readonly List<List<(int To, int Weight)>> _adj;

    public int VertexCount => _adj.Count;

    public AdjacencyListGraph(int initialVertices = 0)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(initialVertices);
        _adj = new List<List<(int To, int Weight)>>(initialVertices);
        for (int i = 0; i < initialVertices; i++)
        {
            _adj.Add(new List<(int To, int Weight)>());
        }
    }

    // 1. Add Vertex: O(1) amortized
    public int AddVertex()
    {
        _adj.Add(new List<(int To, int Weight)>());
        return _adj.Count - 1;
    }

    // 2. Remove Vertex: O(V + E) - purges edges and re-indexes indices > v
    public void RemoveVertex(int v)
    {
        ValidateVertex(v);
        // Remove vertex v's own outgoing edge list
        _adj.RemoveAt(v);

        // Scrub all incoming references to v, and shift vertex references > v down by 1
        for (int i = 0; i < _adj.Count; i++)
        {
            var edges = _adj[i];
            for (int j = edges.Count - 1; j >= 0; j--)
            {
                if (edges[j].To == v)
                {
                    edges.RemoveAt(j);
                }
                else if (edges[j].To > v)
                {
                    edges[j] = (edges[j].To - 1, edges[j].Weight);
                }
            }
        }
    }

    // 3. Add Edge: O(1)
    public void AddEdge(int from, int to, int weight = 1, bool bidirectional = false)
    {
        ValidateVertex(from);
        ValidateVertex(to);
        _adj[from].Add((to, weight));
        if (bidirectional && from != to)
        {
            _adj[to].Add((from, weight));
        }
    }

    // 4. Remove Edge: O(deg(from))
    public void RemoveEdge(int from, int to, bool bidirectional = false)
    {
        ValidateVertex(from);
        ValidateVertex(to);
        _adj[from].RemoveAll(e => e.To == to);
        if (bidirectional && from != to)
        {
            _adj[to].RemoveAll(e => e.To == from);
        }
    }

    // 5. Has Edge: O(deg(from))
    public bool HasEdge(int from, int to)
    {
        ValidateVertex(from);
        ValidateVertex(to);
        foreach (var edge in _adj[from])
        {
            if (edge.To == to) return true;
        }
        return false;
    }

    // 6. Get Neighbors: O(1) reference access, O(deg(u)) iteration
    public IReadOnlyList<(int To, int Weight)> GetNeighbors(int vertex)
    {
        ValidateVertex(vertex);
        return _adj[vertex];
    }

    // 7. In-Degree: O(V + E) scan across all adjacency buckets
    public int InDegree(int vertex)
    {
        ValidateVertex(vertex);
        int count = 0;
        for (int i = 0; i < _adj.Count; i++)
        {
            foreach (var edge in _adj[i])
            {
                if (edge.To == vertex) count++;
            }
        }
        return count;
    }

    // 8. Out-Degree: O(1)
    public int OutDegree(int vertex)
    {
        ValidateVertex(vertex);
        return _adj[vertex].Count;
    }

    private void ValidateVertex(int v)
    {
        if (v < 0 || v >= _adj.Count)
            throw new ArgumentOutOfRangeException(nameof(v), $"Vertex {v} is out of bounds [0, {_adj.Count - 1}].");
    }
}
```

> [!TIP]
> **Static Contest Shortcut:** When `V` is fixed at problem start, use `List<int>[] adj = new List<int>[V];` where each bucket is instantiated. This avoids resizing allocations and maximizes raw memory throughput.

#### Python (3.11+) Complete Implementation

```python
from typing import List, Tuple

class AdjacencyListGraph:
    """Production dynamic adjacency list supporting all 8 core graph operations."""
    __slots__ = ("adj",)

    def __init__(self, initial_vertices: int = 0) -> None:
        if initial_vertices < 0:
            raise ValueError("Initial vertex count must be non-negative.")
        self.adj: List[List[Tuple[int, int]]] = [[] for _ in range(initial_vertices)]

    @property
    def vertex_count(self) -> int:
        return len(self.adj)

    # 1. Add Vertex: O(1) amortized
    def add_vertex(self) -> int:
        self.adj.append([])
        return len(self.adj) - 1

    # 2. Remove Vertex: O(V + E)
    def remove_vertex(self, v: int) -> None:
        self._validate_vertex(v)
        del self.adj[v]
        for i in range(len(self.adj)):
            new_edges = []
            for dest, weight in self.adj[i]:
                if dest == v:
                    continue
                elif dest > v:
                    new_edges.append((dest - 1, weight))
                else:
                    new_edges.append((dest, weight))
            self.adj[i] = new_edges

    # 3. Add Edge: O(1)
    def add_edge(self, u: int, v: int, weight: int = 1, bidirectional: bool = False) -> None:
        self._validate_vertex(u)
        self._validate_vertex(v)
        self.adj[u].append((v, weight))
        if bidirectional and u != v:
            self.adj[v].append((u, weight))

    # 4. Remove Edge: O(deg(u))
    def remove_edge(self, u: int, v: int, bidirectional: bool = False) -> None:
        self._validate_vertex(u)
        self._validate_vertex(v)
        self.adj[u] = [e for e in self.adj[u] if e[0] != v]
        if bidirectional and u != v:
            self.adj[v] = [e for e in self.adj[v] if e[0] != u]

    # 5. Has Edge: O(deg(u))
    def has_edge(self, u: int, v: int) -> bool:
        self._validate_vertex(u)
        self._validate_vertex(v)
        return any(dest == v for dest, _ in self.adj[u])

    # 6. Get Neighbors: O(1) reference access
    def get_neighbors(self, u: int) -> List[Tuple[int, int]]:
        self._validate_vertex(u)
        return self.adj[u]

    # 7. In-Degree: O(V + E)
    def in_degree(self, u: int) -> int:
        self._validate_vertex(u)
        return sum(1 for edges in self.adj for dest, _ in edges if dest == u)

    # 8. Out-Degree: O(1)
    def out_degree(self, u: int) -> int:
        self._validate_vertex(u)
        return len(self.adj[u])

    def _validate_vertex(self, v: int) -> None:
        if not (0 <= v < len(self.adj)):
            raise IndexError(f"Vertex {v} is out of bounds [0, {len(self.adj) - 1}].")
```

---

### 2. Adjacency Matrix Representation

Optimal for dense graphs (`E ≈ V^2`) or scenarios requiring constant-time `O(1)` edge existence checks, additions, and weight updates.

#### C# (.NET 8/9) Complete Implementation

```csharp
using System;
using System.Collections.Generic;

public sealed class AdjacencyMatrixGraph
{
    private readonly List<List<int>> _matrix = new();
    public const int NoEdge = int.MaxValue;

    public int VertexCount => _matrix.Count;

    public AdjacencyMatrixGraph(int initialVertices = 0)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(initialVertices);
        for (int i = 0; i < initialVertices; i++)
        {
            AddVertex();
        }
    }

    // 1. Add Vertex: O(V) row and column expansions
    public int AddVertex()
    {
        int newIdx = _matrix.Count;
        for (int i = 0; i < newIdx; i++)
        {
            _matrix[i].Add(NoEdge);
        }
        var newRow = new List<int>(newIdx + 1);
        for (int j = 0; j < newIdx; j++)
        {
            newRow.Add(NoEdge);
        }
        newRow.Add(0); // Self-distance is 0
        _matrix.Add(newRow);
        return newIdx;
    }

    // 2. Remove Vertex: O(V) list purges
    public void RemoveVertex(int v)
    {
        ValidateVertex(v);
        _matrix.RemoveAt(v);
        for (int i = 0; i < _matrix.Count; i++)
        {
            _matrix[i].RemoveAt(v);
        }
    }

    // 3. Add Edge: O(1)
    public void AddEdge(int from, int to, int weight = 1, bool bidirectional = false)
    {
        ValidateVertex(from);
        ValidateVertex(to);
        _matrix[from][to] = weight;
        if (bidirectional)
        {
            _matrix[to][from] = weight;
        }
    }

    // 4. Remove Edge: O(1)
    public void RemoveEdge(int from, int to, bool bidirectional = false)
    {
        ValidateVertex(from);
        ValidateVertex(to);
        _matrix[from][to] = (from == to) ? 0 : NoEdge;
        if (bidirectional)
        {
            _matrix[to][from] = (from == to) ? 0 : NoEdge;
        }
    }

    // 5. Has Edge: O(1)
    public bool HasEdge(int from, int to)
    {
        ValidateVertex(from);
        ValidateVertex(to);
        return _matrix[from][to] != NoEdge && (from != to || _matrix[from][to] > 0);
    }

    // 6. Get Neighbors: O(V) full row scan
    public List<(int To, int Weight)> GetNeighbors(int vertex)
    {
        ValidateVertex(vertex);
        var neighbors = new List<(int To, int Weight)>();
        for (int c = 0; c < _matrix.Count; c++)
        {
            if (c != vertex && _matrix[vertex][c] != NoEdge)
            {
                neighbors.Add((c, _matrix[vertex][c]));
            }
        }
        return neighbors;
    }

    // 7. In-Degree: O(V) full column scan
    public int InDegree(int vertex)
    {
        ValidateVertex(vertex);
        int deg = 0;
        for (int r = 0; r < _matrix.Count; r++)
        {
            if (r != vertex && _matrix[r][vertex] != NoEdge) deg++;
        }
        return deg;
    }

    // 8. Out-Degree: O(V) full row scan
    public int OutDegree(int vertex)
    {
        ValidateVertex(vertex);
        int deg = 0;
        for (int c = 0; c < _matrix.Count; c++)
        {
            if (c != vertex && _matrix[vertex][c] != NoEdge) deg++;
        }
        return deg;
    }

    private void ValidateVertex(int v)
    {
        if (v < 0 || v >= _matrix.Count)
            throw new ArgumentOutOfRangeException(nameof(v), $"Vertex {v} is out of bounds [0, {_matrix.Count - 1}].");
    }
}
```

#### Python (3.11+) Complete Implementation

```python
import math
from typing import List, Tuple

class AdjacencyMatrixGraph:
    """Fixed or dynamic 2D matrix optimized for dense topologies and O(1) edge checks."""
    __slots__ = ("matrix",)
    NO_EDGE: float = math.inf

    def __init__(self, initial_vertices: int = 0) -> None:
        if initial_vertices < 0:
            raise ValueError("Vertex count must be non-negative.")
        self.matrix: List[List[float]] = []
        for _ in range(initial_vertices):
            self.add_vertex()

    @property
    def vertex_count(self) -> int:
        return len(self.matrix)

    # 1. Add Vertex: O(V)
    def add_vertex(self) -> int:
        n = len(self.matrix)
        for row in self.matrix:
            row.append(self.NO_EDGE)
        new_row = [self.NO_EDGE] * n + [0.0]
        self.matrix.append(new_row)
        return n

    # 2. Remove Vertex: O(V)
    def remove_vertex(self, v: int) -> None:
        self._validate_vertex(v)
        del self.matrix[v]
        for row in self.matrix:
            del row[v]

    # 3. Add Edge: O(1)
    def add_edge(self, u: int, v: int, weight: float = 1.0, bidirectional: bool = False) -> None:
        self._validate_vertex(u)
        self._validate_vertex(v)
        self.matrix[u][v] = weight
        if bidirectional:
            self.matrix[v][u] = weight

    # 4. Remove Edge: O(1)
    def remove_edge(self, u: int, v: int, bidirectional: bool = False) -> None:
        self._validate_vertex(u)
        self._validate_vertex(v)
        self.matrix[u][v] = 0.0 if u == v else self.NO_EDGE
        if bidirectional:
            self.matrix[v][u] = 0.0 if u == v else self.NO_EDGE

    # 5. Has Edge: O(1)
    def has_edge(self, u: int, v: int) -> bool:
        self._validate_vertex(u)
        self._validate_vertex(v)
        return self.matrix[u][v] != self.NO_EDGE and (u != v or self.matrix[u][v] > 0)

    # 6. Get Neighbors: O(V) row scan
    def get_neighbors(self, u: int) -> List[Tuple[int, float]]:
        self._validate_vertex(u)
        return [
            (c, self.matrix[u][c])
            for c in range(len(self.matrix))
            if c != u and self.matrix[u][c] != self.NO_EDGE
        ]

    # 7. In-Degree: O(V) column scan
    def in_degree(self, u: int) -> int:
        self._validate_vertex(u)
        return sum(1 for r in range(len(self.matrix)) if r != u and self.matrix[r][u] != self.NO_EDGE)

    # 8. Out-Degree: O(V) row scan
    def out_degree(self, u: int) -> int:
        self._validate_vertex(u)
        return sum(1 for c in range(len(self.matrix)) if c != u and self.matrix[u][c] != self.NO_EDGE)

    def _validate_vertex(self, v: int) -> None:
        if not (0 <= v < len(self.matrix)):
            raise IndexError(f"Vertex {v} is out of bounds [0, {len(self.matrix) - 1}].")
```

---

### 3. Edge List Representation

Packed array of edge records. Ideal for edge-centric algorithms: sorting edges by weight in Kruskal's algorithm, relaxation passes in Bellman-Ford, or initial edge input parsing.

#### C# (.NET 8/9) Complete Implementation

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

public readonly record struct Edge(int From, int To, int Weight);

public sealed class EdgeListGraph
{
    private readonly List<Edge> _edges = new();
    public int VertexCount { get; private set; }

    public IReadOnlyList<Edge> Edges => _edges;

    public EdgeListGraph(int initialVertices = 0)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(initialVertices);
        VertexCount = initialVertices;
    }

    // 1. Add Vertex: O(1)
    public int AddVertex() => VertexCount++;

    // 2. Remove Vertex: O(E) filter and index shift
    public void RemoveVertex(int v)
    {
        ValidateVertex(v);
        _edges.RemoveAll(e => e.From == v || e.To == v);
        for (int i = 0; i < _edges.Count; i++)
        {
            var e = _edges[i];
            int newFrom = e.From > v ? e.From - 1 : e.From;
            int newTo = e.To > v ? e.To - 1 : e.To;
            if (newFrom != e.From || newTo != e.To)
            {
                _edges[i] = new Edge(newFrom, newTo, e.Weight);
            }
        }
        VertexCount--;
    }

    // 3. Add Edge: O(1)
    public void AddEdge(int from, int to, int weight = 1, bool bidirectional = false)
    {
        ValidateVertex(from);
        ValidateVertex(to);
        _edges.Add(new Edge(from, to, weight));
        if (bidirectional && from != to)
        {
            _edges.Add(new Edge(to, from, weight));
        }
    }

    // 4. Remove Edge: O(E) linear search
    public void RemoveEdge(int from, int to, bool bidirectional = false)
    {
        ValidateVertex(from);
        ValidateVertex(to);
        _edges.RemoveAll(e => e.From == from && e.To == to);
        if (bidirectional && from != to)
        {
            _edges.RemoveAll(e => e.From == to && e.To == from);
        }
    }

    // 5. Has Edge: O(E) linear search
    public bool HasEdge(int from, int to)
    {
        ValidateVertex(from);
        ValidateVertex(to);
        return _edges.Any(e => e.From == from && e.To == to);
    }

    // 6. Get Neighbors: O(E) linear scan
    public List<(int To, int Weight)> GetNeighbors(int vertex)
    {
        ValidateVertex(vertex);
        var neighbors = new List<(int To, int Weight)>();
        foreach (var e in _edges)
        {
            if (e.From == vertex)
            {
                neighbors.Add((e.To, e.Weight));
            }
        }
        return neighbors;
    }

    // 7. In-Degree: O(E) linear scan
    public int InDegree(int vertex)
    {
        ValidateVertex(vertex);
        return _edges.Count(e => e.To == vertex);
    }

    // 8. Out-Degree: O(E) linear scan
    public int OutDegree(int vertex)
    {
        ValidateVertex(vertex);
        return _edges.Count(e => e.From == vertex);
    }

    private void ValidateVertex(int v)
    {
        if (v < 0 || v >= VertexCount)
            throw new ArgumentOutOfRangeException(nameof(v), $"Vertex {v} is out of bounds [0, {VertexCount - 1}].");
    }
}
```

#### Python (3.11+) Complete Implementation

```python
from typing import NamedTuple, List, Tuple

class Edge(NamedTuple):
    u: int
    v: int
    weight: int = 1

class EdgeListGraph:
    """Packed array of edge records optimal for Kruskal's MST and Bellman-Ford."""
    __slots__ = ("vertex_count", "edges")

    def __init__(self, initial_vertices: int = 0) -> None:
        if initial_vertices < 0:
            raise ValueError("Vertex count must be non-negative.")
        self.vertex_count: int = initial_vertices
        self.edges: List[Edge] = []

    # 1. Add Vertex: O(1)
    def add_vertex(self) -> int:
        self.vertex_count += 1
        return self.vertex_count - 1

    # 2. Remove Vertex: O(E)
    def remove_vertex(self, v: int) -> None:
        self._validate_vertex(v)
        self.edges = [e for e in self.edges if e.u != v and e.v != v]
        new_edges = []
        for e in self.edges:
            nu = e.u - 1 if e.u > v else e.u
            nv = e.v - 1 if e.v > v else e.v
            new_edges.append(Edge(nu, nv, e.weight))
        self.edges = new_edges
        self.vertex_count -= 1

    # 3. Add Edge: O(1)
    def add_edge(self, u: int, v: int, weight: int = 1, bidirectional: bool = False) -> None:
        self._validate_vertex(u)
        self._validate_vertex(v)
        self.edges.append(Edge(u, v, weight))
        if bidirectional and u != v:
            self.edges.append(Edge(v, u, weight))

    # 4. Remove Edge: O(E)
    def remove_edge(self, u: int, v: int, bidirectional: bool = False) -> None:
        self._validate_vertex(u)
        self._validate_vertex(v)
        self.edges = [e for e in self.edges if not (e.u == u and e.v == v)]
        if bidirectional and u != v:
            self.edges = [e for e in self.edges if not (e.u == v and e.v == u)]

    # 5. Has Edge: O(E)
    def has_edge(self, u: int, v: int) -> bool:
        self._validate_vertex(u)
        self._validate_vertex(v)
        return any(e.u == u and e.v == v for e in self.edges)

    # 6. Get Neighbors: O(E)
    def get_neighbors(self, u: int) -> List[Tuple[int, int]]:
        self._validate_vertex(u)
        return [(e.v, e.weight) for e in self.edges if e.u == u]

    # 7. In-Degree: O(E)
    def in_degree(self, u: int) -> int:
        self._validate_vertex(u)
        return sum(1 for e in self.edges if e.v == u)

    # 8. Out-Degree: O(E)
    def out_degree(self, u: int) -> int:
        self._validate_vertex(u)
        return sum(1 for e in self.edges if e.u == u)

    def _validate_vertex(self, v: int) -> None:
        if not (0 <= v < self.vertex_count):
            raise IndexError(f"Vertex {v} is out of bounds [0, {self.vertex_count - 1}].")
```

---

### 4. Implicit Grid Graph Representation

For 2D matrices, mazes, and grid games, never allocate an explicit graph. Compute valid adjacent neighbor transitions on-the-fly using directional delta vectors.

#### Dual-Language Implementation

```csharp
public static class GridNavigation
{
    // 4 Cardinal directions: Up, Down, Left, Right
    private static readonly int[] RowDelta4 = [-1, 1, 0, 0];
    private static readonly int[] ColDelta4 = [0, 0, -1, 1];

    // 8 Cardinal + Diagonal directions
    private static readonly int[] RowDelta8 = [-1, -1, -1,  0, 0,  1, 1, 1];
    private static readonly int[] ColDelta8 = [-1,  0,  1, -1, 1, -1, 0, 1];

    public static IEnumerable<(int R, int C)> GetValidNeighbors(int r, int c, int rows, int cols, bool allowDiagonals = false)
    {
        int[] dr = allowDiagonals ? RowDelta8 : RowDelta4;
        int[] dc = allowDiagonals ? ColDelta8 : ColDelta4;
        int count = dr.Length;

        for (int i = 0; i < count; i++)
        {
            int nr = r + dr[i];
            int nc = c + dc[i];
            if (nr >= 0 && nr < rows && nc >= 0 && nc < cols)
            {
                yield return (nr, nc);
            }
        }
    }
}
```

```python
from typing import Iterator, Tuple

# Cardinal delta offsets: (dr, dc)
DIRECTIONS_4 = ((-1, 0), (1, 0), (0, -1), (0, 1))
DIRECTIONS_8 = ((-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1))

def get_valid_neighbors(
    r: int, c: int, rows: int, cols: int, allow_diagonals: bool = False
) -> Iterator[Tuple[int, int]]:
    """Generates in-bounds cell coordinates without allocating an explicit graph."""
    deltas = DIRECTIONS_8 if allow_diagonals else DIRECTIONS_4
    for dr, dc in deltas:
        nr, nc = r + dr, c + dc
        if 0 <= nr < rows and 0 <= nc < cols:
            yield nr, nc
```

---

## 📊 CHAPTER 4: COMPLEXITY DECONSTRUCTION

### Side-by-Side Operations Comparison Table

| Operation | Adjacency List (`List<T>[]`) | Adjacency Matrix (`int[,]`) | Edge List (`List<Edge>`) | Implicit Grid (`R * C`) |
| :--- | :--- | :--- | :--- | :--- |
| **`AddVertex()`** | `O(1)` amortized | `O(V)` dynamic / `O(V^2)` realloc | `O(1)` | `N/A` (Fixed grid geometry) |
| **`RemoveVertex(v)`** | `O(V + E)` (edge scrub & shift) | `O(V)` dynamic / `O(V^2)` realloc | `O(E)` (filter & shift) | `N/A` (Cell state flip) |
| **`AddEdge(u, v)`** | `O(1)` | `O(1)` | `O(1)` | `N/A` (Implicit rule) |
| **`RemoveEdge(u, v)`** | `O(deg(u))` | `O(1)` | `O(E)` | `N/A` (Cell state flip) |
| **`HasEdge(u, v)`** | `O(deg(u))` | `O(1)` | `O(E)` | `O(1)` (Coordinate delta check) |
| **`GetNeighbors(u)`** | `O(deg(u))` | `O(V)` | `O(E)` | `O(1)` (At most 4 or 8) |
| **`InDegree(u)`** | `O(V + E)` (or `O(1)` if cached) | `O(V)` | `O(E)` | `O(1)` (At most 4 or 8) |
| **`OutDegree(u)`** | `O(1)` | `O(V)` | `O(E)` | `O(1)` (At most 4 or 8) |
| **Auxiliary Space** | `O(V + E)` | `O(V^2)` | `O(E)` | `O(1)` (Zero graph storage) |
| **Output Space** | `O(deg(u))` | `O(V)` | `O(E)` | `O(1)` (Yielded tuples) |
| **Primary Use Case** | Sparse graphs (`E << V^2`), BFS/DFS | Dense graphs (`E ≈ V^2`), `O(1)` edge checks | Kruskal's MST, Bellman-Ford | Mazes, image segmentation, 2D boards |

---

### Memory & Cache Deconstruction

- **Dense Matrix Penalty:** For `V = 10^5` with `E = 2 * 10^5` (typical sparse problem):
  - **Adjacency Matrix:** Requires `(10^5)^2 * 4 bytes = 40,000,000,000 bytes ≈ 40 GB`. This triggers an instant **Memory Limit Exceeded (MLE)** crash.
  - **Adjacency List:** Requires `(10^5 pointers) + (2 * 10^5 nodes) * 8 bytes ≈ 2.4 MB`. It fits easily inside L3 CPU cache.
- **CPU Cache Line Behavior:**
  - Scanning `matrix[u, *]` performs contiguous hardware cache prefetching across 64-byte L1 cache lines.
  - Scanning an Adjacency List touches contiguous memory within a single node's list, but hops heap references when navigating from node `u` to node `v`. For sparse traversals, this trade-off is well worth the astronomical memory savings.

---

## 🎙️ CHAPTER 5: 45-MINUTE INTERVIEW VERBAL SCRIPT

### Phase 1: Clarification & Constraint Scoping (0–5 Mins)
- **Candidate:** "Before choosing our graph data structure, I want to clarify four key constraints:
  1. What is the maximum vertex count `V` and edge count `E`?
  2. Are vertex identifiers contiguous `0` to `V - 1`, or arbitrary strings/IDs?
  3. Is the graph directed or undirected, and are edges weighted?
  4. Are self-loops or parallel edges possible?"
- **Interviewer:** "`V <= 10^5`, `E <= 2 * 10^5`, 0-indexed integers, directed, unweighted, no parallel edges."
- **Candidate:** "With `V = 10^5`, an adjacency matrix would require `(10^5)^2 = 10^10` cells, which would consume over `40 GB` of memory and cause an immediate Out-Of-Memory error. The graph is sparse since `E` is proportional to `V`. An **adjacency list** is strictly required."

### Phase 2: High-Level Approach & Trade-Offs (5–12 Mins)
- **Candidate:** "I will represent the graph as an array of integer lists `List<int>[]` of size `V`.
  - Building the graph takes `O(V + E)` time.
  - Traversing all outgoing neighbors of vertex `u` takes `O(deg(u))` time.
  - Total auxiliary space is strictly bounded by `O(V + E)`.
  If vertex IDs were sparse strings or GUIDs, I would map strings to `[0, V-1]` integers via a hash map up front rather than keying the adjacency list directly with strings, avoiding expensive string hashing during traversals."

### Phase 3: Live Coding Walkthrough (12–32 Mins)
- **Candidate:** "I'll write the class with clean input validation first. In C#, I initialize `_adj = new List<int>[v]` and instantiate each bucket with an empty list. When processing a directed edge `[u, v]`, I append `v` to `_adj[u]`. If this were an undirected graph, I would append in both directions: `_adj[u].Add(v)` and `_adj[v].Add(u)`."

### Phase 4: Edge Cases & Verification (32–40 Mins)
- **Candidate:** "Let's verify edge cases:
  1. **Disconnected vertices with degree `0`:** The bucket exists and is an empty list; traversals handle this without null references.
  2. **Dense sub-clusters:** Dynamic list expansion handles variable degrees smoothly.
  3. **Single node `V = 1, E = 0`:** Handled cleanly without exceptions.
  4. **Self-loops (`u -> u`):** If present, `u` is added to `_adj[u]`. Our traversal algorithms will rely on a `visited` set to avoid infinite self-loop recursions."

---

## 🛠️ CHAPTER 6: COMMON PITFALLS & DECISION FRAMEWORK

### Deep Dive: Beginner Pitfalls & Traps

#### 1. The Disconnected Graph Trap
- **The Bug:** Assuming the graph is a single connected component. Writing BFS or DFS that only starts from vertex `0`.
- **The Symptom:** Test cases pass when all vertices are reachable from `0`, but silently fail when isolated nodes or disconnected islands exist.
- **The Fix:** Always wrap the traversal in an outer loop over all vertices `0` to `V - 1`:
  ```csharp
  var visited = new bool[V];
  for (int i = 0; i < V; i++)
  {
      if (!visited[i])
      {
          TraverseComponent(i, visited);
      }
  }
  ```

#### 2. Self-Loops (`u -> u`)
- **The Bug:** An edge points from a node back to itself.
- **The Consequences:**
  - Undirected degree calculations: a self-loop adds `+2` to `deg(u)`.
  - Traversals: if visited checking is missing or deferred, a self-loop causes infinite recursion or an endless queue loop.
  - Cycle detection: a self-loop is a directed cycle of length 1!

#### 3. Parallel Edges (Multi-edges)
- **The Bug:** Multiple edges exist between the exact same pair of nodes `u` and `v`.
- **The Consequences:**
  - In an Adjacency Matrix, naively setting `matrix[u, v] = weight` overwrites previous edges (must take `Math.Min(matrix[u, v], weight)` for shortest-path problems).
  - In an Adjacency List, duplicate entries inflate list sizes and can double-count neighbors during traversals.

#### 4. Cycle Traps & Missing Visited Tracking
- **The Bug:** In trees, cycles do not exist, so a simple `parent` check suffices to prevent backtracking. Beginners frequently carry this assumption over to general graphs and omit the `visited` set.
- **The Fix:** In general graphs, cross-edges and back-edges create cycles. A `visited` tracker (boolean array or hash set) is **mandatory** for all graph traversals.

#### 5. 0-Indexed vs. 1-Indexed Node Numbering
- **The Bug:** Problems on LeetCode/Codeforces often label vertices `1` to `N`. Attempting to access `adj[u]` directly triggers `IndexOutOfRangeException` for `u = N` or wastes slot `0`.
- **The Fix:** Either decrement all node inputs by 1 (`u--; v--;`) upon ingestion, or allocate `adj` with size `N + 1`.

#### 6. Matrix Allocation OOM for Large `V`
- **The Bug:** Blindly writing `int[,] matrix = new int[n, n]` because matrix code is simpler to write.
- **The Consequence:** For `N = 10^5`, this requires `40 GB` RAM and crashes instantly with `OutOfMemoryException`.

---

### Graph Representation Decision Tree

```
                           [ Graph Representation ]
                                       |
                 +---------------------+---------------------+
                 |                                           |
         [ Grid / Maze / Board ]                     [ Explicit Network ]
                 |                                           |
         Implicit Graph:                             Graph Density?
         Boundary Delta Offsets                              |
         Space: O(1) auxiliary               +---------------+---------------+
                                             |                               |
                                       Sparse (E << V^2)              Dense (E ≈ V^2)
                                             |                               |
                                      Adjacency List                  Adjacency Matrix
                                     Space: O(V + E)                  Space: O(V^2)
                                    Neighbor: O(deg(u))               Edge Test: O(1)
```

---

## 🏋️ PRACTICE LADDER

| Problem | LeetCode # | Difficulty | Representation Pattern | Key Invariant |
| :--- | :--- | :--- | :--- | :--- |
| **Find Center of Star Graph** | #1791 | 🟢 Easy | Edge List Inspection | Common node across first two edges |
| **Find if Path Exists in Graph** | #1971 | 🟢 Easy | Adjacency List Build | Undirected connectivity check |
| **Clone Graph** | #133 | 🟡 Medium | Adjacency List + HashMap | Deep clone with node identity map |
| **Number of Islands** | #200 | 🟡 Medium | Implicit Grid Graph | In-place cell mutation / visited set |
| **Reorder Routes to Lead to Zero** | #1466 | 🟡 Medium | Directed Adjacency List | Track original edge direction vs reverse |
| **Minimum Degree of Connected Trio** | #1761 | 🔴 Hard | Hybrid Matrix + List | Fast `O(1)` edge checks via matrix |

---

> 🧭 **Navigation:** [← Week Overview](README.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_08_Day_02_Breadth_First_Search_Instructional.md)
