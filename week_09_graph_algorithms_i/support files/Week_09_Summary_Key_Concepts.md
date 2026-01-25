# 📊 Week 09 Summary — Key Concepts & Quick Reference

**Version:** 1.0  
**Purpose:** Quick-reference guide for Week 09 algorithms, complexities, and decision trees  
**Status:** ✅ ACTIVE

---

## 🎯 ONE-PAGE OVERVIEW

### The Problem
**Goal:** Solve graph optimization problems efficiently: find shortest paths (one-to-all, all-to-all), find minimum spanning trees, maintain dynamic connectivity.

### The Solutions (5 Algorithms)
| Day | Algorithm | Problem | Time | Space | Constraints | When Use |
|:---|:---|:---|:---|:---|:---|:---|
| **1** | Dijkstra | Single-source shortest path | O((V+E) log V) | O(V) | Non-negative weights | Most graphs; positive weights |
| **2** | Bellman–Ford | Single-source shortest path | O(VE) | O(V) | Negative weights OK | Negative weights; detect cycles |
| **3** | Floyd–Warshall | All-pairs shortest path | O(V³) | O(V²) | Negative weights OK | Dense graphs; small V; all pairs needed |
| **4** | Kruskal's MST | Minimum spanning tree | O(E log E) | O(E) | Undirected; weighted | Most MST problems; uses DSU |
| **4** | Prim's MST | Minimum spanning tree | O((V+E) log V) | O(V) | Undirected; weighted | Dense graphs; grow-from-start style |
| **5** | DSU/Union-Find | Dynamic connectivity | O(α(n)) amortized | O(n) | Elements only; no weights | Many queries; cycle detection; Kruskal backbone |

---

## 💡 QUICK DECISION TREE

### "Which Algorithm Should I Use?"

**START: What's your goal?**

```
Is it a shortest-path problem?
├─ YES: Do all paths have non-negative weights?
│  ├─ YES: Do you need one-to-many or one-to-one?
│  │  ├─ One-to-many (single source): Use DIJKSTRA [O((V+E) log V)]
│  │  └─ One-to-one: Use BFS if unweighted; Dijkstra if weighted
│  │
│  └─ NO (negative weights): Do you need all-pairs?
│     ├─ YES: Use FLOYD–WARSHALL [O(V³)]
│     └─ NO: Use BELLMAN–FORD [O(VE)]
│
Is it a minimum spanning tree problem?
├─ YES: How many queries do you have?
│  ├─ Many: Use KRUSKAL [O(E log E) with DSU]
│  └─ Few, dense graph: Use PRIM [O((V+E) log V)]
│
Is it a connectivity/component problem?
├─ YES: Are edges added incrementally (dynamic)?
│  ├─ YES: Use DSU [O(α(n)) amortized per operation]
│  └─ NO: Use BFS/DFS once [O(V+E) for preprocessing]
```

---

## 📈 ALGORITHM COMPLEXITIES AT A GLANCE

### By Problem Type

**Single-Source Shortest Path (non-negative weights):**
- Dijkstra: O((V+E) log V) ← **BEST for most cases**
- BFS (if unit weights): O(V+E) ← **Best if all weights = 1**
- Bellman–Ford: O(VE) ← **Works but slower; handles negatives too**

**Single-Source Shortest Path (with negative weights):**
- Bellman–Ford: O(VE) ← **Standard choice**
- Floyd–Warshall: O(V³) ← **Only if all-pairs needed**

**All-Pairs Shortest Paths:**
- Floyd–Warshall: O(V³) ← **Dense graphs; small V**
- Dijkstra × V: O(V(V+E) log V) ← **Sparse graphs; V² space**
- Bellman–Ford × V: O(V²E) ← **Negatives; sparse graphs**
- Johnson's Algorithm: O(VE log V + V²) ← **Negative weights; sparse**

**Minimum Spanning Tree:**
- Kruskal: O(E log E) ← **Scales with edges; uses DSU**
- Prim: O((V+E) log V) ← **Scales with vertices+edges**
- Both are equivalent; choose based on graph structure (dense vs. sparse)

**Dynamic Connectivity:**
- DSU: O(α(n)) amortized per operation ← **BEST for connectivity queries**
- BFS/DFS: O(V+E) per query ← **Slower for many queries**

---

## 🧠 CORE CONCEPTS CHEAT SHEET

### Day 1: Dijkstra's Algorithm

**Idea:** Greedily expand frontier; always process closest unvisited vertex.

**Key Insight:** Non-negative weights guarantee greedy choice is optimal (once a vertex is processed, no future path can improve it).

**Structure:**
```
1. Initialize: distance[source] = 0, others = ∞
2. Add source to priority queue
3. While queue not empty:
   - Extract min-distance vertex u
   - For each neighbor v of u:
     - If distance[u] + weight(u,v) < distance[v]:
       - Update distance[v]
       - Add (distance[v], v) to queue
```

**Critical:** Stale entries in PQ are OK; skip already-processed vertices when extracting.

---

### Day 2: Bellman–Ford Algorithm

**Idea:** Repeatedly relax all edges V-1 times; distances converge to true shortest paths.

**Key Insight:** DP over path lengths. After k passes, shortest paths using ≤ k edges are finalized.

**Structure:**
```
1. Initialize: distance[source] = 0, others = ∞
2. For i = 1 to V-1:
   - For each edge (u, v, weight):
     - If distance[u] + weight < distance[v]:
       - distance[v] = distance[u] + weight
3. Check for negative cycles (one more pass; if distances still decrease, cycle exists)
```

**Critical:** Handles negatives; detects negative cycles. Slower than Dijkstra for positives.

---

### Day 3: Floyd–Warshall Algorithm

**Idea:** DP over intermediate vertices. Gradually allow using more vertices as intermediates.

**Key Insight:** dist[i][j][k] = shortest i→j using vertices {0..k-1}. K-loop controls which vertices are allowed.

**Structure:**
```
1. Initialize: dist[i][j] = weight(i,j) if edge exists, else ∞; dist[i][i] = 0
2. For k = 0 to V-1:
   - For i = 0 to V-1:
     - For j = 0 to V-1:
       - dist[i][j] = min(dist[i][j], dist[i][k] + dist[k][j])
```

**Critical:** K-loop MUST be outermost. Handles negatives. Detects negative cycles (if dist[i][i] < 0).

---

### Day 4: Kruskal's & Prim's MST

**Kruskal's Idea:** Sort edges, add them greedily if they don't create a cycle (use DSU to check).

**Prim's Idea:** Start from a vertex, grow tree by adding cheapest edge to unvisited vertex (similar to Dijkstra).

**Kruskal Structure:**
```
1. Sort edges by weight
2. For each edge (u, v, weight) in sorted order:
   - If find(u) != find(v) (different components):
     - Add edge to MST
     - union(u, v)
```

**Prim Structure:**
```
1. Start from arbitrary vertex; add to tree
2. While not all vertices in tree:
   - Find cheapest edge from tree to unvisited vertex
   - Add that vertex to tree (similar to Dijkstra's frontier)
```

**Critical:** Kruskal uses DSU; Prim uses priority queue (like Dijkstra).

---

### Day 5: Disjoint Set Union (DSU)

**Idea:** Forest of trees; each tree = one set. Root = representative. Union merges trees. Find returns root.

**Key Optimizations:**
- **Union-by-rank:** Attach lower-rank tree to higher-rank; prevents tall trees.
- **Path compression:** During find, make all traversed nodes point directly to root; flattens tree.

**Structure:**
```
make_set(x): parent[x] = x, rank[x] = 0

find(x):  // With path compression
  if parent[x] != x:
    parent[x] = find(parent[x])
  return parent[x]

union(x, y):
  root_x = find(x), root_y = find(y)
  if root_x == root_y: return
  if rank[root_x] < rank[root_y]:
    parent[root_x] = root_y
  else if rank[root_x] > rank[root_y]:
    parent[root_y] = root_x
  else:
    parent[root_y] = root_x
    rank[root_x] += 1
```

**Critical:** Both optimizations together achieve O(α(n)) ≈ O(1). Union-by-rank prevents degeneracy.

---

## 🔄 COMPARISON TABLES

### Shortest Paths: Head-to-Head

| Scenario | Best Algorithm | Reason |
|:---|:---|:---|
| Positive weights, single source, sparse | **Dijkstra** | O((V+E) log V) beats all alternatives |
| Positive weights, all-pairs, dense | **Floyd–Warshall** | O(V³) simpler than Dijkstra×V |
| Positive weights, all-pairs, sparse | **Dijkstra×V** | O(V(V+E) log V) < O(V³) for sparse |
| Negative weights (no cycle), single source | **Bellman–Ford** | O(VE) only option for correctness |
| Negative weights, all-pairs | **Floyd–Warshall** | O(V³) handles all; detects cycles |
| Negative weights, sparse | **Johnson's** | O(VE log V + V²) optimized for sparse |
| Unit weights (0-1) | **0/1-BFS** | O(V+E) faster than Dijkstra |
| Unit weights (unweighted) | **BFS** | O(V+E) simplest and fastest |

### MST: Kruskal vs. Prim

| Criterion | Kruskal | Prim |
|:---|:---|:---|
| **Time** | O(E log E) | O((V+E) log V) |
| **Space** | O(E) for edges + O(V) for DSU | O(V) for PQ |
| **Best for** | Sparse graphs | Dense graphs |
| **Requires** | DSU for cycle detection | Priority queue |
| **Natural style** | Edge-based (sort edges) | Vertex-based (grow tree) |
| **Practical** | Standard choice in competitions | Good for dense graphs |

### DSU vs. Alternatives for Connectivity

| Task | DSU | BFS/DFS | Graph + Cache |
|:---|:---|:---|:---|
| **Single query: are A and B connected?** | O(α(n)) | O(V+E) | O(1) lookup |
| **Many queries on static graph** | O(m × α(n)) | O(m × (V+E)) | O(1) per query |
| **Incremental edges** | O(α(n)) per add | Recompute O(V+E) | Rebuild O(V+E) |
| **Detect cycles while adding edges** | Perfect ✓ | Hard without special logic | Need check per add |
| **Remove edges dynamically** | Not supported ✗ | Supported (recompute) ✓ | Supported ✓ |

---

## 🎯 INTERVIEW PATTERNS & SOLUTIONS

### Pattern 1: "Find shortest path from A to B"
- **Algorithm:** Dijkstra (if non-negative) or Bellman–Ford (if negatives possible)
- **Implementation time:** 10-15 min
- **Key:** Path reconstruction via predecessor pointers; handle unreachable case (dist[B] = ∞)

### Pattern 2: "Check if graph has a cycle"
- **Algorithm:** DFS with coloring (visited/processing/done) OR DSU during edge addition
- **Implementation time:** 5-10 min
- **Key:** DSU is faster for incremental case; DFS for static graph

### Pattern 3: "Find minimum cost to connect all vertices"
- **Algorithm:** Kruskal's or Prim's MST
- **Implementation time:** 15 min (Kruskal with DSU slightly longer)
- **Key:** Verify graph is connected; handle tie-breaking for duplicate weights

### Pattern 4: "Count connected components"
- **Algorithm:** BFS/DFS (standard) OR DSU (incremental case)
- **Implementation time:** 5 min (BFS/DFS) or 10 min (DSU)
- **Key:** Initialize visited array; run BFS from each unvisited node

### Pattern 5: "Are two elements in the same group?"
- **Algorithm:** DSU with find operation
- **Implementation time:** 2-3 min (just call find)
- **Key:** Elements must be union'd first; then check find(a) == find(b)

### Pattern 6: "Merge groups as edges arrive"
- **Algorithm:** DSU with union operation
- **Implementation time:** 5 min
- **Key:** Process edges in order; union elements; query connectivity after each

### Pattern 7: "Find all shortest paths (not just distance)"
- **Algorithm:** Dijkstra + predecessor tracking OR BFS + level-by-level path reconstruction
- **Implementation time:** 15-20 min
- **Key:** Store predecessor; reconstruct by following predecessors from destination to source

### Pattern 8: "Detect negative cycle in directed graph"
- **Algorithm:** Bellman–Ford or DFS cycle detection
- **Implementation time:** 10 min
- **Key:** After V-1 passes, if any distance still decreases, cycle exists; identify affected vertices

---

## 📐 COMPLEXITY QUICK FACTS

### Inverse Ackermann: "It's basically constant"
- α(1) = 1
- α(2) = 1
- α(3) = 2
- α(4) = 2
- α(16) = 3
- α(65536) = 4
- For all practical n ≤ 2^65536, α(n) ≤ 4

**Takeaway:** DSU with both optimizations is O(1) per operation for any reasonable problem size.

### O(V³) vs. O((V+E) log V)
- O(V³): Floyd–Warshall, all-pairs
- O((V+E) log V): Dijkstra
- For sparse graphs (E ≈ V): Dijkstra ≈ O(V log V)
- For dense graphs (E ≈ V²): Dijkstra ≈ O(V² log V); Floyd–Warshall ≈ O(V³) similar
- Floyd–Warshall wins for very dense graphs (E >> V²) due to simplicity

---

## 🚨 COMMON MISTAKES & FIXES

| Mistake | Cause | Fix |
|:---|:---|:---|
| Dijkstra on negative weights | Greedy breaks | Check weights first; use Bellman–Ford if negative |
| Wrong loop order in Floyd–Warshall | Misunderstand DP | K-loop must be outermost; k controls intermediates |
| DSU without path compression | Forgot optimization | Add: `parent[x] = find(parent[x])` in find |
| DSU without union-by-rank | Forgot optimization | Compare ranks before attaching; increment on tie |
| MST on disconnected graph | Oversight | Check connectivity first; MST is forest if disconnected |
| Floating-point comparison | == is unreliable | Use `abs(a - b) < 1e-9` for floating-point |
| Stale entries in Dijkstra PQ | Inefficiency, not incorrectness | Check `if processed[u]: continue` when extracting |
| Forgot negative cycle check in Floyd–Warshall | Didn't test | Check diagonal after computation: `if dist[i][i] < 0` |

---

## 💾 CODE TEMPLATES (Pseudocode)

### Dijkstra (with path reconstruction)
```
Dijkstra(Graph, source):
  distance[all] = ∞
  distance[source] = 0
  predecessor[all] = -1
  pq.add((0, source))
  
  while pq not empty:
    (dist, u) = pq.extract_min()
    if processed[u]: continue
    processed[u] = true
    
    for each neighbor v of u:
      new_dist = distance[u] + weight(u, v)
      if new_dist < distance[v]:
        distance[v] = new_dist
        predecessor[v] = u
        pq.add((new_dist, v))
  
  return distance, predecessor

ReconstructPath(predecessor, source, target):
  path = []
  current = target
  while current != -1:
    path.prepend(current)
    current = predecessor[current]
  return path
```

### Floyd–Warshall (with cycle detection)
```
FloydWarshall(Graph):
  dist = adjacency_matrix  // with ∞ for non-edges, 0 on diagonal
  
  for k = 0 to V-1:
    for i = 0 to V-1:
      for j = 0 to V-1:
        dist[i][j] = min(dist[i][j], dist[i][k] + dist[k][j])
  
  // Check for negative cycles
  for i = 0 to V-1:
    if dist[i][i] < 0:
      return "Negative cycle detected"
  
  return dist
```

### DSU (with both optimizations)
```
MakeSet(x):
  parent[x] = x
  rank[x] = 0

Find(x):
  if parent[x] != x:
    parent[x] = Find(parent[x])  // Path compression
  return parent[x]

Union(x, y):
  root_x = Find(x)
  root_y = Find(y)
  if root_x == root_y: return
  
  if rank[root_x] < rank[root_y]:
    parent[root_x] = root_y
  else if rank[root_x] > rank[root_y]:
    parent[root_y] = root_x
  else:
    parent[root_y] = root_x
    rank[root_x] += 1
```

### Kruskal's MST (with DSU)
```
Kruskal(Graph):
  edges = all edges sorted by weight
  mst_weight = 0
  mst_edges = []
  
  for each element v: MakeSet(v)
  
  for each edge (u, v, weight) in edges:
    if Find(u) != Find(v):
      Union(u, v)
      mst_edges.add((u, v, weight))
      mst_weight += weight
  
  return mst_weight, mst_edges
```

---

## 🎓 WHAT YOU SHOULD MEMORIZE

**Yes, memorize:**
- Dijkstra: O((V+E) log V), priority queue, non-negative weights
- Bellman–Ford: O(VE), V-1 passes, handles negatives
- Floyd–Warshall: O(V³), k-loop outermost, all-pairs
- Kruskal: O(E log E), sort edges, DSU
- Prim: O((V+E) log V), grow from vertex
- DSU: O(α(n)), union-by-rank, path compression

**Don't memorize:**
- Exact pseudocode (derive from understanding)
- Implementation details (code them from scratch)
- Proof of correctness (understand the intuition)

---

## ✅ END-OF-WEEK CHECKLIST

- [ ] Understand all 5 algorithms deeply
- [ ] Can trace each on a graph
- [ ] Can implement each in < 15 min
- [ ] Know when to use each
- [ ] Understand complexities and why
- [ ] Solved ≥ 3 problems per algorithm
- [ ] Ready for graph interview questions

---

**Status:** ✅ Week 09 Summary — COMPLETE

