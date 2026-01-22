# 🗺 WEEK 5 SUMMARY: KEY CONCEPTS & CONCEPT MAP

**Week 5: Tier 1 Critical Patterns — Unified Overview**

---

## 📊 Visual Concept Map

```
                    ┌─────────────────────────────────────┐
                    │  WEEK 5: CRITICAL PATTERNS           │
                    │  (Unlocking O(N) solutions)          │
                    └──────────────┬──────────────────────┘
                                   │
                ┌──────────────────┼──────────────────────┐
                │                  │                      │
                ▼                  ▼                      ▼
        ┌────────────┐    ┌────────────┐        ┌────────────┐
        │  Hash-Based   │    │  Stack-Based   │        │ Pointer-Based  │
        │  (Lookup)     │    │  (Ordering)    │        │ (Geometry)     │
        └────────────┘    └────────────┘        └────────────┘
             │                  │                      │
    ┌────────┴────┐      ┌──────┴──────┐       ┌──────┴──────┐
    │              │      │             │       │             │
    Day 1       Day 3    Day 2        Day 4A   Day 5      Day 4B
    Hash        Intervals Monotonic   Partition Fast/Slow  Kadane
    Maps & Sets           Stack        Cyclic
```

---

## 📚 Day-by-Day Concept Summary

### DAY 1: HASH MAP & HASH SET PATTERNS
**Core Concept:** O(1) Lookup for Complement Tracking & Frequency Counting

**Key Operations:**
- Insertion: `O(1)` average, `O(N)` worst
- Lookup: `O(1)` average, `O(N)` worst
- Deletion: `O(1)` average, `O(N)` worst

**Key Problems:**
- **Two Sum:** Find two numbers that sum to target (Hash for complement)
- **Anagrams:** Group words that are anagrams (Hash for frequency)
- **Most/Least Frequent:** Count and sort by frequency

**Insight:** Replace "search every element" (O(N²)) with "lookup complement" (O(N)).

**Cognitive Anchor:** "Hash trades space (O(N)) for speed (O(1) lookup). Perfect for 'does this exist?' questions."

---

### DAY 2: MONOTONIC STACK PATTERNS
**Core Concept:** Maintaining Sorted Order While Processing Linearly

**Key Operations:**
- Next Greater Element: Find first element to the right that's larger
- Next Smaller Element: Find first element to the right that's smaller
- Stock Span: Days until higher price

**Key Problems:**
- **Daily Temperatures:** Days to wait for warmer temperature
- **Largest Rectangle in Histogram:** Find max area using heights
- **Trapping Rain Water:** Calculate trapped water volume

**Insight:** Process each element once, pop unnecessary history. O(N) instead of O(N²).

**Cognitive Anchor:** "Monotonic Stack = remember only relevant history. When someone taller arrives, shorter people are no longer useful."

**Visual Pattern:**
```text
Decreasing Stack (Next Greater):
  5
  4
  3
  2
  1 ← Current processing

New element (6) arrives:
  6 ← Larger than all, they all pop
```

---

### DAY 3: MERGE OPERATIONS & INTERVAL PATTERNS
**Core Concept:** Sort First, Then Sweep Linearly to Combine

**Key Operations:**
- Merge Sorted Arrays: O(N + M) with two pointers
- Merge Intervals: O(N log N) sort + O(N) sweep
- Insert Interval: O(N) without re-sorting
- Meeting Rooms: O(N log N) sweep line

**Key Problems:**
- **Merge Intervals:** Consolidate overlapping time ranges
- **Insert Interval:** Add new interval into sorted list
- **Meeting Rooms II:** Min rooms needed (max concurrent meetings)

**Insight:** Sorting collapses the problem. Linear sweep after sorting reveals structure.

**Cognitive Anchor:** "Two-Pointer Merge = zipper two sorted lists. Interval Merge = gather overlaps into consolidated blocks."

**State Progression:**
```text
Before: [1,3], [8,10], [2,6], [15,18]  (messy)
After Sort: [1,3], [2,6], [8,10], [15,18]  (ordered)
After Sweep: [1,6], [8,10], [15,18]  (consolidated)
```

---

### DAY 4A: PARTITION & CYCLIC SORT
**Core Concept:** In-Place Categorization & Range-Based Sorting

**Key Operations:**
- Dutch National Flag: 3-way partition (0s, 1s, 2s)
- Cyclic Sort: Place each number in its correct index
- Move Zeroes: Two-way partition
- Find Missing/Duplicate: Use index as address

**Key Problems:**
- **Sort Colors:** Arrange 0s, 1s, 2s in-place
- **Missing Number:** Find missing in range 1..N
- **Find Duplicate:** Locate duplicate in range 1..N
- **First Missing Positive:** Smallest positive not in array

**Insight:** Leverage value ranges to avoid sorting. O(N) time, O(1) space.

**Cognitive Anchor:** "Cyclic Sort = every number knows its home address. If not home, swap with whoever is there."

**Key Invariant (DNF):**
```text
[0, 0, 0 | 1, 1, 1 | ?, ?, ? | 2, 2, 2]
Low     ↑            ↑              ↑ High
      Processed      Scanning     Processed
```

---

### DAY 4B: KADANE'S ALGORITHM
**Core Concept:** Greedy DP: Reset Negative Prefixes to Maximize Sums

**Key Operations:**
- Max Subarray Sum: Find contiguous subarray with max sum
- Max Product Subarray: Track both max and min (negative flip)
- Circular Max Sum: Use standard + min Kadane

**Key Problems:**
- **Maximum Subarray:** Classic contiguous max
- **Best Time to Buy/Sell Stock:** Variation of max subarray
- **Maximum Circular Subarray Sum:** Handle wrap-around
- **Max Product Subarray:** Handle sign flips

**Insight:** If prefix sum is negative, starting fresh is better than carrying debt.

**Cognitive Anchor:** "Kadane = heavy backpack. If your total goes negative, drop the backpack and start fresh."

**Decision Logic:**
```text
For each element:
  If running_sum < 0:
    running_sum = 0 (drop backpack)
  Else:
    running_sum += element (keep adding)
  
  Update global_max = max(global_max, running_sum)
```

---

### DAY 5: FAST & SLOW POINTERS
**Core Concept:** Geometry-Based Cycle Detection Using Relative Motion

**Key Operations:**
- Detect Cycle: Two pointers meet iff cycle exists
- Find Cycle Start: Second pass with pointer at meeting point
- Find Midpoint: Fast reaches end, slow at middle
- Duplicate Detection: Treat array as linked list

**Key Problems:**
- **Linked List Cycle:** Does cycle exist?
- **Linked List Cycle II:** Where does cycle start?
- **Middle of Linked List:** Find midpoint
- **Happy Number:** Sequence cycles to 1 or loops
- **Find Duplicate Number:** Array has duplicate (constrained)

**Insight:** Two different speeds must collide if a cycle exists. No extra space needed.

**Cognitive Anchor:** "Tortoise & Hare = eventually, the fast one laps the slow one if there's a loop."

**Collision Logic:**
```text
Without cycle:      With cycle:
Hare reaches END    Hare loops, catches Tortoise
Tortoise still OK   They meet inside the cycle
```

---

## 📊 COMPARATIVE ANALYSIS TABLE

| 📌 Pattern | ⏱ Time | 💾 Space | ✅ Primary Use | 🔀 Alternative | Trade-Off |
|-----------|--------|---------|--------------|-------------|-----------|
| **Hash Map** | O(N) | O(N) | Lookup/Frequency | Sorting | Space for speed |
| **Monotonic Stack** | O(N) | O(N) | Range queries, next greater | Brute force (N²) | Complexity for speed |
| **Merge (Sorted)** | O(N+M) | O(1) | Combining sorted data | Sorting again | In-place only |
| **Merge (Intervals)** | O(N log N) | O(N) | Consolidating ranges | Brute force | Sorting overhead |
| **Partition (DNF)** | O(N) | O(1) | Categorization | Quick sort | Single pass only |
| **Cyclic Sort** | O(N) | O(1) | Range 1..N | Hash set | Range-constrained |
| **Kadane** | O(N) | O(1) | Max contiguous sum | DP (same) | Greedy simplicity |
| **Fast/Slow** | O(N) | O(1) | Cycle detection | Hash set | Complex geometry |

---

## 🧩 Key Patterns & Templates

### Pattern 1: Complement Lookup (Hash)
```
For each element x:
  complement = target - x
  If complement in hash_map:
    Return (x, complement)
  Else:
    Add x to hash_map
```

### Pattern 2: Monotonic Stack (Decreasing)
```
For each element:
  While stack not empty and top < current:
    Pop top (current is "Next Greater" for top)
  Push current
```

### Pattern 3: Sort & Sweep (Intervals)
```
1. Sort by start time
2. current = first interval
3. For each next interval:
   If overlaps current:
     Extend current
   Else:
     Save current, set current = next
```

### Pattern 4: Partition (DNF)
```
Initialize: low=0, mid=0, high=N-1
While mid <= high:
  If arr[mid] == 0: Swap(low, mid), low++, mid++
  Else if arr[mid] == 1: mid++
  Else: Swap(mid, high), high--
```

### Pattern 5: Kadane
```
current_sum = 0, max_sum = -infinity
For each element:
  current_sum = max(element, current_sum + element)
  max_sum = max(max_sum, current_sum)
Return max_sum
```

### Pattern 6: Floyd's Cycle Detection
```
slow = head, fast = head
While fast and fast.next:
  slow = slow.next
  fast = fast.next.next
  If slow == fast:
    Return True (cycle exists)
Return False
```

---

## 💡 5 KEY INSIGHTS FROM WEEK 5

### Insight 1: O(1) Space via Indexing
Cyclic Sort and Floyd's show that **clever indexing** (treating values as addresses) beats hash sets for space-constrained problems.

### Insight 2: Order Enables Efficiency
Sorting a problem (intervals, heights) often **linearizes** what looks like O(N²). This is the "Sort & Sweep" principle.

### Insight 3: Invariants Are Everything
Monotonic Stack, Partition, and Kadane all rely on **maintaining an invariant** through each iteration. Understand the invariant, and the code writes itself.

### Insight 4: Geometry Beats Brute Force
Floyd's Cycle Detection solves a problem in O(N) time and O(1) space using **relative motion geometry**—a principle that generalizes to many problems.

### Insight 5: Greedy + DP = Optimal
Kadane's Algorithm is **greedy locally** (reset negative prefix) yet **optimal globally** (maximizes final sum). This principle extends to Week 8.

---

## ⚠ 5 COMMON MISCONCEPTIONS FIXED

### Misconception 1: "Monotonic Stack is just a stack."
**Reality:** The key insight is **maintaining monotonicity** while processing linearly. The stack is just the data structure; the algorithm is about order.

### Misconception 2: "Fast/Slow is always 2:1 ratio."
**Reality:** Any two **coprime speeds** work. 2:1 is standard because it's simplest. 3:1 or 3:2 also work.

### Misconception 3: "Kadane's only works for max subarray."
**Reality:** Kadane's is a **DP template** applicable to max product, circular variants, and even 2D arrays.

### Misconception 4: "Cyclic Sort requires exact 1..N range."
**Reality:** It requires a **bounded range** equal to array size. 0..N-1 or 1..N both work; the key is no duplicates expected (or duplicates are the problem).

### Misconception 5: "Hash Map always beats other approaches."
**Reality:** Hash has O(N) worst-case. For small, bounded ranges, partition or cyclic sort beats hashing on space.

---

## 🎯 Integration Guide: Using Multiple Patterns

**Example 1: Remove Duplicates in Rotated Array**
- Recognize: Array is rotated (similar to rotation problems).
- Patterns: Use **Partition** to move duplicates. Or use **Cyclic Sort** if range is constrained.

**Example 2: Merge K Sorted Lists**
- Recognize: Multiple sorted sequences.
- Patterns: Use **Merge template** on pairs (2-way merge). Or use **Heap** (priority queue) for simultaneous merge.

**Example 3: Find Median in Data Stream**
- Recognize: Running data, need middle value.
- Patterns: Use **Fast/Slow** for midpoint. Or use **Heap** (max-heap and min-heap).

---

**End of Week 5 Summary**  
*Master these 6 patterns, and you've unlocked the door to 65-75% of medium-level interview problems.*