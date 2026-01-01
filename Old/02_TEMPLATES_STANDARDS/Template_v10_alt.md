# 📚 TEMPLATE_v9.2_Mental_Model_First.md — INSTRUCTIONAL & SUPPORT FILE TEMPLATE

**Purpose:** Mental-model-first template for all instructional & support files in DSA Master Curriculum v9.2  
**Status:** ✅ OFFICIAL — Use for all `Week_X_Day_Y_[Topic]_Instructional.md` and weekly support files  
**Philosophy:** Understanding first, code second (or never). Focus on **intuition, mechanics, trade-offs, and pattern recognition** — not memorizing code.

---

## 🔍 GLOBAL QUALITY STANDARDS (MENTAL-MODEL FIRST)

### 🎯 Core Learning Philosophy

- DSA is about:
  - Building **mental models** of how structures and algorithms behave  
  - Developing **engineering intuition** for trade-offs and edge cases  
  - Recognizing **patterns** and mapping problems to them under pressure  
- Code is a **secondary artifact**:
  - We care about **logic, invariants, and mechanics**
  - C# may be used if absolutely needed, but **no full code-first teaching**

### ✅ Instructional File Requirements

Each instructional file MUST have:

- **Structure:**
  - 11 sections in this exact order:
    1. 🤔 The Why  
    2. 📌 The What  
    3. ⚙ The How  
    4. 🎨 Visualization  
    5. 📊 Critical Analysis  
    6. 🏭 Real Systems  
    7. 🔗 Concept Crossovers  
    8. 📐 Mathematical  
    9. 💡 Algorithmic Intuition  
    10. ❓ Knowledge Check  
    11. 🎯 Retention Hook  
  - A dedicated **🧩 5 Cognitive Lenses** block  
  - A **⚔ Supplementary Outcomes** section (problems, Q&A, misconceptions, etc.)

- **Content:**
  - Mental models, diagrams, and step-by-step mechanics  
  - All core **concepts/variations/subtypes** in Section 2 (no missing variants)  
  - At least **3 worked examples** (simple → medium → edge case)  
  - **5–10 real systems** in Section 6  

- **Visuals:**
  - At least **2–3 tables** (e.g., concept summary, complexity, comparisons)  
  - At least **1 trace or structural diagram** (ASCII or Mermaid)  
  - At least **1 decision/comparison table or simple flow** in Sections 7 or 9  
  - Emojis/icons used for emphasis & visual cues (per EMOJI_ICON_GUIDE_v8)

- **Technical:**
  - **Markdown only** (`.md`), UTF-8, LF line endings  
  - **No LaTeX** symbols or encoding (use plain text: `O(n log n)` etc.)  
  - **No code by default**; if absolutely needed, **C# only**, minimal, logic-focused  
  - No Python/Java/C++ blocks

- **Word Count:**
  - **Instructional file total:** target **7,500–15,000 words**  
  - No per-section word quotas; aim for balanced, deep explanations

### ✅ Support File Requirements (per Week)

Each week MUST have:

- `Week_X_Guidelines.md`  
- `Week_X_Summary_Key_Concepts.md`  
- `Week_X_Interview_QA_Reference.md`  
- `Week_X_Problem_Solving_Roadmap.md`  
- `Week_X_Daily_Progress_Checklist.md`

Support files:

- Focus on **integration, planning, and recall** (not long essays)  
- Are also **Markdown only**, no LaTeX, no code (logic-first)

---

## 📋 HOW TO USE THIS TEMPLATE

1. **Copy** this file as the base for new templates or generation prompts.  
2. For an instructional file:
   - Use the “Instructional File Template” section below.
   - Replace all `[PLACEHOLDER]` blocks with topic-specific content.
3. For support files:
   - Use the “Support File Templates” section below.
4. Always:
   - Think like a **lead engineer mentoring a junior** at a whiteboard.  
   - Prioritize **mental simulation and design intuition** over syntax.  
   - Use diagrams, tables, and examples to reduce text clutter.  
   - Ensure **no `[PLACEHOLDER]` text** or meta instructions remain in final student-facing files.

---

# 🧩 TEMPLATE 1 — INSTRUCTIONAL FILE (DSA TOPIC)

File name:  
`Week_X_Day_Y_[Topic_Name]_Instructional.md`

---

# 🎯 WEEK X DAY Y: TOPIC NAME — COMPLETE GUIDE

**Category:** [Category]  |  **Difficulty:** 🟢 / 🟡 / 🔴 (Foundation / Medium / Advanced)  
**Prerequisites:** [Link or list of Week X topics / concepts]  
**Interview Frequency:** X% (approximate frequency in interviews)  
**Real-World Impact:** [Short description of importance in production systems]

---

## 🎓 LEARNING OBJECTIVES

By the end of this topic, you will be able to:

- ✅ Understand [Core concept 1]  
- ✅ Explain [Core concept 2] and its trade-offs  
- ✅ Simulate [Core technique] step-by-step on small examples  
- ✅ Recognize when to use [Pattern/Algorithm] from problem statements  
- ✅ Compare [This concept] against key alternatives  

*(Optional table mapping objectives → sections)*

---

## 🤔 SECTION 1: THE WHY — ENGINEERING MOTIVATION

**Goal:** Motivate *why this topic exists* and where it matters.

Include:

- 2–3 **concrete real-world problems** this topic solves  
- Design goals: what this concept optimizes (time, space, simplicity, robustness, etc.)  
- Trade-offs introduced by using this approach  
- Why interviewers care

**Substructure (suggested):**

- **Real-World Problems This Solves**
  - Problem 1: [Short description]  
    - Where it appears (system / domain): [e.g., indexing in databases]  
    - Business impact: [latency, cost, reliability]  
  - Problem 2: [Short description]  
  - Problem 3: [Optional]

- **Design Goals & Trade-Offs**
  - What performance or design goals this technique optimizes  
  - What you give up (e.g., extra memory, complexity, fewer guarantees)

- **Interview Relevance**
  - Typical question patterns (e.g., “optimize X,” “design Y”)  
  - Depth interviewer expects (conceptual vs implementation details)

*(Include at least one concise table if helpful: problem → system → impact.)*

---

## 📌 SECTION 2: THE WHAT — MENTAL MODEL & CORE CONCEPTS

**Goal:** Build a clear **mental model** and define the concept intuitively.

Include:

- A vivid **core analogy**  
- One main **visual depiction** (shape/structure)  
- **Invariants** that always hold  
- A summary of all **core variations / concepts** with rough complexity

### 🧠 Core Analogy

“Think of this like **[familiar object/process]** because…”

- Describe how this concept behaves in non-technical terms  
- Tie directly to how we’ll visualize it

### 🖼 Visual Representation

- 1 clear diagram (ASCII or Mermaid) showing structure/shape  
- Label key components (nodes, pointers, ranges, etc.)

Example (ASCII):

```text
[ASCII diagram roughly showing structure]
Legend:
- Symbol A = ...
- Symbol B = ...
```

### 🔑 Core Invariants

List fundamental properties that **must always hold** for correctness.

- Invariant 1: [Description + why it matters]  
- Invariant 2: [Description + why it matters]  
- Invariant 3: [Optional]

### 📋 CORE CONCEPTS — Types / Variations / Patterns (LIST ALL)

For this topic, list all relevant subtypes/variations (not just operations):

- Concept/Variation 1: [Short explanation]  
- Concept/Variation 2: [Short explanation]  
- Concept/Variation 3: [etc.]

**Concept Summary Table:**

| # | 🧩 Concept / Variation | ✏️ Brief Description                | ⏱ Time (typical) | 💾 Space (typical) |
|---|------------------------|-------------------------------------|------------------|--------------------|
| 1 | [Concept 1]            | [One-line mental model]             | O(?)             | O(?)               |
| 2 | [Concept 2]            | [One-line mental model]             | O(?)             | O(?)               |
| … |                        |                                     |                  |                    |

### 📐 Formal-ish Definition (Optional, Intuitive First)

- Provide a concise, plain-text “formal” definition only **after** analogy and visuals.  
- No LaTeX; keep it readable.

---

## ⚙ SECTION 3: THE HOW — MECHANICAL WALKTHROUGH

**Goal:** Show how the concept works step-by-step, so the learner can **simulate it mentally**.

Include:

- Definition of **state / data structure representation**  
- Step-by-step **operations** (core actions)  
- Memory behavior (stack vs heap, pointers, locality)  
- Handling of important **edge cases**

### 🧱 State / Data Structure

- Describe what fields/arrays/pointers exist  
- How they relate (diagrams encouraged)

### 🧮 Operation 1: [Name]

- Purpose: [What it does and when used]  
- Steps: bullet or numbered list of mechanical steps (logic-first)  
- Complexity: Time [O(?)] / Space [O(?)]

Optionally show high-level pseudocode in **plain English or minimal C#**, only if it clarifies the mechanics.

### 🔁 Operation 2, 3, … (as needed)

Repeat structure for other important operations (insert, delete, search, update, etc.)

### 💾 Memory & State Behavior

- Where data lives: stack vs heap  
- How pointers/indices move  
- How cache/locality is affected (contiguous vs scattered)

### 🛡 Edge Cases

List key edge cases and how the mechanics handle them:

- Empty structure  
- Single element  
- Very large inputs  
- Duplicates / special values  

Optionally a small table:

| 🚧 Edge Case      | Expected Handling             |
|-------------------|------------------------------|
| Empty input       | [Behavior]                   |
| Single element    | [Behavior]                   |
| …                 |                              |

---

## 🎨 SECTION 4: VISUALIZATION — SIMULATION & EXAMPLES

**Goal:** Make the concept feel **visually obvious** by walking through representative examples.

Include:

- At least 3 examples: simple, medium, and edge/complex
- For each: an initial-state diagram + step-by-step trace

### 🧊 Example 1: Simple Case

- Input: [Small concrete input]  
- Initial state diagram  
- Step-by-step trace (table or bullet list)

Example trace table:

| ⏱ Step | 📥 Input View | 📦 Internal State | 📤 Output / Action |
|--------|---------------|-------------------|--------------------|
| 0      | […]           | […]               | -                  |
| 1      | […]           | […]               | […]                |

### 📈 Example 2: Medium Complexity

- Larger or more interesting input  
- Show how the structure evolves / algorithm proceeds  
- Highlight differences from Example 1

### 🔥 Example 3: Edge / Complex Case

- Edge input (skewed, boundaries, tricky pattern)  
- Show how invariants and mechanics behave

### ❌ Counter-Example: What Goes Wrong

- Describe a **common mistake** or incorrect algorithm  
- Show how it fails on a small example (diagram + trace)  
- Explain which invariant is violated and why that doomed it

---

## 📊 SECTION 5: CRITICAL ANALYSIS — PERFORMANCE & ROBUSTNESS

**Goal:** Connect the mechanics to complexity and robustness.

Include:

- Complexity table for key operations/variants  
- Commentary on where Big-O misleads  
- Real memory/hardware behaviors

### 📈 Complexity Table

| 📌 Operation / Variant | 🟢 Best ⏱ | 🟡 Average ⏱ | 🔴 Worst ⏱ | 💾 Space | 📝 Notes                  |
|------------------------|-----------|-------------|-----------|---------|---------------------------|
| [Op 1]                 | O(?)      | O(?)        | O(?)      | O(?)    | [When/why]               |
| [Op 2]                 | O(?)      | O(?)        | O(?)      | O(?)    | [When/why]               |
| …                      |           |             |           |         |                           |

### 🤔 Where Big-O Misleads

- Situations where asymptotic analysis hides constants or memory effects  
- Comparisons like “same Big-O, very different real speed” (e.g., array vs linked list)

### 🖥 Real Memory Behavior

- How the structure interacts with:
  - CPU caches  
  - TLB / pages  
  - Possible disk I/O (if large)  
- Conditions that cause cache thrashing, extra allocations, etc.

### ⚠ Failure Modes

- List 3–5 important failure modes:
  - Off-by-one / boundary mistakes  
  - Invariant violations  
  - Overflow / recursion depth / memory leaks

---

## 🏭 SECTION 6: REAL SYSTEMS — PRODUCTION INTEGRATION

**Goal:** Show this is not abstract; it’s everywhere in real systems.

Include **5–10 systems** across categories (OS, DB, network, apps, cloud).

### Real Systems Table

| 🏭 System / Domain | 🧩 How This Topic Is Used          | 🎯 Benefit                       | ⚠ Pitfall if Misused          |
|--------------------|------------------------------------|----------------------------------|--------------------------------|
| [Linux kernel]     | [Use]                              | [Benefit]                        | [Pitfall]                      |
| [PostgreSQL]       | [Use]                              | [Benefit]                        | [Pitfall]                      |
| …                  |                                    |                                  |                                |

Then, for 2–3 key systems, add a short paragraph with more detail.

---

## 🔗 SECTION 7: CONCEPT CROSSOVERS — CONNECTIONS & CONTEXT

**Goal:** Place this concept within the wider DSA/map.

Include:

- What it builds on (prerequisites)  
- What builds on it (dependents)  
- How it compares to alternatives

### 📚 Prerequisites

- [Prerequisite 1]: [How it’s used here]  
- [Prerequisite 2]: [How it’s used here]  
- …

### 🚀 Dependents (What Builds on This)

- [Dependent concept 1]: [Uses this for …]  
- [Dependent concept 2]: [Extends this by …]

### 🔄 Comparison to Alternatives

A simple comparison table:

| 📌 Concept / Algorithm | ⏱ Time (core ops) | 💾 Space | ✅ Best For                     | 🔀 vs This (Key Difference)         |
|------------------------|-------------------|---------|--------------------------------|-------------------------------------|
| [Alternative A]        | O(?)              | O(?)    | [Scenario]                     | [Key difference]                    |
| [This concept]         | O(?)              | O(?)    | [Scenario]                     | [Why you’d choose this]             |

---

## 📐 SECTION 8: MATHEMATICAL PERSPECTIVE

**Goal:** Give a light but rigorous underpinning.

Include:

- A formal-ish definition (if applicable)  
- One core property or theorem with a proof sketch  
- Any relevant recurrence or model (if needed)

### 📋 Formal Definition

- State the definition in precise plain language.

### 📐 Key Theorem / Property

- **Theorem:** [Statement]  
- **Proof Sketch:** 5–10 lines explaining the main idea (not full formal proof).

### 🧩 Link to Models

- Note how this fits within the RAM model / possibly external memory model / graph model etc., if relevant.

---

## 💡 SECTION 9: ALGORITHMIC INTUITION — DECISION FRAMEWORK

**Goal:** Help learners **recognize when to use** this concept.

Include:

- A decision framework (table or simple flow)  
- “When to use” vs “When not to use”  
- Interview pattern recognition

### 🎯 Decision Framework

Optionally as a small flow or table:

| ❓ Question                          | If YES → Use This? | If NO → Consider Instead |
|-------------------------------------|---------------------|--------------------------|
| Need [property X]?                  | ✅                  | [Alternative]            |
| Data fits in memory, mostly scans? | ✅ / maybe          | [Alternative]            |
| Many random insertions in middle?  | ❌ / maybe          | [Alternative]            |

### ✅ When to Use This

- Scenario 1: [Why this concept fits]  
- Scenario 2: [Why this concept fits]  
- …

### ❌ When Not to Use (Anti-patterns)

- Scenario A: [Why it’s a bad fit]  
- Scenario B: [Better alternative and why]  

### 🔍 Interview Pattern Recognition

- **Red flags (obvious):**  
  - [Cue 1]  
  - [Cue 2]  
- **Blue flags (subtle):**  
  - [Cue 3]  
  - [Cue 4]

---

## ❓ SECTION 10: KNOWLEDGE CHECK — SOCRATIC QUESTIONS

**Goal:** Encourage self-testing and deep reasoning.

Provide 3–5 open-ended questions. No answers.

Examples:

1. Why does [core technique] succeed where [naive approach] fails?  
2. When would you choose this over [alternative]? What trade-offs are you accepting?  
3. What happens if [key invariant] is violated? Show a failing example.  
4. How does [memory behavior] affect performance in real systems?  
5. How would this structure behave under extreme edge cases?

Encourage learners to draw diagrams and run small simulations.

---

## 🎯 SECTION 11: RETENTION HOOK — MEMORY ANCHORS

**Goal:** Make the concept **sticky**.

Include:

- One-sentence essence  
- Mnemonic device (acronym/phrase)  
- Visual cue  
- Short interview story

### 💎 One-Liner Essence

“[One sentence that captures the core idea.]”

### 🧠 Mnemonic Device

- [Acronym or phrase]  
- Optional table unpacking it:

| 🔤 Letter | 🧩 Meaning         | 💡 Reminder                 |
|----------|--------------------|-----------------------------|
| X        | [Concept]          | [Short hint]                |
| Y        | [Concept]          | [Short hint]                |

### 🖼 Visual Cue

- One small ASCII or conceptual diagram that evokes the structure/behavior at a glance.

### 💼 Real Interview Story

- Very short narrative:
  - Problem context →  
  - How candidate picked this concept →  
  - What impressed the interviewer

---

## 🧩 5 COGNITIVE LENSES

**Goal:** Reflect from multiple perspectives to deepen understanding.

Each lens: 1 short paragraph (and optionally a tiny table/list).

### 🖥 Computational Lens

- How it interacts with CPU, memory hierarchy, RAM model.  
- Where the main cost comes from (pointer chasing, cache misses, etc.).  

### 🧠 Psychological Lens

- Common intuition traps and why they arise.  
- Corrected mental model and a memory aid.

### 🔄 Design Trade-off Lens

- Time vs space, simplicity vs optimization.  
- Concrete trade-off scenarios where you might choose this or alternatives.

### 🤖 AI/ML Analogy Lens

- How this concept maps to an ML idea (e.g., DP ↔ Bellman, search ↔ inference).  
- What this analogy reveals about structure, optimization, or convergence.

### 📚 Historical Context Lens

- Rough origin (who/when) and first significant systems using it.  
- How it evolved and why it remains relevant now.

---

## ⚔ SUPPLEMENTARY OUTCOMES

**Goal:** Practice, interview alignment, and deeper exploration.  
Total length: **≤ 2500 words** (recommended; not hard-enforced per section).

### ⚔ Practice Problems (8–10)

For each:

- **Title:** [Short name]  
- **Source:** [e.g., LeetCode #XXX, Codeforces, Company interview]  
- **Difficulty:** 🟢 / 🟡 / 🔴  
- **Key Concepts:** [List core ideas tested]  
- **Key Constraints:** [Input size, time/space limits, special rules]

> No solutions provided.

### 🎙 Interview Questions (6+)

For each:

- **Qn:** [Interview-style question]  
  - 🔀 **Follow-up 1:** [Variation / deeper probe]  
  - 🔀 **Follow-up 2:** [Alternative scenario / constraint change]  

> No answers provided in instructional file.

### ⚠ Common Misconceptions (3–5)

For each:

- ❌ Misconception: [Wrong belief]  
- 🧠 Why students believe it: [Short explanation]  
- ✅ Reality: [Correct understanding]  
- 💡 Memory aid: [How to remember]  
- 🎯 Impact: [How this misconception harms problem solving]

### 🚀 Advanced Concepts (3–5)

For each:

- 📈 Title: [Advanced topic]  
- 🔗 Relates to: [Core concept in this file]  
- 💼 Why important: [Short justification]  
- 🛠 Applications: [Where it shows up]  

### 🔗 External Resources (3–5)

For each:

- **Title:** [Name of book/article/video]  
- **Type:** 📖 Book / 📝 Article / 🎥 Video / 🛠 Tool / 📄 Paper  
- **Author/Source:** [Name]  
- **Why useful:** [What you’ll learn]  
- **Difficulty:** Beginner / Intermediate / Advanced  
- **Link/Reference:** [URL or citation]

---

## ✅ QUALITY CHECKLIST — FINAL VERIFICATION (INSTRUCTIONAL FILE)

- **Structure:**
  - [ ] 11 sections present, in order  
  - [ ] Cognitive Lenses block included  
  - [ ] Supplementary Outcomes included  

- **Content:**
  - [ ] All key concepts/variations listed in Section 2  
  - [ ] ≥ 3 detailed examples with traces  
  - [ ] 5–10 real systems mentioned in Section 6  
  - [ ] 8–10 practice problems  
  - [ ] 6+ interview questions with follow-ups  
  - [ ] 3–5 misconceptions, 3–5 advanced concepts, 3–5 external resources  

- **Visuals & Style:**
  - [ ] At least 2–3 tables (concepts, complexity, comparisons)  
  - [ ] At least 1 diagram/trace showing state evolution  
  - [ ] Emojis/icons used for clarity (not over-used)  
  - [ ] No LaTeX; plain text math only  
  - [ ] No code, or minimal C# only if absolutely required  

- **Word Count:**
  - [ ] Total approx. 7,500–15,000 words  

- **Cleanup:**
  - [ ] No `[PLACEHOLDER]` text remaining  
  - [ ] No internal meta-instructions left in the student-facing file  

---

# 🧩 TEMPLATE 2–6 — SUPPORT FILES (PER WEEK)

These files guide how to **study**, **integrate**, and **review** the week’s topics.

---

## 📑 TEMPLATE 2 — Week_X_Guidelines.md

**File:** `Week_X_Guidelines.md`

### 🎯 Purpose

- Provide a **roadmap** for the week  
- Clarify learning objectives & key concepts  
- Suggest how to allocate time, practice, and review

### Suggested Structure

1. **🎯 Week Overview**
   - 1–2 paragraphs: what this week is about and why it matters.

2. **🎓 Weekly Learning Objectives**
   - 6–10 bullet points for the week:
     - Understand [concepts]
     - Be able to compare [structures]
     - Recognize [patterns] from problem statements

3. **📚 Key Concepts Overview**
   - Table of days → topics → key concepts.

4. **🧭 Learning Approach & Methodology**
   - How to use instructional files:
     - Read “Why & What” first  
     - Then “How & Visualization” with diagrams  
     - Then practice problems  

5. **⏱ Time & Practice Strategy**
   - Suggested daily time blocks:
     - Concept reading / visualization  
     - Manual simulation  
     - Practice problems  
     - Review / spaced repetition

6. **🔗 Topic Connections Across the Week**
   - How Day 1 concepts feed into Day 2–5.

7. **📋 Weekly Checklist**
   - Concrete items like:
     - Draw one diagram for each major structure  
     - Simulate at least 3 problems per pattern  
     - Write down one misconception you fixed each day

---

## 📑 TEMPLATE 3 — Week_X_Summary_Key_Concepts.md

**File:** `Week_X_Summary_Key_Concepts.md`

### 🎯 Purpose

- Serve as a **quick reference** and **concept map** for the week.

### Suggested Structure

1. **📚 Week Summary**
   - 1–2 paragraphs summarizing what the week covered.

2. **📊 Key Concept Table**

| Day | Topic                         | Core Concepts               | Patterns / Techniques     |
|-----|-------------------------------|-----------------------------|---------------------------|
| 1   | [Topic]                       | [Concepts]                  | [Patterns]                |
| …   |                               |                             |                           |

3. **🧠 5 Key Insights**
   - Bullet list of biggest conceptual takeaways.

4. **⚠ 5 Misconceptions Corrected**
   - Short list of common misunderstandings and their corrections.

5. **🗺 Concept Map (ASCII)**
   - Simple diagram showing relationships between topics.

6. **📈 Mastery Progression**
   - Levels (e.g., Level 1: recall → Level 4: integrate in unseen problems).

---

## 📑 TEMPLATE 4 — Week_X_Interview_QA_Reference.md

**File:** `Week_X_Interview_QA_Reference.md`

### 🎯 Purpose

- Collect **interview-style questions** from the week in one place.

### Suggested Structure

1. **🎯 How to Use This File**
   - Short note: practice recall, think aloud, simulate mentally.

2. **🎙 Questions by Topic**
   - Group by day/topic.
   - For each:
     - Q: [Question]  
       - Follow-up 1: [Variation]  
       - Follow-up 2: [Variation]

3. **🧠 Meta Section (Optional)**
   - Patterns in what interviewers test (e.g., trade-offs, invariants, edge cases).

> Answers can be omitted (strict no-solution policy) or included here if you explicitly decide to allow answers only in this support file.

---

## 📑 TEMPLATE 5 — Week_X_Problem_Solving_Roadmap.md

**File:** `Week_X_Problem_Solving_Roadmap.md`

### 🎯 Purpose

- Provide a **practice strategy**: from simple → complex, linking problems to patterns.

### Suggested Structure

1. **🧭 Problem-Solving Framework**
   - Short general framework (understand → model → pick pattern → implement mentally → verify).

2. **📈 Progressive Practice Table**

| Stage | Goal                    | Problem Types / Sources                  | Notes                     |
|-------|-------------------------|------------------------------------------|---------------------------|
| 1     | Get familiar            | 1–2 easy problems per concept           | Focus: reading & tracing  |
| 2     | Build speed             | Medium problems mixing 2 concepts       | Focus: pattern recognition|
| 3     | Integrate               | Hard problems combining 3+ concepts     | Focus: trade-offs         |

3. **⚠ Common Pitfalls**
   - 5–7 typical mistakes seen when solving problems for this week.

4. **🔄 Review Strategy**
   - How to revisit problems:
     - “Why did I get stuck?”  
     - “Which invariant/pattern did I miss?”  

---

## 📑 TEMPLATE 6 — Week_X_Daily_Progress_Checklist.md

**File:** `Week_X_Daily_Progress_Checklist.md`

### 🎯 Purpose

- Provide a **simple checklist** to track daily progress.

### Suggested Structure

For each day (1–5):

- **Day N Topic & Goals**
  - Core concept(s)  
  - Target activities:
    - Read sections 1–4 of instructional file  
    - Draw 1–2 diagrams  
    - Simulate 2 problems by hand  
    - Attempt 2–3 practice problems  

- **✅ Checklist Items**
  - [ ] I can explain the core analogy  
  - [ ] I can draw the basic structure from memory  
  - [ ] I can state the main invariants  
  - [ ] I can list when to use/not use this pattern  
  - [ ] I attempted N practice problems

At end of week:

- **📌 Weekly Reflection**
  - One concept I deeply understand  
  - One pattern I need to revisit  
  - One misconception I corrected  
  - One real system I now understand better

---

This new template:

- Preserves all **v9.2 structural requirements** (11 sections + lenses + supplementaries).  
- Restores the **v6 mental-model, engineering, and intuition-first approach**.  
- Reduces clutter by removing per-section word quotas and inlined meta instructions.  
- Keeps the system’s **logic-first, no-code-or-C#-only, no-LaTeX, Markdown-only** constraints.  

You can now use this template for future instructional and support files.