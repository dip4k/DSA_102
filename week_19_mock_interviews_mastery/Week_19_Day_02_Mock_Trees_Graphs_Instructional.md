# 📘 Week 19, Day 2: Mock Round 2: Trees, BSTs & Graph Traversals

> 🧭 **Navigation:** [← Previous Day](Week_19_Day_01_Mock_Arrays_Strings_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_19_Day_03_Mock_Dynamic_Programming_Instructional.md)
> 
> 💡 **Instructor Note:** *This round tests structural intuition on hierarchical and directed relationships. Pay special attention to Binary Lifting power-of-two jumps and Kahn's algorithm topological sorting with cycle detection.*

---

## 🎯 Learning Objectives

*   **Interview Simulation:** Experience an authentic 45-minute live senior coding interview on tree queries and directed graph dependency resolution.
*   **Dialogue Navigation:** Defend the transition from naive `O(N)` tree path traversals to `O(log N)` binary lifting jumps and handle topological cycles defensively.
*   **3-Tiered Hint Recovery:** Master systematic hint extraction for multi-dimensional tree table states without losing seniority points.
*   **Senior Rubric Mastery:** Evaluate solutions against top-tier criteria: Precomputation Space/Time Trade-offs, Recursion Stack Safeguards, and Cycle Invariant Enforcement.

## ⚖️ FAANG Senior / Lead Interview Calibration

Tree and graph interview problems separate candidates by how defensively they treat cycle invariants, recursion limits, and precomputation trade-offs:

| Pattern | Precomputation Time | Query Time | Auxiliary Memory | Stack Vulnerability | Senior Calibration Bar |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Naive Tree Walk LCA** | `O(1)` | `O(N)` | `O(1)` | High in skewed tree | Reject. Fails repeated query SLAs. |
| **Euler Tour + RMQ (Tarjan)** | `O(N)` / `O(N log N)` | `O(1)` | `O(N log N)` | Moderate | Strong candidate signal; high implementation complexity. |
| **Binary Lifting LCA** | `O(N log N)` | `O(log N)` | `O(N log N)` (~7 MB) | Prevented via iterative DFS | **Senior Standard (L5): Clear, bug-free, bit-level jumps.** |
| **Kahn's Topological Sort** | N/A | `O(V + E)` | `O(V + E)` queue | Zero recursion (BFS queue) | **Senior Standard (L5): Explicit cycle detection invariant.** |
| **DFS 3-Color Cycle Detection**| N/A | `O(V + E)` | `O(V)` call stack | Vulnerable to deep chain SOE | Must note stack limits in Python (`sys.setrecursionlimit`). |

### The Interviewer's Hidden Rubric: What We Listen For
1. **Scale Clarification:** Does the candidate ask whether the tree is static or dynamic before picking an LCA algorithm?
2. **Cycle Detection Invariant:** In topological sort, does the candidate verify that the output count equals `numCourses` rather than assuming the graph is a DAG?
3. **Bitwise Power-of-Two Mechanics:** Does the candidate understand how `(depthDiff & (1 << j)) != 0` lifts a node along its binary representation?

---

## 📖 Chapter 1: Live Interview Simulation & Dialogue Transcript

### Problem Statement
> **Interviewer:** *"We are building a distributed permissions service. Organizations are structured as a tree of `N` nodes (up to `10^5`). We must answer `Q = 10^5` queries of the form: 'Given employees `u` and `v`, find their lowest common manager (Lowest Common Ancestor)' in real time."*

```
Tree Ancestry Visualization:
           (1) [Root CEO]
          /   \
        (2)   (3)
       /   \     \
     (4)   (5)   (6)
    /
  (7)
Query: LCA(7, 5) -> Manager (2)
```

---

### Authentic Interviewer Dialogue Transcript

**Candidate:** "Thank you. Let's clarify the system scale and operational constraints:
1. Is the tree static after construction, or are nodes dynamically added and removed during queries?
2. Is the tree guaranteed to be connected with a single root, or can it be a forest?
3. What is the maximum node ID, and is it 1-indexed or 0-indexed?"

**Interviewer:** "The organization tree is static once loaded. It is a single connected tree rooted at node 1. Node IDs range from 1 to `N`, where `N <= 10^5`. There are `Q = 10^5` queries."

**Candidate:** "Understood. Let's analyze the naive approach first:
For any query `(u, v)`:
- We could compute the depths of both nodes.
- Walk the deeper node upward step-by-step until depths match.
- Walk both nodes upward together step-by-step until they meet at their LCA.
In a skewed tree (e.g., a linked list of `10^5` nodes), walking upward takes `O(N)` steps per query.
Across `Q = 10^5` queries, total time is `O(Q * N) = 10^10` operations—which would take several seconds and fail our low-latency SLA."

**Interviewer:** "That would definitely violate our SLA. How can we accelerate repeated queries on a static tree?"

**Candidate:** "Since the tree is static, we can invest in **preprocessing**.
Instead of stepping upward 1 step at a time, we can jump in **powers of 2**:
We precompute a table `up[u][k]` representing the `2^k`-th ancestor of node `u`.
Because any integer depth difference `D` can be expressed as a unique sum of powers of 2 (its binary representation), we can lift any node by `D` levels in at most `ceil(log2(N))` jumps!
- Preprocessing: `O(N log N)` time and space via DFS and dynamic programming:
  `up[u][k] = up[up[u][k-1]][k-1]`.
- Per Query:
  1. Lift the deeper node to match the shallower node's depth in `O(log N)` steps.
  2. If `u == v`, one was an ancestor of the other; return `u`.
  3. Simultaneously lift both `u` and `v` by descending powers of 2 whenever `up[u][k] != up[v][k]`.
  4. After the loop, `u` and `v` sit directly beneath the LCA. The LCA is `up[u][0]`.
- Query Latency: `O(log N)` = at most 17 operations per query! Total query phase: `< 20ms`."

**Interviewer:** "Very crisp formulation. Please write the code."

---

## 🧭 Chapter 2: 3-Tiered Hint Progression

```
+-----------------------------------------------------------------------------+
| TIER 1: GENTLE NUDGE (Contextual Awareness)                                 |
| "If repeated queries need to jump up a tree, why jump 1 step when you can   |
| jump in powers of 2? How does binary representation help with distances?"   |
+-----------------------------------------------------------------------------+
                                       |
                                       v
+-----------------------------------------------------------------------------+
| TIER 2: STRUCTURAL ANCHOR (Algorithmic Primitive)                           |
| "Define up[u][k] as the 2^k-th ancestor of node u. The 2^k-th ancestor is   |
| the 2^(k-1)-th ancestor of the 2^(k-1)-th ancestor. Precompute this via    |
| dynamic programming during tree traversal."                                 |
+-----------------------------------------------------------------------------+
                                       |
                                       v
+-----------------------------------------------------------------------------+
| TIER 3: TACTICAL CODE HINT (Implementation Mechanics)                        |
| "Set up[u][k] = up[up[u][k-1]][k-1]. To query LCA(u, v): align depths by   |
| jumping the deeper node using bits of (depth[u] - depth[v]). Then loop k    |
| from logN down to 0: if up[u][k] != up[v][k], jump u = up[u][k], v = up[v][k].|
| Finally, return up[u][0]."                                                  |
+-----------------------------------------------------------------------------+
```

---

## 📊 Chapter 3: Senior Evaluation Rubric

| Dimension | Unsatisfactory (Level 1) | Developing (Level 2) | Senior Bar (Level 3) | Staff/Principal Bar (Level 4) |
| :--- | :--- | :--- | :--- | :--- |
| **Problem Formulation** | Proposes recursive tree walk per query without caching; misses SLA timeout. | Recognizes `O(N)` query problem; suggests path array intersection with high overhead. | Derives Binary Lifting `up[u][k]` recurrence independently; defends `O(log N)` query time. | Formulates Binary Lifting immediately; discusses Tarjan's offline LCA (`O(N + Q)`) as alternative. |
| **Boundary Questioning** | Assumes balanced tree; ignores deep chains causing stack overflow. | Asks generic questions without connecting to algorithmic constraints. | Proactively checks tree connectivity, disconnected components, and depth differences. | Implements iterative DFS or increases stack reservation to guard against recursion overflow. |
| **Space/Time Trade-offs** | Fails to quantify memory table size `N * log N`. | Mentions `O(N log N)` but cannot justify why powers of 2 cover all numbers. | Defends `O(N log N)` space table (~7 MB for `N = 10^5`) fitting into L3 cache. | Explains trade-off between Binary Lifting (`O(log N)` online) and Euler Tour RMQ (`O(1)` query). |
| **Code Cleanliness** | Off-by-one errors in `logN` bounds; index out-of-bounds exceptions. | Working code but repeats lifting logic twice with duplicate code blocks. | Clean, concise helper methods (`LiftNode`, `GetLca`), descriptive variable names. | Flawless idiomatic implementation; zero redundant allocations; robust null guards. |

---

## 💻 Chapter 4: Idiomatic Dual-Language Code

### Problem 1: Lowest Common Ancestor via Binary Lifting

#### C# Primary Implementation (.NET 8/9)

```csharp
using System;
using System.Collections.Generic;

namespace MockInterviews.TreesGraphs;

public sealed class BinaryLiftingLca
{
    private readonly int _n;
    private readonly int _logN;
    private readonly int[][] _up;
    private readonly int[] _depth;

    public BinaryLiftingLca(int n, List<int>[] adj, int root = 1)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(n);
        ArgumentNullException.ThrowIfNull(adj);

        _n = n;
        _logN = Math.Max(1, (int)Math.Ceiling(Math.Log2(n + 1)));

        _depth = new int[n + 1];
        _up = new int[n + 1][];
        for (int i = 0; i <= n; i++) _up[i] = new int[_logN + 1];

        // DFS to establish depths and direct parents (up[u][0])
        Dfs(root, root, 0, adj);

        // Precompute powers-of-two ancestors
        for (int j = 1; j <= _logN; j++)
        {
            for (int i = 1; i <= n; i++)
            {
                int intermediate = _up[i][j - 1];
                _up[i][j] = _up[intermediate][j - 1];
            }
        }
    }

    private void Dfs(int u, int p, int d, List<int>[] adj)
    {
        _depth[u] = d;
        _up[u][0] = p;

        foreach (int v in adj[u])
        {
            if (v != p)
            {
                Dfs(v, u, d + 1, adj);
            }
        }
    }

    public int GetLca(int u, int v)
    {
        if (_depth[u] < _depth[v]) (u, v) = (v, u);

        // 1. Lift u to the same depth as v
        int depthDiff = _depth[u] - _depth[v];
        for (int j = _logN; j >= 0; j--)
        {
            if ((depthDiff & (1 << j)) != 0)
            {
                u = _up[u][j];
            }
        }

        if (u == v) return u;

        // 2. Simultaneously lift u and v
        for (int j = _logN; j >= 0; j--)
        {
            if (_up[u][j] != _up[v][j])
            {
                u = _up[u][j];
                v = _up[v][j];
            }
        }

        return _up[u][0];
    }
}
```

#### Python Secondary Implementation (Python 3.11+)

```python
import math
from typing import List

class BinaryLiftingLca:
    """Precomputed Lowest Common Ancestor engine in O(N log N) time and O(log N) query."""
    def __init__(self, n: int, adj: List[List[int]], root: int = 1):
        self.n = n
        self.log_n = max(1, math.ceil(math.log2(n + 1)))
        self.depth = [0] * (n + 1)
        self.up = [[0] * (self.log_n + 1) for _ in range(n + 1)]

        # DFS to compute depths and parent up[u][0]
        self._dfs(root, root, 0, adj)

        # Precompute power-of-two ancestor table
        for j in range(1, self.log_n + 1):
            for i in range(1, n + 1):
                self.up[i][j] = self.up[self.up[i][j - 1]][j - 1]

    def _dfs(self, u: int, p: int, d: int, adj: List[List[int]]) -> None:
        self.depth[u] = d
        self.up[u][0] = p
        for v in adj[u]:
            if v != p:
                self._dfs(v, u, d + 1, adj)

    def get_lca(self, u: int, v: int) -> int:
        if self.depth[u] < self.depth[v]:
            u, v = v, u

        # 1. Lift u to match depth of v
        depth_diff = self.depth[u] - self.depth[v]
        for j in range(self.log_n, -1, -1):
            if (depth_diff >> j) & 1:
                u = self.up[u][j]

        if u == v:
            return u

        # 2. Lift together
        for j in range(self.log_n, -1, -1):
            if self.up[u][j] != self.up[v][j]:
                u = self.up[u][j]
                v = self.up[v][j]

        return self.up[u][0]
```

---

### Problem 2: Topological Sorting & Cycle Detection (Alien Dictionary / Course Schedule)

#### C# Primary Implementation (.NET 8/9)

```csharp
using System;
using System.Collections.Generic;

namespace MockInterviews.TreesGraphs;

public static class GraphSchedules
{
    /// <summary>
    /// Kahn's Algorithm for Topological Sort with explicit cycle detection.
    /// Returns true if a valid linear order exists; otherwise false (cycle detected).
    /// </summary>
    public static bool TryTopologicalSort(int numCourses, int[][] prerequisites, out List<int> order)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(numCourses);
        ArgumentNullException.ThrowIfNull(prerequisites);

        var adj = new List<int>[numCourses];
        for (int i = 0; i < numCourses; i++) adj[i] = new();

        int[] inDegree = new int[numCourses];

        // Build directed graph: prereq[1] -> prereq[0]
        foreach (var edge in prerequisites)
        {
            int dest = edge[0];
            int src = edge[1];
            adj[src].Add(dest);
            inDegree[dest]++;
        }

        // Initialize queue with all vertices having in-degree 0
        var queue = new Queue<int>();
        for (int i = 0; i < numCourses; i++)
        {
            if (inDegree[i] == 0) queue.Enqueue(i);
        }

        order = new List<int>(numCourses);

        while (queue.Count > 0)
        {
            int curr = queue.Dequeue();
            order.Add(curr);

            foreach (int neighbor in adj[curr])
            {
                inDegree[neighbor]--;
                if (inDegree[neighbor] == 0)
                {
                    queue.Enqueue(neighbor);
                }
            }
        }

        // Cycle invariant: if processed count < total vertices, a cycle exists!
        return order.Count == numCourses;
    }
}
```

#### Python Secondary Implementation (Python 3.11+)

```python
from collections import deque
from typing import List, Tuple

def topological_sort(num_courses: int, prerequisites: List[List[int]]) -> Tuple[bool, List[int]]:
    """Kahn's BFS algorithm for Topological Sort with cycle detection in O(V + E) time."""
    adj: List[List[int]] = [[] for _ in range(num_courses)]
    in_degree = [0] * num_courses

    for dest, src in prerequisites:
        adj[src].append(dest)
        in_degree[dest] += 1

    queue = deque([i for i in range(num_courses) if in_degree[i] == 0])
    order: List[int] = []

    while queue:
        curr = queue.popleft()
        order.append(curr)

        for neighbor in adj[curr]:
            in_degree[neighbor] -= 1
            if in_degree[neighbor] == 0:
                queue.append(neighbor)

    is_valid = len(order) == num_courses
    return is_valid, (order if is_valid else [])
```

---

## 🔬 Chapter 5: Explicit Complexity Deconstruction

| Metric | Binary Lifting LCA | Kahn's Topological Sort | Hardware Rationale |
| :--- | :--- | :--- | :--- |
| **Preprocessing Time** | `O(N log N)` | `O(V + E)` graph construction | Memory reads follow contiguous adjacency vectors. |
| **Query Latency** | `O(log N)` | `O(1)` table lookup / check | Fits in standard CPU register and L1 cache loops. |
| **Auxiliary Memory** | `O(N log N)` 2D array | `O(V + E)` queue and degrees | ~7 MB for `N = 10^5`, fits inside server L3 cache. |
| **Recursion Risk** | Guarded DFS / Iterative | `O(1)` recursion (BFS queue) | Queue prevents stack overflow in deep linear chains. |

---

## 🎙️ Chapter 6: 45-Minute Verbal Script & Step-by-Step Timeline

```
[00:00 - 05:00] Clarification & Invariant Grounding
- Clarify whether graph is dynamic or static, 0-indexed or 1-indexed, and max N and Q.
- State the performance bottleneck: naive path walk takes O(N) per query -> O(Q * N) total.

[05:00 - 15:00] Binary Lifting Mathematical Defense
- Propose Binary Lifting: "Because every integer can be represented as sum of powers of 2,
  we can lift any node across any depth in at most log2(N) jumps.
  Table size is N * ceil(log2(N)), requiring ~7 MB for 100,000 nodes."
- Derive the recurrence: up[u][k] = up[up[u][k-1]][k-1].

[15:00 - 30:00] Live Defensive Coding
- Write DFS to populate depths and direct parent pointers.
- Implement nested loops for power-of-two table generation.
- Implement GetLca with two-phase lifting: depth equalization followed by simultaneous jumps.

[30:00 - 40:00] Edge-Case Verification & Dry Run
- Test when u is direct ancestor of v (GetLca returns u immediately after depth equalization).
- Test when u and v are siblings.
- Test root queries and skewed trees.

[40:00 - 45:00] Production Systems Translation
- "In microservices dependency graphs and build pipelines (e.g., Bazel, MSBuild),
  Kahn's algorithm detects circular build dependencies before compiling."
```

---

## 🔍 Chapter 7: Edge-Case Verification

| Test Case | Scenario | Expected Outcome | Invariant Handling |
| :--- | :--- | :--- | :--- |
| **Node is Ancestor of Other** | `u = 1, v = 7` (1 is root) | Returns `1` | Depth equalization lifts `v` to depth 0; `u == v` condition triggers. |
| **Query Same Node** | `u = 5, v = 5` | Returns `5` | Depth difference is 0; returns immediately without jumping. |
| **Deep Linear Skew Tree** | Linked list `1-2-3-...-10^5` | Query in `O(log N)` | Binary lifting handles maximum depth of `10^5` in at most 17 jumps. |
| **Cyclic Dependency in Graph** | Prerequisites `0 -> 1` and `1 -> 0` | Returns `false` | In-degrees never reach 0; processed vertex count `< numCourses`. |
| **Disconnected Graph** | Multiple independent components | Valid component order | Queue absorbs all in-degree 0 vertices across all components. |

---

> 🧭 **Navigation:** [← Previous Day](Week_19_Day_01_Mock_Arrays_Strings_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_19_Day_03_Mock_Dynamic_Programming_Instructional.md)
