namespace SeniorPractice.Phase01;

/// <summary>
/// Problem #6: Majority Element (LeetCode #169)
/// Difficulty: Easy | Priority: High
/// Governing Invariant: Boyer-Moore Pairwise Annihilation — Strict majority surplus survives 1:1 pairwise cancellations.
/// </summary>
public static class Problem06_MajorityElement
{
    public static int MajorityElement(int[] nums)
    {
        ArgumentNullException.ThrowIfNull(nums);
        if (nums.Length == 0) throw new ArgumentException("Array cannot be empty.", nameof(nums));

        int candidate = nums[0];
        int count = 0;

        foreach (int num in nums)
        {
            if (count == 0)
            {
                candidate = num;
            }

            count += (num == candidate) ? 1 : -1;
        }

        return candidate;
    }
}

