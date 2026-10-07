namespace SeniorPractice.Phase09.Tests;

public class Problem50_InvertBinaryTree_Tests
{
    [Fact]
    public void InvertTree_ShouldSwapSubtrees()
    {
        var root = TreeNode.FromLevelOrder(new int?[] { 4, 2, 7, 1, 3, 6, 9 });
        var inverted = Problem50_InvertBinaryTree.InvertTree(root);
        Assert.Equal(4, inverted?.val);
        Assert.Equal(7, inverted?.left?.val);
        Assert.Equal(2, inverted?.right?.val);
    }
}
