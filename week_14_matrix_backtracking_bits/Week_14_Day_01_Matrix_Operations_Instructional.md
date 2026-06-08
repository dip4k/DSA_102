# 📅 Week 14, Day 1: Coordinate Transformations, Spiral Traversal & Matrix Operations

Welcome to Day 1. Today, we build a solid understanding of how coordinates are shuffled and traversed dynamically in multi-dimensional space, and how these transformations map to physical memory.

---

## 🎯 Learning Objectives
*   Master in-place transpose and 90-degree clockwise/counter-clockwise grid rotations.
*   Implement robust boundary-pointer spiral scans that eliminate index drift.
*   Understand the algebraic foundations of Matrix Chain Multiplication (DP) and fast Strassen multiplication.
*   Solve systems of equations using Gaussian elimination to find matrix determinants and inverses.

---

## 📘 Chapter 1: Context and Motivation

### 1. The Core Engineering Challenge
In standard computer systems, hardware memory is a flat, 1D sequential span of byte addresses. Multi-dimensional tables are a high-level language abstraction. To access a cell at coordinates `[row][col]`, compilers must perform address arithmetic:
{Address}(r, c) = {Base_Address} + (r x {Width} + c) x {Element_Size}
This layout means that accessing adjacent cells inside a `row` is extremely fast (thanks to contiguous sequential cache-line prefetching), whereas jumping vertically down adjacent values within a `column` triggers repetitive cache misses. 
When we perform complex rotations, transpositions, and traversals:
*   We risk triggering high constant-factor penalties because of poor cache access patterns.
*   We face indexing bugs or boundary tracking failures under pressure.

### 2. Naive Pitfalls
The standard way to rotate a grid is to allocate a secondary matrix of identical dimensions and project each item:
{destination}[c][N - 1 - r] = {source}[r][c]
While safe, this naive approach requires O(N^2) extra memory. For large matrices used in graphics pipelines, real-time image processing, and numerical physics simulations, this allocation causes heavy garbage collection stalls and excessive memory usage.

### 3. Real-world Anchor: Image Filters
In digital engines, a bitmap or raw image is a grid of color values. Performing high-frequency operations, like rotating a landscape photo to portrait or rendering frame buffers in graphics engines, requires optimizing these data movements to run directly in CPU caches with zero auxiliary allocations.

---

## 📘 Chapter 2: Mental Model

### 1. Loop-Free Geometric Reflections
Instead of tracking multi-variable coordinate projections, think of 2D grid operations as a series of simple reflection steps. Complex rotations can be decomposed into sequence compositions of two in-place swaps:
*   **A Diagonal Reflection (Transpose)**: Reflections over the primary diagonal (y = x) swap coordinates swap row and column values:
    M[i][j] \longleftrightarrow M[j][i]
*   **A Horizontal/Vertical Reflection (Flips)**: Reversing rows or columns mirrors the values across a central axis.

```text
  INPUT GRID              1. DIAGONAL TRANSPOSE        2. HORIZONTAL COLUMN FLIP
  [ A | B | C ]               [ A | D | G ]              [ G | D | A ]
  [ D | E | F ]     ===>      [ B | E | H ]     ===>     [ H | E | B ]
  [ G | H | I ]               [ C | F | I ]              [ I | F | C ]
```

### 2. The Concentric Castle Walls
To traverse a grid in spiral order, think of the matrix as a series of concentric rectangular rings. We can traverse these rings individually by tracking four boundaries: `top`, `bottom`, `left`, and `right`.
By advancing our pointers layer by layer, we shrink our search space safely inward until the boundaries cross.

#### 📊 Matrix Transformation Taxonomy
| Operation | Algebraic Index Mapping | Sequential Composition Method | Space Complexity | Best-use Case |
| :--- | :--- | :--- | :--- | :--- |
| **Transpose** | M[i][j] \longleftrightarrow M[j][i] | Symmetrical swap strictly above the diagonal (j > i) | O(1) | Symmetrical coordinate swap |
| **Rotate Clockwise 90°** | M[i][j] arrow M[j][N-1-i] | Transpose arrow Reverse Rows | O(1) | Rotate color channels / grids |
| **Rotate Counter-Clockwise 90°** | M[i][j] arrow M[N-1-j][i] | Reverse Rows arrow Transpose | O(1) | Counter-rotation algorithms |
| **Spiral Walk** | Layer/Boundary step tracing | Step-by-step bounds shrinkage (T, B, L, R) | O(1) | Ordered sequence scans |

---

## 📘 Chapter 3: Mechanics

### 1. Transpose and Rotation Mechanics
To transpose a matrix in-place, read and swap elements across the main diagonal. Only loop through cells where j > i to avoid swapping elements back to their original position.

```text
  Loop bounds: Only swap where j > i
  [ x | S | S ]    S : Cells swapped
  [ x | x | S ]    x : Cells preserved (on or below main diagonal)
  [ x | x | x ]
```

Once transposed, reversing each horizontal row individually rotates the entire square grid clockwise 90 degrees in-place.

```csharp
// Transpose an N x N matrix in-place
public static void Transpose(int[][] matrix) {
    int n = matrix.Length;
    for (int i = 0; i < n; i++) {
        for (int j = i + 1; j < n; j++) {
            int temp = matrix[i][j];
            matrix[i][j] = matrix[j][i];
            matrix[j][i] = temp;
        }
    }
}
```

---

### 2. Spiral Walk State Mechanics
Maintain four boundary pointers: `top` (row boundary), `bottom` (row boundary), `left` (column boundary), and `right` (column boundary). Traverse coordinates sequentially along these boundaries and shrink the coordinates inward. You must check that the boundary conditions are still valid before starting each horizontal or vertical traversal to prevent duplicate processing.

```csharp
public static List<int> SpiralOrder(int[][] matrix) {
    var result = new List<int>();
    if (matrix == null || matrix.Length == 0 || matrix[0].Length == 0) return result;
    
    int top = 0;
    int bottom = matrix.Length - 1;
    int left = 0;
    int right = matrix[0].Length - 1;
    
    while (top <= bottom && left <= right) {
        // Step 1: Traverse Left to Right across top boundary
        for (int i = left; i <= right; i++) {
            result.Add(matrix[top][i]);
        }
        top++; // Shrink top boundary
        
        // Step 2: Traverse Top to Bottom along right boundary
        for (int i = top; i <= bottom; i++) {
            result.Add(matrix[i][right]);
        }
        right--; // Shrink right boundary
        
        // Step 3: Traverse Right to Left along bottom boundary (if top and bottom haven't crossed)
        if (top <= bottom) {
            for (int i = right; i >= left; i--) {
                result.Add(matrix[bottom][i]);
            }
            bottom--; // Shrink bottom boundary
        }
        
        // Step 4: Traverse Bottom to Top along left boundary (if left and right haven't crossed)
        if (left <= right) {
            for (int i = bottom; i >= top; i--) {
                result.Add(matrix[i][left]);
            }
            left++; // Shrink left boundary
        }
    }
    return result;
}
```

---

### 3. Strassen & Optimization Algorithms
*   **Classic Matrix Multiplication**: Multiplies matrix A (P x Q) by matrix B (Q x R) to yield matrix C (P x R) in O(P * Q * R) operations using standard triple-nested loop iterations.
*   **Strassen's Algorithm**: Discovers that standard 2 x 2 matrix multiplication can be calculated using only 7 multiplications instead of 8 by applying algebraic subdivisions recursively. This reduces the asymptotic time complexity from O(N^3) to O(N^{log2(7)}) approx. O(N^{2.81}).
*   **Gaussian Elimination**: Converted a systems matrix to an Upper Triangular form by scaling rows and subtracting matching offsets sequentially. Determinants can then be calculated in O(N^3) time by multiplying the resulting diagonal pivot coordinates, while inverses are found using augmented identity arrays [M | I].

---

### 4. Code Walkthroughs (Staircase Search and Zero Markers)

#### A. C# Production-Grade Implementations
```csharp
public static class GridSolvers {
    /// <summary>
    /// Searches for target element in a row-and-column-sorted matrix.
    /// Time Complexity: O(R + C) | Space Complexity: O(1)
    /// </summary>
    public static bool StaircaseSearch(int[][] matrix, int target) {
        if (matrix == null || matrix.Length == 0 || matrix[0].Length == 0) return false;
        int row = 0;
        int col = matrix[0].Length - 1; // Start top-right

        while (row < matrix.Length && col >= 0) {
            int current = matrix[row][col];
            if (current == target) {
                return true;
            }
            if (current > target) {
                col--; // Sift left
            } else {
                row++; // Sift down
            }
        }
        return false;
    }

    /// <summary>
    /// Sets an entire row and column to zero in-place if an element is zero.
    /// Uses row-0 and col-0 as markers to achieve O(1) auxiliary space.
    /// Time Complexity: O(R * C) | Space Complexity: O(1)
    /// </summary>
    public static void SetZeroes(int[][] matrix) {
        if (matrix == null || matrix.Length == 0) return;
        int r = matrix.Length;
        int c = matrix[0].Length;
        bool isZeroCol = false;
        bool isZeroRow = false;

        // Determine if first column needs zeroing
        for (int i = 0; i < r; i++) {
            if (matrix[i][0] == 0) isZeroCol = true;
        }

        // Determine if first row needs zeroing
        for (int j = 0; j < c; j++) {
            if (matrix[0][j] == 0) isZeroRow = true;
        }

        // Use first row and column as markers for internal cells
        for (int i = 1; i < r; i++) {
            for (int j = 1; j < c; j++) {
                if (matrix[i][j] == 0) {
                    matrix[i][0] = 0;
                    matrix[0][j] = 0;
                }
            }
        }

        // Zero cells based on markers
        for (int i = 1; i < r; i++) {
            for (int j = 1; j < c; j++) {
                if (matrix[i][0] == 0 || matrix[0][j] == 0) {
                    matrix[i][j] = 0;
                }
            }
        }

        // Zero first row if marker was set
        if (isZeroRow) {
            for (int j = 0; j < c; j++) matrix[0][j] = 0;
        }

        // Zero first column if marker was set
        if (isZeroCol) {
            for (int i = 0; i < r; i++) matrix[i][0] = 0;
        }
    }
}
```

#### B. Python Clarity-First Implementations
```python
def staircase_search(matrix: list[list[int]], target: int) -> bool:
    """Searches a sorted row/col matrix for target in linear time.
    
    Time Complexity: O(R + C) | Space Complexity: O(1)
    """
    if not matrix or not matrix[0]:
        return False
    row, col = 0, len(matrix[0]) - 1 # Start top-right

    while row < len(matrix) and col >= 0:
        val = matrix[row][col]
        if val == target:
            return True
        if val > target:
            col -= 1 # Sift left
        else:
            row += 1 # Sift down
    return False


def set_zeroes(matrix: list[list[int]]) -> None:
    """Sets complete row/column to zero if any cell inside contains a zero.
    
    Achieves O(1) space complexity by using first row/col as markers.
    """
    if not matrix:
        return
    r, c = len(matrix), len(matrix[0])
    first_row_zero = any(matrix[0][j] == 0 for j in range(c))
    first_col_zero = any(matrix[i][0] == 0 for i in range(r))

    # Mark rows and cols with zero cells using first row/col as markers
    for i in range(1, r):
        for j in range(1, c):
            if matrix[i][j] == 0:
                matrix[i][0] = 0
                matrix[0][j] = 0

    # Zero cells based on markers
    for i in range(1, r):
        for j in range(1, c):
            if matrix[i][0] == 0 or matrix[0][j] == 0:
                matrix[i][j] = 0

    # Zero first row and col if appropriate
    if first_row_zero:
        for j in range(c):
            matrix[0][j] = 0
    if first_col_zero:
        for i in range(r):
            matrix[i][0] = 0
```

---

## 📘 Chapter 4: Performance and Systems

*   **Algorithmic Complexities**:
    *   Transpose: O(N^2) time, O(1) space.
    *   In-Place Rotation: O(N^2) time, O(1) space.
    *   Spiral traversal: O(R * C) time, O(1) space.
*   **CPU L1/L2 Cache Impact**: In C# and C/C++, matrices are stored in **Row-Major** layout (sequential columns inside a row are contiguous in physical memory). Looping over indices via `matrix[r][c]` (with the column index moving fastest in the innermost loop) matches cache prefetching lines. Iterating down columns vertically (`matrix[r][c]` with row index moving fastest) causes frequent cache misses, which can slow down execution by up to 10x on large grids.

---

## 📘 Chapter 5: Integration and Mastery

### 1. Advanced Grid DP: Dungeon Game & Cherry Pickup
*   **Dungeon Game (Backward Grid DP)**:
    *   *The Problem*: Find the minimum health needed at the start to survive a grid path where cells can contain power-ups or traps.
    *   *The Trick*: Moving forward is impossible because the health needed at any cell depends on the path ahead. We must solve this using **Backward DP**, starting from the bottom-right corner and calculating the minimum health needed to survive the step:
        DP[i][j] = max(1, min(DP[i+1][j], DP[i][j+1]) - M[i][j])
*   **Cherry Pickup (Dual Lockstep DP)**:
    *   *The Problem*: Maximize the cherries collected on a round-trip from top-left to bottom-right and back.
    *   *The Trick*: Instead of simulating two separate paths sequentially, simulate two players moving simultaneously from the top-left corner in lockstep. Since both paths complete in the same number of steps (r_1 + c_1 = r_2 + c_2 = t), we define our state as DP[t][r1][r2] and solve the overlapping paths in polynomial time.

### 2. Pattern Selection Rules
*   *Use Transpose + Row Reverse when*: You need to rotate a square grid 90 degrees with zero allocation constraints.
*   *Use Four boundary Pointers when*: You need to traverse a matrix spirally or step-by-step along concentric boundaries.

### 3. Follow-Up Variants
*   **Rotate Image II**: Rotate a rectangular matrix. (Requires transposing a rectangular grid, which cannot be done in-place easily, making row transformations necessary).
*   **Diagonal Traverse**: Traverse a matrix diagonally. Track the current diagonal index (`row + col == diagonal_index`) and reverse directions dynamically on alternating cycles.

---

## 🛠️ Supplementary Material

### Practice Problems
1.  **Rotate Image** ([LeetCode 48](https://leetcode.com/problems/rotate-image/)): Rotate a square matrix 90 degrees clockwise in-place.
2.  **Spiral Matrix** ([LeetCode 54](https://leetcode.com/problems/spiral-matrix/)): Return elements of an M x N matrix in spiral order.
3.  **Set Matrix Zeroes** ([LeetCode 73](https://leetcode.com/problems/set-matrix-zeroes/)): Set entire rows/cols to zero using first row/col as markers in-place.
4.  **Dungeon Game** ([LeetCode 174](https://leetcode.com/problems/dungeon-game/)): Find survival path thresholds using backward matrix DP.
5.  **Cherry Pickup** ([LeetCode 741](https://leetcode.com/problems/cherry-pickup/)): Maximize paths in lockstep using combined index-grid states in 3D DP.

### Misconceptions and Corrections
*   *Incorrect Idea*: Assuming you can transpose a rectangular matrix in-place inside an active rectangular grid boundaries.
    *   *Correction*: Transposing a rectangular matrix changed our boundary sizes (e.g. 3 x 4 arrow 4 x 3), which alters coordinate positions in memory. Doing this in-place requires complex cyclic-shift swaps or auxiliary buffers.
