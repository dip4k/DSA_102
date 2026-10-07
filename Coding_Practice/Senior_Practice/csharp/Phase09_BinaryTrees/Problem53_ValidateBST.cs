namespace SeniorPractice.Phase09;

/// <summary>
/// Problem #53: Validate Binary Search Tree (LeetCode #98)
/// Invariant: Each node value strictly falls within (minBound, maxBound)
/// </summary>
public static class Problem53_ValidateBST
{
    public static bool IsValidBST(TreeNode? root)
    {
        return Validate(root, null, null);
    }

    private static bool Validate(TreeNode? node, long? min, long? max)
    {
        if (node == null) return true;
        if (min.HasValue && node.val <= min.Value) return false;
        if (max.HasValue && node.val >= max.Value) return false;

        return Validate(node.left, min, node.val) && Validate(node.right, node.val, max);
    }
}
