namespace SeniorPractice.Phase03.Tests;

public class Problem18_MinimumWindowSubstring_Tests
{
    [Theory]
    [InlineData("ADOBECODEBANC", "ABC", "BANC")]
    [InlineData("a", "a", "a")]
    [InlineData("a", "aa", "")]
    public void MinWindow_ShouldReturnMinimalSubstring(string s, string t, string expected)
    {
        string result = Problem18_MinimumWindowSubstring.MinWindow(s, t);
        Assert.Equal(expected, result);
    }
}
