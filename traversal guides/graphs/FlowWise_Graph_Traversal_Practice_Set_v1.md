# 🧪 Flow-Wise Graph Traversal — Problem Set (v1)
**Standard:** At least 2 LeetCode problems per subtopic + timeboxes + expected invariant.

---

# 🟢 Level 1 — Basic Traversal

## 1.1 DFS recursion (region exploration)
- LeetCode 200: Number of Islands
  - ⏱️ 35–55 min
  - 🧾 Invariant: dfs marks exactly the cells in one island; no cell visited twice.
- LeetCode 695: Max Area of Island
  - ⏱️ 35–55 min
  - 🧾 Invariant: returned area equals count of nodes visited in that component.

## 1.2 BFS skeleton (queue)
- LeetCode 733: Flood Fill
  - ⏱️ 25–40 min
  - 🧾 Invariant: queue contains pixels discovered but not yet recolored/processed.
- LeetCode 841: Keys and Rooms
  - ⏱️ 25–40 min
  - 🧾 Invariant: visited rooms are exactly those reachable via collected keys.

## 1.3 Disconnected graphs (loop all nodes)
- LeetCode 547: Number of Provinces
  - ⏱️ 30–50 min
  - 🧾 Invariant: each new DFS/BFS start discovers exactly one province.
- LeetCode 1319: Number of Operations to Make Network Connected
  - ⏱️ 45–70 min
  - 🧾 Invariant: components counted correctly; extra edges used to connect components.

---

# 🔵 Level 2 — Iterative DFS + Components

## 2.1 Iterative DFS (stack)
- LeetCode 133: Clone Graph
  - ⏱️ 45–75 min
  - 🧾 Invariant: every cloned node corresponds to exactly one original; edges preserved.
- LeetCode 200: Number of Islands (iterative stack version)
  - ⏱️ 45–70 min
  - 🧾 Invariant: stack contains only undiscovered land cells.

## 2.2 Component patterns (graph + grid)
- LeetCode 463: Island Perimeter
  - ⏱️ 35–55 min
  - 🧾 Invariant: perimeter increments only for edges adjacent to water/out-of-bounds.
- LeetCode 1020: Number of Enclaves
  - ⏱️ 45–70 min
  - 🧾 Invariant: boundary-reachable land is removed/marked; remaining land is enclaves.

---

# 🟠 Level 3 — Shortest Paths (Unweighted) + Coloring

## 3.1 BFS shortest path (distance + parent)
- LeetCode 752: Open the Lock
  - ⏱️ 50–85 min
  - 🧾 Invariant: first time a state is visited gives shortest moves.
- LeetCode 127: Word Ladder
  - ⏱️ 60–110 min
  - 🧾 Invariant: BFS levels correspond to transformation length.

## 3.2 Multi-source BFS
- LeetCode 542: 01 Matrix
  - ⏱️ 45–75 min
  - 🧾 Invariant: cells popped earlier have smaller/equal distance than later pops.
- LeetCode 994: Rotting Oranges
  - ⏱️ 35–60 min
  - 🧾 Invariant: minute count equals BFS layer count; all newly rotten in next layer.

## 3.3 Bipartite check
- LeetCode 785: Is Graph Bipartite?
  - ⏱️ 35–55 min
  - 🧾 Invariant: for every processed edge u-v, color[u] != color[v].
- LeetCode 886: Possible Bipartition
  - ⏱️ 45–70 min
  - 🧾 Invariant: dislike edges connect opposite groups.

---

# 🟣 Level 4 — DAG + Cycles + 0-1 BFS

## 4.1 Topological sort (Kahn)
- LeetCode 207: Course Schedule
  - ⏱️ 45–75 min
  - 🧾 Invariant: queue contains only nodes with indegree 0.
- LeetCode 210: Course Schedule II
  - ⏱️ 45–85 min
  - 🧾 Invariant: produced order respects all edges.

## 4.2 Cycle detection (directed / safe states)
- LeetCode 802: Find Eventual Safe States
  - ⏱️ 60–95 min
  - 🧾 Invariant: nodes marked safe are not part of any cycle and cannot reach a cycle.
- LeetCode 684: Redundant Connection
  - ⏱️ 45–75 min
  - 🧾 Invariant: adding the returned edge is what creates a cycle (use DSU or DFS).

## 4.3 0-1 BFS (deque)
- LeetCode 1368: Minimum Cost to Make at Least One Valid Path in a Grid
  - ⏱️ 75–130 min
  - 🧾 Invariant: deque pop order respects current best costs.
- LeetCode 2290: Minimum Obstacle Removal to Reach Corner
  - ⏱️ 75–130 min
  - 🧾 Invariant: dist matrix decreases monotonically as better paths found.

---

# 🔴 Level 5 — Implicit Graphs + Graph DP

## 5.1 State-space BFS
- LeetCode 1091: Shortest Path in Binary Matrix
  - ⏱️ 45–75 min
  - 🧾 Invariant: visited cells are those already assigned minimal distance.
- LeetCode 909: Snakes and Ladders
  - ⏱️ 60–110 min
  - 🧾 Invariant: first time reaching a square is minimal dice throws.

## 5.2 DP on DAG
- LeetCode 797: All Paths From Source to Target
  - ⏱️ 45–85 min
  - 🧾 Invariant: current path list matches recursion path; rollback correct.
- LeetCode 2050: Parallel Courses III
  - ⏱️ 70–120 min
  - 🧾 Invariant: dp[u] is earliest completion time respecting prerequisites.

---

## ✅ How to practice (recommended)
1) Do Level 1 until visited discipline is automatic.
2) Do Level 3 BFS shortest path (Open the Lock) until you can explain “first time visited is shortest”.
3) Add Level 4 topo sort and directed cycle coloring.
4) Only then attempt 0-1 BFS.

Write the invariant at the top of every solution before coding.
