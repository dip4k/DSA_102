# 📘 Week 01: Computational Fundamentals, RAM Model & Asymptotics

> 🧭 **Navigation:** [← Root Curriculum](../README.md) • [🏠 Curriculum Overview](../README.md) • [📘 Complete Syllabus](../COMPLETE_SYLLABUS.md) • [Next: Week 02 →](../week_02_foundations_ii_linear_data_structures/README.md)

---

> 💡 **Instructor Guidance Note:**  
> *Welcome to Week 01! This week establishes the physical and computational foundation of all software engineering. Instead of memorizing academic formulas, we focus on mechanical realities: how CPUs address RAM, why contiguous memory outperforms pointer-chasing, how input scaling dictates complexity, and how the call stack executes recursion. Take it day by day and embrace the physical mental models.*

---

## 🎯 Weekly Mission & Conceptual Focus

Build rock-solid mental models of program execution, RAM memory hierarchy, complexity bounds (Big-O/Ω/Θ), and recursion call stack mechanics.

```mermaid
%%{init: {"flowchart": {"htmlLabels": true, "padding": 20}, "themeCSS": "foreignObject { overflow: visible !important; }"}}%%
flowchart LR
    classDef stepNode fill:#e3f2fd,stroke:#1565c0,stroke-width:2px,color:#0d47a1
    classDef memoryNode fill:#f3e5f5,stroke:#7b1fa2,stroke-width:2px,color:#4a148c
    classDef recurNode fill:#fff3e0,stroke:#e65100,stroke-width:2px,color:#bf360c
    classDef successNode fill:#e8f5e9,stroke:#2e7d32,stroke-width:2px,color:#1b5e20

    D1["🏢 Day 01: RAM Model<br/>& Pointers"]:::memoryNode --> D2["📈 Day 02: Big-O<br/>& Scaling Curves"]:::stepNode
    D2 --> D3["🪚 Day 03: Space<br/>& Memory Layout"]:::memoryNode
    D3 --> D4["🍽️ Day 04: Recursion I<br/>& Call Stack"]:::recurNode
    D4 --> D5["📝 Day 05: Recursion II<br/>& Memoization"]:::recurNode
    D5 --> D6["⛰️ Day 06: Peak Finding<br/>& Binary Search"]:::successNode
```

---

## 📅 The 6-Day YouTube-Style Masterclass Path

| Day / Episode | Masterclass Module | The 60-Second Hook & Core Concept | Key Interview Takeaway |
| :--- | :--- | :--- | :--- |
| **Day 01** | [📘 RAM Model & Pointers](Week_01_Day_01_RAM_Model_Pointers_Instructional.md) | 🏢 *The Gym Locker Model:* Demystifying memory addresses, Stack vs Heap, and why contiguous arrays crush linked lists in cache locality. | Articulate value vs reference semantics and cache misses in live interviews. |
| **Day 02** | [📘 Asymptotic Analysis (Big-O, Ω, Θ)](Week_01_Day_02_Asymptotic_Analysis_Instructional.md) | 📈 *The Scaling Ceiling & Floor:* De-jargoning Big-O, Big-Omega, and Big-Theta into plain English growth curves that survive enterprise scale. | Dissect asymptotic bounds without confusing worst-case with Big-O. |
| **Day 03** | [📘 Space Complexity & Memory Accounting](Week_01_Day_03_Space_Complexity_Memory_Usage_Instructional.md) | 🪚 *The Carpenter's Workbench:* Auxiliary (scratchpad) space vs Output delivery space, avoiding container OOM kills, and call-stack accounting. | Defend O(1) in-place auxiliary space vs O(N) duplicate allocation. |
| **Day 04** | [📘 Recursion I: The Call Stack Engine](Week_01_Day_04_Recursion_I_Call_Stack_Instructional.md) | 🍽️ *The Cafeteria Plate Stack & Leap of Faith:* Visualizing activation frames, push/unwind phases, base case guards, and iterative conversion. | Walk through recursion trees and calculate stack memory depth with zero anxiety. |
| **Day 05** | [📘 Recursion II: Memoization & DP Foundations](Week_01_Day_05_Recursion_II_Memoization_Instructional.md) | 📝 *The Cheat Sheet Revolution:* Spotting overlapping subproblems and collapsing exponential O(2^N) trees into lightning O(N) linear lookups. | Implement top-down memoization in C# and Python in under 5 minutes. |
| **Day 06** | [📘 Peak Finding & Algorithmic Thinking](Week_01_Day_06_Peak_Finding_Algorithmic_Thinking_Instructional.md) | ⛰️ *Hiking in the Fog:* The MIT 6.006 classic proving Binary Search does NOT require a sorted array—only a directional gradient. | Explain search-space elimination on unsorted inputs with total confidence. |

---

## 📚 Consolidated Playbooks & Reference Guides

*   📖 **Comprehensive Week Playbook:** [WEEK_01_FULL_PLAYBOOK.md](WEEK_01_FULL_PLAYBOOK.md) — High-density synthesis of all invariants, theorems, and pattern triggers for this week.
*   📊 **Visual Concepts Playbook (Hybrid):** [Week_01_Visual_Concepts_Playbook_HYBRID.md](Week_01_Visual_Concepts_Playbook_HYBRID.md) — Compact Mermaid diagrams, state transitions, and complexity reference tables.
*   💻 **Production C# Reference (.NET 8/9):** [Week_01_Extended_CSharp_Complete.md](Week_01_Extended_CSharp_Complete.md) — Idiomatic, zero-allocation memory-aware implementations and problem ladders.
*   🐍 **Production Python Reference (Python 3.11+):** [Week_01_Extended_Python_Complete.md](Week_01_Extended_Python_Complete.md) — Clean, pythonic reference implementations for rapid whiteboard prototyping.

---

## 🛠️ The 5 Core Support Files (`support files/`)

Every week is accompanied by five dedicated support files located in the `support files/` subdirectory:

1.  📋 [Daily Progress Checklist](support%20files/Week_01_Daily_Progress_Checklist.md) — Step-by-step accountability tracker for theory, coding, and variants.
2.  🧭 [Weekly Guidelines & Mental Models](support%20files/Week_01_Guidelines.md) — Strategic roadmap, common pitfalls, and conceptual anchors.
3.  🎙️ [Interview Q&A Comprehensive Reference](support%20files/Week_01_Interview_QA_Reference.md) — High-frequency senior technical questions with verbal response frameworks.
4.  🗺️ [Problem-Solving Roadmap](support%20files/Week_01_Problem_Solving_Roadmap.md) — Tier-1 problem progression ladder from warm-up to advanced.
5.  📑 [Summary & Key Concepts Synthesis](support%20files/Week_01_Summary_Key_Concepts.md) — Fast-revision cheat sheet covering all governing invariants and system trade-offs.

---

> 🧭 **Navigation:** [← Root Curriculum](../README.md) • [🏠 Curriculum Overview](../README.md) • [📘 Complete Syllabus](../COMPLETE_SYLLABUS.md) • [Next: Week 02 →](../week_02_foundations_ii_linear_data_structures/README.md)
