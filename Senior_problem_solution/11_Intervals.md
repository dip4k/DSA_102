# Phase 11: Intervals

> **Focus:** Interval Sorting Invariants, Contiguous Merging, Disjoint 3-Zone Sweeps, Earliest-Deadline Scheduling, and Concurrency Peak Tracking (Min-Heap vs Chronological Event Sweep).  
> **Source Curriculum:** [`Senior_dsa_question_list.md`](../Senior_dsa_question_list.md) — Phase 11 (Problems #60–#63)

---

## 60. Merge Intervals (LeetCode #56)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#intervals` `#sorting` `#contiguous-merging` |
| **LeetCode Link** | [Merge Intervals](https://leetcode.com/problems/merge-intervals/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given an array of `intervals` where $intervals[i] = [start_i, end_i]$, merge all overlapping intervals, and return an array of the non-overlapping intervals that cover all intervals in the input.
- **Key Constraints:**
  - $1 \le intervals.Length \le 10^4$.
  - $intervals[i].Length == 2$.
  - $0 \le start_i \le end_i \le 10^4$.
- **Senior Edge Cases to Defend:**
  - Fully subsumed intervals ($[1, 10]$ completely covers $[2, 5]$).
  - Identical start times with diverging end times ($[1, 4]$ and $[1, 8]$).
  - Zero-length / point intervals ($[1, 1]$ touching $[1, 2]$).
  - Pre-sorted vs reverse-sorted inputs.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Sort intervals ascending by start time. Compare the running active interval with each incoming candidate: if `candidate.start <= active.end`, expand `active.end = max(active.end, candidate.end)`. Otherwise, commit `active` to the results and adopt `candidate` as the new active interval.
- **Sample 1:**
  - **Input:** `intervals = [[1, 3], [2, 6], [8, 10], [15, 18]]`
  - **Output:** `[[1, 6], [8, 10], [15, 18]]`
- **Sample 2:**
  - **Input:** `intervals = [[1, 4], [4, 5]]`
  - **Output:** `[[1, 5]]`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)
- **3.1 The Intuitive Spark & Conceptual Metaphor:**
  - *The Rolling Snowball on a 1D Timeline:* Imagine a snowball rolling forward across a 1D timeline. By sorting intervals by start time, we ensure that as time advances, no upcoming interval can ever start behind where we currently are. As the snowball rolls forward over the interval $[start, end]$, if the next interval starts inside or at the boundary of the snowball (`next.start <= end`), the snowball absorbs it, expanding its reach to `max(end, next.end)`. If the next interval starts in empty space beyond the snowball (`next.start > end`), the current snowball freezes into an unalterable monument (committed to the result), and a brand-new snowball is initiated.
- **3.2 The Naive Bottleneck & Redundant Computation:**
  - In an unsorted array, any interval could potentially overlap with ANY other interval in the entire collection. Finding connected components across all intervals requires an all-pairs intersection graph with $O(N^2)$ edge checks or Disjoint-Set Union (DSU).
  - Sorting by `start` time in $O(N \log N)$ time linearizes the timeline: each interval only needs to be compared against the immediate active predecessor, collapsing the search from $O(N^2)$ to a single $O(N)$ pass.
- **3.3 The Breakthrough Insight & Mathematical Invariant:**
  - **Start-Time Monotonicity Invariant:**
    $$\forall i < j: start_i \le start_j$$
  - **Irrevocable Sealing Principle:**
    If $start_j > active.end$, then for all future intervals $k > j$, by transitivity:
    $$start_k \ge start_j > active.end$$
    Therefore, NO future interval can ever overlap with $active$. It is provably safe to seal $active$ and append it to the settled output.
- **3.4 Cursor Semantics & Invariant Partition Architecture:**
  ```text
  Timeline Sweep State Partition:
  +--------------------------------+----------------------------+-----------------------------+
  |    Settled & Sealed Merges     |   Active Expanding Horizon |  Unexplored Future Intervals|
  |    result.Add(interval)        |   [activeStart, activeEnd] |  [nextStart, nextEnd]       |
  +--------------------------------+----------------------------+-----------------------------+
                                   ^
       If nextStart <= activeEnd  ===> activeEnd = max(activeEnd, nextEnd)
       If nextStart >  activeEnd  ===> Commit active, active = next
  ```
- **3.5 State Transition Triggers & Decision Gates:**
  - Overlap Condition Gate: `if (candidate[0] <= active[1])`
    - Action: `active[1] = Math.Max(active[1], candidate[1]);`
  - Disjoint Condition Gate: `else`
    - Action: Commit `active`, set `active = candidate`.
- **3.6 Concrete Step-by-Step State Trace:**
  - Input: `[[1, 3], [2, 6], [8, 10], [15, 18]]` (Already sorted by start)

  | Step | Candidate Interval | Active Interval Before | Overlap Check ($s_{\text{next}} \le e_{\text{act}}$) | Updated Active Interval | Settled Result List |
  | :--- | :--- | :--- | :--- | :--- | :--- |
  | Init | `[1, 3]` | `[1, 3]` | Baseline | `[1, 3]` | `[]` |
  | 1 | `[2, 6]` | `[1, 3]` | $2 \le 3$ (Overlap!) | `[1, max(3, 6)] = [1, 6]` | `[]` |
  | 2 | `[8, 10]` | `[1, 6]` | $8 > 6$ (Disjoint!) | Commit `[1, 6]`; New active: `[8, 10]` | `[[1, 6]]` |
  | 3 | `[15, 18]`| `[8, 10]` | $15 > 10$ (Disjoint!) | Commit `[8, 10]`; New active: `[15, 18]`| `[[1, 6], [8, 10]]` |
  | End | None | `[15, 18]` | Loop exit | Flush final active: `[15, 18]` | `[[1, 6], [8, 10], [15, 18]]` |

### 4. Approach & Complexity Deconstruction
- **4.1 Anchor Points & Approach Selection Criteria:**
  - **Approach 1 (Sort + Dynamic List Sweep):** Standard idiomatic approach. Collects merged results into a `List<int[]>`. Memory footprint is $O(N)$ for the result.
  - **Approach 2 (In-Place Output Compaction):** Operates directly on the sorted `intervals` array using a `writeIndex` pointer. Overwrites merged intervals in-place, achieving $O(1)$ auxiliary space (excluding the output array). Ideal for memory-constrained embedded environments.
- **4.2 Step-by-Step Natural Progression Flow:**
  - *Step 1: Setup & Boundaries:* Handle empty or single-interval inputs. Sort array by `start` ascending.
  - *Step 2: Main Exploration Loop:* Initialize `active` with first interval. Iterate from index 1 to $N - 1$.
  - *Step 3: Invariant Maintenance & Condition Gates:* Check overlap with `active`. Either expand `active.end` or commit and reassign.
  - *Step 4: Resolution & Return:* Flush the trailing `active` interval into the result list. Return `result.ToArray()`.
- **4.3 Alternative Approaches Analysis:**
  - Connected Components (Graph BFS/DFS): Treats each interval as a node and adds edges between overlapping pairs. Finding connected components takes $O(N^2)$ time and $O(N^2)$ space—strictly inferior to sorting.
- **4.4 Multi-Dimensional Complexity & Trade-Off Matrix:**

| Approach | Time (Best/Avg/Worst) | Aux Space | Output Space | Mutates Input | Cache Locality |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Sort + List Sweep** | $O(N \log N)$ | $O(N)$ | $O(N)$ | No (clones) | High |
| **In-Place Compaction** | $O(N \log N)$ | $O(1)$ | $O(N)$ | Yes (mutates input) | Optimal |
| **Graph DSU / BFS** | $O(N^2)$ | $O(N^2)$ | $O(N)$ | No | Poor |

### 5. Production C# Implementations

```csharp
// ============================================================================
// ARCHITECTURE ANCHOR: Merge Intervals
// Primary: Sort by Start Time + Single-Pass Sweep (O(N log N) Time, O(N) Space)
// Secondary: In-Place Array Compaction (O(N log N) Time, O(1) Extra Space)
// Invariant: Sorted start times guarantee active interval can only overlap with immediate successors
// ============================================================================

public class Solution
{
    /// <summary>
    /// Merges overlapping intervals by sorting on start time and executing a linear sweep.
    /// Non-destructive to original array references if cloned.
    /// </summary>
    public int[][] Merge(int[][] intervals)
    {
        // Boundary Gate: Trivial inputs require no merging
        if (intervals == null || intervals.Length <= 1)
        {
            return intervals ?? Array.Empty<int[]>();
        }

        // Structural Invariant: Sort intervals ascending by start time
        Array.Sort(intervals, (a, b) => a[0].CompareTo(b[0]));

        var result = new List<int[]>();
        // Initialize the active interval with the first chronologically ordered interval
        int[] activeInterval = intervals[0];
        result.Add(activeInterval);

        for (int i = 1; i < intervals.Length; i++)
        {
            int[] current = intervals[i];

            // Overlap Gate: Current interval starts before or at active interval's end
            if (current[0] <= activeInterval[1])
            {
                // Invariant Expansion: Extend active interval's end boundary if current reaches farther
                activeInterval[1] = Math.Max(activeInterval[1], current[1]);
            }
            else
            {
                // Disjoint Gate: Active interval is permanently sealed; initiate a new active horizon
                activeInterval = current;
                result.Add(activeInterval);
            }
        }

        return result.ToArray();
    }
}

public class SolutionInPlace
{
    /// <summary>
    /// Merges intervals in-place by overwriting the input array using a write pointer.
    /// Achieves O(1) auxiliary memory.
    /// </summary>
    public int[][] Merge(int[][] intervals)
    {
        if (intervals == null || intervals.Length <= 1)
        {
            return intervals ?? Array.Empty<int[]>();
        }

        // Sort ascending by start time
        Array.Sort(intervals, (a, b) => a[0].CompareTo(b[0]));

        // writeIndex tracks the position of the latest merged interval
        int writeIndex = 0;

        for (int i = 1; i < intervals.Length; i++)
        {
            // Overlap check against the interval at writeIndex
            if (intervals[i][0] <= intervals[writeIndex][1])
            {
                intervals[writeIndex][1] = Math.Max(intervals[writeIndex][1], intervals[i][1]);
            }
            else
            {
                // Advance write cursor and record new distinct interval
                writeIndex++;
                intervals[writeIndex] = intervals[i];
            }
        }

        // Allocate only the exact final compacted result
        int[][] result = new int[writeIndex + 1][];
        Array.Copy(intervals, result, writeIndex + 1);
        return result;
    }
}
```

---

## 61. Insert Interval (LeetCode #57)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | 🔥 High |
| **Pattern Tags** | `#intervals` `#linear-sweep` `#three-phase-partition` |
| **LeetCode Link** | [Insert Interval](https://leetcode.com/problems/insert-interval/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given an array of non-overlapping intervals `intervals` sorted in ascending order by $start_i$, insert `newInterval = [start, end]` such that `intervals` is still sorted in ascending order and has no overlapping intervals (merge overlapping intervals if necessary).
- **Key Constraints:**
  - $0 \le intervals.Length \le 10^4$.
  - $intervals[i].Length == 2$, $newInterval.Length == 2$.
  - $0 \le start_i \le end_i \le 10^5$.
  - Existing `intervals` are sorted in ascending order by $start_i$ with NO overlaps.
- **Senior Edge Cases to Defend:**
  - Empty existing array $\implies$ return `[newInterval]`.
  - `newInterval` is completely before all existing intervals (no overlap, inserted at head).
  - `newInterval` is completely after all existing intervals (no overlap, inserted at tail).
  - `newInterval` completely spans and swallows all existing intervals into a single super-interval.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** 3-Zone Linear Partition: Because the input is pre-sorted and non-overlapping, sequentially process the timeline in three clean phases: (1) Add intervals strictly to the left of `newInterval`, (2) Merge all overlapping intervals into `newInterval`, (3) Add intervals strictly to the right.
- **Sample 1:**
  - **Input:** `intervals = [[1, 3], [6, 9]]`, `newInterval = [2, 5]`
  - **Output:** `[[1, 5], [6, 9]]`
- **Sample 2:**
  - **Input:** `intervals = [[1, 2], [3, 5], [6, 7], [8, 10], [12, 16]]`, `newInterval = [4, 8]`
  - **Output:** `[[1, 2], [3, 10], [12, 16]]`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)
- **3.1 The Intuitive Spark & Conceptual Metaphor:**
  - *The 3-Zone Timeline Continent:* Inserting `newInterval` into an already-sorted disjoint timeline splits the timeline into three geographical territories:
    1. *Zone 1 (Western Territory - Strictly Left):* Intervals that end before `newInterval` begins ($end_i < new.start$). They are completely immune to collisions. Copy them directly.
    2. *Zone 2 (Collision Horizon - Melting Zone):* Any interval whose start is $\le new.end$. Every interval in this zone collides with `newInterval`. They melt together into one single entity: $new.start = \min(new.start, start_i)$ and $new.end = \max(new.end, end_i)$.
    3. *Zone 3 (Eastern Territory - Strictly Right):* Intervals that start after the melted `newInterval` ends ($start_i > new.end$). Copy them directly.
- **3.2 The Naive Bottleneck & Redundant Computation:**
  - Appending `newInterval` to the end of the array and invoking a full `Array.Sort` costs $O(N \log N)$ and throws away the valuable precondition that `intervals` was ALREADY sorted and non-overlapping.
  - A 3-zone linear sweep runs in guaranteed $O(N)$ time with zero comparison sorting.
- **3.3 The Breakthrough Insight & Mathematical Invariant:**
  - **Disjoint Partition Invariant:**
    $$\text{Zone 1: } intervals[i].end < newInterval.start \iff \text{Interval } i \cap newInterval = \emptyset$$
    $$\text{Zone 2: } intervals[i].start \le newInterval.end \iff \text{Interval } i \cap newInterval \neq \emptyset$$
    $$\text{Zone 3: } intervals[i].start > newInterval.end \iff \text{All remaining intervals are disjoint and right}$$
  - Because `intervals` are sorted, once Zone 2 terminates, no subsequent interval can ever overlap with `newInterval`.
- **3.4 Cursor Semantics & Invariant Partition Architecture:**
  ```text
  Timeline Cursor Partition:
  Zone 1: curr.end < new.start   | Zone 2: curr.start <= new.end | Zone 3: curr.start > new.end
  +------------------------------+-------------------------------+------------------------------+
  | [1, 2]                       | [3, 5], [6, 7], [8, 10]       | [12, 16]                     |
  | Action: result.Add(curr)     | Action: Melt into newInterval | Action: result.Add(curr)     |
  +------------------------------+-------------------------------+------------------------------+
  ```
- **3.5 State Transition Triggers & Decision Gates:**
  - Phase 1 While Gate: `while (i < n && intervals[i][1] < newInterval[0])`
  - Phase 2 While Gate: `while (i < n && intervals[i][0] <= newInterval[1])`
  - Phase 3 While Gate: `while (i < n)`
- **3.6 Concrete Step-by-Step State Trace:**
  - Input: `intervals = [[1, 2], [3, 5], [6, 7], [8, 10], [12, 16]]`, `newInterval = [4, 8]`

  | Phase | Index `i` | Current `intervals[i]` | Condition Evaluated | Action Taken | `newInterval` State | Result Buffer |
  | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
  | Phase 1 | 0 | `[1, 2]` | $2 < 4$ (True) | Add `[1, 2]` | `[4, 8]` | `[[1, 2]]` |
  | Phase 1 | 1 | `[3, 5]` | $5 < 4$ (False) | Exit Phase 1 | `[4, 8]` | `[[1, 2]]` |
  | Phase 2 | 1 | `[3, 5]` | $3 \le 8$ (True) | Melt: min(4,3), max(8,5) | `[3, 8]` | `[[1, 2]]` |
  | Phase 2 | 2 | `[6, 7]` | $6 \le 8$ (True) | Melt: min(3,6), max(8,7) | `[3, 8]` | `[[1, 2]]` |
  | Phase 2 | 3 | `[8, 10]`| $8 \le 8$ (True) | Melt: min(3,8), max(8,10)| `[3, 10]` | `[[1, 2]]` |
  | Phase 2 | 4 | `[12, 16]`| $12 \le 10$ (False) | Exit Phase 2; Commit melted | `[3, 10]` | `[[1, 2], [3, 10]]` |
  | Phase 3 | 4 | `[12, 16]`| In bounds | Add `[12, 16]` | Done | `[[1, 2], [3, 10], [12, 16]]` |

### 4. Approach & Complexity Deconstruction
- **4.1 Anchor Points & Approach Selection Criteria:**
  - **Approach 1 (Three-Phase Linear Sweep):** Optimal $O(N)$ single pass, intuitive structure, zero complex edge cases.
  - **Approach 2 (Binary Search for Overlap Boundaries):** Uses binary search to locate the start and end of Zone 2 in $O(\log N)$ time. However, slicing and splicing the resulting array in memory still requires $O(N)$ copy operations.
- **4.2 Step-by-Step Natural Progression Flow:**
  - *Step 1: Setup & Boundaries:* Handle empty array by returning `[newInterval]`. Initialize result list and cursor `i = 0`.
  - *Step 2: Phase 1 Loop:* While `intervals[i].end < newInterval.start`, append `intervals[i]`.
  - *Step 3: Phase 2 Loop:* While `intervals[i].start <= newInterval.end`, expand `newInterval`. Then append `newInterval`.
  - *Step 4: Phase 3 Loop:* Append all remaining intervals from `i` to $n - 1$. Return array.
- **4.3 Alternative Approaches Analysis:**
  - Append & Sort: Add `newInterval` to end, call `Array.Sort`, then call `Merge`. Simpler code reuse, but degrades performance to $O(N \log N)$.
- **4.4 Multi-Dimensional Complexity & Trade-Off Matrix:**

| Approach | Time Complexity | Auxiliary Space | Output Space | Precondition Exploitation |
| :--- | :--- | :--- | :--- | :--- |
| **Three-Phase Linear Sweep** | $O(N)$ | $O(1)$ extra | $O(N)$ | Full (Exploits sorted & disjoint) |
| **Binary Search + Slice** | $O(\log N + N)$ | $O(1)$ extra | $O(N)$ | Full |
| **Append & Re-Sort** | $O(N \log N)$ | $O(N)$ | $O(N)$ | None (Discards sorted guarantee) |

### 5. Production C# Implementations

```csharp
// ============================================================================
// ARCHITECTURE ANCHOR: Insert Interval
// Primary: Three-Phase Linear Sweep (Guaranteed O(N) Time, O(1) Extra Space)
// Invariant: Exploits pre-sorted disjoint guarantee across three sequential zones:
//   Zone 1: curr.end < new.start (Strictly Left)
//   Zone 2: curr.start <= new.end (Overlap & Melt)
//   Zone 3: curr.start > new.end (Strictly Right)
// ============================================================================

public class Solution
{
    /// <summary>
    /// Inserts newInterval into sorted non-overlapping intervals in optimal O(N) time.
    /// </summary>
    public int[][] Insert(int[][] intervals, int[] newInterval)
    {
        ArgumentNullException.ThrowIfNull(newInterval);

        // Edge Gate: Empty existing collection immediately yields single interval
        if (intervals == null || intervals.Length == 0)
        {
            return new int[][] { newInterval };
        }

        var result = new List<int[]>();
        int i = 0;
        int n = intervals.Length;

        // Phase 1 (Zone 1): Collect all intervals strictly preceding newInterval
        // Invariant: No overlap is mathematically possible when curr.end < new.start
        while (i < n && intervals[i][1] < newInterval[0])
        {
            result.Add(intervals[i]);
            i++;
        }

        // Phase 2 (Zone 2): Melt all overlapping intervals into newInterval
        // Invariant: Overlap exists as long as curr.start <= new.end
        while (i < n && intervals[i][0] <= newInterval[1])
        {
            newInterval[0] = Math.Min(newInterval[0], intervals[i][0]);
            newInterval[1] = Math.Max(newInterval[1], intervals[i][1]);
            i++;
        }
        // Commit the fully expanded composite interval
        result.Add(newInterval);

        // Phase 3 (Zone 3): Collect all intervals strictly succeeding the merged entity
        // Invariant: All remaining intervals have curr.start > new.end
        while (i < n)
        {
            result.Add(intervals[i]);
            i++;
        }

        return result.ToArray();
    }
}
```

---

## 62. Non-overlapping Intervals (LeetCode #435)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | 🔥 High |
| **Pattern Tags** | `#intervals` `#greedy` `#interval-scheduling` `#earliest-finish-time` |
| **LeetCode Link** | [Non-overlapping Intervals](https://leetcode.com/problems/non-overlapping-intervals/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given an array of intervals `intervals` where $intervals[i] = [start_i, end_i]$, return the minimum number of intervals you need to remove to make the rest of the intervals non-overlapping.
- **Key Constraints:**
  - $1 \le intervals.Length \le 10^5$.
  - $intervals[i].Length == 2$.
  - $-5 \times 10^4 \le start_i < end_i \le 5 \times 10^4$.
- **Senior Edge Cases to Defend:**
  - Adjacent boundary points touching: $[1, 2]$ and $[2, 3]$ are non-overlapping by definition (they touch at a single point, which does not constitute an overlap).
  - Concentric identical intervals: $[1, 2]$ and $[1, 2]$ (must remove one).
  - Highly nested intervals: $[1, 100], [2, 3], [3, 4], [4, 5]$ (removing $[1, 100]$ preserves 3 intervals).

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Dual Formulation Equivalence: Minimizing intervals removed is mathematically identical to MAXIMIZING intervals retained. Classical Greedy Interval Scheduling: Sort intervals by earliest end time; greedily retain the interval that finishes earliest to leave maximal space for future intervals.
- **Sample 1:**
  - **Input:** `intervals = [[1, 2], [2, 3], [3, 4], [1, 3]]`
  - **Output:** `1` (Remove `[1, 3]`)
- **Sample 2:**
  - **Input:** `intervals = [[1, 2], [1, 2], [1, 2]]`
  - **Output:** `2`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)
- **3.1 The Intuitive Spark & Conceptual Metaphor:**
  - *The Greedy Conference Hall / Timeline Real Estate:* If you have a conference hall and want to host the greatest number of events possible, which event do you book first? You always book the event that **ends earliest**! The earlier the first event finishes, the more time remains available for subsequent events. Booking an event that finishes later can only reduce or equal your future choices.
- **3.2 The Naive Bottleneck & Redundant Computation:**
  - Exhaustive search (backtracking over $2^N$ subsets of intervals) takes exponential time $O(2^N)$.
  - Dynamic Programming (LIS-style longest non-overlapping chain) takes $O(N^2)$ time.
  - Greedy choice property allows optimal decisions in a single pass after $O(N \log N)$ sorting.
- **3.3 The Breakthrough Insight & Mathematical Invariant:**
  - **Greedy Exchange Argument Proof:**
    Let $S$ be the interval with the earliest finish time among all available candidates. Suppose an optimal subset $OPT$ does not include $S$, but instead selects $I_1$ as its first interval. Because $S$ has the earliest finish time, $end_S \le end_{I_1}$. Replacing $I_1$ with $S$ cannot cause any conflicts with the remaining intervals in $OPT$, because $S$ finishes before or when $I_1$ finishes. Hence, $OPT' = (OPT \setminus \{I_1\}) \cup \{S\}$ is also valid and has identical size $|OPT|$. By induction, a greedy choice never sacrifices optimality.
- **3.4 Cursor Semantics & Invariant Partition Architecture:**
  ```text
  Earliest Finish Timeline Sweep:
  Intervals sorted by end time:
  [ i_1: end=2 ]  --> Accepted! prevEnd = 2
  [ i_2: [1, 3] ] --> Conflict! start=1 < prevEnd=2 ===> REMOVE (removals++)
  [ i_3: [2, 3] ] --> Accepted! start=2 >= prevEnd=2 ===> prevEnd = 3
  ```
- **3.5 State Transition Triggers & Decision Gates:**
  - Non-Conflict Gate: `if (intervals[i][0] >= prevEnd)`
    - Action: Keep interval; update `prevEnd = intervals[i][1]`.
  - Conflict Gate: `else`
    - Action: Conflict detected; increment `removals++`. (Implicitly discard `intervals[i]`).
- **3.6 Concrete Step-by-Step State Trace:**
  - Input: `[[1, 2], [2, 3], [3, 4], [1, 3]]`
  - Sorted by End Time: `[1, 2]`, `[2, 3]`, `[1, 3]`, `[3, 4]`

  | Step | Candidate Interval | `prevEnd` Before | Check ($start \ge prevEnd$) | Action | `prevEnd` After | Total Removals |
  | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
  | Init | `[1, 2]` | $-\infty$ | Baseline | Accept | 2 | 0 |
  | 1 | `[2, 3]` | 2 | $2 \ge 2$ (Pass) | Accept | 3 | 0 |
  | 2 | `[1, 3]` | 3 | $1 < 3$ (**Conflict**) | Remove | 3 | **1** |
  | 3 | `[3, 4]` | 3 | $3 \ge 3$ (Pass) | Accept | 4 | 1 |

  *Result:* 1 interval removed.

### 4. Approach & Complexity Deconstruction
- **4.1 Anchor Points & Approach Selection Criteria:**
  - **Approach 1 (Sort by End Time):** The standard textbook Greedy Interval Scheduling formulation. Most direct proof and implementation.
  - **Approach 2 (Sort by Start Time):** When sorting by start time, on collision you must greedily eliminate the interval with the *larger* end time: `prevEnd = Math.Min(prevEnd, current[1])`.
- **4.2 Step-by-Step Natural Progression Flow:**
  - *Step 1:* Guard against empty/single array. Sort ascending by `end` time.
  - *Step 2:* Initialize `prevEnd = intervals[0][1]`, `removals = 0`.
  - *Step 3:* Iterate from index 1. If start is $\ge prevEnd$, advance `prevEnd`. Else, increment `removals`.
  - *Step 4:* Return `removals`.
- **4.3 Alternative Approaches Analysis:**
  - Sorting by start time: If $curr.start < prevEnd$, increment removals and set $prevEnd = \min(prevEnd, curr.end)$. Both approaches have $O(N \log N)$ time.
- **4.4 Multi-Dimensional Complexity & Trade-Off Matrix:**

| Approach | Time Complexity | Aux Space | Code Complexity |
| :--- | :--- | :--- | :--- |
| **Greedy Sort by End Time** | $O(N \log N)$ | $O(1)$ or $O(\log N)$ sort | Minimal |
| **Greedy Sort by Start Time** | $O(N \log N)$ | $O(1)$ or $O(\log N)$ sort | Requires `Math.Min` on conflict |
| **Dynamic Programming (LIS)** | $O(N^2)$ | $O(N)$ | High |

### 5. Production C# Implementations

```csharp
// ============================================================================
// ARCHITECTURE ANCHOR: Non-overlapping Intervals
// Primary: Greedy Earliest Finish Time (O(N log N) Time, O(1) Extra Space)
// Secondary: Greedy Sort by Start Time with Minimum End Pruning
// Invariant: Greedily picking earliest finishing intervals leaves maximum space
// ============================================================================

public class Solution
{
    /// <summary>
    /// Computes minimum removals by maximizing non-overlapping intervals via Earliest Finish Time.
    /// </summary>
    public int EraseOverlapIntervals(int[][] intervals)
    {
        if (intervals == null || intervals.Length <= 1)
        {
            return 0;
        }

        // Mathematical Invariant: Sort intervals ascending by their END time
        Array.Sort(intervals, (a, b) => a[1].CompareTo(b[1]));

        int removals = 0;
        // The first interval finishes earliest, so it is unconditionally accepted
        int prevEnd = intervals[0][1];

        for (int i = 1; i < intervals.Length; i++)
        {
            // Non-Conflict Gate: Current starts after or exactly when previous ends
            if (intervals[i][0] >= prevEnd)
            {
                // Accept interval and advance horizon
                prevEnd = intervals[i][1];
            }
            else
            {
                // Conflict Gate: Overlap detected. Greedily discard current interval
                removals++;
            }
        }

        return removals;
    }
}

public class SolutionSortByStart
{
    /// <summary>
    /// Alternative greedy approach sorting by start time.
    /// When collision occurs, greedily keeps the interval with smaller end time.
    /// </summary>
    public int EraseOverlapIntervals(int[][] intervals)
    {
        if (intervals == null || intervals.Length <= 1) return 0;

        Array.Sort(intervals, (a, b) => a[0].CompareTo(b[0]));

        int removals = 0;
        int prevEnd = intervals[0][1];

        for (int i = 1; i < intervals.Length; i++)
        {
            if (intervals[i][0] < prevEnd)
            {
                // Overlap: Must eliminate one interval. Greedily eliminate the one that extends farther
                removals++;
                prevEnd = Math.Min(prevEnd, intervals[i][1]);
            }
            else
            {
                prevEnd = intervals[i][1];
            }
        }

        return removals;
    }
}
```

---

## 63. Meeting Rooms II (LeetCode #253)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#intervals` `#min-heap` `#sweep-line` `#two-pointers` `#concurrency` |
| **LeetCode Link** | [Meeting Rooms II](https://leetcode.com/problems/meeting-rooms-ii/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given an array of meeting time intervals `intervals` where $intervals[i] = [start_i, end_i]$, return the minimum number of conference rooms required (equivalent to the peak number of concurrent overlapping meetings).
- **Key Constraints:**
  - $1 \le intervals.Length \le 10^4$.
  - $0 \le start_i < end_i \le 10^6$.
- **Senior Edge Cases to Defend:**
  - Meetings touching at boundaries ($[0, 10]$ and $[10, 20]$ can reuse the exact same conference room without collision).
  - All meetings completely disjoint ($N$ meetings $\implies 1$ room).
  - All meetings completely concurrent ($N$ identical meetings $\implies N$ rooms).

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Track peak overlapping intervals on a continuous timeline. Can be solved either via a Min-Heap tracking active room end times, or a Chronological Event Sweep-Line comparing separate sorted start and end arrays.
- **Sample 1:**
  - **Input:** `intervals = [[0, 30], [5, 10], [15, 20]]`
  - **Output:** `2`
- **Sample 2:**
  - **Input:** `intervals = [[7, 10], [2, 4]]`
  - **Output:** `1`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)
- **3.1 The Intuitive Spark & Conceptual Metaphor:**
  - *Hotel Room Key Reuse vs Airport Turnstile:*
    - *Metaphor A (Min-Heap Room Reuse):* You are a hotel manager. When a guest arrives, you look at the room whose current occupant will check out earliest (root of the min-heap). If the guest arrives at or after checkout time, you hand them that room's key (reuse room; dequeue old checkout, enqueue new checkout). If not, you must build a new room!
    - *Metaphor B (Turnstile Chronological Sweep):* Every meeting start is a person entering a room through a turnstile ($+1$). Every meeting end is a person leaving through a turnstile ($-1$). If you decouple start times and end times into two sorted timelines, you can simulate time advancing. The maximum number of people inside the building at any instant is your answer.
- **3.2 The Naive Bottleneck & Redundant Computation:**
  - Testing all pairs of intervals for concurrency takes $O(N^2)$ time.
  - Sorting starts and ends independently reduces the problem to $O(N \log N)$ sorting and an $O(N)$ two-pointer sweep with zero heap allocations.
- **3.3 The Breakthrough Insight & Mathematical Invariant:**
  - **Decoupled Identity Invariant:**
    A room does NOT care *which* specific meeting took place in it previously; it only cares whether *any* meeting has finished before the next one starts!
    Therefore, pairing between start and end times is completely irrelevant to room allocation:
    $$\text{starts} = [s_1 \le s_2 \le \dots \le s_N], \quad \text{ends} = [e_1 \le e_2 \le \dots \le e_N]$$
    If $\text{starts}[startPtr] < \text{ends}[endPtr]$, a new meeting begins before the earliest ongoing meeting concludes $\implies$ allocate a new room (`roomsNeeded++`).
    Else, an ongoing meeting concludes $\implies$ advance `endPtr++` (existing room freed and reused).
- **3.4 Cursor Semantics & Invariant Partition Architecture:**
  ```text
  Chronological Sweep-Line Dual Array State:
  starts: [ 0,  5, 15 ]   <--- Pointer: startPtr
  ends:   [ 10, 20, 30 ]  <--- Pointer: endPtr

  Compare starts[startPtr] vs ends[endPtr]:
  s=0  < e=10  ===> Room needed! rooms = 1, startPtr++
  s=5  < e=10  ===> Room needed! rooms = 2, startPtr++
  s=15 >= e=10 ===> Room freed! endPtr++ (reuse room without increasing rooms)
  ```
- **3.5 State Transition Triggers & Decision Gates:**
  - New Room Gate: `if (startTimes[startPtr] < endTimes[endPtr]) roomsNeeded++;`
  - Reuse Room Gate: `else endPtr++;`
- **3.6 Concrete Step-by-Step State Trace:**
  - Input: `[[0, 30], [5, 10], [15, 20]]`
  - `starts = [0, 5, 15]`, `ends = [10, 20, 30]`

  | Step | `startPtr` | `endPtr` | `starts[startPtr]` | `ends[endPtr]` | Check ($s < e$) | Action Taken | `roomsNeeded` |
  | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
  | 1 | 0 | 0 | 0 | 10 | $0 < 10$ (True) | Meeting starts before any end $\implies$ Allocate room | 1 |
  | 2 | 1 | 0 | 5 | 10 | $5 < 10$ (True) | Meeting starts before any end $\implies$ Allocate room | **2** |
  | 3 | 2 | 0 | 15 | 10 | $15 \ge 10$ (False)| Meeting ended! Reuse room $\implies$ `endPtr++` | 2 |
  | 4 | 2 | 1 | 15 | 20 | $15 < 20$ (True) | Meeting starts before next end $\implies$ Allocate room | **2** (Reused) |

  *Result:* Maximum concurrent rooms = 2.

### 4. Approach & Complexity Deconstruction
- **4.1 Anchor Points & Approach Selection Criteria:**
  - **Approach 1 (Chronological Sweep-Line with Two Pointers):** Optimal cache locality. Uses flat 1D primitive arrays (`int[]`), avoiding heap objects. The gold standard in high-performance computing.
  - **Approach 2 (Min-Heap Room Allocator):** Natural object-oriented simulation. Preferred when meetings arrive dynamically in a real-time stream.
- **4.2 Step-by-Step Natural Progression Flow:**
  - *Step 1:* Guard against empty input. Extract `startTimes` and `endTimes` arrays.
  - *Step 2:* Sort both arrays independently.
  - *Step 3:* Iterate `startPtr` from 0 to $n - 1$. If `starts[startPtr] < ends[endPtr]`, increment rooms; else advance `endPtr`.
  - *Step 4:* Return `roomsNeeded`.
- **4.3 Alternative Approaches Analysis:**
  - Event Coordinate Compression with TreeMap / Dictionary: Collect events as `+1` (start) and `-1` (end). Walk keys in sorted order tracking prefix sum. $O(N \log N)$ but higher memory allocations.
- **4.4 Multi-Dimensional Complexity & Trade-Off Matrix:**

| Approach | Time Complexity | Auxiliary Space | Cache Locality | Streaming Friendly |
| :--- | :--- | :--- | :--- | :--- |
| **Chronological Sweep (Two Pointers)**| $O(N \log N)$ | $O(N)$ (1D arrays) | Optimal (contiguous RAM) | No |
| **Min-Heap Allocator** | $O(N \log N)$ | $O(N)$ (heap nodes) | Moderate | Yes (streaming) |
| **Event Map / Prefix Sum** | $O(N \log N)$ | $O(N)$ (map entries) | Low | Yes |

### 5. Production C# Implementations

```csharp
// ============================================================================
// ARCHITECTURE ANCHOR: Meeting Rooms II
// Primary: Chronological Sweep-Line with Two Pointers (O(N log N) Time, Optimal Cache)
// Secondary: Min-Heap Resource Allocator (O(N log N) Time, Streaming-Safe)
// Invariant: Peak concurrency equals rooms needed; start/end decoupling preserves correctness
// ============================================================================

public class Solution
{
    /// <summary>
    /// Computes minimum conference rooms using chronological sweep-line over decoupled start/end arrays.
    /// Delivers superior cache performance compared to heap-based allocation.
    /// </summary>
    public int MinMeetingRooms(int[][] intervals)
    {
        if (intervals == null || intervals.Length == 0)
        {
            return 0;
        }

        int n = intervals.Length;
        int[] startTimes = new int[n];
        int[] endTimes = new int[n];

        // Decouple interval pairs into independent temporal boundary arrays
        for (int i = 0; i < n; i++)
        {
            startTimes[i] = intervals[i][0];
            endTimes[i] = intervals[i][1];
        }

        // Sort both primitive arrays independently
        Array.Sort(startTimes);
        Array.Sort(endTimes);

        int roomsNeeded = 0;
        int endPtr = 0;

        // Invariant: Whenever a meeting starts before the earliest ending meeting finishes,
        // concurrency increases and a new room must be provisioned.
        for (int startPtr = 0; startPtr < n; startPtr++)
        {
            if (startTimes[startPtr] < endTimes[endPtr])
            {
                // Concurrency spike: Need a new room
                roomsNeeded++;
            }
            else
            {
                // An existing meeting finished; reuse its room and advance end pointer
                endPtr++;
            }
        }

        return roomsNeeded;
    }
}

public class SolutionHeap
{
    /// <summary>
    /// Simulates room allocation using a Min-Heap tracking active room checkout times.
    /// Natural model for online streaming room reservation systems.
    /// </summary>
    public int MinMeetingRooms(int[][] intervals)
    {
        if (intervals == null || intervals.Length == 0) return 0;

        // Sort meetings chronologically by start time
        Array.Sort(intervals, (a, b) => a[0].CompareTo(b[0]));

        // Min-heap tracking the earliest end time among all active rooms
        var minHeap = new PriorityQueue<int, int>();

        foreach (var meeting in intervals)
        {
            int start = meeting[0];
            int end = meeting[1];

            // Invariant Gate: If earliest meeting finished before current starts, reuse room
            if (minHeap.Count > 0 && start >= minHeap.Peek())
            {
                minHeap.Dequeue();
            }

            // Allocate or renew room with current meeting's end time
            minHeap.Enqueue(end, end);
        }

        // The remaining size of the heap represents the peak concurrent rooms required
        return minHeap.Count;
    }
}
```
