# Stack Mastery — Flow‑Wise Roadmap (Level 1→6) | Lenient

**Goal:** Interview-ready stack intuition: not just “push/pop”, but recognizing when a problem *is* a stack problem.

**Contract:** **LENIENT**

- `pop()` on empty ⇒ return `None` / “no value” (or language equivalent) and do not crash.
- `peek()` on empty ⇒ return `None`.
- For algorithm problems: empty input ⇒ empty output.

Last updated: 2026-02-10

---

## One unified mental model

A stack is a **Last‑In‑First‑Out (LIFO)** container: the most recently pushed element is the first one popped.  
In practice, this makes a stack perfect for: “undo”, “backtrack”, “nested structure”, and “nearest previous unresolved thing.”

Core operations (conceptual):

- **Push**: add to top.
- **Pop**: remove from top.
- **Peek/Top**: read top without removing.
- **isEmpty/size**.

---

## The ladder (same Level 1→5 style + optional Level 6)

| Level | Theme (complexity of state) | What changes in your brain |
|---|---|---|
| L1 | Physical layer: LIFO muscle memory | You can push/pop/peek and dry-run by hand.
| L2 | Structural layer: stack = controlled execution | You use stack to simulate recursion and manage “where to return”.
| L3 | Stateful layer: stack = constraint maintainer | You maintain monotonic constraints; stack stores indices/states.
| L4 | Derived layer: stack compositions | Two stacks, min-stack, queue-via-stacks, multi-stack invariants.
| L5 | Abstract layer: parsing + correctness contracts | Tokens, nesting, streaming, “consume + reduce” models.
| L6 | Optional advanced: contribution/range tricks | Monotonic stack for range contributions, sentinels, Cartesian thinking.

---

## Level 1 — Physical layer (operations and invariants)

### L1.1 What you must be able to do instantly

- Define LIFO and point to the “top”.
- Dry-run push/pop sequences.
- Explain underflow/overflow conceptually.

### L1.2 The invariant library (Level 1)

- After pushing x, `peek()` becomes x.
- After popping, the new top is the previous second-to-top.
- Stack top always represents the most recently *unresolved* element.

### L1.3 Minimal visual

```text
Push 1, Push 2, Push 3

Top
  |
 [3]
 [2]
 [1]

Pop() => 3

Top
  |
 [2]
 [1]
```

---

## Level 2 — Structural layer (simulate control flow)

### L2.1 Stack = “where do I return to?”

The call stack in programming is a stack of activation frames (function calls).  
This is why recursion is naturally modeled by stacks, and why you can convert recursion to an explicit stack.

### L2.2 Pattern: DFS/Tree traversal without recursion

Even if you’re not “doing stacks”, many traversal problems *are* stack problems:

- Tree preorder/inorder/postorder iterative
- Graph DFS iterative

**Core invariant:** stack holds the frontier of pending nodes; pushing order controls visitation order.

### L2.3 Pattern: matching nested structure

Classic: parentheses/brackets matching.

**Core invariant:** stack holds the opening tokens that have not been matched yet; a closing token must match the current top.

---

## Level 3 — Stateful layer (monotonic stack patterns)

This is where stacks become a “solver”, not a container.

### L3.1 Monotonic decreasing stack (Next Greater Element)

Problem shape:

- You want the **next greater to the right** for each element.

State choice:

- Stack keeps a decreasing sequence of candidates (often indices).

Invariant (say it):

- Stack is monotonic; elements that are smaller than the current element get resolved (popped) because the current element is their answer.

### L3.2 Variations (master the axis)

- Next greater to right / left
- Next smaller to right / left
- Previous greater/smaller

**Axis to choose:** traversal direction (left→right vs right→left) + monotonic type (increasing vs decreasing) + store (values vs indices).

### L3.3 “Span” problems

These look different but are the same skeleton:

- Stock Span
- Daily Temperatures

Invariant:

- Stack stores indices with a monotonic property so you can jump over useless days in one pop sequence.

---

## Level 4 — Derived layer (compositions)

### L4.1 Min Stack / Max Stack

Two canonical designs:

- Two stacks: data stack + min stack (track minima).
- One stack of pairs: (value, currentMin).

Invariant:

- min-structure top equals min of all elements currently in the data stack.

### L4.2 Queue using stacks

Use two stacks:

- `in`: for pushes
- `out`: for pops/peeks

Invariant:

- If `out` is non-empty, its top is the queue front.
- If `out` is empty, pour everything from `in` to `out` to reverse order.

### L4.3 Two-stack evaluation (reduce rules)

A common interview direction is expression evaluation:

- value stack
- operator stack

Invariant:

- operator stack respects precedence; reductions happen when an incoming operator can’t be delayed.

---

## Level 5 — Abstract layer (parsing and correctness contracts)

### L5.1 Parsing as “consume and reduce”

Many problems can be re-framed as:

1. Read next token.
2. Push it.
3. While a rule becomes applicable, pop/merge.

Examples:

- Remove adjacent duplicates
- Decode string
- Simplify path

Invariant:

- Stack represents the current reduced prefix; nothing below the top will be changed again unless a higher-level reduction triggers.

### L5.2 Design your contract (lenient)

Before coding, state:

- What happens on empty input?
- What happens on invalid tokens?
- For parsing: do you return partial output or fail?

---

## Level 6 (Optional) — Advanced monotonic stack: ranges and contributions

This is the “pro” layer: turning monotonic boundaries into math.

### L6.1 Range contribution idea

If you know for each index `i`:

- nearest smaller element to left (NSL)
- nearest smaller element to right (NSR)

Then you can compute how many subarrays have `arr[i]` as the minimum, and accumulate contributions.

### L6.2 Sentinels and tie-breaking

To avoid edge cases, you often add sentinel values or sentinel indices.

Tie-break rule to state explicitly:

- For duplicates, decide whether “strictly smaller” or “smaller or equal” defines your boundary.

---

## Common failure modes

- Pushing values when you needed indices (you lose distance/span info).
- Using wrong inequality (`<` vs `<=`) and failing duplicates cases.
- Forgetting to flush the stack at the end (some elements never get resolved).
- For queue-via-stacks, pouring on every operation (turns amortized O(1) into O(n)).

---

## Reference links

- Stack is LIFO and core operations (push/pop/peek): https://www.geeksforgeeks.org/dsa/introduction-to-stack-data-structure-and-algorithm-tutorials/
- Fundamental stack operations list: https://www.geeksforgeeks.org/dsa/basic-operations-in-stack-data-structure-with-implementations/
- Next Greater Element via monotonic stack (O(n) idea): https://www.geeksforgeeks.org/dsa/next-greater-element/
