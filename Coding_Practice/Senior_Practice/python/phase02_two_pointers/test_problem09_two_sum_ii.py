import unittest
from phase02_two_pointers.problem09_two_sum_ii import two_sum_ii

class TestProblem09TwoSumII(unittest.TestCase):
    def test_cases(self):
        self.assertEqual(two_sum_ii([2, 7, 11, 15], 9), [1, 2])
        self.assertEqual(two_sum_ii([2, 3, 4], 6), [1, 3])
        self.assertEqual(two_sum_ii([-1, 0], -1), [1, 2])
        self.assertEqual(two_sum_ii([-1000, -500, 0, 1000], 0), [1, 4])
        self.assertEqual(two_sum_ii([0, 0, 3, 4], 0), [1, 2])

if __name__ == "__main__":
    unittest.main()

