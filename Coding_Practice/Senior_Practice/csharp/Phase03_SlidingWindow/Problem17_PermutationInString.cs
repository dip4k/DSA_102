namespace SeniorPractice.Phase03;

/// <summary>
/// Problem #17: Permutation in String (LeetCode #567)
/// Invariant: Fixed Window of size s1.Length with scalar match counter
/// </summary>
public static class Problem17_PermutationInString
{
    public static bool CheckInclusion(string s1, string s2)
    {
        if (string.IsNullOrEmpty(s1) || string.IsNullOrEmpty(s2) || s1.Length > s2.Length)
            return false;

        int[] s1Map = new int[26];
        int[] s2Map = new int[26];
        int k = s1.Length;

        for (int i = 0; i < k; i++)
        {
            s1Map[s1[i] - 'a']++;
            s2Map[s2[i] - 'a']++;
        }

        int matches = 0;
        for (int i = 0; i < 26; i++)
        {
            if (s1Map[i] == s2Map[i]) matches++;
        }

        if (matches == 26) return true;

        for (int i = k; i < s2.Length; i++)
        {
            int inChar = s2[i] - 'a';
            int outChar = s2[i - k] - 'a';

            s2Map[inChar]++;
            if (s2Map[inChar] == s1Map[inChar]) matches++;
            else if (s2Map[inChar] == s1Map[inChar] + 1) matches--;

            s2Map[outChar]--;
            if (s2Map[outChar] == s1Map[outChar]) matches++;
            else if (s2Map[outChar] == s1Map[outChar] - 1) matches--;

            if (matches == 26) return true;
        }

        return false;
    }
}
