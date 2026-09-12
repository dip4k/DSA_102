# Phase 23: Amazon, Google & Uber Signature Questions

> **Focus:** Multi-Source BFS wavefronts, binary search on continuous answer spaces, nested stack state-machines, dual-graph route hyper-edges, LCA path compression, monotonic contribution calculus, lexicographical prefix search, combinatorial tuple aggregation, and pointer interweaving.  
> **Source Curriculum:** Senior Algorithmic Mastery Curriculum — Phase 23 (Problems #133–#141)

---

## 133. Rotting Oranges (LeetCode #994)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core (Amazon / Google / Uber flagship) |
| **Pattern Tags** | `#multi-source-bfs` `#matrix-traversal` `#breadth-first-search` `#shortest-path` |
| **LeetCode Link** | [Rotting Oranges](https://leetcode.com/problems/rotting-oranges/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** You are given an $m \times n$ grid where each cell has one of three values:
  - `0` representing an empty cell,
  - `1` representing a fresh orange, or
  - `2` representing a rotten orange.
  Every minute, any fresh orange that is 4-directionally adjacent to a rotten orange becomes rotten. Return the minimum number of minutes that must elapse until no cell has a fresh orange. If this is impossible, return `-1`.
- **Assumptions & Contracts:**
  - Adjacency is strictly 4-directional (North, South, East, West); diagonals do not transmit rot.
  - All initially rotten oranges begin spreading rot simultaneously at $t = 0$.
  - Cells labeled `0` are inert obstacles that do not rot and block rot propagation.
- **Key Constraints:**
  - $m == grid.Length$
  - $n == grid[i].Length$
  - $1 \le m, n \le 10$
  - $grid[i][j]$ is `0`, `1`, or `2`.
- **Senior Edge Cases to Defend:**
  - **Zero Fresh Oranges Initially:** $grid$ contains only `0`s and `2`s. Rot propagation terminates at $t = 0$; return `0` immediately.
  - **No Rotten Oranges with Fresh Present:** Fresh oranges exist, but zero rotten oranges exist. Spread cannot initiate; return `-1`.
  - **Unreachable Fresh Orange (Isolated Island):** A fresh orange is bounded entirely by `0`s or edges such that rot cannot reach it; return `-1`.
  - **Simultaneous Wavefront Collision:** Two distinct rotten oranges reach the same fresh orange at the same minute layer. Must be enqueued only once to avoid duplicate queue saturation.
  - **Single Cell Matrix ($1 \times 1$):** `[[0]]` $\implies 0$, `[[1]]` $\implies -1$, `[[2]]` $\implies 0$.

---

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Multi-source Breadth-First Search (BFS) over an unweighted planar grid graph. Rot propagates like a parallel wavefront. The minimum time to rot all oranges corresponds to the maximum BFS depth required to reach every node with initial value `1`.
- **Sample 1:**
  - **Input:** `grid = [[2,1,1],[1,1,0],[0,1,1]]`
  - **Output:** `4`
  - **Explanation:**
    - Minute 0: `[[2,1,1],[1,1,0],[0,1,1]]` (Rotten at (0,0))
    - Minute 1: `[[2,2,1],[2,1,0],[0,1,1]]`
    - Minute 2: `[[2,2,2],[2,2,0],[0,1,1]]`
    - Minute 3: `[[2,2,2],[2,2,0],[0,2,1]]`
    - Minute 4: `[[2,2,2],[2,2,0],[0,2,2]]` (All oranges rotten; total elapsed = 4).
- **Sample 2:**
  - **Input:** `grid = [[2,1,1],[0,1,1],[1,0,1]]`
  - **Output:** `-1`
  - **Explanation:** The orange at cell `(2, 0)` is completely isolated from all rotten oranges by empty cells `0`.

---

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine forest fires ignited at multiple points simultaneously in a dense grid forest. Every minute, fire leaps exactly one unit in the 4 cardinal directions. You do not simulate one fire, reset time, and simulate the second fire. Instead, you drop all fire ignition coordinates into a single queue at minute 0. You process the fire layer by layer (level-order traversal). Each complete round of burning represents one unit of wall-clock time. If green trees remain when the fire runs out of fuel, total combustion was impossible.

#### 3.2 The Naive Bottleneck & Redundant Computation
A naive approach might run an independent single-source BFS from each rotten orange to find the shortest distance to every fresh orange, then compute the minimum distance for each fresh orange, and finally take the maximum over all fresh oranges:
$$\text{Time} = O(R \times (M \times N))$$
where $R$ is the count of rotten oranges. When $R = O(M \times N)$, this degrades to $O((M \times N)^2)$. Moreover, tracking per-cell minimum distances across multiple passes introduces unnecessary auxiliary storage and complex book-keeping.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
By enqueuing **all initial rotten oranges simultaneously** into the BFS queue prior to advancing the clock, we establish the **Equidistant Wavefront Invariant**:
$$\forall u \in \text{Queue at level } k: \text{dist}(\text{Sources}, u) = k$$
Because BFS explores nodes in non-decreasing order of distance from the source set, the first time any fresh orange is visited by the wavefront is guaranteed to be its globally earliest infection timestamp. Each cell is touched at most once, reducing the runtime from $O((M \times N)^2)$ to strictly $O(M \times N)$.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
Multi-Source BFS Level Architecture:
Queue at Minute t: [ Rotten Node A, Rotten Node B, Rotten Node C ] (Count = K)
                      │               │               │
                      ▼               ▼               ▼
           Infect 4-Neighbors   Infect 4-Neighbors   Infect 4-Neighbors
                      │               │               │
                      └───────┬───────┴───────┬───────┘
                              ▼               ▼
Queue at Minute t+1: [ Newly Infected Fresh Oranges ] (Count = K')
In-Place Mutation  : grid[r][c] = 2 immediately upon enqueue
Invariant          : freshCount decremented immediately upon enqueue
```

#### 3.5 State Transition Triggers & Decision Gates
1. **Initialization Gate:** Scan grid once. For every cell:
   - If $grid[r][c] == 2$: `queue.Enqueue((r, c))`
   - If $grid[r][c] == 1$: `freshCount++`
2. **Pre-Flight Short Circuit:** If `freshCount == 0`, return `0` immediately.
3. **Wavefront Loop:** While `queue.Count > 0 && freshCount > 0`:
   - Let `levelSize = queue.Count`.
   - Iterate `levelSize` times:
     - Dequeue `(r, c)`.
     - For each neighbor `(nr, nc)` in `[(-1,0), (1,0), (0,-1), (0,1)]`:
       - If `0 <= nr < m && 0 <= nc < n && grid[nr][nc] == 1`:
         - `grid[nr][nc] = 2` (Mark rotten to prevent duplicate enqueuing)
         - `freshCount--`
         - `queue.Enqueue((nr, nc))`
   - `minutesElapsed++`
4. **Terminal Evaluation:** Return `freshCount == 0 ? minutesElapsed : -1`.

#### 3.6 Concrete Step-by-Step State Trace
Input: `grid = [[2,1,1],[1,1,0],[0,1,1]]`, $m=3, n=3$.
- Initial Scan: Rotten queue: `[(0,0)]`, `freshCount = 6`, `minutesElapsed = 0`.

| Minute ($t$) | Nodes Processed this Layer | Infected Neighbors Enqueued | Grid State After Step | Remaining `freshCount` | Queue State for Next Layer |
| :---: | :--- | :--- | :--- | :---: | :--- |
| **0** | `(0, 0)` | `(0, 1)`, `(1, 0)` | `[[2,2,1],[2,1,0],[0,1,1]]` | 4 | `[(0,1), (1,0)]` |
| **1** | `(0, 1)`, `(1, 0)` | From (0,1): `(0,2)`, `(1,1)`<br>From (1,0): none (1,1 already infected) | `[[2,2,2],[2,2,0],[0,1,1]]` | 2 | `[(0,2), (1,1)]` |
| **2** | `(0, 2)`, `(1, 1)` | From (0,2): none<br>From (1,1): `(2,1)` | `[[2,2,2],[2,2,0],[0,2,1]]` | 1 | `[(2,1)]` |
| **3** | `(2, 1)` | From (2,1): `(2,2)` | `[[2,2,2],[2,2,0],[0,2,2]]` | 0 | `[(2,2)]` |
| **4** | Loop terminates (`freshCount == 0`) | None | Unchanged | 0 | Empty |

Final result: `freshCount == 0` $\implies$ return `4`.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Multi-Source BFS with In-Place Grid Modification:** We use the input matrix itself as the visited set by overwriting `1` with `2`. This achieves optimal $O(1)$ extra spatial overhead beyond the BFS queue.
- **Why BFS over DFS:** DFS explores deep single branches, which fails to capture the simultaneous, uniform physical time expansion of the rot. Finding minimum time via DFS requires calculating all-pairs shortest paths or tracking minimal global timestamps per cell, causing redundant work.

#### 4.2 Step-by-Step Natural Progression Flow
1. Scan the grid to seed the queue with all initial sources and count total fresh oranges.
2. If fresh count is 0, exit immediately with 0.
3. Process the queue level-by-level (each level = 1 minute). Only increment minutes if at least one fresh orange was infected.
4. Return time if all fresh oranges rotted; otherwise return -1.

#### 4.3 Alternative Approaches Analysis
- **Independent Single-Source BFS:** Run BFS from each rotten orange, taking cell-wise minimum over all sources. Runtime $O(R \cdot M \cdot N)$, Space $O(M \cdot N)$. Strictly inferior to multi-source BFS.
- **Cell-by-Cell Simulation (Brute Force):** In each minute, scan all $M \times N$ cells to find fresh oranges adjacent to rotten ones, marking them with temporary state `3`, then convert `3` to `2`. Repeat until no new oranges rot. Time: $O(K \cdot M \cdot N)$ where $K \le M \cdot N$, yielding $O((M \cdot N)^2)$.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Approach | Time (Best) | Time (Avg) | Time (Worst) | Aux Space | Output Space | Cache Locality | In-Place Mutability | Streaming Suitability |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **Multi-Source BFS (Optimal)** | $O(M \cdot N)$ | $O(M \cdot N)$ | $O(M \cdot N)$ | $O(M \cdot N)$ (Queue) | $O(1)$ | High | Mutates $grid$ to mark visited | Low (requires full matrix) |
| **Simulation by Passes** | $O(M \cdot N)$ | $O(M^2 \cdot N^2)$ | $O(M^2 \cdot N^2)$ | $O(1)$ | $O(1)$ | Moderate | Mutates $grid$ | Low |
| **All-Pairs Shortest Path** | $O((MN)^2)$ | $O((MN)^2)$ | $O((MN)^2)$ | $O(M \cdot N)$ | $O(1)$ | Low | Non-mutating | Low |

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: LeetCode #994 - Rotting Oranges
 * ============================================================================
 * Core Pattern      : Multi-Source Breadth-First Search (Level-Order Wavefront)
 * Time Complexity   : O(M * N) - Every cell visited at most a constant number of times
 * Space Complexity  : O(M * N) - Queue holds at most all cells in the grid
 * Selection Rule    : Standard shortest path on unweighted grid with multiple concurrent sources.
 * Defensive Traps   : 1. Avoid incrementing minutes when queue has remaining nodes but no fresh oranges infected.
 *                     2. Check freshCount == 0 initially to return 0 immediately.
 *                     3. Mark grid[nr][nc] = 2 immediately upon enqueuing to prevent duplicate inserts.
 * ============================================================================
 */

using System;
using System.Collections.Generic;

public class Solution
{
    /// <summary>
    /// Computes the minimum minutes until no fresh oranges remain using Multi-Source BFS.
    /// </summary>
    /// <param name="grid">2D integer matrix representing empty, fresh, and rotten cells.</param>
    /// <returns>Minimum minutes elapsed, or -1 if impossible.</returns>
    public int OrangesRotting(int[][] grid)
    {
        // Guard Clause: Validate grid non-nullability and non-emptiness
        if (grid == null || grid.Length == 0 || grid[0].Length == 0)
        {
            return 0;
        }

        int rows = grid.Length;
        int cols = grid[0].Length;
        int freshCount = 0;
        var queue = new Queue<(int R, int C)>();

        // Phase 1: Ingestion & Multi-Source Frontier Seeding
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                if (grid[r][c] == 2)
                {
                    queue.Enqueue((r, c));
                }
                else if (grid[r][c] == 1)
                {
                    freshCount++;
                }
            }
        }

        // Trivial Short Circuit: No fresh oranges to rot
        if (freshCount == 0)
        {
            return 0;
        }

        int minutesElapsed = 0;
        ReadOnlySpan<(int Dr, int Dc)> directions = stackalloc (int Dr, int Dc)[]
        {
            (-1, 0), // North
            (1, 0),  // South
            (0, -1), // West
            (0, 1)   // East
        };

        // Phase 2: Level-by-Level Multi-Source Wavefront Propagation
        while (queue.Count > 0 && freshCount > 0)
        {
            int levelSize = queue.Count;

            for (int i = 0; i < levelSize; i++)
            {
                var (currR, currC) = queue.Dequeue();

                foreach (var (dr, dc) in directions)
                {
                    int nextR = currR + dr;
                    int nextC = currC + dc;

                    // Boundary and state validity check
                    if (nextR >= 0 && nextR < rows && nextC >= 0 && nextC < cols && grid[nextR][nextC] == 1)
                    {
                        // Invariant: Mutate cell state immediately upon enqueueing to prevent duplicate exploration
                        grid[nextR][nextC] = 2;
                        freshCount--;
                        queue.Enqueue((nextR, nextC));
                    }
                }
            }

            minutesElapsed++;
        }

        // Phase 3: Defensive Post-Condition Verification
        return freshCount == 0 ? minutesElapsed : -1;
    }
}
```

---

### 6. Senior Pitfalls & Defensive Traps
1. **The Trailing Minute Bug:** Incrementing `minutesElapsed` blindly at the end of every while loop iteration. If the queue finishes processing the last infected orange, a naive condition `while (queue.Count > 0)` will execute one extra iteration where no fresh oranges are infected, inflating the answer by +1. Defensive fix: Either guard with `while (queue.Count > 0 && freshCount > 0)` or use a flag `bool infectedAny = false` per level.
2. **Duplicate Enqueueing Vulnerability:** Delaying the state mutation (`grid[nr][nc] = 2`) until dequeueing. If multiple rotten oranges share a fresh neighbor, that neighbor will be added to the queue multiple times, ballooning memory to $O((M \cdot N)^2)$ and skewing time complexity. **Rule:** Mutate state *at the point of enqueueing*.
3. **Empty / Non-Uniform Jagged Arrays:** Defensive validation must verify `grid[0] != null` and consistent column lengths in production code.

---

## 134. Split Array Largest Sum (LeetCode #410)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🔴 Hard |
| **Priority** | ⭐ Core (Google flagship / Amazon / Uber) |
| **Pattern Tags** | `#binary-search-on-answer` `#greedy-feasibility` `#monotonic-predicate` `#divide-and-conquer` |
| **LeetCode Link** | [Split Array Largest Sum](https://leetcode.com/problems/split-array-largest-sum/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given an integer array `nums` and an integer `k`, split `nums` into `k` non-empty subarrays such that the largest sum of any subarray is minimized. Return the minimized largest sum of the split. A subarray is a contiguous part of the array.
- **Assumptions & Contracts:**
  - $k$ is always $\le nums.Length$.
  - All elements $nums[i] \ge 0$.
  - Subarrays must be contiguous; reordering elements is strictly forbidden.
- **Key Constraints:**
  - $1 \le nums.Length \le 1000$
  - $0 \le nums[i] \le 10^6$
  - $1 \le k \le \min(50, nums.Length)$
- **Senior Edge Cases to Defend:**
  - **$k == nums.Length$:** Each element forms its own subarray; the answer is strictly $\max(nums)$.
  - **$k == 1$:** The entire array is a single subarray; the answer is $\sum nums$.
  - **Large Sums Causing 32-bit Integer Overflow:** $1000 \times 10^6 = 10^9$, which fits in signed 32-bit int, but intermediate binary search sums (`left + right`) can reach $2 \times 10^9 \approx 2^{31}-1$. Must use 64-bit `long` for binary search boundaries.
  - **All Zeros:** `nums = [0, 0, 0], k = 2` $\implies$ answer is `0`.

---

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Binary Search over the monotonic answer space combined with a greedy feasibility check. Instead of determining *how* to split, we guess a candidate maximum subarray sum $S$ and test if the array can be partitioned into $\le k$ subarrays where no subarray exceeds sum $S$.
- **Sample 1:**
  - **Input:** `nums = [7,2,5,10,8], k = 2`
  - **Output:** `18`
  - **Explanation:** There are four ways to split nums into two subarrays:
    - `[7]` and `[2,5,10,8]`: max sum = 25
    - `[7,2]` and `[5,10,8]`: max sum = 23
    - `[7,2,5]` and `[10,8]`: max sum = 18
    - `[7,2,5,10]` and `[8]`: max sum = 24
    The minimum largest sum is 18.
- **Sample 2:**
  - **Input:** `nums = [1,2,3,4,5], k = 2`
  - **Output:** `9`
  - **Explanation:** Optimal split: `[1,2,3]` (sum 6) and `[4,5]` (sum 9). Max sum = 9.

---

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine packing items into $k$ shipping containers. Each container has a weight limit $S$. You cannot reorder items; they must roll into containers in order off a conveyor belt. If you choose a tiny weight limit $S = 5$, you need 20 containers (fails the $\le k$ limit). If you choose a massive limit $S = 1,000,000$, all items fit into 1 container (feasible, but not minimal). As container capacity $S$ increases, the required container count decreases monotonically. This monotonic relationship allows binary search to pinpoint the exact smallest capacity that requires $\le k$ containers.

#### 3.2 The Naive Bottleneck & Redundant Computation
Dynamic Programming formulation:
$$DP[i][j] = \min_{p < i} \max(DP[p][j-1], \sum_{m=p+1}^i nums[m])$$
- State Space: $N \times k$
- Transition Cost: $O(N)$ to evaluate all possible split points $p$.
- Total Time: $O(k \cdot N^2)$. For $N = 1000, k = 50$, $k \cdot N^2 = 5 \times 10^7$ operations. While it passes, it uses $O(k \cdot N)$ memory and is far slower than binary search.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Monotonicity of Predicate $P(S)$:**
  Let $P(S)$ be the boolean predicate: *"Can `nums` be split into $\le k$ contiguous subarrays, each with sum $\le S$?"*
  $$\text{If } P(S) \text{ is True, then } \forall S' > S, P(S') \text{ is True.}$$
  $$\text{If } P(S) \text{ is False, then } \forall S' < S, P(S') \text{ is False.}$$
- **Exact Search Range:**
  $$low = \max_{i}(nums[i]) \quad \text{(no subarray can have sum less than its largest element)}$$
  $$high = \sum_{i} nums[i] \quad \text{(all elements in 1 subarray)}$$
- The answer space is partitioned into $[False, False, \dots, False, True, True, \dots, True]$. We seek the **first True index**.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
Answer Space Monotonicity:
Target Value (Minimal S where P(S) == True)
                         ▼
S : [ low ... mid_1 ... mid_target ... mid_2 ... high ]
P : [  F        F            T            T        T  ]
      └─────────┬─────────┘  └────────────┬───────────┘
         Too Small:                  Feasible:
      Requires > k cuts           Requires <= k cuts
      low = mid + 1               high = mid
```

#### 3.5 State Transition Triggers & Decision Gates
1. **Feasibility Predicate `CanSplit(nums, k, maxLimit)`:**
   - Initialize `subarraysNeeded = 1`, `runningSum = 0`.
   - For each `num` in `nums`:
     - If `runningSum + num <= maxLimit`: `runningSum += num`.
     - Else: `subarraysNeeded++`, `runningSum = num`.
   - Return `subarraysNeeded <= k`.
2. **Binary Search Loop (`low < high`):**
   - `mid = low + (high - low) / 2`.
   - If `CanSplit(nums, k, mid)` is True:
     - Feasible! Target could be `mid` or smaller $\implies high = mid$.
   - Else:
     - Infeasible! Target must be strictly larger than `mid` $\implies low = mid + 1$.
3. **Termination:** When $low == high$, return $low$.

#### 3.6 Concrete Step-by-Step State Trace
Input: `nums = [7, 2, 5, 10, 8]`, $k = 2$.
- $low = \max(7,2,5,10,8) = 10$.
- $high = 7 + 2 + 5 + 10 + 8 = 32$.

| Iteration | $low$ | $high$ | $mid$ | Greedy Split Simulation for Limit = $mid$ | Subarrays Needed | Feasible ($\le 2$)? | Boundary Update |
| :---: | :---: | :---: | :---: | :--- | :---: | :---: | :--- |
| **1** | 10 | 32 | 21 | `[7,2,5] (14)`, `[10,8] (18)` | 2 | **True** | $high = 21$ |
| **2** | 10 | 21 | 15 | `[7,2,5] (14)`, `[10] (10)`, `[8] (8)` | 3 | **False** | $low = 16$ |
| **3** | 16 | 21 | 18 | `[7,2,5] (14)`, `[10,8] (18)` | 2 | **True** | $high = 18$ |
| **4** | 16 | 18 | 17 | `[7,2,5] (14)`, `[10] (10)`, `[8] (8)` | 3 | **False** | $low = 18$ |
| **End** | 18 | 18 | - | Terminated ($low == high$) | - | - | Return `18` |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Binary Search on Answer (Optimal):** Time complexity is $O(N \log(\sum nums - \max nums))$. For $N = 1000$ and sum $10^9$, $\log_2(10^9) \approx 30$, yielding $\approx 30 \times 1000 = 3 \times 10^4$ operations. Space is strictly $O(1)$.
- **Dynamic Programming (Suboptimal):** Useful only if required to output all split indices or when arbitrary non-contiguous partitions are permitted.

#### 4.2 Step-by-Step Natural Progression Flow
1. Compute $low = \max(nums)$ and $high = \sum nums$.
2. While $low < high$, calculate midpoint.
3. Test feasibility in linear time $O(N)$ using greedy accumulation.
4. Adjust binary search boundaries according to predicate outcome.
5. Return $low$.

#### 4.3 Alternative Approaches Analysis
- **Top-Down Memoized DP:** $DP(index, splitsLeft)$. Memoization table of size $1000 \times 50$. Evaluates splits recursively. Memory overhead $O(N \cdot k)$ and high recursion call stack overhead.
- **Bottom-Up Tabulation with Prefix Sums:** Tabulates $DP[i][j]$ with monotonic queue optimization. Complex to implement without bugs in an interview setting.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Approach | Time (Best) | Time (Avg) | Time (Worst) | Aux Space | Output Space | Cache Locality | In-Place Mutability | Streaming Suitability |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **Binary Search on Answer** | $O(N)$ | $O(N \log S)$ | $O(N \log S)$ | $O(1)$ | $O(1)$ | Optimal | Non-mutating | High |
| **DP (Tabulation)** | $O(k \cdot N^2)$ | $O(k \cdot N^2)$ | $O(k \cdot N^2)$ | $O(k \cdot N)$ | $O(1)$ | High | Non-mutating | Low |
| **DP + Monotonic Queue** | $O(k \cdot N)$ | $O(k \cdot N)$ | $O(k \cdot N)$ | $O(N)$ | $O(1)$ | Moderate | Non-mutating | Low |

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: LeetCode #410 - Split Array Largest Sum
 * ============================================================================
 * Core Pattern      : Binary Search on Monotonic Answer Space + Greedy Feasibility
 * Time Complexity   : O(N * log(Sum - Max)) where N = nums.Length
 * Space Complexity  : O(1) Auxiliary Memory
 * Selection Rule    : Optimization problem minimizing a maximum value over contiguous partitions.
 * Defensive Traps   : 1. Use 64-bit 'long' for low, high, mid to prevent overflow.
 *                     2. Ensure low starts at Math.Max(element), not 0 or 1.
 *                     3. Ensure greedy check handles elements exactly equal to mid.
 * ============================================================================
 */

using System;

public class Solution
{
    /// <summary>
    /// Minimizes the largest subarray sum when splitting nums into k subarrays.
    /// </summary>
    public int SplitArray(int[] nums, int k)
    {
        // Guard Clauses
        if (nums == null || nums.Length == 0)
        {
            throw new ArgumentException("Array cannot be null or empty.", nameof(nums));
        }

        if (k <= 0 || k > nums.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(k), "k must be between 1 and nums.Length.");
        }

        // Structural Invariant: Search space is bounded by [MaxElement, TotalSum]
        long low = 0;
        long high = 0;

        foreach (int val in nums)
        {
            if (val > low)
            {
                low = val;
            }
            high += val;
        }

        // Binary search for the minimal feasible maximum sum
        while (low < high)
        {
            long mid = low + ((high - low) >> 1);

            if (CanSplit(nums, k, mid))
            {
                // Invariant: mid is feasible; target lies in [low, mid]
                high = mid;
            }
            else
            {
                // Invariant: mid is infeasible; target lies in [mid + 1, high]
                low = mid + 1;
            }
        }

        return (int)low;
    }

    /// <summary>
    /// Greedy predicate: Determines if nums can be partitioned into <= k subarrays
    /// with no subarray sum exceeding maxSubarraySum.
    /// </summary>
    private static bool CanSplit(int[] nums, int k, long maxSubarraySum)
    {
        int requiredSubarrays = 1;
        long currentSubarraySum = 0;

        foreach (int num in nums)
        {
            // Defensive check: A single element cannot exceed the capacity
            if (num > maxSubarraySum)
            {
                return false;
            }

            if (currentSubarraySum + num <= maxSubarraySum)
            {
                currentSubarraySum += num;
            }
            else
            {
                // Capacity exceeded: Seal current subarray and start a new one
                requiredSubarrays++;
                currentSubarraySum = num;

                if (requiredSubarrays > k)
                {
                    return false; // Early pruning
                }
            }
        }

        return requiredSubarrays <= k;
    }
}
```

---

### 6. Senior Pitfalls & Defensive Traps
1. **The $low = 0$ Initialization Error:** Setting $low = 0$ or $low = \text{average}(nums)$. If $low < \max(nums)$, the predicate `CanSplit` will encounter an element that individually exceeds `maxSubarraySum`. If not defensively guarded, `currentSubarraySum = num` will start a subarray whose sum immediately exceeds `maxSubarraySum`, triggering an infinite split loop.
2. **Binary Search Boundary Off-By-One:** Using `while (low <= high)` and `high = mid - 1`. If `mid` is feasible, discarding `mid` (`high = mid - 1`) can throw away the optimal answer. The invariant pattern for finding the first `True` in a `[False...True]` monotonic sequence requires:
   - When feasible: `high = mid` (preserve candidate).
   - When infeasible: `low = mid + 1` (discard infeasible).
   - Loop condition: `while (low < high)`.
3. **Subarray Count Overflow:** When splitting, remember that an initial non-empty partition starts with `requiredSubarrays = 1`, not `0`.

---

## 135. Decode String (LeetCode #394)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core (Google / Amazon signature stack question) |
| **Pattern Tags** | `#stack` `#string-parsing` `#recursion` `#state-machine` |
| **LeetCode Link** | [Decode String](https://leetcode.com/problems/decode-string/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given an encoded string, return its decoded string. The encoding rule is: `k[encoded_string]`, where the `encoded_string` inside the square brackets is being repeated exactly `k` times. `k` is guaranteed to be a positive integer. You may assume that the input string is always valid; there are no extra white spaces, square brackets are well-formed, etc.
- **Assumptions & Contracts:**
  - Digits are only for repeat numbers `k`. That is, digits do not appear inside the string content itself (e.g., `3[a]` is valid, `3[a2]` is invalid input).
  - Nesting can be arbitrarily deep: `3[a2[c]]` $\implies$ `accaccacc`.
  - Non-bracketed characters may appear at any position: `2[abc]3[cd]ef`.
- **Key Constraints:**
  - $1 \le s.Length \le 30$
  - `s` consists of lowercase English letters, digits, and square brackets `'['`, `']'`.
  - All integers in `s` are in the range $[1, 300]$.
- **Senior Edge Cases to Defend:**
  - **Multi-Digit Repeat Counts:** E.g., `100[leetcode]`. The parser must accumulate digits iteratively (`k = k * 10 + digit`).
  - **Deep Nesting:** E.g., `2[a3[b2[c]]]`. Stacks must correctly maintain outer prefix strings during inner evaluations.
  - **Bare Characters Outside Brackets:** E.g., `abc3[cd]xyz`. Characters before and after bracket blocks must append seamlessly to the active builder.
  - **Single Repeat ($k = 1$):** `1[a]` $\implies$ `a`.

---

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Context-switching parser utilizing dual stacks or call-stack recursion. A `'['` saves the active string and repetition factor, resetting the current buffer. A `']'` pops the previous string and repeat count, multiplies the current buffer, and prepends the saved prefix.
- **Sample 1:**
  - **Input:** `s = "3[a]2[bc]"`
  - **Output:** `"aaabcbc"`
- **Sample 2:**
  - **Input:** `s = "3[a2[c]]"`
  - **Output:** `"accaccacc"`
  - **Explanation:** Inner `2[c]` becomes `cc`. Then `a + cc` = `acc`. Outer `3[acc]` becomes `accaccacc`.
- **Sample 3:**
  - **Input:** `s = "2[abc]3[cd]ef"`
  - **Output:** `"abcabccdcdcdef"`

---

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Think of an operating system's process thread executing nested subroutine calls. As characters arrive, you write to the current local register (`currString`). When you encounter a multiplier followed by `[`, you are making a recursive function call: you push the current local register and the loop count onto the execution stack and open a fresh local scope. When you encounter `]`, the function returns: you take your finished local buffer, multiply it by the loop count, and append the result back into the caller's restored frame.

#### 3.2 The Naive Bottleneck & Redundant Computation
Attempting to evaluate inner brackets using repeated regex replacements or substring searches (`IndexOf('[')` and `LastIndexOf(']')`):
$$\text{Scan string} \to \text{Locate innermost brackets} \to \text{Replace} \to \text{Repeat}$$
Each replacement re-allocates strings and scans the entire string from scratch, resulting in $O(D \cdot L^2)$ time where $D$ is nesting depth and $L$ is string length. In contrast, a stack processes the token stream in a single linear pass $O(N + \text{OutputLength})$.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Grammar Definition:**
  $$\text{String} \to (\text{Char} \mid \text{Count} \text{ '[' String ']' })^*$$
- **Dual Stack Invariant:**
  At any cursor index $i$, `countStack` holds the repetition factors for all enclosing parent scopes, and `stringStack` holds the partial strings accumulated in those scopes prior to opening their corresponding `[` bracket.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
Parsing State Machine:
Current State: (currK : int, currString : StringBuilder)

Token 'digit': currK = currK * 10 + (c - '0')
Token '['    : countStack.Push(currK); stringStack.Push(currString);
               currK = 0; currString = new StringBuilder();
Token 'char' : currString.Append(c)
Token ']'    : k = countStack.Pop(); prev = stringStack.Pop();
               prev.Append(currString repeated k times);
               currString = prev;
```

#### 3.5 State Transition Triggers & Decision Gates

```mermaid
flowchart TD
    CharInput{"Character c"} -->|isDigit| G1["currK = currK * 10 + (c - '0')"]
    CharInput -->|is '['| G2["Push currK to countStack<br/>Push currStr to strStack<br/>currK = 0; currStr = empty"]
    CharInput -->|isLetter| G3["currStr.Append(c)"]
    CharInput -->|is ']'| G4["k = countStack.Pop()<br/>prevStr = strStack.Pop()<br/>prevStr.Append(currStr * k)<br/>currStr = prevStr"]
```

#### 3.6 Concrete Step-by-Step State Trace
Input: `s = "3[a2[c]]"`

| Step ($i$) | Token | `currK` | `currString` | `countStack` | `stringStack` | Action Taken |
| :---: | :---: | :---: | :--- | :--- | :--- | :--- |
| **0** | `'3'` | 3 | `""` | `[]` | `[]` | Accumulate multiplier |
| **1** | `'['` | 0 | `""` | `[3]` | `[""]` | Context push: save count 3 and empty prefix |
| **2** | `'a'` | 0 | `"a"` | `[3]` | `[""]` | Append character |
| **3** | `'2'` | 2 | `"a"` | `[3]` | `[""]` | Accumulate multiplier |
| **4** | `'['` | 0 | `""` | `[3, 2]` | `["", "a"]` | Context push: save count 2 and prefix `"a"` |
| **5** | `'c'` | 0 | `"c"` | `[3, 2]` | `["", "a"]` | Append character |
| **6** | `']'` | 0 | `"acc"` | `[3]` | `[""]` | Pop 2 and `"a"`; `"a" + "c"*2 = "acc"` |
| **7** | `']'` | 0 | `"accaccacc"` | `[]` | `[]` | Pop 3 and `""`; `"" + "acc"*3 = "accaccacc"` |

Final Output: `"accaccacc"`.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1: Dual Stack Iterative (Production Standard):**
  - High performance, avoids potential stack overflow on deeply nested pathological inputs.
  - Two parallel stacks (`Stack<int>` and `Stack<StringBuilder>`).
- **Approach 2: Recursive Descent Parsing:**
  - Maintains state implicitly on the OS call stack. Elegant, but uses recursion stack space and requires passing an index pointer by reference.

#### 4.2 Step-by-Step Natural Progression Flow
1. Initialize `currK = 0`, `currString = new StringBuilder()`, and the two stacks.
2. Iterate through string characters, firing transition gates.
3. Upon loop completion, `currString.ToString()` contains the decoded text.

#### 4.3 Alternative Approaches Analysis
- **Regex Expansion:** Repeatedly find `(\d+)\[([a-z]+)\]` and replace with repeated string. Highly inefficient ($O(N^2)$ string copy operations) and fragile under complex recursion.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Approach | Time Complexity | Aux Space | Output Space | Call-Stack Risk | String Copy Overhead |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Dual Stack (Iterative)** | $O(\text{MaxK} \cdot N)$ | $O(N)$ | $O(\text{Output})$ | None | Minimal (amortized `StringBuilder`) |
| **Recursive Descent** | $O(\text{MaxK} \cdot N)$ | $O(N)$ | $O(\text{Output})$ | $O(\text{Depth})$ | Moderate |
| **Regex Iterative Search** | $O(D \cdot \text{Output}^2)$ | $O(\text{Output})$ | $O(\text{Output})$ | None | Catastrophic (multiple copies) |

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: LeetCode #394 - Decode String
 * ============================================================================
 * Core Pattern      : Dual Stack Parsing Automaton (Iterative Pushdown)
 * Time Complexity   : O(Output Length + S.Length) - Each char parsed once, builders expanded
 * Space Complexity  : O(N) Auxiliary Space - Stack depth bounded by nesting depth
 * Selection Rule    : Nested structure evaluation with variable multiplier tokens.
 * Defensive Traps   : 1. Accumulate multi-digit numbers (e.g. "100[a]").
 *                     2. Use StringBuilder rather than string concatenation in loops.
 *                     3. Correctly preserve sibling characters outside brackets.
 * ============================================================================
 */

using System;
using System.Collections.Generic;
using System.Text;

public class Solution
{
    /// <summary>
    /// Decodes an encoded string containing nested repetition patterns k[encoded_string].
    /// </summary>
    public string DecodeString(string s)
    {
        // Guard Clause
        if (string.IsNullOrEmpty(s))
        {
            return string.Empty;
        }

        var countStack = new Stack<int>();
        var stringStack = new Stack<StringBuilder>();
        var currentString = new StringBuilder();
        int currentK = 0;

        foreach (char c in s)
        {
            if (char.IsDigit(c))
            {
                // Multi-digit parsing invariant: shift base 10 and add digit
                currentK = (currentK * 10) + (c - '0');
            }
            else if (c == '[')
            {
                // Decision Gate: Save current context onto stacks and start fresh frame
                countStack.Push(currentK);
                stringStack.Push(currentString);

                currentK = 0;
                currentString = new StringBuilder();
            }
            else if (c == ']')
            {
                // Decision Gate: Pop enclosing frame context and multiply current buffer
                int repeatTimes = countStack.Pop();
                StringBuilder previousString = stringStack.Pop();

                // Append multiplied segment to previous scope's buffer
                for (int i = 0; i < repeatTimes; i++)
                {
                    previousString.Append(currentString);
                }

                // Current buffer becomes the fully merged parent buffer
                currentString = previousString;
            }
            else
            {
                // Ordinary alphabetical character
                currentString.Append(c);
            }
        }

        return currentString.ToString();
    }
}
```

---

### 6. Senior Pitfalls & Defensive Traps
1. **The Multi-Digit Number Bug:** Writing `currentK = c - '0'` instead of `currentK = currentK * 10 + (c - '0')`. When $k \ge 10$, this records only the last digit.
2. **String Allocation Bomb:** Using C# string concatenation (`prev += curr`) inside the repetition loop. This generates $O(k)$ intermediate string heap objects, causing severe garbage collector pressure. Always mutate via `StringBuilder.Append()`.
3. **Loss of Outer Plaintext:** Forgetting that letters can precede or succeed brackets (`3[a]b`). Resetting `currentString` to empty rather than continuing from `stringStack.Pop()` will drop intervening letters.

---

## 136. Bus Routes (LeetCode #815)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🔴 Hard |
| **Priority** | ⭐ Core (Uber / Google flagship graph question) |
| **Pattern Tags** | `#breadth-first-search` `#dual-graph` `#hyper-graph` `#shortest-path-unweighted` |
| **LeetCode Link** | [Bus Routes](https://leetcode.com/problems/bus-routes/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** You are given an array `routes` representing bus routes where `routes[i]` is a bus route that the $i$-th bus repeats forever. For example, if `routes[0] = [1, 5, 7]`, this means that the 0-th bus travels in the sequence $1 \to 5 \to 7 \to 1 \to 5 \to 7 \dots$ forever. You start at bus stop `source` and want to go to bus stop `target`. You cannot travel between stops outside of buses. Return the least number of buses you must take to travel from `source` to `target`. If it is impossible, return `-1`.
- **Assumptions & Contracts:**
  - Boarding a bus counts as taking 1 bus. Moving between stops on the *same* bus does NOT cost additional transfers.
  - A transfer occurs when switching from bus $A$ to bus $B$ at any common stop.
  - If `source == target`, you are already at your destination: cost is `0`.
- **Key Constraints:**
  - $1 \le routes.Length \le 500$ (Maximum 500 distinct buses)
  - $1 \le routes[i].Length \le 10^5$
  - Total stops across all routes $\sum routes[i].Length \le 10^5$
  - $0 \le routes[i][j] < 10^6$
  - $0 \le source, target < 10^6$
- **Senior Edge Cases to Defend:**
  - **`source == target`:** Return `0` immediately without searching.
  - **Source or Target Not in Any Route:** Return `-1` immediately.
  - **Disconnected Route Islands:** No overlapping transfer stops between source component and target component $\implies -1$.
  - **Routes with Duplicate Stops:** A route array might contain duplicates; deduplicate using sets if needed.

---

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Breadth-First Search on a **Dual Graph (Route Graph)**. Standard BFS on stops has up to $10^6$ vertices and dense clique edges ($O(L^2)$ per route), triggering TLE/MLE. Transforming the problem so that **buses are the graph nodes** reduces the state space to at most 500 nodes.
- **Sample 1:**
  - **Input:** `routes = [[1,2,7],[3,6,7]], source = 1, target = 6`
  - **Output:** `2`
  - **Explanation:** Take Bus 0 from stop 1 to stop 7. Transfer at stop 7 to Bus 1 to reach stop 6. Total buses = 2.
- **Sample 2:**
  - **Input:** `routes = [[7,12],[4,5,15],[6],[15,19],[9,12,13]], source = 15, target = 12`
  - **Output:** `-1`
  - **Explanation:** Impossible to travel between stop 15 and stop 12.

---

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine a traveler holding a transit pass. You do not care about walking between every pair of bus stops. You only care about **which bus lines you board**. Every bus route is a single "hyper-edge" connecting all its stops. If you board Bus 0, you instantly have zero-cost access to all stops on Bus 0. You look for any other bus that intersects with any stop of Bus 0 and board that bus. The number of buses boarded equals the BFS tree depth on the bus route graph.

#### 3.2 The Naive Bottleneck & Redundant Computation
- **Naive Formulation (Stop Graph):**
  Treat each stop as a node. For a route with $K$ stops, add an edge between all $O(K^2)$ pairs of stops.
  - For $K = 10^5$, $K^2 = 10^{10}$ edges $\implies$ instant Memory Limit Exceeded and Time Limit Exceeded.
- **Dual Graph Formulation (Bus Graph):**
  Treat each **bus route** as a node ($N \le 500$). An undirected edge connects Bus $i$ and Bus $j$ if they share at least one stop.
  - Maximum vertices = 500. Maximum edges = $\binom{500}{2} \approx 125,000$. Search completes in under 15ms.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Inverted Stop Index:**
  Construct a mapping:
  $$\text{stopToBuses} : \text{StopID} \to \text{List of Bus IDs passing through this stop}$$
- **Bipartite Level BFS Invariant:**
  BFS depth $d$ represents taking exactly $d$ buses. When visiting Bus $i$, we inspect all its stops. For each unvisited stop, we expand to all unvisited buses servicing that stop.
- **Dual Visited Sets:**
  1. `visitedBuses` (`bool[]` of size 500): Prevents re-evaluating the same bus route.
  2. `visitedStops` (`HashSet<int>`): Prevents iterating through the same stop's bus list multiple times.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
Dual Graph Bipartite BFS Expansion:
[ Source Stop ]
      │
      ▼ (via stopToBuses map)
[ Initial Buses Enqueued: Level 1 ] (visitedBuses[bus] = true)
      │
      ├───────────────────────┐
      ▼                       ▼
  [ Bus 0 Stops ]         [ Bus 2 Stops ] (visitedStops.Add(stop))
      │                       │
      └───────────┬───────────┘
                  ▼ (via stopToBuses map)
     [ Transfer Buses: Level 2 ]
```

#### 3.5 State Transition Triggers & Decision Gates
1. **Trivial Check:** If `source == target`, return `0`.
2. **Index Building:** Populate `Dictionary<int, List<int>> stopToBuses`. If `source` or `target` is missing from map, return `-1`.
3. **Queue Seeding:** Enqueue all buses passing through `source`. Mark them in `visitedBuses`. Set `busesTaken = 1`.
4. **Level-Order Expansion:**
   - For each bus in current queue level:
     - For each `stop` in `routes[bus]`:
       - If `stop == target`, return `busesTaken`.
       - If `visitedStops.Add(stop)` succeeds:
         - For each `nextBus` in `stopToBuses[stop]`:
           - If `!visitedBuses[nextBus]`:
             - `visitedBuses[nextBus] = true`
             - `queue.Enqueue(nextBus)`
   - `busesTaken++`
5. **Queue Exhaustion:** Target was unreachable $\implies$ return `-1`.

#### 3.6 Concrete Step-by-Step State Trace
Input: `routes = [[1, 2, 7], [3, 6, 7]]`, `source = 1`, `target = 6`.
- `stopToBuses = { 1:[0], 2:[0], 7:[0, 1], 3:[1], 6:[1] }`
- Initial Queue: `[Bus 0]`, `visitedBuses = [true, false]`, `busesTaken = 1`.

| Level (`busesTaken`) | Dequeued Bus | Stops Examined | Target Found? | Unvisited Stops Added | Newly Enqueued Buses |
| :---: | :---: | :--- | :---: | :--- | :--- |
| **1** | Bus 0 | Stop 1, Stop 2, Stop 7 | No | `{1, 2, 7}` | Via Stop 7: Bus 1 is enqueued (`visitedBuses[1] = true`) |
| **2** | Bus 1 | Stop 3, Stop 6, Stop 7 | **Yes! (Stop 6 == target)** | - | **Terminate and return 2** |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Dual BFS (Bus-Level State Space):** Because $N \le 500$, tracking visited buses with a flat boolean array `bool[500]` gives $O(1)$ visited lookups and zero hash-collision overhead.
- **Why not Stop-Level BFS:** As demonstrated in section 3.2, building edges between stops is quadratic in the route size, which is completely intractable.

#### 4.2 Step-by-Step Natural Progression Flow
1. Check `source == target`.
2. Construct inverted index `stopToBuses`.
3. Seed queue with all buses servicing `source`.
4. Run level-order BFS over bus routes. Check each stop of the active route for `target`.
5. Return depth if target reached; `-1` if queue empties.

#### 4.3 Alternative Approaches Analysis
- **Explicit Route-to-Route Adjacency Graph:** Pre-build an adjacency matrix `bool[500, 500]` where `adj[i, j] = true` if routes $i$ and $j$ share a stop. Then run standard BFS from source-buses to target-buses. Valid and clean, but computing the intersection takes $O(N^2 \cdot L)$ upfront. The bipartite on-the-fly expansion avoids checking pairs that are never reached.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Approach | Time Complexity | Aux Space | Bus Limit Scalability | Stop Limit Scalability |
| :--- | :--- | :--- | :--- | :--- |
| **Dual BFS (On-the-Fly)** | $O(\sum |routes[i]|)$ | $O(\sum |routes[i]|)$ | Up to $10^4$ buses | Up to $10^6$ stops |
| **Precomputed Bus Adjacency**| $O(N^2 \cdot L)$ | $O(N^2 + \sum L)$ | $N \le 1000$ | Up to $10^6$ stops |
| **Stop-Level Graph BFS** | $O(\sum |routes[i]|^2)$ | $O(\sum |routes[i]|^2)$ | Fails ($> 10^9$ edges) | Fails |

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: LeetCode #815 - Bus Routes
 * ============================================================================
 * Core Pattern      : Dual-Graph Bipartite BFS (Bus-Level Hyper-Edge Traversal)
 * Time Complexity   : O(Sum of routes[i].Length) - Each stop and route processed once
 * Space Complexity  : O(Sum of routes[i].Length) - Storage for inverted stop-to-bus map
 * Selection Rule    : Shortest transfer path on routes where stops within a route have 0 cost.
 * Defensive Traps   : 1. source == target must return 0 immediately.
 *                     2. Track visitedStops to avoid re-examining the same stops across different buses.
 *                     3. Handle cases where source or target does not exist in any route.
 * ============================================================================
 */

using System;
using System.Collections.Generic;

public class Solution
{
    /// <summary>
    /// Computes the least number of buses required to travel from source to target stop.
    /// </summary>
    public int NumBusesToDestination(int[][] routes, int source, int target)
    {
        // Guard Clause: Already at destination requires 0 buses
        if (source == target)
        {
            return 0;
        }

        if (routes == null || routes.Length == 0)
        {
            return -1;
        }

        int numBuses = routes.Length;

        // Phase 1: Construct Inverted Index (Stop -> List of Bus Indices)
        var stopToBuses = new Dictionary<int, List<int>>();
        for (int busId = 0; busId < numBuses; busId++)
        {
            foreach (int stop in routes[busId])
            {
                if (!stopToBuses.TryGetValue(stop, out var busList))
                {
                    busList = new List<int>();
                    stopToBuses[stop] = busList;
                }
                busList.Add(busId);
            }
        }

        // Defensive Verification: If source or target are absent from transit system
        if (!stopToBuses.ContainsKey(source) || !stopToBuses.ContainsKey(target))
        {
            return -1;
        }

        // Phase 2: BFS Frontier Initialization
        var busQueue = new Queue<int>();
        var visitedBuses = new bool[numBuses];
        var visitedStops = new HashSet<int>();

        // Seed initial frontier with all buses that pass through the source stop
        foreach (int busId in stopToBuses[source])
        {
            visitedBuses[busId] = true;
            busQueue.Enqueue(busId);
        }

        visitedStops.Add(source);
        int busesTaken = 1;

        // Phase 3: Level-Order BFS on Bus Routes
        while (busQueue.Count > 0)
        {
            int levelSize = busQueue.Count;

            for (int i = 0; i < levelSize; i++)
            {
                int currentBus = busQueue.Dequeue();

                // Explore all stops accessible via the current bus
                foreach (int stop in routes[currentBus])
                {
                    // Target Detection Gate
                    if (stop == target)
                    {
                        return busesTaken;
                    }

                    // Invariant: Prune already explored stops to avoid repeated bus lookups
                    if (visitedStops.Add(stop))
                    {
                        // Enqueue all unvisited transfer buses passing through this stop
                        foreach (int nextBus in stopToBuses[stop])
                        {
                            if (!visitedBuses[nextBus])
                            {
                                visitedBuses[nextBus] = true;
                                busQueue.Enqueue(nextBus);
                            }
                        }
                    }
                }
            }

            busesTaken++;
        }

        return -1; // Target unreachable
    }
}
```

---

### 6. Senior Pitfalls & Defensive Traps
1. **The Missing `source == target` Check:** If `source == target`, the algorithm might seed the queue with buses passing through `source` and return `1` instead of `0`. Always guard with an upfront short circuit.
2. **Missing `visitedStops` Pruning:** Tracking only `visitedBuses` and failing to track `visitedStops`. If a major transit hub (e.g., Central Station) is shared by 200 buses, every time a new bus visits Central Station it iterates through all 200 buses again, turning linear traversal into $O(N \cdot \text{Stops})$. Tracking `visitedStops` guarantees each stop is expanded into its bus list exactly once.
3. **Queueing Stops Instead of Buses:** Falling back into stop-level queueing causes memory explosion. The queue must hold **bus indices**.

---

## 137. Step-By-Step Directions From a Binary Tree Node to Another (LeetCode #2096)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core (Amazon / Google flagship tree traversal) |
| **Pattern Tags** | `#binary-tree` `#lowest-common-ancestor` `#depth-first-search` `#path-compression` |
| **LeetCode Link** | [Step-By-Step Directions From a Binary Tree Node to Another](https://leetcode.com/problems/step-by-step-directions-from-a-binary-tree-node-to-another/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** You are given the `root` of a binary tree with `n` nodes. Each node is assigned a unique value from `1` to `n`. You are also given an integer `startValue` representing the value of the start node `s`, and an integer `destValue` representing the value of the destination node `t`. Find the shortest step-by-step directions going from node `s` to node `t`. A step can be one of:
  - `'U'`: Go from a node to its parent node.
  - `'L'`: Go from a node to its left child node.
  - `'R'`: Go from a node to its right child node.
  Return the step-by-step directions of the shortest path from node `s` to node `t`.
- **Assumptions & Contracts:**
  - All $n$ node values are unique.
  - Both `startValue` and `destValue` exist in the tree.
  - `startValue != destValue`.
  - In a tree, the shortest path between any two nodes is unique and passes through their Lowest Common Ancestor (LCA).
- **Key Constraints:**
  - The number of nodes in the tree is $n$.
  - $2 \le n \le 10^5$
  - $1 \le Node.val \le n$
  - All $Node.val$ are unique.
  - $1 \le startValue, destValue \le n$
- **Senior Edge Cases to Defend:**
  - **Start is Ancestor of Dest:** Path consists entirely of `'L'` and `'R'` moves (zero `'U'`s).
  - **Dest is Ancestor of Start:** Path consists entirely of `'U'` moves (zero `'L'`/`'R'`s).
  - **Skewed Trees (Linked List Topology):** Height $O(N)$ could trigger recursion stack overflow if not careful with recursion depth.
  - **Large Output Strings:** For $N = 10^5$, paths can be $10^5$ characters long. Must avoid repeated string concatenations during backtracking.

---

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Find the path from `root` to `startValue` and the path from `root` to `destValue`. Discard their longest common prefix (the path to their Lowest Common Ancestor). Convert all remaining steps in the start path to `'U'` (climbing up to LCA), and append the remaining steps in the destination path (descending from LCA to dest).
- **Sample 1:**
  - **Input:** `root = [5,1,2,3,null,6,4], startValue = 3, destValue = 6`
  - **Output:** `"UURL"`
  - **Explanation:**
    - Root (5) to Start (3): `"LL"`
    - Root (5) to Dest (6): `"RL"`
    - Longest Common Prefix: `""` (Length 0, LCA is 5)
    - Up from 3 to 5: `"UU"`
    - Down from 5 to 6: `"RL"`
    - Combined: `"UURL"`
- **Sample 2:**
  - **Input:** `root = [2,1], startValue = 2, destValue = 1`
  - **Output:** `"L"`
  - **Explanation:** Root to 2 is `""`, Root to 1 is `"L"`. Common prefix length 0. Combined: `"L"`.

---

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine climbing a family genealogical tree from yourself to your second cousin. You both trace your lineage back to a common great-grandparent (the LCA). You climb straight up your family line to that great-grandparent (`U`, `U`, `U`), and then climb down the specific branch leading to your cousin (`L`, `R`). You never need to climb all the way back to the root of human history—you only climb to the point where your ancestral lineages diverge.

#### 3.2 The Naive Bottleneck & Redundant Computation
A common naive approach:
1. Run a DFS pass to find the Lowest Common Ancestor node.
2. Run a second DFS pass from the LCA to find `startValue`, reversing the path to make `'U'`.
3. Run a third DFS pass from the LCA to find `destValue`.
This requires 3 distinct tree traversals, complex tree node references, and building node-parent pointer maps with $O(N)$ hash tables.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Root-to-Node Path Duality:**
  Any node's location in a binary tree is uniquely identified by its string of left/right branch decisions from the root:
  $$P_{\text{start}} = \text{Path}(\text{Root} \to \text{startValue})$$
  $$P_{\text{dest}} = \text{Path}(\text{Root} \to \text{destValue})$$
- **LCA as Common Prefix:**
  The path from the root to $\text{LCA}(\text{start}, \text{dest})$ is precisely the longest common prefix of $P_{\text{start}}$ and $P_{\text{dest}}$:
  $$L = \max \{ k \mid P_{\text{start}}[0 \dots k-1] == P_{\text{dest}}[0 \dots k-1] \}$$
- **Exact Path Formula:**
  $$\text{Final Path} = \underbrace{\text{'U'} \times (|P_{\text{start}}| - L)}_{\text{Ascend to LCA}} + \underbrace{P_{\text{dest}}[L \dots |P_{\text{dest}}|-1]}_{\text{Descend to Dest}}$$
  This requires only **two root-to-node path searches** (which can be done concurrently or sequentially in a single pass), completely eliminating the need for an explicit LCA subroutine!

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
Path Prefix Cancellation:
Root-to-Start : [ L , R , L ] + [ L , R ]   (Total: "LRL" + "LR")
Root-to-Dest  : [ L , R , L ] + [ R , L ]   (Total: "LRL" + "RL")
                └─────┬─────┘
             Common Ancestor Path
               (Length L = 3)

Ascent Steps  : Length(Root-to-Start) - L = 5 - 3 = 2  ===> "UU"
Descent Steps : Root-to-Dest substring from index L    ===> "RL"
Result        : "UU" + "RL" = "UURL"
```

#### 3.5 State Transition Triggers & Decision Gates
1. **DFS Path Finder `FindPath(node, target, pathBuilder)`:**
   - If `node == null`, return `false`.
   - If `node.val == target`, return `true`.
   - **Left Branch Gate:**
     - `pathBuilder.Append('L')`
     - If `FindPath(node.left, target, pathBuilder)` is true, return `true`.
     - `pathBuilder.Length--` (Backtrack)
   - **Right Branch Gate:**
     - `pathBuilder.Append('R')`
     - If `FindPath(node.right, target, pathBuilder)` is true, return `true`.
     - `pathBuilder.Length--` (Backtrack)
   - Return `false`.
2. **Prefix Comparison:** Find first index $i$ where $startPath[i] \neq destPath[i]$.
3. **Assembly:** Construct string with $(startPath.Length - i)$ `'U'`s followed by $destPath.Substring(i)$.

#### 3.6 Concrete Step-by-Step State Trace
Tree: `5` with left `1 (left: 3)`, right `2 (left: 6, right: 4)`. `start = 3`, `dest = 6`.
- $P_{\text{start}} = \text{Root(5)} \to \text{Left(1)} \to \text{Left(3)} \implies \text{"LL"}$.
- $P_{\text{dest}} = \text{Root(5)} \to \text{Right(2)} \to \text{Left(6)} \implies \text{"RL"}$.

| Index $i$ | $P_{\text{start}}[i]$ | $P_{\text{dest}}[i]$ | Match? | Common Prefix Length $L$ |
| :---: | :---: | :---: | :---: | :---: |
| 0 | `'L'` | `'R'` | **Mismatch!** | 0 |

- Number of `'U'`s: $|P_{\text{start}}| - L = 2 - 0 = 2 \implies \text{"UU"}$.
- Suffix of $P_{\text{dest}}$ from index 0: `"RL"`.
- Final Path: `"UU" + "RL" = "UURL"`.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **DFS Backtracking with Reusable `StringBuilder`:** Allocates a single mutable builder per path, appending and popping characters with $O(1)$ amortized cost. Total auxiliary space is bounded by tree height $O(H)$.
- **Avoid string concatenation during DFS:** Passing immutable strings `path + "L"` creates $O(H^2)$ garbage string allocations during deep recursion.

#### 4.2 Step-by-Step Natural Progression Flow
1. Run `FindPath(root, startValue, startPath)`.
2. Run `FindPath(root, destValue, destPath)`.
3. Compare both paths character by character from the beginning to compute common prefix length $L$.
4. Allocate output string of exact length $(startPath.Length - L) + (destPath.Length - L)$ using string constructor and slice.

#### 4.3 Alternative Approaches Analysis
- **Graph Transformation + BFS:** Convert binary tree into an undirected graph by adding parent pointers, then run standard BFS from `startValue` to `destValue`. Time: $O(N)$, but memory is $O(N)$ for graph edges, node lookups, and visited sets. Far more complex and high constant factor overhead.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Approach | Time Complexity | Aux Space | GC Allocations | Tree Mutability |
| :--- | :--- | :--- | :--- | :--- |
| **DFS Path + Prefix Cancellation** | $O(N)$ | $O(H)$ | Minimal (2 builders) | Non-mutating |
| **Tree to Graph + BFS** | $O(N)$ | $O(N)$ (High) | Heavy (nodes/lists) | Non-mutating |
| **Explicit LCA + Dual DFS** | $O(N)$ | $O(H)$ | Moderate | Non-mutating |

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: LeetCode #2096 - Step-By-Step Tree Directions
 * ============================================================================
 * Core Pattern      : Root-to-Node DFS Backtracking + LCA Prefix Cancellation
 * Time Complexity   : O(N) - Traverses tree nodes at most twice to locate paths
 * Space Complexity  : O(H) Auxiliary Space - Stack and path builders bounded by tree height
 * Selection Rule    : Shortest path between two nodes in a tree with directed edge labels.
 * Defensive Traps   : 1. Use StringBuilder with backtrack (Length--) to prevent O(H^2) string allocations.
 *                     2. Use string constructor new string('U', count) for optimal O(1) loop-free allocation.
 *                     3. Ensure common prefix loop stops at Math.Min(length1, length2).
 * ============================================================================
 */

using System;
using System.Text;

public class TreeNode
{
    public int val;
    public TreeNode left;
    public TreeNode right;
    public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
    {
        this.val = val;
        this.left = left;
        this.right = right;
    }
}

public class Solution
{
    /// <summary>
    /// Generates the shortest direction string ('U', 'L', 'R') from startValue to destValue.
    /// </summary>
    public string GetDirections(TreeNode root, int startValue, int destValue)
    {
        // Guard Clauses
        if (root == null)
        {
            throw new ArgumentNullException(nameof(root));
        }

        if (startValue == destValue)
        {
            return string.Empty;
        }

        var startPath = new StringBuilder();
        var destPath = new StringBuilder();

        // Phase 1: Retrieve exact path decisions from root to each target
        FindPath(root, startValue, startPath);
        FindPath(root, destValue, destPath);

        // Phase 2: Identify Lowest Common Ancestor (LCA) via Longest Common Prefix
        int commonPrefixLength = 0;
        int minLength = Math.Min(startPath.Length, destPath.Length);

        while (commonPrefixLength < minLength && 
               startPath[commonPrefixLength] == destPath[commonPrefixLength])
        {
            commonPrefixLength++;
        }

        // Phase 3: Construct Output Path
        int upMovesCount = startPath.Length - commonPrefixLength;
        int downMovesCount = destPath.Length - commonPrefixLength;

        var result = new StringBuilder(upMovesCount + downMovesCount);

        // Invariant: All remaining steps from start to LCA are ascents ('U')
        result.Append('U', upMovesCount);

        // Invariant: All remaining steps from LCA to dest are descents
        for (int i = commonPrefixLength; i < destPath.Length; i++)
        {
            result.Append(destPath[i]);
        }

        return result.ToString();
    }

    /// <summary>
    /// Backtracking DFS that records the branch directions from current node to target.
    /// </summary>
    private static bool FindPath(TreeNode node, int target, StringBuilder path)
    {
        if (node == null)
        {
            return false;
        }

        if (node.val == target)
        {
            return true;
        }

        // Explore Left Branch
        path.Append('L');
        if (FindPath(node.left, target, path))
        {
            return true;
        }
        path.Length--; // Backtrack

        // Explore Right Branch
        path.Append('R');
        if (FindPath(node.right, target, path))
        {
            return true;
        }
        path.Length--; // Backtrack

        return false;
    }
}
```

---

### 6. Senior Pitfalls & Defensive Traps
1. **String Concatenation Garbage Heap Storm:** Writing `FindPath(node.left, target, path + "L")`. For a tree with depth $10^4$, this generates $10^4$ intermediate strings, easily triggering OutOfMemoryException or severe GC pauses. Always pass a single `StringBuilder` and backtrack using `path.Length--`.
2. **Reverse Traversal Mistake:** Forgetting that steps from `startValue` to `LCA` are strictly `'U'`, regardless of whether the original edge was `'L'` or `'R'`. Do NOT invert the characters (e.g., turning `'L'` into `'R'`); every upward step is universally `'U'`.
3. **Prefix Index Out of Bounds:** Forgetting to bound the common prefix loop with `Math.Min(startPath.Length, destPath.Length)`. If one node is a direct ancestor of the other, the loop will crash if not bounded.

---

## 138. Sum of Subarray Ranges (LeetCode #2104)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium (Optimal $O(N)$ is 🔴 Hard level) |
| **Priority** | ⭐ Core (Amazon flagship monotonic stack / Uber) |
| **Pattern Tags** | `#monotonic-stack` `#contribution-technique` `#subarray-calculus` `#linear-sweep` |
| **LeetCode Link** | [Sum of Subarray Ranges](https://leetcode.com/problems/sum-of-subarray-ranges/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** You are given an integer array `nums`. The range of a subarray of `nums` is the difference between the largest and smallest element in the subarray. Return the sum of all subarray ranges of `nums`. A subarray is a contiguous non-empty sequence of elements within an array.
- **Assumptions & Contracts:**
  - Subarrays of length 1 have range $nums[i] - nums[i] = 0$.
  - Range is defined as $\max(nums[i \dots j]) - \min(nums[i \dots j])$.
- **Key Constraints:**
  - $1 \le nums.Length \le 1000$ (LeetCode limits permit $O(N^2)$, but Senior FAANG bar requires strictly $O(N)$ time).
  - $-10^9 \le nums[i] \le 10^9$
- **Senior Edge Cases to Defend:**
  - **64-bit Integer Overflow:** With $N = 1000$ and $nums[i] = 10^9$, range sum can reach $\approx 1000^2 \times 10^9 = 10^{15}$, overflowing signed 32-bit `int` ($2 \times 10^9$). Return type and intermediate accumulators must strictly be `long`.
  - **Duplicate Values in Subarray:** E.g., `nums = [1, 3, 3, 1]`. Must use strict inequality on one side and non-strict inequality on the other side in the monotonic stack to prevent duplicate counting.
  - **All Elements Identical:** `nums = [2, 2, 2]` $\implies$ every range is 0; total sum is 0.
  - **Single Element Array:** `nums = [5]` $\implies$ 0.

---

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** By linearity of summation, the sum of ranges decomposes into:
  $$\sum \text{Range} = \sum_{\text{all subarrays}} \max(sub) - \sum_{\text{all subarrays}} \min(sub)$$
  Instead of evaluating each subarray, we evaluate the **contribution** of each element $nums[i]$ as the maximum across all subarrays containing it, and subtract its contribution as the minimum across all subarrays containing it.
- **Sample 1:**
  - **Input:** `nums = [1, 2, 3]`
  - **Output:** `4`
  - **Explanation:** Subarrays:
    - `[1]`: range = 0
    - `[2]`: range = 0
    - `[3]`: range = 0
    - `[1,2]`: range = 2 - 1 = 1
    - `[2,3]`: range = 3 - 2 = 1
    - `[1,2,3]`: range = 3 - 1 = 2
    Total = 0 + 0 + 0 + 1 + 1 + 2 = 4.
- **Sample 2:**
  - **Input:** `nums = [1, 3, 3]`
  - **Output:** `4`

---

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine a marketplace where every possible group of people compares the tallest person and the shortest person. Calculating this by gathering every possible combination of people is chaos. Instead, ask each individual person: *"In how many distinct groups are you the undisputed tallest person?"* That person calculates their span of dominance to their left and right. Multiply their height by that count. Sum this for everyone to get the total max contribution. Do the same for the shortest person. The difference gives the exact total without ever creating a single group.

#### 3.2 The Naive Bottleneck & Redundant Computation
- **Brute Force ($O(N^2)$):**
  Fix start index $i$, iterate end index $j$ from $i$ to $N-1$, maintaining running `min` and `max`.
  - Operations: $\frac{N(N-1)}{2}$. For $N = 1000$, $\approx 5 \times 10^5$ operations.
  - While acceptable for $N = 1000$, if $N = 10^5$ (as in LeetCode #907), $O(N^2)$ takes hours and triggers TLE. The senior standard demands the optimal $O(N)$ Monotonic Stack approach.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Linearity of Summation:**
  $$\sum_{i \le j} (\max(nums[i \dots j]) - \min(nums[i \dots j])) = \sum_{k=0}^{N-1} nums[k] \cdot C_{\max}(k) - \sum_{k=0}^{N-1} nums[k] \cdot C_{\min}(k)$$
  where $C_{\max}(k)$ is the number of subarrays in which $nums[k]$ is the maximum element.
- **Span Calculation (Combinatorial Product):**
  For index $k$:
  - Let $L$ be the distance to the **Previous Greater Element (PGE)**.
  - Let $R$ be the distance to the **Next Greater or Equal Element (NGEE)**.
  $$C_{\max}(k) = L \times R = (k - \text{PGE}[k]) \times (\text{NGEE}[k] - k)$$
- **Defensive Duplicate Invariant (Strict vs Non-Strict):**
  To ensure a subarray with duplicate maximums (e.g., `[3, 3]`) attributes its maximum to **exactly one** instance:
  - Left boundary: Strictly greater (`>`)
  - Right boundary: Greater or equal (`>=`)

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
Monotonic Stack Dominance Span for nums[k]:
Indices:  ...  [ PGE ]  ...  ...  [ k ]  ...  ...  [ NGEE ]  ...
                 │                  ▲                 │
                 └──── L choices ───┴─── R choices ───┘
               Total Subarrays where nums[k] is Max = L * R
```

#### 3.5 State Transition Triggers & Decision Gates
To compute $\sum \max$ in $O(N)$:
1. Use a monotonic decreasing stack of indices.
2. For each element $nums[i]$ from $0$ to $N$:
   - While stack is not empty and $(i == N \text{ or } nums[stack.Peek()] < nums[i])$:
     - `mid = stack.Pop()`
     - `leftBound = stack.Count == 0 ? -1 : stack.Peek()`
     - `rightBound = i`
     - `count = (long)(mid - leftBound) * (rightBound - mid)`
     - `totalMax += nums[mid] * count`
   - `stack.Push(i)`
3. Mirror the exact same logic with monotonic increasing stack for $\sum \min$.
4. Return `totalMax - totalMin`.

#### 3.6 Concrete Step-by-Step State Trace
Input: `nums = [1, 2, 3]`, $N = 3$. Computing $\sum \max$:

| $i$ | `nums[i]` | Stack Before | Action / Popped Elements | Contribution Calculation | Stack After |
| :---: | :---: | :--- | :--- | :--- | :--- |
| **0** | 1 | `[]` | None | None | `[0]` |
| **1** | 2 | `[0]` | $nums[0] < 2 \implies$ Pop 0 | `mid=0`, $L=(0-(-1))=1, R=(1-0)=1 \implies 1 \times 1 \times nums[0] = 1$ | `[1]` |
| **2** | 3 | `[1]` | $nums[1] < 3 \implies$ Pop 1 | `mid=1`, $L=(1-(-1))=2, R=(2-1)=1 \implies 2 \times 1 \times nums[1] = 4$ | `[2]` |
| **3** | $\infty$ (Virtual) | `[2]` | Pop 2 | `mid=2`, $L=(2-(-1))=3, R=(3-2)=1 \implies 3 \times 1 \times nums[2] = 9$ | `[]` |

Total Max = $1 + 4 + 9 = 14$.
Mirror for Min gives Total Min = $1 \times 3 \times 1 + 1 \times 2 \times 2 + 1 \times 1 \times 3 = 3 + 4 + 3 = 10$.
Total Range Sum = $14 - 10 = 4$.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Optimal: Monotonic Stack (Single-Pass Contribution Method):**
  - Runs in strictly $O(N)$ time and $O(N)$ space.
  - Demonstrates mastery of contribution counting and monotonic boundary conditions.
- **Baseline: Quadratic Brute Force:**
  - $O(N^2)$ time and $O(1)$ space. Simple, but fails Senior expectations if scale increases to $N = 10^5$.

#### 4.2 Step-by-Step Natural Progression Flow
1. Compute total max contributions using monotonic stack.
2. Compute total min contributions using monotonic stack.
3. Return `totalMax - totalMin`.

#### 4.3 Alternative Approaches Analysis
- **Segment Tree / Sparse Table:** Query range minimum and maximum for all $\frac{N(N+1)}{2}$ pairs in $O(1)$ per query after $O(N \log N)$ preprocessing. Total time remains $O(N^2)$. Strictly inferior to Monotonic Stack.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Approach | Time Complexity | Aux Space | 64-bit Overflow Safe | Scale $N = 10^5$ Ready |
| :--- | :--- | :--- | :--- | :--- |
| **Monotonic Stack (Optimal)** | $O(N)$ | $O(N)$ | Yes (explicit `long`) | Yes ($\approx 20\text{ms}$) |
| **Running Min/Max Scan** | $O(N^2)$ | $O(1)$ | Yes | TLE ($> 10\text{s}$) |
| **Sparse Table RMQ** | $O(N^2)$ | $O(N \log N)$ | Yes | TLE |

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: LeetCode #2104 - Sum of Subarray Ranges
 * ============================================================================
 * Core Pattern      : Monotonic Stack Contribution Calculus (Linearity of Summation)
 * Time Complexity   : O(N) - Every element pushed and popped from stack at most once
 * Space Complexity  : O(N) Auxiliary Space - Stack stores indices
 * Selection Rule    : Sum of min/max over all contiguous subarrays in optimal linear time.
 * Defensive Traps   : 1. Intermediate products MUST be cast to (long) to prevent 32-bit overflow.
 *                     2. Use strict inequality on one side and non-strict on the other
 *                        to strictly prevent duplicate counting.
 *                     3. Use a virtual N-th iteration to flush remaining stack elements cleanly.
 * ============================================================================
 */

using System;
using System.Collections.Generic;

public class Solution
{
    /// <summary>
    /// Computes the sum of all subarray ranges in optimal O(N) time using Monotonic Stacks.
    /// </summary>
    public long SubArrayRanges(int[] nums)
    {
        // Guard Clause
        if (nums == null || nums.Length <= 1)
        {
            return 0;
        }

        return ComputeSubarrayContribution(nums, isMax: true) - 
               ComputeSubarrayContribution(nums, isMax: false);
    }

    /// <summary>
    /// Computes the total sum of subarray maximums (isMax=true) or minimums (isMax=false).
    /// </summary>
    private static long ComputeSubarrayContribution(int[] nums, bool isMax)
    {
        int n = nums.Length;
        long totalContribution = 0;
        var stack = new Stack<int>();

        // Iterate up to n inclusive to flush all remaining stack elements at index n
        for (int i = 0; i <= n; i++)
        {
            while (stack.Count > 0 && ShouldPop(stack.Peek(), i, nums, isMax))
            {
                int mid = stack.Pop();
                int leftBound = stack.Count == 0 ? -1 : stack.Peek();
                int rightBound = i;

                // Invariant: Combinatorial product of choices for left and right endpoints
                long count = (long)(mid - leftBound) * (rightBound - mid);
                totalContribution += (long)nums[mid] * count;
            }

            stack.Push(i);
        }

        return totalContribution;
    }

    /// <summary>
    /// Monotonic comparison predicate enforcing strict inequality on left, non-strict on right.
    /// </summary>
    private static bool ShouldPop(int stackTopIndex, int currentIndex, int[] nums, bool isMax)
    {
        if (currentIndex == nums.Length)
        {
            return true; // Virtual element forces flush of all remaining elements
        }

        if (isMax)
        {
            // For Max: Pop when current element is greater or equal
            return nums[currentIndex] >= nums[stackTopIndex];
        }
        else
        {
            // For Min: Pop when current element is smaller or equal
            return nums[currentIndex] <= nums[stackTopIndex];
        }
    }
}
```

---

### 6. Senior Pitfalls & Defensive Traps
1. **The 32-Bit Multiplication Overflow Trap:** Writing `count = (mid - leftBound) * (rightBound - mid)`. If both spans are $5 \times 10^4$, their product is $2.5 \times 10^9$, which overflows signed 32-bit `int` into negative numbers before being assigned to `long`. Always cast the first term to `(long)` explicitly: `(long)(mid - leftBound) * (rightBound - mid)`.
2. **Double-Counting Duplicate Elements:** Using strict inequality (`>`) or non-strict (`>=`) on *both* sides. If `nums = [3, 3]`, both `3`s will claim to be the maximum for the full subarray `[3, 3]`, counting its contribution twice. The invariant rule: **Strict inequality on one side, non-strict on the other.**
3. **Missing Remaining Stack Flush:** Forgetting that elements remaining on the stack after the array iteration ends still have valid spans extending all the way to index $N$. Processing up to $i = N$ as a virtual sentinel guarantees complete evaluation.

---

## 139. Search Suggestions System (LeetCode #1268)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core (Amazon / Google flagship search autocomplete) |
| **Pattern Tags** | `#trie` `#binary-search` `#two-pointers` `#lexicographical-sort` |
| **LeetCode Link** | [Search Suggestions System](https://leetcode.com/problems/search-suggestions-system/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** You are given an array of strings `products` and a string `searchWord`. Design a system that suggests at most three product names from `products` after each character of `searchWord` is typed. Suggested products should have a common prefix with `searchWord`. If there are more than three products with a common prefix, return the three lexicographically minimums products. Return a list of lists of the suggested products after each character of `searchWord` is typed.
- **Assumptions & Contracts:**
  - Matches must share the exact case-sensitive prefix typed so far.
  - Exactly 3 suggestions must be returned, or fewer if fewer than 3 matches exist.
  - Suggestions must be sorted in ascending lexicographical order.
- **Key Constraints:**
  - $1 \le products.Length \le 1000$
  - $1 \le products[i].Length \le 3000$
  - $1 \le \sum products[i].Length \le 2 \times 10^4$
  - All strings in `products` are distinct.
  - `products[i]` and `searchWord` consist of lowercase English letters.
  - $1 \le searchWord.Length \le 1000$
- **Senior Edge Cases to Defend:**
  - **No Matching Prefix:** As soon as a prefix has 0 matches, all subsequent longer prefixes typed will also have 0 matches; return empty lists.
  - **Fewer Than 3 Matches:** Prefix matches only 1 or 2 products; return exactly those available.
  - **SearchWord Longer Than Products:** `products = ["cat"]`, `searchWord = "caterpillar"`. Match exists for first 3 characters, then empty.

---

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Sort `products` lexicographically once. As each character of `searchWord` is typed, maintain a contracting window $[left, right]$ using two pointers. Because products are sorted, all valid prefix matches form a contiguous subarray. The first $\min(3, right - left + 1)$ elements starting at $left$ are guaranteed to be the lexicographically smallest.
- **Sample 1:**
  - **Input:** `products = ["mobile","mouse","moneypot","monitor","mousepad"], searchWord = "mouse"`
  - **Output:**
    ```json
    [
      ["mobile","moneypot","monitor"],
      ["mobile","moneypot","monitor"],
      ["mouse","mousepad"],
      ["mouse","mousepad"],
      ["mouse","mousepad"]
    ]
    ```
- **Sample 2:**
  - **Input:** `products = ["havana"], searchWord = "havana"`
  - **Output:** `[["havana"],["havana"],["havana"],["havana"],["havana"],["havana"]]`

---

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine flipping through a physical Oxford English Dictionary. The entire dictionary is sorted alphabetically. When you type `'m'`, you open the book to the `'m'` section, establishing a start page ($left$) and end page ($right$). When you type `'o'`, you narrow your view to words starting with `"mo"`. As you type more letters, the window $[left, right]$ never expands; it strictly shrinks. To show the user the top 3 results, you simply read the first 3 words on page $left$.

#### 3.2 The Naive Bottleneck & Redundant Computation
Filtering all $N$ products from scratch for every character prefix:
- For a query of length $M$, doing a full scan across $N$ strings takes $O(M \cdot N \cdot L)$ time.
- If $N = 1000, M = 1000$, this performs $10^6$ string comparisons and repeated sorting operations.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Contiguous Window Invariant:**
  Because `products` is sorted lexicographically:
  $$\forall i \le j \le k: \text{If } products[i] \text{ and } products[k] \text{ share prefix } P, \text{ then } products[j] \text{ shares prefix } P.$$
  Therefore, all words matching prefix $searchWord[0 \dots t]$ form a **contiguous range** $[left_t, right_t]$.
- **Monotonic Window Shrinking:**
  $$left_0 \le left_1 \le left_2 \dots \le right_2 \le right_1 \le right_0$$
  As $t$ increases from $0$ to $M-1$:
  - Advance $left$ while $products[left]$ does not match $searchWord[t]$ at index $t$.
  - Decrement $right$ while $products[right]$ does not match $searchWord[t]$ at index $t$.
  - The suggestions are simply elements at indices $left, left+1, left+2$ (up to $right$).

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
Sorted Products Array:
[ ... Out of Scope ... ] [ left ................... right ] [ ... Out of Scope ... ]
                               │      │        │
                               ▼      ▼        ▼
                          Pick 1st,  2nd,     3rd  ===> Current Suggestions
```

#### 3.5 State Transition Triggers & Decision Gates
1. **Sort:** `Array.Sort(products, StringComparer.Ordinal)`.
2. **Initialize Cursors:** `left = 0`, `right = products.Length - 1`.
3. **Prefix Loop ($i = 0 \dots searchWord.Length - 1$):**
   - Char $c = searchWord[i]$.
   - While $left \le right$ and $(products[left].Length \le i \text{ or } products[left][i] \neq c)$:
     - `left++`
   - While $left \le right$ and $(products[right].Length \le i \text{ or } products[right][i] \neq c)$:
     - `right--`
   - Collect up to 3 elements from $left$ to $\min(left + 2, right)$.
   - Add list to result.

#### 3.6 Concrete Step-by-Step State Trace
Input: `products = ["mobile","moneypot","monitor","mouse","mousepad"]` (Sorted), `searchWord = "mouse"`.

| Char Typed ($i$) | Prefix | Valid Range $[left, right]$ | Matching Products in Range | Suggestions Picked |
| :---: | :---: | :---: | :--- | :--- |
| **0** (`'m'`) | `"m"` | $[0, 4]$ | All 5 words | `["mobile", "moneypot", "monitor"]` |
| **1** (`'o'`) | `"mo"` | $[0, 4]$ | All 5 words | `["mobile", "moneypot", "monitor"]` |
| **2** (`'u'`) | `"mou"` | $[3, 4]$ (`left` shifted past `mobile`, `moneypot`, `monitor`) | `["mouse", "mousepad"]` | `["mouse", "mousepad"]` |
| **3** (`'s'`) | `"mous"` | $[3, 4]$ | `["mouse", "mousepad"]` | `["mouse", "mousepad"]` |
| **4** (`'e'`) | `"mouse"` | $[3, 4]$ | `["mouse", "mousepad"]` | `["mouse", "mousepad"]` |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1: Two Pointers on Sorted Array (Optimal Space & Minimal Code):**
  - Sorts in $O(N \log N \cdot L)$.
  - Two-pointer shrinking runs in $O(N + M)$ total character comparisons.
  - Auxiliary memory is strictly $O(1)$ beyond the output list.
- **Approach 2: Trie with Top-3 Caching (Optimal Query Time):**
  - Construct a Trie where each node stores a list of up to 3 product names.
  - Query time is $O(M)$ flat.
  - Better for streaming / real-time search backends serving millions of queries over static catalogs.

#### 4.2 Step-by-Step Natural Progression Flow
1. Sort `products` array lexicographically.
2. Initialize `left = 0, right = products.Length - 1`.
3. Iterate $i$ through $searchWord$; shrink $left$ and $right$.
4. Take slice of up to 3 items and append to result list.

#### 4.3 Alternative Approaches Analysis
- **Binary Search Per Prefix:** For each prefix, run `BinarySearch` to find lower bound of prefix, then check next 3 items. Time $O(M \cdot L \log N)$. Excellent, but Two Pointers achieves $O(N)$ total amortized checks without re-searching from index 0.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Approach | Preprocessing Time | Query Time | Auxiliary Space | Read/Write Flexibility |
| :--- | :--- | :--- | :--- | :--- |
| **Two Pointers (Approach 1)** | $O(N \log N \cdot L)$ | $O(N + M)$ | $O(1)$ | Optimal for single user session |
| **Trie with Top-3 (Approach 2)** | $O(N \cdot L)$ | $O(M)$ | $O(N \cdot L)$ | Optimal for multi-query service |
| **Full Scan Filter** | $0$ | $O(M \cdot N \cdot L)$ | $O(1)$ | Poor |

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: LeetCode #1268 - Search Suggestions System
 * ============================================================================
 * Core Pattern      : Lexicographical Sorting + Two-Pointer Contraction Window
 * Time Complexity   : O(N * log N * L + M) where N = products.Length, M = searchWord.Length
 * Space Complexity  : O(1) Auxiliary Space (excluding returned result list)
 * Selection Rule    : Autocomplete / prefix query returning top-K lexicographical items.
 * Defensive Traps   : 1. Defend against string index out-of-range: check products[left].Length > i.
 *                     2. Use StringComparer.Ordinal for standard ASCII lexicographical consistency.
 *                     3. Ensure left <= right before probing product characters.
 * ============================================================================
 */

using System;
using System.Collections.Generic;

public class Solution
{
    /// <summary>
    /// Returns top 3 lexicographical suggestions for each typed prefix of searchWord.
    /// </summary>
    public IList<IList<string>> SuggestedProducts(string[] products, string searchWord)
    {
        // Guard Clauses
        if (products == null || products.Length == 0 || string.IsNullOrEmpty(searchWord))
        {
            return new List<IList<string>>();
        }

        // Structural Invariant: Sort products lexicographically upfront
        Array.Sort(products, StringComparer.Ordinal);

        var result = new List<IList<string>>(capacity: searchWord.Length);
        int left = 0;
        int right = products.Length - 1;

        for (int i = 0; i < searchWord.Length; i++)
        {
            char targetChar = searchWord[i];

            // Invariant Contraction: Advance left boundary past invalid products
            while (left <= right && (products[left].Length <= i || products[left][i] != targetChar))
            {
                left++;
            }

            // Invariant Contraction: Decrement right boundary past invalid products
            while (left <= right && (products[right].Length <= i || products[right][i] != targetChar))
            {
                right--;
            }

            // Harvest up to 3 lexicographically smallest candidates starting from left cursor
            var currentSuggestions = new List<string>(capacity: 3);
            int availableMatches = Math.Min(3, right - left + 1);

            for (int k = 0; k < availableMatches; k++)
            {
                currentSuggestions.Add(products[left + k]);
            }

            result.Add(currentSuggestions);
        }

        return result;
    }
}
```

---

### 6. Senior Pitfalls & Defensive Traps
1. **The String Index Out Of Bounds Crash:** Probing `products[left][i]` without first validating `products[left].Length > i`. If a short product like `"cat"` is in the list and the user types `"catch"`, `products[left][4]` throws `IndexOutOfRangeException`. Always guard length before character indexing!
2. **Culture-Sensitive Sort Discrepancy:** Using default `Array.Sort(products)` without specifying `StringComparer.Ordinal`. In some locales (e.g., Turkish or Spanish), culture-sensitive sorting places accented or capitalized characters in unexpected orders that fail standard algorithmic constraints.
3. **Loop Overrun Beyond Right Cursor:** When adding suggestions, blindly taking 3 items (`left`, `left+1`, `left+2`) without verifying `left + k <= right`. This leaks non-matching products into the suggestions.

---

## 140. Analyze User Website Visit Pattern (LeetCode #1152)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core (Amazon #1 most frequent signature question) |
| **Pattern Tags** | `#hash-table` `#sorting` `#combinations` `#tuple-aggregation` `#tie-breaker` |
| **LeetCode Link** | [Analyze User Website Visit Pattern](https://leetcode.com/problems/analyze-user-website-visit-pattern/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** You are given two string arrays `username` and `website` and an integer array `timestamp`. All the given arrays are of the same length and the tuple `[username[i], timestamp[i], website[i]]` indicates that the user `username[i]` visited the website `website[i]` at time `timestamp[i]`.
  A **3-sequence** is a list of three websites (not necessarily distinct) visited by the same user in increasing order of time: `(w1, w2, w3)` where $t_1 < t_2 < t_3$.
  The **score** of a 3-sequence is the number of **distinct users** that visited that 3-sequence.
  Return the 3-sequence with the largest score. If there is more than one 3-sequence with the same maximum score, return the **lexicographically smallest** one.
- **Assumptions & Contracts:**
  - A user visiting the same 3-sequence multiple times contributes **only 1** to that 3-sequence's score.
  - Within a single user's 3-sequence, websites do NOT need to be distinct (e.g., `["home", "home", "home"]` is valid if that user visited "home" at three different timestamps).
- **Key Constraints:**
  - $3 \le username.Length \le 50$
  - $1 \le username[i].Length, website[i].Length \le 10$
  - $1 \le timestamp[i] \le 10^9$
  - All tuples `[username[i], timestamp[i], website[i]]` are distinct.
  - At least one 3-sequence exists.
- **Senior Edge Cases to Defend:**
  - **Unsorted Timestamps in Input:** The input arrays are NOT sorted chronologically. They must be explicitly sorted by `timestamp` ascending.
  - **Duplicate 3-Sequences per Single User:** E.g., User A visits `[X, Y, Z, X, Y, Z]`. They can form `(X, Y, Z)` multiple ways, but must count **only once** toward `(X, Y, Z)`'s score.
  - **Lexicographical Tie-Breaking Across 3-Tuples:** When scores match, compare website 1; if equal, website 2; if equal, website 3.
  - **Users with Fewer Than 3 Visits:** Cannot form any 3-sequence; must be ignored.

---

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Group chronological visits by user, generate all unique 3-combinations for each user using a local `HashSet`, increment a global histogram of 3-sequence scores, and find the maximum score with lexicographical tie-breaking.
- **Sample 1:**
  - **Input:**
    ```text
    username = ["joe","joe","joe","james","james","james","james","mary","mary","mary"]
    timestamp = [1,2,3,4,5,6,7,8,9,10]
    website = ["home","about","career","home","cart","maps","home","home","about","career"]
    ```
  - **Output:** `["home","about","career"]`
  - **Explanation:**
    - Joe visited: `["home","about","career"]` $\implies$ `(home, about, career)`
    - James visited: `["home","cart","maps","home"]` $\implies$ `(home, cart, maps)`, `(home, cart, home)`, `(home, maps, home)`, `(cart, maps, home)`
    - Mary visited: `["home","about","career"]` $\implies$ `(home, about, career)`
    - `(home, about, career)` was visited by Joe and Mary (Score = 2). All other patterns have Score = 1.
- **Sample 2:**
  - **Input:**
    ```text
    username = ["u1","u1","u1"], timestamp = [1,2,3], website = ["a","b","a"]
    ```
  - **Output:** `["a","b","a"]`

---

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Think of an analytics dashboard for an e-commerce platform tracking customer journeys. Each user leaves a chronological breadcrumb trail of page visits. You want to find the single 3-step navigation flow that the highest number of unique customers shared. If Alice browses `Cart -> Checkout -> Success` five times in a week, she is still just one customer who completed that journey. Thus, each user casts at most one vote for each distinct 3-page journey they took.

#### 3.2 The Naive Bottleneck & Redundant Computation
- Without sorting chronologically first, verifying $t_1 < t_2 < t_3$ requires checking all $O(N^3)$ triples with manual timestamp comparisons.
- Without user-level deduplication sets, tracking scores requires a matrix of user-to-sequence maps.
- Because $N \le 50$, total combinations per user with $K$ visits is $\binom{K}{3} \le \binom{50}{3} = 19,600$, easily executable in milliseconds.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Chronological Linearization:**
  Sorting by `timestamp` once upfront guarantees that for any user's website list, any index triple $i < j < k$ automatically satisfies $timestamp_i < timestamp_j < timestamp_k$.
- **Per-User Set Invariant:**
  For user $u$, generate all combinations:
  $$\text{UserPatterns}_u = \{ (W_i, W_j, W_k) \mid 0 \le i < j < k < \text{Visits}_u.Count \}$$
  Using a `HashSet<(string, string, string)>` ensures $|W|$ counts each distinct pattern at most once per user.
- **Global Histogram:**
  $$\text{GlobalScore}[P] = \sum_{u} \mathbf{1}_{\{P \in \text{UserPatterns}_u\}}$$

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
Data Pipeline Architecture:
Raw Arrays: (User, Time, Web)
      │
      ▼ Step 1: Zip & Chronological Sort by Timestamp
Sorted Event Log: [ (u1, t1, w1), (u2, t2, w2), ... ]
      │
      ▼ Step 2: Group by User
User Journeys: { "joe" -> [w1, w2, w3], "james" -> [w1, w4, w5, w1] }
      │
      ▼ Step 3: Combination Generator + Local Dedup Set per User
Distinct User Triples: User "james" yields Set { (w1,w4,w5), (w1,w4,w1), ... }
      │
      ▼ Step 4: Global Frequency Aggregation
Score Map: { Pattern -> Count }
      │
      ▼ Step 5: ArgMax with Lexicographical Tie-Breaker
Optimal 3-Sequence
```

#### 3.5 State Transition Triggers & Decision Gates
1. **Combine & Sort:** Create array of records `(user, time, site)` and sort by `time`.
2. **User Partitioning:** Populate `Dictionary<string, List<string>> userToSites`.
3. **Combinatorial Generation:**
   - For each user with `sites.Count >= 3`:
     - Initialize `userPatterns = new HashSet<(string, string, string)>()`.
     - 3 nested loops: $i$ from $0$ to $len - 3$, $j$ from $i + 1$ to $len - 2$, $k$ from $j + 1$ to $len - 1$.
     - `userPatterns.Add((sites[i], sites[j], sites[k]))`.
     - For each pattern in `userPatterns`: `patternCounts[p] = patternCounts.GetValueOrDefault(p) + 1`.
4. **ArgMax Extraction:**
   - Track `maxScore` and `bestPattern`.
   - For each `(pattern, score)`:
     - If `score > maxScore` or (`score == maxScore` and `pattern < bestPattern`):
       - `maxScore = score; bestPattern = pattern;`

#### 3.6 Concrete Step-by-Step State Trace
User "joe": `["home", "about", "career"]` $\implies \binom{3}{3} = 1$ combination: `("home", "about", "career")`.
User "mary": `["home", "about", "career"]` $\implies \binom{3}{3} = 1$ combination: `("home", "about", "career")`.

| User | Combinations Generated | Unique Set for User | Global Pattern Counts | Current Best Pattern |
| :--- | :--- | :--- | :--- | :--- |
| **joe** | `(home, about, career)` | `{(home, about, career)}` | `{(home, about, career): 1}` | `(home, about, career)` (Score: 1) |
| **mary** | `(home, about, career)` | `{(home, about, career)}` | `{(home, about, career): 2}` | `(home, about, career)` (Score: 2) |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Zipped Tuple Sorting + Hash Table Counting:** Standard, clean, and robust for production.
- **ValueTuple `(string, string, string)`:** In .NET 8/9, `ValueTuple` implements structural equality and hash codes out of the box, making it the zero-boilerplate key for `Dictionary` and `HashSet`.

#### 4.2 Step-by-Step Natural Progression Flow
1. Bundle input arrays into a struct/tuple array and sort by timestamp.
2. Group ordered websites by username.
3. Generate distinct 3-combinations per user into a local `HashSet`.
4. Aggregate counts in a global dictionary.
5. Determine the winner using a single pass with tuple lexicographical comparison.

#### 4.3 Alternative Approaches Analysis
- **String Delimited Keys:** Storing `"site1#site2#site3"` as a string key. Inefficient because delimiter escaping is required (what if site contains `#`?), plus continuous string formatting allocations generate massive GC churn.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Approach | Time Complexity | Aux Space | Type Safety | Delimiter Collision Risk |
| :--- | :--- | :--- | :--- | :--- |
| **ValueTuple Grouping (Optimal)** | $O(N \log N + U \cdot K^3)$ | $O(N + U \cdot K^3)$ | Strong | None |
| **String Delimited Key Map** | $O(N \log N + U \cdot K^3)$ | $O(N + U \cdot K^3 \cdot L)$ | Weak | High |

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: LeetCode #1152 - Analyze User Website Visit Pattern
 * ============================================================================
 * Core Pattern      : Chronological Grouping + Combinatorial Tuple Counting
 * Time Complexity   : O(N log N + U * K^3) where N = total visits, U = users, K = visits/user
 * Space Complexity  : O(N + U * K^3) for storing user visit lists and 3-sequence counts
 * Selection Rule    : Identifying most common multi-hop user paths across distinct sessions.
 * Defensive Traps   : 1. Input arrays are NOT sorted by timestamp; sort explicitly.
 *                     2. Deduplicate patterns per user so one user cannot vote multiple times.
 *                     3. Implement strict lexicographical comparison for 3-tuples on tie score.
 * ============================================================================
 */

using System;
using System.Collections.Generic;

public class Solution
{
    private readonly record struct Visit(string User, int Timestamp, string Website);

    /// <summary>
    /// Finds the 3-website visit sequence with the highest distinct user score.
    /// </summary>
    public IList<string> MostVisitedPattern(string[] username, int[] timestamp, string[] website)
    {
        // Guard Clauses
        if (username == null || timestamp == null || website == null ||
            username.Length != timestamp.Length || username.Length != website.Length)
        {
            throw new ArgumentException("Input arrays must be non-null and of identical length.");
        }

        int n = username.Length;
        var visits = new Visit[n];

        for (int i = 0; i < n; i++)
        {
            visits[i] = new Visit(username[i], timestamp[i], website[i]);
        }

        // Phase 1: Chronological Ordering Invariant
        Array.Sort(visits, (a, b) => a.Timestamp.CompareTo(b.Timestamp));

        // Phase 2: Group Chronological Visits by User
        var userVisits = new Dictionary<string, List<string>>();
        foreach (var visit in visits)
        {
            if (!userVisits.TryGetValue(visit.User, out var siteList))
            {
                siteList = new List<string>();
                userVisits[visit.User] = siteList;
            }
            siteList.Add(visit.Website);
        }

        // Phase 3: Combinatorial Generation & User-Level Deduplication
        var patternFrequency = new Dictionary<(string, string, string), int>();

        foreach (var sites in userVisits.Values)
        {
            int siteCount = sites.Count;
            if (siteCount < 3)
            {
                continue; // Cannot form a 3-sequence
            }

            // Invariant: HashSet ensures each distinct 3-sequence receives at most 1 vote per user
            var uniqueUserPatterns = new HashSet<(string W1, string W2, string W3)>();

            for (int i = 0; i < siteCount - 2; i++)
            {
                for (int j = i + 1; j < siteCount - 1; j++)
                {
                    for (int k = j + 1; k < siteCount; k++)
                    {
                        uniqueUserPatterns.Add((sites[i], sites[j], sites[k]));
                    }
                }
            }

            // Ingest distinct votes into global ledger
            foreach (var pattern in uniqueUserPatterns)
            {
                patternFrequency[pattern] = patternFrequency.GetValueOrDefault(pattern) + 1;
            }
        }

        // Phase 4: Identify Max Score with Lexicographical Tie-Breaking
        int maxScore = 0;
        (string W1, string W2, string W3) bestPattern = default;

        foreach (var (pattern, score) in patternFrequency)
        {
            if (score > maxScore)
            {
                maxScore = score;
                bestPattern = pattern;
            }
            else if (score == maxScore)
            {
                // Tie-breaker: Choose lexicographically smaller pattern
                if (ComparePatterns(pattern, bestPattern) < 0)
                {
                    bestPattern = pattern;
                }
            }
        }

        return new List<string> { bestPattern.W1, bestPattern.W2, bestPattern.W3 };
    }

    /// <summary>
    /// Lexicographical comparison for 3-tuples of strings.
    /// </summary>
    private static int ComparePatterns(
        (string W1, string W2, string W3) a, 
        (string W1, string W2, string W3) b)
    {
        int c1 = string.CompareOrdinal(a.W1, b.W1);
        if (c1 != 0) return c1;

        int c2 = string.CompareOrdinal(a.W2, b.W2);
        if (c2 != 0) return c2;

        return string.CompareOrdinal(a.W3, b.W3);
    }
}
```

---

### 6. Senior Pitfalls & Defensive Traps
1. **Assuming Input is Sorted by Timestamp:** The most common failure mode in interviews. The problem description lists timestamps, but does NOT guarantee input array order. Always zip and sort by `timestamp`.
2. **The "Duplicate User Vote" Bug:** Incrementing the global count directly inside the 3 nested loops without a per-user `HashSet`. If User A visits `[X, Y, Z, X, Y, Z]`, without the set, User A alone inflates `(X, Y, Z)`'s score to 2, causing incorrect leaderboard rankings.
3. **Improper Lexicographical Tie-Break:** Comparing concatenated strings `a.W1 + a.W2 + a.W3` vs `b.W1 + b.W2 + b.W3`. Catastrophic boundary flaw: `("a", "bc", "d")` concatenates to `"abcd"`, which equals `("ab", "c", "d")`. You must compare element-by-element (`W1` vs `W1`, then `W2` vs `W2`, then `W3` vs `W3`).

---

## 141. Copy List with Random Pointer (LeetCode #138)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core (Amazon / Google / Uber linked list signature) |
| **Pattern Tags** | `#linked-list` `#hash-table` `#interweaving-nodes` `#pointer-manipulation` |
| **LeetCode Link** | [Copy List with Random Pointer](https://leetcode.com/problems/copy-list-with-random-pointer/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** A linked list of length $n$ is given such that each node contains an additional random pointer, which could point to any node in the list, or `null`. Construct a **deep copy** of the list. The deep copy should consist of exactly $n$ brand new nodes, where each new node has its value set to the value of its corresponding original node. Both the `next` and `random` pointer of the new nodes should point to new nodes in the copied list such that the pointers in the original list and copied list represent the same list state. None of the pointers in the new list should point to nodes in the original list. Return the head of the copied linked list.
- **Assumptions & Contracts:**
  - Original list must remain completely intact and unmodified upon return.
  - The `random` pointer may point to any node in the list, to itself (cycle), or to `null`.
  - Deep copy requirement: No node in the cloned list may reference any node in the original list.
- **Key Constraints:**
  - $0 \le n \le 1000$
  - $-10^4 \le Node.val \le 10^4$
  - `Node.random` is `null` or is pointing to some node in the linked list.
- **Senior Edge Cases to Defend:**
  - **`head == null`:** Empty list $\implies$ return `null`.
  - **Self-Referential Random Pointers:** A node whose random pointer points to itself (`curr.random == curr`).
  - **All Random Pointers Null:** Valid list with zero random references.
  - **Two Nodes Pointing to the Same Random Target:** Cloned references must converge on the same cloned target, not instantiate duplicate copies.

---

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Clone a graph/list structure with arbitrary cross-edges.
  - Approach 1 (Hash Map): Map original nodes to cloned nodes $O(N)$ space.
  - Approach 2 (Interweaving): Weave clone nodes directly between original nodes ($A \to A' \to B \to B'$), assign random pointers via `curr.next.random = curr.random.next`, and decouple the lists in $O(1)$ auxiliary space.
- **Sample 1:**
  - **Input:** `head = [[7,null],[13,0],[11,4],[10,2],[1,0]]`
  - **Output:** `[[7,null],[13,0],[11,4],[10,2],[1,0]]`
- **Sample 2:**
  - **Input:** `head = [[1,1],[2,1]]`
  - **Output:** `[[1,1],[2,1]]`

---

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine a paper document where various paragraphs have sticky arrows pointing to other paragraphs. You want to make an identical replica on a fresh sheet of paper.
- *The Hash Map Method:* You copy each paragraph onto a new sheet, and write down an index translation table in your ledger: *"Old Paragraph 3 corresponds to New Paragraph 3"*.
- *The Interweaving Method ($O(1)$ Space):* Instead of a ledger, you staple the replica paragraph immediately below each original paragraph on the same page. When you see an arrow from Original Paragraph 1 to Original Paragraph 4, you instantly know where the replica arrow goes: it points to the paragraph stapled immediately behind Original Paragraph 4 (`curr.random.next`). Once all arrows are wired, you unstaple the replica pages to form the new document.

#### 3.2 The Naive Bottleneck & Redundant Computation
- A naive clone that traverses `random` pointers recursively without a visited registry will cycle infinitely on cyclic pointer graphs.
- While the $O(N)$ hash table solution is standard, in memory-constrained systems (e.g., embedded firmware, Linux kernel memory managers), allocating an $O(N)$ hash table with node reference hashes and GC overhead is unacceptable. The interweaving algorithm achieves $O(1)$ extra space.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **The Interweaving Association Invariant:**
  By mutating the original list such that:
  $$\forall u: u.next = u' \quad \text{and} \quad u'.next = \text{originalNext}(u)$$
  the copy of any node $u$ is physically stored at $u.next$.
- **Zero-Lookup Random Pointer Wiring:**
  $$u'.random = (u.random \neq \text{null}) \ ? \ u.random.next : \text{null}$$
  Because $u.random$ is an original node, $u.random.next$ is guaranteed to be its exact clone! This eliminates the hash map entirely.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
Phase 1: Node Interweaving
Original:  [ A ] ───────────────────────► [ B ] ───────────────────────► [ C ]
             │                              │                              │
             ▼                              ▼                              ▼
Weaved:    [ A ] ──► [ A' ] ────────────► [ B ] ──► [ B' ] ────────────► [ C ] ──► [ C' ]

Phase 2: Random Pointer Cross-Wiring
If A.random points to C:
Then A'.random (which is A.next.random) = A.random.next (which is C')!

Phase 3: Decoupling (Restoring Original & Extracting Clone)
Original:  [ A ] ────────► [ B ] ────────► [ C ]
Clone   :  [ A' ] ───────► [ B' ] ───────► [ C' ]
```

#### 3.5 State Transition Triggers & Decision Gates
1. **Guard Clause:** If `head == null`, return `null`.
2. **Pass 1 (Interweave):**
   - Cursor `curr = head`.
   - While `curr != null`:
     - `copy = new Node(curr.val)`
     - `copy.next = curr.next`
     - `curr.next = copy`
     - `curr = copy.next`
3. **Pass 2 (Wire Random):**
   - Cursor `curr = head`.
   - While `curr != null`:
     - If `curr.random != null`: `curr.next.random = curr.random.next`.
     - `curr = curr.next.next`.
4. **Pass 3 (Decouple Lists):**
   - `copyHead = head.next`, `curr = head`, `copyCurr = copyHead`.
   - While `curr != null`:
     - `curr.next = curr.next.next`
     - `copyCurr.next = copyCurr.next != null ? copyCurr.next.next : null`
     - `curr = curr.next`
     - `copyCurr = copyCurr.next`
   - Return `copyHead`.

#### 3.6 Concrete Step-by-Step State Trace
Input: `A(1, random: B) -> B(2, random: B) -> null`

| Phase | Active Pointers | State of List Structure | Action Taken |
| :--- | :--- | :--- | :--- |
| **Pass 1** | `curr = A` | `A -> A' -> B -> null` | Interweave `A'` after `A` |
| | `curr = B` | `A -> A' -> B -> B' -> null` | Interweave `B'` after `B` |
| **Pass 2** | `curr = A` | `A.random = B` $\implies A'.random = B.next = B'$ | Wire $A'.random \to B'$ |
| | `curr = B` | `B.random = B` $\implies B'.random = B.next = B'$ | Wire $B'.random \to B'$ (Self-loop) |
| **Pass 3** | Unweaving | Original: `A -> B -> null`<br>Clone: `A' -> B' -> null` | Restores original pointers and detaches clone |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1: In-Place Interweaving ($O(1)$ Aux Space - Senior Bar):**
  - Preferred in senior / lead interviews because it proves deep pointer manipulation skills without relying on associative data structures.
- **Approach 2: Hash Map ($O(N)$ Aux Space):**
  - Practical, clean, easier to debug, but consumes $O(N)$ heap memory for dictionary buckets.

#### 4.2 Step-by-Step Natural Progression Flow
1. Interweave cloned nodes into original list.
2. Traverse list setting clone random pointers via `curr.random.next`.
3. Separate the merged list back into the original list and the clone list.
4. Return cloned head.

#### 4.3 Alternative Approaches Analysis
- **Recursive DFS with Visited Map:** Treats linked list as a general directed graph. Works, but uses $O(N)$ stack frames and $O(N)$ hash table storage.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Approach | Time Complexity | Aux Space | Mutates Original Temporarily | GC Allocation Pressure |
| :--- | :--- | :--- | :--- | :--- |
| **In-Place Interweaving** | $O(N)$ | $O(1)$ | Yes (Restored at exit) | Zero (only clone nodes) |
| **Hash Map Iterative** | $O(N)$ | $O(N)$ | No | Moderate (hash table) |
| **Recursive Graph DFS** | $O(N)$ | $O(N)$ | No | High (stack + table) |

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: LeetCode #138 - Copy List with Random Pointer
 * ============================================================================
 * Core Pattern      : Three-Pass In-Place Interweaving (O(1) Auxiliary Space)
 * Time Complexity   : O(N) - Three linear sweeps over the N-element list
 * Space Complexity  : O(1) Auxiliary Memory (Output clone nodes excluded)
 * Selection Rule    : Deep copy of graph-like pointer list under zero extra memory budget.
 * Defensive Traps   : 1. Always restore original list's next pointers back to their initial state.
 *                     2. Check curr.random != null before accessing curr.random.next.
 *                     3. Guard against null head immediately.
 * ============================================================================
 */

using System;

// Definition for a Node provided by LeetCode
public class Node
{
    public int val;
    public Node next;
    public Node random;

    public Node(int _val)
    {
        val = _val;
        next = null;
        random = null;
    }
}

public class Solution
{
    /// <summary>
    /// Creates a deep copy of a linked list with random pointers in O(N) time and O(1) extra space.
    /// </summary>
    public Node CopyRandomList(Node head)
    {
        // Guard Clause: Empty list yields null
        if (head == null)
        {
            return null;
        }

        // ====================================================================
        // PASS 1: Interweave cloned nodes directly next to their original node
        // Invariant: Original node at 'curr' has its clone located at 'curr.next'
        // ====================================================================
        Node curr = head;
        while (curr != null)
        {
            Node nextOriginal = curr.next;
            Node clone = new Node(curr.val)
            {
                next = nextOriginal
            };
            curr.next = clone;
            curr = nextOriginal;
        }

        // ====================================================================
        // PASS 2: Assign random pointers to the cloned nodes
        // Invariant: clone.random = original.random.next
        // ====================================================================
        curr = head;
        while (curr != null)
        {
            if (curr.random != null)
            {
                // Invariant: curr.random is an original node; curr.random.next is its clone
                curr.next.random = curr.random.next;
            }

            // Move to the next original node
            curr = curr.next.next;
        }

        // ====================================================================
        // PASS 3: Decouple the interwoven lists (Restore Original & Extract Clone)
        // Invariant: Restore original.next and wire clone.next
        // ====================================================================
        curr = head;
        Node cloneHead = head.next;
        Node cloneCurr = cloneHead;

        while (curr != null)
        {
            // Restore original list pointer
            curr.next = curr.next.next;

            // Wire clone list pointer
            if (cloneCurr.next != null)
            {
                cloneCurr.next = cloneCurr.next.next;
            }

            // Advance cursors
            curr = curr.next;
            cloneCurr = cloneCurr.next;
        }

        return cloneHead;
    }
}

/// <summary>
/// Alternative Approach: One-Pass Dictionary Mapping.
/// Space Complexity: O(N) Auxiliary Space.
/// </summary>
public class SolutionHashMap
{
    public Node CopyRandomList(Node head)
    {
        if (head == null) return null;

        var map = new System.Collections.Generic.Dictionary<Node, Node>();
        Node curr = head;

        // Pass 1: Instantiate all cloned nodes
        while (curr != null)
        {
            map[curr] = new Node(curr.val);
            curr = curr.next;
        }

        // Pass 2: Connect next and random pointers using map lookup
        curr = head;
        while (curr != null)
        {
            if (curr.next != null)
            {
                map[curr].next = map[curr.next];
            }
            if (curr.random != null)
            {
                map[curr].random = map[curr.random];
            }
            curr = curr.next;
        }

        return map[head];
    }
}
```

---

### 6. Senior Pitfalls & Defensive Traps
1. **Failing to Restore the Original List:** In Pass 3, extracting the cloned list without properly resetting `curr.next = curr.next.next` leaves the caller's input data structure corrupted. In enterprise code, mutating caller inputs without restoration is an unacceptable side-effect.
2. **Null Reference on `curr.random.next`:** Writing `curr.next.random = curr.random.next` without first verifying `if (curr.random != null)`. If `curr.random` is null, this throws an immediate `NullReferenceException`.
3. **Decoupling Loop Pointer Desynchronization:** Trying to advance `cloneCurr` after `curr` has already advanced past its end, causing off-by-one pointer skips. Always update `curr.next`, update `cloneCurr.next`, and then advance both cursors to their newly assigned next references.
