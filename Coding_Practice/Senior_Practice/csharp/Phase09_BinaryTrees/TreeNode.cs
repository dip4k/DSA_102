namespace SeniorPractice.Phase09;

public class TreeNode
{
    public int val;
    public TreeNode? left;
    public TreeNode? right;

    public TreeNode(int val = 0, TreeNode? left = null, TreeNode? right = null)
    {
        this.val = val;
        this.left = left;
        this.right = right;
    }

    public static TreeNode? FromLevelOrder(int?[] values)
    {
        if (values == null || values.Length == 0 || values[0] == null) return null;

        var root = new TreeNode(values[0]!.Value);
        var queue = new Queue<TreeNode>();
        queue.Enqueue(root);
        int i = 1;

        while (queue.Count > 0 && i < values.Length)
        {
            var curr = queue.Dequeue();

            if (i < values.Length && values[i] != null)
            {
                curr.left = new TreeNode(values[i]!.Value);
                queue.Enqueue(curr.left);
            }
            i++;

            if (i < values.Length && values[i] != null)
            {
                curr.right = new TreeNode(values[i]!.Value);
                queue.Enqueue(curr.right);
            }
            i++;
        }

        return root;
    }
}
