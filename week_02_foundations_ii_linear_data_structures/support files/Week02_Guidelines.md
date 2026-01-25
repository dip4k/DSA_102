# 📋 Week 02 Guidelines: Linear Data Structures & Binary Search Foundations

**Week Overview:** Foundations II — Understanding arrays, linked lists, stacks, queues, and binary search  
**Status:** Core Curriculum Week (Days 1-5) + Optional Advanced (Day 6)  
**Primary Goal:** Master fundamental linear data structures and their critical algorithms  
**Time Allocation:** 20-24 hours core learning + deep practice  

---

## 🎯 Week 02 Learning Arc

This week, you move from theory (Week 01: memory, asymptotics, recursion) to practice. You'll master the data structures that form the backbone of almost every algorithm and system. By week's end, you'll understand why arrays are fast for random access but slow for insertion, why linked lists are flexible but cache-unfriendly, and how binary search achieves O(log n) magic on sorted data.

---

## 📚 Day-by-Day Comprehensive Overview

### Day 1: Static and Dynamic Arrays

**Core Mental Model:** Arrays are contiguous memory blocks. Static size is fixed; dynamic size grows when needed.

**Static Arrays**

- **Representation:** Contiguous memory cells
- **Index-Address Mapping:** `address = base + (index × element_size)`
- **Random Access:** O(1) constant time
- **Locality:** Contiguous layout = excellent cache behavior
- **Pros:** O(1) access, prefetching-friendly, cache-efficient
- **Cons:** Fixed size, resizing requires copying, mid-insertion O(n)

**Row-Major vs Column-Major Layout**
- Row-major (C, C++, Java): rows stored contiguously [1,2,3,4,5,6] for 2×3 matrix
- Column-major (Fortran): columns stored contiguously
- Impact: Row-major is faster iterating by row

**Systems Angle:** Cache lines (64 bytes) mean sequential access is much faster than random. Algorithms exploiting locality win.

**Dynamic Arrays**

- **Model:** Logical size vs capacity
- **Growth Strategy:** Double capacity when full (most common)
  - Insert 1 element: cost O(n) reallocation, but only occasionally
  - Amortized cost over many inserts: O(1) per insert
- **Why Doubling?** If growth is linear (add k each time), amortized cost is O(n). Exponential growth (multiply by 2) gives O(1) amortized.

**Amortized Analysis:**
- 1 insert → capacity 2 (cost 1 for element)
- 2 inserts → capacity 4 (cost 1 reallocation of 2 elements = 2 work total)
- 4 inserts → capacity 8 (cost 1 reallocation of 4 elements = 4 work total)
- 8 inserts → capacity 16 (cost 1 reallocation of 8 elements = 8 work total)
- Total work for n inserts: 1 + 2 + 4 + 8 + ... + n = 2n = O(n)
- Per insert: O(n) / n = O(1) amortized

**Reallocation Effects:**
- All old elements copied to new array
- Pointers/iterators to old array may become invalid
- Address space changes

**Reserve and Shrink:**
- Reserve capacity in advance to avoid reallocation
- Shrink-to-fit: shrink capacity to match size (trade space for speed)

**Real-World:** std::vector (C++), ArrayList (Java), list (Python)

---

### Day 2: Linked Lists

**Core Mental Model:** Nodes connected by pointers. Flexible size but no random access.

**Singly Linked List**
- **Node Structure:** `[value | next pointer]`
- **Operations:**
  - Insert at head: O(1) (create node, update head)
  - Delete at head: O(1) (update head)
  - Insert at position i: O(i) time (must traverse to position i-1)
  - Insert at tail: O(n) without tail pointer, O(1) with tail pointer
  - Delete at arbitrary position: O(position) time (need previous node)
- **Random Access:** O(n) (must traverse from head)
- **Pros:** O(1) insertion/deletion at known node, flexible size
- **Cons:** O(n) random access, poor cache locality (pointer chasing)

**Doubly Linked List**
- **Node Structure:** `[prev pointer | value | next pointer]`
- **Advantages:** Can traverse in both directions, delete node given pointer (only node, not position)
- **Disadvantages:** Extra space for prev pointer, more pointer updates on insert/delete

**Tail Pointer Optimization:**
- Maintain tail pointer to enable O(1) append
- Delete tail becomes O(n) in singly linked list (need to find predecessor)
- Doubly linked list + tail pointer: O(1) delete tail

**Systems Angle:** Pointer chasing causes cache misses. Array of size 1000 is faster than linked list of size 10 for iteration, despite O(n) complexity for both, because array has locality.

**Real-World:** std::list (C++), LinkedList (Java), collections.deque (Python for double-ended)

---

### Day 3: Stacks and Queues

**Stacks: LIFO (Last-In, First-Out)**

- **Operations:**
  - Push: add to top, O(1)
  - Pop: remove from top, O(1)
  - Peek: view top element, O(1)
- **Connection to Call Stack:** Function calls push frames, returns pop frames
- **Use Cases:**
  - Expression evaluation (infix to postfix)
  - Backtracking (DFS on graphs, maze solving)
  - Balanced parentheses checking
  - Undo/redo functionality
- **Implementations:**
  - Array-based: Fixed size or dynamic
  - Linked list-based: Unlimited size, slightly more overhead

**Queues: FIFO (First-In, First-Out)**

- **Operations:**
  - Enqueue: add to back, O(1)
  - Dequeue: remove from front, O(1)
  - Peek front: view first element, O(1)
- **Use Cases:**
  - BFS on graphs (layer-by-layer exploration)
  - Job scheduling, task queues
  - Printer queues, buffer management
- **Circular Buffer Implementation:**
  - Array with front and back pointers
  - Wrap around at end to avoid shifting elements
  - Efficient for array-based queues
- **Linked List Implementation:**
  - Maintain head (front) and tail (back) pointers
  - Enqueue appends at tail, dequeue removes from head

**Deques: Double-Ended Queues**

- **Operations:** Insert/remove at both ends in O(1)
- **Use Cases:** Monotonic queues, sliding window problems
- **Real-World:** std::deque (C++), collections.deque (Python)

**Real-World:** Queue data structures are everywhere—message queues (RabbitMQ), task schedulers (Kubernetes), buffering (network packets).

---

### Day 4: Introduction to Binary Search

**Core Mental Model:** Maintain invariant (target in [low, high)) and narrow the search space geometrically.

**Binary Search Basics**

- **Precondition:** Array must be sorted
- **Invariant:** At each step, the target (if it exists) lies in [low, high)
- **Algorithm:**
  ```
  low = 0, high = n
  while low < high:
    mid = low + (high - low) / 2
    if arr[mid] == target:
      return mid
    elif arr[mid] < target:
      low = mid + 1  // target in [mid+1, high)
    else:
      high = mid     // target in [low, mid)
  return -1 (not found)
  ```
- **Complexity:** O(log n) (depth of recursion/iterations = log n)

**Safe Implementation:**
- **Mid Calculation:** `mid = low + (high - low) / 2` (avoids overflow in `(low + high) / 2`)
- **Termination:** Loop ends when `low == high`, which means either found or not found
- **Off-by-One:** Common pitfall—verify boundary conditions carefully

**Variants:**

1. **First Occurrence:** Find leftmost element equal to target
2. **Last Occurrence:** Find rightmost element equal to target
3. **Lower Bound:** Find first element ≥ target
4. **Upper Bound:** Find first element > target

Example: For array [1,2,2,2,3] and target=2:
- First occurrence: index 1
- Last occurrence: index 3
- Lower bound: index 1
- Upper bound: index 4

**Binary Search on Answer Space** (More Powerful)

- **Insight:** If monotone condition exists (feasible → more feasible), binary search applies
- **Pattern:**
  - Define answer space (e.g., capacity 1 to max)
  - Check if answer X is feasible
  - Binary search to find minimum X that satisfies
- **Examples:**
  - **Capacity planning:** Given deadline D, find minimum capacity C such that all jobs fit
  - **Minimizing maximum load:** Distribute tasks among servers to minimize max server load
  - **Aggressive cows:** Place k cows in stalls to maximize minimum distance between any two

**Why This Matters:** Binary search on answer space is powerful for optimization problems where direct search is expensive.

---

## 🎓 Nine Core Learning Objectives

By end of Week 02, you will be able to:

1. **Design array layouts** and understand index-to-address mapping
2. **Analyze amortized complexity** and understand doubling strategy in dynamic arrays
3. **Choose between array and linked list** for specific use cases
4. **Implement stacks and queues** efficiently with both array and linked list backends
5. **Implement binary search** correctly, avoiding off-by-one errors
6. **Extend binary search** to variants (first/last occurrence, bounds) and answer space
7. **Trace data structure operations** manually and predict performance
8. **Understand systems implications:** locality, cache behavior, pointer overhead
9. **Apply binary search** to optimization problems

---

## 🧭 Four Effective Study Strategies

### Strategy 1: Invariant Thinking
For each data structure operation, write the invariant BEFORE coding.
- Array: "first i elements processed"
- Queue: "front points to first element, back to last"
- Binary search: "target in [low, high)"

### Strategy 2: Manual Tracing
For each algorithm, trace on paper with small input (n=5-10).
- Arrays: draw memory layout, show address calculations
- Linked lists: draw nodes and pointers, visualize insertions
- Binary search: trace iterations, mark invariant at each step

### Strategy 3: Complexity Accounting
For each operation, break down:
- Time: work per element × number of elements
- Space: what's stored, where it's stored (stack vs heap)
- Constants: cache effects, pointer overhead

### Strategy 4: Systems Awareness
Understand WHY structure has its properties:
- Why O(1) array access? Contiguous memory, address formula
- Why O(n) linked list access? Pointer chasing, no direct addressing
- Why amortized O(1) insert? Doubling spreads reallocation cost

---

## ⏱️ Recommended Time Allocation

| Activity | Hours | Focus |
|----------|-------|-------|
| Read Day 1 instructional | 2 | Arrays, static and dynamic |
| Practice Day 1 problems | 3 | Array operations, amortized analysis |
| Read Day 2 instructional | 2 | Linked lists, trade-offs |
| Practice Day 2 problems | 3 | Linked list operations, reversals |
| Read Day 3 instructional | 1.5 | Stacks, queues, deques |
| Practice Day 3 problems | 2.5 | Stack/queue applications |
| Read Day 4-5 instructional | 2.5 | Binary search, variants, answer space |
| Practice Day 4-5 problems | 4 | Binary search problems, optimization |
| Integration & synthesis | 2 | Review all 5, choose appropriate DS |
| Optional Day 6 (advanced) | 1-2 | Amortized analysis proof, advanced variants |
| **Total Core (Days 1-5)** | **22 hours** | **Interview prep complete** |
| **Total with Day 6** | **23-24 hours** | **Mastery level** |

---

## 🚫 Five Common Pitfalls (and How to Avoid Them)

### Pitfall 1: Ignoring Amortized Complexity

**Wrong:** "Dynamic array insert is O(n), so don't use it"

**Reality:** Reallocation happens rarely. Over many inserts, amortized cost is O(1).

**Fix:** Understand amortized analysis. Spreading cost of occasional expensive operations over many cheap operations gives true cost.

---

### Pitfall 2: Pointer Aliasing in Linked Lists

**Wrong:** After deleting node X, still using pointers to X

**Reality:** Node is gone; pointer is invalid (dangling pointer)

**Fix:** Update all pointers when modifying structure. Use predecessor pointers carefully.

---

### Pitfall 3: Binary Search Off-by-One

**Wrong:** `mid = (low + high) / 2`, or wrong termination condition

**Reality:** Can cause infinite loops or incorrect boundaries

**Fix:** Use `mid = low + (high - low) / 2`. Verify invariant [low, high) at each step. Test on small examples.

---

### Pitfall 4: Not Leveraging Binary Search on Answer Space

**Wrong:** Trying to directly compute optimal capacity, load, or distance

**Reality:** Binary search makes it O(log max_value × verification_cost)

**Fix:** Recognize monotone condition, binary search answer space, verify feasibility.

---

### Pitfall 5: Choosing Wrong Data Structure for Task

**Wrong:** Using linked list for frequent random access, or array for frequent insertion in middle

**Reality:** Performance is terrible due to O(n) random access or O(n) insertion

**Fix:** Analyze operation frequencies. Array good for access, linked list good for insertion at known position. Hybrid structures (e.g., deque) for both.

---

## 📊 Concept Map: Week 02 Data Structures

```
Week 02: Linear Data Structures & Binary Search

├─ ARRAYS (Contiguous Memory)
│  ├─ Static: Fixed size, O(1) access, can't resize
│  └─ Dynamic: Grows via doubling, O(1) amortized insert
│
├─ LINKED LISTS (Pointer-Based)
│  ├─ Singly: O(1) head insert, O(n) arbitrary access
│  └─ Doubly: Bidirectional traversal, O(1) tail delete
│
├─ STACKS & QUEUES (Specialized Linear)
│  ├─ Stack: LIFO, use for DFS, backtracking, expression eval
│  ├─ Queue: FIFO, use for BFS, scheduling, buffering
│  └─ Deque: Both ends, use for sliding window
│
└─ BINARY SEARCH (Algorithm on Sorted Data)
   ├─ On Arrays: O(log n) search, variants for boundaries
   └─ On Answer Space: O(log range × verification) optimization
```

---

## 🎯 Ten Key Insights

1. **Amortized complexity reveals true cost.** Doubling strategy seems wasteful (copying n elements) but spreads cost over many operations, giving O(1) per insert on average.

2. **Locality matters more than asymptotics.** Array of 1000 faster than linked list of 10 for iteration, despite same O(n) complexity, because of cache line prefetching.

3. **Linked lists have hidden costs.** Each node needs space for pointer(s) and object header. Data might be scattered across heap, causing cache misses on every dereference.

4. **Stacks and queues are abstractions.** Implementation (array vs linked list) can be hidden. Understand both to choose wisely.

5. **Binary search on answer space is powerful.** If you can check feasibility, you can binary search to find optimal value without explicitly trying all candidates.

6. **Invariants make binary search correct.** The condition [low, high) represents where target must be. Violate it, and algorithm breaks.

7. **Variants matter:** First/last occurrence, lower/upper bounds all work because sorted order is maintained.

8. **Doubling is geometric growth, not linear.** Linear growth (add k) gives O(n) amortized; doubling (multiply by 2) gives O(1) amortized—huge difference.

9. **Pointer overhead is real.** Linked list with n integers uses ~3n words (value + 2 pointers), vs n words for array. Space overhead is 3x.

10. **Choose structures based on access patterns.** Frequent random access? Use array. Frequent insertion/deletion at known position? Use linked list. Both? Use advanced structures or hybrid approaches.

---

## 🔄 How Week 02 Connects to Surrounding Weeks

**From Week 01 (Memory & Asymptotics):**
- RAM model explains O(1) array access
- Big-O notation classifies operations
- Memory layout and cache hierarchy underlie performance differences

**To Week 03 (Sorting & Hashing):**
- Sorted arrays enable binary search
- Arrays are foundation for sorting algorithms
- Hashing uses arrays internally (hash tables)

**To Week 04 (Problem Patterns):**
- Two-pointer pattern uses arrays
- Sliding window uses arrays
- Binary search as pattern (not just algorithm)

**To Week 07+ (Trees & Graphs):**
- Trees generalize linked lists
- Graph representations use arrays and linked lists
- BFS uses queues, DFS uses stacks

---

## ✅ Weekly Checklist

By end of week, you should be able to:

- [ ] Explain index-to-address mapping and memory layout
- [ ] Analyze amortized complexity of dynamic array insertion
- [ ] Compare array vs linked list trade-offs
- [ ] Implement stack with both array and linked list backend
- [ ] Implement queue with circular buffer (array-based)
- [ ] Implement binary search without off-by-one errors
- [ ] Implement binary search variants (first/last, lower/upper bound)
- [ ] Apply binary search to answer space (optimization)
- [ ] Trace data structure operations manually on paper
- [ ] Solve 15-20 problems using these structures

---

## 🎓 Mastery Signals

You've mastered Week 02 when:

1. **You choose structures instinctively.** Given operation frequencies, you immediately pick array or linked list.

2. **You implement without reference.** Can code array, linked list, stack, queue, binary search fluidly.

3. **You understand amortized complexity.** Can explain why doubling gives O(1) amortized, not just accept it.

4. **You trace systems-level details.** Understand cache behavior, pointer overhead, locality implications.

5. **You recognize binary search opportunities.** See monotone conditions and apply binary search on answer space.

6. **You handle edge cases proactively.** Off-by-one errors, empty structures, single elements—all handled before bugs appear.

7. **You connect to real systems.** Know why memory allocators double pool size, why databases use B-trees (arrays + pointers), why message queues are queues.

---

**Status:** Week 02 Guidelines Complete  
**Next:** Week 02 Summary & Key Concepts  
**Time to Completion:** 22+ hours for full mastery