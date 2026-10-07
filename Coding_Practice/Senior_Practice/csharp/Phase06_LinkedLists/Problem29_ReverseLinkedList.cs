namespace SeniorPractice.Phase06;

/// <summary>
/// Problem #29: Reverse Linked List (LeetCode #206)
/// Invariant: Three-pointer iterative reversal (prev, curr, next)
/// </summary>
public static class Problem29_ReverseLinkedList
{
    public static ListNode? ReverseList(ListNode? head)
    {
        ListNode? prev = null;
        ListNode? curr = head;

        while (curr != null)
        {
            ListNode? next = curr.next;
            curr.next = prev;
            prev = curr;
            curr = next;
        }

        return prev;
    }
}
