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
4. Use narrative-first progression:
   - Context and motivation
   - Mental model and invariants
   - Mechanics and examples
   - Performance and trade-offs
   - Integration and mastery
5. Add interview-oriented guidance and failure modes.
6. If implementation is required, use requested language layer (C# or Python).
7. Validate consistency before final output.

Mandatory checks before output
- Complexity correctness
- Edge-case coverage
- No contradictory claims
- No prompt artifacts
- No unresolved placeholders

Output style
- Markdown, repository-ready.
- Directly usable as curriculum material.
- Do not include hidden generation metadata.
