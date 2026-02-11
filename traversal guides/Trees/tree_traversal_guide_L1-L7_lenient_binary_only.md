# Tree Traversal Mastery (Binary Trees) — Level 1→7 (Lenient)

**Scope:** Binary trees only for Levels 1–6.  
**Optional Level 7+:** DS-specific traversal patterns (Segment Tree / Fenwick / Trie) for interview depth.  
**Contract style:** **LENIENT** — empty input returns empty output; undefined cases return safe defaults.

Last updated: 2026-02-10

---

## How to use this guide

1. Read **Level 1** once and immediately hand-trace the example tree on paper.
2. For each level, do: (a) 3 hand traces, (b) 3 “say the invariant out loud” reps, (c) 2 small implementations (later).
3. Only move to the next level when you can predict **stack/queue contents** at any point in the traversal.

---

## One unified mental model

A traversal is always:

- A **frontier** (stack for DFS, queue for BFS).
- A **rule** for what you push next (children order).
- A **visit event** (when you record/emit a node).

If you can answer “**when do I visit?**” you can derive preorder / inorder / postorder / BFS variants.

---

## Lenient contracts (global)

- `root == null` → return `[]` (empty list).
- “View” traversals on empty tree → return `[]`.
- Boundary traversal on single node → return `[root]`.
- Vertical/diagonal traversals → if tie-break is unspecified, use **stable left-to-right by BFS discovery order**.

> Interview note: Always state your tie-break rules upfront for vertical/top/bottom views.

---

## Level roadmap (1→7)

| Level | Theme (Flow-wise) | You master | Typical output type |
|---|---|---|---|
| L1 | Physical layer: safe visiting | Recursive DFS orders + BFS level-order | Flat list of values |
| L2 | Structural layer: explicit control | Iterative DFS, stack states, parent pointers | Flat list + debug traces |
| L3 | Stateful layer: layers & aggregation | Zigzag, reverse levels, per-level summaries, views | List of lists / level maps |
| L4 | Derived projections | Vertical / diagonal / boundary / top-bottom concepts + tie-breaks | Grouped lists / columns |
| L5 | Abstract layer: traversal as a tool | Serialization, iterators, Euler tour framing, rebuild intuition | Strings / streams |
| L6 | Space-optimized | Morris traversal and “thread-restore” discipline | Flat list with O(1) extra space |
| L7+ | DS-specific traversal patterns | Segment tree overlap recursion, Fenwick lowbit walk, Trie enumeration | Query/update traces |

---

## Shared example tree (use for all dry-runs)

Use this tree to hand-trace every traversal:

```text
        A
      /        B     C
    / \        D   E     F
```

Child-order convention (important): **Left child before right child**.

---

## Level 1 — Physical layer (core traversals)

### 1.1 DFS Preorder (Root, Left, Right)

**Visit event:** when you first arrive at a node.

- Output on example: `A B D E C F`
- Use when: copying tree, building prefix-style expressions, “process parent before children.”

**Invariant:** when you record a node, you have not recorded anything from its children yet.

### 1.2 DFS Inorder (Left, Root, Right)

**Visit event:** after finishing left subtree, before exploring right.

- Output: `D B E A C F`
- Use when: BST gives sorted order (because inorder respects BST ordering).

**Invariant:** when you record a node, its entire left subtree has already been recorded.

### 1.3 DFS Postorder (Left, Right, Root)

**Visit event:** after finishing both subtrees.

- Output: `D E B F C A`
- Use when: deleting/freeing nodes, computing subtree properties bottom-up.

**Invariant:** when you record a node, both children subtrees are complete.

### 1.4 BFS Level-order (by depth)

**Frontier:** queue.

- Output by levels: `[[A], [B, C], [D, E, F]]`
- Use when: shortest depth reasoning, per-level aggregation.

**Invariant:** the queue contains nodes discovered but not yet processed; processing is in non-decreasing depth order.

### 1.5 “What can go wrong” (Level 1)

- Recording inorder at the wrong time (you’ll accidentally produce preorder).
- Forgetting child ordering rule (left/right swap silently changes answers).
- Marking “visited” too late in graphs; in trees, cycles aren’t present, but parent-pointers can create cycles (handled in L2).

---

## Level 2 — Structural layer (iterative control)

### 2.1 Iterative preorder (stack)

**Key idea:** push right first, then left, so left pops first.

**Invariant:** stack top is the next node to process in preorder.

### 2.2 Iterative inorder (stack)

**Key idea:** keep pushing left until null; pop = visit; then go right.

**Invariant:** stack is the path of “unfinished ancestors” whose left subtree is done but node not yet visited.

### 2.3 Iterative postorder (two standard patterns)

Pick one and master it:

- **Two stacks** (simple).
- **One stack + lastVisited pointer** (more interview-common).

**Invariant (one-stack):** a node is visited only after its right subtree is either null or already visited.

### 2.4 Parent pointers / back edges

If nodes have `parent` pointers, the structure is no longer a pure tree walk — you must prevent bouncing between parent/child.

Two safe approaches:

- Track `prev` pointer (where you came from) and choose next step.
- Track a visited set (rare for pure tree, but valid).

---

## Level 3 — Stateful layer (layers, zigzags, and views)

### 3.1 Level-order with per-level aggregation

Examples of per-level outputs:

- Sum/avg of each level.
- Max value per level.
- Width of level.

**Invariant:** the loop processes exactly one depth layer at a time (using queue size snapshot).

### 3.2 Zigzag / Spiral level order

**Idea:** alternate direction each level.

Two clean implementations:

- BFS levels + reverse list every other level.
- Deque-based BFS.

**Invariant:** output direction flips, but the discovered set per level is unchanged.

### 3.3 Right/Left side view

Two standard ways:

- BFS: last node in each level.
- DFS: first time you reach a depth (right-first for right view).

**Invariant (DFS view):** first recorded node at a depth is the visible one under chosen child priority.

---

## Level 4 — Derived projections (vertical / diagonal / boundary)

### 4.1 Vertical order (columns)

**Coordinate model:** root at column 0; left child column-1; right child column+1.

**Tie-break contract (lenient default):**

1. Primary: column ascending.
2. Secondary: row/depth ascending.
3. Tertiary: stable left-to-right by BFS discovery.

### 4.2 Top view / bottom view

- **Top view:** first node seen in a column by increasing depth.
- **Bottom view:** last node seen in a column by increasing depth.

**Invariant:** for each column, keep best candidate by (depth, tie-break).

### 4.3 Diagonal traversal

One convention: left child stays on next diagonal; right child stays on same diagonal.

**Invariant:** nodes grouped by diagonal index; group order depends on BFS vs DFS choice (state it).

### 4.4 Boundary traversal

Common decomposition:

- Left boundary (excluding leaves)
- Leaves (left-to-right)
- Right boundary (excluding leaves, reversed)

Lenient edge cases:

- If root is leaf → return `[root]`.

---

## Level 5 — Abstract layer (traversal as a tool)

### 5.1 Serialization / deserialization (traversal-defined encoding)

Two interview-standard encodings:

- Preorder + null markers.
- Level-order + null markers.

**Invariant:** encoding must be unambiguous; decoding reads tokens in the same order the encoder produced.

### 5.2 Iterators mindset

Traversal can be packaged as a state machine:

- `hasNext()` depends on whether frontier is empty.
- `next()` advances one step and returns one visited node.

**Invariant:** frontier fully represents “where you are” in the walk.

### 5.3 Euler tour framing (enter/exit times)

Think of each node as having events:

- ENTER(node)
- BETWEEN(left,right) (inorder moment)
- EXIT(node)

Pre/in/post are just different event-logging choices.

---

## Level 6 — Space-optimized (Morris traversal)

### 6.1 Morris inorder (O(1) extra space)

**Idea:** temporarily “thread” the tree by linking the inorder predecessor to the current node, then restore.

**When safe:**

- You are allowed to mutate pointers temporarily.
- You guarantee restoration (no early returns that skip cleanup).

**When unsafe:**

- Concurrent readers, shared immutable trees, or code paths that can exit early without restoring.

**Invariant:** every temporary thread you create is removed exactly once.

> Optional: Morris preorder is similar (visit on first encounter before threading).

---

## Level 7+ — DS-specific traversal patterns (optional but powerful)

These are not “binary tree traversals” problems; they are traversal *mechanics* that show up as soon as interviews move to range queries and prefix structures.

### 7.1 Segment Tree traversal (query/update recursion)

Traversal is driven by **overlap states** between node-range and query-range:

- No overlap → return neutral value.
- Total overlap → return stored node value.
- Partial overlap → recurse both children and combine.

**Invariant:** every node stores a correct aggregate of its segment; query combines only necessary segments.

### 7.2 Fenwick Tree (BIT) traversal (lowbit walk)

Traversal is index-walking:

- Prefix sum: repeatedly move `i -= lowbit(i)`.
- Point update: repeatedly move `i += lowbit(i)`.

**Invariant:** BIT nodes represent partial prefix segments; the lowbit step moves to next responsible accumulator.

### 7.3 Trie traversal (enumeration / lexicographic DFS)

Traversal is character-walking:

- DFS to emit all words (visit event at “isWord/end” marker).
- Optional: iterate children in sorted order for lexicographic output.

**Invariant:** current path string equals the prefix represented by the current trie node.

---

## Mastery checklist (quick)

You’re interview-ready on traversal when you can:

- Predict stack/queue contents mid-walk.
- State the visit-event rule for each traversal.
- Explain tie-breaks for vertical/top/bottom and defend them.
- Explain why Morris is O(1) extra space and what restoration means.
- Explain segment-tree overlap recursion and BIT lowbit stepping as traversal.
