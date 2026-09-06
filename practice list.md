🚀 Rapid Revision: Senior/Lead DSA Patterns
> Core Philosophy: Master the underlying patterns, identify bottlenecks, and prove invariants. Prioritize derivation over memorization. Target Level 4–5 mastery (ability to derive, prove, and implement flawlessly) for Core problems.
> 
🗝️ Legend
Difficulty: 🟢 Easy | 🟡 Medium | 🔴 Hard
Priority: ⭐ Core | 🔥 High | ⚪ Reinforcement
Senior Level: 👑 Advanced/Lead Focus (Complex invariants, system design crossover, or hard optimization)
📝 Rapid Problem-Solving Checklist
 * Understand: Input, output, constraints, edge cases.
 * Brute Force: What is the naive O(N) or O(2^N) approach?
 * Bottleneck: What repeated work can be cached, eliminated, or ordered?
 * Pattern Match: Hashing? Window? Monotonic Stack? Prefix Sum?
 * Prove Invariant: Why is this safe? Why does it not miss valid answers?
 * Complexity: Time O(?) and Space O(?) (Auxiliary vs. Output).
 * Code (C#): Clear naming, no unnecessary allocations, handle overflow.
 * Test: Empty, size=1, duplicates, negative values, large inputs.
🧠 The Unified Problem List (115 Unique Problems)
1. Arrays, Hashing & Frequency
 * #1 Two Sum 🟢 ⭐ #hashing #dictionary
 * #217 Contains Duplicate 🟢 ⚪ #hashing #hashset
 * #242 Valid Anagram 🟢 ⚪ #hashing #frequency
 * #49 Group Anagrams 🟡 ⭐ #hashing #canonical-form
 * #128 Longest Consecutive Sequence 🟡 ⭐ #hashset #sequence
 * #169 Majority Element 🟢 🔥 #hashing #boyer-moore
 * #238 Product of Array Except Self 🟡 ⭐ #prefix-product
2. Two Pointers
 * #125 Valid Palindrome 🟢 ⚪ #two-pointers #string
 * #167 Two Sum II 🟡 ⭐ #two-pointers #sorted-array
 * #11 Container With Most Water 🟡 ⭐ #two-pointers #greedy
 * #15 3Sum 🟡 ⭐ #sorting #two-pointers #deduplication
 * #18 4Sum 🟡 🔥 #k-sum #two-pointers
 * #42 Trapping Rain Water 🔴 ⭐ 👑 #two-pointers #prefix-max
3. Sliding Window
 * #643 Maximum Average Subarray I 🟡 ⚪ #sliding-window #fixed
 * #3 Longest Substring Without Repeating Characters 🟡 ⭐ #sliding-window #hashset
 * #424 Longest Repeating Character Replacement 🟡 ⭐ #sliding-window #frequency
 * #567 Permutation in String 🟡 🔥 #sliding-window #frequency
 * #76 Minimum Window Substring 🔴 ⭐ 👑 #sliding-window #frequency
4. Prefix Sum & Subarrays
 * #1480 Running Sum of 1d Array 🟢 ⚪ #prefix-sum
 * #724 Find Pivot Index 🟢 🔥 #prefix-sum #invariant
 * #560 Subarray Sum Equals K 🟡 ⭐ #prefix-sum #hashing
 * #53 Maximum Subarray 🟡 ⭐ #kadane #dp
 * #303 Range Sum Query - Immutable 🟢 ⚪ #prefix-sum #range-query
5. Strings
 * #344 Reverse String 🟢 ⚪ #two-pointers
 * #14 Longest Common Prefix 🟢 ⚪ #string
 * #443 String Compression 🟡 🔥 #two-pointers #string
 * #5 Longest Palindromic Substring 🟡 ⭐ #expand-around-center #dp
 * #647 Palindromic Substrings 🟡 🔥 #expand-around-center #dp
6. Linked Lists
 * #206 Reverse Linked List 🟢 ⭐ #linked-list #reversal
 * #21 Merge Two Sorted Lists 🟢 ⚪ #linked-list #merge
 * #141 Linked List Cycle 🟢 ⭐ #fast-slow #cycle-detection
 * #19 Remove Nth Node From End 🟡 ⭐ #fast-slow #two-pointers
 * #143 Reorder List 🟡 ⭐ #fast-slow #reversal #merge
 * #23 Merge K Sorted Lists 🔴 ⭐ 👑 #heap #divide-conquer
 * #25 Reverse Nodes in K-Group 🔴 🔥 👑 #linked-list #recursion
7. Stack & Monotonic Stack
 * #20 Valid Parentheses 🟢 ⭐ #stack
 * #155 Min Stack 🟡 ⭐ #stack #auxiliary-state
 * #150 Evaluate Reverse Polish Notation 🟡 🔥 #stack #expression
 * #739 Daily Temperatures 🟡 ⭐ #monotonic-stack
 * #84 Largest Rectangle in Histogram 🔴 ⭐ 👑 #monotonic-stack #boundary
8. Binary Search
 * #704 Binary Search 🟢 ⭐ #binary-search
 * #35 Search Insert Position 🟢 ⚪ #binary-search #boundary
 * #34 Find First and Last Position 🟡 ⭐ #binary-search #boundary
 * #33 Search in Rotated Sorted Array 🟡 ⭐ #binary-search #rotated-array
 * #153 Find Minimum in Rotated Sorted Array 🟡 🔥 #binary-search #rotated-array
 * #875 Koko Eating Bananas 🟡 ⭐ #binary-search-on-answer
 * #74 Search a 2D Matrix 🟡 🔥 #binary-search #matrix
9. Binary Trees
 * #104 Maximum Depth of Binary Tree 🟢 ⭐ #tree #dfs
 * #100 Same Tree 🟢 ⚪ #tree #dfs
 * #226 Invert Binary Tree 🟢 ⚪ #tree #recursion
 * #102 Binary Tree Level Order Traversal 🟡 ⭐ #tree #bfs #queue
 * #543 Diameter of Binary Tree 🟢 ⭐ #tree #dfs #bottom-up
 * #98 Validate Binary Search Tree 🟡 ⭐ #bst #dfs #invariant
 * #236 Lowest Common Ancestor of a Binary Tree 🟡 ⭐ #tree #dfs
 * #230 Kth Smallest Element in a BST 🟡 🔥 #bst #inorder #stack
10. Heap / Priority Queue
 * #215 Kth Largest Element in an Array 🟡 ⭐ #heap #quickselect
 * #347 Top K Frequent Elements 🟡 ⭐ #heap #hashing #top-k
 * #973 K Closest Points to Origin 🟡 🔥 #heap #top-k
 * #295 Find Median from Data Stream 🔴 ⭐ 👑 #two-heaps #streaming
11. Intervals
 * #56 Merge Intervals 🟡 ⭐ #intervals #sorting
 * #57 Insert Interval 🟡 🔥 #intervals #merge
 * #435 Non-overlapping Intervals 🟡 🔥 #intervals #greedy
 * #253 Meeting Rooms II 🟡 ⭐ #intervals #heap #sweep-line
12. Graph Traversal
 * #200 Number of Islands 🟡 ⭐ #graph #dfs #bfs #grid
 * #133 Clone Graph 🟡 🔥 #graph #dfs #hashmap
 * #547 Number of Provinces 🟡 🔥 #graph #dfs #union-find
 * #323 Number of Connected Components 🟡 ⭐ #graph #dfs #union-find
 * #207 Course Schedule 🟡 ⭐ #graph #topological-sort #cycle
 * #210 Course Schedule II 🟡 🔥 #graph #topological-sort
13. Graph Algorithms
 * #743 Network Delay Time 🟡 ⭐ #graph #dijkstra
 * #684 Redundant Connection 🟡 ⭐ #union-find #cycle-detection
 * #787 Cheapest Flights Within K Stops 🟡 🔥 #graph #shortest-path #dp
 * #1584 Min Cost to Connect All Points 🟡 ⭐ #mst #prim #kruskal
 * #1192 Critical Connections in a Network 🔴 👑 #graph #tarjan #bridges
14. Greedy
 * #55 Jump Game 🟡 ⭐ #greedy #reachability
 * #134 Gas Station 🟡 ⭐ #greedy #invariant
 * #763 Partition Labels 🟡 🔥 #greedy #interval
 * #435 Non-overlapping Intervals 🟡 🔥 #greedy #intervals
15. Dynamic Programming Fundamentals
 * #70 Climbing Stairs 🟢 ⭐ #dp #1d-dp
 * #198 House Robber 🟡 ⭐ #dp #1d-dp
 * #322 Coin Change 🟡 ⭐ #dp #unbounded-knapsack
 * #139 Word Break 🟡 ⭐ #dp #string #hashset
 * #416 Partition Equal Subset Sum 🟡 🔥 #dp #0-1-knapsack
 * #300 Longest Increasing Subsequence 🟡 ⭐ #dp #binary-search
 * #152 Maximum Product Subarray 🟡 🔥 #dp #state-machine
16. Advanced Dynamic Programming
 * #62 Unique Paths 🟡 ⚪ #dp #2d-dp
 * #64 Minimum Path Sum 🟡 🔥 #dp #2d-dp
 * #1143 Longest Common Subsequence 🟡 ⭐ #dp #string #2d-dp
 * #72 Edit Distance 🔴 ⭐ 👑 #dp #string #2d-dp
 * #91 Decode Ways 🟡 🔥 #dp #1d-dp
 * #494 Target Sum 🟡 🔥 #dp #subset-sum
 * #309 Best Time to Buy and Sell Stock with Cooldown 🟡 👑 #dp #state-machine
17. Backtracking
 * #78 Subsets 🟡 ⭐ #backtracking #subsets
 * #46 Permutations 🟡 ⭐ #backtracking #permutations
 * #39 Combination Sum 🟡 ⭐ #backtracking #pruning
 * #17 Letter Combinations of a Phone Number 🟡 🔥 #backtracking #combinations
 * #79 Word Search 🟡 ⭐ #backtracking #grid #dfs
 * #51 N-Queens 🔴 👑 #backtracking #constraint-satisfaction
18. Trie / String Search
 * #208 Implement Trie 🟡 ⭐ #trie #string
 * #211 Design Add and Search Words Data Structure 🟡 🔥 #trie #dfs
 * #212 Word Search II 🔴 👑 #trie #dfs #backtracking
19. Union-Find / Disjoint Set
 * #547 Number of Provinces 🟡 🔥 #union-find
 * #684 Redundant Connection 🟡 ⭐ #union-find #cycle-detection
 * #721 Accounts Merge 🟡 👑 #union-find #graph #hashing
 * #1584 Min Cost to Connect All Points 🟡 ⭐ #union-find #mst
20. Advanced / Senior-Level Masterclass
 * #239 Sliding Window Maximum 🔴 ⭐ 👑 #monotonic-queue #sliding-window
 * #4 Median of Two Sorted Arrays 🔴 👑 #binary-search #partition
 * #84 Largest Rectangle in Histogram 🔴 ⭐ 👑 #monotonic-stack
 * #42 Trapping Rain Water 🔴 ⭐ 👑 #two-pointers #prefix-max
 * #127 Word Ladder 🔴 🔥 👑 #bfs #graph #shortest-path
 * #778 Swim in Rising Water 🔴 👑 #graph #binary-search #dijkstra #union-find
 * #32 Longest Valid Parentheses 🔴 👑 #stack #dp
 * #10 Regular Expression Matching 🔴 👑 #dp #recursion
 * #312 Burst Balloons 🔴 👑 #interval-dp #recursion
 * #297 Serialize and Deserialize Binary Tree 🔴 🔥 👑 #tree #dfs #bfs #serialization
