# ✅ WEEK 11 DAILY PROGRESS CHECKLIST

**Week:** 11 | **Purpose:** Track daily progress and problem-solving skills  
**Use:** Check off as you complete items, identify weak areas

---

## 📅 MONDAY - DAY 1: DP ON TREES

### Concept Mastery

**Understanding Tree DP Framework**
- [ ] Can explain post-order traversal and why it's necessary
- [ ] Can define two-state DP (include/exclude) for a problem
- [ ] Can identify when tree structure guarantees O(n) solution
- [ ] Can explain how tree structure prevents infinite loops
- [ ] Can manually trace DP values on a 5-node tree

**Maximum Independent Set**
- [ ] Understand dp[node][0] = exclude, dp[node][1] = include
- [ ] Can compute transitions: exclude takes sum of max(0,1); include takes value + sum of 0's
- [ ] Can manually trace on provided example (value 10, 5, 3, 2, 4)
- [ ] Can identify base case (leaf nodes)
- [ ] Can reconstruct actual set (not just value)

**Tree Diameter via DP**
- [ ] Understand why diameter = longest path between any two nodes
- [ ] Can compute longest-down path from each node
- [ ] Understand formula: diameter = max(left_depth + right_depth + 1)
- [ ] Can trace on example tree (verify diameter = expected value)
- [ ] Know that time is O(n), space is O(h)

**Subtree Computation**
- [ ] Can compute subtree sum (post-order)
- [ ] Can compute subtree size
- [ ] Understand why post-order is natural for this

**Tree Coloring with k Colors**
- [ ] Understand constraint: adjacent nodes different colors
- [ ] Can define state: dp[node][color] = count of ways
- [ ] Can write transition: sum over child color assignments
- [ ] Can compute for small example (3 nodes, 2 colors)

**Tree Rerooting (Advanced)**
- [ ] Understand two-pass approach
- [ ] Can explain first pass (from original root)
- [ ] Can explain second pass (reroot logic)
- [ ] Know when to use (change root problems)

### Implementation Practice

**Coding Exercises (15 min each)**
- [ ] Maximum independent set: implement from scratch
- [ ] Test on provided examples
- [ ] Verify output matches expected values

- [ ] Tree diameter: implement from scratch
- [ ] Test on tree with multiple configurations
- [ ] Verify diameter is correct

- [ ] Subtree sum: implement post-order solution
- [ ] Test on binary and non-binary trees
- [ ] Verify sums are correct

### Problem Solving (20 min per problem)

**Easy (Warm-up)**
- [ ] LeetCode 337: House Robber III
  - State: dp[node][0/1]
  - Time taken: __ min | Status: ✅ / ⚠️ / ❌
  - Issue (if any): ________________

**Medium**
- [ ] LeetCode 543: Diameter of Binary Tree
  - Time taken: __ min | Status: ✅ / ⚠️ / ❌
  - Issue (if any): ________________

- [ ] LeetCode 124: Binary Tree Maximum Path Sum
  - Time taken: __ min | Status: ✅ / ⚠️ / ❌
  - Issue (if any): ________________

### Reflection

**What I Understood Well:**
- ___________________________________

**What Needs More Practice:**
- ___________________________________

**Problems to Redo:**
- ___________________________________

**Confidence Level (1-10):** __

---

## 📅 TUESDAY - DAY 2: DP ON DAGS

### Concept Mastery

**DAG Fundamentals**
- [ ] Can define DAG (directed, acyclic)
- [ ] Can verify if graph is DAG (no back edges)
- [ ] Understand why DAGs enable topological ordering
- [ ] Can explain why cycles break DP

**Topological Sort (Kahn's Algorithm)**
- [ ] Understand in-degree and out-degree
- [ ] Can implement Kahn's algorithm from scratch
- [ ] Know that Kahn's detects cycles as side effect
- [ ] Time: O(V + E), Space: O(V)
- [ ] Can test on example DAG (5-6 nodes)

**Longest Path in DAG**
- [ ] Understand state: dp[node] = longest path starting at node
- [ ] Can compute topological order first
- [ ] Can write transitions in topological order
- [ ] Know why processing order matters (ALL predecessors first)
- [ ] Time: O(V + E)

**Critical Path Method (CPM)**
- [ ] Understand forward pass (earliest times)
- [ ] Understand backward pass (latest times)
- [ ] Can compute slack = latest_start - earliest_start
- [ ] Can identify critical path (slack = 0)
- [ ] Can trace on example project (5+ tasks)

**Shortest Path with Negative Weights**
- [ ] Understand why cycles enable negative infinity
- [ ] Why DAG allows O(V+E) vs Bellman-Ford O(VE)
- [ ] Can apply relaxation in topological order
- [ ] Know conditions for when to use each algorithm

**All Paths in DAG**
- [ ] State: dp[node] = count of paths from node to target
- [ ] Can compute in topological order (from target backward)
- [ ] Understand transition: sum of successor counts

### Implementation Practice

**Coding Exercises (15 min each)**
- [ ] Implement Kahn's algorithm with cycle detection
- [ ] Test on cyclic and acyclic graphs
- [ ] Verify output order is valid topological sort

- [ ] Implement longest path in DAG
- [ ] Verify topo sort before running DP
- [ ] Test on provided example

- [ ] Implement CPM (forward and backward passes)
- [ ] Verify slack computation
- [ ] Identify critical tasks

### Problem Solving (20 min per problem)

**Easy**
- [ ] LeetCode 207: Course Schedule (Cycle detection)
  - Time taken: __ min | Status: ✅ / ⚠️ / ❌

**Medium**
- [ ] LeetCode 329: Longest Increasing Path in Matrix (Grid as DAG)
  - Time taken: __ min | Status: ✅ / ⚠️ / ❌

- [ ] LeetCode 1203: Sort Items by Groups Respecting Dependencies (CPM variant)
  - Time taken: __ min | Status: ✅ / ⚠️ / ❌

### Debugging Checklist

When DAG DP is wrong, verify in this order:
- [ ] Graph is actually a DAG (no cycles)
- [ ] Topological sort computed correctly
- [ ] Processing nodes in topological order in DP
- [ ] Transitions accessing only computed predecessors
- [ ] Base case (source node) initialized correctly
- [ ] Answer extracted from correct node

### Reflection

**What I Understood Well:**
- ___________________________________

**What Needs More Practice:**
- ___________________________________

**Confidence Level (1-10):** __

---

## 📅 WEDNESDAY - DAY 3: BITMASK & SUBSET DP

### Concept Mastery

**Bitmask Representation**
- [ ] Can convert subset to bitmask mentally (e.g., {0,2} → 0101)
- [ ] Can enumerate all 2^n masks for n=4 (verify count = 16)
- [ ] Understand bit i represents element i
- [ ] Can iterate masks efficiently (0 to 2^n-1)

**Bit Manipulation Operations**
- [ ] Check bit: `(mask & (1 << i)) != 0`
- [ ] Set bit: `mask | (1 << i)`
- [ ] Clear bit: `mask & ~(1 << i)`
- [ ] Toggle bit: `mask ^ (1 << i)`
- [ ] Count set bits: `__builtin_popcount(mask)` or equivalent
- [ ] Can test all 8 operations on paper

**TSP via Bitmask DP**
- [ ] Understand why TSP naive is O(n!) (permutations)
- [ ] State: dp[mask][last] = min cost visiting mask, ending at last
- [ ] Base case: dp[1][0] = 0 (start at city 0)
- [ ] Transition: try all unvisited cities as next
- [ ] Answer: min(dp[fullmask][c] + cost[c][0] for all c)
- [ ] Time: O(2^n × n²), Space: O(2^n × n)

**Feasibility Boundary**
- [ ] n ≤ 15: Can definitely solve (< 1 second)
- [ ] 15 < n ≤ 20: Can solve but tight on time
- [ ] 20 < n ≤ 25: Possible with heavy optimization
- [ ] n > 25: Infeasible, need approximation

**Subset Sum DP**
- [ ] Count subsets with target sum
- [ ] State: dp[sum] = count of ways
- [ ] Iteration: backward (avoid using item twice)
- [ ] Transition: dp[sum] += dp[sum - item]

**Submask Iteration**
- [ ] For each mask, iterate all submasks in O(3^n) total
- [ ] Pattern: `for (int sub = mask; sub > 0; sub = (sub-1) & mask)`
- [ ] Use case: "subset of subset" problems

### Implementation Practice

**Bitmask Drills**
- [ ] For n=4, enumerate all 16 masks and their element sets
- [ ] Verify each bit operation on paper
- [ ] Code a bit utility library (set, check, clear, etc.)

**Coding Exercises (20 min each)**
- [ ] Implement TSP from scratch
  - [ ] Set up dp table (2D array)
  - [ ] Initialize base case
  - [ ] Implement transitions (two nested loops)
  - [ ] Extract answer (return minimum)
  - [ ] Test on 4-city example

- [ ] Implement subset sum counter
  - [ ] 1D dp array
  - [ ] Backward iteration
  - [ ] Test: count subsets of {1,2,3} summing to 3 (answer=2)

### Problem Solving (20 min per problem)

**Easy**
- [ ] LeetCode 526: Beautiful Arrangement (Permutation DP)
  - Time taken: __ min | Status: ✅ / ⚠️ / ❌

**Medium**
- [ ] LeetCode 943: Shortest Superstring (TSP variant)
  - Time taken: __ min | Status: ✅ / ⚠️ / ❌

- [ ] LeetCode 698: Partition to K Equal Subsets (Bitmask DP)
  - Time taken: __ min | Status: ✅ / ⚠️ / ❌

### Bitmask Pitfalls Checklist

When bitmask DP is wrong, check:
- [ ] Bitmask operations correct (test on paper)
- [ ] Transitions trying correct candidates
- [ ] Loop bounds correct (0 to 2^n-1)
- [ ] Not using same element twice
- [ ] Answer extraction correct (all cities visited?)

### Reflection

**What I Understood Well:**
- ___________________________________

**Bitmask Operations I'm Unsure About:**
- ___________________________________

**Confidence Level (1-10):** __

---

## 📅 THURSDAY - DAY 4: STATE COMPRESSION & OPTIMIZATIONS [OPTIONAL]

### Concept Mastery

**Space Optimization via Sliding Window**
- [ ] Understand 2D → 1D transformation
- [ ] Example: LCS (m×n → n space)
- [ ] Example: Edit distance (m×n → n space)
- [ ] Know when sliding window applies (only need previous row/col)

**Memoization vs Tabulation**
- [ ] Memoization (top-down): compute only reachable states
- [ ] Tabulation (bottom-up): compute all states
- [ ] When memoization is faster (sparse state space)
- [ ] When tabulation is faster (dense state space, better cache)

**Dimension Reduction**
- [ ] Example: Can 3D knapsack reduce to 2D? (answer: no, W is always needed)
- [ ] Identify unnecessary dimensions
- [ ] Verify reduced state still captures all information

**Pruning & Branch-and-Bound**
- [ ] Compute upper bound for remaining search
- [ ] Skip branches where current + upper < best
- [ ] Tightness of bounds determines speedup (10-100×)

### Implementation Practice

**Optimization Exercises (15 min each)**
- [ ] Implement LCS both 2D and 1D
  - [ ] Verify both give same answer
  - [ ] Measure space used (2D vs 1D)
  - [ ] Time the 1D version (should be same asymptotically)

- [ ] Implement 0/1 knapsack with pruning
  - [ ] Implement without pruning first
  - [ ] Add upper bound computation
  - [ ] Measure speedup (target: 10-50×)

### Optional Coding Problems

- [ ] LeetCode 87: Scramble String (string DP)
  - Optimize space if needed
  - Time taken: __ min | Status: ✅ / ⚠️ / ❌

### Reflection

**Which optimization is most useful?**
- ___________________________________

**When should I use sliding window vs memoization?**
- ___________________________________

---

## 📅 FRIDAY - DAY 5: MIXED DP PROBLEMS [OPTIONAL]

### Problem Recognition

**Three Key Questions (for each problem)**
- [ ] Are there overlapping subproblems?
- [ ] Is there optimal substructure?
- [ ] Is state space manageable?

**Problem Classes (Identify which class)**
- [ ] Optimization (maximize/minimize)
- [ ] Counting (count ways)
- [ ] Decision (is feasible?)
- [ ] Ordering (find sequence)
- [ ] Graph (paths, substructures)
- [ ] Hybrid (multi-concept)

### Five-Step Problem-Solving Strategy

For each novel problem, complete in 20-25 minutes:

**Step 1: Recognize Subproblems (1-2 min)**
- [ ] What are the recursive subproblems?
- [ ] Do smaller inputs lead to larger answers?

**Step 2: Define State (1-2 min)**
- [ ] What parameters fully define a subproblem?
- [ ] Write: dp[___] = answer for this subproblem

**Step 3: Base Cases (1 min)**
- [ ] What's the smallest problem (usually n=0 or n=1)?
- [ ] Can I compute the answer directly?

**Step 4: Transitions (2-3 min)**
- [ ] How do I combine subproblem answers?
- [ ] Write recurrence relation

**Step 5: Implementation & Verify (5-10 min)**
- [ ] Code the solution (cleanly, not fast)
- [ ] Test on example by hand
- [ ] Verify output is correct

### Problem Practice (20 min per problem)

**Novel/Hybrid Problems**
- [ ] LeetCode 940: Distinct Subsequences II (string DP)
  - Time taken: __ min | Status: ✅ / ⚠️ / ❌
  - Class: ____________
  - State: ____________

- [ ] LeetCode 1140: Stone Game II (game theory DP)
  - Time taken: __ min | Status: ✅ / ⚠️ / ❌
  - Class: ____________
  - State: ____________

- [ ] LeetCode 1312: Minimum Insertion Steps to Make a String Palindrome
  - Time taken: __ min | Status: ✅ / ⚠️ / ❌
  - Class: ____________
  - State: ____________

### Reflection

**DP Problems I Found Easy:**
- ___________________________________

**DP Problems I Found Hard:**
- ___________________________________

**Common Mistakes I Made:**
- ___________________________________

---

## 📊 WEEK 11 OVERALL ASSESSMENT

### Skill Levels (Rate 1-5)

| Skill | Mon | Tue | Wed | Thu | Fri | Final |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| Tree DP | _ | _ | _ | _ | _ | _ |
| DAG DP | _ | _ | _ | _ | _ | _ |
| Bitmask DP | _ | _ | _ | _ | _ | _ |
| State Design | _ | _ | _ | _ | _ | _ |
| Recognition | _ | _ | _ | _ | _ | _ |
| Implementation | _ | _ | _ | _ | _ | _ |
| Optimization | _ | _ | _ | _ | _ | _ |
| **Overall** | _ | _ | _ | _ | _ | _ |

### Problems Solved Summary

| Category | Count | Success Rate |
| :--- | :--- | :--- |
| Easy | _ / 5 | _% |
| Medium | _ / 10 | _% |
| Hard | _ / 5 | _% |
| **Total** | _ / 20 | _% |

### Problem Categories Mastered
- [ ] Tree DP (3+ problems)
- [ ] DAG DP (3+ problems)
- [ ] Bitmask DP (3+ problems)
- [ ] Mixed DP (3+ problems)
- [ ] Optimization (2+ problems)

### Areas to Revisit

**Weak Areas:**
- Concept: _________________ | Practice: __ more problems
- Concept: _________________ | Practice: __ more problems

**Confidence Gaps:**
- Topic: _________________ | Action: _________________

### Post-Week 11 Goals

**Before Moving to Week 12:**
- [ ] Can solve novel tree DP in 15 min
- [ ] Can solve novel DAG DP in 15 min
- [ ] Can solve novel bitmask DP in 20 min
- [ ] Can recognize DP problems instantly
- [ ] Can explain DP solutions to others

**Interview Preparation:**
- [ ] Practiced explaining state designs
- [ ] Solved 3 "tricks I didn't know" problems
- [ ] Can code under time pressure (20 min)
- [ ] Can optimize from naive solution

---

## 📈 WEEK 11 SUCCESS CRITERIA

**You succeeded if:**
- ✅ Completed 15+ problems (3+ per day)
- ✅ Achieved >70% success rate on first attempt
- ✅ Can explain DP concept to someone else
- ✅ Can solve novel problem under 25 minutes
- ✅ Final skill level ≥ 3/5 on all categories

**You exceeded expectations if:**
- 🌟 Completed 20+ problems with >80% success
- 🌟 Can solve hard problems in 20 minutes
- 🌟 Final skill level ≥ 4/5 on all categories
- 🌟 Can teach DP concepts to peers

---

**Checklist Status:** Ready to track daily progress  
**Last Updated:** January 26, 2026

