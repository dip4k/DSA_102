namespace SeniorPractice.Phase07;

/// <summary>
/// Problem #37: Min Stack (LeetCode #155)
/// Invariant: Dual-stack maintaining values and prefix minimums
/// </summary>
public class Problem37_MinStack
{
    private readonly Stack<int> _stack = new();
    private readonly Stack<int> _minStack = new();

    public void Push(int val)
    {
        _stack.Push(val);
        int currentMin = _minStack.Count == 0 ? val : Math.Min(val, _minStack.Peek());
        _minStack.Push(currentMin);
    }

    public void Pop()
    {
        _stack.Pop();
        _minStack.Pop();
    }

    public int Top()
    {
        return _stack.Peek();
    }

    public int GetMin()
    {
        return _minStack.Peek();
    }
}
