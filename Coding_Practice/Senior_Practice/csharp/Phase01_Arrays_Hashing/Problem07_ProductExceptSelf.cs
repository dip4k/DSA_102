namespace SeniorPractice.Phase01;

/// <summary>
/// Problem #7: Product of Array Except Self (LeetCode #238)
/// Difficulty: Medium | Priority: Core
/// Governing Invariant: Prefix/Suffix Decomposition — Answer[i] = Prefix[i-1] * Suffix[i+1].
/// </summary>
public static class Problem07_ProductExceptSelf
{
    public static int[] ProductExceptSelf(int[] nums)
    {
        ArgumentNullException.ThrowIfNull(nums);
        int n = nums.Length;
        if (n < 2) throw new ArgumentException("Array must contain at least 2 elements.", nameof(nums));

        int[] result = new int[n];

        // Pass 1: Build left prefix products directly in result array
        result[0] = 1;
        for (int i = 1; i < n; i++)
        {
            result[i] = result[i - 1] * nums[i - 1];
        }

        // Pass 2: Accumulate right suffix products using a scalar register
        int suffixProduct = 1;
        for (int i = n - 1; i >= 0; i--)
        {
            result[i] *= suffixProduct;
            suffixProduct *= nums[i];
        }

        return result;
    }
}

