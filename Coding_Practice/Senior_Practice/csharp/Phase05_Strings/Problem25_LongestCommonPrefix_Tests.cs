namespace SeniorPractice.Phase05.Tests;

public class Problem25_LongestCommonPrefix_Tests
{
    [Theory]
    [InlineData(new[] { "flower", "flow", "flight" }, "fl")]
    [InlineData(new[] { "dog", "racecar", "car" }, "")]
    [InlineData(new[] { "interspecies", "interstellar", "interstate" }, "inters")]
    [InlineData(new[] { "a" }, "a")]
    public void LongestCommonPrefix_ShouldReturnCommonPrefix(string[] strs, string expected)
    {
        Assert.Equal(expected, Problem25_LongestCommonPrefix.LongestCommonPrefix(strs));
    }
}
