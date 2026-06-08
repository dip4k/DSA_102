# 🗓️ Week 02 Problem-Solving Roadmap: Progression & Strategy

**Purpose:** Guide problem selection, daily progression, and selection mechanisms across 6 cohesive study days.

---

## 🎯 Overall 3-Stage Strategy

### Stage 1: Data Structure Mastery & Memory Foundations
**Goal:** Implement all core linear structures with precise pointer/index adjustments and no reference.  
**Time:** Days 1-3  
**Deliverables**: Static/Dynamic arrays, singly and doubly linked lists.

### Stage 2: Access Discipline & Logarithmic Searches
**Goal:** Restrict access structures to optimize operations and harness sorted array invariants.  
**Time:** Days 4-5  
**Deliverables**: Stacks, circular queue buffers, classical binary searches, and answer-space binary searches.

### Stage 3: Low-Level String/Number Configurations
**Goal:** Bridge abstraction gaps to master raw memory representations, encodings, and bidirectional conversions.  
**Time:** Day 6  
**Deliverables**: Strings Builders memory savings, signed integers offsets, atoi, and itoa implementations from scratch.

---

## 📊 Progression & Difficulty Tables

### 🧮 Arrays & Memory Layout Progression

| Level | Topic | Complexity | Operational Key Focus |
|-------|-------|-----------|------------------------|
| 1️⃣ | Static Array Offsets | O(1) Address Calculation | `Base + Index x Stride` |
| 2️⃣ | Cache Line Benchmarking | O(N) Sequential Scans | Stride 1 vs Stride 16 Cache Misses |
| 3️⃣ | Dynamic Doubling Push | O(1) Amortized | Capacity doubling vs linear increments |
| 4️⃣ | Lazy Shrinking Pop | O(1) Amortized | Preventing thrashing: shrink when length <= capacity/4 |
| 5️⃣ | 2D Row-Major Indexing| O(1) Traversal | Nested loops with fast columns transitions |

### 🔗 Linked List Progression

| Level | Topic | Complexity | Operational Key Focus |
|-------|-------|-----------|------------------------|
| 1️⃣ | Singly List Append | O(1) head, O(N) tail | Managing head pointer updates |
| 2️⃣ | In-Place List Reversal | O(N) | Three-pointer swap transitions (`prev`, `curr`, `next`) |
| 3️⃣ | Middle Node Search | O(N) | Fast (x2) & slow (x1) pointer runner pattern |
| 4️⃣ | Cycle Detection | O(N) | Floyd's cycle detection algorithm |
| 5️⃣ | Queue with Linked List | O(1) enqueue/dequeue | Constant-time tail pointer tracking |

### 📚 Stack/Queue Progression

| Level | Topic | Complexity | Operational Key Focus |
|-------|-------|-----------|------------------------|
| 1️⃣ | Stack (Array Backed) | O(1) Push/Pop | Top indices tracking |
| 2️⃣ | Circular Queue | O(1) Enqueue/Dequeue | Modular index wrap: `(back + 1) % capacity` |
| 3️⃣ | Valid Parentheses | O(N) | Parsing bracket balances |
| 4️⃣ | Min Stack | O(1) GetMin | Sub-tracking using auxiliary minimum stack |
| 5️⃣ | Monotonic Queue | O(N) | Maintaining active elements in sorted order |

### 🔍 Binary Search Progression

| Level | Topic | Complexity | Operational Key Focus |
|-------|-------|-----------|------------------------|
| 1️⃣ | Classic Binary Search | O(log N) | Midpoint index calculations overflow prevention |
| 2️⃣ | Leftmost Occurence | O(log N) | Find first matched duplicates |
| 3️⃣ | Rightmost Occurence | O(log N) | Find last matched duplicates |
| 4️⃣ | Bound Ranges | O(log N) | Lower bound (>=) and Upper bound (>) tracking |
| 5️⃣ | Rotated Array Search | O(log N) | Discontinuity checks |
| 6️⃣ | Binary Search on Answer | O(log(range) x N) | Feasibility checkers with monotonic criteria |

### 💾 Conversions & Immutability Progression

| Level | Topic | Complexity | Operational Key Focus |
|-------|-------|-----------|------------------------|
| 1️⃣ | Unicode surrogate pairs | O(1) | Variable characters widths |
| 2️⃣ | StringBuilder loops | O(N) | Bypassing string immutability heap waste |
| 3️⃣ | Two's Complement Swaps | O(1) | Register bits inversion negation plus one |
| 4️⃣ | atoi Parsing | O(N) | Whitespace skip, sign select, digit extraction, clamp |
| 5️⃣ | itoa Printing | O(N) | Rightmost modulo extraction, reverse, sign prepend |

---

## 🧭 Algorithm Selection Matrix

```
Are you processing elements in a FIFO manner?
   ↓
 [Yes] ────> Use Queue (implemented with circular array buffer or list)
   ↓
 [No]  ────> Need LIFO state reversals/undo paths?
               ↓
             [Yes] ────> Use Stack
               ↓
             [No]  ────> Need instant O(1) random index access?
                           ↓
                         [Yes] ────> Use Array / Dynamic Array
                           ↓
                         [No]  ────> Need O(1) mid-inserts at known reference points?
                                       ↓
                                     [Yes] ────> Use Linked List
                                       ↓
                                     [No]  ────> Search space sorted or monotonic?
                                                   ↓
                                                 [Yes] ────> Apply Binary Search
```
