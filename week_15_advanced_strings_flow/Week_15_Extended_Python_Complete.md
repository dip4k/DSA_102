# Week 15 Extended Python Complete Reference (Python 3.11+)

> 🧭 **Navigation:** [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md)
> 
> 💡 **Instructor Note:** *This Python (3.11+) guide provides complete, battle-tested reference implementations with type annotations, zero unnecessary allocations, and exact invariants matching the C# reference guide.*

---

## 🐍 Production-Grade Python Implementation Blueprint

This reference provides complete, runnable implementations of the core Week 15 patterns:
1. **Z-Algorithm:** Linear-time exact string pattern matching.
2. **Fenwick Tree (BIT):** Logarithmic prefix queries and point updates.
3. **Segment Tree:** Logarithmic range sum queries and point updates.
4. **Edmonds-Karp Max Flow:** Augmenting path BFS on residual graphs with Min-Cut extraction.
5. **Dinic's Algorithm:** Level-graph BFS and blocking-flow DFS with dead-end pointer advance.
6. **Maximum Bipartite Matching:** Optimal network flow reduction with matched-pair extraction.

---

### Pattern 1: Z-Algorithm (Linear Exact String Matching)

```python
def build_z_array(s: str) -> list[int]:
    """
    Computes the Z-array for string s in O(N) time.
    Z[i] is the length of the longest substring starting at s[i] that matches prefix s[0...].
    """
    n = len(s)
    z = [0] * n
    if n == 0:
        return z

    left, right = 0, 0
    for i in range(1, n):
        if i <= right:
            z[i] = min(right - i + 1, z[i - left])
        while i + z[i] < n and s[z[i]] == s[i + z[i]]:
            z[i] += 1
        if i + z[i] - 1 > right:
            left, right = i, i + z[i] - 1
    return z


def find_all_occurrences(text: str, pattern: str) -> list[int]:
    """
    Finds all 0-based starting indices where pattern occurs in text using Z-algorithm.
    Time Complexity: O(N + M) | Space Complexity: O(N + M)
    """
    if not pattern or not text or len(pattern) > len(text):
        return []

    separator = "\x01"  # Sentinel character guaranteed not to collide
    combined = pattern + separator + text
    z = build_z_array(combined)

    m = len(pattern)
    offset = m + 1
    results = []

    for i in range(offset, len(combined)):
        if z[i] == m:
            results.append(i - offset)

    return results
```

---

### Pattern 2: Fenwick Tree (Binary Indexed Tree)

```python
from typing import List

class FenwickTree:
    """
    Binary Indexed Tree (Fenwick Tree) providing O(log N) point updates
    and O(log N) prefix/range queries with O(N) space.
    """
    def __init__(self, size_or_values: int | List[int]):
        if isinstance(size_or_values, int):
            self.n = size_or_values
            self.tree = [0] * (self.n + 1)
        else:
            values = size_or_values
            self.n = len(values)
            self.tree = [0] * (self.n + 1)
            # O(N) linear time construction
            for i in range(1, self.n + 1):
                self.tree[i] += values[i - 1]
                parent = i + (i & -i)
                if parent <= self.n:
                    self.tree[parent] += self.tree[i]

    def add(self, index: int, delta: int) -> None:
        """Adds delta to element at 0-based index."""
        if not (0 <= index < self.n):
            raise IndexError("Index out of bounds.")
        i = index + 1
        while i <= self.n:
            self.tree[i] += delta
            i += i & -i

    def prefix_sum(self, index: int) -> int:
        """Computes sum of elements in A[0...index] inclusive."""
        if index < 0:
            return 0
        if index >= self.n:
            index = self.n - 1
        total = 0
        i = index + 1
        while i > 0:
            total += self.tree[i]
            i -= i & -i
        return total

    def range_query(self, left: int, right: int) -> int:
        """Computes sum of elements in A[left...right] inclusive."""
        if left < 0 or right >= self.n or left > right:
            raise ValueError("Invalid range boundaries.")
        return self.prefix_sum(right) - self.prefix_sum(left - 1)
```

---

### Pattern 3: Segment Tree (Range Sum Queries & Point Updates)

```python
class SegmentTree:
    """
    Segment Tree maintaining range sums and supporting point updates in O(log N) time.
    """
    def __init__(self, arr: List[int]):
        self.n = len(arr)
        self.tree = [0] * (4 * max(1, self.n))
        if self.n > 0:
            self._build(arr, 0, 0, self.n - 1)

    def _build(self, arr: List[int], node: int, start: int, end: int) -> None:
        if start == end:
            self.tree[node] = arr[start]
            return

        mid = start + (end - start) // 2
        left_child = 2 * node + 1
        right_child = 2 * node + 2

        self._build(arr, left_child, start, mid)
        self._build(arr, right_child, mid + 1, end)
        self.tree[node] = self.tree[left_child] + self.tree[right_child]

    def update(self, index: int, value: int) -> None:
        """Updates element at index to value in O(log N) time."""
        if not (0 <= index < self.n):
            raise IndexError("Index out of bounds.")

        def _update(node: int, start: int, end: int) -> None:
            if start == end:
                self.tree[node] = value
                return
            mid = start + (end - start) // 2
            left_child = 2 * node + 1
            right_child = 2 * node + 2
            if index <= mid:
                _update(left_child, start, mid)
            else:
                _update(right_child, mid + 1, end)
            self.tree[node] = self.tree[left_child] + self.tree[right_child]

        _update(0, 0, self.n - 1)

    def query_range(self, ql: int, qr: int) -> int:
        """Returns range sum for A[ql...qr] inclusive in O(log N) time."""
        if ql < 0 or qr >= self.n or ql > qr:
            raise ValueError("Invalid query boundaries.")

        def _query(node: int, start: int, end: int) -> int:
            if qr < start or ql > end:
                return 0
            if ql <= start and end <= qr:
                return self.tree[node]
            mid = start + (end - start) // 2
            left_sum = _query(2 * node + 1, start, mid)
            right_sum = _query(2 * node + 2, mid + 1, end)
            return left_sum + right_sum

        return _query(0, 0, self.n - 1)
```

---

### Pattern 4: Edmonds-Karp Max Flow Algorithm & Min-Cut

```python
from collections import deque
from dataclasses import dataclass, field
from typing import Set, Tuple

@dataclass
class FlowResult:
    max_flow: int
    source_partition: Set[int] = field(default_factory=set)
    sink_partition: Set[int] = field(default_factory=set)
    cut_edges: List[Tuple[int, int, int]] = field(default_factory=list)

class EdmondsKarp:
    """
    Edmonds-Karp Max Flow and Min-Cut solver on residual networks.
    Time Complexity: O(V * E^2) | Space Complexity: O(V^2 + E)
    """
    def __init__(self, nodes: int):
        if nodes <= 0:
            raise ValueError("Node count must be positive.")
        self.n = nodes
        self.capacity = [[0] * nodes for _ in range(nodes)]
        self.residual = [[0] * nodes for _ in range(nodes)]
        self.adj: List[List[int]] = [[] for _ in range(nodes)]

    def add_edge(self, u: int, v: int, cap: int) -> None:
        """Adds directed edge from u to v with specified capacity."""
        if cap < 0:
            raise ValueError("Capacity must be non-negative.")
        self.capacity[u][v] += cap
        self.residual[u][v] += cap
        self.adj[u].append(v)
        self.adj[v].append(u)

    def compute_max_flow_and_min_cut(self, source: int, sink: int) -> FlowResult:
        """Computes Maximum Flow and extracts the bottleneck Minimum Cut."""
        if not (0 <= source < self.n and 0 <= sink < self.n) or source == sink:
            raise ValueError("Invalid source or sink vertex.")

        max_flow = 0
        parent = [-1] * self.n

        def bfs() -> bool:
            parent[:] = [-1] * self.n
            parent[source] = source
            q = deque([source])
            while q:
                u = q.popleft()
                for v in self.adj[u]:
                    if parent[v] == -1 and self.residual[u][v] > 0:
                        parent[v] = u
                        if v == sink:
                            return True
                        q.append(v)
            return False

        # Step 1: Augment flow along shortest path
        while bfs():
            path_flow = float('inf')
            curr = sink
            while curr != source:
                p = parent[curr]
                path_flow = min(path_flow, self.residual[p][curr])
                curr = p

            curr = sink
            while curr != source:
                p = parent[curr]
                self.residual[p][curr] -= path_flow
                self.residual[curr][p] += path_flow
                curr = p

            max_flow += int(path_flow)

        # Step 2: Min-Cut partition extraction
        source_set: Set[int] = set()
        q = deque([source])
        source_set.add(source)
        while q:
            u = q.popleft()
            for v in self.adj[u]:
                if v not in source_set and self.residual[u][v] > 0:
                    source_set.add(v)
                    q.append(v)

        sink_set = {i for i in range(self.n) if i not in source_set}
        cut_edges = []
        for u in source_set:
            for v in self.adj[u]:
                if v in sink_set and self.capacity[u][v] > 0:
                    cut_edges.append((u, v, self.capacity[u][v]))

        return FlowResult(
            max_flow=max_flow,
            source_partition=source_set,
            sink_partition=sink_set,
            cut_edges=cut_edges
        )
```

---

### Pattern 5: Dinic's Maximum Flow Algorithm

```python
class FlowEdge:
    __slots__ = ('to', 'capacity', 'flow', 'rev_index')
    def __init__(self, to: int, capacity: int, rev_index: int):
        self.to = to
        self.capacity = capacity
        self.flow = 0
        self.rev_index = rev_index

class Dinic:
    """
    Dinic's Algorithm for Maximum Network Flow.
    Time Complexity: O(V^2 * E) general, O(E * sqrt(V)) unit/bipartite networks.
    """
    def __init__(self, nodes: int):
        if nodes <= 0:
            raise ValueError("Node count must be positive.")
        self.n = nodes
        self.adj: List[List[FlowEdge]] = [[] for _ in range(nodes)]
        self.level = [-1] * nodes
        self.ptr = [0] * nodes

    def add_edge(self, u: int, v: int, capacity: int) -> None:
        """Adds a directed edge with specified capacity."""
        if capacity < 0:
            raise ValueError("Capacity must be non-negative.")
        forward = FlowEdge(v, capacity, len(self.adj[v]))
        backward = FlowEdge(u, 0, len(self.adj[u]))
        self.adj[u].append(forward)
        self.adj[v].append(backward)

    def compute_max_flow(self, source: int, sink: int) -> int:
        """Computes Maximum Flow from source to sink."""
        if not (0 <= source < self.n and 0 <= sink < self.n) or source == sink:
            raise ValueError("Invalid source or sink vertex.")

        total_flow = 0

        while self._bfs(source, sink):
            self.ptr[:] = [0] * self.n  # Reset work pointers
            while True:
                pushed = self._dfs(source, sink, float('inf'))
                if pushed == 0:
                    break
                total_flow += pushed

        return total_flow

    def _bfs(self, source: int, sink: int) -> bool:
        self.level[:] = [-1] * self.n
        self.level[source] = 0
        q = deque([source])

        while q:
            u = q.popleft()
            for edge in self.adj[u]:
                if edge.capacity - edge.flow > 0 and self.level[edge.to] == -1:
                    self.level[edge.to] = self.level[u] + 1
                    q.append(edge.to)

        return self.level[sink] != -1

    def _dfs(self, u: int, sink: int, flow_in: float) -> int:
        if u == sink or flow_in == 0:
            return int(flow_in)

        for i in range(self.ptr[u], len(self.adj[u])):
            self.ptr[u] = i
            edge = self.adj[u][i]

            if self.level[edge.to] == self.level[u] + 1 and edge.capacity - edge.flow > 0:
                bottleneck = min(flow_in, edge.capacity - edge.flow)
                pushed = self._dfs(edge.to, sink, bottleneck)

                if pushed > 0:
                    edge.flow += pushed
                    self.adj[edge.to][edge.rev_index].flow -= pushed
                    return pushed

        return 0
```

---

### Pattern 6: Maximum Bipartite Matching

```python
class MaximumBipartiteMatching:
    """
    Solves Maximum Bipartite Matching via Dinic's Algorithm in O(E * sqrt(V)) time.
    """
    def __init__(self, left_count: int, right_count: int):
        self.left_count = left_count
        self.right_count = right_count
        self.edges: List[Tuple[int, int]] = []

    def add_edge(self, u: int, v: int) -> None:
        """Adds a candidate matching edge between left node u and right node v."""
        self.edges.append((u, v))

    def solve(self) -> Tuple[int, List[Tuple[int, int]]]:
        source = 0
        sink = self.left_count + self.right_count + 1
        total_nodes = sink + 1

        dinic = Dinic(total_nodes)

        # Source -> Left
        for l in range(1, self.left_count + 1):
            dinic.add_edge(source, l, 1)

        # Left -> Right
        for u, v in self.edges:
            dinic.add_edge(u + 1, self.left_count + v + 1, 1)

        # Right -> Sink
        for r in range(1, self.right_count + 1):
            dinic.add_edge(self.left_count + r, sink, 1)

        max_matching = dinic.compute_max_flow(source, sink)

        # Extract matched pairs
        matches = []
        for l in range(1, self.left_count + 1):
            for edge in dinic.adj[l]:
                if edge.capacity > 0 and edge.flow == 1 and edge.to != source:
                    right_idx = edge.to - self.left_count - 1
                    matches.append((l - 1, right_idx))

        return max_matching, matches
```

---

> 🧭 **Navigation:** [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md)
