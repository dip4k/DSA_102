# 📋 MASTER_PROMPT_v9.2_FINAL.md — DSA Curriculum Generation Master Prompt

**Version:** 9.2 (Unified Complete)  
**Status:** ✅ OFFICIAL — Master control document for instructional file generation  
**Last Updated:** December 31, 2025

---

## 🎯 PURPOSE

This master prompt defines how AI generates **instructional files** for DSA Master Curriculum v9.2. All files must conform to **TEMPLATE_v9.2_FINAL.md** and meet the **v9.2 Quality Standards**.

---

## 🔍 QUALITY STANDARDS (MANDATORY v9.2)

### Per-File Checklist

Every instructional file MUST have:

**Structure:**
- [ ] All **11 sections** present in exact order
- [ ] Clear `##` headers with emojis (v8 style)
- [ ] **Cognitive Lenses** block included (800-1200 words)
- [ ] **Supplementary Outcomes** ≤ 2500 words
- [ ] **No LaTeX** — pure Markdown only
- [ ] **C# code only if absolutely necessary** (logic-first)
- [ ] **Mermaid diagrams** preferred; ASCII optional
- [ ] **ALL CORE CONCEPTS** listed in Section 2

**Word Counts:**
```
Sections 1-10:    900-1500 | 900-1500 | 900-1500 | 900-1500 | 600-900 | 500-800 | 400-600 | 300-500 | 500-800 | 200-300
Section 11:       800-1200 words
Cognitive Lenses: 800-1200 words total
Supplementary:    ≤2500 words
TOTAL FILE:       7,500-15,000 words
```

**Content Requirements:**
- [ ] Section 1: Real problems + motivation
- [ ] Section 2: All core concepts/types/variations + diagrams
- [ ] Section 3: Step-by-step mechanics for each concept
- [ ] Section 4: 3+ visualization examples covering concepts
- [ ] Section 5: Complexity table for all concepts/variations
- [ ] Section 6: 5-10 real systems
- [ ] Section 7: Concept dependencies
- [ ] Section 8: Mathematical foundation
- [ ] Section 9: Decision framework
- [ ] Section 10: Self-assessment questions
- [ ] Section 11: Retention hook (essence + mnemonic + visual + story)
- [ ] 5 Cognitive Lenses: Computational, Psychological, Trade-off, AI/ML, Historical
- [ ] Supplementary: 8+ problems, 6+ Q&A, misconceptions, advanced, resources

**Technical Quality:**
- [ ] Grammar & spelling: Perfect
- [ ] Professional tone throughout
- [ ] Diagrams: Clear, labeled, accurate
- [ ] Tables: Properly formatted Markdown
- [ ] No assumptions — define all terms
- [ ] Examples: Detailed, step-by-step

---

## 📋 GENERATION WORKFLOW

### Step 1: Topic Selection
- Choose topic from **COMPLETE_SYLLABUS_v9.2_FINAL.md**
- **CRITICAL:** Include **Week 4.75** (String Patterns) and restored v8 topics (Cyclic Sort, etc.)
- Identify all **core concepts, types, or variations** for that topic (must be comprehensive)
- Map to learning tier (Tier 1, 1.5, 2, 3)

### Step 2: Template Initialization
- Copy entire **TEMPLATE_v9.2_FINAL.md**
- Replace `[Week_X_Day_Y_Topic]` with actual values
- Ensure all placeholders visible

### Step 3: Content Generation
- **Section 1 (Why):** Motivate with 2-3 real problems (Business impact, systems)
- **Section 2 (What):** Define all concepts/variations + show visual for each (Mandatory list)
- **Section 3 (How):** Explain mechanics step-by-step (Logic first, minimal code)
- **Section 4 (Viz):** Provide examples for different variants (Simple, Medium, Complex)
- **Section 5 (Analysis):** Complexity table covering all concepts (Time/Space/Cache)
- **Section 6 (Systems):** Link to 5-10 real systems (OS, DB, Network, etc.)
- **Section 7 (Links):** Map prerequisites and dependent topics
- **Section 8 (Math):** Mathematical foundation & proofs
- **Section 9 (Intuition):** Decision framework & pattern recognition
- **Section 10 (Check):** 3-5 self-assessment questions
- **Section 11 (Hook):** Essence + mnemonic + visual cue + interview story
- **Cognitive Lenses:** All 5 perspectives (800-1200 words)
- **Supplementary:** 8+ problems (No solutions), 6+ Q&A (No solutions), misconceptions, advanced, resources

### Step 4: Word Count Verification
- Section-by-section word count check
- Ensure total 7,500-15,000 words
- Adjust content density if needed

### Step 5: Quality Checklist
- Verify all 11 sections present
- Confirm cognitive lenses included
- Check emoji usage consistency (v8 style)
- Verify no LaTeX encoding
- Confirm all core concepts covered
- Validate Markdown formatting

### Step 6: File Naming & Submission
- Save as: `[Week_X_Day_Y_Topic]_Instructional.md`
- Submit for peer/AI review
- Flag if any checklist items incomplete

---

## 🧩 5 COGNITIVE LENSES (MANDATORY)

Every instructional file must include these 5 perspectives:

### 🖥️ Computational Lens (200-250 words)
- How CPU/memory architecture impacts this topic
- Cache behavior (L1/L2/L3)
- RAM model considerations
- Modern hardware realities

### 🧠 Psychological Lens (200-250 words)
- Common misconceptions students have
- Why these misconceptions seem plausible
- Correct mental model
- Proven memory aids

### 🔄 Design Trade-off Lens (200-250 words)
- Time vs Space trade-offs
- Simple vs Optimized implementations
- When to choose each variant
- Cost-benefit analysis

### 🤖 AI/ML Analogy Lens (200-250 words)
- Connection to ML training concepts
- Relation to gradient descent, Bellman equation, etc.
- Neural network parallels
- Inference analogies

### 📚 Historical Lens (200-250 words)
- Who invented this concept & when
- First real-world systems
- Evolution over time
- Why it survived/thrived

**Total: 800-1200 words across all 5 lenses**

---

## 📊 SUPPLEMENTARY OUTCOMES (MAX 2500 words)

### Practice Problems (8+)
- Source: LeetCode, Codeforces, interviews, textbooks
- Difficulty: Easy, Medium, Hard, Challenge
- Concepts tested: Clearly labeled
- Constraints: Important limits noted
- **NO SOLUTIONS PROVIDED**

### Interview Q&A (6+)
- Real questions from interviews
- **NO ANSWERS PROVIDED** (Use "NO SOLUTIONS PROVIDED")
- 2+ follow-ups per question
- Complexity discussion (Question only)

### Common Misconceptions (3-5)
- Misconception stated
- Reality explained
- Why it matters
- Memory aid to prevent error

### Advanced Concepts (3-5)
- Topic name
- Prerequisite knowledge
- How it extends this topic
- When to use
- Learning resources

### External Resources (3-5)
- Type: Video, Book, Article, Tool, Paper
- Title & Author
- What it teaches
- Difficulty level
- Link/reference

---

## 🚀 TIPS FOR HIGH QUALITY

**Do:**
- ✅ Use examples with actual data
- ✅ Show state changes visually
- ✅ Compare variants explicitly
- ✅ Link to real systems (5-10 min)
- ✅ Provide decision frameworks
- ✅ Use Mermaid for complex flows
- ✅ Verify all concepts covered
- ✅ Test word counts section-by-section

**Don't:**
- ❌ Use LaTeX math notation
- ❌ Include code unless essential (C# only)
- ❌ Assume prior knowledge
- ❌ Skip edge cases
- ❌ Forget supplementary outcomes
- ❌ Miss any core concepts
- ❌ Use complex jargon without definition

---

## 📁 REFERENCE FILES

- **TEMPLATE_v9.2_FINAL.md** — The gold standard template
- **EMOJI_ICON_GUIDE_v8.md** — Emoji standards
- **SYSTEM_CONFIG_v9.2_FINAL.md** — Global curriculum specs
- **COMPLETE_SYLLABUS_v9.2_FINAL.md** — Topic list

---

**Status:** ✅ **READY FOR GENERATION**  
**Version:** 9.2 (Unified)  
**Updated:** December 31, 2025