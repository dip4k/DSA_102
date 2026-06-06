# Week 13 Extended Python Complete v13

Purpose: provide Python implementation support for backtracking, branch and bound, and amortized-analysis-oriented problem solving.

## Focus tags
- Must: backtracking template, pruning, combinations/permutations, grid-search rollback
- Should: priority-queue branch and bound intuition, N-Queens and Sudoku structure
- Optional: advanced optimization examples

## Pattern 1: generic backtracking template
```python
def backtrack(state, is_complete, next_states, record):
    if is_complete(state):
        record(state)
        return
    for nxt in next_states(state):
        backtrack(nxt, is_complete, next_states, record)
```

## Pattern 2: subsets / combinations
```python
def subsets(nums):
    out = []
    path = []
    def dfs(index):
        out.append(path[:])
        for i in range(index, len(nums)):
            path.append(nums[i])
            dfs(i + 1)
            path.pop()
    dfs(0)
    return out
```

## Pattern 3: permutations with used array
```python
def permute(nums):
    out = []
    path = []
    used = [False] * len(nums)
    def dfs():
        if len(path) == len(nums):
            out.append(path[:])
            return
        for i, value in enumerate(nums):
            if used[i]:
                continue
            used[i] = True
            path.append(value)
            dfs()
            path.pop()
            used[i] = False
    dfs()
    return out
```

## Pattern 4: N-Queens structure
```python
def solve_n_queens(n):
    cols, diag1, diag2 = set(), set(), set()
    board = [['.'] * n for _ in range(n)]
    out = []

    def dfs(r):
        if r == n:
            out.append([''.join(row) for row in board])
            return
        for c in range(n):
            if c in cols or r - c in diag1 or r + c in diag2:
                continue
            cols.add(c); diag1.add(r - c); diag2.add(r + c)
            board[r][c] = 'Q'
            dfs(r + 1)
            board[r][c] = '.'
            cols.remove(c); diag1.remove(r - c); diag2.remove(r + c)

    dfs(0)
    return out
```

## Pattern 5: branch and bound sketch
```python
import heapq

# Use tuples like (-bound, node) when you want a max-priority behavior from Python's min-heap.
```

Core B&B idea:
- branch into candidate decisions
- compute a bound for each partial solution
- discard branches that cannot beat the current best

## Amortized reasoning reminders
- dynamic-array append: occasional resize, average constant-time over long sequences
- not every expensive operation implies bad amortized performance

## Practice ladder
- Must: subsets, permutations, combinations, N-Queens, word search
- Should: Sudoku structure, branch-and-bound knapsack, pruning-order experiments
- Optional: TSP bound sketches and mixed backtracking/greedy reasoning
