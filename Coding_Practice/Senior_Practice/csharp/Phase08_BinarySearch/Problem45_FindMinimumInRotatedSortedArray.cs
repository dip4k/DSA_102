namespace SeniorPractice.Phase08;

/// <summary>
/// Problem #45: Find Minimum in Rotated Sorted Array (LeetCode #153)
/// Invariant: Compare mid to right boundary to locate inflection point
/// </summary>
public static class Problem45_FindMinimumInRotatedSortedArray
{
    public static int FindMin(int[] nums)
    {
        ArgumentNullException.ThrowIfNull(nums);
        int left = 0, right = nums.Length - 1;

        while (left < right)
        {
            int mid = left + (right - left) / 2;
            if (nums[mid] > nums[right])
            {
                left = mid + 1; // inflection is in right half
            }
            else
            {
                right = mid;    // min is at mid or to the left
            }
        }

        return nums[left];
    }
}
