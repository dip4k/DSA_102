# ✅ Week 14 Daily Progress Checklist

Track your progress for each study day. Verify that you understand the concepts and can implement the algorithms before moving forward.

---

## 📅 Day 1: Matrices & Coordinate Shuffling
*   **Theory Check**:
    *   [ ] Explain the cache benefits of looping over sequential columns (row-major order) rather than vertical rows in contiguous memory.
    *   [ ] Explain why transposing a matrix and then reversing its rows rotates it 90 degrees clockwise.
*   **Implementation Check**:
    *   [ ] Implement `Transpose` in-place on a square N x N matrix.
    *   [ ] Implement `RotateClockwise90` in-place on a square N x N matrix.
    *   [ ] Implement `SpiralOrder` boundary tracking on a rectangular matrix.
*   **Error Prevention Check**:
    *   [ ] Verify that your transpose loop only swaps elements above the diagonal (j > i) to avoid double-swabbing cells.

---

## 📅 Day 2: Bitwise Registers & Integer Set Masks
*   **Theory Check**:
    *   [ ] Explain the difference between `&` (intersection), `|` (union), and `^` (parity detection).
    *   [ ] Explain Two's Complement subtraction carry propagation and how it enables `n & -n`.
*   **Implementation Check**:
    *   [ ] Implement `IsPowerOfTwo(n)` with positivity bounds checks.
    *   [ ] Implement Brian Kernighan popcounting to count set bits in O({set bits}) steps.
    *   [ ] Implement a submask loop that systemically decrements an integer set-mask in decreasing order: `sub = (sub - 1) & mask`.
*   **Error Prevention Check**:
    *   [ ] Use unsigned integer types (`uint` or `ulong` in C#) for high-order bit operations to prevent sign bit anomalies under Two's Complement representation.

---

## 📅 Day 3: Primes Factoring & Modular Inverse Powers
*   **Theory Check**:
    *   [ ] Explain why direct division is not defined inside modular expressions and how to resolve `(A / B) % m`.
    *   [ ] Explain the difference in modular inverse requirements for prime moduli (Fermat) vs. composite moduli (Extended Euclidean).
*   **Implementation Check**:
    *   [ ] Implement the Euclidean Greatest Common Divisor recursively in logarithmic time.
    *   [ ] Implement the Sieve of Eratosthenes up to N.
    *   [ ] Implement fast modular exponentiation in O(log e) time using binary decomposition.
*   **Error Prevention Check**:
    *   [ ] Perform intermediate calculations using 64-bit integer types (`long` in C#) to prevent arithmetic overflows.

---

## 📅 Day 4: KMP Fallback Jumps & Prefix Trees
*   **Theory Check**:
    *   [ ] Explain why searching a pattern inside a text stream does not require backing up the main text pointer in the KMP algorithm.
    *   [ ] Compare the performance and capabilities of KMP (single pattern) vs. Aho-Corasick (multi-pattern dictionary).
*   **Implementation Check**:
    *   [ ] Build the Longest Prefix Suffix (LPS) helper table for KMP.
    *   [ ] Implement a complete KMP string scan that returns all 0-based starting indexes.
    *   [ ] Implement a character Trie supporting insertion, search, and prefix matching.
*   **Error Prevention Check**:
    *   [ ] Add safety guards to handle empty patterns and repetitive texts cleanly.

---

## 📅 Day 5: Advanced Totients & Modulo Solvers
*   **Theory Check**:
    *   [ ] Define Euler's Totient function phi(N) and explain how it determines coprime counts.
    *   [ ] Describe a Chinese Remainder Theorem modulus mapping and how it divides high-precision tasks.
*   **Implementation Check**:
    *   [ ] Implement an O(sqrt(N)) totient function using prime factorization.
    *   [ ] Build a Chinese Remainder Theorem (CRT) solver for K modular equations with pairwise coprime moduli.
*   **Error Prevention Check**:
    *   [ ] Verify that all moduli are pairwise coprime before starting your CRT engine to ensure consistent systems have unique solutions.
