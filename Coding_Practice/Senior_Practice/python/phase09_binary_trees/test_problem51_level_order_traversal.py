import unittest
from phase09_binary_trees.tree_node import TreeNode
from phase09_binary_trees.problem51_level_order_traversal import level_order

class TestProblem51(unittest.TestCase):
    def test_cases(self):
        root = TreeNode.from_level_order([3, 9, 20, None, None, 15, 7])
        res = level_order(root)
        self.assertEqual(res, [[3], [9, 20], [15, 7]])

if __name__ == "__main__":
    unittest.main()
