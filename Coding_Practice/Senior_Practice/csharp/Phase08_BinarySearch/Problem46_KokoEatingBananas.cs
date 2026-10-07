namespace SeniorPractice.Phase08;

/// <summary>
/// Problem #46: Koko Eating Bananas (LeetCode #875)
/// Invariant: Binary search on answer space [1, max(piles)] with monotonic feasibility check
/// </summary>
public static class Problem46_KokoEatingBananas
{
    public static int MinEatingSpeed(int[] piles, int h)
    {
        ArgumentNullException.ThrowIfNull(piles);
        int left = 1;
        int right = 0;
        foreach (int p in piles) if (p > right) right = p;

        int ans = right;
        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            if (CanFinish(piles, h, mid))
            {
                ans = mid;
                right = mid - 1; // try smaller speed
            }
            else
            {
                left = mid + 1;  // must eat faster
            }
        }

        return ans;
    }

    private static bool CanFinish(int[] piles, int h, int speed)
    {
        long hours = 0;
        foreach (int p in piles)
        {
            hours += (p + speed - 1) / speed;
            if (hours > h) return false;
        }
        return hours <= h;
    }
}
