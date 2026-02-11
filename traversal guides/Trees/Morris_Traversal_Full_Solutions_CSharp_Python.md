# Morris Traversal — Full Implementations (C# + Python)

This file contains complete working implementations of Morris traversal for **inorder** and **preorder** (and an advanced **postorder** variant) in both **C#** and **Python**. [web:41][web:104][web:112]

---

## What you get
- Morris inorder (O(1) auxiliary space; restores tree) [web:41]
- Morris preorder (O(1) auxiliary space; restores tree) [web:104]
- Morris postorder (advanced; O(1) auxiliary space; restores tree) [web:112]

---

# C# Implementation

## Binary tree node

```csharp
public sealed class TreeNode
{
    public int val;
    public TreeNode? left;
    public TreeNode? right;

    public TreeNode(int val = 0, TreeNode? left = null, TreeNode? right = null)
    {
        this.val = val;
        this.left = left;
        this.right = right;
    }
}
```

## Morris inorder

```csharp
using System.Collections.Generic;

public static class MorrisTraversal
{
    public static IList<int> Inorder(TreeNode? root)
    {
        var res = new List<int>();
        var curr = root;

        while (curr != null)
        {
            if (curr.left == null)
            {
                res.Add(curr.val);
                curr = curr.right;
            }
            else
            {
                var pred = curr.left;
                while (pred.right != null && pred.right != curr)
                    pred = pred.right;

                if (pred.right == null)
                {
                    pred.right = curr;      // create thread
                    curr = curr.left;
                }
                else
                {
                    pred.right = null;      // remove thread
                    res.Add(curr.val);
                    curr = curr.right;
                }
            }
        }

        return res;
    }
}
```

## Morris preorder

```csharp
using System.Collections.Generic;

public static partial class MorrisTraversal
{
    public static IList<int> Preorder(TreeNode? root)
    {
        var res = new List<int>();
        var curr = root;

        while (curr != null)
        {
            if (curr.left == null)
            {
                res.Add(curr.val);
                curr = curr.right;
            }
            else
            {
                var pred = curr.left;
                while (pred.right != null && pred.right != curr)
                    pred = pred.right;

                if (pred.right == null)
                {
                    res.Add(curr.val);      // preorder visit on first encounter
                    pred.right = curr;      // create thread
                    curr = curr.left;
                }
                else
                {
                    pred.right = null;      // remove thread
                    curr = curr.right;
                }
            }
        }

        return res;
    }
}
```

## Morris postorder (advanced)

This variant uses a dummy root and reverses the right edges along a path to emit nodes in correct postorder, then restores edges. [web:112]

```csharp
using System.Collections.Generic;

public static partial class MorrisTraversal
{
    public static IList<int> Postorder(TreeNode? root)
    {
        var res = new List<int>();
        var dummy = new TreeNode(0, left: root, right: null);
        TreeNode? curr = dummy;

        while (curr != null)
        {
            if (curr.left == null)
            {
                curr = curr.right;
            }
            else
            {
                var pred = curr.left;
                while (pred.right != null && pred.right != curr)
                    pred = pred.right;

                if (pred.right == null)
                {
                    pred.right = curr;      // create thread
                    curr = curr.left;
                }
                else
                {
                    // pred.right == curr: remove thread, but first output the path
                    AddReversePath(curr.left!, pred, res);
                    pred.right = null;      // remove thread
                    curr = curr.right;
                }
            }
        }

        return res;
    }

    private static void AddReversePath(TreeNode from, TreeNode to, List<int> res)
    {
        ReverseRightSpine(from, to);

        var node = to;
        while (true)
        {
            res.Add(node.val);
            if (node == from) break;
            node = node.right!;
        }

        ReverseRightSpine(to, from);
    }

    private static void ReverseRightSpine(TreeNode from, TreeNode to)
    {
        if (from == to) return;

        TreeNode? prev = null;
        var curr = from;
        while (prev != to)
        {
            var next = curr.right;
            curr.right = prev;
            prev = curr;
            curr = next!;
        }
    }
}
```

## Quick sanity test (C#)

```csharp
// Tree:
//     1
//    / \
//   2   3
//  / \   \
// 4  5    6
var root = new TreeNode(1,
    left: new TreeNode(2, new TreeNode(4), new TreeNode(5)),
    right: new TreeNode(3, null, new TreeNode(6)));

var ino = MorrisTraversal.Inorder(root);    // 4,2,5,1,3,6
var pre = MorrisTraversal.Preorder(root);  // 1,2,4,5,3,6
var post = MorrisTraversal.Postorder(root); // 4,5,2,6,3,1
```

---

# Python Implementation

## Binary tree node

```python
class TreeNode:
    def __init__(self, val=0, left=None, right=None):
        self.val = val
        self.left = left
        self.right = right
```

## Morris inorder

```python
def morris_inorder(root):
    res = []
    curr = root

    while curr is not None:
        if curr.left is None:
            res.append(curr.val)
            curr = curr.right
        else:
            pred = curr.left
            while pred.right is not None and pred.right is not curr:
                pred = pred.right

            if pred.right is None:
                pred.right = curr   # create thread
                curr = curr.left
            else:
                pred.right = None   # remove thread
                res.append(curr.val)
                curr = curr.right

    return res
```

## Morris preorder

```python
def morris_preorder(root):
    res = []
    curr = root

    while curr is not None:
        if curr.left is None:
            res.append(curr.val)
            curr = curr.right
        else:
            pred = curr.left
            while pred.right is not None and pred.right is not curr:
                pred = pred.right

            if pred.right is None:
                res.append(curr.val)  # preorder visit on first encounter
                pred.right = curr     # create thread
                curr = curr.left
            else:
                pred.right = None     # remove thread
                curr = curr.right

    return res
```

## Morris postorder (advanced)

This version uses a dummy root and reverses right edges along a path to emit nodes in correct postorder, then restores edges. [web:112]

```python
def morris_postorder(root):
    res = []
    dummy = TreeNode(0)
    dummy.left = root
    curr = dummy

    def reverse_right_spine(frm, to):
        if frm is to:
            return
        prev = None
        node = frm
        while prev is not to:
            nxt = node.right
            node.right = prev
            prev = node
            node = nxt

    def add_reverse_path(frm, to):
        reverse_right_spine(frm, to)
        node = to
        while True:
            res.append(node.val)
            if node is frm:
                break
            node = node.right
        reverse_right_spine(to, frm)

    while curr is not None:
        if curr.left is None:
            curr = curr.right
        else:
            pred = curr.left
            while pred.right is not None and pred.right is not curr:
                pred = pred.right

            if pred.right is None:
                pred.right = curr
                curr = curr.left
            else:
                add_reverse_path(curr.left, pred)
                pred.right = None
                curr = curr.right

    return res
```

## Quick sanity test (Python)

```python
# Tree:
#     1
#    / \
#   2   3
#  / \   \
# 4  5    6
root = TreeNode(1,
    left=TreeNode(2, TreeNode(4), TreeNode(5)),
    right=TreeNode(3, None, TreeNode(6))
)

print(morris_inorder(root))    # [4, 2, 5, 1, 3, 6]
print(morris_preorder(root))   # [1, 2, 4, 5, 3, 6]
print(morris_postorder(root))  # [4, 5, 2, 6, 3, 1]
```

---

## Notes you should keep in mind
- Morris traversal temporarily mutates pointers; always ensure threads/edges are restored or later operations on the tree may break. [web:41]
- Postorder Morris is significantly easier to get wrong than inorder/preorder; keep it as an “advanced follow-up” unless explicitly required. [web:112]
