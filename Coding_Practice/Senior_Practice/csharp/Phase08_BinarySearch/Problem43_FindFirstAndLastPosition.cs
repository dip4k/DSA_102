namespace SeniorPractice.Phase08;

/// <summary>
/// Problem #43: Find First and Last Position of Element in Sorted Array (LeetCode #34)
/// Invariant: Dual binary search for lower bound and upper bound
/// </summary>
public static class Problem43_FindFirstAndLastPosition
{
    public static int[] SearchRange(int[] nums, int target)
    {
        ArgumentNullException.ThrowIfNull(nums);
        int first = FindBound(nums, target, isFirst: true);
        if (first == -1) return new[] { -1, -1 };
        int last = FindBound(nums, target, isFirst: false);
        return new[] { first, last };
    }

    private static int FindBound(int[] nums, int target, bool isFirst)
    {
        int left = 0, right = nums.Length - 1;
        int bound = -1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            if (nums[mid] == target)
            {
                bound = mid;
                if (isFirst) right = mid - 1; // look left
                else left = mid + 1;          // look right
            }
            else if (nums[mid] < target)
            {
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }

        return bound;
    }
}
