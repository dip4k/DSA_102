# Hashing Traversal Mastery

## Summary Table

| Level | Mental Model | Pointer/State | Drill Problems |
|---|---|---|---|
| 1. Frequency Counting | Map element to its frequency/occurrence count. | `curr_element` -> `count` | [LeetCode 242: Valid Anagram] (Easy), [LeetCode 387: First Unique Character in a String] (Easy) |
| 2. Complement Mapping | Store seen elements to look up required complements for pairs. | `curr_element` -> `index` (or just existence) | [LeetCode 1: Two Sum] (Easy), [LeetCode 128: Longest Consecutive Sequence] (Medium) |
| 3. Prefix-Sum + HashMap | Map running prefix sum to its frequency or earliest index to find subarrays. | `running_sum` -> `count` / `index` | [LeetCode 560: Subarray Sum Equals K] (Medium), [LeetCode 525: Contiguous Array] (Medium) |
| 4. Sliding Window + HashMap | Maintain frequency of elements in current window to satisfy constraints. | `window_elements` -> `count` | [LeetCode 76: Minimum Window Substring] (Hard), [LeetCode 438: Find All Anagrams in a String] (Medium) |

---

## Level 1: Frequency Counting

**Mental Model & Invariants**
* **What does the state/index mean?**: The hash map keys represent distinct elements in the collection, and values represent their occurrence counts.
* **What region is processed?**: The entire collection (iterated one by one or in a single pass).
* **What is the invariant?**: `hash_map[x]` always equals the exact number of times `x` has been encountered so far.

**Visual State Trace**
Target: Count frequencies in `[a, b, a, c]`

| Step | Element | Hash Map State | Invariant Maintained |
|---|---|---|---|
| 1 | `a` | `{'a': 1}` | 'a' seen once |
| 2 | `b` | `{'a': 1, 'b': 1}` | 'b' seen once |
| 3 | `a` | `{'a': 2, 'b': 1}` | 'a' seen twice |
| 4 | `c` | `{'a': 2, 'b': 1, 'c': 1}` | 'c' seen once |

**Code Snippets**

Problem: Check if two strings are valid anagrams of each other by comparing character frequencies.
```python
# Python
def is_anagram(s: str, t: str) -> bool:
    # 1. Quick length check
    if len(s) != len(t):
        return False
    counts = {}
    # 2. Map elements of first string to their frequencies
    for char in s:
        counts[char] = counts.get(char, 0) + 1
    # 3. Check elements of second string against the map
    for char in t:
        if counts.get(char, 0) == 0:
            return False # Character missing or overused
        # 4. Decrement count to mark as matched
        counts[char] -= 1
    return True
```

Problem: Check if two strings are valid anagrams of each other by comparing character frequencies.
```csharp
// C#
public bool IsAnagram(string s, string t) {
    // 1. Quick length check
    if (s.Length != t.Length) return false;
    var counts = new Dictionary<char, int>();
    // 2. Map elements of first string to their frequencies
    foreach (char c in s) {
        counts[c] = counts.GetValueOrDefault(c, 0) + 1;
    }
    // 3. Check elements of second string against the map
    foreach (char c in t) {
        if (!counts.ContainsKey(c) || counts[c] == 0) return false; // Character missing or overused
        // 4. Decrement count to mark as matched
        counts[c]--;
    }
    return true;
}
```

### ⚠️ Gotchas & Pitfalls
* **KeyError/NullReference**: Accessing keys that haven't been added yet. Always use safe access like `.get(k, 0)` or `.GetValueOrDefault(k, 0)`.
* **Iteration Modification**: Modifying the hash map (adding/removing keys) while iterating over its keys.
* **Array Map vs Hash Map**: If keys are bounded integers (e.g., lowercase letters `a-z`), an array of size 26 is much faster and uses less overhead than a general hash map.

---

## Level 2: Two-Sum / Complement Mapping

**Mental Model & Invariants**
* **What does the state/index mean?**: The hash map stores `seen_element -> its_index`. The current element computes its "complement" (what it needs to form a pair).
* **What region is processed?**: The prefix of the array ending at the current element.
* **What is the invariant?**: At index `i`, the hash map contains all elements from `0` to `i-1`. If `target - nums[i]` exists in the map, a valid pair is found.

**Visual State Trace**
Target: Find pair summing to 9 in `[2, 7, 11, 15]`

| Step | Element | Complement (9 - x) | Action/Check | Hash Map State (`val:idx`) |
|---|---|---|---|---|
| 1 | `2` | `7` | `7` not in map. Add `2`. | `{2: 0}` |
| 2 | `7` | `2` | `2` in map. Pair found! | `{2: 0}` |

**Code Snippets**

Problem: Find the indices of two numbers in an array that sum to a specific target.
```python
# Python
def two_sum(nums: list[int], target: int) -> list[int]:
    seen = {}
    # 1. Iterate through elements processing prefix
    for i, num in enumerate(nums):
        # 2. Compute the required complement
        complement = target - num
        # 3. Check if complement exists in our map
        if complement in seen:
            return [seen[complement], i] # Valid pair found
        # 4. Add current element to map for future lookups
        seen[num] = i
    return []
```

Problem: Find the indices of two numbers in an array that sum to a specific target.
```csharp
// C#
public int[] TwoSum(int[] nums, int target) {
    var seen = new Dictionary<int, int>();
    // 1. Iterate through elements processing prefix
    for (int i = 0; i < nums.Length; i++) {
        // 2. Compute the required complement
        int complement = target - nums[i];
        // 3. Check if complement exists in our map
        if (seen.ContainsKey(complement)) {
            return new int[] { seen[complement], i }; // Valid pair found
        }
        // 4. Add current element to map for future lookups
        seen[nums[i]] = i;
    }
    return new int[0];
}
```

### ⚠️ Gotchas & Pitfalls
* **Using the same element twice**: Adding the current element to the map *before* checking for its complement can lead to using the same element twice (e.g., target 4, current element 2).
* **Overwriting duplicates**: If duplicate values exist in the array, `seen[num] = i` will overwrite the index with the latest one. This is usually fine for Two Sum but might break if you need all pairs.

---

## Level 3: Prefix-Sum + HashMap

**Mental Model & Invariants**
* **What does the state/index mean?**: `running_sum` is the cumulative sum from index 0 to `i`. The hash map stores `prefix_sum -> frequency` or `prefix_sum -> index`.
* **What region is processed?**: Subarrays. A subarray sum from index `j` to `i` is `prefix[i] - prefix[j-1]`.
* **What is the invariant?**: To find a subarray ending at `i` with sum `K`, we check if a prefix sum of `running_sum - K` has been seen before `i`.

**Visual State Trace**
Target: Find subarrays summing to K=3 in `[1, 2, 1, 2, 1]`

| Step | Element | Running Sum | Target Prefix (`Sum - K`) | Matches Found | Hash Map State (`sum:freq`) |
|---|---|---|---|---|---|
| 0 | - | `0` | - | - | `{0: 1}` (Base case) |
| 1 | `1` | `1` | `-2` | 0 | `{0: 1, 1: 1}` |
| 2 | `2` | `3` | `0` | 1 | `{0: 1, 1: 1, 3: 1}` |
| 3 | `1` | `4` | `1` | 1 | `{0: 1, 1: 1, 3: 1, 4: 1}` |
| 4 | `2` | `6` | `3` | 1 | `{0: 1, 1: 1, 3: 1, 4: 1, 6: 1}` |

**Code Snippets**

Problem: Count the total number of contiguous subarrays that sum to K.
```python
# Python
def subarray_sum(nums: list[int], k: int) -> int:
    # 1. Base case: array starting at index 0
    prefix_counts = {0: 1} 
    running_sum = 0
    total_subarrays = 0
    
    for num in nums:
        # 2. Compute cumulative prefix sum
        running_sum += num
        # 3. Calculate target prefix sum needed to form sum K
        target = running_sum - k
        # 4. Check if the target prefix was seen before
        if target in prefix_counts:
            total_subarrays += prefix_counts[target]
        # 5. Add current running sum to the map for future subarrays
        prefix_counts[running_sum] = prefix_counts.get(running_sum, 0) + 1
        
    return total_subarrays
```

Problem: Count the total number of contiguous subarrays that sum to K.
```csharp
// C#
public int SubarraySum(int[] nums, int k) {
    // 1. Base case: array starting at index 0
    var prefixCounts = new Dictionary<int, int> { { 0, 1 } };
    int runningSum = 0;
    int totalSubarrays = 0;
    
    foreach (int num in nums) {
        // 2. Compute cumulative prefix sum
        runningSum += num;
        // 3. Calculate target prefix sum needed to form sum K
        int target = runningSum - k;
        // 4. Check if the target prefix was seen before
        if (prefixCounts.ContainsKey(target)) {
            totalSubarrays += prefixCounts[target];
        }
        // 5. Add current running sum to the map for future subarrays
        prefixCounts[runningSum] = prefixCounts.GetValueOrDefault(runningSum, 0) + 1;
    }
    
    return totalSubarrays;
}
```

### ⚠️ Gotchas & Pitfalls
* **Missing Base Case**: Forgetting to initialize the hash map with `{0: 1}` (or `0 -> -1` for index storage). This causes the code to miss valid subarrays that start from index 0.
* **Negative Numbers**: Standard sliding window fails with negative numbers, which is exactly why Prefix-Sum + HashMap is required. Ensure you don't accidentally try to shrink a window when sums drop.
* **Order of Operations**: You must check for the `target` prefix *before* adding the current `running_sum` to the map to handle `K=0` correctly without matching the current prefix with itself.

---

## Level 4: Sliding Window + HashMap

**Mental Model & Invariants**
* **What does the state/index mean?**: Two pointers (`left`, `right`) define a window. The hash map tracks the frequencies of elements currently inside the window.
* **What region is processed?**: A contiguous subarray (window) defined by `[left, right]`.
* **What is the invariant?**: The hash map strictly represents the exact counts of elements in the `[left, right]` range. As `right` expands, add to map. As `left` shrinks, remove from map.

**Visual State Trace**
Target: Find anagram of "ab" in "cbaebabacd" (Window size 2)

| Step | Window `[left, right]` | Window Hash Map | Target Map | Action |
|---|---|---|---|---|
| 1 | `[c]` | `{'c': 1}` | `{'a':1, 'b':1}` | Expand `right` |
| 2 | `[c, b]` | `{'c': 1, 'b': 1}` | `{'a':1, 'b':1}` | Window full, check. Shrink `left`. |
| 3 | `[b, a]` | `{'b': 1, 'a': 1}` | `{'a':1, 'b':1}` | Match found! Shrink `left`. |

**Code Snippets**

Problem: Find all starting indices of substring anagrams of `p` in `s`.
```python
# Python
def find_anagrams(s: str, p: str) -> list[int]:
    if len(p) > len(s): return []
    
    p_count, window_count = {}, {}
    # 1. Build the target map representing needed frequencies
    for char in p:
        p_count[char] = p_count.get(char, 0) + 1
        
    res = []
    left = 0
    
    for right in range(len(s)):
        # 2. Expand window by adding current element to map
        char = s[right]
        window_count[char] = window_count.get(char, 0) + 1
        
        # 3. Shrink window if it exceeds target length
        if right - left + 1 > len(p):
            left_char = s[left]
            window_count[left_char] -= 1
            # 4. Important: Remove stale keys completely
            if window_count[left_char] == 0:
                del window_count[left_char]
            left += 1
            
        # 5. Check invariant: Does window map match target map?
        if window_count == p_count:
            res.append(left)
            
    return res
```

Problem: Find all starting indices of substring anagrams of `p` in `s`.
```csharp
// C#
public IList<int> FindAnagrams(string s, string p) {
    var res = new List<int>();
    if (p.Length > s.Length) return res;
    
    // 1. Build the target map representing needed frequencies
    var pCount = new Dictionary<char, int>();
    foreach (char c in p) pCount[c] = pCount.GetValueOrDefault(c, 0) + 1;
    
    var windowCount = new Dictionary<char, int>();
    int left = 0;
    
    for (int right = 0; right < s.Length; right++) {
        // 2. Expand window by adding current element to map
        char c = s[right];
        windowCount[c] = windowCount.GetValueOrDefault(c, 0) + 1;
        
        // 3. Shrink window if it exceeds target length
        if (right - left + 1 > p.Length) {
            char leftChar = s[left];
            windowCount[leftChar]--;
            // 4. Important: Remove stale keys completely
            if (windowCount[leftChar] == 0) {
                windowCount.Remove(leftChar);
            }
            left++;
        }
        
        // 5. Check invariant: Does window map match target map?
        // Dictionary comparison in C# requires looping or LINQ, 
        // array map of size 26 is preferred for strings.
        if (IsMatch(pCount, windowCount)) {
            res.Add(left);
        }
    }
    return res;
}

private bool IsMatch(Dictionary<char, int> d1, Dictionary<char, int> d2) {
    if (d1.Count != d2.Count) return false;
    foreach (var kvp in d1) {
        if (!d2.ContainsKey(kvp.Key) || d2[kvp.Key] != kvp.Value) return false;
    }
    return true;
}
```

### ⚠️ Gotchas & Pitfalls
* **Stale Keys**: When a frequency drops to zero after shrinking the window, forgetting to delete the key from the map. This causes map comparisons (`dict1 == dict2`) to fail because `{a:1, b:0} != {a:1}`.
* **Map Comparison Overhead**: Comparing two full hash maps at every step is `O(K)`. If the character set is small (e.g., ASCII), use an array of size 26 or 256. Alternatively, track a `matches` variable to count how many distinct characters meet the required frequency.
* **Window Shrink Condition**: Miscalculating when to shrink the window (e.g., `right - left + 1 >= len(p)` vs `> len(p)`).
