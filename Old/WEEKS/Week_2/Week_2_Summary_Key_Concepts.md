# 📝 Week 2 — Summary & Key Concepts: Linear Structures

**🗓️ Week:** 2  
**📌 Theme:** Linear Data Structures (Arrays, Lists, Stacks, Queues, Binary Search)  
**🎯 Goal:** Master the fundamentals of sequential data storage, access patterns, and search.

---

## 🧠 CORE CONCEPTS CHEAT SHEET

### 1. Arrays & Dynamic Arrays
- **Static Array (`int[]`):** Fixed size, contiguous memory.
  - **Access:** $O(1)$ via address arithmetic: `base + index * size`.
  - **Insert/Delete (Middle):** $O(n)$ due to shifting elements.
  - **Cache:** Excellent locality (prefetching friendly).
- **Dynamic Array (`List<T>`):** Resizable array.
  - **Growth:** Doubles capacity when full (Amortized $O(1)$ append).
  - **Capacity vs Count:** `Capacity` is allocated memory; `Count` is used slots.
  - **Waste:** Up to 50% memory waste to ensure $O(1)$ amortized growth.

### 2. Linked Lists
- **Structure:** Nodes with pointers (`Next`, `Prev`) scattered in heap.
- **Singly (`LinkedListNode<T>` in custom implementation):** Forward only.
- **Doubly (`LinkedList<T>`):** Forward and Backward.
- **Trade-offs:**
  - ✅ $O(1)$ Insert/Delete **IF** you have the reference.
  - ❌ $O(n)$ Access (must traverse).
  - ❌ Poor cache locality (pointer chasing).
  - ❌ Extra memory overhead (16-24 bytes per node object).

### 3. Stacks & Queues (ADTs)
- **Stack (LIFO):** "Last In, First Out".
  - **Implementation:** `Stack<T>` (Array-based).
  - **Use Cases:** Recursion, Undo/Redo, DFS, Parsing.
- **Queue (FIFO):** "First In, First Out".
  - **Implementation:** `Queue<T>` (Circular Array).
  - **Use Cases:** BFS, Job Scheduling, Buffering.
- **Priority Queue:** Sorted by priority (Heap-based, covered Week 5).

### 4. Binary Search
- **Requirement:** **Sorted** collection.
- **Algorithm:** Divide search space in half.
- **Complexity:** $O(\log n)$.
- **Key Formula:** `mid = lo + (hi - lo) / 2` (Prevents overflow).
- **Variants:** Lower Bound (first `>=`), Upper Bound (first `>`).

---

## 📊 COMPLEXITY COMPARISON

| Structure | Access (Index) | Search (Value) | Insert (Head) | Insert (Tail) | Insert (Mid) |
|---|---|---|---|---|---|
| **Array** | $O(1)$ | $O(n)$ | $O(n)$ | $O(1)$ | $O(n)$ |
| **List<T>** | $O(1)$ | $O(n)$ | $O(n)$ | $O(1)^*$ | $O(n)$ |
| **LinkedList<T>** | $O(n)$ | $O(n)$ | $O(1)$ | $O(1)$ | $O(1)^{\dagger}$ |
| **Stack<T>** | N/A | N/A | $O(1)$ (Push) | N/A | N/A |
| **Queue<T>** | N/A | N/A | N/A | $O(1)$ (Enq) | N/A |

$^*$ Amortized complexity.  
$^{\dagger}$ Assuming we have a pointer to the location.

---

## 🛠️ C# IMPLEMENTATION DETAILS

### **Arrays & Lists**
- **Array:** `int[] arr = new int[5];`
  - Fixed size. Allocated on Heap.
- **List:** `List<int> list = new List<int>();`
  - Internally uses `int[]`.
  - `list.Add(x)` checks `if (size == capacity) EnsureCapacity(size + 1);`

### **Linked List**
- C# `LinkedList<T>` is a **Doubly Linked List**.
- It is **circular** in some internal versions but exposed as linear.
- **Note:** `LinkedList<T>` does NOT support O(1) indexing (`list[i]` is invalid).

### **Stack & Queue**
- `Stack<T>` and `Queue<T>` are backed by **Arrays** (Circular buffer for Queue).
- They automatically resize (double capacity) when full.

---

## 🧠 CRITICAL MENTAL MODELS

### 1. The "Locality" Trade-off
- **Arrays** win on speed (CPU Cache) because data is neighbors.
- **Linked Lists** win on structural flexibility but lose on speed (Cache Misses).
- **Rule of Thumb:** Use `List<T>` (Array) by default. Use `LinkedList<T>` only when you explicitly need $O(1)$ splicing/removal from the middle without shifting.

### 2. The "Amortized" Concept
- Paying \$100 once is better than paying \$1 every time, IF you do it rarely enough.
- Doubling array capacity is expensive ($O(n)$), but happens so rarely that the average cost is constant ($O(1)$).

### 3. The "LIFO vs FIFO" Pattern
- **LIFO (Stack):** Processing nested things (brackets, function calls).
- **FIFO (Queue):** Processing independent things in order (requests, jobs).

### 4. The "Divide & Conquer" Pattern
- Binary Search is the simplest Divide & Conquer.
- If you can discard half the possibilities with one check, you have an $O(\log n)$ algorithm.

---

## ⚠️ COMMON PITFALLS

1. **Index Out of Range:**
   - Accessing `arr[arr.Length]` or `arr[-1]`.
   - C# throws `IndexOutOfRangeException`.

2. **Modifying Collection while Iterating:**
   - `foreach (var x in list) { list.Add(y); }` throws `InvalidOperationException`.
   - **Fix:** Use a `for` loop or iterate a copy (`list.ToList()`).

3. **Binary Search Overflow:**
   - `mid = (low + high) / 2` overflows for large integers.
   - **Fix:** `mid = low + (high - low) / 2`.

4. **Reference vs Value Types:**
   - `struct` (Value Type) in `List<T>` is stored inline (efficient).
   - `class` (Reference Type) in `List<T>` stores pointers (extra indirection).

---

## 🔗 CONNECTIONS TO FUTURE WEEKS

- **Hash Tables (Week 3):** Use Arrays (buckets) and Linked Lists (chaining) internally.
- **Trees (Week 5):** A Tree is just a Linked List where each node has 2 pointers (Left/Right).
- **Graphs (Week 6):** An Adjacency List is just an Array of Linked Lists (or Lists).
- **Heaps (Week 5):** Implemented using a Dynamic Array!

---

**Generated:** December 30, 2025
**Context:** C# / No-Code Focus
