namespace SeniorPractice.Phase09.Tests;

public class Problem52_DiameterOfBinaryTree_Tests
{
    [Fact]
    public void DiameterOfBinaryTree_ShouldReturnMaxPathEdges()
    {
        var root = TreeNode.FromLevelOrder(new int?[] { 1, 2, 3, 4, 5 });
        Assert.Equal(3, Problem52_DiameterOfBinaryTree.DiameterOfBinaryTree(root));

        var root2 = TreeNode.FromLevelOrder(new int?[] { 1, 2 });
        Assert.Equal(1, Problem52_DiameterOfBinaryTree.DiameterOfBinaryTree(root2));
    }
}
