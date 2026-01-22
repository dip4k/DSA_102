# 🗺 Week 03 Problem Solving Roadmap — Sorting & Hashing

**Goal:** Move from "implementing algorithms" to "using them as tools" to solve complex problems.

---

## 🚦 Stage 1: The Mechanics (Foundation)
*Focus: Implementing the algorithms from scratch to understand internals.*

1.  **Implement Bubble, Insertion, Selection Sort**
    *   *Why:* Feel the $O(n^2)$ pain. Understand shifting vs swapping.
2.  **Implement Merge Sort & Quick Sort**
    *   *Why:* Grasp recursion, partitioning (Lomuto/Hoare), and merging mechanics.
3.  **Design a Hash Map** (LeetCode 706)
    *   *Why:* Handle collisions manually (chaining or probing). Understand key mapping.
4.  **Implement a Min-Heap**
    *   *Why:* Array-based tree math ($2i+1$), sift-up/sift-down logic.

---

## 🚦 Stage 2: Standard Applications (Patterns)
*Focus: Recognizing where sorting/hashing simplifies the problem.*

### 🧩 Sorting Patterns
5.  **Merge Intervals** (LeetCode 56)
    *   *Pattern:* Sort by start time → iterate linearly.
    *   *Insight:* Sorting converts a complex geometry problem into a linear pass.
6.  **Sort Colors** (Dutch National Flag) (LeetCode 75)
    *   *Pattern:* 3-way Partitioning (like Quick Sort internals).
    *   *Insight:* One pass, $O(1)$ space.
7.  **Kth Largest Element** (LeetCode 215)
    *   *Pattern:* QuickSelect (partitioning) or Min-Heap.
    *   *Insight:* You don't need to sort the full array.

### 🧩 Hashing Patterns
8.  **Two Sum** (LeetCode 1)
    *   *Pattern:* `Map<Value, Index>`. Check `Target - Current`.
    *   *Insight:* Trade space ($O(n)$) for time ($O(n)$ vs $O(n^2)$).
9.  **Group Anagrams** (LeetCode 49)
    *   *Pattern:* Canonical Key (sorted string) $\to$ List of words.
    *   *Insight:* Hashing complex keys.
10. **Longest Consecutive Sequence** (LeetCode 128)
    *   *Pattern:* `HashSet`. Check neighbors ($x-1$, $x+1$).
    *   *Insight:* $O(1)$ lookups enable $O(n)$ global logic on unsorted data.

---

## 🚦 Stage 3: Advanced & Hybrid (The "Aha!" Moment)
*Focus: Combining concepts and optimizing constraints.*

11. **LRU Cache** (LeetCode 146)
    *   *Concepts:* Hash Map + Doubly Linked List.
    *   *Why:* $O(1)$ access *and* $O(1)$ ordered updates.
12. **Top K Frequent Elements** (LeetCode 347)
    *   *Concepts:* Hash Map (count) + Heap (selection) OR Bucket Sort.
    *   *Why:* Compare Heap $O(N \log K)$ vs Bucket Sort $O(N)$.
13. **Insert Delete GetRandom O(1)** (LeetCode 380)
    *   *Concepts:* Hash Map + Dynamic Array (swap-to-delete).
    *   *Why:* Array for random access, Map for index lookup.
14. **Sort List** (LeetCode 148)
    *   *Concepts:* Merge Sort on Linked List.
    *   *Why:* No random access? Quick sort is hard; Merge sort shines.

---

## 🚧 Common Pitfalls & Anti-Patterns

- **⚠️ Using `O(n log n)` sort when `O(n)` counting/bucket sort works.**
    - *Check:* Is the range of values small/finite?
- **⚠️ Forgetting custom object hashing.**
    - *Check:* If using a custom class as a Key, did you override `equals()` AND `hashCode()`?
- **⚠️ Modifying keys while they are in the table.**
    - *Result:* The "missing key" bug.
- **⚠️ Worst-case Quick Sort on sorted data.**
    - *Fix:* Always randomize pivot or shuffle input.
- **⚠️ Ignoring space complexity in recursion.**
    - *Check:* Quick Sort is $O(\log n)$ space, not $O(1)$.

---

## 🎓 Evaluation Criteria

- **Basic:** Can implement standard sorts and use a built-in HashMap/HashSet.
- **Intermediate:** Can recognize "Sort first to solve" patterns (Intervals, Anagrams).
- **Advanced:** Can implement LRU Cache, Custom HashMaps, and discuss Timsort/Introsort trade-offs.