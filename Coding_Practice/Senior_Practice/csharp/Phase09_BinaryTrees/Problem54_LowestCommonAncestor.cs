namespace SeniorPractice.Phase09;

/// <summary>
/// Problem #54: Lowest Common Ancestor of a Binary Tree (LeetCode #236)
/// Invariant: Post-order bubbling — node returning non-null from both subtrees is LCA
/// </summary>
public static class Problem54_LowestCommonAncestor
{
    public static TreeNode? LowestCommonAncestor(TreeNode? root, TreeNode? p, TreeNode? q)
    {
        if (root == null || root == p || root == q) return root;

        var left = LowestCommonAncestor(root.left, p, q);
        var right = LowestCommonAncestor(root.right, p, q);

        if (left != null && right != null) return root;
        return left ?? right;
    }
}
