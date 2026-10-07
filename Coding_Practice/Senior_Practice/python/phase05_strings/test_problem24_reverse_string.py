import unittest
from phase05_strings.problem24_reverse_string import reverse_string

class TestProblem24(unittest.TestCase):
    def test_cases(self):
        s1 = ["h", "e", "l", "l", "o"]
        reverse_string(s1)
        self.assertEqual(s1, ["o", "l", "l", "e", "h"])

        s2 = ["H", "a", "n", "n", "a", "h"]
        reverse_string(s2)
        self.assertEqual(s2, ["h", "a", "n", "n", "a", "H"])

if __name__ == "__main__":
    unittest.main()
