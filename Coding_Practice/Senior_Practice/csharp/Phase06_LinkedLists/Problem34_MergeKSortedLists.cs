namespace SeniorPractice.Phase06;

/// <summary>
/// Problem #34: Merge K Sorted Lists (LeetCode #23)
/// Invariant: Min-Heap / PriorityQueue maintaining k heads simultaneously
/// </summary>
public static class Problem34_MergeKSortedLists
{
    public static ListNode? MergeKLists(ListNode?[] lists)
    {
        if (lists == null || lists.Length == 0) return null;

        var pq = new PriorityQueue<ListNode, int>();
        foreach (var node in lists)
        {
            if (node != null)
            {
                pq.Enqueue(node, node.val);
            }
        }

        var dummy = new ListNode();
        var tail = dummy;

        while (pq.Count > 0)
        {
            var minNode = pq.Dequeue();
            tail.next = minNode;
            tail = tail.next;

            if (minNode.next != null)
            {
                pq.Enqueue(minNode.next, minNode.next.val);
            }
        }

        return dummy.next;
    }
}
