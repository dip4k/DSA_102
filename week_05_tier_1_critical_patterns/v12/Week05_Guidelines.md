# 📋 Week 05 Guidelines: Tier 1 Critical Patterns Mastery

**Week Overview:** Tier 1 Critical Patterns (Hash, Monotonic Stack, Intervals, Partition, Kadane, Fast-Slow)  
**Status:** Core Curriculum Week (Days 1-5) + Optional Advanced (Day 6)  
**Total Content:** 5 comprehensive instructional files + 5 support guides  
**Time Allocation:** 15-20 hours core learning + practice  

---

## 🎯 Week Learning Arc

This week is **foundational bridge week** that brings patterns into sharp focus. You'll master six patterns that collectively solve 30-40% of interview problems:

**Days 1-5:** Master canonical patterns (hash, monotonic stack, intervals, partition/Kadane, fast-slow)  
**Day 6:** Optional advanced (Kadane variants, SCC preview)

By week's end, you'll recognize these patterns instantly and implement them without reference material.

---

## 📚 Day-by-Day Concept Overview

### Day 1: Hash Map & Hash Set Patterns
**Core Mental Model:** Fast lookup/counting using key-value mapping

**Key Techniques:**
- Two-sum complement pattern (O(n) time, O(n) space)
- Frequency counting for duplicates/anagrams (O(n) time)
- Membership testing and deduplication (O(1) average lookup)

**Why It Matters:** Hash tables power 15-20% of interview problems. Most "find pairs/triplets" problems reduce to hash lookups.

**Connection:** Builds on Week 4's two-pointer concepts. Next week strings will use hash tables extensively.

---

### Day 2: Monotonic Stack Patterns
**Core Mental Model:** Stack maintains elements in increasing/decreasing order; when new element breaks order, pop and process

**Key Techniques:**
- Next greater/smaller element (O(n) single pass)
- Stock span and trapping rain water (O(n) stack-based)
- Largest rectangle in histogram (O(n) elegant approach)

**Why It Matters:** Monotonic stack is elegant pattern that solves "next/previous" problems efficiently. Appears in 5-10% of hard problems.

**Connection:** Combines stack (Week 2) with intelligent ordering. Day 3's interval problems use similar thinking.

---

### Day 3: Merge Operations & Interval Patterns
**Core Mental Model:** Sort by start time, greedily merge overlapping intervals

**Key Techniques:**
- Merge sorted arrays (O(n+m) two-pointer merge)
- Merge intervals (O(n log n) sort + greedy merge)
- Insert interval with merging (O(n) smart insertion)

**Why It Matters:** Interval scheduling appears in real systems (calendar, resource allocation). Teaches greedy thinking.

**Connection:** Day 1 hash for quick lookups, Day 2 stack for nesting validation, Day 3 intervals for scheduling. Day 4 partition generalizes sorting.

---

### Day 4: Partition & Kadane's Algorithm
**Core Mental Model Part A:** Dutch National Flag (0-1-2 sorting) and cyclic sort for finding missing/duplicate

**Core Mental Model Part B:** Kadane's algorithm tracks maximum subarray ending here

**Key Techniques:**
- In-place partition with O(1) extra space
- Cyclic sort for 1..n arrays
- Kadane's algorithm: max = max(num, max + num) at each step

**Why It Matters:** Partition pattern teaches in-place manipulation. Kadane's is elegant DP pattern that appears in many variations.

**Connection:** Partition follows from merge operations (opposite direction). Kadane's shows DP thinking emerging naturally.

---

### Day 5: Fast-Slow Pointers
**Core Mental Model:** Two pointers moving at different speeds; when they meet, you've found the cycle or midpoint

**Key Techniques:**
- Floyd's cycle detection (O(n) time, O(1) space)
- Finding cycle start (pointer reset trick)
- Midpoint detection and list splitting (O(n) single pass)

**Why It Matters:** Fast-slow pointers solve linked list problems elegantly. O(1) space when others need O(n).

**Connection:** Culmination of Week 4-5 pattern mastery. Next week strings use similar logic.

---

## 🎓 Eight Core Learning Objectives

By end of Week 05, you will be able to:

1. **Recognize** hash patterns instantly (two-sum, frequency counting, anagrams)
2. **Implement** monotonic stack with confidence (next greater, trapping rain)
3. **Merge** intervals and sorted lists efficiently
4. **Partition** arrays in-place without extra space
5. **Calculate** Kadane's maximum subarray mentally
6. **Detect** cycles in linked lists with O(1) space
7. **Compare** which pattern fits which problem
8. **Optimize** from naive O(n²) to O(n) or O(n log n)

---

## 🧭 How to Study Effectively

### Strategy 1: Pattern Recognition Before Implementation

**Wrong approach:** See problem → code immediately

**Right approach:** See problem → identify pattern → recall technique → implement

Spend 20% time on pattern recognition, 80% on implementation practice.

### Strategy 2: Trace-First, Code-Second

For each algorithm:
1. Read explanation
2. Trace through example manually (no computer)
3. Draw diagrams (hash table growth, stack states)
4. Write pseudocode
5. Implement in language

### Strategy 3: Build Your Mental Checklists

For each pattern, create quick mental references:

**Hash:** "Do I need to count? Look up complements? Check membership?"

**Stack:** "Do I need previous/next? Increasing/decreasing order?"

**Intervals:** "Are ranges overlapping? Merge by start time?"

**Partition:** "Is it 0-1-2 sorting? Find missing/duplicate? In-place required?"

**Kadane:** "Max subarray? Max product? Circular variant?"

**Fast-Slow:** "Linked list cycle? Midpoint? Palindrome?"

### Strategy 4: Problems in Progression

**Stage 1 (Canonical):** Solve classic LeetCode problem per pattern  
**Stage 2 (Variations):** Solve 2-3 variations with different constraints  
**Stage 3 (Integration):** Solve problems mixing patterns or adding system design  

---

## ⏱️ Recommended Time Allocation

| Activity | Hours | Focus |
| :--- | :--- | :--- |
| Read Day 1-2 instructional | 2.5 | Mental models, hash lookups, stack discipline |
| Practice Day 1-2 problems | 3 | 8-10 LeetCode problems total |
| Read Day 3-4 instructional | 2.5 | Intervals, partition, Kadane mechanics |
| Practice Day 3-4 problems | 3 | 8-10 LeetCode problems total |
| Read Day 5 instructional | 1.5 | Fast-slow pointers, cycle detection |
| Practice Day 5 problems | 2 | 4-5 LeetCode problems |
| Integration & synthesis | 1.5 | Review all 5, connect patterns |
| Optional Day 6 (advanced) | 1-2 | Kadane variants, SCC preview |
| **Total Core (Days 1-5)** | **16-17 hours** | Interview prep complete |
| **Total with Day 6** | **17-19 hours** | Mastery level |

---

## 🚫 Five Common Pitfalls (and How to Avoid Them)

### Pitfall 1: Forgetting Edge Cases in Hash Lookups

**Wrong:** Checking `if (map[key] > 0)` without checking if key exists first

**Reality:** KeyNotFoundException or off-by-one bugs

**Fix:** Use ContainsKey() or defaultdict. Always check before accessing.

---

### Pitfall 2: Not Maintaining Monotonic Property

**Wrong:** Treating stack as regular LIFO without checking ordering

**Reality:** Next greater/smaller logic breaks silently

**Fix:** Remember: you pop WHILE elements violate monotonic property, then process.

---

### Pitfall 3: Interval Merge Without Sorting

**Wrong:** Trying to merge intervals without first sorting by start time

**Reality:** Logic becomes O(n²) instead of O(n log n)

**Fix:** Always sort by start time first, then single pass through.

---

### Pitfall 4: Partition Without In-Place Constraint Awareness

**Wrong:** Using extra space for partition when O(1) space required

**Reality:** Solution won't pass efficiency requirements

**Fix:** Understand when partition must be in-place vs when you can allocate.

---

### Pitfall 5: Kadane's With Wrong State Tracking

**Wrong:** `max = max(arr[i], max + arr[i])` without tracking global max

**Reality:** Returns max ending at position, not overall max

**Fix:** Maintain BOTH local max (ending here) and global max (best so far).

---

## 📊 Concept Map: How Week 05 Patterns Connect

```
Week 05: Critical Patterns

├─ LOOK UP FAST (Hash)
│  ├─ Two-sum complement search
│  ├─ Frequency counting
│  └─ Membership testing
│
├─ FIND NEXT/PREVIOUS (Monotonic Stack)
│  ├─ Next greater/smaller
│  ├─ Stock span
│  └─ Trapping rain water
│
├─ MERGE RANGES (Intervals)
│  ├─ Sorted arrays merge
│  ├─ Interval merging
│  └─ Insert interval
│
├─ REARRANGE IN-PLACE (Partition)
│  ├─ Dutch National Flag (0-1-2)
│  ├─ Cyclic sort
│  └─ Move zeros
│
├─ FIND BEST SUBARRAY (Kadane)
│  ├─ Max sum subarray
│  ├─ Max product
│  └─ Circular variants
│
└─ DETECT CYCLES (Fast-Slow)
   ├─ Cycle detection
   ├─ Finding cycle start
   └─ Midpoint detection
```

---

## 🎯 Seven Key Insights to Remember

1. **Hash tables are fast:** O(1) average case for lookup, counting, membership. Never do O(n) scanning when you could hash.

2. **Monotonic stacks process efficiently:** Instead of checking all previous elements (O(n²)), maintain order and pop as needed (O(n) total).

3. **Sorting enables merging:** Sort-then-merge is O(n log n) but parallelizable and cache-friendly. Better than complex in-place logic sometimes.

4. **In-place means O(1) space:** Partition, cyclic sort, rearrangement patterns teach you memory efficiency. Interview-winning pattern.

5. **Kadane's is DP insight:** Local max (ending here) vs global max (best anywhere). Recognize this DP pattern everywhere.

6. **Fast-slow is elegant:** No extra space, O(n) time. Works on linked lists where binary search wouldn't. Powers cycle detection.

7. **Patterns combine:** Hash + two-pointer, stack + interval, partition + sorting. Mastery is recognizing when to combine.

---

## 🔄 How Week 05 Connects to Surrounding Weeks

**From Week 04 (Patterns I):**
- Two-pointer, sliding window taught you invariants → Week 05 uses invariants in hash/stack/partition
- Binary search taught you divide-and-conquer → Week 05 uses similar thinking in merge/intervals

**To Week 06 (Strings):**
- Hash patterns (Day 1) → String frequency/anagram problems
- Monotonic stack (Day 2) → Will see stack for bracket matching
- Fast-slow pointers (Day 5) → Will see for palindrome detection

**To Week 07 (Trees):**
- Tree traversals use stack (Day 2 concept)
- Binary search on trees (Week 04 + 05 thinking)
- Balanced BSTs use partition/rearrange concepts

**To Week 10 (DP):**
- Kadane's algorithm (Day 4) is DP pattern
- Interval DP (Day 3) extends to scheduling problems
- Max subarray variants appear in many DP contexts

---

## ✅ Weekly Checklist

By end of week, you should be able to:

- [ ] Solve two-sum in one pass using hash
- [ ] Implement next greater element with monotonic stack
- [ ] Merge intervals without sorting overhead  
- [ ] Partition array in-place with O(1) space
- [ ] Calculate Kadane's maximum subarray mentally
- [ ] Detect cycle in linked list with O(1) space
- [ ] Recognize which of 6 patterns fit any problem
- [ ] Solve 3-4 problems from each pattern (18+ total)
- [ ] Trace through at least one problem per pattern without running code
- [ ] Explain trade-offs (space vs time) for each approach
- [ ] List real-world systems using each pattern (hash in caching, stack in parsing, intervals in scheduling, etc.)

---

## 🎓 Mastery Signals

You've mastered Week 05 when:

1. **You see patterns in problems instantly.** Someone shows you a problem; you immediately identify "this is hash complement search" or "this needs monotonic stack."

2. **You code without hesitation.** Implement hash patterns, monotonic stack, interval merge, partition, Kadane's without constantly re-reading logic.

3. **You handle edge cases proactively.** Before coding core logic, you list and guard against boundary conditions.

4. **You optimize instinctively.** Instead of coding O(n²), you recognize the O(n) pattern immediately.

5. **You connect patterns.** You see when to combine hash + two-pointer, or stack + interval logic.

6. **You trace manually.** You can trace code on paper and predict output without running it.

7. **You explain to others.** You can teach why monotonic stack works or how Kadane's algorithm is really DP without notes.

---

**Status:** Week 05 Guidelines Complete  
**Next:** Week 05 Summary & Key Concepts  
**Time to Completion:** 15-20 hours core learning