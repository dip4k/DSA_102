import unittest
from phase08_binary_search.problem46_koko_eating_bananas import min_eating_speed

class TestProblem46(unittest.TestCase):
    def test_cases(self):
        self.assertEqual(min_eating_speed([3, 6, 7, 11], 8), 4)
        self.assertEqual(min_eating_speed([30, 11, 23, 4, 20], 5), 30)
        self.assertEqual(min_eating_speed([30, 11, 23, 4, 20], 6), 23)

if __name__ == "__main__":
    unittest.main()
