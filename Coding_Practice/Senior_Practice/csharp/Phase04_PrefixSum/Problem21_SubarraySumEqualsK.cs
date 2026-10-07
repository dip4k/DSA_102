namespace SeniorPractice.Phase04;

/// <summary>
/// Problem #21: Subarray Sum Equals K (LeetCode #560)
/// Invariant: prefix[j] - prefix[i] = k => prefix[i] = prefix[j] - k lookup in frequency map
/// </summary>
public static class Problem21_SubarraySumEqualsK
{
    public static int SubarraySum(int[] nums, int k)
    {
        ArgumentNullException.ThrowIfNull(nums);
        var prefixFreq = new Dictionary<int, int> { [0] = 1 };
        int currentPrefix = 0;
        int count = 0;

        foreach (int x in nums)
        {
            currentPrefix += x;
            int targetPrefix = currentPrefix - k;

            if (prefixFreq.TryGetValue(targetPrefix, out int matches))
            {
                count += matches;
            }

            prefixFreq[currentPrefix] = prefixFreq.GetValueOrDefault(currentPrefix, 0) + 1;
        }

        return count;
    }
}
