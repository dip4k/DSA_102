# Week 05 Extended Python Complete v13

Purpose: build Python fluency for the highest-frequency interview patterns: hash maps, monotonic stack, intervals, partition/cyclic sort, Kadane, and fast/slow pointers.

## Focus tags
- Must: hash counting, monotonic stack, merge intervals, Kadane, Floyd cycle detection
- Should: cyclic sort, interval insertion, stock span, histogram area
- Optional: heap-based merge and pattern-composition drills

## Pattern 1: hash-map complement lookup
```python
def two_sum(nums, target):
    seen = {}
    for i, value in enumerate(nums):
        need = target - value
        if need in seen:
            return [seen[need], i]
        seen[value] = i
    return []
```

## Pattern 2: monotonic stack
```python
def next_greater(nums):
    ans = [-1] * len(nums)
    stack = []  # indices
    for i, value in enumerate(nums):
        while stack and nums[stack[-1]] < value:
            ans[stack.pop()] = value
        stack.append(i)
    return ans
```

Invariant:
- indices in stack remain monotonic by value
- when an index pops, its next greater element is finalized

## Pattern 3: merge intervals
```python
def merge_intervals(intervals):
    if not intervals:
        return []
    intervals = sorted(intervals)
    merged = [intervals[0][:]]
    for start, end in intervals[1:]:
        if start <= merged[-1][1]:
            merged[-1][1] = max(merged[-1][1], end)
        else:
            merged.append([start, end])
    return merged
```

## Pattern 4: partition / Dutch national flag
```python
def sort_colors(nums):
    low = mid = 0
    high = len(nums) - 1
    while mid <= high:
        if nums[mid] == 0:
            nums[low], nums[mid] = nums[mid], nums[low]
            low += 1
            mid += 1
        elif nums[mid] == 2:
            nums[mid], nums[high] = nums[high], nums[mid]
            high -= 1
        else:
            mid += 1
```

## Pattern 5: Kadane
```python
def max_subarray(nums):
    best = current = nums[0]
    for value in nums[1:]:
        current = max(value, current + value)
        best = max(best, current)
    return best
```

## Pattern 6: fast/slow pointers
```python
def has_cycle(head):
    slow = fast = head
    while fast and fast.next:
        slow = slow.next
        fast = fast.next.next
        if slow is fast:
            return True
    return False
```

## Quick practice ladder
- Must: two sum, valid anagram, daily temperatures, merge intervals, maximum subarray, linked-list cycle
- Should: top-k frequent, largest rectangle in histogram, insert interval, sort colors, find duplicate number
- Optional: merge k sorted lists, stock span, cycle start proof trace

## Common Python pitfalls
- Storing stack values instead of indices in boundary problems.
- Forgetting to sort intervals before merging.
- Using list operations that accidentally make a supposedly O(n) solution quadratic.
- Confusing value equality with node identity in fast/slow linked-list logic.
