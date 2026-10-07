namespace SeniorPractice.Phase09.Tests;

public class Problem51_LevelOrderTraversal_Tests
{
    [Fact]
    public void LevelOrder_ShouldReturnLevels()
    {
        var root = TreeNode.FromLevelOrder(new int?[] { 3, 9, 20, null, null, 15, 7 });
        var res = Problem51_LevelOrderTraversal.LevelOrder(root);
        Assert.Equal(3, res.Count);
        Assert.Equal(new[] { 3 }, res[0]);
        Assert.Equal(new[] { 9, 20 }, res[1]);
        Assert.Equal(new[] { 15, 7 }, res[2]);
    }
}
