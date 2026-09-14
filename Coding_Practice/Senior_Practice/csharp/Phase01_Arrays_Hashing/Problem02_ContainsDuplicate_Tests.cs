namespace SeniorPractice.Phase01.Tests;

public class Problem02_ContainsDuplicate_Tests
{
    [Theory]
    [InlineData(new[] { 1, 2, 3, 1 }, true)]
    [InlineData(new[] { 1, 2, 3, 4 }, false)]
    [InlineData(new[] { 1, 1, 1, 3, 3, 4, 3, 2, 4, 2 }, true)]
    [InlineData(new[] { 42 }, false)]
    [InlineData(new[] { -1, -2, -3, -1 }, true)]
    public void ContainsDuplicate_ShouldDetectDuplicates(int[] nums, bool expected)
    {
        bool result = Problem02_ContainsDuplicate.ContainsDuplicate(nums);
        Assert.Equal(expected, result);
    }
}

