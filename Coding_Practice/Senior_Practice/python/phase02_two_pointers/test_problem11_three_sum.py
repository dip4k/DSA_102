import unittest
from phase02_two_pointers.problem11_three_sum import three_sum

class TestProblem11ThreeSum(unittest.TestCase):
    def test_cases(self):
        input_nums = [-1, 0, 1, 2, -1, -4]
        result = three_sum(input_nums)
        sorted_result = sorted([sorted(t) for t in result])
        expected = sorted([[-1, -1, 2], [-1, 0, 1]])
        self.assertEqual(sorted_result, expected)

        self.assertEqual(three_sum([0, 0, 0, 0]), [[0, 0, 0]])
        self.assertEqual(three_sum([1, 2, 3]), [])

if __name__ == "__main__":
    unittest.main()

