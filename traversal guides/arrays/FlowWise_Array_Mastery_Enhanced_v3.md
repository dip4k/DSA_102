# 🧭 Flow-Wise Array Mastery (v3)
**Goal:** Professional-grade intuition for traversals, bounds, and pointer-based patterns (Level 1 → Level 5).  
**Promise:** If you can state the invariant, you can code the solution.

---

# 🗺️ One-page Level Mapping Index (Arrays)
Use this index to instantly map an array problem to the right **level**, the right **pattern**, and the **expected invariant**.

## ✅ Legend
- 🧾 **Invariant**: statement that stays true after each step.
- 🧠 **State**: what you track (indices, counts, sums, deque).

| Level | Pattern / skill | Typical problems (LeetCode examples) | Expected invariant (one-liner) |
|---|---|---|---|
| L1 | Basic traversal | 217 Contains Duplicate, 485 Max Consecutive Ones | “Everything before i is processed; accumulator describes that prefix.” |
| L1 | Bounds discipline | 53 Maximum Subarray, 121 Best Time to Buy/Sell Stock | “I never read outside bounds; my range meaning is consistent.” |
| L1 | Edge case hygiene | 704 Binary Search (empty), 88 Merge Sorted Array (tiny) | “No arr[0] unless n>0; neighbor access is guarded.” |
| L1 | Lockstep iteration | 350 Intersection of Two Arrays II, 977 Squares of a Sorted Array | “Two cursors stay aligned; processed region is final.” |
| L2 | Circular indexing | 189 Rotate Array, 503 Next Greater Element II | “Every computed index stays in 0..n-1 after normalization.” |
| L2 | Index math / offsets | 238 Product of Array Except Self, 66 Plus One | “All i+offset accesses are safe by design or guarded.” |
| L2 | 2D↔1D mapping | 74 Search a 2D Matrix, 378 Kth Smallest in Sorted Matrix | “Mapping is correct; search space shrinks each step.” |
| L3 | Reader/Writer compaction | 27 Remove Element, 26 Remove Duplicates from Sorted Array | “nums[0..write-1] is final compacted prefix.” |
| L3 | Converging pointers | 167 Two Sum II, 11 Container With Most Water | “Everything outside [l,r] is impossible or already optimal.” |
| L3 | Expand from center | 5 Longest Palindromic Substring, 647 Palindromic Substrings | “During expansion, [l..r] stays valid until mismatch.” |
| L4 | Fixed sliding window | 643 Maximum Average Subarray I, 438 Find All Anagrams | “Window is exactly k; update is -out +in.” |
| L4 | Variable sliding window | 3 Longest Substring Without Repeats, 76 Minimum Window Substring | “Window validity invariant holds; l and r only move forward.” |
| L4 | Prefix sums | 303 Range Sum Query, 560 Subarray Sum Equals K | “pref[i+1]=pref[i]+a[i]; range sum uses subtraction.” |
| L4 | Monotonic deque | 239 Sliding Window Maximum, 862 Shortest Subarray ≥ K | “Deque is monotonic and in-window; front is the answer candidate.” |
| L5 | Partitioning (2-way/3-way) | 75 Sort Colors, 215 Kth Largest Element | “Regions are correct; unknown region shrinks.” |
| L5 | Binary search (bounds) | 704 Binary Search, 34 First/Last Position | “Answer stays inside bounds; bounds shrink every step.” |
| L5 | Binary search on answer | 1011 Ship Packages, 410 Split Array Largest Sum | “Can(x) is monotone; search smallest feasible.” |

---

# 🧠 The ladder: complexity of state
- **L1:** One cursor + boundaries.
- **L2:** Cursor + arithmetic mapping.
- **L3:** Two cursors + invariants.
- **L4:** Two cursors + window state (counts/sums/deque/prefix).
- **L5:** Abstract regions / search space.

---

# 🟢 Level 1: The Physical Layer (Basic Movement)
**Goal:** Muscle memory for accessing memory.

## Topics to know
- 🔁 Forward, backward, stride traversal
- 📏 Inclusive vs exclusive ranges
- 🧩 Empty/single edge cases
- 🧵 Lockstep iteration

## Things to master
- ✅ Off-by-one discipline (you can explain why your loop stops)
- ✅ “R is first invalid” reflex for [L, R)
- ✅ You can dry-run any loop in 20 seconds

---

## 1.1 🔁 Mastering the loop: Forward

**Why:** Forward scans are your default for counting, validating, and building derived results.

**What:** Visit i = 0..n-1 exactly once.

**How (step/flow):**
1) Initialize i = 0
2) Read a[i]
3) Update state
4) i += 1
5) Stop at i == n

**Where:** Contains duplicate, max consecutive ones, min/max, validations.

**When:** Use unless you must write without overwriting unread data.

**Visual diagram**
```text
Index:  0   1   2   3   4
Array: [A] [B] [C] [D] [E]
        ^
        i moves →
State:  seen / best / count updates for prefix [0..i]
```

**Python snippet**
```python
for i in range(len(a)):
    x = a[i]
```

**C# snippet**
```csharp
for (int i = 0; i < a.Length; i++)
{
    int x = a[i];
}
```

**Example (detailed): Max Consecutive Ones (stream-friendly)**
- You maintain a running streak; reset on 0.
- This is used in log/event streams too: consecutive successes/failures.

**Python**
```python
def max_consecutive_ones(nums):
    best = 0
    streak = 0
    for x in nums:
        if x == 1:
            streak += 1
            best = max(best, streak)
        else:
            streak = 0
    return best
```

**Common pitfalls**
- ❌ `i <= n` (reads a[n])
- ❌ reading a[0] without checking n>0 in some patterns

**Tips & tricks**
- 💡 Speak the invariant: “best is correct for prefix processed so far.”

**Practice (LeetCode)**
- 217 Contains Duplicate — Invariant: “set == unique elements in processed prefix.”
- 485 Max Consecutive Ones — Invariant: “streak == consecutive 1s ending at i.”

---

## 1.2 🔁 Mastering the loop: Backward

**Why:** Backward traversal prevents overwrite bugs when you write into the same array.

**What:** Visit i = n-1..0.

**How (step/flow):**
1) i = n-1
2) process a[i]
3) i -= 1
4) stop when i < 0

**Where:** plus one (carry), merge from end, right-to-left DP.

**When:** You rely on future values, or you’re writing from right to left.

**Visual diagram**
```text
Index:  0   1   2   3
Array: [A] [B] [C] [D]
                    ^
                    i moves ←
Write safety: you fill from the end so you don’t overwrite unread data.
```

**Example (detailed): Merge Sorted Array (write from end)**
- Real use: merging two sorted streams into a fixed buffer.

**C# (core idea)**
```csharp
// nums1 has extra space at the end.
int i = m - 1;      // last in nums1 data
int j = n - 1;      // last in nums2
int w = m + n - 1;  // write from end

while (j >= 0)
{
    if (i >= 0 && nums1[i] > nums2[j])
        nums1[w--] = nums1[i--];
    else
        nums1[w--] = nums2[j--];
}
```

**Common pitfalls**
- ❌ starting at i = n (out of bounds)
- ❌ forgetting i can become -1

**Tips**
- 💡 Invariant: “suffix after w is final and sorted.”

**Practice (LeetCode)**
- 88 Merge Sorted Array — Invariant: “suffix written is final.”
- 66 Plus One — Invariant: “carry is resolved for suffix [i..end].”

---

## 1.3 🦘 Mastering the loop: Strides

**Why:** Stride loops model parity, block processing, sampling, and interleaving.

**What:** i = start, start+step, start+2*step...

**How (step/flow):**
1) choose start (0 or 1)
2) choose step (2, k)
3) i += step until i >= n

**Where:** even/odd index tasks, processing pairs, chunk scans.

**When:** the task says “every k-th element” or parity-based rule.

**Visual**
```text
Indices: 0 1 2 3 4 5 6 7
Stride2: ^   ^   ^   ^
        0 → 2 → 4 → 6
```

**Python**
```python
for i in range(0, len(a), 2):
    pass
```

**Pitfalls**
- ❌ step = 0 (infinite loop)

**Tips**
- 💡 Stride + modulo = circular traversal (Level 2).

**Practice (LeetCode)**
- 905 Sort Array By Parity — Invariant: “boundary splits parity groups.”
- 1869 Longer Contiguous Segments of Ones than Zeros — Invariant: “max streaks tracked per symbol.”

---

## 1.4 📏 Boundary control (Inclusive vs Exclusive)

### 1.4.1 ✅ Half-open [L, R)

**Why:** Half-open ranges compose cleanly and make length math trivial.

**What:** L included, R excluded; length = R - L.

**How (step/flow):**
1) i = L
2) while i < R: process i
3) i += 1

**Where:** slicing, windows, binary search bounds.

**When:** you want predictable loop lengths and fewer off-by-ones.

**Visual**
```text
Indices: 0 1 2 3 4 5 6
Range:     [L-----R)
Example: L=2, R=6 → {2,3,4,5}
Length: R-L = 4
```

**Pitfalls**
- ❌ treating R like “last valid index”

**Tips**
- 💡 Say: “R is first invalid index.”

**Practice (LeetCode)**
- 303 Range Sum Query - Immutable — Invariant: “pref uses n+1 half-open indexing.”
- 209 Minimum Size Subarray Sum — Invariant: “window boundaries consistent with definition.”

### 1.4.2 ✅ Inclusive [L, R]

**Why:** Some problems naturally describe inclusive boundaries.

**What:** Both ends included; length = R-L+1.

**How:** loop while i <= R.

**Where:** two-pointer meeting loops, some partition loops.

**When:** your invariant is naturally “answer is in [L,R].”

**Pitfalls**
- ❌ empty range representation is tricky (R can become -1)

---

## 1.5 🧩 Edge case handling (Empty & Single)

**Why:** Many runtime errors are “read before check.”

**What:** Empty means n=0; single means n=1.

**How (step/flow):**
1) If n==0, return early.
2) If algorithm needs neighbors, handle n==1.

**Where:** min/max, neighbor comparisons, binary search initialization.

**When:** always—make it your first test.

**Visual**
```text
n=0: []  -> no valid index
n=1: [X] -> only index 0 valid
```

**Pitfalls**
- ❌ accessing a[0] when n==0

**Tips**
- 💡 Write a personal checklist: empty, single, duplicates, extremes.

**Practice (LeetCode)**
- 704 Binary Search — Invariant: “bounds shrink; no invalid mid access.”
- 121 Best Time to Buy/Sell — Invariant: “minSoFar defined for scanned prefix only.”

---

## 1.6 🧵 Simultaneous iteration (Lockstep)

**Why:** Real tasks often compare aligned streams: data vs labels, expected vs actual.

**What:** Same index i visits multiple arrays simultaneously.

**How (step/flow):**
1) Decide length rule (equal vs min length)
2) For i in 0..k-1: process (a[i], b[i])

**Where:** diffing arrays, dot product, merging aligned metadata.

**When:** the data is aligned by index.

**Visual**
```text
a: [A0 A1 A2 A3]
b: [B0 B1 B2 B3]
     ^  ^  ^  ^
     i moves in lockstep
```

**Pitfalls**
- ❌ assuming same length without checking

**Tips**
- 💡 Invariant: “all positions < i are already validated/merged.”

**Practice (LeetCode)**
- 350 Intersection of Two Arrays II — Invariant: “counts represent remaining matches.”
- 977 Squares of a Sorted Array (two-view) — Invariant: “result fill pointer builds sorted output.”

---

# 🔵 Level 2: The Logical Layer (Index Arithmetic)
**Goal:** Think in circles and grids.

## Topics to know
- 🔄 Modulo/wrap-around
- ➕ Offset and neighbor math
- 🧱 2D↔1D flattening

## Things to master
- ✅ Correct normalization for negative/large steps
- ✅ Mapping tests: idx(0,0)=0, idx(1,0)=cols
- ✅ Safe neighbor access strategy

---

## 2.1 🔄 Modular / cyclic arithmetic (wrap-around)

**Why:** Circular arrays appear in rotations, round-robin scheduling, and “next in cycle” problems.

**What:** Convert any step into a valid index using modulo.

**How (step/flow):**
1) k = k % n
2) next = (i + k) % n
3) prev = (i - k + n) % n

**Where:** rotate array, circular next-greater, circular buffers.

**When:** you see “circular”, “wrap”, “rotate”.

**Visual**
```text
Indices: 0 1 2 3 4
          ^       |
          |_______|
(4 + 1) wraps to 0
```

**Pitfalls**
- ❌ forgetting k %= n
- ❌ negative k without normalization

**Tips**
- 💡 If k can be negative: k = (k % n + n) % n.

**Practice (LeetCode)**
- 189 Rotate Array — Invariant: “each element maps to exactly one destination.”
- 503 Next Greater Element II — Invariant: “stack holds decreasing values; i % n simulates wrap.”

---

## 2.2 ➕ Offset math (relative indexing)

**Why:** Many patterns are “current vs neighbor” or “window endpoints.”

**What:** Use i+offset safely.

**How (step/flow):**
1) compute j = i + offset
2) ensure 0 <= j < n
3) access a[j]

**Where:** adjacent comparisons, in-place transforms, difference arrays.

**When:** repeated neighbor access appears.

**Pitfalls**
- ❌ accessing i+1 at the last index

**Tips**
- 💡 Prefer loop ranges that guarantee validity rather than checking inside.

**Practice (LeetCode)**
- 238 Product of Array Except Self — Invariant: “prefix pass correct up to i; suffix pass correct from end.”
- 66 Plus One — Invariant: “carry handled for suffix; prefix unchanged.”

---

## 2.3 🧱 2D-to-1D mapping (flattening)

**Why:** Matrices are stored linearly; mapping lets you reuse 1D algorithms like binary search.

**What:** idx = row*cols + col; row = idx/cols; col = idx%cols.

**How (step/flow):**
1) pick cols
2) convert between representations
3) apply 1D logic

**Where:** binary search in row-wise sorted matrices.

**When:** matrix is treated as sorted sequence.

**Visual**
```text
Matrix (2x3):                 Flat:
[ a b c ]                     [ a b c d e f ]
[ d e f ]                      0 1 2 3 4 5

(row=1,col=2) -> idx=1*3+2=5 -> flat[5]=f
```

**Practice (LeetCode)**
- 74 Search a 2D Matrix — Invariant: “mid maps correctly; bounds shrink.”
- 378 Kth Smallest in Sorted Matrix — Invariant: “count(x) is monotone; search works.”

---

# 🟠 Level 3: The Multi-View Layer (Two Pointers)
**Goal:** Solve in-place and pair problems with invariants.

## Topics to know
- 🧹 Reader/Writer compaction
- 🦀 Converging pointers
- 🌗 Expand from center

## Things to master
- ✅ You can state the compacted-prefix invariant
- ✅ You never move the wrong pointer in sorted two-pointer problems
- ✅ You can trace a center expansion without confusion

---

## 3.1 🧹 Reader/Writer (overwrite / compaction)

**Why:** Best tool for in-place filtering with stable order.

**What:** read scans; write builds the final prefix.

**How (step/flow):**
1) write = 0
2) for read in 0..n-1:
3) if keep(a[read]): a[write]=a[read]; write++
4) answer is prefix length = write

**Where:** remove element, remove duplicates, move zeros.

**When:** you want O(1) extra space.

**Visual**
```text
read walks through every cell:
[a0 a1 a2 a3 a4]
 ^  ^  ^  ^  ^

write marks next slot in the kept prefix:
[ kept kept kept | unknown ... ]
        ^
      write
```

**Pitfalls**
- ❌ incrementing write when skipping

**Tips**
- 💡 Invariant: “0..write-1 is final and valid.”

**Practice (LeetCode)**
- 27 Remove Element — Invariant: “prefix contains kept values.”
- 26 Remove Duplicates from Sorted Array — Invariant: “prefix is unique.”

---

## 3.2 🦀 Converging pointers (left/right pincer)

**Why:** Sorted structure lets you discard impossible pairs.

**What:** l starts left, r starts right; move inward.

**How (step/flow):**
1) compute metric with (a[l], a[r])
2) decide move based on monotonicity
3) move pointer; repeat while l<r

**Where:** two-sum sorted, container max area.

**When:** sorted array or monotone response exists.

**Visual**
```text
[ 1, 2, 4, 7, 11, 15 ]
  l              r
Move l -> increases sum; move r -> decreases sum
```

**Pitfalls**
- ❌ wrong move direction

**Tips**
- 💡 Speak: “What move can fix the mismatch?”

**Practice (LeetCode)**
- 167 Two Sum II — Invariant: “discarded region cannot contain answer.”
- 11 Container With Most Water — Invariant: “moving shorter side is only chance to improve.”

---

## 3.3 🌗 Expand from center

**Why:** Palindromes and symmetric patterns are easiest from the center.

**What:** choose center(s) and expand outward.

**How (step/flow):**
1) pick center (i,i) and (i,i+1)
2) while valid: expand
3) record best

**Where:** longest palindrome substring, count palindromes.

**When:** symmetry is present.

**Visual**
```text
s = a b c b a
      ^
expand:
    ^   ^
  ^       ^
```

**Pitfalls**
- ❌ forgetting even center

**Practice (LeetCode)**
- 5 Longest Palindromic Substring — Invariant: “expanded window remains palindrome.”
- 647 Palindromic Substrings — Invariant: “each center counts all palindromes.”

---

# 🟣 Level 4: The Range Layer (Windows & Prefix)
**Goal:** Process subarrays/substrings efficiently.

## Topics to know
- 🐛 Fixed sliding window
- 🪗 Variable sliding window
- 🧾 Prefix sums + hash map
- 📉 Monotonic deque

## Things to master
- ✅ Your window definition is explicit (e.g., [l,r] or [l,r)) and never changes mid-solution
- ✅ You can say when sliding window fails (negative numbers) and switch to prefix+map
- ✅ You can maintain deque monotonicity without guessing

### Framework note
A common sliding-window framework is “expand right, then shrink left while condition requires,” using a window defined as [left, right) in many templates. [web:68]

---

## 4.1 🐛 Fixed sliding window

**Why:** Recomputing each window is slow; sliding updates in O(1) per move.

**What:** window size k stays constant.

**How (step/flow):**
1) build first window
2) for each new right: add in, remove out
3) update answer

**Where:** max sum/avg of length k, frequency match of fixed length.

**When:** “subarray of length k” is stated.

**Visual**
```text
K=3
[A B C] D E
 A [B C D] E
 A B [C D E]
Update: sum += in - out
```

**Practice (LeetCode)**
- 643 Maximum Average Subarray I — Invariant: “windowSum == sum of current k elements.”
- 438 Find All Anagrams in a String — Invariant: “freq state matches current window exactly.”

---

## 4.2 🪗 Variable sliding window (accordion)

**Why:** Finds longest/shortest window satisfying constraint in linear time when validity changes monotonically with pointer moves.

**What:** window size changes; maintain a validity invariant.

**How (step/flow):**
1) expand right (include item)
2) while invalid (or while valid for min): shrink left
3) update answer at the correct time

**Where:** longest unique substring, at most k distinct, minimum window substring.

**When:** constraint can be repaired by moving left forward.

**Visual**
```text
Expand → Expand → Expand (becomes invalid)
Shrink ← Shrink (becomes valid)
Update answer
Repeat
```

**Pitfalls**
- ❌ using variable sliding window when negatives break monotonicity

**Practice (LeetCode)**
- 3 Longest Substring Without Repeating Characters — Invariant: “window contains no duplicates.”
- 76 Minimum Window Substring — Invariant: “window covers requirement; shrink while still covered.”

---

## 4.3 🧾 Prefix sums (range queries)

**Why:** After O(n) preprocessing, any range sum query is O(1). [web:69]

**What:** pref[0]=0; pref[i+1]=pref[i]+a[i]. [web:69]

**How (step/flow):**
1) build pref of length n+1
2) sum(L..R) = pref[R+1] - pref[L]

**Where:** range sum queries, converting subarray constraints into prefix differences.

**When:** many range queries or subarray sum counting.

**Visual**
```text
a:    [1, 2, 3, 4]
pref: [0, 1, 3, 6, 10]
sum(1..3) = pref[4] - pref[1]
```

**Practice (LeetCode)**
- 303 Range Sum Query - Immutable — Invariant: “pref uses n+1 indexing.”
- 560 Subarray Sum Equals K — Invariant: “count prefixes with sum (running-k).”

---

## 4.4 📉 Monotonic deque

**Why:** Maintains max/min per window in total O(n).

**What:** Deque of indices kept in monotonic value order.

**How (step/flow):**
1) pop back while new value dominates
2) push right index
3) pop front if out of window
4) front is answer

**Where:** sliding window maximum.

**When:** need max/min for each fixed-size window.

**Visual**
```text
Deque stores indices (not values):
front -> current best candidate
Expire old indices as window moves
```

**Practice (LeetCode)**
- 239 Sliding Window Maximum — Invariant: “deque decreasing; front in window.”
- 862 Shortest Subarray with Sum at Least K — Invariant: “prefix deque increasing; pops preserve optimality.”

---

# 🔴 Level 5: The Abstract Layer (Partition & Search)
**Goal:** Treat arrays as regions/search spaces.

## Topics to know
- 🧱 Partitioning (2-way and 3-way)
- 🔍 Binary search (classic + bounds)
- 🧪 Binary search on answer

## Things to master
- ✅ Region invariants (what is guaranteed about each region)
- ✅ One consistent binary-search boundary style
- ✅ Monotone predicate recognition

---

## 5.1 🧱 Partitioning (2-way)

**Why:** Many problems are “group items by predicate” efficiently.

**What:** Maintain boundary; swap good items to left.

**How:** scan i; if good(a[i]) swap into boundary; boundary++.

**Visual**
```text
[ good | unknown | bad ]
 0..b-1  b..i-1   i..end
```

**Practice (LeetCode)**
- 905 Sort Array By Parity — Invariant: “elements before boundary satisfy predicate.”
- 283 Move Zeroes (swap variant) — Invariant: “non-zeros moved left by boundary.”

---

## 5.2 🇳🇱 3-way partition (DNF)

**Why:** One-pass grouping into three categories (0/1/2 or <,=,> pivot).

**What:** Maintain low/mid/high and shrink unknown region.

**Visual**
```text
[ low | mid | unknown | high ]
0..low-1 low..mid-1 mid..high high+1..n-1
```

**Practice (LeetCode)**
- 75 Sort Colors — Invariant: “regions remain correct after each swap.”
- 215 Kth Largest Element (partition) — Invariant: “pivot final; recurse into correct side.”

---

## 5.3 🔍 Binary search (classic + bounds)

**Why:** Discard half when data is sorted or predicate is monotone.

**What:** Maintain bounds that always contain the answer.

**How:** mid, compare, shrink bounds, ensure progress each step.

**Visual**
```text
[l .... mid .... r]
Keep the side that can still contain the answer
```

**Practice (LeetCode)**
- 704 Binary Search — Invariant: “if target exists, it’s inside bounds.”
- 34 First and Last Position — Invariant: “lower bound first >= x; upper bound first > x.”

---

## 5.4 🧪 Binary search on answer

**Why:** Optimization problems often become “smallest x such that Can(x) is true.”

**What:** Search over numeric answer space.

**How:** define monotone Can(x); binary search smallest feasible.

**Practice (LeetCode)**
- 1011 Capacity To Ship Packages Within D Days — Invariant: “Can(cap) monotone.”
- 410 Split Array Largest Sum — Invariant: “Can(maxSum) monotone; greedy partitions correct.”

---

# 🧰 Traversal mastery add-ons (arrays)

## A) 🧪 Micro-tracing (30-second dry run)
- Draw indices and values.
- Write pointer positions (i, l, r, write).
- After each step, update pointers first, then state.

## B) 🧠 Invariant library (memorize these)
- Reader/Writer: “prefix 0..write-1 is final.”
- Two pointers sorted: “discarded side cannot contain solution.”
- Sliding window: “window state exactly matches current window.”
- Prefix sums: “pref is sum of first i elements; range sum is subtraction.” [web:69]

## C) ✅ Debug checklist
- Did my loop always make progress?
- Did I choose inclusive vs exclusive and keep it consistent?
- Did I test empty + single + duplicates?
- If I use window, does it break with negatives?
