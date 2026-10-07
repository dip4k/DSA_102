namespace SeniorPractice.Phase08;

/// <summary>
/// Problem #41: Binary Search (LeetCode #704)
/// Invariant: Search interval [left, right] with overflow-safe midpoint
/// </summary>
public static class Problem41_BinarySearch
{
    public static int Search(int[] nums, int target)
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

        return -1;
    }
}
