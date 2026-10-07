namespace SeniorPractice.Phase08.Tests;

public class Problem42_SearchInsertPosition_Tests
{
    [Theory]
    [InlineData(new[] { 1, 3, 5, 6 }, 5, 2)]
    [InlineData(new[] { 1, 3, 5, 6 }, 2, 1)]
    [InlineData(new[] { 1, 3, 5, 6 }, 7, 4)]
    [InlineData(new[] { 1, 3, 5, 6 }, 0, 0)]
    public void SearchInsert_ShouldReturnInsertionIndex(int[] nums, int target, int expected)
    {
        Assert.Equal(expected, Problem42_SearchInsertPosition.SearchInsert(nums, target));
    }
}
