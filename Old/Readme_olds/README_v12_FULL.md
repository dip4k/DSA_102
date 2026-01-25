# 📚 DSA Master Curriculum v12 – Narrative‑First, MIT‑Aligned

> **Status:** ✅ Production‑ready curriculum  
> **Version:** 12.1 (Aligned with `COMPLETE_SYLLABUS_v13_FINAL.md`)  
> **License:** 📜 MIT

This repository contains the **full DSA Master Curriculum v12** – a 19‑week, MIT‑style course in Data Structures & Algorithms designed to take you from **hardware‑level intuition** to **interview‑ready, production‑grade problem solving**.

The core idea: **teach DSA the way a senior engineer at Google/AWS who also lectures at MIT would teach it** – narrative‑first, visually, grounded in real systems, and organized around reusable patterns.

---

## 🎯 Core Philosophy – How This Course Thinks

- 🧠 **Mental Models First** – Always start with *why* and *how it behaves in memory*, not code templates.  
- 📖 **Narrative‑Driven** – Each instructional file reads like a chapter from a great technical book, **not** a checklist.  
- ⚙️ **Systems‑Grounded** – Concepts are tied to **real systems**: Linux kernel, PostgreSQL, Redis, compilers, caches, distributed systems.  
- 🧩 **Pattern‑Centric** – Problems are grouped into **patterns** (two pointers, BFS/DFS, DP, greedy, etc.), so you recognize signals quickly.  
- 💡 **Understanding Before Code** – Code is the *final expression* of a clear mental model; you should be able to explain the idea on a whiteboard first.  
- 🎨 **Visual by Default** – ASCII diagrams, trace tables, and concept maps are integrated **inline**, exactly where a reader needs them.

This aligns explicitly with **MIT 6.006 & 6.046**:

- Core 6.006 topics (RAM model, sorting, heaps, hashing, graphs, DP) → **core weeks**.  
- 6.046‑style topics (amortized analysis, advanced DP, flows, geometry, FFT, probabilistic DS) → **optional advanced weeks**.

---

## 🗺️ Curriculum Map – Phases & Weeks

The curriculum is organized into **7 phases** over **19 weeks**:

| Phase | Weeks | Focus |
|-------|-------|-------|
| 🏗️ **Phase A – Foundations** | 1–3 | Memory, asymptotics, recursion, linear structures, sorting, hashing |
| 📋 **Phase B – Core Patterns & Strings** | 4–6 | Two‑pointer, sliding window, Tier‑1 patterns, string manipulation |
| 🌳 **Phase C – Trees, Graphs & DP** | 7–11 | Trees, BSTs, graph fundamentals, classic graph algorithms, DP I–II |
| ⚙️ **Phase D – Paradigms** | 12–13 | Greedy algorithms, formal amortized analysis, advanced graphs |
| 🔄 **Phase E – Integration & Extensions** | 14–15 | Matrix patterns, backtracking, bits, advanced strings, flow |
| 🚀 **Phase F – Advanced Deep Dives (Optional)** | 16–18 | Segment trees, BIT, geometry, HLD, FFT, probabilistic DS |
| 🎤 **Phase G – Integration & Mock Interviews** | 19 | Mixed mock interviews & final mastery |

The **complete, authoritative syllabus** lives in `COMPLETE_SYLLABUS_v13_FINAL.md`. This README gives a **GitHub‑friendly, phase‑wise and day‑wise overview**.

---

## 📂 File Types – Instructional & Support Files

Each week has a consistent set of files so learners always know where to look.

### 1️⃣ Instructional Files – WeekXDayY…Instructional.md

**Purpose:** Deep, narrative‑first teaching of each day’s core topic.

**Structure (5‑Chapter Arc):**

1. 🎣 **Context & Motivation**  
   Real engineering scenario, constraints, and why naive solutions fail.
2. 🧠 **Mental Model**  
   Core analogy, ASCII diagrams, invariants, taxonomy of variations.
3. ⚙️ **Mechanics & Implementation**  
   State machine, step‑by‑step logic, inline traces, edge cases, pitfalls.
4. 🏭 **Performance & Real Systems**  
   Big‑O vs reality, caches & memory, 3–5 detailed case studies from real systems.
5. 🎓 **Integration & Mastery**  
   When to use/avoid, links to past/future weeks, reflection questions, retention hook.

Each instructional file also includes:

- 🧬 **5 Cognitive Lenses** (hardware, trade‑off, learning, AIML, historical).  
- 🎯 **Supplementary Outcomes:** 8–10 practice problems, 6–8 interview questions, misconceptions, advanced pointers, external resources.

---

### 2️⃣ WeekXGuidelines.md – Strategic Weekly Guide

- 🎯 High‑level **learning objectives** for the week.  
- 📅 Day‑by‑day concept overview in plain language.  
- 🧭 How the week fits into the **19‑week arc**.  
- ⏱️ Time allocation recommendations (theory vs practice vs review).  
- ⚠️ 5–7 week‑specific pitfalls and how to avoid them.  
- ✅ Weekly mastery checklist.

### 3️⃣ WeekX_Summary_Key_Concepts.md – Grad‑Level Notes

- 📖 Narrative summary of all key ideas from the week.  
- 🗺️ Concept maps and relationship diagrams (ASCII).  
- 📊 Comparison tables (e.g., array vs linked list, BFS vs DFS, Dijkstra vs Bellman–Ford).  
- ❌ 7–10 misconceptions with myth/reality explanations.  
- 🏭 Real‑system case studies tied back to concepts.

### 4️⃣ WeekX_Interview_QA_Reference.md – Question Bank

- 🎤 30–50 curated interview questions **with 2–3 follow‑ups each**.  
- 🧪 Grouped by topic and difficulty; **no answers** (forces active recall).  
- 🧱 Guidance for mock interviews: timing, whiteboard habits, communication.

### 5️⃣ WeekX_Problem_Solving_Roadmap.md – Practice Strategy

- 🧗 3‑stage progression:  
  Stage 1 – canonical problems  
  Stage 2 – variations & constraints  
  Stage 3 – multi‑pattern integration.  
- 🧩 Pattern templates & pseudocode skeletons.  
- 🧮 Decision matrices: problem signals → patterns → data structures.  
- ⚠️ 5–7 problem‑solving pitfalls with practical fixes.

### 6️⃣ WeekX_Daily_Progress_Checklist.md – Execution Plan

- 📅 Day‑by‑day actionable checklist.  
- 🧠 Concepts to understand, with mini prompts.  
- 🏋️ Concrete tasks (trace, draw, code, review).  
- 🔁 Weekly integration tasks and reflection prompts.

### 7️⃣ WeekX_VisualConceptsPlaybook_HYBRID.md – Visual Playbook

- 🗺️ Pattern family trees and mini‑maps for the week.  
- 📊 Enhanced ASCII diagrams and trace tables.  
- ⚠️ Failure modes: wrong vs right with side‑by‑side explanations.  
- ❓ Short quizzes (no answers) to quickly test understanding.

### 8️⃣ WeekX_ProblemSolving_Roadmap_Extended_CSharp.md – C# Extended Support

- 💻 Week‑specific **C# problem‑solving playbook**.  
- 🧩 Mapping from problem phrases → patterns → C# collections.  
- 🧱 Production‑grade C# skeletons with guard clauses and narrative comments.  
- 🪜 Progressive problem ladder (Stage 1–3) with C#‑focused notes.  
- ⚠️ C#‑specific pitfalls (e.g., LINQ in hot paths, boxing, collection choice) and fixes.

---

## 📚 Phase‑Wise & Week‑Wise Syllabus (Current v12)

Below is a **high‑level but detailed** view of the syllabus, extracted from `COMPLETE_SYLLABUS_v13_FINAL.md`.

> For precise bullet‑level content, always refer to the syllabus file itself; this summary is optimized for GitHub readability.

---

### 🏗️ Phase A – Foundations (Weeks 1–3)

#### 🧱 Week 1 – Foundations I: Computational Fundamentals, Peak Finding & Asymptotics

**Goal:** Build a mental model of how programs execute: **RAM model, memory hierarchy, pointers, Big‑O, recursion**, and your first MIT‑style algorithm design story (peak finding).

- 📅 **Day 1 – RAM Model, Virtual Memory & Pointers**  
  RAM as an array of cells; process address space (code/data/heap/stack); variables vs pointers; virtual vs physical memory; TLB & page faults; cache lines & locality.

- 📅 **Day 2 – Asymptotic Analysis**  
  Big‑O/Ω/Θ, common complexity classes, simple recurrences (binary search, merge‑sort intuition), when O(n log n) beats O(n²) in reality.

- 📅 **Day 3 – Space Complexity & Memory Usage**  
  Total vs auxiliary space, stack vs heap, lifetime & scope, overheads, time–space trade‑offs.

- 📅 **Day 4 – Recursion I (Call Stack & Patterns)**  
  Call stack frames, base vs recursive cases, recursion trees, failure modes (infinite recursion, stack overflow).

- 📅 **Day 5 – Recursion II (Patterns & Memoization)**  
  Linear/tree/divide‑and‑conquer recursion, memoization as caching, recognizing overlapping subproblems.

- 📅 **Day 6 (Optional) – Peak Finding (MIT 6.006)**  
  1D/2D peak finding, divide‑and‑conquer strategy, complexity, and meta‑lessons about algorithm design.

---

#### 🧱 Week 2 – Foundations II: Linear Data Structures & Binary Search

**Goal:** Internalize **arrays, dynamic arrays, linked lists, stacks, queues**, and binary search as an invariant‑driven pattern.

- 📅 **Day 1 – Arrays & Memory Layout**  
  Static arrays, row/column‑major layouts, cache‑friendly traversal.

- 📅 **Day 2 – Dynamic Arrays & Amortized Growth**  
  Size vs capacity, doubling strategy, intuitive amortized analysis, reallocation effects.

- 📅 **Day 3 – Linked Lists**  
  Singly/doubly linked lists, operations & costs, pointer chasing vs arrays, locality trade‑offs.

- 📅 **Day 4 – Stacks, Queues & Deques**  
  LIFO/FIFO semantics, array vs list implementations, circular buffers, real‑world uses.

- 📅 **Day 5 – Binary Search & Invariants**  
  Correct, overflow‑safe binary search; variants (first/last occurrence, lower/upper bound); binary search on answer space.

---

#### 🧱 Week 3 – Foundations III: Sorting, Heaps & Hashing

**Goal:** Understand classic sorting algorithms, **heaps**, and **hash tables**, including practical trade‑offs.

- 📅 **Day 1 – Elementary Sorts** – bubble, selection, insertion; stability & in‑place properties.  
- 📅 **Day 2 – Merge Sort & Quick Sort** – divide & conquer, recurrences, real‑world introsort‑style hybrids.  
- 📅 **Day 3 – Heaps & Heap Sort** – binary heap model, heapify, priority queues, heap sort.  
- 📅 **Day 4 – Hash Tables I (Separate Chaining)** – hash functions, load factor, resizing, worst‑case behavior.  
- 📅 **Day 5 – Hash Tables II (Open Addressing & Rolling Hash)** – linear/quadratic probing, double hashing, rolling hash & Karp–Rabin.

---

### 📋 Phase B – Core Patterns & String Patterns (Weeks 4–6)

#### 🧩 Week 4 – Core Problem‑Solving Patterns I

**Goal:** Acquire **two‑pointer, sliding window, divide & conquer, and binary search patterns** that appear across many problems.

- 📅 **Day 1 – Two‑Pointer Patterns** – same‑direction & opposite‑direction, invariants, merging, container‑with‑most‑water.  
- 📅 **Day 2 – Sliding Window (Fixed Size)** – running sum/average, max/min in window with deques.  
- 📅 **Day 3 – Sliding Window (Variable Size)** – grow/shrink windows for constraints (e.g., “at most K distinct”).  
- 📅 **Day 4 – Divide & Conquer** – generic template, inversion counting, majority element.  
- 📅 **Day 5 – Binary Search as a Pattern** – search in monotone predicate/answer space.

---

#### 🧩 Week 5 – Tier 1 Critical Patterns

**Goal:** Master patterns that collectively cover a large fraction of interview questions.

- 📅 **Day 1 – Hash Map / Hash Set Patterns** – complement patterns, frequency counting, membership/deduplication.  
- 📅 **Day 2 – Monotonic Stack** – next greater element, stock span, trapping rain water, largest rectangle in histogram.  
- 📅 **Day 3 – Merge Operations & Interval Patterns** – merging k lists, merge/insert intervals, scheduling.  
- 📅 **Day 4 – Partition, Cyclic Sort & Kadane** – Dutch National Flag, cyclic sort for 1..n arrays, max subarray sum and variants.  
- 📅 **Day 5 – Fast/Slow Pointers** – cycle detection, cycle start, list midpoints, “happy number”.

---

#### 🧩 Week 6 – Tier 1.5 String Manipulation Patterns

**Goal:** Transfer earlier array patterns to **string problems**.

- 📅 **Day 1 – Palindrome Patterns** – simple checks, expand‑around‑center, intro to palindrome partitioning.  
- 📅 **Day 2 – Substring & Sliding Window** – longest substring without repeats, character replacement, anagram substrings, minimum window substring.  
- 📅 **Day 3 – Parentheses & Bracket Matching** – stack‑based validation, longest valid parentheses, generate parentheses, minimal removals.  
- 📅 **Day 4 – String Transformations & Building** – atoi, integer↔Roman, zigzag, compression, performance tips.  
- 📅 **Day 5 (Optional) – String Matching & Rolling Hash** – revisiting Karp–Rabin and alternatives.

---

### 🌳 Phase C – Trees, Graphs & Dynamic Programming (Weeks 7–11)

#### 🌳 Week 7 – Trees & Balanced Search Trees

- 📅 **Day 1 – Binary Trees & Traversals** – tree anatomy, DFS orders, level order, recursion vs explicit stack.  
- 📅 **Day 2 – Binary Search Trees** – BST invariant, search/insert/delete, degenerate trees.  
- 📅 **Day 3 – Balanced BSTs (AVL & Red‑Black, Overview)** – balancing rationale, rotations, trade‑offs, real libraries.  
- 📅 **Day 4 – Tree Patterns** – path sums, diameter, LCA, serialization/deserialization.  
- 📅 **Day 5 (Optional) – Augmented BSTs & Order‑Statistic Trees** – subtree sizes, k‑th statistics, range counts.

---

#### 🌐 Week 8 – Graph Fundamentals: Representations, BFS, DFS & Topological Sort

> Week 8 is the **graph gateway**: how to model, how to traverse, and how to order with dependencies.

- 📅 **Day 1 – Graph Models & Representations**  
  Types (directed/undirected, weighted/unweighted), adjacency list vs matrix vs edge list, sparse vs dense, implicit graphs (grids, puzzles, state spaces), translating real problems into graphs.

- 📅 **Day 2 – Breadth‑First Search (BFS)**  
  Queue‑based frontier expansion, layers as unweighted distances, shortest routes, BFS on grids, conceptual intro to components & bipartiteness.

- 📅 **Day 3 – Depth‑First Search (DFS) & Topological Sort**  
  Recursive vs stack‑based DFS, DFS tree & edge types, cycle detection in directed graphs, topological sort via DFS post‑order and Kahn’s algorithm, scheduling & dependency resolution.

- 📅 **Day 4 – Connectivity & Bipartite Graphs**  
  Connected components via BFS/DFS, bipartite testing via two‑coloring, cycles in undirected vs directed graphs, articulation points (high‑level), Union–Find for offline connectivity.

- 📅 **Day 5 (Optional) – Strongly Connected Components (SCC)**  
  SCC definition & intuition, Kosaraju/Tarjan (conceptual), condensing SCCs to a DAG for further analysis.

---

#### 🛣️ Week 9 – Graph Algorithms I: Shortest Paths, MST & Union–Find

- 📅 **Day 1 – Dijkstra (Single‑Source Shortest Paths)** – priority queue frontier, relaxation, complexity, when Dijkstra vs BFS vs others.  
- 📅 **Day 2 – Bellman–Ford & Negative Weights** – DP over edges, V−1 relaxations, negative cycle detection.  
- 📅 **Day 3 – All‑Pairs Shortest Paths (Floyd–Warshall)** – DP formulation, O(V³) algorithm, dense‑graph use cases.  
- 📅 **Day 4 – Minimum Spanning Trees (Kruskal & Prim)** – MST definition, cut property, DSU, greedy growth.  
- 📅 **Day 5 (Optional) – DSU / Union–Find in Depth** – path compression, union by rank, inverse Ackermann intuition.

---

#### 🧮 Week 10 – Dynamic Programming I: Fundamentals

- 📅 **Day 1 – DP as Recursion + Memoization** – from naive recursion to memoized solutions.  
- 📅 **Day 2 – 1D DP & Knapsack Family** – stairs, house robber, coin change, 0/1 knapsack.  
- 📅 **Day 3 – 2D DP (Grids & Edit Distance)** – path counting, obstacles, Levenshtein distance.  
- 📅 **Day 4 – DP on Sequences** – LCS, LIS (O(n²) & O(n log n) idea).  
- 📅 **Day 5 (Optional) – Story‑Driven DP** – text justification, blackjack‑style DP.

---

#### 🧠 Week 11 – Dynamic Programming II: Trees, DAGs & Advanced Patterns

- 📅 **Day 1 – DP on Trees** – rooted tree DP, diameter, independent set.  
- 📅 **Day 2 – DP on DAGs** – DP over topological order, longest path in DAG.  
- 📅 **Day 3 – Bitmask & Subset DP** – TSP‑style DP, subset counting.  
- 📅 **Day 4 (Optional) – State Compression & Space Optimization** – rolling arrays, bitmasks.  
- 📅 **Day 5 (Optional) – Mixed DP Problems** – multi‑concept DP.

---

### ⚙️ Phase D – Algorithm Paradigms (Weeks 12–13)

#### 🧩 Week 12 – Greedy Algorithms & Exchange Arguments

- Greedy choice property, optimal substructure, interval scheduling/activity selection, MST revisited as greedy, Huffman coding, and classic counterexamples.

#### 🧩 Week 13 – Advanced Graphs & Formal Amortized Analysis

- Advanced graph patterns (articulation points, bridges, 2‑SAT at high level).  
- Formal amortized analysis: **aggregate, accounting, potential** methods.  
- Applications to dynamic arrays, multi‑pop stacks, DSU, and more.

---

### 🔄 Phase E – Integration & Extensions (Weeks 14–15)

#### 🧱 Week 14 – Matrix Problems, Backtracking & Bit Tricks

- Matrix traversal/search, backtracking on grids (word search, Sudoku), bitmask tricks for subsets, and optional number theory & modular arithmetic.

#### 🧵 Week 15 – Advanced Strings & Network Flow

- KMP, Z‑algorithm, Manacher (optional), suffix arrays/trees (conceptual), max‑flow/min‑cut, bipartite matching via flow.

---

### 🚀 Phase F – Advanced Deep Dives (Weeks 16–18, Optional)

Advanced, 6.046‑flavored topics: segment trees, BIT, matrix exponentiation, computational geometry, HLD, FFT, probabilistic DS (Bloom filters, Count–Min Sketch, HyperLogLog), min‑cost flow, algorithmic system design.

---

### 🎤 Phase G – Integration & Mock Interviews (Week 19)

- Mixed mock interviews combining arrays, trees, graphs, DP, strings, flows, geometry.  
- Weak‑spot diagnosis and targeted practice.  
- Emphasis on **communication, clarity, and reasoning**.

---

## 👥 Who This Is For

- 🎓 **Students** building deep fundamentals and preparing for campus placements.  
- 💼 **Working professionals** targeting mid‑senior or staff‑level interviews.  
- 🤖 **Self‑taught developers** seeking a structured, narrative‑first path.  
- 🧑‍🏫 **Educators** designing their own DSA courses or bootcamps.

Prerequisites:

- Comfortable with at least one programming language.  
- Familiarity with basic programming constructs (loops, functions, arrays).  
- Willingness to invest **10–15 hours/week** for the full track (or more for acceleration).

---

## 🧳 Repository Layout (Conceptual)

```text
DSA-Master-v12/
  README_v12_FULL.md          # This file – high-level overview & syllabus
  START_HERE.md               # Orientation & path selection
  LICENSE

  v12-prompts/
    COMPLETE_SYLLABUS_v13_FINAL.md
    MASTER_PROMPT_v12_FINAL.md
    SYSTEM_CONFIG_v12_FINAL.md
    SYSTEM_PROMPT_v12_FOR_AI_CHAT_FINAL.md
    EMOJI_ICON_GUIDE_v12.md
    Template_v12_Narrative_FINAL.md
    VISUAL_PLAYBOOK_GENERATION_PROMPT_v12_UPDATED.md
    WEEKLY_BATCH_GENERATION_PROMPT_v12_FINAL.md
    SYSTEM_PROMPT_v12_EXTENDED_SUPPORT_CSHARP.md
    SYSTEM_PROMPT_v13_EXTENDED_SUPPORT_CSHARP.md
    CONSOLIDATED_CSHARP_EXTENDED_SUPPORT_MASTER_PROMPT_v13.md

  WEEKS/
    Week01/
      Week01Day1...Instructional.md
      ...
      Week01_Guidelines.md
      Week01_Summary_Key_Concepts.md
      Week01_Interview_QA_Reference.md
      Week01_Problem_Solving_Roadmap.md
      Week01_Daily_Progress_Checklist.md
      Week01_VisualConceptsPlaybook_HYBRID.md
      Week01_ProblemSolving_Roadmap_Extended_CSharp.md

    ... Week02/, Week03/, ..., Week19/

  assets/
    diagrams/
    traces/
    tables/
```

Use this as a **reference layout** and adjust folder naming as needed for your project.

---

## 🚀 Getting Started

1. **Skim this README** to understand phases and file types.  
2. Open `START_HERE.md` to choose your learning path (full, acceleration, or topic‑based).  
3. Read `COMPLETE_SYLLABUS_v13_FINAL.md` if you want every detail.  
4. Begin with **Week 1** (or your target week), starting from `WeekXGuidelines.md` and the Day 1 instructional file.

This README is designed to be **GitHub‑ready**: professional, self‑contained, and aligned with the current v12 syllabus, while preserving the narrative‑first philosophy of the DSA Master course.
