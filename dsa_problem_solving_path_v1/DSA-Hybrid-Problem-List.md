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



| # | Problem & Difficulty | Pattern | Key Concept |
|---|----------------------|---------|-------------|
| 1 | **Binary Search** (LC 704)<br>🟢 Easy \| 🔑 Anchor | Binary Search | Search space halving, template |
| 2 | **First Bad Version** (LC 278)<br>🟢 Easy \| 🎯 Practice | Binary Search | Minimization variant |
| 3 | **Climbing Stairs** (LC 70)<br>🟢 Easy \| 🔑 Anchor | 1D DP | Fibonacci DP, state definition |
| 4 | **Power of Two** (LC 231)<br>🟢 Easy \| 🎯 Practice | Bit Manipulation | n & (n-1) trick |
| 5 | **Power of Three** (LC 326)<br>🟢 Easy \| 🎯 Practice | Math | Logarithm properties |
| 6 | **Counting Bits** (LC 338)<br>🟢 Easy \| ⭐ High Value | DP + Bit | count[i] = count[i>>1] + (i&1) |
| 7 | **Find Pivot Index** (LC 724)<br>🟢 Easy \| 🔑 Anchor | Prefix Sum | leftSum = total - rightSum - curr |


---

## Phase 1: Arrays, Strings, and Hashing

This is the bread and butter of technical interviews. We'll focus heavily on Hash Maps for O(1) lookups and Two-Pointer techniques. Master the Sliding Window pattern here—it's incredibly high ROI.



### Part A: Hash Maps & Basic Two-Pointers (Week 2)

| # | Problem & Difficulty | Pattern | Key Concept |
|---|----------------------|---------|-------------|
| 1 | **Two Sum** (LC 1)<br>🟢 Easy \| 🔑 Anchor | Hash Map | Complement search, value→index |
| 2 | **Contains Duplicate** (LC 217)<br>🟢 Easy \| 🎯 Practice | Hash Set | Existence check |
| 3 | **Valid Anagram** (LC 242)<br>🟢 Easy \| ⭐ High Value | Hash Map | Frequency map or sort |
| 4 | **Group Anagrams** (LC 49)<br>🟡 Medium \| 🔑 Anchor | Hash Map | Sorted string as key |
| 5 | **Valid Palindrome** (LC 125)<br>🟢 Easy \| 🔑 Anchor | Two Pointers | Opposite direction, filter chars |
| 6 | **Merge Sorted Array** (LC 88)<br>🟢 Easy \| ⭐ High Value | Two Pointers | Backward iteration, in-place |
| 7 | **Move Zeroes** (LC 283)<br>🟢 Easy \| 🎯 Practice | Two Pointers | Slow/fast pointers, order preserved |
| 8 | **Best Time to Buy and Sell Stock** (LC 121)<br>🟢 Easy \| 🔑 Anchor | Greedy/DP | Track min, update max profit |
| 9 | **Majority Element** (LC 169)<br>🟢 Easy \| ⭐ High Value | Boyer-Moore | Voting algorithm, cancellation |
| 10 | **Single Number** (LC 136)<br>🟢 Easy \| 🎯 Practice | Bit (XOR) | XOR all, duplicates cancel |


### Part B: Sliding Window & Advanced Two-Pointers (Week 3)

| # | Problem & Difficulty | Pattern | Key Concept |
|---|----------------------|---------|-------------|
| 11 | **Longest Substring Without Repeating** (LC 3)<br>🟡 Medium \| 🔑 Anchor | Sliding Window | Variable window, expand/contract |
| 12 | **Longest Repeating Character Replacement** (LC 424)<br>🟡 Medium \| 💡 Advanced | Sliding Window | Window valid: len - maxFreq ≤ k |
| 13 | **Container With Most Water** (LC 11)<br>🟡 Medium \| 🔑 Anchor | Two Pointers | Move shorter height, greedy |
| 14 | **3Sum** (LC 15)<br>🟡 Medium \| 🔑 Anchor | Two Pointers + Sort | Fix one, two-pointer on rest |
| 15 | **Product of Array Except Self** (LC 238)<br>🟡 Medium \| ⭐ High Value | Prefix/Suffix | Left products, then right |
| 16 | **Top K Frequent Elements** (LC 347)<br>🟡 Medium \| ⭐ High Value | Hash Map + Heap | Frequency map + min heap |
| 17 | **Subarray Sum Equals K** (LC 560)<br>🟡 Medium \| 💡 Advanced | Prefix Sum + Map | prefixSum[j] - prefixSum[i] = k |
| 18 | **Rotate Array** (LC 189)<br>🟡 Medium \| 🎯 Practice | Array | Three reverses trick |
| 19 | **Missing Number** (LC 268)<br>🟢 Easy \| 🎯 Practice | Math/Bit | Sum formula or XOR |


**Pattern Connections**:
- Two Sum (1) → 3Sum (15) → 4Sum
- Longest Substring (3) → Longest Repeating (12) → Minimum Window Substring
- Valid Palindrome (125) → Container With Water (11) → Trapping Rain Water (42)

---

## Phase 2: Linked Lists, Stacks, and Queues

Time to manipulate pointers and states. You'll learn the Fast/Slow pointer technique (Floyd's cycle detection) and how to use Monotonic Stacks to solve "next greater element" style queries.



| # | Problem & Difficulty | Pattern | Key Concept |
|---|----------------------|---------|-------------|
| 1 | **Reverse Linked List** (LC 206)<br>🟢 Easy \| 🔑 Anchor | Linked List | Three pointers: prev, curr, next |
| 2 | **Merge Two Sorted Lists** (LC 21)<br>🟢 Easy \| 🔑 Anchor | Linked List | Dummy node pattern |
| 3 | **Linked List Cycle** (LC 141)<br>🟢 Easy \| 🔑 Anchor | Fast/Slow | Floyd's algorithm, detect cycle |
| 4 | **Linked List Cycle II** (LC 142)<br>🟡 Medium \| 💡 Advanced | Fast/Slow | Find cycle entry point |
| 5 | **Middle of the Linked List** (LC 876)<br>🟢 Easy \| 🎯 Practice | Fast/Slow | Fast 2x, slow 1x |
| 6 | **Palindrome Linked List** (LC 234)<br>🟢 Easy \| ⭐ High Value | Fast/Slow + Reverse | Find middle + reverse + compare |
| 7 | **Valid Parentheses** (LC 20)<br>🟢 Easy \| 🔑 Anchor | Stack | LIFO, matching pairs |
| 8 | **Remove Nth Node From End** (LC 19)<br>🟡 Medium \| ⭐ High Value | Two Pointers | N-ahead pointer, one-pass |
| 9 | **Reorder List** (LC 143)<br>🟡 Medium \| 💡 Advanced | Multiple Patterns | Middle + reverse + merge |
| 10 | **Add Two Numbers** (LC 2)<br>🟡 Medium \| ⭐ High Value | Linked List | Digit-by-digit, carry handling |
| 11 | **Daily Temperatures** (LC 739)<br>🟡 Medium \| 🔑 Anchor | Monotonic Stack | Decreasing stack, next greater |
| 12 | **Min Stack** (LC 155)<br>🟡 Medium \| ⭐ High Value | Stack Design | Auxiliary min stack, O(1) getMin |

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


**Pattern Connections**:
- Reverse List (206) → Reverse in k-Group (25) → Reorder List (143)
- Linked List Cycle (141) → Cycle II (142) → Palindrome List (234)
- Valid Parentheses (20) → Daily Temperatures (739) → Min Stack (155)

---

## Phase 3: Trees and Binary Search Trees

Recursion is king here. You'll learn the difference between top-down (passing state to children) and bottom-up (returning state to parents) tree traversals. We'll also cover BST properties and Lowest Common Ancestor (LCA) patterns.



### Part A: Basic Tree Recursion (Week 5)

| # | Problem & Difficulty | Pattern | Key Concept |
|---|----------------------|---------|-------------|
| 1 | **Maximum Depth of Binary Tree** (LC 104)<br>🟢 Easy \| 🔑 Anchor | DFS (Bottom-Up) | 1 + max(left, right) |
| 2 | **Invert Binary Tree** (LC 226)<br>🟢 Easy \| 🔑 Anchor | DFS | Swap children, recurse |
| 3 | **Same Tree** (LC 100)<br>🟢 Easy \| 🎯 Practice | DFS | Structural + value equality |
| 4 | **Symmetric Tree** (LC 101)<br>🟢 Easy \| ⭐ High Value | DFS | Mirror checking, dual recursion |
| 5 | **Diameter of Binary Tree** (LC 543)<br>🟢 Easy \| 🔑 Anchor | DFS (Bottom-Up) | leftHeight + rightHeight |
| 6 | **Balanced Binary Tree** (LC 110)<br>🟢 Easy \| 🎯 Practice | DFS | Height balance: |
| 7 | **Path Sum** (LC 112)<br>🟢 Easy \| 🎯 Practice | DFS (Top-Down) | Subtract curr, check leaf == 0 |
| 8 | **Binary Tree Level Order Traversal** (LC 102)<br>🟡 Medium \| 🔑 Anchor | BFS | Queue, level separation |
| 9 | **Binary Tree Right Side View** (LC 199)<br>🟡 Medium \| ⭐ High Value | BFS/DFS | Last element per level |


### Part B: BST & Advanced Trees (Week 6)

| # | Problem & Difficulty | Pattern | Key Concept |
|---|----------------------|---------|-------------|
| 10 | **Validate Binary Search Tree** (LC 98)<br>🟡 Medium \| 🔑 Anchor | BST | Range validation (min, max) |
| 11 | **Lowest Common Ancestor of BST** (LC 235)<br>🟡 Medium \| 🎯 Practice | BST | Use BST property, split point |
| 12 | **Lowest Common Ancestor of Binary Tree** (LC 236)<br>🟡 Medium \| 🔑 Anchor | DFS | Return node if found, LCA if both |
| 13 | **Kth Smallest Element in BST** (LC 230)<br>🟡 Medium \| ⭐ High Value | BST + Inorder | Inorder gives sorted, count to k |
| 14 | **Convert Sorted Array to BST** (LC 108)<br>🟢 Easy \| 🎯 Practice | BST + D&C | Middle as root, recurse halves |
| 15 | **Binary Tree Maximum Path Sum** (LC 124)<br>🔴 Hard \| 💡 Advanced | DFS (Bottom-Up) | node + max(0,left) + max(0,right) |
| 16 | **Serialize and Deserialize Binary Tree** (LC 297)<br>🔴 Hard \| 💡 Advanced | Tree Design | Preorder with null markers |

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

## Phase 4: Graphs and Graph Traversal

Graphs can look intimidating, but they boil down to a few core templates. We'll cover implicit grid searches, connected components, and Kahn's algorithm for Topological Sorting.



### Part A: Grid DFS/BFS (Week 7)

| # | Problem & Difficulty | Pattern | Key Concept |
|---|----------------------|---------|-------------|
| 1 | **Flood Fill** (LC 733)<br>🟢 Easy \| 🔑 Anchor | DFS/BFS | 4-directional, color change |
| 2 | **Number of Islands** (LC 200)<br>🟡 Medium \| 🔑 Anchor | DFS/BFS | Component counting, mark visited |
| 3 | **Max Area of Island** (LC 695)<br>🟡 Medium \| 🎯 Practice | DFS/BFS | Return area from DFS |
| 4 | **Rotting Oranges** (LC 994)<br>🟡 Medium \| 🔑 Anchor | Multi-Source BFS | Add all rotten to queue, track time |
| 5 | **Surrounded Regions** (LC 130)<br>🟡 Medium \| ⭐ High Value | DFS/BFS | Border search, mark safe |
| 6 | **Pacific Atlantic Water Flow** (LC 417)<br>🟡 Medium \| 💡 Advanced | Multi-Source DFS | Two DFS, intersection |


### Part B: Graph Structures & Topological Sort (Week 8)

| # | Problem & Difficulty | Pattern | Key Concept |
|---|----------------------|---------|-------------|
| 7 | **Clone Graph** (LC 133)<br>🟡 Medium \| ⭐ High Value | DFS/BFS | Hash map: old→new, DFS clone |
| 8 | **Number of Provinces** (LC 547)<br>🟡 Medium \| 🎯 Practice | DFS/Union-Find | Adjacency matrix, components |
| 9 | **Course Schedule** (LC 207)<br>🟡 Medium \| 🔑 Anchor | Topological Sort | Kahn's algorithm, cycle detection |
| 10 | **Course Schedule II** (LC 210)<br>🟡 Medium \| ⭐ High Value | Topological Sort | Return actual order |
| 11 | **Graph Valid Tree** (LC 261)<br>🟡 Medium \| 💡 Advanced | DFS/Union-Find | n-1 edges, no cycles, connected |

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

## Phase 5: Sorting, Searching, and Divide & Conquer

We're leveling up Binary Search. Instead of just finding an element, you'll learn how to binary search over an *answer space* (like in Koko Eating Bananas). We'll also tackle interval merging.



| # | Problem & Difficulty | Pattern | Key Concept |
|---|----------------------|---------|-------------|
| 1 | **Merge Intervals** (LC 56)<br>🟡 Medium \| 🔑 Anchor | Intervals + Sort | Sort by start, merge overlaps |
| 2 | **Insert Interval** (LC 57)<br>🟡 Medium \| ⭐ High Value | Intervals | Three phases: before, merge, after |
| 3 | **Non-overlapping Intervals** (LC 435)<br>🟡 Medium \| 💡 Advanced | Greedy + Intervals | Sort by end, greedy keep earliest |
| 4 | **Meeting Rooms** (LC 252)<br>🟢 Easy \| 🎯 Practice | Intervals + Sort | Check if start < prev end |
| 5 | **Search in Rotated Sorted Array** (LC 33)<br>🟡 Medium \| 🔑 Anchor | Binary Search | Check which half sorted |
| 6 | **Find Minimum in Rotated Array** (LC 153)<br>🟡 Medium \| 🎯 Practice | Binary Search | Compare mid with right |
| 7 | **Find First and Last Position** (LC 34)<br>🟡 Medium \| 🔑 Anchor | Binary Search | Two binary searches: left, right |
| 8 | **Koko Eating Bananas** (LC 875)<br>🟡 Medium \| 🔑 Anchor | BS on Answer | Binary search on speed k |
| 9 | **Search a 2D Matrix** (LC 74)<br>🟡 Medium \| 🎯 Practice | Binary Search | Treat as 1D: row=mid//cols |
| 10 | **Find Peak Element** (LC 162)<br>🟡 Medium \| ⭐ High Value | Binary Search | Follow gradient: mid > mid+1 |
| 11 | **Sort Colors** (LC 75)<br>🟡 Medium \| ⭐ High Value | Two Pointers | Dutch flag, three-way partition |
| 12 | **Capacity To Ship Packages** (LC 1011)<br>🟡 Medium \| 💡 Advanced | BS on Answer | Binary search on capacity |

| 13 | Meeting Rooms II | 253 | M | ⭐ | Heap | 35/25/18 |
| 14 | Minimum Arrows to Burst Balloons | 452 | M | 💡 | Greedy + Intervals | 35/28/20 |
| 15 | Search a 2D Matrix II | 240 | M | ⭐ | Binary Search | 35/25/18 |
| 16 | Median of Two Sorted Arrays | 4 | H | 💡 | Binary Search | 60/50/40 |
| 17 | Split Array Largest Sum | 410 | H | 💡 | BS on Answer | 50/40/30 |
| 18 | Find K Closest Elements | 658 | M | ⭐ | Binary Search | 35/25/18 |


**Pattern Connections**:
- Binary Search (704) → Rotated Array (33) → Find Min (153)
- Merge Intervals (56) → Insert Interval (57) → Non-overlapping (435)
- Koko Bananas (875) → Ship Packages (1011) → Split Array (410)

---

## Phase 6: Greedy Algorithms

Greedy algorithms require proving that a local optimum leads to a global optimum. We'll cover Kadane's algorithm, jump games, and interval scheduling.



| # | Problem & Difficulty | Pattern | Key Concept |
|---|----------------------|---------|-------------|
| 1 | **Maximum Subarray** (LC 53)<br>🟡 Medium \| 🔑 Anchor | Greedy/DP | Kadane's algorithm |
| 2 | **Jump Game** (LC 55)<br>🟡 Medium \| 🔑 Anchor | Greedy | Track furthest reachable |
| 3 | **Jump Game II** (LC 45)<br>🟡 Medium \| 💡 Advanced | Greedy | Track current end, next end |
| 4 | **Gas Station** (LC 134)<br>🟡 Medium \| ⭐ High Value | Greedy | Total gas ≥ cost, start at surplus |
| 5 | **Partition Labels** (LC 763)<br>🟡 Medium \| ⭐ High Value | Greedy | Last occurrence, extend partition |
| 6 | **Best Time to Buy and Sell Stock II** (LC 122)<br>🟡 Medium \| 🎯 Practice | Greedy | Collect all positive differences |
| 7 | **Assign Cookies** (LC 455)<br>🟢 Easy \| 🎯 Practice | Greedy + Sort | Smallest cookie satisfies child |
| 8 | **Min Arrows to Burst Balloons** (LC 452)<br>🟡 Medium \| 💡 Advanced | Greedy + Intervals | Sort by end, shoot at end |

| 9 | Queue Reconstruction by Height | 406 | M | 💡 | Greedy | 40/30/22 |
| 10 | Task Scheduler | 621 | M | ⭐ | Greedy | 40/30/22 |
| 11 | Minimum Number of Taps | 1326 | H | 💡 | Greedy | 50/40/30 |
| 12 | Bag of Tokens | 948 | M | 🎯 | Greedy | 30/22/15 |
| 13 | Boats to Save People | 881 | M | 🎯 | Greedy | 30/22/15 |


**Pattern Connections**:
- Maximum Subarray (53) → Maximum Product Subarray (152)
- Jump Game (55) → Jump Game II (45) → Frog Jump (403)
- Partition Labels (763) → Merge Intervals (56)

---

## Phase 7: Dynamic Programming

DP is often the most feared topic, but it's just recursion with memoization. We'll start with 1D sequences, move to 2D grids, and finally tackle the classic 0/1 Knapsack and State Machine patterns.



### Part A: 1D DP Foundations (Week 11)

| # | Problem & Difficulty | Pattern | Key Concept |
|---|----------------------|---------|-------------|
| 1 | **Climbing Stairs** (LC 70)<br>🟢 Easy \| 🔑 Anchor | 1D DP | dp[i] = dp[i-1] + dp[i-2] |
| 2 | **Min Cost Climbing Stairs** (LC 746)<br>🟢 Easy \| 🎯 Practice | 1D DP | cost[i] + min(dp[i-1], dp[i-2]) |
| 3 | **House Robber** (LC 198)<br>🟡 Medium \| 🔑 Anchor | 1D DP | max(rob i + dp[i-2], dp[i-1]) |
| 4 | **House Robber II** (LC 213)<br>🟡 Medium \| ⭐ High Value | 1D DP | Two sub-problems: [0:n-2], [1:n-1] |
| 5 | **Coin Change** (LC 322)<br>🟡 Medium \| 🔑 Anchor | Unbounded Knapsack | dp[i] = min(dp[i], dp[i-coin] + 1) |
| 6 | **Decode Ways** (LC 91)<br>🟡 Medium \| ⭐ High Value | 1D DP | Check single and double digit |
| 7 | **Maximum Product Subarray** (LC 152)<br>🟡 Medium \| 💡 Advanced | 1D DP | Track max and min (negatives) |
| 8 | **Word Break** (LC 139)<br>🟡 Medium \| 🔑 Anchor | 1D DP | dp[i] = can break s[0:i] |


### Part B: 2D DP & Sequences (Week 12)

| # | Problem & Difficulty | Pattern | Key Concept |
|---|----------------------|---------|-------------|
| 9 | **Unique Paths** (LC 62)<br>🟡 Medium \| 🔑 Anchor | 2D Grid DP | dp[i][j] = dp[i-1][j] + dp[i][j-1] |
| 10 | **Unique Paths II** (LC 63)<br>🟡 Medium \| 🎯 Practice | 2D Grid DP | Handle obstacles: dp[i][j] = 0 |
| 11 | **Minimum Path Sum** (LC 64)<br>🟡 Medium \| ⭐ High Value | 2D Grid DP | grid + min(dp[i-1][j], dp[i][j-1]) |
| 12 | **Longest Common Subsequence** (LC 1143)<br>🟡 Medium \| 🔑 Anchor | 2D Sequence DP | If match: dp[i-1][j-1]+1; else: max |
| 13 | **Edit Distance** (LC 72)<br>🔴 Hard \| 🔑 Anchor | 2D Sequence DP | Insert, delete, replace operations |
| 14 | **Longest Increasing Subsequence** (LC 300)<br>🟡 Medium \| 🔑 Anchor | 1D DP + BS | O(n²) DP or O(n log n) binary search |
| 15 | **Longest Palindromic Substring** (LC 5)<br>🟡 Medium \| ⭐ High Value | 2D DP / Expand | Expand around center or DP table |
| 16 | **Palindromic Substrings** (LC 647)<br>🟡 Medium \| 🎯 Practice | 2D DP / Expand | Count instead of track longest |


### Part C: Advanced DP (Week 13)

| # | Problem & Difficulty | Pattern | Key Concept |
|---|----------------------|---------|-------------|
| 17 | **Partition Equal Subset Sum** (LC 416)<br>🟡 Medium \| 🔑 Anchor | 0/1 Knapsack | Target = totalSum/2, knapsack DP |
| 18 | **Target Sum** (LC 494)<br>🟡 Medium \| ⭐ High Value | DP/Backtracking | Transform to subset sum problem |
| 19 | **Combination Sum IV** (LC 377)<br>🟡 Medium \| 💡 Advanced | Unbounded Knapsack | Order matters variant |
| 20 | **Best Time Stock with Cooldown** (LC 309)<br>🟡 Medium \| 💡 Advanced | State Machine DP | Three states: hold, sold, cooldown |

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

## Phase 8: Backtracking and Recursion

When you need to generate all possibilities, you use Backtracking. We'll learn how to prune decision trees to avoid checking invalid states, covering combinations, permutations, and grid searches.



| # | Problem & Difficulty | Pattern | Key Concept |
|---|----------------------|---------|-------------|
| 1 | **Subsets** (LC 78)<br>🟡 Medium \| 🔑 Anchor | Backtracking | Include/exclude decision tree |
| 2 | **Subsets II** (LC 90)<br>🟡 Medium \| ⭐ High Value | Backtracking | Sort, skip duplicates |
| 3 | **Permutations** (LC 46)<br>🟡 Medium \| 🔑 Anchor | Backtracking | Swap with each remaining |
| 4 | **Permutations II** (LC 47)<br>🟡 Medium \| 💡 Advanced | Backtracking | Frequency map, skip used |
| 5 | **Combinations** (LC 77)<br>🟡 Medium \| 🎯 Practice | Backtracking | Fixed size k, start index |
| 6 | **Combination Sum** (LC 39)<br>🟡 Medium \| 🔑 Anchor | Backtracking | Unlimited use, stay at index |
| 7 | **Combination Sum II** (LC 40)<br>🟡 Medium \| ⭐ High Value | Backtracking | Limited use, skip duplicates |
| 8 | **Letter Combinations Phone** (LC 17)<br>🟡 Medium \| 🎯 Practice | Backtracking | Digit→letters mapping |
| 9 | **Generate Parentheses** (LC 22)<br>🟡 Medium \| ⭐ High Value | Backtracking | Track open/close count |
| 10 | **Word Search** (LC 79)<br>🟡 Medium \| 🔑 Anchor | Backtracking + Grid | 4-directional, mark visited |

| 11 | Palindrome Partitioning | 131 | M | 💡 | Backtracking | 45/35/25 |
| 12 | N-Queens | 51 | H | 💡 | Backtracking | 50/40/30 |
| 13 | N-Queens II | 52 | H | 💡 | Backtracking | 50/40/30 |
| 14 | Sudoku Solver | 37 | H | 💡 | Backtracking | 60/50/40 |
| 15 | Restore IP Addresses | 93 | M | 🎯 | Backtracking | 35/28/20 |
| 16 | Combination Sum III | 216 | M | 🎯 | Backtracking | 30/22/15 |
| 17 | Word Search II | 212 | H | 💡 | Backtracking + Trie | 60/50/40 |
| 18 | Expression Add Operators | 282 | H | 💡 | Backtracking | 60/50/40 |


**Pattern Connections**:
- Subsets (78) → Subsets II (90) → Power Set variations
- Permutations (46) → Permutations II (47) → Next Permutation (31)
- Combination Sum (39) → Combination Sum II (40) → Combination Sum III (216)
- Generate Parentheses (22) → Remove Invalid Parentheses (301)

---

## Phase 9: Heaps, Tries, and Union-Find

Time for advanced data structures. Use Heaps for top-K problems, Tries for prefix-based string searches, and Union-Find for dynamic connectivity and cycle detection.



| # | Problem & Difficulty | Pattern | Key Concept |
|---|----------------------|---------|-------------|
| 1 | **Kth Largest Element in Array** (LC 215)<br>🟡 Medium \| 🔑 Anchor | Heap/Quickselect | Min heap of size k, O(n log k) |
| 2 | **Top K Frequent Elements** (LC 347)<br>🟡 Medium \| 🔑 Anchor | Heap + Hash Map | Frequency map + min heap |
| 3 | **K Closest Points to Origin** (LC 973)<br>🟡 Medium \| 🎯 Practice | Heap | Max heap of size k by distance |
| 4 | **Find Median from Data Stream** (LC 295)<br>🔴 Hard \| 🔑 Anchor | Two Heaps | Max heap (left) + min heap (right) |
| 5 | **Merge k Sorted Lists** (LC 23)<br>🔴 Hard \| ⭐ High Value | Heap | K-way merge with min heap |
| 6 | **Last Stone Weight** (LC 1046)<br>🟢 Easy \| 🎯 Practice | Max Heap | Simple heap simulation |
| 7 | **Number of Connected Components** (LC 323)<br>🟡 Medium \| 🔑 Anchor | Union-Find | Template, count components |
| 8 | **Graph Valid Tree** (LC 261)<br>🟡 Medium \| ⭐ High Value | Union-Find | n-1 edges, no cycles |
| 9 | **Implement Trie** (LC 208)<br>🟡 Medium \| 🔑 Anchor | Trie Design | Insert, search, startsWith |
| 10 | **Design Add Search Words DS** (LC 211)<br>🟡 Medium \| 💡 Advanced | Trie + Backtracking | Wildcard search with '.' |

| 11 | Kth Smallest in Sorted Matrix | 378 | M | ⭐ | Heap/Binary Search | 40/30/22 |
| 12 | Ugly Number II | 264 | M | 💡 | Heap/DP | 40/30/22 |
| 13 | Find K Pairs with Smallest Sums | 373 | M | 💡 | Heap | 45/35/25 |
| 14 | Smallest Range Covering K Lists | 632 | H | 💡 | Heap | 60/50/40 |
| 15 | Replace Words | 648 | M | ⭐ | Trie | 35/25/18 |
| 16 | Word Search II | 212 | H | 💡 | Trie + Backtracking | 60/50/40 |
| 17 | Redundant Connection | 684 | M | ⭐ | Union-Find | 35/28/20 |


**Pattern Connections**:
- Kth Largest (215) → Kth Smallest Matrix (378) → Find Median (295)
- Top K Frequent (347) → K Closest Points (973) → K Closest Elements (658)
- Trie (208) → Add Search Words (211) → Word Search II (212)
- Union-Find (323) → Graph Valid Tree (261) → Accounts Merge (721)

---

## Phase 1: Arrays, Strings, and Hashing

This is the bread and butter of technical interviews. We'll focus heavily on Hash Maps for O(1) lookups and Two-Pointer techniques. Master the Sliding Window pattern here—it's incredibly high ROI.



| # | Problem & Difficulty | Pattern | Key Concept |
|---|----------------------|---------|-------------|
| 1 | **Network Delay Time** (LC 743)<br>🟡 Medium \| 🔑 Anchor | Dijkstra | Single-source shortest path |
| 2 | **Cheapest Flights Within K Stops** (LC 787)<br>🟡 Medium \| ⭐ High Value | BFS/DP | K-constrained shortest path |
| 3 | **Path with Maximum Probability** (LC 1514)<br>🟡 Medium \| 🎯 Practice | Dijkstra Variant | Max heap by probability |
| 4 | **Min Cost to Connect All Points** (LC 1584)<br>🟡 Medium \| 🔑 Anchor | MST (Prim/Kruskal) | Minimum spanning tree |
| 5 | **Critical Connections in Network** (LC 1192)<br>🔴 Hard \| 💡 Advanced | Tarjan's Algorithm | Bridges, low-link values |
| 6 | **Swim in Rising Water** (LC 778)<br>🔴 Hard \| 💡 Advanced | Binary Search/Dijkstra | Time binary search |

| 7 | Shortest Path in Binary Matrix | 1091 | M | ⭐ | BFS | 35/25/18 |
| 8 | All Paths from Source to Target | 797 | M | 🎯 | DFS/Backtracking | 35/25/18 |
| 9 | Find City With Smallest Neighbors | 1334 | M | ⭐ | Floyd-Warshall | 40/30/22 |
| 10 | Min Cost to Reach Destination | 1928 | H | 💡 | Dijkstra + DP | 60/50/40 |
| 11 | Shortest Path Visiting All Nodes | 847 | H | 💡 | BFS + Bitmask | 60/50/40 |
| 12 | Reachable Nodes in Subdivided Graph | 882 | H | 💡 | Dijkstra | 60/50/40 |


**Pattern Connections**:
- Network Delay (743) → Path with Max Probability (1514) → Dijkstra variants
- Min Cost Connect (1584) → Connecting Cities (1135) → MST problems
- Cheapest Flights (787) → Min Cost Destination (1928) → Constrained shortest path

---

## Phase 1: Arrays, Strings, and Hashing

This is the bread and butter of technical interviews. We'll focus heavily on Hash Maps for O(1) lookups and Two-Pointer techniques. Master the Sliding Window pattern here—it's incredibly high ROI.



| # | Problem & Difficulty | Pattern | Key Concept |
|---|----------------------|---------|-------------|
| 1 | **LRU Cache** (LC 146)<br>🟡 Medium \| 🔑 Anchor | Design | DLL + hash map, O(1) operations |
| 2 | **Trapping Rain Water** (LC 42)<br>🔴 Hard \| 🔑 Anchor | Two Pointers/Stack | min(maxLeft, maxRight) - height |
| 3 | **Longest Consecutive Sequence** (LC 128)<br>🟡 Medium \| ⭐ High Value | Hash Set | O(n), check sequence starts |
| 4 | **Basic Calculator II** (LC 227)<br>🟡 Medium \| ⭐ High Value | Stack | Operator precedence, stack |
| 5 | **Sliding Window Maximum** (LC 239)<br>🔴 Hard \| 💡 Advanced | Monotonic Deque | Decreasing deque, max in window |
| 6 | **Largest Rectangle in Histogram** (LC 84)<br>🔴 Hard \| 💡 Advanced | Monotonic Stack | Increasing stack, area calculation |
| 7 | **Valid Sudoku** (LC 36)<br>🟡 Medium \| 🎯 Practice | Hash Set | Three sets: row, col, box |
| 8 | **Rotate Image** (LC 48)<br>🟡 Medium \| 🎯 Practice | Matrix | Transpose + reverse rows |
| 9 | **Spiral Matrix** (LC 54)<br>🟡 Medium \| 🎯 Practice | Matrix | Four boundaries, spiral traversal |
| 10 | **Insert Delete GetRandom O(1)** (LC 380)<br>🟡 Medium \| ⭐ High Value | Array + Hash Map | Map: val→index, swap with last |
| 11 | **Time Based Key-Value Store** (LC 981)<br>🟡 Medium \| ⭐ High Value | Hash Map + BS | Map to (timestamp, value) list |
| 12 | **Encode and Decode Strings** (LC 271)<br>🟡 Medium \| ⭐ High Value | String Design | Length prefix: "4:word5:hello" |

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
