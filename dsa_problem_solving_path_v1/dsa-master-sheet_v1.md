# DSA Master Problem Sheet – Optimized Learning Path

This enhanced problem sheet incorporates:
- **Anchor problems** (master these first for each pattern)
- **Learning tips** for effective practice
- **Core concepts** needed before attempting
- **Spaced repetition schedule**
- **Pattern connections** to related problems
- **Difficulty progression** within each phase

Use this with the "Learning Spiral" method: Read theory → Solve anchor problems → Master pattern → Solve variations.

---

## 📋 How to Use This Sheet

### Daily Problem Template (50 minutes)
1. **[15 min] Analysis** - Read problem, identify pattern, list test cases
2. **[25 min] Implementation** - Code solution, test edge cases
3. **[10 min] Review** - Compare with optimal, note pattern, add to tracker

### Spaced Repetition Schedule
- **First solve**: Today
- **Review 1**: 3 days later
- **Review 2**: 1 week later
- **Review 3**: 2 weeks later
- **Mastered**: After 3 successful reviews

### Problem Priority System
- **🔑 ANCHOR** - Master this first (essential for pattern understanding)
- **⭐ HIGH VALUE** - Common in interviews
- **🎯 PRACTICE** - Variation for pattern reinforcement
- **💡 ADVANCED** - Complex application of pattern

---

## Phase 0 – Foundations & Complexity
**Duration:** Week 1 | **Goal:** Build confidence, learn LeetCode interface

### 🎯 Learning Focus
- Big-O notation and complexity analysis
- Basic recursion and recurrence relations
- Problem-solving workflow on LeetCode

### Problems (7 Easy)

| # | Problem | LC# | Priority | Pattern | Concepts to Know | Anchor Tip |
|---|---------|-----|----------|---------|------------------|------------|
| 1 | Binary Search | 704 | 🔑 | Binary Search | Search space halving, loop invariants | **MASTER THIS FIRST** - Foundation for all binary search problems. Practice both iterative and recursive versions. |
| 2 | First Bad Version | 278 | 🎯 | Binary Search | API constraints, minimization | Apply binary search template from #704. Focus on boundary conditions. |
| 3 | Climbing Stairs | 70 | 🔑 | DP (1D) | Fibonacci sequence, recurrence | **MASTER THIS FIRST** - Simplest DP problem. Understand state definition: dp[i] = ways to reach step i. |
| 4 | Power of Two | 231 | 🎯 | Bit Manipulation | Binary representation, bit tricks | Learn the pattern: n & (n-1) removes rightmost set bit. |
| 5 | Power of Three | 326 | 🎯 | Math | Logarithm properties | Understand log-based verification: log₃(n) should be integer. |
| 6 | Counting Bits | 338 | ⭐ | DP + Bit Manipulation | Hamming weight, DP optimization | Connect to #231. Pattern: count[i] = count[i >> 1] + (i & 1) |
| 7 | Find Pivot Index | 724 | 🔑 | Prefix Sum | Running sum, balance point | **MASTER THIS FIRST** - Core prefix sum technique. leftSum = totalSum - rightSum - nums[i]. |

**Week 1 Success Metrics:**
- ✅ Solve all 7 problems
- ✅ Can explain binary search template from memory
- ✅ Understand basic DP state definition
- ✅ Comfortable with LeetCode submission process

---

## Phase 1 – Arrays, Strings, and Hashing
**Duration:** Weeks 2-3 | **Goal:** Master fundamental patterns (two-pointers, sliding window, hash maps)

### 🎯 Learning Focus
- Two-pointer technique (same direction, opposite direction)
- Sliding window (fixed size, variable size)
- Hash map for O(1) lookups
- In-place array manipulation

### Part A: Hash Maps & Basic Two-Pointers (Week 2)

| # | Problem | LC# | Priority | Pattern | Concepts to Know | Anchor Tip |
|---|---------|-----|----------|---------|------------------|------------|
| 1 | Two Sum | 1 | 🔑 | Hash Map | Complement search, O(1) lookup | **MASTER THIS FIRST** - The hash map pattern. Key insight: store value→index mapping. Check `target - num` in map. |
| 2 | Contains Duplicate | 217 | 🎯 | Hash Set | Uniqueness checking | Apply hash set for existence checks. Pattern: add to set, check if already exists. |
| 3 | Valid Anagram | 242 | ⭐ | Hash Map / Sorting | Character frequency | Two approaches: frequency map or sorted comparison. Map is O(n), sort is O(n log n). |
| 4 | Group Anagrams | 49 | 🔑 | Hash Map | String as key, grouping | **MASTER THIS FIRST** - Advanced hash map usage. Key: sorted string. Value: list of anagrams. |
| 5 | Valid Palindrome | 125 | 🔑 | Two Pointers (opposite) | Character filtering, pointer movement | **MASTER THIS FIRST** - Opposite direction two-pointers. Skip non-alphanumeric, compare case-insensitive. |
| 6 | Merge Sorted Array | 88 | ⭐ | Two Pointers (same) | In-place merge, backward iteration | Key insight: fill from back to avoid overwriting. Three pointers: p1, p2, p (write position). |
| 7 | Move Zeroes | 283 | 🎯 | Two Pointers (same) | In-place swap, order preservation | Slow pointer = position for next non-zero. Fast pointer = current element. |
| 8 | Best Time to Buy and Sell Stock | 121 | 🔑 | Greedy / DP | Min tracking, max profit | **MASTER THIS FIRST** - Track minimum price seen so far, update max profit. One pass. |
| 9 | Majority Element | 169 | ⭐ | Boyer-Moore Voting | Majority definition (>n/2), cancellation | Advanced: Boyer-Moore algorithm. Count increases/decreases. Candidate survives. |
| 10 | Single Number | 136 | 🎯 | Bit Manipulation (XOR) | XOR properties: a^a=0, a^0=a | XOR all elements: duplicates cancel, single remains. |

**Week 2 Checkpoint:**
- ✅ Can implement two-pointer template (same & opposite direction)
- ✅ Know when to use hash map vs hash set
- ✅ Understand in-place array manipulation

### Part B: Sliding Window & Advanced Two-Pointers (Week 3)

| # | Problem | LC# | Priority | Pattern | Concepts to Know | Anchor Tip |
|---|---------|-----|----------|---------|------------------|------------|
| 11 | Longest Substring Without Repeating Chars | 3 | 🔑 | Sliding Window (variable) | Window expansion/contraction, seen set | **MASTER THIS FIRST** - Variable size window. Expand right, contract left when duplicate found. Track with set. |
| 12 | Longest Repeating Character Replacement | 424 | 💡 | Sliding Window (variable) | Character frequency, k replacements | Advanced window. Track max frequency. Window valid if: length - maxFreq ≤ k. |
| 13 | Container With Most Water | 11 | 🔑 | Two Pointers (opposite) | Area calculation, greedy movement | **MASTER THIS FIRST** - Move pointer with shorter height. Why? Taller one might find better partner. |
| 14 | 3Sum | 15 | 🔑 | Two Pointers + Sorting | Duplicate handling, fixed + moving | **MASTER THIS FIRST** - Fix one element, two-pointer on rest. Skip duplicates carefully. Sort first. |
| 15 | Product of Array Except Self | 238 | ⭐ | Prefix/Suffix | Space optimization, running products | Two passes: left products, then right products. Result[i] = leftProduct * rightProduct. |
| 16 | Top K Frequent Elements | 347 | ⭐ | Hash Map + Heap | Frequency counting, selection | Frequency map → Min heap of size k. O(n log k). Alternative: bucket sort O(n). |
| 17 | Subarray Sum Equals K | 560 | 💡 | Prefix Sum + Hash Map | Cumulative sum, sum lookup | Key insight: if prefixSum[j] - prefixSum[i] = k, then subarray[i+1:j] = k. Store prefix sums in map. |
| 18 | Rotate Array | 189 | 🎯 | Array | Cyclic rotation, reversal technique | Three reverses: reverse all, reverse [0:k], reverse [k:n]. |
| 19 | Missing Number | 268 | 🎯 | Math / Bit Manipulation | Sum formula, XOR | Two approaches: expectedSum - actualSum, or XOR all with indices. |

**Week 3 Success Metrics:**
- ✅ Can identify when to use sliding window vs two-pointers
- ✅ Understand variable vs fixed size windows
- ✅ Master prefix sum technique

---

## Phase 2 – Linked Lists, Stacks, and Queues
**Duration:** Week 4 | **Goal:** Pointer manipulation mastery

### 🎯 Learning Focus
- Fast/slow pointer technique (Floyd's algorithm)
- Dummy node pattern for list operations
- Stack for LIFO problems (parentheses, monotonic stack)
- Queue for BFS (covered later in graphs)

### Problems (21 total: 7 Easy, 12 Medium, 2 Hard - Focus on first 12)

| # | Problem | LC# | Priority | Pattern | Concepts to Know | Anchor Tip |
|---|---------|-----|----------|---------|------------------|------------|
| 1 | Reverse Linked List | 206 | 🔑 | Linked List | Pointer reversal, iterative vs recursive | **MASTER THIS FIRST** - Most fundamental list operation. Three pointers: prev, curr, next. Practice both approaches. |
| 2 | Merge Two Sorted Lists | 21 | 🔑 | Linked List | Merging logic, dummy node | **MASTER THIS FIRST** - Dummy node pattern. Compare heads, advance smaller, link. |
| 3 | Linked List Cycle | 141 | 🔑 | Fast/Slow Pointers | Floyd's cycle detection | **MASTER THIS FIRST** - Fast moves 2, slow moves 1. If they meet, cycle exists. |
| 4 | Linked List Cycle II | 142 | 💡 | Fast/Slow Pointers | Cycle entry point | After meeting, reset one to head. Move both by 1. Meeting point = cycle start. Math proof important. |
| 5 | Middle of the Linked List | 876 | 🎯 | Fast/Slow Pointers | Middle finding | Fast moves 2, slow moves 1. When fast reaches end, slow at middle. |
| 6 | Palindrome Linked List | 234 | ⭐ | Fast/Slow + Reverse | Middle + reverse + compare | Combine #876 and #206: find middle, reverse second half, compare. |
| 7 | Valid Parentheses | 20 | 🔑 | Stack | Matching pairs, LIFO | **MASTER THIS FIRST** - Stack for brackets. Opening: push. Closing: pop and match. |
| 8 | Remove Nth Node From End | 19 | ⭐ | Two Pointers | N-ahead pointer, one-pass | Move fast n steps ahead. Then move both. When fast reaches end, slow at target-1. |
| 9 | Reorder List | 143 | 💡 | Multiple patterns | Middle + reverse + merge | Combine: find middle (#876), reverse second half (#206), merge alternately. |
| 10 | Add Two Numbers | 2 | ⭐ | Linked List | Digit-by-digit, carry handling | Track carry. Create new nodes. Handle different lengths. |
| 11 | Daily Temperatures | 739 | 🔑 | Monotonic Stack | Next greater element, index stack | **MASTER THIS FIRST** - Decreasing monotonic stack. Push indices. Pop when curr > stack top. |
| 12 | Min Stack | 155 | ⭐ | Stack Design | Auxiliary stack, O(1) min | Two stacks: main and min. Min stack tracks minimum at each level. |

**Week 4 Checkpoint (First 12 problems):**
- ✅ Can implement fast/slow pointer pattern from memory
- ✅ Know when to use dummy node
- ✅ Understand monotonic stack concept
- ✅ Comfortable with pointer manipulation

**Remaining 9 problems** (Swap Nodes in Pairs, Reverse in k-Group, Rotate List, etc.) - attempt after mastering first 12.

---

## Phase 3 – Trees, BSTs, and Balanced Trees
**Duration:** Weeks 5-6 | **Goal:** Recursion on trees, traversal mastery

### 🎯 Learning Focus
- Recursive tree patterns (top-down, bottom-up)
- Tree traversals (pre, in, post, level-order)
- BST properties and invariants
- Height-balanced concepts

### Part A: Basic Tree Recursion (Week 5)

| # | Problem | LC# | Priority | Pattern | Concepts to Know | Anchor Tip |
|---|---------|-----|----------|---------|------------------|------------|
| 1 | Maximum Depth of Binary Tree | 104 | 🔑 | DFS (bottom-up) | Height calculation, base case | **MASTER THIS FIRST** - Simplest recursion. Base: null→0. Recursive: 1 + max(left, right). |
| 2 | Invert Binary Tree | 226 | 🔑 | DFS | Subtree swapping | **MASTER THIS FIRST** - Swap left and right at each node. Recurse on both. |
| 3 | Same Tree | 100 | 🎯 | DFS | Structural + value equality | Base cases: both null→true, one null→false. Recursive: values match AND recurse. |
| 4 | Symmetric Tree | 101 | ⭐ | DFS | Mirror checking, dual recursion | Helper function: isMirror(left, right). Check left.left vs right.right, left.right vs right.left. |
| 5 | Diameter of Binary Tree | 543 | 🔑 | DFS (bottom-up) | Path length, global maximum | **MASTER THIS FIRST** - At each node: diameter = leftHeight + rightHeight. Track global max. |
| 6 | Balanced Binary Tree | 110 | 🎯 | DFS | Height balance definition | Check at each node: |leftHeight - rightHeight| ≤ 1 AND both subtrees balanced. |
| 7 | Path Sum | 112 | 🎯 | DFS (top-down) | Target sum, root-to-leaf | Subtract current value from target. Base: leaf with target=0. |
| 8 | Binary Tree Level Order Traversal | 102 | 🔑 | BFS | Level separation, queue | **MASTER THIS FIRST** - Queue-based BFS. Process level by level. Track level size. |
| 9 | Binary Tree Right Side View | 199 | ⭐ | BFS / DFS | Rightmost per level | BFS: take last element of each level. DFS: right-first traversal tracking level. |

**Week 5 Checkpoint:**
- ✅ Can write basic tree recursion (top-down and bottom-up)
- ✅ Understand BFS level-order traversal
- ✅ Know when to track global vs return value

### Part B: BST & Advanced Trees (Week 6)

| # | Problem | LC# | Priority | Pattern | Concepts to Know | Anchor Tip |
|---|---------|-----|----------|---------|------------------|------------|
| 10 | Validate Binary Search Tree | 98 | 🔑 | BST | In-order traversal, range checking | **MASTER THIS FIRST** - In-order gives sorted array. Or pass min/max range recursively. |
| 11 | Lowest Common Ancestor of BST | 235 | 🎯 | BST | BST property, ancestor logic | Use BST property: if p,q on different sides of node, that's LCA. |
| 12 | Lowest Common Ancestor of Binary Tree | 236 | 🔑 | DFS | Ancestor definition, path tracking | **MASTER THIS FIRST** - If node finds p or q, return node. If left and right both return, current is LCA. |
| 13 | Kth Smallest Element in a BST | 230 | ⭐ | BST + In-order | In-order traversal, counting | In-order traversal gives sorted order. Count to kth element. |
| 14 | Convert Sorted Array to BST | 108 | 🎯 | BST + Divide & Conquer | Middle element, recursion | Pick middle as root. Left half → left subtree. Right half → right subtree. |
| 15 | Binary Tree Maximum Path Sum | 124 | 💡 | DFS (bottom-up) | Path definition, global max | Hard. At each node: maxPath = node + max(0,left) + max(0,right). Track global max. |
| 16 | Serialize and Deserialize Binary Tree | 297 | 💡 | Tree + Design | Encoding/decoding, delimiters | Hard. Pre-order with null markers. Encode: "1,2,null,null,3,". Decode: split and reconstruct. |

**Week 6 Checkpoint:**
- ✅ Understand BST properties and how to use them
- ✅ Can implement LCA for both BST and binary tree
- ✅ Master in-order traversal for BST problems

**Remaining problems** (traversal variants, construction problems) - practice after core patterns mastered.

---

## Phase 4 – Graphs and Graph Traversal
**Duration:** Weeks 7-8 | **Goal:** DFS/BFS on graphs, connectivity patterns

### 🎯 Learning Focus
- Graph representation (adjacency list, matrix, implicit grid)
- DFS for exploration and connectivity
- BFS for shortest path in unweighted graphs
- Topological sort for DAGs

### Part A: Grid DFS/BFS (Week 7)

| # | Problem | LC# | Priority | Pattern | Concepts to Know | Anchor Tip |
|---|---------|-----|----------|---------|------------------|------------|
| 1 | Flood Fill | 733 | 🔑 | DFS / BFS | Connected component coloring | **MASTER THIS FIRST** - Simplest graph problem. 4-directional DFS. Mark visited by changing color. |
| 2 | Number of Islands | 200 | 🔑 | DFS / BFS | Component counting, grid traversal | **MASTER THIS FIRST** - Count components. Each DFS marks entire island. Iterate grid, DFS on unvisited land. |
| 3 | Max Area of Island | 695 | 🎯 | DFS / BFS | Area calculation | Variation of #200. Return area from DFS instead of just marking. |
| 4 | Rotting Oranges | 994 | 🔑 | BFS | Multi-source BFS, time tracking | **MASTER THIS FIRST** - Add all rotten oranges to queue initially. Track time with level. |
| 5 | Surrounded Regions | 130 | ⭐ | DFS / BFS | Border search, marking | DFS from border 'O's. Mark as safe. Then flip remaining 'O's to 'X'. |
| 6 | Pacific Atlantic Water Flow | 417 | 💡 | DFS / BFS | Multi-source, intersection | Two DFS: from Pacific borders, from Atlantic borders. Return intersection. |

**Week 7 Checkpoint:**
- ✅ Can implement grid DFS/BFS from memory
- ✅ Understand multi-source BFS pattern
- ✅ Know when DFS vs BFS is better

### Part B: Graph Structures & Topological Sort (Week 8)

| # | Problem | LC# | Priority | Pattern | Concepts to Know | Anchor Tip |
|---|---------|-----|----------|---------|------------------|------------|
| 7 | Clone Graph | 133 | ⭐ | DFS / BFS | Deep copy, node mapping | Use hash map: old node → new node. DFS to create and link copies. |
| 8 | Number of Provinces | 547 | 🎯 | DFS / BFS / Union-Find | Adjacency matrix, components | Apply #200 logic to adjacency matrix. Count DFS start points. |
| 9 | Course Schedule | 207 | 🔑 | Topological Sort | Cycle detection, DAG | **MASTER THIS FIRST** - Topological sort with Kahn's algorithm. Track in-degrees. |
| 10 | Course Schedule II | 210 | ⭐ | Topological Sort | Ordering, Kahn's algorithm | Extension of #207. Return the actual order (BFS traversal result). |
| 11 | Graph Valid Tree | 261 | 💡 | DFS / Union-Find | Tree conditions: n-1 edges, no cycles, connected | Tree if: edges = n-1 AND all nodes reachable from one DFS. |

**Week 8 Checkpoint:**
- ✅ Can implement topological sort (Kahn's algorithm)
- ✅ Understand cycle detection in directed graphs
- ✅ Know tree conditions for graphs

**Remaining problems** (Word Ladder, advanced connectivity) - attempt after core patterns.

---

## Phase 5 – Sorting, Searching, and Divide & Conquer
**Duration:** Week 9 | **Goal:** Binary search mastery, interval problems

### 🎯 Learning Focus
- Binary search variants (boundary search, answer space search)
- Interval merging and scheduling
- Sorting as preprocessing step

### Problems (18 total - Focus on first 12)

| # | Problem | LC# | Priority | Pattern | Concepts to Know | Anchor Tip |
|---|---------|-----|----------|---------|------------------|------------|
| 1 | Merge Intervals | 56 | 🔑 | Intervals + Sorting | Overlap detection, merging | **MASTER THIS FIRST** - Sort by start. If curr.start ≤ last.end, merge. Else add new interval. |
| 2 | Insert Interval | 57 | ⭐ | Intervals | Insertion, merge logic | Three phases: before overlap, merge overlapping, after overlap. |
| 3 | Non-overlapping Intervals | 435 | 💡 | Greedy + Intervals | Interval scheduling, removal | Sort by end time. Greedy: keep interval with earliest end. |
| 4 | Meeting Rooms | 252 | 🎯 | Intervals + Sorting | Overlap check | Sort by start. Check if any start < previous end. |
| 5 | Search in Rotated Sorted Array | 33 | 🔑 | Binary Search | Pivot detection, half selection | **MASTER THIS FIRST** - Check which half is sorted. If target in sorted half, search there. Else other half. |
| 6 | Find Minimum in Rotated Sorted Array | 153 | 🎯 | Binary Search | Minimum finding | Compare mid with right. If mid > right, min in right half. Else left half. |
| 7 | Find First and Last Position | 34 | 🔑 | Binary Search | Boundary search | **MASTER THIS FIRST** - Two binary searches: leftmost position, rightmost position. |
| 8 | Koko Eating Bananas | 875 | 🔑 | Binary Search on Answer | Feasibility check, answer space | **MASTER THIS FIRST** - Binary search on speed k. Check if possible within h hours. |
| 9 | Search a 2D Matrix | 74 | 🎯 | Binary Search | Matrix as sorted array | Treat as 1D array: row = mid / cols, col = mid % cols. |
| 10 | Find Peak Element | 162 | ⭐ | Binary Search | Peak definition, gradient | If mid > mid+1, peak in left half (including mid). Else right half. |
| 11 | Sort Colors | 75 | ⭐ | Two Pointers | Dutch National Flag, partition | Three-way partition. Low pointer for 0s, high for 2s, iterate with mid. |
| 12 | Capacity To Ship Packages Within D Days | 1011 | 💡 | Binary Search on Answer | Capacity search, simulation | Similar to #875. Binary search on capacity. Simulate to check feasibility. |

**Week 9 Checkpoint:**
- ✅ Can implement binary search variants (leftmost, rightmost, answer space)
- ✅ Understand interval merging pattern
- ✅ Master binary search on answer space

---

## Phase 6 – Greedy Algorithms
**Duration:** Week 10 | **Goal:** Greedy choice recognition

### 🎯 Learning Focus
- Identifying greedy choice property
- Proof techniques (exchange argument)
- Activity selection pattern

### Problems (13 total - Focus on first 8)

| # | Problem | LC# | Priority | Pattern | Concepts to Know | Anchor Tip |
|---|---------|-----|----------|---------|------------------|------------|
| 1 | Maximum Subarray | 53 | 🔑 | Greedy / DP | Kadane's algorithm | **MASTER THIS FIRST** - Keep running sum. If negative, reset to current element. Track max. |
| 2 | Jump Game | 55 | 🔑 | Greedy | Reachability, furthest position | **MASTER THIS FIRST** - Track furthest reachable. At each i, check if i ≤ furthest. Update furthest. |
| 3 | Jump Game II | 45 | 💡 | Greedy | Minimum jumps, range | Track current range end and next range end. Jump when reaching current end. |
| 4 | Gas Station | 134 | ⭐ | Greedy | Circular array, surplus/deficit | If total gas ≥ total cost, solution exists. Start from where deficit becomes positive. |
| 5 | Partition Labels | 763 | ⭐ | Greedy | Last occurrence, partition | Track last occurrence of each char. Extend partition until reaching last occurrence of all chars in partition. |
| 6 | Best Time to Buy and Sell Stock II | 122 | 🎯 | Greedy | Multiple transactions | Collect all positive differences. Greedy: sell whenever profit available. |
| 7 | Assign Cookies | 455 | 🎯 | Greedy + Sorting | Matching, satisfaction | Sort both. Assign smallest cookie that satisfies each child. |
| 8 | Minimum Number of Arrows to Burst Balloons | 452 | 💡 | Greedy + Intervals | Interval scheduling | Sort by end. Shoot at end of current balloon. Skip overlapping balloons. |

**Week 10 Checkpoint:**
- ✅ Can recognize greedy choice property
- ✅ Understand why greedy works (exchange argument)
- ✅ Master Kadane's algorithm and jump game pattern

---

## Phase 7 – Dynamic Programming
**Duration:** Weeks 11-13 | **Goal:** DP pattern mastery

### 🎯 Learning Focus
- State definition and recurrence relations
- Memoization vs tabulation
- Space optimization techniques
- DP on sequences (LIS, LCS, substring/subsequence)

### Part A: 1D DP Foundations (Week 11)

| # | Problem | LC# | Priority | Pattern | Concepts to Know | Anchor Tip |
|---|---------|-----|----------|---------|------------------|------------|
| 1 | Climbing Stairs | 70 | 🔑 | 1D DP | Fibonacci relation | **MASTER THIS FIRST** - Already solved in Phase 0. Review: dp[i] = dp[i-1] + dp[i-2]. |
| 2 | Min Cost Climbing Stairs | 746 | 🎯 | 1D DP | Cost accumulation | Extension: dp[i] = cost[i] + min(dp[i-1], dp[i-2]). Can start from 0 or 1. |
| 3 | House Robber | 198 | 🔑 | 1D DP | Non-adjacent selection | **MASTER THIS FIRST** - dp[i] = max(rob i + dp[i-2], skip i + dp[i-1]). |
| 4 | House Robber II | 213 | ⭐ | 1D DP | Circular array | Split into two sub-problems: rob houses [0:n-2] or [1:n-1]. Take max. |
| 5 | Coin Change | 322 | 🔑 | Unbounded Knapsack | Minimum coins | **MASTER THIS FIRST** - dp[i] = min coins to make amount i. dp[i] = min(dp[i], dp[i-coin] + 1). |
| 6 | Decode Ways | 91 | ⭐ | 1D DP | Digit grouping, validity | dp[i] = ways to decode s[0:i]. Check single digit and double digit validity. |
| 7 | Maximum Product Subarray | 152 | 💡 | 1D DP | Max/min tracking | Track both max and min (negative numbers). maxProd[i] = max(num, num*maxProd, num*minProd). |
| 8 | Word Break | 139 | 🔑 | 1D DP | Substring matching | **MASTER THIS FIRST** - dp[i] = can break s[0:i]. Check all possible last words. |

**Week 11 Checkpoint:**
- ✅ Can define DP state for 1D problems
- ✅ Understand recurrence relation formulation
- ✅ Know house robber and coin change patterns

### Part B: 2D DP & Sequences (Week 12)

| # | Problem | LC# | Priority | Pattern | Concepts to Know | Anchor Tip |
|---|---------|-----|----------|---------|------------------|------------|
| 9 | Unique Paths | 62 | 🔑 | 2D Grid DP | Path counting | **MASTER THIS FIRST** - dp[i][j] = ways to reach (i,j). dp[i][j] = dp[i-1][j] + dp[i][j-1]. |
| 10 | Unique Paths II | 63 | 🎯 | 2D Grid DP | Obstacles | Extension: if obstacle, dp[i][j] = 0. Else same as #62. |
| 11 | Minimum Path Sum | 64 | ⭐ | 2D Grid DP | Cost minimization | dp[i][j] = grid[i][j] + min(dp[i-1][j], dp[i][j-1]). |
| 12 | Longest Common Subsequence | 1143 | 🔑 | 2D Sequence DP | LCS definition | **MASTER THIS FIRST** - If s1[i]==s2[j]: dp[i][j]=dp[i-1][j-1]+1. Else: max(dp[i-1][j], dp[i][j-1]). |
| 13 | Edit Distance | 72 | 🔑 | 2D Sequence DP | Levenshtein distance | **MASTER THIS FIRST** - Three operations: insert, delete, replace. Take min cost. |
| 14 | Longest Increasing Subsequence | 300 | 🔑 | 1D DP + Binary Search | LIS definition | **MASTER THIS FIRST** - O(n²): dp[i] = max length ending at i. O(n log n): maintain increasing array, binary search position. |
| 15 | Longest Palindromic Substring | 5 | ⭐ | 2D DP / Expand | Palindrome checking | Two approaches: 2D DP table or expand around center. Expand is O(n²) time, O(1) space. |
| 16 | Palindromic Substrings | 647 | 🎯 | 2D DP / Expand | Counting palindromes | Similar to #5. Count instead of tracking longest. |

**Week 12 Checkpoint:**
- ✅ Can build 2D DP tables
- ✅ Understand LCS and Edit Distance patterns
- ✅ Master both grid DP and sequence DP

### Part C: Advanced DP (Week 13)

| # | Problem | LC# | Priority | Pattern | Concepts to Know | Anchor Tip |
|---|---------|-----|----------|---------|------------------|------------|
| 17 | Partition Equal Subset Sum | 416 | 🔑 | 0/1 Knapsack | Subset sum | **MASTER THIS FIRST** - Can partition into two equal sums? Target = totalSum/2. 0/1 knapsack DP. |
| 18 | Target Sum | 494 | ⭐ | DP / Backtracking | +/- assignment | Convert to subset sum: sum(P) - sum(N) = target. Transform to 0/1 knapsack. |
| 19 | Combination Sum IV | 377 | 💡 | Unbounded Knapsack | Order matters | Similar to coin change, but combinations where order matters. |
| 20 | Best Time to Buy and Sell Stock with Cooldown | 309 | 💡 | State Machine DP | Cooldown period | Three states: hold, sold, cooldown. Track all three. |

**Week 13 Checkpoint:**
- ✅ Master 0/1 knapsack pattern
- ✅ Understand state machine DP
- ✅ Can optimize space (rolling array)

**Remaining DP problems** (Regular Expression, Wildcard, Burst Balloons, etc.) - Advanced interval DP for later.

---

## Phase 8 – Backtracking, Recursion, and Combinatorial Search
**Duration:** Week 14 | **Goal:** Backtracking template mastery

### 🎯 Learning Focus
- Backtracking template (explore, mark, recurse, unmark)
- Pruning strategies
- Generating combinations, permutations, subsets

### Problems (18 total - Focus on first 10)

| # | Problem | LC# | Priority | Pattern | Concepts to Know | Anchor Tip |
|---|---------|-----|----------|---------|------------------|------------|
| 1 | Subsets | 78 | 🔑 | Backtracking | Include/exclude decision | **MASTER THIS FIRST** - For each element: include it (recurse), then exclude (backtrack). |
| 2 | Subsets II | 90 | ⭐ | Backtracking | Duplicate handling | Sort first. Skip duplicates: if i > start && nums[i] == nums[i-1], continue. |
| 3 | Permutations | 46 | 🔑 | Backtracking | Order matters | **MASTER THIS FIRST** - Swap current with each remaining. Recurse. Swap back. |
| 4 | Permutations II | 47 | 💡 | Backtracking | Duplicates in permutations | Use frequency map or used array. Skip if already used at this level. |
| 5 | Combinations | 77 | 🎯 | Backtracking | Fixed size, starting index | Choose k elements from n. Use start index to avoid duplicates. |
| 6 | Combination Sum | 39 | 🔑 | Backtracking | Unlimited use, target | **MASTER THIS FIRST** - Can reuse elements. At each step: include current (stay at index) or exclude (move to next). |
| 7 | Combination Sum II | 40 | ⭐ | Backtracking | Limited use, duplicates | Sort and skip duplicates. Each element used once. Move to next after using. |
| 8 | Letter Combinations of a Phone Number | 17 | 🎯 | Backtracking | Mapping, Cartesian product | Map digit to letters. Backtrack through digits. |
| 9 | Generate Parentheses | 22 | ⭐ | Backtracking | Valid parentheses | Track open and close count. Add '(' if open < n. Add ')' if close < open. |
| 10 | Word Search | 79 | 🔑 | Backtracking + Grid | Grid search, path tracking | **MASTER THIS FIRST** - 4-directional DFS. Mark visited, recurse on neighbors, unmark. |

**Week 14 Checkpoint:**
- ✅ Can write backtracking template from memory
- ✅ Understand when to use backtracking vs DP
- ✅ Master pruning for duplicates

**Remaining problems** (Palindrome Partitioning, N-Queens, Sudoku) - Complex applications for later.

---

## Phase 9 – Heaps, Priority Queues, and Advanced Data Structures
**Duration:** Week 15 | **Goal:** Heap operations, Union-Find, Trie

### 🎯 Learning Focus
- Min heap / max heap operations
- Top K pattern with heaps
- Union-Find for connectivity
- Trie for prefix operations

### Problems (17 total - Focus on first 10)

| # | Problem | LC# | Priority | Pattern | Concepts to Know | Anchor Tip |
|---|---------|-----|----------|---------|------------------|------------|
| 1 | Kth Largest Element in an Array | 215 | 🔑 | Heap / Quickselect | Min heap of size k | **MASTER THIS FIRST** - Maintain min heap of size k. Root is kth largest. O(n log k). |
| 2 | Top K Frequent Elements | 347 | 🔑 | Heap + Hash Map | Frequency counting | **MASTER THIS FIRST** - Frequency map → Min heap of size k (by frequency). |
| 3 | K Closest Points to Origin | 973 | 🎯 | Heap | Distance calculation | Same pattern as #1. Max heap of size k (by distance). |
| 4 | Find Median from Data Stream | 295 | 🔑 | Two Heaps Design | Balance maintenance | **MASTER THIS FIRST** - Max heap (left half), min heap (right half). Balance sizes. Median from roots. |
| 5 | Merge k Sorted Lists | 23 | ⭐ | Heap | K-way merge | Min heap of list heads. Pop smallest, add next from that list. |
| 6 | Last Stone Weight | 1046 | 🎯 | Max Heap | Simulation | Simple max heap application. Smash two largest, add result back. |
| 7 | Number of Connected Components | 323 | 🔑 | Union-Find | Component counting | **MASTER THIS FIRST** - Union-Find template. Count initial components, decrease on each union. |
| 8 | Graph Valid Tree | 261 | ⭐ | Union-Find | Cycle detection | Tree if: n-1 edges AND no cycles (all unions successful). |
| 9 | Implement Trie (Prefix Tree) | 208 | 🔑 | Trie Design | Insert, search, prefix | **MASTER THIS FIRST** - TrieNode with children map and isEnd flag. Standard operations. |
| 10 | Design Add and Search Words DS | 211 | 💡 | Trie + Backtracking | Wildcard search | Extension of #208. Backtrack on '.' to try all children. |

**Week 15 Checkpoint:**
- ✅ Can implement min/max heap operations
- ✅ Master two-heaps pattern for median
- ✅ Understand Union-Find template
- ✅ Can implement Trie from scratch

---

## Phase 10 – Graph Shortest Paths, MST, and Flow (Optional Deep Dive)
**Duration:** Week 16 | **Goal:** Advanced graph algorithms

### Problems (12 total - Focus on first 6)

| # | Problem | LC# | Priority | Pattern | Concepts to Know | Anchor Tip |
|---|---------|-----|----------|---------|------------------|------------|
| 1 | Network Delay Time | 743 | 🔑 | Dijkstra | Single-source shortest path | **MASTER THIS FIRST** - Min heap with (distance, node). Relaxation: if newDist < dist[node], update. |
| 2 | Cheapest Flights Within K Stops | 787 | ⭐ | BFS / DP | K-constrained shortest path | BFS with (stops, node, cost). Stop when stops > k. |
| 3 | Path with Maximum Probability | 1514 | 🎯 | Dijkstra variant | Probability product | Max heap by probability. Multiply probabilities instead of adding distances. |
| 4 | Minimum Cost to Connect All Points | 1584 | 🔑 | MST (Prim/Kruskal) | Distance calculation | **MASTER THIS FIRST** - Prim's: grow tree from one point. Or Kruskal: union-find on all edges. |
| 5 | Critical Connections in a Network | 1192 | 💡 | Tarjan's Algorithm | Bridges in graph | Advanced: DFS with discovery time and low-link values. |
| 6 | Swim in Rising Water | 778 | 💡 | Binary Search / Dijkstra | Time binary search | Binary search on water level. Check reachability at each level. |

**Week 16 Checkpoint:**
- ✅ Can implement Dijkstra's algorithm
- ✅ Understand MST algorithms (Prim/Kruskal)
- ✅ Know when to use shortest path vs MST

---

## Phase 11 – Interview-Oriented Mixed Practice
**Duration:** Weeks 17-18 | **Goal:** Speed, pattern recognition, design problems

### Problems (25 total - Focus on first 12)

| # | Problem | LC# | Priority | Pattern | Concepts to Know | Anchor Tip |
|---|---------|-----|----------|---------|------------------|------------|
| 1 | LRU Cache | 146 | 🔑 | Design | Doubly-linked list + hash map | **MASTER THIS FIRST** - Hash map for O(1) lookup. DLL for O(1) removal/addition. Move to head on access. |
| 2 | Trapping Rain Water | 42 | 🔑 | Two Pointers / Stack | Water volume, min height | **MASTER THIS FIRST** - Two pointers: water[i] = min(maxLeft, maxRight) - height[i]. |
| 3 | Longest Consecutive Sequence | 128 | ⭐ | Hash Set | O(n) sequence | Add all to set. For each number, if it's start of sequence (num-1 not in set), count length. |
| 4 | Basic Calculator II | 227 | ⭐ | Stack | Operator precedence | Stack for numbers. Process * and / immediately. + and - add to stack. |
| 5 | Sliding Window Maximum | 239 | 💡 | Monotonic Deque | Max in window | Decreasing deque. Remove elements outside window and smaller than current. |
| 6 | Largest Rectangle in Histogram | 84 | 💡 | Monotonic Stack | Height boundaries | Increasing stack of indices. When current < stack top, pop and calculate area. |
| 7 | Valid Sudoku | 36 | 🎯 | Hash Set | Constraint checking | Three sets per row/col/box. Check uniqueness. |
| 8 | Rotate Image | 48 | 🎯 | Matrix | Transpose + reflect | Two steps: transpose (swap [i][j] with [j][i]), then reverse each row. |
| 9 | Spiral Matrix | 54 | 🎯 | Matrix | Boundary management | Track four boundaries: top, bottom, left, right. Move in spiral. |
| 10 | Insert Delete GetRandom O(1) | 380 | ⭐ | Array + Hash Map Design | Index mapping | Hash map: val→index. Array: actual values. Swap with last for deletion. |
| 11 | Time Based Key-Value Store | 981 | ⭐ | Hash Map + Binary Search | Timestamp lookup | Map key to list of (timestamp, value). Binary search for largest timestamp ≤ target. |
| 12 | Encode and Decode Strings | 271 | ⭐ | String Design | Delimiter strategy | Encode: "4:word5:hello". Include length to handle any delimiter in string. |

**Weeks 17-18 Success Metrics:**
- ✅ Can solve Medium in <30 minutes
- ✅ Recognize patterns quickly
- ✅ Handle design problems (LRU, Time-Based Store)
- ✅ Ready for mock interviews

---

## 📊 Progress Tracking Template

### Weekly Tracker

| Week | Phase | Problems Solved | Patterns Mastered | Review Sessions | Notes |
|------|-------|----------------|-------------------|-----------------|-------|
| 1 | Phase 0 | 7/7 | Binary Search, Basic DP, Prefix Sum | 0 | All Easy ✓ |
| 2 | Phase 1A | 10/10 | Hash Map, Two Pointers | 0 | Confidence building |
| 3 | Phase 1B | 9/9 | Sliding Window | 1 | Reviewed Week 2 problems |
| ... | ... | ... | ... | ... | ... |

### Spaced Repetition Tracker

| Problem | First Solve | Review 1 (3d) | Review 2 (1w) | Review 3 (2w) | Status |
|---------|-------------|---------------|---------------|---------------|--------|
| Two Sum (1) | 2026-02-10 ✓ | 2026-02-13 ⏰ | | | In Progress |
| Binary Search (704) | 2026-02-09 ✓ | 2026-02-12 ✓ | 2026-02-16 ⏰ | | In Progress |
| ... | ... | ... | ... | ... | ... |

### Pattern Cheatsheet (Build as you learn)

**Two Pointers (Opposite Direction)**
```python
# Template
left, right = 0, len(arr) - 1
while left < right:
    if condition_met:
        # process
        left += 1
    else:
        right -= 1
```
**Problems Mastered:** Valid Palindrome (125), Container With Most Water (11), 3Sum (15)

**Sliding Window (Variable Size)**
```python
# Template
left = 0
for right in range(len(arr)):
    # Expand: add arr[right] to window
    while window_invalid:
        # Contract: remove arr[left] from window
        left += 1
    # Update result with valid window
```
**Problems Mastered:** Longest Substring Without Repeating Chars (3), Longest Repeating Character Replacement (424)

---

## 🎯 4-Week Quick Start Plan

### Week 1: Foundation (Phase 0)
**Goal:** 7 problems, build confidence
- Mon: Binary Search (704), First Bad Version (278)
- Tue: Climbing Stairs (70), Power of Two (231)
- Wed: Power of Three (326), Counting Bits (338)
- Thu: Find Pivot Index (724)
- Fri: Review all 7 problems, create pattern notes
- Sat-Sun: Rest or light review

### Week 2: Arrays & Hash Maps (Phase 1A)
**Goal:** 10 problems, master hash map pattern
- Mon-Thu: 2-3 problems per day (Easy focus)
- Fri: Review week's problems
- Sat: Attempt first Medium problem (Two Sum variants)
- Sun: Create "Hash Map Pattern" cheatsheet

### Week 3: Sliding Window & Two Pointers (Phase 1B)
**Goal:** 9 problems, master window patterns
- Mon-Thu: 2 problems per day (mix Easy/Medium)
- Fri: Review + attempt 3Sum (15)
- Sat: Spaced repetition review of Week 1-2
- Sun: Update pattern cheatsheet

### Week 4: Linked Lists (Phase 2, first 12 problems)
**Goal:** Master pointer manipulation
- Mon-Thu: 3 problems per day
- Fri: Review + fast/slow pointer practice
- Sat: Spaced repetition review
- Sun: Assessment: Can you solve Easy problems in <20 min?

**After Week 4:**
- ✅ 38 problems solved
- ✅ 6-7 core patterns mastered
- ✅ Established review habits
- ✅ Ready to continue independently

---

## 💡 Key Success Principles

### 1. **Anchor Problems First**
Always solve 🔑 ANCHOR problems before variations. These teach the pattern foundation.

### 2. **Spaced Repetition is Non-Negotiable**
Solve once → Review 3 days → Review 1 week → Review 2 weeks = Long-term retention

### 3. **Pattern Over Problem Count**
Better to master 50 problems deeply (with 3 reviews each) than solve 200 problems once.

### 4. **Build Your Own Cheatsheet**
Write patterns in your own words. Include: when to use, template code, common gotchas, problems solved.

### 5. **Time-Box Problem Attempts**
Stuck after 30 minutes? Read hints. Still stuck after 45 minutes? Read solution. Reattempt in 3 days.

### 6. **Connect Problems to Patterns**
After solving, ask: "What pattern did this use?" Add to your cheatsheet under that pattern.

### 7. **Explain to Learn**
After solving, explain solution out loud as if teaching a beginner. If you can't, you don't understand it yet.

---

## 📈 Final Statistics

- **Total Problems:** 235
- **Anchor Problems (Master First):** 47 problems marked with 🔑
- **High Value (Common Interview):** 58 problems marked with ⭐
- **Advanced (Complex Applications):** 32 problems marked with 💡
- **Practice Variations:** 98 problems marked with 🎯

**Recommended Path:**
- **Weeks 1-4:** Foundation (38 problems)
- **Weeks 5-8:** Trees + Graphs (46 problems, focus on first 30)
- **Weeks 9-13:** Sorting + Greedy + DP (61 problems, focus on anchors)
- **Weeks 14-16:** Backtracking + Heaps + Advanced (47 problems, focus on first 26)
- **Weeks 17-18:** Mixed Practice + Review (43 problems, focus on design)

**Total: 16-18 weeks of focused study at 10-12 hours/week**

---

Good luck on your DSA mastery journey! Remember: **Consistency > Intensity**. Better to solve 2 problems/day for 90 days than 20 problems/day for 9 days. 🚀
