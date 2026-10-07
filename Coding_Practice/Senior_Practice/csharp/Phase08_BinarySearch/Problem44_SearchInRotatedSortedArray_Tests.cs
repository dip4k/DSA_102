namespace SeniorPractice.Phase08.Tests;

public class Problem44_SearchInRotatedSortedArray_Tests
{
    [Theory]
    [InlineData(new[] { 4, 5, 6, 7, 0, 1, 2 }, 0, 4)]
    [InlineData(new[] { 4, 5, 6, 7, 0, 1, 2 }, 3, -1)]
    [InlineData(new[] { 1 }, 0, -1)]
    [InlineData(new[] { 5, 1, 3 }, 5, 0)]
    public void Search_ShouldFindTargetInRotatedArray(int[] nums, int target, int expected)
    {
        Assert.Equal(expected, Problem44_SearchInRotatedSortedArray.Search(nums, target));
    }
}
