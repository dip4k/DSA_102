import unittest
from phase01_arrays_hashing.problem02_contains_duplicate import contains_duplicate

class TestProblem02ContainsDuplicate(unittest.TestCase):
    def test_cases(self):
        self.assertTrue(contains_duplicate([1, 2, 3, 1]))
        self.assertFalse(contains_duplicate([1, 2, 3, 4]))
        self.assertTrue(contains_duplicate([1, 1, 1, 3, 3, 4, 3, 2, 4, 2]))
        self.assertFalse(contains_duplicate([42]))
        self.assertTrue(contains_duplicate([-1, -2, -3, -1]))

if __name__ == "__main__":
    unittest.main()

