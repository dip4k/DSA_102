# Backtracking Mastery Curriculum 2.0

## Summary Table

| Level | Mental Model | Pointer/State | Drill Problems |
| --- | --- | --- | --- |
| 1. Subsets | Include/Exclude choice at each index. | `index` (current element), `path` (current subset) | [LeetCode 78: Subsets] (Medium) |
| 2. Permutations | Order matters. Select any unused element. | `used` (visited array/bitmask), `path` (current permutation) | [LeetCode 46: Permutations] (Medium) |
| 3. Combinations | Choose `k` elements from `n`. Order doesn't matter. | `start_index` (to avoid permutations), `path` | [LeetCode 77: Combinations] (Medium) |
| 4. Grid/Matrix | 4-directional DFS with backtracking on visited cells. | `(r, c)` (current cell), `visited` (set/mutated grid) | [LeetCode 79: Word Search] (Medium) |
| 5. State-Pruning | Aggressively prune invalid branches to optimize. | `state` (sum, count, etc.), `index` | [LeetCode 39: Combination Sum] (Medium) |

---

## Level 1: Subsets

### Mental Model & Invariants
*   **What does the state/index mean?** `index` represents the current element in the array we are deciding on. `path` represents the subset formed so far.
*   **What region is processed?** The suffix of the array from `index` to the end.
*   **What is the invariant/recurrence relation?** For element at `index`, we branch twice: Include it, or Exclude it. Base case: `index == len(nums)`.

### Visual State Transitions
Example: `nums = [1, 2]`

| Step | Choice/Action | State (`index`, `path`) | Invariant/Result |
| --- | --- | --- | --- |
| 1 | Start | `0, []` | Initial state |
| 2 | Include 1 | `1, [1]` | Path length 1 |
| 3 | Include 2 | `2, [1, 2]` | Base case: Add `[1, 2]` |
| 4 | Backtrack (pop 2) | `1, [1]` | Restore path |
| 5 | Exclude 2 | `2, [1]` | Base case: Add `[1]` |
| 6 | Backtrack (pop 1) | `0, []` | Restore path |
| 7 | Exclude 1 | `1, []` | Path length 0 |
| 8 | Include 2 | `2, [2]` | Base case: Add `[2]` |
| 9 | Backtrack (pop 2) | `1, []` | Restore path |
| 10 | Exclude 2 | `2, []` | Base case: Add `[]` |

### Code Snippets

Problem: Find all possible subsets of an array of unique integers. The solution involves deciding whether to include or exclude each element.

```python
def subsets(nums):
    res = []
    def backtrack(index, path):
        # 1. Base Case: If index reaches array length, record a copy of the path
        if index == len(nums):
            res.append(path[:])
            return
        
        # 2. Branch 1: Include current element
        path.append(nums[index])
        # 3. Recurse with element included
        backtrack(index + 1, path)
        # 4. Backtrack: remove included element to restore state
        path.pop()
        
        # 5. Branch 2: Exclude current element and recurse
        backtrack(index + 1, path)
        
    backtrack(0, [])
    return res
```

```csharp
public IList<IList<int>> Subsets(int[] nums) {
    var res = new List<IList<int>>();
    void Backtrack(int index, List<int> path) {
        // 1. Base Case: If index reaches array length, record a copy of the path
        if (index == nums.Length) {
            res.Add(new List<int>(path));
            return;
        }
        
        // 2. Branch 1: Include current element
        path.Add(nums[index]);
        // 3. Recurse with element included
        Backtrack(index + 1, path);
        // 4. Backtrack: remove included element to restore state
        path.RemoveAt(path.Count - 1);
        
        // 5. Branch 2: Exclude current element and recurse
        Backtrack(index + 1, path);
    }
    Backtrack(0, new List<int>());
    return (IList<IList<int>>)res;
}
```

### ⚠️ Gotchas & Pitfalls
*   **Reference Copying Bug:** Appending the `path` reference instead of a deep copy (`path[:]` or `new List<int>(path)`). The final result will contain empty or identical lists.
*   **Missing Pop:** Forgetting to pop the element (`path.pop()`) after the recursive call, which ruins the backtracking state for the next branches.

### Drill Problems
*   [LeetCode 78: Subsets] (Medium)
*   [LeetCode 90: Subsets II] (Medium)

---

## Level 2: Permutations

### Mental Model & Invariants
*   **What does the state/index mean?** `path` holds the current sequence. We don't use `index` like in subsets; instead, we use a `used` structure (array/set) to track which elements are already in the `path`.
*   **What region is processed?** All available (unused) elements in the array at every step.
*   **What is the invariant/recurrence relation?** If `len(path) == len(nums)`, a valid permutation is formed. Otherwise, iterate through all `nums`, skip if `used`, add to `path`, recurse, and backtrack.

### Visual State Transitions
Example: `nums = [1, 2]`

| Step | Choice/Action | State (`used`, `path`) | Invariant/Result |
| --- | --- | --- | --- |
| 1 | Start | `{}, []` | Initial state |
| 2 | Pick 1 | `{1}, [1]` | Path length 1 |
| 3 | Pick 2 | `{1, 2}, [1, 2]` | Base case: Add `[1, 2]` |
| 4 | Backtrack 2 | `{1}, [1]` | Restore |
| 5 | Backtrack 1 | `{}, []` | Restore |
| 6 | Pick 2 | `{2}, [2]` | Path length 1 |
| 7 | Pick 1 | `{2, 1}, [2, 1]` | Base case: Add `[2, 1]` |

### Code Snippets

Problem: Find all possible permutations of an array of distinct integers. The solution builds permutations by picking any unused element at each step.

```python
def permute(nums):
    res = []
    def backtrack(path, used):
        # 1. Base case: if path length equals array length, record a copy of the path
        if len(path) == len(nums):
            res.append(path[:])
            return
            
        # 2. Iterate through all elements for choices
        for i in range(len(nums)):
            # 3. Skip if element is already used in the current path
            if used[i]: continue
            
            # 4. Mark element as used and add to path
            used[i] = True
            path.append(nums[i])
            
            # 5. Recurse to build the rest of the permutation
            backtrack(path, used)
            
            # 6. Backtrack: remove from path and unmark as used
            path.pop()
            used[i] = False
            
    backtrack([], [False]*len(nums))
    return res
```

```csharp
public IList<IList<int>> Permute(int[] nums) {
    var res = new List<IList<int>>();
    var used = new bool[nums.Length];
    void Backtrack(List<int> path) {
        // 1. Base case: if path length equals array length, record a copy of the path
        if (path.Count == nums.Length) {
            res.Add(new List<int>(path));
            return;
        }
        
        // 2. Iterate through all elements for choices
        for (int i = 0; i < nums.Length; i++) {
            // 3. Skip if element is already used in the current path
            if (used[i]) continue;
            
            // 4. Mark element as used and add to path
            used[i] = true;
            path.Add(nums[i]);
            
            // 5. Recurse to build the rest of the permutation
            Backtrack(path);
            
            // 6. Backtrack: remove from path and unmark as used
            path.RemoveAt(path.Count - 1);
            used[i] = false;
        }
    }
    Backtrack(new List<int>());
    return (IList<IList<int>>)res;
}
```

### ⚠️ Gotchas & Pitfalls
*   **Loop Starting Point:** Starting the `for` loop at `index` instead of `0`. Permutations need to consider all elements every time.
*   **State Un-marking:** Forgetting to toggle `used[i] = False` after the recursive call.

### Drill Problems
*   [LeetCode 46: Permutations] (Medium)
*   [LeetCode 47: Permutations II] (Medium)

---

## Level 3: Combinations

### Mental Model & Invariants
*   **What does the state/index mean?** `start_index` enforces order to avoid duplicate sets (e.g., `[1, 2]` is the same combination as `[2, 1]`). `path` stores current items.
*   **What region is processed?** Elements from `start_index` to `n`.
*   **What is the invariant/recurrence relation?** Iterate `i` from `start_index` to `n`. Pick `i`, recurse with `start_index = i + 1`. Stop when `len(path) == k`.

### Visual State Transitions
Example: `n = 3, k = 2`

| Step | Choice/Action | State (`start`, `path`) | Invariant/Result |
| --- | --- | --- | --- |
| 1 | Start | `1, []` | Initial state |
| 2 | Pick 1 | `2, [1]` | Path length 1 |
| 3 | Pick 2 | `3, [1, 2]` | Base case: Add `[1, 2]` |
| 4 | Backtrack 2 | `3, [1]` | Restore |
| 5 | Pick 3 | `4, [1, 3]` | Base case: Add `[1, 3]` |
| 6 | Backtrack 3 | `4, [1]` | Restore |
| 7 | Backtrack 1 | `2, []` | Restore |
| 8 | Pick 2 | `3, [2]` | Path length 1 |
| 9 | Pick 3 | `4, [2, 3]` | Base case: Add `[2, 3]` |

### Code Snippets

Problem: Return all possible combinations of k numbers chosen from the range [1, n]. The solution enforces order by maintaining a start index to prevent duplicate combinations.

```python
def combine(n, k):
    res = []
    def backtrack(start, path):
        # 1. Base case: combination of required size k is formed
        if len(path) == k:
            res.append(path[:])
            return
            
        # 2. Iterate from start index to n to avoid duplicates and permutations
        for i in range(start, n + 1):
            # 3. Include current number
            path.append(i)
            # 4. Recurse with next index
            backtrack(i + 1, path)
            # 5. Backtrack: remove current number
            path.pop()
            
    backtrack(1, [])
    return res
```

```csharp
public IList<IList<int>> Combine(int n, int k) {
    var res = new List<IList<int>>();
    void Backtrack(int start, List<int> path) {
        // 1. Base case: combination of required size k is formed
        if (path.Count == k) {
            res.Add(new List<int>(path));
            return;
        }
        
        // 2. Iterate from start index to n to avoid duplicates and permutations
        for (int i = start; i <= n; i++) {
            // 3. Include current number
            path.Add(i);
            // 4. Recurse with next index
            Backtrack(i + 1, path);
            // 5. Backtrack: remove current number
            path.RemoveAt(path.Count - 1);
        }
    }
    Backtrack(1, new List<int>());
    return (IList<IList<int>>)res;
}
```

### ⚠️ Gotchas & Pitfalls
*   **Permutation overlap:** Starting the loop at `1` instead of `start`. This generates permutations instead of combinations.
*   **Missed Pruning (Optimization):** Continuing the loop when there aren't enough remaining elements to form a size `k` combination. The loop bound can be optimized to `n - (k - len(path)) + 1`.

### Drill Problems
*   [LeetCode 77: Combinations] (Medium)
*   [LeetCode 216: Combination Sum III] (Medium)

---

## Level 4: Grid/Matrix Backtracking

### Mental Model & Invariants
*   **What does the state/index mean?** `(r, c)` defines current cell. A `visited` matrix or in-place modification (like `board[r][c] = '#'`) prevents infinite cycles.
*   **What region is processed?** The 4 immediate neighbors (up, down, left, right).
*   **What is the invariant/recurrence relation?** If cell is out of bounds or invalid, return `False`. Mark cell, recurse 4 directions. If any returns `True`, bubble up `True`. Otherwise, un-mark cell and return `False`.

### Visual State Transitions
Example: Word Search for "AB"

| Step | Choice/Action | State (`(r,c)`, `visited`) | Invariant/Result |
| --- | --- | --- | --- |
| 1 | Visit 'A' | `(0,0)`, `{(0,0)}` | Match 'A' |
| 2 | Move Right | `(0,1)`, `{(0,0), (0,1)}` | Match 'B', word done. Return True |
| 3 | (Alternative) Move Down | `(1,0)`, `{(0,0), (1,0)}` | Not 'B', Return False |
| 4 | Backtrack 'A' | `(0,0)`, `{}` | Restore visited |

### Code Snippets

Problem: Determine if a given word exists in an m x n grid of characters, where words are formed by adjacent letters horizontally or vertically.

```python
def exist(board, word):
    ROWS, COLS = len(board), len(board[0])
    
    def backtrack(r, c, i):
        # 1. Base case: successfully found all characters of the word
        if i == len(word): return True
        
        # 2. Base cases for boundary violations or character mismatch
        if (r < 0 or c < 0 or r >= ROWS or c >= COLS or board[r][c] != word[i]):
            return False
            
        # 3. Temporarily mark cell as visited to prevent reusing it
        temp = board[r][c]
        board[r][c] = '#' 
        
        # 4. Explore all 4 adjacent directions (DFS)
        res = (backtrack(r+1, c, i+1) or
               backtrack(r-1, c, i+1) or
               backtrack(r, c+1, i+1) or
               backtrack(r, c-1, i+1))
               
        # 5. Backtrack: restore the cell's original value
        board[r][c] = temp
        return res
        
    for r in range(ROWS):
        for c in range(COLS):
            if backtrack(r, c, 0): return True
    return False
```

```csharp
public bool Exist(char[][] board, string word) {
    int rows = board.Length, cols = board[0].Length;
    
    bool Backtrack(int r, int c, int i) {
        // 1. Base case: successfully found all characters of the word
        if (i == word.Length) return true;
        
        // 2. Base cases for boundary violations or character mismatch
        if (r < 0 || c < 0 || r >= rows || c >= cols || board[r][c] != word[i])
            return false;
            
        // 3. Temporarily mark cell as visited to prevent reusing it
        char temp = board[r][c];
        board[r][c] = '#'; 
        
        // 4. Explore all 4 adjacent directions (DFS)
        bool res = Backtrack(r+1, c, i+1) || Backtrack(r-1, c, i+1) ||
                   Backtrack(r, c+1, i+1) || Backtrack(r, c-1, i+1);
                   
        // 5. Backtrack: restore the cell's original value
        board[r][c] = temp; 
        return res;
    }
    
    for (int r = 0; r < rows; r++)
        for (int c = 0; c < cols; c++)
            if (Backtrack(r, c, 0)) return true;
            
    return false;
}
```

### ⚠️ Gotchas & Pitfalls
*   **In-Place Modification Overwrite:** Failing to restore `board[r][c] = temp` accurately after backtracking, corrupting the grid for future searches.
*   **Early Exit Failure:** Not immediately returning `True` when a valid path is found, causing unnecessary TLE on large grids.

### Drill Problems
*   [LeetCode 79: Word Search] (Medium)
*   [LeetCode 212: Word Search II] (Hard)

---

## Level 5: State-Pruning

### Mental Model & Invariants
*   **What does the state/index mean?** `current_sum` or running constraint tracker. `start_index` to prevent duplicate combinations if elements can be reused.
*   **What region is processed?** Remaining elements that do not violate constraints.
*   **What is the invariant/recurrence relation?** If `sum == target`, record path. If `sum > target`, **PRUNE** (return early). Otherwise branch.

### Visual State Transitions
Example: `candidates = [2,3,6]`, `target = 7`

| Step | Choice/Action | State (`sum`, `path`) | Invariant/Result |
| --- | --- | --- | --- |
| 1 | Start | `0, []` | Initial state |
| 2 | Pick 2 | `2, [2]` | Valid |
| 3 | Pick 2 | `4, [2, 2]` | Valid |
| 4 | Pick 2 | `6, [2, 2, 2]` | Valid |
| 5 | Pick 2 | `8, [2, 2, 2, 2]` | Prune: > 7 |
| 6 | Backtrack | `6, [2, 2, 2]` | Restore |
| 7 | Pick 3 | `9, [2, 2, 2, 3]` | Prune: > 7 |

### Code Snippets

Problem: Find all unique combinations of candidates that sum up to a specific target, where candidates can be chosen an unlimited number of times. The solution aggressively prunes invalid branches by sorting inputs and stopping early.

```python
def combinationSum(candidates, target):
    res = []
    # Sorting helps prune earlier
    candidates.sort()
    
    def backtrack(start, current_sum, path):
        # 1. Base case: target sum is exactly reached
        if current_sum == target:
            res.append(path[:])
            return
        
        # 2. Iterate starting from current index to allow reuse but prevent permutations
        for i in range(start, len(candidates)):
            # 3. State Pruning: stop early if adding candidate exceeds target
            if current_sum + candidates[i] > target:
                break 
                
            # 4. Include current candidate
            path.append(candidates[i])
            # 5. Recurse, passing 'i' (not 'i+1') to allow reusing the current element
            backtrack(i, current_sum + candidates[i], path)
            # 6. Backtrack: remove current candidate
            path.pop()
            
    backtrack(0, 0, [])
    return res
```

```csharp
public IList<IList<int>> CombinationSum(int[] candidates, int target) {
    var res = new List<IList<int>>();
    Array.Sort(candidates); // Helps pruning
    
    void Backtrack(int start, int currentSum, List<int> path) {
        // 1. Base case: target sum is exactly reached
        if (currentSum == target) {
            res.Add(new List<int>(path));
            return;
        }
        
        // 2. Iterate starting from current index to allow reuse but prevent permutations
        for (int i = start; i < candidates.Length; i++) {
            // 3. State Pruning: stop early if adding candidate exceeds target
            if (currentSum + candidates[i] > target) break;
            
            // 4. Include current candidate
            path.Add(candidates[i]);
            // 5. Recurse, passing 'i' (not 'i+1') to allow reusing the current element
            Backtrack(i, currentSum + candidates[i], path);
            // 6. Backtrack: remove current candidate
            path.RemoveAt(path.Count - 1);
        }
    }
    
    Backtrack(0, 0, new List<int>());
    return (IList<IList<int>>)res;
}
```

### ⚠️ Gotchas & Pitfalls
*   **Duplicate Trees:** Passing `0` instead of `i` in the recursive call when reusing elements, leading to redundant permutations (e.g. `[2, 3]` and `[3, 2]`).
*   **Sorting Omission:** Pruning via `break` instead of `continue` requires a sorted array. If array isn't sorted, `break` might prematurely terminate valid paths.

### Drill Problems
*   [LeetCode 39: Combination Sum] (Medium)
*   [LeetCode 40: Combination Sum II] (Medium)
