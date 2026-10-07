namespace SeniorPractice.Phase08;

/// <summary>
/// Problem #47: Search a 2D Matrix (LeetCode #74)
/// Invariant: Virtual flatten — row = mid / cols, col = mid % cols
/// </summary>
public static class Problem47_Search2DMatrix
{
    public static bool SearchMatrix(int[][] matrix, int target)
    {
        if (matrix == null || matrix.Length == 0 || matrix[0].Length == 0) return false;

        int rows = matrix.Length;
        int cols = matrix[0].Length;
        int left = 0, right = rows * cols - 1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            int midVal = matrix[mid / cols][mid % cols];

            if (midVal == target) return true;
            if (midVal < target) left = mid + 1;
            else right = mid - 1;
        }

        return false;
    }
}
