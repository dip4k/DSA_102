namespace SeniorPractice.Phase02;

/// <summary>
/// Problem #8: Valid Palindrome (LeetCode #125)
/// Difficulty: Easy | Priority: Reinforcement
/// Governing Invariant: Opposing Inward Convergence — s[left] == s[right] across alphanumeric characters.
/// </summary>
public static class Problem08_ValidPalindrome
{
    public static bool IsPalindrome(string s)
    {
        if (string.IsNullOrEmpty(s)) return true;

        int left = 0;
        int right = s.Length - 1;

        while (left < right)
        {
            while (left < right && !char.IsLetterOrDigit(s[left])) left++;
            while (left < right && !char.IsLetterOrDigit(s[right])) right--;

            if (char.ToLowerInvariant(s[left]) != char.ToLowerInvariant(s[right]))
            {
                return false;
            }

            left++;
            right--;
        }

        return true;
    }
}

