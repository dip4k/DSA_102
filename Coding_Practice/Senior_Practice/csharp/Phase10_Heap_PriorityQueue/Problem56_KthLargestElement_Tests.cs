namespace SeniorPractice.Phase10.Tests;

public class Problem56_KthLargestElement_Tests
{
    [Theory]
    [InlineData(new[] { 3, 2, 1, 5, 6, 4 }, 2, 5)]
    [InlineData(new[] { 3, 2, 3, 1, 2, 4, 5, 5, 6 }, 4, 4)]
    public void FindKthLargest_ShouldReturnKthLargestElement(int[] nums, int k, int expected)
    {
        Assert.Equal(expected, Problem56_KthLargestElement.FindKthLargest(nums, k));
    }
}
