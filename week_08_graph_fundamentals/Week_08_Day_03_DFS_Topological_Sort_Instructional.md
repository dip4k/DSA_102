# 📘 Week 8 Day 3: DFS & Topological Sort — Engineering Guide

> 🧭 **Navigation:** [← Previous Day](Week_08_Day_02_Breadth_First_Search_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_08_Day_04_Connectivity_and_Bipartite_Graphs_Instructional.md)
> 
> 💡 **Instructor Note:** *Topological sorting applies exclusively to Directed Acyclic Graphs (DAGs). Master two canonical interview paradigms: (1) 3-State DFS Cycle Detection + Post-Order Reversal, and (2) Kahn's In-Degree Queue Algorithm. In technical interviews, Kahn's algorithm is preferred whenever you must detect cycles without recursion stack limits or when a tie-breaking order (e.g., lexicographical) is required.*

---

## 🎯 LEARNING OBJECTIVES

By the end of this chapter, you will be able to:

- **Formalize** Directed Acyclic Graphs (DAGs) and the prerequisite-respecting linear ordering invariant.
- **Deconstruct** the mechanics of the Call Stack vs. an Explicit Heap Stack, avoiding stack overflow on degenerate linear graphs.
- **Master** Discovery and Finish timestamps (`discovery[u]`, `finish[u]`) and the Parenthesis Theorem for edge classification.
- **Implement** DFS with 3-state coloring (`UNVISITED` / White, `VISITING` / Gray, `VISITED` / Black) to detect directed cycles and generate topological orders.
- **Explain** why a 2-state boolean visited flag fails on diamond DAGs, generating false-positive cycles.
- **Implement** Kahn's in-degree algorithm with FIFO queues and min-heap lexicographical ordering in modern C# (.NET 8/9) and Python (3.11+).
- **Traverse** disconnected directed graphs to ensure every component and forest is validated.
- **Diagnose & avoid** beginner pitfalls: self-loops, parallel edge degree inflation, reversed post-order sequences, and recursion stack overflow.
- **Deliver** a structured 45-minute technical interview script for Course Schedule and dependency build systems.

---

## 📖 CHAPTER 1: CONTEXT & MOTIVATION

### The Problem of Dependency Scheduling

Any system executing tasks with prerequisite constraints is solving a topological sort:
1. **Package Managers (NuGet, npm, pip):** A library cannot be loaded until all its transitive dependencies have initialized.
2. **Build Systems (MSBuild, CMake, Make):** Target binaries cannot link until object files compile. Circular imports indicate invalid build graphs.
3. **Database Transaction Deadlocks:** A directed wait-for graph tracks resource locks; a directed cycle indicates deadlock requiring a transaction rollback.
4. **Academic Course Scheduling (LeetCode 207 & 210):** Students must complete prerequisite courses before taking advanced electives.

### The Topological Invariant

For every directed edge `u -> v`, vertex `u` must appear **before** vertex `v` in the linear ordering:

```
Topological Invariant:
For every edge u ---> v:
Linear Output: [ ... u ... v ... ]
(u is scheduled strictly before v)
```

- If the graph contains a **directed cycle** (`A -> B -> C -> A`), no linear ordering can satisfy all constraints simultaneously.
- A topological sort exists **if and only if** the directed graph is acyclic (a DAG).

---

## 🧠 CHAPTER 2: BUILDING THE MENTAL MODEL

### 1. Edge Classification & The 3-State Coloring Model

When executing DFS on a directed graph, edges fall into four distinct categories:
1. **Tree Edges:** Edges leading to unvisited nodes (`UNVISITED` / White). These form the DFS spanning forest.
2. **Back Edges:** Edges pointing to an active ancestor currently on the recursion call stack (`VISITING` / Gray). **A back edge proves a directed cycle exists.**
3. **Forward Edges:** Non-tree edges pointing from an ancestor to an already-finished descendant (`VISITED` / Black).
4. **Cross Edges:** Edges pointing to an already-finished node in a different, already-explored branch (`VISITED` / Black).

```
The 3-State State Machine:
  [ 0: UNVISITED ] ---> [ 1: VISITING ] ---> [ 2: VISITED ]
       (White)               (Gray)               (Black)
                         On Call Stack        Finished & Popped
                               |
                      Encounter Gray again?
                      ===> CYCLE DETECTED!
```

#### Why a 2-State (Boolean) Flag Fails: The Diamond DAG Trap

Consider a classic "diamond" DAG with 4 nodes: `0 -> 1 -> 3` and `0 -> 2 -> 3`.

```
Diamond DAG (Acyclic!):
       ( 0 )
      /     \
     v       v
   ( 1 )   ( 2 )
      \     /
       v   v
       ( 3 )
```

1. Start DFS at `0`.
2. Visit `1`, then visit `3`.
3. Node `3` has no outgoing edges: it finishes.
4. Backtrack to `0`, then visit `2`.
5. From `2`, inspect edge `2 -> 3`.
- **With a 2-state boolean `visited` array:** `visited[3]` is `true`. A naive algorithm flags `3` as already visited and incorrectly claims a cycle exists!
- **With 3-state coloring:** `state[3]` is `VISITED` (Black). It is **not** on the active call stack (`state[3] != VISITING`). The edge `2 -> 3` is recognized as a harmless **Cross Edge**. The graph is correctly confirmed as acyclic!

---

### 2. Discovery & Finish Times (Timestamping)

By maintaining a global counter `time`, DFS records two critical timestamps for every vertex `u`:
- `discovery[u]`: Timestamp when vertex `u` is first discovered and colored Gray.
- `finish[u]`: Timestamp when all edges incident from `u` have been fully explored and `u` is colored Black.

```
Parenthesis Theorem:
For any two vertices u and v, their intervals [d[u], f[u]] and [d[v], f[v]]
are either:
1. Entirely disjoint: (u and v are in separate branches)
      [ d[u] ... f[u] ]     [ d[v] ... f[v] ]
2. Strictly nested: (v is a descendant of u in the DFS tree)
      [ d[u] ... [ d[v] ... f[v] ] ... f[u] ]
```

**Topological Sort Connection:** In a DAG, for every directed edge `u -> v`, `finish[u] > finish[v]`. Therefore, sorting vertices by **descending finish time** (or appending to a list upon finishing and reversing) produces a valid topological ordering!

---

### 3. Call Stack vs. Explicit Heap Stack

```
Recursion Call Stack:                 Explicit Heap Stack:
Thread Memory (Limited: 1 MB)         Heap Memory (Gigabytes)
+-----------------------+              +-----------------------+
| Frame 3: Dfs(Node 3)  |              | Stack.Push((3, 0))    |
+-----------------------+              +-----------------------+
| Frame 2: Dfs(Node 2)  |              | Stack.Push((2, 1))    |
+-----------------------+              +-----------------------+
| Frame 1: Dfs(Node 1)  |              | Stack.Push((1, 1))    |
+-----------------------+              +-----------------------+
| Frame 0: Dfs(Node 0)  |              | Stack.Push((0, 2))    |
+-----------------------+              +-----------------------+
Vulnerable to StackOverflow            Safe for V = 10^5+ nodes
```

---

### 4. Visualizing Kahn's In-Degree Progression

```
Sample Prerequisite DAG:
       ( 0 ) ------> ( 1 )
         |             |
         |             v
         +---------> ( 2 ) ------> ( 3 )

Edges: 0 -> 1, 0 -> 2, 1 -> 2, 2 -> 3

Kahn's In-Degree Progression:
  Initial In-Degrees: { 0: 0, 1: 1, 2: 2, 3: 1 }
  Queue (In-Degree == 0): [ 0 ]

  Pop 0: Output [ 0 ]
         Decrement neighbors 1 & 2: { 1: 0, 2: 1 } --> Enqueue 1: [ 1 ]
  Pop 1: Output [ 0, 1 ]
         Decrement neighbor 2:      { 2: 0 }       --> Enqueue 2: [ 2 ]
  Pop 2: Output [ 0, 1, 2 ]
         Decrement neighbor 3:      { 3: 0 }       --> Enqueue 3: [ 3 ]
  Pop 3: Output [ 0, 1, 2, 3 ] (All 4 nodes processed -> Valid DAG!)
```

---

## ⚙️ CHAPTER 3: MECHANICS & DUAL-LANGUAGE IMPLEMENTATIONS

### 1. DFS 3-State Cycle Detection & Topological Sort (Recursive)

Vertices are pushed to the output list only upon completion of all outgoing transitions (post-order finish). Reversing this post-order sequence yields a valid topological sort.

#### C# (.NET 8/9) Implementation

```csharp
using System;
using System.Collections.Generic;

public sealed class DfsTopologicalSorter
{
    private const int Unvisited = 0; // White
    private const int Visiting = 1;  // Gray: On active recursion call stack
    private const int Visited = 2;   // Black: Fully explored & finished

    public static (bool HasCycle, int[] Order) Sort(int numCourses, int[][] prerequisites)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(numCourses);
        var adj = new List<int>[numCourses];
        for (int i = 0; i < numCourses; i++) adj[i] = new List<int>();

        foreach (var edge in prerequisites)
        {
            int dest = edge[0];
            int src = edge[1];
            adj[src].Add(dest); // src must precede dest
        }

        var state = new int[numCourses];
        var finishOrder = new List<int>(numCourses);
        bool hasCycle = false;

        void Dfs(int u)
        {
            if (hasCycle) return;
            state[u] = Visiting; // Enter node: mark Gray

            foreach (int v in adj[u])
            {
                if (state[v] == Visiting)
                {
                    hasCycle = true; // Back-edge to an active ancestor -> CYCLE!
                    return;
                }
                if (state[v] == Unvisited)
                {
                    Dfs(v);
                }
            }

            state[u] = Visited; // Leave node: mark Black
            finishOrder.Add(u); // Post-order append
        }

        // Outer loop guarantees processing all disconnected components
        for (int i = 0; i < numCourses; i++)
        {
            if (state[i] == Unvisited)
            {
                Dfs(i);
                if (hasCycle) return (true, []);
            }
        }

        finishOrder.Reverse(); // Post-order reversed produces topological ordering
        return (false, finishOrder.ToArray());
    }
}
```

#### Python (3.11+) Implementation

```python
from typing import List, Tuple

def dfs_topological_sort(num_courses: int, prerequisites: List[List[int]]) -> Tuple[bool, List[int]]:
    """DFS 3-state coloring topological sort with cycle detection."""
    UNVISITED, VISITING, VISITED = 0, 1, 2
    
    adj: List[List[int]] = [[] for _ in range(num_courses)]
    for dest, src in prerequisites:
        adj[src].append(dest)

    state = [UNVISITED] * num_courses
    finish_order: List[int] = []
    has_cycle = False

    def dfs(u: int) -> None:
        nonlocal has_cycle
        if has_cycle:
            return
        state[u] = VISITING  # Enter node: Gray

        for v in adj[u]:
            if state[v] == VISITING:
                has_cycle = True  # Back-edge detected!
                return
            if state[v] == UNVISITED:
                dfs(v)

        state[u] = VISITED  # Finished node: Black
        finish_order.append(u)

    # Outer loop handles disconnected components and forests
    for i in range(num_courses):
        if state[i] == UNVISITED:
            dfs(i)
            if has_cycle:
                return True, []

    finish_order.reverse()
    return False, finish_order
```

---

### 2. Explicit Stack Iterative DFS (Heap-Safe DFS)

To eliminate recursion stack overflow risks on deep chain topologies (`V = 10^5`), simulate the call stack manually using an explicit stack on the heap.

#### C# (.NET 8/9) Implementation

```csharp
using System;
using System.Collections.Generic;

public static class ExplicitStackDfsSorter
{
    private const int Unvisited = 0;
    private const int Visiting = 1;
    private const int Visited = 2;

    public static (bool HasCycle, int[] Order) Sort(int numCourses, int[][] prerequisites)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(numCourses);
        var adj = new List<int>[numCourses];
        for (int i = 0; i < numCourses; i++) adj[i] = new List<int>();

        foreach (var edge in prerequisites)
        {
            int dest = edge[0];
            int src = edge[1];
            adj[src].Add(dest);
        }

        var state = new int[numCourses];
        var finishOrder = new List<int>(numCourses);

        for (int i = 0; i < numCourses; i++)
        {
            if (state[i] != Unvisited) continue;

            // Stack stores (Node, NextNeighborIndex) to resume loops on backtrack
            var stack = new Stack<(int Vertex, int EdgeIdx)>();
            state[i] = Visiting;
            stack.Push((i, 0));

            while (stack.Count > 0)
            {
                var (u, edgeIdx) = stack.Pop();

                if (edgeIdx < adj[u].Count)
                {
                    // Re-push u with incremented neighbor pointer
                    stack.Push((u, edgeIdx + 1));
                    int v = adj[u][edgeIdx];

                    if (state[v] == Visiting)
                    {
                        return (true, []); // Back-edge detected!
                    }
                    if (state[v] == Unvisited)
                    {
                        state[v] = Visiting;
                        stack.Push((v, 0));
                    }
                }
                else
                {
                    // All outgoing edges from u are finished
                    state[u] = Visited;
                    finishOrder.Add(u);
                }
            }
        }

        finishOrder.Reverse();
        return (false, finishOrder.ToArray());
    }
}
```

#### Python (3.11+) Implementation

```python
from typing import List, Tuple

def explicit_stack_dfs_sort(num_courses: int, prerequisites: List[List[int]]) -> Tuple[bool, List[int]]:
    """Iterative 3-state DFS using an explicit heap stack to prevent recursion overflow."""
    adj: List[List[int]] = [[] for _ in range(num_courses)]
    for dest, src in prerequisites:
        adj[src].append(dest)

    UNVISITED, VISITING, VISITED = 0, 1, 2
    state = [UNVISITED] * num_courses
    finish_order: List[int] = []

    for i in range(num_courses):
        if state[i] == UNVISITED:
            # Stack stores [vertex, next_neighbor_index]
            stack = [(i, 0)]
            state[i] = VISITING

            while stack:
                u, edge_idx = stack[-1]

                if edge_idx < len(adj[u]):
                    stack[-1] = (u, edge_idx + 1)
                    v = adj[u][edge_idx]

                    if state[v] == VISITING:
                        return True, []  # Back-edge detected!
                    elif state[v] == UNVISITED:
                        state[v] = VISITING
                        stack.append((v, 0))
                else:
                    stack.pop()
                    state[u] = VISITED
                    finish_order.append(u)

    finish_order.reverse()
    return False, finish_order
```

---

### 3. Discovery & Finish Time Timestamping DFS

Tracking arrival and departure timestamps enables formal edge classification (Tree, Back, Forward, and Cross edges).

#### Dual-Language Implementation

```csharp
public static class DfsTimestampTracker
{
    public static (int[] Discovery, int[] Finish) ComputeTimestamps(int n, List<int>[] adj)
    {
        var discovery = new int[n];
        var finish = new int[n];
        Array.Fill(discovery, -1);
        Array.Fill(finish, -1);
        int timer = 0;

        void Dfs(int u)
        {
            discovery[u] = timer++;
            foreach (int v in adj[u])
            {
                if (discovery[v] == -1)
                {
                    Dfs(v);
                }
            }
            finish[u] = timer++;
        }

        for (int i = 0; i < n; i++)
        {
            if (discovery[i] == -1) Dfs(i);
        }

        return (discovery, finish);
    }
}
```

```python
from typing import List, Tuple

def dfs_compute_timestamps(n: int, adj: List[List[int]]) -> Tuple[List[int], List[int]]:
    """Records discovery and finish timestamps for all vertices."""
    discovery = [-1] * n
    finish = [-1] * n
    timer = 0

    def dfs(u: int) -> None:
        nonlocal timer
        discovery[u] = timer
        timer += 1
        for v in adj[u]:
            if discovery[v] == -1:
                dfs(v)
        finish[u] = timer
        timer += 1

    for i in range(n):
        if discovery[i] == -1:
            dfs(i)

    return discovery, finish
```

---

### 4. Kahn's Algorithm (In-Degree Queue BFS) & Lexicographical Ordering

Iterative topological sort using a FIFO queue of vertices with `in_degree == 0`. If the count of processed vertices is less than `V`, a cycle exists.

#### C# (.NET 8/9) Implementation

```csharp
using System;
using System.Collections.Generic;

public static class KahnsTopologicalSorter
{
    public static (bool HasCycle, int[] Order) Sort(int numCourses, int[][] prerequisites)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(numCourses);
        var adj = new List<int>[numCourses];
        for (int i = 0; i < numCourses; i++) adj[i] = new List<int>();

        var inDegree = new int[numCourses];
        foreach (var edge in prerequisites)
        {
            int dest = edge[0];
            int src = edge[1];
            adj[src].Add(dest);
            inDegree[dest]++;
        }

        var queue = new Queue<int>();
        for (int i = 0; i < numCourses; i++)
        {
            if (inDegree[i] == 0)
                queue.Enqueue(i);
        }

        var order = new int[numCourses];
        int processedCount = 0;

        while (queue.Count > 0)
        {
            int u = queue.Dequeue();
            order[processedCount++] = u;

            foreach (int v in adj[u])
            {
                inDegree[v]--;
                if (inDegree[v] == 0)
                {
                    queue.Enqueue(v);
                }
            }
        }

        return processedCount == numCourses ? (false, order) : (true, []);
    }

    // Lexicographically smallest topological sort using PriorityQueue
    public static (bool HasCycle, int[] Order) SortLexicographical(int numCourses, int[][] prerequisites)
    {
        var adj = new List<int>[numCourses];
        for (int i = 0; i < numCourses; i++) adj[i] = new List<int>();

        var inDegree = new int[numCourses];
        foreach (var edge in prerequisites)
        {
            adj[edge[1]].Add(edge[0]);
            inDegree[edge[0]]++;
        }

        var pq = new PriorityQueue<int, int>();
        for (int i = 0; i < numCourses; i++)
        {
            if (inDegree[i] == 0) pq.Enqueue(i, i); // Min-heap by node ID
        }

        var order = new int[numCourses];
        int processedCount = 0;

        while (pq.Count > 0)
        {
            int u = pq.Dequeue();
            order[processedCount++] = u;

            foreach (int v in adj[u])
            {
                inDegree[v]--;
                if (inDegree[v] == 0) pq.Enqueue(v, v);
            }
        }

        return processedCount == numCourses ? (false, order) : (true, []);
    }
}
```

#### Python (3.11+) Implementation

```python
import heapq
from collections import deque
from typing import List, Tuple

def kahns_topological_sort(num_courses: int, prerequisites: List[List[int]]) -> Tuple[bool, List[int]]:
    """Kahn's in-degree queue BFS algorithm for topological sort and cycle detection."""
    adj: List[List[int]] = [[] for _ in range(num_courses)]
    in_degree = [0] * num_courses

    for dest, src in prerequisites:
        adj[src].append(dest)
        in_degree[dest] += 1

    queue = deque(i for i in range(num_courses) if in_degree[i] == 0)
    order: List[int] = []

    while queue:
        u = queue.popleft()
        order.append(u)

        for v in adj[u]:
            in_degree[v] -= 1
            if in_degree[v] == 0:
                queue.append(v)

    if len(order) == num_courses:
        return False, order
    return True, []

def kahns_lexicographical_sort(num_courses: int, prerequisites: List[List[int]]) -> Tuple[bool, List[int]]:
    """Kahn's algorithm using min-heap for lexicographically smallest order."""
    adj: List[List[int]] = [[] for _ in range(num_courses)]
    in_degree = [0] * num_courses

    for dest, src in prerequisites:
        adj[src].append(dest)
        in_degree[dest] += 1

    pq = [i for i in range(num_courses) if in_degree[i] == 0]
    heapq.heapify(pq)
    order: List[int] = []

    while pq:
        u = heapq.heappop(pq)
        order.append(u)

        for v in adj[u]:
            in_degree[v] -= 1
            if in_degree[v] == 0:
                heapq.heappush(pq, v)

    if len(order) == num_courses:
        return False, order
    return True, []
```

---

## 📊 CHAPTER 4: COMPLEXITY DECONSTRUCTION

| Algorithm | Time Complexity | Auxiliary Space | Output Space | Stack Overflow Risk |
| :--- | :--- | :--- | :--- | :--- |
| **DFS 3-State (Recursive)** | `O(V + E)` | `O(V)` (`state` + Call Stack) | `O(V)` (`order`) | High (Linear chain crashes default thread stack) |
| **DFS 3-State (Explicit Stack)** | `O(V + E)` | `O(V)` (`state` + Heap Stack) | `O(V)` (`order`) | Zero (Heap allocated) |
| **Kahn's Algorithm (FIFO Queue)** | `O(V + E)` | `O(V)` (`in_degree` + Queue) | `O(V)` (`order`) | Zero (Purely iterative) |
| **Kahn's Algorithm (Min-Heap)** | `O(V log V + E)` | `O(V)` (`in_degree` + Heap) | `O(V)` (`order`) | Zero (Purely iterative) |

### Mechanical Comparison: Kahn's vs. DFS

1. **Stack Safety:** In languages with restricted default thread stacks (Python default recursion limit 1,000; Windows C# 1 MB stack), a degenerate line DAG with `10^5` nodes will cause a stack overflow during recursive DFS. Kahn's uses heap memory via a queue and cannot overflow.
2. **Lexicographical Constraints:** If an interview requires returning the lexicographically smallest topological order, Kahn's algorithm trivially accommodates this by replacing the FIFO queue with a min-heap (`PriorityQueue` in C# / `heapq` in Python). Doing this in DFS requires sorting adjacency lists in reverse, which is less natural.
3. **Cycle Discovery Moment:** Kahn's detects cycles when the queue empties before all vertices are visited (`processedCount < V`). DFS detects cycles the moment a back-edge to a `VISITING` vertex is touched.

---

## 🎙️ CHAPTER 5: 45-MINUTE INTERVIEW VERBAL SCRIPT

### Phase 1: Clarification & Constraint Scoping (0–5 Mins)
- **Candidate:** "Before writing code, let's clarify the relationship semantics:
  1. For each pair `[a, b]`, does `b -> a` represent taking `b` before `a`?
  2. If the graph contains a cycle, what should be returned (empty array)?
  3. If multiple valid topological orders exist, does any valid order suffice?
  4. What are the constraints on `V` (courses) and `E` (dependencies)?"
- **Interviewer:** "`b` must be taken before `a`. Return an empty array if invalid. Any order is acceptable. `V <= 10^5, E <= 2 * 10^5`."
- **Candidate:** "This is a Directed Acyclic Graph topological sort problem. I can use either DFS with 3-state coloring or Kahn's in-degree BFS algorithm.
  - With `V = 10^5`, a long dependency chain could trigger stack overflow in Python or .NET without increased stack space.
  - Therefore, I recommend **Kahn's algorithm**. It is purely iterative, uses an array for in-degrees and a queue for frontier exploration, runs in `O(V + E)` time, and uses `O(V)` auxiliary space."

### Phase 2: High-Level Approach & Trade-Offs (5–12 Mins)
- **Candidate:** "I'll implement Kahn's algorithm:
  1. Construct the adjacency list and compute `inDegree[dest]` for each prerequisite.
  2. Enqueue all vertices with `inDegree == 0`.
  3. Dequeue vertex `u`, add it to `order`, and decrement `inDegree[v]` for all neighbors. If any neighbor reaches `0`, enqueue it.
  4. Finally, if `processedCount == numCourses`, return `order`; otherwise, a cycle exists, so return an empty array."

### Phase 3: Live Coding Walkthrough (12–32 Mins)
- **Candidate:** "I'll write the code now. Notice the input edge inversion: prerequisites are specified as `[course, prereq]`, meaning the directed edge is `prereq -> course`. In the BFS loop, decrementing `inDegree` simulates stripping finished dependencies from the graph."

### Phase 4: Edge Cases & Verification (32–40 Mins)
- **Candidate:** "Let's dry-run edge cases:
  1. **Graph with no edges (`E = 0`):** All nodes have in-degree 0; all are enqueued immediately; returns `[0, 1, ..., V-1]`.
  2. **Disconnected components:** Handled naturally; all component source nodes are enqueued at step 1.
  3. **Mutual cycle (`0 -> 1 -> 0`):** Neither reaches in-degree 0; queue remains empty; returns `[]`.
  4. **Self-loop (`0 -> 0`):** In-degree is 1; never enqueued; returns `[]`."

---

## 🛠️ CHAPTER 6: COMMON PITFALLS & DECISION FRAMEWORK

### Deep Dive: Beginner Pitfalls & Traps

#### 1. The 2-State (Boolean) Visited Trap on Diamond DAGs
- **The Bug:** Using a boolean `visited[u]` array in DFS cycle detection.
- **The Consequence:** When multiple paths converge on a common descendant (diamond pattern), the second path visits an already-visited node and wrongly flags a cycle.
- **The Fix:** Strictly use **3 states** (`UNVISITED`, `VISITING`, `VISITED`). Only a back-edge to a `VISITING` node indicates a cycle.

#### 2. Disconnected Graphs & Forest Cycles
- **The Bug:** Calling DFS or Kahn's starting only from node `0`.
- **The Consequence:** If a disconnected component has a cycle (e.g. `2 -> 3 -> 2`), but node `0` cannot reach it, the algorithm wrongly concludes the entire graph is acyclic!
- **The Fix:** Outer loop over all nodes `0` to `V - 1`.

#### 3. Self-Loops (`u -> u`)
- **The Bug:** An edge points from a node to itself.
- **The Consequence:** A self-loop is a directed cycle of length 1. In Kahn's algorithm, `inDegree[u]` is at least 1, so `u` never enters the queue. In DFS, `state[u] == VISITING` triggers immediate cycle detection.

#### 4. Parallel Edges Inflating In-Degrees
- **The Bug:** Input contains duplicate edges `(u, v)` multiple times.
- **The Consequence:** `inDegree[v]` is incremented twice for the same dependency. When `u` is processed once, `inDegree[v]` only decrements by 1 and never hits 0, wrongly flagging a cycle!
- **The Fix:** Either deduplicate input edges using a hash set, or filter adjacent duplicates during graph construction.

#### 5. Missing Post-Order Reversal in DFS
- **The Bug:** Appending finished nodes to `order` and returning without reversing.
- **The Consequence:** Nodes with out-degree 0 (sinks) finish first and end up at the beginning of the list, producing a reversed topological sort!
- **The Fix:** Always reverse the finished list or prepend elements to the output.

#### 6. Recursion Stack Overflow on Linear DAGs
- **The Bug:** Calling recursive DFS when `V = 10^5` on a chain graph `0 -> 1 -> 2 -> ... -> 10^5`.
- **The Consequence:** Exceeds the default thread stack and crashes the process.
- **The Fix:** Use Kahn's algorithm or an explicit heap stack.

---

### Decision Framework

```
                          [ Directed Graph Problem ]
                                       |
                 +---------------------+---------------------+
                 |                                           |
          [ Cycle Detection ]                         [ Linear Ordering ]
                 |                                           |
      Is recursion stack deep?                       Need Min/Max Order?
         +-------+-------+                               +-------+-------+
         |               |                               |               |
        YES              NO                             YES              NO
         |               |                               |               |
      Kahn's BFS      DFS 3-State                    Kahn's + Heap    Kahn's BFS
       (O(V+E))        (O(V+E))                       (O(V log V))     or DFS
```

---

## 🏋️ PRACTICE LADDER

| Problem | LeetCode # | Difficulty | Pattern | Primary Invariant |
| :--- | :--- | :--- | :--- | :--- |
| **Course Schedule** | #207 | 🟡 Medium | Cycle Detection | Directed cycle implies impossible schedule |
| **Course Schedule II** | #210 | 🟡 Medium | Topological Sort | Kahn's queue or DFS post-order reversal |
| **Alien Dictionary** | #269 | 🔴 Hard | Graph Build + Topo Sort | Prefix mismatch invalidation + Kahn's |
| **Sequence Reconstruction**| #444 | 🟡 Medium | Unique Topo Sort | Queue size must remain exactly 1 at all steps |
| **Minimum Height Trees** | #310 | 🟡 Medium | In-Degree Trimming | Prune degree-1 leaves inward until <= 2 roots remain |

---

> 🧭 **Navigation:** [← Previous Day](Week_08_Day_02_Breadth_First_Search_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_08_Day_04_Connectivity_and_Bipartite_Graphs_Instructional.md)
