namespace SeniorPractice.Phase02;

/// <summary>
/// Problem #10: Container With Most Water (LeetCode #11)
/// Difficulty: Medium | Priority: Core
/// Governing Invariant: Boundary Elimination — Area is bottlenecked by min(h[L], h[R]). 
/// Only advancing the shorter wall can discover a larger area.
/// </summary>
public static class Problem10_ContainerWithMostWater
{
    public static int MaxArea(int[] height)
    {
        ArgumentNullException.ThrowIfNull(height);

        int left = 0;
        int right = height.Length - 1;
        int maxWater = 0;

        while (left < right)
        {
            int width = right - left;
            int hLeft = height[left];
            int hRight = height[right];

            int currentArea = (hLeft < hRight ? hLeft : hRight) * width;
            if (currentArea > maxWater)
            {
                maxWater = currentArea;
            }

            if (hLeft < hRight)
            {
                left++;
            }
            else
            {
                right--;
            }
        }

        return maxWater;
    }
}

