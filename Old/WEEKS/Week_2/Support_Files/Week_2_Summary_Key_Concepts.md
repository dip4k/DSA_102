# 📌 Week 2 Summary — Key Concepts & Patterns

## 🗺 Week 2 at a Glance

**Theme:** Linear Structures & Search  
**Topics:**
1. Arrays
2. Dynamic Arrays
3. Linked Lists
4. Stacks & Queues
5. Binary Search

This file is a **quick reference**. Use it:

- Before interviews as a rapid review.
- At the end of the week to ensure you’ve internalized the essentials.
- When deciding which structure to pick in a new problem.

---

## 🧱 Arrays

### 🧠 One-Liner

**Arrays = contiguous lockers in memory with O(1) direct access but O(n) middle insert/delete.**

### 🗂 Core Facts

- Memory layout: **contiguous block** of equal-sized elements.
- Indexing: `address(i) = base + i * element_size`.
- Complexity:
  - Access/assign by index: O(1).
  - Traverse all elements: O(n).
  - Insert/delete in middle: O(n) (shift elements).
  - Insert at end (with capacity): O(1).

### ✅ When to Use

- Need random access by index.
- Data size known, or dynamic array wraps it.
- Frequent reads, occasional writes/inserts.

### ⚠ Pitfalls

- Off-by-one errors with indices [0..n-1].
- Forgetting that insertion/deletion at front/middle is O(n).
- Out-of-bounds access (undefined behavior / exceptions).

---

## 📦 Dynamic Arrays

### 🧠 One-Liner

**Dynamic arrays = stretchy arrays that over-allocate to make appends amortized O(1).**

### 🗂 Core Facts

- Maintain `size` and `capacity`.
- When size hits capacity:
  - Allocate larger array (often 2× capacity).
  - Copy all elements.
- Complexity:
  - `push_back` / `append`: amortized O(1).
  - Rare resize: O(n) copy.
  - Access by index: O(1).
  - Insert/delete in middle: still O(n).

### ✅ When to Use

- Need array semantics but unknown number of elements in advance.
- Frequent appends at end.
- Underlying container for stacks, queues, vectors.

### ⚠ Pitfalls

- Confusing **amortized O(1)** with “always O(1).”
- Ignoring capacity/size differences.
- Over-aggressive shrinking can cause thrashing.

---

## 🔗 Linked Lists (Singly, Doubly, Circular)

### 🧠 One-Liner

**Linked lists = chains of nodes connected by pointers, trading O(1) local updates for O(n) indexing and weaker cache locality.**

### 🗂 Core Facts

- Singly list:
  - Node: value + `next`.
  - Insert/delete at head: O(1).
  - Delete after known node: O(1).
- Doubly list:
  - Node: value + `prev` + `next`.
  - O(1) delete given node (both neighbors).
- Complexity:
  - Access ith element: O(i).
  - Search by value: O(n).
  - Insert/delete given node: O(1).

### ✅ When to Use

- Frequent insert/delete in the middle **with node pointer** (e.g., LRU caches).
- Intrusive data structures (OS, allocators).
- Functional languages (immutable cons lists).

### ⚠ Pitfalls

- Losing nodes by overwriting `next` without saving it.
- Forgetting to update both directions in doubly lists.
- Thinking lists give O(1) random access—they don’t.

---

## 📚 Stacks & Queues

### 🧠 One-Liner

**Stacks implement LIFO; queues implement FIFO. Both are policies layered over arrays or lists.**

### 🗂 Stack (LIFO)

- Operations:
  - `push(x)`, `pop()`, `top()`, `isEmpty()`.
- Complexity:
  - All O(1) (amortized if array-backed).
- Typical uses:
  - Call stack / recursion.
  - Undo/redo.
  - Expression evaluation.
  - Backtracking.

### 🗂 Queue (FIFO)

- Operations:
  - `enqueue(x)`, `dequeue()`, `front()`, `isEmpty()`.
- Implementations:
  - Linked list with head/tail.
  - Circular buffer in array.
- Complexity:
  - Enqueue/dequeue O(1) (no shifting).
- Typical uses:
  - BFS.
  - Scheduling, job queues.
  - Message queues/buffers.

### ⚠ Pitfalls

- For queues, failing to use a circular buffer → O(n) shifts.
- Confusing stack-based DFS with queue-based BFS.
- Ignoring underflow/overflow conditions.

---

## 🔍 Binary Search

### 🧠 One-Liner

**Binary search halves a sorted/monotonic search space each step, finding targets in O(log n).**

### 🗂 Core Facts

- Works on:
  - Sorted arrays.
  - Monotonic predicates over integer ranges.
- Basic pattern:
  - Maintain interval [L, R] or [L, R).
  - Compute mid.
  - Decide which half to keep using comparisons.
- Complexity:
  - O(log n) time.
  - O(1) space (iterative).

### 🔁 Common Variants

- Exact match: find index where A[i] == target.
- Lower bound: first index i where A[i] ≥ target.
- Upper bound: first index i where A[i] > target.
- First/last occurrence of target.
- Binary search on answer:
  - Over parameter x where `f(x)` is monotonic.

### ⚠ Pitfalls

- Off-by-one errors in loop conditions.
- Using it on unsorted/non-monotonic data.
- Not handling duplicates correctly (first/last occurrence).
- Infinite loops due to `left = mid` and `right = mid` mistakes.

---

## 🧭 Concept Map (ASCII)

```text
                Week 2: Linear Structures & Binary Search
                ------------------------------------------
                          (requires random access)
                         /                             \
                 Arrays & Dynamic Arrays           Binary Search
                  /           \                     (sorted/monotonic)
                 /             \
        Linked Lists       Stacks & Queues
            |                 |
  Node-based, scattered   Policies (LIFO/FIFO)
  memory, O(1) local      built over arrays/lists
  updates, O(n) indexing

Key flows:
- Arrays → Dynamic Arrays → Stacks (array-based)
- Arrays → Binary Search (sorted arrays)
- Linked Lists → Queues (linked), LRU caches
- Stacks/Queues → DFS/BFS in Week 6–7
```

---

## 🎓 Week 2 Milestones

You’re in good shape for Week 2 if you can:

- **Arrays / Dynamic Arrays**
  - [ ] Explain O(1) indexing and O(n) mid-insert/delete.
  - [ ] Explain amortized O(1) append and why grow-by-1 is O(n²).

- **Linked Lists**
  - [ ] Reverse a singly linked list in-place.
  - [ ] Detect a cycle using fast/slow pointers.
  - [ ] Describe why linked lists are worse than arrays for random access.

- **Stacks & Queues**
  - [ ] Implement a stack conceptually with array or list.
  - [ ] Implement a queue with a circular buffer.
  - [ ] Explain where stacks vs queues are used (DFS vs BFS, undo/redo vs schedulers).

- **Binary Search**
  - [ ] Implement basic binary search without off-by-one errors.
  - [ ] Implement at least one variant (first ≥ target or first bad version).
  - [ ] Recognize when to apply binary search on answer.

Use this checklist before moving into Week 3 (Sorting & Hashing).