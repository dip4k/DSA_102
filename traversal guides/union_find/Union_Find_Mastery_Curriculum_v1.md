# Union-Find (Disjoint Set) Traversal Mastery

## 1. Summary Table

| Level | Mental Model | State/Index | Drill Problems |
|---|---|---|---|
| 1. Basic Union-Find | Tree of elements where root represents set ID. | `parent[i]`: Parent of node `i`. | [LeetCode 547: Number of Provinces] (Medium) |
| 2. Path Compression + Rank | Flatten trees for `O(1)` amortized ops. | `parent[i]`, `rank[i]` / `size[i]`. | [LeetCode 684: Redundant Connection] (Medium) |
| 3. Cycle Detection | If two nodes in an edge have same root, a cycle exists. | Node `u`, Node `v` in current edge. | [LeetCode 261: Graph Valid Tree] (Medium) |
| 4. Connected Components | Count distinct roots or track component sizes. | `count`: Active components. | [LeetCode 323: Number of Connected Components] (Medium) |
| 5. Kruskal's MST | Greedily pick minimum weight edges that don't form cycles. | Sorted edges, `weight`. | [LeetCode 1584: Min Cost to Connect All Points] (Medium) |

---

## Level 1: Basic Union-Find

### Mental Models & Invariants
* **What does the state/index mean?** `parent[i]` is the parent node of element `i`. If `parent[i] == i`, `i` is a root.
* **What region is processed?** Nodes `0` to `n-1` undergoing `find(x)` and `union(x, y)` operations.
* **What is the invariant/recurrence relation?** Elements `x` and `y` are in the same set if and only if `find(x) == find(y)`.

### Visual State Transitions
*Operations: Union(0, 1), Union(1, 2)*

| Step | Action | `parent` Array | Invariant |
|---|---|---|---|
| Init | `parent[i] = i` | `[0, 1, 2, 3]` | Each node is its own root |
| 1 | `union(0, 1)` | `[0, 0, 2, 3]` | Set 0 and 1 merged. Root of 1 is 0 |
| 2 | `union(1, 2)` | `[0, 0, 0, 3]` | Find(1)=0, Find(2)=2. Root of 2 becomes 0 |

### Code Snippets

```python
# Problem: Implement a basic Disjoint Set data structure to group elements and determine if they belong to the same set.
class UnionFind:
    def __init__(self, size):
        # 1. Initialize state: Each node is its own parent (root) initially
        self.parent = list(range(size))
        
    def find(self, i):
        # 2. Traverse up the tree until the root is found (where parent[i] == i)
        while i != self.parent[i]:
            i = self.parent[i]
        return i
        
    def union(self, i, j):
        # 3. Find the roots of both elements
        root_i = self.find(i)
        root_j = self.find(j)
        # 4. If roots differ, merge them by making one root point to the other
        if root_i != root_j:
            self.parent[root_i] = root_j
```

```csharp
// Problem: Implement a basic Disjoint Set data structure to group elements and determine if they belong to the same set.
public class UnionFind {
    private int[] parent;
    
    public UnionFind(int size) {
        // 1. Initialize state: Each node is its own parent (root) initially
        parent = new int[size];
        for(int i = 0; i < size; i++) parent[i] = i;
    }
    
    public int Find(int i) {
        // 2. Traverse up the tree until the root is found (where parent[i] == i)
        while(i != parent[i]) {
            i = parent[i];
        }
        return i;
    }
    
    public void Union(int i, int j) {
        // 3. Find the roots of both elements
        int rootI = Find(i);
        int rootJ = Find(j);
        // 4. If roots differ, merge them by making one root point to the other
        if(rootI != rootJ) {
            parent[rootI] = rootJ;
        }
    }
}
```

### ⚠️ Gotchas & Pitfalls
* **Linear Time Degradation:** Without rank or path compression, trees can become linked lists, making `find` $O(N)$.
* **Not Using `find` in `union`:** Mistakenly linking `parent[i] = j` instead of `parent[find(i)] = find(j)`.
* **Zero-based vs One-based Indexing:** Pay attention to node IDs. Initialize `size + 1` if nodes are `1`-indexed.

### Drill Problems
* [LeetCode 547: Number of Provinces] (Medium)

---

## Level 2: Path Compression + Union by Rank

### Mental Models & Invariants
* **What does the state/index mean?** `rank[i]` represents an upper bound on tree height (or size). `parent[i]` lazily flattens during `find()`.
* **What region is processed?** Traversal paths from node to root during `find`.
* **What is the invariant/recurrence relation?** Shorter trees are always attached under taller trees. Paths are flattened to point directly to the root.

### Visual State Transitions
*Operations: Find(3) where path is 3->2->1->0*

| Step | Action | `parent` State | Invariant |
|---|---|---|---|
| 0 | Initial path | `[0, 0, 1, 2]` | Height is 3 |
| 1 | `find(3)` traces up | Reaches root 0 | `parent[3]` will update |
| 2 | Backtrack update | `[0, 0, 0, 0]` | Path flattened. All nodes point to root 0 |

### Code Snippets

```python
# Problem: Optimize Union-Find using path compression for faster lookups and union by rank to keep trees shallow.
class UnionFindRank:
    def __init__(self, size):
        # 1. Initialize state: Each node is its own root, and initial rank (height) is 1
        self.parent = list(range(size))
        self.rank = [1] * size
        
    def find(self, i):
        # 2. Path compression: If not at the root, recursively find root and flatten path
        if self.parent[i] != i:
            self.parent[i] = self.find(self.parent[i]) # Path compression
        return self.parent[i]
        
    def union(self, i, j):
        # 3. Find the roots of both elements
        root_i = self.find(i)
        root_j = self.find(j)
        
        # 4. If already in the same set, no merge is needed
        if root_i != root_j:
            # 5. Union by rank: Attach the shorter tree under the root of the taller tree
            if self.rank[root_i] > self.rank[root_j]:
                self.parent[root_j] = root_i
            elif self.rank[root_i] < self.rank[root_j]:
                self.parent[root_i] = root_j
            else:
                # 6. If ranks are equal, choose one as root and increment its rank
                self.parent[root_j] = root_i
                self.rank[root_i] += 1
            return True
        return False
```

```csharp
// Problem: Optimize Union-Find using path compression for faster lookups and union by rank to keep trees shallow.
public class UnionFindRank {
    private int[] parent;
    private int[] rank;
    
    public UnionFindRank(int size) {
        // 1. Initialize state: Each node is its own root, and initial rank (height) is 1
        parent = new int[size];
        rank = new int[size];
        for(int i = 0; i < size; i++) {
            parent[i] = i;
            rank[i] = 1;
        }
    }
    
    public int Find(int i) {
        // 2. Path compression: If not at the root, recursively find root and flatten path
        if(parent[i] != i) {
            parent[i] = Find(parent[i]); // Path compression
        }
        return parent[i];
    }
    
    public bool Union(int i, int j) {
        // 3. Find the roots of both elements
        int rootI = Find(i);
        int rootJ = Find(j);
        
        // 4. If already in the same set, no merge is needed
        if(rootI == rootJ) return false;
        
        // 5. Union by rank: Attach the shorter tree under the root of the taller tree
        if(rank[rootI] > rank[rootJ]) {
            parent[rootJ] = rootI;
        } else if(rank[rootI] < rank[rootJ]) {
            parent[rootI] = rootJ;
        } else {
            // 6. If ranks are equal, choose one as root and increment its rank
            parent[rootJ] = rootI;
            rank[rootI]++;
        }
        return true;
    }
}
```

### ⚠️ Gotchas & Pitfalls
* **Iterative vs Recursive Path Compression:** Deep recursive `find` can cause StackOverflow in massive graphs; use an iterative two-pass approach if memory is strictly bounded.
* **Union by Size vs Rank:** Rank is height upper-bound; Size tracks total nodes. Both achieve inverse Ackermann time, but tracking size is often more useful for queries asking "how big is this group?".
* **Returning False on Redundant Union:** Always return `true` if a merge occurred and `false` if they were already merged. This simplifies cycle detection.

### Drill Problems
* [LeetCode 684: Redundant Connection] (Medium)

---

## Level 3: Cycle Detection

### Mental Models & Invariants
* **What does the state/index mean?** Iterating over an edge list `[u, v]`.
* **What region is processed?** Every edge in the undirected graph.
* **What is the invariant/recurrence relation?** If `u` and `v` already share the same root before `union(u, v)`, the edge `[u, v]` creates a cycle.

### Visual State Transitions
*Edges: [0,1], [1,2], [0,2]*

| Step | Edge processed | Set states (`parent`) | Root u, Root v | Action |
|---|---|---|---|---|
| 1 | `[0,1]` | `{0,1}` | 0, 1 -> 0, 0 | Merge |
| 2 | `[1,2]` | `{0,1,2}` | 0, 2 -> 0, 0 | Merge |
| 3 | `[0,2]` | `{0,1,2}` | 0, 0 | CYCLE DETECTED |

### Code Snippets

```python
# Problem: Detect redundant connections in an undirected graph by identifying if a new edge forms a cycle.
def findRedundantConnection(edges):
    # 1. Initialize optimized Union-Find for all possible nodes
    uf = UnionFindRank(len(edges) + 1)
    # 2. Iterate through each edge to build the graph
    for u, v in edges:
        # 3. If union returns False, the nodes already share a root, meaning a cycle is detected
        if not uf.union(u, v):
            return [u, v]
    return []
```

```csharp
// Problem: Detect redundant connections in an undirected graph by identifying if a new edge forms a cycle.
public int[] FindRedundantConnection(int[][] edges) {
    // 1. Initialize optimized Union-Find for all possible nodes
    UnionFindRank uf = new UnionFindRank(edges.Length + 1);
    // 2. Iterate through each edge to build the graph
    foreach(var edge in edges) {
        // 3. If Union returns false, the nodes already share a root, meaning a cycle is detected
        if(!uf.Union(edge[0], edge[1])) {
            return edge;
        }
    }
    return new int[0];
}
```

### ⚠️ Gotchas & Pitfalls
* **Directed Graphs:** Standard Union-Find detects cycles in UNDIRECTED graphs. It fails for directed graphs (use DFS/Kahn's instead).
* **Multi-edges & Self-loops:** UF naturally detects these as cycles immediately, which may or may not be desired depending on problem constraints.
* **Multiple Cycles:** UF only tells you an edge completes *a* cycle, it doesn't give you the nodes forming the cycle easily.

### Drill Problems
* [LeetCode 261: Graph Valid Tree] (Medium)

---

## Level 4: Connected Components

### Mental Models & Invariants
* **What does the state/index mean?** `components` counter starts at `N` (total nodes) and decreases by 1 on every successful `union`.
* **What region is processed?** All components globally tracking size.
* **What is the invariant/recurrence relation?** A successful union merges 2 components into 1, reducing total isolated component count by 1.

### Visual State Transitions
*Nodes = 5. Edges: [0,1], [1,2], [3,4]*

| Step | Action | Component Count | Sets |
|---|---|---|---|
| Init | - | 5 | `{0}, {1}, {2}, {3}, {4}` |
| 1 | `[0,1]` | 4 | `{0,1}, {2}, {3}, {4}` |
| 2 | `[1,2]` | 3 | `{0,1,2}, {3}, {4}` |
| 3 | `[3,4]` | 2 | `{0,1,2}, {3,4}` |

### Code Snippets

```python
# Problem: Extend the Union-Find structure to keep track of the total number of disjoint components as edges are processed.
class ComponentUF(UnionFindRank):
    def __init__(self, size):
        super().__init__(size)
        # 1. Initialize component count to total number of isolated nodes
        self.components = size
        
    def union(self, i, j):
        # 2. Attempt to merge sets using parent union logic
        merged = super().union(i, j)
        # 3. If sets were disjoint and a merge occurred, decrease the total component count
        if merged:
            self.components -= 1
        return merged
```

```csharp
// Problem: Extend the Union-Find structure to keep track of the total number of disjoint components as edges are processed.
public class ComponentUF : UnionFindRank {
    public int Components { get; private set; }
    
    public ComponentUF(int size) : base(size) {
        // 1. Initialize component count to total number of isolated nodes
        Components = size;
    }
    
    public new bool Union(int i, int j) {
        // 2. Attempt to merge sets using base union logic
        bool merged = base.Union(i, j);
        // 3. If sets were disjoint and a merge occurred, decrease the total component count
        if (merged) {
            Components--;
        }
        return merged;
    }
}
```

### ⚠️ Gotchas & Pitfalls
* **Counting Post-Hoc via `parent` array:** Scanning `parent` to count roots (`parent[i] == i`) works but only if you run `find(i)` on everything or guarantee path compression has flattened everything. Maintaining a `count` variable is safer and $O(1)$.
* **Isolated Nodes:** Always initialize count to total nodes `N`, not total edges or active nodes mentioned in edge list.
* **Dynamic Queries:** Finding max component size requires tracking `size` array instead of `rank`.

### Drill Problems
* [LeetCode 323: Number of Connected Components in an Undirected Graph] (Medium)
* [LeetCode 200: Number of Islands] (Medium) - Can be done via UF!

---

## Level 5: Kruskal's Minimum Spanning Tree (MST)

### Mental Models & Invariants
* **What does the state/index mean?** Edges sorted by weight.
* **What region is processed?** The edge pool, greedily adding edges that don't form cycles.
* **What is the invariant/recurrence relation?** The lowest weight edge connecting two disjoint sets is always part of the MST. Terminate when `components == 1` or `edges_used == N - 1`.

### Visual State Transitions
*Sorted Edges: (weight, u, v) -> (1, 0, 1), (2, 1, 2), (3, 0, 2)*

| Step | Edge Examined | Valid (No Cycle)? | MST Cost | Action |
|---|---|---|---|---|
| 1 | (1, 0, 1) | Yes | 1 | Merge 0,1 |
| 2 | (2, 1, 2) | Yes | 3 | Merge 1,2 |
| 3 | (3, 0, 2) | No (cycle) | 3 | Skip |

### Code Snippets

```python
# Problem: Apply Kruskal's algorithm using Union-Find to find the minimum cost to connect all points in a plane.
def minCostConnectPoints(points):
    n = len(points)
    edges = []
    # 1. Generate all possible edges with their respective Manhattan distances (weights)
    for i in range(n):
        for j in range(i + 1, n):
            dist = abs(points[i][0] - points[j][0]) + abs(points[i][1] - points[j][1])
            edges.append((dist, i, j))
            
    # 2. Sort edges by weight in ascending order (Greedy approach)
    edges.sort()
    uf = ComponentUF(n)
    mst_cost = 0
    edges_used = 0
    
    # 3. Process edges starting from the smallest weight
    for weight, u, v in edges:
        # 4. If u and v belong to different sets, adding this edge doesn't form a cycle
        if uf.union(u, v):
            mst_cost += weight
            edges_used += 1
            # 5. Early termination: An MST on n nodes always has exactly n - 1 edges
            if edges_used == n - 1:
                break
                
    return mst_cost
```

```csharp
// Problem: Apply Kruskal's algorithm using Union-Find to find the minimum cost to connect all points in a plane.
public int MinCostConnectPoints(int[][] points) {
    int n = points.Length;
    var edges = new List<(int weight, int u, int v)>();
    
    // 1. Generate all possible edges with their respective Manhattan distances (weights)
    for(int i = 0; i < n; i++) {
        for(int j = i + 1; j < n; j++) {
            int dist = Math.Abs(points[i][0] - points[j][0]) + Math.Abs(points[i][1] - points[j][1]);
            edges.Add((dist, i, j));
        }
    }
    
    // 2. Sort edges by weight in ascending order (Greedy approach)
    edges.Sort((a, b) => a.weight.CompareTo(b.weight));
    ComponentUF uf = new ComponentUF(n);
    int mstCost = 0;
    int edgesUsed = 0;
    
    // 3. Process edges starting from the smallest weight
    foreach(var edge in edges) {
        // 4. If u and v belong to different sets, adding this edge doesn't form a cycle
        if(uf.Union(edge.u, edge.v)) {
            mstCost += edge.weight;
            edgesUsed++;
            // 5. Early termination: An MST on n nodes always has exactly n - 1 edges
            if(edgesUsed == n - 1) break;
        }
    }
    
    return mstCost;
}
```

### ⚠️ Gotchas & Pitfalls
* **Sorting Overhead:** Sorting edges dominates runtime $O(E \log E)$. For dense graphs ($E \approx V^2$), Prim's algorithm might perform better.
* **Disconnected Graphs:** If the graph is not fully connected, Kruskal's will stop before `edges_used == N - 1`. Check if `uf.components == 1` at the end to verify a valid MST exists.
* **Edge Object Caching:** Generating pairs on the fly and storing them all uses $O(E)$ memory. If $E$ is huge, consider PriorityQueue or Prim's.

### Drill Problems
* [LeetCode 1584: Min Cost to Connect All Points] (Medium)
* [LeetCode 1135: Connecting Cities With Minimum Cost] (Medium)
