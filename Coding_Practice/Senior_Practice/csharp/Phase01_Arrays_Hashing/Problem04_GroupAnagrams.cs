namespace SeniorPractice.Phase01;

/// <summary>
/// Problem #4: Group Anagrams (LeetCode #49)
/// Difficulty: Medium | Priority: Core
/// Governing Invariant: Canonical Key Partitioning — Words w_1, w_2 share the same sorted/frequency key.
/// </summary>
public static class Problem04_GroupAnagrams
{
    public static IList<IList<string>> GroupAnagrams(string[] strs)
    {
        if (strs == null || strs.Length == 0) return [];

        var groups = new Dictionary<string, List<string>>(capacity: strs.Length);

        foreach (string word in strs)
        {
            char[] chars = word.ToCharArray();
            Array.Sort(chars);
            string key = new string(chars);

            if (!groups.TryGetValue(key, out var list))
            {
                list = new List<string>();
                groups[key] = list;
            }
            list.Add(word);
        }

        return groups.Values.Cast<IList<string>>().ToList();
    }
}

