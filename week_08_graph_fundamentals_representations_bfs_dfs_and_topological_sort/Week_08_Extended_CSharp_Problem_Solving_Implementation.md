# Week 08 Extended CSharp Problem Solving Implementation

Version v1.0  
Filename: `Week_08_Extended_CSharp_Problem_Solving_Implementation.md`  
Hybrid Focus: **Pattern Recognition + Production-Grade C# Implementations**  
Scope: Week 8 – Graph Fundamentals: Representations, BFS, DFS & Topological Sort  
Prerequisites: Week 8 instructional files + visual playbook completed

---

## WEEK 08 OVERVIEW

**Primary Goal (from syllabus):**  
Build strong intuition for graph models and basic traversals (BFS & DFS), including topological sort and core connectivity structures.

**Topics by Day:**
- **Day 1:** Graph Models & Representations (adjacency matrix/list/edge list, implicit graphs).  
- **Day 2:** Breadth-First Search (BFS) – mechanics, shortest paths in unweighted graphs, applications.  
- **Day 3:** Depth-First Search (DFS) & Topological Sort – recursion/stack, edge types, cycle detection, ordering.  
- **Day 4:** Connectivity & Bipartite Graphs – components, bipartite testing, Union–Find, network connectivity.  
- **Day 5 (Optional Advanced):** Strongly Connected Components (SCC) – Kosaraju/Tarjan, condensation DAG.

Main implementation patterns for Week 08:
1. **Graph Representation Pattern** – adjacency list/matrix + implicit graphs.  
2. **Unweighted Shortest Path Pattern** – BFS with distance + parent reconstruction.  
3. **DFS & Topological Sort Pattern** – DFS ordering, Kahn’s algorithm, cycle detection.  
4. **Connectivity & Bipartite Pattern** – multi-source BFS/DFS + 2-coloring + Union–Find.  
5. **SCC Pattern (Advanced)** – Kosaraju-style two-pass DFS.

---

## SECTION 1 – PATTERN RECOGNITION FRAMEWORK 🧭

**Decision Tree – How to Identify & Choose Week 08 Graph Patterns**

```text
Start
 ├─ Is the data naturally a network / relationships between entities?
 │    └─ YES → Model as a graph.
 │
 ├─ Are edges all “equal cost” (1 hop each)?
 │    └─ YES → Unweighted graph → BFS patterns.
 │
 ├─ Is there a notion of dependency or direction (A must come before B)?
 │    └─ YES → Directed graph → DFS + Topological Sort patterns.
 │
 ├─ Do you need to know groups / islands / components?
 │    └─ YES → Connectivity patterns (BFS/DFS per component, or Union–Find).
 │
 ├─ Do you need to check 2-colorability / odd cycle existence?
 │    └─ YES → Bipartite pattern (BFS/DFS 2-coloring).
 │
 └─ Do you care about mutual reachability (u⇄v strongly connected)?
      └─ YES → SCC patterns (Kosaraju/Tarjan).
```

### 1.1 Problem Signals → Pattern Mapping

| Problem Phrases / Signals                                                                 | Pattern Name                               | Why This Pattern?                                                                                          | C# Collection Choice                          | Time / Space (Typical)           |
| ----------------------------------------------------------------------------------------- | ------------------------------------------ | ----------------------------------------------------------------------------------------------------------- | --------------------------------------------- | -------------------------------- |
| “Users, roads, flights, computers and connections” – natural network modelling            | **Graph Representation Pattern**           | You must first choose an efficient graph model (list/matrix/edge list) before any algorithm can run well.  | `List<int>[]`, `List<(int to,int w)>[]`       | Build: O(V + E); Space: O(V + E) |
| “Find minimum number of steps / edges”, “each move costs 1”, “shortest path in grid/maze” | **Unweighted Shortest Path (BFS)**         | BFS explores in layers by distance; first time you visit a node is via a shortest path in edge count.      | `Queue<int>`, `List<int>[]`, `bool[]`         | O(V + E) / O(V + E)             |
| “Check if all tasks can be finished given prerequisites”, “build order”, “dependencies”   | **DFS & Topological Sort Pattern**         | Dependencies form a DAG (if valid). Topological sort orders nodes so edges go forward in time/order.       | `List<int>[]`, `Stack<int>`, `Queue<int>`     | O(V + E) / O(V + E)             |
| “How many connected groups?”, “is graph bipartite?”, “friends in same cluster?”          | **Connectivity & Bipartite Pattern**       | BFS/DFS per component labels islands; 2-coloring detects if nodes can be split into two non-conflicting sets. | `List<int>[]`, `Queue<int>`, `int[] color` | O(V + E) / O(V + E)             |
| “Mutually reachable strongly connected subsystems”, “collapse cycles into meta-nodes”    | **SCC Pattern (Kosaraju)**                 | SCCs cluster vertices with mutual reachability; collapsing SCCs produces a DAG suitable for further DP.    | `List<int>[]` + transpose `List<int>[]`       | O(V + E) / O(V + E)             |

**Read this first:**  
When you see **“shortest number of steps”** with **equal edge cost**, think **BFS**.  
When you see **“ordering with dependencies”**, think **topological sort**.  
When you see **“groups / components / islands”**, think **connectivity patterns or DSU**.

---

## SECTION 2 – ANTI-PATTERNS: WHEN NOT TO USE ❌

### 2.1 Common Week 08 Mistakes (What Fails & Correct Alternatives)

| Wrong Approach                                                                 | Why It Fails                                                                                           | Symptom / Consequence                                                   | Correct Alternative Pattern                         |
| ----------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------ | ------------------------------------------------------------------------ | --------------------------------------------------- |
| Using **DFS** to compute shortest paths in an unweighted graph                | DFS explores depth-first, not by layers; path found is often not the minimum edge count.               | Returns non-minimal path; inconsistent results across traversals.       | Use **BFS** with distance array + parent pointers.  |
| Using **BFS** for weighted shortest paths (edges with non-equal costs)       | BFS assumes equal cost per edge; ignores weights entirely.                                             | “Shortest” path in hops has larger actual cost than another path.       | Use Dijkstra / Bellman–Ford (Week 9).              |
| Building an **adjacency matrix for huge sparse graphs** (e.g., 10^5 nodes)   | O(V²) memory blows up; most entries are unused; cache-unfriendly.                                      | OutOfMemoryException or severe slowdown.                                | Use adjacency lists (`List<int>[]` or similar).     |
| Treating **directed dependency graphs as undirected**                        | Direction encodes causality; ignoring it breaks cycle detection & ordering.                           | Topological sort “succeeds” but build/runtime fails due to hidden cycles. | Use **directed graphs** and proper DFS/Kahn’s.      |
| Checking bipartiteness with **coloring but ignoring components**             | Only coloring the component containing the start node; others remain uncolored.                       | Some nodes never processed; bipartite result is incorrect for full graph. | BFS/DFS from each unvisited node; color globally.  |
| Implementing SCC by naive BFS/DFS from each vertex (O(V·(V+E)))              | Repeated full traversals from every node; too slow for large graphs.                                  | TLE on dense graphs with thousands of vertices.                          | Use **Kosaraju** or **Tarjan** SCC algorithms.      |

**Anti-pattern lesson:** Always map the **problem wording** to the right pattern before writing code. Mis-mapping leads to “correct-looking” code that fails on edge cases or scale.

---

## SECTION 3 – PATTERN IMPLEMENTATIONS (Production-Grade C#) 💻

> Coding standards: guard clauses first, meaningful names, mental model comments, time/space docs, and C#-specific notes.

### 3.1 Pattern 1 – Graph Representation (Adjacency Lists + Implicit Graphs)

**Mental Model:**  
Think of the graph as a set of **cities** (vertices) with **roads** (edges). An adjacency list is a **list of neighbors per city**; very compact if not every city is connected to every other. Grids are like **city blocks** where neighbors are computed from coordinates instead of stored explicitly.

**When to Use:**
- Sparse graphs where `E ≪ V²` (social networks, web links, road maps).  
- Graph algorithms that iterate neighbors frequently (BFS/DFS, Dijkstra, SCC).  
- Grid-based problems (mazes, shortest path in a matrix) via **implicit neighbor generation**.

**Core C# Implementation – Adjacency List for Unweighted Graph**

```csharp
using System;
using System.Collections.Generic;

public class Graph
{
    // Adjacency list representation: for each vertex, store its neighbors.
    private readonly List<int>[] _adjacency;

    /// <summary>
    /// MENTAL MODEL:
    /// Treat this as an array of neighbor lists. For vertex v, _adjacency[v]
    /// contains all vertices reachable by a single edge from v.
    /// Time: O(V + E) to build from edge list.
    /// Space: O(V + E).
    /// </summary>
    public Graph(int vertexCount)
    {
        if (vertexCount <= 0)
            throw new ArgumentOutOfRangeException(nameof(vertexCount), "Vertex count must be positive.");

        _adjacency = new List<int>[vertexCount];
        for (int i = 0; i < vertexCount; i++)
        {
            _adjacency[i] = new List<int>();
        }
    }

    /// <summary>
    /// Adds an undirected edge between u and v.
    /// </summary>
    public void AddUndirectedEdge(int u, int v)
    {
        // STEP 1: Guard clauses – validate indices.
        if (!IsValidVertex(u) || !IsValidVertex(v))
            throw new ArgumentOutOfRangeException("Vertex index out of range.");

        // STEP 2: Add neighbors in both directions.
        _adjacency[u].Add(v);
        _adjacency[v].Add(u);
    }

    /// <summary>
    /// Adds a directed edge u -> v.
    /// </summary>
    public void AddDirectedEdge(int u, int v)
    {
        if (!IsValidVertex(u) || !IsValidVertex(v))
            throw new ArgumentOutOfRangeException("Vertex index out of range.");

        _adjacency[u].Add(v);
    }

    public IEnumerable<int> GetNeighbors(int vertex)
    {
        if (!IsValidVertex(vertex))
            throw new ArgumentOutOfRangeException(nameof(vertex));

        return _adjacency[vertex];
    }

    public int VertexCount => _adjacency.Length;

    private bool IsValidVertex(int v) => v >= 0 && v < _adjacency.Length;
}
```

**C# Engineering Notes:**
- Use `List<int>[]` rather than `List<List<int>>` for faster index access and better locality.  
- Validate vertex indices early to avoid subtle `IndexOutOfRangeException`.  
- For **weighted graphs**, change `List<int>` to `List<(int to, int weight)>` and adjust neighbor iteration accordingly.

**Implicit Grid Neighbors (No Explicit Edge Storage)**

```csharp
public static IEnumerable<(int row, int col)> GetNeighbors(int row, int col, int rows, int cols)
{
    // MENTAL MODEL:
    // Think of this as walking up/down/left/right from (row, col) within bounds.

    // Up
    if (row - 1 >= 0)
        yield return (row - 1, col);

    // Down
    if (row + 1 < rows)
        yield return (row + 1, col);

    // Left
    if (col - 1 >= 0)
        yield return (row, col - 1);

    // Right
    if (col + 1 < cols)
        yield return (row, col + 1);
}
```

### 3.2 Pattern 2 – Unweighted Shortest Path (BFS)

**Mental Model:**  
BFS is a **wave expanding from the source**. Each step pushes the frontier one edge further. The first time you reach a node, you have found the shortest path (fewest edges) to it.

**When to Use:**
- Unweighted graphs where each edge costs the same.  
- Shortest path in a grid/maze.  
- “Minimum number of moves” questions (Knight moves, word ladder with unit cost transformations).

**Core C# Implementation – BFS Distances + Parents**

```csharp
using System;
using System.Collections.Generic;

public static class BfsShortestPath
{
    /// <summary>
    /// MENTAL MODEL:
    /// Explore the graph in concentric layers from the source.
    /// The first time we visit a node, we lock in its shortest distance in edge count.
    /// Time: O(V + E). Space: O(V).
    /// </summary>
    public static (int[] distance, int[] parent) RunBfs(Graph graph, int source)
    {
        if (graph == null)
            throw new ArgumentNullException(nameof(graph));
        if (source < 0 || source >= graph.VertexCount)
            throw new ArgumentOutOfRangeException(nameof(source));

        int n = graph.VertexCount;
        var distance = new int[n];
        var parent = new int[n];
        var visited = new bool[n];
        const int INF = int.MaxValue;

        for (int i = 0; i < n; i++)
        {
            distance[i] = INF;
            parent[i] = -1;
        }

        var queue = new Queue<int>();

        // STEP 1: Initialize source layer.
        visited[source] = true;
        distance[source] = 0;
        queue.Enqueue(source);

        // STEP 2: Layer-by-layer expansion.
        while (queue.Count > 0)
        {
            int u = queue.Dequeue();

            foreach (int v in graph.GetNeighbors(u))
            {
                if (visited[v]) continue; // already assigned best distance

                visited[v] = true;
                distance[v] = distance[u] + 1;
                parent[v] = u;
                queue.Enqueue(v);
            }
        }

        return (distance, parent);
    }

    /// <summary>
    /// Reconstructs the path from source to target using parent pointers.
    /// Returns empty list if target is unreachable.
    /// </summary>
    public static List<int> ReconstructPath(int source, int target, int[] parent)
    {
        if (parent == null)
            throw new ArgumentNullException(nameof(parent));
        if (source < 0 || source >= parent.Length || target < 0 || target >= parent.Length)
            throw new ArgumentOutOfRangeException("Source or target out of range.");

        var path = new List<int>();
        int current = target;

        while (current != -1)
        {
            path.Add(current);
            if (current == source) break;
            current = parent[current];
        }

        path.Reverse();

        // If we never reached the source, no valid path exists.
        if (path.Count == 0 || path[0] != source)
            return new List<int>();

        return path;
    }
}
```

**C# Engineering Notes:**
- Guard for `graph == null` and invalid `source`.  
- Use `INF` to mark unreachable nodes; do not rely on `0` or `-1`.  
- Mark `visited[v]` **when enqueuing** to avoid duplicates in the queue.

### 3.3 Pattern 3 – DFS & Topological Sort

**Mental Model (DFS):**  
DFS walks a **path as deep as possible** before backtracking. Imagine following directories in a file system until you hit a file, then backing up to explore siblings.

**Mental Model (Topological Sort via DFS):**  
In a DAG, if you record nodes when they are **finished** (post-order), then **reverse that order**, every edge will go from earlier to later in the list – a valid dependency-respecting order.

**When to Use:**
- You need a **build/compile order** given dependency edges A → B.  
- You must detect **cycles** in a directed graph.  
- You want to compute DP over DAGs (later weeks).

**Core C# Implementation – DFS-Based Topological Sort + Cycle Detection**

```csharp
using System;
using System.Collections.Generic;

public class DfsTopologicalSorter
{
    private readonly List<int>[] _adjacency;

    /// <summary>
    /// MENTAL MODEL:
    /// Perform DFS and think of each node "leaving" the recursion stack only
    /// after all its dependencies are handled. Reverse of this finishing order
    /// gives a valid build order (topological sort) when the graph is acyclic.
    /// Time: O(V + E). Space: O(V).
    /// </summary>
    public DfsTopologicalSorter(int vertexCount)
    {
        if (vertexCount <= 0)
            throw new ArgumentOutOfRangeException(nameof(vertexCount));

        _adjacency = new List<int>[vertexCount];
        for (int i = 0; i < vertexCount; i++)
        {
            _adjacency[i] = new List<int>();
        }
    }

    public void AddEdge(int from, int to)
    {
        if (!IsValidVertex(from) || !IsValidVertex(to))
            throw new ArgumentOutOfRangeException("Vertex index out of range.");

        _adjacency[from].Add(to);
    }

    public (bool hasCycle, IList<int> order) TopologicalSort()
    {
        int n = _adjacency.Length;
        var visited = new int[n]; // 0 = unvisited, 1 = visiting, 2 = visited
        var result = new List<int>();
        bool hasCycle = false;

        void Dfs(int u)
        {
            if (hasCycle) return; // early exit once a cycle is found

            visited[u] = 1; // visiting (GRAY)

            foreach (int v in _adjacency[u])
            {
                if (visited[v] == 0)
                {
                    Dfs(v);
                }
                else if (visited[v] == 1)
                {
                    // Back edge to an ancestor in the recursion stack → cycle.
                    hasCycle = true;
                    return;
                }
            }

            visited[u] = 2; // done (BLACK)
            result.Add(u);  // post-order append
        }

        for (int i = 0; i < n; i++)
        {
            if (visited[i] == 0)
                Dfs(i);
        }

        if (hasCycle)
            return (true, Array.Empty<int>());

        result.Reverse();
        return (false, result);
    }

    private bool IsValidVertex(int v) => v >= 0 && v < _adjacency.Length;
}
```

**C# Engineering Notes:**
- Use an **int state array** (0/1/2) instead of a `bool` to distinguish “visiting” (on stack) vs “visited”.  
- Return `(hasCycle, order)` so callers can handle cyclic dependency errors cleanly.

### 3.4 Pattern 4 – Connectivity & Bipartite Tests

**Mental Model:**  
Connectivity splits the graph into **islands**; bipartite testing tries to paint the graph in **two colors** such that every edge connects different colors. An odd cycle forces a color conflict.

**When to Use:**
- “How many separate networks / groups are there?”  
- “Can we split people into two groups so that no incompatible pair sits together?”  
- Pre-checks before running algorithms requiring bipartite graphs.

**Core C# Implementation – Components + Bipartite Check (BFS)**

```csharp
using System;
using System.Collections.Generic;

public static class ConnectivityAndBipartite
{
    /// <summary>
    /// Labels connected components of an undirected graph.
    /// componentId[v] = component index of vertex v (0..componentCount-1).
    /// Time: O(V + E).
    /// </summary>
    public static int[] LabelConnectedComponents(Graph graph, out int componentCount)
    {
        if (graph == null)
            throw new ArgumentNullException(nameof(graph));

        int n = graph.VertexCount;
        var componentId = new int[n];
        Array.Fill(componentId, -1);

        int currentComponent = 0;
        var queue = new Queue<int>();

        for (int start = 0; start < n; start++)
        {
            if (componentId[start] != -1) continue; // already assigned

            // BFS from this start to label an entire component.
            componentId[start] = currentComponent;
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                int u = queue.Dequeue();
                foreach (int v in graph.GetNeighbors(u))
                {
                    if (componentId[v] != -1) continue;
                    componentId[v] = currentComponent;
                    queue.Enqueue(v);
                }
            }

            currentComponent++;
        }

        componentCount = currentComponent;
        return componentId;
    }

    /// <summary>
    /// Checks if an undirected graph is bipartite using BFS two-coloring.
    /// color[v] = 0 or 1 for each partition; -1 if graph is not bipartite.
    /// Time: O(V + E).
    /// </summary>
    public static (bool isBipartite, int[] color) IsBipartite(Graph graph)
    {
        if (graph == null)
            throw new ArgumentNullException(nameof(graph));

        int n = graph.VertexCount;
        var color = new int[n];
        Array.Fill(color, -1); // -1 = uncolored

        var queue = new Queue<int>();

        for (int start = 0; start < n; start++)
        {
            if (color[start] != -1) continue;

            color[start] = 0;
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                int u = queue.Dequeue();

                foreach (int v in graph.GetNeighbors(u))
                {
                    if (color[v] == -1)
                    {
                        // Assign opposite color to neighbor.
                        color[v] = 1 - color[u];
                        queue.Enqueue(v);
                    }
                    else if (color[v] == color[u])
                    {
                        // Found same-color edge → odd cycle.
                        return (false, Array.Empty<int>());
                    }
                }
            }
        }

        return (true, color);
    }
}
```

**C# Engineering Notes:**
- Use `Array.Fill` for clear initialization.  
- Check all components by looping over all vertices, not just starting once.  
- Return colors only when bipartite; returning empty array on failure simplifies caller logic.

### 3.5 Pattern 5 – Strongly Connected Components (Kosaraju)

**Mental Model:**  
SCCs are **clusters of nodes where every node can reach every other**. Kosaraju does a **first DFS** to get a finishing-time order, then runs DFS on the **reversed graph** in that order; each DFS in the second pass peels off one SCC.

**When to Use:**
- Modularizing directed graphs into components for **condensation DAG**.  
- Implication graph problems (e.g., 2-SAT; later weeks).  
- Understanding cycles in dependency networks.

**Core C# Implementation – Kosaraju SCC**

```csharp
using System;
using System.Collections.Generic;

public class KosarajuStronglyConnectedComponents
{
    private readonly List<int>[] _adjacency;
    private readonly List<int>[] _transpose;

    /// <summary>
    /// MENTAL MODEL:
    /// Two DFS passes:
    /// 1) Record nodes in order of completion (post-order) on original graph.
    /// 2) Run DFS on the transposed graph in that order to peel off SCCs.
    /// Time: O(V + E). Space: O(V + E).
    /// </summary>
    public KosarajuStronglyConnectedComponents(int vertexCount)
    {
        if (vertexCount <= 0)
            throw new ArgumentOutOfRangeException(nameof(vertexCount));

        _adjacency = new List<int>[vertexCount];
        _transpose = new List<int>[vertexCount];
        for (int i = 0; i < vertexCount; i++)
        {
            _adjacency[i] = new List<int>();
            _transpose[i] = new List<int>();
        }
    }

    public void AddEdge(int from, int to)
    {
        if (!IsValidVertex(from) || !IsValidVertex(to))
            throw new ArgumentOutOfRangeException("Vertex index out of range.");

        _adjacency[from].Add(to);
        _transpose[to].Add(from); // reverse edge
    }

    public int[] ComputeComponents(out int componentCount)
    {
        int n = _adjacency.Length;
        var visited = new bool[n];
        var order = new Stack<int>();

        // STEP 1: DFS on original graph to fill order by finish time.
        void Dfs1(int u)
        {
            visited[u] = true;
            foreach (int v in _adjacency[u])
            {
                if (!visited[v])
                    Dfs1(v);
            }
            order.Push(u);
        }

        for (int i = 0; i < n; i++)
        {
            if (!visited[i]) Dfs1(i);
        }

        // STEP 2: DFS on transpose in order of decreasing finish time.
        Array.Fill(visited, false);
        var componentId = new int[n];
        Array.Fill(componentId, -1);

        componentCount = 0;

        void Dfs2(int u)
        {
            visited[u] = true;
            componentId[u] = componentCount;
            foreach (int v in _transpose[u])
            {
                if (!visited[v])
                    Dfs2(v);
            }
        }

        while (order.Count > 0)
        {
            int v = order.Pop();
            if (!visited[v])
            {
                Dfs2(v);
                componentCount++;
            }
        }

        return componentId;
    }

    private bool IsValidVertex(int v) => v >= 0 && v < _adjacency.Length;
}
```

**C# Engineering Notes:**
- Maintain both forward and transpose adjacency lists as you add edges for O(1) updates.  
- `componentId[v]` labels which SCC each vertex belongs to; use this to build a **condensation DAG** if needed.

---

## SECTION 4 – C# COLLECTION DECISION GUIDE 🗂️

| Use Case                                                                                       | Recommended Collection(s)                        | Why This is Best                                                                                 | When NOT to Use                                               | Better Alternative                         |
| --------------------------------------------------------------------------------------------- | ------------------------------------------------ | ----------------------------------------------------------------------------------------------- | -------------------------------------------------------------- | ----------------------------------------- |
| Static graph with known vertex count; frequent neighbor iteration                            | `List<int>[]` or `List<(int to,int w)>[]`       | O(1) indexed access; low per-edge overhead; cache-friendly iteration.                           | When you need O(1) edge-existence checks frequently.          | Add an adjacency **matrix** for that case. |
| Very dense graph where V is moderate (≤ 2000) and you need constant-time edge lookups        | `bool[,]` or `int[,]` matrix                     | Simple to reason about; `matrix[u,v]` says if edge exists; good for dense graphs.               | Large V (10^5) or very sparse; memory will explode.           | Use adjacency lists.                      |
| BFS/DFS queues                                                                                | `Queue<int>`                                     | O(1) enqueue/dequeue; semantics match BFS perfectly.                                            | Using `List<int>` with `RemoveAt(0)` (O(n) per removal).      | `Queue<int>`                             |
| Explicit stacks for iterative DFS                                                             | `Stack<int>`                                     | O(1) push/pop; expresses DFS semantics clearly.                                                 | Using recursion when call-depth may exceed stack limit.       | Iterative DFS with `Stack<int>`.         |
| Tracking visited nodes or membership in sets (e.g., special vertices)                        | `bool[]` or `HashSet<int>`                       | `bool[]` is fastest when vertex IDs are dense; `HashSet<int>` for sparse IDs.                   | Using `List<int>.Contains` (O(n) per check).                   | `HashSet<int>` / `bool[]`                |
| Disjoint sets for offline connectivity / Kruskal-like behavior                               | Custom **Union–Find** with two `int[]` arrays   | Near O(1) operations with path compression and union-by-rank.                                   | Repeated BFS/DFS for independent connectivity queries.        | DSU (Union–Find).                        |

Key insight: **wrong collection = correct algorithm running slowly.** Always align collections with access patterns (indexing vs iteration vs membership tests).

---

## SECTION 5 – PROGRESSIVE PROBLEM LADDER 🧗

> Note: Problem labels here are descriptive and pattern-oriented; plug in actual platforms (LeetCode, etc.) as needed.

### 5.1 Stage 1 – Canonical Problems (Core Skeletons)

Goal: Implement each core pattern **from scratch in C#** in under 10–15 minutes.

| ID | Difficulty | Pattern                        | C# Focus                                      | Core Concept                                      |
| -- | ---------- | ------------------------------ | --------------------------------------------- | ------------------------------------------------- |
| 1  | Easy       | Graph Representation           | `List<int>[]` construction, safe edge adds   | Build adjacency list from edge list.             |
| 2  | Easy       | BFS Shortest Path              | `Queue<int>`, `distance[]`, `parent[]`       | Compute shortest hops from a single source.      |
| 3  | Easy       | BFS on Grid                    | Implicit neighbors, bounds checks            | Shortest path in a binary matrix/maze.           |
| 4  | Easy       | DFS + Topological Sort         | Recursion with state array                   | Produce topo order or detect cycle.              |
| 5  | Easy       | Components (Undirected)        | Multi-source BFS, `componentId[]`            | Count components and label each vertex.          |
| 6  | Easy       | Bipartite Test                 | BFS 2-coloring with `color[]`                | Detect odd cycle via color conflict.             |
| 7  | Easy       | SCC (Kosaraju)                 | Two-pass DFS, transpose graph                | Label SCC IDs for each node.                     |

### 5.2 Stage 2 – Variations (Boundaries & Edge Cases)

Goal: Learn when a simple pattern needs modification or when to change patterns entirely.

| ID | Difficulty | Pattern                     | Twist / Variation                                      | C# Focus                                        | When Pattern Breaks                        |
| -- | ---------- | --------------------------- | ------------------------------------------------------ | ----------------------------------------------- | ------------------------------------------ |
| 8  | Medium     | BFS                         | Multiple sources as starting points                    | Initialize queue with all sources               | If you fake multi-source by adding a fake node incorrectly. |
| 9  | Medium     | BFS                         | Detecting bipartite + counting component sizes        | Combined `color[]` + `componentSize[]`          | If you forget to re-init per component.     |
| 10 | Medium     | DFS Topological Sort        | Large DAG where recursion depth is close to stack limit | Iterative DFS with `Stack<int>`                 | Pure recursion may overflow stack.         |
| 11 | Medium     | Connectivity + Union–Find   | Processing dynamic addition of edges offline          | Union–Find operations ordering                  | Using BFS per query is too slow.           |
| 12 | Medium     | SCC (Kosaraju)              | Build condensation DAG and run topo sort over SCCs    | Map SCC IDs to DAG, reuse topo pattern          | If SCC IDs not compact/contiguous, mapping fails. |

### 5.3 Stage 3 – Integration (Multiple Patterns Together)

Goal: Combine graph patterns under realistic constraints.

| ID | Difficulty | Patterns Combined                           | C# Focus                                      | Why Combine?                                                   |
| -- | ---------- | ------------------------------------------- | --------------------------------------------- | -------------------------------------------------------------- |
| 13 | Hard       | BFS + Connectivity                          | BFS per component + path queries              | Need both group labeling and shortest paths.                    |
| 14 | Hard       | Topological Sort + DP on DAG (Week 10 tie)  | Use topo order to run DP over DAG             | Ordering plus DP; BFS/DFS alone doesn’t order correctly.       |
| 15 | Hard       | SCC + Topological Sort                      | Collapse SCC, topo sort condensation DAG      | Inside SCCs cycles allowed; between SCCs must respect ordering. |

Use this ladder as a **practice roadmap**: finish Stage 1, then 60–70% of Stage 2, then sample from Stage 3 as time allows.

---

## SECTION 6 – WEEK 08 PITFALLS & C# GOTCHAS 🐞

### 6.1 Runtime Issues by Pattern

| Pattern                         | Bug / Mistake                                                     | C# Symptom / Behavior                           | Quick Fix                                                                 |
| ------------------------------ | ----------------------------------------------------------------- | ----------------------------------------------- | ------------------------------------------------------------------------- |
| BFS Shortest Path              | Marking `visited` on **dequeue** instead of enqueue              | Nodes enqueued multiple times; distances wrong. | Set `visited[v] = true` immediately when you enqueue `v`.                |
| BFS on Grid                    | Forgetting bounds checks on neighbor coordinates                 | `IndexOutOfRangeException` at runtime.          | Centralize neighbor generation in a helper with robust bounds checks.    |
| DFS Topological Sort           | Missing GRAY state; only `bool visited`                          | Cycles undetected; topological order invalid.   | Use `int[] state` (0,1,2) and treat edge to `state == 1` as back edge.   |
| Connectivity Components        | Not iterating through all vertices; starting BFS only once       | Some vertices keep default component ID -1.     | Wrap BFS/DFS inside outer loop over all vertices.                        |
| Bipartite Test                 | Reusing `color[]` without re-initialization for new graphs       | Spurious conflicts or incorrect results.        | Always reinitialize `color[]` to -1 before checking new graph.           |
| SCC (Kosaraju)                 | Forgetting to reset `visited` before second DFS pass             | Many nodes never visited in second pass.        | After first pass, call `Array.Fill(visited, false)` before DFS 2.        |

### 6.2 Collection Gotchas

- ❌ **Using `List<int>.RemoveAt(0)` as a queue:**  
  - Why wrong: O(n) shift per removal → O(n²) BFS.  
  - Fix: Use `Queue<int>` for true O(1) FIFO operations.

- ❌ **Using `HashSet<int>` for dense ID spaces where `bool[]` is enough:**  
  - Why suboptimal: Extra allocations and hashing overhead.  
  - Fix: Use `bool[] visited` for vertex IDs 0..V-1.

- ❌ **Using recursion for DFS on very deep graphs (e.g., long chains) without considering stack limits:**  
  - Symptom: `StackOverflowException` on large inputs.  
  - Fix: Implement iterative DFS with `Stack<int>` when depth can be large.

- ❌ **Reusing mutable collections across tests without clearing:**  
  - Symptom: Edges from previous test cases leak into new ones.  
  - Fix: Recreate adjacency list or call `.Clear()` on each list before reuse.

---

## SECTION 7 – QUICK REFERENCE (INTERVIEW PREP) ⚡

| Pattern Name                      | 1-Liner Mental Model                                                                 | Code Symbol (Skeleton)                                                                                 | When You See This…                                               |
| --------------------------------- | ------------------------------------------------------------------------------------ | ------------------------------------------------------------------------------------------------------ | ----------------------------------------------------------------- |
| Graph Representation (Adj List)   | For each node, keep a list of neighbors; only store edges that exist.               | `var adj = new List<int>[n];` + loop to `new List<int>()` + `adj[u].Add(v);`                           | “Large sparse network”, “dynamic graph traversal”.              |
| BFS Shortest Path (Unweighted)    | Expand in waves; first visit locks in shortest hop count.                            | `Queue<int> q; bool[] vis; dist[v]=dist[u]+1;`                                                         | “Fewest edges / steps; all edges cost 1”.                       |
| DFS + Topological Sort            | Go deep along dependencies; reverse finishing times to get valid order.              | `void Dfs(u){ state[u]=1; foreach(v) Dfs(v); state[u]=2; order.Add(u); } order.Reverse();`             | “Prerequisites / dependencies; need valid build order”.         |
| Connectivity (Components)         | Flood-fill each island; label all vertices reachable from each start.                | Outer loop over `v`; BFS/DFS from unvisited; `componentId[v] = currentComponent;`                      | “How many groups? which group is this node in?”.               |
| Bipartite Testing                 | Try to paint graph in 2 colors so every edge crosses colors; odd cycle blocks this.  | `color[u]=0; queue; color[v]=1-color[u]; if(color[u]==color[v]) not bipartite;`                        | “Split nodes into two non-conflicting sets; no edges inside sets”. |
| SCC (Kosaraju)                    | Two DFS passes: order by finish time, then peel SCCs on reversed graph.              | First DFS: push to stack; reset `visited`; second DFS on transpose in stack order, assign `component`. | “Strong mutual reachability; compress cycles into meta-nodes”.  |

---

## WEEK 08 COMPLETION CHECKLIST ✅

**Pattern Fluency – Can You Recognize & Choose?**
- [ ] Given a new problem, you can decide **within 30–60 seconds** whether it is BFS, DFS/topo, connectivity, bipartite, or SCC-based.  
- [ ] You can explain **why BFS** is correct for unweighted shortest path and **why it fails** with weights.  
- [ ] You can explain **why reversing DFS finish order** yields a topological sort in a DAG.  
- [ ] You can explain why an odd cycle breaks bipartiteness.

**Problem Solving – Can You Practice?**
- [ ] You have implemented at least **3 canonical Stage 1** graph problems using adjacency lists and BFS/DFS.  
- [ ] You have solved **2–3 Stage 2 variations**, including multi-source BFS and bipartite checks.  
- [ ] You have attempted at least **1 Stage 3 integration** problem (e.g., SCC + topo).

**Production Code Quality – Can You Code?**
- [ ] All your BFS/DFS implementations use **guard clauses** and **explicit state** (`visited`, `state`, `color`).  
- [ ] You never use `List<T>.RemoveAt(0)` for queues; you use `Queue<T>`.  
- [ ] Your code clearly documents **time/space complexity** in comments.  
- [ ] You have at least one well-commented C# implementation for BFS, DFS+topo, connectivity, and SCC.

**Interview Ready – Can You Communicate?**
- [ ] You can sketch and explain BFS, DFS, topological sort, and SCC on a whiteboard without looking at code.  
- [ ] You can argue for your **pattern choice**, not just write code (e.g., “Why BFS here, not DFS?”).  
- [ ] You can discuss trade-offs of **adjacency list vs matrix**, and **BFS vs DFS** for particular tasks.

If all boxes are checked, Week 08 graph fundamentals are **interview-ready** for both pattern selection and C# implementation. You are ready to build on this foundation in **Week 09: Graph Algorithms I – Shortest Paths & MST**. 
