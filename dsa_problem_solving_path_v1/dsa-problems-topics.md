# DSA Comprehensive Problem List with Topics & LeetCode Numbers

This file maps each Phase from the learning syllabus to a comprehensive problem list. For each problem:

- **Problem name** and **LeetCode number**
- **Difficulty** (Easy / Medium / Hard)
- **Topics/Patterns to master** before solving it
- **Core concepts** needed

Use this with `dsa-syllabus.md` for theory and solve problems phase by phase.

---

## Phase 0 – Foundations & Complexity

**Focus:** Big-O, recurrences, loop invariants, basic proofs

### Problems

| # | Problem | LeetCode # | Difficulty | Topics to Master | Core Concepts |
|---|---------|-----------|------------|------------------|---------------|
| 1 | Binary Search | 704 | Easy | Array, Binary Search | Logarithmic complexity, search space halving |
| 2 | First Bad Version | 278 | Easy | Binary Search | API-based search, minimization |
| 3 | Climbing Stairs | 70 | Easy | DP, Math | Recurrence relation, Fibonacci pattern |
| 4 | Power of Two | 231 | Easy | Bit Manipulation, Math | Binary representation, powers |
| 5 | Power of Three | 326 | Easy | Math | Logarithmic properties |
| 6 | Counting Bits | 338 | Easy | DP, Bit Manipulation | Hamming weight, DP transitions |
| 7 | Find Pivot Index | 724 | Easy | Array, Prefix Sum | Running sum, balance point |

**Total: 7 problems (7 Easy)**

---

## Phase 1 – Arrays, Strings, and Hashing

**Focus:** Array indexing, two-pointers, sliding window, hash maps, frequency counting

### Problems

| # | Problem | LeetCode # | Difficulty | Topics to Master | Core Concepts |
|---|---------|-----------|------------|------------------|---------------|
| 1 | Two Sum | 1 | Easy | Array, Hash Map | Complement search, O(1) lookup |
| 2 | Contains Duplicate | 217 | Easy | Array, Hash Set | Uniqueness check, set properties |
| 3 | Valid Anagram | 242 | Easy | String, Hash Map, Sorting | Character frequency, anagram definition |
| 4 | Best Time to Buy and Sell Stock | 121 | Easy | Array, Greedy | Min tracking, max profit |
| 5 | Valid Palindrome | 125 | Easy | String, Two Pointers | Character filtering, pointer movement |
| 6 | Merge Sorted Array | 88 | Easy | Array, Two Pointers | In-place merge, backward iteration |
| 7 | Majority Element | 169 | Easy | Array, Boyer-Moore Voting | Majority definition, voting algorithm |
| 8 | Move Zeroes | 283 | Easy | Array, Two Pointers | In-place swap, maintaining order |
| 9 | Length of Last Word | 58 | Easy | String | Reverse iteration, word boundary |
| 10 | Rotate Array | 189 | Easy | Array | Cyclic rotation, reversal technique |
| 11 | Palindrome Number | 9 | Easy | Math | Integer manipulation, reversal |
| 12 | Single Number | 136 | Easy | Bit Manipulation, XOR | XOR properties, cancellation |
| 13 | Unique Number of Occurrences | 1207 | Easy | Hash Map | Frequency map, uniqueness |
| 14 | Longest Substring Without Repeating Characters | 3 | Medium | String, Sliding Window, Hash Set | Window expansion/contraction, seen characters |
| 15 | Longest Palindromic Substring | 5 | Medium | String, DP, Expand Around Center | Palindrome checking, two-pointer expansion |
| 16 | Container With Most Water | 11 | Medium | Array, Two Pointers, Greedy | Area calculation, pointer movement strategy |
| 17 | 3Sum | 15 | Medium | Array, Two Pointers, Sorting | Duplicate handling, fixed + moving pointers |
| 18 | Group Anagrams | 49 | Medium | String, Hash Map | Sorted string as key, grouping |
| 19 | Longest Repeating Character Replacement | 424 | Medium | String, Sliding Window | Character frequency, k replacements |
| 20 | Product of Array Except Self | 238 | Medium | Array, Prefix/Suffix Product | Space optimization, running products |
| 21 | Top K Frequent Elements | 347 | Medium | Array, Hash Map, Heap, Bucket Sort | Frequency counting, selection algorithms |
| 22 | Kth Largest Element in an Array | 215 | Medium | Array, Heap, Quickselect | Partitioning, selection without full sort |
| 23 | Increasing Triplet Subsequence | 334 | Medium | Array, Greedy | Min tracking, triplet condition |
| 24 | Subarray Sum Equals K | 560 | Medium | Array, Hash Map, Prefix Sum | Cumulative sum, sum lookup |
| 25 | Zigzag Conversion | 6 | Medium | String, Simulation | Pattern recognition, row mapping |
| 26 | Longest Common Prefix | 14 | Easy | String, Array | Vertical scanning, early termination |
| 27 | Find All Numbers Disappeared in Array | 448 | Easy | Array, Hash Set | Index marking, in-place detection |
| 28 | Missing Number | 268 | Easy | Array, Math, Bit Manipulation | Sum formula, XOR technique |

**Total: 28 problems (13 Easy, 15 Medium)**

---

## Phase 2 – Linked Lists, Stacks, and Queues

**Focus:** Pointer manipulation, fast/slow pointers, stack/queue operations

### Problems

| # | Problem | LeetCode # | Difficulty | Topics to Master | Core Concepts |
|---|---------|-----------|------------|------------------|---------------|
| 1 | Reverse Linked List | 206 | Easy | Linked List, Recursion | Pointer reversal, iterative vs recursive |
| 2 | Merge Two Sorted Lists | 21 | Easy | Linked List, Recursion | Merging logic, dummy node |
| 3 | Linked List Cycle | 141 | Easy | Linked List, Two Pointers | Floyd's cycle detection, fast/slow |
| 4 | Middle of the Linked List | 876 | Easy | Linked List, Two Pointers | Fast/slow pointer, middle finding |
| 5 | Palindrome Linked List | 234 | Easy | Linked List, Two Pointers, Stack | Middle + reverse, comparison |
| 6 | Remove Linked List Elements | 203 | Easy | Linked List | Conditional removal, dummy head |
| 7 | Valid Parentheses | 20 | Easy | String, Stack | Matching pairs, LIFO property |
| 8 | Min Stack | 155 | Medium | Stack, Design | Auxiliary stack, constant-time min |
| 9 | Add Two Numbers | 2 | Medium | Linked List, Math | Digit-by-digit addition, carry handling |
| 10 | Swap Nodes in Pairs | 24 | Medium | Linked List, Recursion | Pair swapping, pointer manipulation |
| 11 | Reverse Nodes in k-Group | 25 | Hard | Linked List, Recursion | Group reversal, complex pointer logic |
| 12 | Rotate List | 61 | Medium | Linked List, Two Pointers | Circular linking, rotation point |
| 13 | Linked List Cycle II | 142 | Medium | Linked List, Two Pointers | Cycle detection + entry point |
| 14 | Reorder List | 143 | Medium | Linked List, Two Pointers | Middle + reverse + merge |
| 15 | Odd Even Linked List | 328 | Medium | Linked List | Odd/even separation, pointer tracking |
| 16 | Daily Temperatures | 739 | Medium | Array, Stack, Monotonic Stack | Next greater element, index stack |
| 17 | Evaluate Reverse Polish Notation | 150 | Medium | Array, Stack | Operand/operator stack, postfix evaluation |
| 18 | Online Stock Span | 901 | Medium | Stack, Monotonic Stack, Design | Span calculation, decreasing stack |
| 19 | Remove Nth Node From End | 19 | Medium | Linked List, Two Pointers | N-ahead pointer, one-pass removal |
| 20 | Implement Queue using Stacks | 232 | Easy | Stack, Queue, Design | Two-stack technique, amortized O(1) |
| 21 | Implement Stack using Queues | 225 | Easy | Stack, Queue, Design | Queue operations, stack simulation |

**Total: 21 problems (7 Easy, 12 Medium, 2 Hard)**

---

## Phase 3 – Trees, BSTs, and Balanced Trees

**Focus:** Tree traversals, recursion patterns, BST invariants, height balance

### Problems

| # | Problem | LeetCode # | Difficulty | Topics to Master | Core Concepts |
|---|---------|-----------|------------|------------------|---------------|
| 1 | Maximum Depth of Binary Tree | 104 | Easy | Tree, DFS, Recursion | Height calculation, base case |
| 2 | Same Tree | 100 | Easy | Tree, DFS | Structural + value equality |
| 3 | Invert Binary Tree | 226 | Easy | Tree, DFS, BFS | Subtree swapping, recursion |
| 4 | Symmetric Tree | 101 | Easy | Tree, DFS, BFS | Mirror checking, dual recursion |
| 5 | Diameter of Binary Tree | 543 | Easy | Tree, DFS | Path length, height tracking |
| 6 | Balanced Binary Tree | 110 | Easy | Tree, DFS | Height balance, definition |
| 7 | Minimum Depth of Binary Tree | 111 | Easy | Tree, DFS, BFS | Leaf detection, shortest path |
| 8 | Path Sum | 112 | Easy | Tree, DFS | Target sum, root-to-leaf path |
| 9 | Merge Two Binary Trees | 617 | Easy | Tree, DFS | Simultaneous traversal, addition |
| 10 | Binary Tree Level Order Traversal | 102 | Medium | Tree, BFS | Level separation, queue usage |
| 11 | Binary Tree Zigzag Level Order Traversal | 103 | Medium | Tree, BFS | Direction alternation, deque |
| 12 | Binary Tree Right Side View | 199 | Medium | Tree, BFS, DFS | Rightmost per level, level-order |
| 13 | Validate Binary Search Tree | 98 | Medium | Tree, DFS, BST | In-order traversal, range checking |
| 14 | Lowest Common Ancestor of BST | 235 | Easy | Tree, BST | BST property, ancestor logic |
| 15 | Lowest Common Ancestor of Binary Tree | 236 | Medium | Tree, DFS | Ancestor definition, path tracking |
| 16 | Kth Smallest Element in a BST | 230 | Medium | Tree, DFS, BST | In-order traversal, counting |
| 17 | Convert Sorted Array to BST | 108 | Easy | Array, Tree, BST | Middle element, divide-and-conquer |
| 18 | Binary Tree Inorder Traversal | 94 | Easy | Tree, DFS, Stack | Left-root-right, iterative approach |
| 19 | Binary Tree Preorder Traversal | 144 | Easy | Tree, DFS, Stack | Root-left-right, stack usage |
| 20 | Binary Tree Postorder Traversal | 145 | Easy | Tree, DFS, Stack | Left-right-root, complex iterative |
| 21 | Subtree of Another Tree | 572 | Easy | Tree, DFS | Subtree matching, recursion |
| 22 | Construct Binary Tree from Preorder and Inorder | 105 | Medium | Tree, Array, DFS | Root identification, recursion |
| 23 | Construct Binary Tree from Inorder and Postorder | 106 | Medium | Tree, Array, DFS | Root from postorder, recursion |
| 24 | Flatten Binary Tree to Linked List | 114 | Medium | Tree, DFS | Preorder flattening, pointer manipulation |
| 25 | Binary Tree Maximum Path Sum | 124 | Hard | Tree, DFS | Path definition, global maximum |
| 26 | Serialize and Deserialize Binary Tree | 297 | Hard | Tree, Design, BFS/DFS | Encoding/decoding, delimiter usage |
| 27 | Count Good Nodes in Binary Tree | 1448 | Medium | Tree, DFS | Path maximum, counting |

**Total: 27 problems (13 Easy, 11 Medium, 3 Hard)**

---

## Phase 4 – Graphs and Graph Traversal

**Focus:** Graph representations, BFS/DFS, connectivity, topological sort

### Problems

| # | Problem | LeetCode # | Difficulty | Topics to Master | Core Concepts |
|---|---------|-----------|------------|------------------|---------------|
| 1 | Flood Fill | 733 | Easy | Array, DFS, BFS | Connected component coloring |
| 2 | Number of Islands | 200 | Medium | Array, DFS, BFS, Union-Find | Grid traversal, component counting |
| 3 | Max Area of Island | 695 | Medium | Array, DFS, BFS | Area calculation, component size |
| 4 | Clone Graph | 133 | Medium | Graph, DFS, BFS, Hash Map | Deep copy, node mapping |
| 5 | Number of Provinces | 547 | Medium | Graph, DFS, BFS, Union-Find | Adjacency matrix, connected components |
| 6 | Rotting Oranges | 994 | Medium | Array, BFS | Multi-source BFS, time tracking |
| 7 | Course Schedule | 207 | Medium | Graph, DFS, BFS, Topological Sort | Cycle detection, DAG verification |
| 8 | Course Schedule II | 210 | Medium | Graph, DFS, BFS, Topological Sort | Kahn's algorithm, ordering |
| 9 | Surrounded Regions | 130 | Medium | Array, DFS, BFS | Border search, marking |
| 10 | Pacific Atlantic Water Flow | 417 | Medium | Array, DFS, BFS | Multi-source search, intersection |
| 11 | Graph Valid Tree | 261 | Medium | Graph, DFS, BFS, Union-Find | Tree conditions, cycle + connectivity |
| 12 | Word Ladder | 127 | Hard | String, BFS | Transformation graph, shortest path |
| 13 | Keys and Rooms | 841 | Medium | Graph, DFS, BFS | Reachability, key-door model |
| 14 | Nearest Exit from Entrance in Maze | 1926 | Medium | Array, BFS | Shortest path in grid, boundary check |
| 15 | All Paths From Source to Target | 797 | Medium | Graph, DFS, Backtracking | Path enumeration, DAG |
| 16 | Find if Path Exists in Graph | 1971 | Easy | Graph, DFS, BFS, Union-Find | Basic connectivity |
| 17 | Minimum Height Trees | 310 | Medium | Graph, BFS, Topological Sort | Tree center, leaf pruning |
| 18 | Redundant Connection | 684 | Medium | Graph, DFS, Union-Find | Cycle detection, edge removal |
| 19 | Accounts Merge | 721 | Medium | Graph, DFS, BFS, Union-Find | Email grouping, connected components |

**Total: 19 problems (1 Easy, 16 Medium, 2 Hard)**

---

## Phase 5 – Sorting, Searching, and Divide & Conquer

**Focus:** Sorting algorithms, binary search variants, selection algorithms

### Problems

| # | Problem | LeetCode # | Difficulty | Topics to Master | Core Concepts |
|---|---------|-----------|------------|------------------|---------------|
| 1 | Sort an Array | 912 | Medium | Array, Sorting | Quicksort, mergesort implementation |
| 2 | Merge Intervals | 56 | Medium | Array, Sorting | Overlap detection, interval merging |
| 3 | Insert Interval | 57 | Medium | Array | Interval insertion, merge logic |
| 4 | Non-overlapping Intervals | 435 | Medium | Array, Greedy, Sorting | Interval scheduling, removal count |
| 5 | Meeting Rooms | 252 | Easy | Array, Sorting | Overlap check, time sorting |
| 6 | Meeting Rooms II | 253 | Medium | Array, Heap, Sorting | Room allocation, min heap |
| 7 | Search in Rotated Sorted Array | 33 | Medium | Array, Binary Search | Pivot detection, half selection |
| 8 | Find Minimum in Rotated Sorted Array | 153 | Medium | Array, Binary Search | Minimum finding, rotation handling |
| 9 | Find First and Last Position | 34 | Medium | Array, Binary Search | Binary search variants, boundary search |
| 10 | Search a 2D Matrix | 74 | Medium | Array, Binary Search | Matrix as sorted array |
| 11 | Search a 2D Matrix II | 240 | Medium | Array, Binary Search | Step-wise search, elimination |
| 12 | Find Peak Element | 162 | Medium | Array, Binary Search | Peak definition, gradient descent |
| 13 | Koko Eating Bananas | 875 | Medium | Array, Binary Search | Rate binary search, feasibility check |
| 14 | Capacity To Ship Packages Within D Days | 1011 | Medium | Array, Binary Search | Capacity search, day simulation |
| 15 | Sort Colors | 75 | Medium | Array, Two Pointers, Sorting | Dutch National Flag, three-way partition |
| 16 | Median of Two Sorted Arrays | 4 | Hard | Array, Binary Search | Partition search, median definition |
| 17 | Find Median from Data Stream | 295 | Hard | Heap, Design | Two heaps, balance maintenance |
| 18 | Kth Largest Element in a Stream | 703 | Easy | Heap, Design | Min heap, k-size maintenance |

**Total: 18 problems (2 Easy, 13 Medium, 3 Hard)**

---

## Phase 6 – Greedy Algorithms

**Focus:** Greedy choice property, proof techniques, activity selection

### Problems

| # | Problem | LeetCode # | Difficulty | Topics to Master | Core Concepts |
|---|---------|-----------|------------|------------------|---------------|
| 1 | Maximum Subarray | 53 | Medium | Array, DP, Greedy | Kadane's algorithm, local/global max |
| 2 | Jump Game | 55 | Medium | Array, Greedy | Reachability, furthest position |
| 3 | Jump Game II | 45 | Medium | Array, Greedy | Minimum jumps, greedy range |
| 4 | Gas Station | 134 | Medium | Array, Greedy | Circular array, surplus/deficit |
| 5 | Hand of Straights | 846 | Medium | Array, Greedy, Sorting | Consecutive groups, frequency map |
| 6 | Partition Labels | 763 | Medium | String, Greedy | Last occurrence, partition boundary |
| 7 | Minimum Number of Arrows to Burst Balloons | 452 | Medium | Array, Greedy, Sorting | Interval scheduling, shooting position |
| 8 | Task Scheduler | 621 | Medium | Array, Greedy, Heap | Cooling period, idle calculation |
| 9 | Candy | 135 | Hard | Array, Greedy | Two-pass, neighbor comparison |
| 10 | Assign Cookies | 455 | Easy | Array, Greedy, Sorting | Matching, satisfaction |
| 11 | Lemonade Change | 860 | Easy | Array, Greedy | Change-making, bill tracking |
| 12 | Best Time to Buy and Sell Stock II | 122 | Medium | Array, Greedy | Multiple transactions, profit accumulation |
| 13 | Remove K Digits | 402 | Medium | String, Stack, Greedy | Monotonic stack, digit removal |

**Total: 13 problems (2 Easy, 9 Medium, 2 Hard)**

---

## Phase 7 – Dynamic Programming

**Focus:** DP states, recurrence relations, memoization vs tabulation, space optimization

### Problems

| # | Problem | LeetCode # | Difficulty | Topics to Master | Core Concepts |
|---|---------|-----------|------------|------------------|---------------|
| 1 | Climbing Stairs | 70 | Easy | DP, Math | 1D DP, Fibonacci relation |
| 2 | Min Cost Climbing Stairs | 746 | Easy | DP | Cost accumulation, choice at each step |
| 3 | House Robber | 198 | Medium | Array, DP | Non-adjacent selection, state transition |
| 4 | House Robber II | 213 | Medium | Array, DP | Circular array, two sub-problems |
| 5 | Longest Palindromic Substring | 5 | Medium | String, DP | 2D DP table, expand around center |
| 6 | Palindromic Substrings | 647 | Medium | String, DP | Counting, expand around center |
| 7 | Decode Ways | 91 | Medium | String, DP | Digit grouping, valid decoding |
| 8 | Coin Change | 322 | Medium | Array, DP | Unbounded knapsack, minimum coins |
| 9 | Maximum Product Subarray | 152 | Medium | Array, DP | Max/min tracking, negative handling |
| 10 | Word Break | 139 | Medium | String, DP, Trie | Substring matching, DP validity |
| 11 | Longest Increasing Subsequence | 300 | Medium | Array, DP, Binary Search | LIS definition, O(n log n) approach |
| 12 | Partition Equal Subset Sum | 416 | Medium | Array, DP | 0/1 knapsack, subset sum |
| 13 | Unique Paths | 62 | Medium | Math, DP | Grid DP, path counting |
| 14 | Unique Paths II | 63 | Medium | Array, DP | Obstacles, path validity |
| 15 | Longest Common Subsequence | 1143 | Medium | String, DP | 2D DP, LCS definition |
| 16 | Edit Distance | 72 | Hard | String, DP | Levenshtein distance, operations |
| 17 | Regular Expression Matching | 10 | Hard | String, DP | Pattern matching, * and . |
| 18 | Wildcard Matching | 44 | Hard | String, DP | ? and * matching, DP table |
| 19 | Best Time to Buy and Sell Stock with Cooldown | 309 | Medium | Array, DP | State machine, cooldown period |
| 20 | Combination Sum IV | 377 | Medium | Array, DP | Order matters, combinations |
| 21 | Target Sum | 494 | Medium | Array, DP | +/- assignment, subset sum |
| 22 | Interleaving String | 97 | Medium | String, DP | 2D DP, string formation |
| 23 | Maximal Square | 221 | Medium | Array, DP | 2D DP, square size |
| 24 | Unique Binary Search Trees | 96 | Medium | Math, DP | Catalan number, tree counting |
| 25 | Minimum Path Sum | 64 | Medium | Array, DP | Grid path, cost minimization |
| 26 | Triangle | 120 | Medium | Array, DP | Bottom-up, path sum |
| 27 | Delete and Earn | 740 | Medium | Array, DP | House robber variant, frequency |
| 28 | Domino and Tromino Tiling | 790 | Medium | DP | Recurrence, tile placement |
| 29 | Nth Tribonacci Number | 1137 | Easy | DP, Math | Three-term recurrence |
| 30 | Burst Balloons | 312 | Hard | Array, DP | Interval DP, balloon popping |

**Total: 30 problems (3 Easy, 23 Medium, 4 Hard)**

---

## Phase 8 – Backtracking, Recursion, and Combinatorial Search

**Focus:** Recursive decomposition, backtracking template, pruning strategies

### Problems

| # | Problem | LeetCode # | Difficulty | Topics to Master | Core Concepts |
|---|---------|-----------|------------|------------------|---------------|
| 1 | Subsets | 78 | Medium | Array, Backtracking | Include/exclude, power set |
| 2 | Subsets II | 90 | Medium | Array, Backtracking | Duplicate handling, sorting |
| 3 | Permutations | 46 | Medium | Array, Backtracking | Order matters, swapping |
| 4 | Permutations II | 47 | Medium | Array, Backtracking | Duplicates, frequency map |
| 5 | Combinations | 77 | Medium | Backtracking | Fixed size, starting index |
| 6 | Combination Sum | 39 | Medium | Array, Backtracking | Unlimited use, target sum |
| 7 | Combination Sum II | 40 | Medium | Array, Backtracking | Limited use, duplicate skipping |
| 8 | Combination Sum III | 216 | Medium | Array, Backtracking | Fixed count, digit constraints |
| 9 | Letter Combinations of a Phone Number | 17 | Medium | String, Backtracking | Mapping, Cartesian product |
| 10 | Word Search | 79 | Medium | Array, Backtracking | Grid search, path tracking |
| 11 | Palindrome Partitioning | 131 | Medium | String, Backtracking | Substring palindrome, partitioning |
| 12 | Generate Parentheses | 22 | Medium | String, Backtracking | Valid parentheses, open/close count |
| 13 | N-Queens | 51 | Hard | Array, Backtracking | Constraint satisfaction, board state |
| 14 | N-Queens II | 52 | Hard | Backtracking | Count only, optimized |
| 15 | Sudoku Solver | 37 | Hard | Array, Backtracking | Constraint propagation, board validation |
| 16 | Word Search II | 212 | Hard | Array, String, Backtracking, Trie | Multi-word search, prefix optimization |
| 17 | Restore IP Addresses | 93 | Medium | String, Backtracking | Segment validity, dot placement |
| 18 | Partition to K Equal Sum Subsets | 698 | Medium | Array, Backtracking, DP | Subset formation, target matching |

**Total: 18 problems (0 Easy, 13 Medium, 5 Hard)**

---

## Phase 9 – Heaps, Priority Queues, and Advanced Data Structures

**Focus:** Heap operations, priority queue use cases, disjoint-set

### Problems

| # | Problem | LeetCode # | Difficulty | Topics to Master | Core Concepts |
|---|---------|-----------|------------|------------------|---------------|
| 1 | Kth Largest Element in an Array | 215 | Medium | Array, Heap, Quickselect | Min heap of size k, selection |
| 2 | Last Stone Weight | 1046 | Easy | Array, Heap | Max heap, simulation |
| 3 | Top K Frequent Elements | 347 | Medium | Array, Hash Map, Heap | Frequency map, min heap |
| 4 | K Closest Points to Origin | 973 | Medium | Array, Heap, Math | Distance calculation, max heap |
| 5 | Task Scheduler | 621 | Medium | Array, Greedy, Heap | Cooling period, max heap |
| 6 | Find Median from Data Stream | 295 | Hard | Heap, Design | Two heaps, balance |
| 7 | IPO | 502 | Hard | Array, Greedy, Heap | Profit maximization, two heaps |
| 8 | Merge k Sorted Lists | 23 | Hard | Linked List, Heap | K-way merge, min heap |
| 9 | Smallest Range Covering Elements from K Lists | 632 | Hard | Array, Heap, Sliding Window | Range tracking, multi-list |
| 10 | Number of Islands | 200 | Medium | Array, DFS, BFS, Union-Find | Component counting, union-find |
| 11 | Graph Valid Tree | 261 | Medium | Graph, Union-Find | Cycle detection, connectivity |
| 12 | Redundant Connection | 684 | Medium | Graph, Union-Find | Cycle causing edge |
| 13 | Accounts Merge | 721 | Medium | Graph, Union-Find | Component merging |
| 14 | Number of Connected Components | 323 | Medium | Graph, Union-Find | Component counting |
| 15 | Implement Trie (Prefix Tree) | 208 | Medium | String, Trie, Design | Insert, search, prefix |
| 16 | Design Add and Search Words DS | 211 | Medium | String, Trie, Backtracking | Wildcard search |
| 17 | Word Search II | 212 | Hard | Array, Trie, Backtracking | Multi-word, prefix tree |

**Total: 17 problems (1 Easy, 11 Medium, 5 Hard)**

---

## Phase 10 – Graph Shortest Paths, MST, and Flow (Optional Deep Dive)

**Focus:** Dijkstra, Bellman-Ford, MST algorithms, max-flow concepts

### Problems

| # | Problem | LeetCode # | Difficulty | Topics to Master | Core Concepts |
|---|---------|-----------|------------|------------------|---------------|
| 1 | Network Delay Time | 743 | Medium | Graph, Dijkstra, Heap | Single-source shortest path, min heap |
| 2 | Cheapest Flights Within K Stops | 787 | Medium | Graph, BFS, DP | K-constrained shortest path |
| 3 | Path with Maximum Probability | 1514 | Medium | Graph, Dijkstra | Probability product, max heap |
| 4 | Minimum Cost to Connect All Points | 1584 | Medium | Array, MST, Union-Find | Prim/Kruskal, distance calculation |
| 5 | Connecting Cities With Minimum Cost | 1135 | Medium | Graph, MST, Union-Find | Kruskal's algorithm |
| 6 | Min Cost to Connect All Points | 1584 | Medium | Array, MST | Manhattan distance, MST |
| 7 | Dijkstra's Algorithm (Template) | N/A | Medium | Graph, Heap | Priority queue, relaxation |
| 8 | Bellman-Ford Algorithm (Template) | N/A | Medium | Graph, DP | Negative edges, distance array |
| 9 | Floyd-Warshall (Template) | N/A | Medium | Graph, DP | All-pairs shortest path |
| 10 | Swim in Rising Water | 778 | Hard | Array, Binary Search, Union-Find | Time binary search, reachability |
| 11 | Shortest Path Visiting All Nodes | 847 | Hard | Graph, BFS, Bit Manipulation | State-space BFS, bitmask |
| 12 | Critical Connections in a Network | 1192 | Hard | Graph, DFS | Tarjan's algorithm, bridges |

**Total: 12 problems (0 Easy, 9 Medium, 3 Hard)**

---

## Phase 11 – Interview-Oriented Mixed Practice

**Focus:** Pattern recognition, problem-solving speed, communication

### Additional High-Value Problems

| # | Problem | LeetCode # | Difficulty | Topics to Master | Core Concepts |
|---|---------|-----------|------------|------------------|---------------|
| 1 | LRU Cache | 146 | Medium | Hash Map, Linked List, Design | Doubly-linked list, O(1) ops |
| 2 | LFU Cache | 460 | Hard | Hash Map, Linked List, Design | Frequency tracking, min-freq |
| 3 | Design Twitter | 355 | Medium | Hash Map, Heap, Design | Timeline merge, k-way merge |
| 4 | Random Pick with Weight | 528 | Medium | Array, Binary Search, Prefix Sum | Weighted random, cumulative sum |
| 5 | Insert Delete GetRandom O(1) | 380 | Medium | Array, Hash Map, Design | Index mapping, constant time |
| 6 | Encode and Decode Strings | 271 | Medium | String, Design | Delimiter strategy, escaping |
| 7 | Time Based Key-Value Store | 981 | Medium | Hash Map, Binary Search, Design | Timestamp lookup |
| 8 | Design In-Memory File System | 588 | Hard | Hash Map, Trie, Design | Path parsing, directory tree |
| 9 | Trapping Rain Water | 42 | Hard | Array, Two Pointers, Stack | Water volume, min height |
| 10 | Longest Consecutive Sequence | 128 | Medium | Array, Hash Set, Union-Find | O(n) sequence, set lookup |
| 11 | Basic Calculator | 224 | Hard | String, Stack | Expression parsing, parentheses |
| 12 | Basic Calculator II | 227 | Medium | String, Stack | Operator precedence, no parentheses |
| 13 | Sliding Window Maximum | 239 | Hard | Array, Deque, Monotonic Queue | Max in window, deque maintenance |
| 14 | Serialize and Deserialize BST | 449 | Medium | Tree, DFS, Design | Encoding, BST property |
| 15 | Shortest Bridge | 934 | Medium | Array, DFS, BFS | Two-phase: DFS + BFS |
| 16 | Find K Pairs with Smallest Sums | 373 | Medium | Array, Heap | K-way merge, pair generation |
| 17 | Alien Dictionary | 269 | Hard | Graph, Topological Sort | Character ordering, DAG |
| 18 | Integer to English Words | 273 | Hard | String, Math | Number formatting, recursion |
| 19 | Expression Add Operators | 282 | Hard | String, Backtracking | Expression generation, evaluation |
| 20 | Largest Rectangle in Histogram | 84 | Hard | Array, Stack, Monotonic Stack | Height boundaries, area calculation |
| 21 | Maximal Rectangle | 85 | Hard | Array, DP, Stack | 2D histogram, stack application |
| 22 | Valid Sudoku | 36 | Medium | Array, Hash Set | Constraint checking, sets |
| 23 | Spiral Matrix II | 59 | Medium | Array, Matrix, Simulation | Boundary management |
| 24 | Set Matrix Zeroes | 73 | Medium | Array, Matrix | In-place marking, first row/col |
| 25 | Rotate Image | 48 | Medium | Array, Matrix, Math | Transpose + reflect |

**Total: 25 problems (0 Easy, 16 Medium, 9 Hard)**

---

## Summary Statistics

| Phase | Easy | Medium | Hard | Total |
|-------|------|--------|------|-------|
| Phase 0 – Foundations | 7 | 0 | 0 | 7 |
| Phase 1 – Arrays & Strings | 13 | 15 | 0 | 28 |
| Phase 2 – Linked Lists & Stacks | 7 | 12 | 2 | 21 |
| Phase 3 – Trees & BSTs | 13 | 11 | 3 | 27 |
| Phase 4 – Graphs | 1 | 16 | 2 | 19 |
| Phase 5 – Sorting & Search | 2 | 13 | 3 | 18 |
| Phase 6 – Greedy | 2 | 9 | 2 | 13 |
| Phase 7 – Dynamic Programming | 3 | 23 | 4 | 30 |
| Phase 8 – Backtracking | 0 | 13 | 5 | 18 |
| Phase 9 – Heaps & Advanced DS | 1 | 11 | 5 | 17 |
| Phase 10 – Shortest Paths & MST | 0 | 9 | 3 | 12 |
| Phase 11 – Mixed Practice | 0 | 16 | 9 | 25 |
| **TOTAL** | **49** | **148** | **38** | **235** |

---

## How to Use This List

### 1. Study the Corresponding Phase in `dsa-syllabus.md`

Before attempting problems, read the:
- Primary reading (theory from textbooks)
- Supplement / interview focus (applied patterns)
- Key concepts to master (checklist)

### 2. Review "Topics to Master" Column

For each problem, understand the prerequisite concepts listed. If unfamiliar, revisit textbook sections or watch tutorial videos.

### 3. Solve Problems in Order (Easy → Medium → Hard)

Within each phase, tackle easier problems first to build confidence and pattern recognition before moving to harder ones.

### 4. Track Your Progress

- Use checkboxes or spreadsheet to mark completed problems
- Note problems that need revisiting
- Track time taken and solution approaches

### 5. Review and Repeat

After solving a problem:
- Review optimal solution if yours wasn't optimal
- Note the pattern/template used
- Revisit after 1 week, 2 weeks, 1 month (spaced repetition)

### 6. Connect Problems to Patterns

Many problems share underlying patterns:
- **Two Pointers**: Two Sum, 3Sum, Container With Most Water, Trapping Rain Water
- **Sliding Window**: Longest Substring, Min Window Substring, Max Sliding Window
- **Binary Search**: Search in Rotated Array, Koko Eating Bananas, Median of Two Arrays
- **DFS on Trees**: Max Depth, Diameter, Path Sum, Serialize/Deserialize
- **BFS on Graphs**: Number of Islands, Rotting Oranges, Word Ladder
- **Monotonic Stack**: Daily Temperatures, Largest Rectangle, Trapping Rain Water
- **Union-Find**: Number of Islands, Graph Valid Tree, Redundant Connection
- **Interval Merge**: Merge Intervals, Meeting Rooms, Non-overlapping Intervals
- **DP Sequence**: LIS, LCS, Edit Distance, Word Break
- **Backtracking**: Subsets, Permutations, N-Queens, Sudoku

### 7. Supplement with Mock Interviews

After completing 70-80% of a phase:
- Time yourself (45 minutes per problem)
- Explain solution out loud
- Handle follow-up questions (space optimization, edge cases)

---

## Additional Resources

- **LeetCode Patterns**: https://seanprashad.com/leetcode-patterns/
- **NeetCode**: https://neetcode.io (video explanations)
- **Tech Interview Handbook**: https://techinterviewhandbook.org
- **Grind 75**: Customizable study plans
- **AlgoMonster**: Pattern-based learning

---

## Notes

- **LeetCode Premium**: Some problems (marked with 🔒 in LeetCode) require premium subscription
- **Problem Variations**: Some patterns have multiple LeetCode problems—mastering the pattern is more important than solving every single variation
- **Time Investment**: Budget ~30 min for Easy, ~45 min for Medium, ~60 min for Hard per problem (initial attempt)
- **Frustration is Normal**: If stuck after 30 minutes, look at hints or solution—learning the pattern is the goal

Good luck with your DSA journey! 🚀
