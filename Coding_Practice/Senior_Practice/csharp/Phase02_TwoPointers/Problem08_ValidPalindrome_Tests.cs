namespace SeniorPractice.Phase02.Tests;

public class Problem08_ValidPalindrome_Tests
{
    [Theory]
    [InlineData("A man, a plan, a canal: Panama", true)]
    [InlineData("race a car", false)]
    [InlineData(" ", true)]
    [InlineData("., ", true)]
    [InlineData("0P", false)]
    [InlineData("a.", true)]
    public void IsPalindrome_ShouldValidateCorrectly(string s, bool expected)
    {
        bool result = Problem08_ValidPalindrome.IsPalindrome(s);
        Assert.Equal(expected, result);
    }
}

