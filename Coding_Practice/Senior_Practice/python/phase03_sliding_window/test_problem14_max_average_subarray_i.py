import unittest
from phase03_sliding_window.problem14_max_average_subarray_i import find_max_average

class TestProblem14(unittest.TestCase):
    def test_examples(self):
        self.assertAlmostEqual(find_max_average([1, 12, -5, -6, 50, 3], 4), 12.75)
        self.assertAlmostEqual(find_max_average([5], 1), 5.0)
        self.assertAlmostEqual(find_max_average([-1], 1), -1.0)

if __name__ == "__main__":
    unittest.main()
