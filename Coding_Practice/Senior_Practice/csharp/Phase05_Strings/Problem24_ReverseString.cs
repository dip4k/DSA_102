namespace SeniorPractice.Phase05;

/// <summary>
/// Problem #24: Reverse String (LeetCode #344)
/// Invariant: In-place two-pointer swap from opposing ends
/// </summary>
public static class Problem24_ReverseString
{
    public static void ReverseString(char[] s)
    {
        ArgumentNullException.ThrowIfNull(s);
        int left = 0, right = s.Length - 1;
        while (left < right)
        {
            (s[left], s[right]) = (s[right], s[left]);
            left++;
            right--;
        }
    }
}
