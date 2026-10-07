import unittest
from phase09_binary_trees.tree_node import TreeNode
from phase09_binary_trees.problem49_same_tree import is_same_tree

class TestProblem49(unittest.TestCase):
    def test_cases(self):
        p = TreeNode.from_level_order([1, 2, 3])
        q = TreeNode.from_level_order([1, 2, 3])
        self.assertTrue(is_same_tree(p, q))

        p2 = TreeNode.from_level_order([1, 2])
        q2 = TreeNode.from_level_order([1, None, 2])
        self.assertFalse(is_same_tree(p2, q2))

if __name__ == "__main__":
    unittest.main()
