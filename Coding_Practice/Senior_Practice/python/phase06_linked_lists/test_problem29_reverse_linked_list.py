import unittest
from phase06_linked_lists.list_node import ListNode
from phase06_linked_lists.problem29_reverse_linked_list import reverse_list

class TestProblem29(unittest.TestCase):
    def test_cases(self):
        head = ListNode.from_list([1, 2, 3, 4, 5])
        rev = reverse_list(head)
        self.assertEqual(ListNode.to_list(rev), [5, 4, 3, 2, 1])
        self.assertIsNone(reverse_list(None))

if __name__ == "__main__":
    unittest.main()
