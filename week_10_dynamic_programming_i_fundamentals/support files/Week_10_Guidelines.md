# 🎯 Week_10_Guidelines.md

**Week:** 10 | **Phase:** C (Trees, Graphs & Dynamic Programming)  
**Theme:** Dynamic Programming I: Building Intuition from Recursion to Tables  
**Difficulty:** 🟡 Intermediate to 🔴 Advanced  
**Status:** ✅ Complete Learning Framework for Week 10

---

## 📋 WEEK 10 OVERVIEW

Week 10 is your **gateway into dynamic programming (DP)**, one of the most powerful algorithmic paradigms. This week focuses on **fundamentals**: recognizing overlapping subproblems, understanding the spectrum from recursion to memoization to tabulation, and solving problems on 1D arrays, 2D grids, and sequences.

By the end of this week, you will have internalized the mental model that **DP = Recursion + Memoization** and can transform exponential recursive solutions into polynomial-time algorithms.

---

## 🎯 LEARNING OBJECTIVES

*By the end of this week, you will be able to:*

1. **🧠 Recognize Overlapping Subproblems** — Identify when a recursive solution solves the same subproblem multiple times.

2. **🔁 Distinguish Top-Down vs Bottom-Up** — Understand memoization (top-down, recursive) vs tabulation (bottom-up, iterative) and choose the right approach.

3. **📊 Design 1D DP Solutions** — Solve classic problems: Fibonacci, climbing stairs, house robber, coin change using 1D state arrays.

4. **🧩 Extend to 2D & Grid Problems** — Handle grid-based DP: unique paths, edit distance, and matrix chains (conceptually).

5. **📈 Analyze DP Sequences** — Solve longest increasing subsequence (LIS) and longest common subsequence (LCS) problems.

6. **⚖️ Evaluate Time–Space Trade-Offs** — Optimize space: rolling arrays, state compression, and when O(n) space is necessary.

7. **🏭 Connect to Real Systems** — See DP patterns in caching, resource allocation, and scheduling (high-level).

---

## 📅 DAY-BY-DAY CONCEPT OVERVIEW

### **Day 1: Recursion + Memoization (Overlapping Subproblems)**

**Core Concepts:**
- Recursion tree for Fibonacci and climbing stairs.
- Identifying redundant computations (subproblems solved multiple times).
- Memoization as a cache for recursive results.
- Time complexity transformation: O(2^n) → O(n).

**Mental Model:** Think of memoization as "remembering answers you've already computed." Before solving a subproblem, check the memo dictionary. If found, return immediately. Otherwise, compute, cache, and return.

**Key Problems:**
- Fibonacci (naive recursion, then memoization).
- Climbing stairs (transition: f(n) = f(n-1) + f(n-2)).
- Recognize patterns: "Can we reach the end?" "How many ways to reach?"

---

### **Day 2: 1D DP Arrays (Tabulation & Optimization)**

**Core Concepts:**
- Bottom-up DP: fill a table iteratively from base cases to the goal.
- State definition: dp[i] = "optimal solution for subproblem i."
- Recurrence relation: how dp[i] depends on prior states.
- Space optimization: reducing from O(n) to O(1) using rolling variables.

**Mental Model:** Build a table where each cell computes its answer using only values from earlier cells. Progress systematically: dp[0] → dp[1] → ... → dp[n].

**Key Problems:**
- House robber: dp[i] = max(nums[i] + dp[i-2], dp[i-1]).
- Coin change (min coins): dp[i] = min over all coins c of (1 + dp[i-c]).
- 0/1 Knapsack: dp[w] = max(current, value[i] + dp[w - weight[i]]).

---

### **Day 3: 2D DP & Grids**

**Core Concepts:**
- Extending 1D patterns to 2D: dp[i][j] = f(dp[i-1][j], dp[i][j-1], ...).
- Grid navigation: unique paths with obstacles.
- Edit distance: transforming strings with insertions, deletions, replacements.
- Cell dependencies: which prior cells matter for dp[i][j].

**Mental Model:** Imagine filling a table cell by cell. Each cell's value depends on neighbors (usually above and left). The dependencies form a DAG (directed acyclic graph); process cells in topological order.

**Key Problems:**
- Unique paths: dp[i][j] = dp[i-1][j] + dp[i][j-1].
- Edit distance: dp[i][j] based on character match and three operations.

---

### **Day 4: Sequences & Special Patterns**

**Core Concepts:**
- Longest common subsequence (LCS): matching characters in two strings.
- Longest increasing subsequence (LIS): finding monotone chains.
- O(n²) DP for LIS vs O(n log n) binary search optimization.
- State flexibility: different ways to define the same problem.

**Mental Model:** For sequences, the DP state often tracks indices. dp[i] = "best solution considering first i elements" or dp[i][j] = "best solution for s1[0..i] and s2[0..j]."

**Key Problems:**
- LCS: dp[i][j] = dp[i-1][j-1] + 1 if match, else max(dp[i-1][j], dp[i][j-1]).
- LIS: dp[i] = max(dp[j] + 1) for all j < i where arr[j] < arr[i].

---

## 📊 LEARNING METHODOLOGY

### **Phase 1: Understand the Problem (1 day)**
- Study the problem statement.
- Trace through a **simple example by hand** (Fibonacci up to n=4, array of length 3, grid 2×2).
- Ask: "What's the structure? What does a subproblem look like?"

### **Phase 2: Recognize Recursion (1 day)**
- Write the **naive recursive solution** (even if slow).
- Draw the **recursion tree** for a small input. Count duplicate nodes.
- Identify the **recurrence relation**: f(n) = ?
- Estimate complexity: How many unique subproblems? How deep is the tree?

### **Phase 3: Memoize (1 day)**
- Add a **dictionary** or **array cache** to the recursive solution.
- Trace through the memoized version with your example.
- Notice how the recursion tree shrinks: repeated subproblems return cached values.

### **Phase 4: Tabulate (2 days)**
- **Flip the recursion**: start from base cases, build up.
- Create a **table** (1D array for simple, 2D for grid/sequence).
- **Fill the table** in the correct order (usually left-to-right, bottom-to-top).
- Trace through the table filling; verify each cell's recurrence.

### **Phase 5: Optimize & Generalize (1-2 days)**
- **Space optimization**: Can we reduce dimensions? Use rolling arrays?
- **Time optimization**: Are there mathematical insights (e.g., matrix exponentiation for Fibonacci)?
- **Generalize**: How does this pattern extend to related problems?

---

## 📌 KEY DECISION FRAMEWORK

### **When to Use DP:**
- ✅ **Overlapping subproblems** — Same subproblem solved multiple times.
- ✅ **Optimal substructure** — Optimal solution builds from optimal sub-solutions.
- ✅ **Exponential naive recursion** — Brute force is infeasible; DP polynomializes it.

### **When NOT to Use DP:**
- 🛑 **No overlapping subproblems** — Each subproblem unique; memoization wastes space.
- 🛑 **Greedy works** — Greedy choice leads to global optimum (check via exchange argument).
- 🛑 **Polynomial backtracking exists** — Some problems are inherently hard (NP-complete); DP doesn't help.

---

## ⚠️ COMMON PITFALLS & HOW TO AVOID THEM

### **Pitfall 1: Wrong Recurrence Relation**
**Symptom:** Your DP table fills, but answers are wrong.  
**Root Cause:** The recurrence doesn't match the problem's logic.  
**Fix:**
- Write the **recursive function first** and verify it works (slow but correct).
- Trace the recurrence **by hand** on a small example.
- Check: Does dp[i] correctly depend on smaller subproblems?

**Example:**  
❌ Climbing stairs: `dp[i] = dp[i-1] + dp[i-3]` (wrong; ignores i-2 case)  
✅ Correct: `dp[i] = dp[i-1] + dp[i-2]` (both 1-step and 2-step options)

---

### **Pitfall 2: Off-by-One Errors in Indexing**
**Symptom:** Your table fills correctly, but the final answer is off.  
**Root Cause:** Confusion between dp[i] meaning "up to i" vs "starting from i" vs "exactly i."  
**Fix:**
- **Define clearly**: dp[i] = "solution for input of size i" or dp[i] = "solution using first i items."
- Draw a table with small numbers (n=2 or n=3) and verify indices match your definition.
- Use **explicit boundary conditions**: dp[0] = ?, dp[1] = ?, etc.

**Example:**  
Coin change: Is dp[0] = 0 (zero coins for amount 0) or dp[0] = 1 (one way to make 0)?  
**Answer:** dp[0] = 0 (zero coins) for min coins, but dp[0] = 1 for counting ways.

---

### **Pitfall 3: Incorrect State Definition**
**Symptom:** Recurrence is hard to express or feels "missing information."  
**Root Cause:** DP state doesn't capture enough to make optimal decisions.  
**Fix:**
- Think: "What information do I need at this point to make the right choice?"
- Define state to include **all relevant constraints**.
- If you need to "look back" or "check previous decisions," you may need a larger state.

**Example:**  
House robber: `dp[i] = max value robbing houses 0..i`  
Seems simple, but you need to track **whether house i was robbed** to decide house i+1.  
**Better state:** Track separately what dp[i] means when you rob vs skip house i (or track both in the recurrence).

---

### **Pitfall 4: Wrong Initialization**
**Symptom:** Table fills, but base cases are wrong; everything downstream is wrong.  
**Root Cause:** Base cases don't match problem definition or recurrence assumptions.  
**Fix:**
- Verify base cases **manually**: What's the answer for the smallest input?
- Ensure **all cells** the recurrence depends on are initialized before use.
- Use **sentinel values** (e.g., -∞ for maximization, +∞ for minimization) for impossible states.

**Example:**  
Coin change minimum coins:  
❌ `dp[0] = 0` (OK), but dp[1..amount] are uninitialized (may use 0 instead of ∞)  
✅ `dp[0] = 0`, then `for i in 1..amount: dp[i] = INF` before recurrence

---

### **Pitfall 5: Memoization with Mutable Arguments**
**Symptom:** Memoization cache doesn't work; answers wrong or algorithm slow.  
**Root Cause:** Using mutable objects (lists, sets) as cache keys; they hash differently even if equal.  
**Fix:**
- Use **immutable types** as cache keys: tuples, frozen sets, integers, strings.
- In many languages, convert mutable arguments to immutable before caching.
- Use a `@functools.lru_cache` decorator in Python (works for immutable arguments).

**Example:**  
❌ `memo[(arr, idx)]` where arr is a list (unhashable)  
✅ `memo[(tuple(arr), idx)]` converts list to immutable tuple

---

### **Pitfall 6: Not Recognizing Space Optimization Opportunities**
**Symptom:** Your solution is O(n) or O(n²) space; memory limit exceeded or inefficient.  
**Root Cause:** DP cells far in the past are never accessed again; no need to store the full table.  
**Fix:**
- **Analyze dependencies**: For dp[i], which prior indices does it reference?
- If only dp[i-1] and dp[i-2] matter, use **two rolling variables** → O(1) space.
- If a full row/column is needed, keep only two rows → O(n) space instead of O(n²).

**Example:**  
Fibonacci: dp[i] only needs dp[i-1] and dp[i-2].  
❌ `dp = [0] * (n+1)` → O(n) space  
✅ `prev2, prev1 = 0, 1; ... prev2, prev1 = prev1, curr` → O(1) space

---

### **Pitfall 7: Confusing Counting vs Maximization**
**Symptom:** Recurrence looks right, but answers don't match expectations.  
**Root Cause:** Mixing "count combinations" with "find optimal choice" logic.  
**Fix:**
- **Coin change min coins**: dp[i] = min over choices (maximization logic for min).
- **Coin change ways**: dp[i] += dp[i - coin] for each coin (summation logic).
- Use **max/min operations** for optimization; use **+= operations** for counting.

**Example:**  
❌ Counting ways to make amount with coins:  
```
dp[amount] = 1 + dp[amount - coin]  // WRONG: treating as min operation
```
✅ Correct:  
```
dp[amount] += dp[amount - coin]  // Correct: accumulate all ways
```

---

## ⏱️ TIME ALLOCATION STRATEGY

### **Recommended Weekly Schedule (40 hours)**

| Phase | Activity | Duration | Outcome |
|-------|----------|----------|---------|
| **Day 1 Morning** | Understand overlapping subproblems (Fibonacci trace) | 3 hours | Intuition for memoization |
| **Day 1 Afternoon** | Write naive recursion + draw trees; practice recognition | 3 hours | Speed up subproblem spotting |
| **Day 2 Morning** | Implement memoization for 3-4 problems | 4 hours | Comfort with top-down DP |
| **Day 2 Afternoon** | Implement tabulation for same problems | 4 hours | Fluency with bottom-up DP |
| **Day 3 Morning** | 1D DP problems: house robber, coin change, knapsack | 4 hours | Mastery of 1D patterns |
| **Day 3 Afternoon** | Space optimization: rolling arrays, clever tricks | 3 hours | Efficiency optimizations |
| **Day 4 Morning** | 2D DP: unique paths, edit distance | 4 hours | Grid & sequence DP |
| **Day 4 Afternoon** | LCS & LIS problems; O(n²) and O(n log n) variants | 4 hours | Sequence DP patterns |
| **Day 5** | Mixed practice + interview-style problems | 4 hours | Integration & speed |

---

## 📊 WEEKLY CHECKLIST

### **Understanding Phase**
- [ ] Can you explain overlapping subproblems using Fibonacci as example?
- [ ] Can you draw the recursion tree and mark duplicate nodes?
- [ ] Do you understand the difference between top-down and bottom-up?

### **Implementation Phase (1D DP)**
- [ ] Write memoized Fibonacci without looking at notes.
- [ ] Write tabulated Fibonacci and verify it matches recursive version.
- [ ] Solve climbing stairs (both memoized and tabulated).
- [ ] Solve house robber (explain how non-adjacency affects DP state).
- [ ] Solve coin change (min coins version).
- [ ] Solve 0/1 knapsack from scratch.

### **Optimization Phase**
- [ ] Optimize climbing stairs to O(1) space using two variables.
- [ ] Optimize house robber to O(1) space.
- [ ] Explain when you can apply rolling array optimization.
- [ ] Spot optimization opportunities in new problems.

### **2D & Sequences**
- [ ] Solve unique paths on a grid.
- [ ] Solve edit distance and understand the three operations.
- [ ] Solve LCS (longest common subsequence).
- [ ] Implement O(n²) LIS and understand why binary search is faster.
- [ ] Trace through a 2D table filling step-by-step.

### **Integration & Speed**
- [ ] Solve 3 mixed DP problems in < 30 minutes each.
- [ ] Explain the DP approach (state, recurrence, base cases) before coding.
- [ ] Verify your solution on paper before coding.

---

## 🏆 MASTERY SIGNALS

**You've mastered Week 10 when you can:**

1. ✅ **Spot DP problems immediately** — "This is a classic DP pattern" within 30 seconds of reading.
2. ✅ **Design states without trial-and-error** — Define dp[i] clearly and justify it.
3. ✅ **Write recurrences correctly** — No off-by-one errors, no missing cases.
4. ✅ **Optimize naturally** — Spot when space can be reduced; implement rolling arrays smoothly.
5. ✅ **Handle edge cases** — Know what happens for n=0, empty arrays, impossible cases.
6. ✅ **Teach the concept** — Explain DP to someone who's never seen it; they understand.
7. ✅ **Mix paradigms** — Recognize when DP combines with other patterns (DP on grids, DP on strings).

---

## 🎓 KEY CONCEPTS SUMMARY TABLE

| Concept | Definition | When to Use | Example |
|---------|-----------|-------------|---------|
| **Overlapping Subproblems** | Same subproblem solved multiple times | Indicates DP could help; memoization cuts redundancy | Fibonacci f(n) depends on f(n-1) & f(n-2); f(n-1) also computes f(n-2) |
| **Optimal Substructure** | Optimal solution from optimal subsolutions | Problem amenable to DP | Longest increasing subsequence; shortest path |
| **Memoization (Top-Down)** | Recursion + caching results | When recursion is natural; good for sparse subproblems | Backtracking with memoization; game tree evaluation |
| **Tabulation (Bottom-Up)** | Iteratively fill table from base cases | When iteration order is clear; no recursion overhead | Classic DP: coin change, knapsack, Fibonacci |
| **State Definition** | What does dp[i] or dp[i][j] represent? | Must define clearly before coding | dp[i] = max money robbing houses 0..i |
| **Recurrence Relation** | How does dp[i] compute from prior states? | The algorithm's "heart"; must be correct | dp[i] = max(rob[i] + dp[i-2], dp[i-1]) |
| **Base Cases** | Initial values for smallest subproblems | Stopping point for recursion; seeds tabulation | dp[0] = 0 (or 1, depending on problem) |
| **Space Optimization** | Reducing memory via rolling arrays, clever tricks | When full table isn't needed; memory limits tight | Keep only two rows for 2D DP; two variables for 1D |

---

## 📚 RECOMMENDED RESOURCES

### **Textbooks & References**
- *Introduction to Algorithms* (CLRS): Chapters on DP and NP-hardness.
- *Algorithms* (Sedgewick & Wayne): Clear DP explanations.
- MIT OpenCourseWare 6.006: Lectures on DP.

### **Online Platforms**
- **LeetCode**: DP tag; curated tracks for pattern mastery.
- **HackerRank**: DP tutorials and problem sets.
- **GeeksforGeeks**: DP foundational articles.

### **Video Resources**
- StriverG: "DP Problems Explained" series.
- Abdul Bari: "Dynamic Programming" complete playlist.
- MIT 6.006 (OpenCourseWare): Official DP lectures.

---

## 🎯 REFLECTION PROMPTS (Socratic)

At the end of each day, ask yourself:

1. **Can I describe the problem as "find the optimal choice from smaller subproblems"?**
2. **What state information do I need to make that choice?**
3. **What's the recurrence? Can I write it in one line?**
4. **Why is the base case correct? What does it represent?**
5. **Can I optimize space? What's the minimum state I need to track?**
6. **How would I explain this solution to someone unfamiliar with DP?**

---

**Status:** ✅ Week 10 Guidelines Complete — Ready for Implementation

All sections align with v12 narrative-first philosophy and MIT 6.006 DP fundamentals.
