namespace SeniorPractice.Phase04;

/// <summary>
/// Problem #22: Maximum Subarray (LeetCode #53)
/// Invariant: Kadane's Algorithm — currentMax = Math.Max(nums[i], currentMax + nums[i])
/// </summary>
public static class Problem22_MaximumSubarray
{
    public static int MaxSubArray(int[] nums)
    {
        ArgumentNullException.ThrowIfNull(nums);
        if (nums.Length == 0) return 0;

        int currentMax = nums[0];
        int globalMax = nums[0];

        for (int i = 1; i < nums.Length; i++)
        {
            currentMax = Math.Max(nums[i], currentMax + nums[i]);
            globalMax = Math.Max(globalMax, currentMax);
        }

        return globalMax;
    }
}
