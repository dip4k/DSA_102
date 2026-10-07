from .list_node import ListNode

def remove_nth_from_end(head: ListNode | None, n: int) -> ListNode | None:
    dummy = ListNode(0, head)
    fast = dummy
    slow = dummy

    for _ in range(n + 1):
        if fast:
            fast = fast.next

    while fast:
        fast = fast.next
        slow = slow.next

    if slow and slow.next:
        slow.next = slow.next.next

    return dummy.next
