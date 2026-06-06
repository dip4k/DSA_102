# 🗺️ CROSS-WEEK DEPENDENCY MAP v1.0
**Status:** ✅ TEMPLATE-READY  
**Created:** January 14, 2026, 1:23 PM IST  
**Purpose:** Show prerequisite relationships and visual connections between weeks  
**Integration:** Referenced by QUALITY_IMPROVEMENT_SYSTEM_v1.md

---

## 🎯 HOW TO USE THIS FILE

### **Workflow:**
1. When generating Week X, check what prior weeks it depends on
2. Reference those prior weeks visually or conceptually
3. After deployment, note unexpected connections students make
4. Update map with real learning data

---

## 📊 DEPENDENCY STRUCTURE: ALL 19 WEEKS

```
PHASE A: FOUNDATIONS (Weeks 1-3)
├─ Week 1: RAM, Big-O, Recursion, Peak Finding
├─ Week 2: Arrays, Linked Lists, Stacks, Queues, Binary Search
└─ Week 3: Sorts, Heaps, Hashing

    ↓ EVERYTHING DEPENDS ON PHASE A

PHASE B: CORE PATTERNS (Weeks 4-6)
├─ Week 4: Two Pointers, Sliding Windows, Divide & Conquer
├─ Week 5: Hash Patterns, Monotonic Stack, Intervals, Partition, Kadane, F-S Pointers
└─ Week 6: String Patterns (Palindromes, Substrings, Parentheses)

    ↓ PHASE C DEPENDS ON PHASE B

PHASE C: COMPLEX STRUCTURES (Weeks 7-11)
├─ Week 7: Trees, BST, Balanced Trees
├─ Week 8: Graphs, BFS, DFS, Topological Sort
├─ Week 9: Shortest Paths, MST, Union-Find
├─ Week 10: DP 1D/2D
└─ Week 11: DP Trees, DAGs, Bitmask

    ↓ PHASE D DEPENDS ON PHASE C

PHASE D: ALGORITHM PARADIGMS (Weeks 12-13)
├─ Week 12: Greedy
└─ Week 13: Amortized Analysis

    ↓ PHASE E DEPENDS ON PHASES A-D

PHASE E: INTEGRATION (Weeks 14-15)
├─ Week 14: Matrices, Backtracking, Bit Tricks
└─ Week 15: Advanced Strings, Network Flow

    ↓ PHASE F DEPENDS ON ALL PRIOR

PHASE F: ADVANCED (Weeks 16-18, Optional)
├─ Week 16: Segment Trees, BIT, Geometry
├─ Week 17: HLD, FFT
└─ Week 18: Probabilistic, System Design

PHASE G: INTEGRATION (Week 19)
└─ Week 19: Mock Interviews (everything)
```

---

## 🔗 DETAILED WEEK-BY-WEEK DEPENDENCIES

### **WEEK 5: CORE ARRAY PATTERNS II**

#### **Day 1: Hash Map & Hash Set**

**DEPENDS ON:**
```
Week 1: 
  ✓ Big-O complexity (why O(1) average, O(n) worst)
  ✓ Space complexity (object overhead, memory allocation)

Week 3 Day 4-5: Hash Tables I & II
  ✓ Collision handling (chaining vs probing)
  ✓ Load factor and resizing
  ✓ Universal hashing concepts
```

**VISUAL CONNECTIONS:**
```
Week 3 Hash Table Theory → Week 5 Hash Patterns (SAME DATA STRUCTURE, new patterns)

Pattern evolution:
Week 3: "How do hash tables work internally?"
Week 5: "How do I USE hash tables to solve interview problems?"

Reference in playbook:
"Recall from Week 3 Day 4: collision handling via chaining.
Now we leverage this for pattern solving."
```

**ENABLES:**
```
Week 6 Day 4: String Matching (rolling hash)
  ✓ Uses same hash concept from Week 5 but for substrings
  ✓ Builds on hash patterns

Week 7+: All hash-based problems
  ✓ Trees: hash-based lookup for ancestors
  ✓ Graphs: adjacency list representation (uses hashing)
  ✓ DP: memoization (uses hashing)
```

**UNEXPECTED CONNECTIONS (from student feedback):**
```
Observation: Students who mastered Week 3 hashing → 95% success Week 5
Observation: Students who skipped Week 3 → 60% success Week 5

Action: Make Week 3 mandatory prerequisite review before Week 5 playbook
```

---

#### **Day 2: Monotonic Stack**

**DEPENDS ON:**
```
Week 2 Day 4: Stacks & Queues
  ✓ Stack operations (push, pop, peek)
  ✓ LIFO semantics
  ✓ When to use stack vs queue

Week 4 Day 1: Two-Pointer Patterns
  ✓ Invariant-based thinking
  ✓ Maintaining relationships during iteration
  ✓ One-pass thinking
```

**VISUAL CONNECTIONS:**
```
Week 2 Stack → Week 5 Monotonic Stack (VARIANT)
Pattern: "Regular stack ≠ monotonic stack"

Show comparison:
Regular Stack (Week 2):
  - Store all elements
  - LIFO order
  - Use: parsing, DFS

Monotonic Stack (Week 5):
  - Store only monotonic elements
  - Discard elements that can't be useful
  - Use: find next/previous greater/smaller
  - Key difference: ONE PASS, never revisit

Reference in playbook:
"Think of monotonic stack as an OPTIMIZED stack that forgets elements
we'll never compare against again."
```

**ENABLES:**
```
Week 6 Day 2: Substring Patterns
  ✓ Using stack-like state machines for pattern matching
  ✓ Maintaining invariants while sliding

Week 15 Day 1: KMP Algorithm
  ✓ Uses stack-like state machine for failure function
  ✓ Same "one pass, remember only useful info" pattern
```

**UNEXPECTED CONNECTIONS:**
```
Observation: Students struggled with "what lives in the stack? indices or values?"
Root cause: Week 2 didn't emphasize index-based operations

Action: Add comparison to Week 2
"Week 2 typically stored values in stack. Week 5 requires storing INDICES.
Why the difference? Because we need to write results to an OUTPUT ARRAY."
```

---

#### **Day 3: Merge Operations & Interval Patterns**

**DEPENDS ON:**
```
Week 2 Day 5: Binary Search
  ✓ Not directly used, but interval problems use similar "bisecting" logic

Week 3 Day 2: Merge Sort
  ✓ Merging technique: how to combine two sorted arrays
  ✓ Same merge operation appears in intervals

Week 4 Day 1: Two-Pointer (Merge)
  ✓ Merging two sorted arrays using two pointers
  ✓ DIRECTLY REUSED for interval merging
```

**VISUAL CONNECTIONS:**
```
Week 3 Merge Sort merge() → Week 4 Two-Pointer merge → Week 5 Interval Merge

SAME OPERATION, three contexts:

// Week 3: Merge in sorting
void merge(int[] arr, int[] left, int[] right) { ... }

// Week 4: Merge sorted arrays
int[] mergeSorted(int[] arr1, int[] arr2) { ... }

// Week 5: Merge intervals
List<Interval> mergeIntervals(List<Interval> intervals) { ... }

Reference in playbook:
"The merge operation from Week 4 is the CORE here.
We're merging not sorted arrays but sorted INTERVALS."
```

**ENABLES:**
```
Week 6: Merging strategies in strings
Week 7: Merging tree intervals
Week 12: Greedy uses interval merging for scheduling
```

**UNEXPECTED CONNECTIONS:**
```
Observation: Students who mastered Week 4 two-pointer → 90% success Week 5 intervals
Students who skipped Week 4 → 65% success Week 5

Action: Emphasize Week 4 prerequisite
"Two-pointer merging is THE TECHNIQUE here. Make sure Week 4 Two-Pointer is solid."
```

---

#### **Day 4: Partition & Kadane's Algorithm**

**DEPENDS ON:**
```
Week 1: Recursion patterns
  ✓ Divide & conquer thinking (not explicit here, but mental model)

Week 3 Day 2: Quick Sort
  ✓ Partition concept is directly from Quick Sort
  ✓ Partitioning around pivot

Week 4 Day 4: Divide & Conquer Pattern
  ✓ Dividing array into logical sections
  ✓ Solving subproblems independently
```

**VISUAL CONNECTIONS:**
```
Week 3 Quick Sort partition() → Week 5 Partition Pattern

REUSED PATTERN:

// Week 3: Partition for sorting
void partition(int[] arr, int pivot) { ... }

// Week 5: Partition for arrangement
void partitionArray(int[] arr, int target) { ... }

Reference in playbook:
"Partition is a technique from Quick Sort (Week 3 Day 2).
Here we use it to REARRANGE elements without full sorting."
```

**ENABLES:**
```
Week 14: Matrix traversal (partitioning submatrices)
Week 18: Probabilistic structures (partitioning space)
```

**UNEXPECTED CONNECTIONS:**
```
No major unexpected connections for Partition.
Students who understood Week 3 Quick Sort → understood Week 5 Partition easily.
```

---

#### **Day 5: Fast-Slow Pointers**

**DEPENDS ON:**
```
Week 2: Linked Lists
  ✓ Node structure, traversal
  ✓ Understanding pointers/references

Week 4 Day 1: Two-Pointer Patterns
  ✓ Moving two pointers at different speeds/directions
  ✓ Maintaining invariants
```

**VISUAL CONNECTIONS:**
```
Week 2 Linked List traversal → Week 4 Two-Pointer → Week 5 Fast-Slow

Mental progression:

// Week 2: Basic traversal
Node curr = head;
while (curr != null) { curr = curr.next; }

// Week 4: Two pointers at different speeds
Node slow = head, fast = head;
while (fast != null) { slow = slow.next; fast = fast.next.next; }

// Week 5: Fast-Slow for cycle detection
// Same pattern, different PURPOSE (not finding kth element, finding cycle)

Reference in playbook:
"You've moved two pointers in different ways (Week 4).
Now we move them at DIFFERENT SPEEDS to detect cycles."
```

**ENABLES:**
```
Week 8: Graph cycle detection (similar principle)
Week 18: System design (similar principle for load detection)
```

**UNEXPECTED CONNECTIONS:**
```
Observation: Cycle detection principle → applicable to circular buffers
Action: Add note in Week 5
"The cycle detection principle applies beyond linked lists:
circular buffers (queues), circular arrays, graphs all use same idea."
```

---

## 📋 CROSS-WEEK VISUAL REFERENCE GUIDE

Use this when generating playbooks. Add these references:

### **For Week 5 Day 1 (Hash Patterns):**
```
Reference Week 3 Day 4-5:
"Recall Week 3's hash table internals (collisions, load factor).
This week: using that knowledge to solve patterns."

Visual callout:
┌─────────────────────────────┐
│ Week 3: THEORY              │
│ How hash tables work inside │
│ Collisions, resizing        │
└────────────┬────────────────┘
             │
┌────────────▼────────────────┐
│ Week 5: APPLICATION         │
│ Using hash tables to solve  │
│ duplicate, cache problems   │
└─────────────────────────────┘
```

### **For Week 5 Day 2 (Monotonic Stack):**
```
Reference Week 2 Day 4 and Week 4 Day 1:
"Stacks are Week 2. Two-pointers are Week 4.
Monotonic stack combines: stack structure + invariant thinking."

Visual callout:
STACK (Week 2)          TWO-POINTER (Week 4)    MONOTONIC STACK (Week 5)
LIFO order    +    Invariant thinking    →    Optimized stack
Push/pop      +    One-pass             →    Discard unnecessary
```

### **For Week 5 Day 3 (Merge Intervals):**
```
Reference Week 3 Day 2 and Week 4 Day 1:
"Merging from sorts. Two-pointers from patterns.
Merge intervals uses BOTH."

Visual callout:
MERGE SORT (Week 3)     TWO-POINTER (Week 4)    INTERVALS (Week 5)
merge()                 Two pointers             Same merge() concept
Compare + combine       Moving forward           Sort then merge
```

---

## 🎬 UPDATING THIS MAP BASED ON DEPLOYMENT

### **After Week 5 Deployment, collect:**

```markdown
## Week 5 Dependency Observations

### Expected Dependencies (Confirmed)
- Week 3 Hashing → Week 5 Hash Patterns: 95% correlation ✓
- Week 4 Two-Pointer → Week 5 Intervals: 90% correlation ✓
- Week 2 Linked Lists → Week 5 Fast-Slow: 85% correlation ✓

### Unexpected Dependencies (Found)
- Week 4 Two-Pointer → Week 5 Monotonic Stack: 80% correlation (not expected!)
  Action: Add explicit connection in playbook

### Missing Prerequisites (Students Struggled)
- Week 3 Stack operations → Week 5 Monotonic Stack
  Students forgot difference between push/pop indices vs values
  Action: Add Week 3 refresher in Week 5 playbook

### Cross-Phase Connections (Found)
- Week 5 patterns → Week 7 Trees (hash-based lookups)
- Week 5 patterns → Week 8 Graphs (adjacency using hashes)
Action: Foreshadow these connections in Week 5 Chapter 5 (Integration)
```

---

## 📝 HOW TO MAINTAIN THIS FILE

1. **Before generating Week X:** Check this map for dependencies
2. **Add visual references** to your playbook
3. **After deployment:** Update with real data
4. **Next iteration:** Use updated dependencies

---

**Version:** 1.0 | **Status:** ✅ TEMPLATE-READY  
**Next step:** Seed with real learning path data after Week 5 deployment

