# 📋 Week 02 Support Files: Guidelines, Summary & Interview Reference

---

## 📖 PART 1: Week 02 Core Concepts Summary

**Week 02 Theme:** Linear Data Structures & Search Algorithms

### Day 1: Arrays & Memory Layout
**Core Mental Model:** Contiguous memory enables O(1) random access and prefetching, but cache locality dominates practical performance.

**Key Invariants:**
- Address of array[i] = base + i × element_size (constant time computation)
- Sequential access prefetches cache lines; random access thrashes caches
- Row-major layout (C#/Java default) stores matrix row-by-row

**Critical Trade-offs:**
- Arrays: Fast access (O(1)), slow insertion (O(n))
- No wasted capacity (unlike dynamic arrays)
- Excellent cache locality (critical for real performance)

**Interview Prep:**
- Explain why sequential access is 10-100x faster than random
- Discuss row-major vs column-major for matrices
- Understand the gap between Big-O (both O(1)) and real performance

---

### Day 2: Dynamic Arrays & Amortized Growth
**Core Mental Model:** Doubling capacity trades space overhead (~50%) for O(1) amortized insertion.

**Key Insight:** Geometric growth (2x) ensures resizes are rare enough that cost spreads evenly.

**Analysis:**
- Push: O(1) amortized (despite occasional O(n) resizes)
- Resize cost distribution: 1 + 2 + 4 + 8 + ... + n = O(n) total → O(1) per push
- Space: ~50% overhead on average (capacity ≈ 1.5× length asymptotically)

**Critical Trade-offs:**
- Linear growth (size += 1): O(n²) total for n pushes (terrible)
- Geometric growth (size *= 2): O(n) total for n pushes (optimal)
- 1.5x growth vs 2x: Slightly less waste, slightly higher reallocation cost

**Interview Prep:**
- Trace cost for n pushes (show geometric series sums to O(n))
- Explain why doubling is better than incrementing by constant
- Discuss shrinking strategies (prevent thrashing)

---

### Day 3: Linked Lists
**Core Mental Model:** Trade O(1) random access for O(1) insertion at known positions via pointer rerouting.

**Key Operations:**
- Singly: O(1) insert/delete at head, O(n) anywhere else
- Doubly: O(1) at head/tail; O(1) pop_back (singly requires O(n))
- No random access (O(n) to find element at index)

**Performance Reality:**
- Pointer chasing causes cache misses on every dereference
- Practical traversal is 10-50x slower than array despite O(1) per-operation costs
- Each node has overhead (pointers), reducing memory efficiency

**Real Use Cases:**
- LRU caches (doubly linked + hash map)
- Garbage collection (kernel linked lists)
- Undo/redo stacks (browser history)

**Interview Prep:**
- Implement singly & doubly linked lists (insert, delete, reverse)
- Trace pointer updates carefully (common off-by-one bugs)
- Discuss why hybrid structures (linked + hash) are practical

---

### Day 4: Stacks, Queues & Deques
**Core Mental Model:** Restrict access (LIFO/FIFO) for semantic clarity and performance optimization.

**Stack (LIFO):**
- Push/pop at one end (top): O(1)
- Use cases: Call stack, undo/redo, expression evaluation
- Array implementation: O(1) amortized (like dynamic array)

**Queue (FIFO):**
- Enqueue at back, dequeue at front: O(1) each
- Naive array wastes space (front moves right, unused space accumulates)
- Circular buffer: O(1) with no waste (modulo arithmetic for wrap-around)
- Use cases: BFS, task scheduling, message passing

**Deque (Double-Ended):**
- Insert/remove at both ends: O(1) each
- Implementation: Circular buffer with front/back pointers
- Use cases: Sliding window problems, monotonic queue

**Interview Prep:**
- Implement circular buffer queue (wrap-around logic is tricky)
- Trace expression evaluation with stacks
- Understand why circular buffer avoids naive queue waste
- Apply to BFS problem

---

### Day 5: Binary Search & Invariants
**Core Mental Model:** Invariants guide logic and guarantee correctness. Sorting enables logarithmic search.

**The Invariant:** "If target exists, it's in [low, high]."

**Key Variants:**
- Exact match: `while (low <= high)`, `high = mid - 1`
- First occurrence: Keep searching left after finding
- Last occurrence: Keep searching right after finding
- Lower bound: First element >= target
- Upper bound: First element > target

**Binary Search on Answer Space:**
- Convert optimization to feasibility check
- Example: "Minimum capacity to ship in D days?"
- Search over capacity range; binary search for minimum feasible
- Requires monotonic predicate (if X works, X+1 works)

**Complexity:**
- Time: O(log n) comparisons + O(cost_of_check) per binary search
- Space: O(1) unless recursive (then O(log n) call stack)

**Interview Prep:**
- Implement basic binary search (off-by-one is tricky)
- Implement lower_bound/upper_bound variants
- Apply to rotated sorted array search
- Binary search on answer space (minimize max load, aggressive cows)

---

## 📚 PART 2: Week 02 Practice Problem Categories

### Arrays & Memory (Day 1)
**Difficulty 🟢 (Warmup):**
- Sum array sequentially vs random order (measure performance difference)
- Matrix transpose (row-major vs column-major iteration)
- Find max element in 2D array (optimal iteration order)

**Difficulty 🟡 (Intermediate):**
- Cache-efficient matrix multiply (tiling/blocking)
- Jagged vs rectangular array performance
- Understand false sharing on multi-threaded array access

### Dynamic Arrays (Day 2)
**Difficulty 🟢:**
- Implement push/pop with resizing
- Trace cost for n pushes (O(n) total)

**Difficulty 🟡:**
- Compare 1.5x vs 2x growth strategies
- Implement pop with shrinking (prevent thrashing)
- Measure actual vs theoretical amortized cost

### Linked Lists (Day 3)
**Difficulty 🟢:**
- Implement singly linked list (push/pop at head)
- Reverse a linked list (pointer manipulation)

**Difficulty 🟡:**
- Detect cycle (Floyd's algorithm)
- Find kth from last node
- Merge two sorted linked lists

**Difficulty 🟠:**
- Implement LRU cache (doubly linked + hash)
- Copy linked list with arbitrary pointers

### Stacks & Queues (Day 4)
**Difficulty 🟢:**
- Implement stack (array-based)
- Balanced parentheses validator

**Difficulty 🟡:**
- Implement circular buffer queue
- Postfix expression evaluation
- Min stack (track minimum in O(1))

**Difficulty 🟠:**
- Sliding window with deque
- Implement LRU cache with deque
- Monotonic queue for window max/min

### Binary Search (Day 5)
**Difficulty 🟢:**
- Standard binary search (exact match)
- Lower/upper bound

**Difficulty 🟡:**
- First/last occurrence
- Search in rotated sorted array
- Find peak (one-sided monotonic)

**Difficulty 🟠:**
- Minimize maximum load (binary search on answer)
- Aggressive cows (maximize minimum distance)
- Kth smallest in 2D sorted matrix

---

## 🎙️ PART 3: Week 02 Interview Question Bank

### Arrays & Memory (Day 1)
**Q1:** Why is sequential array access faster than random access?
- **Model Answer:** Sequential access fits in cache lines (64 bytes); CPU prefetches automatically. Random access causes cache misses on every access. Difference: 2x to 10x+ in practice.

**Q2:** Explain row-major vs column-major layouts. Why does iteration order matter?
- **Model Answer:** Row-major (C#/Java): [row][col] stored row-by-row. Column-major: stored column-by-column. If you iterate row-major on row-major data, sequential. If you iterate column-major on row-major, you jump around memory → cache misses.

**Q3:** Design a cache-friendly matrix transpose.
- **Model Answer:** Instead of a[i][j] = b[j][i] (column-major on row-major), use tiling: process 64x64 blocks (fit in cache). Transpose within blocks, then concatenate.

---

### Dynamic Arrays (Day 2)
**Q4:** Explain why push is O(1) amortized despite O(n) resizes.
- **Model Answer:** Resizes at 1, 2, 4, 8, ..., n. Total cost: 1+2+4+...+n = O(n). Spread over n pushes = O(1) per push. Key: exponential growth makes resizes rare enough.

**Q5:** Compare 1.5x vs 2x growth. Which is better?
- **Model Answer:** 2x grows faster; fewer resizes (better amortized). 1.5x has less wasted space (~33% vs ~50%). Trade-off: speed vs memory. 2x is typically preferred (reallocation overhead > memory waste).

---

### Linked Lists (Day 3)
**Q6:** Implement a singly linked list with insert/delete at arbitrary index.
- **Model Answer:** Traverse to node before insertion point (O(i)). Reroute pointers (O(1)). Total: O(i). Key: pointer updates must maintain both next and prev in doubly linked.

**Q7:** Why is linked list practical in LRU caches but not as primary data structure?
- **Model Answer:** LRU needs O(1) access (hash map) + O(1) reordering (linked list). Combined: O(1). Linked list alone is O(n) access (impractical). Hybrid leverages both.

---

### Stacks & Queues (Day 4)
**Q8:** Implement a queue without wasting space (circular buffer).
- **Model Answer:** Maintain front index and count. Back index = (front + count) % capacity. On resize: copy elements to linear array, reset front = 0. Modulo arithmetic enables wrap-around without waste.

**Q9:** Evaluate postfix expression "5 3 + 2 *" using a stack.
- **Model Answer:** Push 5 → Push 3 → '+' pops 3,5 → push 8 → Push 2 → '*' pops 2,8 → push 16. Result: 16. Stack naturally models operand order.

---

### Binary Search (Day 5)
**Q10:** Implement binary search. What off-by-one errors are common?
- **Model Answer:** `mid = low + (high - low) / 2` (avoid overflow). For exact match: `while (low <= high)`, `high = mid - 1`. Common bug: `high = mid` (infinite loop). For bounds: `while (low < high)`, `high = mid` (different!).

**Q11:** Find minimum capacity to ship all packages in D days.
- **Model Answer:** Binary search over capacity [max_package, sum_all]. For each capacity, simulate greedy assignment: pack each worker until next package doesn't fit. Count workers needed. Feasible if workers <= k. Find minimum feasible capacity.

---

## 📌 PART 4: Week 02 Misconceptions & Clarifications

**Misconception 1:** "All O(1) operations are equally fast."
- **Reality:** Array indexing and linked list insertion are both O(1), but array is 10-100x faster due to cache locality.
- **Clarification:** Big-O hides constants. Cache effects dominate for moderate input sizes.

**Misconception 2:** "Amortized O(1) means all operations are O(1)."
- **Reality:** Some operations are O(n) (resizes). Amortized means average is O(1).
- **Clarification:** Important for real-time systems: individual push can stall for resizing. Unacceptable if you need deterministic latency.

**Misconception 3:** "Linked lists are always better for insertion."
- **Reality:** Only if you already have a pointer. Finding insertion point is O(n).
- **Clarification:** Hybrids (hash map + linked list) combine benefits.

**Misconception 4:** "Binary search requires a perfectly sorted array."
- **Reality:** Binary search requires monotonicity. Sorted is a special case. Can binary search on any monotonic property.
- **Clarification:** "Can we ship in D days with capacity X?" is monotonic (if yes for X, yes for X+1).

**Misconception 5:** "Doubling capacity wastes too much memory."
- **Reality:** ~50% average overhead. Trade-off is justified for O(1) amortized insertion.
- **Clarification:** For systems with memory constraints, use 1.5x growth (slightly more waste, higher allocation pressure).

---

## 🔗 PART 5: Week 02 → Week 03 Connection

**Week 02 Outcomes:** You can now reason about linear data structures, understand cache effects, analyze amortized costs, and apply binary search.

**Week 03 Preview:** Sorting, heaps, and hashing—next layer of data structures. Sorting is O(n log n) but enables binary search. Heaps maintain order efficiently. Hash tables provide O(1) average lookup (beating binary search's O(log n)).

**Key Progression:**
- Week 01: Fundamentals (memory, complexity, recursion)
- Week 02: Linear structures (arrays, lists, search)
- Week 03: Advanced collections (sorting, heaps, hashing)
- Weeks 4+: Patterns, trees, graphs

---

## 📚 Additional Resources

**Theory:**
- CLRS Chapters 10–13: Comprehensive data structures
- MIT 6.006 Lecture Notes (online): Linear structures, binary search

**Practice:**
- LeetCode: Array, Linked List, Stack, Queue, Binary Search tracks
- HackerRank: Data structures fundamentals
- GeeksforGeeks: Step-by-step implementations with visualizations

**Visualization:**
- Visualgo.net: Interactive visualizations of all Week 02 structures
- CP Algorithms: Algorithm explanations with proofs

---

## ✅ Week 02 Self-Assessment Checklist

**Conceptual Understanding:**
- [ ] Understand why cache locality matters more than Big-O for sequential data
- [ ] Explain amortized analysis intuitively
- [ ] Recognize pointer trade-offs (linked list slowness due to dereferencing)
- [ ] Understand LIFO/FIFO semantics and real-world use cases
- [ ] Know when and why binary search works

**Implementation:**
- [ ] Implement dynamic array with resizing
- [ ] Implement singly and doubly linked lists
- [ ] Implement stack and circular queue
- [ ] Implement binary search and variants
- [ ] Know common off-by-one errors and how to avoid them

**Problem-Solving:**
- [ ] Solve problems combining arrays, lists, and binary search
- [ ] Analyze trade-offs (access speed, insertion speed, memory)
- [ ] Recognize when linear vs binary vs indexed search is appropriate
- [ ] Understand when hybrid structures (hash + linked) add value

**Interview Readiness:**
- [ ] Implement all data structures from scratch
- [ ] Explain design choices (array vs list, 2x vs 1.5x growth)
- [ ] Discuss performance implications of implementation choices
- [ ] Solve medium-difficulty LeetCode problems on these topics

---

**Status:** ✅ Week 02 Support Files Complete

