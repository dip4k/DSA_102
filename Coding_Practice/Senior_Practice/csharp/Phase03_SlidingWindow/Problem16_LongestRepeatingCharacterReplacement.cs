namespace SeniorPractice.Phase03;

/// <summary>
/// Problem #16: Longest Repeating Character Replacement (LeetCode #424)
/// Invariant: Variable Window — (length - maxFreq) <= k. Shrink left when invalid.
/// </summary>
public static class Problem16_LongestRepeatingCharacterReplacement
{
    public static int CharacterReplacement(string s, int k)
    {
        if (string.IsNullOrEmpty(s)) return 0;
        if (s.Length <= k) return s.Length;

        int[] counts = new int[26];
        int left = 0, maxFreq = 0, maxWindow = 0;

        for (int right = 0; right < s.Length; right++)
        {
            int idx = s[right] - 'A';
            counts[idx]++;
            if (counts[idx] > maxFreq) maxFreq = counts[idx];

            while ((right - left + 1) - maxFreq > k)
            {
                counts[s[left] - 'A']--;
                left++;
            }

            maxWindow = Math.Max(maxWindow, right - left + 1);
        }

        return maxWindow;
    }
}
