Senior / Lead DSA Interview — Unified Problem List
> Curated DSA curriculum for Senior Software Engineer / Lead Software Engineer interviews at product companies.
> Goal: Master patterns and problem-solving techniques rather than memorize individual solutions.
> Language: C# / .NET
> Target: Senior / Lead Software Engineer interviews
> Recommended approach: Learn difficult representative problems deeply, then use easier problems to reinforce the same pattern.
> 
Legend
Difficulty
 * 🟢 Easy
 * 🟡 Medium
 * 🔴 Hard
Priority
 * ⭐ Core — Must master
 * 🔥 High — Strongly recommended
 * 💡 Advanced — Learn after core patterns
 * ⚪ Reinforcement — Useful practice
Pattern Tags
Examples: #hashing, #two-pointers, #sliding-window, #prefix-sum, #monotonic-stack, #binary-search, #linked-list, #tree, #bst, #heap, #backtracking, #greedy, #dynamic-programming, #graph, #topological-sort, #union-find, #shortest-path, #mst
Phase 1 — Arrays, Hashing & Frequency
Focus: HashSet / Dictionary, Frequency maps, Complement lookup, Canonical representation, Array invariants
| # | Problem | LC | Difficulty | Priority | Pattern |
|---|---|---|---|---|---|
| 1 | Two Sum | 1 | 🟢 Easy | ⭐ Core | #hashing #dictionary |
| 2 | Contains Duplicate | 217 | 🟢 Easy | ⚪ Reinforcement | #hashing #hashset |
| 3 | Valid Anagram | 242 | 🟢 Easy | ⚪ Reinforcement | #hashing #frequency |
| 4 | Group Anagrams | 49 | 🟡 Medium | ⭐ Core | #hashing #frequency #canonical-form |
| 5 | Longest Consecutive Sequence | 128 | 🟡 Medium | ⭐ Core | #hashing #hashset #sequence |
| 6 | Majority Element | 169 | 🟢 Easy | 🔥 High | #hashing #boyer-moore |
| 7 | Product of Array Except Self | 238 | 🟡 Medium | ⭐ Core | #prefix-product #array |
Pattern Milestone
You should be able to recognize:
 * Need fast existence lookup ➔ HashSet
 * Need key → count/value ➔ Dictionary
 * Need grouping by characteristics ➔ Canonical representation + Dictionary
Phase 2 — Two Pointers
Focus: Sorted arrays, Opposing pointers, Same-direction pointers, Eliminating impossible candidates, Boundary reasoning
| # | Problem | LC | Difficulty | Priority | Pattern |
|---|---|---|---|---|---|
| 8 | Valid Palindrome | 125 | 🟢 Easy | ⚪ Reinforcement | #two-pointers #string |
| 9 | Two Sum II — Input Array Is Sorted | 167 | 🟡 Medium | ⭐ Core | #two-pointers #sorted-array |
| 10 | Container With Most Water | 11 | 🟡 Medium | ⭐ Core | #two-pointers #greedy #boundary |
| 11 | 3Sum | 15 | 🟡 Medium | ⭐ Core | #sorting #two-pointers #deduplication |
| 12 | 4Sum | 18 | 🟡 Medium | 🔥 High | #k-sum #recursion #two-pointers |
| 13 | Trapping Rain Water | 42 | 🔴 Hard | ⭐ Core | #two-pointers #prefix-max #boundary |
Pattern Milestone
Understand this deeply:
> "If the search space is ordered, can I eliminate a whole class of candidates by moving one boundary?"
> 
Phase 3 — Sliding Window
Focus: Fixed-size windows, Variable-size windows, Frequency tracking, Window invariants, Expand → validate → shrink
| # | Problem | LC | Difficulty | Priority | Pattern |
|---|---|---|---|---|---|
| 14 | Maximum Average Subarray I | 643 | 🟡 Medium | ⚪ Reinforcement | #sliding-window #fixed-window |
| 15 | Longest Substring Without Repeating Characters | 3 | 🟡 Medium | ⭐ Core | #sliding-window #hashset |
| 16 | Longest Repeating Character Replacement | 424 | 🟡 Medium | ⭐ Core | #sliding-window #frequency |
| 17 | Permutation in String | 567 | 🟡 Medium | 🔥 High | #sliding-window #frequency |
| 18 | Minimum Window Substring | 76 | 🔴 Hard | ⭐ Core | #sliding-window #frequency #two-pointers |
Pattern Milestone
Recognize:
 * Contiguous range + Need longest/shortest/valid range ➔ Sliding Window
Phase 4 — Prefix Sum & Subarray Patterns
Focus: Running sums, Prefix sums, Prefix sum + HashMap, Range queries, Maximum subarray
| # | Problem | LC | Difficulty | Priority | Pattern |
|---|---|---|---|---|---|
| 19 | Running Sum of 1d Array | 1480 | 🟢 Easy | ⚪ Reinforcement | #prefix-sum |
| 20 | Find Pivot Index | 724 | 🟢 Easy | 🔥 High | #prefix-sum #invariant |
| 21 | Subarray Sum Equals K | 560 | 🟡 Medium | ⭐ Core | #prefix-sum #hashing |
| 22 | Maximum Subarray | 53 | 🟡 Medium | ⭐ Core | #kadane #dynamic-programming |
| 23 | Range Sum Query — Immutable | 303 | 🟢 Easy | ⚪ Reinforcement | #prefix-sum #range-query |
Pattern Milestone
Understand:
 * prefix[j] - prefix[i] ➔ sum of subarray (i, j]
 * Need count/existence of subarrays with target sum ➔ Prefix Sum + HashMap
Phase 5 — Strings
Focus: Character manipulation, Palindromes, String compression, String pattern recognition
| # | Problem | LC | Difficulty | Priority | Pattern |
|---|---|---|---|---|---|
| 24 | Reverse String | 344 | 🟢 Easy | ⚪ Reinforcement | #two-pointers #string |
| 25 | Longest Common Prefix | 14 | 🟢 Easy | ⚪ Reinforcement | #string |
| 26 | String Compression | 443 | 🟡 Medium | 🔥 High | #two-pointers #string |
| 27 | Longest Palindromic Substring | 5 | 🟡 Medium | ⭐ Core | #string #expand-around-center #dp |
| 28 | Palindromic Substrings | 647 | 🟡 Medium | 🔥 High | #string #dp #expand-around-center |
Phase 6 — Linked Lists
Focus: Pointer manipulation, Fast/slow pointers, Reversal, Cycle detection, Merge, Recursive pointer transformations
| # | Problem | LC | Difficulty | Priority | Pattern |
|---|---|---|---|---|---|
| 29 | Reverse Linked List | 206 | 🟢 Easy | ⭐ Core | #linked-list #reversal |
| 30 | Merge Two Sorted Lists | 21 | 🟢 Easy | ⚪ Reinforcement | #linked-list #merge |
| 31 | Linked List Cycle | 141 | 🟢 Easy | ⭐ Core | #fast-slow #cycle-detection |
| 32 | Remove Nth Node From End of List | 19 | 🟡 Medium | ⭐ Core | #fast-slow #two-pointers |
| 33 | Reorder List | 143 | 🟡 Medium | ⭐ Core | #linked-list #fast-slow #reversal #merge |
| 34 | Merge K Sorted Lists | 23 | 🔴 Hard | ⭐ Core | #heap #divide-conquer #linked-list |
| 35 | Reverse Nodes in K-Group | 25 | 🔴 Hard | 🔥 High | #linked-list #reversal #recursion |
Pattern Milestone
Recognize:
 * Linked list + position from end ➔ Fast/slow pointers
 * Linked list + cycle ➔ Floyd's algorithm
 * Linked list + reverse sections ➔ Pointer reversal
Phase 7 — Stack & Monotonic Stack
Focus: Matching/open-close structures, Expression evaluation, Previous/next greater element, Monotonic structures
| # | Problem | LC | Difficulty | Priority | Pattern |
|---|---|---|---|---|---|
| 36 | Valid Parentheses | 20 | 🟢 Easy | ⭐ Core | #stack |
| 37 | Min Stack | 155 | 🟡 Medium | ⭐ Core | #stack #auxiliary-state |
| 38 | Evaluate Reverse Polish Notation | 150 | 🟡 Medium | 🔥 High | #stack #expression |
| 39 | Daily Temperatures | 739 | 🟡 Medium | ⭐ Core | #monotonic-stack |
| 40 | Largest Rectangle in Histogram | 84 | 🔴 Hard | ⭐ Core | #monotonic-stack #boundary |
Pattern Milestone
Understand:
 * Need nearest greater/smaller element ➔ Monotonic Stack
 * For each element: find how far it can extend ➔ Previous smaller + next smaller
Phase 8 — Binary Search
Focus: Classic binary search, Boundary search, Rotated arrays, Binary search on answer
| # | Problem | LC | Difficulty | Priority | Pattern |
|---|---|---|---|---|---|
| 41 | Binary Search | 704 | 🟢 Easy | ⭐ Core | #binary-search |
| 42 | Search Insert Position | 35 | 🟢 Easy | ⚪ Reinforcement | #binary-search #boundary |
| 43 | Find First and Last Position | 34 | 🟡 Medium | ⭐ Core | #binary-search #boundary |
| 44 | Search in Rotated Sorted Array | 33 | 🟡 Medium | ⭐ Core | #binary-search #rotated-array |
| 45 | Find Minimum in Rotated Sorted Array | 153 | 🟡 Medium | 🔥 High | #binary-search #rotated-array |
| 46 | Koko Eating Bananas | 875 | 🟡 Medium | ⭐ Core | #binary-search #answer-space |
| 47 | Search a 2D Matrix | 74 | 🟡 Medium | 🔥 High | #binary-search #matrix |
Pattern Milestone
Distinguish:
 * Search a sorted collection ➔ Binary Search
 * Search a monotonic answer space ➔ Binary Search on Answer
Phase 9 — Binary Trees
Focus: DFS, BFS, Tree recursion, Tree invariants, Bottom-up vs top-down reasoning
| # | Problem | LC | Difficulty | Priority | Pattern |
|---|---|---|---|---|---|
| 48 | Maximum Depth of Binary Tree | 104 | 🟢 Easy | ⭐ Core | #tree #dfs #recursion |
| 49 | Same Tree | 100 | 🟢 Easy | ⚪ Reinforcement | #tree #dfs |
| 50 | Invert Binary Tree | 226 | 🟢 Easy | ⚪ Reinforcement | #tree #recursion |
| 51 | Binary Tree Level Order Traversal | 102 | 🟡 Medium | ⭐ Core | #tree #bfs #queue |
| 52 | Diameter of Binary Tree | 543 | 🟢 Easy | ⭐ Core | #tree #dfs #bottom-up |
| 53 | Validate Binary Search Tree | 98 | 🟡 Medium | ⭐ Core | #bst #dfs #invariant |
| 54 | Lowest Common Ancestor of a Binary Tree | 236 | 🟡 Medium | ⭐ Core | #tree #dfs |
| 55 | Kth Smallest Element in a BST | 230 | 🟡 Medium | 🔥 High | #bst #inorder #stack |
Pattern Milestone
For every tree problem ask:
> "What information does my subtree return to its parent?"
> This question unlocks many tree DP problems.
> 
Phase 10 — Heap / Priority Queue
Focus: Top K, Streaming data, Min heap / max heap, Maintaining partial ordering
| # | Problem | LC | Difficulty | Priority | Pattern |
|---|---|---|---|---|---|
| 56 | Kth Largest Element in an Array | 215 | 🟡 Medium | ⭐ Core | #heap #quickselect #top-k |
| 57 | Top K Frequent Elements | 347 | 🟡 Medium | ⭐ Core | #heap #hashing #top-k |
| 58 | K Closest Points to Origin | 973 | 🟡 Medium | 🔥 High | #heap #top-k |
| 59 | Find Median from Data Stream | 295 | 🔴 Hard | ⭐ Core | #two-heaps #streaming |
Pattern Milestone
Recognize:
 * Need top/bottom K ➔ Heap / Quickselect
 * Need median dynamically ➔ Two Heaps
Phase 11 — Intervals
Focus: Sorting by start/end, Merging, Overlap detection, Scheduling
| # | Problem | LC | Difficulty | Priority | Pattern |
|---|---|---|---|---|---|
| 60 | Merge Intervals | 56 | 🟡 Medium | ⭐ Core | #intervals #sorting |
| 61 | Insert Interval | 57 | 🟡 Medium | 🔥 High | #intervals #merge |
| 62 | Non-overlapping Intervals | 435 | 🟡 Medium | 🔥 High | #intervals #greedy |
| 63 | Meeting Rooms II | 253 | 🟡 Medium | ⭐ Core | #intervals #heap #sweep-line |
Pattern Milestone
Usually:
Intervals ➔ Sort by start ➔ Reason about overlap
But meeting/scheduling problems may additionally use: Heap / Sweep Line
Phase 12 — Graph Traversal
Focus: Graph representation, DFS, BFS, Visited state, Connected components, Grid as graph
| # | Problem | LC | Difficulty | Priority | Pattern |
|---|---|---|---|---|---|
| 64 | Number of Islands | 200 | 🟡 Medium | ⭐ Core | #graph #dfs #bfs #grid |
| 65 | Clone Graph | 133 | 🟡 Medium | 🔥 High | #graph #dfs #bfs #hashmap |
| 66 | Number of Provinces | 547 | 🟡 Medium | 🔥 High | #graph #dfs #union-find |
| 67 | Number of Connected Components | 323 | 🟡 Medium | ⭐ Core | #graph #dfs #union-find |
| 68 | Course Schedule | 207 | 🟡 Medium | ⭐ Core | #graph #topological-sort #cycle-detection |
| 69 | Course Schedule II | 210 | 🟡 Medium | 🔥 High | #graph #topological-sort |
Phase 13 — Graph Algorithms
Focus: Shortest paths, Weighted graphs, MST, Union-Find, Advanced graph reasoning
| # | Problem | LC | Difficulty | Priority | Pattern |
|---|---|---|---|---|---|
| 70 | Network Delay Time | 743 | 🟡 Medium | ⭐ Core | #graph #dijkstra #shortest-path |
| 71 | Redundant Connection | 684 | 🟡 Medium | ⭐ Core | #union-find #cycle-detection |
| 72 | Cheapest Flights Within K Stops | 787 | 🟡 Medium | 🔥 High | #graph #shortest-path #dp |
| 73 | Min Cost to Connect All Points | 1584 | 🟡 Medium | ⭐ Core | #mst #prim #kruskal #union-find |
| 74 | Critical Connections in a Network | 1192 | 🔴 Hard | 💡 Advanced | #graph #tarjan #bridges |
Phase 14 — Greedy
Focus: Local optimal decisions, Proving greedy choices, Reachability, Scheduling, Exchange arguments
| # | Problem | LC | Difficulty | Priority | Pattern |
|---|---|---|---|---|---|
| 75 | Jump Game | 55 | 🟡 Medium | ⭐ Core | #greedy #reachability |
| 76 | Gas Station | 134 | 🟡 Medium | ⭐ Core | #greedy #invariant |
| 77 | Partition Labels | 763 | 🟡 Medium | 🔥 High | #greedy #interval |
| 78 | Non-overlapping Intervals | 435 | 🟡 Medium | 🔥 High | #greedy #intervals |
(Note: "Non-overlapping Intervals" overlaps with Phase 11 intentionally to demonstrate the greedy pattern from a different perspective.)
Phase 15 — Dynamic Programming Fundamentals
Focus: State definition, Recurrence, Base cases, Memoization, Tabulation, Space optimization
| # | Problem | LC | Difficulty | Priority | Pattern |
|---|---|---|---|---|---|
| 79 | Climbing Stairs | 70 | 🟢 Easy | ⭐ Core | #dp #1d-dp |
| 80 | House Robber | 198 | 🟡 Medium | ⭐ Core | #dp #1d-dp |
| 81 | Coin Change | 322 | 🟡 Medium | ⭐ Core | #dp #unbounded-knapsack |
| 82 | Word Break | 139 | 🟡 Medium | ⭐ Core | #dp #string #hashset |
| 83 | Partition Equal Subset Sum | 416 | 🟡 Medium | 🔥 High | #dp #0-1-knapsack |
| 84 | Longest Increasing Subsequence | 300 | 🟡 Medium | ⭐ Core | #dp #binary-search |
| 85 | Maximum Product Subarray | 152 | 🟡 Medium | 🔥 High | #dp #state-machine |
Phase 16 — Advanced Dynamic Programming
Focus: 2D DP, String DP, Sequence alignment, State transitions, Optimization
| # | Problem | LC | Difficulty | Priority | Pattern |
|---|---|---|---|---|---|
| 86 | Unique Paths | 62 | 🟡 Medium | ⚪ Reinforcement | #dp #2d-dp |
| 87 | Minimum Path Sum | 64 | 🟡 Medium | 🔥 High | #dp #2d-dp |
| 88 | Longest Common Subsequence | 1143 | 🟡 Medium | ⭐ Core | #dp #string #2d-dp |
| 89 | Edit Distance | 72 | 🔴 Hard | ⭐ Core | #dp #string #2d-dp |
| 90 | Decode Ways | 91 | 🟡 Medium | 🔥 High | #dp #1d-dp |
| 91 | Target Sum | 494 | 🟡 Medium | 🔥 High | #dp #subset-sum |
| 92 | Best Time to Buy and Sell Stock with Cooldown | 309 | 🟡 Medium | 💡 Advanced | #dp #state-machine |
Phase 17 — Backtracking
Focus: State-space search, Decision trees, Choose → explore → undo, Pruning, Combinatorial generation
| # | Problem | LC | Difficulty | Priority | Pattern |
|---|---|---|---|---|---|
| 93 | Subsets | 78 | 🟡 Medium | ⭐ Core | #backtracking #subsets |
| 94 | Permutations | 46 | 🟡 Medium | ⭐ Core | #backtracking #permutations |
| 95 | Combination Sum | 39 | 🟡 Medium | ⭐ Core | #backtracking #pruning |
| 96 | Letter Combinations of a Phone Number | 17 | 🟡 Medium | 🔥 High | #backtracking #combinations |
| 97 | Word Search | 79 | 🟡 Medium | ⭐ Core | #backtracking #grid #dfs |
| 98 | N-Queens | 51 | 🔴 Hard | 💡 Advanced | #backtracking #constraint-satisfaction |
Phase 18 — Trie / String Search
Focus: Prefix trees, Prefix queries, Dictionary search, DFS + Trie
| # | Problem | LC | Difficulty | Priority | Pattern |
|---|---|---|---|---|---|
| 99 | Implement Trie | 208 | 🟡 Medium | ⭐ Core | #trie #string |
| 100 | Design Add and Search Words Data Structure | 211 | 🟡 Medium | 🔥 High | #trie #dfs #backtracking |
| 101 | Word Search II | 212 | 🔴 Hard | 💡 Advanced | #trie #dfs #backtracking #grid |
Phase 19 — Union-Find / Disjoint Set
Focus: Dynamic connectivity, Connected components, Cycle detection, Kruskal MST
| # | Problem | LC | Difficulty | Priority | Pattern |
|---|---|---|---|---|---|
| 102 | Number of Provinces | 547 | 🟡 Medium | 🔥 High | #union-find |
| 103 | Redundant Connection | 684 | 🟡 Medium | ⭐ Core | #union-find #cycle-detection |
| 104 | Accounts Merge | 721 | 🟡 Medium | 💡 Advanced | #union-find #graph #hashing |
| 105 | Min Cost to Connect All Points | 1584 | 🟡 Medium | ⭐ Core | #union-find #mst |
Phase 20 — Advanced / Senior-Level Patterns
These are not necessarily required for every interview, but they broaden algorithmic maturity.
| # | Problem | LC | Difficulty | Priority | Pattern |
|---|---|---|---|---|---|
| 106 | Sliding Window Maximum | 239 | 🔴 Hard | ⭐ Core | #monotonic-queue #sliding-window |
| 107 | Median of Two Sorted Arrays | 4 | 🔴 Hard | 💡 Advanced | #binary-search #partition |
| 108 | Largest Rectangle in Histogram | 84 | 🔴 Hard | ⭐ Core | #monotonic-stack |
| 109 | Trapping Rain Water | 42 | 🔴 Hard | ⭐ Core | #two-pointers #prefix-max |
| 110 | Word Ladder | 127 | 🔴 Hard | 🔥 High | #bfs #graph #shortest-path |
| 111 | Swim in Rising Water | 778 | 🔴 Hard | 💡 Advanced | #graph #binary-search #dijkstra #union-find |
| 112 | Longest Valid Parentheses | 32 | 🔴 Hard | 💡 Advanced | #stack #dp |
| 113 | Regular Expression Matching | 10 | 🔴 Hard | 💡 Advanced | #dp #recursion |
| 114 | Burst Balloons | 312 | 🔴 Hard | 💡 Advanced | #interval-dp #recursion |
| 115 | Serialize and Deserialize Binary Tree | 297 | 🔴 Hard | 🔥 High | #tree #dfs #bfs #serialization |
Pattern Coverage Matrix
The objective is not to memorize 115 solutions. The objective is to master these patterns:
| Pattern | Core Problems |
|---|---|
| Hashing | Two Sum, Group Anagrams, Longest Consecutive |
| Two Pointers | Container, 3Sum, 4Sum, Trapping Rain Water |
| Sliding Window | Longest Substring, Character Replacement, Minimum Window |
| Prefix Sum | Pivot Index, Subarray Sum K |
| Fast/Slow Pointer | Cycle, Remove Nth, Reorder List |
| Monotonic Stack | Daily Temperatures, Histogram |
| Binary Search | Search, Rotated Array, Koko |
| Binary Search on Answer | Koko |
| Tree DFS | Depth, Diameter, LCA, Validate BST |
| Tree BFS | Level Order |
| Heap / Top K | Kth Largest, Top K, K Closest |
| Two Heaps | Median Data Stream |
| Intervals | Merge, Insert, Meeting Rooms |
| Graph DFS/BFS | Islands, Clone Graph, Components |
| Topological Sort | Course Schedule |
| Union-Find | Redundant Connection, Provinces |
| Dijkstra | Network Delay |
| MST | Min Cost Connect Points |
| Greedy | Jump Game, Gas Station |
| 1D DP | House Robber, Coin Change |
| 2D DP | LCS, Edit Distance |
| Knapsack | Coin Change, Partition Equal Subset Sum |
| State Machine DP | Stock Cooldown |
| Backtracking | Subsets, Permutations, Combination Sum |
| Trie | Trie, Word Search II |
| Monotonic Queue | Sliding Window Maximum |
| Interval DP | Burst Balloons |
Recommended Learning Order
Do not simply solve #1 → #115. Use this progression:
                    DSA
                     │
       ┌─────────────┼─────────────┐
       │             │             │
    Arrays         Lists         Trees
       │             │             │
 Hashing         Pointers       DFS/BFS
       │             │             │
Two Pointers   Fast/Slow       BST
       │             │             │
Sliding Window     Stack        Heap
       │             │             │
Prefix Sum           │        Intervals
       └─────────────┼─────────────┘
                     │
                   Graphs
                     │
        ┌────────────┼────────────┐
        │            │            │
       BFS          DFS       Union-Find
        │            │            │
       DAG        Shortest       MST
                     │
                   DP
                     │
          ┌──────────┴──────────┐
          │                     │
       1D DP                  2D DP
          │                     │
      Knapsack             String DP
          │                     │
          └──────────┬──────────┘
                     │
                Backtracking
                     │
                   Trie

Hard-First Learning Strategy
For each major pattern, start with one difficult representative problem.
Example: Two Pointer
Trapping Rain Water 🔴
        ↓
Understand boundary invariant
        ↓
Container With Most Water 🟡
        ↓
3Sum 🟡
        ↓
4Sum 🟡
        ↓
Two Sum II 🟡

Example: Sliding Window
Minimum Window Substring 🔴
        ↓
Longest Repeating Character Replacement 🟡
        ↓
Longest Substring Without Repeating 🟡
        ↓
Fixed-size window problems

Example: Monotonic Stack
Largest Rectangle in Histogram 🔴
        ↓
Daily Temperatures 🟡
        ↓
Nearest greater/smaller problems

Example: Dynamic Programming
Don't begin with dozens of DP problems. Instead:
House Robber
        ↓
Coin Change
        ↓
Partition Equal Subset Sum
        ↓
LIS
        ↓
LCS
        ↓
Edit Distance
        ↓
State-machine DP

Problem-Solving Protocol
For every problem, follow this process.
Step 1 — Understand
Identify:
 * Input
 * Output
 * Constraints
 * Edge cases
Step 2 — Brute Force
First determine the obvious solution. Ask:
 * What would the naive solution be?
 * What is its complexity?
Step 3 — Find the Bottleneck
Ask:
 * What repeated work is happening?
 * Can I eliminate it?
 * Can I cache it?
 * Can I maintain it incrementally?
 * Can I exploit ordering?
Step 4 — Identify the Pattern
Ask: Hashing? Two pointers? Sliding window? Prefix sum? Stack? Binary search? Tree DFS/BFS? Heap? Graph? Greedy? Dynamic Programming? Backtracking?
Step 5 — Prove the Algorithm
Don't stop at: "This works."
Explain:
 * Invariant
 * Why each operation is safe
 * Why no valid answer is lost
 * Why the algorithm terminates
Step 6 — Complexity
Always explicitly state:
Time:  O(?)
Space: O(?)

Also distinguish Auxiliary space vs Output space.
Step 7 — Implement in C#
Write production-quality C#:
 * Clear variable names
 * Appropriate data structures
 * Avoid unnecessary allocations
 * Handle overflow where relevant
 * Handle edge cases
 * Keep comments focused on invariants rather than obvious syntax
Step 8 — Test
At minimum, test:
 * Normal case
 * Empty input
 * Single element
 * Minimum size
 * Duplicates
 * Already sorted
 * Reverse sorted
 * All same values
 * Negative values
 * Large values
Mastery Levels
For each Core problem, aim for the following levels.
 * Level 1 — Understand: Can explain the solution.
 * Level 2 — Implement: Can code it without looking.
 * Level 3 — Recognize: Can identify the pattern from an unfamiliar problem.
 * Level 4 — Derive: Can independently discover the algorithm.
 * Level 5 — Interview: Can explain: Problem ➔ Brute Force ➔ Bottleneck ➔ Optimization ➔ Invariant / Proof ➔ Complexity ➔ Implementation ➔ Edge Cases
> A Senior/Lead candidate should target Level 4–5 for Core problems.
> 
Senior / Lead Must-Master Set
If interview preparation time becomes limited, prioritize these problems.
Arrays & Hashing
1. Two Sum
 * LeetCode: #1
 * Difficulty: Easy
 * Tags: #hashing, #hashmap, #complement-lookup
2. Group Anagrams
 * LeetCode: #49
 * Difficulty: Medium
 * Tags: #hashing, #frequency-map, #canonical-form, #strings
3. Longest Consecutive Sequence
 * LeetCode: #128
 * Difficulty: Medium
 * Tags: #hashing, #hashset, #sequence, #O(n)
4. Product of Array Except Self
 * LeetCode: #238
 * Difficulty: Medium
 * Tags: #prefix-product, #array, #space-optimization
Two Pointers / K-Sum
5. Container With Most Water
 * LeetCode: #11
 * Difficulty: Medium
 * Tags: #two-pointers, #greedy, #boundary-invariant
6. 3Sum
 * LeetCode: #15
 * Difficulty: Medium
 * Tags: #sorting, #two-pointers, #k-sum, #deduplication
7. Trapping Rain Water
 * LeetCode: #42
 * Difficulty: Hard
 * Tags: #two-pointers, #prefix-max, #boundary, #invariant
Sliding Window
8. Longest Substring Without Repeating Characters
 * LeetCode: #3
 * Difficulty: Medium
 * Tags: #sliding-window, #two-pointers, #hashset, #strings
9. Minimum Window Substring
 * LeetCode: #76
 * Difficulty: Hard
 * Tags: #sliding-window, #two-pointers, #frequency-map, #strings
Prefix Sum
10. Subarray Sum Equals K
 * LeetCode: #560
 * Difficulty: Medium
 * Tags: #prefix-sum, #hashmap, #subarray, #running-sum
Linked Lists
11. Reverse Linked List
 * LeetCode: #206
 * Difficulty: Easy
 * Tags: #linked-list, #pointer-manipulation, #reversal
12. Linked List Cycle
 * LeetCode: #141
 * Difficulty: Easy
 * Tags: #linked-list, #fast-slow-pointers, #floyd-cycle
13. Reorder List
 * LeetCode: #143
 * Difficulty: Medium
 * Tags: #linked-list, #fast-slow-pointers, #reversal, #merge
14. Merge K Sorted Lists
 * LeetCode: #23
 * Difficulty: Hard
 * Tags: #linked-list, #heap, #priority-queue, #divide-and-conquer
Stack / Monotonic Stack
15. Daily Temperatures
 * LeetCode: #739
 * Difficulty: Medium
 * Tags: #monotonic-stack, #stack, #next-greater-element
16. Largest Rectangle in Histogram
 * LeetCode: #84
 * Difficulty: Hard
 * Tags: #monotonic-stack, #stack, #boundary, #histogram
Binary Search
17. Binary Search
 * LeetCode: #704
 * Difficulty: Easy
 * Tags: #binary-search, #search, #sorted-array
18. Search in Rotated Sorted Array
 * LeetCode: #33
 * Difficulty: Medium
 * Tags: #binary-search, #rotated-array, #invariant
19. Koko Eating Bananas
 * LeetCode: #875
 * Difficulty: Medium
 * Tags: #binary-search, #binary-search-on-answer, #monotonic-search-space
Binary Trees / BST
20. Maximum Depth of Binary Tree
 * LeetCode: #104
 * Difficulty: Easy
 * Tags: #tree, #dfs, #recursion, #divide-and-conquer
21. Binary Tree Level Order Traversal
 * LeetCode: #102
 * Difficulty: Medium
 * Tags: #tree, #bfs, #queue, #level-order
22. Diameter of Binary Tree
 * LeetCode: #543
 * Difficulty: Easy
 * Tags: #tree, #dfs, #bottom-up, #tree-dp
23. Validate Binary Search Tree
 * LeetCode: #98
 * Difficulty: Medium
 * Tags: #bst, #dfs, #invariant, #recursion
24. Lowest Common Ancestor of a Binary Tree
 * LeetCode: #236
 * Difficulty: Medium
 * Tags: #tree, #dfs, #recursion, #lca
25. Kth Smallest Element in a BST
 * LeetCode: #230
 * Difficulty: Medium
 * Tags: #bst, #inorder, #stack, #dfs
Heap / Priority Queue
26. Kth Largest Element in an Array
 * LeetCode: #215
 * Difficulty: Medium
 * Tags: #heap, #priority-queue, #top-k, #quickselect
27. Top K Frequent Elements
 * LeetCode: #347
 * Difficulty: Medium
 * Tags: #hashing, #heap, #top-k, #frequency
28. Median from Data Stream
 * LeetCode: #295
 * Difficulty: Hard
 * Tags: #two-heaps, #heap, #priority-queue, #streaming
Intervals
29. Merge Intervals
 * LeetCode: #56
 * Difficulty: Medium
 * Tags: #intervals, #sorting, #merging, #sweep
30. Meeting Rooms II
 * LeetCode: #253
 * Difficulty: Medium
 * Tags: #intervals, #heap, #priority-queue, #sweep-line
Graphs
31. Number of Islands
 * LeetCode: #200
 * Difficulty: Medium
 * Tags: #graph, #dfs, #bfs, #grid, #connected-components
32. Course Schedule
 * LeetCode: #207
 * Difficulty: Medium
 * Tags: #graph, #topological-sort, #dfs, #bfs, #cycle-detection
33. Network Delay Time
 * LeetCode: #743
 * Difficulty: Medium
 * Tags: #graph, #dijkstra, #shortest-path, #priority-queue
34. Redundant Connection
 * LeetCode: #684
 * Difficulty: Medium
 * Tags: #union-find, #disjoint-set, #cycle-detection, #graph
35. Min Cost to Connect All Points
 * LeetCode: #1584
 * Difficulty: Medium
 * Tags: #mst, #prim, #kruskal, #union-find, #graph
Greedy
36. Jump Game
 * LeetCode: #55
 * Difficulty: Medium
 * Tags: #greedy, #reachability, #invariant
37. Gas Station
 * LeetCode: #134
 * Difficulty: Medium
 * Tags: #greedy, #prefix-sum, #invariant
Dynamic Programming
38. House Robber
 * LeetCode: #198
 * Difficulty: Medium
 * Tags: #dynamic-programming, #1d-dp, #state-transition
39. Coin Change
 * LeetCode: #322
 * Difficulty: Medium
 * Tags: #dynamic-programming, #1d-dp, #unbounded-knapsack
40. Longest Increasing Subsequence
 * LeetCode: #300
 * Difficulty: Medium
 * Tags: #dynamic-programming, #lis, #binary-search, #subsequence
41. Longest Common Subsequence
 * LeetCode: #1143
 * Difficulty: Medium
 * Tags: #dynamic-programming, #2d-dp, #strings, #subsequence
42. Edit Distance
 * LeetCode: #72
 * Difficulty: Hard
 * Tags: #dynamic-programming, #2d-dp, #strings, #edit-distance
Backtracking
43. Subsets
 * LeetCode: #78
 * Difficulty: Medium
 * Tags: #backtracking, #recursion, #subsets, #state-space-search
44. Permutations
 * LeetCode: #46
 * Difficulty: Medium
 * Tags: #backtracking, #recursion, #permutations, #state-space-search
45. Combination Sum
 * LeetCode: #39
 * Difficulty: Medium
 * Tags: #backtracking, #recursion, #pruning, #combinations
46. Word Search
 * LeetCode: #79
 * Difficulty: Medium
 * Tags: #backtracking, #dfs, #grid, #state-space-search
Trie
47. Implement Trie
 * LeetCode: #208
 * Difficulty: Medium
 * Tags: #trie, #prefix-tree, #strings
Advanced Senior-Level Problems
48. Sliding Window Maximum
 * LeetCode: #239
 * Difficulty: Hard
 * Tags: #monotonic-queue, #deque, #sliding-window
49. Serialize and Deserialize Binary Tree
 * LeetCode: #297
 * Difficulty: Hard
 * Tags: #tree, #dfs, #bfs, #serialization, #design
50. Word Ladder
 * LeetCode: #127
 * Difficulty: Hard
 * Tags: #graph, #bfs, #shortest-path, #implicit-graph
Core Pattern Map
Use this as a quick recognition guide.
| If you see... | Think... |
|---|---|
| Fast lookup / existence | #hashing |
| Sorted array + pair | #two-pointers |
| Contiguous subarray / substring | #sliding-window |
| Subarray sum | #prefix-sum |
| Nearest greater/smaller | #monotonic-stack |
| Sorted search space | #binary-search |
| Monotonic answer space | #binary-search-on-answer |
| Top K | #heap |
| Dynamic median | #two-heaps |
| Tree hierarchy | #dfs / #bfs |
| Shortest unweighted path | #bfs |
| Shortest weighted path | #dijkstra |
| Dependencies | #topological-sort |
| Dynamic connectivity | #union-find |
| Minimum connection cost | #mst |
| Local optimal decision | #greedy |
| Overlapping subproblems | #dynamic-programming |
| Explore all combinations | #backtracking |
| Prefix matching | #trie |
| Sliding maximum/minimum | #monotonic-queue |
Recommended Practice Workflow
For each problem:
 * Study the pattern
 * Understand the brute force
 * Identify the bottleneck
 * Derive the optimized approach
 * Understand the invariant
 * Implement in C#
 * Test edge cases
 * Solve the problem on LeetCode
 * Solve 1 reinforcement problem
 * Re-solve from memory later
Reinforcement Rule
After solving a Core problem, solve one similar problem using the same pattern.
Example:
Trapping Rain Water
        ↓
Learn two-pointer boundary invariant
        ↓
Container With Most Water
        ↓
Reinforce two-pointer elimination

The objective is not:
> "Can I solve this LeetCode problem?"
> 
The objective is:
> "Can I recognize and derive this pattern when the problem is presented differently?"
> 
