---
name: lld-interactive-runner
description: Scaffold LLD practice templates with domain models, interface contracts, and multi-threaded concurrency unit tests in C# (dotnet test) and Python (py -m unittest). Run tests, detect race conditions, and verify thread safety.
---

# LLD Interactive Runner Skill

## Use When
- Scaffolding a hands-on LLD practice workspace in `Coding_Practice/LLD_Practice/`.
- Running unit tests and multi-threaded concurrency stress tests for LLD problems.
- Validating thread safety, deadlock absence, and race condition resilience.

---

## Workspace Structure for LLD

All LLD code practice is housed under `Coding_Practice/LLD_Practice/`:

```text
Coding_Practice/LLD_Practice/
├── csharp/
│   ├── LLDPractice.Tests.csproj
│   └── Problem07_CustomCache/
│       ├── CustomCache.cs
│       └── Problem07_CustomCache_Tests.cs
└── python/
    ├── __init__.py
    └── problem07_custom_cache/
        ├── __init__.py
        ├── custom_cache.py
        └── test_custom_cache.py
```

---

## Test Execution Commands

Always use the following commands to execute LLD tests:

### C# (.NET 8/9):
Run all LLD tests:
```powershell
dotnet test Coding_Practice\LLD_Practice\csharp\LLDPractice.Tests.csproj
```

Run tests for a specific problem:
```powershell
dotnet test Coding_Practice\LLD_Practice\csharp\LLDPractice.Tests.csproj --filter "FullyQualifiedName~Problem07_CustomCache"
```

### Python (3.11+):
Run all LLD tests:
```powershell
py -m unittest discover -s Coding_Practice\LLD_Practice\python -p "test_*.py"
```

Run tests for a specific problem:
```powershell
py -m unittest Coding_Practice.LLD_Practice.python.problem07_custom_cache.test_custom_cache
```

---

## Scaffolding New LLD Problems

When the user asks to practice an LLD problem (e.g., `Problem06_ParkingLot`):
1. **Create Directory:** `Coding_Practice/LLD_Practice/csharp/Problem06_ParkingLot/` and `Coding_Practice/LLD_Practice/python/problem06_parking_lot/`.
2. **Generate Starter Interface & Domain Models:** Provide clean domain models, value objects, and interfaces.
3. **Generate Unit & Concurrency Test Fixture:**
   - Functional test cases (valid flows, boundary capacities, fee calculations).
   - Multi-threaded concurrency tests (`Task.WhenAll` in C# or `threading.Thread` in Python) to stress-test concurrent requests (e.g., multiple cars entering simultaneously when only 1 spot remains).
4. **Compile and Run Verification:** Run `dotnet test` or `py -m unittest` to confirm the test harness executes and flags unimplemented logic.
