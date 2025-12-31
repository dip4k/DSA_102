🎯 WEEKLY_BATCH_GENERATION_PROMPT_v10.md — MENTAL-MODEL-FIRST BATCH CONTENT GENERATION

Version: 10.0 (Aligned with Template_v10 & SYSTEM_CONFIG_v10)  
Status: ✅ OFFICIAL WEEKLY GENERATION PROMPT  
Purpose: Generate all **weekly instructional & support files** systematically for DSA Master Curriculum v9.2, using the **mental-model-first**, systems-focused approach.

---

## 📋 HOW TO USE THIS PROMPT

Use this prompt inside an AI chat session that already has:

- `SYSTEM_CONFIG_v10_FINAL.md`
- `MASTER_PROMPT_v10_FINAL.md`
- `Template_v10.md` (Instructional & Support Template, Mental-Model-First)
- `COMPLETE_SYLLABUS_v10_FINAL.md`
- `EMOJI_ICON_GUIDE_v8.md`

All **instructional files** MUST:

- Follow `Template_v10.md`  
- Comply with `SYSTEM_CONFIG_v10_FINAL.md`  
- Use **mental models, mechanical explanations, and visual reasoning**  
- Respect the **NO SOLUTIONS PROVIDED** policy in Supplementary Outcomes

All **support files** MUST:

- Follow the support templates in `Template_v10.md`  
- Be concise, practical, and visual (checklists, roadmaps, concept maps)  

---

## ⚠️ BATCH GENERATION TO AVOID TOKEN LIMITS

Do **NOT** generate all weekly files in a single response.

**Batching Rules:**

1. Generate **only 1 file per response** (one markdown file).  
2. Wait for the user to acknowledge/save it.  
3. Only then move to the next file.  
4. If the user says:  
   - `"Continue with next file"` → generate the next requested file.  
   - `"Pause"` → stop file generation until explicitly resumed.

You MUST never bundle multiple files (instructional or support) in the same response.

---

## 🎓 DSA MASTER CURRICULUM v9.2 — WEEKLY BATCH GENERATION REQUEST

### 📌 CURRENT WEEK TARGET

**Generation Request:** Complete Week [X] file set  

- **Week Number:** [X] (1–16, 4.5, 5.5, 4.75, 13, etc., as per `COMPLETE_SYLLABUS_v10_FINAL.md`)  
- **Duration:** [X] days (usually 5; some weeks 4 or 6–7)  
- **Total Files:** Typically 11 instructional + 5 support per week  

All instructional files must:

- Follow `Template_v10.md` (**mental-model-first**)  
- Use generic **concept/type/variation** wording in Section 2 (not operation-only)  
- Use **visuals** (diagrams, tables, flows) to clarify concepts  
- Respect **NO SOLUTIONS PROVIDED** for practice & interview questions  

---

## 🎯 DELIVERABLES REQUIRED (v10)

### PART 1: INSTRUCTIONAL FILES (PER WEEK)

For Week [X], generate the following **instructional files** (topics from `COMPLETE_SYLLABUS_v10_FINAL.md`):

- `Week_[X]_Day_1_[Topic_Name]_Instructional.md`  
- `Week_[X]_Day_2_[Topic_Name]_Instructional.md`  
- `Week_[X]_Day_3_[Topic_Name]_Instructional.md`  
- `Week_[X]_Day_4_[Topic_Name]_Instructional.md`  
- `Week_[X]_Day_5_[Topic_Name]_Instructional.md`  
- (+ `Day_6` / `Day_7` instructional files if the week has 6–7 days)

Each instructional file MUST follow the **11-section structure**:

1. 🤔 The Why — Engineering Motivation  
2. 📌 The What — Mental Model & Core Concepts  
3. ⚙ The How — Mechanical Walkthrough  
4. 🎨 Visualization — Simulation & Examples  
5. 📊 Critical Analysis — Performance & Robustness  
6. 🏭 Real Systems — Integration in Production  
7. 🔗 Concept Crossovers — Connections & Comparisons  
8. 📐 Mathematical & Theoretical Perspective  
9. 💡 Algorithmic Design Intuition  
10. ❓ Knowledge Check — Socratic Reasoning  
11. 🎯 Retention Hook — Memory Anchors  

Plus:

- 🧩 **Cognitive Lenses Block** (5 lenses):
  - 🖥 Computational  
  - 🧠 Psychological  
  - 🔄 Design Trade-off  
  - 🤖 AI/ML Analogy  
  - 📚 Historical  

- ⚔ **Supplementary Outcomes Block**:
  - ⚔ Practice Problems: 8–10 (with source, difficulty, concepts, constraints) — **NO SOLUTIONS PROVIDED**  
  - 🎙 Interview Questions: 6+ (questions + 2+ follow-ups) — **NO SOLUTIONS PROVIDED**  
  - ⚠ Common Misconceptions: 3–5 (misconception, why plausible, reality, memory aid, impact)  
  - 🚀 Advanced Concepts: 3–5 (relation, when to use, why important)  
  - 🔗 External Resources: 3–5 (title, type, author, why useful, difficulty, link)  

#### 🎨 Visual & Structural Requirements (Instructional Files)

Each instructional file must include:

- At least **1 concept summary or comparison table** (e.g., in Section 2 or Section 7)  
- **1 complexity table** (Section 5)  
- **2–3 visuals** overall:
  - Diagrams (ASCII or simple Mermaid)  
  - Traces in table form  
  - Decision/flow charts for algorithm choice

#### 🧾 Format & Code

- **Markdown only** (`.md`), UTF-8, LF line endings  
- **No LaTeX** syntax or encoding (plain text math only: `O(n log n)`)  
- **No code by default**; if absolutely necessary:
  - Use **minimal C#** only  
  - No Python/Java/C++ blocks

#### ⏱ Word Count

- **Target per instructional file:** 7,500–15,000 words total  
- No per-section quotas; use length where it adds real understanding.

---

## PART 2: SUPPORT FILES (5 PER WEEK)

For Week [X], generate the following **support files**, aligned with `Template_v10.md` (v6-style content, v10 philosophy):

1. **`Week_[X]_Guidelines.md`**  
   Include:
   - Week overview & theme  
   - Weekly learning objectives (6–10)  
   - Key concepts overview (per day)  
   - Learning approach & methodology (mental-model-first, no-code simulation)  
   - Common mistakes & pitfalls  
   - Time & practice strategy  
   - Weekly checklist  

2. **`Week_[X]_Summary_Key_Concepts.md`**  
   Include:
   - Week overview (1–2 paragraphs)  
   - Per-day key concept summaries (bullets)  
   - Concept map (ASCII or bullet)  
   - Comparison / relationship table(s)  
   - 5–7 key insights  
   - 5 common misconceptions fixed  

3. **`Week_[X]_Interview_QA_Reference.md`**  
   Include:
   - Brief intro: how to use this file  
   - 30–50 interview-style questions (no answers, unless explicitly allowed), grouped by topic/day  
   - Each with 1–2 follow-up variations  
   - Usage suggestions for mock interviews / self-practice  

4. **`Week_[X]_Problem_Solving_Roadmap.md`**  
   Include:
   - Overall problem-solving framework for the week  
   - Progressive practice plan (simple → medium → integrated/hard)  
   - Common problem-solving pitfalls  
   - Pattern templates/skeletons (text form) for major approaches  

5. **`Week_[X]_Daily_Progress_Checklist.md`**  
   Include:
   - For each day:
     - Top concepts to understand  
     - Concrete activities (trace examples, draw diagrams, attempt problems)  
     - Simple checklist  
   - Weekly integration/reflection section

All support files:

- Must be **Markdown only**  
- Must **not** contain LaTeX or code blocks  
- Should be concise, visual, and action-oriented

---

## 📋 FORMAT & QUALITY REQUIREMENTS (v10)

### Format

- Markdown `.md`, UTF-8, LF line endings  
- Use `#`, `##`, `###` heading levels with emojis per `EMOJI_ICON_GUIDE_v8.md`  
- Plain-text math (`O(n log n)`) only  
- No LaTeX, no HTML heavy formatting

### Content Style

- Mental-model-first:
  - Analogies, diagrams, and state traces  
  - Mechanical understanding and invariants  
  - Trade-off and system thinking  

- No code by default; logic-first descriptions  
- C# snippets only when absolutely necessary to clarify key mechanics

---

## 🚚 DELIVERY & BATCHING RULES

### File Delivery Method

- ✅ Generate **ONE FILE PER RESPONSE** (instructional or support)  
- ✅ Clearly label the file name at the top (e.g., `# Week_1_Day_1_RAM_Model_And_Pointers_Instructional.md`)  
- ✅ Include the entire content of that file in Markdown format  
- ✅ After generating a file, **stop** and wait for user signal

### Delivery Order (Recommended)

1. All instructional files for Week [X], in order:
   - Day 1 → Day 2 → Day 3 → Day 4 → Day 5 (→ Day 6/7 if applicable)
2. Then all 5 support files for Week [X].

**Example interaction pattern:**

1. User: `Generate Week_1_Day_1_RAM_Model_And_Pointers_Instructional.md`  
2. AI: Returns that file only (full Markdown, mental-model-first content).  
3. User: `Continue with Day 2`  
4. AI: Returns `Week_1_Day_2_Asymptotic_Analysis_Instructional.md` only.  
5. User: `Generate Week_1_Guidelines.md` (after all instructional files)  
6. AI: Returns that support file only.  
7. And so on.

---

## ✅ VERIFICATION CHECKLIST (PER INSTRUCTIONAL FILE)

Before accepting an instructional file as complete, verify:

- **Structure & Blocks**
  - [ ] Header present, correct metadata  
  - [ ] All 11 sections present, in order  
  - [ ] Cognitive Lenses block present (5 lenses)  
  - [ ] Supplementary Outcomes present  

- **Content & Coverage**
  - [ ] Section 2 lists all relevant concepts/variations/subtypes  
  - [ ] At least 3 examples with trace/visualization  
  - [ ] 5–10 real systems described in Section 6  
  - [ ] Practice problems: 8–10 (no solutions)  
  - [ ] Interview questions: 6+ with follow-ups (no solutions)  
  - [ ] Misconceptions: 3–5  
  - [ ] Advanced concepts: 3–5  
  - [ ] External resources: 3–5  

- **Visual & Technical**
  - [ ] At least 1 concept summary / comparison table  
  - [ ] Complexity table present (Section 5)  
  - [ ] 2–3 visuals total (diagrams, traces, flows)  
  - [ ] No LaTeX syntax; math written in plain text  
  - [ ] No code or only minimal C# if absolutely needed  
  - [ ] Markdown formatting valid and readable  

- **Word Count**
  - [ ] Estimated total 7,500–15,000 words  

---

## ✅ VERIFICATION CHECKLIST (PER SUPPORT FILE SET, WEEK X)

- [ ] `Week_X_Guidelines.md` present  
- [ ] `Week_X_Summary_Key_Concepts.md` present  
- [ ] `Week_X_Interview_QA_Reference.md` present  
- [ ] `Week_X_Problem_Solving_Roadmap.md` present  
- [ ] `Week_X_Daily_Progress_Checklist.md` present  
- [ ] All in Markdown, no LaTeX, no code blocks  
- [ ] Content is concise, visual, and action-oriented (not duplicate of instructional files)

---

**Status:** ✅ WEEKLY BATCH GENERATION PROMPT v10 — Ready for use with Template_v10 & SYSTEM_CONFIG_v10
