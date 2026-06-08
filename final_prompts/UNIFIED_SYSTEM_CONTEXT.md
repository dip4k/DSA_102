# Unified System Context (v13)

Version
- 1.0 (Unified Prompt System)

Primary goal
- Generate high-quality DSA materials for mastering LeetCode and technical interviews.
- Preserve the repository motto: narrative-first, mental models first, systems-grounded, interview-ready.

Source precedence
1. `COMPLETE_SYLLABUS_v13.md`
2. `v13/*.md`
3. `Readme.md` and `README_v13.md`
4. `COMPLETE_CONTENT_INDEX.md`
5. Legacy compatibility references archived under `Old/v12_prompts_archive/`

Global constraints
- Accuracy over verbosity.
- Explain why before how.
- Include complexity, invariants, trade-offs, and edge cases.
- Avoid prompt artifact leakage and external upload links.
- Use no-code-first explanation; include code only when required.
- Use C#/.NET code only when implementation is required for concept clarity; do not lead with code.

Formatting and pedagogy constraints
- Use meaningful emoji section markers in learner-facing files for scan-ability and consistency.
- Keep narrative flow fluid; avoid checklist-like disconnected blocks.
- Ensure each file progresses from fundamentals to advanced variants naturally.
- Keep support files separated by role (guidelines, summary, interview QA, roadmap, checklist).

Target output depth bands (guidance)
- Instructional files: 7,500-15,000 words (12,000+ preferred for high-complexity days).
- Support files: 3,000-5,000 words each.
- Visual playbooks: 8,000-18,000 words with dense diagrams/tables.
- Language support files (C#/Python): 3,000-6,000 words.
- Weekly full playbook: 6,000-12,000 words.
- If full required topic/subtopic coverage demands it, permit extension up to 30,000 words for the target file.

Naming constraints
- Follow template-driven naming strictly.
- Instructional files: `Week_XX_Day_YY_[Topic_Name]_Instructional.md` (2-digit week/day).

Mandatory structure checks
- Instructional files must include: Context, Mental Model, Mechanics, Performance/Systems, Integration/Mastery.
- Include at least one complexity table and one variation/comparison table where applicable.
- Include wrong-approach contrast and explicit failure modes.
- Include interview follow-up variants for major patterns.

Cohesion and sequence checks
- Topic coverage must map to week/day syllabus topics and subtopics.
- Section transitions should preserve causal flow (motivation -> model -> mechanism -> trade-off -> usage).
- Variations should be ordered basic -> common interview variants -> advanced edge variants.
- Cross-file consistency required between instructional, summary, roadmap, and language support files.

Language policy
- C#: production-grade implementation guidance for interview and engineering readiness.
- Python: clarity-first implementation guidance with idiomatic constructs.
- No language lock-in for conceptual sections.

Output quality gates
- Covers full syllabus scope for target week/day.
- Includes pattern triggers and wrong-approach contrast.
- Includes interview follow-up variants.
- Uses visuals only where they improve understanding.
- Remains consistent with neighboring files in target week folder.
- Meets depth band targets unless topic scope justifies an exception.
- Uses clear emoji-assisted headings and coherent progression.
- Applies unified 8-step self-check before publish using `final_prompts/QUALITY_VALIDATION_CHECKLIST.md` and `final_prompts/UNIFIED_MASTER_PROMPT.md` mandatory checks.
