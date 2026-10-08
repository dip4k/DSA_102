# 🥊 Dynamic Programming vs. Greedy: The Head-to-Head Guide

> *"Greedy takes the first shiny object without looking ahead. Dynamic Programming writes down past calculations in a notebook so it can explore all possibilities safely."*

---

## 💥 The War Story: The Cloud VM Packing Catastrophe

At a Fortune 500 logistics company, the infrastructure team managed a Kubernetes cluster running **15,000 containerized microservices** on top of AWS EC2 bare-metal instances.

To minimize server costs, an engineer wrote a custom pod-scheduling daemon using a **Greedy Best-Fit strategy**:
> *"Whenever a new service pod requests CPU and Memory, greedily pack it into the server with the least remaining free RAM that can still fit the pod."*

The algorithm was fast (`O(N log M)`). It deployed to production on a Friday.

Within 48 hours, the infrastructure bills went through the roof:
1. The greedy scheduler packed pods aggressively, but it left thousands of **tiny memory fragments** behind (e.g., 1.2 GB of leftover RAM on 800 different machines).
2. Because 1.2 GB is too small to fit any standard microservice pod (which needed 2 GB to 4 GB), that leftover memory was **dead, stranded space**. Over **1.1 Terabytes of expensive server memory sat completely useless**!
3. When new core services launched on Monday morning, the cluster couldn't find a single server with 4 GB of contiguous RAM, forcing AWS to spin up **65 additional bare-metal instances at \$4.80/hour each**.
4. The cloud bill spiked by **\$228,000 in a single quarter** from stranded capacity alone!

### The Fix: Multi-Dimensional Knapsack DP
The lead architect replaced the greedy daemon with a **Multi-Dimensional Knapsack Dynamic Programming scheduler**:
- Instead of making irreversible local decisions, the DP scheduler evaluated combinations of pods to find exact-fit packings that minimized dead memory gaps.
- Memory utilization jumped from **68% to 94%**.
- The cluster was able to terminate 48 instances immediately, saving **\$1.4 million annually**.

**The Golden Takeaway:** Greedy works when resources can be taken fractionally without leaving dead gaps. But whenever items are **discrete and indivisible (0/1 all-or-nothing)**, Greedy leaves fragmented dead space. You must use **Dynamic Programming** to find the true global optimum.

---

## 📝 1. The Everyday Hook: Why Do We Need DP?

Try this simple experiment:
1. Write on a piece of paper: `1 + 1 + 1 + 1 + 1`
2. Ask a colleague: *"What does that equal?"*
3. They count and answer: **"5"**.
4. Now write `+ 1` at the very end of the line.
5. Ask your colleague: *"What does it equal now?"*
6. They immediately answer: **"6"**!

Did they recount all five 1s from the very beginning? **No!**  
They remembered that the earlier part was `5`, and simply added `1`.

That is the essence of **Dynamic Programming (DP)**:
> **Dynamic Programming is smart recursion with a notepad.**  
> Whenever you calculate the answer to a subproblem, you write it down. The next time the algorithm encounters that exact same subproblem, it looks up the answer in `O(1)` time instead of recomputing it.

---

## ⚡ 2. The Fundamental Conflict: Greedy vs. DP

```
                 [ Optimization Problem (Min / Max) ]
                                  │
        ┌─────────────────────────┴─────────────────────────┐
        ▼                                                   ▼
   [ GREEDY ]                                            [ DP ]
• Pick the best choice NOW.                       • Test ALL valid choices.
• Irreversible: Never reconsider past moves.     • Compare which choice yields the best future.
• Blindingly FAST: O(N) or O(N log N).           • Memoize sub-answers: O(N * Target).
• RISKY: Fails if early move traps you.          • 100% SAFE: Guaranteed optimal result.
```

---

## 🎯 3. The 4 Essential Interview Variations of DP vs. Greedy

In FAANG interviews, the boundary between DP and Greedy is tested across **4 primary problem archetypes**:

```
                         ┌─────────────────────────────────────────┐
                         │       DP VS GREEDY ARCHETYPES           │
                         └─────────────────────────────────────────┘
                                              │
        ┌─────────────────────┬───────────────┴───────────────┬─────────────────────┐
        ▼                     ▼                               ▼                     ▼
 [ 1. Coin & Change ]  [ 2. Knapsack & Capacity ]      [ 3. Sequences &       [ 4. Skipping &
   • Standard Coins:     • Fractional: Greedy            Substrings ]            Robbing ]
     Greedy works          (by value/weight ratio)      • LCS & Edit Dist      • House Robber
   • Arbitrary Coins:    • 0/1 All-or-Nothing: DP       • LIS (O(N log N))     • Climbing Stairs
     DP required           (capacity state table)       • Palindrome Substr    • Buy/Sell Stock
```

Let's dissect each archetype.

---

## 🪙 Archetype 1: The Coin Change Showdown

### The Problem
You need to make change for a target amount using the minimum number of coins.

### Case A: Standard US Currency (Greedy SUCCEEDS!)
- Coins: `[25, 10, 5, 1]`, Target: `41` cents.
- Greedy takes: `25` (rem 16), then `10` (rem 6), then `5` (rem 1), then `1` (rem 0).
- Total: `4` coins (`25 + 10 + 5 + 1`). This is mathematically optimal!
- **Why Greedy Works Here:** US coin denominations are designed such that every larger coin is at least double the previous, so a larger coin is always more efficient than any combination of smaller coins.

### Case B: Arbitrary Denominations (Greedy FAILS!)
- Coins: `[1, 3, 4]`, Target: `6` cents.
- **Greedy approach:** Takes largest coin `<= 6` -> **`4`** (remaining: 2). Then takes `1`, then `1`.
  - Greedy result: `[4, 1, 1]` (**3 coins**).
- **DP reality:** Explores all possibilities and tests combining smaller coins:
  - Takes `3` (remaining: 3). Takes `3` (remaining: 0).
  - Optimal result: `[3, 3]` (**2 coins**)!

### 🎨 Visual Napkin Trace of DP Table

```
dp[amount] = minimum coins to make amount

Amount:      0   1   2   3   4   5   6
----------------------------------------
Base Case:   0   -   -   -   -   -   -
Coin 1:      0   1   2   3   4   5   6
Coin 3:      0   1   2   1   2   3   2  <-- At 6: min(dp[6], dp[6 - 3] + 1) = dp[3] + 1 = 2!
Coin 4:      0   1   2   1   1   2   2  <-- At 6: min(2, dp[6 - 4] + 1) = min(2, dp[2] + 1) = 2
```

### C# (.NET 8/9) Code (Coin Change DP)
```csharp
namespace Paradigms.DynamicProgramming;

public static class CoinChangeSolver
{
    public static int MinCoins(int[] coins, int amount)
    {
        if (amount <= 0) return 0;
        if (coins is null || coins.Length == 0) return -1;

        int sentinel = amount + 1;
        int[] dp = new int[amount + 1];
        Array.Fill(dp, sentinel);
        dp[0] = 0;

        for (int curr = 1; curr <= amount; curr++)
        {
            foreach (int coin in coins)
            {
                if (curr - coin >= 0)
                {
                    dp[curr] = Math.Min(dp[curr], dp[curr - coin] + 1);
                }
            }
        }

        return dp[amount] > amount ? -1 : dp[amount];
    }
}
```

### Python (3.11+) Code
```python
from typing import List

def min_coins(coins: List[int], amount: int) -> int:
    if amount <= 0:
        return 0
    sentinel = amount + 1
    dp = [sentinel] * (amount + 1)
    dp[0] = 0

    for curr in range(1, amount + 1):
        for coin in coins:
            if curr - coin >= 0:
                dp[curr] = min(dp[curr], dp[curr - coin] + 1)

    return dp[amount] if dp[amount] <= amount else -1
```

---

## 🎒 Archetype 2: Fractional vs. 0/1 Knapsack

This is the canonical theoretical question asked in Google, Meta, and Amazon interviews:  
*"Why is Fractional Knapsack greedy, but 0/1 Knapsack requires Dynamic Programming?"*

```
Capacity: 5 kg

Item A: 4 kg, Value $100  (Ratio: $25.0 / kg)
Item B: 3 kg, Value $70   (Ratio: $23.3 / kg)
Item C: 2 kg, Value $50   (Ratio: $25.0 / kg)

Fractional Knapsack (GREEDY):
Take 100% of Item A (4 kg, $100). 
Need 1 kg more: Take 33.3% of Item B (1 kg, $23.33).
Total Value = $123.33! (Zero wasted capacity!)

0/1 Knapsack (DP REQUIRED):
Greedy by ratio picks Item A ($100). Remaining 1 kg fits NOTHING. Value = $100.
DP explores picking Item B + Item C (3 kg + 2 kg = 5 kg). Value = $120!
DP beats Greedy by $20!
```

### 0/1 Knapsack Production Implementation with 1D Memory Optimization

Instead of using a huge 2D matrix of `O(N * W)` memory, modern production systems compress the DP state into a single **1D array iterated in reverse order**:

#### C# (.NET 8/9) Code
```csharp
namespace Paradigms.DynamicProgramming;

public static class Knapsack01Solver
{
    public static int MaxValue(int[] weights, int[] values, int capacity)
    {
        if (weights == null || values == null || capacity <= 0) return 0;

        int n = weights.Length;
        // 1D rolling array: dp[w] = max value achievable with capacity w
        int[] dp = new int[capacity + 1];

        for (int i = 0; i < n; i++)
        {
            int w = weights[i];
            int v = values[i];

            // Traverse backward from capacity down to item weight
            // Backward traversal prevents using the same item multiple times!
            for (int cap = capacity; cap >= w; cap--)
            {
                dp[cap] = Math.Max(dp[cap], dp[cap - w] + v);
            }
        }

        return dp[capacity];
    }
}
```

#### Python (3.11+) Code
```python
from typing import List

def knapsack_01(weights: List[int], values: List[int], capacity: int) -> int:
    if not weights or not values or capacity <= 0:
        return 0

    dp = [0] * (capacity + 1)

    for w, v in zip(weights, values):
        # Traverse backward to ensure 0/1 single item usage
        for cap in range(capacity, w - 1, -1):
            dp[cap] = max(dp[cap], dp[cap - w] + v)

    return dp[capacity]
```

---

## 🏠 Archetype 3: Skipping & Decision Boundaries (House Robber)

### The FAANG Problem
You are a robber planning to rob houses along a street. Each house has money `nums[i]`. Adjacent houses have connected security alarms: **you cannot rob two adjacent houses on the same night**. What is the maximum money you can rob?

### The Intuitive Hook: The Binary Choice at House `i`
When standing in front of House `i`, you have exactly **two choices**:
1. **Rob House `i`:** You collect `nums[i]`, but you **must skip** House `i - 1`. Your score is `nums[i] + best(i - 2)`.
2. **Skip House `i`:** You collect \$0 from this house, but you are free to keep whatever you took from House `i - 1`. Your score is `best(i - 1)`.

```
Best at House i = Math.Max(best(i - 1), nums[i] + best(i - 2))
```

Because we only need the previous two answers, this can be solved in **`O(N)` time and `O(1)` memory**!

### C# (.NET 8/9) Code
```csharp
namespace Paradigms.DynamicProgramming;

public static class HouseRobberSolver
{
    public static int Rob(int[] nums)
    {
        if (nums is null || nums.Length == 0) return 0;
        if (nums.Length == 1) return nums[0];

        int prev2 = 0;           // best up to i - 2
        int prev1 = nums[0];     // best up to i - 1

        for (int i = 1; i < nums.Length; i++)
        {
            int current = Math.Max(prev1, nums[i] + prev2);
            prev2 = prev1;
            prev1 = current;
        }

        return prev1;
    }
}
```

### Python (3.11+) Code
```python
from typing import List

def rob(nums: List[int]) -> int:
    if not nums:
        return 0
    if len(nums) == 1:
        return nums[0]

    prev2 = 0
    prev1 = nums[0]

    for i in range(1, len(nums)):
        current = max(prev1, nums[i] + prev2)
        prev2 = prev1
        prev1 = current

    return prev1
```

---

## 📊 Summary Comparison: How to Choose in an Interview

| Question to Ask Yourself | If YES... | If NO... |
| :--- | :--- | :--- |
| Can items be broken into fractions? | **⚡ Pick Greedy** (`O(N log N)`) | Check next question |
| Does an early choice restrict or eliminate future valid options? | **📝 Pick Dynamic Programming** (`O(N * W)`) | Pick Greedy |
| Do the subproblems overlap and repeat? | **📝 Pick Dynamic Programming** | Pick Divide & Conquer |
| Is `N <= 25` and we need all valid configurations? | **🧶 Pick Backtracking** | Pick Dynamic Programming |
