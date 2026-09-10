# Greedy Traversal Mastery Curriculum v1

## Summary Table

| Level | Mental Model | Pointer/State | Drill Problems |
| --- | --- | --- | --- |
| **1. Single-pass Choice** | Local optimum unconditionally leads to global optimum. | `current_state`, `max/min_so_far` | [LeetCode 55: Jump Game] (Medium)<br>[LeetCode 121: Best Time to Buy and Sell Stock] (Easy) |
| **2. Sorting-based (Intervals)** | Order by start/end to resolve conflicts linearly. | `last_interval_end`, `current_interval` | [LeetCode 56: Merge Intervals] (Medium)<br>[LeetCode 435: Non-overlapping Intervals] (Medium) |
| **3. PQ/Heap Greedy** | Dynamically select/replace the best available option over time. | `max_heap` or `min_heap` for active choices | [LeetCode 253: Meeting Rooms II] (Medium)<br>[LeetCode 871: Min Number of Refueling Stops] (Hard) |
| **4. Two-pointer Greedy** | Converge from ends to maximize/minimize paired conditions. | `left`, `right` indices | [LeetCode 11: Container With Most Water] (Medium)<br>[LeetCode 881: Boats to Save People] (Medium) |

---

## Level 1: Single-pass Choice

### Mental Models & Invariants
*   **What does the state/index mean?**: Represents the cumulative capability or maximum potential reached up to index `i`.
*   **What region is processed?**: The prefix of the array up to index `i`.
*   **What is the invariant?**: At any step `i`, the state holds the absolute optimal boundary or value achievable using elements from `0` to `i`.

### Visual State Transitions (Jump Game)
Target: `nums = [2, 3, 1, 1, 4]`

| Step | Choice/Action | State (`max_reach`) | Invariant |
| :--- | :--- | :--- | :--- |
| `i=0, val=2` | `max(0, 0 + 2)` | `2` | Max reach from index 0 is 2. |
| `i=1, val=3` | `max(2, 1 + 3)` | `4` | Max reach from indices 0..1 is 4. |
| `i=2, val=1` | `max(4, 2 + 1)` | `4` | Max reach from indices 0..2 is 4. |
| `i=3, val=1` | `max(4, 3 + 1)` | `4` | Max reach from indices 0..3 is 4. |
| `i=4, val=4` | `max(4, 4 + 4)` | `8` (Target Reached) | Max reach >= last index, valid path exists. |

### Code Snippets

Problem: Determine if you can reach the last index of an array where each element represents your maximum jump length at that position.

```python
# Python - Jump Game
def canJump(nums: list[int]) -> bool:
    # 1. Initialize state: maximum reachable index so far
    max_reach = 0
    
    # 2. Iterate through the array to update maximum potential
    for i in range(len(nums)):
        # 3. Check invariant: if current index is unreachable, we cannot proceed
        if i > max_reach:
            return False
            
        # 4. Update state: absolute optimal boundary achievable from elements 0 to i
        max_reach = max(max_reach, i + nums[i])
        
        # 5. Early exit if the end is reachable
        if max_reach >= len(nums) - 1:
            return True
            
    return True
```

Problem: Determine if you can reach the last index of an array where each element represents your maximum jump length at that position.

```csharp
// C# - Jump Game
public bool CanJump(int[] nums) {
    // 1. Initialize state: maximum reachable index so far
    int maxReach = 0;
    
    // 2. Iterate through the array to update maximum potential
    for (int i = 0; i < nums.Length; i++) {
        // 3. Check invariant: if current index is unreachable, we cannot proceed
        if (i > maxReach) return false;
        
        // 4. Update state: absolute optimal boundary achievable from elements 0 to i
        maxReach = Math.Max(maxReach, i + nums[i]);
        
        // 5. Early exit if the end is reachable
        if (maxReach >= nums.Length - 1) return true;
    }
    
    return true;
}
```

### ⚠️ Gotchas & Pitfalls
*   **Unreachable State**: Forgetting to check if the current index `i` is actually reachable (e.g., `i > max_reach`) before updating states.
*   **Premature Optimization**: Returning false immediately on a `0` value without checking if `max_reach` can bypass it.

### Drill Problems
*   [LeetCode 55: Jump Game] (Medium)
*   [LeetCode 121: Best Time to Buy and Sell Stock] (Easy)

---

## Level 2: Sorting-based Greedy (Intervals)

### Mental Models & Invariants
*   **What does the state/index mean?**: Represents the strict boundary (usually `end_time`) of the most recently accepted choice.
*   **What region is processed?**: Sorted sequence of intervals up to index `i`.
*   **What is the invariant?**: The accepted sequence up to `i` forms a valid, non-overlapping set, maximizing the count of intervals or minimizing bounds.

### Visual State Transitions (Non-overlapping Intervals)
Target: `intervals = [[1,2], [2,3], [3,4], [1,3]]`
*Sorted by end time:* `[[1,2], [1,3], [2,3], [3,4]]`

| Step (Interval) | Choice/Action | State (`last_end`, `count`) | Invariant |
| :--- | :--- | :--- | :--- |
| `Init` | Start | `-inf`, `0` | 0 valid intervals chosen. |
| `[1, 2]` | Keep (1 >= -inf) | `2`, `1` | Max intervals up to end=2 is 1. |
| `[1, 3]` | Drop (1 < 2) | `2`, `1` | Interval overlaps, drop to keep `end` minimal. |
| `[2, 3]` | Keep (2 >= 2) | `3`, `2` | Max intervals up to end=3 is 2. |
| `[3, 4]` | Keep (3 >= 3) | `4`, `3` | Max intervals up to end=4 is 3. |

### Code Snippets

Problem: Find the minimum number of intervals you need to remove to make the rest of the intervals non-overlapping.

```python
# Python - Non-overlapping Intervals
def eraseOverlapIntervals(intervals: list[list[int]]) -> int:
    if not intervals: return 0
    
    # 1. Sort by end time to resolve conflicts linearly and leave max room
    intervals.sort(key=lambda x: x[1])
    
    # 2. Initialize state: track end of the last accepted interval
    last_end = float('-inf')
    keep_count = 0
    
    # 3. Process each interval to greedily build the non-overlapping set
    for start, end in intervals:
        # 4. If interval doesn't overlap, it's safe to keep
        if start >= last_end:
            keep_count += 1
            # 5. Maintain invariant: update boundary of most recently accepted choice
            last_end = end
            
    # 6. Total intervals minus the maximum we can keep gives minimum to remove
    return len(intervals) - keep_count
```

Problem: Find the minimum number of intervals you need to remove to make the rest of the intervals non-overlapping.

```csharp
// C# - Non-overlapping Intervals
public int EraseOverlapIntervals(int[][] intervals) {
    if (intervals.Length == 0) return 0;
    
    // 1. Sort by end time to resolve conflicts linearly and leave max room
    Array.Sort(intervals, (a, b) => a[1].CompareTo(b[1]));
    
    // 2. Initialize state: track end of the last accepted interval
    int lastEnd = int.MinValue;
    int keepCount = 0;
    
    // 3. Process each interval to greedily build the non-overlapping set
    foreach (var interval in intervals) {
        // 4. If interval doesn't overlap, it's safe to keep
        if (interval[0] >= lastEnd) {
            keepCount++;
            // 5. Maintain invariant: update boundary of most recently accepted choice
            lastEnd = interval[1];
        }
    }
    
    // 6. Total intervals minus the maximum we can keep gives minimum to remove
    return intervals.Length - keepCount;
}
```

### ⚠️ Gotchas & Pitfalls
*   **Sorting by the wrong attribute**: Sorting by `start` time when you need to maximize non-overlapping intervals (always sort by `end` time to leave maximum room for future intervals).
*   **Inclusive vs. Exclusive bounds**: Mishandling whether `[1,2]` and `[2,3]` overlap. Read problem constraints closely to see if `start == last_end` is valid.

### Drill Problems
*   [LeetCode 56: Merge Intervals] (Medium)
*   [LeetCode 435: Non-overlapping Intervals] (Medium)

---

## Level 3: Priority Queue/Heap Greedy

### Mental Models & Invariants
*   **What does the state/index mean?**: The heap represents the "active" or "deferred" optimal choices we can commit to or swap out.
*   **What region is processed?**: A dynamically changing window or set of available items over time.
*   **What is the invariant?**: The heap always maintains the absolute best components (cheapest, earliest ending, most fuel) required to satisfy the constraints up to the current element.

### Visual State Transitions (Meeting Rooms II)
Target: `intervals = [[0,30], [5,10], [15,20]]`
*Sorted by start time:* `[[0,30], [5,10], [15,20]]`

| Step (Interval) | Choice/Action | State (Min Heap of Ends) | Invariant |
| :--- | :--- | :--- | :--- |
| `[0, 30]` | Allocate new room | `[30]` | 1 room needed, earliest free at 30. |
| `[5, 10]` | Overlaps (5 < 30). Alloc new room | `[10, 30]` | 2 rooms needed, earliest free at 10. |
| `[15, 20]` | Frees room (15 >= 10). Reuse room | `[20, 30]` | 2 rooms needed, earliest free at 20. |

### Code Snippets

Problem: Given an array of meeting time intervals consisting of start and end times, find the minimum number of conference rooms required.

```python
# Python - Meeting Rooms II
import heapq

def minMeetingRooms(intervals: list[list[int]]) -> int:
    if not intervals: return 0
    
    # 1. Sort meetings by start time to process them chronologically
    intervals.sort(key=lambda x: x[0])
    
    # 2. State representation: min-heap storing the end times of active meetings
    free_rooms = []
    
    for start, end in intervals:
        # 3. Maintain invariant: if earliest ending meeting is over, remove it (free the room)
        if free_rooms and free_rooms[0] <= start:
            heapq.heappop(free_rooms)
            
        # 4. Allocate a room for the current meeting (either a new one, or reusing the freed one)
        heapq.heappush(free_rooms, end)
        
    # 5. The number of rooms needed corresponds to the peak size of the min-heap
    return len(free_rooms)
```

Problem: Given an array of meeting time intervals consisting of start and end times, find the minimum number of conference rooms required.

```csharp
// C# - Meeting Rooms II
public int MinMeetingRooms(int[][] intervals) {
    if (intervals.Length == 0) return 0;
    
    // 1. Sort meetings by start time to process them chronologically
    Array.Sort(intervals, (a, b) => a[0].CompareTo(b[0]));
    
    // 2. State representation: priority queue (min-heap) storing the end times of active meetings
    PriorityQueue<int, int> freeRooms = new PriorityQueue<int, int>();
    
    foreach (var interval in intervals) {
        // 3. Maintain invariant: if earliest ending meeting is over, remove it (free the room)
        if (freeRooms.Count > 0 && freeRooms.Peek() <= interval[0]) {
            freeRooms.Dequeue();
        }
        
        // 4. Allocate a room for the current meeting (either a new one, or reusing the freed one)
        freeRooms.Enqueue(interval[1], interval[1]);
    }
    
    // 5. The number of rooms needed corresponds to the peak size of the min-heap
    return freeRooms.Count;
}
```

### ⚠️ Gotchas & Pitfalls
*   **Forgetting Pre-sorting**: Processing elements with a heap without sorting them by time/dependency first leads to state invalidation.
*   **Stale Data in Heap**: Modifying heap elements in place without popping and pushing destroys the heap invariant.

### Drill Problems
*   [LeetCode 253: Meeting Rooms II] (Medium)
*   [LeetCode 871: Minimum Number of Refueling Stops] (Hard)

---

## Level 4: Two-pointer Greedy

### Mental Models & Invariants
*   **What does the state/index mean?**: The bounding indices `left` and `right` represent the remaining untraversed search space.
*   **What region is processed?**: The elements implicitly discarded outside of the `[left, right]` boundary.
*   **What is the invariant?**: Discarding the limiting pointer (e.g., the shorter line) eliminates ONLY suboptimal pairs, ensuring the global optimum remains in `[left, right]`.

### Visual State Transitions (Container with Most Water)
Target: `height = [1, 8, 6, 2, 5, 4, 8, 3, 7]`

| Step | Choice/Action | State (`L`, `R`, `max_area`) | Invariant |
| :--- | :--- | :--- | :--- |
| `L=0(1), R=8(7)` | Area=1*8=8. Move `L` (1 < 7) | `L=1, R=8, max=8` | Max area bounded by index 0 is found; discard 0. |
| `L=1(8), R=8(7)` | Area=7*7=49. Move `R` (8 > 7)| `L=1, R=7, max=49`| Max area bounded by index 8 is found; discard 8. |
| `L=1(8), R=7(3)` | Area=3*6=18. Move `R` (8 > 3)| `L=1, R=6, max=49`| Max area bounded by index 7 is found; discard 7. |

### Code Snippets

Problem: Find two lines that together with the x-axis form a container, such that the container contains the most water.

```python
# Python - Container With Most Water
def maxArea(height: list[int]) -> int:
    # 1. Initialize two pointers to frame the maximum possible width
    left, right = 0, len(height) - 1
    max_area = 0
    
    # 2. Converge from ends to untraversed search space
    while left < right:
        width = right - left
        h_left, h_right = height[left], height[right]
        
        # 3. Calculate area and conditionally update the maximum area so far
        current_area = min(h_left, h_right) * width
        max_area = max(max_area, current_area)
        
        # 4. Maintain invariant: safely discard the limiting pointer since it cannot yield a larger area
        if h_left < h_right:
            left += 1
        else:
            right -= 1
            
    return max_area
```

Problem: Find two lines that together with the x-axis form a container, such that the container contains the most water.

```csharp
// C# - Container With Most Water
public int MaxArea(int[] height) {
    // 1. Initialize two pointers to frame the maximum possible width
    int left = 0;
    int right = height.Length - 1;
    int maxArea = 0;
    
    // 2. Converge from ends to untraversed search space
    while (left < right) {
        int width = right - left;
        int hLeft = height[left];
        int hRight = height[right];
        
        // 3. Calculate area and conditionally update the maximum area so far
        int currentArea = Math.Min(hLeft, hRight) * width;
        maxArea = Math.Max(maxArea, currentArea);
        
        // 4. Maintain invariant: safely discard the limiting pointer since it cannot yield a larger area
        if (hLeft < hRight) {
            left++;
        } else {
            right--;
        }
    }
    
    return maxArea;
}
```

### ⚠️ Gotchas & Pitfalls
*   **Moving the wrong pointer**: Discarding the non-bottleneck pointer guarantees you miss potential larger areas. Always move the restrictive/worse pointer.
*   **Infinite loops on ties**: Failing to handle `h_left == h_right` correctly. It is safe to move either or both, but doing nothing will hang the loop.

### Drill Problems
*   [LeetCode 11: Container With Most Water] (Medium)
*   [LeetCode 881: Boats to Save People] (Medium)
