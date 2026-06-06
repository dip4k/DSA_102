# Week 10 Extended Python Complete v13

Purpose: build Python fluency for core DP patterns: memoization, tabulation, 1D/2D DP, and sequence problems.

## Focus tags
- Must: memoization, tabulation, 1D DP, 2D DP, edit distance, LIS/LCS intuition
- Should: knapsack family, weighted interval scheduling
- Optional: story-driven DP and reconstruction

## Pattern 1: memoized recursion
```python
def fib(n, memo=None):
    if memo is None:
        memo = {}
    if n in memo:
        return memo[n]
    if n < 2:
        return n
    memo[n] = fib(n - 1, memo) + fib(n - 2, memo)
    return memo[n]
```

## Pattern 2: bottom-up 1D DP
```python
def climb_stairs(n):
    if n <= 1:
        return 1
    a, b = 1, 1
    for _ in range(2, n + 1):
        a, b = b, a + b
    return b
```

## Pattern 3: house robber
```python
def rob(nums):
    prev2 = 0
    prev1 = 0
    for value in nums:
        prev2, prev1 = prev1, max(prev1, prev2 + value)
    return prev1
```

## Pattern 4: edit distance
```python
def edit_distance(a, b):
    m, n = len(a), len(b)
    dp = [[0] * (n + 1) for _ in range(m + 1)]
    for i in range(m + 1):
        dp[i][0] = i
    for j in range(n + 1):
        dp[0][j] = j
    for i in range(1, m + 1):
        for j in range(1, n + 1):
            if a[i - 1] == b[j - 1]:
                dp[i][j] = dp[i - 1][j - 1]
            else:
                dp[i][j] = 1 + min(dp[i - 1][j], dp[i][j - 1], dp[i - 1][j - 1])
    return dp[m][n]
```

## Pattern 5: LIS O(n^2)
```python
def lis(nums):
    dp = [1] * len(nums)
    for i in range(len(nums)):
        for j in range(i):
            if nums[j] < nums[i]:
                dp[i] = max(dp[i], dp[j] + 1)
    return max(dp, default=0)
```

## Practice ladder
- Must: climb stairs, house robber, coin change, unique paths, edit distance, LCS/LIS
- Should: weighted interval scheduling, max subarray, matrix chain multiplication intuition
- Optional: story-driven DP formulations and path reconstruction
