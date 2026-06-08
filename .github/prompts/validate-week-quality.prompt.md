---
agent: agent
tools: [vscode, execute, read, agent, edit, search, web, browser, todo]
description: "Validate generated week files for structure, depth, cohesion, sequence, and syllabus coverage using unified quality checklist."
---

Validate week ${input:week_number} quality.

Inputs
- Week folder: ${input:week_folder}
- Scope: ${input:scope} (instructional/support/visual/language/full)

Validation sources
- `final_prompts/QUALITY_VALIDATION_CHECKLIST.md`
- `final_prompts/UNIFIED_TEMPLATE_NARRATIVE.md`
- `COMPLETE_SYLLABUS_v13.md`

Checks
1. Topic and subtopic coverage completeness.
2. Structural compliance (required section blocks).
3. Depth compliance (word-count target bands).
4. Cohesion and sequence flow (basic -> advanced progression).
5. Technical quality (complexity, invariants, edge cases, failure modes).
6. Cross-file consistency across support and language files.

Output
- Provide pass/fail by check category.
- List exact files needing remediation.
- Apply fixes directly when requested.
