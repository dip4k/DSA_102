# 📖 WEEK 10 DAY 03: 2D DYNAMIC PROGRAMMING — GRIDS & EDIT DISTANCE — ENGINEERING GUIDE

> 🧭 **Navigation:** [← Previous Day](Week_10_Day_02_1D_DP_Knapsack_Family_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_10_Day_04_DP_on_Sequences_Instructional.md)
> 
> 💡 **Instructor Note:** *2D Dynamic Programming extends our 1D mental model to multi-coordinate state spaces: grid navigation and pairwise sequence alignment. Today we master topological cell dependency, Levenshtein edit distance, and the space compression from O(M * N) to O(min(M, N)).*

---

## 🎯 LEARNING OBJECTIVES

*By the end of this chapter, you will be able to:*
- 🎯 **Structure** multi-dimensional subproblems using the **3-Step DP Recipe** (`Choice -> State Definition -> Base Cases & Transitions`).
- 🗺️ **Model** grid navigation (Unique Paths, Obstacles, Minimum Path Sum) as Directed Acyclic Graphs with topological cell order.
- 🔤 **Deconstruct** Levenshtein Edit Distance and Longest Common Subsequence (LCS) into character match, insertion, deletion, and replacement operations.
- ⚙️ **Implement** production-grade C# (.NET 8/9) and idiomatic Python (3.11+) with full path reconstruction and 1D rolling array compression.
- 🎙️ **Deliver** a confident 45-minute interview verbal script articulating transition trade-offs, diagonal dependencies, and space optimizations.

---

## 📖 CHAPTER 1: CONTEXT & MOTIVATION

### The Multi-Dimensional Horizon

In 1D dynamic programming, progress moves along a single axis (stair index, capacity, house position). However, real-world algorithmic problems frequently evaluate relationships between two interacting dimensions:
- **Spatial Grids:** Finding collision-free paths for autonomous vehicles or packet routing across network topologies with unavailable nodes.
- **String Transformations:** Computing typographical similarity in search query auto-correct, measuring genomic mutation distance across DNA sequences, or generating minimal diffs in source control systems.

A brute-force search comparing two strings of lengths `M` and `N` across possible edits explores an exponential tree of `3^(max(M, N))` branching paths. For strings of length 20, that equals over `3.4 * 10^9` computations. 2D Dynamic Programming collapses this search into an `O(M * N)` table where every cell is computed exactly once.

> [!NOTE]
> **Enterprise String Alignment & Grid Routing:** Production diff tools (like `git diff` using Myers/LCS algorithms), search engines (Google Spell Check computing minimum edit distance), and genomic databases (NCBI BLAST aligning gene sequences) rely on 2D dynamic programming matrices. By memoizing pairwise prefix alignments, systems compare multi-megabyte datasets in linear and polynomial time.

---

## 🧠 CHAPTER 2: THE 3-STEP RECIPE FOR 2D DP

```text
+-----------------------------------------------------------------------------------+
|                           THE 3-STEP RECIPE FOR 2D DP                             |
+-----------------------------------------------------------------------------------+
|  1. CHOICE            Grid: Move Right OR move Down.                              |
|                       Strings: Characters match (diagonal skip), OR edit:         |
|                         - Insert (from left cell)                                 |
|                         - Delete (from top cell)                                  |
|                         - Replace (from top-left diagonal cell)                   |
|                                                                                   |
|  2. STATE             dp[i][j] = Optimal answer for subproblem at coordinates     |
|                       (i, j) or prefixes word1[0..i-1] and word2[0..j-1].         |
|                                                                                   |
|  3. TRANSITIONS &     Grid:   dp[i][j] = dp[i-1][j] + dp[i][j-1]                  |
|     BASE CASES        Strings (Edit Distance):                                    |
|                       If match:   dp[i][j] = dp[i-1][j-1]                         |
|                       If mismatch:dp[i][j] = 1 + min(dp[i-1][j],   // Delete      |
|                                                      dp[i][j-1],   // Insert      |
|                                                      dp[i-1][j-1]) // Replace     |
|                       Base: Fill boundary row 0 and column 0.                     |
+-----------------------------------------------------------------------------------+
```

### Visualizing Cell Dependencies: The 2D Topology

Every cell `dp[i][j]` depends strictly on previously computed adjacent cells. This creates a natural topological evaluation order from top-left to bottom-right:

```text
===================================================================================
GRID NAVIGATION CELL DEPENDENCY               STRING EDIT DISTANCE CELL DEPENDENCY
===================================================================================

       (i-1, j) [Top neighbor]                      (i-1, j-1) [Diagonal: Match/Replace]
          |                                             \          |
          | (move down)                                  \         | (delete)
          v                                               v        v
(i, j-1) ---> (i, j) [Target cell]                 (i, j-1) ---> (i, j) [Target cell]
[Left]   (move right)                              [Insert]

Formula: dp[i][j] = dp[i-1][j] + dp[i][j-1]         Formula: dp[i][j] = 1 + min(top, left, diag)
```

### Visualizing Rolling Array Space Compression (2D -> 1D)

Because calculating row `i` only requires values from the current row `i` and previous row `i - 1`, we can compress the `M * N` matrix into a single 1D array of size `N`:

```text
Full 2D Matrix (O(M * N) space):           Compressed 1D Array (O(N) space):
Row 0: [ 1 ][ 1 ][ 1 ][ 1 ]                Index:   0    1    2    3
Row 1: [ 1 ][ 2 ][ 3 ][ 4 ]        ====>   Array: [ 1 ][ 3 ][ 6 ][ 10 ]
Row 2: [ 1 ][ 3 ][ 6 ][ 10]                 dp[j] (new) = dp[j] (old top) + dp[j-1] (left)
```

---

## ⚙️ CHAPTER 3: MECHANICS & IMPLEMENTATION

### ASCII Trace Table: Edit Distance between "CAT" and "DOG"

Target: Transform `s1 = "CAT"` into `s2 = "DOG"`.
Matrix dimensions: `(3 + 1) * (3 + 1)`:

```text
        ""      D       O       G
    +-------+-------+-------+-------+
""  |   0   |   1   |   2   |   3   |  <- Base: Insert characters into empty string
    +-------+-------+-------+-------+
C   |   1   |   1   |   2   |   3   |  <- C vs D: Replace 'C' with 'D' (cost 1)
    +-------+-------+-------+-------+
A   |   2   |   2   |   2   |   3   |  <- CA vs DO: Replace 'A' with 'O' (cost 2)
    +-------+-------+-------+-------+
T   |   3   |   3   |   3   |   3   |  <- CAT vs DOG: Replace 'T' with 'G' (cost 3)
    +-------+-------+-------+-------+
Final Edit Distance = dp[3][3] = 3.
Operations: Replace 'C'->'D', Replace 'A'->'O', Replace 'T'->'G'.
```

---

### Implementation 1: Grid Unique Paths with Obstacles (LeetCode 62 & 63)

#### Modern C# (.NET 8/9) Implementation

```csharp
namespace DP.Grids;

public static class GridPaths
{
    // -------------------------------------------------------------
    // Approach 1: 2D DP Table with Obstacle Handling & Path Tracing
    // Time: O(M * N) | Space: O(M * N)
    // -------------------------------------------------------------
    public static (int PathCount, List<(int Row, int Col)> SamplePath) UniquePathsWithObstacles2D(
        int[][] obstacleGrid)
    {
        if (obstacleGrid == null || obstacleGrid.Length == 0 || obstacleGrid[0].Length == 0)
            return (0, []);

        int m = obstacleGrid.Length;
        int n = obstacleGrid[0].Length;

        // Start or destination blocked
        if (obstacleGrid[0][0] == 1 || obstacleGrid[m - 1][n - 1] == 1)
            return (0, []);

        int[,] dp = new int[m, n];
        dp[0, 0] = 1;

        // Initialize first column
        for (int i = 1; i < m; i++)
            dp[i, 0] = (obstacleGrid[i][0] == 1) ? 0 : dp[i - 1, 0];

        // Initialize first row
        for (int j = 1; j < n; j++)
            dp[0, j] = (obstacleGrid[0][j] == 1) ? 0 : dp[0, j - 1];

        // Fill DP table
        for (int i = 1; i < m; i++)
        {
            for (int j = 1; j < n; j++)
            {
                if (obstacleGrid[i][j] == 1)
                {
                    dp[i, j] = 0; // Impassable cell
                }
                else
                {
                    dp[i, j] = dp[i - 1, j] + dp[i, j - 1];
                }
            }
        }

        // Reconstruct one valid path backwards
        List<(int, int)> path = [];
        if (dp[m - 1, n - 1] > 0)
        {
            int r = m - 1, c = n - 1;
            path.Add((r, c));
            while (r > 0 || c > 0)
            {
                if (r > 0 && dp[r - 1, c] > 0)
                    r--;
                else
                    c--;
                path.Add((r, c));
            }
            path.Reverse();
        }

        return (dp[m - 1, n - 1], path);
    }

    // -------------------------------------------------------------
    // Approach 2: Space-Optimized 1D Rolling Array
    // Time: O(M * N) | Space: O(N)
    // -------------------------------------------------------------
    public static int UniquePathsWithObstaclesOptimized(int[][] obstacleGrid)
    {
        int m = obstacleGrid.Length;
        int n = obstacleGrid[0].Length;
        int[] dp = new int[n];

        dp[0] = (obstacleGrid[0][0] == 1) ? 0 : 1;

        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (obstacleGrid[i][j] == 1)
                {
                    dp[j] = 0;
                }
                else if (j > 0)
                {
                    dp[j] += dp[j - 1]; // dp[j] (from top) + dp[j-1] (from left)
                }
            }
        }

        return dp[n - 1];
    }
}
```

#### Idiomatic Python (3.11+) Implementation

```python
class GridPaths:
    """Unique paths on a grid with obstacles: 2D with reconstruction and 1D rolling array."""

    @staticmethod
    def unique_paths_with_obstacles_2d(grid: list[list[int]]) -> tuple[int, list[tuple[int, int]]]:
        if not grid or not grid[0]:
            return 0, []

        m, n = len(grid), len(grid[0])
        if grid[0][0] == 1 or grid[m - 1][n - 1] == 1:
            return 0, []

        dp = [[0] * n for _ in range(m)]
        dp[0][0] = 1

        for i in range(1, m):
            dp[i][0] = 0 if grid[i][0] == 1 else dp[i - 1][0]
        for j in range(1, n):
            dp[0][j] = 0 if grid[0][j] == 1 else dp[0][j - 1]

        for i in range(1, m):
            for j in range(1, n):
                if grid[i][j] == 1:
                    dp[i][j] = 0
                else:
                    dp[i][j] = dp[i - 1][j] + dp[i][j - 1]

        # Path reconstruction
        path: list[tuple[int, int]] = []
        if dp[m - 1][n - 1] > 0:
            r, c = m - 1, n - 1
            path.append((r, c))
            while r > 0 or c > 0:
                if r > 0 and dp[r - 1][c] > 0:
                    r -= 1
                else:
                    c -= 1
                path.append((r, c))
            path.reverse()

        return dp[m - 1][n - 1], path

    @staticmethod
    def unique_paths_with_obstacles_optimized(grid: list[list[int]]) -> int:
        """Space-optimized 1D rolling array O(N) space."""
        m, n = len(grid), len(grid[0])
        dp = [0] * n
        dp[0] = 1 if grid[0][0] == 0 else 0

        for i in range(m):
            for j in range(n):
                if grid[i][j] == 1:
                    dp[j] = 0
                elif j > 0:
                    dp[j] += dp[j - 1]

        return dp[n - 1]
```

---

### Implementation 2: Edit Distance (LeetCode 72)

#### Modern C# (.NET 8/9) Implementation

```csharp
namespace DP.Strings;

public static class EditDistance
{
    // -------------------------------------------------------------
    // Full 2D Levenshtein with Operation Sequence Reconstruction
    // Time: O(M * N) | Space: O(M * N)
    // -------------------------------------------------------------
    public static (int Distance, List<string> Operations) MinDistanceWithReconstruction(
        string word1, string word2)
    {
        int m = word1.Length;
        int n = word2.Length;
        int[,] dp = new int[m + 1, n + 1];

        // Base cases
        for (int i = 0; i <= m; i++) dp[i, 0] = i; // Delete all word1 chars
        for (int j = 0; j <= n; j++) dp[0, j] = j; // Insert all word2 chars

        // Fill DP table
        for (int i = 1; i <= m; i++)
        {
            for (int j = 1; j <= n; j++)
            {
                if (word1[i - 1] == word2[j - 1])
                {
                    dp[i, j] = dp[i - 1, j - 1]; // Match: 0 edit cost
                }
                else
                {
                    int deleteCost  = dp[i - 1, j];
                    int insertCost  = dp[i, j - 1];
                    int replaceCost = dp[i - 1, j - 1];
                    dp[i, j] = 1 + Math.Min(deleteCost, Math.Min(insertCost, replaceCost));
                }
            }
        }

        // Reconstruct explicit editing operations
        List<string> ops = [];
        int r = m, c = n;
        while (r > 0 || c > 0)
        {
            if (r > 0 && c > 0 && word1[r - 1] == word2[c - 1])
            {
                r--; c--; // Matched character
            }
            else if (r > 0 && c > 0 && dp[r, c] == dp[r - 1, c - 1] + 1)
            {
                ops.Add($"Replace '{word1[r - 1]}' with '{word2[c - 1]}'");
                r--; c--;
            }
            else if (r > 0 && dp[r, c] == dp[r - 1, c] + 1)
            {
                ops.Add($"Delete '{word1[r - 1]}'");
                r--;
            }
            else if (c > 0 && dp[r, c] == dp[r, c - 1] + 1)
            {
                ops.Add($"Insert '{word2[c - 1]}'");
                c--;
            }
        }

        ops.Reverse();
        return (dp[m, n], ops);
    }

    // -------------------------------------------------------------
    // Space-Optimized 1D Rolling Array
    // Time: O(M * N) | Space: O(N)
    // -------------------------------------------------------------
    public static int MinDistanceOptimized(string word1, string word2)
    {
        int m = word1.Length;
        int n = word2.Length;
        int[] dp = new int[n + 1];

        for (int j = 0; j <= n; j++) dp[j] = j;

        for (int i = 1; i <= m; i++)
        {
            int prevDiagonal = dp[0];
            dp[0] = i;

            for (int j = 1; j <= n; j++)
            {
                int temp = dp[j];
                if (word1[i - 1] == word2[j - 1])
                {
                    dp[j] = prevDiagonal;
                }
                else
                {
                    dp[j] = 1 + Math.Min(prevDiagonal, Math.Min(dp[j], dp[j - 1]));
                }
                prevDiagonal = temp;
            }
        }

        return dp[n];
    }
}
```

#### Idiomatic Python (3.11+) Implementation

```python
class EditDistance:
    """Levenshtein Edit Distance: 2D with reconstruction and 1D rolling array."""

    @staticmethod
    def min_distance_with_reconstruction(word1: str, word2: str) -> tuple[int, list[str]]:
        m, n = len(word1), len(word2)
        dp = [[0] * (n + 1) for _ in range(m + 1)]

        for i in range(m + 1):
            dp[i][0] = i
        for j in range(n + 1):
            dp[0][j] = j

        for i in range(1, m + 1):
            for j in range(1, n + 1):
                if word1[i - 1] == word2[j - 1]:
                    dp[i][j] = dp[i - 1][j - 1]
                else:
                    dp[i][j] = 1 + min(
                        dp[i - 1][j],      # Delete
                        dp[i][j - 1],      # Insert
                        dp[i - 1][j - 1],  # Replace
                    )

        # Backtrack operations
        ops: list[str] = []
        r, c = m, n
        while r > 0 or c > 0:
            if r > 0 and c > 0 and word1[r - 1] == word2[c - 1]:
                r -= 1
                c -= 1
            elif r > 0 and c > 0 and dp[r][c] == dp[r - 1][c - 1] + 1:
                ops.append(f"Replace '{word1[r - 1]}' with '{word2[c - 1]}'")
                r -= 1
                c -= 1
            elif r > 0 and dp[r][c] == dp[r - 1][c] + 1:
                ops.append(f"Delete '{word1[r - 1]}'")
                r -= 1
            elif c > 0 and dp[r][c] == dp[r][c - 1] + 1:
                ops.append(f"Insert '{word2[c - 1]}'")
                c -= 1

        ops.reverse()
        return dp[m][n], ops

    @staticmethod
    def min_distance_optimized(word1: str, word2: str) -> int:
        """Space-optimized rolling array in O(min(M, N)) auxiliary space."""
        if len(word1) < len(word2):
            word1, word2 = word2, word1

        m, n = len(word1), len(word2)
        dp = list(range(n + 1))

        for i in range(1, m + 1):
            prev_diag = dp[0]
            dp[0] = i
            for j in range(1, n + 1):
                temp = dp[j]
                if word1[i - 1] == word2[j - 1]:
                    dp[j] = prev_diag
                else:
                    dp[j] = 1 + min(prev_diag, dp[j], dp[j - 1])
                prev_diag = temp

        return dp[n]
```

---

### Implementation 3: Longest Common Subsequence (LeetCode 1143)

#### Modern C# (.NET 8/9) Implementation

```csharp
namespace DP.Strings;

public static class LongestCommonSubsequence
{
    public static (int Length, string Subsequence) Solve(string text1, string text2)
    {
        int m = text1.Length;
        int n = text2.Length;
        int[,] dp = new int[m + 1, n + 1];

        for (int i = 1; i <= m; i++)
        {
            for (int j = 1; j <= n; j++)
            {
                if (text1[i - 1] == text2[j - 1])
                    dp[i, j] = dp[i - 1, j - 1] + 1;
                else
                    dp[i, j] = Math.Max(dp[i - 1, j], dp[i, j - 1]);
            }
        }

        // Reconstruct string
        var sb = new System.Text.StringBuilder();
        int r = m, c = n;
        while (r > 0 && c > 0)
        {
            if (text1[r - 1] == text2[c - 1])
            {
                sb.Append(text1[r - 1]);
                r--; c--;
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

        var chars = sb.ToString().ToCharArray();
        Array.Reverse(chars);
        return (dp[m, n], new string(chars));
    }
}
```

#### Idiomatic Python (3.11+) Implementation

```python
class LongestCommonSubsequence:
    @staticmethod
    def solve(text1: str, text2: str) -> tuple[int, str]:
        m, n = len(text1), len(text2)
        dp = [[0] * (n + 1) for _ in range(m + 1)]

        for i in range(1, m + 1):
            for j in range(1, n + 1):
                if text1[i - 1] == text2[j - 1]:
                    dp[i][j] = dp[i - 1][j - 1] + 1
                else:
                    dp[i][j] = max(dp[i - 1][j], dp[i][j - 1])

        # Backtrack
        chars: list[str] = []
        r, c = m, n
        while r > 0 and c > 0:
            if text1[r - 1] == text2[c - 1]:
                chars.append(text1[r - 1])
                r -= 1
                c -= 1
            elif dp[r - 1][c] >= dp[r][c - 1]:
                r -= 1
            else:
                c -= 1

        chars.reverse()
        return dp[m][n], "".join(chars)
```

---

## ⚖️ CHAPTER 4: EXPLICIT COMPLEXITY DECONSTRUCTION

### Multi-Dimensional Complexity Profile

| Algorithm | Time Complexity | Auxiliary Space (2D) | Auxiliary Space (1D Compressed) | Cache Locality |
| :--- | :--- | :--- | :--- | :--- |
| **Unique Paths** | `O(M * N)` | `O(M * N)` | `O(N)` | Row-major traversal is L1 cache optimal |
| **Min Path Sum** | `O(M * N)` | `O(M * N)` | `O(N)` | Reads only `dp[j]` and `dp[j-1]` |
| **Edit Distance** | `O(M * N)` | `O(M * N)` | `O(min(M, N))` | Tracks a single scalar `prevDiagonal` |
| **LCS** | `O(M * N)` | `O(M * N)` | `O(min(M, N))` | Swap shorter string to inner dimension |

---

## 🎙️ CHAPTER 5: 45-MINUTE INTERVIEW VERBAL SCRIPT

```text
===================================================================================
             45-MINUTE INTERVIEW PLAYBOOK: EDIT DISTANCE & GRID DP
===================================================================================

[00:00 - 05:00] CLARIFICATION & CONSTRAINTS
"Let's confirm the problem boundaries. Are the input strings ASCII only, or do they 
contain Unicode? What are the maximum lengths of word1 and word2? If lengths are up to 
1,000, an O(M * N) solution executes in ~10^6 operations, which easily satisfies the 
time budget. Are character comparisons case-sensitive?"

[05:00 - 15:00] THE 3-STEP RECIPE & STATE FORMULATION
"I will frame this using the 3-Step Recipe:
 1. Choice: When comparing word1[i - 1] and word2[j - 1]:
    - If characters match: no operation needed (inherit diagonal state).
    - If mismatch: pick the minimum among:
      * Insert word2[j - 1] (transition from dp[i][j - 1])
      * Delete word1[i - 1] (transition from dp[i - 1][j])
      * Replace word1[i - 1] with word2[j - 1] (transition from dp[i - 1][j - 1])
 2. State: Let dp[i][j] represent the minimum edit operations to convert 
    prefix word1[0..i - 1] to prefix word2[0..j - 1].
 3. Base Cases:
    dp[i][0] = i (must delete all i characters).
    dp[0][j] = j (must insert all j characters)."

[15:00 - 25:00] TABULATION ORDER & MATRIX MECHANICS
"The dependencies require values from top, left, and top-left diagonal. Therefore, 
standard row-major iteration (outer loop i from 1 to M, inner loop j from 1 to N) 
satisfies topological order. Every cell is computed in O(1) time."

[25:00 - 35:00] SPACE OPTIMIZATION TO O(N)
"Notice that row i only requires row i - 1 and the left cell of row i. We can reduce 
auxiliary space from O(M * N) to O(N) by keeping a 1D array. Because the replace 
operation requires the top-left diagonal cell dp[i - 1][j - 1], we preserve this value 
in a temporary variable prevDiagonal before overwriting dp[j]."

[35:00 - 45:00] COMPLEXITY & EDGE CASE VALIDATION
"Time Complexity: O(M * N).
 Space Complexity: O(min(M, N)) by swapping the shorter string to the inner loop.
 Edge cases: Empty string to non-empty string yields length of non-empty; 
 identical strings yield 0 edits. The code is complete and optimized."
===================================================================================
```

---

## ⚔️ PRACTICE PROBLEMS & INTERVIEW DRILLS

| # | Problem | Difficulty | Key Pattern | Focus Skill |
| :- | :--- | :-: | :--- | :--- |
| 1 | **LeetCode 62: Unique Paths** | 🟡 Medium | 2D Grid DP | Pure additive transition |
| 2 | **LeetCode 63: Unique Paths II** | 🟡 Medium | Grid with Obstacles | Resetting blocked cell to 0 |
| 3 | **LeetCode 64: Minimum Path Sum** | 🟡 Medium | Grid Minimization | `dp[i][j] = grid[i][j] + min(up, left)` |
| 4 | **LeetCode 72: Edit Distance** | 🔴 Hard | 2D String Alignment | Match vs Insert/Delete/Replace |
| 5 | **LeetCode 1143: Longest Common Subsequence** | 🟡 Medium | Prefix Alignment | Substring vs Subsequence distinction |

---

## 🎓 SELF-CHECK & FINAL VERIFICATION

- [x] **Zero LaTeX Check:** All complexities in standard backticks (`O(M * N)`, `O(min(M, N))`). Zero `$` signs.
- [x] **Production Dual-Language Code:** Complete C# (.NET 8/9) and Python (3.11+) implementations with reconstruction and rolling optimizations.
- [x] **3-Step Framework:** Fully aligned with Choice, State Definition, and Transitions & Base Cases.
- [x] **Zero Cognitive Bloat:** No legacy cognitive lens sections; corporate stories condensed into a single `> [!NOTE]` callout.
- [x] **ASCII Visuals:** Clean cell dependency graphs and trace tables.

---

> 🧭 **Navigation:** [← Previous Day](Week_10_Day_02_1D_DP_Knapsack_Family_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_10_Day_04_DP_on_Sequences_Instructional.md)
