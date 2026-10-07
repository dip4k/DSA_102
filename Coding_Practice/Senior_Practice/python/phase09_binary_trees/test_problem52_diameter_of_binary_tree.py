import unittest
from phase09_binary_trees.tree_node import TreeNode
from phase09_binary_trees.problem52_diameter_of_binary_tree import diameter_of_binary_tree

class TestProblem52(unittest.TestCase):
    def test_cases(self):
        root = TreeNode.from_level_order([1, 2, 3, 4, 5])
        self.assertEqual(diameter_of_binary_tree(root), 3)

        root2 = TreeNode.from_level_order([1, 2])
        self.assertEqual(diameter_of_binary_tree(root2), 1)

if __name__ == "__main__":
    unittest.main()
