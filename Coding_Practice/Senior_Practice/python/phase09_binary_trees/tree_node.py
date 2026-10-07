from collections import deque

class TreeNode:
    def __init__(self, val: int = 0, left: 'TreeNode | None' = None, right: 'TreeNode | None' = None):
        self.val = val
        self.left = left
        self.right = right

    @staticmethod
    def from_level_order(values: list[int | None]) -> 'TreeNode | None':
        if not values or values[0] is None:
            return None

        root = TreeNode(values[0])
        q = deque([root])
        i = 1

        while q and i < len(values):
            curr = q.popleft()
            if i < len(values) and values[i] is not None:
                curr.left = TreeNode(values[i])
                q.append(curr.left)
            i += 1
            if i < len(values) and values[i] is not None:
                curr.right = TreeNode(values[i])
                q.append(curr.right)
            i += 1

        return root
