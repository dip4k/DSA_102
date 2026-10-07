# 📘 Week 13 Day 02: Classic Backtracking Problems — Engineering Guide

> 🧭 **Navigation:** [← Previous Day](file:///d:/Interview_prep/DSA_102/week_13_backtracking_and_branch_bound/Week_13_Day_01_Backtracking_Fundamentals_Instructional.md) • [🏠 Week Overview](file:///d:/Interview_prep/DSA_102/week_13_backtracking_and_branch_bound/README.md) • [📘 Curriculum Syllabus](file:///d:/Interview_prep/DSA_102/COMPLETE_SYLLABUS.md) • [Next Day →](file:///d:/Interview_prep/DSA_102/week_13_backtracking_and_branch_bound/Week_13_Day_03_Branch_And_Bound_Instructional.md)
> 
> 💡 **Instructor Note:** *Master the distinction between finding ONE valid solution (returning boolean to stop recursion early) versus finding ALL valid solutions (exhausting the full search space). Both use the Choose / Explore / Unchoose contract.*

---

## 🎯 Learning Objectives

- 👑 **Solve** N-Queens using row-by-row decomposition and `O(1)` diagonal math (`r - c` and `r + c`).
- 🧩 **Implement** a production Sudoku Solver terminating immediately on first valid configuration.
- 🗺️ **Apply** in-place grid masking in Word Search to achieve zero auxiliary heap allocation during DFS.
- 🌲 **Trace** multi-dimensional state space pruning using clean ASCII recursion trees.
- 🎙️ **Communicate** exact interview trade-offs between space-optimized bitmasks and hash sets under 45-minute constraints.

---

## 👑 Problem 1: N-Queens (LeetCode 51)

### Mental Model & Invariants

Placing `N` queens on an `N x N` board requires exactly one queen per row. We iterate row-by-row (`r = 0 .. N - 1`), choosing a valid column `c` for each row.

A cell `(r, c)` is threatened if another queen already occupies:
1. Column: `c`
2. Main Diagonal (`\`): `r - c` is constant (range: `-(N - 1) .. (N - 1)`).
3. Anti-Diagonal (`/`): `r + c` is constant (range: `0 .. 2N - 2`).

### ASCII Recursion Tree (N = 4)
```
                                        Row 0: Choice (0, c)
                          /                     |                     \
                     (0, 0)                  (0, 1)                  (0, 2) ...
                   /    |   \               /      \
             (1,0)    (1,1)  (1,2)       (1,3)    (1,0)
              [X]      [X]     │          │        [X]
            (Col)    (Diag)  Row 2     Row 2     (Diag)
                             /   \       /  \
                          (2,0) (2,1) (2,0) (2,2)
                           [X]   [X]    │    [X]
                                       Row 3
                                         │
                                       (3,2)
                                         │
                                  [SOLVED: (0,1),(1,3),(2,0),(3,2)]
---------------------------------------------------------------------------------
[X] ATTACK PRUNED: Column, Main Diagonal (r-c), or Anti-Diagonal (r+c) occupied.
```

### Dual Implementation: N-Queens

```csharp
// C# (.NET 8/9): N-Queens with HashSet Lookup
public sealed class NQueensSolver
{
    public IList<IList<string>> SolveNQueens(int n)
    {
        var solutions = new List<IList<string>>();
        var board = new char[n][];
        for (int i = 0; i < n; i++)
        {
            board[i] = new char[n];
            Array.Fill(board[i], '.');
        }

        var cols = new HashSet<int>();
        var diag1 = new HashSet<int>(); // r - c
        var diag2 = new HashSet<int>(); // r + c

        void Dfs(int r)
        {
            if (r == n)
            {
                solutions.Add(board.Select(row => new string(row)).ToList());
                return;
            }

            for (int c = 0; c < n; c++)
            {
                if (cols.Contains(c) || diag1.Contains(r - c) || diag2.Contains(r + c))
                    continue; // Eager pruning

                // Choose
                board[r][c] = 'Q';
                cols.Add(c);
                diag1.Add(r - c);
                diag2.Add(r + c);

                // Explore
                Dfs(r + 1);

                // Unchoose
                board[r][c] = '.';
                cols.Remove(c);
                diag1.Remove(r - c);
                diag2.Remove(r + c);
            }
        }

        Dfs(0);
        return solutions;
    }
}
```

```python
# Python (3.11+): N-Queens with Set Lookup
class NQueensSolver:
    def solve_n_queens(self, n: int) -> list[list[str]]:
        solutions: list[list[str]] = []
        board = [['.' for _ in range(n)] for _ in range(n)]

        cols: set[int] = set()
        diag1: set[int] = set()  # r - c
        diag2: set[int] = set()  # r + c

        def dfs(r: int) -> None:
            if r == n:
                solutions.append([''.join(row) for row in board])
                return

            for c in range(n):
                if c in cols or (r - c) in diag1 or (r + c) in diag2:
                    continue  # Eager pruning

                # Choose
                board[r][c] = 'Q'
                cols.add(c)
                diag1.add(r - c)
                diag2.add(r + c)

                # Explore
                dfs(r + 1)

                # Unchoose
                board[r][c] = '.'
                cols.remove(c)
                diag1.remove(r - c)
                diag2.remove(r + c)

        dfs(0)
        return solutions
```

---

## 🧩 Problem 2: Sudoku Solver (LeetCode 37)

### Mental Model & Short-Circuit Search

Unlike generating all subsets, Sudoku asks for **one** valid completed board. The recursive function must return a boolean (`bool` / `True`) so that as soon as the terminal cell is filled, execution short-circuits immediately without unwinding back into other candidate branches.

A digit `d ∈ '1'..'9'` is valid at `(r, c)` if:
1. `d` does not exist in row `r`.
2. `d` does not exist in column `c`.
3. `d` does not exist in the `3 x 3` sub-box starting at `(r - r % 3, c - c % 3)`.

### Dual Implementation: Sudoku Solver

```csharp
// C# (.NET 8/9): In-Place Sudoku Solver with Boolean Return
public sealed class SudokuSolver
{
    public void SolveSudoku(char[][] board)
    {
        Solve(board);
    }

    private bool Solve(char[][] board)
    {
        for (int r = 0; r < 9; r++)
        {
            for (int c = 0; c < 9; c++)
            {
                if (board[r][c] != '.') continue;

                for (char digit = '1'; digit <= '9'; digit++)
                {
                    if (IsValid(board, r, c, digit))
                    {
                        board[r][c] = digit; // Choose

                        if (Solve(board))    // Explore with early return
                            return true;

                        board[r][c] = '.';   // Unchoose
                    }
                }
                return false; // None of '1'..'9' worked; backtrack
            }
        }
        return true; // All 81 cells filled validly
    }

    private bool IsValid(char[][] board, int r, int c, char digit)
    {
        int boxRow = 3 * (r / 3);
        int boxCol = 3 * (c / 3);

        for (int i = 0; i < 9; i++)
        {
            if (board[r][i] == digit) return false;
            if (board[i][c] == digit) return false;
            if (board[boxRow + i / 3][boxCol + i % 3] == digit) return false;
        }
        return true;
    }
}
```

```python
# Python (3.11+): In-Place Sudoku Solver with Boolean Return
class SudokuSolver:
    def solve_sudoku(self, board: list[list[str]]) -> None:
        self._solve(board)

    def _solve(self, board: list[list[str]]) -> bool:
        for r in range(9):
            for c in range(9):
                if board[r][c] != '.':
                    continue

                for num in map(str, range(1, 10)):
                    if self._is_valid(board, r, c, num):
                        board[r][c] = num      # Choose

                        if self._solve(board): # Explore
                            return True

                        board[r][c] = '.'      # Unchoose

                return False  # Dead end reached; trigger backtracking
        return True  # Puzzle completely solved

    def _is_valid(self, board: list[list[str]], r: int, c: int, num: str) -> bool:
        box_r, box_c = 3 * (r // 3), 3 * (c // 3)
        for i in range(9):
            if board[r][i] == num:
                return False
            if board[i][c] == num:
                return False
            if board[box_r + i // 3][box_c + i % 3] == num:
                return False
        return True
```

---

## 🗺️ Problem 3: Word Search in Grid (LeetCode 79)

### Mental Model & In-Place Masking

When searching for a word of length `L` on an `M x N` board, backtracking visits cells in 4 cardinal directions (`Up, Down, Left, Right`). To prevent revisiting cells on the current path without allocating an `M x N` visited array:
- Temporarily replace `board[r][c]` with `'#'` on entry.
- Restore `board[r][c]` back to its original character on exit.

```csharp
// C# (.NET 8/9): Word Search with In-Place Masking
public sealed class WordSearchSolver
{
    public bool Exist(char[][] board, string word)
    {
        int m = board.Length, n = board[0].Length;

        for (int r = 0; r < m; r++)
        {
            for (int c = 0; c < n; c++)
            {
                if (Dfs(r, c, 0)) return true;
            }
        }
        return false;

        bool Dfs(int r, int c, int k)
        {
            if (k == word.Length) return true;
            if (r < 0 || r >= m || c < 0 || c >= n || board[r][c] != word[k])
                return false;

            char temp = board[r][c];
            board[r][c] = '#'; // Choose: in-place mask

            bool found = Dfs(r + 1, c, k + 1) ||
                         Dfs(r - 1, c, k + 1) ||
                         Dfs(r, c + 1, k + 1) ||
                         Dfs(r, c - 1, k + 1); // Explore 4 directions

            board[r][c] = temp; // Unchoose: restore
            return found;
        }
    }
}
```

```python
# Python (3.11+): Word Search with In-Place Masking
class WordSearchSolver:
    def exist(self, board: list[list[str]], word: str) -> bool:
        m, n = len(board), len(board[0])

        def dfs(r: int, c: int, k: int) -> bool:
            if k == len(word):
                return True
            if not (0 <= r < m and 0 <= c < n) or board[r][c] != word[k]:
                return False

            temp = board[r][c]
            board[r][c] = '#'  # Choose: in-place mask

            found = (
                dfs(r + 1, c, k + 1) or
                dfs(r - 1, c, k + 1) or
                dfs(r, c + 1, k + 1) or
                dfs(r, c - 1, k + 1)
            )  # Explore

            board[r][c] = temp  # Unchoose: restore
            return found

        for r in range(m):
            for c in range(n):
                if dfs(r, c, 0):
                    return True
        return False
```

---

## 📊 Chapter 4: Explicit Complexity Deconstruction

| Problem | Time Complexity | Auxiliary Stack Space | Auxiliary Heap Space | Governing Formula |
| :--- | :--- | :--- | :--- | :--- |
| **N-Queens** | `O(N!)` | `O(N)` | `O(N)` for hash sets | First row has `N` choices, second has `< N - 1`, etc. |
| **Sudoku Solver** | `O(9^E)` (`E <= 81` empty cells) | `O(E) <= O(81)` | `O(1)` | Upper bound constant `9^81`, pruned in milliseconds |
| **Word Search** | `O(M * N * 3^L)` (`L` = word length) | `O(L)` recursion depth | `O(1)` with in-place masking | At step `k > 0`, at most 3 non-backtracking directions |

### Bitwise Optimization Note for N-Queens
Instead of hash sets, columns and diagonals can be packed into 3 integers (`int cols, diag1, diag2`). Checking and marking collapses from hash table probes into bitwise operations:
- `cols | (1 << c)` marks column `c`.
- `(diag1 | (1 << (r - c + N - 1)))` marks diagonal 1.
This achieves zero allocation and runs up to `10x` faster on modern CPUs.

---

## 🎙️ Chapter 5: 45-Minute Interview Verbal Script

### Step 1: Clarifying Search Objective (00–05 min)
> *"For N-Queens, we want all valid configurations, so we explore the full tree. For Sudoku and Word Search, we only need an existence proof or one valid solution. Therefore, our DFS function must return a boolean so we can immediately prune execution the instant the base condition succeeds."*

### Step 2: Formulating Pruning Invariants (05–15 min)
> *"For N-Queens, checking row-by-row guarantees no horizontal attacks. To enforce vertical and diagonal safety in `O(1)`, I will track occupied columns, `row - col` diagonals, and `row + col` anti-diagonals. For Word Search, instead of allocating a separate `boolean[][] visited` grid, I will mutate the current grid cell to `'#'` and restore it upon unwinding."*

### Step 3: Coding the Recursive Core (15–30 min)
> *"Here is the recursive signature. We check our termination condition first. Next, we loop through candidates. Before branching, we verify validity. If valid, we Choose, recurse, and critically Unchoose. Notice that in Sudoku, if `Solve()` returns true, we bubble `true` up immediately, preventing unnecessary backtracks."*

### Step 4: Edge Cases & Verification (30–45 min)
> *"Edge cases to consider: empty grid, single cell boards (`N = 1`), word longer than total grid area, and unsolvable boards returning false. Space complexity is purely bounded by the call stack height (`O(N)` for N-Queens, `O(L)` for Word Search), guaranteeing zero memory leaks."*

---

## ⚡ Quick Self-Check & Drill

1. **Why does diagonal 1 use `r - c` while diagonal 2 uses `r + c`?**
   *Answer:* Moving diagonally down-right increments both `r` and `c` equally (`r - c` is constant). Moving down-left increments `r` while decrementing `c` (`r + c` is constant).
2. **How does early termination change the backtracking skeleton?**
   *Answer:* The helper returns `bool`. If the recursive call evaluates to `true`, return `true` immediately without undoing the terminal state.
3. **What is the MRV (Minimum Remaining Values) heuristic in Sudoku?**
   *Answer:* Instead of scanning left-to-right, pick the empty cell that has the fewest legal candidate numbers. This minimizes the branching factor at the top of the search tree.

---

> 🧭 **Navigation:** [← Previous Day](file:///d:/Interview_prep/DSA_102/week_13_backtracking_and_branch_bound/Week_13_Day_01_Backtracking_Fundamentals_Instructional.md) • [🏠 Week Overview](file:///d:/Interview_prep/DSA_102/week_13_backtracking_and_branch_bound/README.md) • [📘 Curriculum Syllabus](file:///d:/Interview_prep/DSA_102/COMPLETE_SYLLABUS.md) • [Next Day →](file:///d:/Interview_prep/DSA_102/week_13_backtracking_and_branch_bound/Week_13_Day_03_Branch_And_Bound_Instructional.md)
