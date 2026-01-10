# 🗓️ Week 02 Problem-Solving Roadmap: Progression & Strategy

**Status:** Training Coach Resource  
**Purpose:** Guide problem selection and progression

---

## 🎯 Overall 3-Stage Strategy

### Stage 1: Data Structure Mastery (Foundation)
**Goal:** Implement all core structures from scratch  
**Time:** Days 1-3  
**Outcome:** Code arrays, linked lists, stack, queue without reference

**Implementations:**
- [ ] Static array with index-to-address formula
- [ ] Dynamic array with doubling growth
- [ ] Singly linked list (insert, delete, reverse)
- [ ] Stack (array-based)
- [ ] Queue (circular buffer)
- [ ] Binary search (classic)

### Stage 2: Trade-Off Analysis (Deep Understanding)
**Goal:** Compare structures on various criteria  
**Time:** Days 3-4  
**Outcome:** Choose structure for constraints, justify decision

**Comparisons:**
- Array vs linked list (access, insertion, locality)
- Circular buffer vs linked list queue
- First vs last occurrence binary search
- Answer space binary search vs iteration

### Stage 3: Real-World Application (Mastery+)
**Goal:** Solve interview problems using structures  
**Time:** Day 5  
**Outcome:** Recognize when each structure is useful

---

## 📊 Progression Tables

### Arrays Progression

| Level | Topic | Complexity | Focus |
|-------|-------|-----------|-------|
| 1️⃣ | Static array access | O(1) | Address formula |
| 2️⃣ | Dynamic array append | O(1) amortized | Doubling |
| 3️⃣ | Insert at position | O(n) | Shifting |
| 4️⃣ | Amortized analysis | O(1) avg | Geometric series |
| 5️⃣ | 2D array indexing | O(1) | Row/column-major |

### Linked List Progression

| Level | Topic | Complexity | Focus |
|-------|-------|-----------|-------|
| 1️⃣ | Singly list basic | O(1) head | Node structure |
| 2️⃣ | Reverse linked list | O(n) | Pointer manipulation |
| 3️⃣ | Find middle | O(n) | Two pointers |
| 4️⃣ | Detect cycle | O(n) | Floyd's algorithm |
| 5️⃣ | Merge sorted lists | O(n+m) | Pointer updates |

### Stack/Queue Progression

| Level | Topic | Complexity | Focus |
|-------|-------|-----------|-------|
| 1️⃣ | Stack array | O(1) ops | top pointer |
| 2️⃣ | Queue circular | O(1) ops | front/back wrap |
| 3️⃣ | Min stack | O(1) get-min | Auxiliary stack |
| 4️⃣ | Deque | O(1) both ends | Both pointers |
| 5️⃣ | Monotonic deque | O(n) | Ordering invariant |

### Binary Search Progression

| Level | Topic | Complexity | Focus |
|-------|-------|-----------|-------|
| 1️⃣ | Classic search | O(log n) | [low, high) invariant |
| 2️⃣ | First occurrence | O(log n) | Leftmost |
| 3️⃣ | Last occurrence | O(log n) | Rightmost |
| 4️⃣ | Lower/upper bound | O(log n) | Boundary definitions |
| 5️⃣ | Rotated array | O(log n) | Detect and recurse |
| 6️⃣ | Answer space | O(log × verify) | Feasibility function |

---

## 🚫 Common Problem-Solving Pitfalls

### Pitfall 1: Off-by-One in Binary Search

**Problem:** `while (low <= high)` instead of `while (low < high)`

**Fix:** Maintain [low, high) invariant consistently.

---

### Pitfall 2: Pointer Aliasing in Linked Lists

**Problem:** Using old pointers after reallocation or deletion

**Fix:** Update all pointers when structure changes.

---

### Pitfall 3: Ignoring Amortized Complexity

**Problem:** "Dynamic array is O(n) per insert, bad for performance"

**Reality:** O(1) amortized—rare expensive operations spread over many cheap ones.

**Fix:** Understand geometric growth.

---

### Pitfall 4: Wrong Data Structure for Task

**Problem:** Using linked list for frequent random access

**Fix:** Analyze operation frequencies before choosing.

---

### Pitfall 5: Not Recognizing Answer Space Binary Search

**Problem:** Trying to compute optimal value directly

**Fix:** Check if monotone condition exists, binary search.

---

### Pitfall 6: Incorrect Circular Buffer Wraparound

**Problem:** `index = (index + 1) % n` logic errors

**Fix:** Test wraparound at boundaries, verify with small examples.

---

### Pitfall 7: Missing Edge Cases in Binary Search

**Problem:** Not testing single element, not found, duplicates

**Fix:** Test boundary cases before submitting.

---

## 🔄 Algorithm Selection Matrix

```
Choosing the Right Structure:

Need random access? → Array O(1)
Need frequent insertion? → Linked list O(1) at position
Need both? → Advanced (B-tree, skip list)

Need find in sorted? → Binary search O(log n)
Need find in unsorted? → Hash or linear search
Need optimization? → Binary search on answer space

LIFO needed? → Stack
FIFO needed? → Queue
Both? → Deque

Processing order matters? → Queue/stack
Order doesn't matter? → Set/hash
```

---

## ✅ Per-Structure Study Checklist

### Arrays
- [ ] Understand address formula
- [ ] Implement dynamic array with doubling
- [ ] Calculate amortized cost
- [ ] Design 2D array layout
- [ ] Explain cache implications

### Linked Lists
- [ ] Implement singly list
- [ ] Reverse without extra space
- [ ] Find middle (two pointers)
- [ ] Detect cycles
- [ ] Merge sorted lists

### Stacks
- [ ] Array-based implementation
- [ ] Linked list-based implementation
- [ ] Min stack with auxiliary
- [ ] Expression evaluation
- [ ] Parentheses matching

### Queues
- [ ] Circular buffer queue
- [ ] Linked list queue
- [ ] Deque with both pointers
- [ ] Monotonic queue concept
- [ ] BFS preparation

### Binary Search
- [ ] Classic binary search
- [ ] First/last occurrence
- [ ] Lower/upper bounds
- [ ] Answer space optimization
- [ ] Rotated array variant

---

## 📝 Weekly Problem Milestones

**After Day 1:** Implement dynamic array, understand doubling

**After Day 2:** Implement singly linked list, reverse, merge

**After Day 3:** Implement stack, queue, deque

**After Day 4-5:** 10-15 binary search problems, answer space variant

**Integration:** 3-5 problems mixing structures

---

## 🏁 Interview Problems by Difficulty

| Difficulty | Topics | Example |
|-----------|--------|---------|
| Easy | Array access, stack basics | Valid parentheses |
| Medium | Linked list ops, binary search | Reverse linked list |
| Medium | Amortized analysis, choice | Design LRU cache |
| Hard | Answer space binary search | Minimize max load |
| Hard | Linked list combinations | Merge k lists |
| Hard | Binary search variants | Median of 2 arrays |

---

**Practice Time:** 15-18 hours for 20-25 problems