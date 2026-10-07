namespace SeniorPractice.Phase10.Tests;

public class Problem57_TopKFrequentElements_Tests
{
    [Fact]
    public void TopKFrequent_ShouldReturnMostFrequent()
    {
        var res1 = Problem57_TopKFrequentElements.TopKFrequent(new[] { 1, 1, 1, 2, 2, 3 }, 2);
        Assert.True(res1.Contains(1) && res1.Contains(2) && res1.Length == 2);

        var res2 = Problem57_TopKFrequentElements.TopKFrequent(new[] { 1 }, 1);
        Assert.Equal(new[] { 1 }, res2);
    }
}
