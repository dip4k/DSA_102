# 📋 Week 01 Guidelines: Computational Fundamentals & Algorithmic Thinking

**Week Overview:** Foundations I — Understanding memory, cost models, recursion, and your first algorithm design story  
**Status:** Core Curriculum Week (Days 1-5) + Optional Advanced (Day 6)  
**Primary Goal:** Build rock-solid mental models before diving into data structures and algorithms  
**Time Allocation:** 20-24 hours core learning + deep practice  

---

## 🎯 Week 01 Learning Arc

Before you implement your first algorithm or optimize a system, you need to understand **how your code executes on a machine**. Week 01 establishes the foundations: how memory is organized, why pointers matter, what Big-O notation really means, how recursion actually works, and how to think algorithmically via **peak finding**—your first real algorithm design experience.

This week is foundational. Everything that follows (data structures, algorithms, systems) sits on top of these concepts.

---

## 📚 Day-by-Day Comprehensive Overview

### Day 1: RAM Model, Virtual Memory, Pointers

**Core Mental Model:** A program's memory is organized into logical address spaces. Your CPU accesses memory through addresses, and pointers are just addresses stored in variables.

**The RAM Model**

- **Abstract View:** Memory is an infinite array of cells, each holding one word (8 bytes on 64-bit systems)
- **Constant-time Random Access:** Access any cell in O(1) time—this is the **fundamental assumption**
- **Reality Check:** This breaks down with virtual memory, caches, and page faults, but it's the default model

**Process Address Space**

Modern processes use **virtual addressing** with four key regions:

1. **Code (Text) Segment:** Your program's instructions (usually read-only, shared across process instances)
2. **Data Segment:** Global and static variables (initialized and uninitialized)
3. **Heap:** Dynamically allocated memory (malloc, new) — grows upward, manual or GC-managed
4. **Stack:** Function frames, local variables, return addresses — grows downward

Example: When you call `func(x)`, a frame is pushed with parameters, locals, and return address. When `func` returns, the frame is popped.

**Variables vs Pointers**

- **Variable:** A named location in memory holding a value (e.g., `int x = 5;` stores 5 in a cell)
- **Pointer:** A variable holding an **address** of another variable (e.g., `int *p = &x;` stores x's address)
- **Dereferencing:** Follow the address arrow (e.g., `*p` gives the value at the address)

**Critical Pointer Pitfalls**

- **Uninitialized pointers:** Contain garbage addresses; dereferencing causes crash or undefined behavior
- **Dangling pointers:** Point to freed memory; using them is undefined behavior
- **Double free:** Freeing same memory twice corrupts the allocator
- **Null pointers:** Address 0; dereferencing is always invalid

**Virtual Memory & TLB (Conceptual)**

- **Virtual vs Physical:** Your program sees a virtual address space; hardware translates to physical RAM
- **Pages:** Memory divided into 4KB or 64KB blocks (pages)
- **Page Table:** OS maintains a table mapping virtual → physical addresses
- **TLB (Translation Lookaside Buffer):** Cache for page table entries (very fast hits, slow misses)
- **Page Fault:** Virtual address not in RAM; OS must swap from disk (extremely slow: ~10ms)

**Why Contiguity Matters:**
- Contiguous memory (like arrays) → pages likely to be mapped, few TLB misses
- Scattered memory (like linked lists) → each dereference might miss TLB, causing page faults

**Memory Hierarchy & Caches**

From fastest to slowest:
1. **Registers:** ~1ns, tiny capacity (256 bytes)
2. **L1 Cache:** ~5ns, small (32-64KB per core)
3. **L2 Cache:** ~15ns, medium (256-512KB per core)
4. **L3 Cache:** ~40ns, larger (8-20MB shared)
5. **DRAM (Main Memory):** ~100ns, large (gigabytes)
6. **Disk:** ~10ms, huge (terabytes)

**Cache Lines:** 64 bytes. When you access one memory location, the entire 64-byte cache line is fetched. Sequential access exploits this (spatial locality); random access wastes bandwidth.

**Temporal Locality:** If you use a variable soon after accessing it, it's likely still in cache (fast reuse).

**Systems Angle:** Data alignment and false sharing
- **Alignment:** Compilers pad structs so data aligns naturally (e.g., int at 4-byte boundary). Misaligned access is slower.
- **False Sharing:** Two threads on two cores accessing different variables that happen to share a cache line. Both threads cause cache line thrashing (invalidation every access). Solution: pad data or use different cache lines.

---

### Day 2: Asymptotic Analysis (Big-O, Big-Ω, Big-Θ)

**Core Mental Model:** Express algorithm complexity as a function of input size, ignoring constants and lower-order terms.

**Motivation for Asymptotics**

- Micro-optimizations (constants) matter for small inputs but don't scale
- For n=10^6, constant factors are secondary; growth rate dominates
- We care about: "How does runtime grow as n increases?"

**The Three Notations**

| Notation | Meaning | Intuition |
|----------|---------|-----------|
| **O(f(n))** | Upper bound | ≤ | Algorithm ≤ f(n) work, asymptotically |
| **Ω(f(n))** | Lower bound | ≥ | Algorithm ≥ f(n) work, asymptotically |
| **Θ(f(n))** | Tight bound | = | Algorithm ≈ f(n) work, asymptotically |

**Example:** Binary search

- **Worst case:** O(log n) comparisons (upper bound)
- **Best case:** Ω(1) (lower bound—lucky first guess)
- **Average case:** Θ(log n) (tight—typically)

**Common Complexity Classes (Ordered by Growth)**

```
O(1) < O(log n) < O(n) < O(n log n) < O(n²) < O(n³) < O(2^n) < O(n!)
```

Real-world interpretation (for n = 10^6):

- **O(1):** 1 operation (nanosecond)
- **O(log n):** ~20 operations (microsecond)
- **O(n):** 10^6 operations (millisecond)
- **O(n log n):** ~2×10^7 operations (20 milliseconds)
- **O(n²):** 10^12 operations (seconds)
- **O(2^n):** Impossible (the universe isn't old enough)

**Simple Recurrence Examples**

Binary Search: `T(n) = T(n/2) + O(1)` → **O(log n)**

Merge Sort: `T(n) = 2×T(n/2) + O(n)` → **O(n log n)**

Fibonacci (naive): `T(n) = T(n-1) + T(n-2) + O(1)` → **O(2^n)** (exponential blowup!)

**Comparing Functions**

- When does O(n log n) beat O(n)? When n is large enough. n log n ≈ n × log₂(n) ≈ n × 20 for n=10^6.
- Break-even point: Depends on constants. O(2n) might beat O(n log n) for small n, but O(n log n) wins eventually.

**Systems View**

For small n, constants and cache behavior may dominate asymptotic complexity. For large n, Big-O is reliable.

---

### Day 3: Space Complexity & Memory Usage

**Core Mental Model:** Understand not just time cost, but where memory lives and how much you use.

**Types of Space**

- **Total Space:** Everything used (input, output, scratch)
- **Auxiliary Space:** Scratch space (excluding input/output)
- **Stack vs Heap:** Stack (automatic, limited) vs heap (manual, large)

**Stack vs Heap**

| Property | Stack | Heap |
|----------|-------|------|
| **Memory** | Linear (LIFO) | Free blocks |
| **Allocation** | Automatic (scope) | Manual or GC |
| **Speed** | Fast (pointer bump) | Slower (find free block) |
| **Size** | Limited (~1-10 MB) | Large (gigabytes) |
| **Example** | Local variables | malloc/new |

**Activation Records (Stack Frames)**

When you call `func(int x)`:

```
Frame for func:
[prev frame pointer] ← frame points here
[x = value]
[local var z]
[return address to caller]
```

As functions return, frames pop. Deep recursion can exhaust stack space (stack overflow).

**Space Overhead**

- Pointers: 8 bytes each (64-bit systems)
- Object headers: 16 bytes (Java, Python object metadata)
- Array: Not just n×sizeof(element); includes capacity, metadata

Example: `vector<int>` with 100 ints
- Raw ints: 400 bytes
- Vector: 400 (elements) + 24 (capacity, size metadata) + allocator overhead ≈ 450+ bytes

**Time-Space Trade-offs**

- **Memoization:** Use O(n) space to cache results, convert exponential time to polynomial
- **Precomputation:** Build lookup tables upfront, trade space for query speed
- **Streaming:** Process input once (O(1) space), vs storing all (O(n) space)

---

### Day 4: Recursion I — Call Stack & Basic Patterns

**Core Mental Model:** Recursion is a consequence of how the call stack works. No magic, just pushing and popping frames.

**Call Stack Mechanics**

```
Call: fact(3)
  ┌─ fact(3)
  │  ├─ fact(2)
  │  │  ├─ fact(1)
  │  │  │  └─ fact(0) → return 1
  │  │  └─ return 1 * 1 = 1
  │  └─ return 2 * 1 = 2
  └─ return 3 * 2 = 6
```

Each call pushes a frame. When return statement executes, frame pops.

**Basic Recursive Structure**

Every recursive function needs:

1. **Base Case:** When to stop recursing (e.g., n==0)
2. **Recursive Case:** Reduce problem size (e.g., call with n-1)
3. **Progress:** Guarantee you move toward base case

Missing base case → infinite recursion (stack overflow).

**Simple Examples**

Factorial: `fact(n) = n * fact(n-1)` (if n > 0) else 1
- Base: fact(0) = 1
- Recursive: fact(n) = n × fact(n-1)

Sum of array: `sum(arr, i) = arr[i] + sum(arr, i+1)` (if i < length) else 0

**Recursion Trees**

Visualize call graph:

```
Naive Fibonacci: fib(5)
              fib(5)
             /      \
        fib(4)      fib(3)
       /      \    /      \
    fib(3)  fib(2) fib(2) fib(1)
   /   \    /  \   /  \
fib(2) fib(1) ...
```

Notice many repeated calls (fib(3), fib(2), etc.). This is **exponential blowup** (O(2^n)).

**Failure Modes**

- Missing base case: Infinite recursion
- Recursion not progressing: Infinite recursion
- Deep recursion: Stack overflow (limit is ~10,000 calls typically)

---

### Day 5: Recursion II — Patterns & Memoization

**Core Mental Model:** Recognize recursion patterns. Use memoization to cache repeated subproblems.

**Recursion Patterns**

1. **Linear Recursion:** Single recursive call (e.g., factorial, sum)
   - Depth = n, number of calls = n
   - Time = O(n) (or more with work at each level)

2. **Tree Recursion:** Multiple recursive calls (e.g., Fibonacci)
   - Depth = O(n) or O(log n)
   - Number of calls = O(2^n) or more (exponential!)

3. **Divide-and-Conquer:** Split into disjoint subproblems (e.g., merge sort)
   - Combine results from halves
   - Time = O(n log n) typically

**Tail Recursion**

Tail call: The recursive call is the last operation (no computation after return).

```cpp
// Tail recursive
int fact_tail(int n, int acc) {
    return (n == 0) ? acc : fact_tail(n-1, n*acc);
}
```

Some compilers optimize tail recursion into a loop (no new frame). Java and Python don't.

**Memoization**

Recognize **overlapping subproblems**. Cache results to avoid recomputation.

Example: Fibonacci without memoization
- fib(5) calls fib(4) and fib(3)
- fib(4) calls fib(3) and fib(2)
- fib(3) is computed twice!

With memoization:
```cpp
map<int, long long> memo;
long long fib(int n) {
    if (memo.count(n)) return memo[n];
    if (n <= 1) return n;
    memo[n] = fib(n-1) + fib(n-2);
    return memo[n];
}
```

Now fib(5) is O(n) instead of O(2^n).

**Stack Depth Limits**

Typical limits: ~10,000 recursive calls. Deep recursion can cause stack overflow. Solution: Convert to iteration or use explicit stack.

---

### Day 6: Peak Finding — Your First Algorithm Design Story (Optional Advanced)

**Core Mental Model:** Exploit structure and monotonicity to design better-than-brute-force algorithms.

**1D Peak Finding**

**Definition:** Element greater than or equal to both neighbors.

**Brute Force:** O(n) scan, compare each element.

**Better Approach:**

```
1D array [10, 20, 15, 30, 25, ...]
           ↑   ↑   ↑
         Compare mid with neighbors.
         If mid > both: peak found.
         If left > mid: recurse left (must be a peak there).
         If right > mid: recurse right (must be a peak there).
```

**Why it works:** If left > mid, then somewhere in [left, mid] there must be a peak (because at the boundary, left > mid, and at the far left, either it's a peak or its neighbor is larger, creating an increasing sequence that must peak).

**Complexity:** T(n) = T(n/2) + O(1) → **O(log n)**

**2D Peak Finding**

**Definition:** Element ≥ all four neighbors (up, down, left, right).

**Approach:**

```
1. Pick middle column.
2. Find column-local maximum (largest in that column).
3. Compare with neighbors (left, right).
4. If neighbors are larger, recurse toward that neighbor.
5. Otherwise, current is a peak.
```

**Complexity:** T(n, m) ≈ O(n log m) in typical cases (recur on columns).

**Meta-Lessons**

- **Exploit structure:** Sorted, monotone, or special properties?
- **Divide and conquer:** Split problem, recurse on promising side.
- **Better-than-brute-force mindset:** Don't accept O(n); push for O(log n) or O(n log n).

---

## 🎓 Nine Core Learning Objectives

By end of Week 01, you will be able to:

1. **Explain memory layout:** Understand address spaces, stack vs heap, pointers
2. **Apply Big-O notation:** Classify algorithms by growth rate, compare functions
3. **Analyze space complexity:** Understand memory usage, stack depth, cache implications
4. **Trace recursive functions:** Understand call stack, visualize recursion trees
5. **Apply memoization:** Recognize overlapping subproblems, convert exponential to polynomial
6. **Design simple algorithms:** Use divide-and-conquer, exploit structure
7. **Solve peak finding:** Both 1D and 2D variants
8. **Understand trade-offs:** Time-space, constants vs asymptotics, reality vs theory
9. **Think like a systems engineer:** Understand caches, locality, real-world implications

---

## 🧭 Four Effective Study Strategies

### Strategy 1: Build Mental Models First
Before coding, understand the **why**:
- Why do arrays have O(1) access? Address formula.
- Why is recursion exponential? Lack of caching.
- Why does binary search work? Invariant maintenance.

### Strategy 2: Trace by Hand
Work through examples on paper:
- Trace fact(5): fact(5) → 5×fact(4) → ... → 120
- Trace peak finding on [10,20,15,30,25]: mid=15, left=20 > 15, recurse left...
- Draw recursion trees, mark repeated subproblems

### Strategy 3: Test Edge Cases
- Asymptotics: Test on small (n=2) and large (n=10^6) inputs
- Recursion: Base cases, single element, depth limits
- Peak finding: Arrays with all equal elements, single element

### Strategy 4: Connect to Real Systems
- Memory: Why is cache important? Understand prefetching.
- Complexity: When O(n) vs O(n log n) matters in practice.
- Recursion: What's the recursion depth limit on your system?

---

## ⏱️ Recommended Time Allocation

| Activity | Hours | Focus |
|----------|-------|-------|
| Read Day 1 instructional | 1.5 | RAM, virtual memory, pointers |
| Practice Day 1 concepts | 1.5 | Memory layout, pointer arithmetic |
| Read Day 2 instructional | 1.5 | Big-O, complexity classes |
| Practice Day 2 problems | 2 | Prove complexities, compare functions |
| Read Day 3 instructional | 1 | Space, stack, heap |
| Practice Day 3 concepts | 1.5 | Analyze space, trace stack |
| Read Day 4 instructional | 1.5 | Call stack, recursion basics |
| Practice Day 4 coding | 2 | Trace, code simple recursion |
| Read Day 5 instructional | 1.5 | Patterns, memoization |
| Practice Day 5 coding | 2.5 | Memoization, exponential to poly |
| Peak finding lecture | 1 | Understand algorithm |
| Peak finding implementation | 2 | Code 1D, code 2D |
| Integration & synthesis | 2 | Review all 5, connections |
| Optional Day 6 (advanced) | 1-2 | Deeper analysis, proofs |
| **Total Core (Days 1-5)** | **20 hours** | **Interview prep complete** |
| **Total with Day 6** | **21-22 hours** | **Mastery level** |

---

## 🚫 Five Common Pitfalls (and How to Avoid Them)

### Pitfall 1: "I understand Big-O, but complexity doesn't matter for my code"

**Wrong:** Assuming constants dominate. For n=10^6, O(n²) is unusable.

**Fix:** Calculate actual work. If n=10^6 and O(n²), that's 10^12 operations—likely seconds to minutes.

---

### Pitfall 2: "Recursion is magical"

**Wrong:** Thinking recursion is some special construct, not understanding stack frames.

**Fix:** Trace manually on paper. Draw the call stack. See frames push and pop.

---

### Pitfall 3: "My recursive function is slow; I'll just use memoization"

**Wrong:** Adding memoization without understanding overlapping subproblems.

**Reality:** If your recursion has no overlapping subproblems (e.g., tree traversal), memoization doesn't help.

**Fix:** Recognize overlapping subproblems before memoizing.

---

### Pitfall 4: "Stack depth limit doesn't apply to me"

**Wrong:** Writing deep recursion without testing.

**Reality:** Typical limit is ~10,000. Recursive quicksort on n=10^6 can overflow.

**Fix:** Test with large inputs. Convert to iteration if needed.

---

### Pitfall 5: "Cache and locality don't matter for algorithms"

**Wrong:** Focusing only on Big-O, ignoring constants.

**Reality:** Array iteration: 1 cache miss per 8 elements (64-byte line). Linked list: 1 miss per element. Same O(n), vastly different constants.

**Fix:** Understand cache lines, prefetching, false sharing.

---

## 📊 Concept Map: Week 01 Foundations

```
Week 01: Computational Fundamentals

├─ MEMORY MODEL
│  ├─ RAM (abstract constant-time access)
│  ├─ Virtual Memory (pages, TLB, page faults)
│  ├─ Process Address Space (code, data, heap, stack)
│  └─ Memory Hierarchy (registers → L1/L2/L3 → DRAM → disk)
│
├─ COMPLEXITY ANALYSIS
│  ├─ Big-O, Big-Ω, Big-Θ (upper, lower, tight)
│  ├─ Common classes (O(1) to O(n!))
│  ├─ Recurrence analysis (recurrence trees)
│  └─ Constants and break-even points
│
├─ SPACE COMPLEXITY
│  ├─ Stack (activation records, depth limits)
│  ├─ Heap (allocation, fragmentation)
│  ├─ Auxiliary space
│  └─ Time-space trade-offs
│
├─ RECURSION
│  ├─ Call stack mechanics
│  ├─ Patterns (linear, tree, divide-conquer)
│  ├─ Memoization (overlapping subproblems)
│  └─ Stack overflow concerns
│
└─ ALGORITHMIC THINKING
   ├─ Peak finding (1D and 2D)
   ├─ Divide-and-conquer
   ├─ Exploiting structure
   └─ Better-than-brute-force mindset
```

---

## 🎯 Ten Key Insights

1. **Pointers are just addresses.** Understanding pointers deeply unlocks memory-level reasoning.

2. **Big-O hides constants.** For small n, constants dominate. For large n, growth rate dominates.

3. **Recursion is stack manipulation.** Understanding call frames demystifies recursion.

4. **Memoization is caching.** Overlapping subproblems + caching = exponential to polynomial.

5. **Cache lines matter.** Sequential access is 10-100x faster than random due to cache behavior.

6. **Virtual memory is slow.** Page faults (disk I/O) are millions of times slower than DRAM.

7. **Tail recursion can be optimized.** Compilers (in some languages) convert to loops.

8. **Stack is limited, heap is flexible.** Deep recursion requires heap-based alternatives.

9. **Asymptotics are for large n.** For small n, implement whatever is clearest; for large n, Big-O matters.

10. **Algorithmic thinking is learnable.** Peak finding is your first design story: divide-conquer, exploit structure, find better solutions.

---

## 🔄 How Week 01 Connects to Surrounding Weeks

**Foundation for Everything:**
- Week 02 (Linear Data Structures): Big-O justifies why arrays are O(1) access, linked lists O(n)
- Week 03 (Sorting & Hashing): Recurrences analyze merge sort, binary search on sorted arrays
- Weeks 04+ (Patterns, Graphs, DP): All depend on solid understanding of recursion and complexity

**Ongoing Reminders:**
- Every algorithm design: "What's the Big-O? Can we exploit structure like in peak finding?"
- Every data structure: "Why does this have these complexities? Memory layout matters."
- Every recursion: "Am I experiencing exponential blowup? Do I need memoization?"

---

## ✅ Weekly Checklist

By end of week, you should be able to:

- [ ] Explain memory address space (code, data, heap, stack)
- [ ] Describe the purpose and role of pointers
- [ ] Classify algorithms by Big-O, Big-Ω, Big-Θ
- [ ] Compare growth rates (O(n) vs O(n log n) vs O(n²))
- [ ] Analyze recurrence relations (recurrence tree method)
- [ ] Explain stack frames and recursion mechanics
- [ ] Trace recursive functions on paper
- [ ] Recognize overlapping subproblems
- [ ] Implement memoization to convert exponential to polynomial
- [ ] Solve 1D and 2D peak finding
- [ ] Explain cache behavior and locality implications
- [ ] Understand time-space trade-offs

---

## 🎓 Mastery Signals

You've mastered Week 01 when:

1. **You think about memory.** When designing code, you ask: "Will this fit in cache? Will there be TLB misses?"

2. **You classify instantly.** Given an algorithm, you immediately classify its Big-O and explain why.

3. **You trace recursion fluently.** Manual traces are fast; you visualize call stacks naturally.

4. **You recognize patterns.** You see peak finding structure in other problems (binary search, divide-conquer).

5. **You memoize instinctively.** You spot overlapping subproblems and apply memoization without prompting.

6. **You understand constants.** You know when O(n log n) beats O(n²) and can estimate break-even points.

7. **You code recursion safely.** Base cases are clear, stack depth is monitored, memoization is applied.

8. **You explain to others.** You can teach someone else about Big-O, recursion, and peak finding clearly.

---

**Status:** Week 01 Guidelines Complete  
**Next:** Week 01 Summary & Key Concepts  
**Time to Completion:** 20+ hours for full mastery