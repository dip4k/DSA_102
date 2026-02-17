# ⚡ Array Traversal Quick Start v5

**Goal:** Start solving array traversal problems in 30 to 60 minutes.

---

## ✅ 1) Choose the Pattern

| If the task says... | Use this pattern |
| --- | --- |
| Scan, count, find | Forward scan |
| Preserve right-side data | Backward scan |
| Two arrays aligned | Lockstep |
| In-place filter | Reader/Writer |
| Sorted pair | Converging pointers |
| Best subarray | Sliding window |
| Many range sums | Prefix sums |
| First >= target | Lower bound |
| Wrap around | Modulo index |

---

## ✅ 2) Core Templates

**Forward scan (C#)**
```csharp
for (int i = 0; i < arr.Length; i++)
{
    // use arr[i]
}
```

**Backward scan (Python)**
```python
for i in range(len(arr) - 1, -1, -1):
    # use arr[i]
```

**Two pointers (C#)**
```csharp
int L = 0, R = arr.Length - 1;
while (L < R)
{
    // move L or R
}
```

**Fixed window (Python)**
```python
sum_ = sum(arr[:k])
for r in range(k, len(arr)):
    sum_ += arr[r] - arr[r - k]
```

**Lower bound (C#)**
```csharp
int LowerBound(int[] a, int target)
{
    int L = 0, R = a.Length;
    while (L < R)
    {
        int mid = L + (R - L) / 2;
        if (a[mid] >= target) R = mid;
        else L = mid + 1;
    }
    return L;
}
```

---

## ✅ 3) Debug Invariants

- Index reads only when `0 <= i < n`.
- Window state matches the window.
- Two pointers always move.
- Binary search interval shrinks each loop.

---

## ✅ 4) First 10 Problems

1. Linear search
2. Find max/min
3. Remove element (in-place)
4. Two sum (sorted)
5. Move zeroes
6. Max sum of k-size window
7. Longest substring without repeat
8. Range sum query (prefix sums)
9. Binary search
10. Jump game

---

## ✅ Next Step

Open [FlowWise_Array_Mastery_v5_Final.md](FlowWise_Array_Mastery_v5_Final.md) and finish Level 1 and Level 2 with templates.
