# Copilot Repository Instructions

Purpose
- Use this repository to generate high-quality DSA learning material and interview prep content.
- Preserve the curriculum's narrative-first teaching style and technical rigor.
- Keep all content aligned with v13 syllabus files and existing week folder conventions.

Repository Scope
- Treat `Old/` as archived material. Do not use it as active generation context unless explicitly requested.
- Prefer active folders: week_*, final_prompts, v13, Coding_Practice, LLD, Paradigms, system-design, traversal guides.
- Treat `week_*/v10` and `week_*/v12` subfolders as historical compatibility references unless the task explicitly targets them.

Primary Sources
1. Readme.md and README_v13.md
2. COMPLETE_SYLLABUS_v13.md
3. COMPLETE_CONTENT_INDEX.md
4. v13/*.md syllabus refinements
5. final_prompts/UNIFIED_SYSTEM_CONTEXT.md
6. final_prompts/UNIFIED_MASTER_PROMPT.md
7. final_prompts/UNIFIED_TEMPLATE_NARRATIVE.md
8. final_prompts/language_support/CSHARP_STUDY_MATERIAL_LOGIC.md
9. final_prompts/language_support/PYTHON_STUDY_MATERIAL_LOGIC.md
10. Existing files in target week folder

Conflict Resolution
- If two references disagree, prefer v13 sources in this order:
	1) COMPLETE_SYLLABUS_v13.md
	2) v13/*.md
	3) final_prompts/*.md
	4) Readme.md and README_v13.md
	5) Old/v12_prompts_archive/*.md (compatibility only)

Non-Negotiable Rules
- Accuracy first: no wrong complexity, invariants, or algorithmic claims.
- Teach why before how: motivation, model, mechanism, trade-offs.
- Keep naming consistent with existing repository week/day patterns.
- Prefer no-code explanation first; include C# only when implementation is necessary.
- Avoid adding contradictory content across week support files.
- Use meaningful emoji section markers in learner-facing files when they improve scanability and continuity.
- Follow the canonical instructional arc: context and motivation -> mental model -> mechanics -> performance -> integration.

Required Quality Gates For Generated Content
- Explicit time and space complexity for each approach.
- Pattern trigger cues and selection rationale.
- Edge case coverage and failure modes.
- At least one wrong-approach contrast for non-trivial topics.
- Interview utility: variant follow-ups and communication cues.
- Cohesive progression from basic -> intermediate -> advanced variations.
- Clear structure in instructional files: motivation, model, mechanics, performance, integration.
- Separate support files by role (guidelines, summary, interview QA, roadmap, checklist).
- Follow unified depth targets in `final_prompts/QUALITY_VALIDATION_CHECKLIST.md`.
- Keep emoji usage intentional and stable across related files in the same week package.

Output Behavior
- Generate or update files directly in the repository.
- Keep formatting clean and markdown-friendly.
- Use ASCII diagrams or Mermaid only when they materially improve understanding.
- Do not leak prompt artifacts, upload links, or external scratch tokens into generated curriculum files.
- After generation, update tracking docs in learning_tracking/.
- Ensure learner-facing files are fluid and connected, not fragmented checklists.

Tracking Integration
- Practice log: learning_tracking/Practice_Log.md
- Mistake and retry queue: learning_tracking/Question_Bank.md
- Weekly reflection: learning_tracking/Weekly_Review.md
- Mock outcomes: learning_tracking/Mock_Interview_Log.md
