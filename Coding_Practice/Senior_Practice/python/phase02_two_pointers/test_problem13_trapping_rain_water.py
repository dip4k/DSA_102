import unittest
from phase02_two_pointers.problem13_trapping_rain_water import trap

class TestProblem13TrappingRainWater(unittest.TestCase):
    def test_standard_case(self):
        self.assertEqual(trap([0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1]), 6)

    def test_valley_case(self):
        self.assertEqual(trap([4, 2, 0, 3, 2, 5]), 9)

    def test_ascending(self):
        self.assertEqual(trap([1, 2, 3, 4, 5]), 0)

    def test_descending(self):
        self.assertEqual(trap([5, 4, 3, 2, 1]), 0)

    def test_deep_potholes(self):
        self.assertEqual(trap([3, 0, 2, 0, 4]), 7)

if __name__ == '__main__':
    unittest.main()

