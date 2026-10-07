namespace SeniorPractice.Phase07;

/// <summary>
/// Problem #40: Largest Rectangle in Histogram (LeetCode #84)
/// Invariant: Monotonic Increasing Stack of indices with pop calculating width = (i - stack.Peek() - 1)
/// </summary>
public static class Problem40_LargestRectangleInHistogram
{
    public static int LargestRectangleArea(int[] heights)
    {
        ArgumentNullException.ThrowIfNull(heights);
        int n = heights.Length;
        var stack = new Stack<int>();
        int maxArea = 0;

        for (int i = 0; i <= n; i++)
        {
            int currentHeight = (i == n) ? 0 : heights[i];

            while (stack.Count > 0 && currentHeight < heights[stack.Peek()])
            {
                int h = heights[stack.Pop()];
                int width = stack.Count == 0 ? i : i - stack.Peek() - 1;
                maxArea = Math.Max(maxArea, h * width);
            }

            stack.Push(i);
        }

        return maxArea;
    }
}
