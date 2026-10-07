namespace SeniorPractice.Phase06;

/// <summary>
/// Problem #35: Reverse Nodes in K-Group (LeetCode #25)
/// Invariant: Verify k nodes exist, reverse segment in-place, recurse/reconnect
/// </summary>
public static class Problem35_ReverseNodesInKGroup
{
    public static ListNode? ReverseKGroup(ListNode? head, int k)
    {
        if (head == null || k <= 1) return head;

        // Check if there are at least k nodes remaining
        ListNode? check = head;
        for (int i = 0; i < k; i++)
        {
            if (check == null) return head; // less than k, leave unchanged
            check = check.next;
        }

        // Reverse k nodes
        ListNode? prev = null;
        ListNode? curr = head;
        for (int i = 0; i < k; i++)
        {
            ListNode? nxt = curr!.next;
            curr.next = prev;
            prev = curr;
            curr = nxt;
        }

        // Reconnect tail of reversed group to result of next segment
        head.next = ReverseKGroup(curr, k);
        return prev;
    }
}
