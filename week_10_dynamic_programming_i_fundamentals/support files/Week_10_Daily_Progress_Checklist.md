# 📅 Week_10_Daily_Progress_Checklist.md

**Week:** 10 | **Theme:** Dynamic Programming I: Fundamentals  
**Status:** ✅ Daily Action Plan  
**Philosophy:** Structured progress from understanding → implementation → mastery

---

## 🎯 WEEKLY GOAL
By the end of this week, you will:
- ✅ Understand overlapping subproblems and why DP fixes them
- ✅ Implement memoization and tabulation for 8+ classic problems
- ✅ Solve 1D, 2D, and sequence-based DP problems fluently
- ✅ Optimize space using rolling arrays and clever tricks
- ✅ Recognize DP patterns immediately in new problems

---

## 📅 DAY 1: OVERLAPPING SUBPROBLEMS & MEMOIZATION

### **Morning Session (3 hours)**

#### Understanding Overlapping Subproblems
- [ ] Draw the recursion tree for Fibonacci(5) by hand
  - Count total nodes in tree
  - Circle duplicate nodes
  - Note how many times Fib(2), Fib(3) appear
- [ ] Do the same for Fibonacci(6)
- [ ] Observe the pattern: How does the tree grow exponentially?

#### Naive Fibonacci Implementation
- [ ] Write `fibonacci_naive(n)` without looking at notes
  - [ ] Test with n=5, 6, 10 (time yourself; should be slow for n=30+)
  - [ ] Count approximate function calls: 2^n vs n unique subproblems

#### Analysis Task
- [ ] Complete: "Fibonacci(n) is O(___) because..."
- [ ] Explain to yourself: Why are subproblems repeated?

---

### **Afternoon Session (3 hours)**

#### Memoization Concept
- [ ] Read the definition: "Cache results to avoid recomputing."
- [ ] Understand the pattern:
  ```
  if subproblem in cache: return cache[subproblem]
  result = compute(...)
  cache[subproblem] = result
  return result
  ```

#### Memoized Fibonacci Implementation
- [ ] Write `fibonacci_memoization(n)` from scratch
  - [ ] Use a dictionary or default parameter for the memo
  - [ ] Test: n=30 should now be instant
  - [ ] Verify answers match naive version (for small n)

#### Understand the Transformation
- [ ] Side-by-side comparison:
  - [ ] Naive: Trace the recursion tree (many branches)
  - [ ] Memoized: Trace the same tree but note cache hits
  - [ ] What's the difference? (Same recursion, but fewer actual computations)

#### Climbing Stairs Problem (Memoized)
- [ ] Read problem: "n stairs, take 1 or 2 steps, count ways"
- [ ] Recursive formula: `ways(n) = ways(n-1) + ways(n-2)`
- [ ] Write `climbing_stairs_memoization(n)`
  - [ ] Test: n=10, 20, 50 (all should be instant)

---

### **End-of-Day Check**
- [ ] Can you explain why memoization works (in one sentence)?
- [ ] Can you trace `fibonacci_memoization(5)` on paper and show cache hits?
- [ ] Can you write climbing stairs memoization without notes?

---

## 📅 DAY 2: BOTTOM-UP TABULATION (1D DP)

### **Morning Session (4 hours)**

#### Understand Tabulation
- [ ] Read concept: "Fill a table bottom-up from base cases"
- [ ] Understand the shift from recursion → iteration

#### Fibonacci Tabulation
- [ ] Write `fibonacci_tabulation(n)` from scratch
  - [ ] Create array `dp` of size n+1
  - [ ] Set base cases: dp[0], dp[1]
  - [ ] Fill via loop: `for i in 2..n: dp[i] = dp[i-1] + dp[i-2]`
  - [ ] Return dp[n]
- [ ] Test and verify output matches memoization version

#### Hand Trace (Critical!)
- [ ] Create a table for n=5:
  ```
  i:    0  1  2  3  4  5
  dp:   0  1  ?  ?  ?  ?
  ```
- [ ] Fill each cell step-by-step
- [ ] Verify final answer is correct

#### Climbing Stairs Tabulation
- [ ] Write `climbing_stairs_tabulation(n)`
- [ ] Create and fill the table for n=5
- [ ] Compare: does it match the memoization version?

#### Problem Set 1: House Robber
- [ ] Read problem: "Rob non-adjacent houses for max money"
- [ ] Define state: `dp[i] = max money robbing houses 0..i`
- [ ] Write recurrence: `dp[i] = max(nums[i] + dp[i-2], dp[i-1])`
- [ ] Hand trace example: `houses = [1, 2, 3, 1]`
  ```
  i:    0  1  2  3
  dp:   1  2  4  4
  ```
- [ ] Write `rob_houses(nums)` tabulation version
- [ ] Test on 3 examples

---

### **Afternoon Session (3 hours)**

#### Problem Set 2: Coin Change (Min Coins)
- [ ] Read problem: "Min coins to make amount"
- [ ] Define state: `dp[i] = min coins to make amount i`
- [ ] Write recurrence: `dp[i] = 1 + min(dp[i - coin] for all coins ≤ i)`
- [ ] Key insight: Initialize dp[i] = INF (impossible), then update downward
- [ ] Hand trace example: `coins = [1, 2, 5], amount = 5`
  ```
  amount: 0  1  2  3  4  5
  dp:     0  1  1  2  2  1
  ```
- [ ] Write `coin_change_min(coins, amount)`
- [ ] Test on multiple examples

#### Comparison: Memoization vs Tabulation
- [ ] Implement both versions of one problem (e.g., house robber)
  - [ ] Compare code length: Which is longer?
  - [ ] Compare runtime intuition: Which is faster?
  - [ ] Compare space: Which uses less memory?
  - [ ] Compare readability: Which is clearer?

---

### **End-of-Day Check**
- [ ] Can you write Fibonacci tabulation without notes?
- [ ] Can you hand-trace a table and fill it correctly?
- [ ] Can you solve house robber and coin change tabulation from scratch?

---

## 📅 DAY 3: SPACE OPTIMIZATION & MORE 1D PATTERNS

### **Morning Session (4 hours)**

#### Space Optimization: Rolling Arrays
- [ ] Understand the principle: "We only need the last k values"
- [ ] Classic example: Fibonacci only needs dp[i-1] and dp[i-2]

#### Fibonacci O(1) Space
- [ ] Write `fibonacci_optimized(n)` using two variables:
  ```
  prev2, prev1 = 0, 1
  for i in 2..n:
      curr = prev1 + prev2
      prev2, prev1 = prev1, curr
  return prev1
  ```
- [ ] Test: Does it give the same answer as tabulation version?
- [ ] Verify space is now O(1), not O(n)

#### House Robber O(1) Space
- [ ] Write optimized version using two variables
- [ ] Hand trace on example: `houses = [1, 2, 3, 1]`

#### Climbing Stairs O(1) Space
- [ ] Write optimized version
- [ ] Understand: What are the two variables tracking?

#### When to Optimize
- [ ] Task: For each problem, identify:
  - [ ] Which prior dp values does dp[i] depend on?
  - [ ] Can we reduce to O(1) space? O(k) space?

---

### **Afternoon Session (3-4 hours)**

#### 0/1 Knapsack Problem
- [ ] Read problem: "Capacity W, items with weight & value, max value ≤ W?"
- [ ] Define state: `dp[w] = max value with capacity w`
- [ ] Write recurrence: `dp[w] = max(dp[w], value[i] + dp[w - weight[i]])`
- [ ] **Critical:** Why iterate capacity backward (not forward)?
  - [ ] Forward: Risk using same item twice
  - [ ] Backward: Ensures dp[w - weight[i]] is from previous item iteration
- [ ] Hand trace example:
  ```
  weights = [2, 3, 4], values = [3, 4, 5], capacity = 4
  ```
- [ ] Write `knapsack_01(weights, values, capacity)`
- [ ] Test and verify

#### Variation: Unbounded Knapsack
- [ ] Problem: Can use each item multiple times
- [ ] Difference: Iterate capacity forward (now OK to use same item)
- [ ] Write and test `knapsack_unbounded(...)`

#### Coin Change Ways (Variant)
- [ ] Problem: Count ways to make amount (not minimum coins)
- [ ] Different recurrence: `dp[i] += dp[i - coin]` instead of `min(...)`
- [ ] Key insight: Order of loops matters!
  - [ ] Outer loop = coins (avoid counting same combination twice)
  - [ ] Inner loop = amounts
- [ ] Write `coin_change_ways(coins, amount)`
- [ ] Example: `coins = [1, 2], amount = 3` → 2 ways: {1,1,1}, {1,2}

---

### **End-of-Day Check**
- [ ] Can you reduce a 1D DP to O(1) space?
- [ ] Do you understand why knapsack needs backward iteration?
- [ ] Can you distinguish min coins (min op) from ways (sum op)?

---

## 📅 DAY 4: 2D DP & GRIDS

### **Morning Session (4 hours)**

#### Unique Paths in Grid
- [ ] Problem: m×n grid, move only right/down, count paths
- [ ] Define state: `dp[i][j] = number of paths to (i, j)`
- [ ] Recurrence: `dp[i][j] = dp[i-1][j] + dp[i][j-1]`
- [ ] Base case: First row and column all 1s (one way: go right, or go down)
- [ ] Hand trace 3×3 grid:
  ```
     0  1  2
  0  1  1  1
  1  1  2  3
  2  1  3  6
  ```
- [ ] Write `unique_paths(m, n)`
- [ ] Test on multiple grid sizes

#### 2D Table Filling Order
- [ ] Understand: Must fill in order such that dependencies are available
- [ ] For unique paths: Fill top-to-bottom, left-to-right (natural order)
- [ ] Task: Why can't you fill bottom-to-top? (Depends on cells below, not yet filled)

#### Edit Distance (Levenshtein)
- [ ] Problem: Transform s1 to s2 with min operations (insert, delete, replace)
- [ ] Define state: `dp[i][j] = edit distance between s1[0..i-1] and s2[0..j-1]`
- [ ] Recurrence:
  ```
  if s1[i-1] == s2[j-1]:
      dp[i][j] = dp[i-1][j-1]  (no operation)
  else:
      dp[i][j] = 1 + min(
          dp[i-1][j],      (delete from s1)
          dp[i][j-1],      (insert into s1)
          dp[i-1][j-1]     (replace)
      )
  ```
- [ ] Base cases: `dp[i][0] = i`, `dp[0][j] = j`
  - [ ] Why? (i deletions to empty s2, j insertions to match s2)

#### Edit Distance Hand Trace
- [ ] Example: s1 = "cat", s2 = "dog"
- [ ] Create 4×4 table (including empty string)
- [ ] Fill step-by-step
- [ ] Expected answer: 3 (replace all three characters)

#### Write & Test
- [ ] Write `edit_distance(s1, s2)`
- [ ] Test on multiple examples

---

### **Afternoon Session (3 hours)**

#### Variant: Unique Paths II (Obstacles)
- [ ] Problem: Same, but grid has obstacles; can't pass through
- [ ] Modification: `if obstacle: dp[i][j] = 0 else: dp[i][j] = ...`
- [ ] Write `unique_paths_with_obstacles(grid)`

#### 2D Space Optimization
- [ ] Observe: For unique paths, only need previous row
- [ ] Write `unique_paths_space_optimized(m, n)` using 1D array
  - [ ] Create dp of size n
  - [ ] For each row, update in-place (right-to-left to avoid overwriting)
- [ ] Verify output matches 2D version

#### Longest Common Subsequence (LCS)
- [ ] Problem: Find longest sequence in both strings (non-contiguous)
- [ ] Example: s1 = "ABCD", s2 = "ACBD" → LCS = "ABD" (length 3)
- [ ] State: `dp[i][j] = LCS length of s1[0..i-1] and s2[0..j-1]`
- [ ] Recurrence:
  ```
  if s1[i-1] == s2[j-1]:
      dp[i][j] = 1 + dp[i-1][j-1]  (include character)
  else:
      dp[i][j] = max(dp[i-1][j], dp[i][j-1])  (exclude from one string)
  ```
- [ ] Hand trace: s1 = "ABC", s2 = "AC"
  ```
       ""  A  C
  ""   0  0  0
  A    0  1  1
  B    0  1  1
  C    0  1  2
  ```
- [ ] Write `lcs(s1, s2)`

---

### **End-of-Day Check**
- [ ] Can you fill a 2D table and trace dependencies?
- [ ] Do you understand all three edit distance operations?
- [ ] Can you reduce 2D DP to 1D space?

---

## 📅 DAY 5: SEQUENCES & INTEGRATION

### **Morning Session (3-4 hours)**

#### Longest Increasing Subsequence (O(n²) version)
- [ ] Problem: Find longest strictly increasing subsequence
- [ ] Example: [10, 9, 2, 5, 3, 7, 101, 18] → LIS = [2, 3, 7, 101] (length 4)
- [ ] State: `dp[i] = length of LIS ending at index i`
- [ ] Recurrence: `dp[i] = 1 + max(dp[j] for all j < i where arr[j] < arr[i])`
- [ ] Base case: `dp[i] = 1` (every element is LIS of length 1)
- [ ] Hand trace example:
  ```
  arr: 10  9  2  5  3  7  101  18
  dp:  1   1  1  2  2  3   4   4
  ```
- [ ] Write `lis_quadratic(arr)`
- [ ] Verify: Answer should be max(dp)

#### LIS O(n log n) Optimization (Concept)
- [ ] Understand the idea (don't need to implement, but understand):
  - [ ] Keep a "tails" array: tails[len] = smallest ending value of any LIS of length len+1
  - [ ] For each number, find where it fits via binary search
  - [ ] Update tails accordingly
- [ ] Read through the algorithm once
- [ ] Understand why it's O(n log n) (n iterations × binary search)

---

### **Afternoon Session (4 hours)**

#### Mixed Problem Set (Integration)
Solve 3 problems from scratch, choosing from:
- [ ] Problem 1: One 1D problem (memoization or tabulation)
  - [ ] Time yourself: Target < 10 minutes
  - [ ] Describe the state and recurrence before coding
  
- [ ] Problem 2: One 2D problem (grid or string)
  - [ ] Time yourself: Target < 15 minutes
  - [ ] Hand-trace before coding
  
- [ ] Problem 3: One sequence problem (LCS or LIS)
  - [ ] Time yourself: Target < 15 minutes
  - [ ] Explain the DP state clearly

#### Verification for Each Problem
- [ ] [ ] Did you define state clearly?
- [ ] [ ] Did you write the recurrence correctly?
- [ ] [ ] Did you set base cases correctly?
- [ ] [ ] Does the output match expected answers?
- [ ] [ ] Did you check edge cases (n=0, empty input)?

---

### **End-of-Week Reflection (1 hour)**

#### Personal Checklist
- [ ] I can spot overlapping subproblems in a problem
- [ ] I can choose between memoization and tabulation
- [ ] I can define DP state clearly before coding
- [ ] I can write recurrence relations without errors
- [ ] I can hand-trace tables and verify correctness
- [ ] I can reduce space using rolling arrays
- [ ] I can solve 1D, 2D, and sequence DP problems
- [ ] I understand the difference between optimization and counting DP
- [ ] I can explain DP to someone who's never seen it
- [ ] I can solve 3 DP problems in < 45 minutes total

#### Deep Dive: Where Do You Stand?
- [ ] **Mastery**: Solve any Week 10 problem in < 15 minutes. Explain naturally.
- [ ] **Proficiency**: Solve most problems in < 20 minutes. Need to think through state.
- [ ] **Competence**: Solve with hints or notes. State definition still tricky.
- [ ] **Developing**: Understand the concept but need guidance on implementation.
- [ ] **Struggling**: Need to revisit basics (overlapping subproblems, recurrence).

#### If Struggling
- [ ] Revisit Day 1-2 materials
- [ ] Re-trace Fibonacci and climbing stairs by hand multiple times
- [ ] Try memoization-first approach (feels more like recursion)
- [ ] Write recurrence in words before code

---

## 🎓 WEEKLY RETROSPECTIVE (Optional, Highly Recommended)

At the end of the week, ask yourself:

1. **What was the hardest concept this week?**
   - Overlapping subproblems / Memoization / Tabulation / 2D DP / Sequences / Space optimization

2. **What made it hard?**
   - State definition / Recurrence / Off-by-one errors / Iteration order / Something else

3. **What's your biggest AHA moment?**
   - The moment you understood why DP works / Why iteration order matters / How to optimize space / etc.

4. **Which problem type feels most natural to you?**
   - 1D / 2D grids / Sequences / Still unclear on all

5. **What will you practice more next week?**
   - Same pattern on new problems / Different problem types / Space optimization / Interview-style problems

---

## 📊 PROGRESS TRACKING

Use this table to track your daily performance:

| Day | Morning Check | Afternoon Check | Evening Check | Confidence (1-5) | Notes |
|-----|---|---|---|---|---|
| 1 | Recursion tree drawn | Memoization works | Can explain in 1 sentence | _ | |
| 2 | Tabulation understood | House robber + coin solved | Hand-traced correctly | _ | |
| 3 | Space optimization done | Knapsack working | Min coins vs ways clear | _ | |
| 4 | Unique paths filled | Edit distance done | 2D dependencies clear | _ | |
| 5 | LIS O(n²) written | 3 mixed problems solved | Reflection complete | _ | |

---

**Status:** ✅ Week 10 Daily Progress Checklist Complete — Ready for Implementation
