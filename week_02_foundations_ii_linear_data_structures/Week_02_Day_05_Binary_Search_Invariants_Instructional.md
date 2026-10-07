# 📘 Week 02 Day 05: Binary Search & Invariants — ENGINEERING GUIDE

> 🧭 **Navigation:** [← Previous Day](Week_02_Day_04_Stacks_Queues_Deques_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_02_Day_06_Strings_Numbers_Instructional.md)
> 
> 💡 **Instructor Note:** *Not all sections or topics are mandatory. Feel free to adapt your pace and prioritize high-yield patterns based on your personal interview goals.*

---

## 🎯 LEARNING OBJECTIVES

*By the end of this chapter, you will be able to:*

- 🎯 **Internalize** the invariant-based search framework that eliminates off-by-one errors and infinite loops.
- ⚙️ **Implement** robust implementations for Exact Match, Lower Bound (`>= target`), and Upper Bound (`> target`).
- ⚖️ **Generalize** binary search from sorted collections to monotonic "Answer Spaces" (optimization problems).
- 🛡️ **Prevent** 32-bit signed integer overflow in midpoint calculations (`low + (high - low) / 2`).
- 🏭 **Articulate** production applications (B-Tree page lookups, `git bisect`, branchless binary search) in a 45-minute technical screen.

---

## 📖 CHAPTER 1: CONTEXT & MOTIVATION

### The Problem: Finding a Needle in Massive Data

Consider searching for a target record among 1,000,000,000 sorted database keys:

- **Linear Search (`O(N)`):** Examines keys sequentially. In the worst case, executing 1 billion comparisons requires hundreds of milliseconds of CPU execution.
- **Binary Search (`O(log N)`):** Eliminates half of the remaining search space on every comparison. For 1,000,000,000 elements, it guarantees finding the key or determining absence in **at most 30 comparisons** (`2^30 = 1,073,741,824 > 10^9`).

Binary search delivers a **33,000,000x speedup**. However, binary search is famously difficult to implement without subtle boundary bugs. In fact, Donald Knuth noted that while the first binary search was published in 1946, the first bug-free version was published 16 years later.

> 💡 **Core Invariant:** *Correct binary search is guided by an invariant condition: "If the target exists, it is strictly guaranteed to lie within the active candidate window `[low, high]`." Every iteration must shrink the window while preserving this invariant.*

---

## 🧠 CHAPTER 2: BUILDING THE MENTAL MODEL

### The Dictionary Bisection Analogy

When looking up a word in a 1,000-page dictionary, you do not start at page 1. You open to page 500. If your word starts with "T" and page 500 is "M", you discard pages 1 through 500 in a single physical motion. You bisect the remaining 500 pages into page 750, repeating until located.

---

### 🖼 Visualizing Search Space Elimination

Searching for target `23` in a sorted array:

```text
Initial State: Target = 23, Window = [0 .. 10]
Index:     0    1    2    3    4    5    6    7    8    9   10
Value:  [  2 |  5 |  8 | 12 | 15 | 23 | 30 | 45 | 67 | 89 | 92 ]
           ▲                        ▲                        ▲
          low                      mid                      high
                                 (Val 23)

Step 1: mid = 0 + (10 - 0) / 2 = 5
arr[mid] == 23 -> Target Found at index 5 in exactly 1 step!
```

Searching for target `25` (Non-existent element):

```text
Step 1: low = 0, high = 10 -> mid = 5 (arr[5] = 23)
        23 < 25 -> Target must reside in right sub-array.
        low = mid + 1 = 6. Active Window: [6 .. 10]

Index:     6    7    8    9   10
Value:  [ 30 | 45 | 67 | 89 | 92 ]
           ▲         ▲         ▲
          low       mid       high

Step 2: mid = 6 + (10 - 6) / 2 = 8 (arr[8] = 67)
        67 > 25 -> Target must reside in left sub-array.
        high = mid - 1 = 7. Active Window: [6 .. 7]

Step 3: low = 6, high = 7 -> mid = 6 (arr[6] = 30)
        30 > 25 -> high = mid - 1 = 5.
        low (6) > high (5) -> Candidate space exhausted! Return -1 (Not Found).
```

---

### 🖼 Visualizing Lower Bound vs. Upper Bound with Duplicates

When arrays contain duplicate elements, standard exact-match search returns an arbitrary match. For range queries, we need deterministic boundary finders:

```text
Array with Duplicates: Target = 4
Index:     0    1    2    3    4    5
Value:  [  2 |  4 |  4 |  4 |  7 |  9  ]
                ▲              ▲
                │              └─ Upper Bound (Index 4: first element > 4)
                └─ Lower Bound (Index 1: first element >= 4)
```

- **Lower Bound:** Smallest index `i` such that `arr[i] >= target`.
- **Upper Bound:** Smallest index `i` such that `arr[i] > target`.
- **Count of Target Occurrences:** `UpperBound(target) - LowerBound(target) = 4 - 1 = 3`.

---

### 🖼 Binary Search on Monotonic Answer Space

Binary search applies to any problem that can be modeled as a monotonic predicate function `P(x)`:

```text
Search Space (e.g., Minimum Truck Capacity):
Capacity:      10    11    12    13    14    15    16    17
Can Ship?       F     F     F     F     T     T     T     T
                                        ▲
                               Optimal Minimum Feasible Capacity
```

Once the predicate transitions from `False` to `True`, it **never returns to False**. This monotonic step function allows binary searching across the numerical answer space in `O(log(Max - Min))`.

---

## ⚙️ CHAPTER 3: DUAL-LANGUAGE PRODUCTION IMPLEMENTATIONS

### 1. C# (.NET 8/9): Production Binary Search Suite

```csharp
using System;

namespace LinearStructures.Day05;

public static class BinarySearchSuite
{
    // 1. Classical Exact Match: Returns index or -1
    public static int BinarySearch(int[] arr, int target)
    {
        int low = 0;
        int high = arr.Length - 1;

        while (low <= high)
        {
            // Prevents 32-bit signed integer overflow
            int mid = low + (high - low) / 2;

            if (arr[mid] == target)
            {
                return mid;
            }
            if (arr[mid] < target)
            {
                low = mid + 1; // Discard left half
            }
            else
            {
                high = mid - 1; // Discard right half
            }
        }

        return -1; // Target does not exist
    }

    // 2. Lower Bound: First index where arr[i] >= target
    public static int LowerBound(int[] arr, int target)
    {
        int low = 0;
        int high = arr.Length; // Notice upper limit is Length (can insert at end)

        while (low < high)
        {
            int mid = low + (high - low) / 2;
            if (arr[mid] >= target)
            {
                high = mid; // Candidate found; search left for earlier occurrence
            }
            else
            {
                low = mid + 1;
            }
        }

        return low;
    }

    // 3. Upper Bound: First index where arr[i] > target
    public static int UpperBound(int[] arr, int target)
    {
        int low = 0;
        int high = arr.Length;

        while (low < high)
        {
            int mid = low + (high - low) / 2;
            if (arr[mid] > target)
            {
                high = mid;
            }
            else
            {
                low = mid + 1;
            }
        }

        return low;
    }

    // 4. Binary Search on Answer Space: Capacity to Ship Packages Within D Days (LeetCode 1011)
    public static int ShipWithinDays(int[] weights, int days)
    {
        int low = 0;
        int high = 0;

        foreach (int w in weights)
        {
            low = Math.Max(low, w); // Min capacity must be at least the heaviest single item
            high += w;              // Max capacity is the sum of all items
        }

        while (low < high)
        {
            int midCapacity = low + (high - low) / 2;

            if (CanShip(weights, days, midCapacity))
            {
                high = midCapacity; // Feasible; try smaller capacity
            }
            else
            {
                low = midCapacity + 1; // Infeasible; capacity must be larger
            }
        }

        return low;
    }

    private static bool CanShip(int[] weights, int days, int capacity)
    {
        int daysUsed = 1;
        int currentLoad = 0;

        foreach (int w in weights)
        {
            if (currentLoad + w > capacity)
            {
                daysUsed++;
                currentLoad = 0;
            }
            currentLoad += w;
        }

        return daysUsed <= days;
    }
}
```

---

### 2. Python (3.11+): Idiomatic Binary Search Patterns

```python
from typing import Sequence

class BinarySearchSuite:
    @staticmethod
    def binary_search(arr: Sequence[int], target: int) -> int:
        """Classical exact match returning target index or -1."""
        low: int = 0
        high: int = len(arr) - 1

        while low <= high:
            mid: int = low + (high - low) // 2

            if arr[mid] == target:
                return mid
            elif arr[mid] < target:
                low = mid + 1
            else:
                high = mid - 1

        return -1

    @staticmethod
    def lower_bound(arr: Sequence[int], target: int) -> int:
        """Returns first index where arr[i] >= target."""
        low: int = 0
        high: int = len(arr)

        while low < high:
            mid: int = low + (high - low) // 2
            if arr[mid] >= target:
                high = mid
            else:
                low = mid + 1

        return low

    @staticmethod
    def upper_bound(arr: Sequence[int], target: int) -> int:
        """Returns first index where arr[i] > target."""
        low: int = 0
        high: int = len(arr)

        while low < high:
            mid: int = low + (high - low) // 2
            if arr[mid] > target:
                high = mid
            else:
                low = mid + 1

        return low

    @staticmethod
    def ship_within_days(weights: list[int], days: int) -> int:
        """Binary search on answer space minimizing conveyor belt capacity."""
        low: int = max(weights)
        high: int = sum(weights)

        def can_ship(capacity: int) -> bool:
            days_used: int = 1
            current_load: int = 0
            for w in weights:
                if current_load + w > capacity:
                    days_used += 1
                    current_load = 0
                current_load += w
            return days_used <= days

        while low < high:
            mid_cap: int = low + (high - low) // 2
            if can_ship(mid_cap):
                high = mid_cap  # Feasible, try smaller
            else:
                low = mid_cap + 1  # Infeasible, try larger

        return low

if __name__ == "__main__":
    nums = [2, 4, 4, 4, 7, 9]
    print(f"Exact 7: {BinarySearchSuite.binary_search(nums, 7)}")      # 4
    print(f"Lower 4: {BinarySearchSuite.lower_bound(nums, 4)}")        # 1
    print(f"Upper 4: {BinarySearchSuite.upper_bound(nums, 4)}")        # 4
    print(f"Ship min capacity: {BinarySearchSuite.ship_within_days([1,2,3,4,5,6,7,8,9,10], 5)}")  # 15
```

---

## 🔬 COMPLEXITY DECONSTRUCTION

| Algorithm | Time Complexity | Auxiliary Space | Output Space | Mechanical Justification |
| :--- | :--- | :--- | :--- | :--- |
| **Classical Binary Search** | `O(log N)` | `O(1)` | `O(1)` | Search interval length halves on every step: `N -> N/2 -> N/4 ... -> 1`. Takes at most `ceil(log2(N)) + 1` iterations. |
| **Lower / Upper Bound** | `O(log N)` | `O(1)` | `O(1)` | Preserves half-open invariant `[low, high)` over `N` candidates with zero allocations. |
| **Binary Search on Answer** | `O(N * log(Range))` | `O(1)` | `O(1)` | Takes `log2(Sum - Max)` bisection steps; each feasibility check scans `N` items in `O(N)` time. |

---

## 🎙️ 45-MINUTE INTERVIEW VERBAL SCRIPTS

### Script 1: Integer Overflow in Midpoint Calculation
> *"A notorious bug in binary search is writing `mid = (low + high) / 2`. When `low` and `high` are large 32-bit positive integers—such as in arrays with over `10^9` elements—their sum can exceed `int.MaxValue` (2,147,483,647), overflowing into negative numbers and triggering an `IndexOutOfRangeException`. I always write `mid = low + (high - low) / 2`, which guarantees that all intermediate values remain strictly within valid bounds."*

### Script 2: Defending the Boundary Invariant (`low <= high` vs. `low < high`)
> *"In binary search, the termination condition depends strictly on whether we are searching for an exact match or a boundary. For exact match, the candidate space is inclusive `[low, high]`, so we loop while `low <= high` and update `high = mid - 1` or `low = mid + 1`. For lower bound, we maintain the half-open interval `[low, high)`, where `high` represents the first known index satisfying the predicate. Hence, when `arr[mid] >= target`, we set `high = mid` (keeping `mid` as a candidate), loop while `low < high`, and return `low` when the pointers converge."*

### Script 3: Recognizing Binary Search on Answer Space
> *"When a problem asks to 'minimize the maximum' or 'find the threshold value' and constraints are large, I look for monotonicity in the feasibility function. If a capacity of `C` allows shipping within `D` days, then any capacity greater than `C` will also work. Because the answer space is monotonic (False followed by True), we can binary search over the numerical capacity range `[max(weights), sum(weights)]`. On each step, we verify feasibility in `O(N)` time, reducing overall complexity to `O(N * log(Range))`."*

---

## ⚖️ CHAPTER 4: PRODUCTION TRADEOFFS & SYSTEMS CONTEXT

> [!NOTE]
> **Interview & Systems Context: Database B+ Tree Page Binary Search**  
> In database engines like MySQL InnoDB and PostgreSQL, each table index page contains an array of sorted key offsets. When traversing the tree to satisfy a `SELECT WHERE id = 12345` query, the database engine loads the 16KB page into memory and performs binary search across the slot directory in `O(log K)` time, avoiding linear scans across disk buffers.

> [!NOTE]
> **Interview & Systems Context: Git Bisect for Root-Cause Regression Hunting**  
> Modern DevOps and version control systems utilize `git bisect` to locate the exact commit that introduced a bug out of thousands of changes. By treating commit history as an ordered sequence where commits before the bug are 'good' and commits after are 'bad', engineers find the culprit commit in ~10 builds instead of testing hundreds of commits linearly.

> [!NOTE]
> **Interview & Systems Context: Branch Prediction & Branchless Binary Search**  
> In ultra-low-latency financial trading systems, traditional binary search suffers from high CPU branch misprediction penalties because `arr[mid] < target` is unpredictable (50/50 probability). High-frequency trading engines implement branchless binary search using conditional assembly moves (`cmov`) or Eytzinger array layouts, accelerating lookups by up to 3x on modern superscalar CPUs.

---

## ⚔️ SUPPLEMENTARY OUTCOMES & REVISION

### 🏋️ Practice Problems

| Problem | Difficulty | Key Concept | Target Complexity |
| :--- | :--- | :--- | :--- |
| **Binary Search (Exact Match)** | 🟢 Easy | Midpoint Overflow & Invariants | `O(log N)` Time, `O(1)` Space |
| **Find First and Last Position** | 🟡 Medium | Lower Bound & Upper Bound Pair | `O(log N)` Time, `O(1)` Space |
| **Search in Rotated Sorted Array** | 🟡 Medium | Invariant with Discontinuous Halves | `O(log N)` Time, `O(1)` Space |
| **Capacity to Ship Packages** | 🟡 Medium | Binary Search on Answer Space | `O(N log R)` Time, `O(1)` Space |
| **Koko Eating Bananas** | 🟡 Medium | Monotonic Feasibility Predicate | `O(N log R)` Time, `O(1)` Space |

### 🎙️ Quick Technical Screen Q&A

1. **Q:** *Why is `low + (high - low) / 2` preferred over `(low + high) / 2`?*  
   **A:** To avoid integer overflow when `low + high` exceeds the maximum value of a 32-bit signed integer (`2^31 - 1`).
2. **Q:** *What is the difference between Lower Bound and Upper Bound?*  
   **A:** Lower bound finds the first index where `arr[i] >= target`. Upper bound finds the first index where `arr[i] > target`.
3. **Q:** *What condition must hold to apply binary search to an optimization problem?*  
   **A:** The feasibility predicate must be monotonic: it must evaluate to `False` up to a boundary point, and `True` for all values after (or vice versa).

---

> 🧭 **Navigation:** [← Previous Day](Week_02_Day_04_Stacks_Queues_Deques_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_02_Day_06_Strings_Numbers_Instructional.md)
