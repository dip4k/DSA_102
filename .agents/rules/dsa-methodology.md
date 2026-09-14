# Senior Algorithmic Methodology & Invariant Derivation

## The Senior / Lead Difference
Junior/Mid candidates memorize solutions; Senior/Lead candidates derive invariants and establish boundary contracts. Every problem solution must explain **why** an approach works mathematically before writing any code.

## The 8-Step Framework
1. **Clarify Inputs, Boundaries & Contracts:**
   - Empty input, N = 1, extreme scale (10^5 vs 10^9).
   - Can inputs be negative? Duplicates? Nulls?
2. **State Naive Baseline:**
   - Clear `O(N^2)`, `O(2^N)`, or `O(N!)` brute force.
3. **Identify Bottleneck:**
   - Pinpoint redundant scans, repeated state recalculation, or unindexed history.
4. **Select Invariant & Data Structure:**
   - Define the state preserved across every loop iteration or recursive call.
5. **Prove Correctness & Termination:**
   - Discard Safety: Why can elements be safely eliminated without missing the global optimum?
   - Termination Guarantee: Why do pointers move closer or states shrink on every step?
6. **Explicit Complexity Dimensions:**
   - Time Complexity (`O(N)`, `O(N log N)`, etc.)
   - Auxiliary Space (working memory excluding output)
   - Output Space
7. **Dual-Language Implementation:**
   - Primary: C# (.NET 8/9)
   - Secondary: Python (3.11+)
8. **Edge-Case Dry Run:**
   - Walk through a minimum of 2 concrete edge cases step-by-step with state tables.

## Formatting & Notation Standards
- **Strictly No LaTeX:** Never use LaTeX math delimiters (`$...$`, `$$...$$`, `\(...\)`, `\[...\]`) or LaTeX commands (`\frac`, `\to`, `\le`, etc.).
- **Pure Markdown & Code Formatting:** Always write time/space complexities, mathematical constraints, and invariants in standard Markdown backticks or plain text (e.g., `O(N)`, `O(N log N)`, `N <= 10^5`, `10^5`, `->`, `!=`, `<=`).

