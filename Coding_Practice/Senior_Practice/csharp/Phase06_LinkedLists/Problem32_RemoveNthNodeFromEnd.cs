namespace SeniorPractice.Phase06;

/// <summary>
/// Problem #32: Remove Nth Node From End of List (LeetCode #19)
/// Invariant: Gap of N + 1 between fast and slow pointers with sentinel dummy head
/// </summary>
public static class Problem32_RemoveNthNodeFromEnd
{
    public static ListNode? RemoveNthFromEnd(ListNode? head, int n)
    {
        var dummy = new ListNode(0, head);
        ListNode? fast = dummy;
        ListNode? slow = dummy;

        for (int i = 0; i <= n; i++)
        {
            fast = fast?.next;
        }

        while (fast != null)
        {
            fast = fast.next;
            slow = slow?.next;
        }

        if (slow?.next != null)
        {
            slow.next = slow.next.next;
        }

        return dummy.next;
    }
}
