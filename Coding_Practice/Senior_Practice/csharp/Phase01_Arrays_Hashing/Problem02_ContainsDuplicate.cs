namespace SeniorPractice.Phase01;

/// <summary>
/// Problem #2: Contains Duplicate (LeetCode #217)
/// Difficulty: Easy | Priority: Core
/// Governing Invariant: Set Membership Invariant — S_k = {nums[0] ... nums[k-1]}. nums[k] in S_k => duplicate.
/// </summary>
public static class Problem02_ContainsDuplicate
{
    public static bool ContainsDuplicate(int[] nums)
    {
        ArgumentNullException.ThrowIfNull(nums);

        var seen = new HashSet<int>(capacity: nums.Length);

        foreach (int num in nums)
        {
            if (!seen.Add(num))
            {
                return true;
            }
        }

        return false;
    }
}

