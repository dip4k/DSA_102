namespace SeniorPractice.Phase01.Tests;

public class Problem06_MajorityElement_Tests
{
    [Theory]
    [InlineData(new[] { 3, 2, 3 }, 3)]
    [InlineData(new[] { 2, 2, 1, 1, 1, 2, 2 }, 2)]
    [InlineData(new[] { 42 }, 42)]
    [InlineData(new[] { 6, 5, 5 }, 5)]
    [InlineData(new[] { 1, 1, 1, 2, 3, 1, 4, 1 }, 1)]
    public void MajorityElement_ShouldReturnDominantElement(int[] nums, int expected)
    {
        int result = Problem06_MajorityElement.MajorityElement(nums);
        Assert.Equal(expected, result);
    }
}

