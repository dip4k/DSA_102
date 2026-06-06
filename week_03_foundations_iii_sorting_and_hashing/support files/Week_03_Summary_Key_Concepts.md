# 📖 Week 03 Summary & Key Concepts: Sorting, Heaps, Hashing Deep Reference

**Audience:** Students completing Week 03 instructional content  
**Purpose:** Comprehensive reference for review, retention, and connection-building

---

## 🎯 Week 03 Narrative Summary

**Week 03: Foundations III** transitions you from understanding what data structures do to understanding how they work internally and how to choose between them. You'll master three critical areas:

**Sorting** — algorithms that order data, with radically different performance profiles  
**Heaps** — tree-based structures enabling O(log n) priority queue operations  
**Hashing** — fast lookup with clever collision handling and advanced string matching

---

## 🧠 Comprehensive Concept Maps

### Sorting Algorithms Taxonomy

```
SORTING ALGORITHMS

├─ ELEMENTARY SORTS (O(n²) worst-case)
│  │
│  ├─ BUBBLE SORT (Stable, O(n) best)
│  │  ├─ Mechanism: Swap adjacent elements, bubble largest to end
│  │  ├─ Invariant: After pass i, i largest elements in final positions
│  │  ├─ Comparisons: n(n-1)/2
│  │  ├─ Swaps: 0 (best), n(n-1)/2 (worst)
│  │  ├─ When to use: Nearly sorted, minimizing comparisons not critical
│  │  └─ Real-world: Rarely (educational)
│  │
│  ├─ SELECTION SORT (Unstable, O(n²) always)
│  │  ├─ Mechanism: Find min in unsorted portion, swap with start
│  │  ├─ Invariant: [0,i) is sorted and contains i smallest
│  │  ├─ Comparisons: n(n-1)/2 (always)
│  │  ├─ Swaps: n-1 (minimal, useful for expensive writes)
│  │  ├─ When to use: Minimize writes (flash memory, I/O)
│  │  └─ Real-world: Flash storage, disk sorting in extreme cases
│  │
│  └─ INSERTION SORT (Stable, O(n) best, O(n²) worst)
│     ├─ Mechanism: Maintain sorted prefix, insert each element
│     ├─ Invariant: [0,i) is sorted
│     ├─ Comparisons: n/2 (best), n²/2 (worst)
│     ├─ Swaps: (or shifts) 0 (best), n²/2 (worst)
│     ├─ When to use: Small arrays, nearly sorted data, as hybrid
│     └─ Real-world: In Timsort (Python), Introsort, hybrids
│
├─ ADVANCED SORTS (O(n log n) guaranteed or expected)
│  │
│  ├─ MERGE SORT (Stable, O(n log n) guaranteed, O(n) extra space)
│  │  ├─ Mechanism: Divide array, recursively sort, merge two sorted halves
│  │  ├─ Recurrence: T(n) = 2T(n/2) + O(n)
│  │  ├─ Proof of O(n log n): log n levels, O(n) work per level
│  │  ├─ Merge function: Two-pointer scan, O(n+m) for arrays of size n, m
│  │  ├─ Space: O(n) auxiliary (or O(log n) with careful in-place merge)
│  │  ├─ Comparisons: n log n (average and worst-case)
│  │  ├─ Cache behavior: Poor (alternating halves), but predictable
│  │  ├─ When to use: Need guaranteed O(n log n), need stability
│  │  └─ Real-world: Stable external sorting, multi-threaded sorting
│  │
│  ├─ QUICK SORT (Unstable, O(n log n) expected, O(n²) worst, O(log n) extra space)
│  │  ├─ Mechanism: Choose pivot, partition into <pivot and >pivot, recurse
│  │  ├─ Recurrence (random): T(n) = T(k) + T(n-k-1) + O(n), expected T(n) = O(n log n)
│  │  ├─ Pivot strategies:
│  │  │  ├─ First/last element: O(n²) on sorted input (bad)
│  │  │  ├─ Random element: O(n log n) expected, no bad input (good)
│  │  │  └─ Median-of-three: Reduces bad inputs probability (practical)
│  │  ├─ Partition: Rearrange so <pivot on left, >pivot on right
│  │  ├─ Comparisons: O(n log n) expected, O(n²) worst
│  │  ├─ Cache behavior: Good (in-place, sequential access)
│  │  ├─ Constants: Smaller than merge sort (3 moves per comparison vs merge overhead)
│  │  ├─ When to use: Need in-place, want practical speed
│  │  └─ Real-world: Default choice in most libraries until recently
│  │
│  ├─ HEAP SORT (Unstable, O(n log n) guaranteed, O(1) extra space)
│  │  ├─ Mechanism: Build heap, repeatedly extract root and bubble down
│  │  ├─ Phase 1: Build heap in O(n)
│  │  ├─ Phase 2: Extract min n times, each O(log n), total O(n log n)
│  │  ├─ Comparisons: ~2n log n (twice as many as merge/quick due to sift-down overhead)
│  │  ├─ In-place and stable heap sort possible but complex
│  │  ├─ Cache behavior: Poor (random access within heap)
│  │  ├─ When to use: Need O(n log n) with O(1) space, no stability needed
│  │  └─ Real-world: Rarely (constants worse than quick, less cache-friendly than merge)
│  │
│  └─ HYBRID SORTS (Practical)
│     ├─ TIMSORT (Python, Java object arrays)
│     │  ├─ Uses insertion sort on runs (64 elements)
│     │  ├─ Merges runs using merge sort
│     │  ├─ Near-O(n) on nearly sorted data
│     │  └─ O(n log n) worst-case, stable
│     │
│     └─ INTROSORT (C++ std::sort, GCC libstdc++)
│        ├─ Uses quick sort with depth limit 2·log(n)
│        ├─ If depth exceeded, switch to heap sort (prevent O(n²))
│        ├─ Uses insertion sort for small subarrays
│        └─ Achieves O(n log n) worst-case + quick sort constants
```

**Sorting Algorithm Selection Table**

| Algorithm | Best | Average | Worst | Space | Stable | In-Place | Use Case |
|-----------|------|---------|-------|-------|--------|----------|----------|
| Bubble | O(n) | O(n²) | O(n²) | O(1) | Yes | Yes | Teaching only |
| Selection | O(n²) | O(n²) | O(n²) | O(1) | No | Yes | Minimize writes |
| Insertion | O(n) | O(n²) | O(n²) | O(1) | Yes | Yes | Small/nearly sorted |
| Merge | O(n log n) | O(n log n) | O(n log n) | O(n) | Yes | No | Guaranteed O(n log n) |
| Quick | O(n log n)* | O(n log n) | O(n²) | O(log n) | No | Yes | General-purpose* |
| Heap | O(n log n) | O(n log n) | O(n log n) | O(1) | No | Yes | O(n log n) space bound |

*Quick sort expected, needs good pivot strategy

---

## 🏗️ Heaps: Complete Model

### Binary Heap Structure

**Array Representation** (0-indexed)
```
Index:     0    1    2    3    4    5    6
Array:   [10] [20] [30] [40] [50] [60] [70]

Tree structure:
         10
        /  \
      20    30
     / \   / \
    40 50 60 70
```

**Index Relationships:**
- Parent of i: (i-1)/2 (floor division)
- Left child of i: 2i+1
- Right child of i: 2i+2
- Last non-leaf: (n-1)/2

**Properties:**
- **Complete binary tree:** All levels filled except possibly last, which is left-aligned
- **Height:** ⌊log₂(n)⌋
- **Min-heap property:** Parent ≤ both children
- **Max-heap property:** Parent ≥ both children
- **NOT sorted:** Min-heap is NOT sorted array. Min is guaranteed at root only.

### Core Heap Operations

**Insert (Bubble-Up / Sift-Up)**

```
Insert 5 into min-heap [10, 20, 30, 40, 50, 60, 70]:

Step 1: Add at end
       [10, 20, 30, 40, 50, 60, 70, 5]
       
Step 2: Bubble up (5 < 50, so swap)
       [10, 20, 30, 40, 5, 60, 70, 50]
       
Step 3: Bubble up (5 < 20, so swap)
       [10, 5, 30, 40, 20, 60, 70, 50]
       
Step 4: Bubble up (5 < 10, so swap)
       [5, 10, 30, 40, 20, 60, 70, 50]
       
Step 5: Done (at root)
```

- **Complexity:** O(log n) — height of tree
- **Comparisons:** At most log n

**Extract-Min (Bubble-Down / Sift-Down)**

```
Extract from min-heap [5, 10, 30, 40, 20, 60, 70]:

Step 1: Remove root (5), move last element (70) to root
       [70, 10, 30, 40, 20, 60]
       
Step 2: Bubble down (70 > 10, swap with smaller child 10)
       [10, 70, 30, 40, 20, 60]
       
Step 3: Bubble down (70 > 40, swap with smaller child 40)
       [10, 40, 30, 70, 20, 60]
       
Step 4: Done (70 is leaf, no more children)
```

- **Complexity:** O(log n) — height of tree
- **Comparisons:** At most 2·log n (compare with both children each step)

**Build Heap (Heapify)**

Two approaches:

1. **Naive:** Insert n elements one by one = O(n log n)
2. **Efficient (Bottom-Up):**
   ```
   for i from (n-1)/2 down to 0:
       bubble_down(i)
   ```
   - Start from last non-leaf node
   - Apply bubble-down to each node
   - Work bottom-up
   - **Complexity:** O(n)
   - **Why O(n) not O(n log n)?** Most nodes are leaves (do no work), few internal nodes do work. Sum = n/2 + n/4 + n/8 + ... = n (geometric series)

**Example:**

```
Build min-heap from [10, 20, 30, 40, 50, 60, 70]:

Initial tree:
     10
    /  \
   20   30
  / \   / \
 40 50 60 70

Last non-leaf: index 2 (value 30)
Bubble-down from 30: 30 is already < 60, 70 → done

Index 1 (value 20): 20 < 40, 50 → done

Index 0 (value 10): 10 < children → done

Result: [10, 20, 30, 40, 50, 60, 70] (already min-heap!)
```

### Heap Sort

**Algorithm:**
1. Build heap: O(n)
2. For i from n down to 1:
   - Extract root (minimum): O(1)
   - Place at position i
   - Bubble down remaining n-1 elements: O(log n)
3. Total: O(n) + n·O(log n) = O(n log n)

**Space:** O(1) in-place (use array for both heap and sorted output)

**Why rarely used:**
- Worse cache locality (random access within heap)
- Larger constants: ~2n log n comparisons vs n log n for merge/quick
- Not stable
- Slower in practice on modern systems

---

## #️⃣ Hash Tables: Complete Model

### Hash Function Design

**Purpose:** Map key domain (potentially infinite) to bucket range [0, m-1]

**Quality Criteria:**
- **Uniformity:** Keys distributed evenly across buckets
- **Speed:** O(1) computation
- **Deterministic:** Same input → same output
- **Low collision probability:** Different keys minimize hash collisions

**Examples:**

| Key Type | Hash Function | Quality |
|----------|---------------|---------|
| Integer | h(x) = x mod m | Good if m prime, bad if m power of 2 |
| Integer pair | h(x, y) = (a·x + b·y) mod m | Good if a, b coprime with m |
| String (naive) | h(s) = sum(s[i]) mod m | Bad (anagrams collide) |
| String (rolling) | h(s) = s[0]·B^(m-1) + ... + s[m-1] (mod p) | Excellent (Rabin-Karp) |

### Collision Resolution: Separate Chaining

```
Hash Table with separate chaining (m = 5 buckets):

Bucket 0 → [15] → [20] → null
Bucket 1 → [6] → [21] → null
Bucket 2 → [12] → null
Bucket 3 → null
Bucket 4 → [9] → null
```

**Operations:**
- **Insert(key, value):** Compute h(key), append to list at bucket h(key). O(1) amortized
- **Search(key):** Compute h(key), scan list for key. O(1 + chain length) expected
- **Delete(key):** Compute h(key), find and remove from list. O(1 + chain length) expected

**Load Factor:** α = n / m (average items per bucket)
- If α = 1: Average chain length 1
- If α = 0.5: Average chain length 0.5 (empty slots)
- If α = 2: Average chain length 2 (more collisions)

**Expected time:** O(1 + α)
- Keep α ≤ 1 for good performance
- Typical threshold: Resize when α > 0.75

**Resizing:**
- When α exceeds threshold, double m (choose next prime)
- Rehash all n items into new table: O(n)
- Amortized: Insert is still O(1) amortized (many O(1) insertions spread out one O(n) resize)

### Collision Resolution: Open Addressing

**Linear Probing:**
```
Insert into table of size 5:
Keys: [12, 6, 15, 21, 9]
If all hash to different positions, no problem.
If 12 and 6 both hash to position 1:
  12 goes to position 1
  6 tries 1 (occupied) → tries 2 (free) → inserts at 2
```

- **Primary clustering:** Hash values cluster, creating long chains
- **Complexity:** O(1 / (1 - α)) average case
  - If α = 0.5: O(1 / 0.5) = O(2) acceptable
  - If α = 0.9: O(1 / 0.1) = O(10) terrible

**Double Hashing:**
```
If h1(key) occupied, try:
  h1(key) + h2(key)
  h1(key) + 2·h2(key)
  h1(key) + 3·h2(key)
  ...
```

- Better distribution than linear probing
- Still O(1 / (1 - α)) but with better constants
- Requires h2(key) coprime with m for all slots to be tried

### Rolling Hash & Rabin-Karp String Matching

**Problem:** Find all occurrences of pattern P in text T

**Naive:** Compare P with each T[i..i+m-1], O(n·m) character comparisons

**Rolling Hash Idea:**
- Hash polynomial: H(S) = S[0]·B^(m-1) + S[1]·B^(m-2) + ... + S[m-1] (mod p)
- **Rolling property:** Update in O(1)!
  ```
  H(T[i+1..i+m]) = (H(T[i..i+m-1]) - T[i]·B^(m-1)) · B + T[i+m]
  ```

**Rabin-Karp Algorithm:**

```
Pattern: "ABC", Text: "ABCDE"

Step 1: Hash pattern and first window
  H(ABC) = ...
  H(ABD) = ... (actually ABC first)
  
Step 2: Roll through text
  H(BCD) = update from H(ABC) in O(1)
  H(CDE) = update from H(BCD) in O(1)
  
Step 3: When hash matches, verify characters
```

- **Complexity:** O(n + m) average (hashing + rolling)
- **Worst-case:** O(n·m) if many hash collisions
- **Verification:** If hashes match, still verify characters (rolling hash can collide)

**Multi-pattern Matching (Aho-Corasick):**
- Similar idea but search for multiple patterns simultaneously
- Build automaton, O(n + m + k) where k = number of matches

---

## ❌ Seven Common Misconceptions (Corrected)

### Misconception 1: Elementary Sorts Are Completely Useless
**False Reality:** Insertion sort is used in practice (hybrid algorithms, small arrays). Elementary sorts teach fundamental thinking.

### Misconception 2: Bigger O-notation Means Always Slower
**False Reality:** O(n²) with small constant can outrun O(n log n) with large constant on small inputs. Constants matter!

### Misconception 3: Min-Heap Is a Sorted Array
**False Reality:** Min-heap only guarantees min at root and parent ≤ children. Full sorting would destroy O(log n) operations.

### Misconception 4: Any Hash Function Works Equally
**False Reality:** `h(x) = x mod m` with m = 10 will group multiples of 10 together (bad). Prime m helps.

### Misconception 5: Hash Tables Are Always O(1)
**False Reality:** Average O(1) requires good hash function and load factor management. Bad hash → O(n), poor management → chains grow.

### Misconception 6: Rabin-Karp Is Always O(n)
**False Reality:** O(n + m) average, but O(n·m) worst-case if many hash collisions. Still practical because collisions rare.

### Misconception 7: Stability Doesn't Matter
**False Reality:** Stability matters when equal elements have meaning (sorting records by multiple fields, secondary sorts).

---

## 🔗 Week 03 Connections

**From Week 02:**
- Arrays are foundation for sorting and heap arrays
- Linked lists used in separate chaining
- Binary trees conceptually connected to heaps

**To Week 04 (Patterns):**
- Sorting as preprocessing
- Hash tables for complement/frequency patterns
- Heap for priority queue patterns

**To Week 05 (Critical Patterns):**
- Hash tables for multiple patterns (complement, frequency)
- Sorted arrays for efficient searching
- Merge sort background for merge operations

**To Week 07 (Trees):**
- Heaps are complete binary trees in array form
- Understanding tree height O(log n) from heap operations
- BST concepts build on hash table understanding

**To Week 08+ (Graphs):**
- Dijkstra's algorithm uses priority queue (heap)
- Kruskal's MST uses sorting edges
- Hash tables for adjacency tracking

---

## ✅ Interview Readiness Checklist

You're ready for interviews when:

1. **Pattern Recognition:** Instantly identify which algorithm fits: sort? hash? heap?
2. **Implementation:** Code all sorting variants, heap operations, hash table without reference
3. **Trace Manually:** Trace merge sort, quick sort, heap operations on paper, predict comparisons
4. **Trade-offs:** Explain memory vs time, stability vs speed, worst-case vs average-case
5. **Real-world:** Know why std::sort uses introsort, why Python uses Timsort, why databases use B-trees
6. **Optimization:** Identify when O(n) preprocessing (sorting) enables O(log n) lookups later
7. **Edge Cases:** Handle already-sorted input, duplicates, empty arrays, single element

---

**Next:** Week 03 Interview Q&A Reference  
**Review Time:** 2-3 hours

