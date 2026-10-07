import unittest
from phase06_linked_lists.list_node import ListNode
from phase06_linked_lists.problem31_linked_list_cycle import has_cycle

class TestProblem31(unittest.TestCase):
    def test_cases(self):
        n1 = ListNode(3)
        n2 = ListNode(2)
        n3 = ListNode(0)
        n4 = ListNode(-4)
        n1.next = n2
        n2.next = n3
        n3.next = n4
        n4.next = n2
        self.assertTrue(has_cycle(n1))

        linear = ListNode.from_list([1, 2, 3])
        self.assertFalse(has_cycle(linear))

if __name__ == "__main__":
    unittest.main()
