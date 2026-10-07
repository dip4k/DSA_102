namespace SeniorPractice.Phase10;

/// <summary>
/// Problem #57: Top K Frequent Elements (LeetCode #347)
/// Invariant: Frequency map + Min-Heap of size k prioritized by frequency
/// </summary>
public static class Problem57_TopKFrequentElements
{
    public static int[] TopKFrequent(int[] nums, int k)
    {
        ArgumentNullException.ThrowIfNull(nums);
        var counts = new Dictionary<int, int>();
        foreach (int num in nums)
        {
            counts[num] = counts.GetValueOrDefault(num, 0) + 1;
        }

        var minHeap = new PriorityQueue<int, int>();
        foreach (var (num, freq) in counts)
        {
            minHeap.Enqueue(num, freq);
            if (minHeap.Count > k)
            {
                minHeap.Dequeue();
            }
        }

        int[] result = new int[k];
        for (int i = 0; i < k; i++)
        {
            result[i] = minHeap.Dequeue();
        }

        return result;
    }
}
