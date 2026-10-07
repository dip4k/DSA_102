namespace SeniorPractice.Phase05;

/// <summary>
/// Problem #27: Longest Palindromic Substring (LeetCode #5)
/// Invariant: Expand around center for 2N - 1 candidate centers
/// </summary>
public static class Problem27_LongestPalindromicSubstring
{
    public static string LongestPalindrome(string s)
    {
        if (string.IsNullOrEmpty(s)) return string.Empty;

        int start = 0, maxLen = 0;

        for (int i = 0; i < s.Length; i++)
        {
            // Odd length palindromes centered at i
            int len1 = ExpandAroundCenter(s, i, i);
            // Even length palindromes centered at i, i + 1
            int len2 = ExpandAroundCenter(s, i, i + 1);

            int best = Math.Max(len1, len2);
            if (best > maxLen)
            {
                maxLen = best;
                start = i - (best - 1) / 2;
            }
        }

        return s.Substring(start, maxLen);
    }

    private static int ExpandAroundCenter(string s, int left, int right)
    {
        while (left >= 0 && right < s.Length && s[left] == s[right])
        {
            left--;
            right++;
        }
        return right - left - 1;
    }
}
