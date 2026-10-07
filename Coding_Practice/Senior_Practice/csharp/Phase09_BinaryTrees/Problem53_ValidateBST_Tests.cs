namespace SeniorPractice.Phase09.Tests;

public class Problem53_ValidateBST_Tests
{
    [Fact]
    public void IsValidBST_ShouldValidateCorrectly()
    {
        var valid = TreeNode.FromLevelOrder(new int?[] { 2, 1, 3 });
        Assert.True(Problem53_ValidateBST.IsValidBST(valid));

        var invalid = TreeNode.FromLevelOrder(new int?[] { 5, 1, 4, null, null, 3, 6 });
        Assert.False(Problem53_ValidateBST.IsValidBST(invalid));
    }
}
