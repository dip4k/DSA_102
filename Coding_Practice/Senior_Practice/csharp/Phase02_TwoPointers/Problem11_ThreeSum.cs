namespace SeniorPractice.Phase02;

/// <summary>
/// Problem #11: 3Sum (LeetCode #15)
/// Difficulty: Medium | Priority: Core
/// Governing Invariant: Fixed Anchor + Opposing Two Pointers with In-Place Deduplication.
/// </summary>
public static class Problem11_ThreeSum
{
    public static IList<IList<int>> ThreeSum(int[] nums)
    {
        if (nums == null || nums.Length < 3) return [];

        Array.Sort(nums);
        var result = new List<IList<int>>();
        int n = nums.Length;

        for (int i = 0; i < n - 2; i++)
        {
            if (nums[i] > 0) break;
            if (i > 0 && nums[i] == nums[i - 1]) continue;

            int left = i + 1;
            int right = n - 1;

            while (left < right)
            {
                int sum = nums[i] + nums[left] + nums[right];

                if (sum == 0)
                {
                    result.Add([nums[i], nums[left], nums[right]]);
                    left++;
                    right--;

                    while (left < right && nums[left] == nums[left - 1]) left++;
                    while (left < right && nums[right] == nums[right + 1]) right--;
                }
                else if (sum < 0)
                {
                    left++;
                }
                else
                {
                    right--;
                }
            }
        }

        return result;
    }
}

