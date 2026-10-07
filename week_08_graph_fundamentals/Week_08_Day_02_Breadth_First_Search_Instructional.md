# 📘 Week 8 Day 2: Breadth-First Search (BFS) — Engineering Guide

> 🧭 **Navigation:** [← Previous Day](Week_08_Day_01_Graph_Models_and_Representations_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_08_Day_03_DFS_Topological_Sort_Instructional.md)
> 
> 💡 **Instructor Note:** *The number one bug in BFS implementations is marking a node as visited when it is **dequeued** rather than when it is **enqueued**. Marking upon dequeue allows the same vertex to be enqueued multiple times by different neighbors, exploding queue memory from `O(V)` to `O(E)` and causing severe Time/Memory Limit Exceeded errors.*

---

## 🎯 LEARNING OBJECTIVES

By the end of this chapter, you will be able to:

- **Internalize** Breadth-First Search (BFS) as a radial frontier expansion enforcing strictly monotonic distance ordering.
- **Master** the four core BFS data structures: FIFO Queue, Visited Set/Array, Distance Array (`dist`), and Parent Array (`parent`) for path reconstruction.
- **Trace** BFS executions step-by-step using visual wavefront tables showing queue evolution, array transitions, and branch decisions.
- **Traverse** completely disconnected graphs and count connected components using the outer-loop frontier pattern.
- **Implement** production-grade single-source BFS, unweighted shortest path with parent reconstruction, and multi-source BFS in modern C# (.NET 8/9) and idiomatic Python (3.11+).
- **Diagnose & eliminate** common beginner pitfalls: deferred visited marking, disconnected graph omission, self-loops, parallel edges, and applying BFS to weighted graphs.
- **Deliver** a structured 45-minute technical interview script explaining shortest-path guarantees and multi-source wavefront expansions.

---

## 📖 CHAPTER 1: CONTEXT & MOTIVATION

### The Unweighted Shortest Path Invariant

When edges carry uniform traversal cost (1 hop), finding the minimum steps between states is solved optimally by Breadth-First Search.

1. **Social Degrees of Separation (LinkedIn, Meta):** Discovering mutual connection hops between two members.
2. **Network Routing & Packet Flooding:** Broadcasting configuration packets across adjacent network routers with minimal latency hops.
3. **Puzzles & State Navigation (Word Ladder, Sliding Tiles):** Transitioning from an initial state to a goal state where every move costs exactly 1 unit of effort.
4. **Epidemic / Contagion Modeling (Rotting Oranges, Fire Spread):** Tracking simultaneous spreading phenomena across physical grids via **Multi-Source BFS**.

### Why BFS Guarantees Shortest Paths

A First-In, First-Out (FIFO) queue guarantees that vertices are processed in strictly non-decreasing order of their distance from the source:

```
Distance:   0      1, 1, 1      2, 2, 2, 2      3, 3, ...
Queue:   [ S ] -> [ A, B, C ] -> [ D, E, F, G ] -> [ H, ... ]
```

When vertex `target` is first reached and enqueued from the frontier, no path with fewer hops can possibly exist. If a shorter path existed, the target would have been discovered during an earlier concentric layer.

---

## 🧠 CHAPTER 2: BUILDING THE MENTAL MODEL

### 1. The Concentric Wavefront Model

Think of BFS as dropping a pebble into a calm pool of water. Concentric ripples propagate outward at uniform speed:

```
Concentric Wavefront Expansion:

Layer 0 (dist = 0):                 ( 0 )
                                   /     \
Layer 1 (dist = 1):             ( 1 )   ( 2 )
                                /   \     |
Layer 2 (dist = 2):          ( 3 ) ( 4 )  |
                                \     /  /
Layer 3 (dist = 3):               ( 5 )
```

---

### 2. Concrete Sample Graph & Visual Wavefront Trace Table

To observe the precise interplay of the **Queue**, **Visited Set**, **Distance Array (`dist`)**, and **Parent Array (`parent`)**, consider the following 6-vertex undirected graph:

```
Sample Trace Graph:
          ( 0 )
         /     \
       ( 1 )   ( 2 )
       /   \     |
     ( 3 ) ( 4 ) |
       \     /  /
         ( 5 )

Vertex Set: V = {0, 1, 2, 3, 4, 5}
Source = 0, Target = 5
Adjacency:
  0: [1, 2]
  1: [0, 3, 4]
  2: [0, 5]
  3: [1, 5]
  4: [1, 5]
  5: [2, 3, 4]
```

#### Step-by-Step Execution Trace Table

Initial State:
- `dist` initialized to `[-1, -1, -1, -1, -1, -1]`
- `parent` initialized to `[-1, -1, -1, -1, -1, -1]`
- Seed source `0`: `dist[0] = 0`, `Queue = [ 0 ]`

| Step | Dequeued `u` | `dist[u]` | Inspect Neighbor `v` | Visited Check (`dist[v] == -1`?) | Action Taken | FIFO Queue State | `dist[]` Array `[0,1,2,3,4,5]` | `parent[]` Array `[0,1,2,3,4,5]` |
| :---: | :---: | :---: | :---: | :---: | :---: | :---: | :---: | :---: |
| **0** | — | — | — | — | Seed source `0` | `[ 0 ]` | `[0, -1, -1, -1, -1, -1]` | `[-1, -1, -1, -1, -1, -1]` |
| **1** | **0** | 0 | 1 | Yes (`-1`) | Enqueue 1, `dist[1]=1`, `parent[1]=0` | `[ 1 ]` | `[0, 1, -1, -1, -1, -1]` | `[-1, 0, -1, -1, -1, -1]` |
| | | | 2 | Yes (`-1`) | Enqueue 2, `dist[2]=1`, `parent[2]=0` | `[ 1, 2 ]` | `[0, 1, 1, -1, -1, -1]` | `[-1, 0, 0, -1, -1, -1]` |
| **2** | **1** | 1 | 0 | No (`dist[0]=0`) | Skip (already visited) | `[ 2 ]` | `[0, 1, 1, -1, -1, -1]` | `[-1, 0, 0, -1, -1, -1]` |
| | | | 3 | Yes (`-1`) | Enqueue 3, `dist[3]=2`, `parent[3]=1` | `[ 2, 3 ]` | `[0, 1, 1, 2, -1, -1]` | `[-1, 0, 0, 1, -1, -1]` |
| | | | 4 | Yes (`-1`) | Enqueue 4, `dist[4]=2`, `parent[4]=1` | `[ 2, 3, 4 ]` | `[0, 1, 1, 2, 2, -1]` | `[-1, 0, 0, 1, 1, -1]` |
| **3** | **2** | 1 | 0 | No (`dist[0]=0`) | Skip (already visited) | `[ 3, 4 ]` | `[0, 1, 1, 2, 2, -1]` | `[-1, 0, 0, 1, 1, -1]` |
| | | | 5 | Yes (`-1`) | Enqueue 5, `dist[5]=2`, `parent[5]=2` | `[ 3, 4, 5 ]` | `[0, 1, 1, 2, 2, 2]` | `[-1, 0, 0, 1, 1, 2]` |
| **4** | **3** | 2 | 1 | No (`dist[1]=1`) | Skip | `[ 4, 5 ]` | `[0, 1, 1, 2, 2, 2]` | `[-1, 0, 0, 1, 1, 2]` |
| | | | 5 | No (`dist[5]=2`) | Skip (5 already discovered via 2!) | `[ 4, 5 ]` | `[0, 1, 1, 2, 2, 2]` | `[-1, 0, 0, 1, 1, 2]` |
| **5** | **4** | 2 | 1 | No (`dist[1]=1`) | Skip | `[ 5 ]` | `[0, 1, 1, 2, 2, 2]` | `[-1, 0, 0, 1, 1, 2]` |
| | | | 5 | No (`dist[5]=2`) | Skip (5 already discovered via 2!) | `[ 5 ]` | `[0, 1, 1, 2, 2, 2]` | `[-1, 0, 0, 1, 1, 2]` |
| **6** | **5** | 2 | — | Target Reached! | Break early: shortest path found | `[ ]` | `[0, 1, 1, 2, 2, 2]` | `[-1, 0, 0, 1, 1, 2]` |

#### Path Reconstruction Walkthrough
Starting from target `curr = 5`:
1. `curr = 5` -> add `5` to path. `curr = parent[5] = 2`.
2. `curr = 2` -> add `2` to path. `curr = parent[2] = 0`.
3. `curr = 0` -> add `0` to path. `curr = parent[0] = -1`. Loop terminates.
4. Backwards sequence: `[5, 2, 0]`.
5. Reverse sequence: `[0, 2, 5]`. Minimum distance = `2` hops.

---

### 3. Physical Memory & Queue State Mechanics

```
Queue Frontier Lifecycle:
-----------------------------------------------------------------------------------
Action                Queue Buffer           Visited Array       Distance Array
-----------------------------------------------------------------------------------
Enqueue S             [ S ]                  S: true             S: 0
Dequeue S             [ ]                    -                   -
  Explore neighbors   [ A, B ]               A: true, B: true    A: 1, B: 1
Dequeue A             [ B ]                  -                   -
  Explore neighbors   [ B, C, D ]            C: true, D: true    C: 2, D: 2
-----------------------------------------------------------------------------------
CRITICAL INVARIANT: Vertices inside the queue always have distance values
differing by at most 1: { d, d, ..., d, d+1, d+1, ..., d+1 }.
```

---

## ⚙️ CHAPTER 3: MECHANICS & DUAL-LANGUAGE IMPLEMENTATIONS

### 1. Shortest Path with Parent Reconstruction (Single-Source BFS)

Given an unweighted graph, compute the minimum distance and reconstruct the exact sequence of vertices from `source` to `target`.

#### C# (.NET 8/9) Implementation

```csharp
using System;
using System.Collections.Generic;

public static class BfsShortestPath
{
    public static (int Distance, List<int> Path) FindShortestPath(
        List<int>[] adj, int source, int target)
    {
        ArgumentNullException.ThrowIfNull(adj);
        int n = adj.Length;
        if (source < 0 || source >= n || target < 0 || target >= n)
            throw new ArgumentOutOfRangeException("Source or target vertex out of bounds.");

        if (source == target)
            return (0, [source]);

        var dist = new int[n];
        var parent = new int[n];
        Array.Fill(dist, -1);
        Array.Fill(parent, -1);

        var queue = new Queue<int>();

        // STEP 1: Seed source node
        dist[source] = 0;
        queue.Enqueue(source);

        // STEP 2: Frontier exploration
        while (queue.Count > 0)
        {
            int u = queue.Dequeue();

            if (u == target)
                break; // Early exit: shortest path to target locked

            foreach (int v in adj[u])
            {
                if (dist[v] != -1) continue; // Already visited at smaller or equal distance

                dist[v] = dist[u] + 1;
                parent[v] = u;
                queue.Enqueue(v); // MUST mark visited immediately upon enqueue!
            }
        }

        if (dist[target] == -1)
            return (-1, []); // Target unreachable

        // STEP 3: Reconstruct path backwards from target to source
        var path = new List<int>();
        for (int curr = target; curr != -1; curr = parent[curr])
        {
            path.Add(curr);
        }
        path.Reverse();

        return (dist[target], path);
    }
}
```

#### Python (3.11+) Implementation

```python
from collections import deque
from typing import List, Tuple

def find_shortest_path(
    adj: List[List[int]], source: int, target: int
) -> Tuple[int, List[int]]:
    """Computes unweighted shortest path and reconstructs node sequence via parent pointers."""
    n = len(adj)
    if not (0 <= source < n and 0 <= target < n):
        raise ValueError("Source or target vertex index out of bounds.")

    if source == target:
        return 0, [source]

    dist = [-1] * n
    parent = [-1] * n
    queue = deque([source])
    dist[source] = 0

    while queue:
        u = queue.popleft()

        if u == target:
            break

        for v in adj[u]:
            if dist[v] == -1:  # Unvisited
                dist[v] = dist[u] + 1
                parent[v] = u
                queue.append(v)  # MUST mark visited immediately upon enqueue!

    if dist[target] == -1:
        return -1, []

    # Reconstruct path backwards
    path: List[int] = []
    curr = target
    while curr != -1:
        path.append(curr)
        curr = parent[curr]
    path.reverse()

    return dist[target], path
```

---

### 2. Disconnected Graph Traversal (Connected Components BFS)

A single-source BFS will fail to reach nodes located in isolated subgraphs. To traverse an entire disconnected graph, wrap the BFS in an outer loop over all vertices `0` to `V - 1`.

#### C# (.NET 8/9) Implementation

```csharp
using System;
using System.Collections.Generic;

public static class DisconnectedGraphBfs
{
    public static List<List<int>> GetAllConnectedComponents(List<int>[] adj)
    {
        ArgumentNullException.ThrowIfNull(adj);
        int n = adj.Length;
        var visited = new bool[n];
        var components = new List<List<int>>();

        for (int i = 0; i < n; i++)
        {
            if (visited[i]) continue;

            // Launch a new BFS wavefront for this unvisited component
            var component = new List<int>();
            var queue = new Queue<int>();

            visited[i] = true;
            queue.Enqueue(i);

            while (queue.Count > 0)
            {
                int u = queue.Dequeue();
                component.Add(u);

                foreach (int v in adj[u])
                {
                    if (!visited[v])
                    {
                        visited[v] = true;
                        queue.Enqueue(v);
                    }
                }
            }

            components.Add(component);
        }

        return components;
    }
}
```

#### Python (3.11+) Implementation

```python
from collections import deque
from typing import List

def get_all_connected_components(adj: List[List[int]]) -> List[List[int]]:
    """Traverses an entire disconnected graph, returning each connected component."""
    n = len(adj)
    visited = [False] * n
    components: List[List[int]] = []

    for i in range(n):
        if not visited[i]:
            component: List[int] = []
            queue = deque([i])
            visited[i] = True

            while queue:
                u = queue.popleft()
                component.append(u)

                for v in adj[u]:
                    if not visited[v]:
                        visited[v] = True
                        queue.append(v)

            components.append(component)

    return components
```

---

### 3. Multi-Source BFS (Simultaneous Wavefront Expansion)

When multiple source nodes initiate spread simultaneously (e.g., LeetCode 994: Rotting Oranges, LeetCode 542: 01 Matrix), seed **all** initial sources into the queue at `distance = 0` before starting the loop.

#### C# (.NET 8/9) Implementation

```csharp
using System;
using System.Collections.Generic;

public static class MultiSourceGridBfs
{
    private static readonly int[] Dr = [-1, 1, 0, 0];
    private static readonly int[] Dc = [0, 0, -1, 1];

    public static int ComputeMaxSpreadTime(int[][] grid)
    {
        int rows = grid.Length;
        int cols = grid[0].Length;
        var dist = new int[rows, cols];
        var queue = new Queue<(int R, int C)>();
        int freshCount = 0;

        // STEP 1: Multi-source seeding
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                if (grid[r][c] == 2) // Rotten source
                {
                    queue.Enqueue((r, c));
                    dist[r, c] = 0;
                }
                else
                {
                    dist[r, c] = -1;
                    if (grid[r][c] == 1) freshCount++;
                }
            }
        }

        if (freshCount == 0) return 0;
        int maxTime = 0;

        // STEP 2: Simultaneous radial expansion
        while (queue.Count > 0)
        {
            var (r, c) = queue.Dequeue();

            for (int i = 0; i < 4; i++)
            {
                int nr = r + Dr[i];
                int nc = c + Dc[i];

                if (nr >= 0 && nr < rows && nc >= 0 && nc < cols &&
                    grid[nr][nc] == 1 && dist[nr, nc] == -1)
                {
                    dist[nr, nc] = dist[r, c] + 1;
                    maxTime = Math.Max(maxTime, dist[nr, nc]);
                    grid[nr][nc] = 2; // Infect cell immediately upon enqueue
                    freshCount--;
                    queue.Enqueue((nr, nc));
                }
            }
        }

        return freshCount == 0 ? maxTime : -1;
    }
}
```

#### Python (3.11+) Implementation

```python
from collections import deque
from typing import List

def oranges_rotting(grid: List[List[int]]) -> int:
    """Multi-source BFS spreading infection simultaneously across grid cells."""
    rows, cols = len(grid), len(grid[0])
    queue = deque()
    fresh_count = 0

    # Seed all sources simultaneously at t = 0
    for r in range(rows):
        for c in range(cols):
            if grid[r][c] == 2:
                queue.append((r, c, 0))
            elif grid[r][c] == 1:
                fresh_count += 1

    if fresh_count == 0:
        return 0

    minutes = 0
    directions = ((-1, 0), (1, 0), (0, -1), (0, 1))

    while queue:
        r, c, d = queue.popleft()
        minutes = max(minutes, d)

        for dr, dc in directions:
            nr, nc = r + dr, c + dc
            if 0 <= nr < rows and 0 <= nc < cols and grid[nr][nc] == 1:
                grid[nr][nc] = 2  # Mark infected immediately upon enqueue!
                fresh_count -= 1
                queue.append((nr, nc, d + 1))

    return minutes if fresh_count == 0 else -1
```

---

### 4. Level-by-Level Layer Snapshot Pattern

When problems require tracking discrete discrete layers or levels (e.g., Binary Tree Level Order Traversal, shortest steps in Word Ladder), snapshot the queue's size at the start of each level:

```csharp
int level = 0;
while (queue.Count > 0)
{
    int levelSize = queue.Count; // SNAPSHOT: prevents mixing current level with next level
    for (int i = 0; i < levelSize; i++)
    {
        int u = queue.Dequeue();
        foreach (int v in adj[u])
        {
            if (visited[v]) continue;
            visited[v] = true;
            queue.Enqueue(v);
        }
    }
    level++;
}
```

---

## 📊 CHAPTER 4: COMPLEXITY DECONSTRUCTION

| BFS Variation | Time Complexity | Auxiliary Space | Output Space | Frontier Queue Peak Size |
| :--- | :--- | :--- | :--- | :--- |
| **Standard Graph BFS** | `O(V + E)` | `O(V)` (`visited` + queue) | `O(V)` (`dist` array) | `O(W)` where `W <= V` is max width |
| **Shortest Path + Parent** | `O(V + E)` | `O(V)` (`dist` + `parent`) | `O(L)` where `L <= V` is path length | `O(W)` |
| **Disconnected Graph BFS** | `O(V + E)` | `O(V)` | `O(V)` (component lists) | `O(W)` |
| **Multi-Source Grid BFS** | `O(R * C)` | `O(R * C)` (queue + dist) | `O(1)` or `O(R * C)` | `O(min(R, C))` (diagonal perimeter) |
| **State Space BFS (Word Ladder)** | `O(N * M^2)` | `O(N * M)` (`N` words, length `M`) | `O(1)` (step count) | `O(N)` |

### Queue Frontier Peak Width Analysis

The auxiliary memory of BFS is dominated by the queue's maximum width:
- **Line Graph (Degenerate Chain):** Frontier width is `1`. Memory is `O(1)` queue size.
- **Star Graph (Center node connected to `V - 1` leaves):** At step 1, all `V - 1` nodes enter the queue simultaneously. Frontier width is `O(V)`.
- **2D Grid of size `R * C`:** The wave frontier expands diagonally. Peak queue width is bounded by the perimeter `O(R + C)`.

---

## 🎙️ CHAPTER 5: 45-MINUTE INTERVIEW VERBAL SCRIPT

### Phase 1: Clarification & Constraint Scoping (0–5 Mins)
- **Candidate:** "Before writing code, let me clarify four critical points:
  1. Are edge costs uniform? If edges have variable weights, BFS won't guarantee the shortest path—we'd need Dijkstra's algorithm.
  2. Can the graph contain cycles or disconnected components?
  3. If no path exists from source to target, what should be returned (`-1`, empty list)?
  4. What are the upper bounds on `V` and `E`?"
- **Interviewer:** "Unweighted edges (cost 1 each), cyclic graph possible, return -1 if unreachable, `V, E <= 10^5`."
- **Candidate:** "Because edges have uniform weight, BFS guarantees the minimum edge count in `O(V + E)` time."

### Phase 2: High-Level Approach & Trade-Offs (5–12 Mins)
- **Candidate:** "I will use a FIFO queue, a `dist` array initialized to `-1`, and a `parent` array initialized to `-1`.
  - `dist[u]` serves two purposes: storing the shortest distance and acting as the `visited` tracker (`dist[v] == -1` means unvisited).
  - Crucially, I will mark `dist[v] = dist[u] + 1` **immediately upon enqueuing** `v`. If we defer marking until dequeue, a vertex could be inserted into the queue multiple times, blowing up queue space from `O(V)` to `O(E)`.
  - When we reach `target`, we break early and reconstruct the path backwards via `parent`."

### Phase 3: Live Coding Walkthrough (12–32 Mins)
- **Candidate:** "I'll code the function now. Notice the guard clauses handling out-of-bounds inputs and `source == target`. In the while loop, we dequeue the current vertex `u`. If `u == target`, we break early because BFS guarantees that the first time a node is popped, its shortest distance is found."

### Phase 4: Edge Cases & Verification (32–40 Mins)
- **Candidate:** "Let's trace edge cases:
  1. `source == target`: Returns distance `0` and `[source]` immediately.
  2. Target unreachable (isolated component): Queue empties, `dist[target]` remains `-1`, correctly returns empty path.
  3. Cycle in graph: Handled cleanly because visited nodes are skipped.
  4. Star graph: All leaf nodes enqueued in one batch without duplicates."

---

## 🛠️ CHAPTER 6: COMMON PITFALLS & DECISION FRAMEWORK

### Deep Dive: Beginner Pitfalls & Traps

#### 1. Deferred Visited Marking (Enqueue vs. Dequeue)

```
The Deferred Marking Disaster:
Neighbors A, B, C all connect to node D:

  ( A ) ---+
           |
  ( B ) ---+---> ( D )
           |
  ( C ) ---+

Mark on DEQUEUE (WRONG):
  Pop A: Enqueue D. (Queue: [D])
  Pop B: D is not marked visited yet! Enqueue D again. (Queue: [D, D])
  Pop C: D is still not marked visited! Enqueue D again. (Queue: [D, D, D])
  Memory explodes to O(E) and duplicate work causes TLE/MLE!

Mark on ENQUEUE (CORRECT):
  Pop A: Mark D visited, Enqueue D. (Queue: [D])
  Pop B: D is already marked visited. Skip!
  Pop C: D is already marked visited. Skip!
  Memory strictly bounded by O(V).
```

#### 2. Disconnected Graph Omission
- **The Bug:** Assuming the whole graph can be reached from a single source node `0`.
- **The Consequence:** Unreachable nodes in other components are never visited, leading to wrong answers in connectivity, component counting, or bipartite checking problems.
- **The Fix:** Always use the outer loop `for (int i = 0; i < V; i++)` when traversing an entire graph.

#### 3. Self-Loops & Parallel Edges
- **The Bug:** A node connects to itself (`u -> u`) or has redundant edges (`u -> v` multiple times).
- **The Consequence:** If visited checking is not strictly performed before enqueueing, a self-loop causes vertex `u` to enqueue itself repeatedly in an infinite loop.

#### 4. Applying BFS to Non-Uniform Weighted Graphs
- **The Bug:** Using BFS to find the shortest path when edges have weights (e.g., weights 1 and 10).
- **The Counterexample:** Path A: `0 -> 2` (1 hop, weight 10). Path B: `0 -> 1 -> 2` (2 hops, weights 1 + 1 = 2). BFS finds Path A first because it takes 1 hop, returning cost 10 instead of the optimal cost 2!
- **The Rule:** BFS guarantees shortest paths **only** for unweighted graphs. For non-uniform weights, use **Dijkstra's Algorithm**.

#### 5. Queue Polling Mutation in Level-Order BFS
- **The Bug:** Writing `for (int i = 0; i < queue.Count; i++)` in C# inside the level-processing loop.
- **The Consequence:** Because `queue.Enqueue()` increases `queue.Count` dynamically during the loop, the loop never terminates or processes multiple levels together.
- **The Fix:** Snapshot the count before the inner loop: `int levelSize = queue.Count;`.

---

### Traversal Decision Framework

```
                          [ Graph Traversal Problem ]
                                       |
                 +---------------------+---------------------+
                 |                                           |
         [ Shortest Path? ]                          [ Exhaustive Search / Ordering ]
                 |                                           |
        Edge Weights Uniform?                               DFS / Topo Sort
                 |                                           (Day 03)
        +--------+--------+
        |                 |
     YES: BFS         NO: Non-uniform
     (O(V + E))           |
                 +--------+--------+
                 |                 |
             Weights 0/1?      Weights >= 0?
                 |                 |
             0-1 BFS           Dijkstra (Week 9)
```

---

## 🏋️ PRACTICE LADDER

| Problem | LeetCode # | Difficulty | BFS Pattern | Key Invariant |
| :--- | :--- | :--- | :--- | :--- |
| **Shortest Path in Binary Matrix** | #1091 | 🟡 Medium | 8-Directional Grid BFS | Mark visited immediately upon enqueue |
| **01 Matrix** | #542 | 🟡 Medium | Multi-Source Grid BFS | Seed all `0` cells as initial sources |
| **Rotting Oranges** | #994 | 🟡 Medium | Multi-Source Level BFS | Track fresh count; return elapsed minutes |
| **Word Ladder** | #127 | 🔴 Hard | State Space Implicit BFS | Transform 1 char at a time via dictionary |
| **Open the Lock** | #752 | 🟡 Medium | State Space BFS | Wheel turns as edges; deadends as visited |
| **Bus Routes** | #815 | 🔴 Hard | Dual-Level BFS (Buses & Stops) | Minimize bus transfers, not total stops |

---

> 🧭 **Navigation:** [← Previous Day](Week_08_Day_01_Graph_Models_and_Representations_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_08_Day_03_DFS_Topological_Sort_Instructional.md)
