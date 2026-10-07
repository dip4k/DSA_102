namespace SeniorPractice.Phase08;

/// <summary>
/// Problem #42: Search Insert Position (LeetCode #35)
/// Invariant: Lower bound — first index where nums[mid] >= target
/// </summary>
public static class Problem42_SearchInsertPosition
{
    public static int SearchInsert(int[] nums, int target)
    {
        ArgumentNullException.ThrowIfNull(nums);
        int left = 0, right = nums.Length - 1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            if (nums[mid] == target) return mid;
            if (nums[mid] < target) left = mid + 1;
            else right = mid - 1;
        }

        return left;
    }
}
