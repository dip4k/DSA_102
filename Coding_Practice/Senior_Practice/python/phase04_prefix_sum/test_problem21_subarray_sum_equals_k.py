import unittest
from phase04_prefix_sum.problem21_subarray_sum_equals_k import subarray_sum

class TestProblem21(unittest.TestCase):
    def test_cases(self):
        self.assertEqual(subarray_sum([1, 1, 1], 2), 2)
        self.assertEqual(subarray_sum([1, 2, 3], 3), 2)
        self.assertEqual(subarray_sum([1, -1, 0], 0), 3)

if __name__ == "__main__":
    unittest.main()
