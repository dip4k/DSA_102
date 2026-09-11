# Matrix Traversal Mastery: The Complete Problem-Solving Curriculum

This curriculum teaches matrix traversal from first principles — linear scans, boundary-driven spirals, and graph-based grid search. For each level, understand the traversal order, boundary/state management, and the invariant that keeps your pointers valid.

## Summary Table

| Level | Mental Model | Pointer / State | Drill Problems |
|-------|--------------|-----------------|----------------|
| **1: Linear Scans** | The matrix is a flat structure accessed via index arithmetic. Row-major, column-major, and diagonal scans are just different iteration orders over `(r, c)`. | Single `(r, c)` pair; outer/inner loop order determines scan direction. | [LeetCode 2022: Convert 1D Array Into 2D Array] (Easy), [LeetCode 1572: Matrix Diagonal Sum] (Easy), [LeetCode 498: Diagonal Traverse] (Medium) |
| **2: Spiral & Rotation** | The matrix is an onion of concentric rectangular layers. Peel one layer at a time by walking its four edges, then shrink inward. | Four boundaries: `top`, `bottom`, `left`, `right`. Shrink after each edge walk. | [LeetCode 54: Spiral Matrix] (Medium), [LeetCode 48: Rotate Image] (Medium), [LeetCode 566: Reshape the Matrix] (Easy) |
| **3: Grid BFS/DFS** | The matrix is an implicit graph. Each cell `(r, c)` is a node; its 4 (or 8) neighbors are edges. Traverse with BFS/DFS respecting visited state. | Queue (BFS) or call stack (DFS) + `visited` set or in-place marking. | [LeetCode 733: Flood Fill] (Easy), [LeetCode 200: Number of Islands] (Medium), [LeetCode 1091: Shortest Path in Binary Matrix] (Medium) |

---

## Level 1: Linear Scans (Row-Major, Column-Major, Diagonal)

Matrices are 2D arrays. Every traversal is just choosing an order for `(row, col)` iteration. Master the three canonical orders before touching anything complex.

**Mental Models & Invariants:**
- **What does the state `(r, c)` mean?** Current cell being read/written.
- **What region is processed?** All cells visited by the chosen loop order, exactly once.
- **What is the invariant?** `0 <= r < rows` and `0 <= c < cols` at every access. The scan visits every cell exactly once.

**Index Mapping Formulas:**

| Scan Type | Outer Loop | Inner Loop | Access Pattern |
|-----------|-----------|------------|----------------|
| **Row-major** | `r: 0 → rows-1` | `c: 0 → cols-1` | Left-to-right, top-to-bottom |
| **Column-major** | `c: 0 → cols-1` | `r: 0 → rows-1` | Top-to-bottom, left-to-right |
| **Primary diagonal** | `d: 0 → min(rows,cols)-1` | — | `(d, d)` cells |
| **Anti-diagonal** | `d: 0 → min(rows,cols)-1` | — | `(d, cols-1-d)` cells |
| **1D ↔ 2D** | `idx: 0 → rows*cols-1` | — | `r = idx // cols`, `c = idx % cols` |

**Row-Major Traversal Trace (3×3 matrix `[[1,2,3],[4,5,6],[7,8,9]]`):**

| Step | `r` | `c` | Cell Value | Flat Index (`r*cols+c`) |
|------|-----|-----|------------|-------------------------|
| 0 | 0 | 0 | 1 | 0 |
| 1 | 0 | 1 | 2 | 1 |
| 2 | 0 | 2 | 3 | 2 |
| 3 | 1 | 0 | 4 | 3 |
| 4 | 1 | 1 | 5 | 4 |
| 5 | 1 | 2 | 6 | 5 |
| 6 | 2 | 0 | 7 | 6 |
| 7 | 2 | 1 | 8 | 7 |
| 8 | 2 | 2 | 9 | 8 |

**Diagonal Traverse Trace (3×3 matrix, alternating direction):**

| Step | Diagonal `d` | Direction | Cells Visited |
|------|-------------|-----------|---------------|
| 0 | 0 | ↗ Up-right | `(0,0)` → val 1 |
| 1 | 1 | ↙ Down-left | `(0,1)→(1,0)` → vals 2, 4 |
| 2 | 2 | ↗ Up-right | `(2,0)→(1,1)→(0,2)` → vals 7, 5, 3 |
| 3 | 3 | ↙ Down-left | `(1,2)→(2,1)` → vals 6, 8 |
| 4 | 4 | ↗ Up-right | `(2,2)` → val 9 |

### ⚠️ Gotchas & Pitfalls
- **Row/Column confusion**: Mixing up `matrix[row][col]` vs `matrix[col][row]`. Row is the outer index, column is the inner index.
- **1D ↔ 2D off-by-one**: When converting flat index `idx` back to 2D, always use `cols` (not `rows`) as the divisor: `r = idx // cols`, `c = idx % cols`.
- **Non-square matrices**: Assuming `rows == cols`. Diagonal traversals on rectangular matrices require clamping start/end of each diagonal to valid bounds.

**Code Snippets:**

Problem: Convert a 1D array `original` into a 2D array with `m` rows and `n` columns using index arithmetic.
```python
# Python: Convert 1D Array Into 2D Array [LeetCode 2022]
def construct2DArray(original: list[int], m: int, n: int) -> list[list[int]]:
    # 1. Guard: total elements must exactly fill m x n
    if len(original) != m * n:
        return []
    result = []
    # 2. Slice the flat array into rows of length n
    for r in range(m):
        # 3. Each row starts at index r*n and spans n elements
        result.append(original[r * n : r * n + n])
    return result
```

Problem: Return the sum of the primary diagonal and the secondary diagonal of a square matrix, excluding the center if the size is odd.
```csharp
// C#: Matrix Diagonal Sum [LeetCode 1572]
public int DiagonalSum(int[][] mat) {
    int n = mat.Length;
    int sum = 0;
    for (int i = 0; i < n; i++) {
        // 1. Add primary diagonal element (top-left to bottom-right)
        sum += mat[i][i];
        // 2. Add anti-diagonal element (top-right to bottom-left)
        sum += mat[i][n - 1 - i];
    }
    // 3. If n is odd, the center cell was counted twice — subtract it once
    if (n % 2 == 1)
        sum -= mat[n / 2][n / 2];
    return sum;
}
```

Problem: Traverse a matrix in diagonal zigzag order, alternating direction on each diagonal.
```python
# Python: Diagonal Traverse [LeetCode 498]
def findDiagonalOrder(mat: list[list[int]]) -> list[int]:
    if not mat or not mat[0]:
        return []
    rows, cols = len(mat), len(mat[0])
    result = []
    # 1. Total number of diagonals = rows + cols - 1
    for d in range(rows + cols - 1):
        if d % 2 == 0:
            # 2. Even diagonal: traverse upward (↗). Start row is min(d, rows-1)
            r = min(d, rows - 1)
            c = d - r
            while r >= 0 and c < cols:
                result.append(mat[r][c])
                r -= 1  # move up
                c += 1  # move right
        else:
            # 3. Odd diagonal: traverse downward (↙). Start col is min(d, cols-1)
            c = min(d, cols - 1)
            r = d - c
            while c >= 0 and r < rows:
                result.append(mat[r][c])
                r += 1  # move down
                c -= 1  # move left
    return result
```

Problem: Traverse a matrix in diagonal zigzag order, alternating direction on each diagonal.
```csharp
// C#: Diagonal Traverse [LeetCode 498]
public int[] FindDiagonalOrder(int[][] mat) {
    int rows = mat.Length, cols = mat[0].Length;
    int[] result = new int[rows * cols];
    int idx = 0;
    // 1. Iterate over each diagonal (rows + cols - 1 total)
    for (int d = 0; d < rows + cols - 1; d++) {
        if (d % 2 == 0) {
            // 2. Even diagonal: go up-right
            int r = Math.Min(d, rows - 1);
            int c = d - r;
            while (r >= 0 && c < cols)
                result[idx++] = mat[r--][c++];
        } else {
            // 3. Odd diagonal: go down-left
            int c = Math.Min(d, cols - 1);
            int r = d - c;
            while (c >= 0 && r < rows)
                result[idx++] = mat[r++][c--];
        }
    }
    return result;
}
```

**Drill Problems:**
- **Easy**: [LeetCode 2022: Convert 1D Array Into 2D Array] — Pure index arithmetic: `r = idx // n`, `c = idx % n`.
- **Easy**: [LeetCode 1572: Matrix Diagonal Sum] — Primary `(i,i)` + anti `(i,n-1-i)`, subtract center if odd.
- **Medium**: [LeetCode 498: Diagonal Traverse] — Zigzag across `rows+cols-1` diagonals, flip direction each time.

---

## Level 2: Spiral & Rotation (Boundary Peeling)

The matrix is layers of concentric rectangles. Walk the four edges of the outermost layer, then shrink the boundary inward. Rotation is in-place layer-by-layer swapping.

**Mental Models & Invariants:**
- **What does the state mean?** Four boundaries `top`, `bottom`, `left`, `right` define the current unprocessed rectangular ring.
- **What region is processed?** Cells on the current ring's edges. Inner cells are untouched until boundaries shrink.
- **What is the invariant?** `top <= bottom` and `left <= right`. When violated, all layers are consumed.

**Spiral Order State Trace (3×3 matrix `[[1,2,3],[4,5,6],[7,8,9]]`):**

| Step | Edge | Boundary State (T,B,L,R) | Cells Read | Action |
|------|------|--------------------------|------------|--------|
| 1 | Top → | `(0, 2, 0, 2)` | 1, 2, 3 | Walk `col: L→R` on row `T`. Then `T++` → `(1, 2, 0, 2)` |
| 2 | Right ↓ | `(1, 2, 0, 2)` | 6, 9 | Walk `row: T→B` on col `R`. Then `R--` → `(1, 2, 0, 1)` |
| 3 | Bottom ← | `(1, 2, 0, 1)` | 8, 7 | Walk `col: R→L` on row `B`. Then `B--` → `(1, 1, 0, 1)` |
| 4 | Left ↑ | `(1, 1, 0, 1)` | 4 | Walk `row: B→T` on col `L`. Then `L++` → `(1, 1, 1, 1)` |
| 5 | Top → | `(1, 1, 1, 1)` | 5 | Walk `col: L→R` on row `T`. Then `T++` → `(2, 1, 1, 1)` |
| 6 | — | `T > B` | — | **Stop**. Boundary violated. |

**Result**: `[1, 2, 3, 6, 9, 8, 7, 4, 5]`

**Rotation (90° Clockwise) Layer Trace (4×4 outer ring):**

| Swap Set | Positions Involved | Rule |
|----------|--------------------|------|
| `i=0` | `(0,0)→(0,3)→(3,3)→(3,0)→(0,0)` | Four-way cyclic swap of corners |
| `i=1` | `(0,1)→(1,3)→(3,2)→(2,0)→(0,1)` | Four-way cyclic swap, offset by 1 |
| `i=2` | `(0,2)→(2,3)→(3,1)→(1,0)→(0,2)` | Four-way cyclic swap, offset by 2 |

**Formula**: For layer offset `l`, position `i` within layer:
- `top-left (l, l+i)` → `top-right (l+i, n-1-l)` → `bottom-right (n-1-l, n-1-l-i)` → `bottom-left (n-1-l-i, l)` → back.

### ⚠️ Gotchas & Pitfalls
- **Single row/column residual**: After peeling the top edge and right edge, you must check `top <= bottom` before walking the bottom edge (and `left <= right` before walking the left edge). Without this guard, single-row or single-column matrices double-count elements.
- **Rotation direction confusion**: 90° clockwise: `(r,c) → (c, n-1-r)`. 90° counter-clockwise: `(r,c) → (n-1-c, r)`. Mixing these up silently produces wrong output.
- **Off-by-one in layer count**: For an `n×n` matrix, there are `n/2` layers (integer division). The center cell of an odd-sized matrix is its own trivial layer and needs no swap.

**Code Snippets:**

Problem: Return all elements of an `m×n` matrix in spiral order by peeling layers from outside in.
```python
# Python: Spiral Matrix [LeetCode 54]
def spiralOrder(matrix: list[list[int]]) -> list[int]:
    result = []
    # 1. Initialize boundaries of the unprocessed region
    top, bottom = 0, len(matrix) - 1
    left, right = 0, len(matrix[0]) - 1

    while top <= bottom and left <= right:
        # 2. Walk TOP edge: left → right
        for c in range(left, right + 1):
            result.append(matrix[top][c])
        top += 1  # shrink top boundary inward

        # 3. Walk RIGHT edge: top → bottom
        for r in range(top, bottom + 1):
            result.append(matrix[r][right])
        right -= 1  # shrink right boundary inward

        # 4. Walk BOTTOM edge: right → left (guard against single-row)
        if top <= bottom:
            for c in range(right, left - 1, -1):
                result.append(matrix[bottom][c])
            bottom -= 1

        # 5. Walk LEFT edge: bottom → top (guard against single-column)
        if left <= right:
            for r in range(bottom, top - 1, -1):
                result.append(matrix[r][left])
            left += 1

    return result
```

Problem: Return all elements of an `m×n` matrix in spiral order by peeling layers from outside in.
```csharp
// C#: Spiral Matrix [LeetCode 54]
public IList<int> SpiralOrder(int[][] matrix) {
    var result = new List<int>();
    int top = 0, bottom = matrix.Length - 1;
    int left = 0, right = matrix[0].Length - 1;

    while (top <= bottom && left <= right) {
        // 1. Top edge: left → right
        for (int c = left; c <= right; c++)
            result.Add(matrix[top][c]);
        top++;

        // 2. Right edge: top → bottom
        for (int r = top; r <= bottom; r++)
            result.Add(matrix[r][right]);
        right--;

        // 3. Bottom edge: right → left (only if rows remain)
        if (top <= bottom) {
            for (int c = right; c >= left; c--)
                result.Add(matrix[bottom][c]);
            bottom--;
        }

        // 4. Left edge: bottom → top (only if columns remain)
        if (left <= right) {
            for (int r = bottom; r >= top; r--)
                result.Add(matrix[r][left]);
            left++;
        }
    }
    return result;
}
```

Problem: Rotate an `n×n` matrix 90° clockwise in-place by performing four-way cyclic swaps layer by layer.
```python
# Python: Rotate Image [LeetCode 48]
def rotate(matrix: list[list[int]]) -> None:
    n = len(matrix)
    # 1. Process each concentric layer from outside inward
    for layer in range(n // 2):
        first, last = layer, n - 1 - layer
        for i in range(first, last):
            offset = i - first
            # 2. Save top-left
            tmp = matrix[first][i]
            # 3. Move bottom-left → top-left
            matrix[first][i] = matrix[last - offset][first]
            # 4. Move bottom-right → bottom-left
            matrix[last - offset][first] = matrix[last][last - offset]
            # 5. Move top-right → bottom-right
            matrix[last][last - offset] = matrix[i][last]
            # 6. Move saved top-left → top-right
            matrix[i][last] = tmp
```

Problem: Reshape a matrix into a new shape with different dimensions, preserving row-traversal order.
```csharp
// C#: Reshape the Matrix [LeetCode 566]
public int[][] MatrixReshape(int[][] mat, int r, int c) {
    int m = mat.Length, n = mat[0].Length;
    // 1. Guard: reshape impossible if total element count differs
    if (m * n != r * c) return mat;

    int[][] result = new int[r][];
    for (int i = 0; i < r; i++) result[i] = new int[c];

    // 2. Walk a flat index and map it to both source and destination 2D coords
    for (int idx = 0; idx < m * n; idx++) {
        // 3. Source: flat index → old (row, col)
        int srcR = idx / n, srcC = idx % n;
        // 4. Destination: flat index → new (row, col)
        int dstR = idx / c, dstC = idx % c;
        result[dstR][dstC] = mat[srcR][srcC];
    }
    return result;
}
```

**Drill Problems:**
- **Easy**: [LeetCode 566: Reshape the Matrix] — Map `idx → (idx/n, idx%n)` in old and new shapes.
- **Medium**: [LeetCode 54: Spiral Matrix] — Four-boundary peel with single-row/column guards.
- **Medium**: [LeetCode 48: Rotate Image] — Layer-by-layer four-way swap; `n/2` layers.

---

## Level 3: Grid BFS/DFS (Flood Fill, Islands, Shortest Path)

The matrix is an implicit graph. Each cell `(r, c)` is a node. Edges connect to its 4 (or 8) neighbors. BFS finds shortest paths in unweighted grids. DFS explores connected components.

**Mental Models & Invariants:**
- **What does the state mean?** `(r, c)` is the current node. The `visited` set (or in-place marker) tracks which nodes are fully processed.
- **What region is processed?** The connected component reachable from the source under the problem's constraints.
- **What is the invariant?**
  - **DFS**: Every cell popped from the call stack is processed exactly once. Marking before recursion prevents revisits.
  - **BFS**: The queue maintains a frontier. All cells at distance `d` are processed before distance `d+1` (level-order guarantee). This is why BFS yields shortest path in unweighted grids.

**4-Directional Neighbor Pattern:**

```
directions = [(0,1), (0,-1), (1,0), (-1,0)]   # right, left, down, up
for dr, dc in directions:
    nr, nc = r + dr, c + dc
    if 0 <= nr < rows and 0 <= nc < cols:     # bounds check
        ...                                    # process neighbor
```

**DFS Flood Fill Trace (Start `(1,1)`, old=1, new=2 on `[[1,1,1],[1,1,0],[1,0,1]]`):**

| Call | Cell | Old Val | Action | Grid Snapshot |
|------|------|---------|--------|---------------|
| 1 | `(1,1)` | 1 | Mark `2`. Recurse neighbors. | `[[1,1,1],[1,2,0],[1,0,1]]` |
| 2 | `(0,1)` | 1 | Mark `2`. Recurse. | `[[1,2,1],[1,2,0],[1,0,1]]` |
| 3 | `(0,0)` | 1 | Mark `2`. Recurse. | `[[2,2,1],[1,2,0],[1,0,1]]` |
| 4 | `(1,0)` | 1 | Mark `2`. Recurse. | `[[2,2,1],[2,2,0],[1,0,1]]` |
| 5 | `(2,0)` | 1 | Mark `2`. No valid unvisited neighbors. | `[[2,2,1],[2,2,0],[2,0,1]]` |
| 6 | `(0,2)` | 1 | Mark `2`. No valid unvisited neighbors. | `[[2,2,2],[2,2,0],[2,0,1]]` |

**BFS Shortest Path Trace (Start `(0,0)`, Target `(2,2)` on `[[0,0,0],[1,0,1],[1,0,0]]`):**

| Level (Dist) | Queue Contents | Cells Processed | Grid State |
|-------------|----------------|-----------------|------------|
| 1 | `[(0,0)]` | `(0,0)` | Mark visited |
| 2 | `[(0,1), (1,1)]` | `(0,1)`, `(1,1)` | Expand frontier |
| 3 | `[(0,2), (2,1)]` | `(0,2)`, `(2,1)` | Expand frontier |
| 4 | `[(2,2)]` | `(2,2)` | **Target reached. Distance = 5 cells** |

### ⚠️ Gotchas & Pitfalls
- **Marking after enqueue vs before**: In BFS, mark cells as visited **when enqueuing**, not when dequeuing. Marking on dequeue causes the same cell to be enqueued multiple times by different neighbors, inflating queue size and causing TLE.
- **Flood fill same-color edge case**: If `oldColor == newColor`, DFS/BFS enters infinite recursion. Always check and return early.
- **8-directional vs 4-directional**: Shortest Path in Binary Matrix uses 8 directions (including diagonals). Number of Islands uses 4. Using the wrong neighbor set gives wrong answers.

**Code Snippets:**

Problem: Perform a flood fill starting from pixel `(sr, sc)`. Change all connected pixels of the same original color to `color`.
```python
# Python: Flood Fill [LeetCode 733]
def floodFill(image: list[list[int]], sr: int, sc: int, color: int) -> list[list[int]]:
    original = image[sr][sc]
    # 1. Edge case: if the starting pixel is already the target color, no work needed
    if original == color:
        return image
    rows, cols = len(image), len(image[0])

    def dfs(r: int, c: int) -> None:
        # 2. Bounds check + color match check
        if r < 0 or r >= rows or c < 0 or c >= cols or image[r][c] != original:
            return
        # 3. Mark cell with new color (acts as "visited")
        image[r][c] = color
        # 4. Recurse into all 4 neighbors
        dfs(r + 1, c)
        dfs(r - 1, c)
        dfs(r, c + 1)
        dfs(r, c - 1)

    dfs(sr, sc)
    return image
```

Problem: Perform a flood fill starting from pixel `(sr, sc)`. Change all connected pixels of the same original color to `color`.
```csharp
// C#: Flood Fill [LeetCode 733]
public int[][] FloodFill(int[][] image, int sr, int sc, int color) {
    int original = image[sr][sc];
    // 1. Guard: same color means no work (prevents infinite recursion)
    if (original == color) return image;
    Dfs(image, sr, sc, original, color);
    return image;
}

private void Dfs(int[][] image, int r, int c, int original, int color) {
    // 2. Bounds + color check
    if (r < 0 || r >= image.Length || c < 0 || c >= image[0].Length || image[r][c] != original)
        return;
    // 3. Paint and recurse
    image[r][c] = color;
    Dfs(image, r + 1, c, original, color);
    Dfs(image, r - 1, c, original, color);
    Dfs(image, r, c + 1, original, color);
    Dfs(image, r, c - 1, original, color);
}
```

Problem: Count the number of islands (connected components of `'1'`s) in a 2D grid by sinking each island via DFS.
```python
# Python: Number of Islands [LeetCode 200]
def numIslands(grid: list[list[str]]) -> int:
    if not grid:
        return 0
    rows, cols = len(grid), len(grid[0])
    count = 0

    def dfs(r: int, c: int) -> None:
        # 1. Out of bounds or water/visited — stop
        if r < 0 or r >= rows or c < 0 or c >= cols or grid[r][c] != '1':
            return
        # 2. Sink the land cell to mark it visited (in-place, no extra memory)
        grid[r][c] = '0'
        # 3. Explore all 4 directions
        dfs(r + 1, c)
        dfs(r - 1, c)
        dfs(r, c + 1)
        dfs(r, c - 1)

    # 4. Scan every cell; each unvisited '1' starts a new island
    for r in range(rows):
        for c in range(cols):
            if grid[r][c] == '1':
                count += 1
                dfs(r, c)  # sink the entire island
    return count
```

Problem: Count the number of islands (connected components of `'1'`s) in a 2D grid by sinking each island via DFS.
```csharp
// C#: Number of Islands [LeetCode 200]
public int NumIslands(char[][] grid) {
    int rows = grid.Length, cols = grid[0].Length;
    int count = 0;
    for (int r = 0; r < rows; r++) {
        for (int c = 0; c < cols; c++) {
            if (grid[r][c] == '1') {
                count++;
                Sink(grid, r, c, rows, cols); // sink entire island
            }
        }
    }
    return count;
}

private void Sink(char[][] grid, int r, int c, int rows, int cols) {
    // 1. Boundary + water guard
    if (r < 0 || r >= rows || c < 0 || c >= cols || grid[r][c] != '1') return;
    // 2. Sink and recurse
    grid[r][c] = '0';
    Sink(grid, r + 1, c, rows, cols);
    Sink(grid, r - 1, c, rows, cols);
    Sink(grid, r, c + 1, rows, cols);
    Sink(grid, r, c - 1, rows, cols);
}
```

Problem: Find the shortest path from top-left `(0,0)` to bottom-right `(n-1,n-1)` in a binary matrix, moving in 8 directions. Return `-1` if no path exists.
```python
# Python: Shortest Path in Binary Matrix [LeetCode 1091]
from collections import deque

def shortestPathBinaryMatrix(grid: list[list[int]]) -> int:
    n = len(grid)
    # 1. Start or end blocked — impossible
    if grid[0][0] != 0 or grid[n - 1][n - 1] != 0:
        return -1

    # 2. 8 directions (including diagonals)
    dirs = [(-1,-1),(-1,0),(-1,1),(0,-1),(0,1),(1,-1),(1,0),(1,1)]

    queue = deque([(0, 0, 1)])  # (row, col, path_length)
    # 3. Mark visited ON ENQUEUE to prevent duplicate entries
    grid[0][0] = 1

    while queue:
        r, c, dist = queue.popleft()
        # 4. Reached destination — BFS guarantees shortest path
        if r == n - 1 and c == n - 1:
            return dist
        for dr, dc in dirs:
            nr, nc = r + dr, c + dc
            # 5. Valid, unblocked, unvisited neighbor
            if 0 <= nr < n and 0 <= nc < n and grid[nr][nc] == 0:
                grid[nr][nc] = 1  # mark visited before enqueue
                queue.append((nr, nc, dist + 1))

    return -1  # no path found
```

Problem: Find the shortest path from top-left `(0,0)` to bottom-right `(n-1,n-1)` in a binary matrix, moving in 8 directions. Return `-1` if no path exists.
```csharp
// C#: Shortest Path in Binary Matrix [LeetCode 1091]
public int ShortestPathBinaryMatrix(int[][] grid) {
    int n = grid.Length;
    // 1. Impossible if start or end is blocked
    if (grid[0][0] != 0 || grid[n - 1][n - 1] != 0) return -1;

    // 2. 8-directional movement
    int[][] dirs = {
        new[]{-1,-1}, new[]{-1,0}, new[]{-1,1}, new[]{0,-1},
        new[]{0,1}, new[]{1,-1}, new[]{1,0}, new[]{1,1}
    };

    var queue = new Queue<(int r, int c, int dist)>();
    queue.Enqueue((0, 0, 1));
    grid[0][0] = 1; // 3. Mark visited on enqueue

    while (queue.Count > 0) {
        var (r, c, dist) = queue.Dequeue();
        // 4. Target reached — BFS guarantees minimum distance
        if (r == n - 1 && c == n - 1) return dist;

        foreach (var d in dirs) {
            int nr = r + d[0], nc = c + d[1];
            // 5. Bounds + unblocked + unvisited
            if (nr >= 0 && nr < n && nc >= 0 && nc < n && grid[nr][nc] == 0) {
                grid[nr][nc] = 1; // mark before enqueue
                queue.Enqueue((nr, nc, dist + 1));
            }
        }
    }
    return -1;
}
```

**Drill Problems:**
- **Easy**: [LeetCode 733: Flood Fill] — DFS/BFS from source; guard `oldColor == newColor`.
- **Medium**: [LeetCode 200: Number of Islands] — Count connected components via DFS; sink visited land.
- **Medium**: [LeetCode 1091: Shortest Path in Binary Matrix] — BFS with 8 directions; mark visited on enqueue.
