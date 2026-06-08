# 📅 Week 14, Day 2: Basic Bitwise Operations, Tricks, Subset Enumeration & Gray Codes

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
*   Read (`R = 4` arrow `100`), Write (`W = 2` arrow `010`), and Execute (`X = 1` arrow `001`).
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
A naive population count algorithm shifts an integer bit by bit to count all set flags, which takes O({word\_size}) steps.
**Brian Kernighan's algorithm** is faster because it executes exactly once per active set bit. It uses the property that subtracting 1 from a number flips all bits starting from its rightmost set bit.
Therefore, performing n \ {AND}\ (n-1) clears the lowest set bit in exactly one cycle.

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

## 📘 Chapter 4: Performance and Systems

*   **Computational Cost**: Bitwise operations (`&`, `|`, `^`, `<<`) compile to single-cycle instructions on modern processors, making them faster than high-level conditional structures.
*   **Precision and Sign Bit Trap**: In 32-bit signed integers, shifting a 1 into index 31 alters the sign of the value because of Two's Complement representation. Always use `uint` (unsigned) or `long` (64-bit) structures when manipulating the most significant bits.

---

## 📘 Chapter 5: Integration and Mastery

### 1. Pattern Selection Rules
*   *Use Bit Operations when*: You need to track state membership across small sets (N <= 64) with absolute zero heap memory allocation.
*   *Use XOR Parity Checks when*: You need to find singular elements, mismatched pairs, or double configurations where duplicate pairs cancel out.

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
*   **State Compression DP: Traveling Salesperson (TSP)**: Maintain set of visited nodes as a bitmask index. For state (mask, current_node), bit position i represents if vertex i has been visited.

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
    *   *Correction*: If `n == 0`, `n & (n - 1)` evaluates to `0` even though `0` is not a power of 2. You must explicitly verify that `n > 0`.
