namespace SeniorPractice.Phase08.Tests;

public class Problem41_BinarySearch_Tests
{
    [Theory]
    [InlineData(new[] { -1, 0, 3, 5, 9, 12 }, 9, 4)]
    [InlineData(new[] { -1, 0, 3, 5, 9, 12 }, 2, -1)]
    [InlineData(new[] { 5 }, 5, 0)]
    public void Search_ShouldReturnIndexOrMinusOne(int[] nums, int target, int expected)
    {
        Assert.Equal(expected, Problem41_BinarySearch.Search(nums, target));
    }
}
