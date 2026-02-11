# 🌳 Tree Traversal Mastery — Unified Guide (All Levels) + Appendix (Pre‑Drills)

**Goal:** Master traversal and navigation across tree variants (binary, BST, N-ary, tries, implicit/array trees, rooted trees from edges), with strong invariants and debugging instincts.

---

## ✅ Summary list
- 🧠 One model: **Frontier + Visit timing + State**
- 🌿 DFS: preorder / inorder / postorder, Euler ENTER/EXIT, path-state backtracking
- 🧺 BFS: level-order waves, zigzag, views, level aggregates
- 🧊 Coordinate traversals: vertical order, top/bottom view, diagonal
- 🎒 Iterative DFS: classic stacks + phase frames; postorder alternatives
- 🧵 Advanced: Morris inorder (O(1) auxiliary) with strict restore invariant
- 🌐 Bridge to graphs: rooted trees from **adjacency lists** with `parent/visited`
- 💾 Serialization: preorder-with-nulls, level-order serialization
- 🌿 Trie traversal: lexicographic DFS + rollback

---

## 🗺️ Level roadmap
- **Level 1:** Physical movement + recursion skeleton
- **Level 2:** Representations (pointers, N-ary, implicit arrays)
- **Level 3:** Multi-state traversal (iterative control, BFS waves)
- **Level 4:** Range/path + coordinate traversals
- **Level 5:** Advanced control + implicit search trees
- **Appendix:** Missing modules (pre‑drills) that complete the “tree traversal toolkit” and transition you to graphs

---

<a id="mental-model"></a>
# 🧠 Unified mental model (use for every traversal)

## Why
Traversals feel like many unrelated recipes until you view them as the same loop with different choices.

## What
Every traversal is defined by:
- **Frontier:** what you expand next (stack/queue/deque/thread).
- **Visit timing:** when you do work (ENTER/BETWEEN/EXIT).
- **State:** extra info required for ordering/grouping (depth, parent, phase, col, row, diagonal).

## How (step/flow)
1) Fix child order (binary: left→right; N-ary: children[] order).
2) Choose frontier (stack for DFS, queue for BFS).
3) Choose visit timing (pre/in/post or phases).
4) Add state only if the output demands it (levels/columns/paths).

## Where and When (use cases)
- Interviews: ordering, grouping, views, subtree queries, serialization.
- Systems: hierarchical rendering, indexing, permission inheritance, aggregation.

## Common pitfalls
- Not defining child order.
- Confusing discovery order with visit order (especially BFS).

## Tips and tricks
- Write a one-line contract before coding: “Frontier=?, Visit timing=?, State=?”.

---

<a id="level-1"></a>
# Level 1 🟢 Physical movement (recursive DFS basics)

<a id="l1-template"></a>
## 1.1 Universal recursive DFS template

### Why
Most tree problems are “solve node using solutions of children.”

### What
A function that processes a subtree and returns something (value/bool/list/summary).

### How (step/flow)
1) If node is null → return identity.
2) PRE work (optional).
3) Recurse left / children.
4) IN work (binary only, optional).
5) Recurse right.
6) POST work (optional).
7) Return summary.

### Where and When (use cases)
Traversal lists, depth/height, validation, subtree properties.

### Common pitfalls
Missing base case; wrong identity value.

### Tips and tricks
Say: “When dfs(node) returns, node’s subtree is solved.”

Visual:
```text
      1
     / \
    2   3

PRE : 1 2 3
IN  : 2 1 3
POST: 2 3 1
```

C# template:
```csharp
static T Dfs(BNode? node)
{
    if (node == null) return identity;
    // PRE
    var left = Dfs(node.Left);
    // IN (binary only)
    var right = Dfs(node.Right);
    // POST
    return Combine(left, right, node.Val);
}
```

Python template:
```python
def dfs(node):
    if not node:
        return identity
    # PRE
    left = dfs(node.left)
    # IN
    right = dfs(node.right)
    # POST
    return combine(left, right, node.val)
```

---

## 1.2 Visit timing: preorder / inorder / postorder

### Why
Visit timing determines which information is available when you compute.

### What
- **Preorder:** visit on ENTER (parent first).
- **Inorder:** visit BETWEEN left and right (binary only).
- **Postorder:** visit on EXIT (children first).

### How (step/flow)
- Need to decide before exploring children → preorder.
- Need BST sorted order → inorder (only if BST invariant holds).
- Need child results → postorder.

### Where and When (use cases)
Pre: serialization, outlines; In: BST iteration; Post: aggregation, cleanup.

### Common pitfalls
Assuming inorder implies sorted without BST property.

### Tips and tricks
If the formula uses child results, default to postorder.

---

## 1.3 Edge cases & recursion safety

### Why
Trees can degenerate into a chain; recursion depth can break.

### What
Empty tree, single node, skewed, deep trees.

### How (step/flow)
1) Handle `root == null`.
2) Test 1-node.
3) Test skewed chain.

### Where and When (use cases)
Always.

### Common pitfalls
Stack overflow in deep recursion.

### Tips and tricks
If depth could be huge, switch to Level 3 iterative DFS.

---

### ✅ Level 1 invariants
- Base case is correct.
- Child order is explicit.
- Visit timing matches intended traversal.

### 🐛 Level 1 debugging
Log: `(node, depth, event ENTER/EXIT)`; wrong order ⇒ visit timing/push order.

---

<a id="level-2"></a>
# Level 2 🔵 Representations (what you can traverse)

## 2.1 Pointer-based nodes (binary & N-ary)

### Why
Most trees are node-pointer based; traversal is pointer walking.

### What
- Binary: `Left`, `Right`
- N-ary: `Children[]` (ordered)

### How (step/flow)
Define node type + deterministic child iteration order.

### Where and When (use cases)
DOM/AST, org charts, tries.

### Common pitfalls
Using unordered containers for children when order matters.

### Tips and tricks
If order matters, store children in list/array (or sorted dictionary for tries).

---

## 2.2 Implicit array trees (heap-style)

### Why
Some trees are stored in arrays; traversal becomes index arithmetic.

### What
0-based array:
- left = `2*i + 1`
- right = `2*i + 2`
- parent = `(i-1)/2`

### How (step/flow)
Start at index 0; compute children; stop on out-of-bounds.

### Where and When (use cases)
Heaps, priority queues, complete trees.

### Common pitfalls
Mixing 0-based and 1-based formulas.

### Tips and tricks
Write formulas at the top of the function.

---

### ✅ Level 2 invariants
- Representation rules are fixed (pointers vs array).
- Child iteration order is deterministic.

### 🐛 Level 2 debugging
Implicit trees: log `(i, value, leftIndex, rightIndex)`.

---

<a id="level-3"></a>
# Level 3 🟠 Multi-state traversal (iterative DFS + BFS waves)

## 3.1 Iterative DFS: preorder / inorder / postorder (binary)

### Why
Avoid recursion limits and gain explicit control.

### What
- Preorder: stack, push right then left.
- Inorder: push-left chain, visit on pop.
- Postorder: needs extra state (phase frames, lastVisited, or two stacks).

### How (step/flow)
1) Declare stack invariant.
2) Choose the simplest technique that meets requirements.

### Where and When (use cases)
Deep trees, production safety.

### Common pitfalls
Wrong push order; visiting at wrong moment.

### Tips and tricks
Use **phase frames** when order must be deterministic and you want one reusable pattern.

---

## 3.2 Phase-frame DFS (ENTER/EXIT)

### Why
It turns recursion into a reliable iterative state machine.

### What
Stack items: `(node, phase)` where phase ∈ {ENTER, EXIT}.

### How (step/flow)
- On ENTER: push EXIT, then push children ENTER in reverse desired processing order.
- On EXIT: do postorder work.

### Where and When (use cases)
Iterative postorder, mixed pre+post hooks.

### Common pitfalls
Scheduling children in wrong order.

### Tips and tricks
Treat ENTER = expand, EXIT = finalize.

---

## 3.3 BFS level-order (waves)

### Why
Level grouping and minimum-depth logic are natural with BFS.

### What
Queue frontier + `levelSize` snapshot per wave.

### How (step/flow)
1) Enqueue root.
2) While queue not empty: snapshot `levelSize`; pop exactly that many; enqueue children.

### Where and When (use cases)
Level lists, zigzag, views, per-level aggregates.

### Common pitfalls
Not snapshotting `levelSize` causes level mixing.

### Tips and tricks
Queue contains the next frontier only.

---

### ✅ Level 3 invariants
- Stack/queue is exactly the frontier.
- Phase transitions are consistent.

### 🐛 Level 3 debugging
DFS: log `(node, phase, stackSize)`; BFS: log `(level, levelSize, queueSnapshot)`.

---

<a id="level-4"></a>
# Level 4 🟣 Range/path + coordinate traversals

## 4.1 Path-state DFS (root-to-leaf, backtracking)

### Why
Path problems fail if rollback is wrong.

### What
Maintain `path` that mirrors recursion stack.

### How (step/flow)
ENTER: push; process; recurse children; EXIT: pop.

### Where and When (use cases)
Root-to-leaf paths, sums, trie word enumeration.

### Common pitfalls
Missing pop ⇒ state leaks into siblings.

### Tips and tricks
Mutate one path list; copy only when committing a result.

---

## 4.2 Coordinate traversals (vertical/top/bottom/diagonal)

### Why
Many problems are “traversal + coordinates + grouping.”

### What
- Vertical: group by `col`.
- Top view: first seen per `col`.
- Bottom view: last seen per `col`.
- Diagonal: define diagonal transitions (often right=same, left=+1).

### How (step/flow)
Carry `(row, col)` or `(diagonal)` state and group accordingly.

### Where and When (use cases)
UI projections, structural summaries.

### Common pitfalls
Missing tie-break rule when collisions occur.

### Tips and tricks
If strict ordering is required, store `(col,row,val)` and sort.

---

## 4.3 Boundary traversal (anti-clockwise)

### Why
Composite traversal + de-dup discipline.

### What
root + left boundary (no leaves) + leaves + right boundary (no leaves, reversed).

### How (step/flow)
Walk left boundary; collect leaves; walk right boundary and reverse.

### Where and When (use cases)
Perimeter rendering.

### Common pitfalls
Duplicating leaves.

### Tips and tricks
Define `isLeaf()` once.

---

### ✅ Level 4 invariants
- State updates are consistent.
- De-dup rules are explicit.

### 🐛 Level 4 debugging
Log `(node, depth=row, col/diag, action)`.

---

<a id="level-5"></a>
# Level 5 🔴 Advanced control + implicit trees

## 5.1 Euler tour ENTER/EXIT times

### Why
Subtree queries become range problems.

### What
Record `tin` on ENTER and `tout` on EXIT.

### How (step/flow)
ENTER: time++; traverse children; EXIT: time++.

### Where and When (use cases)
Subtree flattening, ancestor checks.

### Common pitfalls
Recording only one timestamp.

### Tips and tricks
Treat every node as a lifecycle: ENTER then EXIT.

---

## 5.2 Tree DP (postorder summaries + global update)

### Why
Many classic problems need both a return summary and a global best.

### What
`dfs(node)` returns summary; global answer updated using children summaries.

### How (step/flow)
Compute child summaries; update global; return summary.

### Where and When (use cases)
Diameter, max path sum.

### Common pitfalls
Mixing return value with global best.

### Tips and tricks
Write two distinct lines: update global; return summary.

---

## 5.3 Morris inorder (O(1) auxiliary) — concept

### Why
Follow-up constraint: inorder traversal without recursion/stack.

### What
Temporary threads from inorder predecessor to current; later remove.

### How (step/flow)
If no left: visit, go right; else find predecessor; create/remove thread accordingly.

### Where and When (use cases)
Only when explicitly asked for O(1) auxiliary traversal.

### Common pitfalls
Not removing threads corrupts tree.

### Tips and tricks
Invariant: every thread created must be removed.

---

## 5.4 Implicit search trees (state spaces)

### Why
Many problems are “tree traversal” without explicit nodes.

### What
Each state generates next states; DFS enumerates, BFS finds minimum steps.

### How (step/flow)
Define state, transitions, stopping rule; add visited if states can repeat.

### Where and When (use cases)
Backtracking generation, shortest-step transformations.

### Common pitfalls
Treating repeated states as a tree (it’s a graph).

### Tips and tricks
Ask: “Can two paths reach the same state?” If yes, add visited.

---

### ✅ Level 5 invariants
- Summaries are correct at return time.
- Threads/structural edits are restored.
- Visited is used when state repetition exists.

---

<a id="appendix-missing-modules"></a>
# Appendix: Missing Modules (Pre‑Drills)

This appendix completes the traversal toolkit and bridges trees → graphs.

Cross-links:
- Unified model: see [Unified mental model](#mental-model)
- Representations: see [Level 2](#level-2)
- Iterative control: see [Level 3](#level-3)
- Coordinates/paths: see [Level 4](#level-4)

---

<a id="app-adj"></a>
## A1) 🌐 Tree as adjacency list (rooted tree from edges)

### Why
Many tree inputs come as edges; traversal must avoid walking back to parent—this is the bridge to graphs.

### What
Adjacency list `adj[u] = neighbors`, rooted at `root`. Traverse with `(node, parent)` (or `visited[]`).

### How (step/flow)
1) Build `adj` from edges (bidirectional).
2) DFS/BFS from root.
3) Skip `neighbor == parent` (tree) or check `visited` (graph-ready).

### Where and When (use cases)
- Interview: subtree size, distances, diameter from edges.
- Systems: relationship-derived hierarchies.

### Common pitfalls
Forgetting parent/visited ⇒ infinite loop on undirected edges.

### Tips and tricks
Prefer `parent` for trees; prefer `visited` for graphs.

C# DFS:
```csharp
static void Dfs(int u, int parent, List<int>[] adj, List<int> preorder)
{
    preorder.Add(u);
    foreach (var v in adj[u])
    {
        if (v == parent) continue;
        Dfs(v, u, adj, preorder);
    }
}
```

---

<a id="app-views"></a>
## A2) 👁️ Full views family (left/right) + enqueue-order policy

### Why
Views are BFS levels + a selection rule; enqueue order affects what becomes “first.”

### What
- Left view: first node of each level.
- Right view: last node of each level.

### How (step/flow)
BFS with `levelSize`; record `i==0` (left) or `i==levelSize-1` (right).

### Where and When (use cases)
Diagnostics, outlines, right-side-view problems.

### Common pitfalls
Not snapshotting `levelSize` mixes levels.

### Tips and tricks
Standardize child enqueue order (usually left then right), then select first/last.

---

<a id="app-vertical"></a>
## A3) 🧊 Vertical traversal tie-break spec

### Why
Most vertical-traversal failures are missing determinism rules.

### What
Track `(col, row, val)` and sort by `(col asc, row asc, val asc)` when the problem demands strict ordering.

### How (step/flow)
Traverse to collect triples; sort; group by `col`.

### Where and When (use cases)
Vertical traversal variants with collision ordering.

### Common pitfalls
Appending in traversal order when spec requires sorting.

### Tips and tricks
Write tie-break rule at the top of your solution.

---

<a id="app-bfs"></a>
## A4) 🧺 BFS variants toolkit (bottom-up, RTL, aggregates, width)

### Why
Most BFS variants are “level snapshot + transformation.”

### What
- Bottom-up: reverse levels after BFS.
- RTL per level: reverse the level list.
- Aggregates: sum/max/min/avg/count per level.
- Width (simple): max `levelSize`.

### How (step/flow)
Keep one BFS skeleton and plug in a “level handler.”

### Where and When (use cases)
Level summaries and reporting.

### Common pitfalls
Reversing the wrong thing (levels vs nodes).

### Tips and tricks
Separate traversal from presentation.

---

<a id="app-postorder"></a>
## A5) 🎒 Iterative postorder alternatives

### Why
Postorder is the hardest iterative order; you should recognize the common patterns.

### What
- Phase frames (ENTER/EXIT): robust default.
- Two stacks: simple, extra memory.
- lastVisited: one stack, more logic.

### How (step/flow)
Choose based on clarity vs memory vs interview comfort.

### Where and When (use cases)
Bottom-up computations without recursion.

### Common pitfalls
lastVisited bugs that visit parent too early.

### Tips and tricks
Default to phase frames under time pressure.

---

<a id="app-parent"></a>
## A6) 🧭 Parent pointers + iterator mindset

### Why
Parent pointers enable O(1) auxiliary navigation without recursion/stack.

### What
Inorder successor (with parent pointer):
- If right exists: leftmost of right.
- Else climb until you come from a left child.

### How (step/flow)
Two-case rule; implement carefully.

### Where and When (use cases)
BST iterators, ordered traversal.

### Common pitfalls
Applying successor logic to a non-BST when you need sorted semantics.

### Tips and tricks
Think “Iterator state machine: Current + Next().”

---

<a id="app-serialization"></a>
## A7) 💾 Serialization patterns (preorder-with-nulls, level-order)

### Why
Traversal defines an encoding; without null markers, decode can be ambiguous.

### What
- Preorder with `#` markers for nulls.
- BFS level-order with null placeholders.

### How (step/flow)
Serialize into tokens; deserialize by consuming tokens with an index/pointer.

### Where and When (use cases)
Persistence, caching, network transfer.

### Common pitfalls
Index bugs during deserialize.

### Tips and tricks
Log token index while debugging.

---

<a id="app-trie"></a>
## A8) 🌿 Trie traversal (lexicographic DFS + rollback)

### Why
Trie traversal often requires lexicographic output and strict rollback discipline.

### What
DFS children in sorted order; maintain a mutable path (StringBuilder) and rollback on exit.

### How (step/flow)
ENTER append char; if terminal emit; DFS children; EXIT remove char.

### Where and When (use cases)
Autocomplete, dictionary export.

### Common pitfalls
Unordered children leads to non-lexicographic output.

### Tips and tricks
Use `SortedDictionary<char, TrieNode>` in C# for ordered traversal.

---

<a id="app-limits"></a>
## A9) 📏 Complexity & limits rulebook

### Why
Traversal choice is driven by depth/width constraints.

### What
- Recursive DFS risks stack overflow on skewed trees.
- BFS memory spikes with high width.
- Morris is only for explicit O(1) auxiliary constraints.

### How (step/flow)
Estimate depth/width; choose recursion vs iterative stack vs BFS; add state only when needed.

### Where and When (use cases)
Production robustness and interview justification.

### Common pitfalls
Using fancy techniques (Morris) when not required.

### Tips and tricks
Default: recursion for clarity; iterative DFS for deep trees; BFS for level/shortest-depth behavior.

---

## ✅ Pre‑drills readiness checklist
- I can state the traversal contract (frontier + timing + state).
- I can dry-run and list stack/queue snapshots.
- I can switch between node pointers and adjacency list + parent.
- I can explain ordering/tie-break rules when required.
- I can serialize/deserialize preorder-with-nulls without index bugs.
