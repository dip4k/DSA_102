namespace SeniorPractice.Phase02.Tests;

public class Problem12_FourSum_Tests
{
    [Fact]
    public void FourSum_StandardCase_ShouldReturnUniqueQuadruplets()
    {
        int[] nums = [1, 0, -1, 0, -2, 2];
        int target = 0;
        var result = Problem12_FourSum.FourSum(nums, target);

        Assert.Equal(3, result.Count);
        Assert.Contains(result, q => q.OrderBy(x => x).SequenceEqual(new[] { -2, -1, 1, 2 }));
        Assert.Contains(result, q => q.OrderBy(x => x).SequenceEqual(new[] { -2, 0, 0, 2 }));
        Assert.Contains(result, q => q.OrderBy(x => x).SequenceEqual(new[] { -1, 0, 0, 1 }));
    }

    [Fact]
    public void FourSum_IdenticalValues_ShouldReturnSingleQuadruplet()
    {
        int[] nums = [2, 2, 2, 2, 2];
        int target = 8;
        var result = Problem12_FourSum.FourSum(nums, target);

        Assert.Single(result);
        Assert.Equal(new[] { 2, 2, 2, 2 }, result[0]);
    }

    [Fact]
    public void FourSum_OverflowDefense_ShouldNotWrapAround()
    {
        int[] nums = [1000000000, 1000000000, 1000000000, 1000000000];
        int target = -294967296; // Target that matches 32-bit integer overflow wrap
        var result = Problem12_FourSum.FourSum(nums, target);

        Assert.Empty(result); // Must be empty because 4*10^9 != -294967296
    }
}

