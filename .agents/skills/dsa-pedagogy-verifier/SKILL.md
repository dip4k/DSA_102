---
name: dsa-pedagogy-verifier
description: Verify, audit, and refine DSA learning materials to ensure beginner-accessible fluency, progressive scaffolding, intuitive mental models, and zero academic jargon for FAANG interview preparation.
---

# DSA Pedagogy Verifier & Intuitive Coach

## Purpose & Scope
This skill evaluates, audits, and adapts DSA learning materials (specifically weekly curriculum modules in `week_01` through `week_19`, problem writeups, and tutorial notes) to guarantee they are:
1. **Fluent & Beginner-Accessible:** Plain conversational English, zero academic gatekeeping, free of dense theoretical jargon.
2. **Progressively Scaffolded:** Step-by-step cognitive ladder (Everyday Analogy -> Visual Walkthrough -> Warm-up -> Classic FAANG Medium -> Tricky Edge Cases).
3. **Intuitive & Physical:** Grounded in tangible mental models and napkin diagrams rather than abstract math theorems.
4. **Laser-Focused on FAANG Interviews:** Optimized for the real 45-minute live interview format (verbalizing thought process, handling interviewer hints, clean C# and Python code, pragmatic Big-O trade-offs).

---

## 🚦 The 5-Point Pedagogical Verification Rubric

When auditing or generating any file in `week_xy` or reviewing a problem explanation, score the material against these 5 pillars (Target: 5/5):

| Pillar | Passing Criteria (FAANG & Beginner Friendly) | Red Flags (Academic / Overload) |
| :--- | :--- | :--- |
| **1. Beginner Fluency & Tone** | Conversational, encouraging, clear. Uses "You and I" pair-programming tone. Concepts explained in simple everyday terms. | Reads like an academic textbook or discrete math paper. Overly formal, dry, or intimidating language. |
| **2. Jargon Elimination** | Jargon is stripped or immediately decoded into physical intuition. (e.g., "Two fingers moving inward" instead of "Opposing pointer monotonic boundary convergence"). | Heavy use of academic jargon ("governing mathematical invariant", "optimal substructure theorem", "discard-safety axiom"). |
| **3. Progressive Scaffolding** | Follows the **Intuitive 4-Step Ladder**: 1) Real-world hook, 2) Visual step trace, 3) Simple warm-up, 4) Classic FAANG interview problem. | "Hard-First" approach: throwing hard problems (e.g. Trapping Rain Water, Edit Distance) at the learner before warm-ups. |
| **4. Intuitive Visuals & Traces** | Uses clear ASCII diagrams, concrete number traces, and simple visual tables showing variables after every step. | Abstract graphs with empty placeholder nodes, or purely algebraic notation without step-by-step numbers. |
| **5. FAANG Interview Pragmatism** | Prepares the candidate for 45-minute live rounds: clarifying questions, verbal communication script, clean idiomatic code, edge cases. | Focuses on hardware cache lines, compiler internals, or 3-page formal proofs that an interviewer will never ask. |

---

## 📖 Jargon-to-Intuition Translation Dictionary

Whenever you encounter or write these concepts, replace academic jargon with intuitive equivalents:

| Academic / Textbook Jargon | Intuitive Plain-English Equivalent | Physical Metaphor |
| :--- | :--- | :--- |
| **Governing Mathematical Invariant** | The core rule / What always stays true | "The guardrail that never lets us fall" |
| **Discard-Safety Proof** | Why we can safely skip the rest | "Closing doors we know have no prizes behind them" |
| **Monotonic Boundary Convergence** | Squeezing from both ends | "Two friends walking toward each other on a street" |
| **Optimal Substructure** | Building big answers from smaller answers | "Solving a jigsaw puzzle corner-by-corner" |
| **Overlapping Subproblems** | Repeating the exact same calculation | "Calculating 7 x 8 once and remembering 56 instead of recalculating" |
| **Asymptotic Upper Bound O(N)** | Worst-case work proportional to input size | "Reading a book page by page: double the pages = double the time" |
| **Process Address Space / RAM Model** | How memory holds our data | "A giant row of numbered lockers in a gym" |
| **Topological Sort In-Degree Invariant** | Unlocking prerequisites in order | "Taking College Course 101 before Course 201" |
| **State-Space Pruning** | Abandoning dead-end paths early | "Backing out of a maze corridor as soon as you see a wall" |

---

## 🧗 The Intuitive 4-Step Learning Ladder

Every topic or day module must follow this progressive sequence:

```mermaid
flowchart TD
    Step1["Step 1: The Everyday Hook<br/>(Real-world analogy & why naive fails)"]
    Step2["Step 2: The Napkin Trace<br/>(Visual step-by-step walk with numbers)"]
    Step3["Step 3: The Warm-Up to FAANG Classic<br/>(From simple pattern to interview staple)"]
    Step4["Step 4: Interview Ready Delivery<br/>(45-min talk track, C#, Python, & edge cases)"]
    Step1 --> Step2 --> Step3 --> Step4
```

1. **Step 1: The Everyday Hook:**
   - Start with a tangible situation (e.g., finding two books on a shelf that cost \$20 total).
   - Show the naive brute-force approach first: *"Why not check every pair?"* (O(N^2) - slow!).
   - Identify the single bottleneck: *"We are wasting time re-checking things we already know."*

2. **Step 2: The Napkin Trace:**
   - Draw an ASCII diagram with real numbers: `[2, 7, 11, 15]`.
   - Walk through the pointers step-by-step: `Left = 0 (2)`, `Right = 3 (15)`, `Sum = 17`.
   - State the intuitive rule: *"17 is too big! Because the array is sorted, moving Right leftward decreases the sum. We can safely throw away 15!"*

3. **Step 3: The Scaffolding Ladder (Warm-up -> Classic FAANG):**
   - **Level 1 (Warm-up):** Two Sum II (Sorted array, basic opposing pointers).
   - **Level 2 (Classic FAANG Medium):** 3Sum (Fix one number, run two pointers on the rest).
   - **Level 3 (Advanced FAANG Variant):** Trapping Rain Water or 4Sum (handling duplicates, elevation bounds).

4. **Step 4: Interview-Ready Delivery:**
   - **Verbal Communication Script:** Exactly what to say to the interviewer before coding.
   - **Clean C# Implementation:** .NET 8/9, clean readable names, zero unnecessary boilerplate.
   - **Clean Python Implementation:** Python 3.11+, concise, expressive, idiomatic.
   - **Pragmatic Big-O Breakdown:** Time complexity, auxiliary space, output space.
   - **Edge-Case Safety Check:** Empty input, single element, duplicates, negative numbers.

---

## 🛠️ Verification Workflows

### Mode 1: Audit & Verify Existing Material
Run this mode when asked to analyze or check any file in `week_xy`:
1. **Scan for Academic Jargon:** Highlight terms that intimidate beginners (theorems, formal proofs, dense math notation).
2. **Check Progressive Ladder:** Ensure it does NOT start with a Hard problem before simple warm-ups.
3. **Check Visual Clarity & Pragmatic Format Selection:**
   - Detect and flag empty or broken placeholder diagrams (e.g. `R --> N1["State"]`).
   - Flag artificial star/tree diagrams (`R --> N1, N2...`) used on linear steps, dry-runs, bullet points, or failure modes; convert them to clean numbered steps, markdown trace tables, or warning callouts.
   - Retain Mermaid for decision branching flowcharts, binary/AVL trees, Tries, and graph structures.
   - Retain ASCII diagrams for in-place array layouts, pointer positions, and memory buffers where text art is clearer.
4. **Enforce Noise Reduction (Metadata & Trivia Removal):** Flag and remove generic external tool lists (VisuAlgo, Python Tutor, Mermaid Live Editor, etc.) and textbook trivia comparisons that do not serve live FAANG interview mastery.
5. **Enforce GitHub Markdown (Zero LaTeX):** Flag all instances of unescaped LaTeX math (`$...$`, `$$...$$`, `\(...\)`, `\[...\]`, `\to`, `\frac`) and replace with standard Markdown backticks (`O(N)`, `10^5`, `->`).
6. **Web Search Verification:** Use the `search_web` tool to verify problem constraints, LeetCode numbers, edge cases, and current FAANG interview expectations.
7. **Issue a Scorecard:** Provide a 1-5 score per rubric pillar with clear remediation steps.

### Mode 2: Pedagogical Rewrite & Refinement
Run this mode when rewriting a module or creating a new guide:
1. Strip out formal theorem statements and replace with clear everyday analogies.
2. Structure content strictly following the **Intuitive 4-Step Learning Ladder**.
3. **Pragmatic Visuals:** Choose the right medium:
   - Mermaid flowcharts for decision logic and branching.
   - Mermaid trees for genuine tree/graph data structures.
   - ASCII diagrams for in-place arrays, pointer alignments (`L`/`R`), and memory slots.
   - Sequential numbered steps or trace tables for dry-runs and execution walks.
   - NEVER build star/tree graphs (`R --> N1, N2...`) out of linear steps.
4. **Zero Non-Learning Fluff:** Strip out generic tool links and textbook trivia comparisons.
5. **Strict GitHub Markdown (Zero LaTeX):** Use standard Markdown backticks (`O(N)`, `O(1)`, `N <= 10^5`).
6. Provide clean, idiomatic implementations in both C# (.NET 8/9) and Python (3.11+).
7. Cross-reference real test cases using `search_web` when generating problems.

### Mode 3: Interactive Intuitive Tutor
Run this mode when teaching a learner directly:
1. Explain the concept like you're talking to a smart colleague over coffee.
2. Ask interactive questions: *"If the current sum is 18 and our target is 12, which pointer would you move and why?"*
3. Provide positive feedback and connect answers directly to FAANG interview expectations.

---

## 🎨 Modern Mermaid Styling Blueprint

Use this template when building or fixing diagrams:

```mermaid
flowchart LR
    classDef startNode fill:#e3f2fd,stroke:#1565c0,stroke-width:2px,color:#0d47a1
    classDef actionNode fill:#f3e5f5,stroke:#7b1fa2,stroke-width:2px,color:#4a148c
    classDef successNode fill:#e8f5e9,stroke:#2e7d32,stroke-width:2px,color:#1b5e20
    classDef warnNode fill:#fff3e0,stroke:#e65100,stroke-width:2px,color:#bf360c

    Start["🟢 Start: Left=0, Right=N-1"]:::startNode
    Check{"🔍 Sum == Target?"}:::actionNode
    ShrinkLeft["👉 Sum < Target: Move Left"]:::warnNode
    ShrinkRight["👈 Sum > Target: Move Right"]:::warnNode
    Success["✅ Found Pair: Return Indices"]:::successNode

    Start --> Check
    Check -->|"Sum < Target"| ShrinkLeft
    Check -->|"Sum > Target"| ShrinkRight
    Check -->|"Equal"| Success
```

