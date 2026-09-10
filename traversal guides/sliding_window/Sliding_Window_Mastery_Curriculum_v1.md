# Sliding Window Mastery Curriculum

## Summary Table

| Level | Mental Model | Pointer / State | Drill Problems |
| --- | --- | --- | --- |
| 1. Fixed Window | Moving a rigid frame of size `k` across an array. Subtract outgoing, add incoming. | `left = i - k`, `right = i` | [LeetCode 643: Maximum Average Subarray I] (Easy) |
| 2. Dynamic Window (Expand & Shrink) | Rubber band stretching to include valid elements, shrinking when invalid. | `left`, `right`, running sum/frequency | [LeetCode 209: Minimum Size Subarray Sum] (Medium), [LeetCode 3: Longest Substring Without Repeating Characters] (Medium) |
| 3. String Anagram Windows | Window frequency map matching a target frequency map exactly. | `left`, `right`, `char_count`, `matches` | [LeetCode 438: Find All Anagrams in a String] (Medium), [LeetCode 76: Minimum Window Substring] (Hard) |

## Level 1: Fixed Window

### Mental Model & Invariants
*   **What does the state/index mean?**: `i` is the right boundary. The window spans `[i - k + 1, i]`.
*   **What region is processed?**: Exactly `k` contiguous elements.
*   **What is the invariant/recurrence relation?**: `WindowSum = WindowSum + arr[i] - arr[i - k]` for `i >= k`.

### Visual State Transition
Example: `arr = [1, 12, -5, -6, 50, 3]`, `k = 4`

| Step | Action (Add `arr[i]`) | Action (Drop `arr[i-k]`) | DP State (Window Sum) | Invariant Maintained? |
| --- | --- | --- | --- | --- |
| Init `i=0..3` | Add `1, 12, -5, -6` | N/A | 2 | Yes (initial window) |
| `i=4` | Add `50` | Drop `1` | `2 + 50 - 1 = 51` | Yes (Size exactly 4) |
| `i=5` | Add `3` | Drop `12` | `51 + 3 - 12 = 42` | Yes (Size exactly 4) |

### Code Snippets

**Problem:** Given an integer array `nums` and an integer `k`, find a contiguous subarray of length `k` that has the maximum average value. Return the maximum average.

```python
def findMaxAverage(nums: list[int], k: int) -> float:
    # 1. Initialize window: compute the sum of the first k elements
    window_sum = sum(nums[:k])
    # 2. Track the best result seen so far
    max_sum = window_sum
    
    # 3. Slide the fixed frame: add incoming element, subtract outgoing element
    for i in range(k, len(nums)):
        # 4. Recurrence: new window sum = old sum + arr[right] - arr[left_outgoing]
        window_sum += nums[i] - nums[i - k]
        # 5. Update best: keep the maximum window sum encountered
        max_sum = max(max_sum, window_sum)
        
    # 6. Return result: divide only once at the end to avoid floating-point drift
    return max_sum / k
```

```csharp
public double FindMaxAverage(int[] nums, int k) {
    // 1. Initialize window: compute the sum of the first k elements
    int windowSum = 0;
    for (int i = 0; i < k; i++) windowSum += nums[i];
    
    // 2. Track the best result seen so far
    int maxSum = windowSum;
    // 3. Slide the fixed frame from index k onward
    for (int i = k; i < nums.Length; i++) {
        // 4. Recurrence: add incoming element, subtract outgoing element
        windowSum += nums[i] - nums[i - k];
        // 5. Update best: keep the maximum window sum encountered
        maxSum = Math.Max(maxSum, windowSum);
    }
    // 6. Return result: single division at the end preserves precision
    return (double)maxSum / k;
}
```

### ⚠️ Gotchas & Pitfalls
*   **Off-by-one errors in Initialization**: Calculating the sum for the first `k` elements stops at index `k-1`. Ensure the main loop starts at `i = k`.
*   **Handling Arrays Smaller Than `k`**: Always check if `len(nums) < k` and handle accordingly if the problem allows it.
*   **Double Floating Precision**: Dividing by `k` continuously inside the loop can introduce floating-point inaccuracies. Only divide the maximum sum by `k` at the end.

---

## Level 2: Dynamic Window (Expand & Shrink)

### Mental Model & Invariants
*   **What does the state/index mean?**: `right` expands the window, `left` shrinks it to restore validity.
*   **What region is processed?**: Subarrays satisfying a dynamic condition (e.g., `sum >= target`).
*   **What is the invariant/recurrence relation?**: The window `[left, right]` holds elements. While the window condition is violated (or met, depending on max/min search), increment `left` and update the state.

### Visual State Transition
Example: `nums = [2, 3, 1, 2, 4, 3]`, `target = 7`

| Step | Action (`right++`) | Condition `sum >= 7`? | Action (`left++`) | State (`min_len`) |
| --- | --- | --- | --- | --- |
| `r=0..3` | Add `2, 3, 1, 2` (sum=8) | Yes | Drop `2` (`l=1`, sum=6) | 4 |
| `r=4` | Add `4` (sum=10) | Yes | Drop `3` (`l=2`, sum=7)<br>Drop `1` (`l=3`, sum=6) | 3 |
| `r=5` | Add `3` (sum=9) | Yes | Drop `2` (`l=4`, sum=7)<br>Drop `4` (`l=5`, sum=3) | 2 |

### Code Snippets

**Problem:** Given an array of positive integers `nums` and a positive integer `target`, find the minimal length of a contiguous subarray whose sum is greater than or equal to `target`. Return `0` if no such subarray exists.

```python
def minSubArrayLen(target: int, nums: list[int]) -> int:
    # 1. Initialize pointers and state: left boundary, running sum, best length
    left = 0
    window_sum = 0
    min_len = float('inf')
    
    # 2. Expand window: move right boundary across the array
    for right in range(len(nums)):
        # 3. Expand window: include current element in running sum
        window_sum += nums[right]
        
        # 4. Shrink window: while the condition is met, try to minimize length
        while window_sum >= target:
            # 5. Record candidate: current window length is a valid answer
            min_len = min(min_len, right - left + 1)
            # 6. Contract from left: subtract outgoing element and advance left
            window_sum -= nums[left]
            left += 1
            
    # 7. Return result: inf means no valid subarray was found
    return min_len if min_len != float('inf') else 0
```

```csharp
public int MinSubArrayLen(int target, int[] nums) {
    // 1. Initialize pointers and state: left boundary, running sum, best length
    int left = 0, windowSum = 0;
    int minLen = int.MaxValue;
    
    // 2. Expand window: move right boundary across the array
    for (int right = 0; right < nums.Length; right++) {
        // 3. Expand window: include current element in running sum
        windowSum += nums[right];
        
        // 4. Shrink window: while the condition is met, try to minimize length
        while (windowSum >= target) {
            // 5. Record candidate: current window length is a valid answer
            minLen = Math.Min(minLen, right - left + 1);
            // 6. Contract from left: subtract outgoing element and advance left
            windowSum -= nums[left];
            left++;
        }
    }
    // 7. Return result: MaxValue means no valid subarray was found
    return minLen == int.MaxValue ? 0 : minLen;
}
```

### ⚠️ Gotchas & Pitfalls
*   **Incorrect updating order**: Update the minimum/maximum length inside the `while` loop for "shortest" subarrays, but outside the `while` loop for "longest" subarrays.
*   **Stuck Left Pointer**: Forgetting to increment `left` inside the `while` loop leads to infinite loops.
*   **Negative Numbers**: Standard dynamic sliding window fails if arrays contain negative numbers when looking for a target sum (requires Prefix Sum + HashMap instead).

---

## Level 3: String Anagram Windows

### Mental Model & Invariants
*   **What does the state/index mean?**: `right` adds characters to window state, `left` removes them. Window length usually strictly bounded by target string length.
*   **What region is processed?**: Substrings matching exact character frequencies.
*   **What is the invariant/recurrence relation?**: A sliding frequency map `window_counts` is maintained. When `right - left + 1 == target_len`, check for equality with `target_counts`, then shrink `left`.

### Visual State Transition
Example: `s = "cbaebabacd"`, `p = "abc"`

| Step | Action (`right` char) | Action (`left` char) | Frequency State Matches `p`? | Result Recorded |
| --- | --- | --- | --- | --- |
| `r=0..2` | Add `c, b, a` | N/A | Yes | Index 0 |
| `r=3` | Add `e` | Drop `c` | No (`b,a,e`) | None |
| `r=4` | Add `b` | Drop `b` | No (`a,e,b`) | None |
| `r=5` | Add `a` | Drop `a` | No (`e,b,a`) | None |
| `r=6` | Add `b` | Drop `e` | No (`b,a,b`) | None |
| `r=8` | `...` Add `c` | Drop `b` | Yes (`b,a,c`) | Index 6 |

### Code Snippets

**Problem:** Given two strings `s` and `p`, find all start indices of `p`'s anagrams in `s`. An anagram is a rearrangement using all original letters exactly once.

```python
def findAnagrams(s: str, p: str) -> list[int]:
    # 1. Edge case: pattern longer than string means no anagrams possible
    if len(p) > len(s): return []
    
    # 2. Build target frequency map: count each character in pattern p
    p_count = [0] * 26
    s_count = [0] * 26
    for char in p:
        p_count[ord(char) - ord('a')] += 1
        
    res = []
    left = 0
    # 3. Expand window: slide right boundary across string s
    for right in range(len(s)):
        # 4. Add incoming character to window frequency map
        s_count[ord(s[right]) - ord('a')] += 1
        
        # 5. Enforce fixed window size: if window exceeds len(p), drop leftmost char
        if right - left + 1 > len(p):
            s_count[ord(s[left]) - ord('a')] -= 1
            left += 1
            
        # 6. Check invariant: if frequency maps match, record start index
        if s_count == p_count:
            res.append(left)
            
    return res
```

```csharp
public IList<int> FindAnagrams(string s, string p) {
    var res = new List<int>();
    // 1. Edge case: pattern longer than string means no anagrams possible
    if (p.Length > s.Length) return res;
    
    // 2. Build target frequency map: count each character in pattern p
    int[] pCount = new int[26];
    int[] sCount = new int[26];
    foreach (char c in p) pCount[c - 'a']++;
    
    int left = 0;
    // 3. Expand window: slide right boundary across string s
    for (int right = 0; right < s.Length; right++) {
        // 4. Add incoming character to window frequency map
        sCount[s[right] - 'a']++;
        
        // 5. Enforce fixed window size: if window exceeds p.Length, drop leftmost char
        if (right - left + 1 > p.Length) {
            sCount[s[left] - 'a']--;
            left++;
        }
        
        // 6. Check invariant: if frequency maps match, record start index
        if (sCount.SequenceEqual(pCount)) {
            res.Add(left);
        }
    }
    return res;
}
```

### ⚠️ Gotchas & Pitfalls
*   **Dictionary vs Array Comparison**: Comparing dictionaries is `O(K)` where `K` is unique keys. Using a fixed size integer array of size 26 or 128 makes `O(1)` comparisons.
*   **Window Size Enforcement**: Ensure `left` advances immediately when the window size exceeds `len(p)` *before* checking for anagram equality.
*   **Zero Count Keys**: When using a hashmap instead of fixed arrays, counts dropping to zero must be explicitly deleted from the map, otherwise dictionary equality checks fail.
