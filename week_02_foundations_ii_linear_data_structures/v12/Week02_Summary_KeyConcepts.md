# 📖 Week 02 Summary & Key Concepts: Linear Data Structures Deep Reference

**Status:** Graduate-Level Study Notes  
**Audience:** Students completing Week 02 instructional content  
**Purpose:** Comprehensive reference for review and retention

---

## 🎯 Week 02 Narrative Summary

**Week 02: Foundations II** covers the linear data structures that form the backbone of computer science: arrays, linked lists, stacks, queues, and the binary search algorithm. Understanding these deeply—including trade-offs, memory layout, and amortized analysis—is foundational to everything that follows.

---

## 🏗️ ARRAYS: Complete Model

### Static Arrays

```
Memory Layout (0-indexed):
base = 0x1000
element_size = 4 bytes

Index:  0     1     2     3     4
Addr:  0x1000 0x1004 0x1008 0x100C 0x1010
Value: [10]   [20]   [30]   [40]   [50]

Formula: address(i) = base + i × element_size
address(3) = 0x1000 + 3 × 4 = 0x100C ✓
```

**Properties:**
- **Contiguous memory:** Sequential addresses make prefetching efficient
- **O(1) random access:** Address formula lets jump to any index instantly
- **Cache-friendly:** Multiple array elements fit in one 64-byte cache line
- **Fixed size:** Cannot grow without copying entire array

**Pros & Cons:**

| Advantage | Disadvantage |
|-----------|--------------|
| O(1) access | Fixed size |
| Excellent locality | O(n) resize |
| Prefetching works | O(n) mid-insertion |
| Dense storage | O(n) deletion anywhere |

### Dynamic Arrays (Vector Growth)

**Doubling Strategy:**

```
Insert 1: [10]               capacity=1
Insert 2: [10][20]           capacity=2
Insert 3: Reallocate! Copy to new array [10][20][30]  capacity=4
Insert 4: [10][20][30][40]   capacity=4
Insert 5: Reallocate! Copy to [10][20][30][40][50]    capacity=8
...
Insert n: Total work = 1 + 2 + 4 + 8 + ... ≈ 2n = O(n)
Per insert: O(n) / n = O(1) amortized
```

**Why Doubling Works:**
- Sequence of sizes: 1, 2, 4, 8, 16, ...
- Cost of reallocating at each: 1, 2, 4, 8, 16, ... (copy all current elements)
- Sum: 1 + 2 + 4 + ... + n ≈ 2n (geometric series)
- Distributed: 2n work over n inserts = O(1) per insert

**Alternative: Linear Growth (Add k)**
- Sequence: k, 2k, 3k, 4k, ...
- Cost: k + 2k + 3k + ... ≈ n²/2 (quadratic!)
- Per insert: O(n) amortized (terrible!)

**Reallocation Effects:**
- Old array address invalidated
- Pointers/iterators to old elements no longer valid
- New address space may cause cache misses

**Reserve Strategy:**
- Allocate capacity upfront to avoid reallocations
- Trade memory for speed in time-critical sections

---

## 🔗 LINKED LISTS: Complete Model

### Singly Linked List

```
Node Structure:      [data | next]
List:               Head
                     ↓
                   [10]→[20]→[30]→[40]→ null

Operations:
Insert at head: Create new node, update head. O(1)
Insert at tail: Need to traverse to tail. O(n) without tail pointer
Delete at head: Update head to head.next. O(1)
Delete middle: Need predecessor. O(position)
Search: Linear scan. O(n)
Random access: Follow pointers. O(n)
```

**Properties:**
- **Flexible size:** Grows/shrinks with allocations
- **O(1) insertion at known position:** Just update pointers
- **O(n) random access:** Must follow chain from head
- **Poor locality:** Nodes scattered across heap, cache misses

**Pointer Overhead:**
- Each node: data (size varies) + next pointer (8 bytes on 64-bit)
- Example: 100 integers
  - Array: 400 bytes
  - Linked list: 100×(4+8) = 1200 bytes (3x overhead!)

### Doubly Linked List

```
Node:    [prev | data | next]
List:    Head ↔ Node ↔ Node ↔ Tail
           ↑                    ↑
         sentinel             sentinel
```

**Advantages:**
- Can traverse backwards
- Delete given node pointer (singly requires predecessor)
- Tail pointer enables O(1) append

**Disadvantages:**
- Extra pointer per node (16% overhead for typical node)
- More pointer updates on modification

---

## 📚 STACKS & QUEUES

### Stack Implementation (LIFO)

**Array-Based:**
```cpp
Stack (fixed size n):
- Array of size n
- top pointer (initially -1)
- Push: array[++top] = value. O(1)
- Pop: return array[top--]. O(1)
- Full when top == n-1
```

**Linked List-Based:**
```cpp
Stack (unlimited size):
- Head pointer
- Push: new node at head. O(1)
- Pop: remove head node. O(1)
- No size limit, slight pointer overhead
```

**Use Cases:**
- Expression evaluation (infix to postfix)
- Backtracking (maze, N-queens)
- Matching brackets
- DFS on graphs
- Call stack in function calls

### Queue Implementation (FIFO)

**Array-Based (Circular Buffer):**
```cpp
Queue (size n):
- Array of size n
- front, back indices
- Enqueue: array[back] = value; back = (back+1) % n. O(1)
- Dequeue: return array[front]; front = (front+1) % n. O(1)
- Avoids shifting by wrapping around
```

**Linked List-Based:**
```cpp
Queue (unlimited):
- Head (front) and tail (back) pointers
- Enqueue: append at tail. O(1)
- Dequeue: remove from head. O(1)
- More flexible size
```

**Use Cases:**
- BFS on graphs
- Job scheduling
- Task queues (print queue, message queue)
- Buffering

### Deques (Double-Ended Queues)

```
Both ends allow O(1) insert/delete:
Front: add/remove here
  ↓
[..data..]
  ↑
Back: add/remove here

Real-world: std::deque, collections.deque
```

**Use Cases:**
- Monotonic queues
- Sliding window
- Bidirectional processing

---

## 🔍 BINARY SEARCH: Complete Model

### Classic Binary Search

```cpp
int binary_search(vector<int>& arr, int target) {
    int low = 0, high = arr.size();
    
    while (low < high) {
        int mid = low + (high - low) / 2;  // Avoid overflow
        
        if (arr[mid] == target)
            return mid;
        else if (arr[mid] < target)
            low = mid + 1;  // Target in [mid+1, high)
        else
            high = mid;     // Target in [low, mid)
    }
    return -1;  // Not found
}
```

**Invariant:** Target lies in [low, high) (or doesn't exist)

**Proof of Correctness:**
- Initially: [0, n) contains all elements
- Each iteration narrows to half-size range
- Terminates when low == high (one element left)
- If found during search, return immediately
- If loop exits without finding, target not in array

**Complexity:** O(log n) iterations (depth of binary tree)

### Binary Search Variants

| Variant | Find | Used For |
|---------|------|----------|
| First Occurrence | Leftmost element = target | First of many |
| Last Occurrence | Rightmost element = target | Last of many |
| Lower Bound | Leftmost element ≥ target | Insert point |
| Upper Bound | Leftmost element > target | Range queries |

**Example:** arr = [1,2,2,2,3], target = 2
- First: index 1
- Last: index 3
- Lower bound: 1
- Upper bound: 4

### Binary Search on Answer Space

**Pattern:** If property is monotone, binary search applies.

```
Example: Minimize maximum load
Problem: Distribute tasks among k servers to minimize max load

Solution:
1. Define search space: capacity C ∈ [1, sum of all tasks]
2. Feasibility check: Can we schedule all tasks with max capacity C?
   - Greedy: Assign to server with least current load
   - Return true if all fit
3. Binary search: Find minimum C where feasible(C) = true
```

**Why This Works:** If capacity C works, so does C+1 (monotone).

**Complexity:** O(log(max_C) × verification_cost)

**Examples:**
- **Minimum capacity:** Find minimum capacity to ship all packages in days D
- **Maximum minimum distance:** Place k cows in m stalls to maximize minimum distance
- **Minimum maximum height:** Build h heaps from n books to minimize max height

---

## ❌ Seven Misconceptions (Corrected)

1. **"Linked lists are always better for insertion"** → False. If you have random positions, finding position is O(n). Only beneficial if position is already known.

2. **"Array doubling wastes memory"** → False. Doubling strategy spreads cost over many inserts, giving O(1) amortized.

3. **"Binary search only works on arrays"** → False. Works on any monotone search space (feasibility functions, answer ranges).

4. **"Stack and queue implementations don't matter"** → False. Array-based is faster if size bounded, linked-list is better if unbounded.

5. **"Cache behavior doesn't matter for algorithmic complexity"** → False. Array of 1000 can be faster than linked list of 10, both O(n).

6. **"Lower bound and upper bound are the same"** → False. Lower bound finds first ≥, upper bound finds first >.

7. **"Amortized O(1) means every operation is O(1)"** → False. Occasional operations are O(n), but average over many is O(1).

---

## 🔗 Week 02 Connections

**From Week 01:**
- RAM model explains O(1) array access
- Asymptotics classify operations
- Memory layout and caches matter

**To Week 03:**
- Sorted arrays + binary search enables efficient searching
- Arrays are foundation for sorting algorithms
- Data structures used in hash tables, heaps

**To Week 04+:**
- Two-pointer technique uses arrays
- Sliding window uses arrays/deques
- Binary search as algorithmic pattern

**To Week 07+:**
- Trees generalize linked lists
- Graphs represented as arrays/linked lists
- BFS/DFS use queues/stacks

---

## ✅ Interview Readiness Checklist

1. **Instant choice:** Given operation frequencies, pick array or linked list
2. **Correct implementation:** Code binary search, stack, queue without errors
3. **Amortized analysis:** Explain why doubling gives O(1) amortized
4. **Edge cases:** Handle empty structures, single elements, duplicates
5. **Trade-offs:** Discuss time/space/implementation trade-offs
6. **Answer space binary search:** Recognize feasibility function and apply
7. **Systems awareness:** Understand cache, memory layout, pointer overhead

---

**Status:** Week 02 Summary Complete  
**Review Time:** 2-3 hours