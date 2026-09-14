namespace SeniorPractice.Phase01.Tests;

public class Problem05_LongestConsecutiveSequence_Tests
{
    [Theory]
    [InlineData(new[] { 100, 4, 200, 1, 3, 2 }, 4)]
    [InlineData(new[] { 0, 3, 7, 2, 5, 8, 4, 6, 0, 1 }, 9)]
    [InlineData(new int[] { }, 0)]
    [InlineData(new[] { 42 }, 1)]
    [InlineData(new[] { 1, 2, 0, 1 }, 3)]
    [InlineData(new[] { -2, -1, 0, 1, 2 }, 5)]
    public void LongestConsecutive_ShouldReturnMaxStreak(int[] nums, int expected)
    {
        int result = Problem05_LongestConsecutiveSequence.LongestConsecutive(nums);
        Assert.Equal(expected, result);
    }
}

