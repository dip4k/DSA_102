# Week 17 Extended Python Complete v13

Purpose: build Python intuition for advanced DP optimizations, game theory, and counting techniques.

## Focus tags
- Must: recognize when advanced optimization applies
- Should: convex hull trick intuition, Grundy basics, counting identities
- Optional: implementation-heavy contest optimizations

## Pattern 1: DP optimization recognition
- look for transitions of the form `dp[i] = min(dp[j] + m_j * x_i + b_j)`
- if slopes/queries are monotone, a line-container optimization may apply

## Pattern 2: Grundy recurrence
```python
def mex(values):
    s = set(values)
    g = 0
    while g in s:
        g += 1
    return g
```

## Pattern 3: combinatorics helper
```python
from math import comb
```

## Practice ladder
- Must: explain convex hull trick use cases, compute mex by hand, reason about xor of Grundy numbers
- Should: inclusion-exclusion examples and Catalan-number interpretations
- Optional: full advanced line-container implementations
