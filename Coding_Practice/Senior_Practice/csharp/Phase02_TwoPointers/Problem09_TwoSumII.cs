namespace SeniorPractice.Phase02;

/// <summary>
/// Problem #9: Two Sum II — Input Array Is Sorted (LeetCode #167)
/// Difficulty: Medium | Priority: Core
/// Governing Invariant: Monotonic Search-Space Pruning — sum > target => right--; sum < target => left++.
/// </summary>
public static class Problem09_TwoSumII
{
    public static int[] TwoSum(int[] numbers, int target)
    {
        ArgumentNullException.ThrowIfNull(numbers);
        if (numbers.Length < 2) throw new ArgumentException("Array must contain at least 2 elements.", nameof(numbers));

        int left = 0;
        int right = numbers.Length - 1;

        while (left < right)
        {
            long sum = (long)numbers[left] + numbers[right];

            if (sum == target)
            {
                return [left + 1, right + 1]; // 1-based index contract
            }
            else if (sum < target)
            {
                left++;
            }
            else
            {
                right--;
            }
        }

        return [];
    }
}

