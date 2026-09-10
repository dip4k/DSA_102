# Intervals & Line Sweep Mastery

## Summary Table

| Level | Mental Model | Pointer/State | Drill Problems |
| :--- | :--- | :--- | :--- |
| **1. Merge Intervals** | Sort by start, expand active interval | `prev` interval reference | [LeetCode 56: Merge Intervals] (Medium) |
| **2. Insert Interval** | 3 Phases: Left (keep), Middle (merge), Right (keep) | index `i` | [LeetCode 57: Insert Interval] (Medium) |
| **3. Sweep Line** | Chronological timeline of +1 (start) and -1 (end) events | `active_count`, `max_count` | [LeetCode 253: Meeting Rooms II] (Medium) |
| **4. Min Arrows** | Greedy: Sort by end time, shoot at earliest end | `last_arrow_pos` | [LeetCode 452: Minimum Number of Arrows to Burst Balloons] (Medium) |

---

## Level 1: Merge Intervals

- **What does the state/index mean?**: `i` is the current interval in a list sorted by start times. `prev` represents the accumulated overlapping region.
- **What region is processed?**: The continuous block of overlapping intervals processed so far.
- **What is the invariant/recurrence relation?**: If `curr.start <= prev.end`, they overlap -> `prev.end = max(prev.end, curr.end)`. Else, no overlap -> push `curr` and make it the new `prev`.

### Visual State Transitions

Intervals: `[[1,3], [2,6], [8,10], [15,18]]`

| Step | `curr` | `prev` in Result | Overlap? | Action | `result` State |
| :--- | :--- | :--- | :--- | :--- | :--- |
| Initial | - | - | - | Add first | `[[1,3]]` |
| `i=1` | `[2,6]` | `[1,3]` | Yes (2 <= 3) | Update `prev.end` | `[[1,6]]` |
| `i=2` | `[8,10]` | `[1,6]` | No (8 > 6) | Add `curr` | `[[1,6], [8,10]]` |
| `i=3` | `[15,18]` | `[8,10]` | No (15 > 10)| Add `curr` | `[[1,6], [8,10], [15,18]]` |

### Code Snippets

```python
# Problem: Given an array of intervals, merge all overlapping intervals and return an array of the non-overlapping intervals that cover all the intervals in the input.
def merge(intervals: list[list[int]]) -> list[list[int]]:
    # 1. Handle edge case of empty input
    if not intervals:
        return []
    
    # 2. Sort intervals by start time to process them sequentially
    intervals.sort(key=lambda x: x[0])
    
    # 3. Initialize result with the first interval
    res = [intervals[0]]
    
    # 4. Iterate through the remaining intervals
    for curr in intervals[1:]:
        # Reference the last interval added to the result
        prev = res[-1]
        
        # 5. Overlap condition: if current start is <= previous end
        if curr[0] <= prev[1]:
            # Expand the active interval's end to the maximum of both
            prev[1] = max(prev[1], curr[1])
        else:
            # No overlap: push current interval as the new active interval
            res.append(curr)
            
    return res
```

```csharp
// Problem: Given an array of intervals, merge all overlapping intervals and return an array of the non-overlapping intervals that cover all the intervals in the input.
public int[][] Merge(int[][] intervals) {
    // 1. Handle edge case of empty or null input
    if (intervals == null || intervals.Length == 0) return new int[0][];
    
    // 2. Sort intervals by start time to process them sequentially
    Array.Sort(intervals, (a, b) => a[0].CompareTo(b[0]));
    
    // 3. Initialize result with the first interval
    var res = new List<int[]> { intervals[0] };
    
    // 4. Iterate through the remaining intervals
    foreach (var curr in intervals.Skip(1)) {
        // Reference the last interval added to the result
        var prev = res.Last();
        
        // 5. Overlap condition: if current start is <= previous end
        if (curr[0] <= prev[1]) {
            // Expand the active interval's end to the maximum of both
            prev[1] = Math.Max(prev[1], curr[1]);
        } else {
            // No overlap: push current interval as the new active interval
            res.Add(curr);
        }
    }
    
    return res.ToArray();
}
```

### ⚠️ Gotchas & Pitfalls
- **Sorting Requirement**: Forgetting to sort the array initially.
- **Max End Time**: Doing `prev[1] = curr[1]` instead of `max(prev[1], curr[1])` (e.g., `[1,10]` and `[2,3]`).
- **In-place Modifications**: Removing elements from the input array while iterating is error-prone. Always build a new result array.

### Drill Problems
- `[LeetCode 56: Merge Intervals] (Medium)`

---

## Level 2: Insert Interval

- **What does the state/index mean?**: `i` scans original sorted intervals. The process handles 3 distinct phases based on relative position to `newInterval`.
- **What region is processed?**: The original sequence, partitioning into strict left, overlapping middle, strict right.
- **What is the invariant/recurrence relation?**: 
  - Phase 1: `curr.end < new.start` (strictly before).
  - Phase 2: Not Phase 1 and not Phase 3. Merge: `new = [min(curr.start, new.start), max(curr.end, new.end)]`.
  - Phase 3: `curr.start > new.end` (strictly after).

### Visual State Transitions

Intervals: `[[1,2], [3,5], [6,7], [8,10], [12,16]]`, New: `[4,8]`

| Phase | `curr` | Condition | Action / New Interval State | Result State |
| :--- | :--- | :--- | :--- | :--- |
| 1 (Left) | `[1,2]` | `2 < 4` | Add `[1,2]` | `[[1,2]]` |
| 2 (Merge) | `[3,5]` | Overlaps | `new = [min(3,4), max(5,8)]` -> `[3,8]` | `[[1,2]]` |
| 2 (Merge) | `[6,7]` | Overlaps | `new = [min(6,3), max(7,8)]` -> `[3,8]` | `[[1,2]]` |
| 2 (Merge) | `[8,10]` | Overlaps | `new = [min(8,3), max(10,8)]` -> `[3,10]`| `[[1,2]]` |
| End 2 | - | - | Add `new` `[3,10]` | `[[1,2], [3,10]]` |
| 3 (Right)| `[12,16]`| `12 > 10` | Add `[12,16]` | `[[1,2], [3,10], [12,16]]`|

### Code Snippets

```python
# Problem: Insert a new interval into a sorted array of non-overlapping intervals, merging any overlapping intervals to maintain the sorted, non-overlapping property.
def insert(intervals: list[list[int]], newInterval: list[int]) -> list[list[int]]:
    res = []
    i, n = 0, len(intervals)
    
    # 1. Phase 1: Left non-overlapping intervals
    # Add all intervals that end before the new interval starts
    while i < n and intervals[i][1] < newInterval[0]:
        res.append(intervals[i])
        i += 1
        
    # 2. Phase 2: Overlapping intervals
    # Merge all intervals that overlap with the new interval
    while i < n and intervals[i][0] <= newInterval[1]:
        # Expand the new interval to encompass overlapping bounds
        newInterval[0] = min(newInterval[0], intervals[i][0])
        newInterval[1] = max(newInterval[1], intervals[i][1])
        i += 1
    # Add the fully merged new interval
    res.append(newInterval)
    
    # 3. Phase 3: Right non-overlapping intervals
    # Add all remaining intervals that start after the new interval ends
    while i < n:
        res.append(intervals[i])
        i += 1
        
    return res
```

```csharp
// Problem: Insert a new interval into a sorted array of non-overlapping intervals, merging any overlapping intervals to maintain the sorted, non-overlapping property.
public int[][] Insert(int[][] intervals, int[] newInterval) {
    var res = new List<int[]>();
    int i = 0, n = intervals.Length;
    
    // 1. Phase 1: Left non-overlapping intervals
    // Add all intervals that end before the new interval starts
    while (i < n && intervals[i][1] < newInterval[0]) {
        res.Add(intervals[i++]);
    }
    
    // 2. Phase 2: Overlapping intervals
    // Merge all intervals that overlap with the new interval
    while (i < n && intervals[i][0] <= newInterval[1]) {
        // Expand the new interval to encompass overlapping bounds
        newInterval[0] = Math.Min(newInterval[0], intervals[i][0]);
        newInterval[1] = Math.Max(newInterval[1], intervals[i][1]);
        i++;
    }
    // Add the fully merged new interval
    res.Add(newInterval);
    
    // 3. Phase 3: Right non-overlapping intervals
    // Add all remaining intervals that start after the new interval ends
    while (i < n) {
        res.Add(intervals[i++]);
    }
    
    return res.ToArray();
}
```

### ⚠️ Gotchas & Pitfalls
- **Missing the `newInterval` Append**: Forgetting to add `newInterval` after completing Phase 2.
- **Empty Initial List**: Handled correctly if logic depends entirely on `while i < n`.
- **Condition Mix-ups**: Using `<` instead of `<=` when detecting overlapping bounds.

### Drill Problems
- `[LeetCode 57: Insert Interval] (Medium)`

---

## Level 3: Meeting Rooms (Sweep Line)

- **What does the state/index mean?**: Iterating through chronological events (points in time). `state` tracks active overlapping intervals.
- **What region is processed?**: The timeline from earliest start to latest end.
- **What is the invariant/recurrence relation?**: `active += 1` for start events. `active -= 1` for end events. Requires sorting events.

### Visual State Transitions

Intervals: `[[0, 30], [5, 10], [15, 20]]`
Events: `(0, +1), (30, -1), (5, +1), (10, -1), (15, +1), (20, -1)`
Sorted: `(0, +1), (5, +1), (10, -1), (15, +1), (20, -1), (30, -1)`

| Time | Event Type | `active_count` | `max_count` |
| :--- | :--- | :--- | :--- |
| 0 | Start (+1) | 1 | 1 |
| 5 | Start (+1) | 2 | 2 |
| 10 | End (-1) | 1 | 2 |
| 15 | Start (+1) | 2 | 2 |
| 20 | End (-1) | 1 | 2 |
| 30 | End (-1) | 0 | 2 |

### Code Snippets

```python
# Problem: Given an array of meeting time intervals, find the minimum number of conference rooms required by tracking concurrent meetings.
def minMeetingRooms(intervals: list[list[int]]) -> int:
    events = []
    
    # 1. Create chronological events for start (+1) and end (-1) of meetings
    for start, end in intervals:
        events.append((start, 1))
        events.append((end, -1))
        
    # 2. Sort events by time. If tie, process end (-1) before start (1)
    # This ensures a room is freed before allocating a new one at the exact same time
    events.sort(key=lambda x: (x[0], x[1]))
    
    max_rooms = active_rooms = 0
    
    # 3. Sweep line: Iterate through sorted events
    for time, cost in events:
        # Update current active overlapping intervals
        active_rooms += cost
        # Track the maximum concurrent overlap seen so far
        max_rooms = max(max_rooms, active_rooms)
        
    return max_rooms
```

```csharp
// Problem: Given an array of meeting time intervals, find the minimum number of conference rooms required by tracking concurrent meetings.
public int MinMeetingRooms(int[][] intervals) {
    var events = new List<(int time, int cost)>();
    
    // 1. Create chronological events for start (+1) and end (-1) of meetings
    foreach (var interval in intervals) {
        events.Add((interval[0], 1));
        events.Add((interval[1], -1));
    }
    
    // 2. Sort events by time. If tie, process end (-1) before start (1)
    // This ensures a room is freed before allocating a new one at the exact same time
    events.Sort((a, b) => {
        if (a.time == b.time) return a.cost.CompareTo(b.cost);
        return a.time.CompareTo(b.time);
    });
    
    int maxRooms = 0, activeRooms = 0;
    
    // 3. Sweep line: Iterate through sorted events
    foreach (var ev in events) {
        // Update current active overlapping intervals
        activeRooms += ev.cost;
        // Track the maximum concurrent overlap seen so far
        maxRooms = Math.Max(maxRooms, activeRooms);
    }
    
    return maxRooms;
}
```

### ⚠️ Gotchas & Pitfalls
- **Tie-breaker Rules**: If an interval ends at `T` and another starts at `T`, the end event MUST be processed first. (Hence `-1` sorts before `1`).
- **Memory Overhead**: Creating an explicit events array doubles space (O(N)). Alternatively, sort two separate arrays (starts, ends) and two-pointer iterate.

### Drill Problems
- `[LeetCode 253: Meeting Rooms II] (Medium)`

---

## Level 4: Minimum Number of Arrows

- **What does the state/index mean?**: Scanning intervals sorted by **END** time. `last_arrow` tracks the coordinate of the most recently fired arrow.
- **What region is processed?**: The intersection of the current set of overlapping intervals.
- **What is the invariant/recurrence relation?**: Shoot at `curr.end`. If next interval `start > last_arrow`, it cannot be popped by the current arrow. Shoot a new one.

### Visual State Transitions

Intervals: `[[10,16], [2,8], [1,6], [7,12]]`
Sorted by END: `[[1,6], [2,8], [7,12], [10,16]]`

| `curr` Interval | `last_arrow` | Condition: `start > last_arrow` | Action | `arrows` Count |
| :--- | :--- | :--- | :--- | :--- |
| `[1,6]` | `-∞` | Yes (1 > -∞) | Shoot at 6, `last_arrow = 6` | 1 |
| `[2,8]` | `6` | No (2 <= 6) | Skip (already popped) | 1 |
| `[7,12]` | `6` | Yes (7 > 6) | Shoot at 12, `last_arrow = 12` | 2 |
| `[10,16]`| `12` | No (10 <= 12)| Skip (already popped) | 2 |

### Code Snippets

```python
# Problem: Find the minimum number of arrows required to burst all balloons, where an arrow shot at x bursts all balloons that span across x.
def findMinArrowShots(points: list[list[int]]) -> int:
    if not points:
        return 0
        
    # 1. Sort intervals strictly by their end time
    points.sort(key=lambda x: x[1])
    
    # 2. Initialize state: one arrow fired at the first interval's end
    arrows = 1
    last_arrow = points[0][1]
    
    # 3. Scan remaining intervals
    for start, end in points[1:]:
        # 4. If current interval starts after the last arrow, it's not burst
        if start > last_arrow:
            # Shoot a new arrow
            arrows += 1
            # Place the new arrow at the current interval's end for max coverage
            last_arrow = end
            
    return arrows
```

```csharp
// Problem: Find the minimum number of arrows required to burst all balloons, where an arrow shot at x bursts all balloons that span across x.
public int FindMinArrowShots(int[][] points) {
    if (points == null || points.Length == 0) return 0;
    
    // 1. Sort intervals strictly by their end time, avoiding integer overflow
    Array.Sort(points, (a, b) => a[1].CompareTo(b[1]));
    
    // 2. Initialize state: one arrow fired at the first interval's end
    int arrows = 1;
    long lastArrow = points[0][1];
    
    // 3. Scan remaining intervals
    for (int i = 1; i < points.Length; i++) {
        // 4. If current interval starts after the last arrow, it's not burst
        if (points[i][0] > lastArrow) {
            // Shoot a new arrow
            arrows++;
            // Place the new arrow at the current interval's end for max coverage
            lastArrow = points[i][1];
        }
    }
    
    return arrows;
}
```

### ⚠️ Gotchas & Pitfalls
- **Sorting by End Time vs Start Time**: Greedy problems often require sorting by END time to optimally clear the maximum overlap quickly.
- **Integer Overflow**: In C# and Java, doing `a[1] - b[1]` in the comparator can overflow if bounds are `INT_MIN` and `INT_MAX`. Use `.CompareTo()` or long arithmetic.
- **Overlap Definition**: Unlike standard overlap, `[1,2]` and `[2,3]` overlap at `2` and CAN be burst with 1 arrow (i.e. strictly greater `>` check for new arrow).

### Drill Problems
- `[LeetCode 452: Minimum Number of Arrows to Burst Balloons] (Medium)`
