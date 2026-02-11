# 🧭 Advanced Graph Traversal Visual Playbook

**Professional-grade intuition for Levels 3–5 graph patterns**

This playbook covers advanced traversal techniques beyond basic DFS/BFS:
- **Level 3**: Shortest paths (unweighted BFS + parent), multi-source BFS, bipartite coloring
- **Level 4**: Topological sort (Kahn + DFS), directed cycle detection (3-color), 0–1 BFS
- **Level 5**: Implicit state-space graphs, DP on DAG

Each pattern follows the **Why / What / How / Where / When / Visual** teaching standard with Python/C# code, common pitfalls, and invariants.

---

## Table of Contents

1. [Pattern Selection Guide](#pattern-selection-guide)
2. [Level 3: The Frontier Layer](#level-3-the-frontier-layer)
   - [3.1 BFS Shortest Path + Parent Reconstruction](#31-bfs-shortest-path--parent-reconstruction)
   - [3.2 Multi-Source BFS](#32-multi-source-bfs)
   - [3.3 Bipartite Check (Graph Coloring)](#33-bipartite-check-graph-coloring)
3. [Level 4: The Constraint Layer](#level-4-the-constraint-layer)
   - [4.1 Topological Sort (Kahn's Algorithm)](#41-topological-sort-kahns-algorithm)
   - [4.2 Topological Sort (DFS Finish Order)](#42-topological-sort-dfs-finish-order)
   - [4.3 Directed Cycle Detection (3-Color DFS)](#43-directed-cycle-detection-3-color-dfs)
   - [4.4 0–1 BFS (Deque Frontier)](#44-01-bfs-deque-frontier)
4. [Level 5: The Abstract Layer](#level-5-the-abstract-layer)
   - [5.1 State-Space BFS (Implicit Graphs)](#51-state-space-bfs-implicit-graphs)
   - [5.2 DP on DAG (Topological Order)](#52-dp-on-dag-topological-order)
5. [Invariant Library](#invariant-library)
6. [Quick Reference Table](#quick-reference-table)

---

## Pattern Selection Guide

**Use this decision flow to map problem signals to patterns:**

```
Problem requires shortest path in unweighted graph?
├─ YES → 3.1 BFS Shortest Path + Parent
└─ NO  → Continue

Multiple sources spreading simultaneously?
├─ YES → 3.2 Multi-Source BFS
└─ NO  → Continue

Graph must be 2-colorable (bipartite)?
├─ YES → 3.3 Bipartite Check
└─ NO  → Continue

Need ordering that respects directed dependencies (DAG)?
├─ YES → 4.1 Topological Sort (Kahn) or 4.2 (DFS)
└─ NO  → Continue

Need to detect cycle in directed graph?
├─ YES → 4.3 Directed Cycle Detection (3-Color)
└─ NO  → Continue

Edge weights are only 0 or 1?
├─ YES → 4.4 0–1 BFS (Deque)
└─ NO  → Continue

Graph not given explicitly (states + transitions)?
├─ YES → 5.1 State-Space BFS
└─ NO  → Continue

Need DP computation on DAG with dependencies?
└─ YES → 5.2 DP on DAG
```

---

## Level 3: The Frontier Layer

**Goal**: Master distance-aware BFS patterns (shortest paths, multi-source spreading, graph coloring).

---

### 3.1 BFS Shortest Path + Parent Reconstruction

**Why**: BFS discovers nodes in increasing distance order in unweighted graphs. The first time a node is reached is guaranteed to be via a shortest path.

**What**: Maintain `dist[]` and `parent[]` arrays alongside BFS traversal. When discovering node `v` from `u`, set `dist[v] = dist[u] + 1` and `parent[v] = u`.

**How (stepflow)**:
1. Initialize `dist[u] = -1` for all nodes, `parent[u] = -1`
2. Set `dist[src] = 0`, enqueue `src`
3. While queue not empty:
   - `u = queue.pop()`
   - For each neighbor `v` of `u`:
     - If `dist[v] == -1` (unvisited):
       - `dist[v] = dist[u] + 1`
       - `parent[v] = u`
       - Enqueue `v`
4. To reconstruct path: walk backwards from `target` via `parent[]`, then reverse

**Where**: Shortest path in unweighted graphs, minimum moves, word ladder, open the lock.

**When**: Problem asks for "minimum number of steps" or "shortest path" without edge weights.

**Visual**:
```
Graph:  S ---1--- A ---1--- B ---1--- T
        |                           /
        +----------2-----------+---+

BFS from S:
  dist[S] = 0, parent[S] = -1
  dist[A] = 1, parent[A] = S
  dist[B] = 2, parent[B] = A
  dist[T] = 3, parent[T] = B  ← first discovery is shortest

Path reconstruction:
  Walk: T → B → A → S
  Reverse: [S, A, B, T]
```

**Python**:
```python
from collections import deque

def bfs_shortest_path(adj, src, target):
    n = len(adj)
    dist = [-1] * n
    parent = [-1] * n
    dist[src] = 0
    
    q = deque([src])
    while q:
        u = q.popleft()
        if u == target:
            break
        for v in adj[u]:
            if dist[v] == -1:
                dist[v] = dist[u] + 1
                parent[v] = u
                q.append(v)
    
    if dist[target] == -1:
        return []  # unreachable
    
    # Reconstruct path
    path = []
    cur = target
    while cur != -1:
        path.append(cur)
        cur = parent[cur]
    path.reverse()
    return path
```

**C#**:
```csharp
public static List<int> BfsShortestPath(Dictionary<int, List<int>> adj, int src, int target, int n)
{
    var dist = Enumerable.Repeat(-1, n).ToArray();
    var parent = Enumerable.Repeat(-1, n).ToArray();
    dist[src] = 0;
    
    var q = new Queue<int>();
    q.Enqueue(src);
    
    while (q.Count > 0)
    {
        int u = q.Dequeue();
        if (u == target) break;
        
        foreach (int v in adj[u])
        {
            if (dist[v] == -1)
            {
                dist[v] = dist[u] + 1;
                parent[v] = u;
                q.Enqueue(v);
            }
        }
    }
    
    if (dist[target] == -1) return new List<int>();
    
    var path = new List<int>();
    int cur = target;
    while (cur != -1)
    {
        path.Add(cur);
        cur = parent[cur];
    }
    path.Reverse();
    return path;
}
```

**Common pitfalls**:
- Overwriting `parent[v]` after first discovery (only set once!)
- Forgetting to reverse the reconstructed path
- Not checking if target is reachable before reconstruction

**Tips & tricks**:
- **Invariant**: When `u` is popped from queue, `dist[u]` is finalized and minimal
- Multiple shortest paths exist → `parent[]` captures one arbitrary path
- For path count, maintain `count[]` array: `count[v] += count[u]` on discovery

**LeetCode Practice**:
- 752 - Open the Lock
- 127 - Word Ladder
- 1091 - Shortest Path in Binary Matrix

---

### 3.2 Multi-Source BFS

**Why**: Problems like "distance to nearest X" or "time for infection to spread" require starting from multiple sources simultaneously rather than a single source.

**What**: Initialize the BFS queue with **all source nodes** at distance 0, then expand outward. First wave to reach a cell determines its distance.

**How (stepflow)**:
1. Enqueue **all source nodes** (e.g., all `0` cells in matrix, all rotten oranges)
2. Mark all sources with `dist = 0`
3. Run standard BFS level-by-level
4. Each level represents one unit of time/distance spreading outward

**Where**: 01 Matrix (nearest zero), Rotting Oranges (infection spread), As Far from Land as Possible.

**When**: Problem mentions "nearest X", "time for all cells to Y", or "distance from any source".

**Visual**:
```
Grid with multiple sources (O):
  . . O .
  . . . .
  O . . .

Multi-source BFS:
  Queue starts: [O₁, O₂] (both at dist=0)
  
  After 1 step (dist=1):
    1 1 O 1
    1 . . .
    O 1 . .
  
  After 2 steps (dist=2):
    1 1 O 1
    1 2 2 2
    O 1 2 .
```

**Python**:
```python
from collections import deque

def multi_source_bfs_distances(grid):
    """Find distance from each cell to nearest source (0)."""
    R, C = len(grid), len(grid[0])
    dist = [[-1] * C for _ in range(R)]
    q = deque()
    
    # Enqueue all sources
    for r in range(R):
        for c in range(C):
            if grid[r][c] == 0:
                dist[r][c] = 0
                q.append((r, c))
    
    # BFS
    directions = [(0,1), (1,0), (0,-1), (-1,0)]
    while q:
        r, c = q.popleft()
        for dr, dc in directions:
            nr, nc = r + dr, c + dc
            if 0 <= nr < R and 0 <= nc < C and dist[nr][nc] == -1:
                dist[nr][nc] = dist[r][c] + 1
                q.append((nr, nc))
    
    return dist
```

**C#**:
```csharp
public static int[][] MultiSourceBfsDistances(int[][] grid)
{
    int R = grid.Length, C = grid[0].Length;
    var dist = new int[R][];
    for (int i = 0; i < R; i++)
        dist[i] = Enumerable.Repeat(-1, C).ToArray();
    
    var q = new Queue<(int r, int c)>();
    
    // Enqueue all sources
    for (int r = 0; r < R; r++)
        for (int c = 0; c < C; c++)
            if (grid[r][c] == 0)
            {
                dist[r][c] = 0;
                q.Enqueue((r, c));
            }
    
    int[][] dirs = { new[]{0,1}, new[]{1,0}, new[]{0,-1}, new[]{-1,0} };
    while (q.Count > 0)
    {
        var (r, c) = q.Dequeue();
        foreach (var d in dirs)
        {
            int nr = r + d[0], nc = c + d[1];
            if (nr >= 0 && nr < R && nc >= 0 && nc < C && dist[nr][nc] == -1)
            {
                dist[nr][nc] = dist[r][c] + 1;
                q.Enqueue((nr, nc));
            }
        }
    }
    
    return dist;
}
```

**Common pitfalls**:
- Enqueueing sources one at a time in separate BFS calls (breaks simultaneity)
- Not initializing all sources to `dist = 0` before starting BFS
- Off-by-one errors when counting time/steps

**Tips & tricks**:
- **Invariant**: Queue holds all cells at current distance frontier
- For "minimum time" problems, track steps explicitly: freeze `level_size` at each iteration
- Works on any graph structure, not just grids

**LeetCode Practice**:
- 542 - 01 Matrix
- 994 - Rotting Oranges
- 1162 - As Far from Land as Possible

---

### 3.3 Bipartite Check (Graph Coloring)

**Why**: Many constraint problems reduce to "can we partition nodes into two groups such that no edge connects nodes in the same group?"

**What**: Attempt to 2-color the graph using BFS or DFS. Assign colors 0 and 1 such that every edge connects opposite colors. If a conflict is found (edge connecting same color), graph is not bipartite.

**How (stepflow)**:
1. Initialize `color[u] = -1` for all nodes (uncolored)
2. For each uncolored node (handle disconnected components):
   - Assign `color[u] = 0`, start BFS/DFS
   - For each edge `(u, v)`:
     - If `v` is uncolored: `color[v] = 1 - color[u]`
     - If `v` is colored and `color[v] == color[u]`: **CONFLICT** → not bipartite
3. If no conflicts found, graph is bipartite

**Where**: Is Graph Bipartite, Possible Bipartition, scheduling problems with mutual exclusion constraints.

**When**: Problem asks "can we divide into two groups such that no two in same group are connected?"

**Visual**:
```
Bipartite (square cycle):
  0 ─── 1
  |     |
  3 ─── 2

  Colors: 0→A, 1→B, 2→A, 3→B
  All edges connect A↔B ✓

Not bipartite (triangle):
  0 ─── 1
   \   /
    \ /
     2

  Try: 0→A, 1→B, 2→?
  Edge (0,2): 2→B
  Edge (1,2): conflict! (both B)
  ✗ Not bipartite
```

**Python**:
```python
from collections import deque

def is_bipartite(adj):
    n = len(adj)
    color = [-1] * n
    
    for start in range(n):
        if color[start] != -1:
            continue
        
        # BFS from this component
        color[start] = 0
        q = deque([start])
        
        while q:
            u = q.popleft()
            for v in adj[u]:
                if color[v] == -1:
                    color[v] = 1 - color[u]
                    q.append(v)
                elif color[v] == color[u]:
                    return False  # conflict
    
    return True
```

**C#**:
```csharp
public static bool IsBipartite(Dictionary<int, List<int>> adj, int n)
{
    var color = Enumerable.Repeat(-1, n).ToArray();
    
    for (int start = 0; start < n; start++)
    {
        if (color[start] != -1) continue;
        
        color[start] = 0;
        var q = new Queue<int>();
        q.Enqueue(start);
        
        while (q.Count > 0)
        {
            int u = q.Dequeue();
            foreach (int v in adj[u])
            {
                if (color[v] == -1)
                {
                    color[v] = 1 - color[u];
                    q.Enqueue(v);
                }
                else if (color[v] == color[u])
                {
                    return false;
                }
            }
        }
    }
    
    return true;
}
```

**Common pitfalls**:
- Forgetting to check all components (only checking from node 0)
- Using BFS but not marking visited → infinite loop on cycles
- Confusing bipartite with DAG (DAGs can be non-bipartite)

**Tips & tricks**:
- **Invariant**: Every edge connects nodes of opposite colors
- DFS version is cleaner but BFS version mirrors the distance-layer intuition
- Odd-length cycles → not bipartite; even-length cycles → bipartite

**LeetCode Practice**:
- 785 - Is Graph Bipartite?
- 886 - Possible Bipartition

---

## Level 4: The Constraint Layer

**Goal**: Handle structural constraints (DAG ordering, cycle detection, weighted 0/1 edges).

---

### 4.1 Topological Sort (Kahn's Algorithm)

**Why**: Many scheduling problems require processing tasks in an order that respects dependencies (prerequisite → task).

**What**: Repeatedly select nodes with **in-degree 0** (no incoming edges), remove them, and reduce in-degrees of neighbors. If all nodes are processed, a valid topological order exists. Otherwise, a cycle exists.

**How (stepflow)**:
1. Compute `indegree[v]` for all nodes
2. Enqueue all nodes with `indegree = 0`
3. While queue not empty:
   - `u = queue.pop()`, append `u` to result
   - For each edge `(u, v)`:
     - `indegree[v]--`
     - If `indegree[v] == 0`, enqueue `v`
4. If result length equals `n`, return result; else return `[]` (cycle detected)

**Where**: Course Schedule, Task Scheduling, Build Order, dependency resolution.

**When**: Problem involves "prerequisites", "before/after constraints", or "ordering tasks".

**Visual**:
```
DAG:
  0 → 1 → 3
  ↓       ↑
  2 ------+

Initial indegree: [0, 1, 1, 2]
Queue: [0]

Process 0: append 0, reduce indegree[1] and indegree[2]
  indegree: [-, 0, 0, 2]
  Queue: [1, 2]

Process 1: append 1, reduce indegree[3]
  indegree: [-, -, 0, 1]
  Queue: [2]

Process 2: append 2, reduce indegree[3]
  indegree: [-, -, -, 0]
  Queue: [3]

Process 3: append 3
  Result: [0, 1, 2, 3] ✓
```

**Python**:
```python
from collections import deque

def topo_sort_kahn(n, edges):
    adj = [[] for _ in range(n)]
    indegree = [0] * n
    
    for u, v in edges:
        adj[u].append(v)
        indegree[v] += 1
    
    q = deque([u for u in range(n) if indegree[u] == 0])
    result = []
    
    while q:
        u = q.popleft()
        result.append(u)
        for v in adj[u]:
            indegree[v] -= 1
            if indegree[v] == 0:
                q.append(v)
    
    return result if len(result) == n else []
```

**C#**:
```csharp
public static List<int> TopoSortKahn(int n, List<(int u, int v)> edges)
{
    var adj = new List<int>[n];
    for (int i = 0; i < n; i++) adj[i] = new List<int>();
    var indegree = new int[n];
    
    foreach (var (u, v) in edges)
    {
        adj[u].Add(v);
        indegree[v]++;
    }
    
    var q = new Queue<int>();
    for (int u = 0; u < n; u++)
        if (indegree[u] == 0) q.Enqueue(u);
    
    var result = new List<int>();
    while (q.Count > 0)
    {
        int u = q.Dequeue();
        result.Add(u);
        foreach (int v in adj[u])
        {
            indegree[v]--;
            if (indegree[v] == 0) q.Enqueue(v);
        }
    }
    
    return result.Count == n ? result : new List<int>();
}
```

**Common pitfalls**:
- Not checking if `result.length == n` (partial order on cyclic graph)
- Forgetting to initialize `indegree` array correctly
- Using undirected edges (topological sort is for directed graphs only)

**Tips & tricks**:
- **Invariant**: Queue holds all currently-available tasks (no pending dependencies)
- Multiple valid topological orders may exist
- For "minimum time to complete all tasks with dependencies", use BFS with time tracking

**LeetCode Practice**:
- 207 - Course Schedule (detect cycle)
- 210 - Course Schedule II (return order)
- 269 - Alien Dictionary

---

### 4.2 Topological Sort (DFS Finish Order)

**Why**: DFS-based topological sort is more intuitive for recursion-heavy scenarios and is the basis for strongly connected components (SCC) algorithms.

**What**: Run DFS and record the **finish order** (postorder) of nodes. The reverse of finish order is a valid topological ordering.

**How (stepflow)**:
1. Run DFS from all unvisited nodes
2. When a node finishes (all neighbors explored), append to `finish_order`
3. Reverse `finish_order` to get topological order
4. If cycle detected during DFS (via 3-color), return `[]`

**Where**: Course Schedule, SCC preprocessing, dependency resolution.

**When**: Prefer DFS over Kahn when recursion is natural or when building SCC algorithms.

**Visual**:
```
DAG:
  0 → 1 → 3
  ↓       ↑
  2 ------+

DFS from 0:
  Enter 0 → Enter 1 → Enter 3 → Finish 3 → Finish 1
  → Enter 2 → Finish 2 → Finish 0

Finish order: [3, 1, 2, 0]
Reverse: [0, 2, 1, 3] ✓ (valid topo order)
```

**Python**:
```python
def topo_sort_dfs(n, edges):
    adj = [[] for _ in range(n)]
    for u, v in edges:
        adj[u].append(v)
    
    WHITE, GRAY, BLACK = 0, 1, 2
    color = [WHITE] * n
    finish_order = []
    has_cycle = False
    
    def dfs(u):
        nonlocal has_cycle
        if color[u] == GRAY:
            has_cycle = True
            return
        if color[u] == BLACK:
            return
        
        color[u] = GRAY
        for v in adj[u]:
            dfs(v)
        color[u] = BLACK
        finish_order.append(u)
    
    for u in range(n):
        if color[u] == WHITE:
            dfs(u)
    
    if has_cycle:
        return []
    finish_order.reverse()
    return finish_order
```

**C#**:
```csharp
public static List<int> TopoSortDfs(int n, List<(int u, int v)> edges)
{
    var adj = new List<int>[n];
    for (int i = 0; i < n; i++) adj[i] = new List<int>();
    foreach (var (u, v) in edges) adj[u].Add(v);
    
    const int WHITE = 0, GRAY = 1, BLACK = 2;
    var color = new int[n];
    var finishOrder = new List<int>();
    bool hasCycle = false;
    
    void Dfs(int u)
    {
        if (color[u] == GRAY) { hasCycle = true; return; }
        if (color[u] == BLACK) return;
        
        color[u] = GRAY;
        foreach (int v in adj[u]) Dfs(v);
        color[u] = BLACK;
        finishOrder.Add(u);
    }
    
    for (int u = 0; u < n; u++)
        if (color[u] == WHITE) Dfs(u);
    
    if (hasCycle) return new List<int>();
    finishOrder.Reverse();
    return finishOrder;
}
```

**Common pitfalls**:
- Forgetting to reverse the finish order
- Not detecting cycles via 3-color DFS
- Mixing up WHITE/GRAY/BLACK states

**Tips & tricks**:
- **Invariant**: When node finishes, all descendants are already finished
- Finish order = postorder traversal
- For SCC (Kosaraju), you need finish order of original graph + DFS on transpose

**LeetCode Practice**:
- 207 - Course Schedule
- 210 - Course Schedule II

---

### 4.3 Directed Cycle Detection (3-Color DFS)

**Why**: Many graph problems require checking if a directed graph is acyclic (DAG). Standard visited-only tracking doesn't distinguish between back-edges and cross-edges.

**What**: Use **3 colors** during DFS:
- **WHITE (0)**: Unvisited
- **GRAY (1)**: Currently visiting (on recursion stack)
- **BLACK (2)**: Finished (all descendants explored)

A **back-edge** to a GRAY node indicates a cycle.

**How (stepflow)**:
1. Initialize all nodes to WHITE
2. For each WHITE node, run DFS:
   - Mark node GRAY (entering)
   - Recursively visit neighbors
   - If neighbor is GRAY → **cycle detected**
   - Mark node BLACK (exiting)
3. If any back-edge found, return `True` (has cycle)

**Where**: Course Schedule (cycle = impossible), Find Eventual Safe States, dependency cycle detection.

**When**: Problem asks "is there a cycle?" in directed graph.

**Visual**:
```
Cycle example:
  0 → 1 → 2
      ↑   ↓
      +---+

DFS from 0:
  Enter 0 (GRAY) → Enter 1 (GRAY) → Enter 2 (GRAY)
  → Visit 1 again: 1 is GRAY → **BACK-EDGE** → CYCLE ✗

DAG example:
  0 → 1 → 2

DFS from 0:
  Enter 0 (GRAY) → Enter 1 (GRAY) → Enter 2 (GRAY) → Finish 2 (BLACK)
  → Finish 1 (BLACK) → Finish 0 (BLACK)
  No back-edges → No cycle ✓
```

**Python**:
```python
def has_cycle_directed(adj):
    n = len(adj)
    WHITE, GRAY, BLACK = 0, 1, 2
    color = [WHITE] * n
    
    def dfs(u):
        if color[u] == GRAY:
            return True  # back-edge → cycle
        if color[u] == BLACK:
            return False
        
        color[u] = GRAY
        for v in adj[u]:
            if dfs(v):
                return True
        color[u] = BLACK
        return False
    
    for u in range(n):
        if color[u] == WHITE:
            if dfs(u):
                return True
    return False
```

**C#**:
```csharp
public static bool HasCycleDirected(Dictionary<int, List<int>> adj, int n)
{
    const int WHITE = 0, GRAY = 1, BLACK = 2;
    var color = new int[n];
    
    bool Dfs(int u)
    {
        if (color[u] == GRAY) return true;
        if (color[u] == BLACK) return false;
        
        color[u] = GRAY;
        foreach (int v in adj[u])
            if (Dfs(v)) return true;
        color[u] = BLACK;
        return false;
    }
    
    for (int u = 0; u < n; u++)
        if (color[u] == WHITE && Dfs(u)) return true;
    return false;
}
```

**Common pitfalls**:
- Using only 2 colors (visited/unvisited) → can't distinguish back-edges from cross-edges
- Forgetting to mark BLACK on exit → infinite recursion
- Checking cycle on undirected graph (use parent tracking instead)

**Tips & tricks**:
- **Invariant**: GRAY nodes form the current DFS path (recursion stack)
- For undirected graphs, use parent tracking to avoid false cycle detection on tree edges
- Combine with topological sort for "cycle or order" problems

**LeetCode Practice**:
- 207 - Course Schedule
- 802 - Find Eventual Safe States

---

### 4.4 0–1 BFS (Deque Frontier)

**Why**: When edge weights are restricted to 0 or 1, Dijkstra is overkill. 0–1 BFS achieves O(V + E) complexity using a deque.

**What**: Use a **deque** instead of queue. Push 0-weight edges to the **front** (process sooner) and 1-weight edges to the **back** (process later). This maintains monotonic distance order.

**How (stepflow)**:
1. Initialize `dist[u] = ∞` for all, `dist[src] = 0`
2. Push `src` to deque
3. While deque not empty:
   - `u = deque.pop_left()`
   - For each edge `(u, v, w)` where `w ∈ {0, 1}`:
     - If `dist[u] + w < dist[v]`:
       - `dist[v] = dist[u] + w`
       - If `w == 0`: **push_left** `v`
       - If `w == 1`: **push_right** `v`

**Where**: Minimum Cost to Make Valid Path, Minimum Obstacle Removal to Reach Corner, grid pathfinding with 0/1 costs.

**When**: Edge weights are only 0 or 1, and you need shortest path.

**Visual**:
```
Graph with 0/1 weights:
  S -0→ A -1→ C
  |           ↑
  1           0
  ↓           |
  B ----------+

Deque evolution:
  [S]  dist[S]=0
  Pop S, discover A (cost 0) and B (cost 1)
  [A, B]  dist[A]=0, dist[B]=1
  Pop A, discover C (cost 1)
  [C, B]  dist[C]=1
  Pop C, discover nothing
  [B]  dist[B]=1
  Pop B, discover nothing
  
Final: dist[C] = 1 (path S→A→C via 0+1=1)
```

**Python**:
```python
from collections import deque

def zero_one_bfs(n, edges, src, target):
    """edges = [(u, v, w)] where w in {0, 1}"""
    adj = [[] for _ in range(n)]
    for u, v, w in edges:
        adj[u].append((v, w))
        adj[v].append((u, w))  # undirected
    
    INF = float('inf')
    dist = [INF] * n
    dist[src] = 0
    dq = deque([src])
    
    while dq:
        u = dq.popleft()
        for v, w in adj[u]:
            if dist[u] + w < dist[v]:
                dist[v] = dist[u] + w
                if w == 0:
                    dq.appendleft(v)
                else:
                    dq.append(v)
    
    return dist[target] if dist[target] != INF else -1
```

**C#**:
```csharp
public static int ZeroOneBfs(int n, List<(int u, int v, int w)> edges, int src, int target)
{
    var adj = new List<(int v, int w)>[n];
    for (int i = 0; i < n; i++) adj[i] = new List<(int, int)>();
    
    foreach (var (u, v, w) in edges)
    {
        adj[u].Add((v, w));
        adj[v].Add((u, w));
    }
    
    const int INF = int.MaxValue;
    var dist = Enumerable.Repeat(INF, n).ToArray();
    dist[src] = 0;
    var dq = new LinkedList<int>();
    dq.AddLast(src);
    
    while (dq.Count > 0)
    {
        int u = dq.First.Value;
        dq.RemoveFirst();
        
        foreach (var (v, w) in adj[u])
        {
            if (dist[u] + w < dist[v])
            {
                dist[v] = dist[u] + w;
                if (w == 0)
                    dq.AddFirst(v);
                else
                    dq.AddLast(v);
            }
        }
    }
    
    return dist[target] != INF ? dist[target] : -1;
}
```

**Common pitfalls**:
- Using regular queue instead of deque → wrong order, suboptimal results
- Not checking `dist[u] + w < dist[v]` → infinite loop with cycles
- Applying to weights outside {0, 1} → incorrect results

**Tips & tricks**:
- **Invariant**: Deque pops nodes in non-decreasing distance order
- For grids: cost 0 = stay in current lane, cost 1 = turn/obstacle
- Can be extended to 0–k BFS with buckets for small k

**LeetCode Practice**:
- 1368 - Minimum Cost to Make at Least One Valid Path in a Grid
- 2290 - Minimum Obstacle Removal to Reach Corner

---

## Level 5: The Abstract Layer

**Goal**: Recognize when problems are implicitly graphs and apply graph thinking.

---

### 5.1 State-Space BFS (Implicit Graphs)

**Why**: Many puzzle and configuration problems don't provide an explicit adjacency list. Instead, nodes are **states** and edges are **legal transitions**.

**What**: Define:
- **State**: A configuration (e.g., lock combination "0000", board position)
- **Neighbors**: All states reachable via one legal move
- **Visited**: Prevent revisiting states (critical for exponential blow-up)

Run BFS on this implicit graph.

**How (stepflow)**:
1. Define state representation (string, tuple, etc.)
2. Implement `get_neighbors(state)` function
3. Run BFS:
   - Enqueue start state
   - Mark visited
   - For each state, generate neighbors on-the-fly
   - Track steps/distance as usual
4. Stop when target state is reached

**Where**: Word Ladder, Open the Lock, Sliding Puzzle, Jump Game variations, configuration spaces.

**When**: Problem involves "transformations", "moves", "configurations", or "puzzles" without explicit graph.

**Visual**:
```
Open the Lock example:
  State: "0000" (combination lock)
  Move: turn one wheel up/down
  
  "0000" has 8 neighbors:
    "1000", "9000",  (wheel 0)
    "0100", "0900",  (wheel 1)
    "0010", "0090",  (wheel 2)
    "0001", "0009"   (wheel 3)
  
  This forms an implicit graph with 10^4 nodes.
  BFS finds shortest sequence of moves.
```

**Python**:
```python
from collections import deque

def open_lock(deadends, target):
    """Shortest path from '0000' to target, avoiding deadends."""
    dead = set(deadends)
    if "0000" in dead:
        return -1
    
    visited = {"0000"}
    q = deque([("0000", 0)])
    
    while q:
        state, steps = q.popleft()
        if state == target:
            return steps
        
        # Generate neighbors
        for i in range(4):
            digit = int(state[i])
            for delta in [-1, 1]:
                new_digit = (digit + delta) % 10
                neighbor = state[:i] + str(new_digit) + state[i+1:]
                
                if neighbor not in visited and neighbor not in dead:
                    visited.add(neighbor)
                    q.append((neighbor, steps + 1))
    
    return -1
```

**C#**:
```csharp
public static int OpenLock(string[] deadends, string target)
{
    var dead = new HashSet<string>(deadends);
    if (dead.Contains("0000")) return -1;
    
    var visited = new HashSet<string> { "0000" };
    var q = new Queue<(string state, int steps)>();
    q.Enqueue(("0000", 0));
    
    while (q.Count > 0)
    {
        var (state, steps) = q.Dequeue();
        if (state == target) return steps;
        
        for (int i = 0; i < 4; i++)
        {
            int digit = state[i] - '0';
            foreach (int delta in new[] { -1, 1 })
            {
                int newDigit = (digit + delta + 10) % 10;
                var neighbor = state.Substring(0, i) + newDigit + state.Substring(i + 1);
                
                if (!visited.Contains(neighbor) && !dead.Contains(neighbor))
                {
                    visited.Add(neighbor);
                    q.Enqueue((neighbor, steps + 1));
                }
            }
        }
    }
    
    return -1;
}
```

**Common pitfalls**:
- Forgetting `visited` set → exponential time complexity
- Inefficient state representation (deep copying large structures)
- Not handling invalid states (deadends, obstacles)

**Tips & tricks**:
- **Invariant**: Visited prevents exponential explosion; first reach is optimal (BFS)
- Use immutable state representation (strings, tuples) for hashability
- For bidirectional search: run BFS from both start and target

**LeetCode Practice**:
- 752 - Open the Lock
- 127 - Word Ladder
- 773 - Sliding Puzzle
- 1091 - Shortest Path in Binary Matrix

---

### 5.2 DP on DAG (Topological Order)

**Why**: Many DP problems with dependencies form a DAG. Computing DP values in topological order ensures all dependencies are resolved before computing a node's value.

**What**: 
1. Topologically sort the DAG
2. Process nodes in topological order
3. For each node, compute DP value using already-computed neighbors

**How (stepflow)**:
1. Detect if graph is DAG (return early if cycle exists)
2. Compute topological order (Kahn or DFS)
3. Initialize DP array
4. For each node `u` in topological order:
   - `dp[u] = f(dp[neighbors of u])`
5. Return final DP values

**Where**: Longest Path in DAG, Course Schedule III, parallel task scheduling with dependencies.

**When**: Problem combines "dependencies" + "optimization" (max/min/count).

**Visual**:
```
DAG:
  0 → 1 → 3
  ↓       ↑
  2 ------+

Longest path DP:
  Topo order: [0, 1, 2, 3] or [0, 2, 1, 3]
  
  dp[0] = 0 (base)
  dp[1] = max(dp[0] + 1) = 1
  dp[2] = max(dp[0] + 1) = 1
  dp[3] = max(dp[1] + 1, dp[2] + 1) = 2
  
  Longest path: 0 → 1 → 3 (length 2)
```

**Python**:
```python
def longest_path_dag(n, edges):
    """Longest path in weighted DAG using DP."""
    adj = [[] for _ in range(n)]
    indegree = [0] * n
    
    for u, v in edges:
        adj[u].append(v)
        indegree[v] += 1
    
    # Topological sort (Kahn)
    from collections import deque
    q = deque([u for u in range(n) if indegree[u] == 0])
    topo = []
    
    while q:
        u = q.popleft()
        topo.append(u)
        for v in adj[u]:
            indegree[v] -= 1
            if indegree[v] == 0:
                q.append(v)
    
    if len(topo) != n:
        return -1  # cycle
    
    # DP in topological order
    dp = [0] * n
    for u in topo:
        for v in adj[u]:
            dp[v] = max(dp[v], dp[u] + 1)
    
    return max(dp)
```

**C#**:
```csharp
public static int LongestPathDag(int n, List<(int u, int v)> edges)
{
    var adj = new List<int>[n];
    for (int i = 0; i < n; i++) adj[i] = new List<int>();
    var indegree = new int[n];
    
    foreach (var (u, v) in edges)
    {
        adj[u].Add(v);
        indegree[v]++;
    }
    
    var q = new Queue<int>();
    for (int u = 0; u < n; u++)
        if (indegree[u] == 0) q.Enqueue(u);
    
    var topo = new List<int>();
    while (q.Count > 0)
    {
        int u = q.Dequeue();
        topo.Add(u);
        foreach (int v in adj[u])
        {
            indegree[v]--;
            if (indegree[v] == 0) q.Enqueue(v);
        }
    }
    
    if (topo.Count != n) return -1;
    
    var dp = new int[n];
    foreach (int u in topo)
        foreach (int v in adj[u])
            dp[v] = Math.Max(dp[v], dp[u] + 1);
    
    return dp.Max();
}
```

**Common pitfalls**:
- Not checking for cycles before DP
- Processing nodes in wrong order (non-topological)
- Incorrect DP recurrence relation

**Tips & tricks**:
- **Invariant**: When processing node `u`, all predecessors are finalized
- Works for longest path, path counting, resource optimization
- Can compute multiple DP arrays simultaneously (e.g., min and max paths)

**LeetCode Practice**:
- 1192 - Critical Connections in a Network
- 1203 - Sort Items by Groups Respecting Dependencies

---

## Invariant Library

Memorize these one-liners:

| Pattern | Invariant |
|---------|-----------|
| **BFS shortest path** | When `u` is popped, `dist[u]` is minimal and finalized. |
| **Multi-source BFS** | Queue holds all cells at current distance frontier. |
| **Bipartite coloring** | Every edge connects nodes of opposite colors; conflict → not bipartite. |
| **Topo sort (Kahn)** | Queue holds all nodes with in-degree 0 (no pending dependencies). |
| **Topo sort (DFS)** | Finish order reversed = valid topological order. |
| **Directed cycle (3-color)** | GRAY nodes = current recursion path; back-edge to GRAY → cycle. |
| **0–1 BFS** | Deque pops nodes in non-decreasing distance order; 0-edges front, 1-edges back. |
| **State-space BFS** | Visited prevents exponential blow-up; first reach is optimal. |
| **DP on DAG** | Process in topological order; when computing `dp[u]`, all predecessors finalized. |

---

## Quick Reference Table

| Problem Signal | Pattern | Key Data Structure | Complexity |
|----------------|---------|-------------------|------------|
| Shortest path, unweighted | BFS + parent | Queue + dist[] + parent[] | O(V + E) |
| Distance to nearest X | Multi-source BFS | Queue (all sources) | O(V + E) |
| Divide into 2 groups, no same-group edges | Bipartite coloring | Queue/Stack + color[] | O(V + E) |
| Order tasks with prerequisites | Topo sort (Kahn) | Queue + indegree[] | O(V + E) |
| Detect cycle in directed graph | 3-color DFS | Recursion + color[] | O(V + E) |
| Edge weights only 0/1 | 0–1 BFS | Deque + dist[] | O(V + E) |
| Puzzle/transformations | State-space BFS | Queue + visited set | O(states + transitions) |
| DP with dependencies (DAG) | Topo DP | Topo order + dp[] | O(V + E) |

---

## Pattern Combinations

**Real problems often combine multiple patterns:**

| Problem | Patterns Used |
|---------|---------------|
| **Course Schedule** | Topo sort (detect cycle) + Directed cycle detection |
| **Word Ladder** | State-space BFS + Shortest path |
| **Minimum Cost Path (0/1 grid)** | 0–1 BFS + Grid traversal |
| **Parallel Course III** | Topo sort + DP on DAG |
| **Find Eventual Safe States** | Directed cycle detection + DFS finish order |

---

## Final Mental Model

```
Level 3: Distance-aware patterns
  ├─ BFS shortest path (+ parent reconstruction)
  ├─ Multi-source BFS (simultaneous spreading)
  └─ Bipartite coloring (2-color constraint)

Level 4: Structural constraints
  ├─ Topological sort (Kahn BFS / DFS finish)
  ├─ Directed cycle detection (3-color DFS)
  └─ 0–1 BFS (deque optimization)

Level 5: Abstract thinking
  ├─ State-space BFS (implicit graphs)
  └─ DP on DAG (topo order computation)
```

**Master these 8 patterns and you can solve 90% of advanced graph problems.**

---

**Next steps:**
1. Implement each pattern from scratch in both Python and C#
2. Solve 2–3 practice problems per pattern
3. Recognize pattern signals in new problems
4. Combine patterns for complex scenarios

**Remember**: Traversal is about **controlled exploration with invariants**. Master the invariants, and the code writes itself.