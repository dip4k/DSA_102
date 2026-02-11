# Stack Mastery — Drills (Level 1→6) | Lenient

Last updated: 2026-02-10

**How to use:** Do 10–20 minutes per level.  
Rule: write the invariant before you solve.

---

## Level 1 drills (LIFO reflex)

1. Dry-run: push 1,2,3; pop twice; push 4; peek; pop until empty.
2. Design: implement lenient `pop()` and `peek()` behavior on empty.
3. Trace: show stack after each operation for sequence: push a, push b, pop, push c, peek.

---

## Level 2 drills (control flow)

1. Parentheses: validate `()[]{}`, `([)]`, `((()))`, and empty string.
2. Recursion-to-stack: explain how factorial recursion maps to a stack of frames.
3. DFS iterative: given a small graph, list the stack after each push/pop.

---

## Level 3 drills (monotonic)

Use array: `[2, 1, 2, 4, 3]`.

1. Next greater to right: compute answers and show stack state at each index.
2. Next smaller to right: repeat.
3. Daily temperatures-style: compute “days until warmer” and explain why indices are required.
4. Duplicate tie-break drill: use `[2,2,2]` and decide your inequality rules.

---

## Level 4 drills (compositions)

1. Min Stack: simulate pushes: 3,5,2,2,4; after each push, write current min.
2. Queue via stacks: simulate push 1,2,3; pop; push 4; pop twice; peek.
3. Two-stack evaluation: evaluate `3 + 2 * 5` by listing stacks after each token.

---

## Level 5 drills (consume + reduce)

1. Remove adjacent duplicates: `abbaca`.
2. Simplify path: `/a/./b/../../c/`.
3. Decode string: `3[a2[c]]`.

For each: write the reduction rule; show stack after each token.

---

## Level 6 drills (range contribution)

1. For `[3,1,2,4]`, compute NSL/NSR for each index (choose strictness) and compute contribution counts.
2. Largest rectangle in histogram: for `[2,1,5,6,2,3]`, compute previous smaller and next smaller boundaries.
