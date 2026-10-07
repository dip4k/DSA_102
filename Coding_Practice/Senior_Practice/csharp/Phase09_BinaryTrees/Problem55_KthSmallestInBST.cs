namespace SeniorPractice.Phase09;

/// <summary>
/// Problem #55: Kth Smallest Element in a BST (LeetCode #230)
/// Invariant: In-order traversal visits BST elements in strictly monotonic ascending order
/// </summary>
public static class Problem55_KthSmallestInBST
{
    public static int KthSmallest(TreeNode? root, int k)
    {
        var stack = new Stack<TreeNode>();
        var curr = root;

        while (curr != null || stack.Count > 0)
        {
            while (curr != null)
            {
                stack.Push(curr);
                curr = curr.left;
            }

            curr = stack.Pop();
            k--;
            if (k == 0) return curr.val;

            curr = curr.right;
        }

        return -1;
    }
}
