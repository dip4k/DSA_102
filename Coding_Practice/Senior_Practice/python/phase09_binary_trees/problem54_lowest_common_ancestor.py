from .tree_node import TreeNode

def lowest_common_ancestor(root: TreeNode | None, p: TreeNode | None, q: TreeNode | None) -> TreeNode | None:
    if not root or root == p or root == q:
        return root

    left = lowest_common_ancestor(root.left, p, q)
    right = lowest_common_ancestor(root.right, p, q)

    if left and right:
        return root
    return left or right
