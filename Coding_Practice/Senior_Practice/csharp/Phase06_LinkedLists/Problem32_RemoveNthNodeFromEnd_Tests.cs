namespace SeniorPractice.Phase06.Tests;

public class Problem32_RemoveNthNodeFromEnd_Tests
{
    [Fact]
    public void RemoveNthFromEnd_ShouldRemoveCorrectNode()
    {
        var head = ListNode.FromArray(new[] { 1, 2, 3, 4, 5 });
        var res = Problem32_RemoveNthNodeFromEnd.RemoveNthFromEnd(head, 2);
        Assert.Equal(new[] { 1, 2, 3, 5 }, ListNode.ToArray(res));

        var single = ListNode.FromArray(new[] { 1 });
        var resSingle = Problem32_RemoveNthNodeFromEnd.RemoveNthFromEnd(single, 1);
        Assert.Equal(Array.Empty<int>(), ListNode.ToArray(resSingle));
    }
}
