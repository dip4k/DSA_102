# 🧭 One-page Level Mapping Index (Arrays)
Use this index to instantly map an array problem to the right **level**, the right **pattern**, and the **expected invariant**.

## ✅ Legend
- **Invariant** = statement that stays true after each step; if you can’t state it, you don’t own the pattern.
- Problems listed are representative (LeetCode IDs + titles).

| Level | Pattern / skill | Common LeetCode problems (examples) | Expected invariant (one-liner) |
|---|---|---|---|
| L1 | Forward/backward/stride traversal | 217 Contains Duplicate, 485 Max Consecutive Ones, 283 Move Zeroes (scan phase) | “All indices before i are processed exactly once; my accumulator describes that prefix.” |
| L1 | Boundary control ([L,R) vs [L,R]) | 53 Maximum Subarray, 121 Best Time to Buy and Sell Stock | “My loop never reads outside bounds; range meaning is consistent.” |
| L1 | Edge cases (empty/single) | 704 Binary Search (empty), 88 Merge Sorted Array (tiny inputs) | “I never access arr[0] unless n>0; neighbor access is guarded.” |
| L1 | Lockstep (two arrays) | 977 Squares of a Sorted Array (two-view), 349 Intersection of Two Arrays | “Pointers/index i refer to aligned positions; processed part is final.” |
| L2 | Modular / circular indexing | 189 Rotate Array, 503 Next Greater Element II | “Every computed index maps back into [0..n-1] after normalization.” |
| L2 | Offset / relative indexing | 66 Plus One, 238 Product of Array Except Self | “Any access at i+offset is safe by design or guarded.” |
| L2 | 2D↔1D mapping (flatten) | 74 Search a 2D Matrix, 240 Search a 2D Matrix II | “flatIndex ↔ (row,col) mapping is correct; search space shrinks.” |
| L3 | Reader/Writer (overwrite/compaction) | 27 Remove Element, 26 Remove Duplicates from Sorted Array, 283 Move Zeroes | “nums[0..write-1] is the final compacted prefix; read scans remaining.” |
| L3 | Converging pointers (pincer) | 167 Two Sum II, 11 Container With Most Water, 344 Reverse String | “All pairs outside [l,r] are impossible (or already fixed) given pointer moves.” |
| L3 | Expand from center | 5 Longest Palindromic Substring, 647 Palindromic Substrings | “During expansion, [l..r] stays valid (palindrome) until it breaks.” |
| L4 | Fixed sliding window | 643 Maximum Average Subarray I, 438 Find All Anagrams in a String | “Window represents exactly k elements; state updates by -out +in.” |
| L4 | Variable sliding window | 3 Longest Substring Without Repeating Characters, 76 Minimum Window Substring | “Window invariant holds (valid/invalid); l and r only move forward.” |
| L4 | Prefix sums (range queries) | 303 Range Sum Query - Immutable, 560 Subarray Sum Equals K | “pref[i+1]=pref[i]+arr[i]; range sum = pref[r+1]-pref[l].” |
| L4 | Monotonic deque | 239 Sliding Window Maximum, 862 Shortest Subarray with Sum at Least K (variant skills) | “Deque indices are in-window and monotonic; front is always the answer index.” |
| L5 | Partitioning (2-way, 3-way) | 75 Sort Colors, 215 Kth Largest Element in an Array | “Regions remain correct after each swap; unknown region shrinks.” |
| L5 | Binary search (bounds) | 704 Binary Search, 35 Search Insert Position, 34 Find First/Last Position | “Answer stays inside current boundaries; boundaries shrink every step.” |
| L5 | Binary search on answer | 1011 Capacity To Ship Packages Within D Days, 410 Split Array Largest Sum | “Can(x) is monotone; mid selection reduces answer space safely.” |

---

# 📌 Appendix: Problem mappings by level
Use this when you want to *practice exactly what you just learned*.

## 🟢 Level 1 problem mappings
### Forward traversal
- LeetCode 217: Contains Duplicate — Invariant: “seen-set equals unique elements in processed prefix.”
- LeetCode 485: Max Consecutive Ones — Invariant: “currentStreak reflects suffix of 1s ending at i; best reflects max so far.”

### Backward traversal
- LeetCode 88: Merge Sorted Array (write from end) — Invariant: “suffix after writeIdx is final and sorted.”
- LeetCode 66: Plus One (carry from end) — Invariant: “carry handled for suffix i..end; prefix unchanged yet.”

### Stride traversal
- LeetCode 905: Sort Array By Parity (scan + place) — Invariant: “elements before boundary satisfy parity condition.”
- LeetCode 1863: Sum of All Subset XOR Totals (stride-like bit reasoning practice) — Invariant: “bit contributions processed systematically.”

### Boundary control
- LeetCode 53: Maximum Subarray — Invariant: “bestEndingHere is max subarray sum ending at i.”
- LeetCode 121: Best Time to Buy and Sell Stock — Invariant: “minSoFar is min price in prefix; bestProfit uses it.”

### Lockstep (two arrays)
- LeetCode 349: Intersection of Two Arrays — Invariant: “result set equals intersection of processed sets.”
- LeetCode 350: Intersection of Two Arrays II — Invariant: “counts reflect unmatched occurrences.”

## 🔵 Level 2 problem mappings
### Modular / circular indexing
- LeetCode 189: Rotate Array — Invariant: “each element’s destination index is normalized.”
- LeetCode 503: Next Greater Element II — Invariant: “stack holds indices of decreasing values (circular via i % n).”

### Offset / relative indexing
- LeetCode 238: Product of Array Except Self — Invariant: “prefix products correct up to i; suffix pass correct from end.”
- LeetCode 724: Find Pivot Index — Invariant: “leftSum is sum of prefix; totalSum fixed.”

### 2D↔1D mapping
- LeetCode 74: Search a 2D Matrix — Invariant: “mid maps correctly; binary search shrinks.”
- LeetCode 378: Kth Smallest Element in a Sorted Matrix (mapping + search skills) — Invariant: “count(mid) monotone in mid.”

## 🟠 Level 3 problem mappings
### Reader/Writer compaction
- LeetCode 27: Remove Element — Invariant: “nums[0..write-1] contains kept values in order.”
- LeetCode 26: Remove Duplicates from Sorted Array — Invariant: “prefix is unique; write points to next write slot.”

### Converging pointers
- LeetCode 167: Two Sum II — Invariant: “all discarded pairs cannot hit target due to sortedness.”
- LeetCode 11: Container With Most Water — Invariant: “moving shorter side is the only chance to improve height.”

### Expand from center
- LeetCode 5: Longest Palindromic Substring — Invariant: “expansion maintains palindrome until mismatch.”
- LeetCode 647: Palindromic Substrings — Invariant: “each center expansion counts all palindromes for that center.”

## 🟣 Level 4 problem mappings
### Fixed sliding window
- LeetCode 643: Maximum Average Subarray I — Invariant: “windowSum equals sum of current k-window.”
- LeetCode 438: Find All Anagrams in a String — Invariant: “freq state matches exactly the current window.”

### Variable sliding window
- LeetCode 3: Longest Substring Without Repeating Characters — Invariant: “window has all unique chars; left only moves forward.”
- LeetCode 76: Minimum Window Substring — Invariant: “window covers requirements; shrink while still covered.”

### Prefix sums (+ hashmap)
- LeetCode 303: Range Sum Query - Immutable — Invariant: “pref[i+1]=pref[i]+arr[i].”
- LeetCode 560: Subarray Sum Equals K — Invariant: “freq[p] counts prefixes with sum p so far.”

### Monotonic deque
- LeetCode 239: Sliding Window Maximum — Invariant: “deque indices are in range and values decrease front→back.”
- LeetCode 862: Shortest Subarray with Sum at Least K — Invariant: “prefix deque is increasing; popping maintains optimal candidates.”

## 🔴 Level 5 problem mappings
### Partitioning
- LeetCode 75: Sort Colors — Invariant: “0-region | 1-region | unknown | 2-region boundaries stay valid.”
- LeetCode 215: Kth Largest Element in an Array — Invariant: “pivot ends in final position; recurse into correct side.”

### Binary search (bounds)
- LeetCode 704: Binary Search — Invariant: “if target exists, it lies within the current bounds.”
- LeetCode 34: Find First and Last Position — Invariant: “lower bound finds first >= x; upper bound first > x.”

### Binary search on answer
- LeetCode 1011: Capacity To Ship Packages Within D Days — Invariant: “Can(cap) monotone; search smallest feasible.”
- LeetCode 410: Split Array Largest Sum — Invariant: “Can(maxSum) monotone; greedy produces partitions count.”
