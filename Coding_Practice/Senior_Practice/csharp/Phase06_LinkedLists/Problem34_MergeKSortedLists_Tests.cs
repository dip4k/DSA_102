namespace SeniorPractice.Phase06.Tests;

public class Problem34_MergeKSortedLists_Tests
{
    [Fact]
    public void MergeKLists_ShouldMergeMultipleSortedLists()
    {
        var lists = new ListNode?[]
        {
            ListNode.FromArray(new[] { 1, 4, 5 }),
            ListNode.FromArray(new[] { 1, 3, 4 }),
            ListNode.FromArray(new[] { 2, 6 })
        };

        var merged = Problem34_MergeKSortedLists.MergeKLists(lists);
        Assert.Equal(new[] { 1, 1, 2, 3, 4, 4, 5, 6 }, ListNode.ToArray(merged));

        Assert.Null(Problem34_MergeKSortedLists.MergeKLists(Array.Empty<ListNode?>()));
    }
}
