# 🔤 String Traversal & Manipulation Visual Playbook

**Professional-grade intuition for Level 1–5 string patterns**

This playbook covers string traversal and manipulation techniques organized into progressive levels:
- **Level 1**: Basic traversal (indexing, loops, boundaries)
- **Level 2**: Two pointers (opposite direction, same direction)
- **Level 3**: Sliding window (fixed & variable)
- **Level 4**: Pattern matching (KMP, Rabin-Karp, Z-algorithm)
- **Level 5**: Advanced structures (Trie, Suffix Array, Manacher's)

Each pattern follows the **Why / What / How / Where / When / Visual** teaching standard with Python/C# code, common pitfalls, and invariants.

---

## Table of Contents

1. [Pattern Selection Guide](#pattern-selection-guide)
2. [Level 1: The Physical Layer](#level-1-the-physical-layer)
   - [1.1 Forward/Backward/Stride Iteration](#11-forwardbackwardstride-iteration)
   - [1.2 Boundary Control](#12-boundary-control)
   - [1.3 Character Frequency & Hashing](#13-character-frequency--hashing)
3. [Level 2: Two Pointers](#level-2-two-pointers)
   - [2.1 Opposite Direction (Convergent)](#21-opposite-direction-convergent)
   - [2.2 Same Direction (Fast-Slow)](#22-same-direction-fast-slow)
4. [Level 3: Sliding Window](#level-3-sliding-window)
   - [3.1 Fixed-Size Window](#31-fixed-size-window)
   - [3.2 Variable-Size Window](#32-variable-size-window)
5. [Level 4: Pattern Matching](#level-4-pattern-matching)
   - [4.1 Naive Pattern Matching](#41-naive-pattern-matching)
   - [4.2 KMP Algorithm](#42-kmp-algorithm)
   - [4.3 Rabin-Karp (Rolling Hash)](#43-rabin-karp-rolling-hash)
   - [4.4 Z-Algorithm](#44-z-algorithm)
6. [Level 5: Advanced Structures](#level-5-advanced-structures)
   - [5.1 Trie (Prefix Tree)](#51-trie-prefix-tree)
   - [5.2 Suffix Array](#52-suffix-array)
   - [5.3 Manacher's Algorithm (Palindromes)](#53-manachers-algorithm-palindromes)
7. [Invariant Library](#invariant-library)
8. [Quick Reference Table](#quick-reference-table)

---

## Pattern Selection Guide

**Use this decision flow to map problem signals to patterns:**

```
Problem involves single-pass character iteration?
├─ YES → 1.1 Forward/Backward/Stride
└─ NO  → Continue

Need to check palindrome or find pairs in sorted string?
├─ YES → 2.1 Two Pointers (Opposite Direction)
└─ NO  → Continue

Need to remove elements or partition string in-place?
├─ YES → 2.2 Two Pointers (Same Direction)
└─ NO  → Continue

Need substring with exact length K?
├─ YES → 3.1 Fixed-Size Sliding Window
└─ NO  → Continue

Need longest/shortest substring with constraint?
├─ YES → 3.2 Variable-Size Sliding Window
└─ NO  → Continue

Need to find pattern in text?
├─ Naive acceptable → 4.1 Naive Pattern Matching
├─ Pattern has repetition → 4.2 KMP
├─ Multiple patterns → 4.3 Rabin-Karp
└─ Need all occurrences → 4.4 Z-Algorithm

Need prefix-based word search or autocomplete?
├─ YES → 5.1 Trie
└─ NO  → Continue

Need suffix-based queries (longest repeated substring)?
├─ YES → 5.2 Suffix Array
└─ NO  → Continue

Need all palindromic substrings efficiently?
└─ YES → 5.3 Manacher's Algorithm
```

---

## Level 1: The Physical Layer

**Goal**: Master basic string iteration patterns (forward, backward, stride, boundaries, character frequency).

---

### 1.1 Forward/Backward/Stride Iteration

**Why**: Most string problems require controlled iteration over characters. Understanding loop direction and stride patterns prevents off-by-one errors.

**What**: Three fundamental iteration patterns:
- **Forward**: Left to right (`0` to `n-1`)
- **Backward**: Right to left (`n-1` to `0`)
- **Stride**: Process in chunks (e.g., every `k` characters, or by levels)

**How (stepflow)**:
1. **Forward**: `for i in range(len(s))`
2. **Backward**: `for i in range(len(s) - 1, -1, -1)`
3. **Stride**: `for i in range(0, len(s), k)`

**Where**: Character counting, reversal, chunk processing, digit extraction.

**When**: Single-pass problems where order matters.

**Visual**:
```
Forward:
  s = "abcde"
  i:  0 1 2 3 4
      a b c d e  →

Backward:
  s = "abcde"
  i:  4 3 2 1 0
      ← e d c b a

Stride (k=2):
  s = "abcdef"
  i:  0   2   4
      a   c   e
```

**Python**:
```python
# Forward
def count_vowels(s):
    count = 0
    for char in s:
        if char in 'aeiouAEIOU':
            count += 1
    return count

# Backward (build reversed)
def reverse_string(s):
    result = []
    for i in range(len(s) - 1, -1, -1):
        result.append(s[i])
    return ''.join(result)

# Stride (process pairs)
def swap_pairs(s):
    chars = list(s)
    for i in range(0, len(chars) - 1, 2):
        chars[i], chars[i + 1] = chars[i + 1], chars[i]
    return ''.join(chars)
```

**C#**:
```csharp
// Forward
public static int CountVowels(string s)
{
    int count = 0;
    foreach (char c in s)
    {
        if ("aeiouAEIOU".IndexOf(c) >= 0)
            count++;
    }
    return count;
}

// Backward
public static string ReverseString(string s)
{
    var result = new StringBuilder();
    for (int i = s.Length - 1; i >= 0; i--)
        result.Append(s[i]);
    return result.ToString();
}

// Stride
public static string SwapPairs(string s)
{
    var chars = s.ToCharArray();
    for (int i = 0; i < chars.Length - 1; i += 2)
    {
        var temp = chars[i];
        chars[i] = chars[i + 1];
        chars[i + 1] = temp;
    }
    return new string(chars);
}
```

**Common pitfalls**:
- Off-by-one in backward iteration (forgetting `-1, -1` in `range`)
- Not handling odd-length strings in stride patterns
- Modifying string during iteration (strings are immutable in Python/C#)

**Tips & tricks**:
- **Invariant**: Each index visited exactly once
- For in-place modifications, convert to list/char array first
- Stride patterns useful for alternating positions (even/odd indices)

**LeetCode Practice**:
- 344 - Reverse String
- 557 - Reverse Words in a String III
- 942 - DI String Match

---

### 1.2 Boundary Control

**Why**: String problems have precise boundaries (`[0, len-1]`). Checking bounds prevents index errors and handles edge cases.

**What**: Explicit boundary checking before accessing characters. Common edge cases: empty string, single character, substring extraction.

**How (stepflow)**:
1. Check if index is within `[0, len(s) - 1]`
2. For substrings: ensure `start < end` and both within bounds
3. Handle empty string (`len(s) == 0`) separately

**Where**: Substring extraction, character comparison, safe indexing.

**When**: Always before indexing strings, especially with calculated indices.

**Visual**:
```
String: "hello"
Indices: 0 1 2 3 4
Valid:  [0, 4]

Out of bounds:
  s[-1]  ✗ (negative)
  s[5]   ✗ (>= len)

Substring s[1:4]:
  Valid: checks 1 < 4, both in [0, 5)
  Result: "ell"
```

**Python**:
```python
def safe_char_at(s, i):
    """Safely access character at index i."""
    if 0 <= i < len(s):
        return s[i]
    return None

def extract_substring(s, start, end):
    """Extract substring [start, end), with bounds check."""
    if start < 0 or end > len(s) or start >= end:
        return ""
    return s[start:end]

def is_valid_palindrome_indices(s, left, right):
    """Check if indices form valid palindrome range."""
    while left >= 0 and right < len(s) and s[left] == s[right]:
        left -= 1
        right += 1
    return right - left - 1  # Length of palindrome
```

**C#**:
```csharp
public static char? SafeCharAt(string s, int i)
{
    if (i >= 0 && i < s.Length)
        return s[i];
    return null;
}

public static string ExtractSubstring(string s, int start, int end)
{
    if (start < 0 || end > s.Length || start >= end)
        return "";
    return s.Substring(start, end - start);
}

public static int IsValidPalindromeIndices(string s, int left, int right)
{
    while (left >= 0 && right < s.Length && s[left] == s[right])
    {
        left--;
        right++;
    }
    return right - left - 1;
}
```

**Common pitfalls**:
- Forgetting to check bounds before accessing `s[i]`
- Using `len(s)` as valid index (should be `len(s) - 1`)
- Not handling empty strings (`len(s) == 0`)

**Tips & tricks**:
- **Invariant**: All accesses within `[0, len - 1]`
- Python slicing is forgiving (out-of-bounds returns empty), indexing is not
- For expanding palindromes, always check bounds before comparing

**LeetCode Practice**:
- 5 - Longest Palindromic Substring
- 125 - Valid Palindrome
- 680 - Valid Palindrome II

---

### 1.3 Character Frequency & Hashing

**Why**: Many string problems require counting character occurrences or checking character distribution (anagrams, permutations).

**What**: Use hash map (dictionary) to track character frequencies. Enables O(1) lookup and update.

**How (stepflow)**:
1. Initialize empty hash map `freq = {}`
2. Iterate through string: `freq[char] = freq.get(char, 0) + 1`
3. Query frequency: `freq.get(char, 0)`
4. Compare frequencies for anagram checks

**Where**: Anagrams, character counting, substring permutation checks.

**When**: Need to track "what characters exist" or "how many of each character".

**Visual**:
```
String: "aabbcc"

Frequency map:
  'a' → 2
  'b' → 2
  'c' → 2

Anagram check:
  "abc" vs "bca"
  freq1: {'a':1, 'b':1, 'c':1}
  freq2: {'b':1, 'c':1, 'a':1}
  freq1 == freq2 ✓
```

**Python**:
```python
from collections import Counter, defaultdict

def char_frequency(s):
    """Count character frequencies."""
    freq = {}
    for char in s:
        freq[char] = freq.get(char, 0) + 1
    return freq

def are_anagrams(s1, s2):
    """Check if two strings are anagrams."""
    if len(s1) != len(s2):
        return False
    return Counter(s1) == Counter(s2)

def first_unique_char(s):
    """Find index of first non-repeating character."""
    freq = Counter(s)
    for i, char in enumerate(s):
        if freq[char] == 1:
            return i
    return -1
```

**C#**:
```csharp
public static Dictionary<char, int> CharFrequency(string s)
{
    var freq = new Dictionary<char, int>();
    foreach (char c in s)
    {
        if (freq.ContainsKey(c))
            freq[c]++;
        else
            freq[c] = 1;
    }
    return freq;
}

public static bool AreAnagrams(string s1, string s2)
{
    if (s1.Length != s2.Length) return false;
    
    var freq1 = CharFrequency(s1);
    var freq2 = CharFrequency(s2);
    
    if (freq1.Count != freq2.Count) return false;
    foreach (var kv in freq1)
    {
        if (!freq2.ContainsKey(kv.Key) || freq2[kv.Key] != kv.Value)
            return false;
    }
    return true;
}

public static int FirstUniqueChar(string s)
{
    var freq = CharFrequency(s);
    for (int i = 0; i < s.Length; i++)
    {
        if (freq[s[i]] == 1)
            return i;
    }
    return -1;
}
```

**Common pitfalls**:
- Not initializing frequency to 0 before incrementing
- Comparing frequency maps incorrectly (use equality check)
- Forgetting case sensitivity (convert to lowercase if needed)

**Tips & tricks**:
- **Invariant**: `freq[char]` = number of times `char` appears in string
- Python `Counter` simplifies frequency counting
- For fixed character set (e.g., lowercase letters), use array `freq[26]`

**LeetCode Practice**:
- 242 - Valid Anagram
- 383 - Ransom Note
- 387 - First Unique Character in a String

---

## Level 2: Two Pointers

**Goal**: Master two-pointer techniques for efficient string traversal without extra space.

---

### 2.1 Opposite Direction (Convergent)

**Why**: Many string problems can be solved by comparing characters from both ends moving inward (palindromes, pair finding).

**What**: Place one pointer at start (`left = 0`) and one at end (`right = len - 1`). Move inward based on conditions until pointers meet.

**How (stepflow)**:
1. Initialize `left = 0`, `right = len(s) - 1`
2. While `left < right`:
   - Process/compare `s[left]` and `s[right]`
   - Move `left++` and/or `right--` based on condition
3. Terminate when `left >= right`

**Where**: Palindrome checks, pair finding in sorted strings, string reversal.

**When**: Need to compare characters from opposite ends or find pairs with specific sum.

**Visual**:
```
Palindrome check: "racecar"
  left →       ← right
   0 1 2 3 4 5 6
   r a c e c a r
   ✓ ✓ ✓ (meet at center)

Not palindrome: "hello"
  left →       ← right
   0 1 2 3 4
   h e l l o
   ✗ (h != o)
```

**Python**:
```python
def is_palindrome(s):
    """Check if string is palindrome."""
    left, right = 0, len(s) - 1
    while left < right:
        if s[left] != s[right]:
            return False
        left += 1
        right -= 1
    return True

def is_palindrome_ignore_nonalpha(s):
    """Check palindrome ignoring non-alphanumeric."""
    left, right = 0, len(s) - 1
    while left < right:
        # Skip non-alphanumeric
        while left < right and not s[left].isalnum():
            left += 1
        while left < right and not s[right].isalnum():
            right -= 1
        
        if s[left].lower() != s[right].lower():
            return False
        left += 1
        right -= 1
    return True

def reverse_string_inplace(chars):
    """Reverse list of characters in-place."""
    left, right = 0, len(chars) - 1
    while left < right:
        chars[left], chars[right] = chars[right], chars[left]
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
        if (s[left] != s[right])
            return false;
        left++;
        right--;
    }
    return true;
}

public static bool IsPalindromeIgnoreNonalpha(string s)
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

public static void ReverseStringInPlace(char[] chars)
{
    int left = 0, right = chars.Length - 1;
    while (left < right)
    {
        var temp = chars[left];
        chars[left] = chars[right];
        chars[right] = temp;
        left++;
        right--;
    }
}
```

**Common pitfalls**:
- Not handling empty or single-character strings
- Forgetting bounds check when skipping characters
- Off-by-one when comparing (use `<` not `<=`)

**Tips & tricks**:
- **Invariant**: `left` and `right` converge; all characters outside `[left, right]` are processed
- O(n) time, O(1) space (no extra arrays)
- For case-insensitive, use `.lower()` or `char.ToLower()`

**LeetCode Practice**:
- 125 - Valid Palindrome
- 344 - Reverse String
- 167 - Two Sum II (sorted array concept applies to strings)

---

### 2.2 Same Direction (Fast-Slow)

**Why**: Some string problems require in-place modification where one pointer writes and another reads ahead (removing duplicates, vowels, spaces).

**What**: Both pointers start at beginning. **Slow** pointer tracks valid position, **fast** pointer explores ahead. When fast finds valid character, copy to slow position.

**How (stepflow)**:
1. Initialize `slow = 0`, `fast = 0`
2. While `fast < len(s)`:
   - If `s[fast]` is valid, `s[slow] = s[fast]`, `slow++`
   - Always `fast++`
3. Result is `s[0:slow]`

**Where**: Remove duplicates, remove vowels, partition string.

**When**: Need in-place modification without extra space.

**Visual**:
```
Remove duplicates: "aabbcc" → "abc"

  slow fast
   ↓    ↓
   a a b b c c
   
Process:
  s[0]='a', valid → write to slow=0, slow++
  s[1]='a', duplicate → skip
  s[2]='b', valid → write to slow=1, slow++
  s[3]='b', duplicate → skip
  s[4]='c', valid → write to slow=2, slow++
  s[5]='c', duplicate → skip
  
Result: s[0:3] = "abc"
```

**Python**:
```python
def remove_duplicates_sorted(s):
    """Remove adjacent duplicates in sorted string."""
    if not s:
        return ""
    
    chars = list(s)
    slow = 1
    for fast in range(1, len(chars)):
        if chars[fast] != chars[slow - 1]:
            chars[slow] = chars[fast]
            slow += 1
    return ''.join(chars[:slow])

def remove_vowels(s):
    """Remove all vowels from string."""
    vowels = set('aeiouAEIOU')
    chars = list(s)
    slow = 0
    for fast in range(len(chars)):
        if chars[fast] not in vowels:
            chars[slow] = chars[fast]
            slow += 1
    return ''.join(chars[:slow])

def move_zeros(s):
    """Move all '0' characters to end."""
    chars = list(s)
    slow = 0
    for fast in range(len(chars)):
        if chars[fast] != '0':
            chars[slow] = chars[fast]
            slow += 1
    # Fill remaining with '0'
    while slow < len(chars):
        chars[slow] = '0'
        slow += 1
    return ''.join(chars)
```

**C#**:
```csharp
public static string RemoveDuplicatesSorted(string s)
{
    if (string.IsNullOrEmpty(s)) return "";
    
    var chars = s.ToCharArray();
    int slow = 1;
    for (int fast = 1; fast < chars.Length; fast++)
    {
        if (chars[fast] != chars[slow - 1])
        {
            chars[slow] = chars[fast];
            slow++;
        }
    }
    return new string(chars, 0, slow);
}

public static string RemoveVowels(string s)
{
    var vowels = new HashSet<char>("aeiouAEIOU");
    var chars = s.ToCharArray();
    int slow = 0;
    for (int fast = 0; fast < chars.Length; fast++)
    {
        if (!vowels.Contains(chars[fast]))
        {
            chars[slow] = chars[fast];
            slow++;
        }
    }
    return new string(chars, 0, slow);
}
```

**Common pitfalls**:
- Not converting string to mutable structure (list/char array)
- Forgetting to return `s[0:slow]` (not entire string)
- Off-by-one when comparing with `slow - 1`

**Tips & tricks**:
- **Invariant**: `[0, slow)` contains valid result; `[fast, n)` unexplored
- O(n) time, O(1) space (excluding output)
- Pattern also called "partition" or "filter in-place"

**LeetCode Practice**:
- 26 - Remove Duplicates from Sorted Array (concept applies to strings)
- 283 - Move Zeroes (concept applies to strings)
- 1047 - Remove All Adjacent Duplicates In String

---

## Level 3: Sliding Window

**Goal**: Master sliding window patterns for substring problems (fixed and variable size).

---

### 3.1 Fixed-Size Window

**Why**: Many substring problems ask for properties of substrings with exact length `k` (max sum, specific pattern, permutation).

**What**: Maintain a window of exactly `k` characters. Slide window one character at a time: add new character on right, remove old character from left.

**How (stepflow)**:
1. Build initial window `[0, k-1]`
2. Process window, record result
3. Slide: `left++`, add `s[right++]`
4. Repeat until `right >= len(s)`

**Where**: Permutation in string, max/min in fixed window, anagram search.

**When**: Problem specifies exact substring length.

**Visual**:
```
Find max of 3-char window: "abcde"

Window 1: [a b c]   → process
Window 2:  [b c d]  → slide right, drop 'a', add 'd'
Window 3:   [c d e] → slide right, drop 'b', add 'e'
```

**Python**:
```python
from collections import Counter

def find_anagrams(s, p):
    """Find all anagram start indices of p in s."""
    if len(p) > len(s):
        return []
    
    result = []
    p_count = Counter(p)
    window_count = Counter()
    k = len(p)
    
    # Build first window
    for i in range(k):
        window_count[s[i]] += 1
    
    if window_count == p_count:
        result.append(0)
    
    # Slide window
    for i in range(k, len(s)):
        # Add new character
        window_count[s[i]] += 1
        # Remove old character
        window_count[s[i - k]] -= 1
        if window_count[s[i - k]] == 0:
            del window_count[s[i - k]]
        
        # Check if anagram
        if window_count == p_count:
            result.append(i - k + 1)
    
    return result

def max_vowels_in_window(s, k):
    """Max vowels in any substring of length k."""
    vowels = set('aeiouAEIOU')
    
    # First window
    count = sum(1 for i in range(k) if s[i] in vowels)
    max_count = count
    
    # Slide window
    for i in range(k, len(s)):
        if s[i] in vowels:
            count += 1
        if s[i - k] in vowels:
            count -= 1
        max_count = max(max_count, count)
    
    return max_count
```

**C#**:
```csharp
public static List<int> FindAnagrams(string s, string p)
{
    var result = new List<int>();
    if (p.Length > s.Length) return result;
    
    var pCount = new Dictionary<char, int>();
    var windowCount = new Dictionary<char, int>();
    
    foreach (char c in p)
        pCount[c] = pCount.GetValueOrDefault(c, 0) + 1;
    
    int k = p.Length;
    
    // Build first window
    for (int i = 0; i < k; i++)
    {
        windowCount[s[i]] = windowCount.GetValueOrDefault(s[i], 0) + 1;
    }
    
    if (DictEqual(pCount, windowCount))
        result.Add(0);
    
    // Slide window
    for (int i = k; i < s.Length; i++)
    {
        windowCount[s[i]] = windowCount.GetValueOrDefault(s[i], 0) + 1;
        windowCount[s[i - k]]--;
        if (windowCount[s[i - k]] == 0)
            windowCount.Remove(s[i - k]);
        
        if (DictEqual(pCount, windowCount))
            result.Add(i - k + 1);
    }
    
    return result;
}

private static bool DictEqual(Dictionary<char, int> d1, Dictionary<char, int> d2)
{
    if (d1.Count != d2.Count) return false;
    foreach (var kv in d1)
    {
        if (!d2.ContainsKey(kv.Key) || d2[kv.Key] != kv.Value)
            return false;
    }
    return true;
}
```

**Common pitfalls**:
- Not handling window boundaries correctly (off-by-one)
- Forgetting to remove old character when sliding
- Comparing hash maps incorrectly

**Tips & tricks**:
- **Invariant**: Window size always exactly `k`
- Each character added once, removed once → O(n) total
- Use frequency map for anagram/permutation checks

**LeetCode Practice**:
- 438 - Find All Anagrams in a String
- 567 - Permutation in String
- 1456 - Maximum Number of Vowels in a Substring of Given Length

---

### 3.2 Variable-Size Window

**Why**: Many problems ask for longest/shortest substring satisfying a dynamic constraint (no repeating characters, at most K distinct).

**What**: Window expands (right moves forward) until constraint violated, then shrinks (left moves forward) until valid again.

**How (stepflow)**:
1. Initialize `left = 0`, `right = 0`
2. Expand: while `right < len(s)`:
   - Add `s[right]` to window state
   - While window invalid: remove `s[left]`, `left++`
   - Update result (max/min window size)
   - `right++`

**Where**: Longest substring without repeating characters, longest substring with at most K distinct, minimum window substring.

**When**: Problem asks for longest/shortest substring with dynamic constraint.

**Visual**:
```
Longest substring without repeating: "abcabcbb"

Window grows:
  [a] → [a b] → [a b c] (all unique)
  
Add 'a' again → duplicate!
  Shrink: [a b c a] → [b c a] → [c a] (valid)
  
Continue: [c a b] → [a b c] → [b c] → [c b] → [b] → [b b]
  
Max window size: 3 ("abc" or "bca" or "cab")
```

**Python**:
```python
def length_of_longest_substring(s):
    """Longest substring without repeating characters."""
    char_set = set()
    left = 0
    max_len = 0
    
    for right in range(len(s)):
        # Shrink until no duplicates
        while s[right] in char_set:
            char_set.remove(s[left])
            left += 1
        
        # Add current character
        char_set.add(s[right])
        max_len = max(max_len, right - left + 1)
    
    return max_len

def length_of_longest_substring_k_distinct(s, k):
    """Longest substring with at most k distinct characters."""
    char_count = {}
    left = 0
    max_len = 0
    
    for right in range(len(s)):
        # Add character
        char_count[s[right]] = char_count.get(s[right], 0) + 1
        
        # Shrink until <= k distinct
        while len(char_count) > k:
            char_count[s[left]] -= 1
            if char_count[s[left]] == 0:
                del char_count[s[left]]
            left += 1
        
        max_len = max(max_len, right - left + 1)
    
    return max_len

def character_replacement(s, k):
    """Longest substring with at most k replacements."""
    char_count = {}
    left = 0
    max_len = 0
    max_freq = 0
    
    for right in range(len(s)):
        char_count[s[right]] = char_count.get(s[right], 0) + 1
        max_freq = max(max_freq, char_count[s[right]])
        
        # Window invalid if replacements needed > k
        while (right - left + 1) - max_freq > k:
            char_count[s[left]] -= 1
            left += 1
        
        max_len = max(max_len, right - left + 1)
    
    return max_len
```

**C#**:
```csharp
public static int LengthOfLongestSubstring(string s)
{
    var charSet = new HashSet<char>();
    int left = 0, maxLen = 0;
    
    for (int right = 0; right < s.Length; right++)
    {
        while (charSet.Contains(s[right]))
        {
            charSet.Remove(s[left]);
            left++;
        }
        
        charSet.Add(s[right]);
        maxLen = Math.Max(maxLen, right - left + 1);
    }
    
    return maxLen;
}

public static int LengthOfLongestSubstringKDistinct(string s, int k)
{
    var charCount = new Dictionary<char, int>();
    int left = 0, maxLen = 0;
    
    for (int right = 0; right < s.Length; right++)
    {
        charCount[s[right]] = charCount.GetValueOrDefault(s[right], 0) + 1;
        
        while (charCount.Count > k)
        {
            charCount[s[left]]--;
            if (charCount[s[left]] == 0)
                charCount.Remove(s[left]);
            left++;
        }
        
        maxLen = Math.Max(maxLen, right - left + 1);
    }
    
    return maxLen;
}
```

**Common pitfalls**:
- Not tracking window state correctly (set/map)
- Forgetting to shrink window when invalid
- Computing window size wrong (`right - left + 1`)

**Tips & tricks**:
- **Invariant**: Window `[left, right]` is valid; all characters visited at most twice
- O(n) time even with inner while loop (amortized)
- Use set for membership, map for frequency

**LeetCode Practice**:
- 3 - Longest Substring Without Repeating Characters
- 424 - Longest Repeating Character Replacement
- 340 - Longest Substring with At Most K Distinct Characters

---

## Level 4: Pattern Matching

**Goal**: Master efficient string search algorithms (naive, KMP, Rabin-Karp, Z-algorithm).

---

### 4.1 Naive Pattern Matching

**Why**: Simplest pattern matching approach. Good baseline for small inputs or when pattern is short.

**What**: For each position in text, check if pattern matches starting there.

**How (stepflow)**:
1. For `i` in `0` to `len(text) - len(pattern)`:
   - Check if `text[i:i+len(pattern)] == pattern`
   - If match, record index `i`

**Where**: Simple pattern search, short patterns, educational purposes.

**When**: Pattern is very short or simplicity matters more than performance.

**Visual**:
```
Text: "ababcabcab"
Pattern: "abc"

Check positions:
  i=0: "aba" != "abc"
  i=1: "bab" != "abc"
  i=2: "abc" == "abc" ✓ (match!)
  i=3: "bca" != "abc"
  i=4: "cab" != "abc"
  i=5: "abc" == "abc" ✓ (match!)
  ...
```

**Python**:
```python
def naive_pattern_search(text, pattern):
    """Find all occurrences of pattern in text."""
    matches = []
    n, m = len(text), len(pattern)
    
    for i in range(n - m + 1):
        # Check if pattern matches at position i
        match = True
        for j in range(m):
            if text[i + j] != pattern[j]:
                match = False
                break
        if match:
            matches.append(i)
    
    return matches
```

**C#**:
```csharp
public static List<int> NaivePatternSearch(string text, string pattern)
{
    var matches = new List<int>();
    int n = text.Length, m = pattern.Length;
    
    for (int i = 0; i <= n - m; i++)
    {
        bool match = true;
        for (int j = 0; j < m; j++)
        {
            if (text[i + j] != pattern[j])
            {
                match = false;
                break;
            }
        }
        if (match)
            matches.Add(i);
    }
    
    return matches;
}
```

**Complexity**:
- **Time**: O((n - m + 1) × m) = O(n × m) worst case
- **Space**: O(1)

**Common pitfalls**:
- Off-by-one in loop bound (`n - m + 1`)
- Not handling empty pattern or text

**Tips & tricks**:
- **Invariant**: Each starting position checked exactly once
- Python built-in: `text.find(pattern)` uses optimized naive approach
- For multiple searches on same text, use KMP or Rabin-Karp

---

### 4.2 KMP Algorithm

**Why**: Avoids re-checking characters after mismatch by using pattern's internal structure (longest prefix suffix).

**What**: Precompute **LPS (Longest Prefix Suffix) array** that tells how far to shift pattern on mismatch. Search in O(n + m) time.

**How (stepflow)**:
1. **Compute LPS**:
   - `LPS[i]` = length of longest proper prefix that is also suffix for `pattern[0:i+1]`
2. **Search**:
   - Match characters; on mismatch, shift pattern using LPS (no backtracking in text)

**Where**: Efficient exact pattern matching, repeated searches, pattern has repetition.

**When**: Need O(n + m) guaranteed time, pattern has internal structure.

**Visual**:
```
Pattern: "ababc"
LPS:     [0, 0, 1, 2, 0]

Explanation:
  pattern[0:1] = "a"  → LPS[0] = 0 (no proper prefix)
  pattern[0:2] = "ab" → LPS[1] = 0 (no match)
  pattern[0:3] = "aba" → LPS[2] = 1 ("a" prefix = "a" suffix)
  pattern[0:4] = "abab" → LPS[3] = 2 ("ab" prefix = "ab" suffix)
  pattern[0:5] = "ababc" → LPS[4] = 0 (no match)

Matching with mismatch:
  Text: "ababcababcab"
  Pattern: "ababc"
  
  Match "abab", mismatch at 'c' vs 'a'
  Use LPS[3]=2 → shift pattern by (4-2=2), continue from text[4]
```

**Python**:
```python
def compute_lps(pattern):
    """Compute Longest Prefix Suffix array."""
    m = len(pattern)
    lps = [0] * m
    length = 0  # Length of previous longest prefix suffix
    i = 1
    
    while i < m:
        if pattern[i] == pattern[length]:
            length += 1
            lps[i] = length
            i += 1
        else:
            if length != 0:
                length = lps[length - 1]  # Fallback
            else:
                lps[i] = 0
                i += 1
    
    return lps

def kmp_search(text, pattern):
    """KMP pattern matching."""
    n, m = len(text), len(pattern)
    if m == 0:
        return []
    
    lps = compute_lps(pattern)
    matches = []
    
    i = 0  # Index for text
    j = 0  # Index for pattern
    
    while i < n:
        if text[i] == pattern[j]:
            i += 1
            j += 1
        
        if j == m:
            matches.append(i - j)
            j = lps[j - 1]
        elif i < n and text[i] != pattern[j]:
            if j != 0:
                j = lps[j - 1]
            else:
                i += 1
    
    return matches
```

**C#**:
```csharp
public static int[] ComputeLPS(string pattern)
{
    int m = pattern.Length;
    var lps = new int[m];
    int length = 0;
    int i = 1;
    
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

public static List<int> KMPSearch(string text, string pattern)
{
    int n = text.Length, m = pattern.Length;
    var matches = new List<int>();
    if (m == 0) return matches;
    
    var lps = ComputeLPS(pattern);
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
            matches.Add(i - j);
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
    
    return matches;
}
```

**Complexity**:
- **Time**: O(n + m) (linear)
- **Space**: O(m) (LPS array)

**Common pitfalls**:
- Computing LPS incorrectly (fallback logic)
- Not handling empty pattern
- Off-by-one when recording matches

**Tips & tricks**:
- **Invariant**: Text index never backtracks; pattern shifts efficiently
- LPS preprocessing enables O(n) search
- Best when pattern has repeated prefixes

**LeetCode Practice**:
- 28 - Find the Index of the First Occurrence in a String
- 214 - Shortest Palindrome (uses KMP concept)

---

### 4.3 Rabin-Karp (Rolling Hash)

**Why**: Uses hashing to compare pattern with text substrings. Efficient for multiple pattern searches.

**What**: Compute hash of pattern and rolling hash of text substrings. Compare hashes first; verify with string comparison only on hash match.

**How (stepflow)**:
1. Compute `hash(pattern)`
2. Compute `hash(text[0:m])`
3. For each window in text:
   - If `hash(window) == hash(pattern)`: verify strings
   - Roll hash: remove leftmost char, add next char
   - O(1) hash update using polynomial rolling hash

**Where**: Multiple pattern search, 2D pattern matching, plagiarism detection.

**When**: Need to search multiple patterns or pattern length is large.

**Visual**:
```
Text: "abcabc"
Pattern: "abc"
Hash function: polynomial with base 256, mod 101

hash("abc") = (a×256² + b×256 + c) mod 101 = X

Rolling hash:
  Window 1: "abc" → hash = X (match!)
  Window 2: "bca" → remove 'a', add 'a'
  Window 3: "cab" → remove 'b', add 'b'
  Window 4: "abc" → hash = X (match!)
```

**Python**:
```python
def rabin_karp(text, pattern):
    """Rabin-Karp pattern matching with rolling hash."""
    n, m = len(text), len(pattern)
    if m > n:
        return []
    
    d = 256  # Number of characters in alphabet
    q = 101  # Prime modulus
    
    # Compute hash(pattern) and hash(text[0:m])
    p_hash = 0  # Pattern hash
    t_hash = 0  # Text window hash
    h = 1  # d^(m-1) mod q
    
    for i in range(m - 1):
        h = (h * d) % q
    
    for i in range(m):
        p_hash = (d * p_hash + ord(pattern[i])) % q
        t_hash = (d * t_hash + ord(text[i])) % q
    
    matches = []
    
    # Slide pattern over text
    for i in range(n - m + 1):
        # Check hash match
        if p_hash == t_hash:
            # Verify actual strings
            if text[i:i+m] == pattern:
                matches.append(i)
        
        # Roll hash for next window
        if i < n - m:
            t_hash = (d * (t_hash - ord(text[i]) * h) + ord(text[i + m])) % q
            if t_hash < 0:
                t_hash += q
    
    return matches
```

**C#**:
```csharp
public static List<int> RabinKarp(string text, string pattern)
{
    int n = text.Length, m = pattern.Length;
    var matches = new List<int>();
    if (m > n) return matches;
    
    int d = 256;
    int q = 101;
    
    long pHash = 0, tHash = 0, h = 1;
    
    for (int i = 0; i < m - 1; i++)
        h = (h * d) % q;
    
    for (int i = 0; i < m; i++)
    {
        pHash = (d * pHash + pattern[i]) % q;
        tHash = (d * tHash + text[i]) % q;
    }
    
    for (int i = 0; i <= n - m; i++)
    {
        if (pHash == tHash)
        {
            bool match = true;
            for (int j = 0; j < m; j++)
            {
                if (text[i + j] != pattern[j])
                {
                    match = false;
                    break;
                }
            }
            if (match)
                matches.Add(i);
        }
        
        if (i < n - m)
        {
            tHash = (d * (tHash - text[i] * h) + text[i + m]) % q;
            if (tHash < 0)
                tHash += q;
        }
    }
    
    return matches;
}
```

**Complexity**:
- **Time**: O(n + m) average, O(n × m) worst case (many hash collisions)
- **Space**: O(1)

**Common pitfalls**:
- Hash collisions → always verify strings after hash match
- Negative hash values → add modulus
- Overflow with large alphabets

**Tips & tricks**:
- **Invariant**: Hash update is O(1), rolling property
- Choose prime modulus to reduce collisions
- For multiple patterns, compute all pattern hashes once

---

### 4.4 Z-Algorithm

**Why**: Computes longest substring starting at each position that matches prefix. Useful for pattern matching and prefix/suffix problems.

**What**: Build **Z-array** where `Z[i]` = length of longest substring starting at `i` that matches prefix of string.

**How (stepflow)**:
1. Concatenate pattern and text: `S = pattern + "$" + text`
2. Compute Z-array for `S`
3. For all `i` where `Z[i] == len(pattern)`, found match at `i - len(pattern) - 1` in text

**Where**: Pattern matching, prefix/suffix matching, string periodicity.

**When**: Need all prefix matches or comparing string prefixes.

**Visual**:
```
String: "aabaaab"
Z-array:

i:  0 1 2 3 4 5 6
S:  a a b a a a b
Z:  - 1 0 2 2 1 0

Explanation:
  Z[0] = undefined (entire string)
  Z[1] = 1 ("a" matches prefix "a")
  Z[2] = 0 ("b" doesn't match prefix "a")
  Z[3] = 2 ("aa" matches prefix "aa")
  Z[4] = 2 ("aa" matches prefix "aa")
  Z[5] = 1 ("a" matches prefix "a")
  Z[6] = 0 ("b" doesn't match prefix "a")
```

**Python**:
```python
def compute_z_array(s):
    """Compute Z-array for string s."""
    n = len(s)
    z = [0] * n
    left, right = 0, 0
    
    for i in range(1, n):
        if i > right:
            # Outside Z-box, compute from scratch
            left, right = i, i
            while right < n and s[right] == s[right - left]:
                right += 1
            z[i] = right - left
            right -= 1
        else:
            # Inside Z-box, use previously computed values
            k = i - left
            if z[k] < right - i + 1:
                z[i] = z[k]
            else:
                left = i
                while right < n and s[right] == s[right - left]:
                    right += 1
                z[i] = right - left
                right -= 1
    
    return z

def z_algorithm_search(text, pattern):
    """Pattern matching using Z-algorithm."""
    concat = pattern + "$" + text
    z = compute_z_array(concat)
    m = len(pattern)
    
    matches = []
    for i in range(m + 1, len(concat)):
        if z[i] == m:
            matches.append(i - m - 1)
    
    return matches
```

**C#**:
```csharp
public static int[] ComputeZArray(string s)
{
    int n = s.Length;
    var z = new int[n];
    int left = 0, right = 0;
    
    for (int i = 1; i < n; i++)
    {
        if (i > right)
        {
            left = right = i;
            while (right < n && s[right] == s[right - left])
                right++;
            z[i] = right - left;
            right--;
        }
        else
        {
            int k = i - left;
            if (z[k] < right - i + 1)
            {
                z[i] = z[k];
            }
            else
            {
                left = i;
                while (right < n && s[right] == s[right - left])
                    right++;
                z[i] = right - left;
                right--;
            }
        }
    }
    
    return z;
}

public static List<int> ZAlgorithmSearch(string text, string pattern)
{
    string concat = pattern + "$" + text;
    var z = ComputeZArray(concat);
    int m = pattern.Length;
    var matches = new List<int>();
    
    for (int i = m + 1; i < concat.Length; i++)
    {
        if (z[i] == m)
            matches.Add(i - m - 1);
    }
    
    return matches;
}
```

**Complexity**:
- **Time**: O(n + m) (linear)
- **Space**: O(n + m) (Z-array)

**Common pitfalls**:
- Z-box logic is tricky (left/right pointers)
- Forgetting sentinel character (`$`) in concatenation
- Off-by-one when extracting match positions

**Tips & tricks**:
- **Invariant**: Z-box `[left, right]` is rightmost segment matching prefix
- Z-algorithm simpler than KMP for some prefix problems
- Useful for string periodicity detection

---

## Level 5: Advanced Structures

**Goal**: Master advanced string data structures (Trie, Suffix Array, Manacher's).

---

### 5.1 Trie (Prefix Tree)

**Why**: Efficiently stores and searches strings with common prefixes. Enables prefix-based queries (autocomplete, dictionary lookups).

**What**: Tree where each node represents a character. Paths from root to leaves represent strings. Common prefixes share paths.

**How (stepflow)**:
1. **Insert**: For each character in word, create/follow child node
2. **Search**: Follow path for each character; return True if end-of-word marker found
3. **Prefix search**: Follow path for prefix; return True if path exists

**Where**: Autocomplete, spell checker, dictionary, word search games.

**When**: Need efficient prefix queries or store many strings with common prefixes.

**Visual**:
```
Trie storing: "cat", "car", "card", "dog", "do"

       root
       /  \
      c    d
      |    |
      a    o ($)
     / \   |
    t   r  g ($)
   ($) |
       d ($)

($) = end-of-word marker

Search "car": root → c → a → r → ($) ✓
Search "ca": root → c → a (no $) ✗
Prefix "ca": root → c → a ✓ (path exists)
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
    
    def insert(self, word):
        """Insert word into trie."""
        node = self.root
        for char in word:
            if char not in node.children:
                node.children[char] = TrieNode()
            node = node.children[char]
        node.is_end_of_word = True
    
    def search(self, word):
        """Search exact word."""
        node = self.root
        for char in word:
            if char not in node.children:
                return False
            node = node.children[char]
        return node.is_end_of_word
    
    def starts_with(self, prefix):
        """Check if any word starts with prefix."""
        node = self.root
        for char in prefix:
            if char not in node.children:
                return False
            node = node.children[char]
        return True
    
    def find_words_with_prefix(self, prefix):
        """Find all words with given prefix."""
        node = self.root
        for char in prefix:
            if char not in node.children:
                return []
            node = node.children[char]
        
        # DFS to collect all words from this node
        result = []
        self._dfs(node, prefix, result)
        return result
    
    def _dfs(self, node, current_word, result):
        if node.is_end_of_word:
            result.append(current_word)
        for char, child in node.children.items():
            self._dfs(child, current_word + char, result)
```

**C#**:
```csharp
public class TrieNode
{
    public Dictionary<char, TrieNode> Children = new Dictionary<char, TrieNode>();
    public bool IsEndOfWord = false;
}

public class Trie
{
    private TrieNode root = new TrieNode();
    
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
```

**Complexity**:
- **Insert/Search**: O(m) where m = word length
- **Space**: O(ALPHABET_SIZE × N × M) where N = number of words, M = avg length

**Common pitfalls**:
- Not marking end-of-word (search fails)
- Memory overhead for sparse tries
- Not handling empty strings

**Tips & tricks**:
- **Invariant**: Each path from root represents a prefix
- For large alphabets, use hash map for children
- Can store additional data at nodes (frequency, metadata)

**LeetCode Practice**:
- 208 - Implement Trie (Prefix Tree)
- 211 - Design Add and Search Words Data Structure
- 212 - Word Search II

---

### 5.2 Suffix Array

**Why**: Efficiently solves suffix-based queries (longest repeated substring, pattern matching).

**What**: Array of indices representing all suffixes of string in lexicographic order. Enables binary search on suffixes.

**How (stepflow)**:
1. Generate all suffix starting indices
2. Sort indices by comparing corresponding suffixes
3. For pattern search: binary search in sorted suffixes

**Where**: Longest repeated substring, longest common substring, pattern matching.

**When**: Need multiple suffix queries or longest repeated patterns.

**Visual**:
```
String: "banana"
Suffixes:
  0: "banana"
  1: "anana"
  2: "nana"
  3: "ana"
  4: "na"
  5: "a"

Suffix Array (sorted indices):
  [5, 3, 1, 0, 4, 2]
  
Sorted suffixes:
  5: "a"
  3: "ana"
  1: "anana"
  0: "banana"
  4: "na"
  2: "nana"
```

**Python**:
```python
def build_suffix_array(s):
    """Build suffix array for string s."""
    n = len(s)
    suffixes = [(s[i:], i) for i in range(n)]
    suffixes.sort()
    return [index for _, index in suffixes]

def search_pattern_suffix_array(s, pattern, suffix_array):
    """Search pattern using suffix array with binary search."""
    n = len(s)
    m = len(pattern)
    
    # Binary search for pattern
    left, right = 0, n - 1
    while left <= right:
        mid = (left + right) // 2
        suffix = s[suffix_array[mid]:]
        
        if suffix.startswith(pattern):
            return suffix_array[mid]
        elif suffix < pattern:
            left = mid + 1
        else:
            right = mid - 1
    
    return -1

def longest_repeated_substring(s):
    """Find longest repeated substring using suffix array."""
    n = len(s)
    if n == 0:
        return ""
    
    suffix_array = build_suffix_array(s)
    
    # Compute LCP (Longest Common Prefix) between adjacent suffixes
    max_len = 0
    max_index = 0
    
    for i in range(n - 1):
        # Compare adjacent suffixes in sorted order
        suffix1 = s[suffix_array[i]:]
        suffix2 = s[suffix_array[i + 1]:]
        
        # Find common prefix length
        lcp = 0
        for j in range(min(len(suffix1), len(suffix2))):
            if suffix1[j] == suffix2[j]:
                lcp += 1
            else:
                break
        
        if lcp > max_len:
            max_len = lcp
            max_index = suffix_array[i]
    
    return s[max_index:max_index + max_len]
```

**C#**:
```csharp
public static int[] BuildSuffixArray(string s)
{
    int n = s.Length;
    var suffixes = new List<(string suffix, int index)>();
    
    for (int i = 0; i < n; i++)
        suffixes.Add((s.Substring(i), i));
    
    suffixes.Sort((a, b) => string.Compare(a.suffix, b.suffix));
    
    return suffixes.Select(x => x.index).ToArray();
}

public static string LongestRepeatedSubstring(string s)
{
    int n = s.Length;
    if (n == 0) return "";
    
    var suffixArray = BuildSuffixArray(s);
    int maxLen = 0, maxIndex = 0;
    
    for (int i = 0; i < n - 1; i++)
    {
        string suffix1 = s.Substring(suffixArray[i]);
        string suffix2 = s.Substring(suffixArray[i + 1]);
        
        int lcp = 0;
        int minLen = Math.Min(suffix1.Length, suffix2.Length);
        for (int j = 0; j < minLen; j++)
        {
            if (suffix1[j] == suffix2[j])
                lcp++;
            else
                break;
        }
        
        if (lcp > maxLen)
        {
            maxLen = lcp;
            maxIndex = suffixArray[i];
        }
    }
    
    return s.Substring(maxIndex, maxLen);
}
```

**Complexity**:
- **Build**: O(n² log n) naive, O(n log n) optimized
- **Pattern search**: O(m log n) where m = pattern length
- **Space**: O(n)

**Common pitfalls**:
- Naive suffix array construction is slow (O(n² log n))
- Not computing LCP array for advanced queries
- Suffix comparison is expensive for long strings

**Tips & tricks**:
- **Invariant**: Suffix array enables binary search on all suffixes
- Combine with LCP array for O(n) longest repeated substring
- Advanced construction: DC3, SA-IS algorithms

**LeetCode Practice**:
- 1062 - Longest Repeating Substring
- 1044 - Longest Duplicate Substring

---

### 5.3 Manacher's Algorithm (Palindromes)

**Why**: Finds all palindromic substrings in linear time O(n). Much faster than expanding around each center.

**What**: Transform string to avoid even/odd length handling. Use previously computed palindrome information to skip redundant checks.

**How (stepflow)**:
1. Transform string: insert `#` between characters (`"aba"` → `"#a#b#a#"`)
2. Maintain **center** and **right boundary** of rightmost palindrome
3. For each position, use mirror property to initialize radius
4. Expand palindrome from position if needed

**Where**: Longest palindromic substring, count all palindromic substrings.

**When**: Need all palindromes efficiently or longest palindrome.

**Visual**:
```
String: "aba"
Transformed: "#a#b#a#"

Palindrome radii:
  i:  0 1 2 3 4 5 6
  s:  # a # b # a #
  P:  0 1 0 3 0 1 0

Explanation:
  P[3] = 3 means palindrome "#b#" with radius 3
  → original palindrome "aba" length = P[i] (in transformed)
```

**Python**:
```python
def manachers_algorithm(s):
    """Find longest palindromic substring using Manacher's."""
    # Transform string
    transformed = '#'.join('^{}$'.format(s))
    n = len(transformed)
    P = [0] * n
    center = 0
    right = 0
    
    for i in range(1, n - 1):
        # Mirror of i
        mirror = 2 * center - i
        
        # Use previously computed values
        if i < right:
            P[i] = min(right - i, P[mirror])
        
        # Expand palindrome centered at i
        try:
            while transformed[i + P[i] + 1] == transformed[i - P[i] - 1]:
                P[i] += 1
        except IndexError:
            pass
        
        # Update center and right boundary
        if i + P[i] > right:
            center = i
            right = i + P[i]
    
    # Find longest palindrome
    max_len = max(P)
    center_index = P.index(max_len)
    
    # Extract original palindrome
    start = (center_index - max_len) // 2
    return s[start:start + max_len]

def count_palindromic_substrings(s):
    """Count all palindromic substrings."""
    transformed = '#'.join('^{}$'.format(s))
    n = len(transformed)
    P = [0] * n
    center = 0
    right = 0
    
    for i in range(1, n - 1):
        mirror = 2 * center - i
        if i < right:
            P[i] = min(right - i, P[mirror])
        
        try:
            while transformed[i + P[i] + 1] == transformed[i - P[i] - 1]:
                P[i] += 1
        except IndexError:
            pass
        
        if i + P[i] > right:
            center = i
            right = i + P[i]
    
    # Count: each radius contributes (radius + 1) // 2 palindromes
    return sum((p + 1) // 2 for p in P)
```

**C#**:
```csharp
public static string LongestPalindromeManacher(string s)
{
    string transformed = "^#" + string.Join("#", s.ToCharArray()) + "#$";
    int n = transformed.Length;
    var P = new int[n];
    int center = 0, right = 0;
    
    for (int i = 1; i < n - 1; i++)
    {
        int mirror = 2 * center - i;
        
        if (i < right)
            P[i] = Math.Min(right - i, P[mirror]);
        
        while (i + P[i] + 1 < n && i - P[i] - 1 >= 0 &&
               transformed[i + P[i] + 1] == transformed[i - P[i] - 1])
            P[i]++;
        
        if (i + P[i] > right)
        {
            center = i;
            right = i + P[i];
        }
    }
    
    int maxLen = P.Max();
    int centerIndex = Array.IndexOf(P, maxLen);
    int start = (centerIndex - maxLen) / 2;
    
    return s.Substring(start, maxLen);
}
```

**Complexity**:
- **Time**: O(n) (linear)
- **Space**: O(n) (transformed string + P array)

**Common pitfalls**:
- Transformation syntax is tricky (`^` and `$` sentinels)
- Mirror property logic is complex
- Extracting original indices from transformed indices

**Tips & tricks**:
- **Invariant**: Never expand past rightmost known palindrome boundary
- O(n) is guaranteed because each position expands at most once
- Handles both odd and even length palindromes uniformly

**LeetCode Practice**:
- 5 - Longest Palindromic Substring
- 647 - Palindromic Substrings

---

## Invariant Library

Memorize these one-liners:

| Pattern | Invariant |
|---------|-----------|
| **Forward iteration** | Each index visited exactly once, left to right. |
| **Backward iteration** | Each index visited exactly once, right to left. |
| **Two pointers (opposite)** | `left` and `right` converge; all outside `[left, right]` processed. |
| **Two pointers (same)** | `[0, slow)` valid result; `[fast, n)` unexplored. |
| **Fixed window** | Window size always exactly `k`; each character visited once. |
| **Variable window** | Window `[left, right]` always valid; characters visited at most twice. |
| **KMP** | Text index never backtracks; pattern shifts using LPS. |
| **Rabin-Karp** | Hash update is O(1); verify strings on hash match. |
| **Z-algorithm** | Z-box `[left, right]` is rightmost segment matching prefix. |
| **Trie** | Each path from root represents a prefix; common prefixes share paths. |
| **Suffix array** | Enables binary search on all suffixes in O(m log n). |
| **Manacher's** | Never expand past rightmost palindrome boundary; O(n) total. |

---

## Quick Reference Table

| Problem Signal | Pattern | Key Data Structure | Complexity |
|----------------|---------|-------------------|------------|
| Count characters, build frequency | Character frequency | Hash map | O(n) |
| Check palindrome, reverse string | Two pointers (opposite) | Two indices | O(n) |
| Remove elements in-place | Two pointers (same) | Slow/fast pointers | O(n) |
| Substring of exact length K | Fixed-size sliding window | Window + state | O(n) |
| Longest substring with constraint | Variable sliding window | Window + set/map | O(n) |
| Find pattern in text (simple) | Naive pattern matching | None | O(n × m) |
| Find pattern (efficient, repetition) | KMP | LPS array | O(n + m) |
| Find pattern (multiple patterns) | Rabin-Karp | Rolling hash | O(n + m) avg |
| Prefix matches, all occurrences | Z-algorithm | Z-array | O(n + m) |
| Prefix-based word search | Trie | Tree | O(m) per operation |
| Longest repeated substring | Suffix array | Sorted suffixes | O(n² log n) naive |
| All palindromic substrings | Manacher's | Radius array | O(n) |

---

## Pattern Combinations

**Real problems often combine multiple patterns:**

| Problem | Patterns Used |
|---------|---------------|
| **Longest Substring Without Repeating** | Variable sliding window + Character frequency |
| **Permutation in String** | Fixed sliding window + Character frequency |
| **Shortest Palindrome** | KMP + Palindrome check |
| **Palindrome Partitioning** | Backtracking + Manacher's |
| **Word Break** | Trie + DP |
| **Longest Duplicate Substring** | Suffix array + Binary search |

---

## Final Mental Model

```
Level 1: Physical traversal
  ├─ Forward/backward/stride iteration
  ├─ Boundary control
  └─ Character frequency & hashing

Level 2: Two pointers
  ├─ Opposite direction (palindromes, pairs)
  └─ Same direction (in-place removal)

Level 3: Sliding window
  ├─ Fixed-size (exact length K)
  └─ Variable-size (longest/shortest with constraint)

Level 4: Pattern matching
  ├─ Naive (O(n×m))
  ├─ KMP (LPS array, O(n+m))
  ├─ Rabin-Karp (rolling hash)
  └─ Z-algorithm (prefix matching)

Level 5: Advanced structures
  ├─ Trie (prefix queries)
  ├─ Suffix array (suffix queries)
  └─ Manacher's (palindromes O(n))
```

**Master these patterns and you can solve 95% of string problems.**

---

**Next steps:**
1. Implement each pattern from scratch in both Python and C#
2. Solve 2–3 practice problems per pattern
3. Recognize pattern signals in new problems
4. Combine patterns for complex scenarios

**Remember**: String traversal is about **controlled iteration with invariants**. Master the invariants, and the code writes itself.