import unittest
from phase04_prefix_sum.problem23_range_sum_query_immutable import NumArray

class TestProblem23(unittest.TestCase):
    def test_cases(self):
        arr = NumArray([-2, 0, 3, -5, 2, -1])
        self.assertEqual(arr.sum_range(0, 2), 1)
        self.assertEqual(arr.sum_range(2, 5), -1)
        self.assertEqual(arr.sum_range(0, 5), -3)

if __name__ == "__main__":
    unittest.main()
