namespace SeniorPractice.Phase05;

/// <summary>
/// Problem #28: Palindromic Substrings (LeetCode #647)
/// Invariant: Count all palindromic centers via expansion
/// </summary>
public static class Problem28_PalindromicSubstrings
{
    public static int CountSubstrings(string s)
    {
        if (string.IsNullOrEmpty(s)) return 0;
        int count = 0;

        for (int i = 0; i < s.Length; i++)
        {
            count += CountPalindromes(s, i, i);     // odd centers
            count += CountPalindromes(s, i, i + 1); // even centers
        }

        return count;
    }

    private static int CountPalindromes(string s, int left, int right)
    {
        int count = 0;
        while (left >= 0 && right < s.Length && s[left] == s[right])
        {
            count++;
            left--;
            right++;
        }
        return count;
    }
}
