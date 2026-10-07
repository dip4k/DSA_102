import unittest
from phase07_stack_monotonic_stack.problem38_evaluate_reverse_polish_notation import eval_rpn

class TestProblem38(unittest.TestCase):
    def test_cases(self):
        self.assertEqual(eval_rpn(["2", "1", "+", "3", "*"]), 9)
        self.assertEqual(eval_rpn(["4", "13", "5", "/", "+"]), 6)
        self.assertEqual(eval_rpn(["10", "6", "9", "3", "+", "-11", "*", "/", "*", "17", "+", "5", "+"]), 22)

if __name__ == "__main__":
    unittest.main()
