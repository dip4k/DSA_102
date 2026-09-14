namespace SeniorPractice.Phase01.Tests;

public class Problem03_ValidAnagram_Tests
{
    [Theory]
    [InlineData("anagram", "nagaram", true)]
    [InlineData("rat", "car", false)]
    [InlineData("a", "a", true)]
    [InlineData("ab", "a", false)]
    [InlineData("listen", "silent", true)]
    public void IsAnagram_ShouldIdentifyAnagrams(string s, string t, bool expected)
    {
        bool result = Problem03_ValidAnagram.IsAnagram(s, t);
        Assert.Equal(expected, result);
    }
}

