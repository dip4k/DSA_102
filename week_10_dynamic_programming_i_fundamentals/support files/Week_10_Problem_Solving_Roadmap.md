# 🧭 Week_10_Problem_Solving_Roadmap.md

**Week:** 10 | **Topic:** Dynamic Programming I: Fundamentals  
**Status:** ✅ Progressive Practice Strategy  
**Focus:** Pattern Recognition → Implementation → Mastery

---

## 📖 HOW TO USE THIS ROADMAP

This document provides a **three-stage progression** for problem-solving:
- **Stage 1 (Green)**: Direct applications, same as lecture examples
- **Stage 2 (Yellow)**: Variations and constraints, require adaptation
- **Stage 3 (Red)**: Integration and synthesis, combine multiple concepts

**For Each Problem:**
1. Read the problem without looking at hints
2. **Spend 10-15 minutes** thinking through the approach
3. Write the state, recurrence, and base cases on paper
4. Implement (code or pseudocode)
5. Trace through an example
6. Check against the provided solution approach

**If Stuck:**
- Revisit the Guidelines or Summary documents
- Look at the "Approach" section to understand the insight
- Trace a worked example step-by-step
- Return to the problem the next day

---

## 🟢 STAGE 1: BASIC UNDERSTANDING (Direct Applications)

These problems map almost directly to lecture content. The approach is likely what you learned.

### **1.1 Fibonacci Sequence**
**Problem:** Compute Fibonacci(n) where Fibonacci(0)=0, Fibonacci(1)=1, Fibonacci(n)=Fib(n-1)+Fib(n-2).

**Variations:**
- a) Return Fibonacci(n) % MOD
- b) Return the last digit of Fibonacci(n)
- c) Count Fibonacci numbers up to n

**Approach:**
- Recognize overlapping subproblems: Fib(n) calls Fib(n-1) and Fib(n-2), which call Fib(n-2) again.
- Memoization or tabulation: dp[i] = dp[i-1] + dp[i-2]
- Base cases: dp[0]=0, dp[1]=1
- Space optimization: O(1) using two variables

**Difficulty:** ⭐ (Easiest, foundational)

---

### **1.2 Climbing Stairs**
**Problem:** You're on step 0, goal is step n. Each move takes 1 or 2 steps. Count distinct ways.

**Variations:**
- a) Minimum cost to reach step n (each step has a cost)
- b) Ways to reach step n with exactly k moves
- c) Can take 1, 2, or 3 steps

**Approach:**
- State: dp[i] = ways to reach step i
- Recurrence: dp[i] = dp[i-1] + dp[i-2] (come from i-1 or i-2)
- Base cases: dp[0]=1 (already there), dp[1]=1
- For min cost variant: dp[i] = cost[i] + min(dp[i-1], dp[i-2])

**Difficulty:** ⭐ (Foundational, slightly harder than Fibonacci)

---

### **1.3 House Robber**
**Problem:** Houses in a row, each with money. Rob non-adjacent houses for max money.

**Variations:**
- a) Houses arranged in a circle (first and last are adjacent)
- b) Rob houses with constraint that you rob at most k consecutive houses in a row
- c) Different amounts of time to rob each house

**Approach:**
- State: dp[i] = max money robbing houses 0..i
- Recurrence: dp[i] = max(nums[i] + dp[i-2], dp[i-1])
  - Rob house i: gain nums[i] + best from 0..i-2
  - Skip house i: take best from 0..i-1
- Base cases: dp[0]=nums[0], dp[1]=max(nums[0], nums[1])

**Difficulty:** ⭐⭐ (Intermediate, same recurrence structure as climbing stairs)

---

### **1.4 Unique Paths**
**Problem:** m×n grid, move only right/down, count paths from top-left to bottom-right.

**Variations:**
- a) Obstacles in the grid (can't pass through)
- b) Different movement costs
- c) Count paths with specific length

**Approach:**
- State: dp[i][j] = number of paths to reach (i, j)
- Recurrence: dp[i][j] = dp[i-1][j] + dp[i][j-1]
  - Can come from above or from the left
- Base cases: dp[0][j]=1, dp[i][0]=1 (only one way along edges)
- For obstacles: if grid[i][j] is obstacle, dp[i][j]=0; else apply recurrence

**Difficulty:** ⭐⭐ (2D, straightforward extension of 1D)

---

### **1.5 Coin Change (Min Coins)**
**Problem:** Given coins and target amount, find minimum coins needed.

**Variations:**
- a) Coins with values and weights (choose subset)
- b) Limited coins (at most k of each type)
- c) Fewest coins and fewest operations combined

**Approach:**
- State: dp[i] = minimum coins to make amount i
- Recurrence: dp[i] = 1 + min(dp[i-c] for all coins c ≤ i)
- Base case: dp[0]=0 (zero coins for amount 0)
- Initialization: dp[i]=INF for i>0 (impossible initially)
- Key insight: Try all coins, take the minimum

**Difficulty:** ⭐⭐ (Multiple choice per position, optimization)

---

## 🟡 STAGE 2: VARIATIONS & CONSTRAINTS (Adaptation Required)

These problems require modifying the standard approach. You need to adapt the state or recurrence.

### **2.1 Unique Paths II (Obstacles)**
**Problem:** Same as unique paths, but some cells have obstacles.

**Adaptation:**
- Add guard: if cell has obstacle, dp[i][j]=0
- Else apply standard recurrence

**Key Insight:** State remains the same; just add a condition.

---

### **2.2 House Robber II (Circular)**
**Problem:** Houses in a **circle** (first and last are adjacent). Max money robbing non-adjacent.

**Approach:**
- Can't rob both house 0 and house n-1
- Split into two cases:
  - (a) Rob house 0, skip house n-1 → solve for houses 0..n-2
  - (b) Skip house 0, consider house n-1 → solve for houses 1..n-1
- Return max of two cases

**Key Insight:** Break the cycle by splitting the problem.

---

### **2.3 Coin Change II (Counting Ways)**
**Problem:** Count ways to make amount (not just minimum coins).

**Adaptation:**
- Recurrence: dp[i] += dp[i-c] for each coin c (summation, not minimization)
- **Critical:** Loop order matters! Coins outer, amounts inner to count combinations (not permutations)

**Key Insight:** Summing different operations gives counts; minimizing gives optimization.

---

### **2.4 Minimum Cost Climbing Stairs**
**Problem:** Each step has a cost. Start from step 0 or 1. Min cost to reach >= step n.

**Adaptation:**
- dp[i] = min cost to reach step i
- Recurrence: dp[i] = cost[i] + min(dp[i-1], dp[i-2])
- Can start from step 0 or 1, so answer is min(dp[n], dp[n+1]) conceptually
- Careful with endpoints

**Key Insight:** Cost changes the recurrence operation.

---

### **2.5 Edit Distance**
**Problem:** Transform s1 to s2 with min operations (insert, delete, replace).

**Approach:**
- State: dp[i][j] = edit distance between s1[0..i-1] and s2[0..j-1]
- Recurrence:
  - If s1[i-1]==s2[j-1]: dp[i][j]=dp[i-1][j-1]
  - Else: dp[i][j]=1+min(dp[i-1][j], dp[i][j-1], dp[i-1][j-1])
- Base cases: dp[0][j]=j (insert j), dp[i][0]=i (delete i)

**Key Insight:** Three operations lead to three recurrence choices.

---

### **2.6 Longest Common Subsequence (LCS)**
**Problem:** Find longest sequence in both s1 and s2 (non-contiguous match).

**Approach:**
- State: dp[i][j] = LCS length of s1[0..i-1] and s2[0..j-1]
- Recurrence:
  - If s1[i-1]==s2[j-1]: dp[i][j]=1+dp[i-1][j-1] (include character)
  - Else: dp[i][j]=max(dp[i-1][j], dp[i][j-1]) (skip one)
- Base cases: dp[0][j]=0, dp[i][0]=0

**Key Insight:** Matching characters extend the sequence; mismatches choose the better direction.

---

### **2.7 Longest Increasing Subsequence (LIS O(n²))**
**Problem:** Longest strictly increasing subsequence in an array.

**Approach:**
- State: dp[i] = LIS length *ending at* index i (crucial!)
- Recurrence: dp[i] = 1 + max(dp[j] for all j<i where arr[j]<arr[i])
- Base case: dp[i]=1 (every element is LIS of length 1)
- Answer: max(dp)

**Key Insight:** "Ending at index i" determines the recurrence structure.

---

### **2.8 0/1 Knapsack**
**Problem:** Capacity W, items with weight & value. Max value ≤ W? (Each item at most once.)

**Approach:**
- 1D variant: dp[w] = max value with capacity w
- Recurrence: dp[w] = max(dp[w], value[i] + dp[w-weight[i]])
- **Critical:** Iterate capacity **backward** (W to weight[i]) to avoid using item twice
- 2D variant: dp[i][w] = max value using first i items with capacity w (forward iteration is OK)

**Key Insight:** Backward iteration on 1D prevents reusing items.

---

### **2.9 Unbounded Knapsack (Variation)**
**Problem:** Same as 0/1 but each item available **unlimited** times.

**Adaptation:**
- Now iterate capacity **forward** (weight[i] to W)
- Using the same item twice is allowed

**Key Insight:** Iteration direction determines whether items are reused.

---

### **2.10 Climbing Stairs with Minimum Cost**
**Problem:** Each step i has cost c[i]. Min cost to reach step n?

**Approach:**
- dp[i] = min cost to reach step i
- Recurrence: dp[i] = min(dp[i-1], dp[i-2]) + cost[i]
- Base cases: dp[0]=cost[0], dp[1]=min(cost[0]+cost[1], cost[1])

**Key Insight:** Costs change the structure but keep the core recursion.

---

## 🔴 STAGE 3: INTEGRATION & SYNTHESIS (Combine Concepts)

These problems require combining multiple DP ideas or deep understanding of DP mechanics.

### **3.1 Russian Doll Envelopes**
**Problem:** Envelopes with (width, height). Each envelope fits inside another if both dimensions are strictly larger. Max envelopes you can nest?

**Approach:**
- Sort by width ascending, and by height descending (to handle ties)
- Then find LIS on heights
- Why descending on height ties? To avoid cheating (if two have same width, pick the smaller height)

**Key Insight:** Combines sorting + LIS + clever preprocessing.

---

### **3.2 Integer Break**
**Problem:** Break integer n into parts (≥2 parts). Maximize product?

**Approach:**
- dp[i] = max product for integer i
- Recurrence: dp[i] = max(j * dp[i-j]) for j=1..i-1, and max(j * (i-j)) to avoid forcing further breaks
- Constraints: Can't break into 1s (product = 1)

**Key Insight:** Choosing a break point and deciding whether to recurse.

---

### **3.3 Burst Balloons**
**Problem:** Balloons with values. Burst balloons in some order. When you burst balloon i, you earn nums[i-1]*nums[i]*nums[i+1]. Max earnings?

**Approach:**
- This is tricky! Standard DP doesn't work because neighbors change as you burst.
- Solution: Flip the problem. Instead of "which balloon to burst first," ask "which balloon to burst *last*."
- Let dp[l][r] = max coins bursting balloons between l and r (exclusive).
- When balloon k is the last one burst between l and r, neighbors are l and r.

**Key Insight:** Reframing the problem to fit DP structure.

---

### **3.4 Max Product Subarray**
**Problem:** Subarray with max product (can be negative, zero, or positive).

**Approach:**
- dp_max[i] = max product ending at index i
- dp_min[i] = min product ending at index i (important for negatives!)
- Recurrence:
  - dp_max[i] = max(nums[i], nums[i]*dp_max[i-1], nums[i]*dp_min[i-1])
  - dp_min[i] = min(nums[i], nums[i]*dp_max[i-1], nums[i]*dp_min[i-1])
- Answer: max(dp_max)

**Key Insight:** Track both max and min due to sign changes.

---

### **3.5 Longest Palindromic Subsequence**
**Problem:** Find longest subsequence that's a palindrome.

**Approach:**
- Insight: Longest palindromic subsequence = LCS(s, reverse(s))
- Or use DP: dp[i][j] = longest palindrome in s[i..j]
- Recurrence:
  - If s[i]==s[j]: dp[i][j] = 2 + dp[i+1][j-1]
  - Else: dp[i][j] = max(dp[i+1][j], dp[i][j-1])

**Key Insight:** Relate to known problems (LCS) or use range DP.

---

### **3.6 Text Justification (Optional Advanced)**
**Problem:** Justify text within a line width. Min "badness" (sum of squared spaces)?

**Approach:**
- dp[i] = min badness to justify words 0..i
- For each position i, try all possible last lines (j to i)
- Compute cost (badness) of that line
- Recurrence: dp[i] = min(dp[j] + cost(j+1, i)) for all valid j

**Key Insight:** Cost function is non-trivial; understand it first.

---

### **3.7 Decode Ways**
**Problem:** String of digits. Map: 1='A', 2='B', ..., 26='Z'. Count ways to decode?

**Approach:**
- dp[i] = ways to decode s[0..i-1]
- Recurrence:
  - Single digit: if s[i-1]!='0', add dp[i-1]
  - Two digits: if '10' ≤ s[i-2:i] ≤ '26', add dp[i-2]
- Base case: dp[0]=1, dp[1] = 1 if s[0]!='0' else 0

**Key Insight:** Check validity before adding to DP.

---

### **3.8 Maximal Square in Matrix**
**Problem:** Matrix of 0s and 1s. Largest all-1s square?

**Approach:**
- dp[i][j] = side length of largest square with bottom-right at (i,j)
- Recurrence: if matrix[i][j]='1': dp[i][j] = 1 + min(dp[i-1][j], dp[i][j-1], dp[i-1][j-1])
- Else: dp[i][j] = 0

**Key Insight:** Track side length (not area); min of three neighbors.

---

### **3.9 Word Break**
**Problem:** String s and word dictionary. Can s be segmented into words from dict?

**Approach:**
- dp[i] = can s[0..i-1] be segmented?
- Recurrence: dp[i] = true if there exists j<i where dp[j]=true and s[j..i-1] is in dict
- Base case: dp[0]=true (empty string)
- Optimization: Use HashSet for O(1) word lookup

**Key Insight:** Iterate through all split points.

---

### **3.10 Partition Equal Subset Sum**
**Problem:** Array of positive integers. Partition into two subsets with equal sum?

**Approach:**
- Equivalent to: "Is there a subset with sum = total_sum/2?"
- This is 0/1 knapsack with "target sum" as capacity
- dp[i] = can you make sum i?
- Recurrence: dp[i] = dp[i] or dp[i-num[j]] for each number j

**Key Insight:** Reframe as knapsack problem.

---

## 📊 PROBLEM-SOLVING PATTERNS SUMMARY

### **Pattern 1: Linear Sequence (1D Array)**
- State: dp[i] = solution for element/index i
- Recurrence: depends on dp[i-1], dp[i-2], etc.
- Examples: Fibonacci, climbing stairs, house robber, LIS

### **Pattern 2: 2D Grid**
- State: dp[i][j] = solution for cell (i,j)
- Recurrence: depends on dp[i-1][j], dp[i][j-1], etc.
- Examples: Unique paths, edit distance, LCS

### **Pattern 3: Two Strings**
- State: dp[i][j] = solution for s1[0..i-1] and s2[0..j-1]
- Recurrence: compare s1[i-1] and s2[j-1], then recurse
- Examples: Edit distance, LCS

### **Pattern 4: Knapsack Family**
- State: dp[item/index][capacity] = best value
- Recurrence: take/skip item, update capacity
- Examples: 0/1 knapsack, coin change, partition subset

### **Pattern 5: Optimization on Choices**
- State: dp[i] = optimal value considering element i
- Recurrence: choose best among all options
- Examples: House robber, max product subarray, max subarray sum (Kadane)

### **Pattern 6: Range DP**
- State: dp[l][r] = solution for range [l,r]
- Recurrence: try all k in [l, r], merge solutions
- Examples: Burst balloons, palindrome partition, matrix chain multiplication

---

## 🎯 DIFFICULTY ASSESSMENT TABLE

| Problem | Stage | Difficulty | Key Challenge | Time to Solve |
|---------|-------|-----------|---|---|
| Fibonacci | 1 | ⭐ | None, foundational | 5 min |
| Climbing Stairs | 1 | ⭐ | State definition | 8 min |
| House Robber | 1 | ⭐⭐ | Recurrence logic | 10 min |
| Unique Paths | 1 | ⭐⭐ | 2D thinking | 10 min |
| Coin Change | 1 | ⭐⭐ | Iteration logic | 12 min |
| House Robber II | 2 | ⭐⭐ | Problem decomposition | 12 min |
| Coin Change II | 2 | ⭐⭐ | Loop order | 12 min |
| Edit Distance | 2 | ⭐⭐⭐ | Three operations | 15 min |
| LCS | 2 | ⭐⭐⭐ | String comparison | 15 min |
| LIS O(n²) | 2 | ⭐⭐⭐ | "Ending at i" concept | 15 min |
| Burst Balloons | 3 | ⭐⭐⭐⭐ | Problem reframing | 25 min |
| Max Product Subarray | 3 | ⭐⭐⭐ | Min/max tracking | 15 min |
| Longest Palindromic Subseq | 3 | ⭐⭐⭐ | Connecting to LCS | 18 min |
| Word Break | 3 | ⭐⭐⭐ | Iteration over splits | 18 min |

---

## 📋 WEEKLY PROBLEM SCHEDULE

### **Day 1-2: Stage 1 Mastery**
- Fibonacci (all variants)
- Climbing stairs (variants a, b, c)
- House robber (base version)
- Unique paths (base version)
- Coin change min (base version)

### **Day 3: Stage 1 + Early Stage 2**
- House robber II (circular)
- Coin change II (ways)
- Edit distance basics
- Minimum cost climbing stairs

### **Day 4: Stage 2 Consolidation**
- Edit distance deep dive
- LCS implementation
- LIS O(n²)
- One variation per problem

### **Day 5: Stage 3 + Mixed**
- Choose 2-3 stage 3 problems based on interest
- Revisit Stage 1-2 problems for speed
- Aim for sub-15 minutes on stage 1-2

---

## 🏆 MASTERY CRITERIA

**Stage 1 Mastery:**
- ✅ Solve any problem in < 10 minutes
- ✅ Explain state/recurrence/base cases clearly
- ✅ No off-by-one errors

**Stage 2 Mastery:**
- ✅ Solve any problem in < 15 minutes
- ✅ Identify the key variation/challenge
- ✅ Adapt standard approach correctly

**Stage 3 Mastery:**
- ✅ Solve any problem in < 20-25 minutes
- ✅ Recognize problem type
- ✅ Design state and recurrence from scratch
- ✅ Handle edge cases

---

**Status:** ✅ Week 10 Problem Solving Roadmap Complete — Three-stage progression ready for implementation
