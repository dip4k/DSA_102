# Unified Weekly Batch Prompt

Use this for generating full week packages in one workflow.

Input schema
- Week number
- Week folder
- Required outputs:
  - Instructional files
  - Support files
  - Visual playbook
  - Full playbook
  - Language support (optional: C#, Python)

Global generation rules
- Strictly follow template structure and file naming conventions from active context files.
- Instructional naming format: `Week_XX_Day_YY_[Topic_Name]_Instructional.md` (2-digit week/day).
- Keep explanation no-code-first; use C#/.NET only when required for implementation clarity.
- Ensure all day topics/subtopics are fully covered with sufficient depth and visuals where helpful.
- If complete required coverage exceeds normal ranges, allow extension up to 30,000 words per file.

Batch process
1. Validate week scope from v13 syllabus.
2. Generate files in dependency order:
   - Guidelines and summary
   - Day instructional files
   - Interview QA and roadmap
   - Daily checklist
   - Visual/full playbook
   - Language support files
3. Run consistency pass against all generated files.
4. Run cohesion pass:
  - confirm basic -> advanced progression inside each file
  - confirm day-to-day sequence continuity across the week
  - confirm support files reinforce instructional content (not diverge)
5. Produce concise completion report.

Consistency checks
- Naming pattern consistency
- Topic and subtopic coverage
- Complexity and invariant consistency
- No duplicate/conflicting definitions
- Unified 8-step self-check pass completed (use `final_prompts/QUALITY_VALIDATION_CHECKLIST.md` and `final_prompts/UNIFIED_MASTER_PROMPT.md` mandatory checks)

Depth checks
- Instructional files target 7,500-15,000 words.
- Support files target 3,000-5,000 words.
- Visual playbook targets 8,000-18,000 words.
- Language support targets 3,000-6,000 words.

Structure checks
- Instructional files include all 5 chapter blocks.
- Complexity/trade-off and failure-mode sections are present.
- Interview follow-up and variation coverage included.
