# 🧪 Array Traversal Drills v5

**Goal:** Build speed and correctness with targeted drills by pattern. Each drill includes a LeetCode number (when available), a hint, and a follow-up question.

---

## ✅ Level 1 Drills (Physical Movement)

| Drill | LeetCode # | Hint | Follow-up question |
| --- | --- | --- | --- |
| Linear search | N/A (Custom) | Scan left to right and stop early on match. | Can you return the first and last index in one pass? |
| Find max and min in one pass | N/A (Custom) | Track both best values as you scan. | Can you do it with one comparison per element? |
| Count frequency of each value | N/A (Custom) | Use a dictionary or frequency array. | What changes if values are in a small range? |
| Copy array into another array | N/A (Custom) | Read once, write once. | Can you do it with a stride or reversed copy? |
| Check if array is sorted | LC 896 (Monotonic Array) | Track last value and detect violations. | Can you short-circuit at the first violation? |

---

## ✅ Level 2 Drills (Index Arithmetic)

| Drill | LeetCode # | Hint | Follow-up question |
| --- | --- | --- | --- |
| Reverse an array in-place | LC 344 (Reverse String) | Swap symmetric pairs while L < R. | Can you do it with one pointer and index math? |
| Sum every k-th element | N/A (Custom) | Use stride `i += k`. | What if k can be 0 or negative? |
| Rotate array by k | LC 189 (Rotate Array) | Use modulo and reverse trick. | Can you do it in O(1) extra space? |
| Circular queue simulation | LC 622 (Design Circular Queue) | Track head, tail, and count. | How do you distinguish empty vs full? |
| 2D to 1D mapping checks | LC 74 (Search a 2D Matrix) | idx = r * cols + c. | Can you map back (r, c) from idx? |

---

## ✅ Level 3 Drills (Multi-Cursor)

| Drill | LeetCode # | Hint | Follow-up question |
| --- | --- | --- | --- |
| Remove element (in-place) | LC 27 (Remove Element) | Reader scans, writer overwrites. | Can you return the new length only? |
| Remove duplicates from sorted array | LC 26 (Remove Duplicates) | Write when value changes. | Can you allow at most two duplicates? |
| Two sum (sorted) | LC 167 (Two Sum II) | Move the pointer that makes sum closer. | Can you return 1-based indices? |
| Merge two sorted arrays | LC 88 (Merge Sorted Array) | Write from the end to avoid overwrite. | Can you do it without extra space? |
| Partition by parity | LC 905 (Sort Array By Parity) | Use two pointers or compaction. | Can you keep the order stable? |

---

## ✅ Level 4 Drills (Range/Window)

| Drill | LeetCode # | Hint | Follow-up question |
| --- | --- | --- | --- |
| Max sum of k-size window | LC 643 (Maximum Average Subarray I) | Add incoming, subtract outgoing. | Can you track the max index too? |
| Longest substring without repeats | LC 3 (Longest Substring) | Shrink window when duplicate appears. | Can you return the substring itself? |
| Minimum size subarray sum | LC 209 (Min Subarray Sum) | Expand then shrink while valid. | How does it change with negatives? |
| Count subarrays with sum k | LC 560 (Subarray Sum Equals K) | Prefix sum + hashmap. | Can you return all ranges, not just count? |
| Longest run of 1s with at most k zeros | LC 1004 (Max Consecutive Ones III) | Window valid if zero_count <= k. | Can you handle streaming input? |

---

## ✅ Level 5 Drills (Abstract Traversal)

| Drill | LeetCode # | Hint | Follow-up question |
| --- | --- | --- | --- |
| Jump game (reachability) | LC 55 (Jump Game) | Maintain farthest reachable index. | Can you return the minimum jumps too? |
| Binary search for exact match | LC 704 (Binary Search) | Shrink [L, R] every step. | What changes for descending arrays? |
| Lower bound (first >= target) | LC 35 (Search Insert Position) | Use right = n (half-open). | Can you also compute upper bound? |
| Answer space search (Koko) | LC 875 (Koko Eating Bananas) | Define monotonic ok(x). | Can you prove ok(x) is monotone? |
| Capacity to ship packages | LC 1011 (Ship Packages) | Greedy check + binary search. | Can you tighten low/high bounds? |

---

## ✅ Level 6 Drills (Prefix/Suffix)

| Drill | LeetCode # | Hint | Follow-up question |
| --- | --- | --- | --- |
| Prefix sums array | N/A (Custom) | pref[i] = pref[i-1] + a[i]. | Can you make it size n+1? |
| Range sum query with prefix | LC 303 (Range Sum Query) | sum(L,R) = pref[R+1] - pref[L]. | How do you support updates? |
| Product of array except self | LC 238 (Product Except Self) | Left pass, right pass. | Can you do it without division? |
| Suffix max array | N/A (Custom) | Scan from right, keep running max. | What if you need suffix min too? |
| Split array into equal sum | LC 724 (Find Pivot Index) | Track left sum and total sum. | Can you return all valid pivots? |

---

## ✅ Level 7 Drills (Monotonic Stack)

| Drill | LeetCode # | Hint | Follow-up question |
| --- | --- | --- | --- |
| Next greater element | LC 496 (Next Greater Element I) | Stack holds decreasing values. | Can you make it circular? |
| Daily temperatures | LC 739 (Daily Temperatures) | Pop while current is warmer. | Can you solve with a reverse scan? |
| Next smaller element | N/A (Custom) | Stack holds increasing values. | Can you adapt to next smaller to left? |
| Stock span | LC 901 (Online Stock Span) | Pop until greater remains. | How do you support streaming input? |
| Largest rectangle in histogram | LC 84 (Largest Rectangle) | Use stack for previous/next smaller. | Can you derive max area in one pass? |

---

## ✅ Level 8 Drills (Partition)

| Drill | LeetCode # | Hint | Follow-up question |
| --- | --- | --- | --- |
| Sort colors (0, 1, 2) | LC 75 (Sort Colors) | Maintain low, mid, high. | Why do we not increment mid on high swap? |
| Partition by pivot | LC 2161 (Partition Array by Pivot) | Build left, equal, right regions. | Can you do it in-place? |
| Move zeroes to end | LC 283 (Move Zeroes) | Compaction with writer pointer. | Can you keep relative order? |
| Three-way partition by key range | N/A (Custom) | Keep unknown region and shrink it. | What changes if keys are strings? |
| Stable vs unstable partition | N/A (Custom) | Stable keeps original order. | When is stable partition required? |

---

## ✅ Level 9 Drills (Cache/Throughput)

| Drill | LeetCode # | Hint | Follow-up question |
| --- | --- | --- | --- |
| Chunked sum on large arrays | N/A (Custom) | Process contiguous blocks. | How does chunk size affect cache hits? |
| Batch transformation (process in blocks) | N/A (Custom) | Avoid random jumps in memory. | Can you pipeline multiple passes? |
| Compare sequential vs random access | N/A (Custom) | Measure time, same data size. | Why is sequential faster? |
| Analyze CPU cache effects (write notes) | N/A (Custom) | Compare timings and write observations. | What evidence supports your conclusion? |

---

## 📌 Optional Problem Set (Mixed)

| Problem | LeetCode # | Hint | Follow-up question |
| --- | --- | --- | --- |
| Best time to buy and sell stock | LC 121 (Best Time to Buy and Sell Stock) | Track min price and max profit. | Can you do it with one pass? |
| Trapping rain water | LC 42 (Trapping Rain Water) | Two pointers or stack. | Can you explain the invariant for two pointers? |
| Subarray product less than k | LC 713 (Subarray Product < K) | Sliding window with product. | What changes when k <= 1? |
| Find first and last position | LC 34 (First/Last Position) | Two binary searches. | Can you implement lower/upper bounds? |
| Kth missing positive number | LC 1539 (Kth Missing Positive) | Binary search on missing count. | Can you derive the count formula? |
| Longest consecutive sequence | LC 128 (Longest Consecutive) | Use a set and expand from starts. | Can you keep it O(n)? |
| Rotate image | LC 48 (Rotate Image) | Layer-by-layer swap. | Can you do it in-place? |

---

## 📘 Extended Practice Set (Curated, Non-Overlapping)

These are specific, well-known problems that map cleanly to the patterns above.

### 🟢 Level 1

| Topic | Problem | LeetCode # | Time | Invariant / Goal | Hint | Follow-up question |
| --- | --- | --- | --- | --- | --- | --- |
| Forward scan | Find Pivot Index | LC 724 | 15m | left_sum equals sum of 0..i-1. | Track total and left sum. | Can you return all pivots? |
| Backward scan | Duplicate Zeros | LC 1089 | 20m | Shift from end without overwriting unread data. | Count zeros then write from end. | Can you do it without extra array? |
| Stride + matrix | Transpose Matrix | LC 867 | 10m | Map (r,c) to (c,r) correctly. | Use nested loops. | What if matrix is square in-place? |
| Boundaries | Merge Sorted Array | LC 88 | 20m | Fill from end to avoid shifting. | Write pointer from the back. | Can you do it in-place? |
| Edge cases | Plus One | LC 66 | 15m | Carry handled safely for index 0. | Propagate carry from end. | What if all digits are 9? |
| Lockstep | Intersection of Two Arrays II | LC 350 | 15m | Pointers advance by comparison. | Sort then two pointers. | Can you do it with counts instead? |

### 🔵 Level 2

| Topic | Problem | LeetCode # | Time | Invariant / Goal | Hint | Follow-up question |
| --- | --- | --- | --- | --- | --- | --- |
| Cyclic | Circular Array Loop | LC 457 | 40m | Next index normalized into 0..n-1. | Normalize with modulo. | Can you detect direction changes? |
| Flattening | Reshape the Matrix | LC 566 | 15m | count maps to (count / c, count % c). | Use a linear counter. | How do you handle invalid reshape? |
| Simulation | Spiral Matrix | LC 54 | 30m | Boundaries shrink correctly each layer. | Keep top, bottom, left, right. | How do you stop without duplicates? |
| Simulation | Spiral Matrix II | LC 59 | 25m | Write 1..n^2 with correct bounds. | Same boundaries, but writing. | Can you generalize to non-square? |

### 🟠 Level 3

| Topic | Problem | LeetCode # | Time | Invariant / Goal | Hint | Follow-up question |
| --- | --- | --- | --- | --- | --- | --- |
| Converging | Valid Palindrome | LC 125 | 10m | Outside [L,R] already matches rules. | Skip non-alphanumerics. | Can you do it without extra string? |
| Converging | Container With Most Water | LC 11 | 30m | Move the shorter side. | Area limited by min height. | Why is moving taller side never better? |

### 🟣 Level 4

| Topic | Problem | LeetCode # | Time | Invariant / Goal | Hint | Follow-up question |
| --- | --- | --- | --- | --- | --- | --- |
| Fixed window | Permutation in String | LC 567 | 30m | Window freq matches target. | Use fixed-size freq array. | How do you speed up comparisons? |
| Prefix 2D | Range Sum Query 2D - Immutable | LC 304 | 30m | Inclusion-exclusion works per query. | Build 2D prefix sums. | How do you handle negative values? |

### 🔴 Level 5

| Topic | Problem | LeetCode # | Time | Invariant / Goal | Hint | Follow-up question |
| --- | --- | --- | --- | --- | --- | --- |
| Answer space | Aggressive Cows (GFG) | N/A (GFG) | 35m | Can(dist) is monotone. | Greedy placement for check. | Can you prove monotonicity? |
| Rotated BS | Search in Rotated Sorted Array | LC 33 | 30m | One half is sorted each step. | Compare nums[L], nums[mid]. | How do you handle duplicates? |
