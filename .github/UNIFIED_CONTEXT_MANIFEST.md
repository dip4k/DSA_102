# Unified Context Manifest (v13)

Purpose
- Keep generation and revision behavior consistent across active repository content.
- Enforce a single source-of-truth hierarchy aligned with v13 goals.
- Prevent legacy prompt/version drift during Copilot and GenAI workflows.

Active Scope
- Include: week_*, final_prompts, v13, Coding_Practice, LLD, Paradigms, system-design, traversal guides, root metadata files.
- Exclude by default: Old/ (archived historical assets).
- Treat `week_*/v10` and `week_*/v12` as compatibility snapshots unless explicitly targeted.

Source Precedence (highest to lowest)
1. COMPLETE_SYLLABUS_v13.md
2. v13/*.md
3. final_prompts/*.md
4. Readme.md and README_v13.md
5. COMPLETE_CONTENT_INDEX.md
6. Old/v12_prompts_archive/*.md (compatibility references)

Context Rules
- If references conflict, follow source precedence above.
- Do not copy legacy wording when newer v13 wording exists.
- Preserve folder-local naming conventions while keeping conceptual alignment global.
- Remove prompt artifacts or external upload placeholders from generated outputs.

Material Quality Baseline
- Why before how; narrative-first structure.
- Explicit complexity, invariants, trade-offs, and edge cases.
- Interview utility with variant prompts and failure-mode notes.
- Use C# only where implementation examples are required.

Operational Checklist For Any Generation Task
1. Confirm target week/day scope from v13 syllabus.
2. Read neighboring files in target folder.
3. Generate/revise with quality baseline.
4. Validate consistency with support files and roadmap.
5. Update learning_tracking logs when practice-facing work is done.
