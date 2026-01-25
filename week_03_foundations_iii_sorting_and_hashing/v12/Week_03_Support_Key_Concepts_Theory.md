# 📘 Week 03 Support: Key Concepts & Theory Reference

**Week:** 3 | **Category:** Foundations / Theoretical Underpinnings  
**Purpose:** Deep conceptual understanding of sorting, heaps, and hashing  
**Audience:** Engineers seeking theoretical mastery before implementation

---

## 📊 PART I: FOUNDATIONAL CONCEPTS

### 1. Comparison-Based Sorting Lower Bound

**Theorem:** Any comparison-based sorting algorithm requires Ω(n log n) comparisons in the worst case.

**Proof Sketch:**
- There are n! possible permutations of n elements
- Each comparison answers one binary question (< or ≥)
- To distinguish all n! permutations, need log₂(n!) = Θ(n log n) comparisons
- Therefore, no comparison algorithm can do better than O(n log n)

**Implications:**
- Merge sort and heap sort achieve this lower bound → **optimal**
- Quick sort achieves it on average but not worst-case
- Bubble/selection/insertion sorts are O(n²) → **suboptimal**

**Non-Comparative Sorting Exceptions:**
- Counting sort: O(n + k) where k = key range
- Radix sort: O(d(n + 10)) where d = number of digits
- These avoid lower bound by exploiting problem structure (integers, bounded range)

---

### 2. The Recurrence Relation Master Theorem

**Form:** T(n) = a·T(n/b) + f(n)

**Solutions (Simplified):**
- If f(n) = O(n^(log_b(a) - ε)) → T(n) = Θ(n^(log_b(a)))
- If f(n) = Θ(n^(log_b(a))) → T(n) = Θ(n^(log_b(a)) · log n)
- If f(n) = Ω(n^(log_b(a) + ε)) and a·f(n/b) ≤ c·f(n) → T(n) = Θ(f(n))

**Examples:**
```
Merge sort: T(n) = 2·T(n/2) + O(n)
  a=2, b=2, f(n)=O(n)
  log_b(a) = log_2(2) = 1
  f(n) = O(n^1) = Θ(n^(log_b(a)))
  → T(n) = Θ(n log n) ✓

Quick sort: T(n) = 2·T(n/2) + O(n)  [average case]
  Same as merge sort
  → T(n) = Θ(n log n) average ✓

Binary search: T(n) = 1·T(n/2) + O(1)
  a=1, b=2, f(n)=O(1)
  log_b(a) = 0
  f(n) = O(1) = Θ(n^0)
  → T(n) = Θ(log n) ✓
```

---

### 3. Heap Property and Completeness

**Heap Properties:**
- **Heap Property:** Parent ≥ all children (max-heap) or Parent ≤ all children (min-heap)
- **Complete Binary Tree:** All levels filled except possibly last; last level filled left-to-right
- **Height:** Always log₂(n), ensuring O(log n) operations

**Why Completeness Matters:**
- Guarantees tree is **balanced** (no skewed subtrees)
- Enables **array representation** (index arithmetic works)
- Ensures **O(log n) height** (not O(n) like unbalanced BSTs)

**Array Index Arithmetic (0-indexed):**
```
Parent of i:    (i - 1) / 2
Left child:     2i + 1
Right child:    2i + 2
```

---

### 4. Load Factor and Amortized Analysis

**Load Factor:** α = n / m (keys in table / bucket capacity)

**Expected Chain Length:** α  
**Expected Search Time:** O(1 + α)

**Amortized Analysis (Intuitive):**
- Insert n items; some trigger resize at α = 0.75
- Resize cost: O(n) to rehash
- Total cost: O(n) pure inserts + O(n) resizing = O(n)
- **Amortized per insertion:** O(n) / n = O(1)

**Why Resize?**
- Without resize: α grows unbounded → search becomes O(n)
- With resize: α stays constant ≈ 0.5 → search stays O(1)

---

### 5. Hash Function Requirements

**Requirement 1: Determinism**
- Same key always produces same hash
- Enables correctness (find inserted keys)

**Requirement 2: Uniformity**
- Keys spread evenly across m buckets
- Expected chain length = α (not concentrated)

**Requirement 3: Speed**
- Computed in O(1) time
- Not O(n) per computation

**Requirement 4: Avalanche Effect**
- Small input change → large output change
- Prevents adversarial patterns (all keys hash to same bucket)

**Bad Hash Function (violates uniformity):**
```csharp
// Hash only depends on first character
int BadHash(string key) => key[0] % 26;
// All strings starting with 'a' hash to same bucket
// Violates uniformity → chains become O(n)
```

**Good Hash Function (FNV-1a):**
```csharp
// Depends on all characters, good distribution
int FnvHash(string key) {
    int hash = 2166136261;
    foreach (char c in key) {
        hash ^= c;
        hash *= 16777619;
    }
    return Math.Abs(hash);
}
```

---

## 📊 PART II: COMPLEXITY TABLES & COMPARISONS

### Sorting Algorithms Comparison Table

| Algorithm | Best | Average | Worst | Space | Stable | In-Place |
|-----------|------|---------|-------|-------|--------|----------|
| **Bubble** | O(n) | O(n²) | O(n²) | O(1) | Yes | Yes |
| **Selection** | O(n²) | O(n²) | O(n²) | O(1) | No | Yes |
| **Insertion** | O(n) | O(n²) | O(n²) | O(1) | Yes | Yes |
| **Merge** | O(n log n) | O(n log n) | O(n log n) | O(n) | Yes | No |
| **Quick** | O(n log n) | O(n log n) | O(n²) | O(log n) | No | Yes |
| **Heap** | O(n log n) | O(n log n) | O(n log n) | O(1) | No | Yes |
| **Counting** | O(n+k) | O(n+k) | O(n+k) | O(k) | Yes | No |
| **Radix** | O(d(n+10)) | O(d(n+10)) | O(d(n+10)) | O(n+10) | Yes | No |

**Key Insights:**
- Elementary sorts: O(n²), small constants, adaptive (bubble on sorted data)
- Divide-and-conquer: O(n log n) guaranteed
- Non-comparative: O(n+k) or O(d·n) if domain is restricted

---

### Data Structure Operations Comparison

| Operation | Array | Linked List | Heap | BST | Hash Table |
|-----------|-------|-------------|------|-----|------------|
| **Search** | O(log n)* | O(n) | O(n) | O(log n)* | O(1)* |
| **Insert** | O(n) | O(1)** | O(log n) | O(log n)* | O(1)* |
| **Delete** | O(n) | O(1)** | O(log n) | O(log n)* | O(1)* |
| **Find Min** | O(n) | O(n) | O(1) | O(log n) | O(n) |
| **Successor** | N/A | O(n) | N/A | O(log n) | N/A |

*\*Requires sorted array for array search; average-case for BST/hash*  
*\*\*After finding position*

---

### Heap vs Priority Queue Implementations

| Operation | Array | Linked List | Heap | Fibonacci Heap |
|-----------|-------|-------------|------|---|
| **Insert** | O(1) | O(1) | O(log n) | O(1) |
| **Find-Min** | O(1) | O(1) | O(1) | O(1) |
| **Extract-Min** | O(n) | O(1) | O(log n) | O(log n) amortized |
| **Decrease-Key** | O(1) | O(1) | O(log n) | O(1) amortized |
| **Delete** | O(n) | O(1) | O(log n) | O(log n) amortized |
| **Merge** | O(n+m) | O(1) | O(n+m) | O(1) |

**When to use:**
- **Array:** Simple, small n
- **Linked list:** Only extract-min matters
- **Heap:** General priority queue, good balance
- **Fibonacci Heap:** Dijkstra's algorithm (fewer decrease-key calls)

---

### Hash Table Collision Resolution

| Strategy | Chaining | Linear Probing | Quadratic | Double Hash |
|----------|----------|---|---|---|
| **Average Lookup** | O(1+α) | O(1/(1-α)) | O(1/(1-α)) | O(1/(1-α)) |
| **Clustering** | None | Primary | Secondary | None |
| **Max α** | > 1 | ≤ 0.75 | ≤ 0.75 | ≤ 0.75 |
| **Cache-Friendly** | Poor | Excellent | Excellent | Good |
| **Implementation** | Simple | Medium | Medium | Complex |

---

## 📊 PART III: BIG-O CHEAT SHEET FOR WEEK 3

### Sorting Algorithms

```
Elementary Sorts (O(n²))
├─ Bubble Sort:     O(n²) worst, O(n) best, O(1) space
├─ Selection Sort:  O(n²) always, O(1) space, not stable
└─ Insertion Sort:  O(n²) worst, O(n) best, O(1) space, stable

Divide-and-Conquer (O(n log n))
├─ Merge Sort:      O(n log n) always, O(n) space, stable
├─ Quick Sort:      O(n log n) avg, O(n²) worst, O(log n) space
└─ Heap Sort:       O(n log n) always, O(1) space, not stable

Non-Comparative
├─ Counting Sort:   O(n + k) where k = key range
├─ Radix Sort:      O(d(n + 10)) where d = digit count
└─ Bucket Sort:     O(n + k) average (depends on bucket distribution)
```

### Heaps

```
Single Heap Operations
├─ Insert:          O(log n) bubble-up
├─ Extract-Min/Max: O(log n) bubble-down
├─ Build-Heap:      O(n) bottom-up heapify
├─ Heap Sort:       O(n log n)
└─ Heap Property:   Parent ≥ children (max-heap)

Priority Queue (via Heap)
├─ Insert:          O(log n)
├─ Extract-Min:     O(log n)
└─ Decrease-Key:    O(log n)
```

### Hash Tables

```
Hash Table (Separate Chaining)
├─ Insert:          O(1) average, O(n) worst
├─ Search:          O(1 + α) average
├─ Delete:          O(1 + α) average
├─ Resize:          O(n)
└─ Load Factor:     α = n/m, keep α < 0.75

Hash Table (Open Addressing)
├─ Insert:          O(1/(1-α)) average
├─ Search:          O(1/(1-α)) average
├─ Delete:          O(1/(1-α)) average (with tombstones)
└─ Clustering:      Primary (linear), Secondary (quadratic)

Karp-Rabin Rolling Hash
├─ Preprocess:      O(m) for pattern
├─ Scan Text:       O(n) with rolling hash
├─ Total:           O(n + m) expected
└─ Worst-Case:      O(nm) if all positions match
```

---

## 📊 PART IV: DECISION TREES

### Which Sorting Algorithm?

```
START: Choosing a sorting algorithm
  │
  ├─ "I need guaranteed O(n log n) worst-case"
  │  └─ Use MERGE SORT (stable, predictable)
  │
  ├─ "I want fast average-case and in-place"
  │  └─ Use QUICK SORT (with good pivot selection)
  │
  ├─ "Data is nearly sorted"
  │  └─ Use INSERTION SORT (O(n) on sorted data)
  │
  ├─ "Keys are integers in range [0, k]"
  │  └─ k < n log n? → COUNTING SORT
  │     └─ k large? → RADIX SORT or QUICK SORT
  │
  ├─ "I need stable sort"
  │  └─ Use MERGE SORT or ensure stability wrapper
  │
  ├─ "Data > RAM (external sort)"
  │  └─ Use EXTERNAL MERGE SORT
  │
  └─ "I need simple, educational implementation"
     └─ Use BUBBLE or INSERTION SORT
```

### Which Hash Table Strategy?

```
START: Choosing collision resolution
  │
  ├─ "I'm writing an interpreter (Python, Java, Ruby)"
  │  └─ Use SEPARATE CHAINING (simple, flexible)
  │
  ├─ "I need maximum performance (C++, systems code)"
  │  └─ Use LINEAR PROBING (cache-friendly)
  │
  ├─ "I need theoretical guarantees"
  │  └─ Use DOUBLE HASHING (minimal clustering)
  │
  ├─ "I'm concerned about hash flooding"
  │  └─ Use UNIVERSAL HASHING (randomized seed)
  │
  ├─ "I have sparse data (many empty buckets)"
  │  └─ Use SEPARATE CHAINING (less memory)
  │
  └─ "I need fastest average-case lookup"
     └─ Use OPEN ADDRESSING (load factor ≤ 0.75)
```

### When to Use Each Data Structure for Priority Queues?

```
START: Choosing priority queue implementation
  │
  ├─ "General-purpose priority queue"
  │  └─ Use BINARY HEAP (O(log n) insert/extract)
  │
  ├─ "Many decrease-key operations (Dijkstra)"
  │  └─ Use FIBONACCI HEAP (O(1) decrease-key amortized)
  │
  ├─ "Need to merge two heaps"
  │  └─ Use BINOMIAL HEAP (O(log n) merge)
  │
  ├─ "Only need to extract min, never insert"
  │  └─ Use UNSORTED ARRAY (O(1) insert, O(n) extract)
  │
  └─ "Simple implementation priority"
     └─ Use SORTED ARRAY (O(1) extract, O(n) insert)
```

---

## 📊 PART V: STABILITY & IN-PLACE DEFINITIONS

### Stability in Sorting

**Definition:** A sort is stable if equal elements retain their original relative order.

**Example:**
```
Original:  [(3, "apple"), (1, "banana"), (3, "cherry"), (2, "date")]
           (sorted by first element)

Stable Sort Result:
  [(1, "banana"), (2, "date"), (3, "apple"), (3, "cherry")]
   Notice: "apple" before "cherry" (original order preserved for equal keys)

Unstable Sort Result:
  [(1, "banana"), (2, "date"), (3, "cherry"), (3, "apple")]
   Notice: "cherry" before "apple" (original order NOT preserved)
```

**Why It Matters:**
- Multi-level sorting (first by priority, then by timestamp)
- Database queries (ORDER BY multiple columns)
- Visualization (maintain user-expected order for ties)

**Stable Algorithms:** Bubble, insertion, merge, radix, counting  
**Unstable Algorithms:** Selection, quick sort (standard), heap sort

### In-Place Sorting

**Definition:** A sort is in-place if it uses O(log n) or less extra space (recursive stack doesn't count).

**Examples:**
```
In-Place (O(1) or O(log n) space):
├─ Bubble:    O(1)
├─ Selection: O(1)
├─ Insertion: O(1)
├─ Quick:     O(log n) recursion stack
└─ Heap:      O(1)

Not In-Place (O(n) extra space):
├─ Merge:     O(n) temp arrays
├─ Counting:  O(k) count array
└─ Radix:     O(n + 10) buckets
```

**Why It Matters:**
- Memory constraints (embedded systems, large datasets)
- Cache efficiency (in-place avoids allocation overhead)
- Theoretical elegance (sorts with minimal extra resources)

---

**Document Status:** ✅ COMPLETE  
**Total Sections:** 5 major parts with 15 subsections  
**Visual Reference:** Comprehensive for Week 03 theory

