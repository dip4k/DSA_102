# 🌳 Flow-Wise Tree Traversal Mastery (v1)
**Goal:** Professional-grade intuition for tree traversal (Level 1 → Level 5).  
**Style contract:** Why / What / How / Where / When + step-flow + visuals + Python/C# snippets + pitfalls + tips.

---

# 🧭 One-page Level Mapping Index (Trees)
Use this page to instantly map a tree problem to the right **level**, the right **pattern**, and the **expected invariant**.

## ✅ Legend
- 🧾 **Invariant** = what must stay true after each step.
- 🎒 **Implicit stack** = recursion call stack.
- 🧺 **Explicit stack** = your own stack to simulate recursion.

| Level | Pattern / skill | Typical LeetCode problems (examples) | Expected invariant (one-liner) |
|---|---|---|---|
| L1 | Recursive DFS skeleton + base case | 144 Preorder, 94 Inorder, 145 Postorder | “When dfs(node) returns, the traversal/answer for node’s subtree is correct.” |
| L1 | Choose visit timing (pre/in/post) | 104 Max Depth, 111 Min Depth | “I update answer at the correct moment (before/after children) for the chosen order.” |
| L2 | Iterative DFS (stack) | 94 Inorder (iterative), 145 Postorder (iterative) | “Stack represents the same frontier as recursion; no node is processed twice (unless intended).” |
| L2 | Frame-based DFS (stateful stack) | 230 Kth Smallest, 98 Validate BST | “Each frame knows ‘where I am’ (enter/left/right/exit), so order is deterministic.” |
| L3 | BFS / Level order (queue) | 102 Level Order, 199 Right Side View | “Queue contains exactly the next frontier; processing is level-by-level.” |
| L3 | Zigzag / multi-queue variants | 103 Zigzag Level Order, 515 Largest Value in Each Row | “Each level is collected fully before switching direction/aggregation.” |
| L4 | O(1) extra space traversal (Morris) | 94 Inorder (Morris), 99 Recover BST (advanced) | “Temporary threads are created and removed; tree structure is restored.” |
| L4 | Traversal with parent pointers / iterators | 173 BST Iterator, 116 Populating Next Right Pointers | “Pointer-walking never loses the next node; structure remains consistent.” |
| L5 | Traversal as problem solver (tree DP) | 543 Diameter, 124 Max Path Sum | “Each node returns a correct summary of its subtree; global answer updates safely.” |
| L5 | Path-based DFS (backtracking) | 113 Path Sum II, 437 Path Sum III | “Current path state matches root→current; rollback restores state.” |

---

# 🧠 The ladder: complexity of state
- **Level 1:** Recursion (implicit stack) + base case + visit timing.
- **Level 2:** Iteration (explicit stack) + frames/states.
- **Level 3:** Queue frontier (BFS) + level snapshots.
- **Level 4:** Pointer-threading / structural traversal tricks.
- **Level 5:** Compose traversals into proofs (tree DP + backtracking).

---

# 🧱 Baseline node definitions (snippets)

## 🐍 Python
```python
class TreeNode:
    def __init__(self, val=0, left=None, right=None):
        self.val = val
        self.left = left
        self.right = right
```

## 🟦 C#
```csharp
public class TreeNode
{
    public int val;
    public TreeNode left;
    public TreeNode right;
    public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
    {
        this.val = val;
        this.left = left;
        this.right = right;
    }
}
```

---

# 🟢 Level 1: The Physical Layer (Recursive DFS Basics)
**Goal:** Muscle memory for visiting nodes safely using recursion.

## Topics to know
- 🌱 Base case: node is null
- 🧭 Preorder / Inorder / Postorder meaning
- 🧠 “What does dfs(node) return?” thinking
- 🧪 Edge cases: empty tree, single node, skewed tree

## Things to master
- ✅ You can write a correct DFS template in 20 seconds
- ✅ You can explain *why* preorder vs postorder matters
- ✅ You can draw recursion stack snapshots (tiny trees)

---

## 1.1 🧠 The universal recursive DFS template

**Why:** Almost every tree problem is “solve for node using solutions of children.”

**What:** A function that processes a subtree and returns something (value, boolean, depth, etc.).

**How (step/flow):**
1) If node is null → return identity value
2) Optionally process node (pre)
3) Recurse left
4) Optionally process node (in)
5) Recurse right
6) Optionally process node (post)
7) Return combined result

**Where:** traversal lists, depth, sums, validation, subtree properties.

**When:** Default choice when recursion depth is safe.

**Visual (tiny tree)**
```text
      1
     / \
    2   3

Preorder:  1,2,3   (node before children)
Inorder:   2,1,3   (node between children)
Postorder: 2,3,1   (node after children)
```

**Python: preorder traversal**
```python
def preorder(root):
    out = []
    def dfs(node):
        if not node:
            return
        out.append(node.val)   # PRE
        dfs(node.left)
        dfs(node.right)
    dfs(root)
    return out
```

**C#: inorder traversal**
```csharp
IList<int> InorderTraversal(TreeNode root)
{
    var res = new List<int>();
    void Dfs(TreeNode node)
    {
        if (node == null) return;
        Dfs(node.left);
        res.Add(node.val); // IN
        Dfs(node.right);
    }
    Dfs(root);
    return res;
}
```

**Common pitfalls**
- ❌ Forgetting the base case (null) → crashes
- ❌ Using preorder logic when the problem needs postorder aggregation

**Tips & tricks**
- 💡 Say out loud: “dfs(node) fully solves node’s subtree.”
- 💡 Identify the identity return: 0 for sum, 1 for product, False for “found”, etc.

**Practice (2+)**
- LeetCode 144: Binary Tree Preorder Traversal
- LeetCode 94: Binary Tree Inorder Traversal

---

## 1.2 🧭 Visit timing (pre / in / post) and what it unlocks

**Why:** The *same* recursion skeleton can solve different problems by changing when you update state.

**What:**
- **Preorder:** decide/record before children
- **Inorder:** good for BST sorted order
- **Postorder:** compute from children upward (classic tree DP)

**How (step/flow):**
1) Decide what your node needs from children
2) If it needs child results → postorder
3) If it must decide path before exploring → preorder
4) If it needs “between” semantics (BST) → inorder

**Where:**
- Pre: serialization, path building
- In: BST iterator, kth smallest
- Post: height, diameter, balance check

**When:** any time you ask “do I need child answers first?”

**Visual (postorder aggregation)**
```text
Postorder computes upward:

      (ans from children)
             ↑
      1
     / \
    2   3

Compute dfs(2), dfs(3) first, then dfs(1)
```

**Example (detailed): Max depth (postorder)**

**Python**
```python
def max_depth(root):
    def dfs(node):
        if not node:
            return 0
        left = dfs(node.left)
        right = dfs(node.right)
        return 1 + max(left, right)  # POST: use child results
    return dfs(root)
```

**Common pitfalls**
- ❌ Updating global answer too early (before child values exist)

**Tips**
- 💡 If the return depends on children, your visit is postorder.

**Practice (2+)**
- LeetCode 104: Maximum Depth of Binary Tree
- LeetCode 111: Minimum Depth of Binary Tree

---

## 1.3 🧪 Edge cases and recursion safety

**Why:** Trees often degrade into linked lists (skewed), making recursion deep.

**What:** edge cases: empty tree, single node, skewed left/right, duplicates in BST.

**How (step/flow):**
1) Always handle root == null
2) Test 1-node tree
3) Test skewed tree (like a chain)

**Where:** every tree problem.

**When:** always first.

**Visual**
```text
Empty:   null
Single:  [1]
Skewed:  1
         \
          2
           \
            3
```

**Pitfalls**
- ❌ stack overflow risk in deep recursion (switch to Level 2 stack)

**Practice (2+)**
- LeetCode 100: Same Tree
- LeetCode 101: Symmetric Tree

---

# 🔵 Level 2: The Structural Layer (Iterative DFS with Stack)
**Goal:** Replace recursion with explicit control.

## Topics to know
- 🎒 Stack-based DFS (simulate recursion)
- 🧱 “Color marking” or frame-state technique
- 🧩 Null handling in iterative traversal

## Things to master
- ✅ You can implement inorder iteratively without guessing
- ✅ Your stack invariant is crystal clear
- ✅ You can switch between recursion and stack calmly

---

## 2.1 🎒 Iterative inorder traversal (classic stack)

**Why:** Recursion may overflow; iterative is interview-safe and production-safe.

**What:** Push-left chain, then visit, then go right.

**How (step/flow):**
1) curr = root
2) while curr or stack not empty:
3) push curr and move curr=curr.left
4) pop, visit
5) curr = popped.right

**Where:** BST iteration, sorted order extraction.

**When:** you need inorder without recursion.

**Visual**
```text
Push-left phase:
      4
     /
    2
   /
  1

Stack top: [4,2,1]
Pop 1 (visit), then go right (null), pop 2...
```

**Python**
```python
def inorder_iter(root):
    res = []
    st = []
    curr = root
    while curr or st:
        while curr:
            st.append(curr)
            curr = curr.left
        curr = st.pop()
        res.append(curr.val)
        curr = curr.right
    return res
```

**Pitfalls**
- ❌ forgetting the outer loop condition (curr or st)
- ❌ visiting during push-left instead of during pop

**Tips**
- 💡 Invariant: “stack holds the path to current, waiting to be visited.”

**Practice (2+)**
- LeetCode 94: Binary Tree Inorder Traversal (iterative)
- LeetCode 230: Kth Smallest Element in a BST

---

## 2.2 🧱 Frame-based DFS (state machine on stack)

**Why:** Some traversals (postorder) are tricky without a state machine.

**What:** Store (node, state) where state indicates progress.

**How (step/flow):**
1) push (root, ENTER)
2) pop frame
3) push next frames in reverse order you want executed

**Where:** postorder traversal, complex “do something before and after children.”

**When:** you need deterministic pre/in/post hooks iteratively.

**Visual (3-state frame)**
```text
States:
0 ENTER  (before left)
1 IN     (between left and right)
2 EXIT   (after right)

Frame stack simulates recursion call frames.
```

**C# (concept snippet)**
```csharp
// Pseudocode-like pattern (keep it simple in interviews):
// stack holds (node, state)
```

**Pitfalls**
- ❌ pushing frames in wrong order

**Tips**
- 💡 If recursion has 3 moments (pre/in/post), your frame has 3 states.

**Practice (2+)**
- LeetCode 145: Binary Tree Postorder Traversal (iterative)
- LeetCode 98: Validate Binary Search Tree (iterative inorder)

---

# 🟠 Level 3: The Frontier Layer (BFS / Level Order)
**Goal:** Think in waves (distance from root).

## Topics to know
- 🧺 Queue BFS
- 🧱 Level snapshot pattern (process exactly current queue size)
- 🔁 Zigzag and side views

## Things to master
- ✅ You can confidently say what’s in the queue at any time
- ✅ You never mix nodes of different levels in the same list

---

## 3.1 🧺 Level order traversal (BFS)

**Why:** Level-based questions naturally match BFS because it processes nodes by distance from root.

**What:** Use a queue; each iteration processes one full level.

**How (step/flow):**
1) if root null → return []
2) push root into queue
3) while queue not empty:
4) levelSize = queue.size
5) pop exactly levelSize nodes → build one level
6) push their children

**Where:** level order output, minimum depth, right side view.

**When:** you need “level by level” behavior.

**Visual (queue frontier)**
```text
Level 0:       [1]
              /   \
Level 1:    [2]   [3]
            / \     \
Level 2:  [4] [5]   [6]

Queue snapshots:
Start: [1]
After level0: [2,3]
After level1: [4,5,6]
```

**Python**
```python
from collections import deque

def level_order(root):
    if not root:
        return []
    q = deque([root])
    res = []
    while q:
        level = []
        for _ in range(len(q)):
            node = q.popleft()
            level.append(node.val)
            if node.left:  q.append(node.left)
            if node.right: q.append(node.right)
        res.append(level)
    return res
```

**Pitfalls**
- ❌ using `for _ in range(len(q))` while also appending (must capture size before loop)

**Tips**
- 💡 Invariant: “queue contains the next frontier only.”

**Practice (2+)**
- LeetCode 102: Binary Tree Level Order Traversal
- LeetCode 199: Binary Tree Right Side View

---

## 3.2 🔁 Zigzag and level aggregates

**Why:** Once you can do level snapshots, zigzag and per-level stats are easy.

**What:** Collect nodes per level; reverse or alternate insertion direction.

**How (step/flow):**
1) BFS level snapshot
2) if level is odd: reverse list (or write reversed)
3) append to result

**Where:** zigzag traversal, largest value per level.

**When:** output depends on level parity.

**Visual**
```text
Level 0: left→right
Level 1: right→left
Level 2: left→right

Result pattern:
[ [1], [3,2], [4,5,6] ]
```

**Practice (2+)**
- LeetCode 103: Binary Tree Zigzag Level Order Traversal
- LeetCode 515: Find Largest Value in Each Tree Row

---

# 🟣 Level 4: The Pointer Layer (Morris & Structural Tricks)
**Goal:** Traverse without recursion and without an explicit stack (advanced).

## Topics to know
- 🧵 Morris inorder traversal (temporary threading)
- 🧭 Tree restoration discipline
- 🧩 Iterator patterns / next pointers

## Things to master
- ✅ You understand “thread creation” and “thread removal”
- ✅ You never leave the tree modified after traversal

---

## 4.1 🧵 Morris inorder traversal (O(1) extra space idea)

**Why:** Sometimes you want traversal with constant extra space (no recursion/stack).

**What:** Temporarily create a thread from inorder predecessor back to current node, then remove it.

**How (step/flow):**
1) curr = root
2) if curr.left is null: visit curr; curr = curr.right
3) else find predecessor (rightmost of left subtree)
4) if pred.right is null: set pred.right = curr; curr = curr.left
5) else (pred.right == curr): remove thread pred.right=null; visit curr; curr=curr.right

**Where:** inorder traversal without stack; advanced memory constraints.

**When:** explicitly asked for O(1) auxiliary traversal or as a follow-up.

**Visual (threading)**
```text
When curr has left:
    curr
    / \
  ...  R
    \
    pred

Create thread:
pred.right -> curr   (breadcrumb)
Later remove it and continue to curr.right
```

**Pitfalls**
- ❌ forgetting to remove the thread → corrupts tree
- ❌ wrong predecessor search (must be rightmost in left subtree)

**Tips**
- 💡 Invariant: “Every temporary thread is eventually removed.”

**Practice (2+)**
- LeetCode 94: Binary Tree Inorder Traversal (Morris follow-up)
- LeetCode 99: Recover Binary Search Tree (advanced inorder reasoning)

---

## 4.2 🧭 Traversal using structural pointers (next/iterator)

**Why:** Some problems extend the tree with extra pointers (next/right links) to support traversal.

**What:** Use or populate structural pointers to enable level traversal without extra memory.

**How (step/flow):**
- For “next pointers”: connect nodes within a level, then use those links to traverse

**Where:** populating next right pointers, BST iterators.

**When:** the problem provides/asks for pointer augmentation.

**Practice (2+)**
- LeetCode 116: Populating Next Right Pointers in Each Node
- LeetCode 173: Binary Search Tree Iterator

---

# 🔴 Level 5: The Abstract Layer (Traversal as a Problem-Solving Framework)
**Goal:** Use traversal to compute non-trivial properties with proofs.

## Topics to know
- 🧠 Tree DP (postorder returns)
- 🪞 Backtracking (path state)
- 🧷 Global answer updates (diameter, max path)

## Things to master
- ✅ You can define “return value” and “global update” separately
- ✅ You can state the invariant for your return
- ✅ You never forget rollback for backtracking

---

## 5.1 🧠 Tree DP pattern: return a value, update a global

**Why:** Many classic tree problems require a per-node summary + a global best.

**What:**
- dfs(node) returns a summary for this subtree
- globalAnswer updates using summaries

**How (step/flow):**
1) dfs(null) returns identity
2) compute left = dfs(left), right = dfs(right)
3) compute local candidates
4) update globalAnswer
5) return summary to parent

**Where:** diameter, max path sum, balance check.

**When:** a property depends on both subtrees.

**Visual**
```text
Postorder summaries flow upward:
leftSummary   rightSummary
     \         /
        node
         |
     returnSummary

Global answer updated at node using both sides.
```

**Pitfalls**
- ❌ mixing “return value” with “global best”

**Tips**
- 💡 Write two lines: “update global” and “return to parent.”

**Practice (2+)**
- LeetCode 543: Diameter of Binary Tree
- LeetCode 124: Binary Tree Maximum Path Sum

---

## 5.2 🪞 Path-based DFS (backtracking)

**Why:** Path problems require maintaining a current path and rolling back correctly.

**What:** Keep a list/stack of nodes on the path root→current.

**How (step/flow):**
1) add current node to path
2) if leaf: maybe record
3) recurse children
4) remove current node from path (rollback)

**Where:** all root-to-leaf paths, sum paths.

**When:** you need the exact path, not just counts.

**Visual**
```text
path stack:
root -> ... -> curr
push when entering
pop when exiting

Invariant: path exactly matches recursion path
```

**Pitfalls**
- ❌ forgetting rollback → path leaks into sibling computations

**Tips**
- 💡 Always pair: append before recurse, pop after recurse.

**Practice (2+)**
- LeetCode 113: Path Sum II
- LeetCode 437: Path Sum III

---

# 🧰 Traversal mastery add-ons (high leverage)

## A) 🧪 Micro-tracing (30 seconds)
- Draw tree in 3 levels.
- Mark current node.
- Write stack (recursion or explicit) as a vertical list.
- After each step: update pointer/stack first, then output.

## B) 🧠 Invariant library (memorize)
- Recursive DFS: “dfs(node) solves node’s subtree.”
- Iterative inorder: “stack holds path to next node to visit.”
- BFS: “queue holds next frontier only.”
- Morris: “threads are created and removed; tree is restored.”
- Backtracking: “path list equals root→current; rollback restores it.”

## C) ✅ Debug checklist
- Did I handle root == null?
- Did I choose visit timing correctly?
- If iterative: what exactly does stack/queue contain?
- Am I mixing levels in BFS?
- Did I rollback path state?
- Did I accidentally modify the tree and not restore it?

---

## 📚 Further reading (optional)
- DFS concept: go deep then backtrack (general DFS idea). https://cp-algorithms.com/graph/depth-first-search.html
- BFS level-order idea: queue processes nodes level-by-level. (Any BFS/level-order explanation)
- Morris traversal idea: threaded traversal without stack; must restore tree.
