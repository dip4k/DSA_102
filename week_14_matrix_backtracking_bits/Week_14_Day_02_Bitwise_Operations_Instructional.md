# 📅 Week 14, Day 2: Basic Bitwise Operations, Tricks, Subset Enumeration & Gray Codes




> 🧭 **Navigation:** [← Previous Day](Week_14_Day_01_Matrix_Operations_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_14_Day_03_Number_Theory_Basics_Instructional.md)
> 
> 💡 **Instructor Note:** *Not all sections or topics are mandatory. Feel free to adapt your pace and skim or skip sections based on your current focus and interview timeline.*

---

Welcome to Day 2. Today, we study registers, binary digits manipulation, and how to pack full set structures into single-word registers with zero allocation costs.

---

## 🎯 Learning Objectives
*   Master basic bitwise operations: AND, OR, XOR, NOT, and shifting.
*   Manipulate binary configurations using tricks like clearing and isolating the lowest set bit.
*   Enumerate all submasks of a bitmask in decreasing order with a single loop.
*   Understand Gray Code representations and populations counting (popcount) logic.

---

## 📘 Chapter 1: Context and Motivation

### 1. The Core Engineering Challenge
In high-performance computing, memory allocations are expensive. Managing collections of state flags using standard arrays or hash sets requires dynamic heap memory blocks, which triggers garbage collection cycles.
Because a byte contains 8 bits, we can represent 8 distinct boolean values in a single byte, or up to 64 flags in a standard 64-bit integer register. Performing operations directly on these bits executes in a single clock cycle, bypasses the heap, and has zero memory allocation overhead.

### 2. Naive Pitfalls
The standard way to check if a number is a power of two is to repeatedly divide it by 2:
```csharp
while (n % 2 == 0) { n /= 2; }
return n == 1;
```
This naive iterative approach takes O(log N) instructions. If we run this division repeatedly inside a high-frequency loop, it introduces significant performance overhead. 

### 3. Real-world Anchor: Low-level Flags
Operating systems and network protocols represent configurations as combined bit flags inside a single word. For example, file permissions in UNIX are packed into a single integer:
*   Read (`R = 4` -> `100`), Write (`W = 2` -> `010`), and Execute (`X = 1` -> `001`).
*   To enable Read and Write permissions, perform a bitwise OR: `4 | 2 = 6` (`110`).

---

## 📘 Chapter 2: Mental Model

### 1. The Fixed-Width Boolean Register
Think of an integer as a fixed-capacity boolean array indices 0 to 31 or 63.
*   Register index i corresponds to the boolean flag stored at the i-th bit: `(n >> i) & 1`.
*   We can update this flag using bitwise operations:

```text
Position Index:   [ 7 | 6 | 5 | 4 | 3 | 2 | 1 | 0 ]
Active Bitmask:   [ 0 | 1 | 0 | 0 | 1 | 1 | 0 | 1 ] = Value 77
                                        ^
                                   Isolating Bit at Index 3
```

#### 🔌 Core Bit Operator Logic
*   **AND (`&`)**: Intersects active bits. Useful for masking operations: `n & mask`.
*   **OR (`|`)**: Unifies active bits. Useful for enabling flags: `n |= mask`.
*   **XOR (`^`)**: Detects differences. If you flip a bit twice, it returns to its original state.
*   **NOT (`~`)**: Symmetrically complements all bits.
*   **Shifts (`<<`, `>>`)**: Moves components left or right. Each shift is equivalent to multiplying or dividing by 2.

---

## 📘 Chapter 3: Mechanics

### 1. Fundamental Bit tricks
```csharp
public static class BitwiseUtils {
    // Check if power of two (guards against 0 and negatives)
    public static bool IsPowerOfTwo(int n) {
        return n > 0 && (n & (n - 1)) == 0;
    }

    // Clear the rightmost set bit
    public static int ClearLowestSetBit(int n) {
        return n & (n - 1);
    }

    // Isolate the rightmost set bit (returns the isolated value)
    public static int IsolateLowestSetBit(int n) {
        return n & -n;
    }

    // Check if the i-th bit is set
    public static bool CheckBit(int n, int i) {
        return ((n >> i) & 1) == 1;
    }

    // Set the i-th bit to 1
    public static int SetBit(int n, int i) {
        return n | (1 << i);
    }

    // Clear the i-th bit to 0
    public static int ClearBit(int n, int i) {
        return n & ~(1 << i);
    }

    // Toggle the i-th bit
    public static int ToggleBit(int n, int i) {
        return n ^ (1 << i);
    }
}
```

---

### 2. Brian Kernighan's Popcount
A naive population count algorithm shifts an integer bit by bit to count all set flags, which takes `O(word_size)` steps.
**Brian Kernighan's algorithm** is faster because it executes exactly once per active set bit. It uses the property that subtracting 1 from a number flips all bits starting from its rightmost set bit.
Therefore, performing `n & (n - 1)` clears the lowest set bit in exactly one cycle.

```csharp
public static int PopCount(int n) {
    int count = 0;
    while (n > 0) {
        n &= (n - 1); // Clears the lowest set bit
        count++;
    }
    return count;
}
```

---

### 3. Exhaustive Submask Loops
Using a bitmask from 0 to 2^N - 1 lets you enumerate all possible subsets of an array of size N.
To find all subsets (submasks) of a specific active bitmask, use this optimized loop. It systematically clears trailing bits and re-activates sub-bits that match your original filter mask, enumerating all valid submasks in decreasing order.

```csharp
public static List<int> EnumerateSubmasks(int mask) {
    var submasks = new List<int>();
    int submask = mask;
    while (submask > 0) {
        submasks.Add(submask);
        // Step to next valid submask using algebraic intersection
        submask = (submask - 1) & mask;
    }
    submasks.Add(0); // Include the empty set
    return submasks;
}
```

---

### 4. Gray Code Generation
A Gray code is a binary representation system where consecutive numbers differ by exactly one bit.
To convert an integer N to its Gray code representation, use this formula:
Gray(N) = n ^ (n >> 1)

### 4. Python Clarity-First Implementations
```python
def is_power_of_two(n: int) -> bool:
    """Verifies if positive integer n is a power of 2.
    
    Time Complexity: O(1) | Space Complexity: O(1)
    """
    return n > 0 and (n & (n - 1)) == 0


def brian_kernighan_popcount(n: int) -> int:
    """Counts active set bits in an integer.
    
    Time Complexity: O(Set Bits) | Space Complexity: O(1)
    """
    count = 0
    while n > 0:
        n &= n - 1 # Clear lowest set bit
        count += 1
    return count


def enumerate_submasks(mask: int) -> list[int]:
    """Generates all subsets (submasks) of a given integer filter mask.
    
    Time Complexity: O(2^K) where K is number of set bits | Space Complexity: O(1)
    """
    submasks = []
    sub = mask
    while sub > 0:
        submasks.append(sub)
        sub = (sub - 1) & mask
    submasks.append(0)
    return submasks


def integer_to_gray(n: int) -> int:
    """Computes the Gray code representation of value n.
    
    Time Complexity: O(1) | Space Complexity: O(1)
    """
    return n ^ (n >> 1)
```

---

---

## 📘 Chapter 4: Performance, Invariants & Systems Architecture

### 1. Exact Governing Invariants

*   **Two's Complement & LSB Isolation Invariant**: In two's complement binary representation, `-n = ~n + 1`. Subtracting from zero inverts all bits and adds 1, which preserves the lowest set bit of `n` and flips all higher-order bits. Therefore:
    `LSB(n) = n & -n`
    isolates the unique lowest set bit in exactly one ALU instruction.
*   **Brian Kernighan PopCount Invariant**: For any positive integer `n`, `n - 1` toggles the lowest set bit to 0 and flips all subsequent trailing zeros to 1, leaving all bits above the lowest set bit identical. Hence:
    `n & (n - 1)`
    clears strictly the rightmost set bit while preserving every higher bit. The loop runs exactly `K` times (where `K` is the Hamming weight / count of active set bits), terminating deterministically when `n = 0`.
*   **Submask Enumeration Invariant**: For an initial bitmask `M`, the transition:
    `sub = (sub - 1) & M`
    monotonically decreases `sub` through strictly valid subsets of `M` (`sub & M == sub`). Subtracting 1 from `sub` flips its lowest set bit to 0 and all lower bits to 1; intersecting with `M` clears all bits not present in `M`. It enumerates all `2^K` submasks in descending order without visiting any invalid states. The sum across all subsets of size `N` satisfies:
    `sum_{k=0}^N binom(N, k) * 2^k = (1 + 2)^N = 3^N`
*   **Gray Code Invariant**: For any integer `n >= 0`, `Gray(n) = n ^ (n >> 1)`. For every consecutive pair `n` and `n + 1`, their Gray code representations differ at exactly one bit position (Hamming distance = 1), preventing multi-bit transitional glitches in hardware encoders.
*   **XOR Cancellation Invariant**: XOR is associative, commutative, and self-inverting (`x ^ x = 0`, `x ^ 0 = x`). For any sequence where all elements except one appear with even frequency, folding via XOR reduces the entire sequence to the unique element in `O(N)` time and `O(1)` auxiliary space.

---

### 2. Explicit Complexity Deconstruction

| Operation | Time (Best / Avg / Worst) | Auxiliary Space | Output Space | Mathematical Derivation |
| :--- | :--- | :--- | :--- | :--- |
| **IsPowerOfTwo** | `O(1)` / `O(1)` / `O(1)` | `O(1)` | `O(1)` | Single branchless bitwise AND check: `n > 0 && (n & (n - 1)) == 0`. |
| **Brian Kernighan PopCount** | `O(1)` / `O(K)` / `O(K)` | `O(1)` | `O(1)` | Executes exactly `K` iterations, where `K <= 32` (or `64`) is the number of set bits. |
| **Submask Enumeration** | `O(1)` / `O(2^K)` / `O(2^K)` | `O(1)` | `O(2^K)` | Single mask with `K` set bits has exactly `2^K` submasks; `O(3^N)` total work over all `2^N` masks. |
| **IntegerToGray** | `O(1)` / `O(1)` / `O(1)` | `O(1)` | `O(1)` | Single bitwise shift and XOR instruction. |
| **Single Number (XOR)** | `O(N)` / `O(N)` / `O(N)` | `O(1)` | `O(1)` | Single linear pass accumulator; zero heap allocations. |
| **Single Number II (Mod-3)** | `O(N)` / `O(N)` / `O(N)` | `O(1)` | `O(1)` | State machine tracking two 32-bit masks (`ones`, `twos`) over `N` elements. |

---

### 3. Senior Interview Context: Hardware, Registers & Branchless Design

*   **ALU Single-Cycle Throughput**: Bitwise instructions (`AND`, `OR`, `XOR`, `SHL`, `SHR`) execute in 1 CPU clock cycle with sub-nanosecond latency on modern x86/ARM cores. Unlike divisions (`DIV` taking 20-40 cycles) or modulo arithmetic, bit manipulation runs directly in CPU registers without memory access.
*   **Branchless Programming & Pipeline Hazards**: High-performance systems use bit manipulation to eliminate conditional branches (`if`/`else`). A branch misprediction flushes the CPU pipeline, causing a 15-20 cycle penalty. Using bitwise expressions converts control dependencies into data dependencies, maximizing instruction-level parallelism (ILP).
*   **Signed Shift vs Logical Shift Pitfall**:
    - In C#, `>>` on signed `int` performs an **arithmetic shift** (replicates the sign bit). Use unsigned `uint` or the unsigned right-shift operator `>>>` (.NET 7+) to guarantee zero-fill.
    - In Python, integers have arbitrary precision (infinite virtual width). Operations like `~n` invert an infinite sign bit. When simulating 32-bit registers, always mask results with `& 0xFFFFFFFF`.
*   **State Compression in Scaled Systems**: In distributed state machines, graph search engines, and combinatorial solvers (TSP, Hamiltonian Path), storing visited states in `HashSet<int>` creates massive GC overhead and cache misses. Compressing states into a 64-bit integer bitmask reduces memory footprint by up to 64x and allows `O(1)` transition checks.

---

## 🎙️ Senior 45-Minute Verbal Talk Track

```text
+-------------------------------------------------------------------------------+
| PHASE 1: Constraints & Bit-Width Clarification (Minutes 00 - 05)              |
| - Clarify word size (32-bit signed vs 64-bit unsigned), negative values.      |
| - Verify whether input contains duplicates, zeros, or edge boundary limits.   |
+-------------------------------------------------------------------------------+
                                      │
                                      ▼
+-------------------------------------------------------------------------------+
| PHASE 2: Naive Iteration & Bottleneck Analysis (Minutes 05 - 10)              |
| - Propose naive linear bit-scan: 32 loop cycles with modulo/division.         |
| - Highlight CPU cycle cost, branch predictor stalls, and GC heap pressure.    |
+-------------------------------------------------------------------------------+
                                      │
                                      ▼
+-------------------------------------------------------------------------------+
| PHASE 3: Mathematical Invariant Derivation    (Minutes 10 - 20)               |
| - Explain two's complement arithmetic: -n = ~n + 1 => n & -n isolates LSB.   |
| - Prove n & (n - 1) clears the lowest set bit in exactly one instruction.     |
| - Derive (sub - 1) & mask for optimal O(3^N) subset traversal.                |
+-------------------------------------------------------------------------------+
                                      │
                                      ▼
+-------------------------------------------------------------------------------+
| PHASE 4: Branchless Production Implementation (Minutes 20 - 35)               |
| - Implement idiomatic C# (.NET 8/9) and Python (3.11+) routines.              |
| - Add defensive guards for n = 0, signed overflow, and empty edge cases.      |
| - Use unsigned types (uint/ulong) to prevent sign-bit corruption.             |
+-------------------------------------------------------------------------------+
                                      │
                                      ▼
+-------------------------------------------------------------------------------+
| PHASE 5: Hardware Verification & DP Scaling    (Minutes 35 - 45)              |
| - Walk through complexity: O(K) time, O(1) auxiliary space.                  |
| - Test extreme values: n = 0, n = 1, n = 2^31 - 1, n = -2^31.                 |
| - Transition to Bitmask Dynamic Programming (TSP, subset sum, matching).      |
+-------------------------------------------------------------------------------+
```

### Verbal Script Excerpts

*   **Opening (Minutes 00-05)**: *"Before writing code, let me verify the integer specifications. Are we handling signed 32-bit integers or unsigned 64-bit registers? If the input can include zero or negative values, we must explicitly guard against sign-extension bugs and handle `n <= 0` gracefully."*
*   **Invariant Articulation (Minutes 10-20)**: *"A naive popcount checks each of the 32 bits sequentially. However, Brian Kernighan's algorithm observes that `n - 1` borrows from the lowest set bit, flipping it to 0 and setting all trailing zero bits to 1. Computing `n & (n - 1)` therefore clears the lowest set bit while leaving all higher bits untouched. This means our loop runs in `O(K)` iterations where `K` is strictly the number of set bits, achieving optimal efficiency."*
*   **Submask Enumeration Defense (Minutes 35-45)**: *"When iterating through submasks of a bitmask in dynamic programming, a naive loop tests all numbers from `0` to `mask`, taking `O(2^N * 2^N) = O(4^N)`. By using `sub = (sub - 1) & mask`, we only visit valid submasks. By the binomial theorem, the sum of `2^k` over all masks of size `k` equals `(1 + 2)^N = 3^N`, eliminating 75% of the computational work for `N = 15`."*

---

## 📘 Chapter 5: Integration and Mastery

### 1. Pattern Selection Rules
*   *Use Bit Operations when*: You need to track set membership across small universes (`N <= 64`) with absolute zero heap memory allocation.
*   *Use XOR Parity Checks when*: You need to isolate unique elements or detect mismatched pairs where duplicates cancel out symmetrically.
*   *Use Submask Enumeration when*: Solving optimal partitioning problems (e.g., minimum cost to cover subsets, Traveling Salesperson).

### 2. Follow-Up Variants
*   **Single Number II** ([LeetCode 137](https://leetcode.com/problems/single-number-ii/)): An array contains duplicate elements appearing three times except for one unique element.
    *   *Approach*: Use two bitmask variables (`ones` and `twos`) to track cumulative modulo-3 bit parity states. Python implementation:
    ```python
    def single_number_ii(nums: list[int]) -> int:
        ones, twos = 0, 0
        for num in nums:
            ones = (ones ^ num) & ~twos
            twos = (twos ^ num) & ~ones
        return ones
    ```
*   **State Compression DP: Traveling Salesperson (TSP)**: Maintain the set of visited nodes as a bitmask index. For state `(mask, u)`, bit position `i` represents whether vertex `i` has been visited. Transitions take `O(1)` bitwise operations.

---

## 🛠️ Supplementary Material

### Practice Problems
1.  **Single Number** ([LeetCode 136](https://leetcode.com/problems/single-number/)): Find the unique element in an array where all other elements appear twice.
2.  **Number of 1 Bits** ([LeetCode 191](https://leetcode.com/problems/number-of-1-bits/)): Implement Brian Kernighan popcounting.
3.  **Reverse Bits** ([LeetCode 190](https://leetcode.com/problems/reverse-bits/)): Symmetrically swap the bits in a 32-bit integer.
4.  **Subsets** ([LeetCode 78](https://leetcode.com/problems/subsets/)): Enumerate all array subsets using integer bitmask shifts.
5.  **Gray Code** ([LeetCode 89](https://leetcode.com/problems/gray-code/)): Generate a sequence of Gray codes from 0 to 2^N - 1.

### Misconceptions and Corrections
*   *Incorrect Idea*: Assuming that checking `(n & (n - 1)) == 0` is sufficient to prove that `n` is a power of two.
    *   *Correction*: If `n == 0`, `n & (n - 1)` evaluates to `0` even though `0` is not a power of 2. For negative powers of two in signed integers (e.g., `-2147483648`), behavior varies. You must explicitly verify that `n > 0`.
*   *Incorrect Idea*: Assuming Python's `~x` behaves identically to C#'s `~x` on 32-bit registers.
    *   *Correction*: Python integers have unbounded precision. Inverting bit 0 of 0 yields `-1` (infinite leading ones) rather than `0xFFFFFFFF`. Mask with `& 0xFFFFFFFF` to clamp to 32 bits.

---

> 🧭 **Navigation:** [← Previous Day](Week_14_Day_01_Matrix_Operations_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_14_Day_03_Number_Theory_Basics_Instructional.md)
