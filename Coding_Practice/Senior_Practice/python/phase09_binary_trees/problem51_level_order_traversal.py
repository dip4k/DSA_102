from collections import deque
from .tree_node import TreeNode

def level_order(root: TreeNode | None) -> list[list[int]]:
    result: list[list[int]] = []
    if not root:
        return result

    q = deque([root])
    while q:
        level_size = len(q)
        current_level: list[int] = []
        for _ in range(level_size):
            node = q.popleft()
            current_level.append(node.val)
            if node.left:
                q.append(node.left)
            if node.right:
                q.append(node.right)
        result.append(current_level)

    return result
