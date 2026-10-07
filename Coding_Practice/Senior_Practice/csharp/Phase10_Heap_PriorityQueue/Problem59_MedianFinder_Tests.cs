namespace SeniorPractice.Phase10.Tests;

public class Problem59_MedianFinder_Tests
{
    [Fact]
    public void MedianFinder_ShouldCalculateStreamingMedian()
    {
        var mf = new Problem59_MedianFinder();
        mf.AddNum(1);
        mf.AddNum(2);
        Assert.Equal(1.5, mf.FindMedian());
        mf.AddNum(3);
        Assert.Equal(2.0, mf.FindMedian());
    }
}
