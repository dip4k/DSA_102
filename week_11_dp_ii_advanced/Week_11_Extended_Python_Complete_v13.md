# Week 11 Extended Python Complete v13

Purpose: build Python fluency for advanced DP structures: trees, DAGs, bitmasking, and state compression.

## Focus tags
- Must: tree DP, DAG DP, subset/bitmask DP, state compression intuition
- Should: rerooting, path-count DP, optimization of state size
- Optional: mixed advanced DP variants

## Pattern 1: tree DP
```python
def diameter(root):
    best = 0
    def dfs(node):
        nonlocal best
        if not node:
            return 0
        left = dfs(node.left)
        right = dfs(node.right)
        best = max(best, left + right)
        return 1 + max(left, right)
    dfs(root)
    return best
```

## Pattern 2: DAG DP with topo order
```python
from collections import deque

def longest_path_dag(n, edges):
    graph = [[] for _ in range(n)]
    indeg = [0] * n
    for u, v in edges:
        graph[u].append(v)
        indeg[v] += 1
    q = deque(i for i in range(n) if indeg[i] == 0)
    topo = []
    while q:
        u = q.popleft()
        topo.append(u)
        for v in graph[u]:
            indeg[v] -= 1
            if indeg[v] == 0:
                q.append(v)
    dp = [0] * n
    for u in reversed(topo):
        for v in graph[u]:
            dp[u] = max(dp[u], 1 + dp[v])
    return dp
```

## Pattern 3: bitmask DP skeleton
```python
def tsp_dp(dist):
    n = len(dist)
    INF = 10**18
    dp = [[INF] * n for _ in range(1 << n)]
    dp[1][0] = 0
    for mask in range(1 << n):
        for u in range(n):
            if dp[mask][u] == INF:
                continue
            for v in range(n):
                if mask & (1 << v):
                    continue
                nxt = mask | (1 << v)
                dp[nxt][v] = min(dp[nxt][v], dp[mask][u] + dist[u][v])
    full = (1 << n) - 1
    return min(dp[full][u] + dist[u][0] for u in range(n))
```

## Practice ladder
- Must: tree diameter / tree robber style DP, DAG longest path, subset DP basics
- Should: all paths in DAG, rerooting intuition, TSP subset DP
- Optional: aggressive state compression and mixed-constraint DP
