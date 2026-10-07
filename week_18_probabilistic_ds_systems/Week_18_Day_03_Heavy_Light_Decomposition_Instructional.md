# 📘 Week 18, Day 3: Heavy-Light Decomposition (HLD)

> 🧭 **Navigation:** [← Previous Day](Week_18_Day_02_Square_Root_Decomposition_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_18_Day_04_Probabilistic_Data_Structures_Instructional.md)
> 
> 💡 **Instructor Note:** *Heavy-Light Decomposition (HLD) linearizes tree topology into contiguous segments, turning tree path and subtree queries into standard Segment Tree range operations. Master the logarithmic light-edge bound.*

---

## 🎯 Learning Objectives

*   **Core Mental Model:** Understand how classifying tree edges into "Heavy" and "Light" guarantees that any root-to-node path intersects at most `log2(N)` disjoint chains.
*   **Mathematical Proof:** Prove that a light edge halves the remaining subtree size (`size(v) <= size(u) / 2`), ensuring a strict `O(log^2 N)` path query bound when backed by a Segment Tree.
*   **Production Invariant:** Relate HLD tree flattening to distributed hierarchical lock managers (ZooKeeper namespace subtree leases, Linux VFS dentry path validation).
*   **Algorithmic Protocol:** Implement a complete two-pass DFS decomposition and segment tree integration in modern C# (.NET 8/9) and Python (3.11+).

## ⚖️ FAANG Senior / Lead Interview Calibration

When handling queries on tree structures, senior engineers select the appropriate paradigm based on dynamicity and query semantics:

| Tree Processing Paradigm | Path Query | Path Update | Subtree Query | Tree Topology Dynamicity | Space Overhead | Interview Calibration |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **Euler Tour + Fenwick / SegTree** | `O(1)` (RMQ LCA) | `O(log N)` (Point) | `O(log N)` | Static | `O(N)` | Standard for LCA or subtree-only aggregates. |
| **Binary Lifting** | `O(log N)` (Commutative) | `O(N log N)` (Rebuild) | N/A | Static | `O(N log N)` | Standard for static ancestor/LCA queries. |
| **Heavy-Light Decomposition (HLD)** | `O(log^2 N)` | `O(log^2 N)` | `O(log N)` | Static Topology | `O(N)` | **Gold standard for combined dynamic path + subtree queries.** |
| **Link-Cut Tree (LCT)** | `O(log N)` | `O(log N)` | `O(log N)` | Dynamic (Link/Cut) | `O(N)` | Theoretical mastery; rarely coded live due to splay tree complexity. |

### The Recognition Pattern: "When is HLD Mandatory?"
1. **Dynamic Path Modification:** The problem requires updating edge or node weights along the simple path between `u` and `v` while simultaneously querying path aggregations (`max`, `sum`, `xor`).
2. **Dual Path and Subtree Queries:** Both path queries (`path(u, v)`) and subtree queries (`subtree(u)`) must be supported concurrently in sub-linear time.
3. **Static Topology with Dynamic Data:** The tree's shape (edges) remains fixed, but weights are updated continuously under high throughput.

---

## 📖 Chapter 1: Context & Motivation

### 1. The Engineering Challenge: Arbitrary Tree Path Queries

In tree data structures, standard problems include:
- Find the maximum edge/node weight on the simple path between node `u` and node `v`.
- Add a value `delta` to every node along the path from `u` to `v`.
- Query or update the sum of all nodes in the subtree rooted at `u`.

Naive graph search (BFS/DFS) takes `O(N)` per query. If a system handles `Q = 100,000` path queries on a tree with `N = 100,000` nodes, naive traversal requires `10^10` operations, resulting in catastrophic timeouts.

**Heavy-Light Decomposition (HLD)** maps the tree onto a single contiguous 1D array such that:
1. Every **heavy chain** occupies a contiguous interval `[pos[head], pos[tail]]`.
2. Every **subtree** occupies a single contiguous interval `[pos[u], pos[u] + size[u] - 1]`.

Path queries between `u` and `v` traverse at most `O(log N)` heavy chains, querying the underlying Segment Tree in `O(log N)` per chain, achieving a guaranteed `O(log^2 N)` query latency.

---

### 2. Physical Layout & Chain Decomposition

```
Original Tree Structure:
         (1) [size=9]
        // \
       //   \
      //     \
   (2)[size=6] (3)[size=2]
   // \           \
  //   \           \
(4)[s=4] (5)[s=1]  (6)[s=1]
 //
//
(7)[s=3]
//
(8)[s=1]

Legend:
  // : Heavy Edge (leads to child with maximum subtree size)
  \  : Light Edge (leads to any smaller child)

Linearized 1D Segment Tree Array:
+-------+-------+-------+-------+-------+-------+-------+-------+-------+
| Pos 0 | Pos 1 | Pos 2 | Pos 3 | Pos 4 | Pos 5 | Pos 6 | Pos 7 | Pos 8 |
+-------+-------+-------+-------+-------+-------+-------+-------+-------+
| Node 1| Node 2| Node 4| Node 7| Node 8| Node 5| Node 3| Node 6|  ...  |
+-------+-------+-------+-------+-------+-------+-------+-------+-------+
<----------- Heavy Chain 1 ------------><- HC 2 -><--- Heavy Chain 3 --->
```

---

## 🏛️ Chapter 2: Mathematical Formulation & Governing Invariants

### 1. Heavy vs. Light Edge Classification
For a node `u` with children `v_1, v_2, ..., v_k`:
- **Heavy Child:** The unique child `v*` maximizing `size(v)`. In case of a tie, choose arbitrarily.
- **Heavy Edge:** The directed edge `(u, v*)`.
- **Light Edge:** Any edge `(u, v_i)` where `v_i != v*`.

### 2. The Logarithmic Light-Edge Lemma
> **Theorem:** Any simple path from the root to any arbitrary leaf node crosses at most `floor(log2(N))` light edges.

**Proof by Subtree Shrinkage:**
1. Let `(u, v)` be a light edge.
2. By definition, `u` has a heavy child `v*` such that `size(v*) >= size(v)`.
3. The total size of `u` includes `u` itself, `v*`, `v`, and any other children:
   `size(u) = 1 + size(v*) + size(v) + sum(size(other)) >= 1 + 2 * size(v) > 2 * size(v)`
4. Therefore:
   `size(v) < size(u) / 2`
5. Every time we step down a light edge, the subtree size strictly shrinks by more than half.
6. Since the root starts with `size(root) = N` and the minimum subtree size is `1`, one can transition across a light edge at most `log2(N)` times before reaching size 1.

### 3. Path Traversal Invariant
To query the path between `u` and `v`:
- While `head[u] != head[v]`:
  - Identify which chain head is deeper in the tree (suppose `depth[head[u]] >= depth[head[v]]`).
  - Query the contiguous segment `[pos[head[u]], pos[u]]` in the Segment Tree.
  - Jump up to `parent[head[u]]`. (This step traverses a light edge!).
- When `head[u] == head[v]`, both nodes reside on the identical heavy chain.
  - Query the single contiguous segment `[min(pos[u], pos[v]), max(pos[u], pos[v])]`.

---

## 💻 Chapter 3: Idiomatic Dual-Language Implementations

### C# Primary Implementation (.NET 8/9)

```csharp
using System;
using System.Collections.Generic;

namespace AdvancedAlgorithms;

/// <summary>
/// Production-grade Heavy-Light Decomposition with embedded Segment Tree.
/// Supports path sum queries and path updates in O(log^2 N), and subtree queries in O(log N).
/// </summary>
public sealed class HeavyLightDecomposition
{
    private readonly int _n;
    private readonly List<int>[] _adj;
    private readonly int[] _parent;
    private readonly int[] _depth;
    private readonly int[] _heavy;
    private readonly int[] _head;
    private readonly int[] _pos;
    private readonly int[] _subtreeSize;
    private int _curPos;

    private readonly SegmentTree _segTree;

    public HeavyLightDecomposition(int n, List<int>[] adj, int root = 1)
    {
        _n = n;
        _adj = adj;
        _parent = new int[n + 1];
        _depth = new int[n + 1];
        _heavy = new int[n + 1];
        _head = new int[n + 1];
        _pos = new int[n + 1];
        _subtreeSize = new int[n + 1];

        Array.Fill(_heavy, -1);

        // Pass 1: Compute subtree sizes, depths, parents, and heavy children
        DfsSize(root, 0, 0);

        // Pass 2: Decompose into heavy chains and assign contiguous positions
        _curPos = 0;
        DfsHld(root, root);

        _segTree = new SegmentTree(n);
    }

    private int DfsSize(int u, int p, int d)
    {
        _parent[u] = p;
        _depth[u] = d;
        int size = 1;
        int maxChildSize = 0;

        foreach (int v in _adj[u])
        {
            if (v == p) continue;
            int childSize = DfsSize(v, u, d + 1);
            size += childSize;
            if (childSize > maxChildSize)
            {
                maxChildSize = childSize;
                _heavy[u] = v;
            }
        }

        _subtreeSize[u] = size;
        return size;
    }

    private void DfsHld(int u, int h)
    {
        _head[u] = h;
        _pos[u] = ++_curPos;

        // Traverse heavy child first so heavy chain elements get contiguous positions
        if (_heavy[u] != -1)
        {
            DfsHld(_heavy[u], h);
        }

        // Traverse remaining light children, each becoming the head of its own chain
        foreach (int v in _adj[u])
        {
            if (v != _parent[u] && v != _heavy[u])
            {
                DfsHld(v, v);
            }
        }
    }

    /// <summary>
    /// Queries the sum of node values along the simple path between u and v in O(log^2 N).
    /// </summary>
    public long QueryPath(int u, int v)
    {
        long total = 0;

        while (_head[u] != _head[v])
        {
            if (_depth[_head[u]] < _depth[_head[v]])
            {
                (u, v) = (v, u);
            }

            // [pos[head[u]], pos[u]] is a contiguous segment
            total += _segTree.Query(1, 1, _n, _pos[_head[u]], _pos[u]);
            u = _parent[_head[u]];
        }

        if (_depth[u] > _depth[v])
        {
            (u, v) = (v, u);
        }

        total += _segTree.Query(1, 1, _n, _pos[u], _pos[v]);
        return total;
    }

    /// <summary>
    /// Queries the sum of all nodes in the subtree rooted at u in O(log N).
    /// </summary>
    public long QuerySubtree(int u)
    {
        int left = _pos[u];
        int right = _pos[u] + _subtreeSize[u] - 1;
        return _segTree.Query(1, 1, _n, left, right);
    }

    public void UpdateNode(int u, long val)
    {
        _segTree.Update(1, 1, _n, _pos[u], val);
    }

    // =========================================================================
    // Underlying 1-Indexed Contiguous Segment Tree
    // =========================================================================
    private sealed class SegmentTree
    {
        private readonly long[] _tree;
        public SegmentTree(int size) => _tree = new long[4 * size];

        public void Update(int node, int l, int r, int idx, long val)
        {
            if (l == r) { _tree[node] = val; return; }
            int mid = (l + r) / 2;
            if (idx <= mid) Update(2 * node, l, mid, idx, val);
            else Update(2 * node + 1, mid + 1, r, idx, val);
            _tree[node] = _tree[2 * node] + _tree[2 * node + 1];
        }

        public long Query(int node, int l, int r, int ql, int qr)
        {
            if (ql <= l && r <= qr) return _tree[node];
            if (r < ql || l > qr) return 0;
            int mid = (l + r) / 2;
            return Query(2 * node, l, mid, ql, qr) + Query(2 * node + 1, mid + 1, r, ql, qr);
        }
    }
}
```

---

### Python Secondary Implementation (Python 3.11+)

```python
from typing import List

class SegmentTree:
    """1-indexed range sum Segment Tree."""
    def __init__(self, size: int):
        self.size = size
        self.tree = [0] * (4 * size)

    def update(self, node: int, l: int, r: int, idx: int, val: int) -> None:
        if l == r:
            self.tree[node] = val
            return
        mid = (l + r) // 2
        if idx <= mid:
            self.update(2 * node, l, mid, idx, val)
        else:
            self.update(2 * node + 1, mid + 1, r, idx, val)
        self.tree[node] = self.tree[2 * node] + self.tree[2 * node + 1]

    def query(self, node: int, l: int, r: int, ql: int, qr: int) -> int:
        if ql <= l and r <= qr:
            return self.tree[node]
        if r < ql or l > qr:
            return 0
        mid = (l + r) // 2
        return (
            self.query(2 * node, l, mid, ql, qr)
            + self.query(2 * node + 1, mid + 1, r, ql, qr)
        )


class HeavyLightDecomposition:
    """Production Heavy-Light Decomposition for trees."""
    def __init__(self, n: int, adj: List[List[int]], root: int = 1):
        self.n = n
        self.adj = adj
        self.parent = [0] * (n + 1)
        self.depth = [0] * (n + 1)
        self.heavy = [-1] * (n + 1)
        self.head = [0] * (n + 1)
        self.pos = [0] * (n + 1)
        self.subtree_size = [0] * (n + 1)
        self.cur_pos = 0

        # Pass 1: Tree characteristics
        self._dfs_size(root, 0, 0)

        # Pass 2: Heavy-chain decomposition
        self._dfs_hld(root, root)

        self.seg_tree = SegmentTree(n)

    def _dfs_size(self, u: int, p: int, d: int) -> int:
        self.parent[u] = p
        self.depth[u] = d
        size = 1
        max_c_size = 0

        for v in self.adj[u]:
            if v == p:
                continue
            c_size = self._dfs_size(v, u, d + 1)
            size += c_size
            if c_size > max_c_size:
                max_c_size = c_size
                self.heavy[u] = v

        self.subtree_size[u] = size
        return size

    def _dfs_hld(self, u: int, h: int) -> None:
        self.head[u] = h
        self.cur_pos += 1
        self.pos[u] = self.cur_pos

        # Heavy child prioritized to ensure contiguous indices
        if self.heavy[u] != -1:
            self._dfs_hld(self.heavy[u], h)

        for v in self.adj[u]:
            if v != self.parent[u] and v != self.heavy[u]:
                self._dfs_hld(v, v)

    def query_path(self, u: int, v: int) -> int:
        total = 0
        while self.head[u] != self.head[v]:
            if self.depth[self.head[u]] < self.depth[self.head[v]]:
                u, v = v, u

            total += self.seg_tree.query(
                1, 1, self.n, self.pos[self.head[u]], self.pos[u]
            )
            u = self.parent[self.head[u]]

        if self.depth[u] > self.depth[v]:
            u, v = v, u

        total += self.seg_tree.query(1, 1, self.n, self.pos[u], self.pos[v])
        return total

    def query_subtree(self, u: int) -> int:
        left = self.pos[u]
        right = self.pos[u] + self.subtree_size[u] - 1
        return self.seg_tree.query(1, 1, self.n, left, right)

    def update_node(self, u: int, val: int) -> None:
        self.seg_tree.update(1, 1, self.n, self.pos[u], val)
```

---

## 🔬 Chapter 4: Explicit Complexity Deconstruction

| Operation | Time Bound | Auxiliary Memory | Architectural Reason |
| :--- | :--- | :--- | :--- |
| **Preprocessing (DFS 1 & 2)** | `O(N)` | `O(N)` recursion stack | Visits each edge and node exactly twice. |
| **Segment Tree Build** | `O(N)` | `4 * N` array slots | Pre-allocated flat array representing complete binary tree. |
| **Path Query / Update** | `O(log^2 N)` | `O(1)` heap | Crosses `<= log2(N)` heavy chains; each segment tree query is `O(log N)`. |
| **Subtree Query / Update**| `O(log N)` | `O(1)` heap | Subtree forms a single contiguous range `[pos[u], pos[u] + size[u] - 1]`. |
| **Lowest Common Ancestor**| `O(log N)` | `O(1)` heap | Implicitly computed while jumping chain heads toward equal heads. |

---

## 🎙️ Chapter 5: 45-Minute Verbal Script & Interview Playbook

```
[00:00 - 05:00] Clarification & Invariant Formulation
"We are tasked with performing high-frequency path updates and path queries on a general tree of N = 100,000 nodes.
Standard DFS per query would degrade to O(Q * N) = 10^10 operations, which will TLE.
Binary lifting solves LCA in O(log N) but does not readily support range updates on arbitrary path intervals.
I will use Heavy-Light Decomposition to partition the tree into heavy chains mapped to a Segment Tree."

[05:00 - 15:00] Mathematical Justification
"I define the heavy child of node u as the child with the largest subtree size.
Edges to heavy children form heavy chains; all other edges are light edges.
Key Lemma: When transitioning across a light edge from u to child v, size(v) < size(u) / 2.
Therefore, any path from the root to any node crosses at most log2(N) light edges.
Because heavy chains are contiguous in our DFS linearization, a path between any u and v
decomposes into at most 2 * log2(N) contiguous intervals.
Querying each interval on a Segment Tree takes O(log N), giving an overall query time of O(log^2 N)."

[15:00 - 32:00] Implementation Protocol
- Implement DfsSize to collect subtree sizes, depths, and heavy children.
- Implement DfsHld prioritizing the heavy child first, assigning pos[u] sequentially.
- Implement QueryPath: repeatedly jump the head of the deeper node's heavy chain, query the segment tree,
  and step to parent[head[u]] until both nodes share the same heavy chain.

[32:00 - 40:00] Verification & Subtree Property
"Notice that any subtree rooted at u also occupies a strictly contiguous range:
[pos[u], pos[u] + subtreeSize[u] - 1]. Thus subtree queries take just O(log N)!"

[40:00 - 45:00] Real-World Distributed Systems Anchor
"In distributed hierarchical namespaces like ZooKeeper or Linux file system dentry caches,
path-based permissions and hierarchical lock acquisitions require verifying ancestor chains.
Linearizing trees into interval ranges enables constant-time subtree invalidation."
```

---

## 🔍 Chapter 6: Edge-Case Verification

| Test Case | Scenario | Expected Outcome | Invariant Handling |
| :--- | :--- | :--- | :--- |
| **Single Node Tree** | `N = 1, adj = []` | Path `(1, 1)` queries node 1 | `head[1] == 1`, loops terminate immediately, queries `pos[1]`. |
| **Linear Chain (Skewed Tree)** | Tree is a linked list `1-2-3-4` | Solved in `O(log N)` | All edges are heavy edges; the entire tree is a single heavy chain! |
| **Star Graph** | Root 1 connected to leaves `2, ..., N` | Solved in `O(log N)` | One child is chosen heavy; all others are light chains of length 1. |
| **Node is Ancestor of Other** | `u` is direct ancestor of `v` | Path `(u, v)` queries single upward sweep | `head` jumps until `u` and `v` align; no overshooting occurs. |
| **Query Identical Node** | `QueryPath(u, u)` | Evaluates single index `pos[u]` | Handled directly by `pos[u]` to `pos[u]` interval. |

---

> 🧭 **Navigation:** [← Previous Day](Week_18_Day_02_Square_Root_Decomposition_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_18_Day_04_Probabilistic_Data_Structures_Instructional.md)
