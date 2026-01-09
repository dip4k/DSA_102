# 📊 WEEK 02 VISUAL CONCEPTS PLAYBOOK (HYBRID)

**Week:** 2 | **Tier:** Foundations II – Linear Structures, Binary Search  
**Theme:** Static/Dynamic Arrays, Linked Lists, Stacks/Queues/Deques, Binary Search Invariants  
**Format:** Hybrid (Enhanced ASCII + Web Resource Links + Reference Tools)  
**Purpose:** Visual-first concept explanation with embedded professional resources

---

## 🎨 VISUAL LEGEND & RESOURCE GUIDE

### Symbol Reference
| Symbol | Meaning |
|--------|---------|
| `[n]` | Array element or cell |
| `→` | Pointer or next reference |
| `i`, `j` | Index pointers |
| `lo`, `hi`, `mid` | Binary search bounds |
| `┌─┐` | Node or memory cell |
| `███` | Allocated/active memory |
| `░░░` | Unallocated/empty |
| `⇄` | Operation or transition |
| `✓` | Valid state |
| `✗` | Invalid state |
| 🔗 | Link to interactive visualization |

### Professional Visualization Resources

| Tool | Resource | Best For |
|------|----------|----------|
| **VisuAlgo Arrays** | https://visualgo.net/en/list | Array/List visualizations |
| **VisuAlgo Binary Search** | https://visualgo.net/en/bst | Binary search trees + search |
| **Linked List Visualizer** | https://www.cs.usfca.edu/~galles/visualization/LinkedList.html | Linked list operations |
| **GeeksforGeeks Arrays** | https://www.geeksforgeeks.org/array-data-structure/ | Array concepts & problems |
| **GeeksforGeeks Linked Lists** | https://www.geeksforgeeks.org/linked-list-set-1-introduction/ | Linked list operations |
| **GeeksforGeeks Binary Search** | https://www.geeksforgeeks.org/binary-search/ | Binary search guide |

---

## 📅 DAY 1: STATIC ARRAYS & MEMORY LAYOUT

### Pattern Map: Array Family Tree

```
ARRAY STRUCTURES
├─ Static Arrays (Fixed Size)
│  ├─ Contiguous memory
│  ├─ O(1) random access
│  └─ O(n) for insert/delete
│
├─ Dynamic Arrays (Resizable)
│  ├─ Doubling strategy
│  ├─ Amortized O(1) append
│  └─ O(n) for reallocation
│
└─ Multi-Dimensional
   ├─ Row-major layout
   ├─ Column-major layout
   └─ Cache implications
```

---

### Pattern 1.1: Static Array Memory Layout

**Interactive Resource:** 🔗 [VisuAlgo Arrays](https://visualgo.net/en/list)

#### Visual 1: Contiguous Memory Representation

```
ARRAY: [10, 20, 30, 40, 50]
SIZE: 5 elements

MEMORY LAYOUT (Base Address = 1000):
─────────────────────────────────────

Address │ Value │ Index
────────┼───────┼──────
1000    │  10   │  [0]
1004    │  20   │  [1]
1008    │  30   │  [2]
1012    │  40   │  [3]
1016    │  50   │  [4]

(Each element: 4 bytes, contiguous)

INDEX TO ADDRESS FORMULA:
┌──────────────────────────────┐
│ address = base + index × size│
│                              │
│ For index 2:                 │
│ address = 1000 + 2×4 = 1008 ✓│
│ value at 1008 = 30 ✓         │
└──────────────────────────────┘

CACHE ADVANTAGE:
Sequential access (prefetch):
  Access [0] → [1] → [2]
  Same cache line, no misses!

Random access:
  Access [0] → [4] → [2]
  Different cache lines, misses

TIME: O(1) random access
SPACE: O(n) total
```

---

### Pattern 1.2: Row-Major vs Column-Major Layout

#### Visual 1: Matrix Memory Ordering

```
MATRIX (3×3):
┌─────────────┐
│ 1  2  3    │
│ 4  5  6    │
│ 7  8  9    │
└─────────────┘

ROW-MAJOR (C-style):
├─ Rows stored sequentially
├─ Memory: [1,2,3, 4,5,6, 7,8,9]
├─ Iterate rows for cache efficiency
└─ Formula: address = base + (row×cols + col)×size

COLUMN-MAJOR (Fortran-style):
├─ Columns stored sequentially
├─ Memory: [1,4,7, 2,5,8, 3,6,9]
├─ Iterate columns for cache efficiency
└─ Formula: address = base + (col×rows + row)×size

PERFORMANCE IMPACT:
┌──────────────────────────────────┐
│ Row-major traversal:            │
│ for row in range(n):            │
│   for col in range(m):          │
│     access matrix[row][col]  ✓  │
│ Sequential → Fast!              │
│                                  │
│ Column-major traversal (bad):   │
│ for row in range(n):            │
│   for col in range(m):          │
│     access matrix[row][col]  ✗  │
│ Random jumps → Slow!            │
└──────────────────────────────────┘
```

---

## 📈 DAY 2: DYNAMIC ARRAYS & AMORTIZED ANALYSIS

### Pattern Map: Dynamic Array Growth

```
DYNAMIC ARRAY PATTERNS
├─ Capacity vs Size
│  ├─ Logical size (elements)
│  ├─ Physical capacity (allocated)
│  └─ Load factor (size/capacity)
│
├─ Resize Strategy
│  ├─ Doubling (2×)
│  ├─ Linear growth (+ constant)
│  └─ Fibonacci growth
│
└─ Amortized Cost
   ├─ Average per operation
   ├─ Expensive reallocation rare
   └─ O(1) amortized append
```

---

### Pattern 2.1: Doubling Strategy & Reallocation

**Interactive Resource:** 🔗 [VisuAlgo Arrays - Resize](https://visualgo.net/en/list)

#### Visual 1: Capacity Growing Process

```
DYNAMIC ARRAY: Starting with []

OPERATION SEQUENCE (with doubling):
─────────────────────────────────

Step 1: Insert 10
[10]
Size: 1, Capacity: 1

Step 2: Insert 20
├─ Capacity full (1 == 1)
├─ Reallocate: capacity = 2×1 = 2
├─ Copy: [10, 20]
└─ Size: 2, Capacity: 2

Step 3: Insert 30
├─ Capacity full (2 == 2)
├─ Reallocate: capacity = 2×2 = 4
├─ Copy: [10, 20, 30, _, ]
└─ Size: 3, Capacity: 4

Step 4: Insert 40
├─ Room available (3 < 4)
├─ [10, 20, 30, 40]
└─ Size: 4, Capacity: 4

Step 5: Insert 50
├─ Capacity full (4 == 4)
├─ Reallocate: capacity = 2×4 = 8
├─ Copy: [10, 20, 30, 40, 50, _, _, _]
└─ Size: 5, Capacity: 8

GROWTH PATTERN:
Capacity: 1 → 2 → 4 → 8 → 16 → ...

COST ANALYSIS:
┌───────────────────────────────────┐
│ Insert sequence [1..n]:          │
│                                  │
│ Cheap inserts: 1+2+3+4+...+(n-1) │
│ = O(n) total cheap work          │
│                                  │
│ Expensive reallocations:         │
│ Realloc at 1: copy 0 items      │
│ Realloc at 2: copy 1 item       │
│ Realloc at 4: copy 2 items      │
│ Realloc at 8: copy 4 items      │
│ ...                              │
│ Total: 1+2+4+8+... < 2n = O(n)  │
│                                  │
│ Total cost: O(n) for n inserts   │
│ Amortized: O(n)/n = O(1) each!  │
└───────────────────────────────────┘

TIME: O(1) amortized per append
SPACE: O(n) at capacity
```

---

### Pattern 2.2: Amortized Cost Intuition

#### Visual 1: Accumulator Model

```
AMORTIZED ANALYSIS (Aggregate Method):

n=10 appends:
─────────────

Work done per operation:
Op 1: Push 1 item (1 unit)
Op 2: Push 1 item → REALLOCATE (1 item copy, 1 new)
Op 3: Push 1 item (1 unit)
Op 4: Push 1 item (1 unit)
Op 5: Push 1 item → REALLOCATE (3 items copy, 1 new)
...

Total work = 10 (cheap ops) + 8 (reallocation work) = 18 units
For n operations: Total < 3n units
Amortized cost: 3n / n = 3 = O(1) per operation

KEY INSIGHT:
┌──────────────────────────────────┐
│ Even though ONE operation       │
│ (reallocate) is expensive O(n),  │
│ it happens RARELY (log n times)  │
│                                  │
│ Average over many ops: O(1)     │
│ This is amortized analysis!     │
└──────────────────────────────────┘
```

---

## 🔗 DAY 3: LINKED LISTS

### Pattern Map: Linked List Variants

```
LINKED LIST PATTERNS
├─ Singly Linked List
│  ├─ One directional link
│  ├─ Forward traversal only
│  └─ O(n) search, O(1) insert/delete
│
├─ Doubly Linked List
│  ├─ Bidirectional links
│  ├─ Forward & backward traversal
│  └─ More memory, flexible
│
└─ Circular Linked List
   ├─ Last node points to first
   ├─ No null terminator
   └─ Use case: round-robin
```

---

### Pattern 3.1: Node Structure & Pointer Chaining

**Interactive Resource:** 🔗 [Linked List Visualizer](https://www.cs.usfca.edu/~galles/visualization/LinkedList.html)

#### Visual 1: Singly Linked List in Heap

```
LINKED LIST: [10] → [20] → [30] → null

MEMORY REPRESENTATION (Heap):
─────────────────────────────

┌──────────────────┐
│ Node 0 (10)      │
├──────────────────┤
│ value: 10        │
│ next: ──────────────┐
└──────────────────┘  │
                      ▼
┌──────────────────┐
│ Node 1 (20)      │
├──────────────────┤
│ value: 20        │
│ next: ──────────────┐
└──────────────────┘  │
                      ▼
┌──────────────────┐
│ Node 2 (30)      │
├──────────────────┤
│ value: 30        │
│ next: null       │
└──────────────────┘

TRAVERSAL (Starting at head):
─────────────────────────────

current = head (points to Node 0)
print(current.value)  → 10
current = current.next

current = Node 1
print(current.value)  → 20
current = current.next

current = Node 2
print(current.value)  → 30
current = current.next

current = null
STOP

OUTPUT: 10 → 20 → 30 ✓

OPERATIONS:
┌──────────────────────────────┐
│ Search: O(n) - must traverse │
│ Insert at head: O(1)         │
│ Insert after node: O(1)      │
│ Delete after node: O(1)      │
│ Delete by value: O(n)        │
│ Find middle: O(n)            │
└──────────────────────────────┘

VS ARRAY:
Array: O(1) search, O(n) insert
List: O(n) search, O(1) insert (at known node)
```

---

### Pattern 3.2: Insert at Head vs Middle

#### Visual 1: Pointer Manipulation

```
INSERT 15 AT HEAD:

BEFORE:
head ──→ [10] → [20] → [30]

CREATE NEW NODE:
new_node = Node(15)
new_node.next = head
head = new_node

AFTER:
head ──→ [15] ──→ [10] → [20] → [30]

TIME: O(1) constant!


INSERT 25 AFTER NODE [20]:

BEFORE:
[10] → [20] → [30]
       ↑
    given node

CREATE NEW NODE:
new_node = Node(25)
new_node.next = given_node.next  (points to [30])
given_node.next = new_node        (point to new node)

AFTER:
[10] → [20] → [25] → [30]
            ↑    ↑
         inserted

TIME: O(1) - just pointer changes!

KEY OPERATIONS:
┌─────────────────────────────────┐
│ Insertion at known node:        │
│ 1. Create new node              │
│ 2. new_node.next = node.next   │
│ 3. node.next = new_node        │
│ = O(1) - no shifting!          │
│                                │
│ Vs Array:                      │
│ Need to shift all elements     │
│ after insertion point = O(n)   │
└─────────────────────────────────┘
```

---

## 📚 DAY 4: STACKS, QUEUES & DEQUES

### Pattern Map: Linear Structures

```
STACK/QUEUE/DEQUE PATTERNS
├─ Stack (LIFO)
│  ├─ Last-In-First-Out
│  ├─ Push/Pop from end
│  └─ Use: DFS, undo/redo, parsing
│
├─ Queue (FIFO)
│  ├─ First-In-First-Out
│  ├─ Enqueue/Dequeue
│  └─ Use: BFS, task scheduling
│
└─ Deque (Double-Ended)
   ├─ Both ends operations
   ├─ Push/pop front & back
   └─ Use: Sliding window, rotate
```

---

### Pattern 4.1: Stack Operations (LIFO)

#### Visual 1: Push/Pop Storyboard

```
STACK OPERATIONS:
─────────────────

Initial: []

Push 10:
┌──┐
│10│ ← Top
└──┘

Push 20:
┌──┐
│20│ ← Top
├──┤
│10│
└──┘

Push 30:
┌──┐
│30│ ← Top
├──┤
│20│
├──┤
│10│
└──┘

Pop (remove 30):
┌──┐
│20│ ← Top
├──┤
│10│
└──┘

Pop (remove 20):
┌──┐
│10│ ← Top
└──┘

IMPLEMENTATION OPTIONS:
┌──────────────────────────────┐
│ Array-based Stack:          │
│ - items = []                │
│ - top = -1                  │
│ - Push: items[++top] = x    │
│ - Pop: return items[top--]  │
│ - O(1) amortized            │
│                             │
│ Linked List Stack:          │
│ - head = null               │
│ - Push: new node at head    │
│ - Pop: remove head          │
│ - O(1) guaranteed           │
└──────────────────────────────┘
```

---

### Pattern 4.2: Queue Operations (FIFO) & Circular Buffer

#### Visual 1: Enqueue/Dequeue with Circular Buffer

```
QUEUE (Array-based, Circular):
──────────────────────────────

Array: [_, _, _, _, _]  (capacity 5)
Front: 0, Back: 0 (empty)

ENQUEUE 10:
Array: [10, _, _, _, _]
Front: 0, Back: 1

ENQUEUE 20:
Array: [10, 20, _, _, _]
Front: 0, Back: 2

ENQUEUE 30:
Array: [10, 20, 30, _, _]
Front: 0, Back: 3

DEQUEUE (remove 10):
Array: [X, 20, 30, _, _]
Front: 1, Back: 3

DEQUEUE (remove 20):
Array: [X, X, 30, _, _]
Front: 2, Back: 3

ENQUEUE 40, 50, 60 (wrap around!):
Array: [60, X, 30, 40, 50]
Front: 2, Back: 1 (wrapped)

CIRCULAR CALCULATION:
Back = (Back + 1) % Capacity
Front = (Front + 1) % Capacity

BENEFIT:
Normal array queue:        Circular queue:
Dequeue shifts everything  Just move front pointer
O(n) expensive!            O(1) efficient!

TIME: O(1) for enqueue/dequeue
SPACE: O(n) for capacity items
```

---

### Pattern 4.3: Deque (Double-Ended Queue)

#### Visual 1: Deque Operations from Both Ends

```
DEQUE: [10, 20, 30, 40, 50]
        ↑                   ↑
      Front                Back

PUSH_BACK 60 (add to back):
[10, 20, 30, 40, 50, 60]

PUSH_FRONT 5 (add to front):
[5, 10, 20, 30, 40, 50, 60]

POP_BACK (remove 60):
[5, 10, 20, 30, 40, 50]

POP_FRONT (remove 5):
[10, 20, 30, 40, 50]

ACCESS_FRONT: 10
ACCESS_BACK: 50

IMPLEMENTATION:
┌──────────────────────────────────┐
│ Doubly Linked List:             │
│ - Head + Tail pointers          │
│ - All operations O(1)           │
│ - Extra memory (next + prev)    │
│                                  │
│ Circular Buffer Array:          │
│ - Front + Back indices          │
│ - All operations O(1)           │
│ - Efficient memory              │
└──────────────────────────────────┘

USE CASES:
┌──────────────────────────────────┐
│ Sliding window (add/remove)     │
│ LRU cache                        │
│ Undo/Redo systems              │
│ Scheduling (round-robin)        │
└──────────────────────────────────┘
```

---

## 🔍 DAY 5: BINARY SEARCH & INVARIANTS

### Pattern Map: Binary Search Variants

```
BINARY SEARCH PATTERNS
├─ Classic Search
│  ├─ Standard target find
│  ├─ First occurrence
│  └─ Last occurrence
│
├─ Bounded Search
│  ├─ Lower bound
│  ├─ Upper bound
│  └─ Range queries
│
└─ Answer Space Search
   ├─ Feasibility check
   ├─ Minimize/maximize
   └─ Continuous search
```

---

### Pattern 5.1: Invariant & Mid Calculation

**Interactive Resource:** 🔗 [VisuAlgo Binary Search](https://visualgo.net/en/bst)

#### Visual 1: Search Range Halving

```
BINARY SEARCH FOR 7 IN SORTED ARRAY:
─────────────────────────────────────

Array: [1, 3, 5, 7, 9, 11, 13]
Index: [0, 1, 2, 3, 4, 5,  6]

ITERATION 1:
lo=0, hi=6
mid = lo + (hi-lo)/2 = 0 + 3 = 3

Array[3] = 7 == TARGET ✓
FOUND at index 3!


ITERATION 1 (Not found immediately):
lo=0, hi=6
mid = 0 + 3 = 3
Array[3] = 7 < TARGET (looking for 9)

Target is to the right:
lo = mid + 1 = 4

ITERATION 2:
lo=4, hi=6
mid = 4 + (6-4)/2 = 4 + 1 = 5

Array[5] = 11 > TARGET (looking for 9)

Target is to the left:
hi = mid - 1 = 4

ITERATION 3:
lo=4, hi=4
mid = 4 + (4-4)/2 = 4

Array[4] = 9 == TARGET ✓
FOUND at index 4!


INVARIANT MAINTAINED:
┌────────────────────────────────┐
│ After each iteration:         │
│ Target (if exists) is in     │
│ [lo, hi]                     │
│                              │
│ Search space halves each time│
│ = O(log n) iterations max    │
└────────────────────────────────┘

SAFE MID CALCULATION:
❌ WRONG: mid = (lo + hi) / 2
   Can overflow if lo + hi > MAX_INT

✓ CORRECT: mid = lo + (hi - lo) / 2
   Prevents overflow safely
```

---

### Pattern 5.2: First & Last Occurrence

#### Visual 1: Find Boundaries

```
FIND FIRST OCCURRENCE OF 5:
Array: [1, 3, 5, 5, 5, 7, 9]
Index: [0, 1, 2, 3, 4, 5, 6]

Standard binary search finds ANY 5 (e.g., index 3).
We want the LEFTMOST (index 2).

TEMPLATE (First Occurrence):
────────────────────────────

lo = 0, hi = 6, result = -1

ITERATION 1:
mid = 3, Array[3] = 5 == target
├─ Found a match! Record it: result = 3
├─ But check if leftmost: search left half
└─ hi = mid - 1 = 2

ITERATION 2:
mid = 1, Array[1] = 3 < target
└─ lo = mid + 1 = 2

ITERATION 3:
mid = 2, Array[2] = 5 == target
├─ Found! Record it: result = 2
├─ Check left again (might be more)
└─ hi = mid - 1 = 1

ITERATION 4:
lo = 2, hi = 1
lo > hi, STOP

RESULT: Index 2 (LEFTMOST occurrence) ✓


FIND LAST OCCURRENCE OF 5:
────────────────────────

TEMPLATE (Last Occurrence):
Always move to check right half when found

mid = 3, Array[3] = 5 == target
├─ Found! Record: result = 3
├─ Check if rightmost: search right half
└─ lo = mid + 1 = 4

Continue until lo > hi

RESULT: Index 4 (RIGHTMOST occurrence) ✓


TEMPLATE DIFFERENCE:
┌─────────────────────────────┐
│ First occurrence:          │
│ if (arr[mid] >= target)    │
│   hi = mid - 1  (go left) │
│                            │
│ Last occurrence:           │
│ if (arr[mid] <= target)    │
│   lo = mid + 1  (go right)│
└─────────────────────────────┘
```

---

### Common Failure Modes (Day 5)

#### Failure 1: Infinite Loop from Bad Update

```
❌ WRONG:
while lo <= hi:
  mid = (lo + hi) / 2
  if arr[mid] == target:
    return mid
  elif arr[mid] < target:
    lo = mid  ← NO PROGRESS! lo stays same

Result: Infinite loop if mid = lo!

✓ CORRECT:
elif arr[mid] < target:
  lo = mid + 1  ← Always advance lo

Result: Guaranteed progress, terminates
```

#### Failure 2: Off-by-One in Boundaries

```
❌ WRONG (checking last element):
while lo < hi:  ← Stops before last element!
  mid = lo + (hi - lo) / 2
  ...

Result: Never checks if arr[n-1] is target!

✓ CORRECT:
while lo <= hi:  ← Includes last element

Result: All elements checked
```

---

## 🎯 WEEK 02 VISUAL SUMMARY TABLE

```
┌─────────────────────────────────────────────────────┐
│ DAY │ TOPIC         │ Complexity    │ Key Feature │
├─────────────────────────────────────────────────────┤
│ 1   │ Static Arrays │ O(1) access   │ Contiguous  │
│     │ Memory Layout │ O(n) insert   │ memory      │
│     │               │ O(1) space    │             │
│     │               │               │             │
│ 2   │ Dynamic Array │ O(1) amortiz. │ Doubling    │
│     │ Amortized     │ O(n) reallocate│ strategy   │
│     │ Analysis      │ O(n) capacity │             │
│     │               │               │             │
│ 3   │ Linked Lists  │ O(n) search   │ Pointer     │
│     │ Pointer Chain │ O(1) insert   │ chaining    │
│     │               │ O(n) space    │ at head     │
│     │               │               │             │
│ 4   │ Stack/Queue   │ O(1) ops      │ LIFO/FIFO   │
│     │ Deques        │ O(1) space    │ circular    │
│     │               │               │ buffer      │
│     │               │               │             │
│ 5   │ Binary Search │ O(log n)      │ Invariant   │
│     │ Invariants    │ O(1) space    │ halving     │
│     │               │               │             │
└─────────────────────────────────────────────────────┘
```

---

## 📋 COMMON PATTERNS QUICK REFERENCE

```
Structure           │ Access │ Insert │ Delete │ Space │ Use Case
────────────────────┼────────┼────────┼────────┼───────┼──────────
Array (Static)      │ O(1)   │ O(n)   │ O(n)   │ O(n)  │ Fixed
Array (Dynamic)     │ O(1)   │ O(1)* │ O(n)   │ O(n)  │ Growing
Linked List         │ O(n)   │ O(1)† │ O(1)† │ O(n)  │ Insert/Del
Stack               │ Top    │ O(1)   │ O(1)   │ O(n)  │ LIFO
Queue               │ Front  │ O(1)   │ O(1)   │ O(n)  │ FIFO
Deque               │ Both   │ O(1)   │ O(1)   │ O(n)  │ Flexible
Binary Search Tree  │ O(logn)│ O(logn)│ O(logn)│ O(n)  │ Ordered

* Amortized  † At known node position
```

---

## 🔗 RECOMMENDED LEARNING RESOURCES

### Interactive Visualizations
1. **VisuAlgo Arrays** (https://visualgo.net/en/list) — Array/List operations
2. **VisuAlgo Binary Search** (https://visualgo.net/en/bst) — Search and traversal
3. **Linked List Visualizer** (https://www.cs.usfca.edu/~galles/visualization/LinkedList.html) — Node operations
4. **GeeksforGeeks Arrays** (https://www.geeksforgeeks.org/array-data-structure/) — Array reference
5. **GeeksforGeeks Linked Lists** (https://www.geeksforgeeks.org/linked-list-set-1-introduction/) — List operations
6. **GeeksforGeeks Binary Search** (https://www.geeksforgeeks.org/binary-search/) — Search patterns

### Video Tutorials
- "Arrays vs Linked Lists" — Trade-offs and when to use each
- "Binary Search Explained" — Invariant-based thinking
- "Stack and Queue" — LIFO/FIFO operations visualized

---

## 📝 HOW TO USE THIS PLAYBOOK

### Quick Revision (30 mins)
1. Scan pattern maps (5 mins)
2. Read one day's main visuals (5 mins per day)
3. Answer mini quiz (3 mins)
4. Review failure modes (2 mins)

### Deep Learning (2-3 hours)
1. Read playbook + extended subtopics
2. Visit web resource links for interactive animations
3. Implement each data structure yourself
4. Trace operations using playbook visuals

### Interview Prep (1 hour)
1. Quick reference table for complexity
2. Review failure modes
3. Mentally implement each structure
4. Code without looking at reference

---

**Version:** 1.0 Hybrid Approach | **Generated:** Friday, January 09, 2026, 1:24 AM IST  
**System:** v12 Visual Concepts Framework + Web Resources  
**Status:** ✅ PRODUCTION-READY WITH EMBEDDED REFERENCES

**Use web resource links for interactive visualizations while studying!**

