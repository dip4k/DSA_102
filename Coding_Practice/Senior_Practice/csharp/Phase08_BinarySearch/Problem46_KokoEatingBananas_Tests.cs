namespace SeniorPractice.Phase08.Tests;

public class Problem46_KokoEatingBananas_Tests
{
    [Theory]
    [InlineData(new[] { 3, 6, 7, 11 }, 8, 4)]
    [InlineData(new[] { 30, 11, 23, 4, 20 }, 5, 30)]
    [InlineData(new[] { 30, 11, 23, 4, 20 }, 6, 23)]
    public void MinEatingSpeed_ShouldReturnOptimalSpeed(int[] piles, int h, int expected)
    {
        Assert.Equal(expected, Problem46_KokoEatingBananas.MinEatingSpeed(piles, h));
    }
}
