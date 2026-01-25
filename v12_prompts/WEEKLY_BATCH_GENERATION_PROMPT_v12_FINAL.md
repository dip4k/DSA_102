# 🎯 WEEKLY_BATCH_GENERATION_PROMPT_v12_FINAL.md — NARRATIVE-FIRST BATCH GENERATION

**Version:** 12.0 FINAL (Aligned with Template_v12 & SYSTEM_CONFIG_v12_FINAL)  
**Status:** ✅ OFFICIAL WEEKLY GENERATION PROMPT  
**Purpose:** Generate weekly content using the **Narrative-First (v12)** architecture.

---

## 📋 HOW TO USE THIS PROMPT

Use this inside an AI chat session that has:
- `SYSTEM_CONFIG_v12_FINAL.md`
- `MASTER_PROMPT_v12_FINAL.md`
- `Template_v12_Narrative_FINAL.md`
- `EMOJI_ICON_GUIDE_v12.md` (emoji icon guide)
- `COMPLETE_SYLLABUS_v13_FINAL.md`
- `SYSTEM_PROMPT_v12_FOR_AI_CHAT_FINAL.md`
- `V12_prompt_usage.md`
- `SYSTEM_PROMPT_v12_EXTENDED_SUPPORT_CSHARP.md`
- `WEEKLY_BATCH_GENERATION_PROMPT_v12_FINAL.md` (this file)

---

## course syllabus or curriculum file
**use this file for syllabus** :
- `COMPLETE_SYLLABUS_v13_FINAL.md`

## ⚠️ BATCHING RULES (CRITICAL)

1. **One File Per Response:** Do NOT generate multiple files at once.
2. **Wait for Signal:** After generating one file, stop and wait for the user to say "Continue" or "Next".
3. **v12 Adherence:** Ensure every file follows the 5-Chapter Narrative structure.

---

## 🎯 DELIVERABLES (PER WEEK)

### PART 1: INSTRUCTIONAL FILES (5 Files)
For Week [X], generate instructional files for Days 1-5 using `Template_v12_Narrative.md` and `EMOJI_ICON_GUIDE_v12.md`.

**Filename Format:** `Week_[X]_Day_[Y]_[Topic_Name]_Instructional.md`

**Quality Checklist (v12 FINAL):**
1. **Flow:** 5-Chapter Narrative Arc (Context → Mental Model → Mechanics → Reality → Mastery).
2. **Word Count:** **12,000 – 18,000 words** (MIT-Level Depth).
3. **Visuals:** Minimum **5-8 inline visuals** (ASCII diagrams, Trace Tables).
4. **Real Systems:** 3-5 detailed case studies woven into Chapter 4 (no lists).
5. **Tone:** Master Teacher / Senior Engineer (no "Section 1" labels).

---

### PART 2: SUPPORT FILES (5 Files)
After instructional files, generate these 5 support files.

**Word Count Target:** **3,000 – 5,000 words per file.**

1. **`Week_[X]_Guidelines.md`**
   - *Tone:* Strategic Advisor.
   - *Content:* Objectives, Day-by-day overview, Study strategy, Pitfalls, Time allocation.

2. **`Week_[X]_Summary_Key_Concepts.md`**
   - *Tone:* Grad Student Notes.
   - *Content:* Narrative summary, Concept map, Comparison tables, Insights, Misconceptions.

3. **`Week_[X]_Interview_QA_Reference.md`**
   - *Tone:* Interview Coach.
   - *Content:* 30-50 Questions (grouped by topic) + Follow-ups. **NO ANSWERS.**

4. **`Week_[X]_Problem_Solving_Roadmap.md`**
   - *Tone:* Training Coach.
   - *Content:* Strategy, 3-Stage Progression (Basic→Variations→Integration), Templates, Pitfalls.

5. **`Week_[X]_Daily_Progress_Checklist.md`**
   - *Tone:* Daily Planner.
   - *Content:* Daily actions, Concepts to master, Reflection prompts.

---

## 🚚 INTERACTION FLOW

1. **User:** "Generate Week 1 content."
2. **AI:** Generates `Week_01_Day_1...Instructional.md` (v12 format). **STOPS.**
3. **User:** "Continue."
4. **AI:** Generates `Week_01_Day_2...Instructional.md` (v12 format). **STOPS.**
...
5. **AI:** Generates `Week_01_Day_5...Instructional.md`. **STOPS.**
6. **User:** "Generate Support Files."
7. **AI:** Generates `Week_01_Guidelines.md`. **STOPS.**
... (until all 5 support files are done)

---

**Status:** ✅ Ready for v12 FINAL Batch Generation
