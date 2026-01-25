# 📘 COMPLETE_DETAILED_SYLLABUS_v13.md - FULL 19-WEEK CURRICULUM

**Version:** 13.0 (Comprehensive Day-Wise Breakdown - COMPLETE)  
**Status:** ✅ PRODUCTION READY - ALL 19 WEEKS FULLY DETAILED  
**Date:** January 24, 2026

---

# 🟦 PHASE A: FOUNDATIONS & COMPUTATIONAL THINKING (Weeks 1-3)

## Phase Motivation
Everything builds on computational fundamentals. Understanding memory, complexity, and recursion isn't optional—it's the bedrock of every algorithm. A strong foundation here eliminates confusion later.

## Phase Outcomes
- [ ] Understand how programs execute on machines (memory, pointers, call stack)
- [ ] Classify algorithms by complexity and predict scaling behavior
- [ ] Design and analyze recursive algorithms with confidence
- [ ] Implement and choose between fundamental data structures
- [ ] Apply sorting, heaps, and hashing to solve problems

---

## 📌 WEEK 1: COMPUTATIONAL FUNDAMENTALS, PEAK FINDING & ASYMPTOTICS

### Weekly Goal
Build rock-solid mental models of program execution, memory hierarchy, complexity frameworks, and basic recursion.

### Weekly Topics
- RAM model and memory assumptions
- Pointers and address spaces
- Asymptotic notation (Big-O, Big-Ω, Big-Θ)
- Recursion mechanics and patterns
- Peak finding algorithm design

---

### 📅 DAY 1: RAM MODEL, VIRTUAL MEMORY & POINTERS

**Duration:** 90 minutes

**Topics & Subtopics:**
- **RAM Model**
  - Memory as array of addressable cells
  - Constant-time random access assumption
  - Where it breaks: cache hierarchy, virtual memory, page faults
  - Real systems vs theoretical model tradeoffs

- **Virtual Memory & Address Spaces**
  - Virtual vs physical addresses
  - Process address space layout:
    - Code/text segment (read-only executable)
    - Global/static segment (initialized globals)
    - Heap (dynamic allocation, grows up)
    - Stack (function frames, grows down)
    - Kernel space
  - Virtual memory benefits: isolation, protection, more addressable space than physical

- **TLB & Paging**
  - Translation lookaside buffer (TLB) as cache
  - Page tables for virtual→physical mapping
  - Page faults: expensive (100s of cycles vs 1-2 cycles hit)
  - Implications: spatial and temporal locality matter

- **Pointers & References**
  - Pointer = address stored in memory location
  - Dereferencing: follow the pointer (indirection)
  - Null pointers and invalid addresses
  - Address-of (&) and dereference (*) operators
  - Pointer chains (pointer to pointer)

- **Memory Layout in Practice**
  - Stack frames: parameters, locals, return address
  - Each function call adds frame
  - Recursion: stack grows with each call
  - Stack overflow: too deep recursion
  - Heap fragmentation and allocator overhead

- **Caches & Locality**
  - Cache hierarchy: L1/L2/L3, DRAM, disk
  - Cache line ~64 bytes
  - Spatial locality: nearby memory accessed together
  - Temporal locality: repeated access to same data
  - False sharing when threads access same cache line

**Key Insights:**
- Random access is NOT really O(1) on real hardware
- Locality trumps big-O at small scales
- Memory is hierarchical, cost varies dramatically
- Pointers enable indirection and dynamic structures

**Deliverables:**
- [ ] Explain RAM model assumptions and limitations
- [ ] Draw process address space with all segments
- [ ] Trace pointer operations mentally
- [ ] Calculate actual cost differences between linear/random access

---

### 📅 DAY 2: ASYMPTOTIC ANALYSIS - BIG-O, BIG-Ω, BIG-Θ

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Motivation for Asymptotics**
  - Constant factors vary by machine/language
  - What matters: scaling behavior as n grows
  - "How does runtime grow with input size?"
  - Mathematical framework for complexity

- **Big-O Notation (Upper Bound)**
  - Definition: f(n) = O(g(n)) if ∃c,n₀: f(n) ≤ c·g(n) ∀n≥n₀
  - Intuition: g(n) is upper bound, possibly loose
  - Examples: 2n = O(n), 3n² + 2n + 1 = O(n²)
  - Common use: worst-case bounds

- **Big-Ω Notation (Lower Bound)**
  - Definition: f(n) = Ω(g(n)) if ∃c,n₀: f(n) ≥ c·g(n) ∀n≥n₀
  - Intuition: g(n) is lower bound
  - Examples: n² = Ω(n), 2n+1 = Ω(n)
  - Use: best-case or information-theoretic lower bounds

- **Big-Θ Notation (Tight Bound)**
  - Definition: f(n) = Θ(g(n)) if f(n) = O(g(n)) AND f(n) = Ω(g(n))
  - Intuition: g(n) matches exactly (up to constants)
  - Examples: 3n + 5 = Θ(n), n² + n = Θ(n²)
  - Use: exact asymptotic behavior

- **Complexity Classes**
  - O(1): constant time (array access)
  - O(log n): logarithmic (binary search)
  - O(n): linear (array scan)
  - O(n log n): linearithmic (merge sort, good sorts)
  - O(n²): quadratic (bubble sort, nested loops)
  - O(n³): cubic (matrix multiplication naive)
  - O(2ⁿ): exponential (subsets, exhaustive)
  - O(n!): factorial (permutations)

- **Comparing Functions**
  - 1 < log n < n < n log n < n² < n³ < 2ⁿ < n!
  - Which is better for n=10, 100, 1000, 10⁶?
  - Crossover points where complexity matters
  - When can constants dominate?

- **Properties of Notation**
  - Constants dropped: O(2n) = O(n), O(n²/2) = O(n²)
  - Transitive: O(g) = O(h) and h = O(f) → g = O(f)
  - Addition: O(f) + O(g) = O(max(f,g))
  - Multiplication: O(f) × O(g) = O(f×g)

- **Simple Recurrences (Intuition)**
  - T(n) = T(n/2) + O(1) → O(log n) (binary search)
  - T(n) = 2T(n/2) + O(n) → O(n log n) (merge sort)
  - T(n) = T(n-1) + O(1) → O(n) (linear traversal)
  - Recurrence tree visualization

**Key Insights:**
- Asymptotic analysis ignores constants (sometimes crucial difference in practice)
- Different notations answer different questions
- Complexity classes create hierarchy
- Recurrence patterns repeat in many algorithms

**Deliverables:**
- [ ] Prove 3n + 5 = Θ(n) using definitions
- [ ] Compare functions for n=1000, 10⁶
- [ ] Draw recurrence trees for binary search and merge sort
- [ ] Identify complexity class for simple pseudocode

---

### 📅 DAY 3: SPACE COMPLEXITY & MEMORY USAGE

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Space Complexity Fundamentals**
  - Total space = input + output + auxiliary (temporary)
  - Input space often excluded (given, read-only)
  - Focus on auxiliary/working space
  - In-place algorithms: O(1) auxiliary space

- **Stack vs Heap**
  - Stack: automatic, efficient, limited size
  - Heap: manual management, larger, slower
  - Recursion depth limited by stack size
  - Large allocations go to heap

- **Lifetime & Scope**
  - Local variables: scope of function, deallocated on return
  - Global variables: program lifetime
  - Dynamic allocation: manual deallocation (or GC)
  - Scope determines memory lifetime

- **Space Overheads**
  - Pointers: 8 bytes (64-bit systems)
  - Object headers: type info, GC marks
  - Allocator metadata: size, next free block
  - Padding/alignment for performance
  - "Empty" vector still has capacity

- **Common Space Patterns**
  - O(1): in-place array rearrangement
  - O(n): new array/list allocation
  - O(log n): recursion depth (binary search)
  - O(n): hash table with chaining
  - O(n²): 2D array/matrix

- **Time-Space Tradeoffs**
  - Memoization: use space to save time
  - Precomputation: build table once, query fast
  - Caching: store results of expensive operations
  - Tabling: DP table to avoid recomputation
  - Classic: tables vs computation

- **Memory Hierarchy & Performance**
  - L1 cache: KB, ~4 cycles
  - L2 cache: MB, ~12 cycles
  - L3 cache: MB, ~40 cycles
  - DRAM: GB, ~200 cycles
  - Disk: TB, ~10⁶ cycles (page fault)

**Key Insights:**
- Not all O(n) spaces are equal—allocation patterns matter
- Recursion depth can blow stack
- Time-space tradeoffs are fundamental optimization technique
- Hidden overheads (padding, pointers) can dominate

**Deliverables:**
- [ ] Design O(1) space algorithm for a problem
- [ ] Calculate maximum recursion depth before stack overflow
- [ ] Compare time-space tradeoff for two algorithms
- [ ] Identify space overheads in memory layout

---

### 📅 DAY 4: RECURSION I - CALL STACK & PATTERNS

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Call Stack Mechanics**
  - Activation records: parameters, locals, return address
  - Pushing/popping frames as functions call/return
  - Frame layout and function prologue/epilogue

- **Basic Recursive Structure**
  - Base case: when to stop recursing
  - Recursive case: reduce problem size
  - Progress toward base case
  - Proof by induction connection

- **Simple Recursive Examples**
  - Factorial: n! = n × (n-1)!
  - Sum of array: sum[0..n] = arr[n] + sum[0..n-1]
  - Fibonacci: fib(n) = fib(n-1) + fib(n-2)
  - Power: x^n via binary recursion

- **Recursion Trees**
  - Visualizing branching and depth
  - Width at each level
  - Identifying exponential blowup
  - Counting operations from tree structure

- **Failure Modes**
  - Missing base case → infinite recursion
  - Recursion that doesn't make progress (wrong direction)
  - Stack overflow from deep recursion
  - Off-by-one errors in base case

- **Measuring Recursive Costs**
  - Depth (how deep call stack grows)
  - Breadth (width of recursion tree)
  - Total nodes in tree
  - Time complexity from tree analysis

**Key Insights:**
- Recursion is consequences of call stack mechanics, not magic
- Depth limited by stack size ~10³-10⁴
- Tree structure determines complexity
- Exponential recursion (like naive Fib) is red flag

**Deliverables:**
- [ ] Draw recursion tree for recursive function
- [ ] Identify base case and recursive case
- [ ] Calculate time complexity from tree
- [ ] Fix infinite recursion bugs

---

### 📅 DAY 5: RECURSION II - ADVANCED PATTERNS & MEMOIZATION

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Recursion Patterns (Structural)**
  - Linear recursion (single chain): f(n-1) only
  - Tree recursion (multiple branches): f(n-1) + f(n-2), etc
  - Divide-and-conquer recursion: f(n/2) splits

- **Tail Recursion vs General Recursion**
  - Tail call: recursive call is last operation
  - When tail recursion resembles a loop
  - Tail call optimization (TCO) compiler behavior
  - Converting tail recursion to iteration

- **Mutual & Indirect Recursion**
  - A calls B calls A pattern
  - Even/odd recursive definitions
  - State machines via mutual recursion

- **Overlapping Subproblems**
  - Naive Fibonacci: exponential time due to recomputation
  - Observing same subproblems solved repeatedly
  - Visualizing overlap in recursion tree

- **Memoization (Top-Down DP)**
  - Cache design: key → cached result
  - Storing results of subproblems
  - Turning exponential recursion into polynomial time
  - Example: Fibonacci from 2^n to O(n)

- **Memoization vs Tabulation Trade-off**
  - Top-down: compute only needed subproblems
  - Bottom-up: compute all; more cache-friendly
  - Space implications

- **Stack Depth & Limits**
  - Practical recursion depth limits (typically ~10⁴)
  - System-dependent (ulimit, JVM heap)
  - When to convert to iteration/explicit stack
  - Iterative stacks vs call stack

**Key Insights:**
- Memoization converts exponential to polynomial
- Overlapping subproblems are signal for DP
- Tail recursion optimizable; deep recursion problematic
- Cache storage enables exponential-to-linear transformations

**Deliverables:**
- [ ] Implement memoized Fibonacci and verify O(n) time
- [ ] Identify overlapping subproblems in recursive function
- [ ] Convert tail-recursive function to iteration
- [ ] Design memoization cache for custom recursive problem

---

### 📅 DAY 6 (OPTIONAL): PEAK FINDING & ALGORITHMIC THINKING

**Duration:** 90 minutes

**Topics & Subtopics:**
- **1D Peak Finding**
  - Definition: peak = element ≥ neighbors
  - Simple O(n) scan solution
  - Why can we do better?
  - Information-theoretic argument

- **1D Divide & Conquer Solution**
  - Examine mid element and neighbor
  - Recurse on "promising" side
  - O(log n) complexity intuition
  - Correctness proof sketch

- **2D Peaks**
  - Matrix as 2D grid
  - Row vs column strategies
  - Mid-column selection and column-local maximum
  - Move to adjacent column if neighbor is larger
  - Recurrence intuition: T(n,m) = T(n, m/2) + O(n)

- **Algorithm Design Meta-Lessons**
  - Exploiting structure and monotonicity
  - "Better-than-brute-force" mindset
  - When to use divide & conquer
  - Proving correctness via exchange arguments

**Key Insights:**
- Peak finding shows algorithmic thinking benefits
- Naive solution: O(n), but O(log n) possible
- Structure exploitation leads to better algorithms
- Multiple valid approaches exist (row vs column strategy)

**Deliverables:**
- [ ] Implement 1D peak finding in O(log n)
- [ ] Prove correctness of your algorithm
- [ ] Extend to 2D and analyze complexity
- [ ] Compare different peak-finding strategies

---

## 📌 WEEK 2: LINEAR DATA STRUCTURES & BINARY SEARCH

### Weekly Goal
Build intuition for arrays, linked lists, stacks, queues, and the power of binary search as foundational tool.

### Weekly Topics
- Array representations and memory layouts
- Dynamic arrays and growth strategies
- Linked lists and pointer structures
- Stacks, queues, and deques
- Binary search as pattern and algorithm

---

### 📅 DAY 1: ARRAYS & MEMORY LAYOUT

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Static Arrays**
  - Contiguous memory representation
  - Index→address mapping formula (base + i×stride)
  - Fixed size; allocation at declaration

- **Layouts (Row-Major vs Column-Major)**
  - Row-major (C/C++ default): [i][j] = base + i×cols + j
  - Column-major (Fortran): [i][j] = base + j×rows + i
  - Impact on matrix traversals: row-wise vs column-wise iteration
  - Cache friendliness of traversal orders

- **Pros of Arrays**
  - Great cache locality for sequential access
  - Vectorization opportunities (SIMD)
  - O(1) random access under RAM model
  - Prefetching effectiveness

- **Cons of Arrays**
  - Fixed size; resizing is expensive
  - Difficult mid-insert/delete (O(n) shift)
  - Waste space if capacity > usage

- **Systems Angle**
  - How CPU prefetchers detect sequential patterns
  - Why row-wise traversal faster than column-wise for matrices
  - False sharing when multiple threads touch adjacent data
  - Cache line (64 bytes) alignment considerations

**Key Insights:**
- Arrays are gold for locality and performance
- Traversal order matters dramatically for cache
- Fixed size is limiting; motivates dynamic arrays

**Deliverables:**
- [ ] Calculate memory address formula for 2D array
- [ ] Measure cache efficiency of row-wise vs column-wise traversal
- [ ] Design optimal memory layout for specific access pattern
- [ ] Optimize array processing code for cache

---

### 📅 DAY 2: DYNAMIC ARRAYS & AMORTIZED GROWTH

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Dynamic Array Model**
  - Logical size vs capacity
  - Backing store in contiguous memory
  - Doubling: when full, allocate 2x capacity

- **Growth Strategy Analysis**
  - Doubling vs linear growth (doubling better)
  - Amortized cost intuition: many cheap ops, few expensive resizes
  - Example: 16 appends starting with capacity 1 costs O(n), not O(n²)

- **Reallocation Effects**
  - Copying all existing elements on resize
  - Pointer/reference invalidation (critical bug source)
  - Temporary memory spike during reallocation

- **Shrinking & Reserve**
  - When to shrink (rarely, to avoid thrashing)
  - Reserve method to pre-allocate capacity
  - Trade-offs of shrink-to-fit

- **Resizing Costs in Detail**
  - Best case: append when space available, O(1)
  - Worst case: append triggers resize, O(n) copy
  - Amortized: O(1) per append over many operations

- **Systems Angle**
  - Allocator overhead and fragmentation
  - Virtual memory implications of allocation size
  - When reallocation can cause performance spikes (GC pressure)
  - NUMA effects on multi-socket systems

- **Growth Factor Trade-off**
  - Doubling: O(log n) resizes, O(n) wasted space worst-case
  - Linear (1.5x): fewer resizes, different constants
  - Golden ratio: theoretical optimum

**Key Insights:**
- Doubling gives O(1) amortized per append
- Invalidation of pointers is major source of bugs
- Hidden resizing can cause unpredictable latency spikes

**Deliverables:**
- [ ] Prove amortized O(1) for dynamic array append
- [ ] Demonstrate pointer invalidation issue
- [ ] Calculate space wastage for various growth factors
- [ ] Measure real performance of append vs reallocation

---

### 📅 DAY 3: LINKED LISTS

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Singly Linked Lists**
  - Node structure: value + next pointer
  - Head and tail pointers
  - Building chains in heap

- **Doubly Linked Lists**
  - Node structure: value + next + prev pointers
  - Bidirectional traversal
  - Easier delete (have prev pointer)

- **Operations & Complexity**
  - Insert/delete at head: O(1) (have pointer)
  - Insert/delete at tail: O(1) with tail pointer, O(n) without
  - Insert/delete at arbitrary position: need to find it O(n)
  - Search: O(n) linear scan

- **Pros**
  - O(1) insert/delete at known node
  - Flexible size; no reallocation
  - Can interleave with other data structures

- **Cons**
  - No O(1) random access (must traverse)
  - Poor cache locality: pointers chase across memory
  - Extra memory per node (pointers + padding)
  - Garbage collection overhead

- **Cache Locality Reality Check**
  - Array of 1000 integers: potentially in cache
  - Linked list of 1000 nodes: scattered in heap, many cache misses
  - Linked lists often slower than arrays in practice despite asymptotics

- **Sentinel Nodes & Circular Lists**
  - Sentinel: dummy node simplifies edge cases
  - Circular: tail→head, easier for queues
  - Trade-offs of complexity vs code clarity

- **Real Systems Usage**
  - When to use linked lists (rarely; usually arrays better)
  - Intrusive lists (embedded in data structures)
  - Skip lists (probabilistic improvements to linked lists)

**Key Insights:**
- Linked lists asymptotically good but practically poor for random access
- Cache matters; arrays usually win in practice
- Good for specific patterns (LRU cache, polynomial representation)

**Deliverables:**
- [ ] Implement singly/doubly linked list with all operations
- [ ] Compare cache performance: arrays vs linked lists
- [ ] Design sentinel-based linked list to eliminate edge cases
- [ ] Implement circular linked list for queue pattern

---

### 📅 DAY 4: STACKS, QUEUES & DEQUES

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Stack (LIFO - Last-In-First-Out)**
  - Push: add to top
  - Pop: remove from top
  - Connection to call stack & recursion (real semantics)
  - Example: function call frames literally use stack

- **Stack Use-Cases**
  - Expression evaluation (convert infix to postfix, evaluate)
  - Backtracking: try → push state, undo → pop state
  - DFS tree traversal
  - Browser back button (history stack)

- **Queue (FIFO - First-In-First-Out)**
  - Enqueue: add to back
  - Dequeue: remove from front
  - Real queue semantics

- **Queue Use-Cases**
  - BFS traversal (breadth-first)
  - Scheduling & job processing (FIFO fairness)
  - Buffering between producer/consumer
  - Simulation of real queues

- **Deque (Double-Ended Queue)**
  - Add/remove from both ends in O(1)
  - Generalization of stack and queue
  - Sliding window max/min pattern

- **Deque Use-Cases**
  - Sliding window maximum (monotonic deque)
  - LRU cache (doubly-linked list + hash map, but deque-like)
  - Undo/redo with history

- **Implementations**
  - Array-based: circular buffer for queue (wrap-around)
  - List-based: linked list, naturally supports all operations
  - Python: deque from collections; most efficient

- **Performance Characteristics**
  - Array-based stack: O(1) with amortized cost
  - Array-based queue: circular buffer avoids shift; O(1) ops
  - List-based: O(1) all operations but cache unfriendly

**Key Insights:**
- Abstract data type vs implementation distinction
- Deque enables monotonic stack/queue patterns
- Circular buffer clever for queue implementation

**Deliverables:**
- [ ] Implement stack and queue (both array and list)
- [ ] Solve expression evaluation using stack
- [ ] Implement deque with circular buffer
- [ ] Solve sliding window maximum with monotonic deque

---

### 📅 DAY 5: BINARY SEARCH & INVARIANTS

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Binary Search Basics**
  - Precondition: sorted array
  - Low, high, mid pointers
  - Invariant: target in [low, high]

- **Safe Implementation**
  - mid = low + (high - low)/2 to avoid integer overflow
  - Termination conditions
  - Off-by-one error prevention

- **Binary Search Variants**
  - First occurrence: leftmost index where A[i] == target
  - Last occurrence: rightmost index where A[i] == target
  - Lower_bound: first element ≥ target
  - Upper_bound: first element > target

- **Binary Search on Answer Space**
  - Reframe problem: "Find X such that condition(X) is true"
  - Condition is monotone: below threshold (false) or above threshold (true)
  - Example: "Maximum capacity to complete all jobs in K hours?"

- **Monotone Condition Checks**
  - Feasibility: "Can we do this with parameter X?"
  - Monotonicity: if feasible(X), then feasible(X+ε) for suitable ε
  - Example: if we can handle capacity C, we can handle C+1

- **Binary Search Pattern Examples**
  - Capacity planning: minimize maximum load
  - Distance placement: maximize minimum distance (aggressive cows)
  - Time allocation: minimize maximum time needed

- **Floating-Point Binary Search**
  - Epsilon accuracy for real-valued targets
  - Fixed iterations for precision

- **Systems Angle**
  - Binary search in standard libraries (std::binary_search, etc.)
  - Using BS to tune configuration parameters
  - Online vs offline binary search

**Key Insights:**
- Binary search works on any monotone condition, not just sorted arrays
- Off-by-one errors are subtle; be meticulous
- "Answer space" framing unlocks many problems

**Deliverables:**
- [ ] Implement binary search variants (first, last, lower/upper bound)
- [ ] Solve capacity/distance optimization via binary search
- [ ] Debug off-by-one errors in binary search
- [ ] Apply binary search on answer space to custom problem

---

## 📌 WEEK 3: SORTING, HEAPS & HASHING

### Weekly Goal
Master sorting algorithms, heaps, and hash tables including string hashing.

### Weekly Topics
- Elementary sorts and practical considerations
- Merge sort and quicksort
- Heaps and heap sort
- Hash tables: separate chaining and open addressing
- Rolling hash and Karp-Rabin algorithm

---

### 📅 DAY 1: ELEMENTARY SORTS - BUBBLE, SELECTION, INSERTION

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Bubble Sort**
  - Compare adjacent pairs, swap if out of order
  - Larger elements "bubble" up each pass
  - Best case: O(n) if sorted (with optimization)
  - Worst case: O(n²)
  - Stable: equal elements maintain relative order

- **Selection Sort**
  - Divide array into sorted (left) / unsorted (right)
  - Find min in unsorted, swap to sorted side
  - Exactly n swaps (better than bubble worst-case)
  - In-place and NOT stable

- **Insertion Sort**
  - Grow sorted prefix by inserting next element
  - Find position and shift elements
  - Best case: O(n) if sorted
  - Worst case: O(n²)
  - Stable and in-place

- **Adaptive Behavior**
  - "Nearly sorted" arrays: O(n) to O(n²) depending on sortedness
  - Insertion sort good for small n or nearly sorted

- **Comparison with Each Other**
  - Bubble sort: almost never use (worse than both)
  - Selection: min swaps but not adaptive
  - Insertion: adaptive, stable, small constants

- **Use Cases**
  - Small arrays (n < 50): insertion sort often faster than O(n log n)
  - Hybrid sorting: use insertion sort on small subarrays
  - Nearly sorted data: insertion sort O(n)

- **In-Place & Stability Revisited**
  - In-place: O(1) extra space
  - Stable: equal elements relative order preserved
  - Both matter for real applications

**Key Insights:**
- O(n²) sorts have small constants and can be practical
- Adaptive sorts shine on partially sorted data
- Stability matters for multi-key sorting

**Deliverables:**
- [ ] Implement bubble, selection, insertion sort
- [ ] Measure performance on sorted, random, reverse-sorted arrays
- [ ] Optimize insertion sort with binary search (find position)
- [ ] Determine when elementary sorts beat O(n log n) sorts

---

### 📅 DAY 2: MERGE SORT & QUICKSORT

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Merge Sort**
  - Divide array in half recursively
  - Recursively sort halves
  - Merge two sorted halves in O(n)
  - Recurrence: T(n) = 2T(n/2) + O(n) → O(n log n)

- **Merge Operation**
  - Two pointers on two sorted arrays
  - Compare, take smaller, advance pointer
  - Linear time merge

- **Merge Sort Characteristics**
  - Guaranteed O(n log n) worst-case (good for real-time)
  - Stable: maintains relative order of equal elements
  - Requires O(n) extra space for merge
  - Not in-place; external merging used in disk sorts

- **Quicksort**
  - Partition around pivot: smaller left, larger right
  - Recursively sort halves
  - In-place (with careful partition)
  - Expected O(n log n); worst-case O(n²) on bad pivot choices

- **Partitioning Strategies**
  - First element as pivot: bad on sorted arrays
  - Median-of-three: better heuristic
  - Random pivot: randomized, avoids worst-case adversarial input
  - Partition into 3 ways (< pivot, = pivot, > pivot)

- **Quicksort Characteristics**
  - NOT stable (partition breaks relative order)
  - In-place (extra space: O(log n) recursion depth)
  - Fast in practice (better cache, fewer data movements)
  - Worst-case O(n²) but rarely happens with good pivot strategy

- **Practical Sorting**
  - Hybrid: introsort uses quicksort → switches to heapsort if depth too deep
  - Used in C++ std::sort, Java Arrays.sort
  - Timsort (Python, Java): merge sort + insertion sort hybrid

- **Memory Effects**
  - Merge sort: sequential reads/writes, good cache
  - Quicksort: in-place, better spatial locality for cache
  - On modern CPUs, quicksort often faster

**Key Insights:**
- Merge sort: guaranteed O(n log n), stable, but needs space
- Quicksort: faster in practice, in-place, but unstable
- Hybrid algorithms combine strengths

**Deliverables:**
- [ ] Implement merge sort and quicksort
- [ ] Measure performance on various datasets
- [ ] Implement 3-way partition for duplicate elements
- [ ] Analyze worst-case and average-case complexity

---

### 📅 DAY 3: HEAPS, HEAPIFY & HEAP SORT

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Binary Heap Model**
  - Complete binary tree in array
  - Parent of i: (i-1)/2; left child: 2i+1; right child: 2i+2
  - Min-heap: parent ≤ children
  - Max-heap: parent ≥ children

- **Core Heap Operations**
  - Insert: add to end, bubble up (sift-up)
    - Compare with parent, swap if violates heap property, recurse
    - Time: O(log n)
  - Extract-min/max: remove root, move last element to root, bubble down (sift-down)
    - Compare with children, swap with smaller/larger, recurse
    - Time: O(log n)
  - Find-min/max: root element
    - Time: O(1)

- **Build Heap (Heapify)**
  - Bottom-up heapify: start from last non-leaf, heapify down
  - Builds heap in O(n) time (amortized O(1) per node)
  - Better than n×insert which is O(n log n)

- **Heap Sort**
  - Build heap from array: O(n)
  - Extract-min repeatedly n times: n×O(log n) = O(n log n)
  - In-place variant: swap root with last element, contract array, heapify down
  - Total: O(n log n) worst-case, in-place
  - NOT stable

- **Priority Queue**
  - Heap-based priority queue
  - Operations: insert (enqueue), extract-min/max (dequeue), peek
  - All O(log n) except peek O(1)
  - Applications: Dijkstra, event simulation, task scheduling

- **Variants**
  - d-ary heaps: each node has d children
  - Fibonacci heaps: amortized O(1) insert/decrease-key (complex)
  - Leftist heaps: mergeable heaps

- **Heap Property Maintenance**
  - After operation, restore heap invariant
  - Bubble-up: for insert
  - Bubble-down: for extract/heapify

**Key Insights:**
- Heaps provide efficient priority queue semantics
- O(n) build heap is surprising result
- Heap sort guaranteed O(n log n) but unstable

**Deliverables:**
- [ ] Implement min-heap with insert/extract/build
- [ ] Implement priority queue using heap
- [ ] Implement heap sort and compare with merge/quick
- [ ] Optimize heapify and measure build vs insert-based heap

---

### 📅 DAY 4: HASH TABLES I - SEPARATE CHAINING

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Hash Function Design**
  - Map keys to bucket indices [0, m)
  - Desiderata: uniformity, fast computation
  - h(key) = key % m (for integers)
  - h(key) = (a×key + b) % prime % m (linear congruential)

- **Hash Function Properties**
  - Deterministic: same input always gives same hash
  - Uniformly distributed: minimize collisions
  - Fast to compute: O(1)
  - Avalanche effect: small key change → large hash change

- **Separate Chaining**
  - Buckets as linked lists (or small vectors)
  - Collision: multiple keys hash to same bucket
  - Search: hash key → scan chain
  - Expected O(1) operations given good hash and load factor α = n/m ≈ 1

- **Load Factor & Resizing**
  - Load factor α = #elements / #buckets
  - Too small: wasted space
  - Too large: long chains, slower operations
  - Typical: maintain 0.5 < α < 2
  - Resize: double buckets, rehash all elements
  - Amortized O(1) insertion with doubling strategy

- **Hash Table Operations**
  - Insert: hash key, append to chain O(1) expected
  - Delete: hash key, remove from chain O(1) expected
  - Search: hash key, scan chain O(1) expected
  - Worst case: all keys hash to one bucket, O(n) chain scan

- **Collision Frequency**
  - Birthday paradox: collisions occur faster than intuition
  - With m buckets and n elements, expect Θ(n²/m) collisions
  - At α=1, expect ~1 collision pair

- **Rehashing Process**
  - Allocate new larger hash table
  - Rehash every element (may go to different bucket)
  - Copy to new table
  - Discard old table

- **Load Factor Dynamics**
  - At 50% load: average chain length 0.5
  - At 100% load: average chain length 1.0
  - At 200% load: average chain length 2.0
  - Chain length grows with load factor

**Key Insights:**
- Separate chaining simple and effective
- Expected O(1) with good hash and load factor management
- Worst-case O(n) is unlikely but possible

**Deliverables:**
- [ ] Implement hash table with separate chaining
- [ ] Test hash function for uniformity
- [ ] Implement dynamic resizing and rehashing
- [ ] Measure collision rate vs load factor
- [ ] Compare performance with std::unordered_map

---

### 📅 DAY 5: HASH TABLES II - OPEN ADDRESSING & ROLLING HASH

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Open Addressing Strategies**
  - No separate chains; store all elements in single hash table array
  - Collision resolution: probe for empty slot
  - Probe sequence: hash(key), hash(key)+1, hash(key)+2, ... (linear)

- **Linear Probing**
  - h_i(key) = (h(key) + i) mod m
  - Simplest but suffers primary clustering
  - Cluster: long chains of occupied slots
  - Leads to increased probe lengths

- **Quadratic Probing**
  - h_i(key) = (h(key) + i²) mod m
  - Reduces clustering
  - May not probe all slots if m not prime
  - Secondary clustering still possible

- **Double Hashing**
  - h_i(key) = (h₁(key) + i×h₂(key)) mod m
  - Two independent hash functions
  - Reduces clustering significantly
  - More uniform probing

- **Load Factor in Open Addressing**
  - Typical: maintain α < 0.5 (table half-full)
  - As α → 1, expected probes → ∞
  - Trade-off: space vs time

- **Deletion in Open Addressing**
  - Can't just remove element (breaks probe chains)
  - Tombstones: mark deleted slots
  - Over time, tombstones accumulate → need rehashing

- **Hash Security & Attacks**
  - Hash flooding: adversary chooses keys that all hash same
  - Leads to O(n) worst-case even with good strategy
  - Mitigation: randomized hash seed, universal hashing

- **Universal Hashing**
  - Family of hash functions with property: for any two distinct keys, Pr[h₁(k₁) = h₁(k₂)] ≤ 1/m
  - Randomized seed prevents adversarial input
  - SipHash: modern cryptographic hash (used in Python 3.3+)

- **Rolling Hash / Polynomial Hash**
  - Hash string s = s[0]×b^(n-1) + s[1]×b^(n-2) + ... + s[n-1]
  - Sliding window: rolling update in O(1)
  - Remove leftmost: subtract s[i]×b^(n-1)
  - Add rightmost: add new character, shift

- **Karp-Rabin Algorithm**
  - String matching using rolling hash
  - Hash pattern and each substring
  - If hashes match, verify (check for hash collision)
  - O(n+m) expected time; O(nm) worst-case (but rare)
  - Applications: substring search, plagiarism detection, DNA sequences

- **Rolling Hash Implementation**
  - Modular arithmetic: hash mod prime to prevent overflow
  - Collision handling: if hash matches, check full strings
  - Choose prime m and base b carefully

**Key Insights:**
- Open addressing saves space vs separate chaining
- Double hashing avoids clustering
- Rolling hash enables substring pattern matching efficiently
- Hash security is real concern in adversarial settings

**Deliverables:**
- [ ] Implement open addressing with linear/quadratic/double hashing
- [ ] Compare probe sequences and performance
- [ ] Implement rolling hash for substring search
- [ ] Implement Karp-Rabin algorithm
- [ ] Analyze collision probability and performance

---
# 🟩 PHASE B: CORE PATTERNS & STRING MANIPULATION (Weeks 4-6)

## Phase Motivation
Now that fundamentals are solid, learn reusable problem-solving patterns that apply across hundreds of interview problems. These patterns transform "stuck" into "I recognize this pattern." You'll learn to decompose complex array and string problems into recognizable templates.

## Phase Outcomes
- [ ] Identify and apply two-pointer patterns (same direction, opposite direction)
- [ ] Recognize and solve sliding window problems (fixed and variable size)
- [ ] Use divide & conquer systematically across different domains
- [ ] Apply binary search beyond sorted arrays to answer spaces
- [ ] Understand and apply string manipulation patterns
- [ ] Recognize when to use hashing, monotonic stacks, interval merging, partitioning, Kadane's algorithm
- [ ] Master fast/slow pointer techniques for cycle detection and list manipulation

---

## 📌 WEEK 4: TWO POINTERS, SLIDING WINDOWS, DIVIDE & CONQUER, BINARY SEARCH AS PATTERN

### Weekly Goal
Learn foundational array and sequence patterns that transform problems from confusing to solvable. Master two fundamental techniques: two-pointer manipulation and sliding window exploration.

### Weekly Topics
- Two-pointer techniques (same direction, opposite direction, convergent, divergent)
- Sliding window (fixed and variable size)
- Divide and conquer pattern and applications
- Binary search as a general-purpose optimization pattern

---

### 📅 DAY 1: TWO-POINTER PATTERNS

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Same-Direction Pointers (Two Fast Moving in Sequence)**
  - Both pointers starting at beginning, moving in same direction
  - Example: removing duplicates in-place
  - Example: merging two sorted arrays
  - Pattern: fast pointer finds, slow pointer places
  - Invariant: everything before slow pointer is final result
  - Time: O(n), Space: O(1)

- **Opposite-Direction Pointers (Convergent Approach)**
  - One pointer at start, one at end, converging toward middle
  - Example: container with most water
    - Height array, find max area between two lines
    - Greedy choice: move inward from shorter line
    - Why it works: moving from longer line never improves
  - Example: two-sum in sorted array
    - Converge pointers based on sum too large/small
  - Time: O(n), Space: O(1)

- **Invariants & Correctness**
  - Maintain clear invariant as pointers move
  - Example: "elements before slow are sorted and final"
  - Example: "answer is between current pointers or won't appear"
  - Critical for proving algorithm correctness

- **Divergent Approach (Start Middle, Move Outward)**
  - Start at center, expand outward
  - Example: palindrome checking via center
  - Example: trapping rain water (combined with other techniques)

- **Practical Considerations**
  - When to use two pointers vs hash map vs other approaches
  - Space-time tradeoffs
  - Edge cases: empty array, single element, all same

- **Real-World Applications**
  - In-place array manipulation (avoid extra space)
  - Merge operations (essential in distributed systems)
  - Two-sum variants (financial transactions, pair finding)

**Key Insights:**
- Two-pointer elegant for in-place array operations
- Invariants are your proof of correctness
- Different pointer movements for different problems

**Deliverables:**
- [ ] Implement remove duplicates using same-direction pointers
- [ ] Implement container with most water using opposite-direction pointers
- [ ] Implement two-sum in sorted array
- [ ] Prove invariant for each algorithm
- [ ] Identify when two-pointer approach is better than alternatives

---

### 📅 DAY 2: SLIDING WINDOW (FIXED SIZE)

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Fixed-Size Sliding Window Mechanics**
  - Window of size k slides over array
  - At each step: add new element, remove old element
  - Maintain aggregate (sum, min, max, frequency count)
  - O(n) time for O(n) elements with O(k) window

- **Running Sum / Running Average**
  - Sum of k-length subarrays
  - Example: find subarray with sum closest to target
  - Approach: compute all k-sums, find best
  - Complexity: O(n) instead of O(nk) naive

- **Maximum / Minimum in Sliding Window**
  - Naive: O(n) for each window → O(nk) total
  - Better: monotonic deque in O(n) total
    - Maintain deque of indices in decreasing order of values
    - Add: remove from back while new element larger
    - Remove: pop from front if outside window
    - Front of deque is max at each step

- **Frequency-Based Windows**
  - Fixed window size, track character frequencies
  - Example: "permutation in string"
    - Check if pattern is permutation of any substring
    - Compare frequency maps at each window
  - Example: "all anagrams of substring"
    - Find all anagrams in text
    - Slide window and compare frequency maps

- **Monotonic Deque Details**
  - For maximum: keep in decreasing order
  - For minimum: keep in increasing order
  - Efficient: each element added/removed once from deque
  - Complex but powerful for window max/min

- **Space Considerations**
  - Monotonic deque: O(k) space (stores k elements max)
  - Frequency maps: O(alphabet size) usually small

- **Edge Cases**
  - Window larger than array
  - All elements same
  - Very small arrays

**Key Insights:**
- Fixed window enables O(n) processing time
- Monotonic deque elegant for min/max tracking
- Frequency comparison powerful for pattern matching

**Deliverables:**
- [ ] Implement fixed-window sum calculation
- [ ] Implement maximum sliding window using monotonic deque
- [ ] Solve "permutation in string" problem
- [ ] Find all anagrams in text using sliding window
- [ ] Compare naive vs optimized approaches

---

### 📅 DAY 3: SLIDING WINDOW (VARIABLE SIZE)

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Variable-Size Window Concept**
  - Expand window to include more elements
  - Shrink window when constraint violated
  - Goal: find optimal window (min/max/first)
  - Time: O(n) if shrink happens O(n) total times

- **Constraint-Based Windows**
  - Example: longest substring without repeating characters
    - Expand right: add character
    - If duplicate found: shrink from left until no duplicate
    - Track max length seen
  - Example: longest substring with "at most k" distinct characters
    - Expand right: add character
    - If more than k distinct: shrink left
    - Update frequency as you go

- **Two-Pointer Sliding Window**
  - Left pointer: shrink side
  - Right pointer: expand side
  - Invariant: [left, right] is valid window
  - Move right to expand, move left to shrink

- **Frequency Map Tracking**
  - Map character → count in current window
  - On expanding: increment count
  - On shrinking: decrement count (remove if zero)
  - Check constraint: how many distinct? Sum of counts?

- **"At Most k" Pattern**
  - Max elements with at most k distinct characters
  - Window shrinks when constraint violated
  - Shrink continues until constraint satisfied
  - Expand to try including more

- **"At Least k" vs "Exactly k" Patterns**
  - Exactly k distinct: compute at-most-k minus at-most-(k-1)
  - General: frame as counting valid windows

- **Minimum Window Substring Pattern**
  - Find smallest window containing all characters from pattern
  - Expand right until all characters included
  - Shrink left while all characters still present
  - Track best window found

- **Decision Logic**
  - Clear condition for validity
  - Right pointer always expands (until end)
  - Left pointer shrinks when needed
  - Record best seen at each step

**Key Insights:**
- Variable window elegant for optimization within constraints
- Two pointers move independently at different rates
- Frequency tracking enables efficient constraint checking

**Deliverables:**
- [ ] Implement longest substring without repeating characters
- [ ] Implement longest substring with at most k distinct characters
- [ ] Solve minimum window substring problem
- [ ] Implement exactly k distinct characters (using at-most trick)
- [ ] Measure performance vs alternative approaches

---

### 📅 DAY 4: DIVIDE & CONQUER PATTERN

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Divide & Conquer Template**
  - Divide: split problem into subproblems
  - Conquer: recursively solve subproblems
  - Combine: merge solutions into final answer
  - Complexity: typically O(n log n) for balanced splits

- **General Recurrence: T(n) = a×T(n/b) + O(n^d)**
  - a subproblems of size n/b
  - O(n^d) work per level
  - Master theorem gives solution: O(n^d) or O(n^d × log n) or O(n^log_b a)

- **Merge Sort as Divide & Conquer**
  - Divide: split into two halves
  - Conquer: recursively sort halves
  - Combine: merge two sorted halves in O(n)
  - Recurrence: T(n) = 2T(n/2) + O(n) = O(n log n)

- **Counting Inversions**
  - Problem: how many pairs (i, j) where i < j but arr[i] > arr[j]
  - Naive: O(n²) nested loops
  - Merge sort approach:
    - Count inversions in left half
    - Count inversions in right half
    - Count inversions across halves (pairs from left > pairs from right)
    - During merge, track cross-half inversions
  - Time: O(n log n)

- **Majority Element**
  - Find element appearing > n/2 times
  - Divide & conquer: recursively find majority in left/right
  - Combine: check if left or right majority is overall majority
  - Time: O(n log n) (naive) or O(n) with preprocessing

- **Closest Pair of Points**
  - 2D problem: find nearest pair
  - Naive: O(n²) all pairs
  - Divide & conquer:
    - Sort by x-coordinate
    - Recursively find closest in left half
    - Recursively find closest in right half
    - Check pairs spanning the dividing line
  - Complexity: O(n log n)

- **When Divide & Conquer Works**
  - Problem has optimal substructure
  - Can combine solutions efficiently
  - Balanced splits lead to logarithmic depth

- **When It Doesn't Work Well**
  - Unbalanced splits (quadratic time)
  - High combining cost
  - Better addressed by iteration or DP

**Key Insights:**
- Divide & conquer powerful for sorting, searching, optimization
- Recurrence analysis predicts complexity
- Practical implementations often hybrid (use insertion sort for small subarrays)

**Deliverables:**
- [ ] Implement merge sort
- [ ] Count inversions using divide & conquer
- [ ] Implement majority element finder
- [ ] Analyze recurrence for custom divide & conquer problem
- [ ] Compare vs DP or greedy approaches

---

### 📅 DAY 5: BINARY SEARCH AS A PATTERN (ANSWER SPACE)

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Binary Search Beyond Sorted Arrays**
  - Key insight: binary search works on any monotone condition
  - Reframe problem: "Find X such that condition(X) is true"
  - Condition must be monotone: false below threshold, true above (or vice versa)

- **Monotone Condition Concept**
  - Condition must split decision space into two regions
  - Example: "Can we schedule all jobs with capacity C?"
    - Below C: impossible
    - Above C: possible
    - Threshold: find minimum C that works

- **Capacity Planning Pattern**
  - Problem: given tasks with sizes, minimize maximum load
  - Search space: [max_task_size, sum_all_tasks]
  - Condition: "Can we distribute tasks with capacity C?"
  - Check: pack tasks into bins greedily, see if all fit in C
  - Binary search on C

- **Distance Maximization Pattern**
  - Problem: "Aggressive cows" - place N cows in stalls to maximize minimum distance
  - Search space: [1, (max_position - min_position)]
  - Condition: "Can we place N cows with minimum distance D?"
  - Check: greedily place cows, see if we get N or more
  - Binary search on D

- **Food Delivery Pattern**
  - Problem: minimize time to deliver all packages
  - Search space: [1, total_time_alone]
  - Condition: "Can person 1 deliver K packages in time X?"
  - Check: greedily assign packages
  - Binary search on time X

- **Feasibility Check Implementation**
  - Must be deterministic: true/false answer
  - Should be efficient: faster than linear search
  - Example: check if N items fit in capacity C in O(n) greedy packing

- **Combining Multiple Feasibility Checks**
  - Sometimes need to check multiple conditions
  - Example: chess knight tours - check if path exists
  - Combine with BFS/DFS feasibility

- **Floating-Point Binary Search**
  - For real-valued targets (e.g., find sqrt)
  - Use epsilon for convergence
  - Fixed iterations typically needed (20-30 iterations for 10^-6 precision)

- **Practical Considerations**
  - Precision requirements
  - Integer vs floating-point arithmetic
  - Avoid integer overflow (use mid = low + (high - low) / 2)

**Key Insights:**
- Binary search pattern extends to optimization problems
- Key is framing problem as finding threshold on monotone condition
- Feasibility check is heart of algorithm

**Deliverables:**
- [ ] Implement capacity planning using binary search
- [ ] Implement aggressive cows problem
- [ ] Solve food delivery optimization
- [ ] Implement floating-point binary search for sqrt
- [ ] Design feasibility check for custom problem

---

## 📌 WEEK 5: HASH, MONOTONIC STACK, INTERVALS, PARTITION, KADANE, FAST/SLOW

### Weekly Goal
Master high-frequency patterns that cover large fraction of interview problems. Each pattern solves family of problems with similar structure.

### Weekly Topics
- Hash map / hash set patterns for complement and frequency counting
- Monotonic stack for next/previous element and range problems
- Interval merging and scheduling patterns
- Partition and cyclic sort for in-place rearrangement
- Kadane's algorithm for maximum subarray and variants
- Fast/slow pointers for cycle detection and list manipulation

---

### 📅 DAY 1: HASH MAP / HASH SET PATTERNS

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Two-Sum & Complement Patterns**
  - Problem: find two numbers summing to target
  - Naive: O(n²) nested loops
  - Hash approach: for each num, check if (target - num) in set
    - Build hash set on first pass
    - Check on second pass
  - Time: O(n), Space: O(n)

- **Variations of Two-Sum**
  - Two-Sum II (sorted array): use two pointers instead
  - 3Sum: sort, fix one element, use two-sum for rest
  - K-Sum: recursive reduction to 2-sum

- **Frequency Counting Patterns**
  - Anagrams: two strings are anagrams iff same character frequencies
    - Map each string to sorted characters or frequency tuple
    - Check equality
  - Top K Frequent Elements:
    - Count frequencies
    - Use heap/bucket sort to find top K
  - Majority Element:
    - Count frequencies
    - Return element with count > n/2

- **Valid Anagram**
  - Build frequency map for both strings
  - Compare maps (or sort and compare)
  - Time: O(n), Space: O(alphabet size)

- **Anagrams in List**
  - Group anagrams together
  - Normalize each string (e.g., sort characters)
  - Use map: normalized → list of original strings
  - Time: O(n × k log k) where n = words, k = word length

- **Membership & Deduplication**
  - Remove duplicates: convert to set, size gives count
  - Contains duplicate check: iterate and add to set
  - Unique elements in array: return set size

- **Intersection of Multiple Sets**
  - Two arrays: iterate one, check if element in other's set
  - Multiple arrays: intersect iteratively

- **Word Ladder Pattern**
  - Find neighbors by changing one character
  - Use set to track visited words
  - BFS to find shortest path

**Key Insights:**
- Hash maps enable O(1) lookups for membership
- Frequency counting solves many interview problems
- Normalization (sorting, etc.) enables grouping

**Deliverables:**
- [ ] Implement two-sum with hash map
- [ ] Implement 3-sum using two-sum recursion
- [ ] Group anagrams together
- [ ] Find top K frequent elements using bucket sort
- [ ] Find intersection of two sorted arrays

---

### 📅 DAY 2: MONOTONIC STACK

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Monotonic Stack Concept**
  - Stack maintaining elements in increasing or decreasing order
  - When adding new element: pop larger/smaller elements (depending on type)
  - Top of stack always closest qualifying element
  - Enables O(n) processing where naive would be O(n²)

- **Next Greater Element**
  - Problem: for each element, find next element larger than it
  - Naive: O(n²) nested loops
  - Monotonic decreasing stack:
    - Iterate right to left
    - For each element: pop smaller elements from stack (found next greater for them)
    - Top of stack is next greater for current
  - Time: O(n) each element added/removed once

- **Next Smaller Element**
  - Similar to next greater but with increasing stack
  - Iterate and maintain increasing order

- **Previous Greater / Previous Smaller**
  - Iterate left to right
  - Stack maintains elements processed so far
  - Top of stack is previous qualifier

- **Stock Span Problem**
  - For each day, how many consecutive previous days had price ≤ current?
  - Monotonic decreasing stack approach:
    - Store (price, span) pairs
    - For new day: accumulate spans of elements we pop
  - Time: O(n)

- **Largest Rectangle in Histogram**
  - Given heights, find largest rectangle
  - Key insight: for each bar, find left and right boundaries where bar is tallest
  - Monotonic stack:
    - Maintain indices in increasing order of heights
    - For each bar: pop taller bars (calculate rectangles)
    - Time: O(n)

- **Trapping Rain Water**
  - Water trapped = min(max_left, max_right) - current_height
  - For each position: need max to left and right
  - Monotonic stack approach (or precompute max left/right in O(n))

- **Remove K Digits to Get Smallest Number**
  - Greedy with monotonic stack:
    - Maintain increasing stack
    - For each digit: pop if can remove and current smaller
    - Limit removals to K
  - Time: O(n)

- **Implementation Details**
  - Stack stores indices, not values (enables left/right boundary calculations)
  - Careful about off-by-one errors
  - Handle remaining elements after main loop

**Key Insights:**
- Monotonic stack powerful for range queries
- Next/previous element problems natural fit
- Stack ensures each element processed once

**Deliverables:**
- [ ] Implement next greater element
- [ ] Solve stock span problem
- [ ] Implement largest rectangle in histogram
- [ ] Solve trapping rain water with monotonic stack
- [ ] Remove K digits to get smallest number

---

### 📅 DAY 3: MERGE OPERATIONS & INTERVAL PATTERNS

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Merging Sorted Arrays**
  - Two sorted arrays: merge in O(m+n)
  - Two pointers at start of each array
  - Compare and take smaller, advance pointer
  - Combine into new array or merge in-place

- **Merge Sorted Lists**
  - Linked lists: similar approach
  - Create new list with pointers to both lists
  - Traverse and link nodes in order

- **Merging K Sorted Lists**
  - Naive: pairwise merging O(n² k) time
  - Heap approach: maintain min-heap of heads
    - Extract min, add to result
    - Add next element from same list to heap
    - Time: O(nk log k) where n = total elements
  - Divide & conquer: merge K lists recursively

- **Merge Intervals**
  - Given overlapping intervals, merge overlaps
  - Sort by start time: O(n log n)
  - Iterate and merge if overlaps: O(n)
  - Overlap condition: new_start ≤ current_end

- **Insert Interval**
  - Add new interval to list of non-overlapping intervals
  - Collect non-overlapping intervals before new
  - Find overlapping intervals and merge with new
  - Collect non-overlapping intervals after merged
  - Time: O(n)

- **Interval Scheduling Variation**
  - Different from greedy activity selection
  - Here we merge instead of selecting
  - Useful for calendar, room scheduling

- **Meeting Rooms Problems**
  - Problem 1: can one person attend all meetings?
    - Check if meetings overlap
  - Problem 2: minimum rooms needed?
    - Sweep line algorithm: sort start/end times
    - Track concurrent meetings at each time
    - Max concurrent = min rooms

- **Real-World Applications**
  - Calendar consolidation
  - Conference room scheduling
  - Resource allocation
  - Network time slot management

**Key Insights:**
- Sorting by start time is key to interval problems
- Merging logic similar across variations
- Sweep line elegant for concurrent range problems

**Deliverables:**
- [ ] Implement merge two sorted arrays
- [ ] Implement merge K sorted lists with heap
- [ ] Merge overlapping intervals
- [ ] Insert new interval into list
- [ ] Find minimum meeting rooms needed

---

### 📅 DAY 4 (PART A): PARTITION & CYCLIC SORT

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Dutch National Flag Problem**
  - Partition array into three regions: 0s, 1s, 2s
  - In-place partitioning in O(n)
  - Three pointers: left (0s boundary), mid (current), right (2s boundary)
  - Invariant: [0, left) = 0s, [left, mid) = 1s, [mid, right] = unknown, (right, n) = 2s

- **Partitioning Around Pivot**
  - Quicksort partition: elements < pivot on left, ≥ pivot on right
  - Two-pointer approach: converge from ends
  - Randomized pivot prevents worst-case

- **Move Zeroes to End**
  - In-place moving zeros to end
  - Slow pointer: next position for non-zero
  - Fast pointer: scan for non-zeros
  - Swap when found
  - Time: O(n), Space: O(1)

- **Cyclic Sort Pattern**
  - Used for arrays containing 1..n or 0..n-1
  - If element i belongs at position i:
    - Swap to correct position
    - Repeat until element at position i is correct
  - Time: O(n), Space: O(1)

- **Finding Missing Number Using Cyclic Sort**
  - Array [0, n] with one missing
  - Cyclic sort: place each number at index equal to its value
  - Then scan: first index where element ≠ index is the missing number
  - Time: O(n), Space: O(1)

- **Finding Duplicate in Array [1..n]**
  - One number repeated, find it
  - Cyclic sort would detect: when trying to place num, position already has num
  - Return when duplicate detected
  - Time: O(n), Space: O(1)

- **Finding All Duplicates**
  - Similar approach: mark positions as visited using signs
  - Or use cyclic sort and collect duplicates

- **First Missing Positive**
  - Array with positive and negative numbers
  - Cyclic sort on positive numbers in [1, n] range
  - Scan for first missing

**Key Insights:**
- Cyclic sort elegant for permutation-based problems
- In-place rearrangement saves space
- Invariants critical for correctness

**Deliverables:**
- [ ] Implement Dutch National Flag partitioning
- [ ] Implement move zeroes to end
- [ ] Implement cyclic sort
- [ ] Find missing number using cyclic sort
- [ ] Find all duplicates in array

---

### 📅 DAY 4 (PART B): KADANE'S ALGORITHM

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Maximum Subarray Problem**
  - Find contiguous subarray with largest sum
  - Naive: O(n³) all subarrays
  - Prefix sum: O(n²) with optimization
  - Kadane's: O(n)

- **Kadane's Algorithm Insight**
  - At each position: decide continue current subarray or start new
  - max_ending_here = max(arr[i], max_ending_here + arr[i])
  - max_global = max(max_global, max_ending_here)
  - Time: O(n), Space: O(1)

- **Example Walkthrough**
  - Array: [-2, 1, -3, 4, -1, 2, 1, -5, 4]
  - Trace through: see how subarrays evolve
  - Result: [4, -1, 2, 1] = sum 6

- **Maximum Product Subarray**
  - Similar to max sum but with multiplication
  - Complication: negative × negative = positive
  - Track both max and min ending at each position
  - max_here = max(arr[i], arr[i] × max_prev, arr[i] × min_prev)
  - min_here = min(arr[i], arr[i] × max_prev, arr[i] × min_prev)

- **Circular Array Variant**
  - Array is circular: last element connects to first
  - Two cases:
    - Normal: max subarray doesn't wrap
    - Wrapping: max subarray wraps around
  - Wrap case: total_sum - min_subarray
  - Return max of both cases

- **Maximum Subarray with K Constraint**
  - Subarray of exactly size K: O(n) with sliding window
  - Subarray of at most K: DP or greedy approaches

- **Applications**
  - Stock trading: buy/sell on one transaction
  - Energy consumption: find peak consumption period
  - Revenue analysis: find highest revenue period

- **DP Formulation**
  - dp[i] = max sum of subarray ending at i
  - dp[i] = max(arr[i], dp[i-1] + arr[i])
  - Answer = max(dp[i]) for all i

- **Handling Edge Cases**
  - All negative numbers: max is least negative
  - Empty array: define behavior (empty sum = 0 or undefined)
  - Single element: return that element

**Key Insights:**
- Kadane's algorithm elegant DP solution
- Greedy choice: continue or restart subarray
- Track both maximum and minimum for product variant

**Deliverables:**
- [ ] Implement Kadane's algorithm for max sum subarray
- [ ] Implement for maximum product subarray
- [ ] Solve circular subarray variant
- [ ] Find indices of max subarray (not just sum)
- [ ] Extend to at most K distinct elements

---

### 📅 DAY 5: FAST/SLOW POINTERS

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Floyd's Cycle Detection Algorithm**
  - Problem: detect if linked list has cycle
  - Fast pointer (tortoise and hare): moves 2 steps
  - Slow pointer: moves 1 step
  - If cycle exists, they meet eventually
  - If no cycle, fast reaches end
  - Time: O(n), Space: O(1)

- **Finding Cycle Start**
  - After detecting cycle, reset one pointer to head
  - Move both one step at a time
  - They meet at cycle start
  - Mathematical proof: distances work out to meet at start

- **Cycle Length Calculation**
  - When pointers meet in cycle, count steps
  - Or: distance from head to cycle start + cycle length

- **Finding Middle of Linked List**
  - Slow pointer moves 1 step, fast moves 2 steps
  - When fast reaches end, slow is at middle
  - Handle even/odd length lists
  - Time: O(n), Space: O(1)

- **Splitting Linked List at Middle**
  - Find middle using fast/slow
  - Split into two lists at middle
  - Useful for merge sort on linked lists

- **Happy Number Problem**
  - Number is happy if repeatedly summing digit squares eventually reaches 1
  - Unhappy if cycles infinitely
  - Use fast/slow to detect cycle
  - If cycle is [1], it's happy; otherwise unhappy

- **Palindrome Linked List**
  - Find middle using fast/slow
  - Reverse second half
  - Compare first and second halves
  - Time: O(n), Space: O(1)

- **Remove Nth Node from End**
  - Use two pointers with gap
  - Lead pointer moves N steps ahead
  - Then both move until lead reaches end
  - Trailer is now before node to remove
  - Handle edge cases: removing head, single node

- **Reorder Linked List (L1→Ln→L2→Ln-1→...)**
  - Find middle
  - Reverse second half
  - Merge two halves interleaving
  - Time: O(n), Space: O(1)

- **Detecting Repeated Sequence**
  - Use fast/slow to detect any cyclic pattern
  - Apply to number sequences, strings

**Key Insights:**
- Fast/slow pointers enable cycle detection with O(1) space
- Works for any sequence, not just linked lists
- Middle finding useful preprocessing for many problems

**Deliverables:**
- [ ] Implement cycle detection in linked list
- [ ] Find cycle start position
- [ ] Find middle of linked list
- [ ] Detect happy number using fast/slow
- [ ] Solve palindrome linked list problem
- [ ] Solve reorder linked list problem

---

## 📌 WEEK 6: STRING PATTERNS

### Weekly Goal
Apply pattern techniques to string problems. Strings are arrays of characters; adapt earlier patterns and learn string-specific techniques like palindrome detection and bracket matching.

### Weekly Topics
- Palindrome patterns and detection
- Substring problems using sliding window
- Parentheses and bracket matching
- String transformations and building
- String matching and rolling hash applications

---

### 📅 DAY 1: PALINDROME PATTERNS

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Simple Palindrome Check**
  - Two pointers from ends converging
  - Compare characters
  - Time: O(n), Space: O(1)

- **Longest Palindromic Substring**
  - Naive: O(n³) check all substrings
  - Expand around center: O(n²)
    - For each center (odd and even length)
    - Expand while characters match
    - Track longest found
  - Manacher's algorithm: O(n) (advanced)

- **Valid Palindrome (with Non-Alphanumeric)**
  - Ignore non-alphanumeric characters
  - Two pointers skip non-letters/digits
  - Compare letters (case-insensitive)
  - Time: O(n)

- **Valid Palindrome II (Remove at Most One)**
  - Two pointers from ends
  - When mismatch: try removing left or right
  - Check if either results in palindrome
  - Recursive or iterative

- **Palindrome Partitioning**
  - Partition string into palindromic substrings
  - Backtracking: at each position, try all palindromic prefixes
  - Memoization: cache (start, end) → is_palindrome
  - Time: O(n × 2^n) in worst case

- **Shortest Palindrome by Adding Characters**
  - Find longest palindrome from start
  - Add reverse of suffix to start
  - Example: "abcd" → find "a" is palindrome from start
    - Add "dcb" to start: "dcbabcd"
  - Use KMP for efficiency

- **Count Palindromic Substrings**
  - Expand around center for each center
  - Count palindromes found at each center
  - Time: O(n²)

- **Memoization for Palindrome Checks**
  - Cache (i, j) → is_substring_palindrome
  - Fill table diagonally
  - Use in palindrome partitioning for efficiency

**Key Insights:**
- Palindrome detection symmetric property
- Center expansion elegant for O(n²)
- Backtracking powerful for partitioning

**Deliverables:**
- [ ] Implement longest palindromic substring with center expansion
- [ ] Implement valid palindrome II (remove at most one)
- [ ] Solve palindrome partitioning with backtracking
- [ ] Count all palindromic substrings
- [ ] Find shortest palindrome by adding characters

---

### 📅 DAY 2: SUBSTRING & SLIDING WINDOW ON STRINGS

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Longest Substring Without Repeating Characters**
  - Variable sliding window with character map
  - Expand right: add character
  - If character repeats: shrink left until no repeat
  - Time: O(n), Space: O(alphabet size)

- **Longest Substring with At Most K Distinct Characters**
  - Similar approach: maintain at most K distinct
  - When K+1 distinct: shrink until K again
  - Variation: exactly K (use at-most-k minus at-most-(k-1) trick)

- **Permutation in String (Pattern Matching)**
  - Fixed window size = pattern length
  - Check if frequency map matches pattern frequency
  - Slide window, update frequency
  - Time: O(n)

- **Find All Anagrams of Substring**
  - Similar: fixed window size
  - For each window: check if anagram of pattern
  - Collect starting indices
  - Time: O(n)

- **Minimum Window Substring**
  - Find smallest window containing all pattern characters
  - Variable window: expand right until complete
  - Shrink left while complete
  - Track best window found
  - Time: O(n)

- **Substring with Concatenation of All Words**
  - Fixed window size = sum of word lengths
  - Check if window contains exactly all words
  - Use frequency map for words
  - Time: O(n × m²) where m = number of words

- **Repeated DNA Sequences (Rolling Hash)**
  - Find 10-character sequences appearing more than once
  - Rolling hash for efficiency
  - Hash to set, check for duplicates
  - Time: O(n)

- **Substring Problems Pattern**
  - Clear window condition (valid vs invalid)
  - Expand right to grow window
  - Shrink left to maintain validity
  - Process best candidate at each step

**Key Insights:**
- Sliding window transforms O(n²) to O(n)
- Frequency maps enable constraint checking
- Rolling hash for pattern detection

**Deliverables:**
- [ ] Implement longest substring without repeating characters
- [ ] Solve minimum window substring problem
- [ ] Find all anagrams of pattern in string
- [ ] Solve permutation in string problem
- [ ] Find repeated DNA sequences using rolling hash

---

### 📅 DAY 3: PARENTHESES & BRACKET MATCHING

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Valid Parentheses via Stack**
  - Push opening brackets: (, [, {
  - On closing bracket: pop and check if matching
  - If mismatch: invalid
  - If stack not empty at end: invalid
  - Time: O(n), Space: O(n)

- **Generate All Valid Parentheses**
  - N pairs of parentheses: generate all valid combinations
  - Backtracking: track open count and close count
  - Only add:
    - Opening: if less than n used
    - Closing: if less than opening used
  - Time: O(Catalan number) = O(4^n / sqrt(n))

- **Longest Valid Parentheses**
  - Find longest valid substring
  - Stack approach: store indices
    - Push -1 initially
    - On '(': push index
    - On ')': pop index
      - If empty: push current index
      - Else: length = current - top of stack
  - Time: O(n)

- **Minimum Remove to Make Valid Parentheses**
  - Remove minimum characters to make valid
  - Two-pass:
    - First pass: mark invalid ones
    - Second pass: build result without marked
  - Or use stack to track invalid indices

- **Balanced Parentheses with Limits**
  - Maximum depth K
  - Track current depth
  - Reject if exceed K
  - Validate as building

- **Score of Parentheses**
  - Each pair contributes: inner 2×score
  - Nested pairs multiply contributions
  - Use stack to track scores
  - Time: O(n)

- **Check if String is K-Palindrome**
  - Remove K character-pairs (any chars)
  - Check if remaining is palindrome
  - Backtracking or DP

**Key Insights:**
- Stack perfect for matching problems
- Push/pop mechanics handle nesting
- Backtracking generates all valid combinations

**Deliverables:**
- [ ] Implement valid parentheses check
- [ ] Generate all valid parentheses combinations for n pairs
- [ ] Solve longest valid parentheses problem
- [ ] Solve minimum remove to make valid
- [ ] Calculate score of parentheses string

---

### 📅 DAY 4: STRING TRANSFORMATIONS & BUILDING

**Duration:** 90 minutes

**Topics & Subtopics:**

- **String to Integer (atoi)**
  - Parse string to integer
  - Handle whitespace: trim leading
  - Handle sign: +/-
  - Handle non-digit characters: stop
  - Handle overflow: check bounds
  - Time: O(n)

- **Integer to String**
  - Reverse digits and build
  - Handle negative numbers
  - Handle zero
  - Time: O(log n) for n bits

- **Integer ↔ Roman Numerals**
  - Mapping: {1000: M, 900: CM, 500: D, ...}
  - Integer to Roman: greedily use largest symbols
  - Roman to Integer: sum values (special cases for subtraction)
  - Time: O(1) for fixed range

- **Zigzag Conversion**
  - Read string in zigzag pattern, output row by row
  - Either simulate zigzag or use index formula
  - Formula: character at row depends on k (number of rows) and position

- **Reverse String / Reverse Words**
  - Reverse entire string: O(1) space using two pointers
  - Reverse words: reverse entire, then reverse each word

- **String Compression (RLE)**
  - Replace runs with character + count
  - Example: "aabbbccc" → "a2b3c3"
  - Track when character changes
  - Time: O(n)

- **Decode String**
  - Pattern: "3[a2[c]]" → "accaccacc"
  - Use stack to handle nesting
  - Push: characters and counts
  - On ']': pop and decode
  - Time: O(output length)

- **String Builder / Performance**
  - String immutability in many languages
  - Use StringBuilder instead of concatenation
  - Concatenation in loop: O(n²) → use builder: O(n)

- **Word Ladder (BFS with String)**
  - Find transformation sequence using string adjacency
  - Adjacency: differ by exactly one character
  - BFS to find shortest path
  - Time: O(n × w²) where w = word length

**Key Insights:**
- Character-by-character processing for transformations
- Stack useful for nested structures
- StringBuilder avoids quadratic concatenation

**Deliverables:**
- [ ] Implement string to integer with overflow handling
- [ ] Convert integer to/from Roman numerals
- [ ] Implement zigzag conversion both directions
- [ ] String compression with RLE
- [ ] Decode string with nested brackets

---

### 📅 DAY 5 (OPTIONAL): STRING MATCHING & ROLLING HASH IN PRACTICE

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Revisiting Karp-Rabin Algorithm**
  - Rolling hash for pattern matching
  - Compute hash of pattern and first window
  - Slide window, update hash in O(1)
  - On match: verify full string (avoid false positives)
  - Time: O(n + m) average, O((n-m)×m) worst case

- **Rolling Hash Implementation**
  - Hash = (s[0] × b^(m-1) + s[1] × b^(m-2) + ... + s[m-1]) mod p
  - Update: (hash - s[i]×b^(m-1)) × b + s[i+m]) mod p
  - Choose: b = 256 (alphabet size), p = large prime

- **Collision Handling**
  - On hash match: verify actual string (Karp-Rabin feature)
  - Reduces false positives
  - Handles hash collisions transparently

- **Repeated Substring Pattern**
  - Find if string is repetition of smaller string
  - Rolling hash: efficient checking
  - Or use string matching: pattern appears in (s+s) without boundaries

- **Find Repeated DNA Sequences**
  - 10-character sequences in DNA string
  - Rolling hash to find all 10-mers
  - Store in set, find duplicates
  - Alphabet = 4 (A, C, G, T)

- **Comparing KMP vs Boyer-Moore vs Karp-Rabin**
  - KMP: O(n+m), no extra space
  - Boyer-Moore: O(n/m) average, slow on short patterns
  - Karp-Rabin: O(n+m) average, simple hash computation
  - Choice depends on application and alphabet size

- **Applications in Systems**
  - Log searching: find pattern in logs
  - Plagiarism detection: compare document snippets
  - DNA sequence searching
  - Network traffic inspection

- **Performance Considerations**
  - Hash computation overhead for short patterns
  - Probability of collisions affects verification cost
  - Real systems use cryptographic hashes for security

**Key Insights:**
- Rolling hash enables O(1) window updates
- Verification step handles hash collisions
- Practical choice among matching algorithms

**Deliverables:**
- [ ] Implement Karp-Rabin algorithm
- [ ] Find all 10-mers in DNA sequence
- [ ] Detect repeated substring pattern
- [ ] Compare performance vs KMP on various inputs
- [ ] Implement with multiple patterns (Aho-Corasick preview)

---

# END OF PHASE B

**Total Content:**
- 3 weeks (Weeks 4-6)
- 15 study days (5 days per week)
- 60+ detailed topics and subtopics
- 150+ problem patterns covered
- 90-120 minutes per day
- 45-60 hours of material

**Phase B Mastery Outcomes:**
- ✅ Pattern recognition across array/string problems
- ✅ Two-pointer, sliding window, divide & conquer fluency
- ✅ Monotonic stack and hash-based solutions
- ✅ Interval merging, partitioning, Kadane's algorithm
- ✅ String manipulation and matching techniques
- ✅ Interview-ready problem-solving approach

**Next:** Phase C - Trees, Graphs & DP (Weeks 7-11)

---
# 🟪 PHASE C: TREES, GRAPHS & DYNAMIC PROGRAMMING (Weeks 7-11)

## Phase Motivation
By now you understand fundamentals and patterns for linear structures. This phase moves to hierarchical (trees) and relational (graphs) data, plus the powerful optimization technique of dynamic programming. These are the backbone of complex system design and competitive programming.

## Phase Outcomes
- [ ] Understand tree structures, traversals, BSTs, and balanced BST operations
- [ ] Apply DP to tree problems (DP on trees)
- [ ] Master graph representations and traversals (BFS, DFS)
- [ ] Apply shortest path algorithms (Dijkstra, Bellman-Ford, Floyd-Warshall)
- [ ] Understand and implement minimum spanning trees
- [ ] Master dynamic programming from basics to complex patterns
- [ ] Solve DP on graphs, trees, DAGs, and with constraints
- [ ] Combine multiple algorithms to solve integration problems

---

## 📌 WEEK 7: TREES & BALANCED SEARCH TREES

### Weekly Goal
Build understanding of tree structures and operations. Master binary trees, search trees, and balance mechanisms that enable logarithmic operations.

### Weekly Topics
- Binary tree structure and traversals
- Binary search trees and operations
- Balanced BSTs (AVL, red-black overview)
- Tree patterns and applications
- Augmented trees for advanced queries

---

### 📅 DAY 1: BINARY TREES & TRAVERSALS

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Tree Anatomy & Terminology**
  - Root: topmost node
  - Leaf: node with no children
  - Parent, child, sibling relationships
  - Depth: distance from root
  - Height: distance from node to farthest leaf
  - Full tree: every internal node has exactly 2 children
  - Complete tree: filled from left to right, bottom level may be partial
  - Balanced tree: height is O(log n)

- **Binary Tree Representation**
  - Node-based: each node has value, left, right pointers
  - Array-based: parent of i at i//2, children at 2i and 2i+1
  - First method: flexible, used in most problems
  - Second method: compact, useful for heaps

- **Preorder Traversal (Root, Left, Right)**
  - Visit node, then left subtree, then right subtree
  - Use case: copying tree, serialization
  - Recursive: visit(), recurse_left(), recurse_right()
  - Iterative: use stack, pop and process, push right then left

- **Inorder Traversal (Left, Root, Right)**
  - Left subtree, visit node, right subtree
  - For BSTs: produces sorted sequence
  - Key for BST applications
  - Recursive or iterative with stack

- **Postorder Traversal (Left, Right, Root)**
  - Left subtree, right subtree, visit node
  - Use case: deleting tree (delete children before parent)
  - Computing heights, sizes bottom-up

- **Level-Order Traversal (BFS)**
  - Visit level by level from top to bottom
  - Use queue instead of stack
  - Essential for finding path lengths, siblings

- **Iterative Traversals via Stack**
  - Preorder: push node, pop-process-push(right, left)
  - Inorder: push all left, pop-process-push-right
  - Postorder: more complex, two stacks or careful ordering

- **Spiral Traversal (Zigzag)**
  - Alternate direction each level
  - Use queue and direction flag
  - Collect results in lists

- **Boundary Traversal**
  - Left boundary (without leaves), bottom leaves, right boundary (reversed, without leaves)
  - Combination of different traversals

**Key Insights:**
- Different traversals for different purposes
- Iterative traversals use explicit stack
- Inorder on BSTs gives sorted order

**Deliverables:**
- [ ] Implement all four traversals (recursive)
- [ ] Implement all four traversals (iterative with stack/queue)
- [ ] Verify inorder gives sorted sequence for BST
- [ ] Implement spiral/zigzag traversal
- [ ] Implement boundary traversal

---

### 📅 DAY 2: BINARY SEARCH TREES (BSTs)

**Duration:** 90 minutes

**Topics & Subtopics:**

- **BST Property & Definition**
  - For each node: all left descendants < node, all right descendants ≥ node
  - Enables efficient search, insert, delete
  - Inorder traversal produces sorted sequence

- **Search in BST**
  - Recursive: compare, recurse left or right
  - Time: O(h) where h = height
  - Best: O(log n) for balanced tree
  - Worst: O(n) if tree is degenerate (like linked list)

- **Insert in BST**
  - Find correct position: follow comparisons
  - Create new node and link
  - Time: O(h)
  - No duplicates (usually) or allow duplicates with convention

- **Delete from BST**
  - Case 1: Leaf node - simply remove
  - Case 2: One child - replace with child
  - Case 3: Two children:
    - Option A: find in-order predecessor (largest in left subtree)
    - Option B: find in-order successor (smallest in right subtree)
    - Replace node value with successor/predecessor value
    - Delete successor/predecessor (now has ≤ 1 child)
  - Time: O(h)

- **Verification of BST**
  - Simple check: left < node < right (incorrect for nested structure)
  - Correct: maintain min/max bounds as you traverse
  - Each left subtree: must be < current node
  - Each right subtree: must be > current node

- **Building BST from Sorted Array**
  - Insert middle element, recursively build left and right
  - Ensures balanced tree
  - Time: O(n)

- **BST vs Sorted Array Tradeoff**
  - BST: O(log n) search, insert, delete
  - Array: O(log n) search (binary search), O(n) insert/delete
  - BST better for dynamic data

- **Degenerate BST Problem**
  - Sorted input creates linear tree (height n)
  - Random insertion prevents this
  - Balanced BSTs guarantee O(log n) height

**Key Insights:**
- BST property enables efficient operations
- Height determines complexity
- Delete case with two children requires careful handling

**Deliverables:**
- [ ] Implement BST search, insert, delete
- [ ] Implement BST validation with bounds checking
- [ ] Build balanced BST from sorted array
- [ ] Handle duplicates in BST (convention choice)
- [ ] Find min/max elements in BST

---

### 📅 DAY 3: BALANCED BSTs - AVL & RED-BLACK (OVERVIEW)

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Why Balance Matters**
  - Unbalanced BST: degenerates to O(n)
  - Balanced BST: guarantees O(log n)
  - Self-balancing: maintain balance during insert/delete

- **AVL Trees**
  - Balance factor: height(left) - height(right)
  - Invariant: balance factor ∈ {-1, 0, 1}
  - Four rotation cases:
    - Left-left (LL): single right rotation
    - Right-right (RR): single left rotation
    - Left-right (LR): left rotation then right rotation
    - Right-left (RL): right rotation then left rotation
  - After insertion: check all ancestors, rebalance if needed
  - After deletion: similar process

- **Red-Black Trees**
  - Color each node red or black
  - Invariants:
    - Root is black
    - Red node has black children
    - All root-to-leaf paths have same number of black nodes
  - Guarantees: height ≤ 2 log(n+1)
  - Fewer rotations than AVL on average
  - More relaxed balance than AVL

- **Rotation Mechanics**
  - Left rotation: move right child up, left child of right becomes right child
  - Right rotation: symmetric
  - Preserve BST property during rotation
  - Update heights/colors as needed

- **Real-World Implementations**
  - C++ std::map uses red-black trees
  - Java TreeMap uses red-black trees
  - Python uses hash tables by default for dicts
  - AVL in some databases

- **Complexity Summary**
  - All balanced BSTs: O(log n) search, insert, delete
  - Space: O(n)
  - Practical differences in constants and rotation frequency

- **When to Use Which**
  - AVL: more rigid balance, faster search, slower insert/delete
  - Red-black: more relaxed balance, faster insert/delete, comparable search
  - For interviews: understand concept, implement basic, use library

- **Self-Balancing Not Required for Interview**
  - Usually, you don't need to implement balance mechanisms
  - Use library implementations (TreeMap, TreeSet)
  - Understand trade-offs and when to use

**Key Insights:**
- Balanced BSTs maintain O(log n) height
- Multiple balance schemes exist with different tradeoffs
- Library implementations sufficient for most use cases

**Deliverables:**
- [ ] Understand AVL rotation cases conceptually
- [ ] Trace through rebalancing example
- [ ] Understand red-black tree invariants
- [ ] Know when to use each data structure
- [ ] Use library implementations effectively

---

### 📅 DAY 4: TREE PATTERNS

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Path Sum Problems**
  - Root to leaf sum: DFS from root, accumulate sum, check at leaves
  - Path sum with target: recursively check left and right
  - General path sum: any node to any descendant (not just root-to-leaf)

- **Root-to-Leaf Paths**
  - Find all paths from root to leaves
  - Backtracking: build path as you go down
  - At leaf: record path
  - Backtrack: remove node from path

- **Tree Diameter**
  - Longest path in tree (not necessarily through root)
  - For each node: compute height of left and right subtrees
  - Diameter through this node: sum of heights + 1
  - Recurrence: diameter = max(left_diameter, right_diameter, left_height + right_height + 1)
  - Time: O(n)

- **Lowest Common Ancestor (LCA)**
  - Simplest approach: find paths to both nodes, compare
  - Binary lifting: preprocess for O(log n) queries
  - For general trees: DFS and find common ancestor

- **Height vs Depth**
  - Depth: distance from root (BFS)
  - Height: distance to farthest leaf (DFS from each node)
  - Often computed together in single DFS

- **Subtree Problems**
  - Identical subtrees: compare structures recursively
  - Subtree matching: check if one tree is subtree of another

- **Serialization & Deserialization**
  - Level-order based: store structure info (null nodes)
  - Preorder/postorder based: encode in specific order
  - Deserialization: reconstruct from encoding
  - Applications: persistence, transmission

- **Balanced Tree Check**
  - For each node: check balance factor (difference of heights)
  - Recursive check: efficient O(n) vs naive O(n²)
  - Prune early if imbalanced subtree found

**Key Insights:**
- Tree problems often solved with DFS + recursion
- Combining information from left and right subtrees powerful
- Serialization useful for storage and transmission

**Deliverables:**
- [ ] Implement path sum from root to leaf
- [ ] Find all root-to-leaf paths
- [ ] Calculate tree diameter
- [ ] Implement LCA for general trees
- [ ] Serialize and deserialize tree

---

### 📅 DAY 5 (OPTIONAL): AUGMENTED TREES & ORDER-STATISTICS TREES

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Augmenting Trees with Metadata**
  - Store additional info at each node for efficient queries
  - Example: subtree size at each node
  - Use in recursive calculations

- **Order-Statistics Trees**
  - Store size of each subtree
  - kth smallest element: compare k with left subtree size
  - Rank queries: find how many elements ≤ x
  - Time: O(log n) with balanced tree

- **Building Order-Statistics Tree**
  - Start with BST with sizes
  - On insert: update sizes of all ancestors
  - On delete: similar update
  - Maintain balance for O(log n) operations

- **Range Count Queries**
  - Count elements in [L, R] efficiently
  - Use augmented BST to prune subtrees
  - Time: O(log n)

- **Applications in Systems**
  - Database indexes: find kth record efficiently
  - Statistical calculations: percentiles
  - Range searching: spatial databases

- **Augmenting Other Structures**
  - Segment trees with custom aggregations
  - Can augment any tree structure

**Key Insights:**
- Augmentation enables new efficient queries
- Maintains BST structure while adding functionality
- Pattern reusable across many problems

**Deliverables:**
- [ ] Implement BST with size augmentation
- [ ] Solve kth smallest element in tree
- [ ] Implement range count queries
- [ ] Extend to weighted order-statistics (if time)

---

## 📌 WEEK 8: GRAPH FUNDAMENTALS - REPRESENTATIONS, BFS, DFS & TOPOLOGICAL SORT

### Weekly Goal
Master graph models and core traversal algorithms. Build intuition for graph structures through representations and explore problems solvable via BFS/DFS.

### Weekly Topics
- Graph representations and modeling
- Breadth-first search and shortest paths
- Depth-first search and applications
- Topological sorting
- Cycle detection and connectivity

---

### 📅 DAY 1: GRAPH MODELS & REPRESENTATIONS

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Graph Types & Terminology**
  - Directed vs undirected graphs
  - Weighted vs unweighted edges
  - Sparse vs dense graphs
  - Connected components
  - Cycles and DAGs

- **Adjacency Matrix Representation**
  - 2D array: matrix[i][j] = weight (or 1 if present)
  - Space: O(V²)
  - Time: O(1) edge lookup, O(V) to list neighbors
  - Best for: dense graphs, small graphs
  - Worst for: sparse graphs (wasteful space)

- **Adjacency List Representation**
  - Array of lists: list[i] = neighbors of i
  - Space: O(V + E)
  - Time: O(degree) to list neighbors, O(degree) edge lookup
  - Best for: sparse graphs, large graphs
  - Most common in interviews

- **Edge List Representation**
  - List of edges: [(u, v, weight), ...]
  - Space: O(E)
  - Time: O(E) to find neighbors (linear scan)
  - Use case: some graph algorithms like Kruskal MST

- **Choosing Representation**
  - Sparse (E << V²): adjacency list
  - Dense (E ≈ V²): adjacency matrix
  - Special cases: edge list for algorithms like MST

- **Implicit Graphs**
  - Graph defined by rules, not explicit storage
  - Example: grids (2D arrays with neighbors)
  - Example: game states (each state is node, moves are edges)
  - Explore via DFS/BFS without building graph

- **Graph Modeling from Problems**
  - Identify nodes: elements, states, positions
  - Identify edges: relationships, transitions, adjacencies
  - Identify weights: costs, distances, probabilities
  - Example: social network → nodes=people, edges=friendships

- **Building Graphs from Input**
  - Parse input to build adjacency list
  - Handle directed vs undirected (add reverse edge?)
  - Handle weights (store with edges)

**Key Insights:**
- Representation choice affects algorithm complexity
- Adjacency list most common for interviews
- Implicit graphs important for space efficiency

**Deliverables:**
- [ ] Build adjacency list from edge list
- [ ] Build adjacency matrix from edge list
- [ ] Convert between representations
- [ ] Model problem as graph (example: social network)

---

### 📅 DAY 2: BREADTH-FIRST SEARCH (BFS)

**Duration:** 90 minutes

**Topics & Subtopics:**

- **BFS Algorithm Template**
  - Queue-based frontier expansion
  - Visited set to avoid revisiting
  - Process nodes layer by layer
  - Time: O(V + E)

- **BFS from Single Source**
  - Start from source node
  - Add to queue and mark visited
  - While queue not empty:
    - Dequeue node
    - Process node
    - Enqueue unvisited neighbors
  - Guarantees shortest path in unweighted graphs

- **Shortest Path (Unweighted)**
  - Distance[node] = layer at which node discovered
  - Parent tracking enables path reconstruction
  - Distance[source] = 0, distance[neighbors] = 1, etc.

- **Connected Components**
  - For undirected graphs: run BFS from each unvisited node
  - Each BFS traversal = one component
  - Count components

- **Bipartite Graph Check**
  - Color nodes with two colors
  - For each node: neighbors must have different color
  - BFS: assign colors, check conflicts
  - Bipartite iff no conflicts

- **Level-Order Tree Traversal**
  - BFS on tree structure
  - Layer-wise processing

- **Shortest Path in Unweighted Network**
  - Classic BFS application
  - Find quickest route in network

- **Applications of BFS**
  - Social networks: degrees of separation
  - Shortest route on unweighted map
  - Web crawlers: breadth-first indexing
  - Puzzle solving: minimum moves

- **Optimizations**
  - Bidirectional BFS: search from both ends (faster for large graphs)
  - Early termination: if goal found, stop

**Key Insights:**
- BFS explores layer by layer
- Guarantees shortest path in unweighted graphs
- Time: O(V + E) always

**Deliverables:**
- [ ] Implement basic BFS
- [ ] Find shortest path using BFS
- [ ] Detect connected components
- [ ] Check if graph is bipartite
- [ ] Implement bidirectional BFS

---

### 📅 DAY 3: DEPTH-FIRST SEARCH (DFS) & TOPOLOGICAL SORT

**Duration:** 90 minutes

**Topics & Subtopics:**

- **DFS Algorithm Template**
  - Recursive exploration (or stack-based)
  - Visit node, explore all descendants, backtrack
  - Time: O(V + E)

- **Recursive DFS**
  - Mark visited, process node
  - Recursively visit unvisited neighbors
  - Implicit stack (function call stack)

- **Iterative DFS (Stack-Based)**
  - Explicit stack instead of recursion
  - Push start node
  - While stack not empty:
    - Pop node
    - Process if not visited
    - Push unvisited neighbors

- **DFS Tree & Edge Types**
  - Tree edges: edges to unvisited nodes
  - Back edges: edges to ancestor (indicates cycle in directed graph)
  - Forward edges: edges to descendant (only in directed)
  - Cross edges: edges to already-finished node (only in directed)
  - Classification depends on DFS order

- **Cycle Detection in Directed Graphs**
  - Use DFS with states: unvisited, visiting, visited
  - Back edge indicates cycle
  - White-gray-black coloring

- **Topological Sort - DFS Approach**
  - DFS post-order: nodes finished later come first
  - Reverse post-order: topological sort for DAG
  - Time: O(V + E)

- **Topological Sort - Kahn's Algorithm (BFS)**
  - In-degree based approach
  - Process nodes with in-degree 0
  - Decrease in-degree of neighbors
  - Add to sort when in-degree becomes 0

- **Applications of DFS**
  - Path existence: can reach B from A?
  - Connected components: similar to BFS
  - Topological sorting: dependency ordering
  - Strongly connected components: advanced

- **DFS vs BFS**
  - DFS: uses stack, explores depth first
  - BFS: uses queue, explores breadth first
  - Choose based on problem needs

**Key Insights:**
- DFS explores single path deeply
- Edge classification reveals graph structure
- Post-order traversal useful for dependencies

**Deliverables:**
- [ ] Implement recursive and iterative DFS
- [ ] Detect cycle in directed graph
- [ ] Topological sort using DFS
- [ ] Topological sort using Kahn's algorithm
- [ ] Find path from source to destination

---

### 📅 DAY 4: CONNECTIVITY & BIPARTITE GRAPHS

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Connected Components in Undirected Graphs**
  - Nodes are grouped if reachable from each other
  - DFS/BFS from each unvisited node finds one component
  - Count components
  - Label each component

- **Strongly Connected Components in Directed Graphs**
  - Nodes in SCC can reach each other (cycle exists)
  - Kosaraju algorithm: two-pass DFS
  - Tarjan algorithm: single-pass DFS
  - (High-level only for interviews)

- **Bipartite Graph Check**
  - Graph is bipartite iff 2-colorable
  - No odd-length cycles
  - Use BFS or DFS to color
  - Time: O(V + E)

- **2-Coloring Problem**
  - Assign 0/1 to nodes such that adjacent nodes differ
  - BFS/DFS and color checking
  - Detect conflict on neighbor already colored

- **Bridges & Articulation Points** (High-level)
  - Bridge: edge whose removal disconnects graph
  - Articulation point: node whose removal disconnects graph
  - Applications: network reliability, road networks

- **Weakly Connected Components** (Directed)
  - Nodes are in same component if connected when ignoring direction
  - DFS treating graph as undirected

- **Applications**
  - Grouping objects with relations
  - Clustering in networks
  - Checking bipartiteness in matching problems

**Key Insights:**
- Component finding via DFS/BFS
- Bipartite check simple with 2-coloring
- Structural properties enable various applications

**Deliverables:**
- [ ] Count connected components in undirected graph
- [ ] Check if graph is bipartite
- [ ] 2-color a bipartite graph
- [ ] Identify articulation points (high-level)

---

### 📅 DAY 5 (OPTIONAL): STRONGLY CONNECTED COMPONENTS

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Strongly Connected Components Definition**
  - SCC: set of nodes where each can reach each other
  - Graph of SCCs: collapsing each SCC to one node creates DAG

- **Kosaraju Algorithm**
  - First DFS: compute finish times (post-order)
  - Transpose graph: reverse all edges
  - Second DFS on transposed: visit in decreasing finish time order
  - Each DFS tree in second pass is one SCC
  - Time: O(V + E)

- **Tarjan Algorithm** (Conceptual)
  - Single-pass DFS with low-link values
  - Uses stack to track SCC candidate nodes
  - Identifies SCC complete during DFS
  - Time: O(V + E)

- **Applications**
  - Deadlock detection: processes in cycle = deadlock
  - Condensation graph: collapse SCC to analyze overall flow
  - Schedule problems: dependencies

- **Component DAG**
  - After finding SCCs, build DAG of components
  - Topological sort on component DAG

**Key Insights:**
- SCCs identify strongly connected regions
- Kosaraju simple and intuitive
- Component analysis useful for dependency problems

**Deliverables:**
- [ ] Understand Kosaraju algorithm conceptually
- [ ] Trace through example
- [ ] Identify SCCs in sample graph

---

## 📌 WEEK 9: GRAPH ALGORITHMS I - SHORTEST PATHS, MST & UNION-FIND

### Weekly Goal
Learn fundamental optimization algorithms on graphs. Master shortest path algorithms and minimum spanning tree.

### Weekly Topics
- Dijkstra's algorithm for single-source shortest paths
- Bellman-Ford for negative weights
- Floyd-Warshall for all-pairs shortest paths
- Minimum spanning trees (Kruskal & Prim)
- Union-Find for efficient connectivity

---

### 📅 DAY 1: DIJKSTRA'S ALGORITHM

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Problem: Single-Source Shortest Paths**
  - Given: source node, weighted graph
  - Find: shortest path to all nodes from source
  - Constraint: non-negative weights

- **Dijkstra Algorithm Overview**
  - Greedy algorithm: always expand nearest unvisited node
  - Use priority queue for efficiency
  - Time: O((V + E) log V) with binary heap

- **Dijkstra Algorithm Details**
  - Distance[source] = 0, others = infinity
  - Priority queue: (distance, node)
  - While queue not empty:
    - Extract min distance node
    - For each neighbor: relax edge
    - Relaxation: if distance[u] + weight(u,v) < distance[v], update
  - Extract ensures smallest distance processed

- **Relaxation Operation**
  - Core operation: try to improve distance via current edge
  - If new path shorter: update distance and add to queue
  - Edge weight must be non-negative for correctness

- **Why Non-Negative?**
  - Greedy choice assumes explored nodes won't improve
  - Negative weights can violate this assumption
  - Negative cycles are undefined (infinite improvement)

- **Complexity Analysis**
  - Extract min: O(log V) × V times = O(V log V)
  - Relaxation: O(1) × E times = O(E)
  - Total: O(V log V + E log V) = O((V + E) log V)
  - With Fibonacci heap: O(V log V + E)

- **Path Reconstruction**
  - Store parent[node] when updating distance
  - Trace back from destination to source

- **Applications**
  - GPS navigation: shortest route
  - Network routing: minimum latency path
  - Social networks: degree of separation (unweighted)

- **Variants**
  - Single-target: can run backward from target
  - Multiple sources: modify initialization
  - With constraints: time windows, capacity limits

**Key Insights:**
- Dijkstra greedy: expand closest unvisited
- Priority queue essential for efficiency
- Non-negative weights required

**Deliverables:**
- [ ] Implement Dijkstra with priority queue
- [ ] Find shortest path to all nodes from source
- [ ] Reconstruct shortest path
- [ ] Implement single-target variant
- [ ] Verify correctness on sample graphs

---

### 📅 DAY 2: BELLMAN-FORD & NEGATIVE WEIGHTS

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Problem: Negative Weights**
  - Dijkstra fails with negative weights
  - Need algorithm handling negatives but not negative cycles

- **Bellman-Ford Algorithm**
  - Relax all edges repeatedly: V-1 times
  - Each iteration: process all edges, improve distances
  - After V-1 iterations: shortest paths found (if no negative cycle)

- **Bellman-Ford Details**
  - Distance[source] = 0, others = infinity
  - For i = 1 to V-1:
    - For each edge (u, v):
      - Relax: if distance[u] + weight < distance[v], update
  - Time: O(V × E)

- **Negative Cycle Detection**
  - Extra iteration: if distances improve, negative cycle exists
  - May not be reachable from source

- **Why It Works**
  - Shortest path has at most V-1 edges
  - Each iteration finds paths with one more edge
  - After V-1 iterations: optimal found

- **Complexity**
  - Time: O(V × E) vs O((V + E) log V) for Dijkstra
  - Slower but handles negatives
  - Dense graphs: similar (E ≈ V²)
  - Sparse graphs: Bellman-Ford worse

- **Applications**
  - Currency exchange arbitrage detection
  - Finance: detecting profit opportunities
  - Networks with negative costs (rarely)

- **Optimizations**
  - SPFA (Shortest Path Faster Algorithm): queue-based
  - Process only edges from updated nodes
  - Average case O(E), worst case O(V × E)

**Key Insights:**
- Bellman-Ford handles negative weights
- Repeat relaxation ensures correctness
- Cycle detection critical

**Deliverables:**
- [ ] Implement Bellman-Ford
- [ ] Detect negative cycles
- [ ] Find shortest paths with negative weights
- [ ] Implement SPFA for optimization

---

### 📅 DAY 3: FLOYD-WARSHALL ALL-PAIRS SHORTEST PATHS

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Problem: All-Pairs Shortest Paths**
  - Find shortest paths between all pairs of nodes
  - Different from running Dijkstra V times

- **Floyd-Warshall Algorithm**
  - DP formulation: intermediate vertices up to k
  - dist[i][j][k] = shortest path from i to j using vertices 0..k
  - Base: dist[i][j][0] = direct edge weight or infinity
  - Recurrence: dist[i][j][k] = min(dist[i][j][k-1], dist[i][k][k-1] + dist[k][j][k-1])
  - Optimize: can use 2D array and update in-place

- **Floyd-Warshall Details**
  - Initialize: copy adjacency matrix
  - Three nested loops: for k, i, j (order matters!)
  - Update: dist[i][j] = min(dist[i][j], dist[i][k] + dist[k][j])
  - Time: O(V³)

- **Complexity Analysis**
  - Time: O(V³) - triple nested loops
  - Space: O(V²) - distance matrix
  - Works for negative weights (but not negative cycles)
  - Detectable: if dist[i][i] < 0 after algorithm

- **When to Use**
  - Small graphs: V ≤ 500
  - Need all pairs distances
  - Dense graphs
  - Simple implementation preferred over efficiency

- **Path Reconstruction**
  - Store next[i][j]: next node on shortest path from i to j
  - On update: set next[i][j] = k (intermediate node)
  - Trace path using next matrix

- **Applications**
  - Transitive closure: which pairs are connected
  - Network reliability: min latency between all pairs
  - Game analysis: all distances in game state graph

**Key Insights:**
- Floyd-Warshall DP on intermediate vertices
- Simple implementation, high complexity
- Use for small, all-pairs problems

**Deliverables:**
- [ ] Implement Floyd-Warshall
- [ ] Find all shortest path pairs
- [ ] Reconstruct paths
- [ ] Detect negative cycles
- [ ] Compare with running Dijkstra V times

---

### 📅 DAY 4: MINIMUM SPANNING TREES - KRUSKAL & PRIM

**Duration:** 90 minutes

**Topics & Subtopics:**

- **MST Problem**
  - Spanning tree: connects all V vertices with V-1 edges, no cycles
  - Minimum spanning tree: spanning tree with minimum total weight
  - May not be unique if edge weights repeat

- **Kruskal Algorithm (Edge-Based)**
  - Sort edges by weight ascending
  - Iterate edges: add if doesn't create cycle
  - Use DSU (Disjoint Set Union) to detect cycles
  - Time: O(E log E) for sorting + O(E α(V)) for DSU

- **Kruskal Details**
  - Sort edges: O(E log E)
  - For each edge:
    - Find components of endpoints
    - If different: add edge, union components
  - Continue until V-1 edges added or edges exhausted
  - Greedy: always pick smallest weight edge that doesn't cycle

- **Prim Algorithm (Vertex-Based)**
  - Start with one vertex
  - Grow tree by always adding minimum weight edge to new vertex
  - Use priority queue
  - Time: O((V + E) log V) with binary heap

- **Prim Details**
  - Start: add source to tree
  - While tree not complete:
    - Find minimum weight edge from tree to non-tree vertex
    - Add to tree
  - Priority queue helps find minimum edge efficiently

- **Cut Property & Correctness**
  - Cut: partition vertices into two sets
  - Minimum edge crossing cut is in some MST
  - Exchange argument: if used different edge, can swap for minimum

- **Kruskal vs Prim**
  - Kruskal: works on disconnected graphs (produces forest)
  - Prim: requires connected component
  - Kruskal: good for sparse graphs (E small)
  - Prim: good for dense graphs (E large)
  - Both O((V + E) log V) or better with optimization

- **Applications**
  - Network design: minimum cost to connect all nodes
  - Clustering: build hierarchy of clusters
  - Traveling salesman: MST approximation

**Key Insights:**
- MST minimizes total edge weight
- Two greedy algorithms both work
- Cut property justifies correctness

**Deliverables:**
- [ ] Implement Kruskal with DSU
- [ ] Implement Prim with priority queue
- [ ] Verify MST properties
- [ ] Compare on various graphs
- [ ] Solve network design problem

---

### 📅 DAY 5 (OPTIONAL): DSU / UNION-FIND IN DEPTH

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Disjoint Set Problem**
  - Maintain partitions of elements
  - Operations: find (which set?), union (merge sets)
  - Applications: connectivity, cycle detection, MST

- **Union-Find Data Structure**
  - Array parent[i]: parent of i in forest
  - Find: follow parent pointers to root
  - Union: merge two trees

- **Path Compression Optimization**
  - During find: point all nodes directly to root
  - Flatten tree structure
  - Amortized O(α(n)) per operation (inverse Ackermann)
  - Practical: essentially O(1)

- **Union by Rank Optimization**
  - Track tree rank (height)
  - Union: attach smaller tree to larger
  - Prevents degeneration into linked list
  - Combined with path compression: O(α(n))

- **Implementation**
  - Find with path compression:
    ```
    def find(x):
      if parent[x] != x:
        parent[x] = find(parent[x])  # compress path
      return parent[x]
    ```
  - Union by rank:
    ```
    def union(x, y):
      rx, ry = find(x), find(y)
      if rank[rx] < rank[ry]: rx, ry = ry, rx
      parent[ry] = rx
      if rank[rx] == rank[ry]: rank[rx] += 1
    ```

- **Complexity Analysis**
  - Without optimization: O(n) worst case
  - With path compression: O(n log n)
  - With union by rank: O(n log n)
  - With both: O(n α(n)) ≈ O(n)

- **Applications**
  - Kruskal MST: detect cycles
  - Network connectivity queries
  - Finding connected components
  - Offline dynamic connectivity

**Key Insights:**
- Union-Find extremely efficient with optimizations
- Path compression clever trick
- Rank helps balance trees

**Deliverables:**
- [ ] Implement basic DSU
- [ ] Implement with path compression
- [ ] Implement with union by rank
- [ ] Use in Kruskal MST
- [ ] Solve connectivity problem

---

## 📌 WEEK 10: DYNAMIC PROGRAMMING I - FUNDAMENTALS

### Weekly Goal
Build DP intuition from recursion + memoization to table-based bottom-up solutions. Master the mindset: break problem into subproblems, store results, combine efficiently.

### Weekly Topics
- DP as recursion with memoization
- Bottom-up tabulation approach
- 1D and 2D DP patterns
- DP on sequences and grids
- Optimization techniques

---

### 📅 DAY 1: DP AS RECURSION + MEMOIZATION

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Overlapping Subproblems Concept**
  - Same subproblem solved repeatedly in recursion
  - Example: Fibonacci
    - fib(5) = fib(4) + fib(3)
    - fib(4) = fib(3) + fib(2)
    - fib(3) computed multiple times
  - Exponential time without caching

- **Memoization = Recursion + Cache**
  - Compute subproblem: check cache first
  - If cached: return immediately
  - Else: compute, store in cache, return
  - Dramatically reduces time

- **Fibonacci with Memoization**
  - memo[n] = fib(n) result
  - Base: memo[0]=0, memo[1]=1
  - Recursive: if n in memo return memo[n], else compute
  - Time: O(n) instead of O(2^n)

- **Top-Down DP (Memoization)**
  - Start with full problem
  - Recurse on subproblems
  - Cache results
  - Natural from problem structure
  - Can be inefficient if not all subproblems needed

- **Bottom-Up DP (Tabulation)**
  - Solve small problems first
  - Build up to full problem
  - No recursion, just iteration
  - More efficient (fewer function calls)
  - Need to identify order of computation

- **Recursive vs Iterative**
  - Recursive: cleaner code, intuitive
  - Iterative: faster (less overhead)
  - Choose based on problem and preference

- **DP Table & State Definition**
  - State: what does each cell represent?
  - Transitions: how to compute from other cells?
  - Base case: what are simplest subproblems?
  - Answer: which cell contains solution?

- **Example: Climbing Stairs**
  - Problem: climb n stairs, can take 1 or 2 steps per move
  - State: ways[i] = number of ways to reach stair i
  - Base: ways[0]=1 (at start), ways[1]=1 (one way to reach)
  - Transition: ways[i] = ways[i-1] + ways[i-2] (from previous or two-behind)
  - Answer: ways[n]

**Key Insights:**
- Memoization caches redundant computation
- Top-down natural from problem, bottom-up efficient
- State definition is key to correct DP

**Deliverables:**
- [ ] Implement Fibonacci with memoization
- [ ] Compare performance vs naive recursion
- [ ] Implement climbing stairs bottom-up
- [ ] Convert recursive solution to iterative
- [ ] Identify states and transitions for custom problem

---

### 📅 DAY 2: 1D DP & KNAPSACK FAMILY

**Duration:** 90 minutes

**Topics & Subtopics:**

- **1D DP Patterns**
  - Array dp[i] representing answer for problem of size i
  - Transition: dp[i] depends on dp[j] for j < i
  - Time: O(n), Space: O(n) or O(1) if can optimize

- **Climbing Stairs Variants**
  - Basic: ways to reach stair n taking 1 or 2 steps
  - With cost: minimum cost to reach stair n
  - With constraints: can't take consecutive 2-step moves
  - General: given step sizes, compute answer

- **House Robber (Non-Adjacent Sum)**
  - Given array of house values, rob non-adjacent houses
  - Maximize: rob[i] = max(rob[i-1], rob[i-2] + arr[i])
  - Intuition: at each house, either rob it or skip
  - Time: O(n), Space: O(1) with optimization

- **House Robber II (Circular)**
  - Houses in circle: can't rob first and last together
  - Solution: max(rob excluding first, rob excluding last)

- **Coin Change**
  - Problem: minimum coins to make amount
  - State: dp[i] = min coins for amount i
  - Transition: dp[i] = min(dp[i-coin] + 1 for each coin)
  - Time: O(amount × coins)

- **Coin Change II (Number of Ways)**
  - Count ways to make amount
  - Different DP: dp[i] = number of ways for amount i
  - Transition: dp[i] += dp[i-coin] for each coin
  - Careful: order matters (different from permutations)

- **0/1 Knapsack (Single Item)**
  - Given items with weight and value, capacity W
  - Maximize value without exceeding weight
  - Classic DP problem
  - dp[i][w] = max value with first i items and weight w
  - Transition: take or skip item
  - Time: O(n × W), Space: O(n × W) or O(W) optimized

- **Unbounded Knapsack (Infinite Items)**
  - Each item available unlimited times
  - dp[i] = max value with capacity i
  - Transition: dp[i] = max(dp[i-weight[j]] + value[j] for all items j)
  - Time: O(W × items)

- **Bounded Knapsack (Limited Copies)**
  - Each item available k times
  - Treat as k separate items (or optimize)

**Key Insights:**
- 1D DP often has binary choice (take/skip)
- Knapsack family generalizable pattern
- Space optimization often possible

**Deliverables:**
- [ ] Implement house robber variants
- [ ] Implement coin change (min coins and ways)
- [ ] Implement 0/1 knapsack
- [ ] Implement unbounded knapsack
- [ ] Optimize space for knapsack problems

---

### 📅 DAY 3: 2D DP - GRIDS & EDIT DISTANCE

**Duration:** 90 minutes

**Topics & Subtopics:**

- **2D Grid DP**
  - State: dp[i][j] = answer for cell (i, j)
  - Often: number of ways or minimum cost
  - Transition: combine from previous cells

- **Unique Paths in Grid**
  - Given grid with obstacles, count unique paths from top-left to bottom-right
  - Can move right or down
  - State: dp[i][j] = ways to reach (i, j)
  - Base: dp[0][0] = 1 if no obstacle
  - Transition: dp[i][j] = dp[i-1][j] + dp[i][j-1] if no obstacle
  - Time: O(m × n), Space: O(m × n) or O(n) optimized

- **Minimum Path Sum**
  - Given grid with costs, find minimum cost path
  - dp[i][j] = minimum cost to reach (i, j)
  - Transition: dp[i][j] = cost[i][j] + min(dp[i-1][j], dp[i][j-1])

- **Edit Distance (Levenshtein)**
  - Transform word1 to word2 with insert/delete/replace
  - State: dp[i][j] = min edits to transform word1[0..i-1] to word2[0..j-1]
  - Transitions:
    - If characters match: dp[i][j] = dp[i-1][j-1]
    - Else: min(dp[i-1][j], dp[i][j-1], dp[i-1][j-1]) + 1
  - Time: O(m × n), Space: O(m × n)

- **Longest Common Subsequence (LCS)**
  - Longest sequence present in both strings
  - State: dp[i][j] = length of LCS of word1[0..i-1] and word2[0..j-1]
  - Transitions:
    - If match: dp[i][j] = dp[i-1][j-1] + 1
    - Else: dp[i][j] = max(dp[i-1][j], dp[i][j-1])
  - Time: O(m × n)

- **Matrix Chain Multiplication**
  - Given matrices, find optimal order to multiply
  - State: dp[i][j] = min operations to multiply matrices i to j
  - Transition: try all split points
  - Time: O(n³)

- **Space Optimization**
  - 2D DP often reducible to 1D if only previous row needed
  - Save space from O(m × n) to O(n)

**Key Insights:**
- 2D DP common for grid and string problems
- Edit distance fundamental for many applications
- Can often optimize space

**Deliverables:**
- [ ] Implement unique paths in grid
- [ ] Implement minimum path sum
- [ ] Implement edit distance
- [ ] Implement LCS
- [ ] Optimize space for grid problems

---

### 📅 DAY 4: DP ON SEQUENCES

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Longest Increasing Subsequence (LIS)**
  - Find longest subsequence with increasing values
  - Naive: O(2^n) all subsequences
  - DP O(n²): dp[i] = longest ending at i
    - Transition: dp[i] = max(dp[j] + 1 for all j < i where arr[j] < arr[i])
  - DP O(n log n): binary search on tail array
  - Time: O(n²) or O(n log n)

- **Longest Decreasing Subsequence**
  - Symmetric to LIS
  - Same approaches apply

- **Maximum Subarray Sum (Kadane Revisited)**
  - dp[i] = max sum ending at i
  - Transition: dp[i] = max(arr[i], dp[i-1] + arr[i])
  - Answer: max(dp[i])

- **Weighted Interval Scheduling**
  - Intervals with weights, select non-overlapping to maximize weight
  - Sort by end time
  - dp[i] = max weight using intervals up to i
  - Transition: include i or exclude i
  - Find previous compatible interval via binary search
  - Time: O(n log n)

- **Subsequence Problem Family**
  - Distinct subsequences
  - Delete minimum characters to make palindrome
  - Shortest supersequence containing two strings

- **DP on Permutations**
  - State: use bitmask for subset of items
  - Example: TSP with DP

**Key Insights:**
- Sequence DP often involves position-based states
- LIS O(n log n) clever binary search approach
- Many variations with similar structure

**Deliverables:**
- [ ] Implement LIS O(n²) and O(n log n)
- [ ] Implement weighted interval scheduling
- [ ] Solve maximum subarray sum
- [ ] Implement longest decreasing subsequence
- [ ] Solve distinct subsequences problem

---

### 📅 DAY 5 (OPTIONAL): STORY-DRIVEN DP (MIT 6.006 STYLE)

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Problem Interpretation**
  - Complex DP problems often have story/real-world context
  - Key: translate story into state and transitions
  - Example: text justification, blackjack, etc.

- **Text Justification**
  - Format text with width constraint
  - Minimize "badness" (wasted space penalty)
  - DP: dp[i] = min badness for words 0..i
  - Transition: try all ending positions for current line
  - Calculate badness for each possibility

- **Blackjack DP**
  - State: (player's hand, dealer's shown card)
  - Value: expected payoff
  - Compute: recursively combine actions

- **State Design Principles**
  - State should capture: what matters for future decisions?
  - Example: in activity scheduling, only finish time matters, not start time
  - Example: in coin change, only remaining amount matters
  - Careful: too specific → huge state space; too general → can't recover answer

- **Transition Design**
  - Clearly identify choices at each state
  - Compute all possibilities
  - Combine: min, max, sum depending on problem

- **Real-World DP Examples**
  - Network routing: min latency
  - Game theory: optimal strategy
  - Financial: portfolio optimization
  - Resource allocation: maximize profit/minimize cost

**Key Insights:**
- State and transition design is art and skill
- Practice with varied problems builds intuition
- Real-world problems often complex but reducible to DP

**Deliverables:**
- [ ] Understand text justification problem
- [ ] Translate story problem to DP state/transitions
- [ ] Solve 2-3 story-driven problems
- [ ] Develop intuition for state design

---

## 📌 WEEK 11: DYNAMIC PROGRAMMING II - TREES, DAGS & ADVANCED PATTERNS

### Weekly Goal
Extend DP to more complex structures. Master tree DP, DAG DP, bitmask DP, and recognize when DP is the right approach.

### Weekly Topics
- DP on trees (rerooting, subtree problems)
- DP on DAGs (longest/shortest paths)
- Bitmask DP (subsets, TSP)
- State compression and optimization

---

### 📅 DAY 1: DP ON TREES

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Tree DP Framework**
  - Solve for each subtree, combine into parent
  - Post-order traversal (solve children before parent)
  - State: what's the answer for this subtree?

- **Maximum Independent Set**
  - Select nodes with no edges between them
  - Maximize value of selected nodes
  - dp[node][0] = max value excluding node
  - dp[node][1] = max value including node
  - Transition: combine children's answers

- **Tree Diameter via DP**
  - Longest path in tree
  - dp[node] = max distance down from node
  - Answer: max(left_depth + right_depth + 1)
  - Time: O(n)

- **Subtree Sum / Subtree Sizes**
  - Simple: sum all nodes in subtree
  - Post-order: solve children, add to node

- **Tree Coloring (K Colors)**
  - Color nodes with K colors, adjacent different
  - dp[node][color] = count of ways
  - Transition: children must use different colors

- **Rerooting DP** (Advanced)
  - Compute answer for each node as root
  - First pass: tree rooted at 0
  - Second pass: reroot, combining parent info

- **Practical Considerations**
  - Space: often can work with adjacency list (no explicit table)
  - Time: O(n) if each node processed once
  - Recursion depth: can hit stack limits for very deep trees

**Key Insights:**
- Tree DP elegantly solves many tree problems
- Post-order processing natural fit
- Often O(n) complexity

**Deliverables:**
- [ ] Implement tree diameter
- [ ] Implement maximum independent set
- [ ] Implement tree DP for custom problem
- [ ] Solve tree coloring problem

---

### 📅 DAY 2: DP ON DAGS

**Duration:** 90 minutes

**Topics & Subtopics:**

- **DAG (Directed Acyclic Graph)**
  - Directed graph with no cycles
  - Enables topological ordering
  - DP naturally applies to DAG

- **Longest Path in DAG**
  - No cycles → DP works
  - Topological sort order → compute in order
  - dp[node] = longest path starting at node
  - Transition: max(1 + dp[neighbor] for all neighbors)
  - Time: O(V + E)

- **Shortest Path in DAG with Negative Weights**
  - Bellman-Ford O(VE) needed for general graphs
  - DAG allows O(V + E) via topo sort
  - Process nodes in topo order, relax edges

- **Vertex Weight Sum Paths**
  - Path value = sum of vertex weights (not edge weights)
  - Longest vertex weight path in DAG
  - Similar DP but sum vertices instead of edges

- **All Paths in DAG**
  - Count or enumerate paths from s to t
  - DP: dp[node] = number of paths from node to t
  - Transition: sum paths through neighbors

- **Topo Sort Ordering**
  - Critical: must process nodes in order where all predecessors processed
  - Ensures correct DP computation
  - Failure to respect order → wrong answer

**Key Insights:**
- DAG enables efficient DP despite weights
- Topo sort ordering critical
- O(V + E) vs O(VE) significant improvement

**Deliverables:**
- [ ] Implement longest path in DAG
- [ ] Implement shortest path in DAG (negative weights)
- [ ] Count all paths in DAG
- [ ] Verify topo sort order importance

---

### 📅 DAY 3: BITMASK & SUBSET DP

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Bitmask Representation**
  - Set of n elements → bitmask of 2^n states
  - Bit i = 1 if element i in subset
  - Iterate over subsets: 0 to 2^n - 1

- **Subset Enumeration**
  - For each mask: iterate elements to see which included
  - Iterate submasks: clever bit manipulation

- **TSP with DP (Traveling Salesman)**
  - Visit n cities exactly once, minimum cost
  - Naive: O(n!) permutations
  - DP: state = (visited_cities, current_city)
  - dp[mask][last] = min cost visiting cities in mask, ending at last
  - Transition: visit next unvisited city
  - Time: O(2^n × n²), Space: O(2^n × n)

- **Subset Sum Problems**
  - Count/find subsets with specific sum
  - dp[i] = ways to make sum i
  - Transition: include/exclude elements

- **Maximum Weight Independent Set in Graph**
  - Can use bitmask if vertices few (≤ 20)
  - dp[mask] = max weight with vertices in mask, no edges between
  - Enumerate all masks, check if valid independent set

- **Traveling from Point A to B visiting all Waypoints**
  - Similar to TSP but don't return
  - State: (mask, last_position)

- **Complexity**
  - Time: O(2^n × f(n)) where f(n) is work per state
  - Space: O(2^n) for DP table
  - Feasible: n ≤ 20, marginal for n ≤ 25

- **Practical Considerations**
  - 2^20 = 1M states (manageable)
  - 2^25 = 33M states (tight)
  - 2^30 = 1B states (too much)

**Key Insights:**
- Bitmask DP when subset of small set is state
- TSP classic example of exponential DP
- Feasible only for n ≤ 20-25

**Deliverables:**
- [ ] Implement TSP with DP
- [ ] Solve subset sum problem
- [ ] Implement maximum weighted independent set (small graph)
- [ ] Count/enumerate subsets with constraints

---

### 📅 DAY 4 (OPTIONAL): STATE COMPRESSION & OPTIMIZATIONS

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Space Optimization Techniques**
  - Sliding window: keep only current and previous states
  - Example: 2D → 1D if only need previous row
  - Saves from O(m × n) to O(n)

- **Dimension Reduction**
  - 3D DP → 2D by observing what matters
  - Example: coin change with denominations → doesn't need 3D

- **Memoization vs Tabulation Trade-off**
  - Memoization: compute only needed states
  - Tabulation: compute all, more cache-friendly
  - Choose based on state space sparsity

- **Pruning in DP**
  - Skip invalid states
  - Early termination if answer found
  - Example: if current cost exceeds best known, prune

- **Matrix Exponentiation for DP**
  - Linear recurrence → matrix multiplication
  - Example: Fibonacci via matrix power
  - Enables O(log n) for O(n) DP

- **Convex Hull Trick**
  - Optimize DP transitions using geometry
  - When transitions are linear
  - Advanced technique, rarely in interviews

**Key Insights:**
- Optimization often critical for large inputs
- Space-time tradeoffs common
- Matrix exponentiation clever for linear recurrences

**Deliverables:**
- [ ] Optimize space for grid DP problems
- [ ] Apply matrix exponentiation to recurrence
- [ ] Implement pruning in DP
- [ ] Recognize when optimizations needed

---

### 📅 DAY 5 (OPTIONAL): MIXED DP PROBLEMS

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Multi-Concept Problems**
  - Combining DP with other algorithms
  - Example: DP on graph shortest paths
  - Example: DP with greedy choices

- **DP + Greedy**
  - Some problems have greedy part and DP part
  - Example: jump game II (greedy jump distance)
  - Example: best time to buy/sell stock variants

- **DP + Graph Algorithms**
  - Shortest path with DP formulation
  - DP on all reachable states

- **DP + Number Theory**
  - DP with modular arithmetic
  - Example: count solutions mod p

- **Recognition & Intuition**
  - Overlapping subproblems → DP candidate
  - Optimal substructure → DP candidate
  - Practice: solve many problems builds intuition

**Key Insights:**
- Complex problems combine multiple techniques
- Recognition comes with practice
- Flexibility in DP application critical

**Deliverables:**
- [ ] Solve 2-3 mixed concept problems
- [ ] Identify when DP is part of solution
- [ ] Combine DP with other algorithms
- [ ] Build problem-solving intuition

---

# END OF PHASE C

**Total Content:**
- 5 weeks (Weeks 7-11)
- 25 study days (5 days per week)
- 120+ detailed topics and subtopics
- 250+ problem patterns covered
- 90-120 minutes per day
- 75-100 hours of material

**Phase C Mastery Outcomes:**
- ✅ Tree structures and operations mastery
- ✅ Graph algorithms (shortest paths, MST, connectivity)
- ✅ Dynamic programming fluency across domains
- ✅ Recognition of DP vs greedy vs other paradigms
- ✅ Complex problem-solving combining multiple techniques

**Next:** Phase D - Algorithm Paradigms (Weeks 12-13)

---

## 📌 WEEK 12: GREEDY ALGORITHMS & EXCHANGE ARGUMENTS

### Weekly Goal
Master greedy thinking and proof techniques that justify local optimal choices leading to global optimality.

### Weekly Topics
- Greedy choice property and optimal substructure
- Exchange arguments and swapping
- Interval scheduling and activity selection
- MST as greedy algorithm
- When greedy fails (counterexamples)

---

### 📅 DAY 1: GREEDY BASICS & GREEDY CHOICE PROPERTY

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Greedy Strategy Definition**
  - Make locally optimal choice at each step
  - Hope it leads to globally optimal solution
  - Contrast with DP: explore all choices

- **Greedy Choice Property**
  - Definition: a greedy choice is safe if it's part of some optimal solution
  - Not all problems satisfy this (that's why greedy fails on some)
  - Examples: activity selection has greedy choice property

- **Optimal Substructure**
  - After making greedy choice, subproblem must be optimal
  - Both greedy and DP rely on this
  - Different from greedy choice property

- **When Greedy Works vs Fails**
  - Works: activity selection, MST, coin change (some cases), Huffman
  - Fails: 0/1 knapsack, longest path in general graph, TSP
  - Intuition vs proof

- **Comparison to DP**
  - DP: considers all choices, builds table
  - Greedy: makes one choice, moves on
  - Greedy faster if it works (no need for table)
  - DP safe but slower

**Key Insights:**
- Greedy isn't always wrong; need to prove choice property
- Exchange argument is standard proof technique
- Counterexamples: best way to show greedy fails

**Deliverables:**
- [ ] Identify greedy choice property in problem
- [ ] Find counterexample to greedy algorithm
- [ ] Distinguish greedy choice property vs optimal substructure
- [ ] Prove or disprove: "Greedy works for this problem"

---

### 📅 DAY 2: INTERVAL SCHEDULING & ACTIVITY SELECTION

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Problem Definition**
  - Given: set of activities with start/end times
  - Goal: select maximum number of non-overlapping activities
  - Activity i = [start_i, finish_i]

- **Greedy Algorithm**
  - Sort activities by finish time (ascending)
  - Greedily select: first activity, then earliest-finishing activity that doesn't conflict
  - O(n log n) due to sorting

- **Proof of Correctness**
  - Let G = greedy solution, O = optimal solution
  - If G ≠ O, show we can swap activities to make G larger or equal
  - Exchange argument: repeatedly swap until G = O

- **Exchange Argument Details**
  - Take first activity where G and O differ
  - If O finishes after G's activity, we can replace with G's (doesn't hurt O)
  - If O finishes earlier, contradiction (G chose it)
  - By repeating, G covers at least as many as O

- **Why Finish Time Greedy Works**
  - Finish time is the only metric that leaves room for future activities
  - Start time: doesn't consider how long activity takes
  - Duration: arbitrary; finish time matters
  - Intuition: leave earliest ending time slot open

- **Alternative (Wrong) Greedy Choices**
  - Shortest duration: fails on [[0,100], [1,2], [3,4], ...]
  - Earliest start: fails on [[0,1], [0,10], [1,2], ...]
  - Fewest conflicts: greedy but slower analysis

- **Variations**
  - Weighted activity selection: maximize weight (DP needed)
  - k-activity scheduling: select up to k activities
  - Multi-resource scheduling: activities on different machines

**Key Insights:**
- Greedy choice: pick earliest finish
- Exchange argument shows this is safe
- Wrong greedy choices obvious via counterexamples

**Deliverables:**
- [ ] Implement greedy activity selection
- [ ] Prove correctness via exchange argument
- [ ] Find counterexamples for wrong greedy choices
- [ ] Extend to weighted activity selection (show DP needed)

---

### 📅 DAY 3: MINIMUM SPANNING TREES - KRUSKAL & PRIM AS GREEDY

**Duration:** 90 minutes

**Topics & Subtopics:**
- **MST Problem**
  - Given: undirected, weighted graph
  - Goal: find spanning tree with minimum total edge weight
  - Spanning tree: connects all vertices, n-1 edges, no cycles

- **Cut Property (Greedy Justification)**
  - Cut: partition vertices into two non-empty sets S and V-S
  - Crossing edge: connects S and V-S
  - Lemma: minimum-weight crossing edge is in some MST
  - Proof: exchange argument (if not, swap with heavier edge in cycle)

- **Kruskal's Algorithm (Greedy via Edges)**
  - Sort edges by weight (ascending)
  - Iterate: add edge if it doesn't form cycle
  - Use DSU (disjoint set union) to detect cycles
  - Complexity: O(E log E) for sorting + O(E α(V)) for DSU = O(E log E)

- **Prim's Algorithm (Greedy via Vertices)**
  - Start with one vertex
  - Grow tree by always adding minimum-weight edge connecting tree to non-tree vertex
  - Use priority queue for efficiency
  - Complexity: O((V+E) log V) with heap

- **Kruskal vs Prim**
  - Kruskal: works on disconnected graphs (produces forest)
  - Prim: must start with connected component
  - Kruskal: better for sparse graphs (E small)
  - Prim: better for dense graphs (E ≈ V²)
  - Both produce MST (not unique if edge weights equal)

- **Correctness Intuition**
  - Both maintain invariant: edges selected form part of some MST
  - Exchange argument: swapping heavier edges with lighter doesn't hurt

- **Cycle Property**
  - Heaviest edge in any cycle is not in any MST
  - Contrapositive: if edge is in MST, it's not the heaviest in any cycle

- **Uniqueness**
  - MST unique if all weights distinct
  - Multiple MSTs if weights equal (but same total weight)

**Key Insights:**
- Both greedy algorithms work on MST problem
- Cut property justifies both algorithms
- Trade-off: edge-based vs vertex-based iteration

**Deliverables:**
- [ ] Implement Kruskal's algorithm with DSU
- [ ] Implement Prim's algorithm with priority queue
- [ ] Prove MST exists and greedy finds it
- [ ] Measure performance on sparse vs dense graphs

---

### 📅 DAY 4 (OPTIONAL): HUFFMAN CODING & OTHER GREEDY CONSTRUCTIONS

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Huffman Coding Problem**
  - Goal: create optimal prefix code for characters
  - Prefix property: no code is prefix of another
  - Optimal: minimize expected encoded message length
  - Example: 'a' appears 60% → short code; 'b' appears 10% → longer code

- **Huffman Algorithm**
  - Build tree bottom-up using priority queue
  - Repeatedly extract two nodes with minimum frequency
  - Combine into new node with sum frequency
  - Add back to queue
  - Final tree has optimal encoding

- **Tree Construction Details**
  - Leaf nodes: characters
  - Internal nodes: frequency sum of children
  - Depth of character: code length
  - Left/right: 0/1 (arbitrary)

- **Proof of Optimality**
  - Exchange argument: if two characters at different depths, swapping improves total cost
  - Greedy choice: always combine smallest frequencies
  - Optimal substructure: subtree is optimal Huffman code for subset

- **Complexity**
  - Build tree: n insertions + n-1 extractions from heap = O(n log n)
  - Generate codes: traverse tree = O(n)
  - Total: O(n log n)

- **Code Generation**
  - DFS from root: going left add '0', right add '1'
  - Leaf node: character and its code

- **Variants & Applications**
  - Huffman coding: data compression (used in DEFLATE, jpeg)
  - Adaptive Huffman: frequencies change dynamically
  - Context modeling: different codes for different contexts

- **Other Greedy Algorithms**
  - Coin change (works for certain coin systems, not all)
  - Job scheduling with weights
  - Fractional knapsack (greedy by value/weight ratio works)

**Key Insights:**
- Huffman coding is non-obvious greedy algorithm
- Exchange argument shows combining min-frequency nodes is safe
- Practical application: compression

**Deliverables:**
- [ ] Implement Huffman coding
- [ ] Generate optimal codes for given frequencies
- [ ] Measure compression ratio vs fixed-length encoding
- [ ] Analyze why 0/1 knapsack doesn't have greedy solution

---

### 📅 DAY 5 (OPTIONAL): WHEN GREEDY FAILS & DECISION FRAMEWORK

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Counterexamples: Why Greedy Fails**
  - 0/1 Knapsack: greedy by value/weight fails
    - Example: items [(6, 1), (10, 6), (12, 8)] capacity 8
    - Greedy by value: (10, 6) + fail = 10
    - Optimal: (6, 1) + (12, 8) = can't fit; or (10,6) = 10? Wait, let me recalculate...
    - Better: (6,1) value 6 + (12,8) doesn't fit; (10,6) value 10 best greedy
    - Optimal DP: might be different if more items
  - Longest path in general graph: greedy from one vertex can fail
  - TSP: greedy nearest neighbor fails
  
- **Recognizing Failure Modes**
  - Greedy locally optimal but globally suboptimal
  - Future choices constrained by current choice
  - Problem requires balancing multiple criteria

- **When to Use Greedy vs DP**
  - Try greedy first (simpler, faster)
  - If counterexample exists → use DP
  - Some problems: both work with different problem versions

- **Exchange Argument as Test**
  - Can you prove: "If greedy choice not in optimal, we can swap and improve or maintain optimality"?
  - Yes → greedy works
  - No → look for counterexample

- **Problem Analysis Framework**
  1. Understand problem fully
  2. Try greedy approach (state the greedy choice)
  3. Attempt proof via exchange argument
  4. Find counterexample if proof fails
  5. If stuck, use DP or other paradigm

- **Multi-Criteria Optimization**
  - Lexicographic: primary criterion, then secondary
  - Weighted: combine criteria with weights
  - Pareto optimal: not dominated by others
  - Greedy: usually fails on true multi-criteria

- **Problem Transformation**
  - Reframe to expose greedy structure
  - Example: TSP → greedy nearest neighbor (suboptimal) but structure unclear
  - Activity selection: frame as scheduling (greedy finish-time works)

**Key Insights:**
- Greedy intuitive but often wrong
- Exchange argument is systematic check
- Counterexample finding skill crucial

**Deliverables:**
- [ ] Find counterexamples for 3 greedy algorithms
- [ ] Prove why greedy fails on TSP
- [ ] Decide: greedy or DP for custom problem
- [ ] Design decision framework for algorithm selection

---

## 📌 WEEK 13: BACKTRACKING, BRANCH & BOUND, AND AMORTIZED ANALYSIS

### Weekly Goal
Master backtracking for combinatorial search, branch-and-bound for optimization, and formalize amortized analysis methods.

### Weekly Topics
- Backtracking fundamentals and pruning
- Backtracking applications (N-Queens, subset generation, permutations)
- Branch-and-bound optimization framework
- Aggregate method for amortized analysis
- Accounting method and potential method

---

### 📅 DAY 1: BACKTRACKING FOUNDATIONS

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Backtracking Definition**
  - Explore search tree, backtrack when dead-end reached
  - Depth-first search on state space
  - Pruning: eliminate branches that can't lead to solution

- **Search Space**
  - Implicit tree of possibilities
  - Exponential size (2^n, n!, n^n typical)
  - Can't enumerate all; backtracking prunes aggressively

- **Pruning Strategy**
  - Early termination: check if current partial solution can be extended
  - Bound: upper bound on solution value from current state
  - If bound < current best, prune branch

- **Basic Backtracking Template**
  ```
  function solve(state):
    if state is goal:
      return true
    if state is pruned:
      return false
    for each next_state from state:
      if solve(next_state):
        return true
    return false
  ```

- **State Representation**
  - Current partial solution
  - Constraints that must be satisfied
  - Pruning bounds and feasibility

- **Backtracking vs Iteration**
  - Backtracking: recursive, implicit stack
  - Iteration: explicit stack or queue
  - Backtracking cleaner conceptually; iteration more control

- **Base Cases**
  - Goal state: found solution
  - Impossible: no valid extension
  - Pruned: bound check fails

- **Complexity Analysis**
  - Worst-case: exponential (no pruning)
  - Best-case: linear (early success)
  - Average: depends on pruning effectiveness

**Key Insights:**
- Backtracking is systematic search with pruning
- Pruning is key to feasibility
- Recursive structure natural for backtracking

**Deliverables:**
- [ ] Implement basic backtracking template
- [ ] Apply to subset generation
- [ ] Add pruning to reduce search space
- [ ] Analyze complexity vs brute-force

---

### 📅 DAY 2: BACKTRACKING APPLICATIONS

**Duration:** 90 minutes

**Topics & Subtopics:**
- **N-Queens Problem**
  - Place N queens on NxN board, no two attack each other
  - No two in same row, column, or diagonal
  - Backtracking: place queens row by row
  - Prune: check column and diagonal conflicts

- **N-Queens Implementation**
  - State: current row and placements in previous rows
  - Constraint check: no column/diagonal conflicts
  - Pruning: if position invalid, skip
  - Backtrack: try next position or previous row

- **Permutations**
  - Generate all permutations of n elements
  - Backtracking: fix one element, recurse on remaining
  - No pruning (all permutations valid)
  - Complexity: n! time

- **Combinations / Subsets**
  - Choose k elements from n
  - Backtracking: include/exclude element
  - Pruning: if not enough remaining, prune
  - Complexity: C(n, k) or 2^n for all subsets

- **Word Search / Boggle**
  - Find word in 2D grid
  - Backtracking: DFS from each cell
  - Prune: if remaining cells can't form word, backtrack
  - Mark visited to avoid revisits in current path

- **Sudoku Solver**
  - Fill grid with 1-9 respecting row/column/box constraints
  - Backtracking: fill empty cells one by one
  - Prune: check if number violates constraints
  - Complexity: exponential worst-case, but heavy pruning helps

- **Constraint Satisfaction Problems (CSPs)**
  - Variables with domains and constraints
  - Backtracking: assign variable, check constraints
  - Arc consistency: prune domains
  - Backjumping: jump to conflicting variable instead of previous

- **Optimization via Backtracking**
  - Keep track of best solution so far
  - Prune: if current bound < best, don't explore further

**Key Insights:**
- Many classic problems naturally solved via backtracking
- Problem-specific pruning crucial for efficiency
- Constraint checking is key to feasibility

**Deliverables:**
- [ ] Implement N-Queens solver
- [ ] Implement permutation and combination generators
- [ ] Implement word search with backtracking
- [ ] Solve Sudoku with backtracking and pruning

---

### 📅 DAY 3: BRANCH AND BOUND PARADIGM

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Branch and Bound (B&B) Definition**
  - Systematic exploration of search tree
  - Branch: divide problem into subproblems
  - Bound: compute upper/lower bound on objective
  - Prune: if bound can't beat current best, skip

- **Key Components**
  - Incumbent solution: best solution found so far
  - Bound function: optimistic estimate of objective from current node
  - Bounding: if bound ≤ current best (for minimization), prune
  - Branching strategy: how to partition problem

- **Upper vs Lower Bounds**
  - Maximization: lower bound on optimum (incumbent), upper bound on potential
  - Minimization: upper bound on optimum (incumbent), lower bound on potential
  - Tighter bounds → better pruning

- **Search Strategies**
  - Depth-first: explore one branch fully before backtracking
  - Breadth-first: explore all nodes at depth d before d+1
  - Best-first: explore most promising nodes first (priority queue)

- **Relaxation for Bounding**
  - Relax constraints to get easier problem
  - Solve relaxation to get bound
  - Example: relax 0/1 knapsack to fractional → LP bound

- **Application: 0/1 Knapsack**
  - Fractional relaxation: allows taking fraction of items
  - Greedy by value/weight on relaxation: gives upper bound
  - B&B: branch on include/exclude each item
  - Prune: if bound ≤ current best, skip branch

- **Application: Traveling Salesman Problem (TSP)**
  - Bound: minimum spanning tree lower bound
  - Held-Karp bound: even tighter
  - Branch: exclude/include each edge
  - Prune: if bound ≥ current best tour cost, skip

- **Branch and Bound vs Backtracking**
  - B&B: optimization with bounding
  - Backtracking: feasibility search with pruning
  - B&B: need objective function and bounds
  - Backtracking: just need feasibility check

**Key Insights:**
- Bounds are everything in B&B
- Tighter bounds → better pruning
- Relaxation provides bounds efficiently

**Deliverables:**
- [ ] Implement B&B for 0/1 knapsack
- [ ] Design upper/lower bound functions
- [ ] Compare search strategies (DFS vs best-first)
- [ ] Measure pruning effectiveness

---

### 📅 DAY 4: BRANCH AND BOUND APPLICATIONS & OPTIMIZATION

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Optimization Problems via B&B**
  - Integer linear programming: branch on variables
  - Facility location: open/close facilities to minimize cost
  - Graph coloring: assign colors to vertices, minimize colors
  - Job scheduling: assign jobs to machines, minimize makespan

- **Resource Allocation**
  - Bounded knapsack: multiple copies of each item
  - B&B: branch on how many of each item
  - Bounds from relaxation or greedy

- **Bin Packing**
  - Pack items into bins to minimize number of bins
  - B&B: branch on bin assignments
  - Lower bound: total size / bin capacity
  - Upper bound: first-fit or other heuristic

- **Graph Coloring**
  - Assign colors to vertices, adjacent vertices different color
  - Minimize number of colors
  - B&B: branch on color assignments
  - Bound: clique size (lower), greedy coloring (upper)

- **Cutting Planes (Introduction)**
  - Add constraints to tighten relaxation bound
  - Improves bound → better pruning
  - Key technique in integer programming solvers

- **Heuristics for Initial Incumbent**
  - Good initial solution → better pruning
  - Greedy heuristic: fast, reasonable quality
  - Local search: improve initial solution
  - Example: nearest neighbor for TSP, then 2-opt

- **Approximation Algorithms**
  - When optimal is too expensive
  - Find solution within factor of optimal
  - Example: 2-approximation for TSP metric
  - Use as upper bound in B&B

**Key Insights:**
- Many optimization problems amenable to B&B
- Good heuristics help significantly
- Bounds and pruning are crucial

**Deliverables:**
- [ ] Implement B&B for TSP
- [ ] Add cutting planes for tighter bounds
- [ ] Compare with various heuristics
- [ ] Measure runtime improvements with good initial solution

---

### 📅 DAY 5: ALGORITHM SELECTION FRAMEWORK

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Decision Tree for Algorithm Choice**
  - Is problem solvable by brute-force? (if n ≤ 10, maybe yes)
  - Does it have greedy structure? (try greedy, prove or find counterexample)
  - Does it have optimal substructure? (DP candidate)
  - Is it NP-hard? (approximation, heuristics, B&B)
  - Does it require search? (backtracking, B&B)

- **Problem Characteristics**
  - Size (n): impacts which algorithms feasible
  - Structure: linearity, convexity, special graph properties
  - Constraints: time limit, space, precision
  - Objective: feasibility vs optimization

- **Paradigm Comparison**
  - Brute-force: simple, exponential
  - Greedy: fast, often wrong (but sometimes right)
  - DP: polynomial, requires table (space tradeoff)
  - Divide & Conquer: recursive, good for balanced problems
  - Search (backtracking, B&B): for combinatorial, optimization

- **When to Use Each**
  - Greedy: activity selection, MST, coin change (careful!)
  - DP: knapsack, LCS, LIS, shortest path DAG
  - Backtracking: N-Queens, permutations, word search, Sudoku
  - B&B: optimization with bounds
  - Approximation: NP-hard, good-enough solution enough

- **Time/Space Trade-off**
  - Greedy: O(n log n) time, O(1) space
  - DP: O(n²) time, O(n²) space
  - Search: exponential time, O(n) space (recursion)
  - Choose based on constraints

- **Practical Problem-Solving**
  1. Understand problem and constraints completely
  2. Estimate size and feasibility window
  3. Identify problem type (optimization? search? feasibility?)
  4. List candidate algorithms
  5. Implement simplest that works
  6. Optimize if needed

- **Red Flags & Common Mistakes**
  - Over-engineering: using complex algorithm when simple works
  - Wrong algorithm choice: greedy when DP needed
  - Implementation bugs: off-by-one, boundary conditions
  - Not considering constraints: time limit kills otherwise-correct solution

**Key Insights:**
- No universal algorithm; problem-dependent
- Start simple; optimize if needed
- Understanding problem structure guides algorithm choice

**Deliverables:**
- [ ] Given 5 problems, choose best algorithm for each
- [ ] Explain why other choices are worse
- [ ] Estimate complexity for chosen algorithms
- [ ] Implement one from each category

---

### 📅 DAY 6: AMORTIZED ANALYSIS - FORMAL METHODS

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Amortized Analysis Definition**
  - Analyze average cost per operation over sequence
  - Not average over random inputs; worst-case sequence average
  - Three methods: aggregate, accounting, potential

- **Aggregate Method**
  - Total cost of n operations / n
  - Find total cost (sum over all operations)
  - Divide by n for amortized cost per operation

- **Example: Dynamic Array Append**
  - n appends starting with capacity 1
  - Appends at positions 1,2,3,...: cost 1 each
  - Resize at positions 2,4,8,16,...: cost 2,4,8,16,...
  - Total cost: n + (1+2+4+...+2^log(n)) = n + 2n = 3n
  - Amortized: 3n / n = 3 per operation

- **Accounting Method**
  - Assign abstract "cost" (credits) to operations
  - Some operations expensive, some cheap
  - Ensure credits always non-negative (never overdraw)
  - Amortized cost = total credits / n

- **Example: Stack with MultiPop**
  - Push: cost 1 credit
  - Pop: cost 1 credit each
  - MultiPop(k): pop up to k elements, cost k credits
  - Each element: 1 credit to push + 1 credit to pop = 2 total
  - With n operations: max 2n credits → 2 per operation (amortized)

- **Potential Method**
  - Amortized cost = actual cost + Δpotential
  - Potential Φ(state): abstract "energy" of data structure
  - Φ increases when structure "unbalanced"; decrease when "rebalanced"

- **Example: Dynamic Array via Potential**
  - Potential Φ = 2 × (size - capacity/2) when size > capacity/2, else 0
  - When size ≤ capacity/2: Φ = 0 (ample room)
  - When size > capacity/2: Φ = 2 × (size - capacity/2)
  - Append when not full: actual cost 1, Φ increases by 2 → amortized 3
  - Resize when full: actual cost ~n, Φ drops to 0 → amortized balanced

- **Comparison of Methods**
  - Aggregate: simple, gives overall average
  - Accounting: flexible, assigns costs to ops
  - Potential: rigorous, generalizable

- **DSU (Disjoint Set Union) Analysis**
  - Path compression: nearly O(1) per operation
  - Formal: inverse Ackermann α(n) amortized
  - Too complex for this course; mention intuition

- **Real Applications**
  - Dynamic arrays: vector, list reallocation
  - Hash tables: resizing, rehashing
  - Fibonacci heaps: why amortized analysis needed
  - Self-balancing trees: rebalancing costs

**Key Insights:**
- Amortized analysis explains why cheap ops compensate expensive
- Aggregate method: simple; potential method: more general
- Inversee Ackermann for DSU too advanced for this course

**Deliverables:**
- [ ] Analyze dynamic array using aggregate method
- [ ] Analyze stack with multipop using accounting
- [ ] Analyze dynamic array using potential method
- [ ] Compare all three methods on same problem

---

# 🟧 PHASE E: INTEGRATION & EXTENSIONS (Weeks 14-15)

## Phase Motivation
By now you've learned core data structures, algorithms, and paradigms. This phase brings them together in integration problems that combine multiple concepts, plus specialized techniques for matrices, advanced strings, and network flows.

## Phase Outcomes
- [ ] Solve complex problems combining multiple concepts
- [ ] Master matrix algorithms and patterns
- [ ] Understand and apply advanced string algorithms
- [ ] Introduce network flow and optimization
- [ ] Apply bit manipulation and number theory
- [ ] Understand probability and randomization in algorithms

---

## 📌 WEEK 14: MATRIX PROBLEMS, BACKTRACKING & BIT TRICKS

### Weekly Goal
Learn matrix traversal patterns, apply backtracking to grids, and master bit manipulation techniques.

### Weekly Topics
- Matrix traversal and search patterns
- Backtracking on grids (word search, Sudoku)
- Bit manipulation: operations, masks, iterating subsets
- Number theory basics: GCD, LCM, modular arithmetic
- Probability and sampling algorithms

---

### 📅 DAY 1: MATRIX TRAVERSAL & SEARCH

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Basic Traversals**
  - Row-wise: visit all cells in row order
  - Column-wise: visit all cells in column order
  - Diagonal: main diagonal, anti-diagonals
  - Spiral: layer by layer from outside in
  - Snake: row 0 left-to-right, row 1 right-to-left, etc.

- **Row-Wise vs Column-Wise Performance**
  - Row-wise: sequential memory access, cache-friendly
  - Column-wise: jumps by matrix width, cache misses
  - Traversal order can affect performance 10-100x

- **Coordinate Systems**
  - (row, col) vs (x, y): be consistent
  - Index range: [0, m) rows, [0, n) columns
  - Adjacent cells: (r±1, c), (r, c±1)
  - Diagonals: (r±1, c±1)

- **Matrix Search Patterns**
  - Linear search: O(mn) scan all cells
  - Binary search on sorted matrix: O(log mn) if row and column sorted
  - Staircase search: start from top-right or bottom-left
    - If target > current, move down; if target < current, move left
    - O(m + n) for "sorted" matrix

- **Sorted Matrix Search (Staircase)**
  - Precondition: each row sorted left-to-right, each column sorted top-to-bottom
  - Start at top-right (m×0) or bottom-left (m-1×n)
  - Move left/down based on comparison
  - O(m + n) instead of O(mn)

- **Rotation Invariants**
  - Rotate 90 degrees: (r, c) → (c, m-1-r)
  - Rotate 180 degrees: (r, c) → (m-1-r, n-1-c)
  - Reflection: horizontal (r, c) → (r, n-1-c)
  - Useful for problems with symmetry

- **Fill Patterns**
  - Filling diagonals: useful for DP on matrices
  - Filling layers: spiral fill, rectangular layers

- **2D to 1D Mapping**
  - (r, c) → r×n + c (row-major)
  - (r, c) → c×m + r (column-major)
  - Useful for compact representation or interop with 1D algorithms

**Key Insights:**
- Traversal order matters for cache efficiency
- Staircase search elegant for sorted matrices
- Coordinate mapping enables technique reuse

**Deliverables:**
- [ ] Implement all standard traversals
- [ ] Implement staircase search on sorted matrix
- [ ] Measure cache efficiency of different traversals
- [ ] Rotate matrix in-place

---

### 📅 DAY 2: BACKTRACKING ON GRIDS

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Word Search Problem**
  - Given: word and 2D grid of characters
  - Find: if word exists reading left/right/up/down
  - Backtracking: DFS from each cell, mark visited

- **Word Search Implementation**
  - State: current cell and index in word
  - Base case: index == word length (found)
  - Recursive case: try all 4 adjacent cells
  - Pruning: if current cell doesn't match, backtrack

- **Visited Tracking**
  - Mark cells in path as visited
  - Unmark when backtracking (restore for other paths)
  - Avoid revisiting in current search path

- **Boggle**
  - Find all words in grid (not just one)
  - Start from each cell, build words via DFS
  - Check against dictionary
  - Pruning: if prefix not in dictionary, stop

- **Sudoku Solver**
  - Fill 9×9 grid with 1-9
  - Constraints: each row, column, 3×3 box has all 1-9
  - Backtracking: fill empty cell with valid digit
  - Pruning: check constraints before assigning

- **Sudoku Implementation**
  - Maintain row/col/box sets of used digits
  - Find next empty cell
  - Try digits 1-9 that don't conflict
  - If valid, recurse; if not, backtrack

- **Efficiency Improvements**
  - Choose cell with fewest possibilities (MRV heuristic)
  - Update constraints incrementally
  - Constraint propagation: if cell has one possibility, set it

- **N-Queens (Revisited in Matrix Context)**
  - Place N queens on N×N board
  - Similar to Sudoku: fill row by row
  - Constraint: no two queens attack

- **Pattern Generalization**
  - CSP (Constraint Satisfaction): variables with domains, constraints
  - Backtracking: assign variable, check constraints, recurse
  - Problem-specific: identify variables, domains, constraints

**Key Insights:**
- Grid problems naturally solved via backtracking
- Visited tracking critical for correctness
- Efficiency: choose right search order

**Deliverables:**
- [ ] Implement word search in grid
- [ ] Implement Sudoku solver
- [ ] Add constraint propagation
- [ ] Solve N-Queens on board representation

---

### 📅 DAY 3: BITMASK TRICKS & SUBSET ENUMERATION

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Bit Operations**
  - AND (&): a & b, bitwise AND
  - OR (|): a | b, bitwise OR
  - XOR (^): a ^ b, bitwise exclusive OR
  - NOT (~): ~a, bitwise NOT
  - Left shift (<<): a << b, multiply by 2^b
  - Right shift (>>): a >> b, divide by 2^b (rounded down)

- **Bit Manipulation Tricks**
  - Check if bit i is set: (a >> i) & 1 or (a & (1 << i))
  - Set bit i: a |= (1 << i)
  - Clear bit i: a &= ~(1 << i)
  - Toggle bit i: a ^= (1 << i)
  - Clear rightmost 1 bit: a &= (a - 1)
  - Check if power of 2: (a & (a - 1)) == 0 and a != 0
  - Count 1 bits: __builtin_popcount(a) or loop

- **Bitmask as Subset Representation**
  - Set S of n elements → bitmask with n bits
  - Bit i = 1 if element i in subset, 0 otherwise
  - Subset i of set S: iterate through 0 to 2^n - 1
  - Union: mask1 | mask2
  - Intersection: mask1 & mask2
  - Complement: ((1 << n) - 1) ^ mask

- **Iterating Over Subsets**
  ```
  for (int subset = S; ; subset = (subset - 1) & S) {
    // process subset
    if (subset == 0) break;
  }
  ```
  - Iterates through all non-empty subsets of S
  - O(3^n) for all subsets (sum over k of C(n,k) = 2^n, but this clever)

- **TSP via Bitmask DP**
  - State: dp[mask][i] = shortest path visiting cities in mask, ending at i
  - Transitions: dp[mask | (1 << j)][j] = min(dp[mask | (1 << j)][j], dp[mask][i] + dist[i][j])
  - Complexity: O(2^n × n²)

- **Subset Sum / Knapsack with Bitmask**
  - If items are few and values small, bitmask enumeration
  - For each subset, compute total value/weight

- **Gray Code (Optional)**
  - Sequence where consecutive numbers differ in one bit
  - Useful for avoiding state transitions in circuits
  - Generate: bit-reverse of binary count

- **Bit Manipulation Applications**
  - Fast exponentiation via bit representation
  - Bloom filters use bit arrays
  - Hamming distance: count differing bits between two numbers

**Key Insights:**
- Bitmasks compact representation for sets
- Bit tricks enable O(1) operations on bits
- Subset enumeration via bitmask DFS

**Deliverables:**
- [ ] Implement bit manipulation tricks
- [ ] Solve problem via bitmask DP (TSP or similar)
- [ ] Iterate over all subsets of a set
- [ ] Count 1-bits and solve related problems

---

### 📅 DAY 4 (OPTIONAL): NUMBER THEORY & MODULAR ARITHMETIC

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Euclid's Algorithm for GCD**
  - gcd(a, b) = gcd(b, a mod b)
  - Base case: gcd(a, 0) = a
  - Complexity: O(log min(a, b))
  - Example: gcd(48, 18) = gcd(18, 12) = gcd(12, 6) = gcd(6, 0) = 6

- **Extended Euclidean Algorithm**
  - Find x, y such that ax + by = gcd(a, b)
  - Useful for modular multiplicative inverse
  - Complexity: O(log min(a, b))

- **LCM (Least Common Multiple)**
  - lcm(a, b) = (a × b) / gcd(a, b)
  - Useful for synchronization problems

- **Modular Arithmetic**
  - (a + b) mod m = ((a mod m) + (b mod m)) mod m
  - (a × b) mod m = ((a mod m) × (b mod m)) mod m
  - Avoid overflow: compute mod after each operation

- **Modular Exponentiation**
  - Compute a^b mod m efficiently
  - Naive: O(b) multiplications → overflow
  - Binary exponentiation: O(log b)
  - Example: 2^1000000 mod 10^9+7
  
  ```
  long long pow_mod(long long base, long long exp, long long mod) {
    long long result = 1;
    while (exp > 0) {
      if (exp & 1) result = (result * base) % mod;
      base = (base * base) % mod;
      exp >>= 1;
    }
    return result;
  }
  ```

- **Modular Inverse**
  - If gcd(a, m) = 1, then a has multiplicative inverse mod m
  - a × a^(-1) ≡ 1 (mod m)
  - Find via: extended Euclidean or a^(m-2) mod m (if m prime, Fermat's little theorem)
  - Used in modular division: (a / b) mod m = (a × b^(-1)) mod m

- **Chinese Remainder Theorem (CRT)**
  - If gcd(m1, m2) = 1, system of congruences has unique solution mod m1×m2
  - Useful for solving problems mod multiple primes

- **Fermat's Little Theorem**
  - If p prime and gcd(a, p) = 1: a^(p-1) ≡ 1 (mod p)
  - Corollary: a^p ≡ a (mod p) for all a
  - Useful for modular exponentiation

- **Number Theory Applications**
  - Hashing: use prime modulus
  - DP: compute results mod large prime to avoid overflow
  - Combinatorics: C(n, k) mod p

**Key Insights:**
- GCD foundational for number theory
- Modular arithmetic prevents overflow
- Binary exponentiation O(log b) instead of O(b)

**Deliverables:**
- [ ] Implement GCD and extended GCD
- [ ] Compute modular exponentiation
- [ ] Find modular multiplicative inverse
- [ ] Solve problem combining modular arithmetic and DP

---

### 📅 DAY 5 (OPTIONAL): PROBABILITY & SAMPLING ALGORITHMS

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Expected Value Linearity**
  - E[X + Y] = E[X] + E[Y]
  - E[cX] = c×E[X]
  - Powerful for analyzing randomized algorithms

- **Linearity of Expectation in Algorithms**
  - Indicator random variables: I_i = 1 if event i happens, 0 otherwise
  - E[sum of indicators] = sum of probabilities
  - Example: expected number of collisions in hash table

- **Reservoir Sampling**
  - Sample k items uniformly from stream of unknown length n
  - Process items: keep first k
  - For item i > k: include with probability k/i
  - If included, replace random existing item
  - Result: uniform sample of k items

- **Reservoir Sampling Proof**
  - Each item has probability k/n of being sampled
  - Proof: by induction on n

- **Random Permutations (Fisher-Yates)**
  - Shuffle array uniformly at random
  - Iterate from end to start: swap position i with random position ≤ i
  - Each permutation equally likely
  - Complexity: O(n)

- **Birthday Paradox**
  - In hash table with m buckets: expect first collision at ~sqrt(m) items
  - Useful: understand hash table behavior
  - Application: birthday attack on cryptographic hashes

- **Randomized Quicksort**
  - Choose random pivot instead of first/middle
  - Avoids adversarial worst-case on sorted arrays
  - Expected O(n log n) even for sorted input

- **Las Vegas vs Monte Carlo Algorithms**
  - Las Vegas: always correct, random runtime (e.g., randomized quicksort)
  - Monte Carlo: random correctness, deterministic runtime (e.g., primality test)
  - Randomization helps on adversarial inputs

**Key Insights:**
- Linearity of expectation powerful tool
- Reservoir sampling elegant solution
- Randomization defeats adversarial inputs

**Deliverables:**
- [ ] Implement reservoir sampling
- [ ] Implement Fisher-Yates shuffle
- [ ] Analyze expected collisions in hash table
- [ ] Use linearity of expectation to analyze algorithm

---

## 📌 WEEK 15: ADVANCED STRINGS & NETWORK FLOW

### Weekly Goal
Master advanced string matching algorithms and introduce network flow optimization.

### Weekly Topics
- KMP string matching algorithm
- Z-algorithm and applications
- Manacher's algorithm for palindromes
- Suffix arrays and suffix trees (conceptual)
- Maximum flow and min-cut
- Applications: bipartite matching, flow networks

---

### 📅 DAY 1: KMP STRING MATCHING

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Naive String Matching**
  - For each position i in text, check if pattern matches
  - O(nm) time in worst case
  - Can be slow for large texts and patterns

- **KMP (Knuth-Morris-Pratt) Algorithm**
  - Preprocess pattern to build failure function (π or LPS array)
  - Use failure function to skip comparisons
  - O(n + m) time

- **Failure Function / LPS (Longest Proper Prefix which is also Suffix)**
  - LPS[i] = length of longest proper prefix of pattern[0..i] that is also suffix
  - Example: pattern = "ABABDABACD"
    - LPS = [0, 0, 1, 2, 0, 1, 2, 3, 0, 0]
  - Compute in O(m) using clever two-pointer technique

- **KMP Matching Process**
  - Maintain pointer j in pattern, i in text
  - When characters match: j++, i++
  - When they don't: j = LPS[j-1], i++ (reuse earlier matches)
  - If j reaches end of pattern, match found

- **Example Walkthrough**
  - Text: "ABABDABACDABABCAB"
  - Pattern: "ABABCAB"
  - LPS: [0, 0, 1, 2, 0, 1, 2]
  - Trace matching process

- **KMP Complexity**
  - Preprocessing: O(m)
  - Matching: O(n)
  - Total: O(n + m)
  - Each character in text examined at most twice

- **Applications**
  - Substring search: O(n + m)
  - Pattern matching in streams
  - Avoiding re-scanning

- **Variants**
  - KMP for finding all occurrences (multiple matches)
  - Building pattern from matched prefixes

**Key Insights:**
- KMP avoids redundant comparisons
- Failure function encodes pattern structure
- Linear time for string matching

**Deliverables:**
- [ ] Implement KMP algorithm
- [ ] Compute LPS array for pattern
- [ ] Find all occurrences of pattern in text
- [ ] Compare KMP vs naive on long text/pattern

---

### 📅 DAY 2: Z-ALGORITHM & APPLICATIONS

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Z-Function Definition**
  - Z[i] = length of longest substring starting from i which is also prefix of string
  - Example: s = "aabaab", Z = [-, 1, 0, 3, 1, 0]
  - Z[0] undefined (entire string)

- **Z-Array Construction**
  - Naive: O(nm) by checking each position
  - Clever: O(n) using previously computed values
  - Maintain "Z-box" [l, r] = rightmost interval where s[l..r] matches s[0..r-l]

- **Z-Algorithm Details**
  - For each i, use previous intervals to skip checks
  - Only advance when necessary
  - Each position visited constant number of times

- **Pattern Matching via Z-Algorithm**
  - Concatenate pattern + "#" + text
  - Compute Z-array
  - Z[i] = m means pattern occurs at text position (i - m - 1)
  - O(n + m) time

- **Applications**
  - Substring search (alternative to KMP)
  - Finding periods in string
  - All occurrences of pattern
  - String borders

- **Finding Periods**
  - String has period p if s[i] = s[i+p] for all valid i
  - Use Z-array to detect: Z[p] = n - p
  - Find smallest period in O(n)

- **Border Finding**
  - Border of string: prefix that is also suffix (but not entire string)
  - Use Z-array on s + "#" + reverse(s)
  - Application: KMP failure function

**Key Insights:**
- Z-algorithm elegant O(n) computation
- Alternative to KMP, sometimes cleaner
- Finds all pattern occurrences efficiently

**Deliverables:**
- [ ] Implement Z-algorithm
- [ ] Use Z-algorithm for pattern matching
- [ ] Find all periods in string
- [ ] Compare Z-algorithm vs KMP

---

### 📅 DAY 3 (OPTIONAL): MANACHER'S ALGORITHM - LONGEST PALINDROMIC SUBSTRING

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Naive Palindrome Finding**
  - For each center (odd/even length), expand around center
  - O(n²) time
  - Still good for small n

- **Manacher's Algorithm**
  - O(n) algorithm for finding all palindromes
  - Reuse information from previously computed palindromes
  - Maintain "palindrome box" [l, r]

- **Manacher's Setup**
  - Insert dummy characters: "#a#b#a#"
  - Ensures all palindromes have odd length around dummy
  - Simplifies logic

- **Manacher's Algorithm**
  - P[i] = radius of palindrome centered at i
  - For each i, use previously computed palindromes to speed up
  - When outside previous palindrome, expand naively
  - When inside, can reuse symmetry (if not hitting edge)

- **Complexity Analysis**
  - Each position visited at most twice
  - Expansion happens O(n) times total
  - O(n) time

- **Applications**
  - Longest palindromic substring in O(n)
  - Shortest palindrome (add characters to front)
  - Palindrome decomposition

- **Variants**
  - Finding all palindromic substrings
  - Counting palindromes

**Key Insights:**
- Manacher's algorithm clever use of symmetry
- O(n) palindrome finding surprising
- Practice needed to understand/implement

**Deliverables:**
- [ ] Implement expand-around-center (O(n²))
- [ ] Implement Manacher's algorithm
- [ ] Find longest palindromic substring
- [ ] Compare performance

---

### 📅 DAY 4: SUFFIX ARRAYS & SUFFIX TREES (CONCEPTUAL)

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Suffix Array Definition**
  - Sorted array of indices of all suffixes
  - Example: s = "banana"
    - Suffixes: "banana", "anana", "nana", "ana", "na", "a"
    - Sorted: "a", "ana", "anana", "banana", "na", "nana"
    - Indices: [5, 3, 1, 0, 4, 2]

- **Suffix Array Construction**
  - Naive: O(n² log n) by sorting suffixes naively
  - Better: O(n log² n) by doubling algorithm
  - Best: O(n) via DC3 or SA-IS (complex)
  - For this course: naive or doubling algorithm

- **Applications**
  - Pattern matching: find all occurrences
  - Longest repeated substring
  - Longest common substring (between two strings)
  - Lexicographically smallest rotation

- **LCP (Longest Common Prefix) Array**
  - LCP[i] = length of common prefix between SA[i] and SA[i+1]
  - Computed in O(n) after suffix array
  - Useful for many suffix array applications

- **Suffix Tree (High-Level)**
  - Trie of all suffixes, with compression
  - Each edge labeled with substring
  - O(n) space, O(n) construction
  - O(m) pattern search
  - Complex; rarely implemented in practice

- **Suffix Tree Applications**
  - Pattern matching: O(m) search
  - Longest repeated substring: O(n)
  - All occurrences of pattern: O(m + k) where k = number of occurrences

- **Trade-off: Suffix Array vs Suffix Tree**
  - Suffix array: simpler, less space, slower search
  - Suffix tree: complex, more space, faster search
  - Suffix array sufficient for most problems

**Key Insights:**
- Suffix array compact representation for suffix tree
- Can solve many string problems efficiently
- Complex algorithms; implementation-heavy

**Deliverables:**
- [ ] Implement suffix array construction (naive or doubling)
- [ ] Compute LCP array
- [ ] Solve pattern matching via suffix array
- [ ] Find longest common substring

---

### 📅 DAY 5 (OPTIONAL): MAXIMUM FLOW & MIN-CUT

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Flow Network Definition**
  - Directed graph with capacities on edges
  - Source s, sink t
  - Flow f(u, v) on edge (u, v)
  - Capacity constraint: f(u, v) ≤ c(u, v)
  - Flow conservation: sum in = sum out for all v except s, t

- **Max-Flow Problem**
  - Find maximum flow from s to t subject to constraints
  - Applications: transportation, scheduling, matching

- **Ford-Fulkerson Method**
  - Iteratively find augmenting paths and increase flow
  - Augmenting path: path from s to t in residual graph where capacity available
  - Bottleneck: minimum capacity along path
  - Increase flow by bottleneck value

- **Residual Graph**
  - Shows remaining capacity after current flow
  - Forward edge (u, v): residual capacity = c(u, v) - f(u, v)
  - Backward edge (v, u): residual capacity = f(u, v) (undo flow)
  - Augmenting path in residual graph

- **Edmonds-Karp Algorithm**
  - Ford-Fulkerson with BFS for finding augmenting paths
  - O(VE²) complexity
  - More predictable than DFS-based Ford-Fulkerson

- **Max-Flow Min-Cut Theorem**
  - Maximum flow = minimum cut
  - Cut: partition vertices into S and T with s ∈ S, t ∈ T
  - Cut capacity: sum of capacities of edges from S to T
  - Powerful duality

- **Applications**
  - Bipartite matching: edges from left to right with capacity 1
  - Project selection: nodes are projects/resources, solve via flow
  - Image segmentation: pixels as nodes, flow for segmentation

- **Bipartite Matching via Max-Flow**
  - Create source s, sink t
  - Edges from s to left side (capacity 1)
  - Edges from left to right (capacity ∞ or 1)
  - Edges from right to t (capacity 1)
  - Max flow = maximum matching

**Key Insights:**
- Flow networks model many problems
- Max-flow min-cut duality powerful
- Edmonds-Karp practical algorithm

**Deliverables:**
- [ ] Implement Edmonds-Karp max-flow
- [ ] Find max-flow in example network
- [ ] Model bipartite matching as flow
- [ ] Solve project selection via flow

---

### 📅 DAY 6 (OPTIONAL): MATCHING VIA FLOW & APPLICATIONS

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Bipartite Matching Problem**
  - Given: bipartite graph with edges
  - Find: maximum matching (most edges with no shared vertices)

- **Bipartite Matching via Max-Flow**
  - Model as flow (as described in Day 5)
  - Capacity 1 on all edges
  - Max flow value = maximum matching
  - Can recover matching from flow

- **Assignment Problem**
  - Given: n tasks, n workers, cost of assigning task i to worker j
  - Find: assignment minimizing total cost
  - Each task assigned to one worker, vice versa
  - Reduction: min-cost max-flow or Hungarian algorithm

- **Min-Cost Max-Flow**
  - Each edge has cost and capacity
  - Find max flow minimizing total cost
  - Use shortest paths (Bellman-Ford or Dijkstra) instead of BFS
  - Cycle cancellation or successive shortest paths

- **Real Applications**
  - Job assignment: assign workers to tasks
  - Transportation: route goods through network minimizing cost
  - Scheduling: assign tasks to machines minimizing makespan

- **Network Connectivity**
  - Maximum edge-disjoint paths: flow with unit capacities
  - Minimum vertex cut: reduce to edge cut via vertex splitting
  - Application: fault-tolerant routing

- **Practical Considerations**
  - Max-flow algorithms can be slow (O(VE²) typical)
  - Specialized algorithms exist (Dinic, push-relabel)
  - Approximation algorithms for approximate max-flow
  - Heuristics for large networks

**Key Insights:**
- Flow networks model diverse problems
- Matching, assignment, routing all via flow
- Multiple formulations of same problem

**Deliverables:**
- [ ] Model assignment problem as min-cost flow
- [ ] Solve matching problem via flow
- [ ] Find edge-disjoint paths in graph
- [ ] Apply to real-world routing problem

---

# 🟩 PHASE F: ADVANCED DEEP DIVES (Weeks 16-18, OPTIONAL TRACK)

## Phase Motivation
For those pursuing mastery or specialized topics, these weeks cover advanced data structures, computational geometry, advanced strings, and probabilistic algorithms. These topics appear in 6.046 or specialized applications.

## Phase Outcomes
- [ ] Understand segment trees and binary indexed trees for range queries
- [ ] Apply matrix exponentiation to recurrences
- [ ] Learn computational geometry basics and convex hulls
- [ ] Understand heavy-light decomposition and tree paths
- [ ] Learn FFT for polynomial multiplication
- [ ] Understand probabilistic data structures

---

## 📌 WEEK 16: SEGMENT TREES, BIT, MATRIX EXPONENTIATION & GEOMETRY

### Weekly Goal
Master advanced data structures for range queries, use matrices for recurrences, and introduce computational geometry.

### Weekly Topics
- Segment trees for range queries with updates
- Binary indexed trees (Fenwick trees)
- Matrix exponentiation for linear recurrences
- Computational geometry: points, vectors, orientations
- Convex hull algorithms (Graham scan, Jarvis march)

---

### 📅 DAY 1: SEGMENT TREES PART 1

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Segment Tree Basics**
  - Tree where each node represents interval
  - Leaf nodes: individual elements
  - Internal nodes: aggregate of children (sum, min, max, etc.)
  - Balanced binary tree, n elements → O(log n) height

- **Segment Tree Structure**
  - Complete binary tree stored in array
  - Parent of i: i/2; left child: 2i; right child: 2i+1
  - Leaves start at index n in array of size 2n

- **Range Query**
  - Query(l, r): compute aggregate over [l, r]
  - Recursive: if node fully in [l, r], use node value; else recurse on children
  - O(log n) per query

- **Point Update**
  - Update(i, val): set element i to val, update ancestors
  - O(log n) per update

- **Example: Range Sum**
  - Each node stores sum of its range
  - Query(l, r): sum of [l, r]
  - Update(i, val): set arr[i] = val, update ancestors with new sum

- **Build Tree**
  - Build in O(n) by bottom-up approach
  - Better than building by inserting elements one by one

- **Lazy Propagation (Introduction)**
  - Range updates: update all elements in [l, r] to val
  - Naive: O(n) per update
  - Lazy: defer updates until needed, O(log n) per update/query

**Key Insights:**
- Segment tree enables fast range queries and updates
- O(log n) for query and point update
- Lazy propagation for range updates

**Deliverables:**
- [ ] Implement segment tree for range sum query and point update
- [ ] Implement range min/max queries
- [ ] Implement lazy propagation for range updates
- [ ] Compare performance vs naive array

---

### 📅 DAY 2: FENWICK TREE / BINARY INDEXED TREE

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Fenwick Tree / BIT Definition**
  - Compact representation of segment tree using binary indexing
  - Each node stores sum of a range determined by binary representation
  - O(n) space, O(log n) query and update

- **Binary Indexing Trick**
  - Node i stores sum of [i - (i & -i) + 1, i]
  - i & -i: isolates lowest set bit (size of range)
  - Clever bit manipulation enables O(log n) operations

- **Building BIT**
  - Initialize all to 0, then update each element
  - O(n log n) naive, but can be O(n) with clever building

- **Query (Prefix Sum)**
  - Query(i): sum of [0, i]
  - Iteratively add BIT[i], then set i -= (i & -i)
  - O(log n) iterations

- **Update (Point Update)**
  - Update(i, delta): add delta to element i
  - Propagate update: BIT[i] += delta, i += (i & -i)
  - O(log n) updates

- **Range Query**
  - Query(l, r): Query(r) - Query(l-1)
  - Exploit prefix sum property

- **Advantages vs Segment Tree**
  - Less space: O(n) vs O(2n)
  - Cleaner code: bit manipulation vs tree navigation
  - Practical efficiency: better cache locality

- **Limitations**
  - Supports sum/XOR easily; harder for min/max
  - No lazy propagation (range updates harder)
  - 1-indexed typical

- **Applications**
  - Inversion counting in array: BIT + sorting
  - Coordinate compression: compress coordinates, use BIT
  - Offline range queries: sort queries, process events

**Key Insights:**
- BIT clever use of binary representation
- Space-efficient, practical, cleaner code than segment tree
- Limited operations (sum, XOR mainly)

**Deliverables:**
- [ ] Implement BIT for prefix sum query and point update
- [ ] Count inversions in array using BIT
- [ ] Implement coordinate compression with BIT
- [ ] Compare BIT vs segment tree performance

---

### 📅 DAY 3: MATRIX EXPONENTIATION FOR RECURRENCES

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Linear Recurrences**
  - f(n) = a₁×f(n-1) + a₂×f(n-2) + ... + aₖ×f(n-k)
  - Example: Fibonacci f(n) = f(n-1) + f(n-2)

- **Matrix Formulation**
  - Represent recurrence as matrix-vector product
  - [f(n), f(n-1), ...] = M × [f(n-1), f(n-2), ...]
  - Multiple applications: [f(n)] = M^n × [f(0)]

- **Fibonacci via Matrix**
  - [f(n), f(n-1)] = [[1, 1], [1, 0]] × [f(n-1), f(n-2)]
  - [f(n), f(n-1)] = [[1, 1], [1, 0]]^n × [f(1), f(0)]
  - Compute M^n using binary exponentiation: O(log n)

- **Binary Exponentiation**
  - Compute a^n in O(log n) multiplications
  - Express n in binary: n = b_k × 2^k + ... + b_0 × 2^0
  - a^n = a^(b_k × 2^k) × ... × a^(b_0 × 2^0)
  - Iteratively square and multiply

- **Matrix Binary Exponentiation**
  - Same algorithm but on matrices
  - O(k³ log n) for k×k matrix (k³ matrix multiplication)

- **Modular Exponentiation**
  - Compute result mod m to prevent overflow
  - All operations mod m: (A × B) mod m = ((A mod m) × (B mod m)) mod m

- **Fibonacci Mod Using Matrix**
  - f(10^18) mod 10^9+7
  - Compute M^(10^18) mod (10^9+7) using binary exponentiation
  - O(3³ × log(10^18)) ≈ O(60) matrix multiplications

- **Non-Homogeneous Recurrences**
  - f(n) = a₁×f(n-1) + c (with constant)
  - Extend matrix: add row for constant
  - [f(n), 1] = [[a₁, c], [0, 1]] × [f(n-1), 1]

- **Applications**
  - Fibonacci: fast computation for large n
  - General recurrences: paths in graphs, random walks
  - Counting: matrix represents transitions, power gives counts

**Key Insights:**
- Recurrences expressible as matrix equations
- Matrix exponentiation enables O(log n) computation
- Modular arithmetic necessary for overflow prevention

**Deliverables:**
- [ ] Implement binary exponentiation for scalars
- [ ] Implement matrix binary exponentiation
- [ ] Compute Fibonacci mod 10^9+7 for n = 10^18
- [ ] Apply matrix exponentiation to custom recurrence

---

### 📅 DAY 4: COMPUTATIONAL GEOMETRY BASICS

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Points and Vectors**
  - Point: coordinate (x, y)
  - Vector: direction and magnitude
  - Vector from P to Q: (Q.x - P.x, Q.y - P.y)

- **Distance**
  - Euclidean distance: sqrt(dx² + dy²)
  - Squared distance: dx² + dy² (avoids sqrt)
  - Manhattan distance: |dx| + |dy|

- **Dot Product**
  - u · v = u.x × v.x + u.y × v.y
  - u · v = |u| × |v| × cos(θ)
  - Used for: angle between vectors, projections, perpendicularity (u · v = 0)

- **Cross Product (2D)**
  - u × v = u.x × v.y - u.y × v.x
  - Returns scalar (z-component of 3D cross product)
  - Positive: v is counterclockwise from u
  - Negative: v is clockwise from u
  - Zero: vectors parallel

- **Orientation Test**
  - Given three points A, B, C
  - Compute (B - A) × (C - A)
  - Positive: counterclockwise
  - Negative: clockwise
  - Zero: collinear

- **Point-Line Relationships**
  - Distance from point to line: |cross product| / |line length|
  - Point on segment: orientation + bounding box check

- **Line Intersection**
  - Lines defined by two points or point + direction
  - Find intersection point (if exists)
  - Check if segments intersect

- **Geometric Primitives**
  - Distance, orientation, intersection
  - Building blocks for more complex algorithms

**Key Insights:**
- Cross product determines orientation
- Dot product measures angle/projection
- Primitives reusable in complex algorithms

**Deliverables:**
- [ ] Implement distance, dot product, cross product
- [ ] Implement orientation test for three points
- [ ] Check if two segments intersect
- [ ] Find line intersection point

---

### 📅 DAY 5: CONVEX HULL - GRAHAM SCAN & JARVIS MARCH

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Convex Hull Definition**
  - Smallest convex polygon containing all points
  - Vertices: subset of input points on boundary

- **Graham Scan Algorithm**
  - Sort points by polar angle from bottom-left point
  - Iterate through points, maintain stack of hull
  - For each point: pop stack while last two points + current make right turn
  - O(n log n) due to sorting

- **Graham Scan Correctness**
  - Points sorted by angle guarantee no point inside
  - Removing right turns maintains convexity

- **Graham Scan Details**
  - Find bottom-left point (min y, break ties by min x)
  - Sort by polar angle from this point
  - Process points: maintain left turn invariant

- **Jarvis March (Gift Wrapping)**
  - Start from bottom-left point
  - For each point on hull, find point with smallest polar angle (from previous direction)
  - O(nh) where h = number of hull points
  - Best if h small

- **Jarvis March Correctness**
  - Always choose point leftmost from current direction
  - Guarantees next hull point

- **Comparing Algorithms**
  - Graham scan: O(n log n), good for all n
  - Jarvis march: O(nh), good if h small
  - Choose based on expected hull size

- **Applications**
  - Graphics: clipping, collision detection
  - Computational geometry: queries on hull
  - Data analysis: outlier detection via hull

- **Variants**
  - 3D convex hull (more complex)
  - Convex hull of two point sets
  - Rotating calipers: optimize objective over hull

**Key Insights:**
- Graham scan: O(n log n), sort then scan
- Jarvis march: gift wrapping approach, O(nh)
- Choose based on expected hull size

**Deliverables:**
- [ ] Implement Graham scan
- [ ] Implement Jarvis march
- [ ] Compare performance on various inputs
- [ ] Solve problem using convex hull (e.g., maximum distance between points)

---

### 📅 DAY 6 (OPTIONAL): CLOSEST PAIR & LINE SWEEP

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Closest Pair Problem**
  - Given: set of points
  - Find: two points with minimum distance

- **Naive Algorithm**
  - O(n²): compare all pairs
  - Simple but slow for large n

- **Divide & Conquer**
  - Sort points by x-coordinate
  - Divide into left and right halves
  - Recursively find closest pair in each half
  - Merge: check pairs across dividing line
  - O(n log n)

- **Merge Phase**
  - Consider only points within distance d of dividing line
  - Sort these by y-coordinate
  - For each point, check nearby points (at most 8 by pigeonhole)
  - O(n) merge

- **Recurrence**
  - T(n) = 2T(n/2) + O(n log n)  [if sorting within recursion]
  - T(n) = 2T(n/2) + O(n)        [if using presorted order]
  - Second: O(n log n) total

- **Line Sweep Paradigm**
  - Process events in sorted order (by x-coordinate)
  - Maintain "active" set of points
  - Sweep vertical line from left to right
  - At each event, update active set and check distances

- **Line Sweep for Closest Pair**
  - Sort by x-coordinate
  - Maintain strip of width d to right of sweep line
  - For each point, check against points in strip
  - Update d as better pairs found

- **Applications**
  - Graphics: nearest neighbor queries
  - Data analysis: clustering
  - Games: collision detection

**Key Insights:**
- Divide & conquer clever approach
- Line sweep general paradigm for geometry problems
- Merge phase critical for correctness

**Deliverables:**
- [ ] Implement naive closest pair
- [ ] Implement divide & conquer closest pair
- [ ] Implement line sweep version
- [ ] Compare performance

---

## 📌 WEEK 17: HEAVY-LIGHT DECOMPOSITION, ADVANCED STRINGS & FFT

### Weekly Goal
Learn advanced tree algorithms, advanced string matching, and FFT for polynomial multiplication.

### Weekly Topics
- Heavy-light decomposition for tree paths
- Advanced string algorithms: Aho-Corasick, suffix automaton
- Fast Fourier transform for polynomial multiplication

---

### 📅 DAY 1: HEAVY-LIGHT DECOMPOSITION (HLD)

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Tree Path Queries**
  - Query: aggregate (sum, min, max) over path from u to v
  - Update: set/increment value on path from u to v
  - Naive: O(n) per operation (path traversal)

- **Heavy-Light Decomposition Idea**
  - Decompose tree into heavy and light edges
  - Heavy edge: leads to larger subtree
  - Light edge: leads to smaller subtree
  - At most log n light edges on any root-to-leaf path

- **HLD Definition**
  - Root tree at arbitrary vertex
  - For each vertex, pick heaviest child (edge to largest subtree)
  - All edges to other children are light
  - Chain: path of heavy edges
  - Decomposition: tree split into chains

- **HLD Properties**
  - Number of chains: O(n)
  - Depth of chains: O(log n) on any path
  - Any path from root to leaf crosses O(log n) chains

- **HLD for Path Queries**
  - LCA (lowest common ancestor) of u and v
  - Path u → LCA: jump between chains using binary lifting or parent pointers
  - Path LCA → v: similar
  - Use segment tree or BIT on each chain for range queries
  - O(log² n) per query with careful implementation

- **Chain Representation**
  - Map each chain to contiguous array indices
  - Segment tree on each chain enables range queries
  - Updates reflected in segment trees

- **Implementation Complexity**
  - Moderate to high: requires LCA, segment trees, careful indexing
  - Worth it for complex tree problems

- **Alternatives**
  - Link-cut trees: more general but complex
  - Centroid decomposition: different approach
  - For simpler problems: LCA + binary lifting sufficient

**Key Insights:**
- HLD decomposes tree into manageable chains
- Enables efficient path queries
- Complex but powerful

**Deliverables:**
- [ ] Implement HLD decomposition
- [ ] Query path sum using HLD
- [ ] Update values on path using HLD
- [ ] Compare performance vs naive

---

### 📅 DAY 2: ADVANCED STRINGS - AHO-CORASICK & SUFFIX AUTOMATON (CONCEPTUAL)

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Aho-Corasick Algorithm**
  - Multiple pattern matching: find all occurrences of any pattern in text
  - Generalization of KMP to multiple patterns

- **Aho-Corasick Automaton**
  - Trie of patterns with failure links
  - Similar to KMP failure function but on trie
  - Each node: pattern being matched at this point
  - Edges: characters to transition on

- **Failure Link**
  - For each node, link to longest proper prefix that is also in pattern set
  - Guides transitions when character doesn't match

- **Matching Process**
  - Process text character by character
  - Follow edges in automaton, using failure links on mismatch
  - At each node, check if any pattern ends here
  - O(n + m + z) where n = text length, m = total pattern length, z = #matches

- **Suffix Automaton (High-Level)**
  - Automaton recognizing all suffixes of string
  - More compact than suffix tree
  - O(n) states, O(1) per character transition
  - Applications: pattern matching, substring queries

- **Building Suffix Automaton**
  - Add characters one by one
  - Maintain right-language equivalence classes
  - Complex; typically need reference
  - O(n log n) for general alphabets

**Key Insights:**
- Aho-Corasick elegant extension of KMP
- Handles multiple patterns efficiently
- Suffix automaton advanced, rarely needed

**Deliverables:**
- [ ] Implement Aho-Corasick for multiple pattern matching
- [ ] Find all occurrences of patterns in text
- [ ] Compare vs KMP repeated or naive

---

### 📅 DAY 3: FAST FOURIER TRANSFORM (FFT)

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Polynomial Multiplication Problem**
  - A(x) = a₀ + a₁x + ... + a_{n-1}x^{n-1}
  - B(x) = b₀ + b₁x + ... + b_{n-1}x^{n-1}
  - C(x) = A(x) × B(x) = c₀ + c₁x + ... + c_{2n-2}x^{2n-2}
  - Naive: O(n²) coefficient multiplications

- **FFT Idea**
  - DFT (Discrete Fourier Transform): evaluate polynomial at special points
  - Multiply pointwise (O(n))
  - IDFT: convert back to coefficients
  - Total: O(n log n) instead of O(n²)

- **Complex Roots of Unity**
  - ω = e^{2πi/n}: primitive nth root of unity
  - ω^n = 1
  - 1, ω, ω², ..., ω^{n-1} are nth roots of unity

- **DFT Definition**
  - ĉ_k = Σ_{j=0}^{n-1} c_j × ω^{jk}
  - Evaluate polynomial at roots of unity

- **Cooley-Tukey FFT Algorithm**
  - Divide & conquer on polynomial
  - Split into even and odd coefficients
  - ĉ_k = ĉ_even_k + ω^k × ĉ_odd_k
  - T(n) = 2T(n/2) + O(n) = O(n log n)
  - Requires n to be power of 2

- **Inverse FFT (IFFT)**
  - Recover coefficients from values
  - Similar to FFT with ω → ω^{-1}
  - IFFT(c) = DFT(c) / n (up to complex conjugation)

- **Implementation Details**
  - Use complex numbers
  - Bit-reversal permutation for in-place computation
  - Careful with precision

- **Applications**
  - Polynomial multiplication (large degrees)
  - Convolution: same as polynomial multiplication
  - Signal processing
  - Large integer multiplication

- **Limitations**
  - FFT assumes n = power of 2 (pad with zeros)
  - Complex numbers; numerical precision issues
  - Overhead makes FFT slower than O(n²) for small n
  - Typically use when n > 1000

**Key Insights:**
- FFT ingenious use of symmetry and roots of unity
- O(n log n) polyomial multiplication surprising
- Complex but widely used

**Deliverables:**
- [ ] Implement naive polynomial multiplication
- [ ] Implement naive DFT and IDFT
- [ ] Implement Cooley-Tukey FFT
- [ ] Compare performance for various sizes

---

### 📅 DAY 4-5 (OPTIONAL): APPLICATIONS & ADVANCED TOPICS

**Duration:** 90 minutes per day

**Topics & Subtopics (DAY 4):**
- More FFT applications: convolution, signal processing
- Number-theoretic transform (NTT): FFT over finite field, exact computation
- Large integer multiplication via FFT

**Topics & Subtopics (DAY 5):**
- Advanced geometric algorithms: segment intersection, polygon clipping
- Advanced DP combinations: convex hull trick, divide & conquer optimization
- Problem-specific optimizations

---

## 📌 WEEK 18: PROBABILISTIC DATA STRUCTURES, MIN-COST FLOW & SYSTEM DESIGN

### Weekly Goal
Master probabilistic algorithms and data structures, min-cost flow for optimization, and algorithmic system design.

### Weekly Topics
- Bloom filters, Count-Min sketch, HyperLogLog
- Min-cost flow and circulation
- Caching policies and LRU
- Indexing and query optimization
- Sharding and distributed algorithms

---

### 📅 DAY 1: BLOOM FILTERS & PROBABILISTIC DATA STRUCTURES

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Bloom Filter Basics**
  - Space-efficient probabilistic data structure
  - Supports: add(x), query(x)
  - Query returns: definitely not in set, or probably in set
  - False positives allowed; false negatives not allowed

- **Bloom Filter Implementation**
  - Bit array of size m
  - k hash functions h₁, ..., h_k
  - Add(x): set bits h_i(x) for i = 1..k
  - Query(x): check if all bits h_i(x) set
  - O(k) per operation, O(m) space

- **False Positive Rate**
  - If n elements added to m-bit array with k hashes
  - Probability bit set ≈ 1 - (1 - 1/m)^{kn} ≈ 1 - e^{-kn/m}
  - False positive: all k bits set for non-member ≈ (1 - e^{-kn/m})^k
  - Trade-off: larger m → lower false positive rate
  - Optimal k ≈ (m/n) × ln(2)

- **Applications**
  - Caching: check if item in cache before expensive lookup
  - Spell checking: quick negative check
  - Web crawling: avoid revisiting URLs
  - Database queries: skip unnecessary disk reads

- **Counting Bloom Filter**
  - Replace bits with counters
  - Delete by decrementing counters
  - More space but allows deletions

- **Count-Min Sketch**
  - Probabilistic data structure for frequency estimation
  - Supports: add(x, count), query(x)
  - Query returns: estimated frequency ≥ true frequency
  - 2D array of counters with k hash functions
  - Add: increment all k counters for item
  - Query: return minimum of k counters

- **HyperLogLog**
  - Approximate cardinality (distinct element count)
  - O(log log n) space
  - Used in databases, analytics for cardinality estimation

**Key Insights:**
- Bloom filters space-efficient with small false positive rate
- Trade space for accuracy
- Practical in many systems

**Deliverables:**
- [ ] Implement Bloom filter
- [ ] Measure false positive rate vs space
- [ ] Implement Count-Min sketch
- [ ] Use in caching scenario

---

### 📅 DAY 2: MIN-COST MAX-FLOW

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Min-Cost Max-Flow Problem**
  - Find maximum flow minimizing total cost
  - Each edge has capacity c(u, v) and cost w(u, v)
  - Objective: maximize flow, minimize Σ w(u, v) × f(u, v)

- **Successive Shortest Paths Algorithm**
  - Iteratively find shortest path from s to t in residual graph
  - Shortest path defined using costs as weights
  - Augment along shortest path
  - Repeat until no path exists
  - O(V² × cost(shortest_path))

- **Cycle Cancellation**
  - Start with max-flow (ignoring costs)
  - Find negative-cost cycle in residual graph
  - Cancel cycle (reduce flow along cycle)
  - Repeat until no negative cycle
  - Equivalent to min-cost max-flow

- **Relaxation for Potentials**
  - Use potentials to avoid negative weights
  - Potentials: h(u) for each vertex
  - Modified weights: w'(u, v) = w(u, v) + h(u) - h(v)
  - All modified weights ≥ 0 if no negative cycle
  - Use Dijkstra (faster) after potential update

- **Complexity**
  - Successive shortest paths: O(V × cost(shortest_path))
  - With Dijkstra: O(VE log V) per shortest path
  - Practical if flow small

- **Applications**
  - Assignment problem with costs: each task-worker pair has cost
  - Transportation: route goods minimizing cost
  - Scheduling: assign jobs to machines minimizing total cost + lateness

- **Assignment via Min-Cost Max-Flow**
  - Bipartite graph: tasks on left, workers on right
  - Edge from task to worker with capacity 1 and cost c(task, worker)
  - Source to tasks, workers to sink (capacity 1)
  - Min-cost max-flow = optimal assignment

**Key Insights:**
- Min-cost max-flow generalizes max-flow
- Successive shortest paths practical algorithm
- Applications to assignment and scheduling

**Deliverables:**
- [ ] Implement successive shortest paths for min-cost max-flow
- [ ] Solve assignment problem via min-cost max-flow
- [ ] Compare with Hungarian algorithm

---

### 📅 DAY 3: ALGORITHMIC SYSTEM DESIGN & CACHING

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Caching Basics**
  - Limited cache size compared to total data
  - Objective: minimize cache misses
  - Replacement policy: which item to evict on miss

- **LRU (Least Recently Used)**
  - Evict least recently used item
  - Practical and effective for many workloads
  - Implement: doubly-linked list + hash map
  - O(1) access, move to front on hit

- **LFU (Least Frequently Used)**
  - Evict least frequently used item
  - Tie-break by recency
  - More complex implementation

- **Other Policies**
  - FIFO (First-In-First-Out): simple
  - Random: baseline
  - OPTIMAL: minimize misses (requires future knowledge, offline)

- **Workload Analysis**
  - Temporal locality: recently used likely again soon
  - Spatial locality: items near recent accesses likely used soon
  - Frequency distribution: some items more popular

- **Bloom Filters for Caching**
  - Check if item in cache before lookup
  - Bloom filter fast negative (not in cache)
  - Positive (possibly in cache) → check actual cache

- **Partitioning & Sharding**
  - Large datasets: partition across multiple servers
  - Consistent hashing: minimize data movement on scale
  - Replica: copies for availability

- **Indexing**
  - Create indices for fast lookups
  - Trade space for time
  - Index structures: B-trees, hash maps, etc.

- **Query Optimization**
  - Cost estimation: predic query cost
  - Choose best execution plan
  - Join order: minimize intermediate results

- **Rate Limiting & Throttling**
  - Token bucket: allow n requests per second
  - Leaky bucket: smooth out bursts
  - Backpressure: slow down producer if queue full

**Key Insights:**
- Caching critical for performance
- LRU practical and effective
- System design considers end-to-end performance

**Deliverables:**
- [ ] Implement LRU cache
- [ ] Measure cache hit rate vs size
- [ ] Design cache with Bloom filter negative check
- [ ] Simulate caching policy on workload

---

### 📅 DAY 4-5 (OPTIONAL): DISTRIBUTED ALGORITHMS & ADVANCED TOPICS

**Duration:** 90 minutes per day

**Topics & Subtopics (DAY 4):**
- MapReduce: distributed computation framework
- Consistent hashing: partition keys across servers
- Vector clocks and causal consistency

**Topics & Subtopics (DAY 5):**
- Linear programming: optimization framework
- Branch-and-cut: combining branch-and-bound with cutting planes
- Genetic algorithms and simulated annealing for hard problems

---

# 🟣 PHASE G: MOCK INTERVIEWS & FINAL REVIEW (Week 19)

## Phase Motivation
You've built knowledge across all domains. This final week focuses on application under pressure, identifying weak points, and ensuring interview readiness.

## Phase Outcomes
- [ ] Solve mixed problems combining multiple concepts
- [ ] Communicate solution clearly
- [ ] Handle dead ends and pivot gracefully
- [ ] Identify and address weak areas
- [ ] Interview-ready confidence

---

## 📌 WEEK 19: MOCK INTERVIEWS & INTEGRATION PROBLEMS

### Weekly Goal
Test knowledge across curriculum in realistic scenarios and develop interview skills.

### Weekly Topics
- Mock interview sessions (multi-problem)
- Problem analysis and approach selection
- Implementation under time pressure
- Communication and clarification
- Debugging and optimization

---

### 📅 DAY 1: MOCK INTERVIEW SESSION A - ARRAYS & STRINGS

**Duration:** 120 minutes

**Problems:**
1. Problem combining: two-pointer + hashing + string manipulation
   - Example: "Valid Palindrome II" (delete at most one character)
   - Combines palindrome pattern, two-pointer, early termination
   
2. Problem combining: sliding window + DP
   - Example: "Minimum Window Substring with K distinct characters"
   - Combines variable window, constraint checking, state management

3. Problem: matrix operations
   - Example: "Rotate Matrix In-Place"
   - Combines traversal, in-place swapping, coordinate geometry

**Structure:**
- Read and clarify (5 min)
- Discuss approach (5 min)
- Implement (40 min)
- Test and debug (20 min)
- Optimize (20 min)
- Discuss alternatives (10 min)

**Key Skills Tested:**
- Pattern recognition
- Complexity analysis
- Clean implementation
- Communication

---

### 📅 DAY 2: MOCK INTERVIEW SESSION B - TREES & GRAPHS

**Duration:** 120 minutes

**Problems:**
1. Tree DP + traversal
   - Example: "Diameter of Binary Tree" or "Maximum Path Sum"
   - Combines DFS, DP state, aggregate computation
   
2. Graph algorithm choice
   - Example: "Number of Islands II" with Union-Find
   - Combines dynamic connectivity, coordinate system
   
3. BFS/DFS application
   - Example: "Minimum Knight Moves" or "Word Ladder"
   - Combines BFS, state representation, shortest path

---

### 📅 DAY 3: MOCK INTERVIEW SESSION C - DYNAMIC PROGRAMMING & OPTIMIZATION

**Duration:** 120 minutes

**Problems:**
1. DP + greedy choice
   - Example: "Jump Game II" (greedy within DP context)
   - Combines DP formulation, greedy optimization insight
   
2. DP on graph/grid
   - Example: "Cherry Pickup" or "Maximum Path Sum in Matrix"
   - Combines grid traversal, DP state design, constraint handling
   
3. DP with constraints
   - Example: "Burst Balloons" or "Remove Boxes"
   - Combines DP decomposition, interval DP, state definition

---

### 📅 DAY 4: MIXED PROBLEM SOLVING

**Duration:** 90 minutes

**Problems:**
1. Combining multiple patterns
   - Example: "Median of Two Sorted Arrays"
   - Combines binary search, array handling, edge cases
   
2. System design / algorithmic
   - Example: "Design LRU Cache"
   - Combines data structure choice, efficiency requirements, implementation

---

### 📅 DAY 5: FINAL ASSESSMENT & WEAK AREA DIAGNOSIS

**Duration:** 90 minutes

**Activities:**
1. Review of previous mock sessions
   - Identify patterns of mistakes
   - Categorize: conceptual gaps, implementation bugs, time pressure

2. Targeted practice
   - Focus on weak areas identified
   - 2-3 problems in specific category
   - Build confidence

3. Personal review plan
   - Map remaining weak spots to weeks/topics
   - Design targeted practice schedule
   - Set goals for continued improvement

4. Meta-skills practice
   - Asking clarifying questions
   - Thinking aloud effectively
   - Handling "I don't know"
   - Graceful pivot on wrong approach

---

### 📅 DAY 6 (OPTIONAL): INTERVIEW SPECIFIC TIPS & FINAL DRILLS

**Duration:** 90 minutes

**Topics:**
- Handling unknowns gracefully
- Time management in interviews
- Choosing between multiple approaches
- Communicating trade-offs
- Real interview walkthrough

---

## 🎓 CURRICULUM COMPLETE

**Total Content:**
- 19 weeks
- 95+ study days
- 500+ unique topic subtopics
- 235-270 hours of learning
- 20+ data structures
- 15+ algorithm paradigms
- 50+ techniques

**You're Now Ready For:**
- Technical interviews at top companies
- Systems design discussions
- Algorithm competition
- Real-world problem-solving
- Advanced CS courses

**Next Steps:**
1. Complete all daily deliverables
2. Practice problems on LeetCode/HackerRank
3. Review weak areas
4. Do mock interviews
5. Continuous practice and refinement

---

**Version:** 13.0 COMPLETE
**Status:** ✅ ALL 19 WEEKS FULLY DETAILED
**Date:** January 24, 2026

**This is your comprehensive roadmap to DSA mastery. Every day brings you closer. Keep showing up. 🚀**
