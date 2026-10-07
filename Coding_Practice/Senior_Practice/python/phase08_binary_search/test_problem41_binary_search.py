import unittest
from phase08_binary_search.problem41_binary_search import search

class TestProblem41(unittest.TestCase):
    def test_cases(self):
        self.assertEqual(search([-1, 0, 3, 5, 9, 12], 9), 4)
        self.assertEqual(search([-1, 0, 3, 5, 9, 12], 2), -1)
        self.assertEqual(search([5], 5), 0)

if __name__ == "__main__":
    unittest.main()
