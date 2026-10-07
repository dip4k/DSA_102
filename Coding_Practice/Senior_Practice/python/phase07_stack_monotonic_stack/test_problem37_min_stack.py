import unittest
from phase07_stack_monotonic_stack.problem37_min_stack import MinStack

class TestProblem37(unittest.TestCase):
    def test_cases(self):
        ms = MinStack()
        ms.push(-2)
        ms.push(0)
        ms.push(-3)
        self.assertEqual(ms.get_min(), -3)
        ms.pop()
        self.assertEqual(ms.top(), 0)
        self.assertEqual(ms.get_min(), -2)

if __name__ == "__main__":
    unittest.main()
