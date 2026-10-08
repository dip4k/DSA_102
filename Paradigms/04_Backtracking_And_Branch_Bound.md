# 🧶 Backtracking & Branch and Bound: Explained Simply

> *"Backtracking is exploring a maze with a ball of yarn so you can rewind when you hit a wall. Branch & Bound is doing the same thing, but keeping score so you can abandon hallways that can't possibly beat your high score."*

---

## 💥 The War Story: The Route Dispatcher Out-of-Memory Crash

At an autonomous last-mile drone delivery startup, the routing engine had to calculate valid flight paths delivering medical supplies across a city. The city airspace was partitioned into a 3D waypoint grid.

An engineer implemented the path planner using pure recursive depth-first search:
> *"Try every possible sequence of waypoints from the dispatch center to the hospital, avoiding no-fly zones."*

In sunny weather with few flight restrictions, the tests passed because paths were short.

Then came the blizzard of 2025. City authorities declared **18 emergency no-fly zones** due to construction cranes and fallen electrical lines.

When the dispatch system spun up to route emergency insulin:
1. Every time a drone route approached a no-fly zone, the naive DFS didn't prune the route at the boundary. Instead, it recursed 15 levels deeper, exploring thousands of minor trajectory variations *inside dead-end air corridors*.
2. The search tree exploded to `4^18 = 68,719,476,736` recursive function frames!
3. The server's call stack overflowed. Node.js and .NET worker pods crashed with `OutOfMemoryException`.
4. The entire dispatch system went offline during the worst emergency of the year, leaving critical hospitals stranded.

### The Fix: Pruning & Branch and Bound
The lead engineer rewrote the planner using **Constraint Pruning and Branch & Bound**:
1. **Immediate Feasibility Pruning:** The moment a waypoint came within 500 meters of a no-fly zone, the entire branch was pruned immediately.
2. **Branch & Bound Distance Cutoff:** A simple straight-line Euclidean distance to the hospital served as a lower bound. If `currentDistance + straightLineDistance >= bestCompletedRouteFoundSoFar`, the search pruned the path instantly.
3. Total explored paths dropped from **68 billion to fewer than 1,200**.
4. Path calculation time plummeted from **Out of Memory crash to 34 milliseconds**!

**The Golden Takeaway:** Pure recursion will blow up exponentially. Backtracking succeeds **only because of pruning** — slamming the door on dead ends early. Branch & Bound takes this further by using a scoreboard to abandon paths that cannot beat your current best answer.

---

## 🌽 1. The Everyday Hook: The Maze and the Yarn

Imagine stepping into a giant corn maze:
- You walk forward until you reach a fork with three paths: **Left**, **Middle**, and **Right**.
- You choose **Left** and tie a string of yarn to a post.
- You walk down that path for 50 feet and smack straight into a **dead-end wall**.
- What do you do? You don't give up and burn down the maze. You **wind up your yarn, step back to the fork, and try the Middle path**.

That 3-step action is the heartbeat of **Backtracking**:
1. **Choose:** Take a step down a potential path.
2. **Explore:** Recurse deeper into that choice.
3. **Unchoose:** Rewind your state (undo the move) so other paths can be explored cleanly!

```
                    [ Fork at Intersection ]
                               │
            ┌──────────────────┼──────────────────┐
            ▼                  ▼                  ▼
       Path 1 (Left)     Path 2 (Middle)    Path 3 (Right)
            │                  │                  │
        DEAD END!         VALID EXIT!         DEAD END!
       (Rewind Yarn)       (Success!)        (Rewind Yarn)
```

---

## 🎯 2. The 5 Essential Interview Variations of Backtracking

In FAANG interviews, Backtracking and Branch & Bound questions fall into **5 main variations**:

```
                       ┌───────────────────────────────────────────┐
                       │     BACKTRACKING INTERVIEW VARIATIONS     │
                       └───────────────────────────────────────────┘
                                             │
        ┌───────────────────┬────────────────┴───────────────────┬───────────────────┐
        ▼                   ▼                                   ▼                   ▼
 [ 1. Subsets &       [ 2. Permutations:      [ 3. 2D Grid Puzzles:   [ 4. Board Place-   [ 5. Branch &
      Combinations ]       Orderings ]             DFS Traversal ]         ment: Invariants]   Bound (Score) ]
  • Subsets I & II     • Permutations I & II   • Word Search I & II    • N-Queens          • 0/1 Knapsack B&B
  • Combination Sum    • Letter Combinations   • Sudoku Solver         • Sudoku (MRV)      • TSP MST Bound
```

Let's master each variation.

---

## 📦 Variation 1: Subsets & Combinations (The Pick-or-Skip Pattern)

### The FAANG Problem
Given an integer array `nums` of unique elements, return **all possible subsets (the power set)**.

### The Intuitive Hook: The Light Switch
For every number in the array, you have a light switch:
- Turn it **ON**: Include the number in your current subset.
- Turn it **OFF**: Skip the number.
- At index `i`, branch into two: one branch including `nums[i]`, one branch skipping `nums[i]`.

```
                        [ ] (Index 0: num = 1)
                       /   \
                 Include 1   Skip 1
                    /           \
                 [ 1 ]          [ ]  (Index 1: num = 2)
                /     \        /   \
             [1,2]   [1]     [2]   [ ]  --> All 2^N subsets generated!
```

### C# (.NET 8/9) Code
```csharp
namespace Paradigms.Backtracking;

public static class SubsetsSolver
{
    public static IList<IList<int>> Subsets(int[] nums)
    {
        var result = new List<IList<int>>();
        var current = new List<int>();
        Backtrack(0, nums, current, result);
        return result;
    }

    private static void Backtrack(int startIndex, int[] nums, List<int> current, List<IList<int>> result)
    {
        // Every state in the decision tree is a valid subset
        result.Add(new List<int>(current));

        for (int i = startIndex; i < nums.Length; i++)
        {
            // 1. CHOOSE
            current.Add(nums[i]);

            // 2. EXPLORE
            Backtrack(i + 1, nums, current, result);

            // 3. UNCHOOSE (Rewind yarn)
            current.RemoveAt(current.Count - 1);
        }
    }
}
```

### Python (3.11+) Code
```python
from typing import List

def subsets(nums: List[int]) -> List[List[int]]:
    result = []
    current = []

    def backtrack(start_index: int) -> None:
        result.append(list(current))

        for i in range(start_index, len(nums)):
            # 1. CHOOSE
            current.append(nums[i])
            # 2. EXPLORE
            backtrack(i + 1)
            # 3. UNCHOOSE
            current.pop()

    backtrack(0)
    return result
```

---

## 🔀 Variation 2: Permutations (Orderings with Tracking)

### The FAANG Problem
Given an array `nums` of distinct integers, return **all possible permutations** (all unique orderings).

### The Intuitive Hook: The Hat Draw
Imagine drawing names out of a hat:
- Any unused name can be drawn next.
- Keep a boolean array `used[i]` or swap in-place to ensure no number is used twice in the same permutation.

### C# (.NET 8/9) Code
```csharp
namespace Paradigms.Backtracking;

public static class PermutationsSolver
{
    public static IList<IList<int>> Permute(int[] nums)
    {
        var result = new List<IList<int>>();
        var current = new List<int>();
        bool[] used = new bool[nums.Length];
        Backtrack(nums, used, current, result);
        return result;
    }

    private static void Backtrack(int[] nums, bool[] used, List<int> current, List<IList<int>> result)
    {
        if (current.Count == nums.Length)
        {
            result.Add(new List<int>(current));
            return;
        }

        for (int i = 0; i < nums.Length; i++)
        {
            if (used[i]) continue; // Prune: Already selected

            // 1. CHOOSE
            used[i] = true;
            current.Add(nums[i]);

            // 2. EXPLORE
            Backtrack(nums, used, current, result);

            // 3. UNCHOOSE
            current.RemoveAt(current.Count - 1);
            used[i] = false;
        }
    }
}
```

### Python (3.11+) Code
```python
from typing import List

def permute(nums: List[int]) -> List[List[int]]:
    result = []
    current = []
    used = [False] * len(nums)

    def backtrack() -> None:
        if len(current) == len(nums):
            result.append(list(current))
            return

        for i in range(len(nums)):
            if used[i]:
                continue

            used[i] = True
            current.append(nums[i])
            backtrack()
            current.pop()
            used[i] = False

    backtrack()
    return result
```

---

## 🔤 Variation 3: 2D Grid Exploration (Word Search)

### The FAANG Problem
Given an `m x n` grid of characters and a string `word`, return `true` if `word` exists in the grid (moving horizontally or vertically without reusing the same cell twice in a word).

### The Intuitive Hook: In-Place Board Stamping
Instead of allocating a separate `visited[m][n]` boolean matrix, stamp the grid in-place:
1. When you step on a cell, save its character `char temp = board[r][c]`.
2. Stamp it with `#` so subsequent recursive calls know it is occupied.
3. Explore the 4 neighbors (Up, Down, Left, Right).
4. When backtracking, **un-stamp** the cell by restoring `board[r][c] = temp`!

```
Grid:                   Explore 'A':             Backtrack (Restore):
[ 'A' | 'B' | 'C' ]     [ '#' | 'B' | 'C' ]     [ 'A' | 'B' | 'C' ]
[ 'S' | 'F' | 'C' ] ──► [ 'S' | 'F' | 'C' ] ──► [ 'S' | 'F' | 'C' ]
[ 'A' | 'D' | 'E' ]     [ 'A' | 'D' | 'E' ]     [ 'A' | 'D' | 'E' ]
```

### C# (.NET 8/9) Code
```csharp
namespace Paradigms.Backtracking;

public static class WordSearchSolver
{
    public static bool Exist(char[][] board, string word)
    {
        int rows = board.Length;
        int cols = board[0].Length;

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                if (Dfs(board, word, r, c, 0)) return true;
            }
        }

        return false;
    }

    private static bool Dfs(char[][] board, string word, int r, int c, int index)
    {
        if (index == word.Length) return true; // Found entire word!

        // Bounds and character match check (PRUNING)
        if (r < 0 || r >= board.Length || c < 0 || c >= board[0].Length || board[r][c] != word[index])
        {
            return false;
        }

        // 1. CHOOSE: Stamp cell to mark as visited
        char temp = board[r][c];
        board[r][c] = '#';

        // 2. EXPLORE: 4 directions
        bool found = Dfs(board, word, r + 1, c, index + 1) ||
                     Dfs(board, word, r - 1, c, index + 1) ||
                     Dfs(board, word, r, c + 1, index + 1) ||
                     Dfs(board, word, r, c - 1, index + 1);

        // 3. UNCHOOSE: Restore cell for other search paths
        board[r][c] = temp;

        return found;
    }
}
```

### Python (3.11+) Code
```python
from typing import List

def exist(board: List[List[str]], word: str) -> bool:
    rows, cols = len(board), len(board[0])

    def dfs(r: int, c: int, index: int) -> bool:
        if index == len(word):
            return True

        if r < 0 or r >= rows or c < 0 or c >= cols or board[r][c] != word[index]:
            return False

        temp = board[r][c]
        board[r][c] = "#"  # Stamp

        found = (dfs(r + 1, c, index + 1) or
                 dfs(r - 1, c, index + 1) or
                 dfs(r, c + 1, index + 1) or
                 dfs(r, c - 1, index + 1))

        board[r][c] = temp  # Un-stamp
        return found

    for r in range(rows):
        for c in range(cols):
            if dfs(r, c, 0):
                return True

    return False
```

---

## ♟️ Variation 4: Constraint Placement (N-Queens)

### The FAANG Problem
Place `N` chess queens on an `N x N` board such that **no two queens attack each other**.

### The Intuitive Hook: Diagonal Math
- Rows are handled naturally: place exactly 1 queen per row.
- Columns: track with a set `cols`.
- Diagonals:
  - Top-left to bottom-right diagonal: `row - col` is constant!
  - Top-right to bottom-left anti-diagonal: `row + col` is constant!
- Checking whether a square is under attack takes **`O(1)` time**!

```
Row 0: [ Q | . | . | . ]  --> Col 0, Diag -0, AntiDiag 0
Row 1: [ . | . | Q | . ]  --> Col 2, Diag -1, AntiDiag 3
Row 2: Check squares: Col 0 blocked, Col 2 blocked, Diagonals blocked!
       --> PRUNE AND BACKTRACK!
```

---

## 🏷️ Variation 5: Branch and Bound (Knapsack with Greedy Ceiling)

### The Intuitive Hook: The Scoreboard Cut
Suppose you are filling a backpack with items to maximize value:
- Best completed backpack found so far = **\$200**.
- You are evaluating a partial choice currently worth **\$80**.
- Even if you filled all remaining space with the best items fractionally, the maximum possible theoretical bound is only **\$170**.
- **The B&B Cutoff:** Why bother recursing down this branch? It mathematically **cannot beat \$200**. Prune it immediately!

---

## 📊 Summary Comparison: Backtracking Variations

| Variation | Branching Factor | Depth | Typical Pruning Strategy | Key State Restored |
| :--- | :--- | :--- | :--- | :--- |
| **Subsets** | 2 (Include / Skip) | `N` | Index boundary | `current.RemoveAt(last)` |
| **Permutations** | `N - current` | `N` | `used[i]` boolean check | `used[i] = false` |
| **Word Search** | 4 (Up/Down/Left/Right) | Word Length | Boundary + Char match | `board[r][c] = temp` |
| **N-Queens** | `N` (Columns) | `N` (Rows) | HashSets (`col`, `d1`, `d2`) | `sets.Remove(...)` |
| **Branch & Bound** | 2 (Take / Leave) | `N` | Optimistic ceiling `< best` | Track `bestValue` |
