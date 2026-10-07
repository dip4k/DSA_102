namespace SeniorPractice.Phase08.Tests;

public class Problem45_FindMinimumInRotatedSortedArray_Tests
{
    [Theory]
    [InlineData(new[] { 3, 4, 5, 1, 2 }, 1)]
    [InlineData(new[] { 4, 5, 6, 7, 0, 1, 2 }, 0)]
    [InlineData(new[] { 11, 13, 15, 17 }, 11)]
    [InlineData(new[] { 2, 1 }, 1)]
    public void FindMin_ShouldReturnMinimumElement(int[] nums, int expected)
    {
        Assert.Equal(expected, Problem45_FindMinimumInRotatedSortedArray.FindMin(nums));
    }
}
