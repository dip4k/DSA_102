# 📋 Week 03 Guidelines: Sorting, Heaps, Hashing Fundamentals

**Week Overview:** Foundations III — Understanding internal mechanics of sorting, priority queues, and hash tables  
**Status:** Core Curriculum Week (Days 1-5) + Optional Advanced (Day 6)  
**Primary Goal:** Master three fundamental data structures and their trade-offs  
**Time Allocation:** 18-22 hours core learning + deep practice  

---

## 🎯 Week 03 Learning Arc & Context

This week, you transition from learning about individual data structures (Week 02: arrays, lists, queues) to understanding how they're used **internally** and **optimized** for performance. You'll learn:

- **Sorting algorithms:** Elementary (bubble, selection, insertion) and advanced (merge sort, quick sort)
- **Priority queues:** Binary heaps with bubble-up/bubble-down mechanics
- **Hash tables:** Hash functions, collision resolution, load factor management, and advanced techniques like rolling hashing

By week's end, you'll understand why `std::sort` is blazingly fast, why heaps power Dijkstra's algorithm, and why hash tables are the workhorse of modern systems.

---

## 📚 Day-by-Day Concept Overview

### Day 1: Elementary Sorts (Bubble, Selection, Insertion)

**Core Mental Model:** Simple algorithms with O(n²) worst-case, but small constant factors make them suitable for small arrays.

**Key Algorithms:**

**Bubble Sort**
- Mechanism: Repeatedly swap adjacent elements if in wrong order
- Invariant: After pass i, the i largest elements are in their final positions
- Complexity: O(n²) worst, O(n) best (already sorted)
- Stability: Stable (equal elements don't reorder)
- Use case: Teaching, nearly-sorted arrays, adaptive scenarios

**Selection Sort**
- Mechanism: Find minimum in unsorted portion, swap with start
- Invariant: [0, i) is sorted and contains i smallest elements
- Complexity: O(n²) always (no improvement even if sorted)
- Stability: Unstable (swaps can reorder equal elements far apart)
- Use case: Minimizing writes (useful in flash storage)

**Insertion Sort**
- Mechanism: Maintain sorted prefix, insert each new element into correct position
- Invariant: [0, i) is sorted
- Complexity: O(n²) worst, O(n) best (if nearly sorted)
- Stability: Stable
- Use case: Small arrays, nearly sorted data, hybrid algorithms (timsort, introsort use it for small subarrays)

**Why These Matter:**
- Insertion sort is used inside fast sorts (merge/quick) for small subarrays
- Understanding these teaches fundamental sorting thinking
- Real-world: Almost never use bubble sort; selection sort when minimizing writes matters; insertion sort in hybrids

**Comparison Table:**

| Algorithm | Time (Best) | Time (Avg) | Time (Worst) | Space | Stable |
|-----------|-------------|-----------|--------------|-------|--------|
| Bubble | O(n) | O(n²) | O(n²) | O(1) | ✓ |
| Selection | O(n²) | O(n²) | O(n²) | O(1) | ✗ |
| Insertion | O(n) | O(n²) | O(n²) | O(1) | ✓ |

---

### Day 2: Advanced Sorting (Merge Sort, Quick Sort)

**Core Mental Model:** Divide-and-conquer sorting: split array, recursively sort halves, combine results.

**Merge Sort**

- **Mechanism:** Divide array in half, recursively sort each half, merge two sorted arrays
- **Recurrence:** T(n) = 2·T(n/2) + O(n merge time)
- **Analysis:** O(n log n) always (guaranteed worst-case)
- **Space:** O(n) auxiliary space for merging
- **Stability:** Stable
- **Why O(n log n)?** Depth log n, work O(n) per level, log n levels = O(n log n)
- **Real-world:** Used in stable sorting (Python's Timsort uses it), stable external sorting
- **Merge insight:** Two sorted arrays merge in O(n) time via two-pointer scan

**Quick Sort**

- **Mechanism:** Choose pivot, partition into <pivot and >pivot, recursively sort
- **Recurrence:** T(n) = T(k) + T(n-k-1) + O(n partition)
  - Best case (balanced): T(n) = 2·T(n/2) + O(n) = O(n log n)
  - Worst case (unbalanced): T(n) = T(n-1) + O(n) = O(n²)
- **Expected:** O(n log n) with randomized pivot
- **Space:** O(log n) recursion (vs O(n) for merge sort)
- **In-place:** Yes (after partitioning around pivot)
- **Stability:** Unstable (partitioning doesn't preserve relative order)
- **Pivot strategies:**
  - First element: bad for already-sorted (worst case)
  - Random: expected O(n log n), no adversarial input
  - Median-of-three: reduces worst-case probability
- **Real-world:** Faster in practice than merge sort (better cache locality, in-place, smaller constants)

**Introsort (Hybrid)**

- Starts with quick sort
- Switches to merge sort if recursion depth exceeds 2·log(n)
- Falls back to insertion sort for subarrays < 16 elements
- Achieves: O(n log n) worst-case + good constants of quick sort

**Why These Matter:**
- Merge sort: guaranteed O(n log n), stable, teaches divide-and-conquer
- Quick sort: practical speed, in-place, teaches partitioning
- Understand trade-offs: time vs space vs stability vs worst-case

---

### Day 3: Heaps & Heap Sort

**Core Mental Model:** Binary heap is a nearly-complete binary tree stored in array, maintaining heap property (parent ≤ children for min-heap).

**Binary Heap Model**

- **Array representation:** Root at index 0
  - Left child of i: 2i + 1
  - Right child of i: 2i + 2
  - Parent of i: (i - 1) / 2
- **Properties:**
  - Complete binary tree (all levels filled except last, which is left-aligned)
  - Heap property: min-heap (parent ≤ children), max-heap (parent ≥ children)
- **Height:** O(log n) due to complete tree structure

**Core Operations**

**Bubble Up (Sift Up)** — Insert element
1. Add element at end of array
2. Compare with parent; if violates heap property, swap
3. Repeat at parent's position until root or property restored
4. Complexity: O(log n) (height of tree)

**Bubble Down (Sift Down / Heapify Down)** — Remove min/max
1. Remove root
2. Move last element to root
3. Bubble down: compare with children, swap with smaller/larger child
4. Repeat until heap property restored or leaf
5. Complexity: O(log n)

**Build Heap** — Array to heap
- Naive: Insert n elements = O(n log n)
- Efficient: Apply bubble-down to each node (bottom-up) = O(n)
  - Start from last non-leaf node (index n/2 - 1)
  - Bubble down each node
  - Why O(n)? Most nodes are leaves (minimal bubble-down work); few internal nodes do significant work

**Heap Sort**

1. Build heap from array: O(n)
2. Repeatedly extract root (min) and bubble down: O(n log n)
3. Total: O(n log n) worst-case, in-place variant uses O(1) extra space
4. Stability: Unstable (heap operations reorder equal elements)
5. Real-world: Rarely used (not cache-friendly, worse constants than quick sort)

**Priority Queue**

- Abstract data type: Insert, extract-min, peek-min
- Implementation: Binary heap
- Operations: O(log n) insert/extract
- Use cases: Dijkstra's algorithm, event simulation, task scheduling, median finding
- Real-world: `std::priority_queue<T>` (C++), `heapq` (Python), heap (Java)

**Why Heaps Matter:**
- Foundation for Dijkstra, Prim's MST, heap sort, event-driven simulation
- O(log n) operations on dynamic data (unlike array sort)
- Teaching binary tree properties and array-based tree representation

---

### Day 4: Hash Tables I (Separate Chaining)

**Core Mental Model:** Hash table maps keys to buckets using hash function, resolves collisions via chaining or probing.

**Hash Function Basics**

- **Purpose:** Map keys (possibly large domain) to bucket indices (fixed small range [0, m-1])
- **Desiderata:**
  - **Uniformity:** Distribute keys evenly across buckets
  - **Speed:** O(1) computation (not itself expensive)
  - **Deterministic:** Same input always produces same hash
- **Examples:**
  - Integer key: `h(x) = x mod m` (m is table size)
  - String key: Sum of character codes mod m (simple but bad), polynomial rolling hash (good)

**Separate Chaining Collision Resolution**

- **Mechanism:** Each bucket stores a linked list of entries with same hash
- **Operations:**
  - Insert: Compute h(key), append to list at bucket h(key). Expected O(1) if hash is uniform
  - Search: Compute h(key), scan list. Expected O(1 + chain length)
  - Delete: Compute h(key), find and remove from list
- **Load Factor:** α = n / m (average chain length)
  - Expected O(1 + α) per operation
  - Keep α < 1 (less than 1 element per bucket on average) for good performance
  - Resize when α exceeds threshold (e.g., 0.75)
- **Resizing:**
  - When α exceeds threshold, double m (new size must be prime for good distribution)
  - Rehash all elements: O(n) work total
  - Amortized: Insert is still O(1) amortized (occasional O(n) resizing spread over many O(1) operations)
- **Worst case:** All keys hash to same bucket → O(n) search (but extremely unlikely with good hash function)

**Why Separate Chaining?**
- Simple to implement
- Easy to handle collisions
- Doesn't need to search for empty slots
- Cache-unfriendly (linked lists jump around in memory)

---

### Day 5: Hash Tables II (Open Addressing, Rolling Hash, Karp-Rabin)

**Core Mental Model:** Alternative collision strategies, advanced hashing for strings, and security considerations.

**Open Addressing Strategies**

Instead of linked lists, find next available slot in same array.

**Linear Probing**
- On collision at h(key), try h(key) + 1, h(key) + 2, ... (with wraparound)
- **Primary clustering:** Hash values cluster together, creating long probe sequences
- **Complexity:** O(1 + (m - n)) = O(1 / (1 - α)) where α = n / m
  - Acceptable if α < 0.5, degrades rapidly as α approaches 1
- **Pros:** Cache-friendly (probing is linear in array)
- **Cons:** Primary clustering, hard to delete (need tombstones)

**Quadratic Probing**
- On collision, try h(key) + 1², h(key) + 2², h(key) + 3², ...
- **Secondary clustering:** Reduces primary clustering, but lesser effect
- **Complexity:** Still O(1 / (1 - α)) but with better constants
- **Pros:** Better than linear probing
- **Cons:** Doesn't explore all slots unless m is prime and special conditions hold

**Double Hashing**
- Use two independent hash functions: h₁(key) and h₂(key)
- On collision, try h₁ + h₂, h₁ + 2·h₂, h₁ + 3·h₂, ...
- **Pros:** Distributes probe sequence well, explores all slots if h₂(key) coprime with m
- **Cons:** More computation, two hash functions
- **Best in practice:** For open addressing, double hashing is preferred

**Load Factor Thresholds**
- Separate chaining: Resize when α > 1 (can tolerate α > 1)
- Linear probing: Resize when α > 0.5 (performance degrades fast)
- Double hashing: Resize when α > 0.75 (more efficient use of space)

**Rolling Hash & Karp-Rabin**

**Problem:** Pattern matching in string (find all occurrences of pattern P in text T)
- Naive: O(n·m) per-position comparison
- Karp-Rabin: Use rolling hash to skip non-matching positions

**Rolling Hash Idea**
- Hash polynomial: H(S) = S₀·b^(m-1) + S₁·b^(m-2) + ... + S_(m-1) (mod p)
- **Rolling property:** H(S[i+1...i+m]) = (H(S[i...i+m-1]) - S[i]·b^(m-1)) · b + S[i+m] (mod p)
  - Remove first character, shift left, add new character
  - Update in O(1)! (once precompute b^(m-1))

**Karp-Rabin Algorithm**
1. Compute hash of pattern P: O(m)
2. Compute hash of first window T[0...m-1]: O(m)
3. For each position i = 1 to n-m:
   - Roll hash: T[i...i+m-1] hash from T[i-1...i+m-2] hash in O(1)
   - If hash matches, verify characters (avoid false positives)
4. Total: O(n + m) average, O(n·m) worst (if many hash collisions)

**Collision Handling:**
- Use prime modulus p: reduces collision probability
- If hashes match, still verify characters (rolling hash can have collisions)
- Multiple hash functions (double hashing on hash values) for extra safety

**Hash Security & Universal Hashing**

**Hash Flooding Attack**
- Adversary crafts n keys all hashing to same bucket → O(n) per operation
- Example: In contest, submit test case with many keys hashing to same bucket

**Mitigation Strategies:**
1. **Randomized hash seed:** Different seed for each program run (unpredictable hash output)
2. **Universal hashing:** Family of hash functions where any two distinct keys have low collision probability
   - For any two distinct keys x, y: Pr[h(x) = h(y)] ≤ 1/m (if h chosen randomly from family)
   - Example: h(x) = (ax + b) mod p (with a, b random)

**Real-world:** Modern languages use randomized seeds (Python, Ruby) to prevent DOS attacks.

**Why These Matter:**
- Karp-Rabin: Practical string matching, plagiarism detection, DNA sequence searching
- Open addressing: Space-efficient hash tables
- Universal hashing: Theoretically sound, practical security

---

## 🎓 Nine Core Learning Objectives

By end of Week 03, you will be able to:

1. **Compare sorting algorithms:** Trade-offs between time, space, stability, constants, and worst-case behavior
2. **Implement insertion sort** with correct in-place swapping and compare with built-in sorts
3. **Understand merge sort** recurrence, stability, and why it's guaranteed O(n log n)
4. **Implement quick sort** with partition, understand randomization, recognize worst-case pitfall
5. **Build and manipulate binary heaps:** Bubble-up, bubble-down, build-heap in O(n), heap-sort
6. **Design hash tables:** Hash functions, load factor, resizing strategy, when to use
7. **Analyze collisions:** Separate chaining vs open addressing trade-offs
8. **Implement rolling hash** and Karp-Rabin string matching
9. **Choose data structure wisely:** When to use sort vs heap vs hash table, considering constraints

---

## 🧭 Four Effective Study Strategies

### Strategy 1: Visualization Before Code

For each sorting algorithm:
1. Draw array state after each pass/level
2. Mark invariant (what's guaranteed after each step)
3. Calculate comparisons and swaps count
4. Only then write code

For each heap operation:
1. Draw tree representation
2. Mark bubble-up/down path
3. Show array state at each step

### Strategy 2: Complexity Analysis Discipline

Before coding, calculate:
- **Time complexity:** Worst, average, best cases
- **Space complexity:** Auxiliary space beyond input
- **Comparisons & swaps count:** Sometimes O(n log n) but with 10x worse constants
- **When to use:** Which constraints make this algorithm best?

### Strategy 3: Adversarial Thinking

For quick sort: What input causes worst case? (Already sorted, all equal elements)  
For hash tables: What keys cause collisions? (Multiples of table size)  
For heaps: What sequence causes maximum bubble-up distance?

### Strategy 4: Real-World Grounding

- How does `std::sort` (C++) choose between quick sort and heap sort?
- Why does Java's `Arrays.sort()` use different algorithms for primitives vs objects?
- Why do databases use B-trees instead of binary heaps?
- How do search engines use rolling hash for plagiarism detection?

---

## ⏱️ Recommended Time Allocation

| Activity | Hours | Focus |
|----------|-------|-------|
| Read Day 1-2 instructional | 3 | Elementary and advanced sorts |
| Practice Day 1-2 problems | 4 | Implement sorts, analyze trade-offs |
| Read Day 3 instructional | 2 | Heap structure, operations, heap sort |
| Practice Day 3 problems | 3 | Implement heap, heap sort, priority queue |
| Read Day 4-5 instructional | 3 | Hash tables, collision resolution, rolling hash |
| Practice Day 4-5 problems | 4 | Implement hash table, Karp-Rabin, collision tests |
| Integration & synthesis | 2 | Review all 3, when to use each |
| Optional Day 6 (advanced) | 1-2 | Advanced hash techniques |
| **Total Core (Days 1-5)** | **22 hours** | **Interview prep complete** |
| **Total with Day 6** | **23-24 hours** | **Mastery level** |

---

## 🚫 Six Common Pitfalls (and How to Avoid Them)

### Pitfall 1: Confusing Big-O with Actual Speed

**Wrong:** "O(n²) is always slower than O(n log n)"

**Reality:** Insertion sort on 50 elements can outrun quick sort due to better constants and cache locality.

**Fix:** Measure actual time on your system. Understand constants matter for small n. Modern sorts use hybrids (insertion for small arrays).

---

### Pitfall 2: Not Understanding Heap Property

**Wrong:** Assuming min-heap is a sorted array (it's not!)

**Reality:** Min-heap guarantees only parent ≤ children, not full sorting. Tree structure is almost-complete, not sorted.

**Fix:** Draw heap as tree, not array. Verify heap property at each operation. Understand height is O(log n), not O(1).

---

### Pitfall 3: Heap Operations with Wrong Indices

**Wrong:** `left = 2*i`, `right = 2*i + 1`, `parent = i/2` (off by one for 1-indexed vs 0-indexed)

**Reality:** 0-indexed arrays use different formulas. Off-by-one errors cause accessing wrong children.

**Fix:** Verify formulas for your indexing. Test on small examples. Trace manually.

---

### Pitfall 4: Hash Function Misunderstanding

**Wrong:** Thinking any hash function works uniformly

**Reality:** `h(x) = x mod 10` will cluster multiples of 10 together badly.

**Fix:** Understand hash function design: use prime modulus, randomize seeds, test collision distribution.

---

### Pitfall 5: Ignoring Load Factor in Hash Tables

**Wrong:** Inserting 1000 items into table of size 10 without resizing

**Reality:** Chains become long, operations become O(n) instead of O(1).

**Fix:** Implement resize threshold. Monitor load factor. Understand amortized cost of resizing.

---

### Pitfall 6: Confusing Stability with Correctness

**Wrong:** "Unstable sorts are wrong"

**Reality:** Unstable sorts are correct; they just don't preserve order of equal elements. Matters only if you need that property.

**Fix:** Know which sorts are stable (bubble, insertion, merge) and which aren't (selection, quick, heap). Use stable when needed.

---

## 📊 Concept Map: Sorting Algorithms Decision Tree

```
Week 03 Data Structures

├─ SORTING ALGORITHMS (When ordering matters)
│  ├─ Elementary (O(n²))
│  │  ├─ Bubble Sort (stable, few writes)
│  │  ├─ Selection Sort (minimize writes, unstable)
│  │  └─ Insertion Sort (small n, nearly sorted, stable)
│  │
│  └─ Advanced (O(n log n))
│     ├─ Merge Sort (guaranteed O(n log n), stable, extra space)
│     ├─ Quick Sort (fast in practice, in-place, unstable)
│     └─ Heap Sort (guaranteed O(n log n), in-place, unstable)
│
├─ HEAPS (When you need efficient min/max with insertions)
│  ├─ Structure: Complete binary tree in array
│  ├─ Operations: Insert O(log n), Extract-min O(log n)
│  ├─ Build: O(n) with bottom-up bubble-down
│  └─ Use: Dijkstra, priority queue, heap sort
│
└─ HASH TABLES (When you need fast lookup)
   ├─ Collision Resolution
   │  ├─ Separate Chaining (simple, linked lists)
   │  └─ Open Addressing (space-efficient, probing)
   │
   ├─ Advanced
   │  ├─ Rolling Hash (string matching)
   │  ├─ Karp-Rabin (O(n+m) pattern matching)
   │  └─ Universal Hashing (security)
   │
   └─ Implementation: Hash function, load factor, resize
```

---

## 🎯 Ten Key Insights to Remember

1. **Elementary sorts teach fundamentals** even though they're slow. Insertion sort stays competitive for small arrays.

2. **Merge sort is the only one that guarantees O(n log n)** worst-case. Quick sort is O(n²) worst-case but O(n log n) expected.

3. **Heap sort is rarely used in practice** despite O(n log n) guarantee, because quick sort has better constants and cache behavior.

4. **Timsort (Python, Java) is a hybrid:** Uses quick sort, switches to merge sort if recursion gets too deep, uses insertion sort for tiny subarrays.

5. **Heap property is NOT full sorting:** Min-heap only guarantees parent ≤ children. It's a partial order that enables O(log n) operations.

6. **Hash tables achieve O(1) average because of uniform distribution.** Bad hash functions destroy performance. Randomized seeds prevent DOS attacks.

7. **Load factor controls hash table performance.** Keep it below 0.75 for open addressing, below 1.0 for separate chaining.

8. **Karp-Rabin is O(n) on average but O(n·m) worst-case** (hash collisions). Still practical because collisions are rare with good hash.

9. **Separate chaining is simpler but uses more memory** (linked lists everywhere). Open addressing saves memory but has clustering issues.

10. **Real systems use adaptive algorithms:** Analyze your data, then choose. JSON parsing with hashing, sorting billions of integers with hybrid merge-quick.

---

## 🔄 How Week 03 Connects to Surrounding Weeks

**From Week 02 (Arrays & Lists):**
- Arrays are foundation for sorting and heap arrays
- Linked lists used in separate chaining
- Understanding memory layout matters for algorithm performance

**To Week 04 (Problem-Solving Patterns):**
- Sorting as preprocessing step
- Heap as data structure for certain patterns
- Hash tables for complement/frequency counting

**To Week 05 (Critical Patterns):**
- Hash tables core for many patterns
- Monotonic stack (advanced) uses similar ideas
- Interval merging uses sorting

**To Week 07 (Trees):**
- Heaps are trees stored in arrays
- Understanding tree height and structure from Week 03
- Binary search trees similar concepts

**To Week 08+ (Graph Algorithms):**
- Dijkstra uses priority queue (heap)
- Kruskal's MST uses sorting edges
- Hash tables for visited sets

---

## ✅ Weekly Checklist

By end of week, you should be able to:

- [ ] Implement bubble sort, selection sort, insertion sort from scratch
- [ ] Explain why merge sort is O(n log n) and prove via recurrence
- [ ] Implement merge sort and merge function without reference
- [ ] Implement quick sort with partition, understand pivot strategy
- [ ] Implement binary heap with bubble-up, bubble-down, build-heap
- [ ] Implement heap sort and priority queue using heap
- [ ] Design hash function and hash table with separate chaining
- [ ] Analyze collision patterns and load factor trade-offs
- [ ] Implement Karp-Rabin rolling hash pattern matching
- [ ] Compare all algorithms, choose wisely for given constraints
- [ ] Solve 15-20 problems using sorting, heaps, or hashing

---

## 🎓 Mastery Signals

You've mastered Week 03 when:

1. **You understand trade-offs deeply.** Not just "merge sort is O(n log n)" but "it needs extra space and isn't cache-friendly, so quick sort is faster on modern CPUs."

2. **You implement algorithms without reference.** Can code all sorting variants, heap operations, and hash table mechanics fluently.

3. **You trace manually.** Can trace merge sort, quick sort, heap operations on paper and predict exact comparisons and swaps.

4. **You choose algorithms for constraints.** Given "need stable sort with O(1) space," you immediately think "insertion sort for small n, or modify merge sort to avoid extra space (harder)."

5. **You understand real-world implementations.** Know why Java's `Arrays.sort()` behaves differently for int[] vs Integer[], why Python uses Timsort, what Introsort is.

6. **You recognize when each structure helps.** Hash table for fast lookup, heap for priority, sorted array for binary search, unordered for frequency counting.

7. **You debug efficiently.** Trace heap operations, verify hash function distribution, confirm sorting invariants rather than testing randomly.

---

**Status:** Week 03 Guidelines Complete  
**Next:** Week 03 Summary & Key Concepts  
**Time to Completion:** 22+ hours for full mastery