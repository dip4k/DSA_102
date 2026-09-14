import unittest
from phase01_arrays_hashing.problem05_longest_consecutive_sequence import longest_consecutive

class TestProblem05LongestConsecutiveSequence(unittest.TestCase):
    def test_cases(self):
        self.assertEqual(longest_consecutive([100, 4, 200, 1, 3, 2]), 4)
        self.assertEqual(longest_consecutive([0, 3, 7, 2, 5, 8, 4, 6, 0, 1]), 9)
        self.assertEqual(longest_consecutive([]), 0)
        self.assertEqual(longest_consecutive([42]), 1)
        self.assertEqual(longest_consecutive([1, 2, 0, 1]), 3)
        self.assertEqual(longest_consecutive([-2, -1, 0, 1, 2]), 5)

if __name__ == "__main__":
    unittest.main()

