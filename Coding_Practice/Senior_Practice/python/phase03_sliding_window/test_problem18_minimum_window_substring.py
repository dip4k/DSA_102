import unittest
from phase03_sliding_window.problem18_minimum_window_substring import min_window

class TestProblem18(unittest.TestCase):
    def test_cases(self):
        self.assertEqual(min_window("ADOBECODEBANC", "ABC"), "BANC")
        self.assertEqual(min_window("a", "a"), "a")
        self.assertEqual(min_window("a", "aa"), "")

if __name__ == "__main__":
    unittest.main()
