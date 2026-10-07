namespace SeniorPractice.Phase07.Tests;

public class Problem36_ValidParentheses_Tests
{
    [Theory]
    [InlineData("()", true)]
    [InlineData("()[]{}", true)]
    [InlineData("(]", false)]
    [InlineData("([)]", false)]
    [InlineData("{[]}", true)]
    [InlineData("]", false)]
    public void IsValid_ShouldValidateParentheses(string s, bool expected)
    {
        Assert.Equal(expected, Problem36_ValidParentheses.IsValid(s));
    }
}
