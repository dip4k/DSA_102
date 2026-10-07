from .list_node import ListNode

def reverse_k_group(head: ListNode | None, k: int) -> ListNode | None:
    if not head or k <= 1:
        return head

    # Check if k nodes exist
    check = head
    for _ in range(k):
        if not check:
            return head
        check = check.next

    # Reverse k nodes
    prev = None
    curr = head
    for _ in range(k):
        nxt = curr.next
        curr.next = prev
        prev = curr
        curr = nxt

    # Reconnect
    head.next = reverse_k_group(curr, k)
    return prev
