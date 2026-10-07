namespace SeniorPractice.Phase06.Tests;

public class Problem29_ReverseLinkedList_Tests
{
    [Fact]
    public void ReverseList_ShouldReverseNodes()
    {
        var head = ListNode.FromArray(new[] { 1, 2, 3, 4, 5 });
        var reversed = Problem29_ReverseLinkedList.ReverseList(head);
        Assert.Equal(new[] { 5, 4, 3, 2, 1 }, ListNode.ToArray(reversed));

        Assert.Null(Problem29_ReverseLinkedList.ReverseList(null));
    }
}
