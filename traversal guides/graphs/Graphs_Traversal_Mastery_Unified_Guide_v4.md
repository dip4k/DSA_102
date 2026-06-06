# 🕸️ Flow‑Wise Graph Traversal Mastery — Final Unified Guide

**Goal:** Professional-grade intuition for graph traversal and search from **basic DFS/BFS** to **advanced graph algorithms**. [file:198]

**Teaching standard:** every pattern is taught with **Why / What / How / Where / When**, a short **step/flow**, a tiny **visual**, **Python + C# templates**, plus **pitfalls** and **tips**. [file:198]

---

# 🧭 One‑page Level Mapping Index (Graphs)
Use this to map any problem to the right **frontier**, **metadata**, and **invariant**.

## Legend
- **Invariant** = what must stay true after each step.
- **Visited / Color** = prevents repeats and detects cycles.
- Frontier types: **Stack (DFS)**, **Queue (BFS)**, **Deque (0–1 BFS)**, **Min‑heap (Dijkstra/Prim)**.

| Level | Pattern / skill | Typical problems (examples) | Expected invariant (one‑liner) |
|---|---|---|---|
| L1 | Representation + visited discipline | Path exists, Keys & Rooms, Flood Fill | “Visited is exactly the discovered region; nodes enter visited once.” |
| L1 | DFS recursion skeleton | Islands, Reachability | “dfs(u) explores everything reachable from u without revisiting.” |
| L1 | BFS skeleton | Shortest steps (unweighted), Spread problems | “Queue holds the frontier; first discovery is minimal distance.” |
| L2 | Iterative DFS (stack frames) | Large depth graphs, Clone Graph | “Stack simulates recursion; marking time is controlled and explicit.” |
| L2 | Connected components (outer loop) | Provinces, Count groups | “Each new start discovers exactly one component.” |
| L3 | BFS shortest path + parent | Word Ladder, Open the Lock | “Parent set on discovery reconstructs a shortest path.” |
| L3 | Multi‑source BFS | 01 Matrix, Rotting Oranges | “All sources start at dist=0; waves expand together.” |
| L3 | Bipartite (2‑color BFS/DFS) | Possible Bipartition | “All edges connect opposite colors; any conflict breaks bipartite.” |
| L4 | Topological sort (Kahn / DFS finish) | Course Schedule, build dependencies | “Every edge u→v respects u before v; if impossible, cycle exists.” |
| L4 | Cycle detection (directed colors / undirected parent) | Detect cycles | “Directed: back-edge to gray; Undirected: edge to visited≠parent.” |
| L4 | 0–1 BFS | Min obstacles / min cost with 0/1 weights | “Deque pops increasing cost: 0-edges to front, 1-edges to back.” |
| L5 | State‑space (implicit graphs) | Lock puzzles, grid states | “State key is unique; visited prevents exponential blow‑up.” |
| L6 | Dijkstra (nonnegative weights) | Network delay, weighted grids | “Heap pops smallest settled distance; stale heap entries skipped.” |
| L7 | SCC (Kosaraju/Tarjan) | Condensation DAG, cycles at scale | “Nodes in same SCC mutually reachable; SCC graph is a DAG.” |
| L7 | Bridges / Articulation points | Critical connections | “Lowlink detects edges whose removal disconnects components.” |
| L8 | MST (Kruskal/Prim) + DSU | Minimum connection cost | “Pick cheapest edges without cycles; DSU maintains components.” |

---

# L1 — Physical Layer: Basic Movement
**Goal:** muscle memory for moving through graph memory safely (neighbors/edges) with minimal bugs. [file:198]

> At L1 your job is not “know 20 algorithms.”
> Your job is to master 4 primitives: **iterate neighbors**, **control boundaries**, **mark visited correctly**, **maintain a frontier**. [file:198]

---

## L1A) Master the loop (forward / backward / stride)

### L1A.1 Forward scan over nodes (disconnected graphs)
**Why:** Most inputs are not guaranteed connected; you must start traversals from every unvisited node. [file:198]

**What:** Outer loop launches BFS/DFS; each launch discovers one component.

**How (step/flow):**
1) `for u in 0..n-1`
2) if `!visited[u]`: `components++`, run traversal from u

**Where:** components count, connectivity check, bipartite across components, topo init.

**When:** whenever the prompt doesn’t explicitly say “connected graph”.

**Visual**
```text
Component A: 0 -- 1
Component B: 2 -- 3

Loop starts traversal at 0 -> visits {0,1}
Loop starts traversal at 2 -> visits {2,3}
```

**Python**
```python
components = 0
for u in range(n):
    if not visited[u]:
        components += 1
        bfs(u)  # or dfs(u)
```

**C#**
```csharp
int components = 0;
for (int u = 0; u < n; u++)
{
    if (!visited[u])
    {
        components++;
        Bfs(u); // or Dfs(u)
    }
}
```

**Common pitfalls**
- Starting only from node 0.
- Forgetting to reset arrays between test cases.

**Tip**
- Say it out loud: “The outer loop handles disconnected graphs.”

---

### L1A.2 Backward scan (rollback metadata)
**Why:** Paths and finish orders are naturally produced backwards (target→source, finish stack→reverse). [file:198]

**What:** Reverse a produced list once.

**How (step/flow):**
1) produce list in reverse order
2) reverse list once at end

**Where:** parent path reconstruction, topo by DFS finish order, SCC (Kosaraju order).

**Pitfalls**
- Reversing repeatedly inside a loop.

---

### L1A.3 Stride iteration (BFS levels)
**Why:** “minutes / steps / levels” means you must process exactly one BFS layer at a time. [file:198]

**What:** Freeze `levelSize = len(queue)` and process exactly that many pops.

**How (step/flow):**
1) `levelSize = len(q)`
2) repeat `levelSize` pops; enqueue neighbors
3) that completes one layer

**Where:** time-to-spread, distance k, shortest moves.

**Pitfalls**
- Using `for _ in range(len(q))` while `q` grows.

---

## L1B) Boundary control (graph edition)

### L1B.1 Node ID boundaries
**Why:** Many runtime bugs are invalid node indices / wrong allocation sizes. [file:198]

**What:** Treat node range as half-open: `[0, n)`.

**How (step/flow):**
1) allocate arrays length n
2) loop `u in [0,n)`
3) validate/normalize 1-based input once

**Pitfalls**
- Python aliasing: `adj = [[]] * n` (wrong).

---

### L1B.2 Grid bounds gate (implicit graphs)
**Why:** Grid traversal is just graph traversal plus strict bounds/blocked/visited gates. [file:198]

**Gate order:** bounds → blocked → visited → then enqueue/recurse.

**Python**
```python
def inb(r, c):
    return 0 <= r < R and 0 <= c < C
```

**C#**
```csharp
static bool InB(int r, int c, int R, int C)
    => 0 <= r && r < R && 0 <= c && c < C;
```

---

## L1C) Edge cases
- **No edges**: every node is its own component.
- **Self-loop / parallel edges**: traversal still terminates with visited; cycle definitions depend on the problem.

---

## L1D) Lockstep frontier + metadata
**Why:** Correctness comes from updating frontier and arrays together. [file:198]

When discovering `v` from `u` in BFS:
- `visited[v] = true`
- `dist[v] = dist[u] + 1`
- `parent[v] = u`
- enqueue `v`

---

# BFS vs DFS — Decision Rules + Micro‑Trace

## The one-line difference
- **BFS** = queue wave; best for shortest edges in unweighted graphs.
- **DFS** = stack dive; best for structure and full exploration.

## Frontier picture
```text
BFS frontier (queue):      DFS frontier (stack):
front -> [a b c] -> back   bottom -> [a b c] -> top

BFS processes a then b      DFS processes c first
```

## Pitfalls to memorize
- BFS: mark visited at **enqueue**, not dequeue.
- DFS: recursion depth may overflow; use iterative stack when needed.

---

# L2 — Structural Layer: Iterative DFS + Components

## L2.1 Iterative DFS (stack)
**Why:** Avoid recursion depth limits and control timing explicitly. [file:198]

**What:** Stack holds pending nodes.

**How (step/flow):**
1) push start; mark visited
2) pop; push undiscovered neighbors

**Python**
```python
def dfs_iter(start, adj):
    st = [start]
    visited[start] = True
    while st:
        u = st.pop()
        for v in adj[u]:
            if not visited[v]:
                visited[v] = True
                st.append(v)
```

**C#**
```csharp
void DfsIter(int start)
{
    var st = new Stack<int>();
    st.Push(start);
    visited[start] = true;

    while (st.Count > 0)
    {
        int u = st.Pop();
        foreach (var v in adj[u])
            if (!visited[v]) { visited[v] = true; st.Push(v); }
    }
}
```

**Pitfall**
- marking visited on pop causes duplicates.

---

## L2.2 Connected components
**Invariant:** Each BFS/DFS launch discovers exactly one component.

---

# L3 — Frontier Layer: Shortest Paths (Unweighted) + Bipartite

## L3.1 BFS distances + parent path
**Why:** Unweighted shortest path equals the fewest edges. [file:198]

**What:** `dist[]` initialized to -1; set on discovery.

**Python**
```python
from collections import deque

def bfs_path(adj, src, dst):
    n = len(adj)
    dist = [-1]*n
    parent = [-1]*n
    q = deque([src])
    dist[src] = 0
    parent[src] = src

    while q:
        u = q.popleft()
        for v in adj[u]:
            if dist[v] != -1:
                continue
            dist[v] = dist[u] + 1
            parent[v] = u
            q.append(v)

    if dist[dst] == -1:
        return -1, []

    path = []
    cur = dst
    while cur != src:
        path.append(cur)
        cur = parent[cur]
    path.append(src)
    path.reverse()
    return dist[dst], path
```

**C#**
```csharp
(int dist, List<int> path) BfsPath(List<int>[] adj, int src, int dst)
{
    int n = adj.Length;
    var dist = Enumerable.Repeat(-1, n).ToArray();
    var parent = Enumerable.Repeat(-1, n).ToArray();
    var q = new Queue<int>();

    dist[src] = 0;
    parent[src] = src;
    q.Enqueue(src);

    while (q.Count > 0)
    {
        int u = q.Dequeue();
        foreach (var v in adj[u])
        {
            if (dist[v] != -1) continue;
            dist[v] = dist[u] + 1;
            parent[v] = u;
            q.Enqueue(v);
        }
    }

    if (dist[dst] == -1) return (-1, new List<int>());

    var path = new List<int>();
    int cur = dst;
    while (cur != src) { path.Add(cur); cur = parent[cur]; }
    path.Add(src);
    path.Reverse();
    return (dist[dst], path);
}
```

---

## L3.2 Multi-source BFS
**Invariant:** initial queue contains *all* sources at distance 0; BFS waves set minimal distance.

---

## L3.3 Bipartite check (2-color)
**Invariant:** every edge connects opposite colors; conflict => not bipartite.

**Python**
```python
from collections import deque

def is_bipartite(n, edges):
    adj = [[] for _ in range(n)]
    for u,v in edges:
        adj[u].append(v)
        adj[v].append(u)
    for u in range(n):
        adj[u].sort()

    color = [-1]*n
    for s in range(n):
        if color[s] != -1:
            continue
        color[s] = 0
        q = deque([s])
        while q:
            u = q.popleft()
            for v in adj[u]:
                if color[v] == -1:
                    color[v] = color[u] ^ 1
                    q.append(v)
                elif color[v] == color[u]:
                    return False
    return True
```

**C#**
```csharp
bool IsBipartite(int n, (int u,int v)[] edges)
{
    var adj = new List<int>[n];
    for (int i = 0; i < n; i++) adj[i] = new List<int>();
    foreach (var (u,v) in edges) { adj[u].Add(v); adj[v].Add(u); }
    for (int i = 0; i < n; i++) adj[i].Sort();

    var color = Enumerable.Repeat(-1, n).ToArray();
    var q = new Queue<int>();

    for (int s = 0; s < n; s++)
    {
        if (color[s] != -1) continue;
        color[s] = 0;
        q.Enqueue(s);
        while (q.Count > 0)
        {
            int u = q.Dequeue();
            foreach (var v in adj[u])
            {
                if (color[v] == -1)
                {
                    color[v] = color[u] ^ 1;
                    q.Enqueue(v);
                }
                else if (color[v] == color[u]) return false;
            }
        }
    }
    return true;
}
```

---

# L4 — Constraint Layer: Topo, Cycles, 0–1 BFS

## L4.1 Topological sort (Kahn)
**Invariant:** queue contains only nodes with indegree 0; every pop reduces indegrees of outgoing neighbors.

**Lexicographically smallest order:** use a min-heap instead of a queue.

---

## L4.2 Cycle detection

### Directed cycle detection (colors)
- 0 = unvisited
- 1 = visiting (gray)
- 2 = done (black)

**Invariant:** any edge to a gray node is a back-edge → cycle.

### Undirected cycle detection (parent)
**Invariant:** a visited neighbor that is not the parent implies a cycle.

---

## L4.3 0–1 BFS (deque)
**When to use:** weights are only 0 or 1.

**Invariant:** deque processes nodes in nondecreasing distance due to push_front/push_back rule.

---

# L5 — Abstract Layer: Implicit Graphs (State‑Space)

## L5.1 Define state + transitions
**Why:** Many problems do not give adjacency; you generate neighbors.

**What:**
- state: minimal representation (string/tuple/int)
- transitions: legal moves
- visited key: prevents repeats

**Pitfall:** visited key too large (slow) or too small (wrong merges).

---

# L6 — Weighted Shortest Paths: Dijkstra

## L6.1 Dijkstra (nonnegative weights)
**Why:** BFS fails once weights differ; Dijkstra handles nonnegative weights.

**Invariant:** when you pop (d,u) and d == dist[u], dist[u] is finalized.

**Python**
```python
import heapq

def dijkstra(n, edges, src):
    adj = [[] for _ in range(n)]
    for u,v,w in edges:
        adj[u].append((v,w))
        adj[v].append((u,w))
    for u in range(n):
        adj[u].sort()

    INF = 10**18
    dist = [INF]*n
    dist[src] = 0
    pq = [(0, src)]

    while pq:
        d,u = heapq.heappop(pq)
        if d != dist[u]:
            continue
        for v,w in adj[u]:
            nd = d + w
            if nd < dist[v]:
                dist[v] = nd
                heapq.heappush(pq, (nd, v))
    return dist
```

**C#**
```csharp
List<int> Dijkstra(int n, (int u,int v,int w)[] edges, int src)
{
    var adj = new List<(int v,int w)>[n];
    for (int i = 0; i < n; i++) adj[i] = new List<(int,int)>();
    foreach (var (u,v,w) in edges)
    {
        adj[u].Add((v,w));
        adj[v].Add((u,w));
    }
    for (int i = 0; i < n; i++) adj[i].Sort((a,b) => a.v != b.v ? a.v.CompareTo(b.v) : a.w.CompareTo(b.w));

    const int INF = int.MaxValue/4;
    var dist = Enumerable.Repeat(INF, n).ToArray();
    dist[src] = 0;

    var pq = new PriorityQueue<int, int>();
    pq.Enqueue(src, 0);

    while (pq.Count > 0)
    {
        pq.TryDequeue(out int u, out int d);
        if (d != dist[u]) continue; // stale
        foreach (var (v,w) in adj[u])
        {
            int nd = d + w;
            if (nd < dist[v])
            {
                dist[v] = nd;
                pq.Enqueue(v, nd);
            }
        }
    }

    return dist.ToList();
}
```

---

# L7 — Decomposition: SCC + Bridges

## L7.1 SCC (Strongly Connected Components)

### Kosaraju (two-pass)
**Why:** compress cycles into components; the SCC condensation graph is a DAG.

**Step/flow:**
1) DFS on original: push node to `order` on exit
2) reverse graph
3) process nodes in reverse `order`, DFS on reversed to form SCCs

**Invariant:** nodes reached in a second-pass DFS form exactly one SCC.

---

## L7.2 Bridges (Tarjan lowlink)
**Why:** find edges whose removal disconnects the graph.

**Definitions:**
- `tin[u]`: discovery time
- `low[u]`: smallest `tin` reachable from u (via tree edges + back edges)

**Bridge rule:** for tree edge u—v, if `low[v] > tin[u]` then (u,v) is a bridge.

**Pitfalls**
- forgetting parent-skip in undirected DFS.

---

# L8 — Spanning Structures: MST + DSU

## L8.1 DSU (Union-Find)
**Invariant:** `find(x)` returns representative; union merges sets; path compression makes it fast.

## L8.2 Kruskal MST
**Invariant:** sort edges by weight; add an edge iff it connects two different DSU components.

## L8.3 Prim MST
**Invariant:** heap frontier expands a growing tree by always taking the cheapest outgoing edge.

---

# 🧰 Invariant library (memorize)
- DFS: “when dfs(u) returns, all reachable from u (within the allowed graph) are visited.” [file:198]
- BFS: “queue is the frontier; first discovery is minimal distance in unweighted graphs.” [file:198]
- Components: “outer loop starts one traversal per component.” [file:198]
- Kahn topo: “frontier is indegree‑0 nodes only.”
- 0–1 BFS: “deque order preserves shortest distances for 0/1 weights.”
- Dijkstra: “pop-min with stale-skip finalizes dist.”
- SCC: “SCC condensation is a DAG.”
- Bridges: “lowlink reveals critical edges.”
- MST: “no cycles; minimal total weight.”

---

# ✅ Pitfall checklist (before you debug)
- Did I choose the correct frontier (queue / stack / deque / heap)?
- Did I mark visited at enqueue/push (BFS/DFS iterative) and only once?
- For undirected cycle/bridges: did I skip the parent edge?
- For weighted graphs: did I accidentally use BFS instead of Dijkstra/0–1 BFS?
- For grids: did I apply the bounds/blocked/visited gates in the right order?
- For state graphs: is my visited key correct and minimal?

---

# L9 — Advanced Weighted, Matching, and Flow Extensions

Use this layer after L1-L8 are stable. These are not "basic traversal" anymore, but they reuse the same frontier + metadata + invariant discipline.

## L9.1 Bellman-Ford
- Use when edges can be negative and you still need shortest paths.
- Frontier idea: not a queue frontier, but repeated edge-relaxation passes.
- Invariant: after `i` full passes, all shortest paths using at most `i` edges are correct.
- Key interview edge case: one extra pass that still improves distance means a reachable negative cycle exists.

## L9.2 Floyd-Warshall
- Use for all-pairs shortest paths on small dense graphs.
- State view: `dist[i][j]` improves as more intermediate vertices are allowed.
- Invariant: after considering vertices `0..k`, `dist[i][j]` is the best path using only those intermediates.

## L9.3 A* Search
- Use when you have a good admissible heuristic and want practical shortest-path speedups.
- Frontier: min-heap keyed by `g + h`.
- Invariant: heuristic must never overestimate the remaining cost.

## L9.4 Dinic Max Flow
- Use for max-flow problems once the graph becomes clearly about capacities, source, sink, and residual edges.
- Frontier layers still matter: BFS builds the level graph; DFS sends blocking flow.
- Invariant: residual graph always reflects remaining augmenting capacity.

## L9.5 Hopcroft-Karp
- Use for maximum bipartite matching once BFS/DFS and bipartite graphs are already comfortable.
- Invariant: BFS layers identify shortest augmenting paths; DFS augments only along valid layered paths.

## L9.6 Other advanced graph extensions
- `Should`: shortest path with exactly K edges, multi-source Dijkstra, DAG DP for multiple best paths.
- `Optional`: 2-SAT via SCC, Eulerian path, min-cut side recovery, minimum vertex cover in bipartite graphs.

---

# Practice Ladder — Foundational to Advanced

## Core traversal drills (Must)

### L1-L2 foundations
- Path existence / Keys and Rooms / Flood Fill
- Number of Islands / Max Area of Island
- Number of Provinces / connected components
- Clone Graph / iterative DFS discipline

### L3 shortest path and coloring
- Open the Lock
- Word Ladder
- 01 Matrix
- Rotting Oranges
- Is Graph Bipartite?
- Possible Bipartition

### L4 DAG / cycle / constrained frontier
- Course Schedule
- Course Schedule II
- Find Eventual Safe States
- 0-1 BFS style problems such as minimum obstacles / directional grid cost

### L5-L8 deeper graph mastery
- Shortest Path in Binary Matrix
- Network Delay Time
- Path With Minimum Effort
- Critical Connections in a Network
- MST problems (Connecting Cities, Min Cost to Connect All Points)

## Advanced drill ladder (Should / Optional)
- Bellman-Ford distances and path reconstruction
- Floyd-Warshall distances plus next-matrix path recovery
- Dijkstra with path counts and lexicographic tie-breaking
- Multi-source Dijkstra
- A* on grid
- Dinic max flow and min cut side extraction
- Hopcroft-Karp maximum matching
- 2-SAT with SCC
- Eulerian path in directed and undirected graphs

---

# Driver-Based Drill Track

If you want coding repetition rather than theory review, use this sequence while reading the guide:

## Foundational driver drills
- Build adjacency lists (directed and undirected)
- BFS order, DFS recursive order, DFS iterative order
- Reachability, shortest path reconstruction, connected components
- Cycle detection (directed and undirected)
- Bipartite checking, topological sort, 0-1 BFS, Dijkstra, SCC, bridges, MST, DSU queries

## Advanced driver drills
- Bellman-Ford and negative cycles
- Floyd-Warshall with reconstruction
- Dijkstra variants
- A* search
- Dinic max flow
- Hopcroft-Karp matching
- Exact-K-edge shortest path, DAG DP, offline DSU, Eulerian path

Rule of thumb:
- `Must`: complete the foundational driver drills.
- `Should`: add Bellman-Ford, Floyd-Warshall, and one flow/matching drill.
- `Optional`: finish the full advanced pack.

---

# Fluent Learning Workflow

Use this single-file workflow so you do not need separate graph mastery, visual, and drill documents:

1. Read the level section and say the invariant aloud.
2. Dry-run the tiny visual before touching code.
3. Implement the template in one language.
4. Solve one LeetCode-style problem from the matching practice ladder.
5. Write one sentence explaining why the chosen frontier was correct.
6. Only then move to the next level.

This guide is now the canonical merged source for graph traversal learning, practice sequencing, advanced extensions, and visual reinforcement.
