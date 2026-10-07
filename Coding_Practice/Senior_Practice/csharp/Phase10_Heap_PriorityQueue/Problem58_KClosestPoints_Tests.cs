namespace SeniorPractice.Phase10.Tests;

public class Problem58_KClosestPoints_Tests
{
    [Fact]
    public void KClosest_ShouldReturnClosestPoints()
    {
        var pts = new[]
        {
            new[] { 1, 3 },
            new[] { -2, 2 }
        };

        var res = Problem58_KClosestPoints.KClosest(pts, 1);
        Assert.Single(res);
        Assert.Equal(new[] { -2, 2 }, res[0]);
    }
}
