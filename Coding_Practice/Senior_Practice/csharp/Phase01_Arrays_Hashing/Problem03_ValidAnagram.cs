namespace SeniorPractice.Phase01;

/// <summary>
/// Problem #3: Valid Anagram (LeetCode #242)
/// Difficulty: Easy | Priority: Core
/// Governing Invariant: Histogram Net Zero Balance — Sum(|freq[c]|) = 0 <=> strings are anagrams.
/// </summary>
public static class Problem03_ValidAnagram
{
    public static bool IsAnagram(string s, string t)
    {
        if (s == null || t == null || s.Length != t.Length) return false;

        Span<int> freq = stackalloc int[26];

        for (int i = 0; i < s.Length; i++)
        {
            freq[s[i] - 'a']++;
            freq[t[i] - 'a']--;
        }

        foreach (int count in freq)
        {
            if (count != 0) return false;
        }

        return true;
    }
}

