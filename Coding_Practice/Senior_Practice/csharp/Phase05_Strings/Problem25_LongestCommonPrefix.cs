namespace SeniorPractice.Phase05;

/// <summary>
/// Problem #25: Longest Common Prefix (LeetCode #14)
/// Invariant: Vertical scan across all strings at index col
/// </summary>
public static class Problem25_LongestCommonPrefix
{
    public static string LongestCommonPrefix(string[] strs)
    {
        if (strs == null || strs.Length == 0) return string.Empty;

        for (int col = 0; col < strs[0].Length; col++)
        {
            char c = strs[0][col];
            for (int row = 1; row < strs.Length; row++)
            {
                if (col == strs[row].Length || strs[row][col] != c)
                {
                    return strs[0].Substring(0, col);
                }
            }
        }

        return strs[0];
    }
}
