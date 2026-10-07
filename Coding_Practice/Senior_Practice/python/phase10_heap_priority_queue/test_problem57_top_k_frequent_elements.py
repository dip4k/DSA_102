import unittest
from phase10_heap_priority_queue.problem57_top_k_frequent_elements import top_k_frequent

class TestProblem57(unittest.TestCase):
    def test_cases(self):
        res1 = set(top_k_frequent([1, 1, 1, 2, 2, 3], 2))
        self.assertEqual(res1, {1, 2})

        res2 = top_k_frequent([1], 1)
        self.assertEqual(res2, [1])

if __name__ == "__main__":
    unittest.main()
