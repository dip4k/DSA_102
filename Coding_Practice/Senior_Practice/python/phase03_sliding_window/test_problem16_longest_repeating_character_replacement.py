import unittest
from phase03_sliding_window.problem16_longest_repeating_character_replacement import character_replacement

class TestProblem16(unittest.TestCase):
    def test_cases(self):
        self.assertEqual(character_replacement("ABAB", 2), 4)
        self.assertEqual(character_replacement("AABABBA", 1), 4)
        self.assertEqual(character_replacement("AAAA", 2), 4)
        self.assertEqual(character_replacement("ABCDE", 1), 2)

if __name__ == "__main__":
    unittest.main()
