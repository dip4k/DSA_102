# 📘 Week 01 Day 06: Peak Finding & Algorithmic Thinking

> 🧭 **Navigation:** [← Previous Day](Week_01_Day_05_Recursion_II_Memoization_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Week Playbook →](WEEK_01_FULL_PLAYBOOK.md)
> 
> 💡 **Instructor Note:** *Peak finding (MIT 6.006) demonstrates that binary search does not require a sorted array—it requires a directional gradient invariant that eliminates half the search space. Zero LaTeX math is used throughout.*

---

## 🎯 LEARNING OBJECTIVES

*By the end of this chapter, you will be able to:*

- 🎯 **Internalize** the core principle of algorithmic thinking: exploiting mathematical problem structure to achieve exponential speedups over brute force.
- ⚙️ **Implement** 1D Peak Finding in `O(log N)` time and 2D Peak Finding in `O(Rows * log Cols)` time using divide-and-conquer.
- ⚖️ **Prove** the invariant that ensures binary search succeeds on unsorted data through directional gradients.
- 🏭 **Connect** peak finding to production telemetry: anomaly detection in streaming metrics and digital signal crest localization.
- 💬 **Deliver** a compelling 45-minute technical interview walkthrough demonstrating algorithmic design mastery.

---

## 📖 CHAPTER 1: CONTEXT & MOTIVATION

### The Engineering Challenge

> [!NOTE]
> **Production & Interview Context:** In distributed system telemetry, real-time anomaly detection engines monitor millions of metric timestamps per second to identify resource spikes. Running a brute-force linear scan across 1,000,000 metrics per window consumes 1,000,000 operations, introducing unacceptable alerting latency. By recognizing that any continuous or discrete elevation sequence possesses at least one local maximum, we can divide and conquer across the search space, reducing operations from 1,000,000 to just ~20 comparisons (`O(log N)`). In technical interviews, this problem tests whether you recognize that binary search applies to gradient spaces, not just sorted arrays.

### The Solution: Divide-and-Conquer on Gradients

A **1D Peak** in an array `A` is any element `A[i]` that is greater than or equal to its immediate neighbors:
- `A[i] >= A[i - 1]` (if `i > 0`)
- `A[i] >= A[i + 1]` (if `i < N - 1`)

Notice the crucial property: **The array does not need to be sorted.**
If `A[mid] < A[mid + 1]`, we know with mathematical certainty that **at least one peak exists in the right half `[mid + 1 .. N - 1]`**.
- Why? Because if the sequence continues climbing, the last element `A[N - 1]` is a peak; if it stops climbing and drops anywhere, the point where it turns downward is a peak!
- Therefore, we can safely discard the entire left half of the array.

> 💡 **Core Insight:** Algorithmic thinking is the art of finding an invariant that allows you to discard large portions of the search space without inspecting them.

---

## 🧠 CHAPTER 2: BUILDING THE MENTAL MODEL

### The Core Analogy: Hiking a Mountain Ridge in Fog

Imagine hiking along a foggy ridge:
- You stand at midpoint `mid` and cannot see the entire mountain range.
- You check the slope beneath your feet: the trail slopes upward to your right (`A[mid] < A[mid + 1]`).
- You do not need to inspect the valley to your left. By simply walking uphill to the right, you are guaranteed to reach a ridge peak, because the trail cannot rise infinitely without eventually plateauing or turning downward.

### 🖼 Visualizing 1D Search-Space Elimination

```text
Array: [ 3,  5,  8, 12, 15, 14, 10 ]
Index:   0   1   2   3   4   5   6

Step 1: low = 0, high = 6 -> mid = 3 (Value: 12)
        Compare mid with right neighbor: A[3] (12) < A[4] (15)
        Trail rises to the right! Discard left half [0..3].
        
        [ .   .   .   . | 15, 14, 10 ]
                          ^       ^
                        low=4   high=6

Step 2: low = 4, high = 6 -> mid = 5 (Value: 14)
        Compare mid with right neighbor: A[5] (14) > A[6] (10)
        Right neighbor is smaller! Peak cannot be strictly to the right. Discard right half [6].
        
        [ .   .   .   . | 15, 14 | . ]
                          ^   ^
                        low=4 high=5

Step 3: low = 4, high = 5 -> mid = 4 (Value: 15)
        Compare mid with right neighbor: A[4] (15) > A[5] (14)
        Right neighbor is smaller! Discard right half [5].
        high = 4.

Terminates: low == high == 4.
Peak found at index 4 (Value = 15). (15 >= 12 and 15 >= 14)
```

---

### Invariants & Formal Proof

1. **Existence Theorem:** In any non-empty array `A` of length `N >= 1`, at least one peak always exists.
   - *Proof:* The global maximum element of the array is by definition greater than or equal to all adjacent elements, so at least one peak is guaranteed.
2. **Directional Invariant:** If `A[mid] < A[mid + 1]`, a peak exists in `A[mid + 1 .. high]`.
   - *Proof:* Follow the path of non-decreasing steps starting at `mid + 1`. Either elements increase until reaching boundary `high` (making `high` a peak), or the ascent stops at some index `k` where `A[k] >= A[k + 1]`, making index `k` a local peak.
3. **Halving Property:** Each step reduces search range `(high - low + 1)` by at least half, yielding `O(log N)` steps.

---

## ⚙️ CHAPTER 3: MECHANICS & IMPLEMENTATION

### 2D Peak Finding: Matrix Column-Halving Strategy

In an `M x N` matrix, a 2D peak is an element `matrix[i][j]` that is `>=` all four neighbors (up, down, left, right).
- **Brute Force:** Scan every cell and check 4 neighbors -> `O(M * N)` time.
- **Divide-and-Conquer:**
  1. Pick the middle column `mid_col = (left_col + right_col) / 2`.
  2. Find the row index `max_row` containing the global maximum of that column (`O(M)` time).
  3. Compare `matrix[max_row][mid_col]` with its left and right column neighbors.
  4. If it is greater than both horizontal neighbors, it is a 2D peak! (It is already greater than its vertical neighbors by being the column max).
  5. If its right neighbor is larger, discard the left half of the matrix (`left_col = mid_col + 1`).
  6. Recurrence: `T(N) = T(N / 2) + O(M)` -> `O(M * log N)` time!

```text
2D MATRIX COLUMN ELIMINATION:
Col:      0    1    2    3    4
Row 0: [ 10,  12,  14,  11,   9 ]
Row 1: [ 15,  17,  25,  18,  12 ]  <- Mid Col = 2
Row 2: [ 11,  13,  19,  14,  10 ]     Max in Col 2 is 25 (Row 1)
Row 3: [  8,   9,  12,   7,   5 ]

Compare 25 with Left neighbor (17) and Right neighbor (18):
25 >= 17 AND 25 >= 18 -> 25 is a 2D PEAK! Found in O(Rows * log Cols)!
```

---

### 💻 Dual-Language Production Implementations

#### Modern C# (.NET 8/9): 1D & 2D Peak Finding Implementations

```csharp
namespace Foundations.Day06;

using System;

public static class PeakFinding
{
    // 1. 1D Peak Finding: O(log N) Time, O(1) Auxiliary Space
    public static int FindPeak1D(int[] nums)
    {
        ArgumentNullException.ThrowIfNull(nums);
        if (nums.Length == 0) throw new ArgumentException("Array cannot be empty");

        int left = 0, right = nums.Length - 1;
        while (left < right)
        {
            int mid = left + (right - left) / 2;
            // Compare middle element with its right neighbor
            if (nums[mid] < nums[mid + 1])
            {
                left = mid + 1; // Peak guaranteed in right half
            }
            else
            {
                right = mid;    // Peak guaranteed in left half (including mid)
            }
        }
        return left; // left == right is a guaranteed local peak
    }

    // 2. 2D Peak Finding: O(Rows * log(Cols)) Time, O(1) Auxiliary Space
    public static (int Row, int Col) FindPeak2D(int[][] matrix)
    {
        ArgumentNullException.ThrowIfNull(matrix);
        int rows = matrix.Length;
        int cols = matrix[0].Length;

        int leftCol = 0, rightCol = cols - 1;

        while (leftCol <= rightCol)
        {
            int midCol = leftCol + (rightCol - leftCol) / 2;

            // Step 1: Find the maximum element in midCol
            int maxRow = 0;
            for (int r = 1; r < rows; r++)
            {
                if (matrix[r][midCol] > matrix[maxRow][midCol])
                {
                    maxRow = r;
                }
            }

            int currentVal = matrix[maxRow][midCol];
            int leftVal = midCol > 0 ? matrix[maxRow][midCol - 1] : int.MinValue;
            int rightVal = midCol < cols - 1 ? matrix[maxRow][midCol + 1] : int.MinValue;

            // Step 2: Check if current element is a 2D peak
            if (currentVal >= leftVal && currentVal >= rightVal)
            {
                return (maxRow, midCol); // 2D Peak found!
            }

            // Step 3: Halve columns toward strictly larger neighbor
            if (rightVal > currentVal)
            {
                leftCol = midCol + 1;
            }
            else
            {
                rightCol = midCol - 1;
            }
        }

        return (-1, -1);
    }

    public static void RunDemo()
    {
        int[] terrain = [1, 3, 20, 4, 1, 0];
        int peakIdx = FindPeak1D(terrain);
        Console.WriteLine($"1D Peak Index: {peakIdx} (Value: {terrain[peakIdx]})"); // Index 2 (20)

        int[][] elevationGrid = [
            [10, 8, 10, 10],
            [14, 13, 12, 11],
            [15, 9, 11, 21],
            [16, 17, 19, 20]
        ];
        var (pRow, pCol) = FindPeak2D(elevationGrid);
        Console.WriteLine($"2D Peak Coordinate: [{pRow}, {pCol}] (Value: {elevationGrid[pRow][pCol]})");
    }
}
```

#### Idiomatic Python (3.11+): 1D & 2D Peak Finding

```python
"""
Week 01 Day 06: Peak Finding & Algorithmic Thinking in Python 3.11+
Demonstrates 1D binary search on gradients and 2D matrix column-halving.
"""

from __future__ import annotations
from typing import List, Tuple


def find_peak_1d(nums: List[int]) -> int:
    """Finds a 1D peak in O(log N) time and O(1) auxiliary space."""
    if not nums:
        raise ValueError("List cannot be empty")

    left, right = 0, len(nums) - 1
    while left < right:
        mid = (left + right) // 2
        if nums[mid] < nums[mid + 1]:
            left = mid + 1
        else:
            right = mid
    return left


def find_peak_2d(matrix: List[List[int]]) -> Tuple[int, int]:
    """Finds a 2D peak in O(M * log N) time and O(1) auxiliary space."""
    rows = len(matrix)
    cols = len(matrix[0])

    left_col, right_col = 0, cols - 1
    while left_col <= right_col:
        mid_col = (left_col + right_col) // 2

        # Find row index with maximum value in mid_col
        max_row = 0
        for r in range(1, rows):
            if matrix[r][mid_col] > matrix[max_row][mid_col]:
                max_row = r

        curr = matrix[max_row][mid_col]
        left_val = matrix[max_row][mid_col - 1] if mid_col > 0 else float("-inf")
        right_val = matrix[max_row][mid_col + 1] if mid_col < cols - 1 else float("-inf")

        if curr >= left_val and curr >= right_val:
            return (max_row, mid_col)

        if right_val > curr:
            left_col = mid_col + 1
        else:
            right_col = mid_col - 1

    return (-1, -1)


def demonstrate_peak_finding() -> None:
    arr = [1, 2, 1, 3, 5, 6, 4]
    peak_idx = find_peak_1d(arr)
    print(f"1D Peak Index: {peak_idx} (Value: {arr[peak_idx]})")  # Index 1 (2) or 5 (6)

    grid = [
        [10, 8, 10, 10],
        [14, 13, 12, 11],
        [15, 9, 11, 21],
        [16, 17, 19, 20],
    ]
    r, c = find_peak_2d(grid)
    print(f"2D Peak Coordinate: ({r}, {c}) (Value: {grid[r][c]})")


if __name__ == "__main__":
    demonstrate_peak_finding()
```

---

## ⚖️ CHAPTER 4: PERFORMANCE, TRADE-OFFS & REAL SYSTEMS

### Beyond Big-O: Why Gradients Enable Logarithmic Speedups

| Approach | Problem Dimension | Time Complexity | Operations (`N=1,000,000`) | Wall-Clock Latency |
| :--- | :--- | :--- | :--- | :--- |
| **Linear Search** | 1D Array | `O(N)` | 1,000,000 ops | ~1.2 ms |
| **Divide-and-Conquer** | 1D Array | `O(log N)` | ~20 ops | ~25 ns (48,000x faster) |
| **Matrix Brute-Force** | 2D Matrix (`1000 x 1000`)| `O(M * N)` | 1,000,000 ops | ~2.5 ms |
| **Matrix Column-Halving**| 2D Matrix (`1000 x 1000`)| `O(M * log N)` | 10,000 ops | ~25 µs (100x faster) |

### 🏭 Real-World Systems Context

> [!NOTE]
> **Real-Time Telemetry Anomaly Detection:** Cloud telemetry collectors ingest metric time series to detect abnormal load spikes. By applying gradient-based divide-and-conquer over sliding metric windows, monitoring agents locate surge inflection points in `O(log N)` comparisons, alerting operators before system collapse.

> [!NOTE]
> **Digital Elevation Models (GIS):** Geospatial satellite sensors generate massive matrices of elevation data. Topological mapping tools locate drainage peaks and ridgeline divides using 2D peak finding, reducing scanning time from hours of full-matrix passes (`O(M * N)`) to sub-second column-halving traversals.

> [!NOTE]
> **Binary Search Invariant Generalization:** Peak finding proves that binary search is not tied to sorted arrays. As long as a binary predicate can prove that an optimal or valid candidate exists exclusively on one side of a midpoint, half the search space can be safely pruned.

> [!NOTE]
> **Audio Waveform Crest Detection:** In digital signal processing (DSP), dynamic range compressors track waveform peaks to prevent digital audio clipping. Waveform crests are located using directional gradient steps, skipping flat sample stretches while bounding peak-detection latency.

---

## 🔗 CHAPTER 5: INTEGRATION & MASTERY

### Connections Across the Curriculum

- **Precursor (Day 2 - Asymptotics):** Master Theorem explains why column-halving recurrence `T(N) = T(N/2) + O(M)` resolves to `O(M * log N)`.
- **Precursor (Day 4 - Recursion):** Peak finding can be formulated recursively, highlighting the elegance of divide-and-conquer base cases.
- **Week 2 (Binary Search):** Direct precursor to advanced binary search patterns (searching rotated arrays, finding minimum in rotated sorted arrays).

### 🧩 Decision Framework: When to Use Peak Finding

```text
                       Does the problem require finding
                       a local (not global) optimum?
                                     |
                      +--------------+--------------+
                      |                             |
                     YES                            NO
                      |                             |
              Can you determine slope         Must scan all
              from local neighbors?           elements (O(N))
                      |                       for global maximum
               +------+------+
               |             |
              YES            NO
               |             |
         Use Gradient     Brute-Force
         Divide & Conquer Linear Scan
         (O(log N))
```

---

## 📊 COMPLEXITY DECONSTRUCTION

| Algorithm | Best-Case Time | Average-Case Time | Worst-Case Time | Auxiliary Space | Output Space |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **1D Linear Scan** | `O(1)` | `O(N)` | `O(N)` | `O(1)` | `O(1)` |
| **1D Binary Search (Iterative)** | `O(1)` | `O(log N)` | `O(log N)` | `O(1)` | `O(1)` |
| **1D Binary Search (Recursive)** | `O(1)` | `O(log N)` | `O(log N)` | `O(log N)` (stack) | `O(1)` |
| **2D Matrix Column Halving** | `O(Rows)` | `O(Rows * log Cols)` | `O(Rows * log Cols)` | `O(1)` | `O(1)` |

---

## 🎙️ 45-MINUTE INTERVIEW VERBAL SCRIPT

### The Architectural Pitch (3-Minute Candidate Monologue)

> *"When presented with the problem of finding a local peak in an unsorted array, the brute force approach is to scan sequentially in `O(N)` time checking each element against its neighbors.*
>
> *However, we can achieve an exponential speedup to `O(log N)` using divide-and-conquer, because a local peak does not require the entire array to be sorted—it only requires a directional gradient invariant.*
>
> *At any midpoint `mid`, we compare `nums[mid]` to its neighbor `nums[mid + 1]`. If the neighbor is strictly greater, the slope is ascending to the right. Because the array is finite, following this ascending slope guarantees that we will eventually reach a local maximum—either at the boundary or where the values stop increasing and drop. Therefore, we can discard the entire left half of the array with mathematical certainty.*
>
> *This reduces the problem size by half at each step, achieving `O(log N)` time complexity and `O(1)` auxiliary space using an iterative two-pointer loop.*
>
> *This technique also generalizes to 2D matrices: by finding the maximum element in the middle column in `O(Rows)` time and checking its horizontal neighbors, we can halve the column search space each iteration, solving 2D peak finding in `O(Rows * log Cols)` time."*

---

## ⚔️ SUPPLEMENTARY OUTCOMES

### 🏋️ Practice Problems

| # | Problem | Difficulty | Target Complexity | Primary Challenge |
| :--- | :--- | :--- | :--- | :--- |
| 1 | Find Peak Element (LeetCode 162) | 🟡 Medium | `O(log N)` / `O(1)` | Standard 1D gradient binary search |
| 2 | Peak Index in a Mountain Array (LeetCode 852) | 🟢 Easy | `O(log N)` / `O(1)` | Monotonic ascent followed by descent |
| 3 | Find a Peak Element II (LeetCode 1901 - 2D) | 🟡 Medium | `O(M * log N)` / `O(1)` | 2D matrix column-halving |
| 4 | Find Minimum in Rotated Sorted Array (LeetCode 153) | 🟡 Medium | `O(log N)` / `O(1)` | Gradient inflection point search |
| 5 | Verify Peak Invariants via Fuzz Testing | 🟠 Hard | `O(N * trials)` | Automated property validation |

### 🎙️ Interview Questions & Model Answers

1. **Q: Why are we guaranteed that a peak exists in the direction of the larger neighbor?**
   - *Answer:* Consider moving in the direction of the strictly larger neighbor. As we advance, values either continue increasing until the array boundary (in which case the boundary element is a peak), or at some point the next element is smaller or equal (in which case the current element is a peak). Because the array has finite length, a peak must exist along that trajectory.
2. **Q: What happens if there are duplicate consecutive elements (plateaus)?**
   - *Answer:* If strict inequality is required (`nums[i] > nums[i+1]`), plateaus can make both sides appear identical, forcing the algorithm into `O(N)` worst-case linear time. If the definition allows non-strict inequality (`nums[i] >= nums[i+1]`), picking either direction still preserves the existence invariant.
3. **Q: In 2D Peak Finding, why must we find the global maximum of the column instead of any local peak in that column?**
   - *Answer:* If we only found a local peak in the column, moving left or right might lead into a row where the vertical neighbors in the adjacent column are higher than the current value, causing an infinite oscillation between columns. Selecting the global maximum guarantees that the element is strictly greater than all vertical neighbors in its column, ensuring progress.

### ❌ Common Misconceptions

- **Myth:** "Binary search can only be applied to sorted arrays."
  - **Reality:** Binary search works on any collection where a predicate or condition allows you to eliminate half the candidate search space with certainty.
- **Myth:** "Peak finding always finds the global maximum of the array."
  - **Reality:** Peak finding guarantees locating *a* local peak. It does not guarantee finding the highest peak in the array.
- **Myth:** "2D Peak Finding can be solved in `O(log(M * N))`."
  - **Reality:** Checking four directions requires `O(Rows * log Cols)` because scanning the column maximum is necessary to ensure vertical dominance.

---

**End of Week 1 Day 6: Peak Finding & Algorithmic Thinking**

> 🧭 **Navigation:** [← Previous Day](Week_01_Day_05_Recursion_II_Memoization_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Week Playbook →](WEEK_01_FULL_PLAYBOOK.md)
