import unittest
from phase08_binary_search.problem47_search_2d_matrix import search_matrix

class TestProblem47(unittest.TestCase):
    def test_cases(self):
        m = [
            [1, 3, 5, 7],
            [10, 11, 16, 20],
            [23, 30, 34, 60]
        ]
        self.assertTrue(search_matrix(m, 3))
        self.assertFalse(search_matrix(m, 13))

if __name__ == "__main__":
    unittest.main()
