# Week 09 Extended Python Complete v13

Purpose: build Python fluency for weighted graph algorithms: shortest paths, MSTs, and DSU.

## Focus tags
- Must: Dijkstra, Bellman-Ford, Floyd-Warshall, Kruskal/Prim intuition, Union-Find
- Should: path reconstruction, stale-entry skipping, DSU rank/path compression details
- Optional: advanced shortest-path variants

## Pattern 1: Dijkstra
```python
import heapq

def dijkstra(graph, start):
    dist = {node: float('inf') for node in graph}
    dist[start] = 0
    heap = [(0, start)]
    while heap:
        d, u = heapq.heappop(heap)
        if d != dist[u]:
            continue
        for v, w in graph[u]:
            nd = d + w
            if nd < dist[v]:
                dist[v] = nd
                heapq.heappush(heap, (nd, v))
    return dist
```

## Pattern 2: Bellman-Ford
```python
def bellman_ford(n, edges, start):
    dist = [float('inf')] * n
    dist[start] = 0
    for _ in range(n - 1):
        changed = False
        for u, v, w in edges:
            if dist[u] != float('inf') and dist[u] + w < dist[v]:
                dist[v] = dist[u] + w
                changed = True
        if not changed:
            break
    return dist
```

## Pattern 3: Floyd-Warshall
```python
def floyd_warshall(mat):
    n = len(mat)
    dist = [row[:] for row in mat]
    for k in range(n):
        for i in range(n):
            for j in range(n):
                if dist[i][k] + dist[k][j] < dist[i][j]:
                    dist[i][j] = dist[i][k] + dist[k][j]
    return dist
```

## Pattern 4: Union-Find
```python
class DSU:
    def __init__(self, n):
        self.parent = list(range(n))
        self.rank = [0] * n

    def find(self, x):
        if self.parent[x] != x:
            self.parent[x] = self.find(self.parent[x])
        return self.parent[x]

    def union(self, a, b):
        ra, rb = self.find(a), self.find(b)
        if ra == rb:
            return False
        if self.rank[ra] < self.rank[rb]:
            ra, rb = rb, ra
        self.parent[rb] = ra
        if self.rank[ra] == self.rank[rb]:
            self.rank[ra] += 1
        return True
```

## Pattern 5: Kruskal
```python
def kruskal(n, edges):
    dsu = DSU(n)
    total = 0
    chosen = []
    for w, u, v in sorted(edges):
        if dsu.union(u, v):
            total += w
            chosen.append((u, v, w))
    return total, chosen
```

## Practice ladder
- Must: network delay time, course-schedule-style graph review, MST basics, DSU connectivity
- Should: count shortest paths, minimum effort path, Bellman-Ford negative-cycle reasoning
- Optional: all-pairs reconstruction and weighted-graph optimizations
