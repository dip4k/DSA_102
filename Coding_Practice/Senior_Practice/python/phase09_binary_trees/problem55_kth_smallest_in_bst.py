from .tree_node import TreeNode

def kth_smallest(root: TreeNode | None, k: int) -> int:
    stack: list[TreeNode] = []
    curr = root

    while curr or stack:
        while curr:
            stack.append(curr)
            curr = curr.left

        curr = stack.pop()
        k -= 1
        if k == 0:
            return curr.val

        curr = curr.right

    return -1
