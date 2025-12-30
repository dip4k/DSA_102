# 🎙 Week 2 Interview Q&A Reference — Linear Structures & Binary Search

Use this as a **quick interview prep bank**. Questions are grouped by topic:

- Arrays
- Dynamic Arrays
- Linked Lists
- Stacks & Queues
- Binary Search

Answers are intentionally concise—you can elaborate in an interview.

---

## 📚 Arrays (8 Questions)

**Q1.** Why is accessing `A[i]` in an array O(1)?  
**A:** Because arrays are contiguous. The address of `A[i]` is computed as `base + i * element_size`. Under the RAM model, this arithmetic and the subsequent memory access are constant time, independent of array length.

---

**Q2.** What is the time complexity of inserting an element into the middle of an array?  
**A:** O(n) in the worst case, because all elements from the insertion position to the end must be shifted by one to keep the array contiguous and ordered.

---

**Q3.** When might you prefer an array over a linked list?  
**A:** When you need fast random access and are mostly traversing or appending (e.g., dynamic arrays). Arrays have better cache locality and lower per-element memory overhead than linked lists.

---

**Q4.** How would you implement in-place removal of a value from an array?  
**A:** Use a write index: iterate over the array, copy each element not equal to the target to `A[write]`, increment `write`. At the end, `write` is the new length. This is O(n) time and O(1) extra space.

---

**Q5.** What are the pros and cons of representing matrices as arrays of arrays vs flat arrays?  
**A:** Arrays-of-arrays are conceptually simple but may not be fully contiguous. A flat array with row-major/column-major indexing uses a single contiguous block, improving cache locality and easing vectorization, but requires explicit index arithmetic.

---

**Q6.** Why might an O(n) array traversal be faster than an O(n) linked list traversal?  
**A:** Arrays are contiguous, so they benefit from CPU caches and prefetching; each access is likely in the same or nearby cache line. Linked lists are scattered, causing more cache misses and pointer overhead despite having the same asymptotic complexity.

---

**Q7.** What’s the difference between logical length and capacity for an array-like structure?  
**A:** Logical length (size) is the number of valid elements; capacity is the total number of slots allocated. Capacity ≥ size, and excess capacity allows for O(1) amortized append without reallocating on every insertion.

---

**Q8.** How can you rotate an array in-place by k positions?  
**A:** Use reversals: reverse the whole array, then reverse the first k elements, then reverse the remaining n−k. This rotates right by k in O(n) time and O(1) space.

---

## 📦 Dynamic Arrays (8 Questions)

**Q9.** Why is `push_back` on a dynamic array amortized O(1)?  
**A:** The array over-allocates capacity and only occasionally resizes (e.g., doubling). Each element is copied a constant number of times over many pushes, so total work over n pushes is O(n), giving O(1) amortized per push.

---

**Q10.** What happens when a dynamic array runs out of capacity during an append?  
**A:** It allocates a new, larger block (usually with capacity multiplied by a factor like 2), copies the existing elements to the new block, then adds the new element. This single operation is O(n), but infrequent.

---

**Q11.** How does a dynamic array’s growth factor affect performance?  
**A:** Larger growth factors (e.g., 2) reduce the frequency of resizes (fewer O(n) events) but increase memory overhead (more unused capacity). Smaller factors reduce overhead but cause more frequent resizes.

---

**Q12.** What is the complexity of inserting at the front of a dynamic array?  
**A:** O(n) because you must shift all existing elements one position to the right, regardless of available capacity.

---

**Q13.** What is the purpose of a `reserve()` method (e.g., in C++ `vector`)?  
**A:** It pre-allocates capacity to avoid multiple resizes as elements are added. This reduces the number of O(n) copy operations and can improve performance when you know the required size.

---

**Q14.** Why don’t many dynamic array implementations shrink capacity aggressively on every pop?  
**A:** Shrinking each time size decreases would cause frequent reallocations and copies (thrashing), destroying amortized guarantees and performance. Instead, they shrink conservatively or not at all.

---

**Q15.** How does dynamic array behavior map to Python lists or Java `ArrayList`?  
**A:** Both use dynamic arrays under the hood. They provide O(1) indexing, amortized O(1) append, and O(n) insert/delete in the middle, with additional capacity to minimize resizes.

---

**Q16.** When is a dynamic array not appropriate?  
**A:** When you need strict O(1) worst-case per operation (no occasional O(n) spikes), or when frequent insertions/deletions in the middle dominate, in which case linked lists or tree-based structures may be better.

---

## 🔗 Linked Lists (8 Questions)

**Q17.** Why is deletion in a singly linked list O(1) given a pointer to the previous node?  
**A:** Because you can bypass the target node by setting `prev.next = prev.next.next`. This requires updating a single pointer and does not depend on list length.

---

**Q18.** How do you reverse a singly linked list in-place?  
**A:** Use three pointers: `prev`, `curr`, `next`. For each node, save `curr.next` in `next`, set `curr.next = prev`, then move `prev = curr`, `curr = next`. At the end, `prev` is the new head.

---

**Q19.** Why is random access O(n) in a linked list?  
**A:** Because you must traverse from the head, following `next` pointers, until reaching the desired position. There is no direct indexing by offset in linked lists.

---

**Q20.** When would you choose a doubly linked list over a singly linked list?  
**A:** When you need O(1) deletion given a pointer to the node (without needing its predecessor) or bidirectional traversal. This is useful in LRU caches and intrusive lists where you move nodes around frequently.

---

**Q21.** How do you detect a cycle in a linked list?  
**A:** Use Floyd’s cycle-finding algorithm (fast and slow pointers). Move `slow` by one and `fast` by two nodes. If they ever meet, there’s a cycle; if `fast` or `fast.next` becomes null, there isn’t.

---

**Q22.** What are intrusive linked lists and why do kernels use them?  
**A:** Intrusive lists store the list pointers inside the elements themselves. Kernels use them to avoid extra node allocations, reduce memory overhead, and keep metadata close to data, improving locality.

---

**Q23.** What is the space overhead of a linked list vs an array?  
**A:** Linked lists store at least one pointer per node (plus allocator overhead), so they use more memory and are less compact than arrays, which store only the elements.

---

**Q24.** Why are linked lists often slower than arrays despite similar Big-O complexities?  
**A:** Pointer chasing leads to poor cache locality and more cache misses. Arrays pack data contiguously, so traversals leverage caches much better.

---

## 📚 Stacks & Queues (8 Questions)

**Q25.** Give three real-world examples of stack usage.  
**A:** Call stack for function calls/recursion, undo stack in editors, expression evaluation (RPN or operator precedence parsing).

---

**Q26.** How can you implement a stack using a dynamic array?  
**A:** Use the dynamic array as a backing store and treat the logical end as the top. `push` appends, `pop` removes the last element by decrementing size, `top` reads the last element.

---

**Q27.** Why is a circular buffer useful for queue implementation?  
**A:** It allows enqueue/dequeue in O(1) time without shifting elements. Head and tail indices wrap around, reusing freed slots in a fixed-size array.

---

**Q28.** Explain BFS in terms of queue operations.  
**A:** BFS maintains a queue of nodes to visit. It dequeues a node, processes it, and enqueues its unvisited neighbors. This ensures nodes are processed in order of distance from the source (FIFO).

---

**Q29.** How do you implement a queue using two stacks?  
**A:** Maintain `inStack` for enqueues and `outStack` for dequeues. For dequeue, if `outStack` is empty, move all elements from `inStack` to `outStack` (reversing order). Then pop from `outStack`. This yields amortized O(1) per operation.

---

**Q30.** When would you use a stack instead of recursion?  
**A:** When recursion depth might exceed stack limits or when you want more control over traversal order and resource use. You simulate the call stack manually with an explicit stack.

---

**Q31.** Compare linked-list-based and array-based implementations of queues.  
**A:** Linked list queues allow dynamic size without fixed capacity and O(1) enqueue/dequeue, but suffer from pointer overhead and worse locality. Array-based circular queues are more cache-friendly and simpler when capacity is known.

---

**Q32.** What is a monotonic stack and where is it used?  
**A:** A monotonic stack maintains elements in sorted order (increasing or decreasing) as you traverse a sequence. It’s used for problems like next greater element, daily temperatures, and largest rectangle in histogram.

---

## 🔍 Binary Search (8 Questions)

**Q33.** What conditions must hold for binary search to be correct?  
**A:** The search space must be ordered or the predicate must be monotonic. The algorithm must maintain correct loop invariants (e.g., if target exists, it’s in [L,R]) and properly narrow the interval each step.

---

**Q34.** How do you avoid overflow when computing `mid` in binary search in low-level languages?  
**A:** Use `mid = left + (right - left) / 2` instead of `(left+right)/2` to avoid integer overflow when left+right exceeds max int.

---

**Q35.** How would you implement binary search to find the first element ≥ target?  
**A:** Use lower-bound style: initialize `L=0, R=n`. While L<R, mid=(L+R)//2. If A[mid] ≥ target, set R=mid; else L=mid+1. At end, L is the first index with A[L]≥target or n if none.

---

**Q36.** What is binary search on answer?  
**A:** It’s using binary search over an ordered range of possible answers (e.g., capacity, time) rather than array indices, relying on a monotonic feasibility predicate `f(x)` to guide the search for the minimal or maximal valid x.

---

**Q37.** When might binary search be slower than linear search?  
**A:** For small arrays, the overhead of computing mid and branching can outweigh the benefit of fewer comparisons, making a simple linear scan faster in practice.

---

**Q38.** How does binary search work in a rotated sorted array?  
**A:** At each step, determine which half is sorted by comparing A[L], A[mid], A[R]. Then check if the target lies within that sorted half; if so, search there; otherwise, search the other half.

---

**Q39.** Why is binary search preferred over hash lookup when range queries are needed?  
**A:** Binary search operates on sorted data and naturally supports range queries (e.g., all elements in [L,R]). Hash tables provide O(1) point lookups but don’t maintain order, making range queries harder and more expensive.

---

**Q40.** How many steps does binary search take for an array of size 1,000,000?  
**A:** About ⌈log₂ 1,000,000⌉ ≈ 20 steps. Each step halves the interval. This illustrates how efficient O(log n) is compared to O(n).

---