# Copilot Repository Instructions

Purpose
- Use this repository to generate high-quality DSA learning material and interview prep content.
- Preserve the curriculum's narrative-first teaching style and technical rigor.
- Keep all content aligned with v13 syllabus files and existing week folder conventions.

## VS Code & Tool Integration Rules
- **Direct Workspace Modifications**: When providing file edits or creations, always output standard multi-line codeblocks prefixed with the exact file path directive `// filepath: c:\Repos\DSA_Master_Curriculum_v8\...` or `# filepath: ...` so that the **Copilot Edits** pane can automatically parse and apply the changes to your workspace.
- **Incremental Diffs**: For large files, use `// ...existing code...` comments to mark unmodified code blocks. This minimizes token usage and allows clean merging.
- **Terminal Operations (Windows/PowerShell)**: Streamline directory creation and file initialization using single-line copy-paste ready PowerShell commands. e.g., `New-Item -ItemType File -Force -Path ...` or `git` commands.
- **Unit Testing Pane**: When requested to write or run tests, ensure imports match the project's framework (C# NUnit/MS Test, or Python `unittest`/`pytest`) so that VS Code's **Test Explorer** can automatically discover and run them.
- **Deletion Operations**: When instructed to delete/cleanup files, provide clear PowerShell commands to do so: `Remove-Item -Path "c:\Repos\DSA_Master_Curriculum_v8\path\to\file" -Force`.

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
- Use programming language snippets only when required; keep conceptual explanation language-agnostic first, then use C#/.NET when code is needed.
- Avoid adding contradictory content across week support files.
- Use meaningful emoji section markers in learner-facing files when they improve scanability and continuity.
- Follow the canonical instructional arc: context and motivation -> mental model -> mechanics -> performance -> integration.

General Week-Generation Quality Checks
- Strictly follow template structure and file naming convention from active context files.
- Enforce instructional filename format: `Week_XX_Day_YY_[Topic_Name]_Instructional.md` (2-digit week/day).
- Ensure complete topic/subtopic coverage for each day with sufficient explanatory depth.
- Include visual diagrams/representations when they improve concept clarity.
- If required scope cannot be covered within standard depth bands, allow extension up to 30,000 words for that file.
- Apply enhancement rule: cover syllabus scope first, then add directly related prerequisite/adjacent subtopics that are essential to course outcomes.
- Recheck generated content for structure, correctness, and continuity before finalization.

Direct Workspace Modification Support
- When performing file creation or updates, structure response outputs using explicit, industry-standard diff patterns so the VS Code "Copilot Edits" parallel window can auto-ingest and apply them.
- If running terminal tasks, output single-line, copy-paste ready PowerShell command sequences to create directories and stream contents directly into files.

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
- Apply the 8-step unified self-check (primary reference: `final_prompts/QUALITY_VALIDATION_CHECKLIST.md`; align with mandatory checks in `final_prompts/UNIFIED_MASTER_PROMPT.md`):
	1) references exist and match,
	2) logic flow continuity,
	3) number/count correctness,
	4) state-transition consistency,
	5) termination correctness,
	6) red-flag scan,
	7) fix issues,
	8) publish only after all checks pass.

Output Behavior
- Structure outputs so they are immediately applicable via VS Code's "Apply to Editor" feature or the Copilot Edits pane.
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
