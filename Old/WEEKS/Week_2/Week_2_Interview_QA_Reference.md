# 🎙️ Week 2 — Interview Q&A Reference (C# Focus)

**🗓️ Week:** 2  
**📌 Topic:** Linear Structures (Arrays, Lists, Stacks, Queues)  
**🎯 Usage:** Review these before interviews to ensure deep understanding of C# implementation details and theoretical concepts.

---

## 🧠 CONCEPTUAL & THEORETICAL QUESTIONS

### 1. **Array vs Linked List**
**Q: "Why would you choose a Linked List over an Array? Conversely, why Array over Linked List?"**

**A:**
- **Choose Array (`List<T>`) when:**
  - You need **Random Access** ($O(1)$) by index.
  - Memory efficiency is critical (no pointer overhead).
  - Cache locality matters (iterating arrays is much faster due to CPU prefetching).
  - You primarily add/remove from the **end** (Amortized $O(1)$).

- **Choose Linked List (`LinkedList<T>`) when:**
  - You need constant time **Insertions/Deletions** in the middle or beginning ($O(1)$), *provided* you already have a reference to the node.
  - You need to split or merge lists frequently (O(1) pointer swap).
  - You cannot afford the occasional $O(n)$ resize latency of dynamic arrays (e.g., low-latency systems).
  - Memory fragmentation is not a concern, but contiguous memory blocks are unavailable.

---

### 2. **Dynamic Array Growth**
**Q: "How does a dynamic array (like C# `List<T>`) grow? What is the complexity?"**

**A:**
- **Mechanism:** When the internal array is full (`Count == Capacity`), the list allocates a **new array** with double the size (Capacity * 2). It copies all elements from old to new, then discards the old array.
- **Complexity:**
  - **Single Operation:** Worst case $O(n)$ (when resizing).
  - **Amortized:** Average case $O(1)$.
- **Math:** The sum of resizing costs ($1 + 2 + 4 + ... + n$) is bounded by $2n$, meaning total work for $n$ elements is $O(n)$, making per-element cost $O(1)$.

---

### 3. **Stack vs Queue**
**Q: "Explain the difference between Stack and Queue and give a real-world example for each."**

**A:**
- **Stack (LIFO):** Last In, First Out.
  - **Analogy:** Stack of plates.
  - **Use Case:** Undo function in editors, Browser Back button, Function Call Stack (Recursion).
- **Queue (FIFO):** First In, First Out.
  - **Analogy:** Line at a grocery store.
  - **Use Case:** Printer job spooling, Web server request handling, BFS traversal.

---

## 💻 C# SPECIFIC QUESTIONS

### 4. **`ArrayList` vs `List<T>`**
**Q: "What is the difference between `ArrayList` and `List<T>` in C#? Which one should you use?"**

**A:**
- **`ArrayList`:**
  - Legacy collection (Non-generic). Stores everything as `object`.
  - **Issues:** Requires **Boxing/Unboxing** for value types (performance hit) and lacks type safety (runtime errors).
- **`List<T>`:**
  - Generic collection. Strongly typed.
  - **Benefits:** No boxing for structs/ints. Compile-time type safety. Faster.
- **Verdict:** **Always use `List<T>`**. `ArrayList` is deprecated for new development.

---

### 5. **`LinkedList<T>` Implementation**
**Q: "Is C# `LinkedList<T>` singly or doubly linked? What are the implications?"**

**A:**
- It is a **Doubly Linked List**.
- **Nodes:** `LinkedListNode<T>` contains `Value`, `Next`, `Previous`, and `List` (ref to container).
- **Implication:**
  - Removing a node is $O(1)$ because the node knows its neighbors.
  - Memory overhead is higher (2 references per node + object header).

---

### 6. **`Stack<T>` and `Queue<T>` Internal Storage**
**Q: "How are `Stack<T>` and `Queue<T>` implemented internally in .NET? Linked List or Array?"**

**A:**
- Both are backed by **Arrays (`T[]`)**.
- **Stack:** Pushes to index `size`, pops from `size-1`. Resizes like `List<T>`.
- **Queue:** Uses a **Circular Buffer** logic (Head and Tail indices wrap around the array). This avoids shifting elements on Dequeue.
- **Why Array?** Better cache locality and less GC pressure (fewer allocations) compared to a linked-list implementation.

---

### 7. **Binary Search in C#**
**Q: "How do you perform a binary search in C#? What does it return if the item isn't found?"**

**A:**
- **Method:** `Array.BinarySearch(arr, value)` or `list.BinarySearch(value)`.
- **Precondition:** Collection **MUST** be sorted.
- **Return Value:**
  - If found: Index of the item (>= 0).
  - If NOT found: Bitwise complement of the index of the **next larger element** (`~index`).
  - Example: Searching for 5 in `[1, 3, 7]`. 5 belongs at index 2. Returns `~2` (which is `-3`).
- **Why useful?** This allows you to know exactly where to insert the element to maintain sort order.

---

## 🧪 CODING & ALGORITHMIC SCENARIOS

### 8. **Cycle Detection**
**Q: "How do you detect a cycle in a Linked List?"**

**A:**
- **Floyd’s Cycle-Finding Algorithm (Tortoise and Hare).**
- Initialize two pointers, `Slow` and `Fast`.
- `Slow` moves 1 step; `Fast` moves 2 steps.
- If `Fast` reaches null, no cycle.
- If `Fast` equals `Slow`, a cycle exists.
- **Time:** $O(n)$, **Space:** $O(1)$.

### 9. **Queue using Stacks**
**Q: "How would you implement a Queue using two Stacks?"**

**A:**
- Maintain `StackInput` and `StackOutput`.
- **Enqueue:** Push onto `StackInput`.
- **Dequeue:**
  - If `StackOutput` is empty, pop ALL elements from `StackInput` and push them to `StackOutput` (this reverses the order, making LIFO into FIFO).
  - Pop from `StackOutput`.
- **Amortized Complexity:** Each element is pushed twice and popped twice total. Average $O(1)$ per operation.

### 10. **Memory limit in Recursion**
**Q: "What happens if you push too many items onto the Call Stack?"**

**A:**
- **StackOverflowException**.
- **Cause:** Infinite recursion or deep recursion exceeding stack limit (usually 1MB default on Windows).
- **Fix:** Convert recursive algorithm to an **Iterative** one using a `Stack<T>` object on the Heap, which has much more space available than the thread stack.

---

## 🏛️ BEHAVIORAL / SYSTEM DESIGN

### 11. **Choosing Collections**
**Q: "In a high-frequency trading application, would you use `List<Order>` or `LinkedList<Order>` for an order book?"**

**A:**
- Ideally, **neither** directly.
- `List<Order>` is bad for inserting/removing orders in the middle (O(n) shift is too slow).
- `LinkedList<Order>` has bad cache locality (latency spikes).
- **Better Choice:** Often a specialized structure like a fixed-size circular buffer (if volume is bounded) or a standard `List<T>` with "lazy deletion" (marking as deleted instead of removing) to avoid shifts. Or specialized collections like `PriorityQueue`.

---

**Generated:** December 30, 2025
**Context:** C# Focused Interview Prep
