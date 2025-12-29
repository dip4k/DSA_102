# 🗺️ WEEK 1: PROBLEM-SOLVING ROADMAP

## 📍 Learning Path for the Week

We are building the "Physics Engine" of your algorithmic brain. You cannot drive the car (solve LeetCode Hards) until you understand the engine (RAM & Recursion).

### **Phase 1: The Mechanic (Days 1-2)**

* **Goal:** Execute code manually.
* **Activity:** Trace pointers and count operations.
* **Milestone:** Correctly identify the output of a pointer-heavy C snippet without running it.

### **Phase 2: The Architect (Day 3)**

* **Goal:** Budget resources.
* **Activity:** Analyze space vs. time tradeoffs.
* **Milestone:** Explain why a recursive solution might cause an OOM (Out of Memory) error where an iterative one wouldn't.

### **Phase 3: The Magician (Days 4-5)**

* **Goal:** Trust the induction.
* **Activity:** Write recursive logic.
* **Milestone:** Implement `Power(x, n)` in  using recursion.

## 📈 Progression: Simple → Complex

1. **Level 1 (Pointer Arithmetic):** Simple value access `*ptr`.
2. **Level 2 (Linear Recursion):** `fact(n)` or `sum(arr)`. Single branch.
3. **Level 3 (Branching Recursion):** `fib(n)`. Two branches. Tree structure.
4. **Level 4 (Accumulator Pattern):** `fact(n, acc)`. Passing state down.
5. **Level 5 (Memoization Prep):** Analyzing repeated states in `fib(n)`.

## ⚔️ Practice Problem Recommendations

### **Day 1: RAM & Pointers**

* **Problem:** Swap two numbers using pointers.
* **Problem:** Calculate the size of a struct with padding.
* **Problem:** Implement a precise `memcpy` (byte by byte).

### **Day 2: Asymptotic Analysis**

* **Problem:** Find Big-O of `for (i=1; i<n; i*=2)`.
* **Problem:** Find Big-O of `for (i=0; i<n; i++) for (j=0; j<i; j++)`.
* **Problem:** Solve  using Master Theorem.

### **Day 3: Space Complexity**

* **Problem:** Analyze space of Iterative vs Recursive Binary Search.
* **Problem:** Compare space of `int[]` vs `ArrayList<Integer>` in Java.

### **Days 4-5: Recursion**

* **Problem:** LeetCode #509 (Fibonacci Number) - Try both recursive and iterative.
* **Problem:** LeetCode #344 (Reverse String) - Solve recursively.
* **Problem:** LeetCode #231 (Power of Two) - Recursive bit manipulation check.
* **Problem:** Towers of Hanoi (Classic branching recursion).

## 🎯 Problem-Solving Strategies

### **The "20-Minute Rule"**

If you are stuck on a recursion problem for 20 minutes:

1. **Stop coding.**
2. **Draw the Tree.** Identify the Base Case and the Recursive Step.
3. **Check the Type.** Is it linear (one call) or branching (multiple calls)?
4. **Trace Small N.** Walk through  manually.

### **The "Base Case First" Doctrine**

Never write the recursive step until the base case is written and verified. The base case is your safety net.

## 💡 Common Pitfalls to Avoid

* **Pitfall:** Confusing the "Return Phase" with the "Call Phase."
* *Fix:* Remember that code *after* the recursive call happens on the way *up* the stack (unwinding).


* **Pitfall:** Ignoring hidden space costs.
* *Fix:* Always count the stack depth () as  space.


* **Pitfall:** Over-optimizing constants.
* *Fix:* Focus on Big-O first. Optimization is for Day 1 concepts (Cache), but Logic is Day 2 (Big-O).



## ✅ Milestone Achievements

* [ ] I can identify a cache-unfriendly loop.
* [ ] I can prove why Merge Sort is .
* [ ] I can write a tail-recursive function.
* [ ] I understand why `fib(n)` is .

## 🔗 Prerequisites & Next Week

* **Next Week:** Linear Structures (Arrays, Lists).
* **Connection:** You will apply **Pointers** to build Linked Lists. You will apply **Big-O** to compare Lists vs Arrays. You will apply **Recursion** to traverse Lists.

---
