# 📖 Week 04 Summary & Key Concepts: Pattern Foundations Reference

**Status:** Graduate-Level Study Notes  
**Audience:** Students completing Week 04 instructional content  
**Purpose:** Comprehensive reference for review, retention, and interview prep

---

## 🎯 Week 04 Narrative Summary

**Week 04: Problem-Solving Patterns I** introduces you to four foundational patterns that solve 20-30% of interview problems. This is your first "pattern week" where you'll transition from learning data structures to recognizing problem families and solving them efficiently.

### The Four Core Patterns

**Day 1: Two-Pointer** — Use two pointers maintaining invariants to solve merge, removal, and bidirectional problems. Teaches invariant thinking.

**Day 2: Sliding Window Fixed** — Move fixed-size window across array, computing results incrementally. O(n) instead of O(nk).

**Day 3: Sliding Window Variable** — Expand/contract window to maintain constraint. Combines two-pointer with state tracking.

**Day 4: Divide-Conquer** — Recursive decomposition: split, conquer subproblems, combine results. Foundation for trees/graphs.

**Day 5: Binary Search as Pattern** — Maintain invariant (target in [low, high)), narrow search space geometrically. Works beyond sorted arrays.

---

## 🧠 Concept Map: Pattern Relationships

```
Week 04 Patterns: Building Blocks

├─ TWO-POINTER (Invariant Maintenance)
│  ├─ Same-direction (merge, remove)
│  ├─ Opposite-direction (water, complement)
│  └─ Key: Maintain "what's processed" invariant
│
├─ SLIDING WINDOW (Incremental State)
│  ├─ Fixed-size (running computation)
│  ├─ Variable-size (constraint satisfaction)
│  └─ Key: Track state, slide in O(1) per step
│
├─ DIVIDE-CONQUER (Recursive Split)
│  ├─ Split: partition problem
│  ├─ Conquer: solve independently
│  └─ Combine: merge sub-solutions
│
└─ BINARY SEARCH (Monotone Narrowing)
   ├─ On arrays (classic)
   ├─ On answer space (feasibility)
   └─ Key: Maintain "target in [low, high)" invariant
```

---

## 📋 Per-Day Concept Summary

### Day 1: Two-Pointer Patterns

**Definition:** Two pointers traverse data structure with maintained invariant.

**Core Properties:**
- Same-direction: both move right, maintaining processed/unprocessed boundary
- Opposite-direction: pointers converge from ends, maintaining symmetry or complement properties
- Invariant is explicit: "left processed, right unprocessed" or "arr[left] + arr[right] = target if answer exists"

**Same-Direction Example: Merge Sorted Arrays**
- Two pointers at start of each array
- Compare, take smaller, advance pointer of that array
- O(n+m) time, can be done in-place if enough space in first array

**Opposite-Direction Example: Container with Most Water**
- Start at widest container (pointers at ends)
- Area = min(height[left], height[right]) × (right - left)
- Move pointer with smaller height (moving larger height can't improve area)
- Single pass O(n), no extra space

**Complexity Table:**

| Pattern | Time | Space | When |
|---------|------|-------|------|
| Merge sorted | O(n+m) | O(1) or O(n+m) | Combine sorted |
| Remove duplicates | O(n) | O(1) | In-place modification |
| Container water | O(n) | O(1) | Max area with height |
| Two-sum sorted | O(n) | O(1) | Find complement |

---

### Day 2: Sliding Window — Fixed Size

**Definition:** Fixed-size window slides across array, computing result incrementally.

**Core Insight:** Each element enters and leaves window once → O(n) total if operation on add/remove is O(1).

**Running Sum (Simplest Case)**
- Initialize: sum of first k elements
- Slide: remove left element, add new right element
- Update: O(1) per slide

**Maximum in K-length Subarrays (Complex Case)**
- Naive: O(nk) — recalculate max every slide
- Optimized: O(n) using deque (double-ended queue)
  - Maintain decreasing deque of indices
  - Remove indices outside window
  - Remove smaller elements when adding new one
  - Max is always at front

**Why Deque?** Because we need both front (max) and back (for insertion) in O(1).

---

### Day 3: Sliding Window — Variable Size

**Definition:** Window expands/contracts to maintain constraint.

**Algorithm:**
1. Expand: add elements to right until constraint broken or reach end
2. When broken: record best and shrink from left
3. Continue until all elements processed

**At-Most K Distinct Characters**
- Constraint: # distinct characters ≤ K
- Expand right, add char to frequency map
- When # distinct > K: shrink left until back to K
- Track longest valid window

**Key Properties:**
- Each element enters and leaves exactly once → O(n)
- Frequency map must track current count
- When shrinking, decrement counts and remove if 0

**Why O(n) not O(n²)?** Because each element moves left pointer at most once total (not once per comparison).

---

### Day 4: Divide-Conquer Pattern

**Definition:** Recursively split problem, solve subproblems independently, combine results.

**Three Essential Parts:**
1. **Base case:** Problem size = 1 (trivial)
2. **Divide:** Split into k subproblems of size n/k
3. **Conquer:** Recursively solve subproblems
4. **Combine:** Merge k solutions into complete solution

**Merge Sort: The Canonical Example**
- Divide: split array in half
- Conquer: recursively sort each half
- Combine: merge two sorted arrays
- Recurrence: T(n) = 2·T(n/2) + O(n) → O(n log n)

**Majority Element: Alternative Example**
- Divide: split into two halves
- Conquer: find majority in each half
- Combine: count majorities from left/right, return if >n/2 elements

**Counting Inversions: Extension**
- Divide: split array
- Conquer: count inversions in each half
- Combine: merge AND count cross inversions
- Total: O(n log n)

---

### Day 5: Binary Search as Pattern

**Definition:** Maintain invariant (target in [low, high)), narrow search space geometrically.

**Classic Binary Search on Sorted Array**
```
Invariant: target ∈ [low, high) at all times
mid = low + (high - low) / 2

if arr[mid] == target: found
if arr[mid] < target: target in [mid+1, high)
if arr[mid] > target: target in [low, mid)
```

**Binary Search on Answer Space** (More Powerful)
- Problem: "Find minimum X such that condition(X) is true"
- If condition is monotone (once false, always false, or vice versa)
- Binary search on possible X values
- Example: "Minimize maximum load" — binary search on max load, check if feasible

**Why Monotone Matters?** Without monotonicity, there might be gaps and binary search fails. With it, one side is always safe to eliminate.

**Complexity:** O(log n) or O(log(answer_range)) → much better than O(n).

---

## ❌ Seven Common Misconceptions (Corrected)

### Misconception 1: Two-Pointer Only for Sorted Arrays
**False Reality:** Two-pointer works anywhere you can maintain an invariant. Sorted is just one case.

### Misconception 2: Sliding Window Is Harder Than Nested Loop
**False Reality:** Sliding window is actually simpler if you think incrementally. Each element enters/leaves once.

### Misconception 3: Divide-Conquer Is Always Better
**False Reality:** Mergesort O(n log n) needs O(n) extra space. Sometimes O(n²) in-place is acceptable.

### Misconception 4: Binary Search Works on Any "Search" Problem
**False Reality:** Binary search ONLY works if search space is monotone. Non-monotone spaces need other approaches.

### Misconception 5: Sliding Window Only for Subarrays
**False Reality:** Sliding window applies to substrings, subsets, any contiguous problem.

### Misconception 6: Invariants Are Optional Formality
**False Reality:** Invariants are THE KEY to understanding patterns. Without explicit invariant, you'll get lost.

### Misconception 7: These Patterns Are Isolated
**False Reality:** Combine them: divide-conquer + binary search, sliding window + two-pointer. Mastery is knowing combinations.

---

## 🔗 Week 04 Connection to Other Weeks

**From Week 03 (Sorting & Hashing):**
- Merge sort (divide-conquer) builds on Week 3
- Hashing used in sliding window state tracking
- Binary search on sorted arrays

**To Week 05 (Hash, Monotonic Stack, Intervals):**
- Two-pointer merge → merge intervals (Day 3)
- Sliding window + hash → frequent patterns
- Binary search mindset → binary search on ranges

**To Week 06 (String Patterns):**
- Sliding window → longest substring problems (extensively)
- Two-pointer → palindrome checks
- Divide-conquer → KMP string matching preview

**To Week 07 (Trees):**
- Divide-conquer → tree traversals, balanced tree operations
- Binary search → binary search trees
- Two-pointer → level-order traversals

**To Week 10+ (DP):**
- Divide-conquer teaches recursive thinking (precursor to DP)
- Binary search on answer → DP optimization
- Sliding window → DP on intervals

---

## ✅ Interview Readiness: Week 04 Checklist

You're ready for interviews when:

1. **Pattern Recognition < 5 seconds:** See problem → recognize pattern instantly
2. **Implementation without bugs:** Code all 4 patterns fluently
3. **Trace on paper:** Manually trace one problem per pattern
4. **Explain invariants:** State invariant before coding
5. **Handle edge cases:** Speak about boundaries
6. **Combine patterns:** Recognize when multiple patterns apply
7. **Real-world impact:** Connect pattern to actual systems (databases use B-trees, message queues use sliding, etc.)

---

**Status:** Week 04 Summary Complete  
**Next:** Week 04 Interview Q&A Reference  
**Review Time:** 1-2 hours