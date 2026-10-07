# Antigravity Project Instructions: FAANG Senior / Lead Curriculum

Welcome to the **DSA, LLD, HLD & Scaled System Architecture** repository.
This repository is engineered to train Senior, Lead, and Staff Software Engineers for FAANG and Tier-1 product interviews.

---

## 👤 Learner Profile & Persona
- **Role:** Senior Full Stack Software Engineer / Technical Lead
- **Primary Tech Stack:** .NET (C# / ASP.NET Core), Angular, Cloud Architecture (Azure & AWS), Microservices, Distributed Systems.
- **DSA Languages:**
  - **Primary:** **C#** (.NET 8/9, `Span<T>`, `Memory<T>`, `PriorityQueue<TElement, TPriority>`, `ref struct`, pattern matching, zero-allocation memory awareness).
  - **Secondary:** **Python** (Python 3.11+, `collections.deque`, `heapq`, `bisect`, type hints, list comprehensions, rapid whiteboard prototyping).
- **Preparation Goals:** FAANG / Tier-1 Senior & Staff Software Engineering interviews covering:
  1. Data Structures & Algorithms (Invariant-first derivation)
  2. Low-Level Design (LLD / OOD / Clean Architecture / Design Patterns / Concurrency)
  3. High-Level Design (HLD / Distributed Systems / Scalability / Reliability / Cloud Native on Azure & AWS)
  4. Production Engineering & Architecture Trade-offs

---

## 🏛️ Senior Algorithmic Protocol (Intuitive 8-Step Delivery Arc)

Whenever analyzing, writing, or tutoring a DSA problem, **follow the Intuitive 8-Step Delivery Arc**:

```mermaid
flowchart LR
    S1["1. Clarify & Bounds"] --> S2["2. Naive Baseline"]
    S2 --> S3["3. Spot Bottleneck"]
    S3 --> S4["4. Intuitive Hook & Pattern"]
    S4 --> S5["5. Visual Proof & Why It Works"]
    S5 --> S6["6. Explicit Complexity"]
    S6 --> S7["7. Idiomatic Code (C# & Python)"]
    S7 --> S8["8. Edge-Case Dry Run"]
```

1. **Clarify Inputs, Boundaries & Contracts:** Establish constraints (N, value ranges, nullability, duplicates, overflow, concurrency requirements).
2. **Formulate Naive Baseline:** State the brute-force time and space complexity to anchor the discussion simply and clearly.
3. **Identify Computational Bottleneck:** Point out repeated work, redundant recalculation, or unindexed lookups in plain English.
4. **Intuitive Hook & Pattern Selection:** Introduce the core intuition using a physical analogy (e.g. calipers, window sliding, book bookmarks) and name the underlying pattern/data structure without academic jargon.
5. **Visual Proof & Why It Works:** Walk through a visual trace with real numbers showing why candidates can be safely skipped and why the algorithm terminates.
6. **Explicit Complexity Deconstruction:** Always differentiate:
   - **Time Complexity** (`O(N)`, `O(N log N)`, etc.)
   - **Auxiliary Space** (internal working memory: pointers, hash tables, stacks)
   - **Output Space** (returned structures, if any)
7. **Idiomatic Code:**
   - **C# Primary:** Modern, clean, production-grade .NET with overflow guards, guard clauses, and proper data structures.
   - **Python Secondary:** Clean, pythonic, idiomatic implementation for rapid syntax and polyglot versatility.
8. **Concrete Edge-Case Dry Run:** Trace edge cases (e.g., empty collection, single element, duplicates, all negative, extreme bounds, parity issues).

---

## 📁 Repository Directory Taxonomy

- `Senior_problem_solution/`: The primary reference encyclopedia of 164 senior DSA problems across 25 phases.
- `Senior_dsa_question_list.md`: Master question inventory, ROI ranking matrix, and sprint schedules.
- `LLD/`: Object-Oriented Design, Low-Level Design patterns, domain models, and thread-safe in-memory systems.
- `system-design/`: High-Level Design (HLD) deep dives, distributed systems architectures, and enterprise patterns.
- `Coding_Practice/`: Hands-on workspace for active coding, test-driven drills, and unit test suites.
- `learning_tracking/`: Progress tracking logs (`Practice_Log.md`, `Question_Bank.md`, `Weekly_Review.md`, `Mock_Interview_Log.md`).
- `week_01` to `week_19`: Longitudinal foundational and deep-dive weekly curriculums.
- `Old/`: **Historical / archived content**. Do NOT edit or use as active context unless explicitly requested by the user.

---

## ⚙️ Antigravity Operating Guidelines
- **Strictly GitHub Markdown (Zero LaTeX Math):** All curriculum guides, solutions, and responses are formatted strictly for GitHub Markdown. Never use LaTeX syntax (`$...$`, `$$...$$`, `\(...\)`, `\[...\]`, KaTeX, or directives like `\frac`, `\to`, `\le`, `\times`, etc.) anywhere. Express all math, complexity bounds, constraints, and notations using standard Markdown backticks or plain text (e.g., `O(N)`, `O(N log N)`, `O(1)`, `N <= 10^5`, `10^5`, `[0 ... N - 1]`, `A -> B`, `i != j`, `x * y`).
- **Visual Pragmatism (Mermaid vs. ASCII vs. Simple Steps & Tables):**
  - **When to Use Mermaid:** Use Mermaid ONLY where visual logic, state transitions, branching decisions, or true hierarchical structures truly clarify understanding:
    - High-level decision trees & branching flowcharts (`flowchart TD` / `flowchart LR` with diamonds `{...}` and labeled branches `-->|Yes|`).
    - True hierarchical tree and graph topologies (Binary Search Trees, AVL/Red-Black trees, Tries, DFS/BFS graphs).
    - Multi-entity sequences (`sequenceDiagram`) or state machines (`stateDiagram-v2`).
  - **Strictly FORBIDDEN Mermaid Anti-Patterns:**
    - **No Tree/Star Sprawl on Linear Steps:** NEVER convert linear step-by-step algorithms, sequential dry-runs, or execution traces into artificial star/tree graphs (e.g. `R --> N1`, `R --> N2`, `R --> N3...`).
    - **No Mind Maps / Tree Outlines in Mermaid:** Do NOT turn outlines, category lists, or bullet points into tree-shaped Mermaid boxes. Use clean Markdown headers and bullet lists instead.
    - **No Tree Graphs for Failure Modes / Tips:** Do NOT create `R["WRONG"] --> Tip1, Tip2, Tip3`. Use clear warning alerts (`> [!WARNING]`), bullet points, and before-and-after code blocks (`❌ WRONG` vs `✅ CORRECT`).
  - **When to Use ASCII Diagrams:**
    - Linear arrays with pointer indices and boundary markers (e.g., `[ 2 | 7 | 11 | 15 ]`, `  L          R  `).
    - Contiguous memory layouts, linked list node chains (`[Val|Next] -> [Val|Next]`), and stack frames (`| Top |` over `| Base |`). Text/ASCII blocks are vastly more compact, instant to parse, and immune to layout clipping.
  - **When to Use Simple Sequential Steps & Trace Tables:**
    - Dry-runs and step-by-step execution traces: Use clean numbered steps (`Step 1: ...`, `Step 2: ...`) or structured markdown trace tables (`| Step | Element | Stack / Window | Action |`).
  - **Mermaid Quality & Styling Standards (When Used):**
    - **Simplified Complexity & Node Depth:** Keep diagrams focused (under 8–10 nodes per visual) and cognitive load light.
    - **Visually Appealing Colors & High Contrast:** Apply modern hex fills with sharp text contrast (e.g., `style Node fill:#e1f5fe,stroke:#0288d1,color:#01579b`).
    - **Overflow Prevention & Safe Text:** Always wrap node labels in double quotes (e.g., `id["Label Text"]`) and keep label lengths concise.
    - **Learner-Friendly Emojis & Icons:** Enhance visual hierarchy with intuitive emojis in node labels (e.g., `📦 Array`, `👉 Left`, `👈 Right`, `🎯 Target`, `⚡ Fast`, `🐢 Slow`, `🛑 Base`, `✅ Valid`, `❌ Pruned`, `🔍 Window`).
- **Remove Non-Learning Metadata & Irrelevant Comparisons:**
  - **Strip External Tool Dumps:** Remove generic lists of external tools (e.g., VisuAlgo, LeetCode Visualizer, Python Tutor, Excalidraw, Mermaid Live Editor, Big-O Cheat Sheet).
  - **Remove Superficial Comparisons:** Remove generic comparison matrices, textbook trivia, or metadata sections that do not directly help in a 45-minute FAANG interview.
  - Keep comparisons strictly focused on high-ROI algorithmic trade-offs (e.g., Two Pointers vs. Hash Table vs. Binary Search on Answer).
- **Web Search for Audit, Verification & Generation:** Actively leverage web search (`search_web`) to verify LeetCode problem numbers, check current FAANG interview constraints, cross-reference boundary conditions, and pull real test cases during material audits and generation.
- **Intuitive & Beginner-Friendly Pedagogy:** Avoid unnecessary academic jargon, formal mathematical proofs, and abstract textbook theorems. Use physical metaphors, conversational explanations, and progressive scaffolding (Intuition -> Napkin Trace -> Warm-up -> Classic FAANG Medium -> Edge Cases). Ensure content is accessible to a motivated beginner while reaching FAANG interview standards.
- **No Copilot Diff Markers:** Never emit `// filepath: ...` or `// ...existing code...` comments. Antigravity uses native workspace editing tools directly.
- **Linear & Practice-Ready:** When explaining or building exercises, present concepts sequentially. Avoid fragmented notes, cognitive leaps, or circular jumps.
- **Dual-Language Fluency:** Always provide C# as the primary, production-ready solution, and accompany it with a clean Python counterpart.
- **FAANG Interview Pragmatism:** Focus on what is tested in actual 45-minute live technical interviews (problem clarification, communicating thought process, clean code, time/space trade-offs, edge-case testing).



