import unittest
from phase01_arrays_hashing.problem06_majority_element import majority_element

class TestProblem06MajorityElement(unittest.TestCase):
    def test_cases(self):
        self.assertEqual(majority_element([3, 2, 3]), 3)
        self.assertEqual(majority_element([2, 2, 1, 1, 1, 2, 2]), 2)
        self.assertEqual(majority_element([42]), 42)
        self.assertEqual(majority_element([6, 5, 5]), 5)
        self.assertEqual(majority_element([1, 1, 1, 2, 3, 1, 4, 1]), 1)

if __name__ == "__main__":
    unittest.main()

