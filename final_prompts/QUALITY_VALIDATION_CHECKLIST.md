# Quality Validation Checklist (Unified)

Use this checklist before accepting generated curriculum files.

## 1) Coverage Validation
- Week/day topics and subtopics are fully covered.
- Variations relevant to weekly goal are included.
- No major syllabus item is omitted.

## 2) Structure Validation
- Instructional files contain 5 core chapters.
- Support files are separate and role-pure:
  - Guidelines
  - Summary
  - Interview QA
  - Problem Solving Roadmap
  - Daily Progress Checklist
- Visual and language-support files have their dedicated sections.

## 3) Depth Validation
- Instructional: 7,500-15,000 words target.
- Support: 3,000-5,000 words target.
- Visual playbook: 8,000-18,000 words target.
- Language support: 3,000-6,000 words target.

## 4) Cohesion Validation
- Narrative flows from motivation to mastery without abrupt jumps.
- Basic concepts lead into intermediate and advanced variants.
- Cross-file consistency holds across instructional/support/roadmap/language files.

## 5) Technical Validation
- Time and space complexity included.
- Invariants and edge cases are explicit.
- Wrong-approach contrast and failure modes included.
- Interview follow-up variants present.

## 6) Formatting Validation
- Headings are clean and scan-able.
- Emojis used meaningfully, not excessively.
- Visuals/tables are used where they add clarity.
- No prompt artifacts or unresolved placeholders.

## 7) Walkthrough Integrity Validation
- Worked examples are deterministic and final-form (no exploratory or self-correcting narration).
- Trace logs, inline tables, and code snippets agree on state transitions and outcomes.
- No internal contradiction between explanation text and shown values.

## 8) Arithmetic Preconditions Validation
- Formula validity constraints are stated where required (for example, modular inverse existence conditions).
- Number-theory content distinguishes single-query checks from batch precomputation techniques.
- Overflow caveats are explicit when multiplication can exceed integer bounds under large moduli.
