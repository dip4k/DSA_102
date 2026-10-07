namespace SeniorPractice.Phase03;

/// <summary>
/// Problem #14: Maximum Average Subarray I (LeetCode #643)
/// Invariant: Fixed Window of size K — sum += nums[i] - nums[i - k]
/// </summary>
public static class Problem14_MaxAverageSubarrayI
{
    public static double FindMaxAverage(int[] nums, int k)
    {
        ArgumentNullException.ThrowIfNull(nums);
        if (nums.Length < k || k <= 0) return 0.0;

        double currentSum = 0;
        for (int i = 0; i < k; i++) currentSum += nums[i];

        double maxSum = currentSum;
        for (int i = k; i < nums.Length; i++)
        {
            currentSum += nums[i] - nums[i - k];
            if (currentSum > maxSum) maxSum = currentSum;
        }

        return maxSum / k;
    }
}
