import unittest
from phase01_arrays_hashing.problem03_valid_anagram import is_anagram

class TestProblem03ValidAnagram(unittest.TestCase):
    def test_cases(self):
        self.assertTrue(is_anagram("anagram", "nagaram"))
        self.assertFalse(is_anagram("rat", "car"))
        self.assertTrue(is_anagram("a", "a"))
        self.assertFalse(is_anagram("ab", "a"))
        self.assertTrue(is_anagram("listen", "silent"))

if __name__ == "__main__":
    unittest.main()

