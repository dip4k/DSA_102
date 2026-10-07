import unittest
from phase04_prefix_sum.problem22_maximum_subarray import max_sub_array

class TestProblem22(unittest.TestCase):
    def test_cases(self):
        self.assertEqual(max_sub_array([-2, 1, -3, 4, -1, 2, 1, -5, 4]), 6)
        self.assertEqual(max_sub_array([1]), 1)
        self.assertEqual(max_sub_array([5, 4, -1, 7, 8]), 23)
        self.assertEqual(max_sub_array([-5, -2, -10]), -2)

if __name__ == "__main__":
    unittest.main()
