# 🎙️ WEEK 1: INTERVIEW Q&A REFERENCE — COMPLETE ARCHIVE

**🗓️ Week:** 1  |  **Topic:** Foundations (RAM, Pointers, Complexity, Recursion)

**🎯 Goal:** Master the theoretical and mechanical questions asked in FAANG screening rounds.

**📊 Total Questions:** 54

---

## 🟢 PART 1: EASY — FOUNDATIONS & DEFINITIONS (Questions 1-18)

*Focus: Basic definitions, sanity checks, and confirming core understanding.*

**Q1: What exactly is a "Word" in computer architecture?**
📢 **A:** A "Word" is the natural unit of data used by a processor. In modern 64-bit systems, a word is 64 bits (8 bytes). This dictates the size of general-purpose registers and the width of the data bus. Pointers on a 64-bit system are 8 bytes because they need to address the full range of memory ( addresses).

**Q2: Explain the difference between `i++` and `++i` in loop complexity analysis.**
📢 **A:** Asymptotically (), there is no difference. However, in older C++ compilers or complex iterator objects, `i++` (post-increment) required creating a temporary copy of `i` before incrementing, whereas `++i` (pre-increment) did not. Modern compilers optimize this for integers, but `++i` is strictly preferred for C++ iterators to avoid object copy overhead.

**Q3: If an algorithm is , is it always faster than an  algorithm?**
📢 **A:** No. Asymptotic analysis applies as . For small inputs, the constants matter. An algorithm taking  operations is slower than  for any .

**Q4: What is the time complexity of accessing an array index `arr[i]` vs. a linked list node at index `i`?**
📢 **A:** Array access is  because it uses direct pointer arithmetic (`base_address + i * size`). Linked list access is  because you must traverse `i` pointers from the head to reach the destination.

**Q5: Why does a recursive function need a base case?**
📢 **A:** To prevent infinite recursion. Without a base case, the function calls itself indefinitely, adding frames to the call stack until the system runs out of memory, causing a Stack Overflow.

**Q6: What is a "Dangling Pointer"?**
📢 **A:** A pointer that points to a memory location that has already been freed or reallocated. Dereferencing it leads to undefined behavior or crashes. This often happens when returning a pointer to a local stack variable from a function.

**Q7: Define "Spatial Locality" in the context of RAM.**
📢 **A:** The tendency of a processor to access memory locations that are close to those it has accessed recently. Arrays exhibit excellent spatial locality; Linked Lists do not.

**Q8: Define "Temporal Locality".**
📢 **A:** The tendency of a processor to access the *same* memory location repeatedly within a short period. Loop counters and stack variables exhibit high temporal locality.

**Q9: What is the Space Complexity of an iterative algorithm that uses no extra data structures?**
📢 **A:**  or "Constant Space." It uses a fixed amount of memory for variables regardless of the input size.

**Q10: Is  faster or slower than ?**
📢 **A:**  is significantly faster. For : , while .

**Q11: What is a Stack Frame (Activation Record)?**
📢 **A:** A block of memory allocated on the Call Stack for a single function call. It contains the function's local variables, parameters, and the return address (where execution should resume after the function ends).

**Q12: Why is `recursion` considered an "implicit" data structure?**
📢 **A:** Because it uses the system's Call Stack to store state. Even if you don't define a list or tree in your code, the recursive calls themselves form a stack structure in RAM ( space).

**Q13: What does "Amortized " mean?**
📢 **A:** It means that while a single operation might be slow (e.g.,  resizing of a dynamic array), it happens so rarely that the average cost over a sequence of operations is constant .

**Q14: What is the "Memory Wall"?**
📢 **A:** The growing disparity between CPU speed (getting faster quickly) and RAM latency (getting faster slowly). The CPU often spends hundreds of cycles waiting for data to arrive from RAM.

**Q15: Why is `2^n` considered "intractable"?**
📢 **A:** Exponential growth is explosive. Adding just 1 to the input size doubles the runtime. For ,  operations would take years on a supercomputer.

**Q16: What is a "Cache Hit"?**
📢 **A:** When the CPU requests a memory address and finds the data already present in the fast L1, L2, or L3 cache, avoiding a slow RAM access.

**Q17: Can you have a recursive function with two base cases?**
📢 **A:** Yes. For example, Fibonacci often uses `if n == 0 return 0` and `if n == 1 return 1`. Both are necessary to anchor the recursion.

**Q18: What is the Big-O of iterating through a 2D array of size ?**
📢 **A:**  if iterating all elements. If  represents the total number of elements (rows  cols), then it is . Context matters.

---

## 🟡 PART 2: MEDIUM — ANALYSIS & APPLICATION (Questions 19-38)

*Focus: Applying theory to code, trade-offs, and optimization.*

**Q19: Why is Binary Search ? Derive it.**
📢 **A:** In each step, we divide the search space by 2. The sequence of problem sizes is .
Mathematically: .
Since  is the number of steps, complexity is .

**Q20: What is the Space Complexity of recursive Factorial?**
📢 **A:** .
Code: `fact(n) { return n * fact(n-1); }`
This is linear recursion. To calculate `fact(n)`, `fact(n-1)` must exist, which requires `fact(n-2)`... The stack grows to depth .

**Q21: Explain the Master Theorem case for Merge Sort ().**
📢 **A:**
Here  (subproblems),  (split factor),  (merge work).
Work at leaves: .
Since  () is equal to work at leaves (), we multiply by .
Result: .

**Q22: Why is the Stack generally faster than the Heap?**
📢 **A:**

1. **Allocation:** Stack allocation is a single CPU instruction (move stack pointer). Heap allocation involves searching for a free block of the right size (complex logic).
2. **Locality:** The Stack is always hot in the L1 cache. The Heap is scattered, causing cache misses.

**Q23: How does a "Memory Leak" occur in C++ vs. Java?**
📢 **A:**

* **C++:** You allocate memory with `new` but forget to `delete` it. The OS considers it used forever.
* **Java:** You hold a reference to an unused object (e.g., in a static List), preventing the Garbage Collector from freeing it.

**Q24: Analyze the complexity of: `for i in 1..n: i *= 2**`
📢 **A:** . The variable `i` grows exponentially (). It will reach  in  steps.

**Q25: What is "Tail Call Optimization" (TCO)?**
📢 **A:** A compiler optimization where, if the *very last* action of a function is a recursive call, the compiler replaces the call with a jump (loop). This reuses the current stack frame, reducing space from  to .

**Q26: Convert `sum(n)` to Tail Recursion.**
📢 **A:**
Standard: `return n + sum(n-1)` (Pending addition prevents TCO).
Tail: `sum(n, acc) { if n==0 return acc; return sum(n-1, acc + n); }`

**Q27: Is it possible to have  time and  space for traversing a Binary Tree?**
📢 **A:** Yes, using **Morris Traversal**. It modifies the tree structure temporarily (threading pointers) to traverse without a stack, then restores it. Standard recursive/iterative traversal is  space.

**Q28: Why does `string += char` inside a loop result in  complexity in Java/Python?**
📢 **A:** Strings are immutable. `s += c` creates a *new* string, copies all old characters, adds the new one, and discards the old string. Copying 1 char, then 2, then 3... sums to .

**Q29: How many cache lines are fetched when reading a contiguous array of 64 integers (4 bytes each)?**
📢 **A:** Total size:  bytes.
Cache line: 64 bytes.
Lines fetched:  lines. (Assuming perfect alignment).

**Q30: What is the "working set" of a program?**
📢 **A:** The set of memory pages that are currently being used by the process. If the working set fits in the cache, performance is high. If it exceeds cache size, "thrashing" occurs.

**Q31: Explain the difference between `Auxiliary Space` and `Total Space`.**
📢 **A:**

* **Auxiliary:** Extra space used by the algorithm (e.g., temp array).
* **Total:** Auxiliary + Input size.
* Example: Sorting an array in-place has  Auxiliary but  Total space.

**Q32: Why is QuickSort preferred over MergeSort for arrays?**
📢 **A:** QuickSort is in-place ( stack space), whereas MergeSort requires  auxiliary space for the temporary merge arrays. QuickSort also has better cache locality.

**Q33: Why is MergeSort preferred over QuickSort for Linked Lists?**
📢 **A:** Linked Lists don't allow random access (bad for QuickSort partitioning). However, merging lists requires only pointer changes ( space), making MergeSort very efficient and stable for lists.

**Q34: What is the worst-case time complexity of QuickSort? How do we avoid it?**
📢 **A:**  if the pivot is always the smallest/largest element (already sorted array). Avoid it by picking a random pivot or "Median of Three".

**Q35: Can you solve the Fibonacci sequence in  time?**
📢 **A:** Yes, using **Matrix Exponentiation**. . Calculating the power  takes  matrix multiplications.

**Q36: Explain "Branch Prediction" and how it affects Big-O constants.**
📢 **A:** The CPU tries to guess which way an `if` statement will go to pre-load instructions. If it guesses right, code is fast. If wrong, it flushes the pipeline (costly). Sorted arrays allow better branch prediction than unsorted ones in conditional logic.

**Q37: What is the complexity of `str.length()`?**
📢 **A:**

* C (char*):  (must count until `\0`).
* C++/Java/Python:  (length is stored as a property).

**Q38: Why is recursion limited? What determines the limit?**
📢 **A:** Limited by the stack size configured by the OS/Runtime (typically 1MB - 8MB). Depth limit = StackSize / FrameSize.

---

## 🔴 PART 3: HARD — SYSTEMS & DEEP THEORY (Questions 39-54)

*Focus: Hardware interaction, mathematical proofs, and system design.*

**Q39: Why does accessing column-major data in a row-major language (C/C++) destroy performance?**
📢 **A:** C/C++ stores 2D arrays row-by-row.
Row-major access: `arr[0][0], arr[0][1]...` uses the same cache line (Fast).
Column-major access: `arr[0][0], arr[1][0]...` jumps memory locations by `RowWidth`. Each access loads a *new* cache line, and we only use 1 value from it. This causes a near 100% cache miss rate.

**Q40: Prove that any comparison-based sorting algorithm must be .**
📢 **A:**

1. There are  possible permutations of an array.
2. The algorithm must identify the one sorted permutation.
3. Each comparison gives 1 bit of info (True/False), splitting the possibility space by at most half.
4. We need a decision tree of height  such that leaves .
5. .
6. By Stirling's approximation, .
7. Therefore, lower bound is .

**Q41: Explain the "thrashing" phenomenon in terms of the RAM model.**
📢 **A:** Thrashing occurs when the active memory working set exceeds physical RAM. The OS constantly swaps pages between RAM and Disk (Virtual Memory). Since Disk is  slower than RAM, the CPU spends nearly 100% of its time waiting for I/O, and the system freezes.

**Q42: Solve the recurrence .**
📢 **A:** This describes the Harmonic Series.
.
Complexity is .

**Q43: How does Python's `list` recursion limit differ from C++?**
📢 **A:** Python has a hard-coded recursion limit (default 1000) to prevent C-stack overflows, as Python stack frames are large C-structs. C++ has no logical limit, only the physical RAM limit of the stack.

**Q44: Why is `3-way QuickSort` (Dutch National Flag) faster for arrays with many duplicates?**
📢 **A:** Standard QuickSort degrades to  with duplicates. 3-way partition groups elements into , , and . The recursive calls ignore the  section, drastically reducing the problem size when many duplicates exist.

**Q45: If you have a 4GB file and 1GB RAM, how do you sort it?**
📢 **A:** **External Merge Sort**.

1. Read 1GB chunks, sort them in memory (), write to temporary files.
2. Open all temporary files simultaneously.
3. Use a Min-Heap (-way merge) to read the smallest element from the files and write to the final output stream.

**Q46: Why is memory allocation in the Heap non-deterministic in time?**
📢 **A:** The memory allocator (like `malloc`) must search for a contiguous block of free memory of size . Due to fragmentation, this search might traverse the entire free list () or require requesting more pages from the OS (Syscall overhead).

**Q47: Identify the complexity: .**
📢 **A:**
Level 1: 1 node
Level 2: 2 nodes
Level 3: 4 nodes
...
Total work is geometric sum .
Complexity: . (Binary recursion without splitting input size).

**Q48: What is the "Locality of Reference" principle?**
📢 **A:** Programs tend to access a small portion of their address space at any instant.

1. **Temporal:** If accessed now, likely accessed again soon (Loop variables).
2. **Spatial:** If accessed now, neighbors likely accessed soon (Arrays).
Hardware caches rely entirely on this principle.

**Q49: How does "False Sharing" degrade performance in multi-threaded arrays?**
📢 **A:** If two threads modify independent variables that happen to sit on the *same* 64-byte cache line, the CPU cores force each other to invalidate and reload that line repeatedly ("ping-ponging"). This serializes the threads at the hardware level, destroying parallelism.

**Q50: Can a recursive algorithm be Iterative?**
📢 **A:** Yes. Any recursive algorithm can be converted to an iterative one using an explicit Stack data structure.  recursion stack space becomes  heap space for the explicit stack.

**Q51: What is the complexity of the "Towers of Hanoi" problem?**
📢 **A:** . To move  disks, you must move  disks to aux, move bottom to target, then move  from aux to target. Recurrence: .

**Q52: Is `System.arraycopy()` (Java) or `memcpy` (C)  or ?**
📢 **A:** It is . Copying memory requires reading and writing every byte. It is highly optimized (using CPU vector instructions like SIMD to copy 128/256 bits at a time), but strictly linear.

**Q53: Why do we care about "Worst Case" more than "Average Case"?**
📢 **A:** Reliability. In safety-critical systems or cloud infrastructure, a single request triggering the worst case can lock up the system (Denial of Service). "Average" implies a random distribution, but real-world input is often not random (e.g., attacks, sorted data).

**Q54: What is the space complexity of a garbage collected language vs manual?**
📢 **A:** GC languages often have higher memory overhead (1.5x - 2x) to function efficiently. They need extra space to allow "dead" objects to accumulate before cleaning. Manual memory management allows tighter bounds but risks leaks.
