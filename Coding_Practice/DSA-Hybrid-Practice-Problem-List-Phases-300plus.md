# 🎯 HYBRID PRACTICE PROBLEM LIST (PHASES) — 300+ LeetCode Problems
## Topic-aligned • Pattern-first • Interview-focused

> **A curated master practice curriculum of 338 unique LeetCode problems structured across 12 logical learning phases.**  
> **Philosophy:** Master the core problem patterns first through anchor problems, solidify variations, and elevate algorithmic maturity with advanced and challenge problems.  
> **Target Audience:** Software Engineers, Senior Developers, and Tech Leads preparing for product-company and Big-Tech interviews.

---

## 📊 Curriculum Overview & Statistics

### 🚥 Difficulty Distribution
| Difficulty | Problems | Percentage | Role in Curriculum |
| :---: | :---: | :---: | :--- |
| 🟢 **Easy** | 68 | 20.1% | Syntax fluency, base templates, foundational invariants |
| 🟡 **Medium** | 217 | 64.2% | The interview battleground (~70% of questions in live technical rounds) |
| 🔴 **Hard** | 53 | 15.7% | Multi-pattern synthesis, subtle invariant proofs, stretch challenges |
| **Total** | **338** | **100%** | **Comprehensive Full-Spectrum Mastery** |

### 🧭 Priority Legend
- 🔑 **ANCHOR** (62 Problems) — Master these first. The core canonical problem for every pattern.
- ⭐ **HIGH VALUE** (55 Problems) — High frequency in tech interviews; direct variations of anchor patterns.
- 🧱 **STANDARD** (67 Problems) — Core pattern coverage across fundamental scenarios.
- 🎯 **PRACTICE** (89 Problems) — Pattern reinforcement, edge-case handling, and speed building.
- 💡 **ADVANCED** (45 Problems) — Multi-pattern combinations and non-obvious state spaces.
- 🧪 **CHALLENGE** (20 Problems) — Stretch problems that test deep algorithmic intuition.

---

## 🗺️ Quick Phase Navigation

| Phase | Focus Topic | Total Problems | Anchor Count | Direct Jump |
| :---: | :--- | :---: | :---: | :---: |
| **Phase 0** | Foundations & Core Patterns 🧠 | 10 | 4 | [Jump to Phase 0](#phase-0--foundations--core-patterns-) |
| **Phase 1** | Arrays, Strings & Hashing 🧺 | 36 | 5 | [Jump to Phase 1](#phase-1--arrays-strings--hashing-) |
| **Phase 2** | Two Pointers & Sliding Window ↔️🪟 | 35 | 8 | [Jump to Phase 2](#phase-2--two-pointers--sliding-window-️) |
| **Phase 3** | Linked Lists + Stacks/Queues 🔗📚 | 38 | 6 | [Jump to Phase 3](#phase-3--linked-lists--stacksqueues-) |
| **Phase 4** | Trees & BST (DFS/BFS) 🌳 | 36 | 6 | [Jump to Phase 4](#phase-4--trees--bst-dfsbfs-) |
| **Phase 5** | Graphs (DFS/BFS + Topo) 🗺️ | 32 | 4 | [Jump to Phase 5](#phase-5--graphs-dfsbfs--topo-️) |
| **Phase 6** | Binary Search + Intervals 📏🔍 | 34 | 7 | [Jump to Phase 6](#phase-6--binary-search--intervals-) |
| **Phase 7** | Greedy & Monotonic Patterns 🪙 | 17 | 3 | [Jump to Phase 7](#phase-7--greedy--monotonic-patterns-) |
| **Phase 8** | Dynamic Programming (1D/2D/Knapsack) 🧩 | 31 | 8 | [Jump to Phase 8](#phase-8--dynamic-programming-1d2dknapsack-) |
| **Phase 9** | Backtracking & Combinatorics 🌿 | 22 | 4 | [Jump to Phase 9](#phase-9--backtracking--combinatorics-) |
| **Phase 10** | Heaps, Tries, Union-Find, Advanced DS 🧲🌲🤝 | 28 | 4 | [Jump to Phase 10](#phase-10--heaps-tries-union-find-advanced-ds-) |
| **Phase 11** | Advanced Graphs + Design + Mixed Core 🚀 | 19 | 3 | [Jump to Phase 11](#phase-11--advanced-graphs--design--mixed-interview-core-) |

---

## 🔥 ROI-First Execution Sprint Plan — 40 Highest-ROI Problems

> **Do these 40 problems first**, regardless of phase order. Ordered by pattern ROI: interview frequency × derivatives unlocked × learning efficiency. Complete each group before moving to the next.

### 🧭 ROI Sprint Navigation

| Sprint | Pattern | ROI | Hard Anchor | # Problems | Expected Unlock |
| :---: | :--- | :---: | :--- | :---: | :--- |
| **S1** | Two Pointers | 🔥🔥🔥 | Trapping Rain Water | 5 | All sorted-array pair/triplet problems |
| **S2** | Sliding Window | 🔥🔥🔥 | Minimum Window Substring | 6 | All contiguous subarray min/max/valid span |
| **S3** | Monotonic Stack/Queue | 🔥🔥🔥 | Largest Rectangle in Histogram | 5 | All nearest element + window extremes |
| **S4** | DP: 1D → 2D | 🔥🔥🔥 | Edit Distance | 7 | All DP variants: knapsack, state machine, interval |
| **S5** | Binary Search on Answer | 🔥🔥 | Median of Two Sorted Arrays | 5 | All "minimize max / maximize min" problems |
| **S6** | Heap / Two Heaps | 🔥🔥 | Find Median from Data Stream | 5 | All streaming top-K, K-way merge, scheduling |
| **S7** | Graph BFS/DFS/Topo | 🔥🔥 | Word Ladder | 7 | All graph traversal, component, ordering problems |  

### 📋 ROI Sprint — Full Problem Table (40 Problems)

| Sprint | # | Problem | LC# | Difficulty | Phase Ref | Key Invariant |
| :---: | :---: | :--- | :---: | :---: | :---: | :--- |
| **S1** | 1 | [Trapping Rain Water](https://leetcode.com/problems/trapping-rain-water/) | 42 | 🔴 Hard | Ph3 | `left_max` & `right_max` walls — Hard Anchor |
| **S1** | 2 | [Container With Most Water](https://leetcode.com/problems/container-with-most-water/) | 11 | 🟡 Medium | Ph2 | Move shorter pointer; prove no better action exists |
| **S1** | 3 | [3Sum](https://leetcode.com/problems/3sum/) | 15 | 🟡 Medium | Ph2 | Fix outer; two-pointer inner; skip duplicates |
| **S1** | 4 | [3Sum Closest](https://leetcode.com/problems/3sum-closest/) | 16 | 🟡 Medium | Ph2 | Same as 3Sum; track `abs(diff)` min |
| **S1** | 5 | [Two Sum II](https://leetcode.com/problems/two-sum-ii-input-array-is-sorted/) | 167 | 🟡 Medium | Ph2 | Baseline sorted-array opposing pointers |
| **S2** | 6 | [Minimum Window Substring](https://leetcode.com/problems/minimum-window-substring/) | 76 | 🔴 Hard | Ph2 | `matchCount` + two-map technique — Hard Anchor |
| **S2** | 7 | [Sliding Window Maximum](https://leetcode.com/problems/sliding-window-maximum/) | 239 | 🔴 Hard | Ph2 | Monotonic deque of indices; decreasing values |
| **S2** | 8 | [Longest Repeating Character Replacement](https://leetcode.com/problems/longest-repeating-character-replacement/) | 424 | 🟡 Medium | Ph2 | `(windowLen - maxFreq) ≤ k` invariant |
| **S2** | 9 | [Longest Substring Without Repeating Characters](https://leetcode.com/problems/longest-substring-without-repeating-characters/) | 3 | 🟡 Medium | Ph2 | Last-seen index map; jump left pointer |
| **S2** | 10 | [Find All Anagrams in a String](https://leetcode.com/problems/find-all-anagrams-in-a-string/) | 438 | 🟡 Medium | Ph2 | Fixed window; slide and compare counts |
| **S2** | 11 | [Permutation in String](https://leetcode.com/problems/permutation-in-string/) | 567 | 🟡 Medium | Ph2 | Fixed window frequency match |
| **S3** | 12 | [Largest Rectangle in Histogram](https://leetcode.com/problems/largest-rectangle-in-histogram/) | 84 | 🔴 Hard | Ph3 | Prev/next smaller via monotonic stack — Hard Anchor |
| **S3** | 13 | [Daily Temperatures](https://leetcode.com/problems/daily-temperatures/) | 739 | 🟡 Medium | Ph3 | One-directional NGE via monotonic stack |
| **S3** | 14 | [Remove K Digits](https://leetcode.com/problems/remove-k-digits/) | 402 | 🟡 Medium | Ph3 | Monotonic stack for lex-smallest result |
| **S3** | 15 | [Sum of Subarray Minimums](https://leetcode.com/problems/sum-of-subarray-minimums/) | 907 | 🟡 Medium | Ph3 | Contribution = left-span × right-span per element |
| **S3** | 16 | [Online Stock Span](https://leetcode.com/problems/online-stock-span/) | 901 | 🟡 Medium | Ph3 | Accumulate span over dominated elements |
| **S4** | 17 | [Edit Distance](https://leetcode.com/problems/edit-distance/) | 72 | 🔴 Hard | Ph8 | 3-way recurrence: insert/delete/replace — Hard Anchor |
| **S4** | 18 | [Longest Common Subsequence](https://leetcode.com/problems/longest-common-subsequence/) | 1143 | 🟡 Medium | Ph8 | 2D prefix match grid |
| **S4** | 19 | [Coin Change](https://leetcode.com/problems/coin-change/) | 322 | 🟡 Medium | Ph8 | Unbounded knapsack; iterate forwards |
| **S4** | 20 | [Partition Equal Subset Sum](https://leetcode.com/problems/partition-equal-subset-sum/) | 416 | 🟡 Medium | Ph8 | 0-1 knapsack; iterate backwards |
| **S4** | 21 | [Longest Increasing Subsequence](https://leetcode.com/problems/longest-increasing-subsequence/) | 300 | 🟡 Medium | Ph8 | $O(N^2)$ DP → $O(N \log N)$ patience sort |
| **S4** | 22 | [Word Break](https://leetcode.com/problems/word-break/) | 139 | 🟡 Medium | Ph8 | `dp[i] = any dp[j] && dict.Contains(s[j..i])` |
| **S4** | 23 | [Burst Balloons](https://leetcode.com/problems/burst-balloons/) | 312 | 🔴 Hard | Ph8 | Interval DP: choose *last* balloon popped in range |
| **S5** | 24 | [Median of Two Sorted Arrays](https://leetcode.com/problems/median-of-two-sorted-arrays/) | 4 | 🔴 Hard | Ph0 | Partition cut: `leftA + leftB = rightA + rightB` — Hard Anchor |
| **S5** | 25 | [Koko Eating Bananas](https://leetcode.com/problems/koko-eating-bananas/) | 875 | 🟡 Medium | Ph6 | `CanFinish(speed)` feasibility predicate |
| **S5** | 26 | [Capacity to Ship Packages](https://leetcode.com/problems/capacity-to-ship-packages-within-d-days/) | 1011 | 🟡 Medium | Ph6 | Same predicate template |
| **S5** | 27 | [Kth Smallest in Sorted Matrix](https://leetcode.com/problems/kth-smallest-element-in-a-sorted-matrix/) | 378 | 🟡 Medium | Ph6 | Count elements ≤ mid per row |
| **S5** | 28 | [Split Array Largest Sum](https://leetcode.com/problems/split-array-largest-sum/) | 410 | 🔴 Hard | Ph6 | BS on answer + greedy partition simulation |
| **S6** | 29 | [Find Median from Data Stream](https://leetcode.com/problems/find-median-from-data-stream/) | 295 | 🔴 Hard | Ph10 | Two balanced heaps — Hard Anchor |
| **S6** | 30 | [Merge K Sorted Lists](https://leetcode.com/problems/merge-k-sorted-lists/) | 23 | 🔴 Hard | Ph3 | Min-heap of K list heads; pop & advance |
| **S6** | 31 | [Top K Frequent Elements](https://leetcode.com/problems/top-k-frequent-elements/) | 347 | 🟡 Medium | Ph1 | Freq count → min-heap size K |
| **S6** | 32 | [K Closest Points to Origin](https://leetcode.com/problems/k-closest-points-to-origin/) | 973 | 🟡 Medium | Ph10 | Max-heap size K; keep smallest K |
| **S6** | 33 | [Meeting Rooms II](https://leetcode.com/problems/meeting-rooms-ii/) | 253 | 🟡 Medium | Ph6 | Min-heap end times; room reuse logic |
| **S7** | 34 | [Word Ladder](https://leetcode.com/problems/word-ladder/) | 127 | 🔴 Hard | Ph5 | Implicit graph BFS via wildcard patterns — Hard Anchor |
| **S7** | 35 | [Rotting Oranges](https://leetcode.com/problems/rotting-oranges/) | 994 | 🟡 Medium | Ph5 | Multi-source BFS; layers = time |
| **S7** | 36 | [Number of Islands](https://leetcode.com/problems/number-of-islands/) | 200 | 🟡 Medium | Ph5 | Grid DFS/BFS; sink visited land |
| **S7** | 37 | [Course Schedule](https://leetcode.com/problems/course-schedule/) | 207 | 🟡 Medium | Ph5 | Kahn's: indegrees + queue; cycle = unprocessed nodes |
| **S7** | 38 | [Pacific Atlantic Water Flow](https://leetcode.com/problems/pacific-atlantic-water-flow/) | 417 | 🟡 Medium | Ph5 | Reverse reachability from both oceans |
| **S7** | 39 | [Network Delay Time](https://leetcode.com/problems/network-delay-time/) | 743 | 🟡 Medium | Ph11 | Dijkstra's with min-heap; relax edges |
| **S7** | 40 | [Redundant Connection](https://leetcode.com/problems/redundant-connection/) | 684 | 🟡 Medium | Ph10 | Union-Find: `Find(u) == Find(v)` → cycle |

> [!NOTE]
> After completing all 40 Sprint problems, you will have mastered the **core invariants** of every high-frequency pattern. The remaining ~300 problems in the phases below become variations — solvable by applying the same mental model to a new domain.

---

## 🧠 Hard-First Pattern Mastery Cards

> For each pattern, the Hard anchor teaches the hardest invariant. Every subsequent Medium/Easy problem is solved by *recognizing the same invariant in disguise*.

---

### 🃏 Card 1 — Two Pointers

| | |
|:---|:---|
| **Hard Anchor** | [Trapping Rain Water](https://leetcode.com/problems/trapping-rain-water/) 🔴 (LC 42) |
| **Core Invariant** | `water[i] = min(left_max, right_max) - height[i]`. Two opposing pointers update the limiting wall. Move the pointer at the shorter wall — because the taller wall's contribution can only increase further inward. |
| **Trigger Cue** | *Sorted array + pair/triplet satisfying condition + optimize/count* |
| **Medium Derivatives** | [Container With Most Water](https://leetcode.com/problems/container-with-most-water/) · [3Sum](https://leetcode.com/problems/3sum/) · [4Sum](https://leetcode.com/problems/4sum/) · [Two Sum II](https://leetcode.com/problems/two-sum-ii-input-array-is-sorted/) |
| **Easy Warmup** | [Valid Palindrome](https://leetcode.com/problems/valid-palindrome/) · [Reverse String](https://leetcode.com/problems/reverse-string/) |
| **What Clicking This Unlocks** | You will never struggle with sorted-array pair problems again. The proof that "moving the shorter side is the only valid action" generalizes to all two-pointer elimination arguments. |

---

### 🃏 Card 2 — Sliding Window

| | |
|:---|:---|
| **Hard Anchor** | [Minimum Window Substring](https://leetcode.com/problems/minimum-window-substring/) 🔴 (LC 76) |
| **Core Invariant** | Expand `right` until all target characters are covered (`matchCount == required`). Then shrink `left` to minimize window while maintaining coverage. Track best window. |
| **Trigger Cue** | *Contiguous subarray/substring + find min/max/valid span + frequency constraint* |
| **Medium Derivatives** | [Longest Repeating Char Replacement](https://leetcode.com/problems/longest-repeating-character-replacement/) · [Longest Substring Without Repeating](https://leetcode.com/problems/longest-substring-without-repeating-characters/) · [Permutation in String](https://leetcode.com/problems/permutation-in-string/) · [Find All Anagrams](https://leetcode.com/problems/find-all-anagrams-in-a-string/) |
| **Easy Warmup** | [Maximum Average Subarray I](https://leetcode.com/problems/maximum-average-subarray-i/) · [Maximum Number of Vowels in Substring](https://leetcode.com/problems/maximum-number-of-vowels-in-a-substring-of-given-length/) |
| **What Clicking This Unlocks** | The expand-then-shrink rhythm becomes automatic. `matchCount` counter for multi-character tracking generalizes to any "satisfy K conditions" window problem. |

---

### 🃏 Card 3 — Monotonic Stack & Queue

| | |
|:---|:---|
| **Hard Anchor** | [Largest Rectangle in Histogram](https://leetcode.com/problems/largest-rectangle-in-histogram/) 🔴 (LC 84) |
| **Core Invariant** | Maintain a strictly increasing stack of bar indices. When `heights[i] < heights[stack.Peek()]`, pop and compute area: `width = i - stack.Peek() - 1` (or `i` if stack empty). |
| **Trigger Cue** | *Nearest greater/smaller element · Window min/max in $O(N)$ · "How far can I extend?"* |
| **Medium Derivatives** | [Sliding Window Maximum](https://leetcode.com/problems/sliding-window-maximum/) · [Daily Temperatures](https://leetcode.com/problems/daily-temperatures/) · [Remove K Digits](https://leetcode.com/problems/remove-k-digits/) · [Sum of Subarray Minimums](https://leetcode.com/problems/sum-of-subarray-minimums/) |
| **Easy Warmup** | [Next Greater Element I](https://leetcode.com/problems/next-greater-element-i/) |
| **What Clicking This Unlocks** | You understand why the stack must be monotonic — breaking monotonicity means the popped element found its boundary. Deque variant for window min/max follows directly. |

---

### 🃏 Card 4 — Dynamic Programming

| | |
|:---|:---|
| **Hard Anchor** | [Edit Distance](https://leetcode.com/problems/edit-distance/) 🔴 (LC 72) |
| **Core Invariant** | `dp[i][j]` = min ops to convert `s1[0..i-1]` to `s2[0..j-1]`. If chars match: `dp[i-1][j-1]`. Else: `1 + min(dp[i-1][j], dp[i][j-1], dp[i-1][j-1])` for delete/insert/replace. |
| **Trigger Cue** | *Overlapping subproblems + optimal substructure + "minimum/maximum/count ways"* |
| **Medium Derivatives** | [LCS](https://leetcode.com/problems/longest-common-subsequence/) · [Coin Change](https://leetcode.com/problems/coin-change/) · [Partition Subset Sum](https://leetcode.com/problems/partition-equal-subset-sum/) · [LIS](https://leetcode.com/problems/longest-increasing-subsequence/) · [Word Break](https://leetcode.com/problems/word-break/) |
| **Hard Derivatives** | [Burst Balloons](https://leetcode.com/problems/burst-balloons/) · [Regular Expression Matching](https://leetcode.com/problems/regular-expression-matching/) |
| **What Clicking This Unlocks** | The 3-way transition is the hardest DP state. Once mastered, 1D DP (House Robber), knapsack (Coin Change), and state machines (Stock Cooldown) all feel simpler. |

---

### 🃏 Card 5 — Binary Search on Answer Space

| | |
|:---|:---|
| **Hard Anchor** | [Median of Two Sorted Arrays](https://leetcode.com/problems/median-of-two-sorted-arrays/) 🔴 (LC 4) |
| **Core Invariant** | Binary search on partition cut `i` in array A such that `leftA + leftB = rightA + rightB`. Median emerges from border elements of the two partitions. |
| **Trigger Cue** | *"Minimize the maximum" or "maximize the minimum" over a continuous range with a monotonic feasibility check* |
| **Medium Derivatives** | [Koko Eating Bananas](https://leetcode.com/problems/koko-eating-bananas/) · [Capacity to Ship Packages](https://leetcode.com/problems/capacity-to-ship-packages-within-d-days/) · [Magnetic Force Between Two Balls](https://leetcode.com/problems/magnetic-force-between-two-balls/) |
| **Hard Derivatives** | [Split Array Largest Sum](https://leetcode.com/problems/split-array-largest-sum/) · [Minimum Number of Days to Make Bouquets](https://leetcode.com/problems/minimum-number-of-days-to-make-m-bouquets/) |
| **What Clicking This Unlocks** | The mental model: "if I can check feasibility for a given answer in $O(N)$, I can find the optimal answer in $O(N \log N)$." Applies to dozens of optimization problems. |

---

### 🃏 Card 6 — Heap / Two Heaps

| | |
|:---|:---|
| **Hard Anchor** | [Find Median from Data Stream](https://leetcode.com/problems/find-median-from-data-stream/) 🔴 (LC 295) |
| **Core Invariant** | MaxHeap (lower half) + MinHeap (upper half). After each insert, rebalance so `|maxH.Count - minH.Count| ≤ 1`. Median = top of larger heap or average of both tops. |
| **Trigger Cue** | *Streaming data + dynamic top-K + running median + K-way merge* |
| **Medium Derivatives** | [Top K Frequent Elements](https://leetcode.com/problems/top-k-frequent-elements/) · [K Closest Points](https://leetcode.com/problems/k-closest-points-to-origin/) · [Kth Largest Element](https://leetcode.com/problems/kth-largest-element-in-an-array/) · [Meeting Rooms II](https://leetcode.com/problems/meeting-rooms-ii/) |
| **Hard Derivatives** | [Merge K Sorted Lists](https://leetcode.com/problems/merge-k-sorted-lists/) · [Sliding Window Median](https://leetcode.com/problems/sliding-window-median/) |
| **What Clicking This Unlocks** | Any "maintain partial ordering over streaming data" problem uses one of: min-heap-size-K, max-heap-size-K, or two balanced heaps. |

---

### 🃏 Card 7 — Graph BFS / DFS / Topological Sort

| | |
|:---|:---|
| **Hard Anchor** | [Word Ladder](https://leetcode.com/problems/word-ladder/) 🔴 (LC 127) |
| **Core Invariant** | Build implicit graph via wildcard patterns (`h*t` buckets). BFS guarantees shortest transformation path. Bidirectional BFS reduces branch factor from $O(B^D)$ to $O(B^{D/2})$. |
| **Trigger Cue** | *Shortest path on unweighted graph · Connected components · Dependency ordering* |
| **Medium Derivatives** | [Rotting Oranges](https://leetcode.com/problems/rotting-oranges/) · [Number of Islands](https://leetcode.com/problems/number-of-islands/) · [Course Schedule](https://leetcode.com/problems/course-schedule/) · [Pacific Atlantic Water Flow](https://leetcode.com/problems/pacific-atlantic-water-flow/) |
| **Hard Derivatives** | [Alien Dictionary](https://leetcode.com/problems/alien-dictionary/) · [Critical Connections](https://leetcode.com/problems/critical-connections-in-a-network/) |
| **What Clicking This Unlocks** | Every graph problem becomes: (1) choose BFS/DFS/Topo, (2) define visited state correctly, (3) process component-by-component or layer-by-layer. |

---

### 🃏 Card 8 — Backtracking + Pruning

| | |
|:---|:---|
| **Hard Anchor** | [Word Search II](https://leetcode.com/problems/word-search-ii/) 🔴 (LC 212) |
| **Core Invariant** | Build Trie of all words. DFS on grid guided by Trie nodes — prune branches where no word continues. When `isEnd`, record word and remove from Trie to avoid duplicates. |
| **Trigger Cue** | *Generate all combinations/permutations/paths · Constraint satisfaction · "Find all valid X"* |
| **Medium Derivatives** | [Word Search](https://leetcode.com/problems/word-search/) · [Combination Sum](https://leetcode.com/problems/combination-sum/) · [Subsets](https://leetcode.com/problems/subsets/) · [Permutations](https://leetcode.com/problems/permutations/) · [Palindrome Partitioning](https://leetcode.com/problems/palindrome-partitioning/) |
| **Hard Derivatives** | [N-Queens](https://leetcode.com/problems/n-queens/) · [Sudoku Solver](https://leetcode.com/problems/sudoku-solver/) |
| **What Clicking This Unlocks** | The Choose → Recurse → Undo template becomes mechanical. The hard skill is pruning: sort candidates, break early, skip duplicates. |

---

### 🃏 Card 9 — Tree DFS (Bottom-Up DP)

| | |
|:---|:---|
| **Hard Anchor** | [Binary Tree Maximum Path Sum](https://leetcode.com/problems/binary-tree-maximum-path-sum/) 🔴 (LC 124) |
| **Core Invariant** | Each node returns `max(0, leftGain, rightGain) + val` to parent (best downward path). But locally: `candidate = leftGain + rightGain + val` updates global `maxPath`. |
| **Trigger Cue** | *"What does each subtree compute and return to its parent?"* |
| **Medium Derivatives** | [Lowest Common Ancestor](https://leetcode.com/problems/lowest-common-ancestor-of-a-binary-tree/) · [Diameter of Binary Tree](https://leetcode.com/problems/diameter-of-binary-tree/) · [Validate BST](https://leetcode.com/problems/validate-binary-search-tree/) · [Path Sum III](https://leetcode.com/problems/path-sum-iii/) |
| **Hard Derivatives** | [Serialize and Deserialize Binary Tree](https://leetcode.com/problems/serialize-and-deserialize-binary-tree/) · [Binary Tree Cameras](https://leetcode.com/problems/binary-tree-cameras/) |
| **What Clicking This Unlocks** | Every tree problem has one question: "What does my subtree return to its parent?" Answer this and you have the recursive solution structure. |

---

### 🃏 Card 10 — Linked List: Multi-Step Patterns

| | |
|:---|:---|
| **Hard Anchor** | [Reverse Nodes in K-Group](https://leetcode.com/problems/reverse-nodes-in-k-group/) 🔴 (LC 25) |
| **Core Invariant** | Count K nodes ahead. Reverse K-node segment using three-pointer method. Connect `tail` of reversed segment to recursive call for the rest. |
| **Trigger Cue** | *Linked list + restructuring + in-place pointer manipulation* |
| **Medium Derivatives** | [Reorder List](https://leetcode.com/problems/reorder-list/) · [Remove Nth Node From End](https://leetcode.com/problems/remove-nth-node-from-end-of-list/) · [Copy List with Random Pointer](https://leetcode.com/problems/copy-list-with-random-pointer/) · [Sort List](https://leetcode.com/problems/sort-list/) |
| **Easy Warmup** | [Reverse Linked List](https://leetcode.com/problems/reverse-linked-list/) · [Linked List Cycle](https://leetcode.com/problems/linked-list-cycle/) |
| **What Clicking This Unlocks** | Sentinel/dummy node eliminates edge cases. Fast/slow pointer finds mid and cycles. Three-pointer reversal is the foundation of all structural transformations. |

---

### 🃏 Card 11 — Union-Find (Disjoint Set Union)

| | |
|:---|:---|
| **Hard Anchor** | [Critical Connections in a Network](https://leetcode.com/problems/critical-connections-in-a-network/) 🔴 (LC 1192) |
| **Core Invariant** | Tarjan's bridge algorithm: `low[v] = min(disc[v], min low[children], min disc[back-edge neighbors])`. Edge `(u,v)` is a bridge if `low[v] > disc[u]`. |
| **Trigger Cue** | *Dynamic connectivity · Cycle detection in undirected graph · Minimum spanning tree* |
| **Medium Derivatives** | [Redundant Connection](https://leetcode.com/problems/redundant-connection/) · [Number of Provinces](https://leetcode.com/problems/number-of-provinces/) · [Accounts Merge](https://leetcode.com/problems/accounts-merge/) · [Satisfiability of Equality Equations](https://leetcode.com/problems/satisfiability-of-equality-equations/) |
| **Hard Derivatives** | [Remove Max Edges to Keep Graph Traversable](https://leetcode.com/problems/remove-max-number-of-edges-to-keep-graph-fully-traversable/) · [Making A Large Island](https://leetcode.com/problems/making-a-large-island/) |
| **What Clicking This Unlocks** | Union-Find with path compression + union by rank achieves $O(\alpha(N))$ ≈ $O(1)$ amortized. Recognize: "dynamic connectivity" = Union-Find, not BFS/DFS. |

---

### 🃏 Card 12 — Prefix Sum + HashMap

| | |
|:---|:---|
| **Hard Anchor** | [Subarray Sum Equals K](https://leetcode.com/problems/subarray-sum-equals-k/) 🟡 (LC 560) |
| **Core Invariant** | `sum(i..j) = prefix[j] - prefix[i-1]`. Store prefix sum frequency in `Dictionary<int,int>`. For each `prefix[j]`, check if `prefix[j] - k` was seen before → that count of subarrays end at `j`. |
| **Trigger Cue** | *Range sum queries · Count subarrays with sum/product equal to K · Balance between left and right halves* |
| **Medium Derivatives** | [Contiguous Array](https://leetcode.com/problems/contiguous-array/) · [Continuous Subarray Sum](https://leetcode.com/problems/continuous-subarray-sum/) · [Subarray Sums Divisible by K](https://leetcode.com/problems/subarray-sums-divisible-by-k/) · [Path Sum III](https://leetcode.com/problems/path-sum-iii/) |
| **Easy Warmup** | [Running Sum of 1d Array](https://leetcode.com/problems/running-sum-of-1d-array/) · [Find Pivot Index](https://leetcode.com/problems/find-pivot-index/) |
| **What Clicking This Unlocks** | The `prefix[j] - k` complement lookup turns $O(N^2)$ subarray search into $O(N)$ hash map scan. Applies to all "count subarrays satisfying sum condition" problems. |

---

## Phase 0 — Foundations & Core Patterns 🧠

| # | Problem | LC# | Difficulty | Priority | Pattern Tags | Hybrid Approach & Core Invariant |
| :---: | :--- | :---: | :---: | :---: | :--- | :--- |
| 1 | [Valid Parentheses](https://leetcode.com/problems/valid-parentheses/) | 20 | 🟢 Easy | 🔑 ANCHOR | `#stack` | Push opens; pop/match closes |
| 2 | [Maximum Subarray](https://leetcode.com/problems/maximum-subarray/) | 53 | 🟡 Medium | 🔑 ANCHOR | `#kadanes-algorithm` `#greedy` `#dynamic-programming` | Track best ending here |
| 3 | [Climbing Stairs](https://leetcode.com/problems/climbing-stairs/) | 70 | 🟢 Easy | 🔑 ANCHOR | `#1d-dp` | Dp[i]=dp[i-1]+dp[i-2] |
| 4 | [Binary Search](https://leetcode.com/problems/binary-search/) | 704 | 🟢 Easy | 🔑 ANCHOR | `#binary-search` `#search-template` `#loop-invariant` | Classic BS template; shrink search space |
| 5 | [Single Number](https://leetcode.com/problems/single-number/) | 136 | 🟢 Easy | ⭐ HIGH VALUE | `#bit-manipulation` | XOR all; duplicates cancel |
| 6 | [Counting Bits](https://leetcode.com/problems/counting-bits/) | 338 | 🟢 Easy | ⭐ HIGH VALUE | `#dynamic-programming` `#bit-manipulation` | Dp[i]=dp[i>>1]+(i&1) |
| 7 | [Power of Two](https://leetcode.com/problems/power-of-two/) | 231 | 🟢 Easy | 🎯 PRACTICE | `#bit-manipulation` | N>0 and n&(n-1)==0 |
| 8 | [First Bad Version](https://leetcode.com/problems/first-bad-version/) | 278 | 🟢 Easy | 🎯 PRACTICE | `#binary-search` `#boundary-search` `#lower-bound` | BS for leftmost true |
| 9 | [Find the Duplicate Number](https://leetcode.com/problems/find-the-duplicate-number/) | 287 | 🟡 Medium | 💡 ADVANCED | `#binary-search-on-answer` | Floyd cycle on values-as-next |
| 10 | [Median of Two Sorted Arrays](https://leetcode.com/problems/median-of-two-sorted-arrays/) | 4 | 🔴 Hard | 🧪 CHALLENGE | `#binary-search` | Partition arrays; median from borders |

> [!TIP]
> ### 💡 Phase 0 Milestone — Foundations & Algorithmic Invariants
> - **Bit Manipulation Invariant:** `n & (n - 1)` clears the lowest set bit. Duplicates cancel via $x \oplus x = 0$.
> - **Kadane's Principle:** Local optimum decision: extend previous contiguous subarray or discard negative history and start fresh.
> - **Binary Search Template:** Invariant search space $[L, R]$ where condition partitions space into monotonically valid / invalid halves.

---

## Phase 1 — Arrays, Strings & Hashing 🧺

| # | Problem | LC# | Difficulty | Priority | Pattern Tags | Hybrid Approach & Core Invariant |
| :---: | :--- | :---: | :---: | :---: | :--- | :--- |
| 1 | [Two Sum](https://leetcode.com/problems/two-sum/) | 1 | 🟢 Easy | 🔑 ANCHOR | `#hash-map` | Store seen; check `target - x` |
| 2 | [Group Anagrams](https://leetcode.com/problems/group-anagrams/) | 49 | 🟡 Medium | 🔑 ANCHOR | `#hash-map` | Key by sorted string / counts |
| 3 | [Product of Array Except Self](https://leetcode.com/problems/product-of-array-except-self/) | 238 | 🟡 Medium | 🔑 ANCHOR | `#prefix-suffix-product` `#array` `#space-optimization` | Two passes; no division |
| 4 | [Top K Frequent Elements](https://leetcode.com/problems/top-k-frequent-elements/) | 347 | 🟡 Medium | 🔑 ANCHOR | `#hash-map` `#heap` | Count then select top-k |
| 5 | [Subarray Sum Equals K](https://leetcode.com/problems/subarray-sum-equals-k/) | 560 | 🟡 Medium | 🔑 ANCHOR | `#prefix-sum` `#hash-map` | Count prefixSum-k occurrences |
| 6 | [Rotate Image](https://leetcode.com/problems/rotate-image/) | 48 | 🟡 Medium | ⭐ HIGH VALUE | `#matrix-grid` | Transpose then reverse rows |
| 7 | [Set Matrix Zeroes](https://leetcode.com/problems/set-matrix-zeroes/) | 73 | 🟡 Medium | ⭐ HIGH VALUE | `#matrix-grid` | Use first row/col as flags |
| 8 | [Longest Consecutive Sequence](https://leetcode.com/problems/longest-consecutive-sequence/) | 128 | 🟡 Medium | ⭐ HIGH VALUE | `#hash-set` | Start only at sequence heads |
| 9 | [Majority Element](https://leetcode.com/problems/majority-element/) | 169 | 🟢 Easy | ⭐ HIGH VALUE | `#boyer-moore-voting` `#frequency` `#array` | Cancel pairs; keep candidate |
| 10 | [Kth Largest Element in an Array](https://leetcode.com/problems/kth-largest-element-in-an-array/) | 215 | 🟡 Medium | ⭐ HIGH VALUE | `#heap` | Min-heap size k or quickselect |
| 11 | [Encode and Decode Strings](https://leetcode.com/problems/encode-and-decode-strings/) | 271 | 🟡 Medium | ⭐ HIGH VALUE | `#system-design` | Length-prefix encoding |
| 12 | [Contiguous Array](https://leetcode.com/problems/contiguous-array/) | 525 | 🟡 Medium | ⭐ HIGH VALUE | `#prefix-sum` | Map diff(count1-count0)->first index |
| 13 | [Valid Sudoku](https://leetcode.com/problems/valid-sudoku/) | 36 | 🟡 Medium | 🧱 STANDARD | `#hash-map` | Row/col/box sets |
| 14 | [Spiral Matrix](https://leetcode.com/problems/spiral-matrix/) | 54 | 🟡 Medium | 🧱 STANDARD | `#matrix-grid` `#boundary-shrink` | 4 pointers; traverse layers |
| 15 | [Contains Duplicate](https://leetcode.com/problems/contains-duplicate/) | 217 | 🟢 Easy | 🧱 STANDARD | `#hash-set` | Insert; detect repeat |
| 16 | [Valid Anagram](https://leetcode.com/problems/valid-anagram/) | 242 | 🟢 Easy | 🧱 STANDARD | `#hash-map` `#frequency-map` `#string` | Frequency map or sort |
| 17 | [Find All Duplicates in an Array](https://leetcode.com/problems/find-all-duplicates-in-an-array/) | 442 | 🟡 Medium | 🧱 STANDARD | `#array-indexing` `#in-place-marking` | Negate; if already neg => dup |
| 18 | [Find All Numbers Disappeared in an Array](https://leetcode.com/problems/find-all-numbers-disappeared-in-an-array/) | 448 | 🟢 Easy | 🧱 STANDARD | `#array-indexing` `#in-place-marking` | Negate at index=abs(x)-1 |
| 19 | [Sort Characters By Frequency](https://leetcode.com/problems/sort-characters-by-frequency/) | 451 | 🟡 Medium | 🧱 STANDARD | `#hash-map` `#heap` | Count then sort by freq |
| 20 | [Continuous Subarray Sum](https://leetcode.com/problems/continuous-subarray-sum/) | 523 | 🟡 Medium | 🧱 STANDARD | `#prefix-sum` `#modulo-arithmetic` | Seen mod; subarray len>=2 |
| 21 | [Reorganize String](https://leetcode.com/problems/reorganize-string/) | 767 | 🟡 Medium | 🧱 STANDARD | `#heap` `#greedy` | Place most frequent with cooldown |
| 22 | [Subarray Sums Divisible by K](https://leetcode.com/problems/subarray-sums-divisible-by-k/) | 974 | 🟡 Medium | 🧱 STANDARD | `#prefix-sum` `#modulo-arithmetic` | Count equal mods |
| 23 | [Happy Number](https://leetcode.com/problems/happy-number/) | 202 | 🟢 Easy | 🎯 PRACTICE | `#cycle-detection` | Use set or Floyd on digit-square |
| 24 | [Isomorphic Strings](https://leetcode.com/problems/isomorphic-strings/) | 205 | 🟢 Easy | 🎯 PRACTICE | `#hash-map` `#bijection-mapping` | Map s->t and t->s |
| 25 | [Missing Number](https://leetcode.com/problems/missing-number/) | 268 | 🟢 Easy | 🎯 PRACTICE | `#bit-manipulation` | XOR indices and values |
| 26 | [Word Pattern](https://leetcode.com/problems/word-pattern/) | 290 | 🟢 Easy | 🎯 PRACTICE | `#hash-map` `#bijection-mapping` | Pattern↔word bijection |
| 27 | [Intersection of Two Arrays](https://leetcode.com/problems/intersection-of-two-arrays/) | 349 | 🟢 Easy | 🎯 PRACTICE | `#hash-set` | Set intersection |
| 28 | [Intersection of Two Arrays II](https://leetcode.com/problems/intersection-of-two-arrays-ii/) | 350 | 🟢 Easy | 🎯 PRACTICE | `#hash-map` `#frequency-map` | Count with map then output |
| 29 | [Ransom Note](https://leetcode.com/problems/ransom-note/) | 383 | 🟢 Easy | 🎯 PRACTICE | `#hash-map` `#frequency-map` | Count magazine letters |
| 30 | [First Unique Character in a String](https://leetcode.com/problems/first-unique-character-in-a-string/) | 387 | 🟢 Easy | 🎯 PRACTICE | `#hash-map` `#frequency-map` | Count then first count==1 |
| 31 | [Maximum Product of Three Numbers](https://leetcode.com/problems/maximum-product-of-three-numbers/) | 628 | 🟢 Easy | 🎯 PRACTICE | `#sorting` | Pick top3 or 2min*max |
| 32 | [Backspace String Compare](https://leetcode.com/problems/backspace-string-compare/) | 844 | 🟢 Easy | 🎯 PRACTICE | `#two-pointers` `#stack` | Simulate or skip-back pointers |
| 33 | [Sort Array By Parity](https://leetcode.com/problems/sort-array-by-parity/) | 905 | 🟢 Easy | 🎯 PRACTICE | `#two-pointers` | Partition even/odd |
| 34 | [Sort Array By Parity II](https://leetcode.com/problems/sort-array-by-parity-ii/) | 922 | 🟢 Easy | 🎯 PRACTICE | `#two-pointers` | Place evens on even idx |
| 35 | [Reorder Data in Log Files](https://leetcode.com/problems/reorder-data-in-log-files/) | 937 | 🟡 Medium | 🎯 PRACTICE | `#sorting` | Custom comparator; stable |
| 36 | [Maximum Erasure Value](https://leetcode.com/problems/maximum-erasure-value/) | 1695 | 🟡 Medium | 🎯 PRACTICE | `#sliding-window` `#hash-set` | Unique window sum max |

> [!TIP]
> ### 💡 Phase 1 Milestone — Frequency Maps & Index Marking
> - **Complement Lookup:** Store history in `Dictionary<TKey, TValue>` to convert $O(N^2)$ pair searches into $O(1)$ amortized probes.
> - **Canonical Representation:** When grouping equivalence classes (e.g. anagrams), derive a unique immutable key (sorted string or 26-element tuple).
> - **In-Place Sign Marking:** When array values are bounded in $[1, N]$, use index `abs(val) - 1` signs to achieve $O(1)$ auxiliary space.

---

## Phase 2 — Two Pointers & Sliding Window ↔️🪟

| # | Problem | LC# | Difficulty | Priority | Pattern Tags | Hybrid Approach & Core Invariant |
| :---: | :--- | :---: | :---: | :---: | :--- | :--- |
| 1 | [Longest Substring Without Repeating Characters](https://leetcode.com/problems/longest-substring-without-repeating-characters/) | 3 | 🟡 Medium | 🔑 ANCHOR | `#sliding-window` | Expand; shrink on duplicates |
| 2 | [Container With Most Water](https://leetcode.com/problems/container-with-most-water/) | 11 | 🟡 Medium | 🔑 ANCHOR | `#greedy` | Move shorter height inward |
| 3 | [3Sum](https://leetcode.com/problems/3sum/) | 15 | 🟡 Medium | 🔑 ANCHOR | `#two-pointers` | Fix i; scan remaining; skip dups |
| 4 | [Minimum Window Substring](https://leetcode.com/problems/minimum-window-substring/) | 76 | 🔴 Hard | 🔑 ANCHOR | `#sliding-window` | Expand to satisfy; shrink to min |
| 5 | [Valid Palindrome](https://leetcode.com/problems/valid-palindrome/) | 125 | 🟢 Easy | 🔑 ANCHOR | `#two-pointers` | Skip non-alnum; compare |
| 6 | [Two Sum II - Input Array Is Sorted](https://leetcode.com/problems/two-sum-ii-input-array-is-sorted/) | 167 | 🟡 Medium | 🔑 ANCHOR | `#two-pointers` | Move ends based on sum |
| 7 | [Sliding Window Maximum](https://leetcode.com/problems/sliding-window-maximum/) | 239 | 🔴 Hard | 🔑 ANCHOR | `#monotonic-queue` | Maintain decreasing indices |
| 8 | [Longest Repeating Character Replacement](https://leetcode.com/problems/longest-repeating-character-replacement/) | 424 | 🟡 Medium | 🔑 ANCHOR | `#sliding-window` | Keep maxFreq; shrink if invalid |
| 9 | [Next Permutation](https://leetcode.com/problems/next-permutation/) | 31 | 🟡 Medium | ⭐ HIGH VALUE | `#permutation-logic` | Find pivot, swap, reverse suffix |
| 10 | [Merge Sorted Array](https://leetcode.com/problems/merge-sorted-array/) | 88 | 🟢 Easy | ⭐ HIGH VALUE | `#3-pointers` | Fill from back |
| 11 | [Find All Anagrams in a String](https://leetcode.com/problems/find-all-anagrams-in-a-string/) | 438 | 🟡 Medium | ⭐ HIGH VALUE | `#sliding-window` | Slide and match counts |
| 12 | [Permutation in String](https://leetcode.com/problems/permutation-in-string/) | 567 | 🟡 Medium | ⭐ HIGH VALUE | `#sliding-window` | Compare window counts |
| 13 | [Remove Duplicates from Sorted Array](https://leetcode.com/problems/remove-duplicates-from-sorted-array/) | 26 | 🟢 Easy | 🧱 STANDARD | `#two-pointers` `#in-place` | Slow pointer write unique |
| 14 | [Remove Element](https://leetcode.com/problems/remove-element/) | 27 | 🟢 Easy | 🧱 STANDARD | `#two-pointers` `#in-place` | Overwrite/partition |
| 15 | [Minimum Size Subarray Sum](https://leetcode.com/problems/minimum-size-subarray-sum/) | 209 | 🟡 Medium | 🧱 STANDARD | `#sliding-window` | Shrink while sum>=target |
| 16 | [Move Zeroes](https://leetcode.com/problems/move-zeroes/) | 283 | 🟢 Easy | 🧱 STANDARD | `#fast-slow-pointers` `#cycle-detection` | Write non-zeros then fill |
| 17 | [Subarray Product Less Than K](https://leetcode.com/problems/subarray-product-less-than-k/) | 713 | 🟡 Medium | 🧱 STANDARD | `#sliding-window` | Shrink while prod>=k |
| 18 | [Boats to Save People](https://leetcode.com/problems/boats-to-save-people/) | 881 | 🟡 Medium | 🧱 STANDARD | `#two-pointers` | Pair heaviest with lightest |
| 19 | [Max Consecutive Ones III](https://leetcode.com/problems/max-consecutive-ones-iii/) | 1004 | 🟡 Medium | 🧱 STANDARD | `#sliding-window` | At most k zeros; shrink when >k |
| 20 | [3Sum Closest](https://leetcode.com/problems/3sum-closest/) | 16 | 🟡 Medium | 🎯 PRACTICE | `#two-pointers` | Fix i; minimize abs diff |
| 21 | [4Sum](https://leetcode.com/problems/4sum/) | 18 | 🟡 Medium | 🎯 PRACTICE | `#two-pointers` `#k-sum` `#sorting` | Reduce to 2-sum with pruning |
| 22 | [Reverse String](https://leetcode.com/problems/reverse-string/) | 344 | 🟢 Easy | 🎯 PRACTICE | `#pointers` | Swap ends |
| 23 | [Reverse Vowels of a String](https://leetcode.com/problems/reverse-vowels-of-a-string/) | 345 | 🟢 Easy | 🎯 PRACTICE | `#pointers` | Swap vowels |
| 24 | [Valid Palindrome II](https://leetcode.com/problems/valid-palindrome-ii/) | 680 | 🟢 Easy | 🎯 PRACTICE | `#two-pointers` | Allow one deletion |
| 25 | [Binary Subarrays With Sum](https://leetcode.com/problems/binary-subarrays-with-sum/) | 930 | 🟡 Medium | 🎯 PRACTICE | `#sliding-window` | Count via prefix sums |
| 26 | [Bag of Tokens](https://leetcode.com/problems/bag-of-tokens/) | 948 | 🟡 Medium | 🎯 PRACTICE | `#two-pointers` `#greedy` | Spend smallest for power; sell largest |
| 27 | [Squares of a Sorted Array](https://leetcode.com/problems/squares-of-a-sorted-array/) | 977 | 🟢 Easy | 🎯 PRACTICE | `#two-pointers` | Compare abs ends; fill back |
| 28 | [Count Number of Nice Subarrays](https://leetcode.com/problems/count-number-of-nice-subarrays/) | 1248 | 🟡 Medium | 🎯 PRACTICE | `#prefix` | Count odds via prefix |
| 29 | [Number of Substrings Containing All Three Characters](https://leetcode.com/problems/number-of-substrings-containing-all-three-characters/) | 1358 | 🟡 Medium | 🎯 PRACTICE | `#sliding-window` | Count windows with all chars |
| 30 | [Maximum Number of Vowels in a Substring of Given Length](https://leetcode.com/problems/maximum-number-of-vowels-in-a-substring-of-given-length/) | 1456 | 🟡 Medium | 🎯 PRACTICE | `#sliding-window` | Slide count of vowels |
| 31 | [Longest Subarray of 1's After Deleting One Element](https://leetcode.com/problems/longest-subarray-of-1s-after-deleting-one-element/) | 1493 | 🟡 Medium | 🎯 PRACTICE | `#sliding-window` | Allow one zero |
| 32 | [Shortest Subarray with Sum at Least K](https://leetcode.com/problems/shortest-subarray-with-sum-at-least-k/) | 862 | 🔴 Hard | 💡 ADVANCED | `#monotonic-queue` | Monotonic deque on prefix sums |
| 33 | [Subarrays with K Different Integers](https://leetcode.com/problems/subarrays-with-k-different-integers/) | 992 | 🔴 Hard | 💡 ADVANCED | `#sliding-window` | AtMost(K) - AtMost(K-1) |
| 34 | [Longest Continuous Subarray With Absolute Diff <= Limit](https://leetcode.com/problems/longest-continuous-subarray-with-absolute-diff-less-than-or-equal-to-limit/) | 1438 | 🟡 Medium | 💡 ADVANCED | `#monotonic-queue` | Maintain min/max deques |
| 35 | [Frequency of the Most Frequent Element](https://leetcode.com/problems/frequency-of-the-most-frequent-element/) | 1838 | 🟡 Medium | 💡 ADVANCED | `#sliding-window` | Raise window to nums[r] within k |

> [!TIP]
> ### 💡 Phase 2 Milestone — Boundary Pruning & Window Invariants
> - **Opposing Pointers Elimination:** On sorted arrays, greedily shift boundaries to discard entire rows/columns of candidate spaces.
> - **Sliding Window Rule:** Expand `right` to include elements; while window invariant is violated, contract `left` to restore validity.
> - **Exact $K$ Decomposition:** Problems requiring 'exactly $K$' are often solved via `AtMost(K) - AtMost(K - 1)`.

---

## Phase 3 — Linked Lists + Stacks/Queues 🔗📚

| # | Problem | LC# | Difficulty | Priority | Pattern Tags | Hybrid Approach & Core Invariant |
| :---: | :--- | :---: | :---: | :---: | :--- | :--- |
| 1 | [Remove Nth Node From End of List](https://leetcode.com/problems/remove-nth-node-from-end-of-list/) | 19 | 🟡 Medium | 🔑 ANCHOR | `#two-pointers` | Advance fast n; then move both |
| 2 | [Merge Two Sorted Lists](https://leetcode.com/problems/merge-two-sorted-lists/) | 21 | 🟢 Easy | 🔑 ANCHOR | `#dummy-node` | Merge by choosing smaller head |
| 3 | [Linked List Cycle](https://leetcode.com/problems/linked-list-cycle/) | 141 | 🟢 Easy | 🔑 ANCHOR | `#fast-slow-pointers` `#cycle-detection` | Fast/slow meet => cycle |
| 4 | [Min Stack](https://leetcode.com/problems/min-stack/) | 155 | 🟡 Medium | 🔑 ANCHOR | `#stack` `#system-design` | Track mins with aux stack |
| 5 | [Reverse Linked List](https://leetcode.com/problems/reverse-linked-list/) | 206 | 🟢 Easy | 🔑 ANCHOR | `#pointers` | Iterative prev/curr/next |
| 6 | [Daily Temperatures](https://leetcode.com/problems/daily-temperatures/) | 739 | 🟡 Medium | 🔑 ANCHOR | `#monotonic-stack` | Pop while current warmer |
| 7 | [Add Two Numbers](https://leetcode.com/problems/add-two-numbers/) | 2 | 🟡 Medium | ⭐ HIGH VALUE | `#carry` | Simulate addition with carry |
| 8 | [Merge k Sorted Lists](https://leetcode.com/problems/merge-k-sorted-lists/) | 23 | 🔴 Hard | ⭐ HIGH VALUE | `#heap` | Push heads; pop/push next |
| 9 | [Trapping Rain Water](https://leetcode.com/problems/trapping-rain-water/) | 42 | 🔴 Hard | ⭐ HIGH VALUE | `#two-pointers` `#stack` | Use boundaries; accumulate water |
| 10 | [Copy List with Random Pointer](https://leetcode.com/problems/copy-list-with-random-pointer/) | 138 | 🟡 Medium | ⭐ HIGH VALUE | `#hash-map` | Old->new mapping; reconnect |
| 11 | [Linked List Cycle II](https://leetcode.com/problems/linked-list-cycle-ii/) | 142 | 🟡 Medium | ⭐ HIGH VALUE | `#fast-slow-pointers` `#cycle-detection` | Reset one; move both to entry |
| 12 | [Reorder List](https://leetcode.com/problems/reorder-list/) | 143 | 🟡 Medium | ⭐ HIGH VALUE | `#multi-step` | Mid + reverse + merge alternately |
| 13 | [Remove K Digits](https://leetcode.com/problems/remove-k-digits/) | 402 | 🟡 Medium | ⭐ HIGH VALUE | `#monotonic-stack` | Pop bigger digits while k>0 |
| 14 | [Minimum Remove to Make Valid Parentheses](https://leetcode.com/problems/minimum-remove-to-make-valid-parentheses/) | 1249 | 🟡 Medium | ⭐ HIGH VALUE | `#stack` | Mark invalid indices; rebuild |
| 15 | [Simplify Path](https://leetcode.com/problems/simplify-path/) | 71 | 🟡 Medium | 🧱 STANDARD | `#stack` | Process '/', '.', '..' |
| 16 | [Evaluate Reverse Polish Notation](https://leetcode.com/problems/evaluate-reverse-polish-notation/) | 150 | 🟡 Medium | 🧱 STANDARD | `#stack` | Apply ops to top two |
| 17 | [Palindrome Linked List](https://leetcode.com/problems/palindrome-linked-list/) | 234 | 🟢 Easy | 🧱 STANDARD | `#reverse-half` | Find mid; reverse 2nd half; compare |
| 18 | [Decode String](https://leetcode.com/problems/decode-string/) | 394 | 🟡 Medium | 🧱 STANDARD | `#stack` | Parse k[substr] |
| 19 | [Next Greater Element I](https://leetcode.com/problems/next-greater-element-i/) | 496 | 🟢 Easy | 🧱 STANDARD | `#monotonic-stack` | Map next greater via stack |
| 20 | [Next Greater Element II](https://leetcode.com/problems/next-greater-element-ii/) | 503 | 🟡 Medium | 🧱 STANDARD | `#stack` | Traverse twice; mod index |
| 21 | [Asteroid Collision](https://leetcode.com/problems/asteroid-collision/) | 735 | 🟡 Medium | 🧱 STANDARD | `#stack` | Simulate collisions |
| 22 | [Middle of the Linked List](https://leetcode.com/problems/middle-of-the-linked-list/) | 876 | 🟢 Easy | 🧱 STANDARD | `#fast-slow-pointers` `#cycle-detection` | Slow ends at middle |
| 23 | [Online Stock Span](https://leetcode.com/problems/online-stock-span/) | 901 | 🟡 Medium | 🧱 STANDARD | `#monotonic-stack` | Pop while price >= top |
| 24 | [Swap Nodes in Pairs](https://leetcode.com/problems/swap-nodes-in-pairs/) | 24 | 🟡 Medium | 🎯 PRACTICE | `#linked-list` `#pointer-manipulation` | Swap in-place with dummy |
| 25 | [Rotate List](https://leetcode.com/problems/rotate-list/) | 61 | 🟡 Medium | 🎯 PRACTICE | `#linked-list` `#cycle-detection` | Make cycle; break at new tail |
| 26 | [Intersection of Two Linked Lists](https://leetcode.com/problems/intersection-of-two-linked-lists/) | 160 | 🟢 Easy | 🎯 PRACTICE | `#linked-list` `#two-pointers` | Two runners switch heads |
| 27 | [Implement Stack using Queues](https://leetcode.com/problems/implement-stack-using-queues/) | 225 | 🟢 Easy | 🎯 PRACTICE | `#stack` | Rotate to simulate stack |
| 28 | [Implement Queue using Stacks](https://leetcode.com/problems/implement-queue-using-stacks/) | 232 | 🟢 Easy | 🎯 PRACTICE | `#stack` | Amortized enqueue/dequeue |
| 29 | [Odd Even Linked List](https://leetcode.com/problems/odd-even-linked-list/) | 328 | 🟡 Medium | 🎯 PRACTICE | `#linked-list` `#pointer-manipulation` | Separate odd/even chains |
| 30 | [Implement Circular Queue](https://leetcode.com/problems/design-circular-queue/) | 622 | 🟡 Medium | 🎯 PRACTICE | `#system-design` | Array ring buffer |
| 31 | [Design Circular Deque](https://leetcode.com/problems/design-circular-deque/) | 641 | 🟡 Medium | 🎯 PRACTICE | `#system-design` | Array ring buffer |
| 32 | [Minimum Add to Make Parentheses Valid](https://leetcode.com/problems/minimum-add-to-make-parentheses-valid/) | 921 | 🟡 Medium | 🎯 PRACTICE | `#stack` `#greedy` | Count balance; add needed |
| 33 | [Remove All Adjacent Duplicates In String](https://leetcode.com/problems/remove-all-adjacent-duplicates-in-string/) | 1047 | 🟢 Easy | 🎯 PRACTICE | `#stack` | Pop if same as top |
| 34 | [Reverse Nodes in k-Group](https://leetcode.com/problems/reverse-nodes-in-k-group/) | 25 | 🔴 Hard | 💡 ADVANCED | `#linked-list` `#k-group-reversal` | Reverse k nodes repeatedly |
| 35 | [Largest Rectangle in Histogram](https://leetcode.com/problems/largest-rectangle-in-histogram/) | 84 | 🔴 Hard | 💡 ADVANCED | `#monotonic-stack` | Pop heights; compute width |
| 36 | [Sort List](https://leetcode.com/problems/sort-list/) | 148 | 🟡 Medium | 💡 ADVANCED | `#linked-list` `#merge-sort` `#divide-and-conquer` | Split with slow/fast; merge |
| 37 | [Exclusive Time of Functions](https://leetcode.com/problems/exclusive-time-of-functions/) | 636 | 🟡 Medium | 💡 ADVANCED | `#stack` | Track prev timestamp |
| 38 | [Sum of Subarray Minimums](https://leetcode.com/problems/sum-of-subarray-minimums/) | 907 | 🟡 Medium | 💡 ADVANCED | `#monotonic-stack` | Count contribution via spans |

> [!TIP]
> ### 💡 Phase 3 Milestone — Pointer Manipulation & Monotonic Structures
> - **Sentinel / Dummy Nodes:** Prepend a dummy head to eliminate edge cases around empty lists or head deletions/swaps.
> - **Floyd's Tortoise and Hare:** Relative speed delta of 1 node/step guarantees cycle meeting without extra memory allocations.
> - **Monotonic Stack Invariant:** Store indices with strictly increasing/decreasing values to resolve nearest greater/smaller elements in $O(N)$ amortized time.

---

## Phase 4 — Trees & BST (DFS/BFS) 🌳

| # | Problem | LC# | Difficulty | Priority | Pattern Tags | Hybrid Approach & Core Invariant |
| :---: | :--- | :---: | :---: | :---: | :--- | :--- |
| 1 | [Validate Binary Search Tree](https://leetcode.com/problems/validate-binary-search-tree/) | 98 | 🟡 Medium | 🔑 ANCHOR | `#binary-search` `#bst` | Range-check recursion |
| 2 | [Binary Tree Level Order Traversal](https://leetcode.com/problems/binary-tree-level-order-traversal/) | 102 | 🟡 Medium | 🔑 ANCHOR | `#bfs` | Queue by level size |
| 3 | [Maximum Depth of Binary Tree](https://leetcode.com/problems/maximum-depth-of-binary-tree/) | 104 | 🟢 Easy | 🔑 ANCHOR | `#dfs` | Return 1+max(depths) |
| 4 | [Invert Binary Tree](https://leetcode.com/problems/invert-binary-tree/) | 226 | 🟢 Easy | 🔑 ANCHOR | `#dfs` | Swap children recursively |
| 5 | [Lowest Common Ancestor of a Binary Tree](https://leetcode.com/problems/lowest-common-ancestor-of-a-binary-tree/) | 236 | 🟡 Medium | 🔑 ANCHOR | `#dfs` | Return node if p/q; combine |
| 6 | [Diameter of Binary Tree](https://leetcode.com/problems/diameter-of-binary-tree/) | 543 | 🟢 Easy | 🔑 ANCHOR | `#dfs` | Height return; update global diam |
| 7 | [Construct Binary Tree from Preorder and Inorder Traversal](https://leetcode.com/problems/construct-binary-tree-from-preorder-and-inorder-traversal/) | 105 | 🟡 Medium | ⭐ HIGH VALUE | `#recursion` | Pick root from preorder; split inorder |
| 8 | [Flatten Binary Tree to Linked List](https://leetcode.com/problems/flatten-binary-tree-to-linked-list/) | 114 | 🟡 Medium | ⭐ HIGH VALUE | `#dfs` | Rewire using preorder |
| 9 | [BST Iterator](https://leetcode.com/problems/binary-search-tree-iterator/) | 173 | 🟡 Medium | ⭐ HIGH VALUE | `#stack` | Inorder iterator with stack |
| 10 | [Binary Tree Right Side View](https://leetcode.com/problems/binary-tree-right-side-view/) | 199 | 🟡 Medium | ⭐ HIGH VALUE | `#bfs` `#dfs` | Take last of each level |
| 11 | [Kth Smallest Element in a BST](https://leetcode.com/problems/kth-smallest-element-in-a-bst/) | 230 | 🟡 Medium | ⭐ HIGH VALUE | `#inorder` | Iterate inorder count |
| 12 | [Path Sum III](https://leetcode.com/problems/path-sum-iii/) | 437 | 🟡 Medium | ⭐ HIGH VALUE | `#prefix-sum` `#binary-tree` | DFS with prefix-sum map |
| 13 | [Same Tree](https://leetcode.com/problems/same-tree/) | 100 | 🟢 Easy | 🧱 STANDARD | `#dfs` | Compare structure and values |
| 14 | [Symmetric Tree](https://leetcode.com/problems/symmetric-tree/) | 101 | 🟢 Easy | 🧱 STANDARD | `#dfs` | Compare L.left vs R.right |
| 15 | [Convert Sorted Array to Binary Search Tree](https://leetcode.com/problems/convert-sorted-array-to-binary-search-tree/) | 108 | 🟢 Easy | 🧱 STANDARD | `#binary-tree` `#divide-and-conquer` | Mid as root recursively |
| 16 | [Balanced Binary Tree](https://leetcode.com/problems/balanced-binary-tree/) | 110 | 🟢 Easy | 🧱 STANDARD | `#dfs` | Return height or -1 if unbalanced |
| 17 | [Path Sum](https://leetcode.com/problems/path-sum/) | 112 | 🟢 Easy | 🧱 STANDARD | `#binary-tree` `#dfs-recursion` | Subtract target along path |
| 18 | [Populating Next Right Pointers in Each Node](https://leetcode.com/problems/populating-next-right-pointers-in-each-node/) | 116 | 🟡 Medium | 🧱 STANDARD | `#bfs` | Connect level next pointers |
| 19 | [Lowest Common Ancestor of a BST](https://leetcode.com/problems/lowest-common-ancestor-of-a-binary-search-tree/) | 235 | 🟡 Medium | 🧱 STANDARD | `#binary-search` `#bst` | Walk down by comparing p,q |
| 20 | [Subtree of Another Tree](https://leetcode.com/problems/subtree-of-another-tree/) | 572 | 🟢 Easy | 🧱 STANDARD | `#dfs` | Compare at each node |
| 21 | [Merge Two Binary Trees](https://leetcode.com/problems/merge-two-binary-trees/) | 617 | 🟢 Easy | 🧱 STANDARD | `#dfs` | Sum nodes; recurse |
| 22 | [Binary Tree Zigzag Level Order Traversal](https://leetcode.com/problems/binary-tree-zigzag-level-order-traversal/) | 103 | 🟡 Medium | 🎯 PRACTICE | `#bfs` | Alternate direction per level |
| 23 | [Construct Binary Tree from Inorder and Postorder Traversal](https://leetcode.com/problems/construct-binary-tree-from-inorder-and-postorder-traversal/) | 106 | 🟡 Medium | 🎯 PRACTICE | `#recursion` | Root from postorder; split inorder |
| 24 | [Minimum Depth of Binary Tree](https://leetcode.com/problems/minimum-depth-of-binary-tree/) | 111 | 🟢 Easy | 🎯 PRACTICE | `#bfs` `#dfs` | BFS first leaf |
| 25 | [Path Sum II](https://leetcode.com/problems/path-sum-ii/) | 113 | 🟡 Medium | 🎯 PRACTICE | `#dfs` `#backtracking` | Track path list |
| 26 | [Populating Next Right Pointers in Each Node II](https://leetcode.com/problems/populating-next-right-pointers-in-each-node-ii/) | 117 | 🟡 Medium | 🎯 PRACTICE | `#bfs` | Handle missing children |
| 27 | [Sum Root to Leaf Numbers](https://leetcode.com/problems/sum-root-to-leaf-numbers/) | 129 | 🟡 Medium | 🎯 PRACTICE | `#dfs` | Carry number down |
| 28 | [Insert into a Binary Search Tree](https://leetcode.com/problems/insert-into-a-binary-search-tree/) | 701 | 🟡 Medium | 🎯 PRACTICE | `#binary-search` `#bst` | Iterative/recursive insert |
| 29 | [Range Sum of BST](https://leetcode.com/problems/range-sum-of-bst/) | 938 | 🟢 Easy | 🎯 PRACTICE | `#dfs` | Prune by BST property |
| 30 | [Count Good Nodes in Binary Tree](https://leetcode.com/problems/count-good-nodes-in-binary-tree/) | 1448 | 🟡 Medium | 🎯 PRACTICE | `#dfs` | Track path max |
| 31 | [Pseudo-Palindromic Paths in a Binary Tree](https://leetcode.com/problems/pseudo-palindromic-paths-in-a-binary-tree/) | 1457 | 🟡 Medium | 🎯 PRACTICE | `#bit-manipulation` | Toggle counts; check <=1 odd |
| 32 | [Binary Tree Maximum Path Sum](https://leetcode.com/problems/binary-tree-maximum-path-sum/) | 124 | 🔴 Hard | 💡 ADVANCED | `#dfs` | Best downward gain; update answer |
| 33 | [Serialize and Deserialize Binary Tree](https://leetcode.com/problems/serialize-and-deserialize-binary-tree/) | 297 | 🔴 Hard | 💡 ADVANCED | `#dfs` `#system-design` | Preorder with null markers |
| 34 | [House Robber III](https://leetcode.com/problems/house-robber-iii/) | 337 | 🟡 Medium | 💡 ADVANCED | `#dynamic-programming` `#binary-tree` | Return (rob,skip) per node |
| 35 | [Delete Node in a BST](https://leetcode.com/problems/delete-node-in-a-bst/) | 450 | 🟡 Medium | 💡 ADVANCED | `#binary-search` `#bst` | Find node; replace with successor |
| 36 | [Binary Tree Cameras](https://leetcode.com/problems/binary-tree-cameras/) | 968 | 🔴 Hard | 🧪 CHALLENGE | `#greedy` `#dynamic-programming` | States: covered/hasCam/needsCam |

> [!TIP]
> ### 💡 Phase 4 Milestone — Subtree Information Flow & BST Ordering
> - **Bottom-Up Tree DP:** Determine what metric each child returns to its parent (e.g., height, subtree sum, node presence).
> - **BST Invariant:** Every node must satisfy global bounds $(\text{low} < \text{val} < \text{high})$. In-order traversal yields strictly ascending order.
> - **Level-by-Level BFS:** Snapshot queue count `int count = q.Count` to separate the current depth layer from newly discovered children.

---

## Phase 5 — Graphs (DFS/BFS + Topo) 🗺️

| # | Problem | LC# | Difficulty | Priority | Pattern Tags | Hybrid Approach & Core Invariant |
| :---: | :--- | :---: | :---: | :---: | :--- | :--- |
| 1 | [Number of Islands](https://leetcode.com/problems/number-of-islands/) | 200 | 🟡 Medium | 🔑 ANCHOR | `#bfs` `#dfs` `#matrix-grid` | Count components; mark visited |
| 2 | [Course Schedule](https://leetcode.com/problems/course-schedule/) | 207 | 🟡 Medium | 🔑 ANCHOR | `#topological-sort` | Kahn indegrees; detect cycle |
| 3 | [Flood Fill](https://leetcode.com/problems/flood-fill/) | 733 | 🟢 Easy | 🔑 ANCHOR | `#bfs` `#dfs` `#matrix-grid` | Recolor component |
| 4 | [Rotting Oranges](https://leetcode.com/problems/rotting-oranges/) | 994 | 🟡 Medium | 🔑 ANCHOR | `#multi-source-bfs` | BFS layers = minutes |
| 5 | [Surrounded Regions](https://leetcode.com/problems/surrounded-regions/) | 130 | 🟡 Medium | ⭐ HIGH VALUE | `#dfs` | Mark safe from borders; flip rest |
| 6 | [Clone Graph](https://leetcode.com/problems/clone-graph/) | 133 | 🟡 Medium | ⭐ HIGH VALUE | `#bfs` `#dfs` | Old->new clone mapping |
| 7 | [Course Schedule II](https://leetcode.com/problems/course-schedule-ii/) | 210 | 🟡 Medium | ⭐ HIGH VALUE | `#topological-sort` | Return ordering via Kahn |
| 8 | [Graph Valid Tree](https://leetcode.com/problems/graph-valid-tree/) | 261 | 🟡 Medium | ⭐ HIGH VALUE | `#union-find` `#dfs` | N-1 edges + connected + acyclic |
| 9 | [Evaluate Division](https://leetcode.com/problems/evaluate-division/) | 399 | 🟡 Medium | ⭐ HIGH VALUE | `#graph-weighted` | DFS/BFS compute ratios |
| 10 | [01 Matrix](https://leetcode.com/problems/01-matrix/) | 542 | 🟡 Medium | ⭐ HIGH VALUE | `#multi-source-bfs` | Distances from all zeros |
| 11 | [Number of Provinces](https://leetcode.com/problems/number-of-provinces/) | 547 | 🟡 Medium | 🧱 STANDARD | `#union-find` `#dfs` | Count connected components |
| 12 | [Max Area of Island](https://leetcode.com/problems/max-area-of-island/) | 695 | 🟡 Medium | 🧱 STANDARD | `#dfs` | Return area while marking |
| 13 | [Is Graph Bipartite?](https://leetcode.com/problems/is-graph-bipartite/) | 785 | 🟡 Medium | 🧱 STANDARD | `#bfs` | 2-color with conflict check |
| 14 | [Find Eventual Safe States](https://leetcode.com/problems/find-eventual-safe-states/) | 802 | 🟡 Medium | 🧱 STANDARD | `#topological-sort` `#dfs` | Reverse graph topo or color DFS |
| 15 | [Shortest Path in Binary Matrix](https://leetcode.com/problems/shortest-path-in-binary-matrix/) | 1091 | 🟡 Medium | 🧱 STANDARD | `#bfs` | Unweighted shortest path |
| 16 | [Walls and Gates](https://leetcode.com/problems/walls-and-gates/) | 286 | 🟡 Medium | 🎯 PRACTICE | `#multi-source-bfs` | BFS from gates to fill distances |
| 17 | [Island Perimeter](https://leetcode.com/problems/island-perimeter/) | 463 | 🟢 Easy | 🎯 PRACTICE | `#matrix-grid` | Count edges of land |
| 18 | [Minesweeper](https://leetcode.com/problems/minesweeper/) | 529 | 🟡 Medium | 🎯 PRACTICE | `#bfs` `#dfs` | Reveal empties recursively |
| 19 | [All Paths From Source to Target](https://leetcode.com/problems/all-paths-from-source-to-target/) | 797 | 🟡 Medium | 🎯 PRACTICE | `#dfs` `#backtracking` | Enumerate paths in DAG |
| 20 | [Keys and Rooms](https://leetcode.com/problems/keys-and-rooms/) | 841 | 🟡 Medium | 🎯 PRACTICE | `#dfs` | Visit reachable rooms |
| 21 | [Snakes and Ladders](https://leetcode.com/problems/snakes-and-ladders/) | 909 | 🟡 Medium | 🎯 PRACTICE | `#bfs` | Shortest moves with board jumps |
| 22 | [As Far from Land as Possible](https://leetcode.com/problems/as-far-from-land-as-possible/) | 1162 | 🟡 Medium | 🎯 PRACTICE | `#multi-source-bfs` | Max dist from any land |
| 23 | [Number of Closed Islands](https://leetcode.com/problems/number-of-closed-islands/) | 1254 | 🟡 Medium | 🎯 PRACTICE | `#dfs` | Count components not touching border |
| 24 | [Count Sub Islands](https://leetcode.com/problems/count-sub-islands/) | 1905 | 🟡 Medium | 🎯 PRACTICE | `#dfs` | Island in grid2 contained in grid1 |
| 25 | [Find if Path Exists in Graph](https://leetcode.com/problems/find-if-path-exists-in-graph/) | 1971 | 🟢 Easy | 🎯 PRACTICE | `#bfs` `#dfs` | Traverse adjacency list |
| 26 | [Word Ladder](https://leetcode.com/problems/word-ladder/) | 127 | 🔴 Hard | 💡 ADVANCED | `#bfs` | Layered BFS via wildcard buckets |
| 27 | [Minimum Height Trees](https://leetcode.com/problems/minimum-height-trees/) | 310 | 🟡 Medium | 💡 ADVANCED | `#topological-sort` | Peel leaves until centers |
| 28 | [Reconstruct Itinerary](https://leetcode.com/problems/reconstruct-itinerary/) | 332 | 🔴 Hard | 💡 ADVANCED | `#graph` `#eulerian-circuit` `#hierholzers-algorithm` | Hierholzer with min-heap |
| 29 | [Pacific Atlantic Water Flow](https://leetcode.com/problems/pacific-atlantic-water-flow/) | 417 | 🟡 Medium | 💡 ADVANCED | `#dfs` | Reachability from both oceans |
| 30 | [Redundant Connection II](https://leetcode.com/problems/redundant-connection-ii/) | 685 | 🔴 Hard | 💡 ADVANCED | `#graph` | Detect node with two parents + cycle |
| 31 | [Bus Routes](https://leetcode.com/problems/bus-routes/) | 815 | 🔴 Hard | 💡 ADVANCED | `#bfs` | Stops->routes graph |
| 32 | [Alien Dictionary](https://leetcode.com/problems/alien-dictionary/) | 269 | 🔴 Hard | 🧪 CHALLENGE | `#topological-sort` | Build edges by adjacent words |

> [!TIP]
> ### 💡 Phase 5 Milestone — Graph Traversal & Cycle Detection
> - **Grid Exploration:** Model 2D matrices as implicit graphs where each cell connects to 4 neighbors; sink visited land cells to prevent cycles.
> - **Topological Sort (Kahn's Algorithm):** Queue nodes with indegree 0; if total processed nodes $< V$, the graph contains a directed cycle.
> - **Multi-Source BFS:** Enqueue all starting sources simultaneously to compute shortest distances in lockstep parallel waves.

---

## Phase 6 — Binary Search + Intervals 📏🔍

| # | Problem | LC# | Difficulty | Priority | Pattern Tags | Hybrid Approach & Core Invariant |
| :---: | :--- | :---: | :---: | :---: | :--- | :--- |
| 1 | [Search in Rotated Sorted Array](https://leetcode.com/problems/search-in-rotated-sorted-array/) | 33 | 🟡 Medium | 🔑 ANCHOR | `#binary-search` | Find sorted half; discard other |
| 2 | [Find First and Last Position of Element](https://leetcode.com/problems/find-first-and-last-position-of-element-in-sorted-array/) | 34 | 🟡 Medium | 🔑 ANCHOR | `#binary-search` | Two searches: leftmost/rightmost |
| 3 | [Merge Intervals](https://leetcode.com/problems/merge-intervals/) | 56 | 🟡 Medium | 🔑 ANCHOR | `#intervals` | Merge overlaps by tracking end |
| 4 | [Insert Interval](https://leetcode.com/problems/insert-interval/) | 57 | 🟡 Medium | 🔑 ANCHOR | `#intervals` | Append non-overlap; merge; append rest |
| 5 | [Non-overlapping Intervals](https://leetcode.com/problems/non-overlapping-intervals/) | 435 | 🟡 Medium | 🔑 ANCHOR | `#greedy` | Sort by end; count removals |
| 6 | [Koko Eating Bananas](https://leetcode.com/problems/koko-eating-bananas/) | 875 | 🟡 Medium | 🔑 ANCHOR | `#binary-search-on-answer` | Feasibility check hours<=h |
| 7 | [Capacity To Ship Packages Within D Days](https://leetcode.com/problems/capacity-to-ship-packages-within-d-days/) | 1011 | 🟡 Medium | 🔑 ANCHOR | `#binary-search-on-answer` | Feasible(capacity) via simulation |
| 8 | [Meeting Rooms II](https://leetcode.com/problems/meeting-rooms-ii/) | 253 | 🟡 Medium | ⭐ HIGH VALUE | `#heap` | Min-heap of end times |
| 9 | [Kth Smallest Element in a Sorted Matrix](https://leetcode.com/problems/kth-smallest-element-in-a-sorted-matrix/) | 378 | 🟡 Medium | ⭐ HIGH VALUE | `#binary-search-on-answer` | Count <=mid per row |
| 10 | [Minimum Number of Arrows to Burst Balloons](https://leetcode.com/problems/minimum-number-of-arrows-to-burst-balloons/) | 452 | 🟡 Medium | ⭐ HIGH VALUE | `#greedy` | Sort by end; shoot at end |
| 11 | [Interval List Intersections](https://leetcode.com/problems/interval-list-intersections/) | 986 | 🟡 Medium | ⭐ HIGH VALUE | `#two-pointers` | Advance smaller end |
| 12 | [Search a 2D Matrix](https://leetcode.com/problems/search-a-2d-matrix/) | 74 | 🟡 Medium | 🧱 STANDARD | `#binary-search` | Treat matrix as 1D |
| 13 | [Find Minimum in Rotated Sorted Array](https://leetcode.com/problems/find-minimum-in-rotated-sorted-array/) | 153 | 🟡 Medium | 🧱 STANDARD | `#binary-search` | Compare mid to right |
| 14 | [Find Peak Element](https://leetcode.com/problems/find-peak-element/) | 162 | 🟡 Medium | 🧱 STANDARD | `#binary-search` | Move toward rising slope |
| 15 | [Meeting Rooms](https://leetcode.com/problems/meeting-rooms/) | 252 | 🟢 Easy | 🧱 STANDARD | `#sort` | Check start < prevEnd |
| 16 | [Minimum Number of Days to Make m Bouquets](https://leetcode.com/problems/minimum-number-of-days-to-make-m-bouquets/) | 1482 | 🟡 Medium | 🧱 STANDARD | `#binary-search-on-answer` | Feasible(day) by scanning |
| 17 | [Sqrt(x)](https://leetcode.com/problems/sqrtx/) | 69 | 🟢 Easy | 🎯 PRACTICE | `#binary-search` | Largest mid^2 <= x |
| 18 | [Search in Rotated Sorted Array II](https://leetcode.com/problems/search-in-rotated-sorted-array-ii/) | 81 | 🟡 Medium | 🎯 PRACTICE | `#binary-search` | Handle equals; shrink bounds |
| 19 | [Find Minimum in Rotated Sorted Array II](https://leetcode.com/problems/find-minimum-in-rotated-sorted-array-ii/) | 154 | 🔴 Hard | 🎯 PRACTICE | `#duplicates` | If equal, right-- |
| 20 | [H-Index II](https://leetcode.com/problems/h-index-ii/) | 275 | 🟡 Medium | 🎯 PRACTICE | `#binary-search` | Find first citation>=n-i |
| 21 | [Valid Perfect Square](https://leetcode.com/problems/valid-perfect-square/) | 367 | 🟢 Easy | 🎯 PRACTICE | `#binary-search` | Check mid^2 |
| 22 | [Guess Number Higher or Lower](https://leetcode.com/problems/guess-number-higher-or-lower/) | 374 | 🟢 Easy | 🎯 PRACTICE | `#binary-search` | Use feedback to adjust bounds |
| 23 | [Find Right Interval](https://leetcode.com/problems/find-right-interval/) | 436 | 🟡 Medium | 🎯 PRACTICE | `#binary-search` | Map start->index; BS for end |
| 24 | [My Calendar I](https://leetcode.com/problems/my-calendar-i/) | 729 | 🟡 Medium | 🎯 PRACTICE | `#intervals` | Store; check overlap |
| 25 | [Car Fleet](https://leetcode.com/problems/car-fleet/) | 853 | 🟡 Medium | 🎯 PRACTICE | `#stack` | Compute times; merge fleets |
| 26 | [Find the Smallest Divisor Given a Threshold](https://leetcode.com/problems/find-the-smallest-divisor-given-a-threshold/) | 1283 | 🟡 Medium | 🎯 PRACTICE | `#binary-search-on-answer` | Sum ceil(nums/d) <= threshold |
| 27 | [Remove Covered Intervals](https://leetcode.com/problems/remove-covered-intervals/) | 1288 | 🟡 Medium | 🎯 PRACTICE | `#sort` | Sort start asc end desc; count uncovered |
| 28 | [Magnetic Force Between Two Balls](https://leetcode.com/problems/magnetic-force-between-two-balls/) | 1552 | 🟡 Medium | 🎯 PRACTICE | `#binary-search-on-answer` `#greedy` | Place balls greedily; maximize min dist |
| 29 | [Split Array Largest Sum](https://leetcode.com/problems/split-array-largest-sum/) | 410 | 🔴 Hard | 💡 ADVANCED | `#binary-search` `#greedy` | Check partitions needed for maxSum |
| 30 | [My Calendar II](https://leetcode.com/problems/my-calendar-ii/) | 731 | 🟡 Medium | 💡 ADVANCED | `#sweep-line` `#intervals` `#ordered-map` | Allow double booking; prevent triple |
| 31 | [Minimum Interval to Include Each Query](https://leetcode.com/problems/minimum-interval-to-include-each-query/) | 1851 | 🔴 Hard | 💡 ADVANCED | `#heap` | Sort intervals; add feasible by start |
| 32 | [Divide Intervals Into Minimum Number of Groups](https://leetcode.com/problems/divide-intervals-into-minimum-number-of-groups/) | 2406 | 🟡 Medium | 💡 ADVANCED | `#heap` | Greedy with end-time heap |
| 33 | [My Calendar III](https://leetcode.com/problems/my-calendar-iii/) | 732 | 🔴 Hard | 🧪 CHALLENGE | `#sweep-line` `#intervals` `#ordered-map` | Max overlap via diff map |
| 34 | [Minimum Number of Taps to Open to Water a Garden](https://leetcode.com/problems/minimum-number-of-taps-to-open-to-water-a-garden/) | 1326 | 🔴 Hard | 🧪 CHALLENGE | `#intervals` `#greedy` | Jump-game on coverage |

> [!TIP]
> ### 💡 Phase 6 Milestone — Answer Spaces & Interval Overlaps
> - **Binary Search on Answer Space:** Identify monotonic feasibility predicates `CanAchieve(target)` to minimize the maximum or maximize the minimum.
> - **Interval Sorting Rule:** Sort by `start` time to merge overlapping intervals; sort by `end` time for greedy non-overlapping selection.
> - **Concurrency Tracking:** Use a Min-Heap of end times or event sweep-line (`+1` start, `-1` end) to compute peak overlapping capacity.

---

## Phase 7 — Greedy & Monotonic Patterns 🪙

| # | Problem | LC# | Difficulty | Priority | Pattern Tags | Hybrid Approach & Core Invariant |
| :---: | :--- | :---: | :---: | :---: | :--- | :--- |
| 1 | [Jump Game II](https://leetcode.com/problems/jump-game-ii/) | 45 | 🟡 Medium | 🔑 ANCHOR | `#greedy` | BFS-level style on ranges |
| 2 | [Jump Game](https://leetcode.com/problems/jump-game/) | 55 | 🟡 Medium | 🔑 ANCHOR | `#greedy` | Track farthest reachable |
| 3 | [Partition Labels](https://leetcode.com/problems/partition-labels/) | 763 | 🟡 Medium | 🔑 ANCHOR | `#greedy` | Cut when i reaches max lastIndex |
| 4 | [Gas Station](https://leetcode.com/problems/gas-station/) | 134 | 🟡 Medium | ⭐ HIGH VALUE | `#greedy` | If total>=0, start after min prefix |
| 5 | [Largest Number](https://leetcode.com/problems/largest-number/) | 179 | 🟡 Medium | ⭐ HIGH VALUE | `#custom-sorting` `#greedy` | Sort by xy vs yx |
| 6 | [Queue Reconstruction by Height](https://leetcode.com/problems/queue-reconstruction-by-height/) | 406 | 🟡 Medium | ⭐ HIGH VALUE | `#greedy` | Sort desc height; insert by k |
| 7 | [Task Scheduler](https://leetcode.com/problems/task-scheduler/) | 621 | 🟡 Medium | ⭐ HIGH VALUE | `#greedy` | Count max freq; compute idle slots |
| 8 | [Best Time to Buy and Sell Stock II](https://leetcode.com/problems/best-time-to-buy-and-sell-stock-ii/) | 122 | 🟡 Medium | 🧱 STANDARD | `#greedy` | Sum all positive deltas |
| 9 | [Assign Cookies](https://leetcode.com/problems/assign-cookies/) | 455 | 🟢 Easy | 🧱 STANDARD | `#greedy` | Match smallest satisfiable |
| 10 | [Wiggle Subsequence](https://leetcode.com/problems/wiggle-subsequence/) | 376 | 🟡 Medium | 🎯 PRACTICE | `#greedy` `#dynamic-programming` | Count sign changes |
| 11 | [Can Place Flowers](https://leetcode.com/problems/can-place-flowers/) | 605 | 🟢 Easy | 🎯 PRACTICE | `#greedy` | Place if neighbors empty |
| 12 | [Maximum Swap](https://leetcode.com/problems/maximum-swap/) | 670 | 🟡 Medium | 🎯 PRACTICE | `#greedy` | Swap with farthest larger digit |
| 13 | [Hand of Straights](https://leetcode.com/problems/hand-of-straights/) | 846 | 🟡 Medium | 🎯 PRACTICE | `#backtracking` `#counting` | Greedy build sequences from min |
| 14 | [Lemonade Change](https://leetcode.com/problems/lemonade-change/) | 860 | 🟢 Easy | 🎯 PRACTICE | `#greedy` | Use 5/10 bills optimally |
| 15 | [Two City Scheduling](https://leetcode.com/problems/two-city-scheduling/) | 1029 | 🟡 Medium | 🎯 PRACTICE | `#greedy` | Sort by cost diff |
| 16 | [Minimum Deletions to Make Character Frequencies Unique](https://leetcode.com/problems/minimum-deletions-to-make-character-frequencies-unique/) | 1647 | 🟡 Medium | 🎯 PRACTICE | `#greedy` | Decrease freqs to unused |
| 17 | [Candy](https://leetcode.com/problems/candy/) | 135 | 🔴 Hard | 💡 ADVANCED | `#greedy` | Left pass then right pass |

> [!TIP]
> ### 💡 Phase 7 Milestone — Local Choice Optimality & Invariants
> - **Greedy Exchange Proof:** Verify that making the locally optimal choice never eliminates an optimal global solution.
> - **Reachability Horizon:** In jump/boundary games, track maximum reachable index incrementally rather than testing individual paths.
> - **Running Deficit Invariant:** In circular problems (e.g. Gas Station), if total sum $\ge 0$, reset starting candidate after the point of greatest deficit.

---

## Phase 8 — Dynamic Programming (1D/2D/Knapsack) 🧩

| # | Problem | LC# | Difficulty | Priority | Pattern Tags | Hybrid Approach & Core Invariant |
| :---: | :--- | :---: | :---: | :---: | :--- | :--- |
| 1 | [Unique Paths](https://leetcode.com/problems/unique-paths/) | 62 | 🟡 Medium | 🔑 ANCHOR | `#dynamic-programming` `#matrix-grid` | Dp[i][j]=dp[i-1][j]+dp[i][j-1] |
| 2 | [Edit Distance](https://leetcode.com/problems/edit-distance/) | 72 | 🔴 Hard | 🔑 ANCHOR | `#2d-dp` | Min(insert,delete,replace) |
| 3 | [Word Break](https://leetcode.com/problems/word-break/) | 139 | 🟡 Medium | 🔑 ANCHOR | `#dynamic-programming` | Dp[i]=any dp[j] and word(j,i) |
| 4 | [House Robber](https://leetcode.com/problems/house-robber/) | 198 | 🟡 Medium | 🔑 ANCHOR | `#1d-dp` | Dp[i]=max(dp[i-1],dp[i-2]+nums[i]) |
| 5 | [Longest Increasing Subsequence](https://leetcode.com/problems/longest-increasing-subsequence/) | 300 | 🟡 Medium | 🔑 ANCHOR | `#binary-search` `#dynamic-programming` | Patience: tails[] with BS |
| 6 | [Coin Change](https://leetcode.com/problems/coin-change/) | 322 | 🟡 Medium | 🔑 ANCHOR | `#knapsack` | Dp[a]=min(dp[a],dp[a-c]+1) |
| 7 | [Partition Equal Subset Sum](https://leetcode.com/problems/partition-equal-subset-sum/) | 416 | 🟡 Medium | 🔑 ANCHOR | `#knapsack` | Dp[s]=can make sum s |
| 8 | [Longest Common Subsequence](https://leetcode.com/problems/longest-common-subsequence/) | 1143 | 🟡 Medium | 🔑 ANCHOR | `#2d-dp` | Match->diag+1 else max(up,left) |
| 9 | [Longest Palindromic Substring](https://leetcode.com/problems/longest-palindromic-substring/) | 5 | 🟡 Medium | ⭐ HIGH VALUE | `#dynamic-programming` | Expand around center |
| 10 | [Maximal Square](https://leetcode.com/problems/maximal-square/) | 221 | 🟡 Medium | ⭐ HIGH VALUE | `#2d-dp` | Dp=1+min(top,left,diag) |
| 11 | [Minimum Path Sum](https://leetcode.com/problems/minimum-path-sum/) | 64 | 🟡 Medium | 🧱 STANDARD | `#dynamic-programming` `#matrix-grid` | Dp=min(top,left)+cell |
| 12 | [Decode Ways](https://leetcode.com/problems/decode-ways/) | 91 | 🟡 Medium | 🧱 STANDARD | `#dynamic-programming` | Dp[i]=single+double choices |
| 13 | [Triangle](https://leetcode.com/problems/triangle/) | 120 | 🟡 Medium | 🧱 STANDARD | `#dynamic-programming` | Bottom-up min path |
| 14 | [House Robber II](https://leetcode.com/problems/house-robber-ii/) | 213 | 🟡 Medium | 🧱 STANDARD | `#dynamic-programming` | Max(rob 0..n-2, 1..n-1) |
| 15 | [Perfect Squares](https://leetcode.com/problems/perfect-squares/) | 279 | 🟡 Medium | 🧱 STANDARD | `#dynamic-programming` | Dp[i]=min(dp[i-j^2]+1) |
| 16 | [Target Sum](https://leetcode.com/problems/target-sum/) | 494 | 🟡 Medium | 🧱 STANDARD | `#dynamic-programming` | Convert to subset sum count |
| 17 | [Longest Palindromic Subsequence](https://leetcode.com/problems/longest-palindromic-subsequence/) | 516 | 🟡 Medium | 🧱 STANDARD | `#2d-dp` | Dp[i][j] longest in s[i:j] |
| 18 | [Delete and Earn](https://leetcode.com/problems/delete-and-earn/) | 740 | 🟡 Medium | 🧱 STANDARD | `#dynamic-programming` | Convert to house robber on values |
| 19 | [Min Cost Climbing Stairs](https://leetcode.com/problems/min-cost-climbing-stairs/) | 746 | 🟢 Easy | 🧱 STANDARD | `#1d-dp` | Dp[i]=cost[i]+min(dp[i-1],dp[i-2]) |
| 20 | [Min Cost For Tickets](https://leetcode.com/problems/min-cost-for-tickets/) | 983 | 🟡 Medium | 🧱 STANDARD | `#dynamic-programming` | Dp[day]=min cost from day |
| 21 | [Unique Paths II](https://leetcode.com/problems/unique-paths-ii/) | 63 | 🟡 Medium | 🎯 PRACTICE | `#dynamic-programming` `#matrix-grid` | Obstacle cells set dp=0 |
| 22 | [Palindromic Substrings](https://leetcode.com/problems/palindromic-substrings/) | 647 | 🟡 Medium | 🎯 PRACTICE | `#expand` | Count palindromes from centers |
| 23 | [Longest Valid Parentheses](https://leetcode.com/problems/longest-valid-parentheses/) | 32 | 🔴 Hard | 💡 ADVANCED | `#stack` `#dynamic-programming` | DP lengths or stack indices |
| 24 | [Interleaving String](https://leetcode.com/problems/interleaving-string/) | 97 | 🟡 Medium | 💡 ADVANCED | `#2d-dp` | Dp[i][j] whether s3 formed |
| 25 | [Maximum Product Subarray](https://leetcode.com/problems/maximum-product-subarray/) | 152 | 🟡 Medium | 💡 ADVANCED | `#dynamic-programming` | Track maxProd and minProd |
| 26 | [Best Time to Buy and Sell Stock with Cooldown](https://leetcode.com/problems/best-time-to-buy-and-sell-stock-with-cooldown/) | 309 | 🟡 Medium | 💡 ADVANCED | `#state-machine-dp` | Hold/sold/rest transitions |
| 27 | [Combination Sum IV](https://leetcode.com/problems/combination-sum-iv/) | 377 | 🟡 Medium | 💡 ADVANCED | `#dynamic-programming` | Dp[i]+=dp[i-num] |
| 28 | [Regular Expression Matching](https://leetcode.com/problems/regular-expression-matching/) | 10 | 🔴 Hard | 🧪 CHALLENGE | `#2d-dp` | Handle '.' and '*' transitions |
| 29 | [Wildcard Matching](https://leetcode.com/problems/wildcard-matching/) | 44 | 🔴 Hard | 🧪 CHALLENGE | `#2d-dp` | Handle '?' and '*' |
| 30 | [Distinct Subsequences](https://leetcode.com/problems/distinct-subsequences/) | 115 | 🔴 Hard | 🧪 CHALLENGE | `#2d-dp` | Count ways to form t from s |
| 31 | [Burst Balloons](https://leetcode.com/problems/burst-balloons/) | 312 | 🔴 Hard | 🧪 CHALLENGE | `#intervals` `#interval-dp` | Choose last balloon in interval |

> [!TIP]
> ### 💡 Phase 8 Milestone — State Definitions & Space Compression
> - **State Representation:** Define precisely what `dp[i]` or `dp[i][j]` computes (e.g., longest, minimum cost, reachability).
> - **0/1 vs Unbounded Knapsack:** 0/1 knapsack iterates capacities backward (each item used once); unbounded knapsack iterates forward.
> - **Space Optimization:** If `dp[i]` depends only on `dp[i-1]` and `dp[i-2]`, reduce memory from $O(N)$ to $O(1)$ using rolling variables.

---

## Phase 9 — Backtracking & Combinatorics 🌿

| # | Problem | LC# | Difficulty | Priority | Pattern Tags | Hybrid Approach & Core Invariant |
| :---: | :--- | :---: | :---: | :---: | :--- | :--- |
| 1 | [Combination Sum](https://leetcode.com/problems/combination-sum/) | 39 | 🟡 Medium | 🔑 ANCHOR | `#backtracking` | Reuse current; reduce target |
| 2 | [Permutations](https://leetcode.com/problems/permutations/) | 46 | 🟡 Medium | 🔑 ANCHOR | `#backtracking` | Swap/used-array generate orderings |
| 3 | [Subsets](https://leetcode.com/problems/subsets/) | 78 | 🟡 Medium | 🔑 ANCHOR | `#backtracking` | Choose/skip each element |
| 4 | [Word Search](https://leetcode.com/problems/word-search/) | 79 | 🟡 Medium | 🔑 ANCHOR | `#backtracking` `#matrix-grid` | DFS with visited marking |
| 5 | [Generate Parentheses](https://leetcode.com/problems/generate-parentheses/) | 22 | 🟡 Medium | ⭐ HIGH VALUE | `#backtracking` | Add '(' if open<n, ')' if close<open |
| 6 | [Palindrome Partitioning](https://leetcode.com/problems/palindrome-partitioning/) | 131 | 🟡 Medium | ⭐ HIGH VALUE | `#backtracking` | Cut where substring is palindrome |
| 7 | [Letter Combinations of a Phone Number](https://leetcode.com/problems/letter-combinations-of-a-phone-number/) | 17 | 🟡 Medium | 🧱 STANDARD | `#backtracking` | DFS over digit->letters |
| 8 | [Combination Sum II](https://leetcode.com/problems/combination-sum-ii/) | 40 | 🟡 Medium | 🧱 STANDARD | `#backtracking` | Use once; skip duplicates |
| 9 | [Combinations](https://leetcode.com/problems/combinations/) | 77 | 🟡 Medium | 🧱 STANDARD | `#backtracking` | For-loop with start index |
| 10 | [Subsets II](https://leetcode.com/problems/subsets-ii/) | 90 | 🟡 Medium | 🧱 STANDARD | `#duplicates` | Sort; skip duplicates at level |
| 11 | [Permutations II](https://leetcode.com/problems/permutations-ii/) | 47 | 🟡 Medium | 🎯 PRACTICE | `#duplicates` | Sort; used[] skip duplicates |
| 12 | [N-Queens II](https://leetcode.com/problems/n-queens-ii/) | 52 | 🔴 Hard | 🎯 PRACTICE | `#backtracking` `#counting` | Backtracking count solutions |
| 13 | [Restore IP Addresses](https://leetcode.com/problems/restore-ip-addresses/) | 93 | 🟡 Medium | 🎯 PRACTICE | `#backtracking` | Try 1-3 digits per segment |
| 14 | [Matchsticks to Square](https://leetcode.com/problems/matchsticks-to-square/) | 473 | 🟡 Medium | 🎯 PRACTICE | `#backtracking` | Assign sticks to 4 buckets |
| 15 | [Beautiful Arrangement](https://leetcode.com/problems/beautiful-arrangement/) | 526 | 🟡 Medium | 🎯 PRACTICE | `#backtracking` | Place i with divisibility pruning |
| 16 | [Path with Maximum Gold](https://leetcode.com/problems/path-with-maximum-gold/) | 1219 | 🟡 Medium | 🎯 PRACTICE | `#backtracking` | DFS collect; mark visited |
| 17 | [Split a String Into the Max Number of Unique Substrings](https://leetcode.com/problems/split-a-string-into-the-max-number-of-unique-substrings/) | 1593 | 🟡 Medium | 🎯 PRACTICE | `#backtracking` | Use set; try all splits |
| 18 | [Sudoku Solver](https://leetcode.com/problems/sudoku-solver/) | 37 | 🔴 Hard | 💡 ADVANCED | `#backtracking` | Choose empty; try digits with constraints |
| 19 | [N-Queens](https://leetcode.com/problems/n-queens/) | 51 | 🔴 Hard | 💡 ADVANCED | `#backtracking` | Place row-by-row with col/diag sets |
| 20 | [Word Search II](https://leetcode.com/problems/word-search-ii/) | 212 | 🔴 Hard | 💡 ADVANCED | `#trie` `#backtracking` | DFS guided by trie |
| 21 | [Expression Add Operators](https://leetcode.com/problems/expression-add-operators/) | 282 | 🔴 Hard | 🧪 CHALLENGE | `#backtracking` | DFS with prev operand for * |
| 22 | [Remove Invalid Parentheses](https://leetcode.com/problems/remove-invalid-parentheses/) | 301 | 🔴 Hard | 🧪 CHALLENGE | `#bfs` `#backtracking` | Remove minimal; dedupe states |

> [!TIP]
> ### 💡 Phase 9 Milestone — State-Space Exploration & Pruning
> - **Backtracking Core Template:** `Choose(candidate) -> Recurse(next_state) -> Undo(candidate)`.
> - **Duplicate Pruning:** Sort input array first; skip adjacent identical candidates `if (i > start && nums[i] == nums[i-1]) continue;`.
> - **Permutation vs Combination:** Pass `start` index forward for combinations/subsets; use `used[]` boolean array or in-place swap for permutations.

---

## Phase 10 — Heaps, Tries, Union-Find, Advanced DS 🧲🌲🤝

| # | Problem | LC# | Difficulty | Priority | Pattern Tags | Hybrid Approach & Core Invariant |
| :---: | :--- | :---: | :---: | :---: | :--- | :--- |
| 1 | [Implement Trie (Prefix Tree)](https://leetcode.com/problems/implement-trie-prefix-tree/) | 208 | 🟡 Medium | 🔑 ANCHOR | `#trie` | Insert/search/prefix |
| 2 | [Design Add and Search Words Data Structure](https://leetcode.com/problems/design-add-and-search-words-data-structure/) | 211 | 🟡 Medium | 🔑 ANCHOR | `#dfs` `#trie` | Wildcard '.' backtrack |
| 3 | [Find Median from Data Stream](https://leetcode.com/problems/find-median-from-data-stream/) | 295 | 🔴 Hard | 🔑 ANCHOR | `#two-heaps` | Max-heap left, min-heap right |
| 4 | [Number of Connected Components in an Undirected Graph](https://leetcode.com/problems/number-of-connected-components-in-an-undirected-graph/) | 323 | 🟡 Medium | 🔑 ANCHOR | `#union-find` | Union edges; count components |
| 5 | [Random Pick with Weight](https://leetcode.com/problems/random-pick-with-weight/) | 528 | 🟡 Medium | ⭐ HIGH VALUE | `#binary-search` | Prefix sums; pick by random |
| 6 | [Redundant Connection](https://leetcode.com/problems/redundant-connection/) | 684 | 🟡 Medium | ⭐ HIGH VALUE | `#union-find` | First edge creating cycle |
| 7 | [Accounts Merge](https://leetcode.com/problems/accounts-merge/) | 721 | 🟡 Medium | ⭐ HIGH VALUE | `#union-find` | Union emails; group by root |
| 8 | [Snapshot Array](https://leetcode.com/problems/snapshot-array/) | 1146 | 🟡 Medium | ⭐ HIGH VALUE | `#system-design` | Versioned arrays via maps |
| 9 | [Design Underground System](https://leetcode.com/problems/design-underground-system/) | 1396 | 🟡 Medium | ⭐ HIGH VALUE | `#system-design` | Track check-in; compute averages |
| 10 | [Replace Words](https://leetcode.com/problems/replace-words/) | 648 | 🟡 Medium | 🧱 STANDARD | `#trie` | Replace by shortest root prefix |
| 11 | [Top K Frequent Words](https://leetcode.com/problems/top-k-frequent-words/) | 692 | 🟡 Medium | 🧱 STANDARD | `#heap` | Heap with custom ordering |
| 12 | [K Closest Points to Origin](https://leetcode.com/problems/k-closest-points-to-origin/) | 973 | 🟡 Medium | 🧱 STANDARD | `#heap` | Max-heap size k |
| 13 | [Satisfiability of Equality Equations](https://leetcode.com/problems/satisfiability-of-equality-equations/) | 990 | 🟡 Medium | 🧱 STANDARD | `#union-find` | Union == then check != |
| 14 | [Design HashSet](https://leetcode.com/problems/design-hashset/) | 705 | 🟢 Easy | 🎯 PRACTICE | `#buckets` | Separate chaining |
| 15 | [Design HashMap](https://leetcode.com/problems/design-hashmap/) | 706 | 🟢 Easy | 🎯 PRACTICE | `#buckets` | Separate chaining |
| 16 | [Browser History](https://leetcode.com/problems/browser-history/) | 1472 | 🟡 Medium | 🎯 PRACTICE | `#stack` `#system-design` | Stack/list with pointer |
| 17 | [Find K Pairs with Smallest Sums](https://leetcode.com/problems/find-k-pairs-with-smallest-sums/) | 373 | 🟡 Medium | 💡 ADVANCED | `#heap` | Push (i,0) per row; expand |
| 18 | [IPO](https://leetcode.com/problems/ipo/) | 502 | 🔴 Hard | 💡 ADVANCED | `#heap` `#greedy` | Max profit among feasible projects |
| 19 | [Smallest Range Covering Elements from K Lists](https://leetcode.com/problems/smallest-range-covering-elements-from-k-lists/) | 632 | 🔴 Hard | 💡 ADVANCED | `#heap` | Track current max; pop min |
| 20 | [Most Stones Removed with Same Row or Column](https://leetcode.com/problems/most-stones-removed-with-same-row-or-column/) | 947 | 🟡 Medium | 💡 ADVANCED | `#union-find` | Union rows/cols; answer n-components |
| 21 | [Regions Cut By Slashes](https://leetcode.com/problems/regions-cut-by-slashes/) | 959 | 🟡 Medium | 💡 ADVANCED | `#union-find` | Union triangles; count regions |
| 22 | [Stream of Characters](https://leetcode.com/problems/stream-of-characters/) | 1032 | 🔴 Hard | 💡 ADVANCED | `#trie` | Reverse trie + stream suffix match |
| 23 | [Smallest String With Swaps](https://leetcode.com/problems/smallest-string-with-swaps/) | 1202 | 🟡 Medium | 💡 ADVANCED | `#union-find` | Group indices; sort chars per component |
| 24 | [Palindrome Pairs](https://leetcode.com/problems/palindrome-pairs/) | 336 | 🔴 Hard | 🧪 CHALLENGE | `#trie` | Split words; match reversed prefixes |
| 25 | [All O(1) Data Structure](https://leetcode.com/problems/all-oone-data-structure/) | 432 | 🔴 Hard | 🧪 CHALLENGE | `#system-design` | DLL of counts + maps |
| 26 | [Sliding Window Median](https://leetcode.com/problems/sliding-window-median/) | 480 | 🔴 Hard | 🧪 CHALLENGE | `#two-heaps` | Lazy deletion + rebalance |
| 27 | [Making A Large Island](https://leetcode.com/problems/making-a-large-island/) | 827 | 🔴 Hard | 🧪 CHALLENGE | `#union-find` `#dfs` | Label islands; try flip 0 |
| 28 | [Remove Max Number of Edges to Keep Graph Fully Traversable](https://leetcode.com/problems/remove-max-number-of-edges-to-keep-graph-fully-traversable/) | 1579 | 🔴 Hard | 🧪 CHALLENGE | `#union-find` | Greedy union type3 then others |

> [!TIP]
> ### 💡 Phase 10 Milestone — Specialized Data Structures
> - **Disjoint Set Union (DSU):** Union by rank + path compression achieves near-linear $O(\alpha(N))$ dynamic connectivity and cycle detection.
> - **Trie Prefix Matching:** Tree of character edges enables $O(L)$ prefix validation and search, ideal for word boggles and dictionary autocomplete.
> - **Two Heaps Pattern:** Maintain dynamic running median by balancing a Max-Heap (lower half) and Min-Heap (upper half) within $\le 1$ count.

---

## Phase 11 — Advanced Graphs + Design + Mixed Interview Core 🚀

| # | Problem | LC# | Difficulty | Priority | Pattern Tags | Hybrid Approach & Core Invariant |
| :---: | :--- | :---: | :---: | :---: | :--- | :--- |
| 1 | [LRU Cache](https://leetcode.com/problems/lru-cache/) | 146 | 🟡 Medium | 🔑 ANCHOR | `#system-design` | DLL + hashmap $O(1)$ |
| 2 | [Network Delay Time](https://leetcode.com/problems/network-delay-time/) | 743 | 🟡 Medium | 🔑 ANCHOR | `#dijkstras-algorithm` | Min-heap relax edges |
| 3 | [Minimum Cost to Connect All Points](https://leetcode.com/problems/min-cost-to-connect-all-points/) | 1584 | 🟡 Medium | 🔑 ANCHOR | `#minimum-spanning-tree` | Prim/Kruskal |
| 4 | [Basic Calculator II](https://leetcode.com/problems/basic-calculator-ii/) | 227 | 🟡 Medium | ⭐ HIGH VALUE | `#stack` | Process * /; defer + - |
| 5 | [Insert Delete GetRandom O(1)](https://leetcode.com/problems/insert-delete-getrandom-o1/) | 380 | 🟡 Medium | ⭐ HIGH VALUE | `#system-design` | Array + hashmap + swap-delete |
| 6 | [Cheapest Flights Within K Stops](https://leetcode.com/problems/cheapest-flights-within-k-stops/) | 787 | 🟡 Medium | ⭐ HIGH VALUE | `#bfs` `#dynamic-programming` | Layer by stops; relax costs |
| 7 | [Time Based Key-Value Store](https://leetcode.com/problems/time-based-key-value-store/) | 981 | 🟡 Medium | ⭐ HIGH VALUE | `#binary-search` `#system-design` | Store (ts,val) list; BS |
| 8 | [Path With Minimum Effort](https://leetcode.com/problems/path-with-minimum-effort/) | 1631 | 🟡 Medium | ⭐ HIGH VALUE | `#binary-search` `#dijkstras-algorithm` | Minimize max edge cost |
| 9 | [Design Twitter](https://leetcode.com/problems/design-twitter/) | 355 | 🟡 Medium | 🧱 STANDARD | `#heap` `#system-design` | Merge recent tweets by time |
| 10 | [Path with Maximum Probability](https://leetcode.com/problems/path-with-maximum-probability/) | 1514 | 🟡 Medium | 🧱 STANDARD | `#dijkstras-algorithm` | Max-heap by prob; multiply |
| 11 | [Basic Calculator](https://leetcode.com/problems/basic-calculator/) | 224 | 🔴 Hard | 💡 ADVANCED | `#stack` | Parse +,-,(,) with sign stack |
| 12 | [Swim in Rising Water](https://leetcode.com/problems/swim-in-rising-water/) | 778 | 🔴 Hard | 💡 ADVANCED | `#binary-search` `#dijkstras-algorithm` | Minimax path via PQ |
| 13 | [Critical Connections in a Network](https://leetcode.com/problems/critical-connections-in-a-network/) | 1192 | 🔴 Hard | 💡 ADVANCED | `#graph` `#tarjans-bridge-finding` `#dfs-low-link` | Low-link for bridges |
| 14 | [Shortest Path in a Grid with Obstacles Elimination](https://leetcode.com/problems/shortest-path-in-a-grid-with-obstacles-elimination/) | 1293 | 🔴 Hard | 💡 ADVANCED | `#bfs` | (r,c,k) visited |
| 15 | [Minimum Cost to Make at Least One Valid Path in a Grid](https://leetcode.com/problems/minimum-cost-to-make-at-least-one-valid-path-in-a-grid/) | 1368 | 🔴 Hard | 💡 ADVANCED | `#bfs` | Edges cost 0/1 |
| 16 | [LFU Cache](https://leetcode.com/problems/lfu-cache/) | 460 | 🔴 Hard | 🧪 CHALLENGE | `#system-design` | Freq lists + hashmap; $O(1)$ ops |
| 17 | [Parse Lisp Expression](https://leetcode.com/problems/parse-lisp-expression/) | 736 | 🔴 Hard | 🧪 CHALLENGE | `#stack` | Recursive descent / stack |
| 18 | [Shortest Path Visiting All Nodes](https://leetcode.com/problems/shortest-path-visiting-all-nodes/) | 847 | 🔴 Hard | 🧪 CHALLENGE | `#bfs` `#bit-manipulation` | State (node,mask) |
| 19 | [Minimum Cost to Reach Destination in Time](https://leetcode.com/problems/minimum-cost-to-reach-destination-in-time/) | 1928 | 🔴 Hard | 🧪 CHALLENGE | `#dijkstras-algorithm` `#dynamic-programming` | State (node,time) min cost |

> [!TIP]
> ### 💡 Phase 11 Milestone — System-Level Algorithmic Design
> - **O(1) Eviction Design (LRU/LFU):** Combine `Dictionary` for $O(1)$ key lookup with Doubly Linked List for $O(1)$ node promotion and eviction.
> - **Dijkstra's Relaxation:** Greedily expand cheapest unvisited node via PriorityQueue on non-negative weighted graphs.
> - **Minimum Spanning Tree:** Kruskal's with DSU ($O(E \log E)$) for sparse graphs; Prim's with PriorityQueue ($O(E \log V)$) for dense graphs.

---
