# 🔍 Deep Dive — Arrays Levels 2–4 (Lenient)

> 🎯 You picked three deepen areas:
> - 🧮 Level 2: wrap-around + circular buffers
> - 👀 Level 3: partition boundaries (2-region → 3-region)
> - 🪟 Level 4: variable sliding window with real invariants (sum bound, distinct count)

---

## ✅ Summary list
- 🔁 Wrap-around is just **index arithmetic**; circular buffers add **state** (head/tail/count).
- 🧱 Partitioning is boundary management; correctness comes from **region invariants**.
- 🪟 Variable windows are “expand with R, shrink with L” driven by a **precisely stated invariant**.

---

# 🧮 Level 2 — Wrap-around + circular buffers

## A) Wrap-around arithmetic 🔁

### Why
Any time you “loop past the end and continue from the start” you need wrap-around. Done wrong, it causes out-of-range reads or infinite loops.

### What
Key formulas (capacity = `n`):
- Next: `i = (i + 1) % n`
- Prev: `i = (i - 1 + n) % n`
- Offset k: `i = (i + k) % n` (normalize if k can be negative)

### How (step/flow)
```
capacity n = 5
idx:  0 1 2 3 4
next: 0→1→2→3→4→0→...
prev: 0←1←2←3←4←0←...
```

### Where and When (use cases)
- Ring buffers, round-robin scheduling, fixed-size “recent history”, streaming pipelines.

### Common pitfalls
- `n == 0` (mod by zero).
- Negative offsets: `%` can be negative in some languages; normalize.

### Tips and tricks
- Treat wrap logic as a tiny function and test with `n = 1..5`.
- Keep a strict rule: “all physical indices are always in `[0..n-1]`.”

### Edge cases ✅
- `n = 1`: every next/prev is 0.
- Large k: reduce by modulo first to avoid overflow.

---

## B) Circular buffer (ring buffer) 🧷

### Why
A circular buffer is the cleanest way to maintain a fixed-size queue without shifting elements.

### What
You store data in an array, plus:
- `head`: where the next dequeue/read happens
- `tail`: where the next enqueue/write happens
- `count`: how many valid items are in the buffer

### How (step/flow)
**Physical layout vs logical order**
```
capacity = 8
array slots:  0 1 2 3 4 5 6 7
             [ ][ ][ ][ ][ ][ ][ ][ ]

head points to logical front
tail points to next write position

Logical traversal order:
(head) -> head+1 -> ... wrap ... -> (tail-1)
```

**Core invariants (memorize) ✅**
- `0 <= head < capacity` and `0 <= tail < capacity` (when capacity > 0)
- `0 <= count <= capacity`
- `tail` is always the next write position
- `head` is always the next read position

### Where and When (use cases)
- Recent N events, fixed-size message queues, streaming sensor data.

### Common pitfalls
- Full vs empty ambiguity if you only track head/tail (so track `count`, or reserve one slot).
- Forgetting to wrap head/tail after increment.

### Tips and tricks
- In learning mode, always log `(head, tail, count)` on each operation.
- Think “head consumes, tail produces.”

### Edge cases ✅
- `capacity <= 0`: treat as “disabled buffer” (lenient no-op behavior).
- Enqueue on full: either overwrite oldest (circular log) *or* reject enqueue—choose one.

---

## C) Lenient circular buffer implementation (C# + Python) 💻

### Why
You’ll master traversal once you implement logical traversal over a physically wrapped array.

### What
We’ll implement lenient behavior:
- `TryEnqueue(x)` returns `false` if full.
- `TryDequeue(out x)` returns `false` if empty.

### How (step/flow)
- Enqueue: write at `tail`, `tail = next(tail)`, `count++`
- Dequeue: read at `head`, `head = next(head)`, `count--`

### Code (C#)
```csharp
public sealed class RingBuffer<T>
{
    private readonly T[] _buf;
    private int _head; // next read
    private int _tail; // next write
    private int _count;

    public int Capacity => _buf.Length;
    public int Count => _count;

    public RingBuffer(int capacity)
    {
        if (capacity < 0) capacity = 0;           // LENIENT
        _buf = new T[capacity];
        _head = 0; _tail = 0; _count = 0;
    }

    public bool TryEnqueue(T item)
    {
        if (Capacity == 0) return false;
        if (_count == Capacity) return false;     // full

        _buf[_tail] = item;
        _tail = (_tail + 1) % Capacity;
        _count++;
        return true;
    }

    public bool TryDequeue(out T item)
    {
        if (Capacity == 0 || _count == 0)
        {
            item = default!;
            return false;
        }

        item = _buf[_head];
        _head = (_head + 1) % Capacity;
        _count--;
        return true;
    }

    // Traverse in logical order (front to back)
    public T[] Snapshot()
    {
        if (Capacity == 0 || _count == 0) return Array.Empty<T>();

        T[] result = new T[_count];
        for (int k = 0; k < _count; k++)
        {
            int idx = (_head + k) % Capacity;
            result[k] = _buf[idx];
        }
        return result;
    }
}
```

### Code (Python)
```python
class RingBuffer:
    def __init__(self, capacity):
        self.cap = max(0, int(capacity))
        self.buf = [None] * self.cap
        self.head = 0
        self.tail = 0
        self.count = 0

    def try_enqueue(self, x):
        if self.cap == 0 or self.count == self.cap:
            return False
        self.buf[self.tail] = x
        self.tail = (self.tail + 1) % self.cap
        self.count += 1
        return True

    def try_dequeue(self):
        if self.cap == 0 or self.count == 0:
            return (False, None)
        x = self.buf[self.head]
        self.head = (self.head + 1) % self.cap
        self.count -= 1
        return (True, x)

    def snapshot(self):
        return [self.buf[(self.head + k) % self.cap] for k in range(self.count)] if self.count else []
```

---

## ✅ Debug checklist for circular buffers
- Invariants: `0 <= count <= capacity`, head/tail always in range.
- Print/log: `head`, `tail`, `count`, and the snapshot indices.
- Typical bug symptoms:
  - “Missing element” → wrong index when snapshotting (`head + k`).
  - “Duplicates” → not advancing head/tail.
  - “Out of range” → modulo missing.

---

# 👀 Level 3 — Partition boundaries (2-region → 3-region)

## A) Two-region partition 🧱

### Why
Partitioning is the core of in-place filtering and quicksort-like logic.

### What
Maintain boundary `L` meaning:
- `[0..L-1]` are “good” (e.g., `< pivot`)
- `[L..i-1]` are “processed but not good”
- `[i..end]` are unknown

### How (step/flow)
```
| good (<pivot) | other (>=pivot) | unknown... |
 0            L              i

If a[i] is good => swap into L, then L++
Always i++
```

### Where and When (use cases)
- Move all zeros to end, remove value in-place, quick partition.

### Common pitfalls
- Assuming stability (most in-place swaps are not stable).

### Tips and tricks
- Name your boundary by meaning: `nextGood` is often clearer than `L`.

### Edge cases ✅
- All good → boundary ends at n.
- None good → boundary stays at 0.

### Code (C#) — < pivot
```csharp
// LENIENT: null => no-op
void PartitionLessThan(int[] a, int pivot)
{
    if (a == null || a.Length == 0) return;

    int nextGood = 0;
    for (int i = 0; i < a.Length; i++)
    {
        if (a[i] < pivot)
        {
            (a[nextGood], a[i]) = (a[i], a[nextGood]);
            nextGood++;
        }
    }
}
```

---

## B) Three-region partition (Dutch National Flag) 🇳🇱

### Why
For 3 categories, you can finish in one pass by shrinking the unknown region.

### What
Maintain `low`, `mid`, `high`:
- `[0..low-1]` = small
- `[low..mid-1]` = middle
- `[mid..high]` = unknown
- `[high+1..n-1]` = large

### How (step/flow)
```
| small | middle | unknown........ | large |
 0     low      mid            high     n-1

Rules:
- small  -> swap(mid, low), low++, mid++
- middle -> mid++
- large  -> swap(mid, high), high--   (mid stays!)
```

### Where and When (use cases)
- Sort 0/1/2, 3-way partition around pivot, category bucketing.

### Common pitfalls
- Incrementing `mid` after swapping with `high` (you might skip an unknown).

### Tips and tricks
- Mantra: “swap with high means re-check mid.”
- Treat this as boundary traversal, not “sorting.”

### Edge cases ✅
- Already partitioned arrays.
- Many duplicates.
- Values outside categories: decide a policy (skip, treat as middle, collect separately).

### Code (C#) — values 0/1/2
```csharp
// LENIENT: null => no-op
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
            high--; // mid stays
        }
        else
        {
            mid++; // LENIENT skip
        }
    }
}
```

---

## ✅ Debug checklist for partitions
- Two-region log: `i`, `nextGood`, plus array snapshot for tiny arrays.
- Three-region log: `low`, `mid`, `high` every iteration.
- Typical bug symptoms:
  - Infinite loop → one pointer not moving.
  - Skipped items → wrong `mid` update after swap-with-high.

---

# 🪟 Level 4 — Variable sliding window (real invariants)

## The mental model 🧠

### Why
Variable windows are common because they avoid O(n²) subarray enumeration.

### What
Two pointers define the current window `[L..R]`.
- Expand by moving `R`.
- Shrink by moving `L` until the invariant holds.

### How (step/flow)
```
for R = 0..n-1:
  include a[R]
  while window invalid:
    exclude a[L]
    L++
  update answer using valid window
```

### Common pitfalls
- Using the wrong invariant for your data (e.g., sum windows with negative numbers).

### Tips and tricks
- Write the invariant as a sentence and put it right above the `while`.

---

## A) Invariant 1: Sum bound (classic) ➕

### Why
Find shortest/longest subarray where sum crosses a threshold.

### What
Common version: **minimum length subarray with sum >= S**.

### How (step/flow)
- Expand `R`, add `a[R]` to sum.
- While `sum >= S`, shrink from left and update best.

### Big warning ⚠️
This specific shrinking logic is correct when **all numbers are non-negative**.

### Edge cases ✅
- `a == null` or empty → return 0.
- `S <= 0` → return 1 (any single element works) or 0 by your contract; pick one.

### Code (C#) — min length with sum >= S (non-negative arrays)
```csharp
// LENIENT:
// - null/empty => 0
// - S <= 0 => 1 (if array non-empty)
int MinLenSumAtLeast(int[] a, int S)
{
    if (a == null || a.Length == 0) return 0;
    if (S <= 0) return 1;

    int best = int.MaxValue;
    int L = 0;
    long sum = 0;

    for (int R = 0; R < a.Length; R++)
    {
        sum += a[R];

        while (L <= R && sum >= S)
        {
            best = Math.Min(best, R - L + 1);
            sum -= a[L];
            L++;
        }
    }

    return (best == int.MaxValue) ? 0 : best;
}
```

### Tips and tricks
- If negatives exist, this approach breaks; you’ll need a different technique (often prefix sums + deque).

---

## B) Invariant 2: At most K distinct elements 🧩

### Why
This works for general integers (including negatives) because distinct-count is controlled by a frequency map.

### What
Maintain a frequency dictionary for window `[L..R]` such that:
- **Invariant:** `distinctCount <= K`

### How (step/flow)
- Expand with `R`, increment freq.
- While `distinctCount > K`, decrement freq at `L` and move `L`.
- Update best length.

### Edge cases ✅
- `K <= 0` → return 0.
- `a == null` → return 0.

### Code (C#) — longest subarray with at most K distinct
```csharp
// LENIENT:
// - null/empty => 0
// - K <= 0 => 0
int LongestAtMostKDistinct(int[] a, int K)
{
    if (a == null || a.Length == 0) return 0;
    if (K <= 0) return 0;

    var freq = new Dictionary<int, int>();
    int distinct = 0;
    int best = 0;

    int L = 0;
    for (int R = 0; R < a.Length; R++)
    {
        int x = a[R];
        if (!freq.TryGetValue(x, out int c) || c == 0) distinct++;
        freq[x] = c + 1;

        while (distinct > K)
        {
            int y = a[L];
            freq[y]--;
            if (freq[y] == 0) distinct--;
            L++;
        }

        int len = R - L + 1;
        if (len > best) best = len;
    }

    return best;
}
```

### Tips and tricks
- Only update `distinct` when a key transitions 0→1 or 1→0.
- Log `L`, `R`, `distinct` while learning.

---

## ✅ Debug checklist for variable windows
Log:
- `L`, `R`, current window length
- Sum windows: `sum`
- Distinct windows: `distinct` and a small snapshot of `freq` for tiny arrays

Typical bug symptoms:
- Best not updating → you’re updating before validity.
- Infinite shrink loop → not changing state when moving `L`.

---

## 🧭 Decision guide (which invariant to choose?)
- ➕ **Sum >= S (min length)**: use when array values are **non-negative**.
- 🧩 **At most K distinct**: works on any ints; uses map state.
- If your property doesn’t become “more valid” when you shrink, sliding window may not apply.

---

## Next step
Pick one for an even deeper walkthrough with 2–3 traced examples (index-by-index):
- 🔁 Circular buffer: overwrite-oldest variant (always enqueue)
- 🇳🇱 Three-way partition: generic `< pivot`, `== pivot`, `> pivot`
- 🪟 Variable window: “exactly K distinct” built from “at most K”
