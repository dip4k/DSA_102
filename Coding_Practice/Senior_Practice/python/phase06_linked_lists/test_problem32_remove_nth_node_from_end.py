import unittest
from phase06_linked_lists.list_node import ListNode
from phase06_linked_lists.problem32_remove_nth_node_from_end import remove_nth_from_end

class TestProblem32(unittest.TestCase):
    def test_cases(self):
        head = ListNode.from_list([1, 2, 3, 4, 5])
        res = remove_nth_from_end(head, 2)
        self.assertEqual(ListNode.to_list(res), [1, 2, 3, 5])

        single = ListNode.from_list([1])
        res_single = remove_nth_from_end(single, 1)
        self.assertEqual(ListNode.to_list(res_single), [])

if __name__ == "__main__":
    unittest.main()
