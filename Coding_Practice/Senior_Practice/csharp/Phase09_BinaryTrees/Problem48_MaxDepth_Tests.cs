namespace SeniorPractice.Phase09.Tests;

public class Problem48_MaxDepth_Tests
{
    [Fact]
    public void MaxDepth_ShouldComputeHeight()
    {
        var root = TreeNode.FromLevelOrder(new int?[] { 3, 9, 20, null, null, 15, 7 });
        Assert.Equal(3, Problem48_MaxDepth.MaxDepth(root));

        var root2 = TreeNode.FromLevelOrder(new int?[] { 1, null, 2 });
        Assert.Equal(2, Problem48_MaxDepth.MaxDepth(root2));

        Assert.Equal(0, Problem48_MaxDepth.MaxDepth(null));
    }
}
