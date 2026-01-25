# ✅ Week 09 Daily Progress Checklist — Graph Algorithms I

**Version:** 1.0  
**Purpose:** Track daily progress and ensure complete coverage of all Week 09 topics  
**Status:** ✅ ACTIVE

---

## 📋 OVERVIEW

This checklist helps you track:
1. **Conceptual understanding** — Do you grasp the algorithm?
2. **Implementation skills** — Can you code it?
3. **Trade-off analysis** — When should you use it?
4. **Practice mastery** — Have you solved problems?
5. **Interview readiness** — Can you handle interview questions?

**How to use:** Check off items as you complete them. At day's end, aim for ≥80% of items checked.

---

## 🎯 DAY 1: DIJKSTRA'S ALGORITHM (Single-Source Shortest Paths)

### Conceptual Understanding

- [ ] **1.1.1** Understand the problem: Find shortest path from source to all other vertices
- [ ] **1.1.2** Know the constraint: Only works with non-negative edge weights
- [ ] **1.1.3** Understand greedy intuition: Always process closest unvisited vertex
- [ ] **1.1.4** Know why greedy works: Non-negative weights guarantee local optimality = global optimality
- [ ] **1.1.5** Understand the data structure: Priority queue (min-heap) maintains frontier
- [ ] **1.1.6** Understand relaxation principle: Check if path through u is better than known path to v
- [ ] **1.1.7** Understand stale entries: Multiple PQ entries for same vertex OK; skip already-processed ones

### Visual Learning & Tracing

- [ ] **1.2.1** Trace Dijkstra on provided 5-vertex example (from instructional material)
- [ ] **1.2.2** Manually compute distances for each step
- [ ] **1.2.3** Understand priority queue state at each extraction
- [ ] **1.2.4** Verify final distances match manual pathfinding
- [ ] **1.2.5** Create your own 4-vertex graph; trace Dijkstra step-by-step
- [ ] **1.2.6** Identify which vertices are processed and which are frontier at each step
- [ ] **1.2.7** Understand why a vertex, once processed, is never revisited

### Implementation Skills

- [ ] **1.3.1** Code basic Dijkstra (pseudocode → your language)
- [ ] **1.3.2** Implement priority queue (use language's built-in or understand heap)
- [ ] **1.3.3** Implement distance array initialization
- [ ] **1.3.4** Implement relaxation logic (check if new path is shorter)
- [ ] **1.3.5** Implement processed array to skip stale entries
- [ ] **1.3.6** Implement path reconstruction (predecessor pointers)
- [ ] **1.3.7** Test implementation on provided examples; verify distances
- [ ] **1.3.8** Handle edge cases: disconnected graph (some distances remain ∞), self-loops, duplicate edges
- [ ] **1.3.9** Code runs without errors; produces correct output

### Complexity Understanding

- [ ] **1.4.1** Understand why insert/extract from heap is O(log V)
- [ ] **1.4.2** Understand why E edges are processed → O(E log V) for all relaxations
- [ ] **1.4.3** Understand why V vertices are extracted → O(V log V) overhead
- [ ] **1.4.4** Derive O((V+E) log V) complexity from above
- [ ] **1.4.5** Understand space complexity: O(V) for distance and processed arrays
- [ ] **1.4.6** Know when Dijkstra is faster than alternatives:
  - [ ] Faster than BFS? (BFS is O(V+E), so only if E is small)
  - [ ] Faster than Bellman–Ford? (Bellman–Ford is O(VE), so Dijkstra wins unless E ≈ V)

### Trade-offs & Decision-Making

- [ ] **1.5.1** Understand when Dijkstra is appropriate: non-negative weights only
- [ ] **1.5.2** Understand when BFS is better: if all weights are 1 (unweighted)
- [ ] **1.5.3** Understand when Bellman–Ford is needed: if negative weights exist
- [ ] **1.5.4** Understand when Floyd–Warshall is better: if all-pairs needed and graph is dense
- [ ] **1.5.5** Compare with bidirectional search: Why it's ~2x faster for point-to-point queries
- [ ] **1.5.6** Understand A* heuristic (conceptually): How it guides search toward target

### Practice Problems

- [ ] **1.6.1** Solve LeetCode #743 (Network Delay Time) — basic Dijkstra
- [ ] **1.6.2** Solve LeetCode #1631 (Path with Minimum Effort) — Dijkstra variant
- [ ] **1.6.3** Solve LeetCode #778 (Swim in Rising Water) — Dijkstra on transformed weights
- [ ] **1.6.4** Solve one custom problem: Create a graph, find shortest path using your code
- [ ] **1.6.5** Verify solutions: Run on given test cases; check edge cases

### Real-World Applications

- [ ] **1.7.1** Understand GPS navigation: How Dijkstra computes routes
- [ ] **1.7.2** Understand network routing: How OSPF uses shortest paths
- [ ] **1.7.3** Understand social networks: How to find shortest connection chains
- [ ] **1.7.4** Understand game AI: How NPCs path-find using Dijkstra

### Interview Preparation

- [ ] **1.8.1** Can explain Dijkstra to a peer in 2 minutes
- [ ] **1.8.2** Can code Dijkstra in < 15 minutes
- [ ] **1.8.3** Can articulate when Dijkstra fails (negative weights)
- [ ] **1.8.4** Can solve a Dijkstra variant interview question
- [ ] **1.8.5** Can optimize for follow-up: "What if you want actual paths, not just distances?"

### Day 1 Checkpoint

**Total items:** 65  
**Target:** ≥52 items checked (≥80%)

- [ ] **Final check:** Do you feel confident about Dijkstra's algorithm?
  - [ ] YES, move to Day 2
  - [ ] NO, review conceptual sections and implement again

---

## 🎯 DAY 2: BELLMAN–FORD & NEGATIVE WEIGHTS (Single-Source, Handles Negatives)

### Conceptual Understanding

- [ ] **2.1.1** Understand the problem: Find shortest paths even with negative-weight edges
- [ ] **2.1.2** Understand the constraint: Must detect negative cycles
- [ ] **2.1.3** Understand why Dijkstra fails: Greedy no longer optimal; a later edge might improve a "finalized" distance
- [ ] **2.1.4** Understand relaxation principle: Same as Dijkstra, but applied repeatedly
- [ ] **2.1.5** Understand V-1 passes: Why this number ensures convergence
- [ ] **2.1.6** Understand negative cycle detection: Extra pass shows if distances still improve
- [ ] **2.1.7** Understand DP interpretation: Path lengths as DP states; k-th pass finds shortest paths with ≤ k edges

### Visual Learning & Tracing

- [ ] **2.2.1** Trace Bellman–Ford on provided graph with negative edges (from instructional material)
- [ ] **2.2.2** Manually compute distances after each pass
- [ ] **2.2.3** Observe how distance improvements propagate over passes
- [ ] **2.2.4** Verify final distances match manual pathfinding
- [ ] **2.2.5** Create your own graph; trace Bellman–Ford step-by-step
- [ ] **2.2.6** Understand how negatives allow better paths to emerge in later passes
- [ ] **2.2.7** Trace negative cycle detection example (see if diagonal becomes negative)

### Implementation Skills

- [ ] **2.3.1** Code basic Bellman–Ford (no priority queue; just loops)
- [ ] **2.3.2** Implement V-1 relaxation passes
- [ ] **2.3.3** For each pass, iterate over all edges and check relaxation condition
- [ ] **2.3.4** Implement negative cycle detection (one more pass checking for improvements)
- [ ] **2.3.5** Implement path reconstruction (predecessor pointers)
- [ ] **2.3.6** Test on graphs with negative edges; verify distances
- [ ] **2.3.7** Test on graphs with negative cycles; verify cycle detection
- [ ] **2.3.8** Handle edge cases: disconnected graphs, self-loops, very negative edges
- [ ] **2.3.9** Code runs without errors; produces correct output

### Complexity Understanding

- [ ] **2.4.1** Understand why V-1 passes are needed: Longest simple path has V-1 edges
- [ ] **2.4.2** Understand why each pass is O(E): Process all E edges once per pass
- [ ] **2.4.3** Derive O(VE) complexity: V-1 passes × E edges per pass
- [ ] **2.4.4** Understand space complexity: O(V) for distances and predecessors
- [ ] **2.4.5** Compare with Dijkstra: O(VE) vs. O((V+E) log V); when Bellman–Ford is competitive

### Trade-offs & Decision-Making

- [ ] **2.5.1** Understand when Bellman–Ford is necessary: Negative weights present
- [ ] **2.5.2** Understand when Dijkstra is better: If weights are non-negative
- [ ] **2.5.3** Understand when Floyd–Warshall is better: If all-pairs and negatives both needed
- [ ] **2.5.4** Understand complexity trade-offs: O(VE) is slow for dense graphs
- [ ] **2.5.5** Compare with Bellman–Ford × V for all-pairs: O(V²E) vs. Floyd–Warshall O(V³)

### Practice Problems

- [ ] **2.6.1** Solve LeetCode #787 (Cheapest Flights Within K Stops) — Bellman–Ford variant
- [ ] **2.6.2** Create and solve: Graph with negative weights; find shortest path
- [ ] **2.6.3** Create and solve: Graph with negative cycle; detect it
- [ ] **2.6.4** Compare solution with Dijkstra attempt; see why Dijkstra fails

### Real-World Applications

- [ ] **2.7.1** Understand currency exchange arbitrage: How negative cycles represent profits
- [ ] **2.7.2** Understand ride-sharing with credits: How debits/credits appear as negative edges
- [ ] **2.7.3** Understand network protocols: Why some routing protocols tolerate negatives

### Interview Preparation

- [ ] **2.8.1** Can explain Bellman–Ford to a peer in 2 minutes
- [ ] **2.8.2** Can code Bellman–Ford in < 10 minutes
- [ ] **2.8.3** Can articulate why Dijkstra fails on negatives
- [ ] **2.8.4** Can detect and explain negative cycles
- [ ] **2.8.5** Can compare Bellman–Ford vs. Dijkstra in interview

### Day 2 Checkpoint

**Total items:** 48  
**Target:** ≥38 items checked (≥80%)

- [ ] **Final check:** Do you understand Bellman–Ford and when to use it?
  - [ ] YES, move to Day 3
  - [ ] NO, review and re-implement

---

## 🎯 DAY 3: FLOYD–WARSHALL & ALL-PAIRS SHORTEST PATHS

### Conceptual Understanding

- [ ] **3.1.1** Understand the problem: Find shortest paths between ALL pairs of vertices
- [ ] **3.1.2** Understand DP formulation: dist[i][j][k] = shortest using vertices {0..k-1}
- [ ] **3.1.3** Understand k-dimension: Gradually allow using more intermediate vertices
- [ ] **3.1.4** Understand recurrence: min(skip k, use k) = min(dist[i][j][k-1], dist[i][k] + dist[k][j])
- [ ] **3.1.5** Understand k-loop ordering: Must be outermost; controls intermediate vertex set
- [ ] **3.1.6** Understand space optimization: Reuse 2D matrix; update in-place safely
- [ ] **3.1.7** Understand negative cycle detection: Check diagonal; if dist[i][i] < 0, cycle exists

### Visual Learning & Tracing

- [ ] **3.2.1** Trace Floyd–Warshall on provided 4-vertex graph (from instructional material)
- [ ] **3.2.2** Manually compute k=0, k=1, k=2, k=3 iterations
- [ ] **3.2.3** Watch distance matrix evolve; understand how paths improve with more intermediates
- [ ] **3.2.4** Verify final matrix matches manual all-pairs calculation
- [ ] **3.2.5** Create your own 3-vertex graph; trace Floyd–Warshall step-by-step
- [ ] **3.2.6** Understand how (i,j) distance improves when intermediate k connects better paths
- [ ] **3.2.7** Trace negative cycle example; see diagonal become negative

### Implementation Skills

- [ ] **3.3.1** Code basic Floyd–Warshall (triple-nested loop)
- [ ] **3.3.2** Initialize distance matrix: direct edges have weights, non-edges are ∞, diagonal is 0
- [ ] **3.3.3** Implement k-loop as OUTERMOST loop (critical!)
- [ ] **3.3.4** Implement i,j loops inside k-loop
- [ ] **3.3.5** Implement relaxation: dist[i][j] = min(dist[i][j], dist[i][k] + dist[k][j])
- [ ] **3.3.6** Implement path reconstruction (next[i][j] array)
- [ ] **3.3.7** Implement negative cycle detection
- [ ] **3.3.8** Test on various graphs; verify all-pairs distances
- [ ] **3.3.9** Code runs without errors; produces correct output

### Complexity Understanding

- [ ] **3.4.1** Understand triple-nested loop: O(V × V × V) = O(V³)
- [ ] **3.4.2** Understand why k-loop must be outermost: DP dependency structure
- [ ] **3.4.3** Derive O(V³) time complexity
- [ ] **3.4.4** Understand O(V²) space: Single 2D matrix
- [ ] **3.4.5** Compare with Dijkstra × V: O(V(V+E) log V); when Floyd–Warshall wins
- [ ] **3.4.6** Compare with Bellman–Ford × V: O(V²E); when Floyd–Warshall wins

### Trade-offs & Decision-Making

- [ ] **3.5.1** Understand when Floyd–Warshall is best: All-pairs needed; small V; dense graph
- [ ] **3.5.2** Understand when Dijkstra × V is better: Sparse graph; need flexibility
- [ ] **3.5.3** Understand when Bellman–Ford × V is needed: Negative weights + all-pairs
- [ ] **3.5.4** Compare space: Floyd–Warshall O(V²) vs. running Dijkstra V times O(V) per iteration

### Practice Problems

- [ ] **3.6.1** Solve LeetCode #1334 (Design an Expression With Operators to Get Target Value) — or similar all-pairs
- [ ] **3.6.2** Create and solve: 4-vertex graph; compute all-pairs distances using Floyd–Warshall
- [ ] **3.6.3** Compare solution with running Dijkstra 4 times; see if results match
- [ ] **3.6.4** Solve: Find network diameter (max shortest path between any pair)

### Real-World Applications

- [ ] **3.7.1** Understand network reliability: All-pairs distances reveal network structure
- [ ] **3.7.2** Understand social networks: Degrees of separation between all user pairs
- [ ] **3.7.3** Understand compiler optimization: Transitive closure for data-flow analysis

### Interview Preparation

- [ ] **3.8.1** Can explain Floyd–Warshall to a peer in 2 minutes
- [ ] **3.8.2** Can code Floyd–Warshall in < 10 minutes
- [ ] **3.8.3** Can articulate why k-loop must be outermost
- [ ] **3.8.4** Can detect negative cycles from final matrix
- [ ] **3.8.5** Can recommend Floyd–Warshall vs. alternatives in interview

### Day 3 Checkpoint

**Total items:** 45  
**Target:** ≥36 items checked (≥80%)

- [ ] **Final check:** Do you grasp Floyd–Warshall's DP structure?
  - [ ] YES, move to Day 4
  - [ ] NO, re-read Chapter 2 (Mental Model) and re-implement

---

## 🎯 DAY 4: MINIMUM SPANNING TREES (Kruskal & Prim)

### Conceptual Understanding (MST Definition & Theory)

- [ ] **4.1.1** Understand MST definition: Spanning tree with minimum total weight
- [ ] **4.1.2** Understand spanning tree: All vertices connected; exactly V-1 edges; no cycles
- [ ] **4.1.3** Understand why MST is important: Optimal network design, clustering
- [ ] **4.1.4** Understand cut property: Greedy choice along any cut is part of some MST
- [ ] **4.1.5** Understand uniqueness: MST may not be unique if edges have duplicate weights
- [ ] **4.1.6** Understand on disconnected graphs: "MST" is actually a forest of MSTs per component

### Kruskal's Algorithm: Understanding

- [ ] **4.2.1** Understand the idea: Sort edges by weight; add greedily if no cycle
- [ ] **4.2.2** Understand cycle detection: Use DSU (union-find) to check
- [ ] **4.2.3** Understand why it works: Cut property ensures greedy choice is safe
- [ ] **4.2.4** Understand the steps: (1) Sort edges, (2) Initialize DSU, (3) For each edge, (4) If find(u) != find(v), add edge and union
- [ ] **4.2.5** Understand why DSU is perfect for this: O(α(n)) operations make it efficient

### Kruskal's Algorithm: Visual & Implementation

- [ ] **4.3.1** Trace Kruskal on provided example graph
- [ ] **4.3.2** Manually verify which edges form MST
- [ ] **4.3.3** Create your own weighted graph; trace Kruskal step-by-step
- [ ] **4.3.4** Code Kruskal from scratch
- [ ] **4.3.5** Integrate DSU (union-find) for cycle detection
- [ ] **4.3.6** Test on various graphs; verify MST weight
- [ ] **4.3.7** Handle edge cases: disconnected graph (forest), duplicate weights, self-loops
- [ ] **4.3.8** Understand tie-breaking: If edges have equal weight, order doesn't affect MST weight but may affect which edges chosen

### Prim's Algorithm: Understanding

- [ ] **4.4.1** Understand the idea: Start from a vertex; grow tree by adding cheapest edge to unvisited
- [ ] **4.4.2** Understand the steps: (1) Initialize with arbitrary vertex, (2) Maintain frontier, (3) Repeatedly add cheapest edge to unvisited
- [ ] **4.4.3** Understand similarity to Dijkstra: Both maintain frontier using priority queue
- [ ] **4.4.4** Understand advantage: No sorting needed (unlike Kruskal)

### Prim's Algorithm: Visual & Implementation

- [ ] **4.5.1** Trace Prim on provided example graph
- [ ] **4.5.2** Manually verify which edges form MST
- [ ] **4.5.3** Create your own weighted graph; trace Prim step-by-step
- [ ] **4.5.4** Code Prim from scratch
- [ ] **4.5.5** Use priority queue to maintain frontier
- [ ] **4.5.6** Test on various graphs; verify MST weight
- [ ] **4.5.7** Handle edge cases: disconnected graph, duplicate weights

### Comparison: Kruskal vs. Prim

- [ ] **4.6.1** Compare time complexity: Kruskal O(E log E), Prim O((V+E) log V)
- [ ] **4.6.2** Understand when Kruskal is better: Sparse graphs; edges dominate
- [ ] **4.6.3** Understand when Prim is better: Dense graphs; vertices dominate
- [ ] **4.6.4** Understand implementation differences: Kruskal edge-centric, Prim vertex-centric
- [ ] **4.6.5** Verify both produce same MST weight on test graph

### Practice Problems

- [ ] **4.7.1** Solve LeetCode #1135 (Connecting Cities With Minimum Cost) — Kruskal's MST
- [ ] **4.7.2** Solve LeetCode #1584 (Min Cost to Connect All Points) — Kruskal or Prim
- [ ] **4.7.3** Create and solve: Custom graph; find MST using both algorithms
- [ ] **4.7.4** Verify both produce same MST weight

### Real-World Applications

- [ ] **4.8.1** Understand network design: MST minimizes cable/link costs
- [ ] **4.8.2** Understand clustering: MST groups similar items
- [ ] **4.8.3** Understand phylogenetic trees: MST models evolutionary relationships

### Interview Preparation

- [ ] **4.9.1** Can explain MST problem and why it matters
- [ ] **4.9.2** Can code Kruskal in < 12 minutes (with DSU)
- [ ] **4.9.3** Can code Prim in < 12 minutes (with priority queue)
- [ ] **4.9.4** Can explain cut property intuitively
- [ ] **4.9.5** Can choose Kruskal vs. Prim for given graph structure

### Day 4 Checkpoint

**Total items:** 52  
**Target:** ≥42 items checked (≥80%)

- [ ] **Final check:** Do you understand both Kruskal and Prim?
  - [ ] YES, move to Day 5
  - [ ] NO, review and implement again

---

## 🎯 DAY 5: DISJOINT SET UNION (DSU) / UNION–FIND IN DEPTH

### Conceptual Understanding

- [ ] **5.1.1** Understand the problem: Maintain dynamic connectivity; answer "are A and B connected?"
- [ ] **5.1.2** Understand the data structure: Forest of trees; root = set representative
- [ ] **5.1.3** Understand make_set: Create singleton set for each element
- [ ] **5.1.4** Understand find: Return root of element's tree; path indicates set membership
- [ ] **5.1.5** Understand union: Merge two trees by attaching one root to the other
- [ ] **5.1.6** Understand rank: Approximate tree height; used to decide which root becomes parent
- [ ] **5.1.7** Understand path compression: Flatten tree by making nodes point directly to root

### Visual Learning & Tracing

- [ ] **5.2.1** Trace DSU on provided 8-element example with 9 operations (from instructional material)
- [ ] **5.2.2** Manually draw trees after each union operation
- [ ] **5.2.3** Understand how union-by-rank prevents tall trees
- [ ] **5.2.4** Trace path compression: See how trees flatten after find operations
- [ ] **5.2.5** Create your own 6-element DSU; trace step-by-step
- [ ] **5.2.6** Trace cycle detection example (see when find returns same root)
- [ ] **5.2.7** Verify that parent arrays and tree visualizations match

### Implementation Skills

- [ ] **5.3.1** Code make_set(x): parent[x] = x, rank[x] = 0
- [ ] **5.3.2** Code find(x) with path compression (recursive): if parent[x] != x: parent[x] = find(parent[x])
- [ ] **5.3.3** Code find(x) iterative variant (alternative)
- [ ] **5.3.4** Code union(x, y) with union-by-rank
- [ ] **5.3.5** Compare ranks and attach lower-rank to higher-rank
- [ ] **5.3.6** Increment rank only when attaching equal-rank trees
- [ ] **5.3.7** Test on cycle detection: Use DSU to find when adding an edge creates a cycle
- [ ] **5.3.8** Test on connected components: Use DSU to count components after edge additions
- [ ] **5.3.9** Code runs without errors; produces correct output

### Optimization Understanding: Union-by-Rank

- [ ] **5.4.1** Understand why union-by-rank is needed: Prevents tall trees (O(n) in worst case without it)
- [ ] **5.4.2** Understand rank heuristic: Rank is upper bound on tree height
- [ ] **5.4.3** Understand lemma: If element has rank r, its subtree has ≥ 2^r elements
- [ ] **5.4.4** Derive max rank: O(log n) because 2^r ≤ n
- [ ] **5.4.5** Understand that even without path compression, union-by-rank alone gives O(log n) per operation

### Optimization Understanding: Path Compression

- [ ] **5.5.1** Understand why path compression is needed: Further reduces trees to near-flat structure
- [ ] **5.5.2** Understand mechanics: During find, make all traversed nodes point directly to root
- [ ] **5.5.3** Understand idempotence: Compressing twice is same as compressing once
- [ ] **5.5.4** Understand that even without union-by-rank, path compression alone improves amortized complexity

### Amortized Complexity

- [ ] **5.6.1** Understand that union-by-rank + path compression together achieve O(α(n))
- [ ] **5.6.2** Understand inverse Ackermann α(n): Grows so slowly that α(n) ≤ 4 for all practical n
- [ ] **5.6.3** Understand that α(n) ≤ 4 means DSU is essentially O(1) per operation
- [ ] **5.6.4** Understand amortized analysis: Expensive operations are amortized over many cheap operations
- [ ] **5.6.5** Know that no better algorithm exists for DSU (inverse Ackermann is optimal)

### Practice Problems

- [ ] **5.7.1** Solve LeetCode #684 (Redundant Connection) — Cycle detection with DSU
- [ ] **5.7.2** Solve LeetCode #721 (Accounts Merge) — Component grouping with DSU
- [ ] **5.7.3** Solve LeetCode #323 (Number of Connected Components in an Undirected Graph) — Component counting
- [ ] **5.7.4** Solve custom: Use DSU in Kruskal's MST; verify it works
- [ ] **5.7.5** Verify solutions on all test cases

### Real-World Applications

- [ ] **5.8.1** Understand social networks: DSU groups users into connected components
- [ ] **5.8.2** Understand network topology: DSU detects connected subnets
- [ ] **5.8.3** Understand percolation theory: DSU simulates clustering in random structures
- [ ] **5.8.4** Understand Kruskal's MST: DSU is backbone for cycle detection

### Interview Preparation

- [ ] **5.9.1** Can explain DSU to a peer in 2 minutes
- [ ] **5.9.2** Can code DSU with both optimizations in < 10 minutes
- [ ] **5.9.3** Can explain why union-by-rank + path compression are both needed
- [ ] **5.9.4** Can detect cycles using DSU
- [ ] **5.9.5** Can count connected components using DSU
- [ ] **5.9.6** Can use DSU in Kruskal's MST

### Day 5 Checkpoint

**Total items:** 56  
**Target:** ≥45 items checked (≥80%)

- [ ] **Final check:** Do you understand DSU deeply?
  - [ ] YES, you've mastered Week 09!
  - [ ] NO, review Chapter 2-3 and re-implement

---

## 🎯 WEEK-LONG MASTERY TRACKING

### Concepts Mastered

- [ ] Dijkstra's algorithm: Single-source shortest paths (non-negative)
- [ ] Bellman–Ford: Single-source shortest paths (negative weights allowed)
- [ ] Floyd–Warshall: All-pairs shortest paths (DP approach)
- [ ] Kruskal's MST: Edge-based greedy + DSU
- [ ] Prim's MST: Vertex-based greedy + priority queue
- [ ] DSU/Union-Find: Dynamic connectivity with near-constant operations
- [ ] Complexity trade-offs: When each algorithm is best
- [ ] Real-world applications: GPS, routing, arbitrage, clustering, connectivity

### Skills Demonstrated

- [ ] Can implement all 5 core algorithms from memory
- [ ] Can trace each algorithm on a graph step-by-step
- [ ] Can detect bugs in implementations
- [ ] Can recommend algorithm for any graph problem
- [ ] Can optimize for constraints (time, space, flexibility)
- [ ] Can solve interview questions combining multiple algorithms

### Problems Solved (≥15 total)

- [ ] ≥3 Dijkstra problems
- [ ] ≥2 Bellman–Ford problems
- [ ] ≥2 Floyd–Warshall problems
- [ ] ≥3 MST (Kruskal/Prim) problems
- [ ] ≥3 DSU problems
- [ ] ≥2 mixed problems combining algorithms

### Interview Readiness

- [ ] Can solve shortest-path interview question in 30 min
- [ ] Can solve MST interview question in 30 min
- [ ] Can solve connectivity/DSU interview question in 20 min
- [ ] Can handle follow-ups and variations
- [ ] Can explain trade-offs confidently

---

## ✅ FINAL WEEK-END SELF-CHECK

**Before moving to Week 10:**

- [ ] Completed all 5 days of checkpoints
- [ ] ≥80% items checked per day (average)
- [ ] Implemented all 5 core algorithms
- [ ] Solved ≥15 practice problems
- [ ] Understand all complexity analyses
- [ ] Can articulate when to use each algorithm
- [ ] Ready for graph algorithm interview questions
- [ ] Confident explaining algorithms to peers

**If any "NO" above:** Review that topic before moving forward.

---

## 📊 PROGRESS SUMMARY

| Day | Conceptual | Implementation | Problems | Interview Prep | Status |
|:---|:---|:---|:---|:---|:---|
| 1 | [ ] | [ ] | [ ] | [ ] | Not started / In progress / Complete |
| 2 | [ ] | [ ] | [ ] | [ ] | Not started / In progress / Complete |
| 3 | [ ] | [ ] | [ ] | [ ] | Not started / In progress / Complete |
| 4 | [ ] | [ ] | [ ] | [ ] | Not started / In progress / Complete |
| 5 | [ ] | [ ] | [ ] | [ ] | Not started / In progress / Complete |

---

**Status:** ✅ Week 09 Daily Progress Checklist — COMPLETE

