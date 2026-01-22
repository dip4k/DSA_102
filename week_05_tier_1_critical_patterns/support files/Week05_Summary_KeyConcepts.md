# 📖 Week 05 Summary & Key Concepts: Tier 1 Critical Patterns Reference

**Status:** Graduate-Level Study Notes  
**Audience:** Students completing Week 05 instructional content  
**Purpose:** Comprehensive reference for review, retention, and interview prep

---

## 🎯 Week 05 Narrative Summary

**Week 05: Tier 1 Critical Patterns** teaches you to recognize and solve problems using six high-frequency patterns. These patterns, collectively known as "Tier 1 Critical," solve 30-40% of interview problems. You'll move from learning individual techniques to recognizing pattern families and combining them.

### The Six Core Patterns

**Day 1: Hash Map & Hash Set** — Use fast lookups to solve complement-based problems (two-sum), count frequencies (anagrams), check membership. Mental model: "Fast lookup with O(1) average case."

**Day 2: Monotonic Stack** — Maintain stack in increasing/decreasing order to efficiently find next/previous elements. When new element breaks order, pop and process. Mental model: "Efficient next/previous via stack discipline."

**Day 3: Merge Operations & Intervals** — Sort intervals by start time, then greedily merge overlapping ranges. Mental model: "Sort then single-pass greedy merge."

**Day 4A: Partition & Cyclic Sort** — Dutch National Flag (0-1-2 sorting) and cyclic sort for finding missing/duplicate elements in 1..n arrays. Mental model: "Rearrange in-place with O(1) space."

**Day 4B: Kadane's Algorithm** — Track maximum subarray ending at each position. Local max = max(current, sum so far + current). Mental model: "DP tracking: local max vs global max."

**Day 5: Fast-Slow Pointers** — Two pointers at different speeds; when they meet, cycle detected or midpoint found. Mental model: "Floyd's cycle detection: O(n) time, O(1) space."

---

## 🧠 Concept Map: Pattern Relationships

```
Week 05 Patterns Space

├─ LOOKUP SPEED (Hash)
│  ├─ Time: O(1) average, O(n) worst
│  ├─ Space: O(n)
│  ├─ Use: Complement search, frequency, membership
│  └─ When: Need fast O(1) lookups
│
├─ STACK ORDER (Monotonic Stack)
│  ├─ Time: O(n) single pass
│  ├─ Space: O(n) stack
│  ├─ Use: Next/previous element, rain water, histogram
│  └─ When: Need previous element processing
│
├─ MERGE STRATEGY (Intervals)
│  ├─ Time: O(n log n) sort + O(n) merge = O(n log n)
│  ├─ Space: O(1) or O(n) depending on output
│  ├─ Use: Merge intervals, insert interval, merge lists
│  └─ When: Ranges need combining
│
├─ REARRANGE METHOD (Partition)
│  ├─ Time: O(n) partition, O(n) cyclic sort
│  ├─ Space: O(1) in-place
│  ├─ Use: 0-1-2 sort, find missing/duplicate
│  └─ When: In-place rearrangement required
│
├─ SUBARRAY OPTIMIZATION (Kadane)
│  ├─ Time: O(n) single pass
│  ├─ Space: O(1) auxiliary
│  ├─ Use: Max/min subarray, max product, circular
│  └─ When: Optimizing contiguous subarrays
│
└─ CYCLE FINDING (Fast-Slow)
   ├─ Time: O(n)
   ├─ Space: O(1)
   ├─ Use: Cycle detection, midpoint, palindrome
   └─ When: Linked list analysis needed
```

---

## 📋 Per-Day Concept Summary

### Day 1: Hash Map & Hash Set Patterns

**Definition:** Hash tables map keys to values with O(1) average lookup time.

**Core Properties:**
- Hash function: key → index (should be uniform, fast)
- Collision handling: separate chaining or open addressing
- Load factor: size / capacity; resize when > 0.75 (typical)

**Two-Sum Pattern:** Given array + target, find two numbers that sum to target.
- Naive: O(n²) check all pairs
- Hash: O(n) → For each num, check if (target - num) exists in hash

**Frequency Counting:** Count occurrences of each element.
- Anagrams: two strings are anagrams iff same character frequencies
- Top-K frequent: frequency map + heap/sort

**Membership Testing:** Check if element exists in set.
- Deduplication: iterate, add to set, skip if exists
- Vs array: O(n) vs O(1) lookup

**Complexity Table:**

| Operation | Hash Avg | Hash Worst | Array | Set |
|-----------|----------|-----------|-------|-----|
| Lookup | O(1) | O(n) | O(n) | O(1) |
| Insert | O(1) | O(n) | O(1) | O(1) |
| Delete | O(1) | O(n) | O(n) | O(1) |
| Collision | Chaining | O(n) chain | N/A | N/A |

---

### Day 2: Monotonic Stack Patterns

**Definition:** Stack where elements are maintained in increasing or decreasing order.

**Core Insight:** When new element breaks order, pop elements and process them before pushing new element.

**Next Greater Element:** For each element, find the next element to its right that is greater.
- Naive: O(n²) check all right neighbors
- Monotonic: O(n) → decreasing stack, when new element > top, pop and record (new is next greater)

**Stock Span:** Number of consecutive days where price ≤ today's price.
- Use decreasing stack of (price, span)
- For new day: pop cheaper days, add their span to current span

**Trapping Rain Water:** Given elevation map, how much water trapped between hills?
- Monotonic decreasing stack of indices
- When height increases, pop and calculate trapped water
- Water level = min(current, top of stack) - popped element

**Largest Rectangle in Histogram:** Given heights, find largest rectangle.
- Monotonic increasing stack of indices
- When height decreases, pop and calculate area using popped height

---

### Day 3: Merge Operations & Interval Patterns

**Definition:** Combine overlapping intervals or merge sorted structures.

**Interval Merge Algorithm:**
1. Sort intervals by start time (O(n log n))
2. Single pass: for each interval, either merge with last or add as new
3. Result: merged intervals in O(n) after sort

**Merge Sorted Arrays:** Given two sorted arrays, merge into one sorted array.
- Two-pointer merge: O(n+m) time, O(n+m) space for output
- In-place merging in array-of-arrays requires backtracking from end

**Merge K Sorted Lists:** Given k sorted lists, merge into one.
- Naive: merge pairwise O(nk) comparisons = O(nk log k)
- Heap: maintain min-heap of k elements (heads of lists), O(nk log k) same but cleaner

**Insert Interval:** Given sorted intervals and new interval, insert and merge.
- Don't sort (already sorted); smart insertion + merge on the fly
- Find insertion point, merge backward/forward as needed

---

### Day 4A: Partition & Cyclic Sort

**Dutch National Flag (0-1-2 Sorting):**
- Given array of 0s, 1s, 2s (or three groups), sort in one pass
- Algorithm: three pointers (low, mid, high)
  - [0, low): zeros
  - [low, mid): ones
  - [mid, high): unprocessed
  - [high, n): twos

**Cyclic Sort (for 1..n arrays):**
- Each element should be at index = element - 1
- Place element at correct position directly (if not already there)
- Find missing/duplicate by checking positions after sorting

**Find Missing Number:** Given 0..n missing one number.
- Cyclic sort, then find position where arr[i] != i

**Find Duplicate Number:** Given 1..n with one duplicate.
- Can't use space (no hash allowed typically)
- Cyclic sort reveals duplicate
- Or: use fast-slow pointers (Day 5)

---

### Day 4B: Kadane's Algorithm

**Definition:** Maximum sum of a contiguous subarray.

**Algorithm:**
```
max_so_far = 0
max_ending_here = 0
for each element:
  max_ending_here = max(element, max_ending_here + element)
  max_so_far = max(max_so_far, max_ending_here)
```

**Insight:** At each position, decide: start new subarray from here, or extend current?

**Variants:**
- **Max Product Subarray:** Similar but track both min and max (negative × negative = positive)
- **Circular Max Subarray:** Max((total_sum - min_subarray), max_subarray) with care for all-negative case
- **Constrained Kadane:** Max subarray with at least k elements, or budget constraint

**Why DP?** Subproblem: max subarray ending at position i. Build solution from position i-1.

---

### Day 5: Fast-Slow Pointers

**Floyd's Cycle Detection:**
- Slow pointer moves 1 step, fast pointer moves 2 steps
- If cycle exists, they'll meet
- Time: O(n), Space: O(1)

**Finding Cycle Start:**
- Meet point reveals cycle exists
- Reset one pointer to head, move both at speed 1
- They meet again at cycle start

**Midpoint Detection:**
- Fast reaches end when slow reaches middle
- Use: split linked list into two halves

**Why O(1) Space?** No hash table needed for cycle detection (unlike using set).

**Why This Works?** In cycle, relative speed = 1. Eventually they meet.

---

## ❌ Seven Common Misconceptions (Corrected)

### Misconception 1: Hash Tables Always O(1)
**False Reality:** Worst-case O(n) with bad hash or collision. But expected O(1) with good hash function and load factor < 0.75.

### Misconception 2: Monotonic Stack Is Complicated
**False Reality:** Simple idea: when element breaks order, pop and process. Single pass O(n).

### Misconception 3: Interval Problems Require Complex DP
**False Reality:** Sort + greedy pass works for most interval problems. O(n log n) not O(n²).

### Misconception 4: Partition Requires Extra Space
**False Reality:** Dutch National Flag and cyclic sort do in-place O(1) space. Requires more careful logic but possible.

### Misconception 5: Kadane's Only Works With Sum
**False Reality:** Same logic works with product, max-with-constraint, or any subarray optimization.

### Misconception 6: Fast-Slow Pointers Only for Cycles
**False Reality:** Also finds midpoint, splits lists, detects palindromes. Anywhere you need two references with different speeds.

### Misconception 7: These Patterns Are Isolated
**False Reality:** You'll combine them. Hash + partition, stack + intervals, fast-slow + hash for cycle with value. Mastery is recognizing combinations.

---

## 🔗 Week 05 Connection to Other Weeks

**From Week 04 (Patterns I):**
- Two-pointer thinking (Day 1) → hash-assisted two-pointer
- Sliding window (Day 2-3) → monotonic stack for window optimization
- Binary search (Day 4-5) → partition thinking
- Divide-conquer (Day 5) → recursion in cycle detection

**To Week 06 (String Patterns):**
- Hash (Day 1) → frequency counting for anagrams
- Monotonic stack (Day 2) → will see for bracket matching (stack discipline)
- Fast-slow (Day 5) → palindrome detection in strings

**To Week 07 (Trees):**
- Stack discipline (Day 2) → DFS with explicit stack
- Kadane's DP thinking (Day 4) → tree DP patterns
- Intervals (Day 3) → range queries in trees

**To Week 10 (DP):**
- Kadane's algorithm (Day 4) IS DP pattern
- Interval problems (Day 3) → scheduling DP
- Partition thinking (Day 4) → state design in DP

---

## ✅ Interview Readiness: Week 05 Checklist

You're ready for interviews when:

1. **Pattern Recognition < 10 seconds:** See problem → recognize pattern instantly
2. **Implementation without bugs:** Code hash, monotonic stack, intervals fluently
3. **Trace on paper:** Manually trace one problem per pattern without running code
4. **Explain trade-offs:** Time, space, code complexity compared to alternatives
5. **Handle edge cases:** Speak about boundary conditions before implementation
6. **Combine patterns:** Recognize when multiple patterns apply
7. **Real-world impact:** Connect pattern to actual system (cache uses hash, parsers use stack, etc.)

---

**Status:** Week 05 Summary Complete  
**Next:** Week 05 Interview Q&A Reference  
**Review Time:** 1-2 hours