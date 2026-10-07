# 📘 Week 06 Day 2: Substring Sliding Window Patterns — Engineering Guide

> 🧭 **Navigation:** [← Previous Day](Week_06_Day_01_Palindrome_Patterns_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_06_Day_03_Parentheses_Bracket_Matching_Instructional.md)
> 
> 💡 **Instructor Note:** *Sliding window turns quadratic substring searches into amortized linear time. The two essential patterns to master are Variable-Size Windows (expanding until invalid, then shrinking) and Fixed-Size Windows (sliding a rigid frame of length K).*

---

## 🎯 LEARNING OBJECTIVES

By the end of this chapter, you will be able to:

- **Internalize** the variable-size sliding window invariant and prove why two pointers moving monotonically right achieve amortized `O(N)` time.
- **Implement** both dynamic windows (Longest Substring Without Repeating, Minimum Window Substring) and fixed windows (Permutation in String).
- **Optimize** state tracking using direct ASCII frequency arrays (`int[128]`) and jump optimizations (`lastSeen[c] + 1`) to eliminate inner contraction loops.
- **Deliver** a structured 45-minute technical interview script defending amortized pointer progression.

---

## 📖 CHAPTER 1: CONTEXT & MOTIVATION

### The Engineering Challenge

Substring queries with frequency and uniqueness constraints are ubiquitous across software infrastructure:
1. **Search & Tokenization:** Auto-suggest engines locate the longest unique prefix or keyword segment in streaming input.
2. **Packet Inspection & Intrusion Detection:** Deep-packet inspection (DPI) firewalls scan network payloads for contiguous windows containing signature bytes.
3. **Plagiarism Detection & Genomic Matching:** Finding minimum bounding windows containing a target multiset of k-mers or terms.

A brute-force solution checks all `O(N^2)` candidate windows, spending `O(K)` time validating character counts for each, resulting in `O(N^3)` or `O(N^2)` time. The sliding window paradigm guarantees each character is processed at most twice—once entering the window through `right` and at most once exiting through `left`.

> [!NOTE]
> **Interview & Systems Context:** In high-throughput parsing and coding interviews, avoid recalculating window validity from scratch at every step. Maintain an incremental delta state (e.g., an integer `formed` or `matchCount`) that updates in `O(1)` whenever a character enters or leaves. For ASCII text, always prefer fixed arrays (`int[128]`) over generic hash maps to eliminate hashing overhead and heap allocations.

---

## 🧠 CHAPTER 2: BUILDING THE MENTAL MODEL

### The Caterpillar Model: Fixed vs. Dynamic Windows

```
1. Fixed-Size Window (Length K):
   [ L ... R ] ----------> [ L ... R ] ----------> [ L ... R ]
   Window size remains strictly K = R - L + 1.
   Each step: Advance R by 1, advance L by 1. Add s[R], remove s[L-1].

2. Dynamic / Variable-Size Window:
   Step 1 (Expand R):   [ L ..... R ]        -> Pull in elements to satisfy criteria
   Step 2 (Contract L): [     L . R ]        -> Shrink while valid (or invalid) to optimize
```

### Visualizing State Progression on "abcabcbb"

Finding the longest substring without repeating characters:

```
Index:    0   1   2   3   4   5   6   7
Chars:    a   b   c   a   b   c   b   b

R=0 ('a'): [a]                     Window: "a",       Len: 1, Max: 1
R=1 ('b'): [a   b]                 Window: "ab",      Len: 2, Max: 2
R=2 ('c'): [a   b   c]             Window: "abc",     Len: 3, Max: 3
R=3 ('a'):  a  [b   c   a]         'a' duplicate! Jump L past idx 0 -> L=1. Max: 3
R=4 ('b'):  a   b  [c   a   b]     'b' duplicate! Jump L past idx 1 -> L=2. Max: 3
R=5 ('c'):  a   b   c  [a   b   c] 'c' duplicate! Jump L past idx 2 -> L=3. Max: 3
R=6 ('b'):  a   b   c   a   b  [c   b] 'b' duplicate! Jump L past idx 4 -> L=5. Max: 3
R=7 ('b'):  a   b   c   a   b   c   b  [b] 'b' duplicate! Jump L to idx 7 -> L=7. Max: 3
```

### Taxonomy of Sliding Window Variations

| Problem Type | Window Behavior | State Tracking Structure | Invariant Condition |
| :--- | :--- | :--- | :--- |
| **Longest Substring Without Repeats** | Dynamic (Expand R, Jump L) | `int[128]` last seen index | `count[char] <= 1` |
| **Minimum Window Substring** | Dynamic (Expand R, Shrink L) | Target map vs Window map + `formed` | All target counts satisfied |
| **Longest with K Distinct Characters** | Dynamic (Expand R, Shrink L) | `int[128]` char counts + `distinct` | `distinct <= K` |
| **Character Replacement with K Flips** | Dynamic (Expand R, Shrink L) | `int[26]` counts + `maxFreq` | `(R - L + 1) - maxFreq <= K` |
| **Permutation in String / Anagrams** | Fixed (Size `|P|`) | Difference array / `matches` count | Window counts match pattern |

---

## ⚙️ CHAPTER 3: MECHANICS & IMPLEMENTATION

### Operation 1: Variable Window — Longest Substring Without Repeats

Instead of shrinking `left` one character at a time using a while loop, maintain an array `lastSeen` mapping each character to its most recent index. When a duplicate `c` is seen at `right`, jump `left = max(left, lastSeen[c] + 1)`.

```
Jump Invariant:
left = max(left, lastSeen[c] + 1)
Using max() prevents 'left' from regressing backwards to duplicates outside the current window.
```

### Operation 2: Variable Window — Minimum Window Substring

Given text `s` and pattern `t`, find the shortest substring in `s` containing all characters in `t`.
1. Build frequency map of `t`. Let `required` be the number of unique characters in `t`.
2. Expand `right`. If `windowCounts[c] == targetCounts[c]`, increment `formed`.
3. While `formed == required`:
   - Update global minimum span `[minStart, minLen]`.
   - Decrement `windowCounts[s[left]]`. If it falls below `targetCounts[s[left]]`, decrement `formed`.
   - Increment `left`.

---

### 💻 Production-Grade Implementations

#### C# (.NET 8/9 — Zero Allocation with `ReadOnlySpan<char>`)

```csharp
using System;

public static class SlidingWindowSolutions
{
    /// <summary>
    /// Longest Substring Without Repeating Characters (LeetCode 3)
    /// Time Complexity: O(N) | Auxiliary Space: O(1) (128-element stack buffer)
    /// </summary>
    public static int LengthOfLongestSubstring(ReadOnlySpan<char> s)
    {
        if (s.IsEmpty) return 0;

        // Stores 1-based index (0 means unseen)
        Span<int> lastSeen = stackalloc int[128];
        int maxLength = 0;
        int left = 0;

        for (int right = 0; right < s.Length; right++)
        {
            char current = s[right];
            if (current < 128)
            {
                // Advance left pointer past previous occurrence if inside window
                left = Math.Max(left, lastSeen[current]);
                lastSeen[current] = right + 1; // 1-based index
            }

            maxLength = Math.Max(maxLength, right - left + 1);
        }

        return maxLength;
    }

    /// <summary>
    /// Minimum Window Substring (LeetCode 76)
    /// Time Complexity: O(N + M) | Auxiliary Space: O(1) (fixed 128-element arrays)
    /// </summary>
    public static string MinWindow(string s, string t)
    {
        if (string.IsNullOrEmpty(s) || string.IsNullOrEmpty(t) || s.Length < t.Length)
        {
            return string.Empty;
        }

        Span<int> targetCounts = stackalloc int[128];
        Span<int> windowCounts = stackalloc int[128];

        int requiredUnique = 0;
        foreach (char c in t)
        {
            if (c < 128)
            {
                if (targetCounts[c] == 0) requiredUnique++;
                targetCounts[c]++;
            }
        }

        int formed = 0;
        int left = 0;
        int minLen = int.MaxValue;
        int minStart = 0;

        for (int right = 0; right < s.Length; right++)
        {
            char c = s[right];
            if (c < 128)
            {
                windowCounts[c]++;
                if (targetCounts[c] > 0 && windowCounts[c] == targetCounts[c])
                {
                    formed++;
                }
            }

            // Contract window while valid
            while (left <= right && formed == requiredUnique)
            {
                int currentLen = right - left + 1;
                if (currentLen < minLen)
                {
                    minLen = currentLen;
                    minStart = left;
                }

                char leftChar = s[left];
                if (leftChar < 128)
                {
                    windowCounts[leftChar]--;
                    if (targetCounts[leftChar] > 0 && windowCounts[leftChar] < targetCounts[leftChar])
                    {
                        formed--;
                    }
                }
                left++;
            }
        }

        return minLen == int.MaxValue ? string.Empty : s.Substring(minStart, minLen);
    }

    /// <summary>
    /// Permutation in String / Fixed Window Anagram (LeetCode 567)
    /// Time Complexity: O(N) | Auxiliary Space: O(1)
    /// </summary>
    public static bool CheckInclusion(string s1, string s2)
    {
        if (s1.Length > s2.Length) return false;

        Span<int> s1Freq = stackalloc int[26];
        Span<int> winFreq = stackalloc int[26];

        for (int i = 0; i < s1.Length; i++)
        {
            s1Freq[s1[i] - 'a']++;
            winFreq[s2[i] - 'a']++;
        }

        int matches = 0;
        for (int i = 0; i < 26; i++)
        {
            if (s1Freq[i] == winFreq[i]) matches++;
        }

        for (int i = s1.Length; i < s2.Length; i++)
        {
            if (matches == 26) return true;

            int rightIdx = s2[i] - 'a';
            int leftIdx = s2[i - s1.Length] - 'a';

            // Add right character
            winFreq[rightIdx]++;
            if (winFreq[rightIdx] == s1Freq[rightIdx]) matches++;
            else if (winFreq[rightIdx] == s1Freq[rightIdx] + 1) matches--;

            // Remove left character
            winFreq[leftIdx]--;
            if (winFreq[leftIdx] == s1Freq[leftIdx]) matches++;
            else if (winFreq[leftIdx] == s1Freq[leftIdx] - 1) matches--;
        }

        return matches == 26;
    }
}
```

#### Python (3.11+ — Idiomatic & Optimized)

```python
from collections import Counter


class SlidingWindowSolutions:
    @staticmethod
    def length_of_longest_substring(s: str) -> int:
        """
        Longest substring without repeating characters using last-seen index mapping.
        Time: O(N) | Auxiliary Space: O(min(N, Alphabet))
        """
        last_seen: dict[str, int] = {}
        left = 0
        max_length = 0

        for right, ch in enumerate(s):
            if ch in last_seen and last_seen[ch] >= left:
                left = last_seen[ch] + 1
            last_seen[ch] = right
            max_length = max(max_length, right - left + 1)

        return max_length

    @staticmethod
    def min_window(s: str, t: str) -> str:
        """
        Finds the minimum substring of s containing all characters of t.
        Time: O(N + M) | Auxiliary Space: O(Alphabet)
        """
        if not s or not t or len(s) < len(t):
            return ""

        target_counts = Counter(t)
        required_unique = len(target_counts)
        window_counts: dict[str, int] = {}

        formed = 0
        left = 0
        min_len = float("inf")
        best_span = (0, 0)

        for right, char in enumerate(s):
            window_counts[char] = window_counts.get(char, 0) + 1
            if char in target_counts and window_counts[char] == target_counts[char]:
                formed += 1

            while left <= right and formed == required_unique:
                current_len = right - left + 1
                if current_len < min_len:
                    min_len = current_len
                    best_span = (left, right)

                left_char = s[left]
                window_counts[left_char] -= 1
                if (
                    left_char in target_counts
                    and window_counts[left_char] < target_counts[left_char]
                ):
                    formed -= 1
                left += 1

        return (
            ""
            if min_len == float("inf")
            else s[best_span[0] : best_span[1] + 1]
        )

    @staticmethod
    def check_inclusion(s1: str, s2: str) -> bool:
        """
        Returns True if s2 contains a permutation of s1 (fixed-size window).
        Time: O(N) | Auxiliary Space: O(1)
        """
        n1, n2 = len(s1), len(s2)
        if n1 > n2:
            return False

        c1, c2 = [0] * 26, [0] * 26
        for i in range(n1):
            c1[ord(s1[i]) - 97] += 1
            c2[ord(s2[i]) - 97] += 1

        matches = sum(1 for i in range(26) if c1[i] == c2[i])

        for i in range(n1, n2):
            if matches == 26:
                return True

            r_idx = ord(s2[i]) - 97
            l_idx = ord(s2[i - n1]) - 97

            # Add right character
            c2[r_idx] += 1
            if c2[r_idx] == c1[r_idx]:
                matches += 1
            elif c2[r_idx] == c1[r_idx] + 1:
                matches -= 1

            # Remove left character
            c2[l_idx] -= 1
            if c2[l_idx] == c1[l_idx]:
                matches += 1
            elif c2[l_idx] == c1[l_idx] - 1:
                matches -= 1

        return matches == 26
```

---

## ⚖️ CHAPTER 4: COMPLEXITY DECONSTRUCTION

### Explicit Complexity Breakdown

| Pattern / Algorithm | Time (Amortized) | Time (Worst Case) | Auxiliary Space | Output Space | Pointer Movement Bound |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Longest Substring No Repeats** | `O(N)` | `O(N)` | `O(Sigma)` (`128` ASCII) | `O(1)` | `L` and `R` advance at most `N` times each (`<= 2N` total steps) |
| **Minimum Window Substring** | `O(N + M)` | `O(N + M)` | `O(Sigma)` (`128` ASCII) | `O(K)` | `R` steps `N` times, `L` steps at most `N` times |
| **Permutation in String** | `O(N)` | `O(N)` | `O(1)` (`26` letters) | `O(1)` | Exactly `N - M` single-step slide shifts |
| **Brute Force Windows** | `O(N^2)` | `O(N^3)` | `O(Sigma)` | `O(K)` | Re-scans all `N(N+1)/2` possible substrings |

> [!TIP]
> **Why the While Loop Inside Does Not Make It Quadratic:**
> Even though there is a nested `while formed == requiredUnique` loop inside the `for right` loop, notice that `left` only moves forward and never resets. Across the entire execution of the algorithm, `left` can increment at most `N` times. Thus, the total amortized cost of the inner while loop across all iterations is `O(N)`, keeping overall runtime strictly linear.

---

## 🎙️ CHAPTER 5: 45-MINUTE INTERVIEW VERBAL SCRIPT

### Phase 1: Clarification & State Invariant (0–5 Mins)
- **Candidate:** "Let's clarify the constraints: What character set are we handling? If it is standard ASCII, we can use a direct fixed-size 128-element integer buffer instead of a generic hash map to achieve `O(1)` auxiliary space and cache locality. Also, does case matter?"
- **Interviewer:** "ASCII characters, case-sensitive."
- **Candidate:** "Understood. For Minimum Window Substring, if multiple valid windows exist with equal length, does returning any arbitrary one suffice?"
- **Interviewer:** "Yes, any valid minimal window."

### Phase 2: Approach & Invariant Definition (5–12 Mins)
- **Candidate:** "A brute-force solution checks all `O(N^2)` windows, costing `O(N^3)` or `O(N^2)` time. Instead, I'll use a dynamic two-pointer sliding window. 
I'll maintain two invariants:
1. `right` expands monotonically to pull new characters into our window until all target constraints are satisfied.
2. Once valid, `left` contracts monotonically to discard unneeded prefix characters and discover the local minimum.
To verify constraint satisfaction in `O(1)` per step, I'll track a variable `formed` representing how many unique characters in the window currently meet their target count."

### Phase 3: Live Implementation & State Management (12–32 Mins)
- **Candidate:** "Notice how we maintain `formed`: when `windowCounts[c] == targetCounts[c]`, we increment `formed`. Crucially, when contracting from the left, we only decrement `formed` when `windowCounts[s[left]]` drops *strictly below* `targetCounts[s[left]]`. This eliminates iterating over the hash map on every step."

### Phase 4: Edge Case Verification & Amortized Proof (32–40 Mins)
- **Candidate:** "Let's verify edge cases:
  1. `t` is longer than `s`: Immediate return of `""`.
  2. No valid window contains `t`: `minLen` remains infinity, returns `""`.
  3. String `s` equals `t`: Expands to full length, contracts zero times, returns `s`.
  4. Amortized complexity: Even with the nested while loop, each index from `0` to `N-1` is processed by `right` once and `left` at most once. Total operations are bounded by `2N`, which is strictly `O(N)`."

---

## 🛠️ CHAPTER 6: COMMON PITFALLS & DECISION FRAMEWORK

### Common Pitfalls
1. **Regressing the Left Pointer:** In Longest Substring Without Repeats, doing `left = lastSeen[c] + 1` without wrapping in `Math.Max(left, ...)`. If a duplicate appeared before the current `left`, `left` would erroneously jump backwards!
2. **Re-checking the Entire Map:** Comparing two maps of size 26 at every step of a sliding window (`26 * N`), when a single `matches` counter achieves true `O(1)` per step.
3. **Off-by-One in Span Extraction:** Slicing indices incorrectly at termination. Record `bestStart` and `minLen`, and extract the substring once at the end.

### Decision Framework

```
                       [ Substring Problem ]
                                 |
              +------------------+------------------+
              |                                     |
      [ Window Size Known ]                [ Window Size Variable ]
              |                                     |
       Fixed Window of K                   Goal Condition?
      (CheckInclusion / Anagrams)                   |
                                    +---------------+---------------+
                                    |                               |
                             Find Longest                     Find Smallest
                          (No Repeats / K Dist)          (Min Window Substring)
                                    |                               |
                           Expand R until invalid,         Expand R until valid,
                           shrink L to restore validity    shrink L to minimize span
```

---

## 🏋️ PRACTICE LADDER

| Problem | LeetCode # | Difficulty | Window Type | Key State Tracker |
| :--- | :--- | :--- | :--- | :--- |
| **Longest Substring Without Repeating Characters** | #3 | 🟡 Medium | Variable | `lastSeen` 1-based index table |
| **Minimum Window Substring** | #76 | 🔴 Hard | Variable | `formed == requiredUnique` counter |
| **Permutation in String** | #567 | 🟡 Medium | Fixed (`|s1|`) | `matches == 26` delta tracker |
| **Find All Anagrams in a String** | #438 | 🟡 Medium | Fixed (`|p|`) | Sliding frequency difference array |
| **Longest Substring with At Most K Distinct Characters** | #340 | 🟡 Medium | Variable | Distinct characters counter |
| **Longest Repeating Character Replacement** | #424 | 🟡 Medium | Variable | `(R - L + 1) - maxFreq <= K` |
| **Max Consecutive Ones III** | #1004 | 🟡 Medium | Variable | Number of flipped zeros `<= K` |

---

> 🧭 **Navigation:** [← Previous Day](Week_06_Day_01_Palindrome_Patterns_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_06_Day_03_Parentheses_Bracket_Matching_Instructional.md)
