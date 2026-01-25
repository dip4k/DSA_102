# 🚀 WEEK 4.5 — PROBLEM-SOLVING ROADMAP

**Progressive Problem Difficulty & Strategy**

---

## 🎯 LEARNING PHILOSOPHY

Each pattern has a **canonical problem** that teaches the core concept, then variations that test understanding.

**Problem Progression:**
1. **Canonical:** The "textbook" problem
2. **Easy Variant:** Small twist on canonical
3. **Medium Variant:** Added constraint or edge case
4. **Hard Variant:** Combination or advanced mechanics

---

## 📌 DAY 1: HASH MAP / HASH SET — PROBLEM ROADMAP

### The Canonical Problem: Two Sum
- **Difficulty:** Easy
- **Core Idea:** Use HashMap to track complements
- **Strategy:** Single pass, store indices

### Easy Variants
1. **Two Sum II (Sorted Array)** → Two pointers work better
2. **Valid Anagram** → HashMap to count characters
3. **Valid Parentheses** → HashMap for matching pairs

### Medium Variants
1. **3Sum** → Sort + two pointers + HashMap logic
2. **Group Anagrams** → Sort + HashMap
3. **LRU Cache** → HashMap + Doubly Linked List

### Hard Variants
1. **4Sum** → Optimization over 3Sum logic
2. **Maximum Frequency Stack** → HashMap + Heap
3. **Design In-Memory File System** → Nested HashMap

### Recommended Practice Order
```
Day 1 Morning: Two Sum (canonical) + Two Sum II
Day 1 Afternoon: Valid Anagram + Group Anagrams
Day 2 Morning: 3Sum (medium)
Day 2 Afternoon: LRU Cache (design problem)
Day 3+: Advanced variants as time permits
```

---

## 📌 DAY 2: MONOTONIC STACK — PROBLEM ROADMAP

### The Canonical Problem: Next Greater Element
- **Difficulty:** Easy
- **Core Idea:** Pop while condition met, track answer
- **Strategy:** Single pass with stack

### Easy Variants
1. **Next Greater Element I** → Circular array variant
2. **Daily Temperatures** → Temperature-specific version
3. **Previous Smaller Element** → Mirror logic

### Medium Variants
1. **Trapping Rain Water** → Two-pass with height limits
2. **Largest Rectangle in Histogram** → Stack to find boundaries
3. **Stock Span** → Span of consecutive smaller elements

### Hard Variants
1. **Largest Rectangle in Skyline** → Histogram variant
2. **Trapping Water II (2D)** → Extension to grid
3. **Remove K Digits** → Greedy + stack

### Recommended Practice Order
```
Day 2 Morning: Next Greater Element + Daily Temperatures
Day 2 Afternoon: Trapping Rain Water
Day 3 Morning: Largest Rectangle in Histogram
Day 3 Afternoon: Stock Span (if time)
Day 4+: Advanced as time permits
```

---

## 📌 DAY 3: MERGE OPERATIONS — PROBLEM ROADMAP

### The Canonical Problem: Merge Intervals
- **Difficulty:** Easy (conceptually), Medium (implementation)
- **Core Idea:** Sort + iterate + check overlap
- **Strategy:** Single pass merge

### Easy Variants
1. **Insert Interval** → No pre-merge, just insert
2. **Meeting Rooms** → Check if any overlaps
3. **Interval List Intersections** → Find ALL overlaps

### Medium Variants
1. **Meeting Rooms II** → Count concurrent meetings (sort with events)
2. **Maximum CPU Load** → Similar to Meeting Rooms II
3. **Employee Free Time** → Find gaps between meetings

### Hard Variants
1. **Data Stream as Disjoint Intervals** → Dynamic merge on insertions
2. **Video Stitching** → Greedy + intervals
3. **Minimum Interval to Include Each Query** → Sorted intervals + binary search

### Recommended Practice Order
```
Day 3 Morning: Merge Intervals + Insert Interval
Day 3 Afternoon: Meeting Rooms II
Day 4 Morning: Interval List Intersections
Day 4 Afternoon: Employee Free Time (if time)
Day 5+: Advanced as time permits
```

---

## 📌 DAY 4A: PARTITION & CYCLIC SORT — PROBLEM ROADMAP

### The Canonical Problems
**Partition:** Sort Colors (Dutch National Flag)
- **Difficulty:** Easy
- **Core Idea:** Three pointers, three regions
- **Strategy:** Single pass segregation

**Cyclic Sort:** Find Missing Number
- **Difficulty:** Easy
- **Core Idea:** Map value to index
- **Strategy:** Placement-based sorting

### Easy Variants
1. **Sort Colors** → 0, 1, 2 segregation
2. **Move Zeroes** → 0s and non-0s (2-way partition)
3. **Missing Number** → Simple cyclic sort

### Medium Variants
1. **First Missing Positive** → Cyclic sort with filtering
2. **Find All Numbers Disappeared** → Cyclic sort output
3. **Find All Duplicates** → Cyclic sort with indicator

### Hard Variants
1. **Set Matrix Zeroes** → Partition logic on 2D
2. **Wiggle Sort II** → Partition + virtual indexing
3. **Rainbow Sort** → Generalized DNF (k colors)

### Recommended Practice Order
```
Day 4A Morning: Sort Colors + Move Zeroes
Day 4A Afternoon: Find Missing Number + First Missing Positive
Day 5 Morning: Find All Duplicates
Day 5 Afternoon: Set Matrix Zeroes (if time)
```

---

## 📌 DAY 4B: KADANE'S ALGORITHM — PROBLEM ROADMAP

### The Canonical Problem: Maximum Subarray Sum
- **Difficulty:** Easy (logic), Medium (understanding)
- **Core Idea:** Running sum with reset
- **Strategy:** Single pass state machine

### Easy Variants
1. **Maximum Subarray** → Canonical
2. **Best Time to Buy and Sell (once)** → Price difference variant
3. **Maximum Odd Sum** → Sum with odd/even constraint

### Medium Variants
1. **Maximum Product Subarray** → Track min and max
2. **Maximum Sum Circular Subarray** → Handle wrap-around
3. **Maximum Length of Subarray With Positive Product** → Sign tracking

### Hard Variants
1. **Maximum Sum Rectangle in Matrix** → 2D variant
2. **K-Concatenation Maximum Sum** → Repeat with constraint
3. **Maximum Sum with Removals** → Limited removal budget

### Recommended Practice Order
```
Day 4B Morning: Maximum Subarray + Best Time to Buy/Sell
Day 4B Afternoon: Maximum Product Subarray + Circular Subarray
Day 5 Morning: Maximum Length of Odd Sum (if time)
```

---

## 📌 DAY 5: FAST & SLOW POINTERS — PROBLEM ROADMAP

### The Canonical Problem: Linked List Cycle Detection
- **Difficulty:** Easy
- **Core Idea:** Floyd's algorithm (tortoise & hare)
- **Strategy:** Two-pass detection + finding start

### Easy Variants
1. **Linked List Cycle** → Detection only
2. **Happy Number** → Cycle detection on transformations
3. **Middle of Linked List** → Find midpoint

### Medium Variants
1. **Linked List Cycle II** → Find cycle start
2. **Find Duplicate Number** → Array as linked list
3. **Reorder List** → Midpoint + reverse + merge

### Hard Variants
1. **LRU Cache** → Combined with HashMap
2. **Palindrome Linked List** → Midpoint + reverse + compare
3. **Remove Nth Node From End** → Variant of fast/slow spacing

### Recommended Practice Order
```
Day 5 Morning: Linked List Cycle + Happy Number
Day 5 Afternoon: Linked List Cycle II + Find Duplicate
Evening Wrap-up: Middle of List + Palindrome List (review)
```

---

## 🚀 STRATEGIC ADVICE

### For Each Pattern

1. **Implement without looking at solutions.**
   - Time yourself. Aim for < 15 minutes for easy, < 30 for medium.

2. **Trace your code by hand on the provided examples.**
   - Find bugs before running.

3. **Optimize after it works.**
   - Get working → then optimize space/time.

4. **Explain your approach aloud.**
   - Builds communication skills for interviews.

5. **Read others' solutions (after completing yours).**
   - See alternative approaches and learn tricks.

### Handling Difficult Problems

**If stuck for 10 minutes:**
1. Re-read the problem statement word-by-word.
2. Identify which pattern it's asking for.
3. Recall the canonical problem for that pattern.
4. Map the problem's variables to the canonical template.

**If stuck for 20 minutes:**
1. Review the Visualization section of the relevant instructional file.
2. Trace the example by hand using the algorithm.
3. Implement on paper before coding.

**If stuck for 30+ minutes:**
1. Check the problem's hints (if available).
2. Review one similar problem's solution.
3. Re-attempt from scratch with new understanding.

---

## 📊 TIME ESTIMATES (Per Problem)

| Difficulty | Read | Solve | Trace | Optimize | Total |
|------------|------|-------|-------|----------|-------|
| **Easy** | 2 min | 8 min | 3 min | 2 min | ~15 min |
| **Medium** | 3 min | 15 min | 5 min | 7 min | ~30 min |
| **Hard** | 4 min | 20 min | 8 min | 10 min | ~45 min |

---

## 🎯 WEEK-LONG PROBLEM QUOTA

**Recommended:**
- **Easy problems:** 15-20 total (3-4 per pattern)
- **Medium problems:** 12-15 total (2-3 per pattern)
- **Hard problems:** 2-4 total (optional, 1 per pattern max)

**Total:** ~30-40 problems for the week.

---

## 🏆 MASTERY INDICATORS

By the end of the week, you should be able to:

1. ✅ **Identify pattern in < 1 minute** from problem statement
2. ✅ **Code solution in < 20 minutes** (including trace)
3. ✅ **Explain trade-offs** without notes
4. ✅ **Handle edge cases** naturally (not as afterthoughts)
5. ✅ **Solve variants** by mapping to canonical problem

---

**End of Roadmap. Use this as your weekly problem-solving guide!**