# 📘 Week 2 Guidelines — Linear Structures (Arrays → Binary Search)

## 🗓 Week Overview

**Week 2 Theme:** Linear Structures  
**Days:** 1–5  
**Topics:**
- Day 1 – Arrays
- Day 2 – Dynamic Arrays
- Day 3 – Linked Lists
- Day 4 – Stacks & Queues
- Day 5 – Binary Search

This week turns the RAM-model foundations from Week 1 into concrete, everyday data structures. By the end of Week 2, you should be able to:

- Pick the right **linear structure** for a problem (array, list, stack, queue).
- Reason about **time/space trade-offs** of each.
- Implement and use **binary search** and its variants confidently.

---

## 🎯 Week 2 Learning Objectives

By the end of this week, you should be able to:

1. **Arrays & Dynamic Arrays**
   - Explain arrays as **contiguous memory** with O(1) random access.
   - Analyze insertion/deletion costs (O(n)) and cache behavior.
   - Describe how dynamic arrays maintain **size vs capacity** and why `push` is amortized O(1).

2. **Linked Lists**
   - Implement the core operations on **singly and doubly linked lists** at the conceptual level.
   - Reason about **O(1)** insert/delete given a node vs **O(n)** index access.
   - Understand when linked lists are preferable to arrays (LRU cache, free lists, intrusive lists).

3. **Stacks & Queues**
   - Understand LIFO vs FIFO semantics and typical use cases:
     - Stacks: recursion, undo/redo, expression parsing.
     - Queues: BFS, scheduling, message buffers.
   - Distinguish between array-backed and list-backed implementations.
   - Explain circular queues and why they avoid O(n) shifting.

4. **Binary Search**
   - Implement binary search on sorted arrays without off-by-one errors.
   - Use **lower_bound / upper_bound** style variants.
   - Apply **binary search on answer** when a predicate is monotonic.
   - Reason about correctness via loop invariants and O(log n) complexity.

---

## 📚 Conceptual Focus & Approach

### 1. Prioritize Understanding Over Memorization

Don’t treat these structures as “APIs you must memorize.” Instead:

- For each structure, ask:
  - **What memory layout does it use?** (contiguous vs scattered)
  - **What is O(1)?** (which operations are “free”)
  - **What is O(n)?** (which operations are “painful”)
- Connect this back to Week 1:
  - RAM model → array indexing.
  - Space complexity → node overhead vs contiguous storage.
  - Recursion → stack behavior.

### 2. Practice Mental Traces

For each structure/pattern:

- Draw **small diagrams** (5–6 elements).
- Simulate operations step by step:
  - Insert/delete in the middle of an array.
  - Insert/delete nodes in a linked list.
  - Push/pop on a stack.
  - Enqueue/dequeue on a circular queue.
  - Left/right/mid updates in binary search.

The goal is to make pointer movement and index updates **visually obvious** to you.

### 3. Learn to Read Problem Statements for Hints

This week you’ll see:

- Phrases that hint **arrays**:
  - “Given an array…”
  - “In-place…”
  - “Subarray,” “prefix sum,” “sliding window.”
- Phrases that hint **linked lists**:
  - “Given the head of a linked list…”
  - “Nodes with next/prev pointers.”
- Phrases that hint **stack/queue**:
  - “Undo/redo,” “balanced parentheses,” “level-order.”
- Phrases that hint **binary search**:
  - “Sorted…”
  - “Find first/last…”
  - “Minimize/ maximize parameter such that condition holds.”

Practice mapping these hints to structures **before** jumping into code.

---

## 🧠 How to Study Week 2

### Day-Level Strategy

- **Day 1–3 (Arrays, Dynamic Arrays, Linked Lists):**
  - Focus on **memory layout** and basic operations.
  - Contrast arrays vs linked lists relentlessly:
    - O(1) vs O(n) random access.
    - O(n) vs O(1) middle insert/delete (given node).
    - Cache locality vs pointer chasing.
- **Day 4 (Stacks & Queues):**
  - Treat them as **usage patterns** built on arrays/lists.
  - Learn typical applications (DFS/BFS, undo/redo, expression evaluation).
- **Day 5 (Binary Search):**
  - Start with simple exact-match search.
  - Move to **first/last** occurrence, then **binary search on answer**.
  - Practice writing loop invariants and boundary reasoning.

### Recommended Daily Workflow

1. **Concept Pass (30–40 min)**
   - Read the day’s instructional file.
   - Sketch diagrams for each key operation.

2. **Pattern Recognition (10–20 min)**
   - Look at a few problem statements.
   - For each, decide which structure/pattern is most natural and why.

3. **Targeted Practice (60–90 min)**
   - Solve 3–6 problems:
     - Start with 1–2 easy to warm up.
     - Aim for 1–2 mediums connected directly to the day’s topic.

4. **Reflection (10–15 min)**
   - Write down:
     - 1 thing you misunderstood and fixed.
     - 1 pattern you recognized.
     - 1 mistake you won’t make again (e.g., forgetting to update `tail` in linked list).

---

## ⏱ Time Management & Expected Load

Approximate daily time:

- **Reading & notes:** 45–60 minutes.
- **Problems:** 60–90 minutes.
- **Review & reflection:** 10–20 minutes.

Total per day: ~2–3 hours.

If you’re short on time:

- Prioritize:
  - Understanding operations and complexities.
  - A smaller set of **high-yield problems** (e.g., from the weekly roadmap).
- Defer:
  - Some of the harder “extension” problems to a later review.

---

## 🔗 Week 2 in the Bigger Picture

### From Week 1 → Week 2

- Week 1:
  - Built your mental model of **RAM, asymptotic time/space, recursion**.
- Week 2:
  - Instantiates that model:
    - Arrays/dynamic arrays: direct application of RAM & amortized analysis.
    - Linked lists: pointer-heavy structures and space trade-offs.
    - Stacks/queues: concrete manifestations of **call stack** and scheduling.
    - Binary search: your first major **divide-and-conquer** search pattern.

### From Week 2 → Week 3+

- Week 3 (Sorting & Hashing):
  - Many algorithms operate on arrays and dynamic arrays.
  - Hash tables often use arrays + dynamic growth.
- Week 4 & beyond:
  - Stacks/queues appear in DFS/BFS patterns.
  - Linked lists appear in LRU/cache designs.
  - Binary search appears as a subroutine or as “search on answer” in many advanced problems.

---

## ⚔ Practice Strategy for Week 2

### Suggested Problem Mix

Per topic, aim for:

- Arrays & Dynamic Arrays: 6–10 problems
- Linked Lists: 6–10 problems
- Stacks & Queues: 6–10 problems
- Binary Search & Variants: 8–12 problems

Mix:

- 60–70% **easy** and **straightforward medium** problems:
  - Build pattern recognition and confidence.
- 30–40% **core mediums and a few hards**:
  - Push you to combine multiple patterns (e.g., binary search + greedy).

### Focus On:

- Arrays:
  - Two pointers, in-place operations, prefix sums.
- Dynamic arrays:
  - Amortized analysis; understanding when O(1) is **amortized** vs worst-case.
- Linked lists:
  - Reversal, deletion, cycle detection (fast/slow pointers).
- Stacks & queues:
  - BFS, parentheses, monotonic stack intro.
- Binary search:
  - Implement it correctly.
  - Learn to see “sorted/monotonic” cues in problem statements.

---

## ✅ Weekly Checklist (High-Level)

By end of Week 2, you should confidently say “Yes” to:

- [ ] I can explain why array indexing is O(1) and mid-insertion is O(n).
- [ ] I understand how dynamic arrays grow and why `push_back` is amortized O(1).
- [ ] I can conceptually implement a singly linked list and reverse it in O(n) time and O(1) space.
- [ ] I know the trade-offs between array-backed and list-backed stacks/queues.
- [ ] I can implement binary search and at least one variant (lower_bound or binary search on answer) without off-by-one bugs.
- [ ] I can look at a problem and quickly decide which linear structure and pattern is appropriate.

Use this as your **end-of-week self-test**.