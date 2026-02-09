# 🧪 Flow-Wise Array Mastery — Practice Set (15–25 Problems)
**Design:** Problems mapped to each subtopic with:
- ⏱️ suggested timebox
- 🧾 expected invariant (what must stay true)

**Rule:** If you can’t state the invariant, you haven’t learned the pattern yet.

---

# 🟢 Level 1 — Physical Layer (Basic Movement)

## 1A Forward / Backward / Stride
1) ✅ Find the maximum element and its first index
- Subtopic: Forward iteration
- ⏱️ 10–15 min
- 🧾 Invariant: best-so-far is max of arr[0..i] and idx is earliest max.

2) ✅ Reverse an array in-place
- Subtopic: Two-end movement foundation
- ⏱️ 10–15 min
- 🧾 Invariant: prefix and suffix are already reversed; middle is unprocessed.

3) ✅ Sum of elements at even indices
- Subtopic: Stride iteration
- ⏱️ 5–10 min
- 🧾 Invariant: sum equals sum of visited even indices so far.

## 1B Boundary control
4) ✅ Implement slice loop [L, R) and compute its sum
- Subtopic: Half-open boundaries
- ⏱️ 10–15 min
- 🧾 Invariant: i stays in [L, R); sum matches arr[L..i-1].

## 1C Edge cases
5) ✅ Safe min function (empty array behavior defined)
- Subtopic: Empty & single handling
- ⏱️ 10–15 min
- 🧾 Invariant: never read arr[0] if n==0.

## 1D Lockstep
6) ✅ Compare two arrays for equality
- Subtopic: Lockstep iteration
- ⏱️ 10–15 min
- 🧾 Invariant: all positions < i are equal.

---

# 🔵 Level 2 — Logical Layer (Index Arithmetic)

## 2A Modular / cyclic
7) ✅ Rotate array by k (extra array version)
- Subtopic: Modular arithmetic
- ⏱️ 20–30 min
- 🧾 Invariant: new[(i+k)%n] == old[i] for processed i.

8) ✅ Circular array: next greater element (wrap-around practice)
- Subtopic: Wrap-around indexing
- ⏱️ 35–45 min
- 🧾 Invariant: indices treated as i % n; candidates maintained in a monotone structure.

## 2B Offset math
9) ✅ Replace each element with difference to next element
- Subtopic: Offset indexing
- ⏱️ 15–20 min
- 🧾 Invariant: never read arr[i+1] unless i+1 < n.

## 2C 2D ↔ 1D
10) ✅ Search a 2D matrix (treat as flattened sorted array)
- Subtopic: 2D-to-1D mapping
- ⏱️ 30–45 min
- 🧾 Invariant: mid maps to (row,col) correctly; search boundaries shrink each step.

---

# 🟠 Level 3 — Multi-View Layer (Dual Pointers)

## 3A Reader/Writer (compaction)
11) ⭐ Remove Element
- Subtopic: Reader/Writer overwrite
- ⏱️ 15–20 min
- 🧾 Invariant: nums[0..write-1] contains all kept elements (order preserved).

12) ⭐ Move Zeroes
- Subtopic: Reader/Writer overwrite
- ⏱️ 20–25 min
- 🧾 Invariant: all non-zero elements compacted into prefix [0..write-1].

13) ⭐ Remove Duplicates from Sorted Array
- Subtopic: Reader/Writer uniqueness
- ⏱️ 20–25 min
- 🧾 Invariant: prefix is unique and sorted.

## 3B Converging pointers
14) ⭐ Two Sum II (sorted)
- Subtopic: Converging pointers
- ⏱️ 20–30 min
- 🧾 Invariant: all pairs outside [l,r] are impossible given moves.

15) ⭐ Valid Palindrome
- Subtopic: Converging pointers
- ⏱️ 15–20 min
- 🧾 Invariant: outside [l,r] already matches under the rules.

16) ⭐ Merge Sorted Array
- Subtopic: Backward merge
- ⏱️ 25–35 min
- 🧾 Invariant: suffix of output is correct and final after each step.

## 3C Diverging pointers
17) ⭐ Longest Palindromic Substring
- Subtopic: Expand from center
- ⏱️ 35–45 min
- 🧾 Invariant: window remains palindrome while expanding.

---

# 🟣 Level 4 — Range Layer (Windows / Prefix / Deque)

## 4A Fixed window
18) ⭐ Maximum Average Subarray I
- Subtopic: Fixed sliding window
- ⏱️ 15–25 min
- 🧾 Invariant: current sum equals exact k-window sum.

## 4B Variable window
19) ⭐ Longest Substring Without Repeating Characters
- Subtopic: Variable window + map
- ⏱️ 35–45 min
- 🧾 Invariant: window contains no duplicates; left never moves backward.

20) ⭐ Minimum Window Substring
- Subtopic: Variable window + freq constraints
- ⏱️ 45–75 min
- 🧾 Invariant: window counts reflect exactly what’s inside; shrink only while still valid.

## 4C Prefix sums
21) ⭐ Range Sum Query - Immutable
- Subtopic: Prefix sums
- ⏱️ 20–30 min
- 🧾 Invariant: pref[i+1] = pref[i] + arr[i].

## 4D Prefix sum + HashMap
22) ⭐ Subarray Sum Equals K
- Subtopic: Prefix sum + hashmap
- ⏱️ 35–45 min
- 🧾 Invariant: freq[p] equals number of prefixes with sum p seen so far.

## 4E Monotonic deque
23) ⭐ Sliding Window Maximum
- Subtopic: Monotonic deque
- ⏱️ 45–75 min
- 🧾 Invariant: deque indices are in-window; values decrease front→back.

---

# 🔴 Level 5 — Abstract Layer (Partition / Search)

## 5A / 5B Partitioning
24) ⭐ Sort Colors
- Subtopic: Dutch National Flag (3-way partition)
- ⏱️ 30–45 min
- 🧾 Invariant: 0-region, 1-region, unknown, 2-region boundaries remain correct.

25) ⭐ Kth Largest Element in an Array
- Subtopic: Partition selection
- ⏱️ 45–60 min
- 🧾 Invariant: pivot is in final position; recurse only into side containing kth.

## 5C Binary search
26) ⭐ Search Insert Position
- Subtopic: Binary search / lower bound
- ⏱️ 15–25 min
- 🧾 Invariant: answer remains within current boundaries.

## 5D Bounds
27) ⭐ Find First and Last Position of Element in Sorted Array
- Subtopic: lower/upper bound
- ⏱️ 35–50 min
- 🧾 Invariant: lower = first >= x, upper = first > x.

## 5E Binary search on answer
28) ⭐ Capacity To Ship Packages Within D Days
- Subtopic: Binary search on answer
- ⏱️ 45–60 min
- 🧾 Invariant: Can(capacity) is monotone.

29) ⭐ Split Array Largest Sum
- Subtopic: Binary search on answer
- ⏱️ 60–90 min
- 🧾 Invariant: Can(maxSum) is monotone; greedy partition count is correct.
