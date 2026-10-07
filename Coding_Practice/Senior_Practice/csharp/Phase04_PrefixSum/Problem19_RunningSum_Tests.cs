namespace SeniorPractice.Phase04.Tests;

public class Problem19_RunningSum_Tests
{
    [Fact]
    public void RunningSum_ShouldReturnCumulativeSums()
    {
        Assert.Equal(new[] { 1, 3, 6, 10 }, Problem19_RunningSum.RunningSum(new[] { 1, 2, 3, 4 }));
        Assert.Equal(new[] { 1, 2, 3, 4, 5 }, Problem19_RunningSum.RunningSum(new[] { 1, 1, 1, 1, 1 }));
        Assert.Equal(new[] { 3, 4, 6, 16, 17 }, Problem19_RunningSum.RunningSum(new[] { 3, 1, 2, 10, 1 }));
    }
}
