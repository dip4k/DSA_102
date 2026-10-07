# 💻 Week 15 Extended C# Complete Reference (.NET 8/9)

> 🧭 **Navigation:** [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md)
> 
> 💡 **Instructor Note:** *Focus on memory-efficient, modern C# idiom: ReadOnlySpan<char>, zero-allocation memory slices, and clean algorithmic architectures.*

---

## 🧱 Production-Grade C# Implementation Blueprint

This reference provides complete, battle-tested implementations of the core Week 15 patterns:
1. **Z-Algorithm:** Linear-time exact string pattern matching.
2. **Fenwick Tree (BIT):** Logarithmic prefix queries and point updates.
3. **Segment Tree:** Logarithmic range sum queries and point updates.
4. **Edmonds-Karp Max Flow:** Augmenting path BFS on residual graphs.
5. **Dinic's Algorithm:** Level-graph BFS and blocking-flow DFS with dead-end elimination.
6. **Maximum Bipartite Matching:** Optimal network flow reduction with matched-pair extraction.

---

### Pattern 1: Z-Algorithm (Linear Exact String Matching)

```csharp
using System;
using System.Collections.Generic;

namespace Week_15.Reference;

public static class ZAlgorithm
{
    /// <summary>
    /// Computes the Z-array for string s in O(N) time.
    /// Z[i] is the length of the longest substring starting at s[i] that matches prefix s[0...].
    /// </summary>
    public static int[] BuildZArray(ReadOnlySpan<char> s)
    {
        int n = s.Length;
        int[] z = new int[n];
        if (n == 0) return z;

        int left = 0, right = 0;

        for (int i = 1; i < n; i++)
        {
            if (i <= right)
            {
                z[i] = Math.Min(right - i + 1, z[i - left]);
            }

            while (i + z[i] < n && s[z[i]] == s[i + z[i]])
            {
                z[i]++;
            }

            if (i + z[i] - 1 > right)
            {
                left = i;
                right = i + z[i] - 1;
            }
        }

        return z;
    }

    /// <summary>
    /// Finds all 0-indexed occurrences of pattern in text using Z-Algorithm.
    /// Time Complexity: O(N + M) | Auxiliary Space: O(N + M)
    /// </summary>
    public static List<int> Search(string text, string pattern)
    {
        var matches = new List<int>();
        if (string.IsNullOrEmpty(pattern) || string.IsNullOrEmpty(text)) return matches;
        if (pattern.Length > text.Length) return matches;

        string combined = $"{pattern}${text}";
        int[] z = BuildZArray(combined.AsSpan());
        int m = pattern.Length;

        for (int i = m + 1; i < combined.Length; i++)
        {
            if (z[i] == m)
            {
                matches.Add(i - m - 1);
            }
        }

        return matches;
    }
}
```

---

### Pattern 2: Fenwick Tree (Binary Indexed Tree)

```csharp
public class FenwickTree
{
    private readonly long[] _tree;
    public int Size { get; }

    /// <summary>
    /// Initializes a 1-indexed Binary Indexed Tree for n elements.
    /// </summary>
    public FenwickTree(int n)
    {
        Size = n;
        _tree = new long[n + 1];
    }

    /// <summary>
    /// Adds delta to element at 1-based index i in O(log N) time.
    /// </summary>
    public void Add(int index, long delta)
    {
        for (int i = index; i <= Size; i += i & -i)
        {
            _tree[i] += delta;
        }
    }

    /// <summary>
    /// Computes the prefix sum from index 1 to index in O(log N) time.
    /// </summary>
    public long PrefixSum(int index)
    {
        long sum = 0;
        for (int i = index; i > 0; i -= i & -i)
        {
            sum += _tree[i];
        }
        return sum;
    }

    /// <summary>
    /// Computes the range sum [left, right] (1-based inclusive) in O(log N) time.
    /// </summary>
    public long QueryRange(int left, int right)
    {
        if (left > right) return 0;
        return PrefixSum(right) - PrefixSum(left - 1);
    }
}
```

---

### Pattern 3: Segment Tree (Range Sum Queries)

```csharp
public class SegmentTree
{
    private readonly long[] _tree;
    private readonly int _n;

    public SegmentTree(int[] array)
    {
        _n = array.Length;
        _tree = new long[4 * _n];
        if (_n > 0)
        {
            Build(array, 1, 0, _n - 1);
        }
    }

    private void Build(int[] array, int node, int start, int end)
    {
        if (start == end)
        {
            _tree[node] = array[start];
            return;
        }

        int mid = start + (end - start) / 2;
        Build(array, 2 * node, start, mid);
        Build(array, 2 * node + 1, mid + 1, end);
        _tree[node] = _tree[2 * node] + _tree[2 * node + 1];
    }

    /// <summary>
    /// Updates the element at array index to newValue in O(log N) time.
    /// </summary>
    public void Update(int index, int newValue) => Update(1, 0, _n - 1, index, newValue);

    private void Update(int node, int start, int end, int index, int newValue)
    {
        if (start == end)
        {
            _tree[node] = newValue;
            return;
        }

        int mid = start + (end - start) / 2;
        if (index <= mid)
        {
            Update(2 * node, start, mid, index, newValue);
        }
        else
        {
            Update(2 * node + 1, mid + 1, end, index, newValue);
        }

        _tree[node] = _tree[2 * node] + _tree[2 * node + 1];
    }

    /// <summary>
    /// Queries range sum [left, right] inclusive in O(log N) time.
    /// </summary>
    public long Query(int left, int right) => Query(1, 0, _n - 1, left, right);

    private long Query(int node, int start, int end, int left, int right)
    {
        if (right < start || end < left) return 0;
        if (left <= start && end <= right) return _tree[node];

        int mid = start + (end - start) / 2;
        long p1 = Query(2 * node, start, mid, left, right);
        long p2 = Query(2 * node + 1, mid + 1, end, left, right);
        return p1 + p2;
    }
}
```

---

### Pattern 4: Edmonds-Karp Max Flow Algorithm

```csharp
public class EdmondsKarp
{
    private readonly int[,] _capacity;
    private readonly int _nodes;

    public EdmondsKarp(int nodes)
    {
        _nodes = nodes;
        _capacity = new int[nodes, nodes];
    }

    public void AddEdge(int u, int v, int cap)
    {
        _capacity[u, v] += cap; // Supports multiple directed edges
    }

    /// <summary>
    /// Computes the Maximum Flow from source s to sink t in O(V * E^2) time.
    /// </summary>
    public int ComputeMaxFlow(int source, int sink)
    {
        int maxFlow = 0;
        int[] parent = new int[_nodes];

        while (BfsPath(source, sink, parent))
        {
            int pathFlow = int.MaxValue;
            for (int v = sink; v != source; v = parent[v])
            {
                int u = parent[v];
                pathFlow = Math.Min(pathFlow, _capacity[u, v]);
            }

            for (int v = sink; v != source; v = parent[v])
            {
                int u = parent[v];
                _capacity[u, v] -= pathFlow;
                _capacity[v, u] += pathFlow;
            }

            maxFlow += pathFlow;
        }

        return maxFlow;
    }

    private bool BfsPath(int source, int sink, int[] parent)
    {
        Array.Fill(parent, -1);
        parent[source] = source;

        var queue = new Queue<int>();
        queue.Enqueue(source);

        while (queue.Count > 0)
        {
            int u = queue.Dequeue();

            for (int v = 0; v < _nodes; v++)
            {
                if (parent[v] == -1 && _capacity[u, v] > 0)
                {
                    parent[v] = u;
                    if (v == sink) return true;
                    queue.Enqueue(v);
                }
            }
        }

        return false;
    }
}
```

---

### Pattern 5: Dinic's Maximum Flow Algorithm

```csharp
public class Dinic
{
    public class Edge
    {
        public int To { get; init; }
        public int Capacity { get; set; }
        public int Flow { get; set; }
        public int RevIndex { get; init; }

        public Edge(int to, int capacity, int revIndex)
        {
            To = to;
            Capacity = capacity;
            RevIndex = revIndex;
            Flow = 0;
        }
    }

    private readonly int _nodes;
    private readonly List<Edge>[] _adj;
    private readonly int[] _level;
    private readonly int[] _ptr;

    public Dinic(int nodes)
    {
        _nodes = nodes;
        _adj = new List<Edge>[nodes];
        for (int i = 0; i < nodes; i++) _adj[i] = new List<Edge>();
        _level = new int[nodes];
        _ptr = new int[nodes];
    }

    public void AddEdge(int from, int to, int capacity)
    {
        var forward = new Edge(to, capacity, _adj[to].Count);
        var backward = new Edge(from, 0, _adj[from].Count);
        _adj[from].Add(forward);
        _adj[to].Add(backward);
    }

    /// <summary>
    /// Computes Maximum Flow in O(V^2 * E) general, O(E * sqrt(V)) unit/bipartite networks.
    /// </summary>
    public int ComputeMaxFlow(int source, int sink)
    {
        int totalFlow = 0;

        while (BfsLevelGraph(source, sink))
        {
            Array.Fill(_ptr, 0); // Reset work pointers

            while (true)
            {
                int pushed = DfsBlockingFlow(source, sink, int.MaxValue);
                if (pushed == 0) break;
                totalFlow += pushed;
            }
        }

        return totalFlow;
    }

    public List<(int From, int To)> GetMatchedEdges(HashSet<int> leftSet)
    {
        var matches = new List<(int From, int To)>();
        foreach (int u in leftSet)
        {
            foreach (var edge in _adj[u])
            {
                if (edge.Capacity > 0 && edge.Flow == 1)
                {
                    matches.Add((u, edge.To));
                }
            }
        }
        return matches;
    }

    private bool BfsLevelGraph(int source, int sink)
    {
        Array.Fill(_level, -1);
        _level[source] = 0;

        var queue = new Queue<int>();
        queue.Enqueue(source);

        while (queue.Count > 0)
        {
            int u = queue.Dequeue();

            foreach (var edge in _adj[u])
            {
                if (edge.Capacity - edge.Flow > 0 && _level[edge.To] == -1)
                {
                    _level[edge.To] = _level[u] + 1;
                    queue.Enqueue(edge.To);
                }
            }
        }

        return _level[sink] != -1;
    }

    private int DfsBlockingFlow(int u, int sink, int flowIn)
    {
        if (u == sink || flowIn == 0) return flowIn;

        for (; _ptr[u] < _adj[u].Count; _ptr[u]++)
        {
            var edge = _adj[u][_ptr[u]];

            if (_level[edge.To] == _level[u] + 1 && edge.Capacity - edge.Flow > 0)
            {
                int bottleNeck = Math.Min(flowIn, edge.Capacity - edge.Flow);
                int pushed = DfsBlockingFlow(edge.To, sink, bottleNeck);

                if (pushed > 0)
                {
                    edge.Flow += pushed;
                    _adj[edge.To][edge.RevIndex].Flow -= pushed;
                    return pushed;
                }
            }
        }

        return 0;
    }
}
```

---

### Pattern 6: Maximum Bipartite Matching

```csharp
public class MaximumBipartiteMatching
{
    private readonly int _leftCount;
    private readonly int _rightCount;
    private readonly List<(int Left, int Right)> _edges = new();

    public MaximumBipartiteMatching(int leftCount, int rightCount)
    {
        _leftCount = leftCount;
        _rightCount = rightCount;
    }

    public void AddEdge(int leftNode, int rightNode)
    {
        _edges.Add((leftNode, rightNode));
    }

    /// <summary>
    /// Solves Maximum Bipartite Matching via Dinic reduction in O(E * sqrt(V)) time.
    /// </summary>
    public (int MaxMatching, List<(int Left, int Right)> Matches) Solve()
    {
        int source = 0;
        int sink = _leftCount + _rightCount + 1;
        var dinic = new Dinic(sink + 1);
        var leftSet = new HashSet<int>();

        // 1. Source -> Left nodes
        for (int l = 1; l <= _leftCount; l++)
        {
            dinic.AddEdge(source, l, 1);
            leftSet.Add(l);
        }

        // 2. Left nodes -> Right nodes
        foreach (var (l, r) in _edges)
        {
            dinic.AddEdge(l + 1, _leftCount + r + 1, 1);
        }

        // 3. Right nodes -> Sink
        for (int r = 1; r <= _rightCount; r++)
        {
            dinic.AddEdge(_leftCount + r, sink, 1);
        }

        int maxMatching = dinic.ComputeMaxFlow(source, sink);
        var rawMatches = dinic.GetMatchedEdges(leftSet);
        var formattedMatches = new List<(int Left, int Right)>();

        foreach (var (u, v) in rawMatches)
        {
            if (u != source && v != sink)
            {
                formattedMatches.Add((u - 1, v - _leftCount - 1));
            }
        }

        return (maxMatching, formattedMatches);
    }
}
```

---

> 🧭 **Navigation:** [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md)
