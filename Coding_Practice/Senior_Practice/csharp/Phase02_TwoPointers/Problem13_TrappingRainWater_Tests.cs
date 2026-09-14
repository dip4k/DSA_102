namespace SeniorPractice.Phase02.Tests;

public class Problem13_TrappingRainWater_Tests
{
    [Theory]
    [InlineData(new[] { 0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1 }, 6)]
    [InlineData(new[] { 4, 2, 0, 3, 2, 5 }, 9)]
    [InlineData(new[] { 1, 2, 3, 4, 5 }, 0)]
    [InlineData(new[] { 5, 4, 3, 2, 1 }, 0)]
    [InlineData(new[] { 3, 0, 2, 0, 4 }, 7)]
    public void Trap_ShouldCalculateTotalTrappedWater(int[] height, int expected)
    {
        int result = Problem13_TrappingRainWater.Trap(height);
        Assert.Equal(expected, result);
    }
}

