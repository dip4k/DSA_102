namespace SeniorPractice.Phase07.Tests;

public class Problem37_MinStack_Tests
{
    [Fact]
    public void MinStack_ShouldMaintainMinCorrectly()
    {
        var minStack = new Problem37_MinStack();
        minStack.Push(-2);
        minStack.Push(0);
        minStack.Push(-3);
        Assert.Equal(-3, minStack.GetMin());
        minStack.Pop();
        Assert.Equal(0, minStack.Top());
        Assert.Equal(-2, minStack.GetMin());
    }
}
