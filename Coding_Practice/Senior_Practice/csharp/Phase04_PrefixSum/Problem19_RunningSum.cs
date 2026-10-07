namespace SeniorPractice.Phase04;

/// <summary>
/// Problem #19: Running Sum of 1d Array (LeetCode #1480)
/// Invariant: result[i] = result[i-1] + nums[i]
/// </summary>
public static class Problem19_RunningSum
{
    public static int[] RunningSum(int[] nums)
    {
        ArgumentNullException.ThrowIfNull(nums);
        int[] result = new int[nums.Length];
        if (nums.Length == 0) return result;

        result[0] = nums[0];
        for (int i = 1; i < nums.Length; i++)
        {
            result[i] = result[i - 1] + nums[i];
        }

        return result;
    }
}
