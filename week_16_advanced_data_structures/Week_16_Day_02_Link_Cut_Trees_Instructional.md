# 📘 Week 16, Day 2: Link-Cut Trees for Dynamic Forests

> 🧭 **Navigation:** [← Previous Day](Week_16_Day_01_Skip_Lists_And_Treaps_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_16_Day_03_Persistent_Data_Structures_Instructional.md)
> 
> 💡 **Instructor Note:** *Link-Cut Trees (LCT) are the pinnacle of dynamic tree data structures. In a realistic 45-minute FAANG Senior/Staff interview, you will almost never be asked to code a 200-line LCT from scratch. Interviewers test whether you can recognize when simpler structures (DSU, HLD) fail, explain the preferred child / Splay tree duality, and implement key primitives like `Access` and `MakeRoot`.*

---

## 🎯 Learning Objectives

*   **Dynamic Forest Mental Model:** Master the representation of dynamic, mutating forests supporting edge insertion (`Link`), edge deletion (`Cut`), and path aggregates.
*   **The Structural Triad:** Compare DSU vs Heavy-Light Decomposition (HLD) vs Link-Cut Trees (LCT) across dynamic mutations and path query capabilities.
*   **Preferred Child Decomposition:** Understand how dynamic preferred paths partition the represented tree into auxiliary Splay trees keyed by node depth.
*   **Core Mechanics:** Trace the exact pointer mutations in `Access`, `Splay`, and lazy depth-reversal in `MakeRoot`.
*   **Zero-Allocation Implementation:** Build a clean, flat-array Link-Cut Tree in modern C# (.NET 8/9) and idiomatic Python (3.11+).

---

## ⚖️ FAANG Senior / Lead Interview Calibration

When dynamic graph connectivity or path queries arise in senior technical rounds, candidates must navigate distinct architectural trade-offs:

| Data Structure | Dynamic Edge Add (`Link`) | Dynamic Edge Remove (`Cut`) | Path Queries (XOR / Sum / Min) | Time Complexity | Interview Coding Expectation |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **DSU (Disjoint Set Union)** | ✅ Yes (`Union`) | ❌ No (Only rollback via stack) | ❌ No (Cannot query paths) | `O(alpha(N))` | Full implementation expected in 10 mins. |
| **Heavy-Light Decomp (HLD)** | ❌ No (`O(N)` rebuild) | ❌ No (`O(N)` rebuild) | ✅ Yes (`O(log^2 N)` or `O(log N)`) | `O(log^2 N)` query | Full implementation expected for static trees. |
| **Link-Cut Tree (LCT)** | ✅ Yes (`Link`) | ✅ Yes (`Cut`) | ✅ Yes (`QueryPath`) | Amortized `O(log N)` | **Conceptual mastery required**; code isolated primitives (`Access`, `Splay`, `Rotate`). |

### What Top Interviewers Look For
1. **Recognizing the DSU Boundary:** If an interviewer asks: *"Maintain a dynamic network where servers connect and disconnect, and answer whether two nodes are connected,"* a naive candidate suggests DSU. A Senior candidate immediately notes: *"DSU cannot handle edge deletions without rebuilding or rollback. If edges can be removed arbitrarily in an online fashion, we must use Link-Cut Trees (or Euler Tour Trees for component-level aggregates)."*
2. **Recognizing the HLD Boundary:** If path queries are required on a tree whose edges mutate dynamically, HLD collapses because heavy/light edge classifications depend on static subtree sizes. LCT replaces static heavy edges with dynamic preferred edges.
3. **The `Access(x)` Mental Anchor:** Every operation in LCT is a syntactic wrapper around `Access(x)`. If you can explain `Access(x)`, you have solved 90% of the interview question.

---

## 📖 Chapter 1: Context & Physical Mental Models

### 1. Represented Tree vs Auxiliary Splay Trees

An LCT represents a forest of arbitrary rooted trees. It decomposes each represented tree into a collection of vertex-disjoint **Preferred Paths**:
*   A node can have many children in the represented tree, but at most **one** child is designated as its **preferred child**.
*   The edge to the preferred child is a **preferred edge**. All other edges to children are **non-preferred (dashed) edges**.
*   Each preferred path is stored in an **Auxiliary Splay Tree**, where the BST search key is the **depth** of the node in the represented tree.

```text
[Represented Tree]                    [Decomposition into Splay Trees]

        (A) [Depth 0]                     Aux Splay 1 (Path A - B - D):
        / \                                       (B) [Depth 1]
       /   \                                     /   \
     (B)   (C) [Depth 1]               [Depth 0] (A)   (D) [Depth 2]
     / \     \                                         
    /   \     \                                        
  (D)   (E)   (F)                      Aux Splay 2:      Aux Splay 3:
[D:2]  [D:2]  [D:2]                       (E)               (C)
                                                             \
Bold edges (A-B, B-D) = Preferred                             (F)
Dashed edges (A-C, B-E, C-F) = Non-preferred
```

### 2. Path-Parent Pointers (The Asymmetric Link)

In the auxiliary Splay tree:
*   The root of Aux Splay 2 (node `E`) has a pointer `parent[E] = B`.
*   However, `B`'s left child is `A` and `B`'s right child is `D`. `B` does **not** point back to `E` as a child!
*   This asymmetry is the defining invariant of LCT: **A node is a root of an auxiliary Splay tree if and only if its parent in memory does not recognize it as a left or right child.**

```text
IsRoot(x) := (parent[x].left != x && parent[x].right != x)
```

---

## 🏛️ Chapter 2: The Governing Invariants & Algorithmic Mechanics

### 1. The Core Operations

#### `Access(x)`: Making the Path to Root Preferred
`Access(x)` restructures the preferred paths so that the path from the root of the represented tree down to node `x` becomes a single preferred path, and `x` becomes the deepest node on that path.
1. Splay `x` to the root of its current auxiliary tree.
2. Disconnect `x`'s right child (severing any deeper nodes previously on `x`'s preferred path).
3. Connect `x`'s right child to the previously processed auxiliary tree root.
4. Walk up the path-parent pointer to `x`'s parent and repeat until the represented tree root is reached.
5. Splay `x` once more. Now `x` is the root of the auxiliary tree, has no right child, and its left subtree contains all ancestors of `x`.

#### `MakeRoot(x)`: Changing the Represented Root
To make `x` the root of the entire represented tree:
1. Call `Access(x)` (puts the path from represented root to `x` in one Splay tree).
2. Call `Splay(x)` (`x` is at the root; all ancestors are in its left subtree).
3. Invert depth ordering: Set `rev[x] ^= true` (lazy child swap). Now `x` has depth 0, and what were formerly ancestors now have deeper depths!

#### `Link(x, y)`: Adding an Edge
Connects component of `x` to component of `y` by adding directed edge `x -> y`:
1. `MakeRoot(x)`
2. `parent[x] = y`

#### `Cut(x, y)`: Removing an Edge
Removes edge between `x` and `y`:
1. `MakeRoot(x)`
2. `Access(y)` followed by `Splay(y)`.
3. If `x` and `y` are adjacent, `x` must be the left child of `y`, and `x` has no right child.
4. Set `left[y] = 0` and `parent[x] = 0`.

---

## 💻 Chapter 3: Idiomatic Dual-Language Code

### C# Primary Implementation (.NET 8/9)

```csharp
using System;

namespace AdvancedDataStructures;

/// <summary>
/// Production Link-Cut Tree with flat array memory layout.
/// Supports Link, Cut, Dynamic Connectivity, and Path XOR Aggregation in amortized O(log N).
/// </summary>
public sealed class LinkCutTree
{
    private readonly int[] _parent;
    private readonly int[] _left;
    private readonly int[] _right;
    private readonly int[] _val;
    private readonly int[] _subXor;
    private readonly bool[] _rev;

    public LinkCutTree(int n)
    {
        int size = n + 1;
        _parent = new int[size];
        _left = new int[size];
        _right = new int[size];
        _val = new int[size];
        _subXor = new int[size];
        _rev = new bool[size];

        for (int i = 1; i <= n; i++)
        {
            _val[i] = i;
            _subXor[i] = i;
        }
    }

    private bool IsRoot(int x) =>
        _left[_parent[x]] != x && _right[_parent[x]] != x;

    private void Push(int x)
    {
        if (!_rev[x]) return;

        (_left[x], _right[x]) = (_right[x], _left[x]);
        if (_left[x] != 0) _rev[_left[x]] ^= true;
        if (_right[x] != 0) _rev[_right[x]] ^= true;
        _rev[x] = false;
    }

    private void Update(int x)
    {
        _subXor[x] = _val[x] ^ _subXor[_left[x]] ^ _subXor[_right[x]];
    }

    private void Rotate(int x)
    {
        int y = _parent[x];
        int z = _parent[y];
        bool isRight = _right[y] == x;

        if (!IsRoot(y))
        {
            if (_left[z] == y) _left[z] = x;
            else _right[z] = x;
        }
        _parent[x] = z;

        if (isRight)
        {
            _right[y] = _left[x];
            if (_left[x] != 0) _parent[_left[x]] = y;
            _left[x] = y;
        }
        else
        {
            _left[y] = _right[x];
            if (_right[x] != 0) _parent[_right[x]] = y;
            _right[x] = y;
        }

        _parent[y] = x;
        Update(y);
        Update(x);
    }

    private void Splay(int x)
    {
        // Push lazy tags from root down to x
        void PushPath(int u)
        {
            if (!IsRoot(u)) PushPath(_parent[u]);
            Push(u);
        }
        PushPath(x);

        while (!IsRoot(x))
        {
            int y = _parent[x];
            int z = _parent[y];

            if (!IsRoot(y))
            {
                if ((_left[y] == x) ^ (_left[z] == y))
                    Rotate(x);
                else
                    Rotate(y);
            }
            Rotate(x);
        }
    }

    public void Access(int x)
    {
        for (int t = 0; x != 0; t = x, x = _parent[x])
        {
            Splay(x);
            _right[x] = t;
            Update(x);
        }
    }

    public void MakeRoot(int x)
    {
        Access(x);
        Splay(x);
        _rev[x] ^= true;
    }

    public int FindRoot(int x)
    {
        Access(x);
        Splay(x);
        Push(x);
        while (_left[x] != 0)
        {
            x = _left[x];
            Push(x);
        }
        Splay(x);
        return x;
    }

    public bool IsConnected(int x, int y) => FindRoot(x) == FindRoot(y);

    public void Link(int x, int y)
    {
        if (IsConnected(x, y)) return;
        MakeRoot(x);
        _parent[x] = y;
    }

    public void Cut(int x, int y)
    {
        MakeRoot(x);
        Access(y);
        Splay(y);

        // x must be the immediate left child of y
        if (_left[y] == x && _right[x] == 0)
        {
            _left[y] = 0;
            _parent[x] = 0;
            Update(y);
        }
    }

    public int QueryPath(int u, int v)
    {
        MakeRoot(u);
        Access(v);
        Splay(v);
        return _subXor[v];
    }
}
```

### Python Secondary Implementation (Python 3.11+)

```python
from typing import List

class LinkCutTree:
    """Link-Cut Tree supporting dynamic forest mutations and path queries."""

    def __init__(self, n: int):
        self.parent: List[int] = [0] * (n + 1)
        self.left: List[int] = [0] * (n + 1)
        self.right: List[int] = [0] * (n + 1)
        self.val: List[int] = list(range(n + 1))
        self.sub_xor: List[int] = list(range(n + 1))
        self.rev: List[bool] = [False] * (n + 1)

    def _is_root(self, x: int) -> bool:
        p = self.parent[x]
        return self.left[p] != x and self.right[p] != x

    def _push(self, x: int) -> None:
        if self.rev[x]:
            self.left[x], self.right[x] = self.right[x], self.left[x]
            if self.left[x]:
                self.rev[self.left[x]] ^= True
            if self.right[x]:
                self.rev[self.right[x]] ^= True
            self.rev[x] = False

    def _update(self, x: int) -> None:
        self.sub_xor[x] = self.val[x] ^ self.sub_xor[self.left[x]] ^ self.sub_xor[self.right[x]]

    def _rotate(self, x: int) -> None:
        y = self.parent[x]
        z = self.parent[y]
        is_right = (self.right[y] == x)

        if not self._is_root(y):
            if self.left[z] == y:
                self.left[z] = x
            else:
                self.right[z] = x
        self.parent[x] = z

        if is_right:
            self.right[y] = self.left[x]
            if self.left[x]:
                self.parent[self.left[x]] = y
            self.left[x] = y
        else:
            self.left[y] = self.right[x]
            if self.right[x]:
                self.parent[self.right[x]] = y
            self.right[x] = y

        self.parent[y] = x
        self._update(y)
        self._update(x)

    def _splay(self, x: int) -> None:
        stack = []
        curr = x
        while not self._is_root(curr):
            stack.append(curr)
            curr = self.parent[curr]
        stack.append(curr)
        while stack:
            self._push(stack.pop())

        while not self._is_root(x):
            y = self.parent[x]
            z = self.parent[y]
            if not self._is_root(y):
                if (self.left[y] == x) ^ (self.left[z] == y):
                    self._rotate(x)
                else:
                    self._rotate(y)
            self._rotate(x)

    def access(self, x: int) -> None:
        t = 0
        while x:
            self._splay(x)
            self.right[x] = t
            self._update(x)
            t = x
            x = self.parent[x]

    def make_root(self, x: int) -> None:
        self.access(x)
        self._splay(x)
        self.rev[x] ^= True

    def find_root(self, x: int) -> int:
        self.access(x)
        self._splay(x)
        self._push(x)
        while self.left[x]:
            x = self.left[x]
            self._push(x)
        self._splay(x)
        return x

    def is_connected(self, x: int, y: int) -> bool:
        return self.find_root(x) == self.find_root(y)

    def link(self, x: int, y: int) -> None:
        if self.is_connected(x, y):
            return
        self.make_root(x)
        self.parent[x] = y

    def cut(self, x: int, y: int) -> None:
        self.make_root(x)
        self.access(y)
        self._splay(y)
        if self.left[y] == x and self.right[x] == 0:
            self.left[y] = 0
            self.parent[x] = 0
            self._update(y)

    def query_path(self, u: int, v: int) -> int:
        self.make_root(u)
        self.access(v)
        self._splay(v)
        return self.sub_xor[v]
```

---

## 📊 Chapter 4: Explicit Complexity Deconstruction

| Operation | Amortized Time | Worst-Case Single Op | Auxiliary Space | Invariant Mechanism |
| :--- | :--- | :--- | :--- | :--- |
| **`Access(x)`** | `O(log N)` | `O(N)` | `O(log N)` stack | Switches preferred edges; bounded by Sleator-Tarjan potential function. |
| **`Splay(x)`** | `O(log N)` | `O(N)` | `O(log N)` stack | Double-rotation zig-zig / zig-zag cuts path length in half. |
| **`MakeRoot(x)`** | `O(log N)` | `O(N)` | `O(log N)` stack | Calls `Access(x)` + `Splay(x)` + flips `rev` flag in `O(1)`. |
| **`Link(x, y)`** | `O(log N)` | `O(N)` | `O(log N)` stack | `MakeRoot(x)` + single pointer assignment `parent[x] = y`. |
| **`Cut(x, y)`** | `O(log N)` | `O(N)` | `O(log N)` stack | `MakeRoot(x)` + `Access(y)` + unlinks `left[y]`. |
| **`QueryPath(u, v)`**| `O(log N)` | `O(N)` | `O(log N)` stack | Aggregates sub-XOR stored at the root of the splay tree. |

### Sleator-Tarjan Amortized Proof Sketch
*   Define the size of a node `v` in an auxiliary tree as `size(v) = 1 + size(left[v]) + size(right[v])`.
*   Assign rank `r(v) = log2(size(v))`. Define the potential function `Phi = Sum_{v in V} r(v)`.
*   By the Splay Access Lemma, splays amortize to `3 * (r(root) - r(x)) + 1 = O(log N)`.
*   During `Access(x)`, every transition across a dashed path-parent pointer switches a non-preferred edge to preferred. The total number of preferred child alterations across any sequence of `M` operations is proven to be `O((M + N) log N)` using heavy-light edge amortization.
*   Hence, all operations amortize strictly to **`O(log N)`**.

---

## 🎙️ Chapter 5: 45-Minute Senior / Lead Verbal Interview Script

```text
[00:00 - 05:00] Clarify & Problem Modeling
Candidate: "We have a dynamic graph where edges are added and deleted online, and we need to query path 
           aggregates (like path XOR or maximum edge weight). 
           If the graph were a static tree, I would recommend Heavy-Light Decomposition with a Segment Tree 
           for O(log^2 N) queries. If we only had edge additions without deletions, DSU with path compression 
           would suffice in O(alpha(N)). 
           However, because we have both dynamic mutations (Link and Cut) AND path queries, the optimal 
           data structure is a Link-Cut Tree, achieving amortized O(log N) per operation."

[05:00 - 15:00] Invariants & Trade-offs
Candidate: "In an interview setting, implementing the complete Link-Cut Tree takes roughly 150 lines. 
           Let me break down the governing invariants:
           1. We decompose the represented tree into disjoint preferred paths.
           2. Each preferred path is maintained as an auxiliary Splay tree keyed by node depth.
           3. Path-parent pointers connect auxiliary tree roots to their represented parent, but the parent 
              does not have child pointers back. This allows O(1) identification of splay roots via `IsRoot`.
           4. The heart of LCT is `Access(x)`, which splays x to the root, severs deeper preferred children, 
              and links up to the represented root."

[15:00 - 35:00] Coding Core Primitives (Focus on Access & MakeRoot)
Candidate: "I'll implement the flat-array version to guarantee zero garbage collection overhead:
           - First, `Rotate` and `Splay`, taking care to push down lazy reversal tags before rotations.
           - Next, `Access(x)`: looping `x = parent[x]`, splaying `x`, and setting `right[x] = t`.
           - Next, `MakeRoot(x)`: `Access(x)`, `Splay(x)`, then flipping `rev[x]`.
           - Finally, `Link` and `Cut` are straightforward 3-line methods on top of `MakeRoot`."

[35:00 - 45:00] Complexity Deconstruction & Production Robustness
Candidate: "The potential function Phi = Sum log(size(v)) guarantees that all operations amortize to O(log N).
           Edge cases to guard against in production:
           - Calling `Link(x, y)` when x and y are already in the same component creates a cycle. We guard 
             via `IsConnected(x, y)` checking `FindRoot(x) == FindRoot(y)`.
           - Calling `Cut(x, y)` on a non-existent edge: we verify `left[y] == x` and `right[x] == 0` after 
             `MakeRoot(x)` and `Access(y)`."
```

---

## 🔍 Chapter 6: Granular Edge-Case & Invariant Verification Table

| Edge Case Scenario | Concrete Input | Expected Behavior | Invariant Verification & Handling |
| :--- | :--- | :--- | :--- |
| **Cycle Prevention on Link** | `Link(1, 2)` when `1` and `2` connected | Abort without creating cycle. | Guard `if (IsConnected(x, y)) return;` via `FindRoot`. |
| **Cut Non-Existent Edge** | `Cut(1, 4)` where `1` and `4` in different trees | No mutation; graph remains unchanged. | `MakeRoot(1); Access(4); Splay(4)`: condition `left[4] == 1` fails; no disconnect. |
| **Cut Indirect Path Edge** | `1-2-3-4`, calling `Cut(1, 4)` | No mutation; only direct edge can be cut. | `Splay(4)` reveals `left[4]` is `3`, not `1`. Cut is safely rejected. |
| **Single Node Path Query** | `QueryPath(3, 3)` | Returns `val[3]`. | `MakeRoot(3); Access(3); Splay(3)` leaves `3` as splay root with no left or right child. |
| **Multiple Root Inversions** | Consecutive `MakeRoot(x); MakeRoot(y)` | Tree maintains valid depths. | Lazy tag `rev[x] ^= true` cascades down during `_push` on splay path traversal. |
| **Memory Locality on Flat Arrays**| 100,000 nodes, 1-indexed | Zero GC allocations during queries. | Flat contiguous arrays (`int[]`) avoid heap object fragmentation. |

---

> 🧭 **Navigation:** [← Previous Day](Week_16_Day_01_Skip_Lists_And_Treaps_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_16_Day_03_Persistent_Data_Structures_Instructional.md)
