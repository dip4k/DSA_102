# Week 07 Extended Python Complete v13

Purpose: build Python fluency for binary trees, BSTs, balanced-tree reasoning, and common tree patterns.

## Focus tags
- Must: traversals, BST search/insert/delete reasoning, diameter, path sums, LCA
- Should: balanced BST concepts, serialization, order-statistics ideas
- Optional: advanced augmentations and library mapping discussion

## Python mindset for Week 07
- Trees in interviews are usually custom node classes.
- Recursion is natural but deep skewed trees may need iterative traversal.
- Python has no built-in balanced BST in the standard library, so focus on concept mastery and node logic.

## Node template
```python
class TreeNode:
    def __init__(self, val=0, left=None, right=None):
        self.val = val
        self.left = left
        self.right = right
```

## Pattern 1: recursive traversals
```python
def preorder(root):
    if not root:
        return []
    return [root.val] + preorder(root.left) + preorder(root.right)


def inorder(root):
    if not root:
        return []
    return inorder(root.left) + [root.val] + inorder(root.right)
```

## Pattern 2: iterative inorder
```python
def inorder_iter(root):
    stack = []
    curr = root
    out = []
    while stack or curr:
        while curr:
            stack.append(curr)
            curr = curr.left
        curr = stack.pop()
        out.append(curr.val)
        curr = curr.right
    return out
```

Invariant:
- stack holds the path to the next unvisited inorder node

## Pattern 3: BST search
```python
def bst_search(root, target):
    curr = root
    while curr:
        if curr.val == target:
            return curr
        if target < curr.val:
            curr = curr.left
        else:
            curr = curr.right
    return None
```

## Pattern 4: tree diameter
```python
def diameter(root):
    best = 0
    def dfs(node):
        nonlocal best
        if not node:
            return 0
        left = dfs(node.left)
        right = dfs(node.right)
        best = max(best, left + right)
        return 1 + max(left, right)
    dfs(root)
    return best
```

## Pattern 5: lowest common ancestor
```python
def lca(root, p, q):
    if not root or root is p or root is q:
        return root
    left = lca(root.left, p, q)
    right = lca(root.right, p, q)
    if left and right:
        return root
    return left or right
```

## Quick practice ladder
- Must: preorder/inorder/postorder, max depth, validate BST, diameter, path sum, LCA
- Should: serialize/deserialize, kth smallest in BST, balanced tree check
- Optional: augmented BST reasoning and order statistics

## Common Python pitfalls
- Using recursion on highly skewed trees without considering depth.
- Forgetting that inorder is sorted only for BSTs.
- Mixing node identity and node value when solving LCA/intersection-style questions.
