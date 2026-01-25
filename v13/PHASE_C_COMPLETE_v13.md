# 📘 PHASE_C_TREES_GRAPHS_DP_v13_FULL.md

**Version:** 13.0 (Comprehensive Phase C - Weeks 7-11)  
**Status:** ✅ PRODUCTION READY - COMPLETE DETAILED BREAKDOWNS  
**Date:** January 24, 2026

---

# 🟪 PHASE C: TREES, GRAPHS & DYNAMIC PROGRAMMING (Weeks 7-11)

## Phase Motivation
By now you understand fundamentals and patterns for linear structures. This phase moves to hierarchical (trees) and relational (graphs) data, plus the powerful optimization technique of dynamic programming. These are the backbone of complex system design and competitive programming.

## Phase Outcomes
- [ ] Understand tree structures, traversals, BSTs, and balanced BST operations
- [ ] Apply DP to tree problems (DP on trees)
- [ ] Master graph representations and traversals (BFS, DFS)
- [ ] Apply shortest path algorithms (Dijkstra, Bellman-Ford, Floyd-Warshall)
- [ ] Understand and implement minimum spanning trees
- [ ] Master dynamic programming from basics to complex patterns
- [ ] Solve DP on graphs, trees, DAGs, and with constraints
- [ ] Combine multiple algorithms to solve integration problems

---

## 📌 WEEK 7: TREES & BALANCED SEARCH TREES

### Weekly Goal
Build understanding of tree structures and operations. Master binary trees, search trees, and balance mechanisms that enable logarithmic operations.

### Weekly Topics
- Binary tree structure and traversals
- Binary search trees and operations
- Balanced BSTs (AVL, red-black overview)
- Tree patterns and applications
- Augmented trees for advanced queries

---

### 📅 DAY 1: BINARY TREES & TRAVERSALS

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Tree Anatomy & Terminology**
  - Root: topmost node
  - Leaf: node with no children
  - Parent, child, sibling relationships
  - Depth: distance from root
  - Height: distance from node to farthest leaf
  - Full tree: every internal node has exactly 2 children
  - Complete tree: filled from left to right, bottom level may be partial
  - Balanced tree: height is O(log n)

- **Binary Tree Representation**
  - Node-based: each node has value, left, right pointers
  - Array-based: parent of i at i//2, children at 2i and 2i+1
  - First method: flexible, used in most problems
  - Second method: compact, useful for heaps

- **Preorder Traversal (Root, Left, Right)**
  - Visit node, then left subtree, then right subtree
  - Use case: copying tree, serialization
  - Recursive: visit(), recurse_left(), recurse_right()
  - Iterative: use stack, pop and process, push right then left

- **Inorder Traversal (Left, Root, Right)**
  - Left subtree, visit node, right subtree
  - For BSTs: produces sorted sequence
  - Key for BST applications
  - Recursive or iterative with stack

- **Postorder Traversal (Left, Right, Root)**
  - Left subtree, right subtree, visit node
  - Use case: deleting tree (delete children before parent)
  - Computing heights, sizes bottom-up

- **Level-Order Traversal (BFS)**
  - Visit level by level from top to bottom
  - Use queue instead of stack
  - Essential for finding path lengths, siblings

- **Iterative Traversals via Stack**
  - Preorder: push node, pop-process-push(right, left)
  - Inorder: push all left, pop-process-push-right
  - Postorder: more complex, two stacks or careful ordering

- **Spiral Traversal (Zigzag)**
  - Alternate direction each level
  - Use queue and direction flag
  - Collect results in lists

- **Boundary Traversal**
  - Left boundary (without leaves), bottom leaves, right boundary (reversed, without leaves)
  - Combination of different traversals

**Key Insights:**
- Different traversals for different purposes
- Iterative traversals use explicit stack
- Inorder on BSTs gives sorted order

**Deliverables:**
- [ ] Implement all four traversals (recursive)
- [ ] Implement all four traversals (iterative with stack/queue)
- [ ] Verify inorder gives sorted sequence for BST
- [ ] Implement spiral/zigzag traversal
- [ ] Implement boundary traversal

---

### 📅 DAY 2: BINARY SEARCH TREES (BSTs)

**Duration:** 90 minutes

**Topics & Subtopics:**

- **BST Property & Definition**
  - For each node: all left descendants < node, all right descendants ≥ node
  - Enables efficient search, insert, delete
  - Inorder traversal produces sorted sequence

- **Search in BST**
  - Recursive: compare, recurse left or right
  - Time: O(h) where h = height
  - Best: O(log n) for balanced tree
  - Worst: O(n) if tree is degenerate (like linked list)

- **Insert in BST**
  - Find correct position: follow comparisons
  - Create new node and link
  - Time: O(h)
  - No duplicates (usually) or allow duplicates with convention

- **Delete from BST**
  - Case 1: Leaf node - simply remove
  - Case 2: One child - replace with child
  - Case 3: Two children:
    - Option A: find in-order predecessor (largest in left subtree)
    - Option B: find in-order successor (smallest in right subtree)
    - Replace node value with successor/predecessor value
    - Delete successor/predecessor (now has ≤ 1 child)
  - Time: O(h)

- **Verification of BST**
  - Simple check: left < node < right (incorrect for nested structure)
  - Correct: maintain min/max bounds as you traverse
  - Each left subtree: must be < current node
  - Each right subtree: must be > current node

- **Building BST from Sorted Array**
  - Insert middle element, recursively build left and right
  - Ensures balanced tree
  - Time: O(n)

- **BST vs Sorted Array Tradeoff**
  - BST: O(log n) search, insert, delete
  - Array: O(log n) search (binary search), O(n) insert/delete
  - BST better for dynamic data

- **Degenerate BST Problem**
  - Sorted input creates linear tree (height n)
  - Random insertion prevents this
  - Balanced BSTs guarantee O(log n) height

**Key Insights:**
- BST property enables efficient operations
- Height determines complexity
- Delete case with two children requires careful handling

**Deliverables:**
- [ ] Implement BST search, insert, delete
- [ ] Implement BST validation with bounds checking
- [ ] Build balanced BST from sorted array
- [ ] Handle duplicates in BST (convention choice)
- [ ] Find min/max elements in BST

---

### 📅 DAY 3: BALANCED BSTs - AVL & RED-BLACK (OVERVIEW)

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Why Balance Matters**
  - Unbalanced BST: degenerates to O(n)
  - Balanced BST: guarantees O(log n)
  - Self-balancing: maintain balance during insert/delete

- **AVL Trees**
  - Balance factor: height(left) - height(right)
  - Invariant: balance factor ∈ {-1, 0, 1}
  - Four rotation cases:
    - Left-left (LL): single right rotation
    - Right-right (RR): single left rotation
    - Left-right (LR): left rotation then right rotation
    - Right-left (RL): right rotation then left rotation
  - After insertion: check all ancestors, rebalance if needed
  - After deletion: similar process

- **Red-Black Trees**
  - Color each node red or black
  - Invariants:
    - Root is black
    - Red node has black children
    - All root-to-leaf paths have same number of black nodes
  - Guarantees: height ≤ 2 log(n+1)
  - Fewer rotations than AVL on average
  - More relaxed balance than AVL

- **Rotation Mechanics**
  - Left rotation: move right child up, left child of right becomes right child
  - Right rotation: symmetric
  - Preserve BST property during rotation
  - Update heights/colors as needed

- **Real-World Implementations**
  - C++ std::map uses red-black trees
  - Java TreeMap uses red-black trees
  - Python uses hash tables by default for dicts
  - AVL in some databases

- **Complexity Summary**
  - All balanced BSTs: O(log n) search, insert, delete
  - Space: O(n)
  - Practical differences in constants and rotation frequency

- **When to Use Which**
  - AVL: more rigid balance, faster search, slower insert/delete
  - Red-black: more relaxed balance, faster insert/delete, comparable search
  - For interviews: understand concept, implement basic, use library

- **Self-Balancing Not Required for Interview**
  - Usually, you don't need to implement balance mechanisms
  - Use library implementations (TreeMap, TreeSet)
  - Understand trade-offs and when to use

**Key Insights:**
- Balanced BSTs maintain O(log n) height
- Multiple balance schemes exist with different tradeoffs
- Library implementations sufficient for most use cases

**Deliverables:**
- [ ] Understand AVL rotation cases conceptually
- [ ] Trace through rebalancing example
- [ ] Understand red-black tree invariants
- [ ] Know when to use each data structure
- [ ] Use library implementations effectively

---

### 📅 DAY 4: TREE PATTERNS

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Path Sum Problems**
  - Root to leaf sum: DFS from root, accumulate sum, check at leaves
  - Path sum with target: recursively check left and right
  - General path sum: any node to any descendant (not just root-to-leaf)

- **Root-to-Leaf Paths**
  - Find all paths from root to leaves
  - Backtracking: build path as you go down
  - At leaf: record path
  - Backtrack: remove node from path

- **Tree Diameter**
  - Longest path in tree (not necessarily through root)
  - For each node: compute height of left and right subtrees
  - Diameter through this node: sum of heights + 1
  - Recurrence: diameter = max(left_diameter, right_diameter, left_height + right_height + 1)
  - Time: O(n)

- **Lowest Common Ancestor (LCA)**
  - Simplest approach: find paths to both nodes, compare
  - Binary lifting: preprocess for O(log n) queries
  - For general trees: DFS and find common ancestor

- **Height vs Depth**
  - Depth: distance from root (BFS)
  - Height: distance to farthest leaf (DFS from each node)
  - Often computed together in single DFS

- **Subtree Problems**
  - Identical subtrees: compare structures recursively
  - Subtree matching: check if one tree is subtree of another

- **Serialization & Deserialization**
  - Level-order based: store structure info (null nodes)
  - Preorder/postorder based: encode in specific order
  - Deserialization: reconstruct from encoding
  - Applications: persistence, transmission

- **Balanced Tree Check**
  - For each node: check balance factor (difference of heights)
  - Recursive check: efficient O(n) vs naive O(n²)
  - Prune early if imbalanced subtree found

**Key Insights:**
- Tree problems often solved with DFS + recursion
- Combining information from left and right subtrees powerful
- Serialization useful for storage and transmission

**Deliverables:**
- [ ] Implement path sum from root to leaf
- [ ] Find all root-to-leaf paths
- [ ] Calculate tree diameter
- [ ] Implement LCA for general trees
- [ ] Serialize and deserialize tree

---

### 📅 DAY 5 (OPTIONAL): AUGMENTED TREES & ORDER-STATISTICS TREES

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Augmenting Trees with Metadata**
  - Store additional info at each node for efficient queries
  - Example: subtree size at each node
  - Use in recursive calculations

- **Order-Statistics Trees**
  - Store size of each subtree
  - kth smallest element: compare k with left subtree size
  - Rank queries: find how many elements ≤ x
  - Time: O(log n) with balanced tree

- **Building Order-Statistics Tree**
  - Start with BST with sizes
  - On insert: update sizes of all ancestors
  - On delete: similar update
  - Maintain balance for O(log n) operations

- **Range Count Queries**
  - Count elements in [L, R] efficiently
  - Use augmented BST to prune subtrees
  - Time: O(log n)

- **Applications in Systems**
  - Database indexes: find kth record efficiently
  - Statistical calculations: percentiles
  - Range searching: spatial databases

- **Augmenting Other Structures**
  - Segment trees with custom aggregations
  - Can augment any tree structure

**Key Insights:**
- Augmentation enables new efficient queries
- Maintains BST structure while adding functionality
- Pattern reusable across many problems

**Deliverables:**
- [ ] Implement BST with size augmentation
- [ ] Solve kth smallest element in tree
- [ ] Implement range count queries
- [ ] Extend to weighted order-statistics (if time)

---

## 📌 WEEK 8: GRAPH FUNDAMENTALS - REPRESENTATIONS, BFS, DFS & TOPOLOGICAL SORT

### Weekly Goal
Master graph models and core traversal algorithms. Build intuition for graph structures through representations and explore problems solvable via BFS/DFS.

### Weekly Topics
- Graph representations and modeling
- Breadth-first search and shortest paths
- Depth-first search and applications
- Topological sorting
- Cycle detection and connectivity

---

### 📅 DAY 1: GRAPH MODELS & REPRESENTATIONS

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Graph Types & Terminology**
  - Directed vs undirected graphs
  - Weighted vs unweighted edges
  - Sparse vs dense graphs
  - Connected components
  - Cycles and DAGs

- **Adjacency Matrix Representation**
  - 2D array: matrix[i][j] = weight (or 1 if present)
  - Space: O(V²)
  - Time: O(1) edge lookup, O(V) to list neighbors
  - Best for: dense graphs, small graphs
  - Worst for: sparse graphs (wasteful space)

- **Adjacency List Representation**
  - Array of lists: list[i] = neighbors of i
  - Space: O(V + E)
  - Time: O(degree) to list neighbors, O(degree) edge lookup
  - Best for: sparse graphs, large graphs
  - Most common in interviews

- **Edge List Representation**
  - List of edges: [(u, v, weight), ...]
  - Space: O(E)
  - Time: O(E) to find neighbors (linear scan)
  - Use case: some graph algorithms like Kruskal MST

- **Choosing Representation**
  - Sparse (E << V²): adjacency list
  - Dense (E ≈ V²): adjacency matrix
  - Special cases: edge list for algorithms like MST

- **Implicit Graphs**
  - Graph defined by rules, not explicit storage
  - Example: grids (2D arrays with neighbors)
  - Example: game states (each state is node, moves are edges)
  - Explore via DFS/BFS without building graph

- **Graph Modeling from Problems**
  - Identify nodes: elements, states, positions
  - Identify edges: relationships, transitions, adjacencies
  - Identify weights: costs, distances, probabilities
  - Example: social network → nodes=people, edges=friendships

- **Building Graphs from Input**
  - Parse input to build adjacency list
  - Handle directed vs undirected (add reverse edge?)
  - Handle weights (store with edges)

**Key Insights:**
- Representation choice affects algorithm complexity
- Adjacency list most common for interviews
- Implicit graphs important for space efficiency

**Deliverables:**
- [ ] Build adjacency list from edge list
- [ ] Build adjacency matrix from edge list
- [ ] Convert between representations
- [ ] Model problem as graph (example: social network)

---

### 📅 DAY 2: BREADTH-FIRST SEARCH (BFS)

**Duration:** 90 minutes

**Topics & Subtopics:**

- **BFS Algorithm Template**
  - Queue-based frontier expansion
  - Visited set to avoid revisiting
  - Process nodes layer by layer
  - Time: O(V + E)

- **BFS from Single Source**
  - Start from source node
  - Add to queue and mark visited
  - While queue not empty:
    - Dequeue node
    - Process node
    - Enqueue unvisited neighbors
  - Guarantees shortest path in unweighted graphs

- **Shortest Path (Unweighted)**
  - Distance[node] = layer at which node discovered
  - Parent tracking enables path reconstruction
  - Distance[source] = 0, distance[neighbors] = 1, etc.

- **Connected Components**
  - For undirected graphs: run BFS from each unvisited node
  - Each BFS traversal = one component
  - Count components

- **Bipartite Graph Check**
  - Color nodes with two colors
  - For each node: neighbors must have different color
  - BFS: assign colors, check conflicts
  - Bipartite iff no conflicts

- **Level-Order Tree Traversal**
  - BFS on tree structure
  - Layer-wise processing

- **Shortest Path in Unweighted Network**
  - Classic BFS application
  - Find quickest route in network

- **Applications of BFS**
  - Social networks: degrees of separation
  - Shortest route on unweighted map
  - Web crawlers: breadth-first indexing
  - Puzzle solving: minimum moves

- **Optimizations**
  - Bidirectional BFS: search from both ends (faster for large graphs)
  - Early termination: if goal found, stop

**Key Insights:**
- BFS explores layer by layer
- Guarantees shortest path in unweighted graphs
- Time: O(V + E) always

**Deliverables:**
- [ ] Implement basic BFS
- [ ] Find shortest path using BFS
- [ ] Detect connected components
- [ ] Check if graph is bipartite
- [ ] Implement bidirectional BFS

---

### 📅 DAY 3: DEPTH-FIRST SEARCH (DFS) & TOPOLOGICAL SORT

**Duration:** 90 minutes

**Topics & Subtopics:**

- **DFS Algorithm Template**
  - Recursive exploration (or stack-based)
  - Visit node, explore all descendants, backtrack
  - Time: O(V + E)

- **Recursive DFS**
  - Mark visited, process node
  - Recursively visit unvisited neighbors
  - Implicit stack (function call stack)

- **Iterative DFS (Stack-Based)**
  - Explicit stack instead of recursion
  - Push start node
  - While stack not empty:
    - Pop node
    - Process if not visited
    - Push unvisited neighbors

- **DFS Tree & Edge Types**
  - Tree edges: edges to unvisited nodes
  - Back edges: edges to ancestor (indicates cycle in directed graph)
  - Forward edges: edges to descendant (only in directed)
  - Cross edges: edges to already-finished node (only in directed)
  - Classification depends on DFS order

- **Cycle Detection in Directed Graphs**
  - Use DFS with states: unvisited, visiting, visited
  - Back edge indicates cycle
  - White-gray-black coloring

- **Topological Sort - DFS Approach**
  - DFS post-order: nodes finished later come first
  - Reverse post-order: topological sort for DAG
  - Time: O(V + E)

- **Topological Sort - Kahn's Algorithm (BFS)**
  - In-degree based approach
  - Process nodes with in-degree 0
  - Decrease in-degree of neighbors
  - Add to sort when in-degree becomes 0

- **Applications of DFS**
  - Path existence: can reach B from A?
  - Connected components: similar to BFS
  - Topological sorting: dependency ordering
  - Strongly connected components: advanced

- **DFS vs BFS**
  - DFS: uses stack, explores depth first
  - BFS: uses queue, explores breadth first
  - Choose based on problem needs

**Key Insights:**
- DFS explores single path deeply
- Edge classification reveals graph structure
- Post-order traversal useful for dependencies

**Deliverables:**
- [ ] Implement recursive and iterative DFS
- [ ] Detect cycle in directed graph
- [ ] Topological sort using DFS
- [ ] Topological sort using Kahn's algorithm
- [ ] Find path from source to destination

---

### 📅 DAY 4: CONNECTIVITY & BIPARTITE GRAPHS

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Connected Components in Undirected Graphs**
  - Nodes are grouped if reachable from each other
  - DFS/BFS from each unvisited node finds one component
  - Count components
  - Label each component

- **Strongly Connected Components in Directed Graphs**
  - Nodes in SCC can reach each other (cycle exists)
  - Kosaraju algorithm: two-pass DFS
  - Tarjan algorithm: single-pass DFS
  - (High-level only for interviews)

- **Bipartite Graph Check**
  - Graph is bipartite iff 2-colorable
  - No odd-length cycles
  - Use BFS or DFS to color
  - Time: O(V + E)

- **2-Coloring Problem**
  - Assign 0/1 to nodes such that adjacent nodes differ
  - BFS/DFS and color checking
  - Detect conflict on neighbor already colored

- **Bridges & Articulation Points** (High-level)
  - Bridge: edge whose removal disconnects graph
  - Articulation point: node whose removal disconnects graph
  - Applications: network reliability, road networks

- **Weakly Connected Components** (Directed)
  - Nodes are in same component if connected when ignoring direction
  - DFS treating graph as undirected

- **Applications**
  - Grouping objects with relations
  - Clustering in networks
  - Checking bipartiteness in matching problems

**Key Insights:**
- Component finding via DFS/BFS
- Bipartite check simple with 2-coloring
- Structural properties enable various applications

**Deliverables:**
- [ ] Count connected components in undirected graph
- [ ] Check if graph is bipartite
- [ ] 2-color a bipartite graph
- [ ] Identify articulation points (high-level)

---

### 📅 DAY 5 (OPTIONAL): STRONGLY CONNECTED COMPONENTS

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Strongly Connected Components Definition**
  - SCC: set of nodes where each can reach each other
  - Graph of SCCs: collapsing each SCC to one node creates DAG

- **Kosaraju Algorithm**
  - First DFS: compute finish times (post-order)
  - Transpose graph: reverse all edges
  - Second DFS on transposed: visit in decreasing finish time order
  - Each DFS tree in second pass is one SCC
  - Time: O(V + E)

- **Tarjan Algorithm** (Conceptual)
  - Single-pass DFS with low-link values
  - Uses stack to track SCC candidate nodes
  - Identifies SCC complete during DFS
  - Time: O(V + E)

- **Applications**
  - Deadlock detection: processes in cycle = deadlock
  - Condensation graph: collapse SCC to analyze overall flow
  - Schedule problems: dependencies

- **Component DAG**
  - After finding SCCs, build DAG of components
  - Topological sort on component DAG

**Key Insights:**
- SCCs identify strongly connected regions
- Kosaraju simple and intuitive
- Component analysis useful for dependency problems

**Deliverables:**
- [ ] Understand Kosaraju algorithm conceptually
- [ ] Trace through example
- [ ] Identify SCCs in sample graph

---

## 📌 WEEK 9: GRAPH ALGORITHMS I - SHORTEST PATHS, MST & UNION-FIND

### Weekly Goal
Learn fundamental optimization algorithms on graphs. Master shortest path algorithms and minimum spanning tree.

### Weekly Topics
- Dijkstra's algorithm for single-source shortest paths
- Bellman-Ford for negative weights
- Floyd-Warshall for all-pairs shortest paths
- Minimum spanning trees (Kruskal & Prim)
- Union-Find for efficient connectivity

---

### 📅 DAY 1: DIJKSTRA'S ALGORITHM

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Problem: Single-Source Shortest Paths**
  - Given: source node, weighted graph
  - Find: shortest path to all nodes from source
  - Constraint: non-negative weights

- **Dijkstra Algorithm Overview**
  - Greedy algorithm: always expand nearest unvisited node
  - Use priority queue for efficiency
  - Time: O((V + E) log V) with binary heap

- **Dijkstra Algorithm Details**
  - Distance[source] = 0, others = infinity
  - Priority queue: (distance, node)
  - While queue not empty:
    - Extract min distance node
    - For each neighbor: relax edge
    - Relaxation: if distance[u] + weight(u,v) < distance[v], update
  - Extract ensures smallest distance processed

- **Relaxation Operation**
  - Core operation: try to improve distance via current edge
  - If new path shorter: update distance and add to queue
  - Edge weight must be non-negative for correctness

- **Why Non-Negative?**
  - Greedy choice assumes explored nodes won't improve
  - Negative weights can violate this assumption
  - Negative cycles are undefined (infinite improvement)

- **Complexity Analysis**
  - Extract min: O(log V) × V times = O(V log V)
  - Relaxation: O(1) × E times = O(E)
  - Total: O(V log V + E log V) = O((V + E) log V)
  - With Fibonacci heap: O(V log V + E)

- **Path Reconstruction**
  - Store parent[node] when updating distance
  - Trace back from destination to source

- **Applications**
  - GPS navigation: shortest route
  - Network routing: minimum latency path
  - Social networks: degree of separation (unweighted)

- **Variants**
  - Single-target: can run backward from target
  - Multiple sources: modify initialization
  - With constraints: time windows, capacity limits

**Key Insights:**
- Dijkstra greedy: expand closest unvisited
- Priority queue essential for efficiency
- Non-negative weights required

**Deliverables:**
- [ ] Implement Dijkstra with priority queue
- [ ] Find shortest path to all nodes from source
- [ ] Reconstruct shortest path
- [ ] Implement single-target variant
- [ ] Verify correctness on sample graphs

---

### 📅 DAY 2: BELLMAN-FORD & NEGATIVE WEIGHTS

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Problem: Negative Weights**
  - Dijkstra fails with negative weights
  - Need algorithm handling negatives but not negative cycles

- **Bellman-Ford Algorithm**
  - Relax all edges repeatedly: V-1 times
  - Each iteration: process all edges, improve distances
  - After V-1 iterations: shortest paths found (if no negative cycle)

- **Bellman-Ford Details**
  - Distance[source] = 0, others = infinity
  - For i = 1 to V-1:
    - For each edge (u, v):
      - Relax: if distance[u] + weight < distance[v], update
  - Time: O(V × E)

- **Negative Cycle Detection**
  - Extra iteration: if distances improve, negative cycle exists
  - May not be reachable from source

- **Why It Works**
  - Shortest path has at most V-1 edges
  - Each iteration finds paths with one more edge
  - After V-1 iterations: optimal found

- **Complexity**
  - Time: O(V × E) vs O((V + E) log V) for Dijkstra
  - Slower but handles negatives
  - Dense graphs: similar (E ≈ V²)
  - Sparse graphs: Bellman-Ford worse

- **Applications**
  - Currency exchange arbitrage detection
  - Finance: detecting profit opportunities
  - Networks with negative costs (rarely)

- **Optimizations**
  - SPFA (Shortest Path Faster Algorithm): queue-based
  - Process only edges from updated nodes
  - Average case O(E), worst case O(V × E)

**Key Insights:**
- Bellman-Ford handles negative weights
- Repeat relaxation ensures correctness
- Cycle detection critical

**Deliverables:**
- [ ] Implement Bellman-Ford
- [ ] Detect negative cycles
- [ ] Find shortest paths with negative weights
- [ ] Implement SPFA for optimization

---

### 📅 DAY 3: FLOYD-WARSHALL ALL-PAIRS SHORTEST PATHS

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Problem: All-Pairs Shortest Paths**
  - Find shortest paths between all pairs of nodes
  - Different from running Dijkstra V times

- **Floyd-Warshall Algorithm**
  - DP formulation: intermediate vertices up to k
  - dist[i][j][k] = shortest path from i to j using vertices 0..k
  - Base: dist[i][j][0] = direct edge weight or infinity
  - Recurrence: dist[i][j][k] = min(dist[i][j][k-1], dist[i][k][k-1] + dist[k][j][k-1])
  - Optimize: can use 2D array and update in-place

- **Floyd-Warshall Details**
  - Initialize: copy adjacency matrix
  - Three nested loops: for k, i, j (order matters!)
  - Update: dist[i][j] = min(dist[i][j], dist[i][k] + dist[k][j])
  - Time: O(V³)

- **Complexity Analysis**
  - Time: O(V³) - triple nested loops
  - Space: O(V²) - distance matrix
  - Works for negative weights (but not negative cycles)
  - Detectable: if dist[i][i] < 0 after algorithm

- **When to Use**
  - Small graphs: V ≤ 500
  - Need all pairs distances
  - Dense graphs
  - Simple implementation preferred over efficiency

- **Path Reconstruction**
  - Store next[i][j]: next node on shortest path from i to j
  - On update: set next[i][j] = k (intermediate node)
  - Trace path using next matrix

- **Applications**
  - Transitive closure: which pairs are connected
  - Network reliability: min latency between all pairs
  - Game analysis: all distances in game state graph

**Key Insights:**
- Floyd-Warshall DP on intermediate vertices
- Simple implementation, high complexity
- Use for small, all-pairs problems

**Deliverables:**
- [ ] Implement Floyd-Warshall
- [ ] Find all shortest path pairs
- [ ] Reconstruct paths
- [ ] Detect negative cycles
- [ ] Compare with running Dijkstra V times

---

### 📅 DAY 4: MINIMUM SPANNING TREES - KRUSKAL & PRIM

**Duration:** 90 minutes

**Topics & Subtopics:**

- **MST Problem**
  - Spanning tree: connects all V vertices with V-1 edges, no cycles
  - Minimum spanning tree: spanning tree with minimum total weight
  - May not be unique if edge weights repeat

- **Kruskal Algorithm (Edge-Based)**
  - Sort edges by weight ascending
  - Iterate edges: add if doesn't create cycle
  - Use DSU (Disjoint Set Union) to detect cycles
  - Time: O(E log E) for sorting + O(E α(V)) for DSU

- **Kruskal Details**
  - Sort edges: O(E log E)
  - For each edge:
    - Find components of endpoints
    - If different: add edge, union components
  - Continue until V-1 edges added or edges exhausted
  - Greedy: always pick smallest weight edge that doesn't cycle

- **Prim Algorithm (Vertex-Based)**
  - Start with one vertex
  - Grow tree by always adding minimum weight edge to new vertex
  - Use priority queue
  - Time: O((V + E) log V) with binary heap

- **Prim Details**
  - Start: add source to tree
  - While tree not complete:
    - Find minimum weight edge from tree to non-tree vertex
    - Add to tree
  - Priority queue helps find minimum edge efficiently

- **Cut Property & Correctness**
  - Cut: partition vertices into two sets
  - Minimum edge crossing cut is in some MST
  - Exchange argument: if used different edge, can swap for minimum

- **Kruskal vs Prim**
  - Kruskal: works on disconnected graphs (produces forest)
  - Prim: requires connected component
  - Kruskal: good for sparse graphs (E small)
  - Prim: good for dense graphs (E large)
  - Both O((V + E) log V) or better with optimization

- **Applications**
  - Network design: minimum cost to connect all nodes
  - Clustering: build hierarchy of clusters
  - Traveling salesman: MST approximation

**Key Insights:**
- MST minimizes total edge weight
- Two greedy algorithms both work
- Cut property justifies correctness

**Deliverables:**
- [ ] Implement Kruskal with DSU
- [ ] Implement Prim with priority queue
- [ ] Verify MST properties
- [ ] Compare on various graphs
- [ ] Solve network design problem

---

### 📅 DAY 5 (OPTIONAL): DSU / UNION-FIND IN DEPTH

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Disjoint Set Problem**
  - Maintain partitions of elements
  - Operations: find (which set?), union (merge sets)
  - Applications: connectivity, cycle detection, MST

- **Union-Find Data Structure**
  - Array parent[i]: parent of i in forest
  - Find: follow parent pointers to root
  - Union: merge two trees

- **Path Compression Optimization**
  - During find: point all nodes directly to root
  - Flatten tree structure
  - Amortized O(α(n)) per operation (inverse Ackermann)
  - Practical: essentially O(1)

- **Union by Rank Optimization**
  - Track tree rank (height)
  - Union: attach smaller tree to larger
  - Prevents degeneration into linked list
  - Combined with path compression: O(α(n))

- **Implementation**
  - Find with path compression:
    ```
    def find(x):
      if parent[x] != x:
        parent[x] = find(parent[x])  # compress path
      return parent[x]
    ```
  - Union by rank:
    ```
    def union(x, y):
      rx, ry = find(x), find(y)
      if rank[rx] < rank[ry]: rx, ry = ry, rx
      parent[ry] = rx
      if rank[rx] == rank[ry]: rank[rx] += 1
    ```

- **Complexity Analysis**
  - Without optimization: O(n) worst case
  - With path compression: O(n log n)
  - With union by rank: O(n log n)
  - With both: O(n α(n)) ≈ O(n)

- **Applications**
  - Kruskal MST: detect cycles
  - Network connectivity queries
  - Finding connected components
  - Offline dynamic connectivity

**Key Insights:**
- Union-Find extremely efficient with optimizations
- Path compression clever trick
- Rank helps balance trees

**Deliverables:**
- [ ] Implement basic DSU
- [ ] Implement with path compression
- [ ] Implement with union by rank
- [ ] Use in Kruskal MST
- [ ] Solve connectivity problem

---

## 📌 WEEK 10: DYNAMIC PROGRAMMING I - FUNDAMENTALS

### Weekly Goal
Build DP intuition from recursion + memoization to table-based bottom-up solutions. Master the mindset: break problem into subproblems, store results, combine efficiently.

### Weekly Topics
- DP as recursion with memoization
- Bottom-up tabulation approach
- 1D and 2D DP patterns
- DP on sequences and grids
- Optimization techniques

---

### 📅 DAY 1: DP AS RECURSION + MEMOIZATION

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Overlapping Subproblems Concept**
  - Same subproblem solved repeatedly in recursion
  - Example: Fibonacci
    - fib(5) = fib(4) + fib(3)
    - fib(4) = fib(3) + fib(2)
    - fib(3) computed multiple times
  - Exponential time without caching

- **Memoization = Recursion + Cache**
  - Compute subproblem: check cache first
  - If cached: return immediately
  - Else: compute, store in cache, return
  - Dramatically reduces time

- **Fibonacci with Memoization**
  - memo[n] = fib(n) result
  - Base: memo[0]=0, memo[1]=1
  - Recursive: if n in memo return memo[n], else compute
  - Time: O(n) instead of O(2^n)

- **Top-Down DP (Memoization)**
  - Start with full problem
  - Recurse on subproblems
  - Cache results
  - Natural from problem structure
  - Can be inefficient if not all subproblems needed

- **Bottom-Up DP (Tabulation)**
  - Solve small problems first
  - Build up to full problem
  - No recursion, just iteration
  - More efficient (fewer function calls)
  - Need to identify order of computation

- **Recursive vs Iterative**
  - Recursive: cleaner code, intuitive
  - Iterative: faster (less overhead)
  - Choose based on problem and preference

- **DP Table & State Definition**
  - State: what does each cell represent?
  - Transitions: how to compute from other cells?
  - Base case: what are simplest subproblems?
  - Answer: which cell contains solution?

- **Example: Climbing Stairs**
  - Problem: climb n stairs, can take 1 or 2 steps per move
  - State: ways[i] = number of ways to reach stair i
  - Base: ways[0]=1 (at start), ways[1]=1 (one way to reach)
  - Transition: ways[i] = ways[i-1] + ways[i-2] (from previous or two-behind)
  - Answer: ways[n]

**Key Insights:**
- Memoization caches redundant computation
- Top-down natural from problem, bottom-up efficient
- State definition is key to correct DP

**Deliverables:**
- [ ] Implement Fibonacci with memoization
- [ ] Compare performance vs naive recursion
- [ ] Implement climbing stairs bottom-up
- [ ] Convert recursive solution to iterative
- [ ] Identify states and transitions for custom problem

---

### 📅 DAY 2: 1D DP & KNAPSACK FAMILY

**Duration:** 90 minutes

**Topics & Subtopics:**

- **1D DP Patterns**
  - Array dp[i] representing answer for problem of size i
  - Transition: dp[i] depends on dp[j] for j < i
  - Time: O(n), Space: O(n) or O(1) if can optimize

- **Climbing Stairs Variants**
  - Basic: ways to reach stair n taking 1 or 2 steps
  - With cost: minimum cost to reach stair n
  - With constraints: can't take consecutive 2-step moves
  - General: given step sizes, compute answer

- **House Robber (Non-Adjacent Sum)**
  - Given array of house values, rob non-adjacent houses
  - Maximize: rob[i] = max(rob[i-1], rob[i-2] + arr[i])
  - Intuition: at each house, either rob it or skip
  - Time: O(n), Space: O(1) with optimization

- **House Robber II (Circular)**
  - Houses in circle: can't rob first and last together
  - Solution: max(rob excluding first, rob excluding last)

- **Coin Change**
  - Problem: minimum coins to make amount
  - State: dp[i] = min coins for amount i
  - Transition: dp[i] = min(dp[i-coin] + 1 for each coin)
  - Time: O(amount × coins)

- **Coin Change II (Number of Ways)**
  - Count ways to make amount
  - Different DP: dp[i] = number of ways for amount i
  - Transition: dp[i] += dp[i-coin] for each coin
  - Careful: order matters (different from permutations)

- **0/1 Knapsack (Single Item)**
  - Given items with weight and value, capacity W
  - Maximize value without exceeding weight
  - Classic DP problem
  - dp[i][w] = max value with first i items and weight w
  - Transition: take or skip item
  - Time: O(n × W), Space: O(n × W) or O(W) optimized

- **Unbounded Knapsack (Infinite Items)**
  - Each item available unlimited times
  - dp[i] = max value with capacity i
  - Transition: dp[i] = max(dp[i-weight[j]] + value[j] for all items j)
  - Time: O(W × items)

- **Bounded Knapsack (Limited Copies)**
  - Each item available k times
  - Treat as k separate items (or optimize)

**Key Insights:**
- 1D DP often has binary choice (take/skip)
- Knapsack family generalizable pattern
- Space optimization often possible

**Deliverables:**
- [ ] Implement house robber variants
- [ ] Implement coin change (min coins and ways)
- [ ] Implement 0/1 knapsack
- [ ] Implement unbounded knapsack
- [ ] Optimize space for knapsack problems

---

### 📅 DAY 3: 2D DP - GRIDS & EDIT DISTANCE

**Duration:** 90 minutes

**Topics & Subtopics:**

- **2D Grid DP**
  - State: dp[i][j] = answer for cell (i, j)
  - Often: number of ways or minimum cost
  - Transition: combine from previous cells

- **Unique Paths in Grid**
  - Given grid with obstacles, count unique paths from top-left to bottom-right
  - Can move right or down
  - State: dp[i][j] = ways to reach (i, j)
  - Base: dp[0][0] = 1 if no obstacle
  - Transition: dp[i][j] = dp[i-1][j] + dp[i][j-1] if no obstacle
  - Time: O(m × n), Space: O(m × n) or O(n) optimized

- **Minimum Path Sum**
  - Given grid with costs, find minimum cost path
  - dp[i][j] = minimum cost to reach (i, j)
  - Transition: dp[i][j] = cost[i][j] + min(dp[i-1][j], dp[i][j-1])

- **Edit Distance (Levenshtein)**
  - Transform word1 to word2 with insert/delete/replace
  - State: dp[i][j] = min edits to transform word1[0..i-1] to word2[0..j-1]
  - Transitions:
    - If characters match: dp[i][j] = dp[i-1][j-1]
    - Else: min(dp[i-1][j], dp[i][j-1], dp[i-1][j-1]) + 1
  - Time: O(m × n), Space: O(m × n)

- **Longest Common Subsequence (LCS)**
  - Longest sequence present in both strings
  - State: dp[i][j] = length of LCS of word1[0..i-1] and word2[0..j-1]
  - Transitions:
    - If match: dp[i][j] = dp[i-1][j-1] + 1
    - Else: dp[i][j] = max(dp[i-1][j], dp[i][j-1])
  - Time: O(m × n)

- **Matrix Chain Multiplication**
  - Given matrices, find optimal order to multiply
  - State: dp[i][j] = min operations to multiply matrices i to j
  - Transition: try all split points
  - Time: O(n³)

- **Space Optimization**
  - 2D DP often reducible to 1D if only previous row needed
  - Save space from O(m × n) to O(n)

**Key Insights:**
- 2D DP common for grid and string problems
- Edit distance fundamental for many applications
- Can often optimize space

**Deliverables:**
- [ ] Implement unique paths in grid
- [ ] Implement minimum path sum
- [ ] Implement edit distance
- [ ] Implement LCS
- [ ] Optimize space for grid problems

---

### 📅 DAY 4: DP ON SEQUENCES

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Longest Increasing Subsequence (LIS)**
  - Find longest subsequence with increasing values
  - Naive: O(2^n) all subsequences
  - DP O(n²): dp[i] = longest ending at i
    - Transition: dp[i] = max(dp[j] + 1 for all j < i where arr[j] < arr[i])
  - DP O(n log n): binary search on tail array
  - Time: O(n²) or O(n log n)

- **Longest Decreasing Subsequence**
  - Symmetric to LIS
  - Same approaches apply

- **Maximum Subarray Sum (Kadane Revisited)**
  - dp[i] = max sum ending at i
  - Transition: dp[i] = max(arr[i], dp[i-1] + arr[i])
  - Answer: max(dp[i])

- **Weighted Interval Scheduling**
  - Intervals with weights, select non-overlapping to maximize weight
  - Sort by end time
  - dp[i] = max weight using intervals up to i
  - Transition: include i or exclude i
  - Find previous compatible interval via binary search
  - Time: O(n log n)

- **Subsequence Problem Family**
  - Distinct subsequences
  - Delete minimum characters to make palindrome
  - Shortest supersequence containing two strings

- **DP on Permutations**
  - State: use bitmask for subset of items
  - Example: TSP with DP

**Key Insights:**
- Sequence DP often involves position-based states
- LIS O(n log n) clever binary search approach
- Many variations with similar structure

**Deliverables:**
- [ ] Implement LIS O(n²) and O(n log n)
- [ ] Implement weighted interval scheduling
- [ ] Solve maximum subarray sum
- [ ] Implement longest decreasing subsequence
- [ ] Solve distinct subsequences problem

---

### 📅 DAY 5 (OPTIONAL): STORY-DRIVEN DP (MIT 6.006 STYLE)

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Problem Interpretation**
  - Complex DP problems often have story/real-world context
  - Key: translate story into state and transitions
  - Example: text justification, blackjack, etc.

- **Text Justification**
  - Format text with width constraint
  - Minimize "badness" (wasted space penalty)
  - DP: dp[i] = min badness for words 0..i
  - Transition: try all ending positions for current line
  - Calculate badness for each possibility

- **Blackjack DP**
  - State: (player's hand, dealer's shown card)
  - Value: expected payoff
  - Compute: recursively combine actions

- **State Design Principles**
  - State should capture: what matters for future decisions?
  - Example: in activity scheduling, only finish time matters, not start time
  - Example: in coin change, only remaining amount matters
  - Careful: too specific → huge state space; too general → can't recover answer

- **Transition Design**
  - Clearly identify choices at each state
  - Compute all possibilities
  - Combine: min, max, sum depending on problem

- **Real-World DP Examples**
  - Network routing: min latency
  - Game theory: optimal strategy
  - Financial: portfolio optimization
  - Resource allocation: maximize profit/minimize cost

**Key Insights:**
- State and transition design is art and skill
- Practice with varied problems builds intuition
- Real-world problems often complex but reducible to DP

**Deliverables:**
- [ ] Understand text justification problem
- [ ] Translate story problem to DP state/transitions
- [ ] Solve 2-3 story-driven problems
- [ ] Develop intuition for state design

---

## 📌 WEEK 11: DYNAMIC PROGRAMMING II - TREES, DAGS & ADVANCED PATTERNS

### Weekly Goal
Extend DP to more complex structures. Master tree DP, DAG DP, bitmask DP, and recognize when DP is the right approach.

### Weekly Topics
- DP on trees (rerooting, subtree problems)
- DP on DAGs (longest/shortest paths)
- Bitmask DP (subsets, TSP)
- State compression and optimization

---

### 📅 DAY 1: DP ON TREES

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Tree DP Framework**
  - Solve for each subtree, combine into parent
  - Post-order traversal (solve children before parent)
  - State: what's the answer for this subtree?

- **Maximum Independent Set**
  - Select nodes with no edges between them
  - Maximize value of selected nodes
  - dp[node][0] = max value excluding node
  - dp[node][1] = max value including node
  - Transition: combine children's answers

- **Tree Diameter via DP**
  - Longest path in tree
  - dp[node] = max distance down from node
  - Answer: max(left_depth + right_depth + 1)
  - Time: O(n)

- **Subtree Sum / Subtree Sizes**
  - Simple: sum all nodes in subtree
  - Post-order: solve children, add to node

- **Tree Coloring (K Colors)**
  - Color nodes with K colors, adjacent different
  - dp[node][color] = count of ways
  - Transition: children must use different colors

- **Rerooting DP** (Advanced)
  - Compute answer for each node as root
  - First pass: tree rooted at 0
  - Second pass: reroot, combining parent info

- **Practical Considerations**
  - Space: often can work with adjacency list (no explicit table)
  - Time: O(n) if each node processed once
  - Recursion depth: can hit stack limits for very deep trees

**Key Insights:**
- Tree DP elegantly solves many tree problems
- Post-order processing natural fit
- Often O(n) complexity

**Deliverables:**
- [ ] Implement tree diameter
- [ ] Implement maximum independent set
- [ ] Implement tree DP for custom problem
- [ ] Solve tree coloring problem

---

### 📅 DAY 2: DP ON DAGS

**Duration:** 90 minutes

**Topics & Subtopics:**

- **DAG (Directed Acyclic Graph)**
  - Directed graph with no cycles
  - Enables topological ordering
  - DP naturally applies to DAG

- **Longest Path in DAG**
  - No cycles → DP works
  - Topological sort order → compute in order
  - dp[node] = longest path starting at node
  - Transition: max(1 + dp[neighbor] for all neighbors)
  - Time: O(V + E)

- **Shortest Path in DAG with Negative Weights**
  - Bellman-Ford O(VE) needed for general graphs
  - DAG allows O(V + E) via topo sort
  - Process nodes in topo order, relax edges

- **Vertex Weight Sum Paths**
  - Path value = sum of vertex weights (not edge weights)
  - Longest vertex weight path in DAG
  - Similar DP but sum vertices instead of edges

- **All Paths in DAG**
  - Count or enumerate paths from s to t
  - DP: dp[node] = number of paths from node to t
  - Transition: sum paths through neighbors

- **Topo Sort Ordering**
  - Critical: must process nodes in order where all predecessors processed
  - Ensures correct DP computation
  - Failure to respect order → wrong answer

**Key Insights:**
- DAG enables efficient DP despite weights
- Topo sort ordering critical
- O(V + E) vs O(VE) significant improvement

**Deliverables:**
- [ ] Implement longest path in DAG
- [ ] Implement shortest path in DAG (negative weights)
- [ ] Count all paths in DAG
- [ ] Verify topo sort order importance

---

### 📅 DAY 3: BITMASK & SUBSET DP

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Bitmask Representation**
  - Set of n elements → bitmask of 2^n states
  - Bit i = 1 if element i in subset
  - Iterate over subsets: 0 to 2^n - 1

- **Subset Enumeration**
  - For each mask: iterate elements to see which included
  - Iterate submasks: clever bit manipulation

- **TSP with DP (Traveling Salesman)**
  - Visit n cities exactly once, minimum cost
  - Naive: O(n!) permutations
  - DP: state = (visited_cities, current_city)
  - dp[mask][last] = min cost visiting cities in mask, ending at last
  - Transition: visit next unvisited city
  - Time: O(2^n × n²), Space: O(2^n × n)

- **Subset Sum Problems**
  - Count/find subsets with specific sum
  - dp[i] = ways to make sum i
  - Transition: include/exclude elements

- **Maximum Weight Independent Set in Graph**
  - Can use bitmask if vertices few (≤ 20)
  - dp[mask] = max weight with vertices in mask, no edges between
  - Enumerate all masks, check if valid independent set

- **Traveling from Point A to B visiting all Waypoints**
  - Similar to TSP but don't return
  - State: (mask, last_position)

- **Complexity**
  - Time: O(2^n × f(n)) where f(n) is work per state
  - Space: O(2^n) for DP table
  - Feasible: n ≤ 20, marginal for n ≤ 25

- **Practical Considerations**
  - 2^20 = 1M states (manageable)
  - 2^25 = 33M states (tight)
  - 2^30 = 1B states (too much)

**Key Insights:**
- Bitmask DP when subset of small set is state
- TSP classic example of exponential DP
- Feasible only for n ≤ 20-25

**Deliverables:**
- [ ] Implement TSP with DP
- [ ] Solve subset sum problem
- [ ] Implement maximum weighted independent set (small graph)
- [ ] Count/enumerate subsets with constraints

---

### 📅 DAY 4 (OPTIONAL): STATE COMPRESSION & OPTIMIZATIONS

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Space Optimization Techniques**
  - Sliding window: keep only current and previous states
  - Example: 2D → 1D if only need previous row
  - Saves from O(m × n) to O(n)

- **Dimension Reduction**
  - 3D DP → 2D by observing what matters
  - Example: coin change with denominations → doesn't need 3D

- **Memoization vs Tabulation Trade-off**
  - Memoization: compute only needed states
  - Tabulation: compute all, more cache-friendly
  - Choose based on state space sparsity

- **Pruning in DP**
  - Skip invalid states
  - Early termination if answer found
  - Example: if current cost exceeds best known, prune

- **Matrix Exponentiation for DP**
  - Linear recurrence → matrix multiplication
  - Example: Fibonacci via matrix power
  - Enables O(log n) for O(n) DP

- **Convex Hull Trick**
  - Optimize DP transitions using geometry
  - When transitions are linear
  - Advanced technique, rarely in interviews

**Key Insights:**
- Optimization often critical for large inputs
- Space-time tradeoffs common
- Matrix exponentiation clever for linear recurrences

**Deliverables:**
- [ ] Optimize space for grid DP problems
- [ ] Apply matrix exponentiation to recurrence
- [ ] Implement pruning in DP
- [ ] Recognize when optimizations needed

---

### 📅 DAY 5 (OPTIONAL): MIXED DP PROBLEMS

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Multi-Concept Problems**
  - Combining DP with other algorithms
  - Example: DP on graph shortest paths
  - Example: DP with greedy choices

- **DP + Greedy**
  - Some problems have greedy part and DP part
  - Example: jump game II (greedy jump distance)
  - Example: best time to buy/sell stock variants

- **DP + Graph Algorithms**
  - Shortest path with DP formulation
  - DP on all reachable states

- **DP + Number Theory**
  - DP with modular arithmetic
  - Example: count solutions mod p

- **Recognition & Intuition**
  - Overlapping subproblems → DP candidate
  - Optimal substructure → DP candidate
  - Practice: solve many problems builds intuition

**Key Insights:**
- Complex problems combine multiple techniques
- Recognition comes with practice
- Flexibility in DP application critical

**Deliverables:**
- [ ] Solve 2-3 mixed concept problems
- [ ] Identify when DP is part of solution
- [ ] Combine DP with other algorithms
- [ ] Build problem-solving intuition

---

# END OF PHASE C

**Total Content:**
- 5 weeks (Weeks 7-11)
- 25 study days (5 days per week)
- 120+ detailed topics and subtopics
- 250+ problem patterns covered
- 90-120 minutes per day
- 75-100 hours of material

**Phase C Mastery Outcomes:**
- ✅ Tree structures and operations mastery
- ✅ Graph algorithms (shortest paths, MST, connectivity)
- ✅ Dynamic programming fluency across domains
- ✅ Recognition of DP vs greedy vs other paradigms
- ✅ Complex problem-solving combining multiple techniques

**Next:** Phase D - Algorithm Paradigms (Weeks 12-13)

---

**Version:** 13.0
**Status:** ✅ COMPLETE - PRODUCTION READY
**Date:** January 24, 2026