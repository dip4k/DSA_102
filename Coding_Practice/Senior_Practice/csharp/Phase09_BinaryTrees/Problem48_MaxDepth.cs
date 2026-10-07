namespace SeniorPractice.Phase09;

/// <summary>
/// Problem #48: Maximum Depth of Binary Tree (LeetCode #104)
/// Invariant: depth = 1 + Math.Max(depth(left), depth(right))
/// </summary>
public static class Problem48_MaxDepth
{
    public static int MaxDepth(TreeNode? root)
    {
        if (root == null) return 0;
        return 1 + Math.Max(MaxDepth(root.left), MaxDepth(root.right));
    }
}
