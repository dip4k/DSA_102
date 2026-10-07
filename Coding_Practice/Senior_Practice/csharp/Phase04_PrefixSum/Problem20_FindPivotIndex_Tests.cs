namespace SeniorPractice.Phase04.Tests;

public class Problem20_FindPivotIndex_Tests
{
    [Theory]
    [InlineData(new[] { 1, 7, 3, 6, 5, 6 }, 3)]
    [InlineData(new[] { 1, 2, 3 }, -1)]
    [InlineData(new[] { 2, 1, -1 }, 0)]
    public void PivotIndex_ShouldReturnCorrectIndex(int[] nums, int expected)
    {
        Assert.Equal(expected, Problem20_FindPivotIndex.PivotIndex(nums));
    }
}
