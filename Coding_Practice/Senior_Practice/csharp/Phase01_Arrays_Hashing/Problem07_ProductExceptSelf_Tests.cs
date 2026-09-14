namespace SeniorPractice.Phase01.Tests;

public class Problem07_ProductExceptSelf_Tests
{
    [Theory]
    [InlineData(new[] { 1, 2, 3, 4 }, new[] { 24, 12, 8, 6 })]
    [InlineData(new[] { -1, 1, 0, -3, 3 }, new[] { 0, 0, 9, 0, 0 })]
    [InlineData(new[] { 0, 0 }, new[] { 0, 0 })]
    [InlineData(new[] { 5, 2 }, new[] { 2, 5 })]
    [InlineData(new[] { 2, 3, 4, 5 }, new[] { 60, 40, 30, 24 })]
    public void ProductExceptSelf_ShouldComputeProductsWithoutDivision(int[] nums, int[] expected)
    {
        int[] result = Problem07_ProductExceptSelf.ProductExceptSelf(nums);
        Assert.Equal(expected, result);
    }
}

