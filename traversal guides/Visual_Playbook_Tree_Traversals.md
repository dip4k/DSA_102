# 🎛️ Visual Playbook — Tree Traversals (Diagrams Only)
**Purpose:** Copy-paste friendly ASCII diagrams for teaching tree traversal patterns (DFS/BFS/Stack/Morris).

---

## Legend
- Nodes are shown as values in boxes or circles
- `curr`, `stack`, `queue`, `slow/fast` indicate traversal state
- `PRE / IN / POST` indicate visit timing

---

# 🟢 Level 1 — Recursive DFS (Timing)

## 1) Preorder / Inorder / Postorder
```text
      1
     / \
    2   3

PRE  (Node, L, R):  1 2 3
IN   (L, Node, R):  2 1 3
POST (L, R, Node):  2 3 1

Timing markers:
PRE : visit when you enter the node
IN  : visit after left returns
POST: visit after both children return
```

## 2) Recursion stack intuition
```text
Tree:
      1
     / \
    2   3
   /
  4

Call stack during DFS-left-first (concept):
Top
 dfs(4)
 dfs(2)
 dfs(1)
Bottom

When dfs(4) returns, control goes back to dfs(2)
```

## 3) Base case (null)
```text
Any missing child is null

      1
     /
    2
     \
     null

Rule: if node == null -> return immediately
```

---

# 🔵 Level 2 — Iterative DFS (Explicit Stack)

## 4) Iterative inorder (push-left chain)
```text
      4
     /
    2
   / \
  1   3

Algorithm picture:
- Push-left: 4,2,1
- Pop+visit: 1
- Go right: null
- Pop+visit: 2
- Go right: 3 (push-left: 3)
- Pop+visit: 3
- Pop+visit: 4

Stack snapshots (top on right):
[]
[4]
[4,2]
[4,2,1]
pop 1 -> [4,2]
...
```

## 5) Iterative preorder (stack)
```text
      1
     / \
    2   3

Stack: push root
Pop -> visit
Push right then left (so left is processed first)

Stack snapshots:
[1]
pop 1 visit
push 3,2 -> [3,2]
pop 2 visit
pop 3 visit
```

## 6) Postorder via state frames (ENTER/EXIT)
```text
Idea: each node is visited after children.
Use frames:
(node, state)

States:
ENTER: schedule children
EXIT:  output node

Execution order:
pop (node,ENTER)
 push (node,EXIT)
 push (right,ENTER)
 push (left,ENTER)

Invariant: EXIT frames represent nodes whose children are already scheduled
```

---

# 🟠 Level 3 — BFS (Queue Frontier)

## 7) Level order traversal (process by levels)
```text
        1
      /   \
     2     3
    / \     \
   4   5     6

Queue front -> back
Start: [1]
Level0 process 1, enqueue 2,3  -> [2,3]
Level1 process 2,3 enqueue 4,5,6 -> [4,5,6]
Level2 process 4,5,6 -> []

Invariant: queue contains exactly the next frontier
```

## 8) Level snapshot pattern (size-based loop)
```text
while queue not empty:
  levelSize = queue.size
  repeat levelSize times:
    pop node
    push children

Why size snapshot?
Because queue grows during the level.
You must not mix next level into current level output.
```

## 9) Zigzag level order
```text
Level 0: left->right
Level 1: right->left
Level 2: left->right

Output example:
[ [1], [3,2], [4,5,6] ]

Implementation idea:
- collect normal level list
- reverse on odd levels (or fill deque from ends)
```

---

# 🟣 Level 4 — Morris Traversal (O(1) Extra Space)

## 10) Morris inorder (threading)
```text
When curr has left child:
Find predecessor = rightmost node of curr.left

Case A: pred.right is null
  pred.right = curr     (create thread)
  curr = curr.left

Case B: pred.right == curr
  pred.right = null     (remove thread)
  visit curr
  curr = curr.right

Diagram (thread):
    curr
    / \
  ...  R
    \
    pred

Create: pred.right --> curr   (temporary)
Remove later to restore tree

Invariant: every thread created is later removed
```

---

# 🔴 Level 5 — Traversal as Solver (DP + Backtracking)

## 11) Postorder DP (return value + global update)
```text
At each node:
leftSummary  = dfs(left)
rightSummary = dfs(right)

Update global answer using both
Return a summary upward

Flow:
child summaries -> node local compute -> return to parent

Invariant: dfs(node) returns correct summary for node's subtree
```

## 12) Backtracking path state (push/pop)
```text
path stack represents root -> current

Enter node:
  path.push(node)

Explore children

Exit node:
  path.pop()

Invariant: path always matches the recursion path
Rollback prevents sibling contamination
```

---

## Level-Order (BFS) traversal: diagrams

Level-order traversal visits the tree **level by level** from top to bottom, and it uses a **queue** (FIFO) to manage the current frontier.
This traversal is commonly used when you need “layer-wise” processing (levels, siblings, minimum depth / shortest unweighted steps).

### Canonical example tree
```
            1
         /     \
        2       3
      /   \      \
     4     5      6
```

### Diagram 1: “Frontier” mental model (what’s in the queue)
Think: "Queue = the nodes we discovered but haven’t processed yet."

```
Start:
Queue: [1]
Out:   []

Process 1:
Queue: [2, 3]
Out:   [1]

Process 2:
Queue: [3, 4, 5]
Out:   [1, 2]

Process 3:
Queue: [4, 5, 6]
Out:   [1, 2, 3]

Process 4:
Queue: [5, 6]
Out:   [1, 2, 3, 4]

Process 5:
Queue: [6]
Out:   [1, 2, 3, 4, 5]

Process 6:
Queue: []
Out:   [1, 2, 3, 4, 5, 6]
```

### Diagram 2: True “level order” (grouped by level using `levelSize`)
To keep levels separate, snapshot `levelSize = queue.Count` at the start of each level.

```
Initial:
Queue: [1]
Levels: []

Level 0 (levelSize = 1):
- pop 1, push children 2,3
Queue after level: [2, 3]
Levels: [[1]]

Level 1 (levelSize = 2):
- pop 2, push children 4,5
- pop 3, push child 6
Queue after level: [4, 5, 6]
Levels: [[1], [2, 3]]

Level 2 (levelSize = 3):
- pop 4
- pop 5
- pop 6
Queue after level: []
Levels: [[1], [2, 3], [4, 5, 6]]
```

### Diagram 3: Queue as a moving window (useful for interview explanation)
This shows how each level fully “drains” before the next begins (because of the `levelSize` boundary). 
```
Queue before Level 1: [2, 3]      (process exactly 2 nodes)
                       ^  ^
                       |  |
                 drain these two, append their kids -> [4,5,6]
```

## Production-ready BFS level order (C#)
Level-order traversal is the BFS traversal on a tree, implemented using a queue.

```csharp
public static IList<IList<int>> LevelOrder(TreeNode? root)
{
    if (root == null) return new List<IList<int>>();

    var result = new List<IList<int>>();
    var queue = new Queue<TreeNode>();
    queue.Enqueue(root);

    while (queue.Count > 0)
    {
        int levelSize = queue.Count;
        var level = new List<int>(levelSize);

        for (int i = 0; i < levelSize; i++)
        {
            var node = queue.Dequeue();
            level.Add(node.val);

            if (node.left != null) queue.Enqueue(node.left);
            if (node.right != null) queue.Enqueue(node.right);
        }

        result.Add(level);
    }

    return result;
}
```

## Common mistakes (and fixes)

- Mixing levels: If you loop `while(queue.Count > 0)` and keep dequeuing/enqueuing without capturing `levelSize`, you’ll lose level boundaries; fix by snapshotting `levelSize = queue.Count` per level.
- Wrong data structure: Using a stack turns it into DFS; BFS level-order specifically needs a queue.
- Null handling: Always guard `root == null` to avoid exceptions and to correctly return an empty result.

