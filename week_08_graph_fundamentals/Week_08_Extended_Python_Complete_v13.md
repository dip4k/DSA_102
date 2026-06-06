# Week 08 Extended Python Complete v13

Purpose: build Python fluency for graph representations, BFS, DFS, topological sort, connectivity, and SCC intuition.

## Focus tags
- Must: graph representation, BFS, DFS, connected components, bipartite check
- Should: topological sort, cycle detection, SCC overview
- Optional: advanced graph decomposition patterns

## Python mindset for Week 08
- Use adjacency lists by default for sparse graphs.
- `collections.deque` is the default frontier for BFS.
- Recursive DFS is concise, but iterative DFS is safer when depth may be large.

## Pattern 1: adjacency list build
```python
def build_undirected_graph(n, edges):
    graph = [[] for _ in range(n)]
    for u, v in edges:
        graph[u].append(v)
        graph[v].append(u)
    return graph
```

## Pattern 2: BFS shortest path in unweighted graph
```python
from collections import deque

def bfs_dist(graph, start):
    dist = [-1] * len(graph)
    dist[start] = 0
    q = deque([start])
    while q:
        u = q.popleft()
        for v in graph[u]:
            if dist[v] == -1:
                dist[v] = dist[u] + 1
                q.append(v)
    return dist
```

## Pattern 3: DFS traversal
```python
def dfs(graph, start):
    seen = set()
    order = []
    def visit(u):
        seen.add(u)
        order.append(u)
        for v in graph[u]:
            if v not in seen:
                visit(v)
    visit(start)
    return order
```

## Pattern 4: bipartite graph check
```python
from collections import deque

def is_bipartite(graph):
    color = {}
    for start in range(len(graph)):
        if start in color:
            continue
        color[start] = 0
        q = deque([start])
        while q:
            u = q.popleft()
            for v in graph[u]:
                if v not in color:
                    color[v] = color[u] ^ 1
                    q.append(v)
                elif color[v] == color[u]:
                    return False
    return True
```

## Pattern 5: Kahn topological sort
```python
from collections import deque

def topo_sort(n, edges):
    graph = [[] for _ in range(n)]
    indeg = [0] * n
    for u, v in edges:
        graph[u].append(v)
        indeg[v] += 1
    q = deque(i for i in range(n) if indeg[i] == 0)
    order = []
    while q:
        u = q.popleft()
        order.append(u)
        for v in graph[u]:
            indeg[v] -= 1
            if indeg[v] == 0:
                q.append(v)
    return order if len(order) == n else []
```

## Practice ladder
- Must: number of islands, flood fill, provinces, bipartite, course schedule basics
- Should: word ladder, open the lock, eventual safe states, SCC conceptual tracing
- Optional: Tarjan/Kosaraju implementation follow-up
