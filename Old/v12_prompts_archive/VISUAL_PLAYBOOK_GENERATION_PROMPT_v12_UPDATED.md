# 📊 VISUAL CONCEPTS PLAYBOOK GENERATION PROMPT v12 — UPDATED WITH SYLLABUS REFERENCE

**Version:** 12.2 – WITH COMPLETE SYLLABUS v12 REFERENCE  
**Status:** ✅ PRODUCTION-READY TEMPLATE  
**Created:** Saturday, January 10, 2026, 5:48 PM IST  
**Updated:** To reference COMPLETE_SYLLABUS_v13_FINAL.md for all week/day topics  
**Purpose:** Generate hybrid visual support files for ANY week of DSA curriculum  
**Philosophy:** Offline-first with optional web enhancement | Professional quality | Immediate deployment

---

## 🎯 MASTER PROMPT OVERVIEW

This template generates **Week XX Visual Concepts Playbook (HYBRID)** files that are:

- ✅ **Completely self-contained** (30+ ASCII diagrams, works offline)
- ✅ **Web-enhanced** (6 professional tool links embedded)
- ✅ **Educationally validated** (15 quiz questions + 8-10 failure modes)
- ✅ **Production-grade quality** (consistent across all weeks)
- ✅ **Immediate deployment** (100% ready to use)
- ✅ **SYLLABUS-ALIGNED** (references COMPLETE_SYLLABUS_v13_FINAL.md for all topics)

**Output Format:** Markdown file (~18,000 words, 5 days × 5 topics)  
**Time to Generate:** 2-3 hours per week using this template  
**Reusable For:** All 19 weeks with topic substitution only

---

## 📋 HOW TO USE THIS TEMPLATE (UPDATED)

### FOR IMMEDIATE USE (Next 2-3 minutes):

1. **Open:** `COMPLETE_SYLLABUS_v13_FINAL.md`

2. **Find your week's topics:**
   - Search for: `Phase X – Week Y: [WEEK_TITLE]`
   - Extract: Day 1 Core, Day 2 Core, Day 3 Core, Day 4 Core, Day 5 Core
   - (Skip Day 6 Optional Advanced for the visual playbook)

3. **Copy** the **GENERATION PROMPT** section from this file (below)

4. **Replace placeholders** with your syllabus topics:
   - `[WEEK_NUMBER]` → Your week number (from syllabus)
   - `[WEEK_TITLE]` → Week title (from syllabus header)
   - `[DAY_1_TOPIC]` → Day 1 Core topic (from syllabus)
   - `[DAY_2_TOPIC]` → Day 2 Core topic (from syllabus)
   - `[DAY_3_TOPIC]` → Day 3 Core topic (from syllabus)
   - `[DAY_4_TOPIC]` → Day 4 Core topic (from syllabus)
   - `[DAY_5_TOPIC]` → Day 5 Core topic (from syllabus)
   - `[WEB_RESOURCE_URLs]` → 6 tool URLs (see RESOURCE MAPPING below)

5. **Paste** into Claude

6. **Get back:** Complete ~18,000 word playbook (ready to deploy!)

---

## 🔍 HOW TO FIND TOPICS IN SYLLABUS v12

### File Location:
```
COMPLETE_SYLLABUS_v13_FINAL.md
```

### Search Pattern:
Look for sections like:
```
Phase X – Week Y: [WEEK_TITLE]
...
Day 1 Core: [TOPIC_NAME]
Day 2 Core: [TOPIC_NAME]
Day 3 Core: [TOPIC_NAME]
Day 4 Core: [TOPIC_NAME]
Day 5 Core: [TOPIC_NAME]
```

### Example Week 05:
From Syllabus v12:
```
Phase B – Week 5: Tier 1 Critical Patterns (Hash, Stack, Interval, Partition, Kadane)

Day 1 Core: Hash Map & Hash Set Patterns
Day 2 Core: Monotonic Stack
Day 3 Core: Merge Operations & Interval Patterns
Day 4 Core: Partition & Kadane's Algorithm
Day 5 Core: Fast-Slow Pointers
```

Then your replacements would be:
```
[WEEK_NUMBER]     = 05
[WEEK_TITLE]      = Tier 1 Critical Patterns
[DAY_1_TOPIC]     = Hash Map & Hash Set Patterns
[DAY_2_TOPIC]     = Monotonic Stack
[DAY_3_TOPIC]     = Merge Operations & Interval Patterns
[DAY_4_TOPIC]     = Partition & Kadane's Algorithm
[DAY_5_TOPIC]     = Fast-Slow Pointers
```

### ⚠️ IMPORTANT RULES:
- ✅ Use ONLY **Day 1-5 Core** topics (required for interviews/systems)
- ❌ Skip **Day 6 Optional Advanced** (advanced 6.046 content)
- ✅ Copy topic titles exactly as they appear in syllabus
- ✅ These are the topics that matter for the visual playbook

---

## 📚 COMPLETE TOPIC MAPPING: ALL 19 WEEKS (From Syllabus v12)

Use this to quickly identify your week's topics. For exact wording, always cross-check with COMPLETE_SYLLABUS_v13_FINAL.md.

```
WEEK | PHASE | TITLE (Approx) | DAY 1 | DAY 2 | DAY 3 | DAY 4 | DAY 5
─────┼───────┼────────────────┼───────┼───────┼───────┼───────┼──────────────
01   | A     | Foundations I  | RAM   | Big-O | Space | Recur | Peak Find
02   | A     | Foundations II | Array | DynAr | Linked| Stack | Binary Search
03   | A     | Foundations III| Sorts | Merge | Heaps | Hash  | String Match
04   | B     | Patterns I     | 2Ptr  | Slide | Slide | D&C   | Binary Search
05   | B     | Patterns II    | Hash  | Stack | Interval| Partition| F-S
06   | B     | Patterns III   | Palin | Substr| Paren | String| Advanced
07   | C     | Trees I        | BinTr | BST   | Balanced| Tree | Augmented
08   | C     | Graphs I       | Graph | BFS   | DFS   | Connect| SCC
09   | C     | Graphs II      | Dijks | B-F   | F-W   | MST   | DSU
10   | C     | DP I           | Memo  | 1D    | 2D    | Seq   | LCS
11   | C     | DP II          | Tree  | DAG   | Bitmask| Subset| Advanced
12   | D     | Greedy         | Interv| Exchange| MST  | Proof | [Adv]
13   | D     | Analysis       | Aggr  | Account| Potent| DSU   | [Adv]
14   | E     | Matrices       | Trav  | Backtr| Bitmask| NumThy| [Prob]
15   | E     | Strings/Flow   | KMP   | Z-Alg | Manach| Suffix| MaxFlow
16   | F     | Segment/BIT    | SegT  | BIT   | MatExp| Geom  | [Adv]
17   | F     | HLD/FFT        | HLD   | Str   | FFT   | Poly  | [Adv]
18   | F     | Probabilistic  | Bloom | CountMin| HyperLL| Flow | Design
19   | G     | Integration    | Mock1 | Mock2 | Diag  | Integ | Review
```

**Legend:**
- Core days (required): All Days 1-5 shown
- Optional Advanced (skip): Day 6 if present
- [Bracketed] = Optional advanced day

---

## 🔧 GENERATION PROMPT (COPY-PASTE READY)

```
================================================================================
GENERATE WEEK [WEEK_NUMBER] VISUAL CONCEPTS PLAYBOOK (HYBRID)
================================================================================

INSTRUCTIONS:
1. Topics below are from COMPLETE_SYLLABUS_v13_FINAL.md
2. This playbook uses Days 1-5 Core topics (skip Day 6 Optional)
3. Follow the template structure exactly
4. Produce ~18,000 words with 30+ diagrams

================================================================================
METADATA & REQUIREMENTS
================================================================================

WEEK: [WEEK_NUMBER]
TITLE: [WEEK_TITLE]
FILENAME: Week_[WEEK_NUMBER]_Visual_Concepts_Playbook_HYBRID.md
SYLLABUS_SOURCE: COMPLETE_SYLLABUS_v13_FINAL.md
TOTAL_WORDS: ~18,000
FORMAT: Markdown hybrid (ASCII diagrams + web resource links)
DEPLOYMENT: Immediate production use

KEY REQUIREMENT: Works BOTH offline (ASCII only) AND online (with web links)

================================================================================
CORE TOPICS (From Syllabus v12 Days 1-5)
================================================================================

DAY 1 (Core): [DAY_1_TOPIC]
DAY 2 (Core): [DAY_2_TOPIC]
DAY 3 (Core): [DAY_3_TOPIC]
DAY 4 (Core): [DAY_4_TOPIC]
DAY 5 (Core): [DAY_5_TOPIC]

(Day 6 Optional Advanced is NOT included in visual playbook)

Each day covers:
  - Pattern Map (family tree of concepts)
  - 2-3 pattern visualizations (ASCII diagrams)
  - 2-3 failure modes (❌ WRONG vs ✓ CORRECT)
  - 1 quiz question per pattern (3+ per day)
  - 3,000-3,500 words of content

================================================================================
HEADER & LEGEND (REQUIRED)
================================================================================

Title: # 📊 WEEK [WEEK_NUMBER] VISUAL CONCEPTS PLAYBOOK (HYBRID)

Metadata block with:
  - Week number and phase (from syllabus)
  - Theme/Topics: [WEEK_TITLE]
  - Core topics: [DAY_1_TOPIC], [DAY_2_TOPIC], [DAY_3_TOPIC], [DAY_4_TOPIC], [DAY_5_TOPIC]
  - Format: Hybrid (Enhanced ASCII + Web Resource Links)
  - Syllabus source: COMPLETE_SYLLABUS_v13_FINAL.md
  - Purpose statement

Visual Legend:
  - Symbol reference table (| Symbol | Meaning |)
  - Professional Visualization Resources table (6 tools)
    Must include URLs like:
    * [WEB_RESOURCE_1] https://[URL]
    * [WEB_RESOURCE_2] https://[URL]
    * [WEB_RESOURCE_3] https://[URL]
    * [WEB_RESOURCE_4] https://[URL]
    * [WEB_RESOURCE_5] https://[URL]
    * [WEB_RESOURCE_6] https://[URL]

================================================================================
PER-DAY STRUCTURE (REPEAT 5 TIMES, ONE FOR EACH CORE DAY)
================================================================================

### 📅 DAY [X]: [DAY_X_TOPIC]

#### Pattern Map: [CONCEPT_FAMILY_TREE]

Create ASCII family tree showing:
- Main concept and 3-5 subcategories
- Relationships and dependencies
- Hierarchical structure

Example format:
```
CONCEPT_NAME
├─ Variant A
│  ├─ Sub-variant A1
│  └─ Sub-variant A2
├─ Variant B
└─ Variant C
   └─ Special case
```

---

#### Pattern [X].[1]: [FIRST_CONCEPT_NAME]

**Interactive Resource:** 🔗 [WEB_RESOURCE_N](https://URL)

##### Visual 1: [DIAGRAM_TITLE]

Provide enhanced ASCII diagram showing:
- Memory layout OR data structure OR algorithm trace
- Clear labels and annotations
- Step-by-step explanation below diagram

Example format:
```
CONCEPT VISUALIZATION:
────────────────────

Component A       Component B
┌──────────┐     ┌──────────┐
│  Data    │────→│ Result   │
└──────────┘     └──────────┘
    │                 │
    └─── Property ────┘

EXPLANATION:
- What the diagram shows
- Key invariant maintained
- Why this structure matters
```

Number of visuals: Provide 2-3 enhanced ASCII diagrams per pattern

---

#### Pattern [X].[2]: [SECOND_CONCEPT_NAME]

##### Visual 1: [DIAGRAM_TITLE]

[ASCII diagram + explanation]

---

#### Common Failure Modes (Day [X])

##### Failure 1: [COMMON_MISTAKE]

```
❌ WRONG:
─────────

[Show incorrect code or thinking]
Problem: [What breaks]

✓ CORRECT:
──────────

[Show correct approach]
Why: [Explanation of fix]
```

Provide 2-3 failure mode examples per day

---

#### Quiz Questions (Day [X])

Q1: [Concept-based question about Day [X] topic]
Q2: [Concept-based question about implementation]
Q3: [Concept-based question about real-world application]

================================================================================
CLOSING SECTIONS (REQUIRED)
================================================================================

### 🎯 WEEK [X] VISUAL SUMMARY TABLE

Create table with:
| DAY | TOPIC | Complexity | Key Feature |
Covering all 5 days from the syllabus topics

### 📋 COMPLEXITY REFERENCE TABLE

Create table with:
| Structure/Algo | Time | Space | Use When |
For all major concepts covered in Week [X]

### 🔗 RECOMMENDED LEARNING RESOURCES

List all 6 web resources with:
- Tool name
- URL
- Best for (what topic)
- How to use it (3-4 step process)

### 📝 HOW TO USE THIS PLAYBOOK

Three scenarios:
1. Quick Revision (30 mins)
2. Deep Learning (3-4 hours)
3. Interview Prep (1 hour)

With specific instructions for each

### 🚀 COMPLETE WEEK [X] ECOSYSTEM

Show how Week [X] fits into 19-week curriculum:
- TIER 1 (Core learning) – syllabus files
- TIER 2 (Practice) – practice guides
- TIER 3 (Deep revision) – the playbook itself

### ✅ QUALITY CHECKLIST

Comprehensive checklist covering:
- Standalone functionality (offline works)
- Web integration (6 tools embedded)
- Educational quality (15 quizzes, 8-10 failure modes)
- Production readiness (professional grade)
- Consistency with other weeks

================================================================================
CONTENT REQUIREMENTS
================================================================================

TOPICS: Use ONLY the 5 Core day topics from COMPLETE_SYLLABUS_v13_FINAL.md
  - NOT the Day 6 Optional Advanced topic
  - Follow syllabus topic names exactly

VISUALS (MINIMUM):
  - 30+ enhanced ASCII diagrams total
  - 6+ per day (minimum)
  - Every diagram referenced in text
  - Placed inline (not grouped at end)
  - Trace tables for operations
  - Complexity charts where relevant

FAILURE MODES:
  - 2-3 per day (8-10 total)
  - Show ❌ WRONG with problem explanation
  - Show ✓ CORRECT with fix explanation
  - Defensive learning approach

QUIZ QUESTIONS:
  - 3 per day (15 total)
  - Concept-based (not just recall)
  - No answers provided
  - Test understanding not memorization

WEB RESOURCES:
  - 6 professional tools (embedded as links)
  - Each with "How to use" guide
  - URLs must be valid and current
  - Alternative resources if primary link changes

PATTERN MAPS:
  - 5 family trees (one per day)
  - Show concept hierarchies
  - ASCII format
  - Clear relationships

TABLES:
  - Complexity reference (operations vs O(n))
  - Summary (day overview)
  - Topic vs tools matching
  - Concept comparison

================================================================================
WORD COUNT & PACING
================================================================================

Overall: ~18,000 words
Distribution:
  - Header & Legend: ~800 words
  - Day 1: ~3,200 words (pattern map + visuals + failure modes + quiz)
  - Day 2: ~3,200 words (pattern map + visuals + failure modes + quiz)
  - Day 3: ~3,200 words (pattern map + visuals + failure modes + quiz)
  - Day 4: ~3,200 words (pattern map + visuals + failure modes + quiz)
  - Day 5: ~3,200 words (pattern map + visuals + failure modes + quiz)
  - Closing sections: ~1,200 words

Per pattern (within day):
  - Pattern map: 150 words
  - Visual 1: 400-500 words (diagram + explanation)
  - Visual 2: 400-500 words (if present)
  - Failure modes: 300 words per failure (2-3 total)
  - Quiz: 50 words per question (3-4 total)

================================================================================
MARKDOWN FORMATTING RULES
================================================================================

HEADERS:
  # Main title (file name)
  ## Section (day, resources, etc.)
  ### Subsection (pattern, visual, etc.)
  #### Sub-subsection (specific topic)

EMPHASIS:
  **bold** for: Key terms, code keywords, emphasis, topics from syllabus
  *italic* for: Concepts, variable names, new ideas
  `code` for: Actual code snippets, formula symbols

LISTS:
  - Use unordered lists (bullets) for items
  - Use ordered lists only for step-by-step procedures
  - Keep lists flat (no nested bullets)

CODE BLOCKS:
  ```
  ASCII diagrams and tables use code blocks
  ```

TABLES:
  | Column | Column | Column |
  |--------|--------|--------|
  | Cell   | Cell   | Cell   |

LINKS:
  [Link text](https://URL)
  Used only for web resources section

================================================================================
TONE & VOICE
================================================================================

- Conversational but authoritative
- Use analogies and metaphors naturally
- Explain "why" before "how"
- Address reader directly when appropriate
- Balance academic rigor with accessibility
- Assume intermediate technical knowledge
- Reference syllabus topics by name

SAMPLE VOICE:

"The syllabus for this week covers [TOPIC_NAME]. Think of [concept] like [analogy]. 
Here's why this matters: [context]. Watch what happens when [scenario]: [trace]. 
This is exactly how [real system] implements it because [reason]."

================================================================================
QUALITY VALIDATION
================================================================================

Before finalizing, verify:

✅ TOPICS:
  - All 5 day topics from COMPLETE_SYLLABUS_v13_FINAL.md
  - Day 6 Optional NOT included
  - Topic names match syllabus exactly
  - Core days clearly labeled

✅ OFFLINE FUNCTIONALITY:
  - All ASCII diagrams render without external dependencies
  - No image files referenced
  - Pure markdown (GitHub-friendly)
  - Works on any markdown viewer

✅ WEB INTEGRATION:
  - 6 professional tools embedded
  - URLs are valid and current
  - "How to use" guide for each tool
  - Alternative resources mentioned

✅ EDUCATIONAL COMPLETENESS:
  - 5 days covered (30+ topics from syllabus)
  - 3 quiz questions per day (15 total)
  - 2-3 failure modes per day (8-10 total)
  - Pattern family trees show relationships
  - Complexity stated for each concept
  - Real-world applications mentioned
  - References to syllabus where relevant

✅ PRODUCTION READINESS:
  - Professional formatting
  - Consistent terminology
  - Clear navigation structure
  - 100% complete coverage
  - ~18,000 words target
  - Syllabus topics correctly extracted

✅ CONSISTENCY WITH WEEKS 01-04:
  - Same hybrid structure
  - Same visual style (ASCII)
  - Same resource embedding pattern
  - Same quiz/failure mode format
  - Same table formats

================================================================================
FINAL DELIVERABLE
================================================================================

OUTPUT FILE:
  - Name: Week_[WEEK_NUMBER]_Visual_Concepts_Playbook_HYBRID.md
  - Format: Markdown
  - Size: ~18,000 words
  - Status: 100% production-ready
  - Deployment: Immediate use
  - Syllabus-aligned: Yes

QUALITY:
  - Professional grade
  - MIT-level content
  - Proven template (tested on Weeks 01-04)
  - Scalable (same template works for Weeks 05-19)
  - Syllabus-verified (topics extracted from official curriculum)

USAGE:
  - Standalone learning (offline)
  - Enhanced learning (with web tools)
  - Interview preparation
  - Teaching others
  - Quick reference

================================================================================
BEGIN GENERATION
================================================================================

Start by creating the file header with:
1. Main title and metadata (include syllabus reference)
2. Visual legend and resource guide
3. Then proceed day-by-day through the 5-day structure
4. Finish with closing sections
5. Include ALL requirements above

Generate complete, production-ready markdown file ready for immediate deployment.

Topics are from COMPLETE_SYLLABUS_v13_FINAL.md – use them EXACTLY as written.

================================================================================
```

---

## 🎯 KEY UPDATES FROM VERSION 12.1

✅ **Now references COMPLETE_SYLLABUS_v13_FINAL.md** for all topic extraction  
✅ **Clear extraction instructions** showing how to find Day 1-5 Core topics  
✅ **Warns against including** Day 6 Optional Advanced content  
✅ **Complete topic mapping** table for all 19 weeks (extracted from syllabus)  
✅ **Metadata updated** to show syllabus source  
✅ **Quality checklist** now verifies syllabus alignment  
✅ **Generation prompt** now emphasizes syllabus-verified topics  

---

## ✅ USAGE WORKFLOW

1. **Open:** COMPLETE_SYLLABUS_v13_FINAL.md
2. **Find:** Your week's section (Phase X – Week Y: [TITLE])
3. **Extract:** Day 1-5 Core topics (skip Day 6 Optional)
4. **Copy:** This generation prompt
5. **Replace:** 8 placeholders with exact syllabus topics
6. **Paste:** Into Claude
7. **Get:** Production-ready playbook

---

## 📞 SUPPORT

**Files referenced:**
- COMPLETE_SYLLABUS_v13_FINAL.md (official curriculum)
- VISUAL_TEMPLATE_QUICK_START_GUIDE.txt (step-by-step)
- DEPLOYMENT_READY_SUMMARY.md (overview)

**Proven on:** Weeks 01-04 (artifacts 161-164)
**Ready for:** Weeks 05-19 (same template, syllabus-aligned)

---

**Version:** 12.2 | **Status:** ✅ PRODUCTION-READY TEMPLATE  
**Updated:** Saturday, January 10, 2026, 5:48 PM IST  
**Syllabus Reference:** COMPLETE_SYLLABUS_v13_FINAL.md
