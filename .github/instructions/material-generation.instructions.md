---
applyTo: "week_*/**/*.md"
description: "Use when generating or revising weekly DSA material, instructional files, visual playbooks, support files, and C#/Python extended files."
---

Material generation policy

- Use `Old/` as archived reference only. Do not pull active generation content from it.
- Resolve scope using v13-first precedence: COMPLETE_SYLLABUS_v13.md -> v13/*.md -> final_prompts/*.md -> README_v13.md/Readme.md -> Old/v12_prompts_archive references.
- Match syllabus scope exactly for the selected week/day before adding enhancements.
- Preserve folder-local naming and style conventions.
- Keep concept progression clear: context -> model -> mechanism -> performance -> application.
- Include complexity, invariants, trade-offs, edge cases, and interview follow-ups.
- Reconcile terms with neighboring files in the same week to avoid drift.
- Remove accidental prompt artifacts (for example external upload links) if found in target files.
- Ensure files progress from basic to advanced variations in a fluid sequence.
- Prefer meaningful emoji section markers for learner-facing readability.
- Use the canonical chapter flow for instructional files: Context & Motivation, Mental Model, Mechanics, Performance, Integration.
- Keep README/support/playbook/extended language files visually consistent with the same week's instructional tone.

Self-check before finalizing content

1. Scope check: all required topics and subtopics covered.
2. Correctness check: claims and complexity are internally consistent.
3. State check: transitions, constraints, and termination are explicit.
4. Quality check: no placeholders, no contradictory definitions.
5. Integration check: references to related week support files are valid.
6. Structure check: instructional files include motivation, model, mechanics, performance, integration.
7. Depth check: file length aligns with unified targets from `final_prompts/QUALITY_VALIDATION_CHECKLIST.md`.
