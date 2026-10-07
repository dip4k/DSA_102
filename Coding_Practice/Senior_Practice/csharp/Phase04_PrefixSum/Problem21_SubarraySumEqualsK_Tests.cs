namespace SeniorPractice.Phase04.Tests;

public class Problem21_SubarraySumEqualsK_Tests
{
    [Theory]
    [InlineData(new[] { 1, 1, 1 }, 2, 2)]
    [InlineData(new[] { 1, 2, 3 }, 3, 2)]
    [InlineData(new[] { 1, -1, 0 }, 0, 3)]
    public void SubarraySum_ShouldReturnMatchingCount(int[] nums, int k, int expected)
    {
        Assert.Equal(expected, Problem21_SubarraySumEqualsK.SubarraySum(nums, k));
    }
}
