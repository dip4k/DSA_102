# 📘 Week 8 Day 4: Connectivity & Bipartite Graphs — Engineering Guide

> 🧭 **Navigation:** [← Previous Day](Week_08_Day_03_DFS_Topological_Sort_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_08_Day_05_Strongly_Connected_Components_Instructional.md)
> 
> 💡 **Instructor Note:** *Master the fundamental duality between static and dynamic connectivity: use BFS/DFS when the graph is static and already built (`O(V + E)`), and use Disjoint Set Union (DSU / Union-Find) when edges arrive dynamically in an online stream (`O(E * alpha(V))`). For bipartite testing, always remember to iterate through all vertices to handle disconnected subgraphs.*

---

## 🎯 LEARNING OBJECTIVES

By the end of this chapter, you will be able to:

- **Partition** undirected graphs into connected components using both multi-pass traversals and Disjoint Set Union (DSU).
- **Verify** bipartiteness using BFS/DFS 2-coloring, understanding the Odd-Cycle Obstruction theorem.
- **Implement** production-grade DSU with path compression and union by rank in modern C# (.NET 8/9) and idiomatic Python (3.11+).
- **Deconstruct** the Inverse Ackermann complexity bound `O(alpha(V))` and deliver a structured 45-minute technical interview script.

---

## 📖 CHAPTER 1: CONTEXT & MOTIVATION

### Network Partitions & Bipartite Structures

Connectivity questions lie at the heart of distributed infrastructure and constraint satisfaction:
1. **Cloud Fault Domains (AWS, Azure):** Given server racks and redundant cross-links, can every node communicate with every other node, or is there an isolated partition?
2. **Recommendation Engines & Bipartite Graphs:** Users and Products form two distinct sets. Edges only exist between Users and Products (never User-to-User or Product-to-Product).
3. **Ride-Sharing & Two-Sided Matching (Uber, Lyft):** Drivers and Riders form a bipartite graph. Maximum bipartite matching matches supply with demand.
4. **Constraint Conflict Satisfaction (Possible Bipartition):** Splitting a group into two rival teams such that no pair of enemies ends up on the same team.

### The Underlying Graph Invariants

- **Connected Component:** A maximal subgraph where every pair of vertices has a mutual undirected path.
- **Bipartite Graph (2-Colorable):** Vertices can be partitioned into two disjoint subsets `U` and `V` such that every edge connects a vertex in `U` to a vertex in `V`.
- **The Odd-Cycle Invariant:** A graph is bipartite **if and only if** it contains no cycles of odd length.

---

## 🧠 CHAPTER 2: BUILDING THE MENTAL MODEL

### Bipartite 2-Coloring & The Odd-Cycle Conflict

```
Valid Bipartite Graph (Even Cycle / Trees):
  Color 0:    ( A )       ( C )
                \        /
                 \      /
  Color 1:         ( B )
  Edges: (A-B), (B-C). Every edge connects Color 0 to Color 1. Valid!

Invalid Bipartite Graph (Odd Cycle / Triangle):
                  ( A: 0 )
                   /    \
                  /      \
             ( B: 1 ) -- ( C: 1? Conflict! )
  Edge (B-C) connects two nodes of identical color (1 <-> 1).
  ODD CYCLE OF LENGTH 3 DETECTED ===> NOT BIPARTITE!
```

### Disjoint Set Union (DSU) Tree Forest

```
DSU Forest Representation:
  Initial: Each vertex is its own root.
    [0]  [1]  [2]  [3]  [4]

  After Union(0, 1), Union(1, 2):
        ( 1 )              Path Compression: Find(0)
       /     \             flattens the tree directly
     ( 0 )   ( 2 )         under the root:
                           ( 1 )
                          /  |  \
                        (0) (2) (3)
```

---

## ⚙️ CHAPTER 3: MECHANICS & DUAL-LANGUAGE IMPLEMENTATIONS

### 1. Connected Components Labeling (BFS)

Labels all vertices with their component identifier (`0, 1, ..., C - 1`) across disconnected subgraphs.

#### C# (.NET 8/9) Implementation

```csharp
using System;
using System.Collections.Generic;

public static class ComponentLabeler
{
    public static (int ComponentCount, int[] ComponentIds) LabelComponents(
        List<int>[] adj, int n)
    {
        var componentIds = new int[n];
        Array.Fill(componentIds, -1);
        int count = 0;
        var queue = new Queue<int>();

        for (int i = 0; i < n; i++)
        {
            if (componentIds[i] != -1) continue;

            // Start new component
            componentIds[i] = count;
            queue.Enqueue(i);

            while (queue.Count > 0)
            {
                int u = queue.Dequeue();
                foreach (int v in adj[u])
                {
                    if (componentIds[v] == -1)
                    {
                        componentIds[v] = count;
                        queue.Enqueue(v);
                    }
                }
            }

            count++;
        }

        return (count, componentIds);
    }
}
```

#### Python (3.11+) Implementation

```python
from collections import deque
from typing import List, Tuple

def label_components(adj: List[List[int]], n: int) -> Tuple[int, List[int]]:
    """Labels all connected components in an undirected graph via BFS."""
    component_ids = [-1] * n
    count = 0

    for i in range(n):
        if component_ids[i] != -1:
            continue

        component_ids[i] = count
        queue = deque([i])

        while queue:
            u = queue.popleft()
            for v in adj[u]:
                if component_ids[v] == -1:
                    component_ids[v] = count
                    queue.append(v)

        count += 1

    return count, component_ids
```

---

### 2. Bipartite Verification via 2-Coloring (BFS)

Assigns alternating colors `0` and `1`. Returns `false` if an edge connects two vertices of identical color. Handles disconnected graphs by iterating over all vertices.

#### C# (.NET 8/9) Implementation

```csharp
public static class BipartiteChecker
{
    public static bool IsBipartite(int[][] graph)
    {
        int n = graph.Length;
        var color = new int[n];
        Array.Fill(color, -1); // -1 = uncolored

        var queue = new Queue<int>();

        for (int i = 0; i < n; i++)
        {
            if (color[i] != -1) continue; // Component already validated

            color[i] = 0;
            queue.Enqueue(i);

            while (queue.Count > 0)
            {
                int u = queue.Dequeue();

                foreach (int v in graph[u])
                {
                    if (color[v] == -1)
                    {
                        color[v] = 1 - color[u]; // Invert color: 0 -> 1, 1 -> 0
                        queue.Enqueue(v);
                    }
                    else if (color[v] == color[u])
                    {
                        return false; // Same color edge detected (odd cycle)
                    }
                }
            }
        }

        return true;
    }
}
```

#### Python (3.11+) Implementation

```python
from collections import deque
from typing import List

def is_bipartite(graph: List[List[int]]) -> bool:
    """Validates whether an undirected graph is bipartite via BFS 2-coloring."""
    n = len(graph)
    color = [-1] * n

    for i in range(n):
        if color[i] != -1:
            continue

        color[i] = 0
        queue = deque([i])

        while queue:
            u = queue.popleft()

            for v in graph[u]:
                if color[v] == -1:
                    color[v] = 1 - color[u]
                    queue.append(v)
                elif color[v] == color[u]:
                    return False  # Odd cycle found

    return True
```

---

### 3. Disjoint Set Union (DSU / Union-Find)

Production DSU with **Path Compression** and **Union by Rank** for nearly `O(1)` dynamic connectivity.

#### C# (.NET 8/9) Implementation

```csharp
public sealed class DisjointSetUnion
{
    private readonly int[] _parent;
    private readonly int[] _rank;
    public int ComponentCount { get; private set; }

    public DisjointSetUnion(int n)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(n);
        ComponentCount = n;
        _parent = new int[n];
        _rank = new int[n];
        for (int i = 0; i < n; i++) _parent[i] = i;
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

        if (rootX == rootY) return false; // Already in same component

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

        ComponentCount--;
        return true;
    }

    public bool Connected(int x, int y) => Find(x) == Find(y);
}
```

#### Python (3.11+) Implementation

```python
class DisjointSetUnion:
    """Disjoint Set Union with Path Compression and Union by Rank."""
    __slots__ = ("parent", "rank", "component_count")

    def __init__(self, n: int) -> None:
        self.component_count: int = n
        self.parent: list[int] = list(range(n))
        self.rank: list[int] = [0] * n

    def find(self, x: int) -> int:
        if self.parent[x] != x:
            self.parent[x] = self.find(self.parent[x])  # Path compression
        return self.parent[x]

    def union(self, x: int, y: int) -> bool:
        root_x = self.find(x)
        root_y = self.find(y)

        if root_x == root_y:
            return False

        if self.rank[root_x] < self.rank[root_y]:
            self.parent[root_x] = root_y
        elif self.rank[root_x] > self.rank[root_y]:
            self.parent[root_y] = root_x
        else:
            self.parent[root_y] = root_x
            self.rank[root_x] += 1

        self.component_count -= 1
        return True

    def connected(self, x: int, y: int) -> bool:
        return self.find(x) == self.find(y)
```

---

## 📊 CHAPTER 4: COMPLEXITY DECONSTRUCTION

| Technique | Time Complexity | Auxiliary Space | Dynamic Edge Insertions? | Edge Removals? |
| :--- | :--- | :--- | :--- | :--- |
| **BFS / DFS Component Labeling** | `O(V + E)` | `O(V)` | ❌ Re-run required (`O(V+E)`) | ❌ Re-run required |
| **BFS 2-Coloring Bipartite** | `O(V + E)` | `O(V)` | ❌ Static graph check | ❌ Static graph check |
| **DSU (Path Compression + Rank)**| `O(E * alpha(V))` | `O(V)` | ✅ Incremental `O(alpha(V))` | ❌ Not supported |

### The Inverse Ackermann Invariant `alpha(V)`

- `alpha(V)` is the functional inverse of the Ackermann function.
- For all realistic universe scales (`V <= 10^{80}`), `alpha(V) <= 4`.
- Thus, DSU operations run in **effective `O(1)` amortized time** per operation.
- Without path compression or rank, tree height degrades to `O(V)`, turning DSU operations into slow `O(V)` scans.

---

## 🎙️ CHAPTER 5: 45-MINUTE INTERVIEW VERBAL SCRIPT

### Phase 1: Clarification & Constraint Scoping (0–5 Mins)
- **Candidate:** "Before deciding between BFS/DFS and Union-Find:
  1. Is the graph static or do edges arrive dynamically over time?
  2. Can the graph contain multiple disconnected components?
  3. For bipartite checks, can vertices have self-loops or isolated nodes?
  4. What are the constraints on `V` and `E`?"
- **Interviewer:** "Static graph, `V <= 10^5, E <= 2 * 10^5`, disconnected components are possible, no self-loops."
- **Candidate:** "Because the graph is static, standard BFS 2-coloring in `O(V + E)` time is optimal."

### Phase 2: High-Level Approach & Trade-Offs (5–12 Mins)
- **Candidate:** "A graph is bipartite if and only if it contains no odd-length cycles.
  - We can verify this by attempting a 2-coloring.
  - We maintain a `color` array initialized to `-1`.
  - We iterate through all vertices `0` to `V - 1`. If vertex `i` is uncolored, we seed a BFS with color `0`.
  - For every neighbor `v` of `u`, if `v` is uncolored, we assign `1 - color[u]`. If `color[v] == color[u]`, an odd cycle exists, and we return `false`.
  - If all components color successfully, we return `true`."

### Phase 3: Live Coding Walkthrough (12–32 Mins)
- **Candidate:** "I'll implement the BFS solution. Notice the outer loop from `0` to `n - 1`: this guarantees that disconnected graphs with isolated nodes or separate islands are fully colored. The color flip `1 - color[u]` cleanly toggles between 0 and 1 without branching."

### Phase 4: Edge Cases & Verification (32–40 Mins)
- **Candidate:** "Let's test edge cases:
  1. Empty graph or single node (`V = 1, E = 0`): Colors immediately; returns `true`.
  2. Disconnected components where one is bipartite and one has an odd cycle: The outer loop reaches the invalid component and immediately returns `false`.
  3. Star graph / Tree: Trees contain zero cycles, so they are always bipartite; returns `true`."

---

## 🛠️ CHAPTER 6: COMMON PITFALLS & DECISION FRAMEWORK

### Common Pitfalls
1. **Single-Source Assumption:** Initiating traversal only from vertex `0`. If vertex `0` is in an isolated component, remaining components with odd cycles are never checked.
2. **Missing Path Compression:** Omitting `parent[x] = find(parent[x])` in DSU, which degrades lookup performance to `O(N)`.
3. **Using DSU for Directed Reachability:** DSU models symmetric (undirected) equivalence relations. It cannot solve directed reachability or DAG topological sorting.

### Connectivity Decision Tree

```
                           [ Connectivity Problem ]
                                      |
                +---------------------+---------------------+
                |                                           |
         [ Static Graph ]                            [ Dynamic Stream ]
                |                                           |
        Need 2-Coloring?                                    |
        +-------+-------+                                   |
        |               |                                   |
       YES              NO                                  |
        |               |                                   |
    BFS/DFS 2-Color   BFS/DFS Components               Disjoint Set Union
       (O(V+E))          (O(V+E))                      (O(E * alpha(V)))
```

---

## 🏋️ PRACTICE LADDER

| Problem | LeetCode # | Difficulty | Core Pattern | Key Invariant |
| :--- | :--- | :--- | :--- | :--- |
| **Is Graph Bipartite?** | #785 | 🟡 Medium | BFS 2-Coloring | Absence of odd-length cycles across all components |
| **Possible Bipartition** | #886 | 🟡 Medium | 2-Coloring Graph Build | Rivalry edges must alternate colors |
| **Number of Provinces** | #547 | 🟡 Medium | Component Count | DSU or BFS to count distinct connected sets |
| **Redundant Connection**| #684 | 🟡 Medium | DSU Cycle Detection | First edge where `Find(u) == Find(v)` forms cycle |
| **Accounts Merge** | #721 | 🟡 Medium | DSU Email Grouping | Union emails sharing identical account owners |

---

> 🧭 **Navigation:** [← Previous Day](Week_08_Day_03_DFS_Topological_Sort_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_08_Day_05_Strongly_Connected_Components_Instructional.md)
