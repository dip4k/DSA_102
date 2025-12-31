# 🔗 IMPLEMENTATION_GUIDE_v9.1.md — How to Use the Updated Standards

**Version:** 9.1  
**Purpose:** Quick reference for implementing v9.1 across the curriculum  
**Last Updated:** December 30, 2025

---

## 📋 SUMMARY OF ALL CHANGES

### NEW v9.1 FILES (Created December 30, 2025)

1. **TEMPLATE_v9.1.md** ✅
   - Complete template with all 11 sections + operations coverage
   - Operations placeholder for comprehensive listings
   - Cognitive Lenses block integrated
   - Word count ranges per section (7,500-15,000 total)
   - Generic file naming: `[Week_X_Day_Y_Topic]_Instructional.md`

2. **MASTER_PROMPT_v9.1.md** ✅
   - Generation workflow guide
   - Quality standards checklist
   - Content requirements per section
   - 5 Cognitive Lenses specification
   - Tips for high-quality generation

3. **SYSTEM_CONFIG_v9.1.md** ✅
   - Global curriculum specifications
   - Per-file requirements (word counts, structure)
   - Cognitive Lenses mandatory block
   - Supplementary Outcomes specification
   - Code & diagram policies

4. **SYSTEM_PROMPT_v9.1_FOR_AI.md** ✅
   - AI-optimized system prompt
   - Strict rules for generation
   - Section-by-section guidance
   - Pre/during/post-generation checklists
   - Verification procedures

5. **README_v9.1_QUALITY_STANDARDS.md** ✅
   - Complete guide for v9.1
   - What's new summary
   - Quality standards reference
   - Quick start instructions
   - Submission checklist

6. **IMPLEMENTATION_GUIDE_v9.1.md** ✅
   - This file — integration instructions

---

## 🔄 HOW TO INTEGRATE INTO YOUR WORKFLOW

### Step 1: Update System Memory
Add to your system memory/context:

```
ACTIVE CURRICULUM VERSION: 9.1
TEMPLATE: TEMPLATE_v9.1.md
MASTER PROMPT: MASTER_PROMPT_v9.1.md
SYSTEM CONFIG: SYSTEM_CONFIG_v9.1.md
AI PROMPT: SYSTEM_PROMPT_v9.1_FOR_AI.md
QUALITY STANDARD: MANDATORY v9.1 checklist
```

### Step 2: Establish New Standards

**Old Standard (v8):**
- 5,500-10,500 words per file
- 8-10 sections (not fixed)
- No cognitive lenses
- Optional supplementary outcomes

**New Standard (v9.1):**
- 7,500-15,000 words per file ✅
- Exactly 11 sections + Cognitive Lenses + Supplementary ✅
- MANDATORY 5 Cognitive Lenses (800-1200 words) ✅
- MANDATORY Supplementary Outcomes (≤2500 words) ✅
- ALL core operations must be listed in Section 2 ✅

### Step 3: File Naming Convention

**New naming (v9.1):**
```
[Week_X_Day_Y_Topic]_Instructional.md

Example:
Week_1_Day_1_Introduction_To_Arrays_Instructional.md
Week_4_75_Day_1_Palindrome_Patterns_Instructional.md
Week_5_Day_3_Backtracking_Complete_Instructional.md
```

**Old naming (v8):** Varied formats — DEPRECATED

### Step 4: Quality Assurance

Before accepting any file, verify:

```
✅ Structure Checklist:
   - 11 sections present?
   - Cognitive Lenses included?
   - Supplementary Outcomes included?
   - Proper emoji usage?

✅ Word Count Checklist:
   - Section 1 (Why): 900-1500?
   - Section 2 (What): 900-1500?
   - Section 3 (How): 900-1500?
   - Section 4 (Viz): 900-1500?
   - Section 5 (Analysis): 600-900?
   - Section 6 (Systems): 500-800?
   - Section 7 (Crossovers): 400-600?
   - Section 8 (Math): 300-500?
   - Section 9 (Intuition): 500-800?
   - Section 10 (Check): 200-300?
   - Section 11 (Hook): 800-1200?
   - Cognitive Lenses: 800-1200?
   - Supplementary: ≤2500?
   - TOTAL: 7,500-15,000?

✅ Content Checklist:
   - ALL core operations listed in Section 2?
   - Each operation has visual diagram?
   - Complexity table covers all operations?
   - 5-10 real systems mentioned?
   - 8+ practice problems provided?
   - 6+ interview Q&A pairs?
   - All 5 cognitive lenses covered?
   - No LaTeX (pure Markdown)?
   - C# only (or no code)?
   - Grammar perfect?
```

---

## 🎯 IMPLEMENTATION TIMELINE

### Phase 1: Immediate (Dec 30, 2025)
- ✅ Create all v9.1 documents (DONE)
- ✅ Update system memory
- ✅ Establish quality standards
- [ ] Review and approve v9.1 files

### Phase 2: Short-term (Next 5 days)
- [ ] Generate first 3-5 files using v9.1
- [ ] Test checklist procedures
- [ ] Refine guidance based on generation experience
- [ ] Update any outdated reference materials

### Phase 3: Mid-term (2-3 weeks)
- [ ] All new instructional files use v9.1
- [ ] Legacy v8 files reviewed for compatibility
- [ ] Complete first full week of v9.1 content
- [ ] Documentation refined

### Phase 4: Full rollout (Ongoing)
- [ ] All 60+ instructional files converted to v9.1
- [ ] Consistent 7,500-15,000 word count
- [ ] All cognitive lenses included
- [ ] 460,000-570,000 total curriculum words

---

## 📚 DOCUMENT HIERARCHY

```
Level 1 — AUTHORITY DOCUMENTS
├── TEMPLATE_v9.1.md               [Structure authority]
├── SYSTEM_CONFIG_v9.1.md          [Standards authority]
└── MASTER_PROMPT_v9.1.md          [Generation authority]

Level 2 — GUIDANCE DOCUMENTS
├── SYSTEM_PROMPT_v9.1_FOR_AI.md   [AI instruction]
├── README_v9.1_QUALITY_STANDARDS.md [Educational]
└── IMPLEMENTATION_GUIDE_v9.1.md   [This file]

Level 3 — SUPPORT DOCUMENTS
├── EMOJI_ICON_GUIDE_v8.md         [Visual reference]
├── COMPLETE_SYLLABUS_WEEKS_1_TO_16_v8_FINAL.md [Content map]
└── [Week_X_Day_Y_Topic]_Instructional.md [Generated files]
```

---

## ✅ VERIFICATION PROTOCOL

### Pre-Generation Checklist
```
Before asking AI/human to generate a file:
- [ ] Topic identified (Week X, Day Y, specific concept)
- [ ] All core operations identified (MUST be complete)
- [ ] Real systems examples sourced (5-10 minimum)
- [ ] Interview problems identified (8+ minimum)
- [ ] TEMPLATE_v9.1.md reference available
- [ ] SYSTEM_PROMPT_v9.1_FOR_AI.md will be provided
```

### Post-Generation Checklist
```
After file is generated, verify:
- [ ] All 11 sections present (use checklist from template)
- [ ] Cognitive Lenses block included (all 5)
- [ ] Supplementary Outcomes complete
- [ ] Each section word count verified
- [ ] Total word count: 7,500-15,000
- [ ] Section 2 lists ALL operations
- [ ] No LaTeX (pure Markdown)
- [ ] C# only or no code
- [ ] Emojis consistent
- [ ] Grammar perfect
- [ ] Examples detailed
- [ ] Diagrams clear + labeled

If ANY item fails: Revise and recheck
If ALL items pass: APPROVED ✅
```

---

## 🚀 TRANSITION TIPS

### For Manual Writers
- Keep TEMPLATE_v9.1.md open while writing
- Track word counts per section (use counter)
- Write operations section (Section 2) FIRST
- Ensure ALL operations are covered
- Add cognitive lenses as separate section
- Complete supplementary outcomes last
- Run checklist before submitting

### For AI Users
- Copy SYSTEM_PROMPT_v9.1_FOR_AI.md into context
- Provide topic + all operations list
- Reference TEMPLATE_v9.1.md explicitly
- Let AI generate complete file
- Verify checklist items before accepting
- If checklist fails: Request specific revisions

### For Reviewers
- Use provided checklist (don't create custom ones)
- Verify word counts section-by-section (not just total)
- Confirm ALL operations in Section 2 (common miss)
- Ensure cognitive lenses are truly present (not just mentioned)
- Check that supplementary outcomes meet minimums

---

## 🔗 CROSS-REFERENCES

**Need to understand something? Find it here:**

| Question | Answer in |
|----------|-----------|
| How do I generate a file? | MASTER_PROMPT_v9.1.md |
| What structure should I follow? | TEMPLATE_v9.1.md |
| What are the standards? | SYSTEM_CONFIG_v9.1.md |
| How do I instruct AI? | SYSTEM_PROMPT_v9.1_FOR_AI.md |
| What's the overview? | README_v9.1_QUALITY_STANDARDS.md |
| How do I verify quality? | Any checklist above |
| What emojis should I use? | EMOJI_ICON_GUIDE_v8.md |
| What topics exist? | COMPLETE_SYLLABUS_WEEKS_1_TO_16_v8_FINAL.md |

---

## ⚡ QUICK REFERENCE

### Word Counts at a Glance
```
Sections 1-4 (each):  900-1500  (conceptual foundation)
Sections 5-7 (each):  400-900   (analysis + connections)
Sections 8-10 (each): 200-500   (math + practice)
Section 11:           800-1200  (retention)
Cognitive Lenses:     800-1200  (5 perspectives)
Supplementary:        ≤2500     (problems + resources)
─────────────────────────────
TOTAL:               7,500-15,000
```

### Structure at a Glance
```
1️⃣  WHY (motivation)        → 2️⃣  WHAT (concepts)      → 3️⃣  HOW (mechanics)
↓                            ↓                           ↓
4️⃣  VISUALIZATION (examples) → 5️⃣  ANALYSIS (complexity) → 6️⃣  SYSTEMS (real world)
↓                            ↓                           ↓
7️⃣  CROSSOVERS (dependencies) → 8️⃣  MATH (foundation) → 9️⃣  INTUITION (framework)
↓                            ↓
🔟 CHECK (self-test)      → 1️⃣1️⃣ RETENTION (hook)
                           ↓
                    🧩 COGNITIVE LENSES (5 perspectives)
                           ↓
                    ⚔️ SUPPLEMENTARY (problems + resources)
```

---

## 📞 SUPPORT

If you encounter issues:

1. **"What should Section 2 contain?"**
   → TEMPLATE_v9.1.md, Section 2 placeholder

2. **"How many words should Section 1 be?"**
   → SYSTEM_CONFIG_v9.1.md, Word Count Targets table

3. **"What are cognitive lenses?"**
   → MASTER_PROMPT_v9.1.md, 5 Cognitive Lenses section

4. **"How do I generate this with AI?"**
   → SYSTEM_PROMPT_v9.1_FOR_AI.md, start of file

5. **"How do I verify quality?"**
   → README_v9.1_QUALITY_STANDARDS.md, Submission Checklist

---

## 🎓 LEARNING RESOURCES

- **New to v9.1?** Read: README_v9.1_QUALITY_STANDARDS.md (15 min)
- **Generating a file?** Follow: MASTER_PROMPT_v9.1.md (30 min)
- **Using AI?** Provide: SYSTEM_PROMPT_v9.1_FOR_AI.md (AI reads)
- **Reviewing work?** Use: Checklists in SYSTEM_CONFIG_v9.1.md (10 min)
- **Full reference?** Open: TEMPLATE_v9.1.md + SYSTEM_CONFIG_v9.1.md (ongoing)

---

## 📊 METRICS TO TRACK

As you generate files, track:

```
Total Files Generated (v9.1):     ____ / 60
Total Words Generated:             ____ / 460,000
Average Words per File:            ____ (target: 11,000)
Files with Complete Operations:    ____ / 60
Files with Cognitive Lenses:       ____ / 60
Files with Supplementary:          ____ / 60
Quality Checklist Pass Rate:       ____ % (target: 100%)
```

---

## ✅ FINAL STATUS

**v9.1 Implementation Status:**

- ✅ All core documents created
- ✅ Quality standards defined
- ✅ Template finalized
- ✅ AI prompt optimized
- ✅ Verification checklists prepared
- ✅ Word counts specified
- ✅ Operations requirement explicit
- ✅ Cognitive lenses mandatory
- ✅ Supplementary outcomes required

**Status: READY FOR FULL IMPLEMENTATION** 🚀

---

**Version:** 9.1  
**Last Updated:** December 30, 2025  
**Status:** ✅ ACTIVE & OFFICIAL  
**Next Review:** When first 5 files generated
