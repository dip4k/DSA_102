# Python DSA Standards (Secondary Language for FAANG)

## Purpose
Python is used as the secondary language for rapid whiteboard prototyping, algorithmic clarity, and polyglot interview flexibility.

## Conventions & Best Practices
1. **Target Python Version:** Python 3.11+.
2. **Type Annotations:**
   - Always include standard PEP 484 type hints on function signatures:
     ```python
     def two_sum(nums: list[int], target: int) -> list[int]:
     ```
3. **Core Modules:**
   - **Queue / Deque:** Use `from collections import deque`. Never use `list.pop(0)` ($O(N)$); always use `deque.popleft()` ($O(1)$).
   - **Heaps:** Use `import heapq`. Remember that `heapq` is a **Min-Heap**. For Max-Heap, negate values (`-val`) or use custom tuples `(-priority, val)`.
   - **Binary Search:** Use `bisect.bisect_left` and `bisect.bisect_right`.
   - **Counters / Defaults:** Use `collections.Counter` and `collections.defaultdict`.
4. **Efficiency & Idioms:**
   - Midpoint calculation: `mid = (left + right) // 2` (Python handles arbitrarily large integers automatically, but mention overflow in languages like C#/Java).
   - In-place string mutation: Python strings are immutable; convert to a list `chars = list(s)` when in-place array algorithms are required, and `''.join(chars)` at the end.
   - List comprehensions: Keep them readable; avoid deeply nested expressions in algorithmic solutions.

