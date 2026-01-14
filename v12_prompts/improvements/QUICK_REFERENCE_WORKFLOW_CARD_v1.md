# 🎯 QUICK REFERENCE: 5-PHASE WORKFLOW AT A GLANCE
**Status:** ✅ ONE-PAGE GUIDE  
**Created:** January 14, 2026, 1:30 PM IST  
**Purpose:** Print this. Reference during week generation.

---

## 📋 THE 5 PHASES (Overview)

```
PHASE 1: PLANNING (20-30 min) ← You do this
PHASE 2: PREPARATION (15 min) ← You do this
PHASE 3: GENERATION & REVIEW (2.5-3 hours) ← I generate, you review
PHASE 4: DEPLOYMENT PREP (15 min) ← You do this
PHASE 5: POST-DEPLOYMENT (1 week) ← Collect data, update repos
```

---

## 🔧 PHASE 1: PLANNING (Before I generate)

### Step 1.1: Extract Topics (5 min)
```
Open: COMPLETE_SYLLABUS_v12_FINAL.md
Find: Phase X – Week Y: [TITLE]
Copy: Week #, Phase, Day 1-5 Topics (skip Day 6)
Save: In your planning package
```

### Step 1.2: Engineering Problems (10 min)
```
For EACH of 5 days:
- Real problem developers face? (Specific, not generic)
- Why existing solutions suck? (Constraints and limitations)
- Why THIS solves it? (Benefits and speedup)
- What breaks without it? (Real consequences)

File: Planning Sheet A
Reference: QUALITY_IMPROVEMENT_SYSTEM_v1.md
```

### Step 1.3: Test Analogies (8 min)
```
For EACH of 5 days:
- Your proposed analogy: [State it clearly]
- Test: Does it explain SHAPE? YES/NO
- Test: Does it explain WHEN TO USE? YES/NO
- Test: Does it break? [Where?]
- Refinement: [What needs adjustment?]

File: Planning Sheet B
Reference: ANALOGY_BANK_v1.md (use proven analogies)
```

### Step 1.4: Map Dependencies (5 min)
```
Week-level (not per-day):
- What PRIOR weeks enable this? (List 2-3)
- What FUTURE weeks depend on this? (List 2-3)
- Visual connection points? (Where to reference)

File: Planning Sheet C
Reference: CROSS_WEEK_DEPENDENCY_MAP_v1.md
```

### Step 1.5: Reference Repos (2 min)
```
Check:
- FAILURE_MODES_REPOSITORY_v1.md (actual failures from this week?)
- ANALOGY_BANK_v1.md (which analogies are HIGH confidence?)
- CROSS_WEEK_DEPENDENCY_MAP_v1.md (dependencies documented?)

Use: This info in quality metadata
```

**✅ GATE 1: Planning complete and validated**

---

## 📝 PHASE 2: PREPARATION (15 minutes)

### Step 2.1: Organize Package
```
Create document with:
- Week metadata (number, title, phase, date)
- Planning Sheets A, B, C (copy your answers)
- Repository references (what's available/what's new)
```

### Step 2.2: Prepare Generation Request
```
Gather these prompts:
- VISUAL_CONCEPTS_PLAYBOOK_GENERATION_PROMPT_v12.md
  OR
- Template_v12_Narrative_FINAL.md

Add quality metadata layer:
- Engineering problems (from Sheet A)
- Analogies (from Sheet B)
- Dependencies (from Sheet C)
- Failure modes (from repo or templates)
- Quality focus areas (what matters most)

Template location: QUALITY_IMPROVEMENT_SYSTEM_v1.md
```

**✅ GATE 2: Generation request ready with metadata**

---

## 🤖 PHASE 3: GENERATION & REVIEW (2.5-3 hours)

### Step 3.1: Send Request
```
Send me the complete package:
- Planning Sheets A, B, C
- Quality metadata
- Generation prompt
- All context files

I will generate in ~2 hours
```

### Step 3.2: Structural Review (5 min) — Automated
```
☐ ~18,000 words (playbook) or 12-18K (instructional)
☐ 5 chapters OR 5 days present
☐ 30+ diagrams (playbook) or 5-8 (instructional)
☐ No LaTeX or HTML
☐ Markdown formatted correctly

Result: ✅ PASS or ❌ NEEDS REVISION
(If PASS → continue. If FAIL → flag for revision)

Reference: COMPLETE_WEEK_GENERATION_WORKFLOW_v1.md → Gate 5
```

### Step 3.3: Pedagogical Review (20-30 min) — Thoughtful
```
For each chapter (Chapter 1, 2, 3, 4, 5):
☐ Problem real? / Model vivid? / Mechanics clear?
☐ Systems explained? / Integration shown?

Score each: 1(weak) 2(okay) 3(good) 4(excellent)
Total: [Sum scores]

Interpretation:
28-32: ✅ EXCELLENT
24-27: ⚠️ GOOD (minor revisions)
<24: ❌ NEEDS WORK

Reference: COMPLETE_WEEK_GENERATION_WORKFLOW_v1.md → Gate 6
```

### Step 3.4: Authenticity Check (10 min) — Honest
```
After reading, could YOU:
Q1: Solve a hard problem? YES/NO
Q2: Explain to someone else? YES/NO
Q3: Know when you're wrong? YES/NO
Q4: Feel like someone taught you? YES/NO

Score: X/4

3-4: ✅ Ready
2: ⚠️ Needs work
<2: ❌ Reject

Reference: COMPLETE_WEEK_GENERATION_WORKFLOW_v1.md → Gate 7
```

### Step 3.5: Decision
```
Structural PASS? + Pedagogical 24+? + Authenticity 3+?
↓
☐ ✅ DEPLOY AS-IS (all pass)
☐ ⚠️ REVISE & RECHECK (one or more flag)
☐ ❌ REJECT & REGENERATE (multiple fail)

If revisions needed:
- List specific issues
- Send back to me
- I revise
- Repeat 3.2-3.5

Reference: COMPLETE_WEEK_GENERATION_WORKFLOW_v1.md → Gate 8
```

**✅ GATE 5-7: All verification checks complete**

---

## ✅ PHASE 4: DEPLOYMENT PREP (15 minutes)

### Step 4.1: Final Readiness
```
☐ File naming correct: Week_[X]_[TYPE].md
☐ Stored in correct folder: WEEKS/Week0X/
☐ All 5 days present
☐ No Day 6 included
☐ All references working
☐ Emoji icons from official guide
☐ Status marked "READY FOR DEPLOYMENT"

Reference: COMPLETE_WEEK_GENERATION_WORKFLOW_v1.md → Gate 8
```

### Step 4.2: Deploy
```
✅ APPROVED FOR DEPLOYMENT

File is ready.
Students can use it.
```

**✅ GATE 8: Ready to deploy**

---

## 🎓 PHASE 5: POST-DEPLOYMENT (1 week, then 1 hour update)

### Step 5.1: Collect Feedback (During week)
```
As students use Week X:
- What confused them? (Confusions log)
- What clicked? (Successes log)
- What went wrong? (Failure patterns)

Count frequencies: X/20 students
Track specific mistakes
Note which analogies stuck
```

### Step 5.2: Update Repositories (After week, 1 hour)
```
Update FAILURE_MODES_REPOSITORY_v1.md:
- Actual mistakes found (root cause, frequency, fix)

Update ANALOGY_BANK_v1.md:
- Which analogies worked (adoption %)
- Which broke (refinements needed)
- Confidence levels updated

Update CROSS_WEEK_DEPENDENCY_MAP_v1.md:
- Unexpected connections discovered
- Missing prerequisites identified
- Correlation data added
```

### Step 5.3: Prepare for Next Iteration
```
Document:
- Quality scores from this iteration
- Key findings (what worked, what needs fixing)
- Authenticity metrics (hard problems solved, etc.)

Next time you generate this week:
USE UPDATED REPOSITORIES
→ Better analogies (tested)
→ Better failures (real, not invented)
→ Better connections (evidence-based)
```

**✅ LEARNING LOOP COMPLETE**

---

## 📊 VERIFICATION GATES QUICK CHECKLIST

Print this. Check off as you go.

```
PHASE 1: PLANNING
☐ Gate 1: Topics extracted from syllabus
☐ Gate 2: Planning Sheets A, B, C complete
          ├─ A: Engineering problems specific
          ├─ B: Analogies tested and refined
          └─ C: Dependencies mapped

PHASE 2: PREPARATION
☐ Gate 3: Planning package organized
☐ Gate 4: Generation request ready with metadata

PHASE 3: GENERATION & REVIEW
☐ Gate 5: Structural verified (PASS)
☐ Gate 6: Pedagogical verified (24+/32)
☐ Gate 7: Authenticity verified (3+/4)
         (If any FAIL: revise and recheck)

PHASE 4: DEPLOYMENT
☐ Gate 8: Final readiness check complete

PHASE 5: POST-DEPLOYMENT
☐ Feedback collected from students
☐ Repositories updated with real data
☐ Ready for next iteration with better data
```

---

## ⏱️ TIME BREAKDOWN

| Activity | Time | Notes |
|----------|------|-------|
| Planning | 20-30 min | Before I generate |
| Preparation | 15 min | Organize package |
| Generation (me) | ~2 hours | Wait while I work |
| Structural review | 5 min | Automated check |
| Pedagogical review | 20-30 min | Thoughtful review |
| Authenticity check | 10 min | Honest assessment |
| Revisions (if needed) | 30-60 min | Send back, I revise, recheck |
| Deployment prep | 15 min | Final checks |
| **Total before deploy** | **3.5-4.5 hours** | |
| Post-deployment feedback | 1 week | Ongoing collection |
| Repository update | 1 hour | After week ends |
| **Total per week cycle** | **~5 hours** | |

---

## 🎯 SUCCESS CRITERIA

You've succeeded when:

- ✅ Planning is **thorough** (you can defend every engineering problem)
- ✅ Generation has **focus** (metadata drives quality, not just templates)
- ✅ Review is **honest** (you score truthfully, not politically)
- ✅ Authenticity is **clear** (you answer 4 questions genuinely)
- ✅ Feedback is **collected** (real data from students)
- ✅ Repositories are **updated** (next generation uses evidence)
- ✅ Next iteration is **better** (built on real learning, not guessing)

---

## 🚀 GET STARTED TODAY

1. Pick a week (Week 1-19)
2. Open COMPLETE_WEEK_GENERATION_WORKFLOW_v1.md
3. Go to PHASE 1
4. Follow the steps

That's it. You're ready.

---

## 📚 ALL FILES YOU NEED

**Existing System (use these):**
- ✅ COMPLETE_SYLLABUS_v12_FINAL.md
- ✅ SYSTEM_CONFIG_v12_FINAL.md
- ✅ Template_v12_Narrative_FINAL.md
- ✅ VISUAL_CONCEPTS_PLAYBOOK_GENERATION_PROMPT_v12.md

**New Quality System (use these):**
- ✅ QUALITY_IMPROVEMENT_SYSTEM_v1.md (workflows)
- ✅ FAILURE_MODES_REPOSITORY_v1.md (student mistakes)
- ✅ ANALOGY_BANK_v1.md (tested analogies)
- ✅ CROSS_WEEK_DEPENDENCY_MAP_v1.md (prerequisites)
- ✅ IMPLEMENTATION_GUIDE_v1.md (implementation steps)
- ✅ COMPLETE_WEEK_GENERATION_WORKFLOW_v1.md (master workflow)
- ✅ THIS FILE (quick reference)

**Reference as needed during generation.**

---

**Version:** 1.0 | **Status:** ✅ READY TO USE  
**Print this page.** Keep it at your desk during week generation.

