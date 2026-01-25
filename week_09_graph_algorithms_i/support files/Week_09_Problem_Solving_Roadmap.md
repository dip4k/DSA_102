# 🗺️ Week 09 Problem-Solving Roadmap — Progressive Practice Path

**Version:** 1.0  
**Purpose:** Structured problem-solving progression from easy to hard  
**Status:** ✅ ACTIVE

---

## 📖 HOW TO USE THIS ROADMAP

This guide provides a **structured pathway** for solving Week 09 problems, organized by:
1. **Tier level** (Foundational → Integration → Advanced)
2. **Algorithm focus** (Dijkstra, Bellman–Ford, Floyd–Warshall, MST, DSU)
3. **Problem difficulty** (Easy → Medium → Hard)
4. **Expected time** and **learning focus**

**Recommended approach:**
1. Start with **Tier 1: Foundational** problems (easier, build confidence)
2. Move to **Tier 2: Integration** problems (combine algorithms, handle variants)
3. Tackle **Tier 3: Advanced** problems (optimization, system design)
4. Return to weak areas as needed

---

## 🎯 TIER 1: FOUNDATIONAL PROBLEMS (Build Core Competency)

### Goal
Master each algorithm individually; understand mechanics and basic applications.

**Time allocation:** 15-20 hours total  
**Problems:** 15-20  
**Target mastery:** 80%+ correctness on first attempts

---

### Tier 1.1: Dijkstra's Algorithm (3-4 problems)

#### Problem T1.1.1: Shortest Path in Graph 🟢
**Difficulty:** Easy  
**Time:** 15-20 min  
**Focus:** Understand priority queue frontier and distance updates

**Description:**
```
Given a weighted graph with n nodes and edges,
find the shortest distance from source to all nodes.

Input:
  n = 5
  edges = [[0,1,4], [0,2,2], [1,2,1], [1,3,5], [2,3,8], [2,4,10], [3,4,2]]
  source = 0

Expected Output:
  [0, 3, 2, 13, 15]
```

**Learning Focus:**
- [ ] Understand why priority queue is needed (greedy selection)
- [ ] Implement distance initialization
- [ ] Implement relaxation logic
- [ ] Handle edge cases (isolated nodes, unreachable nodes)

**Solution Template:**
```
1. Initialize: dist[source] = 0, others = ∞
2. Add source to min-heap
3. While heap not empty:
   - Extract min-distance node u
   - For each neighbor v:
     - If dist[u] + weight(u,v) < dist[v]:
       - Update dist[v]
       - Add v to heap
```

**Verification:**
- [ ] Distances match expected output
- [ ] No negative distances (algorithm doesn't support negatives)
- [ ] Unreachable nodes remain ∞

**Related problems to solve next:** T1.1.2 (path reconstruction)

---

#### Problem T1.1.2: Reconstruct Shortest Paths 🟢
**Difficulty:** Easy  
**Time:** 15-20 min  
**Focus:** Path reconstruction using predecessor pointers

**Description:**
```
Same as T1.1.1, but also return the actual path from source to each node.

Output:
  distances = [0, 3, 2, 13, 15]
  paths = [
    [0],           // Path to 0
    [0, 2, 1],     // Path to 1: 0 -> 2 -> 1
    [0, 2],        // Path to 2
    [0, 2, 1, 3],  // Path to 3
    [0, 2, 1, 3, 4] // Path to 4
  ]
```

**Learning Focus:**
- [ ] Maintain predecessor array during Dijkstra
- [ ] Reconstruct path by backtracking from destination to source
- [ ] Handle unreachable nodes (no path exists)

**Solution Template:**
```
During Dijkstra:
  When dist[v] is updated:
    predecessor[v] = u

Path reconstruction:
  current = destination
  path = []
  while current != source:
    path.prepend(current)
    current = predecessor[current]
  path.prepend(source)
  return path
```

**Verification:**
- [ ] Each path's distance matches the computed distance
- [ ] Paths are logically valid (edges exist, weights sum correctly)

---

#### Problem T1.1.3: Network Delay Time (LeetCode #743) 🟡
**Difficulty:** Medium  
**Time:** 20-25 min  
**Focus:** Apply Dijkstra to directed graph; find max distance

**Description:**
```
You send a signal from node k. Return the time it takes for all nodes
to receive the signal. If impossible, return -1.

Example:
  n = 4, times = [[2,1,1],[2,3,1],[3,4,1]], k = 2
  Output: 3
```

**Learning Focus:**
- [ ] Build adjacency list from edge list
- [ ] Run Dijkstra from source k
- [ ] Find maximum distance (bottleneck)
- [ ] Handle unreachable nodes

**Approach:**
```
1. Build adjacency list (directed)
2. Run Dijkstra from k
3. Find max distance among all nodes
4. If any distance = ∞, return -1; else return max
```

**Verification:**
- [ ] Max distance is when last node receives signal
- [ ] Returns -1 for unreachable nodes
- [ ] Works with directed edges

---

#### Problem T1.1.4: Swim in Rising Water (LeetCode #778) 🟡
**Difficulty:** Medium  
**Time:** 25-30 min  
**Focus:** Modified Dijkstra (minimax path)

**Description:**
```
Find the path from top-left to bottom-right that minimizes
the maximum elevation encountered.

Example:
  grid = [[0,1,3,2],
          [5,1,2,0],
          [4,3,2,4]]
  Output: 4  // Path avoiding steep climbs
```

**Learning Focus:**
- [ ] Understand minimax problem (minimize the maximum value)
- [ ] Modify Dijkstra relaxation condition
- [ ] Use grid as graph (4-way adjacency)

**Key modification:**
```
Instead of: dist[u] + weight < dist[v]
Use: max(dist[u], grid[next_x][next_y]) < dist[v]
```

**Verification:**
- [ ] Output minimizes the maximum elevation
- [ ] Path is valid (reachable from start to end)
- [ ] Handles grid boundaries correctly

---

### Tier 1.2: Bellman–Ford & Negatives (2-3 problems)

#### Problem T1.2.1: Basic Bellman–Ford 🟢
**Difficulty:** Easy  
**Time:** 15-20 min  
**Focus:** V-1 passes, relaxation pattern

**Description:**
```
Find shortest paths from source to all nodes, with negative weight edges allowed.

Input:
  n = 5
  edges = [[0,1,4], [0,2,2], [1,2,-3], [1,3,2], [2,3,4]]
  source = 0

Output:
  distances = [0, 4, 2, 6, ∞]
```

**Learning Focus:**
- [ ] Understand why V-1 passes suffice
- [ ] Implement relaxation loop
- [ ] Compare with Dijkstra (no priority queue needed)
- [ ] Handle negative weights correctly

**Solution Template:**
```
1. Initialize: dist[source] = 0, others = ∞
2. For i = 1 to V-1:
   - For each edge (u, v, w):
     - If dist[u] + w < dist[v]:
       - dist[v] = dist[u] + w
3. Return dist
```

**Verification:**
- [ ] Distances match expected output
- [ ] Handles negative weights (should differ from Dijkstra)

---

#### Problem T1.2.2: Negative Cycle Detection 🟡
**Difficulty:** Medium  
**Time:** 20-25 min  
**Focus:** Detect cycles, identify affected nodes

**Description:**
```
Same as T1.2.1, but detect if a negative cycle exists reachable from source.

Example:
  edges with a cycle: [..., [2,0,-10], ...]
  Should detect cycle and return True
```

**Learning Focus:**
- [ ] Extra pass after V-1 iterations to detect improvement
- [ ] Understand which nodes are affected by negative cycles
- [ ] Return cycle nodes (optional advanced)

**Detection Logic:**
```
After V-1 passes:
  For each edge (u, v, w):
    If dist[u] + w < dist[v]:
      return True  // Negative cycle exists
  return False
```

**Verification:**
- [ ] Correctly identifies positive-weight cycles as no cycle
- [ ] Correctly identifies negative-weight cycles
- [ ] Returns True/False as expected

---

### Tier 1.3: Floyd–Warshall (2-3 problems)

#### Problem T1.3.1: All-Pairs Shortest Paths 🟢
**Difficulty:** Easy  
**Time:** 15-20 min  
**Focus:** Triple-nested loop, k-dimension critical

**Description:**
```
Compute shortest distances between all pairs of vertices.

Input:
  n = 4
  edges = [[0,1,3], [0,3,7], [1,2,1], [1,3,2], [2,0,4], [3,2,1]]

Output:
  dist[i][j] = shortest distance from i to j
  dist = [
    [0, 3, 4, 2],
    [5, 0, 1, 2],
    [4, 7, 0, 9],
    [5, 8, 1, 0]
  ]
```

**Learning Focus:**
- [ ] Initialize matrix correctly
- [ ] Understand k-dimension (intermediate vertices)
- [ ] **K-LOOP MUST BE OUTERMOST** (most common mistake!)
- [ ] Verify all distances

**Solution Template:**
```
1. Initialize dist matrix
2. For k = 0 to n-1:        // K-LOOP OUTERMOST!
   - For i = 0 to n-1:
     - For j = 0 to n-1:
       - dist[i][j] = min(dist[i][j], dist[i][k] + dist[k][j])
```

**Verification:**
- [ ] dist[i][i] = 0 for all i
- [ ] dist is symmetric if graph is undirected
- [ ] Spot-check: dist[0][3] = 2 via intermediate nodes

---

#### Problem T1.3.2: Detect Negative Cycles 🟡
**Difficulty:** Medium  
**Time:** 20-25 min  
**Focus:** Check diagonal after computation

**Description:**
```
Same as T1.3.1, but also detect if negative cycles exist.

If negative cycle exists, return the fact; else return dist matrix.
```

**Learning Focus:**
- [ ] Understand cycle detection in all-pairs context
- [ ] Check diagonal: dist[i][i]
- [ ] If any dist[i][i] < 0, cycle exists

**Detection Logic:**
```
After Floyd–Warshall:
  For i = 0 to n-1:
    If dist[i][i] < 0:
      return "Negative cycle detected at vertex i"
  return dist
```

**Verification:**
- [ ] Correctly identifies absence of cycles (all dist[i][i] = 0)
- [ ] Correctly identifies negative cycles (some dist[i][i] < 0)

---

### Tier 1.4: MST - Kruskal (2-3 problems)

#### Problem T1.4.1: Basic Kruskal's MST 🟢
**Difficulty:** Easy  
**Time:** 15-20 min  
**Focus:** Sorting edges, DSU for cycle detection

**Description:**
```
Given n vertices and weighted edges, find MST using Kruskal's algorithm.
Return MST weight and edges.

Input:
  n = 4
  edges = [[0,1,10], [0,2,6], [0,3,5], [1,3,15], [2,3,4]]

Output:
  mst_weight = 19
  mst_edges = [[2,3,4], [0,3,5], [0,2,6], [0,1,10]]
```

**Learning Focus:**
- [ ] Sort edges by weight
- [ ] Use DSU to avoid cycles
- [ ] Understand why greedy works (cut property)
- [ ] Stop when n-1 edges are added

**Solution Template:**
```
1. Sort edges by weight
2. Initialize DSU
3. For each edge (u, v, w):
   - If find(u) != find(v):
     - Add edge to MST
     - Union(u, v)
     - mst_weight += w
     - If |MST| == n-1: break
```

**Verification:**
- [ ] MST has n-1 edges
- [ ] Total weight is minimal
- [ ] No cycles in MST

---

#### Problem T1.4.2: Prim's MST 🟢
**Difficulty:** Easy  
**Time:** 15-20 min  
**Focus:** Growing tree from vertex, priority queue

**Description:**
```
Same as T1.4.1, but using Prim's algorithm instead of Kruskal's.
```

**Learning Focus:**
- [ ] Start from arbitrary vertex
- [ ] Maintain frontier of edges to unvisited nodes
- [ ] Priority queue for cheapest edge selection
- [ ] Similar structure to Dijkstra

**Solution Template:**
```
1. Start from vertex 0
2. Add all edges from 0 to priority queue
3. While PQ not empty and |MST| < n-1:
   - Extract min-weight edge (u, v)
   - If v already visited: continue
   - Mark v visited, add edge to MST
   - Add all edges from v to unvisited nodes
```

**Verification:**
- [ ] Same MST weight as Kruskal
- [ ] Works on undirected graphs
- [ ] Handles disconnected graphs correctly

---

#### Problem T1.4.3: Connecting Cities (LeetCode variant) 🟡
**Difficulty:** Medium  
**Time:** 20-25 min  
**Focus:** MST on practical problem, handling disconnected graphs

**Description:**
```
Connect n cities with minimum cost. If impossible (disconnected), return -1.

Input:
  n = 3
  connections = [[1,2,5],[1,3,6],[2,3,1]]

Output:
  mst_cost = 6  // Edges: 2-3 (cost 1) and 1-2 (cost 5)
```

**Learning Focus:**
- [ ] Check if graph is connected (MST has n-1 edges)
- [ ] Return -1 for disconnected graphs
- [ ] Apply Kruskal or Prim

**Verification:**
- [ ] Returns -1 if fewer than n-1 edges added
- [ ] Returns correct MST cost otherwise
- [ ] All n cities are connected

---

### Tier 1.5: DSU / Union–Find (3-4 problems)

#### Problem T1.5.1: Basic DSU Implementation 🟢
**Difficulty:** Easy  
**Time:** 10-15 min  
**Focus:** Parent pointers, find/union operations

**Description:**
```
Implement DSU with make_set, find, union operations.
Test with:
  make_set(0..3)
  union(0, 1), union(2, 3), union(0, 2)
  find(0), find(1), find(2), find(3)  // All should return same root
```

**Learning Focus:**
- [ ] Initialize parent[i] = i for all i
- [ ] Implement find with path compression
- [ ] Implement union with union-by-rank
- [ ] Test on provided examples

**Verification:**
- [ ] find returns consistent root for same set
- [ ] Different sets have different roots
- [ ] Path compression works (tree flattens)

---

#### Problem T1.5.2: Cycle Detection (Redundant Connection) 🟡
**Difficulty:** Medium  
**Time:** 20-25 min  
**Focus:** DSU for finding cycle-creating edge

**Description:**
```
Given n vertices and n edges (one extra), find the edge that creates a cycle.
Return the LAST such edge.

Input:
  n = 3
  edges = [[1,2],[2,3],[1,3]]

Output:
  [1,3]  // Removing this edge leaves a tree
```

**Learning Focus:**
- [ ] Process edges in order
- [ ] Find first edge where endpoints are already connected
- [ ] Track and return the last such edge
- [ ] Use DSU for efficient connectivity check

**Solution Template:**
```
result = []
for (u, v) in edges:
  if find(u) == find(v):
    result = [u, v]  // Update, don't return immediately
  else:
    union(u, v)
return result
```

**Verification:**
- [ ] Removing returned edge leaves a tree
- [ ] Returns last redundant edge if multiple exist

---

#### Problem T1.5.3: Connected Components Count 🟡
**Difficulty:** Medium  
**Time:** 15-20 min  
**Focus:** Component counting using DSU

**Description:**
```
Given n vertices and edges, count connected components using DSU.

Input:
  n = 5
  edges = [[0,1], [1,2], [3,4]]

Output:
  2  // Components: {0,1,2}, {3,4}
```

**Learning Focus:**
- [ ] Union all edge endpoints
- [ ] Count distinct roots
- [ ] Handle isolated vertices

**Solution Template:**
```
roots = set()
for i in 0 to n-1:
  roots.add(find(i))
return len(roots)
```

**Verification:**
- [ ] Correct component count
- [ ] Isolated vertices counted as individual components

---

#### Problem T1.5.4: Accounts Merge (LeetCode #721) 🟡
**Difficulty:** Medium  
**Time:** 25-30 min  
**Focus:** DSU for grouping elements

**Description:**
```
Merge accounts with common emails.

Input:
  accounts = [["John", "johnsmith@mail.com", "john00@mail.com"],
              ["John", "johnsmith@mail.com", "john_newyork@mail.com"],
              ["Mary", "mary@mail.com"]]

Output:
  [["John", "john00@mail.com", "john_newyork@mail.com", "johnsmith@mail.com"],
   ["Mary", "mary@mail.com"]]
```

**Learning Focus:**
- [ ] Map emails to account indices
- [ ] Union accounts sharing emails
- [ ] Group emails by account (root)
- [ ] Format and sort output

**Solution Template:**
```
1. Create email -> account mapping
2. Union accounts sharing emails
3. Group emails by root account
4. Format output: [name, sorted_emails]
```

**Verification:**
- [ ] All emails of same person are merged
- [ ] Emails are sorted alphabetically
- [ ] Person name is preserved

---

## 🎯 TIER 2: INTEGRATION PROBLEMS (Combine Algorithms)

### Goal
Solve variant problems requiring algorithm combination or optimization decisions.

**Time allocation:** 15-20 hours total  
**Problems:** 12-15  
**Target mastery:** 70%+ correctness on first attempts

---

### Tier 2.1: Combined Shortest Paths (2-3 problems)

#### Problem T2.1.1: Shortest Path with at Most K Stops 🟡
**Difficulty:** Medium  
**Time:** 25-30 min  
**Focus:** Dijkstra variant with constraints

**Description:**
```
Find shortest path with AT MOST k stops.

Input:
  n = 4, flights = [[0,1,100],[1,2,100],[0,2,500],[1,3,600],[2,3,200]],
  src = 0, dst = 3, k = 1

Output:
  700  // Path: 0 -> 1 -> 3 with 1 stop
```

**Learning Focus:**
- [ ] Add stop count as DP dimension
- [ ] State: (cost, city, stops_used)
- [ ] Only process if stops <= k
- [ ] Compare with Bellman–Ford variant

**Approach:**
```
Use Dijkstra with (dist, city, stops) tuple
Only enqueue if stops < k
Return min distance to destination across all stop counts
```

**Verification:**
- [ ] Path uses at most k stops
- [ ] Distance is minimal for k stops constraint
- [ ] Result differs from unconstrained Dijkstra

---

#### Problem T2.1.2: Currency Exchange Arbitrage 🟡
**Difficulty:** Medium  
**Time:** 25-30 min  
**Focus:** Transform multiplicative to additive (log), detect positive cycles

**Description:**
```
Detect arbitrage: Start with 1 unit of currency X, after exchanges, have > 1 unit X.

Input:
  rates = [["USD", "EUR", 1.1], ["EUR", "JPY", 120], ["JPY", "USD", 0.01]]

Output:
  True  // 1 USD -> 1.1 EUR -> 132 JPY -> 1.32 USD (profit!)
```

**Learning Focus:**
- [ ] Transform multiplication to addition via log
- [ ] Detect positive cycles (negative in log-space)
- [ ] Use Bellman–Ford with negated log rates
- [ ] Run from each source vertex

**Key transformation:**
```
log(a * b) = log(a) + log(b)
log(rate) = log of exchange rate
Negate to find positive cycles (profit)
```

**Verification:**
- [ ] Correctly identifies arbitrage when possible
- [ ] Returns False for no-profit scenarios

---

### Tier 2.2: MST Variants (2-3 problems)

#### Problem T2.2.1: Minimax Path / Max-Min Path 🟡
**Difficulty:** Medium  
**Time:** 25-30 min  
**Focus:** Modified Floyd–Warshall, maximize minimum edge

**Description:**
```
Find path maximizing the minimum edge weight (bottleneck path).

Input:
  graph: 0-1 (weight 2), 1-2 (weight 3), 0-2 (weight 1)

Output:
  Paths 0 to 2:
    0 -> 2: bottleneck = 1
    0 -> 1 -> 2: bottleneck = min(2, 3) = 2 <- BEST
  Answer: 2
```

**Learning Focus:**
- [ ] Modify Floyd–Warshall to maximize minimum
- [ ] Use: dist[i][j] = max(dist[i][j], min(dist[i][k], dist[k][j]))
- [ ] Same structure, different operation
- [ ] Compare with Dijkstra variant

**Approach:**
```
Floyd–Warshall with:
  dist[i][j] = max(dist[i][j], min(dist[i][k], dist[k][j]))
```

**Verification:**
- [ ] Returns maximum bottleneck value for each pair
- [ ] Bottleneck is minimum edge on optimal path

---

#### Problem T2.2.2: Minimum Cost to Connect All Points (Geometric MST) 🟡
**Difficulty:** Medium  
**Time:** 25-30 min  
**Focus:** MST on complete geometric graph

**Description:**
```
Connect n 2D points with minimum Manhattan distance.

Input:
  points = [[0,0],[2,2],[3,10],[5,2],[7,0]]

Output:
  20  // MST weight
```

**Learning Focus:**
- [ ] Build complete graph (all pairs)
- [ ] Calculate distances
- [ ] Apply Kruskal or Prim
- [ ] Prim is O(n²) vs Kruskal O(n² log n) for complete graphs

**Approach:**
```
1. Generate all edges with Manhattan distances
2. Sort by distance
3. Apply Kruskal's algorithm
4. Or use Prim starting from arbitrary point
```

**Verification:**
- [ ] MST weight is minimal
- [ ] Handles all n points
- [ ] Distance formula is correct (Manhattan)

---

### Tier 2.3: DSU Advanced Applications (2-3 problems)

#### Problem T2.3.1: Kruskal's MST with DSU 🟡
**Difficulty:** Medium  
**Time:** 20-25 min  
**Focus:** Integration of Kruskal + DSU

**Description:**
```
Implement Kruskal's MST from scratch with DSU.

Verify:
  - MST has exactly n-1 edges
  - MST weight is minimal
  - No cycles
```

**Learning Focus:**
- [ ] DSU is backbone of Kruskal
- [ ] Without DSU, cycle detection is O(n) per edge
- [ ] With DSU, it's O(α(n)) per edge
- [ ] Understand why Kruskal is O(E log E)

**Verification:**
- [ ] MST weight matches expected
- [ ] No cycles in MST
- [ ] All vertices connected

---

#### Problem T2.3.2: Bipartite Graph Check via DSU 🟡
**Difficulty:** Medium  
**Time:** 25-30 min  
**Focus:** DSU with complementary nodes

**Description:**
```
Check if graph is bipartite using DSU.

Input:
  n = 4
  edges = [[1,0], [2,0], [3,1], [3,2]]

Output:
  False  // Odd cycle exists, not bipartite
```

**Learning Focus:**
- [ ] DSU with 2n nodes (each vertex and its complement)
- [ ] For edge (u, v): union(u, v+n) and union(v, u+n)
- [ ] Check if find(u) == find(v) (contradiction)
- [ ] Compare with DFS coloring

**Approach:**
```
1. Create DSU with 2n nodes
2. For each edge (u, v):
   - union(u, v+n)  // u with complement of v
   - union(v, u+n)  // v with complement of u
   - If find(u) == find(v): not bipartite
3. If no contradiction: bipartite
```

**Verification:**
- [ ] Correctly identifies bipartite graphs
- [ ] Correctly rejects non-bipartite graphs

---

### Tier 2.4: Multiple Algorithm Scenarios (2-3 problems)

#### Problem T2.4.1: Choose Your Algorithm 🟡
**Difficulty:** Medium  
**Time:** 20-25 min  
**Focus:** Decision-making about algorithm choice

**Description:**
```
Given problem statements, choose the best algorithm and justify.

Examples:
1. "Find shortest path in sparse unweighted graph" -> ?
2. "Find MST in dense complete graph" -> ?
3. "Detect all components in dynamic graph" -> ?
```

**Learning Focus:**
- [ ] Understand trade-offs: BFS vs. Dijkstra, Kruskal vs. Prim, DSU vs. BFS
- [ ] Consider graph properties: sparse/dense, weighted/unweighted
- [ ] Consider problem constraints: number of queries, updates, etc.
- [ ] Match algorithms to scenarios

**Decision Framework:**
- Sparse + unweighted -> BFS
- Sparse + weighted + non-negative -> Dijkstra
- Sparse + weighted + negative -> Bellman–Ford
- Dense + all-pairs -> Floyd–Warshall
- MST + sparse -> Kruskal
- MST + dense -> Prim
- Dynamic connectivity -> DSU

**Verification:**
- [ ] Chosen algorithm is optimal for constraints
- [ ] Justification is sound

---

## 🎯 TIER 3: ADVANCED PROBLEMS (Deep Mastery)

### Goal
Solve complex problems requiring deep understanding, optimizations, and system thinking.

**Time allocation:** 10-15 hours total  
**Problems:** 8-10  
**Target mastery:** 60%+ correctness (these are challenging)

---

### Tier 3.1: System Design & Real-World Applications (2-3 problems)

#### Problem T3.1.1: GPS Navigation System 🔴
**Difficulty:** Hard  
**Time:** 45-60 min  
**Focus:** System design, Dijkstra optimization, real-world trade-offs

**Description:**
```
Design a GPS navigation system:
- User requests route from A to B
- Millions of concurrent queries
- Map has millions of vertices and edges
- Updates occur (new roads, closures)
- Optimize for query time

Questions:
1. Which algorithm? Dijkstra, A*, bidirectional?
2. How to preprocess for speed?
3. How to handle dynamic updates?
4. How to optimize memory?
```

**Learning Focus:**
- [ ] Understand Dijkstra's limitations (no preprocessing)
- [ ] Learn A* heuristic (Euclidean distance)
- [ ] Learn bidirectional search (~2x speedup)
- [ ] Preprocessing techniques (landmarks, hub labels)
- [ ] Trade-offs: query time vs. preprocessing cost

**System Design Aspects:**
```
Preprocessing Phase:
  - Compute hierarchy (highways vs. local roads)
  - Identify landmarks
  - Store edge weights

Query Phase:
  - Run A* from source
  - Use geographic heuristic
  - Prune unpromising directions

Update Phase:
  - Incrementally update affected nodes
  - Recompute local subgraphs
  - Invalidate cached paths
```

**Verification:**
- [ ] Justifies algorithm choice
- [ ] Addresses scale (millions of vertices)
- [ ] Balances speed vs. memory
- [ ] Handles updates efficiently

---

#### Problem T3.1.2: Social Network Connectivity Analysis 🔴
**Difficulty:** Hard  
**Time:** 45-60 min  
**Focus:** DSU + Floyd–Warshall, degrees of separation, clustering

**Description:**
```
Analyze a social network:
- Billions of users, trillions of friendship edges
- Query: Degrees of separation between two users?
- Query: Detect cliques or clusters?
- Query: Find influencers (highest degree nodes)?

Design approach for each query type.
```

**Learning Focus:**
- [ ] Degrees of separation = shortest path length (BFS, not DSU alone)
- [ ] Clustering = connected components (DSU for incremental, BFS for one-shot)
- [ ] Influencers = degree analysis + betweenness centrality
- [ ] Trade-offs: offline vs. online, memory vs. speed

**Approach:**
```
Degrees of Separation:
  - Use BFS from query source
  - Return distance to destination
  - Time: O(V+E) per query
  
Components:
  - Use DSU for incremental updates
  - Use BFS/DFS for one-shot analysis
  
Influencers:
  - Track node degrees
  - Sort by degree
  - Return top-k
```

**Verification:**
- [ ] Correctly computes degrees of separation
- [ ] Efficiently identifies components
- [ ] Handles billions of nodes (memory-aware)

---

### Tier 3.2: Optimization Problems (2-3 problems)

#### Problem T3.2.1: SPFA (Shortest Path Faster Algorithm) 🔴
**Difficulty:** Hard  
**Time:** 30-40 min  
**Focus:** Optimize Bellman–Ford, understand when it's faster

**Description:**
```
Implement SPFA and compare with Bellman–Ford and Dijkstra.

SPFA = Bellman–Ford + BFS optimization
Instead of checking all E edges in each pass,
maintain queue of nodes with recent updates.

Scenarios:
1. Sparse graph with negatives: SPFA faster than Bellman–Ford?
2. Dense graph: Still faster?
3. When to use SPFA vs. Bellman–Ford?
```

**Learning Focus:**
- [ ] SPFA maintains queue of "active" nodes
- [ ] Only relax edges from active nodes
- [ ] Average O(E), worst-case O(V*E)
- [ ] Better than Bellman–Ford for sparse graphs
- [ ] Track node entry count for cycle detection

**Implementation Template:**
```
1. Initialize queue with source
2. While queue not empty:
   - Dequeue node u
   - For each neighbor v:
     - If dist[u] + weight < dist[v]:
       - Update dist[v]
       - If v not in queue: enqueue v
3. Track entry count per node for cycle detection
```

**Verification:**
- [ ] SPFA produces same distances as Bellman–Ford
- [ ] Faster on sparse graphs with negatives
- [ ] Cycle detection works correctly

---

#### Problem T3.2.2: All-Pairs with Edge Constraints 🔴
**Difficulty:** Hard  
**Time:** 40-50 min  
**Focus:** Multi-dimensional DP

**Description:**
```
Find all-pairs shortest paths with MULTIPLE constraints:
1. Shortest distance
2. Using at most k edges
3. Avoiding specific vertices
4. Minimizing cost subject to delay constraint

Example: Shortest path with ≤ 100 cost AND ≤ 5 hops.
```

**Learning Focus:**
- [ ] Extend Floyd–Warshall with additional dimensions
- [ ] DP state: (i, j, k, edges) or similar
- [ ] Understand when brute-force fails (exponential dimensions)
- [ ] Balance space and time

**Approach:**
```
DP[i][j][k][hops] = shortest distance from i to j
                    using ≤ k cost
                    using ≤ hops edges

Recurrence:
  DP[i][j][k][h] = min(
    DP[i][j][k-1][h],  // Don't use all budget
    min over v of (DP[i][v][k-cost(v,j)][h-1] + cost(v,j))
  )
```

**Verification:**
- [ ] Satisfies all constraints
- [ ] Minimizes objective (distance)
- [ ] Handles infeasible cases (impossible constraints)

---

### Tier 3.3: Theoretical & Proof-Based (1-2 problems)

#### Problem T3.3.1: Prove MST Correctness via Cut Property 🔴
**Difficulty:** Hard  
**Time:** 45-60 min  
**Focus:** Theoretical understanding, proof writing

**Description:**
```
Prove that Kruskal's algorithm produces a minimum spanning tree.

Use the Cut Property:
  For any cut (S, V-S), the minimum-weight edge crossing
  the cut is part of some MST.

Proof outline:
1. Show Kruskal's first edge is in some MST
2. Show Kruskal's k-th edge is in some MST (induction)
3. Conclude final tree is an MST
```

**Learning Focus:**
- [ ] Understand cut property intuitively
- [ ] Formalize proof by contradiction or induction
- [ ] Relate algorithm steps to cut property
- [ ] Identify where greedy choice is justified

**Proof Structure:**
```
Theorem: Kruskal's algorithm produces an MST.

Proof by induction on edges added:
  Base: First edge is min-weight; by cut property, it's in some MST
  
  Step: Assume T_k (first k edges of Kruskal) is in some MST T*.
        Show T_{k+1} is in some MST.
        
        Edge e_{k+1} is minimum among remaining edges not forming cycle.
        Consider cut defined by T_k's components.
        e_{k+1} is minimum crossing this cut.
        By cut property, some MST contains e_{k+1}.
        Thus T_{k+1} is in some MST.
  
  Conclusion: Final tree T (with n-1 edges) is an MST.
```

**Verification:**
- [ ] Proof is logically sound
- [ ] Uses cut property correctly
- [ ] Covers all cases (including ties)

---

## 📋 RECOMMENDED COMPLETION ORDER

### Week 1 (Days 1-2): Dijkstra & Bellman–Ford
**Tier 1:** T1.1.1 → T1.1.2 → T1.1.3 → T1.1.4 → T1.2.1 → T1.2.2  
**Tier 2:** T2.1.1 → T2.1.2  
**Time:** 6-8 hours

### Week 2 (Days 3-4): Floyd–Warshall & MST
**Tier 1:** T1.3.1 → T1.3.2 → T1.4.1 → T1.4.2 → T1.4.3  
**Tier 2:** T2.2.1 → T2.2.2 → T2.4.1  
**Time:** 6-8 hours

### Week 3 (Day 5): DSU
**Tier 1:** T1.5.1 → T1.5.2 → T1.5.3 → T1.5.4  
**Tier 2:** T2.3.1 → T2.3.2  
**Time:** 5-6 hours

### Week 4+: Advanced & Optimization
**Tier 2:** Remaining integration problems  
**Tier 3:** System design, optimization, proofs  
**Time:** 10-15 hours (as time allows)

---

## 🎯 SUCCESS CRITERIA

**By end of Tier 1:**
- [ ] Implement all 5 core algorithms from memory
- [ ] Trace algorithms on paper
- [ ] Solve 15+ foundational problems
- [ ] Score 80%+ on first attempts

**By end of Tier 2:**
- [ ] Solve variant problems with small adjustments
- [ ] Combine algorithms for complex scenarios
- [ ] Understand trade-offs and optimization
- [ ] Score 70%+ on first attempts

**By end of Tier 3:**
- [ ] Solve novel problems requiring creativity
- [ ] Design systems applying algorithms
- [ ] Understand theoretical foundations
- [ ] Score 60%+ on first attempts

---

**Status:** ✅ Week 09 Problem-Solving Roadmap — COMPLETE

