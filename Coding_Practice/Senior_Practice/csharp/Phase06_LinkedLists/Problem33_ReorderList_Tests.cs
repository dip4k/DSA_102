namespace SeniorPractice.Phase06.Tests;

public class Problem33_ReorderList_Tests
{
    [Fact]
    public void ReorderList_ShouldInterleave()
    {
        var head1 = ListNode.FromArray(new[] { 1, 2, 3, 4 });
        Problem33_ReorderList.ReorderList(head1);
        Assert.Equal(new[] { 1, 4, 2, 3 }, ListNode.ToArray(head1));

        var head2 = ListNode.FromArray(new[] { 1, 2, 3, 4, 5 });
        Problem33_ReorderList.ReorderList(head2);
        Assert.Equal(new[] { 1, 5, 2, 4, 3 }, ListNode.ToArray(head2));
    }
}
