# 13. Graph Traversal Mastery Guide

**Goal:** Professional-grade intuition for graph traversal and search from **basic DFS/BFS** to **advanced graph algorithms**.
**Philosophy (Curriculum 2.0):** Focus heavily on pointer states, invariants, and step-by-step state transitions. Stop memorizing syntax; master the mental models.

---

## 🧭 Curriculum 2.0 Summary Table

| Level | Mental Model | Pointer / Frontier State | Drill Problems |
|---|---|---|---|
| **L1: Basic Movement** | Mark territories. Never revisit. | `queue`/`stack` frontier. | 🟢 200, 🟡 547 |
| **L2: Iterative & Components** | Explicit control of stack frames. | Outer loop for isolation. | 🟡 133, 🟡 323 |
| **L3: Shortest Paths** | Expand in concentric waves. | Level-sized queue batches. | 🟡 994, 🟡 433 |
| **L4: Constraints & 0-1** | Directed dependencies & dual-cost. | Indegree arr / Deque. | 🟡 207, 🔴 1368 |
| **L5: State-Space** | Nodes as complex structural states. | Tuple/String hashed states. | 🟡 752, 🔴 773 |
| **L6: Dijkstra (Weighted)** | Prioritize minimum accumulated cost. | Min-Heap `(dist, node)`. | 🟡 743, 🟡 1631 |
| **L7: Decomposition** | Discovery times map structural flaws. | `tin` and `low` timers. | 🔴 1192 |
| **L8: Spanning Trees** | Cheapest full connectivity. | Sorted edges + DSU. | 🟡 1584, 🔴 1489 |

---

## L1 — Physical Layer: Basic Movement (DFS/BFS)

**Mental Model:** Traversal explores all reachable nodes from a start point.
**What region is processed?** The immediate unvisited neighbors of the current node `u`.
**What is the invariant?** `Visited` exactly matches the discovered region; nodes enter the visited set once and only once upon generation.

### Visual State Transition (BFS)
*Graph: A-B, A-C, B-D. Start at A.*

| Step | Current `u` | Queue (Frontier) | Visited Set | Action / Invariant |
|---|---|---|---|---|
| 1 | - | `[A]` | `{A}` | Push start node, mark visited immediately. |
| 2 | `A` | `[B, C]` | `{A, B, C}` | Pop A, push unvisited neighbors (B, C), mark them. |
| 3 | `B` | `[C, D]` | `{A, B, C, D}` | Pop B, push D, mark visited. |
| 4 | `C` | `[D]` | `{A, B, C, D}` | Pop C, no unvisited neighbors. |
| 5 | `D` | `[]` | `{A, B, C, D}` | Pop D, no unvisited neighbors. Frontier empty. |

### Code Snippets

**Python:**
Problem: Traverse all reachable nodes from a starting position without revisiting any node.
```python
def bfs(start, adj):
    # 1. Initialize frontier (queue) and mark territory (visited set)
    queue = deque([start])
    visited = {start}
    
    # 2. Process nodes until the frontier is exhausted
    while queue:
        # 3. Extract the current node to process
        u = queue.popleft()
        
        # 4. Explore immediate unvisited neighbors
        for v in adj[u]:
            if v not in visited:
                # 5. Invariant: Mark visited IMMEDIATELY to prevent duplicate enqueuing
                visited.add(v)
                queue.append(v)
```

**C#:**
Problem: Traverse all reachable nodes from a starting position without revisiting any node.
```csharp
public void BFS(int start, List<int>[] adj) {
    // 1. Initialize frontier (queue) and mark territory (visited set)
    Queue<int> q = new Queue<int>();
    HashSet<int> visited = new HashSet<int>();
    
    q.Enqueue(start);
    visited.Add(start);
    
    // 2. Process nodes until the frontier is exhausted
    while (q.Count > 0) {
        // 3. Extract the current node to process
        int u = q.Dequeue();
        
        // 4. Explore immediate unvisited neighbors
        foreach (int v in adj[u]) {
            // 5. Invariant: HashSet.Add returns true only if newly added (marked immediately)
            if (visited.Add(v)) { 
                q.Enqueue(v);
            }
        }
    }
}
```

### ⚠️ Gotchas & Pitfalls
- **Queue Duplication (BFS):** Failing to mark a node as visited *immediately* when enqueuing it. If you wait until it is popped to mark it, the same node can be enqueued multiple times by different neighbors, leading to exponential memory blowup.
- **Grid Mutation Overwrites:** Modifying the grid in-place (e.g., `grid[r][c] = -1`) as a visited set without properly scoping or reverting it, breaking other functions or queries that rely on the original graph data.
- **Empty Input Crashes:** Not handling the base case of an empty graph, grid, or null start node, resulting in immediate out-of-bounds or null reference exceptions.

### 🎯 Drill Problems
- 🟢 [LeetCode 200: Number of Islands]
- 🟢 [LeetCode 733: Flood Fill]
- 🟡 [LeetCode 547: Number of Provinces]

---

## L2 — Structural Layer: Iterative DFS + Components

**Mental Model:** Recursion relies on an implicit stack which can overflow. An explicit stack safely simulates this. Many graphs are disconnected; we need an outer loop to discover isolated components.
**What does the index mean?** The outer loop variable `i` represents a potential start node for a new component.
**What is the invariant?** Each launch of the traversal from the outer loop discovers exactly one complete connected component. Marking happens on *push*, strictly preventing duplicates in the stack.

### Visual State Transition (Iterative DFS)
*Graph: A-B, A-C. Start at A.*

| Step | Stack State | Visited Set | Action / Invariant |
|---|---|---|---|
| 1 | `[A]` | `{A}` | Push A, mark visited. |
| 2 | `[B, C]` | `{A, B, C}` | Pop A, push B and C, mark both. |
| 3 | `[B]` | `{A, B, C}` | Pop C, no unvisited neighbors. |
| 4 | `[]` | `{A, B, C}` | Pop B, no unvisited neighbors. |

### Code Snippets

**Python:**
Problem: Count the number of connected components in an undirected graph using iterative DFS with an explicit stack.
```python
def count_components(n: int, edges: list[list[int]]) -> int:
    # 1. Build adjacency list for undirected graph
    adj = [[] for _ in range(n)]
    for u, v in edges:
        adj[u].append(v)
        adj[v].append(u)
        
    visited = set()
    components = 0
    
    # 2. Outer loop: Launch DFS from every unvisited node to cover disconnected components
    for i in range(n):
        if i not in visited:
            components += 1
            # 3. Explicit stack safely simulates call stack without recursion depth limits
            stack = [i]
            visited.add(i)
            
            while stack:
                curr = stack.pop()
                for nei in adj[curr]:
                    if nei not in visited:
                        # 4. Invariant: Mark on push to strictly avoid duplicate stack entries
                        visited.add(nei)
                        stack.append(nei)
                        
    return components
```

**C#:**
Problem: Count the number of connected components in an undirected graph using iterative DFS with an explicit stack.
```csharp
public int CountComponents(int n, int[][] edges) {
    // 1. Build adjacency list for undirected graph
    List<int>[] adj = new List<int>[n];
    for (int i = 0; i < n; i++) adj[i] = new List<int>();
    foreach (var e in edges) {
        adj[e[0]].Add(e[1]);
        adj[e[1]].Add(e[0]);
    }

    var visited = new HashSet<int>();
    int components = 0;

    // 2. Outer loop: Launch DFS from every unvisited node to cover disconnected components
    for (int i = 0; i < n; i++) {
        if (visited.Add(i)) {
            components++;
            // 3. Explicit stack safely simulates call stack without recursion depth limits
            var stack = new Stack<int>();
            stack.Push(i);

            while (stack.Count > 0) {
                int curr = stack.Pop();
                foreach (int nei in adj[curr]) {
                    // 4. Invariant: Mark on push to strictly avoid duplicate stack entries
                    if (visited.Add(nei)) {
                        stack.Push(nei);
                    }
                }
            }
        }
    }
    return components;
}
```

### ⚠️ Gotchas & Pitfalls
- **Missing the Outer Loop:** Forgetting the outer loop when the graph might be disconnected, meaning the traversal halts after one component and silently misses unvisited sections.
- **Late Marking on Push:** Pushing to the stack without checking if it's visited *before* pushing can lead to extremely large stacks if multiple paths point to the same node in dense graphs.
- **Recursion Limit Exceeded:** Falling back to recursive DFS for deep components (like a long linked list shape) without increasing the system recursion limit in languages like Python (`sys.setrecursionlimit`), causing crashes.

### 🎯 Drill Problems
- 🟡 [LeetCode 133: Clone Graph]
- 🟡 [LeetCode 323: Number of Connected Components in an Undirected Graph]

---

## L3 — Frontier Layer: Shortest Paths & Multi-Source

**Mental Model:** BFS expands like ripples in a pond. Multi-source BFS starts with multiple pebbles dropped simultaneously.
**What region is processed?** A complete "level" or "wave" of nodes at distance `d` before any node at `d+1`.
**What is the invariant?** The queue strictly contains nodes at distance `d`, followed by nodes at distance `d+1`. First discovery guarantees minimal distance.

### Visual State Transition (Level-by-Level)

| Step | Queue | `level_size` | Current Depth | Action / Invariant |
|---|---|---|---|---|
| 1 | `[A]` | 1 | 0 | Process A. Push B, C. |
| 2 | `[B, C]` | 2 | 1 | Process B, C. Push D. |
| 3 | `[D]` | 1 | 2 | Process D. |

### Code Snippets
**Python:**
Problem: Find the shortest distance from multiple starting nodes (sources) to all other reachable nodes simultaneously.
```python
def multi_source_bfs(sources, adj):
    # 1. Initialize frontier with all starting pebbles and mark them visited
    queue = deque(sources)
    visited = set(sources)
    steps = 0
    
    # 2. Process concentric waves level-by-level
    while queue:
        # 3. Capture the exact size of the current wave
        level_size = len(queue)
        
        # 4. Process all nodes strictly within the current wave
        for _ in range(level_size):
            u = queue.popleft()
            
            # 5. Expand to next wave of neighbors
            for v in adj[u]:
                if v not in visited:
                    # 6. Invariant: Mark immediately to avoid duplicate work
                    visited.add(v)
                    queue.append(v)
                    
        # 7. Increment distance strictly after finishing the entire wave
        steps += 1
        
    return steps
```

**C#:**
Problem: Find the shortest distance from multiple starting nodes (sources) to all other reachable nodes simultaneously.
```csharp
public int MultiSourceBFS(List<int> sources, List<int>[] adj) {
    // 1. Initialize frontier with all starting pebbles and mark them visited
    var queue = new Queue<int>(sources);
    var visited = new HashSet<int>(sources);
    int steps = 0;

    // 2. Process concentric waves level-by-level
    while (queue.Count > 0) {
        // 3. Capture the exact size of the current wave
        int levelSize = queue.Count;

        // 4. Process all nodes strictly within the current wave
        for (int i = 0; i < levelSize; i++) {
            int u = queue.Dequeue();

            // 5. Expand to next wave of neighbors
            foreach (int v in adj[u]) {
                if (visited.Add(v)) {
                    // 6. Invariant: Mark immediately to avoid duplicate work
                    queue.Enqueue(v);
                }
            }
        }

        // 7. Increment distance strictly after finishing the entire wave
        steps++;
    }
    return steps;
}
```

### ⚠️ Gotchas & Pitfalls
- **Step Increment Location:** Incrementing the level/step counter inside the inner neighbor loop instead of outside the `level_size` loop, completely breaking the breadth-first distance count.
- **Missing Base Distances:** Enqueueing the initial sources but forgetting to initialize the distance/steps to 0 (or equivalent base value) for *all* sources before the loop starts.
- **Wrong Queue Size snapshot:** Not capturing `len(queue)` into a variable (like `level_size`) before looping. Evaluating `len(queue)` directly in the loop condition causes the loop to consume newly added elements from the next level prematurely.

### 🎯 Drill Problems
- 🟡 [LeetCode 994: Rotting Oranges]
- 🟡 [LeetCode 433: Minimum Genetic Mutation]
- 🟡 [LeetCode 785: Is Graph Bipartite?]

---

## L4 — Constraint Layer: Topo Sort, Cycles, & 0-1 BFS

**Mental Model:** Dependencies dictate processing order.
**What does the index mean?** `indegree[u]` counts unresolved prerequisite edges pointing to `u`.
**What is the invariant?** In Kahn's Algorithm, the queue contains ONLY nodes with `indegree == 0`.

### Visual State Transition (Kahn's Topo Sort)
*Graph: A -> B, A -> C, B -> D, C -> D.*

| Node | Initial Indegree | Step 1 (Pop A) | Step 2 (Pop B) | Step 3 (Pop C) | Action / Invariant |
|---|---|---|---|---|---|
| **A** | 0 | (Popped) | - | - | Enqueue A initially. |
| **B** | 1 | 0 (Enqueue) | (Popped) | - | Indegree hits 0, enqueue. |
| **C** | 1 | 0 (Enqueue) | 0 | (Popped) | Indegree hits 0, enqueue. |
| **D** | 2 | 2 | 1 | 0 (Enqueue) | Wait until all dependencies clear. |

### Code Snippets

**Python:**
Problem: Find the topological ordering of courses given prerequisite dependencies (Kahn's algorithm).
```python
def find_order(num_courses: int, prerequisites: list[list[int]]) -> list[int]:
    # 1. Build adjacency list and compute indegrees
    adj = [[] for _ in range(num_courses)]
    indegree = [0] * num_courses
    for dest, src in prerequisites:
        adj[src].append(dest)
        indegree[dest] += 1
        
    # 2. Invariant: Initialize queue ONLY with nodes having indegree == 0 (zero prerequisites)
    queue = deque([i for i in range(num_courses) if indegree[i] == 0])
    topo_order = []
    
    while queue:
        u = queue.popleft()
        topo_order.append(u)
        
        # 3. For each dependent node, decrement unresolved prerequisite count
        for v in adj[u]:
            indegree[v] -= 1
            # 4. Once all prerequisites are satisfied (indegree hits 0), enqueue
            if indegree[v] == 0:
                queue.append(v)
                
    # 5. Cycle check: If order length < num_courses, a cycle prevented resolution
    return topo_order if len(topo_order) == num_courses else []
```

**C#:**
Problem: Find the topological ordering of courses given prerequisite dependencies (Kahn's algorithm).
```csharp
public int[] FindOrder(int numCourses, int[][] prerequisites) {
    // 1. Build adjacency list and compute indegrees
    List<int>[] adj = new List<int>[numCourses];
    for (int i = 0; i < numCourses; i++) adj[i] = new List<int>();
    int[] indegree = new int[numCourses];

    foreach (var pre in prerequisites) {
        adj[pre[1]].Add(pre[0]);
        indegree[pre[0]]++;
    }

    // 2. Invariant: Initialize queue ONLY with nodes having indegree == 0
    var queue = new Queue<int>();
    for (int i = 0; i < numCourses; i++) {
        if (indegree[i] == 0) queue.Enqueue(i);
    }

    var topoOrder = new List<int>();

    while (queue.Count > 0) {
        int u = queue.Dequeue();
        topoOrder.Add(u);

        // 3. For each dependent node, decrement unresolved prerequisite count
        foreach (int v in adj[u]) {
            indegree[v]--;
            // 4. Once all prerequisites are satisfied (indegree hits 0), enqueue
            if (indegree[v] == 0) {
                queue.Enqueue(v);
            }
        }
    }

    // 5. Cycle check: If order length < numCourses, a cycle prevented resolution
    return topoOrder.Count == numCourses ? topoOrder.ToArray() : Array.Empty<int>();
}
```

### ⚠️ Gotchas & Pitfalls
- **Unverified Topo Result:** In Kahn's algorithm, forgetting to check if the final topological sorted list length equals the total number of nodes, which indicates a cycle wasn't processed and the graph can't be fully resolved.
- **0-1 BFS Queue Mix-up:** Using a standard Queue for 0-1 BFS instead of a Deque. Appending a weight-0 edge to the back instead of the front violates the distance invariant and gives incorrect shortest paths.
- **Indegree Over-decrement:** Decrementing indegrees in the neighbor loop without ensuring the dependency is actually valid, leading to negative indegrees and logic bugs.

### 🎯 Drill Problems
- 🟡 [LeetCode 207: Course Schedule]
- 🟡 [LeetCode 210: Course Schedule II]
- 🟡 [LeetCode 802: Find Eventual Safe States]
- 🔴 [LeetCode 1368: Minimum Cost to Make at Least One Valid Path in a Grid] *(0-1 BFS)*

---

## L5 — Abstract Layer: Implicit Graphs (State-Space)

**Mental Model:** Nodes aren't integers; they are structural states (like matrix grids or strings).
**What is the invariant?** Serialized representations (e.g., tuples or strings) act as unique identifiers in the `visited` set to prevent endless cycles in the state space.

### Code Snippets

**Python:**
Problem: Find the minimum total number of turns required to open a combination lock avoiding deadends using BFS on a state space graph.
```python
def open_lock(deadends: list[str], target: str) -> int:
    dead = set(deadends)
    if "0000" in dead: return -1
    
    # 1. State: 4-digit string representing lock combination; queue holds (state, turns)
    queue = deque([("0000", 0)])
    visited = {"0000"}
    
    while queue:
        state, turns = queue.popleft()
        if state == target:
            return turns
            
        # 2. Explore all 8 adjacent state transitions (4 dials, +1 or -1 each)
        for i in range(4):
            digit = int(state[i])
            for move in (-1, 1):
                next_digit = (digit + move) % 10
                next_state = state[:i] + str(next_digit) + state[i+1:]
                # 3. Invariant: Check deadends and visited set on string state
                if next_state not in dead and next_state not in visited:
                    visited.add(next_state)
                    queue.append((next_state, turns + 1))
                    
    return -1
```

**C#:**
Problem: Find the minimum total number of turns required to open a combination lock avoiding deadends using BFS on a state space graph.
```csharp
public int OpenLock(string[] deadends, string target) {
    var dead = new HashSet<string>(deadends);
    if (dead.Contains("0000")) return -1;

    // 1. State: 4-digit string representing lock combination
    var queue = new Queue<(string State, int Turns)>();
    var visited = new HashSet<string> { "0000" };
    queue.Enqueue(("0000", 0));

    while (queue.Count > 0) {
        var (state, turns) = queue.Dequeue();
        if (state == target) return turns;

        // 2. Explore all 8 adjacent state transitions (4 dials, +1 or -1 each)
        for (int i = 0; i < 4; i++) {
            int digit = state[i] - '0';
            foreach (int move in new[] { -1, 1 }) {
                int nextDigit = (digit + move + 10) % 10;
                char[] chars = state.ToCharArray();
                chars[i] = (char)('0' + nextDigit);
                string nextState = new string(chars);

                // 3. Invariant: Check deadends and visited set on string state
                if (!dead.Contains(nextState) && visited.Add(nextState)) {
                    queue.Enqueue((nextState, turns + 1));
                }
            }
        }
    }
    return -1;
}
```

### ⚠️ Gotchas & Pitfalls
- **Mutable State Hashing:** Trying to add a mutable type (like a List or Array) to a Visited HashSet, causing unhashable type errors. You must convert states to immutable types like Tuples or Strings.
- **Serialization Overhead:** Using heavy string operations or deep copies at every traversal step without optimization, leading to severe Time Limit Exceeded (TLE) issues despite having the right time complexity logic.
- **Backtracking vs BFS:** Accidentally reverting state changes in a BFS context (like you would in backtracking DFS) instead of passing completely new, independent copies of the state into the queue.

### 🎯 Drill Problems
- 🟡 [LeetCode 752: Open the Lock]
- 🔴 [LeetCode 773: Sliding Puzzle]

---

## L6 — Weighted Shortest Paths (Dijkstra)

**Mental Model:** Expand the frontier prioritizing the lowest accumulated cost, rather than the fewest edges.
**Pointer State:** `Min-Heap` storing `(dist, node)`.
**What is the invariant?** The heap always pops the globally minimum distance available. Stale heap entries are ignored.

> [!NOTE]
> For priority queue internals, zero-allocation min-heaps in .NET (`PriorityQueue<TElement, TPriority>`), and stale-entry pruning strategies, see the [Queues Traversal Mastery Guide](../11_queues/queues_mastery_guide.md#level-6-priority-queue-frontier-best-first).

### Visual State Transition (Dijkstra Stale Skip)

| Step | Heap State | Popped `(d, u)` | `dist` Array | Action / Invariant |
|---|---|---|---|---|
| 1 | `[(0, A)]` | `(0, A)` | `A:0` | Process A. Updates B to 5, C to 2. |
| 2 | `[(2, C), (5, B)]` | `(2, C)` | `C:2` | Process C. Finds shorter path to B (cost 3). |
| 3 | `[(3, B), (5, B)]` | `(3, B)` | `B:3` | Process B at cost 3. |
| 4 | `[(5, B)]` | `(5, B)` | `B:3` | **STALE SKIP**: `5 > dist[B] (3)`. Ignored! |

### Code Snippets

**Python:**
Problem: Find the minimum weighted distance from a source node to all other nodes in a graph with non-negative edge weights.
```python
import heapq

def dijkstra(n: int, adj: list[list[tuple[int, int]]], src: int) -> list[int]:
    # 1. Initialize distance array with infinity
    dist = [float('inf')] * n
    dist[src] = 0
    
    # 2. Min-Heap prioritizing lowest accumulated cost (distance, node)
    pq = [(0, src)]
    
    while pq:
        # 3. Pop the node with current globally minimum distance
        d, u = heapq.heappop(pq)
        
        # 4. Invariant check: Skip stale heap entries if a shorter path was already recorded
        if d > dist[u]:
            continue
            
        # 5. Relax edges to all neighbors
        for v, w in adj[u]:
            if dist[u] + w < dist[v]:
                dist[v] = dist[u] + w
                heapq.heappush(pq, (dist[v], v))
                
    return dist
```

**C#:**
Problem: Find the minimum weighted distance from a source node to all other nodes in a graph with non-negative edge weights.
```csharp
public int[] Dijkstra(int n, List<(int node, int weight)>[] adj, int src) {
    // 1. Initialize distance array with max values
    int[] dist = new int[n];
    Array.Fill(dist, int.MaxValue);
    dist[src] = 0;
    
    // 2. Initialize Min-Heap prioritizing the lowest accumulated cost
    PriorityQueue<int, int> pq = new PriorityQueue<int, int>();
    pq.Enqueue(src, 0);
    
    while (pq.Count > 0) {
        // 3. Pop the node with the current globally minimum distance
        pq.TryDequeue(out int u, out int d);
        
        // 4. Invariant check: Skip stale heap entries if we already found a better path
        if (d > dist[u]) continue; // Skip stale
        
        // 5. Relax edges to all neighbors
        foreach (var edge in adj[u]) {
            int v = edge.node;
            int w = edge.weight;
            
            // 6. If a cheaper path is found, update and push to heap
            if (dist[u] + w < dist[v]) {
                dist[v] = dist[u] + w;
                pq.Enqueue(v, dist[v]);
            }
        }
    }
    return dist;
}
```

### ⚠️ Gotchas & Pitfalls
- **Missing Stale Skip Check:** Forgetting the `if (d > dist[u]) continue;` check, resulting in redundant processing of outdated paths and Time Limit Exceeded (TLE) on dense graphs.
- **Standard Queue Mistake:** Using a standard queue instead of a Min-Heap. This downgrades Dijkstra into an inefficient BFS that repeatedly overrides distances but processes them completely out of order, often causing exponential worst-case time.
- **Integer Overflow on Cost:** Initializing distance arrays with `Integer.MAX_VALUE` (or similar max values) and then adding edge weights to them (`dist[u] + w`) without a bounds check, causing integer overflow to a negative number.

### 🎯 Drill Problems
- 🟡 [LeetCode 743: Network Delay Time]
- 🟡 [LeetCode 1631: Path With Minimum Effort]

---

## L7 — Decomposition: SCCs & Bridges

**Mental Model:** Graph structure analysis using DFS discovery timelines.
**What does the index mean?** `tin[u]` is the time `u` was first visited. `low[u]` is the lowest `tin` reachable from `u`.
**What is the invariant?** If `low[v] > tin[u]`, the edge `(u, v)` is a bridge because `v` cannot reach back to `u` or its ancestors.

### ⚠️ Gotchas & Pitfalls
- **Wrong Back-Edge Update:** Accidentally updating `low[u] = min(low[u], low[v])` instead of `low[u] = min(low[u], tin[v])` for a back-edge (where `v` is already visited). Using `low[v]` corrupts the definition of discovery time for SCCs (Tarjan's).
- **Not Tracking Parent:** Not passing the `parent` reference in the DFS signature for undirected graphs, causing the algorithm to immediately traverse back along the edge it just came from and incorrectly flag it as a cycle.
- **Global Time Mismanagement:** Failing to increment the global timer variable properly across recursive calls, or resetting it accidentally in different connected components.

### 🎯 Drill Problems
- 🔴 [LeetCode 1192: Critical Connections in a Network]

---

## L8 — Spanning Structures (MST + DSU)

**Mental Model:** Build the cheapest network connecting all nodes.
**What is the invariant?** (Kruskal) Always pick the globally cheapest edge that connects two disjoint sets, maintaining a cycle-free structure.

### ⚠️ Gotchas & Pitfalls
- **Missing Path Compression:** Forgetting to apply path compression (`parent[x] = find(parent[x])`) or union by rank in the DSU implementation, turning fast `O(α(N))` operations into slow `O(N)` linear scans.
- **Skipping Edge Sorting (Kruskal's):** Failing to sort the edges by weight before processing in Kruskal's algorithm, nullifying the greedy choice property and resulting in an invalid MST that isn't minimum.
- **Disconnected MST:** Failing to check if all nodes were successfully united into a single component at the end. If the graph is disconnected, an MST spanning all nodes is impossible, and the code should handle this gracefully (e.g., returning `-1`).

### 🎯 Drill Problems
- 🟡 [LeetCode 1584: Min Cost to Connect All Points]
- 🔴 [LeetCode 1489: Find Critical and Pseudo-Critical Edges in Minimum Spanning Tree]
