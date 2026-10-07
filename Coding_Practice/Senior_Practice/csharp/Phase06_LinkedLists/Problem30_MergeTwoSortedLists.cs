namespace SeniorPractice.Phase06;

/// <summary>
/// Problem #30: Merge Two Sorted Lists (LeetCode #21)
/// Invariant: Sentinel dummy node with two-pointer comparison
/// </summary>
public static class Problem30_MergeTwoSortedLists
{
    public static ListNode? MergeTwoLists(ListNode? list1, ListNode? list2)
    {
        var dummy = new ListNode();
        var tail = dummy;

        while (list1 != null && list2 != null)
        {
            if (list1.val <= list2.val)
            {
                tail.next = list1;
                list1 = list1.next;
            }
            else
            {
                tail.next = list2;
                list2 = list2.next;
            }
            tail = tail.next;
        }

        tail.next = list1 ?? list2;
        return dummy.next;
    }
}
