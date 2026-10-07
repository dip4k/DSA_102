# 📘 WEEK 12 DAY 2: ACTIVITY SELECTION & INTERVAL PROBLEMS — ENGINEERING GUIDE

> 🧭 **Navigation:** [← Previous Day](Week_12_Day_01_Greedy_Fundamentals_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_12_Day_03_Huffman_Coding_And_Optimal_Trees_Instructional.md)
> 
> 💡 **Instructor Note:** *Interval problems form one of the highest-frequency patterns in engineering loops. Master the distinction between interval selection (greedy earliest finish time), interval partitioning (sweep-line or min-heap), and weighted intervals (dynamic programming).*

---

## 🎯 LEARNING OBJECTIVES

*By the end of this chapter, you will be able to:*

- 🎯 **Disambiguate** the 4 core interval problem archetypes instantly in interview settings.
- ⚙️ **Implement** production-grade solutions for Activity Selection and Meeting Rooms in C# (.NET 8/9 with `PriorityQueue`) and Python (3.11+ with `heapq`).
- ⚖️ **Explain** why sorting by start time or duration fails, while sorting by earliest finish time is provably optimal.
- 🧠 **Defend** interval selection using a clean, intuitive Exchange Argument.
- 🏭 **Architect** resource schedulers for calendar backends, cloud worker pools, and video ad slots.

---

## 📖 CHAPTER 1: CONTEXT & MOTIVATION

### The Engineering Challenge

You are building a resource scheduling service for a cloud platform. Customers submit compute jobs defined by an interval `[start, end)`.
Depending on the customer's tier, you face different operational objectives:

1. **Dedicated Worker (Single Machine):** Maximize the total count of non-overlapping jobs that can execute on one CPU core.
2. **Cluster Autoscaler:** Calculate the minimum number of worker nodes required to execute *all* incoming jobs without conflict.
3. **Revenue Maximization:** If each job pays a custom monetary rate, maximize total dollar payout.

If you attack all three with the same approach, you will fail.
- Objective 1 is **Activity Selection** -> Solved by **Greedy (Earliest Finish Time)** in `O(N log N)` time and `O(1)` auxiliary space.
- Objective 2 is **Interval Partitioning (Meeting Rooms II)** -> Solved by **Sweep-Line** or **Min-Heap** in `O(N log N)` time and `O(N)` space.
- Objective 3 is **Weighted Interval Scheduling** -> Greedy fails; requires **Dynamic Programming** with Binary Search in `O(N log N)` time and `O(N)` space.

```
THE INTERVAL DECISION MATRIX:
+-----------------------------------+-----------------------------------+
| Problem Archetype                 | Optimal Algorithmic Paradigm      |
+-----------------------------------+-----------------------------------+
| 1. Max Non-Overlapping Count      | Greedy (Sort by Finish Time)      |
| 2. Min Resource Allocation (Rooms)| Greedy Sweep-Line / Min-Heap      |
| 3. Merge Overlapping Ranges       | Two-Pointer Scan (Sort by Start)  |
| 4. Maximize Weighted Profit       | Dynamic Programming + Bisect      |
+-----------------------------------+-----------------------------------+
```

---

## 🧠 CHAPTER 2: THE GREEDY INTERVAL MENTAL MODEL

### Why Earliest Finish Time (EFT) Wins

Consider 3 candidate greedy rules for selecting the maximum number of non-overlapping intervals:

```
Candidate Rule 1: Earliest Start Time
Counterexample:
  Job A: [0 ---------------------------------------------------- 100]
  Job B:   [1 --- 3]
  Job C:              [4 --- 6]
  Rule 1 picks Job A first, blocking Jobs B and C. Optimal = 2, Rule 1 = 1. (FAILS)

Candidate Rule 2: Shortest Duration
Counterexample:
  Job A: [0 ---- 5]
  Job B:       [4 - 6]   <-- Shortest duration (length = 2)
  Job C:          [5 ---- 10]
  Rule 2 picks Job B, blocking both Job A and Job C. Optimal = 2, Rule 2 = 1. (FAILS)

Candidate Rule 3: Earliest Finish Time (WINNER)
Physical Intuition:
  By choosing the interval that finishes earliest, you leave the maximum possible
  unallocated timeline for future tasks. You minimize "opportunity cost".
```

### Visualizing the Timeline Sweep

```
Activities:
  A: [1, 4)
  B: [3, 5)
  C: [0, 6)
  D: [5, 7)
  E: [3, 9)
  F: [8, 11)

Timeline: 0  1  2  3  4  5  6  7  8  9  10 11
C:        [-----------)
A:           [--------)
B:                 [-----)
D:                       [-----)
E:                 [-----------------)
F:                                   [--------)

Greedy Pass Sorted by Finish Time:
1. Sort by finish: A(4), B(5), C(6), D(7), E(9), F(11)
2. Pick A(ends 4): Selected -> [A], LastEnd = 4
3. Check B(starts 3): 3 < 4 (Conflict) -> Skip
4. Check C(starts 0): 0 < 4 (Conflict) -> Skip
5. Check D(starts 5): 5 >= 4 (Compatible!) -> Selected -> [A, D], LastEnd = 7
6. Check E(starts 3): 3 < 7 (Conflict) -> Skip
7. Check F(starts 8): 8 >= 7 (Compatible!) -> Selected -> [A, D, F], LastEnd = 11

Final Selection: {A, D, F} -> Exactly 3 non-overlapping activities.
```

### The Simplified Exchange Argument

```
Exchange Proof for Activity Selection:
1. Let G = [g1, g2, ..., gk] be the greedy schedule sorted by finish time.
2. Let OPT = [o1, o2, ..., om] be any optimal schedule, sorted by finish time (m >= k).
3. If G == OPT, greedy is optimal.
4. If G != OPT, let the first difference occur at step 1:
   - By greedy design, g1 has the earliest finish time among ALL valid activities.
   - Therefore: Finish(g1) <= Finish(o1).
5. Substitute o1 with g1 in OPT:
   - Since g1 finishes at or before o1, g1 cannot conflict with o2, o3, ..., om.
   - The modified schedule OPT' = [g1, o2, ..., om] is still valid and has size m.
6. Repeat this substitution for all elements by induction.
   OPT can be transformed entirely into G without reducing the activity count.
   Therefore, k == m, proving G is globally optimal.
```

---

## ⚙️ CHAPTER 3: PRODUCTION IMPLEMENTATIONS

### Pattern 1: Activity Selection (Maximum Non-Overlapping Intervals)

#### Production C# (.NET 8/9)

```csharp
namespace DsaMastery.Intervals;

using System;
using System.Collections.Generic;

public readonly record struct Interval(int Start, int End);

public static class ActivitySelector
{
    /// <summary>
    /// Returns the maximum count of non-overlapping intervals.
    /// Time Complexity: O(N log N) dominated by sorting.
    /// Space Complexity: O(1) auxiliary (in-place sort on Array/Span).
    /// </summary>
    public static int MaxNonOverlappingCount(Interval[] intervals)
    {
        ArgumentNullException.ThrowIfNull(intervals);
        if (intervals.Length <= 1) return intervals.Length;

        // Sort by end time ascending; tie-break by start time ascending
        Array.Sort(intervals, static (a, b) =>
        {
            int cmp = a.End.CompareTo(b.End);
            return cmp != 0 ? cmp : a.Start.CompareTo(b.Start);
        });

        int count = 1;
        int lastEnd = intervals[0].End;

        for (int i = 1; i < intervals.Length; i++)
        {
            if (intervals[i].Start >= lastEnd)
            {
                count++;
                lastEnd = intervals[i].End;
            }
        }

        return count;
    }

    /// <summary>
    /// Returns the actual selected non-overlapping intervals.
    /// Space Complexity: O(N) for result list.
    /// </summary>
    public static List<Interval> SelectMaxActivities(Interval[] intervals)
    {
        ArgumentNullException.ThrowIfNull(intervals);
        if (intervals.Length == 0) return [];

        Array.Sort(intervals, static (a, b) => a.End.CompareTo(b.End));

        var selected = new List<Interval> { intervals[0] };
        int lastEnd = intervals[0].End;

        for (int i = 1; i < intervals.Length; i++)
        {
            if (intervals[i].Start >= lastEnd)
            {
                selected.Add(intervals[i]);
                lastEnd = intervals[i].End;
            }
        }

        return selected;
    }
}
```

#### Production Python (3.11+)

```python
from typing import List, Tuple

def max_non_overlapping_count(intervals: List[Tuple[int, int]]) -> int:
    """
    Computes maximum non-overlapping intervals count using earliest finish time greedy rule.
    Time Complexity: O(N log N)
    Space Complexity: O(1) auxiliary memory
    """
    if not intervals:
        return 0

    # Sort in-place by finish time ascending
    intervals.sort(key=lambda x: x[1])

    count = 1
    last_end = intervals[0][1]

    for start, end in intervals[1:]:
        if start >= last_end:
            count += 1
            last_end = end

    return count

def select_max_activities(intervals: List[Tuple[int, int]]) -> List[Tuple[int, int]]:
    """
    Returns the chosen subset of non-overlapping intervals.
    """
    if not intervals:
        return []

    sorted_intervals = sorted(intervals, key=lambda x: x[1])
    selected = [sorted_intervals[0]]
    last_end = sorted_intervals[0][1]

    for start, end in sorted_intervals[1:]:
        if start >= last_end:
            selected.append((start, end))
            last_end = end

    return selected
```

---

### Pattern 2: Meeting Rooms II (Interval Partitioning / Minimum Rooms)

**Problem Statement:**
Given an array of meeting time intervals `[[start_i, end_i]]`, find the minimum number of conference rooms required so that no two meetings overlap in the same room.

**Greedy Mechanics: Min-Heap vs Sweep-Line**
1. **Min-Heap Approach:**
   - Sort intervals by **start time** ascending.
   - Maintain a min-heap storing the `end` times of active rooms.
   - For each new meeting:
     - If `meeting.start >= heap.Peek()`: the earliest finishing room has freed up! Reuse it (`heap.Dequeue()`).
     - Otherwise: allocate a new room.
     - Enqueue `meeting.end` into the heap.
   - Peak size of the heap is the answer.
2. **Sweep-Line Approach:**
   - Split each interval into two events: `(start, +1)` and `(end, -1)`.
   - Sort events by timestamp. If timestamps match, process `end (-1)` before `start (+1)` so back-to-back meetings don't trigger a new room.
   - Track running concurrency; record the peak concurrency.

```
ASCII Room Allocation Timeline:
Meetings: M1 [1, 4), M2 [2, 5), M3 [5, 8), M4 [3, 6), M5 [7, 9)

Sorted by Start: M1[1,4), M2[2,5), M4[3,6), M3[5,8), M5[7,9)

Room 1: [ M1: 1-4 )        [ M3: 5-8 )
Room 2:    [ M2: 2-5 )                [ M5: 7-9 )
Room 3:       [ M4: 3-6 )

Min-Heap State Evolution:
Step 1 (M1 [1,4)): Heap = [4]                   (1 room)
Step 2 (M2 [2,5)): Start 2 < 4. Heap = [4, 5]   (2 rooms)
Step 3 (M4 [3,6)): Start 3 < 4. Heap = [4, 5, 6](3 rooms - Peak!)
Step 4 (M3 [5,8)): Start 5 >= 4. Reuse R1. Pop 4, Push 8. Heap = [5, 6, 8]
Step 5 (M5 [7,9)): Start 7 >= 5. Reuse R2. Pop 5, Push 9. Heap = [6, 8, 9]

Total Rooms Needed = 3
```

#### Production C# (.NET 8/9 with `PriorityQueue`)

```csharp
namespace DsaMastery.Intervals;

using System;
using System.Collections.Generic;

public static class MeetingRoomsScheduler
{
    /// <summary>
    /// Approach 1: Min-Heap of room end times using PriorityQueue.
    /// Time Complexity: O(N log N)
    /// Space Complexity: O(N) auxiliary memory for heap
    /// </summary>
    public static int MinMeetingRoomsHeap(Interval[] intervals)
    {
        ArgumentNullException.ThrowIfNull(intervals);
        if (intervals.Length <= 1) return intervals.Length;

        // Sort by start time ascending
        Array.Sort(intervals, static (a, b) => a.Start.CompareTo(b.Start));

        // PriorityQueue acts as a min-heap storing end times
        var roomEndHeap = new PriorityQueue<int, int>();
        roomEndHeap.Enqueue(intervals[0].End, intervals[0].End);

        for (int i = 1; i < intervals.Length; i++)
        {
            var current = intervals[i];
            
            // Check if earliest meeting has finished
            if (roomEndHeap.TryPeek(out int earliestEnd, out _) && current.Start >= earliestEnd)
            {
                roomEndHeap.Dequeue(); // Reuse room
            }

            roomEndHeap.Enqueue(current.End, current.End);
        }

        return roomEndHeap.Count;
    }

    /// <summary>
    /// Approach 2: Sweep-Line Event Sorting.
    /// Time Complexity: O(N log N)
    /// Space Complexity: O(N) auxiliary memory for events
    /// </summary>
    public static int MinMeetingRoomsSweepLine(Interval[] intervals)
    {
        ArgumentNullException.ThrowIfNull(intervals);
        if (intervals.Length <= 1) return intervals.Length;

        int n = intervals.Length;
        var events = new (int Time, int Delta)[2 * n];
        int idx = 0;

        foreach (var interval in intervals)
        {
            events[idx++] = (interval.Start, 1);  // Room opens
            events[idx++] = (interval.End, -1);   // Room frees
        }

        // Sort by Time ascending. If times match, -1 (end) comes before +1 (start)
        Array.Sort(events, static (a, b) =>
        {
            int cmp = a.Time.CompareTo(b.Time);
            return cmp != 0 ? cmp : a.Delta.CompareTo(b.Delta);
        });

        int activeRooms = 0;
        int maxRooms = 0;

        foreach (var evt in events)
        {
            activeRooms += evt.Delta;
            if (activeRooms > maxRooms)
            {
                maxRooms = activeRooms;
            }
        }

        return maxRooms;
    }
}
```

#### Production Python (3.11+ with `heapq`)

```python
import heapq
from typing import List, Tuple

def min_meeting_rooms_heap(intervals: List[Tuple[int, int]]) -> int:
    """
    Computes minimum meeting rooms using a min-heap of active room end times.
    Time Complexity: O(N log N)
    Space Complexity: O(N) for heap storage
    """
    if not intervals:
        return 0

    # Sort intervals by start time ascending
    sorted_intervals = sorted(intervals, key=lambda x: x[0])

    # Min-heap stores end times of ongoing meetings
    end_heap: List[int] = []
    heapq.heappush(end_heap, sorted_intervals[0][1])

    for start, end in sorted_intervals[1:]:
        # If earliest meeting ended before or at current start, reuse that room
        if start >= end_heap[0]:
            heapq.heappop(end_heap)

        # Allocate room (either reused or new)
        heapq.heappush(end_heap, end)

    return len(end_heap)

def min_meeting_rooms_sweep(intervals: List[Tuple[int, int]]) -> int:
    """
    Computes minimum meeting rooms using sweep-line event transformation.
    Time Complexity: O(N log N)
    Space Complexity: O(N) for event storage
    """
    events: List[Tuple[int, int]] = []
    for start, end in intervals:
        events.append((start, 1))   # Start meeting (+1 room)
        events.append((end, -1))    # End meeting (-1 room)

    # Sort by time. At matching time, -1 (end) precedes +1 (start)
    events.sort(key=lambda x: (x[0], x[1]))

    current_rooms = 0
    max_rooms = 0

    for _, delta in events:
        current_rooms += delta
        max_rooms = max(max_rooms, current_rooms)

    return max_rooms
```

---

## ⚖️ CHAPTER 4: PERFORMANCE & COMPLEXITY DECONSTRUCTION

### Complexity Comparison

| Problem | Primary Operation | Time Complexity | Auxiliary Space | Bottleneck |
| :--- | :--- | :--- | :--- | :--- |
| **Activity Selection** | Sort by End Time + Scan | `O(N log N)` | `O(1)` | Array sort comparisons |
| **Meeting Rooms II (Heap)** | Sort by Start + Heap Ops | `O(N log N)` | `O(N)` | Heapify & sift-down ops |
| **Meeting Rooms II (Sweep)** | Sort `2N` Events + Scan | `O(N log N)` | `O(N)` | 2N event sorting |
| **Weighted Interval (DP)** | Sort + Bisect + DP Array | `O(N log N)` | `O(N)` | Binary search per index |

#### Critical Engineering Realities:
- **Zero Allocations in C#:** For high-throughput schedulers, sort interval arrays in place via `Array.Sort` or `MemoryExtensions.Sort` using stack-allocated structs or spans to eliminate GC pressure.
- **Cache Locality:** Sweep-line traverses contiguous arrays linearly (`O(N)` cache hits), whereas heap structures jump memory pointers during sift-downs. On inputs with `N > 10^5`, sweep-line often beats heaps in raw wall-clock time despite identical Big-O.

---

## 🎙️ CHAPTER 5: 45-MINUTE INTERVIEW VERBAL SCRIPT

```
[00:00 - 05:00] Clarifying Interval Semantics
"Let's clarify the boundary semantics:
 1. Are intervals half-open [start, end) or closed [start, end]?
    If half-open, a meeting ending at 4 and another starting at 4 do NOT conflict.
 2. Can start and end times be negative or zero?
 3. Are intervals pre-sorted?
 For Activity Selection, our goal is to maximize the count of jobs on one machine.
 For Meeting Rooms, our goal is to minimize the total machines required for all jobs."

[05:00 - 12:00] Proving the Greedy Criterion
"Why does earliest finish time work for Activity Selection while earliest start fails?
 If we sort by start time, a single long job like [0, 100] could prevent 20 shorter jobs.
 If we sort by duration, [4, 6] could conflict with [0, 5] and [5, 10].
 Earliest finish time is optimal because it minimizes the remaining timeline used.
 An exchange argument formally confirms this: if any optimal solution picks a different
 first job, swapping it with our earliest finisher preserves validity without reducing size."

[12:00 - 25:00] Explaining the Architecture: Min-Heap vs Sweep-Line
"For Meeting Rooms II:
 Option A: Min-Heap. Sort by start time. Maintain a min-heap of active end times.
 If the next meeting starts after heap.Peek(), we pop and reuse that room.
 Otherwise, we push and expand the room count.
 
 Option B: Sweep-Line. Transform N intervals into 2N events (start = +1, end = -1).
 Sort events chronologically. If timestamps tie, end events (-1) MUST precede start (+1)
 so that a room is released before a new meeting claims it.
 I will implement the Min-Heap approach first, as it allows returning active room assignments."

[25:00 - 35:00] Clean Coding (Walk through the implementation)
"In C#, I'll use PriorityQueue<int, int> available in .NET 6+.
 In Python, I'll use heapq. Notice how we handle the empty input edge case..."

[35:00 - 42:00] Complexity Verification
"Time Complexity: Sorting takes O(N log N). In the heap approach, we do N heap insertions
 and at most N deletions. Total time is O(N log N).
 Space Complexity: The heap holds at most N elements, giving O(N) auxiliary space."

[42:00 - 45:00] Edge Cases & Defenses
"Edge cases covered:
 - Empty or single interval -> Return 0 or 1 immediately.
 - Identical intervals -> Stack concurrently, allocating separate rooms.
 - Back-to-back meetings [1, 4) and [4, 7) -> Valid reuse of Room 1 because 4 >= 4."
```

---

## ⚔️ SUPPLEMENTARY OUTCOMES

### 🏋️ Practice Problems

| # | Problem | Source | Difficulty | Key Concept |
| :--- | :--- | :--- | :--- | :--- |
| 1 | Non-overlapping Intervals | LeetCode 435 | Medium | Activity Selection (`N - max_count`) |
| 2 | Minimum Arrows to Burst Balloons | LeetCode 452 | Medium | Greedy coordinate overlap |
| 3 | Meeting Rooms II | LeetCode 253 | Medium | Min-heap / Sweep-line |
| 4 | Merge Intervals | LeetCode 56 | Medium | Two-pointer interval merging |
| 5 | Insert Interval | LeetCode 57 | Medium | Ordered insertion & compaction |
| 6 | Interval List Intersections | LeetCode 986 | Medium | Dual two-pointer sweep |
| 7 | Car Pooling | LeetCode 1094 | Medium | Difference array / capacity check |
| 8 | Employee Free Time | LeetCode 759 | Hard | Merge busy ranges, invert gaps |

### 🎙️ Interview Questions

1. **Q:** How do you convert LeetCode 435 (Non-overlapping Intervals) to Activity Selection?
   - *Answer:* The minimum intervals to remove equals `Total Intervals - Maximum Non-Overlapping Intervals`.
2. **Q:** What happens in sweep-line if start (+1) is sorted before end (-1) at the same timestamp?
   - *Answer:* It artificially spikes peak concurrency by 1, wrongly requiring an extra room for back-to-back meetings.
3. **Q:** Why does weighted interval scheduling require dynamic programming?
   - *Answer:* An interval with high profit might finish late, invalidating the greedy "earliest finish" advantage.

---

**End of Week 12 Day 02 Instructional File**

---
> 🧭 **Navigation:** [← Previous Day](Week_12_Day_01_Greedy_Fundamentals_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_12_Day_03_Huffman_Coding_And_Optimal_Trees_Instructional.md)
