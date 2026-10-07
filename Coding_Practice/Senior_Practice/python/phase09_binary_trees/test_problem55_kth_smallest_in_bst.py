import unittest
from phase09_binary_trees.tree_node import TreeNode
from phase09_binary_trees.problem55_kth_smallest_in_bst import kth_smallest

class TestProblem55(unittest.TestCase):
    def test_cases(self):
        root = TreeNode.from_level_order([3, 1, 4, None, 2])
        self.assertEqual(kth_smallest(root, 1), 1)

        root2 = TreeNode.from_level_order([5, 3, 6, 2, 4, None, None, 1])
        self.assertEqual(kth_smallest(root2, 3), 3)

if __name__ == "__main__":
    unittest.main()
