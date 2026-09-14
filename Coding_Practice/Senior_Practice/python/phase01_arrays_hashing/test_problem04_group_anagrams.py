import unittest
from phase01_arrays_hashing.problem04_group_anagrams import group_anagrams

class TestProblem04GroupAnagrams(unittest.TestCase):
    def test_cases(self):
        input_strs = ["eat", "tea", "tan", "ate", "nat", "bat"]
        result = group_anagrams(input_strs)
        sorted_result = sorted([sorted(group) for group in result])
        expected = sorted([["bat"], ["nat", "tan"], ["ate", "eat", "tea"]])
        self.assertEqual(sorted_result, expected)

        self.assertEqual(group_anagrams([""]), [[""]])
        self.assertEqual(group_anagrams(["a"]), [["a"]])

if __name__ == "__main__":
    unittest.main()

