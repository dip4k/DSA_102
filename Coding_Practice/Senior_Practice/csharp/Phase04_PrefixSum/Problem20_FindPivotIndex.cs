namespace SeniorPractice.Phase04;

/// <summary>
/// Problem #20: Find Pivot Index (LeetCode #724)
/// Invariant: leftSum == totalSum - leftSum - nums[i]
/// </summary>
public static class Problem20_FindPivotIndex
{
    public static int PivotIndex(int[] nums)
    {
        ArgumentNullException.ThrowIfNull(nums);
        int totalSum = 0;
        foreach (int x in nums) totalSum += x;

        int leftSum = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            if (leftSum == totalSum - leftSum - nums[i])
            {
                return i;
            }
            leftSum += nums[i];
        }

        return -1;
    }
}
