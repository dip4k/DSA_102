import unittest
from phase06_linked_lists.list_node import ListNode
from phase06_linked_lists.problem34_merge_k_sorted_lists import merge_k_lists

class TestProblem34(unittest.TestCase):
    def test_cases(self):
        lists = [
            ListNode.from_list([1, 4, 5]),
            ListNode.from_list([1, 3, 4]),
            ListNode.from_list([2, 6]),
        ]
        merged = merge_k_lists(lists)
        self.assertEqual(ListNode.to_list(merged), [1, 1, 2, 3, 4, 4, 5, 6])
        self.assertIsNone(merge_k_lists([]))

if __name__ == "__main__":
    unittest.main()
