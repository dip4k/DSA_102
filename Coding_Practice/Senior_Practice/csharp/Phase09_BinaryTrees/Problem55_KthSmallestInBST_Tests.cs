namespace SeniorPractice.Phase09.Tests;

public class Problem55_KthSmallestInBST_Tests
{
    [Fact]
    public void KthSmallest_ShouldReturnKthElement()
    {
        var root = TreeNode.FromLevelOrder(new int?[] { 3, 1, 4, null, 2 });
        Assert.Equal(1, Problem55_KthSmallestInBST.KthSmallest(root, 1));

        var root2 = TreeNode.FromLevelOrder(new int?[] { 5, 3, 6, 2, 4, null, null, 1 });
        Assert.Equal(3, Problem55_KthSmallestInBST.KthSmallest(root2, 3));
    }
}
