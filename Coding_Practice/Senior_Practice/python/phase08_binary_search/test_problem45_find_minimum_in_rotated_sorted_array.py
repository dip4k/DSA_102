import unittest
from phase08_binary_search.problem45_find_minimum_in_rotated_sorted_array import find_min

class TestProblem45(unittest.TestCase):
    def test_cases(self):
        self.assertEqual(find_min([3, 4, 5, 1, 2]), 1)
        self.assertEqual(find_min([4, 5, 6, 7, 0, 1, 2]), 0)
        self.assertEqual(find_min([11, 13, 15, 17]), 11)
        self.assertEqual(find_min([2, 1]), 1)

if __name__ == "__main__":
    unittest.main()
