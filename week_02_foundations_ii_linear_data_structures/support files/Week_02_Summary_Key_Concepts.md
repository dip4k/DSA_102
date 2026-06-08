# 📖 Week 02 Summary & Key Concepts: Linear Data Structures Deep Reference

**Audience:** Students completing Week 02 instructional content  
**Purpose:** Comprehensive, high-fidelity reference for review, retention, and quick reference.

---

## 🏗️ Day 1: Static Arrays & Memory Layout

Static arrays are contiguous blocks of physical memory with a fixed size determined at allocation.

### Address Offset Arithmetic
To retrieve an element at target index `i`, computer runtimes compute the destination address in constant time without iterating elements:
$$\text{Address}(i) = \text{Base} + i \times \text{Stride}$$
Where `Base` is the starting memory address, and `Stride` corresponds to the byte size of each element.

### CPU Caching & Cache Lines
*   **Cache Lines**: Memory controllers fetch data in sequential blocks (typically 64 bytes) called cache lines.
*   **Sequential Scan Advantage**: Because arrays store elements contiguously, reading element `0` automatically loads the subsequent elements into the L1/L2 cache, leading to near-instantaneous sequential read speeds.
*   **Stride Jumps Cost**: Reading elements with large stride differences (or randomly) forces the hardware to discard pre-fetched cache lines, causing slow system lookups.
*   **Matrix Layouts**:
    *   **Row-major Layout**: Rows are stored contiguously in memory. Iterating columns inside rows (`matrix[r][c]`) maximizes cache hits.
    *   **Column-major Layout**: Columns are stored contiguously in memory.

---

## 🏗️ Day 2: Dynamic Arrays & Amortized Growth

Dynamic arrays combine static arrays with geometric growth strategies to support open-ended elements appends.

### Capacity Resizing Theory
*   **Logical Length**: Actual count of occupied slots.
*   **Physical Capacity**: Count of allocated memory slots.
*   **Doubling Rescale**: When logical length reaches physical capacity, allocate a new array of size `2 x capacity` and copy existing values.
*   **Amortized Analysis**: Copying N elements is an O(N) operation. However, resizing happens exponentially rarely (at indices 1, 2, 4, 8, ...). Summed over N pushes, copying takes:
    $$1 + 2 + 4 + 8 + ... + N = 2N - 1 = O(N)$$
    Spread over N pushes, average insertion takes O(1) amortized time.
*   **Incremental Growth Pitfall**: Growing by a fixed amount (like +k slots) during overflows requires O(N^2) work for N pushes, yielding slow O(N) average-case insertions.

---

## 🔗 Day 3: Linked Lists

Linked Lists store elements in separate node objects scattered across the heap, connected with reference pointers.

### Singly vs. Doubly Linked Lists
*   **Singly Linked List (SLL)**: Nodes contain a value and a single `next` pointer.
    *   *Pros*: Small memory footprint.
    *   *Cons*: Bidirectional traversal is impossible; deleting a node requires finding its predecessor.
*   **Doubly Linked List (DLL)**: Nodes contain prev, value, and next pointers.
    *   *Pros*: Supports bidirectional traversal and O(1) removals.
    *   *Cons*: Extra pointer per node (8 bytes on 64-bit platforms).
*   **Pointer Reversal**: Reversing an SLL is achieved by re-wirings nodes using three pointers (`prev`, `curr`, `next`) iteratively.
*   **Locality Gap**: Linked lists lack contiguity, which causes cache line faults on every node lookup, making sequential scans significantly slower than array iterations.

---

## 📚 Day 4: Stacks, Queues & Deques

Enforcing access discipline simplifies algorithms and enables high-performance optimizations.

*   **Stacks (LIFO: Last-In, First-Out)**: Push and Pop at top in O(1). Ideal for calls tracking, bracket validation, and local backtracking states.
*   **Queues (FIFO: First-In, First-Out)**: Enqueue at back, Dequeue at front in O(1). Used for pipeline buffers, job schedules, and graphs Breadth-First Searches.
*   **Circular Buffers**: Implement arrays queues in constant time without shifting elements by utilizing modulo wraparound arithmetic:
    $$\text{front} = (\text{front} + 1) \% \text{capacity}$$
    $$\text{back} = (\text{back} + 1) \% \text{capacity}$$
*   **Deques (Double-Ended Queues)**: Generalize stacks and queues, supporting constant O(1) operations at both ends. Ideal for sliding window monotonic queues.

---

## 🔍 Day 5: Binary Search & Invariants

Binary search narrows the search space half-by-half in sorted arrays by maintaining a search invariant: `target (if it exists) is in [low, high)`.

*   **Midpoint Overflow**: Computing midpoints using `(low + high) / 2` can cause integer overflow when arrays are extremely large. Always use `low + (high - low) / 2`.
*   **Coordinate Bounds**:
    *   **First Occurence (Leftmost)**: Tighten `high = mid` when finding a match to sift left.
    *   **Last Occurence (Rightmost)**: Shift `low = mid + 1` when finding a match to sift right.
    *   **Lower Bound**: Find leftmost element >= target.
    *   **Upper Bound**: Find leftmost element > target.
*   **Search on Answer Space**: Leverages binary search on monotone functions. If feasibility checks are monotonic (X works -> X+1 works), we can binary search on range `[1, Max_Bound]` to locate the minimum possible solution.

---

## 💾 Day 6: Strings & Numbers - Representations & Conversions

Data representations at the hardware level determine system safety and performance.

*   **String Immutability**: String contents cannot be altered once created. Constructing strings iteratively using concatenation (`+=`) allocates a new string each time, leading to O(N^2) copying operations. Use a `StringBuilder` growable array buffer to append characters in linear O(N) time.
*   **Signed Integers**: Represented in hardware registers using **Two's Complement** encoding.
*   **Arithmetic Overflow**: Occurs when values exceed the physical bit-width boundaries, wrapping elements across positive/negative points.
*   **ASCII vs. Unicode & UTF-8/UTF-16**: ASCII is a 7-bit system; Unicode maps characters to codepoints; UTF-16 uses 2 bytes per character; UTF-8 is a variable-length encoding (1 to 4 bytes) that is backward-compatible with ASCII.
*   **Conversions Implementations**: `atoi` parses numeric byte structures to integer spaces; `itoa` extracts digits right-to-left using `% 10` and `/ 10` divisions and reverses the character result safely.
