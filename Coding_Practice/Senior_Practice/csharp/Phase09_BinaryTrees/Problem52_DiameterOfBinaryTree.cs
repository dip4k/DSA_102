namespace SeniorPractice.Phase09;

/// <summary>
/// Problem #52: Diameter of Binary Tree (LeetCode #543)
/// Invariant: Subtree returns depth; concurrent global diameter = leftDepth + rightDepth
/// </summary>
public static class Problem52_DiameterOfBinaryTree
{
    public static int DiameterOfBinaryTree(TreeNode? root)
    {
        int maxDiameter = 0;
        GetDepth(root, ref maxDiameter);
        return maxDiameter;
    }

    private static int GetDepth(TreeNode? node, ref int maxDiameter)
    {
        if (node == null) return 0;
        int left = GetDepth(node.left, ref maxDiameter);
        int right = GetDepth(node.right, ref maxDiameter);

        maxDiameter = Math.Max(maxDiameter, left + right);
        return 1 + Math.Max(left, right);
    }
}
