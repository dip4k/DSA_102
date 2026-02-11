# 🧠 Arrays Mastery Pack (Levels 5–9) — Lenient Contracts

> 🎯 Focus: **Abstract search-space traversal → prefix/suffix state → monotonic stacks → in-place partitions → performance-aware traversal**.

---

## ✅ Summary list
- 🧭 **Level 5:** Traversal as *states + transitions* (reachability, binary-search-as-navigation).
- 🧷 **Level 6:** Prefix/suffix carry-forward state (two-pass patterns).
- 📚 **Level 7:** Monotonic stack traversal (next greater/smaller, spans).
- 🧹 **Level 8:** In-place partition boundaries (2-way and 3-way regions).
- ⚙️ **Level 9:** Throughput/locality traversal (chunking, access order).
- 🤝 Contracts: **LENIENT** (return defaults; avoid throwing).
- 🧠 Includes: tips, tricks, edge cases, invariants, debugging logs.

---

## 🤝 Lenient contract rules (we’ll follow)
When inputs are invalid, return a safe default so the caller can continue.

- `int[] a == null` → return `false`, `0`, `-1`, or `Array.Empty<int>()` depending on intent.
- “No answer” → return `-1` (index), `false` (predicate), or empty array.
- Bad parameters (e.g., `k <= 0`) → return default (often `0`/`-1`/empty).

> ✅ Tip: Lenient doesn’t mean silent bugs—log/trace in debug builds if you need observability.

---

# Level 5 — Abstract search space traversal 🧭

## Patterns in this level
- 🦘 Reachability / frontier traversal (Jump Game style)
- 🧭 Binary search as navigation (index space traversal)
- 🧱 “Answer-space” binary search (monotonic predicate)

---

## 5.1 🦘 Reachability traversal (frontier)

### Why
Some arrays *define movement rules* (from index i you can jump/transition). Traversal becomes: “what indices are reachable?”

### What
Maintain a frontier `farthest` such that all indices `<= farthest` are reachable.

### How (step/flow)
```
Interpretation:
- i is a state
- a[i] defines how far you can go
- frontier expands as you discover better reach

idx:     0 1 2 3 4 5
reach:   ^^^^^^.....  (farthest grows)
```

### Code (C#)
```csharp
// LENIENT:
// - null => false
bool CanReachEnd(int[] a)
{
    if (a == null || a.Length == 0) return false;

    int farthest = 0;
    for (int i = 0; i <= farthest && i < a.Length; i++)
    {
        if (a[i] < 0) continue;            // lenient: ignore negative jumps
        farthest = Math.Max(farthest, i + a[i]);
        if (farthest >= a.Length - 1) return true;
    }

    return false;
}
```

### Code (Python)
```python
def can_reach_end(a):
    if not a:
        return False
    farthest = 0
    i = 0
    while i <= farthest and i < len(a):
        jump = a[i]
        if jump >= 0:
            farthest = max(farthest, i + jump)
            if farthest >= len(a) - 1:
                return True
        i += 1
    return False
```

### Where and When (use cases)
- Jump reachability, greedy frontiers, “maximum reachable” logic.

### Common pitfalls
- Traversing unreachable indices (missing the guard `i <= farthest`).
- Assuming all values are valid movement (negative/huge values need a policy).

### Tips and tricks
- Think “I traverse only what I can reach.” The loop condition encodes correctness.
- Frontier is your traversal boundary; if it stops growing, you’re stuck.

### Edge cases ✅
- `a == null` or `a.Length == 0` → `false`.
- `a.Length == 1` → `true` if you consider “already at end”; choose and document (this implementation returns `false` for empty, `true` once frontier logic runs—adjust if needed).
- All zeros except first index → only reachable if start is already end.
- Very large values → `i + a[i]` may overflow `int`; use `long` if needed.

---

## 5.2 🧭 Binary search as navigation (find-any)

### Why
Binary search is a traversal strategy over index space: you shrink a range by halving.

### What
Keep `[L..R]` as the candidate range; compute `mid`; move left or right.

### How (step/flow)
```
Range: [L........R]
Mid:      ^
Move boundary based on comparison
```

### Code (C#)
```csharp
// LENIENT:
// - null => -1
int BinarySearchAny(int[] a, int target)
{
    if (a == null || a.Length == 0) return -1;

    int L = 0, R = a.Length - 1;
    while (L <= R)
    {
        int mid = L + (R - L) / 2;
        int v = a[mid];

        if (v == target) return mid;
        if (v < target) L = mid + 1;
        else R = mid - 1;
    }

    return -1;
}
```

### Where and When (use cases)
- Search in sorted arrays, membership tests.

### Common pitfalls
- Using the wrong invariant for the variant you want (any vs first/last occurrence).

### Tips and tricks
- Print `(L, mid, R)` while learning; it makes mistakes obvious.

### Edge cases ✅
- Empty or null → `-1`.
- Duplicates → returns *some* match, not guaranteed first/last.

---

## 5.3 🧱 Answer-space binary search (monotonic predicate)

### Why
Sometimes you don’t search the array—you search the **answer** (minimum feasible value), using a monotonic “is feasible?” check.

### What
Define predicate `ok(x)` such that:
- `ok(x) == false` for small x
- `ok(x) == true` for large x
(or the reverse)

### How (step/flow)
```
Search space: low................high
Predicate flips once (monotonic)
Binary search for boundary
```

### Code (C#) — template
```csharp
// LENIENT template: caller decides defaults.
// Returns smallest x in [low..high] that satisfies ok(x), or -1 if none.
int MinXTrue(int low, int high, Func<int, bool> ok)
{
    if (ok == null) return -1;
    if (low > high) return -1;

    int ans = -1;
    int L = low, R = high;

    while (L <= R)
    {
        int mid = L + (R - L) / 2;
        if (ok(mid))
        {
            ans = mid;
            R = mid - 1;
        }
        else
        {
            L = mid + 1;
        }
    }

    return ans;
}
```

### Where and When (use cases)
- Min capacity, min days, max minimal distance, “can we do it with X?” type problems.

### Common pitfalls
- Predicate not actually monotonic.

### Tips and tricks
- Test `ok(x)` across a small range and ensure it flips once.

### Edge cases ✅
- No feasible x → return `-1`.
- Multiple feasible x → boundary search finds smallest true.

---

## ✅ Level 5 invariants checklist
- Frontier problems: process only reachable states; frontier never decreases.
- Binary search: range shrinks every iteration.
- Answer-space search: predicate is monotonic; you’re searching for a boundary.

## 🪲 Level 5 debugging guide
Log:
- Frontier: `i`, `a[i]`, `farthest`.
- Binary search: `L`, `mid`, `R`, `a[mid]`.
- Answer-space: `mid`, `ok(mid)`, and chosen move.
Typical symptoms:
- Infinite loop → boundaries not moving.
- Wrong boundary result → wrong update for `L/R` after `ok(mid)`.

---

# Level 6 — Prefix/suffix carry-forward 🧷

## Patterns in this level
- ➕ Prefix accumulation (running state)
- ➖ Suffix accumulation
- 🔄 Two-pass “exclude self” patterns

---

## 6.1 ➕ Prefix accumulation

### Why
You want “aggregate up to i” quickly (range queries, balance tracking).

### What
Carry a running value forward.

### How (step/flow)
```
a:    [2, 5, -1, 3]
pref: [2, 7,  6, 9]   (inclusive prefix sums)
```

### Code (C#)
```csharp
// LENIENT:
// - null => empty
int[] PrefixSums(int[] a)
{
    if (a == null || a.Length == 0) return Array.Empty<int>();

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
- Range sum queries, cumulative metrics.

### Common pitfalls
- Overflow with large values.

### Tips and tricks
- If overflow is possible, switch `run` and `pref` to `long`.

### Edge cases ✅
- Empty/null → empty output.
- All negatives → still correct.

---

## 6.2 ➖ Suffix accumulation

### Why
Same logic as prefix, but from the end; useful for “future contribution” problems.

### What
Traverse from right to left and carry state.

### How (step/flow)
```
a:     [2, 5, -1, 3]
suff:  [9, 7,  2, 3]  (inclusive suffix sums)
```

### Code (C#)
```csharp
// LENIENT:
// - null => empty
int[] SuffixSums(int[] a)
{
    if (a == null || a.Length == 0) return Array.Empty<int>();

    int n = a.Length;
    int[] suff = new int[n];
    int run = 0;

    for (int i = n - 1; i >= 0; i--)
    {
        run += a[i];
        suff[i] = run;
    }

    return suff;
}
```

### Where and When (use cases)
- Right-side contributions, suffix constraints.

### Common pitfalls
- Off-by-one in reverse loop.

### Tips and tricks
- Use `for (i=n-1; i>=0; i--)` and let empty arrays short-circuit.

---

## 6.3 🔄 Two-pass “exclude self” pattern

### Why
You often need “everything except i” without division or nested loops.

### What
First pass builds left contribution; second pass multiplies in right contribution.

### How (step/flow)
```
a:      [1, 2, 3, 4]
left:   [1, 1, 2, 6]   (product of items before i)
right:  [24,12,4,1]    (product of items after i)
ans:    [24,12,8,6]
```

### Code (C#)
```csharp
// LENIENT:
// - null => empty
int[] ProductExceptSelf(int[] a)
{
    if (a == null || a.Length == 0) return Array.Empty<int>();

    int n = a.Length;
    int[] ans = new int[n];

    int left = 1;
    for (int i = 0; i < n; i++)
    {
        ans[i] = left;
        left *= a[i];
    }

    int right = 1;
    for (int i = n - 1; i >= 0; i--)
    {
        ans[i] *= right;
        right *= a[i];
    }

    return ans;
}
```

### Where and When (use cases)
- Product-except-self, exclude-self sums, two-pass contributions.

### Common pitfalls
- Overflow, and handling zeros (this algorithm naturally supports zeros, but overflow still applies).

### Tips and tricks
- Replace multiplication with any associative operation you can “carry” (sum, XOR), if problem fits.

### Edge cases ✅
- One element → answer often `[1]` by convention (this implementation returns `[1]`).
- Multiple zeros → all answers become 0.

---

## ✅ Level 6 invariants checklist
- Running state equals aggregate of processed prefix/suffix.
- Two-pass: left pass writes “before i”; right pass multiplies “after i”.

## 🪲 Level 6 debugging guide
Log:
- Prefix: `i`, `run`, `pref[i]`.
- Two-pass: print `ans` after left pass, then after right pass.
Typical symptoms:
- Wrong by shift → you mixed inclusive/exclusive meaning.

---

# Level 7 — Monotonic stack traversal 📚

## Patterns in this level
- 📈 Next greater element (NGE)
- 📉 Next smaller element (NSE)
- 🧱 Span-style counts (how far until smaller/greater)

---

## 7.1 📈 Next greater element (NGE)

### Why
Some answers depend on the *next future element that beats current*. Stack stores unresolved indices until resolved.

### What
Keep indices in a stack such that their values are monotonic (commonly decreasing for NGE).

### How (step/flow)
```
a:   [2, 1, 5]
idx:  0  1  2

Process 2 -> stack [0]
Process 1 -> stack [0,1]
Process 5 -> resolves 1 then 2 (pop until stack top bigger than 5)
```

### Code (C#)
```csharp
// LENIENT:
// - null => empty
int[] NextGreater(int[] a)
{
    if (a == null || a.Length == 0) return Array.Empty<int>();

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
- Next greater/smaller, stock span, histogram problems.

### Common pitfalls
- Pushing values instead of indices (you lose where to write answers).

### Tips and tricks
- Say the invariant: “Stack indices correspond to decreasing values.”

### Edge cases ✅
- Strict vs non-strict: decide whether `>=` counts as “greater.”
- Duplicate values: your choice (`>` vs `>=`) changes results.

---

## 7.2 📉 Next smaller element (NSE) (pattern flip)

### Why
Same traversal, different comparison.

### What
Change the pop condition.

### How (step/flow)
- For NSE: pop while `a[i] < a[st.Peek()]`.

### Tips and tricks
- Keep one “master template” and swap the comparator.

---

## ✅ Level 7 invariants checklist
- Each index is pushed once and popped once → linear time.
- Stack monotonic property holds after each iteration.

## 🪲 Level 7 debugging guide
Log:
- `i`, `a[i]`, stack indices and their values after each step.
Typical symptoms:
- Wrong answers with duplicates → strictness (`>` vs `>=`) mismatch.

---

# Level 8 — In-place partition boundaries 🧹

## Patterns in this level
- 🧱 Two-way partition (< pivot | >= pivot)
- 🇳🇱 Three-way partition (0/1/2 or < = >)
- 🧲 Compaction (remove elements in-place)

---

## 8.1 🧱 Two-way partition (< pivot)

### Why
You can reorganize in one pass, in-place.

### What
Maintain boundary `L` = “next slot for small.”

### How (step/flow)
```
| < pivot ..... | unknown ........ |
 0            L                 i

If a[i] < pivot => swap into L, L++
```

### Code (C#)
```csharp
// LENIENT:
// - null => no-op
void PartitionLessThan(int[] a, int pivot)
{
    if (a == null || a.Length == 0) return;

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
- Quicksort partition step, bucketing, preprocessing.

### Common pitfalls
- Assuming stability (this is not stable).

### Tips and tricks
- If you need stability, you typically need extra space or a different strategy.

### Edge cases ✅
- All < pivot → L ends at n.
- None < pivot → L stays 0.

---

## 8.2 🇳🇱 Three-way partition (Dutch National Flag)

### Why
For 3 categories, you can sort in one pass by shrinking an unknown region.

### What
Maintain `low`, `mid`, `high`:
- `[0..low-1]` small
- `[low..mid-1]` middle
- `[mid..high]` unknown
- `[high+1..n-1]` large

### How (step/flow)
```
| small | middle | unknown........ | large |
 0     low      mid            high     n-1

Rules:
- if a[mid] is small => swap with low; low++, mid++
- if a[mid] is middle => mid++
- if a[mid] is large => swap with high; high-- (mid stays)
```

### Code (C#) — example for values 0/1/2
```csharp
// LENIENT:
// - null => no-op
void Sort012(int[] a)
{
    if (a == null || a.Length == 0) return;

    int low = 0, mid = 0, high = a.Length - 1;

    while (mid <= high)
    {
        int v = a[mid];
        if (v == 0)
        {
            (a[low], a[mid]) = (a[mid], a[low]);
            low++; mid++;
        }
        else if (v == 1)
        {
            mid++;
        }
        else if (v == 2)
        {
            (a[mid], a[high]) = (a[high], a[mid]);
            high--;
        }
        else
        {
            // LENIENT policy: unknown value, just skip it (or choose a different policy)
            mid++;
        }
    }
}
```

### Where and When (use cases)
- Bucketing categories, 3-way partition around a pivot.

### Common pitfalls
- Incrementing `mid` after swapping with `high` (you may skip an unknown).

### Tips and tricks
- Keep a mantra: “swap-with-high means re-check mid.”

### Edge cases ✅
- Arrays with values outside expected categories → pick a policy (skip, treat as middle, etc.).
- Already sorted → should still run safely.

---

## 8.3 🧲 In-place compaction (remove X)

### Why
Classic traversal + write-pointer pattern: keep what you want and overwrite the rest.

### What
Read pointer scans, write pointer advances only when keeping an element.

### How (step/flow)
```
read:  i walks all elements
write: w marks next keep slot

If keep(a[i]): a[w] = a[i]; w++
Answer length = w
```

### Code (C#)
```csharp
// LENIENT:
// - null => 0
// Returns the new logical length after removing target.
int RemoveInPlace(int[] a, int target)
{
    if (a == null || a.Length == 0) return 0;

    int w = 0;
    for (int i = 0; i < a.Length; i++)
    {
        if (a[i] != target)
        {
            a[w] = a[i];
            w++;
        }
    }
    return w;
}
```

### Where and When (use cases)
- Remove elements, filter in-place, compaction.

### Common pitfalls
- Forgetting that elements beyond returned length are “don’t care.”

### Tips and tricks
- This traversal is stable for kept elements (relative order preserved).

### Edge cases ✅
- All removed → returns 0.
- None removed → returns n.

---

## ✅ Level 8 invariants checklist
- Partition: boundary indices correctly separate regions after each step.
- Compaction: `[0..w-1]` always contains exactly the kept items.

## 🪲 Level 8 debugging guide
Log:
- Partition: `low`, `mid`, `high` plus array snapshot for small inputs.
- Compaction: `i`, `w`, and what was kept/removed.
Typical symptoms:
- Missing elements → write pointer update wrong.
- Infinite loop → pointer not moving (mid/high confusion).

---

# Level 9 — Performance-aware traversal ⚙️

## Patterns in this level
- 📦 Chunked traversal
- 🧊 Locality-friendly access
- 🚫 Avoiding unnecessary passes

---

## 9.1 📦 Chunked traversal

### Why
Large arrays benefit from processing in blocks (better cache behavior, easier parallelization later).

### What
Process `[start..end)` in chunks.

### How (step/flow)
```
Chunk size = B
[0..B) [B..2B) [2B..3B) ...
```

### Code (C#)
```csharp
// LENIENT:
// - null => 0
int SumChunked(int[] a, int blockSize)
{
    if (a == null || a.Length == 0) return 0;
    if (blockSize <= 0) blockSize = 1024; // lenient fallback

    long sum = 0;

    for (int start = 0; start < a.Length; start += blockSize)
    {
        int end = Math.Min(start + blockSize, a.Length);
        for (int i = start; i < end; i++)
            sum += a[i];
    }

    // lenient: clamp to int range (or change return type to long)
    if (sum > int.MaxValue) return int.MaxValue;
    if (sum < int.MinValue) return int.MinValue;
    return (int)sum;
}
```

### Where and When (use cases)
- Analytics on big arrays, pipeline stages, batch processing.

### Common pitfalls
- Choosing a block size without measuring (profile before tuning).

### Tips and tricks
- Start simple (sequential scan), then chunk only if needed.

### Edge cases ✅
- blockSize larger than n → just one chunk.
- sum overflow → pick a policy (use `long`, clamp, or return `long`).

---

## 9.2 🧊 Locality-friendly access

### Why
Even with the same Big-O, memory access patterns can dominate runtime.

### What
Prefer sequential access over random jumps.

### How (step/flow)
- ✅ Good: single forward scan
- ⚠️ Risky: bouncing around indices in unpredictable order

### Tips and tricks
- If you must do random access, see if you can reorder work (batch indices, sort queries, or cache results).

---

## ✅ Level 9 invariants checklist
- Correctness first: block boundaries cover all indices exactly once.
- No missing/overlapping chunks: next start = previous end.

## 🪲 Level 9 debugging guide
Log:
- `(start, end)` for chunks and total processed count.
Typical symptoms:
- Missing elements → chunk end math wrong.
- Double-counting → overlapping ranges.

---

# 🧠 Cross-level tips & edge-case playbook

## Movement rules (memorize) 🧭
- Every loop must make progress: a pointer/index changes in every iteration.
- Every read must be guarded: check bounds before reading.

## Edge cases you should test mentally ✅
- `null` array (lenient default)
- empty array `[]`
- single element `[x]`
- all equal values
- strictly increasing / strictly decreasing
- duplicates around boundary conditions
- very large values (overflow risk)

## Debug print pack 🪲
- Pointers: print `i`, or `(L,R)`, or `(low,mid,high)`.
- State: print `farthest`, window metric, stack contents, write index.

---

## Next step
Tell me which of these you want to deepen with a few **guided examples** (still not drills), and whether you want Python snippets everywhere or only where it adds clarity:
- 🧭 Answer-space binary search (hardest to get right)
- 📚 Monotonic stack variants (NGE/NSE + span)
- 🧹 Three-way partition with custom categories
