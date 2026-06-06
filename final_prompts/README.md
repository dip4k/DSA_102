# Unified Prompt Framework (v13)

Purpose
- Provide one final prompt system for generating DSA curriculum content at scale.
- Preserve the narrative-first teaching style and technical rigor from earlier prompt generations.
- Support both C# and Python study-material logic without duplicating framework structure.

What is in this folder
- `UNIFIED_SYSTEM_CONTEXT.md`: source precedence, quality bars, and generation policies.
- `UNIFIED_MASTER_PROMPT.md`: primary instruction for material generation.
- `UNIFIED_TEMPLATE_NARRATIVE.md`: narrative template for instructional and support content.
- `UNIFIED_WEEKLY_BATCH_PROMPT.md`: workflow for week-level batch generation.
- `language_support/CSHARP_STUDY_MATERIAL_LOGIC.md`: C# implementation-focused extension logic.
- `language_support/PYTHON_STUDY_MATERIAL_LOGIC.md`: Python implementation-focused extension logic.
- `MIGRATION_MAP.md`: mapping from legacy prompt files to unified equivalents.

Design principles
- One framework, multiple language tracks.
- v13 syllabus first, compatibility references second.
- Why-before-how pedagogy maintained.
- Interview readiness and production realism retained.

Usage order
1. Read `UNIFIED_SYSTEM_CONTEXT.md`.
2. Use `UNIFIED_MASTER_PROMPT.md` for generation requests.
3. Apply `UNIFIED_TEMPLATE_NARRATIVE.md` structure.
4. Add language-specific layer from `language_support/` when required.
5. Use `UNIFIED_WEEKLY_BATCH_PROMPT.md` for full-week generation.
