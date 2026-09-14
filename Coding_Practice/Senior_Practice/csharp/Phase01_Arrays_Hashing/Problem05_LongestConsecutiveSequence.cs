namespace SeniorPractice.Phase01;

/// <summary>
/// Problem #5: Longest Consecutive Sequence (LeetCode #128)
/// Difficulty: Medium | Priority: Core
/// Governing Invariant: Left Boundary Invariant — x is streak anchor <=> x - 1 not in Set.
/// </summary>
public static class Problem05_LongestConsecutiveSequence
{
    public static int LongestConsecutive(int[] nums)
    {
        if (nums == null || nums.Length == 0) return 0;

        var numSet = new HashSet<int>(nums);
        int longestStreak = 0;

        foreach (int num in numSet)
        {
            if (!numSet.Contains(num - 1))
            {
                int currentNum = num;
                int currentStreak = 1;

                while (numSet.Contains(currentNum + 1))
                {
                    currentNum++;
                    currentStreak++;
                }

                longestStreak = Math.Max(longestStreak, currentStreak);
            }
        }

        return longestStreak;
    }
}

