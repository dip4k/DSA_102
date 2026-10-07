import unittest
from phase06_linked_lists.list_node import ListNode
from phase06_linked_lists.problem33_reorder_list import reorder_list

class TestProblem33(unittest.TestCase):
    def test_cases(self):
        h1 = ListNode.from_list([1, 2, 3, 4])
        reorder_list(h1)
        self.assertEqual(ListNode.to_list(h1), [1, 4, 2, 3])

        h2 = ListNode.from_list([1, 2, 3, 4, 5])
        reorder_list(h2)
        self.assertEqual(ListNode.to_list(h2), [1, 5, 2, 4, 3])

if __name__ == "__main__":
    unittest.main()
