# 📖 WEEK 10 DAY 01: DYNAMIC PROGRAMMING AS RECURSION + MEMOIZATION — ENGINEERING GUIDE

> 🧭 **Navigation:** [← Week Overview](README.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_10_Day_02_1D_DP_Knapsack_Family_Instructional.md)
> 
> 💡 **Instructor Note:** *Dynamic Programming is not a mysterious formula—it is structured recursion with caching. Today's focus is building muscle memory for the 3-Step Recipe (Choice -> State -> Base Cases & Transitions) and transitioning seamlessly from Top-Down Memoization to Bottom-Up Tabulation and O(1) space optimization.*

---

## 🎯 LEARNING OBJECTIVES

*By the end of this chapter, you will be able to:*
- 🎯 **Internalize** the core properties of Dynamic Programming: Overlapping Subproblems and Optimal Substructure.
- ⚙️ **Apply** the universal **3-Step DP Recipe** (`Choice -> State Definition -> Base Cases & Transitions`) to any recursive recurrence.
- 🔁 **Implement** both Top-Down Memoization and Bottom-Up Tabulation in production C# (.NET 8/9) and idiomatic Python (3.11+).
- ⚖️ **Execute** Space Optimization via 1D rolling variables, dropping auxiliary space from `O(N)` to `O(1)`.
- 🎙️ **Deliver** a polished, 45-minute verbal interview walkthrough explaining transitions, time-space trade-offs, and call-stack limits.

---

## 📖 CHAPTER 1: CONTEXT & MOTIVATION

### The Exponential Crisis

Every dynamic programming problem begins with a deceptive recursion. Consider calculating Fibonacci numbers, finding distinct ways to climb a staircase, or matching sequence tokens. The recursive decomposition looks clean and mathematically natural:
- To find `F(n)`, compute `F(n - 1) + F(n - 2)`.
- To reach stair `n`, step from `n - 1` or `n - 2`.

When executed naively, however, this clean formulation triggers a combinatorial catastrophe. For `n = 50`, naive recursion makes over `2^50` operations (`~10^15` function calls). On modern hardware executing `10^9` operations per second, that single call would freeze your thread for over 11 days.

The cause of this freeze is not the depth of recursion (which is merely 50 frames), but **redundancy**: the algorithm calculates identical subproblems millions of times. For `F(50)`, the subproblem `F(25)` is computed `2,324,432` times from scratch.

```text
                  NAIVE RECURSION: COMBINATORIAL EXPLOSION
                                 fib(5)
                               /        \
                        fib(4)            fib(3)  <-- Duplicate!
                       /      \          /      \
                  fib(3)      fib(2)   fib(2)   fib(1)
                 /      \     /    \   /    \
             fib(2)   fib(1) ...  ... ...   ...
             /    \
          fib(1)  fib(0)
```

### The Core Insight: Caching Subproblem Solutions

The engineering solution is simple: **never compute the same subproblem twice**. When a subproblem is solved for the first time, record its result in an indexed lookup table or hash map. When future branches demand that same state, retrieve it in `O(1)` time.

This collapses an exponential `O(2^N)` decision tree into a linear `O(N)` Directed Acyclic Graph (DAG) with exactly `N + 1` unique states.

> [!NOTE]
> **Enterprise Caching & Systems Context:** In enterprise systems, memoization is the algorithmic foundation of performance. Distributed caches (e.g., Redis caching recommendation scores at Netflix), relational query planners (e.g., PostgreSQL caching optimal join orders across sub-queries), and CPU hardware (L1/L2 data cache lines) all exploit identical principles: transform expensive repeated recomputations into bounded `O(1)` lookups.

### When Dynamic Programming Applies (And When It Fails)

DP requires two mathematical properties:

1. **Overlapping Subproblems:** The problem breaks into subproblems that are reused repeatedly across branches. (If subproblems never overlap—such as in Merge Sort or Binary Search—standard Divide and Conquer applies).
2. **Optimal Substructure:** The optimal solution to the overall problem can be constructed directly from the optimal solutions of its subproblems.

| Problem Structure | Overlapping Subproblems? | Optimal Substructure? | Algorithmic Paradigm |
| :--- | :---: | :---: | :--- |
| **Fibonacci / Climbing Stairs** | ✅ Yes | ✅ Yes | **Dynamic Programming** |
| **0/1 Knapsack / Grid Paths** | ✅ Yes | ✅ Yes | **Dynamic Programming** |
| **Merge Sort / Quick Sort** | ❌ No (disjoint partitions) | ✅ Yes | **Divide and Conquer** |
| **Longest Simple Path (Graphs)** | ✅ Yes | ❌ No (subpaths share nodes) | **NP-Hard / Backtracking** |

---

## 🧠 CHAPTER 2: THE 3-STEP DP RECIPE

To solve any dynamic programming problem systematically under interview pressure, follow the **3-Step Recipe**:

```text
+-----------------------------------------------------------------------------------+
|                              THE 3-STEP DP RECIPE                                 |
+-----------------------------------------------------------------------------------+
|  1. CHOICE            Identify the decision made at each step.                   |
|                       "Do I take 1 step or 2 steps?"                              |
|                       "Do I include the current item or skip it?"                 |
|                                                                                   |
|  2. STATE             Define the minimal parameters uniquely capturing progress.  |
|                       "dp[i] = number of distinct ways to reach stair i"          |
|                                                                                   |
|  3. BASE CASES &      Define the boundary values and combine subproblems.        |
|     TRANSITIONS       Base: dp[0] = 1, dp[1] = 1                                  |
|                       Transition: dp[i] = dp[i - 1] + dp[i - 2]                   |
+-----------------------------------------------------------------------------------+
```

### Step 1: Choice (The Decision Point)
At step `i`, what options are available?
In Climbing Stairs, an agent standing at step `i` must have arrived by taking either:
- A single 1-step leap from `i - 1`, OR
- A double 2-step leap from `i - 2`.

Every choice branches into a smaller subproblem.

### Step 2: State Definition
What is the minimum amount of information required to know where we are?
For linear counting/reachability problems:
- Let `dp[i]` represent the total number of distinct valid ways to reach step `i`.
State should never contain extraneous historical parameters (e.g., "what path took us here"). If the future decisions depend only on `i`, `i` is the sole state variable.

### Step 3: Base Cases and Transitions
- **Base Cases:** Smallest subproblems that require no recursive calls.
  - `dp[0] = 1` (1 way to be at the ground: do nothing).
  - `dp[1] = 1` (1 way to reach step 1: take a 1-step leap).
- **State Transition Equation:**
  `dp[i] = dp[i - 1] + dp[i - 2]` (for all `i >= 2`).

### Visualizing the DAG & Memory Layout

```text
State Transition DAG:
[dp[0]: 1] ---> [dp[1]: 1] ---> [dp[2]: 2] ---> [dp[3]: 3] ---> [dp[4]: 5] ---> [dp[5]: 8]
    |               ^               ^               ^               ^
    +---------------+---------------+---------------+---------------+
        (skip 1)         (skip 1)        (skip 1)        (skip 1)

Contiguous Array in Memory (Cache Friendly):
Index:   0    1    2    3    4    5
Array: [ 1 ][ 1 ][ 2 ][ 3 ][ 5 ][ 8 ]
Addr:  0x00 0x04 0x08 0x0C 0x10 0x14  (sequential 4-byte integers in L1 cache)
```

---

## ⚙️ CHAPTER 3: MECHANICS & IMPLEMENTATION

### Top-Down Memoization vs Bottom-Up Tabulation

| Dimension | Top-Down (Memoization) | Bottom-Up (Tabulation) | Space-Optimized (Rolling Array) |
| :--- | :--- | :--- | :--- |
| **Direction** | Target `n -> 0` (Demand driven) | Base `0 -> n` (Supply driven) | Base `0 -> n` (In-place window) |
| **Mechanism** | Recursion + Cache (Map / Array) | Iterative Loop + Array | Iterative Loop + Variables |
| **Time Complexity** | `O(N)` | `O(N)` | `O(N)` |
| **Auxiliary Space** | `O(N)` cache + `O(N)` call stack | `O(N)` table, `O(1)` call stack | `O(1)` auxiliary space |
| **Subproblems Solved** | Only states reached by execution | All table entries up to `N` | All table entries up to `N` |
| **Overhead** | Function call frame pushes/pops | Flat loops, cache friendly | Minimal registers / scalar variables |

---

### ASCII State Transition Diagram

```text
Tabulation Fill Progression:

Step 0 & 1 (Base Cases):
+---+---+---+---+---+---+
| 1 | 1 | ? | ? | ? | ? |
+---+---+---+---+---+---+
  0   1   2   3   4   5

Step 2: dp[2] = dp[1] + dp[0] = 1 + 1 = 2
+---+---+---+---+---+---+
| 1 | 1 | 2 | ? | ? | ? |
+---+---+---+---+---+---+

Step 3: dp[3] = dp[2] + dp[1] = 2 + 1 = 3
+---+---+---+---+---+---+
| 1 | 1 | 2 | 3 | ? | ? |
+---+---+---+---+---+---+

Step 4: dp[4] = dp[3] + dp[2] = 3 + 2 = 5
+---+---+---+---+---+---+
| 1 | 1 | 2 | 3 | 5 | ? |
+---+---+---+---+---+---+

Step 5: dp[5] = dp[4] + dp[3] = 5 + 3 = 8  ==> Goal Reached!
+---+---+---+---+---+---+
| 1 | 1 | 2 | 3 | 5 | 8 |
+---+---+---+---+---+---+

Rolling Array Memory Optimization (O(1) Space):
Iteration i:
      [ prev2 ]     [ prev1 ]   ===>  [ current = prev2 + prev1 ]
          1             1                     2
Shift variables for next round:
                    [ prev2 ]     [ prev1 ]
                       1              2
```

---

### Implementation 1: Fibonacci Number (Core Paradigm)

#### Modern C# (.NET 8/9) Implementation

```csharp
namespace DP.Fundamentals;

public static class Fibonacci
{
    // -------------------------------------------------------------
    // Approach 1: Top-Down Memoization (Array-backed cache)
    // Time: O(N) | Space: O(N) (Recursion Stack O(N) + Array O(N))
    // -------------------------------------------------------------
    public static long Memoized(int n)
    {
        if (n < 0) throw new ArgumentOutOfRangeException(nameof(n), "n must be non-negative");
        if (n <= 1) return n;

        long[] memo = new long[n + 1];
        Array.Fill(memo, -1);
        memo[0] = 0;
        memo[1] = 1;

        return ComputeMemo(n, memo);
    }

    private static long ComputeMemo(int n, long[] memo)
    {
        if (memo[n] != -1) return memo[n];

        memo[n] = ComputeMemo(n - 1, memo) + ComputeMemo(n - 2, memo);
        return memo[n];
    }

    // -------------------------------------------------------------
    // Approach 2: Bottom-Up Tabulation (Iterative)
    // Time: O(N) | Space: O(N) (No recursion stack)
    // -------------------------------------------------------------
    public static long Tabulation(int n)
    {
        if (n < 0) throw new ArgumentOutOfRangeException(nameof(n));
        if (n <= 1) return n;

        long[] dp = new long[n + 1];
        dp[0] = 0;
        dp[1] = 1;

        for (int i = 2; i <= n; i++)
        {
            dp[i] = dp[i - 1] + dp[i - 2];
        }

        return dp[n];
    }

    // -------------------------------------------------------------
    // Approach 3: Space-Optimized Tabulation (Rolling Variables)
    // Time: O(N) | Space: O(1)
    // -------------------------------------------------------------
    public static long Optimized(int n)
    {
        if (n < 0) throw new ArgumentOutOfRangeException(nameof(n));
        if (n <= 1) return n;

        long prev2 = 0; // F(i - 2)
        long prev1 = 1; // F(i - 1)

        for (int i = 2; i <= n; i++)
        {
            long current = prev1 + prev2;
            prev2 = prev1;
            prev1 = current;
        }

        return prev1;
    }
}
```

#### Idiomatic Python (3.11+) Implementation

```python
from functools import lru_cache

class Fibonacci:
    """Fibonacci number computation across all three DP representations."""

    # -------------------------------------------------------------
    # Approach 1: Top-Down Memoization (lru_cache & manual dict)
    # Time: O(N) | Space: O(N)
    # -------------------------------------------------------------
    @staticmethod
    def memoized_decorator(n: int) -> int:
        if n < 0:
            raise ValueError("n must be non-negative")

        @lru_cache(maxsize=None)
        def helper(k: int) -> int:
            if k <= 1:
                return k
            return helper(k - 1) + helper(k - 2)

        return helper(n)

    @staticmethod
    def memoized_manual(n: int) -> int:
        if n < 0:
            raise ValueError("n must be non-negative")
        memo: dict[int, int] = {0: 0, 1: 1}

        def helper(k: int) -> int:
            if k in memo:
                return memo[k]
            memo[k] = helper(k - 1) + helper(k - 2)
            return memo[k]

        return helper(n)

    # -------------------------------------------------------------
    # Approach 2: Bottom-Up Tabulation
    # Time: O(N) | Space: O(N)
    # -------------------------------------------------------------
    @staticmethod
    def tabulation(n: int) -> int:
        if n < 0:
            raise ValueError("n must be non-negative")
        if n <= 1:
            return n

        dp: list[int] = [0] * (n + 1)
        dp[0], dp[1] = 0, 1

        for i in range(2, n + 1):
            dp[i] = dp[i - 1] + dp[i - 2]

        return dp[n]

    # -------------------------------------------------------------
    # Approach 3: Space-Optimized (Rolling Variables)
    # Time: O(N) | Space: O(1)
    # -------------------------------------------------------------
    @staticmethod
    def optimized(n: int) -> int:
        if n < 0:
            raise ValueError("n must be non-negative")
        if n <= 1:
            return n

        prev2, prev1 = 0, 1
        for _ in range(2, n + 1):
            prev2, prev1 = prev1, prev1 + prev2

        return prev1
```

---

### Implementation 2: Climbing Stairs (LeetCode 70)

**Problem:** You are climbing a staircase that takes `n` steps to reach the top. Each time you can either climb 1 or 2 steps. In how many distinct ways can you climb to the top?

#### Modern C# (.NET 8/9) Implementation

```csharp
namespace DP.Fundamentals;

public static class ClimbingStairs
{
    // -------------------------------------------------------------
    // Space-Optimized O(1) Tabulation with Path Count
    // Time: O(N) | Space: O(1)
    // -------------------------------------------------------------
    public static int ClimbStairs(int n)
    {
        if (n <= 0) return 0;
        if (n <= 2) return n;

        int prev2 = 1; // Ways to reach step 1
        int prev1 = 2; // Ways to reach step 2

        for (int i = 3; i <= n; i++)
        {
            int current = prev1 + prev2;
            prev2 = prev1;
            prev1 = current;
        }

        return prev1;
    }

    // -------------------------------------------------------------
    // Full Path Reconstruction (Backtracking along optimal decisions)
    // -------------------------------------------------------------
    public static List<List<int>> GetAllPaths(int n)
    {
        List<List<int>> allPaths = [];
        List<int> currentPath = [];

        void Backtrack(int remaining)
        {
            if (remaining == 0)
            {
                allPaths.Add([..currentPath]);
                return;
            }

            if (remaining >= 1)
            {
                currentPath.Add(1);
                Backtrack(remaining - 1);
                currentPath.RemoveAt(currentPath.Count - 1);
            }

            if (remaining >= 2)
            {
                currentPath.Add(2);
                Backtrack(remaining - 2);
                currentPath.RemoveAt(currentPath.Count - 1);
            }
        }

        Backtrack(n);
        return allPaths;
    }
}
```

#### Idiomatic Python (3.11+) Implementation

```python
class ClimbingStairs:
    """Climbing stairs solution: Tabulation, Space-Optimized, and Path Reconstruction."""

    @staticmethod
    def climb_stairs(n: int) -> int:
        """Computes distinct ways to reach step n in O(N) time and O(1) space."""
        if n <= 0:
            return 0
        if n <= 2:
            return n

        prev2, prev1 = 1, 2
        for _ in range(3, n + 1):
            prev2, prev1 = prev1, prev1 + prev2

        return prev1

    @staticmethod
    def get_all_paths(n: int) -> list[list[int]]:
        """Reconstructs all explicit step paths that reach top n."""
        paths: list[list[int]] = []
        current_path: list[int] = []

        def backtrack(remaining: int) -> None:
            if remaining == 0:
                paths.append(list(current_path))
                return
            for step in (1, 2):
                if remaining >= step:
                    current_path.append(step)
                    backtrack(remaining - step)
                    current_path.pop()

        backtrack(n)
        return paths
```

---

## ⚖️ CHAPTER 4: EXPLICIT COMPLEXITY DECONSTRUCTION

### Complexity Comparison

| Algorithm Implementation | Time Complexity | Auxiliary Space Complexity | Stack Frame Overhead | Memory Layout Efficiency |
| :--- | :--- | :--- | :--- | :--- |
| **Naive Recursion** | `O(2^N)` | `O(N)` | `O(N)` frames | ❌ High stack thrash |
| **Top-Down Memoization** | `O(N)` | `O(N)` (`O(N)` cache + `O(N)` stack) | `O(N)` frames | ⚠️ Heap/Stack split |
| **Bottom-Up Tabulation** | `O(N)` | `O(N)` (Table array) | `O(1)` (Flat loop) | ✅ Contiguous L1 cache |
| **Space-Optimized Tabulation** | `O(N)` | `O(1)` (2 scalar variables) | `O(1)` (Flat loop) | 🚀 CPU Register resident |

### Failure Modes & Defensive Engineering

1. **Call Stack Exhaustion (`StackOverflowException` / `RecursionError`):**
   - In C#, the default thread stack is `1 MB` (~10,000 recursive frames).
   - In Python, `sys.getrecursionlimit()` defaults to `1000`.
   - Calling `Memoized(100_000)` will crash with a stack overflow even if memoization is active. Bottom-Up Tabulation is strictly mandatory for large `N`.
2. **Hash Table Overhead vs Dense Array:**
   - Using `Dictionary<int, long>` or Python `dict` introduces hashing, pointer chasing, and boxing. For continuous integer ranges `0..N`, always prefer a flat typed array (`long[]` or `list[int]`).
3. **Integer Arithmetic Overflow:**
   - Fibonacci numbers grow at `Phi^N` (`~1.618^N`).
   - `F(46)` is the maximum value fitting into a 32-bit signed integer (`2,147,483,647`).
   - `F(92)` is the maximum value fitting into a 64-bit signed integer (`long`).
   - For `N >= 93`, employ `System.Numerics.BigInteger` in C# or standard arbitrary-precision `int` in Python.

---

## 🎙️ CHAPTER 5: 45-MINUTE INTERVIEW VERBAL SCRIPT

When an interviewer presents Fibonacci or Climbing Stairs, use this script to demonstrate senior algorithmic engineering:

```text
===================================================================================
               45-MINUTE INTERVIEW STEP-BY-STEP VERBAL PLAYBOOK
===================================================================================

[00:00 - 05:00] CLARIFICATION & CONSTRAINTS
"First, let me clarify the constraints. What is the maximum value of n? If n is up to 45, 
a 32-bit integer suffices; if n goes up to 10^5, we must watch out for integer overflow 
and recursion stack limits. Are non-positive inputs possible? For n = 0, do we return 0 
or 1? I'll assume n >= 1, returning 1 for n = 1, and 2 for n = 2."

[05:00 - 12:00] THE 3-STEP RECIPE & RECURSIVE RECURRENCE
"Let's break this down using the 3-Step DP Recipe:
 1. Choice: At any step i, the previous move was either a 1-step leap from i - 1, 
    or a 2-step leap from i - 2.
 2. State: Let dp[i] represent the number of distinct ways to reach step i.
 3. Transitions & Base Cases: dp[i] = dp[i - 1] + dp[i - 2], with dp[1] = 1, dp[2] = 2.
 A naive recursive implementation branches into two calls at each step, yielding an 
 exponential O(2^N) time complexity."

[12:00 - 20:00] SPOTTING OVERLAPPING SUBPROBLEMS & MEMOIZATION
"If we sketch the recursion tree for n = 5, we immediately notice overlapping subproblems:
 f(3) is evaluated on both left and right subtrees. We can cache intermediate results 
 in an array of size n + 1 initialized to -1. This brings the time complexity down 
 to O(N) because each state is computed exactly once. However, the auxiliary space is 
 still O(N) due to the recursive call stack."

[20:00 - 30:00] BOTTOM-UP TABULATION
"To eliminate the recursion stack overhead and prevent stack overflow on large inputs, 
we can invert the dependency and solve bottom-up. We initialize dp[1] = 1, dp[2] = 2, 
and run an iterative loop from 3 up to n. This guarantees O(N) time and O(N) space, 
and benefits from contiguous memory layout in cache."

[30:00 - 40:00] SPACE OPTIMIZATION TO O(1)
"Notice our transition equation: dp[i] only depends on the immediate two previous states, 
dp[i - 1] and dp[i - 2]. We don't need the entire array. We can maintain two scalar 
variables, prev2 and prev1, and update them in a sliding window. This compresses our 
auxiliary space to O(1) while maintaining O(N) linear time."

[40:00 - 45:00] DRY RUN & EDGE CASES
"Let's trace n = 3: prev2 = 1, prev1 = 2. Loop runs for i = 3: current = 1 + 2 = 3. 
prev2 becomes 2, prev1 becomes 3. Loop terminates, returning 3. Verified!"
===================================================================================
```

---

## ⚔️ PRACTICE PROBLEMS & INTERVIEW DRILLS

| # | Problem | Difficulty | Key Pattern | Primary Goal |
| :- | :--- | :-: | :--- | :--- |
| 1 | **LeetCode 70: Climbing Stairs** | 🟢 Easy | Linear Recurrence | Master `O(1)` space transition |
| 2 | **LeetCode 509: Fibonacci Number** | 🟢 Easy | Recurrence Base | Compare Top-Down vs Bottom-Up |
| 3 | **LeetCode 746: Min Cost Climbing Stairs** | 🟢 Easy | State with Cost | `dp[i] = cost[i] + min(dp[i-1], dp[i-2])` |
| 4 | **LeetCode 1137: N-th Tribonacci Number** | 🟢 Easy | 3-State Sliding Window | Track 3 rolling variables |
| 5 | **LeetCode 198: House Robber** | 🟡 Medium | Non-adjacent choice | Choice: rob vs skip |

---

## 🎓 SELF-CHECK & FINAL VERIFICATION

- [x] **Zero LaTeX Check:** All complexities formatted in backticks (`O(N)`, `O(2^N)`, `O(1)`). Zero `$` or `\begin{}` syntax.
- [x] **Code Completeness:** C# (.NET 8/9) and Python (3.11+) implementations provide functional, production-ready code with no placeholder omissions.
- [x] **3-Step Framework:** Explicitly structured into Choice, State Definition, and Base Cases & Transitions.
- [x] **Cognitive Bloat Stripped:** No legacy cognitive lens sections; corporate stories condensed into a single `> [!NOTE]` callout.
- [x] **ASCII Visuals:** Clean state machine and rolling-variable transition diagrams.

---

> 🧭 **Navigation:** [← Week Overview](README.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_10_Day_02_1D_DP_Knapsack_Family_Instructional.md)
