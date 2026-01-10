# ✅ Week 02 Daily Progress Checklist: Detailed Execution Plan

**Status:** Daily Accountability Tracker  
**Purpose:** Track progress, ensure mastery

---

## 📅 MONDAY: Day 1 Core — Static & Dynamic Arrays

### Morning Session (2.5 hours)

**Read Instructional (45 min)**
- [ ] Read Week 02 Day 1: Static & Dynamic Arrays
- [ ] Key concepts: address formula, doubling, amortized
- [ ] 3 insights:
  1. ___________________________________
  2. ___________________________________
  3. ___________________________________

**Visualize & Understand (75 min)**
- [ ] Draw static array memory layout (base=0x1000, elements 4 bytes each)
  - [ ] Index 0 → address 0x1000
  - [ ] Index 5 → address 0x1014
- [ ] Draw dynamic array growth (sizes 1→2→4→8)
  - [ ] Show reallocation at each doubling
  - [ ] Calculate total work: 1+2+4+8 = 15 vs O(n)=8

### Afternoon Session (3 hours)

**Implement (2 hours)**
- [ ] Static array class with address calculation
  - [ ] Time: _____ min
- [ ] Dynamic array with doubling
  - [ ] grow() method
  - [ ] Time: _____ min
  - [ ] Test: insert 10 elements, verify capacities 1,2,4,8

**Trace & Analysis (1 hour)**
- [ ] Manually trace 5 inserts, show reallocation
- [ ] Amortized analysis: total work = 1+2+4 = 7 for 4 inserts
- [ ] Per insert: 7/4 ≈ O(1)

### Evening: Ready for Day 2? ☐ Yes ☐ No

---

## 📅 TUESDAY: Day 2 Core — Linked Lists

### Morning Session (2.5 hours)

**Read Instructional (50 min)**
- [ ] Read Week 02 Day 2: Linked Lists
- [ ] Node structure, operations, trade-offs

**Visualize (75 min)**
- [ ] Draw singly linked list: Head → [10|next] → [20|next] → null
- [ ] Show insert at position 1: create node, update pointers
- [ ] Show delete at head: update Head pointer
- [ ] Trace reverse operation on [1→2→3→null]

### Afternoon Session (3 hours)

**Implement (2.5 hours)**
- [ ] Singly linked list: insert, delete, search
  - [ ] Time: _____ min
  - [ ] Test: [10,20,30], insert 25 at position 2
- [ ] Reverse linked list (iterative)
  - [ ] Time: _____ min
  - [ ] Pointer updates clear? ☐ Yes
- [ ] Find middle with two pointers
  - [ ] Slow and fast pointers
  - [ ] Odd vs even length

**Analysis (30 min)**
- [ ] Compare to array: insert middle is O(n) both!
  - [ ] Why linked list doesn't win?
  - [ ] Cache locality difference

### Evening: Ready for Day 3? ☐ Yes ☐ No

---

## 📅 WEDNESDAY: Day 3 Core — Stacks & Queues

### Morning Session (2 hours)

**Read Instructional (45 min)**
- [ ] Read Week 02 Day 3: Stacks, Queues, Deques

**Visualize (75 min)**
- [ ] Stack with array: push 1,2,3; show top pointer
- [ ] Queue with circular buffer: enqueue/dequeue wraparound
- [ ] Deque with both pointers

### Afternoon Session (2.5 hours)

**Implement (1.5 hours)**
- [ ] Stack with array (fixed size)
- [ ] Queue with circular buffer
  - [ ] Wraparound logic
  - [ ] Distinguish full vs empty
- [ ] Min stack (auxiliary stack)

**Applications (1 hour)**
- [ ] Valid parentheses using stack
- [ ] Level-order BFS using queue

### Evening: Ready for Days 4-5? ☐ Yes ☐ No

---

## 📅 THURSDAY: Day 4 Core — Binary Search (Part 1)

### Morning Session (2.5 hours)

**Read Instructional (50 min)**
- [ ] Read Week 02 Day 4-5: Binary Search

**Visualize (75 min)**
- [ ] Draw binary search tree for [1,3,5,7,9,11], target=7
  - [ ] Iteration 1: low=0, high=6, mid=3, arr[3]=7 ✓
- [ ] Draw first occurrence on [1,2,2,2,3]
  - [ ] Multiple iterations needed
- [ ] Answer space: minimize max load
  - [ ] Binary search on capacity 1..100
  - [ ] Feasibility check: greedy assignment

### Afternoon Session (3 hours)

**Code Binary Search (2 hours)**
- [ ] Classic binary search
  - [ ] Time: _____ min, correct? ☐ Yes
  - [ ] Invariant [low, high) maintained? ☐
- [ ] First occurrence
  - [ ] Modification: when high=mid vs mid+1?
  - [ ] Test on [1,2,2,2,3] for target=2
- [ ] Answer space: capacity planning
  - [ ] Feasibility function
  - [ ] Binary search to find minimum

**Analysis (1 hour)**
- [ ] Trace each variant, mark invariant at each step
- [ ] Why O(log n)?

### Evening: Ready for more? ☐ Yes ☐ No

---

## 📅 FRIDAY: Day 5 Core — Binary Search (Part 2)

### Morning Session (2.5 hours)

**Implement Variants (1.5 hours)**
- [ ] Last occurrence
- [ ] Lower bound (first ≥)
- [ ] Upper bound (first >)
- [ ] Test on [1,2,2,2,3]: target=2
  - [ ] First: index 1
  - [ ] Last: index 3
  - [ ] Lower: 1
  - [ ] Upper: 4

**Advanced Problems (1 hour)**
- [ ] Rotated sorted array peak
- [ ] Minimize maximum load (detailed)
- [ ] Aggressive cows: maximize min distance

### Afternoon Session (3 hours)

**Practice (3 hours)**
- [ ] 5-6 binary search problems
  - [ ] LeetCode: basic, variants, answer space mix
  - [ ] Times: _____, _____, _____ (should decrease)
- [ ] Answer space: at least 2 problems

---

## 📅 SATURDAY: Integration & Synthesis

### Morning (2 hours)

**Review All (90 min)**
- [ ] Arrays: < 2 min explanation
- [ ] Linked lists: < 2 min explanation
- [ ] Stacks/queues: < 2 min explanation
- [ ] Binary search: < 2 min explanation
- [ ] When to use each: < 1 min each
- [ ] Total: _____ min (target < 15 min)

**Decision Quiz (30 min)**
- [ ] Given 8 scenarios, choose structure:
  1. Need random access → _______________
  2. Frequent insertion at head → _______________
  3. FIFO order needed → _______________
  4. Find in sorted array → _______________
  5. (continue for 8 total)
  - [ ] Accuracy: ____ / 8

### Afternoon (2 hours)

**Integration Problems (2 hours)**
- [ ] LRU cache: hash map + doubly linked list
- [ ] Merge k sorted linked lists
- [ ] Sliding window with deque
- [ ] Times: _____, _____, _____
- [ ] All correct? ☐ Yes ☐ Review

---

## 🎯 MASTERY VERIFICATION

**By end of Week 02, verify:**

- [ ] Dynamic array implementation with doubling
- [ ] Singly linked list (insert, delete, reverse)
- [ ] Stack (array-based)
- [ ] Queue (circular buffer)
- [ ] Deque (both pointers)
- [ ] Binary search (classic)
- [ ] Binary search variants (first/last, bounds)
- [ ] Binary search on answer space (3+ problems)
- [ ] 20-25 problems total completed
- [ ] Can explain amortized analysis
- [ ] Can choose right structure for task

**Final Assessment:**
- [ ] Can code all structures without reference? ☐ Yes ☐ Partial
- [ ] Understand amortized complexity? ☐ Yes ☐ Partial
- [ ] Recognize binary search opportunities? ☐ Yes ☐ Partial
- [ ] Ready for Week 03? ☐ YES

---

**Status:** Week 02 Daily Checklist Complete  
**Total Time:** 22+ hours  
**Mastery Level:** ______ / 100  
**Ready for Week 03?** ☐ YES ☐ REVIEW