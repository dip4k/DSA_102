import unittest
from phase05_strings.problem27_longest_palindromic_substring import longest_palindrome

class TestProblem27(unittest.TestCase):
    def test_cases(self):
        self.assertIn(longest_palindrome("babad"), ["bab", "aba"])
        self.assertEqual(longest_palindrome("cbbd"), "bb")
        self.assertEqual(longest_palindrome("a"), "a")

if __name__ == "__main__":
    unittest.main()
