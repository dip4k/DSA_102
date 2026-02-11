# 🧭 Graph Traversal Mastery — Unified Guide (DFS/BFS → Advanced)

## How to use this guide
- For any graph problem, write a 1‑line contract: **Frontier + Visit timing + State**.
- Frontier choices: stack (DFS), queue (BFS), deque (0–1 BFS), min-heap (Dijkstra), DSU (connectivity offline/online).
- State choices: visited/color, parent, distance, entry/exit times, indegree, component id.

---

## Level map (L1 → L8)

### L1 — Graph fundamentals (representation + invariants)
**You must be fluent in:**
- Graph types: undirected vs directed; weighted vs unweighted; simple vs multigraph; connected vs disconnected.
- Representations:
  - Adjacency list (default): `adj[u] = [(v,w)]` or `adj[u] = [v]`.
  - Adjacency matrix (dense graphs).
  - Edge list (Kruskal, input formats).
- Determinism policy (recommended for practice): always iterate nodes `0..n-1`, and sort adjacency lists when output ordering matters.

**Core invariants:**
- Undirected traversal must avoid walking back: use `visited[]` or parent skipping.
- Directed traversal must not “parent-skip”; cycles can occur via other edges.

---

### L2 — BFS mastery (queue frontier)
**What BFS gives you:**
- Shortest path lengths in **unweighted** graphs.
- Level layering (distance from source).

**Templates to master:**
- BFS distances: `dist[src]=0`, push `src`, relax neighbors once.
- BFS parent reconstruction: set `parent[v]=u` the first time you discover `v`.
- Multi-source BFS: initialize queue with multiple sources at distance 0.

**Pitfalls:**
- Not marking visited at enqueue time (causes duplicates and incorrect parents).
- Forgetting disconnected components (BFS from one node doesn’t cover all nodes).

---

### L3 — DFS mastery (stack/recursion frontier)
**What DFS gives you:**
- Component discovery, cycle detection structure, ordering by finish times.

**Templates to master:**
- Recursive DFS (with `visited[]`).
- Iterative DFS (explicit stack).
- Entry/exit timestamps (`tin/tout`) for subtree-like reasoning in rooted graphs.

**Pitfalls:**
- Recursion depth in large graphs (switch to iterative if needed).

---

### L4 — Components + bipartite + cycle detection
**Connected components (undirected):** BFS/DFS from each unvisited node.

**Cycle detection:**
- Undirected: DFS with parent OR BFS with parent.
- Directed: DFS colors (0=unvisited,1=visiting,2=done) or recursion stack set.

**Bipartite check:**
- BFS coloring (two colors). A conflict edge => not bipartite.

---

### L5 — DAG ordering (topological sort)
Two canonical approaches:
- **Kahn’s algorithm**: indegree + queue; for lexicographically smallest order use a min-heap.
- **DFS finish order**: push node to output on EXIT; reverse at end.

Use topo sorting for:
- course scheduling, build systems, dependency ordering, DAG DP.

---

### L6 — Shortest paths: choose the right frontier
**Frontier chooser:**
- BFS queue: all edges weight 1 (or equal).
- Deque: edges weights are 0 or 1 (0–1 BFS).
- Min-heap: nonnegative weights (Dijkstra).

**0–1 BFS invariant:**
- When relaxing edge weight 0, push_front; weight 1, push_back.

**Dijkstra invariant:**
- Pop the smallest tentative distance; skip stale heap entries.

---

### L7 — Graph decomposition (SCC)
Strongly Connected Components (directed):
- Kosaraju (2 passes: order by finish time on original, then DFS on reversed graph).
- Tarjan (1 pass using stack + lowlink).

Use SCC for:
- condensation DAG, reasoning about cycles at component level.

---

### L8 — Spanning structures + offline connectivity
**MST:**
- Kruskal: sort edges by weight + DSU.
- Prim: heap frontier (like Dijkstra but for MST).

**DSU (Union-Find):**
- Fast connectivity queries with path compression + union by size/rank.

---

## Debug checklist (universal)
- Did I choose the correct frontier (stack/queue/deque/heap)?
- When exactly do I mark visited (enqueue/push vs pop)?
- What is my tie-break policy (sorted neighbors, smallest parent)?
- For directed graphs: am I using color states for cycles?
- For weighted: am I accidentally using BFS where Dijkstra is required?

---

## Minimal templates (pseudocode)

### BFS distances (unweighted)
```text
q = queue()
dist = [-1]*n
dist[src]=0
q.push(src)
while q:
  u=q.pop()
  for v in adj[u]:
    if dist[v]==-1:
      dist[v]=dist[u]+1
      parent[v]=u
      q.push(v)
```

### Directed cycle (DFS colors)
```text
color[u]=1  // visiting
for v in adj[u]:
  if color[v]==0 and dfs(v): return true
  if color[v]==1: return true
color[u]=2
return false
```

### 0–1 BFS
```text
dist = [inf]*n
deque = [(src)]
dist[src]=0
while deque:
  u = pop_front()
  for (v,w) in adj[u]:  // w in {0,1}
    if dist[u]+w < dist[v]:
      dist[v]=dist[u]+w
      if w==0: push_front(v)
      else: push_back(v)
```

### Dijkstra
```text
dist=[inf]*n
heap.push((0,src))
dist[src]=0
while heap:
  (d,u)=heap.pop_min()
  if d!=dist[u]: continue
  for (v,w) in adj[u]:
    if d+w < dist[v]:
      dist[v]=d+w
      parent[v]=u
      heap.push((dist[v],v))
```
