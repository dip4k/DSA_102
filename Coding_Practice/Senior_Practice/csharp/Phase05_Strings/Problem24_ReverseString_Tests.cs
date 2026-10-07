namespace SeniorPractice.Phase05.Tests;

public class Problem24_ReverseString_Tests
{
    [Fact]
    public void ReverseString_ShouldReverseInPlace()
    {
        char[] s1 = new[] { 'h', 'e', 'l', 'l', 'o' };
        Problem24_ReverseString.ReverseString(s1);
        Assert.Equal(new[] { 'o', 'l', 'l', 'e', 'h' }, s1);

        char[] s2 = new[] { 'H', 'a', 'n', 'n', 'a', 'h' };
        Problem24_ReverseString.ReverseString(s2);
        Assert.Equal(new[] { 'h', 'a', 'n', 'n', 'a', 'H' }, s2);
    }
}
