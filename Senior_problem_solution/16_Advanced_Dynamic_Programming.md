# Phase 16: Advanced Dynamic Programming

> **Focus:** 2D Grid Topologies, String Alignment Geometry (LCS & Levenshtein Edit Distance), State Machine DP (Stock Trading with Cooldown), Knapsack Algebraic Reductions, and Space-Optimized Rolling Buffers.  
> **Source Curriculum:** [`Senior_dsa_question_list.md`](../Senior_dsa_question_list.md) — Phase 16 (Problems #86–#92)

---
## 86. Unique Paths (LeetCode #62)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⚪ Reinforcement |
| **Pattern Tags** | `#2d-dp` `#grid-paths` `#space-optimization` `#combinatorics` |
| **LeetCode Link** | [Unique Paths](https://leetcode.com/problems/unique-paths/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** There is a robot on an $m \times n$ grid located at the top-left corner $(0, 0)$. The robot can only move either down or right at any point. Return the number of possible unique paths to reach the bottom-right corner $(m - 1, n - 1)$.
- **Key Constraints:**
  - $1 \le m, n \le 100$.
  - Result fits within a standard 32-bit signed integer ($\le 2 \times 10^9$).
- **Senior Edge Cases to Defend:**
  - $m = 1$ or $n = 1 \implies 1$ single straight line path.
  - Combinatorics intermediate overflow: In $\binom{m+n-2}{m-1}$, calculating factorials directly overflows 64-bit integers. Multiplication and division must be interleaved iteratively using `long`.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** 2D DAG Path Counting: $dp[r][c] = dp[r - 1][c] + dp[r][c - 1]$. The grid is topologically ordered naturally by row-major traversal. Can be compressed to a single rolling 1D row, or solved algebraically in $O(\min(m, n))$ via combinations $\binom{m + n - 2}{m - 1}$.
- **Sample 1:**
  - **Input:** `m = 3, n = 7`
  - **Output:** `28`
- **Sample 2:**
  - **Input:** `m = 3, n = 2`
  - **Output:** `3`
  - **Explanation:** Down-Right-Down, Down-Down-Right, Right-Down-Down.

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine city street intersections laid out on a perfect rectangular grid. Because one-way laws permit traffic to move strictly South (Down) or East (Right), every trip from the origin $(0, 0)$ to destination $(m - 1, n - 1)$ is a Directed Acyclic Graph (DAG) traversal. Any vehicle arriving at intersection $(r, c)$ must have arrived from either $(r - 1, c)$ (from the North) or $(r, c - 1)$ (from the West). By the fundamental Rule of Sum in combinatorics:
$$\text{Paths}(r, c) = \text{Paths}(r - 1, c) + \text{Paths}(r, c - 1)$$
This is Pascal's Triangle rotated 45 degrees, where each cell is the sum of its two predecessors.

#### 3.2 The Naive Bottleneck & Redundant Computation
A naive recursive exploration branches into two options at every coordinate:
$$\text{Count}(r, c) = \text{Count}(r + 1, c) + \text{Count}(r, c + 1)$$
This generates a complete binary decision tree of depth $(m - 1) + (n - 1) = m + n - 2$. The total number of leaf evaluations is:
$$T(m, n) = O\left(2^{m + n}\right)$$
For a $20 \times 20$ grid, $2^{38} \approx 2.75 \times 10^{11}$ calls, resulting in catastrophic timeout. Since there are only $m \times n$ unique grid coordinates, memoization or bottom-up DP reduces this to $O(m \times n)$.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**Space Optimization via In-Place Rolling Row:**
Notice that computing cell $(r, c)$ requires only:
1. The cell directly above it: $(r - 1, c)$.
2. The cell directly to its left: $(r, c - 1)$.
If we maintain a single 1D array `row` of size $n$:
- Before updating `row[c]`, its existing value is precisely the value from row $r - 1$ at column $c$ (the cell from above).
- `row[c - 1]` has already been updated in the current row pass, so it represents the cell from the left!
- Therefore, the transition collapses into a single addition:
  $$\text{row}[c] = \text{row}[c] + \text{row}[c - 1]$$
This eliminates the 2D matrix entirely, reducing auxiliary space from $O(M \times N)$ to $O(N)$ (or $O(\min(M, N))$ by swapping dimensions).

**Combinatorics Closed-Form Formula:**
Total steps required $= (m - 1) + (n - 1) = m + n - 2$. Exactly $m - 1$ steps must be downward moves. Thus:
$$\text{UniquePaths}(m, n) = \binom{m + n - 2}{m - 1} = \frac{(m + n - 2)!}{(m - 1)! (n - 1)!}$$

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
2D GRID TO 1D ROLLING ROW REDUCTION:
2D Representation:
        c - 1      c
r - 1 [   .   ] [ Top ]
r     [ Left  ] [ (r, c) ]

1D In-Place Rolling Row:
row: [ row[0] ... row[c - 1] (Updated: Left) | row[c] (Previous Row: Top) | ... row[n - 1] ]
Transition: row[c] += row[c - 1]
```

- `c`: Column cursor advancing from $1$ to $n - 1$.
- `row[c]`: Before addition, holds the path count from the cell directly above ($r - 1, c$).
- `row[c - 1]`: Holds the finalized path count for the cell directly to the left in the current row ($r, c - 1$).
- **Invariant:** When evaluating `row[c]`, `row[0 .. c - 1]` contains finalized values for row $r$, while `row[c .. n - 1]` contains finalized values from row $r - 1$.

#### 3.5 State Transition Triggers & Decision Gates
1. **Base Row Seed:** Initialize `row` of size $n$ with all $1$s (only 1 path to reach any cell along the first row: move strictly right).
2. **Row Step Gate:** For $r = 1 \dots m - 1$:
   - For $c = 1 \dots n - 1$: `row[c] += row[c - 1]`.
3. **Terminal Gate:** After $m - 1$ row passes, `row[n - 1]` holds the answer.

#### 3.6 Concrete Step-by-Step State Trace
Trace input: $m = 3, n = 4$. Initial `row = [1, 1, 1, 1]`.

| Pass | State of `row` array | Explanation |
| :---: | :--- | :--- |
| **Row 0** | `[1, 1, 1, 1]` | Base case: Only 1 way along top edge (all Right moves) |
| **Row 1 ($r=1$)** | | |
| $c=1$ | `row[1] = 1 + 1 = 2` | 1 (from top) + 1 (from left) |
| $c=2$ | `row[2] = 1 + 2 = 3` | 1 (from top) + 2 (from left) |
| $c=3$ | `row[3] = 1 + 3 = 4` | 1 (from top) + 3 (from left) |
| **After Row 1** | `[1, 2, 3, 4]` | Row 1 paths resolved |
| **Row 2 ($r=2$)** | | |
| $c=1$ | `row[1] = 2 + 1 = 3` | 2 (from top) + 1 (from left) |
| $c=2$ | `row[2] = 3 + 3 = 6` | 3 (from top) + 3 (from left) |
| $c=3$ | `row[3] = 4 + 6 = 10` | 4 (from top) + 6 (from left) |
| **After Row 2** | `[1, 3, 6, 10]` | Destination reached! |

Final return: `row[3] = 10`.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (1D Rolling Row DP):** The interview standard. Simple to explain, $O(M \times N)$ time, $O(N)$ space, impervious to arithmetic overflow issues.
- **Approach 2 (Combinatorics Closed Formula):** Optimal for large grids with small dimensions ($O(\min(M, N))$ time, $O(1)$ space). Must defend against intermediate multiplication overflow by alternating multiply and divide steps.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Setup & Boundaries:** Handle $m = 1$ or $n = 1$ (return 1). Allocate array `row` of size $n$, filled with 1.
- **Step 2: Main Exploration Loop:** Outer loop $r \in [1 \dots m - 1]$. Inner loop $c \in [1 \dots n - 1]$.
- **Step 3: State Accumulation:** `row[c] += row[c - 1]`.
- **Step 4: Resolution & Return:** Return `row[n - 1]`.

#### 4.3 Alternative Approaches Analysis
- **Approach 2: Combinatorics Formula:**
  - Total steps $N = m + n - 2$. Number of down steps $k = \min(m - 1, n - 1)$.
  - Accumulate $\prod_{i=1}^k \frac{N - k + i}{i}$ using a 64-bit integer (`long`), multiplying the numerator first, then dividing by $i$ (which is guaranteed to divide evenly at each step).

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: 1D Rolling Row DP | Approach 2: Combinatorics Formula |
| :--- | :--- | :--- |
| **Time Complexity (Best / Avg / Worst)** | $O(M \times N)$ | $O(\min(M, N))$ |
| **Auxiliary Space** | $O(N)$ array storage | $O(1)$ scalar variables |
| **Output Space** | $O(1)$ scalar integer | $O(1)$ scalar integer |
| **Cache Locality** | Sequential cache line reads | Pure CPU register execution |
| **In-Place Mutability** | Non-destructive | Non-destructive |
| **Streaming Suitability** | High | High |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Problem #86 - Unique Paths
// Core Pattern: 2D Grid Dynamic Programming / Rolling 1D Array Space Compression
// Primary Invariant: row[c] = row[c] (from above) + row[c - 1] (from left)
// Space Defense: 1D buffer of size n reduces O(M x N) memory to O(N).
// Arithmetic Defense: Combinatorics implementation uses long and alternating division.
// ============================================================================
```

#### Implementation 1: 1D Rolling Row DP (Production Standard)
```csharp
public class Solution
{
    public int UniquePaths(int m, int n)
    {
        // Guard Clauses: Single row or single column allows only 1 straight path
        if (m <= 0 || n <= 0) return 0;
        if (m == 1 || n == 1) return 1;

        // Ensure n is the smaller dimension to optimize memory usage to O(min(M, N))
        if (n > m)
        {
            return UniquePaths(n, m);
        }

        // 1D array representing the active row's path counts
        int[] row = new int[n];

        // Base case: There is exactly 1 way to reach any cell in the first row (move right)
        Array.Fill(row, 1);

        // Process rows 1 through m - 1
        for (int r = 1; r < m; r++)
        {
            for (int c = 1; c < n; c++)
            {
                // Invariant: Before update, row[c] holds the count from the cell above (r - 1, c).
                // row[c - 1] holds the newly updated count from the cell to the left (r, c - 1).
                row[c] += row[c - 1];
            }
        }

        return row[n - 1];
    }
}
```

#### Implementation 2: Combinatorics $O(\min(M, N))$ Formula
```csharp
public class SolutionCombinatorics
{
    public int UniquePaths(int m, int n)
    {
        if (m <= 0 || n <= 0) return 0;
        if (m == 1 || n == 1) return 1;

        // Total steps = (m - 1) down + (n - 1) right = m + n - 2
        int totalSteps = m + n - 2;
        // Symmetry property: C(N, k) == C(N, N - k); choose smaller k
        int k = Math.Min(m - 1, n - 1);

        long result = 1;

        // Interleave multiplication and division to prevent intermediate 64-bit overflow
        for (int i = 1; i <= k; i++)
        {
            result = result * (totalSteps - k + i) / i;
        }

        return (int)result;
    }
}
```

---

## 87. Minimum Path Sum (LeetCode #64)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | 🔥 High |
| **Pattern Tags** | `#2d-dp` `#grid-cost-min` `#in-place-tabulation` |
| **LeetCode Link** | [Minimum Path Sum](https://leetcode.com/problems/minimum-path-sum/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given an $m \times n$ `grid` filled with non-negative numbers, find a path from top-left to bottom-right that minimizes the sum of all numbers along its path. You can only move either down or right.
- **Key Constraints:**
  - $m == grid.Length, n == grid[0].Length \in [1, 200]$.
  - $0 \le grid[i][j] \le 200$.
- **Senior Edge Cases to Defend:**
  - $1 \times 1$ grid: Returns `grid[0][0]`.
  - Single row or single column: Only one possible path exists (pure prefix sum).
  - Mutability contract: In production systems, clarify whether mutating the input `grid` in-place is permitted by the caller.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** 2D Min-Cost DAG: $dp[r][c] = grid[r][c] + \min(dp[r - 1][c], dp[r][c - 1])$. Every cell is reached via either its top or left neighbor.
- **Sample 1:**
  - **Input:** `grid = [[1, 3, 1], [1, 5, 1], [4, 2, 1]]`
  - **Output:** `7`
  - **Explanation:** Path $1 \to 3 \to 1 \to 1 \to 1$ minimizes the sum to 7.
- **Sample 2:**
  - **Input:** `grid = [[1, 2, 3], [4, 5, 6]]`
  - **Output:** `12`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Picture a mountainous landscape where each cell elevation corresponds to an energy toll. You must travel from the northwest peak $(0, 0)$ to the southeast valley $(m - 1, n - 1)$, carrying a battery pack. Because gravity and time allow movement only southward (Down) or eastward (Right), your arrival at cell $(r, c)$ can only happen from $(r - 1, c)$ or $(r, c - 1)$. To minimize accumulated energy cost, you should greedily pick whichever neighbor incurred the least cumulative cost. This optimal substructure guarantees that local min-cost combination leads to the global minimum.

#### 3.2 The Naive Bottleneck & Redundant Computation
A brute-force DFS visits every possible path from origin to destination:
$$\text{MinCost}(r, c) = grid[r][c] + \min(\text{MinCost}(r + 1, c), \text{MinCost}(r, c + 1))$$
This generates $O(2^{m + n})$ paths. Subproblems for overlapping downstream paths (such as reaching $(2, 2)$ from $(1, 2)$ vs $(2, 1)$) are evaluated repeatedly from scratch. DP memoizes minimum cumulative cost per cell, evaluating each cell exactly once in $O(M \times N)$ time.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**Cell Cost Relaxation:**
$$dp[r][c] = grid[r][c] + \min(dp[r - 1][c], dp[r][c - 1])$$
- **Boundary Invariants:**
  - For cells in the first row ($r = 0, c > 0$), movement can only come from the left: $dp[0][c] = dp[0][c - 1] + grid[0][c]$.
  - For cells in the first column ($c = 0, r > 0$), movement can only come from above: $dp[r][0] = dp[r - 1][0] + grid[r][0]$.
- **Space Compression:**
  Using a 1D rolling array `dp[c]`:
  - When starting row $r$, `dp[0]` updates via `dp[0] += grid[r][0]` (only top predecessor exists).
  - For $c > 0$, `dp[c]` (before update) holds the min cost from row $r - 1$ at column $c$ (top), while `dp[c - 1]` holds the newly resolved min cost from the left.
  - Thus: `dp[c] = grid[r][c] + Math.Min(dp[c], dp[c - 1])`.
  - Reduces space to $O(N)$ without mutating the input.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
2D TO 1D MIN-COST RELAXATION:
Previous Row State:  dp[c] (Cost to arrive from top)
Current Row Left:    dp[c - 1] (Cost to arrive from left)

Relaxation Gate:
dp[c] = grid[r][c] + min(dp[c], dp[c - 1])
```

- `dp[c]`: Stores the minimum cumulative cost to reach column $c$ in the current row being processed.
- `grid[r][c]`: The local transit cost of cell $(r, c)$.
- **Invariant:** After processing column $c$ in row $r$, `dp[0 .. c]` holds the true optimal path sums for row $r$, while `dp[c + 1 .. n - 1]` preserves optimal path sums from row $r - 1$.

#### 3.5 State Transition Triggers & Decision Gates
1. **Origin Gate:** `dp[0] = grid[0][0]`.
2. **First Row Initialization:** For $c = 1 \dots n - 1$: `dp[c] = dp[c - 1] + grid[0][c]`.
3. **Row Progression Gate:** For $r = 1 \dots m - 1$:
   - Column 0: `dp[0] += grid[r][0]`.
   - Columns $1 \dots n - 1$: `dp[c] = grid[r][c] + Math.Min(dp[c], dp[c - 1])`.
4. **Resolution Gate:** Return `dp[n - 1]`.

#### 3.6 Concrete Step-by-Step State Trace
Trace input:
`grid = [[1, 3, 1],`
`        [1, 5, 1],`
`        [4, 2, 1]]`

| Row | $c=0$ | $c=1$ | $c=2$ | `dp` Array State |
| :---: | :---: | :---: | :---: | :--- |
| **Row 0** | $1$ | $1 + 3 = 4$ | $4 + 1 = 5$ | `[1, 4, 5]` |
| **Row 1** | $1 + 1 = 2$ | $5 + \min(4, 2) = 7$ | $1 + \min(5, 7) = 6$ | `[2, 7, 6]` |
| **Row 2** | $2 + 4 = 6$ | $2 + \min(7, 6) = 8$ | $1 + \min(6, 8) = 7$ | `[6, 8, 7]` |

Final return: `dp[2] = 7`.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (1D Space-Optimized DP):** Recommended for production systems. It does not mutate input parameters (thread-safe, side-effect free) and operates in $O(N)$ auxiliary memory.
- **Approach 2 (In-Place Mutation DP):** Optimal for embedded or strictly memory-constrained environments where auxiliary allocation is forbidden ($O(1)$ space). Modifies `grid` directly.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Setup & Boundaries:** Handle 1-cell or empty inputs. Allocate `dp` array of size $n$.
- **Step 2: Seed First Row:** Compute prefix sums across `grid[0]`.
- **Step 3: Main Row Loop:** For each row $r$ from $1$ to $m - 1$:
  - Update `dp[0]` with `grid[r][0]`.
  - For $c = 1 \dots n - 1$, update `dp[c] = grid[r][c] + Math.Min(dp[c], dp[c - 1])`.
- **Step 4: Resolution & Return:** Return `dp[n - 1]`.

#### 4.3 Alternative Approaches Analysis
- **Approach 2: In-Place Matrix Tabulation ($O(1)$ Extra Space):**
  - Accumulate prefix sums in row 0: `grid[0][c] += grid[0][c - 1]`.
  - Accumulate prefix sums in col 0: `grid[r][0] += grid[r - 1][0]`.
  - For internal cells: `grid[r][c] += Math.Min(grid[r - 1][c], grid[r][c - 1])`.
  - Return `grid[m - 1][n - 1]`.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: 1D Space-Optimized DP | Approach 2: In-Place Mutation |
| :--- | :--- | :--- |
| **Time Complexity (Best / Avg / Worst)** | $O(M \times N)$ | $O(M \times N)$ |
| **Auxiliary Space** | $O(N)$ array storage | $O(1)$ strictly in-place |
| **Output Space** | $O(1)$ scalar integer | $O(1)$ scalar integer |
| **Cache Locality** | Sequential cache line reads | Sequential cache line reads |
| **In-Place Mutability** | Non-destructive (safe) | Destructive (mutates input) |
| **Streaming Suitability** | Moderate | Low |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Problem #87 - Minimum Path Sum
// Core Pattern: 2D Grid Dynamic Programming / Min-Cost Relaxation
// Primary Invariant: dp[c] = grid[r][c] + min(dp[c] (top), dp[c-1] (left))
// Immutability Defense: 1D rolling array avoids mutating caller's input matrix.
// Boundary Defense: Handles 1x1, 1xN, and Mx1 grids cleanly.
// ============================================================================
```

#### Implementation 1: 1D Space-Optimized DP (Production Standard)
```csharp
public class Solution
{
    public int MinPathSum(int[][] grid)
    {
        // Guard Clauses
        if (grid == null || grid.Length == 0 || grid[0].Length == 0)
        {
            return 0;
        }

        int m = grid.Length;
        int n = grid[0].Length;

        // 1D DP table holding the minimum path sums for the current row
        int[] dp = new int[n];

        // Seed origin
        dp[0] = grid[0][0];

        // Initialize the first row: Only horizontal moves from the left are possible
        for (int c = 1; c < n; c++)
        {
            dp[c] = dp[c - 1] + grid[0][c];
        }

        // Process subsequent rows
        for (int r = 1; r < m; r++)
        {
            // First column: Only vertical moves from above are possible
            dp[0] += grid[r][0];

            for (int c = 1; c < n; c++)
            {
                // Invariant: dp[c] holds the min sum to reach cell above (r - 1, c).
                // dp[c - 1] holds the min sum to reach cell to the left (r, c - 1).
                dp[c] = grid[r][c] + Math.Min(dp[c], dp[c - 1]);
            }
        }

        return dp[n - 1];
    }
}
```

#### Implementation 2: In-Place Grid Tabulation ($O(1)$ Extra Space)
```csharp
public class SolutionInPlace
{
    public int MinPathSum(int[][] grid)
    {
        if (grid == null || grid.Length == 0 || grid[0].Length == 0)
        {
            return 0;
        }

        int m = grid.Length;
        int n = grid[0].Length;

        // In-place cumulative sum along the top row
        for (int c = 1; c < n; c++)
        {
            grid[0][c] += grid[0][c - 1];
        }

        // In-place cumulative sum along the left column
        for (int r = 1; r < m; r++)
        {
            grid[r][0] += grid[r - 1][0];
        }

        // In-place relaxation for all interior cells
        for (int r = 1; r < m; r++)
        {
            for (int c = 1; c < n; c++)
            {
                grid[r][c] += Math.Min(grid[r - 1][c], grid[r][c - 1]);
            }
        }

        return grid[m - 1][n - 1];
    }
}
```

---

## 88. Longest Common Subsequence (LeetCode #1143)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#2d-dp` `#string-alignment` `#subsequence-grid` `#space-optimization` |
| **LeetCode Link** | [Longest Common Subsequence](https://leetcode.com/problems/longest-common-subsequence/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given two strings `text1` and `text2`, return the length of their longest common subsequence. If no common subsequence exists, return 0.
- **Key Constraints:**
  - $1 \le text1.Length, text2.Length \le 1000$.
  - Strings consist of lowercase English letters.
- **Senior Edge Cases to Defend:**
  - Completely disjoint strings (e.g. `"abc"` and `"def"` $\implies 0$).
  - Identical strings (returns `text1.Length`).
  - One string is a substring of the other.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** 2D String Alignment Grid: If characters match, extend diagonally: $1 + dp[i - 1][j - 1]$. Else, take maximum of horizontal or vertical reduction: $\max(dp[i - 1][j], dp[i][j - 1])$.
- **Sample 1:**
  - **Input:** `text1 = "abcde", text2 = "ace"`
  - **Output:** `3`
  - **Explanation:** The longest common subsequence is `"ace"`.
- **Sample 2:**
  - **Input:** `text1 = "abc", text2 = "abc"`
  - **Output:** `3`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine two parallel ticker tapes running through an optical scanner. One tape displays string $A$ and the other displays string $B$. You have read heads at positions $i$ on tape $A$ and $j$ on tape $B$:
- **Match Case ($A[i-1] == B[j-1]$):** The scanner detects identical symbols. Both read heads advance simultaneously diagonally, locking in 1 point of subsequence length and reducing the subproblem to the remaining prefixes.
- **Mismatch Case ($A[i-1] \neq B[j-1]$):** The characters diverge. Since they cannot be paired, you explore two alternative paths: either drop the current character of $A$ and retain $B$ (vertical move), or drop the current character of $B$ and retain $A$ (horizontal move), taking the maximum of the two results.

#### 3.2 The Naive Bottleneck & Redundant Computation
A brute-force solution generates all $2^M$ subsequences of `text1` and checks if each exists as a subsequence in `text2` ($O(N)$ validation time):
$$T(M, N) = O(2^M \times N)$$
For $M = 1000$, $2^{1000} \approx 10^{301}$, which is impossible. Overlapping subproblems abound: comparing prefixes $(i, j)$ occurs thousands of times along distinct recursive paths. 2D DP memoizes the $M \times N$ grid, reducing runtime to $O(M \times N) \le 10^6$ operations.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**2D Grid Alignment Recurrence:**
Let $dp[i][j]$ be the length of the longest common subsequence of prefixes $text1[0 \dots i - 1]$ and $text2[0 \dots j - 1]$:
$$dp[i][j] = \begin{cases} 1 + dp[i - 1][j - 1] & \text{if } text1[i - 1] == text2[j - 1] \\ \max(dp[i - 1][j], dp[i][j - 1]) & \text{if } text1[i - 1] \neq text2[j - 1] \end{cases}$$
**Space Compression Invariant:**
Evaluating row $i$ references only row $i - 1$ (from diagonal $dp[i-1][j-1]$ and top $dp[i-1][j]$) and the current row $i$ (from left $dp[i][j-1]$).
- We maintain two rolling 1D buffers: `prev` and `curr` of length $N + 1$.
- By ensuring `text2` is the shorter string ($N \le M$), auxiliary space drops to $O(\min(M, N))$, occupying less than 4 KB of memory.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
2D ALIGNMENT CELL GEOMETRY:
                j - 1               j
i - 1 [ prev[j - 1]: Diagonal ] [ prev[j]: Top (Drop A[i-1]) ]
i     [ curr[j - 1]: Left     ] [ curr[j]: Active Cell       ]

Decision:
If text1[i - 1] == text2[j - 1] ==> curr[j] = 1 + prev[j - 1]
Else                            ==> curr[j] = max(prev[j], curr[j - 1])
```

- `prev[j]`: LCS length for prefix $text1[0 \dots i - 2]$ with $text2[0 \dots j - 1]$.
- `curr[j]`: LCS length for prefix $text1[0 \dots i - 1]$ with $text2[0 \dots j - 1]$.
- **Base Case Invariant:** $dp[0][j] = 0$ and $dp[i][0] = 0$ (empty prefix has 0 common elements with any string).

#### 3.5 State Transition Triggers & Decision Gates
For $i = 1 \dots M$:
1. **Inner Scan:** For $j = 1 \dots N$:
   - **Match Gate:** If $text1[i - 1] == text2[j - 1]$, `curr[j] = 1 + prev[j - 1]`.
   - **Mismatch Gate:** Else, `curr[j] = Math.Max(prev[j], curr[j - 1])`.
2. **Buffer Swap:** Set `prev = curr` (reusing memory buffers).
3. **Resolution:** Return `prev[N]`.

#### 3.6 Concrete Step-by-Step State Trace
Trace input: `text1 = "abcde"`, `text2 = "ace"`. $M = 5, N = 3$.

| $i$ | Character $text1[i-1]$ | `curr[1]` (`'a'`) | `curr[2]` (`'c'`) | `curr[3]` (`'e'`) | `prev` buffer after row |
| :---: | :---: | :---: | :---: | :---: | :--- |
| **0** | `""` | 0 | 0 | 0 | `[0, 0, 0, 0]` |
| **1** | `'a'` | $1+0 = 1$ (Match) | $\max(0, 1) = 1$ | $\max(0, 1) = 1$ | `[0, 1, 1, 1]` |
| **2** | `'b'` | $\max(1, 0) = 1$ | $\max(1, 1) = 1$ | $\max(1, 1) = 1$ | `[0, 1, 1, 1]` |
| **3** | `'c'` | $\max(1, 0) = 1$ | $1+1 = 2$ (Match) | $\max(1, 2) = 2$ | `[0, 1, 2, 2]` |
| **4** | `'d'` | $\max(1, 0) = 1$ | $\max(2, 1) = 2$ | $\max(2, 2) = 2$ | `[0, 1, 2, 2]` |
| **5** | `'e'` | $\max(1, 0) = 1$ | $\max(2, 1) = 2$ | $1+2 = 3$ (Match) | `[0, 1, 2, 3]` |

Final answer: `3` (subsequence `"ace"`).

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Two-Row Rolling DP):** Production optimal. $O(M \times N)$ time, $O(\min(M, N))$ space. Extreme cache efficiency, minimal memory allocation.
- **Approach 2 (Full 2D Matrix with Backtracking):** Essential when the interviewer asks to **reconstruct the actual subsequence string**. The full table allows backtracking from $(M, N)$ back to $(0, 0)$.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Setup & Boundaries:** If either string is empty, return 0. Swap strings if necessary to ensure `text2` is the shorter string.
- **Step 2: Buffer Allocation:** Allocate two 1D integer arrays `prev` and `curr` of length $N + 1$.
- **Step 3: Nested Exploration:** Loop $i \in [1 \dots M]$ and $j \in [1 \dots N]$. Apply match/mismatch gates.
- **Step 4: Buffer Rotation & Return:** Swap `(prev, curr)` at the end of each outer loop. Return `prev[N]`.

#### 4.3 Alternative Approaches Analysis
- **Full 2D Table Backtracking:**
  - Allocate `dp = new int[M + 1, N + 1]`.
  - Populate full table.
  - Backtrack starting at $(M, N)$: If $text1[i-1] == text2[j-1]$, prepend char to result, move $(i-1, j-1)$. Else if $dp[i-1][j] \ge dp[i][j-1]$, move $i--$, else $j--$.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: Two-Row Rolling DP | Approach 2: Full 2D Table Backtracking |
| :--- | :--- | :--- |
| **Time Complexity (Best / Avg / Worst)** | $O(M \times N)$ | $O(M \times N)$ |
| **Auxiliary Space** | $O(\min(M, N))$ | $O(M \times N)$ |
| **Output Space** | $O(1)$ scalar integer | $O(\min(M, N))$ reconstructed string |
| **Cache Locality** | High (fits in L1 cache) | Low (large 2D table cache thrashing) |
| **In-Place Mutability** | Non-destructive | Non-destructive |
| **Streaming Suitability** | High (streaming one string against another) | Low |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Problem #88 - Longest Common Subsequence
// Core Pattern: 2D String Alignment DP / Two-Row Space Compression
// Primary Invariant: If match: 1 + prev[j-1]; Else: max(prev[j], curr[j-1])
// Space Defense: Swapping arguments guarantees O(min(M, N)) auxiliary memory.
// Cache Defense: Two alternating 1D buffers maximize L1 CPU cache residency.
// ============================================================================
```

#### Implementation 1: Two-Row Rolling DP (Production Optimal)
```csharp
public class Solution
{
    public int LongestCommonSubsequence(string text1, string text2)
    {
        // Guard Clauses
        if (string.IsNullOrEmpty(text1) || string.IsNullOrEmpty(text2))
        {
            return 0;
        }

        // Memory Optimization: Guarantee text2 is the shorter string
        // Reduces auxiliary space complexity to O(min(M, N))
        if (text1.Length < text2.Length)
        {
            return LongestCommonSubsequence(text2, text1);
        }

        int m = text1.Length;
        int n = text2.Length;

        // Two rolling row buffers representing dp[i - 1] and dp[i]
        int[] prev = new int[n + 1];
        int[] curr = new int[n + 1];

        for (int i = 1; i <= m; i++)
        {
            char c1 = text1[i - 1];

            for (int j = 1; j <= n; j++)
            {
                char c2 = text2[j - 1];

                // Invariant Gate:
                // If characters match: Extend diagonal alignment by 1
                // If characters differ: Take max of dropping c1 (prev[j]) or dropping c2 (curr[j - 1])
                if (c1 == c2)
                {
                    curr[j] = 1 + prev[j - 1];
                }
                else
                {
                    curr[j] = Math.Max(prev[j], curr[j - 1]);
                }
            }

            // Swap buffer references to advance state horizon without reallocation
            int[] temp = prev;
            prev = curr;
            curr = temp;
        }

        return prev[n];
    }
}
```

#### Implementation 2: Full 2D Table with Subsequence Reconstruction
```csharp
public class SolutionReconstruction
{
    public (int Length, string Subsequence) LcsWithReconstruction(string text1, string text2)
    {
        if (string.IsNullOrEmpty(text1) || string.IsNullOrEmpty(text2))
        {
            return (0, string.Empty);
        }

        int m = text1.Length;
        int n = text2.Length;
        int[,] dp = new int[m + 1, n + 1];

        for (int i = 1; i <= m; i++)
        {
            for (int j = 1; j <= n; j++)
            {
                if (text1[i - 1] == text2[j - 1])
                {
                    dp[i, j] = 1 + dp[i - 1, j - 1];
                }
                else
                {
                    dp[i, j] = Math.Max(dp[i - 1, j], dp[i, j - 1]);
                }
            }
        }

        // Backtrack to reconstruct the actual longest common subsequence string
        var sb = new System.Text.StringBuilder();
        int r = m, c = n;

        while (r > 0 && c > 0)
        {
            if (text1[r - 1] == text2[c - 1])
            {
                sb.Append(text1[r - 1]);
                r--;
                c--;
            }
            else if (dp[r - 1, c] >= dp[r, c - 1])
            {
                r--;
            }
            else
            {
                c--;
            }
        }

        char[] chars = sb.ToString().ToCharArray();
        Array.Reverse(chars);
        return (dp[m, n], new string(chars));
    }
}
```

---

## 89. Edit Distance (LeetCode #72)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🔴 Hard |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#2d-dp` `#levenshtein-distance` `#string-transformation` |
| **LeetCode Link** | [Edit Distance](https://leetcode.com/problems/edit-distance/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given two strings `word1` and `word2`, return the minimum number of operations required to convert `word1` to `word2`. Permitted operations:
  - Insert a character
  - Delete a character
  - Replace a character
- **Key Constraints:**
  - $0 \le word1.Length, word2.Length \le 500$.
  - Strings consist of lowercase English letters.
- **Senior Edge Cases to Defend:**
  - Either string empty ($word1.Length == 0 \implies word2.Length$ inserts; $word2.Length == 0 \implies word1.Length$ deletes).
  - Both strings identical (returns 0).

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Classical Levenshtein Distance: 3-way branch corresponding to Insert ($dp[i][j - 1]$), Delete ($dp[i - 1][j]$), and Replace ($dp[i - 1][j - 1]$).
- **Sample 1:**
  - **Input:** `word1 = "horse", word2 = "ros"`
  - **Output:** `3`
  - **Explanation:**
    1. `horse` $\to$ `rorse` (replace 'h' with 'r')
    2. `rorse` $\to$ `rose` (remove 'r')
    3. `rose` $\to$ `ros` (remove 'e')
- **Sample 2:**
  - **Input:** `word1 = "intention", word2 = "execution"`
  - **Output:** `5`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine a word processor's spellchecker. To convert word $A$ into word $B$, you place a cursor at the end of both strings ($i$ on $A$, $j$ on $B$). You want to find the cheapest sequence of keystrokes:
1. **Free Match:** If $A[i-1] == B[j-1]$, the characters already match. No keystroke required! Move both cursors diagonally: $dp[i-1][j-1]$.
2. **Replacement:** Change $A[i-1]$ to match $B[j-1]$. Cost is 1 + subproblem $dp[i-1][j-1]$ (both characters consumed).
3. **Deletion:** Delete $A[i-1]$. Cost is 1 + subproblem $dp[i-1][j]$ ($A[i-1]$ discarded, $B$ remains unconsumed).
4. **Insertion:** Insert $B[j-1]$ into $A$. Cost is 1 + subproblem $dp[i][j-1]$ ($B[j-1]$ matched and consumed, $A$ remains).
Because every edit operation maps directly to one of these three directional grid steps, the minimum edit distance is strictly the shortest weighted path through an $(M+1) \times (N+1)$ transformation grid.

#### 3.2 The Naive Bottleneck & Redundant Computation
A recursive branching model without caching evaluates:
$$T(m, n) = T(m-1, n-1) + T(m-1, n) + T(m, n-1) + O(1)$$
This yields a ternary decision tree of depth $\max(M, N)$, resulting in $O(3^{\max(M, N)})$ operations. For strings of length 500, $3^{500} \approx 3.6 \times 10^{238}$, which is astronomically intractable. By recording optimal edit distances for the $(M+1) \times (N+1)$ prefix pairs, DP executes in $O(M \times N) \le 2.5 \times 10^5$ operations.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**Levenshtein Recurrence:**
$$dp[i][j] = \begin{cases} dp[i - 1][j - 1] & \text{if } word1[i - 1] == word2[j - 1] \\ 1 + \min\Big( dp[i - 1][j - 1], \; dp[i - 1][j], \; dp[i][j - 1] \Big) & \text{if } word1[i - 1] \neq word2[j - 1] \end{cases}$$
**1D Space Compression with Diagonal Caching:**
To compute row $i$ in a 1D array `dp[j]`:
- `dp[j]` currently holds the value from the row above: $dp[i - 1][j]$ (Delete).
- `dp[j - 1]` holds the newly updated value from the left: $dp[i][j - 1]$ (Insert).
- The diagonal value $dp[i - 1][j - 1]$ (Replace) was overwritten when column $j - 1$ was updated!
- By preserving the overwritten diagonal in a scalar variable `prevDiagonal`, we can execute the full Levenshtein transition using a **single 1D array of size $N + 1$**, dropping auxiliary space from $O(M \times N)$ to $O(N)$.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
1D LEVENSHTEIN ARRAY WITH DIAGONAL CACHE:
dp array: [ dp[0] ... dp[j - 1] (Insert) | dp[j] (Delete) ... dp[N] ]
                         ^
                  prevDiagonal (Replace / Match: cell [i - 1, j - 1])

Transitions:
If word1[i - 1] == word2[j - 1]:
    dp[j] = prevDiagonal
Else:
    dp[j] = 1 + min(prevDiagonal, min(dp[j], dp[j - 1]))
```

- `dp[j]`: Before update, stores $dp[i - 1][j]$ (cost of deleting $word1[i-1]$).
- `dp[j - 1]`: Stores $dp[i][j - 1]$ (cost of inserting $word2[j-1]$).
- `prevDiagonal`: Stores $dp[i - 1][j - 1]$ (cost of replacing or matching).
- **Base Cases:** $dp[j] = j$ for row 0 (inserting $j$ characters to form prefix of length $j$). Column 0 base case: $dp[0] = i$ (deleting $i$ characters).

#### 3.5 State Transition Triggers & Decision Gates
1. **Base Array Seed:** `dp[j] = j` for $j \in [0 \dots N]$.
2. **Row Progression:** For $i = 1 \dots M$:
   - Cache `prevDiagonal = dp[0]`.
   - Update `dp[0] = i`.
   - For $j = 1 \dots N$:
     - Save `temp = dp[j]`.
     - Apply Match Gate or 3-Way Min Gate.
     - Update `prevDiagonal = temp`.
3. **Resolution:** Return `dp[N]`.

#### 3.6 Concrete Step-by-Step State Trace
Trace input: `word1 = "horse"`, `word2 = "ros"`.
Initial `dp` (Row 0): `[0, 1, 2, 3]` (target prefixes `""`, `"r"`, `"ro"`, `"ros"`).

| Row $i$ | Char $word1[i-1]$ | $dp[0]$ | $dp[1]$ (`'r'`) | $dp[2]$ (`'o'`) | $dp[3]$ (`'s'`) | Notes |
| :---: | :---: | :---: | :---: | :---: | :---: | :--- |
| **0** | `""` | 0 | 1 | 2 | 3 | Base insertions |
| **1** | `'h'` | 1 | $1+\min(0, 1, 1) = 1$ | $1+\min(1, 2, 1) = 2$ | $1+\min(2, 3, 2) = 3$ | All replace/insert |
| **2** | `'o'` | 2 | $1+\min(1, 1, 2) = 2$ | $1$ (**Match!** prevDiag=1) | $1+\min(2, 3, 1) = 2$ | Match at 'o' |
| **3** | `'r'` | 3 | $2$ (**Match!** prevDiag=2) | $1+\min(1, 1, 2) = 2$ | $1+\min(2, 2, 2) = 3$ | Match at 'r' |
| **4** | `'s'` | 4 | $1+\min(3, 2, 4) = 3$ | $1+\min(2, 2, 3) = 3$ | $2$ (**Match!** prevDiag=2) | Match at 's' |
| **5** | `'e'` | 5 | $1+\min(4, 3, 5) = 4$ | $1+\min(3, 3, 4) = 4$ | $1+\min(2, 2, 4) = 3$ | Delete 'e' |

Final edit distance: `3`.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (1D Space-Optimized DP):** Optimal for production. Reduces memory from $O(M \times N)$ to $O(N)$ using a single cached scalar variable `prevDiagonal`.
- **Approach 2 (Full 2D Grid DP):** Best when the interviewer requires the **explicit sequence of edit operations** (diff generator), as the 2D grid enables backtracking to print `"Insert 'x'"`, `"Delete 'y'"`, etc.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Setup & Boundaries:** Check if either string is empty; return the other's length directly.
- **Step 2: Table Allocation:** Allocate array `dp` of size $N + 1$. Fill with $0, 1, 2, \dots, N$.
- **Step 3: Exploration Loop:** Outer loop $i \in [1 \dots M]$. Inner loop $j \in [1 \dots N]$.
- **Step 4: Diagonal State Rotation:** Cache `temp = dp[j]`, update `dp[j]`, update `prevDiagonal = temp`.
- **Step 5: Resolution & Return:** Return `dp[N]`.

#### 4.3 Alternative Approaches Analysis
- **Approach 2: Full 2D Table with Edit Traceback:**
  - Allocate `dp = new int[M + 1, N + 1]`.
  - Fill grid using standard Levenshtein formula.
  - Backtrack from $(M, N)$ to $(0, 0)$ checking whether optimal transition came from $(i-1, j-1)$ (match/replace), $(i-1, j)$ (delete), or $(i, j-1)$ (insert).

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: 1D Space-Optimized DP | Approach 2: Full 2D Grid Backtracking |
| :--- | :--- | :--- |
| **Time Complexity (Best / Avg / Worst)** | $O(M \times N)$ | $O(M \times N)$ |
| **Auxiliary Space** | $O(N)$ array storage | $O(M \times N)$ matrix |
| **Output Space** | $O(1)$ scalar integer | $O(M + N)$ operation diff list |
| **Cache Locality** | High (single array fits in L1 cache) | Low (large 2D table) |
| **In-Place Mutability** | Non-destructive | Non-destructive |
| **Streaming Suitability** | High | Low |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Problem #89 - Edit Distance
// Core Pattern: 2D Levenshtein Distance / Diagonal-Cached 1D Compression
// Primary Invariant: If match: prevDiagonal; Else: 1 + min(Replace, Delete, Insert)
// Space Defense: prevDiagonal cache variable collapses 2D matrix to single 1D array.
// Boundary Defense: Empty string cases return length of non-empty string.
// ============================================================================
```

#### Implementation 1: 1D Space-Optimized DP (Production Standard)
```csharp
public class Solution
{
    public int MinDistance(string word1, string word2)
    {
        // Guard Clauses
        if (word1 == null) word1 = string.Empty;
        if (word2 == null) word2 = string.Empty;

        int m = word1.Length;
        int n = word2.Length;

        // Trivial boundary cases
        if (m == 0) return n;
        if (n == 0) return m;

        // Ensure word2 is the shorter string to optimize space to O(min(M, N))
        if (n > m)
        {
            return MinDistance(word2, word1);
        }

        // dp[j] stores edit distance from word1 prefix to word2[0 .. j - 1]
        int[] dp = new int[n + 1];

        // Base case: Converting an empty word1 to word2 prefix requires j insertions
        for (int j = 0; j <= n; j++)
        {
            dp[j] = j;
        }

        for (int i = 1; i <= m; i++)
        {
            // prevDiagonal caches dp[i - 1][j - 1] before it gets overwritten
            int prevDiagonal = dp[0];

            // Base case: Converting word1[0 .. i - 1] to empty word2 requires i deletions
            dp[0] = i;

            char c1 = word1[i - 1];

            for (int j = 1; j <= n; j++)
            {
                // Save current dp[j] (which represents dp[i - 1][j]) before overwriting
                int temp = dp[j];
                char c2 = word2[j - 1];

                if (c1 == c2)
                {
                    // Free match: Characters are identical, inherit diagonal cost directly
                    dp[j] = prevDiagonal;
                }
                else
                {
                    // Invariant Gate: 1 + min(
                    //   Replace: prevDiagonal (dp[i-1][j-1]),
                    //   Delete:  dp[j]        (dp[i-1][j]),
                    //   Insert:  dp[j-1]      (dp[i][j-1])
                    // )
                    int minOperations = Math.Min(prevDiagonal, Math.Min(dp[j], dp[j - 1]));
                    dp[j] = 1 + minOperations;
                }

                // Advance diagonal cache for next column
                prevDiagonal = temp;
            }
        }

        return dp[n];
    }
}
```

#### Implementation 2: Full 2D Table with Edit Script Reconstruction
```csharp
public class SolutionReconstruction
{
    public (int Distance, List<string> Operations) MinDistanceWithScript(string word1, string word2)
    {
        int m = word1.Length;
        int n = word2.Length;

        int[,] dp = new int[m + 1, n + 1];

        for (int i = 0; i <= m; i++) dp[i, 0] = i;
        for (int j = 0; j <= n; j++) dp[0, j] = j;

        for (int i = 1; i <= m; i++)
        {
            for (int j = 1; j <= n; j++)
            {
                if (word1[i - 1] == word2[j - 1])
                {
                    dp[i, j] = dp[i - 1, j - 1];
                }
                else
                {
                    dp[i, j] = 1 + Math.Min(dp[i - 1, j - 1], Math.Min(dp[i - 1, j], dp[i, j - 1]));
                }
            }
        }

        // Backtrack to extract operation sequence
        var ops = new List<string>();
        int r = m, c = n;

        while (r > 0 || c > 0)
        {
            if (r > 0 && c > 0 && word1[r - 1] == word2[c - 1])
            {
                r--;
                c--;
            }
            else if (r > 0 && c > 0 && dp[r, c] == 1 + dp[r - 1, c - 1])
            {
                ops.Add($"Replace '{word1[r - 1]}' at index {r - 1} with '{word2[c - 1]}'");
                r--;
                c--;
            }
            else if (r > 0 && dp[r, c] == 1 + dp[r - 1, c])
            {
                ops.Add($"Delete '{word1[r - 1]}' at index {r - 1}");
                r--;
            }
            else
            {
                ops.Add($"Insert '{word2[c - 1]}' at index {r}");
                c--;
            }
        }

        ops.Reverse();
        return (dp[m, n], ops);
    }
}
```

---

## 90. Decode Ways (LeetCode #91)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | 🔥 High |
| **Pattern Tags** | `#1d-dp` `#string-decoding` `#valid-prefix-states` |
| **LeetCode Link** | [Decode Ways](https://leetcode.com/problems/decode-ways/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** A message containing letters from A-Z can be encoded into numbers using: `'A' -> "1"`, ..., `'Z' -> "26"`. Given a string `s` containing only digits, return the number of ways to decode it.
- **Key Constraints:**
  - $1 \le s.Length \le 100$.
  - `s` contains only digits and may contain leading zero(s).
- **Senior Edge Cases to Defend:**
  - Leading zero (e.g. `"0"`, `"06"` $\implies 0$ ways, as '0' cannot map to any character).
  - Isolated zeroes (e.g. `"10"`, `"20"` $\implies 1$ way; `"30"`, `"00"` $\implies 0$ ways).
  - Valid double digits: Only numbers between $10$ and $26$ can be decoded as two-character tokens.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** 1D DP Jump Transitions: Similar to Climbing Stairs with conditional gates. From index $i$, leap 1 step if $s[i] \in ['1'..'9']$, leap 2 steps if $s[i-1 \dots i] \in [10..26]$.
- **Sample 1:**
  - **Input:** `s = "226"`
  - **Output:** `3`
  - **Explanation:** `"BZ"` (2 26), `"VF"` (22 6), or `"BBF"` (2 2 6).
- **Sample 2:**
  - **Input:** `s = "06"`
  - **Output:** `0`
  - **Explanation:** `"06"` cannot be mapped to `"F"` because of the leading zero (`"6"` is different from `"06"`).

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine a telegraph receiver decoding an incoming stream of numbers. Every letter $A-Z$ is encoded as a 1-digit code ($1-9$) or a 2-digit code ($10-26$). As you scan from left to right, you stand at index $i$:
- Can you consume the single digit $s[i]$ as an independent letter? Yes, provided $s[i] \neq '0'$. If so, all valid decodings reaching step $i - 1$ safely extend to step $i$.
- Can you consume the two-digit pair $s[i-1 \dots i]$ as a compound letter? Yes, provided the combined integer value lies strictly between $10$ and $26$. If so, all valid decodings reaching step $i - 2$ safely extend to step $i$.
Because of the conditional filters on step 1 and step 2, this is a **Gated Fibonacci Sequence**.

#### 3.2 The Naive Bottleneck & Redundant Computation
A recursive DFS parses 1 or 2 digits at each step:
$$\text{Decode}(i) = [s[i] \neq '0'] \times \text{Decode}(i + 1) + [10 \le s[i..i+1] \le 26] \times \text{Decode}(i + 2)$$
For a string of all ones (`"1111111111"`), every position branches into two valid choices, creating a complete Fibonacci tree of depth $N$:
$$T(N) = O(2^N)$$
For $N = 100$, $2^{100} \approx 1.26 \times 10^{30}$. By recognizing that state $i$ depends only on $i - 1$ and $i - 2$, we can collapse computation to a linear scan of $O(N)$ time and $O(1)$ auxiliary space.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**Conditional Transition Invariant:**
Let $dp[i]$ be the number of ways to decode the prefix $s[0 \dots i - 1]$:
$$dp[i] = \underbrace{[s[i - 1] \neq '0'] \times dp[i - 1]}_{\text{Single Digit Gate}} + \underbrace{[10 \le s[i - 2 \dots i - 1] \le 26] \times dp[i - 2]}_{\text{Two Digit Gate}}$$
- **State Compression:** Because $dp[i]$ references only $dp[i - 1]$ and $dp[i - 2]$, we maintain only two integer registers: `prev1` (representing $dp[i - 1]$) and `prev2` (representing $dp[i - 2]$).
- **Zero Absorption Invariant:** When $s[i - 1] == '0'$, the single-digit contribution is strictly 0. If the two-digit contribution also evaluates to 0 (e.g. `"30"` or `"00"`), `current` becomes 0, and the entire downstream decoding collapses to 0.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
GATED DECODING HORIZON:
s:  ... [ s[i - 2] ] [ s[i - 1] ] [ s[i] ... ]
          prev2         prev1       Active Digit
                      (i - 1)          (i)

Gate 1 (Single Digit): If s[i - 1] in ['1'..'9']  ==> current += prev1
Gate 2 (Two Digits):   If s[i-2..i-1] in [10..26] ==> current += prev2
```

- `prev2`: Number of decodings for prefix of length $i - 2$. Initialized to 1 (empty prefix).
- `prev1`: Number of decodings for prefix of length $i - 1$. Initialized to 1 (if $s[0] \neq '0'$).
- `current`: Number of decodings for prefix of length $i$.
- **Invariant:** After processing index $i$, `prev1` represents the exact count of valid decodings for $s[0 \dots i - 1]$.

#### 3.5 State Transition Triggers & Decision Gates
For each index $i$ from $1$ to $N - 1$:
1. **Reset Gate:** `current = 0`.
2. **Single-Digit Gate:** Let $d_1 = s[i] - '0'$. If $d_1 \in [1 \dots 9]$, `current += prev1`.
3. **Two-Digit Gate:** Let $d_2 = (s[i - 1] - '0') \times 10 + d_1$. If $d_2 \in [10 \dots 26]$, `current += prev2`.
4. **Early Exit Gate:** If `current == 0`, the message is invalid; terminate and return 0.
5. **Shift Gate:** `prev2 = prev1; prev1 = current;`.

#### 3.6 Concrete Step-by-Step State Trace
Trace input: `s = "226"`. Length = 3.
Base cases: $s[0] = '2' \implies$ `prev2 = 1, prev1 = 1`.

| $i$ | Digit $s[i]$ | Prior Char $s[i-1]$ | Single Digit ($d_1$) | Two Digits ($d_2$) | `current` calculation | `(prev2, prev1)` after shift |
| :---: | :---: | :---: | :---: | :---: | :---: | :---: |
| **Init** | — | — | — | — | — | `prev2 = 1, prev1 = 1` |
| **1** | `'2'` | `'2'` | $2 \in [1..9] \implies +1$ | $22 \in [10..26] \implies +1$ | $current = 1 + 1 = 2$ | `prev2 = 1, prev1 = 2` |
| **2** | `'6'` | `'2'` | $6 \in [1..9] \implies +2$ | $26 \in [10..26] \implies +1$ | $current = 2 + 1 = 3$ | `prev2 = 2, prev1 = 3` |

Final return: `prev1 = 3`.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Two-Variable Space Optimized):** Optimal standard. $O(N)$ time, $O(1)$ auxiliary space. Direct linear scan, zero object allocations, early termination on invalid digit configurations.
- **Approach 2 (1D DP Array):** Use when the interviewer requests intermediate state auditing or step-by-step decoding tracing. Incurs $O(N)$ auxiliary heap memory.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Setup & Boundaries:** If $s$ is empty or starts with `'0'`, return 0 immediately.
- **Step 2: Register Initialization:** Set `prev2 = 1, prev1 = 1`.
- **Step 3: Exploration Loop:** Loop $i$ from $1$ to $N - 1$.
- **Step 4: Condition Gates:** Test $d_1 \in [1..9]$ and $d_2 \in [10..26]$. Sum contributions.
- **Step 5: Resolution & Return:** Return `prev1`.

#### 4.3 Alternative Approaches Analysis
- **Approach 2: 1D DP Array:**
  - Allocate `dp = new int[N + 1]`.
  - $dp[0] = 1, dp[1] = s[0] == '0' ? 0 : 1$.
  - For $i = 2 \dots N$: check single digit $s[i-1]$ and two digits $s[i-2..i-1]$.
  - Return $dp[N]$.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: Two-Variable Rolling DP | Approach 2: 1D DP Array |
| :--- | :--- | :--- |
| **Time Complexity (Best / Avg / Worst)** | $O(1)$ / $O(N)$ / $O(N)$ | $O(N)$ / $O(N)$ / $O(N)$ |
| **Auxiliary Space** | $O(1)$ scalar variables | $O(N)$ array storage |
| **Output Space** | $O(1)$ scalar integer | $O(1)$ scalar integer |
| **Cache Locality** | Pure CPU register execution | Sequential heap array access |
| **In-Place Mutability** | Non-destructive | Non-destructive |
| **Streaming Suitability** | High (processes digits online) | Low |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Problem #90 - Decode Ways
// Core Pattern: 1D Dynamic Programming / Gated Fibonacci State Reduction
// Primary Invariant: dp[i] = (validSingle ? prev1 : 0) + (validDouble ? prev2 : 0)
// Leading Zero Defense: s[0] == '0' or invalid double zero returns 0 immediately.
// Space Defense: Two rolling scalar variables achieve O(1) auxiliary space.
// ============================================================================
```

#### Implementation 1: Two-Variable Space Optimized (Production Standard)
```csharp
public class Solution
{
    public int NumDecodings(string s)
    {
        // Guard Clause: Empty string or leading zero cannot be decoded
        if (string.IsNullOrEmpty(s) || s[0] == '0')
        {
            return 0;
        }

        int n = s.Length;

        // prev2 corresponds to dp[i - 2] (base case: empty prefix has 1 valid decoding)
        int prev2 = 1;
        // prev1 corresponds to dp[i - 1] (base case: first character has 1 valid decoding)
        int prev1 = 1;

        for (int i = 1; i < n; i++)
        {
            int current = 0;
            int singleDigit = s[i] - '0';
            int twoDigits = (s[i - 1] - '0') * 10 + singleDigit;

            // Gate 1: Valid single digit decoding ('1' .. '9')
            if (singleDigit >= 1 && singleDigit <= 9)
            {
                current += prev1;
            }

            // Gate 2: Valid two digit decoding ("10" .. "26")
            if (twoDigits >= 10 && twoDigits <= 26)
            {
                current += prev2;
            }

            // Early Exit: If neither gate opened, string cannot be decoded further
            if (current == 0)
            {
                return 0;
            }

            // Shift historical registers forward
            prev2 = prev1;
            prev1 = current;
        }

        return prev1;
    }
}
```

#### Implementation 2: 1D DP Array (Explicit State Auditing)
```csharp
public class SolutionArray
{
    public int NumDecodings(string s)
    {
        if (string.IsNullOrEmpty(s) || s[0] == '0') return 0;

        int n = s.Length;
        int[] dp = new int[n + 1];

        dp[0] = 1; // Empty string base case
        dp[1] = 1; // First char already validated != '0'

        for (int i = 2; i <= n; i++)
        {
            int singleDigit = s[i - 1] - '0';
            int twoDigits = (s[i - 2] - '0') * 10 + singleDigit;

            if (singleDigit >= 1 && singleDigit <= 9)
            {
                dp[i] += dp[i - 1];
            }

            if (twoDigits >= 10 && twoDigits <= 26)
            {
                dp[i] += dp[i - 2];
            }
        }

        return dp[n];
    }
}
```

---

## 91. Target Sum (LeetCode #494)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | 🔥 High |
| **Pattern Tags** | `#0-1-knapsack` `#subset-sum-reduction` `#1d-dp` |
| **LeetCode Link** | [Target Sum](https://leetcode.com/problems/target-sum/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given an integer array `nums` and an integer `target`, build an expression by assigning either `'+'` or `'-'` before each number. Return the number of expressions that evaluate to `target`.
- **Key Constraints:**
  - $1 \le nums.Length \le 20$.
  - $0 \le nums[i] \le 1000, 0 \le \sum nums[i] \le 1000$.
  - $-1000 \le target \le 1000$.
- **Senior Edge Cases to Defend:**
  - $|target| > \sum nums$: Mathematically impossible to reach target, return 0.
  - $(\sum nums + target)$ is odd: Cannot split into integer subsets, return 0.
  - Zeroes in `nums`: Each zero doubles the number of valid subsets ($+0$ and $-0$ both equal 0).

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Algebraic Reduction to 0-1 Knapsack Subset Sum:
  $$P - N = \text{target} \implies P - (\text{total} - P) = \text{target} \implies 2P = \text{total} + \text{target} \implies P = \frac{\text{total} + \text{target}}{2}$$
- **Sample 1:**
  - **Input:** `nums = [1, 1, 1, 1, 1], target = 3`
  - **Output:** `5`
  - **Explanation:** Five ways to assign symbols to make sum 3:
    `-1+1+1+1+1 = 3`, `+1-1+1+1+1 = 3`, etc.
- **Sample 2:**
  - **Input:** `nums = [1], target = 1`
  - **Output:** `1`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine splitting an assortment of weights into two separate pans of a balance scale: the positive pan $P$ and the negative pan $N$. The net displacement is $P - N = target$. Because every weight must be placed in exactly one pan, $P + N = totalSum$.
Adding the two equations together:
$$(P - N) + (P + N) = target + totalSum \implies 2P = totalSum + target \implies P = \frac{totalSum + target}{2}$$
Suddenly, the complex problem of exploring combinations of positive and negative signs collapses completely into: **How many subsets of `nums` sum up to exactly $P$?** This is the exact definition of the standard **0-1 Knapsack Count** problem!

#### 3.2 The Naive Bottleneck & Redundant Computation
A brute-force recursive backtracking algorithm branches into $+$ and $-$ at each index:
$$\text{Solve}(i, currentSum) = \text{Solve}(i + 1, currentSum + nums[i]) + \text{Solve}(i + 1, currentSum - nums[i])$$
With $N = 20$, the binary tree has $2^{20} \approx 1.05 \times 10^6$ operations. While $2^{20}$ passes on LeetCode due to small $N$, if $N = 100$, $2^{100} \approx 1.26 \times 10^{30}$ crashes immediately. The knapsack reduction executes in $O(N \times P) \le 20 \times 1000 = 2 \times 10^4$ operations.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**Parity and Feasibility Invariants:**
1. If $|target| > totalSum$, target is outside the achievable range $\implies$ return 0.
2. If $(totalSum + target)$ is odd, $P = (totalSum + target) / 2$ is not an integer $\implies$ return 0.
3. If $totalSum + target < 0$, target is negative beyond total sum $\implies$ return 0.

**0-1 Knapsack Backward Sweep Invariant:**
To count subsets that sum to $P$ using each element at most once:
$$dp[w] = dp[w] + dp[w - num]$$
Iterating capacity $w$ **backwards** from $P$ down to $num$ guarantees that $dp[w - num]$ reflects the count from the prefix $nums[0 \dots i - 1]$, preventing the same element from being accumulated repeatedly.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
0-1 KNAPSACK SUBSET SUM COUNT:
Target capacity P = (totalSum + target) / 2
dp array: [ dp[0] ... dp[w - num] ... dp[w] ... dp[P] ]
Sweep direction: <=========================

Transition:
dp[w] += dp[w - num]
```

- `num`: The active number from `nums`.
- `w`: Capacity cursor scanning backwards from $P$ down to $num$.
- `dp[w]`: Stores the total number of subsets from processed items that sum to exact weight $w$.
- **Base Case Invariant:** $dp[0] = 1$ (exactly 1 subset sums to 0: the empty set $\emptyset$).

#### 3.5 State Transition Triggers & Decision Gates
1. **Mathematical Feasibility Gate:**
   - If $|target| > totalSum$ or $(totalSum + target) \% 2 \neq 0$, return 0.
2. **Setup:** Set $P = (totalSum + target) / 2$. Allocate `dp = new int[P + 1]`. Set `dp[0] = 1`.
3. **Capacity Accumulation:** For each $num \in nums$, for $w = P$ down to $num$:
   $$dp[w] += dp[w - num]$$
4. **Resolution:** Return `dp[P]`.

#### 3.6 Concrete Step-by-Step State Trace
Trace input: `nums = [1, 1, 1, 1, 1], target = 3`.
$totalSum = 5$. $P = (5 + 3) / 2 = 4$.
Initial `dp` array of size 5: `[1, 0, 0, 0, 0]`.

| Iteration ($num$) | Backward Sweep $w$ | Updated `dp` Array `[0, 1, 2, 3, 4]` | Explanation |
| :---: | :---: | :---: | :--- |
| **Init** | — | `[1, 0, 0, 0, 0]` | Empty subset sums to 0 |
| **$num = 1$ (1st)** | $4 \to 1$ | `[1, 1, 0, 0, 0]` | Subsets: `{0: 1, 1: 1}` |
| **$num = 1$ (2nd)** | $4 \to 1$ | `[1, 2, 1, 0, 0]` | Subsets: `{0: 1, 1: 2, 2: 1}` |
| **$num = 1$ (3rd)** | $4 \to 1$ | `[1, 3, 3, 1, 0]` | Subsets: `{0: 1, 1: 3, 2: 3, 3: 1}` |
| **$num = 1$ (4th)** | $4 \to 1$ | `[1, 4, 6, 4, 1]` | Subsets: Pascal's row 4 |
| **$num = 1$ (5th)** | $4 \to 1$ | `[1, 5, 10, 10, 5]` | $dp[4] = 5$ |

Final result: $dp[4] = 5$.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (0-1 Knapsack 1D Reduction):** Strictly superior. Converts an exponential sign-assignment search into a fast pseudo-polynomial DP with $O(N \times P)$ time and $O(P)$ space.
- **Approach 2 (Memoized Top-Down DFS):** Direct formulation caching `(index, currentSum)`. Incurs offset indexing to handle negative running sums, resulting in $O(N \times \text{totalSum})$ space and recursion overhead.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Summation & Parity Check:** Calculate `totalSum`. Verify $|target| \le totalSum$ and $(totalSum + target) \% 2 == 0$.
- **Step 2: Capacity Allocation:** Let $P = (totalSum + target) / 2$. Allocate `dp = new int[P + 1]`. Set `dp[0] = 1`.
- **Step 3: Knapsack Backward Sweep:** For each $num \in nums$, sweep $w$ from $P$ down to $num$, adding $dp[w - num]$ into $dp[w]$.
- **Step 4: Resolution & Return:** Return `dp[P]`.

#### 4.3 Alternative Approaches Analysis
- **Approach 2: Top-Down DFS with Memoization Table:**
  - State space: `(index, sum)`.
  - Because `sum` can range from $-totalSum$ to $+totalSum$, use an offset of $totalSum$ for array indexing: `memo[index, sum + totalSum]`.
  - Recursively sum choices for $+nums[i]$ and $-nums[i]$.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: 0-1 Knapsack 1D Reduction | Approach 2: Memoized Top-Down DFS |
| :--- | :--- | :--- |
| **Time Complexity (Best / Avg / Worst)** | $O(N \times P)$ | $O(N \times \text{totalSum})$ |
| **Auxiliary Space** | $O(P)$ array storage ($P \le 1000$) | $O(N \times \text{totalSum})$ 2D memo table |
| **Output Space** | $O(1)$ scalar integer | $O(1)$ scalar integer |
| **Cache Locality** | High (linear array backward scan) | Moderate (2D table jumps) |
| **In-Place Mutability** | Non-destructive | Non-destructive |
| **Streaming Suitability** | High | Low |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Problem #91 - Target Sum
// Core Pattern: Algebraic Reduction to 0-1 Knapsack / Subset Sum Count
// Primary Invariant: P = (totalSum + target) / 2; dp[w] += dp[w - num] backward.
// Parity Defense: If (totalSum + target) is odd or |target| > totalSum, return 0.
// Space Defense: 1D array of size P + 1 achieves O(P) auxiliary memory.
// ============================================================================
```

#### Implementation 1: 0-1 Knapsack 1D Reduction (Production Standard)
```csharp
public class Solution
{
    public int FindTargetSumWays(int[] nums, int target)
    {
        // Guard Clause
        if (nums == null || nums.Length == 0)
        {
            return 0;
        }

        int totalSum = 0;
        foreach (int num in nums)
        {
            totalSum += num;
        }

        // Parity & Boundary Defenses:
        // 1. If target is beyond the absolute reachable sum, 0 ways exist
        // 2. (totalSum + target) must be even to divide into an integer subset target P
        // 3. (totalSum + target) must be >= 0
        if (Math.Abs(target) > totalSum || ((totalSum + target) & 1) == 1)
        {
            return 0;
        }

        // Reduced positive subset target: P = (totalSum + target) / 2
        int subsetTarget = (totalSum + target) / 2;

        // dp[w] stores the number of subsets summing to weight w
        int[] dp = new int[subsetTarget + 1];

        // Base case: There is exactly 1 way to form sum 0 (the empty subset)
        dp[0] = 1;

        foreach (int num in nums)
        {
            // CRITICAL INVARIANT: Iterate capacity BACKWARD from subsetTarget down to num
            // Backward traversal ensures each number is counted at most once per subset (0-1 knapsack)
            // Note: nums[i] can be 0. When num == 0, dp[w] += dp[w], doubling the count as expected!
            for (int w = subsetTarget; w >= num; w--)
            {
                dp[w] += dp[w - num];
            }
        }

        return dp[subsetTarget];
    }
}
```

#### Implementation 2: Top-Down Memoized DFS (Explicit State Graph)
```csharp
public class SolutionDfs
{
    public int FindTargetSumWays(int[] nums, int target)
    {
        if (nums == null || nums.Length == 0) return 0;

        int totalSum = 0;
        foreach (int num in nums) totalSum += num;

        if (Math.Abs(target) > totalSum) return 0;

        // memo[index, sum + totalSum] caches subproblem results
        int[,] memo = new int[nums.Length, 2 * totalSum + 1];
        for (int i = 0; i < nums.Length; i++)
        {
            for (int j = 0; j < 2 * totalSum + 1; j++)
            {
                memo[i, j] = int.MinValue;
            }
        }

        return Dfs(nums, 0, 0, target, totalSum, memo);
    }

    private static int Dfs(int[] nums, int index, int currentSum, int target, int totalSum, int[,] memo)
    {
        // Base case: All numbers have been assigned a sign
        if (index == nums.Length)
        {
            return currentSum == target ? 1 : 0;
        }

        int memoCol = currentSum + totalSum;
        if (memo[index, memoCol] != int.MinValue)
        {
            return memo[index, memoCol];
        }

        // Branch 1: Add current number
        int add = Dfs(nums, index + 1, currentSum + nums[index], target, totalSum, memo);
        // Branch 2: Subtract current number
        int subtract = Dfs(nums, index + 1, currentSum - nums[index], target, totalSum, memo);

        memo[index, memoCol] = add + subtract;
        return memo[index, memoCol];
    }
}
```

---

## 92. Best Time to Buy and Sell Stock with Cooldown (LeetCode #309)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | 💡 Advanced |
| **Pattern Tags** | `#state-machine-dp` `#stock-trading` `#fsm` |
| **LeetCode Link** | [Stock with Cooldown](https://leetcode.com/problems/best-time-to-buy-and-sell-stock-with-cooldown/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given an array `prices` where `prices[i]` is the stock price on day $i$, find the maximum profit possible. You may complete as many transactions as you like with the condition: after you sell your stock, you cannot buy stock on the next day (1 day mandatory cooldown).
- **Key Constraints:**
  - $1 \le prices.Length \le 5000$.
  - $0 \le prices[i] \le 1000$.
- **Senior Edge Cases to Defend:**
  - $prices.Length \le 1 \implies 0$ profit (cannot execute a buy-sell cycle).
  - Strictly decreasing prices (e.g. `[5, 4, 3, 2, 1]` $\implies 0$).
  - Two-day prices: profit is $\max(0, prices[1] - prices[0])$.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** 3-State Finite State Machine (FSM): At the end of each day, the trader is in one of three states: `Held` (holding stock), `Sold` (just sold stock today), `Reset` (uninvested and not in cooldown).
- **Sample 1:**
  - **Input:** `prices = [1, 2, 3, 0, 2]`
  - **Output:** `3`
  - **Explanation:** Transactions = `[buy, sell, cooldown, buy, sell]`. Profit = $(2 - 1) + (2 - 0) = 3$.
- **Sample 2:**
  - **Input:** `prices = [1]`
  - **Output:** `0`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine a stock trading algorithm modeled as a 3-gear mechanical transmission:
1. **Gear 1 (`Held`):** You currently own a share. You can either stay in Gear 1 (hold through the day) or shift into Gear 2 (`Sold`) by selling at today's market price.
2. **Gear 2 (`Sold`):** You just executed a sale today. The transaction mechanism locks up for a mandatory 24-hour cooldown period. You have no choice: at the end of the day, the transmission automatically drops into Gear 3 (`Reset`).
3. **Gear 3 (`Reset`):** You are cash-rich, holding no stock, and past cooldown. You can either stay in Gear 3 (do nothing) or shift into Gear 1 (`Held`) by buying a share at today's price.
This 3-state cycle models the cooldown constraint without having to track transaction histories.

#### 3.2 The Naive Bottleneck & Redundant Computation
A brute-force recursive search branches at every day into `{Buy, Sell, Rest}` while managing a boolean `cooldown` flag:
$$T(N) = O(2^N)$$
For $N = 5000$, $2^{5000} \approx 10^{1505}$, causing immediate stack overflow. However, notice that regardless of what happened in the previous 100 days, the optimal profit on day $i$ depends **only on which of the 3 discrete states you occupy** at the end of day $i - 1$. FSM dynamic programming reduces this to $O(N)$ time and $O(1)$ space.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**FSM State Transition Equations:**
At day $i$:
$$\begin{aligned} \text{Held}[i] &= \max(\text{Held}[i - 1], \; \text{Reset}[i - 1] - prices[i]) \\ \text{Sold}[i] &= \text{Held}[i - 1] + prices[i] \\ \text{Reset}[i] &= \max(\text{Reset}[i - 1], \; \text{Sold}[i - 1]) \end{aligned}$$
- **Why `Reset[i - 1] - prices[i]` for Buy?** Because to buy on day $i$, you must NOT have sold on day $i - 1$ (cooldown rule). You can only buy if you were in the `Reset` state yesterday!
- **Optimal Liquidation:** At the end of trading, you should never be holding a stock (`Held` involves unrecovered purchase costs). The global answer is strictly $\max(\text{Sold}[N - 1], \text{Reset}[N - 1])$.
- **Register Compaction:** Because state $i$ references only state $i - 1$, three scalar variables (`held`, `sold`, `reset`) completely eliminate array allocations ($O(1)$ space).

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
FINITE STATE MACHINE TRANSITION TOPOLOGY:

      ┌────── (Rest: held) ──────┐
      │                          │
      ▼                          │
   [ HELD ] ──── (Sell: +price) ─┴───► [ SOLD ]
      ▲                                   │
      │                                   │ (Cooldown: automatic)
 (Buy: -price)                            │
      │                                   ▼
   [ RESET ] ◄──── (Rest: reset) ──── [ RESET ]
```

- `held`: Maximum profit achievable where a share of stock is currently owned. Initialized to $-prices[0]$ on Day 0.
- `sold`: Maximum profit achievable where a share of stock was liquidated on the current day. Initialized to $0$ (or $-\infty$).
- `reset`: Maximum profit achievable where no stock is owned and the trader is free to purchase. Initialized to $0$.
- **Invariant:** After processing day $i$, the variables hold the exact supremum profit for each respective portfolio status.

#### 3.5 State Transition Triggers & Decision Gates
For each day $i$ from $1$ to $N - 1$:
1. **Cache Previous Horizon:** `prevHeld = held, prevSold = sold, prevReset = reset`.
2. **Held Gate:** `held = Math.Max(prevHeld, prevReset - prices[i])`.
3. **Sold Gate:** `sold = prevHeld + prices[i]`.
4. **Reset Gate:** `reset = Math.Max(prevReset, prevSold)`.
5. **Terminal Gate:** Return `Math.Max(sold, reset)`.

#### 3.6 Concrete Step-by-Step State Trace
Trace input: `prices = [1, 2, 3, 0, 2]`.
Day 0 base values: `held = -1, sold = 0, reset = 0`.

| Day $i$ | Price | `held` ($\max(held, reset - price)$) | `sold` ($prevHeld + price$) | `reset` ($\max(reset, prevSold)$) | Portfolio Interpretation |
| :---: | :---: | :---: | :---: | :---: | :--- |
| **0** | 1 | $-1$ (Buy) | 0 | 0 | Buy at 1 |
| **1** | 2 | $\max(-1, 0 - 2) = -1$ | $-1 + 2 = 1$ | $\max(0, 0) = 0$ | Sold at 2 (Profit = 1) |
| **2** | 3 | $\max(-1, 0 - 3) = -1$ | $-1 + 3 = 2$ | $\max(0, 1) = 1$ | Cooldown absorbs profit 1 |
| **3** | 0 | $\max(-1, 1 - 0) = 1$ | $-1 + 0 = -1$| $\max(1, 2) = 2$ | Buy at 0 with cash reserve 1! |
| **4** | 2 | $\max(1, 2 - 2) = 1$ | $1 + 2 = 3$ | $\max(2, -1) = 2$ | Sold at 2 (Total profit = 3) |

Final answer: $\max(sold, reset) = \max(3, 2) = 3$.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (3-Variable FSM):** Gold standard for production trading systems and interviews. $O(N)$ time, $O(1)$ space, zero heap allocation, clear mathematical modeling.
- **Approach 2 (Top-Down Memoized State Search):** Use if the state transition rules are highly dynamic (e.g. transaction fees, variable multi-day cooldowns $K > 1$). Models states recursively with a memoization table.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Setup & Boundaries:** If array has $\le 1$ element, return 0.
- **Step 2: Seed State Registers:** `held = -prices[0], sold = 0, reset = 0`.
- **Step 3: Sequential Day Progression:** For each day from $1$ to $N - 1$, snapshot prior states, apply FSM transitions.
- **Step 4: Resolution & Return:** Return $\max(sold, reset)$.

#### 4.3 Alternative Approaches Analysis
- **Approach 2: Top-Down DFS with Memoization Table:**
  - Recursive signature: `Dfs(day, holdingState)`.
  - `holdingState` $\in \{0: 	ext{Uninvested}, 1: 	ext{Holding}, 2: 	ext{Cooldown}\}$.
  - Memo table of size $N \times 3$. Transitions branch into Hold/Sell/Buy/Rest.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: 3-Variable FSM (Optimal) | Approach 2: Top-Down Memoized DFS |
| :--- | :--- | :--- |
| **Time Complexity (Best / Avg / Worst)** | $O(N)$ / $O(N)$ / $O(N)$ | $O(N)$ / $O(N)$ / $O(N)$ |
| **Auxiliary Space** | $O(1)$ scalar variables | $O(N)$ memo table + $O(N)$ call stack |
| **Output Space** | $O(1)$ scalar integer | $O(1)$ scalar integer |
| **Cache Locality** | Pure CPU register execution | Moderate |
| **In-Place Mutability** | Non-destructive | Non-destructive |
| **Streaming Suitability** | High (processes ticker prices online) | Low |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Problem #92 - Stock with Cooldown
// Core Pattern: Finite State Machine Dynamic Programming (3-State FSM)
// Primary Invariant: held = max(held, reset - price); sold = held + price; reset = max(reset, sold)
// Cooldown Defense: Buying requires transition strictly from 'reset', enforcing 1-day lock.
// Space Defense: 3 scalar registers achieve O(1) auxiliary memory.
// ============================================================================
```

#### Implementation 1: 3-Variable FSM (Production Standard)
```csharp
public class Solution
{
    public int MaxProfit(int[] prices)
    {
        // Guard Clause: Minimum 2 days required to complete any profitable transaction
        if (prices == null || prices.Length <= 1)
        {
            return 0;
        }

        // State 1: Currently holding a share of stock (cash reduced by purchase price)
        int held = -prices[0];
        // State 2: Just sold a share today (mandatory cooldown tomorrow)
        int sold = 0;
        // State 3: Ready to purchase stock (not holding, not in cooldown)
        int reset = 0;

        for (int i = 1; i < prices.Length; i++)
        {
            int currentPrice = prices[i];

            // Snapshot previous day's states before updating
            int prevHeld = held;
            int prevSold = sold;
            int prevReset = reset;

            // Invariant Transitions:
            // 1. Held: Continue holding previous stock OR buy new stock today from Reset state
            held = Math.Max(prevHeld, prevReset - currentPrice);

            // 2. Sold: Liquidate previously held stock at today's market price
            sold = prevHeld + currentPrice;

            // 3. Reset: Remain uninvested OR exit yesterday's cooldown period
            reset = Math.Max(prevReset, prevSold);
        }

        // At end of trading, optimum portfolio must not hold unliquidated stock
        return Math.Max(sold, reset);
    }
}
```

#### Implementation 2: Top-Down Memoized State Search ($O(N)$ Space)
```csharp
public class SolutionDfs
{
    private const int UNINVESTED = 0;
    private const int HOLDING = 1;
    private const int COOLDOWN = 2;

    public int MaxProfit(int[] prices)
    {
        if (prices == null || prices.Length <= 1) return 0;

        // memo[day, state] initialized to -1
        int[,] memo = new int[prices.Length, 3];
        for (int i = 0; i < prices.Length; i++)
        {
            memo[i, 0] = -1;
            memo[i, 1] = -1;
            memo[i, 2] = -1;
        }

        return Dfs(prices, 0, UNINVESTED, memo);
    }

    private static int Dfs(int[] prices, int day, int state, int[,] memo)
    {
        if (day >= prices.Length)
        {
            return 0;
        }

        if (memo[day, state] != -1)
        {
            return memo[day, state];
        }

        int maxProfit;

        switch (state)
        {
            case UNINVESTED:
                // Choice 1: Buy today -> enter HOLDING
                int buy = -prices[day] + Dfs(prices, day + 1, HOLDING, memo);
                // Choice 2: Rest today -> remain UNINVESTED
                int restUninvested = Dfs(prices, day + 1, UNINVESTED, memo);
                maxProfit = Math.Max(buy, restUninvested);
                break;

            case HOLDING:
                // Choice 1: Sell today -> enter COOLDOWN
                int sell = prices[day] + Dfs(prices, day + 1, COOLDOWN, memo);
                // Choice 2: Hold today -> remain HOLDING
                int hold = Dfs(prices, day + 1, HOLDING, memo);
                maxProfit = Math.Max(sell, hold);
                break;

            case COOLDOWN:
                // Forced Rest: Exit cooldown -> enter UNINVESTED
                maxProfit = Dfs(prices, day + 1, UNINVESTED, memo);
                break;

            default:
                maxProfit = 0;
                break;
        }

        memo[day, state] = maxProfit;
        return maxProfit;
    }
}
```
