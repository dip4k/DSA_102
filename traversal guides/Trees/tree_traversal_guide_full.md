# 🌳 Tree Traversal Guide (Binary + N-ary Enhancement)

## ✅ Summary list
- 🧠 Unified model: **Frontier + Visit event + State** (all traversals are variations).
- 🧭 DFS (Binary): preorder / inorder / postorder, recursive and iterative (stack + phases). [web:1]
- 🧺 BFS (Binary): level-order (wave processing), zigzag, right/left view (selection per level). [web:1]
- 🧵 Advanced: Morris inorder concept (binary, pointer-threading, O(1) extra structure). [web:4]
- 🌿 N-ary enhancement: preorder/postorder (children left→right), level-order BFS (queue), and the “push children right→left onto stack” trick for iterative preorder. [web:49][web:58]

---

## 🧠 Unify all traversals

### Why
If you treat each traversal as a separate recipe, you’ll memorize. If you treat traversal as a control system, you’ll reason.

### What
Every traversal is defined by:
- **Frontier**: what to expand next (stack, queue, or temporary thread).
- **Visit event**: when you “do work” on a node (ENTER / BETWEEN / EXIT).
- **State**: extra info carried along (depth, parent, phase, column).

### How (step/flow)
1) Pick frontier type.
2) Define visit event timing.
3) Define child iteration order.
4) Add state only when output requires grouping/selection.

### Where and When (use cases)
- Interviews: ordering output, grouping by level, views, subtree computations.
- Systems: file trees, org charts, ASTs, permission inheritance.

### Common pitfalls
- Not defining left-to-right child order (your output order changes silently).
- Confusing “discovery” with “visit” (especially in BFS variants).

### Tips and tricks
- Write a one-line contract: “Visit on ENTER; children left-to-right.”

---

# Part A — Binary trees

## A0. Visual baseline

```text
        A
      /   \
     B     C
    / \     \
   D   E     F

Levels:
0: A
1: B, C
2: D, E, F
```

---

## A1. DFS preorder (Root → Left → Right) 🧾

### Why
Preorder is “parent first”. It’s natural for copying/serializing shape, or when parent decisions guide child work.

### What
Visit order: Root, then left subtree, then right subtree. [web:1]

### How (step/flow)
- Recursive: VISIT at ENTER.
- Iterative (stack): pop, visit, push right then left (so left is processed first).

### Where and When (use cases)
- Serialize/clone a tree structure; prefix expression form. [web:1]

### Common pitfalls
- Pushing left then right on a stack (you’ll process right first).

### Tips and tricks
- Mental rule: **stack reverses order**; push children in reverse of desired processing.

**Traversal on baseline tree**
```text
A B D E C F
```

---

## A2. DFS inorder (Left → Root → Right) 📐

### Why
Inorder is the “between children” visit event; it’s the canonical traversal for ordered binary structures.

### What
Visit order: left subtree, root, right subtree. [web:1]

### How (step/flow)
- Recursive: left → visit → right.
- Iterative: keep walking left pushing nodes, then pop/visit, then go right.

### Where and When (use cases)
- BST sorted order traversal (assuming BST invariant holds).

### Common pitfalls
- Applying inorder to N-ary trees without defining what “between” means.

### Tips and tricks
- Inorder iterative mantra: “go left until null; pop; go right.”

**Traversal on baseline tree**
```text
D B E A C F
```

---

## A3. DFS postorder (Left → Right → Root) 🧱

### Why
Postorder is “children first”. It’s the default for bottom-up computations.

### What
Visit order: left subtree, right subtree, root. [web:1]

### How (step/flow)
- Recursive: visit at EXIT.
- Iterative: use (a) two stacks, or (b) one stack + lastVisited, or (c) explicit phase states.

### Where and When (use cases)
- Compute subtree height/sum, delete/free nodes.

### Common pitfalls
- In iterative postorder, visiting a node before both children are fully processed.

### Tips and tricks
- If your formula uses child results, reach for postorder.

**Traversal on baseline tree**
```text
D E B F C A
```

---

## A4. BFS level-order (by levels) 🧺

### Why
BFS gives you “distance from root first” and natural level grouping.

### What
Level-order visits nodes level by level (Breadth-First Traversal). [web:1]

### How (step/flow)
1) Queue starts with root.
2) Pop front, visit, push children.
3) If you need level groups: process in **waves** (at each level, loop exactly `queue_size` times).

### Where and When (use cases)
- Minimum depth, level averages/sums, grouping output by depth.

### Common pitfalls
- Not using wave-size when grouping; nodes leak into the wrong level.

### Tips and tricks
- Wave processing is the cleanest level grouping pattern.

**Traversal on baseline tree (flat)**
```text
A B C D E F
```

---

## A5. Zigzag level-order 🔄

### Why
Teaches control of ordering *within* a level while preserving BFS layering.

### What
Alternate direction each level: L→R then R→L then L→R.

### How (step/flow)
- Run BFS by waves.
- Reverse the collected list for odd levels (or insert into a deque by parity).

### Where and When (use cases)
- Interview variants; UI “snake-like” reading of levels.

### Common pitfalls
- Reversing the entire output, instead of per-level.

### Tips and tricks
- Define parity: “level 0 is L→R”.

---

## A6. Left view / Right view 👁️

### Why
Views reduce each level to one representative: first or last.

### What
- Left view: first node encountered per level.
- Right view: last node encountered per level.

### How (step/flow)
- BFS by waves.
- Left view: take `level[0]`; right view: take `level[-1]`.

### Where and When (use cases)
- Quick shape summaries; UI outline/preview.

### Common pitfalls
- Wrong enqueue order can change which node becomes “first” in level.

### Tips and tricks
- Fix child order first (left then right), then views become trivial.

---

## A7. Euler tour (ENTER/EXIT events) 🔁

### Why
Many tasks need two moments per node: setup at entry and finalize at exit.

### What
A DFS that emits events at ENTER and EXIT; useful for timestamps and subtree range thinking. [web:4]

### How (step/flow)
- ENTER: record `tin[node]`.
- Recurse children.
- EXIT: record `tout[node]`.

### Where and When (use cases)
- Subtree queries after flattening, ancestor checks using time intervals.

### Common pitfalls
- Not recording both enter and exit consistently.

### Tips and tricks
- Think lifecycle: ENTER (discover) → EXIT (complete).

---

## A8. Morris inorder (concept) 🧵

### Why
It shows inorder traversal without stack/recursion by temporarily threading pointers.

### What
Morris traversal uses temporary links to predecessors and later restores the tree, enabling inorder traversal with O(1) extra structure. [web:4]

### How (step/flow)
- If no left: visit, go right.
- Else find predecessor in left subtree:
  - If predecessor.right is null: set to current, go left.
  - Else predecessor.right points to current: remove thread, visit, go right.

### Where and When (use cases)
- When an interviewer explicitly asks for inorder with no stack/recursion.

### Common pitfalls
- Forgetting to restore threads (mutates tree).

### Tips and tricks
- Treat Morris as an advanced specialty tool; prefer stack-based traversal in real code.

---

# Part B — N-ary trees (enhancement) 🌿

## B0. N-ary visual baseline

```text
        A
     /  |  \
    B   C   D
   / \      |
  E   F     G

Child iteration order is left-to-right in the children list.
```

---

## B1. N-ary preorder (Root then children left→right) 🧾

### Why
You often want to process a parent before its categories/items.

### What
Visit root first, then recursively visit each child left-to-right. [web:49]

### How (step/flow)
- Recursive: visit node, then loop children.
- Iterative (stack): pop, visit, push children **right-to-left** so leftmost child is processed first. [web:49]

### Where and When (use cases)
- Printing outlines, exporting hierarchical structures.

### Common pitfalls
- Pushing children left-to-right on a stack (you’ll process rightmost child first).

### Tips and tricks
- Stack reverses; push children in reverse.

---

## B2. N-ary postorder (children then root) 🧱

### Why
When you need results from all children before computing parent.

### What
Traverse all children left-to-right, then visit root.

### How (step/flow)
- Recursive: loop children, then visit.
- Iterative: can be done with two stacks or stack+phase state.

### Where and When (use cases)
- Aggregation across many child subtrees.

### Common pitfalls
- Visiting parent too early.

### Tips and tricks
- If you need child results, visit on EXIT.

---

## B3. N-ary level-order (BFS) 🧺

### Why
Level grouping is common in UI rendering and in “distance from root” reasoning.

### What
BFS processes nodes level-by-level using a queue. [web:58]

### How (step/flow)
- Queue root.
- For each wave: pop waveSize nodes, visit, enqueue all their children left-to-right.

### Where and When (use cases)
- Level summaries, progressive expansion, breadth exploration.

### Common pitfalls
- Not using wave size when you need level groups.

### Tips and tricks
- The wave pattern works identically for binary and N-ary trees.

---

# ✅ Cross-cutting invariants

## DFS invariants
- Child order is explicit and consistent.
- Visit timing (ENTER/BETWEEN/EXIT) matches the required order.
- Each node is visited exactly once per visit event.

## BFS invariants
- Queue is FIFO.
- For level grouping, each level processes exactly the nodes present at wave start.

---

# 🐛 Debugging playbook

## DFS
- Print: `node, depth, event(ENTER/EXIT)`.
- Wrong order with correct set → push order / visit timing.

## BFS
- Print: `level, waveSize, queueSnapshot`.
- Wrong grouping → waveSize computed at wrong time.

---

# 📚 Free references
- GeeksforGeeks — Binary tree traversal orders and BFS: https://www.geeksforgeeks.org/dsa/tree-traversals-inorder-preorder-and-postorder/ [web:1]
- Wikipedia — Tree traversal (event framing, general concepts): https://en.wikipedia.org/wiki/Tree_traversal [web:4]
- GeeksforGeeks — N-ary preorder traversal and iterative child push ordering: https://www.geeksforgeeks.org/dsa/preorder-traversal-of-a-n-ary-tree/ [web:49]
- GeeksforGeeks — N-ary level order traversal (BFS): https://www.geeksforgeeks.org/dsa/level-order-traversal-of-n-ary-tree/ [web:58]
