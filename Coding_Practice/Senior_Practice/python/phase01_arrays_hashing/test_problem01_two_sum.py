import unittest
from phase01_arrays_hashing.problem01_two_sum import two_sum

class TestProblem01TwoSum(unittest.TestCase):
    def test_basic_case(self):
        self.assertEqual(two_sum([2, 7, 11, 15], 9), [0, 1])

    def test_middle_pair(self):
        self.assertEqual(two_sum([3, 2, 4], 6), [1, 2])

    def test_duplicates(self):
        self.assertEqual(two_sum([3, 3], 6), [0, 1])

    def test_negatives(self):
        self.assertEqual(two_sum([-1, -2, -3, -4, -5], -8), [2, 4])

    def test_zero_target(self):
        self.assertEqual(two_sum([0, 4, 3, 0], 0), [0, 3])

if __name__ == '__main__':
    unittest.main()
