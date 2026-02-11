# 🧮 Arrays Mastery Pack (Levels 2–4) — Lenient Contracts

> 🎯 Focus: **Index arithmetic → multi-cursor traversal → range/window traversal** with clean movement rules, strong invariants, and practical templates.

---

## ✅ Summary list
- 🧮 **Level 2:** Reverse, stride, wrap-around, 2D↔1D mapping.
- 👀 **Level 3:** Two pointers, merge traversal, partition-style scanning.
- 🪟 **Level 4:** Fixed + variable sliding windows, segment (block) traversal.
- 🧷 After each level: invariants checklist + what to log when debugging.
- 🤝 Contracts: **LENIENT** (return defaults instead of throwing).

---

## 🤝 Lenient contract rules (we’ll follow)
When inputs are invalid, return a safe default so calling code can continue.

- `int[] a == null` → return `0`, `-1`, or `Array.Empty<int>()` depending on what makes sense.
- `k <= 0` or `k > a.Length` → return a default (often `0` or `-1`) instead of throwing.
- If there is “no answer” (e.g., not found) → return `-1`.

> ✅ Tip: Always document the default explicitly in the function header comment.

---

# Level 2 — Index arithmetic 🧮

## Patterns in this level
- ⬅️ Reverse traversal
- 🦘 Stride (step) traversal
- 🔁 Cyclic traversal (modulo wrap)
- 🧭 2D-to-1D mapping

---

## 2.1 ⬅️ Reverse traversal

### Why
Reverse scans simplify suffix problems and prevent overwriting when modifying in-place.

### What
Traverse `i = n-1 .. 0`.

### How (step/flow)
```
arr:  [10][20][30][40]
idx:   0   1   2   3
move:            i←  i←  i←  i← stop
```

### Code (C#)
```csharp
// LENIENT:
// - null => -1
int LastIndexOf(int[] a, int target)
{
    if (a == null) return -1;

    for (int i = a.Length - 1; i >= 0; i--)
        if (a[i] == target) return i;

    return -1;
}
```

### Code (Python)
```python
def last_index_of(a, target):
    if a is None:
        return -1
    for i in range(len(a) - 1, -1, -1):
        if a[i] == target:
            return i
    return -1
```

### Where and When (use cases)
- Last occurrence, rightmost boundary checks, in-place deletions from the end.

### Common pitfalls
- Confusing `i > 0` vs `i >= 0` (skips index 0).

### Tips and tricks
- Empty array? Start at `-1`, the loop naturally doesn’t run.

---

## 2.2 🦘 Stride traversal (`i += step`)

### Why
Useful for sampling, even/odd lanes, and periodic checks.

### What
Move by a constant stride: `i = 0, step, 2*step, ...`.

### How (step/flow)
```
idx:   0 1 2 3 4 5 6 7
visit: ^   ^   ^   ^
step=2 => 0,2,4,6
```

### Code (C#)
```csharp
// LENIENT:
// - null => 0
// - step <= 0 => 0
int SumEveryKth(int[] a, int step)
{
    if (a == null) return 0;
    if (step <= 0) return 0;

    int sum = 0;
    for (int i = 0; i < a.Length; i += step)
        sum += a[i];

    return sum;
}
```

### Where and When (use cases)
- Sampling logs, downscaling, parity scans.

### Common pitfalls
- `step == 0` → infinite loop.

### Tips and tricks
- If you need “every k-th starting at offset p”, use `for (i=p; i<n; i+=k)`.

---

## 2.3 🔁 Cyclic traversal (wrap-around)

### Why
Circular buffers and rotating schedules are “arrays with wrap.”

### What
Use modulo: `next = (i + 1) % n`.

### How (step/flow)
```
idx:  0 1 2 3 4
wrap: 0→1→2→3→4→0→...
```

### Code (C#)
```csharp
// LENIENT:
// - n <= 0 => return 0 (no movement)
int NextIndex(int i, int n)
{
    if (n <= 0) return 0;
    int next = (i + 1) % n;
    if (next < 0) next += n; // defensive normalization
    return next;
}
```

### Where and When (use cases)
- Ring buffers, round-robin allocation.

### Common pitfalls
- `n == 0` modulo crash (so we guard it leniently).

### Tips and tricks
- Treat “next index” as a tiny unit you test with `n=1..5`.

---

## 2.4 🧭 2D-to-1D mapping

### Why
Many grid problems are faster/simpler with a flat array.

### What
With `cols`:
- `idx = r * cols + c`
- `r = idx / cols`
- `c = idx % cols`

### How (step/flow)
```
rows=3 cols=4
(0,0)=0  (0,1)=1  (0,2)=2  (0,3)=3
(1,0)=4  (1,1)=5  (1,2)=6  (1,3)=7
(2,0)=8  (2,1)=9 (2,2)=10 (2,3)=11
```

### Code (C#)
```csharp
// LENIENT:
// - cols <= 0 => -1
int ToIndex(int r, int c, int cols)
{
    if (cols <= 0) return -1;
    return r * cols + c;
}
```

### Where and When (use cases)
- Image buffers, game boards, DP grids.

### Common pitfalls
- Mixing `rows` and `cols`.

### Tips and tricks
- Validate mapping with a 2x3 mini grid before coding the full solution.

---

## ✅ Level 2 invariants checklist
- Every read uses: `0 <= i < a.Length`.
- Stride loops guarantee `step > 0` (or return default).
- Cyclic logic guarantees `n > 0` (or return default).

## 🪲 Level 2 debugging guide
Print/log:
- The **sequence of indices** visited (not just values).
- For mapping: `(r, c, idx)` for a few sample points.
Typical symptoms:
- Skipping elements → wrong increment.
- Crash → missing bounds guard.
- Infinite loop → step not changing / step=0.

---

# Level 3 — Multi-cursor traversal 👀👀

## Patterns in this level
- ↔️ Two pointers (left/right)
- 🔀 Merge traversal (two sorted arrays)
- 🧹 Partition boundary scanning (two/three regions)

---

## 3.1 ↔️ Two pointers (left/right)

### Why
You can solve “pair from ends” tasks in one pass.

### What
Maintain `L` and `R` and move based on comparisons.

### How (step/flow)
```
idx:  0 1 2 3 4 5
      L         R
move: L→ ... ←R
```

### Code (C#)
```csharp
// LENIENT:
// - null => true (treat as trivially symmetric)
bool IsMirror(int[] a)
{
    if (a == null) return true;

    int L = 0, R = a.Length - 1;
    while (L < R)
    {
        if (a[L] != a[R]) return false;
        L++; R--;
    }
    return true;
}
```

### Where and When (use cases)
- Symmetry checks, pair-sum with sorted arrays, shrinking from ends.

### Common pitfalls
- Wrong stop condition (`L <= R` vs `L < R`).

### Tips and tricks
- Decide what it means to be “unfinished”: usually “at least 2 elements remain” → `L < R`.

---

## 3.2 🔀 Merge traversal (two sorted arrays)

### Why
Linear-time combination beats nested loops.

### What
Walk A and B with `(i, j)`; always advance the pointer you consumed.

### How (step/flow)
```
A: [1,3,7]
B: [2,3,8]
 i^    
 j^    
Rule: advance i if A[i] smaller, else advance j.
```

### Code (C#)
```csharp
// LENIENT:
// - null inputs => return 0
int CountCommonSorted(int[] a, int[] b)
{
    if (a == null || b == null) return 0;

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

### Where and When (use cases)
- Intersection, merge, diff, dedup scans.

### Common pitfalls
- Forgetting to advance `i` or `j` in one branch → infinite loop.

### Tips and tricks
- Progress rule: “each loop must increment `i` or `j` (or both).”

---

## 3.3 🧹 Partition boundary scanning

### Why
You can reorganize elements in-place by moving boundaries, not by copying.

### What
Maintain region boundaries that grow/shrink.

### How (step/flow)
```
| good..... | unknown........ | bad..... |
 0        L             R     n-1

Scan unknown, swap into the correct region, move boundary.
```

### Code (C#) — compact non-stable partition
```csharp
// LENIENT:
// - null => no-op
// Moves all values < pivot to the left (order not preserved).
void PartitionLessThan(int[] a, int pivot)
{
    if (a == null) return;

    int L = 0;
    for (int i = 0; i < a.Length; i++)
    {
        if (a[i] < pivot)
        {
            (a[L], a[i]) = (a[i], a[L]);
            L++;
        }
    }
}
```

### Where and When (use cases)
- Preprocessing, bucketing, quicksort partition step.

### Common pitfalls
- Assuming this is stable (it isn’t).

### Tips and tricks
- Name `L` by meaning: “next slot for < pivot.”

---

## ✅ Level 3 invariants checklist
- Two pointers: `L` only moves right, `R` only moves left (or clearly justified).
- Merge: each iteration advances at least one pointer.
- Partition: the “done region(s)” remain correct after every swap.

## 🪲 Level 3 debugging guide
Print/log:
- Two pointers: `(L, R)` and values at those indices.
- Merge: `(i, j)` plus which pointer advanced.
- Partition: boundary `L` and a small array snapshot.
Typical symptoms:
- Infinite loop → pointer not advancing.
- Wrong results → region invariant broken after swap.

---

# Level 4 — Range/window traversal 🪟

## Patterns in this level
- 🪟 Fixed-size sliding window
- 🧲 Variable-size sliding window (invariant-driven)
- 🧩 Segment traversal (process blocks/runs)

---

## 4.1 🪟 Fixed-size sliding window

### Why
Compute all length-k aggregates in O(n) instead of O(nk).

### What
Maintain a rolling state for window `[R-k+1 .. R]`.

### How (step/flow)
```
idx:  0 1 2 3 4 5
k=3: [0 1 2]
     [1 2 3]
     [2 3 4]
```

### Code (C#)
```csharp
// LENIENT:
// - null => 0
// - invalid k => 0
int MaxWindowSum(int[] a, int k)
{
    if (a == null) return 0;
    if (k <= 0 || k > a.Length) return 0;

    int sum = 0;
    for (int i = 0; i < k; i++) sum += a[i];

    int best = sum;
    for (int R = k; R < a.Length; R++)
    {
        sum += a[R];
        sum -= a[R - k];
        if (sum > best) best = sum;
    }
    return best;
}
```

### Where and When (use cases)
- Rolling sums/averages, fixed horizon monitoring.

### Common pitfalls
- Wrong leaving index (`R-k`).

### Tips and tricks
- Role naming: `R` enters; `R-k` leaves.

---

## 4.2 🧲 Variable-size sliding window (template)

### Why
This is the backbone of “longest/shortest subarray meeting a condition.”

### What
Use an outer pointer to expand and an inner loop to shrink until the invariant holds.

### How (step/flow)
```
Expand: move R right, add a[R] to state
Shrink: while invalid, remove a[L] from state, L++
Result: update answer using current [L..R]
```

### Code (C#) — skeleton
```csharp
// LENIENT template: decide your default answer.
int SlidingWindowTemplate(int[] a)
{
    if (a == null) return 0;

    int L = 0;
    // state variables here (sum, counts, etc.)

    int best = 0; // default

    for (int R = 0; R < a.Length; R++)
    {
        // include a[R] in state

        while (/* window invalid */ false)
        {
            // remove a[L] from state
            L++;
        }

        // window [L..R] is valid here
        // update best
    }

    return best;
}
```

### Where and When (use cases)
- Constraints like sum bounds, distinct count, max frequency, etc.

### Common pitfalls
- Updating `best` when the window is still invalid.

### Tips and tricks
- Write the invariant as a sentence and keep it next to the loop.

---

## 4.3 🧩 Segment traversal (process contiguous blocks)

### Why
Sometimes the array naturally decomposes into runs (equal values, positives, zeros, etc.).

### What
Advance `i`, then “consume a run” with `j` until the run ends.

### How (step/flow)
```
idx:  0 1 2 3 4 5 6
val:  1 1 1 0 0 2 2
runs: [1 1 1] [0 0] [2 2]
```

### Code (C#)
```csharp
// LENIENT:
// - null => 0
int CountRuns(int[] a)
{
    if (a == null) return 0;
    if (a.Length == 0) return 0;

    int runs = 0;
    int i = 0;
    while (i < a.Length)
    {
        runs++;
        int value = a[i];
        int j = i;
        while (j < a.Length && a[j] == value) j++;
        i = j;
    }
    return runs;
}
```

### Where and When (use cases)
- RLE compression logic, grouping, streak detection.

### Common pitfalls
- Forgetting `i = j` at the end (infinite loop).

### Tips and tricks
- Think of it as “outer loop finds a run start; inner loop finds run end.”

---

## ✅ Level 4 invariants checklist
- Fixed window: window always has exactly `k` elements once initialized.
- Variable window: state always corresponds to `[L..R]` and is updated symmetrically.
- Segment traversal: `i` always jumps to the next unprocessed index.

## 🪲 Level 4 debugging guide
Print/log:
- Windows: `L`, `R`, and your invariant metric (sum/count/map size).
- Segments: `i`, `j`, and the segment `[i..j-1]`.
Typical symptoms:
- Wrong window size → leaving index math wrong.
- Missing best answer → updating best before validity.
- Infinite loops → forgot to advance `L`, `R`, `i`, or `j`.

---

# 🧭 Mastery plan (Levels 2–4)

## Week-style progression 📅 (adjust freely)
- **Day 1–2:** Level 2 patterns (reverse, stride, wrap, mapping)
- **Day 3–4:** Level 3 patterns (two pointers, merge, partition)
- **Day 5–6:** Level 4 fixed window + segments
- **Day 7:** Level 4 variable window + full review

## Daily micro-routine (15–25 min) ⏱️
- 5 min: redraw the traversal diagram (index line / window / boundaries)
- 10 min: retype the template from memory (C#)
- 5–10 min: debug-run on a tiny input and log indices

---

## Next step (your choice)
Pick one to deepen next:
- 🧮 Level 2: wrap-around + circular buffers
- 👀 Level 3: partition boundaries (2-region → 3-region)
- 🪟 Level 4: variable sliding window with a real invariant (sum bound or distinct count)
