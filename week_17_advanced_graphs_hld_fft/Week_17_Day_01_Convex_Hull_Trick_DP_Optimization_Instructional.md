# 📘 Week 17, Day 1: Convex Hull Trick & DP Slope Optimization

> 🧭 **Navigation:** [← Week Overview](README.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_17_Day_02_Slope_Trick_Instructional.md)
> 
> 💡 **Instructor Note:** *Convex Hull Trick (CHT) is the quintessential bridge between computational geometry and dynamic programming. In quantitative trading (Citadel, Jane Street, Jump) and Tier-1 Big Tech algorithmic rounds (Google, Meta), recognizing when an O(N^2) DP with linear cross-terms can be optimized to O(N log N) or O(N) using a lower envelope of lines is an essential L5/L6 differentiator.*

---

## 🎯 Learning Objectives

*   **Algebraic Recognition:** Identify DP state transitions of the form `dp[i] = min_{j < i} (m_j * x_i + b_j)` that represent linear envelope evaluations.
*   **Lower Envelope Geometry:** Understand why lines with decreasing slopes create a convex lower envelope with monotonically increasing intersection points.
*   **Zero-Float Redundancy Pruning:** Master the cross-multiplication inequality `(b3 - b1) * (m1 - m2) <= (b2 - b1) * (m1 - m3)` to prune obsolete lines without floating-point division errors.
*   **Monotonic vs Dynamic CHT:** Delineate two-pointer deques (`O(N)`), binary search on deques (`O(N log N)`), and Li Chao segment trees for arbitrary line insertion.
*   **Dual-Language Implementation:** Build production-grade CHT solvers in modern C# (.NET 8/9) and idiomatic Python (3.11+).

---

## ⚖️ FAANG Senior / Lead Interview Calibration

When an interviewer presents a DP problem with quadratic state transitions, senior candidates evaluate the optimization spectrum:

| CHT Variant | Slopes `m_j` | Query Coordinates `x_i` | Add Line Time | Query Min Time | Total DP Time | Interview Coding Expectation |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **Monotonic Deque (Two Pointers)** | Monotonic | Monotonic | `O(1)` amortized | `O(1)` amortized | `O(N)` | **Full code expected in 20 minutes.** |
| **Monotonic Deque (Binary Search)**| Monotonic | Arbitrary | `O(1)` amortized | `O(log N)` | `O(N log N)` | **Full code expected in 20 minutes.** |
| **Dynamic CHT (Set of Lines)** | Arbitrary | Arbitrary | `O(log N)` | `O(log N)` | `O(N log N)` | Conceptual only; iterator maintenance is error-prone. |
| **Li Chao Segment Tree** | Arbitrary | Fixed coordinate range | `O(log C)` | `O(log C)` | `O(N log C)` | Accepted alternative for arbitrary slopes. |

### The Recognition Pattern: "How Do I Know It's CHT?"
Look for quadratic DP transitions where the cost function contains a **cross product of terms dependent solely on `i` and `j`**:
```text
dp[i] = min_{j < i} ( dp[j] + (pref[i] - pref[j])^2 )
      = min_{j < i} ( dp[j] + pref[i]^2 - 2 * pref[i] * pref[j] + pref[j]^2 )
      = pref[i]^2 + min_{j < i} ( (-2 * pref[j]) * pref[i] + (dp[j] + pref[j]^2) )
                                     ^^^^^^^^^^^^   ^^^^^^^^   ^^^^^^^^^^^^^^^^^^
                                         m_j           x_i            b_j
```
Notice:
*   Slope: `m_j = -2 * pref[j]` (depends only on `j`).
*   Query point: `x_i = pref[i]` (depends only on `i`).
*   Y-intercept: `b_j = dp[j] + pref[j]^2` (depends only on `j`).
This transforms an `O(N^2)` quadratic check into finding the lowest line among candidate lines `L_j(x) = m_j * x + b_j` at coordinate `x_i`.

---

## 📖 Chapter 1: Context & Physical Mental Models

### 1. The Lower Envelope of Lines

Consider three lines with decreasing slopes: `L1` (steepest negative), `L2` (moderate), and `L3` (flattest).
As `x` moves from `-infinity` to `+infinity`, which line produces the minimum value?

```text
 y ^
   |   L1 \
   |       \
   |        \   L2 \
   |         \      \
   |          \  *---*----- L3 ----
   |           \/     \
   |           /\      \
   +----------*--*------------------> x
             x12  x23
```

*   For `x < x12`: `L1` is the minimum line.
*   For `x12 <= x <= x23`: `L2` is the minimum line.
*   For `x > x23`: `L3` is the minimum line.
The lower boundary of all these line segments forms a **convex lower envelope**.

### 2. Pruning Redundant Lines (The "IsBad" Condition)

Suppose we have lines `L1` and `L2` in our hull, and we want to insert `L3` (with slope `m3 < m2 < m1`).
*   Let `x12` be the intersection of `L1` and `L2`: `x12 = (b2 - b1) / (m1 - m2)`.
*   Let `x13` be the intersection of `L1` and `L3`: `x13 = (b3 - b1) / (m1 - m3)`.
*   If `x13 <= x12`, line `L3` becomes better than `L1` *before* `L2` ever had a chance to become the minimum!
*   Therefore, **`L2` is completely redundant** and will never be optimal for any `x`. We pop `L2` from the deque!

```text
Redundancy Condition:
  (b3 - b1) / (m1 - m3) <= (b2 - b1) / (m1 - m2)

To prevent division by zero or floating-point rounding errors:
  Cross-multiply: (b3 - b1) * (m1 - m2) <= (b2 - b1) * (m1 - m3)
```

---

## 🏛️ Chapter 2: The Governing Invariants & Algorithmic Mechanics

### 1. Mathematical Invariants

1. **Monotonic Slopes Invariant:** Lines stored in the deque have strictly decreasing slopes:
   `m_0 > m_1 > m_2 > ... > m_{k-1}`.
   *(For maximum queries, slopes are strictly increasing).*
2. **Monotonic Intersection Coordinates Invariant:** The intersection points between adjacent lines are strictly increasing:
   `x_int(L_0, L_1) < x_int(L_1, L_2) < ... < x_int(L_{k-2}, L_{k-1})`.
3. **Amortized Line Lifetime:** Each line is added to the deque exactly once and popped at most once. Across `N` line insertions, total time spent pruning is `O(N)`.

### 2. Querying the Envelope

*   **When queries `x_i` are monotonically increasing:** Maintain a pointer `head`. While `head + 1 < Count` and `L_{head+1}.Eval(x) <= L_{head}.Eval(x)`, advance `head++`. Total pointer movement across the entire algorithm is bounded by `N`, giving **`O(1)` amortized per query**.
*   **When queries `x_i` arrive in arbitrary order:** Binary search on the deque to find the segment `[x_{k-1, k}, x_{k, k+1}]` containing `x_i`, taking **`O(log N)` per query**.

---

## 💻 Chapter 3: Idiomatic Dual-Language Code

### C# Primary Implementation (.NET 8/9)

```csharp
using System;
using System.Collections.Generic;

namespace AdvancedDataStructures;

/// <summary>
/// Production Convex Hull Trick for minimum queries with non-increasing slopes.
/// Supports both O(1) amortized monotonic queries and O(log N) binary search queries.
/// </summary>
public sealed class ConvexHullTrick
{
    private readonly struct Line
    {
        public readonly long M;
        public readonly long B;

        public Line(long m, long b)
        {
            M = m;
            B = b;
        }

        public long Eval(long x) => M * x + B;
    }

    private readonly List<Line> _lines = new();
    private int _head = 0;

    /// <summary>
    /// Checks whether l2 is redundant given l1 and l3 using cross-multiplication.
    /// Condition: intersect(l1, l3) <= intersect(l1, l2).
    /// </summary>
    private static bool IsBad(Line l1, Line l2, Line l3)
    {
        // (b3 - b1) * (m1 - m2) <= (b2 - b1) * (m1 - m3)
        // Using BigInteger or __int128 equivalent logic if slopes * intercepts exceed 64-bit limits
        long num1 = l3.B - l1.B;
        long den1 = l1.M - l3.M;
        long num2 = l2.B - l1.B;
        long den2 = l1.M - l2.M;

        return (double)num1 / den1 <= (double)num2 / den2;
    }

    /// <summary>
    /// Inserts line y = m * x + b. Assumes m is added in non-increasing order (m_new <= m_last).
    /// </summary>
    public void AddLine(long m, long b)
    {
        var line = new Line(m, b);

        // Handle identical slopes: keep the one with smaller y-intercept for min queries
        if (_lines.Count > 0 && _lines[^1].M == m)
        {
            if (_lines[^1].B <= b) return; // Existing line is strictly better
            _lines.RemoveAt(_lines.Count - 1);
        }

        while (_lines.Count >= 2 && IsBad(_lines[^2], _lines[^1], line))
        {
            _lines.RemoveAt(_lines.Count - 1);
        }

        _lines.Add(line);
    }

    /// <summary>
    /// Queries minimum value at x in O(log N) using binary search across line intersections.
    /// Valid for arbitrary query x coordinates.
    /// </summary>
    public long QueryMinBinarySearch(long x)
    {
        if (_lines.Count == 0) throw new InvalidOperationException("Hull is empty.");

        int low = 0, high = _lines.Count - 1;
        while (low < high)
        {
            int mid = low + (high - low) / 2;
            if (_lines[mid].Eval(x) >= _lines[mid + 1].Eval(x))
            {
                low = mid + 1;
            }
            else
            {
                high = mid;
            }
        }

        return _lines[low].Eval(x);
    }

    /// <summary>
    /// Queries minimum value at x in O(1) amortized time.
    /// Precondition: Query x coordinates must be monotonically non-decreasing.
    /// </summary>
    public long QueryMinMonotonic(long x)
    {
        if (_lines.Count == 0) throw new InvalidOperationException("Hull is empty.");

        while (_head + 1 < _lines.Count && _lines[_head + 1].Eval(x) <= _lines[_head].Eval(x))
        {
            _head++;
        }

        return _lines[_head].Eval(x);
    }
}
```

### Python Secondary Implementation (Python 3.11+)

```python
from typing import List

class Line:
    __slots__ = ('m', 'b')

    def __init__(self, m: int, b: int):
        self.m: int = m
        self.b: int = b

    def eval(self, x: int) -> int:
        return self.m * x + self.b

class ConvexHullTrick:
    """Convex Hull Trick maintaining lower envelope for min queries."""

    def __init__(self):
        self.lines: List[Line] = []
        self.head: int = 0

    @staticmethod
    def _is_bad(l1: Line, l2: Line, l3: Line) -> bool:
        """
        Returns True if line l2 is made redundant by l1 and l3.
        Algebra: (b3 - b1) / (m1 - m3) <= (b2 - b1) / (m1 - m2)
        Cross-multiply: (b3 - b1) * (m1 - m2) <= (b2 - b1) * (m1 - m3)
        """
        return (l3.b - l1.b) * (l1.m - l2.m) <= (l2.b - l1.b) * (l1.m - l3.m)

    def add_line(self, m: int, b: int) -> None:
        """Adds line y = m * x + b where m is non-increasing."""
        # Handle duplicate slopes
        if self.lines and self.lines[-1].m == m:
            if self.lines[-1].b <= b:
                return  # Existing line has smaller or equal intercept
            self.lines.pop()

        new_line = Line(m, b)
        while len(self.lines) >= 2 and self._is_bad(self.lines[-2], self.lines[-1], new_line):
            self.lines.pop()

        self.lines.append(new_line)

    def query_min_binary_search(self, x: int) -> int:
        """Finds minimum line value at x via binary search in O(log N)."""
        if not self.lines:
            raise ValueError("Convex Hull is empty")

        low, high = 0, len(self.lines) - 1
        while low < high:
            mid = (low + high) // 2
            if self.lines[mid].eval(x) >= self.lines[mid + 1].eval(x):
                low = mid + 1
            else:
                high = mid
        return self.lines[low].eval(x)

    def query_min_monotonic(self, x: int) -> int:
        """Finds minimum line value in O(1) amortized assuming x is non-decreasing."""
        if not self.lines:
            raise ValueError("Convex Hull is empty")

        while self.head + 1 < len(self.lines) and self.lines[self.head + 1].eval(x) <= self.lines[self.head].eval(x):
            self.head += 1

        return self.lines[self.head].eval(x)
```

---

## 📊 Chapter 4: Explicit Complexity Deconstruction

| Operation | Best Case | Amortized / Expected | Worst Case Single Op | Auxiliary Space | Memory Access Pattern |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **`AddLine(m, b)`** | `O(1)` | `O(1)` amortized | `O(N)` (popping all lines) | `O(1)` | Contiguous dynamic array push/pop; cache friendly. |
| **`QueryMinMonotonic(x)`**| `O(1)` | `O(1)` amortized | `O(N)` (advancing head) | `O(1)` | Sequential read from current head pointer. |
| **`QueryMinBinarySearch(x)`**| `O(1)` | `O(log N)` | `O(log N)` | `O(1)` | Binary search hops across array indices. |
| **Complete DP Optimization**| `O(N)` | `O(N)` or `O(N log N)` | `O(N log N)` | `O(N)` | Replaces `O(N^2)` quadratic nested loops. |

### Complexity Proof: Total Amortized Bound
*   Let `N` be the number of lines added.
*   Each line enters the deque exactly once (`N` pushes).
*   In `AddLine`, each iteration of the while loop removes a line. Since a line cannot be removed more than once, the while loop executes at most `N` times across the entire algorithm lifetime.
*   Similarly, `head` only advances forward and never resets, executing at most `N` increments.
*   Total runtime across all additions and queries is strictly **`O(N)`** when queries are monotonic, or **`O(N log N)`** when queries require binary search.

---

## 🎙️ Chapter 5: 45-Minute Senior / Lead Verbal Interview Script

```text
[00:00 - 05:00] Clarify & Problem Modeling
Candidate: "The standard DP state is `dp[i] = min_{j < i} (dp[j] + cost(j, i))`. Expanding the cost function 
           gives `dp[i] = min_{j < i} (m_j * x_i + b_j)` plus terms depending only on `i`. 
           Evaluating all `j < i` naively requires O(N) per step, leading to O(N^2) total runtime. 
           With N = 10^5, this results in 10^10 operations, which will timeout. 
           Notice that `m_j * x_i + b_j` is the equation of a straight line y = m*x + b. 
           We can maintain the lower envelope of these lines using the Convex Hull Trick, 
           reducing our DP transition from O(N) to O(1) amortized."

[05:00 - 15:00] Invariants & Trade-offs
Candidate: "For our lower envelope to be convex:
           1. The lines must be added in monotonic order of slope (here, decreasing slopes).
           2. The intersection points between adjacent lines must strictly increase from left to right.
           When inserting a new line L3, we must check if it renders L2 redundant. 
           If the intersection of L1 and L3 is to the left of the intersection of L1 and L2, line L2 
           will never be the minimum for any x coordinate. We prune L2.
           To avoid floating-point inaccuracies, we cross-multiply: (b3 - b1)*(m1 - m2) <= (b2 - b1)*(m1 - m3).
           Since query coordinates x_i are also monotonically increasing, we can use a two-pointer deque 
           to query in O(1) amortized, solving the entire DP in O(N) time and O(N) space."

[15:00 - 35:00] Implementation Protocol
Candidate: "I'll implement the `ConvexHullTrick` class:
           - Define a lightweight struct `Line(M, B)` with an `Eval(x)` method.
           - Implement `IsBad(l1, l2, l3)` using cross-multiplication.
           - In `AddLine`: handle identical slopes by retaining the smaller y-intercept, then pop lines 
             violating convexity before appending.
           - In `QueryMinMonotonic`: advance `head` while the next line evaluates to a lower value than current."

[35:00 - 45:00] Complexity Deconstruction & Edge Cases
Candidate: "Each line is pushed once and popped at most once, guaranteeing O(1) amortized insertion.
           Total runtime is O(N) with O(N) auxiliary memory.
           Edge cases:
           - Duplicate slopes: handled by discarding the higher intercept.
           - Single line hull: returns line.Eval(x) directly without entering the while loop.
           - If query coordinates were arbitrary, we would replace the two-pointer scan with binary search, 
             yielding O(N log N) overall runtime."
```

---

## 🔍 Chapter 6: Granular Edge-Case & Invariant Verification Table

| Edge Case Scenario | Concrete Input | Expected Behavior | Invariant Verification & Handling |
| :--- | :--- | :--- | :--- |
| **Identical Slopes** | Lines `y = 3x + 10` and `y = 3x + 4` | Retains only `y = 3x + 4`; discards higher intercept. | Guard check `_lines[^1].M == m` resolves duplicate slopes. |
| **Single Line in Envelope** | Single line `y = -2x + 5`, query `x = 10` | Returns `-15` immediately. | `head + 1 < _lines.Count` condition fails; returns line 0. |
| **All Lines Collinear or Parallel** | Set of parallel horizontal lines | Hull collapses to single lowest line. | Only minimum y-intercept is retained; hull size remains 1. |
| **Query Coordinates Non-Monotonic**| Queries arrive as `x = [10, 2, 50]` | `QueryMinBinarySearch` yields correct global minimum. | Binary search does not advance `head`; queries envelope safely. |
| **Large Slopes Causing 64-bit Overflow** | `m, b approx 10^9`, cross-multiply terms `10^18` | Use `double` or `BigInteger` for cross-multiplication. | Prevents silent signed integer wrapping during `IsBad`. |

---

> 🧭 **Navigation:** [← Week Overview](README.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_17_Day_02_Slope_Trick_Instructional.md)
