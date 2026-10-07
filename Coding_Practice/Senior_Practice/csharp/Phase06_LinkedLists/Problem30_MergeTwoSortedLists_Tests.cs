namespace SeniorPractice.Phase06.Tests;

public class Problem30_MergeTwoSortedLists_Tests
{
    [Fact]
    public void MergeTwoLists_ShouldMergeInSortedOrder()
    {
        var l1 = ListNode.FromArray(new[] { 1, 2, 4 });
        var l2 = ListNode.FromArray(new[] { 1, 3, 4 });
        var merged = Problem30_MergeTwoSortedLists.MergeTwoLists(l1, l2);
        Assert.Equal(new[] { 1, 1, 2, 3, 4, 4 }, ListNode.ToArray(merged));

        Assert.Null(Problem30_MergeTwoSortedLists.MergeTwoLists(null, null));
    }
}
