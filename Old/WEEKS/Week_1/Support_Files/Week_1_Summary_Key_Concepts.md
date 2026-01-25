# 📌 WEEK 1: SUMMARY & KEY CONCEPTS REFERENCE

## 🎯 Key Concepts in Bullet Format

### **Day 1: RAM Model & Pointers**

* **Von Neumann Architecture:** CPU and Memory are separate. The "Bus" connecting them is the bottleneck.
* **L1/L2/L3 Cache:** Hardware caches that store recently used data to avoid slow RAM access.
* **Cache Line (64 Bytes):** The minimum unit of data transfer. Fetching 1 byte actually fetches 64 neighbors.
* **Spatial Locality:** Arrays are fast because they fit in cache lines.
* **Temporal Locality:** Stack variables are fast because they are used repeatedly.
* **Pointer:** A variable storing a memory address (8 bytes on 64-bit OS).

### **Day 2: Asymptotic Analysis**

* **Big-O ():** Upper Bound. Worst-case scenario. Used for guarantees.
* **Big-Omega ():** Lower Bound. Best-case scenario.
* **Big-Theta ():** Tight Bound. The function is squeezed between constants.
* **Master Theorem:** Formula to solve recurrences like .
* **Dominant Term:** As , only the highest power matters ().

### **Day 3: Space Complexity**

* **Auxiliary Space:** Extra space used by algorithm (e.g., temp array).
* **Total Space:** Auxiliary + Input Size.
* **Call Stack Space:**  of recursion.
* **Memory Overhead:** Objects (like Java Integers) have metadata overhead compared to primitives.

### **Day 4: Recursion I (Foundations)**

* **Base Case:** The condition that stops recursion.
* **Recursive Step:** The logic that reduces the problem size.
* **Winding/Unwinding:** The stack grows as we go deeper (winding) and shrinks as we return (unwinding).
* **Inductive Proof:** Recursion is valid if Base Case is true and  holds.

### **Day 5: Recursion II (Advanced)**

* **Multiple Recursion:** Calling the function  times (e.g., Fibonacci). Leads to exponential time .
* **Tail Recursion:** Recursion where the call is the *very last* operation.
* **Accumulator:** A parameter passed down to hold state in tail recursion.
* **Memoization:** Caching results of sub-problems to optimize overlapping recursion.

## 💡 Core Ideas (One-Liners)

1. **RAM:** "Memory is a street, Cache is your backpack, Disk is a warehouse."
2. **Pointers:** "Addresses, not values. Maps, not destinations."
3. **Big-O:** "The shape of the curve, not the height of the graph."
4. **Space:** "The rent you pay for the room to do your work."
5. **Recursion:** "Trusting the copy of yourself to solve the smaller problem."

## 🔗 Concept Relationships

* **Hardware ↔ Math:** Cache Lines (Hardware) explain why  Array is faster than  Linked List (Math).
* **Recursion ↔ Space:** Deep recursion depth directly translates to high Space Complexity ( stack).
* **Tail Recursion ↔ Iteration:** Tail recursive functions are mathematically equivalent to `while` loops.

## 📊 Visual Concept Map (ASCII)

```
       [ALGORITHMIC FOUNDATIONS]
              /         \
      [PHYSICAL]       [ABSTRACT]
         |                 |
     [RAM MODEL]       [ANALYSIS]
    /     |     \      /    |    \
 Stack   Heap   Cache O() Omega() Theta()
   |      |       |
 Local  Dynamic  Speed
 Vars   Objects

```

## ⚡ Highlights of Each Day

* **Day 1:** Visualizing the "Cache Miss" penalty (~100ns).
* **Day 2:** Learning that  is the limit for comparison sorting.
* **Day 3:** distinguishing between *Auxiliary* and *Total* space.
* **Day 4:** Tracing the "Unwinding" phase where the work actually happens.
* **Day 5:** Converting  space factorial to  space using Tail Recursion.

## 🎓 Learning Milestones

* [ ] **Milestone 1:** Can trace a pointer dereference chain on paper.
* [ ] **Milestone 2:** Can derive Big-O for a triple-nested loop with dependent variables.
* [ ] **Milestone 3:** Can calculate the memory usage of a recursive tree.
* [ ] **Milestone 4:** Can write a valid recursive function without a Stack Overflow.
* [ ] **Milestone 5:** Can explain TCO to a layperson.

---
