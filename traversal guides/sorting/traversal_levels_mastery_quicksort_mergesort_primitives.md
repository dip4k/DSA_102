# 🧭 Traversal Levels Mastery: Quicksort + Mergesort Primitives

> **Goal:** Achieve mastery over the 3 primitives that power Quicksort and Mergesort:
> - 🧩 **Partition boundaries** (Levels 3 & 8)
> - 🔀 **Two-list merge traversal** (Level 3)
> - 🪟 **Range recursion over subarrays** (Levels 4 & 5)
>
> You’ll learn them as **traversal levels**, each with: **Why / What / How (step-by-step) / Where / Edge cases / Pitfalls / Tips**.

---

## ✅ Summary list
- **Level 1:** Cursor safety & linear scan foundations (strings/arrays)
- **Level 2:** Index arithmetic & boundary math (reverse/stride/wrap)
- **Level 3:** Multi-cursor traversal (two pointers, merge cursors, boundary cursors)
- **Level 4:** Range traversal (subarray segments, inclusive vs half-open)
- **Level 5:** Recursion as traversal (divide-and-conquer call stack over ranges) [web:229]
- **Level 6 (supporting):** Stable vs unstable movement, and tie-breaking
- **Level 7 (supporting):** Copy-back / buffer traversal discipline (merges)
- **Level 8:** In-place partition region invariants (2-way and 3-way) [web:213]
- **Level 9:** Performance traversal (reuse buffers, reduce allocations, reduce swaps)

> Note: Quicksort’s core is partitioning a range around a pivot. [web:213]
> Merge sort’s core is dividing into sublists and merging sorted lists. [web:238]

---

# Level 1 — Cursor safety foundations 🚶

## Why
Every “advanced” traversal (partition, merge, recursion) collapses if you can’t guarantee safe reads and guaranteed progress.

## What
Two rules:
- ✅ **Bounds rule:** read only when index is valid.
- ✅ **Progress rule:** in every loop iteration, at least one index moves toward termination.

## How (step/flow)
Checklist you apply to any loop:
1) What variables index into the data? (`i`, `j`, `L`, `R`, `lo`, `hi`, `mid`)
2) What is the legal range for each?
3) Which variables *must* move each iteration?
4) What is the stop condition and what does it mean?

## Where
- Partition loops, merge loops, recursive range loops.

## Edge cases ✅
- Empty range (`lo > hi`)
- Single element (`lo == hi`)
- All elements equal (progress must still happen)

## Common pitfalls
- Dereferencing before bounds checks.
- Infinite loops when “no move” branch exists.

## Tips and tricks
- Print indices first, values second (values might be invalid to read).

---

# Level 2 — Index arithmetic & boundary math 🧮

## Why
Partition and merge are *boundary math* problems as much as they are comparisons.

## What
You must be fluent in:
- Inclusive ranges: `[lo..hi]`
- Half-open ranges: `[lo..hi)`
- Converting between them without off-by-one errors

## How (step/flow)
### Decide ONE range convention per implementation
- If you choose inclusive `[lo..hi]`:
  - base case: `lo >= hi`
  - left half: `[lo..mid]`
  - right half: `[mid+1..hi]`

### Midpoint safe formula
- `mid = lo + (hi - lo) / 2`

## Where
- Mergesort range splits
- Quicksort recursion boundaries

## Edge cases ✅
- `hi - lo` can overflow if you use `(lo + hi) / 2` on huge indices; use the safe formula.

## Common pitfalls
- Mixing conventions (half-open split but inclusive base case).

## Tips and tricks
- Write a one-line “range contract” comment at function top.

---

# Level 3 — Multi-cursor traversal 👀👀

This is where the two main primitives live:
- 🔀 **Merge traversal**
- 🧩 **Partition boundary traversal (as multiple cursors)**

---

## 3A) 🔀 Primitive: Two-list merge traversal

### Why
Mergesort’s combine step is literally “merge two sorted sequences.” [web:238]

### What
You maintain two read cursors and one write cursor:
- `i` over left
- `j` over right
- `k` writes output

### How (step/flow)
**Invariant**
- Output `[lo..k-1]` is always sorted and equals the smallest `k-lo` items seen so far.

**Steps**
1) Compare `left[i]` and `right[j]`.
2) Write the smaller into output, advance that cursor.
3) When one side ends, copy the remainder of the other side.

### Walkthrough (tiny)
Left `[1,4,9]`, Right `[2,3,10]`

| Step | i | L[i] | j | R[j] | Pick | Out |
|---:|---:|---:|---:|---:|:---|:---|
| 0 | 0 | 1 | 0 | 2 | 1 | [1] |
| 1 | 1 | 4 | 0 | 2 | 2 | [1,2] |
| 2 | 1 | 4 | 1 | 3 | 3 | [1,2,3] |
| 3 | 1 | 4 | 2 | 10 | 4 | [1,2,3,4] |
| 4 | 2 | 9 | 2 | 10 | 9 | [1,2,3,4,9] |
| end | - | - | - | - | copy 10 | [1,2,3,4,9,10] |

### Where
- Mergesort merge step
- Merging logs, merging sorted streams

### Edge cases ✅
- One side empty → output is the other side.
- Duplicates → decide stability rule: use `<=` to keep left-first stability.

### Common pitfalls
- Forgetting to copy remainders.
- Using `<` when stability is required.

### Tips and tricks
- Always ensure one cursor moves on every comparison.

---

## 3B) 🧩 Primitive: Partition boundaries as multi-cursor traversal

### Why
Partitioning is the real work of quicksort: reorder a range around a pivot. [web:213]

### What
You maintain boundaries that separate “regions” and shrink the unknown region.

### How (step/flow)
The mental model is always:
```
| region A | region B | unknown........ | region C |
```
Your loop moves one cursor each step so unknown shrinks.

### Where
- Quicksort partition
- In-place bucketing

### Edge cases ✅
- Many equals to pivot (choose 3-way partition)
- Already sorted inputs (pivot choice matters)

### Common pitfalls
- Not re-checking the swapped-in element (common in 3-way partition).

### Tips and tricks
- Draw regions and label them every time.

---

# Level 4 — Range traversal over subarrays 🪟

## Why
Both sorts work on **subranges** repeatedly; your correctness depends on consistent range handling.

## What
Range traversal means:
- Passing `(lo, hi)` correctly
- Not crossing boundaries
- Treating empty/single-element ranges as base cases

## How (step/flow)
For a function working on inclusive ranges:
1) Base case: if `lo >= hi` return.
2) Ensure recursive calls strictly reduce the range size.
3) Ensure combine step touches exactly the intended range.

### Where
- Quicksort recursion after partition
- Mergesort recursion before merge

### Edge cases ✅
- Partition returns boundary near edges (`p == lo` or `p == hi`)
- Very small ranges (2 items) must still reduce

### Common pitfalls
- Off-by-one in recursion calls (especially if you switch partition schemes).

### Tips and tricks
- Log `(lo, hi)` for every recursive call until intuition forms.

---

# Level 5 — Recursion as traversal (divide-and-conquer) 🧭

## Why
Divide-and-conquer is itself a traversal over a **tree of subproblems**: split → solve → combine. [web:229]

## What
Recursion visits subranges like a DFS:
- Enter range
- Recurse children
- Combine
- Exit

## How (step/flow)
### Generic template
```
Solve(range):
  if small: solve directly
  split into left/right
  Solve(left)
  Solve(right)
  Combine(left,right)
```
This describes both quicksort (combine=partition then recurse) and mergesort (recurse then combine=merge). [web:229][web:238][web:213]

### Where
- Quicksort and mergesort control flow

### Edge cases ✅
- Deep recursion risk (quicksort worst-case pivot choices)
- Stack depth concerns for huge n

### Common pitfalls
- Forgetting that “combine” must preserve invariants for parent range.

### Tips and tricks
- Visualize the recursion tree; each level processes disjoint ranges.

---

# Level 6 — Supporting mastery: stability & tie-breaking 🧷

## Why
Mergesort can be stable depending on merge tie-breaking; quicksort is typically not stable.

## What
Tie-breaking rule in merge:
- `<=` picks left first → stable order preservation

## How (step/flow)
- In merge, when `L[i] == R[j]`, choose from left first.

## Where
- Mergesort, stable multi-key sorts

## Edge cases ✅
- Many duplicates (stability changes output ordering of equals)

## Common pitfalls
- Changing `<` to `<=` without realizing it changes stability behavior.

## Tips and tricks
- If you need stable sorting, mergesort is a safer conceptual fit.

---

# Level 7 — Supporting mastery: buffer traversal discipline 📦

## Why
Mergesort requires a temp buffer; most bugs are copy-back and boundary mistakes.

## What
You must master:
- “write into temp then copy back”
- correct indices for temp writes

## How (step/flow)
1) Merge into `temp[lo..hi]`.
2) Copy `temp[lo..hi]` back to `a[lo..hi]`.

## Edge cases ✅
- `lo == hi` should not write or copy unnecessarily.

## Pitfalls
- Copying wrong range (e.g., copying full temp each time).

## Tips and tricks
- Reuse one temp array for whole algorithm (Level 9 performance discipline).

---

# Level 8 — In-place partition invariants (2-way and 3-way) 🧹

## Why
This is the *core primitive* behind quicksort’s speed: one-pass in-place reorder around pivot. [web:213]

## What
Two must-master variants:
- **2-way partition**: `< pivot` vs `>= pivot`
- **3-way partition**: `< pivot` vs `== pivot` vs `> pivot` (Dutch National Flag)

## How (step/flow)
### 8A) 2-way partition (boundary `store`)
**Invariant** (Lomuto-style):
- `[lo..store-1] < pivot`
- `[store..j-1] >= pivot`
- `[j..hi-1] unknown`

Step-by-step:
1) Choose pivot.
2) Scan `j` from `lo` to `hi-1`.
3) If `a[j] < pivot`, swap into `store`, `store++`.
4) Swap pivot into `store`.

### Edge cases ✅
- All less than pivot → pivot ends at hi.
- None less than pivot → pivot moves to lo.

### Pitfalls
- Using this 2-way partition on many duplicates can cause unbalanced recursion.

---

### 8B) 3-way partition (regions lt / i / gt)
**Regions**
- `[lo..lt-1] < pivot`
- `[lt..i-1] == pivot`
- `[i..gt] unknown`
- `[gt+1..hi] > pivot`

Step-by-step:
1) pivot = a[lo] (or chosen pivot)
2) Initialize `lt=lo`, `i=lo`, `gt=hi`
3) While `i <= gt`:
   - if `a[i] < pivot`: swap(a[i], a[lt]), lt++, i++
   - else if `a[i] > pivot`: swap(a[i], a[gt]), gt-- (i stays)
   - else: i++

### Edge cases ✅
- All equals pivot → one linear scan, no recursion needed on equals.

### Pitfalls
- Incrementing `i` after swapping with `gt` (skips unknown).

### Tips and tricks
- Memorize: “swap with gt means re-check i.”

---

# Level 9 — Performance traversal discipline ⚙️

## Why
Both algorithms can be correct but slow if you allocate or copy excessively.

## What
- Mergesort: reuse a single temp buffer.
- Quicksort: reduce swaps, pick better pivot, use 3-way for duplicates.

## How (step/flow)
- Measure passes: how many times do you touch each element?
- Mergesort touches elements during merge; quicksort touches them during partition.

## Edge cases ✅
- Nearly sorted arrays: pivot strategy matters.
- Huge arrays: recursion depth / stack usage matters.

## Tips and tricks
- For production sorting, you often combine ideas (introsort etc.), but mastery starts with primitives.

---

# 🧭 Additional subtopics required for true mastery

## 1) Partition scheme variants
- Lomuto vs Hoare vs 3-way
- Know how recursion bounds change per scheme

## 2) Correctness proofs by invariants
- Partition: regions remain correct after each swap
- Merge: output prefix is always sorted

## 3) Recursion to iteration mapping
- Mergesort can be bottom-up iterative
- Quicksort can be implemented with an explicit stack

## 4) Stability and in-place tradeoffs
- Stability matters for multi-key sorts
- In-place vs extra-memory impacts design

---

# ✅ Mastery path (recommended)

## Phase 1: Primitives only (no full sort yet)
1) 🔀 Merge two sorted arrays (trace i/j/k).
2) 🧹 2-way partition (trace store/j).
3) 🇳🇱 3-way partition (trace lt/i/gt).

## Phase 2: Range discipline
4) 🪟 Implement range functions with strict `[lo..hi]` convention; trace boundaries.

## Phase 3: Full algorithms
5) ⚡ Quicksort = partition + recurse on ranges.
6) 🧬 Mergesort = recurse on ranges + merge.

## Phase 4: Stress with edge cases
- All equal
- Already sorted
- Reverse sorted
- Many duplicates
- Tiny arrays (0,1,2 elements)

---

## Next step
Tell me which partition scheme you want as your “default mental model”:
- Lomuto (simpler)
- Hoare (fewer swaps)
- 3-way (best with duplicates)
