# 🗺️ Week 14 Problem Solving Roadmap

> 🧭 **Navigation:** [← Back to Week Overview](../README.md) • [📘 Complete Syllabus](../../COMPLETE_SYLLABUS.md)
> 
> 💡 **Instructor Note:** *Support files are modular reference tools. Use them flexibly to self-assess, review key formulas, or prepare for verbal interviews.*

---

Use this guide to determine which specialized technique applies to a given problem.

---

## 🧭 Decision Matrix

```mermaid
%%{init: {"flowchart": {"htmlLabels": true, "padding": 20}}}%%
graph TD
    A[Analyze Problem Statement] --> B{"Is the search space a 2D<br/>grid?"}
    B -->|Yes| C{"Is it sorting or<br/>traversing?"}
    C -->|Traversing| D["Use Spiral/Boundary scans<br/>or Row-Major iterations"]
    C -->|Transforming| E["Use in-place Transpose &<br/>flips"]
    C -->|Searching| F["Use Staircase Search or<br/>Matrix DP"]
    B -->|No| G{"Does it track<br/>combinations, subsets, or<br/>states?"}
    G -->|Yes| H{"Is the size of the set<br/>&le; 64?"}
    H -->|Yes| I["Use Bitwise Masks, Bit<br/>Tricks, or Submask Loops"]
    H -->|No| J["Use standard sets/maps,<br/>Backtracking, or DFS"]
    G -->|No| K{"Does it use prime<br/>numbers, modular results,<br/>or divisibility?"}
    K -->|Yes| L["Use GCD, Sieve, fast<br/>exponents, or CRT modular<br/>inverses"]
    K -->|No| M{"Is it substring searching<br/>or prefix matching?"}
    M -->|Yes| N["Use KMP fallback<br/>matching, Tries, or Radix<br/>sorting"]
```

---

## 🎯 Quick Reference Selection Guide

*   *If the problem is "Rotate square grid in-place"*: Use transposition followed by axial flips (O(N^2) time, O(1) space).
*   *If the problem is "Check power of 2"*: Use `n > 0 &&_ (n & (n - 1)) == 0`.
*   *If the problem is "Enumerate all subset states"*: Loop through masks using submask algebraic decrements (`(sub - 1) & mask`).
*   *If the problem is "Find modular inverse of a prime modulus"*: Use fast exponentiation with Fermat's exponent: `ModPow(A, P-2, P)`.
*   *If the problem is "Find single pattern in stream without backtracking"*: Precompute the LPS table and run a KMP search (O(N + M) time).
*   *If the problem is "Solve systems of simultaneous mod equations"*: Verify that the moduli are coprime, then use the Chinese Remainder Theorem.

---

> 🧭 **Navigation:** [← Back to Week Overview](../README.md) • [📘 Complete Syllabus](../../COMPLETE_SYLLABUS.md)
