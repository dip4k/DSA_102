import unittest
from phase06_linked_lists.list_node import ListNode
from phase06_linked_lists.problem35_reverse_nodes_in_k_group import reverse_k_group

class TestProblem35(unittest.TestCase):
    def test_cases(self):
        h1 = ListNode.from_list([1, 2, 3, 4, 5])
        res1 = reverse_k_group(h1, 2)
        self.assertEqual(ListNode.to_list(res1), [2, 1, 4, 3, 5])

        h2 = ListNode.from_list([1, 2, 3, 4, 5])
        res2 = reverse_k_group(h2, 3)
        self.assertEqual(ListNode.to_list(res2), [3, 2, 1, 4, 5])

if __name__ == "__main__":
    unittest.main()
