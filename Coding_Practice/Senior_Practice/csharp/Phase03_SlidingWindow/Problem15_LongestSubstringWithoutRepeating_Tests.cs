namespace SeniorPractice.Phase03.Tests;

public class Problem15_LongestSubstringWithoutRepeating_Tests
{
    [Theory]
    [InlineData("abcabcbb", 3)]
    [InlineData("bbbbb", 1)]
    [InlineData("pwwkew", 3)]
    [InlineData("", 0)]
    [InlineData(" ", 1)]
    [InlineData("au", 2)]
    public void LengthOfLongestSubstring_ShouldReturnMaxLength(string s, int expected)
    {
        int result = Problem15_LongestSubstringWithoutRepeating.LengthOfLongestSubstring(s);
        Assert.Equal(expected, result);
    }
}
