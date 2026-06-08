# 📝 Week 14 Summary & Key Concepts

This document summarizes the core mathematical and algorithmic concepts covered in Week 14.

---

## 📅 Day 1: Matrices & Coordinate Shuffling
*   **Transpose**: Swapping indices symmetrically across the primary diagonal (M[i][j] <=ftrightarrow M[j][i]).
*   **In-Place Clockwise Rotation**: Execute a diagonal transpose followed by a horizontal row reversal. This rotates a square matrix 90 degrees with zero allocations.
*   **Spiral Walk boundary pointer tracking**: Maintain four boundary pointers (`top`, `bottom`, `left`, `right`) to traverse concentric boundaries safely.

---

## 📅 Day 2: Bitwise Registers & Integer Set Masks
*   **Bit Masking**: Checking, setting, clearing, and toggling specific bit flag variables.
*   **Isolate lowest set bit**: Use Two's Complement subtraction properties (`n & -n`) to isolate the lowest set bit.
*   **Brian Kernighan popcounting**: Count set bits in O({set bits}) steps by repeatedly clearing the lowest set bit: `n &= n - 1`.
*   **Submask Decrement iterations**: Enumerate all subsets (submasks) of a given bitmask in decreasing order: `sub = (sub - 1) & mask`.

---

## 📅 Day 3: Primes Factoring & Modular Inverse Powers
*   **Greatest Common Divisor (GCD)**: The Euclidean greatest common divisor (GCD) algorithm replaces the larger number with its modular remainder recursively: gcd(a, b) = gcd(b, a \bmod b).
*   **Sieve of Eratosthenes**: Mark multiples of primes starting from p^2 to find all prime numbers up to N.
*   **Logarithmic exponentiation**: Square the base and halve the exponent in each step to calculate modular exponents in O(log e) time.
*   **Modular Multiplicative Inverse**: Compute the modular inverse of a number modulo a prime using Fermat's Little Theorem: A^{-1} == A^{P-2} +/-od P.

---

## 📅 Day 4: KMP Fallback Jumps & Prefix Trees
*   **LPS Table**: Precompute fallback indexes matching proper prefixes of a pattern to its suffixes.
*   **KMP Single Pattern Matching**: Scan text streams linearly without backing up the search pointer by stepping fallback states based on the LPS table.
*   **Trie**: Merge shared prefixes of multiple words into a single search tree to support fast prefix matching.
*   **Radix Sorting**: Sort elements stably without comparing keys directly by sorting digit columns or characters sequentially.

---

## 📅 Day 5: Advanced Totients & Modulo Solvers
*   **Euler's Totient phi(N)**: Count coprime integers up to N using the prime factor allocation product formula.
*   **Chinese Remainder Theorem**: Find a unique, high-precision solution for a system of multiple congruent mod equation channels.
