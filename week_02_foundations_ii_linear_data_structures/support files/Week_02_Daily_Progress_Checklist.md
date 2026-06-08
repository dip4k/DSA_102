# ✅ Week 02 Daily Progress Checklist: Detailed Execution Plan

**Purpose:** Track daily learning milestones, verify structural mechanics, and maintain steady progress through 6 structured study days.

---

## 📅 MONDAY: Day 1 — Static Arrays & Memory Layout

### Sessions & Deliverables
*   **Study & Read (45 min)**:
    *   [ ] Read Week 02 Day 1: Static Arrays & Memory Layout.
    *   [ ] Key concepts: address offset calculation formula (`Base + Index x Stride`), Row-Major vs. Column-Major mapping, L1/L2 Cache line prefetching (64-byte blocks).
*   **Visualize & Understand (45 min)**:
    *   [ ] Draw a static array block in contiguous memory. Label the byte offsets.
    *   [ ] Trace why index accessing is a fast O(1) mathematical operation.
*   **Code & Benchmark (60 min)**:
    *   [ ] Code a static array address calculator.
    *   [ ] Run a memory access stride benchmark comparing sequential row-major loops vs. vertical column-wise skips.
*   **Review & Verify**:
    *   [ ] Did you verify why sequential stride is 10-100x faster than vertical or random access? ☐ Yes

---

## 📅 TUESDAY: Day 2 — Dynamic Arrays & Amortized Growth

### Sessions & Deliverables
*   **Study & Read (45 min)**:
    *   [ ] Read Week 02 Day 2: Dynamic Arrays & Amortized Growth.
    *   [ ] Key concepts: logical size vs. physical capacity, reallocation costs, and geometric doubling strategy.
*   **Visualize & Understand (45 min)**:
    *   [ ] Draw a dynamic array resizing sequence (sizes 1 -> 2 -> 4 -> 8).
    *   [ ] Total work for N elements: show why the sum of doubling copies equals a geometric series (2N - 1 = O(N)).
*   **Code & Implement (60 min)**:
    *   [ ] Implement a custom Dynamic Array class (`Push`, `Get`, `Resize` methods).
    *   [ ] Implement pop options with lazy shrinking (shrink when length drops below capacity/4).
*   **Review & Verify**:
    *   [ ] Can you explain the difference between amortized cost vs. worst-case individual reallocation? ☐ Yes

---

## 📅 WEDNESDAY: Day 3 — Linked Lists

### Sessions & Deliverables
*   **Study & Read (45 min)**:
    *   [ ] Read Week 02 Day 3: Linked Lists.
    *   [ ] Key concepts: Singly Linked Lists (SLL) vs. Doubly Linked Lists (DLL), pointer manipulation, and cache locality trade-offs.
*   **Visualize & Understand (45 min)**:
    *   [ ] Draw a singly linked list reversal on paper.
    *   [ ] Visual check: show prev, current, and next pointer re-wirings step-by-step.
*   **Code & Implement (60 min)**:
    *   [ ] Implement singly and doubly linked list node operations.
    *   [ ] Implement iterative linked list reversal ($O(N)$ time, $O(1)$ space).
    *   [ ] Implement middle node search using slow/fast pointers.
*   **Review & Verify**:
    *   [ ] Are you saving adjacent references before updating pointer links to prevent lost nodes? ☐ Yes

---

## 📅 THURSDAY: Day 4 — Stacks, Queues & Deques

### Sessions & Deliverables
*   **Study & Read (45 min)**:
    *   [ ] Read Week 02 Day 4: Stacks, Queues & Deques.
    *   [ ] Key concepts: LIFO vs. FIFO semantics, expression parsing patterns, and circular queue optimization.
*   **Visualize & Understand (45 min)**:
    *   [ ] Draw a circular buffer queue. Show how head/tail pointers wrap around sequentially.
    *   [ ] Trace the state of a parentheses matching stack on the string "({[]})".
*   **Code & Implement (60 min)**:
    *   [ ] Implement an array-backed circular queue with wraparound indices.
    *   [ ] Solve valid parentheses matching using a stack.
*   **Review & Verify**:
    *   [ ] Can you distinguish how a full circular queue is differenced from an empty circular queue? ☐ Yes

---

## 📅 FRIDAY: Day 5 — Binary Search & Invariants

### Sessions & Deliverables
*   **Study & Read (45 min)**:
    *   [ ] Read Week 02 Day 5: Binary Search & Invariants.
    *   [ ] Key concepts: high-order index overflow protection, bounds mapping (lower/upper bound), and answer space optimization.
*   **Visualize & Understand (45 min)**:
    *   [ ] Draw first and last occurrence searches on a sorted duplicate sequence like [1, 2, 2, 2, 3].
    *   [ ] Concept check: trace answer-space capacity sifting on paper.
*   **Code & Implement (60 min)**:
    *   [ ] Implement binary search first/last occurrences, lower bounds, and upper bounds.
    *   [ ] Solve a binary search on answer space problem (e.g. Split Array Largest Sum).
*   **Review & Verify**:
    *   [ ] Does your midpoint index check avoid high overflow (using `low + (high - low) / 2`)? ☐ Yes

---

## 📅 SATURDAY: Day 6 — Strings, Numbers & Conversions

### Sessions & Deliverables
*   **Study & Read (45 min)**:
    *   [ ] Read Week 02 Day 6: Strings & Numbers.
    *   [ ] Key concepts: UTF-16 surrogate pairs, string immutability copies, signed integer Two's complement representation, and conversion mechanisms.
*   **Visualize & Understand (45 min)**:
    *   [ ] Sketch how string immutability creates intermediate garbage overhead in a loop.
    *   [ ] Trace positive/negative integer boundaries across 8-bit limits.
*   **Code & Implement (60 min)**:
    *   [ ] Implement string-to-integer conversion (`atoi`) with whitespace skipping, sign checking, and overflow clamping.
    *   [ ] Implement integer-to-string conversion (`itoa`) with proper handling of zero and negative values.
*   **Review & Verify**:
    *   [ ] Does your conversion code explicitly clamp overflows before multiplying by 10 and adding? ☐ Yes

---

## 🏁 WEEKLY MASTERY VERIFICATION

By the end of Week 02, verify:
*   [ ] You can explain static address calculators and row-major cache lines.
*   [ ] You can implement dynamic array doubling and lazy shrinking (length <= capacity/4).
*   [ ] You can reverse a singly linked list in-place and find the middle node using slow/fast pointers.
*   [ ] You can implement a circular queue using modular indices without index drift.
*   [ ] You can implement binary search, lower bounds, upper bounds, and answer space.
*   [ ] You can implement `atoi` and `itoa` with complete overflow protections.
*   [ ] You can explain why String builders are linear O(N) when repeated string concatenations are quadratic O(N^2).
