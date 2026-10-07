namespace SeniorPractice.Phase03.Tests;

public class Problem17_PermutationInString_Tests
{
    [Theory]
    [InlineData("ab", "eidbaooo", true)]
    [InlineData("ab", "eidboaoo", false)]
    [InlineData("adc", "dcda", true)]
    [InlineData("hello", "ooolleoooleh", false)]
    public void CheckInclusion_ShouldReturnExpected(string s1, string s2, bool expected)
    {
        bool result = Problem17_PermutationInString.CheckInclusion(s1, s2);
        Assert.Equal(expected, result);
    }
}
