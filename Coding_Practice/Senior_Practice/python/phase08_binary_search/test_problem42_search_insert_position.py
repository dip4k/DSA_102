import unittest
from phase08_binary_search.problem42_search_insert_position import search_insert

class TestProblem42(unittest.TestCase):
    def test_cases(self):
        self.assertEqual(search_insert([1, 3, 5, 6], 5), 2)
        self.assertEqual(search_insert([1, 3, 5, 6], 2), 1)
        self.assertEqual(search_insert([1, 3, 5, 6], 7), 4)
        self.assertEqual(search_insert([1, 3, 5, 6], 0), 0)

if __name__ == "__main__":
    unittest.main()
