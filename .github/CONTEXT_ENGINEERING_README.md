# Context Engineering Setup

This folder contains workspace-level Copilot context for professional material generation and practice tracking.

Structure
- copilot-instructions.md: global repository behavior.
- instructions/: scoped behavior by file patterns.
- prompts/: reusable chat prompts for common workflows.
- skills/: domain workflows that can be invoked when needed.
- final_prompts/ (repo root): canonical unified prompt, template, and language-support framework.

How to use
1. Keep copilot-instructions.md as stable baseline policy.
2. Put narrowly scoped rules in instructions/*.instructions.md via applyTo.
3. Use prompts/*.prompt.md for repeatable tasks.
4. Use skills for multi-step workflows.

Recommended maintenance
- Update syllabus references when versions change.
- Keep descriptions explicit so tools are discoverable.
- Avoid broad applyTo patterns unless truly global.
- Keep `Old/` archived and excluded from default generation context.
- Use `.github/UNIFIED_CONTEXT_MANIFEST.md` as the central context precedence reference.
- Keep `final_prompts/` as the single active prompt framework and archive legacy prompt systems under `Old/`.
