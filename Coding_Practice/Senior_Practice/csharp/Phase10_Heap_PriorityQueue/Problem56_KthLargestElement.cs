namespace SeniorPractice.Phase10;

/// <summary>
/// Problem #56: Kth Largest Element in an Array (LeetCode #215)
/// Invariant: Min-Heap of size k retains the k largest elements; root is kth largest
/// </summary>
public static class Problem56_KthLargestElement
{
    public static int FindKthLargest(int[] nums, int k)
    {
        ArgumentNullException.ThrowIfNull(nums);
        var minHeap = new PriorityQueue<int, int>();

        foreach (int num in nums)
        {
            minHeap.Enqueue(num, num);
            if (minHeap.Count > k)
            {
                minHeap.Dequeue();
            }
        }

        return minHeap.Peek();
    }
}
