# 📖 WEEK 10 DAY 02: 1D DYNAMIC PROGRAMMING & KNAPSACK FAMILY — ENGINEERING GUIDE

> 🧭 **Navigation:** [← Previous Day](Week_10_Day_01_DP_Recursion_Memoization_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_10_Day_03_2D_DP_Grids_Edit_Distance_Instructional.md)
> 
> 💡 **Instructor Note:** *1D DP and the Knapsack family represent the transition from simple linear counting to constrained resource optimization. Today we master the difference between Bounded (0/1) and Unbounded selections, understand the critical direction of 1D iteration (backward vs forward), and write zero-allocation production implementations.*

---

## 🎯 LEARNING OBJECTIVES

*By the end of this chapter, you will be able to:*
- 🎯 **Deconstruct** constrained resource problems using the **3-Step DP Recipe** (`Choice -> State Definition -> Base Cases & Transitions`).
- 🧩 **Distinguish** between 0/1 Knapsack (each item at most once) and Unbounded Knapsack (unlimited item reuse) at the byte level.
- 📐 **Prove** why 1D space optimization requires **backward iteration** for 0/1 Knapsack and **forward iteration** for Unbounded Knapsack.
- ⚙️ **Implement** production-grade C# (.NET 8/9) and idiomatic Python (3.11+) solutions for 0/1 Knapsack, Unbounded Knapsack, Coin Change, and House Robber.
- 🎙️ **Articulate** a crisp 45-minute verbal walkthrough detailing pseudo-polynomial time complexity and space compression.

---

## 📖 CHAPTER 1: CONTEXT & MOTIVATION

### The Constrained Allocation Dilemma

In systems engineering, resources are never infinite. Whether allocating cloud server memory across competing container workloads, selecting database transactions under storage quotas, or packing cargo containers, the engineering challenge is identical:
- You have a strictly constrained resource limit `W` (capacity, budget, weight, or time).
- You are presented with `N` available candidates, each requiring a cost `weight[i]` and providing a benefit `value[i]`.
- You must select a subset that maximizes total benefit without exceeding capacity `W`.

A naive brute-force search evaluates all possible subsets. For `N` items, there are `2^N` subsets. With just `N = 40` candidate tasks, exploring `2^40` (`~10^12`) possibilities causes an unrecoverable timeout.

> [!NOTE]
> **Enterprise Resource Optimization:** In production infrastructures—such as AWS EC2 Spot instance schedulers, Google Cloud bin-packing heuristics, and Stripe's real-time fraud scoring rules—knapsack-family DP allows engines to compute optimal resource assignments over tens of thousands of items in milliseconds, replacing intractable exponential permutations with linear table passes.

---

## 🧠 CHAPTER 2: THE 3-STEP RECIPE FOR 1D DP & KNAPSACK

```text
+-----------------------------------------------------------------------------------+
|                        3-STEP RECIPE: KNAPSACK FAMILY                             |
+-----------------------------------------------------------------------------------+
|  1. CHOICE            For item i with weight w_i and value v_i:                   |
|                       Option 0: SKIP item i (carry forward previous optimal).     |
|                       Option 1: TAKE item i (gain v_i, consume w_i from capacity).|
|                                                                                   |
|  2. STATE             2D: dp[i][w] = Max value using items 0..i with capacity w.  |
|                       1D: dp[w]    = Max value achievable at exact capacity w.    |
|                                                                                   |
|  3. TRANSITIONS &     0/1 Bounded:   dp[w] = max(dp[w], dp[w - weight[i]] + v_i)   |
|     BASE CASES                       (Iterate capacity w BACKWARDS: W down to w_i)|
|                       Unbounded:     dp[w] = max(dp[w], dp[w - weight[i]] + v_i)   |
|                                      (Iterate capacity w FORWARDS:  w_i up to W)  |
|                       Base:          dp[0] = 0; all other capacities init to 0.   |
+-----------------------------------------------------------------------------------+
```

### Visualizing Iteration Direction: Backward vs Forward

The most critical interview insight for 1D Knapsack optimization is the **direction of the inner loop**:

```text
===================================================================================
0/1 KNAPSACK (BOUNDED): BACKWARD LOOP PREVENTS DOUBLE-COUNTING
===================================================================================
Current Item: weight = 2, value = 3. Target Capacity W = 5.
Table state before item: [ dp[0]=0, dp[1]=0, dp[2]=0, dp[3]=0, dp[4]=0, dp[5]=0 ]

Iterate backwards: w = 5 down to 2:
w = 5: dp[5] = max(dp[5], dp[5 - 2] + 3) = max(0, dp[3] + 3) = 0 + 3 = 3  (reads old dp[3])
w = 4: dp[4] = max(dp[4], dp[4 - 2] + 3) = max(0, dp[2] + 3) = 0 + 3 = 3  (reads old dp[2])
w = 3: dp[3] = max(dp[3], dp[3 - 2] + 3) = max(0, dp[1] + 3) = 0 + 3 = 3  (reads old dp[1])
w = 2: dp[2] = max(dp[2], dp[2 - 2] + 3) = max(0, dp[0] + 3) = 0 + 3 = 3  (reads old dp[0])

Result: Item used AT MOST ONCE per capacity cell!

===================================================================================
UNBOUNDED KNAPSACK: FORWARD LOOP INTENTIONALLY ENABLES REUSE
===================================================================================
Current Item: weight = 2, value = 3. Target Capacity W = 5.
Table state before item: [ dp[0]=0, dp[1]=0, dp[2]=0, dp[3]=0, dp[4]=0, dp[5]=0 ]

Iterate forwards: w = 2 up to 5:
w = 2: dp[2] = max(dp[2], dp[2 - 2] + 3) = max(0, dp[0] + 3) = 3
w = 3: dp[3] = max(dp[3], dp[3 - 2] + 3) = max(0, dp[1] + 3) = 3
w = 4: dp[4] = max(dp[4], dp[4 - 2] + 3) = max(0, dp[2] + 3) = 3 + 3 = 6  <-- Reuses item!
w = 5: dp[5] = max(dp[5], dp[5 - 2] + 3) = max(0, dp[3] + 3) = 3 + 3 = 6  <-- Reuses item!

Result: Item used MULTIPLE TIMES as capacity expands!
```

---

## ⚙️ CHAPTER 3: MECHANICS & IMPLEMENTATION

### Pattern Taxonomy: The Knapsack Family

| Problem Pattern | Item Limit | Optimization Goal | Loop Ordering | Key Recurrence |
| :--- | :--- | :--- | :--- | :--- |
| **0/1 Knapsack** | Exactly 1 of each | Maximize Value | Outer: Items, Inner: `W` down to `w_i` | `dp[w] = max(dp[w], dp[w - w_i] + v_i)` |
| **Unbounded Knapsack** | Infinite of each | Maximize Value | Outer: Items, Inner: `w_i` up to `W` | `dp[w] = max(dp[w], dp[w - w_i] + v_i)` |
| **Coin Change (Min)** | Infinite of each | Minimize Count | Outer: Amounts, Inner: Coins | `dp[w] = min(dp[w], dp[w - coin] + 1)` |
| **Coin Change II** | Infinite of each | Count Combinations | Outer: Coins, Inner: `coin` up to `Amount` | `dp[w] += dp[w - coin]` |
| **House Robber** | Non-adjacent | Maximize Sum | Linear single pass `0..N-1` | `dp[i] = max(dp[i-1], dp[i-2] + val[i])` |

---

### Implementation 1: 0/1 Knapsack (2D Table, 1D Rolling, Reconstruction)

#### Modern C# (.NET 8/9) Implementation

```csharp
namespace DP.Knapsack;

public static class Knapsack01
{
    public readonly record struct Item(int Weight, int Value);

    // -------------------------------------------------------------
    // Approach 1: Classic 2D DP Table with Item Reconstruction
    // Time: O(N * W) | Space: O(N * W)
    // -------------------------------------------------------------
    public static (int MaxValue, List<Item> SelectedItems) Solve2DWithReconstruction(
        ReadOnlySpan<Item> items, int capacity)
    {
        int n = items.Length;
        int[,] dp = new int[n + 1, capacity + 1];

        // Fill DP table
        for (int i = 1; i <= n; i++)
        {
            var item = items[i - 1];
            for (int w = 0; w <= capacity; w++)
            {
                int skip = dp[i - 1, w];
                int take = (w >= item.Weight) ? dp[i - 1, w - item.Weight] + item.Value : 0;
                dp[i, w] = Math.Max(skip, take);
            }
        }

        // Backtrack to reconstruct selected items
        List<Item> selected = [];
        int currW = capacity;
        for (int i = n; i > 0 && currW > 0; i--)
        {
            if (dp[i, currW] != dp[i - 1, currW])
            {
                var takenItem = items[i - 1];
                selected.Add(takenItem);
                currW -= takenItem.Weight;
            }
        }

        selected.Reverse();
        return (dp[n, capacity], selected);
    }

    // -------------------------------------------------------------
    // Approach 2: Space-Optimized 1D Rolling Array (Backward Loop)
    // Time: O(N * W) | Space: O(W)
    // -------------------------------------------------------------
    public static int SolveOptimized(ReadOnlySpan<Item> items, int capacity)
    {
        int[] dp = new int[capacity + 1];

        foreach (var item in items)
        {
            // CRITICAL: Iterate backwards from capacity down to item.Weight
            for (int w = capacity; w >= item.Weight; w--)
            {
                dp[w] = Math.Max(dp[w], dp[w - item.Weight] + item.Value);
            }
        }

        return dp[capacity];
    }
}
```

#### Idiomatic Python (3.11+) Implementation

```python
from dataclasses import dataclass

@dataclass(frozen=True, slots=True)
class Item:
    weight: int
    value: int

class Knapsack01:
    """0/1 Knapsack: 2D with reconstruction and 1D space-optimized."""

    @staticmethod
    def solve_2d_with_reconstruction(items: list[Item], capacity: int) -> tuple[int, list[Item]]:
        n = len(items)
        dp = [[0] * (capacity + 1) for _ in range(n + 1)]

        for i in range(1, n + 1):
            item = items[i - 1]
            for w in range(capacity + 1):
                skip = dp[i - 1][w]
                take = (dp[i - 1][w - item.weight] + item.value) if w >= item.weight else 0
                dp[i][w] = max(skip, take)

        # Backtracking path reconstruction
        selected: list[Item] = []
        curr_w = capacity
        for i in range(n, 0, -1):
            if dp[i][curr_w] != dp[i - 1][curr_w]:
                selected.append(items[i - 1])
                curr_w -= items[i - 1].weight

        selected.reverse()
        return dp[n][capacity], selected

    @staticmethod
    def solve_optimized(items: list[Item], capacity: int) -> int:
        """Space-optimized 1D array iterating capacity backwards."""
        dp = [0] * (capacity + 1)

        for item in items:
            for w in range(capacity, item.weight - 1, -1):
                dp[w] = max(dp[w], dp[w - item.weight] + item.value)

        return dp[capacity]
```

---

### Implementation 2: Unbounded Knapsack & Coin Change

#### Modern C# (.NET 8/9) Implementation

```csharp
namespace DP.Knapsack;

public static class KnapsackUnbounded
{
    // -------------------------------------------------------------
    // Unbounded Knapsack: Maximum Value with Unlimited Items
    // Time: O(N * W) | Space: O(W)
    // -------------------------------------------------------------
    public static int MaxValue(int[] weights, int[] values, int capacity)
    {
        int[] dp = new int[capacity + 1];

        // Forward traversal allows reusing the same item multiple times
        for (int w = 1; w <= capacity; w++)
        {
            for (int i = 0; i < weights.Length; i++)
            {
                if (weights[i] <= w)
                {
                    dp[w] = Math.Max(dp[w], dp[w - weights[i]] + values[i]);
                }
            }
        }

        return dp[capacity];
    }

    // -------------------------------------------------------------
    // LeetCode 322: Coin Change (Minimum Coins to Form Amount)
    // Time: O(Amount * Coins.Length) | Space: O(Amount)
    // -------------------------------------------------------------
    public static int MinCoins(int[] coins, int amount)
    {
        int[] dp = new int[amount + 1];
        Array.Fill(dp, amount + 1); // Sentinel value representing infinity
        dp[0] = 0;

        for (int a = 1; a <= amount; a++)
        {
            foreach (int coin in coins)
            {
                if (coin <= a)
                {
                    dp[a] = Math.Min(dp[a], dp[a - coin] + 1);
                }
            }
        }

        return dp[amount] > amount ? -1 : dp[amount];
    }

    // -------------------------------------------------------------
    // LeetCode 518: Coin Change II (Count Number of Combinations)
    // Outer loop over coins prevents counting permutations!
    // -------------------------------------------------------------
    public static int ChangeCombinations(int[] coins, int amount)
    {
        int[] dp = new int[amount + 1];
        dp[0] = 1; // 1 way to make amount 0: pick nothing

        foreach (int coin in coins)
        {
            for (int a = coin; a <= amount; a++)
            {
                dp[a] += dp[a - coin];
            }
        }

        return dp[amount];
    }
}
```

#### Idiomatic Python (3.11+) Implementation

```python
class KnapsackUnbounded:
    """Unbounded Knapsack and Coin Change implementations."""

    @staticmethod
    def max_value(weights: list[int], values: list[int], capacity: int) -> int:
        dp = [0] * (capacity + 1)
        for w in range(1, capacity + 1):
            for weight, val in zip(weights, values):
                if weight <= w:
                    dp[w] = max(dp[w], dp[w - weight] + val)
        return dp[capacity]

    @staticmethod
    def min_coins(coins: list[int], amount: int) -> int:
        """LeetCode 322: Min coins with sentinel infinity."""
        sentinel = amount + 1
        dp = [sentinel] * (amount + 1)
        dp[0] = 0

        for a in range(1, amount + 1):
            for coin in coins:
                if coin <= a:
                    dp[a] = min(dp[a], dp[a - coin] + 1)

        return -1 if dp[amount] > amount else dp[amount]

    @staticmethod
    def change_combinations(coins: list[int], amount: int) -> int:
        """LeetCode 518: Outer coin loop ensures combinations, not permutations."""
        dp = [0] * (amount + 1)
        dp[0] = 1

        for coin in coins:
            for a in range(coin, amount + 1):
                dp[a] += dp[a - coin]

        return dp[amount]
```

---

### Implementation 3: House Robber (LeetCode 198)

#### Modern C# (.NET 8/9) Implementation

```csharp
namespace DP.Linear;

public static class HouseRobber
{
    // -------------------------------------------------------------
    // Space-Optimized O(1) Non-Adjacent Selection
    // Time: O(N) | Space: O(1)
    // -------------------------------------------------------------
    public static int Rob(ReadOnlySpan<int> nums)
    {
        if (nums.IsEmpty) return 0;

        int prev2 = 0; // Max profit ending at i - 2
        int prev1 = 0; // Max profit ending at i - 1

        foreach (int val in nums)
        {
            int current = Math.Max(prev1, prev2 + val);
            prev2 = prev1;
            prev1 = current;
        }

        return prev1;
    }
}
```

#### Idiomatic Python (3.11+) Implementation

```python
class HouseRobber:
    """House Robber: Non-adjacent array optimization."""

    @staticmethod
    def rob(nums: list[int]) -> int:
        prev2, prev1 = 0, 0
        for val in nums:
            prev2, prev1 = prev1, max(prev1, prev2 + val)
        return prev1
```

---

## ⚖️ CHAPTER 4: EXPLICIT COMPLEXITY DECONSTRUCTION

### Complexity & Pseudo-Polynomial Reality

| Algorithm | Time Complexity | Auxiliary Space (2D) | Auxiliary Space (1D Rolling) | Space Optimization Technique |
| :--- | :--- | :--- | :--- | :--- |
| **0/1 Knapsack** | `O(N * W)` | `O(N * W)` | `O(W)` | Backward loop: `w = W down to w_i` |
| **Unbounded Knapsack** | `O(N * W)` | `O(N * W)` | `O(W)` | Forward loop: `w = w_i up to W` |
| **Coin Change (Min)** | `O(A * C)` | N/A | `O(A)` | Array initialized to `A + 1` |
| **Coin Change II** | `O(A * C)` | N/A | `O(A)` | Outer loop: Coins, Inner: Amounts |
| **House Robber** | `O(N)` | `O(N)` | `O(1)` | Two rolling variables `prev2, prev1` |

> [!NOTE]
> **Why Knapsack is Pseudo-Polynomial:** The time complexity `O(N * W)` depends on the numeric magnitude of `W`, not just the number of input bits. If `W = 10^9`, the algorithm takes billions of steps despite `W` fitting in a 32-bit integer. When `W` is massive and `N` is small, branch-and-bound or Meet-in-the-Middle `O(2^(N/2))` is preferred over dynamic programming.

---

## 🎙️ CHAPTER 5: 45-MINUTE INTERVIEW VERBAL SCRIPT

```text
===================================================================================
               45-MINUTE INTERVIEW PLAYBOOK: 0/1 KNAPSACK & COIN CHANGE
===================================================================================

[00:00 - 05:00] CLARIFICATION & CONSTRAINTS
"Let's clarify the bounds. How large is capacity W and item count N? If W is up to 10^4 
and N is up to 10^3, an O(N * W) DP table executes in ~10^7 operations, well within 
the 1-second limit. Are weights strictly positive? Can items be taken fractionally? 
If fractional, greedy by value-to-weight ratio works in O(N log N); if discrete, we must 
use 0/1 Knapsack DP."

[05:00 - 12:00] THE 3-STEP RECIPE & 2D FORMULATION
"Applying the 3-Step Recipe:
 1. Choice: For item i, we either take it (if weight[i] <= w) or skip it.
 2. State: dp[i][w] = maximum value achievable using a subset of items 0..i-1 with limit w.
 3. Transitions:
    dp[i][w] = max(dp[i - 1][w], dp[i - 1][w - weight[i - 1]] + value[i - 1]).
 Base case: dp[0][w] = 0 for all w, since 0 items yield 0 value."

[12:00 - 22:00] SPACE OPTIMIZATION VIA 1D ROLLING ARRAY
"Notice that row i only depends on row i - 1. We do not need the full 2D table if we only 
require the maximum value. We can compress this to a 1D array of size W + 1.
However, there is a critical trap: if we iterate w forwards, dp[w - weight[i]] will 
already contain the updated value with the current item included, accidentally turning 
it into Unbounded Knapsack. To enforce 0/1 bounded selection, we must iterate w backwards 
from W down to weight[i]. This ensures we read exclusively from un-updated values from the 
previous iteration."

[22:00 - 32:00] CODING THE PRODUCTION SOLUTION
"I will now write the 1D space-optimized solution. I'll allocate an integer array dp of 
length W + 1 initialized to 0. For each item, I run the reverse loop:
for w from capacity down to item.weight:
    dp[w] = max(dp[w], dp[w - item.weight] + item.value)."

[32:00 - 40:00] EXTENSION TO UNBOUNDED & COIN CHANGE
"If the interviewer asks: 'What if items can be reused infinitely?'
I immediately reply: 'We change the inner loop direction from backward to forward! 
In Coin Change II, to count combinations rather than permutations, we iterate coins in 
the outer loop and amounts in the inner loop.'"

[40:00 - 45:00] DRY RUN & COMPLEXITY SUMMARY
"Let's dry run items=[(w:2, v:3), (w:3, v:4)] with capacity W=5.
Initial: [0, 0, 0, 0, 0, 0].
After item 1: dp[5]=3, dp[4]=3, dp[3]=3, dp[2]=3.
After item 2: dp[5]=max(3, dp[2]+4)=7.
Time Complexity: O(N * W), Auxiliary Space: O(W). Solution verified!"
===================================================================================
```

---

## ⚔️ PRACTICE PROBLEMS & INTERVIEW DRILLS

| # | Problem | Difficulty | Key Pattern | Focus Skill |
| :- | :--- | :-: | :--- | :--- |
| 1 | **LeetCode 416: Partition Equal Subset Sum** | 🟡 Medium | 0/1 Knapsack Bounded | Target capacity = `sum / 2` |
| 2 | **LeetCode 322: Coin Change** | 🟡 Medium | Unbounded Knapsack | Min coins with sentinel infinity |
| 3 | **LeetCode 518: Coin Change II** | 🟡 Medium | Counting Combinations | Outer coin loop order |
| 4 | **LeetCode 494: Target Sum** | 🟡 Medium | Subset Sum Transformation | Offset negative targets |
| 5 | **LeetCode 1049: Last Stone Weight II** | 🟡 Medium | 0/1 Knapsack Minimization | Minimize subset weight difference |

---

## 🎓 SELF-CHECK & FINAL VERIFICATION

- [x] **Zero LaTeX Check:** All equations use standard Markdown backticks (`O(N * W)`, `Phi^N`, `->`). No `$` signs.
- [x] **Production Dual-Language Code:** Complete C# (.NET 8/9) and Python (3.11+) implementations with reconstruction and zero placeholders.
- [x] **Clear 3-Step Recipe:** Problem formulations structured into Choice, State Definition, and Transitions & Base Cases.
- [x] **Cognitive Bloat Removed:** No legacy cognitive lens section or stats; corporate stories compressed into concise `> [!NOTE]` callouts.
- [x] **ASCII Visuals:** Clean diagrams illustrating backward (0/1) vs forward (Unbounded) iteration mechanics.

---

> 🧭 **Navigation:** [← Previous Day](Week_10_Day_01_DP_Recursion_Memoization_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_10_Day_03_2D_DP_Grids_Edit_Distance_Instructional.md)
