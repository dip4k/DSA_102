# Phase 12: Graph Traversal

> **Focus:** Grid as Implicit Graph, Connected Components via In-Place Sinking, Deep-Copying Cyclic Topologies, Kahn's BFS In-Degree Algorithm vs 3-State DFS Cycle Detection, and Topological Ordering.  
> **Source Curriculum:** [`Senior_dsa_question_list.md`](../Senior_dsa_question_list.md) — Phase 12 (Problems #64–#69)

---

## 64. Number of Islands (LeetCode #200)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#graph` `#grid-dfs` `#grid-bfs` `#connected-components` `#flood-fill` |
| **LeetCode Link** | [Number of Islands](https://leetcode.com/problems/number-of-islands/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given an $m \times n$ 2D binary grid `grid` representing a map of `'1'`s (land) and `'0'`s (water), return the number of islands. An island is formed by connecting adjacent lands horizontally or vertically and is surrounded by water. Assume all four edges of the grid are surrounded by water.
- **Key Constraints:**
  - $m == \text{grid.Length}, n == \text{grid}[i]\text{.Length} \in [1, 300]$.
  - $\text{grid}[i][j]$ is strictly `'0'` or `'1'`.
  - Total grid cells $V = m \times n \le 90,000$.
- **Senior Edge Cases to Defend:**
  - Entire grid filled with land ($300 \times 300 = 90,000$ cells): Recursive DFS depth can reach $90,000$, causing a stack overflow exception if thread stack limits ($1\text{ MB}$) are exceeded.
  - Grid contains zero land cells (all `'0'`): Must return `0` without queue or stack allocations.
  - Alternating checkerboard pattern: Maximizes the number of single-cell components ($\lceil (m \times n) / 2 \rceil$).

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Connected Component Counting on an implicit 4-directional planar grid graph. We scan row-by-row; each unvisited `'1'` triggers an island discovery, after which the entire landmass is "sunk" (mutated to `'0'`) to eliminate re-traversal.
- **Sample 1:**
  - **Input:** `grid = [["1","1","0"],["1","1","0"],["0","0","1"]]`
  - **Output:** `2`
- **Sample 2:**
  - **Input:** `grid = [["1","1","1"],["0","1","0"],["1","1","1"]]`
  - **Output:** `1`

---

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine the grid as an archipelago viewed from a satellite. Water is `'0'`, and land is `'1'`. When an explorer lands on an unmapped island cell, they immediately release a controlled flood (or dye) that spreads across every contiguous land cell. By mutating each visited land cell from `'1'` to `'0'` ("sinking the island"), the entire landmass is submerged into the sea. Once the flood subsides, the explorer resumes scanning from the next coordinate. Because the previously explored island is now indistinguishable from water, it can never trigger a redundant count or recursive cycle.

#### 3.2 The Naive Bottleneck & Redundant Computation
A brute-force approach without in-place mutation or visited tracking might attempt pairwise pathfinding between every pair of `'1'` cells ($O((M \times N)^2)$) to identify equivalence classes. Alternatively, maintaining an external `bool[M, N]` visited matrix incurs $O(M \times N)$ extra heap memory and poor CPU cache locality due to multi-dimensional pointer indirection. Without immediate marking upon discovery, a naive BFS pushes the same neighbor into the queue from multiple adjacent cells, leading to exponential queue explosion ($O(4^K)$ queue bloat).

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Component Counting Equivalence:** The grid defines an undirected graph $G = (V, E)$ where $V = \{(r, c) \mid \text{grid}[r][c] = \text{'1'}\}$ and $E = \{((r_1, c_1), (r_2, c_2)) \mid |r_1 - r_2| + |c_1 - c_2| = 1\}$. The number of islands equals the number of connected components in $G$.
- **Destructive Marking Invariant:** Mutating $\text{grid}[r][c] = \text{'0'}$ preserves the global topological count: every land cell belongs to exactly one connected component and is submerged into water at most once.
- **Root Trigger Invariant:** An increment of `islandCount` occurs if and only if $\text{grid}[r][c] == \text{'1'}$ during the outer linear raster scan. The subsequent traversal guarantees that when the traversal terminates, every node in that component satisfies $\text{grid}[r'][c'] == \text{'0'}$.

#### 3.4 Cursor Semantics & Invariant Partition Architecture
The algorithm uses an outer raster scan $(r, c)$ and an inner exploration frontier (call stack for DFS or FIFO queue for BFS):
- **Settled Region:** Coordinates $(i, j)$ where $i < r$ or ($i == r$ and $j < c$) that were originally `'1'` are now submerged to `'0'`.
- **Active Frontier:** Cells currently being traversed by the inner DFS stack or BFS queue.
- **Unexplored Region:** Cells ahead of the raster scan $(r, c)$ that have not yet been reached.

```text
Raster Scan (r, c) Progression:
+-------------------------------------------------------------+
| [ Settled: Sunk Land ('0') & Native Water ('0') ]           |
| (0, 0) ... (r, c - 1)                                       |
+-------------------------------------------------------------+
| Current Cell: (r, c) == '1'  ==> Trigger Island Component   |
|   |--> [ Active Frontier: DFS Stack / BFS Queue ]           |
|   |--> Sinks all 4-directional contiguous '1's to '0'       |
+-------------------------------------------------------------+
| [ Unexplored Region: Untouched '0's and '1's ]              |
| (r, c + 1) ... (M - 1, N - 1)                               |
+-------------------------------------------------------------+
```

#### 3.5 State Transition Triggers & Decision Gates
- **Outer Loop Gate:** For each cell $(r, c) \in [0, M-1] \times [0, N-1]$:
  - If $\text{grid}[r][c] == \text{'1'}$, execute `islandCount++` and invoke `Sink(r, c)`.
  - If $\text{grid}[r][c] == \text{'0'}$, advance cursor $c++$.
- **Inner Traversal Gate (Boundary & Water Guard):** For any neighbor $(nr, nc)$:
  - Condition 1: $0 \le nr < M$ and $0 \le nc < N$ (In-bounds check).
  - Condition 2: $\text{grid}[nr][nc] == \text{'1'}$ (Land check).
  - Transition: Mutate $\text{grid}[nr][nc] = \text{'0'}$ **immediately before or upon** enqueuing/recursing to prevent duplicate visits.

#### 3.6 Concrete Step-by-Step State Trace
Consider input grid ($3 \times 3$):
```text
Row 0: 1  1  0
Row 1: 1  1  0
Row 2: 0  0  1
```
- **Step 1:** Outer scan arrives at $(0, 0) == \text{'1'}$. Increment `islandCount = 1`. Trigger `Sink(0, 0)`.
- **Step 2 (Sink Component 1):**
  - Mutate $(0, 0) \to \text{'0'}$.
  - Explore neighbors of $(0, 0)$: $(1, 0)$ is `'1'`, $(0, 1)$ is `'1'`.
  - Mutate $(1, 0) \to \text{'0'}$, explore its neighbor $(1, 1)$ which is `'1'`.
  - Mutate $(1, 1) \to \text{'0'}$.
  - Mutate $(0, 1) \to \text{'0'}$.
  - Grid state after Component 1 sink:
    ```text
    0  0  0
    0  0  0
    0  0  1
    ```
- **Step 3:** Outer scan proceeds through $(0, 1), (0, 2), (1, 0), (1, 1), (1, 2), (2, 0), (2, 1)$. All are `'0'`, so loop continues without action.
- **Step 4:** Outer scan arrives at $(2, 2) == \text{'1'}$. Increment `islandCount = 2`. Trigger `Sink(2, 2)`.
  - Mutate $(2, 2) \to \text{'0'}$. All neighbors are either out-of-bounds or `'0'`.
- **Step 5:** Scan terminates at $(2, 2)$. Return `islandCount = 2`.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **When to choose Approach 1 (In-Place DFS):** Standard interviews, clean and concise code, small to medium grids ($M \times N \le 10,000$). Best when code brevity is prized and stack depth is safely bounded.
- **When to choose Approach 2 (Queue-Based BFS):** Production enterprise environments, massive grid dimensions ($M \times N \ge 90,000$), or environments with strict stack memory limits (e.g., embedded systems or default $1\text{ MB}$ Windows CLR thread stacks). BFS guarantees zero risk of `StackOverflowException`.
- **Cache Locality & Memory Budget:** DFS achieves excellent spatial locality when traversing rows sequentially, but recursive call frames carry register overhead. BFS uses a flat FIFO queue with lower per-element overhead, and its maximum queue size is bounded by the perimeter of the component ($O(\min(M, N))$), which is strictly superior to DFS worst-case stack depth ($O(M \times N)$).

#### 4.2 Step-by-Step Natural Progression Flow
1. **Setup & Boundaries:** Validate input dimensions. Return `0` immediately if `grid == null` or length is `0`.
2. **Main Exploration Loop:** Iterate $(r, c)$ across $[0, M-1] \times [0, N-1]$.
3. **Invariant Maintenance & Condition Gates:** When $\text{grid}[r][c] == \text{'1'}$, increment `islandCount` and dispatch the sinking procedure.
4. **Resolution & Return:** When the raster scan completes, all land cells are guaranteed to be `'0'`, and `islandCount` contains the exact component count.

#### 4.3 Alternative Approaches Analysis
- **Disjoint Set Union (DSU):** Can map each 2D cell $(r, c)$ to 1D index $r \times N + c$. Union adjacent land cells. While mathematically elegant and suitable for dynamic streaming grids (e.g., LC #305 Number of Islands II), DSU incurs $O(M \times N \cdot \alpha(M \times N))$ time and $O(M \times N)$ auxiliary parent arrays, making it strictly inferior to in-place DFS/BFS for static grids.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: In-Place DFS | Approach 2: Queue-Based BFS | Approach 3: Disjoint Set Union (DSU) |
| :--- | :--- | :--- | :--- |
| **Time Complexity (Best)** | $O(M \times N)$ | $O(M \times N)$ | $O(M \times N)$ |
| **Time Complexity (Avg)** | $O(M \times N)$ | $O(M \times N)$ | $O(M \times N \cdot \alpha(M \cdot N))$ |
| **Time Complexity (Worst)** | $O(M \times N)$ | $O(M \times N)$ | $O(M \times N \cdot \alpha(M \cdot N))$ |
| **Auxiliary Space** | $O(M \times N)$ call stack | $O(\min(M, N))$ queue | $O(M \times N)$ parent array |
| **Output Space** | $O(1)$ scalar count | $O(1)$ scalar count | $O(1)$ scalar count |
| **Cache Locality** | High (localized recursion) | Moderate (FIFO queue pointers) | Poor (pointer chasing in trees) |
| **In-Place Mutability** | Yes (sinks to `'0'`) | Yes (sinks to `'0'`) | No (requires external parent table) |
| **Streaming Suitability** | Poor (requires static access) | Poor (requires full grid access) | High (handles dynamic cell additions) |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Number of Islands (#64)
// Selection Heuristic: 
//   - Approach 1 (DFS): Optimal for standard interview defense; minimal boilerplate.
//   - Approach 2 (BFS): Optimal for enterprise systems; immune to StackOverflowException.
// Invariant: Mutating grid[r][c] to '0' upon discovery guarantees each node is visited once.
// ============================================================================
```

#### Approach 1: In-Place Recursive DFS Sinking (Optimal Standard)
```csharp
public class Solution
{
    public int NumIslands(char[][] grid)
    {
        // Guard against null or degenerate empty grid inputs
        if (grid == null || grid.Length == 0 || grid[0].Length == 0)
        {
            return 0;
        }

        int rows = grid.Length;
        int cols = grid[0].Length;
        int islandCount = 0;

        // Outer raster scan over all cells in row-major order
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                // Decision Gate: Unvisited land cell marks the root of a new island
                if (grid[r][c] == '1')
                {
                    islandCount++;
                    // Invariant: DfsSink will completely submerge all 4-directionally connected '1's
                    DfsSink(grid, r, c, rows, cols);
                }
            }
        }

        return islandCount;
    }

    private static void DfsSink(char[][] grid, int r, int c, int rows, int cols)
    {
        // Boundary Gate: Guard against index out-of-bounds
        // Water Gate: Skip if current cell is already water ('0')
        if (r < 0 || r >= rows || c < 0 || c >= cols || grid[r][c] != '1')
        {
            return;
        }

        // In-place sinking: Mutate land cell to water before recursing to break cycles
        grid[r][c] = '0';

        // Recurse strictly in 4 cardinal directions (Up, Down, Left, Right)
        DfsSink(grid, r + 1, c, rows, cols); // Down
        DfsSink(grid, r - 1, c, rows, cols); // Up
        DfsSink(grid, r, c + 1, rows, cols); // Right
        DfsSink(grid, r, c - 1, rows, cols); // Left
    }
}
```

#### Approach 2: Queue-Based BFS Sinking (Stack-Safe Enterprise)
```csharp
public class SolutionBfs
{
    // Direction offsets for cardinal neighbor exploration: Down, Up, Right, Left
    private static readonly (int dr, int dc)[] Directions = 
    { 
        (1, 0), (-1, 0), (0, 1), (0, -1) 
    };

    public int NumIslands(char[][] grid)
    {
        if (grid == null || grid.Length == 0 || grid[0].Length == 0) return 0;

        int rows = grid.Length;
        int cols = grid[0].Length;
        int islandCount = 0;
        
        // Reusable FIFO queue storing 2D coordinates encoded as value tuples
        var queue = new Queue<(int r, int c)>();

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                if (grid[r][c] == '1')
                {
                    islandCount++;
                    // Critical Invariant: Sink root immediately upon discovery to prevent duplicate enqueues
                    grid[r][c] = '0';
                    queue.Enqueue((r, c));

                    while (queue.Count > 0)
                    {
                        var (currR, currC) = queue.Dequeue();

                        foreach (var (dr, dc) in Directions)
                        {
                            int nr = currR + dr;
                            int nc = currC + dc;

                            // In-bounds verification and land check
                            if (nr >= 0 && nr < rows && nc >= 0 && nc < cols && grid[nr][nc] == '1')
                            {
                                // Critical Invariant: Sink neighbor IMMEDIATELY before enqueuing.
                                // Sinking on dequeue causes duplicate queue insertions and O(4^K) memory bloat.
                                grid[nr][nc] = '0';
                                queue.Enqueue((nr, nc));
                            }
                        }
                    }
                }
            }
        }

        return islandCount;
    }
}
```

---

## 65. Clone Graph (LeetCode #133)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | 🔥 High |
| **Pattern Tags** | `#graph` `#hash-map` `#dfs` `#bfs` `#deep-copy` |
| **LeetCode Link** | [Clone Graph](https://leetcode.com/problems/clone-graph/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given a reference of a node in a connected undirected graph, return a deep copy (clone) of the graph. Each node in the graph contains an integer `val` and a list of its neighbors `List<Node>`.
- **Node Definition:**
  ```csharp
  public class Node {
      public int val;
      public IList<Node> neighbors;
      public Node(int val = 0, IList<Node> neighbors = null) {
          this.val = val;
          this.neighbors = neighbors ?? new List<Node>();
      }
  }
  ```
- **Key Constraints:**
  - Number of nodes $V \in [0, 100]$.
  - Node values $1 \le \text{node.val} \le 100$, all values are distinct.
  - Graph is connected and undirected. Cycles and self-loops are permissible.
- **Senior Edge Cases to Defend:**
  - Input `node == null`: Must immediately return `null` without throwing `NullReferenceException`.
  - Single node with empty `neighbors` list: Return a cloned node with identical value and an empty list.
  - Cyclic topologies (e.g., triangle $1-2-3-1$): Traversal must not enter an infinite loop.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Graph Deep Copy with Memoized Bijection. Construct a one-to-one mapping from original `Node` references to freshly instantiated cloned `Node` references, replicating topology while preventing cycle-induced infinite recursion.
- **Sample 1:**
  - **Input:** `adjList = [[2,4],[1,3],[2,4],[1,3]]`
  - **Output:** Cloned graph where node 1 connects to 2 and 4, node 2 connects to 1 and 3, etc., but all object references are completely distinct from the original instances.

---

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine an architect reproducing a complex network of interconnected rooms. As the architect walks through Room $u$, they immediately construct a replica Room $u'$ in a new warehouse and record the pairing $(u \leftrightarrow u')$ in an index registry. When exploring hallways leading out of Room $u$, if a hallway leads to a room that has already been built (found in the registry), the architect simply builds a corridor to the existing replica rather than duplicating the room. If it leads to an unbuilt room, the architect builds it and continues walking.

#### 3.2 The Naive Bottleneck & Redundant Computation
A naive tree-copy algorithm recursively instantiates new nodes for every neighbor. In a cyclic graph, this leads to infinite recursion and `StackOverflowException`. In a graph with multiple paths to the same node (e.g., a diamond graph), without memoization, the algorithm duplicates nodes exponentially, resulting in an invalid graph topology where multiple distinct nodes share the same value instead of forming a single merged vertex.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Bijection Invariant:** There exists a strict bijection $f: V_{\text{orig}} \to V_{\text{clone}}$. For every original node $u \in V_{\text{orig}}$, there is exactly one cloned node $u' \in V_{\text{clone}}$ such that $u' = f(u)$.
- **Memoization Timing Invariant:** Cloned node $u'$ must be registered in the dictionary `visited[u] = u'` **before** recursing or iterating over $u$'s neighbors. This breaks directed and undirected cycles: when neighbor $v$ attempts to traverse back to $u$, `visited.TryGetValue(u, out var clone)` succeeds in $O(1)$, returning the pre-existing clone and closing the loop.

#### 3.4 Cursor Semantics & Invariant Partition Architecture
- **Settled Nodes:** Vertices $u$ present in `visited` dictionary whose clone $u'$ has been instantiated and whose neighbors are either currently being wired or fully wired.
- **Frontier:** Recursion stack frames (DFS) or FIFO queue elements (BFS) actively processing neighbor adjacency lists.
- **Unexplored:** Nodes reachable in the original graph that have not yet been placed in the dictionary.

```text
Graph Cloning State Partition:
+-------------------------------------------------------------+
| Cloned Registry (Dictionary<Node, Node>):                   |
|   Original u  --> Cloned u'  (Instance instantiated)        |
|   Any edge to u returns u' immediately (Cycle Break)        |
+-------------------------------------------------------------+
| Active Frontier (Queue / Call Stack):                       |
|   Current node u: For each neighbor v in u.neighbors:       |
|     - If v in visited: clone_u.neighbors.Add(visited[v])   |
|     - Else: visited[v] = new Node(v.val); recurse/enqueue   |
+-------------------------------------------------------------+
| Unexplored Graph Nodes: Awaiting discovery                  |
+-------------------------------------------------------------+
```

#### 3.5 State Transition Triggers & Decision Gates
- **Initial Guard:** If `node == null`, return `null`.
- **Lookup Gate:** When inspecting neighbor $v$ of current node $u$:
  - Gate 1: If `visited.ContainsKey(v)` is true:
    - Action: Directly append `visited[v]` to `visited[u].neighbors`.
  - Gate 2: If `visited.ContainsKey(v)` is false:
    - Action: Instantiate `v' = new Node(v.val)`. Store `visited[v] = v'`. Push $v$ to frontier. Append $v'$ to `visited[u].neighbors`.

#### 3.6 Concrete Step-by-Step State Trace
Consider graph with 2 nodes mutually connected: $1 \leftrightarrow 2$.
- **Step 1:** Call `CloneGraph(1)`.
  - `visited` is empty. Instantiate `clone1 = new Node(1)`.
  - Register `visited[1] = clone1`.
- **Step 2:** Iterate neighbors of node 1: Neighbor is node 2.
  - `visited.ContainsKey(2)` is `false`.
  - Instantiate `clone2 = new Node(2)`. Register `visited[2] = clone2`.
  - Recurse on node 2:
- **Step 3:** Inside `CloneGraph(2)`:
  - Iterate neighbors of node 2: Neighbor is node 1.
  - `visited.ContainsKey(1)` is `true`! Return `visited[1]` (`clone1`).
  - Append `clone1` to `clone2.neighbors`.
  - Node 2 neighbor loop finishes. Return `clone2`.
- **Step 4:** Back in `CloneGraph(1)`:
  - Append `clone2` to `clone1.neighbors`.
  - Node 1 neighbor loop finishes. Return `clone1`.
- Cloned topology is verified: `clone1.neighbors` contains `clone2`, and `clone2.neighbors` contains `clone1`.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **When to choose Approach 1 (Recursive DFS):** Extremely clean, functional, and idiomatic in interviews. Ideal when graph size is small ($V \le 100$).
- **When to choose Approach 2 (Iterative BFS):** Production systems with arbitrary or unknown graph depths where recursion depth could overflow. Guarantees deterministic heap memory management.
- **Cache Locality & Overhead:** BFS allocates a queue alongside the dictionary. DFS uses call stack frames. Both have $O(V)$ auxiliary space and $O(V + E)$ time complexity.

#### 4.2 Step-by-Step Natural Progression Flow
1. **Setup & Boundaries:** Check for `node == null`. Initialize `Dictionary<Node, Node>`.
2. **Main Exploration Loop:** Traverse graph via DFS recursion or BFS queue dequeue.
3. **Invariant Maintenance & Condition Gates:** Instantiate clone before traversing neighbors. Check dictionary before instantiating any neighbor.
4. **Resolution & Return:** Return the cloned node corresponding to the input root.

#### 4.3 Alternative Approaches Analysis
- **Array-based Indexing:** Since $1 \le \text{node.val} \le 100$, an array `Node[101]` can replace the dictionary. This eliminates hashing collisions and achieves $O(1)$ direct array indexing with near-zero overhead.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: Recursive DFS | Approach 2: Iterative BFS | Approach 3: Array Indexed Map |
| :--- | :--- | :--- | :--- |
| **Time Complexity (Best/Avg/Worst)** | $O(V + E)$ | $O(V + E)$ | $O(V + E)$ |
| **Auxiliary Space** | $O(V)$ map + $O(V)$ stack | $O(V)$ map + $O(V)$ queue | $O(V)$ array + $O(V)$ queue |
| **Output Space** | $O(V + E)$ cloned graph | $O(V + E)$ cloned graph | $O(V + E)$ cloned graph |
| **Cache Locality** | Moderate | High | Excellent (contiguous array) |
| **In-Place Mutability** | No (pure deep copy) | No (pure deep copy) | No (pure deep copy) |
| **Stack Overflow Risk** | Low for $V \le 100$, High for large $V$ | Zero | Zero |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Clone Graph (#65)
// Selection Heuristic: 
//   - Approach 1 (DFS): Preferred in interviews for conciseness; recursion handles wiring.
//   - Approach 2 (BFS): Preferred in enterprise backend services for stack safety.
// Invariant: Register clone in dictionary BEFORE visiting neighbors to neutralize cycles.
// ============================================================================
```

#### Approach 1: Recursive DFS with Lookup Map (Standard)
```csharp
public class Solution
{
    // Lookup dictionary mapping original Node reference to its cloned counterpart
    private readonly Dictionary<Node, Node> _clonedMap = new();

    public Node CloneGraph(Node node)
    {
        // Edge Case: Null graph reference
        if (node == null) return null;

        // Invariant: If node was already cloned, return existing instance to break cyclic recursion
        if (_clonedMap.TryGetValue(node, out var existingClone))
        {
            return existingClone;
        }

        // Critical Timing: Instantiate clone and register in map BEFORE exploring neighbors
        var clone = new Node(node.val);
        _clonedMap[node] = clone;

        // Recursively clone and wire all neighbors
        foreach (var neighbor in node.neighbors)
        {
            clone.neighbors.Add(CloneGraph(neighbor));
        }

        return clone;
    }
}
```

#### Approach 2: Iterative BFS with Queue (Stack-Safe)
```csharp
public class SolutionBfs
{
    public Node CloneGraph(Node node)
    {
        if (node == null) return null;

        var clonedMap = new Dictionary<Node, Node>();
        var queue = new Queue<Node>();

        // Initialize root clone and enqueue
        clonedMap[node] = new Node(node.val);
        queue.Enqueue(node);

        while (queue.Count > 0)
        {
            Node current = queue.Dequeue();

            foreach (Node neighbor in current.neighbors)
            {
                // If neighbor hasn't been instantiated yet, clone and enqueue it
                if (!clonedMap.ContainsKey(neighbor))
                {
                    clonedMap[neighbor] = new Node(neighbor.val);
                    queue.Enqueue(neighbor);
                }

                // Wire edge from current clone to neighbor clone
                clonedMap[current].neighbors.Add(clonedMap[neighbor]);
            }
        }

        return clonedMap[node];
    }
}
```

---

## 66. Number of Provinces (LeetCode #547)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | 🔥 High |
| **Pattern Tags** | `#graph` `#dfs` `#union-find` `#connected-components` `#adjacency-matrix` |
| **LeetCode Link** | [Number of Provinces](https://leetcode.com/problems/number-of-provinces/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** There are $n$ cities. You are given an $n \times n$ matrix `isConnected` where `isConnected[i][j] = 1` if city $i$ and city $j$ are directly connected, and `0` otherwise. Return the total number of provinces (connected components).
- **Key Constraints:**
  - $1 \le n \le 200$.
  - $\text{isConnected}[i][i] == 1$.
  - $\text{isConnected}[i][j] == \text{isConnected}[j][i]$.
- **Senior Edge Cases to Defend:**
  - Fully disconnected graph: Identity matrix $\implies n$ provinces.
  - Fully connected complete graph ($K_n$): All entries are $1 \implies 1$ province.
  - Linear chain: City 0 connected to 1, 1 to 2, ..., $n-2$ to $n-1 \implies 1$ province.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Count connected components in an undirected adjacency matrix graph. Traverse each unvisited vertex using DFS/BFS to mark its entire transitive closure, or merge disjoint sets dynamically.
- **Sample 1:**
  - **Input:** `isConnected = [[1,1,0],[1,1,0],[0,0,1]]`
  - **Output:** `2`

---

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Think of $n$ cities on a map. Some are linked by highways. A province is a political territory where every city in it can reach every other city by highway (directly or indirectly). We walk through cities $0$ to $n - 1$. When we step into an unvisited city, we declare a new province and hoist a flag. We then send messengers along every connecting highway, marking all reached cities as claimed by this province. When the messengers return, we continue looking for the next unclaimed city.

#### 3.2 The Naive Bottleneck & Redundant Computation
A brute-force approach might compute the transitive closure matrix using the Floyd-Warshall algorithm in $O(N^3)$ time, checking reachability between all city pairs. Scanning the entire adjacency matrix repeatedly without a `visited` array would cause infinite loops between mutually connected cities $i$ and $j$.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Component Partition Invariant:** Connectedness is an equivalence relation (reflexive, symmetric, transitive). The graph vertices are partitioned into disjoint equivalence classes.
- **Single Discovery Invariant:** A boolean array `visited[i]` of length $n$ tracks claimed cities. When scanning index $i$ from $0$ to $n - 1$, if `visited[i] == false`, city $i$ must belong to an undiscovered equivalence class. Incrementing `provinces++` and launching a DFS from $i$ marks all nodes in that equivalence class in $O(N)$ operations per vertex, totaling $O(N^2)$ time to scan the matrix.

#### 3.4 Cursor Semantics & Invariant Partition Architecture
- **Raster Cursor $i \in [0, n - 1]$:** Iterates through all vertices.
- **`visited` Bitset/Array:** Marks whether city $k$ has been absorbed into an identified province.
- **Inner Traversal Cursor:** Traverses row $u$ of the matrix, checking all potential neighbors $v \in [0, n - 1]$.

```text
City State Partition During Outer Scan:
[ Cities 0 ... i - 1: Fully Visited & Partitioned into Provinces ]
City i:
  - If visited[i] == true: Skip (already assigned to earlier province)
  - If visited[i] == false:
      provinceCount++
      DFS(i) --> Sets visited[v] = true for all v reachable from i
[ Cities i + 1 ... n - 1: Unexplored Candidates ]
```

#### 3.5 State Transition Triggers & Decision Gates
- **Outer Scan Gate:** If `!visited[i]`, increment `provinceCount` and invoke `Dfs(i)`.
- **DFS Transition Gate:** For current city $u$, iterate $v \in [0, n - 1]$:
  - Condition: `isConnected[u][v] == 1 && !visited[v]`.
  - Action: Mark `visited[v] = true` and recurse `Dfs(v)`.

#### 3.6 Concrete Step-by-Step State Trace
Input `isConnected` ($3 \times 3$):
```text
Row 0: [1, 1, 0]
Row 1: [1, 1, 0]
Row 2: [0, 0, 1]
```
- Initialize `visited = [F, F, F]`, `provinces = 0`.
- **Outer $i = 0$:** `visited[0]` is `false`.
  - `provinces = 1`. Launch `Dfs(0)`.
  - `visited[0] = true`.
  - Check neighbors of 0:
    - $v = 0$: `visited[0]` is true, skip.
    - $v = 1$: `isConnected[0][1] == 1` and `!visited[1]`. Recurse `Dfs(1)`.
      - In `Dfs(1)`: `visited[1] = true`.
      - Check neighbors of 1:
        - $v = 0$: `visited[0]` true, skip.
        - $v = 1$: `visited[1]` true, skip.
        - $v = 2$: `isConnected[1][2] == 0`, skip.
      - Return from `Dfs(1)`.
    - $v = 2$: `isConnected[0][2] == 0`, skip.
  - Return from `Dfs(0)`. `visited` is now `[T, T, F]`.
- **Outer $i = 1$:** `visited[1]` is `true`. Skip.
- **Outer $i = 2$:** `visited[2]` is `false`.
  - `provinces = 2`. Launch `Dfs(2)`.
  - `visited[2] = true`. Check neighbors of 2: only self-loop $v = 2$ is 1.
  - Return from `Dfs(2)`. `visited` is `[T, T, T]`.
- Loop finishes. Return `provinces = 2`.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **When to choose Approach 1 (DFS Matrix Traversal):** Optimal for dense or fixed adjacency matrices. Requires zero auxiliary data structures beyond a boolean array of size $N$. Minimal code complexity.
- **When to choose Approach 2 (Disjoint Set Union):** Preferred when connections are streaming or arrive dynamically as an edge list. DSU allows online query capability.
- **Complexity Trade-Off:** In an adjacency matrix of size $N \times N$, any algorithm must read $O(N^2)$ entries. DFS achieves $O(N^2)$ time with strictly $O(N)$ space. DSU achieves $O(N^2 \cdot \alpha(N))$ time with $O(N)$ space.

#### 4.2 Step-by-Step Natural Progression Flow
1. **Setup & Boundaries:** Initialize `visited = new bool[n]`, `provinces = 0`.
2. **Main Exploration Loop:** Iterate $i$ from $0$ to $n - 1$.
3. **Invariant Maintenance & Condition Gates:** If $i$ is unvisited, increment count and run DFS across row $i$ to exhaustively mark reachable vertices.
4. **Resolution & Return:** Return final province count.

#### 4.3 Alternative Approaches Analysis
- **BFS with Queue:** Replace DFS call stack with a FIFO queue. Achieves identical $O(N^2)$ time and $O(N)$ space, offering stack safety if $N$ is very large (though for $N \le 200$, stack overflow is impossible).

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: DFS Traversal | Approach 2: Disjoint Set Union (DSU) | Approach 3: BFS Traversal |
| :--- | :--- | :--- | :--- |
| **Time Complexity (All Cases)** | $O(N^2)$ | $O(N^2 \cdot \alpha(N))$ | $O(N^2)$ |
| **Auxiliary Space** | $O(N)$ visited array | $O(N)$ parent/rank arrays | $O(N)$ visited + queue |
| **Output Space** | $O(1)$ | $O(1)$ | $O(1)$ |
| **Cache Locality** | High (row-major matrix scans) | Moderate | High |
| **Streaming Suitability** | Poor (static matrix required) | High (handles dynamic edges) | Poor |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Number of Provinces (#66)
// Selection Heuristic: 
//   - Approach 1 (DFS): Optimal O(N^2) time with minimal O(N) auxiliary space.
//   - Approach 2 (DSU): Standard pattern for dynamic edge streams and equivalence grouping.
// Invariant: An unvisited city i represents a newly discovered connected component.
// ============================================================================
```

#### Approach 1: DFS Component Traversal (Optimal Matrix Traversal)
```csharp
public class Solution
{
    public int FindCircleNum(int[][] isConnected)
    {
        if (isConnected == null || isConnected.Length == 0) return 0;

        int n = isConnected.Length;
        bool[] visited = new bool[n];
        int provinces = 0;

        // Iterate over each city; each unvisited city triggers a new province
        for (int i = 0; i < n; i++)
        {
            if (!visited[i])
            {
                provinces++;
                // Invariant: Dfs will mark every city in i's connected component as visited
                Dfs(isConnected, visited, i, n);
            }
        }

        return provinces;
    }

    private static void Dfs(int[][] isConnected, bool[] visited, int city, int n)
    {
        // Mark current city as visited immediately
        visited[city] = true;

        // Scan the entire row for directly connected neighbors
        for (int neighbor = 0; neighbor < n; neighbor++)
        {
            // Decision Gate: Direct highway exists AND neighbor has not yet been claimed
            if (isConnected[city][neighbor] == 1 && !visited[neighbor])
            {
                Dfs(isConnected, visited, neighbor, n);
            }
        }
    }
}
```

#### Approach 2: Disjoint Set Union (Union by Rank & Path Compression)
```csharp
public class SolutionDsu
{
    public int FindCircleNum(int[][] isConnected)
    {
        int n = isConnected.Length;
        int[] parent = new int[n];
        int[] rank = new int[n];
        for (int i = 0; i < n; i++) parent[i] = i;

        int provinces = n;

        // Find with Path Compression: Flattens the tree directly to root
        int Find(int x)
        {
            if (parent[x] != x)
            {
                parent[x] = Find(parent[x]);
            }
            return parent[x];
        }

        // Union by Rank: Attaches shallower tree under deeper tree
        void Union(int x, int y)
        {
            int rootX = Find(x);
            int rootY = Find(y);

            if (rootX != rootY)
            {
                if (rank[rootX] < rank[rootY])
                {
                    parent[rootX] = rootY;
                }
                else if (rank[rootX] > rank[rootY])
                {
                    parent[rootY] = rootX;
                }
                else
                {
                    parent[rootY] = rootX;
                    rank[rootX]++;
                }

                // Invariant: Every successful union of disjoint components reduces province count by 1
                provinces--;
            }
        }

        // Iterate strictly over the upper triangle to avoid redundant pair checks
        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                if (isConnected[i][j] == 1)
                {
                    Union(i, j);
                }
            }
        }

        return provinces;
    }
}
```

---

## 67. Number of Connected Components in an Undirected Graph (LeetCode #323)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#graph` `#union-find` `#dfs` `#bfs` `#edge-list` |
| **LeetCode Link** | [Number of Connected Components](https://leetcode.com/problems/number-of-connected-components-in-an-undirected-graph/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given an integer $n$ and an array of edges where `edges[i] = [a_i, b_i]` indicates an edge between $a_i$ and $b_i$ in an undirected graph, return the number of connected components in the graph.
- **Key Constraints:**
  - $1 \le n \le 2000$.
  - $1 \le \text{edges.Length} \le 5000$.
  - $0 \le a_i, b_i < n$, $a_i \ne b_i$.
  - No duplicate edges.
- **Senior Edge Cases to Defend:**
  - `edges` is empty: Graph consists of $n$ completely isolated single-node components $\implies$ return $n$.
  - Forest with multiple trees and disconnected isolated nodes.
  - Complete dense subgraph alongside completely isolated vertices.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Component Counting on a Sparse Edge List. Start with $n$ disjoint singletons. Each edge between distinct components merges them, decrementing component count.
- **Sample 1:**
  - **Input:** `n = 5`, `edges = [[0, 1], [1, 2], [3, 4]]`
  - **Output:** `2` (Components are $\{0, 1, 2\}$ and $\{3, 4\}$)

---

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine $n$ isolated islands in an ocean, each with count $1$ (total components = $n$). Construction crews lay bridges one by one from the given edge list. Before building a bridge between island $u$ and island $v$, the surveyor checks if $u$ and $v$ are already connected by some sequence of existing bridges. If they are already in the same archipelago, the new bridge adds redundant connectivity (a cycle) and changes nothing. If they are in different archipelagos, building the bridge unifies two separate landmasses into one, decreasing the total count of isolated archipelagos by exactly $1$.

#### 3.2 The Naive Bottleneck & Redundant Computation
Building an $N \times N$ adjacency matrix for sparse graphs wastes $O(N^2)$ memory ($2000 \times 2000 = 4 \times 10^6$ ints) when there are only $5000$ edges. Building an adjacency list and running DFS/BFS requires allocating list arrays and node references ($O(V + E)$), followed by traversing all edges twice. Disjoint Set Union (DSU) avoids building any adjacency structure entirely, streaming the edges directly.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Spanning Forest Invariant:** In any undirected graph with $V$ vertices, an acyclic subgraph (forest) with $E_{\text{tree}}$ edges partitions the graph into exactly $C = V - E_{\text{tree}}$ connected components.
- **Cycle Invariance:** An edge $(u, v)$ reduces the component count if and only if $\text{Find}(u) \ne \text{Find}(v)$. If $\text{Find}(u) == \text{Find}(v)$, the edge is a back-edge within an existing component and does not alter the count.

#### 3.4 Cursor Semantics & Invariant Partition Architecture
- **DSU Parent Array (`parent[x]`):** Stores the parent pointer of node $x$, initially pointing to itself.
- **Rank Array (`rank[x]`):** Stores tree height bound to balance unions.
- **Component Counter (`Count`):** Initialized to $n$. Decremented on each successful merge.

```text
DSU Evolution on edges [[0, 1], [1, 2], [3, 4]]:
Initial Forest (Count = 5):
  {0}  {1}  {2}  {3}  {4}

Edge (0, 1): Find(0) != Find(1) ==> Union(0, 1) ==> Count = 4
  {0, 1}  {2}  {3}  {4}

Edge (1, 2): Find(1) != Find(2) ==> Union(0, 2) ==> Count = 3
  {0, 1, 2}  {3}  {4}

Edge (3, 4): Find(3) != Find(4) ==> Union(3, 4) ==> Count = 2
  {0, 1, 2}  {3, 4}
Final Component Count = 2
```

#### 3.5 State Transition Triggers & Decision Gates
- For each edge `[u, v]` in `edges`:
  - Compute `rootU = Find(u)` and `rootV = Find(v)`.
  - Gate: If `rootU == rootV`, do nothing (cycle/redundant edge).
  - Gate: If `rootU != rootV`, attach the root of the lower rank tree to the root of the higher rank tree, and execute `ComponentCount--`.

#### 3.6 Concrete Step-by-Step State Trace
Input: `n = 5`, `edges = [[0, 1], [1, 2], [3, 4]]`.
- `parent = [0, 1, 2, 3, 4]`, `rank = [0, 0, 0, 0, 0]`, `Count = 5`.
- **Process Edge `[0, 1]`:**
  - `Find(0) = 0`, `Find(1) = 1`. Different roots.
  - `rank[0] == rank[1] \implies parent[1] = 0, rank[0] = 1`.
  - `Count = 4`.
- **Process Edge `[1, 2]`:**
  - `Find(1) = 0`, `Find(2) = 2`. Different roots.
  - `rank[0] (1) > rank[2] (0) \implies parent[2] = 0`.
  - `Count = 3`.
- **Process Edge `[3, 4]`:**
  - `Find(3) = 3`, `Find(4) = 4`. Different roots.
  - `rank[3] == rank[4] \implies parent[4] = 3, rank[3] = 1`.
  - `Count = 2`.
- End of edges. Return `Count = 2`.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **When to choose Approach 1 (DSU):** Default and strictly optimal for edge-list input. Avoids adjacency list construction, processes edges in a single streaming pass, and exhibits exceptional cache locality.
- **When to choose Approach 2 (Adjacency List + BFS/DFS):** When the problem requires returning the actual vertices belonging to each component (not just the count), or when graph traversal paths must be inspected.

#### 4.2 Step-by-Step Natural Progression Flow
1. **Setup & Boundaries:** Initialize `DisjointSet` with $n$ elements.
2. **Main Exploration Loop:** Iterate over each edge `[u, v]`.
3. **Invariant Maintenance & Condition Gates:** Apply `Union(u, v)`. If roots differ, decrement counter.
4. **Resolution & Return:** Return `dsu.ComponentCount`.

#### 4.3 Alternative Approaches Analysis
- **Adjacency List BFS:** Construct `List<int>[n]`. Populate with bidirectional edges. Run BFS using `visited[n]`. Increment count on each BFS trigger from unvisited nodes. Complexity: $O(V + E)$ time and space.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: Disjoint Set Union (DSU) | Approach 2: Adjacency List + BFS/DFS |
| :--- | :--- | :--- |
| **Time Complexity** | $O(V + E \cdot \alpha(V))$ | $O(V + E)$ |
| **Auxiliary Space** | $O(V)$ (parent + rank arrays) | $O(V + E)$ (graph adjacency list + visited + queue) |
| **Edge Processing** | Single-pass streaming | Two-pass (build graph then traverse) |
| **Memory Allocation** | Strictly 2 flat integer arrays | Multiple `List<int>` object allocations |
| **Cache Locality** | High (flat contiguous memory) | Moderate (pointer indirection in adjacency lists) |

---

### 5. Production C# Implementation

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Number of Connected Components (#67)
// Selection Heuristic: 
//   - Approach 1 (DSU): Optimal for sparse edge lists; eliminates graph allocation.
//   - Approach 2 (Adjacency BFS): Preferred if component member lists are required.
// Invariant: Count starts at n; each valid tree edge decrements Count by exactly 1.
// ============================================================================
```

#### Approach 1: Disjoint Set Union (Optimal Single-Pass)
```csharp
public class Solution
{
    public int CountComponents(int n, int[][] edges)
    {
        var dsu = new DisjointSet(n);

        // Stream through each edge and union endpoints
        foreach (var edge in edges)
        {
            dsu.Union(edge[0], edge[1]);
        }

        return dsu.ComponentCount;
    }

    private sealed class DisjointSet
    {
        private readonly int[] _parent;
        private readonly int[] _rank;
        public int ComponentCount { get; private set; }

        public DisjointSet(int n)
        {
            ComponentCount = n;
            _parent = new int[n];
            _rank = new int[n];
            // Initialize each node as its own root
            for (int i = 0; i < n; i++) _parent[i] = i;
        }

        // Path compression: Flattens tree hierarchy during lookup
        public int Find(int x)
        {
            if (_parent[x] != x)
            {
                _parent[x] = Find(_parent[x]);
            }
            return _parent[x];
        }

        // Union by rank: Keeps trees balanced
        public bool Union(int x, int y)
        {
            int rootX = Find(x);
            int rootY = Find(y);

            // Cycle detected: Already in same component, no count decrement
            if (rootX == rootY) return false;

            // Merge smaller tree under larger tree
            if (_rank[rootX] < _rank[rootY])
            {
                _parent[rootX] = rootY;
            }
            else if (_rank[rootX] > _rank[rootY])
            {
                _parent[rootY] = rootX;
            }
            else
            {
                _parent[rootY] = rootX;
                _rank[rootX]++;
            }

            // Invariant: Merging two distinct components reduces total count by 1
            ComponentCount--;
            return true;
        }
    }
}
```

#### Approach 2: Adjacency List + BFS Traversal
```csharp
public class SolutionBfs
{
    public int CountComponents(int n, int[][] edges)
    {
        // Construct adjacency list
        var adj = new List<int>[n];
        for (int i = 0; i < n; i++) adj[i] = new List<int>();

        foreach (var edge in edges)
        {
            adj[edge[0]].Add(edge[1]);
            adj[edge[1]].Add(edge[0]);
        }

        bool[] visited = new bool[n];
        int count = 0;
        var queue = new Queue<int>();

        for (int i = 0; i < n; i++)
        {
            if (!visited[i])
            {
                count++;
                visited[i] = true;
                queue.Enqueue(i);

                while (queue.Count > 0)
                {
                    int curr = queue.Dequeue();

                    foreach (int neighbor in adj[curr])
                    {
                        if (!visited[neighbor])
                        {
                            visited[neighbor] = true;
                            queue.Enqueue(neighbor);
                        }
                    }
                }
            }
        }

        return count;
    }
}
```

---

## 68. Course Schedule (LeetCode #207)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#graph` `#topological-sort` `#kahns-algorithm` `#cycle-detection` `#3-state-dfs` |
| **LeetCode Link** | [Course Schedule](https://leetcode.com/problems/course-schedule/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** There are `numCourses` courses labeled from $0$ to `numCourses - 1`. You are given an array `prerequisites` where `prerequisites[i] = [a_i, b_i]` indicates that you must take course $b_i$ first before course $a_i$ ($b_i \to a_i$). Return `true` if you can finish all courses, or `false` otherwise (i.e. detect if a directed cycle exists).
- **Key Constraints:**
  - $1 \le \text{numCourses} \le 2000$.
  - $0 \le \text{prerequisites.Length} \le 5000$.
  - All pairs $[a_i, b_i]$ are unique.
- **Senior Edge Cases to Defend:**
  - Direct 2-node cycle: $[[1, 0], [0, 1]] \implies \text{false}$.
  - Self-loop: $[[0, 0]] \implies \text{false}$ (course requires itself).
  - Disconnected Directed Acyclic Graph (DAG): Multiple independent dependency trees $\implies \text{true}$.
  - Large cycle embedded within a larger DAG: Must detect the cycle even if peripheral nodes are acyclic.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Directed Cycle Detection in a Dependency Graph. A valid completion order exists if and only if the graph is a Directed Acyclic Graph (DAG). Solved via Kahn's In-Degree BFS or 3-State DFS Back-Edge Coloring.
- **Sample 1:**
  - **Input:** `numCourses = 2, prerequisites = [[1, 0]]` $\implies$ `true` (Take 0 then 1).
- **Sample 2:**
  - **Input:** `numCourses = 2, prerequisites = [[1, 0], [0, 1]]` $\implies$ `false` (Deadlock cycle).

---

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
- **Kahn's Onion Peeling:** Think of courses as layers of an onion. A course with in-degree 0 has zero remaining prerequisites and sits on the outermost surface. You can safely peel (take) this course. When peeled, its outgoing prerequisite edges disappear, possibly exposing new courses with in-degree 0 on the next layer. If the entire graph is peeled away, all courses can be finished. If a cycle exists, the courses inside the cycle form a locked circle of mutual dependencies; none can ever reach in-degree 0, and peeling grinds to a premature halt.
- **3-State DFS Recursion Path (Call-Stack Tracing):** Think of traversing a maze. Unvisited rooms are White. When entering a room, you leave a live flashlight on (Gray). When leaving the room after all forward paths have been checked, you turn off the flashlight and paint the room Black (safe). If you step into a room that already has a live flashlight shining (Gray), you have walked in a circle and hit your own footprints on the active path (a back-edge) $\implies$ deadlock!

#### 3.2 The Naive Bottleneck & Redundant Computation
A brute-force cycle detection searches for all simple paths starting from each node $u$ to determine if any path loops back to $u$. For dense graphs or complex DAGs, path enumeration runs in $O(V!)$ time. Even maintaining a simple `visited` boolean set fails because in a DAG, multiple valid paths can converge on the same shared prerequisite (diamond graph: $A \to B \to D$ and $A \to C \to D$). Marking $D$ as "visited" prematurely without distinguishing between *nodes on the current recursion stack* versus *nodes already verified cycle-free* causes false-positive cycle reports.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **DAG Topological Invariant:** A directed graph has a topological ordering if and only if it contains no directed cycles.
- **Kahn's Peeling Invariant:** Every DAG contains at least one vertex with in-degree 0. Removing an in-degree 0 vertex and its incident edges yields another DAG. Inductively, Kahn's algorithm terminates with `processedCount == numCourses` if and only if the graph is acyclic.
- **Back-Edge Theorem:** A directed graph contains a cycle if and only if a depth-first search yields a **back-edge** (an edge pointing to an ancestor currently on the recursion call stack, represented by state `Visiting` / Gray).

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
Kahn's BFS In-Degree State Partition:
+-------------------------------------------------------------+
| Settled Courses (processedCount):                           |
|   In-degree reached 0, popped from queue, edges removed     |
+-------------------------------------------------------------+
| Active Frontier (Queue):                                    |
|   Currently ready courses with in-degree == 0               |
+-------------------------------------------------------------+
| Remaining Graph:                                            |
|   Courses with in-degree > 0 awaiting prerequisite clearing |
|   * If cycle exists, cycle nodes remain stuck here *        |
+-------------------------------------------------------------+
```

```text
3-State DFS Coloring Partition:
State 0 (Unvisited / White): Node has not been touched yet.
State 1 (Visiting / Gray):   Node is currently on the active recursion call stack.
                             An edge pointing here is a BACK-EDGE (Cycle detected!).
State 2 (Visited / Black):   Subtree rooted at node is completely explored and acyclic.
                             Safe to skip immediately in O(1).
```

#### 3.5 State Transition Triggers & Decision Gates
- **Kahn's BFS Gate:**
  - Enqueue condition: `inDegree[course] == 0`.
  - Edge relaxation: When course $u$ is dequeued, for each neighbor $v$ in `adj[u]`:
    - Decrement `inDegree[v]--`.
    - Gate: If `inDegree[v] == 0`, `queue.Enqueue(v)`.
- **3-State DFS Gate:**
  - For node $u$: Set `state[u] = Visiting`.
  - For each neighbor $v$:
    - If `state[v] == Visiting`: **Return `true` (Cycle detected).**
    - If `state[v] == Unvisited`: Recurse `HasCycle(v)`. If it returns `true`, propagate `true`.
  - Post-order: Set `state[u] = Visited` (Black). Return `false`.

#### 3.6 Concrete Step-by-Step State Trace
Input: `numCourses = 4`, `prerequisites = [[1, 0], [2, 0], [3, 1], [3, 2]]` (Edges: $0 \to 1, 0 \to 2, 1 \to 3, 2 \to 3$).
- In-degrees: $0: 0, 1: 1, 2: 1, 3: 2$.
- Initial Queue with in-degree 0: `[0]`. `processedCount = 0`.
- **Iteration 1:** Dequeue `0`. `processedCount = 1`.
  - Decrement neighbors of 0:
    - $1$: in-degree becomes $0 \implies$ Enqueue `1`.
    - $2$: in-degree becomes $0 \implies$ Enqueue `2`.
  - Queue: `[1, 2]`.
- **Iteration 2:** Dequeue `1`. `processedCount = 2`.
  - Decrement neighbor $3$: in-degree becomes $2 - 1 = 1$.
  - Queue: `[2]`.
- **Iteration 3:** Dequeue `2`. `processedCount = 3`.
  - Decrement neighbor $3$: in-degree becomes $1 - 1 = 0 \implies$ Enqueue `3`.
  - Queue: `[3]`.
- **Iteration 4:** Dequeue `3`. `processedCount = 4`.
  - No neighbors. Queue empty.
- Loop terminates. `processedCount == 4 == numCourses` $\implies$ Return `true`.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **When to choose Approach 1 (Kahn's BFS):** Top recommendation for interviews. Directly answers "can we schedule" and seamlessly generalizes to Course Schedule II (generating the topological order). No risk of stack overflow.
- **When to choose Approach 2 (3-State DFS):** Exceptional when you need to detect cycles early on massive sparse graphs without building in-degree arrays. Stops immediately upon encountering the first back-edge.
- **Memory & Cache:** Kahn's allocates an in-degree array ($O(V)$) and a queue ($O(V)$). DFS uses a state array ($O(V)$) and the program call stack ($O(V)$).

#### 4.2 Step-by-Step Natural Progression Flow
1. **Setup & Boundaries:** Construct adjacency list. For Kahn's, compute in-degrees.
2. **Main Exploration Loop:** Enqueue in-degree 0 nodes (or loop $0 \dots V-1$ for DFS).
3. **Invariant Maintenance & Condition Gates:** Decrement in-degrees and enqueue on 0 (or check White/Gray/Black states).
4. **Resolution & Return:** Check `processedCount == numCourses` (or absence of back-edges).

#### 4.3 Alternative Approaches Analysis
- **Tarjan's Strongly Connected Components (SCC):** Finds cycles by identifying maximal strongly connected subgraphs with size $> 1$. Correct, but massive overkill ($2\times$ more lines of code) compared to Kahn's or 3-state DFS.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: Kahn's Algorithm (BFS) | Approach 2: 3-State DFS Coloring |
| :--- | :--- | :--- |
| **Time Complexity (All Cases)** | $O(V + E)$ | $O(V + E)$ |
| **Auxiliary Space** | $O(V + E)$ (graph + in-degree + queue) | $O(V + E)$ (graph + state array + stack) |
| **Cycle Detection Timing** | Discovered at end when queue empties early | Discovered instantaneously upon back-edge |
| **Generalizability to LC #210** | Immediate (record dequeue sequence) | Requires reversing post-order traversal |
| **Stack Overflow Risk** | Zero | Bounded by $V$ |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Course Schedule (#68)
// Selection Heuristic: 
//   - Approach 1 (Kahn's BFS): Preferred standard; natural topological peeling.
//   - Approach 2 (3-State DFS): Preferred when early cycle exit is critical.
// Invariant: Graph is a DAG iff all nodes reach in-degree 0 (Kahn's) or no back-edges exist (DFS).
// ============================================================================
```

#### Approach 1: Kahn's Algorithm (BFS In-Degree Peeling)
```csharp
public class Solution
{
    public bool CanFinish(int numCourses, int[][] prerequisites)
    {
        int[] inDegree = new int[numCourses];
        var adjList = new List<int>[numCourses];
        for (int i = 0; i < numCourses; i++) adjList[i] = new List<int>();

        // Build directed graph: prereq (edge[1]) -> course (edge[0])
        foreach (var edge in prerequisites)
        {
            int course = edge[0];
            int prereq = edge[1];
            adjList[prereq].Add(course);
            inDegree[course]++;
        }

        // Initialize frontier with all nodes possessing 0 incoming prerequisites
        var queue = new Queue<int>();
        for (int i = 0; i < numCourses; i++)
        {
            if (inDegree[i] == 0) queue.Enqueue(i);
        }

        int processedCourses = 0;

        while (queue.Count > 0)
        {
            int current = queue.Dequeue();
            processedCourses++;

            // Decrement in-degree for all outgoing neighbors
            foreach (int neighbor in adjList[current])
            {
                inDegree[neighbor]--;
                // Decision Gate: Neighbor has all prerequisites fulfilled
                if (inDegree[neighbor] == 0)
                {
                    queue.Enqueue(neighbor);
                }
            }
        }

        // Invariant: If processedCourses < numCourses, a cycle trapped the remaining nodes
        return processedCourses == numCourses;
    }
}
```

#### Approach 2: 3-State DFS Coloring (White / Gray / Black)
```csharp
public class SolutionDfs
{
    // Tri-color states representing node lifecycle during DFS
    private enum State 
    { 
        Unvisited = 0, // White: Unexplored
        Visiting = 1,  // Gray: On active recursion stack
        Visited = 2    // Black: Completely explored and verified cycle-free
    }

    public bool CanFinish(int numCourses, int[][] prerequisites)
    {
        var adjList = new List<int>[numCourses];
        for (int i = 0; i < numCourses; i++) adjList[i] = new List<int>();

        foreach (var edge in prerequisites)
        {
            adjList[edge[1]].Add(edge[0]);
        }

        var states = new State[numCourses];

        // Traverse each component to detect cycles in disconnected graphs
        for (int i = 0; i < numCourses; i++)
        {
            if (states[i] == State.Unvisited)
            {
                if (HasCycle(i, adjList, states))
                {
                    return false; // Cycle detected
                }
            }
        }

        return true;
    }

    private static bool HasCycle(int node, List<int>[] adjList, State[] states)
    {
        // Mark node as Gray (active in recursion stack)
        states[node] = State.Visiting;

        foreach (int neighbor in adjList[node])
        {
            // Decision Gate 1: Back-edge detected! Neighbor is an ancestor on the active call stack
            if (states[neighbor] == State.Visiting)
            {
                return true;
            }

            // Decision Gate 2: Unvisited neighbor; recursively search for cycles
            if (states[neighbor] == State.Unvisited)
            {
                if (HasCycle(neighbor, adjList, states))
                {
                    return true;
                }
            }
            // Note: If states[neighbor] == State.Visited (Black), it is safely cycle-free; skip.
        }

        // Mark node as Black (fully explored and safe) before popping call stack
        states[node] = State.Visited;
        return false;
    }
}
```

---

## 69. Course Schedule II (LeetCode #210)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | 🔥 High |
| **Pattern Tags** | `#graph` `#topological-sort` `#kahns-algorithm` `#ordering` `#dag` |
| **LeetCode Link** | [Course Schedule II](https://leetcode.com/problems/course-schedule-ii/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** There are `numCourses` courses labeled from $0$ to `numCourses - 1`. Given an array `prerequisites` where `prerequisites[i] = [a_i, b_i]` represents directed edge $b_i \to a_i$, return the ordering of courses you should take to finish all courses. If there are multiple valid answers, return any of them. If it is impossible to finish all courses (cycle exists), return an empty array.
- **Key Constraints:**
  - $1 \le \text{numCourses} \le 2000$.
  - $0 \le \text{prerequisites.Length} \le numCourses \times (numCourses - 1)$.
  - All prerequisite pairs are distinct.
- **Senior Edge Cases to Defend:**
  - Cycle present: Must return `new int[0]` (never return a partial array).
  - Empty prerequisites: Any permutation of $0 \dots numCourses - 1$ is valid; return `[0, 1, ..., numCourses - 1]`.
  - Disconnected subgraphs: Must schedule all independent trees into a single linear sequence.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Construct an explicit linear ordering of DAG vertices such that for every directed edge $u \to v$, $u$ appears before $v$. Solved by accumulating dequeued vertices in Kahn's algorithm or reversing post-order DFS.
- **Sample 1:**
  - **Input:** `numCourses = 4, prerequisites = [[1,0],[2,0],[3,1],[3,2]]`
  - **Output:** `[0, 2, 1, 3]` (or `[0, 1, 2, 3]`)

---

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine an assembly line scheduler. Certain tasks cannot begin until prerequisite components have been assembled. You have an output conveyor belt (`order[]`) of size $numCourses$ with a pointer `writeIndex = 0`. At any time, you examine your task list: any task whose dependencies have all been satisfied sits in your "Ready" crate (Queue). You pull a task from the crate, place it onto the conveyor belt at `order[writeIndex++]`, and notify all downstream tasks that this dependency is cleared. If you fill all $numCourses$ slots on the belt, the schedule is complete. If the crate runs empty before the belt is filled, a circular dependency locked the system, and you discard the belt (return empty array).

#### 3.2 The Naive Bottleneck & Redundant Computation
Generating all $N!$ permutations and testing each against $E$ edges requires $O(N! \cdot E)$ time. Repeatedly searching the graph for in-degree 0 nodes without an active queue takes $O(V^2 + E)$ time. Kahn's algorithm maintains the in-degree count dynamically, avoiding re-scans and achieving optimal linear $O(V + E)$ time.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Chronological Construction Invariant:** A vertex $v$ can only be enqueued after all its incoming edges have been removed. Consequently, every prerequisite of $v$ is written to `order[]` at an index strictly less than the index at which $v$ is written.
- **Cycle Detection Guarantee:** If the graph has a cycle, vertices in the cycle never attain in-degree 0. Thus `writeIndex` will strictly satisfy $\text{writeIndex} < \text{numCourses}$ when the queue becomes empty.

#### 3.4 Cursor Semantics & Invariant Partition Architecture
- `writeIndex`: Direct cursor pointing to the next available write slot in the pre-allocated `order` array.
- Invariant Partition Diagram:

```text
Topological Order Array [0 ... numCourses - 1]:
+------------------------------------+-----------------------------+
| order[0 ... writeIndex - 1]        | order[writeIndex ... N - 1] |
| Fully Scheduled Courses            | Unfilled / Unreachable      |
| (All prerequisites satisfied)      | (Awaiting clearance)        |
+------------------------------------+-----------------------------+
               ^
               | writeIndex cursor advances monotonically on each Dequeue
```

#### 3.5 State Transition Triggers & Decision Gates
- **Initial Setup:** Compute `inDegree` array and enqueue all nodes with `inDegree[i] == 0`.
- **Dequeue Step:**
  - `int curr = queue.Dequeue()`.
  - `order[writeIndex++] = curr`.
- **Neighbor Relaxation:**
  - For each `neighbor` in `adjList[curr]`:
    - `inDegree[neighbor]--`.
    - Gate: If `inDegree[neighbor] == 0`, `queue.Enqueue(neighbor)`.
- **Final Validation Gate:**
  - If `writeIndex == numCourses`, return `order`.
  - Else, return `Array.Empty<int>()`.

#### 3.6 Concrete Step-by-Step State Trace
Input: `numCourses = 4`, `prerequisites = [[1, 0], [2, 0], [3, 1], [3, 2]]`.
- Pre-allocated `order = new int[4]`, `writeIndex = 0`.
- `inDegree = [0: 0, 1: 1, 2: 1, 3: 2]`.
- Queue: `[0]`.
- **Step 1:** Dequeue `0`. `order[0] = 0`, `writeIndex = 1`.
  - Neighbor 1: inDegree becomes $0 \implies$ Enqueue 1.
  - Neighbor 2: inDegree becomes $0 \implies$ Enqueue 2.
  - Queue: `[1, 2]`.
- **Step 2:** Dequeue `1`. `order[1] = 1`, `writeIndex = 2`.
  - Neighbor 3: inDegree becomes $1$.
  - Queue: `[2]`.
- **Step 3:** Dequeue `2`. `order[2] = 2`, `writeIndex = 3`.
  - Neighbor 3: inDegree becomes $0 \implies$ Enqueue 3.
  - Queue: `[3]`.
- **Step 4:** Dequeue `3`. `order[3] = 3`, `writeIndex = 4`.
  - Queue empty.
- `writeIndex == 4 == numCourses` $\implies$ Return `[0, 1, 2, 3]`.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **When to choose Approach 1 (Kahn's BFS with Pre-allocated Array):** Universally preferred. Writes directly into a flat integer array `int[numCourses]`, achieving zero heap reallocation, zero reversal overhead, and maximum CPU cache efficiency.
- **When to choose Approach 2 (DFS Post-Order Reversal):** Useful when implementing functional topological sorts. Traverses graph using DFS, pushes nodes onto a stack upon return, and pops or reverses the collection.

#### 4.2 Step-by-Step Natural Progression Flow
1. **Setup & Boundaries:** Allocate `int[] order = new int[numCourses]`, `int writeIndex = 0`, and `adjList`.
2. **Main Exploration Loop:** Dequeue ready courses and write to `order[writeIndex++]`.
3. **Invariant Maintenance & Condition Gates:** Decrement neighbor in-degrees and enqueue newly freed courses.
4. **Resolution & Return:** If `writeIndex == numCourses`, return `order`; otherwise return `Array.Empty<int>()`.

#### 4.3 Alternative Approaches Analysis
- **DFS Post-Order with Reversal:** Use tri-color state array (0 = White, 1 = Gray, 2 = Black). On cycle detection (Gray node encountered), return empty array. Upon finishing DFS for node $u$, write $u$ into `order[--writeIndex]` (filling from back to front). This achieves $O(V + E)$ without needing an extra array reversal pass.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: Kahn's BFS (Pre-allocated Array) | Approach 2: DFS Post-Order Reversal |
| :--- | :--- | :--- |
| **Time Complexity (All Cases)** | $O(V + E)$ | $O(V + E)$ |
| **Auxiliary Space** | $O(V + E)$ | $O(V + E)$ |
| **Output Space** | $O(V)$ pre-allocated array | $O(V)$ array |
| **Memory Allocations** | Single `int[V]` array write | `int[V]` array + call stack |
| **Cache Locality** | Sequential array writes | Stack unwinding + reverse writes |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Course Schedule II (#69)
// Selection Heuristic: 
//   - Approach 1 (Kahn's BFS): Optimal single-pass forward array construction.
//   - Approach 2 (DFS Reverse): Classic topological sort via post-order traversal.
// Invariant: Direct array write at order[writeIndex++] ensures O(1) appending and 0 allocations.
// ============================================================================
```

#### Approach 1: Kahn's Algorithm (Pre-Allocated Array Buffer)
```csharp
public class Solution
{
    public int[] FindOrder(int numCourses, int[][] prerequisites)
    {
        int[] inDegree = new int[numCourses];
        var adjList = new List<int>[numCourses];
        for (int i = 0; i < numCourses; i++) adjList[i] = new List<int>();

        // Build directed adjacency list: prereq (edge[1]) -> course (edge[0])
        foreach (var edge in prerequisites)
        {
            int course = edge[0];
            int prereq = edge[1];
            adjList[prereq].Add(course);
            inDegree[course]++;
        }

        // Initialize queue with all courses that have no prerequisites
        var queue = new Queue<int>();
        for (int i = 0; i < numCourses; i++)
        {
            if (inDegree[i] == 0) queue.Enqueue(i);
        }

        // Pre-allocate exact output array; writeIndex acts as the insertion cursor
        int[] order = new int[numCourses];
        int writeIndex = 0;

        while (queue.Count > 0)
        {
            int current = queue.Dequeue();
            // Append course whose prerequisites are all satisfied
            order[writeIndex++] = current;

            foreach (int neighbor in adjList[current])
            {
                inDegree[neighbor]--;
                // Gate: Prerequisite barrier cleared for neighbor
                if (inDegree[neighbor] == 0)
                {
                    queue.Enqueue(neighbor);
                }
            }
        }

        // Invariant: If writeIndex == numCourses, a full topological order exists.
        // Otherwise, a cycle prevented at least one course from clearing prerequisites.
        return writeIndex == numCourses ? order : Array.Empty<int>();
    }
}
```

#### Approach 2: DFS Post-Order Traversal with Reverse Array Writing
```csharp
public class SolutionDfs
{
    private enum State { Unvisited = 0, Visiting = 1, Visited = 2 }

    public int[] FindOrder(int numCourses, int[][] prerequisites)
    {
        var adjList = new List<int>[numCourses];
        for (int i = 0; i < numCourses; i++) adjList[i] = new List<int>();

        foreach (var edge in prerequisites)
        {
            adjList[edge[1]].Add(edge[0]);
        }

        var states = new State[numCourses];
        int[] order = new int[numCourses];
        // Fill from back to front to achieve reverse post-order without an extra reverse pass
        int writeIndex = numCourses - 1;

        for (int i = 0; i < numCourses; i++)
        {
            if (states[i] == State.Unvisited)
            {
                if (!Dfs(i, adjList, states, order, ref writeIndex))
                {
                    return Array.Empty<int>(); // Cycle detected
                }
            }
        }

        return order;
    }

    private static bool Dfs(int node, List<int>[] adjList, State[] states, int[] order, ref int writeIndex)
    {
        states[node] = State.Visiting; // On active stack

        foreach (int neighbor in adjList[node])
        {
            // Back-edge detected
            if (states[neighbor] == State.Visiting) return false;

            if (states[neighbor] == State.Unvisited)
            {
                if (!Dfs(neighbor, adjList, states, order, ref writeIndex))
                {
                    return false;
                }
            }
        }

        states[node] = State.Visited;
        // Invariant: Post-order insertion: Place node once all downstream dependencies are resolved
        order[writeIndex--] = node;
        return true;
    }
}
```
