namespace SeniorPractice.Phase09.Tests;

public class Problem54_LowestCommonAncestor_Tests
{
    [Fact]
    public void LowestCommonAncestor_ShouldFindLCA()
    {
        var root = new TreeNode(3);
        var n5 = new TreeNode(5);
        var n1 = new TreeNode(1);
        var n6 = new TreeNode(6);
        var n2 = new TreeNode(2);
        var n7 = new TreeNode(7);
        var n4 = new TreeNode(4);
        var n0 = new TreeNode(0);
        var n8 = new TreeNode(8);

        root.left = n5;
        root.right = n1;
        n5.left = n6;
        n5.right = n2;
        n2.left = n7;
        n2.right = n4;
        n1.left = n0;
        n1.right = n8;

        var lca1 = Problem54_LowestCommonAncestor.LowestCommonAncestor(root, n5, n1);
        Assert.Equal(3, lca1?.val);

        var lca2 = Problem54_LowestCommonAncestor.LowestCommonAncestor(root, n5, n4);
        Assert.Equal(5, lca2?.val);
    }
}
