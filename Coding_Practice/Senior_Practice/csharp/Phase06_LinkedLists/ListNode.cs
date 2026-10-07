namespace SeniorPractice.Phase06;

public class ListNode
{
    public int val;
    public ListNode? next;

    public ListNode(int val = 0, ListNode? next = null)
    {
        this.val = val;
        this.next = next;
    }

    public static ListNode? FromArray(int[] values)
    {
        if (values == null || values.Length == 0) return null;
        var dummy = new ListNode();
        var curr = dummy;
        foreach (int v in values)
        {
            curr.next = new ListNode(v);
            curr = curr.next;
        }
        return dummy.next;
    }

    public static int[] ToArray(ListNode? head)
    {
        var list = new List<int>();
        while (head != null)
        {
            list.Add(head.val);
            head = head.next;
        }
        return list.ToArray();
    }
}
