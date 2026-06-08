---
agent: agent
tools: [vscode, execute, read, agent, edit, search, web, browser, todo]
description: "Run a repository-wide unification pass (excluding Old/) to align docs and generation context with v13 goals."
---

Run a full unification pass across active repository folders.

Rules
- Exclude `Old/` from edits and context unless explicitly requested.
- Apply source precedence from `.github/UNIFIED_CONTEXT_MANIFEST.md`.
- Normalize version labels and references to v13 where intended.
- Preserve historically intentional comparison docs if they are analysis artifacts.

Targets
- Root metadata files (Readme.md, README_v13.md, START_HERE.md, COMPLETE_* files)
- Week folders and support files
- final_prompts and v13 references
- .github context files

Output
- Update files directly.
- Return concise summary: changed files, key normalizations, residual drift list.
