# Week 04 Extended Python Complete v13

Purpose: build Python fluency for core sequence patterns: two pointers, sliding windows, divide and conquer, and binary search as a pattern.

## Focus tags
- Must: two pointers, fixed/variable windows, binary-search invariants
- Should: divide-and-conquer traces, answer-space binary search
- Optional: monotonic feasibility checks and proof-style explanations

## Python mindset for Week 04
- Python makes pointer-style array work easy because indexing is concise.
- Sliding-window code must still state invariants explicitly; short code is not the same as clear logic.
- Binary search should be written around interval meaning, not memorized branch snippets.

## Pattern 1: opposite-direction two pointers
```python
def two_sum_sorted(nums, target):
    left, right = 0, len(nums) - 1
    while left < right:
        total = nums[left] + nums[right]
        if total == target:
            return [left, right]
        if total < target:
            left += 1
        else:
            right -= 1
    return []
```

Invariant:
- if a solution exists, it remains inside `[left, right]`
- everything outside has already been ruled out

## Pattern 2: same-direction read/write pointers
```python
def remove_duplicates_sorted(nums):
    if not nums:
        return 0
    write = 1
    for read in range(1, len(nums)):
        if nums[read] != nums[write - 1]:
            nums[write] = nums[read]
            write += 1
    return write
```

## Pattern 3: fixed-size sliding window
```python
def max_sum_k(nums, k):
    if k <= 0 or k > len(nums):
        return 0
    window = sum(nums[:k])
    best = window
    for i in range(k, len(nums)):
        window += nums[i] - nums[i - k]
        best = max(best, window)
    return best
```

## Pattern 4: variable-size sliding window
```python
def longest_no_repeat(s):
    seen = {}
    left = 0
    best = 0
    for right, ch in enumerate(s):
        if ch in seen and seen[ch] >= left:
            left = seen[ch] + 1
        seen[ch] = right
        best = max(best, right - left + 1)
    return best
```

Invariant:
- current window `s[left:right+1]` has no duplicates

## Pattern 5: divide and conquer
```python
def merge_count(left, right):
    out = []
    i = j = 0
    while i < len(left) and j < len(right):
        if left[i] <= right[j]:
            out.append(left[i])
            i += 1
        else:
            out.append(right[j])
            j += 1
    out.extend(left[i:])
    out.extend(right[j:])
    return out
```

Use this mental model for merge-sort-style combine steps and inversion counting.

## Pattern 6: binary search on answer
```python
def min_capacity(weights, days):
    def can(cap):
        used_days = 1
        current = 0
        for w in weights:
            if current + w > cap:
                used_days += 1
                current = 0
            current += w
        return used_days <= days

    left, right = max(weights), sum(weights)
    while left < right:
        mid = (left + right) // 2
        if can(mid):
            right = mid
        else:
            left = mid + 1
    return left
```

## Quick practice ladder
- Must: two-sum sorted, remove duplicates, max average subarray, longest substring without repeating, minimum size subarray sum
- Should: find all anagrams, minimum window substring, count inversions, ship packages in D days
- Optional: maximize minimum distance and other answer-space search problems

## Common Python pitfalls
- Forgetting that variable windows need both expansion and shrinking state updates.
- Using binary search without stating what `left` and `right` mean.
- Applying two pointers to unsorted data without first justifying it.
