# 📖 WEEK 10 DAY 04: DYNAMIC PROGRAMMING ON SEQUENCES — ENGINEERING GUIDE

> 🧭 **Navigation:** [← Previous Day](Week_10_Day_03_2D_DP_Grids_Edit_Distance_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_10_Day_05_Story_Driven_DP_Advanced_Instructional.md)
> 
> 💡 **Instructor Note:** *Sequence DP transitions our state definition from coordinates to monotonic chains and contiguous subarrays. Today's centerpiece is mastering Longest Increasing Subsequence (LIS)—progressing from the standard O(N^2) formulation to the optimal O(N log N) Patience Sorting binary search—alongside Kadane's algorithm and Weighted Interval Scheduling.*

---

## 🎯 LEARNING OBJECTIVES

*By the end of this chapter, you will be able to:*
- 🎯 **Formulate** sequence optimization problems using the **3-Step DP Recipe** (`Choice -> State Definition -> Base Cases & Transitions`).
- 📈 **Master** Longest Increasing Subsequence (LIS) in both `O(N^2)` DP and `O(N log N)` binary search (Patience Sorting).
- ⚡ **Implement** Kadane's Algorithm for Maximum Subarray Sum with `O(N)` time, `O(1)` space, and boundary index tracking.
- ⏱️ **Solve** Weighted Interval Scheduling using memoized DP combined with binary search (`p(i)`).
- 🎙️ **Deliver** a polished 45-minute verbal interview script explaining why the greedy replacement in Patience Sorting preserves optimal subsequence length.

---

## 📖 CHAPTER 1: CONTEXT & MOTIVATION

### The Monotonic Subsequence Challenge

In sequential data analysis, values arrive in ordered streams: financial ticks, sensor readings, system timestamps, or user activity logs. A central task is identifying monotonic patterns embedded inside noisy data:
- What is the longest upward trend in a volatile price feed?
- What is the maximum profit achievable from overlapping compute tasks with discrete start times, deadlines, and weights?
- What contiguous window in a time series produces the highest aggregate revenue?

A brute-force evaluation of subsequences requires checking all `2^N` subsets of the sequence. For `N = 100`, `2^100` (`~1.26 * 10^30`) calculations cannot be solved within our universe's lifetime. Dynamic programming reduces this to `O(N^2)`, and with binary search, to `O(N log N)`.

> [!NOTE]
> **Enterprise Event Processing & Scheduling:** High-frequency trading engines monitor monotonic order flows to detect algorithmic arbitrage, while OS task dispatchers and cloud workflow orchestrators (e.g., Kubernetes CronJobs and Apache Airflow) execute Weighted Interval Scheduling to pack high-priority, mutually conflicting jobs into execution queues without resource collisions.

---

## 🧠 CHAPTER 2: THE 3-STEP RECIPE FOR SEQUENCE DP

```text
+-----------------------------------------------------------------------------------+
|                        3-STEP RECIPE: SEQUENCE DP PATTERNS                        |
+-----------------------------------------------------------------------------------+
|  1. CHOICE            LIS: For each previous element j < i:                       |
|                       Can arr[i] extend the increasing chain ending at arr[j]?     |
|                       Kadane: Extend the running subarray OR start fresh at i?    |
|                       Intervals: Exclude interval i OR include i + latest         |
|                       compatible interval p(i)?                                   |
|                                                                                   |
|  2. STATE             LIS:     dp[i] = Length of LIS strictly ending at index i.  |
|                       Kadane:  dp[i] = Maximum subarray sum ending at index i.    |
|                       Sched:   dp[i] = Max weight using a subset of tasks 0..i.   |
|                                                                                   |
|  3. TRANSITIONS &     LIS:     dp[i] = 1 + max({dp[j] : j < i, arr[j] < arr[i]}) |
|     BASE CASES        Kadane:  dp[i] = max(nums[i], dp[i-1] + nums[i])            |
|                       Sched:   dp[i] = max(dp[i-1], weight[i] + dp[p(i)])         |
|                       Base:    LIS: dp[i] = 1; Kadane: dp[0] = nums[0].           |
+-----------------------------------------------------------------------------------+
```

### Visualizing LIS: O(N^2) Table vs O(N log N) Patience Sorting

```text
===================================================================================
1. O(N^2) DP TABLE: LOOK BACK ACROSS ALL PRIOR J < I
===================================================================================
Array:      [  10,   9,   2,   5,   3,   7,  101,  18  ]
Index:          0    1    2    3    4    5    6    7

dp array:   [   1,   1,   1,   2,   2,   3,   4,   4  ]
                             ^         ^
                             |         |-- dp[5] = 1 + max(dp[2], dp[4]) = 1 + 2 = 3
                             |-- dp[3] = 1 + dp[2] = 2

===================================================================================
2. O(N log N) PATIENCE SORTING (TAILS ARRAY)
===================================================================================
Each element replaces the first tail >= num (binary search), or appends if > all.

Insert 10:   Pile 0: [10]
Insert 9:    Pile 0: [ 9]  (replaced 10)
Insert 2:    Pile 0: [ 2]  (replaced 9)
Insert 5:    Pile 0: [ 2]  | Pile 1: [ 5]
Insert 3:    Pile 0: [ 2]  | Pile 1: [ 3]  (replaced 5)
Insert 7:    Pile 0: [ 2]  | Pile 1: [ 3]  | Pile 2: [ 7]
Insert 101:  Pile 0: [ 2]  | Pile 1: [ 3]  | Pile 2: [ 7]  | Pile 3: [101]
Insert 18:   Pile 0: [ 2]  | Pile 1: [ 3]  | Pile 2: [ 7]  | Pile 3: [ 18] (replaced 101)

Final tails array: [ 2, 3, 7, 18 ]
Length of LIS = tails.Length = 4 (Subsequence: [2, 3, 7, 18])
```

---

## ⚙️ CHAPTER 3: MECHANICS & IMPLEMENTATION

### Implementation 1: Longest Increasing Subsequence (LIS - LeetCode 300)

#### Modern C# (.NET 8/9) Implementation

```csharp
namespace DP.Sequences;

public static class LongestIncreasingSubsequence
{
    // -------------------------------------------------------------
    // Approach 1: Classic O(N^2) DP with Full Subsequence Reconstruction
    // Time: O(N^2) | Space: O(N)
    // -------------------------------------------------------------
    public static (int Length, List<int> Sequence) SolveN2WithReconstruction(ReadOnlySpan<int> nums)
    {
        if (nums.IsEmpty) return (0, []);

        int n = nums.Length;
        int[] dp = new int[n];
        int[] parent = new int[n];
        Array.Fill(dp, 1);
        Array.Fill(parent, -1);

        int maxLength = 1;
        int bestEndIndex = 0;

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < i; j++)
            {
                if (nums[j] < nums[i] && dp[j] + 1 > dp[i])
                {
                    dp[i] = dp[j] + 1;
                    parent[i] = j;
                }
            }

            if (dp[i] > maxLength)
            {
                maxLength = dp[i];
                bestEndIndex = i;
            }
        }

        // Backtrack optimal subsequence
        List<int> sequence = [];
        int curr = bestEndIndex;
        while (curr != -1)
        {
            sequence.Add(nums[curr]);
            curr = parent[curr];
        }

        sequence.Reverse();
        return (maxLength, sequence);
    }

    // -------------------------------------------------------------
    // Approach 2: Optimal O(N log N) Patience Sorting with Binary Search
    // Time: O(N log N) | Space: O(N)
    // -------------------------------------------------------------
    public static int SolveNLogN(ReadOnlySpan<int> nums)
    {
        if (nums.IsEmpty) return 0;

        List<int> tails = new(nums.Length);

        foreach (int x in nums)
        {
            int idx = BinarySearchFirstGreaterOrEqual(tails, x);
            if (idx == tails.Count)
            {
                tails.Add(x);
            }
            else
            {
                tails[idx] = x; // Greedily tighten the smallest tail of length idx + 1
            }
        }

        return tails.Count;
    }

    private static int BinarySearchFirstGreaterOrEqual(List<int> tails, int target)
    {
        int left = 0, right = tails.Count;
        while (left < right)
        {
            int mid = left + (right - left) / 2;
            if (tails[mid] >= target)
                right = mid;
            else
                left = mid + 1;
        }
        return left;
    }
}
```

#### Idiomatic Python (3.11+) Implementation

```python
from bisect import bisect_left

class LongestIncreasingSubsequence:
    """Longest Increasing Subsequence: O(N^2) with reconstruction and O(N log N) binary search."""

    @staticmethod
    def solve_n2_with_reconstruction(nums: list[int]) -> tuple[int, list[int]]:
        if not nums:
            return 0, []

        n = len(nums)
        dp = [1] * n
        parent = [-1] * n

        max_len = 1
        best_end = 0

        for i in range(n):
            for j in range(i):
                if nums[j] < nums[i] and dp[j] + 1 > dp[i]:
                    dp[i] = dp[j] + 1
                    parent[i] = j

            if dp[i] > max_len:
                max_len = dp[i]
                best_end = i

        # Reconstruct path
        seq: list[int] = []
        curr = best_end
        while curr != -1:
            seq.append(nums[curr])
            curr = parent[curr]

        seq.reverse()
        return max_len, seq

    @staticmethod
    def solve_n_log_n(nums: list[int]) -> int:
        """Patience sorting using bisect_left in O(N log N) time."""
        tails: list[int] = []

        for x in nums:
            idx = bisect_left(tails, x)
            if idx == len(tails):
                tails.append(x)
            else:
                tails[idx] = x

        return len(tails)
```

---

### Implementation 2: Kadane's Algorithm — Maximum Subarray Sum (LeetCode 53)

#### Modern C# (.NET 8/9) Implementation

```csharp
namespace DP.Sequences;

public static class Kadane
{
    // -------------------------------------------------------------
    // Maximum Subarray Sum with O(1) Space & Index Tracking
    // Time: O(N) | Space: O(1)
    // -------------------------------------------------------------
    public static (int MaxSum, int StartIndex, int EndIndex) MaxSubarray(ReadOnlySpan<int> nums)
    {
        if (nums.IsEmpty) return (0, -1, -1);

        int currentSum = nums[0];
        int maxSum = nums[0];

        int bestStart = 0;
        int bestEnd = 0;
        int tempStart = 0;

        for (int i = 1; i < nums.Length; i++)
        {
            if (nums[i] > currentSum + nums[i])
            {
                currentSum = nums[i]; // Start new subarray
                tempStart = i;
            }
            else
            {
                currentSum += nums[i]; // Extend existing subarray
            }

            if (currentSum > maxSum)
            {
                maxSum = currentSum;
                bestStart = tempStart;
                bestEnd = i;
            }
        }

        return (maxSum, bestStart, bestEnd);
    }
}
```

#### Idiomatic Python (3.11+) Implementation

```python
class Kadane:
    """Kadane's algorithm for Maximum Subarray Sum with index tracking."""

    @staticmethod
    def max_subarray(nums: list[int]) -> tuple[int, int, int]:
        if not nums:
            return 0, -1, -1

        current_sum = nums[0]
        max_sum = nums[0]
        best_start = best_end = temp_start = 0

        for i in range(1, len(nums)):
            if nums[i] > current_sum + nums[i]:
                current_sum = nums[i]
                temp_start = i
            else:
                current_sum += nums[i]

            if current_sum > max_sum:
                max_sum = current_sum
                best_start = temp_start
                best_end = i

        return max_sum, best_start, best_end
```

---

### Implementation 3: Weighted Interval Scheduling (LeetCode 1235 Variant)

#### Modern C# (.NET 8/9) Implementation

```csharp
namespace DP.Sequences;

public static class WeightedIntervalScheduling
{
    public readonly record struct Interval(int Start, int End, int Weight);

    public static int MaxWeight(Interval[] intervals)
    {
        if (intervals == null || intervals.Length == 0) return 0;

        // Sort intervals by finish time
        var sorted = intervals.OrderBy(x => x.End).ToArray();
        int n = sorted.Length;
        int[] dp = new int[n];
        dp[0] = sorted[0].Weight;

        for (int i = 1; i < n; i++)
        {
            int excludeCurrent = dp[i - 1];
            int includeCurrent = sorted[i].Weight;

            int p = FindLatestNonConflicting(sorted, i);
            if (p != -1)
            {
                includeCurrent += dp[p];
            }

            dp[i] = Math.Max(excludeCurrent, includeCurrent);
        }

        return dp[n - 1];
    }

    private static int FindLatestNonConflicting(Interval[] sorted, int i)
    {
        int left = 0, right = i - 1;
        int result = -1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            if (sorted[mid].End <= sorted[i].Start)
            {
                result = mid;
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }

        return result;
    }
}
```

#### Idiomatic Python (3.11+) Implementation

```python
from bisect import bisect_right
from dataclasses import dataclass

@dataclass(frozen=True, slots=True)
class Interval:
    start: int
    end: int
    weight: int

class WeightedIntervalScheduling:
    @staticmethod
    def max_weight(intervals: list[Interval]) -> int:
        if not intervals:
            return 0

        # Sort by end time
        sorted_intervals = sorted(intervals, key=lambda x: x.end)
        n = len(sorted_intervals)
        end_times = [inv.end for inv in sorted_intervals]

        dp = [0] * n
        dp[0] = sorted_intervals[0].weight

        for i in range(1, n):
            exclude_curr = dp[i - 1]
            include_curr = sorted_intervals[i].weight

            # Binary search for latest interval ending <= sorted_intervals[i].start
            p = bisect_right(end_times, sorted_intervals[i].start) - 1
            if p >= 0:
                include_curr += dp[p]

            dp[i] = max(exclude_curr, include_curr)

        return dp[n - 1]
```

---

## ⚖️ CHAPTER 4: EXPLICIT COMPLEXITY DECONSTRUCTION

### Sequence Algorithms Complexity Breakdown

| Algorithm | Time Complexity | Auxiliary Space | Bottleneck Phase | Optimization Pivot |
| :--- | :--- | :--- | :--- | :--- |
| **LIS Standard DP** | `O(N^2)` | `O(N)` | Inner loop `j < i` scan | Checks all prior states |
| **LIS Patience Sorting** | `O(N log N)` | `O(N)` | `bisect_left` per item | Tails array is strictly sorted |
| **Kadane's Algorithm** | `O(N)` | `O(1)` | Single loop pass | Resets running sum if `< 0` |
| **Weighted Interval Scheduling** | `O(N log N)` | `O(N)` | Sorting + binary search `p(i)` | Binary search on end times |

---

## 🎙️ CHAPTER 5: 45-MINUTE INTERVIEW VERBAL SCRIPT

```text
===================================================================================
             45-MINUTE INTERVIEW PLAYBOOK: LIS O(N^2) TO O(N LOG N)
===================================================================================

[00:00 - 05:00] CLARIFICATION & CONSTRAINTS
"Let's confirm the problem details. Are we looking for strictly increasing or non-decreasing? 
Strictly increasing means nums[j] < nums[i]; non-decreasing allows equality. 
What is the size of the array? If N is up to 2,500, an O(N^2) solution executes within 
~3 * 10^6 operations. However, if N is 10^5, an O(N^2) algorithm triggers a Time Limit 
Exceeded error (~10^10 operations). We must aim for O(N log N)."

[05:00 - 15:00] THE 3-STEP RECIPE & O(N^2) FORMULATION
"Let's start with the standard DP approach:
 1. Choice: For element nums[i], we inspect all previous indices j < i. If nums[j] < nums[i], 
    nums[i] can append to the subsequence ending at j.
 2. State: Let dp[i] be the length of the longest increasing subsequence ending at index i.
 3. Transitions & Base Cases:
    dp[i] = 1 + max({dp[j] : j < i and nums[j] < nums[i]}).
    Base case: dp[i] = 1 for all i (the single element itself).
    The answer is max(dp). Time: O(N^2), Space: O(N)."

[15:00 - 30:00] THE OPTIMIZATION BREAKTHROUGH: PATIENCE SORTING O(N LOG N)
"How can we do better? Notice that to extend an increasing subsequence of length L, we only 
care about the smallest possible ending element (tail). A smaller tail gives more room for 
subsequent elements to be larger.
Let tails[k] store the minimum tail value of all increasing subsequences of length k + 1.
Two crucial properties:
 1. The tails array is strictly sorted. Proof: A tail of length k + 1 must be strictly 
    larger than a tail of length k.
 2. For each number x, we can binary search tails in O(log N) to find the first tail >= x:
    - If x is larger than all tails, x extends the LIS length: append x to tails.
    - Otherwise, overwrite tails[idx] = x. This greedily decreases the tail value for that length."

[30:00 - 40:00] IMPLEMENTATION & CODING
"I'll write the O(N log N) solution using bisect_left / custom binary search. The size of the 
tails list at the end represents the length of the LIS."

[40:00 - 45:00] DRY RUN & COMPLEXITY WRAP-UP
"Tracing [10, 9, 2, 5, 3, 7, 101, 18]:
 tails evolves: [10] -> [9] -> [2] -> [2, 5] -> [2, 3] -> [2, 3, 7] -> [2, 3, 7, 101] -> [2, 3, 7, 18].
 Length = 4. Time Complexity: O(N log N), Auxiliary Space: O(N). Completed!"
===================================================================================
```

---

## ⚔️ PRACTICE PROBLEMS & INTERVIEW DRILLS

| # | Problem | Difficulty | Key Pattern | Focus Skill |
| :- | :--- | :-: | :--- | :--- |
| 1 | **LeetCode 300: Longest Increasing Subsequence** | 🟡 Medium | Monotonic Chains | `O(N log N)` binary search |
| 2 | **LeetCode 53: Maximum Subarray** | 🟡 Medium | Contiguous Kadane | `O(1)` space running max |
| 3 | **LeetCode 673: Number of Longest Increasing Subsequence** | 🟡 Medium | LIS with Count | Maintain `lengths` & `counts` |
| 4 | **LeetCode 354: Russian Doll Envelopes** | 🔴 Hard | 2D Sorting + 1D LIS | Sort width asc, height desc |
| 5 | **LeetCode 1235: Maximum Profit in Job Scheduling** | 🔴 Hard | Weighted Interval DP | Sort ends + binary search `p(i)` |

---

## 🎓 SELF-CHECK & FINAL VERIFICATION

- [x] **Zero LaTeX Check:** All complexities in standard backticks (`O(N^2)`, `O(N log N)`). Zero `$` syntax.
- [x] **Production Dual-Language Code:** Complete C# (.NET 8/9) and Python (3.11+) implementations with reconstruction and binary search.
- [x] **3-Step Framework:** Clear structure around Choice, State Definition, and Transitions & Base Cases.
- [x] **Cognitive Bloat Removed:** No legacy cognitive lens section; corporate stories condensed into a single `> [!NOTE]` callout.
- [x] **ASCII Visuals:** Clean diagrams illustrating Patience Sorting card piles and the tails array evolution.

---

> 🧭 **Navigation:** [← Previous Day](Week_10_Day_03_2D_DP_Grids_Edit_Distance_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_10_Day_05_Story_Driven_DP_Advanced_Instructional.md)
