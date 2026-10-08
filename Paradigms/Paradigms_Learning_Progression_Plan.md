# 🗺️ Algorithmic Paradigms: Learning Progression Plan

> *"From zero knowledge to confident FAANG problem solver: A practical, milestone-based roadmap designed without academic jargon."*

---

## 🎯 Purpose of This Progression Plan

This progression plan replaces dense academic lecture timetables with an intuitive, milestone-driven roadmap. It guides a beginner through mastering the core algorithmic blueprints required for Tier-1 and FAANG technical interviews.

---

## 🪜 The 4-Stage Mastery Ladder

```
[ Stage 1: Intuition & Hooks ]    ──> Everyday analogies, physical mental models, zero math.
           │
           ▼
[ Stage 2: Core Paradigms ]       ──> Greedy, Divide & Conquer, DP, Backtracking, Amortized.
           │
           ▼
[ Stage 3: Cross-Integration ]    ──> Blending paradigms: Grids, Matrices, Bits, and Strings.
           │
           ▼
[ Stage 4: Interview Fluency ]    ──> 45-minute live delivery, verbal talk tracks, edge cases.
```

---

## 📅 Milestone Roadmap

### 🟢 Milestone 1: The Greedy Blueprint (Week 12 Alignment)
- **Goal:** Master the art of the immediate choice. Know when local optimality leads to global optimality, and know when greedy breaks.
- **Key Concepts:**
  - The "No Regrets" sanity check (safe exchange intuition).
  - Interval & Activity scheduling (earliest finish time rule).
  - Minimum Spanning Trees (Kruskal & Prim intuition).
  - The Classic Traps: Coin change failure, 0/1 Knapsack failure.
- **Reference Material:**
  - 📖 [01. Greedy Paradigm Explained Simply](01_Greedy_Paradigm_Explained_Simply.md)
  - 📂 Related Curriculum: `week_12_greedy_and_paradigms/`
- **Exit Checklist:**
  - [ ] Can explain why picking the earliest finish time works for interval scheduling.
  - [ ] Can construct a 3-element counterexample where greedy coin change fails.
  - [ ] Can implement Interval Scheduling in C# and Python from memory.

---

### 🟡 Milestone 2: Divide & Conquer vs. Dynamic Programming (Weeks 10, 11 & 12 Alignment)
- **Goal:** Understand when problems can be chopped into independent pieces vs. when subproblems overlap and require memoization.
- **Key Concepts:**
  - Split, Solve, and Stitch (Divide & Conquer 3-step rhythm).
  - Merge Sort trace (zipper merge with two pointers).
  - Dynamic Programming: The notepad metaphor (storing calculations).
  - The Two Signs: Overlapping Subproblems and Building Blocks.
  - Fractional Knapsack (Greedy) vs. 0/1 Knapsack (DP).
- **Reference Material:**
  - 📖 [02. Divide & Conquer Explained Simply](02_Divide_And_Conquer_Explained_Simply.md)
  - 📖 [03. Dynamic Programming vs. Greedy](03_Dynamic_Programming_vs_Greedy.md)
  - 📂 Related Curriculum: `week_10_dynamic_programming_i_fundamentals/`, `week_11_dp_ii_advanced/`
- **Exit Checklist:**
  - [ ] Can explain the difference between Divide & Conquer and Decrease & Conquer.
  - [ ] Can trace a 1D DP table for Coin Change with pen and paper.
  - [ ] Can clearly articulate why Fractional Knapsack is greedy but 0/1 Knapsack requires DP.

---

### 🟠 Milestone 3: Backtracking & Branch and Bound (Week 13 Alignment)
- **Goal:** Master systematic search over combinatorial state spaces with intelligent pruning.
- **Key Concepts:**
  - The 3-Step Rhythm: Choose -> Explore -> Unchoose.
  - Pruning: Abandoning dead ends the moment a constraint is violated.
  - Branch & Bound: Backtracking with a scoreboard (pruning when a branch cannot beat the best record).
  - N-Queens with `O(1)` column and diagonal set lookups.
  - Sudoku and Word Search grid exploration.
- **Reference Material:**
  - 📖 [04. Backtracking & Branch and Bound](04_Backtracking_And_Branch_Bound.md)
  - 📂 Related Curriculum: `week_13_backtracking_and_branch_bound/`
- **Exit Checklist:**
  - [ ] Can write the universal Backtracking recursive template from memory.
  - [ ] Can differentiate Backtracking (finding any valid arrangement) from Branch & Bound (finding the optimal arrangement).
  - [ ] Can solve N-Queens in C# and Python using set-based pruning.

---

### 🔵 Milestone 4: Amortized Cost Analysis (Week 13 Alignment)
- **Goal:** Understand how infrequent heavy operations are absorbed by frequent cheap operations.
- **Key Concepts:**
  - The 30-Day Subway Pass mental model.
  - Dynamic Array resizing (`List<T>`, `vector`, Python `list`).
  - The Bulk Receipt (Aggregate), Piggy Bank (Accounting), and Energy Tank (Potential) models.
  - Stack with MultiPop amortized `O(1)` analysis.
- **Reference Material:**
  - 📖 [05. Amortized Analysis in Plain English](05_Amortized_Analysis_Plain_English.md)
- **Exit Checklist:**
  - [ ] Can explain why `List<T>.Add()` is amortized `O(1)` even though array resizing takes `O(N)`.
  - [ ] Can explain why arrays double their capacity instead of adding fixed slots like `+100`.
  - [ ] Can prove why `MultiPop` on a stack is amortized `O(1)`.

---

### 🟣 Milestone 5: Cross-Paradigm Integration (Weeks 14 & 15 Alignment)
- **Goal:** Blend multiple paradigms to solve complex, hybrid interview challenges.
- **Key Concepts:**
  - Backtracking on 2D Grids (Word Search II with Tries).
  - Bitmask Dynamic Programming (Traveling Salesperson for small `N <= 20`).
  - Fast String Matching (KMP and Z-Algorithm prefix failure models).
  - Network Flow as a paradigm reduction (Max-Flow, Min-Cut, Bipartite Matching).
- **Reference Material:**
  - 📖 [06. Paradigm Decision Framework](06_Paradigm_Decision_Framework.md)
  - 📂 Related Curriculum: `week_14_matrix_backtracking_bits/`, `week_15_advanced_strings_flow/`
- **Exit Checklist:**
  - [ ] Can classify any unseen interview problem into one of the 6 paradigms within 3 minutes.
  - [ ] Can use the 5-step emergency pivot when a first intuition hits a dead end.
  - [ ] Can deliver the interview talk track out loud before typing code.

---

## 🏆 Self-Assessment Scorecard

Rate your confidence from 1 to 5 for each topic before moving on to full mock interviews:

| Topic | Confidence (1-5) | Status |
| :--- | :--- | :--- |
| **Greedy Choice & Sanity Check** | ⭐⭐⭐⭐⭐ | Ready |
| **Divide & Conquer Merge Logic** | ⭐⭐⭐⭐⭐ | Ready |
| **DP vs. Greedy Decision Making** | ⭐⭐⭐⭐⭐ | Ready |
| **Backtracking Choose-Explore-Undo** | ⭐⭐⭐⭐⭐ | Ready |
| **Branch & Bound Optimistic Cuts** | ⭐⭐⭐⭐⭐ | Ready |
| **Amortized Array Resizing Logic** | ⭐⭐⭐⭐⭐ | Ready |
| **45-Minute Live Interview Delivery** | ⭐⭐⭐⭐⭐ | Ready |
