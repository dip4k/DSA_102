import unittest
from phase09_binary_trees.tree_node import TreeNode
from phase09_binary_trees.problem54_lowest_common_ancestor import lowest_common_ancestor

class TestProblem54(unittest.TestCase):
    def test_cases(self):
        root = TreeNode(3)
        n5 = TreeNode(5)
        n1 = TreeNode(1)
        n4 = TreeNode(4)
        root.left = n5
        root.right = n1
        n5.right = TreeNode(2, None, n4)

        self.assertEqual(lowest_common_ancestor(root, n5, n1).val, 3)
        self.assertEqual(lowest_common_ancestor(root, n5, n4).val, 5)

if __name__ == "__main__":
    unittest.main()
