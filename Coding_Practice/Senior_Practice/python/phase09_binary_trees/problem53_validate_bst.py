from .tree_node import TreeNode

def is_valid_bst(root: TreeNode | None) -> bool:
    def validate(node: TreeNode | None, min_val: float, max_val: float) -> bool:
        if not node:
            return True
        if not (min_val < node.val < max_val):
            return False
        return validate(node.left, min_val, node.val) and validate(node.right, node.val, max_val)

    return validate(root, float("-inf"), float("inf"))
