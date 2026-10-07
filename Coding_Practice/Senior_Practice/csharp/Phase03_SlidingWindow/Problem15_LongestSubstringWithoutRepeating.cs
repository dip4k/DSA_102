namespace SeniorPractice.Phase03;

/// <summary>
/// Problem #15: Longest Substring Without Repeating Characters (LeetCode #3)
/// Invariant: Dynamic Window — lastSeen[c] advances left boundary past duplicate
/// </summary>
public static class Problem15_LongestSubstringWithoutRepeating
{
    public static int LengthOfLongestSubstring(string s)
    {
        if (string.IsNullOrEmpty(s)) return 0;

        int[] lastPos = new int[128];
        Array.Fill(lastPos, -1);

        int maxLen = 0, left = 0;
        for (int right = 0; right < s.Length; right++)
        {
            char c = s[right];
            if (lastPos[c] >= left)
            {
                left = lastPos[c] + 1;
            }
            lastPos[c] = right;
            maxLen = Math.Max(maxLen, right - left + 1);
        }

        return maxLen;
    }
}
