namespace SeniorPractice.Phase09;

/// <summary>
/// Problem #49: Same Tree (LeetCode #100)
/// Invariant: Simultaneous structural and value isomorphism
/// </summary>
public static class Problem49_SameTree
{
    public static bool IsSameTree(TreeNode? p, TreeNode? q)
    {
        if (p == null && q == null) return true;
        if (p == null || q == null || p.val != q.val) return false;
        return IsSameTree(p.left, q.left) && IsSameTree(p.right, q.right);
    }
}
