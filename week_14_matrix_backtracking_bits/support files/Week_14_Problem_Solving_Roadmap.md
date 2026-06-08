# 🗺️ Week 14 Problem Solving Roadmap

Use this guide to determine which specialized technique applies to a given problem.

---

## 🧭 Decision Matrix

```mermaid
graph TD
    A[Analyze Problem Statement] --> B{Is the search space a 2D grid?}
    B -->|Yes| C{Is it sorting or traversing?}
    C -->|Traversing| D[Use Spiral/Boundary scans or Row-Major iterations]
    C -->|Transforming| E[Use in-place Transpose & flips]
    C -->|Searching| F[Use Staircase Search or Matrix DP]
    B -->|No| G{Does it track combinations, subsets, or states?}
    G -->|Yes| H{Is the size of the set <= 64?}
    H -->|Yes| I[Use Bitwise Masks, Bit Tricks, or Submask Loops]
    H -->|No| J[Use standard sets/maps, Backtracking, or DFS]
    G -->|No| K{Does it use prime numbers, modular results, or divisibility?}
    K -->|Yes| L[Use GCD, Sieve, fast exponents, or CRT modular inverses]
    K -->|No| M{Is it substring searching or prefix matching?}
    M -->|Yes| N[Use KMP fallback matching, Tries, or Radix sorting]
```

---

## 🎯 Quick Reference Selection Guide

*   *If the problem is "Rotate square grid in-place"*: Use transposition followed by axial flips (O(N^2) time, O(1) space).
*   *If the problem is "Check power of 2"*: Use `n > 0 &&_ (n & (n - 1)) == 0`.
*   *If the problem is "Enumerate all subset states"*: Loop through masks using submask algebraic decrements (`(sub - 1) & mask`).
*   *If the problem is "Find modular inverse of a prime modulus"*: Use fast exponentiation with Fermat's exponent: `ModPow(A, P-2, P)`.
*   *If the problem is "Find single pattern in stream without backtracking"*: Precompute the LPS table and run a KMP search (O(N + M) time).
*   *If the problem is "Solve systems of simultaneous mod equations"*: Verify that the moduli are coprime, then use the Chinese Remainder Theorem.
