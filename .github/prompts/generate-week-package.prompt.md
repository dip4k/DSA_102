---
agent: ask
description: "Generate or revise a full week package (instructional, support, visual, and C# files) aligned to v13 syllabus and repo conventions."
---

Generate a week package for Week ${input:week_number}.

Inputs
- Week folder: ${input:week_folder}
- Scope: ${input:scope} (instructional/support/visual/csharp/full)
- Syllabus source (primary): COMPLETE_SYLLABUS_v13.md
- Syllabus source (secondary): v13/*.md
- Unified context reference: final_prompts/UNIFIED_SYSTEM_CONTEXT.md
- Unified template reference: final_prompts/UNIFIED_TEMPLATE_NARRATIVE.md
- Language references: final_prompts/language_support/CSHARP_STUDY_MATERIAL_LOGIC.md, final_prompts/language_support/PYTHON_STUDY_MATERIAL_LOGIC.md
- Compatibility reference: Old/v12_prompts_archive/COMPLETE_SYLLABUS_v13_FINAL.md
- Optional refinement source: ${input:refinement_file}

Execution requirements
1. Read existing files in the target week folder first.
2. Ignore archived `Old/` content unless explicitly requested.
3. Preserve naming conventions already used in that folder.
4. Ensure all required topics/subtopics are covered.
5. Include complexity and edge-case sections.
6. Add concise visual aids only where they improve clarity.
7. Prefer no-code explanation first; use C# only when needed.
8. Update learning_tracking files after generation session.
9. Validate final outputs against `final_prompts/QUALITY_VALIDATION_CHECKLIST.md`.
10. Use meaningful emoji markers in learner-facing headings and checkpoints where they improve readability.
11. For instructional files, follow the canonical narrative arc: Context & Motivation -> Mental Model -> Mechanics -> Performance -> Integration.

Output
- Create or update files directly in repo.
- Provide a short completion summary with file list and what changed.
