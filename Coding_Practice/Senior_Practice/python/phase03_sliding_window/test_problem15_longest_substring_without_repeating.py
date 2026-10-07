import unittest
from phase03_sliding_window.problem15_longest_substring_without_repeating import length_of_longest_substring

class TestProblem15(unittest.TestCase):
    def test_cases(self):
        self.assertEqual(length_of_longest_substring("abcabcbb"), 3)
        self.assertEqual(length_of_longest_substring("bbbbb"), 1)
        self.assertEqual(length_of_longest_substring("pwwkew"), 3)
        self.assertEqual(length_of_longest_substring(""), 0)
        self.assertEqual(length_of_longest_substring(" "), 1)

if __name__ == "__main__":
    unittest.main()
