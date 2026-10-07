import unittest
from phase09_binary_trees.tree_node import TreeNode
from phase09_binary_trees.problem50_invert_binary_tree import invert_tree

class TestProblem50(unittest.TestCase):
    def test_cases(self):
        root = TreeNode.from_level_order([4, 2, 7, 1, 3, 6, 9])
        inv = invert_tree(root)
        self.assertEqual(inv.val, 4)
        self.assertEqual(inv.left.val, 7)
        self.assertEqual(inv.right.val, 2)

if __name__ == "__main__":
    unittest.main()
