namespace SeniorPractice.Phase09.Tests;

public class Problem49_SameTree_Tests
{
    [Fact]
    public void IsSameTree_ShouldCheckIsomorphism()
    {
        var p = TreeNode.FromLevelOrder(new int?[] { 1, 2, 3 });
        var q = TreeNode.FromLevelOrder(new int?[] { 1, 2, 3 });
        Assert.True(Problem49_SameTree.IsSameTree(p, q));

        var p2 = TreeNode.FromLevelOrder(new int?[] { 1, 2 });
        var q2 = TreeNode.FromLevelOrder(new int?[] { 1, null, 2 });
        Assert.False(Problem49_SameTree.IsSameTree(p2, q2));
    }
}
