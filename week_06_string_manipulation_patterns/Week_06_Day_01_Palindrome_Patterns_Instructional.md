# 📘 Week 06 Day 1: Palindrome Patterns — Engineering Guide

> 🧭 **Navigation:** [← Week Overview](README.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_06_Day_02_Substring_Sliding_Window_Patterns_Instructional.md)
> 
> 💡 **Instructor Note:** *Focus on the structural symmetry invariant: a palindrome string satisfies `s[i] == s[n - 1 - i]`. Master the two foundational mechanics: two-pointer inward convergence for validation and outward expansion around 2N - 1 candidate centers for substring discovery.*

---

## 🎯 LEARNING OBJECTIVES

By the end of this chapter, you will be able to:

- **Internalize** the palindrome symmetry invariant and why two-pointer convergence delivers optimal `O(N)` time with `O(1)` auxiliary space.
- **Implement** expand-around-center for substring discovery in `O(N^2)` time without allocating quadratic memory matrices.
- **Write** zero-allocation C# (.NET 8/9) using `ReadOnlySpan<char>` alongside idiomatic Python (3.11+).
- **Deliver** a structured 45-minute technical interview script explaining trade-offs against dynamic programming and Manacher's algorithm.

---

## 📖 CHAPTER 1: CONTEXT & MOTIVATION

### The Engineering Challenge

Detecting palindromic symmetry is a fundamental string processing primitive. Consider high-throughput systems:
1. **Bioinformatics & Genomics:** Restriction enzymes recognize inverted palindromic DNA target sites (e.g., `GAATTC` / `CTTAAG`) to splice sequences.
2. **Text Processing & Code Editors:** Real-time syntax engines highlight symmetric tokens across multi-megabyte source buffers.
3. **Security & Content Moderation:** Reverse-spoofing and homoglyph attacks disguise malicious usernames or tokens using bilateral mirror symmetry.

A naive substring approach generates all `O(N^2)` substrings and tests each in `O(N)` time, resulting in an unviable `O(N^3)` algorithm. For a 10,000-character payload, that requires `10^12` operations. Understanding the underlying symmetry invariant collapses this search space.

> [!NOTE]
> **Interview & Systems Context:** In high-volume production gateways and competitive interviews, testing palindromes should never allocate auxiliary strings via reversals (e.g., avoid `s == s[::-1]` in Python or allocating new strings in C#). Allocation incurs heap fragmentation and GC pressure. Always prefer zero-allocation two-pointer traversal (`O(1)` auxiliary space).

---

## 🧠 CHAPTER 2: BUILDING THE MENTAL MODEL

### The Core Symmetry Invariant

A string `s` of length `N` is a palindrome if and only if:
```
s[i] == s[N - 1 - i]   for all 0 <= i < N / 2
```

Because the left half determines the right half, every palindrome has a geometric center of symmetry. However, mirror centers differ depending on parity:
- **Odd Length (e.g., "racecar"):** Center lies on a concrete character (index 3, `'e'`). Number of single-character centers = `N`.
- **Even Length (e.g., "abba"):** Center lies in the boundary between two adjacent characters (`s[1]` and `s[2]`). Number of boundary centers = `N - 1`.

Total candidate centers in a string of length `N`: `N + (N - 1) = 2N - 1`.

```
Odd-Length Palindrome ("racecar", length 7):
  Indices:    0     1     2     3     4     5     6
  Chars:    [ r ] [ a ] [ c ] [ e ] [ c ] [ a ] [ r ]
              |     |     |     ^     |     |     |
              |     |     +-----+-----+     |     |  (Radius 1: "cec")
              |     +-----------+-----------+     |  (Radius 2: "aceca")
              +-----------------+-----------------+  (Radius 3: "racecar")
                             Center (i = 3)

Even-Length Palindrome ("abba", length 4):
  Indices:    0     1         2     3
  Chars:    [ a ] [ b ]  |  [ b ] [ a ]
              |     |    |    |     |
              |     +----+----+     |  (Radius 1: "bb", center between 1 and 2)
              +----------+----------+  (Radius 2: "abba")
                    Virtual Mirror
```

### Inward Convergence vs. Outward Expansion

```
1. Inward Convergence (Validation):
   Left --------------------->   <--------------------- Right
   [ 0 ]   [ 1 ]   [ 2 ] ... [ N-3 ]   [ N-2 ]   [ N-1 ]
   Rule: Advance inwards while s[L] == s[R]. Early termination on first mismatch.

2. Outward Expansion (Substring Discovery):
                     <-- L       R -->
   [ 0 ] ... [ i-1 ]    [ i ]   [ i+1 ] ... [ N-1 ]
   Rule: Start at candidate center. Expand outward while s[L] == s[R] and bounds hold.
```

### Taxonomy of Palindrome Variations

| Variation | Invariant Tested | Core Mechanism | Time Complexity | Auxiliary Space |
| :--- | :--- | :--- | :--- | :--- |
| **Valid Palindrome** | Entire string symmetry | Two-pointer inward sweep | `O(N)` | `O(1)` |
| **Longest Palindromic Substring** | Max contiguous symmetry | Expand around `2N - 1` centers | `O(N^2)` | `O(1)` |
| **Palindromic Substrings Count** | Total valid substrings | Expand around `2N - 1` centers | `O(N^2)` | `O(1)` |
| **Palindrome Partitioning** | All components symmetric | Backtracking + Center/DP lookup | `O(N * 2^N)` | `O(N)` recursion depth |
| **Longest Palindromic Subsequence** | Non-contiguous symmetry | 2D Dynamic Programming | `O(N^2)` | `O(N^2)` or `O(N)` |

---

## ⚙️ CHAPTER 3: MECHANICS & IMPLEMENTATION

### Operation 1: Two-Pointer Palindrome Validation

To validate alphanumeric palindromes (LeetCode 125), skip non-alphanumeric characters without allocating filtered intermediate strings.

```
String: "A man, a plan, a canal: Panama"

Step 1: L=0 ('A'), R=29 ('a') -> Match (case-insensitive)
Step 2: L=1 (' '), R=29 ('a') -> Skip L whitespace -> L=2 ('m')
        R=28 ('m')             -> Match ('m' == 'm')
...
Step K: Pointers cross (L >= R) -> Verified True in O(1) Auxiliary Space
```

### Operation 2: Expand-Around-Center for Longest Substring

For each index `i` from `0` to `N - 1`:
1. Expand around odd center: `left = i, right = i`
2. Expand around even center: `left = i, right = i + 1`
3. Update global maximum window `[bestStart, bestLen]`

```
Trace on s = "babad":

Center 0 (odd):  L=0, R=0 ('b')         -> Length 1 ("b")
Center 0 (even): L=0, R=1 ('b','a')     -> Mismatch
Center 1 (odd):  L=1, R=1 ('a')
                 Expand: L=0, R=2 ('b','b') -> Match! Length 3 ("bab")
                 Expand: L=-1 (out of bounds) -> Stop
Center 1 (even): L=1, R=2 ('a','b')     -> Mismatch
Center 2 (odd):  L=2, R=2 ('b')
                 Expand: L=1, R=3 ('a','a') -> Match! Length 3 ("aba")
                 Expand: L=0, R=4 ('b','d') -> Mismatch
...
Global Maxima: "bab" or "aba" (length 3).
```

---

### 💻 Production-Grade Implementations

#### C# (.NET 8/9 — Zero Allocation with `ReadOnlySpan<char>`)

```csharp
using System;

public static class PalindromeSolutions
{
    /// <summary>
    /// Validates if a string is a palindrome ignoring non-alphanumeric chars.
    /// Time Complexity: O(N) | Auxiliary Space: O(1) (Zero GC Allocations)
    /// </summary>
    public static bool IsValidPalindrome(ReadOnlySpan<char> s)
    {
        int left = 0;
        int right = s.Length - 1;

        while (left < right)
        {
            while (left < right && !char.IsLetterOrDigit(s[left]))
            {
                left++;
            }
            while (left < right && !char.IsLetterOrDigit(s[right]))
            {
                right--;
            }

            if (char.ToLowerInvariant(s[left]) != char.ToLowerInvariant(s[right]))
            {
                return false;
            }

            left++;
            right--;
        }

        return true;
    }

    /// <summary>
    /// Finds the longest palindromic substring using center expansion.
    /// Time Complexity: O(N^2) | Auxiliary Space: O(1) | Output Space: O(K)
    /// </summary>
    public static string LongestPalindrome(string s)
    {
        if (string.IsNullOrEmpty(s) || s.Length <= 1)
        {
            return s ?? string.Empty;
        }

        ReadOnlySpan<char> span = s.AsSpan();
        int bestStart = 0;
        int maxLength = 1;

        for (int i = 0; i < span.Length; i++)
        {
            // Odd length expansion (center at i)
            Expand(span, i, i, ref bestStart, ref maxLength);

            // Even length expansion (center between i and i + 1)
            Expand(span, i, i + 1, ref bestStart, ref maxLength);
        }

        return s.Substring(bestStart, maxLength);
    }

    private static void Expand(ReadOnlySpan<char> s, int left, int right, ref int bestStart, ref int maxLength)
    {
        while (left >= 0 && right < s.Length && s[left] == s[right])
        {
            left--;
            right++;
        }

        // Substring length is (right - 1) - (left + 1) + 1 = right - left - 1
        int currentLength = right - left - 1;
        if (currentLength > maxLength)
        {
            maxLength = currentLength;
            bestStart = left + 1;
        }
    }
}
```

#### Python (3.11+ — Idiomatic & In-Place)

```python
class PalindromeSolutions:
    @staticmethod
    def is_valid_palindrome(s: str) -> bool:
        """
        Validates if s is a palindrome considering only alphanumeric characters.
        Time: O(N) | Auxiliary Space: O(1)
        """
        left = 0
        right = len(s) - 1

        while left < right:
            while left < right and not s[left].isalnum():
                left += 1
            while left < right and not s[right].isalnum():
                right -= 1

            if s[left].lower() != s[right].lower():
                return False

            left += 1
            right -= 1

        return True

    @staticmethod
    def longest_palindrome(s: str) -> str:
        """
        Discovers the longest palindromic substring via expand-around-center.
        Time: O(N^2) | Auxiliary Space: O(1) | Output Space: O(K)
        """
        if not s or len(s) <= 1:
            return s

        start = 0
        max_len = 1

        def expand(left: int, right: int) -> tuple[int, int]:
            while left >= 0 and right < len(s) and s[left] == s[right]:
                left -= 1
                right += 1
            # Current valid palindrome starts at left + 1 with length right - left - 1
            return left + 1, right - left - 1

        for i in range(len(s)):
            # Odd length center
            o_start, o_len = expand(i, i)
            if o_len > max_len:
                start, max_len = o_start, o_len

            # Even length center
            e_start, e_len = expand(i, i + 1)
            if e_len > max_len:
                start, max_len = e_start, e_len

        return s[start : start + max_len]
```

---

## ⚖️ CHAPTER 4: COMPLEXITY DECONSTRUCTION

### Explicit Complexity Breakdown

| Algorithm / Operation | Time (Best Case) | Time (Worst Case) | Auxiliary Space | Output Space | Algorithmic Bottleneck |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Two-Pointer Validation** | `O(1)` (early mismatch) | `O(N)` (valid palindrome) | `O(1)` | `O(1)` | Cache-line sequential scan |
| **Expand-Around-Center** | `O(N)` (no repeated chars) | `O(N^2)` (e.g., "aaaaa") | `O(1)` | `O(K)` | Quadratic expansions around identical characters |
| **Dynamic Programming Matrix** | `O(N^2)` | `O(N^2)` | `O(N^2)` | `O(K)` | `N x N` boolean matrix allocation overhead |
| **Manacher's Algorithm** | `O(N)` | `O(N)` | `O(N)` | `O(K)` | Preprocessed string with sentinel boundaries |

> [!TIP]
> **Why Expand-Around-Center Beats DP in Practice:**
> While both Expand-Around-Center and 2D DP share `O(N^2)` worst-case time, DP *always* performs `N(N+1)/2` operations and allocates `O(N^2)` heap memory. Expand-Around-Center terminates early whenever `s[left] != s[right]`, typically running in near-linear time on natural language strings, and requires zero auxiliary memory (`O(1)` space).

---

## 🎙️ CHAPTER 5: 45-MINUTE INTERVIEW VERBAL SCRIPT

### Phase 1: Clarification & Constraint Scoping (0–5 Mins)
- **Candidate:** "Before jumping into code, I'd like to confirm the character set and constraints. Does the string contain Unicode, or is it standard ASCII? Should the matching be case-insensitive, and should non-alphanumeric characters (spaces, punctuation) be ignored?"
- **Interviewer:** "Standard ASCII, case-insensitive, ignore non-alphanumeric."
- **Candidate:** "Understood. Also, what is the maximum length of `s`? If `N <= 10^3`, an `O(N^2)` center expansion is optimal; if `N >= 10^5`, we would discuss Manacher's algorithm."

### Phase 2: High-Level Approach & Trade-Offs (5–12 Mins)
- **Candidate:** "For string validation, a naive approach reverses the string and compares. That costs `O(N)` auxiliary space and allocates a new string. Instead, I will use a two-pointer inward sweep from index `0` and `N-1`. By skipping invalid characters and comparing lowercased characters in-place, we achieve `O(N)` time with strictly `O(1)` auxiliary space."
- **Candidate (for Longest Substring):** "For finding the longest palindromic substring, checking every substring is `O(N^3)`. A 2D DP table achieves `O(N^2)` time but costs `O(N^2)` memory. Instead, I'll leverage the symmetry invariant: every palindrome expands from a center. There are `2N - 1` possible centers (odd and even). Expanding outward from each center gives `O(N^2)` worst-case time and `O(1)` auxiliary space."

### Phase 3: Live Coding Walkthrough (12–32 Mins)
- **Candidate:** "I'll implement the center expansion. Notice how I calculate the valid span length: when the while loop breaks, `left` and `right` have each moved one step too far. The valid start index is `left + 1`, and the valid length is `(right - 1) - (left + 1) + 1 = right - left - 1`. Let's handle both odd and even centers cleanly."

### Phase 4: Edge Cases & Verification (32–40 Mins)
- **Candidate:** "Let's dry-run edge cases:
  1. Empty string `""` or single character `"a"`: Returns immediately without looping.
  2. String with no palindromes larger than 1 (e.g., `"abcdef"`): Correctly returns any single character of length 1.
  3. String with all identical characters (e.g., `"aaaa"`): Expands fully to length 4.
  4. Even palindrome at boundaries (e.g., `"cbbd"`): Expands center between index 1 and 2, captures `"bb"`."

---

## 🛠️ CHAPTER 6: COMMON PITFALLS & DECISION FRAMEWORK

### Common Pitfalls
1. **Off-by-One in Span Length:** Using `right - left + 1` instead of `right - left - 1` after the expansion loop terminates.
2. **Ignoring Even Centers:** Only checking single-character centers `expand(i, i)` and missing even palindromes like `"abba"`.
3. **Unnecessary Allocations:** Doing `s.Substring()` inside the inner loop instead of tracking `bestStart` and `maxLength` integers.

### Decision Framework

```
                          [ Palindrome Problem ]
                                     |
               +---------------------+---------------------+
               |                                           |
       [ Validate String ]                        [ Substring Discovery ]
               |                                           |
       Two-Pointer Inward                          Input Length N?
       Convergence (O(N), O(1))                            |
                                           +---------------+---------------+
                                           |                               |
                                       N <= 2,000                      N > 10,000
                                           |                               |
                                  Expand-Around-Center            Manacher's Algorithm
                                      (O(N^2), O(1))                  (O(N), O(N))
```

---

## 🏋️ PRACTICE LADDER

| Problem | LeetCode # | Difficulty | Key Pattern | Primary Invariant |
| :--- | :--- | :--- | :--- | :--- |
| **Valid Palindrome** | #125 | 🟢 Easy | Two-Pointer Inward | `s[L] == s[R]` skipping non-alphanumeric |
| **Palindrome Number** | #9 | 🟢 Easy | Math / Two-Pointer | Reversing half the integer digits |
| **Longest Palindromic Substring** | #5 | 🟡 Medium | Expand-Around-Center | `2N - 1` odd/even center sweeps |
| **Palindromic Substrings** | #647 | 🟡 Medium | Expand-Around-Center | Accumulate expansion matches |
| **Valid Palindrome II** | #680 | 🟢 Easy | Two-Pointer with Branch | At most one deletion tolerance |
| **Shortest Palindrome** | #214 | 🔴 Hard | Prefix Matching / KMP | Longest palindromic prefix |
| **Palindrome Partitioning** | #131 | 🟡 Medium | Backtracking + Palindrome Check | Prefix palindrome cut recursion |
| **Longest Palindromic Subsequence**| #516 | 🟡 Medium | 2D Dynamic Programming | `dp[i][j] = dp[i+1][j-1] + 2` |

---

> 🧭 **Navigation:** [← Week Overview](README.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_06_Day_02_Substring_Sliding_Window_Patterns_Instructional.md)
