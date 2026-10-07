namespace SeniorPractice.Phase06;

/// <summary>
/// Problem #33: Reorder List (LeetCode #143)
/// Invariant: 3-step pipeline: (1) Find mid, (2) reverse second half, (3) interleave
/// </summary>
public static class Problem33_ReorderList
{
    public static void ReorderList(ListNode? head)
    {
        if (head?.next == null) return;

        // 1. Find midpoint
        ListNode? slow = head;
        ListNode? fast = head;
        while (fast != null && fast.next != null && fast.next.next != null)
        {
            slow = slow!.next;
            fast = fast.next.next;
        }

        // 2. Reverse second half
        ListNode? second = slow!.next;
        slow.next = null; // Split
        ListNode? prev = null;
        while (second != null)
        {
            ListNode? nxt = second.next;
            second.next = prev;
            prev = second;
            second = nxt;
        }

        // 3. Interleave first and reversed second
        ListNode? first = head;
        second = prev;
        while (second != null)
        {
            ListNode? tmp1 = first!.next;
            ListNode? tmp2 = second.next;

            first.next = second;
            second.next = tmp1;

            first = tmp1;
            second = tmp2;
        }
    }
}
