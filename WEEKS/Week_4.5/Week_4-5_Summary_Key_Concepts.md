# 🗂️ WEEK 4.5 — SUMMARY & KEY CONCEPTS

**Quick Reference for Week 4.5 Critical Patterns**

---

## 📋 DAY-BY-DAY CONCEPT BREAKDOWN

### 📌 DAY 1: HASH MAP / HASH SET PATTERNS

**Core Concept:**
```
HashMap/HashSet = O(1) average lookup/insertion (O(n) worst case)
Trade-off: Space O(n) for Speed O(1)
```

**Key Operations:**
- Insert: `map[key] = value` → O(1)
- Lookup: `key in map` → O(1)
- Delete: `del map[key]` → O(1)

**Problem Types:**
- Two Sum, Valid Anagram, Group Anagrams
- Frequency counting, Duplicate detection
- LRU Cache foundation

**Interview Frequency:** 65% (Very High)

---

### 📌 DAY 2: MONOTONIC STACK

**Core Concept:**
```
Stack = LIFO + Ordering constraint
Monotonic = Maintain either increasing or decreasing order
```

**Key Operations:**
- While stack not empty AND top < current:
  - Pop, update answer
  - Push current

**Problem Types:**
- Next Greater Element, Previous Smaller Element
- Trapping Rain Water, Largest Rectangle in Histogram
- Daily Temperatures, Stock Span

**Interview Frequency:** 55% (High)

---

### 📌 DAY 3: MERGE OPERATIONS & INTERVALS

**Core Concept:**
```
Merge = Combine overlapping intervals
Sort first → iterate → merge overlaps
```

**Key Operations:**
- Sort by start time: O(n log n)
- Iterate and merge overlaps: O(n)
- Total: O(n log n)

**Problem Types:**
- Merge Intervals, Insert Interval
- Meeting Rooms, Calendar scheduling
- Interval scheduling optimization

**Interview Frequency:** 50% (Medium-High)

---

### 📌 DAY 4A: PARTITION & CYCLIC SORT

**Core Concept:**
```
Partition = Segregate elements based on predicate (O(n), O(1) space)
Cyclic Sort = Place elements in their "home" index (O(n), O(1) space)
```

**Key Operations:**
- DNF (3-way): `if val==0: swap(low), if val==2: swap(high), else: move mid`
- Cyclic: `if nums[i] != nums[nums[i]-1]: swap(nums[i], nums[nums[i]-1])`

**Problem Types:**
- Sort Colors (0, 1, 2), Move Zeroes
- First Missing Positive, Find Duplicates
- In-place segregation

**Interview Frequency:** 60% (High)

---

### 📌 DAY 4B: KADANE'S ALGORITHM

**Core Concept:**
```
Running Sum = max(current, running_sum + current)
If negative, reset. Track global max.
```

**Key Operations:**
- `current_sum = max(nums[i], current_sum + nums[i])`
- `global_max = max(global_max, current_sum)`

**Problem Types:**
- Maximum Subarray, Maximum Product Subarray
- Circular Subarray Maximum, Max Subarray K
- Financial analysis, signal processing

**Interview Frequency:** 60% (High)

---

### 📌 DAY 5: FAST & SLOW POINTERS

**Core Concept:**
```
Floyd's Cycle Detection = Two pointers at different speeds
Slow 1x, Fast 2x → Will meet in cycles
```

**Key Operations:**
- Cycle detection: while fast != slow → move
- Cycle start: reset slow to head, move both 1x → meet at start
- Midpoint: fast at end → slow at middle

**Problem Types:**
- Linked List Cycle, Cycle Start Detection
- Find Duplicate in Array, Middle of List
- Happy Number, Reorder List

**Interview Frequency:** 60% (High)

---

## 🗺️ CONCEPT MAP — HOW TOPICS CONNECT

```
         HashMap
           |
           ├──→ Frequency Counting
           |       |
           |       └──→ Monotonic Stack + HashMap
           |           (Next Greater Frequency)
           |
           └──→ Caching (LRU)
                   |
                   └──→ Linked List Ordering

      Sorting (Merging)
           |
           ├──→ Merge Intervals
           |       |
           |       └──→ Overlap Detection
           |
           └──→ Merge K Sorted Lists

    In-Place Modification
           |
           ├──→ Partition (DNF)
           ├──→ Cyclic Sort
           └──→ Kadane's (Running State)
                   |
                   └──→ DP Foundation

       Linked Lists
           |
           ├──→ Fast/Slow Pointers
           |       |
           |       └──→ Cycle Detection
           |       └──→ Midpoint Finding
           |       └──→ Reordering
           |
           └──→ Merging Lists
```

---

## 🎯 QUICK REFERENCE TABLE — ALL PATTERNS

| Pattern | Time | Space | Best For | Constraint |
|---------|------|-------|----------|-----------|
| **HashMap** | O(n) avg | O(n) | Frequency, Lookup | None |
| **Monotonic Stack** | O(n) | O(n) | Next/Prev Element | Linear scan |
| **Merge Intervals** | O(n log n) | O(n) or O(1) | Overlaps | Sorted input |
| **Partition (DNF)** | O(n) | O(1) | Segregation | In-place |
| **Cyclic Sort** | O(n) | O(1) | Missing/Duplicates | Dense range [1,n] |
| **Kadane** | O(n) | O(1) | Max Subarray | Contiguous |
| **Fast/Slow** | O(n) | O(1) | Cycles, Midpoint | Linked Lists |

---

## 💡 COMMON PROBLEM VARIATIONS

### HashMap
- Two Sum, Two Sum II (sorted array)
- Valid Anagram, Group Anagrams
- Isomorphic Strings, Majority Element
- LRU Cache, Frequency-based problems

### Monotonic Stack
- Next Greater Element, Previous Smaller
- Daily Temperatures, Stock Span
- Trapping Rain Water, Largest Rectangle

### Merge Intervals
- Merge Intervals, Insert Interval
- Meeting Rooms I/II, Overlapping Events

### Partition/Cyclic
- Sort Colors, Move Zeroes
- First Missing Positive, Find All Duplicates
- Segregate 0s, 1s, 2s

### Kadane
- Maximum Subarray, Maximum Product
- Circular Max, Max Profit (Stock)
- Best Time to Buy/Sell Stock variants

### Fast/Slow
- Linked List Cycle, Cycle Start
- Find Duplicate Number, Happy Number
- Middle of List, Palindrome List

---

## 🔍 DECISION TREE — WHICH PATTERN TO USE?

```
Is the problem about FINDING something in a collection?
├─ YES: Use HashMap/HashSet (frequency, existence)
└─ NO: Continue

Does it require NEXT/PREVIOUS relationships in order?
├─ YES: Use Monotonic Stack
└─ NO: Continue

Are there OVERLAPPING INTERVALS?
├─ YES: Use Merge Intervals
└─ NO: Continue

Is it asking for IN-PLACE segregation or sorting?
├─ YES: Use Partition (DNF) or Cyclic Sort
└─ NO: Continue

Is it asking for MAXIMUM/MINIMUM SUBARRAY?
├─ YES: Use Kadane's Algorithm
└─ NO: Continue

Is it about LINKED LISTS or CYCLES?
├─ YES: Use Fast/Slow Pointers
└─ NO: Might need multiple patterns or advanced techniques
```

---

## ⚡ QUICK IMPLEMENTATION TEMPLATES

### HashMap Counting
```python
count_map = {}
for item in items:
    count_map[item] = count_map.get(item, 0) + 1
```

### Monotonic Stack
```python
stack = []
for i, val in enumerate(nums):
    while stack and nums[stack[-1]] < val:
        pop_idx = stack.pop()
        # process
    stack.append(i)
```

### Merge Intervals
```python
intervals.sort()
merged = [intervals[0]]
for i in range(1, len(intervals)):
    if intervals[i][0] <= merged[-1][1]:
        merged[-1][1] = max(merged[-1][1], intervals[i][1])
    else:
        merged.append(intervals[i])
```

### Kadane's Algorithm
```python
curr_sum = nums[0]
max_sum = nums[0]
for i in range(1, len(nums)):
    curr_sum = max(nums[i], curr_sum + nums[i])
    max_sum = max(max_sum, curr_sum)
```

### Fast/Slow Cycle Detection
```python
slow = fast = head
while fast and fast.next:
    slow = slow.next
    fast = fast.next.next
    if slow == fast:
        return True  # Cycle detected
return False
```

---

## 📊 COMPLEXITY SUMMARY

| Pattern | Best | Avg | Worst | Space | Notes |
|---------|------|-----|-------|-------|-------|
| HashMap | O(1) | O(n) | O(n) | O(n) | Hash collisions worst case |
| Stack | O(n) | O(n) | O(n) | O(n) | Single pass through |
| Merge | O(n log n) | O(n log n) | O(n log n) | O(n) | Dominated by sort |
| Partition | O(n) | O(n) | O(n) | O(1) | One pass, in-place |
| Kadane | O(n) | O(n) | O(n) | O(1) | Linear scan only |
| Fast/Slow | O(n) | O(n) | O(n) | O(1) | Linked list traversal |

---

**End of Summary. Use this as a quick reference during problem-solving!**