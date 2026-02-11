# 🧱 Flow‑Wise Stacks + Queues Mastery (v1)

**Goal:** Build professional-grade intuition for stack/queue **traversal patterns**: LIFO/FIFO discipline, monotonic structures, window frontiers, and parsing stacks.

**Format standard:** each pattern uses **Why / What / How / Where / When**, plus pitfalls + a one-line invariant (Flow‑Wise style). 

---

# 🧭 One‑page Level Mapping Index (Stacks + Queues)

## Legend
- **Invariant** = what must remain true after each operation.
- **Frontier** = the data structure that represents “next to process”.

| Level | Pattern / skill | Canonical drills | Expected invariant (one‑liner) |
|---|---|---|---|
| L1 | Core operations + discipline | Valid parentheses, MinStack, Queue via stacks | “Stack/queue content exactly matches the unresolved frontier.” |
| L2 | Monotonic stack (next/prev greater/smaller) | Daily Temperatures, NSE/PSE, NGE circular | “Indices in stack are monotonic by value; top is nearest candidate.” |
| L3 | Range aggregation via monotonic stack | Histogram, Sum of subarray minimums | “When an index pops, its span boundaries are finalized.” |
| L4 | Monotonic deque (sliding window) | Window max/min | “Deque stores candidates in order; front is always the answer.” |
| L5 | Deque + prefix sums | Shortest subarray sum ≥ K | “Prefix indices in deque are increasing; prefixes are strictly useful.” |
| L6 | Parsing stacks (shunting-yard) | Infix → postfix, postfix eval | “Operator stack preserves precedence/associativity correctness.” |

---

# L1 — Physical Layer: LIFO/FIFO as a frontier

## L1.1 Stack (LIFO)
**Why:** You need “last opened must close first” and “undo/backtrack” mechanics.

**What:** A stack stores items where the most recent item is processed first.

**How (step/flow):**
1) Push when you encounter an opening / new unresolved item.
2) Pop when you encounter its matching close / resolution.

**Where:** parentheses matching, backtracking, DFS simulation, expression parsing.

**When:** whenever constraints are nested or reverse-order resolution is required.

**Common pitfalls**
- Popping from an empty stack (always guard).
- Storing values when you needed indices (monotonic patterns almost always store indices).

---

## L1.2 Queue (FIFO)
**Why:** You need “process in arrival order” (wavefront / level order / buffering).

**What:** A queue stores items processed in the same order they are inserted.

**How (step/flow):**
1) Enqueue to add work.
2) Dequeue to process the oldest work.

**Where:** BFS, producer-consumer buffers, level snapshots, scheduling.

**When:** whenever “first seen should be processed first”.

---

# L2 — Monotonic Stack: next/prev greater/smaller

## L2.1 Next Greater Element (NGE) / Next Smaller Element (NSE)
**Why:** Many “nearest to left/right” queries can be answered in one pass.

**What:** Maintain a stack of indices where values are monotonic.

**How (typical right-scan for NGE):**
1) Iterate i from left to right.
2) While stack not empty and a[i] > a[stack.top], pop j and set answer[j]=i.
3) Push i.

**Invariant:** Stack indices are strictly decreasing by value (for NGE scan).

**Where:** daily temperatures, stock span, trapping rainwater components.

**Pitfalls**
- Off-by-one on boundaries.
- Using <= vs < changes how duplicates behave; define it explicitly.

---

## L2.2 Circular NGE
**Why:** Array wraps around (circular next greater).

**What:** Run the scan twice (simulate wrap) and only set answers once.

**How:** iterate i in 0..2n-1, use i%n as index.

---

# L3 — Range contributions (monotonic stack)

## L3.1 Largest Rectangle in Histogram
**Why:** Each bar becomes the minimum height over some maximal span.

**What:** Find previous smaller and next smaller boundaries.

**How:** Use increasing stack; when a bar drops, pop and finalize rectangle area.

**Invariant:** When index k pops, you now know its maximal span (left boundary is new top, right boundary is i-1).

---

## L3.2 Sum of Subarray Minimums
**Why:** Each element contributes as the minimum of some number of subarrays.

**What:** Contribution = value * (#choices left) * (#choices right).

**How:** Use PLE (previous less) and NLE (next less-or-equal) with monotonic stacks.

**Pitfall:** Duplicate handling must be consistent to avoid double counting.

---

# L4 — Monotonic Deque: sliding windows

## L4.1 Sliding Window Maximum
**Why:** You need max for every window in O(n).

**What:** A deque of indices kept in decreasing value order.

**How (step/flow):**
1) Pop back while new value is >= back’s value.
2) Push new index.
3) Pop front if it’s out of window.
4) Front is the window max.

**Invariant:** Deque values are decreasing; indices are increasing; front is always best.

**Pitfalls**
- Removing out-of-window too late.
- Using values instead of indices (you can’t expire properly).

---

# L5 — Deque + prefix sums (advanced queue traversal)

## L5.1 Shortest subarray with sum ≥ K
**Why:** You need minimal length with constraint; brute force is too slow.

**What:** Use prefix sums P[i]; want smallest (i-j) with P[i]-P[j] ≥ K.

**How:** Maintain deque of candidate j with increasing prefix sums.

**Invariant:** Prefix sums at deque indices are strictly increasing (dominated prefixes removed).

---

# L6 — Parsing stacks (shunting-yard + evaluation)

## L6.1 Infix → Postfix (Shunting-yard)
**Why:** Postfix can be evaluated with a single stack.

**What:** Operator stack + output list.

**How (step/flow):**
1) Numbers go to output.
2) Operators pop higher/equal precedence ops from stack (respect associativity).
3) '(' pushes; ')' pops until '('.

**Invariant:** Operator stack is ordered so that popped order respects precedence.

---

## L6.2 Postfix evaluation
**Why:** Clean evaluation model: operator consumes last two values.

**How:** Scan tokens; push numbers; on operator pop b, pop a, push (a op b).

---

# Practice rule (very important)
For every stack/queue problem, write a one-line contract at the top:
- **Frontier:** what does the stack/queue represent right now?
- **Metadata:** what extra arrays/state exist (indices, prefix sums, precedence)?
- **Invariant:** what must be true after each push/pop?
