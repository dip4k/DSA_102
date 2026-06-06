# 🗺 WEEK 2 — CONCEPT MAP & SUMMARY

**Week Theme:** Linear Data Structures & Search  

---

## 📚 Key Concepts (Per Day)

### 📅 Day 1: Arrays
- **Contiguity:** Elements are side-by-side in RAM.
- **Math:** Address = `Base + Index * Size`.
- **Speed:** O(1) Read/Write. O(n) Insert/Delete (shifting).

### 📅 Day 2: Dynamic Arrays
- **Growth:** Start small, double capacity when full.
- **Amortization:** Resizing is expensive O(n), but happens rarely. Avg cost is O(1).
- **Usage:** Default list type in most languages (`List<T>`, `ArrayList`).

### 📅 Day 3: Linked Lists
- **Flexibility:** Nodes scattered in heap, connected by pointers.
- **Trade-off:** Fast insert (if pointer known) vs Slow access (must walk).
- **Variations:** Singly, Doubly, Circular, Sentinel Nodes.

### 📅 Day 4: Stacks & Queues
- **Stack (LIFO):** Top-only access. Used for recursion, parsing, undo.
- **Queue (FIFO):** Front/Back access. Used for buffering, BFS.
- **Implementation:** Stack via Array/List. Queue via List/Circular Buffer.

### 📅 Day 5: Binary Search
- **Divide & Conquer:** Check mid, discard half.
- **Constraint:** Requires sorted data (or monotonic function).
- **Power:** Reduces 1 Billion items to ~30 checks.

---

## 🔗 Concept Relationships

```text
Memory (RAM)
    ↓
Arrays (Fixed Size)
    ↓ (Add resizing logic)
Dynamic Arrays (Flexible Size) ——→ Used to implement → Stacks (LIFO)
    ↓                                                 ↓
    ↓ (Break contiguity)                              ↓
Linked Lists (Nodes + Pointers) —→ Used to implement → Queues (FIFO)
                                                      ↓
                                        Binary Search (Needs Arrays)
```

---

## 📊 Comparison & Relationship Table

| 📌 Structure | ⏱ Access | ⏱ Insert (End) | ⏱ Insert (Mid) | 💾 Locality | ✅ Best For |
|-------------|---------|----------------|----------------|-------------|-------------|
| **Array** | O(1) | N/A (Fixed) | N/A | Excellent | Fixed buffers, matrices. |
| **Dynamic Array** | O(1) | O(1) Amort. | O(n) | Excellent | General lists. |
| **Linked List** | O(n) | O(1) | O(1)* | Poor | Undo history, fragmentation. |
| **Stack** | O(1) (Top) | O(1) | N/A | Good (Array) | LIFO logic. |
| **Queue** | O(1) (Front) | O(1) | N/A | Good (Array) | FIFO logic. |

*\* If pointer is already at location.*

---

## 💡 7 Key Insights from Week 2

1. **Memory Layout Matters:** Arrays are fast because of CPU Caches. Lists are slow because of pointer chasing.
2. **Amortization is Real:** "Expensive occasionally" is fine if "Cheap usually" makes up for it.
3. **Pointers are just Addresses:** A Linked List is just a scavenger hunt through RAM.
4. **LIFO vs FIFO:** The order you process data determines the algorithm (DFS vs BFS).
5. **Binary Search needs Order:** You can't binary search chaos. Sort it first.
6. **Off-By-One Kills:** In Binary Search, be paranoid about `<=` vs `<`.
7. **Sentinel Nodes:** A dummy head node eliminates 50% of edge case bugs in Linked Lists.

---

## ⚠ 5 Common Misconceptions Fixed

1. **"LinkedList is better for inserting."**
   → Reality: Only if you already have the pointer! Finding the spot is O(n).
2. **"Stack is a specific memory region."**
   → Reality: "The Stack" (memory) is different from "Stack" (Data Structure).
3. **"Binary Search mid is `(L+H)/2`."**
   → Reality: That overflows. Use `L + (H-L)/2`.
4. **"Arrays are static in Python."**
   → Reality: Python lists are Dynamic Arrays.
5. **"Doubling array capacity wastes space."**
   → Reality: It wastes *some* space to save massive amounts of CPU time.

---

*End of Week 2 Summary*
