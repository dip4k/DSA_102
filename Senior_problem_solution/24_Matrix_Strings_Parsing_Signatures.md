# Phase 24: Matrix, Strings, Parsing & In-Place Signatures

> **Focus:** Boundary-Constrained Traversal, Matrix In-Place Symmetries & Zero Flagging, In-Place Word Reversal, Version/Revision Parsing, Morris-Style Tree Flattening, Valley-Peak Extremum Invariants, Non-Destructive List Arithmetic, Canonical Unix Path Stacks, Greedy Text Line Justification, and Operator Precedence Stackless Arithmetic.  
> **Source Curriculum:** [`Senior_dsa_question_list.md`](../Senior_dsa_question_list.md) — Phase 24 (Problems #142–#152)  
> **Target Level:** Senior / Staff / Lead Software Engineer (Microsoft, Apple, Stripe Signature Patterns)  
> **Language & Runtime:** C# 12 / .NET 8 & 9 (Idiomatic, Span-optimized, Zero-allocation where applicable)

---

---

## 142. Spiral Matrix (LeetCode #54)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#matrix` `#boundary-shrink` `#state-machine` `#simulation` |
| **LeetCode Link** | [Spiral Matrix](https://leetcode.com/problems/spiral-matrix/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given an $m \times n$ matrix `matrix`, return all elements of the matrix in spiral order (clockwise progression starting at top-left $(0, 0)$ moving right, down, left, up, and contracting inward).
- **Assumptions & Contracts:**
  - The input is a rectangular 2D array of integers $matrix[m][n]$.
  - The output must be an ordered sequence (`IList<int>` / `List<int>`) containing exactly $m \times n$ values representing the clockwise spiral traversal.
  - An empty matrix or matrix with zero columns must return an empty list immediately without exception.
- **Key Constraints:**
  - $m == matrix.Length$
  - $n == matrix[i].Length$
  - $1 \le m, n \le 10$ (standard LeetCode), generalizes to arbitrary $m, n \le 10^4$ subject to total elements $m \times n \le 10^6$.
  - $-100 \le matrix[i][j] \le 100$.
- **Senior Edge Cases to Defend:**
  - **Single Element ($1 \times 1$):** `[[1]]` must yield `[1]` without boundary crossing errors.
  - **Single Row ($1 \times N$):** `[[1, 2, 3, 4]]` must traverse right and immediately terminate; scanning left or up must be guarded against.
  - **Single Column ($M \times 1$):** `[[1], [2], [3], [4]]` must traverse down and immediately terminate without redundant backward column sweeps.
  - **Non-Square Rectangular ($M \ne N$):** Matrices such as $3 \times 4$ or $4 \times 3$ where row or column boundaries exhaust before the other dimension.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Maintain 4 dynamic perimeter limits: `top`, `bottom`, `left`, `right`. Traverse along the active perimeter in clockwise cycle (Right $\to$ Down $\to$ Left $\to$ Up). After traversing a boundary line, immediately contract that boundary inward. Before performing the reverse directions (Left and Up), verify that the opposing boundaries have not crossed (`top <= bottom` and `left <= right`) to eliminate duplicate element ingestion.
- **Sample 1:**
  - **Input:** `matrix = [[1, 2, 3], [4, 5, 6], [7, 8, 9]]`
  - **Output:** `[1, 2, 3, 6, 9, 8, 7, 4, 5]`
- **Sample 2:**
  - **Input:** `matrix = [[1, 2, 3, 4], [5, 6, 7, 8], [9, 10, 11, 12]]`
  - **Output:** `[1, 2, 3, 4, 8, 12, 11, 10, 9, 5, 6, 7]`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine peeling an onion or unrolling tape along the outer perimeter of a rectangular field. You walk along the outermost northern boundary, then pave it over (shrink `top` down). You then walk along the eastern boundary and pave it over (shrink `right` left). If the northern and southern fences have not collided, you walk west along the southern fence and pave it over (shrink `bottom` up). If the western and eastern fences have not collided, you walk north along the western fence and pave it over (shrink `left` right). You repeat this contraction until all concentric layers have been completely visited.

#### 3.2 The Naive Bottleneck & Redundant Computation
A naive approach uses an auxiliary $M \times N$ `visited[m, n]` boolean matrix and advances a direction vector $(dr, dc)$ switching when encountering a visited cell or border. While asymptotically $O(M \times N)$, allocating and maintaining an auxiliary visited matrix incurs $O(M \times N)$ extra heap allocation, degrades cache locality through pointer indirections, and risks off-by-one infinite turning loops. Boundary shrinkage eliminates the visited ledger entirely by representing visited state as 4 scalar integers.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Shrinking Box Invariant:**
  At any point in time, the unvisited elements reside strictly within the closed bounding box:
  $$\Omega = \{ (r, c) \mid \text{top} \le r \le \text{bottom} \land \text{left} \le c \le \text{right} \}$$
  Each of the 4 directional sweeps consumes exactly one full segment of the bounding box and strictly contracts one boundary:
  - Sweep 1 (East): visits $(top, c)$ for $c \in [left, right]$; contracts $top \leftarrow top + 1$.
  - Sweep 2 (South): visits $(r, right)$ for $r \in [top, bottom]$; contracts $right \leftarrow right - 1$.
  - Sweep 3 (West): guarded by $top \le bottom$; visits $(bottom, c)$ for $c \in [right, left]$ step $-1$; contracts $bottom \leftarrow bottom - 1$.
  - Sweep 4 (North): guarded by $left \le right$; visits $(r, left)$ for $r \in [bottom, top]$ step $-1$; contracts $left \leftarrow left + 1$.
- **Termination Guarantee:**
  $$(bottom - top + 1) + (right - left + 1)$$
  decreases by at least 2 in each complete outer cycle. The algorithm terminates deterministically when $top > bottom$ or $left > right$.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
Clockwise Boundary Contraction Architecture:

        left                             right
          |                                |
  top ---> [ 1 ]   [ 2 ]   [ 3 ]   [ 4 ]  (Sweep East: row = top, c: left -> right; top++)
           [ 5 ]   [ 6 ]   [ 7 ]   [ 8 ]  |
           [ 9 ]   [10 ]   [11 ]   [12 ]  v (Sweep South: col = right, r: top -> bottom; right--)
bottom -> [13 ]   [14 ]   [15 ]   [16 ]
             ^                       |
             |                       v
          (Sweep North: col = left,  (Sweep West: row = bottom, c: right -> left; bottom--)
           r: bottom -> top; left++)  [Guarded by top <= bottom]
          [Guarded by left <= right]
```

#### 3.5 State Transition Triggers & Decision Gates
1. **Loop Invariant Gate:** While `top <= bottom && left <= right`, enter layer traversal.
2. **East Sweep:** Iterate $c$ from `left` to `right`. Add $matrix[top][c]$. Then `top++`.
3. **South Sweep:** Iterate $r$ from `top` to `bottom`. Add $matrix[r][right]$. Then `right--`.
4. **West Sweep Gate (Crucial Defense):** Check `if (top <= bottom)`. If true, iterate $c$ from `right` down to `left`. Add $matrix[bottom][c]$. Then `bottom--`. If false, row space is exhausted; skip to avoid re-reading the row just processed in East sweep!
5. **North Sweep Gate (Crucial Defense):** Check `if (left <= right)`. If true, iterate $r$ from `bottom` down to `top`. Add $matrix[r][left]$. Then `left++`. If false, column space is exhausted; skip to avoid re-reading the column just processed in South sweep!

#### 3.6 Concrete Step-by-Step State Trace
Input: `matrix = [[1, 2, 3, 4], [5, 6, 7, 8], [9, 10, 11, 12]]` ($M = 3, N = 4$).

| Cycle / Sweep | Active Boundaries Before | Traversed Coordinates & Values | Boundary Mutation | Output State |
| :--- | :--- | :--- | :--- | :--- |
| **C1: East** | $T=0, B=2, L=0, R=3$ | $(0,0)\to(0,3)$: `[1, 2, 3, 4]` | $T \leftarrow 1$ | `[1, 2, 3, 4]` |
| **C1: South** | $T=1, B=2, L=0, R=3$ | $(1,3)\to(2,3)$: `[8, 12]` | $R \leftarrow 2$ | `[1, 2, 3, 4, 8, 12]` |
| **C1: West** | $T=1 \le B=2 \implies$ Pass | $(2,2)\to(2,0)$: `[11, 10, 9]` | $B \leftarrow 1$ | `[..., 11, 10, 9]` |
| **C1: North** | $L=0 \le R=2 \implies$ Pass | $(1,0)$: `[5]` | $L \leftarrow 1$ | `[..., 9, 5]` |
| **C2: East** | $T=1, B=1, L=1, R=2$ | $(1,1)\to(1,2)$: `[6, 7]` | $T \leftarrow 2$ | `[..., 5, 6, 7]` |
| **C2: South** | $T=2, B=1, L=1, R=2$ | Loop condition $r \in [2, 1]$ empty | $R \leftarrow 1$ | `[..., 6, 7]` |
| **C2: West** | $T=2 > B=1 \implies$ **Blocked** | None (Gate defends against duplicate) | Unchanged | `[..., 6, 7]` |
| **C2: North** | $L=1 \le R=1$, but $T=2 > B=1$ | None (Outer loop terminates) | Exit | Final `Count = 12` |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1: Four-Boundary Shrinkage (Optimal & Industry Standard):** Tracks `top`, `bottom`, `left`, `right`. Operates in $O(1)$ auxiliary space, pre-allocates the exact capacity $m \times n$, and provides zero branching penalty.
- **Approach 2: Direction Vectors + In-Place Sentinel Mutation:** Uses directions `dr = [0, 1, 0, -1]`, `dc = [1, 0, -1, 0]` and marks visited cells with `101` (since elements are within $[-100, 100]$). Destructive to input data; rejected in immutable API contracts.

#### 4.2 Step-by-Step Natural Progression Flow
1. **Setup & Bounds:** Verify $matrix \ne null$ and dimensions $m, n > 0$. Instantiate `List<int>` with capacity $m \times n$ to prevent list re-allocations.
2. **Boundary Initialization:** Set `top = 0`, `bottom = m - 1`, `left = 0`, `right = n - 1`.
3. **Execution Loop:** Run while `top <= bottom && left <= right`.
4. **Perimeter Sweeps with Defensive Gates:** Execute East, South, West (guarded), North (guarded).
5. **Return:** Return populated list.

#### 4.3 Alternative Approaches Analysis
- **Simulation with Visited Array:** Requires an $m \times n$ bool array. Increases space to $O(M \times N)$ without any performance benefit.
- **Recursive Peeling:** Peels the outer ring and recurses on submatrix $(top+1, bottom-1, left+1, right-1)$. Incurs $O(\min(M, N))$ stack frames, which risks stack overflow for extreme matrix dimensions.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Approach | Time (Best) | Time (Avg) | Time (Worst) | Auxiliary Space | Output Space | Cache Locality | In-Place Mutability | Streaming Suitability |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **1. Boundary Shrink (Selected)** | $O(M \times N)$ | $O(M \times N)$ | $O(M \times N)$ | $O(1)$ | $O(M \times N)$ | High (row scans) | Non-destructive | Offline |
| **2. Visited Grid Simulation** | $O(M \times N)$ | $O(M \times N)$ | $O(M \times N)$ | $O(M \times N)$ | $O(M \times N)$ | Moderate | Non-destructive | Offline |
| **3. In-Place Sentinel** | $O(M \times N)$ | $O(M \times N)$ | $O(M \times N)$ | $O(1)$ | $O(M \times N)$ | Moderate | Mutates input | Offline |

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: Spiral Matrix (LeetCode #54)
 * ============================================================================
 * Core Pattern      : Boundary-Shrink Simulation (Clockwise Inward Contracting Box)
 * Time Complexity   : O(M * N) - Every cell visited exactly once
 * Space Complexity  : O(1) Auxiliary - Output list pre-allocated to exact capacity M * N
 * Selection Rule    : Standard four-pointer perimeter sweep with defensive boundary guards
 * Defensive Traps   : Must guard West sweep with (top <= bottom) and North sweep with (left <= right)
 * ============================================================================
 */

using System;
using System.Collections.Generic;

public class Solution
{
    /// <summary>
    /// Returns all elements of an M x N matrix in spiral order.
    /// Employs four-boundary inward contraction to achieve O(M * N) time and O(1) auxiliary space.
    /// </summary>
    /// <param name="matrix">The 2D rectangular integer grid.</param>
    /// <returns>Ordered list of elements in clockwise spiral sequence.</returns>
    public IList<int> SpiralOrder(int[][] matrix)
    {
        // Guard Clause: Validate non-null and non-empty matrix
        if (matrix == null || matrix.Length == 0 || matrix[0].Length == 0)
        {
            return Array.Empty<int>();
        }

        int rows = matrix.Length;
        int cols = matrix[0].Length;
        int totalElements = rows * cols;

        // Pre-allocate list capacity to avoid dynamic resizing and heap thrashing
        var result = new List<int>(capacity: totalElements);

        // Boundary Pointers
        int top = 0;
        int bottom = rows - 1;
        int left = 0;
        int right = cols - 1;

        while (top <= bottom && left <= right)
        {
            // 1. Traverse East along the active top boundary
            for (int col = left; col <= right; col++)
            {
                result.Add(matrix[top][col]);
            }
            top++; // Contract top boundary downward

            // 2. Traverse South along the active right boundary
            for (int row = top; row <= bottom; row++)
            {
                result.Add(matrix[row][right]);
            }
            right--; // Contract right boundary leftward

            // 3. Traverse West along the active bottom boundary (Defensive Guard against single row)
            if (top <= bottom)
            {
                for (int col = right; col >= left; col--)
                {
                    result.Add(matrix[bottom][col]);
                }
                bottom--; // Contract bottom boundary upward
            }

            // 4. Traverse North along the active left boundary (Defensive Guard against single column)
            if (left <= right)
            {
                for (int row = bottom; row >= top; row--)
                {
                    result.Add(matrix[row][left]);
                }
                left++; // Contract left boundary rightward
            }
        }

        return result;
    }
}
```

---

### 6. Senior Pitfalls & Defensive Traps
- **The Single-Row / Single-Column Duplicate Ingestion Trap:** The most frequent bug in Spiral Matrix occurs when $M = 1$ or $N = 1$ (or during the final pass of an odd-dimension rectangular matrix). After the East sweep increments `top`, if you do not check `if (top <= bottom)` before executing the West sweep, the algorithm will re-traverse the same single row in reverse, creating duplicates. The exact symmetric risk applies to the North sweep if `left <= right` is not verified.
- **Dynamic List Reallocation Churn:** In high-throughput backend services, returning a `List<int>` that expands dynamically via default growth (`capacity 4 -> 8 -> 16...`) creates unnecessary GC generational churn. Always initialize `new List<int>(rows * cols)`.
- **Jagged Array Invariants:** In C#, `int[][]` is an array of arrays, not a true 2D contiguous block `int[,]`. Defend against non-rectangular rows by verifying `matrix[i].Length == cols` if inputs come from untrusted external sources.

---

## 143. Rotate Image (LeetCode #48)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#matrix` `#in-place` `#transpose-reflect` `#linear-algebra` |
| **LeetCode Link** | [Rotate Image](https://leetcode.com/problems/rotate-image/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** You are given an $n \times n$ 2D `matrix` representing an image. Rotate the image by 90 degrees clockwise in-place. You must rotate the image directly without allocating another 2D matrix.
- **Assumptions & Contracts:**
  - The input matrix is strictly square ($n \times n$).
  - Transformation must be completely in-place: $O(1)$ auxiliary memory.
  - Return type is `void`; matrix is mutated directly.
- **Key Constraints:**
  - $n == matrix.Length == matrix[i].Length$
  - $1 \le n \le 20$ (generalizes to arbitrary $n \le 10^4$)
  - $-1000 \le matrix[i][j] \le 1000$
- **Senior Edge Cases to Defend:**
  - **$1 \times 1$ Matrix:** `[[1]]` requires zero operations; should exit gracefully.
  - **$2 \times 2$ Matrix:** Smallest non-trivial rotation testing boundary corner swaps.
  - **Odd $N$ ($3 \times 3, 5 \times 5$):** The center coordinate $((n-1)/2, (n-1)/2)$ must remain strictly unmoved.
  - **Even $N$ ($4 \times 4$):** No central fixed point; all cells participate in 4-cycle orbits.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Decompose a 90° clockwise geometric rotation into two elementary linear algebra operations:
  $$\text{Rotate}_{90^{\circ}}(\mathbf{M}) = \text{Reflect}_{\text{horizontal}}(\mathbf{M}^T)$$
  First, transpose the matrix across its main diagonal ($swap(matrix[i][j], matrix[j][i])$ for $j > i$). Second, reverse each row horizontally ($swap(matrix[i][c], matrix[i][n - 1 - c])$).
- **Sample 1:**
  - **Input:** `matrix = [[1, 2, 3], [4, 5, 6], [7, 8, 9]]`
  - **Output:** `[[7, 4, 1], [8, 5, 2], [9, 6, 3]]`
  - **Explanation:** Transpose yields `[[1, 4, 7], [2, 5, 8], [3, 6, 9]]`. Horizontal row reflection yields `[[7, 4, 1], [8, 5, 2], [9, 6, 3]]`.
- **Sample 2:**
  - **Input:** `matrix = [[5, 1, 9, 11], [2, 4, 8, 10], [13, 3, 6, 7], [15, 14, 12, 16]]`
  - **Output:** `[[15, 13, 2, 5], [14, 3, 4, 1], [12, 6, 8, 9], [16, 7, 10, 11]]`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Rotating a sheet of graph paper clockwise by 90 degrees can be executed cleanly in two folds. First, flip the paper across its top-left to bottom-right diagonal: rows turn into columns (Transpose). Then, flip the paper over vertically from left to right along its vertical centerline (Horizontal Reflection). The combined result is mathematically identical to a pure rigid 90° clockwise rotation.

#### 3.2 The Naive Bottleneck & Redundant Computation
Allocating an auxiliary $N \times N$ matrix allows a straightforward mapping: $rotated[c][n - 1 - r] = matrix[r][c]$. However, this requires $O(N^2)$ auxiliary heap space. Attempting direct in-place cyclic replacement of 4-tuples $(r, c) \to (c, n-1-r) \to (n-1-r, n-1-c) \to (n-1-c, r)$ is $O(1)$ space, but requires complicated nested offset math that is exceptionally prone to off-by-one index corruption. Transpose + Reflect achieves $O(1)$ auxiliary space with simple, clean, linear cache-line traversals.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Coordinate Mapping Proof:**
  Let $(r, c)$ be the row and column indices of any cell in an $N \times N$ matrix.
  1. **Transpose Step:**
     $$(r, c) \mapsto (c, r)$$
  2. **Horizontal Row Reflection Step:**
     $$(c, r) \mapsto (c, n - 1 - r)$$
  Observe that any cell originally at $(r, c)$ ends up at $(c, n - 1 - r)$, which is the exact definition of a 90° clockwise rotation!
  $$\begin{bmatrix} r_{\text{new}} \\ c_{\text{new}} \end{bmatrix} = \begin{bmatrix} 0 & 1 \\ -1 & 0 \end{bmatrix} \begin{bmatrix} r \\ c \end{bmatrix} + \begin{bmatrix} 0 \\ n-1 \end{bmatrix} = \begin{bmatrix} c \\ n - 1 - r \end{bmatrix}$$

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
Decomposition: Transpose -> Reflect

Initial Grid:
[ 1,  2,  3 ]          Diagonal Transpose (j > i)         [ 1,  4,  7 ]
[ 4,  5,  6 ]   ======================================>   [ 2,  5,  8 ]
[ 7,  8,  9 ]         matrix[i][j] <-> matrix[j][i]       [ 3,  6,  9 ]

                                                                   |
                                                                   v  Horizontal Row Reversal
                                                                      matrix[i][c] <-> matrix[i][n-1-c]
Final Rotated Grid:
[ 7,  4,  1 ]
[ 8,  5,  2 ]   <======================================
[ 9,  6,  3 ]
```

#### 3.5 State Transition Triggers & Decision Gates
1. **Transpose Gate:** For each row $i \in [0, n - 1]$, iterate $j \in [i + 1, n - 1]$. Note that $j$ starts strictly at $i + 1$. If $j$ starts at $0$, cells are swapped twice, neutralizing the transpose.
2. **Reflection Gate:** For each row $i \in [0, n - 1]$, iterate column cursor $c \in [0, \lfloor n / 2 \rfloor - 1]$. Swap $matrix[i][c]$ with $matrix[i][n - 1 - c]$.

#### 3.6 Concrete Step-by-Step State Trace
Input: `matrix = [[1, 2, 3], [4, 5, 6], [7, 8, 9]]` ($N = 3$).

| Step | Operation Target | Action / Swap | Matrix State After Operation |
| :--- | :--- | :--- | :--- |
| **Init** | Input Matrix | None | `[[1,2,3],[4,5,6],[7,8,9]]` |
| **T1** | $(i=0, j=1)$ | Swap `matrix[0][1]` (2) with `matrix[1][0]` (4) | `[[1,4,3],[2,5,6],[7,8,9]]` |
| **T2** | $(i=0, j=2)$ | Swap `matrix[0][2]` (3) with `matrix[2][0]` (7) | `[[1,4,7],[2,5,6],[3,8,9]]` |
| **T3** | $(i=1, j=2)$ | Swap `matrix[1][2]` (6) with `matrix[2][1]` (8) | `[[1,4,7],[2,5,8],[3,6,9]]` (Transpose Complete) |
| **R1** | Row 0 ($c=0$) | Swap `matrix[0][0]` (1) with `matrix[0][2]` (7) | `[[7,4,1],[2,5,8],[3,6,9]]` |
| **R2** | Row 1 ($c=0$) | Swap `matrix[1][0]` (2) with `matrix[1][2]` (8) | `[[7,4,1],[8,5,2],[3,6,9]]` |
| **R3** | Row 2 ($c=0$) | Swap `matrix[2][0]` (3) with `matrix[2][2]` (9) | `[[7,4,1],[8,5,2],[9,6,3]]` (Reflection Complete) |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1: Transpose + Row Reversal (Optimal & Recommended):** High cache locality during the horizontal reversal phase, clean modular code, impossible to introduce index inversion defects.
- **Approach 2: Four-Way Ring Cyclic Shift ($O(1)$ Space):** Traverses concentric square shells from outside to inside. Rotates 4 items at a time: `temp = top; top = left; left = bottom; bottom = right; right = temp`. Mathematically minimal operations ($N^2 / 4$ iterations), but complex 4-way indexing.

#### 4.2 Step-by-Step Natural Progression Flow
1. **Guard Clause:** If $matrix == null$ or $matrix.Length \le 1$, return immediately.
2. **Phase 1 (Transpose):** Dual loop $i \in [0, n - 2]$, $j \in [i + 1, n - 1]$. Swap in-place via tuple `(matrix[i][j], matrix[j][i]) = (matrix[j][i], matrix[i][j])`.
3. **Phase 2 (Row Reversal):** For each row $i$, invoke `Array.Reverse(matrix[i])` or use two pointers $left = 0, right = n - 1$ to swap across the center.

#### 4.3 Alternative Approaches Analysis
- **Counter-Clockwise Rotation:** If the requirement were 90° counter-clockwise rotation, the sequence would invert: Transpose then Vertical Column Reversal (or Row Reversal then Transpose).

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Approach | Time (Best) | Time (Avg) | Time (Worst) | Auxiliary Space | Output Space | Cache Locality | In-Place Mutability | Streaming Suitability |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **1. Transpose + Reflect** | $O(N^2)$ | $O(N^2)$ | $O(N^2)$ | $O(1)$ | $O(1)$ | High (row scans) | Yes (In-place) | Offline |
| **2. 4-Way Concentric Shell** | $O(N^2)$ | $O(N^2)$ | $O(N^2)$ | $O(1)$ | $O(1)$ | Moderate (4 jumps) | Yes (In-place) | Offline |
| **3. Auxiliary Buffer** | $O(N^2)$ | $O(N^2)$ | $O(N^2)$ | $O(N^2)$ | $O(N^2)$ | High | Non-mutating | Offline |

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: Rotate Image (LeetCode #48)
 * ============================================================================
 * Core Pattern      : Matrix Symmetry Decomposition (Transpose + Horizontal Reflection)
 * Time Complexity   : O(N^2) - Transpose visits N*(N-1)/2 elements; Reversal visits N*(N/2) elements
 * Space Complexity  : O(1) Auxiliary - Strictly in-place pointer/tuple swaps
 * Selection Rule    : Transpose + Reverse is preferred over 4-way shell rotation for maintainability
 * Defensive Traps   : In Transpose, inner loop must start at i + 1, NOT 0 (avoiding double-swap)
 * ============================================================================
 */

using System;

public class Solution
{
    /// <summary>
    /// Rotates an N x N matrix clockwise by 90 degrees in-place.
    /// Uses Transposition followed by Horizontal Row Reversal.
    /// </summary>
    /// <param name="matrix">Square 2D grid to mutate in-place.</param>
    public void Rotate(int[][] matrix)
    {
        // Guard Clause: Null or single-cell matrices require no manipulation
        if (matrix == null || matrix.Length <= 1)
        {
            return;
        }

        int n = matrix.Length;

        // PHASE 1: Transpose Matrix across the main diagonal (swap (i, j) with (j, i))
        // Invariant: j strictly starts at i + 1 to prevent double-swapping elements back to original state
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                // Idiomatic C# tuple swap eliminates temporary variable boilerplate
                (matrix[i][j], matrix[j][i]) = (matrix[j][i], matrix[i][j]);
            }
        }

        // PHASE 2: Horizontally reflect each row (reverse row elements)
        // Highly cache-friendly: accesses contiguous row memory
        for (int i = 0; i < n; i++)
        {
            ReverseRow(matrix[i], n);
        }
    }

    /// <summary>
    /// In-place two-pointer reversal of a single row.
    /// </summary>
    private static void ReverseRow(int[] row, int n)
    {
        int left = 0;
        int right = n - 1;

        while (left < right)
        {
            (row[left], row[right]) = (row[right], row[left]);
            left++;
            right--;
        }
    }
}

/// <summary>
/// High-Performance Alternative: 4-Way Concentric Shell In-Place Rotation.
/// Minimal memory writes (exactly 1 write per cell), useful when cache writes are expensive.
/// </summary>
public class SolutionConcentricShell
{
    public void Rotate(int[][] matrix)
    {
        if (matrix == null || matrix.Length <= 1) return;

        int n = matrix.Length;
        int layers = n / 2;

        for (int layer = 0; layer < layers; layer++)
        {
            int first = layer;
            int last = n - 1 - layer;

            for (int i = first; i < last; i++)
            {
                int offset = i - first;

                // Save top element
                int top = matrix[first][i];

                // 1. Move Left to Top
                matrix[first][i] = matrix[last - offset][first];

                // 2. Move Bottom to Left
                matrix[last - offset][first] = matrix[last][last - offset];

                // 3. Move Right to Bottom
                matrix[last][last - offset] = matrix[i][last];

                // 4. Move Top to Right
                matrix[i][last] = top;
            }
        }
    }
}
```

---

### 6. Senior Pitfalls & Defensive Traps
- **The Transpose Double-Swap Regression:** If the inner loop of the transpose is written as `for (int j = 0; j < n; j++)`, every pair is swapped when $(i, j)$ is visited, and then swapped back when $(j, i)$ is visited! The entire matrix ends up completely unchanged. Always ensure `j = i + 1`.
- **False Assumption of Rectangular Inputs:** The Transpose + Reflect algorithm mathematically requires a square matrix ($M = N$). In interview scenarios, clarify immediately whether rectangular rotations are possible. If $M \ne N$, an in-place rotation is mathematically impossible without re-allocating a new $N \times M$ matrix because the shape of the array memory buffer itself must transform.
- **Cache-Line Inefficiencies in Column Sweeps:** Notice that Transposition reads across row $i$ and column $j$. In modern CPUs, column traversal causes cache line misses on large matrices. In performance-critical engines, cache-blocking (tiling) transpositions into $32 \times 32$ or $64 \times 64$ sub-blocks optimizes L1/L2 cache residency.

---

---

## 144. Set Matrix Zeroes (LeetCode #73)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#matrix` `#in-place` `#state-encoding` `#bitmasking` |
| **LeetCode Link** | [Set Matrix Zeroes](https://leetcode.com/problems/set-matrix-zeroes/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given an $m \times n$ integer matrix `matrix`, if an element is 0, set its entire row and column to 0's in-place.
- **Assumptions & Contracts:**
  - The modification must occur strictly in-place.
  - While an $O(m \times n)$ or $O(m + n)$ space solution is trivial, the senior expectation is an optimal $O(1)$ auxiliary space solution.
  - Return type is `void`; matrix is mutated directly.
- **Key Constraints:**
  - $m == matrix.Length$
  - $n == matrix[0].Length$
  - $1 \le m, n \le 200$
  - $-2^{31} \le matrix[i][j] \le 2^{31} - 1$ (cells can take any 32-bit integer value, precluding magic-number sentinel values like `-1` or `int.MaxValue`).
- **Senior Edge Cases to Defend:**
  - **Zero at Origin $(0, 0)$:** Both Row 0 and Column 0 must be zeroed out; the indicator at $(0, 0)$ must not cross-contaminate.
  - **Zero in Row 0 but not Col 0 (and vice versa):** Setting Row 0 to zero prematurely could cause the entire matrix to become zeroes.
  - **No Zeroes:** Matrix must remain completely unaltered.
  - **All Zeroes:** Matrix remains all zeroes with minimal branch overhead.
  - **Single Row ($1 \times N$) or Single Column ($M \times 1$):** Boundary flags must resolve correctly without out-of-bounds indexing.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Use the matrix's own first row ($matrix[0][j]$) and first column ($matrix[i][0]$) as the ledger arrays to record which rows and columns must be zeroed. Because cell $matrix[0][0]$ overlaps both the first row and first column, disambiguate them using a single scalar variable `col0HasZero` (tracking column 0) while letting $matrix[0][0]$ track row 0.
- **Sample 1:**
  - **Input:** `matrix = [[1, 1, 1], [1, 0, 1], [1, 1, 1]]`
  - **Output:** `[[1, 0, 1], [0, 0, 0], [1, 0, 1]]`
- **Sample 2:**
  - **Input:** `matrix = [[0, 1, 2, 0], [3, 4, 5, 2], [1, 3, 1, 5]]`
  - **Output:** `[[0, 0, 0, 0], [0, 4, 5, 0], [0, 3, 1, 0]]`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine a massive spreadsheet where certain cells contain a critical error (zero). When an error occurs, the entire row and column must be crossed out. Instead of buying a fresh notepad (allocating auxiliary memory $O(M+N)$), you use the top row and left-most margin of the existing sheet to make tally marks. A mark in the top margin of column $j$ means "cross out column $j$". A mark in the left margin of row $i$ means "cross out row $i$". The only collision is the top-left corner cell $(0, 0)$, which belongs to both margins. You solve this by holding a small post-it note in your hand (`col0HasZero`) exclusively for the first column.

#### 3.2 The Naive Bottleneck & Redundant Computation
If you immediately overwrite rows and columns with 0 as soon as you find a 0, you create a catastrophic cascade: a newly written 0 will be mistaken for an original 0 in subsequent iterations, eventually turning the entire matrix into 0s! The naive fix allocates an $M \times N$ clone ($O(M \times N)$ space), or allocates two boolean arrays `row[m]` and `col[n]` ($O(M + N)$ space). The breakthrough senior optimization reuses existing matrix memory for these vectors.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **In-Place Header Encoding Invariant:**
  For any cell $(i, j)$ with $i \ge 1$ and $j \ge 1$:
  $$matrix[i][j] = 0 \implies matrix[i][0] = 0 \land matrix[0][j] = 0$$
- **Bottom-Up / Reverse Propagation Invariant:**
  When writing zeroes back to the matrix, we must update the inner matrix $(1 \dots m-1, 1 \dots n-1)$ first. If we update the first row or first column first, we overwrite the ledgers and lose the record of which downstream rows and columns were originally marked!

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
Header Sentinel Ledger Architecture:

        col 0      col 1       col 2       col 3
       (col0)
row 0: [ M[0,0] ]  [ M[0,1] ]  [ M[0,2] ]  [ M[0,3] ]  <-- Ledger for Columns 1..3
         |                                                 (M[0,0] ledger for Row 0)
row 1: [ M[1,0] ]  [ M[1,1] ]  [ M[1,2] ]  [ M[1,3] ]
row 2: [ M[2,0] ]  [ M[2,1] ]  [ M[2,2] ]  [ M[2,3] ]
         ^          ---------------------------------
         |                    Inner Matrix
   Ledger for Rows 1..2     (Processed first using headers)
   
   Separate Scalar: bool col0HasZero (tracks Column 0 independently)
```

#### 3.5 State Transition Triggers & Decision Gates
1. **Ledger Scan (Forward Pass):**
   - For $i \in [0, m-1]$:
     - If $matrix[i][0] == 0$, set `col0HasZero = true`.
     - For $j \in [1, n-1]$:
       - If $matrix[i][j] == 0$, set $matrix[i][0] = 0$ and $matrix[0][j] = 0$.
2. **Inner Body Zeroing (Reverse / Downstream Pass):**
   - For $i \in [1, m-1]$ and $j \in [1, n-1]$:
     - If $matrix[i][0] == 0 \lor matrix[0][j] == 0$, set $matrix[i][j] = 0$.
3. **Row 0 Zeroing:**
   - If $matrix[0][0] == 0$, set all cells in row 0 to 0 ($matrix[0][j] = 0$ for $j \in [0, n-1]$).
4. **Col 0 Zeroing:**
   - If `col0HasZero == true`, set all cells in col 0 to 0 ($matrix[i][0] = 0$ for $i \in [0, m-1]$).

#### 3.6 Concrete Step-by-Step State Trace
Input: `matrix = [[0, 1, 2, 0], [3, 4, 5, 2], [1, 3, 1, 5]]` ($M=3, N=4$).

| Phase | Cell / Action | Ledger Mutation | Matrix State |
| :--- | :--- | :--- | :--- |
| **Init** | Input | `col0HasZero = false` | `[[0, 1, 2, 0], [3, 4, 5, 2], [1, 3, 1, 5]]` |
| **P1: Scan $i=0$** | $matrix[0][0] == 0 \implies col0=T$<br>$matrix[0][3] == 0 \implies M[0][3]=0, M[0][0]=0$ | `col0 = true`<br>`M[0][0]=0, M[0][3]=0` | `[[0, 1, 2, 0], [3, 4, 5, 2], [1, 3, 1, 5]]` |
| **P1: Scan $i=1, 2$**| No zeroes in inner cells | No change | Same |
| **P2: Inner Zero**| For $i \in [1..2], j \in [1..3]$:<br>Check $M[i][0]$ or $M[0][j]$ | $M[0][3] == 0 \implies$ col 3 zeroes! | `M[1][3] = 0`, `M[2][3] = 0` |
| **P3: Row 0 Zero**| $M[0][0] == 0 \implies$ Entire Row 0 zeroes | All $M[0][j] = 0$ | `[[0, 0, 0, 0], [3, 4, 5, 0], [1, 3, 1, 0]]` |
| **P4: Col 0 Zero**| `col0 == true` $\implies$ Entire Col 0 zeroes | All $M[i][0] = 0$ | `[[0, 0, 0, 0], [0, 4, 5, 0], [0, 3, 1, 0]]` |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1: First Row & Column Sentinel Ledger ($O(1)$ Extra Space - Optimal):** Uses in-situ header elements as markers plus one boolean scalar. Meets Senior/Principal bar for embedded and cache-conscious engineering.
- **Approach 2: Auxiliary $O(M + N)$ BitSets / Boolean Arrays:** Simple to code, but uses heap allocations proportional to matrix perimeter. Acceptable only as a preliminary baseline.

#### 4.2 Step-by-Step Natural Progression Flow
1. **Inspect Dimensions:** Handle trivial null/empty matrices.
2. **First Col Check:** Inspect column 0 independently to initialize `col0HasZero`.
3. **Record Marks:** Traverse the rest of the matrix. For every cell with value 0, set its row header $matrix[i][0] = 0$ and column header $matrix[0][j] = 0$.
4. **Populate Inner Body:** Iterate backwards from $(m-1, n-1)$ down to $(1, 1)$ to avoid corrupting header flags before reading them.
5. **Populate Headers:** Finally apply row 0 and column 0 zeroing based on $matrix[0][0]$ and `col0HasZero`.

#### 4.3 Alternative Approaches Analysis
- **Magic Number Sentinels:** Replacing zeroes with an unused number (e.g., `int.MinValue + 7`) during pass 1 and resolving in pass 2. Defective because the constraints state elements span the full range $[-2^{31}, 2^{31} - 1]$; no magic value is guaranteed unused.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Approach | Time (Best) | Time (Avg) | Time (Worst) | Auxiliary Space | Output Space | Cache Locality | In-Place Mutability | Streaming Suitability |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **1. Header Sentinels** | $O(M \times N)$ | $O(M \times N)$ | $O(M \times N)$ | $O(1)$ | $O(1)$ | High (row-major) | Yes (In-place) | Offline |
| **2. O(M+N) Vectors** | $O(M \times N)$ | $O(M \times N)$ | $O(M \times N)$ | $O(M + N)$ | $O(1)$ | High | Yes (In-place) | Offline |
| **3. Matrix Clone** | $O(M \times N)$ | $O(M \times N)$ | $O(M \times N)$ | $O(M \times N)$ | $O(1)$ | Moderate | Non-destructive | Offline |

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: Set Matrix Zeroes (LeetCode #73)
 * ============================================================================
 * Core Pattern      : In-Place Header Flagging (O(1) Auxiliary Space)
 * Time Complexity   : O(M * N) - Two full passes across the matrix
 * Space Complexity  : O(1) Auxiliary - Reuses row 0 and col 0; single boolean flag
 * Selection Rule    : Mandatory for memory-constrained zero-heap allocation requirements
 * Defensive Traps   : Disambiguate matrix[0][0] collision using separate col0HasZero flag;
 *                     Always update inner cells (1..m-1, 1..n-1) BEFORE zeroing row 0
 * ============================================================================
 */

using System;

public class Solution
{
    /// <summary>
    /// Modifies the matrix in-place so that if an element is 0, its entire row and column are 0.
    /// Operates in O(M * N) time and O(1) auxiliary space using header marking.
    /// </summary>
    /// <param name="matrix">2D rectangular grid of integers.</param>
    public void SetZeroes(int[][] matrix)
    {
        // Guard Clause
        if (matrix == null || matrix.Length == 0 || matrix[0].Length == 0)
        {
            return;
        }

        int m = matrix.Length;
        int n = matrix[0].Length;

        // Disambiguation flag: matrix[0][0] tracks row 0; col0HasZero tracks column 0
        bool col0HasZero = false;

        // PASS 1: Scan matrix and register zero markers in row 0 and col 0
        for (int i = 0; i < m; i++)
        {
            // Check if column 0 naturally contains a zero
            if (matrix[i][0] == 0)
            {
                col0HasZero = true;
            }

            // Scan columns 1 through n - 1
            for (int j = 1; j < n; j++)
            {
                if (matrix[i][j] == 0)
                {
                    matrix[i][0] = 0; // Mark corresponding row
                    matrix[0][j] = 0; // Mark corresponding column
                }
            }
        }

        // PASS 2: Update inner cells (1 <= i < m, 1 <= j < n) based on header marks
        // Note: Iterating in reverse (bottom-up, right-to-left) preserves header integrity
        for (int i = m - 1; i >= 1; i--)
        {
            for (int j = n - 1; j >= 1; j--)
            {
                if (matrix[i][0] == 0 || matrix[0][j] == 0)
                {
                    matrix[i][j] = 0;
                }
            }
        }

        // PASS 3: Update Row 0 if matrix[0][0] was flagged
        if (matrix[0][0] == 0)
        {
            for (int j = 0; j < n; j++)
            {
                matrix[0][j] = 0;
            }
        }

        // PASS 4: Update Column 0 if col0HasZero was flagged
        if (col0HasZero)
        {
            for (int i = 0; i < m; i++)
            {
                matrix[i][0] = 0;
            }
        }
    }
}
```

---

### 6. Senior Pitfalls & Defensive Traps
- **The Header Premature Zeroing Hazard:** If row 0 or column 0 is zeroed out before the inner cells $(1 \dots m-1, 1 \dots n-1)$ are evaluated, all header markers are wiped out. The entire remainder of the matrix would then erroneously turn to zeroes. Bottom-up or inner-first updates are strictly mandatory.
- **The Origin Collision Bug:** Trying to let $matrix[0][0]$ represent both row 0 and column 0 causes mutual corruption. If row 0 has a zero, $matrix[0][0]$ becomes 0, which would trick the algorithm into thinking column 0 must also be zeroed out. A distinct scalar (`col0HasZero`) is required.
- **Sentinel Overflow Invalidation:** Never use out-of-band values like `int.MaxValue` or negative sentinels unless the problem constraints explicitly forbid them. In LeetCode #73, values span all 32-bit signed integers.

---

## 145. Reverse Words in a String (LeetCode #151)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#string` `#two-pointers` `#in-place` `#parsing` |
| **LeetCode Link** | [Reverse Words in a String](https://leetcode.com/problems/reverse-words-in-a-string/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given an input string `s`, reverse the order of the words. A word is defined as a sequence of non-space characters. The words in `s` will be separated by at least one space. Return a string of the words in reverse order concatenated by a single space.
- **Assumptions & Contracts:**
  - The returned string must not contain leading or trailing spaces.
  - Multiple consecutive spaces between words must be reduced to a single space.
  - Words themselves must retain their original internal character order (only the sequence of words is reversed).
- **Key Constraints:**
  - $1 \le s.Length \le 10^4$
  - `s` contains English letters (upper-case and lower-case), digits, and spaces `' '`.
  - There is at least one word in `s`.
- **Senior Edge Cases to Defend:**
  - **Leading and Trailing Spaces:** `"   hello world   "` must resolve to `"world hello"`.
  - **Irregular Spacing:** `"a   good   example"` must resolve to `"example good a"`.
  - **Single Word with Extreme Padding:** `"   lonely   "` must resolve to `"lonely"`.
  - **Single Character:** `"a"` must resolve to `"a"`.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** The in-place canonical algorithm operates in 3 distinct steps on a mutable character array:
  1. **Clean & Compact Spaces:** Use a fast/slow two-pointer write cursor to strip leading, trailing, and duplicate spaces, producing a normalized character span of length $K$.
  2. **Global Array Reversal:** Reverse the entire compacted character span $[0 \dots K - 1]$. Words are now in correct reverse order, but each individual word is backwards.
  3. **Local Word Reversal:** Traverse the span and reverse the characters of each word individually back to their natural order.
- **Sample 1:**
  - **Input:** `s = "the sky is blue"`
  - **Output:** `"blue is sky the"`
- **Sample 2:**
  - **Input:** `s = "  hello world  "`
  - **Output:** `"world hello"`
- **Sample 3:**
  - **Input:** `s = "a good   example"`
  - **Output:** `"example good a"`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine a train made of freight cars (words) where each car contains letters in order. You want to invert the train's direction on a single track. If you grab the entire train and flip it end-to-end 180 degrees, the caboose is now at the front and the engine is at the back. However, all the letters inside each car are now upside down and reversed! To fix this, you simply walk into each freight car individually and reverse its letters back. Two global/local reversals cancel out the character inversion while preserving the reversed car order.

#### 3.2 The Naive Bottleneck & Redundant Computation
Using `s.Split(' ', StringSplitOptions.RemoveEmptyEntries)` followed by `Array.Reverse()` and `string.Join(" ", ...)` creates multiple intermediate string objects and an array of substrings on the managed heap. For large documents or high-throughput stream processing, this causes high GC pressure. The three-pass in-place reversal on a pooled `char[]` buffer or `Span<char>` allocates only the final string output.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Inversion Duality Invariant:**
  Let word $W_i$ be a string of characters $c_{i,1} c_{i,2} \dots c_{i,k}$. The sentence is:
  $$S = W_1 \circ W_2 \circ \dots \circ W_m$$
  Reversing the entire string $S^R$ yields:
  $$S^R = W_m^R \circ W_{m-1}^R \circ \dots \circ W_1^R$$
  Applying reversal to each word block individually $(W_i^R)^R = W_i$ yields:
  $$(S^R)_{\text{words restored}} = W_m \circ W_{m-1} \circ \dots \circ W_1$$
  which is precisely the required reversed word order with zero extra auxiliary word storage!

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
Three-Pass In-Place Architecture:

Input: "  the   sky  is   blue  "

Pass 1: Space Compaction (Read/Write Cursors)
        [t, h, e, ' ', s, k, y, ' ', i, s, ' ', b, l, u, e]  (Length = K = 15)

Pass 2: Global Reversal (0 to K - 1)
        [e, u, l, b, ' ', s, i, ' ', y, k, s, ' ', e, h, t]

Pass 3: Local Word Reversal (Pointers at word boundaries)
        Word 1: [e, u, l, b] ---> [b, l, u, e]
        Word 2: [s, i]       ---> [i, s]
        Word 3: [y, k, s]    ---> [s, k, y]
        Word 4: [e, h, t]    ---> [t, h, e]
        
Final Result: "blue is sky the"
```

#### 3.5 State Transition Triggers & Decision Gates
1. **Compaction Cursor Gate:**
   - Scan `read` from 0 to $N - 1$. Skip leading spaces.
   - When non-space is encountered, if `write > 0`, insert a single space separator: `chars[write++] = ' '`.
   - Copy word characters: `while (read < N && chars[read] != ' ') chars[write++] = chars[read++]`.
2. **Global Reverse:**
   - `Reverse(chars, 0, write - 1)`.
3. **Word Restore Gate:**
   - Use two pointers `start = 0, end = 0`.
   - Advance `end` until space or end of compacted buffer (`end == write || chars[end] == ' '`).
   - Reverse individual word `Reverse(chars, start, end - 1)`.
   - Update `start = end + 1`, `end = start`.

#### 3.6 Concrete Step-by-Step State Trace
Input: `s = "  Bob    Loves  Alice  "`

| Phase | Read Cursor | Write Cursor / Action | Compacted Array Content |
| :--- | :--- | :--- | :--- |
| **P1: Compaction** | Skips `[0..1]` | Read `[2..4]` ("Bob") $\implies$ copy | `['B','o','b']` (`write=3`) |
| **P1: Compaction** | Skips `[5..8]` | `write > 0 \implies` append `' '` | `['B','o','b',' ']` (`write=4`) |
| **P1: Compaction** | Read `[9..13]` ("Loves") | Copy "Loves" | `['B','o','b',' ','L','o','v','e','s']` (`write=9`) |
| **P1: Compaction** | Skips `[14..15]` | Append `' '` + copy "Alice" | `[...,' ','A','l','i','c','e']` (`write=15`) |
| **P2: Global Rev** | $0 \to 14$ | Reverse entire span $[0 \dots 14]$ | `"ecilA sevoL boB"` |
| **P3: Local Rev** | Word 1 $[0 \dots 4]$ | Reverse `"ecilA"` $\to$ `"Alice"` | `"Alice sevoL boB"` |
| **P3: Local Rev** | Word 2 $[6 \dots 10]$| Reverse `"sevoL"` $\to$ `"Loves"` | `"Alice Loves boB"` |
| **P3: Local Rev** | Word 3 $[12 \dots 14]$| Reverse `"boB"` $\to$ `"Bob"` | `"Alice Loves Bob"` |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1: Three-Pass In-Place Array Reversal (Optimal System Standard):** Operates directly on a mutable `char[]` or `Span<char>`. Demonstrates mastery of two-pointer cursor management, zero intermediate string allocation, and strictly $O(N)$ execution.
- **Approach 2: Right-to-Left Extraction with StringBuilder:** Traverses string backwards from index $N-1$ down to 0, identifying word tokens and appending to a `StringBuilder`. Highly readable and allocates only the builder capacity $N$.

#### 4.2 Step-by-Step Natural Progression Flow
1. Convert string `s` to `char[] chars`.
2. Compact characters using two pointers `read` and `write`.
3. Reverse the slice `0` to `write - 1`.
4. Iterate through the slice, identifying word boundaries separated by `' '`, reversing each word in-place.
5. Return `new string(chars, 0, write)`.

#### 4.3 Alternative Approaches Analysis
- **Regex Split / LINQ:** `string.Join(" ", s.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Reverse())`. Concisely written in one line, but creates multiple temporary object arrays and closures. Unacceptable in performance-critical paths.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Approach | Time (Best) | Time (Avg) | Time (Worst) | Auxiliary Space | Output Space | Cache Locality | In-Place Mutability | Streaming Suitability |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **1. In-Place 3-Pass** | $O(N)$ | $O(N)$ | $O(N)$ | $O(N)$ (C# char[]) | $O(N)$ | Optimal | Mutable buffer | Offline |
| **2. Backward StringBuilder**| $O(N)$ | $O(N)$ | $O(N)$ | $O(N)$ | $O(N)$ | High | Non-mutating | Offline |
| **3. String.Split + LINQ** | $O(N)$ | $O(N)$ | $O(N)$ | $O(N)$ (multiple heap) | $O(N)$ | Poor (GC churn) | Non-mutating | Offline |

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: Reverse Words in a String (LeetCode #151)
 * ============================================================================
 * Core Pattern      : Three-Pass In-Place Reversal (Compaction -> Global Rev -> Local Rev)
 * Time Complexity   : O(N) - Linear passes over character array
 * Space Complexity  : O(N) in C# due to string immutability; O(1) in C++ mutable in-place strings
 * Selection Rule    : Preferred for memory-conscious tokenization and in-place buffer manipulation
 * Defensive Traps   : Guard against multiple middle spaces and preserve word internal order
 * ============================================================================
 */

using System;
using System.Text;

public class Solution
{
    /// <summary>
    /// Reverses words in a string with clean single-space delimiters.
    /// Employs three-pass in-place reversal: Compact Spaces -> Reverse All -> Reverse Each Word.
    /// </summary>
    /// <param name="s">The input string possibly containing messy spaces.</param>
    /// <returns>Reversed words formatted with single spaces.</returns>
    public string ReverseWords(string s)
    {
        if (string.IsNullOrWhiteSpace(s))
        {
            return string.Empty;
        }

        char[] chars = s.ToCharArray();
        int n = chars.Length;

        // STEP 1: Compact characters and normalize whitespace in-place
        int write = 0;
        int read = 0;

        while (read < n)
        {
            // Skip leading or consecutive whitespace
            while (read < n && chars[read] == ' ')
            {
                read++;
            }

            if (read >= n) break;

            // If we have already written a word, insert a single delimiter space
            if (write > 0)
            {
                chars[write++] = ' ';
            }

            // Copy the active word characters
            while (read < n && chars[read] != ' ')
            {
                chars[write++] = chars[read++];
            }
        }

        // STEP 2: Reverse the entire compacted buffer [0 .. write - 1]
        Reverse(chars, 0, write - 1);

        // STEP 3: Reverse each individual word back to normal
        int wordStart = 0;
        for (int i = 0; i <= write; i++)
        {
            // Word boundary encountered at space or at the end of the compacted length
            if (i == write || chars[i] == ' ')
            {
                Reverse(chars, wordStart, i - 1);
                wordStart = i + 1;
            }
        }

        return new string(chars, 0, write);
    }

    /// <summary>
    /// Reverses a segment of the character array in-place between left and right inclusive.
    /// </summary>
    private static void Reverse(char[] array, int left, int right)
    {
        while (left < right)
        {
            (array[left], array[right]) = (array[right], array[left]);
            left++;
            right--;
        }
    }
}

/// <summary>
/// Alternative High-Readability Approach: Right-to-Left Word Extraction via StringBuilder.
/// </summary>
public class SolutionStringBuilder
{
    public string ReverseWords(string s)
    {
        if (string.IsNullOrWhiteSpace(s)) return string.Empty;

        var sb = new StringBuilder(capacity: s.Length);
        int right = s.Length - 1;

        while (right >= 0)
        {
            // Skip trailing spaces
            while (right >= 0 && s[right] == ' ')
            {
                right--;
            }

            if (right < 0) break;

            // Find word boundary
            int left = right;
            while (left >= 0 && s[left] != ' ')
            {
                left--;
            }

            // Append space delimiter if not first word
            if (sb.Length > 0)
            {
                sb.Append(' ');
            }

            // Append word slice
            sb.Append(s, left + 1, right - left);

            right = left - 1;
        }

        return sb.ToString();
    }
}
```

---

### 6. Senior Pitfalls & Defensive Traps
- **String Immutability in Managed Runtimes (.NET / Java):** In C#, `System.String` is immutable. True zero-allocation in-place reversal can only be executed on a `char[]` or `Span<char>`. In interview settings, explicitly state: *"In C#, because strings are immutable, we convert to a char array ($O(N)$ space), but the algorithmic transformation on the character buffer operates in $O(1)$ auxiliary space."*
- **The Trailing Word Off-by-One:** When reversing individual words in Step 3, the loop must check up to `i <= write` (not `i < write`). If checked only up to `i < write`, the very last word does not encounter a trailing space and will remain unreversed unless flushed explicitly after the loop.
- **Buffer Garbage Exposure:** When constructing `new string(chars, 0, write)`, always pass the compacted length `write`. Passing `new string(chars)` will include stale trailing characters left over from the uncompacted original string.

---

---

## 146. Compare Version Numbers (LeetCode #165)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | 🔥 High |
| **Pattern Tags** | `#string` `#two-pointers` `#parsing` `#simulation` |
| **LeetCode Link** | [Compare Version Numbers](https://leetcode.com/problems/compare-version-numbers/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given two version strings, `version1` and `version2`, compare them. A version number consists of revisions separated by dots `'.'`. The value of a revision is its integer value ignoring leading zeros. If a version doesn't specify an index revision, treat it as 0.
  - If `version1 < version2`, return -1.
  - If `version1 > version2`, return 1.
  - Otherwise, return 0.
- **Assumptions & Contracts:**
  - Input consists only of digits and the `'.'` separator.
  - Revisions fit within a standard 32-bit signed integer.
  - Comparison proceeds strictly from left-to-right revision index.
- **Key Constraints:**
  - $1 \le version1.Length, version2.Length \le 500$
  - `version1` and `version2` only contain digits and `'.'`.
  - Revisions do not have leading `'.'` or trailing `'.'`.
  - No consecutive dots: `..`.
- **Senior Edge Cases to Defend:**
  - **Asymmetric Revision Counts:** `"1.0"` vs `"1.0.0.0"` must evaluate as equal ($0$).
  - **Leading Zeros within Revision:** `"1.01"` vs `"1.001"` must evaluate as equal ($0$).
  - **Trailing Non-Zero Revision:** `"1.0.1"` vs `"1"` must evaluate to $1$.
  - **Extreme Values:** Revision values like `2147483647` or versions with dozens of revision levels.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Use two independent cursors `i` and `j` to simultaneously parse numerical revision chunks from `version1` and `version2` on the fly. If one version terminates early, treat its absent revisions as numerical `0`. As soon as a parsed chunk differs, return the comparison sign immediately; if both strings exhaust with all chunks equal, return 0.
- **Sample 1:**
  - **Input:** `version1 = "1.2", version2 = "1.10"`
  - **Output:** `-1`
  - **Explanation:** Revision 0 is 1 for both. Revision 1 is 2 vs 10; $2 < 10$, so return -1.
- **Sample 2:**
  - **Input:** `version1 = "1.01", version2 = "1.001"`
  - **Output:** `0`
  - **Explanation:** Ignoring leading zeros, "01" and "001" both represent integer 1.
- **Sample 3:**
  - **Input:** `version1 = "1.0", version2 = "1.0.0.0"`
  - **Output:** `0`
  - **Explanation:** Missing revisions in `version1` default to 0.

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine comparing two odometer readouts or software semantic versions. You line them up side-by-side. You read the first section between dots, convert it to a pure integer, and compare. If they are equal, you jump over the dot and read the next section. If one version string ends while the other continues, the missing vehicle simply has 0 mileage in that tier. You never need to allocate memory for the entire odometer; you only read the active digits between punctuation marks.

#### 3.2 The Naive Bottleneck & Redundant Computation
The naive approach calls `string.Split('.')` on both inputs and parses every string chunk with `int.Parse()`. This allocates two string arrays and numerous heap-allocated substring objects. If `version1` has 100 revisions and differs on the very first revision, the split-based approach wasted time and memory tokenizing 99 irrelevant chunks. The streaming two-pointer approach parses characters sequentially, allocating $0$ auxiliary heap memory and terminating on the very first mismatch.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Streaming Revision Accumulation Invariant:**
  At revision chunk $k$:
  $$v_1^{(k)} = \sum_{p = start}^{end} (version1[p] - '0') \times 10^{(end - p)}$$
  If cursor $i \ge len_1$, then $v_1^{(k)} = 0$.
- **Early-Termination Invariant:**
  Because version levels possess strict lexicographical priority:
  $$v_1^{(k)} \ne v_2^{(k)} \implies \text{Sign}(v_1^{(k)} - v_2^{(k)}) \text{ is final for the entire version}$$
  No downstream revision can ever alter the outcome.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
Streaming Two-Pointer Zero-Allocation Architecture:

version1: " 1 . 0 2 . 3 "
            ^
            i (parses chunk = 1, then skips '.')

version2: " 1 . 2 . 3 . 4 "
            ^
            j (parses chunk = 1, then skips '.')

Chunk 0: num1 = 1, num2 = 1  ===> Equal, advance past '.'
Chunk 1: num1 = 2, num2 = 2  ===> Equal, advance past '.'
Chunk 2: num1 = 3, num2 = 3  ===> Equal, advance past '.'
Chunk 3: i at end -> num1 = 0; j parses 4 -> num2 = 4 ===> 0 < 4 ===> Return -1
```

#### 3.5 State Transition Triggers & Decision Gates
1. **Loop Invariant Gate:** While `i < n1 || j < n2`, continue parsing.
2. **Chunk 1 Accumulator:** While `i < n1 && version1[i] != '.'`, execute `num1 = num1 * 10 + (version1[i] - '0'); i++`. Then skip the delimiter: `if (i < n1) i++`.
3. **Chunk 2 Accumulator:** While `j < n2 && version2[j] != '.'`, execute `num2 = num2 * 10 + (version2[j] - '0'); j++`. Then skip the delimiter: `if (j < n2) j++`.
4. **Comparison Gate:**
   - If `num1 < num2`, return -1 immediately.
   - If `num1 > num2`, return 1 immediately.
5. **Terminal Fallthrough:** If loop exits without differences, return 0.

#### 3.6 Concrete Step-by-Step State Trace
Input: `version1 = "1.0", version2 = "1.0.0.0"`

| Step | Cursor $i$ | Cursor $j$ | Parsed $num_1$ | Parsed $num_2$ | Comparison | Action |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **0** | $0 \to 1$ | $0 \to 1$ | `1` | `1` | $1 == 1$ | Skip '.', advance cursors |
| **1** | $2 \to 3$ | $2 \to 3$ | `0` | `0` | $0 == 0$ | Skip '.', advance cursors |
| **2** | $3 \ge n_1$| $4 \to 5$ | `0` (default) | `0` | $0 == 0$ | $i$ exhausted; skip '.', advance $j$ |
| **3** | $3 \ge n_1$| $6 \to 7$ | `0` (default) | `0` | $0 == 0$ | $j$ exhausted; loop ends |
| **End**| — | — | — | — | All equal | Return `0` |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1: Zero-Allocation Streaming Two-Pointer Parser (Optimal):** Operates directly over the raw string characters. $O(N_1 + N_2)$ time and $O(1)$ auxiliary space. Zero heap garbage generation.
- **Approach 2: String.Split with Array Padding:** Splits both strings into `string[]`, pads the shorter array with zeroes, and compares. Simpler syntax but creates garbage-collected arrays.

#### 4.2 Step-by-Step Natural Progression Flow
1. Initialize cursors `i = 0, j = 0`.
2. Enter while loop conditioned on `i < version1.Length || j < version2.Length`.
3. Reset accumulator variables `num1 = 0, num2 = 0`.
4. Extract digits for `version1` until `.` or end.
5. Extract digits for `version2` until `.` or end.
6. Compare `num1` and `num2`.
7. Advance past dot separators.
8. Return 0 if loop finishes without mismatch.

#### 4.3 Alternative Approaches Analysis
- **BigInteger Parsing:** If constraints indicated revisions could exceed 64-bit integers, parsing into `System.Numerics.BigInteger` or comparing strings by trimming leading zeroes and then comparing lengths followed by lexicographical order would be required. Under standard 32-bit constraints, integer arithmetic is vastly superior.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Approach | Time (Best) | Time (Avg) | Time (Worst) | Auxiliary Space | Output Space | Cache Locality | In-Place Mutability | Streaming Suitability |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **1. Streaming Two-Pointer** | $O(1)$ | $O(N_1 + N_2)$ | $O(N_1 + N_2)$ | $O(1)$ | $O(1)$ | Optimal | Non-mutating | Excellent (online) |
| **2. String.Split** | $O(N_1 + N_2)$ | $O(N_1 + N_2)$ | $O(N_1 + N_2)$ | $O(N_1 + N_2)$ | $O(1)$ | Moderate | Non-mutating | Poor (offline) |

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: Compare Version Numbers (LeetCode #165)
 * ============================================================================
 * Core Pattern      : Streaming Two-Pointer Parsing (Zero-Allocation Lexicographical Scan)
 * Time Complexity   : O(max(N1, N2)) - Linear single-pass parse
 * Space Complexity  : O(1) Auxiliary - Zero heap allocations; scalar cursors only
 * Selection Rule    : High-throughput API gateway & version negotiation services
 * Defensive Traps   : Missing revision levels must default to 0; handle integer overflow if untrusted
 * ============================================================================
 */

using System;

public class Solution
{
    /// <summary>
    /// Compares two version strings revision by revision.
    /// Employs streaming two-pointer parsing to achieve O(1) auxiliary memory.
    /// </summary>
    /// <param name="version1">First version string.</param>
    /// <param name="version2">Second version string.</param>
    /// <returns>-1 if v1 < v2; 1 if v1 > v2; 0 if equal.</returns>
    public int CompareVersion(string version1, string version2)
    {
        int n1 = version1?.Length ?? 0;
        int n2 = version2?.Length ?? 0;

        int i = 0;
        int j = 0;

        // Continue until both version strings are completely parsed
        while (i < n1 || j < n2)
        {
            long num1 = 0;
            // Parse numerical chunk for version1
            while (i < n1 && version1[i] != '.')
            {
                num1 = num1 * 10 + (version1[i] - '0');
                i++;
            }

            long num2 = 0;
            // Parse numerical chunk for version2
            while (j < n2 && version2[j] != '.')
            {
                num2 = num2 * 10 + (version2[j] - '0');
                j++;
            }

            // Early Termination Gate: Immediate resolution on chunk divergence
            if (num1 < num2)
            {
                return -1;
            }
            if (num1 > num2)
            {
                return 1;
            }

            // Skip delimiter '.' for version1 if not at end
            if (i < n1 && version1[i] == '.')
            {
                i++;
            }

            // Skip delimiter '.' for version2 if not at end
            if (j < n2 && version2[j] == '.')
            {
                j++;
            }
        }

        return 0;
    }
}
```

---

### 6. Senior Pitfalls & Defensive Traps
- **Integer Overflow on Giant Revisions:** While standard LeetCode tests fit in 32-bit integers, production version strings (such as build hashes or timestamp-based semantic versions like `20260912221500`) can easily overflow a standard 32-bit `int`. Using `long` (or treating oversized tokens via string length comparison) provides defensive safety.
- **Skipping Empty Chunk Delimiters:** Notice that after parsing digits, we check `if (i < n1 && version1[i] == '.') i++;`. If written without checking bounds or assuming both strings have a dot, index out-of-bounds exceptions will occur.
- **Leading Zero Equivalence vs String Equality:** Directly comparing string representations of revisions fails because `"01" != "001"`. Revisions must be normalized mathematically or stripped of leading zeroes before comparing.

---

## 147. Flatten Binary Tree to Linked List (LeetCode #114)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#binary-tree` `#linked-list` `#morris-traversal` `#in-place` `#dfs` |
| **LeetCode Link** | [Flatten Binary Tree to Linked List](https://leetcode.com/problems/flatten-binary-tree-to-linked-list/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given the `root` of a binary tree, flatten the tree into a "linked list" in-place:
  - The "linked list" should use the same `TreeNode` class where the `right` child pointer points to the next node in the list and the `left` child pointer is always `null`.
  - The "linked list" should be in the same order as a pre-order traversal of the binary tree.
- **Assumptions & Contracts:**
  - Mutation must occur strictly in-place; do not instantiate new `TreeNode` objects.
  - Pre-order traversal order: $\text{Node} \to \text{Left Subtree} \to \text{Right Subtree}$.
  - The flattened list is rooted at the original `root`.
- **Key Constraints:**
  - The number of nodes in the tree is in the range $[0, 2000]$.
  - $-100 \le Node.val \le 100$.
- **Senior Edge Cases to Defend:**
  - **Null Tree (`root == null`):** Immediate no-op return.
  - **Single Node:** No changes; left child is already null.
  - **Left-Skewed Tree:** Every node has only a left child; must flip all left children to right without losing connections.
  - **Right-Skewed Tree:** Already flattened; algorithm must detect and complete with $O(N)$ minimal work.
  - **Full Binary Tree:** Complex splicing where left subtree tails must attach to right subtree heads.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Pre-order traversal visits the current node, then its entire left subtree, then its right subtree. This means the *last* node visited in the left subtree (the rightmost node of `curr.left`) must immediately precede `curr.right`! By splicing `curr.right` onto the rightmost node of `curr.left`, moving `curr.left` to `curr.right`, and setting `curr.left = null`, we flatten the tree in $O(1)$ auxiliary memory without recursion or an auxiliary stack (Morris-style splicing).
- **Sample 1:**
  - **Input:** `root = [1, 2, 5, 3, 4, null, 6]`
  - **Output:** `[1, null, 2, null, 3, null, 4, null, 5, null, 6]`
- **Sample 2:**
  - **Input:** `root = []`
  - **Output:** `[]`
- **Sample 3:**
  - **Input:** `root = [0]`
  - **Output:** `[0]`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine hanging a folded paper mobile into a single straight vertical string. Whenever you encounter a left branch hanging from the current joint, you look down to the very lowest tip of that left branch. You detach the right branch from the current joint and tape it to the tip of that left branch. Then you swing the entire left branch over to hang straight down from the right joint, making sure the left side is now empty. Now you step down to the next node on the right and repeat.

#### 3.2 The Naive Bottleneck & Redundant Computation
A naive approach performs a standard pre-order DFS, collects all nodes into a `List<TreeNode>`, and then iterates through the list re-wiring pointers (`list[i].left = null; list[i].right = list[i + 1]`). This uses $O(N)$ extra heap memory. A recursive reverse pre-order DFS (Right $\to$ Left $\to$ Root) uses $O(H)$ stack frames. The Morris-style threading approach achieves strictly $O(1)$ auxiliary memory by utilizing the tree's own null pointers to establish connections.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Pre-Order Tail Predecessor Invariant:**
  In a pre-order traversal:
  $$\text{Traversal}(curr) = [curr] \circ \text{Traversal}(curr.left) \circ \text{Traversal}(curr.right)$$
  The very first node of $\text{Traversal}(curr.right)$ must be visited immediately after the *final* node of $\text{Traversal}(curr.left)$.
  In any binary tree, the final node of the left subtree visited in pre-order is its **rightmost descendant**:
  $$\text{tail}(curr.left) = \text{node } p \text{ reached by } p = curr.left \text{ then } p = p.right \text{ while } p.right \ne \text{null}$$
  Therefore, setting $\text{tail}(curr.left).right \leftarrow curr.right$ guarantees pre-order continuity!

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
Morris Pre-Order Splicing Architecture:

        1 (curr)                         1 (curr)
       / \                                \
      2   5                                2
     / \   \        ===========>          / \
    3   4   6                            3   4
                                              \
                                               5 (original curr.right attached to tail of left)
                                                \
                                                 6
Then move curr.left to curr.right:
1 -> 2 -> 3 -> 4 -> 5 -> 6
```

#### 3.5 State Transition Triggers & Decision Gates
1. Set `curr = root`.
2. While `curr != null`:
   - **Branch Gate:** If `curr.left != null`:
     - Find the rightmost node of `curr.left`:
       `TreeNode predecessor = curr.left;`
       `while (predecessor.right != null) predecessor = predecessor.right;`
     - Splice: `predecessor.right = curr.right;`
     - Re-wire: `curr.right = curr.left;`
     - Nullify: `curr.left = null;`
   - **Advance Cursor:** `curr = curr.right`.

#### 3.6 Concrete Step-by-Step State Trace
Input: `root = [1, 2, 5, 3, 4, null, 6]`

| Step | `curr.val` | `curr.left != null` | Rightmost Predecessor of `curr.left` | Action Taken | Resulting Right Spine from `curr` |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **1** | `1` | Yes (`2`) | Node `4` | `4.right = 5; 1.right = 2; 1.left = null` | $1 \to 2 \dots$ |
| **2** | `2` | Yes (`3`) | Node `3` | `3.right = 4; 2.right = 3; 2.left = null` | $1 \to 2 \to 3 \dots$ |
| **3** | `3` | No | None | Advance `curr = curr.right` (Node `4`) | $1 \to 2 \to 3 \to 4 \dots$ |
| **4** | `4` | No | None | Advance `curr = curr.right` (Node `5`) | $1 \to 2 \to 3 \to 4 \to 5 \dots$ |
| **5** | `5` | No | None | Advance `curr = curr.right` (Node `6`) | $1 \to 2 \to 3 \to 4 \to 5 \to 6$ |
| **6** | `6` | No | None | Advance `curr = curr.right` (`null`) | Terminate |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1: Morris In-Place Splicing (Optimal $O(1)$ Space):** Restructures pointers iteratively without allocating stack frames or collections. Ideal for systems with restricted stack memory.
- **Approach 2: Reverse Pre-Order Recursive DFS ($O(H)$ Stack):** Traverses `Right -> Left -> Root` maintaining a `prev` pointer. Beautifully concise, but consumes $O(H)$ stack space ($O(N)$ worst case for skewed trees).

#### 4.2 Step-by-Step Natural Progression Flow
1. Handle base case `root == null`.
2. Maintain pointer `curr = root`.
3. If `curr.left` exists, find its rightmost descendant `predecessor`.
4. Splice `curr.right` onto `predecessor.right`.
5. Shift `curr.left` into `curr.right` and wipe `curr.left`.
6. Advance `curr` along `curr.right`.

#### 4.3 Alternative Approaches Analysis
- **Queue/List Collection:** Performing pre-order traversal into `List<TreeNode>` and rewiring in a second pass. Rejected in senior interviews because it violates the in-place constraint.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Approach | Time (Best) | Time (Avg) | Time (Worst) | Auxiliary Space | Output Space | Cache Locality | In-Place Mutability | Streaming Suitability |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **1. Morris Splicing** | $O(N)$ | $O(N)$ | $O(N)$ | $O(1)$ | $O(1)$ | High | Yes (In-place) | Offline |
| **2. Reverse DFS** | $O(N)$ | $O(N)$ | $O(N)$ | $O(H)$ | $O(1)$ | Moderate | Yes (In-place) | Offline |
| **3. List Extraction** | $O(N)$ | $O(N)$ | $O(N)$ | $O(N)$ | $O(1)$ | High | Creates List | Offline |

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: Flatten Binary Tree to Linked List (LeetCode #114)
 * ============================================================================
 * Core Pattern      : Morris-Style Splicing (Constant Auxiliary Space)
 * Time Complexity   : O(N) - Each node/edge visited at most twice
 * Space Complexity  : O(1) Auxiliary - Zero recursion stack, zero heap allocation
 * Selection Rule    : Mandatory for memory-constrained embedded environments
 * Defensive Traps   : Must nullify curr.left after shifting to prevent cycles / invalid trees
 * ============================================================================
 */

using System;

/// <summary>
/// Definition for a binary tree node.
/// </summary>
public class TreeNode
{
    public int val;
    public TreeNode left;
    public TreeNode right;
    public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
    {
        this.val = val;
        this.left = left;
        this.right = right;
    }
}

public class Solution
{
    /// <summary>
    /// Flattens a binary tree into a right-oriented pre-order linked list in-place.
    /// Employs Morris splicing to achieve O(N) time and O(1) auxiliary space.
    /// </summary>
    /// <param name="root">Root node of the binary tree.</param>
    public void Flatten(TreeNode root)
    {
        TreeNode curr = root;

        while (curr != null)
        {
            // If current node has a left subtree, splice it into the right spine
            if (curr.left != null)
            {
                // Find the rightmost node of the left subtree (pre-order predecessor of curr.right)
                TreeNode predecessor = curr.left;
                while (predecessor.right != null)
                {
                    predecessor = predecessor.right;
                }

                // Splice original right subtree to the predecessor's right
                predecessor.right = curr.right;

                // Move entire left subtree to the right
                curr.right = curr.left;

                // Nullify left pointer to satisfy the single-linked list requirement
                curr.left = null;
            }

            // Move to the next node in the flattened pre-order chain
            curr = curr.right;
        }
    }
}

/// <summary>
/// Alternative Approach: Reverse Pre-Order Recursive DFS (Right -> Left -> Root).
/// Elegant and simple, but consumes O(H) call-stack memory.
/// </summary>
public class SolutionRecursiveDFS
{
    private TreeNode _prev = null;

    public void Flatten(TreeNode root)
    {
        if (root == null) return;

        // Post-order variant: Visit right first, then left, then node
        Flatten(root.right);
        Flatten(root.left);

        root.right = _prev;
        root.left = null;
        _prev = root;
    }
}
```

---

### 6. Senior Pitfalls & Defensive Traps
- **Forgetting to Nullify `curr.left`:** The problem contract explicitly dictates: *"the left child pointer is always null."* Failing to set `curr.left = null` leaves dangling left branches, violating the definition of the resulting linked list and causing LeetCode or consumers to fail validation.
- **Predecessor Search Loop Condition:** In Morris Traversal for in-order printing, the loop checks `while (predecessor.right != null && predecessor.right != curr)` to break temporary threads. But here, we are *permanently restructuring* the tree; the loop must simply check `while (predecessor.right != null)`. Adding the `!= curr` check is unnecessary and demonstrates a lack of conceptual clarity regarding permanent restructuring vs temporary traversal threading.
- **Stack Overflow in Deep Skewed Trees:** The recursive reverse DFS solution has $O(H)$ space complexity. For a degenerate linked-list tree with $N = 2000$ nodes, a recursive method pushes 2000 stack frames. The iterative Morris splicing approach operates with strictly $O(1)$ stack space.

---

---

## 148. Best Time to Buy and Sell Stock (LeetCode #121)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟢 Easy |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#array` `#dynamic-programming` `#prefix-min` `#one-pass` |
| **LeetCode Link** | [Best Time to Buy and Sell Stock](https://leetcode.com/problems/best-time-to-buy-and-sell-stock/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** You are given an array `prices` where `prices[i]` is the price of a given stock on the $i$-th day. You want to maximize your profit by choosing a single day to buy one stock and choosing a different day in the future to sell that stock. Return the maximum profit you can achieve. If you cannot achieve any profit, return 0.
- **Assumptions & Contracts:**
  - Transaction constraint: Exactly at most 1 buy transaction followed chronologically by 1 sell transaction.
  - Causality constraint: Selling on day $j$ requires buying on day $i$ where $i < j$.
  - Negative profits are not allowed; if all prices drop monotonically, return 0.
- **Key Constraints:**
  - $1 \le prices.Length \le 10^5$
  - $0 \le prices[i] \le 10^4$
- **Senior Edge Cases to Defend:**
  - **Monotonically Decreasing:** `[7, 6, 4, 3, 1]` must yield `0`.
  - **Monotonically Increasing:** `[1, 2, 3, 4, 5]` must yield `4` (buy day 0, sell day 4).
  - **Strictly Flat / Constant:** `[3, 3, 3, 3]` must yield `0`.
  - **Global Minimum Occurs on the Final Day:** `[3, 8, 1]` must yield `5` (buy at 3, sell at 8; the drop to 1 at the end cannot be used as a buy point for previous days).
  - **Singleton Array:** `[5]` must yield `0` without out-of-bounds access.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** At any given day $i$, the maximum profit attainable by selling on day $i$ is $prices[i] - \min_{0 \le j < i}(prices[j])$. By keeping a running prefix minimum `minPrice`, we can evaluate every possible selling day in a single $O(N)$ pass, updating `maxProfit` monotonically.
- **Sample 1:**
  - **Input:** `prices = [7, 1, 5, 3, 6, 4]`
  - **Output:** `5`
  - **Explanation:** Buy on day 1 (price = 1) and sell on day 4 (price = 6), profit = $6 - 1 = 5$.
- **Sample 2:**
  - **Input:** `prices = [7, 6, 4, 3, 1]`
  - **Output:** `0`
  - **Explanation:** In this case, no transactions are done and max profit = 0.

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine surfing down a price chart. To make the most money when selling today, you wish you had bought at the deepest valley you passed earlier on your journey. As you walk forward day by day, you record the lowest valley you have ever seen (`minPrice`). At every new day, you calculate the elevation gain from that lowest valley to your current height. If today's height beats your historical highest gain, you update your record (`maxProfit`). If today's price is even lower than your lowest valley, you update the valley for all future days.

#### 3.2 The Naive Bottleneck & Redundant Computation
A brute-force solution checks all pairs $(i, j)$ with $0 \le i < j < N$ and computes $prices[j] - prices[i]$, requiring $O(N^2)$ comparisons. This re-evaluates previously visited minimums repeatedly. Because the optimal buy point for any sell day $j$ is strictly independent of future days, the minimum can be tracked incrementally in $O(1)$ amortized time per step, collapsing $O(N^2)$ to $O(N)$.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Prefix Minimum Decomposition Invariant:**
  $$\text{maxProfit} = \max_{0 \le j < N} \left( prices[j] - \min_{0 \le i \le j}(prices[i]) \right)$$
- **Online State Machine Invariant:**
  Let state at step $k$ be $S_k = (\mu_k, \pi_k)$ where:
  $$\mu_k = \min(\mu_{k-1}, prices[k])$$
  $$\pi_k = \max(\pi_{k-1}, prices[k] - \mu_k)$$
  This state transitions in $O(1)$ time per day, maintains causality naturally, and requires only two scalar registers.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
Prefix Minimum & Optimal Selling Horizon:

Days:      [0]    [1]    [2]    [3]    [4]    [5]
Prices:     7      1      5      3      6      4
            ^      ^
            |      +-- New lowest valley (minPrice = 1)
            +-- Initial minPrice = 7

At Day 2 (Price 5): Profit = 5 - 1 = 4 (New maxProfit = 4)
At Day 3 (Price 3): Profit = 3 - 1 = 2 (maxProfit remains 4)
At Day 4 (Price 6): Profit = 6 - 1 = 5 (New maxProfit = 5)
At Day 5 (Price 4): Profit = 4 - 1 = 3 (maxProfit remains 5)

Final Output: 5
```

#### 3.5 State Transition Triggers & Decision Gates
1. **Valley Update Gate:** If $prices[i] < minPrice$, update $minPrice = prices[i]$.
2. **Profit Update Gate:** Else if $prices[i] - minPrice > maxProfit$, update $maxProfit = prices[i] - minPrice$.
3. **Loop Boundary:** Iterate $i$ from 0 to $N - 1$.

#### 3.6 Concrete Step-by-Step State Trace
Input: `prices = [7, 1, 5, 3, 6, 4]`

| Day ($i$) | Price (`prices[i]`) | `minPrice` Before | Decision / Branch | `minPrice` After | Current Potential Profit | `maxProfit` After |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **0** | `7` | $\infty$ | $7 < \infty \implies$ New Min | `7` | $7 - 7 = 0$ | `0` |
| **1** | `1` | `7` | $1 < 7 \implies$ New Min | `1` | $1 - 1 = 0$ | `0` |
| **2** | `5` | `1` | $5 > 1 \implies$ Check Profit | `1` | $5 - 1 = 4$ | `4` |
| **3** | `3` | `1` | $3 > 1 \implies$ Check Profit | `1` | $3 - 1 = 2$ | `4` |
| **4** | `6` | `1` | $6 > 1 \implies$ Check Profit | `1` | $6 - 1 = 5$ | `5` |
| **5** | `4` | `1` | $4 > 1 \implies$ Check Profit | `1` | $4 - 1 = 3$ | `5` |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1: One-Pass Prefix Minimum (Optimal & Standard):** Single pass, $O(N)$ time, $O(1)$ space, zero heap allocation, cache-friendly contiguous memory scanning.
- **Approach 2: Kadane's Maximum Subarray Transformation:** Transform `prices` into daily deltas $\Delta_i = prices[i] - prices[i-1]$. The maximum profit corresponds exactly to the maximum contiguous subarray sum of deltas. Insightful for interviews to connect greedy buy/sell logic to Kadane's DP.

#### 4.2 Step-by-Step Natural Progression Flow
1. Handle null or single-element arrays (return 0).
2. Initialize `minPrice = prices[0]` and `maxProfit = 0`.
3. Loop through prices from index 1 to $N - 1$.
4. At each price, update `minPrice` if current price is lower; otherwise evaluate profit and update `maxProfit`.
5. Return `maxProfit`.

#### 4.3 Alternative Approaches Analysis
- **Brute Force Pairs:** $O(N^2)$ time. For $N = 10^5$, $N^2 = 10^{10}$ operations, causing Time Limit Exceeded (TLE).

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Approach | Time (Best) | Time (Avg) | Time (Worst) | Auxiliary Space | Output Space | Cache Locality | In-Place Mutability | Streaming Suitability |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **1. Prefix Minimum** | $O(N)$ | $O(N)$ | $O(N)$ | $O(1)$ | $O(1)$ | Optimal (L1 cache) | Non-mutating | Excellent (online) |
| **2. Kadane's Delta** | $O(N)$ | $O(N)$ | $O(N)$ | $O(1)$ | $O(1)$ | Optimal | Non-mutating | Excellent (online) |
| **3. Brute Force Pairs**| $O(N^2)$ | $O(N^2)$ | $O(N^2)$ | $O(1)$ | $O(1)$ | Moderate | Non-mutating | Poor |

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: Best Time to Buy and Sell Stock (LeetCode #121)
 * ============================================================================
 * Core Pattern      : Prefix Extremum Tracking / Online Dynamic Programming
 * Time Complexity   : O(N) - Single linear pass across price array
 * Space Complexity  : O(1) Auxiliary - Two scalar CPU registers
 * Selection Rule    : Standard template for 1-transaction financial optimization
 * Defensive Traps   : Must handle N < 2 gracefully; minPrice initialized to prices[0]
 * ============================================================================
 */

using System;

public class Solution
{
    /// <summary>
    /// Computes the maximum profit from at most one buy and one sell transaction.
    /// Employs a single-pass prefix minimum tracking invariant.
    /// </summary>
    /// <param name="prices">Array of daily stock prices.</param>
    /// <returns>Maximum achievable non-negative profit.</returns>
    public int MaxProfit(int[] prices)
    {
        // Guard Clause: At least two days are required to buy and sell
        if (prices == null || prices.Length <= 1)
        {
            return 0;
        }

        int minPrice = prices[0];
        int maxProfit = 0;

        for (int i = 1; i < prices.Length; i++)
        {
            int currentPrice = prices[i];

            if (currentPrice < minPrice)
            {
                // Invariant: Found a deeper valley; update buy benchmark for all future days
                minPrice = currentPrice;
            }
            else
            {
                // Invariant: Potential sell point; evaluate profit against historical minimum
                int currentProfit = currentPrice - minPrice;
                if (currentProfit > maxProfit)
                {
                    maxProfit = currentProfit;
                }
            }
        }

        return maxProfit;
    }
}

/// <summary>
/// Theoretical Alternative: Kadane's Maximum Subarray Sum over Daily Price Deltas.
/// Demonstrates the mathematical duality between prefix minimums and contiguous subarray maximums.
/// </summary>
public class SolutionKadaneDuality
{
    public int MaxProfit(int[] prices)
    {
        if (prices == null || prices.Length <= 1) return 0;

        int maxSoFar = 0;
        int currentMax = 0;

        for (int i = 1; i < prices.Length; i++)
        {
            int delta = prices[i] - prices[i - 1];
            currentMax = Math.Max(0, currentMax + delta);
            maxSoFar = Math.Max(maxSoFar, currentMax);
        }

        return maxSoFar;
    }
}
```

---

### 6. Senior Pitfalls & Defensive Traps
- **The Causality Violation Trap:** A classic beginner bug is calculating `max(prices) - min(prices)`. If the global maximum occurs *before* the global minimum (e.g., `prices = [9, 1, 2]`), `max - min = 9 - 1 = 8`, but you cannot sell on day 0 and buy on day 1! Chronological causality is strictly non-negotiable.
- **Initialization with 0 for `minPrice`:** Initializing `minPrice = 0` causes `minPrice` to immediately swallow any positive price, rendering `prices[i] < minPrice` impossible if prices are strictly non-negative. Always initialize `minPrice` with `prices[0]` or `int.MaxValue`.
- **Negative Profit Leaks:** If the market continually plunges (e.g., `[5, 4, 3, 2, 1]`), the optimal move is to make zero transactions. `maxProfit` must be initialized to `0` and never drop below 0.

---

## 149. Add Two Numbers II (LeetCode #445)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | 🔥 High |
| **Pattern Tags** | `#linked-list` `#stack` `#math` `#recursion` |
| **LeetCode Link** | [Add Two Numbers II](https://leetcode.com/problems/add-two-numbers-ii/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** You are given two non-empty linked lists representing two non-negative integers. The most significant digit comes first and each of their nodes contains a single digit. Add the two numbers and return the sum as a linked list.
- **Assumptions & Contracts:**
  - Most Significant Digit (MSD) is at the head of each list.
  - No leading zeros in numbers except the number 0 itself.
  - Follow-up Contract: **Do not reverse the input lists** (immutable input requirement).
- **Key Constraints:**
  - The number of nodes in each linked list is in the range $[1, 100]$.
  - $0 \le Node.val \le 9$.
- **Senior Edge Cases to Defend:**
  - **Asymmetric List Lengths:** `[7, 2, 4, 3]` + `[5, 6, 4]` = `[7, 8, 0, 7]`.
  - **Cascading Carry Ripple Across All Digits:** `[9, 9, 9]` + `[1]` = `[1, 0, 0, 0]` (creates a new MSD head node).
  - **Addition with Zero:** `[0]` + `[0]` = `[0]`.
  - **Single Digit Operations:** `[5]` + `[5]` = `[1, 0]`.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Addition naturally proceeds from Least Significant Digit (LSD) to Most Significant Digit (MSD). Because the inputs are presented in reverse (MSD first) and mutative reversal is forbidden, we use two LIFO Stacks to reverse the traversal order non-destructively. Popping from both stacks extracts digits in LSD-first order, sums them with a running `carry`, and prepends new nodes to the result list head.
- **Sample 1:**
  - **Input:** `l1 = [7, 2, 4, 3], l2 = [5, 6, 4]`
  - **Output:** `[7, 8, 0, 7]`
- **Sample 2:**
  - **Input:** `l1 = [2, 4, 3], l2 = [5, 6, 4]`
  - **Output:** `[8, 0, 7]`
- **Sample 3:**
  - **Input:** `l1 = [0], l2 = [0]`
  - **Output:** `[0]`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine two stacks of plates, each with a digit written on it, where the top plate is the highest power of 10 (thousands, hundreds, etc.). You cannot reverse the stacks because they are glued down. To add them like elementary school addition, you pick up the plates from each stack one-by-one and place them into two fresh bins. Now, the ones-place digits are resting right on top of both bins! You pop digits from the top of the bins, add them with any carry, and construct the answer chain from the bottom up by prepending newly minted answer nodes.

#### 3.2 The Naive Bottleneck & Redundant Computation
Converting the linked lists into integers (`long` or `BigInteger`) and adding them fails when the list length exceeds primitive integer capacity (e.g., 100 digits exceeds 64-bit `ulong` by 80 orders of magnitude). Reversing the input lists in-place is fast ($O(1)$ space), but mutates caller data, violating thread safety and API immutability contracts. Dual explicit stacks provide an $O(N_1 + N_2)$ non-destructive solution.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **LIFO Decimal Positional Invariant:**
  By pushing all nodes from $L_1$ and $L_2$ onto stacks $S_1$ and $S_2$:
  $$\text{Top}(S_1) = \text{LSD}(L_1), \quad \text{Top}(S_2) = \text{LSD}(L_2)$$
- **Carry Propagation Invariant:**
  At step $k$ from right to left:
  $$\text{sum}_k = d_1^{(k)} + d_2^{(k)} + \text{carry}_{k-1}$$
  $$\text{digit}_k = \text{sum}_k \pmod{10}$$
  $$\text{carry}_k = \lfloor \text{sum}_k / 10 \rfloor$$
  Prepending $\text{digit}_k$ to the result head builds the output in correct MSD-first order:
  $$\text{head}_{\text{new}} = \text{new ListNode}(\text{digit}_k, \text{head}_{\text{prev}})$$

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
Dual Stack Non-Destructive Addition Architecture:

Input L1: [ 7 ] -> [ 2 ] -> [ 4 ] -> [ 3 ]
Input L2: [ 5 ] -> [ 6 ] -> [ 4 ]

Push to Stacks:
Stack 1 (LIFO): [ 3, 4, 2, 7 ] (Top is 3)
Stack 2 (LIFO): [ 4, 6, 5 ]    (Top is 4)

Pops & Carry Addition:
1. Pop 3 + 4 + carry 0 = 7  ---> carry = 0, Node(7) -> null
2. Pop 4 + 6 + carry 0 = 10 ---> carry = 1, Node(0) -> Node(7)
3. Pop 2 + 5 + carry 1 = 8  ---> carry = 0, Node(8) -> Node(0) -> Node(7)
4. Pop 7 + 0 + carry 0 = 7  ---> carry = 0, Node(7) -> Node(8) -> Node(0) -> Node(7)

Result Head: [ 7 ] -> [ 8 ] -> [ 0 ] -> [ 7 ]
```

#### 3.5 State Transition Triggers & Decision Gates
1. **Ingestion:** Push all nodes of $l_1$ onto `s1`; push all nodes of $l_2$ onto `s2`.
2. **Summation Gate:** Loop while `s1.Count > 0 || s2.Count > 0 || carry != 0`.
3. **Digit Extraction:**
   - $d_1 = s_1.\text{Count} > 0 \ ? \ s_1.\text{Pop}() : 0$
   - $d_2 = s_2.\text{Count} > 0 \ ? \ s_2.\text{Pop}() : 0$
4. **Prepend Node:**
   - Create node with value $(d_1 + d_2 + carry) \% 10$.
   - Point `newNode.next = head`.
   - Set `head = newNode`.
   - Update `carry = (d1 + d2 + carry) / 10`.

#### 3.6 Concrete Step-by-Step State Trace
Input: `l1 = [9, 9]`, `l2 = [1]`

| Step | Stack 1 Top | Stack 2 Top | Carry In | Total Sum | New Digit ($sum \% 10$) | Carry Out | Result Head Chain |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **0** | `[9, 9]` | `[1]` | `0` | — | — | — | `null` |
| **1** | Pop `9` | Pop `1` | `0` | $9 + 1 + 0 = 10$ | `0` | `1` | `[0] -> null` |
| **2** | Pop `9` | Empty (`0`) | `1` | $9 + 0 + 1 = 10$ | `0` | `1` | `[0] -> [0] -> null` |
| **3** | Empty | Empty | `1` | $0 + 0 + 1 = 1$ | `1` | `0` | `[1] -> [0] -> [0] -> null` |
| **End**| Empty | Empty | `0` | Terminate | — | — | Return `[1, 0, 0]` |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1: Dual Explicit Stacks (Optimal Non-Destructive):** Reverses digit processing order without mutating the input linked lists. Simple to reason about, eliminates recursion stack limits, and operates in $O(N_1 + N_2)$ time and space.
- **Approach 2: Length Counting + Recursive Digit Alignment:** Count lengths $N_1$ and $N_2$. Recursively traverse both lists, skipping $N_1 - N_2$ nodes in the longer list, adding corresponding digits on unwind, and bubbling carry up the call stack. Achieves $O(1)$ auxiliary heap space, but consumes $O(\max(N_1, N_2))$ call stack memory.

#### 4.2 Step-by-Step Natural Progression Flow
1. Handle null cases (if $l_1 == null$ return $l_2$; if $l_2 == null$ return $l_1$).
2. Push all values of $l_1$ into `Stack<int> s1`.
3. Push all values of $l_2$ into `Stack<int> s2`.
4. Initialize `carry = 0`, `ListNode head = null`.
5. Enter loop while `s1.Count > 0 || s2.Count > 0 || carry != 0`.
6. Compute sum, create new node, prepend to `head`.
7. Return `head`.

#### 4.3 Alternative Approaches Analysis
- **Destructive List Reversal:** Inverting $l_1$ and $l_2$ using standard 3-pointer reversal, adding via LeetCode #2 logic, and reversing the result list. While $O(1)$ auxiliary space, it violates the non-destructive contract of production libraries where inputs may be shared across threads.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Approach | Time (Best) | Time (Avg) | Time (Worst) | Auxiliary Space | Output Space | Cache Locality | In-Place Mutability | Streaming Suitability |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **1. Dual Stacks** | $O(N_1 + N_2)$ | $O(N_1 + N_2)$ | $O(N_1 + N_2)$ | $O(N_1 + N_2)$ | $O(\max(N_1, N_2))$ | Moderate | Non-destructive | Offline |
| **2. Recursive Alignment** | $O(N_1 + N_2)$ | $O(N_1 + N_2)$ | $O(N_1 + N_2)$ | $O(\max(N_1, N_2))$ stack | $O(\max(N_1, N_2))$ | Moderate | Non-destructive | Offline |
| **3. In-Place Reversal** | $O(N_1 + N_2)$ | $O(N_1 + N_2)$ | $O(N_1 + N_2)$ | $O(1)$ | $O(\max(N_1, N_2))$ | High | Destructive | Offline |

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: Add Two Numbers II (LeetCode #445)
 * ============================================================================
 * Core Pattern      : LIFO Stack Digit Inversion (Non-Destructive Addition)
 * Time Complexity   : O(N1 + N2) - Linear pass to push, linear pass to pop & sum
 * Space Complexity  : O(N1 + N2) Auxiliary - Two explicit stacks for digit reversal
 * Selection Rule    : Mandatory when input list reversal is forbidden by contract
 * Defensive Traps   : Must continue loop if carry != 0 after stacks are empty
 * ============================================================================
 */

using System;
using System.Collections.Generic;

/// <summary>
/// Definition for singly-linked list.
/// </summary>
public class ListNode
{
    public int val;
    public ListNode next;
    public ListNode(int val = 0, ListNode next = null)
    {
        this.val = val;
        this.next = next;
    }
}

public class Solution
{
    /// <summary>
    /// Adds two numbers represented by linked lists with most significant digit first.
    /// Preserves input immutability by using dual explicit stacks.
    /// </summary>
    /// <param name="l1">Head node of first number list.</param>
    /// <param name="l2">Head node of second number list.</param>
    /// <returns>Head node of the sum linked list.</returns>
    public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
    {
        if (l1 == null) return l2;
        if (l2 == null) return l1;

        var stack1 = new Stack<int>();
        var stack2 = new Stack<int>();

        // Ingest digits into stacks to achieve LIFO (LSD-first) extraction
        for (ListNode curr = l1; curr != null; curr = curr.next)
        {
            stack1.Push(curr.val);
        }

        for (ListNode curr = l2; curr != null; curr = curr.next)
        {
            stack2.Push(curr.val);
        }

        ListNode head = null;
        int carry = 0;

        // Process until all digits and any residual carry are fully resolved
        while (stack1.Count > 0 || stack2.Count > 0 || carry != 0)
        {
            int sum = carry;

            if (stack1.Count > 0)
            {
                sum += stack1.Pop();
            }

            if (stack2.Count > 0)
            {
                sum += stack2.Pop();
            }

            // Extract single digit and update carry
            int digit = sum % 10;
            carry = sum / 10;

            // Prepend new node to the front of the resulting chain
            var newNode = new ListNode(digit, head);
            head = newNode;
        }

        return head;
    }
}
```

---

### 6. Senior Pitfalls & Defensive Traps
- **The Residual Carry Drop:** The most frequent trap is writing the while loop condition as `while (stack1.Count > 0 || stack2.Count > 0)`. If $99 + 1 = 100$, both stacks become empty while `carry == 1`. If the carry is not checked in the loop condition, the leading `1` digit is dropped, producing `00` instead of `100`!
- **Data Mutation Violation:** In production systems, reversing input linked lists (`l1 = Reverse(l1)`) creates subtle race conditions if other threads or services are currently reading from those shared nodes. Always honor non-destructive API contracts unless explicitly authorized to mutate.
- **Recursion Depth Limits on Large Numbers:** While recursive length-alignment approaches avoid explicit stack allocations, an input list with $10^5$ digits will cause a fatal `StackOverflowException`. Explicit heap-based stacks (`Stack<int>`) scale safely across arbitrarily long digit sequences.

---

---

## 150. Simplify Path (LeetCode #71)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#string` `#stack` `#simulation` `#unix-filesystem` |
| **LeetCode Link** | [Simplify Path](https://leetcode.com/problems/simplify-path/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given an absolute path for a Unix-style file system, which begins with a slash `'/'`, transform this path into its simplified canonical path.
  - A period `.` refers to the current directory.
  - A double period `..` refers to the directory up a level (parent directory).
  - Multiple consecutive slashes such as `//` and `///` are treated as a single slash `/`.
  - Any sequence of periods other than `.` and `..` (such as `...` or `....`) are treated as valid file or directory names.
  - The canonical path must start with a single slash `/`.
  - Any two directories must be separated by a single slash `/`.
  - The path must not end with a trailing `/`, unless it is the root directory.
  - The path must not contain any `.` or `..` segments.
- **Assumptions & Contracts:**
  - Input is always an absolute path (starts with `/`).
  - Traversing above the root directory via `..` is a no-op; root remains root.
- **Key Constraints:**
  - $1 \le path.Length \le 3000$
  - `path` consists of English letters, digits, period `'.'`, slash `'/'`, and underscore `'_'`.
  - `path` is a valid absolute Unix path.
- **Senior Edge Cases to Defend:**
  - **Popping Past Root:** `"/../"` must evaluate to `"/"` (cannot escape root directory).
  - **Multiple Consecutive Slashes:** `"/home//foo/"` must normalize to `"/home/foo"`.
  - **Dot Directories as Names:** `"/.../a/../b/c/../d/./"` must evaluate to `"/.../b/d"`.
  - **Empty Intermediate Tokens:** Leading, trailing, and duplicate slashes create empty tokens that must be skipped.
  - **Pure Root Path:** `"/"` must evaluate to `"/"`.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Tokenize the path by the delimiter `'/'`. Treat the directory hierarchy as a LIFO stack. For each token:
  - If token is `""` (empty from consecutive slashes) or `"."` (current dir): do nothing.
  - If token is `".."` (parent dir): pop from stack if stack is non-empty.
  - Otherwise (valid name): push token onto stack.
  Finally, join all elements in the stack from bottom to top separated by `'/'` and prepend a root `'/'`. If the stack is empty, return `"/"`.
- **Sample 1:**
  - **Input:** `path = "/home/"`
  - **Output:** `"/home"`
- **Sample 2:**
  - **Input:** `path = "/../"`
  - **Output:** `"/"`
- **Sample 3:**
  - **Input:** `path = "/home//foo/"`
  - **Output:** `"/home/foo"`
- **Sample 4:**
  - **Input:** `path = "/.../a/../b/c/../d/./"`
  - **Output:** `"/.../b/d"`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine a hiker navigating a trail system with a GPS breadcrumb log. Every time the hiker enters a new canyon (`"foo"`), the GPS appends that canyon name to the trail log. If the hiker consults their compass and rests (`"."`), the log doesn't change. If the hiker retraces their steps back to the previous fork (`".."`), the GPS removes the most recent canyon from the log. If the hiker is already at base camp (root) and tries to retrace further back (`".."`), they simply stay at base camp. At the end of the day, the active breadcrumb trail is the canonical path.

#### 3.2 The Naive Bottleneck & Redundant Computation
Attempting string search-and-replace (`path.Replace("//", "/")` or regex replacement) is flawed and slow: replacing `"//"` in a loop takes $O(N^2)$ time due to repeated string scans and allocations, and it fails to properly account for tricky cases like `"/a/b/../../"`. Using a token stream evaluated via a stack achieves strictly $O(N)$ linear time and linear space.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Stack Invariant of Directory Depth:**
  At any token $k$, the stack contents $[d_0, d_1, \dots, d_{m-1}]$ represent the exact valid ancestral descent from root to the current active directory.
  - `token == ".."`:
    $$\text{Stack} \leftarrow \text{Stack}[0 \dots m - 2] \quad (\text{if } m > 0)$$
  - `token == "."` or `token == ""`:
    $$\text{Stack} \leftarrow \text{Stack} \quad (\text{no-op})$$
  - `token == name`:
    $$\text{Stack} \leftarrow \text{Stack} \cup \{ name \}$$

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
Unix Path Token Stack Lifecycle:

Input: "/a/./b/../../c/"
Tokens: ["", "a", ".", "b", "..", "..", "c", ""]

Token Action:
- ""   : Ignore
- "a"  : Push "a"          ---> Stack: ["a"]
- "."  : Ignore            ---> Stack: ["a"]
- "b"  : Push "b"          ---> Stack: ["a", "b"]
- ".." : Pop "b"           ---> Stack: ["a"]
- ".." : Pop "a"           ---> Stack: []
- "c"  : Push "c"          ---> Stack: ["c"]
- ""   : Ignore            ---> Stack: ["c"]

Reconstruction:
"/" + string.Join("/", Stack) ===> "/c"
```

#### 3.5 State Transition Triggers & Decision Gates
1. **Token Splitting:** Tokenize by `'/'` (or use streaming span slice without allocation).
2. **Token Classification Gate:**
   - Case 1: `string.IsNullOrEmpty(token) || token == "."` $\implies$ Continue (no-op).
   - Case 2: `token == ".."` $\implies$ If `stack.Count > 0`, pop top directory.
   - Case 3: Default $\implies$ Push `token`.
3. **Reconstruction Gate:**
   - Prepend `'/'`.
   - Append stack tokens joined by `'/'`. If stack is empty, canonical path is `"/"`.

#### 3.6 Concrete Step-by-Step State Trace
Input: `path = "/.../a/../b/c/../d/./"`

| Token Index | Token | Action | Stack State (Bottom -> Top) |
| :--- | :--- | :--- | :--- |
| **0** | `""` | Skip empty | `[]` |
| **1** | `"..."` | Valid dir name $\implies$ Push | `["..."]` |
| **2** | `"a"` | Push | `["...", "a"]` |
| **3** | `".."` | Pop `"a"` | `["..."]` |
| **4** | `"b"` | Push | `["...", "b"]` |
| **5** | `"c"` | Push | `["...", "b", "c"]` |
| **6** | `".."` | Pop `"c"` | `["...", "b"]` |
| **7** | `"d"` | Push | `["...", "b", "d"]` |
| **8** | `"."` | Skip current dir | `["...", "b", "d"]` |
| **9** | `""` | Skip trailing empty | `["...", "b", "d"]` |
| **Final** | — | Format: `'/' + Join('/', stack)` | `"/.../b/d"` |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1: Token List / Stack Simulation (Optimal Production Standard):** Using a `List<string>` as a vector stack allows direct in-order iteration during reconstruction, eliminating the need to reverse an explicit `Stack<string>` when building the output.
- **Approach 2: Zero-Allocation `ReadOnlySpan<char>` Parser:** Uses `Span<char>` slicing to identify tokens without allocating substring objects until output formatting. Ideal for high-performance file server kernels.

#### 4.2 Step-by-Step Natural Progression Flow
1. Guard check for empty or null path.
2. Split path by `'/'`.
3. Iterate tokens, filtering empty and `"."` segments.
4. Execute pops on `".."` if elements exist.
5. Push valid names into a `List<string>`.
6. Use `StringBuilder` or `string.Join` to produce `"/ + Join('/', list)"`.

#### 4.3 Alternative Approaches Analysis
- **Regex Substitution:** Replacing redundant slashes and traversing with regex matching. Unmaintainable, highly allocation-heavy, and susceptible to ReDoS (Regular Expression Denial of Service) vulnerabilities.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Approach | Time (Best) | Time (Avg) | Time (Worst) | Auxiliary Space | Output Space | Cache Locality | In-Place Mutability | Streaming Suitability |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **1. List Stack** | $O(N)$ | $O(N)$ | $O(N)$ | $O(N)$ | $O(N)$ | High | Non-mutating | Offline |
| **2. Span<char> Parser** | $O(N)$ | $O(N)$ | $O(N)$ | $O(N)$ tokens | $O(N)$ | Optimal | Non-mutating | Excellent |
| **3. Regex Iteration** | $O(N^2)$ | $O(N^2)$ | $O(N^2)$ | $O(N)$ | $O(N)$ | Poor | Non-mutating | Poor |

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: Simplify Path (LeetCode #71)
 * ============================================================================
 * Core Pattern      : LIFO Stack Directory Canonicalization (Unix Path Invariant)
 * Time Complexity   : O(N) - Linear single-pass tokenization and stack processing
 * Space Complexity  : O(N) Auxiliary - Directory token stack and StringBuilder
 * Selection Rule    : Standard solution for operating system virtual filesystem resolvers
 * Defensive Traps   : ".." when stack is empty must be safely ignored (cannot escape root);
 *                     Tokens like "..." are valid filenames, NOT navigation operators!
 * ============================================================================
 */

using System;
using System.Collections.Generic;
using System.Text;

public class Solution
{
    /// <summary>
    /// Simplifies a Unix-style absolute path to its canonical representation.
    /// Employs a List-backed stack to support efficient forward joining.
    /// </summary>
    /// <param name="path">Unix absolute file system path.</param>
    /// <returns>The normalized canonical path.</returns>
    public string SimplifyPath(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            return "/";
        }

        // Split on slash; consecutive slashes naturally yield empty tokens
        string[] tokens = path.Split('/');

        // Use List<string> as stack to enable linear forward traversal during reconstruction
        var stack = new List<string>(capacity: tokens.Length);

        foreach (string token in tokens)
        {
            // Gate 1: Ignore empty segments (from '//' or leading/trailing '/') and current dir '.'
            if (string.IsNullOrEmpty(token) || token == ".")
            {
                continue;
            }

            // Gate 2: Handle parent directory navigation '..'
            if (token == "..")
            {
                if (stack.Count > 0)
                {
                    stack.RemoveAt(stack.Count - 1); // Pop parent
                }
                // If stack is empty, attempting to go above root is a safe no-op
            }
            else
            {
                // Gate 3: Valid file/folder name (e.g. "foo", "...", "hidden_file")
                stack.Add(token);
            }
        }

        // Fast path for root
        if (stack.Count == 0)
        {
            return "/";
        }

        // Reconstruct canonical path
        var sb = new StringBuilder(capacity: path.Length);
        foreach (string dir in stack)
        {
            sb.Append('/').Append(dir);
        }

        return sb.ToString();
    }
}
```

---

### 6. Senior Pitfalls & Defensive Traps
- **The `"..."` False Positive Bug:** A prevalent trap is assuming any token containing multiple dots means "navigate parent directory". In Unix, `".."` is parent, `"."` is current, but `"..."` or `".hidden"` or `"...."` are 100% valid directory names! Never check `token.StartsWith("..")`; check exact equality `token == ".."`.
- **Using `Stack<T>` Instead of `List<T>` for Formatting:** If using `System.Collections.Generic.Stack<string>`, enumerating the stack yields elements in LIFO order (top-to-bottom, reverse path). You must reverse it or call `ToArray()` followed by `Array.Reverse()`. Using `List<string>` allows `Add()` as push, `RemoveAt(Count - 1)` as pop, and direct forward iteration.
- **Root Slash Drop on Empty Stack:** If the path is `"/../"`, after popping nothing, the stack is empty. If your formatter simply joins tokens with `'/'`, it would return `""`. Defend with an explicit check: if `stack.Count == 0`, return `"/"`.

---

## 151. Text Justification (LeetCode #68)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🔴 Hard |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#string` `#greedy` `#simulation` `#two-pointers` |
| **LeetCode Link** | [Text Justification](https://leetcode.com/problems/text-justification/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given an array of strings `words` and a width `maxWidth`, format the text such that each line has exactly `maxWidth` characters and is fully (left and right) justified.
  - Pack as many words as possible in each line using a greedy approach.
  - Pad extra spaces `' '` so that each line has exactly `maxWidth` characters.
  - Extra spaces between words should be distributed as evenly as possible. If the number of spaces on a line does not divide evenly between words, the empty slots on the left will be assigned more spaces than the slots on the right.
  - For the last line of text, it should be left-justified, and no extra space is inserted between words (single spaces between words, remaining spaces padded at the end).
  - A line containing only one word must also be left-justified (word followed by spaces).
- **Assumptions & Contracts:**
  - Each word's length is guaranteed to be $\le maxWidth$.
  - Words contain only non-space characters.
  - Output is a list of justified line strings where every string has length exactly `maxWidth`.
- **Key Constraints:**
  - $1 \le words.Length \le 300$
  - $1 \le words[i].Length \le 20$
  - $words[i].Length \le maxWidth \le 100$
- **Senior Edge Cases to Defend:**
  - **Single Word Line:** A line containing only one word must place all padding spaces to the right of the word.
  - **Last Line of Text:** Words separated by exactly 1 space, remainder padded to the right up to `maxWidth`.
  - **Uneven Space Distribution:** E.g., 8 total spaces across 3 word gaps $\implies$ gaps receive 3, 3, 2 spaces.
  - **Exact Fit:** Total word characters plus minimum 1-space gaps equals `maxWidth` exactly.
  - **Single Character Words:** Words of length 1 packed with tight spacing.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Use two pointers `left` and `right` to greedily identify the maximal contiguous slice of words `words[left .. right - 1]` that fit on a line with at least 1 space between words:
  $$\sum_{k = left}^{right - 1} \text{len}(words[k]) + (right - 1 - left) \le maxWidth$$
  Once the slice is determined:
  - If `right == words.Length` (last line) OR `right - left == 1` (single word): Left-justify with single spaces and pad remainder to the right.
  - Otherwise: Distribute $(maxWidth - \sum \text{len})$ spaces across the $G = right - left - 1$ gaps using quotient $base = spaces / G$ and remainder $extra = spaces \% G$. The first $extra$ gaps receive $base + 1$ spaces, while the rest receive $base$ spaces.
- **Sample 1:**
  - **Input:** `words = ["This", "is", "an", "example", "of", "text", "justification."], maxWidth = 16`
  - **Output:**
    ```text
    [
       "This    is    an",
       "example  of text",
       "justification.  "
    ]
    ```
- **Sample 2:**
  - **Input:** `words = ["What","must","be","acknowledgment","shall","be"], maxWidth = 16`
  - **Output:**
    ```text
    [
      "What   must   be",
      "acknowledgment  ",
      "shall be        "
    ]
    ```

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine typesetting a book for a printing press. You have a fixed metal frame of width `maxWidth`. You take words from a drawer and set them into the line, placing at least one thin spacer between them. When the next word won't fit, you stop. You measure how much empty air remains on the right. If this is the middle of a paragraph, you distribute that air evenly across all the spacers, giving any leftover pixels to the leftmost gaps first so the page looks balanced. But if this is the final line of a chapter, you never stretch the words—you just let the words sit normally on the left and leave the remaining space blank on the right.

#### 3.2 The Naive Bottleneck & Redundant Computation
Naive code often handles word collection, space counting, gap distribution, and trailing line logic in a tangled web of conditionals and nested loops with repetitive string concatenations. Decomposing the typesetting pipeline into two clear decoupled phases—(1) Greedy Word Window Sizing, (2) Line Formatting Engine—eliminates code complexity and guarantees zero off-by-one errors.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Greedy Word Range Packing:**
  Line $L$ contains $words[left \dots right - 1]$ maximizing $right$ subject to:
  $$\text{totalWordLen}(left, right) + (right - left - 1) \le maxWidth$$
- **Space Balancing Arithmetic:**
  Let $W = \text{totalWordLen}(left, right)$ and $G = right - left - 1$ (number of gaps).
  Total spaces to distribute: $S = maxWidth - W$.
  Each gap $g \in [0, G - 1]$ receives:
  $$\text{spaces}(g) = \left\lfloor \frac{S}{G} \right\rfloor + \begin{cases} 1 & \text{if } g < (S \pmod G) \\ 0 & \text{otherwise} \end{cases}$$
  This distributes extra spaces strictly to the leftmost gaps and satisfies $\sum \text{spaces}(g) = S$.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
Two-Pointer Window & Gap Distribution Architecture:

words = ["This", "is", "an", "example", "of", "text", "justification."]
maxWidth = 16

Line 1 Window: left = 0 ("This"), right = 3 ("example" exceeds: 4+2+2+7 + 3 = 18 > 16)
Words: ["This", "is", "an"]
Word Len: 4 + 2 + 2 = 8
Total Spaces: 16 - 8 = 8
Gaps: 3 words - 1 = 2 gaps
baseSpace = 8 / 2 = 4
extraSpace = 8 % 2 = 0
Gap 0: 4 spaces
Gap 1: 4 spaces
Line 1: "This" + "    " + "is" + "    " + "an" ===> Length 16!
```

#### 3.5 State Transition Triggers & Decision Gates
1. Initialize `left = 0`.
2. While `left < words.Length`:
   - Find `right`: Start `right = left + 1`, `len = words[left].Length`.
   - While `right < words.Length && len + 1 + words[right].Length <= maxWidth`:
     `len += 1 + words[right].Length; right++;`
   - **Line Formatting Gate:**
     - Case A: `right == words.Length || right - left == 1` (Last line OR single word) $\implies$ Left-justify.
     - Case B: Fully justified $\implies$ Calculate `baseSpace = spaces / gaps` and `extraSpace = spaces % gaps`.
   - Advance: `left = right`.

#### 3.6 Concrete Step-by-Step State Trace
Input: `words = ["What","must","be","acknowledgment","shall","be"], maxWidth = 16`

| Line # | `left` | `right` | Selected Words | Mode | Spaces Math | Formatted Output Line |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **L1** | 0 | 3 | `["What", "must", "be"]` | Full Justify | Words=10, Spaces=6, Gaps=2 $\implies 3, 3$ | `"What   must   be"` |
| **L2** | 3 | 4 | `["acknowledgment"]` | Single Word | Words=14, Spaces=2, Pad right | `"acknowledgment  "` |
| **L3** | 4 | 6 | `["shall", "be"]` | Last Line | Words=7, 1 space between, Pad right 7 | `"shall be        "` |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1: Greedy Two-Pointer + StringBuilder Formatting (Optimal Standard):** $O(N)$ linear time over characters, minimal allocations, pre-allocated `StringBuilder` of capacity `maxWidth`.
- **Approach 2: High-Performance Span-Based Formatting:** Formats directly into a `string.Create` buffer. Zero intermediate string allocation per line.

#### 4.2 Step-by-Step Natural Progression Flow
1. Maintain outer cursor `left = 0`.
2. Greedily advance `right` to pack maximum words fitting into `maxWidth`.
3. Distinguish between normal full-justification lines and left-justified lines (last line or single-word line).
4. Build line string with exact space spacing and append to result.
5. Set `left = right` and repeat until all words are typeset.

#### 4.3 Alternative Approaches Analysis
- **Dynamic Programming (Knuth-Plass Line Breaking):** Used in TeX to minimize overall "raggedness" across a whole paragraph by penalizing stretched/compressed spaces globally. For LeetCode #68, the problem statement explicitly mandates *greedy* line packing, making DP an over-engineered violation of problem rules.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Approach | Time (Best) | Time (Avg) | Time (Worst) | Auxiliary Space | Output Space | Cache Locality | In-Place Mutability | Streaming Suitability |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **1. Greedy Window (Selected)**| $O(N)$ | $O(N)$ | $O(N)$ | $O(maxWidth)$ | $O(N)$ | High | Non-mutating | Semi-streaming |
| **2. String.Create Span** | $O(N)$ | $O(N)$ | $O(N)$ | $O(1)$ heap | $O(N)$ | Optimal | Non-mutating | Semi-streaming |

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: Text Justification (LeetCode #68)
 * ============================================================================
 * Core Pattern      : Greedy Sliding Window + Proportional Space Distribution
 * Time Complexity   : O(TotalCharacters) - Linear scan and formatting
 * Space Complexity  : O(maxWidth) Auxiliary - Working StringBuilder buffer per line
 * Selection Rule    : Production standard for document rendering and reporting engines
 * Defensive Traps   : Handle single-word lines as left-justified; distribute extra spaces left-to-right
 * ============================================================================
 */

using System;
using System.Collections.Generic;
using System.Text;

public class Solution
{
    /// <summary>
    /// Justifies an array of words into lines of exactly maxWidth characters.
    /// Employs greedy windowing and modular gap space distribution.
    /// </summary>
    /// <param name="words">Array of word tokens.</param>
    /// <param name="maxWidth">Target width of every formatted line.</param>
    /// <returns>List of fully justified and left-justified line strings.</returns>
    public IList<string> FullJustify(string[] words, int maxWidth)
    {
        var result = new List<string>();
        if (words == null || words.Length == 0)
        {
            return result;
        }

        int n = words.Length;
        int left = 0;

        while (left < n)
        {
            // STEP 1: Greedily pack as many words as possible into the current line
            int right = left + 1;
            int lineWordChars = words[left].Length;

            // Check if adding words[right] with a minimum 1-space gap exceeds maxWidth
            while (right < n && lineWordChars + 1 + words[right].Length + (right - 1 - left) <= maxWidth)
            {
                lineWordChars += words[right].Length;
                right++;
            }

            int numWords = right - left;
            int numGaps = numWords - 1;
            var sb = new StringBuilder(capacity: maxWidth);

            // STEP 2: Format Line based on line classification
            // Condition for Left-Justification: It is the final line OR the line contains only 1 word
            if (right == n || numWords == 1)
            {
                for (int i = left; i < right; i++)
                {
                    sb.Append(words[i]);
                    if (i < right - 1)
                    {
                        sb.Append(' ');
                    }
                }

                // Pad remaining trailing spaces to reach exact maxWidth
                while (sb.Length < maxWidth)
                {
                    sb.Append(' ');
                }
            }
            else
            {
                // Full Justification: Distribute spaces across the gaps
                int totalSpaces = maxWidth - lineWordChars;
                int baseSpaces = totalSpaces / numGaps;
                int extraSpaces = totalSpaces % numGaps;

                for (int i = left; i < right; i++)
                {
                    sb.Append(words[i]);

                    // Append spaces to gap between words
                    if (i < right - 1)
                    {
                        int gapIndex = i - left;
                        int spacesToApply = baseSpaces + (gapIndex < extraSpaces ? 1 : 0);
                        sb.Append(' ', spacesToApply);
                    }
                }
            }

            result.Add(sb.ToString());

            // Advance to the next line window
            left = right;
        }

        return result;
    }
}
```

---

### 6. Senior Pitfalls & Defensive Traps
- **Division by Zero on Single-Word Lines:** If a line contains only 1 word, the number of gaps is $numWords - 1 = 0$. Executing `totalSpaces / numGaps` triggers a catastrophic `DivideByZeroException`! Single-word lines must be explicitly routed to the left-justification branch.
- **The Last Line Trap:** In the last line, even if multiple words and gaps exist, the words must *never* be stretched across the line! They must be separated by single spaces, and all remaining padding must be dumped at the end.
- **Leftmost Bias for Extra Spaces:** When extra spaces cannot be distributed evenly ($extraSpaces = totalSpaces \% numGaps > 0$), they must be awarded to the leftmost gaps first (e.g. `gapIndex < extraSpaces ? base + 1 : base`), not distributed randomly or right-aligned.

---

---

## 152. Basic Calculator II (LeetCode #227)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#string` `#stack` `#math` `#parsing` `#operator-precedence` |
| **LeetCode Link** | [Basic Calculator II](https://leetcode.com/problems/basic-calculator-ii/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given a string `s` which represents an arithmetic expression containing non-negative integers and operators `'+'`, `'-'`, `'*'`, `'/'`, evaluate this expression and return its integer value. Integer division should truncate toward zero.
- **Assumptions & Contracts:**
  - The expression is always valid.
  - Intermediate evaluation results fit within a 32-bit signed integer $[-2^{31}, 2^{31} - 1]$.
  - No parentheses `()` are present in this expression variant.
  - You are not allowed to use any built-in library function (such as `DataTable.Compute` or `eval()`).
- **Key Constraints:**
  - $1 \le s.Length \le 3 \times 10^5$
  - `s` consists of integers, operators `'+'`, `'-'`, `'*'`, `'/'`, and spaces `' '`.
  - Division by zero never occurs.
- **Senior Edge Cases to Defend:**
  - **Whitespace Inundation:** `" 3 +  5 / 2 "` contains leading, trailing, and arbitrarily interspersed spaces.
  - **Precedence Order Reversal:** `"3 + 2 * 2"` must evaluate to $7$, NOT $10$ (multiplication must execute before addition).
  - **Consecutive High-Precedence Operations:** `"14 - 3 / 2"` must evaluate to $13$ (integer division $3 / 2 = 1$, then $14 - 1 = 13$).
  - **Multi-Digit Numbers:** `"100000000 / 10 / 10"` with chained divisions.
  - **Unary Sign Non-Existence:** Problem specifies non-negative integers, but subtractions produce negative intermediate terms.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Multiplication and division bind tighter than addition and subtraction. We can delay addition and subtraction by maintaining a running sum `totalSum` and an active term register `lastNumber`. When encountering high-precedence operators (`*`, `/`), we immediately fold the incoming number into `lastNumber`. When encountering low-precedence operators (`+`, `-`), the previous `lastNumber` is settled and committed into `totalSum`. This collapses a 2-pass stack algorithm into an optimal $O(1)$ auxiliary memory single pass.
- **Sample 1:**
  - **Input:** `s = "3+2*2"`
  - **Output:** `7`
- **Sample 2:**
  - **Input:** `s = " 3/2 "`
  - **Output:** `1`
- **Sample 3:**
  - **Input:** `s = " 3+5 / 2 "`
  - **Output:** `5`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Think of an accounting ledger. Every time someone says "add \$5" or "subtract \$3", you write down the signed item on a draft line. But if someone says "multiply that by 4", you cannot write a new line—you must immediately multiply the number currently sitting on your draft line. When the entire calculation ends, you simply sum up all the finalized draft lines. To do this without a stack of paper, you keep only one finalized total in your ledger (`totalSum`) and the one draft number you are currently multiplying or dividing (`lastNumber`).

#### 3.2 The Naive Bottleneck & Redundant Computation
A classic compiler parser uses Dijkstra's Shunting-Yard algorithm to convert infix expressions to Reverse Polish Notation (RPN) using two stacks (operators and operands) and then evaluates the RPN queue. While necessary for arbitrary expressions with parentheses, for simple expressions with only $\{+, -, *, /\}$, this allocates two stacks of size $O(N)$, causing high memory footprint when $N = 3 \times 10^5$. Even a single operand stack allocates $O(N)$ memory. A stackless streaming state machine solves it in $O(1)$ auxiliary memory.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Algebraic Additive Normalization:**
  Any valid expression can be expressed as a linear summation of multiplicative terms:
  $$E = \sum_{k=1}^m T_k, \quad \text{where each } T_k = \pm \prod c_j^{\pm 1}$$
  - When operator is `+` or `-`: $T_{k-1}$ is sealed and added to $\text{totalSum}$; a new term $T_k = \pm \text{currentNumber}$ begins.
  - When operator is `*`: $T_k \leftarrow T_k \times \text{currentNumber}$.
  - When operator is `/`: $T_k \leftarrow \lfloor T_k / \text{currentNumber} \rfloor$.
- **Terminal Sentinel Guarantee:**
  By treating the virtual end of the string ($i == n - 1$) as a synthetic operator trigger, the trailing number is guaranteed to be folded and committed into the running state.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
Stackless State Machine Architecture:

Expression: " 3  +  2  *  2 "
             ---   ---   ---
              |     |     |
              v     v     v
State Registers:
- totalSum    : Sum of all permanently finalized multiplicative terms
- lastNumber  : Current active term being multiplied/divided
- currentNum  : Accumulator for digits of the incoming number
- lastOp      : The operator preceding currentNum (initialized to '+')

Trace:
- Encounter '3': currentNum = 3
- Encounter '+': lastOp was '+' -> lastNumber = +3; lastOp = '+'; currentNum = 0
- Encounter '2': currentNum = 2
- Encounter '*': lastOp was '+' -> totalSum += lastNumber (0 + 3 = 3), lastNumber = 2; lastOp = '*'; currentNum = 0
- Encounter '2': currentNum = 2
- End of String: lastOp was '*' -> lastNumber = lastNumber * 2 (2 * 2 = 4)

Final Return: totalSum + lastNumber = 3 + 4 = 7!
```

#### 3.5 State Transition Triggers & Decision Gates
For each character $c$ at index $i \in [0, n - 1]$:
1. **Digit Ingestion:** If $c$ is a digit, `currentNumber = currentNumber * 10 + (c - '0')`.
2. **Operator / Terminal Gate:** If $c$ is non-whitespace and not a digit, OR $i == n - 1$:
   - Branch on `lastOperator`:
     - Case `'+'`: `totalSum += lastNumber; lastNumber = currentNumber;`
     - Case `'-'`: `totalSum += lastNumber; lastNumber = -currentNumber;`
     - Case `'*'`: `lastNumber = lastNumber * currentNumber;`
     - Case `'/'`: `lastNumber = lastNumber / currentNumber;`
   - Reset: `currentNumber = 0`, `lastOperator = c`.

#### 3.6 Concrete Step-by-Step State Trace
Input: `s = "14 - 3 / 2"` ($n = 10$).

| Char Index ($i$) | Char (`s[i]`) | `currentNum` | `totalSum` | `lastNumber` | `lastOp` | Action Triggered |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **0, 1** | `'1', '4'` | `14` | `0` | `0` | `'+'` | Accumulate digits |
| **3** | `'-'` | `14` | `0` | `14` | `'-'` | `lastOp=='+' \implies` `last=14`, `op='-'`, `curr=0` |
| **5** | `'3'` | `3` | `0` | `14` | `'-'` | Accumulate digit |
| **7** | `'/'` | `3` | `14` | `-3` | `'/'` | `lastOp=='-' \implies` `total+=14`, `last=-3`, `op='/'` |
| **9** | `'2'` (End) | `2` | `14` | `-1` | `'/'` | `lastOp=='/' \implies` `last = -3 / 2 = -1` |
| **Final** | — | — | `14` | `-1` | — | Return `totalSum + lastNumber = 14 + (-1) = 13` |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1: Stackless Constant-Space State Machine (Optimal Principal Standard):** Tracks `totalSum` and `lastNumber`. Achieves $O(N)$ time and strictly $O(1)$ auxiliary space. Zero heap memory allocation. Handles large strings ($N = 3 \times 10^5$) with optimal CPU instruction pipelining.
- **Approach 2: Explicit Operand Stack ($O(N)$ Memory):** Pushes signed numbers onto `Stack<int>`. On `*` and `/`, pops, executes, and pushes back. Sums stack at termination. Classic compiler design model, very intuitive, but allocates $O(N)$ extra memory on the managed heap.

#### 4.2 Step-by-Step Natural Progression Flow
1. Validate non-empty string.
2. Initialize `totalSum = 0`, `lastNumber = 0`, `currentNumber = 0`, `lastOperator = '+'`.
3. Loop through indices $i = 0 \dots n - 1$.
4. Check digit vs operator.
5. On operator or index $n - 1$, execute precedence transition.
6. Return `totalSum + lastNumber`.

#### 4.3 Alternative Approaches Analysis
- **Shunting-Yard + RPN:** Generalizes to parentheses and unary operators, but is unnecessarily complex for LeetCode #227 where expressions have no parentheses.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Approach | Time (Best) | Time (Avg) | Time (Worst) | Auxiliary Space | Output Space | Cache Locality | In-Place Mutability | Streaming Suitability |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **1. Stackless State Machine**| $O(N)$ | $O(N)$ | $O(N)$ | $O(1)$ | $O(1)$ | Optimal | Non-mutating | Excellent (online) |
| **2. Explicit Stack** | $O(N)$ | $O(N)$ | $O(N)$ | $O(N)$ | $O(1)$ | Moderate | Non-mutating | Semi-streaming |
| **3. Shunting-Yard RPN** | $O(N)$ | $O(N)$ | $O(N)$ | $O(N)$ | $O(1)$ | Poor | Non-mutating | Offline |

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: Basic Calculator II (LeetCode #227)
 * ============================================================================
 * Core Pattern      : Stackless Precedence State Machine (Running Term Invariant)
 * Time Complexity   : O(N) - Single linear pass over string of length N
 * Space Complexity  : O(1) Auxiliary - Four CPU scalar registers (zero heap allocation)
 * Selection Rule    : High-throughput parsing engines and expression evaluation services
 * Defensive Traps   : Trigger evaluation at i == n - 1 to flush final number; ignore whitespace
 * ============================================================================
 */

using System;
using System.Collections.Generic;

public class Solution
{
    /// <summary>
    /// Evaluates an arithmetic expression containing +, -, *, / and non-negative integers.
    /// Employs a stackless state machine to achieve O(N) time and O(1) auxiliary space.
    /// </summary>
    /// <param name="s">Mathematical expression string.</param>
    /// <returns>Truncated integer evaluation result.</returns>
    public int Calculate(string s)
    {
        if (string.IsNullOrWhiteSpace(s))
        {
            return 0;
        }

        int totalSum = 0;
        int lastNumber = 0;
        int currentNumber = 0;
        char lastOperator = '+';

        int n = s.Length;

        for (int i = 0; i < n; i++)
        {
            char c = s[i];

            // 1. Accumulate multi-digit numeric tokens
            if (char.IsDigit(c))
            {
                currentNumber = currentNumber * 10 + (c - '0');
            }

            // 2. Evaluate operator precedence when encountering an operator or the end of string
            // Note: Whitespace is explicitly ignored by the (c != ' ') predicate
            if ((!char.IsDigit(c) && c != ' ') || i == n - 1)
            {
                switch (lastOperator)
                {
                    case '+':
                        // Fold previous lastNumber into totalSum; start new positive term
                        totalSum += lastNumber;
                        lastNumber = currentNumber;
                        break;

                    case '-':
                        // Fold previous lastNumber into totalSum; start new negative term
                        totalSum += lastNumber;
                        lastNumber = -currentNumber;
                        break;

                    case '*':
                        // High precedence: immediately multiply into current active term
                        lastNumber = lastNumber * currentNumber;
                        break;

                    case '/':
                        // High precedence: immediately divide into current active term (truncates to zero)
                        lastNumber = lastNumber / currentNumber;
                        break;
                }

                // Update state registers for the next token
                lastOperator = c;
                currentNumber = 0;
            }
        }

        // Add the final active term to the running total
        return totalSum + lastNumber;
    }
}

/// <summary>
/// Classical Approach: Explicit Operand Stack.
/// Useful in interviews to demonstrate the shunting evolution before presenting the O(1) space optimization.
/// </summary>
public class SolutionStackApproach
{
    public int Calculate(string s)
    {
        if (string.IsNullOrWhiteSpace(s)) return 0;

        var stack = new Stack<int>();
        int currentNumber = 0;
        char lastOperator = '+';
        int n = s.Length;

        for (int i = 0; i < n; i++)
        {
            char c = s[i];

            if (char.IsDigit(c))
            {
                currentNumber = currentNumber * 10 + (c - '0');
            }

            if ((!char.IsDigit(c) && c != ' ') || i == n - 1)
            {
                if (lastOperator == '+')
                {
                    stack.Push(currentNumber);
                }
                else if (lastOperator == '-')
                {
                    stack.Push(-currentNumber);
                }
                else if (lastOperator == '*')
                {
                    stack.Push(stack.Pop() * currentNumber);
                }
                else if (lastOperator == '/')
                {
                    stack.Push(stack.Pop() / currentNumber);
                }

                lastOperator = c;
                currentNumber = 0;
            }
        }

        int result = 0;
        while (stack.Count > 0)
        {
            result += stack.Pop();
        }

        return result;
    }
}
```

---

### 6. Senior Pitfalls & Defensive Traps
- **The End-of-String Flush Drop:** The most common parsing defect is writing `if (!char.IsDigit(c) && c != ' ')` without the `|| i == n - 1` condition. In `"3 + 2 * 2"`, when index reaches the final `'2'`, the digit is accumulated into `currentNumber`, but loop terminates without ever applying the `'*'` operator to that final number! Always ensure terminal indices flush the trailing token.
- **Whitespace False-Triggering:** If whitespace `' '` is mistakenly treated as an operator, `currentNumber` will be flushed prematurely, turning multi-digit numbers into garbage or triggering duplicate operator evaluation.
- **Integer Division Truncation in C#:** C# integer division `/` truncates toward zero by default for both positive and negative numbers (`-3 / 2 == -1`, `3 / 2 == 1`), which perfectly adheres to the problem requirement. In languages like Python (`//` floors toward $-\infty$), one must explicitly use `int(a / b)`.

---

## 153. Binary Tree Maximum Path Sum (LeetCode #124)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🔴 Hard |
| **Priority** | 👑 Lead Anchor |
| **Pattern Tags** | `#tree` `#dfs` `#post-order` `#bottom-up-dp` `#divide-and-conquer` |
| **LeetCode Link** | [Binary Tree Maximum Path Sum](https://leetcode.com/problems/binary-tree-maximum-path-sum/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** A **path** in a binary tree is a sequence of nodes where each pair of adjacent nodes in the sequence has an edge connecting them. A node can only appear in the sequence at most once. Note that the path does not need to pass through the root. The **path sum** of a path is the sum of the node's values in the path. Given the `root` of a binary tree, return the maximum **path sum** of any non-empty path.
- **Assumptions & Contracts:**
  - The path must be non-empty (contains at least one node).
  - Node values can be negative, zero, or positive.
  - A path can branch at at most ONE node (the highest ancestor/apex node of the path). When returning gain to a parent, the path cannot fork.
- **Key Constraints:**
  - The number of nodes in the tree is in the range $[1, 3 \times 10^4]$.
  - $-1000 \le Node.val \le 1000$.
- **Senior Edge Cases to Defend:**
  - **All Negative Node Values:** `[-3]`, or `[-2, -1]`. The maximum path is the single least negative node (e.g., `-1`), NOT 0. The global maximum tracker must be initialized to `int.MinValue`, not `0`.
  - **Negative Gain Subtrees:** A subtree whose total gain is negative should be clamped to `0` via `Math.Max(0, gain)` to indicate that the path chooses to exclude that branch entirely.
  - **Single Node Tree:** Tree with exactly 1 node returns that node's value.
  - **Linear Skewed Tree (Linked List shape):** Deep trees ($H = 3 \times 10^4$) must avoid stack overflow in deep recursion.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** At each node $u$, we compute two distinct mathematical quantities:
  1. **Global Turnaround Path Sum (In-Node Apex):** $u.val + \max(0, \text{leftGain}) + \max(0, \text{rightGain})$. This path turns around at node $u$ as its peak apex, utilizing both children. This value cannot be extended higher to $u$'s parent.
  2. **Max Gain to Propagate to Parent (Exportable Path):** $u.val + \max(0, \max(\text{leftGain}, \text{rightGain}))$. The parent can only choose at most ONE branch from $u$.
- **Sample 1:**
  - **Input:** `root = [1, 2, 3]`
  - **Output:** `6` (Path: $2 \to 1 \to 3$)
- **Sample 2:**
  - **Input:** `root = [-10, 9, 20, null, null, 15, 7]`
  - **Output:** `42` (Path: $15 \to 20 \to 7$)

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine a mountain range where every peak and pass has an elevation or toll (which can be positive or negative). You want to hike a continuous trail with the highest net scenic score. When you stand on a mountain ridge (node $u$), you ask your scouts on the left and right slopes: *"What is the best scenic trail you can form starting from me and heading straight down your ridge?"* If a ridge has only treacherous bogs that lower your score (negative gain), you prune it: take 0. You have two options:
1. **Camp at the Apex:** You combine the best descent on the left with the best descent on the right and yourself. This forms a complete peak-to-peak ridge hike. You radio this total to the global record book.
2. **Report to HQ (Parent):** You cannot tell HQ to visit both left and right valleys because a hiker cannot be in two places at once without retracing their steps (which violates the contract). You can only report: *"If you come down to me, the best continuation is either my left slope or my right slope, whichever is higher."*

#### 3.2 The Naive Bottleneck & Redundant Computation
A brute-force solution picks all pairs of nodes $(u, v)$ in the tree ($O(N^2)$ pairs), finds the unique simple path between them via LCA in $O(N)$ time, and sums their node values in $O(N)$ time.
- **Total Naive Time:** $O(N^3)$.
- **Redundancy:** Every subtree is traversed millions of times to recompute identical segment sums. A bottom-up post-order DFS visits every node exactly once, updating the global maximum path in $O(N)$ total time.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Post-Order Subproblem Recurrence:**
  $$\text{Gain}(node) = node.val + \max(0, \max(\text{Gain}(node.left), \text{Gain}(node.right)))$$
- **Apex Turning Path Update:**
  $$\text{ApexPath}(node) = node.val + \max(0, \text{Gain}(node.left)) + \max(0, \text{Gain}(node.right))$$
  $$\text{GlobalMax} = \max(\text{GlobalMax}, \text{ApexPath}(node))$$
- **Pruning Invariant:**
  If $\text{Gain}(child) < 0$, clamping to $0$ guarantees that negative subtrees never drag down the optimal path.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
               (Parent)
                  │
                  ▼
              [ Node u ]  <─── Apex Turnaround: u.val + leftGain + rightGain
             /          \      (Candidate for Global Maximum)
            /            \
           ▼              ▼
     [ Left Gain ]   [ Right Gain ]
     (Clamped to 0)  (Clamped to 0)
```

- Every node $u$ is processed strictly after its left and right subtrees have settled (Post-Order DFS).
- The returned value is strictly the **unbranched straight-line gain** that can be extended by the parent.

#### 3.5 State Transition Triggers & Decision Gates
1. **Base Case:** If `node == null`, return `0`.
2. **Left Subtree Probe:** Compute `leftGain = Math.Max(0, MaxGain(node.left))`.
3. **Right Subtree Probe:** Compute `rightGain = Math.Max(0, MaxGain(node.right))`.
4. **Apex Evaluation Gate:** Compute `currentApex = node.val + leftGain + rightGain`. Update `_maxPathSum = Math.Max(_maxPathSum, currentApex)`.
5. **Propagation Gate:** Return `node.val + Math.Max(leftGain, rightGain)` to caller.

#### 3.6 Concrete Step-by-Step State Trace
Tree: `[-10, 9, 20, null, null, 15, 7]`

| Step | Current Node | `leftGain` (clamped) | `rightGain` (clamped) | `currentApex` | Global Max Path Before $\to$ After | Return Value to Parent |
| :---: | :---: | :---: | :---: | :---: | :---: | :---: |
| 1 | `9` (leaf) | `0` | `0` | $9 + 0 + 0 = 9$ | $-\infty \to 9$ | $9 + 0 = 9$ |
| 2 | `15` (leaf) | `0` | `0` | $15 + 0 + 0 = 15$ | $9 \to 15$ | $15 + 0 = 15$ |
| 3 | `7` (leaf) | `0` | `0` | $7 + 0 + 0 = 7$ | $15 \to 15$ | $7 + 0 = 7$ |
| 4 | `20` | $\max(0, 15) = 15$ | $\max(0, 7) = 7$ | $20 + 15 + 7 = 42$ | $15 \to \mathbf{42}$ | $20 + \max(15, 7) = 35$ |
| 5 | `-10` (root) | $\max(0, 9) = 9$ | $\max(0, 35) = 35$ | $-10 + 9 + 35 = 34$ | $42 \to \mathbf{42}$ | $-10 + 35 = 25$ |

**Final Maximum Path Sum:** `42` (Nodes: $15 \to 20 \to 7$).

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Bottom-Up Post-Order DFS (Optimal):**
  - Linear single-pass $O(N)$ time.
  - Subtrees report single-branch gain upwards while updating global apex in-place.
- **Tree DP with State Memoization:**
  - On generic trees/DAGs, DP states `dp[node][0]` (pass through) and `dp[node][1]` (endpoint) formalize this exact logic.

#### 4.2 Step-by-Step Natural Progression Flow
1. Initialize `_maxSum = int.MinValue` to defend against all-negative trees.
2. Define recursive helper `CalculateGain(TreeNode node)`.
3. In helper, recursively compute gains of left and right children, clamping negative returns to 0.
4. Update `_maxSum` with `node.val + leftGain + rightGain`.
5. Return `node.val + Math.Max(leftGain, rightGain)` for parent continuation.

#### 4.3 Alternative Approaches Analysis
- **Top-Down DFS with Path Parameter:**
  - Passing parent accumulators downwards fails because paths can start and end anywhere and can turn around at non-root nodes without parent involvement.
- **Tree Rerooting DP:**
  - Overkill for this problem; post-order single pass resolves all possible apexes without re-rooting.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Approach | Time Complexity | Auxiliary Space | Output Space | Mutates Tree | Stack Overhead |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Post-Order Bottom-Up DFS** | $O(N)$ | $O(H)$ | $O(1)$ | No | Recursion Call Stack |
| **All-Pairs LCA Path Sum** | $O(N^3)$ | $O(N)$ | $O(1)$ | No | Quadratic Pair Graph |

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: Problem #153 - Binary Tree Maximum Path Sum
 * ============================================================================
 * Core Pattern      : Bottom-Up Post-Order Tree DFS (Exportable Gain vs Apex Turnaround)
 * Time Complexity   : O(N) single-pass traversal visiting each node exactly once
 * Space Complexity  : O(H) call-stack space where H is tree height (O(log N) avg, O(N) worst)
 * Selection Rule    : Bottom-up post-order enables computing subtree gain before parent;
 *                     clamping negative gains to 0 prunes loss-making branches.
 * Defensive Traps   : 1. Initialize global max to int.MinValue (defends all-negative trees).
 *                     2. Do NOT return the apex turnaround value to the parent.
 * ============================================================================
 */

namespace SeniorDSA.MatrixAndStrings;

public class SolutionMaxPathSum
{
    private int _maxPathSum;

    /// <summary>
    /// Computes the maximum path sum of any non-empty path in the binary tree.
    /// Operates in O(N) time and O(H) auxiliary call-stack memory.
    /// </summary>
    /// <param name="root">The root node of the binary tree.</param>
    /// <returns>The maximum path sum.</returns>
    public int MaxPathSum(TreeNode? root)
    {
        // Guard Clause: Contract requires a non-empty path
        if (root == null)
        {
            return 0;
        }

        // Defensive Initialization: Must be int.MinValue to support trees with exclusively negative values
        _maxPathSum = int.MinValue;

        ComputeGainDownwards(root);

        return _maxPathSum;
    }

    /// <summary>
    /// Post-order recursive helper. Computes the maximum single-branch downward gain
    /// that can be extended by the parent, while updating the global apex turnaround path.
    /// </summary>
    private int ComputeGainDownwards(TreeNode? node)
    {
        if (node == null)
        {
            return 0;
        }

        // Invariant: Clamp negative gains to 0 (prune branches that decrease path value)
        int leftGain = Math.Max(0, ComputeGainDownwards(node.left));
        int rightGain = Math.Max(0, ComputeGainDownwards(node.right));

        // Decision Gate: Calculate the complete apex turnaround path turning at current node
        int currentApexPath = node.val + leftGain + rightGain;

        // State Mutation: Record candidate against global maximum
        _maxPathSum = Math.Max(_maxPathSum, currentApexPath);

        // Propagation Invariant: Return only ONE branch to parent (cannot fork at parent level)
        return node.val + Math.Max(leftGain, rightGain);
    }
}
```

---

### 6. Senior Pitfalls & Defensive Traps

1. **The All-Negative Initializer Bug:**
   - *Trap:* Initializing `_maxPathSum = 0`.
   - *Failure:* If the tree contains only negative numbers (e.g., `[-3]`), the method returns `0`, which is a phantom empty path violating the contract.
   - *Defense:* Always initialize `_maxPathSum = int.MinValue`.
2. **The Turnaround Return Violation:**
   - *Trap:* Returning `node.val + leftGain + rightGain` to the parent.
   - *Failure:* A parent cannot reuse both children of its child without creating a cycle or degree-3 branching path.
   - *Defense:* Return strictly `node.val + Math.Max(leftGain, rightGain)`.
3. **Negative Subtree Inclusion Bug:**
   - *Trap:* Forgetting `Math.Max(0, ...)` when reading child gain.
   - *Failure:* Subtrees with negative sum degrade the parent's path score instead of being safely excluded.
