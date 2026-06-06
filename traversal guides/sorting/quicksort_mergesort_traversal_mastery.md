# 🔀 Quicksort & Mergesort Mastery (Traversal-First)

> 🎯 Goal: Learn **Quicksort** and **Mergesort** by focusing on the *traversal patterns* they are built from (your Levels model), then cement intuition via walkthrough traces.

---

## ✅ Summary list
- ⚡ **Quicksort** = divide-and-conquer + **in-place partition traversal**.
- 🧬 **Mergesort** = divide-and-conquer + **merge traversal** using two cursors.
- 🧭 You’ll master both by mastering 3 primitives:
  - 🧩 **Partition boundaries** (Levels 3 & 8)
  - 🔀 **Two-list merge traversal** (Level 3)
  - 🪟 **Range recursion over subarrays** (Levels 4 & 5)

---

# 0) Traversal patterns map 🗺️

## Which traversal level shows up where?

### Quicksort
- 🪟 **Level 4 (Range traversal):** each recursive call works on a subarray `[lo..hi]`.
- 🧭 **Level 5 (Recursion / call stack mental model):** divide-and-conquer recursion over ranges.
- 👀 **Level 3 (Multi-cursor):** partition schemes use multiple indices (e.g., `i`, `j`, `L`, `R`).
- 🧹 **Level 8 (In-place partition boundaries):** the heart of quicksort.

### Mergesort
- 🪟 **Level 4 (Range traversal):** divide into `[lo..mid]` and `[mid+1..hi]`.
- 🧭 **Level 5 (Recursion):** recursively sort ranges.
- 🔀 **Level 3 (Merge traversal):** merge two sorted halves with two cursors.
- ⚙️ **Level 9 (Performance mindset):** minimize allocations by reusing temp buffers.

---

# ⚡ 1) Quicksort

## Why
Quicksort is fast in practice because it partitions in place and reduces the problem recursively.

## What
Pick a **pivot**, partition the array into `< pivot` and `>= pivot` (or 3-way), then quicksort the left and right partitions.

## How (step/flow)
```
QuickSort(lo, hi):
  if lo >= hi: return
  p = Partition(lo, hi)
  QuickSort(lo, p-1)
  QuickSort(p+1, hi)
```

### Partition is the traversal ⭐
Partition is a **single pass** over the range `[lo..hi]` where boundaries move and invariants must remain true.

## Where and When (use cases)
- In-memory sorting when average-case speed and in-place operation matter.

## Common pitfalls
- ❌ Partition invariants not maintained → array ends up “almost partitioned” but incorrect.
- ❌ Pivot choice can cause worst-case recursion depth on already sorted/reverse-sorted arrays.
- ❌ Confusing partition scheme (Lomuto vs Hoare) and using wrong recursion bounds.

## Tips and tricks
- ✅ Master partition first. Quicksort is “partition + recursion driver.”
- ✅ Choose a safer pivot (random pivot, median-of-three) to reduce worst-case frequency.
- ✅ Prefer **3-way partition** when duplicates are common.

---

## 1.1 Partition scheme A: Lomuto (simple boundary walk) 🧹

### Why
Easier to implement and reason about at first.

### What
- Pivot = last element
- `store` boundary: next position for `< pivot`
- One scan index `j` moves through the range

### Invariant (say it before coding)
At any time:
- `[lo .. store-1]` are `< pivot`
- `[store .. j-1]` are `>= pivot`
- `[j .. hi-1]` are unknown
- `pivot` at `hi`

### C# (Lomuto partition)
```csharp
static int PartitionLomuto(int[] a, int lo, int hi)
{
    if (a == null || a.Length == 0) return lo; // lenient
    if (lo < 0) lo = 0;
    if (hi >= a.Length) hi = a.Length - 1;
    if (lo >= hi) return lo;

    int pivot = a[hi];
    int store = lo;

    for (int j = lo; j < hi; j++)
    {
        if (a[j] < pivot)
        {
            (a[store], a[j]) = (a[j], a[store]);
            store++;
        }
    }

    (a[store], a[hi]) = (a[hi], a[store]);
    return store;
}
```

### Python (Lomuto partition)
```python
def partition_lomuto(a, lo, hi):
    if not a:
        return lo
    lo = max(lo, 0)
    hi = min(hi, len(a) - 1)
    if lo >= hi:
        return lo

    pivot = a[hi]
    store = lo
    for j in range(lo, hi):
        if a[j] < pivot:
            a[store], a[j] = a[j], a[store]
            store += 1
    a[store], a[hi] = a[hi], a[store]
    return store
```

---

## 1.2 Lomuto partition walkthrough (index-by-index) 🧪

### Example
Array:
```
a = [9, 3, 7, 1, 8, 2, 5]
lo=0, hi=6, pivot=5
```

We track: `j`, `store`, and the array.

Initial:
- pivot = 5
- store = 0

| Step | j | a[j] | Condition | Action | store after | Array |
|---:|---:|---:|:---|:---|---:|:---|
| 0 | - | - | init | - | 0 | [9,3,7,1,8,2,5] |
| 1 | 0 | 9 | 9 < 5? no | none | 0 | [9,3,7,1,8,2,5] |
| 2 | 1 | 3 | 3 < 5? yes | swap a[store]↔a[j] (0↔1) | 1 | [3,9,7,1,8,2,5] |
| 3 | 2 | 7 | 7 < 5? no | none | 1 | [3,9,7,1,8,2,5] |
| 4 | 3 | 1 | 1 < 5? yes | swap (1↔3) | 2 | [3,1,7,9,8,2,5] |
| 5 | 4 | 8 | 8 < 5? no | none | 2 | [3,1,7,9,8,2,5] |
| 6 | 5 | 2 | 2 < 5? yes | swap (2↔5) | 3 | [3,1,2,9,8,7,5] |
| end | - | - | finish | swap pivot into store (3↔6) | 3 | [3,1,2,5,8,7,9] |

Result:
- pivot index `p = 3`
- left `[0..2]` is `< 5`
- right `[4..6]` is `>= 5`

---

## 1.3 Quicksort driver (C# + Python)

### C#
```csharp
static void QuickSort(int[] a, int lo, int hi)
{
    if (a == null || a.Length <= 1) return;

    lo = Math.Max(lo, 0);
    hi = Math.Min(hi, a.Length - 1);
    if (lo >= hi) return;

    int p = PartitionLomuto(a, lo, hi);
    QuickSort(a, lo, p - 1);
    QuickSort(a, p + 1, hi);
}
```

### Python
```python
def quicksort(a, lo, hi):
    if not a or len(a) <= 1:
        return
    lo = max(lo, 0)
    hi = min(hi, len(a) - 1)
    if lo >= hi:
        return

    p = partition_lomuto(a, lo, hi)
    quicksort(a, lo, p - 1)
    quicksort(a, p + 1, hi)
```

---

## 1.4 Advanced quicksort: 3-way partition (duplicates-friendly) 🇳🇱

### Why
If many elements equal pivot, 2-way partition keeps recursing on equals and slows down.

### What
Maintain regions:
- `< pivot`
- `== pivot`
- `> pivot`

### How (step/flow)
```
| < pivot | == pivot | unknown........ | > pivot |
 0       lt        i            gt       n-1

if a[i] < pivot: swap(i, lt), lt++, i++
if a[i] == pivot: i++
if a[i] > pivot: swap(i, gt), gt-- (i stays)
```

### C# snippet
```csharp
static (int ltEnd, int gtStart) Partition3Way(int[] a, int lo, int hi)
{
    if (a == null || a.Length == 0) return (lo, hi);
    lo = Math.Max(lo, 0);
    hi = Math.Min(hi, a.Length - 1);
    if (lo >= hi) return (lo, hi);

    int pivot = a[lo];
    int lt = lo;
    int i = lo;
    int gt = hi;

    while (i <= gt)
    {
        if (a[i] < pivot)
        {
            (a[lt], a[i]) = (a[i], a[lt]);
            lt++; i++;
        }
        else if (a[i] > pivot)
        {
            (a[i], a[gt]) = (a[gt], a[i]);
            gt--; // i stays
        }
        else
        {
            i++;
        }
    }

    // equal region is [lt..gt]
    return (lt - 1, gt + 1);
}
```

---

# 🧬 2) Mergesort

## Why
Mergesort guarantees O(n log n) time by dividing into halves and merging sorted results.

## What
Recursively split the array into halves until size 1, then **merge** two sorted halves into one sorted range.

## How (step/flow)
```
MergeSort(lo, hi):
  if lo >= hi: return
  mid = (lo+hi)/2
  MergeSort(lo, mid)
  MergeSort(mid+1, hi)
  Merge(lo, mid, hi)
```

### Merge is the traversal ⭐
Merge is a classic **Level 3 merge traversal**:
- pointer `i` walks left half
- pointer `j` walks right half
- pointer `k` writes into temp/output

## Where and When (use cases)
- Stable sorting (conceptually), linked-list sorting, external sorting, guaranteed performance.

## Common pitfalls
- ❌ Off-by-one boundaries: mixing inclusive vs exclusive ranges.
- ❌ Copying back incorrectly after merge.
- ❌ Allocating a new temp array at each recursion level (slow); reuse a shared temp.

## Tips and tricks
- Use a single `temp[]` buffer reused across merges.
- Be explicit about your range convention (here we use inclusive `[lo..hi]`).

---

## 2.1 Merge walkthrough (index-by-index) 🔀

### Example merge
Merge left and right halves:
```
Left  = [1, 4, 9]
Right = [2, 3, 10]
```

Pointers:
- i for left, j for right, k for output

| Step | i | Left[i] | j | Right[j] | Pick | Output |
|---:|---:|---:|---:|---:|:---|:---|
| 0 | 0 | 1 | 0 | 2 | 1 | [1] |
| 1 | 1 | 4 | 0 | 2 | 2 | [1,2] |
| 2 | 1 | 4 | 1 | 3 | 3 | [1,2,3] |
| 3 | 1 | 4 | 2 | 10 | 4 | [1,2,3,4] |
| 4 | 2 | 9 | 2 | 10 | 9 | [1,2,3,4,9] |
| end | - | - | - | - | append remaining (10) | [1,2,3,4,9,10] |

---

# Merge-Family Extensions (Merged)

These are the important merge-like traversal applications folded into the sorting mastery file.

## Union and difference of sorted arrays
- Same primitive as merge: compare fronts, emit, and advance at least one cursor.
- Add duplicate-handling rules explicitly.

## Merge intervals
- Sort by start time, then scan with an anchor interval.
- Invariant: the current merged interval is the correct collapse of everything processed so far.

## Merge join (database-style)
- Traverse two sorted relations by key.
- On equal keys, emit matching pairs; otherwise advance the smaller key.

## K-way merge
- Generalize two-way merge with a priority queue of current heads.
- Invariant: the heap frontier always exposes the next globally smallest candidate.

## Timed practice ladder (merged)

### Must
- Merge two sorted arrays/lists
- Merge sort implementation and trace
- Quicksort partition trace
- Sort colors / Dutch National Flag

### Should
- Merge intervals
- Union/intersection/difference of sorted arrays
- K-way merge

### Optional advanced
- Merge join interpretation
- Interval-style merge variants with database or streaming framing

This file is now the merged source for quicksort, mergesort, merge-family techniques, and sorting traversal practice.

This is pure “two sorted arrays” traversal (Level 3 merge traversal).

---

## 2.2 Full mergesort walkthrough (small array) 🧪

### Example
```
a = [4, 1, 3, 2]
```

Split phase (ranges):
```
[4,1,3,2]
 -> [4,1] and [3,2]
 -> [4] [1] [3] [2]
```

Merge phase:
- merge [4] and [1] => [1,4]
- merge [3] and [2] => [2,3]
- merge [1,4] and [2,3] => [1,2,3,4]

This is Level 5 recursion over Level 4 ranges, plus Level 3 merge traversal.

---

## 2.3 C# mergesort (reuses temp) 💻

```csharp
static void MergeSort(int[] a)
{
    if (a == null || a.Length <= 1) return;
    int[] temp = new int[a.Length];
    MergeSortRange(a, temp, 0, a.Length - 1);
}

static void MergeSortRange(int[] a, int[] temp, int lo, int hi)
{
    if (lo >= hi) return;

    int mid = lo + (hi - lo) / 2;
    MergeSortRange(a, temp, lo, mid);
    MergeSortRange(a, temp, mid + 1, hi);
    Merge(a, temp, lo, mid, hi);
}

static void Merge(int[] a, int[] temp, int lo, int mid, int hi)
{
    int i = lo;
    int j = mid + 1;
    int k = lo;

    while (i <= mid && j <= hi)
    {
        if (a[i] <= a[j]) temp[k++] = a[i++];
        else temp[k++] = a[j++];
    }

    while (i <= mid) temp[k++] = a[i++];
    while (j <= hi) temp[k++] = a[j++];

    for (int t = lo; t <= hi; t++) a[t] = temp[t];
}
```

---

## 2.4 Python mergesort (teaching version)

```python
def merge_sort(a):
    if a is None or len(a) <= 1:
        return a if a is not None else []

    temp = [0] * len(a)

    def sort(lo, hi):
        if lo >= hi:
            return
        mid = (lo + hi) // 2
        sort(lo, mid)
        sort(mid + 1, hi)
        merge(lo, mid, hi)

    def merge(lo, mid, hi):
        i, j, k = lo, mid + 1, lo
        while i <= mid and j <= hi:
            if a[i] <= a[j]:
                temp[k] = a[i]
                i += 1
            else:
                temp[k] = a[j]
                j += 1
            k += 1
        while i <= mid:
            temp[k] = a[i]
            i += 1
            k += 1
        while j <= hi:
            temp[k] = a[j]
            j += 1
            k += 1
        for t in range(lo, hi + 1):
            a[t] = temp[t]

    sort(0, len(a) - 1)
    return a
```

---

# 🧭 Route to master both (traversal-first) 

## Step 1: Master the primitives (fastest ROI)
- 🧹 Partition boundaries
  - Start with **2-region** partition (< pivot | >= pivot)
  - Then **3-region** partition (< | == | >)
  - Practice the invariant drawing: regions + unknown
- 🔀 Merge traversal
  - Two pointers + output pointer; prove “always progress”
  - Learn stability implications (`<=` vs `<`)
- 🪟 Range recursion
  - Always define range convention: inclusive `[lo..hi]` or half-open `[lo..hi)`

## Step 2: Master quicksort
1) Implement + trace partition only (no recursion).
2) Add recursion and trace subarray ranges.
3) Add pivot strategy (random/median-of-three).
4) Add 3-way partition for duplicates.

## Step 3: Master mergesort
1) Implement merge for two sorted arrays.
2) Implement range mergesort with a reusable temp buffer.
3) Trace every merge with `(i,j,k)` pointers.

## Step 4: Compare tradeoffs
- Quicksort: in-place, usually fast, but can degrade on bad pivots.
- Mergesort: predictable performance, uses extra memory for merging.

---

## ✅ Debugging checklist (both)
- Always log indices and boundaries:
  - Quicksort: `(lo, hi, pivot, store/lt/i/gt)`
  - Mergesort: `(lo, mid, hi, i, j, k)`
- Typical symptoms:
  - Infinite loop → a pointer not moving.
  - Slightly unsorted output → merge/partition invariant broken.
  - Stack overflow → recursion too deep (quicksort pivot issue) or very large input.
