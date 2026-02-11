# Tree Traversal Drills — Levels 1→7 (Lenient)

**Purpose:** short, repeatable drills to internalize traversal invariants before heavy problem-solving.

Contract: LENIENT (empty tree → empty outputs).

Last updated: 2026-02-10

---

## How to run drills (3 passes)

1. **Hand-trace pass (paper):** draw frontier (stack/queue) after each step.
2. **Invariant pass (voice):** say the invariant after every pop/dequeue.
3. **Implementation pass (later):** implement once in your language, then stop coding and return to reasoning.

---

## Level 1 drills (core orders)

1. Dry-run preorder/inorder/postorder on the shared example tree; write outputs.
2. For inorder, mark the exact moment each node is “visited” (circle it).
3. Modify example tree by removing node E; recompute all 4 traversals.
4. Single-node tree: list outputs for all traversals.
5. Empty tree: define outputs for all traversals (lenient).
6. Given output `A B D E C F`, identify traversal type and justify.
7. Create a tree where preorder == inorder (is it possible? under what shape?).
8. Create a tree where preorder == postorder (constraints?).
9. Explain one real use-case each for pre/in/post without code.
10. Debug drill: someone claims inorder output `B D E A C F` — find the bug (visit timing).

---

## Level 2 drills (iterative control)

1. Write stack states for iterative preorder on the example tree (push order matters).
2. Iterative inorder: list the sequence of (push-left...) and pops.
3. Postorder (one stack + lastVisited): simulate the lastVisited pointer updates.
4. Convert recursive preorder to iterative without changing output order.
5. Parent-pointer walk: given a node with parent links, design a no-visited-set traversal using `prev`.
6. “Early exit” drill: if you search for a value and return immediately, which traversals risk leaving state inconsistent? (think Level 6 too).

---

## Level 3 drills (levels and views)

1. BFS level-order: write queue contents at start of each level.
2. Compute: max per level, avg per level, and right side view on example tree.
3. Zigzag: produce output list-of-lists and show direction flag per level.
4. Design: given a target depth k, output only that level in O(width) memory.
5. Multi-source idea transfer: how would “level waves” look if you started from leaves? (conceptual drill).

---

## Level 4 drills (vertical/diagonal/boundary)

1. Assign (row, col) coordinates to each node in the example tree.
2. Vertical order: group nodes by column using your tie-break contract.
3. Top view vs bottom view: produce both and explain difference.
4. Boundary traversal: list left boundary, leaves, right boundary separately then concatenate.
5. Tie-break stress test: design a tree where two nodes share same (row, col); decide ordering and state your rule.

---

## Level 5 drills (serialization and iterator thinking)

1. Preorder+null serialization: write tokens for the example tree.
2. Level-order+null serialization: write tokens for the example tree.
3. Explain why null markers are required for unique reconstruction in preorder encoding.
4. Iterator drill: describe what data must be stored to resume inorder traversal after yielding one value.
5. Euler tour drill: list ENTER and EXIT events for each node; derive preorder and postorder from those events.

---

## Level 6 drills (Morris)

1. Inorder Morris: identify the inorder predecessor of A and B in the example tree.
2. Step-by-step: list when a thread is created and when it is removed.
3. Safety drill: list 3 scenarios where Morris is unsafe (shared structure, exceptions/early return, concurrency).
4. Restoration drill: describe how you would guarantee restoration even if a match is found early.

---

## Level 7+ drills (segment tree / BIT / trie traversal mechanics)

### Segment tree

1. For a range-sum query [l, r], classify nodes into: no overlap / total / partial.
2. Explain why partial overlap requires exploring both children.
3. Lazy propagation drill: describe when you *push* a lazy tag (traversal-driven).

### Fenwick (BIT)

1. For i=13 (binary 1101), list indices visited by prefix-sum walk `i -= lowbit(i)`.
2. For update at i=13, list indices visited by `i += lowbit(i)` until n.
3. Explain lowbit in words (what it removes/adds from the index).

### Trie

1. Given words: ["to", "tea", "ted"], draw the trie and list DFS-emitted words.
2. If children are processed in sorted order, explain why output is lexicographic.
3. Add an END marker rule: what exactly is the visit event?
