namespace SeniorPractice.Phase07.Tests;

public class Problem39_DailyTemperatures_Tests
{
    [Fact]
    public void DailyTemperatures_ShouldComputeSpanToWarmerDay()
    {
        var temps = new[] { 73, 74, 75, 71, 69, 72, 76, 73 };
        var expected = new[] { 1, 1, 4, 2, 1, 1, 0, 0 };
        Assert.Equal(expected, Problem39_DailyTemperatures.DailyTemperatures(temps));

        Assert.Equal(new[] { 1, 1, 1, 0 }, Problem39_DailyTemperatures.DailyTemperatures(new[] { 30, 40, 50, 60 }));
        Assert.Equal(new[] { 1, 1, 0 }, Problem39_DailyTemperatures.DailyTemperatures(new[] { 30, 60, 90 }));
    }
}
