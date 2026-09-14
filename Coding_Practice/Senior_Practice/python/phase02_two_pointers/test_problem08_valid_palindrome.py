import unittest
from phase02_two_pointers.problem08_valid_palindrome import is_palindrome

class TestProblem08ValidPalindrome(unittest.TestCase):
    def test_cases(self):
        self.assertTrue(is_palindrome("A man, a plan, a canal: Panama"))
        self.assertFalse(is_palindrome("race a car"))
        self.assertTrue(is_palindrome(" "))
        self.assertTrue(is_palindrome("., "))
        self.assertFalse(is_palindrome("0P"))
        self.assertTrue(is_palindrome("a."))

if __name__ == "__main__":
    unittest.main()

