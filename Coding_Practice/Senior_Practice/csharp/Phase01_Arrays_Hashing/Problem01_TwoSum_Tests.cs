namespace SeniorPractice.Phase01.Tests;

public class Problem01_TwoSum_Tests
{
    [Theory]
    [InlineData(new[] { 2, 7, 11, 15 }, 9, new[] { 0, 1 })]
    [InlineData(new[] { 3, 2, 4 }, 6, new[] { 1, 2 })]
    [InlineData(new[] { 3, 3 }, 6, new[] { 0, 1 })]
    [InlineData(new[] { -1, -2, -3, -4, -5 }, -8, new[] { 2, 4 })]
    [InlineData(new[] { 0, 4, 3, 0 }, 0, new[] { 0, 3 })]
    public void TwoSum_ShouldReturnCorrectIndices(int[] nums, int target, int[] expected)
    {
        int[] result = Problem01_TwoSum.TwoSum(nums, target);
        Assert.Equal(expected, result);
    }
}
