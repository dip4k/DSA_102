# ✅ Week 2 Daily Progress Checklist — Linear Structures

Use this as a daily guide. Check items as you complete them.

---

## 📅 Day 1 — Arrays

### 🎯 Learning Goals

- [ ] Understand arrays as contiguous memory with O(1) index access.
- [ ] Explain why insert/delete in the middle is O(n).
- [ ] Perform in-place modifications using two pointers.

### 📚 Concepts to Cover

- [ ] Array memory layout & index → address.
- [ ] Complexity of access vs insertion/deletion.
- [ ] Two-pointer patterns (slow/fast, left/right).
- [ ] Basic in-place transformations (remove, rotate, partition).

### ⚔ Suggested Problems

- [ ] Remove Element (LeetCode 27)
- [ ] Remove Duplicates from Sorted Array (LeetCode 26)
- [ ] Move Zeroes (LeetCode 283)
- [ ] Best Time to Buy and Sell Stock (LeetCode 121)
- [ ] Maximum Subarray (LeetCode 53)

### 🎙 Interview Prompts

- [ ] “Why is array indexing O(1)?”
- [ ] “Show how to remove a value from an array in-place.”
- [ ] “When would you choose an array over a linked list?”

---

## 📅 Day 2 — Dynamic Arrays

### 🎯 Learning Goals

- [ ] Explain `size` vs `capacity`.
- [ ] Describe why `append` is amortized O(1).
- [ ] Understand growth strategies (doubling vs +1).

### 📚 Concepts to Cover

- [ ] Internal structure of dynamic arrays (`array`, `size`, `capacity`).
- [ ] Amortized analysis of `push_back`.
- [ ] Impact of growth factor on performance and memory.
- [ ] Real examples: Python list, Java `ArrayList`, C++ `vector`.

### ⚔ Suggested Tasks / Problems

- [ ] Design a simple dynamic array API (push, pop, get, size).
- [ ] Implement conceptual stack using dynamic array.
- [ ] Read about `std::vector::reserve` and capacity behavior.
- [ ] Min Stack (LeetCode 155) — reinforcing dynamic array + extra stack.

### 🎙 Interview Prompts

- [ ] “Explain amortized analysis for dynamic arrays.”
- [ ] “What happens when a dynamic array runs out of capacity?”
- [ ] “Compare grow-by-1 vs doubling strategies.”

---

## 📅 Day 3 — Linked Lists

### 🎯 Learning Goals

- [ ] Understand singly vs doubly linked list structures.
- [ ] Reverse a singly linked list in O(n) time and O(1) space.
- [ ] Use fast/slow pointers for cycle detection / middle finding.

### 📚 Concepts to Cover

- [ ] Node structure (`value`, `next`, optionally `prev`).
- [ ] O(1) insertion/deletion given node vs O(n) index access.
- [ ] Edge cases: empty list, single element, head/tail operations.
- [ ] Cycle detection (Floyd’s algorithm).

### ⚔ Suggested Problems

- [ ] Reverse Linked List (LeetCode 206)
- [ ] Remove Nth Node From End of List (LeetCode 19)
- [ ] Merge Two Sorted Lists (LeetCode 21)
- [ ] Linked List Cycle (LeetCode 141)
- [ ] Linked List Cycle II (LeetCode 142) — stretch

### 🎙 Interview Prompts

- [ ] “Walk me through in-place reversal of a linked list.”
- [ ] “How do you detect a cycle using O(1) extra space?”
- [ ] “When is a doubly linked list preferable to a singly linked list?”

---

## 📅 Day 4 — Stacks & Queues

### 🎯 Learning Goals

- [ ] Understand LIFO (stack) vs FIFO (queue) semantics.
- [ ] Implement conceptual stack & queue using arrays/lists.
- [ ] Apply stack/queue to typical problems (DFS/BFS, parentheses).

### 📚 Concepts to Cover

- [ ] Stack operations (push, pop, top).
- [ ] Queue operations (enqueue, dequeue, front).
- [ ] Circular queue / ring buffer.
- [ ] Applications: undo/redo, BFS, expression evaluation.

### ⚔ Suggested Problems

- [ ] Valid Parentheses (LeetCode 20)
- [ ] Implement Queue using Stacks (LeetCode 232)
- [ ] Binary Tree Level Order Traversal (LeetCode 102)
- [ ] Min Stack (LeetCode 155) — if not done
- [ ] Daily Temperatures (LeetCode 739) — monotonic stack (stretch)

### 🎙 Interview Prompts

- [ ] “Describe how recursion uses a stack under the hood.”
- [ ] “Why is a circular buffer good for a queue?”
- [ ] “How do you implement a queue using two stacks, and what is the complexity?”

---

## 📅 Day 5 — Binary Search

### 🎯 Learning Goals

- [ ] Implement binary search without off-by-one errors.
- [ ] Understand lower_bound/upper_bound style variants.
- [ ] Apply binary search on answer (monotonic predicates).

### 📚 Concepts to Cover

- [ ] Binary search invariants ([L,R] vs [L,R) styles).
- [ ] Variants: first occurrence, last occurrence, first ≥ target.
- [ ] Rotated sorted arrays concept.
- [ ] Binary search on answer: capacity/time/threshold problems.

### ⚔ Suggested Problems

- [ ] Binary Search (LeetCode 704)
- [ ] Search Insert Position (LeetCode 35)
- [ ] First Bad Version (LeetCode 278)
- [ ] Find First and Last Position of Element (LeetCode 34)
- [ ] Search in Rotated Sorted Array (LeetCode 33)
- [ ] Capacity To Ship Packages Within D Days (LeetCode 1011) — answer search

### 🎙 Interview Prompts

- [ ] “Explain the loop invariant in your binary search implementation.”
- [ ] “How do you implement lower_bound (first ≥ target)?”
- [ ] “What is binary search on answer? Give an example.”

---

## 🧾 End-of-Week Self-Check

By end of Day 5, confirm:

- [ ] I can articulate pros/cons of arrays, dynamic arrays, linked lists.
- [ ] I can choose stack vs queue vs simple array/list for traversal problems.
- [ ] I can implement and adapt binary search patterns confidently.
- [ ] I’ve solved at least:
  - [ ] 6+ array/dynamic array problems.
  - [ ] 6+ linked list problems.
  - [ ] 6+ stack/queue problems.
  - [ ] 8+ binary search problems.

---