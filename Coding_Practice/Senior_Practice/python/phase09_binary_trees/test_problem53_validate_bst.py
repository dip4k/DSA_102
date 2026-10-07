import unittest
from phase09_binary_trees.tree_node import TreeNode
from phase09_binary_trees.problem53_validate_bst import is_valid_bst

class TestProblem53(unittest.TestCase):
    def test_cases(self):
        valid = TreeNode.from_level_order([2, 1, 3])
        self.assertTrue(is_valid_bst(valid))

        invalid = TreeNode.from_level_order([5, 1, 4, None, None, 3, 6])
        self.assertFalse(is_valid_bst(invalid))

if __name__ == "__main__":
    unittest.main()
