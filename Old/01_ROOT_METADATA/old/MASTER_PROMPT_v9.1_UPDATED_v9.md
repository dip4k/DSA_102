# 📋 MASTER_PROMPT_v9.1.md — DSA Curriculum Generation Master Prompt (UPDATED v9)

**Version:** 9.1 (Updated with v9 Syllabus Integration)  
**Status:** ✅ OFFICIAL — Master control document for instructional file generation  
**Last Updated:** December 30, 2025 (Updated for v9 Syllabus with Week 4.75)

---

## 🎯 PURPOSE

This master prompt defines how AI generates **instructional files** for DSA Master Curriculum v9.1. All files must conform to **TEMPLATE_v9.1.md** and meet the **v9.1 Quality Standards**.

---

## 🔍 QUALITY STANDARDS (MANDATORY v9.1)

### Per-File Checklist

Every instructional file MUST have:

**Structure:**
- [ ] All **11 sections** present in exact order
- [ ] Clear `##` headers with emojis
- [ ] **Cognitive Lenses** block included (800-1200 words)
- [ ] **Supplementary Outcomes** ≤ 2500 words
- [ ] **No LaTeX** — pure Markdown only
- [ ] **C# code only if absolutely necessary** (logic-first)
- [ ] **Mermaid diagrams** preferred; ASCII optional
- [ ] **ALL CORE OPERATIONS** listed in Section 2

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
- [ ] Section 2: All operations with variants + diagrams
- [ ] Section 3: Step-by-step mechanics for each operation
- [ ] Section 4: 3+ visualization examples covering operations
- [ ] Section 5: Complexity table for all operations
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
- Choose topic from **COMPLETE_SYLLABUS_v9_FINAL.md** ✅ **[UPDATED FOR v9]**
- Note: v9 includes new **Week 4.75 String Patterns** (4 days)
- Identify all **core operations** for that topic (must be comprehensive)
- Map to learning tier (Tier 1, 1.5, 2, 3)

### Step 2: Template Initialization
- Copy entire TEMPLATE_v9.1.md
- Replace `[Week_X_Day_Y_Topic]` with actual values
- Ensure all placeholders visible

### Step 3: Content Generation
- Section 1: Motivate with 2-3 real problems
- Section 2: Define all operations + show visual for each
- Section 3: Explain how to execute each operation
- Section 4: Provide examples for different operation variants
- Section 5: Create complexity table covering all operations
- Section 6: Link to 5-10 real systems using operations
- Section 7: Map prerequisites and dependent topics
- Section 8: Provide mathematical foundation if applicable
- Section 9: Decision framework for operation selection
- Section 10: 3-5 self-assessment questions
- Section 11: Essence + mnemonic + visual cue + interview story
- Cognitive Lenses: All 5 perspectives (800-1200 words)
- Supplementary: 8+ problems, 6+ Q&A, misconceptions, advanced, resources

### Step 4: Word Count Verification
- Section-by-section word count check
- Ensure total 7,500-15,000 words
- Adjust content density if needed

### Step 5: Quality Checklist
- Verify all 11 sections present
- Confirm cognitive lenses included
- Check emoji usage consistency
- Verify no LaTeX encoding
- Confirm all operations covered
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
- NO SOLUTIONS PROVIDED

### Interview Q&A (6+)
- Real questions from interviews
- Detailed answers (150-250 words each)
- 2+ follow-ups per question
- Complexity discussion
- Why this question matters

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
- ✅ Link to real systems
- ✅ Provide decision frameworks
- ✅ Use Mermaid for complex flows
- ✅ Verify all operations covered
- ✅ Test word counts section-by-section

**Don't:**
- ❌ Use LaTeX math notation
- ❌ Include code unless essential
- ❌ Assume prior knowledge
- ❌ Skip edge cases
- ❌ Forget supplementary outcomes
- ❌ Miss any core operations
- ❌ Use complex jargon without definition

---

## 📁 REFERENCE FILES

- **TEMPLATE_v9.1.md** — The gold standard template
- **EMOJI_ICON_GUIDE_v8.md** — Emoji standards
- **SYSTEM_CONFIG_v9_FINAL.md** — Global curriculum specs
- **COMPLETE_SYLLABUS_v9_FINAL.md** — Topic list ✅ **[UPDATED FOR v9]**

---

**Status:** ✅ **READY FOR GENERATION**  
**Version:** 9.1 (Updated for v9 Syllabus)  
**Updated:** December 30, 2025
