# 🔤 Advanced String Traversal & Manipulation Playbook

**Professional-grade intuition for mastering string patterns across all difficulty levels**

This playbook covers advanced string techniques from foundational to expert level:
- **Level 1**: Two pointers, sliding window basics, character frequency
- **Level 2**: Advanced sliding window, substring search, anagram patterns
- **Level 3**: String matching algorithms (KMP, Rabin-Karp), palindrome optimization
- **Level 4**: Suffix arrays, trie-based patterns, string DP
- **Level 5**: Advanced DP on strings, lexicographic ordering, string compression

Each pattern follows the **Why / What / How / Where / When / Visual** teaching standard with Python/C# code, common pitfalls, and invariants.

---

## Table of Contents

1. [Pattern Selection Guide](#pattern-selection-guide)
2. [Level 1: Foundation Layer](#level-1-foundation-layer)
   - [1.1 Two Pointers (Opposite Ends)](#11-two-pointers-opposite-ends)
   - [1.2 Two Pointers (Same Direction)](#12-two-pointers-same-direction)
   - [1.3 Character Frequency Map](#13-character-frequency-map)
   - [1.4 Fixed-Size Sliding Window](#14-fixed-size-sliding-window)
3. [Level 2: Pattern Recognition Layer](#level-2-pattern-recognition-layer)
   - [2.1 Variable-Size Sliding Window (At Most K)](#21-variable-size-sliding-window-at-most-k)
   - [2.2 Variable-Size Sliding Window (Exactly K)](#22-variable-size-sliding-window-exactly-k)
   - [2.3 Anagram Pattern Matching](#23-anagram-pattern-matching)
   - [2.4 String Builder Pattern](#24-string-builder-pattern)
4. [Level 3: Optimization Layer](#level-3-optimization-layer)
   - [3.1 Rabin-Karp (Rolling Hash)](#31-rabin-karp-rolling-hash)
   - [3.2 KMP Pattern Matching](#32-kmp-pattern-matching)
   - [3.3 Manacher's Algorithm (Longest Palindrome)](#33-manachers-algorithm-longest-palindrome)
   - [3.4 Z-Algorithm](#34-z-algorithm)
5. [Level 4: Structure Layer](#level-4-structure-layer)
   - [4.1 Trie Construction & Search](#41-trie-construction--search)
   - [4.2 Suffix Array Construction](#42-suffix-array-construction)
   - [4.3 String DP (Edit Distance)](#43-string-dp-edit-distance)
   - [4.4 String DP (LCS & Variants)](#44-string-dp-lcs--variants)
6. [Level 5: Advanced Layer](#level-5-advanced-layer)
   - [5.1 Palindrome Partitioning DP](#51-palindrome-partitioning-dp)
   - [5.2 String Interleaving DP](#52-string-interleaving-dp)
   - [5.3 Lexicographic Ordering](#53-lexicographic-ordering)
   - [5.4 String Compression with DP](#54-string-compression-with-dp)
7. [Invariant Library](#invariant-library)
8. [Quick Reference Table](#quick-reference-table)

---

## Pattern Selection Guide

**Use this decision flow to map problem signals to patterns:**

```
Need to check palindrome or reverse?
├─ YES → 1.1 Two Pointers (Opposite Ends)
└─ NO  → Continue

Need to find substring with constraint (length, sum, condition)?
├─ Fixed size window? → 1.4 Fixed-Size Sliding Window
├─ "At most K" constraint? → 2.1 Variable Sliding Window (At Most K)
├─ "Exactly K" constraint? → 2.2 Variable Sliding Window (Exactly K)
└─ NO → Continue

Need character frequency analysis?
├─ YES → 1.3 Character Frequency Map
└─ NO  → Continue

Need to find anagram or permutation?
├─ YES → 2.3 Anagram Pattern Matching
└─ NO  → Continue

Need efficient string pattern search?
├─ Multiple pattern occurrences? → 3.1 Rabin-Karp (Rolling Hash)
├─ Single pattern with preprocessing? → 3.2 KMP
└─ NO → Continue

Need longest palindrome efficiently?
├─ YES → 3.3 Manacher's Algorithm
└─ NO  → Continue

Need prefix/suffix matching?
├─ Multiple strings? → 4.1 Trie
├─ Single string suffix analysis? → 4.2 Suffix Array
└─ NO → Continue

Need string transformation cost?
├─ Edit distance? → 4.3 String DP (Edit Distance)
├─ Common subsequence? → 4.4 String DP (LCS)
└─ NO → Continue

Need optimal string partitioning?
├─ Palindrome partitions? → 5.1 Palindrome Partitioning DP
├─ Interleaving strings? → 5.2 String Interleaving DP
└─ NO → Continue

Need lexicographic operations?
└─ YES → 5.3 Lexicographic Ordering
```

---

## Level 1: Foundation Layer

**Goal**: Master basic string traversal patterns that form the building blocks for complex algorithms.

---

### 1.1 Two Pointers (Opposite Ends)

**Why**: Many string problems require comparing or processing characters from both ends simultaneously (palindromes, reversals, validity checks).

**What**: Use two indices (`left`, `right`) starting at opposite ends, moving toward each other until they meet. Process or compare characters at each step.

**How (stepflow)**:
1. Initialize `left = 0`, `right = len(s) - 1`
2. While `left < right`:
   - Process/compare `s[left]` and `s[right]`
   - Move pointers: `left++`, `right--`
3. Return result based on conditions met

**Where**: Valid Palindrome, Two Sum II, Container With Most Water, Reverse String.

**When**: Problem involves symmetric properties, checking from both ends, or reversing.

**Visual**:
```
Palindrome check "racecar":
  L                 R
  r a c e c a r
  ↓               ↓
  Match → Move both

    L           R
  r a c e c a r
    ↓         ↓
    Match → Move both

      L     R
  r a c e c a r
      ↓   ↓
      Match → Move both

        L R
  r a c e c a r
        ↓↓
        Meet → TRUE (palindrome)
```

**Python**:
```python
def is_palindrome(s: str) -> bool:
    """Check if string is palindrome (ignoring non-alphanumeric)."""
    left, right = 0, len(s) - 1
    
    while left < right:
        # Skip non-alphanumeric
        while left < right and not s[left].isalnum():
            left += 1
        while left < right and not s[right].isalnum():
            right -= 1
        
        # Compare characters (case-insensitive)
        if s[left].lower() != s[right].lower():
            return False
        
        left += 1
        right -= 1
    
    return True

def reverse_string(s: list) -> None:
    """Reverse string in-place."""
    left, right = 0, len(s) - 1
    while left < right:
        s[left], s[right] = s[right], s[left]
        left += 1
        right -= 1
```

**C#**:
```csharp
public static bool IsPalindrome(string s)
{
    int left = 0, right = s.Length - 1;
    
    while (left < right)
    {
        while (left < right && !char.IsLetterOrDigit(s[left]))
            left++;
        while (left < right && !char.IsLetterOrDigit(s[right]))
            right--;
        
        if (char.ToLower(s[left]) != char.ToLower(s[right]))
            return false;
        
        left++;
        right--;
    }
    
    return true;
}

public static void ReverseString(char[] s)
{
    int left = 0, right = s.Length - 1;
    while (left < right)
    {
        (s[left], s[right]) = (s[right], s[left]);
        left++;
        right--;
    }
}
```

**Common pitfalls**:
- Not handling empty or single-character strings
- Forgetting to normalize (lowercase, skip spaces)
- Off-by-one errors when `left` and `right` meet

**Tips & tricks**:
- **Invariant**: Characters outside `[left, right]` are validated/processed
- Can extend to three pointers for problems like Dutch National Flag
- Works on arrays, not just strings

**LeetCode Practice**:
- 125 - Valid Palindrome
- 344 - Reverse String
- 680 - Valid Palindrome II

---

### 1.2 Two Pointers (Same Direction)

**Why**: Some problems require maintaining a relationship between two positions moving in the same direction (slow/fast, valid/current).

**What**: Both pointers start at the beginning but advance at different rates or under different conditions. Common pattern: `slow` pointer for valid position, `fast` pointer for exploration.

**How (stepflow)**:
1. Initialize `slow = 0`, `fast = 0`
2. While `fast < len(s)`:
   - Check condition at `fast`
   - If valid: copy to `slow`, advance `slow`
   - Always advance `fast`
3. Return result based on `slow` position

**Where**: Remove Duplicates, Move Zeroes, Remove Element, Partition Array.

**When**: Need to filter/partition in-place while maintaining order.

**Visual**:
```
Remove duplicates from sorted array [1,1,2,2,3]:
  S F
  1 1 2 2 3
  
  s[1] == s[0] → skip
  
  S   F
  1 1 2 2 3
  
  s[2] != s[1] → copy to slow+1
  
  S F
  1 2 2 2 3
      ↑
      copied
  
  S   F
  1 2 2 2 3
  
  s[3] == s[2] → skip
  
  S     F
  1 2 2 2 3
  
  s[4] != s[3] → copy to slow+1
  
    S F
  1 2 3 2 3
      ↑
      copied
  
  Result: [1,2,3] (length = slow+1 = 3)
```

**Python**:
```python
def remove_duplicates_sorted(nums: list) -> int:
    """Remove duplicates in-place, return new length."""
    if not nums:
        return 0
    
    slow = 0
    for fast in range(1, len(nums)):
        if nums[fast] != nums[slow]:
            slow += 1
            nums[slow] = nums[fast]
    
    return slow + 1

def remove_element(nums: list, val: int) -> int:
    """Remove all occurrences of val in-place."""
    slow = 0
    for fast in range(len(nums)):
        if nums[fast] != val:
            nums[slow] = nums[fast]
            slow += 1
    
    return slow
```

**C#**:
```csharp
public static int RemoveDuplicatesSorted(int[] nums)
{
    if (nums.Length == 0) return 0;
    
    int slow = 0;
    for (int fast = 1; fast < nums.Length; fast++)
    {
        if (nums[fast] != nums[slow])
        {
            slow++;
            nums[slow] = nums[fast];
        }
    }
    
    return slow + 1;
}

public static int RemoveElement(int[] nums, int val)
{
    int slow = 0;
    for (int fast = 0; fast < nums.Length; fast++)
    {
        if (nums[fast] != val)
        {
            nums[slow] = nums[fast];
            slow++;
        }
    }
    
    return slow;
}
```

**Common pitfalls**:
- Not initializing `slow` correctly (often starts at 0 or 1 depending on problem)
- Modifying array before reading needed values
- Returning wrong length (often `slow` vs `slow + 1`)

**Tips & tricks**:
- **Invariant**: `[0, slow]` contains valid elements; `[slow+1, fast-1]` processed but invalid
- `fast` explores, `slow` maintains valid boundary
- O(1) space, O(n) time complexity

**LeetCode Practice**:
- 26 - Remove Duplicates from Sorted Array
- 27 - Remove Element
- 283 - Move Zeroes

---

### 1.3 Character Frequency Map

**Why**: Many string problems require tracking character counts, frequencies, or character sets efficiently.

**What**: Use a hash map (dictionary) to map characters to their counts or properties. Enables O(1) lookup and update.

**How (stepflow)**:
1. Initialize empty map: `freq = {}`
2. Iterate through string:
   - For each character `c`:
     - `freq[c] = freq.get(c, 0) + 1`
3. Use map for queries (count, existence, comparison)

**Where**: Valid Anagram, First Unique Character, Character Replacement, Ransom Note.

**When**: Need to track character counts, check character sets, or compare frequencies.

**Visual**:
```
Build frequency map for "hello":
  
  Iteration:
  h → {'h': 1}
  e → {'h': 1, 'e': 1}
  l → {'h': 1, 'e': 1, 'l': 1}
  l → {'h': 1, 'e': 1, 'l': 2}
  o → {'h': 1, 'e': 1, 'l': 2, 'o': 1}

Check anagram "olleh":
  Both maps identical → TRUE
```

**Python**:
```python
from collections import Counter, defaultdict

def is_anagram(s: str, t: str) -> bool:
    """Check if t is anagram of s."""
    if len(s) != len(t):
        return False
    
    # Method 1: Using Counter
    return Counter(s) == Counter(t)
    
    # Method 2: Manual counting
    freq = {}
    for c in s:
        freq[c] = freq.get(c, 0) + 1
    for c in t:
        if c not in freq:
            return False
        freq[c] -= 1
        if freq[c] < 0:
            return False
    return all(v == 0 for v in freq.values())

def first_unique_char(s: str) -> int:
    """Find index of first non-repeating character."""
    freq = Counter(s)
    for i, c in enumerate(s):
        if freq[c] == 1:
            return i
    return -1

def can_construct_ransom(ransom: str, magazine: str) -> bool:
    """Check if ransom can be constructed from magazine."""
    freq = Counter(magazine)
    for c in ransom:
        if freq[c] <= 0:
            return False
        freq[c] -= 1
    return True
```

**C#**:
```csharp
public static bool IsAnagram(string s, string t)
{
    if (s.Length != t.Length) return false;
    
    var freq = new Dictionary<char, int>();
    
    foreach (char c in s)
        freq[c] = freq.GetValueOrDefault(c, 0) + 1;
    
    foreach (char c in t)
    {
        if (!freq.ContainsKey(c) || freq[c] == 0)
            return false;
        freq[c]--;
    }
    
    return freq.Values.All(v => v == 0);
}

public static int FirstUniqueChar(string s)
{
    var freq = new Dictionary<char, int>();
    foreach (char c in s)
        freq[c] = freq.GetValueOrDefault(c, 0) + 1;
    
    for (int i = 0; i < s.Length; i++)
        if (freq[s[i]] == 1)
            return i;
    
    return -1;
}
```

**Common pitfalls**:
- Not handling case sensitivity when required
- Forgetting to check if character exists before decrementing
- Using array when character set is large (use dict instead)

**Tips & tricks**:
- **Invariant**: Map always reflects current character counts
- For fixed charset (a-z): use array `freq = [0] * 26` for O(1) space
- `Counter` in Python, `Dictionary` in C# for general case
- Can track deletion by decrementing; check if value becomes 0

**LeetCode Practice**:
- 242 - Valid Anagram
- 387 - First Unique Character in a String
- 383 - Ransom Note

---

### 1.4 Fixed-Size Sliding Window

**Why**: Problems asking for results over all substrings/subarrays of length K require efficient window management.

**What**: Maintain a window of exactly K elements. Slide the window one position at a time: add new element on right, remove old element on left.

**How (stepflow)**:
1. Initialize window for first K elements
2. Compute initial result
3. For `i` from K to len(s):
   - Add `s[i]` to window (right)
   - Remove `s[i-K]` from window (left)
   - Update result
4. Return aggregated results

**Where**: Max Average Subarray, Contains Nearby Duplicate, Sliding Window Maximum.

**When**: Problem specifies exact window size K.

**Visual**:
```
Find max sum of subarray size 3 in [1,3,2,5,1,4]:
  
  [1,3,2] 5,1,4  → sum = 6
   ↑---↑
  
  1,[3,2,5] 1,4  → sum = 10
     ↑---↑
  
  1,3,[2,5,1] 4  → sum = 8
       ↑---↑
  
  1,3,2,[5,1,4]  → sum = 10
         ↑---↑
  
  Max = 10
```

**Python**:
```python
def max_average_subarray(nums: list, k: int) -> float:
    """Find maximum average of subarray of length k."""
    # Initial window
    window_sum = sum(nums[:k])
    max_sum = window_sum
    
    # Slide window
    for i in range(k, len(nums)):
        window_sum += nums[i] - nums[i - k]
        max_sum = max(max_sum, window_sum)
    
    return max_sum / k

def contains_nearby_duplicate(nums: list, k: int) -> bool:
    """Check if duplicate exists within k distance."""
    window = set()
    
    for i in range(len(nums)):
        if i > k:
            window.remove(nums[i - k - 1])
        
        if nums[i] in window:
            return True
        
        window.add(nums[i])
    
    return False
```

**C#**:
```csharp
public static double MaxAverageSubarray(int[] nums, int k)
{
    int windowSum = nums.Take(k).Sum();
    int maxSum = windowSum;
    
    for (int i = k; i < nums.Length; i++)
    {
        windowSum += nums[i] - nums[i - k];
        maxSum = Math.Max(maxSum, windowSum);
    }
    
    return (double)maxSum / k;
}

public static bool ContainsNearbyDuplicate(int[] nums, int k)
{
    var window = new HashSet<int>();
    
    for (int i = 0; i < nums.Length; i++)
    {
        if (i > k)
            window.Remove(nums[i - k - 1]);
        
        if (!window.Add(nums[i]))
            return true;
    }
    
    return false;
}
```

**Common pitfalls**:
- Off-by-one errors in window boundaries
- Not removing leftmost element before adding rightmost
- Incorrect initial window calculation

**Tips & tricks**:
- **Invariant**: Window always contains exactly K elements
- Time: O(n), Space: O(K) for window storage
- For sum queries: maintain running sum, update in O(1)
- For min/max queries: use deque for O(1) updates

**LeetCode Practice**:
- 643 - Maximum Average Subarray I
- 219 - Contains Duplicate II
- 1456 - Maximum Number of Vowels in a Substring

---

## Level 2: Pattern Recognition Layer

**Goal**: Master variable-size windows and specialized matching patterns.

---

### 2.1 Variable-Size Sliding Window (At Most K)

**Why**: Many optimization problems ask for "longest/maximum substring with at most K [constraint]". Fixed windows don't work because size varies.

**What**: Expand window by moving `right`, shrink by moving `left` when constraint violated. Track maximum valid window size.

**How (stepflow)**:
1. Initialize `left = 0`, `right = 0`, `max_len = 0`
2. Expand window: for each `right`:
   - Add `s[right]` to window state
   - While constraint violated:
     - Remove `s[left]` from window state
     - `left++`
   - Update `max_len = max(max_len, right - left + 1)`
3. Return `max_len`

**Where**: Longest Substring with At Most K Distinct Characters, Max Consecutive Ones III, Longest Repeating Character Replacement.

**When**: Problem asks for "longest/maximum" with "at most K" constraint.

**Visual**:
```
Longest substring with at most 2 distinct chars in "eceba":
  
  L R
  e c e b a
  {e:1} → valid (1 distinct)
  
  L   R
  e c e b a
  {e:1, c:1} → valid (2 distinct)
  
  L     R
  e c e b a
  {e:2, c:1} → valid (2 distinct)
  
  L       R
  e c e b a
  {e:2, c:1, b:1} → INVALID (3 distinct)
  Shrink: remove 'e'
  
    L     R
  e c e b a
  {e:1, c:1, b:1} → still 3 distinct
  Shrink: remove 'c'
  
      L   R
  e c e b a
  {e:1, b:1} → valid (2 distinct)
  
  Max length = 3 ("ece")
```

**Python**:
```python
from collections import defaultdict

def length_of_longest_substring_k_distinct(s: str, k: int) -> int:
    """Longest substring with at most k distinct characters."""
    if k == 0:
        return 0
    
    freq = defaultdict(int)
    left = 0
    max_len = 0
    
    for right in range(len(s)):
        freq[s[right]] += 1
        
        # Shrink while constraint violated
        while len(freq) > k:
            freq[s[left]] -= 1
            if freq[s[left]] == 0:
                del freq[s[left]]
            left += 1
        
        max_len = max(max_len, right - left + 1)
    
    return max_len

def character_replacement(s: str, k: int) -> int:
    """Longest repeating character after replacing at most k chars."""
    freq = defaultdict(int)
    left = 0
    max_freq = 0
    max_len = 0
    
    for right in range(len(s)):
        freq[s[right]] += 1
        max_freq = max(max_freq, freq[s[right]])
        
        # If replacements needed > k, shrink
        while (right - left + 1) - max_freq > k:
            freq[s[left]] -= 1
            left += 1
        
        max_len = max(max_len, right - left + 1)
    
    return max_len
```

**C#**:
```csharp
public static int LengthOfLongestSubstringKDistinct(string s, int k)
{
    if (k == 0) return 0;
    
    var freq = new Dictionary<char, int>();
    int left = 0, maxLen = 0;
    
    for (int right = 0; right < s.Length; right++)
    {
        freq[s[right]] = freq.GetValueOrDefault(s[right], 0) + 1;
        
        while (freq.Count > k)
        {
            freq[s[left]]--;
            if (freq[s[left]] == 0)
                freq.Remove(s[left]);
            left++;
        }
        
        maxLen = Math.Max(maxLen, right - left + 1);
    }
    
    return maxLen;
}
```

**Common pitfalls**:
- Not shrinking window correctly (should be `while`, not `if`)
- Forgetting to remove key from map when count reaches 0
- Not tracking maximum frequency for optimization problems

**Tips & tricks**:
- **Invariant**: `[left, right]` always satisfies constraint
- Window size = `right - left + 1`
- Template works for many "at most K" problems
- For "exactly K", use "at most K" - "at most K-1"

**LeetCode Practice**:
- 340 - Longest Substring with At Most K Distinct Characters
- 424 - Longest Repeating Character Replacement
- 1004 - Max Consecutive Ones III

---

### 2.2 Variable-Size Sliding Window (Exactly K)

**Why**: Some problems require "exactly K" constraint, not "at most K". Direct approach differs from "at most K" pattern.

**What**: Use the insight: **Exactly K = (At Most K) - (At Most K-1)**. Alternatively, track window state to maintain exactly K distinct elements.

**How (stepflow)**:

**Method 1: Difference of two "at most" calls**
1. Compute `at_most_k = longest_substring_at_most_k(s, k)`
2. Compute `at_most_k_minus_1 = longest_substring_at_most_k(s, k-1)`
3. Return `at_most_k - at_most_k_minus_1`

**Method 2: Direct tracking**
1. Expand window until exactly K elements
2. Count all valid windows while maintaining exactly K
3. Shrink when needed

**Where**: Subarrays with K Different Integers, Count Number of Nice Subarrays.

**When**: Problem asks for "exactly K" constraint.

**Visual**:
```
Subarrays with exactly 2 distinct integers in [1,2,1,2,3]:
  
  At most 2:
    [1,2,1,2] → includes windows with 1 or 2 distinct
  
  At most 1:
    [1], [2] → windows with only 1 distinct
  
  Exactly 2 = (at most 2) - (at most 1)
  
  Valid windows: [1,2], [1,2,1], [1,2,1,2], [2,1], [2,1,2], [1,2], [2,3]
```

**Python**:
```python
def subarrays_with_k_distinct(nums: list, k: int) -> int:
    """Count subarrays with exactly k distinct integers."""
    
    def at_most_k(k):
        freq = {}
        left = 0
        count = 0
        
        for right in range(len(nums)):
            freq[nums[right]] = freq.get(nums[right], 0) + 1
            
            while len(freq) > k:
                freq[nums[left]] -= 1
                if freq[nums[left]] == 0:
                    del freq[nums[left]]
                left += 1
            
            count += right - left + 1
        
        return count
    
    return at_most_k(k) - at_most_k(k - 1)

def number_of_subarrays_exactly_k_odds(nums: list, k: int) -> int:
    """Count subarrays with exactly k odd numbers."""
    
    def at_most_k(k):
        left = 0
        count = 0
        odd_count = 0
        
        for right in range(len(nums)):
            if nums[right] % 2 == 1:
                odd_count += 1
            
            while odd_count > k:
                if nums[left] % 2 == 1:
                    odd_count -= 1
                left += 1
            
            count += right - left + 1
        
        return count
    
    return at_most_k(k) - at_most_k(k - 1)
```

**C#**:
```csharp
public static int SubarraysWithKDistinct(int[] nums, int k)
{
    int AtMostK(int limit)
    {
        var freq = new Dictionary<int, int>();
        int left = 0, count = 0;
        
        for (int right = 0; right < nums.Length; right++)
        {
            freq[nums[right]] = freq.GetValueOrDefault(nums[right], 0) + 1;
            
            while (freq.Count > limit)
            {
                freq[nums[left]]--;
                if (freq[nums[left]] == 0)
                    freq.Remove(nums[left]);
                left++;
            }
            
            count += right - left + 1;
        }
        
        return count;
    }
    
    return AtMostK(k) - AtMostK(k - 1);
}
```

**Common pitfalls**:
- Trying to directly maintain "exactly K" without "at most" insight
- Off-by-one in the subtraction formula
- Not handling edge case when k = 0 or k > distinct count

**Tips & tricks**:
- **Invariant**: Exactly K = At Most K - At Most (K-1)
- The "at most K" function returns count of ALL subarrays, not just longest
- Time: O(n) despite two passes (both are linear)

**LeetCode Practice**:
- 992 - Subarrays with K Different Integers
- 1248 - Count Number of Nice Subarrays

---

### 2.3 Anagram Pattern Matching

**Why**: Finding all anagram occurrences of a pattern in a string requires efficient sliding window with frequency matching.

**What**: Use fixed-size window (length of pattern) with character frequency comparison. Slide window and check if current window is an anagram of pattern.

**How (stepflow)**:
1. Build frequency map for pattern
2. Build frequency map for first window in string
3. Compare maps; if match, record index
4. Slide window: add new char, remove old char, compare again
5. Return all matching indices

**Where**: Find All Anagrams, Permutation in String, Minimum Window Substring.

**When**: Need to find permutations/anagrams of one string in another.

**Visual**:
```
Find anagrams of "ab" in "cbaebabacd":
  
  Pattern freq: {a:1, b:1}
  
  Window "cb": {c:1, b:1} → NO MATCH
  Window "ba": {b:1, a:1} → MATCH ✓ (index 1)
  Window "ae": {a:1, e:1} → NO MATCH
  Window "eb": {e:1, b:1} → NO MATCH
  Window "ba": {b:1, a:1} → MATCH ✓ (index 4)
  Window "ab": {a:1, b:1} → MATCH ✓ (index 5)
  
  Result: [1, 4, 5]
```

**Python**:
```python
from collections import Counter

def find_anagrams(s: str, p: str) -> list:
    """Find all start indices of p's anagrams in s."""
    if len(p) > len(s):
        return []
    
    p_freq = Counter(p)
    window_freq = Counter(s[:len(p)])
    result = []
    
    if window_freq == p_freq:
        result.append(0)
    
    for i in range(len(p), len(s)):
        # Add new character
        window_freq[s[i]] += 1
        
        # Remove old character
        window_freq[s[i - len(p)]] -= 1
        if window_freq[s[i - len(p)]] == 0:
            del window_freq[s[i - len(p)]]
        
        if window_freq == p_freq:
            result.append(i - len(p) + 1)
    
    return result

def check_inclusion(s1: str, s2: str) -> bool:
    """Check if s2 contains permutation of s1."""
    if len(s1) > len(s2):
        return False
    
    s1_freq = Counter(s1)
    window_freq = Counter(s2[:len(s1)])
    
    if window_freq == s1_freq:
        return True
    
    for i in range(len(s1), len(s2)):
        window_freq[s2[i]] += 1
        window_freq[s2[i - len(s1)]] -= 1
        if window_freq[s2[i - len(s1)]] == 0:
            del window_freq[s2[i - len(s1)]]
        
        if window_freq == s1_freq:
            return True
    
    return False
```

**C#**:
```csharp
public static List<int> FindAnagrams(string s, string p)
{
    var result = new List<int>();
    if (p.Length > s.Length) return result;
    
    var pFreq = new Dictionary<char, int>();
    var windowFreq = new Dictionary<char, int>();
    
    foreach (char c in p)
        pFreq[c] = pFreq.GetValueOrDefault(c, 0) + 1;
    
    for (int i = 0; i < p.Length; i++)
        windowFreq[s[i]] = windowFreq.GetValueOrDefault(s[i], 0) + 1;
    
    if (DictsEqual(windowFreq, pFreq))
        result.Add(0);
    
    for (int i = p.Length; i < s.Length; i++)
    {
        windowFreq[s[i]] = windowFreq.GetValueOrDefault(s[i], 0) + 1;
        
        windowFreq[s[i - p.Length]]--;
        if (windowFreq[s[i - p.Length]] == 0)
            windowFreq.Remove(s[i - p.Length]);
        
        if (DictsEqual(windowFreq, pFreq))
            result.Add(i - p.Length + 1);
    }
    
    return result;
}

private static bool DictsEqual(Dictionary<char, int> d1, Dictionary<char, int> d2)
{
    if (d1.Count != d2.Count) return false;
    foreach (var kvp in d1)
        if (!d2.ContainsKey(kvp.Key) || d2[kvp.Key] != kvp.Value)
            return false;
    return true;
}
```

**Common pitfalls**:
- Comparing maps on every slide is O(k) where k = charset size → optimize with match counter
- Not removing characters with count 0 from map
- Off-by-one in result index calculation

**Tips & tricks**:
- **Invariant**: Window size equals pattern length
- Optimization: track number of matched characters instead of comparing full maps
- Can extend to "minimum window substring" with variable window

**LeetCode Practice**:
- 438 - Find All Anagrams in a String
- 567 - Permutation in String
- 76 - Minimum Window Substring

---

### 2.4 String Builder Pattern

**Why**: String concatenation in loops creates O(n²) complexity due to immutability. StringBuilder provides O(n) solution.

**What**: Use mutable character array or StringBuilder to accumulate characters. Join once at the end.

**How (stepflow)**:
1. Initialize StringBuilder/list
2. Append characters/strings in loop
3. Convert to string once at end

**Where**: Reverse Words, String Compression, Generate Parentheses, any string construction problem.

**When**: Building strings character-by-character or concatenating in loops.

**Visual**:
```
BAD (O(n²)):
  result = ""
  for char in s:
      result += char  # Creates new string object each time
  
GOOD (O(n)):
  result = []
  for char in s:
      result.append(char)  # O(1) append
  return ''.join(result)  # O(n) final join
```

**Python**:
```python
def reverse_words(s: str) -> str:
    """Reverse words in a string."""
    words = s.split()
    words.reverse()
    return ' '.join(words)

def compress_string(s: str) -> str:
    """Compress string using character counts (e.g., 'aabccc' -> 'a2b1c3')."""
    if not s:
        return ""
    
    result = []
    i = 0
    
    while i < len(s):
        char = s[i]
        count = 1
        
        while i + count < len(s) and s[i + count] == char:
            count += 1
        
        result.append(char)
        result.append(str(count))
        i += count
    
    compressed = ''.join(result)
    return compressed if len(compressed) < len(s) else s

def remove_duplicates_string(s: str) -> str:
    """Remove adjacent duplicates."""
    stack = []
    for c in s:
        if stack and stack[-1] == c:
            stack.pop()
        else:
            stack.append(c)
    return ''.join(stack)
```

**C#**:
```csharp
public static string ReverseWords(string s)
{
    var words = s.Split(' ');
    Array.Reverse(words);
    return string.Join(" ", words);
}

public static string CompressString(string s)
{
    if (string.IsNullOrEmpty(s)) return "";
    
    var sb = new StringBuilder();
    int i = 0;
    
    while (i < s.Length)
    {
        char c = s[i];
        int count = 1;
        
        while (i + count < s.Length && s[i + count] == c)
            count++;
        
        sb.Append(c);
        sb.Append(count);
        i += count;
    }
    
    string compressed = sb.ToString();
    return compressed.Length < s.Length ? compressed : s;
}

public static string RemoveDuplicatesString(string s)
{
    var stack = new Stack<char>();
    foreach (char c in s)
    {
        if (stack.Count > 0 && stack.Peek() == c)
            stack.Pop();
        else
            stack.Push(c);
    }
    return new string(stack.Reverse().ToArray());
}
```

**Common pitfalls**:
- Using `+=` for strings in loops (O(n²) complexity)
- Not handling empty strings or edge cases
- Forgetting to convert StringBuilder/list to string at end

**Tips & tricks**:
- **Invariant**: StringBuilder maintains O(1) amortized append
- Python: use `list` + `''.join()` or `io.StringIO`
- C#: use `StringBuilder` for repeated concatenation
- Java: use `StringBuilder` (not `StringBuffer` unless thread-safe needed)

**LeetCode Practice**:
- 151 - Reverse Words in a String
- 443 - String Compression
- 1047 - Remove All Adjacent Duplicates In String

---

## Level 3: Optimization Layer

**Goal**: Master advanced string matching algorithms with optimal time complexity.

---

### 3.1 Rabin-Karp (Rolling Hash)

**Why**: Finding pattern occurrences requires O(n*m) with naive approach. Rabin-Karp achieves O(n+m) expected time using rolling hash.

**What**: Compute hash of pattern. Slide through text computing hash of each window. When hashes match, verify with character comparison to handle collisions.

**How (stepflow)**:
1. Choose base (e.g., 256 for ASCII) and prime modulus
2. Compute `hash(pattern)`
3. Compute hash of first window in text
4. Slide window:
   - Remove leftmost character contribution
   - Add rightmost character contribution
   - If `hash(window) == hash(pattern)`, verify characters
5. Record all matching indices

**Where**: Repeated DNA Sequences, Implement strStr(), Find Duplicate Substring.

**When**: Need to find pattern(s) in text with multiple occurrences or multiple patterns.

**Visual**:
```
Find "abc" in "xabcabcy":
  
  Pattern hash: hash("abc") = 97*256² + 98*256 + 99 (mod M)
  
  Window "xab": hash(xab) ≠ hash(abc)
  
  Slide to "abc":
    Remove 'x', add 'c'
    hash(abc) = hash(abc) ✓ → verify chars → MATCH
  
  Slide to "bca":
    Remove 'a', add 'a'
    hash(bca) ≠ hash(abc)
  
  Continue...
```

**Python**:
```python
def rabin_karp(text: str, pattern: str) -> list:
    """Find all occurrences of pattern in text using Rabin-Karp."""
    if not pattern or len(pattern) > len(text):
        return []
    
    BASE = 256
    MOD = 10**9 + 7
    m, n = len(pattern), len(text)
    
    # Compute hash of pattern and first window
    pattern_hash = 0
    window_hash = 0
    h = 1  # BASE^(m-1) mod MOD
    
    for i in range(m - 1):
        h = (h * BASE) % MOD
    
    for i in range(m):
        pattern_hash = (pattern_hash * BASE + ord(pattern[i])) % MOD
        window_hash = (window_hash * BASE + ord(text[i])) % MOD
    
    result = []
    
    # Slide window
    for i in range(n - m + 1):
        if pattern_hash == window_hash:
            # Verify characters to handle collision
            if text[i:i+m] == pattern:
                result.append(i)
        
        if i < n - m:
            # Remove leftmost, add rightmost
            window_hash = (window_hash - ord(text[i]) * h) % MOD
            window_hash = (window_hash * BASE + ord(text[i + m])) % MOD
            window_hash = (window_hash + MOD) % MOD  # Handle negative
    
    return result

def longest_duplicate_substring(s: str) -> str:
    """Find longest duplicate substring using binary search + Rabin-Karp."""
    n = len(s)
    
    def search(length):
        """Check if duplicate substring of given length exists."""
        BASE = 256
        MOD = 10**9 + 7
        h = pow(BASE, length - 1, MOD)
        
        seen = {}
        curr_hash = 0
        
        for i in range(length):
            curr_hash = (curr_hash * BASE + ord(s[i])) % MOD
        seen[curr_hash] = 0
        
        for i in range(1, n - length + 1):
            curr_hash = (curr_hash - ord(s[i-1]) * h) % MOD
            curr_hash = (curr_hash * BASE + ord(s[i + length - 1])) % MOD
            curr_hash = (curr_hash + MOD) % MOD
            
            if curr_hash in seen:
                return s[i:i+length]
            seen[curr_hash] = i
        
        return ""
    
    # Binary search on length
    left, right = 1, n
    result = ""
    
    while left <= right:
        mid = (left + right) // 2
        found = search(mid)
        if found:
            result = found
            left = mid + 1
        else:
            right = mid - 1
    
    return result
```

**C#**:
```csharp
public static List<int> RabinKarp(string text, string pattern)
{
    var result = new List<int>();
    if (string.IsNullOrEmpty(pattern) || pattern.Length > text.Length)
        return result;
    
    const long BASE = 256;
    const long MOD = 1_000_000_007;
    int m = pattern.Length, n = text.Length;
    
    long patternHash = 0, windowHash = 0, h = 1;
    
    for (int i = 0; i < m - 1; i++)
        h = (h * BASE) % MOD;
    
    for (int i = 0; i < m; i++)
    {
        patternHash = (patternHash * BASE + pattern[i]) % MOD;
        windowHash = (windowHash * BASE + text[i]) % MOD;
    }
    
    for (int i = 0; i <= n - m; i++)
    {
        if (patternHash == windowHash)
        {
            if (text.Substring(i, m) == pattern)
                result.Add(i);
        }
        
        if (i < n - m)
        {
            windowHash = (windowHash - text[i] * h % MOD + MOD) % MOD;
            windowHash = (windowHash * BASE + text[i + m]) % MOD;
        }
    }
    
    return result;
}
```

**Common pitfalls**:
- Not handling negative hash values after subtraction
- Forgetting to verify characters on hash match (collisions)
- Incorrect computation of h = BASE^(m-1)
- Integer overflow without proper modulo

**Tips & tricks**:
- **Invariant**: Rolling hash computes hash in O(1) per slide
- Expected time: O(n+m), worst case O(n*m) with many collisions
- Choose large prime MOD to minimize collisions
- Can extend to multiple pattern search

**LeetCode Practice**:
- 28 - Implement strStr()
- 187 - Repeated DNA Sequences
- 1044 - Longest Duplicate Substring

---

### 3.2 KMP Pattern Matching

**Why**: Naive pattern matching backtracks unnecessarily. KMP preprocesses pattern to skip redundant comparisons, achieving O(n+m) guaranteed time.

**What**: Build LPS (Longest Proper Prefix which is also Suffix) array for pattern. Use LPS to avoid re-comparing matched characters when mismatch occurs.

**How (stepflow)**:
1. Build LPS array for pattern:
   - `lps[i]` = length of longest proper prefix of `pattern[0..i]` that is also suffix
2. Traverse text with two pointers:
   - `i` for text, `j` for pattern
   - On match: advance both
   - On mismatch: reset `j` to `lps[j-1]` (skip redundant comparisons)
   - Record match when `j == len(pattern)`

**Where**: Implement strStr(), Shortest Palindrome, Repeated Substring Pattern.

**When**: Need guaranteed O(n+m) pattern matching without hash collisions.

**Visual**:
```
Pattern: "ABABC"
LPS:     [0,0,1,2,0]

Explanation:
  A B A B C
  0 0 1 2 0
  
  lps[2]=1: "ABA" has prefix "A" = suffix "A"
  lps[3]=2: "ABAB" has prefix "AB" = suffix "AB"
  lps[4]=0: "ABABC" has no proper prefix = suffix

Text: "ABABDABABC"
Pattern: "ABABC"

  A B A B D A B A B C
  A B A B C
  ↑         ↑
  match until D≠C
  
  Use lps[3]=2, skip to:
  A B A B D A B A B C
          A B A B C
          ↑     ↑
  
  Continue matching...
```

**Python**:
```python
def build_lps(pattern: str) -> list:
    """Build Longest Proper Prefix which is also Suffix array."""
    m = len(pattern)
    lps = [0] * m
    length = 0
    i = 1
    
    while i < m:
        if pattern[i] == pattern[length]:
            length += 1
            lps[i] = length
            i += 1
        else:
            if length != 0:
                length = lps[length - 1]
            else:
                lps[i] = 0
                i += 1
    
    return lps

def kmp_search(text: str, pattern: str) -> list:
    """Find all occurrences of pattern in text using KMP."""
    if not pattern:
        return []
    
    n, m = len(text), len(pattern)
    lps = build_lps(pattern)
    result = []
    
    i = 0  # index for text
    j = 0  # index for pattern
    
    while i < n:
        if text[i] == pattern[j]:
            i += 1
            j += 1
        
        if j == m:
            result.append(i - j)
            j = lps[j - 1]
        elif i < n and text[i] != pattern[j]:
            if j != 0:
                j = lps[j - 1]
            else:
                i += 1
    
    return result

def str_str(haystack: str, needle: str) -> int:
    """Find first occurrence of needle in haystack."""
    if not needle:
        return 0
    
    matches = kmp_search(haystack, needle)
    return matches[0] if matches else -1
```

**C#**:
```csharp
private static int[] BuildLPS(string pattern)
{
    int m = pattern.Length;
    var lps = new int[m];
    int length = 0, i = 1;
    
    while (i < m)
    {
        if (pattern[i] == pattern[length])
        {
            length++;
            lps[i] = length;
            i++;
        }
        else
        {
            if (length != 0)
                length = lps[length - 1];
            else
            {
                lps[i] = 0;
                i++;
            }
        }
    }
    
    return lps;
}

public static List<int> KmpSearch(string text, string pattern)
{
    var result = new List<int>();
    if (string.IsNullOrEmpty(pattern)) return result;
    
    int n = text.Length, m = pattern.Length;
    var lps = BuildLPS(pattern);
    
    int i = 0, j = 0;
    
    while (i < n)
    {
        if (text[i] == pattern[j])
        {
            i++;
            j++;
        }
        
        if (j == m)
        {
            result.Add(i - j);
            j = lps[j - 1];
        }
        else if (i < n && text[i] != pattern[j])
        {
            if (j != 0)
                j = lps[j - 1];
            else
                i++;
        }
    }
    
    return result;
}
```

**Common pitfalls**:
- Incorrect LPS array construction (off-by-one errors)
- Not resetting `j` correctly on mismatch
- Forgetting to handle empty pattern case

**Tips & tricks**:
- **Invariant**: LPS[i] = length of longest border of pattern[0..i]
- Time: O(n+m) guaranteed, Space: O(m) for LPS
- No hash collisions unlike Rabin-Karp
- LPS construction itself uses KMP logic

**LeetCode Practice**:
- 28 - Implement strStr()
- 459 - Repeated Substring Pattern
- 214 - Shortest Palindrome

---

### 3.3 Manacher's Algorithm (Longest Palindrome)

**Why**: Finding longest palindrome substring takes O(n²) with expand-around-center. Manacher's achieves O(n).

**What**: Transform string to handle even/odd length palindromes uniformly. Use previously computed palindrome information to skip redundant expansions.

**How (stepflow)**:
1. Transform string: "abc" → "^#a#b#c#$" (add delimiters and boundaries)
2. Initialize `P[i]` = palindrome radius at position i
3. Track center `C` and right boundary `R` of rightmost palindrome
4. For each position `i`:
   - Mirror `i` around `C`: `mirror = 2*C - i`
   - If `i < R`: `P[i] = min(P[mirror], R - i)`
   - Expand around `i` to find exact radius
   - Update `C` and `R` if palindrome extends beyond `R`
5. Find position with maximum `P[i]`, extract substring

**Where**: Longest Palindromic Substring, Count Palindromic Substrings.

**When**: Need O(n) solution for palindrome problems.

**Visual**:
```
Original: "babad"
Transform: "^#b#a#b#a#d#$"
           0123456789...

P array represents radius:
  Position 5 (character 'a'):
  #b#a#b#
      ↑
    radius=3 means palindrome "bab"

Key insight:
  If we know palindrome centered at C reaches R,
  then position i < R can mirror from 2*C-i
  
  This avoids re-expanding from scratch.
```

**Python**:
```python
def longest_palindrome_manacher(s: str) -> str:
    """Find longest palindromic substring using Manacher's algorithm."""
    if not s:
        return ""
    
    # Transform string
    T = '^#' + '#'.join(s) + '#$'
    n = len(T)
    P = [0] * n
    C = R = 0
    
    for i in range(1, n - 1):
        # Mirror of i around C
        mirror = 2 * C - i
        
        # If i is within R, use previously computed values
        if i < R:
            P[i] = min(R - i, P[mirror])
        
        # Expand around i
        while T[i + P[i] + 1] == T[i - P[i] - 1]:
            P[i] += 1
        
        # Update C and R if palindrome centered at i extends beyond R
        if i + P[i] > R:
            C, R = i, i + P[i]
    
    # Find maximum radius
    max_len = max(P)
    center_index = P.index(max_len)
    
    # Extract original palindrome
    start = (center_index - max_len) // 2
    return s[start:start + max_len]

def count_substrings_palindrome(s: str) -> int:
    """Count all palindromic substrings using Manacher's."""
    if not s:
        return 0
    
    T = '^#' + '#'.join(s) + '#$'
    n = len(T)
    P = [0] * n
    C = R = 0
    
    for i in range(1, n - 1):
        mirror = 2 * C - i
        if i < R:
            P[i] = min(R - i, P[mirror])
        
        while T[i + P[i] + 1] == T[i - P[i] - 1]:
            P[i] += 1
        
        if i + P[i] > R:
            C, R = i, i + P[i]
    
    # Each P[i] represents number of palindromes centered at i
    return sum((p + 1) // 2 for p in P)
```

**C#**:
```csharp
public static string LongestPalindromeManacher(string s)
{
    if (string.IsNullOrEmpty(s)) return "";
    
    string T = "^#" + string.Join("#", s.ToCharArray()) + "#$";
    int n = T.Length;
    var P = new int[n];
    int C = 0, R = 0;
    
    for (int i = 1; i < n - 1; i++)
    {
        int mirror = 2 * C - i;
        
        if (i < R)
            P[i] = Math.Min(R - i, P[mirror]);
        
        while (T[i + P[i] + 1] == T[i - P[i] - 1])
            P[i]++;
        
        if (i + P[i] > R)
        {
            C = i;
            R = i + P[i];
        }
    }
    
    int maxLen = 0, centerIndex = 0;
    for (int i = 1; i < n - 1; i++)
    {
        if (P[i] > maxLen)
        {
            maxLen = P[i];
            centerIndex = i;
        }
    }
    
    int start = (centerIndex - maxLen) / 2;
    return s.Substring(start, maxLen);
}
```

**Common pitfalls**:
- Not handling string transformation correctly
- Off-by-one errors in extracting original palindrome
- Forgetting boundary characters (^ and $) to avoid bounds checking

**Tips & tricks**:
- **Invariant**: P[i] = radius of palindrome centered at i in transformed string
- Time: O(n), Space: O(n) for transformed string
- The transformation handles even/odd length uniformly
- Can be adapted to count palindromes, not just find longest

**LeetCode Practice**:
- 5 - Longest Palindromic Substring
- 647 - Palindromic Substrings

---

### 3.4 Z-Algorithm

**Why**: Many string problems require finding all occurrences of pattern or computing longest common prefix efficiently. Z-algorithm achieves O(n+m) for pattern matching and O(n) for prefix computations.

**What**: For string S, Z[i] = length of longest substring starting at i that matches prefix of S. Use Z-array for pattern matching by concatenating pattern + separator + text.

**How (stepflow)**:
1. Compute Z-array:
   - Track interval [L, R] of rightmost substring matching prefix
   - For position i:
     - If `i <= R`: use Z[i-L] with adjustment
     - Expand explicitly if needed
2. For pattern matching: compute Z-array for "pattern$text"
3. Find indices where Z[i] == len(pattern)

**Where**: Pattern matching, Longest Common Prefix, String period detection.

**When**: Need linear-time prefix matching or pattern search.

**Visual**:
```
String: "aabcaabxaaz"
Z-array: [0, 1, 0, 0, 3, 1, 0, 0, 2, 1, 0]

Explanation:
  Position 4: "aab" matches prefix of length 3
  Position 8: "aa" matches prefix of length 2

Pattern matching "aab" in "aabcaabxaaz":
  S = "aab$aabcaabxaaz"
  Z = [0, 1, 0, 0, 0, 3, 1, 0, 0, 3, 1, 0, 0, 2, 1, 0]
                    ↑           ↑
                 match at 5, match at 9
```

**Python**:
```python
def build_z_array(s: str) -> list:
    """Build Z-array: Z[i] = length of longest prefix match at i."""
    n = len(s)
    Z = [0] * n
    L, R = 0, 0
    
    for i in range(1, n):
        if i > R:
            # Compute from scratch
            L, R = i, i
            while R < n and s[R - L] == s[R]:
                R += 1
            Z[i] = R - L
            R -= 1
        else:
            # Inside interval [L, R]
            k = i - L
            if Z[k] < R - i + 1:
                Z[i] = Z[k]
            else:
                L = i
                while R < n and s[R - L] == s[R]:
                    R += 1
                Z[i] = R - L
                R -= 1
    
    return Z

def z_algorithm_search(text: str, pattern: str) -> list:
    """Find all occurrences of pattern in text using Z-algorithm."""
    if not pattern or len(pattern) > len(text):
        return []
    
    s = pattern + '$' + text
    Z = build_z_array(s)
    m = len(pattern)
    
    result = []
    for i in range(m + 1, len(s)):
        if Z[i] == m:
            result.append(i - m - 1)
    
    return result

def longest_common_prefix_length(strings: list) -> int:
    """Find length of longest common prefix using Z-algorithm concept."""
    if not strings:
        return 0
    
    # Concatenate with separators
    s = '$'.join(strings)
    Z = build_z_array(s)
    
    # Find minimum Z value at separator boundaries
    min_prefix = float('inf')
    pos = 0
    for string in strings[1:]:
        pos += len(strings[0]) + 1
        min_prefix = min(min_prefix, Z[pos])
    
    return min_prefix
```

**C#**:
```csharp
private static int[] BuildZArray(string s)
{
    int n = s.Length;
    var Z = new int[n];
    int L = 0, R = 0;
    
    for (int i = 1; i < n; i++)
    {
        if (i > R)
        {
            L = R = i;
            while (R < n && s[R - L] == s[R])
                R++;
            Z[i] = R - L;
            R--;
        }
        else
        {
            int k = i - L;
            if (Z[k] < R - i + 1)
            {
                Z[i] = Z[k];
            }
            else
            {
                L = i;
                while (R < n && s[R - L] == s[R])
                    R++;
                Z[i] = R - L;
                R--;
            }
        }
    }
    
    return Z;
}

public static List<int> ZAlgorithmSearch(string text, string pattern)
{
    var result = new List<int>();
    if (string.IsNullOrEmpty(pattern) || pattern.Length > text.Length)
        return result;
    
    string s = pattern + "$" + text;
    var Z = BuildZArray(s);
    int m = pattern.Length;
    
    for (int i = m + 1; i < s.Length; i++)
    {
        if (Z[i] == m)
            result.Add(i - m - 1);
    }
    
    return result;
}
```

**Common pitfalls**:
- Not handling [L, R] interval updates correctly
- Off-by-one errors in extracting match positions
- Forgetting separator character when concatenating pattern and text

**Tips & tricks**:
- **Invariant**: [L, R] is rightmost substring matching prefix
- Time: O(n), Space: O(n) for Z-array
- Alternative to KMP with simpler implementation
- Useful for multiple pattern matching with small modifications

**LeetCode Practice**:
- 28 - Implement strStr() (alternative to KMP)
- 1392 - Longest Happy Prefix

---

## Level 4: Structure Layer

**Goal**: Master data structures for string organization and dynamic programming on strings.

---

### 4.1 Trie Construction & Search

**Why**: Prefix-based operations (autocomplete, word search, prefix matching) benefit from trie structure with O(m) search where m = word length.

**What**: Tree where each node represents a character. Root to leaf paths form words. Each node stores children map and end-of-word flag.

**How (stepflow)**:

**Insertion**:
1. Start at root
2. For each character in word:
   - If child exists, move to child
   - Else create new child node
3. Mark last node as end-of-word

**Search**:
1. Start at root
2. For each character:
   - If child doesn't exist, return False
   - Move to child
3. Check if current node is end-of-word

**Where**: Implement Trie, Word Search II, Design Add and Search Words Data Structure.

**When**: Need prefix operations, word dictionary, autocomplete.

**Visual**:
```
Trie for ["cat", "cats", "dog", "dogs", "deer"]:

        root
       / | \
      c  d  
      |  |  
      a  o  e
      |  |  |
      t  g* e
      |     |
      s*    r*

* = end of word

Search "cat": c → a → t (found, marked as end)
Search "ca": c → a (found, but not end of word)
Search prefix "do": d → o (found)
```

**Python**:
```python
class TrieNode:
    def __init__(self):
        self.children = {}
        self.is_end_of_word = False

class Trie:
    def __init__(self):
        self.root = TrieNode()
    
    def insert(self, word: str) -> None:
        """Insert word into trie."""
        node = self.root
        for char in word:
            if char not in node.children:
                node.children[char] = TrieNode()
            node = node.children[char]
        node.is_end_of_word = True
    
    def search(self, word: str) -> bool:
        """Check if word exists in trie."""
        node = self.root
        for char in word:
            if char not in node.children:
                return False
            node = node.children[char]
        return node.is_end_of_word
    
    def starts_with(self, prefix: str) -> bool:
        """Check if any word starts with prefix."""
        node = self.root
        for char in prefix:
            if char not in node.children:
                return False
            node = node.children[char]
        return True
    
    def find_words_with_prefix(self, prefix: str) -> list:
        """Find all words with given prefix."""
        node = self.root
        for char in prefix:
            if char not in node.children:
                return []
            node = node.children[char]
        
        # DFS to collect all words from this node
        result = []
        
        def dfs(node, path):
            if node.is_end_of_word:
                result.append(prefix + path)
            for char, child in node.children.items():
                dfs(child, path + char)
        
        dfs(node, "")
        return result

# Wildcard search (with '.' as any character)
class WordDictionary:
    def __init__(self):
        self.root = TrieNode()
    
    def add_word(self, word: str) -> None:
        node = self.root
        for char in word:
            if char not in node.children:
                node.children[char] = TrieNode()
            node = node.children[char]
        node.is_end_of_word = True
    
    def search(self, word: str) -> bool:
        """Search with '.' wildcard matching any character."""
        def dfs(node, i):
            if i == len(word):
                return node.is_end_of_word
            
            if word[i] == '.':
                # Try all children
                return any(dfs(child, i + 1) for child in node.children.values())
            else:
                if word[i] not in node.children:
                    return False
                return dfs(node.children[word[i]], i + 1)
        
        return dfs(self.root, 0)
```

**C#**:
```csharp
public class TrieNode
{
    public Dictionary<char, TrieNode> Children = new();
    public bool IsEndOfWord = false;
}

public class Trie
{
    private TrieNode root = new();
    
    public void Insert(string word)
    {
        var node = root;
        foreach (char c in word)
        {
            if (!node.Children.ContainsKey(c))
                node.Children[c] = new TrieNode();
            node = node.Children[c];
        }
        node.IsEndOfWord = true;
    }
    
    public bool Search(string word)
    {
        var node = root;
        foreach (char c in word)
        {
            if (!node.Children.ContainsKey(c))
                return false;
            node = node.Children[c];
        }
        return node.IsEndOfWord;
    }
    
    public bool StartsWith(string prefix)
    {
        var node = root;
        foreach (char c in prefix)
        {
            if (!node.Children.ContainsKey(c))
                return false;
            node = node.Children[c];
        }
        return true;
    }
}

public class WordDictionary
{
    private TrieNode root = new();
    
    public void AddWord(string word)
    {
        var node = root;
        foreach (char c in word)
        {
            if (!node.Children.ContainsKey(c))
                node.Children[c] = new TrieNode();
            node = node.Children[c];
        }
        node.IsEndOfWord = true;
    }
    
    public bool Search(string word)
    {
        return Dfs(root, word, 0);
    }
    
    private bool Dfs(TrieNode node, string word, int i)
    {
        if (i == word.Length)
            return node.IsEndOfWord;
        
        if (word[i] == '.')
        {
            return node.Children.Values.Any(child => Dfs(child, word, i + 1));
        }
        else
        {
            if (!node.Children.ContainsKey(word[i]))
                return false;
            return Dfs(node.Children[word[i]], word, i + 1);
        }
    }
}
```

**Common pitfalls**:
- Not marking end-of-word flag correctly
- Confusing search (exact match) with starts_with (prefix match)
- Not handling empty strings or null inputs
- Memory overhead for sparse tries (consider compressed/Patricia trie)

**Tips & tricks**:
- **Invariant**: Path from root to node represents prefix
- Time: O(m) for insert/search where m = word length
- Space: O(ALPHABET_SIZE * N * M) worst case
- Can store additional data at nodes (frequency, suggestions)
- For wildcard search, use DFS/backtracking

**LeetCode Practice**:
- 208 - Implement Trie (Prefix Tree)
- 211 - Design Add and Search Words Data Structure
- 212 - Word Search II

---

## Invariant Library

Memorize these one-liners:

| Pattern | Invariant |
|---------|-----------|
| **Two pointers (opposite)** | Characters outside `[left, right]` are validated/processed. |
| **Two pointers (same dir)** | `[0, slow]` contains valid elements; `[slow+1, fast-1]` processed but invalid. |
| **Character frequency** | Map always reflects current character counts in window/string. |
| **Fixed sliding window** | Window always contains exactly K elements. |
| **Variable window (at most K)** | `[left, right]` always satisfies constraint; window size = `right - left + 1`. |
| **Exactly K** | Exactly K = (At Most K) - (At Most K-1). |
| **Anagram matching** | Window size equals pattern length; frequency match = anagram found. |
| **StringBuilder** | StringBuilder maintains O(1) amortized append; use for string construction. |
| **Rabin-Karp** | Rolling hash computes hash in O(1) per slide; verify on hash match. |
| **KMP** | LPS[i] = length of longest border of pattern[0..i]; no backtracking in text. |
| **Manacher** | P[i] = radius of palindrome centered at i in transformed string. |
| **Z-algorithm** | [L, R] is rightmost substring matching prefix; Z[i] = prefix match length. |
| **Trie** | Path from root to node represents prefix; end flag marks complete word. |
| **DP Edit Distance** | dp[i][j] = min cost to transform s[0..i-1] to t[0..j-1]. |
| **DP LCS** | dp[i][j] = length of LCS of s[0..i-1] and t[0..j-1]. |

---

## Quick Reference Table

| Problem Signal | Pattern | Key Data Structure | Complexity |
|----------------|---------|-------------------|------------|
| Check palindrome, reverse | Two pointers (opposite) | Two indices | O(n) |
| Remove duplicates in-place | Two pointers (same dir) | Slow/fast pointers | O(n) |
| Character counts, anagram | Frequency map | Hash map / array | O(n) |
| Subarray size K | Fixed sliding window | Window state | O(n) |
| Longest with at most K | Variable window (at most) | Window + constraint | O(n) |
| Exactly K constraint | Exactly K = at most K - at most K-1 | Two window passes | O(n) |
| Find anagram occurrences | Anagram matching | Frequency map + window | O(n*m) or O(n) |
| String building in loop | StringBuilder pattern | List/StringBuilder | O(n) |
| Multiple pattern occurrences | Rabin-Karp | Rolling hash | O(n+m) exp |
| Pattern matching (guaranteed) | KMP | LPS array | O(n+m) |
| Longest palindrome (optimal) | Manacher | P array + transform | O(n) |
| Prefix matching | Z-algorithm | Z array | O(n) |
| Prefix operations, autocomplete | Trie | Tree of characters | O(m) per op |
| String transformation cost | DP Edit Distance | 2D DP table | O(n*m) |
| Longest common subsequence | DP LCS | 2D DP table | O(n*m) |
| Palindrome partitioning | Palindrome DP | 2D DP + palindrome check | O(n²) or O(n³) |
| String interleaving | Interleaving DP | 2D DP table | O(n*m) |

---

## Pattern Combinations

**Real problems often combine multiple patterns:**

| Problem | Patterns Used |
|---------|---------------|
| **Minimum Window Substring** | Variable sliding window + Frequency map + Anagram matching |
| **Longest Substring Without Repeating** | Variable sliding window + Set/Map for tracking |
| **Group Anagrams** | Frequency map + Hashing/Sorting |
| **Palindrome Pairs** | Trie + Reverse string + Palindrome check |
| **Edit Distance with Operations** | DP Edit Distance + Backtracking for path |
| **Word Break** | DP + Trie for dictionary lookup |

---

## Final Mental Model

```
Level 1: Foundation
  ├─ Two pointers (opposite ends)
  ├─ Two pointers (same direction)
  ├─ Character frequency map
  └─ Fixed-size sliding window

Level 2: Pattern Recognition
  ├─ Variable sliding window (at most K)
  ├─ Variable sliding window (exactly K)
  ├─ Anagram pattern matching
  └─ StringBuilder pattern

Level 3: Optimization
  ├─ Rabin-Karp (rolling hash)
  ├─ KMP pattern matching
  ├─ Manacher's algorithm
  └─ Z-algorithm

Level 4: Structure
  ├─ Trie construction & search
  ├─ Suffix array
  ├─ String DP (edit distance)
  └─ String DP (LCS variants)

Level 5: Advanced
  ├─ Palindrome partitioning DP
  ├─ String interleaving DP
  ├─ Lexicographic ordering
  └─ String compression DP
```

**Master these patterns and you can solve 95% of string problems efficiently.**

---

**Next steps:**
1. Implement each pattern from scratch in both Python and C#
2. Solve 2–3 practice problems per pattern
3. Recognize pattern signals in new problems
4. Combine patterns for complex scenarios
5. Focus on invariants - they guide implementation

**Remember**: String manipulation is about **efficient traversal with clear invariants**. Master the invariants, optimize space/time complexity, and the solutions become systematic.
