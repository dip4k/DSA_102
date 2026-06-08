# 📋 Week 02 Guidelines: Linear Data Structures & Binary Search Foundations

**Week Overview:** Foundations II — Understanding arrays, dynamic arrays, linked lists, stacks, queues, deques, binary search, and character string/number conversions.  
**Primary Goal:** Master fundamental linear data structures, their low-level memory implications, and invariant-driven algorithms.  
**Time Allocation:** 24-28 hours core learning + deep practice.  

---

## 🎯 Week 02 Learning Arc

This week, you move from abstract computational fundamentals (Week 01: memory hierarchy, complexity classes, recursion trees) to concrete data structure implementations. You'll master the linear shapes that form the backbone of almost every modern software system. By week's end, you'll understand why static arrays are fast for random access, why dynamic arrays grow geometrically, how linked lists trade cache locality for insertion flexibility, how stacks and queues enforce access constraints, why binary search achieves logarithmic scaling, and how numbers and text are represented at the hardware level.

---

## 📚 Day-by-Day Comprehensive Overview

### Day 1: Static Arrays & Memory Layout

**Core Mental Model:** Arrays are blocks of contiguous physical memory. Element references are converted into fast, loop-free address arithmetic formulas.

*   **Contiguous Layout**: Elements live right next to other in physical addresses.
*   **Index-Address Mapping**: `address = base + (index x element_size)`.
*   **Random Access**: O(1) constant time, regardless of array size.
*   **Cache Locality**: Accessing sequential coordinates yields CPU cache prefetching hits, whereas column-wise or random indexing causes cache line misses and system stalls.
*   **Row-major vs. Column-major**: Row-major stored contiguously row after row (C, C++, Java, C#); Column-major stored column after column (Fortran).

---

### Day 2: Dynamic Arrays & Amortized Growth

**Core Mental Model:** Growable arrays manage a logical size alongside a physical capacity, trading memory overhead for constant average-case insertions.

*   **Logical Length vs. Physical Capacity**: Length tracks valid elements; Capacity tracks allocated memory cells.
*   **Growth Strategy**: Double physical capacity when full: new capacity = 2 x old capacity.
*   **Amortized Complexity**: Rescaling requires an O(N) copy operation, but doubling happens rarely. Over N insertions, total work sums geometrically to 2N - 1 = O(N) total, bringing the average (amortized) cost per append to O(1).
*   **Under-allocation Risk**: Growing by 1 cell on each insert (linear scaling) results in O(N^2) total work for N elements.

---

### Day 3: Linked Lists

**Core Mental Model:** Scattered node blocks connected dynamically through pointer variables. Discard memory contiguity to achieve constant-time insertions at known positions.

*   **Structure**: Node objects containing values and adjacent next/prev pointers.
*   **Random Access**: O(N) traversal. Must traverse nodes one by one starting from the head.
*   **In-Place Pointer Swaps**: Operations like reversing, splitting, and merging are solved efficiently by updating pointer references.
*   **Locality Deficit**: Because nodes are allocated randomly across the system heap, dereferencing pointers causes repeated L1/L2 cache misses.

---

### Day 4: Stacks, Queues & Deques

**Core Mental Model:** Specialized data structures enforcing strict access discipline.

*   **Stacks (LIFO: Last-In, First-Out)**: Elements pushed and popped only at the top of the structure. Ideal for function call frame tracking, parentheses parsing, and graph Depth-First Search (DFS).
*   **Queues (FIFO: First-In, First-Out)**: Elements appended at the back (enqueue) and removed from the front (dequeue). Critical for resource scheduling, buffering, and Breadth-First Search (BFS) layer-by-layer exploration.
*   **Deques (Double-Ended Queues)**: Both ends allow O(1) insertions and deletions. Leveraged for sliding window optimizations and monotonic tracking.
*   **Circular Buffer**: Array-based queue implementation that uses modular wraparound arithmetic (`index = (index + 1) % capacity`) to prevent index drift.

---

### Day 5: Binary Search & Invariants

**Core Mental Model:** Half-interval search over sorted data by maintaining a search invariant: `target (if it exists) is inside [low, high)`.

*   **Indices Division Invariant**: Calculate `mid = low + (high - low) / 2` to prevent memory offset overflows.
*   **Binary Search Variants**: First/last occurrence, lower bound (first element >= target), and upper bound (first element > target) modify boundaries.
*   **Search on Answer Space**: Transform optimization objectives into feasibility checks. Verify if option X is possible in O(N) time, and binary search to find the minimum feasible value in O(log(max) x N) steps.

---

### Day 6: Strings & Numbers - Representations & Conversions

**Core Mental Model:** Text and numeric primitives are byte streams stored in physical registers and heap buffers.

*   **Character Encoding**: ASCII (8-bit), Unicode, and UTF-8/UTF-16.
*   **String Immutability**: String allocations cannot be changed once created. Modifying characters inside a loop creates copy overhead of O(N^2) complexity. Use `StringBuilder` buffer arrays to collect appends in linear O(N) time.
*   **Numeric Structures**: Signed integers represented via two's complement. Overflow occurs when a value exceeds the physical bit-width, wrapping values across positive/negative boundaries.
*   **Double-Direction Conversions**: Implement parsing (`atoi`) and printing (`itoa`) from scratch with proper trailing whitespaces, sign characters, and overflow checks.

---

## 📅 Recommended Time Allocation

| Day | Topic | Selected Focus Area | Practice Targets | Time (hours) |
|---|---|---|---|---|
| **Day 1** | Static Arrays | Cache Line Strides & Matrix Formats | Address Calculator, Stride Benchmark | 3–4 hrs |
| **Day 2** | Dynamic Arrays | Amortized Analysis & Doubling | Push with Resizing, Shrink, Pop | 3–4 hrs |
| **Day 3** | Linked Lists | Reversals, Slow & Fast Pointers | Reverse SLL, Detect Cycle, Middle | 4–5 hrs |
| **Day 4** | Stacks & Queues | Access Discipline & Circular Buffer | Circular Queue, Min Stack, Parentheses | 4–5 hrs |
| **Day 5** | Binary Search | Boundary Cases & Answer Space | Lower Bound, Rotated Array, Splitting | 5–6 hrs |
| **Day 6** | Strings & Numbers| Memory Headers, Encodings, Converts | Code atoi & itoa from first principles | 4–5 hrs |

---

## 🛡️ Six Common Pitfalls (and How to Avoid Them)

### Pitfall 1: Ignoring Cache Stride Limits
*   *Wrong*: Looping down matrix columns vertically in contiguous column loops.
*   *Fix*: Perform iterations in Row-Major order where innermost loops match contiguous column addresses in physical memory.

### Pitfall 2: Thrashing Resizes
*   *Wrong*: Shrinking dynamic array capacity immediately when length drops below capacity/2. This causes repeated modifications and resizes if you push and pop near the boundary.
*   *Fix*: Shrink only when length drops below capacity/4.

### Pitfall 3: Pointer Loss
*   *Wrong*: Updating node links before saving references to the rest of the list.
*   *Fix*: Always pre-allocate temporary pointer references (e.g. `next = current.next`) before rewiring.

### Pitfall 4: Queueing inside Lists
*   *Wrong*: Using `List<T>.RemoveAt(0)` to dequeue from a queue. This forces $O(N)$ index shifts on every pop.
*   *Fix*: Use a `Queue<T>` with a circular buffer or a linked list backend.

### Pitfall 5: Binary Search Overflow
*   *Wrong*: Computing midpoints using `mid = (low + high) / 2`.
*   *Fix*: Always use `mid = low + (high - low) / 2`.

### Pitfall 6: Arithmetic Overflow in Conversions
*   *Wrong*: Accumulating string character digits directly into integer spaces without checking if the value exceeds max boundaries.
*   *Fix*: Check if `result > (Integer_Max - digit) / 10` before multiplying and adding.

---

## 🎓 Mastery Signals

You've mastered Week 02 when:
1.  **You understand memory layouts**: You can explain index address mapping, cache prefetching, and reference pointer sizes instantly.
2.  **You analyze amortized bounds**: You can explain why doubling gives O(1) amortized insertion, and why linear incremental growth degrades to quadratic complexity.
3.  **You write zero-error pointer code**: You can reverse lists, detect cycles, find middles, and manage head/tail references confidently.
4.  **You implement circular structures**: You can code circular arrays and queues with modular wraparound indices cleanly.
5.  **You handle binary search variants**: You can explain lower bounds, upper bounds, and binary search on answer spaces.
6.  **You understand low-level representations**: You can explain immutability, UTF-16 surrogate pairs, two's complement negation, and convert string-to-integer (`atoi`) and integer-to-string (`itoa`) from scratch with proper overflow checks.
