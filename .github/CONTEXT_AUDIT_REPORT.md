# Context Audit Report (Active Repo Pass)

Date
- 2026-06-06

Scope
- Full markdown inventory and version-drift scan across active repository content.
- `Old/` treated as archived and excluded from normalization edits.

What was normalized
- Added unified source-of-truth manifest.
- Updated global Copilot repo instructions with archive exclusion and precedence rules.
- Updated material generation instruction with v13-first policy and artifact cleanup guard.
- Updated generation prompt to prefer v13 sources and ignore archived content.
- Updated START_HERE.md label drift from v12 to v13.
- Fixed naming/reference drift in Readme.md.

Primary context policy now
1. COMPLETE_SYLLABUS_v13.md
2. v13/*.md
3. final_prompts/*.md
4. Readme.md and README_v13.md
5. COMPLETE_CONTENT_INDEX.md
6. Old/v12_prompts_archive/*.md compatibility references

Residual drift intentionally not auto-rewritten
- Analysis documents that compare v10/v12/v13 under Paradigms/.
- Historical compatibility subfolders under `week_*/v10` and `week_*/v12`.

Residual issues recommended for next pass
- Audit generated week files for accidental prompt/upload artifacts.
- Standardize repeated metadata lines that still mention v11/v12 strengths where not intended.
- Run targeted cleanup for malformed references in graph/week files if they affect learner-facing output.

Outcome
- Repository context is now unified for Copilot and advanced GenAI workflows while preserving historical research artifacts.
