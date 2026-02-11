# 🕸️ Flow-Wise Graph Traversal Mastery (v1)
**Goal:** Professional-grade intuition for graph traversal and search (Level 1 → Level 5).  
**Standards (same as your previous guides):** Why / What / How / Where / When + step/flow + lots of visuals + Python/C# snippets + common pitfalls + tips/tricks + invariants + mapped practice.

---

# 🧭 One-page Level Mapping Index (Graphs)
Use this to instantly map a graph problem to the right **level**, **pattern**, and **expected invariant**.

## ✅ Legend
- 🧾 **Invariant** = what stays true after each step.
- 🧠 **Visited** = prevents cycles and repeated work.
- 🧺 **Queue** = BFS frontier (wave).
- 🎒 **Stack** = DFS frontier (depth-first).

| Level | Pattern / skill | Typical LeetCode problems (examples) | Expected invariant (one-liner) |
|---|---|---|---|
| L1 | Graph representation + visited discipline | 1971 Find if Path Exists, 841 Keys and Rooms | “Every node is marked visited at most once; visited set matches explored region.” |
| L1 | DFS recursion skeleton | 200 Number of Islands, 695 Max Area of Island | “dfs(u) fully explores u’s connected region without revisiting nodes.” |
| L1 | BFS skeleton | 733 Flood Fill, 994 Rotting Oranges | “Queue contains exactly the frontier; every enqueued node is newly discovered.” |
| L2 | Iterative DFS (stack) | 133 Clone Graph, 547 Number of Provinces | “Stack represents pending nodes; visited prevents repeats; pop order doesn’t break correctness.” |
| L2 | Connected components | 323 Number of Connected Components (if available), 547 Provinces | “Each new DFS/BFS starting node discovers exactly one component.” |
| L3 | Shortest path in unweighted graphs (BFS + parent) | 752 Open the Lock, 127 Word Ladder | “First time you pop/visit a node is the shortest distance from source.” |
| L3 | Multi-source BFS | 542 01 Matrix, 994 Rotting Oranges | “Queue starts with all sources at distance 0; distances expand outward in layers.” |
| L3 | Bipartite check (BFS coloring) | 785 Is Graph Bipartite, 886 Possible Bipartition | “Each edge connects opposite colors; conflict implies not bipartite.” |
| L4 | Topological sort (DAG) | 207 Course Schedule, 210 Course Schedule II | “Order respects edges u->v: u appears before v; cycle means impossible.” |
| L4 | Cycle detection (directed/undirected) | 684 Redundant Connection, 802 Find Eventual Safe States | “A back-edge (or color conflict) implies a cycle; parent edge ignored in undirected.” |
| L4 | 0-1 BFS / weighted frontier tricks | 1368 Min Cost Valid Path, 2290 Min Obstacle Removal | “Deque pops smallest current cost first; edges 0 go front, edges 1 go back.” |
| L5 | State-space graphs (implicit nodes) | 127 Word Ladder, 1091 Shortest Path in Binary Matrix | “A ‘state’ is a node; neighbors are generated; visited prevents exponential blow-up.” |
| L5 | Graph DP on DAG | 797 All Paths From Source to Target, 1203 Sort Items by Groups | “dp[u] is final once all outgoing dependencies computed (topo order).” |

---

# 🧠 The ladder: complexity of state
- **Level 1:** Just traverse safely: adjacency + visited + base templates.
- **Level 2:** Control stack/iteration + discover components.
- **Level 3:** Add distance layers + parents for shortest paths (unweighted).
- **Level 4:** Add structure constraints: DAG ordering, cycles, 0-1 frontier.
- **Level 5:** Turn problems into graphs (implicit/state graphs) + DP-on-graph thinking.

---

# 🧱 Graph representations (baseline)

## 0.1 Adjacency list (most common)
**Why:** Space-efficient for sparse graphs, easy traversal.

**Visual**
```text
Edges: 0-1, 0-2, 1-3
Adj:
0: [1,2]
1: [0,3]
2: [0]
3: [1]
```

### 🐍 Python
```python
adj = [[] for _ in range(n)]
for u, v in edges:
    adj[u].append(v)
    adj[v].append(u)  # omit for directed graphs
```

### 🟦 C#
```csharp
var adj = new List<int>[n];
for (int i = 0; i < n; i++) adj[i] = new List<int>();
foreach (var (u, v) in edges)
{
    adj[u].Add(v);
    adj[v].Add(u); // omit for directed
}
```

## 0.2 Grid-as-graph (the hidden graph)
**Why:** Many “matrix BFS/DFS” problems are graphs where each cell is a node.

**Visual**
```text
Grid:
(0,0) (0,1) (0,2)
(1,0) (1,1) (1,2)

Neighbors of (r,c):
(r-1,c), (r+1,c), (r,c-1), (r,c+1)  (if in bounds)
```

---

# 🟢 Level 1: The Physical Layer (Basic Traversal)
**Goal:** Muscle memory for visited + exploring neighbors.

## Topics to know
- ✅ visited discipline (when to mark)
- 🌊 BFS queue skeleton
- 🕳️ DFS recursion skeleton
- 🧩 edge cases: disconnected graphs, self-loops, duplicates

## Things to master
- ✅ Mark visited when you enqueue/push (prevents duplicates)
- ✅ State your invariant: “visited = discovered region”
- ✅ Handle disconnected graphs by looping through all nodes

---

## 1.1 🕳️ DFS (recursive) — explore a connected region

**Why:** DFS is the simplest way to explore everything reachable from a start node.

**What:** Go deep along neighbors; mark visited; backtrack.

**How (step/flow):**
1) dfs(u): mark u visited
2) for each v in adj[u]: if not visited[v], dfs(v)

**Where:** connected components, island counting, reachability.

**When:** you need “explore region” and recursion depth is safe.

**Visual**
```text
Graph:
0--1--3
 \
  2

Start dfs(0):
Visit 0 -> then 1 -> then 3, backtrack, then 2

Visited grows like a paint fill.
```

**Python**
```python
def dfs(u):
    visited[u] = True
    for v in adj[u]:
        if not visited[v]:
            dfs(v)
```

**C#**
```csharp
void Dfs(int u)
{
    visited[u] = true;
    foreach (int v in adj[u])
        if (!visited[v]) Dfs(v);
}
```

**Common pitfalls**
- ❌ forgetting visited -> infinite recursion on cycles
- ❌ recursion depth overflow on large/deep graphs (switch to Level 2 stack)

**Tips & tricks**
- 💡 Invariant: “once visited[u] is true, u will never be processed again.”

**Practice (2+)**
- LeetCode 1971: Find if Path Exists in Graph
- LeetCode 200: Number of Islands (grid DFS)

---

## 1.2 🌊 BFS (queue) — explore in waves

**Why:** BFS naturally processes by distance (#edges) in unweighted graphs.

**What:** Use a queue; enqueue start; pop; enqueue undiscovered neighbors.

**How (step/flow):**
1) queue = [start]
2) visited[start] = true
3) while queue:
4) u = pop front
5) for v in adj[u]: if not visited[v]: visited[v]=true; push v

**Where:** shortest path in unweighted graphs, level expansion, multi-source spread.

**When:** distance matters or you need layer-by-layer growth.

**Visual (frontier growth)**
```text
Level 0: {S}
Level 1: neighbors of S
Level 2: neighbors of Level1 not seen yet

Queue is the pipe that enforces this order.
```

**Python**
```python
from collections import deque

def bfs(start):
    q = deque([start])
    visited[start] = True
    while q:
        u = q.popleft()
        for v in adj[u]:
            if not visited[v]:
                visited[v] = True
                q.append(v)
```

**C#**
```csharp
void Bfs(int start)
{
    var q = new Queue<int>();
    q.Enqueue(start);
    visited[start] = true;

    while (q.Count > 0)
    {
        int u = q.Dequeue();
        foreach (int v in adj[u])
        {
            if (!visited[v])
            {
                visited[v] = true;
                q.Enqueue(v);
            }
        }
    }
}
```

**Common pitfalls**
- ❌ marking visited when dequeuing instead of enqueuing (causes duplicates)
- ❌ forgetting to initialize visited per test case

**Tips**
- 💡 Invariant: “Queue contains nodes discovered but not processed.”

**Practice (2+)**
- LeetCode 733: Flood Fill
- LeetCode 841: Keys and Rooms

---

## 1.3 🧩 Disconnected graphs (loop over all nodes)

**Why:** Many graphs have multiple components; starting from one node won’t cover all.

**What:** For each node not visited, start a new DFS/BFS.

**How (step/flow):**
1) for u in 0..n-1:
2) if not visited[u]: components++; dfs(u)

**Where:** count components, check if graph is fully connected.

**When:** input doesn’t guarantee connected.

**Visual**
```text
Component A: 0--1
Component B: 2--3

Loop u=0 starts exploring A
Loop u=2 starts exploring B
```

**Practice (2+)**
- LeetCode 547: Number of Provinces
- LeetCode 1319: Number of Operations to Make Network Connected

---

# 🔵 Level 2: The Structural Layer (Iterative DFS + Components)
**Goal:** Replace recursion with stack + learn component mechanics.

## Topics to know
- 🎒 iterative DFS (stack)
- 🧱 parent tracking (avoid false cycles in undirected)
- 🧩 grid traversal hygiene (bounds + visited)

## Things to master
- ✅ You can convert recursive DFS to stack DFS
- ✅ You know exactly when to mark visited

---

## 2.1 🎒 Iterative DFS (stack)

**Why:** Avoid recursion depth limits and gain explicit control.

**What:** Stack holds nodes to process.

**How (step/flow):**
1) push start; mark visited
2) while stack not empty:
3) u = pop
4) for v in adj[u]: if not visited: mark + push

**Visual**
```text
Stack (top at right):
[0]
pop 0, push neighbors -> [1,2]
pop 2 -> ...

DFS order depends on push order, but reachability is correct.
```

**Pitfalls**
- ❌ marking visited on pop -> duplicates

**Practice (2+)**
- LeetCode 133: Clone Graph
- LeetCode 695: Max Area of Island (iterative)

---

## 2.2 🧱 Connected components as a reusable pattern

**Why:** “Count groups” appears everywhere: islands, provinces, friend circles.

**What:** Outer loop over nodes + inner traversal.

**How:** each fresh traversal discovers exactly one component.

**Invariant:** “No node belongs to two components.”

**Practice (2+)**
- LeetCode 200: Number of Islands
- LeetCode 547: Number of Provinces

---

# 🟠 Level 3: The Frontier Layer (Shortest Paths in Unweighted Graphs)
**Goal:** BFS + distances + parents.

## Topics to know
- 📏 BFS distance array
- 🧭 parent array for path reconstruction
- 🌋 multi-source BFS
- 🎨 bipartite coloring

## Things to master
- ✅ First time discovered = shortest distance (unweighted)
- ✅ You can reconstruct path using parent pointers

---

## 3.1 📏 BFS shortest distance + parent (path reconstruction)

**Why:** BFS gives shortest number-of-edges distance in unweighted graphs.

**What:** Maintain dist[u] and parent[u].

**How (step/flow):**
1) dist = -1 for all
2) dist[src]=0; parent[src]=-1
3) BFS: when discovering v from u:
   dist[v]=dist[u]+1; parent[v]=u

**Visual**
```text
S -- A -- B -- T

When T is first discovered, dist[T] is minimal.
Parents form a breadcrumb chain:
T <- B <- A <- S
```

**Pitfalls**
- ❌ overwriting parent/dist after already visited

**Practice (2+)**
- LeetCode 752: Open the Lock
- LeetCode 127: Word Ladder

---

## 3.2 🌋 Multi-source BFS (many starting points)

**Why:** Spread/rot/nearest-source problems start from multiple sources simultaneously.

**What:** Initialize queue with all sources at distance 0.

**How (step/flow):**
1) enqueue all sources
2) set their dist=0
3) BFS expands outward

**Visual**
```text
Sources: * *
Wave expands from all sources together.
The first wave to reach a cell sets the minimal distance.
```

**Practice (2+)**
- LeetCode 542: 01 Matrix
- LeetCode 994: Rotting Oranges

---

## 3.3 🎨 Bipartite check (BFS/DFS coloring)

**Why:** Many constraint problems reduce to 2-coloring.

**What:** Color nodes 0/1 so neighbors differ.

**How (step/flow):**
1) for each component: if uncolored, assign 0 and BFS
2) for each edge u-v: if color[v] == color[u] -> conflict

**Visual**
```text
u(0) -- v(1) -- w(0)
All edges connect opposite colors
Conflict occurs if an edge connects same color
```

**Practice (2+)**
- LeetCode 785: Is Graph Bipartite?
- LeetCode 886: Possible Bipartition

---

# 🟣 Level 4: The Constraint Layer (DAG, Cycles, 0-1 BFS)
**Goal:** Traversal with global structure constraints.

## Topics to know
- 🧱 topological sort (Kahn BFS / DFS finish order)
- 🔁 cycle detection (directed/undirected)
- 🪙 0-1 BFS (deque)

## Things to master
- ✅ “indegree” meaning and updates
- ✅ directed cycle detection using colors (0/1/2)

---

## 4.1 🧱 Topological sort (Kahn’s algorithm)

**Why:** Scheduling prerequisites requires an order that respects directed edges.

**What:** Repeatedly take nodes with indegree 0.

**How (step/flow):**
1) compute indegree[]
2) enqueue all nodes with indegree 0
3) pop u, append to order
4) for v in adj[u]: indegree[v]-- ; if 0 enqueue
5) if order size < n => cycle

**Visual**
```text
u -> v means u must come before v

Queue holds all currently-available tasks (indegree 0)
Processing u unlocks its neighbors.
```

**Practice (2+)**
- LeetCode 207: Course Schedule
- LeetCode 210: Course Schedule II

---

## 4.2 🔁 Cycle detection (directed graph coloring)

**Why:** Cycles break prerequisite ordering and many dependency problems.

**What:** Use colors:
- 0 = unvisited
- 1 = visiting (in current recursion stack)
- 2 = done

**How (step/flow):**
1) dfs(u): color[u]=1
2) for v in adj[u]:
   if color[v]==1 => back-edge => cycle
   if color[v]==0 => dfs(v)
3) color[u]=2

**Visual**
```text
0 (white) -> 1 (gray) -> 2 (black)

Back-edge to gray means a cycle.
```

**Practice (2+)**
- LeetCode 207: Course Schedule (cycle detection)
- LeetCode 802: Find Eventual Safe States

---

## 4.3 🪙 0-1 BFS (deque frontier)

**Why:** When edges have weight 0 or 1, you can do shortest path faster than Dijkstra using a deque.

**What:** Use deque; push 0-weight edges to front, 1-weight edges to back.

**How (step/flow):**
1) dist = INF; dist[src]=0
2) pop left u
3) for each edge u->v with w in {0,1}:
   if dist[u]+w < dist[v]: update dist[v]
   if w==0: pushleft(v) else pushright(v)

**Visual**
```text
Deque:
front ... back
0-cost neighbors go to front (processed sooner)
1-cost neighbors go to back

Invariant: deque pops the smallest-current-cost node first (for 0/1 weights)
```

**Practice (2+)**
- LeetCode 1368: Minimum Cost to Make at Least One Valid Path in a Grid
- LeetCode 2290: Minimum Obstacle Removal to Reach Corner

---

# 🔴 Level 5: The Abstract Layer (Implicit Graphs + Proof Thinking)
**Goal:** Turn problems into graphs and traverse the state space correctly.

## Topics to know
- 🧠 Implicit nodes (states) + neighbor generation
- 🧷 visited key design (what uniquely identifies a state)
- 🧮 DAG DP with topo order

## Things to master
- ✅ Define the state precisely (node)
- ✅ Define transitions precisely (edges)
- ✅ Prove visited key prevents revisits without losing optimal answers

---

## 5.1 🧠 State-space BFS (implicit graph)

**Why:** Many “puzzle” problems don’t give a graph, but you can generate neighbors.

**What:** A state is a node; legal moves generate edges.

**How (step/flow):**
1) define state representation (string/tuple)
2) generate neighbors (moves)
3) BFS with visited to avoid exponential repeats

**Visual**
```text
State: "0000"
Neighbors: change one wheel +/-1

This is a graph even though it wasn’t given as adj list.
```

**Practice (2+)**
- LeetCode 752: Open the Lock
- LeetCode 1091: Shortest Path in Binary Matrix

---

## 5.2 🧮 DP on DAG (traversal order matters)

**Why:** If the graph is a DAG, you can compute dp in topo order.

**What:** dp[u] depends on dp of neighbors or prerequisites.

**How (step/flow):**
1) topological sort
2) process nodes in that order
3) relax dp transitions

**Visual**
```text
Topo order ensures when you process u,
all nodes that u depends on are already computed.
```

**Practice (2+)**
- LeetCode 797: All Paths From Source to Target
- LeetCode 2050: Parallel Courses III (topo + dp)

---

# 🧰 Traversal mastery add-ons (graphs)

## A) 🧪 Micro-tracing (fast debugging)
- Draw 5 nodes and edges.
- Write visited set.
- For BFS: write queue as [front ... back].
- For DFS: write stack (or recursion) as vertical list.

## B) 🧠 Invariant library (memorize)
- DFS: “dfs(u) explores all reachable nodes from u exactly once.”
- BFS: “queue holds frontier; first discovery gives shortest distance (unweighted).”
- Components: “starting a traversal from an unvisited node finds exactly one component.”
- Topo (Kahn): “queue holds indegree 0 nodes only.”
- Directed cycle (colors): “back-edge to gray implies cycle.”
- 0-1 BFS: “deque order respects current best distances for 0/1 weights.”

## C) ✅ Pitfall checklist
- Did I mark visited on enqueue/push?
- Am I reusing visited/dist arrays incorrectly across test cases?
- If undirected: am I ignoring the parent edge in cycle checks?
- For BFS: did I freeze levelSize before iterating a level?
- For state graphs: is my visited key correct and minimal?
