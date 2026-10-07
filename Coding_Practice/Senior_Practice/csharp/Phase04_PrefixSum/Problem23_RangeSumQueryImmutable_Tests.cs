namespace SeniorPractice.Phase04.Tests;

public class Problem23_RangeSumQueryImmutable_Tests
{
    [Fact]
    public void NumArray_ShouldReturnCorrectRangeSums()
    {
        var array = new Problem23_NumArray(new[] { -2, 0, 3, -5, 2, -1 });
        Assert.Equal(1, array.SumRange(0, 2));
        Assert.Equal(-1, array.SumRange(2, 5));
        Assert.Equal(-3, array.SumRange(0, 5));
    }
}
