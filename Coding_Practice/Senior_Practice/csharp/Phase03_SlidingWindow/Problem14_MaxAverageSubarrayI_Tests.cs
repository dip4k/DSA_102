namespace SeniorPractice.Phase03.Tests;

public class Problem14_MaxAverageSubarrayI_Tests
{
    [Theory]
    [InlineData(new[] { 1, 12, -5, -6, 50, 3 }, 4, 12.75)]
    [InlineData(new[] { 5 }, 1, 5.0)]
    [InlineData(new[] { -1 }, 1, -1.0)]
    public void FindMaxAverage_ShouldReturnMaxAverage(int[] nums, int k, double expected)
    {
        double result = Problem14_MaxAverageSubarrayI.FindMaxAverage(nums, k);
        Assert.Equal(expected, result, precision: 5);
    }
}
