# 🗓️ Week 01 Problem-Solving Roadmap: Structured Practice & Progression

**Purpose:** Guide problem selection, progression, and pattern recognition

---

## 🎯 Overall 3-Stage Strategy

### Stage 1: Concept Mastery (Foundation)
**Goal:** Understand all five major concept areas deeply  
**Time:** Days 1-5  
**Outcome:** You explain memory, Big-O, recursion, memoization from first principles

**Concepts to Master:**
- [ ] Memory: address spaces, pointers, stack, heap, virtual memory
- [ ] Complexity: Big-O, Big-Ω, Big-Θ, recurrence analysis
- [ ] Space: Stack depth, heap allocation, time-space trade-offs
- [ ] Recursion: Patterns, call stack, overlapping subproblems
- [ ] Memoization: Caching strategy, when to apply

### Stage 2: Algorithmic Thinking (Deep Understanding)
**Goal:** Design and analyze algorithms  
**Time:** Days 5-6  
**Outcome:** You solve peak finding and similar problems

**Problem Types:**
- Peak finding (1D and 2D)
- Divide-and-conquer problems
- Recognize structure and exploit it

### Stage 3: Systems Awareness (Mastery+)
**Goal:** Connect algorithms to real hardware  
**Time:** Day 6  
**Outcome:** You reason about cache, memory layout, real-world performance

---

## 📊 Detailed Progression Tables

### Memory & Pointers Progression

| Level | Topic | Focus |
|-------|-------|-------|
| 1️⃣ | Address spaces | Code, data, heap, stack |
| 2️⃣ | Pointers | Addresses, dereferencing, null |
| 3️⃣ | Stack frames | Push/pop, function calls |
| 4️⃣ | Virtual memory | Pages, TLB, translation |
| 5️⃣ | Cache hierarchy | Lines, locality, latencies |
| 6️⃣ | False sharing | Multi-threading concerns |

### Complexity Analysis Progression

| Level | Topic | Focus |
|-------|-------|-------|
| 1️⃣ | Big-O notation | Upper bound, intuition |
| 2️⃣ | Complexity classes | List and order by growth |
| 3️⃣ | Recurrence trees | Visual analysis method |
| 4️⃣ | Simple recurrences | T(n)=T(n/2)+1, T(n)=2T(n/2)+n |
| 5️⃣ | Complex recurrences | Arbitrary branching, combining |

### Space Complexity Progression

| Level | Topic | Focus |
|-------|-------|-------|
| 1️⃣ | Stack frames | Activation records |
| 2️⃣ | Stack depth | Recursion depth limits |
| 3️⃣ | Heap allocation | malloc, fragmentation |
| 4️⃣ | Object overhead | Headers, metadata, capacity |
| 5️⃣ | Time-space trade-offs | Memoization, precomputation |

### Recursion Progression

| Level | Topic | Focus |
|-------|-------|-------|
| 1️⃣ | Call stack basics | Frames, push/pop |
| 2️⃣ | Simple recursion | Factorial, sum, fibonacci |
| 3️⃣ | Recursion trees | Visualize call structure |
| 4️⃣ | Exponential blowup | Overlapping subproblems |
| 5️⃣ | Memoization | Convert O(2^n) to O(n) |
| 6️⃣ | Complex recursion | Divide-and-conquer |

### Peak Finding Progression

| Level | Topic | Focus |
|-------|-------|-------|
| 1️⃣ | Problem definition | 1D peak with examples |
| 2️⃣ | Brute force | O(n) scan approach |
| 3️⃣ | Divide-conquer | O(log n) solution |
| 4️⃣ | 2D extension | Matrix peak finding |
| 5️⃣ | Proofs | Why the algorithm works |

---

## 🚫 Seven Common Problem-Solving Pitfalls

### Pitfall 1: Underestimating Impact of Constants

**Problem:** "O(n log n) is obviously better than O(n²)"

**Reality:** For n=100, O(n²) = 10,000 ops, O(n log n) ≈ 650 ops. For n=1000, O(n²) = 10^6, O(n log n) ≈ 10,000. But with different constants, curves cross!

**Fix:** Calculate actual operations for realistic input sizes.

---

### Pitfall 2: Assuming Virtual Memory is Free

**Problem:** "I don't need to think about page faults"

**Reality:** Page fault = 10ms disk I/O. Accessing one page-fault can cost as much as 1 million CPU cycles!

**Fix:** Understand contiguous memory, TLB behavior, working set.

---

### Pitfall 3: Ignoring Cache Behavior in Algorithm Design

**Problem:** "Array and linked list are both O(n), so they're equivalent"

**Reality:** Array: cache-friendly, prefetch, 10-100x faster. Linked list: pointer chasing, TLB misses, terrible.

**Fix:** Constants matter. Understand cache lines and spatial locality.

---

### Pitfall 4: Recursion Without Understanding Limits

**Problem:** "I'll just make it recursive; it's elegant"

**Reality:** Stack overflow at ~10,000 calls. Deep recursion on large problem fails.

**Fix:** Test with realistic input sizes. Understand stack limits.

---

### Pitfall 5: Not Recognizing Overlapping Subproblems

**Problem:** "Memoization didn't help my recursive function"

**Reality:** Memoization only helps if subproblems overlap. Tree traversal has no overlap.

**Fix:** Check for repeated subproblems before memoizing.

---

### Pitfall 6: Peak Finding Panic

**Problem:** "I can't think of a better solution than O(n) scan"

**Reality:** Exploit structure: divide-conquer, check neighbors, recurse on larger side.

**Fix:** Practice peak finding. Recognize the pattern in other problems.

---

### Pitfall 7: Not Connecting Theory to Systems

**Problem:** "This is all theoretical; real systems are different"

**Reality:** Real systems follow the theory. Virtual memory, caches, TLB all matter.

**Fix:** Understand WHY theory matters. Run experiments on your system.

---

## 🔄 Algorithm Selection Decision Matrix

```
Choosing the Right Approach:

Problem involves search in structure?
├─ Sorted array? → Binary search (O(log n))
├─ Graph? → BFS/DFS
├─ Tree? → Tree traversal + structure
└─ Unsorted? → Scan (O(n))

Recursive structure possible?
├─ Overlapping subproblems? → Memoization
├─ Divide-and-conquer? → Split and combine
├─ No overlap? → Direct recursion OK
└─ Depth concern? → Convert to iteration

Optimize further?
├─ Can exploit monotonicity? → Binary search or similar
├─ Can divide and conquer? → Reduce complexity class
└─ Can precompute? → Trade space for speed
```

---

## ✅ Per-Concept Study Checklist

### Memory & Pointers Checklist
- [ ] Draw process address space
- [ ] Trace pointers and dereferencing
- [ ] Understand stack frames
- [ ] Explain TLB and page faults
- [ ] Measure cache line size on your system

### Complexity Analysis Checklist
- [ ] Prove Big-O, Big-Ω, Big-Θ for simple algorithms
- [ ] Rank 10 complexity classes by growth
- [ ] Analyze recurrence via recurrence tree (5 examples)
- [ ] Calculate break-even points for two algorithms

### Space Complexity Checklist
- [ ] Calculate stack depth for recursive function
- [ ] Analyze heap allocation patterns
- [ ] Measure object overhead on your system
- [ ] Identify time-space trade-offs in problems

### Recursion Checklist
- [ ] Trace 5+ recursive functions by hand
- [ ] Identify recursion patterns (linear, tree, divide-conquer)
- [ ] Implement memoization (3+ problems)
- [ ] Convert recursive to iterative

### Peak Finding Checklist
- [ ] Understand 1D peak definition and brute force
- [ ] Implement O(log n) 1D peak finding
- [ ] Understand 2D peak definition
- [ ] Implement 2D peak finding
- [ ] Analyze complexity of both

---

## 📝 Weekly Problem Milestones

**After Day 1:** Understand address spaces, pointers, stack/heap

**After Day 2:** Analyze 3+ algorithms with Big-O, prove complexities

**After Day 3:** Analyze space usage, understand stack depth limits

**After Day 4:** Trace 5+ recursive functions, understand call stack

**After Day 5:** Implement memoization on 2+ problems, convert O(2^n) to O(n)

**After Day 6:** Solve peak finding 1D and 2D, recognize pattern in other problems

**Integration:** 5+ problems mixing concepts (memory + recursion + complexity)

---

## 🏁 Interview Problems by Difficulty

| Difficulty | Topic | Example |
|-----------|-------|---------|
| Easy | Memory | Pointer arithmetic, address calculation |
| Easy | Complexity | Classify algorithm, simple recurrence |
| Medium | Big-O | Compare algorithms, break-even points |
| Medium | Recursion | Simple recursive function, trace |
| Medium | Memoization | Optimize recursive solution |
| Hard | Peak finding | 1D and 2D solving |
| Hard | Complexity | Analyze complex recurrence |
| Hard | Systems | Design algorithm aware of cache/memory |

---

**Practice Time:** 15-18 hours for 20-25 problems and exercises
