namespace SeniorPractice.Phase04.Tests;

public class Problem22_MaximumSubarray_Tests
{
    [Theory]
    [InlineData(new[] { -2, 1, -3, 4, -1, 2, 1, -5, 4 }, 6)]
    [InlineData(new[] { 1 }, 1)]
    [InlineData(new[] { 5, 4, -1, 7, 8 }, 23)]
    [InlineData(new[] { -5, -2, -10 }, -2)]
    public void MaxSubArray_ShouldReturnMaxSum(int[] nums, int expected)
    {
        Assert.Equal(expected, Problem22_MaximumSubarray.MaxSubArray(nums));
    }
}
