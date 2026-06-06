# ✅ Week 05 Daily Progress Checklist: Action Plan for Mastery

**Audience:** Self-directed learners  
**Purpose:** Track daily progress, ensure depth not just breadth

---

## 📅 MONDAY: Day 1 Core — Hash Map & Hash Set Patterns

### Morning Session (2 hours)

**Read Instructional Content (45 min)**
- [ ] Read Week 05 Day 1 Instructional file: Hash Map & Hash Set Patterns
- [ ] Note: 2-sum complement pattern, frequency counting, membership testing
- [ ] Mark 3 key insights that surprised you:
  1. ___________________________________
  2. ___________________________________
  3. ___________________________________

**Visualize & Trace (30 min)**
- [ ] Draw hash table with collision (separate chaining, open addressing)
- [ ] Trace two-sum for example: arr=[2,7,11,15], target=9
  - [ ] Show hash state after each iteration
  - [ ] Mark when complement found
- [ ] Trace frequency counting for "listen" and "silent" (anagrams)

**Comprehension Check (15 min)**
- [ ] Explain to someone: Why is hash O(1) average vs O(n) worst?
- [ ] Write down: When is sorting better than hashing? (example needed)
- [ ] List: 3 real-world systems using hash tables for speed

### Afternoon Session (2.5 hours)

**Code Practice Problems (2 hours)**
- [ ] LeetCode 1: Two Sum (🟢 Easy)
  - [ ] First attempt time: ____ min
  - [ ] Optimized time: ____ min
  - [ ] Key learning: _______________

- [ ] LeetCode 242: Valid Anagram (🟢 Easy)
  - [ ] Implementation time: ____ min
  - [ ] Space optimization insight: _______________

- [ ] LeetCode 349: Intersection of Two Arrays (🟢 Easy)
  - [ ] Compare: set vs hash vs sorting approaches
  - [ ] Time complexity for each: _______________

**Reflection & Integration (30 min)**
- [ ] When would you NOT use hash? (edge cases, constraints)
  - Reason 1: _______________
  - Reason 2: _______________
- [ ] Connect to Week 04: How does hash differ from two-pointer search?
- [ ] Mental note: Hash _______ is next pattern after mastering today

### Evening Checkpoint
- [ ] Can code two-sum without references? ☐ Yes ☐ No
- [ ] Understand why load factor matters? ☐ Yes ☐ Partial ☐ No
- [ ] Ready for Day 2? ☐ Yes ☐ Review needed

---

## 📅 TUESDAY: Day 2 Core — Monotonic Stack Patterns

### Morning Session (2 hours)

**Read Instructional Content (45 min)**
- [ ] Read Week 05 Day 2 Instructional file: Monotonic Stack Patterns
- [ ] Key concept: Stack maintains order, pop on violation, single pass O(n)
- [ ] Mark 3 surprising insights:
  1. ___________________________________
  2. ___________________________________
  3. ___________________________________

**Visualize & Trace (30 min)**
- [ ] Draw stack state evolution for [2,1,2,4,3] (next greater element)
  - [ ] Show after each push/pop
  - [ ] Mark when results recorded
- [ ] Trace trapping rain water for [0,1,0,2,1,0,1,3,2,1,2,1]
  - [ ] Show stack content at each step
  - [ ] Mark water trapped calculation

**Comprehension Check (15 min)**
- [ ] Explain: Why is monotonic stack O(n) not O(n²)?
- [ ] Compare: Monotonic decreasing vs increasing when to use each?
- [ ] List: 3 problems that use monotonic stack pattern

### Afternoon Session (2.5 hours)

**Code Practice Problems (2 hours)**
- [ ] LeetCode 496: Next Greater Element (🟢 Easy)
  - [ ] First attempt time: ____ min
  - [ ] Stack state visualization: clear? ☐ Yes ☐ Need review

- [ ] LeetCode 739: Daily Temperatures (🟡 Medium)
  - [ ] Stack contains: __________ (indices or values?)
  - [ ] Why better than brute force? _______________

- [ ] LeetCode 42: Trapping Rain Water (🔴 Hard)
  - [ ] Time attempt 1: ____ min
  - [ ] If stuck: reference solution, understand, code again
  - [ ] Key insight: _____________________

**Reflection & Integration (30 min)**
- [ ] Compare Day 1 (hash) to Day 2 (stack): when use each?
  - Hash: _____________
  - Stack: _____________
- [ ] Could you use hash for next greater element? Why or why not?
- [ ] Monotonic stack + Day 1 hash combination?

### Evening Checkpoint
- [ ] Can code next greater element from scratch? ☐ Yes ☐ No
- [ ] Understand trapping rain water logic? ☐ Yes ☐ Partial ☐ No
- [ ] Ready for Day 3? ☐ Yes ☐ Review needed

---

## 📅 WEDNESDAY: Day 3 Core — Merge Operations & Interval Patterns

### Morning Session (2 hours)

**Read Instructional Content (45 min)**
- [ ] Read Week 05 Day 3 Instructional file: Merge Operations & Intervals
- [ ] Key concept: Sort by start time, then greedy single-pass merge
- [ ] Mark surprises:
  1. ___________________________________
  2. ___________________________________
  3. ___________________________________

**Visualize & Trace (30 min)**
- [ ] Draw interval merge for [[1,3],[2,6],[8,10],[15,18]]
  - [ ] Show sorted order
  - [ ] Show merge decisions at each step
- [ ] Trace merge sorted arrays for [1,4,5,6] and [2,3,5]
  - [ ] Show two-pointer positions at each step

**Comprehension Check (15 min)**
- [ ] Explain: Why sort by START time not END time?
- [ ] When does greedy merging work vs fail?
- [ ] Connect to Day 1/2: Any hash or stack needed here?

### Afternoon Session (2.5 hours)

**Code Practice Problems (2 hours)**
- [ ] LeetCode 56: Merge Intervals (🟡 Medium)
  - [ ] Sorting time: ____ min
  - [ ] Implementation time: ____ min
  - [ ] Complexity: Time ____, Space ____

- [ ] LeetCode 57: Insert Interval (🟡 Medium)
  - [ ] Naive (sort all): time ____
  - [ ] Optimized (smart insertion): time ____
  - [ ] Why faster? _______________

- [ ] LeetCode 23: Merge K Sorted Lists (🔴 Hard)
  - [ ] Compare approaches: pairwise vs heap
  - [ ] Heap approach time: ____
  - [ ] Heap approach space: ____

**Reflection & Integration (30 min)**
- [ ] When would you NOT need to sort intervals?
- [ ] Real system: Calendar scheduling, meetings conflicting. How?
- [ ] Connection to Day 1/2: Do you need hash or stack for intervals?

### Evening Checkpoint
- [ ] Can merge intervals without sorting reference? ☐ Yes ☐ No
- [ ] Understand insert interval optimization? ☐ Yes ☐ Partial
- [ ] Ready for Day 4? ☐ Yes ☐ Review needed

---

## 📅 THURSDAY: Day 4A & 4B — Partition & Kadane's Algorithm

### Morning Session (2 hours)

**Read Instructional Content (45 min)**
- [ ] Read Week 05 Day 4A: Partition & Cyclic Sort
- [ ] Read Week 05 Day 4B: Kadane's Algorithm
- [ ] Key concepts:
  - 4A: Dutch flag (3 pointers), cyclic sort (position-based)
  - 4B: Kadane tracks local_max vs global_max, DP thinking
- [ ] Mark surprises:
  1. ___________________________________
  2. ___________________________________

**Visualize & Trace (45 min)**
- [ ] Draw Dutch flag for [1,0,2,1,0,2] (0-1-2 sorting)
  - [ ] Show three pointers at each step
  - [ ] Mark what happens when element doesn't move
- [ ] Trace Kadane's for [-2,1,-3,4,-1,2,1,-5,4]
  - [ ] Show local_max and global_max at each iteration
  - [ ] Identify maximum subarray
- [ ] Trace cyclic sort for [3,4,0,1,2]
  - [ ] Show position-value mismatches

**Comprehension Check (20 min)**
- [ ] Why O(1) space for partition vs O(n) for hash?
- [ ] Kadane vs greedy: what's the DP insight?
- [ ] Cyclic sort: why position matters, not just value

### Afternoon Session (2.5 hours)

**Code Practice Problems (2 hours)**
- [ ] LeetCode 75: Sort Colors (🟡 Medium) — Dutch Flag
  - [ ] Three pointer positions: clear? ☐ Yes
  - [ ] Time: ______ min
  - [ ] Tested on: [2,0,2,1,1,0] ✓

- [ ] LeetCode 53: Maximum Subarray (🟡 Medium) — Kadane's
  - [ ] All-negative case handled? ☐ Yes
  - [ ] Code time: ____ min
  - [ ] Compared to brute force? ✓

- [ ] LeetCode 41: First Missing Positive (🔴 Hard) — Cyclic Sort
  - [ ] Understood position = value - 1? ☐ Yes
  - [ ] Time attempt 1: ____ min
  - [ ] If stuck, reviewed solution ☐

**Reflection & Integration (30 min)**
- [ ] Partition + hash: when combine?
- [ ] Kadane + Day 1/2 patterns: possible?
- [ ] Cyclic sort insight: why not just mark in array?

### Evening Checkpoint
- [ ] Can code Dutch flag from scratch? ☐ Yes ☐ No
- [ ] Kadane's logic crystal clear? ☐ Yes ☐ Need review
- [ ] Ready for Day 5? ☐ Yes ☐ Review needed

---

## 📅 FRIDAY: Day 5 Core — Fast-Slow Pointers

### Morning Session (2 hours)

**Read Instructional Content (45 min)**
- [ ] Read Week 05 Day 5: Fast-Slow Pointers & Floyd's Cycle Detection
- [ ] Key concept: Two pointers at different speeds, meeting guarantees cycle
- [ ] Mark surprises:
  1. ___________________________________
  2. ___________________________________
  3. ___________________________________

**Visualize & Trace (30 min)**
- [ ] Draw fast-slow convergence for linked list with cycle
  - [ ] Show slow at 1 step, fast at 2 steps each iteration
  - [ ] Mark meeting point
- [ ] Trace finding cycle start using pointer reset trick
  - [ ] Show why head + meeting point = cycle start
- [ ] Trace midpoint finding for even/odd length lists

**Comprehension Check (15 min)**
- [ ] Why O(n) time and O(1) space?
- [ ] Compare to cycle detection with hash: trade-offs?
- [ ] When would you use fast-slow vs other patterns?

### Afternoon Session (2.5 hours)

**Code Practice Problems (2 hours)**
- [ ] LeetCode 141: Linked List Cycle (🟢 Easy)
  - [ ] Implementation time: ____ min
  - [ ] Space used: ____ (verify O(1))

- [ ] LeetCode 142: Linked List Cycle II (🟡 Medium)
  - [ ] Pointer reset trick understood? ☐ Yes
  - [ ] Why it works mathematically? _______________
  - [ ] Time: ____ min

- [ ] LeetCode 876: Middle of the Linked List (🟢 Easy)
  - [ ] Even/odd cases handled? ☐ Yes
  - [ ] Used fast-slow? ☐ Yes

**Reflection & Integration (30 min)**
- [ ] Fast-slow + hash: cycle detection improvements?
- [ ] Could Kadane's or partition use fast-slow? Why/why not?
- [ ] Generalization: cycles in non-linked structures?

### Evening Checkpoint
- [ ] Can code cycle detection from scratch? ☐ Yes ☐ No
- [ ] Pointer reset logic makes sense? ☐ Yes ☐ Partial
- [ ] Ready for integration? ☐ Yes ☐ Review needed

---

## 📅 SATURDAY: Integration & Synthesis

### Morning Session (2 hours)

**Review All 6 Patterns (90 min)**
- [ ] Hash: 1 min explanation from scratch
- [ ] Stack: 1 min explanation from scratch
- [ ] Intervals: 1 min explanation from scratch
- [ ] Partition: 1 min explanation from scratch
- [ ] Kadane's: 1 min explanation from scratch
- [ ] Fast-Slow: 1 min explanation from scratch
- [ ] Total time for all 6: _______ min (should be < 10 min)

**Pattern Decision Matrix (30 min)**
- [ ] Given 10 problems, classify each to pattern:
  - [ ] Problem 1: Pattern ____________ (confidence: %)
  - [ ] Problem 2: Pattern ____________ (confidence: %)
  - [ ] ...continue for 10 total
  - [ ] Accuracy: ____ / 10 correct

### Afternoon Session (2 hours)

**Integration Problems (120 min)**
- [ ] Pick 2 hard problems that mix patterns
  - [ ] Problem A: ____________
    - [ ] Patterns needed: _______ + ________
    - [ ] Time: ____ min (first attempt)
    - [ ] After discussion: ✓
  
  - [ ] Problem B: ____________
    - [ ] Patterns needed: _______ + ________
    - [ ] Time: ____ min (first attempt)
    - [ ] After discussion: ✓

### Evening Session (1 hour)

**Weekly Reflection**
- [ ] Most confident pattern: ______________
- [ ] Pattern needing more practice: ______________
- [ ] Pattern combination that clicked: ____ + ____
- [ ] Real system I'd apply Week 05 to: ______________
- [ ] One insight I'll remember forever: _________________

---

## 🎯 MASTERY VERIFICATION

**By end of Week 05, validate:**

- [ ] **Hash:** Code two-sum, frequency counting, membership test without reference (3/3)
- [ ] **Stack:** Code next greater, stock span without reference (2/2)
- [ ] **Intervals:** Code merge, insert interval without reference (2/2)
- [ ] **Partition:** Code Dutch flag, cyclic sort without reference (2/2)
- [ ] **Kadane:** Code max subarray, max product without reference (2/2)
- [ ] **Fast-Slow:** Code cycle detection, midpoint without reference (2/2)

**Total: 6 patterns × 2-3 problems each = 14-18 problems this week**

**Final Assessment:**
- [ ] Can identify pattern instantly (<10 sec) for any problem
- [ ] Can code any pattern without hesitation
- [ ] Can combine patterns for complex problems
- [ ] Understand time/space tradeoffs
- [ ] Ready for interviews? ☐ YES

---

## 📝 Notes Section

**Breakthroughs this week:**
- _____________________________
- _____________________________
- _____________________________

**Areas to review before interview:**
- _____________________________
- _____________________________

**Favorite pattern explanation to teach others:**
- _____________________________

---

**Total Time Investment:** 15-18 hours (core)  
**Pattern Mastery Level:** ______ / 100  
**Ready for Week 06?** ☐ YES (Move forward) ☐ REVIEW (One more day)
