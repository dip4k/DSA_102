namespace SeniorPractice.Phase05;

/// <summary>
/// Problem #26: String Compression (LeetCode #443)
/// Invariant: Two pointers — read advances run-length, write commits char + digits in-place
/// </summary>
public static class Problem26_StringCompression
{
    public static int Compress(char[] chars)
    {
        ArgumentNullException.ThrowIfNull(chars);
        int write = 0;
        int read = 0;

        while (read < chars.Length)
        {
            char curr = chars[read];
            int runStart = read;

            while (read < chars.Length && chars[read] == curr)
            {
                read++;
            }

            int count = read - runStart;
            chars[write++] = curr;

            if (count > 1)
            {
                foreach (char digit in count.ToString())
                {
                    chars[write++] = digit;
                }
            }
        }

        return write;
    }
}
