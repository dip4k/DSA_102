# 🧭 WEEK 5 PROBLEM-SOLVING ROADMAP & PATTERN TEMPLATES

**Week 5: From Simple to Complex — Problem Progression Guide**

---

## 🎯 Overall Strategy: Three-Stage Mastery

```
Stage 1: Foundation (Days 1-3)
├─ Understand each pattern in isolation
├─ Trace examples by hand
└─ Solve straightforward problems

Stage 2: Application (Days 3-4)
├─ Recognize patterns in variations
├─ Combine with basic techniques
└─ Handle moderate constraints

Stage 3: Integration (Days 5 & Beyond)
├─ Mix multiple patterns in one problem
├─ Adapt patterns to novel contexts
└─ Optimize for hidden constraints
```

---

## 🪜 STAGE 1: SIMPLE → DIRECT APPLICATION

### Week 5 Day 1: Hash Maps (Foundation)

**Progression:**

1. **Warm-up:** Two Sum (Find two numbers summing to target)
   - **Key:** Use hash to store complements
   - **Constraint:** None yet
   - **Trace:** Walk through with `[2, 7, 11, 15]`, target = 9

2. **Build Intuition:** Valid Anagram
   - **Key:** Frequency counting with hash
   - **Constraint:** Same characters, different order
   - **Trace:** Compare `"listen"` vs `"silent"`

3. **Extend:** Majority Element
   - **Key:** Frequency threshold (> n/2)
   - **Constraint:** Guaranteed to exist
   - **Trace:** `[3, 2, 3]` → 3 appears 2 times (> 3/2)

**Checkpoint 1:** After these 3, you understand "Hash as a frequency/lookup tool".

---

### Week 5 Day 2: Monotonic Stack (Foundation)

**Progression:**

1. **Warm-up:** Next Greater Element I
   - **Key:** Monotonic decreasing stack
   - **Constraint:** Linear scan, single pass
   - **Trace:** `[1, 2, 1]` → `[2, -1, -1]`

2. **Build Intuition:** Daily Temperatures
   - **Key:** Same as Next Greater, but return days instead of value
   - **Constraint:** Realistic context (temperatures)
   - **Trace:** `[73, 74, 75]` → `[1, 1, 0]` (cooler follows)

3. **Extend (Harder):** Largest Rectangle in Histogram
   - **Key:** Next smaller (left & right), then compute area
   - **Constraint:** 2D thinking (height & width)
   - **Trace:** `[2, 1, 5, 6, 2, 3]` → Area = 10 (for heights 5 & 6)

**Checkpoint 2:** After these 3, you understand "Monotonic Stack preserves efficiency by removing useless history".

---

### Week 5 Day 3: Intervals (Foundation)

**Progression:**

1. **Warm-up:** Merge Sorted Array
   - **Key:** Two-pointer merge (already familiar)
   - **Constraint:** Arrays are already sorted
   - **Trace:** `[1, 3]` + `[2]` → `[1, 2, 3]`

2. **Build Intuition:** Merge Intervals
   - **Key:** Sort first, then merge overlaps
   - **Constraint:** Consolidate ranges
   - **Trace:** `[[1,3], [2,6], [8,10]]` → `[[1,6], [8,10]]`

3. **Extend:** Insert Interval
   - **Key:** No re-sorting needed (list is sorted)
   - **Constraint:** Single pass insertion
   - **Trace:** Insert `[5, 7]` into `[[1,3], [6,9]]` → `[[1,3], [5,9]]`

**Checkpoint 3:** After these 3, you understand "Sort collapses complexity; linear sweep reveals structure".

---

## 🪜 STAGE 2: VARIATIONS & COMBINED CONSTRAINTS

### Week 5 Day 4A: Partition & Cyclic Sort (Building)

**Progression:**

1. **Warm-up:** Sort Colors (DNF)
   - **Key:** 3-way partition using pointers
   - **Constraint:** 0, 1, 2 only
   - **Trace:** `[2, 0, 2, 1, 1, 0]` → `[0, 0, 1, 1, 2, 2]`
   - **Insight:** When to move Low/Mid/High separately

2. **Build Intuition:** Move Zeroes
   - **Key:** 2-way partition (zeroes vs non-zeroes)
   - **Constraint:** Maintain relative order
   - **Trace:** `[0, 1, 0, 3, 12]` → `[1, 3, 12, 0, 0]`
   - **Insight:** Read/Write pointer separation

3. **Extend (Harder):** Missing Number + Find Duplicate
   - **Key:** Cyclic Sort (values map to indices)
   - **Constraint:** Range 1..N (no extra space)
   - **Trace:** `[3, 1, 3, 4, 2]` with N=4. Duplicate is 3.
   - **Insight:** Array is a linked list; value is "next pointer"

**Checkpoint 4:** After these 3, you understand "Indexing replaces sorting; O(1) space via clever addressing".

---

### Week 5 Day 4B: Kadane's Algorithm (Building)

**Progression:**

1. **Warm-up:** Maximum Subarray
   - **Key:** Reset negative prefixes
   - **Constraint:** Contiguous elements only
   - **Trace:** `[-2, 1, -3, 4, -1, 2, 1, -5, 4]` → `4 + (-1) + 2 + 1 = 6`
   - **Insight:** "Greedy local = optimal global"

2. **Build Intuition:** Best Time to Buy & Sell Stock
   - **Key:** Max profit = max(price[j] - price[i]) for j > i
   - **Constraint:** Single transaction
   - **Trace:** `[7, 1, 5, 3, 6, 4]` → Profit = 5 (buy at 1, sell at 6)
   - **Insight:** Reframe as "max subarray on deltas"

3. **Extend (Harder):** Maximum Product Subarray
   - **Key:** Track both max and min (negatives flip)
   - **Constraint:** Product instead of sum
   - **Trace:** `[2, 3, -2, 4]` → Product = 6 (from 2*3)
   - **Insight:** Negative numbers create new max potential

**Checkpoint 5:** After these 3, you understand "Kadane = greedy DP on local decisions".

---

### Week 5 Day 5: Fast & Slow Pointers (Building)

**Progression:**

1. **Warm-up:** Linked List Cycle
   - **Key:** Two pointers at different speeds collide iff cycle
   - **Constraint:** No extra storage
   - **Trace:** `1 -> 2 -> 3 -> 4 -> 2` (cycle at 2)
   - **Insight:** Relative motion geometry

2. **Build Intuition:** Middle of Linked List
   - **Key:** Slow reaches middle when fast reaches end
   - **Constraint:** Single pass, O(1) space
   - **Trace:** `[1, 2, 3, 4, 5]` → Middle is 3
   - **Insight:** Fast/Slow for "finding positions"

3. **Extend (Harder):** Linked List Cycle II (Find Cycle Start)
   - **Key:** Two passes; second pass uses meeting point
   - **Constraint:** O(1) space (no visited set)
   - **Trace:** Find where cycle starts, not just existence
   - **Insight:** "Geometric equivalence of distances" (math)

**Checkpoint 6:** After these 3, you understand "Geometry-based O(1) space solutions".

---

## 🪜 STAGE 3: MIXED PATTERNS & NOVEL CONSTRAINTS

### Mixed Problem 1: Longest Substring with At Most K Distinct Characters
**Patterns Used:** Hash (frequency) + Sliding Window  
**Difficulty:** 🟡 Medium

**Approach:**
1. Use hash to track character frequencies in current window
2. Maintain left and right pointers for window
3. Expand right, shrink left when more than k distinct characters

**Key Decision:** When to expand vs. shrink the window?

---

### Mixed Problem 2: Merge K Sorted Lists
**Patterns Used:** Merge (two pointers) + Heap (priority queue)  
**Difficulty:** 🔴 Hard

**Approach:**
1. Naive: Merge lists pairwise (O(NK) where N=num lists, K=list size)
2. Optimized: Use heap to track k heads simultaneously

**Key Decision:** Pairwise merge vs. k-way merge?

---

### Mixed Problem 3: Rearrange Array Max/Min
**Patterns Used:** Partition + Array Manipulation  
**Difficulty:** 🟡 Medium

**Approach:**
1. Find median (or k-th element) using Quickselect (partitioning)
2. Rearrange by comparing with median

**Key Decision:** Do we need the actual median or just the partition?

---

## ⚠ COMMON PROBLEM-SOLVING PITFALLS

### Pitfall 1: "I'll brute force first, then optimize"
**Why It's Wrong:** Brute force often locks you into a mindset (e.g., checking every pair). Jumping to O(N²) thinking prevents you from seeing the O(N) pattern.

**Fix:** After reading a problem, **always ask:** "What structure can I exploit?" Hash? Sorting? Pointers?

---

### Pitfall 2: "Sorting should always come first"
**Why It's Wrong:** Some problems are O(N) without sorting (Hash, Monotonic Stack, Kadane). Sorting (O(N log N)) can be overkill.

**Fix:** Ask: "Is the order important?" If yes, sort. If no, consider unsorted approaches.

---

### Pitfall 3: "Floyd's Cycle Detection is too complicated"
**Why It's Wrong:** You memorize it but don't understand the geometry. When variations come, you're stuck.

**Fix:** Draw the racetrack. Understand: "Hare laps Tortoise = cycle proven."

---

### Pitfall 4: "I solved the standard version; variations must also work"
**Why It's Wrong:** A small constraint change (circular array, k transactions, m+n size) breaks your solution.

**Fix:** For each problem, **explicitly trace** the variation. Ensure your invariants still hold.

---

### Pitfall 5: "Edge cases are optional"
**Why It's Wrong:** Edge cases often reveal algorithmic flaws (off-by-one, wrong reset condition).

**Fix:** Trace these FIRST: empty input, single element, all same value, sorted input.

---

## 📌 PATTERN TEMPLATES & PSEUDOCODE

### Template 1: Hash-Based Frequency (Two Sum)
```
frequencies = empty hash
for each num in array:
  complement = target - num
  if complement in frequencies:
    return (num, complement)
  frequencies[num] = frequencies.get(num, 0) + 1
return None (no pair found)
```

**When to Use:** "Find pair/triple/k-tuple with property X"

---

### Template 2: Monotonic Stack (Next Greater)
```
stack = empty
result = [-1] * len(array)
for i from end to start:
  while stack not empty and stack.top < array[i]:
    idx = stack.pop()
    result[idx] = array[i]
  stack.push(i)
return result
```

**When to Use:** "Find next/prev element satisfying a property"

---

### Template 3: Sort & Sweep (Merge Intervals)
```
sort intervals by start time
current = intervals[0]
merged = [current]
for next in intervals[1:]:
  if next.start <= current.end:
    current.end = max(current.end, next.end)
  else:
    merged.append(current)
    current = next
merged.append(current)
return merged
```

**When to Use:** "Merge/consolidate ranges, find overlaps, detect conflicts"

---

### Template 4: Partition (DNF)
```
low = 0, mid = 0, high = len-1
while mid <= high:
  if arr[mid] == 0:
    swap(arr[low], arr[mid])
    low++, mid++
  else if arr[mid] == 1:
    mid++
  else:
    swap(arr[mid], arr[high])
    high--
```

**When to Use:** "Categorize into groups, in-place segregation"

---

### Template 5: Kadane (Max Subarray)
```
current_sum = 0
max_sum = -infinity
for num in array:
  current_sum = max(num, current_sum + num)
  max_sum = max(max_sum, current_sum)
return max_sum
```

**When to Use:** "Max/min contiguous sum/product, local optimization"

---

### Template 6: Floyd's Cycle (Detect & Find Start)
```
// Phase 1: Detect
slow = fast = head
while fast and fast.next:
  slow = slow.next
  fast = fast.next.next
  if slow == fast:
    cycle_detected = true
    break

// Phase 2: Find Start
if cycle_detected:
  slow = head
  while slow != fast:
    slow = slow.next
    fast = fast.next
  return slow (or fast) — this is cycle start
```

**When to Use:** "Detect cycles, find meeting points, geometry-based O(1) space"

---

## 🎯 STRATEGY BY PROBLEM TYPE

| Problem Type | Recommended Patterns | Why | Example |
|---|---|---|---|
| **Pair/Element Finding** | Hash | O(1) lookup for complements | Two Sum |
| **Next Greater/Smaller** | Monotonic Stack | Single pass, O(N) | Daily Temps |
| **Range/Interval** | Sort & Sweep | Sorting enables linear structure | Merge Intervals |
| **Ordering/Categorization** | Partition | In-place, O(1) space | Sort Colors |
| **Contiguous Subarray** | Kadane | Greedy DP | Max Subarray |
| **Cycle/Midpoint** | Fast & Slow | Geometry, O(1) space | Linked List Cycle |

---

**End of Week 5 Problem-Solving Roadmap**  
*Progress through stages 1→2→3. Master templates. Recognize patterns by problem signature.*