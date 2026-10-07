namespace SeniorPractice.Phase04;

/// <summary>
/// Problem #23: Range Sum Query - Immutable (LeetCode #303)
/// Invariant: prefix[i] = prefix[i-1] + nums[i-1]; SumRange(L, R) = prefix[R+1] - prefix[L]
/// </summary>
public class Problem23_NumArray
{
    private readonly int[] _prefix;

    public Problem23_NumArray(int[] nums)
    {
        ArgumentNullException.ThrowIfNull(nums);
        _prefix = new int[nums.Length + 1];
        for (int i = 0; i < nums.Length; i++)
        {
            _prefix[i + 1] = _prefix[i] + nums[i];
        }
    }

    public int SumRange(int left, int right)
    {
        return _prefix[right + 1] - _prefix[left];
    }
}
