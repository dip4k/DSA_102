namespace SeniorPractice.Phase01;

/// <summary>
/// Problem #1: Two Sum (LeetCode #1)
/// Difficulty: Easy | Priority: Core
/// Governing Invariant: Complement Lookup — nums[i] + complement = target <=> complement = target - nums[i]
/// </summary>
public static class Problem01_TwoSum
{
    public static int[] TwoSum(int[] nums, int target)
    {
        ArgumentNullException.ThrowIfNull(nums);

        var seen = new Dictionary<int, int>(capacity: nums.Length);

        for (int i = 0; i < nums.Length; i++)
        {
            int complement = target - nums[i];
            if (seen.TryGetValue(complement, out int prevIndex))
            {
                return [prevIndex, i];
            }

            seen[nums[i]] = i;
        }

        return [];
    }
}
