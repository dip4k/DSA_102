# Week 18 Extended Python Complete v13

Purpose: build Python intuition for meet-in-the-middle, decomposition strategies, and advanced contest-style range/path techniques.

## Focus tags
- Must: meet-in-the-middle, sqrt decomposition, HLD motivation
- Should: advanced query decomposition patterns
- Optional: specialized hybrid tricks

## Pattern 1: meet-in-the-middle subset sums
```python
def subset_sums(nums):
    out = [0]
    for x in nums:
        out += [s + x for s in out]
    return out
```

## Pattern 2: sqrt decomposition idea
- block size about `int(sqrt(n))`
- precompute per-block aggregates
- answer query by combining full blocks and boundary leftovers

## Pattern 3: heavy-light motivation
- reduce tree path queries to a logarithmic number of segment ranges

## Practice ladder
- Must: subset-sum half splitting, block decomposition reasoning, HLD path decomposition explanation
- Should: query/update trade-off analysis
- Optional: implementation-heavy contest structures
