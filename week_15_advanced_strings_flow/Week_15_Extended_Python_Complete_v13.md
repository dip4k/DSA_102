# Week 15 Extended Python Complete v13

Purpose: build Python fluency for advanced string matching, range queries, and network-flow fundamentals.

## Focus tags
- Must: Z-algorithm intuition, segment/Fenwick reasoning, max-flow foundations
- Should: matching reductions, lazy propagation concepts, advanced string comparisons
- Optional: heavier flow variants and role-specific extensions

## Pattern 1: Z-algorithm skeleton
```python
def z_array(s):
    z = [0] * len(s)
    left = right = 0
    for i in range(1, len(s)):
        if i <= right:
            z[i] = min(right - i + 1, z[i - left])
        while i + z[i] < len(s) and s[z[i]] == s[i + z[i]]:
            z[i] += 1
        if i + z[i] - 1 > right:
            left, right = i, i + z[i] - 1
    return z
```

## Pattern 2: Fenwick tree
```python
class Fenwick:
    def __init__(self, n):
        self.bit = [0] * (n + 1)

    def add(self, i, delta):
        i += 1
        while i < len(self.bit):
            self.bit[i] += delta
            i += i & -i

    def prefix(self, i):
        i += 1
        total = 0
        while i > 0:
            total += self.bit[i]
            i -= i & -i
        return total
```

## Pattern 3: Edmonds-Karp skeleton
```python
from collections import deque

def bfs_level(cap, flow, s, t, parent):
    n = len(cap)
    for i in range(n):
        parent[i] = -1
    parent[s] = s
    q = deque([s])
    while q:
        u = q.popleft()
        for v in range(n):
            if parent[v] == -1 and cap[u][v] - flow[u][v] > 0:
                parent[v] = u
                if v == t:
                    return True
                q.append(v)
    return False
```

## Practice ladder
- Must: Z-array matching trace, Fenwick point-update/prefix-query, bipartite matching via flow idea
- Should: segment-tree range query/update reasoning, Edmonds-Karp implementation, flow-to-matching reduction
- Optional: Dinic/min-cost max-flow follow-up
