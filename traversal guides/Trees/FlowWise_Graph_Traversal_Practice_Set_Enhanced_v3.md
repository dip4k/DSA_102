# 🧭 FlowWise Graph Traversal Practice Set — Enhanced v2

> Designed as short, repeatable drills with clear invariants.
>
> Focus: traversal mechanics (frontier discipline, visited/state, components, shortest paths in unweighted graphs, topo traversal in DAGs).

---

## ✅ Summary list
- Level-based progression: visited discipline → components → shortest paths (BFS) → topo traversal → 0-1 BFS.
- Every problem includes: timebox, expected invariant, and the failure mode it targets.
- Recommended workflow: write the invariant at the top before coding.

---

## 🧠 Unified graph traversal model

### Why
Graphs break “tree intuition” because cycles and multiple paths exist.

### What
- **Frontier**: stack (DFS) or queue (BFS) or deque (0-1 BFS).
- **Visited/state**: prevents revisits and enforces correctness (coloring, distance, parent).
- **Components loop**: if graph can be disconnected, you must start traversals from every unvisited node.

### How (step/flow)
1) Choose representation (adj list, grid neighbors, implicit state generator).
2) Choose traversal (DFS/BFS) based on goal.
3) Define visited/state (bool visited, color, dist, parent).
4) If disconnected: loop over all nodes.

### Common pitfalls
- Forgetting visited → infinite loops or exponential blowup.
- Using DFS for shortest path in unweighted graphs (BFS is the canonical choice).

### Tips and tricks
- When you pop a node from frontier, you should be able to say what property is already final (visited=true, dist is minimal, etc.).

---

# Level 1 — Basic traversal discipline (visited + neighbors)

## 1.1 DFS recursion (grid/region exploration)

### Why
This builds the “mark exactly once” habit.

### What
Run DFS from a start node to mark an entire region/component.

### How (step/flow)
- On entry: mark visited.
- Recurse to each unvisited neighbor.

### Where and When
- Grids (islands), connected regions.

### Common pitfalls
- Marking visited *after* recursion → revisits.

### Tips and tricks
- Mark visited immediately when you discover the node.

**Problems (pick 2; repeat until automatic)**
- LeetCode 200 — Number of Islands (35–55 min), invariant: DFS marks exactly one island; no cell visited twice.
- LeetCode 695 — Max Area of Island (35–55 min), invariant: returned area equals number of visited cells in component.

## 1.2 BFS skeleton (queue frontier)

### Why
BFS builds “frontier wave” intuition.

### What
Queue holds discovered-but-not-processed nodes.

### How (step/flow)
- Enqueue start, mark visited.
- While queue not empty: pop, expand neighbors.

### Where and When
- Multi-step expansion, shortest path in unweighted graphs.

### Common pitfalls
- Marking visited when popped rather than when enqueued (can cause duplicates).

### Tips and tricks
- For shortest path BFS: first time you visit a node is the shortest distance.

**Problems**
- LeetCode 733 — Flood Fill (25–40 min), invariant: queue contains pixels discovered but not yet processed.
- LeetCode 841 — Keys and Rooms (25–40 min), invariant: visited rooms are exactly those reachable from room 0.

## 1.3 Disconnected graphs (loop all nodes)

### Why
Many graphs are not connected; one traversal is not enough.

### What
Outer loop: for each node, if unvisited start a traversal.

### How (step/flow)
- Count components by counting how many times you start a traversal.

### Common pitfalls
- Assuming connected input.

### Tips and tricks
- Always ask: “Can this be disconnected?” before choosing approach.

**Problems**
- LeetCode 547 — Number of Provinces (30–50 min), invariant: each new DFS/BFS start discovers exactly one province.
- LeetCode 1319 — Number of Operations to Make Network Connected (45–70 min), invariant: components counted correctly; extra edges used to connect components.

---

# Level 2 — Iterative DFS + component patterns

## 2.1 Iterative DFS (explicit stack)

### Why
Iterative DFS is safer than recursion for deep graphs.

### What
Use stack as the frontier.

### How (step/flow)
- Push start, mark visited.
- Pop, expand neighbors.

### Common pitfalls
- Pushing neighbors without visited check → duplicates.

### Tips and tricks
- Same invariant as recursion: a node is expanded once.

**Problems**
- LeetCode 133 — Clone Graph (45–75 min), invariant: every cloned node corresponds to exactly one original; edges preserved.
- LeetCode 200 — Number of Islands (iterative) (45–70 min), invariant: stack contains only discovered land cells not yet expanded.

## 2.2 Component patterns in grids

### Why
Grids are the best place to internalize component traversal.

### What
Traverse each component and compute something (perimeter, enclaves).

### How (step/flow)
- Expand a component; update metric per cell/edge.

### Common pitfalls
- Counting edges twice or missing boundary checks.

### Tips and tricks
- For perimeter: add 1 for each side adjacent to water/out-of-bounds.

**Problems**
- LeetCode 463 — Island Perimeter (35–55 min), invariant: perimeter increments only for edges adjacent to water/out-of-bounds.
- LeetCode 1020 — Number of Enclaves (45–70 min), invariant: boundary-reachable land removed/marked; remaining land are enclaves.

---

# Level 3 — Shortest paths (unweighted BFS) + coloring

## 3.1 BFS shortest path (distance + parent)

### Why
In unweighted graphs, BFS is the canonical shortest-path traversal.

### What
Maintain `dist[]` and optionally `parent[]`.

### How (step/flow)
- Initialize dist[start]=0; enqueue start.
- For each neighbor not visited: set dist=dist[cur]+1, set parent, enqueue.

### Common pitfalls
- Revisiting nodes and overwriting dist.

### Tips and tricks
- First visit = shortest distance (unweighted).

**Problems**
- LeetCode 752 — Open the Lock (50–85 min), invariant: first time a state is visited gives shortest moves.
- LeetCode 127 — Word Ladder (60–110 min), invariant: BFS levels correspond to transformation length.

## 3.2 Multi-source BFS

### Why
When you have many sources, BFS from all at once avoids repeated work.

### What
Initialize queue with all sources (dist=0 for each).

### How (step/flow)
- Enqueue all sources first.
- Then run standard BFS.

### Common pitfalls
- Running BFS per source (too slow) instead of multi-source.

### Tips and tricks
- Multi-source BFS gives the nearest source distance.

**Problems**
- LeetCode 542 — 01 Matrix (45–75 min), invariant: earlier pops have <= distance than later pops.
- LeetCode 994 — Rotting Oranges (35–60 min), invariant: minute count equals BFS layer count.

## 3.3 Bipartite checking (2-coloring)

### Why
Coloring adds a second dimension of state to traversal.

### What
Assign colors 0/1; for each edge u-v, colors must differ.

### How (step/flow)
- For each uncolored node: BFS/DFS color it, enforce constraints on neighbors.

### Common pitfalls
- Not handling disconnected graph (must start from every node).

### Tips and tricks
- Use `color = -1` for uncolored.

**Problems**
- LeetCode 785 — Is Graph Bipartite? (35–55 min), invariant: for every processed edge u-v, color[u] != color[v].
- LeetCode 886 — Possible Bipartition (45–70 min), invariant: dislike edges connect opposite groups.

---

# Level 4 — DAG traversal (topological order) + cycle logic

## 4.1 Topological traversal (Kahn’s BFS)

### Why
Topo order is a traversal over constraints (edges). It’s BFS on indegrees.

### What
Maintain indegree; queue nodes with indegree 0.

### How (step/flow)
- Compute indegree.
- Enqueue all nodes with indegree 0.
- Pop, append to order, decrement neighbors’ indegree; enqueue newly-zero.

### Common pitfalls
- Forgetting to enqueue all initial indegree-0 nodes.

### Tips and tricks
- Invariant: queue contains only indegree-0 nodes.

**Problems**
- LeetCode 207 — Course Schedule (45–75 min), invariant: queue contains only nodes with indegree 0.
- LeetCode 210 — Course Schedule II (45–85 min), invariant: produced order respects all edges.

## 4.2 Directed cycle detection / safe states

### Why
“Can reach a cycle” questions require careful state coloring.

### What
DFS coloring states: unvisited, visiting, visited (or safe/unsafe).

### How (step/flow)
- DFS: when you see a “visiting” node again → cycle.

### Common pitfalls
- Not distinguishing visiting vs visited.

### Tips and tricks
- Use 3-color arrays: 0=unvisited,1=visiting,2=done.

**Problems**
- LeetCode 802 — Find Eventual Safe States (60–95 min), invariant: safe nodes cannot reach a cycle.
- LeetCode 684 — Redundant Connection (45–75 min), invariant: returned edge is the one that creates a cycle in an undirected graph.

---

# Level 5 — Deque frontier (0-1 BFS) + implicit graphs

## 5.1 0-1 BFS (deque)

### Why
When edge weights are only 0 or 1, 0-1 BFS is a traversal that behaves like Dijkstra but simpler.

### What
Use a deque:
- Weight 0 edge → push front
- Weight 1 edge → push back

### How (step/flow)
- Pop from front.
- Relax neighbors; update dist; push accordingly.

### Common pitfalls
- Using normal BFS queue (incorrect when weights exist).

### Tips and tricks
- Invariant: deque pop order respects nondecreasing dist.

**Problems**
- LeetCode 1368 — Minimum Cost to Make at Least One Valid Path in a Grid (75–130 min), invariant: deque order respects current best costs.
- LeetCode 2290 — Minimum Obstacle Removal to Reach Corner (75–130 min), invariant: dist decreases only when a better path is found.

## 5.2 State-space BFS (implicit graphs)

### Why
Some graphs are not given explicitly; neighbors are generated from a state.

### What
Treat each state as a node; generate next states.

### How (step/flow)
- BFS for minimum steps.
- Store visited states to prevent revisits.

### Common pitfalls
- State explosion due to missing pruning/visited.

### Tips and tricks
- Design a compact hashable representation for a state.

**Problems**
- LeetCode 1091 — Shortest Path in Binary Matrix (45–75 min), invariant: visited cells have assigned minimal distance.
- LeetCode 909 — Snakes and Ladders (60–110 min), invariant: first time reaching a square is minimal dice throws.

---

## ✅ How to practice (recommended ladder)
1) Grind Level 1 until visited discipline is automatic.
2) Grind Level 3 shortest path until you can explain: “first time visited is shortest.”
3) Add Level 4 topo + cycle-coloring.
4) Only then attempt 0-1 BFS.

Write the invariant at the top of every solution before coding.
