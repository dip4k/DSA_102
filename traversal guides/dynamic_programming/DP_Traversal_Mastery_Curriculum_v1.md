# Dynamic Programming: Traversal Mastery Curriculum

## 📊 Curriculum Summary

| Level | Mental Model | Pointer / State | Drill Problems |
| :--- | :--- | :--- | :--- |
| **L1: 1D DP** | A sequence of choices where current choice depends on recent past choices. | `i`: the current step or index in a 1D sequence. | [LeetCode 70: Climbing Stairs] (Easy)<br>[LeetCode 198: House Robber] (Medium) |
| **L2: 2D Grid DP** | Moving through a spatial grid where paths converge from top/left. | `(r, c)`: current row and column in the grid. | [LeetCode 62: Unique Paths] (Medium)<br>[LeetCode 64: Minimum Path Sum] (Medium) |
| **L3: 2D String DP** | Matching or transforming one string into another by comparing prefixes/suffixes. | `(i, j)`: lengths of prefixes for `word1` and `word2`. | [LeetCode 1143: Longest Common Subsequence] (Medium)<br>[LeetCode 72: Edit Distance] (Medium) |
| **L4: State Machine DP** | Managing multiple parallel states at each step (e.g., holding/not holding stock). | `(i, state)`: step `i` and categorical `state`. | [LeetCode 122: Best Time to Buy and Sell Stock II] (Medium)<br>[LeetCode 309: Best Time to Buy and Sell Stock with Cooldown] (Medium) |
| **L5: Knapsack DP** | Selecting items under a constraint (capacity/weight) to maximize/minimize value. | `(i, w)`: considering items up to `i` with weight limit `w`. | [LeetCode 416: Partition Equal Subset Sum] (Medium)<br>[LeetCode 322: Coin Change] (Medium) |
| **L6: Tree DP** | Bottom-up aggregation of information from children to parent in a tree. | `node`: the current subtree root. | [LeetCode 337: House Robber III] (Medium)<br>[LeetCode 124: Binary Tree Maximum Path Sum] (Hard) |

---

## Level 1: 1D Dynamic Programming

- **What does the state/index mean?** `dp[i]` represents the optimal solution for a sequence of length `i` or ending at index `i`.
- **What region is processed?** The prefix of the sequence `[0...i]`.
- **What is the invariant/recurrence relation?** `dp[i] = max(dp[i-1], dp[i-2] + nums[i])` (e.g., House Robber).

### Visual State Transition (House Robber: `nums = [2, 7, 9, 3, 1]`)
| Step `i` | Choice / Action | DP State `[dp[i-1], dp[i-2] + nums[i]]` | Invariant |
| :--- | :--- | :--- | :--- |
| 0 (`val=2`) | Take `2` | `dp[0] = max(0, 0 + 2) = 2` | Max profit up to index 0 is 2 |
| 1 (`val=7`) | Skip `2`, Take `7` | `dp[1] = max(2, 0 + 7) = 7` | Max profit up to index 1 is 7 |
| 2 (`val=9`) | Take `9` + `dp[0]` | `dp[2] = max(7, 2 + 9) = 11` | Max profit up to index 2 is 11 |
| 3 (`val=3`) | Skip `3`, keep `dp[2]` | `dp[3] = max(11, 7 + 3) = 11` | Max profit up to index 3 is 11 |

### Code Snippets

Problem: Maximize the total amount of money you can rob tonight without robbing adjacent houses.

```python
def rob(nums: list[int]) -> int:
    # 1. Handle base cases
    if not nums: return 0
    if len(nums) == 1: return nums[0]
    
    # 2. Initialize state for prev2 (dp[i-2]) and prev1 (dp[i-1])
    prev2, prev1 = 0, 0
    for num in nums:
        # 3. Calculate max money for current house (skip vs. rob)
        curr = max(prev1, prev2 + num)
        # 4. Advance pointers/state for the next iteration
        prev2 = prev1
        prev1 = curr
        
    # 5. Return optimal solution for sequence
    return prev1
```
```csharp
public int Rob(int[] nums) {
    // 1. Handle base cases
    if (nums == null || nums.Length == 0) return 0;
    
    // 2. Initialize state for prev2 (dp[i-2]) and prev1 (dp[i-1])
    int prev2 = 0, prev1 = 0;
    foreach (int num in nums) {
        // 3. Calculate max money for current house (skip vs. rob)
        int curr = Math.Max(prev1, prev2 + num);
        // 4. Advance pointers/state for the next iteration
        prev2 = prev1;
        prev1 = curr;
    }
    
    // 5. Return optimal solution for sequence
    return prev1;
}
```

### ⚠️ Gotchas & Pitfalls
- **Base Case Initialization:** Forgetting to handle `n=0` or `n=1` explicitly often causes `IndexOutOfBounds`.
- **State Overwrite:** When optimizing space to $O(1)$, overwriting `prev1` before assigning `prev2` will break the recurrence.
- **Index Alignment:** Confusing whether `dp[i]` corresponds to the $i$-th element of the array or length $i$.

### Drill Problems
- [LeetCode 70: Climbing Stairs] (Easy)
- [LeetCode 198: House Robber] (Medium)

---

## Level 2: 2D Grid DP

- **What does the state/index mean?** `dp[r][c]` represents the optimal path cost/count from `(0, 0)` to `(r, c)`.
- **What region is processed?** A bounding box from the origin to `(r, c)`.
- **What is the invariant/recurrence relation?** `dp[r][c] = dp[r-1][c] + dp[r][c-1]` (e.g., Unique Paths).

### Visual State Transition (Unique Paths: `3x3` grid)
| Step `(r, c)` | Choice / Action | DP State `dp[r-1][c] + dp[r][c-1]` | Invariant |
| :--- | :--- | :--- | :--- |
| (0, 1) | Come from left | `dp[0][1] = 0 + 1 = 1` | 1 way to reach top border |
| (1, 0) | Come from top | `dp[1][0] = 1 + 0 = 1` | 1 way to reach left border |
| (1, 1) | Top + Left | `dp[1][1] = dp[0][1] + dp[1][0] = 2`| 2 ways to reach middle cell |

### Code Snippets

Problem: Find the total number of unique paths from the top-left corner to the bottom-right corner of an m x n grid, moving only down or right.

```python
def uniquePaths(m: int, n: int) -> int:
    # 1. Initialize 1D DP array representing the previous row state
    dp = [1] * n
    # 2. Iterate through rows starting from 1 (0th row is all 1s)
    for r in range(1, m):
        # 3. Iterate through columns starting from 1
        for c in range(1, n):
            # 4. Invariant: path count is paths from left (dp[c-1]) + paths from top (dp[c])
            dp[c] += dp[c-1]
            
    # 5. Return the last element which represents the bottom-right corner
    return dp[-1]
```
```csharp
public int UniquePaths(int m, int n) {
    // 1. Initialize 1D DP array representing the previous row state
    int[] dp = new int[n];
    Array.Fill(dp, 1);
    
    // 2. Iterate through rows starting from 1 (0th row is all 1s)
    for (int r = 1; r < m; r++) {
        // 3. Iterate through columns starting from 1
        for (int c = 1; c < n; c++) {
            // 4. Invariant: path count is paths from left (dp[c-1]) + paths from top (dp[c])
            dp[c] += dp[c-1];
        }
    }
    
    // 5. Return the last element which represents the bottom-right corner
    return dp[n-1];
}
```

### ⚠️ Gotchas & Pitfalls
- **Grid Boundary Checks:** Accessing `r-1` or `c-1` on the 0th row/column without padding or conditional checks.
- **Space Optimization Trap:** Updating `dp[c]` using the same array only works if you need `dp[r-1][c]` (old value) and `dp[r][c-1]` (new value). If dependencies change (e.g., diagonals), you need a temporary variable.
- **Obstacle Handling:** Forgetting to set `dp[r][c] = 0` when encountering an obstacle, causing invalid paths to propagate.

### Drill Problems
- [LeetCode 62: Unique Paths] (Medium)
- [LeetCode 64: Minimum Path Sum] (Medium)

---

## Level 3: 2D String DP

- **What does the state/index mean?** `dp[i][j]` is the optimal solution for the prefix of `word1` up to length `i` and `word2` up to length `j`.
- **What region is processed?** Substrings `word1[0...i-1]` and `word2[0...j-1]`.
- **What is the invariant/recurrence relation?** If `word1[i-1] == word2[j-1]`: `dp[i-1][j-1] + 1`, else: `max(dp[i-1][j], dp[i][j-1])` (e.g., LCS).

### Visual State Transition (LCS: `word1="abc"`, `word2="ahc"`)
| Step `(i, j)` | Chars | Choice / Action | DP State | Invariant |
| :--- | :--- | :--- | :--- | :--- |
| (1, 1) | `a`, `a` | Match | `dp[1][1] = dp[0][0] + 1 = 1` | "a" matches "a" |
| (2, 2) | `b`, `h` | Mismatch | `dp[2][2] = max(dp[1][2], dp[2][1]) = 1`| Take max of dropping 'b' or 'h' |
| (3, 3) | `c`, `c` | Match | `dp[3][3] = dp[2][2] + 1 = 2` | "c" matches "c", add to previous max |

### Code Snippets

Problem: Find the length of the longest common subsequence between two strings.

```python
def longestCommonSubsequence(text1: str, text2: str) -> int:
    m, n = len(text1), len(text2)
    # 1. Initialize a (m+1) x (n+1) DP matrix with zeros
    dp = [[0] * (n + 1) for _ in range(m + 1)]
    
    # 2. Iterate through each character of both strings (1-based index)
    for i in range(1, m + 1):
        for j in range(1, n + 1):
            # 3. If characters match, extend the LCS by 1
            if text1[i-1] == text2[j-1]:
                dp[i][j] = dp[i-1][j-1] + 1
            # 4. If mismatched, take the max of skipping either character
            else:
                dp[i][j] = max(dp[i-1][j], dp[i][j-1])
                
    # 5. Result is at the bottom-right of the matrix
    return dp[m][n]
```
```csharp
public int LongestCommonSubsequence(string text1, string text2) {
    int m = text1.Length, n = text2.Length;
    // 1. Initialize a (m+1) x (n+1) DP matrix with zeros
    int[,] dp = new int[m + 1, n + 1];
    
    // 2. Iterate through each character of both strings (1-based index)
    for (int i = 1; i <= m; i++) {
        for (int j = 1; j <= n; j++) {
            // 3. If characters match, extend the LCS by 1
            if (text1[i-1] == text2[j-1]) {
                dp[i, j] = dp[i-1, j-1] + 1;
            } else {
                // 4. If mismatched, take the max of skipping either character
                dp[i, j] = Math.Max(dp[i-1, j], dp[i, j-1]);
            }
        }
    }
    
    // 5. Result is at the bottom-right of the matrix
    return dp[m, n];
}
```

### ⚠️ Gotchas & Pitfalls
- **1-Based Indexing for DP Matrix:** Using 0-based loops often leads to messy `i-1`/`j-1` boundary checks. It's cleaner to pad the matrix with an extra row and column (length + 1).
- **String Indexing with 1-Based Matrix:** Remember that `dp[i][j]` maps to `word[i-1]` and `word[j-1]`.
- **Misidentifying Diagonal State:** `dp[i-1][j-1]` implies processing *both* characters. In Edit Distance, this is a replacement/match, not an insertion/deletion.

### Drill Problems
- [LeetCode 1143: Longest Common Subsequence] (Medium)
- [LeetCode 72: Edit Distance] (Medium)

---

## Level 4: State Machine DP

- **What does the state/index mean?** `dp[i][state]` is the optimal value at step `i` given a specific categorical `state` (e.g., Holding Stock vs. Empty).
- **What region is processed?** Prefix `[0...i]` while explicitly tracking parallel realities.
- **What is the invariant/recurrence relation?** `hold[i] = max(hold[i-1], empty[i-1] - price[i])`

### Visual State Transition (Stock II: `prices = [7, 1, 5]`)
| Step `i` | Price | Choice / Action | DP State `(Empty, Hold)` | Invariant |
| :--- | :--- | :--- | :--- | :--- |
| 0 | 7 | Buy or do nothing | `E=0, H=-7` | Max cash is 0, max hold is -7 |
| 1 | 1 | Sell (loss) or Buy | `E=0, H=max(-7, 0-1) = -1` | Holding 1 is better than holding 7 |
| 2 | 5 | Sell 1 for 5 | `E=max(0, -1+5) = 4, H=-1` | Max cash is 4 |

### Code Snippets

Problem: Maximize profit by buying and selling a stock multiple times, holding at most one share at a time.

```python
def maxProfit(prices: list[int]) -> int:
    # 1. Initialize states for day 0 (cannot hold without buying, so -inf)
    empty, hold = 0, -float('inf')
    
    for p in prices:
        # 2. Calculate next state: continue empty or sell currently held stock
        next_empty = max(empty, hold + p)
        # 3. Calculate next state: continue holding or buy new stock
        next_hold = max(hold, empty - p)
        # 4. Update states simultaneously
        empty, hold = next_empty, next_hold
        
    # 5. Maximum profit will always be when not holding stock at the end
    return empty
```
```csharp
public int MaxProfit(int[] prices) {
    // 1. Initialize states for day 0 (cannot hold without buying, so min value)
    int empty = 0, hold = int.MinValue;
    
    foreach (int p in prices) {
        // 2. Calculate next state: continue empty or sell currently held stock
        int nextEmpty = Math.Max(empty, hold == int.MinValue ? empty : hold + p);
        // 3. Calculate next state: continue holding or buy new stock
        int nextHold = Math.Max(hold, empty - p);
        // 4. Update states simultaneously
        empty = nextEmpty;
        hold = nextHold;
    }
    
    // 5. Maximum profit will always be when not holding stock at the end
    return empty;
}
```

### ⚠️ Gotchas & Pitfalls
- **Simultaneous Updates:** Updating `empty` and using the *new* `empty` to calculate `hold` on the same step. Always use temporary variables or tuple unpacking.
- **Initialization of Invalid States:** A state like `hold` at day 0 must be initialized to `-infinity`, not `0`, because it's impossible to hold a stock without buying it.
- **Cooldowns/Constraints:** Failing to reference the correct historical state (e.g., `empty[i-2]` instead of `empty[i-1]` for a 1-day cooldown).

### Drill Problems
- [LeetCode 122: Best Time to Buy and Sell Stock II] (Medium)
- [LeetCode 309: Best Time to Buy and Sell Stock with Cooldown] (Medium)

---

## Level 5: Knapsack DP

- **What does the state/index mean?** `dp[w]` is the max value / min coins / boolean possibility for exactly/up to capacity `w`.
- **What region is processed?** Capacity constraints `1` to `W` considering the first `i` items.
- **What is the invariant/recurrence relation?** `dp[w] = dp[w] || dp[w - weight[i]]` (e.g., Subset Sum).

### Visual State Transition (Coin Change: `coins=[1, 2]`, `amount=3`)
| Step (Coin, `w`) | Choice / Action | DP State `min(dp[w], dp[w-c] + 1)` | Invariant |
| :--- | :--- | :--- | :--- |
| (c=1, w=1) | Use 1 | `dp[1] = dp[0] + 1 = 1` | Min coins for 1 is 1 |
| (c=1, w=2) | Use 1 | `dp[2] = dp[1] + 1 = 2` | Min coins for 2 is 2 |
| (c=2, w=2) | Use 2 | `dp[2] = min(2, dp[0] + 1) = 1` | Overwrite: Min coins for 2 is 1 |
| (c=2, w=3) | Use 2 | `dp[3] = min(inf, dp[1] + 1) = 2`| Min coins for 3 is 2 |

### Code Snippets

Problem: Find the minimum number of coins needed to make up a given amount. Return -1 if impossible.

```python
def coinChange(coins: list[int], amount: int) -> int:
    # 1. Initialize DP array with infinity, since we are minimizing
    dp = [float('inf')] * (amount + 1)
    # 2. Base case: 0 coins needed for amount 0
    dp[0] = 0
    
    # 3. Iterate through each available coin
    for coin in coins:
        # 4. Update the DP array for capacities starting from current coin weight
        for w in range(coin, amount + 1):
            # 5. Choice: do not use coin vs. use coin (add 1 to optimal subproblem)
            dp[w] = min(dp[w], dp[w - coin] + 1)
            
    # 6. Return answer if valid, otherwise -1
    return dp[amount] if dp[amount] != float('inf') else -1
```
```csharp
public int CoinChange(int[] coins, int amount) {
    // 1. Initialize DP array with a max value, since we are minimizing
    int[] dp = new int[amount + 1];
    Array.Fill(dp, amount + 1);
    // 2. Base case: 0 coins needed for amount 0
    dp[0] = 0;
    
    // 3. Iterate through each available coin
    foreach (int coin in coins) {
        // 4. Update the DP array for capacities starting from current coin weight
        for (int w = coin; w <= amount; w++) {
            // 5. Choice: do not use coin vs. use coin (add 1 to optimal subproblem)
            dp[w] = Math.Min(dp[w], dp[w - coin] + 1);
        }
    }
    
    // 6. Return answer if valid, otherwise -1
    return dp[amount] > amount ? -1 : dp[amount];
}
```

### ⚠️ Gotchas & Pitfalls
- **0/1 Knapsack vs. Unbounded:** Looping capacity left-to-right allows reusing the same item (Unbounded Knapsack). Looping right-to-left prevents reuse (0/1 Knapsack).
- **Initialization Value:** Using `0` for minimization problems instead of `infinity`, causing the recurrence to always return `0`.
- **Inner vs. Outer Loops:** Swapping the item loop and capacity loop changes whether you are finding combinations (order doesn't matter) vs. permutations (order matters).

### Drill Problems
- [LeetCode 416: Partition Equal Subset Sum] (Medium)
- [LeetCode 322: Coin Change] (Medium)

---

## Level 6: Tree DP

- **What does the state/index mean?** `dfs(node)` returns the optimal states for the subtree rooted at `node`.
- **What region is processed?** Subtrees bottom-up (Post-order Traversal).
- **What is the invariant/recurrence relation?** `state_node = combine(state_left, state_right, node.val)`

### Visual State Transition (House Robber III)
| Step `Node` | Choice / Action | DP State `(Rob, Skip)` | Invariant |
| :--- | :--- | :--- | :--- |
| Leaf (L) | Base calculation | `(val, 0)` | Rob gives val, skip gives 0 |
| Leaf (R) | Base calculation | `(val, 0)` | Rob gives val, skip gives 0 |
| Parent | Combine subtrees | `Rob = val + L.Skip + R.Skip` <br> `Skip = max(L) + max(R)`| Parent choices depend on children states |

### Code Snippets

Problem: Maximize the total amount of money robbed from houses arranged in a binary tree, without robbing directly linked (parent-child) houses.

```python
# class TreeNode:
#     def __init__(self, val=0, left=None, right=None):
#         self.val = val
#         self.left = left
#         self.right = right

def rob(root: Optional[TreeNode]) -> int:
    # 1. Define DFS to return optimal states (rob_this, skip_this)
    def dfs(node):
        # 2. Base case: null node yields 0 for both choices
        if not node:
            return (0, 0) # (rob_this, skip_this)
            
        # 3. Post-order traversal: process left and right subtrees
        left = dfs(node.left)
        right = dfs(node.right)
        
        # 4. Recurrence: If we rob this node, we MUST skip children
        rob_this = node.val + left[1] + right[1]
        # 5. Recurrence: If we skip this node, we can take max of child choices
        skip_this = max(left) + max(right)
        
        # 6. Return combined state to parent
        return (rob_this, skip_this)
        
    # 7. Start from root and return the maximum of the two states
    return max(dfs(root))
```
```csharp
public int Rob(TreeNode root) {
    // 1. Start from root and return the maximum of the two states
    int[] res = Dfs(root);
    return Math.Max(res[0], res[1]);
}

// Returns {rob_this, skip_this}
private int[] Dfs(TreeNode node) {
    // 2. Base case: null node yields 0 for both choices
    if (node == null) return new int[] {0, 0};
    
    // 3. Post-order traversal: process left and right subtrees
    int[] left = Dfs(node.left);
    int[] right = Dfs(node.right);
    
    // 4. Recurrence: If we rob this node, we MUST skip children
    int robThis = node.val + left[1] + right[1];
    // 5. Recurrence: If we skip this node, we can take max of child choices
    int skipThis = Math.Max(left[0], left[1]) + Math.Max(right[0], right[1]);
    
    // 6. Return combined state to parent
    return new int[] {robThis, skipThis};
}
```

### ⚠️ Gotchas & Pitfalls
- **Returning Global State Instead of Subtree State:** Modifying a global maximum variable incorrectly during back-propagation instead of isolating subtree states.
- **Null Node Base Cases:** Returning `-infinity` for null nodes can break sum-based recurrences. Usually `0` or neutral values are required.
- **Top-Down vs. Bottom-Up:** Attempting to pass accumulated state top-down when the problem strictly requires combining child values bottom-up.

### Drill Problems
- [LeetCode 337: House Robber III] (Medium)
- [LeetCode 124: Binary Tree Maximum Path Sum] (Hard)
