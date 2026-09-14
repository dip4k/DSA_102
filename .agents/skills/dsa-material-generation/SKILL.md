---
name: dsa-material-generation
description: Generate or refine DSA curriculum files with rigorous invariant derivation, dual-language (C# & Python) implementations, and senior-level depth.
---

# DSA Material Generation Skill

## Use When
- Generating or updating solution guides in `Senior_problem_solution/` or week curriculum folders.
- Creating dual-language (C# & Python) algorithmic analyses.
- Synthesizing pattern summaries, visual representations, or interview Q&A.

## Content Requirements
1. **8-Step Delivery Arc:** Every problem must follow the 8-step framework (Clarify -> Naive Baseline -> Bottleneck -> Invariant -> Proof -> Complexity -> Idiomatic C# & Python -> Edge-Case Dry Run).
2. **Dual-Language Fluency:**
   - C# (.NET 8/9, `PriorityQueue`, `Span<T>`, overflow safety).
   - Python (Python 3.11+, typed, idiomatic).
3. **Mathematical Invariant Rigor:** State explicit loop invariants and discard-safety proofs.
4. **Complexity Breakdown:** Deconstruct into Time, Auxiliary Space, and Output Space.
5. **Strictly No LaTeX:** Never emit LaTeX math syntax (`$...$`, `$$...$$`, `\(...\)`, `\[...\]`, `\to`, `\le`, `\times`, etc.). Express all time/space complexities, constraints, mathematical formulas, and notations using plain text or Markdown code backticks (e.g., `O(N log N)`, `N <= 10^5`, `10^5`, `->`, `!=`).

