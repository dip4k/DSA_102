namespace SeniorPractice.Phase08.Tests;

public class Problem47_Search2DMatrix_Tests
{
    [Fact]
    public void SearchMatrix_ShouldFindTarget()
    {
        var matrix = new[]
        {
            new[] { 1, 3, 5, 7 },
            new[] { 10, 11, 16, 20 },
            new[] { 23, 30, 34, 60 }
        };

        Assert.True(Problem47_Search2DMatrix.SearchMatrix(matrix, 3));
        Assert.False(Problem47_Search2DMatrix.SearchMatrix(matrix, 13));
    }
}
