import unittest
from phase07_stack_monotonic_stack.problem36_valid_parentheses import is_valid

class TestProblem36(unittest.TestCase):
    def test_cases(self):
        self.assertTrue(is_valid("()"))
        self.assertTrue(is_valid("()[]{}"))
        self.assertFalse(is_valid("(]"))
        self.assertFalse(is_valid("([)]"))
        self.assertTrue(is_valid("{[]}"))
        self.assertFalse(is_valid("]"))

if __name__ == "__main__":
    unittest.main()
