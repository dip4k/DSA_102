# 🎯 WEEKLY GENERATION PROMPT v9.2 — FOR BATCH CONTENT GENERATION

**Version:** 9.2 (Unified with v8.0 + v9.1)  
**Status:** ✅ OFFICIAL WEEKLY GENERATION PROMPT  
**Purpose:** Generate all weekly instructional & support files systematically for DSA Master Curriculum v9.2

---

## 📋 HOW TO USE THIS PROMPT

Use this prompt inside an AI chat session that already has:

- `SYSTEM_PROMPT_v9.2_FOR_AI_CHAT.md`
- `SYSTEM_CONFIG_v9.2_FINAL.md`
- `MASTER_PROMPT_v9.2_FINAL.md`
- `TEMPLATE_v9.2_FINAL.md`
- `COMPLETE_SYLLABUS_v9.2_FINAL.md`
- `EMOJI_ICON_GUIDE_v8.md`

All instructional files MUST follow `TEMPLATE_v9.2_FINAL.md` and v9.2 quality standards.

---

## ⚠️ BATCH GENERATION TO AVOID TOKEN LIMITS

**Do NOT generate all 11 weekly files in a single response.**

Instead, follow this **strict batching rule**:

1. Generate **only 1 file per response** (one markdown file).
2. Wait for the user to acknowledge/save it.
3. Only then move to the next file.
4. If the user says:  
   - `"Continue with next file"` → generate the next requested file.  
   - `"Pause"` → stop file generation until explicitly resumed.

You MUST **never bundle multiple files** in the same response.

---

## 📌 WEEKLY BATCH GENERATION REQUEST (v9.2)

Copy-paste and fill for a given week:

# 🎓 DSA MASTER CURRICULUM v9.2 — WEEKLY BATCH GENERATION REQUEST

## 📌 CURRENT WEEK TARGET

**Generation Request:** Complete Week [X] file set  
- **Week Number:** [X] (1–16, 4.5, 5.5, 13, 4.75, etc. as per `COMPLETE_SYLLABUS_v9.2_FINAL.md`)  
- **Duration:** [X] days (usually 5; Week 13 may be 6–7)  
- **Total Files:** 11–13 files per week  

All instructional files must:
- Follow `TEMPLATE_v9.2_FINAL.md`  
- Use generic **concept/type/variation** wording (not operation-only)  
- Respect **NO SOLUTIONS PROVIDED** policy in Supplementary section  

---

## 🎯 DELIVERABLES REQUIRED (v9.2)

### PART 1: INSTRUCTIONAL FILES (5–7 per week)

For Week [X], generate the following instructional files (topics from `COMPLETE_SYLLABUS_v9.2_FINAL.md`):

**File 1:** `Week_[X]_Day_1_[Topic_Name]_Instructional.md`  
**File 2:** `Week_[X]_Day_2_[Topic_Name]_Instructional.md`  
**File 3:** `Week_[X]_Day_3_[Topic_Name]_Instructional.md`  
**File 4:** `Week_[X]_Day_4_[Topic_Name]_Instructional.md`  
**File 5:** `Week_[X]_Day_5_[Topic_Name]_Instructional.md`  
(+ Day 6/7 if applicable for that week)

For **each** instructional file, enforce:

- 🤔 **Section 1: The Why** (900–1500 words)  
- 📌 **Section 2: The What** (900–1500 words)  
  - Use **CORE CONCEPTS — LIST ALL (MANDATORY)**  
  - Accept **concepts / types / variations / patterns**, not just “operations”.
- ⚙️ **Section 3: The How** (900–1500 words)  
- 🎨 **Section 4: Visualization** (900–1500 words)  
- 📊 **Section 5: Critical Analysis** (600–900 words)  
- 🏭 **Section 6: Real Systems** (500–800 words)  
- 🔗 **Section 7: Concept Crossovers** (400–600 words)  
- 📐 **Section 8: Mathematical** (300–500 words)  
- 💡 **Section 9: Algorithmic Intuition** (500–800 words)  
- ❓ **Section 10: Knowledge Check** (200–300 words)  
- 🎯 **Section 11: Retention Hook** (800–1200 words)  

Plus **MANDATORY blocks**:

- 🧩 **5 Cognitive Lenses** (800–1200 words total)  
- ⚔️ **Supplementary Outcomes** (≤2500 words total) with:
  - ⚔️ Practice Problems: 8–10 (with sources), **NO SOLUTIONS PROVIDED**  
  - 🎙️ Interview Questions: 6+ (questions + follow-ups only, **NO SOLUTIONS PROVIDED**)  
  - ⚠️ Common Misconceptions: 3–5  
  - 🚀 Advanced Concepts: 3–5  
  - 🔗 External Resources: 3–5  

**Word Count Target per file (v9.2):** 7,500–15,000 words total.

---

### PART 2: SUPPORT FILES (5+ per week)

For Week [X], generate support files (adapted to v9.2, but v8 “look & feel”):

**Support File 1:** `Week_[X]_Guidelines.md`  
- Weekly learning objectives  
- Key concepts overview  
- Learning approach & methodology  
- Tips & strategies for the week  
- How topics connect across the week  
- Practice strategy and time management  
- Weekly checklist  

**Support File 2:** `Week_[X]_Summary_Key_Concepts.md`  
- Quick reference for all topics of the week  
- Core ideas in bullet format  
- Concept map / relationships (ASCII)  
- Highlights per day  

**Support File 3:** `Week_[X]_Interview_QA_Reference.md`  
- Consolidated **questions only** (no answers, to align with NO SOLUTIONS policy if you want strict parity)  
  - If you prefer answers here, you can locally decide; v9.2 core rule applies to instructional files.  

**Support File 4:** `Week_[X]_Problem_Solving_Roadmap.md`  
- Recommended practice progression  
- Strategy from simple → complex  
- Common pitfalls  

**Support File 5:** `Week_[X]_Daily_Progress_Checklist.md`  
- Day-wise checkpoints  
- Key concepts & tasks per day  

(You can extend with additional support files as needed.)

---

## 📋 FORMAT & QUALITY REQUIREMENTS (v9.2)

### Format

- Markdown `.md`, UTF-8, LF line endings  
- Use `#`, `##`, `###` with emojis per `EMOJI_ICON_GUIDE_v8.md`  
- No LaTeX; use plain-text math like `O(n log n)`  

### Code Policy

- Default: **no code** (logic-first).  
- If absolutely necessary: **C# only**.  
- No Python/Java/C++ code blocks.

### Cognitive Lenses

Each instructional file must include all 5 lenses:

- 🖥️ Computational  
- 🧠 Psychological  
- 🔄 Design Trade-off  
- 🤖 AI/ML Analogy  
- 📚 Historical  

---

## 🚚 DELIVERY & BATCHING RULES

### File Delivery Method

- ✅ Generate **ONE FILE PER RESPONSE**  
- ✅ Clearly label file name at top  
- ✅ Entire content of that file in Markdown  
- ✅ Stop after one file and wait for user signal  

### Delivery Order (Recommended)

1. All **instructional files** for Week [X], in order: Day 1 → Day 5 (→ Day 6/7).  
2. Then **support files** for Week [X].  

Example interaction pattern:

1. System/User: “Generate `Week_1_Day_1_RAM_Model_And_Pointers_Instructional.md`.”  
2. AI: Return **only that file**.  
3. User: “Continue with Day 2.”  
4. AI: Return `Week_1_Day_2_..._Instructional.md`.  
5. Repeat until all requested files are generated.

---

## ✅ VERIFICATION CHECKLIST (PER INSTRUCTIONAL FILE)

- [ ] All 11 sections present, correct order  
- [ ] 5 Cognitive Lenses block present  
- [ ] Supplementary Outcomes present  
- [ ] Word count between 7,500–15,000  
- [ ] No LaTeX, no non-C# code  
- [ ] 5–10 real systems mentioned  
- [ ] Complexity table present  
- [ ] Practice Problems: 8–10, **NO SOLUTIONS PROVIDED**  
- [ ] Interview Questions: 6+ with follow-ups, **NO SOLUTIONS PROVIDED**  
- [ ] Emojis & headings follow template  
