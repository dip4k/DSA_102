import unittest
from phase10_heap_priority_queue.problem59_median_finder import MedianFinder

class TestProblem59(unittest.TestCase):
    def test_cases(self):
        mf = MedianFinder()
        mf.add_num(1)
        mf.add_num(2)
        self.assertEqual(mf.find_median(), 1.5)
        mf.add_num(3)
        self.assertEqual(mf.find_median(), 2.0)

if __name__ == "__main__":
    unittest.main()
