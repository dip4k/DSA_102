# ⚙️ SYSTEM_CONFIG_v9.1_FINAL.md — Unified Quality & Structure Standards

**Version:** 9.1 (with v9.1 Quality Standards Integration)  
**Scope:** All instructional and support files in DSA Master Curriculum v9.1  
**Last Updated:** December 30, 2025

---

## 🎯 Global Curriculum Specs

- **Duration:** 16.75 weeks (including Week 4.75)
- **Teaching Days:** 85
- **Core Topics:** 75+
- **Patterns:** 60+ (including 4 new string patterns)
- **Interview Coverage:** 98%+
- **String Coverage:** 90%+
- **Total Files:** 165+ (≈60 instructional, 105+ support)
- **Total Words:** 460,000–570,000

---

## 📚 INSTRUCTIONAL FILE REQUIREMENTS (v9.1)

### Structure & Formatting

**All 11 sections (MANDATORY):**
1. 🤔 The Why (900-1500 words)
2. 📌 The What (900-1500 words)
3. ⚙️ The How (900-1500 words)
4. 🎨 Visualization (900-1500 words)
5. 📊 Critical Analysis (600-900 words)
6. 🏭 Real Systems (500-800 words)
7. 🔗 Concept Crossovers (400-600 words)
8. 📐 Mathematical (300-500 words)
9. 💡 Algorithmic Intuition (500-800 words)
10. ❓ Knowledge Check (200-300 words)
11. 🎯 Retention Hook (800-1200 words)

**Plus:**
- 🧩 **5 Cognitive Lenses Block:** 800-1200 words total
- ⚔️ **Supplementary Outcomes:** ≤2500 words max

**Formatting:**
- Headings use Markdown `##` / `###` with emojis
- No LaTeX; use plain Markdown text
- Use Mermaid where diagrams help; ASCII as backup
- All code (if any) in C# only

### Word Count Targets

```
Per-Section Breakdown:
Sec 1 (Why):         900-1500 words  — Real problems + motivation
Sec 2 (What):        900-1500 words  — All operations + concepts
Sec 3 (How):         900-1500 words  — Mechanics for each operation
Sec 4 (Viz):         900-1500 words  — 3+ examples per operation
Sec 5 (Analysis):    600-900 words   — Complexity + edge cases
Sec 6 (Systems):     500-800 words   — 5-10 real-world systems
Sec 7 (Crossovers):  400-600 words   — Prerequisites + dependencies
Sec 8 (Math):        300-500 words   — Formal foundation
Sec 9 (Intuition):   500-800 words   — Decision framework
Sec 10 (Check):      200-300 words   — Self-assessment questions

Section 11:          800-1200 words  — Essence + mnemonic + visual + story

Cognitive Lenses:    800-1200 words  — All 5 perspectives combined

Supplementary:       ≤2500 words     — Problems, Q&A, misconceptions, advanced, resources

TOTAL PER FILE:      7,500-15,000 words
```

---

## 🧩 COGNITIVE LENSES (MANDATORY — v9.1 Addition)

Every instructional file must include a dedicated **"Cognitive Lenses"** block covering:

### 🖥️ Computational Lens (200-250 words)
- CPU/memory architecture impact
- Cache behavior & optimization
- RAM model considerations

### 🧠 Psychological Lens (200-250 words)
- Common misconceptions
- Why they seem plausible
- Correct mental model

### 🔄 Design Trade-off Lens (200-250 words)
- Time vs Space trade-offs
- When to pick each variant
- Cost-benefit analysis

### 🤖 AI/ML Analogy Lens (200-250 words)
- Connection to ML concepts
- Gradient descent, Bellman, etc.
- Neural network parallels

### 📚 Historical Lens (200-250 words)
- Origin & evolution
- First implementations
- Real-world adoption timeline

**Placement:** After Section 11 or before Supplementary Outcomes  
**Total:** 800-1200 words across all 5

---

## ⚔️ SUPPLEMENTARY OUTCOMES SPECIFICATION

Per instructional file (MAX 2500 words total):

### Practice Problems (8+)
- Platform (LeetCode, Codeforces, interview, textbook)
- Difficulty (Easy/Medium/Hard/Challenge)
- What it tests (concept coverage)
- Constraints (important limits)
- **NO SOLUTIONS PROVIDED**

### Interview Q&A (6+ pairs)
- Real interview question
- Detailed answer (150-250 words)
- 2+ follow-up variations
- Complexity discussion

### Common Misconceptions (3-5)
- Misconception statement
- Reality explanation
- Why it matters
- Prevention memory aid

### Advanced Concepts (3-5)
- Topic name
- Prerequisites needed
- Extension relationship
- When to use
- Resources

### External Resources (3-5)
- Type (Video/Book/Article/Tool/Paper)
- Title & author
- What it teaches
- Difficulty level
- Full link/reference

---

## 💻 CODE & DIAGRAM POLICY (v9.1)

### Code Policy
**Default: NO CODE** — Use clear English + pseudocode
- Only if **absolutely necessary** for clarity
- Use **C# exclusively** if required
- Keep snippets short & conceptual
- Focus on logic, not syntax

### Diagram Policy
- **Prefer Mermaid** for flowcharts, state machines, graphs
- Support with ASCII for step traces
- Must be:
  - Correct & accurate
  - Clearly labeled
  - Legible in plain Markdown
  - Directly relevant

---

## 📊 PER-WEEK CONTENT EXPECTATIONS

For a typical teaching week (5 days):

- **Instructional Files:** 5-7
- **Support Files:** 6-7
- **Total Words:** 30,000-35,000
- **Practice Problems:** 40-50
- **Interview Q&A:** 50+
- **Real Systems Examples:** 25-30

*Special weeks (4.5, 4.75, 5.5, 13) may vary slightly but follow same per-file standards*

---

## 🧾 PER-FILE QUALITY CHECKLIST

Before submitting any instructional file:

**Structure:**
- [ ] All 11 sections present
- [ ] Sections in correct order
- [ ] Cognitive Lenses block included
- [ ] Supplementary Outcomes block included

**Word Counts:**
- [ ] Each section within defined range
- [ ] Cognitive Lenses: 800-1200 words
- [ ] Supplementary: ≤2500 words
- [ ] Total file: 7,500-15,000 words

**Content Quality:**
- [ ] No LaTeX (pure Markdown)
- [ ] No code or minimal C# only
- [ ] 3+ worked examples
- [ ] Complexity table included
- [ ] 5-10 real systems referenced
- [ ] 8+ practice problems
- [ ] 6+ interview Q&A pairs
- [ ] 5 cognitive lenses covered

**Technical Correctness:**
- [ ] Time/space analysis accurate
- [ ] Examples match algorithms
- [ ] All operations covered
- [ ] Claims verified

---

## 🔗 REFERENCE INTEGRATION

This config works with:
- **TEMPLATE_v9.1.md** — Instructional template
- **MASTER_PROMPT_v9.1.md** — Generation guidance
- **EMOJI_ICON_GUIDE_v8.md** — Visual consistency
- **COMPLETE_SYLLABUS_WEEKS_1_TO_16_v8_FINAL.md** — Topic list

---

## ✅ Status

- **Curriculum Version:** 9.1
- **Quality Standard:** This document + TEMPLATE_v9.1.md
- **Usage:** All generators follow this config + template

**Last Updated:** December 30, 2025
