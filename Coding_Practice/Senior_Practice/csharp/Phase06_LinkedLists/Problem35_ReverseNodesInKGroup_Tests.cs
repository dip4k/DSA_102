namespace SeniorPractice.Phase06.Tests;

public class Problem35_ReverseNodesInKGroup_Tests
{
    [Fact]
    public void ReverseKGroup_ShouldReverseInGroups()
    {
        var h1 = ListNode.FromArray(new[] { 1, 2, 3, 4, 5 });
        var res1 = Problem35_ReverseNodesInKGroup.ReverseKGroup(h1, 2);
        Assert.Equal(new[] { 2, 1, 4, 3, 5 }, ListNode.ToArray(res1));

        var h2 = ListNode.FromArray(new[] { 1, 2, 3, 4, 5 });
        var res2 = Problem35_ReverseNodesInKGroup.ReverseKGroup(h2, 3);
        Assert.Equal(new[] { 3, 2, 1, 4, 5 }, ListNode.ToArray(res2));
    }
}
