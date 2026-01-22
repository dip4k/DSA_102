# 📋 Week 04 Guidelines: Pattern Foundations (Two-Pointer, Sliding Window, Divide-Conquer, Binary Search)

**Week Overview:** Problem-Solving Patterns I — Foundational patterns that solve 20-30% of interview problems  
**Status:** Core Curriculum Week (Days 1-5) + Optional Advanced (Day 6)  
**Total Content:** 5 comprehensive instructional files + 5 support guides  
**Time Allocation:** 15-18 hours core learning + practice  

---

## 🎯 Week Learning Arc

This week is **your first major pattern week**—the bridge from foundations (Weeks 1-3) to advanced patterns. You'll master four fundamental patterns that appear in countless interview problems:

**Days 1-5:** Master canonical patterns (two-pointer, sliding windows fixed/variable, divide-conquer, binary search)  
**Day 6:** Optional advanced (design pattern strategies)

By week's end, you'll recognize these patterns instantly and know when to apply each one.

---

## 📚 Day-by-Day Concept Overview

### Day 1: Two-Pointer Patterns
**Core Mental Model:** Two pointers (same direction or opposite) maintaining invariants as they move

**Key Techniques:**
- Same-direction merge (two sorted arrays/lists) — O(n+m) time
- Removing duplicates in-place — O(n) with O(1) space
- Opposite-direction container with most water — O(n) single pass

**Why It Matters:** Two-pointer teaches you to maintain invariants while optimizing. Most two-sum variants use this thinking.

**Connection:** Builds on Week 2 (arrays/lists). Next week hash tables will speed up complement lookups.

---

### Day 2: Sliding Window — Fixed Size
**Core Mental Model:** Move a fixed-size window across array, computing results incrementally

**Key Techniques:**
- Running sum of k-element windows (O(n) not O(nk))
- Maximum/minimum in sliding window using deque optimization
- Pattern: initialize first window, then slide by removing left, adding right

**Why It Matters:** Sliding window teaches incremental computation. Transforms naive O(nk) into O(n).

**Connection:** Day 1's two-pointer thinking extended. Day 3's variable window is natural progression.

---

### Day 3: Sliding Window — Variable Size
**Core Mental Model:** Expand/contract window to maintain constraint (e.g., "at most K distinct")

**Key Techniques:**
- At-most K distinct characters (frequency map + two-pointer)
- Longest substring without repeating (index map for quick lookup)
- Shrink when constraint violated, track best while valid

**Why It Matters:** Variable window is more complex but crucial for substring problems. O(n) if done right.

**Connection:** Combines Day 1 (two-pointer thinking) + Day 2 (window pattern) + data structures (hash for counts).

---

### Day 4: Divide-and-Conquer Pattern
**Core Mental Model:** Split problem, solve subproblems independently, combine results

**Key Techniques:**
- Merge sort: divide, recursively sort, merge (O(n log n) worst-case)
- Majority element: divide, find majorities in halves, decide with counts
- Counting inversions: divide-conquer extension of merge sort

**Why It Matters:** Divide-conquer teaches recursive thinking and is precursor to all tree/graph algorithms.

**Connection:** Uses recursion from Week 1. Leads to binary search thinking Day 5. Foundation for merge operations (Week 5).

---

### Day 5: Binary Search as Pattern
**Core Mental Model:** Maintain invariant (target lies in [low, high)), narrow search space geometrically

**Key Techniques:**
- Binary search on sorted arrays (classic)
- Binary search on answer space (feasibility checking)
- Minimize maximum load, maximize minimum distance problems

**Why It Matters:** Binary search teaches "search by guess" and applies far beyond sorted arrays. Essential for optimization problems.

**Connection:** Uses divide-conquer thinking. Leads to binary search tree (Week 7) and many optimization problems.

---

## 🎓 Eight Core Learning Objectives

By end of Week 04, you will be able to:

1. **Use two pointers effectively** for merge, removal, and bidirectional traversal problems
2. **Implement sliding window** with fixed size in O(n) time
3. **Solve variable-window problems** with proper shrinking logic
4. **Divide and conquer** to reduce complexity from brute force
5. **Binary search beyond sorted arrays** using feasibility functions
6. **Combine patterns** (two-pointer + binary search, sliding + divide-conquer)
7. **Analyze invariants** that make patterns work
8. **Optimize from O(n²) to O(n)** or O(n log n) confidently

---

## 🧭 How to Study Effectively

### Strategy 1: Invariant-First Thinking

**Wrong approach:** See problem → code → debug

**Right approach:** See problem → identify invariant → maintain invariant while moving pointers/windows

Examples:
- Two-pointer merge: "left subarray ≤ all unseen, right subarray ≤ all unseen"
- Sliding window: "window[left..right] satisfies constraint"
- Binary search: "target lies in [low, high)"

### Strategy 2: Animation Over Code

For each pattern:
1. Read explanation
2. Manually animate on paper (show state at each step)
3. Draw pointers/windows moving
4. Mark when invariant is established/maintained
5. Only then code

### Strategy 3: Complexity Before Implementation

Ask BEFORE coding:
- "Is this O(n) or O(n²)? Why?"
- "What state must I track?"
- "When do pointers move? How?"
- "What data structure helps?"

### Strategy 4: Pattern Recognition Drills

**Daily 10-minute drill:** See problem title, identify pattern < 5 seconds:
- "Merge sorted arrays" → Two-pointer same-direction
- "Longest substring with K distinct" → Sliding window variable
- "Maximum in subarrays of size K" → Sliding window fixed + deque
- "Minimize maximum load" → Binary search on answer

---

## ⏱️ Recommended Time Allocation

| Activity | Hours | Focus |
| :--- | :--- | :--- |
| Read Day 1-2 instructional | 2.5 | Two-pointer, sliding window fixed |
| Practice Day 1-2 problems | 3 | 8-10 LeetCode problems total |
| Read Day 3-4 instructional | 2.5 | Sliding variable, divide-conquer |
| Practice Day 3-4 problems | 3 | 8-10 LeetCode problems total |
| Read Day 5 instructional | 1.5 | Binary search patterns |
| Practice Day 5 problems | 2 | 4-5 LeetCode problems |
| Integration & synthesis | 1.5 | Review all 4, connect patterns |
| Optional Day 6 (advanced) | 1-2 | Advanced pattern design |
| **Total Core (Days 1-5)** | **16-17 hours** | **Interview prep complete** |
| **Total with Day 6** | **17-19 hours** | **Mastery level** |

---

## 🚫 Five Common Pitfalls (and How to Avoid Them)

### Pitfall 1: Moving Pointers Without Maintaining Invariant

**Wrong:** Moving left pointer randomly without tracking what's been processed

**Reality:** Invariant breaks, algorithm produces wrong results

**Fix:** Always ask "what does my invariant guarantee?" before moving a pointer. Make invariant explicit in code comments.

---

### Pitfall 2: Sliding Window Without Tracking State

**Wrong:** Using window size without tracking frequency map or constraints

**Reality:** Window becomes invalid, you don't know when to shrink

**Fix:** Maintain explicit constraint state. For frequency, track count of distinct chars in window. For substring, track last-index of each char.

---

### Pitfall 3: Divide-Conquer Termination

**Wrong:** Forgetting base case or not making progress toward base case

**Reality:** Infinite recursion or incomplete solution

**Fix:** Verify: base case exists, recursive calls make problem strictly smaller, all sub-results combine correctly.

---

### Pitfall 4: Binary Search Off-by-One

**Wrong:** Using `mid = (low + high) / 2` which overflows, or wrong termination condition

**Reality:** Infinite loops or incorrect answer on boundary cases

**Fix:** Use `mid = low + (high - low) / 2`. Verify termination: what's the invariant when loop exits?

---

### Pitfall 5: Not Recognizing Pattern Variants

**Wrong:** Solving each problem from scratch instead of recognizing pattern

**Reality:** Inefficient problem-solving, slow interviews

**Fix:** Spend 30 seconds asking: "Is this two-pointer? Sliding window? Binary search? Or combination?" Pattern recognition is 80% of interview success.

---

## 📊 Concept Map: How Week 04 Patterns Connect

```
Week 04: Pattern Foundations

├─ TWO-POINTER (Dual Movement)
│  ├─ Same-direction (merge, duplicates)
│  └─ Opposite-direction (water, sum in sorted)
│
├─ SLIDING WINDOW (Incremental Computation)
│  ├─ Fixed-size (running sum, max in window)
│  └─ Variable-size (at-most K, longest substring)
│
├─ DIVIDE-CONQUER (Recursive Decomposition)
│  ├─ Divide: split problem
│  ├─ Conquer: solve subproblems
│  └─ Combine: merge results
│
└─ BINARY SEARCH (Geometric Narrowing)
   ├─ On sorted arrays (classic)
   └─ On answer space (feasibility check)
```

---

## 🎯 Seven Key Insights to Remember

1. **Invariants are everything.** If you can state the invariant, you can design the pattern. If you can't, you'll get lost.

2. **Sliding window is O(n) only if each element moves left and right exactly once.** Don't mess this up—don't reset pointers inside loop.

3. **Divide-conquer is recursive and requires three things:** split, conquer, combine. Missing any one and it's not divide-conquer.

4. **Binary search works on ANY monotone property**, not just sorted arrays. "Can we achieve X with budget Y?" is binary searchable if monotone.

5. **Two pointers teach you to think in invariants and state.** This thinking transfers to graphs, DP, and advanced patterns.

6. **Combine patterns:** sliding window + two-pointer, binary search + divide-conquer. Mastery is knowing when and how to combine.

7. **O(n) is better than O(n log n) which is better than O(n²).** But understand WHY—algorithmic thinking beats code tricks every time.

---

## 🔄 How Week 04 Connects to Surrounding Weeks

**From Week 03 (Sorting & Hashing):**
- Sorting (merge sort) → divide-conquer thinking
- Hashing (frequency maps) → used in sliding window
- Binary search on sorted arrays → foundation

**To Week 05 (Hash, Monotonic Stack, Intervals):**
- Two-pointer merge → Day 3 (merge intervals)
- Sliding window + hash → Day 1 (hash complement)
- Binary search → Day 4 (partition thinking)

**To Week 06 (String Patterns):**
- Sliding window (Day 2-3) → substring problems extensively
- Two-pointer (Day 1) → palindrome detection
- Binary search → applicable to strings

**To Week 07 (Trees):**
- Divide-conquer → tree traversals naturally
- Binary search → binary search trees
- Two-pointer → level-order thinking

---

## ✅ Weekly Checklist

By end of week, you should be able to:

- [ ] Merge two sorted arrays in-place with O(1) extra space
- [ ] Implement sliding window for fixed-size problems (3+ examples)
- [ ] Solve variable-window problems with proper constraint tracking
- [ ] Divide-conquer a problem from scratch (write recurrence)
- [ ] Binary search on answer space (capacity, distance problems)
- [ ] Combine patterns for complex problems (2-3 examples)
- [ ] Identify pattern instantly for any problem (< 10 sec)
- [ ] Solve 15-20 problems from this week's patterns
- [ ] Trace through at least one problem per pattern without running code
- [ ] Explain trade-offs (time, space, code complexity) for each pattern
- [ ] List real-world systems using each pattern

---

## 🎓 Mastery Signals

You've mastered Week 04 when:

1. **You see patterns in problems instantly.** Someone shows you a problem; you immediately identify "this needs two-pointer merge" or "this is binary search on answer."

2. **You code without hesitation.** Implement two-pointer, sliding windows, divide-conquer, binary search without constantly re-reading logic.

3. **You handle edge cases proactively.** Before coding core logic, you list and guard against boundary conditions.

4. **You optimize instinctively.** Instead of coding O(n²), you recognize the O(n) pattern immediately.

5. **You maintain invariants explicitly.** Your code comments state invariants and you verify them mentally before submitting.

6. **You trace manually.** You can trace code on paper and predict output without running it for all 4 patterns.

7. **You combine patterns.** You see when to use binary search within two-pointer, or divide-conquer before sliding window.

---

**Status:** Week 04 Guidelines Complete  
**Next:** Week 04 Summary & Key Concepts  
**Time to Completion:** 15-18 hours core learning