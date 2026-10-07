namespace SeniorPractice.Phase09;

/// <summary>
/// Problem #50: Invert Binary Tree (LeetCode #226)
/// Invariant: Pointer swap across all subtrees
/// </summary>
public static class Problem50_InvertBinaryTree
{
    public static TreeNode? InvertTree(TreeNode? root)
    {
        if (root == null) return null;
        var left = InvertTree(root.left);
        var right = InvertTree(root.right);
        root.left = right;
        root.right = left;
        return root;
    }
}
