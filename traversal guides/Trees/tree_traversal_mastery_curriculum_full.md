# 🌳 Traversal Mastery Curriculum — Trees (Binary + N-ary + Variants)

You are my Data Structures and Algorithms instructor and coach.

**Goal:** Achieve mastery in traversal and navigation for **trees**, building deep intuition for how to walk them correctly and efficiently.

**Scope:** Binary Trees, BSTs, N-ary Trees, Heaps/Implicit Array Trees, Tries (prefix trees), and “implicit trees” (search spaces).

---

## ✅ Summary list
- 🧠 Unifying model: **Frontier (stack/queue) + Visit timing + State**
- 🌿 DFS: preorder / inorder / postorder, Euler tour, path-state DFS
- 🧺 BFS: level-order, zigzag, multi-source-like patterns on trees (multiple roots/forests), views
- 🧱 Traversal variants: boundary, vertical order, top/bottom view, diagonal
- 🧵 Space/structure tricks: iterative frames, Morris (binary, O(1) auxiliary, restore threads)
- 🌿 N-ary enhancement: preorder/postorder/BFS with ordered children
- 🧮 Index navigation: heap-style arrays (parent/child index formulas)
- 🧪 Debugging: invariants + micro-tracing logs that pinpoint ordering/state bugs

---

# Level 1 🟢 Physical movement (tree walking)

## 1.1 Vocabulary → movement primitives

### Why
If “depth/level/subtree/leaf” is fuzzy, traversal becomes memorization and bugs multiply.

### What
- **Depth/level:** number of edges from root.
- **Height:** edges to deepest leaf.
- **Subtree(u):** u plus all descendants.
- **Leaf:** node with no children.

### How (step/flow)
1) Start at root.
2) At each node, you can move to children (down) or backtrack (up).
3) Your traversal order is defined by *which next move you choose*.

### Where and When (use cases)
- File systems, org charts, DOM/AST, menus.

### Common pitfalls
- Mixing “level(root)=0” vs “level(root)=1” across functions.

### Tips and tricks
- Choose `level(root)=0` and stay consistent.

Visual:
```text
        A (depth 0)
      /   \
     B     C (depth 1)
    / \     \
   D   E     F (depth 2)
```

---

## 1.2 The traversal contract (the 3 questions)

### Why
Every traversal is just a different answer to the same control questions.

### What
Ask these before coding:
1) **Frontier:** stack vs queue?
2) **Visit timing:** ENTER vs BETWEEN vs EXIT?
3) **State:** do I need depth/parent/phase/column?

### How (step/flow)
1) Write the contract as a sentence.
2) Dry-run a 6-node tree.
3) Only then write code.

### Where and When (use cases)
- Any traversal variant (views, vertical order, boundary).

### Common pitfalls
- Not defining child order (left-to-right), causing “same nodes, wrong order.”

### Tips and tricks
- Always declare child order: **binary = left then right**, **N-ary = children list order**.

Visual:
```text
Contract examples:
- Preorder: VISIT on ENTER; children left->right
- BFS level order: VISIT on dequeue; process by level snapshots
```

---

## 1.3 Recursive DFS skeleton (binary baseline)

### Why
Recursion matches tree structure and is the fastest way to get correct traversal logic.

### What
A reusable template with an explicit base case and a clear visit moment.

### How (step/flow)
1) If node is null: return.
2) Do PRE work (optional).
3) Recurse left.
4) Do IN work (optional).
5) Recurse right.
6) Do POST work (optional).

### Where and When (use cases)
- Traversal lists, subtree checks, small/medium depth trees.

### Common pitfalls
- Forgetting the null base case.

### Tips and tricks
- Think: “dfs(node) fully solves node’s subtree.”

Python (template):
```python
def dfs(node):
    if not node:
        return
    # PRE
    dfs(node.left)
    # IN
    dfs(node.right)
    # POST
```

C# (template):
```csharp
void Dfs(TreeNode? node)
{
    if (node == null) return;
    // PRE
    Dfs(node.left);
    // IN
    Dfs(node.right);
    // POST
}
```

---

### ✅ Level 1 invariants checklist
- Base case handles null safely.
- Child order is explicitly defined.
- “Visit timing” matches the traversal you intended.

### 🐛 Level 1 mini debugging guide
- Print/log: `nodeValue` + `event (ENTER/EXIT)` + `depth`.
- Wrong order but correct set of nodes → visit timing bug.
- Missing nodes → null handling / early return / incorrect child link.

---

# Level 2 🔵 Index/pointer navigation (representations)

## 2.1 Pointer-based nodes (binary & N-ary)

### Why
Most real trees are pointer/node based; traversal is pointer walking.

### What
- Binary node: `left`, `right`.
- N-ary node: `children[]`.

### How (step/flow)
1) Define the node type.
2) Decide child iteration order.
3) Keep traversal logic independent from the data stored.

### Where and When (use cases)
- ASTs, DOM, tries, org charts.

### Common pitfalls
- Iterating children in non-deterministic order (e.g., hash-based sets).

### Tips and tricks
- Use arrays/lists for children when output order matters.

Python node examples:
```python
class BNode:
    def __init__(self, val, left=None, right=None):
        self.val = val
        self.left = left
        self.right = right

class NNode:
    def __init__(self, val, children=None):
        self.val = val
        self.children = children or []
```

C# node examples:
```csharp
public class BNode
{
    public int Val;
    public BNode? Left;
    public BNode? Right;
    public BNode(int val, BNode? left=null, BNode? right=null)
    { Val = val; Left = left; Right = right; }
}

public class NNode
{
    public int Val;
    public List<NNode> Children = new();
    public NNode(int val) { Val = val; }
}
```

---

## 2.2 Implicit array trees (heap-style) 🧮

### Why
Some trees are stored in arrays (complete binary trees): traversal becomes **index arithmetic**.

### What
For 0-based array `a`:
- left child index = `2*i + 1`
- right child index = `2*i + 2`
- parent index = `(i-1)//2`

### How (step/flow)
1) Start at index `0` (root).
2) Compute child indices.
3) Stop when index is out of bounds.

### Where and When (use cases)
- Heaps, priority queues, implicit complete trees.

### Common pitfalls
- Off-by-one errors (mixing 0-based vs 1-based formulas).

### Tips and tricks
- Write formulas at the top of your function.

Python (DFS over implicit heap indices):
```python
def preorder_heap(a, i=0, out=None):
    out = out or []
    if i >= len(a):
        return out
    out.append(a[i])
    preorder_heap(a, 2*i+1, out)
    preorder_heap(a, 2*i+2, out)
    return out
```

C# (child index helpers):
```csharp
static int Left(int i)  => 2*i + 1;
static int Right(int i) => 2*i + 2;
static int Parent(int i)=> (i - 1) / 2;
```

---

### ✅ Level 2 invariants checklist
- Representation rules are fixed (pointer vs array).
- Child iteration order is deterministic.
- Index formulas match the chosen indexing scheme.

### 🐛 Level 2 mini debugging guide
- Print/log for implicit trees: `(i, value, leftIndex, rightIndex)`.
- If traversal “skips” nodes → child index math or bounds check is wrong.

---

# Level 3 🟠 Multi-state traversal (iterative control)

## 3.1 DFS preorder / inorder / postorder (binary)

### Why
These are the canonical DFS visit-timing variants.

### What
- **Preorder:** Root → Left → Right
- **Inorder:** Left → Root → Right (binary only)
- **Postorder:** Left → Right → Root

### How (step/flow)
- Preorder: VISIT on ENTER.
- Inorder: VISIT after finishing left.
- Postorder: VISIT on EXIT.

### Where and When (use cases)
- Interview scenario: “Return traversal list in X order.”
- System scenario: “Serialize a tree” (preorder often) or “aggregate bottom-up” (postorder).

### Common pitfalls
- Accidentally using preorder when you need child results.

### Tips and tricks
- If a node’s value depends on children, your VISIT is postorder.

Python (three recursive traversals):
```python
def preorder(root, out):
    if not root: return
    out.append(root.val)
    preorder(root.left, out)
    preorder(root.right, out)

def inorder(root, out):
    if not root: return
    inorder(root.left, out)
    out.append(root.val)
    inorder(root.right, out)

def postorder(root, out):
    if not root: return
    postorder(root.left, out)
    postorder(root.right, out)
    out.append(root.val)
```

C# (preorder recursive):
```csharp
static void Preorder(BNode? node, List<int> outList)
{
    if (node == null) return;
    outList.Add(node.Val);
    Preorder(node.Left, outList);
    Preorder(node.Right, outList);
}
```

---

## 3.2 Iterative DFS with explicit stack (binary)

### Why
Avoid recursion depth limits and gain precise control.

### What
- Iterative preorder: stack of nodes.
- Iterative inorder: push-left chain.
- Iterative postorder: needs extra state (two stacks, lastVisited, or phase frames).

### How (step/flow)
1) Choose which order you need.
2) Write the **stack invariant**.
3) Pick the simplest correct technique:
   - preorder: push right then left
   - inorder: push-left then pop
   - postorder: phase frames (ENTER/EXIT)

### Where and When (use cases)
- Interview scenario: “Implement inorder iteratively.”
- System scenario: “Traverse very deep tree without stack overflow.”

### Common pitfalls
- Wrong push order (stack reverses).
- In postorder: visiting parent too early.

### Tips and tricks
- Phase frames remove guessing.

Python (postorder via phases):
```python
def postorder_iter(root):
    if not root: return []
    out = []
    stack = [(root, 0)]  # 0=ENTER, 1=EXIT
    while stack:
        node, phase = stack.pop()
        if not node:
            continue
        if phase == 0:
            stack.append((node, 1))
            stack.append((node.right, 0))
            stack.append((node.left, 0))
        else:
            out.append(node.val)
    return out
```

C# (phase idea):
```csharp
// Stack holds (node, phase). ENTER schedules children, EXIT outputs.
```

---

## 3.3 BFS level-order (binary and N-ary)

### Why
BFS is the “by depth” traversal and a base for zigzag, views, per-level stats.

### What
Queue traversal with **level snapshots** (wave processing).

### How (step/flow)
1) Enqueue root.
2) While queue not empty:
   - snapshot `levelSize`.
   - pop exactly levelSize nodes.
   - enqueue their children.

### Where and When (use cases)
- Interview scenario: “Return list of levels.”
- System scenario: “Render a hierarchy level-by-level.”

### Common pitfalls
- Forgetting to snapshot levelSize (mixing levels).

### Tips and tricks
- Invariant: “queue contains the next frontier only.”

Python (level-order grouped):
```python
from collections import deque

def level_order(root):
    if not root: return []
    q = deque([root])
    res = []
    while q:
        level = []
        for _ in range(len(q)):
            node = q.popleft()
            level.append(node.val)
            if node.left: q.append(node.left)
            if node.right: q.append(node.right)
        res.append(level)
    return res
```

C# (level snapshot concept):
```csharp
// levelSize = queue.Count; pop exactly levelSize nodes; enqueue their children.
```

---

### ✅ Level 3 invariants checklist
- Stack/queue contents are precisely defined (frontier only).
- Push/enqueue order matches the required output order.
- Phase/state transitions are consistent (no missing EXIT).

### 🐛 Level 3 mini debugging guide
- DFS: log `(node, phase, stackSize)`.
- BFS: log `(level, levelSize, queueSnapshot)`.

---

# Level 4 🟣 Range/path traversal (coordinates, boundaries, paths)

## 4.1 Root-to-leaf path traversal (path-state DFS) 🧳

### Why
Many tasks are about **paths**, not individual nodes.

### What
DFS with a mutable `path` list that mirrors recursion depth.

### How (step/flow)
1) ENTER: path.append(node)
2) If leaf: consume path (record/check)
3) Recurse children
4) EXIT: path.pop()

### Where and When (use cases)
- Interview scenario: “Return all root-to-leaf paths.”
- System scenario: “Compute breadcrumb navigation for each leaf page.”

### Common pitfalls
- Forgetting to pop (path leaks into siblings).

### Tips and tricks
- Append/pop must be paired in the same function.

Python (path skeleton):
```python
def dfs_path(node, path, out):
    if not node: return
    path.append(node.val)
    if not node.left and not node.right:
        out.append(list(path))
    else:
        dfs_path(node.left, path, out)
        dfs_path(node.right, path, out)
    path.pop()
```

---

## 4.2 Euler tour (ENTER/EXIT times) ⏱️

### Why
Turns subtree questions into contiguous ranges (useful for queries).

### What
Record `tin[node]` at ENTER and `tout[node]` at EXIT.

### How (step/flow)
1) On ENTER: assign time; record tin.
2) Traverse children.
3) On EXIT: assign time; record tout.

### Where and When (use cases)
- Interview scenario: “Is u ancestor of v?” using time intervals.
- System scenario: “Subtree analytics” (aggregate a subtree via a range).

### Common pitfalls
- Recording only one timestamp.

### Tips and tricks
- Lifecycle thinking: ENTER = discovery, EXIT = completion.

Python (tin/tout skeleton):
```python
time = 0

def euler_times(node, tin, tout, depth=0):
    global time
    if not node: return
    tin[node] = time; time += 1
    euler_times(node.left, tin, tout, depth+1)
    euler_times(node.right, tin, tout, depth+1)
    tout[node] = time; time += 1
```

---

## 4.3 Vertical order, top/bottom view (coordinate BFS) 🧊

### Why
Many “view/projection” problems are traversals + coordinates.

### What
Assign a **column (col)**:
- root col = 0
- left col - 1
- right col + 1

### How (step/flow)
1) BFS with items `(node, col)`.
2) Bucket by col.
3) For top view: keep first per col; bottom view: keep last per col.

### Where and When (use cases)
- Interview scenario: “Vertical order traversal.”
- System scenario: “Layout tree nodes into columns.”

### Common pitfalls
- Not defining tie-break rules when multiple nodes share (row, col).

### Tips and tricks
- If strict ordering is required, also track `row=depth` and sort.

Visual:
```text
        A(0)
       /   \
   B(-1)   C(+1)
     \
     D(0)

Columns:
-1: [B]
 0: [A, D]
+1: [C]
```

---

## 4.4 Boundary traversal (composite traversal) 🧱

### Why
Trains precision: compose multiple walks while avoiding duplicates.

### What
Boundary (anti-clockwise):
- root
- left boundary (excluding leaves)
- all leaves (left-to-right)
- right boundary (excluding leaves, reversed)

### How (step/flow)
1) Add root.
2) Walk left boundary.
3) Collect leaves with DFS.
4) Walk right boundary; reverse.

### Where and When (use cases)
- Interview scenario: “Print boundary of binary tree.”
- System scenario: “Perimeter highlight in visualization.”

### Common pitfalls
- Duplicating leaves.

### Tips and tricks
- Define `isLeaf(node)` once; use everywhere.

---

### ✅ Level 4 invariants checklist
- Any extra state (col/row/path) is updated consistently.
- Composite traversals prevent duplicates by design.
- Tie-break rules are explicit when required.

### 🐛 Level 4 mini debugging guide
- Log `(node, depth, col/diagonal, decision)`.
- If groups are right but order is wrong → tie-break / traversal-vs-sort mismatch.

---

# Level 5 🔴 Abstract search space traversal (implicit trees)

## 5.1 Implicit trees (backtracking/search)

### Why
Many problems *are trees* even when you don’t build nodes—each state generates children states.

### What
Examples:
- Generate permutations/subsets
- Game decision trees
- Parsing choices

### How (step/flow)
1) Define state.
2) Define “children” generator.
3) Choose DFS (enumerate) or BFS (min steps).
4) Track visited if states can repeat (then it’s a graph).

### Where and When (use cases)
- Interview scenario: “Generate all valid strings under constraints.”
- System scenario: “Explore decision outcomes in a rule engine.”

### Common pitfalls
- Treating repeated states as a tree (should be graph with visited).

### Tips and tricks
- Ask: “Can two paths produce the same state?” If yes, add visited.

---

## 5.2 Morris traversal (binary, O(1) auxiliary) 🧵

### Why
Advanced technique: inorder traversal without recursion/stack via temporary threading.

### What
Temporarily link inorder predecessor’s right pointer to the current node, then restore.

### How (step/flow)
1) If no left child: visit; go right.
2) Else find predecessor (rightmost of left subtree).
3) If predecessor.right is null: set it to current (thread) and go left.
4) Else: remove thread; visit; go right.

### Where and When (use cases)
- Interview follow-up: “O(1) extra space inorder.”
- Systems: rarely used; mostly educational/constraint-driven.

### Common pitfalls
- Forgetting to remove threads (corrupts tree).

### Tips and tricks
- Invariant: every created thread is later removed.

---

### ✅ Level 5 invariants checklist
- State transitions are correct and reversible (backtracking).
- Morris threads are always removed.
- If state repetition is possible: visited is enforced.

### 🐛 Level 5 mini debugging guide
- Backtracking: log `state` + `choice` + `rollback`.
- Morris: log when threads are created/removed and verify structure after.

---

# 🌿 N-ary Trees (enhancement module)

## N1. N-ary preorder

### Why
Parent-first is common in outlines and hierarchical exports.

### What
Visit node, then visit each child in order.

### How (step/flow)
- Recursive: visit then loop children.
- Iterative: stack pop/visit; push children **right-to-left** so leftmost is processed first.

### Where and When (use cases)
- Interview scenario: “N-ary preorder traversal.”
- System scenario: “Render menu tree in reading order.”

### Common pitfalls
- Pushing children left-to-right on a stack (reverses sibling order).

### Tips and tricks
- Stack reverses; push children in reverse.

Python (iterative preorder idea):
```python
def nary_preorder(root):
    if not root: return []
    out = []
    st = [root]
    while st:
        node = st.pop()
        out.append(node.val)
        for child in reversed(node.children):
            st.append(child)
    return out
```

---

## N2. N-ary postorder

### Why
Child-first aggregation with many children.

### What
Visit all children, then the node.

### How (step/flow)
- Recursive: for child in children: dfs(child); then visit.
- Iterative: phase frames (ENTER/EXIT) generalize cleanly.

### Where and When (use cases)
- Interview scenario: “Compute subtree size for N-ary tree.”
- System scenario: “Aggregate metrics up a category tree.”

### Common pitfalls
- Visiting parent before all children.

### Tips and tricks
- Prefer EXIT-phase visit when you need child results.

---

## N3. N-ary level-order BFS

### Why
Same queue frontier logic as binary.

### What
Level snapshot BFS; enqueue all children.

### How (step/flow)
- Snapshot levelSize; pop exactly that many; enqueue children.

### Where and When (use cases)
- Interview scenario: “Return level lists for N-ary tree.”
- System scenario: “Render hierarchical UI level-by-level.”

### Common pitfalls
- Not snapshotting levelSize.

### Tips and tricks
- Wave processing is universal.

---

# 🧰 Traversal chooser (fast mapping)

| Requirement | Usually pick | Why it fits |
|---|---|---|
| Parent before children | Preorder | Decide/emit before exploring |
| Children before parent | Postorder | Aggregation needs child results |
| Sorted order (BST) | Inorder | Left < root < right |
| Group by depth | BFS level-order | Natural wave processing |
| One node per level (view) | BFS + select | Pick first/last per level |
| Subtree as range | Euler tour | tin/tout define intervals |
| Coordinate grouping | BFS/DFS + state | Carry col/row/diagonal |

---

# 🔗 Free external resources (browseable)

- VisuAlgo — BST/AVL + traversal animations: https://visualgo.net/en/bst
  - What to learn from this: how preorder/inorder/postorder visit timing changes output; how structure affects traversal.

- USFCA Data Structure Visualizations (trees, tries): https://www.cs.usfca.edu/~galles/visualization/
  - What to learn from this: interactive intuition for tree operations and how traversals walk the structure.

- Wikipedia — Tree traversal (conceptual overview): https://en.wikipedia.org/wiki/Tree_traversal
  - What to learn from this: standardized terminology and traversal families.

- GeeksforGeeks — Tree traversals (pre/in/post): https://www.geeksforgeeks.org/dsa/tree-traversals-inorder-preorder-and-postorder/
  - What to learn from this: canonical order definitions and baseline implementations.

- GeeksforGeeks — Euler tour of tree: https://www.geeksforgeeks.org/dsa/euler-tour-tree/
  - What to learn from this: enter/exit recording and how Euler tour sequences represent structure.

---

## Next step
Say which focus you want first:
- **Binary only**, or **Binary + N-ary** from day 1?
- And do you want the homework drills in **STRICT** (throw exceptions) or **LENIENT** (return defaults) style?
