---
name: dsa-material-generation
description: "Generate or refine DSA curriculum files by week/day with v13 syllabus alignment, quality checks, and naming consistency. Use for instructional, support, visual, and C#/Python extended files."
---

# DSA Material Generation Skill

## Use When
- Creating or revising week/day instructional files.
- Building week support files (guidelines, summary, interview QA, roadmap, checklist).
- Generating visual concepts playbooks.
- Generating C# or Python extended implementation support files.

## Prompt Framework
- Primary unified prompt context is under `final_prompts/`.
- Use `final_prompts/UNIFIED_SYSTEM_CONTEXT.md` and `final_prompts/UNIFIED_TEMPLATE_NARRATIVE.md` by default.
- Use language-specific logic from `final_prompts/language_support/` for implementation-oriented files.
- Validate outputs using `final_prompts/QUALITY_VALIDATION_CHECKLIST.md`.
- Use `Old/v12_prompts_archive/` only when compatibility details are explicitly requested.

## Inputs
- Target week and optional day.
- Target folder path.
- Syllabus source and any refinement source.
- Requested output file types.

## Workflow
1. Scope from syllabus.
2. Read neighboring files in target week folder.
3. Generate content with narrative-first progression.
4. Apply technical self-check (correctness, complexity, edge cases, consistency).
5. Save files with existing naming convention.
6. Update learning_tracking if requested.

## Formatting Expectations
- Use meaningful emoji section markers in learner-facing material where they improve readability.
- Instructional files should follow the canonical 5-part arc: Context & Motivation, Mental Model, Mechanics, Performance, Integration.
- Support files and language guides should feel visually aligned with the instructional files for the same week.

## Content Requirements
- Explicit complexity and trade-off discussion.
- Pattern recognition signals and pitfalls.
- At least one concrete edge-case analysis per major concept.
- Interview follow-up variants for high-frequency patterns.
- Visual elements only where they improve explanation quality.
- Narrative continuity and basic -> advanced progression within each file.
- Proper separation and cohesion between instructional, support, visual, and language-support files.
- Use emoji sparingly but consistently for headings, warnings, and learner checkpoints.

## Depth Targets
- Instructional: 7,500-15,000 words.
- Support files: 3,000-5,000 words.
- Visual playbook: 8,000-18,000 words.
- Language support: 3,000-6,000 words.

## Done Criteria
- Coverage matches week/day scope.
- Claims are consistent and technically correct.
- No placeholders or unresolved references.
- Naming and style match target folder conventions.
