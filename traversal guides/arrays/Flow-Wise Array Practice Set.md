# **🧪 Flow-Wise Array Mastery: The Practice Set**

**How to use this set:**

1. **Timebox:** Stick to the suggested time. If you fail, read the solution, wait 2 hours, and try again.  
2. **The Invariant Rule:** You have NOT solved the problem unless you can write down the **Invariant** (the logic that remains true after every loop iteration).

## ---

**🟢 Level 1: The Physical Layer (Movement & Boundaries)**

*Focus: Clean loops, no off-by-one errors, safe edge cases.*

| Topic | Problem | Difficulty | Time | Core Invariant / Goal |
| :---- | :---- | :---- | :---- | :---- |
| **1A. Forward** | [Find Pivot Index](https://leetcode.com/problems/find-pivot-index/) | Easy | 15m | left\_sum equals sum of 0 to i-1. |
| **1A. Backward** | ([https://leetcode.com/problems/duplicate-zeros/](https://leetcode.com/problems/duplicate-zeros/)) | Easy | 20m | Iterate backwards to shift without overwriting unread data. |
| **1A. Stride** | ([https://leetcode.com/problems/transpose-matrix/](https://leetcode.com/problems/transpose-matrix/)) | Easy | 10m | Map (r, c) to (c, r) using nested loops correctly. |
| **1B. Boundaries** | ([https://leetcode.com/problems/merge-sorted-array/](https://leetcode.com/problems/merge-sorted-array/)) | Easy | 20m | Fill from the **end** (index m+n-1) to avoid shifting. |
| **1C. Edge Cases** | [Plus One](https://leetcode.com/problems/plus-one/) | Easy | 15m | Handle the carry-over at index 0 (array resizing). |
| **1D. Lockstep** | ([https://leetcode.com/problems/intersection-of-two-arrays-ii/](https://leetcode.com/problems/intersection-of-two-arrays-ii/)) | Easy | 15m | Increment pointers i and j based on comparison. |

## ---

**🔵 Level 2: The Logical Layer (Index Math)**

*Focus: Modulo arithmetic, Flattening, Simulation.*

| Topic | Problem | Difficulty | Time | Core Invariant / Goal |
| :---- | :---- | :---- | :---- | :---- |
| **2A. Cyclic** | ([https://leetcode.com/problems/rotate-array/](https://leetcode.com/problems/rotate-array/)) | Medium | 25m | (i \+ k) % n places element in correct spot. Use **Reverse** logic. |
| **2A. Cyclic** | [Circular Array Loop](https://leetcode.com/problems/circular-array-loop/) | Medium | 40m | Next index is ((i \+ nums\[i\]) % n \+ n) % n. |
| **2B. Flattening** | ([https://leetcode.com/problems/search-a-2d-matrix/](https://leetcode.com/problems/search-a-2d-matrix/)) | Medium | 20m | Treat (r, c) as index \= r \* cols \+ c for Binary Search. |
| **2B. Flattening** | ([https://leetcode.com/problems/reshape-the-matrix/](https://leetcode.com/problems/reshape-the-matrix/)) | Easy | 15m | count maps to (count / c, count % c). |
| **2C. Spiral** | ([https://leetcode.com/problems/spiral-matrix/](https://leetcode.com/problems/spiral-matrix/)) | Medium | 30m | Simulation with 4 boundaries (top, bottom, left, right). |
| **2C. Spiral** | ([https://leetcode.com/problems/spiral-matrix-ii/](https://leetcode.com/problems/spiral-matrix-ii/)) | Medium | 25m | Same simulation, but writing values 1..n^2. |

## ---

**🟠 Level 3: The Multi-View Layer (Dual Pointers)**

*Focus: In-place modification, Sorted array tricks, Partitioning.*

| Topic | Problem | Difficulty | Time | Core Invariant / Goal |
| :---- | :---- | :---- | :---- | :---- |
| **3A. Read/Write** | ([https://leetcode.com/problems/remove-duplicates-from-sorted-array/](https://leetcode.com/problems/remove-duplicates-from-sorted-array/)) | Easy | 15m | nums\[0..write\] is always unique sorted prefix. |
| **3A. Read/Write** | [Move Zeroes](https://leetcode.com/problems/move-zeroes/) | Easy | 10m | nums\[0..write-1\] contains all non-zeroes found so far. |
| **3B. Converging** | ([https://leetcode.com/problems/two-sum-ii-input-array-is-sorted/](https://leetcode.com/problems/two-sum-ii-input-array-is-sorted/)) | Medium | 15m | Sum \> Target implies Right is too big (decrement R). |
| **3B. Converging** | [Valid Palindrome](https://leetcode.com/problems/valid-palindrome/) | Easy | 10m | Characters at L and R match (skipping non-alphanumeric). |
| **3B. Converging** | ([https://leetcode.com/problems/container-with-most-water/](https://leetcode.com/problems/container-with-most-water/)) | Medium | 30m | Area limited by shorter line. Always move the shorter pointer. |
| **3C. DNF** | ([https://leetcode.com/problems/sort-colors/](https://leetcode.com/problems/sort-colors/)) | Medium | 25m | \[0..low-1\]=0, \[high+1..end\]=2. Mid scans the unknown. |

## ---

**🟣 Level 4: The Range Layer (Windows & Prefixes)**

*Focus: Subarrays, Substrings, Range Queries.*

| Topic | Problem | Difficulty | Time | Core Invariant / Goal |
| :---- | :---- | :---- | :---- | :---- |
| **4A. Fixed Win** | ([https://leetcode.com/problems/maximum-average-subarray-i/](https://leetcode.com/problems/maximum-average-subarray-i/)) | Easy | 15m | Sum \+= new \- old. Window size is always k. |
| **4A. Fixed Win** | ([https://leetcode.com/problems/permutation-in-string/](https://leetcode.com/problems/permutation-in-string/)) | Medium | 30m | Maintain frequency array of window size len(s1). |
| **4B. Var Win** | ([https://leetcode.com/problems/minimum-size-subarray-sum/](https://leetcode.com/problems/minimum-size-subarray-sum/)) | Medium | 20m | Expand while \< target, shrink while \>= target (to optimize). |
| **4B. Var Win** | ([https://leetcode.com/problems/longest-substring-without-repeating-characters/](https://leetcode.com/problems/longest-substring-without-repeating-characters/)) | Medium | 25m | Window \`\` never contains duplicates. |
| **4B. Var Win** | [Max Consecutive Ones III](https://leetcode.com/problems/max-consecutive-ones-iii/) | Medium | 25m | Window valid if zero\_count \<= k. |
| **4C. Prefix** | ([https://leetcode.com/problems/range-sum-query-immutable/](https://leetcode.com/problems/range-sum-query-immutable/)) | Easy | 15m | P\[i\] \= P\[i-1\] \+ nums\[i-1\]. |
| **4C. Prefix** | ([https://leetcode.com/problems/range-sum-query-2d-immutable/](https://leetcode.com/problems/range-sum-query-2d-immutable/)) | Medium | 30m | Sum(D) \= Total \- Top \- Left \+ TopLeftOverlap. |
| **4D. Prefix+Map** | ([https://leetcode.com/problems/subarray-sum-equals-k/](https://leetcode.com/problems/subarray-sum-equals-k/)) | Medium | 30m | Map stores count of PrefixSum. Look for Curr \- K. |

## ---

**🔴 Level 5: The Abstract Layer (Search & Partition)**

*Focus: Logarithmic search, Answer Space.*

| Topic | Problem | Difficulty | Time | Core Invariant / Goal |
| :---- | :---- | :---- | :---- | :---- |
| **5A. BS Basic** | ([https://leetcode.com/problems/binary-search/](https://leetcode.com/problems/binary-search/)) | Easy | 10m | Target is in \`\`. mid excludes half. |
| **5A. BS Bound** | ([https://leetcode.com/problems/search-insert-position/](https://leetcode.com/problems/search-insert-position/)) | Easy | 15m | Find **Lower Bound** (first element \>= target). |
| **5A. BS Bound** | [First & Last Position](https://leetcode.com/problems/find-first-and-last-position-of-element-in-sorted-array/) | Medium | 25m | Run BS twice: once for Lower Bound, once for Upper. |
| **5B. BS Answer** | ([https://leetcode.com/problems/koko-eating-bananas/](https://leetcode.com/problems/koko-eating-bananas/)) | Medium | 30m | Monotonic: If speed S works, S+1 works. Find min S. |
| **5B. BS Answer** | ([https://leetcode.com/problems/capacity-to-ship-packages-within-d-days/](https://leetcode.com/problems/capacity-to-ship-packages-within-d-days/)) | Medium | 30m | Check function: Can we ship with Cap capacity in D days? |
| **5B. BS Answer** | ([https://practice.geeksforgeeks.org/problems/aggressive-cows/1](https://practice.geeksforgeeks.org/problems/aggressive-cows/1)) | Medium | 35m | Maximize the Minimum distance. check(dist) is greedy. |
| **5C. Rotated** | ([https://leetcode.com/problems/search-in-rotated-sorted-array/](https://leetcode.com/problems/search-in-rotated-sorted-array/)) | Medium | 30m | Identify which half is sorted (nums\[L\] \<= nums\[mid\]). |

## ---

**🧠 Self-Assessment Rubric**

After attempting a problem, rate yourself:

* **Passing:** You wrote the code within the time limit AND you can verbally explain the **Invariant** (e.g., "Left pointer always points to the start of the valid window").  
* **Needs Review:** You solved it via trial-and-error, or you struggled with off-by-one errors (Edge cases 1B/1C).  
* **Failing:** You couldn't identify the pattern (e.g., trying DP on a Sliding Window problem).

**Next Step:** Once you complete this list, you are ready for **Matrix DP** and **Graph Algorithms**, as you now possess the fundamental skills to traverse any grid or adjacency list efficiently.