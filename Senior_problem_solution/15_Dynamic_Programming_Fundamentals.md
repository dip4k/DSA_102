# Phase 15: Dynamic Programming Fundamentals

> **Focus:** State Space Definitions, Recurrence Topologies, 1D Tabulation, Space Invariants, Unbounded vs 0-1 Knapsack Sweeps, Patience Sorting ($O(N \log N)$ LIS), and Dual Min-Max Sign Flipping.  
> **Source Curriculum:** [`Senior_dsa_question_list.md`](../Senior_dsa_question_list.md) — Phase 15 (Problems #79–#85)

---
## 79. Climbing Stairs (LeetCode #70)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟢 Easy |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#1d-dp` `#fibonacci` `#space-optimization` `#matrix-exponentiation` |
| **LeetCode Link** | [Climbing Stairs](https://leetcode.com/problems/climbing-stairs/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** You are climbing a staircase. It takes $n$ steps to reach the top. Each time you can either climb 1 or 2 steps. In how many distinct ways can you climb to the top?
- **Key Constraints:**
  - $1 \le n \le 45$.
- **Senior Edge Cases to Defend:**
  - $n = 1 \implies 1$, $n = 2 \implies 2$.
  - 32-bit signed integer overflow defense: At $n = 45$, ways $= 1,836,311,903 < 2^{31} - 1$, fitting safely within signed 32-bit `int`. If $n \ge 46$, transition to `long` or modular arithmetic is mandatory.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Additive DAG decomposition. Reaching step $i$ requires taking a single leap from $i - 1$ or a double leap from $i - 2$. The total distinct configurations to reach step $i$ is strictly the sum of ways to reach its two immediate predecessors: $dp[i] = dp[i - 1] + dp[i - 2]$.
- **Sample 1:**
  - **Input:** `n = 3`
  - **Output:** `3`
  - **Explanation:** Three valid path permutations: `1 + 1 + 1`, `1 + 2`, `2 + 1`.
- **Sample 2:**
  - **Input:** `n = 4`
  - **Output:** `5`
  - **Explanation:** `1+1+1+1`, `1+1+2`, `1+2+1`, `2+1+1`, `2+2`.

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine standing on the ground floor looking up at a staircase. To land on step $i$, physics dictates that your last physical move must have originated from either step $i - 1$ (by jumping 1 step) or step $i - 2$ (by leaping 2 steps). Because a 1-step leap and a 2-step leap are mutually exclusive physical events, the Rule of Sum in combinatorics guarantees that the total distinct paths arriving at step $i$ equals the disjoint union of all valid paths reaching $i - 1$ plus all valid paths reaching $i - 2$. This establishes an exact isomorphism with the Fibonacci sequence shifted by one index ($ways(n) = F(n+1)$).

#### 3.2 The Naive Bottleneck & Redundant Computation
The naive top-down recursion expresses the solution as:
$$T(n) = T(n - 1) + T(n - 2) + O(1)$$
Visualized as a decision tree, this generates a full binary tree of depth $n$:
```text
                       Climb(5)
                     /          \
             Climb(4)            Climb(3)
             /      \           /      \
        Climb(3)   Climb(2)  Climb(2)   Climb(1)
        /     \
    Climb(2) Climb(1)
```
Notice that `Climb(3)` is evaluated independently multiple times, and smaller subproblems like `Climb(2)` and `Climb(1)` are re-evaluated exponentially:
$$T(n) = O(2^n) \text{ operations}$$
For $n = 45$, this executes $\approx 2^{45} \approx 3.51 \times 10^{13}$ operations, causing guaranteed interview timeout. Dynamic Programming eliminates this redundancy by computing each subproblem exactly once.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
The state space possesses **Optimal Substructure** and **Markovian Memory Independence**:
$$\text{Ways}(i) = \text{Ways}(i - 1) + \text{Ways}(i - 2)$$
Because the calculation of step $i$ depends strictly on steps $i - 1$ and $i - 2$, historical values prior to $i - 2$ are completely irrelevant to future transitions. We can permanently discard all steps $< i - 2$, collapsing an $O(N)$ linear memory allocation into two scalar registers ($O(1)$ auxiliary space).
Furthermore, this linear recurrence can be formalized as a matrix transformation:
$$\begin{pmatrix} dp[i] \\ dp[i - 1] \end{pmatrix} = \begin{pmatrix} 1 & 1 \\ 1 & 0 \end{pmatrix} \begin{pmatrix} dp[i - 1] \\ dp[i - 2] \end{pmatrix}$$
By repeatedly applying this matrix, we obtain:
$$\begin{pmatrix} dp[n] \\ dp[n - 1] \end{pmatrix} = \begin{pmatrix} 1 & 1 \\ 1 & 0 \end{pmatrix}^{n - 2} \begin{pmatrix} dp[2] \\ dp[1] \end{pmatrix}$$
Using binary exponentiation (repeated squaring), the $k$-th power of a $2 \times 2$ matrix can be computed in $O(\log n)$ operations.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
STATE HORIZON PARTITION:
[ 1 ............ i - 3 ]  |  [ prev2: (i - 2) ]  |  [ prev1: (i - 1) ]  |  [ current: (i) ]  |  [ i + 1 ........ n ]
└──────────┬───────────┘     └────────┬───────┘     └────────┬───────┘     └───────┬───────┘     └──────────┬────────┘
  Settled & Discarded         Historical Anchor 2    Historical Anchor 1       Active State          Unexplored Stream
```

- `prev2`: Stores $dp[i - 2]$, the total distinct paths to the second most recent step.
- `prev1`: Stores $dp[i - 1]$, the total distinct paths to the immediate predecessor step.
- `current`: Evaluates $dp[i] = prev1 + prev2$.
- **Invariant:** At the start of iteration $i$, `prev2` and `prev1` accurately hold the validated combination counts for steps $i - 2$ and $i - 1$ respectively.

#### 3.5 State Transition Triggers & Decision Gates
For each step index $i$ from $3$ up to $n$:
1. **Summation Gate:** Compute `current = prev1 + prev2`.
2. **Horizon Advance Gate (Left Shift):** Set `prev2 = prev1` (discard old $i - 2$; former $i - 1$ becomes new $i - 2$).
3. **Frontier Advance Gate:** Set `prev1 = current` (current step becomes new $i - 1$).
4. **Terminal Gate:** When $i$ reaches $n$, `prev1` holds the exact solution for step $n$.

#### 3.6 Concrete Step-by-Step State Trace
Trace input: $n = 5$, Base cases: `prev2 = 1` (step 1), `prev1 = 2` (step 2).

| Iteration ($i$) | `prev2` ($i - 2$) | `prev1` ($i - 1$) | Calculation (`prev1 + prev2`) | New `prev2` | New `prev1` | Invariant Verification |
| :---: | :---: | :---: | :---: | :---: | :---: | :--- |
| **Initial** | 1 | 2 | — | 1 | 2 | Base states 1 and 2 established |
| **$i = 3$** | 1 | 2 | $2 + 1 = 3$ | 2 | 3 | $dp[3] = 3$ (`111`, `12`, `21`) |
| **$i = 4$** | 2 | 3 | $3 + 2 = 5$ | 3 | 5 | $dp[4] = 5$ |
| **$i = 5$** | 3 | 5 | $5 + 3 = 8$ | 5 | 8 | $dp[5] = 8$ |

Final return value: `prev1 = 8`.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Space-Optimized Iterative 1D):** The definitive standard for interviews and production code. It executes in $O(N)$ time with $O(1)$ auxiliary space, eliminates GC heap pressure entirely, and has zero recursion overhead.
- **Approach 2 (Matrix Exponentiation):** Select when $n$ scales to astronomical limits ($n \ge 10^9$) under a modular arithmetic constraint (e.g., $\pmod{10^9 + 7}$), where linear iteration would timeout. It provides a formal demonstration of algebraic transition systems in $O(\log n)$ time.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Setup & Boundaries:** Handle trivial cases immediately: if $n \le 2$, return $n$ directly.
- **Step 2: Main Exploration Loop:** Initialize `prev2 = 1` and `prev1 = 2`. Loop index $i$ from $3$ to $n$.
- **Step 3: Invariant Maintenance & Condition Gates:** Add `prev1 + prev2`, roll state forward without allocating temporary objects.
- **Step 4: Resolution & Return:** Return `prev1`.

#### 4.3 Alternative Approaches Analysis
- **Approach 2: Matrix Exponentiation ($O(\log N)$):**
  - We define transition matrix $M = \begin{pmatrix} 1 & 1 \\ 1 & 0 \end{pmatrix}$.
  - Compute $M^{n - 1}$ using binary exponentiation: if exponent bit is 1, multiply accumulator by current base; square current base at each bit shift.
  - Final answer is $M[0, 0] \times dp[1] + M[0, 1] \times dp[0]$. Runs in $O(\log n)$ time with $O(1)$ auxiliary storage.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: Space-Optimized Iterative 1D | Approach 2: Matrix Exponentiation |
| :--- | :--- | :--- |
| **Time Complexity (Best / Avg / Worst)** | $O(N)$ / $O(N)$ / $O(N)$ | $O(\log N)$ / $O(\log N)$ / $O(\log N)$ |
| **Auxiliary Space** | $O(1)$ scalar variables | $O(1)$ fixed $2 \times 2$ matrix |
| **Output Space** | $O(1)$ scalar integer | $O(1)$ scalar integer |
| **Cache Locality** | Pure CPU register execution | Pure CPU register execution |
| **In-Place Mutability** | N/A (read-only calculation) | N/A (read-only calculation) |
| **Streaming Suitability** | High (can yield intermediate steps online) | Low (requires final $n$ upfront) |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Problem #79 - Climbing Stairs
// Core Pattern: Additive 1D Dynamic Programming / Fibonacci State Reduction
// Primary Invariant: At step i, total distinct paths = dp[i - 1] + dp[i - 2].
// Space Defense: Two rolling registers eliminate O(N) array allocation.
// Overflow Defense: Valid for n <= 45; beyond n=45, cast to long.
// ============================================================================
```

#### Implementation 1: Space-Optimized Iterative DP (Production Optimal)
```csharp
public class Solution
{
    public int ClimbStairs(int n)
    {
        // Guard Clause: Steps 1 and 2 map directly to 1 and 2 distinct ways
        if (n <= 2)
        {
            return n;
        }

        // Rolling registers representing dp[i - 2] and dp[i - 1]
        int prev2 = 1; // Base case: ways to reach step 1
        int prev1 = 2; // Base case: ways to reach step 2

        // Iterate sequentially from step 3 through step n
        for (int i = 3; i <= n; i++)
        {
            // Invariant: Ways to reach current step is sum of reaching prior two steps
            int current = prev1 + prev2;

            // Slide the historical window forward
            prev2 = prev1;
            prev1 = current;
        }

        return prev1;
    }
}
```

#### Implementation 2: Matrix Exponentiation ($O(\log N)$ Runtime)
```csharp
public class SolutionMatrix
{
    public int ClimbStairs(int n)
    {
        // Direct resolution for initial base cases
        if (n <= 2)
        {
            return n;
        }

        // Fibonacci transformation matrix: [[1, 1], [1, 0]]
        int[,] transition = { { 1, 1 }, { 1, 0 } };

        // Exponentiate transition matrix to power (n - 1)
        MatrixPower(transition, n - 1);

        // State vector [F(2), F(1)] = [2, 1]
        // Result is transition[0, 0] * 2 + transition[0, 1] * 1
        return transition[0, 0];
    }

    private static void MatrixPower(int[,] matrix, int power)
    {
        // Initialize result as identity matrix I
        int[,] result = { { 1, 0 }, { 0, 1 } };

        while (power > 0)
        {
            // If current bit is set, multiply result by current base matrix
            if ((power & 1) == 1)
            {
                Multiply(result, matrix);
            }

            // Square the base matrix for the next bit position
            Multiply(matrix, matrix);
            power >>= 1;
        }

        // Copy computed result back into matrix reference
        Array.Copy(result, matrix, 4);
    }

    private static void Multiply(int[,] a, int[,] b)
    {
        int r00 = a[0, 0] * b[0, 0] + a[0, 1] * b[1, 0];
        int r01 = a[0, 0] * b[0, 1] + a[0, 1] * b[1, 1];
        int r10 = a[1, 0] * b[0, 0] + a[1, 1] * b[1, 0];
        int r11 = a[1, 0] * b[0, 1] + a[1, 1] * b[1, 1];

        a[0, 0] = r00;
        a[0, 1] = r01;
        a[1, 0] = r10;
        a[1, 1] = r11;
    }
}
```

---

## 80. House Robber (LeetCode #198)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#1d-dp` `#include-exclude` `#state-compression` |
| **LeetCode Link** | [House Robber](https://leetcode.com/problems/house-robber/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** You are planning to rob houses along a street. Each house has a certain amount of money stashed. Adjacent houses have security systems that trigger the police if two adjacent houses were broken into on the same night. Return the maximum amount of money you can rob tonight without alerting the police.
- **Key Constraints:**
  - $1 \le nums.Length \le 100$.
  - $0 \le nums[i] \le 400$.
- **Senior Edge Cases to Defend:**
  - Single house ($nums.Length == 1 \implies nums[0]$).
  - Two houses ($nums.Length == 2 \implies \max(nums[0], nums[1])$).
  - All houses having 0 money ($nums = [0, 0, 0] \implies 0$).

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Binary Choice Invariant: At house $i$, choose either: (1) Skip house $i$ and retain profit from house $i-1$, or (2) Rob house $i$ and add profit from house $i-2$.
- **Sample 1:**
  - **Input:** `nums = [1, 2, 3, 1]`
  - **Output:** `4`
  - **Explanation:** Rob house 1 (money = 1) and house 3 (money = 3). Total stolen = $1 + 3 = 4$.
- **Sample 2:**
  - **Input:** `nums = [2, 7, 9, 3, 1]`
  - **Output:** `12`
  - **Explanation:** Rob house 1 (money = 2), house 3 (money = 9), and house 5 (money = 1). Total stolen = $2 + 9 + 1 = 12$.

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine an alarm tripwire strung between every adjacent pair of houses. You walk down the street sequentially. At each house $i$, you face a strict binary decision gate:
1. **Rob house $i$:** You cut the wire to house $i-1$ (meaning you cannot have robbed house $i-1$), but you collect $nums[i]$ plus the maximum loot accumulated up through house $i-2$.
2. **Skip house $i$:** You leave house $i$ untouched, preserving your ability to have taken whatever maximum loot was possible through house $i-1$.
This non-local constraint (no two consecutive selections) decomposes cleanly into localized prefix decisions because whether you rob house $i$ depends only on whether house $i-1$ was robbed.

#### 3.2 The Naive Bottleneck & Redundant Computation
A brute-force recursive tree branches at every house into two options:
$$\text{Rob}(i) = \max(\text{Rob}(i + 1), nums[i] + \text{Rob}(i + 2))$$
This creates a recursive call tree of depth $N$ with a branching factor of 2:
```text
                          Rob(0)
                        /        \
                 Rob(1)            Rob(2)
                /      \          /      \
            Rob(2)    Rob(3)   Rob(3)    Rob(4)
```
Notice that `Rob(2)` and `Rob(3)` are evaluated redundantly across multiple distinct decision branches. The naive complexity is $O(2^N)$, which for $N = 100$ results in $2^{100} \approx 1.26 \times 10^{30}$ calls.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
The problem satisfies Bellman's Principle of Optimality:
$$dp[i] = \max(dp[i - 1], dp[i - 2] + nums[i])$$
**Safety of Discarding:** Why can we safely discard sub-optimal prefix choices? Because future houses ($i + 1, i + 2, \dots$) only care about the **maximum profit** achieved up to house $i - 1$ and house $i - 2$. The specific sequence of robberies chosen in the past creates no lingering side effects other than the binary state of the immediate predecessor.
Furthermore, state $i$ references only $i - 1$ and $i - 2$. Thus, we need not maintain an entire array: two scalar registers (`robPrev1` and `robPrev2`) suffice, reducing space complexity from $O(N)$ to $O(1)$.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
STATE REGISTERS PARTITION:
[ Houses 0 ... i - 3 ]  |  [ robPrev2 (i - 2) ]  |  [ robPrev1 (i - 1) ]  |  [ Active: nums[i] ]  |  [ i + 1 ... N - 1 ]
└──────────┬───────────┘     └────────┬────────┘     └────────┬────────┘     └────────┬─────────┘     └──────────┬────────┘
     Settled & Inactive       Max loot up to i-2     Max loot up to i-1          Current candidate         Unexplored houses
```

- `robPrev2`: The maximum profit achievable from houses $0$ through $i - 2$.
- `robPrev1`: The maximum profit achievable from houses $0$ through $i - 1$.
- `current`: The optimal profit through house $i$: $\max(robPrev1, robPrev2 + nums[i])$.
- **Invariant:** After processing house $i$, `robPrev1` strictly equals the optimal loot obtainable from the prefix $nums[0 \dots i]$.

#### 3.5 State Transition Triggers & Decision Gates
At each house $i$:
1. **Decision Gate:**
   - Candidate A (Skip house $i$): Loot remains `robPrev1`.
   - Candidate B (Rob house $i$): Loot becomes `robPrev2 + nums[i]`.
2. **Commit Gate:** `current = Math.Max(robPrev1, robPrev2 + nums[i])`.
3. **Register Rotation Gate:** `robPrev2 = robPrev1; robPrev1 = current;`.

#### 3.6 Concrete Step-by-Step State Trace
Trace input: `nums = [2, 7, 9, 3, 1]`

| Step | House $i$ | `nums[i]` | `robPrev2` ($i - 2$) | `robPrev1` ($i - 1$) | Choice: Skip vs Rob (`robPrev1` vs `robPrev2 + num`) | New `current` | State Shift (`robPrev2`, `robPrev1`) |
| :---: | :---: | :---: | :---: | :---: | :---: | :---: | :---: |
| **Init**| — | — | 0 | 0 | Base empty prefix | 0 | `(0, 0)` |
| **1** | 0 | 2 | 0 | 0 | $\max(0, 0 + 2) = 2$ | 2 | `(0, 2)` |
| **2** | 1 | 7 | 0 | 2 | $\max(2, 0 + 7) = 7$ | 7 | `(2, 7)` |
| **3** | 2 | 9 | 2 | 7 | $\max(7, 2 + 9) = 11$ | 11 | `(7, 11)` |
| **4** | 3 | 3 | 7 | 11 | $\max(11, 7 + 3) = 11$ | 11 | `(11, 11)` |
| **5** | 4 | 1 | 11 | 11 | $\max(11, 11 + 1) = 12$ | 12 | `(11, 12)` |

Final return value: `12`.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Space-Optimized Rolling State):** Production optimal. $O(N)$ linear time, $O(1)$ auxiliary space. Use whenever you only need the maximum profit scalar.
- **Approach 2 (Tabulation Array $O(N)$ Space):** Use when the interviewer asks you to **reconstruct the exact houses robbed**. The $O(N)$ array allows backtracking from index $N-1$ down to 0 to recover the house indices.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Setup & Boundaries:** Check for empty array or single element.
- **Step 2: Main Exploration Loop:** Iterate each element in `nums`, updating `current = Math.Max(robPrev1, robPrev2 + money)`.
- **Step 3: Invariant Maintenance:** Rotate `robPrev2 = robPrev1` and `robPrev1 = current`.
- **Step 4: Resolution & Return:** Return `robPrev1`.

#### 4.3 Alternative Approaches Analysis
- **Approach 2: Tabulation with Subsequence Reconstruction:**
  - Allocate `dp = new int[N]`.
  - $dp[0] = nums[0]$, $dp[1] = \max(nums[0], nums[1])$.
  - For $i = 2 \dots N-1$: $dp[i] = \max(dp[i-1], dp[i-2] + nums[i])$.
  - To reconstruct: Start at $i = N-1$. If $dp[i] == dp[i-1]$, house $i$ was skipped; decrement $i$. Else, house $i$ was robbed; add $i$ to output list, decrement $i -= 2$.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: Space-Optimized Rolling State | Approach 2: Tabulation with Reconstruction |
| :--- | :--- | :--- |
| **Time Complexity (Best / Avg / Worst)** | $O(N)$ / $O(N)$ / $O(N)$ | $O(N)$ / $O(N)$ / $O(N)$ |
| **Auxiliary Space** | $O(1)$ scalar variables | $O(N)$ array storage |
| **Output Space** | $O(1)$ integer loot | $O(N)$ reconstructed house list |
| **Cache Locality** | Pure CPU registers | Sequential heap array access |
| **In-Place Mutability** | Non-destructive (read-only) | Non-destructive (read-only) |
| **Streaming Suitability** | High (processes stream online) | Low (requires full array to reconstruct) |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Problem #80 - House Robber
// Core Pattern: Include / Exclude 1D Dynamic Programming
// Primary Invariant: dp[i] = max(dp[i-1], dp[i-2] + nums[i])
// Space Defense: Two rolling registers achieve O(1) auxiliary space.
// Edge Defense: Handles empty array, single house, and all-zero amounts safely.
// ============================================================================
```

#### Implementation 1: Space-Optimized Rolling State (Production Standard)
```csharp
public class Solution
{
    public int Rob(int[] nums)
    {
        // Guard Clauses for boundary conditions
        if (nums == null || nums.Length == 0)
        {
            return 0;
        }

        if (nums.Length == 1)
        {
            return nums[0];
        }

        // robPrev2 holds max loot achievable up to house i - 2
        int robPrev2 = 0;
        // robPrev1 holds max loot achievable up to house i - 1
        int robPrev1 = 0;

        foreach (int money in nums)
        {
            // Invariant: At current house, choose between skipping (keep robPrev1)
            // or robbing (robPrev2 + money stashed in current house)
            int current = Math.Max(robPrev1, robPrev2 + money);

            // Shift historical registers forward
            robPrev2 = robPrev1;
            robPrev1 = current;
        }

        return robPrev1;
    }
}
```

#### Implementation 2: Tabulation with Path Reconstruction
```csharp
public class SolutionReconstruction
{
    public (int MaxLoot, List<int> RobbedHouseIndices) RobWithIndices(int[] nums)
    {
        if (nums == null || nums.Length == 0)
        {
            return (0, new List<int>());
        }

        int n = nums.Length;
        if (n == 1)
        {
            return (nums[0], new List<int> { 0 });
        }

        int[] dp = new int[n];
        dp[0] = nums[0];
        dp[1] = Math.Max(nums[0], nums[1]);

        for (int i = 2; i < n; i++)
        {
            dp[i] = Math.Max(dp[i - 1], dp[i - 2] + nums[i]);
        }

        // Backtrack to recover which houses were selected
        var indices = new List<int>();
        int curr = n - 1;

        while (curr >= 0)
        {
            if (curr == 0)
            {
                indices.Add(0);
                break;
            }
            else if (curr == 1)
            {
                indices.Add(nums[1] > nums[0] ? 1 : 0);
                break;
            }

            // If dp[curr] equals dp[curr - 1], current house was skipped
            if (dp[curr] == dp[curr - 1])
            {
                curr--;
            }
            else
            {
                // House curr was robbed; it contributed nums[curr] + dp[curr - 2]
                indices.Add(curr);
                curr -= 2;
            }
        }

        indices.Reverse();
        return (dp[n - 1], indices);
    }
}
```

---

## 81. Coin Change (LeetCode #322)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#1d-dp` `#unbounded-knapsack` `#bottom-up-tabulation` |
| **LeetCode Link** | [Coin Change](https://leetcode.com/problems/coin-change/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given an integer array `coins` representing coin denominations and an integer `amount`, return the fewest number of coins needed to make up that amount. If that amount cannot be formed, return `-1`. You may use an infinite number of each coin.
- **Key Constraints:**
  - $1 \le coins.Length \le 12$.
  - $1 \le coins[i] \le 2^{31} - 1$.
  - $0 \le amount \le 10^4$.
- **Senior Edge Cases to Defend:**
  - `amount == 0 \implies 0` coins.
  - Large coin denomination exceeds `amount` (must guard against negative index access).
  - Amount cannot be formed (return `-1`).
  - Sentinel overflow defense: Use `amount + 1` instead of `int.MaxValue` to prevent integer wrap-around when adding 1 (`int.MaxValue + 1 = int.MinValue`).

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Unbounded Knapsack (Shortest Path in a DAG). Minimum transitions from capacity 0 to target `amount`. Capacity iterates **forward** from $1 \dots amount$ to allow infinite reuse of each coin.
- **Sample 1:**
  - **Input:** `coins = [1, 2, 5]`, `amount = 11`
  - **Output:** `3`
  - **Explanation:** $11 = 5 + 5 + 1$ (3 coins).
- **Sample 2:**
  - **Input:** `coins = [2]`, `amount = 3`
  - **Output:** `-1`
  - **Explanation:** Amount 3 cannot be formed using only denomination 2.

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Picture a currency exchange vending machine. You want to pay an exact total of `amount` using the fewest number of coins. Think of each monetary amount from $0$ to `amount` as a stepping stone across a river. If you are standing on stone $a$, you can leap forward to stone $a + c$ by spending one coin of value $c$. Reversing the perspective: To reach stone $a$ with the minimum number of coins, you look back at all stones $a - c$ you could have leapt from, and take the best past result plus 1:
$$dp[a] = \min_{c \in coins} (dp[a - c] + 1)$$
Because coins can be used an infinite number of times, this is an **Unbounded Knapsack** problem.

#### 3.2 The Naive Bottleneck & Redundant Computation
A brute-force search explores all coin combinations via recursion:
```text
                         Change(11)
                     /       |       \
              Change(10)  Change(9)  Change(6)
               /   |  \
          Change(9)...
```
The search tree has depth up to $amount / \min(coins)$ and branching factor $|coins|$. For $amount = 100$ and coin $1$, depth is $100$, branching is $3$, yielding $O(C^A)$ operations. Subproblems like `Change(9)` and `Change(6)` are recomputed thousands of times along different permutations (e.g., $[5, 1]$ vs $[1, 5]$). Tabulation collapses this exponential tree into a linear state array of size $amount + 1$.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**Forward Sweep Direction Invariant:**
- In unbounded knapsack, capacity $a$ iterates **forward** from $1$ to $amount$.
- When evaluating $dp[a]$ with coin $c$, the subproblem referenced is $dp[a - c]$. Because $a - c < a$, $dp[a - c]$ has **already been evaluated in the current iteration**, meaning it may itself have already utilized coin $c$. This accurately models infinite coin availability.
- **Sentinel Invariant:** We initialize the DP table with $amount + 1$. Since the smallest coin denomination is $\ge 1$, the maximum possible coins for any amount is $amount$ (all 1s). Thus, $amount + 1$ acts as a strictly impenetrable infinity sentinel that cannot be mistaken for a valid coin count and never overflows signed 32-bit integers upon addition.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
CAPACITY SWEEP HORIZON:
dp[0] = 0 (Base Case: 0 coins for 0 amount)
[ dp[0] ... dp[a - 1] (Settled Optimal Capacities) ]  |  [ dp[a] (Active Capacity) ]  |  [ dp[a + 1] ... dp[amount] (Uncomputed) ]
└─────────────────────────┬─────────────────────────┘     └────────────┬───────────┘     └───────────────────┬──────────────────┘
            Known fewest coins for sub-amounts                 Testing all coins c                  Pending evaluation
```

- `a`: The active monetary capacity being resolved ($1 \le a \le amount$).
- `coin`: The candidate denomination being applied ($c \in coins$).
- `dp[a - c]`: The historical optimal solution for remainder $a - c$.
- **Invariant:** Prior to computing $dp[a]$, all entries $dp[0 \dots a - 1]$ contain the strictly minimal coin counts to form their respective amounts.

#### 3.5 State Transition Triggers & Decision Gates
For each capacity $a \in [1 \dots amount]$:
1. **Feasibility Gate:** Check if $a - c \ge 0$. If false, coin $c$ is too large for capacity $a$; skip it.
2. **Relaxation Gate:** If $dp[a - c] + 1 < dp[a]$, update:
   $$dp[a] = dp[a - c] + 1$$
3. **Terminal Gate:** At completion, if $dp[amount] > amount$, target is unreachable; return `-1`. Otherwise, return $dp[amount]$.

#### 3.6 Concrete Step-by-Step State Trace
Trace input: `coins = [1, 2, 5]`, `amount = 7`. Sentinel = $7 + 1 = 8$.

| Capacity $a$ | `dp` before testing coins | Coin $c=1$ (`dp[a-1]+1`) | Coin $c=2$ (`dp[a-2]+1`) | Coin $c=5$ (`dp[a-5]+1`) | Final $dp[a]$ |
| :---: | :---: | :---: | :---: | :---: | :---: |
| **0** | 0 (Base) | — | — | — | 0 |
| **1** | 8 | $dp[0]+1 = 1$ | Skip ($1-2 < 0$) | Skip ($1-5 < 0$) | 1 |
| **2** | 8 | $dp[1]+1 = 2$ | $dp[0]+1 = 1$ | Skip | 1 |
| **3** | 8 | $dp[2]+1 = 2$ | $dp[1]+1 = 2$ | Skip | 2 |
| **4** | 8 | $dp[3]+1 = 3$ | $dp[2]+1 = 2$ | Skip | 2 |
| **5** | 8 | $dp[4]+1 = 3$ | $dp[3]+1 = 3$ | $dp[0]+1 = 1$ | 1 |
| **6** | 8 | $dp[5]+1 = 2$ | $dp[4]+1 = 3$ | $dp[1]+1 = 2$ | 2 |
| **7** | 8 | $dp[6]+1 = 3$ | $dp[5]+1 = 2$ | $dp[2]+1 = 2$ | 2 |

Final answer: $dp[7] = 2$ (coins $5 + 2$).

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Bottom-Up 1D Tabulation):** Standard production choice. Excellent CPU cache locality, no call-stack overhead, easily vectorized by modern JIT compilers.
- **Approach 2 (Top-Down Memoized DFS):** Ideal when the target amount is very sparse relative to coin multiples (only a tiny fraction of intermediate amounts are reachable). Incurs recursion overhead and stack depth proportional to $amount$.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Setup & Boundaries:** If $amount == 0$, return 0 immediately. Allocate `dp` of length $amount + 1$, initialized with sentinel `amount + 1`. Set `dp[0] = 0`.
- **Step 2: Main Exploration Loop:** Loop $a$ from $1$ to $amount$. Inner loop through each `coin` in `coins`.
- **Step 3: Invariant Maintenance & Condition Gates:** If $a \ge coin$, relax $dp[a] = \min(dp[a], 1 + dp[a - coin])$.
- **Step 4: Resolution & Return:** If $dp[amount] > amount$ return `-1`, else return $dp[amount]$.

#### 4.3 Alternative Approaches Analysis
- **Top-Down Memoized DFS:**
  - Create `memo` array initialized to 0.
  - Recursively query `Dfs(rem)`. If $rem < 0$, return $-1$. If $rem == 0$, return $0$.
  - Probe all coins: compute `res = Dfs(rem - coin)`. If valid, update minimum.
  - Store in `memo[rem]` and return.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: Bottom-Up 1D Tabulation | Approach 2: Top-Down Memoized DFS |
| :--- | :--- | :--- |
| **Time Complexity (Best / Avg / Worst)** | $O(A \times C)$ | $O(A \times C)$ |
| **Auxiliary Space** | $O(A)$ array storage | $O(A)$ memo array + $O(A)$ recursion stack |
| **Output Space** | $O(1)$ scalar integer | $O(1)$ scalar integer |
| **Cache Locality** | Sequential linear stride | Non-sequential recursion stack jumps |
| **In-Place Mutability** | Non-destructive | Non-destructive |
| **Streaming Suitability** | Low (requires fixed target $A$) | Low (requires fixed target $A$) |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Problem #81 - Coin Change
// Core Pattern: Unbounded Knapsack / Forward Capacity Sweep
// Primary Invariant: dp[a] = min_{c in coins}(1 + dp[a - c])
// Sentinel Defense: Initialized to amount + 1 to prevent int.MaxValue + 1 overflow.
// Direction Invariant: Forward sweep 1..amount permits infinite coin reuse.
// ============================================================================
```

#### Implementation 1: Bottom-Up 1D Tabulation (Production Optimal)
```csharp
public class Solution
{
    public int CoinChange(int[] coins, int amount)
    {
        // Guard Clauses
        if (amount < 0) return -1;
        if (amount == 0) return 0;
        if (coins == null || coins.Length == 0) return -1;

        // Allocate DP table representing sub-amounts 0 .. amount
        int[] dp = new int[amount + 1];

        // Sentinel value: amount + 1 represents unreachable infinity
        // Avoids arithmetic overflow bugs that occur when adding 1 to int.MaxValue
        Array.Fill(dp, amount + 1);

        // Base case: Zero coins required to form an amount of 0
        dp[0] = 0;

        // Invariant: Forward iteration over capacity enables unbounded (infinite) coin reuse
        for (int a = 1; a <= amount; a++)
        {
            for (int i = 0; i < coins.Length; i++)
            {
                int coin = coins[i];

                // Feasibility check: Can only use coin if capacity >= coin denomination
                if (a - coin >= 0)
                {
                    // Relaxation gate: Min coins to reach a is 1 + min coins to reach (a - coin)
                    dp[a] = Math.Min(dp[a], 1 + dp[a - coin]);
                }
            }
        }

        // If dp[amount] exceeds amount, no valid combination was found
        return dp[amount] > amount ? -1 : dp[amount];
    }
}
```

#### Implementation 2: Top-Down Memoized DFS
```csharp
public class SolutionMemo
{
    public int CoinChange(int[] coins, int amount)
    {
        if (amount < 0) return -1;
        if (amount == 0) return 0;

        // memo[rem] stores fewest coins to form 'rem'; 0 indicates uncomputed
        int[] memo = new int[amount + 1];
        return ComputeMinCoins(coins, amount, memo);
    }

    private static int ComputeMinCoins(int[] coins, int rem, int[] memo)
    {
        // Base cases: Over-spent capacity
        if (rem < 0) return -1;
        // Exact change achieved
        if (rem == 0) return 0;

        // Cache hit: Return already validated optimal subproblem
        if (memo[rem] != 0) return memo[rem];

        int minCoins = int.MaxValue;

        foreach (int coin in coins)
        {
            int subResult = ComputeMinCoins(coins, rem - coin, memo);

            // If subproblem is reachable and beats current minimum
            if (subResult >= 0 && subResult < minCoins)
            {
                minCoins = 1 + subResult;
            }
        }

        // Store -1 if amount cannot be formed, otherwise store minCoins
        memo[rem] = (minCoins == int.MaxValue) ? -1 : minCoins;
        return memo[rem];
    }
}
```

---

## 82. Word Break (LeetCode #139)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#1d-dp` `#hash-set` `#string-partition` `#pruned-search` |
| **LeetCode Link** | [Word Break](https://leetcode.com/problems/word-break/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given a string `s` and a dictionary of strings `wordDict`, return `true` if `s` can be segmented into a space-separated sequence of one or more dictionary words. Dictionary words may be reused.
- **Key Constraints:**
  - $1 \le s.Length \le 300$.
  - $1 \le wordDict.Length \le 1000$.
  - $1 \le wordDict[i].Length \le 20$.
  - `s` and words consist of lowercase English letters.
- **Senior Edge Cases to Defend:**
  - Long string with overlapping prefixes (e.g. `s = "aaaaaaa"`, `wordDict = ["a", "aa", "aaa"]`).
  - No matching dictionary words.
  - Pruning defense: Substring check must be capped at `maxWordLength` to avoid $O(N^2)$ substring allocations.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** 1D DP on Prefix Partition: $dp[i] = \text{true}$ if prefix $s[0 \dots i - 1]$ can be segmented into valid dictionary words.
- **Sample 1:**
  - **Input:** `s = "leetcode"`, `wordDict = ["leet", "code"]`
  - **Output:** `true`
  - **Explanation:** Return true because `"leetcode"` can be segmented as `"leet code"`.
- **Sample 2:**
  - **Input:** `s = "catsandog"`, `wordDict = ["cats", "dog", "sand", "and", "cat"]`
  - **Output:** `false`
  - **Explanation:** Segmentations like `"cats sand og"` leave an invalid residual `"og"`.

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine a bridge constructed of stone pavers numbered $0$ to $N$. You stand at stone $0$ (the start of the string). You want to cross safely to stone $N$ (the end of the string). You can place a plank between stone $j$ and stone $i$ ($j < i$) if and only if:
1. Stone $j$ has already been safely reached ($dp[j] == \text{true}$).
2. The substring carved between stone $j$ and $i$ ($s[j \dots i-1]$) matches an authorized dictionary word.
Thus, reachability is an unweighted directed acyclic graph (DAG) path problem where each valid dictionary word constitutes a forward directed jump.

#### 3.2 The Naive Bottleneck & Redundant Computation
A brute-force recursive decomposition checks all possible prefixes of $s$, and recurses on the suffix:
```text
                         Break("leetcode")
                       /        |         \
            "l"+"eetcode"   "le"+"etcode"  "leet"+"code"
                 (x)             (x)          (Valid)
                                                 |
                                            Break("code")
```
For an input like `s = "aaaaaaaa"` with `wordDict = ["a", "aa", "aaa"]`, this explores all $2^{N-1}$ compositions of the string, running in $O(2^N)$ time. Identical suffixes (e.g., `s[3..N]`) are validated repeatedly. DP memoizes prefix reachability, reducing checks to at most $N$ prefix states.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**Prefix Reachability Bellman Equation:**
$$dp[i] = \bigvee_{j = \max(0, i - L)}^{i - 1} \Big( dp[j] \land (s[j \dots i - 1] \in \text{wordDict}) \Big)$$
where $L = \max_{w \in wordDict} |w|$ is the maximum length of any dictionary word.
- **Critical Pruning Invariant:** In natural language dictionaries, word lengths are strictly bounded ($L \le 20$). Therefore, the search for previous split point $j$ does **not** need to scan from $0$ to $i - 1$ ($O(N)$). It only needs to inspect $j \in [\max(0, i - L), i - 1]$.
- This drops the inner loop from $O(N)$ to $O(L)$, slashing total complexity from $O(N^3)$ to $O(N \cdot L^2)$ (accounting for string hashing/comparison).

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
STRING PREFIX REACHABILITY PARTITION:
s = [  l   e   e   t   c   o   d   e  ]
     0   1   2   3   4   5   6   7   8
dp: [ T , F , F , F , T , F , F , F , ? ]
     └──────┬──────┘   └──────┬──────┘   └──┬──┘
    Settled Prefixes   Active Window L    Evaluating dp[8]
```

- `i`: Target prefix length being evaluated ($1 \le i \le N$).
- `j`: Potential split index separating the valid prefix $s[0 \dots j-1]$ and candidate suffix word $s[j \dots i-1]$.
- `dp[i]`: Boolean indicating whether prefix $s[0 \dots i-1]$ can be completely partitioned into dictionary words.
- **Base Case Invariant:** $dp[0] = \text{true}$ (the empty string prefix is vacuously valid).

#### 3.5 State Transition Triggers & Decision Gates
For each prefix length $i$ from $1$ to $N$:
1. **Window Boundary Gate:** Set `minJ = Math.Max(0, i - maxWordLen)`.
2. **Reverse Probe Gate:** Scan $j$ backwards from $i - 1$ down to `minJ`.
3. **Double-Validation Gate:**
   - Check if $dp[j] == \text{true}$. If false, skip substring extraction immediately.
   - If $dp[j] == \text{true}$, extract substring $s[j \dots i-1]$ and probe `HashSet<string>`.
   - If match found, set $dp[i] = \text{true}$ and `break` (early exit for inner loop).

#### 3.6 Concrete Step-by-Step State Trace
Trace input: `s = "leetcode"`, `wordDict = ["leet", "code"]`, `maxWordLen = 4`.

| $i$ | Substring evaluated | $j$ probed | $dp[j]$ | Substring $s[j \dots i-1]$ | In Dict? | $dp[i]$ | Notes |
| :---: | :---: | :---: | :---: | :---: | :---: | :---: | :--- |
| **0** | `""` | — | — | — | — | **True** | Base case: Empty prefix |
| **1** | `"l"` | 0 | True | `"l"` | No | False | |
| **2** | `"le"` | 1, 0 | False, True | `"e"`, `"le"` | No | False | |
| **3** | `"lee"`| 2, 1, 0 | F, F, T | `"e"`, `"ee"`, `"lee"` | No | False | |
| **4** | `"leet"`| 0 | True | `"leet"` | **Yes** | **True** | Match found at $j=0$! Early break |
| **5** | `"leetc"`| 4 | True | `"c"` | No | False | $j=0$ out of window ($5 - 0 > 4$) |
| **6** | `"leetco"`| 4 | True | `"co"` | No | False | |
| **7** | `"leetcod"`| 4 | True | `"cod"` | No | False | |
| **8** | `"leetcode"`| 4 | True | `"code"` | **Yes** | **True** | Match found at $j=4$! Early break |

Final return: $dp[8] = \text{true}$.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (1D DP with Max-Word-Length Pruning):** Preferred in 95% of software engineering interviews. It uses standard hash sets, requires minimal boilerplate, and executes with optimal $O(N \cdot L^2)$ time where $L \le 20$.
- **Approach 2 (Trie-Guided DP):** Optimal when word dictionary contains hundreds of thousands of words and character-level streaming is desired. Replaces hash lookups with $O(1)$ child pointer traversals, eliminating string allocation overhead.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Setup & Boundaries:** Convert `wordDict` to `HashSet<string>` for $O(1)$ average lookups. Precompute `maxWordLen`. Allocate `dp` of size $N + 1$ with $dp[0] = \text{true}$.
- **Step 2: Main Exploration Loop:** Outer loop $i \in [1 \dots N]$. Inner loop $j \in [i - 1 \dots \max(0, i - maxWordLen)]$.
- **Step 3: Condition Gates:** Test $dp[j]$ first before calling `Substring`. If match found, set $dp[i] = \text{true}$ and break.
- **Step 4: Resolution & Return:** Return $dp[N]$.

#### 4.3 Alternative Approaches Analysis
- **Approach 2: Trie-Guided DP ($O(N \cdot L)$ Time):**
  - Insert all dictionary words into a Prefix Trie.
  - For each starting index $i$ where $dp[i] == \text{true}$, traverse down the Trie with characters $s[i], s[i+1], \dots$.
  - Whenever a Trie node has `isWord == true`, mark $dp[\text{currIndex} + 1] = \text{true}$.
  - Completely avoids string allocations/hashing.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: 1D DP with Length Pruning | Approach 2: Trie-Guided DP |
| :--- | :--- | :--- |
| **Time Complexity (Best / Avg / Worst)** | $O(N)$ / $O(N \cdot L^2)$ / $O(N \cdot L^2)$ | $O(N)$ / $O(N \cdot L)$ / $O(N \cdot L)$ |
| **Auxiliary Space** | $O(N + D \cdot L)$ | $O(N + \text{Trie Nodes})$ |
| **Output Space** | $O(1)$ boolean | $O(1)$ boolean |
| **Cache Locality** | High (sequential array reads) | Moderate (pointer chasing in Trie) |
| **In-Place Mutability** | Non-destructive | Non-destructive |
| **Streaming Suitability** | High (can process character by character) | High |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Problem #82 - Word Break
// Core Pattern: 1D Dynamic Programming / Prefix Partition Reachability
// Primary Invariant: dp[i] is true iff dp[j] is true and s[j..i-1] in dict.
// Pruning Defense: Restrict inner split check to j >= max(0, i - maxWordLen).
// Memory Defense: Check dp[j] BEFORE Substring allocation to save GC heap churn.
// ============================================================================
```

#### Implementation 1: 1D DP with Max Length Pruning (Optimal Standard)
```csharp
public class Solution
{
    public bool WordBreak(string s, IList<string> wordDict)
    {
        // Guard Clause
        if (string.IsNullOrEmpty(s) || wordDict == null || wordDict.Count == 0)
        {
            return false;
        }

        // Store words in a HashSet for O(1) expected lookup
        var dict = new HashSet<string>(wordDict);

        // Precompute maximum word length in dictionary to prune redundant checks
        int maxWordLen = 0;
        foreach (string word in wordDict)
        {
            if (word.Length > maxWordLen)
            {
                maxWordLen = word.Length;
            }
        }

        int n = s.Length;
        // dp[i] indicates whether prefix s[0 .. i - 1] can be segmented
        bool[] dp = new bool[n + 1];

        // Base case: An empty prefix represents a valid segmentation origin
        dp[0] = true;

        for (int i = 1; i <= n; i++)
        {
            // Pruning: A valid word ending at i cannot start earlier than i - maxWordLen
            int minJ = Math.Max(0, i - maxWordLen);

            // Iterate backwards to favor longer candidate words and hit early break sooner
            for (int j = i - 1; j >= minJ; j--)
            {
                // Invariant Guard: Only allocate and probe substring if prefix [0 .. j - 1] is valid
                if (dp[j] && dict.Contains(s.Substring(j, i - j)))
                {
                    dp[i] = true;
                    // Early exit: Single valid partition suffices to seal dp[i] as true
                    break;
                }
            }
        }

        return dp[n];
    }
}
```

#### Implementation 2: Trie-Guided DP (Zero Substring Allocation)
```csharp
public class SolutionTrie
{
    private class TrieNode
    {
        public readonly TrieNode[] Children = new TrieNode[26];
        public bool IsWord;
    }

    public bool WordBreak(string s, IList<string> wordDict)
    {
        if (string.IsNullOrEmpty(s) || wordDict == null || wordDict.Count == 0)
        {
            return false;
        }

        // Build Prefix Trie from dictionary
        var root = new TrieNode();
        foreach (string word in wordDict)
        {
            var curr = root;
            foreach (char c in word)
            {
                int idx = c - 'a';
                if (curr.Children[idx] == null)
                {
                    curr.Children[idx] = new TrieNode();
                }
                curr = curr.Children[idx];
            }
            curr.IsWord = true;
        }

        int n = s.Length;
        bool[] dp = new bool[n + 1];
        dp[0] = true;

        // Traverse string; whenever dp[i] is true, match forward down the Trie
        for (int i = 0; i < n; i++)
        {
            if (!dp[i]) continue;

            var curr = root;
            for (int j = i; j < n; j++)
            {
                int idx = s[j] - 'a';
                if (curr.Children[idx] == null)
                {
                    // Character branch does not exist in dictionary; abort forward scan
                    break;
                }

                curr = curr.Children[idx];
                if (curr.IsWord)
                {
                    dp[j + 1] = true;
                }
            }
        }

        return dp[n];
    }
}
```

---

## 83. Partition Equal Subset Sum (LeetCode #416)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | 🔥 High |
| **Pattern Tags** | `#0-1-knapsack` `#backward-sweep` `#boolean-reachability` |
| **LeetCode Link** | [Partition Equal Subset Sum](https://leetcode.com/problems/partition-equal-subset-sum/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given an integer array `nums`, return `true` if you can partition the array into two subsets such that the sum of elements in both subsets is equal.
- **Key Constraints:**
  - $1 \le nums.Length \le 200$.
  - $1 \le nums[i] \le 100$.
  - Total sum $\le 2 \times 10^4 \implies target \le 10^4$.
- **Senior Edge Cases to Defend:**
  - Total sum is odd $\implies$ mathematically impossible to divide into two equal integers, return `false` immediately.
  - Maximum single element $> target \implies$ impossible to balance, return `false`.
  - Array length $< 2 \implies$ cannot partition into two non-empty subsets.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** 0-1 Knapsack Boolean Reachability: Partitioning array into two equal subsets reduces to finding a single subset summing to $target = \text{totalSum} / 2$.
- **Sample 1:**
  - **Input:** `nums = [1, 5, 11, 5]`
  - **Output:** `true`
  - **Explanation:** The array can be partitioned as `[1, 5, 5]` and `[11]`, both summing to 11.
- **Sample 2:**
  - **Input:** `nums = [1, 2, 3, 5]`
  - **Output:** `false`
  - **Explanation:** Sum is 11 (odd); cannot be partitioned into two equal integer halves.

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine a mechanical scale balance. You want to place numbers on both sides until the scale balances perfectly. The total weight on the scale is $\sum nums$. If total weight is odd, balancing is physically impossible. If even, balancing is achieved if and only if you can pack a knapsack of capacity exactly $target = \text{totalSum} / 2$. Because each number in `nums` can be chosen **at most once**, this is the quintessential **0-1 Knapsack** problem.

#### 3.2 The Naive Bottleneck & Redundant Computation
A brute-force enumeration evaluates all $2^N$ subsets (the power set). For each element, we branch into:
1. Include $nums[i]$ in subset.
2. Exclude $nums[i]$ from subset.
```text
                         Subset(0, sum=0)
                       /                  \
             Include nums[0]             Exclude nums[0]
             /             \             /             \
       Include nums[1]   Exclude... Include nums[1]   Exclude...
```
For $N = 200$, $2^{200} \approx 1.6 \times 10^{60}$ states. However, the maximum possible target sum is bounded by $200 \times 100 / 2 = 10,000$. By caching the reachable subset sums, the state space contracts from $O(2^N)$ to $O(N \times target) \le 200 \times 10,000 = 2 \times 10^6$ operations.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**0-1 Knapsack Backward Sweep Invariant:**
In a 1D DP array of size $target + 1$, capacity $w$ must be updated **strictly in reverse** (from $target$ down to $num$):
$$dp[w] = dp[w] \lor dp[w - num]$$
- **Why Backward?** If we iterated forward ($num \to target$), then $dp[w - num]$ would have already been updated in the **current pass**, meaning element $num$ could be added repeatedly multiple times (unbounded knapsack behavior).
- By sweeping **backward**, when we compute $dp[w]$, the value $dp[w - num]$ is guaranteed to reflect the reachability state from the **previous element** ($i - 1$), strictly enforcing the 0-1 single-use invariant!

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
CAPACITY REVERSE SWEEP ARCHITECTURE:
dp array: [ 0 ........................................ target ]
Sweep direction: <===========================================
[ 0 ... w - num (Prior State) ]  |  [ w (Active Target) ]  |  [ w + 1 ... target (Updated Current State) ]
└──────────────┬──────────────┘     └─────────┬─────────┘     └──────────────────────┬──────────────────────┘
   Untouched prior layer               Testing num                 Already sealed for current item
```

- `num`: The current candidate integer being incorporated from `nums`.
- `w`: The capacity cursor scanning backwards from $target$ down to $num$.
- `dp[w]`: Boolean indicating whether a subset with exact sum $w$ can be formed.
- **Invariant:** Before `num` is evaluated at capacity $w$, all values $dp[0 \dots w]$ originate strictly from elements prior to `num`.

#### 3.5 State Transition Triggers & Decision Gates
For each number $num \in nums$:
1. **Capacity Scan Gate:** Loop $w$ from $target$ down to $num$.
2. **OR-Union Gate:** Set $dp[w] = dp[w] \lor dp[w - num]$.
3. **Early Exit Gate:** If $dp[target] == \text{true}$, terminate entire algorithm immediately and return `true`.

#### 3.6 Concrete Step-by-Step State Trace
Trace input: `nums = [1, 5, 11, 5]`, `totalSum = 22`, `target = 11`.
Initial `dp` array of size 12: `dp[0] = True`, all others `False`.

| Step | Current `num` | Reverse Sweep Range $w$ | Newly Reached Capacities ($dp[w] = \text{true}$) | All Reachable Sums |
| :---: | :---: | :---: | :---: | :--- |
| **Init** | — | — | $dp[0] = \text{True}$ | `{0}` |
| **1** | 1 | $11 \to 1$ | $dp[1] = dp[1] \lor dp[0] = \text{True}$ | `{0, 1}` |
| **2** | 5 | $11 \to 5$ | $dp[6] = dp[1] = \text{T}$, $dp[5] = dp[0] = \text{T}$ | `{0, 1, 5, 6}` |
| **3** | 11 | $11 \to 11$ | $dp[11] = dp[0] = \text{T}$ | `{0, 1, 5, 6, 11}` |

At step 3, $dp[11] == \text{True}$! Early exit triggered. Return `true`.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (1D 0-1 Knapsack Backward Sweep):** Standard interview recommendation. $O(N \times \text{target})$ time and $O(\text{target})$ space. Simple, reliable, and includes early termination.
- **Approach 2 (BitSet / Bitwise Left-Shift):** Systems-level optimization. Represent the DP boolean array as a `BitArray` or integer bitmask. Transition reduces to:
  $$\text{bits} = \text{bits} \mid (\text{bits} \ll num)$$
  Executes $64\times$ faster by leveraging word-level hardware bitwise parallelism in CPU registers.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Setup & Boundaries:** Sum elements. If sum is odd, return `false`. Let $target = sum / 2$. Allocate boolean `dp` of size $target + 1$ with $dp[0] = \text{true}$.
- **Step 2: Main Exploration Loop:** For each $num \in nums$, sweep $w$ backwards from $target$ down to $num$.
- **Step 3: Invariant Maintenance & Early Exit:** $dp[w] = dp[w] \lor dp[w - num]$. If $dp[target]$ is true, return `true`.
- **Step 4: Resolution & Return:** Return $dp[target]$.

#### 4.3 Alternative Approaches Analysis
- **Approach 2: Bitwise BitSet Invariant:**
  - Initialize a dynamic bit array with bit 0 set: `bits[0] = 1`.
  - For each `num`, left shift the bitset by `num` and bitwise OR with itself:
    $$\text{bits} \mathrel{\vert}= (\text{bits} \ll num)$$
  - Target is reachable if bit at index $target$ is set. Time complexity: $O(N \times target / 64)$.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: 1D Knapsack Backward Sweep | Approach 2: BitSet Word Parallelism |
| :--- | :--- | :--- |
| **Time Complexity (Best / Avg / Worst)** | $O(N)$ / $O(N \times S)$ / $O(N \times S)$ | $O(N \times S / 64)$ |
| **Auxiliary Space** | $O(S)$ boolean array ($S = \text{target}$) | $O(S / 64)$ 64-bit integer words |
| **Output Space** | $O(1)$ boolean | $O(1)$ boolean |
| **Cache Locality** | Sequential cache line reads | High (fits entirely in L1 data cache) |
| **In-Place Mutability** | Non-destructive | Non-destructive |
| **Streaming Suitability** | High (can process items online) | High |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Problem #83 - Partition Equal Subset Sum
// Core Pattern: 0-1 Knapsack / Backward Capacity Sweep
// Primary Invariant: dp[w] = dp[w] || dp[w - num] swept backward from target to num.
// Parity Defense: Odd totalSum immediately returns false.
// Optimization: Early exit if dp[target] becomes true before all numbers processed.
// ============================================================================
```

#### Implementation 1: 1D Knapsack Backward Sweep (Production Standard)
```csharp
public class Solution
{
    public bool CanPartition(int[] nums)
    {
        // Guard Clause: Minimum 2 elements required for partition
        if (nums == null || nums.Length < 2)
        {
            return false;
        }

        int totalSum = 0;
        int maxNum = 0;
        foreach (int num in nums)
        {
            totalSum += num;
            if (num > maxNum) maxNum = num;
        }

        // Parity Defense: An odd sum cannot be divided into two equal integer halves
        if ((totalSum & 1) == 1)
        {
            return false;
        }

        int target = totalSum / 2;

        // Boundary Defense: If any single number exceeds target, subsets cannot balance
        if (maxNum > target)
        {
            return false;
        }

        // dp[w] indicates whether a subset sum of exact weight w can be formed
        bool[] dp = new bool[target + 1];

        // Base case: A subset sum of 0 is always achievable (the empty subset)
        dp[0] = true;

        foreach (int num in nums)
        {
            // CRITICAL INVARIANT: Iterate capacity BACKWARD from target down to num
            // Backward traversal guarantees each number is used at most once (0-1 knapsack)
            // If iterated forward, dp[w - num] could have used 'num' in the current iteration
            for (int w = target; w >= num; w--)
            {
                dp[w] = dp[w] || dp[w - num];
            }

            // Early Exit Optimization: Target already formed; terminate immediately
            if (dp[target])
            {
                return true;
            }
        }

        return dp[target];
    }
}
```

#### Implementation 2: BitSet Word Parallelism (Bitwise Simulation)
```csharp
public class SolutionBitSet
{
    public bool CanPartition(int[] nums)
    {
        if (nums == null || nums.Length < 2) return false;

        int totalSum = 0;
        foreach (int num in nums) totalSum += num;

        if ((totalSum & 1) == 1) return false;
        int target = totalSum / 2;

        // Represent boolean array as an array of 64-bit unsigned integers (ulong)
        int numWords = (target / 64) + 1;
        ulong[] bitset = new ulong[numWords];

        // Base case: Bit 0 is set (subset sum of 0)
        bitset[0] = 1UL;

        foreach (int num in nums)
        {
            // Shift bitset left by num bits and OR with existing bitset
            ShiftLeftAndOr(bitset, num, target);

            // Check if bit at 'target' position is set
            int targetWord = target / 64;
            int targetBit = target % 64;
            if ((bitset[targetWord] & (1UL << targetBit)) != 0)
            {
                return true;
            }
        }

        return false;
    }

    private static void ShiftLeftAndOr(ulong[] bitset, int shift, int maxTarget)
    {
        int wordShift = shift / 64;
        int bitShift = shift % 64;
        int maxWord = maxTarget / 64;

        for (int i = maxWord; i >= wordShift; i--)
        {
            ulong carry = 0;
            if (bitShift > 0 && i - wordShift - 1 >= 0)
            {
                carry = bitset[i - wordShift - 1] >> (64 - bitShift);
            }

            ulong shifted = (bitset[i - wordShift] << bitShift) | carry;
            bitset[i] |= shifted;
        }
    }
}
```

---

## 84. Longest Increasing Subsequence (LeetCode #300)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#dp` `#patience-sorting` `#binary-search` `#O(n-log-n)` |
| **LeetCode Link** | [Longest Increasing Subsequence](https://leetcode.com/problems/longest-increasing-subsequence/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given an integer array `nums`, return the length of the longest strictly increasing subsequence. Solve in $O(N \log N)$ time.
- **Key Constraints:**
  - $1 \le nums.Length \le 2500$.
  - $-10^4 \le nums[i] \le 10^4$.
- **Senior Edge Cases to Defend:**
  - Array strictly decreasing (LIS length $= 1$).
  - Array containing duplicate values: Strict inequality requires replacing the first element $\ge x$ (not $> x$).
  - All elements identical: LIS length $= 1$.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Patience Sorting: Maintain a strictly increasing array `tails` where `tails[i]` stores the smallest tail value of all increasing subsequences of length $i + 1$.
- **Sample 1:**
  - **Input:** `nums = [10, 9, 2, 5, 3, 7, 101, 18]`
  - **Output:** `4`
  - **Explanation:** The longest increasing subsequence is `[2, 3, 7, 101]`, therefore the length is 4. (Another valid LIS is `[2, 3, 7, 18]`).
- **Sample 2:**
  - **Input:** `nums = [0, 1, 0, 3, 2, 3]`
  - **Output:** `4`
  - **Explanation:** `[0, 1, 2, 3]`.

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine playing a card solitaire game called **Patience Sorting**. As cards from `nums` are dealt one by one:
- You place card $x$ onto the leftmost pile whose visible top card is $\ge x$. This covers that top card, reducing the pile's value.
- If card $x$ is strictly greater than all visible pile tops, you open a brand new pile to the far right.
At the end of the game, the total number of piles is mathematically guaranteed to equal the length of the Longest Increasing Subsequence!

#### 3.2 The Naive Bottleneck & Redundant Computation
A classical dynamic programming approach defines:
$$dp[i] = 1 + \max_{j < i, nums[j] < nums[i]} dp[j]$$
For every element $i$, we must scan all $j \in [0 \dots i - 1]$:
$$T(N) = \sum_{i=1}^{N-1} i = \frac{N(N - 1)}{2} \implies O(N^2)$$
For $N = 2500$, $N^2 \approx 6.25 \times 10^6$ operations. While passable on older benchmarks, when $N = 10^5$, $N^2 = 10^{10}$, which times out severely. Patience sorting optimizes this search from $O(N)$ linear scans to $O(\log N)$ binary searches.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**Strict Monotonicity of the `tails` Array:**
Let `tails[k]` be the minimum possible ending element of any increasing subsequence of length $k + 1$ found so far.
- **Invariant:** `tails` is strictly monotonically increasing:
  $$\text{tails}[0] < \text{tails}[1] < \text{tails}[2] < \dots < \text{tails}[L - 1]$$
- **Proof:** Suppose $\text{tails}[k] \ge \text{tails}[k + 1]$. The subsequence of length $k + 2$ ending with $\text{tails}[k + 1]$ has an immediate predecessor of length $k + 1$ whose tail must be $< \text{tails}[k + 1] \le \text{tails}[k]$. This contradicts the definition of $\text{tails}[k]$ as the minimum tail of length $k + 1$. Thus, strict monotonicity holds.
- Because `tails` is sorted, we can use **Binary Search** (lower bound: first element $\ge x$) to locate where card $x$ lands in $O(\log L)$ time.
- **Greedy Tail Tightening:** Replacing $\text{tails}[idx]$ with a smaller $x$ leaves maximal headroom for future numbers to extend the subsequence.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
PATIENCE SORTING TAILS ARCHITECTURE:
tails array: [ tails[0] < tails[1] < ... < tails[length - 1] ]
Incoming element x:
Binary search for first element >= x:
If found at idx < length:  tails[idx] = x  (Tightens tail for length idx + 1)
If left == length:         tails[length] = x; length++ (Discovers longer subsequence)
```

- `tails[idx]`: Stores the smallest terminal element of an increasing subsequence of length $idx + 1$.
- `length`: The active maximum length of an increasing subsequence found so far.
- `left, right`: Binary search cursors locating the lower bound of incoming candidate $x$.

#### 3.5 State Transition Triggers & Decision Gates
For each number $x \in nums$:
1. **Binary Search Gate:** Find `idx = BinarySearchLowerBound(tails, 0, length - 1, x)`.
2. **Replacement Gate:** Set `tails[idx] = x`.
3. **Extension Gate:** If `idx == length`, increment `length++`.

#### 3.6 Concrete Step-by-Step State Trace
Trace input: `nums = [10, 9, 2, 5, 3, 7, 101, 18]`

| Step | Incoming $x$ | Binary Search Target | Binary Search Result `idx` | `tails` Array After Update | Active LIS Length |
| :---: | :---: | :---: | :---: | :--- | :---: |
| **1** | 10 | First element $\ge 10$ | `idx = 0` | `[10]` | 1 |
| **2** | 9 | First element $\ge 9$ | `idx = 0` | `[9]` (10 replaced by 9) | 1 |
| **3** | 2 | First element $\ge 2$ | `idx = 0` | `[2]` (9 replaced by 2) | 1 |
| **4** | 5 | First element $\ge 5$ | `idx = 1` | `[2, 5]` (Appended) | 2 |
| **5** | 3 | First element $\ge 3$ | `idx = 1` | `[2, 3]` (5 replaced by 3) | 2 |
| **6** | 7 | First element $\ge 7$ | `idx = 2` | `[2, 3, 7]` (Appended) | 3 |
| **7** | 101 | First element $\ge 101$ | `idx = 3` | `[2, 3, 7, 101]` (Appended) | 4 |
| **8** | 18 | First element $\ge 18$ | `idx = 3` | `[2, 3, 7, 18]` (101 replaced by 18) | 4 |

Final LIS length: `4`.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Patience Sorting with Binary Search):** Strictly optimal $O(N \log N)$ time and $O(N)$ space. Mandatory whenever $N > 2500$ or when the interviewer requests $O(N \log N)$.
- **Approach 2 (Tabulation 1D DP):** $O(N^2)$ time and $O(N)$ space. Best when the interviewer explicitly asks to **reconstruct the actual sequence**, as standard patience sorting overwrites tail values and does not store parents.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Setup & Boundaries:** Check if array is null or empty. Allocate `tails` array of size $N$. Initialize `length = 0`.
- **Step 2: Main Exploration Loop:** Loop through each $x \in nums$.
- **Step 3: Binary Search Predicate:** Set `left = 0, right = length - 1`. While `left <= right`, if `tails[mid] >= x` set `right = mid - 1`, else `left = mid + 1`.
- **Step 4: Update & Extension:** Set `tails[left] = x`. If `left == length`, increment `length++`.
- **Step 5: Resolution & Return:** Return `length`.

#### 4.3 Alternative Approaches Analysis
- **Approach 2: $O(N^2)$ DP with Full Parent Tracking:**
  - Allocate `dp = new int[N]` (filled with 1) and `parent = new int[N]` (filled with -1).
  - For $i = 1 \dots N-1$: For $j = 0 \dots i-1$: if $nums[j] < nums[i]$ and $dp[j] + 1 > dp[i]$, set $dp[i] = dp[j] + 1$ and $parent[i] = j$.
  - Find index with maximum $dp[i]$, and trace backwards via `parent` pointers to reconstruct the exact LIS.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: Patience Sorting + Binary Search | Approach 2: Tabulation 1D DP |
| :--- | :--- | :--- |
| **Time Complexity (Best / Avg / Worst)** | $O(N \log N)$ / $O(N \log N)$ / $O(N \log N)$ | $O(N^2)$ / $O(N^2)$ / $O(N^2)$ |
| **Auxiliary Space** | $O(N)$ tails array | $O(N)$ dp array |
| **Output Space** | $O(1)$ length scalar | $O(1)$ length (or $O(L)$ reconstructed path) |
| **Cache Locality** | High (small active `tails` array) | Moderate (quadratic nested scans) |
| **In-Place Mutability** | Non-destructive | Non-destructive |
| **Streaming Suitability** | High (processes numbers online) | Low (requires full array) |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Problem #84 - Longest Increasing Subsequence
// Core Pattern: Patience Sorting / Monotonic Tails with Binary Search
// Primary Invariant: tails[k] stores the smallest tail of all increasing subseqs of length k+1.
// Strict Inequality: Strictly increasing means we replace tails[mid] >= x.
// Complexity Target: O(N log N) time, O(N) auxiliary space.
// ============================================================================
```

#### Implementation 1: Patience Sorting with Binary Search ($O(N \log N)$ Production Standard)
```csharp
public class Solution
{
    public int LengthOfLIS(int[] nums)
    {
        // Guard Clause
        if (nums == null || nums.Length == 0)
        {
            return 0;
        }

        // tails[k] stores smallest tail value of all valid increasing subsequences of length k + 1
        int[] tails = new int[nums.Length];
        int length = 0;

        foreach (int x in nums)
        {
            // Binary search for lower bound of x in tails[0 .. length - 1]
            // Lower bound: First index where tails[idx] >= x
            int left = 0;
            int right = length - 1;

            while (left <= right)
            {
                // Defensive mid calculation to avoid integer overflow
                int mid = left + (right - left) / 2;

                if (tails[mid] >= x)
                {
                    // Look for earlier candidate on the left
                    right = mid - 1;
                }
                else
                {
                    // tails[mid] < x, search right
                    left = mid + 1;
                }
            }

            // Invariant Gate:
            // If left == length: x is strictly greater than all tails, extending LIS
            // If left < length:  x lowers the upper bound for subsequences of length left + 1
            tails[left] = x;
            if (left == length)
            {
                length++;
            }
        }

        return length;
    }
}
```

#### Implementation 2: Tabulation 1D DP ($O(N^2)$ with Path Reconstruction)
```csharp
public class SolutionDpReconstruction
{
    public (int Length, List<int> Subsequence) LengthOfLISWithSequence(int[] nums)
    {
        if (nums == null || nums.Length == 0)
        {
            return (0, new List<int>());
        }

        int n = nums.Length;
        int[] dp = new int[n];
        int[] parent = new int[n];
        Array.Fill(dp, 1);
        Array.Fill(parent, -1);

        int maxLen = 1;
        int bestEndIdx = 0;

        for (int i = 1; i < n; i++)
        {
            for (int j = 0; j < i; j++)
            {
                // Invariant: If strictly increasing and improves prior prefix length
                if (nums[j] < nums[i] && dp[j] + 1 > dp[i])
                {
                    dp[i] = dp[j] + 1;
                    parent[i] = j;
                }
            }

            if (dp[i] > maxLen)
            {
                maxLen = dp[i];
                bestEndIdx = i;
            }
        }

        // Reconstruct the actual sequence
        var result = new List<int>();
        int curr = bestEndIdx;
        while (curr != -1)
        {
            result.Add(nums[curr]);
            curr = parent[curr];
        }

        result.Reverse();
        return (maxLen, result);
    }
}
```

---

## 85. Maximum Product Subarray (LeetCode #152)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | 🔥 High |
| **Pattern Tags** | `#dp` `#dual-state-tracking` `#sign-flip-inversion` |
| **LeetCode Link** | [Maximum Product Subarray](https://leetcode.com/problems/maximum-product-subarray/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given an integer array `nums`, find a contiguous subarray that has the largest product, and return that product.
- **Key Constraints:**
  - $1 \le nums.Length \le 2 \times 10^4$.
  - $-10 \le nums[i] \le 10$.
  - The product of any prefix or suffix of `nums` is guaranteed to fit in a 32-bit integer.
- **Senior Edge Cases to Defend:**
  - Single negative element (e.g., `nums = [-2] \implies -2`).
  - Zero barriers (zeros reset running products and isolate subarrays).
  - Even vs odd count of negative numbers.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Dual-State DP Tracking: Because multiplying two negative numbers yields a positive number, a large negative running product can flip into the maximum positive product upon encountering another negative number.
- **Sample 1:**
  - **Input:** `nums = [2, 3, -2, 4]`
  - **Output:** `6`
  - **Explanation:** `[2, 3]` gives the largest product 6.
- **Sample 2:**
  - **Input:** `nums = [-2, 0, -1]`
  - **Output:** `0`
  - **Explanation:** The result cannot be 2, because `[-2, -1]` is not a contiguous subarray. Max product is 0.

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine a rollercoaster ride with sharp sign-flipping portals. Positive numbers amplify your altitude (both peaks and valleys). But when you pass through a negative portal, the entire topography inverts: the highest mountain peak flips into the deepest oceanic trench, and the deepest trench vaults into the highest mountain peak! Zero acts as a black hole: any trajectory falling into zero is absorbed and resets to zero.
Because of this inversion property, tracking only the maximum running product (as in Kadane's algorithm for sum) is mathematically insufficient. You must simultaneously track the **extreme minimum** (the largest negative value) to capture the rebound upon the next negative number.

#### 3.2 The Naive Bottleneck & Redundant Computation
A brute-force solution checks all possible contiguous subarrays $[i \dots j]$:
$$T(N) = \sum_{i=0}^{N-1} \sum_{j=i}^{N-1} 1 \implies O(N^2)$$
For $N = 2 \times 10^4$, $N^2 = 4 \times 10^8$ operations, exceeding standard time limits. Moreover, naive multiplication can cause integer overflow if not pruned. Tracking dual states solves this in a single pass of $O(N)$ time with $O(1)$ space.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**Sign-Flip Inversion Invariant:**
At index $i$, the candidate maximum and minimum ending at $i$ are selected from:
1. Starting a brand-new subarray at $nums[i]$.
2. Extending the previous maximum: $currentMax \times nums[i]$.
3. Extending the previous minimum: $currentMin \times nums[i]$.

When $nums[i] < 0$, multiplying by a negative number reverses inequalities:
$$A > B \implies A \times (-k) < B \times (-k)$$
Thus, the former minimum multiplied by a negative number becomes the new candidate maximum!
- **State Inversion Gate:** If $nums[i] < 0$, simply swap `(currentMax, currentMin)` before taking the standard Kadane transitions:
  $$currentMax = \max(nums[i], currentMax \times nums[i])$$
  $$currentMin = \min(nums[i], currentMin \times nums[i])$$

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
DUAL-STATE INVERSION HORIZON:
Incoming num < 0:
Swap(currentMax, currentMin)
[ 0 ... i - 1 ]           |     i (Current)     |  [ i + 1 ... N - 1 ]
currentMax, currentMin    |  Apply num to both  |  Pending
└──────────┬───────────┘     └────────┬────────┘     └───────┬───────┘
  Settled Running Extrema        Updated Extrema         Unexplored
```

- `globalMax`: Global maximum product encountered anywhere across all processed subarrays. Initialized to `nums[0]`.
- `currentMax`: The maximum product of a contiguous subarray ending strictly at index $i$.
- `currentMin`: The minimum product of a contiguous subarray ending strictly at index $i$.
- **Invariant:** After processing index $i$, `currentMax` and `currentMin` accurately store the true supremum and infimum contiguous products terminating at index $i$.

#### 3.5 State Transition Triggers & Decision Gates
For each index $i$ from $1$ to $N - 1$:
1. **Sign Detection Gate:** If $nums[i] < 0$, swap `currentMax` and `currentMin`.
2. **Max Relaxation Gate:** `currentMax = Math.Max(nums[i], currentMax * nums[i])`.
3. **Min Relaxation Gate:** `currentMin = Math.Min(nums[i], currentMin * nums[i])`.
4. **Global Update Gate:** `globalMax = Math.Max(globalMax, currentMax)`.

#### 3.6 Concrete Step-by-Step State Trace
Trace input: `nums = [2, 3, -2, 4, -1]`. Initial: `globalMax = 2, currentMax = 2, currentMin = 2`.

| Step ($i$) | `nums[i]` | Swap? | `currentMax` calculation | `currentMin` calculation | `globalMax` | Notes |
| :---: | :---: | :---: | :---: | :---: | :---: | :--- |
| **Init** | 2 | — | 2 | 2 | 2 | Seeded with `nums[0]` |
| **1** | 3 | No | $\max(3, 2 \times 3) = 6$ | $\min(3, 2 \times 3) = 3$ | 6 | Extended positive chain |
| **2** | -2 | **Yes** (swap 6, 3 $\to$ 3, 6) | $\max(-2, 3 \times (-2)) = -2$ | $\min(-2, 6 \times (-2)) = -12$ | 6 | Sign flip: $-12$ stored in min |
| **3** | 4 | No | $\max(4, -2 \times 4) = 4$ | $\min(4, -12 \times 4) = -48$ | 6 | $-48$ stored in min |
| **4** | -1 | **Yes** (swap 4, -48 $\to$ -48, 4) | $\max(-1, -48 \times (-1)) = 48$ | $\min(-1, 4 \times (-1)) = -4$ | **48** | Double negative hits! $48$ beats 6 |

Final result: `48` (from subarray `[2, 3, -2, 4, -1]`).

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Dual-Track Variables DP):** Industry gold standard. Single pass, $O(1)$ auxiliary space, handles zeros naturally without special-cased branch resets.
- **Approach 2 (Forward and Backward Prefix/Suffix Sweeps):** Insightful algebraic observation: If there are an even number of negative numbers, the product of the whole array (excluding zeros) is positive. If odd, dropping either the first negative or the last negative yields the maximum product. Thus, max product is always the maximum prefix or suffix product.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Setup & Boundaries:** Check if array is null or empty. Initialize `globalMax, currentMax, currentMin = nums[0]`.
- **Step 2: Main Exploration Loop:** Iterate $i$ from $1$ to $N - 1$.
- **Step 3: Condition Gates:** If $nums[i] < 0$, swap `currentMax` and `currentMin`. Compute new `currentMax = Math.Max(num, currentMax * num)` and `currentMin = Math.Min(num, currentMin * num)`.
- **Step 4: Resolution & Return:** Update `globalMax` and return.

#### 4.3 Alternative Approaches Analysis
- **Approach 2: Two-Pass Prefix and Suffix Product:**
  - Initialize `prefix = 0, suffix = 0, maxProd = nums[0]`.
  - For $i = 0 \dots N-1$:
    - `prefix = (prefix == 0 ? 1 : prefix) * nums[i]`
    - `suffix = (suffix == 0 ? 1 : suffix) * nums[N - 1 - i]`
    - `maxProd = Math.Max(maxProd, Math.Max(prefix, suffix))`
  - Automatically resets when encountering a zero barrier.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: Dual-Track Variables DP | Approach 2: Prefix/Suffix Sweep |
| :--- | :--- | :--- |
| **Time Complexity (Best / Avg / Worst)** | $O(N)$ / $O(N)$ / $O(N)$ | $O(N)$ / $O(N)$ / $O(N)$ |
| **Auxiliary Space** | $O(1)$ scalar variables | $O(1)$ scalar variables |
| **Output Space** | $O(1)$ integer product | $O(1)$ integer product |
| **Cache Locality** | Optimal (single forward pass) | Moderate (concurrent forward and backward reads) |
| **In-Place Mutability** | Non-destructive | Non-destructive |
| **Streaming Suitability** | High (pure single pass online) | Low (requires suffix from end of array) |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Problem #85 - Maximum Product Subarray
// Core Pattern: Dual-Track Variables Dynamic Programming / Sign Flip Inversion
// Primary Invariant: When num < 0, swap(currentMax, currentMin) because negation flips extrema.
// Zero Defense: Math.Max(num, currentMax * num) resets subarray if prior product degrades below num.
// Space Defense: O(1) auxiliary registers eliminate O(N) arrays.
// ============================================================================
```

#### Implementation 1: Dual-Track Variables DP (Production Standard)
```csharp
public class Solution
{
    public int MaxProduct(int[] nums)
    {
        // Guard Clause
        if (nums == null || nums.Length == 0)
        {
            return 0;
        }

        // Initialize extrema registers with the first element
        int globalMax = nums[0];
        int currentMax = nums[0];
        int currentMin = nums[0];

        for (int i = 1; i < nums.Length; i++)
        {
            int num = nums[i];

            // Invariant Gate: Multiplying by a negative number inverts order (max becomes min, min becomes max)
            if (num < 0)
            {
                int temp = currentMax;
                currentMax = currentMin;
                currentMin = temp;
            }

            // Invariant: Decide whether to start new subarray at num or extend previous running product
            currentMax = Math.Max(num, currentMax * num);
            currentMin = Math.Min(num, currentMin * num);

            // Update overall global maximum
            if (currentMax > globalMax)
            {
                globalMax = currentMax;
            }
        }

        return globalMax;
    }
}
```

#### Implementation 2: Forward and Backward Prefix/Suffix Sweeps
```csharp
public class SolutionTwoPass
{
    public int MaxProduct(int[] nums)
    {
        if (nums == null || nums.Length == 0)
        {
            return 0;
        }

        int n = nums.Length;
        int maxProduct = nums[0];
        int prefix = 0;
        int suffix = 0;

        for (int i = 0; i < n; i++)
        {
            // Zero barrier reset: Reset running product to 1 if prior value was 0
            prefix = (prefix == 0 ? 1 : prefix) * nums[i];
            suffix = (suffix == 0 ? 1 : suffix) * nums[n - 1 - i];

            int localMax = Math.Max(prefix, suffix);
            if (localMax > maxProduct)
            {
                maxProduct = localMax;
            }
        }

        return maxProduct;
    }
}
```
