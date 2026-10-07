namespace SeniorPractice.Phase07;

/// <summary>
/// Problem #39: Daily Temperatures (LeetCode #739)
/// Invariant: Monotonic Decreasing Stack storing indices
/// </summary>
public static class Problem39_DailyTemperatures
{
    public static int[] DailyTemperatures(int[] temperatures)
    {
        ArgumentNullException.ThrowIfNull(temperatures);
        int n = temperatures.Length;
        int[] result = new int[n];
        var stack = new Stack<int>(); // stores indices

        for (int i = 0; i < n; i++)
        {
            while (stack.Count > 0 && temperatures[i] > temperatures[stack.Peek()])
            {
                int prevIndex = stack.Pop();
                result[prevIndex] = i - prevIndex;
            }
            stack.Push(i);
        }

        return result;
    }
}
