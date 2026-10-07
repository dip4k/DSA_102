from .list_node import ListNode

def has_cycle(head: ListNode | None) -> bool:
    if not head:
        return False
    slow = head
    fast = head
    while fast and fast.next:
        slow = slow.next
        fast = fast.next.next
        if slow == fast:
            return True
    return False
