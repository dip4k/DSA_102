namespace SeniorPractice.Phase03;

/// <summary>
/// Problem #18: Minimum Window Substring (LeetCode #76)
/// Invariant: Shrinkable Window — expand right until valid, then shrink left to minimize
/// </summary>
public static class Problem18_MinimumWindowSubstring
{
    public static string MinWindow(string s, string t)
    {
        if (string.IsNullOrEmpty(s) || string.IsNullOrEmpty(t) || s.Length < t.Length)
            return string.Empty;

        int[] targetCounts = new int[128];
        foreach (char c in t) targetCounts[c]++;

        int[] windowCounts = new int[128];
        int required = 0;
        for (int i = 0; i < 128; i++) if (targetCounts[i] > 0) required++;

        int formed = 0, left = 0;
        int minLen = int.MaxValue, minStart = 0;

        for (int right = 0; right < s.Length; right++)
        {
            char c = s[right];
            windowCounts[c]++;
            if (targetCounts[c] > 0 && windowCounts[c] == targetCounts[c])
            {
                formed++;
            }

            while (left <= right && formed == required)
            {
                if (right - left + 1 < minLen)
                {
                    minLen = right - left + 1;
                    minStart = left;
                }

                char removeChar = s[left];
                windowCounts[removeChar]--;
                if (targetCounts[removeChar] > 0 && windowCounts[removeChar] < targetCounts[removeChar])
                {
                    formed--;
                }
                left++;
            }
        }

        return minLen == int.MaxValue ? string.Empty : s.Substring(minStart, minLen);
    }
}
