# 🏦 Question Bank & Reattempt Queue

> **Track failure patterns, analyze invariant breakdowns, and schedule spaced-repetition reattempts to achieve FAANG interview mastery.**

---

## 🔁 Spaced-Repetition Reattempt Protocol

```mermaid
flowchart LR
    F["❌ Attempt Failure<br/>(Hint needed or bug)"]:::fail
    A["1. Root Cause Analysis<br/>& Invariant Breakdown"]:::step
    R1["2. Retest +3 Days<br/>(Dry-run verification)"]:::step
    R2["3. Retest +7 Days<br/>(Clean code from scratch)"]:::step
    M["✅ Mastered<br/>(Pattern internalized)"]:::pass

    F --> A --> R1 --> R2 --> M

    classDef fail fill:#b71c1c,stroke:#ff8a80,stroke-width:2px,color:#ffffff
    classDef step fill:#1e293b,stroke:#3b82f6,stroke-width:2px,color:#ffffff
    classDef pass fill:#1b5e20,stroke:#81c784,stroke-width:2px,color:#ffffff
```

---

## 🔍 Root-Cause Failure Taxonomy

| Failure Code | Category | Typical Root Cause | Remediation Protocol |
| :---: | :--- | :--- | :--- |
| **`ERR-INV`** | **Invariant Formulation Bug** | Vague pointer condition or incomplete window validity contract | Formulate physical metaphor and state invariant explicitly in comments before coding |
| **`ERR-BND`** | **Boundary & Off-by-One** | Inclusion vs exclusion error, `left <= right` vs `left < right` | Test empty set, single element, and 2-element edge cases before running code |
| **`ERR-OVF`** | **Integer Overflow** | 32-bit `int` multiplication exceeds `2^31 - 1` | Pre-cast to `long` before arithmetic, or apply modulo reduction on intermediate steps |
| **`ERR-MEM`** | **Memory / Allocation Overhead** | Reallocating arrays or hash maps inside tight inner loops | Use pre-sized arrays, `Span<T>`, or in-place pointer manipulation |
| **`ERR-PAT`** | **Pattern Mismatch** | Attempted Greedy on DP problem, or BFS where DFS was needed | Ask clarifying constraints (`N <= 10^5` rules out `O(N^2)` DP) |
| **`ERR-EDG`** | **Edge-Case Omission** | Failed to consider negatives, zeroes, duplicate keys, or parity | Run mental sanity check against extreme bounds table |

---

## 📋 Active Reattempt Queue

| Date Added | Sprint | Problem & Link | LC# | Failure Code | Root Cause & Failure Detail | Fix Strategy & Invariant Proof | Next Reattempt | Status |
| :---: | :---: | :--- | :---: | :---: | :--- | :--- | :---: | :---: |
| 2026-06-06 | S01 | [3Sum](https://leetcode.com/problems/3sum/) | 15 | `ERR-BND` | Duplicate triplets emitted due to moving pointers before duplicate skipping loop. | Skip identical elements after recording valid triplet: `while (l < r && nums[l] == nums[l+1]) l++;` | 2026-06-12 | 🟡 Re-Testing |
| 2026-06-07 | S02 | [Minimum Window Substring](https://leetcode.com/problems/minimum-window-substring/) | 76 | `ERR-INV` | Advanced left pointer before checking whether match count was strictly satisfied. | Maintain `formed` counter tracking distinct characters matching target frequency. | 2026-06-13 | ⚪ Queued |
| 2026-06-08 | W14 | [Power of Two](https://leetcode.com/problems/power-of-two/) | 231 | `ERR-EDG` | Checked bitwise condition `(n & (n - 1)) == 0` without validating `n > 0`, causing `0` and negatives to return true. | Prepend strict positivity guard clause `n > 0`. | 2026-06-11 | ✅ Mastered |
| 2026-06-08 | W14 | [Count Primes](https://leetcode.com/problems/count-primes/) | 204 | `ERR-OVF` | Inner loop starting index `i * i` overflowed 32-bit integer when `i > 46340`. | Cast loop iterator to `long` or bound condition to `(long)i * i < n`. | 2026-06-11 | ✅ Mastered |
| 2026-06-09 | S07 | [Lowest Common Ancestor](https://leetcode.com/problems/lowest-common-ancestor-of-a-binary-tree/) | 236 | `ERR-INV` | Assumed both targets must be leaves; failed when `p` was direct ancestor of `q`. | When current node matches `p` or `q`, immediately return current node without descending further. | 2026-06-14 | ⚪ Queued |
| 2026-06-10 | S10 | [Coin Change](https://leetcode.com/problems/coin-change/) | 322 | `ERR-OVF` | Initialized DP table with `int.MaxValue`, causing `dp[i - coin] + 1` to overflow into negative numbers. | Initialize with sentinel value `amount + 1` instead of `int.MaxValue`. | 2026-06-14 | ⚪ Queued |

---

## 🗃️ Master Sprint Question Bank (3-Tier Curated Ladder)

### Sprint 1: Two Pointers (ROI Rank #1)
- 🟢 **Tier 1 (Warm-Up):** [Two Sum II - Input Array Is Sorted (LC 167)](https://leetcode.com/problems/two-sum-ii-input-array-is-sorted/)
- 🟡 **Tier 2 (FAANG Medium):** [Container With Most Water (LC 11)](https://leetcode.com/problems/container-with-most-water/)
- 🟡 **Tier 2 (FAANG Medium):** [3Sum (LC 15)](https://leetcode.com/problems/3sum/)
- 🔴 **Tier 3 (Hard Anchor):** [Trapping Rain Water (LC 42)](https://leetcode.com/problems/trapping-rain-water/)
- 🟡 **Tier 2 (Variant):** [4Sum (LC 18)](https://leetcode.com/problems/4sum/)

### Sprint 2: Sliding Window (ROI Rank #1)
- 🟢 **Tier 1 (Warm-Up):** [Maximum Average Subarray I (LC 643)](https://leetcode.com/problems/maximum-average-subarray-i/)
- 🟡 **Tier 2 (FAANG Medium):** [Longest Substring Without Repeating Characters (LC 3)](https://leetcode.com/problems/longest-substring-without-repeating-characters/)
- 🟡 **Tier 2 (FAANG Medium):** [Longest Repeating Character Replacement (LC 424)](https://leetcode.com/problems/longest-repeating-character-replacement/)
- 🔴 **Tier 3 (Hard Anchor):** [Minimum Window Substring (LC 76)](https://leetcode.com/problems/minimum-window-substring/)
- 🟡 **Tier 2 (Variant):** [Permutation in String (LC 567)](https://leetcode.com/problems/permutation-in-string/)

### Sprint 3: Prefix Sum & Hash Maps (ROI Rank #8)
- 🟢 **Tier 1 (Warm-Up):** [Find Pivot Index (LC 724)](https://leetcode.com/problems/find-pivot-index/)
- 🟡 **Tier 2 (FAANG Medium):** [Subarray Sum Equals K (LC 560)](https://leetcode.com/problems/subarray-sum-equals-k/)
- 🟡 **Tier 2 (FAANG Medium):** [Contiguous Array (LC 525)](https://leetcode.com/problems/contiguous-array/)
- 🟡 **Tier 2 (FAANG Medium):** [Product of Array Except Self (LC 238)](https://leetcode.com/problems/product-of-array-except-self/)

### Sprint 4: Monotonic Stack & Queue (ROI Rank #2)
- 🟢 **Tier 1 (Warm-Up):** [Daily Temperatures (LC 739)](https://leetcode.com/problems/daily-temperatures/)
- 🟡 **Tier 2 (FAANG Medium):** [Next Greater Element II (LC 503)](https://leetcode.com/problems/next-greater-element-ii/)
- 🔴 **Tier 3 (Hard Anchor):** [Largest Rectangle in Histogram (LC 84)](https://leetcode.com/problems/largest-rectangle-in-histogram/)
- 🔴 **Tier 3 (Hard Anchor):** [Sliding Window Maximum (LC 239)](https://leetcode.com/problems/sliding-window-maximum/)

### Sprint 5: Binary Search on Answer Space (ROI Rank #3)
- 🟢 **Tier 1 (Warm-Up):** [Binary Search (LC 704)](https://leetcode.com/problems/binary-search/)
- 🟡 **Tier 2 (FAANG Medium):** [Koko Eating Bananas (LC 875)](https://leetcode.com/problems/koko-eating-bananas/)
- 🟡 **Tier 2 (FAANG Medium):** [Search in Rotated Sorted Array (LC 33)](https://leetcode.com/problems/search-in-rotated-sorted-array/)
- 🔴 **Tier 3 (Hard Anchor):** [Median of Two Sorted Arrays (LC 4)](https://leetcode.com/problems/median-of-two-sorted-arrays/)

### Sprint 6: Linked Lists & Multi-Pointer Navigation (ROI Rank #7)
- 🟢 **Tier 1 (Warm-Up):** [Reverse Linked List (LC 206)](https://leetcode.com/problems/reverse-linked-list/)
- 🟡 **Tier 2 (FAANG Medium):** [Reorder List (LC 143)](https://leetcode.com/problems/reorder-list/)
- 🟡 **Tier 2 (FAANG Medium):** [Linked List Cycle II (LC 142)](https://leetcode.com/problems/linked-list-cycle-ii/)
- 🔴 **Tier 3 (Hard Anchor):** [Merge k Sorted Lists (LC 23)](https://leetcode.com/problems/merge-k-sorted-lists/)

### Sprint 7: Trees & Bottom-Up DFS (ROI Rank #10)
- 🟢 **Tier 1 (Warm-Up):** [Maximum Depth of Binary Tree (LC 104)](https://leetcode.com/problems/maximum-depth-of-binary-tree/)
- 🟡 **Tier 2 (FAANG Medium):** [Lowest Common Ancestor of a Binary Tree (LC 236)](https://leetcode.com/problems/lowest-common-ancestor-of-a-binary-tree/)
- 🟡 **Tier 2 (FAANG Medium):** [Validate Binary Search Tree (LC 98)](https://leetcode.com/problems/validate-binary-search-tree/)
- 🔴 **Tier 3 (Hard Anchor):** [Binary Tree Maximum Path Sum (LC 124)](https://leetcode.com/problems/binary-tree-maximum-path-sum/)
- 🔴 **Tier 3 (Hard Anchor):** [Serialize and Deserialize Binary Tree (LC 297)](https://leetcode.com/problems/serialize-and-deserialize-binary-tree/)

### Sprint 8: Heaps & Top-K Streaming (ROI Rank #5)
- 🟢 **Tier 1 (Warm-Up):** [Kth Largest Element in a Stream (LC 703)](https://leetcode.com/problems/kth-largest-element-in-a-stream/)
- 🟡 **Tier 2 (FAANG Medium):** [Kth Largest Element in an Array (LC 215)](https://leetcode.com/problems/kth-largest-element-in-an-array/)
- 🟡 **Tier 2 (FAANG Medium):** [Top K Frequent Elements (LC 347)](https://leetcode.com/problems/top-k-frequent-elements/)
- 🔴 **Tier 3 (Hard Anchor):** [Find Median from Data Stream (LC 295)](https://leetcode.com/problems/find-median-from-data-stream/)

### Sprint 9: Graphs (BFS/DFS, Topological Sort, DSU) (ROI Rank #4)
- 🟢 **Tier 1 (Warm-Up):** [Number of Islands (LC 200)](https://leetcode.com/problems/number-of-islands/)
- 🟡 **Tier 2 (FAANG Medium):** [Course Schedule (LC 207)](https://leetcode.com/problems/course-schedule/)
- 🟡 **Tier 2 (FAANG Medium):** [Clone Graph (LC 133)](https://leetcode.com/problems/clone-graph/)
- 🔴 **Tier 3 (Hard Anchor):** [Word Ladder (LC 127)](https://leetcode.com/problems/word-ladder/)

### Sprint 10: Dynamic Programming (1D & 2D Transitions) (ROI Rank #2)
- 🟢 **Tier 1 (Warm-Up):** [Climbing Stairs (LC 70)](https://leetcode.com/problems/climbing-stairs/)
- 🟡 **Tier 2 (FAANG Medium):** [Coin Change (LC 322)](https://leetcode.com/problems/coin-change/)
- 🟡 **Tier 2 (FAANG Medium):** [Longest Increasing Subsequence (LC 300)](https://leetcode.com/problems/longest-increasing-subsequence/)
- 🔴 **Tier 3 (Hard Anchor):** [Edit Distance (LC 72)](https://leetcode.com/problems/edit-distance/)
