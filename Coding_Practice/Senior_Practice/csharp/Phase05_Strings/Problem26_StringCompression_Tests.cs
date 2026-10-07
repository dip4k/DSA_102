namespace SeniorPractice.Phase05.Tests;

public class Problem26_StringCompression_Tests
{
    [Fact]
    public void Compress_ShouldCompressInPlace()
    {
        char[] c1 = new[] { 'a', 'a', 'b', 'b', 'c', 'c', 'c' };
        int len1 = Problem26_StringCompression.Compress(c1);
        Assert.Equal(6, len1);
        Assert.Equal("a2b2c3", new string(c1, 0, len1));

        char[] c2 = new[] { 'a' };
        int len2 = Problem26_StringCompression.Compress(c2);
        Assert.Equal(1, len2);
        Assert.Equal("a", new string(c2, 0, len2));

        char[] c3 = new[] { 'a', 'b', 'b', 'b', 'b', 'b', 'b', 'b', 'b', 'b', 'b', 'b', 'b' };
        int len3 = Problem26_StringCompression.Compress(c3);
        Assert.Equal(4, len3);
        Assert.Equal("ab12", new string(c3, 0, len3));
    }
}
