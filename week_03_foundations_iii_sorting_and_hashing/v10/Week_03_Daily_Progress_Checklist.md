# ✅ Week 03 Daily Progress Checklist

**Theme:** Sorting Internals, Heaps, and Hashing Mechanics.  
**Goal:** Build strong mental models of data movement and collision handling.

---

## 📅 Day 1: Elementary Sorts
**Focus:** Mechanics of Bubble, Selection, Insertion.

- [ ] **Read:** Week 03 Day 1 Instructional File.
- [ ] **Visual Trace:** Draw `[5, 1, 4, 2, 8]` and trace **Insertion Sort** step-by-step on paper.
- [ ] **Concept Check:** Explain why Insertion Sort is fast for nearly sorted data.
- [ ] **Code:** Implement Bubble, Selection, and Insertion sort from scratch.
- [ ] **Reflection:** Why is stability important? (Example: Sorting Excel columns).

---

## 📅 Day 2: Merge & Quick Sort
**Focus:** Divide & Conquer, Partitioning, Recursion.

- [ ] **Read:** Week 03 Day 2 Instructional File.
- [ ] **Visual Trace:** Draw the recursion tree for **Merge Sort** on 8 elements.
- [ ] **Visual Trace:** Trace **Lomuto Partitioning** on `[3, 8, 2, 5, 1, 4, 7, 6]`.
- [ ] **Code:** Implement Merge Sort.
- [ ] **Code:** Implement Quick Sort (choose one partition scheme).
- [ ] **Problem:** Solve "Sort an Array" (LeetCode 912) using your implementations.

---

## 📅 Day 3: Heaps & Heap Sort
**Focus:** Tree-in-Array, Sifting, Priority Queues.

- [ ] **Read:** Week 03 Day 3 Instructional File.
- [ ] **Visual Trace:** Draw a Max-Heap array. Simulate `insert(10)` (sift up) and `extract-max()` (sift down).
- [ ] **Concept Check:** Explain mapping `index` to `left_child`, `right_child`, `parent`.
- [ ] **Code:** Implement a basic Priority Queue.
- [ ] **Problem:** "Kth Largest Element in an Array" (Try Heap approach).

---

## 📅 Day 4: Hashing I (Separate Chaining)
**Focus:** Hash Functions, Buckets, Load Factor.

- [ ] **Read:** Week 03 Day 4 Instructional File.
- [ ] **Visual Trace:** Draw a Hash Table (size 5). Insert keys `10, 15, 20` (all collide). Show the linked list.
- [ ] **Concept Check:** What is the load factor $\alpha$? When do we resize?
- [ ] **Code:** Implement a simple `HashTable` class using Chaining.
- [ ] **Problem:** "Design HashMap" (LeetCode 706).

---

## 📅 Day 5: Hashing II (Open Addressing)
**Focus:** Probing, Clustering, Advanced Hashing.

- [ ] **Read:** Week 03 Day 5 Instructional File.
- [ ] **Visual Trace:** Draw a Hash Table. Insert colliding keys using **Linear Probing**. Show where they land.
- [ ] **Visual Trace:** Delete a key in the middle of a probe sequence. Mark the **Tombstone**.
- [ ] **Deep Dive:** Read about Python's Dictionary implementation (Open Addressing).
- [ ] **Reflection:** Compare Chaining vs. Open Addressing (Cache, Deletion, Space).

---

## 🔁 Weekly Review & Integration
- [ ] **System Design:** How would you shard a massive database? (Consistent Hashing concept).
- [ ] **Comparison:** Fill out the Sorting/Hashing comparison tables from memory.
- [ ] **Mock Interview:** Explain to a rubber duck why Quick Sort is usually preferred over Heap Sort.
- [ ] **Cleanup:** Organize your notes and diagrams for the week.

**Status:** ⬜ Not Started | 🟦 In Progress | ✅ Complete