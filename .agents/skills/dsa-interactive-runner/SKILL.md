---
name: dsa-interactive-runner
description: Scaffold practice templates, run test cases for C# (dotnet test) and Python (pytest), and verify algorithmic solutions locally.
---

# DSA Interactive Runner Skill

## Use When
- The user asks to practice a specific problem from `Senior_problem_solution/` or `Senior_dsa_question_list.md`.
- Scaffolding a practice workspace for a problem in `Coding_Practice/`.
- Executing unit tests to verify solution correctness, edge case handling, and benchmarks.

## Workflow
1. **Scaffold:** Create a dedicated directory under `Coding_Practice/problems/<problem_id>_<slug>/`:
   - `Solution.cs` and `SolutionTests.cs` (or `solution.py` and `test_solution.py`).
2. **Template Generation:**
   - Provide problem signature, constraints in comments, and blank method with invariant hints.
   - Include test cases covering general cases, empty/single-element bounds, and extreme values.
3. **Execution & Verification:**
   - Run `dotnet test` or `pytest` via `run_command` to validate the user's implementation.
   - Report test results, execution time, and any failing test vectors.

