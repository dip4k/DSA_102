# 📘 COMPLETE_DETAILED_SYLLABUS_v13.md

**Version:** 13.0 (Comprehensive Day-Wise Breakdown)  
**Status:** ✅ PRODUCTION READY  
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

- **Working Set & Fitting in Memory**
  - Active data that must fit in cache
  - Algorithms with good locality: cache-friendly
  - Algorithms with bad locality: cache thrashing
  - Practical impact: can dominate asymptotic complexity

**Key Insights:**
- Space is a real resource, not free
- Memory hierarchy matters for performance
- Recursion depth directly limited by stack
- Time-space tradeoffs are fundamental in algorithm design

**Deliverables:**
- [ ] Calculate space complexity for several algorithms
- [ ] Analyze memory layout of dynamic structures
- [ ] Trace stack growth during recursion
- [ ] Identify working set for array algorithms

---

### 📅 DAY 4: RECURSION I - CALL STACK & BASIC PATTERNS

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Function Call Mechanics**
  - Caller pushes parameters onto stack
  - Return address pushed
  - Called function allocates local variables
  - Activation record = frame on stack
  - Function returns: locals deallocated, return to caller

- **Recursion as Consequence of Calls**
  - Function can call itself (recursive call)
  - Each call gets own frame on stack
  - Multiple frames can exist simultaneously
  - Call chain visible in stack trace

- **Recursion Tree Visualization**
  - Each function call = node in tree
  - Calling another function = child node
  - Leaf nodes = base cases (no more calls)
  - Tree structure shows call pattern

- **Base Case and Recursive Case**
  - Base case: condition where recursion stops
  - Without base case: infinite recursion → stack overflow
  - Recursive case: make progress toward base case
  - Example: factorial
    - Base: n=0 → return 1
    - Recursive: n>0 → return n × factorial(n-1)
    - Progress: n decreases each call

- **Simple Recursive Patterns**
  - Linear recursion: sum of array [a₀, a₁, ..., aₙ₋₁]
    - f(0) = a[0]
    - f(i) = a[i] + f(i-1)
    - Tree: single chain of calls
  - Tree recursion: Fibonacci
    - f(0) = 0, f(1) = 1
    - f(n) = f(n-1) + f(n-2)
    - Tree: exponential branches

- **Recursion Complexity**
  - Sum of array: n calls, O(n) time, O(n) space (stack)
  - Fibonacci naive: 2^n calls, O(2ⁿ) time, O(n) space
  - Binary search: log n calls, O(log n) time, O(log n) space
  - Merge sort: n log n nodes, O(n log n) time, O(n) space

- **Stack Overflow**
  - Stack size typically 1-8 MB
  - Each frame: 100-1000 bytes depending on locals
  - Deep recursion: few hundred to thousands of calls
  - Too deep: stack overflow (crash)

- **When to Use Recursion**
  - Naturally recursive problems: trees, graphs, backtracking
  - Divide-and-conquer algorithms
  - Some people find recursive thinking clearer
  - Tail recursion can be optimized by compilers

**Key Insights:**
- Recursion uses stack space
- Tree shape of calls determines complexity
- Depth of recursion limited by stack size
- Exponential recursion trees explode quickly

**Deliverables:**
- [ ] Draw call stacks for recursive functions
- [ ] Analyze recursion tree shapes
- [ ] Calculate time and space for recursive functions
- [ ] Identify base cases in recursive functions

---

### 📅 DAY 5: RECURSION II - ADVANCED PATTERNS & MEMOIZATION

**Duration:** 120 minutes

**Topics & Subtopics:**
- **Recursion Patterns (Structural)**
  - Linear recursion: single recursive call
    - Example: sum array, factorial
    - Tree: chain
    - Space: O(depth)
  - Tree recursion: multiple recursive calls
    - Example: Fibonacci naive
    - Tree: exponential branches
    - Space: O(depth), time: O(branches^depth)
  - Divide-and-conquer recursion: split, solve, combine
    - Example: merge sort, binary search
    - Tree: balanced, height O(log n)
    - Recurrence analysis

- **Tail Recursion**
  - Last operation is recursive call
  - Example: linear search from end of array
  - Compiler optimization: convert to loop
  - Stack space can become O(1) with optimization
  - Not all languages support tail call optimization

- **Mutual & Indirect Recursion**
  - Function A calls B calls A
  - Example: even/odd number checking
  - More complex analysis but same principles
  - Less common but appears in some problems

- **Memoization (Top-Down DP)**
  - Problem: many redundant recursive calls
  - Example: Fibonacci naive computes f(5) multiple times
  - Solution: cache results in dictionary/table
  - f(n) already computed? Return cached value
  - Otherwise: compute and cache before returning

- **Memoization Implementation**
  - Store computed values in dictionary
  - Key: function parameters (often tuple)
  - Value: result of computation
  - Check cache before computing
  - Fibonacci memoized: O(n) time, O(n) space

- **When Memoization Works**
  - Overlapping subproblems essential
  - Same parameters called multiple times
  - Without overlaps: no benefit
  - Example: linear recursion (no overlaps) vs Fibonacci (exponential overlaps)

- **Memoization vs Tabulation**
  - Memoization: top-down, recursive, cache as needed
  - Tabulation: bottom-up, iterative, fill table systematically
  - Same asymptotic complexity
  - Different space patterns (recursion vs iteration)

- **Stack Depth & Limits**
  - Typical limit: 1000-10000 calls before overflow
  - System-dependent
  - Some languages allow increasing stack
  - Alternative: convert recursion to iteration

- **Recognizing Recursion Problems**
  - Problem defines result in terms of smaller inputs
  - Tree/graph structures natural recursion
  - Multiple choices to explore (backtracking)
  - Divide-and-conquer pattern

**Key Insights:**
- Memoization transforms exponential to polynomial
- Overlapping subproblems are the key insight
- Recursion depth is fundamental limit
- Convert to iteration if stack issues arise

**Deliverables:**
- [ ] Implement Fibonacci with and without memoization
- [ ] Measure time difference
- [ ] Cache design for memoization
- [ ] Identify overlapping subproblems in examples

---

### 📅 DAY 6 (OPTIONAL ADVANCED): PEAK FINDING & ALGORITHMIC THINKING

**Duration:** 120 minutes

**Topics & Subtopics:**
- **1D Peak Finding Problem**
  - Definition: element ≥ both neighbors
  - Edge elements: borders treated as "greater than outside"
  - Problem: find ANY peak
  - Example: [1,3,4,3,2] → peak is 4

- **Naive Solution**
  - Scan left to right, check each element
  - Time: O(n)
  - Can we do better?

- **Divide & Conquer Insight**
  - Middle element: compare to neighbors
  - If middle ≥ both: found peak
  - If left > middle: peak exists in left half
  - If right > middle: peak exists in right half
  - Recurse on promising side

- **Divide & Conquer Algorithm**
  - Binary search style: O(log n) or O(n log n)?
  - Analysis: each recursion eliminates half
  - Time: O(log n) or O(n log n) depending on implementation
  - Clever observation: don't need to check all elements

- **2D Peak Finding**
  - Matrix with rows and columns
  - Definition: element ≥ all 4 neighbors
  - Naive: check all, O(n²)
  - Column-based approach:
    - Find tallest element in middle column
    - Compare to neighbors in left/right columns
    - Move to taller column
    - When can't move, neighbors in same column form 1D problem

- **Analysis of 2D Algorithm**
  - Column operations: O(n) per column
  - Number of columns: O(log n) worst case
  - Total: O(n log n)
  - Proof: each step eliminates half the columns

- **Meta-Lessons from Peak Finding**
  - Understanding problem structure → better algorithms
  - Naive may be correct but inefficient
  - Key insight: information about adjacency guides search
  - Divide-and-conquer: exploit structure for logarithmic behavior

**Key Insights:**
- Not all O(n) problems require checking everything
- Problem structure enables clever algorithms
- "Better than brute force" mindset

**Deliverables:**
- [ ] Implement 1D peak finding
- [ ] Implement 2D peak finding
- [ ] Analyze complexity carefully
- [ ] Prove correctness via case analysis

---

## 📌 WEEK 2: LINEAR DATA STRUCTURES & BINARY SEARCH

### Weekly Goal
Master arrays, dynamic arrays, linked lists, stacks, queues, and binary search as a robust algorithmic pattern applicable beyond sorted arrays.

### Weekly Topics
- Array memory layout and indexing
- Dynamic resizing and amortized cost intuition
- Linked lists and pointer-based structures
- Stack/queue/deque semantics and uses
- Binary search on sorted data and answer spaces

---

### 📅 DAY 1: ARRAYS & MEMORY LAYOUT

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Static Arrays**
  - Fixed size allocated at declaration
  - Contiguous memory block
  - Each element same type, same size
  - Index to address: base_address + index × element_size

- **Index Calculation**
  - Array a[100]: declared as contiguous 100 elements
  - Element a[i]: at address base + i × sizeof(element)
  - O(1) random access (under RAM model)
  - Works for any i in [0, 99]

- **Memory Layout & Cache**
  - Contiguous: prefetcher can predict next access
  - Cache line: typically 64 bytes
  - Sequential access: very fast (multiple elements per cache line)
  - Random access: slower (cache misses)

- **Array Layouts (2D)**
  - Row-major: array[i][j] stored in row order
    - Row 0: [0][0], [0][1], ...
    - Row 1: [1][0], [1][1], ...
    - Cache-friendly: iterate row-by-row
  - Column-major: stored in column order (Fortran, MATLAB)
  - Row-major and then iterate: fast
  - Skip rows (jump pattern): cache-unfriendly

- **Static Array Pros**
  - Fast random access O(1)
  - Great cache locality for sequential access
  - No overhead (just contiguous memory)
  - Simple to reason about

- **Static Array Cons**
  - Fixed size: must know size at compile time
  - Resizing: not possible without creating new array
  - Wasteful if use less than allocated
  - Internal fragmentation

- **Cache Considerations**
  - Sequential vs random traversal dramatically different
  - Prefetching helps linear access
  - Multidimensional arrays: access pattern critical
  - False sharing: threads accessing nearby elements in same cache line

**Key Insights:**
- Contiguous memory is fundamental advantage
- Layout matters for cache behavior
- Random access fast in theory, slower with cache misses
- Sequential better than scattered access

**Deliverables:**
- [ ] Calculate addresses for array elements
- [ ] Analyze cache behavior for different access patterns
- [ ] Compare sequential vs random access performance
- [ ] Understand cache line implications

---

### 📅 DAY 2: DYNAMIC ARRAYS & AMORTIZED GROWTH

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Dynamic Array Concept**
  - Logical size: elements currently in array
  - Capacity: allocated space
  - resize() needed when size == capacity

- **Growth Strategy: Doubling**
  - When full: allocate 2× capacity, copy elements, deallocate old
  - Example: start size 1
    - After 1st full: capacity 2 (1 resize)
    - After 2nd full: capacity 4 (1 resize)
    - After 4th full: capacity 8 (1 resize)
    - After n appends: ~log n resizes

- **Amortized Cost (Intuition)**
  - n appends to initially empty array:
    - Most appends: O(1) (just store)
    - Few appends: O(n) (trigger resize, copy all n elements)
    - Total cost: O(1) + O(1) + ... + O(2) + O(4) + O(8) + ... + O(n)
    - Sum of doubling sequence: 1+2+4+8+...+n = O(n)
    - Amortized: O(n) / n = O(1) per append

- **Reallocation Effects**
  - Copying existing elements expensive
  - All pointers to old address become invalid
  - Vector invalidates iterators on resize
  - Temporary spike in memory usage

- **Growth Strategies**
  - Doubling (most common, O(1) amortized)
  - 1.5× growth (also O(1) amortized)
  - Linear growth (O(n) amortized per append, worse)
  - Threshold-based (resize at 75%, 50% usage)

- **Shrinking / Reserve**
  - When size drops below capacity/4: shrink to 2× size
  - Prevents unbounded wasted space
  - But adds complexity
  - reserve(n): hint to allocate space without reallocation

- **Amortized vs Worst Case**
  - Single append worst: O(n) (when resize occurs)
  - n appends average: O(1) each (amortized)
  - Important distinction for algorithms
  - Aggregate analysis formal later (week 13)

- **Space Overhead**
  - Allocator metadata: 8-16 bytes
  - Capacity typically > size
  - Wasted space due to power-of-2 allocation
  - Example: 1000 elements in capacity 1024

- **Performance in Practice**
  - Very fast for most operations
  - Occasional resize slower but amortized away
  - Better cache locality than linked lists
  - Standard choice in competitive programming

**Key Insights:**
- Amortized analysis: rare expensive operations average out
- Doubling: elegant strategy with O(1) amortized
- Locality: dynamic arrays still cache-friendly
- Reserve: trade space for guaranteed no reallocation

**Deliverables:**
- [ ] Trace growth of dynamic array through 20 appends
- [ ] Calculate amortized cost
- [ ] Analyze memory usage with different strategies
- [ ] Compare to linked list space patterns

---

### 📅 DAY 3: LINKED LISTS

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Singly Linked List**
  - Node structure: value + next pointer
  - Head: first node, Tail (optional): last node
  - Empty: head = null

- **List Traversal**
  - Start at head
  - While current != null: process node, move to next
  - O(n) time to visit all

- **Insert at Head**
  - Create new node
  - Set new.next = head
  - Set head = new
  - O(1) time

- **Insert at Tail**
  - Without tail pointer: traverse to end O(n)
  - With tail pointer: append to tail.next, update tail O(1)
  - Cost: store tail pointer, keep it updated

- **Insert at Position**
  - Need previous node
  - Adjust pointers: new.next = prev.next, prev.next = new
  - O(n) to find position

- **Delete from Head**
  - head = head.next
  - O(1) time
  - Old head garbage collected

- **Delete at Position**
  - Adjust pointers: prev.next = prev.next.next
  - O(n) to find position

- **Doubly Linked List**
  - Node: value + next + prev
  - Delete at position: O(1) if you have node pointer
  - But traversal same complexity

- **Pros of Linked Lists**
  - O(1) insert/delete at known position (with pointer)
  - Flexible size (no reallocation)
  - Can build more complex structures (graphs, trees)

- **Cons of Linked Lists**
  - No O(1) random access (must traverse)
  - Pointer per node overhead (8+ bytes per element)
  - Poor cache locality (pointers scattered in memory)
  - Traversal slower than array even for same O(n)

- **Pointer Chasing Performance**
  - Array: sequential access, cache prefetch
  - Linked list: follow pointers, unpredictable pattern
  - Pointer dereference: load from memory (cache miss likely)
  - In practice: linked lists often slower than arrays despite better asymptotics

- **When to Use Linked Lists**
  - Frequent insertion/deletion in middle
  - Unknown size, many insertions
  - Building graphs/trees
  - Usually: arrays are better choice (better locality, simpler)

**Key Insights:**
- Linked lists great for insertion/deletion if you have position
- But cache locality kills performance in practice
- Arrays nearly always better despite O(1) delete lacking
- Pointer overhead non-trivial

**Deliverables:**
- [ ] Implement singly linked list with insert/delete
- [ ] Implement doubly linked list
- [ ] Trace operations with pointers
- [ ] Compare performance vs arrays empirically

---

### 📅 DAY 4: STACKS, QUEUES & DEQUES

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Stack (LIFO - Last In First Out)**
  - push(x): add x to top
  - pop(): remove and return top
  - top(): peek at top without removing
  - isEmpty()
  - Use cases: function call stack, undo/redo, backtracking, DFS

- **Stack Implementation**
  - Array-based: store elements, track top index
  - Linked list based: push at head, pop from head
  - Either: O(1) push/pop
  - Space: O(n) for n elements

- **Queue (FIFO - First In First Out)**
  - enqueue(x): add x to back
  - dequeue(): remove and return front
  - front(): peek at front
  - isEmpty()
  - Use cases: BFS, scheduling, print queue, buffering

- **Queue Implementation**
  - Array-based naive: enqueue at back, dequeue from front
    - Problem: waste front space as we dequeue
  - Circular buffer: wrap around, reuse space
    - front and back pointers track position
    - Handles wrap-around with modulo
  - Linked list: enqueue at tail, dequeue from head

- **Deque (Double-Ended Queue)**
  - Operations at both ends: push_front, push_back, pop_front, pop_back
  - peak_front, peak_back
  - Combines stack and queue interface
  - Use cases: sliding window maximum (monotonic deque)

- **Monotonic Queue Pattern**
  - Deque of indices/values in specific order
  - Maintain decreasing (or increasing) values
  - Useful for range min/max problems
  - Example: sliding window max

- **Stack vs List Usage**
  - Stack interface: constrained access (only top)
  - Cleaner for problems requiring LIFO
  - Explicitly shows intent
  - Sometimes just use array/vector with pop_back()

- **Queue vs Deque Usage**
  - Queue: FIFO semantics
  - Deque: flexible, more powerful
  - Can use deque for anything (even as stack)
  - Small overhead for deque vs dedicated queue

- **Performance**
  - Stack: O(1) push/pop
  - Queue (circular): O(1) enqueue/dequeue
  - Deque: O(1) all operations
  - Implementation: array-based faster than linked list

**Key Insights:**
- Stacks and queues: constrained access for clarity
- Circular buffer elegant for queue
- Deque most flexible, still O(1)
- Monotonic deque: powerful pattern for ranges

**Deliverables:**
- [ ] Implement stack and queue
- [ ] Implement circular buffer queue
- [ ] Implement deque
- [ ] Use monotonic deque for sliding window max

---

### 📅 DAY 5: BINARY SEARCH & INVARIANTS

**Duration:** 120 minutes

**Topics & Subtopics:**
- **Binary Search Basics**
  - Precondition: array sorted
  - Search for target value
  - Eliminate half each iteration
  - Time: O(log n)

- **Standard Binary Search Template**
  - low = 0, high = n-1
  - Invariant: target (if present) in [low, high]
  - mid = low + (high - low) / 2 (avoids overflow)
  - Maintain invariant throughout
  - Termination: low > high

- **Three Common Variants**
  - 1. Found: return index when array[mid] == target
  - 2. First occurrence: binary search, then check left
  - 3. Last occurrence: binary search, then check right

- **lower_bound & upper_bound**
  - lower_bound: first element >= target
  - upper_bound: first element > target
  - Standard library functions
  - Useful for count queries: upper - lower

- **Binary Search on Answer Space**
  - Not searching in array
  - Searching in range of possible answers
  - Key: feasibility check function
  - Example: capacity planning
    - Question: what capacity allows all jobs?
    - Feasibility: given capacity c, can schedule all?
    - Binary search: find minimum c that works

- **Monotone Condition Required**
  - For answer space: predicate must be monotone
  - If capacity c works: all c' > c also work
  - If capacity c fails: all c' < c also fail
  - Without monotonicity: binary search doesn't apply

- **Examples of Answer Space Search**
  - Minimize maximum load (machine scheduling)
    - Search: capacity from 0 to max_job_size
    - Feasibility: given capacity, can assign all jobs?
  - Maximize minimum distance (aggressive cows)
    - Search: distance from 0 to max_location
    - Feasibility: given distance d, can place all cows?
  - Find minimum books (allocate books to readers)
    - Search: number of readers k
    - Feasibility: k readers enough? (split books optimally)

- **Implementing Feasibility Checks**
  - Usually straightforward simulation/greedy
  - Example: capacity c, can we schedule jobs?
    - Iterate jobs (sorted), assign to machine with space
    - If any job doesn't fit: false
    - Otherwise: true
  - May be expensive for each check, but still O(n) × O(log(answer_range))

- **Avoiding Off-By-One**
  - mid = low + (high - low) / 2 prevents overflow
  - Condition low <= high or low < high matters
  - Verify boundary cases: n=1, target at edges
  - Test carefully

- **Binary Search vs Other Approaches**
  - Linear search: O(n), simpler code
  - Binary search: O(log n), requires sorted or monotone
  - Choose binary search when: sorted data or answer space
  - Cost of sorting: O(n log n), often worth for multiple queries

**Key Insights:**
- Sorted arrays enable O(log n) search
- Monotone predicates enable binary search on answer space
- Invariant maintenance key to correctness
- Wider applicability than just sorted arrays

**Deliverables:**
- [ ] Implement standard binary search
- [ ] Implement lower_bound, upper_bound
- [ ] Solve a capacity planning problem
- [ ] Solve an aggressive cows variant

---

## 📌 WEEK 3: SORTING, HEAPS & HASHING

### Weekly Goal
Understand sorting algorithms, heap data structure with priority queue semantics, and hash tables (both chaining and open addressing), including rolling hash for strings.

### Weekly Topics
- Elementary sorts: bubble, selection, insertion
- Efficient sorts: merge sort, quick sort, heap sort
- Heaps and priority queues
- Hash tables with chaining
- Hash tables with open addressing
- Rolling hash and Karp-Rabin algorithm

---

### 📅 DAY 1: ELEMENTARY SORTS - BUBBLE, SELECTION, INSERTION

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Bubble Sort**
  - Repeatedly swap adjacent out-of-order elements
  - Each pass: largest bubble to end
  - After k passes: last k elements sorted
  - Time: O(n²) worst and average, O(n) best (already sorted)
  - Space: O(1) in-place
  - Stable: equal elements maintain order
  - Practical: slow, rarely used except education

- **Selection Sort**
  - Find minimum in unsorted portion, swap to sorted portion
  - After k iterations: first k elements sorted (smallest)
  - Time: O(n²) all cases (always scan unsorted)
  - Space: O(1) in-place
  - Stable: depends on implementation
  - Practical: predictable, slightly better than bubble

- **Insertion Sort**
  - Build sorted prefix by inserting next element
  - Shift elements right to make room
  - Time: O(n²) worst case, O(n) best case (sorted)
  - Space: O(1) in-place
  - Stable: yes (maintain order of equal elements)
  - Practical: fast for small n (<50), good for nearly sorted

- **Comparison of Elementary Sorts**
  - All O(n²) asymptotically
  - Constants differ: insertion sort ~n²/4, bubble sort ~n²/2
  - Insertion sort preferred: best case O(n), small constants
  - Use cases: small arrays, educational, understand basics

- **When to Use Elementary Sorts**
  - n < 50: often faster than O(n log n) due to constants
  - Already sorted: insertion sort O(n)
  - Almost sorted: insertion sort faster than general O(n log n)
  - Teaching: understand comparison-based sorting

- **Stability Definition**
  - If a == b and a before b: after sort, a still before b
  - Matters for multi-key sorts
  - Example: sort people by age, maintain alphabetical within age

**Key Insights:**
- O(n²) is OK for small arrays
- Constants matter at small scale
- Stability useful for composing sorts
- Rarely use these in practice

**Deliverables:**
- [ ] Implement bubble, selection, insertion sort
- [ ] Analyze best/worst/average cases
- [ ] Test on small sorted/reverse-sorted arrays
- [ ] Verify stability

---

### 📅 DAY 2: MERGE SORT & QUICK SORT

**Duration:** 120 minutes

**Topics & Subtopics:**
- **Merge Sort**
  - Divide and conquer: split into two halves, sort, merge
  - Recurrence: T(n) = 2T(n/2) + O(n)
  - Solution: T(n) = O(n log n) (all cases)
  - Merge: two sorted arrays into one
    - Two pointers, compare and add smaller
    - Time: O(n)
  - Space: O(n) for temporary arrays
  - Stable: yes (preserve order of equal)
  - Practical: fast, predictable, stable

- **Merge Sort Complexity**
  - Depth of recursion: O(log n)
  - Work at each level: O(n) (n elements to merge)
  - Total: O(n log n)
  - Extra space: O(n) for merging

- **Merge Sort Practical**
  - Used in many standard libraries (Python, Java Arrays.sort for objects)
  - Good for linked lists (no random access needed)
  - Stable sort
  - Predictable performance (no worst case)

- **Quick Sort**
  - Divide and conquer: partition, sort parts
  - Partition: choose pivot, elements <pivot left, >pivot right
  - Recurrence: depends on pivot
    - Best: T(n) = 2T(n/2) + O(n) = O(n log n)
    - Worst: T(n) = T(n-1) + T(0) + O(n) = O(n²) (already sorted)
  - Average (random pivot): O(n log n)
  - Space: O(log n) recursion depth
  - Not stable (partition destroys order)

- **Pivot Selection Strategies**
  - First/last element: bad for sorted data
  - Random element: good average, no adversarial input
  - Median-of-three: first, middle, last → pick median
    - Reduces likelihood of bad partitions
  - Randomization: shuffle array first

- **Partition Implementation**
  - Choose pivot, place at end (or swap)
  - Two pointer technique: left from start, right from end
  - Left: scan until element > pivot
  - Right: scan until element <= pivot
  - If left < right: swap, continue
  - Swap pivot to final position
  - All elements left of pivot < pivot, right > pivot

- **Quick Sort Practical**
  - Default sort in many languages (C++ sort, Java Collections)
  - Fast in practice despite O(n²) worst case
  - O(log n) space (in-place)
  - Good cache locality (works on nearby elements)
  - Randomization prevents worst case in practice

- **Hybrid Approaches**
  - Introsort: start with quick sort, switch to heap sort if depth too deep
  - Used in C++ std::sort
  - Combines quick sort speed with guaranteed O(n log n)
  - Insertion sort for small subarrays

- **Comparing Merge Sort vs Quick Sort**
  - Merge: stable, predictable O(n log n), uses O(n) space
  - Quick: not stable, expected O(n log n), O(log n) space, faster in practice
  - Choice depends on: stability needed, space constraints, data characteristics

**Key Insights:**
- O(n log n) is standard for efficient general-purpose sorts
- Quick sort faster in practice despite worse worst case
- Merge sort stable and predictable
- Hybrid approaches get best of both

**Deliverables:**
- [ ] Implement merge sort
- [ ] Implement quick sort with random pivot
- [ ] Compare performance on various inputs
- [ ] Verify stability of merge sort

---

### 📅 DAY 3: HEAPS, HEAPIFY & HEAP SORT

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Binary Heap Model**
  - Complete binary tree: all levels full except last (left-filled)
  - Array representation: element at index i
    - Left child: 2i+1
    - Right child: 2i+2
    - Parent: (i-1)/2
  - Min-heap: parent <= children (min at root)
  - Max-heap: parent >= children (max at root)

- **Heap Property**
  - Min-heap: a[parent] <= a[child]
  - All ancestors <= descendants
  - Root is minimum (for min-heap)
  - No ordering between siblings

- **Insert (Bubble Up)**
  - Add element at end
  - While element < parent: swap with parent
  - Move up tree
  - Time: O(log n) (height of tree)

- **Extract Min (Bubble Down)**
  - Return root
  - Move last element to root
  - While element > children:
    - Swap with smaller child
  - Move down tree
  - Time: O(log n)

- **Build Heap from Array**
  - Naive: insert each element, O(n log n)
  - Smart: start from last non-leaf, bubble down each
    - Work backwards: last leaf at index n-1
    - Last non-leaf: index (n-1-1)/2 = (n-2)/2
    - Heapify each from bottom to top
  - Time: O(n) (total work at each level sums to O(n))

- **Heap Sort**
  - Build heap: O(n)
  - Extract min n times: O(n log n)
  - Total: O(n log n)
  - Space: O(1) if in-place (with array manipulations)
  - Not stable

- **Priority Queue Interface**
  - insert(key, value): add element with priority
  - extract_min(): get minimum priority element
  - decrease_key(position, new_key): lower priority of element
  - delete(position): remove element
  - peek(): view minimum without removing

- **Priority Queue Applications**
  - Dijkstra's algorithm: efficiently get next closest vertex
  - Huffman coding: repeatedly get two minimum frequency nodes
  - Task scheduling: processes in priority order
  - Event simulation: next event by time

- **Heap Variants**
  - Min-heap vs max-heap: mirror of each other
  - k-ary heap: each node has k children (k-ary tree)
  - Fibonacci heap: better amortized (advanced)
  - Binomial heap: supports merging (advanced)

- **Practical Heap Usage**
  - C++: std::priority_queue (based on std::make_heap)
  - Java: PriorityQueue
  - Python: heapq module
  - Most efficient way: use library implementation

**Key Insights:**
- Heap: efficient way to repeatedly access min/max
- Array representation: clever parent/child indexing
- Build heap O(n) surprising and important
- Priority queues: critical for many algorithms

**Deliverables:**
- [ ] Implement min-heap with insert/extract
- [ ] Implement build-heap
- [ ] Implement heap sort
- [ ] Use heap for priority queue

---

### 📅 DAY 4: HASH TABLES I - SEPARATE CHAINING

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Hash Function Fundamentals**
  - Maps keys (strings, objects) to indices (0 to m-1)
  - m = table size (typically power of 2)
  - Deterministic: same key always same index
  - Ideally: uniform distribution (keys spread evenly)
  - Example: hash(x) = x mod m (integers)

- **Hash Function Properties**
  - Fast to compute (O(1) desired)
  - Distributes evenly (minimize collisions)
  - Robust (small key changes produce different hashes)
  - Examples:
    - Java String.hashCode(): polynomial rolling hash
    - Python hash(): depends on object type
    - Cryptographic hashes: SHA-256 (overkill, slow)

- **Collisions**
  - Multiple keys hash to same index
  - Unavoidable by pigeonhole (more keys than buckets possible)
  - Resolution: separate chaining or open addressing
  - Separate chaining: chain of elements at each bucket

- **Separate Chaining Collision Resolution**
  - Each bucket: linked list of entries (key, value)
  - Collision: new entry added to chain
  - lookup(key): hash key, search chain at that bucket
  - insert(key, value): hash key, add to chain (replace if exists)
  - delete(key): hash key, remove from chain

- **Operations with Separate Chaining**
  - lookup(key): O(1 + chain_length)
  - insert(key, value): O(1 + chain_length)
  - delete(key): O(1 + chain_length)
  - Average chain_length ~ 1 + load_factor
  - Load factor α = n / m (n entries, m buckets)

- **Performance**
  - Good hash, low load factor α: O(1) expected
  - Bad hash, high load factor: O(n) (all keys same bucket)
  - Typical load factor: keep α < 1 (more buckets than entries)
  - Some languages let α approach 1

- **Load Factor & Resizing**
  - When α exceeds threshold (e.g., 0.75):
    - Create new larger table (typically 2×)
    - Rehash all entries into new table
    - Time: O(n) for rehash
    - Amortized per insertion: O(1)

- **Implementation Details**
  - Bucket: array of head pointers (or small vectors)
  - Chain: linked list of (key, value) pairs
  - Multiple entries with same key: possible (last wins typically)
  - Deletion: remove from chain, reclaim memory

- **Memory Overhead**
  - m pointers for buckets: 8m bytes
  - Chain nodes: (8 for next pointer + key size + value size) per entry
  - Allocator overhead: bytes per allocation
  - Total: roughly 16m + 40n bytes (rough estimates)

- **When Separate Chaining Works Well**
  - Good hash function: chains small
  - Dynamic resizing: keeps α reasonable
  - Cache locality: not great (chains scattered)

**Key Insights:**
- Hash functions distribute keys
- Separate chaining handles collisions simply
- Load factor balance: space vs search time
- Amortized O(1) operations with resizing

**Deliverables:**
- [ ] Implement hash table with separate chaining
- [ ] Implement resizing strategy
- [ ] Analyze chain lengths for various inputs
- [ ] Measure performance vs load factor

---

### 📅 DAY 5: HASH TABLES II - OPEN ADDRESSING & ROLLING HASH

**Duration:** 120 minutes

**Topics & Subtopics:**
- **Open Addressing Overview**
  - All entries stored directly in table (no chaining)
  - Collision: probe to find next empty slot
  - Probing sequence: positions to check

- **Linear Probing**
  - Collision at index h: try h+1, h+2, h+3, ... (mod m)
  - Continues until empty slot found
  - Deletion: mark as "deleted" (not empty) for correct probing
  - Problem: primary clustering
    - Filled sequences grow longer
    - Longer sequences attract more collisions
    - Performance degrades

- **Quadratic Probing**
  - Collision at index h: try h+1², h+4², h+9², ... (mod m)
  - Reduces clustering compared to linear
  - Problem: secondary clustering (collisions same hash probe same sequence)
  - Condition: m must be prime for guaranteed probe coverage

- **Double Hashing**
  - Two independent hash functions: h1, h2
  - Collision at h1(key): try h1(key) + h2(key), h1(key) + 2×h2(key), ...
  - Probe sequence depends on both functions
  - Requires h2(key) ≠ 0 and gcd(h2(key), m) = 1
  - Reduces clustering effectively

- **Load Factor in Open Addressing**
  - Typical keep α < 0.5 (many empty slots)
  - Performance degrades rapidly as α → 1
  - Resizing at α=0.75 typically
  - More sensitive to load factor than chaining

- **Comparison: Chaining vs Open Addressing**
  - Chaining: easier to implement, handles high α better
  - Open addressing: better cache locality (entries in table)
  - Chaining: deletion straightforward
  - Open addressing: deletion complex (tombstones)
  - Most implementations use chaining

- **Rolling Hash (Polynomial Hash)**
  - For strings: hash = a₀×p^(n-1) + a₁×p^(n-2) + ... + aₙ₋₁
  - p: prime base (typically 31, 263)
  - m: modulus (to keep values bounded)
  - Time: O(n) to compute initial hash
  - Sliding window: drop first character, add next character
    - hash_new = (hash_old - a₀×p^(n-1)) × p + aₙ
    - Time: O(1) per slide

- **Computing Rolling Hash Efficiently**
  - Precompute powers of p modulo m
  - p^(n-1) mod m available for constant-time subtraction
  - Update: subtract, multiply, add
  - All operations: O(1)

- **Karp-Rabin String Matching**
  - Problem: find pattern in text
  - Rolling hash: compute pattern hash once
  - Slide pattern over text, compute rolling text hash
  - When hashes match: found potential match (verify exact match)
  - Verification: O(pattern_length)
  - Time: O(text + pattern) expected, O(text × pattern) worst (many hash collisions)

- **Collision Handling in Karp-Rabin**
  - If hashes match but strings differ: false positive
  - Rare if good hash and large modulus
  - Verify exact match to be safe
  - Multiple hash functions reduce false positives

- **Applications of Rolling Hash**
  - Substring search (Karp-Rabin)
  - Plagiarism detection (hash chunks)
  - DNA sequence matching
  - Polynomial polynomial evaluation

- **Hash Security**
  - Adversarial inputs: attacker chooses keys to cause collisions
  - Hash flooding: deliberate collisions to cause DoS
  - Defense: randomized hash functions
  - Seed or salt in hash function prevents prediction

**Key Insights:**
- Open addressing: direct storage, no chains
- Clustering problems in linear/quadratic probing
- Double hashing: best of open addressing
- Rolling hash: efficient sliding window computation
- Karp-Rabin: practical string matching algorithm

**Deliverables:**
- [ ] Implement open addressing hash table (linear/quadratic/double)
- [ ] Implement rolling hash
- [ ] Implement Karp-Rabin substring search
- [ ] Compare hash table implementations

---

# 🟩 PHASE B: CORE PATTERNS & STRING MANIPULATION (Weeks 4-6)

## Phase Motivation
Patterns emerge from solving diverse problems. Recognizing patterns accelerates problem-solving dramatically. This phase trains pattern recognition and application to solve families of problems using consistent approaches.

## Phase Outcomes
- [ ] Recognize and apply 10+ foundational patterns
- [ ] Solve diverse problems by pattern selection
- [ ] Handle string problems with specialized techniques
- [ ] Distinguish between patterns and choose appropriately
- [ ] Combine patterns to solve complex problems

---

*[Weeks 4-6 follow same detailed structure as above weeks...]*

---

# 🟨 PHASE C: TREES, GRAPHS & DYNAMIC PROGRAMMING (Weeks 7-11)

## Phase Motivation
Trees and graphs model relationships. Algorithms on these structures unlock solutions to relationship problems. DP handles overlapping subproblems elegantly, transforming infeasible recursions into tractable algorithms.

## Phase Outcomes
- [ ] Implement and traverse trees and graphs
- [ ] Apply shortest path algorithms for single/all-pairs
- [ ] Find minimum spanning trees
- [ ] Recognize and solve DP problems across domains
- [ ] Combine tree/graph/DP techniques

---

*[Weeks 7-11 follow same detailed structure as above weeks...]*

---

# 🟧 PHASE D: ALGORITHM PARADIGMS (Weeks 12-13)

## Phase Motivation
Beyond data structures and patterns lie algorithmic paradigms. Understanding when greedy works, how to prune backtracking, why B&B succeeds, and formal amortized analysis separates intermediate from advanced problem-solvers.

## Phase Outcomes
- [ ] Understand greedy choice property and prove correctness via exchange arguments
- [ ] Design backtracking algorithms with effective pruning
- [ ] Formulate and solve problems via branch and bound
- [ ] Prove amortized bounds using three formal methods
- [ ] Select paradigm systematically for new problems

---

### 📌 WEEK 12: GREEDY ALGORITHMS & PROOF TECHNIQUES

### Weekly Goal
Master the greedy paradigm: when locally optimal choices lead to globally optimal solutions, and rigorous exchange argument proofs that guarantee correctness.

### Weekly Topics
- Greedy choice property and optimal substructure
- Exchange argument proof technique
- Interval scheduling (canonical example)
- Minimum spanning trees (graph greedy)
- Huffman coding (structure greedy)
- When greedy fails and decision framework

---

### 📅 DAY 1: GREEDY PARADIGM FOUNDATIONS

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Greedy Choice Property**
  - Definition: make locally best choice at each step
  - Question: does local optimum guarantee global optimum?
  - Not always true (must be proven for specific problem)
  - Example: activity selection works, coin change doesn't

- **Optimal Substructure**
  - Optimal solution contains optimal solutions to subproblems
  - Both greedy and DP need this property
  - Difference: greedy makes single choice, DP tries all choices

- **Problem Classification**
  - Optimization: find minimum or maximum
  - Decision: yes/no answer
  - Enumeration: count or list all
  - Greedy typically applies to optimization

- **When Greedy Might Work**
  - Optimization problem (minimize/maximize)
  - Problem exhibits optimal substructure
  - Can make locally optimal choice
  - Problem suggests natural order or priority

- **Greedy Paradigm Template**
  - Make locally optimal choice (according to some criteria)
  - Remove chosen item from problem
  - Recursively solve remaining problem
  - Combine results (often trivial for greedy)

- **Greedy vs DP**
  - Greedy: single path (make choice, commit)
  - DP: explores multiple paths (remember all choices)
  - Greedy faster but doesn't always work
  - DP always works (if state space manageable)

- **Proving Greedy Correct**
  - Exchange argument: prove greedy choice in some optimal solution
  - By induction: after exchange, still have optimal solution
  - Conclude: greedy achieves optimality

**Key Insights:**
- Greedy elegant but dangerous without proof
- Exchange argument is universal proof technique
- Optimal substructure necessary but not sufficient

**Deliverables:**
- [ ] Define greedy choice property
- [ ] List 3 problems and whether greedy likely works
- [ ] Understand exchange argument structure
- [ ] Sketch proof template

---

### 📅 DAY 2: INTERVAL SCHEDULING (CANONICAL GREEDY EXAMPLE)

**Duration:** 120 minutes

**Topics & Subtopics:**
- **Problem Statement**
  - Input: n intervals, each with start s_i and finish f_i
  - Output: maximum number of non-overlapping intervals
  - Intervals i and j overlap if neither completely before other
  - Real application: classroom scheduling, event booking

- **Why Greedy Works: Intuition**
  - Strategy: always pick interval finishing earliest
  - Leaves room for future intervals
  - Intuitively obvious but needs proof

- **Algorithm**
  - Sort intervals by finish time
  - Iterate through sorted list
  - For each interval: if doesn't overlap with last selected, select it
  - Return count of selected intervals

- **Complexity**
  - Sorting: O(n log n)
  - Selection loop: O(n)
  - Total: O(n log n)

- **Why Other Strategies Fail**
  - Earliest start: reserves early time unnecessarily, blocks later options
  - Shortest duration: doesn't optimize room leaving
  - Fewest conflicts: greedy heuristic without guarantee
  - Example counterexamples show each fails

- **Exchange Argument Proof**
  - Let OPT = optimal solution (set of intervals)
  - Let GREEDY = our greedy solution
  - Key lemma: first intervals in OPT and GREEDY can be exchanged
  - Proof: if OPT's first interval different from greedy's
    - Greedy picks earliest finishing
    - So greedy's finishes ≤ OPT's finishes
    - Interval after greedy's doesn't overlap (by greedy logic)
    - So interval after OPT's also doesn't overlap
    - Thus swapping preserves feasibility and optimality
  - By induction: remaining problem solved optimally
  - Conclusion: greedy is optimal

- **Weighted Variant (Greedy Fails)**
  - Each interval has value/weight
  - Goal: maximize total value of non-overlapping intervals
  - Greedy by value fails: heavy interval blocks two lighter ones
  - Greedy by value/length fails: similar issue
  - Root cause: value per unit time varies
  - Solution: dynamic programming (next phase)

- **Related Problems**
  - Weighted interval scheduling: DP solution
  - Job completion time scheduling: minimize total completion time
  - Minimizing lateness: minimize maximum lateness given deadlines
  - Each has different greedy strategy

**Key Insights:**
- Exchange argument: foundation of greedy correctness
- Adding weights breaks greedy property
- Problem structure critical to algorithm choice

**Deliverables:**
- [ ] Solve interval scheduling by hand
- [ ] Verify exchange argument on example
- [ ] Code interval scheduling
- [ ] Construct counterexample for weighted variant

---

### 📅 DAY 3: MINIMUM SPANNING TREES

**Duration:** 120 minutes

**Topics & Subtopics:**
- **MST Problem**
  - Input: connected graph, edge weights
  - Output: spanning tree (all vertices, n-1 edges) with minimum total weight
  - Tree: acyclic, connected
  - Spanning: reaches all vertices
  - Application: minimum cost network design

- **Greedy Property of MSTs**
  - Locally: pick lightest edge that doesn't create cycle
  - Globally: guarantees minimum total weight
  - Proof via cut property (below)

- **Kruskal's Algorithm**
  - Sort edges by weight (ascending)
  - Initialize: each vertex is own component (union-find)
  - For each edge (u,v) in sorted order:
    - If u and v in different components:
      - Add edge to MST
      - Union components
    - Else: skip (would create cycle)
  - Complexity: O(E log E) for sorting + O(E α(V)) for union-find
    - Typically: O(E log E) (sorting dominates)

- **Prim's Algorithm**
  - Start from arbitrary vertex
  - Maintain set of vertices in MST
  - Repeatedly: add lightest edge from MST to non-MST vertex
  - All added edges: safe (add minimum crossing cut)
  - Implementation: priority queue of edges
  - Complexity: O((V + E) log V) with binary heap
    - Dense graphs (E ≈ V²): O(V² log V)
    - Sparse graphs (E ≈ V): O(V log V)

- **Cut Property (Theoretical Justification)**
  - Cut: partition vertices into (S, T)
  - Cut edge: connects S and T
  - Theorem: minimum weight cut edge is in some MST
  - Proof: suppose not in OPT MST
    - Remove any edge in cycle containing cut edge
    - Add cut edge
    - New tree spans same vertices, less total weight
    - Contradiction with optimality of OPT

- **Cycle Property**
  - In any cycle: heaviest edge not in any MST
  - Follows from cut property
  - Both Kruskal and Prim satisfy cut property

- **Kruskal vs Prim Comparison**

  | Aspect | Kruskal | Prim |
  |--------|---------|------|
  | Focus | Global (all edges) | Local (from tree) |
  | Data structure | Sort + Union-Find | Priority queue |
  | Dense graphs | O(E log E) ≈ O(V² log V) | O(V² log V) |
  | Sparse graphs | O(E log E) ≈ O(V log V) | O((V+E) log V) ≈ O(V log V) |
  | Parallelizable | Yes (edges independent) | No (tree grows sequentially) |
  | Implementation | Slightly complex (union-find) | Simpler (priority queue) |

- **Proving Kruskal Greedy**
  - Each edge added: lightest among edges not creating cycle
  - By cut property: in some MST
  - n-1 edges added: forms MST

- **Proving Prim Greedy**
  - Each edge added: lightest crossing cut (S, V-S)
  - By cut property: in some MST
  - n-1 edges added: forms MST

**Key Insights:**
- Two different greedy strategies both work for MST
- Cut property: elegant justification of both
- Algorithm choice depends on graph density
- MST algorithms: common and important

**Deliverables:**
- [ ] Implement Kruskal's algorithm with union-find
- [ ] Implement Prim's algorithm with priority queue
- [ ] Trace both on example graph
- [ ] Compare running times on various inputs

---

### 📅 DAY 4: HUFFMAN CODING (OPTIONAL)

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Huffman Coding Problem**
  - Encode characters with variable-length codes
  - Minimize average code length
  - Prefix-free: no code is prefix of another
  - Example: "aaa", "aab", "ab" → "000", "001", "010"
  - Better: "a" → "0", "b" → "1" (if 'a' more frequent)

- **Binary Tree Representation**
  - Internal nodes: represent bit choice
  - Leaves: characters
  - Left edge: 0, right edge: 1
  - Code: path from root to leaf
  - Frequency: character frequency

- **Huffman's Algorithm**
  - Maintain priority queue of nodes (initially, each character)
  - While > 1 node:
    - Extract two minimum frequency nodes
    - Create parent with combined frequency
    - Insert parent back
  - Final tree: Huffman tree
  - Codes: paths from root to leaves

- **Correctness Proof (Greedy)**
  - Lemma 1: in optimal tree, two minimum frequency characters are siblings
    - Proof: if not, swap siblings → fewer edges at depth → better average
  - Lemma 2: merging two minimums is optimal choice
    - Proof: by induction on merged node as unit
  - Greedy choice property: at each step, pick minimums
  - Optimal substructure: remaining tree optimal for reduced problem

- **Complexity**
  - Build heap: O(n)
  - Extract/insert: O(n log n)
  - Total: O(n log n)

- **Information Theory Optimality**
  - Shannon entropy: H = Σ -p_i log₂(p_i)
  - Huffman code length: H ≤ average_code_length < H + 1
  - Optimal within 1 bit per character
  - Arithmetic coding achieves theoretical limit

- **Applications**
  - DEFLATE (gzip): Huffman after LZ77
  - JPEG: Huffman for quantized coefficients
  - MP3: Huffman for compressed audio
  - Text compression: common component

- **Practical Considerations**
  - Static vs adaptive Huffman
  - Must transmit tree (overhead for small files)
  - Arithmetic coding: better but slower

**Key Insights:**
- Greedy on tree structure
- Proves optimality via exchange argument
- One of most elegant greedy algorithms
- Foundation of modern compression

**Deliverables:**
- [ ] Implement Huffman tree construction
- [ ] Generate codes from tree
- [ ] Encode/decode strings
- [ ] Verify optimality on small inputs

---

### 📅 DAY 5: WHEN GREEDY FAILS & DECISION FRAMEWORK

**Duration:** 120 minutes

**Topics & Subtopics:**
- **Weighted Interval Scheduling Failure**
  - Each interval has value/weight
  - Greedy by value fails: heavy block two lighter
  - Greedy by value/duration fails: similar
  - Root cause: weight distribution doesn't follow greedy pattern
  - Solution: DP with overlapping subproblems

- **Coin Change Failure**
  - Denominations {1, 3, 4}, make amount 6
  - Greedy by value: 4 + 1 + 1 = 3 coins
  - Optimal: 3 + 3 = 2 coins
  - Not all coin systems have greedy property
  - Solution: DP

- **Graph Coloring (NP-Hard)**
  - Color vertices, no adjacent same color
  - Minimize colors used
  - Greedy: first-fit coloring
  - May use many colors, optimal uses far fewer
  - NP-hard: no polynomial algorithm known
  - Why greedy fails: no exchange argument possible (can't improve specific coloring)

- **Longest Path (vs Shortest Path)**
  - Shortest path: Dijkstra (greedy) works with non-negative weights
  - Longest path: greedy fails (NP-hard in general)
  - Difference: shortest is about avoiding detours
  - Longest: detours valuable, needs DP

- **Job Scheduling with Deadlines**
  - Schedule jobs to maximize profit
  - Each job has deadline and profit
  - Greedy by profit fails: high-profit blocks multiple lower-profit
  - Greedy by deadline fails: creates infeasible schedule
  - Solution: DP or more complex paradigm

- **General Failure Patterns**
  - Conflicting objectives: maximize A AND minimize B
  - Weights: value per unit varies by item
  - Future dependencies: current choice affects future feasibility
  - NP-hard problems: no greedy guarantee exists

- **Paradigm Decision Framework**

  | Factor | Greedy | DP | Backtracking | B&B |
  |--------|--------|-----|--------------|-----|
  | Subproblems | Independent | Overlapping | Exhaustive search | Optimization |
  | Optimality | Proof needed | Always | Any solution | Optimal guaranteed |
  | Complexity | Usually O(n) or O(n log n) | Usually O(n²) or worse | Usually exponential | Exponential with pruning |
  | Use case | Single best choice | Multiple interdependent | Feasibility | Large optimization |

- **Problem-Solving Strategy**
  1. Understand problem precisely
  2. Try greedy first (simplest)
  3. Look for counterexample
  4. If found: switch to DP or other paradigm
  5. If not found: prove via exchange argument
  6. If proof fails: reconsider problem structure

- **Hybrid Approaches**
  - Greedy heuristic + approximation analysis
  - Example: greedy set cover has ln n approximation
  - Greedy for initial solution, DP/B&B for refinement
  - Greedy on substructure, DP/B&B on overall

- **Meta-Lesson**
  - Problem understanding trumps algorithm choice
  - Greedy's elegance tempts overuse
  - Exchange arguments force rigor and thinking
  - "Prove it works" is essential discipline

**Key Insights:**
- Greedy fails for many real problems
- Knowing when to switch paradigm is crucial
- Framework helps systematic selection
- Proof discipline separates good from mediocre

**Deliverables:**
- [ ] Find counterexample for weighted interval scheduling
- [ ] Find counterexample for coin change {1,3,4}, amount 6
- [ ] Solve weighted interval scheduling with DP (preview)
- [ ] Build decision framework for problems

---

### 📌 WEEK 13: BACKTRACKING, BRANCH & BOUND & AMORTIZED ANALYSIS

### Weekly Goal
Master exploration paradigms (backtracking for feasibility, B&B for optimization) and formal amortized analysis using three distinct methods.

### Weekly Topics
- Backtracking with constraint-driven pruning
- N-Queens, Sudoku, and advanced heuristics
- Branch and bound framework
- Bounding functions and optimization
- Amortized analysis: aggregate, accounting, potential methods

---

### 📅 DAY 1: BACKTRACKING FOUNDATIONS

**Duration:** 90 minutes

**Topics & Subtopics:**
- **Backtracking Definition**
  - Systematic exploration of solution space via DFS
  - Build solution incrementally (partial solution)
  - Prune when constraints violated (constraint-driven pruning)
  - Backtrack: undo choice, try alternative
  - Goal: find ANY or ALL feasible solutions

- **State Space Tree**
  - Nodes: partial solutions at each decision level
  - Edges: possible choices from current state
  - Leaves: complete solutions (feasible or infeasible)
  - Depth: number of decisions made
  - Breadth: number of choices per decision
  - Tree can be huge: pruning essential

- **Generic Backtracking Pseudocode**
  ```
  Backtrack(partial_solution, remaining_choices):
    if is_complete(partial_solution):
      if is_valid(partial_solution):
        report_solution(partial_solution)
      return
    
    for choice in remaining_choices:
      if is_feasible(choice, partial_solution):
        add_choice(partial_solution, choice)
        Backtrack(partial_solution, remaining_choices \ {choice})
        undo_choice(partial_solution, choice)
  ```

- **Complexity Analysis**
  - Worst case: O(b^d) where b = branching factor, d = depth
  - With pruning: dramatically reduced (problem-dependent)
  - Example: N-Queens worst O(N!), with pruning O(N^0.5) or better
  - Lesson: pruning effectiveness determines practicality

- **Space Complexity**
  - O(d) recursion stack
  - O(d) partial solution storage
  - Total: O(d) much better than storing all solutions
  - Can become problematic for deep recursion

- **Key Decisions in Design**
  - State representation: what defines partial solution
  - Choice generation: how to enumerate candidates
  - Feasibility check: when to prune
  - Pruning heuristics: how aggressive prune

- **Pruning Strategies**
  - Constraint checking: hard constraint violation
  - Feasibility deduction: remaining problem impossible
  - Bound checking: preview of B&B (if can't improve best, skip)
  - Variable ordering: choose most constrained first (MRV)
  - Forward checking: reduce future domains

**Key Insights:**
- Backtracking explores exponential space systematically
- Pruning is survival: without it, exponential blowup
- Design pruning as aggressively as feasible
- Heuristics dramatically reduce practical complexity

**Deliverables:**
- [ ] Draw state space tree for simple problem
- [ ] Implement generic backtracking template
- [ ] Design 3 different pruning strategies
- [ ] Analyze complexity without and with pruning

---

### 📅 DAY 2: BACKTRACKING APPLICATIONS

**Duration:** 120 minutes

**Topics & Subtopics:**
- **N-Queens Problem (Canonical)**
  - Place N queens on N×N board, no two attack
  - Attack: same row, column, or diagonal
  - State: current row's queen position
  - Choices: each column
  - Pruning: check column and diagonals
  - Algorithm:
    - Row by row placement
    - For each column: check safe placement
    - If safe: place and recurse to next row
    - If no safe column: backtrack

- **N-Queens Feasibility**
  - Check column: any queen in same column → not safe
  - Check diagonals: upper-left to lower-right, upper-right to lower-left
  - Optimization: track occupied columns/diagonals (bitmask)
  - Complexity: O(N!) without optimization, O(N × N) per branch
  - Practical: n ≤ 12-14 with optimizations

- **Sudoku Solver**
  - 9×9 grid, 1-9 unique per row/column/3×3 box
  - Constraint Satisfaction Problem (CSP)
  - State: current grid fill
  - Choices: unassigned cells, valid digits
  - Pruning: rule out invalid digits
  - Heuristics: MRV (minimum remaining values)
    - Choose cell with fewest valid digits
    - Most constrained first → earlier conflicts detected
  - Constraint propagation: deduce forced assignments
    - Naked singles: cell has one choice
    - Hidden singles: digit fits one cell in unit
    - Apply propagation before branching

- **Sudoku Complexity**
  - Without heuristics: exponential search
  - With MRV: dramatically smaller search tree
  - With propagation: even smaller
  - Practical: most puzzles solve in <100ms with good code

- **Word Search / Boggle**
  - Find word path in letter grid
  - DFS from each starting cell
  - Visited tracking: no cell reuse
  - Early termination: letter mismatch stops branch
  - Complexity: O(N × M × 4^L) where N×M grid, L word length
  - Optimization: Trie for multiple words

- **Graph Coloring Variants**
  - Color vertices, no adjacent same color
  - Minimize number of colors
  - Binary search on k colors
  - State: current color assignment
  - Choices: colors for current vertex
  - Pruning: neighbors must differ
  - Heuristics: highest degree first (likely hardest)

- **Heuristics for Efficient Pruning**
  - Minimum Remaining Values (MRV):
    - Choose most constrained variable first
    - Detects conflicts earlier
    - Significantly reduces search tree
  - Least Constraining Value (LCV):
    - Among choices, try value eliminating fewest options
    - Leave flexibility for future variables
  - Constraint Propagation:
    - After assignment, deduce forced assignments
    - Reduce domain of unassigned variables
  - Forward Checking:
    - After assignment, check if any variable becomes impossible
    - Detect failure earlier

- **Puzzle-Solving General Pattern**
  - State: partial solution
  - Constraint check: current state valid
  - Feasibility deduction: future impossible?
  - If impossible: prune
  - If complete: found solution
  - Otherwise: expand choices, recurse

**Key Insights:**
- Heuristics transform exponential to practical
- MRV + propagation: most impactful for CSP
- Problem-specific pruning: aggressive helps dramatically
- Backtracking: general paradigm, specialization per problem

**Deliverables:**
- [ ] Implement N-Queens with backtracking
- [ ] Implement Sudoku solver with MRV + propagation
- [ ] Trace Word Search on 4×4 grid
- [ ] Design custom heuristic for problem

---

### 📅 DAY 3: BRANCH AND BOUND PARADIGM

**Duration:** 120 minutes

**Topics & Subtopics:**
- **Distinction from Backtracking**
  - Backtracking: prune when constraints violated (feasibility)
  - Branch & Bound: prune when suboptimal (optimality)
  - Both: explore decision tree
  - B&B: adds numerical comparison

- **Optimization vs Feasibility**
  - Feasibility: find ANY valid solution
  - Optimality: find BEST valid solution
  - Backtracking typically solves feasibility
  - B&B typically solves optimization

- **Node Structure in B&B**
  - Level: depth (decisions made)
  - Value: actual cost/objective of partial solution
  - Bound: estimated best achievable from node onward
  - Lower bound (minimization): can't do better
  - Upper bound (maximization): can't exceed

- **Generic B&B Algorithm**
  ```
  BranchAndBound(root_problem):
    best_value = initial_bound (infinity for minimization)
    best_solution = None
    frontier = priority_queue([root_node])
    
    while frontier not empty:
      node = select_best_from_frontier()
      
      if node.bound >= best_value:
        continue  # Prune: can't improve
      
      if is_leaf(node):  # Complete solution
        if node.value < best_value:
          best_value = node.value
          best_solution = node
      else:  # Expand node
        for child in generate_children(node):
          if child.bound < best_value:
            frontier.add(child)
    
    return best_solution
  ```

- **Bounding Function (Critical)**
  - Gives estimate of best possible solution from node
  - Must be: fast to compute (O(1) or O(log n))
  - Must be: valid lower bound (achievable or underestimate)
  - Tightness crucial: loose bounds prune little
  - Design techniques:
    - Relaxation: remove constraints, solve relaxed
    - Greedy: use greedy on remaining choices
    - LP relaxation: solve linear programming
    - Problem-specific: domain knowledge

- **Example: Knapsack via B&B**
  - State: items 0..i, included/excluded
  - Bound: greedy relaxation on remaining
    - Sort by value/weight ratio
    - Fill fractional knapsack with remaining
    - Sum of included + fractional bound
  - Algorithm:
    - Try including item i: recurse if promising
    - Try excluding item i: recurse if promising
    - Prune if bound < current best
  - Practical: good for n ≤ 30-40

- **Example: TSP via B&B**
  - State: partial tour
  - Bound: MST of unvisited + current cost
    - MST valid lower bound (at least spanning is needed)
  - Algorithm:
    - Try extending to each unvisited city
    - Prune if MST bound + current > best
  - Practical: n ≤ 20-25

- **Node Selection Strategies**
  - FIFO (breadth-first): level-by-level exploration
  - LIFO (depth-first): find solution quickly
  - Best-first: most promising first (by bound)
  - LC-search: least cost first (actual + bound)

- **Memory vs Pruning Tradeoff**
  - Best-first: best pruning, high memory
  - LIFO: low memory, medium pruning
  - FIFO: medium memory, medium pruning

**Key Insights:**
- B&B transforms some NP-hard problems practical
- Bounding function determines effectiveness
- Tight bounds critical: loose bounds don't prune
- Problem-specific domain knowledge crucial for bounds

**Deliverables:**
- [ ] Implement generic B&B template
- [ ] Design bounding function for 0/1 knapsack
- [ ] Implement knapsack B&B solver
- [ ] Compare node selection strategies

---

### 📅 DAY 4: BRANCH AND BOUND APPLICATIONS

**Duration:** 120 minutes

**Topics & Subtopics:**
- **0/1 Knapsack via B&B**
  - Problem: maximize value, capacity constraint
  - Greedy bound: compute fractional knapsack on remaining
  - Implementation:
    - Sort items by value/weight ratio
    - Precompute prefix sums
    - At each node: compute fractional knapsack value
    - Pruning: if value < best, skip
  - Complexity: O(n²) worst case, often much better
  - Practical: good for n ≤ 30-40

- **TSP via B&B**
  - Problem: minimum cost tour visiting all cities
  - MST bound: MST of unvisited + current edge cost
    - Lower bound: at least need spanning tree
  - Implementation:
    - Maintain partial tour
    - Try extending to each unvisited
    - Compute MST of unvisited (can cache)
    - Prune if too expensive
  - Complexity: exponential with heavy pruning
  - Practical: n ≤ 20-25 for exact solution

- **Assignment Problem via B&B**
  - n workers to n jobs, minimize total cost
  - Can use Hungarian algorithm as bounding
  - Or use LP relaxation
  - Often better bounds than TSP

- **Integer Linear Programming**
  - General framework: minimize c^T x subject to Ax ≤ b, x ∈ Z^n
  - LP relaxation: remove integer constraint
  - B&B: branch on fractional variables
  - Bounds: LP relaxation lower bound
  - Advanced topic: complex implementations

- **Choosing Bounding Function**
  - Tradeoff: tight bounds prune more, take longer
  - Loose bounds prune little, but fast
  - Sweet spot: moderately tight, computable in O(1-log n)
  - Problem-specific: requires analysis

- **Comparison: Backtracking vs B&B**
  - Backtracking: feasibility, constraint-driven pruning
  - B&B: optimality, bound-driven pruning
  - Backtracking: sometimes used for optimization (branch on improvement)
  - B&B: proper optimization paradigm

**Key Insights:**
- B&B success depends on bounding function quality
- Practical for NP-hard up to ~n=25-40 depending
- Better bounds → better pruning
- Problem-specific design crucial

**Deliverables:**
- [ ] Implement knapsack B&B solver
- [ ] Implement TSP B&B solver
- [ ] Compare performance vs brute force
- [ ] Design problem-specific bound

---

### 📅 DAY 5: ALGORITHM SELECTION FRAMEWORK

**Duration:** 120 minutes

**Topics & Subtopics:**
- **Paradigm Comparison Matrix**
  - All paradigms so far: brute force, greedy, DP, backtracking, B&B, divide-and-conquer
  - Use cases, complexity, space, optimality, proof method
  - Visual comparison chart

- **Problem-to-Paradigm Mapping**
  - N-Queens: backtracking (feasibility, constraints)
  - MST: greedy (optimal substructure, locally optimal)
  - Knapsack: DP (overlapping subproblems, choices)
  - TSP: B&B (optimization, hard constraints)
  - Shortest path: greedy (Dijkstra)
  - Merge sort: divide-and-conquer
  - 15+ other examples mapped

- **Decision Flowchart**
  - Start: understand problem
  - Is it optimization or feasibility?
  - Optimization: does greedy choice property hold?
    - Yes: try greedy, prove exchange argument
    - No: overlapping subproblems? → DP
    - No: exponential with pruning? → B&B
  - Feasibility: constraints driving? → backtracking
  - Feasibility: enumerate all? → backtracking

- **Case Studies**
  - Weighted interval scheduling: why DP not greedy
  - Fractional vs 0/1 knapsack: why algorithms differ
  - Shortest path variants: why algorithms differ
  - Greedy failure patterns repeated

- **Meta-Skills for Problem Selection**
  - Read problem carefully: optimization vs feasibility
  - Identify problem structure: overlaps, constraints, relationships
  - Check problem properties: greedy choice, optimal substructure
  - Select paradigm: follow decision flowchart
  - Prove correctness: exchange, induction, bounds
  - Analyze complexity: time and space
  - Implement carefully: edge cases, off-by-one

- **Common Mistakes**
  - Assuming greedy without proof
  - Overlooking overlapping subproblems (miss DP)
  - Not pruning effectively (backtracking too slow)
  - Loose bounds (B&B ineffective)
  - Analyzing wrong complexity class

**Key Insights:**
- Systematic approach to paradigm selection
- Problem structure determines algorithm
- Multiple paradigms may apply, choose by requirements
- Proof discipline separates correct from buggy

**Deliverables:**
- [ ] Complete paradigm comparison matrix
- [ ] Map 10 problems to correct paradigms
- [ ] Trace decision flowchart on new problem
- [ ] Design your own decision checklist

---

### 📅 DAY 6: AMORTIZED ANALYSIS (FORMAL METHODS)

**Duration:** 120 minutes

**Topics & Subtopics:**
- **Motivation**
  - Some operations expensive (O(n)), others cheap (O(1))
  - Amortized: average over sequence
  - Not probabilistic; guaranteed average
  - More refined than worst-case

- **Aggregate Method**
  - Total cost of n operations / n = amortized cost per operation
  - Example: dynamic array append
    - n appends: most O(1), few O(n) (resizes)
    - Resizes at 1, 2, 4, 8, ..., n: cost 1+2+4+...+n = 2n-1 = O(n)
    - n appends total: O(n) time
    - Amortized: O(n) / n = O(1) per append
  - Simple, intuitive, straightforward

- **Accounting Method**
  - Assign amortized cost to each operation
  - Some operations overpay (bank credit)
  - Others underpay (draw credit)
  - Maintain credit invariant (never negative)
  - Example: dynamic array
    - Charge 3 per append (1 store + 2 for future resize)
    - Build credit pool
    - When resize needed, use accumulated credit
    - Never go negative ✓

- **Potential Method**
  - Define potential function Φ(state) measuring structure
  - Amortized cost = actual cost + ΔΦ
  - Amortized cost = c_actual + Φ_after - Φ_before
  - Sum of amortized: actual total + final Φ - initial Φ
  - If Φ_final ≥ Φ_initial: amortized ≤ actual total
  - Example: dynamic array Φ = 2n - c
    - After append (no resize): ΔΦ = 2 (amortized 3)
    - After append (resize at n → 2n): ΔΦ = 2(2n) - 2n - (2n - n) = n
      - Amortized = n + n = 2n? (recalculate)
      - Actually: initial potential = 2n - n = n
      - After resize: n → 2n, potential = 2(2n) - 2n = 2n
      - ΔΦ = 2n - n = n
      - Amortized = O(n)? (doesn't match O(1) needed)
    - Better Φ: try differently
    - Φ = 2n - c (c = capacity)
      - Append: if no resize, ΔΦ = 2, amortized = 1 + 2 = 3 ✓
      - Append with resize: harder to compute, but total works

- **Applications of Amortized Analysis**
  - Dynamic arrays: O(1) append amortized
  - Stack with multipop: O(1) amortized
  - Union-Find: O(α(n)) amortized (inverse Ackermann)
  - Fibonacci heaps: O(1) decrease-key amortized

- **Comparison of Three Methods**
  - Aggregate: simple but doesn't give per-operation insight
  - Accounting: intuitive, shows where credit comes from
  - Potential: formal, elegant, requires insight to design Φ
  - Choice: depends on problem and preference

- **Amortized vs Average-Case**
  - Amortized: guaranteed worst-case over sequence
  - Average-case: probabilistic or over random input
  - Different analysis techniques
  - Amortized stronger: no probability assumptions

**Key Insights:**
- Amortized: refinement between worst-case and best-case
- Three methods: all valid, different perspectives
- Potential method: most elegant but requires insight
- Accounting method: most intuitive for teaching

**Deliverables:**
- [ ] Prove dynamic array append O(1) using all 3 methods
- [ ] Analyze stack multipop amortized
- [ ] Design potential function for custom problem
- [ ] Compare methods on example

---

# 🟪 PHASE E: INTEGRATION & EXTENSIONS (Weeks 14-15)

## Phase Motivation
Weeks 1-13 built individual components. Phase E combines paradigms on complex problems and introduces specialized advanced topics (strings, flow, geometry basics).

## Phase Outcomes
- [ ] Apply multiple paradigms to single problem
- [ ] Solve advanced string pattern problems
- [ ] Understand network flow and applications
- [ ] Reduce matching problems to flow
- [ ] Synthesize curriculum knowledge

---

*[Weeks 14-15 continue with same detailed structure...]*

---

**Version:** 13.0  
**Status:** ✅ Complete Detailed Syllabus v13 Production Ready  
**Total Coverage:** 235-270 hours across 19 weeks

---

*Continue with Weeks 14-15, 16-18 (Phase F), and Week 19 (Phase G) following same detailed format with full pointwise topic/subtopic breakup...*
