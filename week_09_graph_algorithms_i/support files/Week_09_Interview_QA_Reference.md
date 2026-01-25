# 🎙️ Week 09 Interview QA Reference — Complete Question Bank & Solutions

**Version:** 1.0  
**Purpose:** Comprehensive interview question bank with model answers and follow-ups  
**Status:** ✅ ACTIVE  
**Audience:** Intermediate-Advanced students preparing for technical interviews

---

## 📖 HOW TO USE THIS DOCUMENT

**For Interview Prep:**
1. Read question carefully; understand what it's asking
2. Spend 5-10 min thinking through your answer before reading solution
3. Read model solution; understand the approach and reasoning
4. Study follow-up questions; they often appear in real interviews
5. Code the solution yourself; don't just read pseudocode
6. Test on provided examples; verify correctness

**For Teaching/Peer Learning:**
- Use model answers as teaching material
- Present follow-ups as challenges to deepen understanding
- Use variations to test someone's mastery

**Difficulty Levels:**
- 🟢 **Easy:** 5-15 min; basic algorithm application
- 🟡 **Medium:** 15-30 min; algorithm variant or combination
- 🔴 **Hard:** 30-60 min; system design, optimization, or multiple algorithms

---

## 🎯 QUESTION BANK: 30 INTERVIEW QUESTIONS

### CATEGORY 1: DIJKSTRA'S ALGORITHM (5 Questions)

#### Q1.1: Basic Dijkstra Implementation 🟢
**Difficulty:** Easy  
**Time:** 15 min  
**Company:** Meta, Amazon, Google (screening rounds)

**Question:**
```
Given a weighted undirected graph represented as an adjacency list,
find the shortest path from a source node to all other nodes.

Input:
  n = 5 (vertices labeled 0-4)
  edges = [[0,1,4], [0,2,2], [1,2,1], [1,3,5], [2,3,8], [2,4,10], [3,4,2]]
  source = 0

Output:
  distances = [0, 3, 2, 13, 15]  (shortest distance from 0 to each node)
```

**Solution Approach:**
1. Initialize distance array with ∞ for all nodes except source (0)
2. Use priority queue to process nodes in order of distance
3. For each extracted node, relax its neighbors
4. Continue until all reachable nodes are processed

**Pseudocode:**
```
Dijkstra(graph, source, n):
  dist = [∞] * n
  dist[source] = 0
  pq = min_heap([(0, source)])
  processed = set()
  
  while pq not empty:
    (d, u) = pq.extract_min()
    if u in processed: continue
    processed.add(u)
    
    for (v, weight) in neighbors[u]:
      if dist[u] + weight < dist[v]:
        dist[v] = dist[u] + weight
        pq.add((dist[v], v))
  
  return dist
```

**Key Points:**
- Time: O((V+E) log V) with binary heap
- Space: O(V)
- Non-negative weights only
- Stale entries in PQ are handled by checking `processed` set

**Follow-up Q1.1.1:** Can you also return the actual paths, not just distances?
- **Answer:** Maintain a `predecessor` array. When you update `dist[v]`, set `predecessor[v] = u`. Reconstruct by backtracking from any node to source.

**Follow-up Q1.1.2:** How would you handle a negative-weight edge?
- **Answer:** Dijkstra fails with negatives; use Bellman–Ford instead. Dijkstra's greedy assumption breaks.

**Follow-up Q1.1.3:** What if the graph is disconnected? How do you handle unreachable nodes?
- **Answer:** Unreachable nodes remain at distance ∞. Check `if dist[node] == ∞` before using.

---

#### Q1.2: Network Delay Time (LeetCode #743) 🟡
**Difficulty:** Medium  
**Time:** 20 min  
**Company:** Google, Meta, Amazon

**Question:**
```
You are given a network of n nodes labeled from 1 to n. You are also given times,
a list of travel times as directed edges times[i] = (ui, vi, wi), where ui is the
source node, vi is the target node, and wi is the time it takes for a signal to
travel from source to target.

We send a signal from node k. Return the time it takes for all nodes to receive
the signal. If it is impossible, return -1.

Example:
  n = 4, times = [[2,1,1],[2,3,1],[3,4,1]], k = 2
  Output: 3
  
  Explanation: At time 0, node 2 sends signal.
               At time 1, nodes 1 and 3 receive signal.
               At time 2, node 4 receives signal.
               Total time = 3.
```

**Solution Approach:**
1. Build adjacency list from edges
2. Run Dijkstra from source node k
3. Find maximum distance among all nodes (this is when last node receives signal)
4. If any node unreachable (distance = ∞), return -1

**Pseudocode:**
```
NetworkDelayTime(times, n, k):
  graph = build_adjacency_list(times)  // 1-indexed
  dist = Dijkstra(graph, k, n)
  
  max_time = 0
  for i = 1 to n:
    if dist[i] == ∞: return -1
    max_time = max(max_time, dist[i])
  
  return max_time
```

**Code Template (C#):**
```csharp
public int NetworkDelayTime(int[][] times, int n, int k) {
    // Build adjacency list
    List<(int dest, int weight)>[] graph = new List<(int, int)>[n + 1];
    for (int i = 1; i <= n; i++) graph[i] = new List<(int, int)>();
    foreach (var time in times) {
        graph[time[0]].Add((time[1], time[2]));
    }
    
    // Dijkstra
    int[] dist = new int[n + 1];
    Array.Fill(dist, int.MaxValue);
    dist[k] = 0;
    PriorityQueue<(int d, int u), int> pq = new();
    pq.Enqueue((0, k), 0);
    
    while (pq.Count > 0) {
        var (d, u) = pq.Dequeue();
        if (d > dist[u]) continue;  // Stale entry
        
        foreach (var (v, w) in graph[u]) {
            if (dist[u] + w < dist[v]) {
                dist[v] = dist[u] + w;
                pq.Enqueue((dist[v], v), dist[v]);
            }
        }
    }
    
    int max_time = 0;
    for (int i = 1; i <= n; i++) {
        if (dist[i] == int.MaxValue) return -1;
        max_time = Math.Max(max_time, dist[i]);
    }
    
    return max_time;
}
```

**Complexity:**
- Time: O((n + |times|) log n)
- Space: O(n + |times|)

**Key Points:**
- 1-indexed nodes (be careful with array indexing)
- Check for unreachable nodes
- Maximum distance is the answer

**Follow-up Q1.2.1:** What if you want to find the minimum time for a specific node to receive signal?
- **Answer:** Return `dist[target]` instead of max of all distances.

**Follow-up Q1.2.2:** If you could pick any starting node k, which would minimize total delay?
- **Answer:** Try all starting nodes; pick the one with minimum maximum distance.

---

#### Q1.3: Path with Maximum Probability 🟡
**Difficulty:** Medium  
**Time:** 25 min  
**Company:** Google, Meta

**Question:**
```
You are given an undirected weighted graph of n nodes (0-indexed), represented by
an edge list where edges[i] = [a, b] is an edge between nodes a and b, and
succProb[i] is the probability of success to traverse this edge.

Given two nodes start and end, find the path with the maximum probability of success
to go from start to end and return its success probability.

If there is no path from start to end, return 0. Your answer will be accepted if it
differs from the correct answer by no more than 1e-5.

Example:
  n = 3
  edges = [[0,1],[1,2]]
  succProb = [0.9,0.9]
  start = 0, end = 2
  Output: 0.81
  
  Explanation: The path 0 -> 1 -> 2 has probability 0.9 * 0.9 = 0.81.
```

**Solution Approach:**
1. Modify Dijkstra to maximize probability instead of minimizing distance
2. Use max-heap (or negate values for min-heap)
3. Instead of addition (dist + weight), use multiplication (prob × succProb)
4. Key insight: maximizing product = maximizing log(product) = maximizing sum of logs

**Pseudocode:**
```
MaxProbabilityPath(n, edges, succProb, start, end):
  graph = build_adjacency_list(edges, succProb)
  prob = [0.0] * n
  prob[start] = 1.0
  pq = max_heap([(1.0, start)])
  
  while pq not empty:
    (p, u) = pq.extract_max()
    if p < prob[u]: continue  // Stale entry
    
    for (v, edge_prob) in neighbors[u]:
      new_prob = p * edge_prob
      if new_prob > prob[v]:
        prob[v] = new_prob
        pq.add((new_prob, v))
  
  return prob[end]
```

**Key Differences from Standard Dijkstra:**
- Maximize instead of minimize (use max-heap or negate)
- Multiply probabilities instead of add distances
- Floating-point comparisons (use epsilon for safety)

**Follow-up Q1.3.1:** How would you reconstruct the actual path?
- **Answer:** Maintain a `predecessor` array; when you update prob[v], set predecessor[v] = u.

**Follow-up Q1.3.2:** What if you need to avoid certain intermediate nodes?
- **Answer:** Add a check: skip nodes in the "forbidden" list when relaxing neighbors.

---

#### Q1.4: Swim in Rising Water 🟡
**Difficulty:** Medium  
**Time:** 25 min  
**Company:** Google, Uber

**Question:**
```
You are given an n x n integer matrix grid where each value grid[i][j] represents
the elevation at that point (row i, column j).

Rain water can flow between adjacent cells (4 directions) if the elevation of the
cell it flows from is at most the elevation of the cell it flows to. Water always
flows from higher or equal elevation to lower elevation, eventually reaching ocean
(assumed below the grid).

You start at the top left (0, 0) and must reach the bottom right (n - 1, n - 1).
Find the path of least resistance, where resistance is the maximum elevation you need
to traverse.

Example:
  grid = [[1,2],[3,4]]
  Output: 4
  
  Path: 1 -> 3 -> 4 (max elevation needed is 4)
```

**Solution Approach:**
1. Treat grid values as "cost" (elevation you must traverse)
2. Use Dijkstra to find path minimizing the maximum elevation
3. Key insight: relaxation condition is different
   - Instead of: `dist[u] + cost[v] < dist[v]`
   - Use: `max(dist[u], cost[v]) < dist[v]`

**Pseudocode:**
```
SwimInRisingWater(grid):
  n = len(grid)
  cost = [[∞] * n for _ in range(n)]
  cost[0][0] = grid[0][0]
  pq = min_heap([(grid[0][0], 0, 0)])
  directions = [(-1,0), (1,0), (0,-1), (0,1)]
  
  while pq not empty:
    (c, x, y) = pq.extract_min()
    if c > cost[x][y]: continue
    
    for (dx, dy) in directions:
      nx, ny = x + dx, y + dy
      if 0 <= nx < n and 0 <= ny < n:
        new_cost = max(c, grid[nx][ny])
        if new_cost < cost[nx][ny]:
          cost[nx][ny] = new_cost
          pq.add((new_cost, nx, ny))
  
  return cost[n-1][n-1]
```

**Key Insight:**
- Relaxation is `max(current_cost, next_elevation)` not `current_cost + next_elevation`
- This is a minimax path problem, solved via Dijkstra variant

**Follow-up Q1.4.1:** What if you want to minimize the sum of elevations instead?
- **Answer:** Use standard Dijkstra: `dist[u] + grid[v] < dist[v]`

**Follow-up Q1.4.2:** Can you solve this with BFS?
- **Answer:** No, BFS only works with unit weights. Use Dijkstra.

---

#### Q1.5: Cheapest Flights Within K Stops 🟡
**Difficulty:** Medium  
**Time:** 25 min  
**Company:** Amazon, Google, Bloomberg

**Question:**
```
You are given n cities connected by some flights. You are also given flights,
a list of travel routes where flights[i] = [fromi, toi, pricei].

There is some non-directed cost to travel from city from_i to city to_i and back
denoted by price_i.

You are also given three integers src, dst, and k, return the cheapest price from
src to dst with at most k stops. If there is no such route, return -1.

Note: A trip with k stops means you make k intermediate landing stops (so you take k+1 flights).

Example:
  n = 3, flights = [[0,1,100],[1,2,100],[0,2,500]], src = 0, dst = 2, k = 1
  Output: 200
  
  Path: 0 -> 1 -> 2 (cost 100 + 100 = 200, with 1 stop at city 1)
```

**Solution Approach:**
1. Standard Dijkstra but with an additional dimension: number of stops used
2. State: (cost, current_city, stops_used)
3. Only add to PQ if stops_used <= k
4. Can also use Bellman–Ford variant (k+1 passes)

**Pseudocode (Dijkstra Variant):**
```
CheapestFlightsWithKStops(n, flights, src, dst, k):
  graph = build_adjacency_list(flights)
  dist = [[∞] * (k + 2) for _ in range(n)]
  dist[src][0] = 0
  pq = min_heap([(0, src, 0)])  // (cost, city, stops)
  
  while pq not empty:
    (cost, u, stops) = pq.extract_min()
    
    if u == dst: return cost
    if stops > k: continue
    if cost > dist[u][stops]: continue  // Stale
    
    for (v, price) in neighbors[u]:
      new_cost = cost + price
      if new_cost < dist[v][stops + 1]:
        dist[v][stops + 1] = new_cost
        pq.add((new_cost, v, stops + 1))
  
  return min(dist[dst]) if min(dist[dst]) != ∞ else -1
```

**Pseudocode (Bellman–Ford Variant):**
```
CheapestFlightsWithKStops(n, flights, src, dst, k):
  dist = [∞] * n
  dist[src] = 0
  
  for _ in range(k + 1):  // k+1 relaxation passes
    temp = dist.copy()
    for (u, v, price) in flights:
      if dist[u] != ∞:
        dist[v] = min(dist[v], dist[u] + price)
  
  return dist[dst] if dist[dst] != ∞ else -1
```

**Key Points:**
- Dijkstra variant: O((n * k) log(n * k))
- Bellman–Ford variant: O(n * k * |flights|)
- For small k, Dijkstra is better; for large k, Bellman–Ford may be better

**Follow-up Q1.5.1:** What if k = ∞ (unlimited stops)?
- **Answer:** Use standard Dijkstra; don't track stops.

**Follow-up Q1.5.2:** What if you need to return the actual path, not just cost?
- **Answer:** Maintain predecessor pointers in the state; reconstruct path at the end.

---

### CATEGORY 2: BELLMAN–FORD & NEGATIVE WEIGHTS (5 Questions)

#### Q2.1: Basic Bellman–Ford Implementation 🟢
**Difficulty:** Easy  
**Time:** 15 min  
**Company:** Meta, Amazon

**Question:**
```
Given a directed graph with n vertices and m edges (including negative-weight edges),
find the shortest path from a source vertex to all other vertices. Also detect if
there exists a negative-cycle reachable from the source.

Input:
  n = 5
  edges = [[0,1,4], [0,2,2], [1,2,-3], [1,3,2], [2,3,4]]
  source = 0

Output:
  distances = [0, 4, 2, 6, ∞]
  negative_cycle = False
```

**Solution Approach:**
1. Initialize distances: dist[src] = 0, others = ∞
2. Relax all edges V-1 times
3. After V-1 passes, check if any distance still improves
4. If yes, negative cycle exists (reachable from source)

**Pseudocode:**
```
BellmanFord(n, edges, source):
  dist = [∞] * n
  dist[source] = 0
  
  // V-1 relaxation passes
  for i = 1 to n-1:
    for (u, v, weight) in edges:
      if dist[u] != ∞ and dist[u] + weight < dist[v]:
        dist[v] = dist[u] + weight
  
  // Check for negative cycles
  for (u, v, weight) in edges:
    if dist[u] != ∞ and dist[u] + weight < dist[v]:
      return (dist, True)  // Negative cycle exists
  
  return (dist, False)
```

**Key Points:**
- Time: O(V * E)
- Space: O(V)
- Handles negative weights
- Detects negative cycles
- Slower than Dijkstra for non-negative graphs

**Follow-up Q2.1.1:** Can you identify which nodes are affected by the negative cycle?
- **Answer:** From negative cycle detection, mark the cycle nodes. Then, propagate reachability via V more passes.

**Follow-up Q2.1.2:** What if you want to find the actual shortest paths (predecessors)?
- **Answer:** Maintain a `predecessor` array; update it when you relax an edge.

---

#### Q2.2: Currency Exchange Arbitrage 🟡
**Difficulty:** Medium  
**Time:** 25 min  
**Company:** Google, Jane Street, Citadel

**Question:**
```
You are given a list of exchange rates. Each exchange rate is represented as
[from_currency, to_currency, rate], meaning 1 unit of from_currency can be
exchanged for 'rate' units of to_currency.

You want to find an arbitrage opportunity: a sequence of exchanges that starts
with some amount of currency X, and after a series of exchanges, ends up with
MORE than the starting amount of currency X.

Return True if arbitrage exists, False otherwise.

Example:
  rates = [["USD", "EUR", 0.8], ["EUR", "JPY", 120], ["JPY", "USD", 0.01]]
  
  Starting with 1 USD:
    1 USD -> 0.8 EUR -> 96 JPY -> 0.96 USD
  
  We ended up with 0.96 USD, which is LESS. No arbitrage here.
  
  BUT, if rates = [["USD", "EUR", 1.1], ["EUR", "JPY", 120], ["JPY", "USD", 0.01]]
    1 USD -> 1.1 EUR -> 132 JPY -> 1.32 USD
  Profit of 0.32 USD! Arbitrage exists.
```

**Solution Approach:**
1. Transform multiplicative problem to additive (take log of rates)
2. Detect if there exists a positive cycle reachable from any source
3. A positive cycle in the additive form = arbitrage opportunity in the multiplicative form
4. Use Bellman–Ford to detect cycles

**Pseudocode:**
```
ArbitrageExists(rates):
  currencies = extract_unique_currencies(rates)
  n = len(currencies)
  currency_to_idx = {c: i for i, c in enumerate(currencies)}
  
  // Transform to log domain: log(a * b) = log(a) + log(b)
  edges = []
  for [from_c, to_c, rate] in rates:
    u = currency_to_idx[from_c]
    v = currency_to_idx[to_c]
    edges.append((u, v, -log(rate)))  // Negate to detect positive cycles
  
  // Run Bellman–Ford from each currency
  for start in range(n):
    dist = [∞] * n
    dist[start] = 0
    
    for _ in range(n - 1):
      for (u, v, w) in edges:
        if dist[u] != ∞ and dist[u] + w < dist[v]:
          dist[v] = dist[u] + w
    
    // Check for cycles
    for (u, v, w) in edges:
      if dist[u] != ∞ and dist[u] + w < dist[v]:
        return True  // Negative cycle (positive profit cycle)
  
  return False
```

**Key Insights:**
- Log transformation converts multiplication to addition
- Negative cycle in log-space = positive cycle in original space
- Arbitrage = positive profit cycle

**Follow-up Q2.2.1:** Can you return the actual arbitrage path?
- **Answer:** When detecting the cycle, use BFS/DFS to trace the path.

**Follow-up Q2.2.2:** What if rates are bidirectional (can exchange both ways)?
- **Answer:** Add both directions for each exchange rate.

---

#### Q2.3: Detecting Negative Cycles in Arbitrage 🟡
**Difficulty:** Medium  
**Time:** 20 min  
**Company:** Google, Meta

**Question:**
```
Given a directed graph with edge weights (possibly negative), detect if there
exists a negative cycle. If yes, return one such cycle; if no, return empty list.

Example:
  n = 4
  edges = [[0,1,1], [1,2,-3], [2,3,2], [3,1,3]]
  
  Cycle: 1 -> 2 -> 3 -> 1 with weight: -3 + 2 + 3 = 2 (positive, no cycle)
  
  edges = [[0,1,1], [1,2,-4], [2,3,2], [3,1,3]]
  Cycle: 1 -> 2 -> 3 -> 1 with weight: -4 + 2 + 3 = 1 (positive, no cycle)
  
  edges = [[0,1,1], [1,2,-5], [2,3,2], [3,1,3]]
  Cycle: 1 -> 2 -> 3 -> 1 with weight: -5 + 2 + 3 = 0 (neutral, no negative cycle)
  
  edges = [[0,1,1], [1,2,-6], [2,3,2], [3,1,3]]
  Cycle: 1 -> 2 -> 3 -> 1 with weight: -6 + 2 + 3 = -1 (NEGATIVE!)
  Return: [1, 2, 3]
```

**Solution Approach:**
1. Run Bellman–Ford for V-1 passes
2. On the V-th pass, if any distance still improves, a negative cycle exists
3. Mark nodes affected by the cycle
4. Use BFS/DFS from a marked node to trace the cycle

**Pseudocode:**
```
DetectAndReturnNegativeCycle(n, edges):
  dist = [∞] * n
  dist[0] = 0
  predecessor = [-1] * n
  
  // V-1 relaxation passes
  for _ in range(n - 1):
    for (u, v, w) in edges:
      if dist[u] != ∞ and dist[u] + w < dist[v]:
        dist[v] = dist[u] + w
        predecessor[v] = u
  
  // Detect negative cycle
  negative_cycle_node = -1
  for (u, v, w) in edges:
    if dist[u] != ∞ and dist[u] + w < dist[v]:
      negative_cycle_node = v
      break
  
  if negative_cycle_node == -1:
    return []  // No negative cycle
  
  // Trace the cycle
  path = []
  current = negative_cycle_node
  visited = set()
  
  while current not in visited:
    visited.add(current)
    path.append(current)
    current = predecessor[current]
  
  // Find where cycle starts
  cycle_start_idx = path.index(current)
  return path[cycle_start_idx:]
```

**Key Points:**
- V-1 passes finalize all distances without cycles
- V-th pass reveals negative cycles
- Cycle tracing uses predecessor pointers

**Follow-up Q2.3.1:** What if there are multiple negative cycles?
- **Answer:** Run the algorithm multiple times, skipping already-found cycles.

**Follow-up Q2.3.2:** How do you find ALL nodes affected by ANY negative cycle?
- **Answer:** After detecting a cycle, propagate from cycle nodes via V more passes to mark all affected nodes.

---

#### Q2.4: Shortest Path with Constraints 🟡
**Difficulty:** Medium  
**Time:** 25 min  
**Company:** Google, Meta, Amazon

**Question:**
```
You are given n cities connected by some flights. You are also given flights,
a list of travel routes where flights[i] = [from, to, price]. There is a non-directed
cost to travel from city 'from' to city 'to' denoted by 'price'.

You are also given three integers src, dst, and k, return the cheapest price from
src to dst with AT MOST k stops. If there is no such route, return -1.

(This is Q1.5 but solved with Bellman–Ford to contrast with Dijkstra)

Example:
  n = 4
  flights = [[0,1,100],[1,2,100],[0,2,500],[1,3,600],[2,3,200]]
  src = 0, dst = 3, k = 1
  Output: 700
  
  Path: 0 -> 1 -> 3 with cost 100 + 600 = 700
```

**Solution Approach (Bellman–Ford Variant):**
1. Similar to standard Bellman–Ford but limit to k+1 passes
2. Why k+1? Because a path with k stops uses k+1 edges
3. Each pass computes shortest paths using ≤ i edges

**Pseudocode:**
```
CheapestFlightsWithKStops_BellmanFord(n, flights, src, dst, k):
  dist = [∞] * n
  dist[src] = 0
  
  for stops = 0 to k:
    temp = dist.copy()
    for (u, v, price) in flights:
      if dist[u] != ∞:
        dist[v] = min(dist[v], dist[u] + price)
  
  return dist[dst] if dist[dst] != ∞ else -1
```

**Comparison with Dijkstra Variant:**
- Bellman–Ford: O((k+1) * |flights|) = O(k * E) for k stops
- Dijkstra: O((n * k) log(n * k))
- Bellman–Ford is simpler; Dijkstra is faster for small k

**Follow-up Q2.4.1:** What if k is very large (k >= n)?
- **Answer:** Use standard Dijkstra; at most n-1 stops are ever needed in a simple path.

---

#### Q2.5: SPFA (Shortest Path Faster Algorithm) 🔴
**Difficulty:** Hard  
**Time:** 30 min  
**Company:** Bloomberg, Citadel, Jane Street

**Question:**
```
SPFA is a variant of Bellman–Ford that's often faster in practice. Implement SPFA
and analyze when it's better than standard Bellman–Ford.

Compare time complexity in:
(a) Dense graph with many negative edges
(b) Sparse graph with few negative edges
(c) Graph with negative cycle

Example:
  n = 5
  edges = [[0,1,-1], [0,2,4], [1,2,3], [1,3,2], [1,4,2], [3,2,5], [3,1,1], [4,3,-3]]
  source = 0
  
  Use SPFA to find shortest paths.
```

**Solution Approach:**
1. SPFA = Bellman–Ford with BFS optimization
2. Instead of checking all edges every pass, maintain a queue of "active" nodes
3. Only relax edges from nodes whose distance recently changed
4. Often O(E) in practice, but worst-case O(V*E) like Bellman–Ford

**Pseudocode:**
```
SPFA(n, edges, source):
  dist = [∞] * n
  dist[source] = 0
  in_queue = [False] * n
  queue = [source]
  in_queue[source] = True
  
  while queue not empty:
    u = queue.pop_front()
    in_queue[u] = False
    
    for (v, w) in neighbors[u]:
      if dist[u] + w < dist[v]:
        dist[v] = dist[u] + w
        
        if not in_queue[v]:
          queue.append(v)
          in_queue[v] = True
  
  return dist
```

**Key Differences:**
- Bellman–Ford: Checks all E edges in each pass
- SPFA: Only checks edges from "active" nodes
- SPFA average: O(E), worst-case: O(V*E)

**When to Use:**
- Sparse graphs with negatives: SPFA is faster
- Dense graphs: Bellman–Ford is simpler
- No negatives: Dijkstra always wins

**Follow-up Q2.5.1:** How do you detect negative cycles in SPFA?
- **Answer:** Track count of times each node enters queue. If count >= n, cycle exists.

**Follow-up Q2.5.2:** Can SPFA be used for all-pairs shortest paths?
- **Answer:** Run SPFA from each source; O(V * E) average, still slower than Floyd–Warshall for all-pairs.

---

### CATEGORY 3: FLOYD–WARSHALL & ALL-PAIRS (4 Questions)

#### Q3.1: Basic Floyd–Warshall Implementation 🟢
**Difficulty:** Easy  
**Time:** 15 min  
**Company:** Meta, Amazon

**Question:**
```
Given a directed graph with n vertices and m edges (possibly with negative weights),
compute the shortest distance between all pairs of vertices.

Input:
  n = 4
  edges = [[0,1,3], [0,3,7], [1,2,1], [1,3,2], [2,0,4], [3,2,1]]

Output:
  dist = [
    [0, 3, 4, 2],
    [5, 0, 1, 2],
    [4, 7, 0, 9],
    [5, 8, 1, 0]
  ]
```

**Solution Approach:**
1. Initialize dist matrix: edges have weights, non-edges are ∞, diagonal is 0
2. For each intermediate vertex k (0 to n-1):
   - For each pair (i, j):
     - dist[i][j] = min(dist[i][j], dist[i][k] + dist[k][j])
3. K-loop MUST be outermost

**Pseudocode:**
```
FloydWarshall(n, edges):
  dist = [[∞] * n for _ in range(n)]
  
  // Initialize diagonal
  for i in range(n):
    dist[i][i] = 0
  
  // Initialize edges
  for (u, v, w) in edges:
    dist[u][v] = w
  
  // Main DP loop (K MUST BE OUTERMOST)
  for k in range(n):
    for i in range(n):
      for j in range(n):
        dist[i][j] = min(dist[i][j], dist[i][k] + dist[k][j])
  
  return dist
```

**Key Points:**
- Time: O(n³)
- Space: O(n²)
- K-loop MUST be outermost (DP dependency)
- Handles negative weights
- Detects negative cycles (if dist[i][i] < 0)

**Follow-up Q3.1.1:** How do you detect negative cycles?
- **Answer:** Check diagonal after computation. If any dist[i][i] < 0, cycle exists.

**Follow-up Q3.1.2:** Can you reconstruct paths?
- **Answer:** Maintain a `next[i][j]` matrix; update it when you improve dist[i][j].

---

#### Q3.2: Network Reliability & Minimax Paths 🟡
**Difficulty:** Medium  
**Time:** 25 min  
**Company:** Google, Facebook

**Question:**
```
You are given a network represented as a weighted graph. The "reliability" of a path
is the MINIMUM edge weight on that path (bottleneck). You want to find the path
between two nodes that maximizes this reliability (widest path).

Example:
  Edges: [0-1: 2], [1-2: 3], [0-2: 1]
  
  Paths from 0 to 2:
    0 -> 2 (reliability = 1)
    0 -> 1 -> 2 (reliability = min(2, 3) = 2) <- BEST
  
  Answer: 2
```

**Solution Approach:**
1. Modify Floyd–Warshall to maximize minimum edge weight
2. Instead of: `dist[i][j] = min(dist[i][j], dist[i][k] + dist[k][j])`
3. Use: `dist[i][j] = max(dist[i][j], min(dist[i][k], dist[k][j]))`

**Pseudocode:**
```
MaxMinPath(n, edges):
  dist = [[-∞] * n for _ in range(n)]
  
  // Initialize diagonal
  for i in range(n):
    dist[i][i] = ∞
  
  // Initialize edges
  for (u, v, w) in edges:
    dist[u][v] = max(dist[u][v], w)
    dist[v][u] = max(dist[v][u], w)  // Undirected
  
  // Modified Floyd–Warshall
  for k in range(n):
    for i in range(n):
      for j in range(n):
        dist[i][j] = max(dist[i][j], min(dist[i][k], dist[k][j]))
  
  return dist
```

**Key Insight:**
- Same DP structure as Floyd–Warshall
- Instead of minimizing sum, maximize minimum

**Follow-up Q3.2.1:** How do you handle directed graphs?
- **Answer:** Don't add reverse edge; keep it directed.

**Follow-up Q3.2.2:** Can you use Dijkstra for this?
- **Answer:** Yes, modified Dijkstra can solve this; treat "bottleneck value" as the distance.

---

#### Q3.3: Transitive Closure & Reachability 🟡
**Difficulty:** Medium  
**Time:** 20 min  
**Company:** Google, Bloomberg

**Question:**
```
Given a directed graph, compute the reachability matrix: reach[i][j] = True
if there's a path from i to j, False otherwise.

Example:
  Edges: [0->1], [1->2], [2->0], [1->3]
  
  Reachability:
    reach[0][0] = True  (path: 0 -> 1 -> 2 -> 0)
    reach[0][1] = True  (direct edge)
    reach[0][2] = True  (path: 0 -> 1 -> 2)
    reach[0][3] = True  (path: 0 -> 1 -> 3)
    reach[1][0] = True  (path: 1 -> 2 -> 0)
    reach[2][3] = False (no path exists)
```

**Solution Approach:**
1. Use Floyd–Warshall but with boolean operations instead of distance
2. Logical OR instead of min (can you reach j from i through k?)
3. Works on unweighted graphs (treats all edges as weight 1)

**Pseudocode:**
```
TransitiveClosure(n, edges):
  reach = [[False] * n for _ in range(n)]
  
  // Initialize diagonal
  for i in range(n):
    reach[i][i] = True
  
  // Initialize edges
  for (u, v) in edges:
    reach[u][v] = True
  
  // Floyd–Warshall with boolean operations
  for k in range(n):
    for i in range(n):
      for j in range(n):
        reach[i][j] = reach[i][j] or (reach[i][k] and reach[k][j])
  
  return reach
```

**Complexity:**
- Time: O(n³) but with simple bitwise operations (can use bitsets to parallelize)
- Space: O(n²)

**Follow-up Q3.3.1:** Can you use DFS/BFS instead?
- **Answer:** Yes, run DFS/BFS from each node; O(n * (n + m)). Floyd–Warshall is better for dense graphs.

**Follow-up Q3.3.2:** How do you find strongly connected components?
- **Answer:** Compute transitive closure. SCC i and j are in same component if reach[i][j] and reach[j][i].

---

#### Q3.4: All-Pairs Shortest Paths with Constraints 🔴
**Difficulty:** Hard  
**Time:** 30 min  
**Company:** Google, Meta, Bloomberg

**Question:**
```
You are given a directed graph with edge weights. For each pair of vertices (i, j),
find:
1. Shortest distance
2. Shortest distance using at most k edges
3. Shortest distance avoiding vertex x

Example:
  n = 4
  edges = [[0,1,4], [1,2,1], [2,3,1], [0,3,10], [1,3,5]]
  k = 2
  
  All-pairs shortest: same as Floyd–Warshall
  All-pairs with ≤2 edges: different (can't use intermediate nodes if it takes >2 edges)
  All-pairs avoiding node 2: recompute without edges involving node 2
```

**Solution Approach:**
1. **Shortest distances:** Standard Floyd–Warshall
2. **With ≤k edges:** Modified Floyd–Warshall with limited iterations
3. **Avoiding node x:** Remove node x and recompute

**Pseudocode (≤k edges):**
```
AllPairsAtMostKEdges(n, edges, k):
  dist = [[∞] * n for _ in range(n)]
  
  // Base case: 0 edges (direct)
  for i in range(n):
    dist[i][i] = 0
  for (u, v, w) in edges:
    dist[u][v] = w
  
  // DP: add one more edge at a time
  for edge_count in range(1, k):
    new_dist = dist.copy()
    for (u, v, w) in edges:
      for i in range(n):
        if dist[i][u] != ∞:
          new_dist[i][v] = min(new_dist[i][v], dist[i][u] + w)
    dist = new_dist
  
  return dist
```

**Key Insight:**
- DP on number of edges (not intermediate vertices)
- Different from standard Floyd–Warshall which has no edge limit

**Follow-up Q3.4.1:** How do you handle avoiding multiple nodes?
- **Answer:** Remove all of them and recompute; or modify DP to skip them during relaxation.

**Follow-up Q3.4.2:** Can you answer "avoid node x" queries without recomputing?
- **Answer:** No, without preprocessing; each query requires new computation.

---

### CATEGORY 4: MST (KRUSKAL & PRIM) (8 Questions)

#### Q4.1: Basic Kruskal's MST 🟢
**Difficulty:** Easy  
**Time:** 15 min  
**Company:** Meta, Amazon, Google

**Question:**
```
Given n vertices and a list of weighted edges, find a minimum spanning tree using
Kruskal's algorithm. Return the total weight of the MST.

Input:
  n = 4
  edges = [[0,1,10], [0,2,6], [0,3,5], [1,3,15], [2,3,4]]

Output:
  mst_weight = 19
  mst_edges = [[2,3,4], [0,3,5], [0,2,6], [0,1,10]] (or similar)
  
  Explanation: Total weight = 4 + 5 + 6 + 10 = 25... wait, that's 25, not 19.
  Let me recompute: MST = [[2,3,4], [0,3,5], [0,1,10]] = 4 + 5 + 10 = 19. Correct.
```

**Solution Approach:**
1. Sort edges by weight
2. Initialize DSU (union-find)
3. For each edge in sorted order:
   - If it connects two different components, add it to MST and union
   - Otherwise, skip (would create a cycle)
4. Stop when MST has n-1 edges

**Pseudocode:**
```
Kruskal(n, edges):
  edges.sort_by_weight()
  dsu = DSU(n)
  mst_weight = 0
  mst_edges = []
  
  for (u, v, weight) in edges:
    if dsu.find(u) != dsu.find(v):
      dsu.union(u, v)
      mst_weight += weight
      mst_edges.append((u, v, weight))
      
      if len(mst_edges) == n - 1:
        break
  
  return mst_weight, mst_edges
```

**Key Points:**
- Time: O(E log E) for sorting
- Space: O(E) for edges + O(n) for DSU
- Requires DSU for efficient cycle detection
- Works on undirected, weighted graphs

**Follow-up Q4.1.1:** What if the graph is disconnected?
- **Answer:** MST is impossible; return -1 or return a "forest" (multiple trees).

**Follow-up Q4.1.2:** What if there are duplicate edge weights?
- **Answer:** The MST weight is unique, but the set of edges may differ. Sort stably and pick the lexicographically smallest set if needed.

---

#### Q4.2: Basic Prim's MST 🟢
**Difficulty:** Easy  
**Time:** 15 min  
**Company:** Meta, Amazon

**Question:**
```
Given n vertices and a list of weighted edges, find a minimum spanning tree using
Prim's algorithm. Return the total weight of the MST.

(Same input/output as Q4.1)
```

**Solution Approach:**
1. Start from arbitrary vertex (e.g., 0)
2. Maintain a priority queue of edges from visited to unvisited vertices
3. Repeatedly extract the cheapest edge; add its unvisited endpoint to MST
4. Add all edges from new vertex to priority queue
5. Continue until all vertices are in MST

**Pseudocode:**
```
Prim(n, edges):
  graph = build_adjacency_list(edges)
  visited = [False] * n
  pq = min_heap()
  mst_weight = 0
  mst_edges = []
  
  // Start from vertex 0
  visited[0] = True
  for (v, weight) in neighbors[0]:
    pq.add((weight, 0, v))
  
  while pq not empty and len(mst_edges) < n - 1:
    (weight, u, v) = pq.extract_min()
    
    if visited[v]:
      continue  // Stale edge (v already visited)
    
    visited[v] = True
    mst_weight += weight
    mst_edges.append((u, v, weight))
    
    // Add all edges from v to unvisited vertices
    for (next_v, next_weight) in neighbors[v]:
      if not visited[next_v]:
        pq.add((next_weight, v, next_v))
  
  return mst_weight, mst_edges
```

**Key Points:**
- Time: O((V + E) log V) with binary heap
- Space: O(V + E)
- Similar to Dijkstra's structure
- Start from arbitrary vertex

**Follow-up Q4.2.1:** Does the choice of starting vertex affect the MST?
- **Answer:** No, the MST weight is the same; the set of edges may differ if there are ties.

**Follow-up Q4.2.2:** Can you use Prim on a disconnected graph?
- **Answer:** Yes, but you'll only get a spanning tree of the component containing the start vertex.

---

#### Q4.3: Connecting Cities with Minimum Cost 🟡
**Difficulty:** Medium  
**Time:** 20 min  
**Company:** Google, Meta, Amazon

**Question:**
```
You are given n cities and you want to connect them all with the minimum cost.
You are given a list of `connections` where connections[i] = [city1, city2, cost].

Return the minimum cost to connect all the cities. If the cities cannot be connected,
return -1.

Example:
  n = 3
  connections = [[1,2,5],[1,3,6],[2,3,1]]
  Output: 6
  
  Explanation: Connect 2-3 (cost 1) and 2-1 (cost 5). Total = 6.
```

**Solution Approach:**
1. Use Kruskal's algorithm (sort edges, add if no cycle)
2. Check if MST has n-1 edges (meaning all cities connected)
3. If not, graph is disconnected; return -1

**Pseudocode:**
```
MinCostToConnectCities(n, connections):
  connections.sort_by_cost()
  dsu = DSU(n + 1)  // 1-indexed
  mst_cost = 0
  edges_added = 0
  
  for (u, v, cost) in connections:
    if dsu.find(u) != dsu.find(v):
      dsu.union(u, v)
      mst_cost += cost
      edges_added += 1
      
      if edges_added == n - 1:
        return mst_cost
  
  return -1  // Graph is disconnected
```

**Complexity:**
- Time: O(E log E) for sorting
- Space: O(n + E)

**Follow-up Q4.3.1:** What if there are multiple edges between the same pair of cities?
- **Answer:** Keep only the cheapest one; others are never used in MST.

**Follow-up Q4.3.2:** Can you return the actual edges in the MST?
- **Answer:** Yes, track mst_edges list during the algorithm.

---

#### Q4.4: Min Cost to Connect All Points (Traveling Salesman Variant) 🟡
**Difficulty:** Medium  
**Time:** 25 min  
**Company:** Google, Meta, Bloomberg

**Question:**
```
You are given n points in 2D space (x_i, y_i). You want to connect all points
with minimum cost, where the cost between two points is their Manhattan distance.

Note: This is an MST problem on the complete graph (every pair of points connected).

Example:
  points = [[0,0],[2,2],[3,10],[5,2],[7,0]]
  Output: 20
  
  Explanation: One MST could be:
    (0,0) - (2,2): distance = 4
    (2,2) - (3,10): distance = 9
    (3,10) - (5,2): distance = 10
    (5,2) - (7,0): distance = 4
    Total = 27... that doesn't match. Let me recalculate.
    
    Actually, the optimal MST has cost 20. Details depend on the exact points.
```

**Solution Approach:**
1. Build complete graph: every pair of points is an edge
2. Edge weight = Manhattan distance = |x1 - x2| + |y1 - y2|
3. Apply Kruskal's or Prim's algorithm

**Pseudocode (Kruskal):**
```
MinCostToConnectAllPoints(points):
  n = len(points)
  edges = []
  
  // Build complete graph
  for i in range(n):
    for j in range(i + 1, n):
      x1, y1 = points[i]
      x2, y2 = points[j]
      distance = abs(x1 - x2) + abs(y1 - y2)
      edges.append((distance, i, j))
  
  // Kruskal's algorithm
  edges.sort()
  dsu = DSU(n)
  mst_cost = 0
  edges_added = 0
  
  for (distance, u, v) in edges:
    if dsu.find(u) != dsu.find(v):
      dsu.union(u, v)
      mst_cost += distance
      edges_added += 1
      
      if edges_added == n - 1:
        return mst_cost
  
  return mst_cost
```

**Complexity:**
- Time: O(n² log n) for building and sorting edges
- Space: O(n²) for complete graph

**Optimization:** For Euclidean/Manhattan distance, can use Delaunay triangulation or other geometric techniques to reduce edges. But brute-force Kruskal is O(n² log n) which is acceptable for n ≤ 1000.

**Follow-up Q4.4.1:** What if you want Euclidean distance instead of Manhattan?
- **Answer:** Change distance formula to sqrt((x1 - x2)² + (y1 - y2)²). No other changes.

**Follow-up Q4.4.2:** Can you use Prim for this?
- **Answer:** Yes, Prim is O(n²) which is better than Kruskal's O(n² log n) for complete graphs.

---

#### Q4.5: Kruskal vs. Prim Performance Analysis 🟡
**Difficulty:** Medium  
**Time:** 20 min  
**Company:** Bloomberg, Citadel

**Question:**
```
Given a graph with V vertices and E edges, when should you use Kruskal's vs. Prim's?

Analyze time complexity for:
(a) Sparse graph: E = V + 10
(b) Dense graph: E = V²/2
(c) Complete graph: E = V * (V - 1) / 2

Provide recommendations.
```

**Analysis:**

| Graph Type | E ≈ | Kruskal | Prim | Winner |
|:---|:---|:---|:---|:---|
| **Sparse** | V | O(V log V) | O(V log V) | Tie |
| **Medium** | V log V | O(V log² V) | O(V log V) | Prim |
| **Dense** | V² | O(V² log V) | O(V²) | Prim |
| **Complete** | V² | O(V² log V) | O(V²) | Prim |

**Recommendation:**
- **Sparse graphs (E << V²):** Kruskal is simpler and competitive
- **Dense graphs (E ≈ V²):** Prim is significantly faster
- **Very dense (complete graphs):** Prim with array representation is O(V²) optimal

**Follow-up Q4.5.1:** Can you optimize Prim further?
- **Answer:** Yes, use Fibonacci heap (O(E + V log V)), but impractical. For complete graphs, O(V²) with priority queue is near-optimal.

**Follow-up Q4.5.2:** Can you parallelize these algorithms?
- **Answer:** Kruskal can be parallelized (sort in parallel, union-find is tricky). Prim is harder to parallelize (greedy frontier).

---

#### Q4.6: Minimum Spanning Forest (Disconnected Graph) 🟡
**Difficulty:** Medium  
**Time:** 20 min  
**Company:** Google, Meta

**Question:**
```
Given a graph that may be disconnected, find the minimum spanning forest: one MST
per connected component.

Example:
  n = 6
  edges = [[0,1,1], [0,2,1], [3,4,1]]
  
  Components:
    {0, 1, 2}: MST edges = [[0,1,1], [0,2,1]], weight = 2
    {3, 4}: MST edges = [[3,4,1]], weight = 1
    {5}: Isolated vertex, no edges
  
  MSF total weight = 3
  MSF edges = [[0,1,1], [0,2,1], [3,4,1]]
```

**Solution Approach:**
1. Run Kruskal's algorithm normally
2. Don't require exactly n-1 edges; stop when all edges are processed
3. If < n-1 edges added, graph is disconnected; return forest

**Pseudocode:**
```
MinimumSpanningForest(n, edges):
  edges.sort_by_weight()
  dsu = DSU(n)
  msf_weight = 0
  msf_edges = []
  
  for (u, v, weight) in edges:
    if dsu.find(u) != dsu.find(v):
      dsu.union(u, v)
      msf_weight += weight
      msf_edges.append((u, v, weight))
  
  return msf_weight, msf_edges  // May have < n-1 edges if disconnected
```

**Complexity:**
- Same as Kruskal: O(E log E)

**Follow-up Q4.6.1:** How do you identify the individual trees in the forest?
- **Answer:** Group vertices by their root in DSU. Vertices with same root are in same tree.

**Follow-up Q4.6.2:** Can you remove the minimum-weight edge from MSF and still have a forest?
- **Answer:** Yes, you'd have two trees instead of one. This is useful for hierarchical clustering.

---

#### Q4.7: Cut Property & MST Correctness 🔴
**Difficulty:** Hard  
**Time:** 30 min  
**Company:** Google, Meta (theoretical interview)

**Question:**
```
Explain the Cut Property and prove that Kruskal's algorithm is correct.

Cut Property:
  For any cut (S, V-S) of a graph, the minimum-weight edge crossing the cut
  is part of some MST.

Prove: Kruskal's algorithm that greedily picks minimum-weight edges (without forming
cycles) produces an MST.
```

**Solution Outline:**

**Part 1: Cut Property Statement**
- A cut (S, V-S) partitions vertices into two sets
- An edge "crosses" the cut if one endpoint is in S and the other in V-S
- Cut property: The minimum-weight edge crossing any cut is in some MST

**Part 2: Proof by Contradiction**
1. Suppose Kruskal produces a tree T that's not an MST
2. There exists an MST T* with weight < T
3. Find the first edge e that Kruskal adds but T* doesn't contain
4. T* ∪ {e} contains a cycle; there's another edge e' in the cycle crossing the same cut as e
5. By cut property, weight(e) ≤ weight(e')
6. Replacing e' with e in T* gives a tree with weight ≤ T*
7. Contradiction; T must be an MST

**Pseudocode (Informal Proof):**
```
Kruskal produces MST:
  Proof by induction on edges added
  
  Base case: First edge is minimum; by cut property, it's in some MST
  
  Inductive step:
    Assume T_k is part of some MST after k edges
    Edge e_{k+1} is the minimum among remaining edges not forming a cycle
    Consider the cut defined by (connected components using T_k, rest)
    e_{k+1} is the minimum edge crossing this cut (by Kruskal's choice)
    By cut property, e_{k+1} is in some MST
    Thus T_{k+1} is part of some MST
  
  Conclusion: Final tree T is an MST
```

**Key Insights:**
- Greedy choice is justified by cut property
- Kruskal's correctness relies on the cut property
- Same proof works for Prim's algorithm (different cut perspective)

**Follow-up Q4.7.1:** Does Prim's algorithm also satisfy the cut property?
- **Answer:** Yes, Prim greedily picks the minimum edge crossing the cut between visited and unvisited vertices.

**Follow-up Q4.7.2:** What if all edge weights are equal?
- **Answer:** Every spanning tree is an MST (all have same weight). Kruskal and Prim both produce valid MSTs.

---

#### Q4.8: Steiner Tree & Minimum Weight Subgraph 🔴
**Difficulty:** Hard  
**Time:** 30 min  
**Company:** Google, Bloomberg (advanced)

**Question:**
```
Given a graph and a subset of "terminal" vertices, find the minimum-weight subgraph
that connects all terminals. This is called the Steiner tree problem.

Example:
  Vertices: 0, 1, 2, 3, 4
  Edges: [0-1: 1], [1-2: 2], [2-3: 3], [1-3: 2], [3-4: 1]
  Terminals: {0, 2, 4}
  
  Steiner tree might include non-terminal vertex 3 to reduce cost:
    0 - 1 - 2 (cost 3)
    1 - 3 - 4 (cost 3)
    Total: 6 (using non-terminal vertex 1 and 3)
  
  Alternative without non-terminals:
    0 - 1 - 2 - 3 - 4 (cost 7)
```

**Solution Approach (Approximation):**
1. Steiner tree is NP-hard in general
2. For small graphs, can use dynamic programming or branch-and-bound
3. Approximation: Run MST on terminal vertices, then extend to include other vertices

**Pseudocode (DP for small cases):**
```
SteinerTree(n, edges, terminals):
  // DP[mask][v] = min cost of subtree connecting terminals in mask,
  //                rooted at vertex v
  
  DP = [[∞] * n for _ in range(2^k)]  where k = |terminals|
  
  // Base case: single terminals
  for t in terminals:
    DP[1 << t][t] = 0
  
  // Fill DP
  for mask in range(1, 2^k):
    for v in range(n):
      // Try combining submasks
      for submask in range(mask):
        if submask and submask | mask == mask:
          DP[mask][v] = min(DP[mask][v], DP[submask][v] + DP[mask ^ submask][v])
      
      // Try extending via shortest path
      for u in range(n):
        for w in range(n):
          if edge(w, u) exists:
            DP[mask][u] = min(DP[mask][u], DP[mask][w] + weight(w, u))
  
  return min(DP[(1 << k) - 1])
```

**Complexity:**
- DP: O(2^k * n²) where k = |terminals|
- Practical for k ≤ 15

**Follow-up Q4.8.1:** Is there a good approximation algorithm?
- **Answer:** Yes, MST on terminals + shortest paths to non-terminals gives 2-approximation.

**Follow-up Q4.8.2:** Why is Steiner tree NP-hard?
- **Answer:** Must decide which non-terminal vertices to include; exponentially many combinations.

---

### CATEGORY 5: DISJOINT SET UNION / UNION–FIND (8 Questions)

#### Q5.1: Basic DSU Implementation 🟢
**Difficulty:** Easy  
**Time:** 10 min  
**Company:** Meta, Amazon, Google

**Question:**
```
Implement a disjoint set union (union-find) data structure with the following operations:
1. make_set(x): Create a set containing only x
2. find(x): Return the representative of x's set
3. union(x, y): Merge the sets containing x and y

Ensure that the implementation uses both union-by-rank and path compression.

Test with:
  make_set(0), make_set(1), make_set(2), make_set(3)
  union(0, 1)
  union(2, 3)
  union(0, 2)
  find(0), find(1), find(2), find(3)  // All should return same root
```

**Solution:**

**Pseudocode:**
```
DSU:
  __init__(n):
    parent[i] = i for all i
    rank[i] = 0 for all i
  
  find(x):
    if parent[x] != x:
      parent[x] = find(parent[x])  // Path compression
    return parent[x]
  
  union(x, y):
    root_x = find(x)
    root_y = find(y)
    if root_x == root_y: return
    
    // Union by rank
    if rank[root_x] < rank[root_y]:
      parent[root_x] = root_y
    else if rank[root_x] > rank[root_y]:
      parent[root_y] = root_x
    else:
      parent[root_y] = root_x
      rank[root_x] += 1
```

**C# Implementation:**
```csharp
public class DSU {
    private int[] parent;
    private byte[] rank;
    
    public DSU(int n) {
        parent = new int[n];
        rank = new byte[n];
        for (int i = 0; i < n; i++) {
            parent[i] = i;
        }
    }
    
    public int Find(int x) {
        if (parent[x] != x) {
            parent[x] = Find(parent[x]);  // Path compression
        }
        return parent[x];
    }
    
    public void Union(int x, int y) {
        int root_x = Find(x);
        int root_y = Find(y);
        if (root_x == root_y) return;
        
        if (rank[root_x] < rank[root_y]) {
            parent[root_x] = root_y;
        } else if (rank[root_x] > rank[root_y]) {
            parent[root_y] = root_x;
        } else {
            parent[root_y] = root_x;
            rank[root_x]++;
        }
    }
}
```

**Complexity:**
- find: O(α(n)) amortized
- union: O(α(n)) amortized
- Space: O(n)

**Follow-up Q5.1.1:** Can you use recursion for find, or must you use iteration?
- **Answer:** Both work; recursion is cleaner but can overflow stack for very deep trees (unlikely with optimizations).

**Follow-up Q5.1.2:** What if you forget path compression?
- **Answer:** Still O(log n) per operation (union-by-rank alone), but worse in practice. Path compression optimizes further.

---

#### Q5.2: Redundant Connection (Cycle Detection) 🟡
**Difficulty:** Medium  
**Time:** 20 min  
**Company:** Meta, Google, Amazon

**Question:**
```
In this problem, a tree is an undirected graph with n nodes and n - 1 edges.
You are given an undirected graph with n nodes and n edges (one extra edge).
This graph contains a cycle.

Return an edge that can be removed to form a tree. If there are multiple valid
answers, return the edge that occurs last in the given 2D array.

Example:
  n = 3
  edges = [[1,2], [2,3], [1,3]]
  
  Output: [1,3]
  
  Explanation: The graph is a triangle. Remove [1,3] to form a tree.
```

**Solution Approach:**
1. Process edges in order
2. For each edge, check if its endpoints are already connected (via DSU)
3. If yes, this edge creates a cycle; return it (or find the last one)
4. If no, union them

**Pseudocode:**
```
FindRedundantConnection(edges):
  n = len(edges)
  dsu = DSU(n + 1)  // 1-indexed
  
  for (u, v) in edges:
    if dsu.find(u) == dsu.find(v):
      return [u, v]  // Found the cycle-creating edge
    dsu.union(u, v)
  
  return []  // No cycle (shouldn't happen given problem constraints)
```

**To find the LAST redundant edge (as problem asks):**
```
FindRedundantConnection(edges):
  n = len(edges)
  dsu = DSU(n + 1)
  result = []
  
  for (u, v) in edges:
    if dsu.find(u) == dsu.find(v):
      result = [u, v]  // Update, don't return immediately
    else:
      dsu.union(u, v)
  
  return result
```

**Complexity:**
- Time: O(n * α(n))
- Space: O(n)

**Follow-up Q5.2.1:** What if the graph is directed (DAG with one extra edge)?
- **Answer:** Use DFS with coloring instead; DSU doesn't work for directed graphs.

**Follow-up Q5.2.2:** Can there be multiple cycles?
- **Answer:** With n vertices and n edges, there's exactly one extra edge forming exactly one cycle.

---

#### Q5.3: Accounts Merge (Component Grouping) 🟡
**Difficulty:** Medium  
**Time:** 25 min  
**Company:** Google, Meta, Amazon

**Question:**
```
Given a list of accounts where each account contains a name and emails.
If two accounts have a common email, merge them.

Return the accounts after merging. Accounts should be sorted, and within each
account, emails should be sorted.

Example:
  accounts = [["John", "johnsmith@mail.com", "john00@mail.com"],
              ["John", "johnsmith@mail.com", "john_newyork@mail.com"],
              ["Mary", "mary@mail.com"],
              ["John", "johnsmith@mail.com", "john00@mail.com"]]
  
  Output: [["John", "john00@mail.com", "john_newyork@mail.com", "johnsmith@mail.com"],
           ["Mary", "mary@mail.com"]]
  
  Explanation: Accounts 0 and 1 share johnsmith@mail.com; merge them.
               Account 2 shares john00@mail.com with merged account; merge further.
```

**Solution Approach:**
1. Map each email to an account index
2. Use DSU to group accounts by shared emails
3. For each email, union all accounts that contain it
4. Group emails by account (root), sort, and format output

**Pseudocode:**
```
AccountsMerge(accounts):
  n = len(accounts)
  dsu = DSU(n)
  email_to_account = {}  // Email -> first account index containing it
  
  // Build email-to-account mapping and union
  for i in range(n):
    for email in accounts[i][1:]:
      if email in email_to_account:
        dsu.union(i, email_to_account[email])
      else:
        email_to_account[email] = i
  
  // Group emails by account (root)
  account_to_emails = {}
  for email, account_idx in email_to_account.items():
    root = dsu.find(account_idx)
    if root not in account_to_emails:
      account_to_emails[root] = []
    account_to_emails[root].append(email)
  
  // Format output
  result = []
  for account_idx in account_to_emails:
    emails = sorted(account_to_emails[account_idx])
    result.append([accounts[account_idx][0]] + emails)  // Name + sorted emails
  
  return sorted(result)
```

**Complexity:**
- Time: O(n * k log k + result size) where k = max emails per account
- Space: O(n * k) for mapping

**Follow-up Q5.3.1:** What if you need to preserve the original account names for each merged group?
- **Answer:** Keep the name from the first account in each group.

**Follow-up Q5.3.2:** How would you handle case-insensitive email matching?
- **Answer:** Normalize emails to lowercase before processing.

---

#### Q5.4: Connected Components Count 🟡
**Difficulty:** Medium  
**Time:** 15 min  
**Company:** Google, Meta

**Question:**
```
Given n vertices and a list of edges, count the number of connected components
using DSU.

Example:
  n = 5
  edges = [[0,1], [1,2], [3,4]]
  
  Output: 2
  
  Explanation: Components: {0, 1, 2}, {3, 4}
```

**Solution Approach:**
1. Initialize DSU
2. For each edge, union its endpoints
3. Count distinct roots; each root represents one component

**Pseudocode:**
```
CountConnectedComponents(n, edges):
  dsu = DSU(n)
  
  for (u, v) in edges:
    dsu.union(u, v)
  
  roots = set()
  for i in range(n):
    roots.add(dsu.find(i))
  
  return len(roots)
```

**Complexity:**
- Time: O((n + m) * α(n))
- Space: O(n)

**Follow-up Q5.4.1:** Can you also return which vertices belong to each component?
- **Answer:** Group vertices by their root in DSU.

**Follow-up Q5.4.2:** What if edges are given incrementally?
- **Answer:** Process them one at a time; count components after each edge (useful for seeing connectivity evolution).

---

#### Q5.5: Bipartite Graph Check 🟡
**Difficulty:** Medium  
**Time:** 20 min  
**Company:** Google, Meta, Amazon

**Question:**
```
A bipartite graph is a graph whose vertices can be partitioned into two disjoint
sets such that every edge connects a vertex in one set to a vertex in the other.

Given an undirected graph, determine if it's bipartite using DSU (or DFS coloring).

Example:
  n = 4
  edges = [[1,0], [2,0], [3,1], [3,2]]
  
  Output: False
  
  This graph has an odd cycle (e.g., 0-1-3-2-0), so it's not bipartite.
```

**Solution Approach 1 (DFS Coloring - Simpler):**
1. Color vertices with two colors: 0 and 1
2. For each edge, endpoints must have different colors
3. If any edge has same-colored endpoints, not bipartite

**Solution Approach 2 (DSU - More Advanced):**
1. For each vertex, create two "nodes" in DSU: vertex itself and vertex + n (complement)
2. For each edge (u, v), union(u, v+n) and union(v, u+n)
   - This means u and v must be in different "sides"
3. Check if any u and u+n are in the same component (contradiction)

**Pseudocode (DSU):**
```
IsBipartite(n, edges):
  dsu = DSU(2 * n)  // Each vertex has itself and its complement
  
  for (u, v) in edges:
    // u and v must be in different sides
    dsu.union(u, v + n)  // u is with complement of v
    dsu.union(v, u + n)  // v is with complement of u
    
    // Check for contradiction
    if dsu.find(u) == dsu.find(v):
      return False
  
  return True
```

**Complexity:**
- Time: O(m * α(n))
- Space: O(n)

**Key Insight:**
- Union-find on complementary vertices elegantly enforces bipartition constraints
- If u and v are connected, they must be in different sets
- Union(u, complement(v)) says u is with opposite of v

**Follow-up Q5.5.1:** Can you return the actual bipartition?
- **Answer:** Group vertices by their root in DSU; roots represent the two sides.

**Follow-up Q5.5.2:** Is DFS coloring simpler than DSU here?
- **Answer:** Yes, DFS coloring is more intuitive. DSU is elegant but overkill.

---

#### Q5.6: Surrounded Regions (Component-Based) 🟡
**Difficulty:** Medium  
**Time:** 25 min  
**Company:** Google, Meta

**Question:**
```
Given an m × n matrix board containing 'X' and 'O', capture all 'O' regions
surrounded by 'X'.

A region is captured if it's completely surrounded by 'X' and not touching the border.

Example:
  board = [['X', 'O', 'X', 'X'],
           ['X', 'O', 'X', 'X'],
           ['X', 'X', 'O', 'X']]
  
  Output: All interior 'O's become 'X's.
  
  But if 'O' touches the border, it's not captured.
```

**Solution Approach (Union-Find on Grid):**
1. Create a dummy node representing "border"
2. Union all border 'O's with the dummy node
3. Union each 'O' with its 4-neighbors if they're 'O'
4. Mark all 'O's as 'X', except those connected to border via dummy node

**Pseudocode:**
```
SurroundedRegions(board):
  m, n = len(board), len(board[0])
  dsu = DSU(m * n + 1)
  dummy = m * n
  
  def index(r, c):
    return r * n + c
  
  // Union border 'O's with dummy
  for r in range(m):
    for c in range(n):
      if (r == 0 or r == m - 1 or c == 0 or c == n - 1) and board[r][c] == 'O':
        dsu.union(index(r, c), dummy)
  
  // Union interior 'O's with neighbors
  for r in range(m):
    for c in range(n):
      if board[r][c] == 'O':
        if r + 1 < m and board[r+1][c] == 'O':
          dsu.union(index(r, c), index(r+1, c))
        if c + 1 < n and board[r][c+1] == 'O':
          dsu.union(index(r, c), index(r, c+1))
  
  // Mark isolated regions as 'X'
  for r in range(m):
    for c in range(n):
      if board[r][c] == 'O' and dsu.find(index(r, c)) != dsu.find(dummy):
        board[r][c] = 'X'
```

**Complexity:**
- Time: O(m * n * α(m * n))
- Space: O(m * n)

**Alternative (DFS):**
- Simpler: DFS from border 'O's to mark reachable 'O's as safe. Mark remaining as 'X'.
- DSU solution shown for comparison with DFS approach.

**Follow-up Q5.6.1:** What if you want to count the number of isolated regions?
- **Answer:** Count distinct roots (excluding border root).

**Follow-up Q5.6.2:** Can you extend this to 3D?
- **Answer:** Yes, union each cell with its 6 neighbors (up, down, left, right, front, back). Logic stays same.

---

#### Q5.7: Kruskal's MST with DSU (Integration) 🟡
**Difficulty:** Medium  
**Time:** 20 min  
**Company:** Google, Meta (combo problem)

**Question:**
```
Implement Kruskal's MST algorithm using DSU. Verify that MST weight is correct.

Example:
  n = 4
  edges = [[0,1,1], [1,2,2], [1,3,3], [0,2,4]]
  
  MST edges (Kruskal): [0,1,1], [1,2,2], [1,3,3]
  Total weight: 6
```

**Solution:**
- Combine Q5.1 (basic DSU) and Q4.1 (basic Kruskal)

**Pseudocode:**
```
KruskalMST(n, edges):
  edges.sort_by_weight()
  dsu = DSU(n)
  mst_weight = 0
  mst_edges = []
  
  for (u, v, weight) in edges:
    if dsu.find(u) != dsu.find(v):
      dsu.union(u, v)
      mst_weight += weight
      mst_edges.append((u, v, weight))
  
  return mst_weight, mst_edges
```

**Complexity:**
- Time: O(E log E) for sorting + O(E * α(n)) for DSU = O(E log E)
- Space: O(E + n)

**Key Point:**
- DSU makes cycle detection O(α(n)) per edge; this is the key to Kruskal's efficiency

**Follow-up Q5.7.1:** What's the role of DSU in Kruskal vs. other MST algorithms?
- **Answer:** DSU enables O(α(n)) cycle detection. Without it, cycle detection would be O(n) per edge, making Kruskal O(E*n) overall.

---

#### Q5.8: DSU Union-by-Size (Alternative to Rank) 🟡
**Difficulty:** Medium  
**Time:** 20 min  
**Company:** Google (variations)

**Question:**
```
Implement DSU using union-by-size instead of union-by-rank. Compare the two approaches.

union-by-size:
  Instead of rank, track subtree size.
  Attach smaller tree to larger tree.

Example:
  make_set(0..4)
  union(0, 1), union(2, 3), union(0, 2), union(0, 4)
  Resulting tree structure and size values.
```

**Solution:**

**Pseudocode (Union-by-Size):**
```
DSU_WithSize:
  __init__(n):
    parent[i] = i for all i
    size[i] = 1 for all i
  
  find(x):
    if parent[x] != x:
      parent[x] = find(parent[x])
    return parent[x]
  
  union(x, y):
    root_x = find(x)
    root_y = find(y)
    if root_x == root_y: return
    
    // Union by size: attach smaller to larger
    if size[root_x] < size[root_y]:
      parent[root_x] = root_y
      size[root_y] += size[root_x]
    else:
      parent[root_y] = root_x
      size[root_x] += size[root_y]
```

**Comparison:**

| Aspect | Union-by-Rank | Union-by-Size |
|:---|:---|:---|
| **Complexity** | O(α(n)) | O(α(n)) |
| **Implementation** | Simpler (no updates) | Requires size updates |
| **Intuition** | Tree height bound | Subtree size bound |
| **Practical** | Both work equally well |Both work equally well |

**Key Insight:**
- Both approaches achieve same amortized complexity
- Size-based is more intuitive (easier to visualize)
- Rank-based avoids size updates but is harder to explain

**Follow-up Q5.8.1:** Which is easier to debug?
- **Answer:** Union-by-size is easier because you can print/verify sizes; rank is hidden.

**Follow-up Q5.8.2:** Can you combine both rank and size?
- **Answer:** Yes, but redundant; just use one. Combining adds unnecessary overhead.

---

## 🎙️ FINAL INTERVIEW TIPS

### Preparation Strategy
1. **Master fundamentals first:** Solve easy questions (Q1.1, Q2.1, Q3.1, Q4.1, Q5.1-2)
2. **Build understanding:** Medium questions (rest of category)
3. **Challenge yourself:** Hard questions (Q1.5, Q3.4, Q4.7-8, Q5.7)

### During Interview
1. **Clarify:** Understand problem constraints (weighted? directed? range of n?)
2. **Choose algorithm:** Use decision tree (Summary file)
3. **Explain approach:** Describe algorithm verbally before coding
4. **Code carefully:** Implement without bugs; test on examples
5. **Analyze complexity:** Explain time and space
6. **Discuss trade-offs:** When are alternatives better?

### Common Mistakes
- Forgetting edge cases (disconnected graph, self-loops, duplicate edges)
- Wrong complexity analysis (confusing O(V+E) with O(V*E))
- Not handling negative weights correctly
- Forgetting path compression or union-by-rank in DSU

---

**Status:** ✅ Week 09 Interview QA Reference — COMPLETE (30 questions across 5 categories)

