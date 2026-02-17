# 🧪 Array Traversal Drills Guide v5

**Purpose:** A detailed guide for how to practice drills effectively, with tips, reminders, and execution steps.

---

## ✅ How to Use This Guide

- Pick a level and complete 3 to 5 drills in one sitting.
- Write the invariant before writing code.
- Solve once from scratch, then rewrite from memory.
- After each drill, write a 2-line postmortem: what went wrong and what you fixed.

---

## 🧭 Drill Workflow (Repeatable)

1. **Identify the pattern** (forward scan, window, pointers, etc.).
2. **State the invariant** in one sentence.
3. **Pick the template** and annotate it with your invariant.
4. **Dry-run** one small example on paper.
5. **Code** with guards and clear boundaries.
6. **Verify** with edge cases: empty, single, tiny window, duplicates.

---

## 🧠 Things to Remember (Global)

- Every read must be in-bounds.
- Every loop must make progress.
- Every window must update state when it grows or shrinks.
- Every binary search must shrink the interval.
- Every partition must shrink the unknown region.

---

## 🟢 Level 1: Physical Movement

**Focus:** One index, no surprises.

**Drills (from v5):**
- Linear search
- Find max/min in one pass
- Count frequency
- Copy array
- Check if array is sorted

**How to do**
- Use a forward scan with a single accumulator.
- Write the invariant: "prefix processed".

**Things to remember**
- Use `i < n`, not `i <= n - 1`.
- Guard neighbor access (`i + 1 < n`).

**Tips and tricks**
- If you can, stop early on failure conditions.
- For max/min, start from the first element after checking empty.

---

## 🔵 Level 2: Index Arithmetic

**Focus:** Index math without bounds mistakes.

**Drills (from v5):**
- Reverse array in-place
- Sum every k-th element
- Rotate by k (modulo)
- Circular queue simulation
- 2D to 1D mapping checks

**How to do**
- For reverse, use two pointers and swap.
- For modulo, normalize indexes with `(i + k) % n`.

**Things to remember**
- `n == 0` must be handled before modulo.
- Always test with `n = 1` and `k > n`.

**Tips and tricks**
- Define helper: `next(i) = (i + 1) % n`.
- For mapping, verify `idx -> (r,c)` with a 2x3 grid.

---

## 🟠 Level 3: Multi-Cursor

**Focus:** Two pointers and compaction.

**Drills (from v5):**
- Remove element (in-place)
- Remove duplicates from sorted array
- Two sum (sorted)
- Merge two sorted arrays
- Partition by parity

**How to do**
- Reader/Writer: reader scans, writer writes valid items.
- Converging: move left or right based on condition.

**Things to remember**
- Reader/Writer invariant: `writer <= reader`.
- Converging invariant: answer must lie within [L, R].

**Tips and tricks**
- For compaction, do not swap unless you want stability lost.
- For sorted arrays, never use two pointers on unsorted data.

---

## 🟣 Level 4: Range/Window

**Focus:** Window state and monotonicity.

**Drills (from v5):**
- Max sum k-window
- Longest substring without repeats
- Minimum size subarray sum
- Count subarrays with sum k
- Longest run of 1s with at most k zeros

**How to do**
- Fixed window: add incoming, remove outgoing.
- Variable window: expand R, shrink L while invalid.

**Things to remember**
- Each move of L or R must update the state.
- Variable windows require monotonicity to be valid.

**Tips and tricks**
- Use a dictionary or frequency array for counts.
- For sums with negatives, use prefix + map instead of sliding window.

---

## 🔴 Level 5: Abstract Traversal

**Focus:** Search space, reachability.

**Drills (from v5):**
- Jump game
- Binary search
- Lower bound
- Answer-space search
- Capacity to ship packages

**How to do**
- Binary search: shrink [L, R] every step.
- Answer-space search: define monotonic `ok(x)` first.

**Things to remember**
- If interval does not shrink, you are stuck.
- Use `mid = L + (R - L) / 2`.

**Tips and tricks**
- Print `(L, mid, R)` while debugging.
- For answer-space, test `ok(x)` on 3 values before search.

---

## 🟤 Level 6: Prefix/Suffix

**Focus:** Carry-forward state.

**Drills (from v5):**
- Prefix sums array
- Range sum with prefix
- Product except self
- Suffix max array
- Split array into equal sum

**How to do**
- One pass left-to-right, one pass right-to-left.

**Things to remember**
- Define whether prefix is inclusive or exclusive and stick to it.

**Tips and tricks**
- When overflow is possible, use a larger type.

---

## 🟧 Level 7: Monotonic Stack

**Focus:** Deferred resolution.

**Drills (from v5):**
- Next greater element
- Daily temperatures
- Next smaller element
- Stock span
- Largest rectangle in histogram

**How to do**
- Keep indices in a monotonic stack.
- Pop while current resolves the top.

**Things to remember**
- Stack holds indices, not values.
- Each index is pushed and popped at most once.

**Tips and tricks**
- State the invariant: stack values are decreasing or increasing.

---

## 🟫 Level 8: Partition

**Focus:** Region boundaries.

**Drills (from v5):**
- Sort colors
- Partition by pivot
- Move zeroes
- Three-way partition by key range
- Stable vs unstable partition

**How to do**
- Track low/mid/high and shrink the unknown region.

**Things to remember**
- Do not increment mid after swapping with high.

**Tips and tricks**
- Draw the four regions before coding.

---

## ⚙️ Level 9: Cache/Throughput

**Focus:** Locality and chunking.

**Drills (from v5):**
- Chunked sum on large arrays
- Batch transformation
- Compare sequential vs random access
- Analyze cache effects

**How to do**
- Process contiguous ranges in fixed chunk size.

**Things to remember**
- Only optimize after correctness.

**Tips and tricks**
- Measure performance before and after changes.

---

## ✅ Quick Self-Check

- I can state the invariant before coding.
- I can trace a 5-element example quickly.
- I can explain why my loop stops.

---

## 🔗 Related Files

- [FlowWise_Array_Mastery_v5_Final.md](FlowWise_Array_Mastery_v5_Final.md)
- [Array_Traversal_Drills_v5.md](Array_Traversal_Drills_v5.md)
- [Array_Traversal_QuickStart_v5.md](Array_Traversal_QuickStart_v5.md)
- [Visual_Playbook_Arrays.md](Visual_Playbook_Arrays.md)
