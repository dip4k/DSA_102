# 🗺️ Flow-Wise Array Mastery v5 (Final)

**Purpose:** A complete, structured learning guide for array traversal mastery using **what / why / how / visuals / code**.

**Primary languages:** C# and Python

---

## 📌 Table of Contents

- [🗺️ Flow-Wise Array Mastery v5 (Final)](#️-flow-wise-array-mastery-v5-final)
  - [📌 Table of Contents](#-table-of-contents)
  - [🧭 How to Use This Guide](#-how-to-use-this-guide)
  - [🗺️ Learning Roadmap](#️-learning-roadmap)
  - [🗂️ Problem Mapping Index](#️-problem-mapping-index)
  - [🟢 Level 1: Physical Movement](#-level-1-physical-movement)
    - [1A) ➡️ Linear Forward Scan](#1a-️-linear-forward-scan)
    - [1B) ⬅️ Linear Backward Scan](#1b-️-linear-backward-scan)
    - [1C) 🔍 Sentinel Stop (Stop on Condition)](#1c--sentinel-stop-stop-on-condition)
    - [1D) 📏 Half-Open Boundaries](#1d--half-open-boundaries)
    - [1E) 🧵 Lockstep Iteration](#1e--lockstep-iteration)
  - [🔵 Level 2: Index Arithmetic](#-level-2-index-arithmetic)
    - [2A) 🦘 Stride Traversal](#2a--stride-traversal)
    - [2B) 🔄 Cyclic Traversal (Modulo)](#2b--cyclic-traversal-modulo)
    - [2C) 🧱 2D-to-1D Mapping](#2c--2d-to-1d-mapping)
  - [🟠 Level 3: Multi-Cursor Traversal](#-level-3-multi-cursor-traversal)
    - [3A) 🧹 Reader/Writer (Compaction)](#3a--readerwriter-compaction)
    - [3B) 🦀 Converging Pointers](#3b--converging-pointers)
    - [3C) 🔀 Merge-Style Traversal](#3c--merge-style-traversal)
  - [🟣 Level 4: Range/Window Traversal](#-level-4-rangewindow-traversal)
    - [4A) 🪟 Fixed-Size Window](#4a--fixed-size-window)
    - [4B) 🧲 Variable-Size Window](#4b--variable-size-window)
    - [4C) 🧩 Segment Traversal](#4c--segment-traversal)
  - [🔴 Level 5: Abstract Traversal](#-level-5-abstract-traversal)
    - [5A) 🦘 Reachability (Jump Rules)](#5a--reachability-jump-rules)
    - [5B) 🔍 Binary Search (Index Space)](#5b--binary-search-index-space)
    - [5C) 🧪 Binary Search on Answer Space](#5c--binary-search-on-answer-space)
  - [🟤 Level 6: Prefix/Suffix Traversal](#-level-6-prefixsuffix-traversal)
    - [6A) ➕ Prefix Accumulation](#6a--prefix-accumulation)
    - [6B) ➖ Suffix Accumulation](#6b--suffix-accumulation)
    - [6C) 🔄 Two-Pass Carry](#6c--two-pass-carry)
  - [🟧 Level 7: Monotonic Stack Traversal](#-level-7-monotonic-stack-traversal)
    - [7A) 📈 Next Greater Element](#7a--next-greater-element)
  - [🟫 Level 8: In-Place Partition Traversal](#-level-8-in-place-partition-traversal)
    - [8A) 🇳🇱 Three-Way Partition](#8a--three-way-partition)
  - [⚙️ Level 9: Cache/Throughput Traversal](#️-level-9-cachethroughput-traversal)
    - [9A) 📦 Chunked Scans](#9a--chunked-scans)
  - [✅ Pattern Checklist](#-pattern-checklist)
  - [🧪 Practice Map](#-practice-map)

---

## 🧭 How to Use This Guide

- Study one level per sitting.
- Write templates for each pattern in both languages.
- Solve 3 to 5 problems per level before moving on.
- Track invariants. Debug by printing indices and key state.

---

## 🗺️ Learning Roadmap

| Level | Focus | Key Idea | Typical Problems |
| --- | --- | --- | --- |
| 1 | Physical movement | One index | Scan, count, copy |
| 2 | Index arithmetic | One index + math | Rotate, flatten, stride |
| 3 | Multi-cursor | Two indices | Two pointers, merge, compaction |
| 4 | Range/window | Window state | Sliding window, segments |
| 5 | Abstract traversal | Search space | Binary search, reachability |
| 6 | Prefix/suffix | Carry state | Prefix sums, two-pass |
| 7 | Monotonic stack | Deferred resolution | Next greater/smaller |
| 8 | Partition | Region boundaries | Dutch flag, bucket by key |
| 9 | Cache/throughput | Memory locality | Chunked scans |

---

## 🗂️ Problem Mapping Index

Use this index to map a problem to the correct level, pattern, and invariant.

**Legend**
- Invariant: statement that stays true after each step.
- State: what you track (indices, counts, sums, deque).

| Level | Pattern / skill | Typical problems (LeetCode examples) | Expected invariant (one-liner) |
| --- | --- | --- | --- |
| L1 | Basic traversal | 217 Contains Duplicate, 485 Max Consecutive Ones | Everything before i is processed; accumulator describes that prefix. |
| L1 | Bounds discipline | 53 Maximum Subarray, 121 Best Time to Buy/Sell Stock | I never read outside bounds; my range meaning is consistent. |
| L1 | Edge case hygiene | 704 Binary Search (empty), 88 Merge Sorted Array (tiny) | No arr[0] unless n > 0; neighbor access is guarded. |
| L1 | Lockstep iteration | 350 Intersection of Two Arrays II, 977 Squares of a Sorted Array | Two cursors stay aligned; processed region is final. |
| L2 | Circular indexing | 189 Rotate Array, 503 Next Greater Element II | Every computed index stays in 0..n-1 after normalization. |
| L2 | Index math / offsets | 238 Product of Array Except Self, 66 Plus One | All i + offset accesses are safe by design or guarded. |
| L2 | 2D to 1D mapping | 74 Search a 2D Matrix, 378 Kth Smallest in Sorted Matrix | Mapping is correct; search space shrinks each step. |
| L3 | Reader/Writer compaction | 27 Remove Element, 26 Remove Duplicates from Sorted Array | nums[0..write-1] is final compacted prefix. |
| L3 | Converging pointers | 167 Two Sum II, 11 Container With Most Water | Everything outside [l,r] is impossible or already optimal. |
| L3 | Expand from center | 5 Longest Palindromic Substring, 647 Palindromic Substrings | During expansion, [l..r] stays valid until mismatch. |
| L4 | Fixed sliding window | 643 Maximum Average Subarray I, 438 Find All Anagrams | Window is exactly k; update is -out +in. |
| L4 | Variable sliding window | 3 Longest Substring Without Repeats, 76 Minimum Window Substring | Window validity holds; l and r only move forward. |
| L4 | Prefix sums | 303 Range Sum Query, 560 Subarray Sum Equals K | pref[i+1] = pref[i] + a[i]; range sum uses subtraction. |
| L4 | Monotonic deque | 239 Sliding Window Maximum, 862 Shortest Subarray at Least K | Deque is monotonic and in-window; front is the answer candidate. |
| L5 | Partitioning (2-way/3-way) | 75 Sort Colors, 215 Kth Largest Element | Regions are correct; unknown region shrinks. |
| L5 | Binary search (bounds) | 704 Binary Search, 34 First/Last Position | Answer stays inside bounds; bounds shrink every step. |
| L5 | Binary search on answer | 1011 Ship Packages, 410 Split Array Largest Sum | Can(x) is monotone; search smallest feasible. |

---

## 🟢 Level 1: Physical Movement

### 1A) ➡️ Linear Forward Scan

**What:** Visit indices from 0 to n - 1.

**Why:** Default traversal for scans, counts, and transformations.

**How:** Increment i until i == n.

**Visual**
```
Index: 0 1 2 3 4
Array: A C E G K
       ^
       i ->
```

**C#**
```csharp
for (int i = 0; i < arr.Length; i++)
{
    int current = arr[i];
    // process
}
```

**Python**
```python
for i in range(len(arr)):
    current = arr[i]
    # process
```

**Pitfalls**
- Off-by-one errors (`i <= n - 1` is easy to slip)
- Modifying list length during traversal

---

### 1B) ⬅️ Linear Backward Scan

**What:** Visit indices from n - 1 to 0.

**Why:** Prevents overwrite when writing to the right.

**How:** Decrement i until i < 0.

**Visual**
```
Index: 0 1 2 3
Array: A C E G
          ^
          i <-
```

**C#**
```csharp
for (int i = arr.Length - 1; i >= 0; i--)
{
    int current = arr[i];
}
```

**Python**
```python
for i in range(len(arr) - 1, -1, -1):
    current = arr[i]
```

---

### 1C) 🔍 Sentinel Stop (Stop on Condition)

**What:** Traverse until a condition is met.

**Why:** Not all scans need full traversal.

**How:** Check bounds first, then condition.

**Visual**
```
Scan until a[i] < 0
Index: 0 1 2 3 4
Array: 2 4 1 -3 9
             ^ stop
```

**C#**
```csharp
int FirstNegativeIndex(int[] a)
{
    int i = 0;
    while (i < a.Length && a[i] >= 0)
    {
        i++;
    }
    return (i == a.Length) ? -1 : i;
}
```

**Python**
```python
def first_negative_index(a):
    i = 0
    while i < len(a) and a[i] >= 0:
        i += 1
    return -1 if i == len(a) else i
```

---

### 1D) 📏 Half-Open Boundaries

**What:** Use [L, R) ranges.

**Why:** Prevents off-by-one errors and composes well.

**How:** Start at L, stop before R.

**Visual**
```
Index: 0 1 2 3 4 5
Range:   [ L-----R )
```

**C#**
```csharp
void ProcessRange(int[] arr, int left, int right)
{
    for (int i = left; i < right; i++)
    {
        Console.WriteLine(arr[i]);
    }
}
```

---

### 1E) 🧵 Lockstep Iteration

**What:** One index walks two aligned arrays.

**Why:** Comparing or merging aligned data.

**Visual**
```
A: 2 4 6
B: 1 3 5
    ^
    i
```

**C#**
```csharp
int limit = Math.Min(a.Length, b.Length);
for (int i = 0; i < limit; i++)
{
    Console.WriteLine(a[i] + b[i]);
}
```

**Python**
```python
for x, y in zip(a, b):
    print(x + y)
```

---

## 🔵 Level 2: Index Arithmetic

### 2A) 🦘 Stride Traversal

**What:** Visit every k-th element.

**Why:** Sampling or parity scans.

**How:** i += step.

**Visual**
```
Index: 0 1 2 3 4 5 6
Visit: ^   ^   ^   ^   (step=2)
```

**C#**
```csharp
for (int i = 0; i < a.Length; i += step)
{
    sum += a[i];
}
```

**Python**
```python
for i in range(0, len(a), step):
    s += a[i]
```

---

### 2B) 🔄 Cyclic Traversal (Modulo)

**What:** Wrap index with modulo.

**Why:** Circular buffer, rotations.

**How:** (i + 1) % n, (i - 1 + n) % n.

**Visual**
```
Index: 0 1 2 3
Move:  3 -> (3 + 1) % 4 = 0
```

**C#**
```csharp
int NextIndex(int i, int n) => (i + 1) % n;
```

**Python**
```python
def next_index(i, n):
    return (i + 1) % n
```

---

### 2C) 🧱 2D-to-1D Mapping

**What:** Convert (row, col) to linear index.

**Why:** Flatten matrices or binary search a matrix.

**How:** idx = row * cols + col.

**Visual**
```
rows=2, cols=3
(1,0) -> 1*3 + 0 = 3
```

**C#**
```csharp
int Index(int r, int c, int cols) => r * cols + c;
```

**Python**
```python
def index(r, c, cols):
    return r * cols + c
```

---

## 🟠 Level 3: Multi-Cursor Traversal

### 3A) 🧹 Reader/Writer (Compaction)

**What:** Two forward indices, one reads, one writes.

**Why:** In-place filtering (remove zeros, duplicates).

**How:** If reader valid, write to writer and advance both.

**Visual**
```
[1, 0, 2, 0, 3]
 W  R
After:
[1, 2, 3, 0, 0]
    W     R
```

**Python**
```python
def move_zeroes(nums):
    writer = 0
    for reader in range(len(nums)):
        if nums[reader] != 0:
            nums[writer], nums[reader] = nums[reader], nums[writer]
            writer += 1
```

**C#**
```csharp
int RemoveElement(int[] a, int val)
{
    int w = 0;
    for (int r = 0; r < a.Length; r++)
    {
        if (a[r] != val)
        {
            a[w++] = a[r];
        }
    }
    return w; // new length
}
```

---

### 3B) 🦀 Converging Pointers

**What:** Left and right move toward each other.

**Why:** Pair search in sorted arrays.

**How:** Adjust left/right based on condition.

**Visual**
```
[1, 3, 5, 8] target=8
 L        R
1+8=9 -> R--
 L     R
1+5=6 -> L++
   L  R
3+5=8 found
```

**C#**
```csharp
int[] TwoSumSorted(int[] numbers, int target)
{
    int left = 0, right = numbers.Length - 1;
    while (left < right)
    {
        int sum = numbers[left] + numbers[right];
        if (sum == target) return new[] { left + 1, right + 1 };
        if (sum < target) left++;
        else right--;
    }
    return Array.Empty<int>();
}
```

---

### 3C) 🔀 Merge-Style Traversal

**What:** Two indices for two sorted arrays.

**Why:** Merge or intersection in linear time.

**How:** Advance the pointer you consume.

**C#**
```csharp
int CountCommonSorted(int[] a, int[] b)
{
    int i = 0, j = 0, count = 0;
    while (i < a.Length && j < b.Length)
    {
        if (a[i] == b[j]) { count++; i++; j++; }
        else if (a[i] < b[j]) i++;
        else j++;
    }
    return count;
}
```

**Python**
```python
def count_common_sorted(a, b):
    i = j = count = 0
    while i < len(a) and j < len(b):
        if a[i] == b[j]:
            count += 1
            i += 1
            j += 1
        elif a[i] < b[j]:
            i += 1
        else:
            j += 1
    return count
```

---

## 🟣 Level 4: Range/Window Traversal

### 4A) 🪟 Fixed-Size Window

**What:** Maintain state for every size-k subarray.

**Why:** Efficient rolling aggregates.

**How:** Add entering element, remove leaving element.

**Visual**
```
idx:  0 1 2 3 4 5
k=3: [0 1 2] -> [1 2 3] -> [2 3 4]
```

**C#**
```csharp
int MaxWindowSum(int[] a, int k)
{
    int sum = 0;
    for (int i = 0; i < k; i++) sum += a[i];

    int best = sum;
    for (int r = k; r < a.Length; r++)
    {
        sum += a[r];
        sum -= a[r - k];
        if (sum > best) best = sum;
    }
    return best;
}
```

---

### 4B) 🧲 Variable-Size Window

**What:** Expand right, shrink left to restore invariant.

**Why:** Longest/shortest subarray under constraint.

**How:** Update state when moving L/R.

**C# Template**
```csharp
int L = 0;
for (int R = 0; R < a.Length; R++)
{
    // include a[R]
    while (/* invalid */)
    {
        // remove a[L]
        L++;
    }
    // window [L..R] valid here
}
```

**Python Template**
```python
left = 0
for right in range(len(a)):
    # include a[right]
    while is_invalid():
        # remove a[left]
        left += 1
```

---

### 4C) 🧩 Segment Traversal

**What:** Process contiguous blocks (runs).

**Why:** Useful for run-length logic or grouping.

**How:** Extend right until value changes.

**C#**
```csharp
void ProcessRuns(int[] a)
{
    int i = 0;
    while (i < a.Length)
    {
        int start = i;
        int value = a[i];
        while (i < a.Length && a[i] == value) i++;
        int end = i; // [start, end)
        // process run
    }
}
```

---

## 🔴 Level 5: Abstract Traversal

### 5A) 🦘 Reachability (Jump Rules)

**What:** Explore what indices are reachable.

**Why:** Some arrays define transitions.

**How:** Maintain farthest reachable index.

**C#**
```csharp
bool CanReachEnd(int[] a)
{
    int farthest = 0;
    for (int i = 0; i <= farthest && i < a.Length; i++)
    {
        farthest = Math.Max(farthest, i + a[i]);
    }
    return farthest >= a.Length - 1;
}
```

---

### 5B) 🔍 Binary Search (Index Space)

**What:** Search a sorted array by halving.

**Why:** Log-time search.

**How:** Shrink [L, R] by comparing mid.

**C#**
```csharp
int BinarySearch(int[] a, int target)
{
    int L = 0, R = a.Length - 1;
    while (L <= R)
    {
        int mid = L + (R - L) / 2;
        if (a[mid] == target) return mid;
        if (a[mid] < target) L = mid + 1;
        else R = mid - 1;
    }
    return -1;
}
```

---

### 5C) 🧪 Binary Search on Answer Space

**What:** Search a value range with a monotonic check.

**Why:** Implicit arrays (capacity, speed, time).

**How:** If check(mid) true, move right to mid, else move left.

**Python**
```python
def min_eating_speed(piles, h):
    def can_finish(k):
        hours = 0
        for p in piles:
            hours += (p + k - 1) // k
        return hours <= h

    left, right = 1, max(piles)
    while left < right:
        mid = left + (right - left) // 2
        if can_finish(mid):
            right = mid
        else:
            left = mid + 1
    return left
```

---

## 🟤 Level 6: Prefix/Suffix Traversal

### 6A) ➕ Prefix Accumulation

**What:** Carry a running aggregate forward.

**Why:** Enables O(1) range sum queries.

**How:** pref[i] = pref[i - 1] + a[i].

**C#**
```csharp
int[] PrefixSums(int[] a)
{
    int[] pref = new int[a.Length];
    int run = 0;
    for (int i = 0; i < a.Length; i++)
    {
        run += a[i];
        pref[i] = run;
    }
    return pref;
}
```

---

### 6B) ➖ Suffix Accumulation

**What:** Carry aggregate from right to left.

**Why:** Useful for suffix max/min or right-side totals.

**C#**
```csharp
int[] SuffixSums(int[] a)
{
    int[] suf = new int[a.Length];
    int run = 0;
    for (int i = a.Length - 1; i >= 0; i--)
    {
        run += a[i];
        suf[i] = run;
    }
    return suf;
}
```

---

### 6C) 🔄 Two-Pass Carry

**What:** Combine left and right aggregates.

**Why:** Problems like product of array except self.

**Python**
```python
def product_except_self(nums):
    n = len(nums)
    out = [1] * n

    left = 1
    for i in range(n):
        out[i] *= left
        left *= nums[i]

    right = 1
    for i in range(n - 1, -1, -1):
        out[i] *= right
        right *= nums[i]

    return out
```

---

## 🟧 Level 7: Monotonic Stack Traversal

### 7A) 📈 Next Greater Element

**What:** Stack of unresolved indices.

**Why:** Resolve future-dependent relationships in O(n).

**How:** Pop while current is greater than stack top.

**C#**
```csharp
int[] NextGreater(int[] a)
{
    int n = a.Length;
    int[] ans = new int[n];
    Array.Fill(ans, -1);

    var st = new Stack<int>();
    for (int i = 0; i < n; i++)
    {
        while (st.Count > 0 && a[i] > a[st.Peek()])
            ans[st.Pop()] = a[i];

        st.Push(i);
    }
    return ans;
}
```

---

## 🟫 Level 8: In-Place Partition Traversal

### 8A) 🇳🇱 Three-Way Partition

**What:** Maintain small, mid, and large regions.

**Why:** One-pass bucketing for 0/1/2 or small key sets.

**How:** Swap with boundaries and adjust pointers.

**C#**
```csharp
void Partition012(int[] a)
{
    int low = 0, mid = 0, high = a.Length - 1;
    while (mid <= high)
    {
        if (a[mid] == 0) { (a[low], a[mid]) = (a[mid], a[low]); low++; mid++; }
        else if (a[mid] == 1) { mid++; }
        else { (a[mid], a[high]) = (a[high], a[mid]); high--; }
    }
}
```

---

## ⚙️ Level 9: Cache/Throughput Traversal

### 9A) 📦 Chunked Scans

**What:** Process data in contiguous blocks.

**Why:** Improves locality and throughput on large arrays.

**How:** Iterate in fixed chunk sizes, then handle remainder.

**C#**
```csharp
void ChunkedSum(int[] a, int chunk)
{
    int i = 0;
    long total = 0;
    for (; i + chunk <= a.Length; i += chunk)
    {
        for (int j = 0; j < chunk; j++) total += a[i + j];
    }
    for (; i < a.Length; i++) total += a[i];
}
```

---

## ✅ Pattern Checklist

| Pattern | Invariant | Common Use |
| --- | --- | --- |
| Forward scan | i in [0..n) | scan/copy |
| Backward scan | i in [n-1..0] | overwrite-safe write |
| Lockstep | same index for A/B | aligned arrays |
| Reader/Writer | writer <= reader | in-place filter |
| Converging | left < right | sorted pair |
| Fixed window | size constant | rolling sum |
| Variable window | invariant valid | best subarray |
| Prefix sum | pref[i] = sum(0..i) | range sums |
| Binary search | [L,R] shrinks | sorted search |

---

## 🧪 Practice Map

**Level 1:** linear search, max/min, copy array

**Level 2:** rotate array, search matrix, circular queue

**Level 3:** remove duplicates, two sum sorted, merge arrays

**Level 4:** longest substring, min size subarray sum, max k-window sum

**Level 5:** jump game, classic binary search, answer-space search

**Level 6:** product except self, prefix range query, suffix max

**Level 7:** next greater element, daily temperatures

**Level 8:** sort colors, 3-way partition

**Level 9:** chunked scans, cache-friendly traversal
