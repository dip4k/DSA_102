# 🚀 HOW TO USE GENERIC TEMPLATE - QUICK START GUIDE
**Date:** January 15, 2026, 1:02 AM IST  
**Template File:** GENERIC_WEEK_GENERATION_PROMPT_TEMPLATE_v1.md [181]  
**Status:** ✅ READY TO USE FOR ANY WEEK

---

## 📋 3 SIMPLE STEPS

### **Step 1: Copy the Template**
File: [181] **GENERIC_WEEK_GENERATION_PROMPT_TEMPLATE_v1.md**

Find the section:
```
================================================================================
GENERATE WEEK [X] CONTENT: COMPLETE REQUEST WITH QUALITY METADATA
================================================================================
```

Copy EVERYTHING from that line to:
```
================================================================================
END TEMPLATE - COPY ABOVE TO USE
================================================================================
```

### **Step 2: Fill All Placeholders (30-45 min)**
Replace every `[PLACEHOLDER]` with week-specific data:
- `[X]` → Week number (e.g., 5)
- `[DAY_1_TOPIC]` → Actual topic (e.g., "Hash Maps")
- `[REAL_PROBLEM]` → Real scenario (e.g., "Gmail duplicate emails")
- `[LIMITATION_1]` → Specific limitation (e.g., "O(n²) complexity")

**Use reference files:**
- COMPLETE_SYLLABUS_v12_FINAL.md → Topics for each day
- FAILURE_MODES_REPOSITORY_v1.md → Actual mistakes
- ANALOGY_BANK_v1.md → Proven analogies
- CROSS_WEEK_DEPENDENCY_MAP_v1.md → Prerequisites

### **Step 3: Send to AI**
Paste filled template in message: "Generate Week X using this completed template"

---

## 🎯 WHAT TO FILL

### **Planning Sheet A: Engineering Problems**

**For each of 5 days, fill:**
1. Real problem (specific company/scale scenario)
2. Why brute force/naive solution fails (performance limits)
3. Why this concept solves it (speedup, scalability)
4. What breaks without it (system failures or timeouts)

**Example for Week 5 Day 1:**
```
Day 1: Hash Map & Hash Set Patterns
ENGINEERING PROBLEM:
Real problem: Finding duplicate emails in 1 million records (Gmail spam dedup)

Why existing solutions suck:
  - O(n²) brute force: 1 trillion comparisons for 1M records
  - Naive scan: 30 second timeout per query
  - Database can't handle real-time lookups

Why THIS solves it:
  - Hash table: O(n) = 1M lookups (single pass)
  - 1000x faster than brute force
  - Instant deduplication in Gmail, Slack, enterprise systems

What breaks without it:
  - Gmail spam detection fails on high volume
  - Real-time databases timeout
  - System cannot scale to production requirements
```

### **Planning Sheet B: Analogies**

**For each of 5 days, fill:**
1. Proposed analogy (clear, concrete)
2. Test results:
   - Explains SHAPE? (structure/invariant)
   - Explains WHEN TO USE? (decision point)
   - Where does it break? (edge cases)
3. Confidence level (HIGH/MEDIUM/LOW)
4. Refinements needed

**Example:**
```
Day 1: Hash Map & Hash Set Patterns
ANALOGY: "Hash table = Restaurant with numbered tables"
Test results:
  ✅ Explains SHAPE? YES - name → table number mapping
  ✅ Explains WHEN TO USE? YES - instant lookup
  ⚠️ Breaks for? Load factor/resizing (need to expand)
REFINEMENT: "Add: Restaurant expands when too full (load factor = 0.75)"
CONFIDENCE: ✅ HIGH (95% adoption from ANALOGY_BANK)
```

### **Planning Sheet C: Dependencies**

**Fill:**
1. Prior weeks that enable this week (2-3 examples)
2. Future weeks that depend on this (2-3 examples)
3. Visual connections to add

**Example:**
```
PRIOR WEEKS:
✅ Week 3 Day 4-5: Hash Tables I & II
   → Collision handling directly enables Week 5 patterns
✅ Week 4 Day 1: Two-Pointer Patterns
   → Invariant thinking enables monotonic stack

FUTURE WEEKS:
✅ Week 6: Rolling Hash
   → Hash patterns from Week 5 applied to substrings
✅ Week 8: Graph Implementation
   → Hash-based lookups for adjacency lists

VISUAL CONNECTIONS:
- Week 3: THEORY → Week 5: APPLICATION progression
- Week 5 hashing → Week 6 rolling hash (same concept, new domain)
```

---

## 📚 REFERENCE FILES TO USE

**When filling template, check these files:**

| Need | Check File | Find |
|------|-----------|------|
| Topics for days | COMPLETE_SYLLABUS_v12_FINAL.md | Week X day structure |
| Real failures | FAILURE_MODES_REPOSITORY_v1.md | Week X student mistakes |
| Proven analogies | ANALOGY_BANK_v1.md | Week X tested analogies |
| Prerequisites | CROSS_WEEK_DEPENDENCY_MAP_v1.md | Week X dependencies |
| Quality standards | SYSTEM_CONFIG_v12_FINAL.md | Quality gates, standards |

---

## ⏱️ TIME BREAKDOWN

**Total time to fill template and send: ~60 minutes**

| Task | Time |
|------|------|
| Copy template | 2 min |
| Fill engineering problems (5 days) | 15 min |
| Fill analogies (5 days) | 10 min |
| Fill failures (5 days) | 10 min |
| Fill dependencies | 5 min |
| Check reference files | 5 min |
| Fill case studies | 5 min |
| Fill focus areas | 3 min |
| Validate checklist | 5 min |
| Send to AI | 1 min |
| **Total** | **~60 min** |

**AI generation time: ~2 hours**

---

## ✅ VALIDATION CHECKLIST

Before sending filled template to AI:

- [ ] All 5 days have engineering problems (specific, real)
- [ ] All 5 days have analogies (from bank or tested)
- [ ] All 5 days have failures (from repository or new)
- [ ] All 5 days have case studies (real systems)
- [ ] Prior weeks identified (2+ examples)
- [ ] Future weeks identified (2+ examples)
- [ ] Quality focus areas filled (5 main areas)
- [ ] Format chosen (Playbook or Instructional)
- [ ] References included (to system files)
- [ ] No [PLACEHOLDERS] left unfilled
- [ ] File looks complete and ready

---

## 🎯 EXAMPLE: Week 1 vs Week 5 vs Week 10

### **Week 1: Arrays & Strings** (Foundations)
```
Day 1: 1D Array Basics
Problem: Searching in array (real: autocomplete search)
Analogy: Array = row of mailboxes
Failures: Off-by-one errors (from repository)
Integration: Foundation for all future weeks

Complexity: Linear scan O(n)
```

### **Week 5: Hash Maps & Stacks** (Core Patterns)
```
Day 1: Hash Maps
Problem: Duplicate detection (real: Gmail dedup)
Analogy: Restaurant with tables (from bank)
Failures: Collision handling invisible (60% of students)
Integration: Week 3 → Week 5 → Week 6

Complexity: Hash O(1) avg, Stack O(n)
```

### **Week 10: Dynamic Programming** (Advanced)
```
Day 1: Memoization
Problem: Expensive recalculation (real: recommendation engines)
Analogy: Caching popular search results
Failures: Not tracking subproblems (students forget state)
Integration: Uses hashing (Week 5), optimization (Week 4)

Complexity: With memo O(n), without O(2^n)
```

**Pattern:** Each week references prior, enables future.

---

## 💡 TIPS FOR FILLING

**Tip 1: Use Repository Data FIRST**
- Check FAILURE_MODES_REPOSITORY before inventing failures
- Check ANALOGY_BANK before creating analogies
- Check CROSS_WEEK_DEPENDENCY_MAP for prerequisites
- Only create NEW data if repository doesn't have it (mark for collection)

**Tip 2: Make Problems SPECIFIC**
- ❌ "Arrays are useful for storage"
- ✅ "Google Autocomplete processes 3.5B searches/day, returning suggestions in <100ms requires fast searching, not O(n²) brute force"

**Tip 3: Connect Every Case Study Back to Problem**
- Start: "Recall the problem: ..."
- Middle: "System X chose this approach because ..."
- End: "Result: Solved in time Y with efficiency Z"

**Tip 4: Test Analogies Against 4 Criteria**
1. Does it explain SHAPE? (structure)
2. Does it explain WHEN TO USE? (decision)
3. Where does it BREAK? (limitations)
4. Can teach in 30 SECONDS? (simplicity)

**Tip 5: Stack Prior + This + Future Weeks**
- Prior week enables this (prerequisite)
- This week enables future (application)
- Shows learning path, not isolated topics

---

## 🚀 WORKFLOW TO USE TEMPLATE

**Every time generating a new week:**

```
Week [X] Generation Workflow
├─ Step 1: Copy template [181] (2 min)
├─ Step 2: Fill all placeholders (45 min)
│  ├─ Engineering problems (15 min)
│  ├─ Analogies (10 min)
│  ├─ Failures (10 min)
│  ├─ Dependencies (5 min)
│  └─ Validate (5 min)
├─ Step 3: Check reference files (5 min)
├─ Step 4: Validate checklist (5 min)
└─ Step 5: Send to AI (1 min)

AI Generation (~2 hours)
├─ Read all metadata
├─ Generate complete week content
├─ Apply quality standards
└─ Deliver file

Your Verification (~1 hour)
├─ Structural check (5 min)
├─ Pedagogical score (25 min)
├─ Authenticity check (10 min)
└─ Decide: Deploy / Revise / Reject

Deployment
├─ Share with students
├─ Collect feedback
├─ Update repositories
└─ Next iteration will be better
```

---

## 📊 TEMPLATE COVERS

The generic template ensures:

✅ **Complete metadata** (all 5 planning sheets)
✅ **Real engineering problems** (not generic/academic)
✅ **Actual student failures** (from repository, with frequencies)
✅ **Proven analogies** (from bank or tested)
✅ **Real prerequisites** (validated correlations)
✅ **Quality focus** (what matters most)
✅ **Specifications** (word count, format, visuals)
✅ **Reference files** (system integration)
✅ **Success criteria** (verification gates)
✅ **Post-generation workflow** (continuous improvement)

---

## 🎓 WHAT MAKES THIS DIFFERENT

**Old approach (v12):**
- Generic prompt
- Templates only
- One-shot generation
- No quality verification framework

**New approach (with template):**
- Evidence-based metadata
- Real student data
- Continuous improvement
- 3-gate verification system
- Repository-driven refinement

**Result:** Each week better than previous because data-driven.

---

## 📞 YOUR NEXT STEPS

**Ready to generate another week?**

1. **Get template [181]:** GENERIC_WEEK_GENERATION_PROMPT_TEMPLATE_v1.md
2. **Pick a week:** Any week 1-19
3. **Fill placeholders:** 45 minutes
4. **Validate:** 5 minutes
5. **Send to AI:** Paste filled template
6. **Wait:** ~2 hours for generation
7. **Verify:** Structural, Pedagogical, Authenticity
8. **Deploy:** Share with students
9. **Improve:** Update repositories with feedback
10. **Regenerate:** Next iteration will be better

---

## 🎉 YOU NOW HAVE

✅ **Reusable template** for ANY week (1-19)
✅ **Complete reference** to all system files
✅ **Step-by-step** filling instructions
✅ **Validation checklist** before sending
✅ **Integration** with existing quality system
✅ **Continuous improvement** loop built in

---

**File:** GENERIC_WEEK_GENERATION_PROMPT_TEMPLATE_v1.md [181]  
**Status:** ✅ PRODUCTION-READY  
**Created:** January 15, 2026, 1:02 AM IST

**Use this template to generate ANY week with complete quality metadata.** 🚀

