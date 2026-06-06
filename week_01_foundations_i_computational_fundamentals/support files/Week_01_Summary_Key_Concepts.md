# 📖 Week 01 Summary & Key Concepts: Computational Foundations Deep Reference

**Audience:** Students completing Week 01 instructional content  
**Purpose:** Comprehensive reference for review and retention

---

## 🎯 Week 01 Narrative Summary

**Week 01: Foundations I** establishes the bedrock understanding that all future learning builds upon. You move from abstract algorithm concepts to the **concrete reality of how programs execute on machines**—memory layout, address spaces, the actual cost of operations, and how to reason about algorithms systematically.

---

## 🏗️ MEMORY MODEL: Complete Landscape

### The RAM (Random Access Memory) Model

```
Abstract View:
[Cell 0] [Cell 1] [Cell 2] [Cell 3] ... [Cell n]
  8 bytes  8 bytes  8 bytes  8 bytes    8 bytes

Assumption: Access any cell in O(1) time
Reality: True for L1 cache hits, false for disk I/O
```

**Key Insight:** The RAM model is your default mental model. It's correct enough for complexity analysis, but breaks down when:
- Cache misses occur
- Virtual memory triggers page faults
- Multiple threads create contention

### Process Address Space

Modern 64-bit process (typical layout, high-to-low addresses):

```
High Addresses (2^63)
┌─────────────────────────┐
│  Kernel Space           │ (OS, not accessible)
├─────────────────────────┤
│  Stack (grows down)     │ Functions, locals, frames
│  ...                    │
│  (unused space)         │
│  ...                    │
│  Heap (grows up)        │ malloc, new, dynamic
├─────────────────────────┤
│  Data Segment           │ Globals, statics
│  (initialized + uninit) │
├─────────────────────────┤
│  Code Segment (Text)    │ Instructions (read-only)
└─────────────────────────┘
Low Addresses (0)
```

**Stack:** LIFO structure. When function calls, frame pushed (parameters + locals + return address). When function returns, frame popped. Typical size: 1-10 MB.

**Heap:** Free-form allocation. malloc/new request memory; OS finds free block and returns pointer. Fragmentation possible. Typical size: gigabytes.

**Data Segment:** Static lifetime (program start to end). No allocation/deallocation needed.

**Code Segment:** Read-only. Instructions of your program.

### Virtual Memory & TLB

**Virtual vs Physical Addressing:**
- **Virtual address:** Address you see in your program (0 to 2^64)
- **Physical address:** Actual location in RAM (much smaller)
- **Page table:** OS maintains mapping from virtual → physical pages

**Pages:** Memory divided into 4KB or 64KB pages (typically 4KB). All address translations happen at page granularity.

**TLB (Translation Lookaside Buffer):** Cache for page table entries. Stores recent virtual → physical mappings. TLB hits are fast (~5 cycles); misses are slow (page table lookup ~100 cycles or page fault ~10ms disk I/O).

**Why Contiguity Matters:**
- Array of 1000 ints, sequentially accessed: Likely on same page or adjacent pages. Few TLB misses.
- Linked list of 1000 nodes, scattered across heap: Each dereference might cross page boundary. Potential TLB miss, especially page fault if page not in memory.

### Memory Hierarchy & Caches

```
Speed          Size        Access Time
Registers      256 B       ~1 ns
L1 Cache       32-64 KB    ~5 ns
L2 Cache       256-512 KB  ~15 ns
L3 Cache       8-20 MB     ~40 ns
DRAM           Gigabytes   ~100 ns
Disk           Terabytes   ~10 ms
```

**Cache Line:** 64 bytes (typical). When you access one memory location, the entire 64-byte cache line is fetched from the next level. Sequential access exploits this (spatial locality); random access wastes bandwidth.

**Temporal Locality:** If you use a variable soon after accessing it, it's likely still in a cache. Reuse is fast.

**False Sharing:** Two threads on different cores accessing different variables that share a cache line. Both threads cause cache line invalidations on every access. Solution: pad data or use different cache lines.

---

## 📊 BIG-O NOTATION: Complete Model

### The Three Notations

| Notation | Definition | Intuitive Meaning |
|----------|-----------|-------------------|
| **O(f(n))** | ∃ c, n₀: 0 ≤ T(n) ≤ c·f(n) ∀n > n₀ | ≤ c·f(n), asymptotically |
| **Ω(f(n))** | ∃ c, n₀: c·f(n) ≤ T(n) ∀n > n₀ | ≥ c·f(n), asymptotically |
| **Θ(f(n))** | Both O(f(n)) AND Ω(f(n)) | = f(n), asymptotically |

**Example: Binary Search**
- Comparisons: 1 to log₂(n)
- Best case: 1 (lucky first guess) → Ω(1)
- Worst case: log₂(n) → O(log n)
- Average: log₂(n) → Θ(log n)

### Complexity Classes (Ranked by Growth)

```
O(1) < O(log n) < O(n) < O(n log n) < O(n²) < O(n³) < O(2^n) < O(n!)

For n = 10^6:
O(1):      1 op       ~nanosecond
O(log n):  20 ops     ~microsecond
O(n):      10^6 ops   ~millisecond
O(n log n):2×10^7 ops ~20 milliseconds
O(n²):     10^12 ops  ~1000 seconds
O(2^n):    Impossible
```

### Recurrence Analysis (Recurrence Tree Method)

**Binary Search:** T(n) = T(n/2) + O(1)
```
Level 0:   1 node,    O(1) work = O(1)
Level 1:   1 node,    O(1) work = O(1)
Level 2:   1 node,    O(1) work = O(1)
...
Depth:     log₂(n)

Total: log₂(n) levels × O(1) work = O(log n)
```

**Merge Sort:** T(n) = 2·T(n/2) + O(n)
```
Level 0:   1 node,    O(n) work = O(n)
Level 1:   2 nodes,   O(n/2) each = O(n) total
Level 2:   4 nodes,   O(n/4) each = O(n) total
...
Depth:     log₂(n)

Total: log₂(n) levels × O(n) work = O(n log n)
```

**Naive Fibonacci:** T(n) = T(n-1) + T(n-2) + O(1)
```
Level 0:   1 node
Level 1:   2 nodes
Level 2:   4 nodes
Level 3:   8 nodes
...
Depth:     n

Total: 2^0 + 2^1 + 2^2 + ... + 2^n = O(2^n)
```

---

## 🧠 SPACE COMPLEXITY: Memory Usage Analysis

### Stack vs Heap Memory

**Stack (Automatic):**
- Function frames: parameters, return address, local variables
- Automatic deallocation: when function returns, frame pops
- Limited size: typically 1-10 MB
- Fast allocation: just bump a pointer
- Failure mode: stack overflow (deep recursion or large locals)

**Heap (Manual or GC):**
- Dynamically allocated objects
- Manual deallocation (malloc/free) or garbage collected
- Large size: up to available physical RAM
- Slower allocation: find free block, may require compaction
- Failure mode: memory leak (allocated but never freed)

### Activation Records (Stack Frames)

```cpp
void foo(int x, int y) {
    int z = 10;
    char buf[100];
    // Frame structure (stack):
    // [return address to caller]
    // [saved old frame pointer]
    // [x = value]
    // [y = value]
    // [z = 10]
    // [buf[0..99] = uninitialized]
}
```

Deep recursion (e.g., recursive quicksort on 10^6 elements) can cause stack overflow.

### Space Overhead

Common hidden space costs:
- **Pointers:** 8 bytes each (64-bit)
- **Object headers:** 16 bytes (Java, Python object metadata)
- **Allocator metadata:** 8-16 bytes per allocation (tracking size, free list)
- **Capacity vs size:** Array may allocate 100 slots but use only 50

---

## 🔄 RECURSION: Call Stack Mechanics

### Recursion Tree Visualization

```
fact(4) = 4 * fact(3)
   |
   └─ fact(3) = 3 * fact(2)
      |
      └─ fact(2) = 2 * fact(1)
         |
         └─ fact(1) = 1 * fact(0)
            |
            └─ fact(0) = 1
```

Each box is a stack frame. Computation happens **on the way back up** (unwind).

### Recursion Patterns

**Linear Recursion:** Single recursive call
- Call chain: T(n) → T(n-1) → T(n-2) → ... → T(0)
- Depth: n, Calls: n
- Example: factorial, sum of array

**Tree Recursion:** Multiple recursive calls
- Call tree branches exponentially
- Depth: n or log n, Calls: exponential
- Example: naive Fibonacci (O(2^n)), merge sort (O(n log n) work total)

**Divide-and-Conquer:** Split into disjoint subproblems
- Call tree has specific structure (halves, thirds, etc.)
- Depth: log n, Calls: O(n) or more depending on work at each level
- Example: merge sort, quicksort, binary search

### Memoization (Caching Recursion Results)

**Overlapping Subproblems:** When recursive function recomputes same inputs multiple times.

**Naive Fibonacci:**
```
fib(5)
  fib(4) + fib(3)
    (fib(3) + fib(2)) + fib(3)  ← fib(3) computed twice!
```

**With Memoization:**
```cpp
map<int, long long> memo;
long long fib(int n) {
    if (memo.count(n)) return memo[n];  // Cache hit
    if (n <= 1) return n;
    long long res = fib(n-1) + fib(n-2);
    memo[n] = res;  // Cache for future calls
    return res;
}
```

Now each fib(k) computed once. Time: O(n), not O(2^n).

---

## 🎯 PEAK FINDING: Algorithm Design Story

### 1D Peak Finding

**Definition:** Element ≥ both neighbors.

**Brute Force:** O(n) scan.

**Divide-and-Conquer (O(log n)):**

```
Input: [10, 20, 15, 30, 25, ...]
       [0   1   2   3   4 ...]

1. mid = 2
2. arr[mid] = 15, left neighbor = 20, right neighbor = 30
3. 20 > 15, so recurse left [0..2]
4. mid = 1
5. arr[mid] = 20, left = 10, right = 15
6. 20 ≥ both, so 20 is a peak!
```

**Why it works:**
- If left > mid: left side has a "higher point" than mid. If you recurse left, you're guaranteed to find a peak (either left itself or its left neighbor, etc., must eventually peak).
- Similarly for right.

**Complexity:** T(n) = T(n/2) + O(1) = **O(log n)**

### 2D Peak Finding

**Definition:** Element ≥ all four neighbors (up, down, left, right).

**Approach:**

```
1. Choose middle column.
2. Find column-local maximum (largest in column).
3. Check left and right neighbors.
4. If left neighbor > column max, recurse left.
5. If right neighbor > column max, recurse right.
6. Otherwise, column max is a peak.
```

**Intuition:** If left > max, then to the left there must be a peak (can trace back to an edge or a local peak). Recurse toward larger side.

**Complexity:** O(n log m) typically (n rows, m columns).

---

## 🚫 Seven Misconceptions (Corrected)

1. **"O(n) and O(2n) are the same, so constants don't matter"** → Wrong. For big-O they're the same, but in practice 2n is twice as slow. Constants matter!

2. **"My recursive function must be inefficient"** → Wrong. Recursion is O(n) if structured right (e.g., tree traversal). Exponential comes from overlapping subproblems.

3. **"Memoization always helps"** → Wrong. Only helps if overlapping subproblems exist. Tree traversal has no overlap; memoization doesn't help.

4. **"Stack overflow only happens with deep recursion"** → Wrong. Also happens with large local arrays (`char buf[10000000]` on stack).

5. **"Virtual memory and paging are transparent; I don't need to think about them"** → Wrong. Page faults (disk I/O) are millions of times slower. Locality matters.

6. **"Cache and Big-O are unrelated"** → Wrong. Array iteration: O(n) with great cache behavior. Linked list: O(n) with terrible cache. Constants differ by 10-100x.

7. **"Peak finding only applies to peak problems"** → Wrong. Same structure (divide-conquer, exploit monotonicity) appears in binary search, merge sort, and many algorithmic problems.

---

## 🔗 Week 01 Connections

**Foundation for All Future Weeks:**
- Week 02: Big-O justifies array O(1) access, linked list O(n) access
- Week 03: Recurrence analysis of sorting (merge sort O(n log n), quick sort expected O(n log n) worst O(n²))
- Week 04+: Algorithmic patterns all rely on recursion understanding
- Systems everywhere: Cache-aware programming, memory layout, avoiding false sharing

---

**Review Time:** 2-3 hours
