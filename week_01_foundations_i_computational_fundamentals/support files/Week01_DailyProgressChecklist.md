# ✅ Week 01 Daily Progress Checklist: Action Plan & Execution

**Status:** Daily Accountability Tracker  
**Audience:** Self-directed learners  
**Purpose:** Track daily progress, ensure depth not just breadth

---

## 📅 MONDAY: Day 1 Core — RAM Model, Virtual Memory, Pointers

### Morning Session (2.5 hours)

**Read Instructional Content (50 min)**
- [ ] Read Week 01 Day 1: RAM Model, Virtual Memory, Pointers
- [ ] Focus areas: address spaces, pointers, virtual memory, caches
- [ ] Write down 3 key insights:
  1. ___________________________________
  2. ___________________________________
  3. ___________________________________

**Visualize & Understand (60 min)**
- [ ] Draw process address space: code, data, heap, stack
  - [ ] High to low addresses
  - [ ] Heap grows up, stack grows down
- [ ] Trace pointer: variable x, pointer p = &x, dereference *p
- [ ] Draw memory hierarchy: registers → L3 cache → DRAM → disk
  - [ ] Access times for each level

**Comprehension Check (20 min)**
- [ ] What's in a stack frame?
- [ ] What causes page fault?
- [ ] Why is contiguous memory faster?

### Afternoon Session (2.5 hours)

**Practice Understanding (1.5 hours)**
- [ ] Calculate addresses: base=0x1000, element at index 5, size 4 bytes
- [ ] Pointer arithmetic: p+1 means what?
- [ ] Measure on your system:
  - [ ] Stack size (ulimit -s)
  - [ ] Cache line size (getconf LEVEL1_DCACHE_LINESIZE)
  - [ ] Pointer size (sizeof(void*))

**System Exploration (1 hour)**
- [ ] Visualize virtual memory: man mmap, understand page tables
- [ ] Understand cache behavior: sequential vs random access
- [ ] Recognize false sharing scenario

### Evening Checkpoint
- [ ] Can explain address space? ☐ Yes ☐ Partial ☐ No
- [ ] Can explain pointers? ☐ Yes ☐ Partial ☐ No
- [ ] Understand virtual memory? ☐ Yes ☐ Partial ☐ No
- [ ] Ready for Day 2? ☐ Yes ☐ Review needed

---

## 📅 TUESDAY: Day 2 Core — Big-O Notation & Asymptotic Analysis

### Morning Session (2 hours)

**Read Instructional Content (45 min)**
- [ ] Read Week 01 Day 2: Big-O, Big-Ω, Big-Θ, Complexity Classes
- [ ] Key concepts: upper bound, lower bound, tight bounds

**Visualize (75 min)**
- [ ] Draw growth curves for O(1), O(log n), O(n), O(n log n), O(n²), O(2^n)
  - [ ] n=10, 100, 1000, 10^6
  - [ ] See where they cross
- [ ] Example: binary search is O(log n)
  - [ ] Recurrence: T(n) = T(n/2) + O(1)
  - [ ] Recurrence tree: depth log n

### Afternoon Session (3 hours)

**Classify Algorithms (2 hours)**
- [ ] Analyze: linear scan → O(n)
- [ ] Analyze: binary search → O(log n)
- [ ] Analyze: bubble sort → O(n²)
- [ ] Analyze: merge sort → O(n log n)
- [ ] Prove each via recurrence tree

**Practice (1 hour)**
- [ ] Prove: T(n)=T(n/2)+1 is O(log n)
- [ ] Prove: T(n)=2T(n/2)+n is O(n log n)
- [ ] Prove: T(n)=T(n-1)+O(1) is O(n)

### Evening Checkpoint
- [ ] Can classify algorithm complexity? ☐ Yes ☐ Partial ☐ No
- [ ] Can prove via recurrence tree? ☐ Yes ☐ Partial ☐ No
- [ ] Ready for Day 3? ☐ Yes ☐ Review needed

---

## 📅 WEDNESDAY: Day 3 Core — Space Complexity & Memory Usage

### Morning Session (1.5 hours)

**Read Instructional Content (40 min)**
- [ ] Read Week 01 Day 3: Space Complexity, Stack, Heap

**Visualize (50 min)**
- [ ] Draw stack frame for function call
  - [ ] Parameters, locals, return address
- [ ] Trace deep recursion: fact(5)
  - [ ] Show each frame pushed
  - [ ] Calculate stack space used
- [ ] Draw heap allocations and fragmentation

### Afternoon Session (2.5 hours)

**Analyze Space (1.5 hours)**
- [ ] Recursive function: fact(n) space is O(n)?
  - [ ] Trace, count frames
- [ ] Recursive function: sum(arr, i) space is O(n)?
  - [ ] Linear vs tree recursion
- [ ] Array vs linked list space overhead
  - [ ] Pointers, metadata, alignment

**Practice (1 hour)**
- [ ] Calculate stack depth on your system
  - [ ] Measure recursion limit (approximate)
- [ ] Estimate heap allocation for 1 million objects

### Evening Checkpoint
- [ ] Understand stack depth limits? ☐ Yes ☐ Partial ☐ No
- [ ] Can analyze space complexity? ☐ Yes ☐ Partial ☐ No
- [ ] Ready for Day 4? ☐ Yes ☐ Review needed

---

## 📅 THURSDAY: Day 4 Core — Recursion I (Call Stack & Patterns)

### Morning Session (2 hours)

**Read Instructional Content (50 min)**
- [ ] Read Week 01 Day 4: Recursion, Call Stack, Basic Patterns

**Visualize (70 min)**
- [ ] Draw recursion tree for fact(5)
- [ ] Draw recursion tree for fib(5): show repeated calls
  - [ ] fib(3) appears multiple times
- [ ] Identify overlapping subproblems

### Afternoon Session (3 hours)

**Code & Trace (2 hours)**
- [ ] Implement factorial (recursive)
  - [ ] Time: _____ min
  - [ ] Trace on paper: fact(5) → 5 * fact(4) → ...
- [ ] Implement Fibonacci (naive)
  - [ ] Time: _____ min
  - [ ] Identify exponential blowup
- [ ] Implement sum of array (recursive)
  - [ ] Time: _____ min

**Analysis (1 hour)**
- [ ] Complexity of each:
  - [ ] Factorial: O(n) time, O(n) space
  - [ ] Fib naive: O(2^n) time
  - [ ] Sum: O(n) time, O(n) space

### Evening Checkpoint
- [ ] Can trace recursion manually? ☐ Yes ☐ Partial ☐ No
- [ ] Understand call stack mechanics? ☐ Yes ☐ Partial ☐ No
- [ ] Ready for Day 5? ☐ Yes ☐ Review needed

---

## 📅 FRIDAY: Day 5 Core — Recursion II (Patterns & Memoization)

### Morning Session (2 hours)

**Read Instructional Content (50 min)**
- [ ] Read Week 01 Day 5: Recursion Patterns, Memoization

**Visualize (70 min)**
- [ ] Naive fib(5): Show all calls, count duplicates
  - [ ] fib(3) called how many times?
- [ ] With memoization: Reuse cached results
  - [ ] Drastically fewer calls

### Afternoon Session (3 hours)

**Implement (2 hours)**
- [ ] Memoized Fibonacci
  - [ ] Time: _____ min
  - [ ] Complexity: O(n) instead of O(2^n)
  - [ ] Verify: fib(30) fast with memo, slow without
- [ ] Another problem with memoization (e.g., climbing stairs)
  - [ ] Time: _____ min

**Analysis (1 hour)**
- [ ] When is memoization helpful?
- [ ] When is memoization useless (no overlapping subproblems)?
- [ ] Space cost of memoization?

### Evening Checkpoint
- [ ] Implement memoization? ☐ Yes ☐ Partial ☐ No
- [ ] Recognize overlapping subproblems? ☐ Yes ☐ Partial ☐ No
- [ ] Ready for Day 6? ☐ Yes ☐ Review needed

---

## 📅 SATURDAY: Day 6 Optional — Peak Finding & Algorithmic Thinking

### Morning Session (2 hours)

**Read Instructional Content (45 min)**
- [ ] Read Week 01 Day 6: Peak Finding, Algorithmic Design

**Visualize (75 min)**
- [ ] 1D peak: [10, 20, 15, 30, 25]
  - [ ] Brute force: scan, find peaks
- [ ] Divide-conquer: check mid, recurse on larger side
  - [ ] Trace on array above
- [ ] 2D peak: matrix
  - [ ] Column-local max approach

### Afternoon Session (2 hours)

**Implement (1.5 hours)**
- [ ] 1D peak finding O(log n)
  - [ ] Time: _____ min
  - [ ] Correct? ☐ Yes
  - [ ] Test: edge cases (size 1, 2, duplicates)
- [ ] 2D peak finding
  - [ ] Time: _____ min

**Analysis (30 min)**
- [ ] Why divide-conquer works?
- [ ] Complexity analysis
- [ ] Compare to brute force

### Evening: Week Integration (1.5 hours)

**Review All 5 Days (45 min)**
- [ ] Memory: < 2 min explanation
- [ ] Big-O: < 2 min explanation
- [ ] Space: < 2 min explanation
- [ ] Recursion: < 2 min explanation
- [ ] Peak finding: < 2 min explanation
- [ ] Total: _____ min (target < 15 min)

**Synthesize (45 min)**
- [ ] Connect all concepts: memory limits recursion depth
- [ ] Complexity analysis: Big-O is simplified but useful
- [ ] Algorithmic thinking: exploit structure like in peak finding
- [ ] Key insight: ___________________________________

---

## 🎯 MASTERY VERIFICATION

**By end of Week 01, validate:**

- [ ] Memory:
  - [ ] Draw address space
  - [ ] Explain pointers, virtual memory
  - [ ] Understand TLB, caches

- [ ] Complexity:
  - [ ] Classify algorithms
  - [ ] Prove via recurrence tree
  - [ ] Compare functions

- [ ] Space:
  - [ ] Analyze stack depth
  - [ ] Understand heap allocation
  - [ ] Time-space trade-offs

- [ ] Recursion:
  - [ ] Trace manually
  - [ ] Recognize patterns
  - [ ] Apply memoization

- [ ] Peak Finding:
  - [ ] Implement 1D O(log n)
  - [ ] Implement 2D
  - [ ] Understand why it works

**Final Assessment:**
- [ ] Understand fundamentals deeply? ☐ YES ☐ PARTIAL
- [ ] Can explain to someone else? ☐ YES ☐ PARTIAL
- [ ] Ready for Week 02? ☐ YES ☐ REVIEW

---

**Status:** Week 01 Daily Checklist Complete  
**Total Time Investment:** 20+ hours  
**Mastery Level:** ______ / 100  
**Ready for Week 02?** ☐ YES (Move forward) ☐ REVIEW (One more day)