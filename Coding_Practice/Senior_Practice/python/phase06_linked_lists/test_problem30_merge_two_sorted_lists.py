import unittest
from phase06_linked_lists.list_node import ListNode
from phase06_linked_lists.problem30_merge_two_sorted_lists import merge_two_lists

class TestProblem30(unittest.TestCase):
    def test_cases(self):
        l1 = ListNode.from_list([1, 2, 4])
        l2 = ListNode.from_list([1, 3, 4])
        merged = merge_two_lists(l1, l2)
        self.assertEqual(ListNode.to_list(merged), [1, 1, 2, 3, 4, 4])

if __name__ == "__main__":
    unittest.main()
