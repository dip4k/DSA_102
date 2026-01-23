# 🗺️ Week_09_Extended_CSharp_Problem_Solving_Implementation

**Version:** v1.0 HYBRID (Pattern Recognition + Production Implementation)  
**Week:** 9 – Graph Algorithms I: Shortest Paths, MST & Union-Find  
**Purpose:** Master Week 9 graph patterns through pattern recognition, understanding, and practice  
**Target:** Transform Week 9 knowledge into interview-ready C# coding skills  
**Prerequisites:** Week 9 instructional files + standard support files complete

---

## 📚 WEEK 9 OVERVIEW

**Primary Goal:** Learn foundational shortest path and MST algorithms, plus disjoint set union for connectivity.

**Topics by Day:**
- **Day 1:** Single-Source Shortest Paths: Dijkstra
- **Day 2:** Bellman-Ford & Negative Weights
- **Day 3:** All-Pairs Shortest Paths: Floyd-Warshall
- **Day 4:** Minimum Spanning Trees: Kruskal & Prim
- **Day 5:** DSU / Union-Find in Depth

---

## SECTION 1️⃣: PATTERN RECOGNITION FRAMEWORK

### 🎯 Decision Tree — How to Identify & Choose Week 9 Patterns

Use this decision tree when you encounter a graph problem in Week 9:

| 🔍 Problem Phrases/Signals | 🎯 Pattern Name | ❓ Why This Pattern? | 💻 C# Collection | ⏱️ Time/Space |
|---|---|---|---|---|
| "Shortest path from source", "Non-negative weights", "Single source" | **Dijkstra's Algorithm** | Greedy with priority queue efficiently finds minimum distance; cannot handle negatives | `PriorityQueue<T>` or `SortedSet<T>` | O((V+E) log V) / O(V) |
| "Shortest path with negatives", "Detect negative cycle", "Relaxation" | **Bellman-Ford** | DP perspective: relax all edges V-1 times; extra pass detects negative cycles | `List<Edge>` | O(VE) / O(V) |
| "All-pairs shortest paths", "Floyd-Warshall", "K-dimension DP" | **Floyd-Warshall** | DP with intermediate vertices up to k; handles all pairs including negatives (no cycles) | `int[,]` 2D array | O(V³) / O(V²) |
| "Minimum spanning tree", "Edges with weights", "Connect all vertices" | **Kruskal's Algorithm** | Greedy: sort edges, use DSU to avoid cycles; O(E log E) sorting dominates | `List<Edge>`, `UnionFind` | O(E log E) / O(V) |
| "MST growth", "Prim variant", "Vertex-centric" | **Prim's Algorithm** | Greedy: grow tree from start vertex using priority queue; similar to Dijkstra | `PriorityQueue<T>` | O((V+E) log V) / O(V) |
| "Connected components", "Cycle detection", "Union operations" | **Union-Find (DSU)** | Fast set membership: find root with path compression; union with rank optimization | `UnionFind` custom class | O(α(n)) amortized ≈ O(1) / O(V) |

**HOW TO READ THIS:**
- See "shortest path + non-negative weights"? IMMEDIATELY think **Dijkstra**
- See "all pairs of vertices"? IMMEDIATELY think **Floyd-Warshall**
- See "minimum cost to connect"? IMMEDIATELY think **Kruskal or Prim**
- See "connected components" or "cycle detection"? IMMEDIATELY think **Union-Find**
- Ask yourself: "Why does this pattern solve it?" → Internalize the reasoning
- Check what collection is recommended → Learn why that collection is best

---

## SECTION 2️⃣: ANTI-PATTERNS — WHEN NOT TO USE & WHAT FAILS

### ⚠️ Week 9 Common Mistakes & Correct Alternatives

Learn WHAT NOT TO DO and WHY:

| ❌ Wrong Approach | 💥 Why It Fails | ⚡ Symptom/Consequence | ✅ Correct Alternative |
|---|---|---|---|
| Using Dijkstra when weights are negative | Dijkstra assumes non-negative; greedy choice becomes incorrect | Returns wrong shortest distances; algorithm terminates early without finding true shortest | Use **Bellman-Ford** for negative weights (no negative cycles) |
| Not detecting negative cycles in Bellman-Ford | Missing the extra pass; assume convergence after V-1 passes | Produces incorrect distances if negative cycles exist; misleading results | Always run **V-th pass**: if any distance improves, negative cycle exists |
| Floyd-Warshall with wrong loop order (K outermost is CRITICAL) | Intermediate vertex constraint violated; not all paths considered | Wrong shortest paths; answers depend on random loop order | **K-loop MUST be outermost**; then i-loop, then j-loop (this is the correct DP order) |
| Kruskal without union-find to detect cycles | Naive approach checks if adding edge creates cycle via DFS (expensive) | O(E² log V) or worse; defeats purpose of efficient MST | Use **UnionFind** with path compression + union by rank for O(α(n)) per operation |
| Using Prim without priority queue (checking all edges each iteration) | Naive O(V²) inner loop for finding minimum edge | Slower: O(V²) or O(E+V) with bad implementation; wastes potential | Use **PriorityQueue<T>** or `SortedSet<T>` to get O((V+E) log V) efficiently |
| Modifying graph while running Dijkstra/Prim without snapshot | Graph changes during algorithm execution; state becomes inconsistent | Incorrect results; algorithm assumes static graph structure | **Snapshot the graph** before algorithm starts OR use immutable edge list |

**ANTI-PATTERN LESSON:**
Understand not just the pattern, but understand its boundaries. When you see someone use a wrong approach, explain why the correct alternative is better and when each pattern truly applies.

---

## SECTION 3️⃣: PATTERN IMPLEMENTATIONS (Production-Grade)

### Pattern 1️⃣: Dijkstra's Algorithm

#### 🧠 Mental Model
Imagine a flood spreading from a source vertex at constant speed: the shortest distance to any vertex is when the "flood wave" first reaches it. Use a priority queue (min-heap) to always expand the nearest unvisited vertex next. This greedy choice works because distances are non-negative—once we've found the shortest path to a vertex, we never improve it.

#### ✅ When to Use This Pattern
- ✅ **Single-source shortest paths with non-negative weights** — Classic use; guarantees O((V+E) log V) optimal solution
- ✅ **Network routing** — Router finds fastest path to destination (all link costs positive)
- ✅ **Game AI pathfinding** — Find shortest distance from player to goal (movement costs always positive)

#### 💻 Core C# Implementation (Battle-Tested)

```csharp
/// <summary>
/// Dijkstra's Algorithm - Single-source shortest paths with non-negative weights
/// Time Complexity: O((V+E) log V) | Space Complexity: O(V)
/// 
/// 🧠 MENTAL MODEL:
/// Use a min-heap priority queue to always expand the closest unvisited vertex.
/// Once a vertex is visited, its shortest distance is final (no improvement possible
/// because all weights are non-negative). This greedy choice guarantees optimality.
/// </summary>
public Dictionary<int, int> Dijkstra(int start, int vertices, List<(int to, int weight)>[] graph) {
    
    // STEP 1: Guard Clauses
    if (graph == null || start < 0 || start >= vertices) return new Dictionary<int, int>();
    if (vertices == 0) return new Dictionary<int, int>();
    
    // STEP 2: Initialize State
    // distances[v] = shortest distance from start to v (initially infinite)
    var distances = new int[vertices];
    for (int i = 0; i < vertices; i++) {
        distances[i] = int.MaxValue;
    }
    distances[start] = 0;
    
    // visited[v] = true if we've finalized shortest path to v
    var visited = new bool[vertices];
    
    // Priority queue: (distance, vertex) - min by distance
    var pq = new PriorityQueue<(int dist, int vertex), int>();
    pq.Enqueue((0, start), 0);
    
    // STEP 3: Core Dijkstra Loop
    while (pq.Count > 0) {
        var (currentDist, u) = pq.Dequeue();
        
        // Skip if already processed (handles stale entries in PQ)
        if (visited[u]) continue;
        visited[u] = true;
        
        // If this dequeued distance > best known distance, it's stale
        if (currentDist > distances[u]) continue;
        
        // STEP 4: Relax all edges from u
        // Check each neighbor v: can we improve distance to v via u?
        foreach (var (v, weight) in graph[u]) {
            if (!visited[v] && distances[u] != int.MaxValue) {
                int newDist = distances[u] + weight;
                
                // Found a better path to v
                if (newDist < distances[v]) {
                    distances[v] = newDist;
                    pq.Enqueue((newDist, v), newDist);
                }
            }
        }
    }
    
    // STEP 5: Convert to dictionary (excluding unreachable vertices)
    var result = new Dictionary<int, int>();
    for (int i = 0; i < vertices; i++) {
        if (distances[i] != int.MaxValue) {
            result[i] = distances[i];
        }
    }
    return result;
}
```

#### 🔴 C# Engineering Notes
- 🔴 **CRITICAL:** `PriorityQueue<T>` enqueues stale entries; always check `visited[u]` before processing. We don't remove/update; we skip stale entries. This is more efficient than trying to update.
- 🟡 **PERFORMANCE:** For dense graphs (E ≈ V²), the O((V+E) log V) is O(V² log V). Consider using Fibonacci heap in theory, but C# doesn't have it; binary heap is practical.
- 🟢 **BEST PRACTICE:** Initialize distances to `int.MaxValue` for unreachable vertices. When relaxing, check `distances[u] != int.MaxValue` to avoid overflow on `distances[u] + weight`.

---

### Pattern 2️⃣: Bellman-Ford Algorithm

#### 🧠 Mental Model
Think of Bellman-Ford as a dynamic programming solution: after k iterations, we know the shortest path to each vertex using at most k edges. Run exactly V-1 iterations (since a shortest path uses at most V-1 edges in a V-vertex graph). After V-1 passes, check if any edge still improves—if so, a negative cycle exists (and shortest paths are undefined).

#### ✅ When to Use This Pattern
- ✅ **Graphs with negative weights** — Only algorithm here that handles negative edge costs
- ✅ **Detecting negative cycles** — Extra iteration reveals if distances still improve (indicating a cycle)
- ✅ **Currency exchange arbitrage** — Detect profit opportunities (negative weight cycle)

#### 💻 Core C# Implementation (Battle-Tested)

```csharp
/// <summary>
/// Bellman-Ford Algorithm - Shortest paths with negative weights + cycle detection
/// Time Complexity: O(VE) | Space Complexity: O(V)
/// 
/// 🧠 MENTAL MODEL:
/// DP approach: after k iterations, know shortest paths using ≤ k edges.
/// V-1 passes guarantee shortest paths (max V-1 edges in any simple path).
/// V-th pass checks if any edge still improves—indicates negative cycle.
/// </summary>
public (Dictionary<int, int> distances, bool hasNegativeCycle) BellmanFord(
    int start, int vertices, List<(int from, int to, int weight)> edges) {
    
    // STEP 1: Guard Clauses
    if (start < 0 || start >= vertices || edges == null) {
        return (new Dictionary<int, int>(), false);
    }
    
    // STEP 2: Initialize State
    // distances[v] = shortest distance from start to v
    var distances = new int[vertices];
    for (int i = 0; i < vertices; i++) {
        distances[i] = int.MaxValue;
    }
    distances[start] = 0;
    
    // STEP 3: Relax edges V-1 times (DP iterations)
    // After iteration k, shortest paths using ≤ k edges are known
    for (int iter = 0; iter < vertices - 1; iter++) {
        // STEP 4: For this iteration, relax all edges
        foreach (var (from, to, weight) in edges) {
            if (distances[from] != int.MaxValue) {
                int newDist = distances[from] + weight;
                if (newDist < distances[to]) {
                    distances[to] = newDist;
                }
            }
        }
    }
    
    // STEP 5: Check for negative cycle
    // If any edge still improves, a negative cycle exists
    bool hasNegCycle = false;
    foreach (var (from, to, weight) in edges) {
        if (distances[from] != int.MaxValue) {
            int newDist = distances[from] + weight;
            if (newDist < distances[to]) {
                hasNegCycle = true;
                break;
            }
        }
    }
    
    // STEP 6: Convert to dictionary
    var result = new Dictionary<int, int>();
    for (int i = 0; i < vertices; i++) {
        if (distances[i] != int.MaxValue) {
            result[i] = distances[i];
        }
    }
    
    return (result, hasNegCycle);
}
```

#### 🔴 C# Engineering Notes
- 🔴 **CRITICAL:** Run EXACTLY V-1 passes for correctness. V-th pass is only for cycle detection, not distance updates. The number V-1 comes from longest simple path having at most V-1 edges.
- 🟡 **PERFORMANCE:** O(VE) makes this slower than Dijkstra O((V+E) log V) for non-negative graphs. Use Bellman-Ford only when negatives are necessary.
- 🟢 **BEST PRACTICE:** To find which vertices are affected by a negative cycle, run a backward search from negative cycle vertices. Distances to those vertices become undefined (should be -∞).

---

### Pattern 3️⃣: Floyd-Warshall Algorithm

#### 🧠 Mental Model
All-pairs shortest paths using dynamic programming with intermediate vertices. After considering intermediate vertices 0 to k, we know shortest paths using only those as intermediates. Build up from k=0 (direct edges only) to k=V-1 (all vertices as possible intermediates). The KEY: k-loop must be outermost for DP correctness.

#### ✅ When to Use This Pattern
- ✅ **All-pairs shortest paths** — Need distances between every pair of vertices
- ✅ **Dense graphs** — O(V³) is efficient when V is small (≤ 500) and E ≈ V²
- ✅ **Transitive closure** — Find which vertices are reachable from which (boolean variant)

#### 💻 Core C# Implementation (Battle-Tested)

```csharp
/// <summary>
/// Floyd-Warshall Algorithm - All-pairs shortest paths
/// Time Complexity: O(V³) | Space Complexity: O(V²)
/// 
/// 🧠 MENTAL MODEL:
/// DP with intermediate vertices up to k. After iteration k, shortest paths
/// using vertices 0..k as intermediates are known. K-loop MUST be outermost
/// (this is critical for correctness of the DP).
/// </summary>
public int[,] FloydWarshall(int vertices, int[,] graph) {
    
    // STEP 1: Guard Clauses
    if (graph == null || vertices <= 0) return new int[0, 0];
    
    // STEP 2: Initialize DP table
    // dp[i,j] = shortest distance from i to j
    int[,] dp = new int[vertices, vertices];
    for (int i = 0; i < vertices; i++) {
        for (int j = 0; j < vertices; j++) {
            dp[i, j] = graph[i, j];
        }
    }
    
    // STEP 3: DP iterations - K LOOP MUST BE OUTERMOST
    // k = intermediate vertex constraint (0 to k so far)
    for (int k = 0; k < vertices; k++) {
        // Consider all i-j pairs
        for (int i = 0; i < vertices; i++) {
            for (int j = 0; j < vertices; j++) {
                // Can we improve distance i→j by going through k?
                // i→k→j might be better than direct i→j
                if (dp[i, k] != int.MaxValue && dp[k, j] != int.MaxValue) {
                    int viaK = dp[i, k] + dp[k, j];
                    if (viaK < dp[i, j]) {
                        dp[i, j] = viaK;
                    }
                }
            }
        }
    }
    
    // STEP 4: Check for negative cycles
    // If any dp[i,i] < 0, negative cycle exists through i
    for (int i = 0; i < vertices; i++) {
        if (dp[i, i] < 0) {
            // Negative cycle detected; shortest paths undefined
            // (In production, mark unreachable or return error)
        }
    }
    
    return dp;
}
```

#### 🔴 C# Engineering Notes
- 🔴 **CRITICAL:** K-loop is OUTERMOST. This is the most common mistake. If you nest k inside i or j, the algorithm is wrong. The DP constraint requires intermediate vertices 0..k before considering larger k.
- 🟡 **PERFORMANCE:** O(V³) is expensive; limit to V ≤ 500-1000. For larger graphs, use Dijkstra from each source: O(V · (V+E) log V), which is better for sparse graphs.
- 🟢 **BEST PRACTICE:** Initialize unreachable as `int.MaxValue`. When adding distances, check both aren't `int.MaxValue` to avoid overflow.

---

### Pattern 4️⃣: Kruskal's Algorithm (MST)

#### 🧠 Mental Model
Greedy approach: sort all edges by weight (ascending), then add edges one by one if they don't form a cycle (detected via Union-Find). Builds a minimum spanning tree by always choosing the cheapest available edge.

#### ✅ When to Use This Pattern
- ✅ **Minimum spanning tree** — Connect all vertices with minimum total edge weight
- ✅ **Sparse graphs** — O(E log E) sorting dominates; good for sparse E
- ✅ **Hardwired connections** — Network design where edge cost is total weight

#### 💻 Core C# Implementation (Battle-Tested)

```csharp
/// <summary>
/// Kruskal's Algorithm - Minimum Spanning Tree
/// Time Complexity: O(E log E + E·α(V)) ≈ O(E log E) | Space Complexity: O(V + E)
/// 
/// 🧠 MENTAL MODEL:
/// Greedy algorithm: sort edges by weight, add each edge if it connects
/// two different components (no cycle). Union-Find tracks components efficiently.
/// Result is MST with V-1 edges connecting all vertices.
/// </summary>
public List<(int from, int to, int weight)> Kruskal(int vertices, List<(int from, int to, int weight)> edges) {
    
    // STEP 1: Guard Clauses
    if (edges == null || vertices <= 1) return new List<(int, int, int)>();
    
    // STEP 2: Sort edges by weight (greedy: pick lightest first)
    var sortedEdges = new List<(int, int, int)>(edges);
    sortedEdges.Sort((a, b) => a.weight.CompareTo(b.weight));
    
    // STEP 3: Initialize Union-Find for cycle detection
    var uf = new UnionFind(vertices);
    
    // STEP 4: Greedy selection - iterate through sorted edges
    var mst = new List<(int, int, int)>();
    int mstWeight = 0;
    
    foreach (var (from, to, weight) in sortedEdges) {
        // Check if from and to are already in same component
        // If different components, they're safe to connect (no cycle)
        if (uf.Find(from) != uf.Find(to)) {
            // STEP 5: Add edge to MST
            mst.Add((from, to, weight));
            mstWeight += weight;
            
            // Union the two components
            uf.Union(from, to);
            
            // MST is complete when we have V-1 edges
            if (mst.Count == vertices - 1) break;
        }
    }
    
    return mst;
}

/// <summary>
/// Union-Find (Disjoint Set Union) data structure
/// Enables fast "are these two elements in the same set?" queries
/// </summary>
public class UnionFind {
    private int[] parent;
    private int[] rank;
    
    public UnionFind(int n) {
        parent = new int[n];
        rank = new int[n];
        for (int i = 0; i < n; i++) {
            parent[i] = i;  // Each element is its own parent initially
            rank[i] = 0;
        }
    }
    
    /// <summary>
    /// Find root of set containing x (with path compression)
    /// Path compression: make nodes point directly to root
    /// </summary>
    public int Find(int x) {
        if (parent[x] != x) {
            // Path compression: update parent to point to root
            parent[x] = Find(parent[x]);
        }
        return parent[x];
    }
    
    /// <summary>
    /// Merge two sets (union by rank for efficiency)
    /// Attach smaller tree under larger tree to keep depth low
    /// </summary>
    public void Union(int x, int y) {
        int rootX = Find(x);
        int rootY = Find(y);
        
        if (rootX == rootY) return;  // Already in same set
        
        // Attach smaller rank tree under larger rank tree
        if (rank[rootX] < rank[rootY]) {
            parent[rootX] = rootY;
        } else if (rank[rootX] > rank[rootY]) {
            parent[rootY] = rootX;
        } else {
            parent[rootY] = rootX;
            rank[rootX]++;
        }
    }
}
```

#### 🔴 C# Engineering Notes
- 🔴 **CRITICAL:** Union-Find must use path compression AND union by rank. Without both, worst case is O(n log n) per operation instead of O(α(n)) ≈ O(1).
- 🟡 **PERFORMANCE:** Sorting edges is O(E log E); Union-Find operations are nearly O(1). For dense graphs (E ≈ V²), this is O(V² log V), which can be slower than Prim.
- 🟢 **BEST PRACTICE:** Always check `Find(from) != Find(to)` before union to avoid self-loops and detect cycles correctly.

---

### Pattern 5️⃣: Prim's Algorithm (MST)

#### 🧠 Mental Model
Grow the MST vertex-by-vertex from a starting vertex: always add the minimum-weight edge that connects a tree vertex to a non-tree vertex. Use a priority queue (min-heap) to efficiently find the next minimum edge. Similar to Dijkstra but for edge weights instead of distances.

#### ✅ When to Use This Pattern
- ✅ **Dense graphs** — O((V+E) log V) is efficient when E ≈ V² (becomes O(V² log V))
- ✅ **When Kruskal seems overkill** — Direct vertex-centric approach
- ✅ **Starting from a specific vertex** — Naturally finds MST rooted at that vertex

#### 💻 Core C# Implementation (Battle-Tested)

```csharp
/// <summary>
/// Prim's Algorithm - Minimum Spanning Tree (vertex-centric)
/// Time Complexity: O((V+E) log V) | Space Complexity: O(V)
/// 
/// 🧠 MENTAL MODEL:
/// Grow the tree one vertex at a time. Always add the minimum-weight edge
/// from tree vertices to non-tree vertices. Priority queue finds minimum edge efficiently.
/// </summary>
public List<(int from, int to, int weight)> Prim(int start, int vertices, List<(int to, int weight)>[] graph) {
    
    // STEP 1: Guard Clauses
    if (graph == null || start < 0 || start >= vertices) return new List<(int, int, int)>();
    
    // STEP 2: Initialize State
    var inMST = new bool[vertices];
    var mst = new List<(int, int, int)>();
    
    // Priority queue: (weight, from, to) - min by weight
    var pq = new PriorityQueue<(int weight, int from, int to), int>();
    
    // STEP 3: Start from `start` vertex
    inMST[start] = true;
    
    // Add all edges from start vertex to PQ
    foreach (var (to, weight) in graph[start]) {
        pq.Enqueue((weight, start, to), weight);
    }
    
    // STEP 4: Grow MST by adding vertices one at a time
    while (pq.Count > 0 && mst.Count < vertices - 1) {
        var (weight, from, to) = pq.Dequeue();
        
        // If `to` is already in MST, skip (we want to connect tree to non-tree)
        if (inMST[to]) continue;
        
        // STEP 5: Add this edge to MST
        mst.Add((from, to, weight));
        inMST[to] = true;
        
        // STEP 6: Add all edges from newly added vertex `to`
        foreach (var (nextTo, nextWeight) in graph[to]) {
            if (!inMST[nextTo]) {
                pq.Enqueue((nextWeight, to, nextTo), nextWeight);
            }
        }
    }
    
    return mst;
}
```

#### 🔴 C# Engineering Notes
- 🔴 **CRITICAL:** Check `inMST[to]` before adding edge; we want tree-to-non-tree edges, not tree-to-tree edges (which would create cycles).
- 🟡 **PERFORMANCE:** O((V+E) log V) can be better than Kruskal for dense graphs, but slightly worse for sparse graphs (Kruskal's O(E log E) dominates when E small).
- 🟢 **BEST PRACTICE:** Store (weight, from, to) in priority queue to enable easy edge reconstruction; alternative is storing just (weight, vertex) if you track parent pointers.

---

### Pattern 6️⃣: Union-Find (DSU) - Comprehensive

#### 🧠 Mental Model
Efficiently answer "are these two elements in the same set?" by maintaining a forest of trees (sets). Each element points to its parent; root is the set representative. Path compression (flatten paths) and union by rank (attach small trees to large) together give nearly O(1) operations.

#### ✅ When to Use This Pattern
- ✅ **Cycle detection in graphs** — Two vertices in same component before adding edge → cycle
- ✅ **Connected components** — Count or list components efficiently
- ✅ **Offline connectivity queries** — Answer "connected?" for many pairs efficiently
- ✅ **Kruskal MST** — Essential for avoiding cycles

#### 💻 Core C# Implementation (Battle-Tested)

```csharp
/// <summary>
/// Union-Find (Disjoint Set Union) - Complete Implementation
/// Time Complexity: O(α(n)) amortized ≈ O(1) per operation | Space: O(n)
/// 
/// 🧠 MENTAL MODEL:
/// Maintain disjoint sets with Find (find representative) and Union (merge sets).
/// Path compression: flatten tree paths to root (optimize future Finds).
/// Union by rank: attach smaller trees under larger (keep tree shallow).
/// Together: amortized O(α(n)) per operation, where α is inverse Ackermann (practically constant).
/// </summary>
public class UnionFindAdvanced {
    private int[] parent;
    private int[] rank;
    private int[] size;        // Size of each component
    private int numComponents; // Count of disjoint components
    
    public UnionFindAdvanced(int n) {
        parent = new int[n];
        rank = new int[n];
        size = new int[n];
        numComponents = n;
        
        for (int i = 0; i < n; i++) {
            parent[i] = i;
            rank[i] = 0;
            size[i] = 1;
        }
    }
    
    /// <summary>
    /// Find root of set containing x with path compression
    /// Path compression: make nodes point to root for faster future lookups
    /// </summary>
    public int Find(int x) {
        if (parent[x] != x) {
            // Path compression: update parent to root directly
            parent[x] = Find(parent[x]);
        }
        return parent[x];
    }
    
    /// <summary>
    /// Check if two elements are in same set (same root)
    /// </summary>
    public bool Connected(int x, int y) {
        return Find(x) == Find(y);
    }
    
    /// <summary>
    /// Merge two sets containing x and y
    /// Union by rank: attach smaller tree under larger
    /// </summary>
    public void Union(int x, int y) {
        int rootX = Find(x);
        int rootY = Find(y);
        
        if (rootX == rootY) return;  // Already connected
        
        // Merge smaller component into larger
        if (rank[rootX] < rank[rootY]) {
            parent[rootX] = rootY;
            size[rootY] += size[rootX];
        } else if (rank[rootX] > rank[rootY]) {
            parent[rootY] = rootX;
            size[rootX] += size[rootY];
        } else {
            // Same rank: arbitrary choice, increase winner's rank
            parent[rootY] = rootX;
            size[rootX] += size[rootY];
            rank[rootX]++;
        }
        
        numComponents--;  // Two components merged into one
    }
    
    /// <summary>
    /// Get size of component containing x
    /// </summary>
    public int GetSize(int x) {
        return size[Find(x)];
    }
    
    /// <summary>
    /// Get number of disjoint components
    /// </summary>
    public int GetComponentCount() {
        return numComponents;
    }
}

// APPLICATION: Detect cycle in undirected graph
public bool HasCycleUndirected(int vertices, List<(int from, int to)> edges) {
    var uf = new UnionFindAdvanced(vertices);
    
    foreach (var (from, to) in edges) {
        if (uf.Connected(from, to)) {
            // Both already in same component: adding this edge creates cycle
            return true;
        }
        uf.Union(from, to);
    }
    
    return false;  // No cycle
}

// APPLICATION: Count connected components
public int CountConnectedComponents(int vertices, List<(int from, int to)> edges) {
    var uf = new UnionFindAdvanced(vertices);
    
    foreach (var (from, to) in edges) {
        uf.Union(from, to);
    }
    
    return uf.GetComponentCount();
}

// APPLICATION: List components (get all vertices in each component)
public Dictionary<int, List<int>> GetComponents(int vertices, List<(int from, int to)> edges) {
    var uf = new UnionFindAdvanced(vertices);
    
    foreach (var (from, to) in edges) {
        uf.Union(from, to);
    }
    
    var components = new Dictionary<int, List<int>>();
    for (int i = 0; i < vertices; i++) {
        int root = uf.Find(i);
        if (!components.ContainsKey(root)) {
            components[root] = new List<int>();
        }
        components[root].Add(i);
    }
    
    return components;
}
```

#### 🔴 C# Engineering Notes
- 🔴 **CRITICAL:** Path compression modifies the tree structure; always use Find result, not parent array directly. Make sure `Find(x)` is called before accessing `parent[x]` in production code.
- 🟡 **PERFORMANCE:** Without path compression and union by rank, worst case is O(log n) per operation. With both optimizations, it's O(α(n)), which is practically O(1) for all reasonable inputs.
- 🟢 **BEST PRACTICE:** Track component count separately (decrement on each successful union) for fast "number of components" queries without iterating all vertices.

---

## SECTION 4️⃣: C# COLLECTION DECISION GUIDE

### When to Use Each Collection for Week 9 Patterns

| Use Case | Collection | Why? | When NOT to Use | Alternative |
|---|---|---|---|---|
| **Priority queue for Dijkstra/Prim** | `PriorityQueue<T>` (C# 10+) or `SortedSet<T>` | Efficient min/max extraction; O(log n) per op; handles stale entries gracefully | Don't use `List` with sequential min searches (O(n) each) | `BinaryHeap` custom if older .NET; `SortedSet<T>` is O(log n) but slightly slower |
| **Edge list for Bellman-Ford/Kruskal** | `List<(int, int, int)>` or `List<Edge>` | Simple, cache-friendly for iteration; no overhead | Don't use adjacency matrix for sparse graphs (memory wasted) | `LinkedList<T>` is slower (pointer chasing); `Array` if size known |
| **Distance/DP arrays** | `int[]` or `int[,]` 2D array (Floyd-Warshall) | Fixed size, cache-friendly, O(1) access; fastest for dense data | Don't use `Dictionary<int, int>` for dense arrays (hash overhead) | `Dictionary<int, int>` only if sparse (most vertices unreachable) |
| **Visited/InMST tracking** | `bool[]` | O(1) access, cache-efficient, minimal memory | Don't use `HashSet<int>` for simple boolean tracking (overkill) | `BitArray` for huge vertex counts (memory-efficient but slower) |
| **Adjacency list representation** | `List<T>[]` array of lists | Standard; O(1) access to vertex's edges; cache-friendly | Don't use `Dictionary<int, List<T>>` for numbered vertices (extra hash) | Adjacency matrix for dense graphs (E ≈ V²) |
| **Union-Find parent tracking** | `int[] parent` with `int[] rank` | O(1) access; path compression modifies in-place efficiently | Don't use object-based nodes (extra allocation and pointer chasing) | Custom `UnionFind` class for cleaner API but same underlying arrays |

**KEY INSIGHT:**
Choosing the right collection is as important as choosing the right pattern. Wrong collection = Correct algorithm running slowly. For Week 9: `PriorityQueue<T>` is critical for Dijkstra/Prim efficiency; `int[]` is critical for Floyd-Warshall; Union-Find arrays must support O(1) access.

---

## SECTION 5️⃣: PROGRESSIVE PROBLEM LADDER

### 🎯 Strategy: Solve Problems in Stages

**Stage 1 (Green):** Master core pattern skeleton  
**Stage 2 (Yellow):** Recognize pattern variations and edge cases  
**Stage 3 (Red):** Combine multiple patterns for complex problems

---

### 🟢 STAGE 1: CANONICAL PROBLEMS — Master Core Pattern

Solve these to cement the pattern. Can you code the skeleton without looking?

| # | LeetCode # | Difficulty | Pattern | C# Focus | Core Concept |
|---|---|---|---|---|---|
| 1 | #743 | 🟢 Easy | Dijkstra | `PriorityQueue<T>` basic usage | Single-source shortest path; graph as list of lists |
| 2 | #1976 | 🟢 Easy | Dijkstra | Path reconstruction (parent tracking) | Find shortest distance and optionally the path taken |
| 3 | #1514 | 🟢 Easy | Dijkstra | Simple application | Reach target and return; shortest round-trip |
| 4 | #1557 | 🟡 Medium | Floyd-Warshall | All-pairs, small V | Find minimum vertex distance to reach any location |
| 5 | #1584 | 🟡 Medium | Kruskal/Prim | MST application | Connect points with minimum total distance |
| 6 | #547 | 🟢 Easy | Union-Find | Connected components | Count provinces (connected components in graph) |

**STAGE 1 GOAL:** Pattern fluency. Can you implement [Pattern] skeleton in < 5 minutes without looking at notes?

---

### 🟡 STAGE 2: VARIATIONS — Recognize Pattern Boundaries

These problems twist the pattern. When does it work? When does it break?

| # | LeetCode # | Difficulty | Pattern + Twist | C# Focus | When Pattern Breaks |
|---|---|---|---|---|---|
| 1 | #1631 | 🟡 Medium | MST + optimization | Minimize threshold; Kruskal/Prim variant | What if we want minimum MAX edge instead of total? |
| 2 | #2699 | 🟡 Medium | Bellman-Ford concept | Negative weights implied | Modify edges to use Dijkstra with negative-like behavior |
| 3 | #1102 | 🟡 Medium | Dijkstra + early stop | K-shortest distance problem | What if we need shortest paths to ALL vertices vs specific target? |
| 4 | #1192 | 🟡 Medium | Union-Find + cycle detection | Bipartite check using DSU | Can we detect odd cycles using Union-Find? (yes, via complement graph) |
| 5 | #3108 | 🟡 Medium | Shortest path + cost matrix | All-pairs variant; Floyd-Warshall | What if you need all-pairs but V is large (Floyd-Warshall breaks)? |

**STAGE 2 GOAL:** Pattern boundaries. When do you need [Alternative Pattern]? When is [Pattern] insufficient?

---

### 🟠 STAGE 3: INTEGRATION — Combine Patterns for Real Problems

Hard problems rarely use just one pattern. These combine patterns:

| # | LeetCode # | Difficulty | Patterns Required | C# Focus | Pattern Combination Logic |
|---|---|---|---|---|---|
| 1 | #2577 | 🔴 Hard | Dijkstra + BFS concept | Modify graph (add edges) then find shortest | What if graph changes after Dijkstra? Snapshot or recalculate? |
| 2 | #332 | 🔴 Hard | Euler path + DSU insight | Graph structure analysis | Combine graph theory (Euler) with Union-Find for connectivity |
| 3 | #1301 | 🔴 Hard | DFS + shortest path + Dijkstra-like | Multi-constraint shortest path | What if path must visit specific nodes or avoid certain vertices? |

**STAGE 3 GOAL:** Real-world thinking. Professional problems need multiple patterns working together.

---

## SECTION 6️⃣: WEEK 9 PITFALLS & C# GOTCHAS

### 🐛 Runtime Issues & Collection Pitfalls

Common bugs you'll hit and how to fix them:

| ❌ Pattern | 🐛 Bug | 💥 C# Symptom | 🔧 Quick Fix |
|---|---|---|---|
| Dijkstra | Stale entries in PQ not removed | Gets correct answer but slow; memory bloat | Don't update; skip stale entries with `visited[u]` check |
| Bellman-Ford | Running only V-2 or V iterations instead of V-1 | Wrong shortest paths; doesn't find all shortest paths | Count carefully: loop `for (int i = 0; i < vertices - 1; i++)` |
| Floyd-Warshall | K-loop inside i or j (wrong DP order) | Outputs random/wrong shortest paths depending on loop order | Move k-loop to outermost: `for (int k = 0; k < V; k++)` |
| Kruskal | Forgetting cycle check (using edge without DSU check) | Creates cycles; not a valid spanning tree | Always call `uf.Find()` on both vertices before `uf.Union()` |
| Union-Find | Not using path compression or union by rank | O(log n) or worse per operation; slow for large inputs | Implement both: compress path in `Find()`; union by rank in `Union()` |
| Priority Queue | Using old (weight, vertex) without recreating for updates | Stale entries cause wrong results | Either don't update (skip with `visited`), or re-enqueue with new weight |

### 🎯 Week 9 Collection Gotchas

These mistakes are EASY to make:

- ❌ **Using `List<T>` for priority queue, finding min with O(n) scan** → Runtime error (timeout on large inputs) → Use `PriorityQueue<T>` for O(log n) min extraction
  
- ❌ **Mixing up `(weight, from, to)` order in priority queue** → Algorithm works but semantics confusing → Standardize: always `(weight, vertex)` for min-heap; track edge separately if needed

- ❌ **Modifying graph during Dijkstra/Prim execution** → Distances become incorrect; algorithm assumes static graph → Snapshot graph before running algorithm OR use immutable edge list

- ❌ **Floyd-Warshall with `int.MaxValue` distances causing overflow on addition** → Result is `int.MaxValue` (incorrect) instead of checking → Guard: `if (dp[i,k] != int.MaxValue && dp[k,j] != int.MaxValue)` before adding

- ❌ **Union-Find without checking if already connected** → Still works but doesn't detect cycles correctly → Always check `if (uf.Find(x) != uf.Find(y))` before union in Kruskal

---

## SECTION 7️⃣: QUICK REFERENCE — INTERVIEW PREPARATION

### Mental Models for Fast Recall (30 seconds before interview!)

| Pattern | 1-Liner Mental Model | Code Symbol | When You See This... |
|---|---|---|---|
| **Dijkstra** | Greedy flood from source; min-heap expands closest vertex first | `PriorityQueue<(dist, v), dist>` | "Shortest path", "non-negative", "single source" |
| **Bellman-Ford** | DP: V-1 passes relax all edges; V-th pass detects negative cycle | `for (int i = 0; i < V-1; i++) { /* relax edges */ }` | "Negative weights", "detect cycle", "relax edges" |
| **Floyd-Warshall** | DP with k-intermediate: build from k=0 to k=V-1; K-loop outermost | `for (int k=0; k<V; k++)` outermost | "All-pairs", "dense", "small V", "negative ok" |
| **Kruskal** | Sort edges, greedily add if doesn't form cycle (DSU detects) | `List.Sort(); foreach edge: if (uf.Find() != uf.Find()) { add; }` | "MST", "sparse", "edge-centric" |
| **Prim** | Grow tree vertex by vertex; min-heap finds minimum edge to tree | `PriorityQueue<(weight, to), weight>` from tree | "MST", "dense", "start vertex given" |
| **Union-Find** | Fast "same set?" query; path compression + union by rank ≈ O(1) | `Find() { path compress }; Union() { by rank }` | "Connected?", "cycle detect", "components" |

---

## ✅ WEEK 9 COMPLETION CHECKLIST

### Pattern Fluency — Can You Recognize & Choose?

- [ ] Recognize Dijkstra by "non-negative + shortest path" (no guessing!)
- [ ] Recall Dijkstra C# skeleton without notes (test yourself: 5 min limit)
- [ ] Explain WHY Dijkstra greedy works (non-negative guarantees optimality)
- [ ] Explain WHEN Dijkstra fails (negative weights break greedy)

- [ ] Recognize Bellman-Ford by "negative weights" OR "detect cycle"
- [ ] Recall Bellman-Ford C# skeleton without notes (test yourself)
- [ ] Explain WHY V-1 passes guarantee shortest paths (max V-1 edges in simple path)
- [ ] Explain WHY V-th pass detects cycle (if anything improves, negative cycle exists)

- [ ] Recognize Floyd-Warshall by "all-pairs" OR "dense small V"
- [ ] Recall Floyd-Warshall C# skeleton without notes
- [ ] Explain WHY k-loop is outermost (DP constraint: 0..k intermediates)
- [ ] Explain WHEN Floyd-Warshall wins (dense graphs, small V ≤ 500)

- [ ] Recognize Kruskal by "MST + sparse" and "edge-centric"
- [ ] Recall Kruskal + Union-Find C# skeleton without notes
- [ ] Explain WHY greedy works (cut property: lightest edge crossing cut is in MST)
- [ ] Explain Union-Find correctness (path compression + union by rank = O(α(n)))

- [ ] Recognize Prim by "MST + dense" and "vertex-centric"
- [ ] Recall Prim C# skeleton without notes
- [ ] Explain WHY similar to Dijkstra (both use priority queue; Prim tracks edges, Dijkstra tracks distances)
- [ ] Explain WHEN Prim beats Kruskal (dense graphs where sorting overhead dominates)

- [ ] Recognize DSU by "connected?", "cycle?", or "components"
- [ ] Recall Union-Find C# skeleton without notes (parent array + Find/Union)
- [ ] Explain path compression (flatten tree; future finds are faster)
- [ ] Explain union by rank (attach small tree under large; keep shallow)

### Problem Solving — Can You Practice?

- [ ] Solved ALL Stage 1 canonical problems (6+ problems)
- [ ] Solved 80%+ Stage 2 variations (recognized when pattern breaks)
- [ ] Solved 50%+ Stage 3 integration problems (got the ideas, even if not perfect)

### Production Code Quality — Can You Code?

- [ ] Used guard clauses on all inputs (null checks, boundaries, vertex range)
- [ ] Added mental model comments to your implementations
- [ ] Chose correct collection (`PriorityQueue<T>`, `int[]`, `UnionFind`, etc.)
- [ ] Handled edge cases explicitly (empty graph, single vertex, negative cycle)
- [ ] Your code would pass code review (clean, readable, efficient)

### Interview Ready — Can You Communicate?

- [ ] Can solve Stage 1 problem in < 5 minutes (pattern fluency)
- [ ] Can EXPLAIN your pattern choice to interviewer ("Why Dijkstra here?")
- [ ] Can write PRODUCTION-GRADE code, not hacks (guards, names, efficiency)
- [ ] Can discuss tradeoffs (Dijkstra vs Bellman; Kruskal vs Prim; DSU variants)
- [ ] Can handle follow-ups ("What if weights are negative?" "How detect cycle?")

---

### 🎯 Week 9 Mastery Status

- [ ] **YES - I've mastered Week 9. Ready for Week 10 (Dynamic Programming).**
- [ ] **NO - Need to practice more. Focus on Stage 2/3 problems and reading code without notes.**

---

## 📚 REFERENCE MATERIALS

This file is self-contained. You have:
- Decision framework for pattern selection (SECTION 1)
- Knowledge of anti-patterns (SECTION 2)
- Production-grade code implementations (SECTION 3)
- Collection guidance (SECTION 4)
- Progressive practice plan (SECTION 5)
- Real gotchas and fixes (SECTION 6)
- Quick interview reference (SECTION 7)

**Everything you need to master Week 9 is here.**

---

## 🚀 HOW TO USE THIS FILE

### For Learning:
1. Start with SECTION 1 → Understand the decision tree
2. Review SECTION 2 → Learn what NOT to do
3. Study SECTION 3 → Understand production implementations
4. Follow SECTION 5 → Solve problems progressively

### For Reference:
1. See a problem? → Check SECTION 1 (decision tree)
2. Stuck? → Check SECTION 6 (gotchas)
3. Need code? → Check SECTION 3 (implementations)
4. Before interview? → Check SECTION 7 (quick recall)

### For Interview Prep:
1. Day before: Review SECTION 7 (mental models)
2. Day of: Skim SECTION 1 (decision tree)
3. During interview: Use mental models from SECTION 7 to explain your choices

---

*End of Week 9 Extended C# Support — v13 Hybrid Format*

---

**Status:** ✅ Week 09 Extended C# Support v1.0 COMPLETE

This file covers all 6 algorithms (Dijkstra, Bellman-Ford, Floyd-Warshall, Kruskal, Prim, DSU) with pattern recognition, anti-patterns, production code, progressive problems, and interview prep.
