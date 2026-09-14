import unittest
from phase02_two_pointers.problem10_container_with_most_water import max_area

class TestProblem10ContainerWithMostWater(unittest.TestCase):
    def test_standard_case(self):
        self.assertEqual(max_area([1, 8, 6, 2, 5, 4, 8, 3, 7]), 49)

    def test_two_elements(self):
        self.assertEqual(max_area([1, 1]), 1)

    def test_plateau(self):
        self.assertEqual(max_area([4, 3, 2, 1, 4]), 16)

    def test_triangle(self):
        self.assertEqual(max_area([1, 2, 1]), 2)

if __name__ == '__main__':
    unittest.main()

