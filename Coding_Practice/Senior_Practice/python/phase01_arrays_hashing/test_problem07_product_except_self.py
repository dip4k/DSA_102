import unittest
from phase01_arrays_hashing.problem07_product_except_self import product_except_self

class TestProblem07ProductExceptSelf(unittest.TestCase):
    def test_cases(self):
        self.assertEqual(product_except_self([1, 2, 3, 4]), [24, 12, 8, 6])
        self.assertEqual(product_except_self([-1, 1, 0, -3, 3]), [0, 0, 9, 0, 0])
        self.assertEqual(product_except_self([0, 0]), [0, 0])
        self.assertEqual(product_except_self([5, 2]), [2, 5])
        self.assertEqual(product_except_self([2, 3, 4, 5]), [60, 40, 30, 24])

if __name__ == "__main__":
    unittest.main()

