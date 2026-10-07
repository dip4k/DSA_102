from .tree_node import TreeNode

def diameter_of_binary_tree(root: TreeNode | None) -> int:
    max_diameter = 0

    def get_depth(node: TreeNode | None) -> int:
        nonlocal max_diameter
        if not node:
            return 0
        left = get_depth(node.left)
        right = get_depth(node.right)
        max_diameter = max(max_diameter, left + right)
        return 1 + max(left, right)

    get_depth(root)
    return max_diameter
