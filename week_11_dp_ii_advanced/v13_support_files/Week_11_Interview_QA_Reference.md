# 📘 WEEK 11 INTERVIEW Q&A REFERENCE

**Week:** 11 | **Purpose:** Prepare for technical interviews with curated Q&A  
**Format:** Question + Expected Answer + Follow-ups + Scoring

---

## 📌 INTERVIEW PREPARATION OVERVIEW

**Interview Focus Areas:**
- Tree DP fundamentals & problem recognition
- DAG DP and topological ordering
- Bitmask DP and subset enumeration
- State design and transition logic
- Space/time optimization techniques
- Real-world applications & trade-offs

**Total Questions:** 45 (organized by difficulty)

---

## 🎯 TIER 1: FUNDAMENTAL QUESTIONS (Warm-up)

### Q1.1: Explain Tree DP in 30 Seconds

**Question:** "What makes tree DP different from general DP?"

**Expected Answer:**
- Tree has no cycles (no infinite loops)
- Can solve each subtree independently
- Post-order traversal combines results bottom-up
- Each node processed exactly once → O(n)

**Scoring Rubric:**
- ✅ Mentioned no cycles: +1
- ✅ Mentioned post-order: +1
- ✅ Mentioned O(n) efficiency: +1
- ✅ Clear explanation (no hand-waving): +1
- **Total: 4 points**

**Follow-up Questions:**
1. Why post-order and not pre-order?
2. Could you do this with BFS instead?
3. What's the space complexity?

**Answer to Follow-up 1:**
"Pre-order would process parent before children, but we need children's answers first. Post-order guarantees all children are solved when we reach the parent."

---

### Q1.2: Two-State DP Pattern

**Question:** "When you see 'include/exclude' in a DP problem, what state design do you use?"

**Expected Answer:**
```
dp[i][0] = answer excluding element i
dp[i][1] = answer including element i

Then transitions combine:
dp[i][0] = best of (dp[i-1][0], dp[i-1][1]) without i
dp[i][1] = dp[i-1][0] + value[i]  (must exclude i-1 if including i)
```

**Scoring Rubric:**
- ✅ Clear state definition: +2
- ✅ Correct transition logic: +2
- ✅ Explained dependency on previous state: +1
- **Total: 5 points**

**Follow-up Questions:**
1. Is there ever a case where you use [0/1/2] instead of [0/1]?
2. Can you convert this to bottom-up easily?

---

### Q1.3: Topological Sort Purpose

**Question:** "Why is topological sort necessary for DAG DP? Why can't you just process nodes in arbitrary order?"

**Expected Answer:**
"Topological sort ensures that when you process a node, all its predecessors are already processed. If you process arbitrarily, you might encounter a node before its predecessor is computed, leading to incorrect/uninitialized values. Topo order is the key to correctness."

**Scoring Rubric:**
- ✅ Mentioned predecessor processing requirement: +2
- ✅ Explained consequence of violating order: +2
- ✅ Linked to correctness (not efficiency): +1
- **Total: 5 points**

**Follow-up Questions:**
1. What's the time to compute topological sort?
2. Could you use memoization instead?
3. How do you verify a graph is actually a DAG?

---

### Q1.4: Bitmask Basics

**Question:** "How do you represent a subset using a bitmask? Give an example."

**Expected Answer:**
"Each bit position represents an element. Bit i = 1 means element i is in the subset.

Example: For set {A, B, C, D} with n=4:
- Mask 0101 (binary) = elements at index 0 and 2 = {A, C}
- Mask 1111 = {A, B, C, D} (all)
- Mask 0001 = {A} (single element)

To iterate all subsets, loop mask from 0 to 2^n - 1."

**Scoring Rubric:**
- ✅ Clear bit-to-element mapping: +2
- ✅ Provided concrete example: +1
- ✅ Explained how to iterate: +1
- ✅ Mentioned 2^n bound: +1
- **Total: 5 points**

**Follow-up Questions:**
1. How do you check if bit i is set?
2. How do you set bit i?
3. How do you iterate all subsets of a given mask?

**Answers:**
1. `(mask & (1 << i)) != 0`
2. `mask | (1 << i)` or `mask |= (1 << i)`
3. `for (int sub = mask; sub > 0; sub = (sub-1) & mask)`

---

## 🎯 TIER 2: CONCEPTUAL DEEP-DIVES

### Q2.1: Maximum Independent Set in Trees

**Question:** "Design a DP solution for maximum independent set in a tree. What's the state and transition?"

**Expected Answer:**

**State:**
```
dp[node][0] = max value in subtree if node is EXCLUDED
dp[node][1] = max value in subtree if node is INCLUDED
```

**Transition:**
```
For a leaf:
  dp[leaf][0] = 0
  dp[leaf][1] = leaf.value

For internal node:
  dp[node][0] = sum(max(dp[child][0], dp[child][1]) for each child)
               (if we exclude node, children can be included or excluded)
  
  dp[node][1] = node.value + sum(dp[child][0] for each child)
               (if we include node, children MUST be excluded)
```

**Answer:** `max(dp[root][0], dp[root][1])`

**Scoring Rubric:**
- ✅ State definition clear: +2
- ✅ Base case correct: +1
- ✅ Transition logic for exclude: +1
- ✅ Transition logic for include: +1
- ✅ Correct answer extraction: +1
- **Total: 6 points**

**Follow-up Questions:**
1. What's the time and space complexity?
2. Can you reconstruct the actual set (not just value)?
3. What happens if all node values are the same?

**Answers:**
1. Time: O(n), Space: O(h) where h is tree height
2. During DP, track which choice was made at each node, then backtrack
3. Still works, but you might get multiple optimal solutions

---

### Q2.2: Tree Diameter

**Question:** "How do you find the diameter (longest path) of a tree using DP?"

**Expected Answer:**

**Key Insight:** "Diameter is formed by combining the two longest paths from some node. So at each node, track the longest path going DOWN."

**State:**
```
dp[node] = longest path going DOWN from node
diameter = max over all nodes of (left_depth + right_depth + 1)
```

**Algorithm:**
```
1. DFS from root
2. For each node, compute longest paths to its children
3. Track top 2 depths (longest and second-longest)
4. Diameter at this node = longest + second-longest + 1
5. Return global maximum diameter
```

**Scoring Rubric:**
- ✅ Identified longest-down path insight: +2
- ✅ State definition: +1
- ✅ Algorithm for combining two paths: +1
- ✅ Mentioned tracking top 2 depths: +1
- ✅ Time complexity O(n) stated: +1
- **Total: 6 points**

**Follow-up Questions:**
1. Why do you need the top 2 depths (not just top 1)?
2. How would you handle a tree with only one child per node (linked list)?
3. Can diameter be computed with BFS instead?

---

### Q2.3: Critical Path Method

**Question:** "You're given a project with tasks and dependencies. How would you find the critical path (tasks that, if delayed, delay the whole project)?"

**Expected Answer:**

**Approach:** Use DAG DP with two passes.

**Forward Pass** (earliest start/finish times):
```
For each task in topological order:
  earliest_start = max(earliest_finish of predecessors)
  earliest_finish = earliest_start + duration
```

**Backward Pass** (latest start/finish times):
```
For each task in reverse topological order:
  latest_finish = min(latest_start of successors)
  latest_start = latest_finish - duration
```

**Critical Path:** Tasks where `latest_start == earliest_start` (slack = 0)

**Scoring Rubric:**
- ✅ Mentioned two passes: +1
- ✅ Forward pass correct: +2
- ✅ Backward pass correct: +2
- ✅ Slack definition and critical criteria: +1
- **Total: 6 points**

**Follow-up Questions:**
1. What's the time complexity?
2. Can there be multiple critical paths?
3. How do you handle cycles in the task graph?

**Answers:**
1. O(V + E) for topo sort + passes
2. Yes, multiple paths can have slack=0
3. Cycles indicate impossible project (detect with cycle checker)

---

### Q2.4: TSP via Bitmask DP

**Question:** "Solve Traveling Salesman Problem for n=4 cities. Explain your approach."

**Example:** 
```
Distances:
  0 1 2 3
0 ∞ 10 15 20
1 10 ∞ 35 25
2 15 35 ∞ 30
3 20 25 30 ∞
```

**Expected Answer:**

**State:**
```
dp[mask][last] = minimum cost to visit cities in mask, ending at city last

Interpretation: Bitmask shows which cities visited (1 = visited, 0 = unvisited)
```

**Base Case:**
```
dp[1][0] = 0  (start at city 0, visited only 0)
dp[mask][city] = INF for mask not containing city (invalid)
```

**Transition:**
```
For each mask and last city:
  For each unvisited city next:
    new_mask = mask | (1 << next)
    dp[new_mask][next] = min(dp[new_mask][next], 
                              dp[mask][last] + dist[last][next])
```

**Answer:**
```
fullmask = (1 << 4) - 1 = 15 (all 4 cities)
result = min(dp[fullmask][c] + dist[c][0] for c in 1..3)
       = min(dp[15][1]+dist[1][0], dp[15][2]+dist[2][0], dp[15][3]+dist[3][0])
```

**Trace Example:**
```
dp[1][0] = 0
dp[3][1] = dp[1][0] + dist[0][1] = 0 + 10 = 10
dp[5][2] = dp[1][0] + dist[0][2] = 0 + 15 = 15
dp[7][3] = dp[1][0] + dist[0][3] = 0 + 20 = 20
dp[3][1] + dist[1][2] → dp[7][2] = 10 + 35 = 45
...
```

**Scoring Rubric:**
- ✅ State definition clear: +1
- ✅ Base case correct: +1
- ✅ Transition logic: +2
- ✅ Answer extraction: +1
- ✅ Complexity mentioned: +1
- **Total: 6 points**

**Follow-up Questions:**
1. What's the time and space complexity?
2. For what values of n is this feasible?
3. How would you reconstruct the actual tour?

**Answers:**
1. Time: O(2^n × n²), Space: O(2^n × n)
2. Feasible for n ≤ 20, marginal for n ≤ 25
3. Track parent decisions during DP, backtrack from end

---

## 🎯 TIER 3: ADVANCED & TRICKY QUESTIONS

### Q3.1: State Design for Novel Problem

**Question:** "You're given a problem: 'Count number of paths in a tree from any node to any other node where path sum equals target.' Design the DP state."

**Expected Answer (with thinking process):**

**Step 1: Identify what varies**
- Current node
- Target sum remaining

**Step 2: Define state**
```
dp[node][remaining_sum] = number of paths in subtree rooted at node
                          that start at node and have sum = remaining_sum
```

**Step 3: Transition**
```
For each child of node:
  For each sum_value in valid range:
    If child can contribute (sum_value - child.value):
      dp[node][sum_value] += dp[child][sum_value - child.value]
```

**Step 4: Answer**
```
Result = sum of (dp[node][target] for all nodes)
```

**Scoring Rubric:**
- ✅ Identified varying parameters: +1
- ✅ Clear state definition: +2
- ✅ Transition logic: +2
- ✅ Thought process shown: +1
- **Total: 6 points**

---

### Q3.2: Space Optimization

**Question:** "You have a DP solution that uses O(m × n) space. How would you optimize to O(n)?"

**Expected Answer:**

**Key Insight:** "If transitions only depend on the previous row, you only need to keep current and previous row in memory."

**Original:**
```
for i in 0..m:
  for j in 0..n:
    dp[i][j] = f(dp[i-1][j], dp[i-1][j-1], ...)
```

**Optimized:**
```
prev = [0] * (n+1)
curr = [0] * (n+1)

for i in 0..m:
  for j in 0..n:
    curr[j] = f(prev[j], prev[j-1], ...)
  prev, curr = curr, prev  // swap
```

**Scoring Rubric:**
- ✅ Identified iteration dependency: +2
- ✅ Showed why previous rows can be discarded: +1
- ✅ Presented optimized code: +2
- ✅ Mentioned trade-off (harder to reconstruct): +1
- **Total: 6 points**

---

### Q3.3: When NOT to Use DP

**Question:** "You see an interviewer write a problem on the board. What red flags tell you DP is NOT the right approach?"

**Expected Answers:**

1. **No overlapping subproblems**
   - Each subproblem solved at most once
   - Example: Iterating through array once (can use greedy)

2. **No optimal substructure**
   - Optimal solution doesn't build on optimal subproblems
   - Example: Longest path in general graph (might need backtracking)

3. **State space too large**
   - Number of unique subproblems > 10^8
   - Would cause memory/time issues
   - Example: Unrestricted subset (2^n for large n)

4. **Simpler algorithm exists**
   - Greedy works (e.g., activity selection)
   - Graph traversal sufficient (e.g., connectivity)
   - Example: Coin change with denominations {1, 5, 10, 25}

5. **Constraints don't match DP**
   - Output is not optimizing a numeric value
   - Example: "Find all paths" (use DFS, not DP)

**Scoring Rubric:**
- ✅ Mentioned overlapping subproblems: +1
- ✅ Mentioned optimal substructure: +1
- ✅ Mentioned state space size: +1
- ✅ Mentioned alternative algorithms: +1
- ✅ Gave examples: +1
- **Total: 5 points**

---

## 🎯 TIER 4: REAL-WORLD SCENARIOS

### Q4.1: Production System Design

**Question:** "At a company, you need to compute the longest path in a build dependency graph with 10,000 tasks. What algorithm would you use and why?"

**Expected Answer:**

**Best Approach:** DAG DP with topological sort

**Reasoning:**
```
1. Build graph is a DAG (no circular dependencies)
2. Need longest path (critical path for scheduling)
3. Topological sort ensures correct DP order
4. Time: O(V + E) is efficient even for 10K tasks
5. Can identify bottleneck tasks (critical path)
```

**Algorithm:**
```
1. Build dependency graph (tasks as nodes, deps as edges)
2. Topological sort (Kahn's algorithm)
3. DP: dp[task] = longest path from this task
4. Verify no cycles during topo sort
5. Return max(dp[task])
```

**Why not Bellman-Ford?**
```
- Bellman-Ford is O(VE), inefficient for 10K tasks
- DAG DP is O(V + E), much faster
- Saves potentially 1000× time
```

**Scoring Rubric:**
- ✅ Chose correct algorithm: +2
- ✅ Explained why (DAG + topo sort advantage): +2
- ✅ Time complexity comparison: +1
- ✅ Practical implementation details: +1
- **Total: 6 points**

---

### Q4.2: Interview Communication

**Question:** "You're stuck on a DP problem during an interview. How do you communicate your thought process?"

**Expected Answer:**

**Step 1: Acknowledge Stuck Point**
"I'm not immediately seeing the state definition. Let me think through this systematically."

**Step 2: Identify Subproblems**
"What do I need to solve for smaller inputs? For position i and situation X, what's the answer?"

**Step 3: Propose State**
"I think the state should be dp[i][j] representing... Does that make sense to you?"

**Step 4: Verify on Example**
"Let me trace through a small example: if n=3, then... Do you see how this would work?"

**Step 5: Discuss Transitions**
"To compute dp[i], I need dp[i-1] and... What do you think about this dependency?"

**Step 6: Verify Correctness**
"So the recurrence is... and base case is... Is this logic sound?"

**Scoring Rubric:**
- ✅ Asked for feedback (collaborative): +1
- ✅ Explained reasoning (not just code): +1
- ✅ Traced example: +1
- ✅ Discussed trade-offs: +1
- ✅ Stayed calm/positive: +1
- **Total: 5 points**

---

## ✅ ANSWER SCORING GUIDE

### Interview Scoring System

**Perfect Answer (10 pts):**
- Completely correct
- Well-explained
- Time/space complexity mentioned
- Edge cases considered
- Clean code (if requested)

**Good Answer (8 pts):**
- Core logic correct
- Minor omissions
- Mostly clear explanation
- Complexity mentioned

**Adequate Answer (6 pts):**
- Correct approach
- Some confusion in details
- Can clarify with follow-ups
- Mostly correct

**Incomplete Answer (4 pts):**
- Right idea but incomplete
- Several errors
- Needs significant clarification

**Wrong Answer (0 pts):**
- Fundamentally incorrect
- Shows misunderstanding

---

## 📋 INTERVIEW PREPARATION CHECKLIST

### Before Interview

- [ ] Review all 15 core concepts (Tier 1-2)
- [ ] Practice explaining each concept aloud (30 sec each)
- [ ] Trace through 3 examples by hand (tree DP, DAG DP, TSP)
- [ ] Do mock interviews (1 hour, 3 problems)
- [ ] Review red flags (when NOT to use DP)
- [ ] Practice communication techniques

### During Interview

- [ ] Ask clarifying questions (constraints, examples)
- [ ] Define state clearly before coding
- [ ] Trace through example before coding
- [ ] Code cleanly (not fast)
- [ ] Discuss complexity (time and space)
- [ ] Test edge cases

### After Problem

- [ ] "Can I optimize further?"
- [ ] "Are there trade-offs to discuss?"
- [ ] "When would this approach fail?"
- [ ] "How would you modify for constraint X?"

---

## 🎓 MOCK INTERVIEW SCENARIOS

### Scenario 1: Tree DP (20 min)

**Problem:** "Given a tree, find the maximum sum of values such that no two adjacent nodes are selected."

**Time Breakdown:**
- Clarification: 2 min
- State design: 3 min
- Example trace: 3 min
- Code: 8 min
- Complexity discussion: 2 min
- Follow-up: 2 min

**Expected Solution:** Maximum independent set (standard DP)

---

### Scenario 2: DAG DP (20 min)

**Problem:** "Given tasks with dependencies, find all tasks that are on the critical path."

**Time Breakdown:**
- Clarification: 2 min
- Algorithm design: 4 min
- Forward/backward passes: 3 min
- Code: 7 min
- Complexity discussion: 2 min
- Follow-up: 2 min

---

### Scenario 3: Bitmask DP (25 min)

**Problem:** "Count number of ways to visit all nodes in a small graph exactly once, starting and ending at node 0."

**Time Breakdown:**
- Clarification: 2 min
- State definition: 3 min
- Example trace (small input): 4 min
- Code: 10 min
- Complexity + feasibility: 3 min
- Follow-up: 3 min

---

## 📊 PERFORMANCE METRICS

**Track your improvement:**

| Interview #1 | Interview #2 | Interview #3 |
| :--- | :--- | :--- |
| Problem 1: _/10 | Problem 1: _/10 | Problem 1: _/10 |
| Problem 2: _/10 | Problem 2: _/10 | Problem 2: _/10 |
| Problem 3: _/10 | Problem 3: _/10 | Problem 3: _/10 |
| Communication: _/5 | Communication: _/5 | Communication: _/5 |
| **Total: _/35** | **Total: _/35** | **Total: _/35** |

**Goal:** 28+/35 (80%+) = Ready for real interview

---

**File Status:** ✅ Complete | **Last Updated:** January 26, 2026

