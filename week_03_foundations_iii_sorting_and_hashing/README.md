# 📘 Week 03: Sorting Algorithms, Heaps & Hash Tables

> 🧭 **Navigation:** [← Previous: Week 02](../week_02_foundations_ii_linear_data_structures/README.md) • [🏠 Curriculum Overview](../README.md) • [📘 Complete Syllabus](../COMPLETE_SYLLABUS.md) • [Next: Week 04 →](../week_04_core_problem_solving_patterns_i/README.md)

---

> 💡 **Instructor Guidance Note:**  
> *Welcome to Week 03! This week bridges primitive data structures to advanced algorithms. We demystify the two engines of modern software: Ordering (Sorting & Priority Queues) and Instant Lookup (Hash Tables). Master why Quicksort dominates in cache performance, why Merge Sort is mandatory for stable external streams, why building a heap is surprisingly O(N), and how hash tables resolve collisions under the hood.*

---

## 🎯 Weekly Mission & Conceptual Focus

Master comparison sorts, divide-and-conquer mechanics (Merge & Quick Sort), complete binary tree heap invariants, sift-down math, separate chaining vs open addressing, tombstone markers, and rolling hash foundations.

```mermaid
%%{init: {"flowchart": {"htmlLabels": true, "padding": 20}, "themeCSS": "foreignObject { overflow: visible !important; }"}}%%
flowchart LR
    classDef sortNode fill:#e3f2fd,stroke:#1565c0,stroke-width:2px,color:#0d47a1
    classDef heapNode fill:#fff3e0,stroke:#e65100,stroke-width:2px,color:#bf360c
    classDef hashNode fill:#e8f5e9,stroke:#2e7d32,stroke-width:2px,color:#1b5e20

    D1["🧹 Day 01: Sorting Fundamentals<br/>& Stability"]:::sortNode --> D2["⚡ Day 02: Merge & Quick Sort<br/>(Divide & Conquer)"]:::sortNode
    D2 --> D3["🏔️ Day 03: Heaps, Priority Queues<br/>& O(N) Build-Heap"]:::heapNode
    D3 --> D4["🗝️ Day 04: Hash Tables I<br/>& Separate Chaining"]:::hashNode
    D4 --> D5["🔍 Day 05: Hash Tables II<br/>& Rolling Hash (Rabin-Karp)"]:::hashNode
```

---

## 📅 The 5-Day YouTube-Style Masterclass Path

| Day / Episode | Masterclass Module | The 60-Second Hook & Core Concept | Key Interview Takeaway |
| :--- | :--- | :--- | :--- |
| **Day 01** | [📘 Sorting Fundamentals & Stability](Week_03_Day_01_Sorting_Fundamentals_Instructional.md) | 🃏 *Sorting a Hand of Playing Cards:* Why Insertion Sort dominates small arrays (`N < 32`), why Bubble Sort fails, and what stability means for multi-key database sorting. | Explain sorting stability and justify when quadratic sorts outperform O(N log N) hybrids. |
| **Day 02** | [📘 Merge & Quick Sort (Divide & Conquer)](Week_03_Day_02_Merge_Quick_Sort_Instructional_Revised.md) | ⚔️ *The Heavyweight Bout:* Merge Sort (guaranteed O(N log N), stable, O(N) space) vs Quick Sort (cache-friendly in-place partitioning, O(1) space, O(N^2) pivot hazard). | Walk through Lomuto vs Hoare partitioning and eliminate Quicksort recursion stack overflow. |
| **Day 03** | [📘 Heaps, Priority Queues & O(N) Build-Heap](Week_03_Day_03_Heaps_Heapify_Heap_Sort_Instructional.md) | 🏔️ *The Corporate Org Chart:* Complete binary tree array indexing (`2i + 1`, `2i + 2`), Top-K streaming elements, and the mathematical visual proof of O(N) build-heap. | Implement sift-down and defend Top-K streaming heap selection in 45-minute rounds. |
| **Day 04** | [📘 Hash Tables I: Separate Chaining](Week_03_Day_04_Hash_Tables_Separate_Chaining_Instructional.md) | 📬 *The Giant Mailroom:* Hash distribution, the Avalanche Effect, load factor threshold (0.75), and separate chaining buckets with linked nodes. | Explain hash collision degradation and write custom generic hash tables with dynamic resizing. |
| **Day 05** | [📘 Hash Tables II: Open Addressing & Rolling Hash](Week_03_Day_05_Hash_Tables_Open_Addressing_Rolling_Hash_Instructional.md) | 🚗 *The Crowded Parking Lot:* Linear probing, tombstone deletion markers, Robin Hood displacement, and Rabin-Karp polynomial rolling hash for O(N) substring search. | Defend open addressing cache locality and implement rolling hash without modulo arithmetic bugs. |

---

## 📚 Consolidated Playbooks & Reference Guides

*   📖 **Comprehensive Week Playbook:** [WEEK_03_FULL_PLAYBOOK.md](WEEK_03_FULL_PLAYBOOK.md) — High-density synthesis of all invariants, theorems, and pattern triggers for this week.
*   📊 **Visual Concepts Playbook (Hybrid):** [Week_03_Visual_Concepts_Playbook_HYBRID.md](Week_03_Visual_Concepts_Playbook_HYBRID.md) — Compact Mermaid diagrams, state transitions, and complexity reference tables.
*   💻 **Production C# Reference (.NET 8/9):** [Week_03_Extended_CSharp_Complete.md](Week_03_Extended_CSharp_Complete.md) — Idiomatic, zero-allocation memory-aware implementations and problem ladders.
*   🐍 **Production Python Reference (Python 3.11+):** [Week_03_Extended_Python_Complete.md](Week_03_Extended_Python_Complete.md) — Clean, pythonic reference implementations for rapid whiteboard prototyping.

---

## 🛠️ The 5 Core Support Files (`support files/`)

Every week is accompanied by five dedicated support files located in the `support files/` subdirectory:

1.  📋 [Daily Progress Checklist](support%20files/Week_03_Daily_Progress_Checklist.md) — Step-by-step accountability tracker for theory, coding, and variants.
2.  🧭 [Weekly Guidelines & Mental Models](support%20files/Week_03_Guidelines.md) — Strategic roadmap, common pitfalls, and conceptual anchors.
3.  🎙️ [Interview Q&A Comprehensive Reference](support%20files/Week_03_Interview_QA_Reference.md) — High-frequency senior technical questions with verbal response frameworks.
4.  🗺️ [Problem-Solving Roadmap](support%20files/Week_03_Problem_Solving_Roadmap.md) — Tier-1 problem progression ladder from warm-up to advanced.
5.  📑 [Summary & Key Concepts Synthesis](support%20files/Week_03_Summary_Key_Concepts.md) — Fast-revision cheat sheet covering all governing invariants and system trade-offs.

---

> 🧭 **Navigation:** [← Previous: Week 02](../week_02_foundations_ii_linear_data_structures/README.md) • [🏠 Curriculum Overview](../README.md) • [📘 Complete Syllabus](../COMPLETE_SYLLABUS.md) • [Next: Week 04 →](../week_04_core_problem_solving_patterns_i/README.md)
