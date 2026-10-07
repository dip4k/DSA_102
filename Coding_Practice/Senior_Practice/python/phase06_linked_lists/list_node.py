class ListNode:
    def __init__(self, val: int = 0, next: 'ListNode | None' = None):
        self.val = val
        self.next = next

    @staticmethod
    def from_list(values: list[int]) -> 'ListNode | None':
        if not values:
            return None
        dummy = ListNode()
        curr = dummy
        for v in values:
            curr.next = ListNode(v)
            curr = curr.next
        return dummy.next

    @staticmethod
    def to_list(head: 'ListNode | None') -> list[int]:
        result = []
        curr = head
        while curr:
            result.append(curr.val)
            curr = curr.next
        return result
