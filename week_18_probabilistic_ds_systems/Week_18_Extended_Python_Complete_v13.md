# Week 18 Extended Python Complete v13

> 🧭 **Navigation:** [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS_v13.md)
> 
> 💡 **Instructor Note:** *This Python (3.11+) guide provides reference implementations, zero-allocation patterns, and test verification drills. Use it alongside daily study as a practical code blueprint.*

---

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

---

> 🧭 **Navigation:** [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS_v13.md)
