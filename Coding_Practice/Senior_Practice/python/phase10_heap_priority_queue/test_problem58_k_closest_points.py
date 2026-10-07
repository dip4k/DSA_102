import unittest
from phase10_heap_priority_queue.problem58_k_closest_points import k_closest

class TestProblem58(unittest.TestCase):
    def test_cases(self):
        res = k_closest([[1, 3], [-2, 2]], 1)
        self.assertEqual(res, [[-2, 2]])

if __name__ == "__main__":
    unittest.main()
