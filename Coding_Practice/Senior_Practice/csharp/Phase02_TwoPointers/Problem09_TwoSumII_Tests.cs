namespace SeniorPractice.Phase02.Tests;

public class Problem09_TwoSumII_Tests
{
    [Theory]
    [InlineData(new[] { 2, 7, 11, 15 }, 9, new[] { 1, 2 })]
    [InlineData(new[] { 2, 3, 4 }, 6, new[] { 1, 3 })]
    [InlineData(new[] { -1, 0 }, -1, new[] { 1, 2 })]
    [InlineData(new[] { -1000, -500, 0, 1000 }, 0, new[] { 1, 4 })]
    [InlineData(new[] { 0, 0, 3, 4 }, 0, new[] { 1, 2 })]
    public void TwoSum_ShouldReturnOneBasedIndices(int[] numbers, int target, int[] expected)
    {
        int[] result = Problem09_TwoSumII.TwoSum(numbers, target);
        Assert.Equal(expected, result);
    }
}

