# 🧗 Week 08 Problem Solving Roadmap – Graph Fundamentals

Version: v1.0  
Filename: `Week_08_Problem_Solving_Roadmap.md`  
Week: 08 – Graph Fundamentals: Representations, BFS, DFS & Topological Sort

---

## 🎯 1. Overall Problem-Solving Strategy for Week 08

This week’s problems revolve around **three pillars**:

1. **Modelling:** turning real problems into graphs (nodes, edges, directions, weights).  
2. **Traversals:** using **BFS** and **DFS** to explore, compute distances, detect cycles, and order tasks.  
3. **Structure:** discovering components, bipartite partitions, and SCCs.

Your strategy:
- Start each problem by writing down:
  - What is a **vertex** here?  
  - What is an **edge**? Directed or undirected? Weighted or unweighted?  
  - Is the graph **explicit** or **implicit**?
- Decide on the **pattern**:
  - Unweighted shortest path → **BFS**.  
  - Task ordering / dependencies → **Topological sort**.  
  - Reachability / components → **BFS or DFS**.  
  - 2-coloring / odd cycle detection → **Bipartite test**.  
  - Mutual reachability clusters → **SCC** (advanced).
- Choose the **representation** and **collections** accordingly:
  - Sparse graph → adjacency list.  
  - Dense + small graph → adjacency matrix.  
  - Grid or puzzle → implicit neighbors.

The goal is to **recognize the pattern in under 60 seconds** per problem.

---

## 🧩 2. Three-Stage Progression

### Stage 1 – Basic Understanding & Direct Applications

Focus: master **core skeletons** of BFS, DFS, topo sort, components, bipartite checks, and SCCs.

Target problems:
- Build graph from edge list and perform simple BFS/DFS.  
- Single-source shortest path in unweighted graphs.  
- Level-order traversal on trees via BFS.  
- Counting connected components in an undirected graph.  
- Bipartite check using BFS/DFS 2-coloring.  
- Simple DAG topological sort.

Your checklist for Stage 1:
- Can you write BFS/DFS **without looking up** the algorithm?  
- Can you trace them by hand on small custom graphs?  
- Can you quickly compute **time and space complexity**?

---

### Stage 2 – Variations & Constraints

Focus: deal with **twists** that stress-test your understanding.

Typical variations:
- Multi-source BFS (multiple starting nodes).  
- Shortest path in a grid with obstacles (implicit graph).  
- BFS in large graphs where memory matters → choose representation carefully.  
- DFS on large DAGs where recursion depth may overflow → switch to iterative.  
- Components in graphs given as **edge lists** only → first convert, then traverse.  
- Bipartite check in disconnected graphs → repeat BFS from all components.  
- Using DSU to maintain connectivity dynamically as edges are added.

Your checklist for Stage 2:
- Do you know how to **adapt BFS/DFS** to multi-source, grids, or disconnected graphs?  
- Can you reason about when to switch from recursion to an explicit stack?  
- Can you handle **input formats** that are not immediately adjacency lists?

---

### Stage 3 – Integration & Mixed Concepts

Focus: **combine** multiple Week 08 patterns and prepare for Week 09+.

Example integrated scenarios:
- Build a dependency graph of tasks, detect cycles (DFS), then produce a topological order for the acyclic case.  
- Use BFS to compute distances within each connected component, then apply additional logic per component (e.g., find diameter approximations).  
- Collapse SCCs into a component DAG and then run **topological sort** or DP on the condensation graph.  
- Combine bipartite checks with connectivity queries to analyze complex constraint systems.

Your checklist for Stage 3:
- Can you **sketch the whole pipeline** (model → represent → traverse → analyze) without getting stuck in code?  
- Can you justify each design choice (representation, traversal, pattern) clearly?

---

## 🧱 3. Pattern Templates & Pseudocode Skeletons

### 3.1 Graph Construction (Adjacency List)

**Pattern:** Build adjacency list from edge list.

```text
Input: n vertices (0..n-1), list of edges (u, v)
Output: adjacency list adj[0..n-1]

Initialize adj as array of n empty lists
For each edge (u, v):
    adj[u].add(v)
    if undirected: adj[v].add(u)
```

**When to use:** Almost always as the default for sparse graphs.

---

### 3.2 BFS – Unweighted Shortest Path

**Pattern:** Single-source BFS for distances and parents.

```text
Input: adjacency list adj, source s
Output: dist[v], parent[v]

Initialize dist[v] = INF for all v
Initialize parent[v] = -1 for all v
Initialize visited[v] = false for all v
Create empty queue Q

visited[s] = true
dist[s] = 0
Q.enqueue(s)

while Q not empty:
    u = Q.dequeue()
    for each v in adj[u]:
        if not visited[v]:
            visited[v] = true
            dist[v] = dist[u] + 1
            parent[v] = u
            Q.enqueue(v)
```

**Extensions:** multi-source BFS (enqueue all sources initially with distance 0).

---

### 3.3 DFS – Exploration & Cycle Detection (Directed)

**Pattern:** DFS with 3-state color array.

```text
Input: adjacency list adj
Output: hasCycle boolean, ordering list (optional)

state[v] = 0 for all v   # 0=unvisited,1=visiting,2=finished
hasCycle = false
order = empty list

function dfs(u):
    state[u] = 1  # visiting
    for each v in adj[u]:
        if state[v] == 0:
            dfs(v)
        else if state[v] == 1:
            hasCycle = true
    state[u] = 2  # finished
    order.append(u)  # post-order

for each vertex v:
    if state[v] == 0:
        dfs(v)

# for topological sort on DAG:
if not hasCycle:
    reverse(order)
```

---

### 3.4 Kahn’s Algorithm – Topological Sort via In-Degrees

**Pattern:** BFS-like topo sort.

```text
Input: adjacency list adj
Output: topoOrder list or failure if cycle

Compute inDegree[v] for all v
Initialize queue Q with all v where inDegree[v] == 0
Initialize topoOrder = []

while Q not empty:
    u = Q.dequeue()
    topoOrder.append(u)
    for each v in adj[u]:
        inDegree[v] -= 1
        if inDegree[v] == 0:
            Q.enqueue(v)

if topoOrder.count != n:
    # cycle exists
else:
    # topoOrder is valid
```

---

### 3.5 Connected Components (Undirected)

**Pattern:** BFS/DFS from each unvisited vertex.

```text
Initialize compId[v] = -1 for all v
currentComp = 0

for each vertex s:
    if compId[s] != -1: continue
    run BFS/DFS from s
    for every visited vertex v in this run:
        compId[v] = currentComp
    currentComp += 1
```

---

### 3.6 Bipartite Check (2-Coloring)

**Pattern:** BFS with colors 0/1.

```text
Initialize color[v] = -1 for all v

for each vertex s:
    if color[s] != -1: continue
    color[s] = 0
    enqueue s in Q
    while Q not empty:
        u = Q.dequeue()
        for each v in adj[u]:
            if color[v] == -1:
                color[v] = 1 - color[u]
                Q.enqueue(v)
            else if color[v] == color[u]:
                # not bipartite (odd cycle)
                return false
return true
```

---

### 3.7 SCC – Kosaraju (Conceptual Skeleton)

```text
Input: adjacency list adj, transpose graph adjT
Output: compId[v]

1) First DFS pass: compute finish order
visited[v] = false for all v
order = empty list

function dfs1(u):
    visited[u] = true
    for each v in adj[u]:
        if not visited[v]:
            dfs1(v)
    order.append(u)

for each vertex v:
    if not visited[v]: dfs1(v)

2) Second DFS pass on transpose graph
visited[v] = false for all v
compId[v] = -1
currentComp = 0

function dfs2(u):
    visited[u] = true
    compId[u] = currentComp
    for each v in adjT[u]:
        if not visited[v]:
            dfs2(v)

for v in order in reverse:
    if not visited[v]:
        dfs2(v)
        currentComp += 1
```

---

## ⚠️ 4. Problem-Solving Pitfalls & Fixes (5–7)

1. **Skipping the modelling step**  
   - Problem: You jump into coding without defining vertices/edges clearly.  
   - Consequence: Wrong representation, incorrect neighbors, subtle bugs.  
   - Fix: Always spend 1–2 minutes writing “Nodes = …, Edges = …, Directed? Weighted?” before coding.

2. **Using BFS where DFS/topo is required (and vice versa)**  
   - Problem: You choose BFS for dependency ordering or DFS for shortest unweighted paths.  
   - Fix: Repeat to yourself: *BFS = distances; DFS + topo = dependencies/structure*.

3. **Ignoring disconnected components**  
   - Problem: BFS or DFS from node 0 and assume you covered everything.  
   - Fix: Wrap traversals in an outer loop; test on graphs with multiple components.

4. **Not handling cycles in topological sort problems**  
   - Problem: You assume the graph is acyclic; algorithm fails or returns incomplete order.  
   - Fix: Always integrate **cycle detection** into topo sort and explain what happens in cyclic cases.

5. **Mismanaging visited state across test cases**  
   - Problem: Reusing global visited arrays/containers without clearing them.  
   - Fix: Reinitialize all graph-related arrays per test case; watch for stale state.

6. **Incorrect neighbor generation in implicit graphs (grids)**  
   - Problem: Off-by-one or forgetting boundary checks; accessing invalid indices.  
   - Fix: Centralize neighbor logic in a helper; test on small grids with edges.

7. **Assuming any cycle breaks bipartiteness**  
   - Problem: You mark even cycles as invalid.  
   - Fix: Remember: only **odd** cycles break bipartiteness; 4-cycles are fine.

---

## 🧮 5. Decision Matrix – Which Approach When?

| Problem Signal                                              | Representation           | Core Pattern / Algorithm      |
| ----------------------------------------------------------- | ------------------------ | ----------------------------- |
| “Unweighted shortest path, min number of steps/hops”        | Adjacency list / grid    | BFS                           |
| “Check if tasks can be scheduled given prerequisites”       | Directed adjacency list  | DFS + topo sort / Kahn        |
| “Count groups / islands / clusters”                         | Undirected adjacency list| BFS/DFS components; optional DSU |
| “Check if we can split into two non-conflicting groups”     | Undirected adjacency list| BFS/DFS bipartite check       |
| “Analyze circular dependencies / mutual reachability”       | Directed adjacency list  | SCC (Kosaraju/Tarjan)         |
| “Grid/maze navigation with walls and moves N/E/S/W”         | Implicit grid neighbors  | BFS (unweighted)              |

Before you write code, locate your problem in this table or extend it with your own rows.

---

## ✅ 6. Weekly Problem-Solving Goals

By the end of Week 08, try to achieve:

- **Stage 1:**  
  - 2–3 BFS shortest-path problems (including one grid-based).  
  - 2 DFS/topological sort problems.  
  - 1–2 connectivity/bipartite problems.

- **Stage 2:**  
  - 1 multi-source BFS problem.  
  - 1 topo-sort problem with cycle detection.  
  - 1 DSU-based connectivity problem.

- **Stage 3:**  
  - 1 problem combining SCC + topo or connectivity + BFS distances.

Use this roadmap as your **checklist** when selecting problems from LeetCode, Codeforces, or any other platform.
