# 🧭 Traversal-Level Mastery for Quicksort & Mergesort (with Primitives)

> **Goal:** Achieve mastery of Quicksort and Mergesort by mastering the *traversal primitives* they’re built from.

---

## ✅ Summary list
- ⚡ **Quicksort** = **range recursion** + **in-place partition boundary traversal**.
- 🧬 **Mergesort** = **range recursion** + **two-cursor merge traversal**.
- 🧱 3 primitives to master:
  - 🧩 Partition boundaries (**Levels 3 & 8**)
  - 🔀 Two-list merge traversal (**Level 3**)
  - 🪟 Range recursion over subarrays (**Levels 4 & 5**)

---

# 0) Roadmap: levels → primitives → algorithms 🗺️

## Mastery ladder
1) **Level 1:** Safe linear scanning (cursor discipline)
2) **Level 2:** Index arithmetic (reverse, mid, offsets)
3) **Level 3:** Multi-cursor traversal (two pointers, write pointer, merge pointers)
4) **Level 4:** Range traversal (operate on `[lo..hi]` or `[lo..hi)` consistently)
5) **Level 5:** Recursion as traversal (call stack, base case, shrink guarantees)
6) **Level 8:** In-place boundary partitions (2-way + 3-way region invariants)
7) **Level 9:** Performance polish (buffer reuse, pivot strategy, thresholds)

---

# Level 1 — Safe linear scan 🚶

## Why
All higher patterns fail if you can’t guarantee safe reads/writes and progress.

## What
The universal rule:
- Only read/write `a[i]` when `0 <= i < n`.
- Every loop must advance at least one control variable.

## How (step/flow)
```
for i from 0 to n-1:
  read a[i]
  maybe write somewhere
```

## Where and When
- Foundation for partition scans and merge scans.

## Edge cases ✅
- Empty array → loops should naturally do nothing.
- Single element → base case for recursion.

## Pitfalls / gotchas
- Off-by-one (`i < n` vs `i <= n-1`).
- Infinite loops in `while` due to missing increments.

## C# snippet
```csharp
static int CountLessThan(int[] a, int pivot)
{
    if (a == null) return 0; // lenient

    int count = 0;
    for (int i = 0; i < a.Length; i++)
        if (a[i] < pivot) count++;

    return count;
}
```

## Python snippet
```python
def count_less_than(a, pivot):
    if not a:
        return 0
    c = 0
    for i in range(len(a)):
        if a[i] < pivot:
            c += 1
    return c
```

---

# Level 2 — Index arithmetic 🧮

## Why
Sorting algorithms rely on midpoints, subranges, and reverse/backward scans.

## What
Key arithmetic used constantly:
- Midpoint: `mid = lo + (hi - lo) // 2`
- Range sizes: `len = hi - lo + 1` (inclusive) or `len = hi - lo` (half-open)

## How (step/flow)
### Choose one range convention (must be consistent)
- **Inclusive**: `[lo..hi]`
  - left: `[lo..mid]`
  - right: `[mid+1..hi]`
- **Half-open**: `[lo..hi)`
  - left: `[lo..mid)`
  - right: `[mid..hi)`

## Where and When
- Mergesort splitting.
- Quicksort recursion bounds.

## Edge cases ✅
- Large indices: use `lo + (hi-lo)/2` to avoid overflow.

## Pitfalls / gotchas
- Mixing inclusive and half-open in the same function.

## C# snippet (mid)
```csharp
static int Mid(int lo, int hi)
{
    // inclusive bounds
    return lo + (hi - lo) / 2;
}
```

## Python snippet
```python
def mid(lo, hi):
    return lo + (hi - lo) // 2
```

---

# Level 3 — Multi-cursor traversal 👀👀

> This level contains **both** core primitives you need: **partition scanning** and **merge scanning**.

## Why
Quicksort and mergesort are powered by “multiple moving indices” with strict invariants.

## What
You must internalize these cursor roles:
- ✅ **Read cursor**: scans unknown elements
- ✅ **Write cursor / boundary**: marks next slot for a category
- ✅ **Left/Right cursors**: walk two sorted halves during merge

---

## Primitive A: 🔀 Two-list merge traversal (Level 3)

### Why
Mergesort’s combine step is *exactly* “merge two sorted sequences” using two pointers.

### What
Maintain:
- `i` = left half cursor
- `j` = right half cursor
- `k` = output cursor

### How (step/flow)
```
while i in left and j in right:
  take smaller (or <= for stability)
append remaining tail
```

### Edge cases ✅
- One side empty → result is the other side.
- Duplicates → decide stability: use `<=` to prefer left element first.

### Pitfalls / gotchas
- Forgetting to copy the remaining tail.
- Misplacing boundaries (mid off-by-one).

### C# snippet (merge two sorted ranges, inclusive)
```csharp
static void Merge(int[] a, int[] temp, int lo, int mid, int hi)
{
    int i = lo, j = mid + 1, k = lo;

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

### Python snippet
```python
def merge(a, temp, lo, mid, hi):
    i, j, k = lo, mid + 1, lo
    while i <= mid and j <= hi:
        if a[i] <= a[j]:
            temp[k] = a[i]; i += 1
        else:
            temp[k] = a[j]; j += 1
        k += 1
    while i <= mid:
        temp[k] = a[i]; i += 1; k += 1
    while j <= hi:
        temp[k] = a[j]; j += 1; k += 1
    for t in range(lo, hi + 1):
        a[t] = temp[t]
```

---

## Primitive B: 🧩 Partition scanning (Level 3 → Level 8)

### Why
Partition is a single traversal that rearranges elements into regions.

### What
Two popular mental models:
- **Write-pointer partition** (Lomuto style)
- **Two-ended partition** (Hoare style)

### How (step/flow) — write-pointer model
```
store = lo
for j in lo..hi-1:
  if a[j] < pivot:
    swap a[store], a[j]
    store++
swap pivot into a[store]
```

### Edge cases ✅
- All < pivot → store ends at hi.
- None < pivot → store stays lo.
- Many == pivot → 2-way partition can degrade recursion; consider 3-way partition.

### Pitfalls / gotchas
- Using the wrong recursion bounds for your partition scheme.
- Assuming stability (partition swapping is not stable).

### C# snippet (Lomuto partition, inclusive)
```csharp
static int PartitionLomuto(int[] a, int lo, int hi)
{
    if (a == null || a.Length == 0) return lo; // lenient
    lo = Math.Max(lo, 0);
    hi = Math.Min(hi, a.Length - 1);
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

### Python snippet
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

# Level 4 — Range traversal over subarrays 🪟

## Why
Both algorithms work on **subarray ranges**; most bugs come from wrong boundaries.

## What
A “range function” has:
- input range: `[lo..hi]` (inclusive) or `[lo..hi)` (half-open)
- base case: range length 0 or 1
- shrink rule: recursive calls must strictly reduce range size

## How (step/flow)
### Range template (inclusive)
```
Solve(lo, hi):
  if lo >= hi: return
  mid = lo + (hi-lo)/2
  Solve(lo, mid)
  Solve(mid+1, hi)
  Combine(lo, mid, hi)
```

## Where and When
- Mergesort split ranges.
- Quicksort recursively sorts partitions.

## Edge cases ✅
- `lo == hi` → single element.
- invalid inputs: `lo < 0`, `hi >= n`, `lo > hi`.

## Pitfalls / gotchas
- Off-by-one in mid split (`mid` belongs to left in inclusive convention).

## C# snippet (range guard)
```csharp
static void ClampRange(int n, ref int lo, ref int hi)
{
    if (n <= 0) { lo = 0; hi = -1; return; }
    lo = Math.Max(lo, 0);
    hi = Math.Min(hi, n - 1);
    if (lo > hi) { lo = 0; hi = -1; }
}
```

---

# Level 5 — Recursion as traversal (call stack mastery) 🧭

## Why
Divide-and-conquer sorting is fundamentally a traversal of a recursion tree of ranges.

## What
You must be able to answer these three questions at any recursive call:
1) What range am I responsible for?
2) What is the base case?
3) What strictly decreases so we terminate?

## How (step/flow)
### Visual: recursion tree for mergesort on 8 items
```
[0..7]
  [0..3]      [4..7]
 [0..1][2..3] [4..5][6..7]
 ...
```

## Where and When
- Quicksort: recursion follows partitioned subranges.
- Mergesort: recursion follows halving, then merges on the way back.

## Edge cases ✅
- Deep recursion risk:
  - Quicksort worst-case depth can be n (bad pivots).
  - Mergesort depth is ~log2(n).

## Pitfalls / gotchas
- Forgetting to shrink ranges in quicksort if partition returns boundaries incorrectly.

## Tips and tricks
- For quicksort, recurse on the **smaller** side first to reduce max stack depth.

## C# snippet (safe quicksort recursion order)
```csharp
static void QuickSort(int[] a, int lo, int hi)
{
    if (a == null || a.Length <= 1) return;

    ClampRange(a.Length, ref lo, ref hi);
    if (hi < lo) return;

    while (lo < hi)
    {
        int p = PartitionLomuto(a, lo, hi);

        // Recurse smaller side first; loop on the larger side
        if (p - lo < hi - p)
        {
            QuickSort(a, lo, p - 1);
            lo = p + 1;
        }
        else
        {
            QuickSort(a, p + 1, hi);
            hi = p - 1;
        }
    }
}
```

## Python snippet
```python
def quicksort(a, lo, hi):
    if not a or len(a) <= 1:
        return
    lo = max(lo, 0)
    hi = min(hi, len(a) - 1)
    if lo >= hi:
        return

    # tail-recursion-like: recurse smaller side first
    while lo < hi:
        p = partition_lomuto(a, lo, hi)
        if p - lo < hi - p:
            quicksort(a, lo, p - 1)
            lo = p + 1
        else:
            quicksort(a, p + 1, hi)
            hi = p - 1
```

---

# Level 8 — In-place partition boundaries (mastery level for Quicksort) 🧹

## Why
This is the “real skill” behind quicksort, selection, and many in-place array problems.

## What
### 2-way partition (already covered)
- Regions: `< pivot` | `>= pivot`

### 3-way partition (Dutch National Flag)
- Regions: `< pivot` | `== pivot` | `> pivot`

## How (step/flow)
### 3-way invariant diagram
```
| < pivot | == pivot | unknown........ | > pivot |
  lo     lt         i             gt      hi

Rules:
- if a[i] < pivot: swap(i, lt), lt++, i++
- if a[i] == pivot: i++
- if a[i] > pivot: swap(i, gt), gt-- (i stays)
```

## Edge cases ✅
- All elements equal pivot → linear scan, no recursion cost.
- Many duplicates → 3-way partition avoids repeated work.

## Pitfalls / gotchas
- Incrementing `i` after swapping with `gt` (you will skip the swapped-in unknown).

## Things to remember
- “Swap with `gt` means re-check `i`.”

## C# snippet (3-way partition)
```csharp
static (int lt, int gt) Partition3Way(int[] a, int lo, int hi)
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

    return (lt, gt); // equals region is [lt..gt]
}
```

## Python snippet
```python
def partition_3way(a, lo, hi):
    if not a:
        return (lo, hi)
    lo = max(lo, 0)
    hi = min(hi, len(a) - 1)
    if lo >= hi:
        return (lo, hi)

    pivot = a[lo]
    lt = lo
    i = lo
    gt = hi

    while i <= gt:
        if a[i] < pivot:
            a[lt], a[i] = a[i], a[lt]
            lt += 1
            i += 1
        elif a[i] > pivot:
            a[i], a[gt] = a[gt], a[i]
            gt -= 1
        else:
            i += 1

    return (lt, gt)
```

---

# Level 9 — Performance & robustness polish ⚙️

## Why
Correct quicksort/mergesort can still be slow or crash due to recursion depth or allocation patterns.

## What
Key mastery topics:
- 🎲 **Pivot strategy** (random / median-of-three) to reduce worst-case quicksort likelihood.
- 🧪 **Small-array cutoff**: switch to insertion sort for tiny ranges.
- ♻️ **Temp reuse**: mergesort should reuse one temp buffer.
- 🧠 **Stability**: mergesort can be stable depending on merge tie-breaking (`<=`).

## How (step/flow)
- Quicksort: pick pivot carefully + 3-way partition when duplicates heavy.
- Mergesort: allocate `temp` once, pass down.

## Edge cases ✅
- Very large arrays:
  - recursion depth risks (quicksort)
  - memory pressure (mergesort temp)

## Pitfalls / gotchas
- Allocating temp per merge call.
- Not controlling recursion depth in quicksort.

---

# 🧠 Putting it all together: mastery walkthroughs

## A) Quicksort mastery flow
1) Draw partition regions and invariants.
2) Trace partition on 5–8 elements until it feels automatic.
3) Add recursion over ranges, log `(lo, hi)` at entry.
4) Add 3-way partition and compare recursion depth on duplicates.

## B) Mergesort mastery flow
1) Master merge of two sorted halves with pointer trace `(i, j, k)`.
2) Add recursion splitting ranges; ensure base case triggers.
3) Reuse temp buffer; verify copy-back boundaries.

---

# ✅ Universal checklist (if it’s wrong)
- Are your ranges inclusive or half-open? (must be consistent)
- Does every loop advance at least one cursor?
- Are your region invariants true after every swap?
- Are you handling empty / single-element ranges?

---

# Next step
Tell me which one you want to trace next with full logs:
- ⚡ Quicksort with 3-way partition on many duplicates
- 🧬 Mergesort merge with exact `(i,j,k)` movement on a tricky array
