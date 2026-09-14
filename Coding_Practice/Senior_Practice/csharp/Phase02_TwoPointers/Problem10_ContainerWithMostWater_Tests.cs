namespace SeniorPractice.Phase02.Tests;

public class Problem10_ContainerWithMostWater_Tests
{
    [Theory]
    [InlineData(new[] { 1, 8, 6, 2, 5, 4, 8, 3, 7 }, 49)]
    [InlineData(new[] { 1, 1 }, 1)]
    [InlineData(new[] { 4, 3, 2, 1, 4 }, 16)]
    [InlineData(new[] { 1, 2, 1 }, 2)]
    public void MaxArea_ShouldCalculateMaximumWater(int[] height, int expected)
    {
        int result = Problem10_ContainerWithMostWater.MaxArea(height);
        Assert.Equal(expected, result);
    }
}

