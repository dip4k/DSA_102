namespace SeniorPractice.Phase06;

/// <summary>
/// Problem #31: Linked List Cycle (LeetCode #141)
/// Invariant: Floyd's Tortoise and Hare — fast and slow pointer convergence
/// </summary>
public static class Problem31_LinkedListCycle
{
    public static bool HasCycle(ListNode? head)
    {
        if (head == null) return false;

        ListNode? slow = head;
        ListNode? fast = head;

        while (fast != null && fast.next != null)
        {
            slow = slow!.next;
            fast = fast.next.next;

            if (slow == fast)
            {
                return true;
            }
        }

        return false;
    }
}
