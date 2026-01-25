# 🗺 Week 2 Problem-Solving Roadmap — Linear Structures & Binary Search

This roadmap guides your **practice progression** for Week 2:

- What to practice each day.
- How to move from easy → medium → pattern mastery.
- How to connect topics across days.

---

## 🎯 High-Level Goals

By the end of Week 2, your problem-solving should reflect:

1. **Arrays / Dynamic Arrays**
   - Comfortable with in-place operations and two-pointer patterns.
   - Understand when O(n) vs O(n²) solutions are acceptable.

2. **Linked Lists**
   - Confident with pointer manipulation: reverse, delete, detect cycles.
   - No fear of null checks and boundary cases.

3. **Stacks & Queues**
   - Instantly recognize stack/queue patterns (undo, parentheses, BFS).
   - Implement conceptual stacks/queues on arrays/lists.

4. **Binary Search**
   - Write bug-free binary searches.
   - Recognize and apply binary search on answers.

---

## 🗓 Day-by-Day Plan

### 📅 Day 1: Arrays

**Concept Focus:**
- Contiguous memory, O(1) indexing.
- Insertion/deletion cost.
- Two-pointer and in-place operations.

**Problem Progression:**

1. **Warm-Up (Easy)**
   - Remove Element (LC 27)
   - Remove Duplicates from Sorted Array (LC 26)
2. **Core Practice (Easy/Medium)**
   - Move Zeroes (LC 283)
   - Best Time to Buy and Sell Stock (LC 121)
   - Maximum Subarray (LC 53)
3. **Stretch (Optional)**
   - Product of Array Except Self (LC 238 – Medium)
   - Container With Most Water (LC 11 – Medium, uses 2 pointers)

**Key Skills:**
- In-place array rewriting with a write-pointer.
- Recognizing when O(n) one-pass solution replaces O(n²) naive.

---

### 📅 Day 2: Dynamic Arrays

**Concept Focus:**
- size vs capacity.
- Amortized analysis for push.
- When dynamic arrays are used (Python list, Java `ArrayList`, etc.).

**Problem Progression:**

1. **Implementation / Design**
   - “Design Dynamic Array” (design-style question).
   - Implement Stack using Dynamic Array (conceptually).
2. **Use in Real Problems**
   - Min Stack (LC 155 – Medium): dynamic array often used as backing.
   - Implement Queue using Stacks (LC 232 – Easy) – solidify amortized thinking.
3. **Stretch**
   - Design HashMap (backed by array + chaining) – preview for Week 3.

**Key Skills:**
- Explain amortized O(1) append clearly.
- Understand when to use reserve/pre-allocation.

---

### 📅 Day 3: Linked Lists

**Concept Focus:**
- Singly & doubly lists.
- Pointer manipulation & invariants.
- Fast/slow pointer techniques.

**Problem Progression:**

1. **Warm-Up (Easy)**
   - Reverse Linked List (LC 206)
   - Remove Linked List Elements (LC 203)
2. **Core Practice (Medium)**
   - Remove Nth Node From End of List (LC 19)
   - Linked List Cycle (LC 141)
   - Merge Two Sorted Lists (LC 21)
3. **Stretch**
   - Linked List Cycle II (LC 142)
   - Reorder List (LC 143 – more complex pointer manipulation)

**Key Skills:**
- Reverse a list in-place without losing nodes.
- Use fast/slow pointers to detect cycles or find middle.
- Handle head/tail updates correctly.

---

### 📅 Day 4: Stacks & Queues

**Concept Focus:**
- LIFO / FIFO patterns.
- Expression evaluation, parentheses.
- BFS and level-order traversal.

**Problem Progression:**

1. **Stack Basics (Easy)**
   - Valid Parentheses (LC 20)
   - Min Stack (LC 155, if not done)
2. **Queue & BFS (Easy/Medium)**
   - Implement Queue using Stacks (LC 232 – if not done)
   - Binary Tree Level Order Traversal (LC 102)
3. **Pattern Extensions**
   - Daily Temperatures (LC 739 – monotonic stack, Medium)
   - Sliding Window Maximum (LC 239 – monotonic deque, Hard; optional)

**Key Skills:**
- Map nested structures → stack.
- Map “level by level” / “shortest steps” → queue/BFS.
- Start seeing monotonic stack/queue patterns.

---

### 📅 Day 5: Binary Search

**Concept Focus:**
- Exact match search.
- Bound-finding variants.
- Binary search on answer.

**Problem Progression:**

1. **Core Binary Search (Easy)**
   - Binary Search (LC 704)
   - Search Insert Position (LC 35)
   - First Bad Version (LC 278 – binary search on predicate)
2. **Variants (Medium)**
   - Find First and Last Position of Element (LC 34)
   - Search in Rotated Sorted Array (LC 33)
   - Find Minimum in Rotated Sorted Array (LC 153)
3. **Answer Search (Medium)**
   - Sqrt(x) (LC 69)
   - Koko Eating Bananas (LC 875) or Capacity To Ship Packages (LC 1011)

**Key Skills:**
- Implement correct loop invariants.
- Recognize when to use `left <= right` vs `left < right` styles.
- Understand monotonic predicate design for answer search.

---

## 📈 Difficulty Progression & Volume

### Recommended Volume (per topic):

- Arrays/Dynamic Arrays: 8–12 problems.
- Linked Lists: 8–12 problems.
- Stacks & Queues: 8–12 problems.
- Binary Search & variants: 10–14 problems.

Total: ~34–50 problems for the week (depending on schedule and prior experience).

If time-limited:

- Prioritize:
  - Arrays (Day1) + Linked Lists (Day3).
  - Stacks/Queues (Day4).
  - At least 4–6 solid binary search problems.

---

## 💡 Strategy Tips

### 1. Always Ask “What Is the Underlying Structure?”

Before solving:

- Arrays → think indices, two pointers, binary search.
- Linked lists → think pointers, O(1) local changes, fast/slow pointers.
- Stack → think nested structure, last operation undone first.
- Queue → think BFS, level order, first-come-first-served.
- Binary search → think sorted/monotonic, halving search space.

### 2. Prefer Clear Patterns Over Clever Tricks

For interviews:

- Show that you recognize when a stack, queue, or binary search is appropriate.
- Use well-known templates and explain them clearly.

### 3. Practice Explaining Trade-offs

After each problem, ask:

- Why did this structure fit?
- What would happen if I used a different one (array vs list, stack vs recursion)?
- How does complexity change?

Being able to articulate these answers impresses interviewers.

---

## ✅ Milestone Checks

By end of Week 2, you should be able to:

- Arrays:
  - [ ] Solve in-place array problems without extra arrays.
- Dynamic Arrays:
  - [ ] Explain amortized analysis of append in your own words.
- Linked Lists:
  - [ ] Reverse a list and detect a cycle quickly on a whiteboard.
- Stacks & Queues:
  - [ ] Implement queue using two stacks and describe amortized behavior.
- Binary Search:
  - [ ] Solve at least one “binary search on answer” problem independently.

Use these milestones as your **readiness indicator** before moving to Week 3.

---