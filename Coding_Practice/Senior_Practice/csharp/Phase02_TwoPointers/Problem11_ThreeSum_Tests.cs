namespace SeniorPractice.Phase02.Tests;

public class Problem11_ThreeSum_Tests
{
    [Fact]
    public void ThreeSum_StandardCase_ShouldReturnUniqueTriplets()
    {
        int[] nums = [-1, 0, 1, 2, -1, -4];
        var result = Problem11_ThreeSum.ThreeSum(nums);

        Assert.Equal(2, result.Count);
        Assert.Contains(result, t => t.OrderBy(x => x).SequenceEqual(new[] { -1, -1, 2 }));
        Assert.Contains(result, t => t.OrderBy(x => x).SequenceEqual(new[] { -1, 0, 1 }));
    }

    [Fact]
    public void ThreeSum_AllZeros_ShouldReturnSingleTriplet()
    {
        int[] nums = [0, 0, 0, 0];
        var result = Problem11_ThreeSum.ThreeSum(nums);

        Assert.Single(result);
        Assert.Equal(new[] { 0, 0, 0 }, result[0]);
    }

    [Fact]
    public void ThreeSum_NoSolution_ShouldReturnEmpty()
    {
        int[] nums = [1, 2, 3];
        var result = Problem11_ThreeSum.ThreeSum(nums);
        Assert.Empty(result);
    }
}

