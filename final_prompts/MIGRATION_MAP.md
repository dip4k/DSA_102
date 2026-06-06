# Migration Map: Legacy -> Unified

This document maps legacy prompt assets to the unified framework.

Legacy sources archived
- `v12_prompts/*` -> `Old/v12_prompts_archive/*`
- `prompt_dump.txt` -> `Old/context_engineering_archive/prompt_dump.txt`
- `prompt_usage_samples.txt` -> `Old/context_engineering_archive/prompt_usage_samples.txt`

Unified replacements
- Legacy system prompts -> `UNIFIED_SYSTEM_CONTEXT.md`
- Legacy master prompt -> `UNIFIED_MASTER_PROMPT.md`
- Legacy narrative template -> `UNIFIED_TEMPLATE_NARRATIVE.md`
- Legacy weekly batch prompt -> `UNIFIED_WEEKLY_BATCH_PROMPT.md`
- Legacy C# support prompts -> `language_support/CSHARP_STUDY_MATERIAL_LOGIC.md`
- New Python support logic -> `language_support/PYTHON_STUDY_MATERIAL_LOGIC.md`

Notes
- Historical prompt files remain available in `Old/` for audit and compatibility.
- Active generation should use `final_prompts/` only.
