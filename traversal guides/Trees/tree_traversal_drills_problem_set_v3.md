# 🌳 Tree Traversal Drills & Problem Set (v1)

This pack is split into two parts:
- **Part 1 – Drills:** Small, focused coding drills with **Python and C# skeletons + driver stubs**, no solutions.
- **Part 2 – Problem Set:** Curated LeetCode problems with timeboxes and invariants, aligned to traversal mastery.

---

## Part 1 – Tree Traversal Drills (Code Skeletons Only)

### Shared Binary Tree Definitions

Use these node definitions across all drills.

#### Python
```python
from collections import deque
from typing import List, Optional

class TreeNode:
    def __init__(self, val: int = 0, left: 'Optional[TreeNode]' = None, right: 'Optional[TreeNode]' = None):
        self.val = val
        self.left = left
        self.right = right


def build_sample_tree() -> Optional[TreeNode]:
    """Builds a small sample tree for manual testing.

            1
           / \
          2   3
         / \   \
        4   5   6
    """
    n4 = TreeNode(4)
    n5 = TreeNode(5)
    n6 = TreeNode(6)
    n2 = TreeNode(2, n4, n5)
    n3 = TreeNode(3, None, n6)
    root = TreeNode(1, n2, n3)
    return root
```

#### C#
```csharp
using System;
using System.Collections.Generic;

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

public static class TreeBuilder
{
    // Builds a small sample tree for manual testing.
    //         1
    //        / \
    //       2   3
    //      / \   \
    //     4   5   6
    public static TreeNode BuildSampleTree()
    {
        var n4 = new TreeNode(4);
        var n5 = new TreeNode(5);
        var n6 = new TreeNode(6);
        var n2 = new TreeNode(2, n4, n5);
        var n3 = new TreeNode(3, null, n6);
        var root = new TreeNode(1, n2, n3);
        return root;
    }
}
```

---

### Drill 1.1 – Recursive Preorder Traversal (Root, Left, Right)

**Goal:** Implement a recursive preorder traversal that returns the list of node values.

- Visit order: **root → left subtree → right subtree**.
- Empty tree should return an empty list.

#### Python Skeleton
```python
def preorder_traversal(root: Optional[TreeNode]) -> List[int]:
    """Return preorder traversal of the tree rooted at `root`.

    Preorder: Root, Left, Right.
    """
    # TODO: implement recursive preorder traversal
    raise NotImplementedError


if __name__ == "__main__":
    root = build_sample_tree()
    result = preorder_traversal(root)
    print("Preorder:", result)
```

#### C# Skeleton
```csharp
public static class Drill11
{
    // Return preorder traversal of the tree rooted at `root`.
    // Preorder: Root, Left, Right.
    public static IList<int> PreorderTraversal(TreeNode root)
    {
        // TODO: implement recursive preorder traversal
        throw new NotImplementedException();
    }

    public static void Run()
    {
        var root = TreeBuilder.BuildSampleTree();
        var result = PreorderTraversal(root);
        Console.WriteLine("Preorder: " + string.Join(", ", result));
    }
}
```

---

### Drill 1.2 – Recursive Inorder Traversal (Left, Root, Right)

**Goal:** Implement a recursive inorder traversal that returns the list of node values.

- Visit order: **left subtree → root → right subtree**.
- For a valid BST, the output should be sorted ascending.

#### Python Skeleton
```python
def inorder_traversal(root: Optional[TreeNode]) -> List[int]:
    """Return inorder traversal of the tree rooted at `root`.

    Inorder: Left, Root, Right.
    """
    # TODO: implement recursive inorder traversal
    raise NotImplementedError


if __name__ == "__main__":
    root = build_sample_tree()
    result = inorder_traversal(root)
    print("Inorder:", result)
```

#### C# Skeleton
```csharp
public static class Drill12
{
    // Return inorder traversal of the tree rooted at `root`.
    // Inorder: Left, Root, Right.
    public static IList<int> InorderTraversal(TreeNode root)
    {
        // TODO: implement recursive inorder traversal
        throw new NotImplementedException();
    }

    public static void Run()
    {
        var root = TreeBuilder.BuildSampleTree();
        var result = InorderTraversal(root);
        Console.WriteLine("Inorder: " + string.Join(", ", result));
    }
}
```

---

### Drill 1.3 – Recursive Postorder Traversal (Left, Right, Root)

**Goal:** Implement a recursive postorder traversal that returns the list of node values.

- Visit order: **left subtree → right subtree → root**.
- Often used when computing properties bottom-up (heights, sums, etc.).

#### Python Skeleton
```python
def postorder_traversal(root: Optional[TreeNode]) -> List[int]:
    """Return postorder traversal of the tree rooted at `root`.

    Postorder: Left, Right, Root.
    """
    # TODO: implement recursive postorder traversal
    raise NotImplementedError


if __name__ == "__main__":
    root = build_sample_tree()
    result = postorder_traversal(root)
    print("Postorder:", result)
```

#### C# Skeleton
```csharp
public static class Drill13
{
    // Return postorder traversal of the tree rooted at `root`.
    // Postorder: Left, Right, Root.
    public static IList<int> PostorderTraversal(TreeNode root)
    {
        // TODO: implement recursive postorder traversal
        throw new NotImplementedException();
    }

    public static void Run()
    {
        var root = TreeBuilder.BuildSampleTree();
        var result = PostorderTraversal(root);
        Console.WriteLine("Postorder: " + string.Join(", ", result));
    }
}
```

---

### Drill 2.1 – Iterative Inorder Traversal (Explicit Stack)

**Goal:** Implement iterative inorder traversal using an explicit stack.

- Invariant: the stack always contains the path from root to the current node being explored.

#### Python Skeleton
```python
def inorder_traversal_iterative(root: Optional[TreeNode]) -> List[int]:
    """Iterative inorder traversal using an explicit stack."""
    # TODO: implement iterative inorder traversal
    raise NotImplementedError


if __name__ == "__main__":
    root = build_sample_tree()
    result = inorder_traversal_iterative(root)
    print("Inorder (iterative):", result)
```

#### C# Skeleton
```csharp
public static class Drill21
{
    // Iterative inorder traversal using an explicit stack.
    public static IList<int> InorderTraversalIterative(TreeNode root)
    {
        // TODO: implement iterative inorder traversal
        throw new NotImplementedException();
    }

    public static void Run()
    {
        var root = TreeBuilder.BuildSampleTree();
        var result = InorderTraversalIterative(root);
        Console.WriteLine("Inorder (iterative): " + string.Join(", ", result));
    }
}
```

---

### Drill 2.2 – Iterative Preorder Traversal (Explicit Stack)

**Goal:** Implement iterative preorder traversal using a stack.

- Hint: push right child first, then left child, so that left is processed first.

#### Python Skeleton
```python
def preorder_traversal_iterative(root: Optional[TreeNode]) -> List[int]:
    """Iterative preorder traversal using a stack."""
    # TODO: implement iterative preorder traversal
    raise NotImplementedError


if __name__ == "__main__":
    root = build_sample_tree()
    result = preorder_traversal_iterative(root)
    print("Preorder (iterative):", result)
```

#### C# Skeleton
```csharp
public static class Drill22
{
    // Iterative preorder traversal using a stack.
    public static IList<int> PreorderTraversalIterative(TreeNode root)
    {
        // TODO: implement iterative preorder traversal
        throw new NotImplementedException();
    }

    public static void Run()
    {
        var root = TreeBuilder.BuildSampleTree();
        var result = PreorderTraversalIterative(root);
        Console.WriteLine("Preorder (iterative): " + string.Join(", ", result));
    }
}
```

---

### Drill 2.3 – Iterative Postorder Traversal (Two Stacks or State Machine)

**Goal:** Implement iterative postorder traversal.

- You may use two stacks, or a single stack with a `(node, state)` frame.

#### Python Skeleton
```python
def postorder_traversal_iterative(root: Optional[TreeNode]) -> List[int]:
    """Iterative postorder traversal.

    You may use:
    - Two stacks, or
    - One stack with explicit state.
    """
    # TODO: implement iterative postorder traversal
    raise NotImplementedError


if __name__ == "__main__":
    root = build_sample_tree()
    result = postorder_traversal_iterative(root)
    print("Postorder (iterative):", result)
```

#### C# Skeleton
```csharp
public static class Drill23
{
    // Iterative postorder traversal.
    // You may use:
    // - Two stacks, or
    // - One stack with explicit state.
    public static IList<int> PostorderTraversalIterative(TreeNode root)
    {
        // TODO: implement iterative postorder traversal
        throw new NotImplementedException();
    }

    public static void Run()
    {
        var root = TreeBuilder.BuildSampleTree();
        var result = PostorderTraversalIterative(root);
        Console.WriteLine("Postorder (iterative): " + string.Join(", ", result));
    }
}
```

---

### Drill 3.1 – Level-Order Traversal (BFS)

**Goal:** Return a list of levels, where each level is a list of node values from left to right.

- Use a queue.
- Process one level at a time.

#### Python Skeleton
```python
def level_order(root: Optional[TreeNode]) -> List[List[int]]:
    """Return level-order traversal (list of levels)."""
    # TODO: implement BFS level-order traversal
    raise NotImplementedError


if __name__ == "__main__":
    root = build_sample_tree()
    levels = level_order(root)
    print("Level-order:", levels)
```

#### C# Skeleton
```csharp
public static class Drill31
{
    // Return level-order traversal (list of levels).
    public static IList<IList<int>> LevelOrder(TreeNode root)
    {
        // TODO: implement BFS level-order traversal
        throw new NotImplementedException();
    }

    public static void Run()
    {
        var root = TreeBuilder.BuildSampleTree();
        var levels = LevelOrder(root);
        Console.WriteLine("Level-order:");
        foreach (var lvl in levels)
        {
            Console.WriteLine("[" + string.Join(", ", lvl) + "]");
        }
    }
}
```

---

### Drill 3.2 – Zigzag Level-Order Traversal

**Goal:** Return level-order traversal where direction alternates each level:

- Level 0: left → right
- Level 1: right → left
- Level 2: left → right
- etc.

#### Python Skeleton
```python
def zigzag_level_order(root: Optional[TreeNode]) -> List[List[int]]:
    """Return zigzag level-order traversal."""
    # TODO: implement zigzag level-order traversal
    raise NotImplementedError


if __name__ == "__main__":
    root = build_sample_tree()
    levels = zigzag_level_order(root)
    print("Zigzag level-order:", levels)
```

#### C# Skeleton
```csharp
public static class Drill32
{
    // Return zigzag level-order traversal.
    public static IList<IList<int>> ZigzagLevelOrder(TreeNode root)
    {
        // TODO: implement zigzag level-order traversal
        throw new NotImplementedException();
    }

    public static void Run()
    {
        var root = TreeBuilder.BuildSampleTree();
        var levels = ZigzagLevelOrder(root);
        Console.WriteLine("Zigzag level-order:");
        foreach (var lvl in levels)
        {
            Console.WriteLine("[" + string.Join(", ", lvl) + "]");
        }
    }
}
```

---

### Drill 4.1 – Boundary Traversal of Binary Tree

**Goal:** Return the boundary of the binary tree in anti-clockwise order:

1. Root (if not a leaf)
2. Left boundary (top-down, excluding leaves)
3. All leaves (left to right)
4. Right boundary (bottom-up, excluding leaves)

#### Python Skeleton
```python
def boundary_traversal(root: Optional[TreeNode]) -> List[int]:
    """Return the boundary of the tree in anti-clockwise order.

    Order:
    - Root (if not leaf)
    - Left boundary (excluding leaves)
    - All leaves (left to right)
    - Right boundary (excluding leaves, bottom-up)
    """
    # TODO: implement boundary traversal
    raise NotImplementedError


if __name__ == "__main__":
    root = build_sample_tree()
    boundary = boundary_traversal(root)
    print("Boundary:", boundary)
```

#### C# Skeleton
```csharp
public static class Drill41
{
    // Return the boundary of the tree in anti-clockwise order.
    public static IList<int> BoundaryTraversal(TreeNode root)
    {
        // TODO: implement boundary traversal
        throw new NotImplementedException();
    }

    public static void Run()
    {
        var root = TreeBuilder.BuildSampleTree();
        var boundary = BoundaryTraversal(root);
        Console.WriteLine("Boundary: " + string.Join(", ", boundary));
    }
}
```

---

### Drill 4.2 – Root-to-Leaf Paths (Path-Based DFS Backtracking)

**Goal:** Return all root-to-leaf paths as lists of integers.

- Use DFS with a path list.
- Remember to **rollback** the path when backtracking.

#### Python Skeleton
```python
def root_to_leaf_paths(root: Optional[TreeNode]) -> List[List[int]]:
    """Return all root-to-leaf paths as lists of node values."""
    # TODO: implement path-based DFS with backtracking
    raise NotImplementedError


if __name__ == "__main__":
    root = build_sample_tree()
    paths = root_to_leaf_paths(root)
    print("Root-to-leaf paths:")
    for p in paths:
        print(p)
```

#### C# Skeleton
```csharp
public static class Drill42
{
    // Return all root-to-leaf paths as lists of node values.
    public static IList<IList<int>> RootToLeafPaths(TreeNode root)
    {
        // TODO: implement path-based DFS with backtracking
        throw new NotImplementedException();
    }

    public static void Run()
    {
        var root = TreeBuilder.BuildSampleTree();
        var paths = RootToLeafPaths(root);
        Console.WriteLine("Root-to-leaf paths:");
        foreach (var p in paths)
        {
            Console.WriteLine("[" + string.Join(", ", p) + "]");
        }
    }
}
```

---

## Part 2 – Tree Traversal Problem Set

Each subsection lists representative LeetCode problems. For each problem:
- Try to **state the traversal invariant** before coding.
- Timeboxes are approximate and assume focused practice.

---

### Level 1 – Recursive DFS Traversals

**Focus:** Basic preorder, inorder, postorder on binary trees.

1. **Binary Tree Preorder Traversal** – LeetCode 144
   - ⏱️ 20–35 minutes
   - Invariant: when the recursive call on a node returns, the preorder list for that subtree is complete.

2. **Binary Tree Inorder Traversal** – LeetCode 94
   - ⏱️ 20–35 minutes
   - Invariant: inorder traversal of a BST yields a sorted sequence of node values.

3. **Binary Tree Postorder Traversal** – LeetCode 145
   - ⏱️ 25–40 minutes
   - Invariant: a node is processed only after both its left and right subtrees have been fully processed.

4. **Maximum Depth of Binary Tree** – LeetCode 104
   - ⏱️ 20–35 minutes
   - Invariant: `dfs(node)` returns the correct height of the subtree rooted at `node`.

---

### Level 2 – BFS / Level-Order Views

**Focus:** Queue-based traversals, level snapshots, basic views.

1. **Binary Tree Level Order Traversal** – LeetCode 102
   - ⏱️ 25–40 minutes
   - Invariant: the queue contains exactly the nodes of the current and future levels; per-iteration processing consumes one full level.

2. **Binary Tree Level Order Traversal II** – LeetCode 107
   - ⏱️ 25–40 minutes
   - Invariant: level lists are built from top to bottom, then reversed or inserted accordingly to get bottom-up order.

3. **Binary Tree Right Side View** – LeetCode 199
   - ⏱️ 30–45 minutes
   - Invariant: for each depth, the view records exactly one node – the last node encountered at that level in BFS or the first in right-first DFS.

4. **Average of Levels in Binary Tree** – LeetCode 637
   - ⏱️ 25–40 minutes
   - Invariant: each BFS level is processed independently; sums and counts per level are correct before moving to the next.

---

### Level 3 – Tree DP and Path-Based Traversals

**Focus:** Using postorder for aggregation and DFS paths with backtracking.

1. **Path Sum** – LeetCode 112
   - ⏱️ 25–40 minutes
   - Invariant: along each path from root to current node, the running sum is correct; leaves are checked against target.

2. **Path Sum II** – LeetCode 113
   - ⏱️ 35–55 minutes
   - Invariant: the current path list always represents the exact root-to-current-node path; rollback happens after exploring each child.

3. **Diameter of Binary Tree** – LeetCode 543
   - ⏱️ 35–55 minutes
   - Invariant: `dfs(node)` returns the longest downward path starting at `node`; a global diameter is updated using left + right paths.

4. **Binary Tree Maximum Path Sum** – LeetCode 124 (advanced)
   - ⏱️ 50–80 minutes
   - Invariant: `dfs(node)` returns the maximum sum of a path starting at `node` and going down; global answer tracks any path passing through `node`.

---

### Level 4 – Iterative Traversals and Structural Pointers

**Focus:** Iterative control with stacks and pointer-based traversal enhancements.

1. **Kth Smallest Element in a BST** – LeetCode 230
   - ⏱️ 30–50 minutes
   - Invariant: iterative inorder traversal produces nodes in sorted order; decrementing `k` on each visit finds the kth element.

2. **Binary Search Tree Iterator** – LeetCode 173
   - ⏱️ 40–65 minutes
   - Invariant: the internal stack of the iterator always represents the path to the next smallest node.

3. **Populating Next Right Pointers in Each Node** – LeetCode 116
   - ⏱️ 40–65 minutes
   - Invariant: for each level, all `next` pointers between siblings are correctly linked before moving to the next level.

4. **Populating Next Right Pointers in Each Node II** – LeetCode 117 (advanced)
   - ⏱️ 50–80 minutes
   - Invariant: traversal using `next` pointers maintains correct frontier across uneven levels; no node is skipped or linked incorrectly.

---

### Level 5 – Views, Coordinates, and Boundary

**Focus:** Traversal with extra state (columns, depth), boundary extraction.

1. **Binary Tree Vertical Order Traversal** – LeetCode 314 (or 987: Vertical Order Traversal of a Binary Tree)
   - ⏱️ 50–80 minutes
   - Invariant: each node is assigned a consistent `(row, col)`; nodes are grouped by column and ordered by row (and possibly value).

2. **Binary Tree Boundary** – (e.g., Boundary of Binary Tree, commonly in interview sets)
   - ⏱️ 50–80 minutes
   - Invariant: each node appears at most once in the boundary; left boundary excludes leaves, right boundary collected bottom-up, leaves are all leaf nodes.

3. **Binary Tree Zigzag Level Order Traversal** – LeetCode 103
   - ⏱️ 30–50 minutes
   - Invariant: nodes are grouped by level via BFS; direction alternates per level while preserving membership.

4. **Binary Tree Paths** – LeetCode 257
   - ⏱️ 25–40 minutes
   - Invariant: the current path string or list matches the recursion stack; rollback happens after visiting each child.

---

### Recommended Practice Flow

1. Finish **Drills 1.x and 2.x** until recursive and iterative DFS are automatic.
2. Add **Drills 3.x** and Level 2 problems to internalize BFS level-order.
3. Use **Drill 4.2** with Level 3 problems to master path-based DFS and tree DP.
4. Finally, tackle **boundary and coordinate** problems at Level 5 for full traversal mastery.
