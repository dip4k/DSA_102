---
name: dsa-interactive-runner
description: Scaffold practice templates, run test cases for C# (dotnet test) and Python (py -m unittest / pytest), and verify algorithmic solutions locally with automated test runners.
---

# DSA Interactive Runner Skill

## Use When
- The user asks to practice a specific problem from `Senior_problem_solution/` or `Senior_dsa_question_list.md`.
- Scaffolding a practice workspace for a problem in `Coding_Practice/Senior_Practice/`.
- Executing unit tests to verify solution correctness, edge case handling, zero-allocation constraints, and benchmarks.

---

## Workspace Structure

The primary test-driven practice suite is located in `Coding_Practice/Senior_Practice/`:

```text
Coding_Practice/Senior_Practice/
├── csharp/
│   ├── SeniorPractice.Tests.csproj
│   ├── Phase01_Arrays_Hashing/
│   │   ├── Problem01_TwoSum.cs
│   │   └── Problem01_TwoSum_Tests.cs
│   └── ... (Phases 01 through 11)
└── python/
    ├── __init__.py
    ├── phase01_arrays_hashing/
    │   ├── problem01_two_sum.py
    │   └── test_problem01_two_sum.py
    └── ... (Phases 01 through 11)
```

---

## Test Execution Commands

### C# (.NET 8/9):
Run entire test suite:
```powershell
dotnet test Coding_Practice\Senior_Practice\csharp\SeniorPractice.Tests.csproj
```

Run tests for a specific phase or problem:
```powershell
dotnet test Coding_Practice\Senior_Practice\csharp\SeniorPractice.Tests.csproj --filter "FullyQualifiedName~TwoSum"
```

### Python (3.11+):
Run entire test suite:
```powershell
py -m unittest discover -s Coding_Practice\Senior_Practice\python
```

Run tests for a specific problem:
```powershell
py -m unittest Coding_Practice.Senior_Practice.python.phase01_arrays_hashing.test_problem01_two_sum
```

---

## Scaffolding New DSA Problems

1. **Locate Target Phase:** Identify target phase folder (e.g., `Phase03_SlidingWindow`).
2. **Generate Files:**
   - C#: `ProblemXX_<Slug>.cs` and `ProblemXX_<Slug>_Tests.cs`.
   - Python: `problemxx_<slug>.py` and `test_problemxx_<slug>.py`.
3. **Include Comprehensive Test Vectors:**
   - Happy path / standard cases.
   - Minimal boundaries (empty, single element).
   - Extreme bounds (negative numbers, `int.MinValue`, `int.MaxValue`).
   - Duplicate values and parity conditions.
4. **Run Verification:** Run `dotnet test` or `py -m unittest` to confirm test suite discovery.
