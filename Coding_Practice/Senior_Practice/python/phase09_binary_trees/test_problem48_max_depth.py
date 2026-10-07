import unittest
from phase09_binary_trees.tree_node import TreeNode
from phase09_binary_trees.problem48_max_depth import max_depth

class TestProblem48(unittest.TestCase):
    def test_cases(self):
        t1 = TreeNode.from_level_order([3, 9, 20, None, None, 15, 7])
        self.assertEqual(max_depth(t1), 3)

        t2 = TreeNode.from_level_order([1, None, 2])
        self.assertEqual(max_depth(t2), 2)
        self.assertEqual(max_depth(None), 0)

if __name__ == "__main__":
    unittest.main()
