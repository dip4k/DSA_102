# 🎯 HYBRID DSA CURRICULUM
## Theory + Problems Integration | 19-Week Complete Mastery Path

---

## 📋 Overview

**Approach**: Learn theory → Apply immediately → Master through practice  
**Duration**: 19 weeks (133 days)  
**Time Commitment**: 10-12 hours/week  
**Total Problems**: 235+ LeetCode problems  
**Focus**: Interview readiness + Deep understanding

### 🎓 Learning Philosophy

1. **Theory First**: Understand WHY before WHAT
2. **Immediate Application**: Solve problems right after learning
3. **Spaced Repetition**: Review problems at Day 3, 10, 24
4. **Pattern Recognition**: Group problems by underlying patterns
5. **Progressive Difficulty**: Easy → Medium → Hard within each topic

### ⏱️ Time Allocation Per Topic

- **📖 Theory Study**: 30% (concept, examples, visualizations)
- **💻 Problem Solving**: 50% (implementation, debugging)
- **📝 Review & Notes**: 20% (pattern notes, edge cases)

---

## 🗓️ Week 1: Foundations & Complexity Analysis

### Day 1-2: Time & Space Complexity

**📖 Theory (3 hours)**
- Big O, Theta, Omega notation
- Best, average, worst case analysis
- Common complexities: O(1), O(log n), O(n), O(n log n), O(n²), O(2ⁿ)
- Space complexity vs auxiliary space
- Amortized analysis basics

**💻 Problems (2 hours)**
1. 🔑 **Binary Search** (LC 704) - O(log n) template
2. 🎯 **First Bad Version** (LC 278) - Minimization variant

**📝 Key Takeaways**
- Master Big O calculation from code
- Recognize when to trade space for time
- Binary search reduces O(n) to O(log n)

---

### Day 3-4: Bit Manipulation & Math Fundamentals

**📖 Theory (2 hours)**
- Binary representation and operations
- AND, OR, XOR, NOT, left/right shift
- Common tricks: `n & (n-1)`, `n & -n`, XOR properties
- GCD, LCM, prime numbers
- Modular arithmetic

**💻 Problems (3 hours)**
3. 🎯 **Power of Two** (LC 231) - n & (n-1) trick
4. 🎯 **Power of Three** (LC 326) - Logarithm approach
5. ⭐ **Counting Bits** (LC 338) - DP + Bit manipulation
6. 🎯 **Single Number** (LC 136) - XOR cancellation

**📝 Key Takeaways**
- XOR properties: a ^ a = 0, a ^ 0 = a
- `n & (n-1)` removes rightmost set bit
- Bit manipulation achieves O(1) space

---

### Day 5-7: Basic DP & Prefix Sum Introduction

**📖 Theory (3 hours)**
- What is Dynamic Programming?
- Overlapping subproblems + Optimal substructure
- Top-down (memoization) vs Bottom-up (tabulation)
- State definition and recurrence relation
- Prefix sum technique

**💻 Problems (4 hours)**
7. 🔑 **Climbing Stairs** (LC 70) - Classic Fibonacci DP
8. 🔑 **Find Pivot Index** (LC 724) - Prefix sum pattern

**📝 Key Takeaways**
- DP State: `dp[i]` = optimal solution for subproblem i
- Recurrence: `dp[i] = f(dp[i-1], dp[i-2], ...)`
- Prefix sum: O(1) range queries after O(n) preprocessing

**🔄 Week 1 Review**: Solve all 7 problems again without hints

---

## 🗓️ Week 2-3: Arrays, Strings & Hashing

### Week 2: Hash Maps & Two Pointers

**📖 Theory (4 hours)**
- Hash table internals: hash function, collision resolution
- Hash map vs hash set use cases
- Two pointers: same direction vs opposite direction
- In-place array manipulation techniques
- Time-space tradeoffs

**💻 Day 8-10: Hash Map Patterns (5 hours)**
1. 🔑 **Two Sum** (LC 1) - Complement search, O(n)
2. 🎯 **Contains Duplicate** (LC 217) - Existence check
3. ⭐ **Valid Anagram** (LC 242) - Frequency counting
4. 🔑 **Group Anagrams** (LC 49) - Sorted string as key

**💻 Day 11-14: Two Pointers (6 hours)**
5. 🔑 **Valid Palindrome** (LC 125) - Opposite direction
6. ⭐ **Merge Sorted Array** (LC 88) - Backward iteration
7. 🎯 **Move Zeroes** (LC 283) - Slow/fast pointers
8. 🔑 **Best Time to Buy Stock** (LC 121) - Track min
9. ⭐ **Majority Element** (LC 169) - Boyer-Moore voting
10. 🎯 **Missing Number** (LC 268) - Math/XOR

**📝 Key Patterns**
- Hash map for O(1) lookups: complement, frequency
- Two pointers reduce O(n²) to O(n)
- Same direction: slow/fast for in-place modifications
- Opposite direction: palindrome, pair sum problems

---

### Week 3: Sliding Window & Advanced Arrays

**📖 Theory (4 hours)**
- Sliding window technique: variable vs fixed size
- Window expansion and contraction rules
- Prefix/suffix product patterns
- Heap for top-K problems

**💻 Day 15-18: Variable Sliding Window (6 hours)**
11. 🔑 **Longest Substring Without Repeating** (LC 3) - Expand/contract
12. 💡 **Longest Repeating Character Replacement** (LC 424) - len - maxFreq ≤ k
13. 🔑 **Container With Most Water** (LC 11) - Greedy two pointers

**💻 Day 19-21: Advanced Array Techniques (5 hours)**
14. 🔑 **3Sum** (LC 15) - Fix one + two pointers
15. ⭐ **Product of Array Except Self** (LC 238) - Prefix/suffix products
16. ⭐ **Top K Frequent Elements** (LC 347) - Frequency + min heap
17. 💡 **Subarray Sum Equals K** (LC 560) - Prefix sum + hash map
18. 🎯 **Rotate Array** (LC 189) - Three reverses

**📝 Key Patterns**
- Variable window: while (invalid) contract left
- Fixed window: process each k-size window
- Prefix sum + map: count subarrays with property
- Three reverses trick for rotation

**🔄 Week 2-3 Review**: Re-solve all 🔑 ANCHOR problems

---

## 🗓️ Week 4: Linked Lists, Stacks & Queues

### Day 22-24: Linked List Fundamentals

**📖 Theory (3 hours)**
- Singly vs doubly linked lists
- Dummy node pattern for edge cases
- Fast/slow pointer (Floyd's algorithm)
- Reverse linked list technique
- In-place vs extra space

**💻 Problems (6 hours)**
1. 🔑 **Reverse Linked List** (LC 206) - Three pointers
2. 🔑 **Merge Two Sorted Lists** (LC 21) - Dummy node
3. 🔑 **Linked List Cycle** (LC 141) - Fast/slow detection
4. 💡 **Linked List Cycle II** (LC 142) - Find entry point
5. 🎯 **Middle of Linked List** (LC 876) - Fast 2x, slow 1x
6. ⭐ **Palindrome Linked List** (LC 234) - Find middle + reverse

**📝 Key Patterns**
- Dummy node simplifies head operations
- Fast/slow pointer: cycle detection, middle finding
- Reverse: prev, curr, next = curr, next, next.next

---

### Day 25-28: Stacks & Advanced Linked Lists

**📖 Theory (3 hours)**
- Stack LIFO property and applications
- Monotonic stack for next greater/smaller
- Stack in function calls and recursion
- Two-pointer techniques on linked lists

**💻 Stack Problems (4 hours)**
7. 🔑 **Valid Parentheses** (LC 20) - Matching pairs
8. 🔑 **Daily Temperatures** (LC 739) - Monotonic stack
9. ⭐ **Min Stack** (LC 155) - Auxiliary min stack

**💻 Advanced Linked Lists (4 hours)**
10. ⭐ **Remove Nth Node From End** (LC 19) - N-ahead pointer
11. 💡 **Reorder List** (LC 143) - Middle + reverse + merge
12. ⭐ **Add Two Numbers** (LC 2) - Digit-by-digit carry

**📝 Key Patterns**
- Monotonic stack: while stack[-1] < curr, pop
- Valid parentheses: stack for opening, pop on closing
- N-ahead pointer: create gap of N nodes

**🔄 Week 4 Review**: Focus on pointer manipulation patterns

---

## 🗓️ Week 5-6: Trees & Binary Search Trees

### Week 5: Basic Tree Traversal & Recursion

**📖 Theory (4 hours)**
- Binary tree structure and properties
- Recursion on trees: base case + recursive case
- DFS: preorder, inorder, postorder
- BFS: level-order traversal with queue
- Top-down vs bottom-up recursion
- Tree height and depth concepts

**💻 Day 29-32: Basic Recursion (6 hours)**
1. 🔑 **Maximum Depth** (LC 104) - Bottom-up: 1 + max(left, right)
2. 🔑 **Invert Binary Tree** (LC 226) - Swap children
3. 🎯 **Same Tree** (LC 100) - Structural equality
4. ⭐ **Symmetric Tree** (LC 101) - Mirror checking
5. 🔑 **Diameter of Binary Tree** (LC 543) - leftH + rightH
6. 🎯 **Balanced Binary Tree** (LC 110) - |left - right| ≤ 1
7. 🎯 **Path Sum** (LC 112) - Top-down with target

**💻 Day 33-35: BFS & Level-Order (4 hours)**
8. 🔑 **Level Order Traversal** (LC 102) - Queue with level separation
9. ⭐ **Binary Tree Right Side View** (LC 199) - Last of each level

**📝 Key Patterns**
- Bottom-up: return info to parent (max depth, diameter)
- Top-down: pass info to children (path sum, validate)
- BFS template: queue, while queue not empty

---

### Week 6: BST Properties & Advanced Trees

**📖 Theory (4 hours)**
- Binary Search Tree property: left < node < right
- BST operations: search, insert, delete
- Inorder traversal gives sorted sequence
- LCA (Lowest Common Ancestor) techniques
- Tree construction from traversals

**💻 Day 36-39: BST Patterns (6 hours)**
10. 🔑 **Validate BST** (LC 98) - Range validation
11. 🎯 **LCA of BST** (LC 235) - Use BST property
12. 🔑 **LCA of Binary Tree** (LC 236) - Return node if found
13. ⭐ **Kth Smallest in BST** (LC 230) - Inorder to k
14. 🎯 **Convert Sorted Array to BST** (LC 108) - Middle as root

**💻 Day 40-42: Hard Trees (5 hours)**
15. 💡 **Binary Tree Max Path Sum** (LC 124) - node + max(0,left) + max(0,right)
16. 💡 **Serialize and Deserialize** (LC 297) - Preorder with nulls

**📝 Key Patterns**
- BST validation: pass valid range (min, max) down
- LCA: return node when found, propagate up
- Inorder on BST = sorted array
- Tree construction: identify root, split, recurse

**🔄 Week 5-6 Review**: Master tree recursion patterns

---

## 🗓️ Week 7-8: Graph Theory & Traversal

### Week 7: DFS, BFS & Grid Problems

**📖 Theory (4 hours)**
- Graph representations: adjacency list vs matrix
- DFS vs BFS: when to use which
- Connected components counting
- Grid as implicit graph: 4-directional, 8-directional
- Visited tracking: set, modify grid, separate matrix
- Multi-source BFS

**💻 Day 43-46: Grid DFS/BFS (6 hours)**
1. 🔑 **Flood Fill** (LC 733) - Basic DFS/BFS
2. 🔑 **Number of Islands** (LC 200) - Component counting
3. 🎯 **Max Area of Island** (LC 695) - Return area
4. 🔑 **Rotting Oranges** (LC 994) - Multi-source BFS
5. ⭐ **Surrounded Regions** (LC 130) - Border DFS
6. 💡 **Pacific Atlantic Water Flow** (LC 417) - Two DFS

**📝 Key Patterns**
- Grid DFS: mark visited, recurse 4 directions
- Multi-source BFS: add all sources to queue initially
- Border search: start from edges, mark safe regions

---

### Week 8: Graph Structures & Topological Sort

**📖 Theory (4 hours)**
- Graph cloning with hash map
- Cycle detection in directed/undirected graphs
- Topological sort: Kahn's algorithm (BFS-based)
- Prerequisites and dependency resolution
- DAG (Directed Acyclic Graph) properties

**💻 Day 50-53: Graph Algorithms (6 hours)**
7. ⭐ **Clone Graph** (LC 133) - Hash map old→new
8. 🎯 **Number of Provinces** (LC 547) - Adjacency matrix DFS
9. 🔑 **Course Schedule** (LC 207) - Topological sort, cycle detection
10. ⭐ **Course Schedule II** (LC 210) - Return order
11. 💡 **Graph Valid Tree** (LC 261) - n-1 edges, no cycles

**📝 Key Patterns**
- Clone: hash map to track old→new mapping
- Topological sort: indegree array, queue for 0 indegree
- Tree conditions: n-1 edges AND no cycles AND connected

**🔄 Week 7-8 Review**: DFS vs BFS decision making

---

## 🗓️ Week 9: Binary Search & Divide-Conquer

### Day 57-60: Binary Search Mastery

**📖 Theory (4 hours)**
- Binary search templates: finding element vs boundary
- Search space halving principle
- Rotated array handling
- Binary search on answer space
- 2D matrix as 1D array

**💻 Problems (7 hours)**
1. 🔑 **Search in Rotated Array** (LC 33) - Check which half sorted
2. 🎯 **Find Minimum in Rotated** (LC 153) - Compare mid with right
3. 🔑 **Find First and Last Position** (LC 34) - Two BS: left, right
4. 🔑 **Koko Eating Bananas** (LC 875) - BS on speed k
5. 🎯 **Search 2D Matrix** (LC 74) - Treat as 1D
6. ⭐ **Find Peak Element** (LC 162) - Follow gradient
7. 💡 **Capacity to Ship Packages** (LC 1011) - BS on capacity

**📝 Key Patterns**
- Standard BS: find exact element
- Left boundary: while left < right, return left
- Right boundary: while left < right, return right
- BS on answer: binary search on possible values, check feasibility

---

### Day 61-63: Intervals & Sorting

**📖 Theory (3 hours)**
- Interval problems: sort by start vs end
- Merge overlapping intervals
- Greedy interval selection
- Two pointers for sorted arrays

**💻 Problems (5 hours)**
8. 🔑 **Merge Intervals** (LC 56) - Sort by start, merge overlaps
9. ⭐ **Insert Interval** (LC 57) - Three phases
10. 💡 **Non-overlapping Intervals** (LC 435) - Sort by end, greedy
11. 🎯 **Meeting Rooms** (LC 252) - Check overlaps
12. ⭐ **Sort Colors** (LC 75) - Dutch flag partition

**📝 Key Patterns**
- Sort by start: merge overlapping
- Sort by end: greedy selection
- Two pointers: three-way partition

**🔄 Week 9 Review**: Master binary search template

---

## 🗓️ Week 10: Greedy Algorithms

### Day 64-68: Greedy Fundamentals

**📖 Theory (3 hours)**
- Greedy choice property
- Exchange argument for correctness
- Greedy vs Dynamic Programming
- Kadane's algorithm deep dive
- Jump game strategy

**💻 Problems (8 hours)**
1. 🔑 **Maximum Subarray** (LC 53) - Kadane's algorithm
2. 🔑 **Jump Game** (LC 55) - Track furthest reachable
3. 💡 **Jump Game II** (LC 45) - Track current/next end
4. ⭐ **Gas Station** (LC 134) - Total surplus + start point
5. ⭐ **Partition Labels** (LC 763) - Last occurrence tracking
6. 🎯 **Best Time to Buy/Sell II** (LC 122) - Collect all gains
7. 🎯 **Assign Cookies** (LC 455) - Greedy assignment
8. 💡 **Min Arrows Burst Balloons** (LC 452) - Interval greedy

**📝 Key Patterns**
- Kadane's: maxEndingHere = max(num, maxEndingHere + num)
- Jump game: update furthest reachable, check if reachable
- Interval greedy: sort by end time, shoot at end

**🔄 Week 10 Review**: Prove greedy correctness

---

## 🗓️ Week 11-13: Dynamic Programming Mastery

### Week 11: 1D DP Foundation

**📖 Theory (5 hours)**
- DP characteristics: optimal substructure + overlapping subproblems
- State definition: what does dp[i] represent?
- Recurrence relation derivation
- Base cases identification
- Top-down (memoization) vs bottom-up (tabulation)
- Space optimization techniques

**💻 Day 71-74: Basic 1D DP (6 hours)**
1. 🔑 **Climbing Stairs** (LC 70) - Fibonacci pattern
2. 🎯 **Min Cost Climbing Stairs** (LC 746) - Cost + min(dp[i-1], dp[i-2])
3. 🔑 **House Robber** (LC 198) - max(rob + dp[i-2], dp[i-1])
4. ⭐ **House Robber II** (LC 213) - Two sub-problems

**💻 Day 75-77: Unbounded Knapsack (5 hours)**
5. 🔑 **Coin Change** (LC 322) - min(dp[i-coin] + 1)
6. ⭐ **Decode Ways** (LC 91) - Single + double digit
7. 💡 **Maximum Product Subarray** (LC 152) - Track max and min
8. 🔑 **Word Break** (LC 139) - Can break s[0:i]

**📝 Key Patterns**
- State: dp[i] = optimal for problem size i
- Recurrence: try all choices, take best
- Space optimization: rolling array technique

---

### Week 12: 2D DP & Sequence Problems

**📖 Theory (4 hours)**
- 2D DP tables: grid vs sequence
- State definition for two variables
- Grid DP: path counting, min/max path
- Sequence DP: string matching, LCS, edit distance

**💻 Day 78-81: Grid DP (5 hours)**
9. 🔑 **Unique Paths** (LC 62) - dp[i][j] = dp[i-1][j] + dp[i][j-1]
10. 🎯 **Unique Paths II** (LC 63) - Obstacles handling
11. ⭐ **Minimum Path Sum** (LC 64) - grid + min(top, left)

**💻 Day 82-84: Sequence DP (6 hours)**
12. 🔑 **Longest Common Subsequence** (LC 1143) - If match: dp[i-1][j-1]+1
13. 🔑 **Edit Distance** (LC 72) - Insert, delete, replace
14. 🔑 **Longest Increasing Subsequence** (LC 300) - O(n²) DP or O(n log n) BS
15. ⭐ **Longest Palindromic Substring** (LC 5) - Expand around center
16. 🎯 **Palindromic Substrings** (LC 647) - Count palindromes

**📝 Key Patterns**
- Grid DP: dp[i][j] depends on top, left, top-left
- LCS: if match dp[i-1][j-1]+1, else max(dp[i-1][j], dp[i][j-1])
- Edit distance: min(insert, delete, replace)

---

### Week 13: Advanced DP (Knapsack & State Machine)

**📖 Theory (4 hours)**
- 0/1 Knapsack vs Unbounded Knapsack
- Subset sum variations
- State machine DP for stock problems
- Space optimization: 1D rolling array

**💻 Day 85-88: Knapsack Patterns (6 hours)**
17. 🔑 **Partition Equal Subset Sum** (LC 416) - Target = sum/2
18. ⭐ **Target Sum** (LC 494) - Transform to subset sum
19. 💡 **Combination Sum IV** (LC 377) - Order matters
20. 💡 **Best Time Stock with Cooldown** (LC 309) - State machine

**💻 Day 89-91: Hard DP (Optional, 5 hours)**
21. 💡 **Regular Expression Matching** (LC 10) - 2D DP
22. 💡 **Wildcard Matching** (LC 44) - Similar to regex
23. 💡 **Burst Balloons** (LC 312) - Interval DP

**📝 Key Patterns**
- 0/1 Knapsack: each item used once
- Unbounded: each item used unlimited times
- State machine: define states, transitions
- Interval DP: subproblems on intervals

**🔄 Week 11-13 Review**: Reconstruct DP tables from scratch

---

## 🗓️ Week 14: Backtracking & Recursion

### Day 92-96: Backtracking Patterns

**📖 Theory (4 hours)**
- Backtracking vs brute force
- Decision tree visualization
- Pruning strategies
- Combinations vs permutations
- Duplicate handling with sorting

**💻 Combinations (4 hours)**
1. 🔑 **Subsets** (LC 78) - Include/exclude tree
2. ⭐ **Subsets II** (LC 90) - Skip duplicates
3. 🎯 **Combinations** (LC 77) - Fixed size k
4. 🔑 **Combination Sum** (LC 39) - Unlimited use
5. ⭐ **Combination Sum II** (LC 40) - Limited use

**💻 Permutations & Grid (5 hours)**
6. 🔑 **Permutations** (LC 46) - Swap technique
7. 💡 **Permutations II** (LC 47) - Frequency map
8. 🎯 **Letter Combinations** (LC 17) - Digit mapping
9. ⭐ **Generate Parentheses** (LC 22) - Track open/close
10. 🔑 **Word Search** (LC 79) - Grid backtracking

**📝 Key Patterns**
- Combinations: for loop + start index
- Permutations: swap or used array
- Pruning: sort + skip duplicates
- Grid: mark visited, unmark on backtrack

**🔄 Week 14 Review**: Identify pruning opportunities

---

## 🗓️ Week 15: Heaps & Advanced Data Structures

### Day 99-103: Heap Fundamentals

**📖 Theory (4 hours)**
- Heap properties: min heap vs max heap
- Heapify operations: bubble up, bubble down
- Top-K pattern with min/max heap
- Two heaps for median
- K-way merge with heap

**💻 Heap Problems (6 hours)**
1. 🔑 **Kth Largest Element** (LC 215) - Min heap of size k
2. 🔑 **Top K Frequent Elements** (LC 347) - Frequency + heap
3. 🎯 **K Closest Points** (LC 973) - Max heap by distance
4. 🔑 **Find Median from Stream** (LC 295) - Two heaps
5. ⭐ **Merge k Sorted Lists** (LC 23) - K-way merge
6. 🎯 **Last Stone Weight** (LC 1046) - Simple max heap

**📝 Key Patterns**
- Top K smallest: max heap of size k
- Top K largest: min heap of size k
- Median: max heap (left) + min heap (right)
- K-way merge: heap for k elements

---

### Day 104-105: Union-Find & Trie

**📖 Theory (3 hours)**
- Union-Find: union by rank, path compression
- Connected components counting
- Trie structure and operations
- Prefix matching applications

**💻 Problems (4 hours)**
7. 🔑 **Number of Connected Components** (LC 323) - Union-Find
8. ⭐ **Graph Valid Tree** (LC 261) - Union-Find variant
9. 🔑 **Implement Trie** (LC 208) - Insert, search, startsWith
10. 💡 **Add Search Words** (LC 211) - Trie + wildcard

**📝 Key Patterns**
- Union-Find: parent array, find with compression
- Trie: children map, isEnd flag
- Prefix search: traverse trie

**🔄 Week 15 Review**: Implement heap/trie from scratch

---

## 🗓️ Week 16: Advanced Graph Algorithms

### Day 106-110: Shortest Path & MST

**📖 Theory (5 hours)**
- Dijkstra's algorithm: single-source shortest path
- Priority queue implementation
- Minimum Spanning Tree: Prim's, Kruskal's
- Tarjan's algorithm for bridges
- BFS for unweighted graphs

**💻 Problems (7 hours)**
1. 🔑 **Network Delay Time** (LC 743) - Dijkstra's
2. ⭐ **Cheapest Flights K Stops** (LC 787) - BFS/DP variant
3. 🎯 **Path with Max Probability** (LC 1514) - Max heap Dijkstra
4. 🔑 **Min Cost Connect Points** (LC 1584) - MST (Prim/Kruskal)
5. 💡 **Critical Connections** (LC 1192) - Tarjan's bridges
6. 💡 **Swim in Rising Water** (LC 778) - Binary search or Dijkstra

**📝 Key Patterns**
- Dijkstra: priority queue, relax neighbors
- MST: connect all vertices, minimum total weight
- Bridges: edge whose removal disconnects graph

**🔄 Week 16 Review**: Dijkstra and MST templates

---

## 🗓️ Week 17-18: Mixed Practice & Design Problems

### Week 17: System Design & Data Structure Design

**📖 Theory (4 hours)**
- LRU Cache: DLL + hash map
- Design principles: API clarity, time complexity
- Trade-offs: time vs space vs simplicity
- Thread safety considerations (basic)

**💻 Day 113-117: Design Problems (8 hours)**
1. 🔑 **LRU Cache** (LC 146) - DLL + hash map
2. 🔑 **Trapping Rain Water** (LC 42) - Two pointers/stack
3. ⭐ **Longest Consecutive Sequence** (LC 128) - Hash set
4. ⭐ **Basic Calculator II** (LC 227) - Stack for operators
5. 💡 **Sliding Window Maximum** (LC 239) - Monotonic deque
6. 💡 **Largest Rectangle Histogram** (LC 84) - Monotonic stack
7. 🎯 **Valid Sudoku** (LC 36) - Three hash sets

**📝 Key Patterns**
- LRU: move to front on access, evict from back
- Monotonic deque: maintain decreasing order
- Design: clear API, optimal operations

---

### Week 18: Advanced Mixed Practice

**💻 Day 118-122: Interview Simulation (10 hours)**
8. 🎯 **Rotate Image** (LC 48) - Transpose + reverse
9. 🎯 **Spiral Matrix** (LC 54) - Four boundaries
10. ⭐ **Insert Delete GetRandom O(1)** (LC 380) - Array + map
11. ⭐ **Time Based Key-Value** (LC 981) - Map + binary search
12. ⭐ **Encode and Decode Strings** (LC 271) - Length prefix

**Additional Practice (Optional)**
13. 💡 **LFU Cache** (LC 460)
14. ⭐ **Design Twitter** (LC 355)
15. 💡 **Maximum Frequency Stack** (LC 895)

**📝 Focus Areas**
- Clean code: variable naming, edge cases
- Time/space analysis explanation
- Multiple solutions: brute force → optimal

**🔄 Week 17-18 Review**: Mock interviews, timed practice

---

## 🗓️ Week 19: Final Review & Interview Prep

### Day 125-129: Pattern Consolidation

**📖 Review All Patterns (8 hours)**
- Arrays: two pointers, sliding window, prefix sum
- Linked Lists: fast/slow, dummy node, reversal
- Trees: DFS recursion, BFS level-order, BST properties
- Graphs: DFS/BFS, topological sort, shortest path
- DP: 1D, 2D, knapsack, state machine
- Backtracking: combinations, permutations, pruning
- Heaps: top-K, median, k-way merge
- Binary Search: element, boundary, answer space

**💻 Anchor Problem Sprint (8 hours)**
- Re-solve all 47 🔑 ANCHOR problems without hints
- Time yourself: Easy <20 min, Medium <35 min
- Focus on pattern recognition speed

---

### Day 130-133: Mock Interviews & Final Prep

**Day 130-131: Mock Technical Interviews (6 hours)**
- Simulate real interview conditions
- Practice explaining approach before coding
- Discuss time/space complexity
- Handle follow-up questions

**Day 132: Weak Area Focus (4 hours)**
- Identify 3-5 weak patterns from practice
- Re-study theory and re-solve problems
- Create cheat sheet for each pattern

**Day 133: Interview Readiness Check (3 hours)**
- Can recognize pattern in <5 minutes?
- Can explain approach clearly?
- Can code without syntax errors?
- Can analyze complexity correctly?
- Ready for behavioral questions?

---

## 📊 Progress Tracking

### Weekly Checkpoints
- [ ] Week 1: Complexity analysis mastery
- [ ] Week 2-3: Array/hash map patterns confident
- [ ] Week 4: Linked list pointer manipulation smooth
- [ ] Week 5-6: Tree recursion patterns automatic
- [ ] Week 7-8: Graph traversal decision making quick
- [ ] Week 9: Binary search template memorized
- [ ] Week 10: Greedy strategy recognition sharp
- [ ] Week 11-13: DP state definition fluent
- [ ] Week 14: Backtracking pruning effective
- [ ] Week 15: Heap/trie implementation solid
- [ ] Week 16: Shortest path algorithms ready
- [ ] Week 17-18: Design problems confident
- [ ] Week 19: Mock interview success rate >70%

### Skill Milestones
- [ ] 50 problems solved (Foundation)
- [ ] 100 problems solved (Intermediate)
- [ ] 165 problems solved (Advanced)
- [ ] All 47 anchors mastered (Expert)
- [ ] Can solve Easy in <20 min
- [ ] Can solve Medium in <35 min
- [ ] Pattern recognition in <5 min
- [ ] Can explain multiple approaches
- [ ] Zero-bug first submission rate >60%

---

## 🎯 Problem Priority Legend

- **🔑 ANCHOR (47)**: Master FIRST - Foundation of each pattern
- **⭐ HIGH VALUE (58)**: Common in interviews - Practice after anchors
- **💡 ADVANCED (32)**: Complex applications - Attempt when confident
- **🎯 PRACTICE (98)**: Variations for reinforcement

### Study Strategy by Level

**Beginner (0-3 months)**
- Focus: 🔑 ANCHOR + 🎯 PRACTICE
- Goal: 80-100 problems
- Review: 3-4 times per problem

**Intermediate (3-6 months)**
- Focus: 🔑 ANCHOR + ⭐ HIGH VALUE
- Goal: 120-150 problems
- Review: 2-3 times per problem

**Advanced (6+ months / Interview prep)**
- Focus: All priorities, emphasize ⭐ + 💡
- Goal: 165-200 problems
- Review: 1-2 times per problem

---

## 📚 Learning Resources

### Theory Supplements
- **Visualization**: VisuAlgo, Algorithm Visualizer
- **Video**: Abdul Bari (Algorithms), MIT OpenCourseWare
- **Books**: "Cracking the Coding Interview", "Elements of Programming Interviews"
- **Practice**: LeetCode, HackerRank, CodeSignal

### Pattern Cheat Sheets
Create concise notes for:
- When to use each pattern
- Template code structure
- Common variations
- Time/space complexity
- Edge cases to check

---

## 🔄 Spaced Repetition Schedule

**First Solve**: Day 0 (learn + implement)
**Review 1**: Day 3 (3 days later)
**Review 2**: Day 10 (7 days after review 1)
**Review 3**: Day 24 (14 days after review 2)
**Mastered**: After 3 successful reviews

### Daily Routine (2 hours)
- **[30 min]** Theory review or pattern study
- **[15 min]** Problem analysis (pattern, approach, complexity)
- **[50 min]** Implementation and testing
- **[15 min]** Review optimal solution, update notes
- **[10 min]** Log progress, schedule next review

---

## 💡 Tips for Success

### During Problem Solving
1. **Read Carefully**: Understand constraints, edge cases
2. **Identify Pattern**: Which category? Similar to what?
3. **Brute Force First**: Get a working solution
4. **Optimize**: Can we do better? Different data structure?
5. **Dry Run**: Test with example + edge cases
6. **Complexity Analysis**: Time and space before coding
7. **Clean Code**: Meaningful names, comments for clarity

### Interview Preparation
1. **Think Aloud**: Explain your reasoning continuously
2. **Ask Questions**: Clarify requirements, constraints
3. **Start Simple**: Brute force → Optimized approach
4. **Test Cases**: Example, edge, large input
5. **Handle Ambiguity**: Make reasonable assumptions
6. **Time Management**: 40 min problem, leave 5 for questions
7. **Stay Calm**: Stuck? Revisit approach, ask hints

### Common Pitfalls to Avoid
- ❌ Jumping to code without clear approach
- ❌ Not considering edge cases (empty, single element, duplicates)
- ❌ Forgetting to analyze time/space complexity
- ❌ Not testing with different inputs
- ❌ Overcomplicating solutions
- ❌ Ignoring constraints (can affect approach choice)
- ❌ Memorizing solutions instead of understanding patterns

---

## 🎓 Graduation Criteria

### Ready for Interviews When:
✅ Can solve Easy problems in <20 minutes  
✅ Can solve Medium problems in <35 minutes  
✅ Recognize pattern family in <5 minutes  
✅ Explain multiple approaches fluently  
✅ Analyze time/space complexity accurately  
✅ Handle edge cases systematically  
✅ Write bug-free code in first attempt (60%+ rate)  
✅ Complete 3+ mock interviews successfully  
✅ Solved 165+ problems across all patterns  
✅ Mastered all 47 🔑 ANCHOR problems  

---

## 🚀 Final Words

**Remember**: 
- **Consistency > Intensity**: 2 hours daily for 133 days > 20 hours once a week
- **Understanding > Memorization**: Learn patterns, not solutions
- **Quality > Quantity**: Master 165 problems deeply > solve 500 superficially
- **Review > New**: Spaced repetition is the secret weapon
- **Practice > Theory**: Theory guides, practice solidifies

**You've got this!** 💪 Stay consistent, trust the process, and track your progress. Every problem solved is a step closer to mastery.

---

*Last Updated: 2026 | Based on 235+ LeetCode problems | Optimized for FAANG interviews*