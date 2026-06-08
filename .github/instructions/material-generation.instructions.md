---
applyTo: "week_*/**/*.md"
description: "Use when generating or revising weekly DSA material, instructional files, visual playbooks, support files, and C#/Python extended files."
---

Material generation policy

- Use `Old/` as archived reference only. Do not pull active generation content from it.
- Resolve scope using v13-first precedence: COMPLETE_SYLLABUS_v13.md -> v13/*.md -> final_prompts/*.md -> README_v13.md/Readme.md -> Old/v12_prompts_archive references.
- Match syllabus scope exactly for the selected week/day before adding enhancements.
- Preserve folder-local naming and style conventions.
- Enforce canonical instructional naming: `Week_XX_Day_YY_[Topic_Name]_Instructional.md` with 2-digit week/day.
- Keep concept progression clear: context -> model -> mechanism -> performance -> application.
- Keep explanations no-code-first; use C#/.NET snippets only when required for concept, mechanism, or interview implementation clarity.
- Include complexity, invariants, trade-offs, edge cases, and interview follow-ups.
- Keep worked examples deterministic: avoid self-correcting narration (for example, "wait, let me retrace") inside learner-facing walkthroughs.
- For number-theory topics, explicitly distinguish single-query primality check vs sieve precomputation, state modular-inverse existence conditions, and mention overflow caveats for large moduli.
- Reconcile terms with neighboring files in the same week to avoid drift.
- Remove accidental prompt artifacts (for example external upload links) if found in target files.
- Ensure files progress from basic to advanced variations in a fluid sequence.
- Prefer meaningful emoji section markers for learner-facing readability.
- Ensure all day topics and subtopics are fully covered; add visual representations where they materially improve comprehension.
- If complete coverage needs extra depth, permit extension beyond normal ranges up to 30,000 words for that file.
- Enhancement rule: cover syllabus topics first, then add only directly relevant adjacent subtopics that are essential for course mastery.
- Use the canonical chapter flow for instructional files: Context & Motivation, Mental Model, Mechanics, Performance, Integration.
- Keep README/support/playbook/extended language files visually consistent with the same week's instructional tone.

Self-check before finalizing content

1. Scope check: all required topics and subtopics covered.
2. Correctness check: claims and complexity are internally consistent.
3. State check: transitions, constraints, and termination are explicit.
4. Quality check: no placeholders, no contradictory definitions.
5. Quality check: no Latex symbols or formula use plain text versions.
6. Integration check: references to related week support files are valid.
7. Structure check: instructional files include motivation, model, mechanics, performance, integration.
8. Depth check: file length aligns with unified targets from `final_prompts/QUALITY_VALIDATION_CHECKLIST.md`.
9. Walkthrough integrity check: traces/tables/code comments agree with each other and do not contain contradictory intermediate states.
10. Arithmetic constraints check: formulas include domain preconditions (for example, inverse exists only when gcd(a, m) = 1).
11. Unified self-check pass (use `final_prompts/QUALITY_VALIDATION_CHECKLIST.md` and `final_prompts/UNIFIED_MASTER_PROMPT.md` mandatory checks):
	- verify references,
	- verify logic flow,
	- verify counts/numbers,
	- verify state transitions,
	- verify termination,
	- scan red flags,
	- fix detected issues,
	- publish only when all checks pass.
