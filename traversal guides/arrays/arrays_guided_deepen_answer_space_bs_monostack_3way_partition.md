# 🔍 Guided Deep Dive (No Drills) — Arrays: Answer-Space BS + Monotonic Stacks + 3-Way Partition

> Primary language: **C#** 🟦  
> Python snippets: **only where it adds clarity** 🐍

---

## ✅ Summary list
- 🧭 **Answer-space binary search**: you binary search the *answer*, not the array.
- 📚 **Monotonic stacks**: you traverse once while keeping “unresolved indices” in a stack.
- 🧹 **Three-way partition**: you maintain 3 regions + 1 unknown region, shrinking unknown until done.

---

# 🧭 1) Answer-space binary search (hardest to get right)

## Why
This pattern solves “minimize the maximum” / “maximize the minimum” problems when feasibility is monotonic.

## What
You search a numeric range `[low..high]` for the smallest (or largest) value that satisfies `ok(x)`.

**Key requirement:** `ok(x)` must be monotonic (once it becomes true, it stays true; or vice versa).

## How (step/flow)
### The invariant you must keep
- If you’re searching for **min x such that ok(x) is true**:
  - Maintain: `ans` is the best true seen so far.
  - Move left when `ok(mid)` is true (try smaller).
  - Move right when `ok(mid)` is false (need bigger).

### C# template (lenient)
```csharp
// Returns smallest x in [low..high] with ok(x) == true, else -1.
int MinTrue(int low, int high, Func<int, bool> ok)
{
    if (ok == null) return -1;                 // LENIENT
    if (low > high) return -1;                 // LENIENT

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

## Guided example A: “Split into ≤ D groups, minimize max group sum” 📦

### Problem (small)
- Array: `a = [7, 2, 5, 10, 8]`
- D = 2 groups
- Goal: minimize the **maximum** group sum.

### Why `ok(capacity)` is monotonic
If you can split into ≤ D groups with max-sum ≤ `cap`, then any bigger cap also works.

### Define `ok(cap)` (greedy feasibility)
Rule: scan left-to-right, keep adding until adding next would exceed `cap`, then start a new group.

**C# `ok(cap)` (lenient)**
```csharp
bool CanSplitWithCap(int[] a, int D, int cap)
{
    if (a == null || a.Length == 0) return false; // LENIENT
    if (D <= 0) return false;                     // LENIENT

    int groups = 1;
    long sum = 0;

    for (int i = 0; i < a.Length; i++)
    {
        int x = a[i];
        if (x > cap) return false;               // single item too big => impossible

        if (sum + x <= cap)
        {
            sum += x;
        }
        else
        {
            groups++;
            sum = x;
            if (groups > D) return false;
        }
    }

    return true;
}
```

### Choose the search range
- `low = max(a)` (must fit the largest single item)
- `high = sum(a)` (always feasible as one group)

For our array:
- `low = 10`
- `high = 32`

### Trace the binary search (mid → ok(mid) → move)
```
L=10, R=32
mid=21  ok? yes  -> ans=21, R=20
mid=15  ok? no   -> L=16
mid=18  ok? yes  -> ans=18, R=17
mid=16  ok? no   -> L=17
mid=17  ok? no   -> L=18
stop => ans=18
```

### Show why `ok(18)` is true (greedy split)
Capacity 18:
- group1: 7 + 2 + 5 = 14
- next 10 would make 24 > 18 → new group
- group2: 10 + 8 = 18
Groups used = 2 ≤ D → feasible.

## Common pitfalls
- Using a predicate that is **not** monotonic.
- Wrong boundaries: using `low=0` when the minimum feasible must be ≥ max element.
- Returning `L` blindly (only safe if you wrote a boundary-style loop correctly).

## Tips and tricks
- Always write (and test) `ok(x)` first on 3–5 hand-made `x` values.
- Print `(L, mid, R, ok(mid))` while learning.

## Edge cases ✅
- Empty/null array → return -1 (or 0) by contract; pick and document.
- D >= n → answer becomes `max(a)`.
- Overflow: use `long` for sums.

---

# 📚 2) Monotonic stack variants (NGE/NSE + span)

## Why
You want “next greater/smaller” or “how far until something happens” in **O(n)** instead of O(n²).

## What
A stack stores indices whose answer isn’t known yet.

### Core invariant patterns
- **Next Greater Element (NGE)**: stack keeps indices with **decreasing** values.
- **Next Smaller Element (NSE)**: stack keeps indices with **increasing** values.

## How (step/flow)
General idea:
- Walk `i` left-to-right.
- While current value resolves the stack top, pop and fill answer.
- Push current index.

---

## Guided example A: Next Greater Element (NGE) 📈

Array: `a = [2, 1, 2, 4, 3]`
Goal: `ans[i] = next value to the right that is greater than a[i]`, else -1.

### C# (lenient)
```csharp
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

### Step-by-step trace 🧪
Legend: stack shown as `[idx:value, ... top]`

1) i=0, a[i]=2
- stack empty → push 0
- stack: `[0:2]`

2) i=1, a[i]=1
- 1 > 2? no → push 1
- stack: `[0:2, 1:1]`

3) i=2, a[i]=2
- 2 > 1? yes → pop 1, ans[1]=2
- 2 > 2? no → push 2
- stack: `[0:2, 2:2]`
- ans so far: `[-1, 2, -1, -1, -1]`

4) i=3, a[i]=4
- 4 > 2? yes → pop 2, ans[2]=4
- 4 > 2? yes → pop 0, ans[0]=4
- push 3
- stack: `[3:4]`
- ans so far: `[4, 2, 4, -1, -1]`

5) i=4, a[i]=3
- 3 > 4? no → push 4
- stack: `[3:4, 4:3]`

Done. Unresolved indices keep -1.
Final ans: `[4, 2, 4, -1, -1]`

## Common pitfalls
- Using values instead of indices (you can’t write answers back correctly).
- Getting duplicate behavior wrong: decide whether “greater” means `>` or `>=`.

## Tips and tricks
- Say the invariant: “values on stack are decreasing (for NGE).”

## Edge cases ✅
- Strictly decreasing array → all -1.
- Duplicates → your comparator choice matters.

---

## Guided example B: Next Smaller Element (NSE) 📉

Array: `a = [4, 5, 2, 10, 8]`
Goal: next smaller value to the right.

**Change only the comparison:** pop while `a[i] < a[st.Peek()]`.

### Tip
Keep **one** master function and pass a comparator (or write two functions) to avoid mixing conditions.

---

## Guided example C: Stock span (distance to previous greater) 🧱

Array: prices `p = [100, 80, 60, 70, 60, 75, 85]`
Span[i] = number of consecutive days up to i where price <= p[i].

### Why this is a monotonic stack
You want the previous index with a **greater** value; pop all smaller/equal values.

### C# (lenient)
```csharp
int[] StockSpan(int[] p)
{
    if (p == null || p.Length == 0) return Array.Empty<int>();

    int n = p.Length;
    int[] span = new int[n];
    var st = new Stack<int>(); // indices of decreasing prices

    for (int i = 0; i < n; i++)
    {
        while (st.Count > 0 && p[st.Peek()] <= p[i])
            st.Pop();

        int prevGreaterIdx = (st.Count == 0) ? -1 : st.Peek();
        span[i] = i - prevGreaterIdx;

        st.Push(i);
    }

    return span;
}
```

### Tiny trace highlight
- i=0 (100): stack empty → prevGreater=-1 → span=1, push 0
- i=1 (80): prevGreater=0 → span=1, push 1
- i=3 (70): pop 60, prevGreater=80 → span=2
- i=6 (85): pop 75,60,70,60,80; prevGreater=100 → span=6

Expected spans: `[1, 1, 1, 2, 1, 4, 6]`

---

# 🧹 3) Three-way partition with custom categories

## Why
You often need to reorder items into 3 buckets in one pass (not necessarily sort).

## What
Maintain 3 regions + unknown:
- `[0..low-1]` = group A
- `[low..mid-1]` = group B
- `[mid..high]` = unknown
- `[high+1..n-1]` = group C

## How (step/flow)
Rules depend on your category function `cat(x)` returning A/B/C.

**Mantra:**
- A: swap(mid, low), low++, mid++
- B: mid++
- C: swap(mid, high), high-- (mid stays)

---

## Guided example A: Partition by pivot (<, ==, >) 🎯

Array: `a = [3, 5, 2, 5, 6, 5, 1]`, pivot = 5
Goal order: `<5` then `==5` then `>5`.

### C# (lenient)
```csharp
void ThreeWayPartition(int[] a, int pivot)
{
    if (a == null || a.Length == 0) return;

    int low = 0, mid = 0, high = a.Length - 1;

    while (mid <= high)
    {
        if (a[mid] < pivot)
        {
            (a[low], a[mid]) = (a[mid], a[low]);
            low++; mid++;
        }
        else if (a[mid] == pivot)
        {
            mid++;
        }
        else
        {
            (a[mid], a[high]) = (a[high], a[mid]);
            high--; // mid stays
        }
    }
}
```

### Step-by-step trace (selected steps)
State shown as: `low, mid, high | array`

Start:
- `0,0,6 | [3, 5, 2, 5, 6, 5, 1]`

mid=0 (3 < 5) => swap with low(0): no change, low=1 mid=1
- `1,1,6 | [3, 5, 2, 5, 6, 5, 1]`

mid=1 (5 == 5) => mid=2
- `1,2,6 | [3, 5, 2, 5, 6, 5, 1]`

mid=2 (2 < 5) => swap(mid=2, low=1)
- `2,3,6 | [3, 2, 5, 5, 6, 5, 1]`

mid=4 (6 > 5) => swap(mid=4, high=6)
- `2,4,5 | [3, 2, 5, 5, 1, 5, 6]`  (mid stays to re-check)

mid=4 (1 < 5) => swap(mid=4, low=2)
- `3,5,5 | [3, 2, 1, 5, 5, 5, 6]`

Done (mid > high):
- Result: `[3, 2, 1, 5, 5, 5, 6]`

## Common pitfalls
- Incrementing `mid` after swapping with `high` (you skip checking the new value).

## Tips and tricks
- Draw the 4 regions every time you implement it.
- Use variable names like `lt`, `i`, `gt` if that matches your mental model.

## Edge cases ✅
- All elements < pivot → everything ends in left region.
- All == pivot → just a linear scan.
- Many > pivot → many swaps with `high` (mid often stays).

---

## Guided example B: Custom buckets (negative / zero / positive) ➖0➕

Goal order: negatives, then zeros, then positives.

**Category function:**
- A: x < 0
- B: x == 0
- C: x > 0

This is the same algorithm as above with different comparisons.

### Tip
When you create custom categories, ensure every value maps to exactly one bucket.

---

# ✅ Universal traversal boundary reminder
All index-based examples assume you only read `a[i]` when `0 <= i < a.Length`. [page:1]

---

## Next step (pick one)
Answer with one option and I’ll provide 2–3 additional traced examples (still no drills):
- 🧭 Answer-space BS: write `ok(x)` for a new scenario you care about (capacity/days/partitions) and we’ll verify monotonicity.
- 📚 Monotonic stack: choose NGE vs NSE vs span; we’ll handle duplicates and strictness.
- 🧹 3-way partition: choose your 3 custom categories (e.g., <A, A..B, >B) and we’ll trace.

Also confirm: do you want **Python snippets everywhere** or **only where they add clarity**?
