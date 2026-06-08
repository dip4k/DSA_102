# 🎙️ Week 02 Interview Q&A Comprehensive Reference

This guide provides high-frequency interview questions, verbal answers, and step-by-step walkthroughs for Week 2's core topics across all 6 study days.

---

## 🧮 Day 1: Static Arrays & Memory Layout

#### Q1. What is the address formula for a multi-dimensional row-major matrix? Why does iteration order matter for performance?
*   **Verbal Answer**:
    "In a 2D matrix stored in Row-Major order (where adjacent memory cells within the same row are contiguous), the memory address of cell `matrix[i][j]` is calculated using:
    $$\text{Address}(i, j) = \text{Base} + (i \times \text{Columns} + j) \times \text{Stride}$$
    Because of this memory layout, iterating through the grid row-by-row (incrementing `j` in the innermost loop) matches CPU cache-line prefetching, where 64-byte blocks of contiguous memory are loaded into L1/L2 caches. Conversely, iterating column-by-column (incrementing `i` in the innermost loop) jumps across memory lines, causing frequent cache misses and system stalls."

---

## 🏗️ Day 2: Dynamic Arrays & Amortized Growth

#### Q2. Explain the difference between amortized and worst-case time complexity, using dynamic array capacity doubling as an example.
*   **Verbal Answer**:
    "Worst-case complexity measures the cost of a single, worst-case individual operation. For a dynamic array, appending an element causes a resize if the array is full, which takes $O(N)$ time to copy $N$ elements.
    Amortized complexity, on the other hand, measures the average cost per operation over a sequence of operations. For a sequence of $N$ pushes into an empty dynamic array that doubles its capacity on overflow, we perform $N$ standard appends and $\log_2 N$ resizes.
    The sum of all copy operations is a geometric series:
    $$1 + 2 + 4 + 8 + \dots + N \approx 2N$$
    Therefore, the total work for $N$ additions is at most $3N = O(N)$.
    The average cost per operation is $3N / N = 3 = O(1)$ amortized. This shows that rare, expensive operations are spread evenly over many cheap operations."

---

## 🔗 Day 3: Linked Lists

#### Q3. Compare Arrays and Linked Lists. What are the key performance and memory trade-offs?
*   **Verbal Answer**:
    "Arrays and linked lists have opposite trade-offs:
    *   **Access**: Arrays support constant-time $O(1)$ random indexing. Linked lists require $O(N)$ linear traversal from the head node.
    *   **Insertion/Deletion**: Arrays are slow ($O(N)$) for mid-list modifications because elements must be shifted. Linked lists support constant-time $O(1)$ insertions and deletions once the target node is located, requiring only pointer updates.
    *   **Memory**: Arrays have zero per-element memory overhead. Linked lists have high pointer memory overhead, requiring an extra 8 bytes (or 16 bytes for doubly linked lists) per node on 64-bit systems.
    *   **Hardware Locality**: Arrays are stored contiguously in memory, maximizing CPU cache prefetching. Linked list nodes are allocated dynamically on the heap, leading to pointer-chasing cache misses."

---

## 📚 Day 4: Stacks, Queues & Deques

#### Q4. How do you implement a queue using a circular array buffer? How do you distinguish between full and empty queue states?
*   **Verbal Answer**:
    "A circular queue maintains front and back pointers, wrapping around the end of the array using modular arithmetic:
    $$\text{back} = (\text{back} + 1) \% \text{capacity}$$
    The main challenge is that both empty and full states result in `front == back`.
    We can resolve this in two ways:
    1.  **Keep a `size` count variable**: The queue is empty when `size == 0`, and full when `size == capacity`.
    2.  **Leave one empty slot**: Keep the capacity at $N + 1$. The queue is empty when `front == back`, and full when `(back + 1) % capacity == front`."

---

## 🔍 Day 5: Binary Search & Invariants

#### Q5. Why is `mid = low + (high - low) / 2` preferred over `mid = (low + high) / 2` in binary search?
*   **Verbal Answer**:
    "Computing the midpoint using `mid = (low + high) / 2` is fine for small arrays. However, if the array is extremely large (where `low + high` exceeds the maximum value of a 32-bit signed integer, $2^{31} - 1$), the sum overflows to a negative value, causing out-of-bounds exceptions or infinite loops.
    Using `mid = low + (high - low) / 2` is algebraically identical but prevents overflow because we subtract before adding, keeping the intermediate values within safe boundaries."

---

## 💾 Day 6: Strings & Numbers - Representations & Conversions

#### Q6. What is string immutability, and why does building a string using repeated concatenation inside a loop trigger poor quadratic performance?
*   **Verbal Answer**:
    "String immutability is a design decision where string values cannot be changed after allocation.
    When we append characters to a string in a loop using `s += char`, the runtime cannot modify the existing string in-place. Instead, it must allocate a new string of size `s.Length + 1` and copy all existing characters over.
    This copies increasingly more data with each iteration:
    $$1 + 2 + 3 + \dots + n \approx \frac{n^2}{2} = O(n^2)\text{ copying operations}$$
    To solve this, we use a `StringBuilder` (which manages a contiguous dynamic character array buffer internally). It performs appends in $O(1)$ amortized time, allocating new memory only when the buffer fills."

---

#### Q7. Implement `atoi` (String-to-Integer converter) from scratch. How do you handle overflow before a numerical register overflows?
*   **Verbal Answer**:
    "To implement `atoi` safely, we must parse and build the integer digit-by-digit:
    1.  Skip leading whitespaces.
    2.  Check for optional sign characters (`+` or `-`).
    3.  Iterate over digital characters, computing `result = result * 10 + digit`.
    To prevent overflow *before* it occurs inside 32-bit signed integers, we perform a bounds check prior to updating the result:
    ```csharp
    if (result > (int.MaxValue - digit) / 10) {
        return sign == 1 ? int.MaxValue : int.MinValue;
    }
    ```
    This check keeps intermediate divisions within safe boundaries, allowing us to clamp values to `int.MaxValue` or `int.MinValue` immediately."
