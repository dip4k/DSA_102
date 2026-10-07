namespace SeniorPractice.Phase08;

/// <summary>
/// Problem #44: Search in Rotated Sorted Array (LeetCode #33)
/// Invariant: At least one half is strictly sorted; test target containment in sorted half
/// </summary>
public static class Problem44_SearchInRotatedSortedArray
{
    public static int Search(int[] nums, int target)
    {
        ArgumentNullException.ThrowIfNull(nums);
        int left = 0, right = nums.Length - 1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            if (nums[mid] == target) return mid;

            // Check if left half is sorted
            if (nums[left] <= nums[mid])
            {
                if (nums[left] <= target && target < nums[mid])
                    right = mid - 1;
                else
                    left = mid + 1;
            }
            else // Right half is sorted
            {
                if (nums[mid] < target && target <= nums[right])
                    left = mid + 1;
                else
                    right = mid - 1;
            }
        }

        return -1;
    }
}
