# 🎯 HYBRID PRACTICE PROBLEM LIST (PHASES) — 300+ LeetCode Problems
## Topic-aligned • Pattern-first • Interview-focused

This file is organized strictly by **phases** (matching the hybrid curriculum).
Each problem includes: LC#, priority, **topics to know**, and a 1-line **approach**.

### 🧭 Priority Legend
- 🔑 ANCHOR — master first
- ⭐ HIGH VALUE — frequent in interviews
- 🧱 STANDARD — core coverage
- 🎯 PRACTICE — reinforce variations
- 💡 ADVANCED — multi-pattern / tricky
- 🧪 CHALLENGE — optional stretch

**Total unique problems:** 338+

---

## Phase 0 — Foundations & Core Patterns 🧠

| # | Problem | LC# | Priority | Topics to know | Hybrid approach |
|---:|---|---:|---|---|---|
| 1 | Valid Parentheses | 20 | 🔑 ANCHOR | Stack | Push opens; pop/match closes |
| 2 | Maximum Subarray | 53 | 🔑 ANCHOR | Kadane, greedy/DP | Track best ending here |
| 3 | Climbing Stairs | 70 | 🔑 ANCHOR | 1D DP, recurrence | dp[i]=dp[i-1]+dp[i-2] |
| 4 | Binary Search | 704 | 🔑 ANCHOR | Big-O, loop invariants | Classic BS template; shrink search space |
| 5 | Single Number | 136 | ⭐ HIGH VALUE | XOR properties | XOR all; duplicates cancel |
| 6 | Counting Bits | 338 | ⭐ HIGH VALUE | DP + bit ops | dp[i]=dp[i>>1]+(i&1) |
| 7 | Power of Two | 231 | 🎯 PRACTICE | Bit tricks | n>0 and n&(n-1)==0 |
| 8 | First Bad Version | 278 | 🎯 PRACTICE | Boundary search | BS for leftmost true |
| 9 | Find the Duplicate Number | 287 | 💡 ADVANCED | Cycle detection, binary search | Floyd cycle on values-as-next |
| 10 | Median of Two Sorted Arrays | 4 | 🧪 CHALLENGE | Binary search partitions | Partition arrays; median from borders |
---

## Phase 1 — Arrays, Strings & Hashing 🧺

| # | Problem | LC# | Priority | Topics to know | Hybrid approach |
|---:|---|---:|---|---|---|
| 1 | Two Sum | 1 | 🔑 ANCHOR | Hash map | Store seen; check target-x |
| 2 | Group Anagrams | 49 | 🔑 ANCHOR | Hashing key | Key by sorted string / counts |
| 3 | Product of Array Except Self | 238 | 🔑 ANCHOR | Prefix/suffix products | Two passes; no division |
| 4 | Top K Frequent Elements | 347 | 🔑 ANCHOR | Hash + heap/buckets | Count then select top-k |
| 5 | Subarray Sum Equals K | 560 | 🔑 ANCHOR | Prefix sum + hash | Count prefixSum-k occurrences |
| 6 | Rotate Image | 48 | ⭐ HIGH VALUE | Matrix ops | Transpose then reverse rows |
| 7 | Set Matrix Zeroes | 73 | ⭐ HIGH VALUE | Matrix markers | Use first row/col as flags |
| 8 | Longest Consecutive Sequence | 128 | ⭐ HIGH VALUE | Hash set | Start only at sequence heads |
| 9 | Majority Element | 169 | ⭐ HIGH VALUE | Boyer–Moore | Cancel pairs; keep candidate |
| 10 | Kth Largest Element in an Array | 215 | ⭐ HIGH VALUE | Heap/quickselect | Min-heap size k or quickselect |
| 11 | Encode and Decode Strings | 271 | ⭐ HIGH VALUE | String design | Length-prefix encoding |
| 12 | Contiguous Array | 525 | ⭐ HIGH VALUE | Prefix sum map | Map diff(count1-count0)->first index |
| 13 | Valid Sudoku | 36 | 🧱 STANDARD | Constraints, hashing | Row/col/box sets |
| 14 | Spiral Matrix | 54 | 🧱 STANDARD | Boundaries | 4 pointers; traverse layers |
| 15 | Contains Duplicate | 217 | 🧱 STANDARD | Set | Insert; detect repeat |
| 16 | Valid Anagram | 242 | 🧱 STANDARD | Counts, sorting | Frequency map or sort |
| 17 | Find All Duplicates in an Array | 442 | 🧱 STANDARD | Index marking | Negate; if already neg => dup |
| 18 | Find All Numbers Disappeared in an Array | 448 | 🧱 STANDARD | Index marking | Negate at index=abs(x)-1 |
| 19 | Sort Characters By Frequency | 451 | 🧱 STANDARD | Hash + bucket/heap | Count then sort by freq |
| 20 | Continuous Subarray Sum | 523 | 🧱 STANDARD | Prefix mod | Seen mod; subarray len>=2 |
| 21 | Reorganize String | 767 | 🧱 STANDARD | Heap greedy | Place most frequent with cooldown |
| 22 | Subarray Sums Divisible by K | 974 | 🧱 STANDARD | Prefix mod counts | Count equal mods |
| 23 | Happy Number | 202 | 🎯 PRACTICE | Cycle detection | Use set or Floyd on digit-square |
| 24 | Isomorphic Strings | 205 | 🎯 PRACTICE | Bijection mapping | Map s->t and t->s |
| 25 | Missing Number | 268 | 🎯 PRACTICE | XOR/sum | XOR indices and values |
| 26 | Word Pattern | 290 | 🎯 PRACTICE | Mapping | Pattern↔word bijection |
| 27 | Intersection of Two Arrays | 349 | 🎯 PRACTICE | Set | Set intersection |
| 28 | Intersection of Two Arrays II | 350 | 🎯 PRACTICE | Counts | Count with map then output |
| 29 | Ransom Note | 383 | 🎯 PRACTICE | Counts | Count magazine letters |
| 30 | First Unique Character in a String | 387 | 🎯 PRACTICE | Counts | Count then first count==1 |
| 31 | Maximum Product of Three Numbers | 628 | 🎯 PRACTICE | Sorting | Pick top3 or 2min*max |
| 32 | Backspace String Compare | 844 | 🎯 PRACTICE | Stack/2ptr | Simulate or skip-back pointers |
| 33 | Sort Array By Parity | 905 | 🎯 PRACTICE | Two pointers | Partition even/odd |
| 34 | Sort Array By Parity II | 922 | 🎯 PRACTICE | Two pointers | Place evens on even idx |
| 35 | Reorder Data in Log Files | 937 | 🎯 PRACTICE | Sorting | Custom comparator; stable |
| 36 | Maximum Erasure Value | 1695 | 🎯 PRACTICE | Window + set | Unique window sum max |
---

## Phase 2 — Two Pointers & Sliding Window ↔️🪟

| # | Problem | LC# | Priority | Topics to know | Hybrid approach |
|---:|---|---:|---|---|---|
| 1 | Longest Substring Without Repeating Characters | 3 | 🔑 ANCHOR | Sliding window | Expand; shrink on duplicates |
| 2 | Container With Most Water | 11 | 🔑 ANCHOR | Greedy pointers | Move shorter height inward |
| 3 | 3Sum | 15 | 🔑 ANCHOR | Sort + 2 pointers | Fix i; scan remaining; skip dups |
| 4 | Minimum Window Substring | 76 | 🔑 ANCHOR | Window + counts | Expand to satisfy; shrink to min |
| 5 | Valid Palindrome | 125 | 🔑 ANCHOR | Two pointers | Skip non-alnum; compare |
| 6 | Two Sum II - Input Array Is Sorted | 167 | 🔑 ANCHOR | Two pointers | Move ends based on sum |
| 7 | Sliding Window Maximum | 239 | 🔑 ANCHOR | Monotonic deque | Maintain decreasing indices |
| 8 | Longest Repeating Character Replacement | 424 | 🔑 ANCHOR | Window + freq | Keep maxFreq; shrink if invalid |
| 9 | Next Permutation | 31 | ⭐ HIGH VALUE | Permutation logic | Find pivot, swap, reverse suffix |
| 10 | Merge Sorted Array | 88 | ⭐ HIGH VALUE | 3 pointers | Fill from back |
| 11 | Find All Anagrams in a String | 438 | ⭐ HIGH VALUE | Fixed window | Slide and match counts |
| 12 | Permutation in String | 567 | ⭐ HIGH VALUE | Fixed window counts | Compare window counts |
| 13 | Remove Duplicates from Sorted Array | 26 | 🧱 STANDARD | In-place | Slow pointer write unique |
| 14 | Remove Element | 27 | 🧱 STANDARD | In-place | Overwrite/partition |
| 15 | Minimum Size Subarray Sum | 209 | 🧱 STANDARD | Variable window | Shrink while sum>=target |
| 16 | Move Zeroes | 283 | 🧱 STANDARD | Slow/fast | Write non-zeros then fill |
| 17 | Subarray Product Less Than K | 713 | 🧱 STANDARD | Variable window | Shrink while prod>=k |
| 18 | Boats to Save People | 881 | 🧱 STANDARD | Sort + 2ptr | Pair heaviest with lightest |
| 19 | Max Consecutive Ones III | 1004 | 🧱 STANDARD | Window with budget | At most k zeros; shrink when >k |
| 20 | 3Sum Closest | 16 | 🎯 PRACTICE | Sort + 2ptr | Fix i; minimize abs diff |
| 21 | 4Sum | 18 | 🎯 PRACTICE | Sort + k-sum | Reduce to 2-sum with pruning |
| 22 | Reverse String | 344 | 🎯 PRACTICE | Pointers | Swap ends |
| 23 | Reverse Vowels of a String | 345 | 🎯 PRACTICE | Pointers | Swap vowels |
| 24 | Valid Palindrome II | 680 | 🎯 PRACTICE | Two pointers | Allow one deletion |
| 25 | Binary Subarrays With Sum | 930 | 🎯 PRACTICE | Prefix/window | Count via prefix sums |
| 26 | Bag of Tokens | 948 | 🎯 PRACTICE | Greedy 2ptr | Spend smallest for power; sell largest |
| 27 | Squares of a Sorted Array | 977 | 🎯 PRACTICE | 2 pointers | Compare abs ends; fill back |
| 28 | Count Number of Nice Subarrays | 1248 | 🎯 PRACTICE | Prefix | Count odds via prefix |
| 29 | Number of Substrings Containing All Three Characters | 1358 | 🎯 PRACTICE | Window | Count windows with all chars |
| 30 | Maximum Number of Vowels in a Substring of Given Length | 1456 | 🎯 PRACTICE | Fixed window | Slide count of vowels |
| 31 | Longest Subarray of 1's After Deleting One Element | 1493 | 🎯 PRACTICE | Window | Allow one zero |
| 32 | Shortest Subarray with Sum at Least K | 862 | 💡 ADVANCED | Deque + prefix | Monotonic deque on prefix sums |
| 33 | Subarrays with K Different Integers | 992 | 💡 ADVANCED | Window | AtMost(K) - AtMost(K-1) |
| 34 | Longest Continuous Subarray With Absolute Diff <= Limit | 1438 | 💡 ADVANCED | 2 deques | Maintain min/max deques |
| 35 | Frequency of the Most Frequent Element | 1838 | 💡 ADVANCED | Sort + window | Raise window to nums[r] within k |
---

## Phase 3 — Linked Lists + Stacks/Queues 🔗📚

| # | Problem | LC# | Priority | Topics to know | Hybrid approach |
|---:|---|---:|---|---|---|
| 1 | Remove Nth Node From End of List | 19 | 🔑 ANCHOR | Two pointers | Advance fast n; then move both |
| 2 | Merge Two Sorted Lists | 21 | 🔑 ANCHOR | Dummy node | Merge by choosing smaller head |
| 3 | Linked List Cycle | 141 | 🔑 ANCHOR | Floyd | Fast/slow meet => cycle |
| 4 | Min Stack | 155 | 🔑 ANCHOR | Design | Track mins with aux stack |
| 5 | Reverse Linked List | 206 | 🔑 ANCHOR | Pointers | Iterative prev/curr/next |
| 6 | Daily Temperatures | 739 | 🔑 ANCHOR | Monotonic stack | Pop while current warmer |
| 7 | Add Two Numbers | 2 | ⭐ HIGH VALUE | Carry | Simulate addition with carry |
| 8 | Merge k Sorted Lists | 23 | ⭐ HIGH VALUE | Heap | Push heads; pop/push next |
| 9 | Trapping Rain Water | 42 | ⭐ HIGH VALUE | Stack/2ptr | Use boundaries; accumulate water |
| 10 | Copy List with Random Pointer | 138 | ⭐ HIGH VALUE | Hash map | Old->new mapping; reconnect |
| 11 | Linked List Cycle II | 142 | ⭐ HIGH VALUE | Floyd math | Reset one; move both to entry |
| 12 | Reorder List | 143 | ⭐ HIGH VALUE | Multi-step | Mid + reverse + merge alternately |
| 13 | Remove K Digits | 402 | ⭐ HIGH VALUE | Monotonic stack | Pop bigger digits while k>0 |
| 14 | Minimum Remove to Make Valid Parentheses | 1249 | ⭐ HIGH VALUE | Stack | Mark invalid indices; rebuild |
| 15 | Simplify Path | 71 | 🧱 STANDARD | Stack | Process '/', '.', '..' |
| 16 | Evaluate Reverse Polish Notation | 150 | 🧱 STANDARD | Stack | Apply ops to top two |
| 17 | Palindrome Linked List | 234 | 🧱 STANDARD | Reverse half | Find mid; reverse 2nd half; compare |
| 18 | Decode String | 394 | 🧱 STANDARD | Stack | Parse k[substr] |
| 19 | Next Greater Element I | 496 | 🧱 STANDARD | Monotonic stack | Map next greater via stack |
| 20 | Next Greater Element II | 503 | 🧱 STANDARD | Circular stack | Traverse twice; mod index |
| 21 | Asteroid Collision | 735 | 🧱 STANDARD | Stack | Simulate collisions |
| 22 | Middle of the Linked List | 876 | 🧱 STANDARD | Fast/slow | Slow ends at middle |
| 23 | Online Stock Span | 901 | 🧱 STANDARD | Monotonic stack | Pop while price >= top |
| 24 | Swap Nodes in Pairs | 24 | 🎯 PRACTICE | Pointer ops | Swap in-place with dummy |
| 25 | Rotate List | 61 | 🎯 PRACTICE | Cycle | Make cycle; break at new tail |
| 26 | Intersection of Two Linked Lists | 160 | 🎯 PRACTICE | Pointer sync | Two runners switch heads |
| 27 | Implement Stack using Queues | 225 | 🎯 PRACTICE | Queue rotation | Rotate to simulate stack |
| 28 | Implement Queue using Stacks | 232 | 🎯 PRACTICE | 2 stacks | Amortized enqueue/dequeue |
| 29 | Odd Even Linked List | 328 | 🎯 PRACTICE | Rewire | Separate odd/even chains |
| 30 | Implement Circular Queue | 622 | 🎯 PRACTICE | Design | Array ring buffer |
| 31 | Design Circular Deque | 641 | 🎯 PRACTICE | Design | Array ring buffer |
| 32 | Minimum Add to Make Parentheses Valid | 921 | 🎯 PRACTICE | Greedy/stack | Count balance; add needed |
| 33 | Remove All Adjacent Duplicates In String | 1047 | 🎯 PRACTICE | Stack | Pop if same as top |
| 34 | Reverse Nodes in k-Group | 25 | 💡 ADVANCED | Chunk reversal | Reverse k nodes repeatedly |
| 35 | Largest Rectangle in Histogram | 84 | 💡 ADVANCED | Monotonic stack | Pop heights; compute width |
| 36 | Sort List | 148 | 💡 ADVANCED | Merge sort | Split with slow/fast; merge |
| 37 | Exclusive Time of Functions | 636 | 💡 ADVANCED | Stack | Track prev timestamp |
| 38 | Sum of Subarray Minimums | 907 | 💡 ADVANCED | Monotonic stack | Count contribution via spans |
---

## Phase 4 — Trees & BST (DFS/BFS) 🌳

| # | Problem | LC# | Priority | Topics to know | Hybrid approach |
|---:|---|---:|---|---|---|
| 1 | Validate Binary Search Tree | 98 | 🔑 ANCHOR | BST invariants | Range-check recursion |
| 2 | Binary Tree Level Order Traversal | 102 | 🔑 ANCHOR | BFS | Queue by level size |
| 3 | Maximum Depth of Binary Tree | 104 | 🔑 ANCHOR | DFS recursion | Return 1+max(depths) |
| 4 | Invert Binary Tree | 226 | 🔑 ANCHOR | DFS | Swap children recursively |
| 5 | Lowest Common Ancestor of a Binary Tree | 236 | 🔑 ANCHOR | DFS | Return node if p/q; combine |
| 6 | Diameter of Binary Tree | 543 | 🔑 ANCHOR | DFS bottom-up | Height return; update global diam |
| 7 | Construct Binary Tree from Preorder and Inorder Traversal | 105 | ⭐ HIGH VALUE | Recursion | Pick root from preorder; split inorder |
| 8 | Flatten Binary Tree to Linked List | 114 | ⭐ HIGH VALUE | DFS | Rewire using preorder |
| 9 | BST Iterator | 173 | ⭐ HIGH VALUE | Stack | Inorder iterator with stack |
| 10 | Binary Tree Right Side View | 199 | ⭐ HIGH VALUE | BFS/DFS | Take last of each level |
| 11 | Kth Smallest Element in a BST | 230 | ⭐ HIGH VALUE | Inorder | Iterate inorder count |
| 12 | Path Sum III | 437 | ⭐ HIGH VALUE | Prefix sum on tree | DFS with prefix-sum map |
| 13 | Same Tree | 100 | 🧱 STANDARD | DFS | Compare structure and values |
| 14 | Symmetric Tree | 101 | 🧱 STANDARD | Mirror DFS | Compare L.left vs R.right |
| 15 | Convert Sorted Array to Binary Search Tree | 108 | 🧱 STANDARD | Divide and conquer | Mid as root recursively |
| 16 | Balanced Binary Tree | 110 | 🧱 STANDARD | DFS + sentinel | Return height or -1 if unbalanced |
| 17 | Path Sum | 112 | 🧱 STANDARD | Top-down | Subtract target along path |
| 18 | Populating Next Right Pointers in Each Node | 116 | 🧱 STANDARD | BFS/pointers | Connect level next pointers |
| 19 | Lowest Common Ancestor of a BST | 235 | 🧱 STANDARD | BST property | Walk down by comparing p,q |
| 20 | Subtree of Another Tree | 572 | 🧱 STANDARD | DFS | Compare at each node |
| 21 | Merge Two Binary Trees | 617 | 🧱 STANDARD | DFS | Sum nodes; recurse |
| 22 | Binary Tree Zigzag Level Order Traversal | 103 | 🎯 PRACTICE | BFS | Alternate direction per level |
| 23 | Construct Binary Tree from Inorder and Postorder Traversal | 106 | 🎯 PRACTICE | Recursion | Root from postorder; split inorder |
| 24 | Minimum Depth of Binary Tree | 111 | 🎯 PRACTICE | BFS/DFS | BFS first leaf |
| 25 | Path Sum II | 113 | 🎯 PRACTICE | DFS backtracking | Track path list |
| 26 | Populating Next Right Pointers in Each Node II | 117 | 🎯 PRACTICE | BFS | Handle missing children |
| 27 | Sum Root to Leaf Numbers | 129 | 🎯 PRACTICE | DFS | Carry number down |
| 28 | Insert into a Binary Search Tree | 701 | 🎯 PRACTICE | BST | Iterative/recursive insert |
| 29 | Range Sum of BST | 938 | 🎯 PRACTICE | DFS | Prune by BST property |
| 30 | Count Good Nodes in Binary Tree | 1448 | 🎯 PRACTICE | DFS | Track path max |
| 31 | Pseudo-Palindromic Paths in a Binary Tree | 1457 | 🎯 PRACTICE | Bitmask | Toggle counts; check <=1 odd |
| 32 | Binary Tree Maximum Path Sum | 124 | 💡 ADVANCED | DFS + global | Best downward gain; update answer |
| 33 | Serialize and Deserialize Binary Tree | 297 | 💡 ADVANCED | Design + DFS | Preorder with null markers |
| 34 | House Robber III | 337 | 💡 ADVANCED | Tree DP | Return (rob,skip) per node |
| 35 | Delete Node in a BST | 450 | 💡 ADVANCED | BST | Find node; replace with successor |
| 36 | Binary Tree Cameras | 968 | 🧪 CHALLENGE | Greedy/DP | States: covered/hasCam/needsCam |
---

## Phase 5 — Graphs (DFS/BFS + Topo) 🗺️

| # | Problem | LC# | Priority | Topics to know | Hybrid approach |
|---:|---|---:|---|---|---|
| 1 | Number of Islands | 200 | 🔑 ANCHOR | DFS/BFS grid | Count components; mark visited |
| 2 | Course Schedule | 207 | 🔑 ANCHOR | Topological sort | Kahn indegrees; detect cycle |
| 3 | Flood Fill | 733 | 🔑 ANCHOR | DFS/BFS grid | Recolor component |
| 4 | Rotting Oranges | 994 | 🔑 ANCHOR | Multi-source BFS | BFS layers = minutes |
| 5 | Surrounded Regions | 130 | ⭐ HIGH VALUE | Border DFS | Mark safe from borders; flip rest |
| 6 | Clone Graph | 133 | ⭐ HIGH VALUE | DFS/BFS + map | Old->new clone mapping |
| 7 | Course Schedule II | 210 | ⭐ HIGH VALUE | Topo order | Return ordering via Kahn |
| 8 | Graph Valid Tree | 261 | ⭐ HIGH VALUE | DFS/DSU | n-1 edges + connected + acyclic |
| 9 | Evaluate Division | 399 | ⭐ HIGH VALUE | Graph weighted | DFS/BFS compute ratios |
| 10 | 01 Matrix | 542 | ⭐ HIGH VALUE | Multi-source BFS | Distances from all zeros |
| 11 | Number of Provinces | 547 | 🧱 STANDARD | DFS/DSU | Count connected components |
| 12 | Max Area of Island | 695 | 🧱 STANDARD | DFS | Return area while marking |
| 13 | Is Graph Bipartite? | 785 | 🧱 STANDARD | BFS coloring | 2-color with conflict check |
| 14 | Find Eventual Safe States | 802 | 🧱 STANDARD | Topo/DFS | Reverse graph topo or color DFS |
| 15 | Shortest Path in Binary Matrix | 1091 | 🧱 STANDARD | BFS | Unweighted shortest path |
| 16 | Walls and Gates | 286 | 🎯 PRACTICE | Multi-source BFS | BFS from gates to fill distances |
| 17 | Island Perimeter | 463 | 🎯 PRACTICE | Grid scan | Count edges of land |
| 18 | Minesweeper | 529 | 🎯 PRACTICE | DFS/BFS | Reveal empties recursively |
| 19 | All Paths From Source to Target | 797 | 🎯 PRACTICE | DFS backtracking | Enumerate paths in DAG |
| 20 | Keys and Rooms | 841 | 🎯 PRACTICE | DFS | Visit reachable rooms |
| 21 | Snakes and Ladders | 909 | 🎯 PRACTICE | BFS | Shortest moves with board jumps |
| 22 | As Far from Land as Possible | 1162 | 🎯 PRACTICE | Multi-source BFS | Max dist from any land |
| 23 | Number of Closed Islands | 1254 | 🎯 PRACTICE | DFS | Count components not touching border |
| 24 | Count Sub Islands | 1905 | 🎯 PRACTICE | DFS | Island in grid2 contained in grid1 |
| 25 | Find if Path Exists in Graph | 1971 | 🎯 PRACTICE | DFS/BFS | Traverse adjacency list |
| 26 | Word Ladder | 127 | 💡 ADVANCED | BFS | Layered BFS via wildcard buckets |
| 27 | Minimum Height Trees | 310 | 💡 ADVANCED | Topo trimming | Peel leaves until centers |
| 28 | Reconstruct Itinerary | 332 | 💡 ADVANCED | Eulerian path | Hierholzer with min-heap |
| 29 | Pacific Atlantic Water Flow | 417 | 💡 ADVANCED | Multi-source DFS | Reachability from both oceans |
| 30 | Redundant Connection II | 685 | 💡 ADVANCED | Graph | Detect node with two parents + cycle |
| 31 | Bus Routes | 815 | 💡 ADVANCED | BFS | Stops->routes graph |
| 32 | Alien Dictionary | 269 | 🧪 CHALLENGE | Topo + compare | Build edges by adjacent words |
---

## Phase 6 — Binary Search + Intervals 📏🔍

| # | Problem | LC# | Priority | Topics to know | Hybrid approach |
|---:|---|---:|---|---|---|
| 1 | Search in Rotated Sorted Array | 33 | 🔑 ANCHOR | Rotated BS | Find sorted half; discard other |
| 2 | Find First and Last Position of Element | 34 | 🔑 ANCHOR | Boundary BS | Two searches: leftmost/rightmost |
| 3 | Merge Intervals | 56 | 🔑 ANCHOR | Sort intervals | Merge overlaps by tracking end |
| 4 | Insert Interval | 57 | 🔑 ANCHOR | Intervals | Append non-overlap; merge; append rest |
| 5 | Non-overlapping Intervals | 435 | 🔑 ANCHOR | Greedy | Sort by end; count removals |
| 6 | Koko Eating Bananas | 875 | 🔑 ANCHOR | BS on answer | Feasibility check hours<=h |
| 7 | Capacity To Ship Packages Within D Days | 1011 | 🔑 ANCHOR | BS on answer | Feasible(capacity) via simulation |
| 8 | Meeting Rooms II | 253 | ⭐ HIGH VALUE | Heap | Min-heap of end times |
| 9 | Kth Smallest Element in a Sorted Matrix | 378 | ⭐ HIGH VALUE | BS on value | Count <=mid per row |
| 10 | Minimum Number of Arrows to Burst Balloons | 452 | ⭐ HIGH VALUE | Greedy | Sort by end; shoot at end |
| 11 | Interval List Intersections | 986 | ⭐ HIGH VALUE | Two pointers | Advance smaller end |
| 12 | Search a 2D Matrix | 74 | 🧱 STANDARD | BS | Treat matrix as 1D |
| 13 | Find Minimum in Rotated Sorted Array | 153 | 🧱 STANDARD | Rotated BS | Compare mid to right |
| 14 | Find Peak Element | 162 | 🧱 STANDARD | Gradient BS | Move toward rising slope |
| 15 | Meeting Rooms | 252 | 🧱 STANDARD | Sort | Check start < prevEnd |
| 16 | Minimum Number of Days to Make m Bouquets | 1482 | 🧱 STANDARD | BS on answer | Feasible(day) by scanning |
| 17 | Sqrt(x) | 69 | 🎯 PRACTICE | BS | Largest mid^2 <= x |
| 18 | Search in Rotated Sorted Array II | 81 | 🎯 PRACTICE | Duplicates | Handle equals; shrink bounds |
| 19 | Find Minimum in Rotated Sorted Array II | 154 | 🎯 PRACTICE | Duplicates | If equal, right-- |
| 20 | H-Index II | 275 | 🎯 PRACTICE | BS | Find first citation>=n-i |
| 21 | Valid Perfect Square | 367 | 🎯 PRACTICE | BS | Check mid^2 |
| 22 | Guess Number Higher or Lower | 374 | 🎯 PRACTICE | BS API | Use feedback to adjust bounds |
| 23 | Find Right Interval | 436 | 🎯 PRACTICE | Sort + BS | Map start->index; BS for end |
| 24 | My Calendar I | 729 | 🎯 PRACTICE | Intervals | Store; check overlap |
| 25 | Car Fleet | 853 | 🎯 PRACTICE | Sort + stack | Compute times; merge fleets |
| 26 | Find the Smallest Divisor Given a Threshold | 1283 | 🎯 PRACTICE | BS on answer | Sum ceil(nums/d) <= threshold |
| 27 | Remove Covered Intervals | 1288 | 🎯 PRACTICE | Sort | Sort start asc end desc; count uncovered |
| 28 | Magnetic Force Between Two Balls | 1552 | 🎯 PRACTICE | BS + greedy | Place balls greedily; maximize min dist |
| 29 | Split Array Largest Sum | 410 | 💡 ADVANCED | BS + greedy | Check partitions needed for maxSum |
| 30 | My Calendar II | 731 | 💡 ADVANCED | Sweep | Allow double booking; prevent triple |
| 31 | Minimum Interval to Include Each Query | 1851 | 💡 ADVANCED | Heap | Sort intervals; add feasible by start |
| 32 | Divide Intervals Into Minimum Number of Groups | 2406 | 💡 ADVANCED | Heap | Greedy with end-time heap |
| 33 | My Calendar III | 732 | 🧪 CHALLENGE | Sweep | Max overlap via diff map |
| 34 | Minimum Number of Taps to Open to Water a Garden | 1326 | 🧪 CHALLENGE | Greedy intervals | Jump-game on coverage |
---

## Phase 7 — Greedy & Monotonic Patterns 🪙

| # | Problem | LC# | Priority | Topics to know | Hybrid approach |
|---:|---|---:|---|---|---|
| 1 | Jump Game II | 45 | 🔑 ANCHOR | Greedy ranges | BFS-level style on ranges |
| 2 | Jump Game | 55 | 🔑 ANCHOR | Greedy reach | Track farthest reachable |
| 3 | Partition Labels | 763 | 🔑 ANCHOR | Greedy | Cut when i reaches max lastIndex |
| 4 | Gas Station | 134 | ⭐ HIGH VALUE | Greedy proof | If total>=0, start after min prefix |
| 5 | Largest Number | 179 | ⭐ HIGH VALUE | Custom sort | Sort by xy vs yx |
| 6 | Queue Reconstruction by Height | 406 | ⭐ HIGH VALUE | Greedy + sort | Sort desc height; insert by k |
| 7 | Task Scheduler | 621 | ⭐ HIGH VALUE | Greedy | Count max freq; compute idle slots |
| 8 | Best Time to Buy and Sell Stock II | 122 | 🧱 STANDARD | Greedy | Sum all positive deltas |
| 9 | Assign Cookies | 455 | 🧱 STANDARD | Greedy + sort | Match smallest satisfiable |
| 10 | Wiggle Subsequence | 376 | 🎯 PRACTICE | Greedy/DP | Count sign changes |
| 11 | Can Place Flowers | 605 | 🎯 PRACTICE | Greedy | Place if neighbors empty |
| 12 | Maximum Swap | 670 | 🎯 PRACTICE | Greedy digits | Swap with farthest larger digit |
| 13 | Hand of Straights | 846 | 🎯 PRACTICE | Counting | Greedy build sequences from min |
| 14 | Lemonade Change | 860 | 🎯 PRACTICE | Greedy counts | Use 5/10 bills optimally |
| 15 | Two City Scheduling | 1029 | 🎯 PRACTICE | Greedy | Sort by cost diff |
| 16 | Minimum Deletions to Make Character Frequencies Unique | 1647 | 🎯 PRACTICE | Greedy | Decrease freqs to unused |
| 17 | Candy | 135 | 💡 ADVANCED | Two-pass greedy | Left pass then right pass |
---

## Phase 8 — Dynamic Programming (1D/2D/Knapsack) 🧩

| # | Problem | LC# | Priority | Topics to know | Hybrid approach |
|---:|---|---:|---|---|---|
| 1 | Unique Paths | 62 | 🔑 ANCHOR | Grid DP | dp[i][j]=dp[i-1][j]+dp[i][j-1] |
| 2 | Edit Distance | 72 | 🔑 ANCHOR | 2D DP | Min(insert,delete,replace) |
| 3 | Word Break | 139 | 🔑 ANCHOR | DP on prefix | dp[i]=any dp[j] and word(j,i) |
| 4 | House Robber | 198 | 🔑 ANCHOR | 1D DP | dp[i]=max(dp[i-1],dp[i-2]+nums[i]) |
| 5 | Longest Increasing Subsequence | 300 | 🔑 ANCHOR | DP/BS | Patience: tails[] with BS |
| 6 | Coin Change | 322 | 🔑 ANCHOR | Unbounded knapsack | dp[a]=min(dp[a],dp[a-c]+1) |
| 7 | Partition Equal Subset Sum | 416 | 🔑 ANCHOR | 0/1 knapsack | dp[s]=can make sum s |
| 8 | Longest Common Subsequence | 1143 | 🔑 ANCHOR | 2D DP | Match->diag+1 else max(up,left) |
| 9 | Longest Palindromic Substring | 5 | ⭐ HIGH VALUE | Expand/DP | Expand around center |
| 10 | Maximal Square | 221 | ⭐ HIGH VALUE | 2D DP | dp=1+min(top,left,diag) |
| 11 | Minimum Path Sum | 64 | 🧱 STANDARD | Grid DP | dp=min(top,left)+cell |
| 12 | Decode Ways | 91 | 🧱 STANDARD | DP on string | dp[i]=single+double choices |
| 13 | Triangle | 120 | 🧱 STANDARD | DP | Bottom-up min path |
| 14 | House Robber II | 213 | 🧱 STANDARD | Circular DP | Max(rob 0..n-2, 1..n-1) |
| 15 | Perfect Squares | 279 | 🧱 STANDARD | DP | dp[i]=min(dp[i-j^2]+1) |
| 16 | Target Sum | 494 | 🧱 STANDARD | Transform DP | Convert to subset sum count |
| 17 | Longest Palindromic Subsequence | 516 | 🧱 STANDARD | 2D DP | dp[i][j] longest in s[i:j] |
| 18 | Delete and Earn | 740 | 🧱 STANDARD | DP transform | Convert to house robber on values |
| 19 | Min Cost Climbing Stairs | 746 | 🧱 STANDARD | 1D DP | dp[i]=cost[i]+min(dp[i-1],dp[i-2]) |
| 20 | Min Cost For Tickets | 983 | 🧱 STANDARD | DP | dp[day]=min cost from day |
| 21 | Unique Paths II | 63 | 🎯 PRACTICE | Grid DP | Obstacle cells set dp=0 |
| 22 | Palindromic Substrings | 647 | 🎯 PRACTICE | Expand | Count palindromes from centers |
| 23 | Longest Valid Parentheses | 32 | 💡 ADVANCED | DP/stack | DP lengths or stack indices |
| 24 | Interleaving String | 97 | 💡 ADVANCED | 2D DP | dp[i][j] whether s3 formed |
| 25 | Maximum Product Subarray | 152 | 💡 ADVANCED | DP max/min | Track maxProd and minProd |
| 26 | Best Time to Buy and Sell Stock with Cooldown | 309 | 💡 ADVANCED | State DP | hold/sold/rest transitions |
| 27 | Combination Sum IV | 377 | 💡 ADVANCED | DP order matters | dp[i]+=dp[i-num] |
| 28 | Regular Expression Matching | 10 | 🧪 CHALLENGE | 2D DP | Handle '.' and '*' transitions |
| 29 | Wildcard Matching | 44 | 🧪 CHALLENGE | 2D DP | Handle '?' and '*' |
| 30 | Distinct Subsequences | 115 | 🧪 CHALLENGE | 2D DP | Count ways to form t from s |
| 31 | Burst Balloons | 312 | 🧪 CHALLENGE | Interval DP | Choose last balloon in interval |
---

## Phase 9 — Backtracking & Combinatorics 🌿

| # | Problem | LC# | Priority | Topics to know | Hybrid approach |
|---:|---|---:|---|---|---|
| 1 | Combination Sum | 39 | 🔑 ANCHOR | Backtracking | Reuse current; reduce target |
| 2 | Permutations | 46 | 🔑 ANCHOR | Backtracking | Swap/used-array generate orderings |
| 3 | Subsets | 78 | 🔑 ANCHOR | Backtracking | Choose/skip each element |
| 4 | Word Search | 79 | 🔑 ANCHOR | Grid backtracking | DFS with visited marking |
| 5 | Generate Parentheses | 22 | ⭐ HIGH VALUE | Backtracking | Add '(' if open<n, ')' if close<open |
| 6 | Palindrome Partitioning | 131 | ⭐ HIGH VALUE | Backtracking | Cut where substring is palindrome |
| 7 | Letter Combinations of a Phone Number | 17 | 🧱 STANDARD | Backtracking | DFS over digit->letters |
| 8 | Combination Sum II | 40 | 🧱 STANDARD | Backtracking | Use once; skip duplicates |
| 9 | Combinations | 77 | 🧱 STANDARD | Backtracking | For-loop with start index |
| 10 | Subsets II | 90 | 🧱 STANDARD | Duplicates | Sort; skip duplicates at level |
| 11 | Permutations II | 47 | 🎯 PRACTICE | Duplicates | Sort; used[] skip duplicates |
| 12 | N-Queens II | 52 | 🎯 PRACTICE | Counting | Backtracking count solutions |
| 13 | Restore IP Addresses | 93 | 🎯 PRACTICE | Backtracking | Try 1-3 digits per segment |
| 14 | Matchsticks to Square | 473 | 🎯 PRACTICE | Backtracking | Assign sticks to 4 buckets |
| 15 | Beautiful Arrangement | 526 | 🎯 PRACTICE | Backtracking | Place i with divisibility pruning |
| 16 | Path with Maximum Gold | 1219 | 🎯 PRACTICE | Backtracking | DFS collect; mark visited |
| 17 | Split a String Into the Max Number of Unique Substrings | 1593 | 🎯 PRACTICE | Backtracking | Use set; try all splits |
| 18 | Sudoku Solver | 37 | 💡 ADVANCED | Backtracking | Choose empty; try digits with constraints |
| 19 | N-Queens | 51 | 💡 ADVANCED | Backtracking | Place row-by-row with col/diag sets |
| 20 | Word Search II | 212 | 💡 ADVANCED | Trie + backtracking | DFS guided by trie |
| 21 | Expression Add Operators | 282 | 🧪 CHALLENGE | Backtracking | DFS with prev operand for * |
| 22 | Remove Invalid Parentheses | 301 | 🧪 CHALLENGE | BFS/backtracking | Remove minimal; dedupe states |
---

## Phase 10 — Heaps, Tries, Union-Find, Advanced DS 🧲🌲🤝

| # | Problem | LC# | Priority | Topics to know | Hybrid approach |
|---:|---|---:|---|---|---|
| 1 | Implement Trie (Prefix Tree) | 208 | 🔑 ANCHOR | Trie | Insert/search/prefix |
| 2 | Design Add and Search Words Data Structure | 211 | 🔑 ANCHOR | Trie + DFS | Wildcard '.' backtrack |
| 3 | Find Median from Data Stream | 295 | 🔑 ANCHOR | Two heaps | Max-heap left, min-heap right |
| 4 | Number of Connected Components in an Undirected Graph | 323 | 🔑 ANCHOR | DSU | Union edges; count components |
| 5 | Random Pick with Weight | 528 | ⭐ HIGH VALUE | Prefix + BS | Prefix sums; pick by random |
| 6 | Redundant Connection | 684 | ⭐ HIGH VALUE | DSU | First edge creating cycle |
| 7 | Accounts Merge | 721 | ⭐ HIGH VALUE | DSU | Union emails; group by root |
| 8 | Snapshot Array | 1146 | ⭐ HIGH VALUE | Design | Versioned arrays via maps |
| 9 | Design Underground System | 1396 | ⭐ HIGH VALUE | Design | Track check-in; compute averages |
| 10 | Replace Words | 648 | 🧱 STANDARD | Trie | Replace by shortest root prefix |
| 11 | Top K Frequent Words | 692 | 🧱 STANDARD | Heap + sort | Heap with custom ordering |
| 12 | K Closest Points to Origin | 973 | 🧱 STANDARD | Heap | Max-heap size k |
| 13 | Satisfiability of Equality Equations | 990 | 🧱 STANDARD | DSU | Union == then check != |
| 14 | Design HashSet | 705 | 🎯 PRACTICE | Buckets | Separate chaining |
| 15 | Design HashMap | 706 | 🎯 PRACTICE | Buckets | Separate chaining |
| 16 | Browser History | 1472 | 🎯 PRACTICE | Design | Stack/list with pointer |
| 17 | Find K Pairs with Smallest Sums | 373 | 💡 ADVANCED | Heap | Push (i,0) per row; expand |
| 18 | IPO | 502 | 💡 ADVANCED | Greedy + heaps | Max profit among feasible projects |
| 19 | Smallest Range Covering Elements from K Lists | 632 | 💡 ADVANCED | Heap | Track current max; pop min |
| 20 | Most Stones Removed with Same Row or Column | 947 | 💡 ADVANCED | DSU | Union rows/cols; answer n-components |
| 21 | Regions Cut By Slashes | 959 | 💡 ADVANCED | DSU | Union triangles; count regions |
| 22 | Stream of Characters | 1032 | 💡 ADVANCED | Trie | Reverse trie + stream suffix match |
| 23 | Smallest String With Swaps | 1202 | 💡 ADVANCED | DSU + sort | Group indices; sort chars per component |
| 24 | Palindrome Pairs | 336 | 🧪 CHALLENGE | Trie | Split words; match reversed prefixes |
| 25 | All O(1) Data Structure | 432 | 🧪 CHALLENGE | Design | DLL of counts + maps |
| 26 | Sliding Window Median | 480 | 🧪 CHALLENGE | Two heaps | Lazy deletion + rebalance |
| 27 | Making A Large Island | 827 | 🧪 CHALLENGE | DSU/DFS | Label islands; try flip 0 |
| 28 | Remove Max Number of Edges to Keep Graph Fully Traversable | 1579 | 🧪 CHALLENGE | DSU | Greedy union type3 then others |
---

## Phase 11 — Advanced Graphs + Design + Mixed Interview Core 🚀

| # | Problem | LC# | Priority | Topics to know | Hybrid approach |
|---:|---|---:|---|---|---|
| 1 | LRU Cache | 146 | 🔑 ANCHOR | Design | DLL + hashmap O(1) |
| 2 | Network Delay Time | 743 | 🔑 ANCHOR | Dijkstra | Min-heap relax edges |
| 3 | Minimum Cost to Connect All Points | 1584 | 🔑 ANCHOR | MST | Prim/Kruskal |
| 4 | Basic Calculator II | 227 | ⭐ HIGH VALUE | Stack parsing | Process * /; defer + - |
| 5 | Insert Delete GetRandom O(1) | 380 | ⭐ HIGH VALUE | Design | Array + hashmap + swap-delete |
| 6 | Cheapest Flights Within K Stops | 787 | ⭐ HIGH VALUE | BFS/DP | Layer by stops; relax costs |
| 7 | Time Based Key-Value Store | 981 | ⭐ HIGH VALUE | Design + BS | Store (ts,val) list; BS |
| 8 | Path With Minimum Effort | 1631 | ⭐ HIGH VALUE | Dijkstra/BS | Minimize max edge cost |
| 9 | Design Twitter | 355 | 🧱 STANDARD | Design + heap | Merge recent tweets by time |
| 10 | Path with Maximum Probability | 1514 | 🧱 STANDARD | Dijkstra variant | Max-heap by prob; multiply |
| 11 | Basic Calculator | 224 | 💡 ADVANCED | Stack | Parse +,-,(,) with sign stack |
| 12 | Swim in Rising Water | 778 | 💡 ADVANCED | Dijkstra/BS | Minimax path via PQ |
| 13 | Critical Connections in a Network | 1192 | 💡 ADVANCED | Tarjan | Low-link for bridges |
| 14 | Shortest Path in a Grid with Obstacles Elimination | 1293 | 💡 ADVANCED | BFS state | (r,c,k) visited |
| 15 | Minimum Cost to Make at Least One Valid Path in a Grid | 1368 | 💡 ADVANCED | 0-1 BFS | Edges cost 0/1 |
| 16 | LFU Cache | 460 | 🧪 CHALLENGE | Design | Freq lists + hashmap; O(1) ops |
| 17 | Parse Lisp Expression | 736 | 🧪 CHALLENGE | Parsing | Recursive descent / stack |
| 18 | Shortest Path Visiting All Nodes | 847 | 🧪 CHALLENGE | BFS bitmask | State (node,mask) |
| 19 | Minimum Cost to Reach Destination in Time | 1928 | 🧪 CHALLENGE | DP + Dijkstra | State (node,time) min cost |