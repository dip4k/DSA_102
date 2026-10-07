namespace SeniorPractice.Phase03.Tests;

public class Problem16_LongestRepeatingCharacterReplacement_Tests
{
    [Theory]
    [InlineData("ABAB", 2, 4)]
    [InlineData("AABABBA", 1, 4)]
    [InlineData("AAAA", 2, 4)]
    [InlineData("ABCDE", 1, 2)]
    public void CharacterReplacement_ShouldReturnMaxWindow(string s, int k, int expected)
    {
        int result = Problem16_LongestRepeatingCharacterReplacement.CharacterReplacement(s, k);
        Assert.Equal(expected, result);
    }
}
