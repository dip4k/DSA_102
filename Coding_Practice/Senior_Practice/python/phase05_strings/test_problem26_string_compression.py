import unittest
from phase05_strings.problem26_string_compression import compress

class TestProblem26(unittest.TestCase):
    def test_cases(self):
        c1 = ["a", "a", "b", "b", "c", "c", "c"]
        l1 = compress(c1)
        self.assertEqual(l1, 6)
        self.assertEqual(c1[:l1], ["a", "2", "b", "2", "c", "3"])

        c2 = ["a"]
        l2 = compress(c2)
        self.assertEqual(l2, 1)
        self.assertEqual(c2[:l2], ["a"])

        c3 = ["a", "b", "b", "b", "b", "b", "b", "b", "b", "b", "b", "b", "b"]
        l3 = compress(c3)
        self.assertEqual(l3, 4)
        self.assertEqual(c3[:l3], ["a", "b", "1", "2"])

if __name__ == "__main__":
    unittest.main()
