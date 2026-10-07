import unittest
from phase05_strings.problem25_longest_common_prefix import longest_common_prefix

class TestProblem25(unittest.TestCase):
    def test_cases(self):
        self.assertEqual(longest_common_prefix(["flower", "flow", "flight"]), "fl")
        self.assertEqual(longest_common_prefix(["dog", "racecar", "car"]), "")
        self.assertEqual(longest_common_prefix(["interspecies", "interstellar", "interstate"]), "inters")
        self.assertEqual(longest_common_prefix(["a"]), "a")

if __name__ == "__main__":
    unittest.main()
