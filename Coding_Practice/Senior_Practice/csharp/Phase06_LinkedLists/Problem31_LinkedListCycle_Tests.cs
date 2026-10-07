namespace SeniorPractice.Phase06.Tests;

public class Problem31_LinkedListCycle_Tests
{
    [Fact]
    public void HasCycle_ShouldDetectCycle()
    {
        var n1 = new ListNode(3);
        var n2 = new ListNode(2);
        var n3 = new ListNode(0);
        var n4 = new ListNode(-4);
        n1.next = n2;
        n2.next = n3;
        n3.next = n4;
        n4.next = n2; // cycle

        Assert.True(Problem31_LinkedListCycle.HasCycle(n1));

        var linear = ListNode.FromArray(new[] { 1, 2, 3 });
        Assert.False(Problem31_LinkedListCycle.HasCycle(linear));
    }
}
