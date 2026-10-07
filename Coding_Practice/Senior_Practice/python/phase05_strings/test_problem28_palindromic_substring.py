import unittest
from phase05_strings.problem28_palindromic_substring import count_substrings

class TestProblem28(unittest.TestCase):
    def test_cases(self):
        self.assertEqual(count_substrings("abc"), 3)
        self.assertEqual(count_substrings("aaa"), 6)
        self.assertEqual(count_substrings("a"), 1)

if __name__ == "__main__":
    unittest.main()
