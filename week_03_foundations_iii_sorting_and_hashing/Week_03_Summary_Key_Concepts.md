# 🗺 Week 03 – Concept Map & Summary (Sorting & Hashing Foundations)

## 🌐 High-Level Overview

Week 03 ties together **ordering** (sorting) and **fast access by key** (hashing). Sorting algorithms train you to think about **global structure** (rearranging arrays for later use), while hashing teaches **direct access with collisions and probabilistic guarantees**.

You should end this week being able to:

- Visualize **how** each sorting algorithm moves elements.
- Explain **why** `O(n log n)` sorts are preferred and when `O(n²)` sorts are fine.
- Describe **how hash tables actually resolve collisions** and what can go wrong.

---

## 🔗 Concept Map (Textual)

- **Sorting:**
  - Elementary: bubble, selection, insertion
  - Divide-and-conquer: merge sort, quick sort
  - Heap-based: heap sort (via binary heap)

- **Hash Tables:**
  - Hash function + load factor
  - Collision strategies:
    - Separate chaining (linked lists / arrays)
    - Open addressing (linear, quadratic, double hashing)
  - Advanced hashing:
    - Robin Hood hashing
    - Cuckoo hashing
    - Universal hashing
    - Perfect hashing (for static sets)

Relationships:

- Merge sort and heap sort both achieve `O(n log n)` but use **different structures** (arrays vs heaps).
- Quick sort and hashing both rely on **good distribution** properties of functions (pivot choice vs hash function).
- Separate chaining vs open addressing are different design points for **handling collisions**.

---

## 📚 Key Concepts Per Day

### 🟦 Day 1 – Elementary Sorts

- **Bubble Sort**
  - Repeatedly swap adjacent out-of-order elements.
  - Good mental model for “bubbling” large values to the end.
  - Acceptable for very small `n` or nearly sorted arrays.

- **Selection Sort**
  - Repeatedly select the minimum (or maximum) and place it at the correct position.
  - Always performs `O(n²)` comparisons regardless of input order.
  - Very simple but rarely used in production except in constrained environments.

- **Insertion Sort**
  - Treat prefix as sorted, insert each new element into this sorted prefix.
  - Excellent for nearly sorted data and very small arrays.
  - Often used as the “base case” algorithm inside hybrid sorts (e.g., TimSort).

---

### 🟦 Day 2 – Merge Sort & Quick Sort

- **Merge Sort**
  - Divide array into halves, recursively sort each half, merge sorted halves.
  - Stable, predictable `O(n log n)` time, but uses extra `O(n)` space.
  - Widely used in external sorting and library implementations where stability matters.

- **Quick Sort**
  - Partition around a pivot, recursively sort left and right partitions.
  - Average `O(n log n)` but worst-case `O(n²)` (e.g., sorted input with bad pivot selection).
  - In-place, very cache-friendly, often faster in practice than merge sort with good pivot strategy.

- **Partition Schemes**
  - Lomuto vs Hoare partitioning: different mechanics and pivot positions; impacts performance and stability.

---

### 🟦 Day 3 – Heap Sort & Priority Queues

- **Binary Heap**
  - Complete binary tree stored in array; parent–child relationships derived by index arithmetic.
  - Maintains heap property: each node ≤ (min-heap) or ≥ (max-heap) its children.

- **Heap Operations**
  - `heapify` builds a heap from an array in `O(n)`.
  - `insert` and `extract-min/max` in `O(log n)`.

- **Heap Sort**
  - Build a heap, repeatedly extract-min/max and place at end of array.
  - Time `O(n log n)`, in-place, not stable but very predictable.

- **Priority Queues**
  - Abstract view of heap: operations like “push task” and “pop highest priority”.
  - Used in algorithms like Dijkstra’s and in schedulers.

---

### 🟦 Day 4 – Hash Tables I (Separate Chaining)

- **Hash Function & Table**
  - Maps keys to bucket indices using `hash(key) % m`.
  - Load factor `α = n/m` controls expected bucket length.

- **Separate Chaining**
  - Each bucket holds a linked list or small dynamic array of entries.
  - Simple deletion (unlink node), easy to implement and extend.

- **Performance**
  - Average `O(1 + α)` for lookup and insert with a good hash function and bounded `α`.
  - Worst-case `O(n)` when many keys collide or when hash function is poor.

---

### 🟦 Day 5 – Hash Tables II (Open Addressing & Advanced Hashing)

- **Open Addressing**
  - Stores all entries in a single array; on collision, probe alternative slots.
  - Strategies:
    - Linear probing
    - Quadratic probing
    - Double hashing

- **Clustering**
  - Primary and secondary clustering reduce performance at high load factors.
  - Must monitor and control load factor more aggressively than with chaining.

- **Advanced Hashing**
  - Robin Hood hashing: evens out probe distances.
  - Cuckoo hashing: uses multiple tables and guaranteed `O(1)` worst-case lookups.
  - Universal hashing: random hash selection to prevent adversarial collision attacks.
  - Perfect hashing: collision-free hashing for static key sets.

---

## 📊 Comparison & Relationship Table (Sorting)

```markdown
| 🧩 Algorithm      | ⏱ Time (Avg)   | ⏱ Time (Worst) | 💾 Space     | 🔁 Stable | ✅ Best Use-Case                                 |
|-------------------|----------------|----------------|-------------|---------|--------------------------------------------------|
| Bubble Sort       | O(n²)          | O(n²)          | O(1)        | Yes     | Tiny arrays, educational, nearly sorted         |
| Selection Sort    | O(n²)          | O(n²)          | O(1)        | No      | Very small arrays, minimal swaps                 |
| Insertion Sort    | O(n²)          | O(n²)          | O(1)        | Yes     | Nearly sorted arrays, small segments in hybrids |
| Merge Sort        | O(n log n)     | O(n log n)     | O(n)        | Yes     | Stable sort requirements, external sorting      |
| Quick Sort        | O(n log n)     | O(n²)          | O(log n)*   | No      | General-purpose in-memory sorting               |
| Heap Sort         | O(n log n)     | O(n log n)     | O(1)        | No      | Predictable performance, limited extra memory   |
```

`*` Stack space for recursion.

---

## 📊 Comparison & Relationship Table (Hashing)

```markdown
| 🧩 Strategy                | ⚙ Collision Handling        | ⏱ Avg Lookup  | 🔴 Worst Lookup | 💾 Space     | ✅ Best For                                      |
|----------------------------|-----------------------------|---------------|-----------------|-------------|-------------------------------------------------|
| Separate Chaining          | Linked list / small array   | O(1 + α)      | O(n)            | O(n + m)    | Flexible load factor, frequent deletions        |
| Linear Probing             | Probe i+1, i+2, ...         | O(1/(1-α))    | O(n)            | O(m)        | Cache-friendly, low α                           |
| Quadratic Probing          | Probe i+1², i+2², ...       | O(1/(1-α))    | O(n)            | O(m)        | Reduced primary clustering                      |
| Double Hashing             | h1 + i*h2                   | O(1/(1-α))    | O(n)            | O(m)        | Higher load factors, fewer clusters             |
| Robin Hood Hashing         | Steal from short probes     | ~O(1)         | O(log n) exp    | O(m)        | Lower variance, more predictable performance    |
| Cuckoo Hashing             | Multiple tables, eviction   | O(1)          | O(1) lookup     | O(m) 2x     | Hard O(1) lookup guarantees                     |
| Perfect Hashing (static)   | Collision-free mapping      | O(1)          | O(1)            | O(n)        | Static key sets, compilers, dictionaries        |
```

---

## 💡 Key Insights (5–7 Bullets)

- **Not all `O(n log n)` sorts behave the same**: merge sort trades extra memory for stability, quick sort trades worst-case risk for in-place speed, heap sort trades intuitive structure for more complex constant factors.

- **Quadratic sorts are not “useless”**: insertion sort is widely used inside real-world hybrid algorithms (like TimSort) for small subarrays.

- **Hash tables are not magically `O(1)`**: their performance depends on good hash functions, controlled load factors, and well-chosen collision strategies.

- **Separate chaining vs open addressing** is a **time–space–locality trade-off**: chaining uses pointers and extra memory, open addressing uses contiguous arrays and better cache locality.

- **Advanced hashing (Cuckoo, Robin Hood, universal, perfect)** is about **controlling worst-case behavior and adversarial inputs**, not just average speed.

- **Understanding heaps, merges, partitions, and hash probing at a mechanical level** makes later topics (graphs, dynamic programming, system design) much easier.

---

## ⚠ 5 Misconceptions Fixed

1. **“All `O(n²)` sorts are bad and should never be used.”**  
   → Insertion sort is critical for small or nearly sorted segments; real-world library sorts use it internally.

2. **“Merge sort and quick sort are interchangeable since both are `O(n log n)`.”**  
   → Merge sort is stable but uses extra memory; quick sort is in-place but can hit `O(n²)` without careful pivot selection.

3. **“Heap sort is just another `O(n log n)` sort, nothing special.”**  
   → Heap sort is conceptually linked to **priority queues**, which are essential in algorithms like Dijkstra’s.

4. **“Hash tables are always O(1), so implementation details do not matter.”**  
   → Collision strategy, load factor, and hash function quality dramatically affect real performance and security.

5. **“Separate chaining is strictly worse than open addressing.”**  
   → Chaining often handles high load factors and frequent deletions more gracefully; open addressing is better in cache-heavy, low-deletion scenarios.

---

## ✅ How to Use This Summary

- Use this file as a **quick revision** at the end of Week 03.  
- Before moving to Week 04:
  - Revisit the comparison tables.
  - Try to **explain each row aloud** without reading the description.
  - Re-draw at least one diagram per major concept (sorts, heaps, hash tables) from memory.
