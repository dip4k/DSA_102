# 🌳 Tree Traversal Drills (No Solutions) + LeetCode Problem Set

**Purpose:** A structured set of time-boxed drills to practice tree traversal and navigation across variants **without providing solutions/answers**.

**How to use (recommended):**
1) For each drill, write a 1-line contract: **Frontier + Visit timing + State**.
2) Do a 60–90s dry-run on a tiny tree, writing stack/queue snapshots.
3) Implement only after you can predict the next 3 steps.
4) When stuck: log what the drill asks you to log (stack/queue/path/coords), not random prints.

---

## ✅ Baseline types (C#)

```csharp
public class BNode
{
    public int Val;
    public BNode? Left;
    public BNode? Right;
    public BNode(int val, BNode? left = null, BNode? right = null)
    { Val = val; Left = left; Right = right; }
}

public class NNode
{
    public int Val;
    public List<NNode> Children = new();
    public NNode(int val) { Val = val; }
}

public class PNode
{
    public int Val;
    public PNode? Left;
    public PNode? Right;
    public PNode? Parent;
    public PNode(int val) { Val = val; }
}

public class TrieNode
{
    public bool IsTerminal;
    public SortedDictionary<char, TrieNode> Next = new();
}
```

---

# Level 1 🟢 Recursive DFS timing (15 drills)

### L1-01: Recursive preorder list
- Timebox: 4 min
- Pattern: DFS preorder (ENTER visit)
- Signature:
```csharp
static List<int> PreorderRecursive(BNode? root)
```
- Contract: if `root == null` return empty list.
- Must log while debugging: `(node.Val, depth)` on ENTER.

### L1-02: Recursive inorder list (binary)
- Timebox: 4 min
- Pattern: DFS inorder (BETWEEN visit)
- Signature:
```csharp
static List<int> InorderRecursive(BNode? root)
```
- Contract: if null return empty.
- Must log: `ENTER`, `BETWEEN`, `EXIT` events as strings.

### L1-03: Recursive postorder list
- Timebox: 4 min
- Pattern: DFS postorder (EXIT visit)
- Signature:
```csharp
static List<int> PostorderRecursive(BNode? root)
```
- Contract: if null return empty.
- Must log: `(node.Val, event)`.

### L1-04: Count nodes in a tree (postorder summary)
- Timebox: 4 min
- Pattern: return summary from children
- Signature:
```csharp
static int CountNodes(BNode? root)
```
- Contract: if null return 0.
- Must log: `returnValue` at each node.

### L1-05: Compute height (depth/height mental model)
- Timebox: 4 min
- Pattern: postorder aggregate
- Signature:
```csharp
static int Height(BNode? root)
```
- Contract: empty tree height = 0.
- Must log: `(node.Val, leftHeight, rightHeight)`.

### L1-06: Mirror traversal reasoning
- Timebox: 5 min
- Pattern: preorder but swap child traversal order
- Signature:
```csharp
static List<int> PreorderRightFirst(BNode? root)
```
- Contract: null => empty.
- Must log: child order taken at each node.

### L1-07: Leaf collection (left-to-right)
- Timebox: 5 min
- Pattern: DFS leaf predicate
- Signature:
```csharp
static List<int> LeavesLeftToRight(BNode? root)
```
- Contract: null => empty.
- Must log: when leaf detected.

### L1-08: Root-to-leaf path count
- Timebox: 5 min
- Pattern: DFS counting paths
- Signature:
```csharp
static int CountRootToLeafPaths(BNode? root)
```
- Contract: null => 0.
- Must log: leaf hits.

### L1-09: Serialize preorder with null markers (strings)
- Timebox: 6 min
- Pattern: preorder + null markers
- Signature:
```csharp
static List<string> SerializePreorderWithNulls(BNode? root)
```
- Contract: represent null as "#".
- Must log: token stream as it is produced.

### L1-10: Deserialize preorder with null markers
- Timebox: 6 min
- Pattern: stream pointer parsing
- Signature:
```csharp
static BNode? DeserializePreorderWithNulls(IList<string> tokens)
```
- Contract: invalid token stream => throw `ArgumentException`.
- Must log: `index` movement.

### L1-11: Validate recursion safety trigger
- Timebox: 3 min
- Task: write a note (as a comment) describing when recursion is unsafe.
- Signature: N/A
- Must include: what “skewed tree” means.

### L1-12: N-ary preorder (recursive)
- Timebox: 5 min
- Pattern: visit then loop children
- Signature:
```csharp
static List<int> NaryPreorderRecursive(NNode? root)
```
- Contract: null => empty.
- Must log: `(node.Val, childIndex)`.

### L1-13: N-ary postorder (recursive)
- Timebox: 5 min
- Pattern: loop children then visit
- Signature:
```csharp
static List<int> NaryPostorderRecursive(NNode? root)
```
- Contract: null => empty.
- Must log: EXIT events.

### L1-14: Trie DFS emit all words (lexicographic)
- Timebox: 6 min
- Pattern: DFS + path rollback
- Signature:
```csharp
static List<string> TrieAllWords(TrieNode root)
```
- Contract: root not null; if null throw.
- Must log: path length on ENTER/EXIT.

### L1-15: DFS event tracer
- Timebox: 5 min
- Task: Implement a traversal that outputs events like "ENTER 5", "EXIT 5".
- Signature:
```csharp
static List<string> EulerEnterExitTrace(BNode? root)
```
- Contract: null => empty.
- Must log: depth alongside ENTER/EXIT.

---

# Level 2 🔵 Representation drills (10 drills)

### L2-01: Implicit heap preorder (array tree)
- Timebox: 6 min
- Pattern: index arithmetic
- Signature:
```csharp
static List<int> PreorderImplicitHeap(int[] a)
```
- Contract: empty array => empty list.
- Must log: `(i, leftIndex, rightIndex)`.

### L2-02: Implicit heap BFS by levels
- Timebox: 6 min
- Pattern: queue indices; level snapshot
- Signature:
```csharp
static List<List<int>> LevelOrderImplicitHeap(int[] a)
```
- Contract: empty => empty.
- Must log: queue contents as indices.

### L2-03: Rooted tree from edges (adjacency list build)
- Timebox: 6 min
- Pattern: build adjacency
- Signature:
```csharp
static List<int>[] BuildAdj(int n, (int u, int v)[] edges)
```
- Contract: invalid node id => throw.
- Must log: degree of each node.

### L2-04: Rooted adjacency DFS preorder
- Timebox: 6 min
- Pattern: DFS with parent
- Signature:
```csharp
static List<int> PreorderAdj(int root, List<int>[] adj)
```
- Contract: root out of range => throw.
- Must log: `(u, parent)`.

### L2-05: Rooted adjacency BFS levels
- Timebox: 6 min
- Pattern: BFS with parent; levels
- Signature:
```csharp
static List<List<int>> LevelsAdj(int root, List<int>[] adj)
```
- Contract: root invalid => throw.
- Must log: `(level, levelSize)`.

### L2-06: Deterministic traversal order policy
- Timebox: 4 min
- Task: ensure traversal output is deterministic by sorting adjacency lists.
- Signature:
```csharp
static void SortAdj(List<int>[] adj)
```
- Contract: null adj => throw.

### L2-07: N-ary iterative preorder ordering check
- Timebox: 5 min
- Pattern: stack reverse-push rule
- Signature:
```csharp
static List<int> NaryPreorderIterative(NNode? root)
```
- Contract: null => empty.
- Must log: the order children are pushed.

### L2-08: Trie child ordering policy
- Timebox: 3 min
- Task: convert an unordered child store to ordered store.
- Signature: N/A

### L2-09: Convert pointer tree to adjacency list
- Timebox: 6 min
- Pattern: traversal + edge emission
- Signature:
```csharp
static List<int>[] PointerTreeToAdj(BNode root)
```
- Contract: root not null.
- Must log: edges emitted.

### L2-10: Depth array from adjacency BFS
- Timebox: 6 min
- Pattern: BFS distances
- Signature:
```csharp
static int[] DepthFromRoot(int root, List<int>[] adj)
```
- Contract: root invalid => throw.
- Must log: `(u, depth[u])` when assigned.

---

# Level 3 🟠 Iterative control (20 drills)

### L3-01: Iterative inorder (push-left chain)
- Timebox: 6 min
- Signature:
```csharp
static List<int> InorderIterative(BNode? root)
```
- Must log: stack contents by node value.

### L3-02: Iterative preorder (stack)
- Timebox: 5 min
- Signature:
```csharp
static List<int> PreorderIterative(BNode? root)
```
- Must log: push order (right then left).

### L3-03: Iterative postorder using phase frames
- Timebox: 7 min
- Signature:
```csharp
static List<int> PostorderIterative_Phases(BNode? root)
```
- Must log: `(node.Val, phase)`.

### L3-04: Iterative postorder using two stacks
- Timebox: 7 min
- Signature:
```csharp
static List<int> PostorderIterative_TwoStacks(BNode? root)
```
- Must log: sizes of both stacks.

### L3-05: Iterative postorder using lastVisited pointer
- Timebox: 8 min
- Signature:
```csharp
static List<int> PostorderIterative_LastVisited(BNode? root)
```
- Must log: `(peek.Val, lastVisited?.Val)`.

### L3-06: BFS level-order (grouped)
- Timebox: 6 min
- Signature:
```csharp
static List<List<int>> LevelOrder(BNode? root)
```
- Must log: `(level, levelSize)`.

### L3-07: BFS zigzag
- Timebox: 7 min
- Signature:
```csharp
static List<List<int>> ZigzagLevelOrder(BNode? root)
```
- Must log: parity of level.

### L3-08: BFS reverse level-order (bottom-up)
- Timebox: 6 min
- Signature:
```csharp
static List<List<int>> LevelOrderBottomUp(BNode? root)
```
- Must log: total levels produced before reversing.

### L3-09: BFS right-to-left per level (presentation)
- Timebox: 6 min
- Signature:
```csharp
static List<List<int>> LevelsRightToLeft(BNode? root)
```
- Must log: before/after reversal per level.

### L3-10: BFS per-level aggregates (sum, max)
- Timebox: 7 min
- Signature:
```csharp
static (List<long> sums, List<int> maxes) LevelAggregates(BNode? root)
```
- Must log: `sum` and `max` per level.

### L3-11: Left view (BFS)
- Timebox: 6 min
- Signature:
```csharp
static List<int> LeftView(BNode? root)
```
- Must log: selected node each level.

### L3-12: Right view (BFS)
- Timebox: 6 min
- Signature:
```csharp
static List<int> RightView(BNode? root)
```
- Must log: selected node each level.

### L3-13: BFS width (simple)
- Timebox: 4 min
- Signature:
```csharp
static int MaxLevelWidth(BNode? root)
```
- Must log: each `levelSize`.

### L3-14: Validate BST using iterative inorder
- Timebox: 8 min
- Signature:
```csharp
static bool IsValidBstIterative(BNode? root)
```
- Contract: duplicates policy must be stated in a comment.
- Must log: previous value tracking.

### L3-15: Kth smallest in BST (iterative inorder)
- Timebox: 8 min
- Signature:
```csharp
static int KthSmallest(BNode? root, int k)
```
- Contract: invalid k => throw.
- Must log: decrement of k.

### L3-16: BST iterator skeleton (IEnumerable)
- Timebox: 10 min
- Task: implement an iterator class; no need to integrate with `yield`.
- Signature:
```csharp
public sealed class BstIterator
{
    public BstIterator(BNode? root) { }
    public bool HasNext() => throw new NotImplementedException();
    public int Next() => throw new NotImplementedException();
}
```
- Must log: stack size per Next().

### L3-17: Parent-pointer inorder successor
- Timebox: 8 min
- Signature:
```csharp
static PNode? InorderSuccessor(PNode? x)
```
- Contract: null input => return null.
- Must log: climb steps.

### L3-18: Populate next-right pointers (BFS)
- Timebox: 10 min
- Task: define your own Node class with `Next` pointer.
- Must log: per-level linking operations.

### L3-19: Level-order serialization (BFS tokens)
- Timebox: 10 min
- Signature:
```csharp
static List<string> SerializeLevelOrder(BNode? root)
```
- Contract: choose null marker and document it.
- Must log: queue tokens.

### L3-20: Level-order deserialization
- Timebox: 10 min
- Signature:
```csharp
static BNode? DeserializeLevelOrder(IList<string> tokens)
```
- Contract: invalid tokens => throw.
- Must log: token index and node creation.

---

# Level 4 🟣 Coordinates + composite traversals (15 drills)

### L4-01: Vertical order (stable BFS, no sorting)
- Timebox: 10 min
- Signature:
```csharp
static List<List<int>> VerticalOrder_Stable(BNode? root)
```
- Contract: define what happens when multiple nodes share same column.
- Must log: `(node.Val, col)`.

### L4-02: Vertical traversal (strict tie-break sorting)
- Timebox: 12 min
- Signature:
```csharp
static List<List<int>> VerticalTraversal_Sorted(BNode? root)
```
- Contract: document sort key: col asc, row asc, val asc.
- Must log: collected triples count.

### L4-03: Top view
- Timebox: 8 min
- Signature:
```csharp
static List<int> TopView(BNode? root)
```
- Must log: first-fill per column.

### L4-04: Bottom view
- Timebox: 8 min
- Signature:
```csharp
static List<int> BottomView(BNode? root)
```
- Must log: overwrite per column.

### L4-05: Diagonal traversal
- Timebox: 10 min
- Signature:
```csharp
static List<List<int>> DiagonalTraversal(BNode? root)
```
- Contract: define diagonal rule in a comment.
- Must log: diagonal id assignments.

### L4-06: Boundary traversal (anti-clockwise)
- Timebox: 12 min
- Signature:
```csharp
static List<int> BoundaryAntiClockwise(BNode? root)
```
- Contract: de-dup leaves.
- Must log: which sub-pass emitted each value.

### L4-07: Leaves only (used by boundary)
- Timebox: 6 min
- Signature:
```csharp
static List<int> LeavesOnly(BNode? root)
```

### L4-08: Root-to-leaf paths (list of lists)
- Timebox: 10 min
- Signature:
```csharp
static List<List<int>> RootToLeafPaths(BNode? root)
```
- Must log: path length on enter/exit.

### L4-09: Path sum existence (backtracking)
- Timebox: 10 min
- Signature:
```csharp
static bool HasRootToLeafSum(BNode? root, int target)
```
- Must log: remaining target.

### L4-10: Euler tin/tout times (adjacency rooted)
- Timebox: 12 min
- Signature:
```csharp
static (int[] tin, int[] tout) EulerTimes(int root, List<int>[] adj)
```
- Contract: tree is connected.
- Must log: (u, tin[u], tout[u]).

### L4-11: Subtree range flatten (Euler order array)
- Timebox: 12 min
- Signature:
```csharp
static (int[] order, int[] tin, int[] tout) EulerFlatten(int root, List<int>[] adj)
```
- Must log: order length.

### L4-12: Ancestor check using tin/tout
- Timebox: 6 min
- Signature:
```csharp
static bool IsAncestor(int u, int v, int[] tin, int[] tout)
```
- Contract: arrays valid.

### L4-13: Trie prefix enumeration
- Timebox: 10 min
- Signature:
```csharp
static List<string> TrieWordsWithPrefix(TrieNode root, string prefix)
```
- Contract: if prefix missing => empty list.
- Must log: node count visited.

### L4-14: N-ary level order
- Timebox: 8 min
- Signature:
```csharp
static List<List<int>> NaryLevelOrder(NNode? root)
```

### L4-15: N-ary zigzag
- Timebox: 10 min
- Signature:
```csharp
static List<List<int>> NaryZigzag(NNode? root)
```

---

# Level 5 🔴 Tree DP + advanced constraints (10 drills)

### L5-01: Diameter pattern (return summary + update global)
- Timebox: 12 min
- Signature:
```csharp
static int Diameter(BNode? root)
```
- Must log: summary returned at each node.

### L5-02: Max path sum pattern
- Timebox: 12 min
- Signature:
```csharp
static int MaxPathSum(BNode? root)
```
- Must log: best-down and global best.

### L5-03: Balanced tree check (height + early stop)
- Timebox: 10 min
- Signature:
```csharp
static bool IsBalanced(BNode? root)
```
- Must log: sentinel propagation.

### L5-04: Morris inorder traversal (implement carefully)
- Timebox: 15 min
- Signature:
```csharp
static List<int> InorderMorris(BNode? root)
```
- Contract: tree must be restored.
- Must log: thread create/remove operations.

### L5-05: Implicit search tree (state space) DFS skeleton
- Timebox: 8 min
- Task: write a generic DFS that expands a state into next states.
- Signature:
```csharp
static void DfsState<T>(T start, Func<T, IEnumerable<T>> expand, Action<T> visit)
```
- Contract: avoid cycles if expand can repeat states.

### L5-06: BFS over implicit states skeleton
- Timebox: 8 min
- Signature:
```csharp
static void BfsState<T>(T start, Func<T, IEnumerable<T>> expand, Action<T> visit)
```

### L5-07: Tree from edges: compute subtree sizes
- Timebox: 12 min
- Signature:
```csharp
static int[] SubtreeSizes(int root, List<int>[] adj)
```
- Must log: size computed per node.

### L5-08: Tree from edges: farthest node by BFS (diameter helper)
- Timebox: 10 min
- Signature:
```csharp
static (int node, int dist) FarthestNode(int start, List<int>[] adj)
```

### L5-09: Iterator with parent pointers (no stack)
- Timebox: 12 min
- Task: design Next() using parent-pointer successor only.
- Signature: up to you.

### L5-10: Serialization robustness checklist
- Timebox: 6 min
- Task: write a checklist comment for token validity and error handling.

---

# ✅ LeetCode problem set (tree traversal-focused)

Use this list after you can complete 70–80% of the drills without looking up templates.

## DFS orders + basics
- 144 Binary Tree Preorder Traversal
- 94 Binary Tree Inorder Traversal
- 145 Binary Tree Postorder Traversal
- 104 Maximum Depth of Binary Tree
- 111 Minimum Depth of Binary Tree
- 100 Same Tree
- 101 Symmetric Tree

## Iterative DFS + BST inorder applications
- 94 Inorder Traversal (iterative)
- 230 Kth Smallest Element in a BST
- 98 Validate Binary Search Tree

## BFS level-order family
- 102 Binary Tree Level Order Traversal
- 103 Binary Tree Zigzag Level Order Traversal
- 199 Binary Tree Right Side View
- 515 Find Largest Value in Each Tree Row

## Structural pointers / iterators
- 116 Populating Next Right Pointers in Each Node
- 173 Binary Search Tree Iterator

## Tree DP + path-state
- 543 Diameter of Binary Tree
- 124 Binary Tree Maximum Path Sum
- 113 Path Sum II
- 437 Path Sum III

## Advanced follow-ups (optional)
- 99 Recover Binary Search Tree (inorder reasoning; Morris as follow-up)

---

## Notes (to keep this “no solutions”)
- This file intentionally provides **signatures, contracts, and logging targets** but no implementations.
- If you want an auto-checking harness without revealing solutions, we can generate a harness that reads expected outputs from hidden files you keep locally.
