# 🎯 Master DSA Problem List

Welcome to the streamlined DSA Hybrid Curriculum problem list. This list contains ~235 carefully curated problems broken down by pattern and phase. 

### How to use this list
1. **Core Anchors (🔑)**: Solve these first. They teach the fundamental invariant of the pattern.
2. **High Value (⭐)**: Frequently asked in interviews. Master these to build fluency.
3. **Practice (🎯)**: Use these to build speed and reinforce your mental models.
4. **Advanced (💡)**: Complex variations to challenge your understanding.

Focus on understanding the **Key Concept** for each problem rather than timing yourself rigidly.

---

## Phase 0: Foundations & Complexity

Let's start with the absolute basics. In this phase, you'll build your foundation with Binary Search, basic bit manipulation, and prefix sums. Don't rush these—they are the building blocks for everything else.



| # | Problem | Difficulty | Priority | Pattern | Key Concept |
|---|---------|------------|----------|---------|-------------|
| 1 | **Binary Search** (704) | 🟢 Easy | 🔑 Anchor | Binary Search | Search space halving, template |
| 2 | **First Bad Version** (278) | 🟢 Easy | 🎯 Practice | Binary Search | Minimization variant |
| 3 | **Climbing Stairs** (70) | 🟢 Easy | 🔑 Anchor | 1D DP | Fibonacci DP, state definition |
| 4 | **Power of Two** (231) | 🟢 Easy | 🎯 Practice | Bit Manipulation | n & (n-1) trick |
| 5 | **Power of Three** (326) | 🟢 Easy | 🎯 Practice | Math | Logarithm properties |
| 6 | **Counting Bits** (338) | 🟢 Easy | ⭐ High Value | DP + Bit | count[i] = count[i>>1] + (i&1) |
| 7 | **Find Pivot Index** (724) | 🟢 Easy | 🔑 Anchor | Prefix Sum | leftSum = total - rightSum - curr |


---

## Phase 1: Arrays, Strings, and Hashing

This is the bread and butter of technical interviews. We'll focus heavily on Hash Maps for O(1) lookups and Two-Pointer techniques. Master the Sliding Window pattern here—it's incredibly high ROI.



### Part A: Hash Maps & Basic Two-Pointers (Week 2)

| # | Problem | Difficulty | Priority | Pattern | Key Concept |
|---|---------|------------|----------|---------|-------------|
| 1 | **Two Sum** (1) | 🟢 Easy | 🔑 Anchor | Hash Map | Complement search, value→index |
| 2 | **Contains Duplicate** (217) | 🟢 Easy | 🎯 Practice | Hash Set | Existence check |
| 3 | **Valid Anagram** (242) | 🟢 Easy | ⭐ High Value | Hash Map | Frequency map or sort |
| 4 | **Group Anagrams** (49) | 🟡 Medium | 🔑 Anchor | Hash Map | Sorted string as key |
| 5 | **Valid Palindrome** (125) | 🟢 Easy | 🔑 Anchor | Two Pointers | Opposite direction, filter chars |
| 6 | **Merge Sorted Array** (88) | 🟢 Easy | ⭐ High Value | Two Pointers | Backward iteration, in-place |
| 7 | **Move Zeroes** (283) | 🟢 Easy | 🎯 Practice | Two Pointers | Slow/fast pointers, order preserved |
| 8 | **Best Time to Buy and Sell Stock** (121) | 🟢 Easy | 🔑 Anchor | Greedy/DP | Track min, update max profit |
| 9 | **Majority Element** (169) | 🟢 Easy | ⭐ High Value | Boyer-Moore | Voting algorithm, cancellation |
| 10 | **Single Number** (136) | 🟢 Easy | 🎯 Practice | Bit (XOR) | XOR all, duplicates cancel |


### Part B: Sliding Window & Advanced Two-Pointers (Week 3)

| # | Problem | Difficulty | Priority | Pattern | Key Concept |
|---|---------|------------|----------|---------|-------------|
| 11 | **Longest Substring Without Repeating** (3) | 🟡 Medium | 🔑 Anchor | Sliding Window | Variable window, expand/contract |
| 12 | **Longest Repeating Character Replacement** (424) | 🟡 Medium | 💡 Advanced | Sliding Window | Window valid: len - maxFreq ≤ k |
| 13 | **Container With Most Water** (11) | 🟡 Medium | 🔑 Anchor | Two Pointers | Move shorter height, greedy |
| 14 | **3Sum** (15) | 🟡 Medium | 🔑 Anchor | Two Pointers + Sort | Fix one, two-pointer on rest |
| 15 | **Product of Array Except Self** (238) | 🟡 Medium | ⭐ High Value | Prefix/Suffix | Left products, then right |
| 16 | **Top K Frequent Elements** (347) | 🟡 Medium | ⭐ High Value | Hash Map + Heap | Frequency map + min heap |
| 17 | **Subarray Sum Equals K** (560) | 🟡 Medium | 💡 Advanced | Prefix Sum + Map | prefixSum[j] - prefixSum[i] = k |
| 18 | **Rotate Array** (189) | 🟡 Medium | 🎯 Practice | Array | Three reverses trick |
| 19 | **Missing Number** (268) | 🟢 Easy | 🎯 Practice | Math/Bit | Sum formula or XOR |


**Pattern Connections**:
- Two Sum (1) → 3Sum (15) → 4Sum
- Longest Substring (3) → Longest Repeating (12) → Minimum Window Substring
- Valid Palindrome (125) → Container With Water (11) → Trapping Rain Water (42)

---

## Phase 2: Linked Lists, Stacks, and Queues

Time to manipulate pointers and states. You'll learn the Fast/Slow pointer technique (Floyd's cycle detection) and how to use Monotonic Stacks to solve "next greater element" style queries.



| # | Problem | Difficulty | Priority | Pattern | Key Concept |
|---|---------|------------|----------|---------|-------------|
| 1 | **Reverse Linked List** (206) | 🟢 Easy | 🔑 Anchor | Linked List | Three pointers: prev, curr, next |
| 2 | **Merge Two Sorted Lists** (21) | 🟢 Easy | 🔑 Anchor | Linked List | Dummy node pattern |
| 3 | **Linked List Cycle** (141) | 🟢 Easy | 🔑 Anchor | Fast/Slow | Floyd's algorithm, detect cycle |
| 4 | **Linked List Cycle II** (142) | 🟡 Medium | 💡 Advanced | Fast/Slow | Find cycle entry point |
| 5 | **Middle of the Linked List** (876) | 🟢 Easy | 🎯 Practice | Fast/Slow | Fast 2x, slow 1x |
| 6 | **Palindrome Linked List** (234) | 🟢 Easy | ⭐ High Value | Fast/Slow + Reverse | Find middle + reverse + compare |
| 7 | **Valid Parentheses** (20) | 🟢 Easy | 🔑 Anchor | Stack | LIFO, matching pairs |
| 8 | **Remove Nth Node From End** (19) | 🟡 Medium | ⭐ High Value | Two Pointers | N-ahead pointer, one-pass |
| 9 | **Reorder List** (143) | 🟡 Medium | 💡 Advanced | Multiple Patterns | Middle + reverse + merge |
| 10 | **Add Two Numbers** (2) | 🟡 Medium | ⭐ High Value | Linked List | Digit-by-digit, carry handling |
| 11 | **Daily Temperatures** (739) | 🟡 Medium | 🔑 Anchor | Monotonic Stack | Decreasing stack, next greater |
| 12 | **Min Stack** (155) | 🟡 Medium | ⭐ High Value | Stack Design | Auxiliary min stack, O(1) getMin |

**Advanced Problems (Attempt after mastering first 12)**:
| # | Problem | Difficulty | Priority | Pattern | Key Concept |
|---|---------|------------|----------|---------|-------------|
| 13 | **Swap Nodes in Pairs** (24) | 🟡 Medium | 🎯 Practice | Linked List | Dummy node, swap adjacent |
| 14 | **Reverse Nodes in k-Group** (25) | 🔴 Hard | 💡 Advanced | Linked List | Check length k, reverse k nodes |
| 15 | **Rotate List** (61) | 🟡 Medium | 🎯 Practice | Linked List | Connect tail to head, break at len-k |
| 16 | **Partition List** (86) | 🟡 Medium | 🎯 Practice | Linked List | Two dummy lists: < x and >= x |
| 17 | **Sort List** (148) | 🟡 Medium | 💡 Advanced | Merge Sort | Merge sort, find middle with fast/slow |
| 18 | **Intersection of Two Linked Lists** (160) | 🟢 Easy | 🎯 Practice | Two Pointers | Switch heads at end, equalizes length |
| 19 | **Remove Linked List Elements** (203) | 🟢 Easy | 🎯 Practice | Linked List | Dummy node, skip target values |
| 20 | **Odd Even Linked List** (328) | 🟡 Medium | 🎯 Practice | Linked List | Separate odd/even pointers, merge |
| 21 | **Flatten Multilevel Doubly Linked List** (430) | 🟡 Medium | 💡 Advanced | DFS | DFS, connect child to next, track tail |


**Pattern Connections**:
- Reverse List (206) → Reverse in k-Group (25) → Reorder List (143)
- Linked List Cycle (141) → Cycle II (142) → Palindrome List (234)
- Valid Parentheses (20) → Daily Temperatures (739) → Min Stack (155)

---

## Phase 3: Trees and Binary Search Trees

Recursion is king here. You'll learn the difference between top-down (passing state to children) and bottom-up (returning state to parents) tree traversals. We'll also cover BST properties and Lowest Common Ancestor (LCA) patterns.



### Part A: Basic Tree Recursion (Week 5)

| # | Problem | Difficulty | Priority | Pattern | Key Concept |
|---|---------|------------|----------|---------|-------------|
| 1 | **Maximum Depth of Binary Tree** (104) | 🟢 Easy | 🔑 Anchor | DFS (Bottom-Up) | 1 + max(left, right) |
| 2 | **Invert Binary Tree** (226) | 🟢 Easy | 🔑 Anchor | DFS | Swap children, recurse |
| 3 | **Same Tree** (100) | 🟢 Easy | 🎯 Practice | DFS | Structural + value equality |
| 4 | **Symmetric Tree** (101) | 🟢 Easy | ⭐ High Value | DFS | Mirror checking, dual recursion |
| 5 | **Diameter of Binary Tree** (543) | 🟢 Easy | 🔑 Anchor | DFS (Bottom-Up) | leftHeight + rightHeight |
| 6 | **Balanced Binary Tree** (110) | 🟢 Easy | 🎯 Practice | DFS | Height balance: |
| 7 | **Path Sum** (112) | 🟢 Easy | 🎯 Practice | DFS (Top-Down) | Subtract curr, check leaf == 0 |
| 8 | **Binary Tree Level Order Traversal** (102) | 🟡 Medium | 🔑 Anchor | BFS | Queue, level separation |
| 9 | **Binary Tree Right Side View** (199) | 🟡 Medium | ⭐ High Value | BFS/DFS | Last element per level |


### Part B: BST & Advanced Trees (Week 6)

| # | Problem | Difficulty | Priority | Pattern | Key Concept |
|---|---------|------------|----------|---------|-------------|
| 10 | **Validate Binary Search Tree** (98) | 🟡 Medium | 🔑 Anchor | BST | Range validation (min, max) |
| 11 | **Lowest Common Ancestor of BST** (235) | 🟡 Medium | 🎯 Practice | BST | Use BST property, split point |
| 12 | **Lowest Common Ancestor of Binary Tree** (236) | 🟡 Medium | 🔑 Anchor | DFS | Return node if found, LCA if both |
| 13 | **Kth Smallest Element in BST** (230) | 🟡 Medium | ⭐ High Value | BST + Inorder | Inorder gives sorted, count to k |
| 14 | **Convert Sorted Array to BST** (108) | 🟢 Easy | 🎯 Practice | BST + D&C | Middle as root, recurse halves |
| 15 | **Binary Tree Maximum Path Sum** (124) | 🔴 Hard | 💡 Advanced | DFS (Bottom-Up) | node + max(0,left) + max(0,right) |
| 16 | **Serialize and Deserialize Binary Tree** (297) | 🔴 Hard | 💡 Advanced | Tree Design | Preorder with null markers |

| # | Problem | Difficulty | Priority | Pattern | Key Concept |
|---|---------|------------|----------|---------|-------------|
| 17 | **Subtree of Another Tree** (572) | 🟢 Easy | 🎯 Practice | DFS | DFS, Same Tree check at each node |
| 18 | **Construct Tree from Preorder & Inorder** (105) | 🟡 Medium | 💡 Advanced | Tree Construction | Preorder gives root, Inorder gives sizes |
| 19 | **Binary Tree Zigzag Level Order** (103) | 🟡 Medium | 🎯 Practice | BFS | BFS, reverse alternate levels |
| 20 | **Count Good Nodes in Binary Tree** (1448) | 🟡 Medium | ⭐ High Value | DFS | DFS, pass max_val down |
| 21 | **Binary Tree Paths** (257) | 🟢 Easy | 🎯 Practice | DFS | DFS, string building |
| 22 | **Sum of Left Leaves** (404) | 🟢 Easy | 🎯 Practice | DFS | DFS, flag for is_left |
| 23 | **Find Leaves of Binary Tree** (366) | 🟡 Medium | 💡 Advanced | DFS | DFS (Bottom-Up), return height |

**Weeks 5-6 Checkpoint**: ✅ BST properties | ✅ LCA patterns | ✅ Inorder traversal | ✅ Tree construction

**Pattern Connections**:
- Max Depth (104) → Diameter (543) → Max Path Sum (124)
- Validate BST (98) → Kth Smallest (230) → BST Iterator (173)
- Level Order (102) → Right Side View (199) → Zigzag (103)

---

## Phase 4: Graphs and Graph Traversal

Graphs can look intimidating, but they boil down to a few core templates. We'll cover implicit grid searches, connected components, and Kahn's algorithm for Topological Sorting.



### Part A: Grid DFS/BFS (Week 7)

| # | Problem | Difficulty | Priority | Pattern | Key Concept |
|---|---------|------------|----------|---------|-------------|
| 1 | **Flood Fill** (733) | 🟢 Easy | 🔑 Anchor | DFS/BFS | 4-directional, color change |
| 2 | **Number of Islands** (200) | 🟡 Medium | 🔑 Anchor | DFS/BFS | Component counting, mark visited |
| 3 | **Max Area of Island** (695) | 🟡 Medium | 🎯 Practice | DFS/BFS | Return area from DFS |
| 4 | **Rotting Oranges** (994) | 🟡 Medium | 🔑 Anchor | Multi-Source BFS | Add all rotten to queue, track time |
| 5 | **Surrounded Regions** (130) | 🟡 Medium | ⭐ High Value | DFS/BFS | Border search, mark safe |
| 6 | **Pacific Atlantic Water Flow** (417) | 🟡 Medium | 💡 Advanced | Multi-Source DFS | Two DFS, intersection |


### Part B: Graph Structures & Topological Sort (Week 8)

| # | Problem | Difficulty | Priority | Pattern | Key Concept |
|---|---------|------------|----------|---------|-------------|
| 7 | **Clone Graph** (133) | 🟡 Medium | ⭐ High Value | DFS/BFS | Hash map: old→new, DFS clone |
| 8 | **Number of Provinces** (547) | 🟡 Medium | 🎯 Practice | DFS/Union-Find | Adjacency matrix, components |
| 9 | **Course Schedule** (207) | 🟡 Medium | 🔑 Anchor | Topological Sort | Kahn's algorithm, cycle detection |
| 10 | **Course Schedule II** (210) | 🟡 Medium | ⭐ High Value | Topological Sort | Return actual order |
| 11 | **Graph Valid Tree** (261) | 🟡 Medium | 💡 Advanced | DFS/Union-Find | n-1 edges, no cycles, connected |

**Advanced Graph Problems (Practice after core)**:
| # | Problem | Difficulty | Priority | Pattern | Key Concept |
|---|---------|------------|----------|---------|-------------|
| 12 | **Word Ladder** (127) | 🔴 Hard | 💡 Advanced | BFS | BFS on word transformations, wildcards |
| 13 | **Word Ladder II** (126) | 🔴 Hard | 💡 Advanced | BFS + Backtracking | BFS for shortest path + DFS for paths |
| 14 | **Alien Dictionary** (269) | 🔴 Hard | 💡 Advanced | Topological Sort | Build graph from adjacent words, Topo Sort |
| 15 | **Evaluate Division** (399) | 🟡 Medium | 💡 Advanced | DFS/Union-Find | Graph of divisions, DFS/BFS or Union Find |
| 16 | **Accounts Merge** (721) | 🟡 Medium | ⭐ High Value | Union-Find | Union-Find on emails, map to names |

**Weeks 7-8 Checkpoint**: ✅ Topological sort (Kahn's) | ✅ Cycle detection | ✅ Multi-source patterns | ✅ Tree conditions

**Pattern Connections**:
- Flood Fill (733) → Number of Islands (200) → Max Area (695)
- Rotting Oranges (994) → Pacific Atlantic (417) → 01 Matrix (542)
- Course Schedule (207) → Course Schedule II (210) → Alien Dictionary (269)

---

## Phase 5: Sorting, Searching, and Divide & Conquer

We're leveling up Binary Search. Instead of just finding an element, you'll learn how to binary search over an *answer space* (like in Koko Eating Bananas). We'll also tackle interval merging.



| # | Problem | Difficulty | Priority | Pattern | Key Concept |
|---|---------|------------|----------|---------|-------------|
| 1 | **Merge Intervals** (56) | 🟡 Medium | 🔑 Anchor | Intervals + Sort | Sort by start, merge overlaps |
| 2 | **Insert Interval** (57) | 🟡 Medium | ⭐ High Value | Intervals | Three phases: before, merge, after |
| 3 | **Non-overlapping Intervals** (435) | 🟡 Medium | 💡 Advanced | Greedy + Intervals | Sort by end, greedy keep earliest |
| 4 | **Meeting Rooms** (252) | 🟢 Easy | 🎯 Practice | Intervals + Sort | Check if start < prev end |
| 5 | **Search in Rotated Sorted Array** (33) | 🟡 Medium | 🔑 Anchor | Binary Search | Check which half sorted |
| 6 | **Find Minimum in Rotated Array** (153) | 🟡 Medium | 🎯 Practice | Binary Search | Compare mid with right |
| 7 | **Find First and Last Position** (34) | 🟡 Medium | 🔑 Anchor | Binary Search | Two binary searches: left, right |
| 8 | **Koko Eating Bananas** (875) | 🟡 Medium | 🔑 Anchor | BS on Answer | Binary search on speed k |
| 9 | **Search a 2D Matrix** (74) | 🟡 Medium | 🎯 Practice | Binary Search | Treat as 1D: row=mid//cols |
| 10 | **Find Peak Element** (162) | 🟡 Medium | ⭐ High Value | Binary Search | Follow gradient: mid > mid+1 |
| 11 | **Sort Colors** (75) | 🟡 Medium | ⭐ High Value | Two Pointers | Dutch flag, three-way partition |
| 12 | **Capacity To Ship Packages** (1011) | 🟡 Medium | 💡 Advanced | BS on Answer | Binary search on capacity |

| # | Problem | Difficulty | Priority | Pattern | Key Concept |
|---|---------|------------|----------|---------|-------------|
| 13 | **Meeting Rooms II** (253) | 🟡 Medium | ⭐ High Value | Heap | Min-heap of end times or Chronological sort |
| 14 | **Minimum Arrows to Burst Balloons** (452) | 🟡 Medium | 💡 Advanced | Greedy + Intervals | Sort by end time, greedy |
| 15 | **Search a 2D Matrix II** (240) | 🟡 Medium | ⭐ High Value | Binary Search | Start top-right or bottom-left |
| 16 | **Median of Two Sorted Arrays** (4) | 🔴 Hard | 💡 Advanced | Binary Search | Binary search on smaller array for partition |
| 17 | **Split Array Largest Sum** (410) | 🔴 Hard | 💡 Advanced | BS on Answer | Binary search on max sum, count subarrays |
| 18 | **Find K Closest Elements** (658) | 🟡 Medium | ⭐ High Value | Binary Search | Binary search for left bound, sliding window |


**Pattern Connections**:
- Binary Search (704) → Rotated Array (33) → Find Min (153)
- Merge Intervals (56) → Insert Interval (57) → Non-overlapping (435)
- Koko Bananas (875) → Ship Packages (1011) → Split Array (410)

---

## Phase 6: Greedy Algorithms

Greedy algorithms require proving that a local optimum leads to a global optimum. We'll cover Kadane's algorithm, jump games, and interval scheduling.



| # | Problem | Difficulty | Priority | Pattern | Key Concept |
|---|---------|------------|----------|---------|-------------|
| 1 | **Maximum Subarray** (53) | 🟡 Medium | 🔑 Anchor | Greedy/DP | Kadane's algorithm |
| 2 | **Jump Game** (55) | 🟡 Medium | 🔑 Anchor | Greedy | Track furthest reachable |
| 3 | **Jump Game II** (45) | 🟡 Medium | 💡 Advanced | Greedy | Track current end, next end |
| 4 | **Gas Station** (134) | 🟡 Medium | ⭐ High Value | Greedy | Total gas ≥ cost, start at surplus |
| 5 | **Partition Labels** (763) | 🟡 Medium | ⭐ High Value | Greedy | Last occurrence, extend partition |
| 6 | **Best Time to Buy and Sell Stock II** (122) | 🟡 Medium | 🎯 Practice | Greedy | Collect all positive differences |
| 7 | **Assign Cookies** (455) | 🟢 Easy | 🎯 Practice | Greedy + Sort | Smallest cookie satisfies child |
| 8 | **Min Arrows to Burst Balloons** (452) | 🟡 Medium | 💡 Advanced | Greedy + Intervals | Sort by end, shoot at end |

| # | Problem | Difficulty | Priority | Pattern | Key Concept |
|---|---------|------------|----------|---------|-------------|
| 9 | **Queue Reconstruction by Height** (406) | 🟡 Medium | 💡 Advanced | Greedy | Sort by height desc, insert by k |
| 10 | **Task Scheduler** (621) | 🟡 Medium | ⭐ High Value | Greedy | Max heap of frequencies or Math formula |
| 11 | **Minimum Number of Taps** (1326) | 🔴 Hard | 💡 Advanced | Greedy | Jump Game II variant on intervals |
| 12 | **Bag of Tokens** (948) | 🟡 Medium | 🎯 Practice | Greedy | Two pointers, greedy face up/down |
| 13 | **Boats to Save People** (881) | 🟡 Medium | 🎯 Practice | Greedy | Two pointers, heaviest + lightest |


**Pattern Connections**:
- Maximum Subarray (53) → Maximum Product Subarray (152)
- Jump Game (55) → Jump Game II (45) → Frog Jump (403)
- Partition Labels (763) → Merge Intervals (56)

---

## Phase 7: Dynamic Programming

DP is often the most feared topic, but it's just recursion with memoization. We'll start with 1D sequences, move to 2D grids, and finally tackle the classic 0/1 Knapsack and State Machine patterns.



### Part A: 1D DP Foundations (Week 11)

| # | Problem | Difficulty | Priority | Pattern | Key Concept |
|---|---------|------------|----------|---------|-------------|
| 1 | **Climbing Stairs** (70) | 🟢 Easy | 🔑 Anchor | 1D DP | dp[i] = dp[i-1] + dp[i-2] |
| 2 | **Min Cost Climbing Stairs** (746) | 🟢 Easy | 🎯 Practice | 1D DP | cost[i] + min(dp[i-1], dp[i-2]) |
| 3 | **House Robber** (198) | 🟡 Medium | 🔑 Anchor | 1D DP | max(rob i + dp[i-2], dp[i-1]) |
| 4 | **House Robber II** (213) | 🟡 Medium | ⭐ High Value | 1D DP | Two sub-problems: [0:n-2], [1:n-1] |
| 5 | **Coin Change** (322) | 🟡 Medium | 🔑 Anchor | Unbounded Knapsack | dp[i] = min(dp[i], dp[i-coin] + 1) |
| 6 | **Decode Ways** (91) | 🟡 Medium | ⭐ High Value | 1D DP | Check single and double digit |
| 7 | **Maximum Product Subarray** (152) | 🟡 Medium | 💡 Advanced | 1D DP | Track max and min (negatives) |
| 8 | **Word Break** (139) | 🟡 Medium | 🔑 Anchor | 1D DP | dp[i] = can break s[0:i] |


### Part B: 2D DP & Sequences (Week 12)

| # | Problem | Difficulty | Priority | Pattern | Key Concept |
|---|---------|------------|----------|---------|-------------|
| 9 | **Unique Paths** (62) | 🟡 Medium | 🔑 Anchor | 2D Grid DP | dp[i][j] = dp[i-1][j] + dp[i][j-1] |
| 10 | **Unique Paths II** (63) | 🟡 Medium | 🎯 Practice | 2D Grid DP | Handle obstacles: dp[i][j] = 0 |
| 11 | **Minimum Path Sum** (64) | 🟡 Medium | ⭐ High Value | 2D Grid DP | grid + min(dp[i-1][j], dp[i][j-1]) |
| 12 | **Longest Common Subsequence** (1143) | 🟡 Medium | 🔑 Anchor | 2D Sequence DP | If match: dp[i-1][j-1]+1; else: max |
| 13 | **Edit Distance** (72) | 🔴 Hard | 🔑 Anchor | 2D Sequence DP | Insert, delete, replace operations |
| 14 | **Longest Increasing Subsequence** (300) | 🟡 Medium | 🔑 Anchor | 1D DP + BS | O(n²) DP or O(n log n) binary search |
| 15 | **Longest Palindromic Substring** (5) | 🟡 Medium | ⭐ High Value | 2D DP / Expand | Expand around center or DP table |
| 16 | **Palindromic Substrings** (647) | 🟡 Medium | 🎯 Practice | 2D DP / Expand | Count instead of track longest |


### Part C: Advanced DP (Week 13)

| # | Problem | Difficulty | Priority | Pattern | Key Concept |
|---|---------|------------|----------|---------|-------------|
| 17 | **Partition Equal Subset Sum** (416) | 🟡 Medium | 🔑 Anchor | 0/1 Knapsack | Target = totalSum/2, knapsack DP |
| 18 | **Target Sum** (494) | 🟡 Medium | ⭐ High Value | DP/Backtracking | Transform to subset sum problem |
| 19 | **Combination Sum IV** (377) | 🟡 Medium | 💡 Advanced | Unbounded Knapsack | Order matters variant |
| 20 | **Best Time Stock with Cooldown** (309) | 🟡 Medium | 💡 Advanced | State Machine DP | Three states: hold, sold, cooldown |

| # | Problem | Difficulty | Priority | Pattern | Key Concept |
|---|---------|------------|----------|---------|-------------|
| 21 | **Regular Expression Matching** (10) | 🔴 Hard | 💡 Advanced | 2D DP | 2D DP, match preceding element |
| 22 | **Wildcard Matching** (44) | 🔴 Hard | 💡 Advanced | 2D DP | 2D DP or Two pointers with backtracking |
| 23 | **Burst Balloons** (312) | 🔴 Hard | 💡 Advanced | Interval DP | Interval DP, last balloon to burst |
| 24 | **Distinct Subsequences** (115) | 🔴 Hard | 💡 Advanced | 2D DP | 2D DP, match character or skip |
| 25 | **Interleaving String** (97) | 🟡 Medium | 💡 Advanced | 2D DP | 2D DP, path finding in grid |
| 26 | **Longest Valid Parentheses** (32) | 🔴 Hard | 💡 Advanced | Stack/DP | Stack of indices or DP array |
| 27 | **Minimum Window Substring** (76) | 🔴 Hard | 💡 Advanced | Sliding Window | Sliding window, character frequency map |
| 28 | **Trapping Rain Water** (42) | 🔴 Hard | 💡 Advanced | Two Pointers/DP | Two pointers, max_left and max_right |
| 29 | **Maximal Rectangle** (85) | 🔴 Hard | 💡 Advanced | Stack + DP | Histogram DP on each row |
| 30 | **Scramble String** (87) | 🔴 Hard | 💡 Advanced | 3D DP | 3D DP or Recursion with memoization |

**Weeks 11-13 Checkpoint**: ✅ 0/1 knapsack | ✅ State machine DP | ✅ Space optimization | ✅ 2D sequence patterns

**Pattern Connections**:
- Climbing Stairs (70) → Min Cost (746) → House Robber (198)
- Coin Change (322) → Combination Sum IV (377) → Perfect Squares (279)
- Unique Paths (62) → Min Path Sum (64) → Triangle (120)
- LCS (1143) → Edit Distance (72) → Distinct Subsequences (115)
- House Robber (198) → House Robber II (213) → House Robber III (337)

---

## Phase 8: Backtracking and Recursion

When you need to generate all possibilities, you use Backtracking. We'll learn how to prune decision trees to avoid checking invalid states, covering combinations, permutations, and grid searches.



| # | Problem | Difficulty | Priority | Pattern | Key Concept |
|---|---------|------------|----------|---------|-------------|
| 1 | **Subsets** (78) | 🟡 Medium | 🔑 Anchor | Backtracking | Include/exclude decision tree |
| 2 | **Subsets II** (90) | 🟡 Medium | ⭐ High Value | Backtracking | Sort, skip duplicates |
| 3 | **Permutations** (46) | 🟡 Medium | 🔑 Anchor | Backtracking | Swap with each remaining |
| 4 | **Permutations II** (47) | 🟡 Medium | 💡 Advanced | Backtracking | Frequency map, skip used |
| 5 | **Combinations** (77) | 🟡 Medium | 🎯 Practice | Backtracking | Fixed size k, start index |
| 6 | **Combination Sum** (39) | 🟡 Medium | 🔑 Anchor | Backtracking | Unlimited use, stay at index |
| 7 | **Combination Sum II** (40) | 🟡 Medium | ⭐ High Value | Backtracking | Limited use, skip duplicates |
| 8 | **Letter Combinations Phone** (17) | 🟡 Medium | 🎯 Practice | Backtracking | Digit→letters mapping |
| 9 | **Generate Parentheses** (22) | 🟡 Medium | ⭐ High Value | Backtracking | Track open/close count |
| 10 | **Word Search** (79) | 🟡 Medium | 🔑 Anchor | Backtracking + Grid | 4-directional, mark visited |

| # | Problem | Difficulty | Priority | Pattern | Key Concept |
|---|---------|------------|----------|---------|-------------|
| 11 | **Palindrome Partitioning** (131) | 🟡 Medium | 💡 Advanced | Backtracking | Backtracking, check palindrome prefix |
| 12 | **N-Queens** (51) | 🔴 Hard | 💡 Advanced | Backtracking | Backtracking, track cols, diags, anti-diags |
| 13 | **N-Queens II** (52) | 🔴 Hard | 💡 Advanced | Backtracking | Backtracking, count solutions only |
| 14 | **Sudoku Solver** (37) | 🔴 Hard | 💡 Advanced | Backtracking | Backtracking, try 1-9, validate row/col/box |
| 15 | **Restore IP Addresses** (93) | 🟡 Medium | 🎯 Practice | Backtracking | Backtracking, valid segments (0-255) |
| 16 | **Combination Sum III** (216) | 🟡 Medium | 🎯 Practice | Backtracking | Backtracking, 1-9, fixed size k |
| 17 | **Word Search II** (212) | 🔴 Hard | 💡 Advanced | Backtracking + Trie | Trie + Backtracking, prune found words |
| 18 | **Expression Add Operators** (282) | 🔴 Hard | 💡 Advanced | Backtracking | Backtracking, track eval and prev operand |


**Pattern Connections**:
- Subsets (78) → Subsets II (90) → Power Set variations
- Permutations (46) → Permutations II (47) → Next Permutation (31)
- Combination Sum (39) → Combination Sum II (40) → Combination Sum III (216)
- Generate Parentheses (22) → Remove Invalid Parentheses (301)

---

## Phase 9: Heaps, Tries, and Union-Find

Time for advanced data structures. Use Heaps for top-K problems, Tries for prefix-based string searches, and Union-Find for dynamic connectivity and cycle detection.



| # | Problem | Difficulty | Priority | Pattern | Key Concept |
|---|---------|------------|----------|---------|-------------|
| 1 | **Kth Largest Element in Array** (215) | 🟡 Medium | 🔑 Anchor | Heap/Quickselect | Min heap of size k, O(n log k) |
| 2 | **Top K Frequent Elements** (347) | 🟡 Medium | 🔑 Anchor | Heap + Hash Map | Frequency map + min heap |
| 3 | **K Closest Points to Origin** (973) | 🟡 Medium | 🎯 Practice | Heap | Max heap of size k by distance |
| 4 | **Find Median from Data Stream** (295) | 🔴 Hard | 🔑 Anchor | Two Heaps | Max heap (left) + min heap (right) |
| 5 | **Merge k Sorted Lists** (23) | 🔴 Hard | ⭐ High Value | Heap | K-way merge with min heap |
| 6 | **Last Stone Weight** (1046) | 🟢 Easy | 🎯 Practice | Max Heap | Simple heap simulation |
| 7 | **Number of Connected Components** (323) | 🟡 Medium | 🔑 Anchor | Union-Find | Template, count components |
| 8 | **Graph Valid Tree** (261) | 🟡 Medium | ⭐ High Value | Union-Find | n-1 edges, no cycles |
| 9 | **Implement Trie** (208) | 🟡 Medium | 🔑 Anchor | Trie Design | Insert, search, startsWith |
| 10 | **Design Add Search Words DS** (211) | 🟡 Medium | 💡 Advanced | Trie + Backtracking | Wildcard search with '.' |

| # | Problem | Difficulty | Priority | Pattern | Key Concept |
|---|---------|------------|----------|---------|-------------|
| 11 | **Kth Smallest in Sorted Matrix** (378) | 🟡 Medium | ⭐ High Value | Heap/Binary Search | Min-heap or Binary search on value range |
| 12 | **Ugly Number II** (264) | 🟡 Medium | 💡 Advanced | Heap/DP | Three pointers (x2, x3, x5), min multiples |
| 13 | **Find K Pairs with Smallest Sums** (373) | 🟡 Medium | 💡 Advanced | Heap | Min-heap with (sum, i, j) |
| 14 | **Smallest Range Covering K Lists** (632) | 🔴 Hard | 💡 Advanced | Heap | Min-heap of list pointers, track max_val |
| 15 | **Replace Words** (648) | 🟡 Medium | ⭐ High Value | Trie | Trie, find shortest prefix |
| 16 | **Word Search II** (212) | 🔴 Hard | 💡 Advanced | Trie + Backtracking | Trie + Backtracking, prune found words |
| 17 | **Redundant Connection** (684) | 🟡 Medium | ⭐ High Value | Union-Find | Union-Find, find cycle edge |


**Pattern Connections**:
- Kth Largest (215) → Kth Smallest Matrix (378) → Find Median (295)
- Top K Frequent (347) → K Closest Points (973) → K Closest Elements (658)
- Trie (208) → Add Search Words (211) → Word Search II (212)
- Union-Find (323) → Graph Valid Tree (261) → Accounts Merge (721)

---

## Phase 10: Advanced Graphs

Advanced graph algorithms like Dijkstra, Minimum Spanning Trees, and Eulerian Paths.




| # | Problem | Difficulty | Priority | Pattern | Key Concept |
|---|---------|------------|----------|---------|-------------|
| 1 | **Network Delay Time** (743) | 🟡 Medium | 🔑 Anchor | Dijkstra | Single-source shortest path |
| 2 | **Cheapest Flights Within K Stops** (787) | 🟡 Medium | ⭐ High Value | BFS/DP | K-constrained shortest path |
| 3 | **Path with Maximum Probability** (1514) | 🟡 Medium | 🎯 Practice | Dijkstra Variant | Max heap by probability |
| 4 | **Min Cost to Connect All Points** (1584) | 🟡 Medium | 🔑 Anchor | MST (Prim/Kruskal) | Minimum spanning tree |
| 5 | **Critical Connections in Network** (1192) | 🔴 Hard | 💡 Advanced | Tarjan's Algorithm | Bridges, low-link values |
| 6 | **Swim in Rising Water** (778) | 🔴 Hard | 💡 Advanced | Binary Search/Dijkstra | Time binary search |

| # | Problem | Difficulty | Priority | Pattern | Key Concept |
|---|---------|------------|----------|---------|-------------|
| 7 | **Shortest Path in Binary Matrix** (1091) | 🟡 Medium | ⭐ High Value | BFS | BFS, 8-directional, shortest path |
| 8 | **All Paths from Source to Target** (797) | 🟡 Medium | 🎯 Practice | DFS/Backtracking | DFS, DAG guarantees no cycles |
| 9 | **Find City With Smallest Neighbors** (1334) | 🟡 Medium | ⭐ High Value | Floyd-Warshall | Floyd-Warshall or Dijkstra from all nodes |
| 10 | **Min Cost to Reach Destination** (1928) | 🔴 Hard | 💡 Advanced | Dijkstra + DP | Dijkstra with time constraint |
| 11 | **Shortest Path Visiting All Nodes** (847) | 🔴 Hard | 💡 Advanced | BFS + Bitmask | BFS with bitmask for visited state |
| 12 | **Reachable Nodes in Subdivided Graph** (882) | 🔴 Hard | 💡 Advanced | Dijkstra | Dijkstra, max/min heap on distance |


**Pattern Connections**:
- Network Delay (743) → Path with Max Probability (1514) → Dijkstra variants
- Min Cost Connect (1584) → Connecting Cities (1135) → MST problems
- Cheapest Flights (787) → Min Cost Destination (1928) → Constrained shortest path

---

## Phase 11: Design & Advanced Data Structures

Data structure design problems and advanced structures like Segment Trees and LFUs.




| # | Problem | Difficulty | Priority | Pattern | Key Concept |
|---|---------|------------|----------|---------|-------------|
| 1 | **LRU Cache** (146) | 🟡 Medium | 🔑 Anchor | Design | DLL + hash map, O(1) operations |
| 2 | **Trapping Rain Water** (42) | 🔴 Hard | 🔑 Anchor | Two Pointers/Stack | min(maxLeft, maxRight) - height |
| 3 | **Longest Consecutive Sequence** (128) | 🟡 Medium | ⭐ High Value | Hash Set | O(n), check sequence starts |
| 4 | **Basic Calculator II** (227) | 🟡 Medium | ⭐ High Value | Stack | Operator precedence, stack |
| 5 | **Sliding Window Maximum** (239) | 🔴 Hard | 💡 Advanced | Monotonic Deque | Decreasing deque, max in window |
| 6 | **Largest Rectangle in Histogram** (84) | 🔴 Hard | 💡 Advanced | Monotonic Stack | Increasing stack, area calculation |
| 7 | **Valid Sudoku** (36) | 🟡 Medium | 🎯 Practice | Hash Set | Three sets: row, col, box |
| 8 | **Rotate Image** (48) | 🟡 Medium | 🎯 Practice | Matrix | Transpose + reverse rows |
| 9 | **Spiral Matrix** (54) | 🟡 Medium | 🎯 Practice | Matrix | Four boundaries, spiral traversal |
| 10 | **Insert Delete GetRandom O(1)** (380) | 🟡 Medium | ⭐ High Value | Array + Hash Map | Map: val→index, swap with last |
| 11 | **Time Based Key-Value Store** (981) | 🟡 Medium | ⭐ High Value | Hash Map + BS | Map to (timestamp, value) list |
| 12 | **Encode and Decode Strings** (271) | 🟡 Medium | ⭐ High Value | String Design | Length prefix: "4:word5:hello" |

| # | Problem | Difficulty | Priority | Pattern | Key Concept |
|---|---------|------------|----------|---------|-------------|
| 13 | **LFU Cache** (460) | 🔴 Hard | 💡 Advanced | Design | Two hash maps (key->val, freq->DLL) |
| 14 | **All O'one Data Structure** (432) | 🔴 Hard | 💡 Advanced | Design | Doubly linked list of frequency buckets |
| 15 | **Design Twitter** (355) | 🟡 Medium | ⭐ High Value | Design | Hash maps + Max heap for recent tweets |
| 16 | **Design Search Autocomplete** (642) | 🔴 Hard | 💡 Advanced | Trie + Design | Trie + Hash map for frequencies |
| 17 | **Longest Substring with K Distinct** (340) | 🟡 Medium | ⭐ High Value | Sliding Window | Sliding window, hash map for counts |
| 18 | **Maximum Frequency Stack** (895) | 🔴 Hard | 💡 Advanced | Stack + Hash Map | Hash maps (freq, group->stack) |
| 19 | **Design Snake Game** (353) | 🟡 Medium | 💡 Advanced | Design | Queue for body, Hash set for fast lookup |
| 20 | **Design Tic-Tac-Toe** (348) | 🟡 Medium | ⭐ High Value | Design | Arrays for row/col sums, track diagonals |
| 21 | **Range Sum Query 2D - Mutable** (308) | 🔴 Hard | 💡 Advanced | Segment Tree | 2D Binary Indexed Tree (Fenwick) |
| 22 | **Serialize and Deserialize BST** (449) | 🟡 Medium | ⭐ High Value | Tree + Design | Preorder traversal, upper/lower bounds |
| 23 | **Design File System** (588) | 🟡 Medium | 💡 Advanced | Trie + Design | Trie or Hash map, track paths and values |
| 24 | **Random Pick with Weight** (528) | 🟡 Medium | ⭐ High Value | Prefix Sum + BS | Prefix sum array + Binary search |
| 25 | **Online Stock Span** (901) | 🟡 Medium | ⭐ High Value | Monotonic Stack | Monotonic Stack (decreasing), accumulate span |

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
- [ ] Phase 11 - Design & Advanced Data Structures (Weeks 17-18)

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
**Total**: 73 core problems + reviews

### 8-Week Standard (Solid Foundation)
**Weeks 1-2**: Phase 0-1 (Arrays, Hashing, Two Pointers, Sliding Window)
**Weeks 4-5**: Phase 3 (Trees, BST)
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
