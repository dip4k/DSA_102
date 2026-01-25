# 📘 WEEK_10_FULL_PLAYBOOK.md — Dynamic Programming I: Fundamentals

**Course:** DSA Mastery — Algorithm Engineering  
**Week:** 10 (Fundamentals)  
**Duration:** 5 Days (Core + Optional Advanced)  
**Difficulty:** 🟢 Green → 🟡 Yellow → 🔴 Red  
**Prerequisites:** Week 09 (Recursion, Memoization Basics)  

---

# 🎯 WEEK 10 OVERVIEW

## Primary Goal

**Build DP intuition from recursion + memoization to table-based solutions.**

Transform recursive problems with overlapping subproblems into efficient polynomial-time solutions using dynamic programming.

## Why This Week Comes Here

DP is fundamental to optimizing recursive solutions. This is your first deep dive into one of computer science's most powerful paradigms. You'll learn to recognize problems that seem computationally intractable and solve them elegantly.

## MIT Alignment

Aligned with MIT 6.006 (Introduction to Algorithms):
- DP basics and recurrence relations
- Top-down memoization vs bottom-up tabulation
- Examples: Fibonacci, grids, sequences, games
- Optimization techniques (space reduction, binary search)

## Learning Arc

```
Day 01: Foundations → Overlapping subproblems, exponential to polynomial
     ↓
Day 02: 1D Patterns → Stairs, robber, coins, knapsack
     ↓
Day 03: 2D Patterns → Grids, edit distance, string alignment
     ↓
Day 04: Sequences → LCS, LIS, optimization insights
     ↓
Day 05: Advanced Design → State design, real systems, mastery
```

---

# 📅 DAY 1: DP AS RECURSION + MEMOIZATION

**Topic:** 🧠 Overlapping Subproblems, 🔁 Top-Down, ↕ Bottom-Up  
**Difficulty:** 🟢 Easy-Intermediate  
**Time Allocation:** 90 minutes core + 45 minutes practice  

## 1.1 THE EXPONENTIAL PROBLEM: Fibonacci

### The Problem
```
fib(n) = fib(n-1) + fib(n-2)  [standard mathematical definition]
fib(0) = 0, fib(1) = 1

Question: Compute fib(5)?
```

### Naive Recursion (Exponential)
```
fib(5)
  = fib(4) + fib(3)
  = [fib(3) + fib(2)] + [fib(2) + fib(1)]
  = [[fib(2) + fib(1)] + [fib(1) + fib(0)]] + [[fib(1) + fib(0)] + fib(1)]
  = ...

Observation: fib(2) is computed multiple times!
Observation: fib(3) is computed multiple times!

Tree depth: O(n)
Branching factor: 2
Total nodes: O(2^n) → EXPONENTIAL!
```

### The Insight: Overlapping Subproblems
```
         fib(5)
        /      \
     fib(4)    fib(3) ← computed again!
     /   \      /   \
  fib(3) fib(2) fib(2) fib(1)
  /  \   /  \   /  \
fib(2)fib(1) ... (repeated)

Pattern: Small subproblems are solved repeatedly.
Solution: Solve each unique subproblem ONCE and reuse the answer.
```

### Complexity Analysis
```
Naive Recursion: O(2^n) time, O(n) space (call stack)
With Memoization: O(n) time, O(n) space (memo table)
```

## 1.2 SOLUTION 1: TOP-DOWN APPROACH (MEMOIZATION)

### The Idea
```
1. Use recursion (natural for problem thinking)
2. Before computing, check memo table
3. If result exists, return it (O(1))
4. Otherwise, compute, store in memo, return
```

### Implementation

```csharp
/// <summary>
/// Fibonacci with Top-Down Memoization
/// Time: O(n) | Space: O(n)
/// 
/// 🧠 MENTAL MODEL:
/// Before computing fib(n), check "Have I solved this before?"
/// If yes, return cached result (O(1))
/// If no, compute it recursively, cache it, return
/// </summary>
public int FibonacciMemoization(int n, Dictionary<int, int> memo = null) {
    // Initialize memo table on first call
    if (memo == null) memo = new Dictionary<int, int>();
    
    // Base cases
    if (n == 0) return 0;
    if (n == 1) return 1;
    
    // Check if already computed
    if (memo.ContainsKey(n)) {
        return memo[n];  // Cache hit: return immediately
    }
    
    // Compute if not in memo
    int result = FibonacciMemoization(n - 1, memo) + FibonacciMemoization(n - 2, memo);
    
    // Store in memo before returning
    memo[n] = result;
    return result;
}

// Usage:
// int fib5 = FibonacciMemoization(5);  // Returns 5
```

### Execution Trace: FibonacciMemoization(5)

```
Call Stack & Memo Table Evolution:

fib(5)
  fib(4) [not in memo, compute]
    fib(3) [not in memo, compute]
      fib(2) [not in memo, compute]
        fib(1) → return 1
        fib(0) → return 0
      fib(2) = 1, store in memo
      fib(1) → return 1 (base case)
    fib(3) = 2, store in memo
    fib(2) → return 1 (MEMO HIT! no recursion)
  fib(4) = 3, store in memo
  fib(3) → return 2 (MEMO HIT!)
fib(5) = 5, return

Memo table at end: {0:0, 1:1, 2:1, 3:2, 4:3, 5:5}

Function calls: ~10 (not 31!)
```

## 1.3 SOLUTION 2: BOTTOM-UP APPROACH (TABULATION)

### The Idea
```
1. Don't use recursion; use iteration
2. Build table from smallest subproblems up to the main problem
3. dp[i] = result for problem of size i
4. Fill table left-to-right: dp[i] depends on dp[i-1], dp[i-2], etc.
```

### Implementation

```csharp
/// <summary>
/// Fibonacci with Bottom-Up Tabulation
/// Time: O(n) | Space: O(n)
/// 
/// 🧠 MENTAL MODEL:
/// Build an array where dp[i] = fib(i)
/// Start from base cases (dp[0], dp[1])
/// Fill remaining cells using recurrence: dp[i] = dp[i-1] + dp[i-2]
/// Return dp[n]
/// </summary>
public int FibonacciTabulation(int n) {
    // Guard: handle edge cases
    if (n == 0) return 0;
    if (n == 1) return 1;
    
    // dp[i] = Fibonacci number at position i
    int[] dp = new int[n + 1];
    
    // Base cases
    dp[0] = 0;
    dp[1] = 1;
    
    // Fill remaining cells bottom-up
    for (int i = 2; i <= n; i++) {
        dp[i] = dp[i - 1] + dp[i - 2];
    }
    
    return dp[n];
}

// Usage:
// int fib5 = FibonacciTabulation(5);  // Returns 5
```

### Tabulation Trace: FibonacciTabulation(5)

```
Building dp array step-by-step:

n = 5

Step 1: Initialize
dp = [?, ?, ?, ?, ?, ?]  (indices 0-5)

Step 2: Base cases
dp = [0, 1, ?, ?, ?, ?]
      ↑  ↑

Step 3: Fill i=2
dp[2] = dp[1] + dp[0] = 1 + 0 = 1
dp = [0, 1, 1, ?, ?, ?]

Step 4: Fill i=3
dp[3] = dp[2] + dp[1] = 1 + 1 = 2
dp = [0, 1, 1, 2, ?, ?]

Step 5: Fill i=4
dp[4] = dp[3] + dp[2] = 2 + 1 = 3
dp = [0, 1, 1, 2, 3, ?]

Step 6: Fill i=5
dp[5] = dp[4] + dp[3] = 3 + 2 = 5
dp = [0, 1, 1, 2, 3, 5]

Return: dp[5] = 5
```

## 1.4 SPACE OPTIMIZATION: O(1) Space

### Insight
```
To compute dp[i], you only need dp[i-1] and dp[i-2]
You don't need the entire array
→ Keep only last two values
→ Space: O(n) → O(1)
```

### Implementation

```csharp
/// <summary>
/// Fibonacci with Space Optimization
/// Time: O(n) | Space: O(1)
/// </summary>
public int FibonacciOptimized(int n) {
    if (n == 0) return 0;
    if (n == 1) return 1;
    
    int prev2 = 0;  // dp[i-2]
    int prev1 = 1;  // dp[i-1]
    
    for (int i = 2; i <= n; i++) {
        int current = prev1 + prev2;  // dp[i]
        prev2 = prev1;                 // shift: dp[i-2] = dp[i-1]
        prev1 = current;               // shift: dp[i-1] = dp[i]
    }
    
    return prev1;
}
```

## 1.5 COMPARISON & KEY INSIGHTS

| Approach | Time | Space | Pros | Cons |
|----------|------|-------|------|------|
| Naive Recursion | O(2^n) | O(n) | Simple code | Exponential! Unusable |
| Memoization (Top-Down) | O(n) | O(n) | Intuitive, recursive | Function call overhead |
| Tabulation (Bottom-Up) | O(n) | O(n) | Fast, iterative | Must define all states |
| Optimized | O(n) | O(1) | Minimal space | Less generalizable |

## 1.6 THE DP PRINCIPLE

### Bellman's Principle of Optimality
```
"An optimal solution to a problem contains optimal solutions to subproblems."

For DP to apply:
1. Problem must have optimal substructure
   (optimal solution = optimal choices on subproblems)

2. Subproblems must overlap
   (same subproblem appears multiple times)

If both hold → DP converts exponential to polynomial
```

## 1.7 CLIMBING STAIRS VARIANT: The Gateway Problem

### Problem
```
You're at step 0. You want to reach step n.
At each step, you can climb 1 or 2 steps.
How many distinct ways to reach step n?

Example: n=3
  Path 1: 1+1+1
  Path 2: 1+2
  Path 3: 2+1
  Answer: 3 ways
```

### DP Solution

```csharp
/// <summary>
/// Climbing Stairs - Count distinct ways to reach step n
/// Time: O(n) | Space: O(n) → O(1) optimized
/// </summary>
public int ClimbingStairs(int n) {
    if (n == 1) return 1;
    if (n == 2) return 2;
    
    int[] dp = new int[n + 1];
    dp[1] = 1;  // One way to reach step 1
    dp[2] = 2;  // Two ways to reach step 2
    
    // dp[i] = ways to reach step i = dp[i-1] + dp[i-2]
    // (either come from step i-1 and climb 1, or from i-2 and climb 2)
    for (int i = 3; i <= n; i++) {
        dp[i] = dp[i - 1] + dp[i - 2];
    }
    
    return dp[n];
}
```

---

# 📅 DAY 2: 1D DP & KNAPSACK FAMILY

**Topics:** 🪜 Climbing Stairs Variants, 🏠 House Robber, 💰 Coin Change, 🎒 0/1 Knapsack  
**Difficulty:** 🟡 Intermediate  
**Time Allocation:** 120 minutes core + 60 minutes practice  

## 2.1 CLIMBING STAIRS WITH COSTS

### Problem
```
Each step i has a cost[i].
Starting from step 0 or 1, reach step n with minimum total cost.
At each step, you can climb 1 or 2 steps.

Example:
  cost = [1, 100, 1, 1, 1, 100, 1, 1, 100, 1]
  Minimum cost to reach step 9?
  
  Optimal: 0 → 1 → 3 → 4 → 5 → 7 → 8 → 9
  Cost: 1 + 1 + 1 + 1 + 1 + 1 = 6
```

### DP Solution

```csharp
/// <summary>
/// Min Cost Climbing Stairs
/// Time: O(n) | Space: O(n) → O(1)
/// </summary>
public int MinCostClimbingStairs(int[] cost) {
    int n = cost.Length;
    if (n == 0) return 0;
    if (n == 1) return cost[0];
    
    // dp[i] = min cost to reach step i
    int[] dp = new int[n];
    dp[0] = cost[0];  // Cost to reach step 0
    dp[1] = cost[1];  // Cost to reach step 1
    
    for (int i = 2; i < n; i++) {
        // To reach step i, either:
        // - Come from step i-1 and climb 1 (cost = dp[i-1] + cost[i])
        // - Come from step i-2 and climb 2 (cost = dp[i-2] + cost[i])
        dp[i] = Math.Min(dp[i - 1], dp[i - 2]) + cost[i];
    }
    
    return Math.Min(dp[n - 1], dp[n - 2]);  // Can reach top from last or second-last
}
```

## 2.2 HOUSE ROBBER: NON-ADJACENT SELECTION

### Problem
```
Houses in a row. House i has value[i] money.
You can rob any house, but NOT two adjacent houses.
Maximize total money robbed.

Example:
  value = [1, 2, 3, 1]
  Option 1: Rob houses 0 and 2 → 1 + 3 = 4 ✓ (optimal)
  Option 2: Rob houses 1 and 3 → 2 + 1 = 3
  Option 3: Rob house 1 → 2
  Answer: 4
```

### DP Solution

```csharp
/// <summary>
/// House Robber - Max sum with non-adjacent constraint
/// Time: O(n) | Space: O(n)
/// </summary>
public int Rob(int[] nums) {
    if (nums == null || nums.Length == 0) return 0;
    if (nums.Length == 1) return nums[0];
    if (nums.Length == 2) return Math.Max(nums[0], nums[1]);
    
    // dp[i] = max money robbing houses 0..i (with non-adjacent constraint)
    int[] dp = new int[nums.Length];
    dp[0] = nums[0];
    dp[1] = Math.Max(nums[0], nums[1]);
    
    for (int i = 2; i < nums.Length; i++) {
        // Either:
        // - Rob house i: dp[i-2] + nums[i] (can't rob i-1)
        // - Skip house i: dp[i-1] (already have best up to i-1)
        dp[i] = Math.Max(
            dp[i - 2] + nums[i],  // Rob house i
            dp[i - 1]              // Skip house i
        );
    }
    
    return dp[nums.Length - 1];
}
```

### House Robber Trace: [1, 2, 3, 1]

```
dp[0] = 1 (rob house 0)
dp[1] = max(1, 2) = 2 (rob house 1 only)
dp[2] = max(dp[0] + 3, dp[1]) = max(1 + 3, 2) = 4 (rob houses 0,2)
dp[3] = max(dp[1] + 1, dp[2]) = max(2 + 1, 4) = 4 (keep robbing 0,2)

Answer: 4
```

## 2.3 COIN CHANGE: TWO VARIANTS

### Variant A: Minimum Coins

**Problem:** Given coin denominations and target amount, find minimum coins needed.

```
coins = [1, 2, 5], amount = 5
Answer: 1 coin (one 5-coin) ✓

coins = [2], amount = 3
Answer: Impossible (can't make 3 with only 2s)
```

### Solution

```csharp
/// <summary>
/// Coin Change - Minimum coins for target amount
/// Time: O(n × amount) | Space: O(amount)
/// </summary>
public int CoinChange(int[] coins, int amount) {
    // dp[i] = min coins needed to make amount i
    int[] dp = new int[amount + 1];
    
    // Initialize: amounts 0..amount need "infinity" coins (impossible)
    for (int i = 1; i <= amount; i++) {
        dp[i] = amount + 1;  // Use amount+1 as "infinity"
    }
    dp[0] = 0;  // 0 coins needed to make amount 0
    
    // For each amount
    for (int i = 1; i <= amount; i++) {
        // Try each coin
        foreach (int coin in coins) {
            if (coin <= i) {
                // If we use this coin, we need 1 + dp[i - coin]
                dp[i] = Math.Min(dp[i], 1 + dp[i - coin]);
            }
        }
    }
    
    return dp[amount] == amount + 1 ? -1 : dp[amount];
}
```

### Variant B: Number of Ways

**Problem:** Count the number of ways to make the amount.

```
coins = [1, 2, 5], amount = 5
Ways:
  1+1+1+1+1
  1+1+1+2
  1+2+2
  5

Answer: 4 ways
```

### Solution

```csharp
/// <summary>
/// Coin Change II - Number of ways to make amount
/// Time: O(n × amount) | Space: O(amount)
/// </summary>
public int Change(int amount, int[] coins) {
    // dp[i] = number of ways to make amount i
    int[] dp = new int[amount + 1];
    dp[0] = 1;  // 1 way to make 0 (use no coins)
    
    // For each coin (order matters for counting combinations, not permutations)
    foreach (int coin in coins) {
        for (int i = coin; i <= amount; i++) {
            dp[i] += dp[i - coin];
        }
    }
    
    return dp[amount];
}
```

## 2.4 0/1 KNAPSACK: THE CLASSIC

### Problem
```
You have a knapsack of capacity W (weight limit).
You have n items, each with weight[i] and value[i].
You can take each item 0 or 1 times (not duplicates).
Maximize total value without exceeding capacity.

Example:
  Capacity = 4
  Items: (weight, value) = [(2,3), (3,4), (4,5)]
  
  Option 1: Take item 0 and 1 → weight 5 > 4, invalid
  Option 2: Take item 0 only → weight 2, value 3
  Option 3: Take item 1 only → weight 3, value 4
  Option 4: Take item 2 only → weight 4, value 5 ✓ (optimal)
  
  Answer: 5
```

### DP Solution

```csharp
/// <summary>
/// 0/1 Knapsack - Maximize value with weight constraint
/// Time: O(n × W) | Space: O(n × W) → O(W) optimized
/// </summary>
public int Knapsack01(int[] weights, int[] values, int capacity) {
    int n = weights.Length;
    
    // dp[i][w] = max value using items 0..i-1 with capacity w
    int[][] dp = new int[n + 1][];
    for (int i = 0; i <= n; i++) {
        dp[i] = new int[capacity + 1];
    }
    
    // Base case: dp[0][w] = 0 (no items, value is 0)
    // Already initialized to 0
    
    // Fill the table
    for (int i = 1; i <= n; i++) {
        for (int w = 0; w <= capacity; w++) {
            // Option 1: Don't take item i-1
            dp[i][w] = dp[i - 1][w];
            
            // Option 2: Take item i-1 (if it fits)
            if (weights[i - 1] <= w) {
                int valueWithItem = values[i - 1] + dp[i - 1][w - weights[i - 1]];
                dp[i][w] = Math.Max(dp[i][w], valueWithItem);
            }
        }
    }
    
    return dp[n][capacity];
}
```

### Knapsack Trace: weights=[2,3,4], values=[3,4,5], capacity=4

```
Build dp table (items × capacity):

          w=0  w=1  w=2  w=3  w=4
i=0 (no items)
         [0,   0,   0,   0,   0]

i=1 (item 0: w=2, v=3)
         [0,   0,   3,   3,   3]
             (skip, skip, take, take, take)

i=2 (item 1: w=3, v=4)
         [0,   0,   3,   4,   7]
             (skip, skip, skip, max(3,4), max(3,3+4))

i=3 (item 2: w=4, v=5)
         [0,   0,   3,   4,   5]
             (skip, skip, skip, skip, max(7, 5))

Answer: dp[3][4] = 5
```

---

# 📅 DAY 3: 2D DP - GRIDS & EDIT DISTANCE

**Topics:** 🧩 Grid DP (Unique Paths, Obstacles), 🔤 Edit Distance  
**Difficulty:** 🟡 Intermediate  
**Time Allocation:** 120 minutes core + 60 minutes practice  

## 3.1 GRID DP: UNIQUE PATHS

### Problem
```
m × n grid. Start at top-left [0,0], end at bottom-right [m-1, n-1].
Only move right or down.
Count distinct paths to reach the goal.

Example: 3×3 grid
  . . .
  . . .
  . . .

Answer: C(4, 2) = 6 paths
  (You need 2 downs and 2 rights, arranged in 4 moves)
```

### DP Solution

```csharp
/// <summary>
/// Unique Paths - Count paths on m×n grid
/// Time: O(m × n) | Space: O(m × n)
/// </summary>
public int UniquePaths(int m, int n) {
    // dp[i][j] = number of paths to reach [i, j]
    int[][] dp = new int[m][];
    for (int i = 0; i < m; i++) {
        dp[i] = new int[n];
    }
    
    // Base case: top-left cell
    dp[0][0] = 1;
    
    // Fill first row (can only move right)
    for (int j = 1; j < n; j++) {
        dp[0][j] = 1;
    }
    
    // Fill first column (can only move down)
    for (int i = 1; i < m; i++) {
        dp[i][0] = 1;
    }
    
    // Fill remaining cells
    for (int i = 1; i < m; i++) {
        for (int j = 1; j < n; j++) {
            // Paths to [i,j] = paths from above + paths from left
            dp[i][j] = dp[i - 1][j] + dp[i][j - 1];
        }
    }
    
    return dp[m - 1][n - 1];
}
```

### Unique Paths Trace: 3×3

```
DP table evolution:

Step 1: Base case
[1, 0, 0]
[0, 0, 0]
[0, 0, 0]

Step 2: First row (can only reach by moving right)
[1, 1, 1]
[0, 0, 0]
[0, 0, 0]

Step 3: First column (can only reach by moving down)
[1, 1, 1]
[1, 0, 0]
[1, 0, 0]

Step 4: Fill [1,1]
[1, 1, 1]
[1, 2, 0]  (paths = 1 from above + 1 from left)
[1, 0, 0]

Step 5: Fill [1,2]
[1, 1, 1]
[1, 2, 3]  (paths = 1 from above + 2 from left)
[1, 0, 0]

Step 6: Fill [2,1]
[1, 1, 1]
[1, 2, 3]
[1, 3, 0]  (paths = 1 from above + 2 from left)

Step 7: Fill [2,2]
[1, 1, 1]
[1, 2, 3]
[1, 3, 6]  (paths = 3 from above + 3 from left)

Answer: 6
```

## 3.2 GRID DP WITH OBSTACLES

### Problem
```
Same as above, but some cells are blocked (obstacles).
Can't move through obstacles.

Example: 3×3 with obstacle at [1,1]
  . . .
  . X .
  . . .

Answer: 2 paths (the obstacle blocks some routes)
```

### Solution

```csharp
/// <summary>
/// Unique Paths with Obstacles
/// Time: O(m × n) | Space: O(m × n)
/// </summary>
public int UniquePathsWithObstacles(int[][] obstacleGrid) {
    int m = obstacleGrid.Length;
    int n = obstacleGrid[0].Length;
    
    if (obstacleGrid[0][0] == 1 || obstacleGrid[m-1][n-1] == 1) {
        return 0;  // Start or end blocked
    }
    
    int[][] dp = new int[m][];
    for (int i = 0; i < m; i++) {
        dp[i] = new int[n];
    }
    
    dp[0][0] = 1;  // Start
    
    // First row
    for (int j = 1; j < n; j++) {
        if (obstacleGrid[0][j] == 0) {
            dp[0][j] = dp[0][j - 1];
        }
        // If obstacle, dp[0][j] remains 0
    }
    
    // First column
    for (int i = 1; i < m; i++) {
        if (obstacleGrid[i][0] == 0) {
            dp[i][0] = dp[i - 1][0];
        }
    }
    
    // Remaining cells
    for (int i = 1; i < m; i++) {
        for (int j = 1; j < n; j++) {
            if (obstacleGrid[i][j] == 0) {
                dp[i][j] = dp[i - 1][j] + dp[i][j - 1];
            }
            // If obstacle, dp[i][j] remains 0
        }
    }
    
    return dp[m - 1][n - 1];
}
```

## 3.3 EDIT DISTANCE (LEVENSHTEIN)

### Problem
```
Transform string s1 into string s2 using minimum operations:
  - Insert a character (cost 1)
  - Delete a character (cost 1)
  - Replace a character (cost 1)

Example:
  s1 = "cat"
  s2 = "dog"
  
  Operations: 
    - Replace 'c' with 'd'
    - Replace 'a' with 'o'
    - Replace 't' with 'g'
  
  Answer: 3
```

### DP Solution

```csharp
/// <summary>
/// Edit Distance (Levenshtein)
/// Time: O(m × n) | Space: O(m × n)
/// </summary>
public int EditDistance(string s1, string s2) {
    int m = s1.Length;
    int n = s2.Length;
    
    // dp[i][j] = edit distance for s1[0..i-1] and s2[0..j-1]
    int[][] dp = new int[m + 1][];
    for (int i = 0; i <= m; i++) {
        dp[i] = new int[n + 1];
    }
    
    // Base cases
    for (int i = 0; i <= m; i++) {
        dp[i][0] = i;  // Delete all i characters from s1
    }
    for (int j = 0; j <= n; j++) {
        dp[0][j] = j;  // Insert all j characters
    }
    
    // Fill table
    for (int i = 1; i <= m; i++) {
        for (int j = 1; j <= n; j++) {
            if (s1[i - 1] == s2[j - 1]) {
                dp[i][j] = dp[i - 1][j - 1];  // Match, no operation
            }
            else {
                // Choose minimum of three operations
                dp[i][j] = 1 + Math.Min(
                    Math.Min(
                        dp[i - 1][j],      // Delete
                        dp[i][j - 1]       // Insert
                    ),
                    dp[i - 1][j - 1]       // Replace
                );
            }
        }
    }
    
    return dp[m][n];
}
```

### Edit Distance Trace: "cat" → "dog"

```
Build DP table:

        ""  d   o   g
    ""  0   1   2   3
    c   1   1   2   3  (replace c→d, then insert o,g)
    a   2   2   2   3  (replace a→o, then...)
    t   3   3   3   3  (all characters need replacing)

Reading:
  [1,1]: c vs d → don't match → 1 + min(dp[0,1]=1, dp[1,0]=1, dp[0,0]=0) = 1 (replace)
  [2,2]: ca vs do → a≠o → 1 + min(dp[1,2]=2, dp[2,1]=2, dp[1,1]=1) = 2
  [3,3]: cat vs dog → t≠g → 1 + min(dp[2,3]=3, dp[3,2]=3, dp[2,2]=2) = 3

Answer: 3
```

---

# 📅 DAY 4: DP ON SEQUENCES

**Topics:** 🔗 Longest Common Subsequence (LCS), 📈 Longest Increasing Subsequence (LIS)  
**Difficulty:** 🟡 Intermediate (LIS O(n log n) is advanced)  
**Time Allocation:** 120 minutes core + 60 minutes practice  

## 4.1 LONGEST COMMON SUBSEQUENCE (LCS)

### Problem
```
Two sequences (strings, arrays).
Find the longest subsequence common to both.
(Subsequence: not necessarily consecutive, but order preserved)

Example:
  seq1 = "AGGTAB"
  seq2 = "GXTXAYB"
  LCS = "GTAB" (length 4)
  
  Explanation:
    seq1: A G G T A B
             ↓ ↓ ↓ ↓
    seq2: G X T X A Y B
             ↓     ↓     ↓

  Other common subsequences: "GT" (length 2), "GAB" (length 3), etc.
```

### DP Solution

```csharp
/// <summary>
/// Longest Common Subsequence
/// Time: O(m × n) | Space: O(m × n)
/// </summary>
public int LongestCommonSubsequence(string seq1, string seq2) {
    int m = seq1.Length;
    int n = seq2.Length;
    
    // dp[i][j] = LCS length for seq1[0..i-1] and seq2[0..j-1]
    int[][] dp = new int[m + 1][];
    for (int i = 0; i <= m; i++) {
        dp[i] = new int[n + 1];
    }
    
    // Fill table
    for (int i = 1; i <= m; i++) {
        for (int j = 1; j <= n; j++) {
            if (seq1[i - 1] == seq2[j - 1]) {
                // Characters match: extend LCS
                dp[i][j] = 1 + dp[i - 1][j - 1];
            }
            else {
                // Characters don't match: skip one from either sequence
                dp[i][j] = Math.Max(dp[i - 1][j], dp[i][j - 1]);
            }
        }
    }
    
    return dp[m][n];
}
```

### LCS Trace: "ACE" vs "ABE"

```
Build DP table:

       ""  A   B   E
    "" 0   0   0   0
    A  0   1   1   1  (A matches, extend; B,E don't)
    C  0   1   1   1  (C matches neither; max of up/left)
    E  0   1   1   2  (E matches; extend from dp[1,2]=1)

Answer: 2 (LCS = "AE")
```

## 4.2 LONGEST INCREASING SUBSEQUENCE (LIS) - O(n²)

### Problem
```
Single array of integers.
Find longest subsequence where elements are in strictly increasing order.

Example:
  arr = [10, 9, 2, 5, 3, 7, 101, 18]
  LIS = [2, 3, 7, 101] or [2, 3, 7, 18] (length 4)
```

### Solution (Naive O(n²))

```csharp
/// <summary>
/// Longest Increasing Subsequence - O(n²) approach
/// Time: O(n²) | Space: O(n)
/// </summary>
public int LongestIncreasingSubsequenceQuadratic(int[] arr) {
    if (arr.Length == 0) return 0;
    
    // dp[i] = length of LIS ending at index i
    int[] dp = new int[arr.Length];
    
    for (int i = 0; i < arr.Length; i++) {
        dp[i] = 1;  // Every element is LIS of length 1
        
        // Check all previous elements
        for (int j = 0; j < i; j++) {
            if (arr[j] < arr[i]) {
                // Can extend LIS ending at j
                dp[i] = Math.Max(dp[i], 1 + dp[j]);
            }
        }
    }
    
    // Return maximum LIS length
    int maxLIS = 0;
    foreach (int length in dp) {
        maxLIS = Math.Max(maxLIS, length);
    }
    return maxLIS;
}
```

### LIS O(n²) Trace: [10, 9, 2, 5, 3, 7, 101, 18]

```
Computing LIS lengths:

Index 0 (val=10): dp[0] = 1 (just [10])

Index 1 (val=9): No previous < 9, so dp[1] = 1 ([9])

Index 2 (val=2): No previous < 2, so dp[2] = 1 ([2])

Index 3 (val=5): Prev elements < 5: 2
                 dp[3] = 1 + dp[2] = 2 ([2,5])

Index 4 (val=3): Prev elements < 3: 2
                 dp[4] = 1 + dp[2] = 2 ([2,3])

Index 5 (val=7): Prev elements < 7: 2, 5, 3
                 Best: dp[4] + 1 = 3 ([2,3,7])

Index 6 (val=101): Prev elements < 101: all
                   Best: dp[5] + 1 = 4 ([2,3,7,101])

Index 7 (val=18): Prev elements < 18: 10, 9, 2, 5, 3, 7
                  Best: dp[5] + 1 = 4 ([2,3,7,18])

dp = [1, 1, 1, 2, 2, 3, 4, 4]
Answer: 4
```

## 4.3 LONGEST INCREASING SUBSEQUENCE - O(n log n) OPTIMIZATION

### Key Insight
```
Maintain a helper array: helper[i] = smallest ending value of LIS of length i+1

When processing new element:
  - Binary search to find its position in helper
  - It either extends longest LIS or replaces a worse tail
```

### Solution

```csharp
/// <summary>
/// Longest Increasing Subsequence - O(n log n) with binary search
/// Time: O(n log n) | Space: O(n)
/// </summary>
public int LongestIncreasingSubsequenceLinearLog(int[] arr) {
    List<int> helper = new List<int>();
    
    foreach (int num in arr) {
        // Binary search for position where num should go
        int pos = BinarySearchPosition(helper, num);
        
        if (pos == helper.Count) {
            // num is larger than all in helper, extend LIS
            helper.Add(num);
        }
        else {
            // num can replace an existing element (better tail)
            helper[pos] = num;
        }
    }
    
    return helper.Count;
}

private int BinarySearchPosition(List<int> helper, int target) {
    int left = 0, right = helper.Count;
    
    while (left < right) {
        int mid = left + (right - left) / 2;
        if (helper[mid] < target) {
            left = mid + 1;
        }
        else {
            right = mid;
        }
    }
    
    return left;
}
```

### O(n log n) Trace: [10, 9, 2, 5, 3, 7, 101, 18]

```
Processing elements with binary search:

Process 10:
  helper = []
  10 > nothing → append
  helper = [10]

Process 9:
  Binary search for 9 in [10]
  9 < 10, position = 0
  Replace helper[0] with 9
  helper = [9]

Process 2:
  Binary search for 2 in [9]
  2 < 9, position = 0
  Replace helper[0] with 2
  helper = [2]

Process 5:
  Binary search for 5 in [2]
  5 > 2, position = 1 (end)
  Append 5
  helper = [2, 5]

Process 3:
  Binary search for 3 in [2, 5]
  3 > 2 and 3 < 5, position = 1
  Replace helper[1] with 3
  helper = [2, 3]

Process 7:
  Binary search for 7 in [2, 3]
  7 > 3, position = 2 (end)
  Append 7
  helper = [2, 3, 7]

Process 101:
  Binary search for 101 in [2, 3, 7]
  101 > 7, position = 3 (end)
  Append 101
  helper = [2, 3, 7, 101]

Process 18:
  Binary search for 18 in [2, 3, 7, 101]
  18 > 7 and 18 < 101, position = 3
  Replace helper[3] with 18
  helper = [2, 3, 7, 18]

Length of helper = 4
Answer: 4
```

---

# 📅 DAY 5: STORY-DRIVEN DP — ADVANCED DESIGN

**Topics:** 📄 Text Justification, ♠️ Blackjack-Style DP, 🤹 Interpreting DP States  
**Difficulty:** 🔴 Advanced  
**Time Allocation:** 120 minutes core + 60 minutes practice  

## 5.1 TEXT JUSTIFICATION: BADNESS-DRIVEN FORMATTING

### Problem
```
Format words into lines with width constraint W.
Minimize "badness" = sum of (unused_spaces)³ per line.

Example:
  words = ["a", "very", "long", "word"]
  W = 8
  
  Possible formatting:
    Line 1: "a very" (length 6, unused = 2, badness = 8)
    Line 2: "long" (length 4, unused = 4, badness = 64)
    Line 3: "word" (length 4, unused = 4, badness = 64)
    Total: 136
```

### DP Solution

```csharp
/// <summary>
/// Text Justification - Minimize badness
/// Time: O(n²) | Space: O(n)
/// </summary>
public int MinimumBadness(string[] words, int width) {
    int n = words.Length;
    
    // Precompute costs
    int[][] cost = new int[n][];
    for (int i = 0; i < n; i++) {
        cost[i] = new int[n];
        for (int j = i; j < n; j++) {
            int totalLength = 0;
            for (int k = i; k <= j; k++) {
                totalLength += words[k].Length;
            }
            totalLength += (j - i);  // Spaces between words
            
            if (totalLength > width) {
                cost[i][j] = int.MaxValue;
            }
            else {
                int spaces = width - totalLength;
                cost[i][j] = spaces * spaces * spaces;  // Cubic
            }
        }
    }
    
    // dp[i] = min badness for words 0..i-1
    int[] dp = new int[n + 1];
    dp[0] = 0;
    
    for (int i = 1; i <= n; i++) {
        dp[i] = int.MaxValue;
        
        for (int j = 0; j < i; j++) {
            if (cost[j][i-1] != int.MaxValue) {
                dp[i] = Math.Min(dp[i], cost[j][i-1] + dp[j]);
            }
        }
    }
    
    return dp[n];
}
```

## 5.2 BLACKJACK: GAME TREE DP

### Problem
```
In simplified blackjack:
  - You have cards summing to my_total
  - Dealer shows one card
  - Decide: hit (take another card) or stand?
  
State: (my_total, dealer_card)
Goal: Maximize probability of winning
```

### Solution

```csharp
/// <summary>
/// Blackjack Optimal Play
/// Time: O(states × transitions) ≈ O(210) | Space: O(210)
/// </summary>
public double BlackjackEV(int myTotal, int dealerCard) {
    Dictionary<(int, int), double> memo = new Dictionary<(int, int), double>();
    return ComputeEV(myTotal, dealerCard, memo);
}

private double ComputeEV(int myTotal, int dealerCard, Dictionary<(int, int), double> memo) {
    if (memo.ContainsKey((myTotal, dealerCard))) {
        return memo[(myTotal, dealerCard)];
    }
    
    // Base cases
    if (myTotal > 21) return -1.0;  // Bust, lost
    if (myTotal == 21) return 1.0;  // Blackjack, win
    
    // Option 1: Stand
    double standValue = CompareHands(myTotal, dealerCard);
    
    // Option 2: Hit (average over card outcomes)
    double hitValue = 0.0;
    for (int card = 1; card <= 10; card++) {
        hitValue += ComputeEV(myTotal + card, dealerCard, memo);
    }
    hitValue /= 10.0;
    
    double result = Math.Max(standValue, hitValue);
    memo[(myTotal, dealerCard)] = result;
    return result;
}

private double CompareHands(int myTotal, int dealerCard) {
    // Simplified: dealer stands on their showing card
    int dealerTotal = dealerCard + 6;  // Average assumption
    
    if (dealerTotal > 21) return 1.0;   // Dealer busts
    if (myTotal > dealerTotal) return 1.0;    // I win
    if (myTotal == dealerTotal) return 0.0;   // Tie
    return -1.0;  // Dealer wins
}
```

## 5.3 CHOOSING MEANINGFUL STATES: THE ART

### Principle 1: Only Track Essential Information

**Bad state design:**
```csharp
Dictionary<(int word_idx, List<Word> words_used, int current_line_length)>
// Reasons bad:
// - words_used is redundant (can derive from word_idx)
// - Too much information → state space explodes
```

**Good state design:**
```csharp
Dictionary<int, int>  // dp[word_idx] = min badness
// Reasons good:
// - Minimal: only word_idx
// - Clear: which words remain to be formatted
// - Manageable: O(n) states
```

### Principle 2: Markovian Property

**Check:** Does future decision depend only on current state?

```
Text Justification:
  Future depends on: remaining words
  NOT on: how previous words were formatted
  → Markovian ✓

Blackjack:
  Future depends on: my_total, dealer_card
  NOT on: what cards were dealt before
  → Markovian ✓

Pathfinding with history:
  "Avoid cells you've visited"
  Future depends on: full history of visited cells
  NOT on: just current position
  → Non-Markovian ✗ (need different approach)
```

---

# 🎓 WEEK 10 SUMMARY & MASTERY CHECKLIST

## Key Concepts Mastered

| Day | Core Concept | Real-World Application | Complexity |
|-----|--------------|----------------------|------------|
| 1 | Overlapping subproblems, memoization, tabulation | Exponential → polynomial conversion | O(n) from O(2^n) |
| 2 | 1D DP patterns (stairs, robber, coins, knapsack) | Optimization under constraints | O(n·W) |
| 3 | 2D DP (grids, edit distance) | Pathfinding, string alignment | O(m·n) |
| 4 | Sequence analysis (LCS, LIS) | Text diff, trend detection | O(n²) or O(n log n) |
| 5 | State design mastery | Game AI, document layout, real systems | Design-dependent |

## Interview Readiness

✅ Can implement classic DP problems (Days 1-4)  
✅ Can optimize space/time trade-offs  
✅ Can recognize when DP applies  
✅ Can design DP from scratch (Day 5)  
✅ Can explain Bellman's principle  
✅ Can trace through DP tables  

## Common Pitfalls to Avoid

❌ **Base case errors** → Always validate dp[0], dp[1], edge cases  
❌ **Index off-by-one** → Careful with string/array indexing  
❌ **Wrong recurrence** → Verify logic flow step-by-step  
❌ **Infinite loops** → Ensure termination condition  
❌ **Space explosion** → Check if state can be optimized  
❌ **Non-Markovian dependency** → Validate DP assumption  

## Practice Roadmap

| Level | Problem Count | Difficulty | Examples |
|-------|---------------|-----------|----------|
| Foundation | 5 | Easy | Fibonacci, climbing stairs, house robber |
| Intermediate | 15 | Medium | Coin change, grid paths, knapsack |
| Advanced | 20 | Hard | Edit distance, LCS, text justification, game trees |
| Expert | 10 | Very Hard | Interval DP, state optimization, custom design |

---

# 🏆 WEEK 10 LEARNING OUTCOMES

By completing Week 10, you will have:

✅ **Understood** why DP transforms exponential problems into polynomial  
✅ **Implemented** 15+ classic DP solutions across categories  
✅ **Optimized** space and time complexity trade-offs  
✅ **Recognized** DP patterns in unfamiliar problems  
✅ **Designed** DP solutions from first principles  
✅ **Applied** DP to real systems (games, layouts, bioinformatics, trading)  

---

**Status:** ✅ Week 10 Full Playbook Complete

One comprehensive reference guide spanning all 5 days of DP fundamentals, ready for study, reference, and interview preparation.

---