namespace SeniorPractice.Phase10;

/// <summary>
/// Problem #59: Find Median from Data Stream (LeetCode #295)
/// Invariant: Two-heap balance — maxHeap(lower) holds N/2 or N/2+1, minHeap(upper) holds N/2
/// </summary>
public class Problem59_MedianFinder
{
    // lowerHalf stores smaller elements; top is maximum of lower half
    private readonly PriorityQueue<int, int> _lowerHalf = new();
    // upperHalf stores larger elements; top is minimum of upper half
    private readonly PriorityQueue<int, int> _upperHalf = new();

    public void AddNum(int num)
    {
        // Enqueue to lower half first (inverted priority for max-heap behavior)
        _lowerHalf.Enqueue(num, -num);

        // Balance invariant: max of lower half <= min of upper half
        int maxLower = _lowerHalf.Dequeue();
        _upperHalf.Enqueue(maxLower, maxLower);

        // Size invariant: lowerHalf.Count >= upperHalf.Count
        if (_lowerHalf.Count < _upperHalf.Count)
        {
            int minUpper = _upperHalf.Dequeue();
            _lowerHalf.Enqueue(minUpper, -minUpper);
        }
    }

    public double FindMedian()
    {
        if (_lowerHalf.Count > _upperHalf.Count)
        {
            return _lowerHalf.Peek();
        }

        return (_lowerHalf.Peek() + (double)_upperHalf.Peek()) / 2.0;
    }
}
