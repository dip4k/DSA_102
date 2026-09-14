namespace SeniorPractice.Phase02;

/// <summary>
/// Problem #13: Trapping Rain Water (LeetCode #42)
/// Difficulty: Hard | Priority: Core / Flagship Anchor
/// Governing Invariant: Boundary Confinement — If leftMax < rightMax, water at left is leftMax - height[left].
/// </summary>
public static class Problem13_TrappingRainWater
{
    public static int Trap(int[] height)
    {
        if (height == null || height.Length < 3) return 0;

        int left = 0;
        int right = height.Length - 1;
        int leftMax = height[left];
        int rightMax = height[right];
        int totalWater = 0;

        while (left < right)
        {
            if (leftMax < rightMax)
            {
                left++;
                if (height[left] < leftMax)
                {
                    totalWater += leftMax - height[left];
                }
                else
                {
                    leftMax = height[left];
                }
            }
            else
            {
                right--;
                if (height[right] < rightMax)
                {
                    totalWater += rightMax - height[right];
                }
                else
                {
                    rightMax = height[right];
                }
            }
        }

        return totalWater;
    }
}

