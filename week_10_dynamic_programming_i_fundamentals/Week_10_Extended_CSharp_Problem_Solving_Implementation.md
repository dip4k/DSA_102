# 📘 WEEK 10: DYNAMIC PROGRAMMING I — C# EXTENDED SUPPORT & IMPLEMENTATION GUIDE

**Document Type:** C# Extended Implementation Reference
**Scope:** Week 10 (Days 01-05) — Complete DP Implementation Patterns
**Language:** C# (.NET 6+)
**Target:** Production-ready code, best practices, optimization techniques
**Updated:** January 2026

---

## 🗂️ TABLE OF CONTENTS

1. **Introduction & C# Specific Considerations**
2. **Day 01: DP Fundamentals in C#**
3. **Day 02: 1D DP & Knapsack Family Implementation**
4. **Day 03: 2D DP — Grids & Strings in C#**
5. **Day 04: Sequence DP Algorithms**
6. **Day 05: Advanced Stories & Custom Solutions**
7. **Performance Optimization & Memory Management**
8. **Testing & Validation Framework**
9. **Complete Working Examples Repository**

---

## 🎯 INTRODUCTION: C# & DP CONSIDERATIONS

### Why C# for DP?

```csharp
// ✅ ADVANTAGES IN C#:
// 1. Strong typing catches state errors at compile time
// 2. LINQ enables elegant filtering and transformations
// 3. Dictionary<TKey, TValue> for efficient memoization
// 4. Array-based solutions are memory-efficient
// 5. Span<T> and Memory<T> for optimized algorithms

// ⚠️ GOTCHAS TO AVOID:
// 1. Stack overflow with deep recursion (max ~10K depth)
// 2. Array allocation failures for very large DP tables
// 3. Dictionary lookup overhead vs array access
// 4. Boxing/unboxing with value types (use struct carefully)
```

### Performance Characteristics

```csharp
// Data structure performance for DP:

// Arrays (Best for dense DP tables):
// Access: O(1) constant
// Memory: Contiguous, cache-friendly
// Example: dp[i][j] for grid problems

// Dictionary (Best for sparse DP or memoization):
// Access: O(1) average, O(n) worst case
// Memory: Hash-based, flexible
// Example: memo[state] for complex states

// Choose based on:
// - Array if state space is dense and bounded
// - Dictionary if state space is sparse or unbounded
```

---

## 📅 DAY 1: DP FUNDAMENTALS IN C#

### 1.1 Memoization Pattern (Top-Down)

**Problem:** Fibonacci number calculation

**Without Memoization (Exponential):**

```csharp
public class FibonacciExponential
{
    // ❌ DO NOT USE: O(2^n) time complexity
    public static int Fibonacci(int n)
    {
        if (n <= 1)
            return n;
        
        return Fibonacci(n - 1) + Fibonacci(n - 2);
    }
    
    // For n=40: Takes ~1 second (for n=50: hours!)
    // Reason: fib(39) + fib(38) = many redundant calculations
}
```

**With Memoization (Linear):**

```csharp
public class FibonacciMemoized
{
    // ✅ RECOMMENDED: O(n) time, O(n) space
    
    // Approach 1: Dictionary-based memoization
    private Dictionary<int, long> memo = new();
    
    public long ComputeFibonacci(int n)
    {
        if (n <= 1)
            return n;
        
        // Check cache first
        if (memo.ContainsKey(n))
            return memo[n];
        
        // Compute and cache
        long result = ComputeFibonacci(n - 1) + ComputeFibonacci(n - 2);
        memo[n] = result;
        
        return result;
    }
    
    // Approach 2: Array-based memoization (faster for small n)
    public static long ComputeFibonacciArray(int n)
    {
        if (n <= 1)
            return n;
        
        long[] memo = new long[n + 1];
        memo[0] = 0;
        memo[1] = 1;
        
        return ComputeFibonacciHelper(n, memo);
    }
    
    private static long ComputeFibonacciHelper(int n, long[] memo)
    {
        if (memo[n] != 0 && n > 1)
            return memo[n];
        
        if (n <= 1)
            return n;
        
        memo[n] = ComputeFibonacciHelper(n - 1, memo) + 
                  ComputeFibonacciHelper(n - 2, memo);
        
        return memo[n];
    }
}

// Usage:
var fib = new FibonacciMemoized();
Console.WriteLine(fib.ComputeFibonacci(40)); // Instant: 102334155
```

### 1.2 Tabulation Pattern (Bottom-Up)

**Iterative DP Solution:**

```csharp
public class FibonacciTabulation
{
    // ✅ BEST: O(n) time, O(n) space, no recursion overhead
    public static long ComputeFibonacci(int n)
    {
        if (n <= 1)
            return n;
        
        // Create DP table
        long[] dp = new long[n + 1];
        
        // Base cases
        dp[0] = 0;
        dp[1] = 1;
        
        // Fill table iteratively
        for (int i = 2; i <= n; i++)
        {
            dp[i] = dp[i - 1] + dp[i - 2];
        }
        
        return dp[n];
    }
    
    // Space-optimized version: O(1) space
    public static long ComputeFibonacciOptimized(int n)
    {
        if (n <= 1)
            return n;
        
        long prev2 = 0; // dp[i-2]
        long prev1 = 1; // dp[i-1]
        
        for (int i = 2; i <= n; i++)
        {
            long current = prev1 + prev2;
            prev2 = prev1;
            prev1 = current;
        }
        
        return prev1;
    }
}

// Trace for n=5:
// dp[0] = 0
// dp[1] = 1
// dp[2] = dp[1] + dp[0] = 1 + 0 = 1
// dp[3] = dp[2] + dp[1] = 1 + 1 = 2
// dp[4] = dp[3] + dp[2] = 2 + 1 = 3
// dp[5] = dp[4] + dp[3] = 3 + 2 = 5 ✓

// Performance comparison:
// Recursive (no memo): O(2^n) - exponential
// Memoization: O(n) - linear, but with recursion overhead
// Tabulation: O(n) - linear, optimal
```

### 1.3 Climbing Stairs Implementation

**Problem:** n stairs, can take 1 or 2 steps. How many ways?

```csharp
public class ClimbingStairs
{
    // Top-Down: Memoization approach
    public int ClimbStairsMemo(int n, Dictionary<int, int> memo = null)
    {
        memo ??= new Dictionary<int, int>();
        
        if (n <= 2)
            return n;
        
        if (memo.ContainsKey(n))
            return memo[n];
        
        memo[n] = ClimbStairsMemo(n - 1, memo) + ClimbStairsMemo(n - 2, memo);
        return memo[n];
    }
    
    // Bottom-Up: Tabulation approach (RECOMMENDED)
    public int ClimbStairsDP(int n)
    {
        if (n <= 2)
            return n;
        
        int[] dp = new int[n + 1];
        dp[1] = 1;
        dp[2] = 2;
        
        for (int i = 3; i <= n; i++)
        {
            dp[i] = dp[i - 1] + dp[i - 2];
        }
        
        return dp[n];
    }
    
    // Space-optimized (constant space)
    public int ClimbStairsOptimized(int n)
    {
        if (n <= 2)
            return n;
        
        int prev1 = 2;
        int prev2 = 1;
        
        for (int i = 3; i <= n; i++)
        {
            int current = prev1 + prev2;
            prev2 = prev1;
            prev1 = current;
        }
        
        return prev1;
    }
    
    // With reconstruction: Show the actual paths
    public List<List<int>> ClimbStairsWithPaths(int n)
    {
        var result = new List<List<int>>();
        var path = new List<int>();
        GeneratePaths(n, path, result);
        return result;
    }
    
    private void GeneratePaths(int remaining, List<int> path, List<List<int>> result)
    {
        if (remaining == 0)
        {
            result.Add(new List<int>(path));
            return;
        }
        
        if (remaining >= 1)
        {
            path.Add(1);
            GeneratePaths(remaining - 1, path, result);
            path.RemoveAt(path.Count - 1);
        }
        
        if (remaining >= 2)
        {
            path.Add(2);
            GeneratePaths(remaining - 2, path, result);
            path.RemoveAt(path.Count - 1);
        }
    }
}

// Test cases:
// ClimbStairs(1) = 1 → [1]
// ClimbStairs(2) = 2 → [1,1], [2]
// ClimbStairs(3) = 3 → [1,1,1], [1,2], [2,1]
// ClimbStairs(4) = 5 → [1,1,1,1], [1,1,2], [1,2,1], [2,1,1], [2,2]
```

### 1.4 State Design Pattern

```csharp
public class StateDesignTemplate
{
    // Generic DP state definition template
    
    /// <summary>
    /// Three critical questions for every DP problem:
    /// Q1: What does each state represent?
    /// Q2: How do we combine subproblem solutions?
    /// Q3: What are the base cases and final answer location?
    /// </summary>
    public class DPState
    {
        // Example: Climbing stairs
        // Q1: dp[i] = number of ways to reach stair i
        // Q2: dp[i] = dp[i-1] + dp[i-2]
        // Q3: Base: dp[1]=1, dp[2]=2; Answer: dp[n]
        
        public int[] DP { get; set; }
        public int StateSize { get; set; }
        
        public DPState(int n)
        {
            StateSize = n;
            DP = new int[n + 1];
        }
        
        public virtual void InitializeBase()
        {
            // Override in subclasses
            throw new NotImplementedException();
        }
        
        public virtual void FillTable()
        {
            // Override in subclasses
            throw new NotImplementedException();
        }
        
        public virtual int GetAnswer()
        {
            // Override in subclasses
            throw new NotImplementedException();
        }
    }
    
    // Concrete implementation for climbing stairs
    public class ClimbingStairsState : DPState
    {
        public ClimbingStairsState(int n) : base(n) { }
        
        public override void InitializeBase()
        {
            if (StateSize >= 1) DP[1] = 1;
            if (StateSize >= 2) DP[2] = 2;
        }
        
        public override void FillTable()
        {
            for (int i = 3; i <= StateSize; i++)
            {
                DP[i] = DP[i - 1] + DP[i - 2];
            }
        }
        
        public override int GetAnswer()
        {
            return DP[StateSize];
        }
    }
    
    // Usage pattern
    public static int SolveDP(int n)
    {
        var state = new ClimbingStairsState(n);
        state.InitializeBase();
        state.FillTable();
        return state.GetAnswer();
    }
}
```

---

## 📅 DAY 2: 1D DP & KNAPSACK FAMILY IMPLEMENTATION

### 2.1 House Robber Pattern

**Problem:** Array of house values, rob non-adjacent to maximize total.

```csharp
public class HouseRobber
{
    // Approach 1: DP with full table
    public int Rob(int[] nums)
    {
        if (nums == null || nums.Length == 0)
            return 0;
        if (nums.Length == 1)
            return nums[0];
        
        int[] dp = new int[nums.Length];
        dp[0] = nums[0];
        dp[1] = Math.Max(nums[0], nums[1]);
        
        for (int i = 2; i < nums.Length; i++)
        {
            // Option 1: Rob house i + best solution up to house i-2
            // Option 2: Skip house i and take best solution up to house i-1
            dp[i] = Math.Max(nums[i] + dp[i - 2], dp[i - 1]);
        }
        
        return dp[nums.Length - 1];
    }
    
    // Approach 2: Space-optimized (O(1) space)
    public int RobOptimized(int[] nums)
    {
        if (nums == null || nums.Length == 0)
            return 0;
        
        int prev2 = 0; // dp[i-2]
        int prev1 = 0; // dp[i-1]
        
        foreach (int num in nums)
        {
            int current = Math.Max(num + prev2, prev1);
            prev2 = prev1;
            prev1 = current;
        }
        
        return prev1;
    }
    
    // With house indices returned
    public (int MaxValue, List<int> RobbedHouses) RobWithReconstruction(int[] nums)
    {
        if (nums == null || nums.Length == 0)
            return (0, new List<int>());
        
        int n = nums.Length;
        int[] dp = new int[n];
        dp[0] = nums[0];
        
        if (n > 1)
            dp[1] = Math.Max(nums[0], nums[1]);
        
        for (int i = 2; i < n; i++)
        {
            dp[i] = Math.Max(nums[i] + dp[i - 2], dp[i - 1]);
        }
        
        // Reconstruct which houses were robbed
        var robbed = new List<int>();
        int i_track = n - 1;
        
        while (i_track >= 0)
        {
            // Check if current house was robbed
            if (i_track == 0)
            {
                if (dp[0] > 0) robbed.Add(0);
                break;
            }
            
            if (i_track == 1)
            {
                if (dp[1] >= nums[0])
                    robbed.Add(1);
                else if (nums[0] > 0)
                    robbed.Add(0);
                break;
            }
            
            // Check if robbing current house was optimal
            if (nums[i_track] + dp[i_track - 2] == dp[i_track])
            {
                robbed.Add(i_track);
                i_track -= 2;
            }
            else
            {
                i_track--;
            }
        }
        
        robbed.Reverse();
        return (dp[n - 1], robbed);
    }
}

// Test cases:
// nums = [1,2,3,1] → 4 (rob house 0 + house 2)
// nums = [2,7,9,3,1] → 12 (rob houses 0,2,4)
```

### 2.2 Coin Change Implementation

**Problem:** Minimum coins to make target amount.

```csharp
public class CoinChange
{
    // Approach 1: DP with full table
    public int MinCoins(int[] coins, int amount)
    {
        // dp[i] = minimum coins needed for amount i
        int[] dp = new int[amount + 1];
        
        // Initialize: amount 0 needs 0 coins, others need "infinity"
        Array.Fill(dp, amount + 1);
        dp[0] = 0;
        
        for (int i = 1; i <= amount; i++)
        {
            foreach (int coin in coins)
            {
                if (coin <= i)
                {
                    // Use this coin if it leads to fewer total coins
                    dp[i] = Math.Min(dp[i], dp[i - coin] + 1);
                }
            }
        }
        
        return dp[amount] > amount ? -1 : dp[amount];
    }
    
    // Approach 2: With path reconstruction
    public (int MinCoins, List<int> CoinsCombination) MinCoinsWithPath(int[] coins, int amount)
    {
        int[] dp = new int[amount + 1];
        int[] parent = new int[amount + 1]; // Track which coin was used
        
        Array.Fill(dp, amount + 1);
        dp[0] = 0;
        
        for (int i = 1; i <= amount; i++)
        {
            foreach (int coin in coins)
            {
                if (coin <= i && dp[i - coin] + 1 < dp[i])
                {
                    dp[i] = dp[i - coin] + 1;
                    parent[i] = coin; // Remember which coin led here
                }
            }
        }
        
        // Reconstruct path
        var result = new List<int>();
        int current = amount;
        
        while (current > 0)
        {
            int coin = parent[current];
            result.Add(coin);
            current -= coin;
        }
        
        return (dp[amount], result);
    }
    
    // Number of ways to make amount (related problem)
    public int ChangeWays(int[] coins, int amount)
    {
        // dp[i] = number of ways to make amount i
        int[] dp = new int[amount + 1];
        dp[0] = 1; // One way: use no coins
        
        // IMPORTANT: Iterate coins first, then amounts
        // This avoids counting different orderings as different ways
        foreach (int coin in coins)
        {
            for (int i = coin; i <= amount; i++)
            {
                dp[i] += dp[i - coin];
            }
        }
        
        return dp[amount];
    }
}

// Test cases:
// MinCoins([1,2,5], 5) → 1 (use one 5-coin)
// MinCoins([2], 3) → -1 (impossible)
// MinCoins([10], 10) → 1
// ChangeWays([1,2,5], 5) → 4 ([5], [2,2,1], [2,1,1,1], [1,1,1,1,1])
```

### 2.3 0/1 Knapsack Implementation

**Problem:** Maximize value within weight capacity (each item once).

```csharp
public class Knapsack01
{
    public class Item
    {
        public int Weight { get; set; }
        public int Value { get; set; }
        public int Index { get; set; }
        
        public Item(int weight, int value, int index = 0)
        {
            Weight = weight;
            Value = value;
            Index = index;
        }
        
        public double Ratio => (double)Value / Weight;
    }
    
    // Approach 1: Standard 2D DP
    public int KnapsackValue(Item[] items, int capacity)
    {
        int n = items.Length;
        int[,] dp = new int[n + 1, capacity + 1];
        
        // dp[i][w] = max value using first i items with weight limit w
        for (int i = 1; i <= n; i++)
        {
            for (int w = 0; w <= capacity; w++)
            {
                // Option 1: Don't take item i-1
                int skipValue = dp[i - 1, w];
                
                // Option 2: Take item i-1 (if it fits)
                int takeValue = 0;
                if (items[i - 1].Weight <= w)
                {
                    takeValue = items[i - 1].Value + dp[i - 1, w - items[i - 1].Weight];
                }
                
                dp[i, w] = Math.Max(skipValue, takeValue);
            }
        }
        
        return dp[n, capacity];
    }
    
    // Approach 2: Space-optimized 1D DP
    public int KnapsackValueOptimized(Item[] items, int capacity)
    {
        int[] dp = new int[capacity + 1];
        
        foreach (var item in items)
        {
            // IMPORTANT: Iterate backwards to avoid using same item twice
            for (int w = capacity; w >= item.Weight; w--)
            {
                dp[w] = Math.Max(dp[w], dp[w - item.Weight] + item.Value);
            }
        }
        
        return dp[capacity];
    }
    
    // Approach 3: With item reconstruction
    public (int MaxValue, List<Item> SelectedItems) KnapsackWithItems(Item[] items, int capacity)
    {
        int n = items.Length;
        int[,] dp = new int[n + 1, capacity + 1];
        
        for (int i = 1; i <= n; i++)
        {
            for (int w = 0; w <= capacity; w++)
            {
                int skipValue = dp[i - 1, w];
                int takeValue = 0;
                
                if (items[i - 1].Weight <= w)
                {
                    takeValue = items[i - 1].Value + dp[i - 1, w - items[i - 1].Weight];
                }
                
                dp[i, w] = Math.Max(skipValue, takeValue);
            }
        }
        
        // Reconstruct which items were selected
        var selected = new List<Item>();
        int i_ptr = n;
        int w_ptr = capacity;
        
        while (i_ptr > 0 && w_ptr > 0)
        {
            // Check if item i_ptr-1 was taken
            if (dp[i_ptr, w_ptr] != dp[i_ptr - 1, w_ptr])
            {
                selected.Add(items[i_ptr - 1]);
                w_ptr -= items[i_ptr - 1].Weight;
            }
            
            i_ptr--;
        }
        
        selected.Reverse();
        return (dp[n, capacity], selected);
    }
}

// Test case:
// items = [(w:2,v:3), (w:3,v:4)], capacity=5
// Result: value=7, items=[(w:2,v:3), (w:3,v:4)]
```

### 2.4 Unbounded Knapsack

```csharp
public class KnapsackUnbounded
{
    // Each item can be used unlimited times
    public int MaxValue(int[] weights, int[] values, int capacity)
    {
        int[] dp = new int[capacity + 1];
        
        for (int w = 0; w <= capacity; w++)
        {
            for (int i = 0; i < weights.Length; i++)
            {
                if (weights[i] <= w)
                {
                    // Key difference from 0/1: use dp[w], not dp[i-1][w]
                    // This allows reusing the same item
                    dp[w] = Math.Max(dp[w], dp[w - weights[i]] + values[i]);
                }
            }
        }
        
        return dp[capacity];
    }
    
    // Coin change is a special case (all values = 1)
    public int CoinCombinations(int[] coins, int amount)
    {
        int[] dp = new int[amount + 1];
        dp[0] = 1; // Empty combination
        
        foreach (int coin in coins)
        {
            for (int i = coin; i <= amount; i++)
            {
                dp[i] += dp[i - coin];
            }
        }
        
        return dp[amount];
    }
}
```

---

## 📅 DAY 3: 2D DP — GRIDS & STRINGS IN C#

### 3.1 Unique Paths in Grid

```csharp
public class UniquePaths
{
    // Approach 1: 2D DP
    public int CountPaths(int[][] grid)
    {
        int rows = grid.Length;
        int cols = grid[0].Length;
        
        int[,] dp = new int[rows, cols];
        
        // Initialize first cell
        dp[0, 0] = grid[0][0] == 1 ? 0 : 1; // 1 = obstacle
        
        // Initialize first row
        for (int j = 1; j < cols; j++)
        {
            dp[0, j] = (grid[0][j] == 1) ? 0 : dp[0, j - 1];
        }
        
        // Initialize first column
        for (int i = 1; i < rows; i++)
        {
            dp[i, 0] = (grid[i][0] == 1) ? 0 : dp[i - 1, 0];
        }
        
        // Fill remaining cells
        for (int i = 1; i < rows; i++)
        {
            for (int j = 1; j < cols; j++)
            {
                if (grid[i][j] == 1)
                {
                    dp[i, j] = 0; // Can't reach obstacle
                }
                else
                {
                    dp[i, j] = dp[i - 1, j] + dp[i, j - 1];
                }
            }
        }
        
        return dp[rows - 1, cols - 1];
    }
    
    // Approach 2: Space-optimized (only need previous row)
    public int CountPathsOptimized(int[][] grid)
    {
        int rows = grid.Length;
        int cols = grid[0].Length;
        
        int[] dp = new int[cols];
        
        // Initialize first cell
        dp[0] = grid[0][0] == 1 ? 0 : 1;
        
        // Initialize first row
        for (int j = 1; j < cols; j++)
        {
            dp[j] = (grid[0][j] == 1) ? 0 : dp[j - 1];
        }
        
        // Process each row
        for (int i = 1; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (grid[i][j] == 1)
                {
                    dp[j] = 0;
                }
                else if (j > 0)
                {
                    dp[j] = dp[j] + dp[j - 1]; // up + left
                }
            }
        }
        
        return dp[cols - 1];
    }
    
    // Path reconstruction
    public List<(int, int)> ReconstructPath(int[][] grid)
    {
        int rows = grid.Length;
        int cols = grid[0].Length;
        
        var path = new List<(int, int)>();
        int i = rows - 1, j = cols - 1;
        
        path.Add((i, j));
        
        while (i > 0 || j > 0)
        {
            if (i == 0)
                j--;
            else if (j == 0)
                i--;
            else if (/* path came from up */ true) // Simplified
                i--;
            else
                j--;
            
            path.Add((i, j));
        }
        
        path.Reverse();
        return path;
    }
}

// Grid visualization:
// S . .  (S = start, E = end, X = obstacle)
// . . .
// . . E
// 
// Paths:
// RRDDR (right, right, down, down, right)
// RDDRD, etc.
```

### 3.2 Edit Distance (Levenshtein)

```csharp
public class EditDistance
{
    // Classic 2D DP
    public int MinDistance(string word1, string word2)
    {
        int m = word1.Length;
        int n = word2.Length;
        
        // dp[i][j] = min edits to transform word1[0..i-1] to word2[0..j-1]
        int[,] dp = new int[m + 1, n + 1];
        
        // Base cases
        for (int i = 0; i <= m; i++)
            dp[i, 0] = i; // Delete all characters
        
        for (int j = 0; j <= n; j++)
            dp[0, j] = j; // Insert all characters
        
        // Fill table
        for (int i = 1; i <= m; i++)
        {
            for (int j = 1; j <= n; j++)
            {
                if (word1[i - 1] == word2[j - 1])
                {
                    // Characters match: no operation needed
                    dp[i, j] = dp[i - 1, j - 1];
                }
                else
                {
                    // Take minimum of three operations
                    int delete = dp[i - 1, j];     // Delete from word1
                    int insert = dp[i, j - 1];     // Insert into word1
                    int replace = dp[i - 1, j - 1]; // Replace
                    
                    dp[i, j] = 1 + Math.Min(delete, Math.Min(insert, replace));
                }
            }
        }
        
        return dp[m, n];
    }
    
    // Space-optimized version
    public int MinDistanceOptimized(string word1, string word2)
    {
        int m = word1.Length;
        int n = word2.Length;
        
        int[] prev = new int[n + 1];
        int[] curr = new int[n + 1];
        
        // Initialize first row
        for (int j = 0; j <= n; j++)
            prev[j] = j;
        
        for (int i = 1; i <= m; i++)
        {
            curr[0] = i;
            
            for (int j = 1; j <= n; j++)
            {
                if (word1[i - 1] == word2[j - 1])
                {
                    curr[j] = prev[j - 1];
                }
                else
                {
                    curr[j] = 1 + Math.Min(
                        Math.Min(prev[j], curr[j - 1]),
                        prev[j - 1]
                    );
                }
            }
            
            // Swap rows
            (prev, curr) = (curr, prev);
        }
        
        return prev[n];
    }
    
    // With operations reconstruction
    public (int Distance, List<string> Operations) EditWithOperations(string word1, string word2)
    {
        int m = word1.Length;
        int n = word2.Length;
        
        int[,] dp = new int[m + 1, n + 1];
        
        // Initialize
        for (int i = 0; i <= m; i++) dp[i, 0] = i;
        for (int j = 0; j <= n; j++) dp[0, j] = j;
        
        // Fill table
        for (int i = 1; i <= m; i++)
        {
            for (int j = 1; j <= n; j++)
            {
                if (word1[i - 1] == word2[j - 1])
                    dp[i, j] = dp[i - 1, j - 1];
                else
                    dp[i, j] = 1 + Math.Min(
                        Math.Min(dp[i - 1, j], dp[i, j - 1]),
                        dp[i - 1, j - 1]
                    );
            }
        }
        
        // Reconstruct operations
        var operations = new List<string>();
        int i_ptr = m, j_ptr = n;
        
        while (i_ptr > 0 || j_ptr > 0)
        {
            if (i_ptr == 0)
            {
                operations.Add($"Insert '{word2[j_ptr - 1]}'");
                j_ptr--;
            }
            else if (j_ptr == 0)
            {
                operations.Add($"Delete '{word1[i_ptr - 1]}'");
                i_ptr--;
            }
            else if (word1[i_ptr - 1] == word2[j_ptr - 1])
            {
                i_ptr--;
                j_ptr--;
            }
            else
            {
                int delete = dp[i_ptr - 1, j_ptr];
                int insert = dp[i_ptr, j_ptr - 1];
                int replace = dp[i_ptr - 1, j_ptr - 1];
                
                int minOp = Math.Min(delete, Math.Min(insert, replace));
                
                if (minOp == replace)
                {
                    operations.Add($"Replace '{word1[i_ptr - 1]}' with '{word2[j_ptr - 1]}'");
                    i_ptr--;
                    j_ptr--;
                }
                else if (minOp == delete)
                {
                    operations.Add($"Delete '{word1[i_ptr - 1]}'");
                    i_ptr--;
                }
                else
                {
                    operations.Add($"Insert '{word2[j_ptr - 1]}'");
                    j_ptr--;
                }
            }
        }
        
        operations.Reverse();
        return (dp[m, n], operations);
    }
}

// Test cases:
// MinDistance("horse", "ros") → 3
// Edit: replace 'h'→'r', delete 'r', delete 'e'
// MinDistance("", "abc") → 3
// MinDistance("a", "a") → 0
```

### 3.3 Longest Common Subsequence

```csharp
public class LongestCommonSubsequence
{
    // Return length of LCS
    public int LCSLength(string text1, string text2)
    {
        int m = text1.Length;
        int n = text2.Length;
        
        int[,] dp = new int[m + 1, n + 1];
        
        for (int i = 1; i <= m; i++)
        {
            for (int j = 1; j <= n; j++)
            {
                if (text1[i - 1] == text2[j - 1])
                {
                    dp[i, j] = dp[i - 1, j - 1] + 1;
                }
                else
                {
                    dp[i, j] = Math.Max(dp[i - 1, j], dp[i, j - 1]);
                }
            }
        }
        
        return dp[m, n];
    }
    
    // Return actual LCS string
    public string GetLCS(string text1, string text2)
    {
        int m = text1.Length;
        int n = text2.Length;
        
        int[,] dp = new int[m + 1, n + 1];
        
        // Build DP table
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
        
        // Reconstruct LCS
        var lcs = new System.Text.StringBuilder();
        int i_ptr = m, j_ptr = n;
        
        while (i_ptr > 0 && j_ptr > 0)
        {
            if (text1[i_ptr - 1] == text2[j_ptr - 1])
            {
                lcs.Insert(0, text1[i_ptr - 1]);
                i_ptr--;
                j_ptr--;
            }
            else if (dp[i_ptr - 1, j_ptr] > dp[i_ptr, j_ptr - 1])
            {
                i_ptr--;
            }
            else
            {
                j_ptr--;
            }
        }
        
        return lcs.ToString();
    }
}

// Test cases:
// LCS("abcde", "ace") → 3 ("ace")
// LCS("abc", "abc") → 3 ("abc")
// LCS("abc", "def") → 0 ("")
```

---

## 📅 DAY 4: SEQUENCE DP ALGORITHMS

### 4.1 Longest Increasing Subsequence (LIS)

```csharp
public class LongestIncreasingSubsequence
{
    // Approach 1: O(n²) DP
    public int LengthOfLIS_DP(int[] nums)
    {
        if (nums == null || nums.Length == 0)
            return 0;
        
        int n = nums.Length;
        int[] dp = new int[n];
        
        // dp[i] = length of LIS ending at index i
        for (int i = 0; i < n; i++)
        {
            dp[i] = 1; // At minimum, the element itself
            
            // Check all previous elements
            for (int j = 0; j < i; j++)
            {
                if (nums[j] < nums[i])
                {
                    dp[i] = Math.Max(dp[i], dp[j] + 1);
                }
            }
        }
        
        // Return maximum length
        return dp.Max();
    }
    
    // Approach 2: O(n log n) Binary Search
    public int LengthOfLIS_BinarySearch(int[] nums)
    {
        if (nums == null || nums.Length == 0)
            return 0;
        
        var tails = new List<int>();
        
        foreach (int num in nums)
        {
            // Find position to insert/replace
            int pos = BinarySearch(tails, num);
            
            if (pos == tails.Count)
                tails.Add(num);
            else
                tails[pos] = num;
        }
        
        return tails.Count;
    }
    
    private int BinarySearch(List<int> tails, int target)
    {
        int left = 0, right = tails.Count;
        
        while (left < right)
        {
            int mid = left + (right - left) / 2;
            
            if (tails[mid] < target)
                left = mid + 1;
            else
                right = mid;
        }
        
        return left;
    }
    
    // With reconstruction
    public (int Length, List<int> Subsequence) LIS_WithPath(int[] nums)
    {
        if (nums == null || nums.Length == 0)
            return (0, new List<int>());
        
        int n = nums.Length;
        int[] dp = new int[n];
        int[] parent = new int[n];
        Array.Fill(parent, -1);
        
        for (int i = 0; i < n; i++)
        {
            dp[i] = 1;
            
            for (int j = 0; j < i; j++)
            {
                if (nums[j] < nums[i] && dp[j] + 1 > dp[i])
                {
                    dp[i] = dp[j] + 1;
                    parent[i] = j;
                }
            }
        }
        
        // Find index with maximum LIS length
        int maxLen = 0, maxIdx = 0;
        for (int i = 0; i < n; i++)
        {
            if (dp[i] > maxLen)
            {
                maxLen = dp[i];
                maxIdx = i;
            }
        }
        
        // Reconstruct
        var lis = new List<int>();
        int idx = maxIdx;
        
        while (idx != -1)
        {
            lis.Add(nums[idx]);
            idx = parent[idx];
        }
        
        lis.Reverse();
        return (maxLen, lis);
    }
}

// Examples:
// [3,10,2,1,20] → Length 3, LIS = [3,10,20]
// [0,1,0,4,4,4,2,2,2,2,2,2,2,7,7,7,7,7,7] → Length 4
```

### 4.2 Maximum Subarray Sum (Kadane's Algorithm)

```csharp
public class MaximumSubarraySum
{
    // Classic Kadane's algorithm
    public int MaxSubArray(int[] nums)
    {
        if (nums == null || nums.Length == 0)
            return 0;
        
        int maxCurrent = nums[0]; // Max sum ending at current position
        int maxGlobal = nums[0];  // Overall maximum
        
        for (int i = 1; i < nums.Length; i++)
        {
            // Either extend current subarray or start new one
            maxCurrent = Math.Max(nums[i], maxCurrent + nums[i]);
            
            // Update global maximum
            maxGlobal = Math.Max(maxGlobal, maxCurrent);
        }
        
        return maxGlobal;
    }
    
    // With subarray indices
    public (int MaxSum, int StartIdx, int EndIdx) MaxSubArray_WithIndices(int[] nums)
    {
        int maxCurrent = nums[0];
        int maxGlobal = nums[0];
        
        int startIdx = 0;
        int endIdx = 0;
        int tempStart = 0;
        
        for (int i = 1; i < nums.Length; i++)
        {
            if (nums[i] > maxCurrent + nums[i])
            {
                maxCurrent = nums[i];
                tempStart = i;
            }
            else
            {
                maxCurrent = maxCurrent + nums[i];
            }
            
            if (maxCurrent > maxGlobal)
            {
                maxGlobal = maxCurrent;
                startIdx = tempStart;
                endIdx = i;
            }
        }
        
        return (maxGlobal, startIdx, endIdx);
    }
    
    // Variants: Maximum product subarray
    public int MaxProductSubarray(int[] nums)
    {
        int maxProd = nums[0];
        int minProd = nums[0]; // Track minimum (for negatives)
        int result = nums[0];
        
        for (int i = 1; i < nums.Length; i++)
        {
            // When encountering negative, max becomes min and vice versa
            if (nums[i] < 0)
                (maxProd, minProd) = (minProd, maxProd);
            
            maxProd = Math.Max(nums[i], maxProd * nums[i]);
            minProd = Math.Min(nums[i], minProd * nums[i]);
            
            result = Math.Max(result, maxProd);
        }
        
        return result;
    }
}

// Examples:
// [-2,1,-3,4,-1,2,1,-5,4] → 6 (subarray [4,-1,2,1])
// [5,-3,5] → 9 (entire array or [5])
// [-2] → -2 (only element)
```

### 4.3 Weighted Interval Scheduling

```csharp
public class WeightedIntervalScheduling
{
    public class Interval
    {
        public int Start { get; set; }
        public int End { get; set; }
        public int Weight { get; set; }
        public int Index { get; set; }
        
        public Interval(int start, int end, int weight, int index = 0)
        {
            Start = start;
            End = end;
            Weight = weight;
            Index = index;
        }
    }
    
    public int MaxWeight(Interval[] intervals)
    {
        // Sort by end time
        var sorted = intervals.OrderBy(x => x.End).ToArray();
        
        int n = sorted.Length;
        int[] dp = new int[n];
        
        dp[0] = sorted[0].Weight;
        
        for (int i = 1; i < n; i++)
        {
            // Option 1: Don't include interval i
            int excludeValue = dp[i - 1];
            
            // Option 2: Include interval i
            int p = FindLatestNonConflicting(sorted, i);
            int includeValue = sorted[i].Weight;
            if (p != -1)
                includeValue += dp[p];
            
            dp[i] = Math.Max(excludeValue, includeValue);
        }
        
        return dp[n - 1];
    }
    
    private int FindLatestNonConflicting(Interval[] intervals, int i)
    {
        // Binary search for latest interval that ends before intervals[i] starts
        int left = 0, right = i - 1;
        int result = -1;
        
        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            
            if (intervals[mid].End <= intervals[i].Start)
            {
                result = mid;
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }
        
        return result;
    }
    
    public (int MaxWeight, List<Interval> SelectedIntervals) MaxWeightWithPath(Interval[] intervals)
    {
        var sorted = intervals.OrderBy(x => x.End).ToArray();
        
        int n = sorted.Length;
        int[] dp = new int[n];
        bool[] included = new bool[n];
        
        dp[0] = sorted[0].Weight;
        included[0] = true;
        
        for (int i = 1; i < n; i++)
        {
            int excludeValue = dp[i - 1];
            
            int p = FindLatestNonConflicting(sorted, i);
            int includeValue = sorted[i].Weight;
            if (p != -1)
                includeValue += dp[p];
            
            if (includeValue > excludeValue)
            {
                dp[i] = includeValue;
                included[i] = true;
            }
            else
            {
                dp[i] = excludeValue;
                included[i] = false;
            }
        }
        
        // Reconstruct
        var selected = new List<Interval>();
        int i_ptr = n - 1;
        
        while (i_ptr >= 0)
        {
            if (included[i_ptr])
            {
                selected.Add(sorted[i_ptr]);
                i_ptr = FindLatestNonConflicting(sorted, i_ptr);
            }
            else
            {
                i_ptr--;
            }
        }
        
        selected.Reverse();
        return (dp[n - 1], selected);
    }
}

// Example:
// Intervals: [(1,3,5), (2,5,7), (4,6,8), (6,7,4)]
// Optimal: [(1,3,5), (4,6,8), (6,7,4)] = 17
```

---

## 📅 DAY 5: ADVANCED STORIES & CUSTOM SOLUTIONS

### 5.1 Text Justification Problem

```csharp
public class TextJustification
{
    public List<string> JustifyText(string[] words, int maxWidth)
    {
        var result = new List<string>();
        
        int n = words.Length;
        int[] dp = new int[n + 1];
        int[] parent = new int[n + 1];
        
        // dp[i] = minimum badness for words 0..i-1
        // Initially infinity
        for (int i = 0; i <= n; i++)
            dp[i] = int.MaxValue;
        
        dp[0] = 0;
        
        for (int i = 1; i <= n; i++)
        {
            for (int j = i - 1; j >= 0; j--)
            {
                int lineLength = GetLength(words, j, i - 1);
                
                if (lineLength <= maxWidth)
                {
                    int badness = (i == n) ? 0 : (maxWidth - lineLength) * (maxWidth - lineLength);
                    
                    if (dp[j] != int.MaxValue && dp[j] + badness < dp[i])
                    {
                        dp[i] = dp[j] + badness;
                        parent[i] = j;
                    }
                }
            }
        }
        
        // Reconstruct and format
        List<string> justifiedLines = new();
        int idx = n;
        
        while (idx > 0)
        {
            int start = parent[idx];
            justifiedLines.Add(FormatLine(words, start, idx - 1, maxWidth, idx == n));
            idx = start;
        }
        
        justifiedLines.Reverse();
        return justifiedLines;
    }
    
    private int GetLength(string[] words, int start, int end)
    {
        int length = 0;
        for (int i = start; i <= end; i++)
            length += words[i].Length;
        length += (end - start); // Spaces between words
        return length;
    }
    
    private string FormatLine(string[] words, int start, int end, int maxWidth, bool isLast)
    {
        var sb = new System.Text.StringBuilder();
        
        if (isLast)
        {
            // Last line: left-justified
            for (int i = start; i <= end; i++)
            {
                sb.Append(words[i]);
                if (i < end)
                    sb.Append(' ');
            }
        }
        else
        {
            // Distribute spaces evenly
            int wordCount = end - start + 1;
            int totalChars = 0;
            for (int i = start; i <= end; i++)
                totalChars += words[i].Length;
            
            int totalSpaces = maxWidth - totalChars;
            int spaceBetween = wordCount - 1;
            
            if (spaceBetween == 0)
            {
                sb.Append(words[start]);
            }
            else
            {
                int spacesPerGap = totalSpaces / spaceBetween;
                int extraSpaces = totalSpaces % spaceBetween;
                
                for (int i = start; i <= end; i++)
                {
                    sb.Append(words[i]);
                    if (i < end)
                    {
                        int spaces = spacesPerGap;
                        if (i - start < extraSpaces)
                            spaces++;
                        
                        sb.Append(new string(' ', spaces));
                    }
                }
            }
        }
        
        // Pad to maxWidth
        while (sb.Length < maxWidth)
            sb.Append(' ');
        
        return sb.ToString();
    }
}
```

---

## 🔧 PERFORMANCE OPTIMIZATION & MEMORY MANAGEMENT

### Memory Optimization Techniques

```csharp
public class MemoryOptimization
{
    // 1. Space reduction: 2D → 1D
    public class SpaceOptimizationExample
    {
        // Before: O(m × n) space for 2D grid DP
        public int GridDP_2D(int[][] grid)
        {
            int rows = grid.Length;
            int cols = grid[0].Length;
            
            int[,] dp = new int[rows, cols];
            // ... fill table ...
            return dp[rows - 1, cols - 1];
        }
        
        // After: O(n) space using rolling array
        public int GridDP_1D(int[][] grid)
        {
            int rows = grid.Length;
            int cols = grid[0].Length;
            
            int[] dp = new int[cols];
            // ... fill using rolling array ...
            return dp[cols - 1];
        }
    }
    
    // 2. Use arrays instead of dictionaries when possible
    public class DataStructureChoice
    {
        // Slower: Dictionary for memoization
        private Dictionary<int, long> memo = new();
        public long Fib_Dict(int n)
        {
            if (memo.ContainsKey(n))
                return memo[n];
            
            // ... computation ...
            return memo[n];
        }
        
        // Faster: Array for memoization
        private long[] memo_array = new long[1000];
        public long Fib_Array(int n)
        {
            if (memo_array[n] != 0)
                return memo_array[n];
            
            // ... computation ...
            return memo_array[n];
        }
    }
    
    // 3. Use Span<T> for performance-critical code
    public int OptimalDP(Span<int> nums)
    {
        // Span<T> avoids array copies and bounds checks
        int maxSum = nums[0];
        int currentSum = nums[0];
        
        for (int i = 1; i < nums.Length; i++)
        {
            currentSum = Math.Max(nums[i], currentSum + nums[i]);
            maxSum = Math.Max(maxSum, currentSum);
        }
        
        return maxSum;
    }
}
```

### Memoization Best Practices

```csharp
public class MemoizationBestPractices
{
    // Pattern 1: State-based memoization
    public class StateKey : IEquatable<StateKey>
    {
        public int Index { get; set; }
        public int Capacity { get; set; }
        
        public override bool Equals(object obj) => Equals(obj as StateKey);
        
        public bool Equals(StateKey other)
        {
            return other != null && Index == other.Index && Capacity == other.Capacity;
        }
        
        public override int GetHashCode()
        {
            return HashCode.Combine(Index, Capacity);
        }
    }
    
    // Pattern 2: Custom memoization with tuples
    public Dictionary<(int, int), int> MemoTable = new();
    
    public int DPWithMemo(int i, int j)
    {
        if (MemoTable.ContainsKey((i, j)))
            return MemoTable[(i, j)];
        
        // ... computation ...
        int result = /* ... */;
        
        MemoTable[(i, j)] = result;
        return result;
    }
    
    // Pattern 3: Lazy initialization
    private Dictionary<int, long> memo = new();
    
    public long ComputeWithLazy(int n)
    {
        if (!memo.TryGetValue(n, out long result))
        {
            result = ComputeHelper(n);
            memo[n] = result;
        }
        
        return result;
    }
    
    private long ComputeHelper(int n) => /* ... */;
}
```

---

## 🧪 TESTING & VALIDATION FRAMEWORK

```csharp
public class DPTestingFramework
{
    // Generic test case structure
    public record TestCase<TInput, TOutput>(
        string Description,
        TInput Input,
        TOutput Expected
    );
    
    // Test helper
    public class TestRunner<TInput, TOutput>
    {
        private readonly Func<TInput, TOutput> algorithm;
        private readonly List<TestCase<TInput, TOutput>> testCases;
        
        public TestRunner(Func<TInput, TOutput> algo)
        {
            algorithm = algo;
            testCases = new();
        }
        
        public void AddTest(string desc, TInput input, TOutput expected)
        {
            testCases.Add(new(desc, input, expected));
        }
        
        public void RunAll()
        {
            int passed = 0;
            int failed = 0;
            
            foreach (var test in testCases)
            {
                try
                {
                    var result = algorithm(test.Input);
                    
                    if (EqualityComparer<TOutput>.Default.Equals(result, test.Expected))
                    {
                        Console.WriteLine($"✓ {test.Description}");
                        passed++;
                    }
                    else
                    {
                        Console.WriteLine($"✗ {test.Description}: Expected {test.Expected}, got {result}");
                        failed++;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"✗ {test.Description}: {ex.Message}");
                    failed++;
                }
            }
            
            Console.WriteLine($"\nResults: {passed} passed, {failed} failed");
        }
    }
    
    // Example usage
    public void TestClimbingStairs()
    {
        var runner = new TestRunner<int, int>(n => new ClimbingStairs().ClimbStairsDP(n));
        
        runner.AddTest("Single stair", 1, 1);
        runner.AddTest("Two stairs", 2, 2);
        runner.AddTest("Three stairs", 3, 3);
        runner.AddTest("Four stairs", 4, 5);
        runner.AddTest("Five stairs", 5, 8);
        
        runner.RunAll();
    }
}
```

---

## 📊 COMPLEXITY REFERENCE TABLE

```csharp
public class ComplexityReference
{
    // Summary table for all Day 1-5 algorithms
    
    /*
    ╔══════════════════════════════╦═════════╦═══════════╦══════════════╗
    ║ Algorithm                    ║ Time    ║ Space     ║ Notes        ║
    ╠══════════════════════════════╬═════════╬═══════════╬══════════════╣
    ║ Fibonacci (recursive)         ║ O(2^n)  ║ O(n)      ║ Exponential  ║
    ║ Fibonacci (memo)              ║ O(n)    ║ O(n)      ║ Linear       ║
    ║ Fibonacci (tabulation)        ║ O(n)    ║ O(n)      ║ Optimal      ║
    ║ Fibonacci (space-opt)         ║ O(n)    ║ O(1)      ║ Best         ║
    ╠══════════════════════════════╬═════════╬═══════════╬══════════════╣
    ║ Climbing Stairs              ║ O(n)    ║ O(n) O(1) ║ Like Fib     ║
    ║ House Robber                 ║ O(n)    ║ O(1)      ║ 2-state      ║
    ║ Coin Change                  ║ O(n×m)  ║ O(n)      ║ n=amount,m=coin║
    ║ 0/1 Knapsack                 ║ O(n×W)  ║ O(W)      ║ n=items,W=cap║
    ║ Unbounded Knapsack           ║ O(W×n)  ║ O(W)      ║ Items ∞      ║
    ╠══════════════════════════════╬═════════╬═══════════╬══════════════╣
    ║ Unique Paths (grid)          ║ O(m×n)  ║ O(n)      ║ Space-opt    ║
    ║ Min Path Sum                 ║ O(m×n)  ║ O(n)      ║ Space-opt    ║
    ║ Edit Distance                ║ O(m×n)  ║ O(min(m,n))║ Space-opt   ║
    ║ LCS                          ║ O(m×n)  ║ O(min(m,n))║ Space-opt    ║
    ║ Matrix Chain Mult            ║ O(n³)   ║ O(n²)     ║ Interval DP  ║
    ╠══════════════════════════════╬═════════╬═══════════╬══════════════╣
    ║ LIS (DP)                     ║ O(n²)   ║ O(n)      ║ Simple       ║
    ║ LIS (Binary Search)          ║ O(n logn)║ O(n)     ║ Optimal      ║
    ║ Kadane's Algorithm           ║ O(n)    ║ O(1)      ║ Greedy-like  ║
    ║ Weighted Interval Sched      ║ O(n logn)║ O(n)     ║ With binary search║
    ║ Text Justification           ║ O(n²)   ║ O(n)      ║ Custom DP    ║
    ╚══════════════════════════════╩═════════╩═══════════╩══════════════╝
    */
}
```

---

## 🎓 COMPLETE WORKING EXAMPLES REPOSITORY

### Full Implementation: 0/1 Knapsack with All Variants

```csharp
public class KnapsackCompleteExample
{
    [TestMethod]
    public void TestKnapsack01()
    {
        var items = new[]
        {
            new Item(2, 3, 0),
            new Item(3, 4, 1)
        };
        
        var solver = new Knapsack01();
        
        // Test 1: Just value
        int value = solver.KnapsackValue(items, 5);
        Assert.AreEqual(7, value);
        
        // Test 2: With reconstruction
        var (maxVal, selected) = solver.KnapsackWithItems(items, 5);
        Assert.AreEqual(7, maxVal);
        Assert.AreEqual(2, selected.Count);
        
        // Test 3: Space-optimized
        int value2 = solver.KnapsackValueOptimized(items, 5);
        Assert.AreEqual(7, value2);
    }
    
    [TestMethod]
    public void TestEdgeCases()
    {
        var solver = new Knapsack01();
        
        // Empty items
        Assert.AreEqual(0, solver.KnapsackValue(new Item[0], 10));
        
        // Zero capacity
        Assert.AreEqual(0, solver.KnapsackValue(new[] { new Item(1, 5, 0) }, 0));
        
        // Item too heavy
        Assert.AreEqual(0, solver.KnapsackValue(new[] { new Item(10, 100, 0) }, 5));
    }
}
```

---

## 📝 SUMMARY & KEY TAKEAWAYS

**C# Advantages for DP:**
✅ Strong typing prevents state errors
✅ Dictionary/HashSet for efficient memoization
✅ LINQ for elegant array operations
✅ Span<T> for performance optimization
✅ Native support for complex state objects

**C# Gotchas to Avoid:**
❌ Deep recursion causes stack overflow
❌ Boxing/unboxing overhead
❌ Dictionary lookup slower than array access
❌ Large array allocation failures

**Best Practices Summary:**
1. Start with clear problem analysis (3 DP questions)
2. Implement brute force first
3. Add memoization (top-down) or tabulation (bottom-up)
4. Optimize space using rolling arrays
5. Reconstruct solutions using parent pointers
6. Test thoroughly with edge cases

---

**End of Week 10 C# Extended Support — Production-Ready Implementation Guide**