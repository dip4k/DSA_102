namespace SeniorPractice.Phase09;

/// <summary>
/// Problem #51: Binary Tree Level Order Traversal (LeetCode #102)
/// Invariant: BFS queue capturing level snapshot size
/// </summary>
public static class Problem51_LevelOrderTraversal
{
    public static IList<IList<int>> LevelOrder(TreeNode? root)
    {
        var result = new List<IList<int>>();
        if (root == null) return result;

        var queue = new Queue<TreeNode>();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            int levelSize = queue.Count;
            var currentLevel = new List<int>(levelSize);

            for (int i = 0; i < levelSize; i++)
            {
                var node = queue.Dequeue();
                currentLevel.Add(node.val);

                if (node.left != null) queue.Enqueue(node.left);
                if (node.right != null) queue.Enqueue(node.right);
            }

            result.Add(currentLevel);
        }

        return result;
    }
}
