# 📘 Week 02: Linear Data Structures & Binary Search Invariants

> 🧭 **Navigation:** [← Previous: Week 01](../week_01_foundations_i_computational_fundamentals/README.md) • [🏠 Curriculum Overview](../README.md) • [📘 Complete Syllabus](../COMPLETE_SYLLABUS.md) • [Next: Week 03 →](../week_03_foundations_iii_sorting_and_hashing/README.md)

---

> 💡 **Instructor Guidance Note:**  
> *Welcome to Week 02! Having conquered computational fundamentals in Week 01, we now construct the primary data structures that form 80% of all software engineering: Arrays, Dynamic Buffers, Linked Lists, Stacks, Queues, and Binary Search boundaries. Focus on pointer manipulation, memory locality, and zero-allocation patterns.*

---

## 🎯 Weekly Mission & Conceptual Focus

Master static and dynamic array memory layouts, cache prefetching, singly and doubly linked lists with sentinel nodes, circular ring queues, stack mechanics, and boundary-proof binary search invariants.

```mermaid
%%{init: {"flowchart": {"htmlLabels": true, "padding": 20}, "themeCSS": "foreignObject { overflow: visible !important; }"}}%%
flowchart LR
    classDef arrayNode fill:#e3f2fd,stroke:#1565c0,stroke-width:2px,color:#0d47a1
    classDef listNode fill:#f3e5f5,stroke:#7b1fa2,stroke-width:2px,color:#4a148c
    classDef bufferNode fill:#fff3e0,stroke:#e65100,stroke-width:2px,color:#bf360c
    classDef searchNode fill:#e8f5e9,stroke:#2e7d32,stroke-width:2px,color:#1b5e20

    D1["📦 Day 01: Arrays &<br/>Memory Layout"]:::arrayNode --> D2["📈 Day 02: Dynamic Arrays<br/>& Doubling"]:::arrayNode
    D2 --> D3["🔗 Day 03: Linked Lists<br/>& Sentinels"]:::listNode
    D3 --> D4["🔄 Day 04: Stacks, Queues<br/>& Ring Buffers"]:::bufferNode
    D4 --> D5["🎯 Day 05: Binary Search<br/>Invariants"]:::searchNode
    D5 --> D6["🔤 Day 06: Strings &<br/>Numerical Invariants"]:::searchNode
```

---

## 📅 The 6-Day YouTube-Style Masterclass Path

| Day / Episode | Masterclass Module | The 60-Second Hook & Core Concept | Key Interview Takeaway |
| :--- | :--- | :--- | :--- |
| **Day 01** | [📘 Arrays & Memory Layout](Week_02_Day_01_Arrays_Memory_Layout_Instructional.md) | 📦 *The Row of Numbered Lockers:* Why contiguous array indexing is pure arithmetic (`base + i * size`) with 1 ns L1 cache hits. | Articulate cache locality, spatial prefetching, and Span<T> in live rounds. |
| **Day 02** | [📘 Dynamic Arrays & Amortized Growth](Week_02_Day_02_Dynamic_Arrays_Amortized_Growth_Instructional.md) | 📈 *The Expanding Suitcase:* Why doubling capacity gives O(1) amortized appends, and why naive 1-by-1 expansion creates an O(N^2) disaster. | Prove amortized O(1) via the Banker's accounting method on the whiteboard. |
| **Day 03** | [📘 Linked Lists & Sentinel Surgery](Week_02_Day_03_Linked_Lists_Instructional.md) | 🔗 *The Scavenger Hunt & The Bodyguard:* Eliminating null checks with dummy sentinel nodes and mastering clean pointer surgery without losing head. | Execute in-place list reversal and cycle detection without null-pointer exceptions. |
| **Day 04** | [📘 Stacks, Queues & Ring Buffers](Week_02_Day_04_Stacks_Queues_Deques_Instructional.md) | 🔄 *The Circular Carousel:* Implementing zero-allocation bounded ring buffers with modulo arithmetic, LIFO undo stacks, and FIFO processing. | Implement thread-safe ring buffers and explain array-backed vs linked structures. |
| **Day 05** | [📘 Binary Search Invariants](Week_02_Day_05_Binary_Search_Invariants_Instructional.md) | 🎯 *The Caliper Squeeze & The Shrinking Window:* Demystifying `<` vs `<=`, `mid` overflow guards, and finding leftmost vs rightmost insertion points. | Write bug-free binary search in 3 minutes without off-by-one infinite loops. |
| **Day 06** | [📘 Strings & Numerical Invariants](Week_02_Day_06_Strings_Numbers_Instructional.md) | 🔤 *The Immutable Scroll:* String builder allocations, ASCII sliding pointers, Unicode code points, and palindrome verification. | Defend memory-efficient string manipulation and avoid O(N^2) concatenation traps. |

---

## 📚 Consolidated Playbooks & Reference Guides

*   📖 **Comprehensive Week Playbook:** [WEEK_02_FULL_PLAYBOOK.md](WEEK_02_FULL_PLAYBOOK.md) — High-density synthesis of all invariants, theorems, and pattern triggers for this week.
*   📊 **Visual Concepts Playbook (Hybrid):** [Week_02_Visual_Concepts_Playbook_HYBRID.md](Week_02_Visual_Concepts_Playbook_HYBRID.md) — Compact Mermaid diagrams, state transitions, and complexity reference tables.
*   💻 **Production C# Reference (.NET 8/9):** [Week_02_Extended_CSharp_Complete.md](Week_02_Extended_CSharp_Complete.md) — Idiomatic, zero-allocation memory-aware implementations and problem ladders.
*   🐍 **Production Python Reference (Python 3.11+):** [Week_02_Extended_Python_Complete.md](Week_02_Extended_Python_Complete.md) — Clean, pythonic reference implementations for rapid whiteboard prototyping.

---

## 🛠️ The 5 Core Support Files (`support files/`)

Every week is accompanied by five dedicated support files located in the `support files/` subdirectory:

1.  📋 [Daily Progress Checklist](support%20files/Week_02_Daily_Progress_Checklist.md) — Step-by-step accountability tracker for theory, coding, and variants.
2.  🧭 [Weekly Guidelines & Mental Models](support%20files/Week_02_Guidelines.md) — Strategic roadmap, common pitfalls, and conceptual anchors.
3.  🎙️ [Interview Q&A Comprehensive Reference](support%20files/Week_02_Interview_QA_Reference.md) — High-frequency senior technical questions with verbal response frameworks.
4.  🗺️ [Problem-Solving Roadmap](support%20files/Week_02_Problem_Solving_Roadmap.md) — Tier-1 problem progression ladder from warm-up to advanced.
5.  📑 [Summary & Key Concepts Synthesis](support%20files/Week_02_Summary_Key_Concepts.md) — Fast-revision cheat sheet covering all governing invariants and system trade-offs.

---

> 🧭 **Navigation:** [← Previous: Week 01](../week_01_foundations_i_computational_fundamentals/README.md) • [🏠 Curriculum Overview](../README.md) • [📘 Complete Syllabus](../COMPLETE_SYLLABUS.md) • [Next: Week 03 →](../week_03_foundations_iii_sorting_and_hashing/README.md)
