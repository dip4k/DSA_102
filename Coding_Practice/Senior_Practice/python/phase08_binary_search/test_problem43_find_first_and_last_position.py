import unittest
from phase08_binary_search.problem43_find_first_and_last_position import search_range

class TestProblem43(unittest.TestCase):
    def test_cases(self):
        self.assertEqual(search_range([5, 7, 7, 8, 8, 10], 8), [3, 4])
        self.assertEqual(search_range([5, 7, 7, 8, 8, 10], 6), [-1, -1])
        self.assertEqual(search_range([], 0), [-1, -1])

if __name__ == "__main__":
    unittest.main()
