# DSA Hybrid Problem List - Complete Collection

## Overview

This hybrid problem list combines **all 235+ problems** from the master sheet with enhanced metadata for optimal learning:

- **Problem Priority**: 🔑 Anchor | ⭐ High Value | 💡 Advanced | 🎯 Practice
- **Phase Tracking**: Organized by 11 phases
- **Pattern Tags**: Primary and secondary patterns
- **Difficulty**: Easy | Medium | Hard
- **Time Estimates**: Based on skill level
- **Learning Notes**: Key concepts and templates
- **Connections**: Related problems for pattern reinforcement

---

## How to Use This List

### Priority System
1. **🔑 ANCHOR (47 problems)** - Master these FIRST for each pattern
2. **⭐ HIGH VALUE (58 problems)** - Common in interviews, practice after anchors
3. **💡 ADVANCED (32 problems)** - Complex applications, attempt after mastering basics
4. **🎯 PRACTICE (98 problems)** - Variations for pattern reinforcement

### Study Approach
1. Solve all 🔑 ANCHOR problems in a phase first
2. Then tackle ⭐ HIGH VALUE problems
3. Practice 🎯 PRACTICE problems for reinforcement
4. Attempt 💡 ADVANCED when confident

### Time Estimates
- **Beginner**: First time seeing pattern
- **Intermediate**: Familiar with pattern
- **Advanced**: Can solve variations quickly

---

## Phase 0 – Foundations & Complexity (Week 1)

**Total: 7 Problems (All Easy)**

| # | Problem | LC# | Diff | Priority | Pattern | Time (B/I/A) | Key Concept |
|---|---------|-----|------|----------|---------|--------------|-------------|
| 1 | Binary Search | 704 | E | 🔑 | Binary Search | 25/15/10 | Search space halving, template |
| 2 | First Bad Version | 278 | E | 🎯 | Binary Search | 20/12/8 | Minimization variant |
| 3 | Climbing Stairs | 70 | E | 🔑 | 1D DP | 25/15/10 | Fibonacci DP, state definition |
| 4 | Power of Two | 231 | E | 🎯 | Bit Manipulation | 15/10/5 | n & (n-1) trick |
| 5 | Power of Three | 326 | E | 🎯 | Math | 15/10/5 | Logarithm properties |
| 6 | Counting Bits | 338 | E | ⭐ | DP + Bit | 20/15/10 | count[i] = count[i>>1] + (i&1) |
| 7 | Find Pivot Index | 724 | E | 🔑 | Prefix Sum | 20/12/8 | leftSum = total - rightSum - curr |

**Week 1 Checkpoint**: ✅ Binary search template | ✅ Basic DP | ✅ Prefix sum | ✅ Bit manipulation basics

---

## Phase 1 – Arrays, Strings, and Hashing (Weeks 2-3)

**Total: 19 Problems (13 Easy, 6 Medium)**

### Part A: Hash Maps & Basic Two-Pointers (Week 2)

| # | Problem | LC# | Diff | Priority | Pattern | Time (B/I/A) | Key Concept |
|---|---------|-----|------|----------|---------|--------------|-------------|
| 1 | Two Sum | 1 | E | 🔑 | Hash Map | 20/12/7 | Complement search, value→index |
| 2 | Contains Duplicate | 217 | E | 🎯 | Hash Set | 15/10/5 | Existence check |
| 3 | Valid Anagram | 242 | E | ⭐ | Hash Map | 20/12/8 | Frequency map or sort |
| 4 | Group Anagrams | 49 | M | 🔑 | Hash Map | 30/20/12 | Sorted string as key |
| 5 | Valid Palindrome | 125 | E | 🔑 | Two Pointers | 20/15/10 | Opposite direction, filter chars |
| 6 | Merge Sorted Array | 88 | E | ⭐ | Two Pointers | 25/15/10 | Backward iteration, in-place |
| 7 | Move Zeroes | 283 | E | 🎯 | Two Pointers | 20/12/8 | Slow/fast pointers, order preserved |
| 8 | Best Time to Buy and Sell Stock | 121 | E | 🔑 | Greedy/DP | 25/15/10 | Track min, update max profit |
| 9 | Majority Element | 169 | E | ⭐ | Boyer-Moore | 25/18/12 | Voting algorithm, cancellation |
| 10 | Single Number | 136 | E | 🎯 | Bit (XOR) | 15/10/5 | XOR all, duplicates cancel |

**Week 2 Checkpoint**: ✅ Hash map patterns | ✅ Two-pointer (same & opposite) | ✅ In-place manipulation

### Part B: Sliding Window & Advanced Two-Pointers (Week 3)

| # | Problem | LC# | Diff | Priority | Pattern | Time (B/I/A) | Key Concept |
|---|---------|-----|------|----------|---------|--------------|-------------|
| 11 | Longest Substring Without Repeating | 3 | M | 🔑 | Sliding Window | 35/25/15 | Variable window, expand/contract |
| 12 | Longest Repeating Character Replacement | 424 | M | 💡 | Sliding Window | 40/30/20 | Window valid: len - maxFreq ≤ k |
| 13 | Container With Most Water | 11 | M | 🔑 | Two Pointers | 30/20/12 | Move shorter height, greedy |
| 14 | 3Sum | 15 | M | 🔑 | Two Pointers + Sort | 40/30/20 | Fix one, two-pointer on rest |
| 15 | Product of Array Except Self | 238 | M | ⭐ | Prefix/Suffix | 30/20/15 | Left products, then right |
| 16 | Top K Frequent Elements | 347 | M | ⭐ | Hash Map + Heap | 30/22/15 | Frequency map + min heap |
| 17 | Subarray Sum Equals K | 560 | M | 💡 | Prefix Sum + Map | 35/25/18 | prefixSum[j] - prefixSum[i] = k |
| 18 | Rotate Array | 189 | M | 🎯 | Array | 25/18/12 | Three reverses trick |
| 19 | Missing Number | 268 | E | 🎯 | Math/Bit | 15/10/5 | Sum formula or XOR |

**Week 3 Checkpoint**: ✅ Sliding window (variable) | ✅ 3Sum pattern | ✅ Prefix sum advanced

**Pattern Connections**:
- Two Sum (1) → 3Sum (15) → 4Sum
- Longest Substring (3) → Longest Repeating (12) → Minimum Window Substring
- Valid Palindrome (125) → Container With Water (11) → Trapping Rain Water (42)

---

## Phase 2 – Linked Lists, Stacks, and Queues (Week 4)

**Total: 21 Problems (7 Easy, 12 Medium, 2 Hard) - Focus on first 12**

| # | Problem | LC# | Diff | Priority | Pattern | Time (B/I/A) | Key Concept |
|---|---------|-----|------|----------|---------|--------------|-------------|
| 1 | Reverse Linked List | 206 | E | 🔑 | Linked List | 25/15/10 | Three pointers: prev, curr, next |
| 2 | Merge Two Sorted Lists | 21 | E | 🔑 | Linked List | 25/18/12 | Dummy node pattern |
| 3 | Linked List Cycle | 141 | E | 🔑 | Fast/Slow | 20/15/10 | Floyd's algorithm, detect cycle |
| 4 | Linked List Cycle II | 142 | M | 💡 | Fast/Slow | 35/25/18 | Find cycle entry point |
| 5 | Middle of the Linked List | 876 | E | 🎯 | Fast/Slow | 15/10/7 | Fast 2x, slow 1x |
| 6 | Palindrome Linked List | 234 | E | ⭐ | Fast/Slow + Reverse | 30/22/15 | Find middle + reverse + compare |
| 7 | Valid Parentheses | 20 | E | 🔑 | Stack | 20/15/10 | LIFO, matching pairs |
| 8 | Remove Nth Node From End | 19 | M | ⭐ | Two Pointers | 30/20/15 | N-ahead pointer, one-pass |
| 9 | Reorder List | 143 | M | 💡 | Multiple Patterns | 40/30/22 | Middle + reverse + merge |
| 10 | Add Two Numbers | 2 | M | ⭐ | Linked List | 30/22/15 | Digit-by-digit, carry handling |
| 11 | Daily Temperatures | 739 | M | 🔑 | Monotonic Stack | 35/25/18 | Decreasing stack, next greater |
| 12 | Min Stack | 155 | M | ⭐ | Stack Design | 30/22/15 | Auxiliary min stack, O(1) getMin |

**Advanced Problems (Attempt after mastering first 12)**:
| 13 | Swap Nodes in Pairs | 24 | M | 🎯 | Linked List | 30/22/15 |
| 14 | Reverse Nodes in k-Group | 25 | H | 💡 | Linked List | 50/40/30 |
| 15 | Rotate List | 61 | M | 🎯 | Linked List | 30/22/15 |
| 16 | Partition List | 86 | M | 🎯 | Linked List | 30/22/15 |
| 17 | Sort List | 148 | M | 💡 | Merge Sort | 40/30/22 |
| 18 | Intersection of Two Linked Lists | 160 | E | 🎯 | Two Pointers | 25/18/12 |
| 19 | Remove Linked List Elements | 203 | E | 🎯 | Linked List | 20/15/10 |
| 20 | Odd Even Linked List | 328 | M | 🎯 | Linked List | 30/22/15 |
| 21 | Flatten Multilevel Doubly Linked List | 430 | M | 💡 | DFS | 40/30/22 |

**Week 4 Checkpoint**: ✅ Fast/slow pointer | ✅ Dummy node | ✅ Monotonic stack | ✅ Pointer manipulation

**Pattern Connections**:
- Reverse List (206) → Reverse in k-Group (25) → Reorder List (143)
- Linked List Cycle (141) → Cycle II (142) → Palindrome List (234)
- Valid Parentheses (20) → Daily Temperatures (739) → Min Stack (155)

---

## Phase 3 – Trees, BSTs, and Balanced Trees (Weeks 5-6)

**Total: 23+ Problems**

### Part A: Basic Tree Recursion (Week 5)

| # | Problem | LC# | Diff | Priority | Pattern | Time (B/I/A) | Key Concept |
|---|---------|-----|------|----------|---------|--------------|-------------|
| 1 | Maximum Depth of Binary Tree | 104 | E | 🔑 | DFS (Bottom-Up) | 20/12/8 | 1 + max(left, right) |
| 2 | Invert Binary Tree | 226 | E | 🔑 | DFS | 20/12/8 | Swap children, recurse |
| 3 | Same Tree | 100 | E | 🎯 | DFS | 20/15/10 | Structural + value equality |
| 4 | Symmetric Tree | 101 | E | ⭐ | DFS | 25/18/12 | Mirror checking, dual recursion |
| 5 | Diameter of Binary Tree | 543 | E | 🔑 | DFS (Bottom-Up) | 30/22/15 | leftHeight + rightHeight |
| 6 | Balanced Binary Tree | 110 | E | 🎯 | DFS | 25/18/12 | Height balance: |left - right| ≤ 1 |
| 7 | Path Sum | 112 | E | 🎯 | DFS (Top-Down) | 20/15/10 | Subtract curr, check leaf == 0 |
| 8 | Binary Tree Level Order Traversal | 102 | M | 🔑 | BFS | 30/22/15 | Queue, level separation |
| 9 | Binary Tree Right Side View | 199 | M | ⭐ | BFS/DFS | 30/22/15 | Last element per level |

**Week 5 Checkpoint**: ✅ Basic tree recursion | ✅ BFS level-order | ✅ Top-down vs bottom-up

### Part B: BST & Advanced Trees (Week 6)

| # | Problem | LC# | Diff | Priority | Pattern | Time (B/I/A) | Key Concept |
|---|---------|-----|------|----------|---------|--------------|-------------|
| 10 | Validate Binary Search Tree | 98 | M | 🔑 | BST | 35/25/18 | Range validation (min, max) |
| 11 | Lowest Common Ancestor of BST | 235 | M | 🎯 | BST | 25/18/12 | Use BST property, split point |
| 12 | Lowest Common Ancestor of Binary Tree | 236 | M | 🔑 | DFS | 35/25/18 | Return node if found, LCA if both |
| 13 | Kth Smallest Element in BST | 230 | M | ⭐ | BST + Inorder | 30/22/15 | Inorder gives sorted, count to k |
| 14 | Convert Sorted Array to BST | 108 | E | 🎯 | BST + D&C | 25/18/12 | Middle as root, recurse halves |
| 15 | Binary Tree Maximum Path Sum | 124 | H | 💡 | DFS (Bottom-Up) | 50/40/30 | node + max(0,left) + max(0,right) |
| 16 | Serialize and Deserialize Binary Tree | 297 | H | 💡 | Tree Design | 50/40/30 | Preorder with null markers |

**Additional Tree Problems (Practice after core)**:
| 17 | Subtree of Another Tree | 572 | E | 🎯 | DFS | 30/22/15 |
| 18 | Construct Tree from Preorder & Inorder | 105 | M | 💡 | Tree Construction | 45/35/25 |
| 19 | Binary Tree Zigzag Level Order | 103 | M | 🎯 | BFS | 30/22/15 |
| 20 | Count Good Nodes in Binary Tree | 1448 | M | ⭐ | DFS | 30/22/15 |
| 21 | Binary Tree Paths | 257 | E | 🎯 | DFS | 25/18/12 |
| 22 | Sum of Left Leaves | 404 | E | 🎯 | DFS | 20/15/10 |
| 23 | Find Leaves of Binary Tree | 366 | M | 💡 | DFS | 35/25/18 |

**Weeks 5-6 Checkpoint**: ✅ BST properties | ✅ LCA patterns | ✅ Inorder traversal | ✅ Tree construction

**Pattern Connections**:
- Max Depth (104) → Diameter (543) → Max Path Sum (124)
- Validate BST (98) → Kth Smallest (230) → BST Iterator (173)
- Level Order (102) → Right Side View (199) → Zigzag (103)

---

## Phase 4 – Graphs and Graph Traversal (Weeks 7-8)

**Total: 16+ Problems**

### Part A: Grid DFS/BFS (Week 7)

| # | Problem | LC# | Diff | Priority | Pattern | Time (B/I/A) | Key Concept |
|---|---------|-----|------|----------|---------|--------------|-------------|
| 1 | Flood Fill | 733 | E | 🔑 | DFS/BFS | 20/15/10 | 4-directional, color change |
| 2 | Number of Islands | 200 | M | 🔑 | DFS/BFS | 30/22/15 | Component counting, mark visited |
| 3 | Max Area of Island | 695 | M | 🎯 | DFS/BFS | 30/22/15 | Return area from DFS |
| 4 | Rotting Oranges | 994 | M | 🔑 | Multi-Source BFS | 35/25/18 | Add all rotten to queue, track time |
| 5 | Surrounded Regions | 130 | M | ⭐ | DFS/BFS | 35/28/20 | Border search, mark safe |
| 6 | Pacific Atlantic Water Flow | 417 | M | 💡 | Multi-Source DFS | 40/30/22 | Two DFS, intersection |

**Week 7 Checkpoint**: ✅ Grid DFS/BFS | ✅ Multi-source BFS | ✅ DFS vs BFS choice

### Part B: Graph Structures & Topological Sort (Week 8)

| # | Problem | LC# | Diff | Priority | Pattern | Time (B/I/A) | Key Concept |
|---|---------|-----|------|----------|---------|--------------|-------------|
| 7 | Clone Graph | 133 | M | ⭐ | DFS/BFS | 35/25/18 | Hash map: old→new, DFS clone |
| 8 | Number of Provinces | 547 | M | 🎯 | DFS/Union-Find | 30/22/15 | Adjacency matrix, components |
| 9 | Course Schedule | 207 | M | 🔑 | Topological Sort | 35/25/18 | Kahn's algorithm, cycle detection |
| 10 | Course Schedule II | 210 | M | ⭐ | Topological Sort | 35/25/18 | Return actual order |
| 11 | Graph Valid Tree | 261 | M | 💡 | DFS/Union-Find | 35/28/20 | n-1 edges, no cycles, connected |

**Advanced Graph Problems (Practice after core)**:
| 12 | Word Ladder | 127 | H | 💡 | BFS | 50/40/30 |
| 13 | Word Ladder II | 126 | H | 💡 | BFS + Backtracking | 60/50/40 |
| 14 | Alien Dictionary | 269 | H | 💡 | Topological Sort | 50/40/30 |
| 15 | Evaluate Division | 399 | M | 💡 | DFS/Union-Find | 40/30/22 |
| 16 | Accounts Merge | 721 | M | ⭐ | Union-Find | 40/30/22 |

**Weeks 7-8 Checkpoint**: ✅ Topological sort (Kahn's) | ✅ Cycle detection | ✅ Multi-source patterns | ✅ Tree conditions

**Pattern Connections**:
- Flood Fill (733) → Number of Islands (200) → Max Area (695)
- Rotting Oranges (994) → Pacific Atlantic (417) → 01 Matrix (542)
- Course Schedule (207) → Course Schedule II (210) → Alien Dictionary (269)

---

## Phase 5 – Sorting, Searching, and Divide & Conquer (Week 9)

**Total: 18 Problems (Focus on first 12)**

| # | Problem | LC# | Diff | Priority | Pattern | Time (B/I/A) | Key Concept |
|---|---------|-----|------|----------|---------|--------------|-------------|
| 1 | Merge Intervals | 56 | M | 🔑 | Intervals + Sort | 30/22/15 | Sort by start, merge overlaps |
| 2 | Insert Interval | 57 | M | ⭐ | Intervals | 35/25/18 | Three phases: before, merge, after |
| 3 | Non-overlapping Intervals | 435 | M | 💡 | Greedy + Intervals | 35/28/20 | Sort by end, greedy keep earliest |
| 4 | Meeting Rooms | 252 | E | 🎯 | Intervals + Sort | 20/15/10 | Check if start < prev end |
| 5 | Search in Rotated Sorted Array | 33 | M | 🔑 | Binary Search | 35/25/18 | Check which half sorted |
| 6 | Find Minimum in Rotated Array | 153 | M | 🎯 | Binary Search | 30/22/15 | Compare mid with right |
| 7 | Find First and Last Position | 34 | M | 🔑 | Binary Search | 35/25/18 | Two binary searches: left, right |
| 8 | Koko Eating Bananas | 875 | M | 🔑 | BS on Answer | 35/28/20 | Binary search on speed k |
| 9 | Search a 2D Matrix | 74 | M | 🎯 | Binary Search | 25/18/12 | Treat as 1D: row=mid//cols |
| 10 | Find Peak Element | 162 | M | ⭐ | Binary Search | 30/22/15 | Follow gradient: mid > mid+1 |
| 11 | Sort Colors | 75 | M | ⭐ | Two Pointers | 30/22/15 | Dutch flag, three-way partition |
| 12 | Capacity To Ship Packages | 1011 | M | 💡 | BS on Answer | 40/30/22 | Binary search on capacity |

**Additional Problems (Practice after core)**:
| 13 | Meeting Rooms II | 253 | M | ⭐ | Heap | 35/25/18 |
| 14 | Minimum Arrows to Burst Balloons | 452 | M | 💡 | Greedy + Intervals | 35/28/20 |
| 15 | Search a 2D Matrix II | 240 | M | ⭐ | Binary Search | 35/25/18 |
| 16 | Median of Two Sorted Arrays | 4 | H | 💡 | Binary Search | 60/50/40 |
| 17 | Split Array Largest Sum | 410 | H | 💡 | BS on Answer | 50/40/30 |
| 18 | Find K Closest Elements | 658 | M | ⭐ | Binary Search | 35/25/18 |

**Week 9 Checkpoint**: ✅ Binary search variants | ✅ Interval merging | ✅ BS on answer space | ✅ 2D matrix search

**Pattern Connections**:
- Binary Search (704) → Rotated Array (33) → Find Min (153)
- Merge Intervals (56) → Insert Interval (57) → Non-overlapping (435)
- Koko Bananas (875) → Ship Packages (1011) → Split Array (410)

---

## Phase 6 – Greedy Algorithms (Week 10)

**Total: 13 Problems (Focus on first 8)**

| # | Problem | LC# | Diff | Priority | Pattern | Time (B/I/A) | Key Concept |
|---|---------|-----|------|----------|---------|--------------|-------------|
| 1 | Maximum Subarray | 53 | M | 🔑 | Greedy/DP | 25/18/12 | Kadane's algorithm |
| 2 | Jump Game | 55 | M | 🔑 | Greedy | 30/22/15 | Track furthest reachable |
| 3 | Jump Game II | 45 | M | 💡 | Greedy | 35/28/20 | Track current end, next end |
| 4 | Gas Station | 134 | M | ⭐ | Greedy | 35/28/20 | Total gas ≥ cost, start at surplus |
| 5 | Partition Labels | 763 | M | ⭐ | Greedy | 35/25/18 | Last occurrence, extend partition |
| 6 | Best Time to Buy and Sell Stock II | 122 | M | 🎯 | Greedy | 25/18/12 | Collect all positive differences |
| 7 | Assign Cookies | 455 | E | 🎯 | Greedy + Sort | 20/15/10 | Smallest cookie satisfies child |
| 8 | Min Arrows to Burst Balloons | 452 | M | 💡 | Greedy + Intervals | 35/28/20 | Sort by end, shoot at end |

**Additional Problems (Advanced)**:
| 9 | Queue Reconstruction by Height | 406 | M | 💡 | Greedy | 40/30/22 |
| 10 | Task Scheduler | 621 | M | ⭐ | Greedy | 40/30/22 |
| 11 | Minimum Number of Taps | 1326 | H | 💡 | Greedy | 50/40/30 |
| 12 | Bag of Tokens | 948 | M | 🎯 | Greedy | 30/22/15 |
| 13 | Boats to Save People | 881 | M | 🎯 | Greedy | 30/22/15 |

**Week 10 Checkpoint**: ✅ Greedy choice property | ✅ Exchange argument | ✅ Kadane's algorithm | ✅ Jump game pattern

**Pattern Connections**:
- Maximum Subarray (53) → Maximum Product Subarray (152)
- Jump Game (55) → Jump Game II (45) → Frog Jump (403)
- Partition Labels (763) → Merge Intervals (56)

---

## Phase 7 – Dynamic Programming (Weeks 11-13)

**Total: 40+ Problems**

### Part A: 1D DP Foundations (Week 11)

| # | Problem | LC# | Diff | Priority | Pattern | Time (B/I/A) | Key Concept |
|---|---------|-----|------|----------|---------|--------------|-------------|
| 1 | Climbing Stairs | 70 | E | 🔑 | 1D DP | 20/12/8 | dp[i] = dp[i-1] + dp[i-2] |
| 2 | Min Cost Climbing Stairs | 746 | E | 🎯 | 1D DP | 25/18/12 | cost[i] + min(dp[i-1], dp[i-2]) |
| 3 | House Robber | 198 | M | 🔑 | 1D DP | 30/22/15 | max(rob i + dp[i-2], dp[i-1]) |
| 4 | House Robber II | 213 | M | ⭐ | 1D DP | 35/25/18 | Two sub-problems: [0:n-2], [1:n-1] |
| 5 | Coin Change | 322 | M | 🔑 | Unbounded Knapsack | 35/28/20 | dp[i] = min(dp[i], dp[i-coin] + 1) |
| 6 | Decode Ways | 91 | M | ⭐ | 1D DP | 35/28/20 | Check single and double digit |
| 7 | Maximum Product Subarray | 152 | M | 💡 | 1D DP | 40/30/22 | Track max and min (negatives) |
| 8 | Word Break | 139 | M | 🔑 | 1D DP | 35/28/20 | dp[i] = can break s[0:i] |

**Week 11 Checkpoint**: ✅ 1D DP state definition | ✅ Recurrence relation | ✅ House robber pattern | ✅ Coin change

### Part B: 2D DP & Sequences (Week 12)

| # | Problem | LC# | Diff | Priority | Pattern | Time (B/I/A) | Key Concept |
|---|---------|-----|------|----------|---------|--------------|-------------|
| 9 | Unique Paths | 62 | M | 🔑 | 2D Grid DP | 25/18/12 | dp[i][j] = dp[i-1][j] + dp[i][j-1] |
| 10 | Unique Paths II | 63 | M | 🎯 | 2D Grid DP | 30/22/15 | Handle obstacles: dp[i][j] = 0 |
| 11 | Minimum Path Sum | 64 | M | ⭐ | 2D Grid DP | 30/22/15 | grid + min(dp[i-1][j], dp[i][j-1]) |
| 12 | Longest Common Subsequence | 1143 | M | 🔑 | 2D Sequence DP | 40/30/22 | If match: dp[i-1][j-1]+1; else: max |
| 13 | Edit Distance | 72 | H | 🔑 | 2D Sequence DP | 50/40/30 | Insert, delete, replace operations |
| 14 | Longest Increasing Subsequence | 300 | M | 🔑 | 1D DP + BS | 40/30/22 | O(n²) DP or O(n log n) binary search |
| 15 | Longest Palindromic Substring | 5 | M | ⭐ | 2D DP / Expand | 35/28/20 | Expand around center or DP table |
| 16 | Palindromic Substrings | 647 | M | 🎯 | 2D DP / Expand | 30/25/18 | Count instead of track longest |

**Week 12 Checkpoint**: ✅ 2D DP tables | ✅ LCS and Edit Distance | ✅ Grid DP | ✅ Sequence DP

### Part C: Advanced DP (Week 13)

| # | Problem | LC# | Diff | Priority | Pattern | Time (B/I/A) | Key Concept |
|---|---------|-----|------|----------|---------|--------------|-------------|
| 17 | Partition Equal Subset Sum | 416 | M | 🔑 | 0/1 Knapsack | 40/30/22 | Target = totalSum/2, knapsack DP |
| 18 | Target Sum | 494 | M | ⭐ | DP/Backtracking | 40/30/22 | Transform to subset sum problem |
| 19 | Combination Sum IV | 377 | M | 💡 | Unbounded Knapsack | 35/28/20 | Order matters variant |
| 20 | Best Time Stock with Cooldown | 309 | M | 💡 | State Machine DP | 45/35/25 | Three states: hold, sold, cooldown |

**Additional DP Problems (Advanced)**:
| 21 | Regular Expression Matching | 10 | H | 💡 | 2D DP | 60/50/40 |
| 22 | Wildcard Matching | 44 | H | 💡 | 2D DP | 60/50/40 |
| 23 | Burst Balloons | 312 | H | 💡 | Interval DP | 60/50/40 |
| 24 | Distinct Subsequences | 115 | H | 💡 | 2D DP | 50/40/30 |
| 25 | Interleaving String | 97 | M | 💡 | 2D DP | 45/35/25 |
| 26 | Longest Valid Parentheses | 32 | H | 💡 | Stack/DP | 50/40/30 |
| 27 | Minimum Window Substring | 76 | H | 💡 | Sliding Window | 50/40/30 |
| 28 | Trapping Rain Water | 42 | H | 💡 | Two Pointers/DP | 45/35/25 |
| 29 | Maximal Rectangle | 85 | H | 💡 | Stack + DP | 60/50/40 |
| 30 | Scramble String | 87 | H | 💡 | 3D DP | 60/50/40 |

**Weeks 11-13 Checkpoint**: ✅ 0/1 knapsack | ✅ State machine DP | ✅ Space optimization | ✅ 2D sequence patterns

**Pattern Connections**:
- Climbing Stairs (70) → Min Cost (746) → House Robber (198)
- Coin Change (322) → Combination Sum IV (377) → Perfect Squares (279)
- Unique Paths (62) → Min Path Sum (64) → Triangle (120)
- LCS (1143) → Edit Distance (72) → Distinct Subsequences (115)
- House Robber (198) → House Robber II (213) → House Robber III (337)

---

## Phase 8 – Backtracking, Recursion, and Combinatorial Search (Week 14)

**Total: 18 Problems (Focus on first 10)**

| # | Problem | LC# | Diff | Priority | Pattern | Time (B/I/A) | Key Concept |
|---|---------|-----|------|----------|---------|--------------|-------------|
| 1 | Subsets | 78 | M | 🔑 | Backtracking | 30/22/15 | Include/exclude decision tree |
| 2 | Subsets II | 90 | M | ⭐ | Backtracking | 35/25/18 | Sort, skip duplicates |
| 3 | Permutations | 46 | M | 🔑 | Backtracking | 35/25/18 | Swap with each remaining |
| 4 | Permutations II | 47 | M | 💡 | Backtracking | 40/30/22 | Frequency map, skip used |
| 5 | Combinations | 77 | M | 🎯 | Backtracking | 30/22/15 | Fixed size k, start index |
| 6 | Combination Sum | 39 | M | 🔑 | Backtracking | 35/28/20 | Unlimited use, stay at index |
| 7 | Combination Sum II | 40 | M | ⭐ | Backtracking | 35/28/20 | Limited use, skip duplicates |
| 8 | Letter Combinations Phone | 17 | M | 🎯 | Backtracking | 25/18/12 | Digit→letters mapping |
| 9 | Generate Parentheses | 22 | M | ⭐ | Backtracking | 35/25/18 | Track open/close count |
| 10 | Word Search | 79 | M | 🔑 | Backtracking + Grid | 40/30/22 | 4-directional, mark visited |

**Additional Problems (Advanced)**:
| 11 | Palindrome Partitioning | 131 | M | 💡 | Backtracking | 45/35/25 |
| 12 | N-Queens | 51 | H | 💡 | Backtracking | 50/40/30 |
| 13 | N-Queens II | 52 | H | 💡 | Backtracking | 50/40/30 |
| 14 | Sudoku Solver | 37 | H | 💡 | Backtracking | 60/50/40 |
| 15 | Restore IP Addresses | 93 | M | 🎯 | Backtracking | 35/28/20 |
| 16 | Combination Sum III | 216 | M | 🎯 | Backtracking | 30/22/15 |
| 17 | Word Search II | 212 | H | 💡 | Backtracking + Trie | 60/50/40 |
| 18 | Expression Add Operators | 282 | H | 💡 | Backtracking | 60/50/40 |

**Week 14 Checkpoint**: ✅ Backtracking template | ✅ Pruning duplicates | ✅ Combinations vs permutations | ✅ Grid backtracking

**Pattern Connections**:
- Subsets (78) → Subsets II (90) → Power Set variations
- Permutations (46) → Permutations II (47) → Next Permutation (31)
- Combination Sum (39) → Combination Sum II (40) → Combination Sum III (216)
- Generate Parentheses (22) → Remove Invalid Parentheses (301)

---

## Phase 9 – Heaps, Priority Queues, and Advanced Data Structures (Week 15)

**Total: 17 Problems (Focus on first 10)**

| # | Problem | LC# | Diff | Priority | Pattern | Time (B/I/A) | Key Concept |
|---|---------|-----|------|----------|---------|--------------|-------------|
| 1 | Kth Largest Element in Array | 215 | M | 🔑 | Heap/Quickselect | 30/22/15 | Min heap of size k, O(n log k) |
| 2 | Top K Frequent Elements | 347 | M | 🔑 | Heap + Hash Map | 35/25/18 | Frequency map + min heap |
| 3 | K Closest Points to Origin | 973 | M | 🎯 | Heap | 30/22/15 | Max heap of size k by distance |
| 4 | Find Median from Data Stream | 295 | H | 🔑 | Two Heaps | 50/40/30 | Max heap (left) + min heap (right) |
| 5 | Merge k Sorted Lists | 23 | H | ⭐ | Heap | 45/35/25 | K-way merge with min heap |
| 6 | Last Stone Weight | 1046 | E | 🎯 | Max Heap | 20/15/10 | Simple heap simulation |
| 7 | Number of Connected Components | 323 | M | 🔑 | Union-Find | 30/22/15 | Template, count components |
| 8 | Graph Valid Tree | 261 | M | ⭐ | Union-Find | 35/28/20 | n-1 edges, no cycles |
| 9 | Implement Trie | 208 | M | 🔑 | Trie Design | 35/25/18 | Insert, search, startsWith |
| 10 | Design Add Search Words DS | 211 | M | 💡 | Trie + Backtracking | 40/30/22 | Wildcard search with '.' |

**Additional Problems (Advanced)**:
| 11 | Kth Smallest in Sorted Matrix | 378 | M | ⭐ | Heap/Binary Search | 40/30/22 |
| 12 | Ugly Number II | 264 | M | 💡 | Heap/DP | 40/30/22 |
| 13 | Find K Pairs with Smallest Sums | 373 | M | 💡 | Heap | 45/35/25 |
| 14 | Smallest Range Covering K Lists | 632 | H | 💡 | Heap | 60/50/40 |
| 15 | Replace Words | 648 | M | ⭐ | Trie | 35/25/18 |
| 16 | Word Search II | 212 | H | 💡 | Trie + Backtracking | 60/50/40 |
| 17 | Redundant Connection | 684 | M | ⭐ | Union-Find | 35/28/20 |

**Week 15 Checkpoint**: ✅ Heap operations | ✅ Two-heaps pattern | ✅ Union-Find template | ✅ Trie from scratch

**Pattern Connections**:
- Kth Largest (215) → Kth Smallest Matrix (378) → Find Median (295)
- Top K Frequent (347) → K Closest Points (973) → K Closest Elements (658)
- Trie (208) → Add Search Words (211) → Word Search II (212)
- Union-Find (323) → Graph Valid Tree (261) → Accounts Merge (721)

---

## Phase 10 – Graph Shortest Paths, MST, and Flow (Week 16)

**Total: 12 Problems (Focus on first 6)**

| # | Problem | LC# | Diff | Priority | Pattern | Time (B/I/A) | Key Concept |
|---|---------|-----|------|----------|---------|--------------|-------------|
| 1 | Network Delay Time | 743 | M | 🔑 | Dijkstra | 45/35/25 | Single-source shortest path |
| 2 | Cheapest Flights Within K Stops | 787 | M | ⭐ | BFS/DP | 45/35/25 | K-constrained shortest path |
| 3 | Path with Maximum Probability | 1514 | M | 🎯 | Dijkstra Variant | 40/30/22 | Max heap by probability |
| 4 | Min Cost to Connect All Points | 1584 | M | 🔑 | MST (Prim/Kruskal) | 45/35/25 | Minimum spanning tree |
| 5 | Critical Connections in Network | 1192 | H | 💡 | Tarjan's Algorithm | 60/50/40 | Bridges, low-link values |
| 6 | Swim in Rising Water | 778 | H | 💡 | Binary Search/Dijkstra | 50/40/30 | Time binary search |

**Additional Problems (Advanced)**:
| 7 | Shortest Path in Binary Matrix | 1091 | M | ⭐ | BFS | 35/25/18 |
| 8 | All Paths from Source to Target | 797 | M | 🎯 | DFS/Backtracking | 35/25/18 |
| 9 | Find City With Smallest Neighbors | 1334 | M | ⭐ | Floyd-Warshall | 40/30/22 |
| 10 | Min Cost to Reach Destination | 1928 | H | 💡 | Dijkstra + DP | 60/50/40 |
| 11 | Shortest Path Visiting All Nodes | 847 | H | 💡 | BFS + Bitmask | 60/50/40 |
| 12 | Reachable Nodes in Subdivided Graph | 882 | H | 💡 | Dijkstra | 60/50/40 |

**Week 16 Checkpoint**: ✅ Dijkstra's algorithm | ✅ MST (Prim/Kruskal) | ✅ Shortest path variants | ✅ Advanced graph algorithms

**Pattern Connections**:
- Network Delay (743) → Path with Max Probability (1514) → Dijkstra variants
- Min Cost Connect (1584) → Connecting Cities (1135) → MST problems
- Cheapest Flights (787) → Min Cost Destination (1928) → Constrained shortest path

---

## Phase 11 – Interview-Oriented Mixed Practice (Weeks 17-18)

**Total: 25+ Problems (Focus on first 12)**

| # | Problem | LC# | Diff | Priority | Pattern | Time (B/I/A) | Key Concept |
|---|---------|-----|------|----------|---------|--------------|-------------|
| 1 | LRU Cache | 146 | M | 🔑 | Design | 45/35/25 | DLL + hash map, O(1) operations |
| 2 | Trapping Rain Water | 42 | H | 🔑 | Two Pointers/Stack | 45/35/25 | min(maxLeft, maxRight) - height |
| 3 | Longest Consecutive Sequence | 128 | M | ⭐ | Hash Set | 35/25/18 | O(n), check sequence starts |
| 4 | Basic Calculator II | 227 | M | ⭐ | Stack | 40/30/22 | Operator precedence, stack |
| 5 | Sliding Window Maximum | 239 | H | 💡 | Monotonic Deque | 50/40/30 | Decreasing deque, max in window |
| 6 | Largest Rectangle in Histogram | 84 | H | 💡 | Monotonic Stack | 50/40/30 | Increasing stack, area calculation |
| 7 | Valid Sudoku | 36 | M | 🎯 | Hash Set | 30/22/15 | Three sets: row, col, box |
| 8 | Rotate Image | 48 | M | 🎯 | Matrix | 30/22/15 | Transpose + reverse rows |
| 9 | Spiral Matrix | 54 | M | 🎯 | Matrix | 35/25/18 | Four boundaries, spiral traversal |
| 10 | Insert Delete GetRandom O(1) | 380 | M | ⭐ | Array + Hash Map | 40/30/22 | Map: val→index, swap with last |
| 11 | Time Based Key-Value Store | 981 | M | ⭐ | Hash Map + BS | 40/30/22 | Map to (timestamp, value) list |
| 12 | Encode and Decode Strings | 271 | M | ⭐ | String Design | 35/25/18 | Length prefix: "4:word5:hello" |

**Additional Mixed Practice (Advanced)**:
| 13 | LFU Cache | 460 | H | 💡 | Design | 60/50/40 |
| 14 | All O'one Data Structure | 432 | H | 💡 | Design | 60/50/40 |
| 15 | Design Twitter | 355 | M | ⭐ | Design | 50/40/30 |
| 16 | Design Search Autocomplete | 642 | H | 💡 | Trie + Design | 60/50/40 |
| 17 | Longest Substring with K Distinct | 340 | M | ⭐ | Sliding Window | 35/25/18 |
| 18 | Maximum Frequency Stack | 895 | H | 💡 | Stack + Hash Map | 50/40/30 |
| 19 | Design Snake Game | 353 | M | 💡 | Design | 50/40/30 |
| 20 | Design Tic-Tac-Toe | 348 | M | ⭐ | Design | 40/30/22 |
| 21 | Range Sum Query 2D - Mutable | 308 | H | 💡 | Segment Tree | 60/50/40 |
| 22 | Serialize and Deserialize BST | 449 | M | ⭐ | Tree + Design | 40/30/22 |
| 23 | Design File System | 588 | M | 💡 | Trie + Design | 50/40/30 |
| 24 | Random Pick with Weight | 528 | M | ⭐ | Prefix Sum + BS | 35/28/20 |
| 25 | Online Stock Span | 901 | M | ⭐ | Monotonic Stack | 35/25/18 |

**Weeks 17-18 Checkpoint**: ✅ Design problems | ✅ Fast problem recognition | ✅ Mock interview ready | ✅ <30 min Medium

**Pattern Connections**:
- LRU Cache (146) → LFU Cache (460) → All O'one (432)
- Trapping Rain Water (42) → Container With Water (11) → Largest Rectangle (84)
- Sliding Window Max (239) → Daily Temperatures (739) → Monotonic deque/stack

---

## Summary Statistics

### Total Problems by Difficulty
- **Easy**: 41 problems (17%)
- **Medium**: 136 problems (58%)
- **Hard**: 58 problems (25%)
- **Total**: 235 problems

### Total Problems by Priority
- **🔑 ANCHOR**: 47 problems (20%) - **MASTER THESE FIRST**
- **⭐ HIGH VALUE**: 58 problems (25%) - Common in interviews
- **💡 ADVANCED**: 32 problems (14%) - Complex applications
- **🎯 PRACTICE**: 98 problems (41%) - Pattern reinforcement

### Phase Distribution
- **Phase 0**: 7 problems (Week 1)
- **Phase 1**: 19 problems (Weeks 2-3)
- **Phase 2**: 21 problems (Week 4, focus on 12)
- **Phase 3**: 23+ problems (Weeks 5-6)
- **Phase 4**: 16+ problems (Weeks 7-8)
- **Phase 5**: 18 problems (Week 9, focus on 12)
- **Phase 6**: 13 problems (Week 10, focus on 8)
- **Phase 7**: 40+ problems (Weeks 11-13)
- **Phase 8**: 18 problems (Week 14, focus on 10)
- **Phase 9**: 17 problems (Week 15, focus on 10)
- **Phase 10**: 12 problems (Week 16, focus on 6)
- **Phase 11**: 25+ problems (Weeks 17-18, focus on 12)

### Time Commitment Estimates
- **Total Core Problems** (focus set): ~165 problems
- **Average Time per Problem**: 25-35 minutes
- **Total Estimated Time**: 90-120 hours (spread over 16-18 weeks)
- **Weekly Commitment**: 10-12 hours optimal
- **Daily Commitment**: 1.5-2 hours (6 days/week)

### Pattern Coverage
1. **Arrays & Hashing**: 19 problems
2. **Two Pointers**: 12 problems
3. **Sliding Window**: 8 problems
4. **Linked Lists**: 21 problems
5. **Stacks & Queues**: 12 problems
6. **Trees & BST**: 23+ problems
7. **Graphs**: 27+ problems
8. **Binary Search**: 18 problems
9. **Greedy**: 13 problems
10. **Dynamic Programming**: 40+ problems
11. **Backtracking**: 18 problems
12. **Heaps**: 17 problems
13. **Advanced Data Structures**: 15 problems
14. **Design Problems**: 12 problems

---

## Study Recommendations

### For Beginners (0-3 months experience)
1. Start with Phase 0-1 (Weeks 1-3)
2. Focus ONLY on 🔑 ANCHOR and 🎯 PRACTICE problems
3. Spend 3-4 reviews per problem (spaced repetition)
4. Time estimate: Upper bound (Beginner column)
5. Goal: Build strong foundation in 6-8 core patterns

### For Intermediate (3-6 months experience)
1. Complete Phases 0-5 thoroughly (Weeks 1-9)
2. Tackle 🔑 ANCHOR and ⭐ HIGH VALUE problems
3. Spend 2-3 reviews per problem
4. Time estimate: Middle bound (Intermediate column)
5. Goal: Master 15-20 patterns, prepare for interviews

### For Advanced (6+ months experience or interview prep)
1. Complete all 11 phases (16-18 weeks)
2. Focus on ⭐ HIGH VALUE and 💡 ADVANCED problems
3. Spend 1-2 reviews per problem
4. Time estimate: Lower bound (Advanced column)
5. Goal: Pattern recognition in <5 min, execution in <25 min

### Spaced Repetition Schedule
- **First solve**: Day 0
- **Review 1**: Day 3 (3 days later)
- **Review 2**: Day 10 (1 week after review 1)
- **Review 3**: Day 24 (2 weeks after review 2)
- **Mastered**: After 3 successful reviews without hints

### Daily Problem Template (50 minutes)
1. **[15 min] Analysis** - Read, identify pattern, list test cases
2. **[25 min] Implementation** - Code, test edge cases
3. **[10 min] Review** - Compare with optimal, note pattern, update tracker

---

## Pattern Quick Reference

### Two Pointers
**Same Direction**: Slow/fast, in-place modifications (Move Zeroes, Remove Duplicates)
**Opposite Direction**: Palindrome, pair finding (Valid Palindrome, 3Sum)

### Sliding Window
**Variable Size**: Expand right, contract left (Longest Substring, Longest Repeating)
**Fixed Size**: Process fixed k-size window (Max in Sliding Window)

### Binary Search
**Find Element**: Standard binary search (Binary Search)
**Find Boundary**: Leftmost/rightmost occurrence (Find First and Last Position)
**Answer Space**: Feasibility check (Koko Bananas, Ship Packages)

### Dynamic Programming
**1D**: Linear sequence (Climbing Stairs, House Robber, Coin Change)
**2D Grid**: Path counting (Unique Paths, Min Path Sum)
**2D Sequence**: String matching (LCS, Edit Distance)
**0/1 Knapsack**: Subset selection (Partition Equal Subset Sum)

### Tree Recursion
**Bottom-Up**: Return info to parent (Max Depth, Diameter)
**Top-Down**: Pass info to children (Path Sum, Validate BST)

### Graph Traversal
**DFS**: Connected components, cycle detection (Number of Islands)
**BFS**: Shortest path, level-by-level (Rotting Oranges)
**Topological Sort**: DAG ordering (Course Schedule)

### Backtracking
**Combinations**: Order doesn't matter (Subsets, Combination Sum)
**Permutations**: Order matters (Permutations)
**Grid Search**: Mark/unmark visited (Word Search)

---

## Master Checklist

### Phase Completion Tracking
- [ ] Phase 0 - Foundations (Week 1)
- [ ] Phase 1 - Arrays & Hashing (Weeks 2-3)
- [ ] Phase 2 - Linked Lists & Stacks (Week 4)
- [ ] Phase 3 - Trees & BST (Weeks 5-6)
- [ ] Phase 4 - Graphs (Weeks 7-8)
- [ ] Phase 5 - Sorting & Searching (Week 9)
- [ ] Phase 6 - Greedy (Week 10)
- [ ] Phase 7 - Dynamic Programming (Weeks 11-13)
- [ ] Phase 8 - Backtracking (Week 14)
- [ ] Phase 9 - Heaps & Data Structures (Week 15)
- [ ] Phase 10 - Advanced Graphs (Week 16)
- [ ] Phase 11 - Mixed Practice (Weeks 17-18)

### Pattern Mastery Checklist
- [ ] Hash Map for complement search
- [ ] Two Pointers (same & opposite direction)
- [ ] Sliding Window (variable & fixed)
- [ ] Fast/Slow Pointers (Floyd's algorithm)
- [ ] Stack for matching pairs
- [ ] Monotonic Stack/Deque
- [ ] Binary Search (all variants)
- [ ] DFS on trees (top-down & bottom-up)
- [ ] BFS level-order traversal
- [ ] BST property exploitation
- [ ] Grid DFS/BFS
- [ ] Topological Sort (Kahn's algorithm)
- [ ] Interval merging and scheduling
- [ ] 1D DP patterns
- [ ] 2D DP (grid & sequence)
- [ ] 0/1 Knapsack
- [ ] Backtracking template
- [ ] Heap operations (min/max)
- [ ] Two-heaps pattern
- [ ] Union-Find
- [ ] Trie implementation
- [ ] Greedy choice recognition
- [ ] Kadane's algorithm

### Skill Milestones
- [ ] Can solve Easy in <20 minutes
- [ ] Can solve Medium in <35 minutes
- [ ] Recognize patterns in <5 minutes
- [ ] Know when to use which data structure
- [ ] Can optimize brute force to optimal
- [ ] Explain time/space complexity clearly
- [ ] Handle edge cases systematically
- [ ] Write clean, bug-free code in first attempt
- [ ] Complete mock interview without hints

---

## Quick Start Guides

### 4-Week Intensive (Interview in 1 month)
**Week 1**: Phase 0 + Phase 1A (Two Sum, Hash Maps, Basic Two Pointers) - 17 problems
**Week 2**: Phase 1B + Phase 2 (Sliding Window, Linked Lists, Stacks) - 21 problems
**Week 3**: Phase 3 + Phase 4A (Trees, Grid DFS/BFS) - 15 problems
**Week 4**: Phase 7A (1D DP) + Phase 11 (Design) + Review all anchors - 20 problems
**Total**: 73 core problems + reviews

### 8-Week Standard (Solid Foundation)
**Weeks 1-2**: Phase 0-1 (Arrays, Hashing, Two Pointers, Sliding Window)
**Week 3**: Phase 2 (Linked Lists, Stacks)
**Weeks 4-5**: Phase 3 (Trees, BST)
**Week 6**: Phase 4 (Graphs)
**Week 7**: Phase 5-6 (Binary Search, Greedy)
**Week 8**: Phase 7A-B (1D and 2D DP) + Review
**Total**: 100+ problems

### 16-Week Complete (Interview Mastery)
Follow the phase-by-phase schedule in the detailed syllabus.
**Total**: 165+ core problems + advanced practice

---

**Good luck on your DSA mastery journey! Remember:**
- **Quality > Quantity**: Master anchors before moving to variations
- **Spaced Repetition**: Review is more important than solving new problems
- **Pattern Recognition**: Focus on identifying patterns quickly
- **Consistency > Intensity**: 2 problems/day for 90 days > 20 problems/day for 9 days

**Track your progress in the master sheet and use spaced repetition religiously. You've got this! 🚀**
