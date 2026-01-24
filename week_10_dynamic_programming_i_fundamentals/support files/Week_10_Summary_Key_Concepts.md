# 🗺️ Week_10_Summary_Key_Concepts.md

**Week:** 10 | **Phase:** C (Trees, Graphs & Dynamic Programming)  
**Topic:** Dynamic Programming I: Fundamentals  
**Status:** ✅ Comprehensive Concept Reference  
**Word Count:** ~4,500 words (deep reference material)

---

## 📖 WEEK 10 NARRATIVE: FROM RECURSION TO OPTIMIZATION

Week 10 traces a **learning arc**: you begin with the observation that naive recursion solves the same subproblems redundantly. This redundancy explodes exponentially for problems like Fibonacci. The insight is simple but profound: **cache results**. Memoization does this recursively (top-down); tabulation does it iteratively (bottom-up). Both transform overlapping subproblem explosion into polynomial time.

The week progresses from understanding this principle (Day 1) → applying it to 1D arrays (Days 2-3) → extending to 2D grids and sequences (Days 4). By the end, you see DP as a unified framework: define state clearly, express the recurrence, fill the table, extract the answer.

---

## DAY-BY-DAY CONCEPT SUMMARIES

### **DAY 1: OVERLAPPING SUBPROBLEMS & MEMOIZATION**

#### Core Insight
**Naive recursion wastes time by recomputing identical subproblems.**

Fibonacci( n) naively calls Fibonacci(n-1) + Fibonacci(n-2). But Fibonacci(n-1) also calls Fibonacci(n-2). The recursion tree has exponential size because nodes are duplicated.

Example: Fibonacci(5) = Fibonacci(4) + Fibonacci(3)
- Fibonacci(4) = Fibonacci(3) + Fibonacci(2) ← Here, Fibonacci(3) repeats!
- Fibonacci(3) = Fibonacci(2) + Fibonacci(1)

Total unique subproblems: only 6 (Fibonacci(0) through Fibonacci(5)), but the tree has 15 nodes.

**Fix**: Store results of subproblems. When you see the same subproblem again, return the cached result.

#### Memoization (Top-Down DP)
```
def fib_memo(n, memo={}):
    if n in memo:
        return memo[n]
    if n <= 1:
        return n
    memo[n] = fib_memo(n-1, memo) + fib_memo(n-2, memo)
    return memo[n]
```

Time: O(n) — Each unique subproblem solved once.  
Space: O(n) — Memo dictionary + recursion stack.

**Mental model:** "Before computing, check if I've seen this before. If yes, return cached. If no, compute, cache, return."

#### Common Variations
1. **Climbing Stairs**: Ways to reach step n if you can climb 1 or 2 steps.
   - Recursive: `ways(n) = ways(n-1) + ways(n-2)`
   - Same structure as Fibonacci; memoization helps identically.

2. **Coin Change (Memoization Variant)**: Min coins to make amount n.
   - Recursive: `min_coins(n) = 1 + min(min_coins(n - coin) for coin in coins)`
   - Multiple recursive branches (one per coin); memoization saves exponential redundancy.

---

### **DAY 2: TABULATION & 1D DP ARRAYS**

#### Core Insight
**Fill a table iteratively from base cases to the goal, avoiding recursion overhead.**

Instead of recursion, create an array `dp` where `dp[i]` stores the optimal answer for subproblem i. Fill from left to right (or right to left, depending on dependencies).

#### Pattern: 1D DP
```
dp[0] = base_case_1
dp[1] = base_case_2
for i in range(2, n+1):
    dp[i] = recurrence(dp[i-1], dp[i-2], ...)
return dp[n]
```

#### Classic 1D Problems

**1. Fibonacci (Tabulation)**
```
dp[0] = 0, dp[1] = 1
for i in 2..n:
    dp[i] = dp[i-1] + dp[i-2]
return dp[n]
```
Time: O(n), Space: O(n)

**2. House Robber (Non-Adjacent Sum)**
- Problem: Rob houses in a row. Can't rob adjacent. Maximize money.
- Subproblem: "Max money robbing houses 0..i?"
- Recurrence: `dp[i] = max(rob[i] + dp[i-2], dp[i-1])`
  - Rob house i: gain nums[i] + best from 0..i-2
  - Skip house i: take best from 0..i-1

**3. Coin Change (Min Coins)**
- Problem: Given coins and amount, find minimum coins to make amount.
- Subproblem: "Min coins to make amount i?"
- Recurrence: `dp[i] = min(dp[i - coin] + 1 for coin in coins if coin <= i)`
- Base case: `dp[0] = 0` (zero coins for amount 0)

**4. 0/1 Knapsack (Capacity-Constrained Selection)**
- Problem: Capacity W, items with weight & value. Max value ≤ W?
- Subproblem: "Max value with capacity w?"
- Recurrence: `dp[w] = max(dp[w], value[i] + dp[w - weight[i]])`
- Iteration order: Backward (w = capacity down to weight[i]) to avoid using the same item twice.

#### Key Insight: Iteration Order
- **Forward (left-to-right):** Use when dp[i] depends on dp[j] where j < i.
- **Backward (right-to-left):** Use in knapsack to avoid re-using items (1D variant of 2D DP trick).

#### Space Optimization: Rolling Variables
Many 1D problems only need the last 1–2 values. Replace the full array with individual variables.

**Example: Climbing Stairs optimized from O(n) to O(1)**
```
prev2, prev1 = 1, 1  // dp[0], dp[1]
for i in 2..n:
    curr = prev1 + prev2
    prev2, prev1 = prev1, curr
return prev1
```

---

### **DAY 3: 2D DP & GRIDS**

#### Core Insight
**Extend 1D patterns to 2D: dp[i][j] depends on dp[i-1][j], dp[i][j-1], or other neighbors.**

#### Pattern: 2D DP
```
dp[0][0] = base_case
// Initialize first row and column
for i in 0..m:
    dp[i][0] = f(i)
for j in 0..n:
    dp[0][j] = g(j)
// Fill rest
for i in 1..m:
    for j in 1..n:
        dp[i][j] = recurrence(dp[i-1][j], dp[i][j-1], ...)
return dp[m][n]
```

#### Classic 2D Problems

**1. Unique Paths in Grid**
- Problem: m×n grid, move right or down, count paths.
- Subproblem: "Paths to reach cell (i, j)?"
- Recurrence: `dp[i][j] = dp[i-1][j] + dp[i][j-1]`
  - Can arrive from above or left.
- Base case: `dp[0][j] = 1, dp[i][0] = 1` (one way: all right, or all down)

**2. Edit Distance (Levenshtein)**
- Problem: Transform string s1 to s2 with min operations (insert, delete, replace).
- Subproblem: "Cost to transform s1[0..i] to s2[0..j]?"
- Recurrence:
  - If s1[i-1] == s2[j-1]: `dp[i][j] = dp[i-1][j-1]` (no op needed)
  - Else: `dp[i][j] = 1 + min(dp[i-1][j], dp[i][j-1], dp[i-1][j-1])`
    - Delete from s1: dp[i-1][j]
    - Insert to s1: dp[i][j-1]
    - Replace: dp[i-1][j-1]

**3. Unique Paths II (Obstacles)**
- Variant: Grid has obstacles; can't pass through them.
- Recurrence: `dp[i][j] = 0 if obstacle else (dp[i-1][j] + dp[i][j-1])`

---

### **DAY 4: SEQUENCES & SPECIAL PATTERNS**

#### Core Insight
**String/array sequences often use DP with indices as state: dp[i] or dp[i][j].**

#### Longest Common Subsequence (LCS)
- Problem: Find longest sequence appearing in both strings (not necessarily contiguous).
- Subproblem: "LCS length of s1[0..i] and s2[0..j]?"
- Recurrence:
  - If s1[i-1] == s2[j-1]: `dp[i][j] = 1 + dp[i-1][j-1]`
  - Else: `dp[i][j] = max(dp[i-1][j], dp[i][j-1])`
- Base case: `dp[0][j] = 0, dp[i][0] = 0` (empty string has LCS 0)

**Example:** s1 = "ABCD", s2 = "ACBD"  
LCS = "ABD" (length 3)

#### Longest Increasing Subsequence (LIS)
- Problem: Find longest strictly increasing subsequence in an array.
- Subproblem: "LIS length ending at index i?"
- Recurrence: `dp[i] = max(dp[j] + 1 for j < i where arr[j] < arr[i])`
- Base case: `dp[i] = 1` (every element is an LIS of length 1)
- Time: O(n²)

**Optimization to O(n log n):**
Use binary search on a "tails" array where tails[j] = smallest ending value of any LIS of length j+1.
- For each number, find where it fits via binary search.
- Update or extend the tails array.

**Example:** arr = [10, 9, 2, 5, 3, 7]
- 10: tails = [10]
- 9: tails = [9] (9 < 10, replace)
- 2: tails = [2] (2 < 9, replace)
- 5: tails = [2, 5] (extend)
- 3: tails = [2, 3] (replace 5 with 3)
- 7: tails = [2, 3, 7] (extend)
- LIS length: 3

---

## 🧠 5 KEY COGNITIVE LENSES

### **1. The Recursion Lens**
"What if I solve this recursively? What's the base case? The recurrence?"
- Forces clarity on problem structure.
- Helps spot overlapping subproblems.
- Often easy to understand, slow to run.

### **2. The Memoization Lens**
"What's being recomputed? Can I cache those results?"
- Natural bridge between recursion and DP.
- Good for sparse subproblems.
- No need to worry about fill order.

### **3. The Tabulation Lens**
"How do I fill a table from smaller to larger subproblems?"
- Forces clear iteration order.
- No recursion overhead.
- Must define dependencies carefully.

### **4. The Space Optimization Lens**
"Do I really need the full table? Which old values do I still need?"
- Reveals structure in DP dependencies.
- Cuts memory usage dramatically.
- Classic trick: rolling arrays.

### **5. The Real-World Lens**
"Where does this pattern appear in production?"
- Database query optimization (DP on execution plans).
- Caching and memoization in web services.
- Resource allocation in task scheduling.
- Edit distance in spell-checkers and DNA sequencing.

---

## ⚔️ COMPARATIVE TABLE: WHEN TO USE WHICH APPROACH

| Situation | Memoization | Tabulation | Notes |
|-----------|------------|-----------|-------|
| **Natural recursion** | ✅ Best | ❌ Less natural | Go top-down if recursion is intuitive |
| **Sparse subproblems** | ✅ Efficient | ❌ Wastes space | Only compute needed subproblems |
| **All subproblems needed** | ❌ Extra overhead | ✅ Clean | Tabulation simpler |
| **Avoiding recursion stack** | ❌ Uses stack | ✅ Uses loop | Iteration preferred if depth risk |
| **Debugging** | ✅ Easy traces | ⚖️ Table hard to read | Recursion traces flow naturally |
| **Production code** | ⚖️ Common | ✅ Standard | Tabulation often preferred for clarity |

---

## 📊 COMPLEXITY SUMMARY TABLE

| Problem | State | Time | Space (Memoization) | Space (Tabulation) | Space (Optimized) |
|---------|-------|------|-------------|-------------|----------|
| **Fibonacci** | dp[i] | O(n) | O(n) | O(n) | O(1) |
| **Climbing Stairs** | dp[i] | O(n) | O(n) | O(n) | O(1) |
| **House Robber** | dp[i] | O(n) | O(n) | O(n) | O(1) |
| **Coin Change (Min)** | dp[i] | O(n × m) | O(n) | O(n) | O(n) |
| **0/1 Knapsack** | dp[w] | O(n × W) | O(W) | O(W) | O(W) |
| **Unique Paths** | dp[i][j] | O(m × n) | O(m × n) | O(m × n) | O(n) |
| **Edit Distance** | dp[i][j] | O(m × n) | O(m × n) | O(m × n) | O(n) |
| **LCS** | dp[i][j] | O(m × n) | O(m × n) | O(m × n) | O(n) |
| **LIS (Quadratic)** | dp[i] | O(n²) | O(n) | O(n) | O(n) |
| **LIS (Optimized)** | helper[] | O(n log n) | O(n) | O(n) | O(n) |

---

## 🔑 7 KEY INSIGHTS TO INTERNALIZE

### **Insight 1: Optimal Substructure**
If the optimal solution to problem n uses optimal solutions to smaller problems, DP applies.  
**Test**: Can you express `f(n) = f(smaller) OP g(smaller)` cleanly?

### **Insight 2: Recurrence is Everything**
The recurrence relation **is** the algorithm. Get it right, and the code writes itself.  
**Test**: Can you write the recurrence in one line without ambiguity?

### **Insight 3: Base Cases Matter**
Wrong base cases corrupt the entire table. Verify manually for n=0, n=1.  
**Test**: Can you hand-trace dp[0..3] and match expected answers?

### **Insight 4: State Definition Determines Difficulty**
A poor state definition makes the recurrence hard to express. A good state makes it trivial.  
**Test**: If the recurrence feels awkward, re-think the state.

### **Insight 5: Iteration Order is Critical**
Tabulation must fill in an order such that dependencies are satisfied before use.  
**Test**: For each cell, are all its dependencies already filled?

### **Insight 6: Space Optimization is Structural**
You can reduce space only if you understand which old values are never reused.  
**Test**: Can you identify the "window" of state you need to keep?

### **Insight 7: DP ≠ Memorization with Recursion**
DP is about **problem structure**, not just caching. A memoized solution isn't DP if there's no overlapping subproblems.  
**Test**: Are you actually solving overlapping subproblems? Or is each call unique?

---

## ❌ COMMON MISCONCEPTIONS CORRECTED

| Misconception | Reality |
|---|---|
| **"DP is just recursion with caching."** | DP is a **problem-solving paradigm**. Memoization is one technique. |
| **"DP always means coding a table."** | DP can be memoization (top-down, recursive) or tabulation (bottom-up, iterative). |
| **"I must optimize to O(1) space."** | Sometimes O(n) space is acceptable. Optimize only when necessary. |
| **"DP solves any problem with overlapping subproblems."** | You also need **optimal substructure**. Overlapping subproblems alone isn't enough. |
| **"I should always fill the table left-to-right."** | Iteration order depends on dependencies. Sometimes right-to-left or column-first is needed. |
| **"Once I fill the table, I'm done."** | You still must **extract the answer** correctly (dp[n][m], max(dp[i]), etc.). |
| **"DP is harder than greedy."** | DP is **harder to design** but guaranteed correct if structure permits. Greedy is **easier to code** but harder to verify. |

---

## 🎯 DECISION FLOWCHART (When to Use DP)

```
Problem: Find optimal solution?
├─ YES → Has optimal substructure? (Optimal solution = f(optimal subsolutions))
│  ├─ YES → Has overlapping subproblems? (Same subproblem solved multiple times)
│  │  ├─ YES → DP is suitable! ✓
│  │  └─ NO → Greedy or divide-and-conquer may work
│  └─ NO → Greedy, divide-and-conquer, or brute-force
└─ NO → Not an optimization problem; may need heuristics or approximation
```

---

## 🔗 MENTAL MODEL: THE DP JOURNEY

**Step 1: Recognize the problem structure**
"This feels like an optimization problem. Can I break it into smaller versions?"

**Step 2: Define the state**
"What information uniquely identifies a subproblem?"

**Step 3: Write the recurrence**
"How does the optimal solution for state S relate to optimal solutions for smaller states?"

**Step 4: Identify base cases**
"What's the answer for the smallest subproblem (n=0, empty array, single element)?"

**Step 5: Choose a method**
"Top-down (memoization) or bottom-up (tabulation)? Memoization if recursion is natural; tabulation if iteration order is clear."

**Step 6: Implement and verify**
"Code it up. Hand-trace on a small example. Check base cases and boundaries."

**Step 7: Optimize if needed**
"Can I reduce space? Use rolling arrays or clever indexing?"

---

## 📈 PROGRESSION OF COMPLEXITY

| Level | Pattern | Examples | Difficulty |
|-------|---------|----------|-----------|
| **L1: Basic 1D** | dp[i] depends on dp[i-1], dp[i-2] | Fibonacci, climbing stairs | Easy |
| **L2: 1D with Multiple Choices** | dp[i] = min/max over multiple options | Coin change, house robber | Medium |
| **L3: Knapsack Variants** | Multi-dimensional state (item, capacity) | 0/1 knapsack, bounded knapsack | Medium |
| **L4: 2D Grid Problems** | dp[i][j] with two spatial dimensions | Unique paths, edit distance | Medium-Hard |
| **L5: Sequence Problems** | Comparing two sequences; LCS, LIS | LCS, LIS, string matching | Hard |
| **L6: Mixed Structures** | DP on trees, DAGs, or state machines | Tree DP, DAG shortest path | Hard |
| **L7: Optimization & SoS** | Combining DP with advanced tricks | SoS DP, profile DP, convex hull trick | Very Hard |

---

## 🧬 WEEK 10 IN ONE TABLE

| Day | Core Topic | Key Problem | Mental Model |
|-----|-----------|-----------|--|
| **1** | Overlapping Subproblems | Fibonacci naive vs memo | Recursion tree has duplicate nodes; memoization cuts redundancy |
| **2** | 1D DP & Tabulation | Climbing stairs | Fill array left-to-right using recurrence; O(n) time, O(n) space |
| **3** | Space Optimization | Climbing stairs O(1) space | Only need last two values; use rolling variables |
| **4** | 2D DP & Grids | Edit distance | Fill m×n table; dp[i][j] depends on three neighbors |
| **5** | Sequences | LIS & LCS | Track indices into two strings/arrays; build solution bottom-up |

---

**Status:** ✅ Week 10 Summary Complete — All key concepts documented with examples, tables, and mental models.
