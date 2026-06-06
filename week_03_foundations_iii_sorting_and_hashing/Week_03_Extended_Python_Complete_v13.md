# Week 03 Extended Python Complete v13

Purpose: turn Week 03 sorting, heap, and hashing theory into Python implementation fluency.

## Week 03 Python focus

Python gives you excellent built-ins, but Week 03 is where you must understand when to trust them and when to explain the underlying algorithm anyway.

Topics covered:
- elementary sorts as teaching tools
- merge sort and quicksort mechanics
- heap usage with `heapq`
- dictionary and set behavior as hash tables
- rolling hash and Rabin-Karp in Python

---

## Pattern recognition map

| Signal | Python tool / pattern | Why |
|---|---|---|
| tiny or nearly sorted | insertion-sort reasoning | explains hybrid sort behavior |
| guaranteed stable built-in sort | `sorted`, `.sort()` | Python uses TimSort |
| repeated min extraction | `heapq` | binary heap with O(log n) push/pop |
| expected O(1) lookup | `dict` / `set` | practical hash-table backbone |
| substring matching with rolling window | polynomial rolling hash | reusable for Rabin-Karp |

---

## 1. Elementary sorts for invariants

Insertion sort is the most educational of the O(n^2) sorts.

```python
def insertion_sort(nums):
    a = nums[:]
    for i in range(1, len(a)):
        key = a[i]
        j = i - 1
        while j >= 0 and a[j] > key:
            a[j + 1] = a[j]
            j -= 1
        a[j + 1] = key
    return a
```

Invariant:
- `a[0:i]` is sorted before each outer iteration

---

## 2. Merge sort and quicksort

Merge sort:

```python
def merge_sort(a):
    if len(a) <= 1:
        return a
    mid = len(a) // 2
    left = merge_sort(a[:mid])
    right = merge_sort(a[mid:])
    out = []
    i = j = 0
    while i < len(left) and j < len(right):
        if left[i] <= right[j]:
            out.append(left[i])
            i += 1
        else:
            out.append(right[j])
            j += 1
    out.extend(left[i:])
    out.extend(right[j:])
    return out
```

Quicksort partition idea:
- choose pivot
- maintain `< pivot` and `>= pivot` regions
- recurse on subranges

Interview note:
- in Python interviews, you often explain quicksort rather than hand-implement the most optimized in-place variant unless asked explicitly

---

## 3. Built-in sorting in Python

```python
items = [(2, 'b'), (1, 'a'), (2, 'a')]
items.sort(key=lambda x: (x[0], x[1]))
```

What to say:
- Python sort is stable
- implemented with TimSort
- excellent for real-world partially ordered data
- still know merge/quicksort trade-offs for algorithm interviews

---

## 4. Heap operations with `heapq`

```python
import heapq

heap = []
heapq.heappush(heap, 5)
heapq.heappush(heap, 2)
heapq.heappush(heap, 8)
smallest = heapq.heappop(heap)
```

Use cases:
- top-k smallest or largest
- priority scheduling
- Dijkstra and Prim later in the course

Max-heap trick:

```python
heapq.heappush(heap, -value)
```

---

## 5. Hash tables in Python

```python
counts = {}
for x in [1, 2, 2, 3]:
    counts[x] = counts.get(x, 0) + 1
```

Key truths:
- `dict` and `set` are hash-table based
- average expected O(1) lookup/update
- worst case can degrade, so say expected or average when discussing complexity

---

## 6. Rolling hash and Rabin-Karp

```python
def rabin_karp(text, pattern):
    if not pattern or len(pattern) > len(text):
        return []
    base = 257
    mod = 10**9 + 7
    m = len(pattern)
    power = pow(base, m - 1, mod)
    ph = 0
    th = 0
    for i in range(m):
        ph = (ph * base + ord(pattern[i])) % mod
        th = (th * base + ord(text[i])) % mod
    out = []
    for i in range(len(text) - m + 1):
        if ph == th and text[i:i+m] == pattern:
            out.append(i)
        if i + m < len(text):
            th = (th - ord(text[i]) * power) % mod
            th = (th * base + ord(text[i + m])) % mod
    return out
```

Important wording:
- expected O(n + m) with good hashing
- worst case O(nm) if collisions force many verifications

---

## Python interview checklist for Week 03

- Explain the invariant for insertion sort, merge, or partition.
- Know when to use `sorted()` in real code and when to explain the underlying algorithm.
- Use `heapq` for heap problems.
- Say `dict`/`set` are expected O(1), not unconditional O(1).
- Mention verification on hash matches in Rabin-Karp.

---

## Recommended practice

Must:
- insertion sort trace
- merge two sorted lists/arrays
- top-k frequent with heap or bucket logic
- valid anagram / group anagrams
- Rabin-Karp pattern search

Should:
- merge sort implementation
- quicksort partition walkthrough
- min-heap vs max-heap patterns

Optional:
- compare `heapq` solution vs full sort solution
- experiment with custom objects and sort keys
