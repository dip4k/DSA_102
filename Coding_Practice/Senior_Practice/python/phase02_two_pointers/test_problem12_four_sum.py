import unittest
from phase02_two_pointers.problem12_four_sum import four_sum

class TestProblem12FourSum(unittest.TestCase):
    def test_cases(self):
        input_nums = [1, 0, -1, 0, -2, 2]
        target = 0
        result = four_sum(input_nums, target)
        sorted_result = sorted([sorted(q) for q in result])
        expected = sorted([[-2, -1, 1, 2], [-2, 0, 0, 2], [-1, 0, 0, 1]])
        self.assertEqual(sorted_result, expected)

        self.assertEqual(four_sum([2, 2, 2, 2, 2], 8), [[2, 2, 2, 2]])
        self.assertEqual(four_sum([1, 2, 3], 6), [])

if __name__ == "__main__":
    unittest.main()

