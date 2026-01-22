# 📝 GENERIC WEEK GENERATION PROMPT TEMPLATE v1.0
**Status:** ✅ PRODUCTION-READY TEMPLATE  
**Created:** January 15, 2026, 12:59 AM IST  
**Purpose:** Reusable template for generating ANY week (Weeks 1-19) with complete quality metadata  
**Usage:** Copy, fill placeholders, send to AI for generation

---

## 🎯 HOW TO USE THIS TEMPLATE

**Step 1:** Copy entire template below (between the === lines)  
**Step 2:** Replace all [PLACEHOLDERS] with your week-specific data  
**Step 3:** Fill in Planning Sheets A, B, C  
**Step 4:** Reference repository data for your week  
**Step 5:** Send to AI for generation  

**Time to fill:** 30-45 minutes per week

---

```
================================================================================
GENERATE WEEK [X] CONTENT: COMPLETE REQUEST WITH QUALITY METADATA
================================================================================

=== PART 1: PLANNING SHEETS (COMPLETED) ===

PLANNING SHEET A: ENGINEERING PROBLEMS (Why does this matter?)

Day 1: [DAY_1_TOPIC]
ENGINEERING PROBLEM:
Real problem: [SPECIFIC REAL-WORLD PROBLEM - not generic]
Example: [Real scenario: company, scale, user problem]

Why existing solutions suck:
  - [Limitation 1]: [Impact on scale/performance]
  - [Limitation 2]: [Impact on users/systems]
  - [Limitation 3]: [Real consequence]

Why THIS solves it:
  - [Solution benefit 1]: [Speedup or improvement]
  - [Solution benefit 2]: [Scalability gain]
  - [Solution benefit 3]: [Real-world application]

What breaks without it:
  - [Consequence 1]: [System failure or timeout]
  - [Consequence 2]: [User impact]
  - [Consequence 3]: [Business impact]

Day 2: [DAY_2_TOPIC]
ENGINEERING PROBLEM:
Real problem: [SPECIFIC REAL-WORLD PROBLEM]
[Repeat format above]

Day 3: [DAY_3_TOPIC]
ENGINEERING PROBLEM:
[Repeat format]

Day 4: [DAY_4_TOPIC]
ENGINEERING PROBLEM:
[Repeat format]

Day 5: [DAY_5_TOPIC]
ENGINEERING PROBLEM:
[Repeat format]

---

PLANNING SHEET B: ANALOGIES (Do they work?)

Day 1: [DAY_1_TOPIC]
ANALOGY: "[PROPOSED ANALOGY - state clearly]"
Test results:
  ✅ Explains SHAPE? [YES/NO]: [Why or why not?]
  ✅ Explains WHEN TO USE? [YES/NO]: [Why or why not?]
  ⚠️ Breaks for? [WHERE IT BREAKS]: [What edge case?]
  ✅ Can teach in 30 seconds? [YES/NO]: [Is it intuitive?]
REFINEMENT: [What needs adjustment?]
CONFIDENCE: [HIGH/MEDIUM/LOW] (from ANALOGY_BANK if available)

Day 2: [DAY_2_TOPIC]
ANALOGY: "[PROPOSED ANALOGY]"
[Repeat testing format]

Day 3: [DAY_3_TOPIC]
[Repeat]

Day 4: [DAY_4_TOPIC]
[Repeat]

Day 5: [DAY_5_TOPIC]
[Repeat]

---

PLANNING SHEET C: CROSS-WEEK DEPENDENCIES

PRIOR WEEKS (What enables this week?):
✅ Week [X] Day [Y]: [Topic] → [HOW IT ENABLES THIS WEEK]
✅ Week [X] Day [Y]: [Topic] → [HOW IT ENABLES THIS WEEK]
✅ Week [X] Day [Y]: [Topic] → [HOW IT ENABLES THIS WEEK]

[List at least 2-3 prior weeks with specific dependencies]

FUTURE WEEKS (What weeks depend on this?):
✅ Week [X]: [Topic] → [HOW THIS WEEK ENABLES IT]
✅ Week [X]: [Topic] → [HOW THIS WEEK ENABLES IT]
✅ Week [X]: [Topic] → [HOW THIS WEEK ENABLES IT]

[List at least 2-3 future weeks with specific applications]

VISUAL CONNECTIONS TO ADD:
- [Connection 1]: [Prior week] → [This week] progression
- [Connection 2]: [This week] → [Future week] application
- [Connection 3]: [Cross-week concept link]

---

=== PART 2: REPOSITORY REFERENCES ===

FAILURE MODES (From FAILURE_MODES_REPOSITORY - ACTUAL STUDENT MISTAKES):

DAY 1: [DAY_1_TOPIC]
Failure 1: [ACTUAL MISTAKE TITLE]
  - Symptom: [What wrong code does / output]
  - Root Cause: [Why students think this way]
  - Frequency: [X/20 students (Y%)]
  - Teaching Fix: [How to prevent / teach better]

Failure 2: [ANOTHER MISTAKE]
  [Repeat above format]

Failure 3: [ANOTHER MISTAKE]
  [Repeat above format]

DAY 2: [DAY_2_TOPIC]
Failure 1: [MISTAKE]
  [Format above]

Failure 2: [MISTAKE]
  [Format above]

[Continue for Days 3, 4, 5 - at least 2 failures per day, total 8-10 failures]

---

ANALOGIES (From ANALOGY_BANK - TESTED & PROVEN):

Day 1: "[ANALOGY 1]"
  - Confidence: [✅ HIGH / ⚠️ MEDIUM / ❌ LOW]
  - Tested: [How many students understood? X%]
  - Where to use: [Chapter/section where this analogy goes]

Day 2: "[ANALOGY 2]"
  [Format above]

[Continue for all 5 days - use high-confidence analogies when available]

---

DEPENDENCIES (From CROSS_WEEK_DEPENDENCY_MAP - VALIDATED):

Week [X] ← Week [Y]: [X]% correlation (students strong in Week Y → [X]% success)
Week [X] ← Week [Z]: [X]% correlation (students weak in Week Z → [X]% struggle)
Week [X] → Week [A]: [PATTERN NAME] (direct application)
Week [X] → Week [B]: [PATTERN NAME] (same concept, new domain)

[List specific correlations and connections]

---

=== PART 3: QUALITY FOCUS AREAS ===

WHAT MATTERS MOST FOR WEEK [X]:

1. ENGINEERING PROBLEM CLARITY (Chapter 1 / Day Problems)
   - MUST connect to REAL developer problems (not generic/academic)
   - MUST show why brute force/naive solution fails
   - MUST show why THIS concept solves it at scale
   - Examples:
     * NOT: "Hashing is useful for lookups"
     * YES: "Gmail receives 1.8B emails/day, finding duplicates in real-time requires O(1) lookups"

2. ANALOGY QUALITY (Chapter 2 / Pattern Map)
   - MUST be vivid and concrete (something learner knows)
   - MUST explain SHAPE (structure/invariant)
   - MUST explain WHEN TO USE (decision point)
   - MUST acknowledge WHERE IT BREAKS (edge cases)
   - Examples: Restaurant = Hash Table, Mountains = Monotonic Stack

3. FAILURE MODE AUTHENTICITY (Chapter 3 / Common Pitfalls)
   - MUST use ACTUAL mistakes from repository (not invented)
   - Include frequency data (X/20 students)
   - Show ROOT CAUSE not just symptom
   - Include teaching fix that prevents it
   - Examples from this week:
     * [List 2-3 representative failures for focus]

4. REAL SYSTEMS CASE STUDIES (Chapter 4 / Performance)
   - MUST explain WHY system chose this approach
   - MUST show real performance impact (numbers, scale)
   - MUST be stories not just lists
   - MUST connect back to Chapter 1 problem
   - Examples for this week:
     * Day 1: [Real system 1], [Real system 2], [Real system 3]
     * Day 2: [Real system 1], [Real system 2]
     * [Continue for each day]

5. CROSS-WEEK INTEGRATION (Chapter 5 / Integration)
   - MUST show how prior weeks enable this week
   - MUST show how this week enables future weeks
   - MUST foreshadow next week's applications
   - Add visual: "Week X: THEORY → Week [X+1]: APPLICATION"
   - Add visual: "Week [X-1]: FOUNDATION → Week X: ADVANCED APPLICATION"

---

=== PART 4: GENERATION SPECIFICATIONS ===

CHOOSE ONE FORMAT:

OPTION A: PLAYBOOK FORMAT
- Word count: ~18,000 words
- Structure: 5 days (Day 1-5 core, skip optional)
- Visuals: 30+ ASCII diagrams total, 6+ per day
- Quiz: 15 questions (3 per day)
- Failure modes: 8-10 actual mistakes (organized by day)
- Web resources: 6+ embedded links
- Tables: Complexity reference, week summary
- Code: C# examples only
- Format: Markdown, no LaTeX, inline visuals

OPTION B: INSTRUCTIONAL FILE FORMAT
- Word count: 12,000-18,000
- Structure: 5 chapters (Context, Mental Model, Mechanics, Performance, Integration)
- Visuals: 5-8 inline throughout
- Case studies: 3-5 real system stories in Performance chapter
- Integration: Chapter 5 shows prior/future connections
- Learning objectives: At top
- Cognitive lenses: All 5 covered
- Code: C# examples only
- Format: Markdown, no LaTeX

SPECIFY FORMAT: [PLAYBOOK / INSTRUCTIONAL]

---

=== PART 5: REFERENCE FILES ===

USE THESE FILES AS REFERENCE (from your existing system):

For structure:
  - COMPLETE_SYLLABUS_v12_FINAL_Updated.md (Week [X] topics, phase, day structure)
  - SYSTEM_CONFIG_v12_FINAL.md (quality gates, standards)

For templates:
  - Template_v12_Narrative_FINAL.md (instructional blueprint for Option B)
  - VISUAL_CONCEPTS_PLAYBOOK_GENERATION_PROMPT_v12.md (playbook blueprint for Option A)

For code standards:
  - SYSTEM_PROMPT_v12_EXTENDED_SUPPORT_CSHARP.md

For quality system:
  - QUALITY_IMPROVEMENT_SYSTEM_v1.md (planning protocols)
  - FAILURE_MODES_REPOSITORY_v1.md (actual mistakes)
  - ANALOGY_BANK_v1.md (proven analogies)
  - CROSS_WEEK_DEPENDENCY_MAP_v1.md (prerequisites)

For workflow:
  - COMPLETE_WEEK_GENERATION_WORKFLOW_v1.md (5 phases)
  - QUICK_REFERENCE_WORKFLOW_CARD_v1.md (quick ref)

---

=== PART 6: CRITICAL REQUIREMENTS ===

GENERATION MUST INCLUDE:

✅ All 5 days fully covered (no partial coverage)
✅ Failure modes from FAILURE_MODES_REPOSITORY (actual mistakes, with frequencies)
✅ Analogies from ANALOGY_BANK (proven, high-confidence)
✅ Engineering problems that feel REAL (not generic/academic)
✅ Case studies that explain WHY system chose this (not just what it does)
✅ Integration section showing prior week → this week → future week connections
✅ Vivid ASCII diagrams inline (not at end of document)
✅ Complexity tables comparing approaches
✅ No invented content (use repository data)
✅ Honest depth (feels like someone taught you, not just presented)

DO NOT INCLUDE:

❌ Day 6 Optional/Advanced section
❌ LaTeX or HTML formatting
❌ Generic failure modes (use repository only)
❌ Invented analogies (use bank only)
❌ List-based case studies (need stories with WHY)
❌ Weak integration (must show real connections)
❌ Content that contradicts existing system files

---

=== PART 7: SUCCESS CRITERIA ===

AFTER GENERATION, VERIFY:

Structural Gate (5 min):
  ☐ Word count in range
  ☐ 5 days/chapters structured
  ☐ Visuals inline throughout
  ☐ Complexity tables present
  ☐ No LaTeX or HTML errors
  ☐ Markdown formatting consistent
  RESULT: ✅ PASS or ❌ NEEDS REVISION

Pedagogical Gate (20-30 min):
  For each day/chapter:
  ☐ Engineering problem real & compelling? (1-4 score)
  ☐ Analogies vivid & tested? (1-4)
  ☐ Mechanics clear & traceable? (1-4)
  ☐ Systems explained (not listed)? (1-4)
  ☐ Integration shown? (1-4)
  
  Total: ___/32
  28-32: ✅ EXCELLENT
  24-27: ⚠️ GOOD
  <24: ❌ NEEDS WORK

Authenticity Gate (10 min):
  ☐ Could solve hard problem after reading? YES/NO
  ☐ Could explain to someone else? YES/NO
  ☐ Know when to use vs avoid? YES/NO
  ☐ Feel like someone taught you? YES/NO
  
  Score: ___/4
  3-4: ✅ EXCELLENT
  2: ⚠️ WEAK
  <2: ❌ POOR

---

=== PART 8: AFTER GENERATION ===

POST-GENERATION WORKFLOW:

1. You receive generated file
2. Do verification checks (Structural, Pedagogical, Authenticity)
3. Decision: DEPLOY / REVISE / REJECT
4. If revisions: Send specific feedback, AI revises, recheck
5. If approved: Deploy to students
6. Collect feedback during deployment
7. Update repositories with real data:
   - FAILURE_MODES_REPOSITORY: Add actual mistakes found
   - ANALOGY_BANK: Update what worked/what didn't
   - CROSS_WEEK_DEPENDENCY_MAP: Add real correlation data
8. Next generation uses updated data (better each time)

---

================================================================================
END TEMPLATE - COPY ABOVE TO USE
================================================================================
```

---

## 🔧 FILLING OUT THE TEMPLATE

### **Quick Reference: What to Put in Each Section**

| Section | Example | Source |
|---------|---------|--------|
| Engineering Problem | "Finding duplicates in 1M emails" | Real systems |
| Why brute force sucks | "O(n²) = 1T comparisons, 30s timeout" | Scalability limits |
| Why this solves it | "Hash = O(1), 1000x faster" | Algorithm benefits |
| Real problem consequence | "Gmail spam detection fails" | Business impact |
| Analogy | "Restaurant with tables" | ANALOGY_BANK or create new |
| Failure mode | "Student assumes no collisions" | FAILURE_MODES_REPOSITORY or real data |
| Failure frequency | "60% of first attempts" | From student data |
| Prior week dependency | "Week 3 Hash theory enables Week 5 patterns" | CROSS_WEEK_DEPENDENCY_MAP |
| Case study | "Gmail processes 1.8B emails/day..." | Real system, public info |
| Case study WHY | "Switched from DB queries (500ms) to hash (1ms)" | Performance reason |

---

## 📋 STEP-BY-STEP FILLING PROCESS

### **For Each Week:**

**1. Get Topics (5 min)**
```
Open: COMPLETE_SYLLABUS_v12_FINAL_Updated.md
Find: Phase X – Week Y: [WEEK_TITLE]
Copy: Week number, phase, Day 1-5 topics
```

**2. Engineering Problems (15 min)**
```
For each day:
- What real problem do developers face?
- Why does naive solution fail?
- How does THIS concept solve it?
- What breaks without it?
(Must be SPECIFIC, not generic)
```

**3. Analogies (10 min)**
```
For each day:
- Check ANALOGY_BANK for proven analogies
- If found: Use high-confidence one
- If not found: Create & test against 4 criteria
- Note: Does it explain shape? When to use? Where breaks?
```

**4. Failures (10 min)**
```
For each day:
- Check FAILURE_MODES_REPOSITORY
- If Week has data: Copy actual mistakes + frequencies
- If not: Use templates but mark for collection after deployment
- Include: Root cause + teaching fix (not just symptom)
```

**5. Dependencies (5 min)**
```
Check CROSS_WEEK_DEPENDENCY_MAP:
- What prior weeks enable this week?
- What future weeks depend on this?
- What correlations exist?
- Visual connections to show?
```

**6. Case Studies (5 min)**
```
For each day:
- What real system uses this?
- Why did they choose this approach?
- What performance improvement?
- How does it solve the Chapter 1 problem?
```

**Total time to fill template: 30-45 minutes per week**

---

## 🎯 TEMPLATE VARIATIONS

### **Variation 1: For Weeks WITH Existing Repository Data**
```
Use:
- ACTUAL student failures (from FAILURE_MODES_REPOSITORY)
- PROVEN analogies (from ANALOGY_BANK, high confidence)
- REAL prerequisites (from CROSS_WEEK_DEPENDENCY_MAP)

Result: High-quality generation (evidence-based)
```

### **Variation 2: For Weeks WITHOUT Repository Data (First Time)**
```
Use:
- Planning sheets (filled manually)
- Template failures (marked for collection after deployment)
- Test new analogies (will get real data next cohort)
- Map dependencies yourself (will validate next cohort)

Result: Good-quality generation (template-based)
Next iteration: Better (now has real data)
```

### **Variation 3: For Updating Existing Week**
```
Use:
- Previous generation as reference
- Updated repositories (new student data)
- Refined analogies (from feedback)
- Real failures (collected from last cohort)

Result: Improved generation (incremental refinement)
```

---

## 💡 PRO TIPS FOR FILLING THE TEMPLATE

**Tip 1: Make Problems REAL**
- ❌ "Array problems are useful"
- ✅ "Gmail processes 1.8B emails/day, finding duplicates without O(n²) complexity is critical"

**Tip 2: Use Repository Data First**
- Check FAILURE_MODES_REPOSITORY for actual mistakes
- Check ANALOGY_BANK for proven analogies
- Check CROSS_WEEK_DEPENDENCY_MAP for real correlations
- Don't invent when data exists

**Tip 3: Show Numbers in Case Studies**
- ❌ "Redis uses hashing"
- ✅ "Redis uses hash tables for ~1ms lookups vs 100ms database queries"

**Tip 4: Connect Every Case Study Back to Chapter 1**
- Start with: "Recall the Chapter 1 problem: ..."
- Explain: "System X chose this approach because ..."
- Show: "Result: Achieved Y benefit"

**Tip 5: Be Specific About Integration**
- ❌ "This relates to Week 6"
- ✅ "Week 5 hash patterns directly enable Week 6 rolling hash (same concept applied to substrings)"

---

## ✅ VALIDATION CHECKLIST BEFORE SENDING

Before sending template to AI:

- [ ] All 5 days have engineering problems (specific, real)
- [ ] All 5 days have analogies (tested or from bank)
- [ ] All 5 days have failures (from repository or marked as new)
- [ ] All 5 days have case studies (real systems with WHY)
- [ ] Dependencies identified (prior weeks, future weeks)
- [ ] Quality focus areas filled (what matters for this week)
- [ ] References to repository files included
- [ ] Format chosen (Playbook or Instructional)
- [ ] Critical requirements listed (what to include/exclude)
- [ ] Success criteria defined (verification gates)
- [ ] All [PLACEHOLDERS] replaced with actual data

---

## 📝 EXAMPLE: FILLING FOR WEEK 7 (TREES)

```
[EXAMPLE - DO NOT USE, JUST FOR REFERENCE]

PLANNING SHEET A: ENGINEERING PROBLEMS

Day 1: Binary Search Trees
ENGINEERING PROBLEM:
Real problem: Finding ancestor in org chart with 100K+ people

Why existing solutions suck:
  - Array scan: O(n) per lookup = 100K operations
  - Hash table: Doesn't preserve hierarchy relationships
  - Database query: 100ms latency per lookup

Why BST solves it:
  - Balanced tree: O(log n) = ~17 operations for 100K nodes
  - Preserves hierarchy: Can traverse up/down
  - In-memory: ~1ms per lookup

What breaks without it:
  - HR system shows wrong subordinates
  - Manager queries timeout
  - Cannot efficiently navigate org structure

...and so on for Days 2-5
```

---

## 🚀 USAGE WORKFLOW

**Every time you want to generate a new week:**

1. **Copy this template** ← You're reading it now
2. **Fill placeholders** (30-45 min per week)
3. **Check reference files** (repository, syllabus)
4. **Validate checklist** (make sure all filled)
5. **Send to AI** ("Use GENERIC_WEEK_GENERATION_PROMPT_TEMPLATE_v1.md, fill with Week X data, generate")
6. **AI generates** (~2 hours)
7. **You verify** (Structural, Pedagogical, Authenticity)
8. **Deploy or Revise**
9. **Collect feedback** (during deployment)
10. **Update repositories** (for next generation)

---

## 📊 COMPARISON: Old vs New Approach

| Aspect | Before (v12) | After (New System) |
|--------|--------------|-------------------|
| Generation request | Generic prompt | Metadata-driven template |
| Planning | Minimal | Detailed (Sheets A, B, C) |
| Failure modes | Invented | From repository |
| Analogies | New each time | Proven from bank |
| Dependencies | Assumed | Real correlations |
| Iteration | One-shot | Continuous improvement |
| Quality verification | Checkboxes | 3 authentic gates |
| Time to generate | ~2 hours | ~2 hours (but better) |
| Time to prepare | ~15 min | ~45 min (but worth it) |
| Result quality | Good | Excellent (evidence-based) |

---

## 🎯 YOU NOW HAVE

**This reusable template** for ANY week:
- ✅ All placeholder sections
- ✅ Step-by-step instructions
- ✅ Real examples
- ✅ Validation checklist
- ✅ Pro tips
- ✅ Integration with existing system

**Use it for:**
- ✅ Weeks 1-19 (any week)
- ✅ First generation (template-based)
- ✅ Subsequent generations (evidence-based with repository)
- ✅ Updates/refinements (continuous improvement)

---

## 📞 NEXT STEPS

**To use this template:**

1. **Copy the template** (from the ```` section between === lines)
2. **Pick a week** (1-19, any week)
3. **Fill all [PLACEHOLDERS]** (45 min)
4. **Check reference files** (5 min)
5. **Validate** (5 min)
6. **Send to AI** ("Generate Week X using this completed template")
7. **AI generates** (~2 hours)
8. **You verify** and deploy

---

**Template Version:** 1.0  
**Status:** ✅ PRODUCTION-READY  
**Created:** January 15, 2026, 12:59 AM IST

Use this for generating ANY week with complete quality metadata. 🚀

