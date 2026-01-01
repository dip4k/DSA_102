# 🎯 WEEK 2 — GUIDELINES & LEARNING STRATEGY

**Week Theme:** Linear Data Structures & Search  
**Status:** Core Building Blocks  
**Prerequisites:** Week 1 (Foundations, Memory Model)

---

## 📚 Week Overview

Week 2 transitions from the theoretical RAM model to the concrete tools you will use every day: **Arrays, Linked Lists, Stacks, Queues, and Binary Search**.

**Core Philosophy:**
- **Contiguity vs. Flexibility:** Understand the physical trade-off between contiguous arrays (fast access, rigid) and scattered linked lists (slow access, flexible).
- **Invariants:** Learn to respect the rules of each structure (LIFO for Stack, Sorted for Binary Search).
- **Mechanics:** Stop guessing indices. Learn the precise pointer movements.

**Connection to Previous Week:** Week 1 taught you that memory is an array. Week 2 shows you how to organize that memory into useful shapes.  
**Connection to Next Week:** These structures are the nodes and edges you will use to build Trees and Graphs later.

---

## 🎓 Weekly Learning Objectives

By the end of Week 2, you will be able to:

1. **Implement Dynamic Arrays** and explain amortized O(1) growth.
2. **Manipulate Pointers** safely to reverse lists and detect cycles without crashing.
3. **Visualize Stacks & Queues** and choose the right one for DFS/BFS or buffering.
4. **Master Binary Search** invariants (`low <= high`) to eliminate off-by-one errors.
5. **Analyze Locality:** Explain why iterating an Array is faster than a Linked List.

---

## 📋 Key Concepts Overview

| Day | 🎯 Core Topic | 💡 One-Line Essence |
|-----|--------------|---------------------|
| 1 | **Arrays** | Contiguous memory providing O(1) random access. |
| 2 | **Dynamic Arrays** | Arrays that grow automatically (Amortized O(1)). |
| 3 | **Linked Lists** | Flexible chains of nodes; O(1) insert but O(n) access. |
| 4 | **Stacks & Queues** | Restricting access (LIFO/FIFO) to enforce order. |
| 5 | **Binary Search** | Discarding half the search space at every step. |

---

## 🧭 Learning Approach & Methodology

### 🔄 Daily Study Flow (2-3 hours per day)

**Phase 1: Visualization (45 min)**
- Draw the memory layout.
- For Lists: Draw the pointers.
- For Stacks/Queues: Draw the "entry/exit" points.
- *Don't write code until you can draw the state change.*

**Phase 2: Mechanical Tracing (45 min)**
- Manually trace `Insert` into a Dynamic Array (triggering a resize).
- Manually trace `Reverse List` pointer swaps.
- Manually trace `Binary Search` on a 5-element array.

**Phase 3: Pattern Matching (60 min)**
- Practice recognizing: "This needs a Stack" (Parsing) vs "This needs a Queue" (Buffering).
- Solve 2-3 basic problems (e.g., Reverse List, Valid Parentheses).

**Phase 4: Reflection (15 min)**
- Why did I get an Off-By-One error?
- Did I forget to check for `null`?

---

## 🧱 Common Mistakes & Pitfalls

### ❌ Pitfall 1: Confusing Array vs List
- **Mistake:** "I'll use a Linked List because I insert often."
- **Reality:** If you insert at the *end*, Dynamic Array is faster (cache locality). Linked List is only faster for *front/middle* inserts.
- **Fix:** Default to `List<T>` (Dynamic Array) unless you have a specific reason not to.

### ❌ Pitfall 2: The "Lost Head" (Linked Lists)
- **Mistake:** `Head = Head.Next` without saving the old head (if needed) or checking for null.
- **Fix:** Always check `if (Head == null)` first.

### ❌ Pitfall 3: Stack Overflow (Recursion)
- **Mistake:** Using recursion for deep stacks (10,000+ items).
- **Fix:** Use an explicit `Stack<T>` class for deep traversals.

### ❌ Pitfall 4: Binary Search "Mid" Bug
- **Mistake:** `mid = (low + high) / 2`.
- **Reality:** Overflows for large integers.
- **Fix:** Always use `low + (high - low) / 2`.

---

## 🕒 Time & Practice Strategy

### ⏰ Suggested Daily Schedule

| Activity | Time | Purpose |
|----------|------|---------|
| Concept Reading | 45 min | Understand the "Shape" |
| Diagramming | 30 min | Visualize Pointers/Indices |
| Coding Basics | 60 min | Implement from scratch (once) |
| LeetCode Easy | 45 min | Apply to simple problems |

### 🔁 Integration
- **Weekend:** Re-implement a Linked List and Binary Search from memory. If you stumble, re-read that day.

---

## ✅ Weekly Checklist

### 📖 Conceptual Understanding
- [ ] I can explain why Array access is O(1).
- [ ] I understand "Amortized" complexity for resizing.
- [ ] I can verify if a Binary Search implementation handles Empty Arrays correctly.
- [ ] I know when to use a Stack vs a Queue.

### 🧪 Practical Skills
- [ ] I have implemented a Dynamic Array (resize logic).
- [ ] I have reversed a Linked List on a whiteboard.
- [ ] I have written Binary Search without looking at a template.

### 🎙 Interview Readiness
- [ ] I can explain the difference between `Array`, `ArrayList`, and `LinkedList`.
- [ ] I can analyze the space complexity of Recursive Binary Search.

---

*End of Week 2 Guidelines*
