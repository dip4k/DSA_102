namespace SeniorPractice.Phase07.Tests;

public class Problem40_LargestRectangleInHistogram_Tests
{
    [Theory]
    [InlineData(new[] { 2, 1, 5, 6, 2, 3 }, 10)]
    [InlineData(new[] { 2, 4 }, 4)]
    [InlineData(new[] { 1 }, 1)]
    [InlineData(new[] { 2, 1, 2 }, 3)]
    public void LargestRectangleArea_ShouldReturnMaxArea(int[] heights, int expected)
    {
        Assert.Equal(expected, Problem40_LargestRectangleInHistogram.LargestRectangleArea(heights));
    }
}
