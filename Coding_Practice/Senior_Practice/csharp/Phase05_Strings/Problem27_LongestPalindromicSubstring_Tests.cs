namespace SeniorPractice.Phase05.Tests;

public class Problem27_LongestPalindromicSubstring_Tests
{
    [Theory]
    [InlineData("babad", 3)] // "bab" or "aba"
    [InlineData("cbbd", 2)]  // "bb"
    [InlineData("a", 1)]
    [InlineData("ac", 1)]
    public void LongestPalindrome_ShouldReturnCorrectLength(string s, int expectedLength)
    {
        string result = Problem27_LongestPalindromicSubstring.LongestPalindrome(s);
        Assert.Equal(expectedLength, result.Length);
    }
}
