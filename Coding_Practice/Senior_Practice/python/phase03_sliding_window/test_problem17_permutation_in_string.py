import unittest
from phase03_sliding_window.problem17_permutation_in_string import check_inclusion

class TestProblem17(unittest.TestCase):
    def test_cases(self):
        self.assertTrue(check_inclusion("ab", "eidbaooo"))
        self.assertFalse(check_inclusion("ab", "eidboaoo"))
        self.assertTrue(check_inclusion("adc", "dcda"))
        self.assertFalse(check_inclusion("hello", "ooolleoooleh"))

if __name__ == "__main__":
    unittest.main()
