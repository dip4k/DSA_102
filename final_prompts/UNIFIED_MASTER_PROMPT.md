# Unified Master Prompt (v13)

Use this prompt for generating or revising DSA curriculum files.

Instruction to model

You are generating DSA curriculum content for this repository.
Follow the unified system context and repository conventions.

Inputs required
- Target week number and optional day.
- Target file type (instructional, support, visual, full playbook, language support).
- Target folder path.
- Language layer if needed (C# or Python).

Execution rules
1. Scope from v13 syllabus first.
2. Read neighboring files in the same week folder.
3. Preserve naming and tone conventions already used there.
4. Keep explanation no-code-first; add C#/.NET code only when required.
5. Use narrative-first progression:
   - Context and motivation
   - Mental model and invariants
   - Mechanics and examples
   - Performance and trade-offs
   - Integration and mastery
6. Add interview-oriented guidance and failure modes.
7. If implementation is required, use requested language layer (C# or Python).
8. Validate consistency before final output.

Mandatory checks before output
- Complexity correctness
- Edge-case coverage
- Full topic/subtopic coverage for the target day/week
- Walkthrough integrity (trace narrative, state table, and outputs are mutually consistent)
- Domain preconditions on formulas (for example, modular inverse requires gcd(a, m) = 1; Fermat form requires prime modulus)
- No contradictory claims
- No self-correcting draft narration in learner-facing text
- No prompt artifacts
- No unresolved placeholders
- Generic 8-step self-check completed (reference mapping: `Old/v12_prompts_archive/Generic_AI_Self_Check_Correction_Step.md`)

Output style
- Markdown, repository-ready.
- Directly usable as curriculum material.
- Do not include hidden generation metadata.
