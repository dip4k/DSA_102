# 🎤 Week 14 Interview Q&A v13: Matrices, Bitmasks & Number Theory

Welcome to the Week 14 Interview Q&A guide. This document contains high-frequency interview questions, architectural drill-downs, and communication answers designed to help you explain low-level mathematical structures, string preprocessing state transitions, and grid transformations.

---

### 🧮 Part 1: Matrix Operations & Grid Manipulations

#### Q1. How do you rotate a square matrix 90 degrees counter-clockwise in-place?
**Verbal Answer**:
"To rotate an N x N matrix 90 degrees counter-clockwise in-place, we can combine two simpler transformations:
1.  First, reverse each row individually.
2.  Second, transpose the matrix symmetrically along its main diagonal (M[i][j] <=ftrightarrow M[j][i] where j > i).
Alternatively, we can reverse the columns then transpose, or transpose first then reverse each column. Composing these operations is simpler and less prone to off-by-one errors than tracking complex coordinate mapping indices in a single traversal."

**Technical Evaluation**:
*   **Time Complexity**: O(N^2) because we must visit each cell in the matrix twice.
*   **Space Complexity**: O(1) auxiliary space because elements are swapped in-place.

---

#### Q2. Explain how to set, clear, and toggle a specific cell during high-frequency matrix state searches.
**Verbal Answer**:
"In search algorithms like backtracking, we often need to mark cells as visited. If memory is a constraint, we can pack row states into an integer bitmask.
For a grid of width W, the flat index of a cell is calculated as:
{flat\_index} = {row} x W + {col}
*   **To check the state**: `(visitedBitmask & (1L << flat_index)) != 0`
*   **To enable/set the state**: `visitedBitmask |= (1L << flat_index)`
*   **To clear/restore the state**: `visitedBitmask &= ~(1L << flat_index)`
*   **To toggle the state**: `visitedBitmask ^= (1L << flat_index)`
This approach takes constant O(1) time and avoids heap allocations inside search loops."

---

#### Q3. How do you search for an element in an M x N matrix where both the rows and columns are sorted?
**Verbal Answer**:
"We can solve this efficiently in linear time using a **Staircase Search** (often called the saddle-back search).
Instead of searching from the top-left, we start at either the **top-right** (index `(0, N-1)`) or the **bottom-left** (index `(M-1, 0)`).
Starting at the top-right `(0, N-1)`:
*   If our target is smaller than the current element, we can safely eliminate the entire current column and move **left** (`col--`).
*   If our target is larger, we can safely eliminate the entire current row and move **down** (`row++`).
Repeat this process until we either find the element or move outside the grid boundaries."

**Complexity**:
*   **Time**: O(M + N) worst-case.
*   **Space**: O(1) auxiliary space.

---

### 🔌 Part 2: Bitwise Manipulation & State Compactness

#### Q4. Why is checking `(n & (n - 1)) == 0` insufficient to prove `n` is a power of 2?
**Verbal Answer**:
"The bit operation `n & (n - 1)` clears the rightmost set bit. If the result is `0`, it indicates that the number has at most one set bit.
However, this check is insufficient on its own for two reasons:
1.  **Zero Value**: If `n` is `0`, `n & (n - 1)` evaluates to `0` even though `0` is not a power of 2.
2.  **Negatives**: In languages with signed integers, negative numbers can cause unexpected behavior depending on Two's Complement layout.
To make this check safe, we must explicitly verify that `n` is strictly positive:
```csharp
bool isPowerOfTwo = n > 0 &&_ (n & (n - 1)) == 0;
```"

---

#### Q5. How does Brian Kernighan's population count algorithm operate, and why does it beat naive bit shifting?
**Verbal Answer**:
"A naive popcount algorithm shifts the bits of an integer one by one, checking all 32 or 64 positions. This always takes O({word\_size}) operations.
**Brian Kernighan's algorithm** is faster because it executes exactly once per active set bit. It uses the property that subtracting 1 from a number flips all bits starting from its rightmost set bit:
n - 1 == {flips bits up to the rightmost 1-bit}
Therefore, performing n \ {AND}\ (n-1) clears the lowest set bit in exactly one cycle:
```csharp
int count = 0;
while (n > 0) {
    n &= (n - 1); // Clears the lowest set bit
    count++;
}
```"

**Performance**:
*   **Asymptotic Cost**: O({set bits}) steps instead of O({bit width}). In sparse masks, this is extremely fast.

---

### 📐 Part 3: Number Theory & Congruences

#### Q6. What is the modular multiplicative inverse, when does it exist, and how is it used to divide inside a modular expression?
**Verbal Answer**:
"In modular arithmetic, division is not defined directly because remainders must be integers. To perform division, we multiply the numerator by the **modular multiplicative inverse** of the denominator.
The modular inverse of B modulo M is an integer x such that:
B x x == 1 +/-od M
This inverse exists if and only if B and M are coprime:
gcd(B, M) = 1
If M is prime, we can use **Fermat's Little Theorem** to calculate the inverse in logarithmic time:
A^{-1} == A^{M-2} +/-od M
If M is not prime, we must use the **Extended Euclidean Algorithm** to calculate the inverse."

---

#### Q7. Describe the Chinese Remainder Theorem (CRT) and how it parallelizes huge arithmetic operations.
**Verbal Answer**:
"The Chinese Remainder Theorem states that if we have a system of congruence equations modulo pairwise coprime integers m_1, m_2, \dots, m_k, there exists a unique solution x modulo their product M = product m_i.
In high-precision computing, we can partition calculations on extremely large numbers by running them in parallel modulo smaller, coprime moduli. We then reconstruct the final high-precision result using the CRT formula:
x = sum(a_i * M_i * y_i) (mod M)
This speeds up execution because operations on smaller integer types run directly in hardware registers, avoiding the high cost of arbitrary-precision software structures."

---

### 🧵 Part 4: Advanced Strings & Pattern Compilations

#### Q8. What is the distinction between KMP and Aho-Corasick?
**Verbal Answer**:
"Both KMP and Aho-Corasick perform string matching in linear time without backtracking, but they differ in scale:
*   **KMP (Knuth-Morris-Pratt)**: Matches a **single** pattern P of length M against a streaming text T of length N. It uses a 1D precomputed table (LPS table) representing loop fallback points.
*   **Aho-Corasick**: Matches **multiple** patterns simultaneously. It compiles a dictionary of words into a single Trie, then adds failure links to connect mismatch points to the longest valid suffix traversed so far.
Aho-Corasick can scan a streaming text for thousands of words simultaneously in a single linear pass."

---

#### Q9. Why is a standard Trie memory-heavy, and how do real-world search engines optimize its footprint?
**Verbal Answer**:
"A standard Trie is memory-heavy because each tree node contains pointers to its child nodes (e.g. 26 pointers for lowercase English, or a dynamic hash map). Each pointer takes 8 bytes on 64-bit systems, and node metadata adds significant overhead, resulting in poor cache performance.
Real-world systems use two optimization techniques:
1.  **Compressed Tries (Radix Trees)**: Merges adjacent nodes with single children into a single string path, reducing the total node count.
2.  **Double-Array Tries**: Compares and packs node transitions into two parallel arrays (`base` and `check`). This represents the entire Trie as a flat memory space, replacing pointer tree walks with fast index lookups."
