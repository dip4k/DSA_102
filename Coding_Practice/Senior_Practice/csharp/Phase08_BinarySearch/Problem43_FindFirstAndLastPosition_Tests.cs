namespace SeniorPractice.Phase08.Tests;

public class Problem43_FindFirstAndLastPosition_Tests
{
    [Theory]
    [InlineData(new[] { 5, 7, 7, 8, 8, 10 }, 8, new[] { 3, 4 })]
    [InlineData(new[] { 5, 7, 7, 8, 8, 10 }, 6, new[] { -1, -1 })]
    [InlineData(new int[0], 0, new[] { -1, -1 })]
    public void SearchRange_ShouldReturnCorrectBounds(int[] nums, int target, int[] expected)
    {
        Assert.Equal(expected, Problem43_FindFirstAndLastPosition.SearchRange(nums, target));
    }
}
