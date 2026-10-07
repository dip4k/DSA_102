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
1. **Intuitive 8-Step Delivery Arc:** Every problem must follow the intuitive framework (Clarify & Bounds -> Naive Baseline -> Spot Bottleneck -> Intuitive Hook & Pattern -> Visual Proof -> Explicit Complexity -> Clean C# & Python -> Edge-Case Dry Run).
2. **Dual-Language Fluency:**
   - C# (.NET 8/9, `PriorityQueue`, `Span<T>`, overflow safety, zero-allocation awareness).
   - Python (Python 3.11+, typed, idiomatic, concise).
3. **Intuitive Invariant & Physical Mental Models:** Explain the core invariant in plain English with everyday metaphors rather than dense academic jargon.
4. **Complexity Breakdown:** Deconstruct into Time Complexity, Auxiliary Space, and Output Space.
5. **Strictly GitHub Markdown (Zero LaTeX):** Never emit LaTeX math syntax (`$...$`, `$$...$$`, `\(...\)`, `\[...\]`, `\to`, `\le`, `\times`, etc.). Express all time/space complexities, constraints, mathematical formulas, and notations using Markdown code backticks or plain text (e.g., `O(N log N)`, `N <= 10^5`, `10^5`, `->`, `!=`).
6. **Pragmatic Visual Selection (Mermaid, ASCII, Steps & Tables):**
   - Use Mermaid ONLY where visual logic, state transitions, branching decisions, or true hierarchical structures truly clarify understanding (branching flowcharts, BST/AVL/Trie/graph structures, sequence diagrams).
   - NEVER create artificial tree/star sprawl (`R --> N1, N2...`) for linear traces, sequential steps, or failure mode tips.
   - Use compact ASCII diagrams for arrays, pointer alignments (`L` / `R`), and memory buffers.
   - Use clean numbered steps or Markdown trace tables for dry runs and execution walks.
7. **Noise Reduction & No Non-Learning Metadata:** Strip out external tool link matrices (VisuAlgo, Excalidraw, etc.) and textbook trivia comparisons that don't directly serve a 45-minute FAANG interview.
8. **Web Search Verification:** Use `search_web` to cross-check LeetCode numbers, test cases, and constraints against authoritative sources.



