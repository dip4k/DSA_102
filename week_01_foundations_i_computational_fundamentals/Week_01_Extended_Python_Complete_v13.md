# Week 01 Extended Python Complete v13

Purpose: turn Week 01 foundations into Python reasoning and implementation fluency.

## Week 01 Python focus

Python is a good language for Week 01 because the syntax stays out of the way while you learn recursion, memoization, and problem decomposition. The trade-off is that Python hides low-level memory details and has a shallow default recursion limit, so you need to be explicit about what is conceptual versus what is language behavior.

Topics covered:
- RAM model vs Python runtime reality
- asymptotic reasoning in Python code
- recursion patterns in Python
- memoization with dictionaries and decorators
- iterative fallback when recursion depth is risky

---

## Pattern recognition map

| Signal | Python pattern | Why |
|---|---|---|
| "process one smaller subproblem" | linear recursion | direct reduction, one recursive call |
| "same subproblem repeated" | memoization | cache results, avoid exponential blow-up |
| "depth may be large" | iterative rewrite | Python recursion limit is a practical constraint |
| "need complexity explanation" | loop + call-count analysis | makes Big-O visible in code |

---

## 1. Linear recursion

```python
def sum_array(arr, i=0):
    if i == len(arr):
        return 0
    return arr[i] + sum_array(arr, i + 1)
```

Mental model:
- base case stops at the first index beyond the array
- each call owns one element and delegates the rest
- time is O(n), stack space is O(n)

Common pitfalls:
- using `i > len(arr)` instead of `i == len(arr)`
- forgetting that Python recursion is not tail-call optimized

---

## 2. Tree recursion and overlap

```python
def fib_naive(n):
    if n < 2:
        return n
    return fib_naive(n - 1) + fib_naive(n - 2)
```

Why it is slow:
- repeated evaluation of the same states such as `fib(3)`, `fib(4)`
- exponential call tree, roughly O(2^n)

Use this only as the conceptual baseline.

---

## 3. Memoization with a dictionary

```python
def fib_memo(n, memo=None):
    if memo is None:
        memo = {}
    if n in memo:
        return memo[n]
    if n < 2:
        return n
    memo[n] = fib_memo(n - 1, memo) + fib_memo(n - 2, memo)
    return memo[n]
```

Why this matters:
- each state is computed once
- time becomes O(n)
- extra space is O(n) for memo + recursion stack

Python note:
- explicit dictionaries are best for learning
- `functools.lru_cache` is great once the concept is clear

```python
from functools import lru_cache

@lru_cache(maxsize=None)
def fib_cached(n):
    if n < 2:
        return n
    return fib_cached(n - 1) + fib_cached(n - 2)
```

---

## 4. Recursion depth reality in Python

Python is not the language to assume deep recursion safety.

```python
import sys
print(sys.getrecursionlimit())
```

Typical guidance:
- recursive interview examples: fine
- deep tree/graph recursion on large input: risky
- production-safe alternative: use an explicit stack or queue

Example rewrite:

```python
def factorial_iterative(n):
    result = 1
    for value in range(2, n + 1):
        result *= value
    return result
```

---

## 5. Peak finding mindset in Python

```python
def peak_1d(nums):
    left, right = 0, len(nums) - 1
    while left < right:
        mid = (left + right) // 2
        if nums[mid] < nums[mid + 1]:
            left = mid + 1
        else:
            right = mid
    return left
```

Key invariant:
- a peak always exists in the current interval
- each step throws away one side safely

Time: O(log n)
Space: O(1)

---

## Python interview checklist for Week 01

- Explain why recursion works before writing code.
- State base case and progress condition out loud.
- Mention recursion depth risk in Python when relevant.
- Use dictionary memoization before decorators if you need to teach the concept.
- Distinguish conceptual RAM model from Python object overhead.

---

## Recommended practice

Must:
- factorial
- sum of array recursively
- fibonacci naive vs memoized
- climbing stairs memoized
- 1D peak finding

Should:
- reverse string recursively
- max element recursively
- recursion tree tracing by hand

Optional:
- 2D peak-finding reasoning
- compare memoization with bottom-up tabulation
