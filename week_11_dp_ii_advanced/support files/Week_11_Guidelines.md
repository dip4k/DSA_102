# 📘 WEEK 11 GUIDELINES & BEST PRACTICES

**Week:** 11 | **Category:** Dynamic Programming II — Trees, DAGs & Advanced  
**Purpose:** Structured guidance for mastering Week 11 content  
**Last Updated:** January 26, 2026

---

## 📌 WEEK 11 OVERVIEW

**Weekly Goal:** Extend DP to complex structures (trees, DAGs, bitmasks) and master advanced optimization patterns.

**Time Allocation:**
- Day 1 (Trees): 120 minutes
- Day 2 (DAGs): 120 minutes
- Day 3 (Bitmask): 120 minutes
- Day 4 (Optimizations): 90 minutes [OPTIONAL]
- Day 5 (Mixed DP): 90 minutes [OPTIONAL]
- **Total Core:** 360 minutes (6 hours) | **With Optional:** 540 minutes (9 hours)

---

## 🎯 LEARNING FRAMEWORK

### Success Indicators for Week 11

By end of Week 11, you should be able to:

- ✅ **Recognize** DP problems on trees despite disguised language
- ✅ **Design** DP states for tree/DAG/bitmask problems in <5 minutes
- ✅ **Implement** tree DP, DAG DP, and bitmask DP in 15-20 minutes
- ✅ **Optimize** DP solutions for space using compression techniques
- ✅ **Identify** when DP is NOT the right approach (red flags)
- ✅ **Connect** DP concepts to real production systems
- ✅ **Solve** interview questions combining DP with other algorithms

---

## 📚 STUDY GUIDELINES BY DAY

### DAY 1: DP ON TREES (120 min)

#### Phase 1: Conceptual Understanding (30 min)
**Read:**
- Week_11_Day_01_DP_On_Trees_Instructional.md (Chapters 1-2)
- Focus on tree DP framework and mental model

**Key Concepts to Grasp:**
- Post-order traversal (children before parent)
- Two-state DP (include/exclude)
- Why tree structure enables O(n) solutions

**Reflection Questions:**
1. Why is post-order traversal necessary for tree DP?
2. How does tree structure eliminate cycles?
3. What's the difference between "exclude" and "don't include"?

#### Phase 2: Deep Dive into Operations (60 min)
**Study Operations (15 min each):**
1. Maximum Independent Set
   - State definition: dp[node][0], dp[node][1]
   - Trace example with actual values
   - Verify transitions logic

2. Tree Diameter
   - Identify why it's O(n) not O(n²)
   - Trace through binary tree example
   - Understand "turning point" insight

3. Subtree Computation & Coloring
   - Simple but essential patterns
   - Foundation for more complex problems

4. Tree Rerooting (advanced)
   - Two-pass algorithm
   - When to use in problems

**Practice During Reading:**
- Draw trees by hand, compute DP values
- Verify answers with provided traces
- Don't just read; compute yourself

#### Phase 3: Practice & Integration (30 min)
**Solve Problems (15 min per problem):**
1. Start with "House Robber III" (LeetCode 337)
   - Simplest tree DP problem
   - Two states directly applicable

2. Then "Tree Diameter" (LeetCode 543)
   - Apply diameter DP directly

3. Finally "Maximum Path Sum" (LeetCode 124)
   - Combines path computation with tree structure

**During Problem Solving:**
- Time yourself (aim for 15-20 min per problem)
- Write state definitions before coding
- Trace through example by hand first

---

### DAY 2: DP ON DAGS (120 min)

#### Phase 1: Understanding Acyclicity (25 min)
**Read:** Instructional.md Chapter 1-2

**Critical Insights:**
- DAGs enable topological ordering (no cycles)
- DP works along topological order
- Violating topo order → wrong answer

**Verify Understanding:**
- Can you identify a DAG vs general graph?
- Why does cycle-free property matter?
- When would DP fail on a cyclic graph?

#### Phase 2: Topological Sort Deep Dive (35 min)
**Study:**
- Kahn's Algorithm (BFS-based)
- Cycle detection as side effect
- Time complexity: O(V + E)

**Implement Kahn's Algorithm:**
- Code it from scratch (don't copy)
- Test on simple examples (3-4 nodes)
- Verify cycle detection works

#### Phase 3: DP on DAG (40 min)
**Three Core Operations:**

1. **Longest Path** (15 min)
   - Topo sort → DP in order
   - Why transitions work: all predecessors done
   - Trace example with 5-6 nodes

2. **Critical Path Method** (15 min)
   - Forward pass: earliest start/finish
   - Backward pass: latest start/finish
   - Slack computation
   - Real example: 5-task project

3. **Shortest Path with Negative Weights** (10 min)
   - How DAG enables O(V+E) solution
   - Compare to Bellman-Ford O(VE)
   - Why general graphs need Bellman-Ford

**Key Debugging Skills:**
- Topological order wrong? → Answer wrong
- Verify topo order before debugging DP

#### Phase 4: Integration (20 min)
**Practice:**
1. LeetCode 207: Course Schedule
   - Detect cycles using topo sort

2. LeetCode 329: Longest Increasing Path in Matrix
   - Grid as DAG
   - Apply longest path DP

---

### DAY 3: BITMASK & SUBSET DP (120 min)

#### Phase 1: Bitmask Fundamentals (20 min)
**Master These Operations:**
- `(mask & (1 << i))` — check if bit i set
- `mask | (1 << i)` — set bit i
- `mask ^ (1 << i)` — toggle bit i
- `__builtin_popcount(mask)` — count set bits

**Practice Drill (5 min):**
- For n=4, enumerate all 16 masks
- Identify which elements each mask represents
- Verify you understand bitmask-subset correspondence

#### Phase 2: TSP Deep Dive (60 min)
**Problem Understanding (10 min):**
- Why TSP is hard (n! permutations)
- How DP transforms it (2^n × n² → feasible)
- Feasibility boundary: n ≤ 20

**DP Solution (30 min):**
- State: dp[mask][last] = min cost
- Initialization: dp[1][0] = 0
- Transition: try all next cities
- Answer: min(dp[fullmask][c] + cost[c][0])

**Implementation & Tracing (20 min):**
- Code TSP from scratch (don't copy)
- Trace through 4-city example
- Verify cost = sum of distances
- Check that all states are reachable

**Critical Debugging:**
- If answer wrong, check:
  1. Bitmask operations correct?
  2. Transition formula correct?
  3. Answer extraction correct?

#### Phase 3: Subset Sum & Enumeration (25 min)
**Subset Sum DP:**
- Count subsets with target sum
- 1D DP with backward iteration
- Why backward? (avoid using item twice)

**Submask Iteration:**
- For each mask, iterate all submasks
- O(3^n) over all masks
- Useful for "subset of subset" problems

#### Phase 4: Integration (15 min)
**Solve:**
1. LeetCode 943: Shortest Superstring (TSP variant)
2. LeetCode 698: Partition to K Equal Subsets (bitmask + recursion)

---

### DAY 4: STATE COMPRESSION [OPTIONAL] (90 min)

#### Phase 1: Sliding Window (30 min)
**Understand:**
- Original: dp[i][j] = O(m × n)
- Optimized: dp[j] = O(n)
- Only need previous row at any time

**Implement:**
- LCS space-optimized
- Edit distance space-optimized
- Verify results match 2D version

#### Phase 2: Dimension Reduction (20 min)
**Concept:**
- 3D DP → 2D by eliminating unnecessary dimension
- Example: coin change (is dimension 3 necessary?)

**Analyze:**
- When is dimension truly independent?
- When can we eliminate it?

#### Phase 3: Memoization vs Tabulation (20 min)
**Comparison:**
- Memoization: compute only reachable states
- Tabulation: compute all states upfront
- Which is faster? (Depends on problem)

**Decision Framework:**
- Sparse state space → memoization
- Dense state space → tabulation
- Benchmark both for your problem

#### Phase 4: Pruning (20 min)
**Branch-and-Bound:**
- Compute upper bound for remaining search
- Prune if current + upper < best
- Need tight bounds for effectiveness

**Practice:**
- Implement 0/1 knapsack with pruning
- Compare runtime with/without pruning

---

### DAY 5: MIXED DP PROBLEMS [OPTIONAL] (90 min)

#### Phase 1: Recognition Framework (20 min)
**Three Key Questions:**
1. Overlapping subproblems?
2. Optimal substructure?
3. Reasonable state space?

**If all YES → DP likely works**

#### Phase 2: Problem Taxonomy (20 min)
**Six Classes:**
1. Optimization (maximize/minimize)
2. Counting (count ways)
3. Decision (feasibility)
4. Ordering (sequence with constraints)
5. Graph (paths, substructures)
6. Hybrid (multi-concept)

**For each new problem:**
- Identify which class
- Reference appropriate solution pattern

#### Phase 3: Five-Step Strategy (30 min)
**Under Time Pressure:**
1. Recognize subproblems (1-2 min)
2. Define state (1-2 min)
3. Base cases (1 min)
4. Transitions (2-3 min)
5. Implement & verify (5-10 min)

**Practice:** Solve novel problem in 20 minutes

#### Phase 4: Integration Problems (20 min)
**Solve:**
1. Combine DP with binary search
2. Combine DP with greedy
3. Design for unfamiliar problem

---

## ⚡ PERFORMANCE OPTIMIZATION TIPS

### Coding Efficiency
- **Memoization decorator (if language supports):** Reduce boilerplate
- **1D vs 2D arrays:** Use 1D with proper indexing if space critical
- **Early termination:** Break loops when answer found

### Time Management During Problems
- **2 min planning phase:** State, transitions, base cases
- **3 min implementation:** Code cleanly (not fast)
- **1 min verification:** Trace example, check edge cases

### Debugging DP Solutions
**If wrong answer, check in order:**
1. Base cases correct?
2. Initialization correct?
3. Transition formula correct?
4. Answer extraction correct?
5. Loop bounds correct?

**If time limit exceeded:**
1. Is state space too large?
2. Can you compress dimensions?
3. Can you prune branches?
4. Is there a better algorithm?

---

## 🎯 INTERVIEW PREPARATION FOCUS

### High-Probability Interview Questions

**Definitely Ask:**
- Tree DP (independent set, diameter)
- DAG DP (longest path, critical path)
- TSP or bitmask DP variant

**Likely to Ask:**
- DP state design for novel problem
- Space optimization
- When to use DP vs greedy

**Strategy:**
- Practice describing state clearly
- Explain transitions to interviewer
- Verify understanding before coding

### Communication Best Practices

**When Stuck:**
1. State your observations ("overlapping subproblems")
2. Propose state definition
3. Ask for feedback before coding

**During Coding:**
- Explain state initialization
- Narrate transition logic
- Test on example while coding

**After Coding:**
- Trace through example
- Discuss space/time complexity
- Suggest optimizations

---

## 📊 WEEK 11 MASTERY CHECKLIST

### Essential Skills (Day 1-3, Core)
- [ ] Implement max independent set (2-state tree DP)
- [ ] Compute tree diameter in O(n)
- [ ] Perform topological sort (Kahn's)
- [ ] Apply DP on DAG using topo order
- [ ] Implement TSP with bitmask DP (n ≤ 15)
- [ ] Enumerate subsets and submasks
- [ ] Identify when DP applies (3 questions)
- [ ] Design state in < 3 minutes

### Advanced Skills (Day 4-5, Optional)
- [ ] Optimize DP space (2D → 1D)
- [ ] Choose memoization vs tabulation
- [ ] Implement pruning strategy
- [ ] Solve hybrid problems (DP + greedy/binary search)
- [ ] Recognize multi-concept problems
- [ ] Solve novel problem under time pressure

### Production Skills
- [ ] Implement tree DP iteratively (no stack overflow)
- [ ] Handle negative weights in DAG
- [ ] Precompute validity for bitmask
- [ ] Debug DP solution systematically
- [ ] Profile performance bottlenecks

---

## 🔗 CROSS-WEEK CONNECTIONS

### Prerequisites (Review if Needed)
- **Week 10:** Basic DP (subproblems, memoization, tabulation)
- **Weeks 8-9:** Trees and graphs (traversal, representation)
- **Weeks 4-5:** Bit manipulation (essential for bitmask DP)
- **Week 6-7:** Sorting (needed for some DAG algorithms)

### Forward Connections
- **Week 12:** Approximation algorithms (when DP infeasible)
- **Week 14:** Game theory DP (minimax on game states)
- **Week 15:** Complexity theory (understand DP's limits)
- **Week 16:** Competitive programming (apply DP at speed)

---

## 🚀 DAILY SCHEDULE TEMPLATE

### Recommended Daily Routine

#### Morning Study (120-180 min)
1. Read instructional material (45 min)
2. Work through examples by hand (30 min)
3. Implement solutions from scratch (30-45 min)
4. Test and debug (15 min)

#### Afternoon Practice (60-90 min)
1. Solve 2-3 practice problems (45 min)
2. Compare solutions with reference (15 min)
3. Optimize for space/time (15 min)

#### Evening Review (30 min)
1. Trace through problems again
2. Identify weak areas
3. Plan next day

---

## 🛑 RED FLAGS & WHEN TO PIVOT

### Signs You're Stuck (Not Learning)
- ❌ Reading same section for 3rd time without understanding
- ❌ Copying code without understanding logic
- ❌ Can't explain solution to someone else
- ❌ Same mistake on multiple problems

**Action:** Take break, re-read introductory material, ask for help

### When to Use Different Approach
- ❌ DP taking 20+ minutes to code → state might be wrong
- ❌ DP state space > 10^8 → compression needed
- ❌ Greedy works and is simpler → use greedy instead

**Action:** Reconsider problem structure, check if DP is right approach

---

## 📝 REFLECTION TEMPLATE (Daily)

End each day, answer:

1. **What I learned today:**
   - [Top 2-3 insights]

2. **What confused me:**
   - [Specific concepts to revisit]

3. **What I'll practice tomorrow:**
   - [2-3 specific problems]

4. **How confident I am (1-10):**
   - Tree DP: __
   - DAG DP: __
   - Bitmask DP: __
   - Overall Week 11: __

---

## 🎓 WEEK 11 SUCCESS DEFINITION

**You've succeeded at Week 11 if you can:**

1. ✅ Solve tree DP problems in 15-20 minutes
2. ✅ Design DAG DP for scheduling problems
3. ✅ Implement TSP DP for n ≤ 20
4. ✅ Optimize DP solutions for space
5. ✅ Recognize DP problems in disguise
6. ✅ Explain concepts to others
7. ✅ Pass interview-style questions

**You're ready for Week 12 if:** You can solve a novel DP problem (not seen before) in 25 minutes with >80% success rate.

---

**File Status:** ✅ Complete | **Last Updated:** January 26, 2026

