# ✅ Week 14 Daily Progress Checklist v13: Matrices, Bitmasks & Number Theory

Use this daily checklist to verify your learning and ensure your implementation skills are interview-ready. Tick off each milestone once you can perform the task successfully.

---

## 📅 Day 1: Matrices & Coordinate Shuffling
*   **Concepts & Theory**:
    *   [ ] Explain why composing a Transpose step with a Row Reversal rotates a square matrix clockwise 90 degrees.
    *   [ ] Explain the cache benefits of looping over sequential columns (row-major order) rather than vertical rows in contiguous memory.
*   **Coding & Mechanics**:
    *   [ ] Implement `Transpose` in-place on an N x N matrix.
    *   [ ] Implement `RotateClockwise90` in-place on an N x N matrix.
    *   [ ] Implement `SpiralOrder` boundary tracking on a rectangular matrix with no boundary drift.
*   **Invariants & Edge Cases**:
    *   [ ] Test your spiral walker on flat 1 x N and N x 1 matrices.
    *   [ ] Symmetrically loop indices only where j > i during transposition to avoid double-swapping cells.

---

## 📅 Day 2: Bitwise Registers & Integer Set Masks
*   **Concepts & Theory**:
    *   [ ] Explain the difference between `&` (intersection), `|` (union), and `^` (parity detection).
    *   [ ] Explain Two's Complement subtraction carry propagation and how it enables `n & -n`.
*   **Coding & Mechanics**:
    *   [ ] Implement `IsPowerOfTwo(n)` with bounds checks for non-positive boundaries.
    *   [ ] Implement Brian Kernighan popcounting to count active bits in O({set bits}) steps.
    *   [ ] Write a submask loop that systemically decrements an integer set-mask in decreasing order: `sub = (sub - 1) & mask`.
    *   [ ] Generate a sequence of Gray codes from 0 to 2^N - 1.
*   **Invariants & Edge Cases**:
    *   [ ] Check for overflow traps by using unsigned parameters (`uint` or `ulong`) when shifting the 31st or 63rd bit.

---

## 📅 Day 3: Primes Factoring & Modular Inverse powers
*   **Concepts & Theory**:
    *   [ ] Explain why direct division is not defined inside modular expressions and how to resolve `(A / B) % m`.
    *   [ ] Explain the difference in modular inverse requirements for prime moduli (Fermat) vs. composite moduli (Extended Euclidean).
*   **Coding & Mechanics**:
    *   [ ] Implement the Euclidean Greatest Common Divisor recursively in logarithmic time.
    *   [ ] Implement the Sieve of Eratosthenes up to N.
    *   [ ] Implement fast modular exponentiation in O(log e) time using binary decomposition.
    *   [ ] Build a modular multiplier that supports multiplicative inverse calculations: `(A * ModInverse(B, P)) % P`.
*   **Invariants & Edge Cases**:
    *   [ ] Add safety guards to handle cases where divisor `B` or modulus `M` is `0`.

---

## 📅 Day 4: KMP Fallback Jumps & Prefix Trees
*   **Concepts & Theory**:
    *   [ ] Explain why searching a pattern inside a text stream does not require backing up the main text pointer in the KMP algorithm.
    *   [ ] Compare the capabilities of KMP (single pattern) vs. Aho-Corasick (multi-pattern dictionary).
*   **Coding & Mechanics**:
    *   [ ] Build the Longest Prefix Suffix (LPS) helper table for KMP.
    *   [ ] Implement a complete KMP string scan that returns all 0-based starting indexes.
    *   [ ] Implement a character Trie supporting insertion, search, and prefix matching.
*   **Invariants & Edge Cases**:
    *   [ ] Validate KMP matching on empty patterns and highly repetitive texts (e.g. text: "AAAAAA", pattern: "AA").

---

## 📅 Day 5: Advanced Totients & Modulo Solvers
*   **Concepts & Theory**:
    *   [ ] Define Euler's Totient function phi(N) and explain how it determines coprime counts.
    *   [ ] Describe a Chinese Remainder Theorem modulus mapping and how it divides high-precision tasks.
*   **Coding & Mechanics**:
    *   [ ] Implement an O(sqrt(N)) totient function using prime factorization.
    *   [ ] Build a Chinese Remainder Theorem (CRT) solver for K modular equations with pairwise coprime moduli.
*   **Invariants & Edge Cases**:
    *   [ ] Test your CRT solver to ensure it handles non-coprime input and identifies inconsistent systems.
