namespace SeniorPractice.Phase10;

/// <summary>
/// Problem #58: K Closest Points to Origin (LeetCode #973)
/// Invariant: Max-Heap of size k keeping smallest squared Euclidean distances
/// </summary>
public static class Problem58_KClosestPoints
{
    public static int[][] KClosest(int[][] points, int k)
    {
        ArgumentNullException.ThrowIfNull(points);
        // Max-heap: invert priority with negative distance
        var maxHeap = new PriorityQueue<int[], int>();

        foreach (var pt in points)
        {
            int distSq = pt[0] * pt[0] + pt[1] * pt[1];
            maxHeap.Enqueue(pt, -distSq);

            if (maxHeap.Count > k)
            {
                maxHeap.Dequeue();
            }
        }

        int[][] result = new int[k][];
        for (int i = 0; i < k; i++)
        {
            result[i] = maxHeap.Dequeue();
        }

        return result;
    }
}
