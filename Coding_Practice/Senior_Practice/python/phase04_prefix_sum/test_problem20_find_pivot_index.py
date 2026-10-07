import unittest
from phase04_prefix_sum.problem20_find_pivot_index import pivot_index

class TestProblem20(unittest.TestCase):
    def test_cases(self):
        self.assertEqual(pivot_index([1, 7, 3, 6, 5, 6]), 3)
        self.assertEqual(pivot_index([1, 2, 3]), -1)
        self.assertEqual(pivot_index([2, 1, -1]), 0)

if __name__ == "__main__":
    unittest.main()
