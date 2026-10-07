import unittest
from phase08_binary_search.problem44_search_in_rotated_sorted_array import search_rotated

class TestProblem44(unittest.TestCase):
    def test_cases(self):
        self.assertEqual(search_rotated([4, 5, 6, 7, 0, 1, 2], 0), 4)
        self.assertEqual(search_rotated([4, 5, 6, 7, 0, 1, 2], 3), -1)
        self.assertEqual(search_rotated([1], 0), -1)
        self.assertEqual(search_rotated([5, 1, 3], 5), 0)

if __name__ == "__main__":
    unittest.main()
