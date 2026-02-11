# Tree Traversal — Visual Playbook (Binary Trees) | Levels 1→7 (Lenient)

**Scope:** Binary trees only for Levels 1–6.  
**Optional Level 7+:** traversal mechanics that commonly appear in interviews once range/prefix DS show up (Segment Tree / Fenwick(BIT) / Trie).  
**Contract:** **LENIENT** — empty tree ⇒ empty output; ambiguous ordering variants must declare tie-break rules.

---

## Visual legend (read this once)

```text
[ ]  = node (value)
→    = pointer/edge direction
S:   = stack (top on the right)
Q:   = queue (front on the left)
V()  = visit event (record/emit node)
```

Traversal always = **Frontier + Child order + Visit event**.

---

## The master map (Levels 1→7)

```text
Tree Traversal Mastery
├─ L1 Physical: define orders + safe visiting
│  ├─ DFS: Pre / In / Post
│  └─ BFS: Level-order
├─ L2 Structural: explicit control of state
│  ├─ Iterative DFS (stack)
│  ├─ Inorder stack discipline
│  └─ Postorder without recursion (stateful stack)
├─ L3 Stateful: layers + aggregation + views
│  ├─ Level grouping, zigzag
│  ├─ Per-level metrics
│  └─ Left/Right view (BFS/DFS)
├─ L4 Derived projections: same tree, different “camera angles”
│  ├─ Vertical (columns), Top/Bottom
│  ├─ Diagonal
│  └─ Boundary
├─ L5 Abstract: traversal as a tool
│  ├─ Serialization (pre+null, level+null)
│  ├─ Iterators (yield one node at a time)
│  └─ Euler tour events (enter/exit)
├─ L6 Space-optimized: Morris (thread + restore)
└─ L7+ DS traversal mechanics
   ├─ Segment tree overlap recursion
   ├─ Fenwick (BIT) lowbit index-walk
   └─ Trie DFS enumeration
```

---

## Shared example tree (use for all traces)

```text
        A
      /   \
     B     C
    / \     \
   D   E     F

Convention: Left child before Right child.
```

---

## Level 1 — Physical layer (core orders)

### L1.1 DFS Preorder (Root, Left, Right)

**Visit event:** on arrival. (Root-first)  
Definition: Root-Left-Right.  

```text
Preorder walk (V on arrival)

Start at A: V(A)
Go left:  V(B)
Go left:  V(D)
Back:     V(E)
Go right: V(C)
Go right: V(F)

Output: A B D E C F
```

Frontier intuition (implicit recursion stack):

```text
Call stack snapshot (conceptual)
A
└─ B
   └─ D   (returns)
   └─ E   (returns)
└─ C
   └─ F
```

### L1.2 DFS Inorder (Left, Root, Right)

**Visit event:** after finishing left subtree.  
Definition: Left-Root-Right.  

```text
Inorder (V when “coming back from left”)

Leftmost first: D
Then B
Then E
Then A
Then C
Then F

Output: D B E A C F
```

“Visit moment” marker:

```text
For node B:
- Traverse left subtree (D)
- V(B)
- Traverse right subtree (E)

That middle moment is the inorder signature.
```

### L1.3 DFS Postorder (Left, Right, Root)

**Visit event:** after both subtrees.  

```text
Postorder (V on exit)

Finish D and E, then visit B
Finish F, then visit C
Finish B and C, then visit A

Output: D E B F C A
```

### L1.4 BFS Level-order (by depth)

BFS explores level-by-level using a queue (level-order traversal).  

```text
Q: [A]
Pop A, push children → Q: [B, C]
Pop B, push D,E     → Q: [C, D, E]
Pop C, push F       → Q: [D, E, F]
Pop D               → Q: [E, F]
Pop E               → Q: [F]
Pop F               → Q: []

Level groups: [A] | [B,C] | [D,E,F]
```

---

## Level 2 — Structural layer (iterative control)

### L2.1 Iterative Preorder (explicit stack)

Rule: push **right** then **left** so left pops first.

```text
S: [A]
Pop A => V(A), push C then B  → S: [C, B]
Pop B => V(B), push E then D  → S: [C, E, D]
Pop D => V(D)                 → S: [C, E]
Pop E => V(E)                 → S: [C]
Pop C => V(C), push F         → S: [F]
Pop F => V(F)                 → S: []

Output: A B D E C F
```

### L2.2 Iterative Inorder (stack = path of unfinished ancestors)

Core loop:
- Push left chain.
- Pop = visit.
- Go right.

```text
Start cur=A, S=[]
Push A,B,D  → S:[A,B,D], cur=null
Pop D → V(D), cur=D.right=null
Pop B → V(B), cur=E
Push E → S:[A,E], cur=null
Pop E → V(E), cur=null
Pop A → V(A), cur=C
Push C → S:[C], cur=null
Pop C → V(C), cur=F
Push F → S:[F], cur=null
Pop F → V(F)

Output: D B E A C F
```

### L2.3 Iterative Postorder (one stack + lastVisited)

Postorder is hardest iteratively because the visit is on **exit**.

State you track:
- stack of nodes
- `lastVisited` (which subtree you just finished)

```text
Idea picture (not full trace):

If top = X
- If X has an unvisited left → go left
- Else if X has an unvisited right → go right
- Else V(X), pop, lastVisited=X

This enforces: “visit only after both children are done.”
```

### L2.4 Parent pointers (avoid ping-pong)

If each node has `.parent`, naive walking can bounce forever:

```text
B ↔ A
If you always consider parent as a neighbor, you created a graph.

Safe trick: keep prev pointer
(prev, cur) determines whether you came from parent/left/right.
```

---

## Level 3 — Stateful layer (layers, zigzag, and views)

### L3.1 BFS per-level grouping (queue size snapshot)

```text
Q:[A]
levelSize=1 → output [A]
Q becomes [B,C]
levelSize=2 → output [B,C]
Q becomes [D,E,F]
levelSize=3 → output [D,E,F]
```

### L3.2 Zigzag / Spiral

Visual rule:

```text
Level 0: left → right  : [A]
Level 1: right → left  : [C, B]
Level 2: left → right  : [D, E, F]
```

Two implementation mental models:

```text
Model 1: BFS levels then reverse odd levels.
Model 2: deque frontier; depending on direction, pop/push ends.
```

### L3.3 Right side view

BFS view idea:

```text
At each level, the last node processed is visible from the right.
Level 0 last = A
Level 1 last = C
Level 2 last = F
Right view: [A, C, F]
```

DFS view idea:

```text
Do DFS with priority Right before Left.
First time you reach depth d, record that node.
```

---

## Level 4 — Derived projections (vertical / diagonal / boundary)

### L4.1 Coordinate model for vertical order

Assign each node:
- row = depth
- col = horizontal column (left = -1, right = +1)

```text
(A) row0 col0
(B) row1 col-1
(C) row1 col+1
(D) row2 col-2
(E) row2 col0
(F) row2 col+2
```

Column buckets:

```text
col -2: [D]
col -1: [B]
col  0: [A, E]
col +1: [C]
col +2: [F]
```

Tie-break contract (state this in interviews):
- sort by col, then row; if still tied, keep stable left-to-right discovery order.

### L4.2 Top view vs bottom view

```text
Top view = first (smallest row) per col
Bottom view = last (largest row) per col

For this tree:
Top    : [D, B, A, C, F]
Bottom : [D, B, E, C, F]
(ordered by increasing col)
```

### L4.3 Diagonal traversal (one common convention)

Convention example:
- Right child stays on same diagonal
- Left child goes to next diagonal

```text
Diagonal 0: A, C, F
Diagonal 1: B, E
Diagonal 2: D
```

### L4.4 Boundary traversal (anti-clockwise)

Decompose boundary:

```text
Left boundary (exclude leaves): A, B
Leaves left→right: D, E, F
Right boundary (exclude leaves, reverse): C

Boundary: A B D E F C
```

Lenient edge case: single node → [A]

---

## Level 5 — Abstract layer (serialization, iterators, Euler events)

### L5.1 Serialization: preorder + null markers

Idea: record null children explicitly so shape is reconstructible.

```text
Preorder with # for null:
A, B, D, #, #, E, #, #, C, #, F, #, #

Read it as:
V(A)
  V(B)
    V(D)
      #
      #
    V(E)
      #
      #
  V(C)
    #
    V(F)
      #
      #
```

### L5.2 Serialization: level-order + null markers

```text
Level-order with #:
A, B, C, D, E, #, F, #, #, #, #, #, #

(Exact trailing # policy varies; you can trim trailing nulls if your decoder agrees.)
```

### L5.3 Iterator mindset (inorder iterator)

State machine:

```text
Iterator state = (stack, cur)

hasNext(): cur != null OR stack not empty
next(): push-left chain; pop; visit; set cur = popped.right
```

### L5.4 Euler tour events (enter/exit)

```text
Every node X has events:
ENTER(X)
  ... left subtree ...
IN(X)   (the inorder moment)
  ... right subtree ...
EXIT(X)

Preorder  = log ENTER
Inorder   = log IN
Postorder = log EXIT
```

---

## Level 6 — Space-optimized (Morris traversal)

Morris traversal performs inorder without recursion/stack by creating temporary links (“threads”) and restoring them afterward.  

### L6.1 Morris inorder: thread → traverse → restore

Key picture:

```text
Goal: when at cur=A, you want to return to A after finishing left subtree.

Find predecessor of A in left subtree = rightmost node in A.left.
Here predecessor is E (because B.right = E, and E.right is null).

Thread it:
E.right → A   (temporary)

Now you can walk left subtree, and later when you hit E again,
you follow E.right back to A, then restore E.right=null.
```

Safety rule (must say in interviews):

```text
Invariant: Every thread you create is removed exactly once.
Avoid early returns that skip restoration.
```

---

## Level 7+ — DS-specific traversal mechanics (optional but interview-useful)

These are “traversal thinking” in disguise.

### L7.1 Segment tree query = overlap-driven recursion

```text
Each node covers a segment [L..R]. Query is [i..j].

Cases:
1) No overlap      → return identity (e.g., 0 for sum, +inf for min)
2) Total overlap   → return node.value
3) Partial overlap → recurse left + right, combine

This is traversal: the query walks only the parts of the tree that matter.
```

Mini-visual:

```text
                 [0..7]
           /                \
        [0..3]             [4..7]
      /       \           /       \
   [0..1]   [2..3]     [4..5]   [6..7]

Query [2..6] hits:
- partial at [0..7]
- partial at [0..3] → total at [2..3]
- partial at [4..7] → total at [4..5] and partial at [6..7]
```

### L7.2 Fenwick (BIT) traversal = lowbit index-walk

Two walks:

```text
Prefix sum(i):
while i > 0:
  ans += bit[i]
  i -= lowbit(i)

Update(i, delta):
while i <= n:
  bit[i] += delta
  i += lowbit(i)

lowbit(i) = i & (-i)
```

Example walk (indices only):

```text
i = 13 (1101b)
lowbit(13)=1  → 13 → 12
lowbit(12)=4  → 12 → 8
lowbit(8) =8  → 8  → 0
Visited: 13, 12, 8
```

### L7.3 Trie DFS enumeration (words = root-to-end markers)

```text
Words: to, tea, ted

(root)
  t
   └─ o (END)
   └─ e
       └─ a (END)
       └─ d (END)

DFS rule: visit END markers as “emit a word”.
If children are processed in sorted order, output is lexicographic.
```

---

## Common failure modes (WRONG → CORRECT)

### 1) Wrong visit timing (DFS)

```text
WRONG: claiming inorder but visiting on arrival
Result: you produced preorder.

CORRECT: inorder visits after finishing left subtree.
```

### 2) Iterative preorder push order

```text
WRONG: push left then right
Stack pops right first → traversal becomes Root, Right, Left.

CORRECT: push right first, then left.
```

### 3) Morris early-return bug

```text
WRONG: return once you “found target” without restoring threads.
Tree stays mutated.

CORRECT: either (a) avoid early return, (b) track restoration, or (c) don’t use Morris.
```

### 4) Vertical/top/bottom view tie-break ambiguity

```text
WRONG: ignore ties (same col, same depth)
May fail hidden tests.

CORRECT: declare contract: col asc, depth asc, stable BFS order (or value sort if problem demands).
```

---

## Quick quiz (no answers)

1. If you log ENTER events, which traversal order do you get? How about EXIT events?
2. In iterative inorder, what exactly does the stack represent at any moment?
3. Why does pushing right before left preserve preorder output in a stack-based traversal?
4. For right-side view, why does “first node at depth” work if DFS prioritizes right-first?
5. In boundary traversal, why must you exclude leaves from left/right boundaries?
6. In Morris inorder, what is the inorder predecessor and why is it the correct threading target?
7. Segment tree query: give an example range where a node has partial overlap and explain why both children must be visited.
8. BIT prefix sum walk: why does subtracting lowbit eventually reaches 0 in O(log n)?
9. Trie enumeration: what is the visit event, and what ordering constraint yields lexicographic output?

---

## Complexity reference (fast lookup)

```text
Traversal / Pattern                  Time        Extra space (typical)
Pre/In/Post recursive                O(n)        O(h) call stack
Pre/In/Post iterative                O(n)        O(h) explicit stack
Level-order BFS                       O(n)        O(w) queue (w=max width)
Vertical/diagonal grouping             O(n)        O(n) maps/lists
Serialize preorder+null                O(n)        O(h) (encoder stack)
Morris inorder                         O(n)        O(1) extra space
Segment tree range query               O(log n)*   O(log n) recursion (*depends on overlap)
Fenwick prefix/update                  O(log n)    O(1) extra space
Trie DFS enumerate all words           O(total chars) O(depth)
```

---

## References (for definitions and key claims)

- Core DFS/BFS traversal definitions: GeeksforGeeks — *Tree Traversal Techniques* https://www.geeksforgeeks.org/dsa/tree-traversals-inorder-preorder-and-postorder/
- Pre/In/Post order patterns: GeeksforGeeks — *Preorder vs Inorder vs Postorder* https://www.geeksforgeeks.org/dsa/preorder-vs-inorder-vs-postorder/
- General taxonomy: Wikipedia — *Tree traversal* https://en.wikipedia.org/wiki/Tree_traversal
- Morris idea (temporary links + restore): GeeksforGeeks — *Reverse Morris traversal…* https://www.geeksforgeeks.org/dsa/reverse-morris-traversal-using-threaded-binary-tree/
- Segment tree overlap cases: Rust Programming — *Range Query in Segment Tree* https://rustp.org/array-algorithms/range-query-segment-tree/
