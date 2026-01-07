# 📘 Week 03 Support: Algorithm Patterns & Techniques

**Week:** 3 | **Category:** Patterns & Practical Techniques  
**Purpose:** Recognize recurring patterns across sorting, heaps, and hashing  
**Audience:** Engineers building intuition for algorithmic problem-solving

---

## 🎯 PATTERN 1: Divide-and-Conquer

### The Core Principle

**Pattern:** Divide problem into independent subproblems, solve recursively, combine results.

**Three Phases:**
1. **Divide:** Split into k subproblems of size n/k
2. **Conquer:** Recursively solve each subproblem
3. **Combine:** Merge solutions

### Where It Appears (Week 3)

**Merge Sort:**
```
Divide:  [5,2,8,1] → [5,2] and [8,1]
Conquer: Recursively sort [5,2] and [8,1]
Combine: Merge [2,5] and [1,8] → [1,2,5,8]
```

**Quick Sort:**
```
Divide:  Choose pivot 5, partition [5,2,8,1] → [2,1,5,8]
Conquer: Recursively sort left [2,1] and right [8]
Combine: Already sorted by partition!
```

**Heap Sort (Implicit):**
```
Build heap (bottom-up divide-and-conquer of tree structure)
Extract repeatedly (conquer via bubble-down)
```

### Pattern Recognition Questions

1. **Can you split the problem?** (Divide)
2. **Are subproblems independent?** (No overlap)
3. **Can you combine solutions?** (Tractable combine cost)

If YES to all → consider divide-and-conquer

---

## 🎯 PATTERN 2: Trade-Off Optimization

### The Core Principle

**Pattern:** Different algorithms optimize different dimensions. Choose based on constraints.

### Week 3 Examples

**Sorting Algorithm Trade-Offs:**

```
Quick Sort: Fast average (low constants), risky worst-case O(n²)
Merge Sort: Guaranteed O(n log n), slower in practice, O(n) space
Heap Sort:  Guaranteed O(n log n), in-place, even slower than merge

When n = 1,000,000:
├─ Quick sort: Usually best (small constants dominate)
├─ Merge sort: Predictable, needed for stability
└─ Heap sort: Theoretical elegance, practical pain
```

**Hash Table Trade-Offs:**

```
Separate Chaining:
├─ Pros: Simple, flexible load factor, good cache misses OK
├─ Cons: Pointer overhead, poor cache locality
└─ Used: Python dict, Java HashMap

Open Addressing (Linear Probing):
├─ Pros: Cache-friendly, no pointers, fast average
├─ Cons: Can't exceed ~75% full, clustering issues
└─ Used: C++ STL unordered_map, high-performance systems
```

### Pattern Recognition Questions

1. **What's the bottleneck?** (Time? Space? Cache? Stability?)
2. **What can I trade?** (Sacrifice space for time? Stability for speed?)
3. **What constraint dominates?** (Is it CPU? Memory? I/O?)

---

## 🎯 PATTERN 3: Implicit Encoding

### The Core Principle

**Pattern:** Encode structure implicitly (via index/formula) instead of explicitly (via pointers).

### Week 3 Examples

**Heap (Implicit Tree in Array):**

```
Explicit (Pointer-based):
struct Node { int val; Node* left; Node* right; }
// Need pointer dereference per navigation → cache misses

Implicit (Array-based Heap):
int[] heap = [50, 30, 20, 10, 5, 15];
// Parent of i: (i-1)/2
// Left child of i: 2*i + 1
// No pointers, just arithmetic → cache-friendly!
```

**Hash Table (Implicit Buckets):**

```
Explicit (Separate Chaining):
Entry[] buckets;
buckets[h] → linked list → follow pointers → O(1+α) with cache misses

Implicit (Open Addressing):
Entry[] table;
h → check table[h], table[(h+1)%m], ... → O(1+α) with no pointers
```

### Pattern Recognition Questions

1. **Can you compute position/relationship via formula?** (Arithmetic)
2. **Can you avoid storing explicit pointers?** (Memory+speed win)
3. **Does structure allow arithmetic encoding?** (Trees, heaps yes; arbitrary graphs no)

---

## 🎯 PATTERN 4: Load Factor & Dynamic Resizing

### The Core Principle

**Pattern:** Maintain performance by resizing when load factor exceeds threshold.

### Week 3 Examples

**Hash Tables:**

```
Load factor α = n / m

As α increases:
├─ α = 0.25: Average chain length 0.25 → O(1.25) search
├─ α = 0.50: Average chain length 0.50 → O(1.50) search
├─ α = 0.75: Average chain length 0.75 → O(1.75) search
├─ α = 1.00: Average chain length 1.00 → O(2.00) search
└─ α = 2.00: Average chain length 2.00 → O(3.00) search (too slow!)

Strategy: When α > threshold (0.75), resize to double buckets
Result: New α = 0.375 → back to fast O(1) search
```

**Dynamic Arrays:**

```
Same principle: When array full, allocate double size
Copy existing elements: O(n)
Amortized per insertion: O(1)
```

### Pattern Recognition Questions

1. **Does performance degrade as utilization increases?** (YES)
2. **Can you enlarge the structure?** (Cost O(n) but amortized OK)
3. **What threshold triggers resize?** (Usually 50-75%)

---

## 🎯 PATTERN 5: Randomization for Average-Case

### The Core Principle

**Pattern:** Use randomization to avoid worst-case adversarial inputs.

### Week 3 Examples

**Quick Sort:**

```
Bad pivot (always smallest/largest): O(n²) via skewed partition
Good pivot (always median-ish): O(n log n)

How to guarantee good pivot?
→ Choose random pivot!

Probability analysis:
├─ Prob(pivot is in middle 50%): 50%
└─ Expected depth: O(log n) despite bad luck possible

Result: O(n log n) average with negligible O(n²) probability
```

**Hash Functions:**

```
Adversarial Input: Attacker crafts keys that all hash to same bucket
Result: O(n) degeneration

Solution 1: Randomized hash seed
├─ Each hash table instance uses different random seed
├─ Attacker can't predict hashes across restarts
└─ Expected O(1) despite adversarial input

Solution 2: Universal Hashing
├─ Randomly choose from universal family H
├─ For ANY two keys, P(collision) ≤ 1/m
└─ Guarantees hold regardless of input pattern
```

### Pattern Recognition Questions

1. **Is there a worst-case adversarial input?** (YES)
2. **Can you randomize the algorithm?** (Pivot selection, hash seed)
3. **Does randomization kill worst-case but keep average-case?** (YES)

---

## 🎯 PATTERN 6: Amortized Analysis

### The Core Principle

**Pattern:** Accept occasional O(n) operations if amortized to O(1) per operation.

### Week 3 Examples

**Hash Table Resizing:**

```
Sequence of n insertions:
  0-3:    O(1) each (no resize) → 4 operations
  4:      O(4) (resize to m=8) → 4 operations
  5-7:    O(1) each → 3 operations
  8:      O(8) (resize to m=16) → 8 operations
  9-15:   O(1) each → 7 operations
  16:     O(16) (resize to m=32) → 16 operations
  ...

Total: 4 + 4 + 3 + 8 + 7 + 16 + ... = O(n) for all insertions
Amortized per insertion: O(n) / n = O(1)
```

**Why Amortized Matters:**
- Individual insertions might be O(n), but rare
- Average case is O(1)
- Total work for n operations is O(n)

### Pattern Recognition Questions

1. **Do expensive operations happen rarely?** (YES, resizes at powers of 2)
2. **Is total work linear?** (Sum of cost over n operations)
3. **Can you average the cost?** (O(total) / n = amortized)

---

## 🎯 PATTERN 7: Stability Preservation

### The Core Principle

**Pattern:** Some algorithms preserve original order for equal elements (stable); others don't.

### Week 3 Examples

**Stable Sorts:**

```
Merge Sort:
  When merging [2a, 5a] and [2b, 5b]:
  Compare keys: 2 == 2? Take left first → [2a, 2b, ...]
  Result: Preserves original order (a before b) ✓

Insertion Sort:
  When inserting element in sorted prefix:
  Insert before (not after) equal elements → preserves order ✓
```

**Unstable Sorts:**

```
Quick Sort:
  Partition around pivot:
  Left < pivot < right
  No guarantee about order of elements with same key
  Result: Order not preserved ✗

Heap Sort:
  Extract-min repeatedly:
  No ordering guarantee for equal elements
  Result: Order not preserved ✗
```

### When Stability Matters

```
Database multi-column sort:
  Sort by (priority, timestamp)
  If priority sort unstable, loses original timestamp order

User interface:
  User expects "equal items stay in original order"
  Unstable sort surprises users
```

---

## 📚 SUPPLEMENTARY: ALGORITHM SELECTION MATRIX

**Situation → Algorithm:**

| Situation | Best Algorithm | Why |
|-----------|---|---|
| Small n (< 50) | Insertion Sort | Low constants, simple, stable |
| Nearly sorted | Insertion Sort | O(n) on nearly sorted input |
| Need stable | Merge Sort | Only good stable O(n log n) |
| In-place O(n log n) | Heap Sort | Sacrifices constants for guarantee |
| Fast average case | Quick Sort | Low constants, cache-friendly |
| Need worst-case guarantee | Merge Sort | Or Heap Sort |
| Integer keys [0, k] | Counting Sort | O(n+k) beats O(n log n) |
| Multi-digit integers | Radix Sort | O(d·n) vs O(n log n) |
| External sorting (> RAM) | External Merge Sort | Minimizes disk I/O |
| Library function | Introsort variant | Quick + Heap fallback |

---

**Document Status:** ✅ COMPLETE  
**Patterns Covered:** 7 major with sub-patterns  
**Practical Focus:** Problem-solving, algorithm selection

