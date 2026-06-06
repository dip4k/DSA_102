# 🗺️ WEEK 11 PROBLEM-SOLVING ROADMAP

**Week:** 11 | **Purpose:** Structured problem-solving progression from basic to advanced  
**Format:** Problem list organized by day, difficulty, and concept

---

## 📌 ROADMAP OVERVIEW

**Problem Structure:**
- **Core Problems (15):** Essential for mastery
- **Practice Problems (25):** Reinforce concepts
- **Challenge Problems (10):** Stretch your limits

**Total: 50 problems spanning all Week 11 topics**

---

## 🗺️ GUIDED PROBLEM-SOLVING PATH

### DAY 1: TREE DP PROBLEMS

#### Phase 1A: Concept Verification (5 problems, 15 min each)

**Level: Easy | Goal: Verify understanding of tree DP basics**

**P1.1: House Robber III**
- **Link:** LeetCode 337
- **Concept:** Tree DP with include/exclude states
- **Given:** Binary tree with node values
- **Goal:** Rob houses (nodes) with max value, can't rob adjacent nodes
- **Expected Approach:** dp[node][0/1] for exclude/include
- **Time Target:** 15 min
- **Difficulty:** ⭐ Easy

**Why this problem?**
- Most direct application of 2-state tree DP
- Clear state definition: include or exclude
- Standard transitions
- Good warm-up before harder problems

**Key Insights:**
1. Post-order traversal (solve children first)
2. If you rob node, children must be skipped
3. If you don't rob node, children can be any state
4. Combine: max value = max of two choices

**Solution Verification:**
```
Example: Tree [3, 2, 3, null, 3, null, 1]
    3
   / \
  2   3
   \   \
    3   1

Answer: Rob {3, 3, 3} = 9 (or {2, 3, 1} = 6)
Maximum: 9 ✓
```

---

**P1.2: Binary Tree Maximum Path Sum**
- **Link:** LeetCode 124
- **Concept:** Path computation in trees (includes diameter idea)
- **Given:** Binary tree with values (can be negative)
- **Goal:** Find path with maximum sum (no parent-child requirement)
- **Expected Approach:** Track max path down + diameter idea
- **Time Target:** 15 min
- **Difficulty:** ⭐⭐ Medium

**Key Challenge:** 
- Need to track "max path going down" (like diameter)
- Also need "max path through node" (combining left + right)
- Can't just take two longest paths (might be on same side)

**Solution Idea:**
```
For each node:
  max_down_left = max path going down left child
  max_down_right = max path going down right child
  
  max_through_node = node.value + max_down_left + max_down_right
  (only if helpful; can be negative)
  
  return max_down = node.value + max(max_down_left, max_down_right, 0)
```

---

**P1.3: Diameter of Binary Tree**
- **Link:** LeetCode 543
- **Concept:** Pure diameter computation via DP
- **Given:** Binary tree
- **Goal:** Find longest path between any two nodes
- **Expected Approach:** dp[node] = longest path down from node
- **Time Target:** 15 min
- **Difficulty:** ⭐ Easy

**Clean Solution Pattern:**
```
For each node:
  left_depth = longest path down left subtree
  right_depth = longest path down right subtree
  
  diameter_here = left_depth + right_depth + 1
  max_diameter = max overall
  
  return (left_depth + 1) to parent
```

---

**P1.4: Balanced Binary Tree**
- **Link:** LeetCode 110
- **Concept:** Tree DP with early termination
- **Given:** Binary tree
- **Goal:** Check if balanced (height difference ≤ 1 at each node)
- **Expected Approach:** Bottom-up height computation
- **Time Target:** 10 min
- **Difficulty:** ⭐ Easy

---

**P1.5: Lowest Common Ancestor**
- **Link:** LeetCode 236
- **Concept:** Tree DP with search
- **Given:** Binary tree, two nodes
- **Goal:** Find lowest common ancestor
- **Expected Approach:** Post-order search in subtrees
- **Time Target:** 12 min
- **Difficulty:** ⭐ Easy

---

#### Phase 1B: State Variations (5 problems, 20 min each)

**Level: Medium | Goal: Master different state designs for trees**

**P1.6: Validate Binary Search Tree**
- **Link:** LeetCode 98
- **Concept:** Tree DP with constraints
- **Approach:** Track valid range for each subtree
- **Time Target:** 15 min
- **Difficulty:** ⭐⭐ Medium

---

**P1.7: Path Sum III**
- **Link:** LeetCode 437
- **Concept:** Tree DP with target path counting
- **Approach:** DFS tracking paths + counts
- **Challenge:** Paths don't have to start at root
- **Time Target:** 20 min
- **Difficulty:** ⭐⭐ Medium

---

**P1.8: Binary Tree Longest Consecutive Sequence**
- **Link:** LeetCode 549
- **Concept:** Tree DP tracking sequences
- **Approach:** Bottom-up, track increasing/decreasing counts
- **Time Target:** 20 min
- **Difficulty:** ⭐⭐ Medium

---

**P1.9: Distribute Coins in Binary Tree**
- **Link:** LeetCode 979
- **Concept:** Tree DP with flow computation
- **Approach:** Calculate excess/deficit at each node
- **Time Target:** 25 min
- **Difficulty:** ⭐⭐⭐ Hard

---

**P1.10: Tree Coloring** (Custom)
- **Concept:** Tree DP with state choices
- **Given:** Tree, k colors
- **Goal:** Count valid colorings (adjacent ≠ colors)
- **Approach:** dp[node][color] = count
- **Time Target:** 20 min
- **Difficulty:** ⭐⭐ Medium

---

#### Phase 1C: Advanced Tree DP (3 problems, 25 min each)

**Level: Hard | Goal: Master complex tree patterns**

**P1.11: Tree Rerooting**
- **Link:** LeetCode 834 / Custom
- **Concept:** Two-pass algorithm for re-rooting
- **Approach:** First pass from original root, second pass reroot
- **Time Target:** 25 min
- **Difficulty:** ⭐⭐⭐ Hard

---

**P1.12: Binary Tree Maximum Product Path**
- **Concept:** Track both max and min (negative × negative = positive)
- **Approach:** Similar to max path sum but track two values
- **Time Target:** 25 min
- **Difficulty:** ⭐⭐⭐ Hard

---

**P1.13: Binary Tree Vertical Order Traversal** (DP variant)
- **Concept:** Tree DP with coordinate tracking
- **Time Target:** 20 min
- **Difficulty:** ⭐⭐ Medium

---

### DAY 2: DAG DP PROBLEMS

#### Phase 2A: Fundamentals (5 problems, 15 min each)

**Level: Easy | Goal: Master topological ordering and basic DAG DP**

**P2.1: Course Schedule (Cycle Detection)**
- **Link:** LeetCode 207
- **Concept:** Detect cycles using topological sort
- **Given:** Graph of course prerequisites
- **Goal:** Determine if all courses can be taken
- **Expected Approach:** Kahn's algorithm, check if all processed
- **Time Target:** 15 min
- **Difficulty:** ⭐ Easy

**Key Insight:**
```
If topological sort processes < n nodes, cycle exists
If all n nodes processed, no cycle (is DAG)
```

---

**P2.2: Course Schedule II (Topological Order)**
- **Link:** LeetCode 210
- **Concept:** Return actual topological order
- **Given:** Prerequisites
- **Goal:** Return valid course order (or empty if impossible)
- **Expected Approach:** Kahn's algorithm, return sorted order
- **Time Target:** 15 min
- **Difficulty:** ⭐ Easy

---

**P2.3: Longest Increasing Path in Matrix**
- **Link:** LeetCode 329
- **Concept:** Matrix as DAG, longest path DP
- **Given:** 2D matrix
- **Goal:** Find longest increasing path
- **Expected Approach:** Treat matrix as DAG (edges between adjacent cells with increasing values)
- **Time Target:** 20 min
- **Difficulty:** ⭐⭐ Medium

**Key Insight:**
```
If matrix[i][j] < matrix[i+1][j], there's an edge from (i,j) to (i+1,j)
This forms a DAG (increasing constraint prevents cycles)
Apply DP: dp[i][j] = longest path starting at (i,j)
```

---

**P2.4: Minimum Height Trees**
- **Link:** LeetCode 310
- **Concept:** DAG DP on trees (special case)
- **Given:** Tree edges
- **Goal:** Find nodes that minimize tree height when used as root
- **Expected Approach:** Two-pass algorithm
- **Time Target:** 20 min
- **Difficulty:** ⭐⭐ Medium

---

**P2.5: Sequence Reconstruction**
- **Link:** LeetCode 444
- **Concept:** DAG ordering verification
- **Given:** Original sequence and ordered lists
- **Goal:** Verify if ordered lists are consistent with original
- **Expected Approach:** Topological sort, check if matches original
- **Time Target:** 15 min
- **Difficulty:** ⭐⭐ Medium

---

#### Phase 2B: Complex DAG Problems (5 problems, 20-30 min each)

**Level: Medium-Hard | Goal: Master DAG DP in realistic scenarios**

**P2.6: Critical Path Method** (Custom)
- **Concept:** Forward and backward passes in DAG
- **Given:** Tasks with durations and dependencies
- **Goal:** Identify critical path
- **Approach:** Forward pass (earliest times), backward pass (latest times)
- **Time Target:** 25 min
- **Difficulty:** ⭐⭐⭐ Hard

---

**P2.7: Alien Dictionary**
- **Link:** LeetCode 269
- **Concept:** DAG ordering from constraints
- **Given:** Sorted words in alien language
- **Goal:** Determine character order
- **Expected Approach:** Build DAG of character relationships, topological sort
- **Time Target:** 25 min
- **Difficulty:** ⭐⭐⭐ Hard

---

**P2.8: Build Buildings**
- **Link:** LeetCode 1547
- **Concept:** DAG DP with constraints
- **Given:** Buildings with costs and prerequisites
- **Goal:** Minimum cost to build all buildings
- **Time Target:** 30 min
- **Difficulty:** ⭐⭐⭐ Hard

---

**P2.9: Shortest Path in DAG with Negative Weights**
- **Concept:** DAG DP advantage over Bellman-Ford
- **Given:** DAG with weighted edges (can be negative)
- **Goal:** Shortest path from source
- **Approach:** Topo sort, relax edges in order, O(V+E)
- **Time Target:** 20 min
- **Difficulty:** ⭐⭐⭐ Hard

---

**P2.10: Maximum Sum Path in DAG** (Custom)
- **Concept:** DAG DP optimization
- **Given:** DAG with node weights
- **Goal:** Find path with maximum sum
- **Approach:** dp[node] = max path starting at node, process in reverse topo order
- **Time Target:** 25 min
- **Difficulty:** ⭐⭐⭐ Hard

---

### DAY 3: BITMASK & SUBSET DP PROBLEMS

#### Phase 3A: Bitmask Fundamentals (5 problems, 15-20 min each)

**Level: Easy-Medium | Goal: Master bitmask operations and basic subset DP**

**P3.1: Traveling Salesman Problem (Small n)**
- **Concept:** TSP with bitmask DP
- **Given:** n cities (n ≤ 15), distance matrix
- **Goal:** Minimum cost tour visiting all cities
- **Expected Approach:** dp[mask][last] = min cost to visit mask ending at last
- **Time Target:** 25 min
- **Difficulty:** ⭐⭐ Medium

**Complete Trace:**
```
n = 4 cities
Start: dp[0001][0] = 0  (only city 0, cost 0)

Step 1: Expand to 2 cities
  dp[0011][1] = 0 + dist[0][1]
  dp[0101][2] = 0 + dist[0][2]
  dp[1001][3] = 0 + dist[0][3]

Step 2: Expand to 3 cities
  dp[0111][2] = min(dp[0011][1] + dist[1][2], dp[0101][2])
  dp[0111][3] = min(dp[0011][1] + dist[1][3], dp[1001][3])
  ...

Step 3: All 4 cities (mask = 1111)
  dp[1111][1] = min(dp[0111][2] + dist[2][1], dp[0111][3] + dist[3][1])
  dp[1111][2] = ...
  dp[1111][3] = ...

Answer: min(dp[1111][i] + dist[i][0] for i in 1..3)
```

---

**P3.2: Unique Paths III**
- **Link:** LeetCode 980
- **Concept:** Bitmask for grid cell visited tracking
- **Given:** Small grid with obstacles
- **Goal:** Count paths visiting all empty cells
- **Expected Approach:** dp[r][c][mask] = ways to reach (r,c) having visited mask cells
- **Time Target:** 20 min
- **Difficulty:** ⭐⭐ Medium

---

**P3.3: Partition to K Equal Subsets**
- **Link:** LeetCode 698
- **Concept:** Bitmask + recursive backtracking
- **Given:** Array of numbers
- **Goal:** Partition into k equal-sum subsets
- **Expected Approach:** Try assigning elements to subsets using bitmask
- **Time Target:** 25 min
- **Difficulty:** ⭐⭐⭐ Hard

---

**P3.4: Shortest Superstring**
- **Link:** LeetCode 943
- **Concept:** TSP variant (string merging)
- **Given:** Array of strings
- **Goal:** Find shortest superstring containing all strings
- **Expected Approach:** dp[mask][last] = shortest superstring visiting strings in mask, last string is last
- **Time Target:** 30 min
- **Difficulty:** ⭐⭐⭐ Hard

---

**P3.5: Beautiful Arrangement**
- **Link:** LeetCode 526
- **Concept:** Permutation with constraints via bitmask
- **Given:** n numbers
- **Goal:** Count arrangements where i divides arr[i]
- **Expected Approach:** dp[mask] = count of valid arrangements
- **Time Target:** 20 min
- **Difficulty:** ⭐⭐ Medium

---

#### Phase 3B: Advanced Bitmask (5 problems, 25-30 min each)

**Level: Hard | Goal: Master complex bitmask patterns**

**P3.6: Optimal Account Balancing**
- **Link:** LeetCode 465
- **Concept:** Bitmask DP for transaction optimization
- **Given:** Transactions between people
- **Goal:** Minimum number of transactions to balance all
- **Approach:** Bitmask tracks settled accounts, branch-and-bound
- **Time Target:** 35 min
- **Difficulty:** ⭐⭐⭐ Hard

---

**P3.7: Find the Celebrity**
- **Link:** LeetCode 277
- **Concept:** Bitmask + graph (advanced)
- **Given:** Relationship matrix (knows[i][j])
- **Goal:** Find celebrity (known by all, knows no one)
- **Time Target:** 20 min
- **Difficulty:** ⭐⭐ Medium

---

**P3.8: Subset Sum** (Classic)
- **Concept:** Bitmask enumeration
- **Given:** Array of numbers, target sum
- **Goal:** Find any subset with sum = target
- **Approach:** Enumerate all 2^n subsets via bitmask, check sum
- **Time Target:** 15 min
- **Difficulty:** ⭐⭐ Medium (for bitmask variant)

---

**P3.9: Maximum Weight Independent Set (Small Graph)**
- **Concept:** Bitmask DP for graph property
- **Given:** Small graph (≤ 20 nodes) with weights
- **Goal:** Maximum weight subset with no adjacent nodes
- **Approach:** dp[mask] = max weight if taking subset, verify no edges
- **Time Target:** 25 min
- **Difficulty:** ⭐⭐⭐ Hard

---

**P3.10: Steiner Tree Problem** (Advanced)
- **Concept:** Bitmask DP on trees
- **Given:** Tree, subset of terminals
- **Goal:** Minimum-weight subtree connecting terminals
- **Approach:** dp[node][mask] = min cost to connect terminals in mask
- **Time Target:** 40 min
- **Difficulty:** ⭐⭐⭐ Hard

---

### DAY 4: STATE COMPRESSION & OPTIMIZATION [OPTIONAL]

#### Phase 4A: Space Optimization (5 problems, 20 min each)

**P4.1: Longest Common Subsequence (Space Optimized)**
- **Link:** LeetCode 1143
- **Concept:** 2D → 1D via sliding window
- **Goal:** Find LCS, optimize space to O(n)
- **Approach:** Keep only previous and current rows
- **Time Target:** 20 min
- **Difficulty:** ⭐⭐ Medium

---

**P4.2: Edit Distance (Space Optimized)**
- **Link:** LeetCode 72
- **Concept:** Levenshtein distance with space optimization
- **Goal:** Minimum edits to transform string, use O(n) space
- **Time Target:** 20 min
- **Difficulty:** ⭐⭐ Medium

---

**P4.3: Coin Change (Standard → Optimized)**
- **Link:** LeetCode 322
- **Concept:** Understand when space compression is safe
- **Approach:** 1D DP is natural (no previous-row dependency)
- **Time Target:** 15 min
- **Difficulty:** ⭐ Easy

---

**P4.4: Knapsack with Pruning**
- **Concept:** Branch-and-bound optimization
- **Given:** Items with weights/values, capacity
- **Goal:** Maximum value, prune infeasible branches
- **Approach:** Compute upper bound, skip if can't improve
- **Time Target:** 25 min
- **Difficulty:** ⭐⭐⭐ Hard

---

**P4.5: Regex Matcher** (Space optimization discussion)
- **Concept:** DP optimization trade-offs
- **Time Target:** 20 min
- **Difficulty:** ⭐⭐ Medium

---

### DAY 5: MIXED DP PROBLEMS [OPTIONAL]

#### Phase 5A: Problem Recognition (5 problems)

**P5.1: Word Break**
- **Link:** LeetCode 139
- **Concept:** DP for string segmentation
- **Time Target:** 15 min
- **Difficulty:** ⭐ Easy

---

**P5.2: Decode Ways**
- **Link:** LeetCode 91
- **Concept:** Counting problem with constraints
- **Time Target:** 15 min
- **Difficulty:** ⭐⭐ Medium

---

**P5.3: Arithmetic Slices**
- **Link:** LeetCode 413
- **Concept:** Simple DP on array
- **Time Target:** 10 min
- **Difficulty:** ⭐ Easy

---

**P5.4: Best Time to Buy and Sell Stock IV**
- **Link:** LeetCode 188
- **Concept:** Multi-dimensional DP
- **Time Target:** 25 min
- **Difficulty:** ⭐⭐⭐ Hard

---

**P5.5: Paint House II**
- **Link:** LeetCode 265
- **Concept:** DP optimization (from O(nk²) to O(nk))
- **Time Target:** 20 min
- **Difficulty:** ⭐⭐⭐ Hard

---

#### Phase 5B: Novel Problems (5 problems)

**P5.6: Regex Matcher with '.' and '*'**
- **Link:** LeetCode 10
- **Concept:** Complex state design
- **Time Target:** 30 min
- **Difficulty:** ⭐⭐⭐ Hard

---

**P5.7: Stone Game**
- **Link:** LeetCode 877
- **Concept:** Game theory DP
- **Time Target:** 20 min
- **Difficulty:** ⭐⭐ Medium

---

**P5.8: Distinct Subsequences**
- **Link:** LeetCode 115
- **Concept:** Counting with constraints
- **Time Target:** 20 min
- **Difficulty:** ⭐⭐ Medium

---

**P5.9: Wildcard Matching**
- **Link:** LeetCode 44
- **Concept:** Pattern matching DP
- **Time Target:** 25 min
- **Difficulty:** ⭐⭐⭐ Hard

---

**P5.10: Jump Game IV**
- **Link:** LeetCode 1345
- **Concept:** Graph DP variant
- **Time Target:** 20 min
- **Difficulty:** ⭐⭐ Medium

---

## 📊 PROBLEM PROGRESSION MATRIX

### By Difficulty

| Difficulty | Count | Problems | Estimated Time |
| :--- | :--- | :--- | :--- |
| ⭐ Easy | 12 | P1.1-1.5, P2.1-2.2, P4.3, P5.1, P5.3 | 180 min |
| ⭐⭐ Medium | 20 | P1.6-1.8, P2.3-2.5, P3.1-3.2, P3.5, P4.1-4.3, P5.2, P5.7-5.8, P5.10 | 400 min |
| ⭐⭐⭐ Hard | 18 | P1.9-1.13, P2.6-2.10, P3.3-3.4, P3.6, P3.9-3.10, P4.4, P5.4-5.6, P5.9 | 540 min |
| **Total** | **50** | **All** | **1120 min** |

---

## 🎯 STUDY PLAN OPTIONS

### OPTION A: Core Only (Essential Skills)
**15 problems, 360 min (6 hours)**

**Day 1:** P1.1, P1.2, P1.3, P1.4, P1.5
**Day 2:** P2.1, P2.2, P2.3, P2.4, P2.5
**Day 3:** P3.1, P3.2, P3.3, P3.4, P3.5

---

### OPTION B: Core + Practice (Reinforcement)
**25 problems, 750 min (12.5 hours)**

**Day 1:** P1.1-P1.10 (tree DP)
**Day 2:** P2.1-P2.5 (DAG DP basics)
**Day 3:** P3.1-P3.5 (bitmask DP)
**Day 4:** P4.1-P4.3 (space optimization)
**Day 5:** P5.1-P5.5 (mixed DP)

---

### OPTION C: Complete Mastery (50 problems)
**50 problems, 1120 min (18.7 hours)**

**Complete all problems in roadmap**

---

## ✅ PROBLEM-SOLVING CHECKLIST

For each problem, verify:

- [ ] **Understand:** Can explain problem in own words
- [ ] **Identify:** Recognize it as tree/DAG/bitmask DP
- [ ] **Design:** State definition clear before coding
- [ ] **Trace:** Manually verify on small example
- [ ] **Code:** Implement correctly
- [ ] **Test:** Try edge cases
- [ ] **Optimize:** Discuss time/space trade-offs
- [ ] **Learn:** Understand solution, not just got AC

---

## 📈 MASTERY TRACKING

| Problem | Attempt 1 | Attempt 2 | Attempt 3 | Status |
| :--- | :--- | :--- | :--- | :--- |
| P1.1 | __ min | __ min | __ min | ✅/⚠️/❌ |
| P1.2 | __ min | __ min | __ min | ✅/⚠️/❌ |
| ... | ... | ... | ... | ... |

---

**File Status:** ✅ Complete | **Last Updated:** January 26, 2026

