namespace SeniorPractice.Phase05.Tests;

public class Problem28_PalindromicSubstrings_Tests
{
    [Theory]
    [InlineData("abc", 3)]
    [InlineData("aaa", 6)]
    [InlineData("a", 1)]
    public void CountSubstrings_ShouldReturnTotalCount(string s, int expected)
    {
        Assert.Equal(expected, Problem28_PalindromicSubstrings.CountSubstrings(s));
    }
}
