# 🧭 Array Traversal Mastery (Concepts + Code)

> **Goal:** Build deep intuition for how to *walk arrays correctly and efficiently*—index movement, pointer-style thinking, range navigation, and traversal-as-state.

---

## ✅ Summary list
- 🧱 Level-by-level roadmap (Level 1 → Level 9+ advanced).
- 🧠 For every pattern: **Why / What / How (step/flow) / Where and When / Common pitfalls / Tips and tricks**.
- 🧰 Visuals: index lines, pointer diagrams, window diagrams, small tables.
- 💻 Code snippets: **C# (primary)** + **Python (secondary)**.
- 🪲 Debugging: invariants + what to print/log (no drills yet).

---

## 🗺️ Roadmap (arrays only)
- **Level 1:** Physical movement (linear walk)
- **Level 2:** Index arithmetic (reverse, stride, wrap)
- **Level 3:** Multi-cursor traversal (two pointers, fast/slow style)
- **Level 4:** Range/window traversal (sliding window, segments)
- **Level 5:** Abstract “state graph” traversal (jump rules, reachability)
- **Level 6 (optional):** Prefix/suffix traversal (carry-forward state)
- **Level 7 (optional):** Monotonic stack traversal (next greater/smaller)
- **Level 8 (advanced):** In-place partition traversal (three-way, Dutch flag)
- **Level 9 (advanced):** Cache/throughput traversal (chunking, locality)

---

# Level 1 — Physical movement 🚶

## Patterns in this level
- ➡️ Linear forward scan
- ⬅️ Linear backward scan (preview)
- 🔍 Sentinel-style termination (stop on a condition)

---

## 1) ➡️ Linear forward scan

### Why
Most array bugs are *movement bugs*: forgetting to advance, stopping too early, or reading out of bounds.

### What
Walk indices from left to right: `i = 0, 1, 2, ... n-1`.

### How (step/flow)
**Index line**
```
arr:  [10][20][30][40]
idx:   0   1   2   3
move:  i→  i→  i→  i→ stop
```

**C#**
```csharp
int Sum(int[] a)
{
    if (a == null) throw new ArgumentNullException(nameof(a));

    int sum = 0;
    for (int i = 0; i < a.Length; i++)
        sum += a[i];

    return sum;
}
```

**Python**
```python
def sum_array(a):
    if a is None:
        raise ValueError("a is None")
    s = 0
    for i in range(len(a)):
        s += a[i]
    return s
```

### Where and When (use cases)
- Counting, filtering, min/max, first match, transforming to another array.

### Common pitfalls
- Off-by-one: using `i <= a.Length - 1` *works* but is more error-prone than `i < a.Length`.
- Null array reference (C#): always decide your contract.

### Tips and tricks
- Say the invariant: “Before reading `a[i]`, I guarantee `0 <= i < a.Length`.”
- Prefer `i < a.Length` as your loop condition.

---

## 2) 🔍 Sentinel-style termination

### Why
Some traversals aren’t “visit everything.” You stop when you *find* something or reach a logical boundary.

### What
Move forward until a condition is met.

### How (step/flow)
**Example:** find first index where value is negative.

**C#**
```csharp
int FirstNegativeIndex(int[] a)
{
    if (a == null) throw new ArgumentNullException(nameof(a));

    int i = 0;
    while (i < a.Length && a[i] >= 0)
        i++;

    return (i == a.Length) ? -1 : i;
}
```

### Where and When (use cases)
- Find first/last occurrence, stop-on-threshold, scan until pivot.

### Common pitfalls
- Forgetting to advance `i` in the loop body.
- Checking `a[i]` before verifying `i < a.Length`.

### Tips and tricks
- Put bounds first in compound conditions: `i < n && condition(a[i])`.

---

## ✅ Level 1 invariants checklist
- `i` is always in `[0..n]`.
- Every read `a[i]` happens only when `i < n`.
- The loop makes progress (`i` increases).

## 🪲 Level 1 debugging (what to log)
- Print: `i`, `a[i]` (only when valid), and the reason you stop.
- Symptom map: stops too early → condition wrong; crashes → bounds wrong; infinite loop → missing increment.

---

# Level 2 — Index arithmetic 🧮

## Patterns in this level
- ⬅️ Reverse traversal
- 🦘 Stride / step traversal (`i += k`)
- 🔁 Cyclic traversal (modulo wrap)
- 🧭 2D-to-1D mapping (array-backed grid)

---

## 1) ⬅️ Reverse traversal

### Why
Many problems are “from the end”: suffix checks, in-place edits, compaction.

### What
Walk `i = n-1 ... 0`.

### How (step/flow)
```
arr:  [10][20][30][40]
idx:   0   1   2   3
move:            i←  i←  i←  i← stop
```

**C#**
```csharp
int LastIndexOf(int[] a, int target)
{
    if (a == null) throw new ArgumentNullException(nameof(a));

    for (int i = a.Length - 1; i >= 0; i--)
        if (a[i] == target) return i;

    return -1;
}
```

### Where and When (use cases)
- Find last match, right-to-left constraints, build reversed output.

### Common pitfalls
- Underflow: `i--` when `i` is `0` is fine; the loop must stop at `i >= 0`.
- Empty array: start index becomes `-1` (handle naturally by loop condition).

### Tips and tricks
- Prefer `for` for reverse loops; it keeps decrement next to the condition.

---

## 2) 🦘 Stride / step traversal

### Why
You sometimes sample every k-th element or traverse “lanes” (even/odd indices).

### What
Use `i += step`.

### How (step/flow)
```
idx:  0 1 2 3 4 5 6 7
visit ^   ^   ^   ^
step=2 => 0,2,4,6
```

**C#**
```csharp
int SumEveryKth(int[] a, int step)
{
    if (a == null) throw new ArgumentNullException(nameof(a));
    if (step <= 0) throw new ArgumentOutOfRangeException(nameof(step));

    int sum = 0;
    for (int i = 0; i < a.Length; i += step)
        sum += a[i];

    return sum;
}
```

### Where and When (use cases)
- Sampling, downscaling, skipping headers, parity scans.

### Common pitfalls
- `step == 0` → infinite loop.

### Tips and tricks
- Always validate `step > 0`.

---

## 3) 🔁 Cyclic traversal (modulo)

### Why
Circular buffers and wrap-around schedules are “arrays with a rotating start.”

### What
Advance like `i = (i + 1) % n`.

### How (step/flow)
```
idx:  0 1 2 3 4
wrap: 0→1→2→3→4→0→...
```

**C#**
```csharp
int NextIndex(int i, int n) => (i + 1) % n;
```

### Where and When (use cases)
- Ring buffers, round-robin iteration.

### Common pitfalls
- `n == 0` (mod by zero).
- Negative indices: define whether you allow them; if you do, normalize carefully.

### Tips and tricks
- Treat “next index” as a function and test it on tiny `n` values.

---

## 4) 🧭 2D-to-1D mapping (array-backed grid)

### Why
Many “matrix” problems can be stored in a flat array for speed/space; traversal depends on correct mapping.

### What
Mapping with `cols`:
- `idx = r * cols + c`
- `r = idx / cols`
- `c = idx % cols`

### How (step/flow)
```
rows=3, cols=4
(r,c) -> idx
(0,0)=0  (0,1)=1  (0,2)=2  (0,3)=3
(1,0)=4  (1,1)=5  (1,2)=6  (1,3)=7
(2,0)=8  (2,1)=9 (2,2)=10 (2,3)=11
```

**C#**
```csharp
int Index(int r, int c, int cols) => r * cols + c;
```

### Where and When (use cases)
- Image buffers, game boards, DP tables stored flat.

### Common pitfalls
- Mixing up rows vs cols.

### Tips and tricks
- Verify using a 2x3 grid on paper before coding.

---

## ✅ Level 2 invariants checklist
- Your “next index” formula never produces out-of-range indices.
- In reverse loops: you never read when `i < 0`.
- In cyclic loops: `n > 0`.

## 🪲 Level 2 debugging (what to log)
- Log the sequence of indices visited (not just values).
- For 2D mapping: log `(r,c,idx)` triples for a few points.

---

# Level 3 — Multi-cursor traversal 👀👀

## Patterns in this level
- ↔️ Two pointers (left/right)
- 🪟 Two pointers as a moving boundary (precursor to windows)
- 🔀 Merge-style traversal (two sorted arrays)

---

## 1) ↔️ Two pointers (left/right)

### Why
You can solve in one pass what would otherwise take nested loops.

### What
Maintain `L` and `R` and move them toward each other.

### How (step/flow)
```
idx:  0 1 2 3 4 5
      L         R
move: L→ ... ←R
```

**C# (palindrome-style check on int array)**
```csharp
bool IsMirror(int[] a)
{
    if (a == null) throw new ArgumentNullException(nameof(a));

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
- Pairing from ends, partitioning around a pivot, “meet in the middle.”

### Common pitfalls
- Using `L <= R` when you don’t mean to compare the middle with itself.

### Tips and tricks
- Decide the stop condition by what “unfinished work” means: usually `L < R`.

---

## 2) 🔀 Merge-style traversal

### Why
Two sorted inputs can be traversed with two indices in linear time.

### What
Use `i` for A, `j` for B; always advance the one you consumed.

### How (step/flow)
```
A: [1,3,7]
B: [2,3,8]
 i^     
 j^     
Take smaller (or both if equal), advance pointers.
```

**C# (count common elements, handles duplicates simply)**
```csharp
int CountCommonSorted(int[] a, int[] b)
{
    if (a == null) throw new ArgumentNullException(nameof(a));
    if (b == null) throw new ArgumentNullException(nameof(b));

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
- Merging sorted streams, intersection, dedup, diff-like scans.

### Common pitfalls
- Not advancing one pointer in every branch → infinite loop.

### Tips and tricks
- Guarantee progress: every loop iteration must increment `i` or `j` (or both).

---

## ✅ Level 3 invariants checklist
- Two-pointer loops always move at least one pointer.
- All reads are guarded (`i < n`, `j < m`).

## 🪲 Level 3 debugging (what to log)
- Print `(L,R)` or `(i,j)` each iteration plus the chosen move.

---

# Level 4 — Range/window traversal 🪟

## Patterns in this level
- 🪟 Sliding window (fixed size)
- 🧲 Sliding window (variable size with invariant)
- 🧩 Segment traversal (process contiguous blocks)

---

## 1) 🪟 Sliding window (fixed size)

### Why
You want aggregates over every length-k subarray without recomputing from scratch.

### What
Maintain a window sum/state and roll it forward.

### How (step/flow)
```
idx:  0 1 2 3 4 5
k=3: [0 1 2]
     [1 2 3]
     [2 3 4]
```

**C# (max sum of any k-length window)**
```csharp
int MaxWindowSum(int[] a, int k)
{
    if (a == null) throw new ArgumentNullException(nameof(a));
    if (k <= 0 || k > a.Length) throw new ArgumentOutOfRangeException(nameof(k));

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
- Rolling averages, throughput monitoring, fixed horizon analytics.

### Common pitfalls
- Wrong “element leaving window” index (`R-k`).

### Tips and tricks
- Name indices by role: `R` is the entering index; `R-k` is leaving.

---

## 2) 🧲 Sliding window (variable size)

### Why
You need the smallest/largest window meeting a condition.

### What
Expand `R` to include more, shrink `L` to restore validity.

### How (step/flow)
```
Maintain invariant:
- Expand R until condition satisfied.
- Shrink L while still satisfied.
```

**C# (concept snippet: template)**
```csharp
// Template only: you define the invariant + how to update state.
int L = 0;
for (int R = 0; R < a.Length; R++)
{
    // include a[R] in state

    while (/* window invalid */)
    {
        // remove a[L] from state
        L++;
    }

    // window [L..R] is valid here
}
```

### Where and When (use cases)
- Longest/shortest subarray with constraint (sum, distinct count, etc.).

### Common pitfalls
- Updating state in the wrong order when moving `L`.

### Tips and tricks
- Write the invariant as a sentence: “Window is valid iff ...”

---

## ✅ Level 4 invariants checklist
- Window is always a valid index range: `0 <= L <= R < n` (when non-empty).
- State matches the window exactly (every include has a matching remove).

## 🪲 Level 4 debugging (what to log)
- Log `L`, `R`, and the invariant metric (sum/count/map size).

---

# Level 5 — Abstract “state graph” traversal 🧠

## Patterns in this level
- 🦘 Jump game style reachability
- 🧭 Binary search as a traversal on index space

---

## 1) 🦘 Reachability traversal (jump rules)

### Why
Some arrays define transitions (from index i, you can jump to other indices). Traversal becomes reachability.

### What
You’re not “visiting every index” anymore; you’re exploring what you *can reach*.

### How (step/flow)
```
state: i
moves: i -> i + a[i] (or range i+1..i+a[i])
goal: reach last index
```

**C# (greedy reach frontier; traversal of reachable prefix)**
```csharp
bool CanReachEnd(int[] a)
{
    if (a == null) throw new ArgumentNullException(nameof(a));
    int farthest = 0;

    for (int i = 0; i <= farthest && i < a.Length; i++)
        farthest = Math.Max(farthest, i + a[i]);

    return farthest >= a.Length - 1;
}
```

### Where and When (use cases)
- Jump reachability, constraints propagation, greedy frontiers.

### Common pitfalls
- Forgetting the guard `i <= farthest` (you might traverse unreachable indices).

### Tips and tricks
- Think “frontier”: the traversal range is `[0..farthest]` and it grows.

---

## 2) 🧭 Binary search (traversal on index space)

### Why
Binary search is a *navigation strategy* over an ordered array: you move by halving.

### What
Maintain a search interval and shrink it.

### How (step/flow)
```
Range: [L........R]
Mid:      ^
If mid too small => L = mid+1
Else => R = mid-1 (or keep mid depending on variant)
```

**C# (classic find)**
```csharp
int BinarySearch(int[] a, int target)
{
    if (a == null) throw new ArgumentNullException(nameof(a));

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

### Where and When (use cases)
- Search in sorted arrays, lower/upper bound variants, answer-space search.

### Common pitfalls
- Mixing variants (find-any vs lower-bound) without clear invariants.

### Tips and tricks
- Always write your invariant: “target must be inside [L..R] if it exists.”

---

## ✅ Level 5 invariants checklist
- Your “state transitions” never read out of bounds.
- Your reachable/frontier logic never processes unreachable states.
- For binary search: each iteration shrinks the interval.

## 🪲 Level 5 debugging (what to log)
- Jump reachability: log `i` and `farthest`.
- Binary search: log `(L, mid, R)` each iteration.

---

# Level 6 (optional) — Prefix/suffix traversal 🧷

## Patterns in this level
- ➕ Prefix accumulation
- ➖ Suffix accumulation
- 🔄 Two-pass carry-forward

---

## 1) ➕ Prefix accumulation

### Why
You often need “everything up to i” quickly; traversal builds a running state.

### What
Carry a cumulative value forward.

### How (step/flow)
```
a:    [2, 5, -1, 3]
pref: [2, 7,  6, 9]
```

**C#**
```csharp
int[] PrefixSums(int[] a)
{
    if (a == null) throw new ArgumentNullException(nameof(a));

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

### Where and When (use cases)
- Range sum queries, running totals, “balance” style problems.

### Common pitfalls
- Integer overflow (choose `long` when needed).

### Tips and tricks
- Define `pref[i]` meaning precisely (inclusive vs exclusive).

---

## ✅ Level 6 invariants checklist
- Running state equals the aggregate over the processed prefix.

## 🪲 Level 6 debugging (what to log)
- Print `i`, `run`, and `pref[i]`.

---

# Level 7 (optional) — Monotonic stack traversal 📚

## Patterns in this level
- 📈 Next greater element (NGE)
- 📉 Next smaller element

---

## 1) 📈 Next greater element (NGE)

### Why
Some traversals require “remembering unresolved indices” until a future element resolves them.

### What
Use a stack of indices whose next-greater hasn’t been found yet.

### How (step/flow)
```
a:   [2, 1, 5]
idx:  0  1  2
Stack holds indices with decreasing values.
When you see 5, it resolves 1 and 2.
```

**C# (core idea)**
```csharp
int[] NextGreater(int[] a)
{
    if (a == null) throw new ArgumentNullException(nameof(a));

    int n = a.Length;
    int[] ans = new int[n];
    Array.Fill(ans, -1);

    var st = new Stack<int>(); // indices

    for (int i = 0; i < n; i++)
    {
        while (st.Count > 0 && a[i] > a[st.Peek()])
            ans[st.Pop()] = a[i];

        st.Push(i);
    }

    return ans;
}
```

### Where and When (use cases)
- Stock span variants, nearest greater/smaller, histogram-like scans.

### Common pitfalls
- Storing values instead of indices (you usually need indices to fill answers).

### Tips and tricks
- Describe the stack invariant: “indices on stack have decreasing values.”

---

## ✅ Level 7 invariants checklist
- Stack maintains monotonic property.
- Each index is pushed once and popped once (linear time).

## 🪲 Level 7 debugging (what to log)
- Log stack indices and their values after each iteration.

---

# Level 8 (advanced) — In-place partition traversal 🧹

## Patterns in this level
- 🇳🇱 Three-way partition (Dutch National Flag)
- 🧱 Stable vs unstable compaction thinking

---

## 1) 🇳🇱 Three-way partition

### Why
You can reorder in one pass by moving boundaries around three regions.

### What
Maintain three zones:
- `[0..low-1]` = small
- `[low..mid-1]` = equal
- `[mid..high]` = unknown
- `[high+1..n-1]` = large

### How (step/flow)
```
| small | equal | unknown........ | large |
 0     low     mid             high    n-1
```

**C# (concept snippet)**
```csharp
void Partition012(int[] a)
{
    if (a == null) throw new ArgumentNullException(nameof(a));

    int low = 0, mid = 0, high = a.Length - 1;
    while (mid <= high)
    {
        if (a[mid] == 0) { (a[low], a[mid]) = (a[mid], a[low]); low++; mid++; }
        else if (a[mid] == 1) { mid++; }
        else { (a[mid], a[high]) = (a[high], a[mid]); high--; }
    }
}
```

### Where and When (use cases)
- Bucketing small sets of categories, quick partition steps.

### Common pitfalls
- Incrementing `mid` after swapping with `high` (you might skip unknown).

### Tips and tricks
- Treat `mid` as “current unknown.” Only increment it when you know it’s resolved.

---

## ✅ Level 8 invariants checklist
- Regions remain correct after every operation.
- “Unknown” region shrinks every iteration.

## 🪲 Level 8 debugging (what to log)
- Log `low`, `mid`, `high` each loop plus the array snapshot for small inputs.

---

# Level 9 (advanced) — Cache/throughput traversal ⚙️

## Patterns in this level
- 📦 Chunked traversal
- 🧊 Locality-friendly access patterns

### Why
Even correct traversals can be slow; access order matters for performance.

### What
Prefer sequential memory access; process in chunks for large arrays.

### How (step/flow)
- Walk contiguous ranges when possible.
- Avoid random index jumps unless algorithm requires it.

### Where and When (use cases)
- Data processing pipelines, analytics, large buffers.

### Common pitfalls
- Premature optimization (optimize only after correctness + profiling).

### Tips and tricks
- First: make invariants obvious. Then: measure.

---

# 🧭 Decision guides (arrays)

## ✅ Pick the traversal pattern
- 🧱 Need every element once in order → **Level 1 linear scan**.
- 🔁 Need reverse/suffix logic → **Level 2 reverse**.
- ↔️ Need pair reasoning / from-ends checks → **Level 3 two pointers**.
- 🪟 Need best subarray under constraints → **Level 4 sliding window**.
- 🦘 Moves depend on values (reachability/jumps) → **Level 5 state traversal**.
- ➕ Need many range sums → **Level 6 prefix sums**.
- 📚 Need next greater/smaller relationships → **Level 7 monotonic stack**.
- 🧹 Need in-place bucketing → **Level 8 partition boundaries**.

## ✅ Pick loop style (C#)

### `for` vs `while` vs `foreach`
| Scenario | Prefer `for` | Prefer `while` | Prefer `foreach` |
|---|---|---|---|
| Fixed movement `i++` over full array | ✅ Most readable | ➖ Works but more boilerplate | ✅ If you don’t need index |
| Two pointers (L/R) | ✅ Often clean | ✅ If moves depend on conditions | ❌ Needs indices |
| Sliding window with inner shrink loop | ✅ Outer `for (R...)` | ✅ Inner `while (invalid)` | ❌ Needs control |
| Sentinel stop (“until found”) | ➖ Possible | ✅ Natural expression | ❌ Hard to stop early with context |
| Need index for writes (in-place edits) | ✅ | ✅ | ❌ |

---

# 🧰 Mini cheat sheets

## Off-by-one checklist ✅
- Are you using `i < n` (safe) or `i <= n-1` (easy to slip)?
- If using windows, is it `[L..R]` inclusive or `[L..R)` half-open? Pick one and stick to it.

## What to log when stuck 🪲
- Linear scans: `i`, `a[i]`.
- Two pointers: `(L,R)` plus which one moved.
- Windows: `(L,R)` and the invariant metric.
- Binary search: `(L,mid,R)` and the branch taken.

---

## Next step
Tell me which levels you want to master first (e.g., Level 1–2, or Level 3–4), and whether you prefer **STRICT** or **LENIENT** contracts when we later add drills.
