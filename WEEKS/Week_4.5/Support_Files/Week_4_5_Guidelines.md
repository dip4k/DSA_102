# 📘 Week 4.5 Guidelines — Tier 1 Critical Problem-Solving Patterns

**🗓️ Week:** 4.5  
**📌 Theme:** Critical Patterns Solving 70-80% of Interview Problems  
**🎯 Goal:** Master foundational patterns before advanced data structures  
**⏱️ Duration:** 5 days | ~18-25 hours total  
**🌟 Status:** TIER 1 — Highest Priority Patterns

---

## 🎯 WEEK OVERVIEW

Week 4.5 bridges **Week 4 (Problem-Solving Fundamentals)** and **Week 5 (Trees & BST)** by introducing critical patterns that appear in 70-80% of technical interviews. These patterns are **TIER 1** — candidates must know them cold.

### Why Week 4.5 Exists

After mastering Week 4's foundational patterns (Two Pointers, Sliding Window, D&C, Binary Search), candidates need **high-frequency patterns** that complement these basics before diving into data structures:

1. **Hash Map / Hash Set** — THE foundational pattern (80%+ interviews)
2. **Monotonic Stack** — Specialized but powerful (next greater/smaller family)
3. **Merge Operations & Intervals** — Combining sorted collections, scheduling
4. **Partition & Cyclic Sort** — In-place rearrangement, missing/duplicate detection
5. **Kadane's Algorithm** — Maximum subarray (DP foundation)
6. **Fast & Slow Pointers** — Linked list cycle detection (promoted from Week 13)

---

## 📊 WEEK 4.5 STRUCTURE

### **Day 1: Hash Map / Hash Set** (3-4 hours)
**Criticality:** 🔴 TIER 1 (80%+ interviews)

**Topics:**
- HashMap: O(1) average lookup/insertion
- HashSet: membership testing, duplicates
- Two Sum problem (THE classic)
- Anagram detection & frequency counting
- Internal structure: buckets, chaining, load factor

**Key Problems:**
- Two Sum (#1) — **MUST master**
- Valid Anagram (#242)
- Group Anagrams (#49)
- Contains Duplicate (#217)

**Learning Outcomes:**
- ✅ Understand O(1) lookup vs O(n) array search
- ✅ Master when to use Hash Map vs Hash Set vs Two Pointers
- ✅ Apply to frequency counting, complement search
- ✅ Recognize hash collision handling

---

### **Day 2: Monotonic Stack** (2-3 hours)
**Criticality:** 🟡 MEDIUM-HIGH (40-50% interviews)

**Topics:**
- Maintaining monotonic order (increasing/decreasing)
- Next greater element pattern
- Trapping rain water (HARD classic)
- Largest rectangle in histogram
- Daily temperatures problem

**Key Problems:**
- Next Greater Element I (#496)
- Daily Temperatures (#739)
- Trapping Rain Water (#42) — HARD
- Largest Rectangle Histogram (#84) — HARD

**Learning Outcomes:**
- ✅ Understand amortized O(n) complexity (each element push/pop once)
- ✅ Master when to use increasing vs decreasing stack
- ✅ Apply to "next greater/smaller" family of problems
- ✅ Recognize stack-based optimization patterns

---

### **Day 3: Merge Operations & Intervals** (3-3.5 hours)
**Criticality:** 🟡 MEDIUM-HIGH (50-60% interviews)

**Topics:**
- Merging sorted arrays: O(n+m) time
- Merging sorted lists: O(m+n) with two pointers
- Merge K sorted lists: heap approach O(N log K)
- **Interval problems:** Overlapping intervals, interval scheduling
- Merging in real systems (merge sort, merge join)

**Key Problems:**
- Merge Two Sorted Lists (#21)
- Merge K Sorted Lists (#23)
- Merge Intervals (#56)
- Insert Interval (#57)
- Interval List Intersections (#986)

**Learning Outcomes:**
- ✅ Master two-pointer merge: O(n+m)
- ✅ Understand K-way merge with min-heap: O(N log K)
- ✅ Apply to interval scheduling problems
- ✅ Recognize when to sort intervals first

---

### **Day 4 (Part A): Partition & Cyclic Sort** (2-2.5 hours)
**Criticality:** 🟡 MEDIUM (30-40% interviews)

**Topics:**
- Dutch National Flag problem (3-way partition)
- Move zeroes to end
- Partition by pivot (quicksort foundation)
- **Cyclic Sort pattern:** Finding missing numbers, duplicates in [1..n]
- In-place segregation with O(1) space

**Key Problems:**
- Move Zeroes (#283)
- Sort Colors (#75) — Dutch Flag
- Find All Missing Numbers (#448) — Cyclic Sort
- Find Duplicate Number (#287)
- First Missing Positive (#41) — HARD

**Learning Outcomes:**
- ✅ Master Dutch National Flag (3 pointers: low, mid, high)
- ✅ Understand Cyclic Sort for [1..n] range problems
- ✅ Apply in-place rearrangement with O(1) space
- ✅ Recognize when Cyclic Sort applies vs Hash Set

---

### **Day 4 (Part B): Kadane's Algorithm** (2-2.5 hours)
**Criticality:** 🟡 HIGH (50% interviews)

**Topics:**
- Maximum subarray problem (THE classic DP problem)
- Maximum product subarray (handle negatives)
- DP formulation: T[i] = max subarray ending at i
- Recurrence: max(arr[i], max_ending_at_i-1 + arr[i])
- Constraint variations (circular, multiple transactions)

**Key Problems:**
- Maximum Subarray (#53) — **Classic!**
- Maximum Product Subarray (#152)
- Maximum Sum Circular Subarray (#918)
- Best Time to Buy/Sell Stock III (#123) — Extended

**Learning Outcomes:**
- ✅ Master Kadane's DP recurrence
- ✅ Understand O(n) single-pass optimization
- ✅ Apply to product variant (track min and max)
- ✅ Handle edge case: all negative numbers

---

### **Day 5: Fast & Slow Pointers** (2.5-3 hours)
**Criticality:** 🟡 HIGH (60% interviews)

**Topics:**
- Fast & Slow Pointers mechanics
- Linked list cycle detection (Floyd's algorithm)
- Finding cycle start (extended Floyd's)
- Midpoint finding (fast reaches end → slow at middle)
- Happy number problem
- **Promoted from Week 13** due to high ROI

**Key Problems:**
- Linked List Cycle (#141) — Foundation
- Linked List Cycle II (#142) — Find start
- Middle of Linked List (#876)
- Happy Number (#202)
- Remove Nth from End (#19)
- Palindrome Linked List (#234)

**Learning Outcomes:**
- ✅ Understand relative speed = 1 (why they meet)
- ✅ Master Floyd's cycle detection: O(n) time, O(1) space
- ✅ Apply extended Floyd's to find cycle start
- ✅ Recognize O(1) space advantage over Hash Set

---

## 🎯 LEARNING PHILOSOPHY

### Tier 1 Pattern Criteria

Patterns in Week 4.5 meet ALL these criteria:

1. **High Interview Frequency:** Appear in 40%+ of technical interviews
2. **Foundational:** Build intuition for advanced patterns in later weeks
3. **High ROI:** Small investment (2-4 hours) → massive interview coverage
4. **Prerequisite:** Needed before data structures (Week 5+)

### Pattern Recognition First, Implementation Second

Week 4.5 emphasizes **pattern recognition**:
- Red flag keywords (e.g., "two sum" → Hash Map, "next greater" → Monotonic Stack)
- Trade-off discussion (Hash Map vs Two Pointers, Stack vs Two Pointers)
- When NOT to use the pattern (equally important)

**Goal:** Within 30 seconds of reading problem, identify the pattern.

---

## 📚 PREREQUISITES

### Required (Must Complete First)
- ✅ **Week 2:** Arrays, Linked Lists, Hash Tables
- ✅ **Week 3:** Sorting, Binary Search
- ✅ **Week 4:** Two Pointers, Sliding Window, Divide & Conquer

### Recommended (Helpful Context)
- Basic complexity analysis (O(n), O(log n), O(n²))
- Recursion fundamentals
- Basic DP intuition (for Kadane's)

---

## 🎓 DAILY STRUCTURE (Per Day)

Each day follows this structure:

### **1. Study Phase (60-90 min)**
- Read instructional file (all 11 sections)
- Understand "The Why" (motivation, real-world impact)
- Study visualizations (ASCII diagrams, traces)
- Review complexity analysis

### **2. Practice Phase (2-3 hours)**
- Solve Easy problems (15-20 min each)
- Attempt Medium problems (25-35 min each)
- Read Hard solutions (understand approach, optional implementation)

### **3. Review Phase (30 min)**
- Self-test questions (end of each instructional file)
- Check edge cases
- Review common pitfalls
- Update progress checklist

---

## 🎯 SUCCESS METRICS

### Per Difficulty Level
- **Easy:** Solve in 10-15 min without hints (80%+ success rate)
- **Medium:** Solve in 25-35 min (60%+ success rate, 1 hint allowed first time)
- **Hard:** Understand optimal approach (implement in 40-50 min, optional)

### Pattern Mastery Indicators
- ✅ **Recognition:** Identify pattern from keywords in <30 sec
- ✅ **Trade-offs:** Can compare multiple approaches (Hash Map vs Two Pointers)
- ✅ **Edge Cases:** Automatically check empty, negatives, boundaries
- ✅ **Explanation:** Can teach pattern to someone else
- ✅ **Coding Speed:** Easy in <15 min, Medium in <30 min

---

## 📊 WEEK 4.5 ROADMAP

```
START: Week 4 Complete
    ↓
Day 1: Hash Map / Hash Set (Foundation)
    ↓
Day 2: Monotonic Stack (Specialized)
    ↓
Day 3: Merge & Intervals (Divide & Conquer extension)
    ↓
Day 4A: Partition / Cyclic Sort (In-place)
Day 4B: Kadane's Algorithm (DP foundation)
    ↓
Day 5: Fast & Slow Pointers (Linked List mastery)
    ↓
Review: Mixed practice, weak spot focus
    ↓
READY: Week 5 (Trees & BST)
```

---

## 🔗 INTEGRATION WITH OTHER WEEKS

### Week 4 → Week 4.5 Connection
- **Two Pointers** → Hash Map (Two Sum: sorted vs unsorted)
- **Sliding Window** → Hash Set (longest substring without repeating)
- **Divide & Conquer** → Merge K Lists (binary tree of merges)

### Week 4.5 → Week 5 Connection
- **Hash Map** → Tree/Graph adjacency lists
- **Fast & Slow** → Tree problems (detect cycles in graphs)
- **Merge** → BST merge, tree traversal merging

### Week 4.5 → Later Weeks
- **Hash Map** → Memoization (DP, Week 10+)
- **Kadane's** → Advanced DP patterns (Week 10+)
- **Monotonic Stack** → Advanced stack problems (Week 6)

---

## 💡 STUDY TIPS

### 1. Master Two Sum Cold
**Why:** Two Sum (#1) is THE gateway problem. Interviewers expect instant recognition.
- Goal: Solve in <10 min without hints
- Practice: Daily for first 3 days
- Variations: Two Sum II (sorted), 3Sum, 4Sum

### 2. Pattern Recognition Drills
Spend 10 min daily:
- Read problem title + description
- Identify pattern in <30 sec
- Don't code, just recognize
- Check answer (which pattern?)

### 3. Draw Stack States (Monotonic Stack)
Monotonic Stack is hard to visualize initially:
- Draw stack contents at each step
- Trace Next Greater Element manually (3-4 examples)
- Understand why each element pushed/popped once

### 4. Understand "Why" Before "How"
Each pattern solves a specific problem inefficiency:
- Hash Map: O(n²) nested loops → O(n) with O(1) lookup
- Monotonic Stack: O(n²) nested → O(n) amortized
- Merge K: O(N*K) sequential → O(N log K) with heap

### 5. Spaced Repetition
- Day 3: Re-solve Day 1 problems
- Day 7: Re-solve all Week 4.5 problems
- Week 5: Random 5 problems from Week 4.5

---

## ⚠️ COMMON MISTAKES TO AVOID

### 1. Pattern Misidentification
**Mistake:** Applying wrong pattern (Hash Map when should use Two Pointers)
**Fix:** Always check: sorted or unsorted? Space constraints?

### 2. Nested Loops for Two Sum
**Mistake:** Using O(n²) brute force
**Fix:** This is automatic red flag in interviews (shows lack of Hash Map knowledge)

### 3. Thinking Monotonic Stack is O(n²)
**Mistake:** "Nested loop → O(n²)"
**Fix:** Amortized analysis: each element push/pop once → O(n)

### 4. Cyclic Sort on Wrong Range
**Mistake:** Applying to array not in [1..n] or [0..n]
**Fix:** Verify range before using Cyclic Sort

### 5. Kadane's: Initialize Max to 0
**Mistake:** Returns 0 for all-negative array
**Fix:** Initialize to arr[0]

---

## 📈 INTERVIEW RELEVANCE

### Frequency by Pattern (Based on LeetCode/Interview data)
1. **Hash Map / Hash Set:** 80%+ (appears in almost every interview)
2. **Fast & Slow Pointers:** 60% (linked lists very common)
3. **Merge Operations:** 50-60% (K-way merge, intervals)
4. **Kadane's Algorithm:** 50% (subarray problems)
5. **Monotonic Stack:** 40-50% (niche but powerful)
6. **Partition / Cyclic Sort:** 30-40% (less common but O(1) space advantage)

### FAANG Relevance
- **Google:** Hash Map (maps, indexing), Intervals (scheduling)
- **Meta:** Hash Map (social graph adjacency), Fast & Slow (graph cycles)
- **Amazon:** Merge Operations (distributed systems), Kadane's (inventory optimization)
- **Netflix:** Intervals (content scheduling), Hash Map (recommendation caching)
- **Apple:** Hash Map (system design), Monotonic Stack (UI rendering optimization)

---

## 🎯 COMPLETION CRITERIA

Before moving to Week 5, ensure:

✅ **Two Sum in <15 min:** Can solve without hints (CRITICAL)  
✅ **Pattern Recognition:** <30 sec to identify pattern from keywords  
✅ **Trade-offs:** Can discuss Hash Map vs Two Pointers  
✅ **Monotonic Stack:** Understand amortized O(n)  
✅ **Merge K Lists:** Why heap O(N log K)?  
✅ **Kadane's:** Derive DP recurrence  
✅ **Fast & Slow:** Explain cycle detection math  
✅ **Easy Success Rate:** 80%+ solved in time limit  
✅ **Medium Success Rate:** 60%+ solved in time limit

---

## 📚 RECOMMENDED RESOURCES

### Primary (Use These)
1. **Week 4.5 Instructional Files:** Complete all 6 files
2. **LeetCode Explore:** Hash Table, Stack cards
3. **NeetCode:** Pattern-based playlists (YouTube)
4. **Visualgo:** Hash Table, Stack visualizations

### Supplementary (Optional)
5. **CLRS (Chapter 11):** Hash Tables (theory)
6. **Elements of Programming Interviews:** Chapter 12 (Hash Tables)
7. **Grokking Patterns:** Hash Map, Stack sections

---

## 🚀 FINAL NOTES

Week 4.5 is **TIER 1 — CRITICAL**. These patterns appear in 70-80% of interviews. Master them before Week 5.

**Time Investment:** 18-25 hours total  
**ROI:** Solve 70-80% of interview problems  
**Difficulty:** 🟡 Medium (accessible but requires practice)  
**Impact:** 🔴 CRITICAL (make-or-break for interviews)

**Remember:** Pattern recognition > memorization. Understand "why" each pattern works, not just "how" to code it.

---

**Generated:** December 30, 2025  
**Version:** 9.0 (Week 4.5 NEW)  
**Status:** ✅ COMPLETE