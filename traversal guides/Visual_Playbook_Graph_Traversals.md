# 🎛️ Visual Playbook — Graph Traversal (Diagrams Only)
**Purpose:** Copy-paste friendly ASCII diagrams for teaching graph traversal patterns (DFS/BFS/Shortest path/Topo/0-1 BFS).

---

## Legend
- `visited` = discovered nodes
- `stack` = DFS frontier
- `queue` = BFS frontier
- `dist[]` = distance from source in edges (unweighted)
- `parent[]` = predecessor for path reconstruction
- `indegree[]` = number of incoming edges (directed)
- For directed graphs, arrows show edge direction

---

# 🟢 Level 1 — Basic Traversal (Visited Discipline)

## 1) Visited prevents cycles
```text
Cycle:
0 -- 1
|    |
3 -- 2

If you don't mark visited:
0 -> 1 -> 2 -> 3 -> 0 -> ... (infinite)

Invariant: once visited[u] is true, u is never expanded again
```

## 2) Graph representations
### Adjacency list
```text
Edges: 0-1, 0-2, 1-3
Adj:
0: [1,2]
1: [0,3]
2: [0]
3: [1]
```

### Grid as graph (4-direction)
```text
(r,c) has neighbors:
(r-1,c) (r+1,c) (r,c-1) (r,c+1)   if in bounds

[ . ][ . ][ . ]
[ . ][ X ][ . ]
[ . ][ . ][ . ]

X expands to its valid neighbors
```

---

# 🔵 Level 2 — DFS (Depth Frontier)

## 3) Recursive DFS (go deep then backtrack)
```text
Graph:
0 -- 1 -- 3
 \ 
  2

DFS from 0 (one possible order):
Visit: 0 -> 1 -> 3, backtrack, then 2

Invariant: dfs(u) fully explores u's reachable region
```

## 4) Iterative DFS (explicit stack)
```text
stack (top on right)
Start: [0]
Pop 0, push neighbors -> [1,2]
Pop 2 -> [...]
Pop 1 -> [...]

Order depends on push order, but reachability is correct
Invariant: stack contains nodes discovered but not expanded yet
```

---

# 🟠 Level 3 — BFS (Wave Frontier) + Shortest Path

## 5) BFS layers (ring of fire)
```text
Unweighted graph distances from S:

Level 0: {S}
Level 1: neighbors of S
Level 2: neighbors of Level1 not seen
...

Invariant: queue processes nodes in non-decreasing distance
```

## 6) Queue snapshots (frontier)
```text
S -- A -- B -- T

Queue states:
[ S ]
pop S, push A -> [ A ]
pop A, push B -> [ B ]
pop B, push T -> [ T ]

When T is discovered, dist[T] is minimal (unweighted)
```

## 7) parent[] reconstructs shortest path
```text
Parents after BFS:
T <- B <- A <- S

Reconstruct by walking parent pointers backwards
(reverse at end)

Invariant: parent[v] is the node that first discovered v
```

## 8) Multi-source BFS
```text
Sources: S1 and S2

Queue init: [S1, S2]
Both have dist = 0

Wave expands from both simultaneously
Nearest source wins (first discovery)

Invariant: first time a node gets distance is minimal over all sources
```

## 9) Bipartite coloring (BFS/DFS)
```text
2-coloring:
(u)0 -- (v)1 -- (w)0

If you ever see an edge where both ends have same color -> NOT bipartite

Invariant: for every processed edge u-v, color[u] != color[v]
```

---

# 🟣 Level 4 — DAG + Cycles + Specialized Frontiers

## 10) Topological sort (Kahn's algorithm)
```text
Directed edges:
A -> C
B -> C
C -> D

indegree:
A:0  B:0  C:2  D:1

Queue init: [A, B]   (indegree 0 only)
Pop A -> reduce indegree(C)
Pop B -> C becomes 0 -> enqueue C
Pop C -> enqueue D

Invariant: queue always contains exactly nodes with indegree 0
If processed < n => cycle exists
```

## 11) Directed cycle detection (color states)
```text
Colors:
0 = unvisited (white)
1 = visiting  (gray)
2 = done      (black)

Back-edge to gray => cycle

u(gray) -> v(gray)  => cycle detected
Invariant: gray nodes are exactly the current recursion stack
```

## 12) 0-1 BFS (deque beats Dijkstra for weights 0/1)
```text
Edges have weight 0 or 1
Use deque:
- 0-edge relax: push FRONT
- 1-edge relax: push BACK

Deque:
front ........................ back

Invariant: nodes are popped in non-decreasing distance
(0-cost neighbors stay in same layer; 1-cost go to next layer)
```

---

# 🔴 Level 5 — Implicit State Graphs

## 13) State-space BFS (graph is not given)
```text
State = node
Move = edge

Example idea: "lock" state: 0000
Neighbors: change one wheel +1/-1

You generate neighbors on the fly
Visited prevents exponential revisits

Invariant: each state is processed at most once (for unweighted BFS)
```
