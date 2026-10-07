import unittest
from phase07_stack_monotonic_stack.problem40_largest_rectangle_in_histogram import largest_rectangle_area

class TestProblem40(unittest.TestCase):
    def test_cases(self):
        self.assertEqual(largest_rectangle_area([2, 1, 5, 6, 2, 3]), 10)
        self.assertEqual(largest_rectangle_area([2, 4]), 4)
        self.assertEqual(largest_rectangle_area([1]), 1)
        self.assertEqual(largest_rectangle_area([2, 1, 2]), 3)

if __name__ == "__main__":
    unittest.main()
