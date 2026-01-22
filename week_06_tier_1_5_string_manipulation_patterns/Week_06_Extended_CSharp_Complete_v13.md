# 🗺️ Week_06_Extended_CSharp_Problem_Solving_Implementation — COMPLETE v13

**Version:** v1.0 HYBRID (Pattern Recognition + Production Implementation)  
**Week:** 6 – Tier 1.5 String Manipulation Patterns  
**Purpose:** Master practical string patterns for palindromes, substrings, parentheses, and transformations  
**Target:** Transform Week 6 string patterns into interview-ready C# coding skills  
**Prerequisites:** Week 6 instructional files + standard support files complete

---

## 📚 WEEK 6 OVERVIEW

**Primary Goal:** Learn practical string patterns for palindromes, substrings, parentheses, and transformations. Strings are arrays of characters; this week adapts earlier patterns to text.

**Why This Week Comes Here:** Strings are arrays of characters; this week adapts earlier patterns (two-pointer, sliding window, stack) to text manipulation.

**Topics by Day:**
- **Day 1:** Palindrome Patterns (two-pointer checks, expand-around-center, partitioning)
- **Day 2:** Substring & Sliding Window on Strings (longest without repeating, character replacement, anagram substrings, minimum window)
- **Day 3:** Parentheses & Bracket Matching (valid parentheses, generate parentheses, longest valid, minimum remove)
- **Day 4:** String Transformations & Building (atoi, roman numerals, zigzag, compression, performance)
- **Day 5:** String Matching & Rolling Hash (Karp-Rabin revisited, algorithm comparisons, applications)

---

## SECTION 1️⃣: PATTERN RECOGNITION FRAMEWORK

### 🎯 Decision Tree — How to Identify & Choose Week 6 Patterns

Use this decision tree when you encounter a problem in Week 6:

| 🔍 Problem Phrases/Signals | 🎯 Pattern Name | ❓ Why This Pattern? | 💻 C# Collection | ⏱️ Time/Space |
|---|---|---|---|---|
| "Palindrome", "Read same forwards/backwards", "Two pointers", "Expand around center" | **Palindrome Patterns** | Two-pointer from ends match; expand-around-center O(n²) | `string` with indices | O(n²) / O(1) |
| "Longest substring", "No repeating characters", "At most K distinct", "Character map" | **Sliding Window on Strings** | Variable window + frequency map; shrink when invalid | `string` + `Dictionary<char,int>` | O(n) / O(1) |
| "Anagram substring", "Permutation in string", "Fixed window", "Frequency matching" | **Fixed Window Anagram** | Precompute first window; slide and compare frequencies | `string` + `Dictionary<char,int>` | O(n+m) / O(1) |
| "Valid parentheses", "Matching brackets", "Balanced", "Stack needed" | **Parentheses Matching** | Stack for open brackets; pop on close; match types | `Stack<char>` | O(n) / O(n) |
| "Longest valid parentheses", "Longest valid substring", "Stack tracking indices" | **Valid Parentheses DP/Stack** | Stack stores indices; calculate lengths on valid substrings | `Stack<int>` | O(n) / O(n) |
| "String to integer", "Atoi", "Overflow", "Sign handling" | **String Parsing** | Character-by-character scan; overflow/sign guards | `string` iteration | O(n) / O(1) |
| "Roman numerals", "Integer to string", "Mapping", "Range-based lookup" | **String Encoding** | Map values to symbols; reverse lookup; greedy | `Dictionary<int,string>` | O(log n) / O(1) |
| "Substring search", "Pattern matching", "Rolling hash", "Karp-Rabin" | **Rolling Hash Search** | Polynomial hash; O(1) window update; verify on match | `string` | O(n+m) / O(1) |

**HOW TO READ THIS:**
- See "[Signal X]" in a problem? IMMEDIATELY think "[Pattern Y]"
- Ask yourself: "Why does this pattern solve it?" → Internalize the reasoning
- Check recommended collection and complexity

---

## SECTION 2️⃣: ANTI-PATTERNS — WHEN NOT TO USE & WHAT FAILS

### ⚠️ Week 6 Common Mistakes & Correct Alternatives

Learn WHAT NOT TO DO and WHY:

| ❌ Wrong Approach | 💥 Why It Fails | ⚡ Symptom/Consequence | ✅ Correct Alternative |
|---|---|---|---|
| Palindrome by reversing entire string | Expensive string reversal O(n) per check | Slow for many checks; naive implementation | Two-pointer from ends O(1) space or expand-around-center |
| String concatenation in loop | O(n²) due to string immutability | TLE on large strings; every concat creates new string | Use `StringBuilder` for O(n) building |
| Sliding window without tracking frequency count | Wrong character match detection | False positives or negatives in substring matching | Track frequency map; update on expand/shrink |
| Parentheses match without type checking | Wrong bracket (open [, close }) | Incorrect valid/invalid detection | Use `Dictionary<char,char>` for type mapping |
| Integer overflow without checking | `OverflowException` or wrong result | Negative becomes positive or vice versa | Guard: `if (result > int.MaxValue / 10)` before mul |
| Not handling edge cases in atoi | Null, empty, non-numeric | Crash or wrong conversion | Guard: empty string, skip whitespace, validate digit |
| Using array instead of StringBuilder | O(n²) due to repeated allocation | Slow string building; TLE on large transformations | Always: `StringBuilder sb = new()` for concatenation |
| Substring search without verification on hash match | Hash collision gives false positive | Wrong answer silently | Always verify: manual substring compare on hash hit |

**ANTI-PATTERN LESSON:**
String problems often have immutability gotchas. StringBuilder and careful guards prevent TLE and crashes.

---

## SECTION 3️⃣: PATTERN IMPLEMENTATIONS (Production-Grade)

### Pattern 1: Palindrome Patterns

#### 🧠 Mental Model
Two approaches: (1) Two-pointer from ends: O(1) space, compare until middle. (2) Expand-around-center: for each possible center (n positions), expand outward. Finding longest palindrome uses second approach: O(n²) time, O(1) space.

#### ✅ When to Use This Pattern
- ✅ Check if string is palindrome (two-pointer).
- ✅ Find longest palindromic substring (expand-around-center).
- ✅ Palindrome partitioning (backtracking, DP later).
- ✅ Shortest palindrome (string preprocessing).

#### 💻 Core C# Implementation (Battle-Tested)

```csharp
/// <summary>
/// Palindrome Patterns - Two-Pointer and Expand-Around-Center
/// Time: O(n) or O(n²) | Space: O(1) or O(n)
/// 
/// 🧠 MENTAL MODEL:
/// Two-pointer: compare from both ends; O(1) space.
/// Expand-around-center: for each center, expand outward; O(n²) time, O(1) space.
/// Longest palindrome uses center expansion (beats DP/Manacher for learning).
/// </summary>
public class PalindromeSolution
{
    /// <summary>
    /// Is String a Palindrome - Two Pointers
    /// Time: O(n) | Space: O(1)
    /// </summary>
    public static bool IsPalindrome(string s)
    {
        // STEP 1: Guard
        if (string.IsNullOrEmpty(s)) return true;
        
        // STEP 2: Initialize pointers at opposite ends
        int left = 0;
        int right = s.Length - 1;
        
        // STEP 3: Compare from ends
        while (left < right)
        {
            if (s[left] != s[right])
                return false;
            
            left++;
            right--;
        }
        
        return true;
    }
    
    /// <summary>
    /// Is Palindrome (Skip Non-Alphanumeric)
    /// Time: O(n) | Space: O(1)
    /// </summary>
    public static bool IsPalindromeValid(string s)
    {
        // STEP 1: Guard
        if (string.IsNullOrEmpty(s)) return true;
        
        // STEP 2: Two pointers
        int left = 0;
        int right = s.Length - 1;
        
        while (left < right)
        {
            // Skip non-alphanumeric on left
            while (left < right && !char.IsLetterOrDigit(s[left]))
                left++;
            
            // Skip non-alphanumeric on right
            while (left < right && !char.IsLetterOrDigit(s[right]))
                right--;
            
            // Compare alphanumeric (case-insensitive)
            if (char.ToLower(s[left]) != char.ToLower(s[right]))
                return false;
            
            left++;
            right--;
        }
        
        return true;
    }
    
    /// <summary>
    /// Longest Palindromic Substring - Expand Around Center
    /// Time: O(n²) | Space: O(1) excluding output
    /// 
    /// 🧠 MENTAL MODEL:
    /// For each possible center (even and odd), expand left/right.
    /// Track longest palindrome found.
    /// O(n) centers × O(n) expand = O(n²).
    /// </summary>
    public static string LongestPalindrome(string s)
    {
        // STEP 1: Guard
        if (string.IsNullOrEmpty(s) || s.Length == 1) return s;
        
        // STEP 2: Track longest
        int start = 0;
        int maxLen = 1;
        
        // STEP 3: Expand around each center
        for (int i = 0; i < s.Length; i++)
        {
            // Odd-length palindromes (single character center)
            int len1 = ExpandAroundCenter(s, i, i);
            
            // Even-length palindromes (two character center)
            int len2 = ExpandAroundCenter(s, i, i + 1);
            
            int len = Math.Max(len1, len2);
            
            if (len > maxLen)
            {
                maxLen = len;
                start = i - (len - 1) / 2;
            }
        }
        
        return s.Substring(start, maxLen);
    }
    
    private static int ExpandAroundCenter(string s, int left, int right)
    {
        // Expand from center while valid
        while (left >= 0 && right < s.Length && s[left] == s[right])
        {
            left--;
            right++;
        }
        
        // Return length (right - left - 1 is final valid span)
        return right - left - 1;
    }
}
```

#### 🔴 C# Engineering Notes
- 🔴 **CRITICAL:** Expand-around-center handles both odd (center = single char) and even (center = between two chars) length palindromes.
- 🟡 **PERFORMANCE:** O(n²) is acceptable for palindrome problems; Manacher's O(n) is advanced.
- 🟢 **BEST PRACTICE:** Two-pointer for simple checks; expand-around-center for longest substring.

---

### Pattern 2: Sliding Window on Strings (Variable & Fixed)

#### 🧠 Mental Model
Variable window: expand right, add character to frequency map. Shrink left when constraint violated (e.g., repeating character or frequency exceeds limit). Fixed window: precompute first window, slide by remove+add. For anagrams, compare frequency maps.

#### ✅ When to Use This Pattern
- ✅ Longest substring without repeating characters.
- ✅ Character replacement within K changes.
- ✅ Anagram substring / permutation in string.
- ✅ Minimum window substring.

#### 💻 Core C# Implementation (Battle-Tested)

```csharp
/// <summary>
/// Sliding Window on Strings - Variable and Fixed Windows
/// Time: O(n) amortized | Space: O(1) for character sets
/// 
/// 🧠 MENTAL MODEL:
/// Variable: expand right, track frequency, shrink when invalid.
/// Fixed: precompute first window state, slide with O(1) updates.
/// </summary>
public class StringWindowSolution
{
    /// <summary>
    /// Longest Substring Without Repeating Characters - Variable Window
    /// Time: O(n) | Space: O(min(n, alphabet_size))
    /// </summary>
    public static int LengthOfLongestSubstring(string s)
    {
        // STEP 1: Guard
        if (string.IsNullOrEmpty(s)) return 0;
        
        // STEP 2: Character → last seen index
        Dictionary<char, int> lastSeen = new();
        int maxLen = 0;
        int left = 0;
        
        // STEP 3: Expand window with right pointer
        for (int right = 0; right < s.Length; right++)
        {
            char c = s[right];
            
            // If character seen and in current window, move left past it
            if (lastSeen.ContainsKey(c) && lastSeen[c] >= left)
            {
                left = lastSeen[c] + 1;
            }
            
            // Record latest position
            lastSeen[c] = right;
            
            // Update max length
            maxLen = Math.Max(maxLen, right - left + 1);
        }
        
        return maxLen;
    }
    
    /// <summary>
    /// Character Replacement Within K Changes
    /// Time: O(n) | Space: O(1) for 26 letters
    /// 
    /// 🧠 MENTAL MODEL:
    /// Window valid if: (window_size - max_frequency) ≤ k
    /// Meaning: k changes to make all characters same.
    /// Expand right; shrink left when invalid.
    /// </summary>
    public static int CharacterReplacement(string s, int k)
    {
        // STEP 1: Guard
        if (string.IsNullOrEmpty(s)) return 0;
        
        // STEP 2: Frequency of characters in window
        Dictionary<char, int> freq = new();
        int maxFreq = 0;
        int left = 0;
        int maxLen = 0;
        
        // STEP 3: Expand window
        for (int right = 0; right < s.Length; right++)
        {
            char c = s[right];
            
            // Add character
            if (!freq.ContainsKey(c))
                freq[c] = 0;
            freq[c]++;
            
            maxFreq = Math.Max(maxFreq, freq[c]);
            
            // STEP 4: Shrink if changes needed > k
            while (right - left + 1 - maxFreq > k)
            {
                char leftChar = s[left];
                freq[leftChar]--;
                left++;
            }
            
            maxLen = Math.Max(maxLen, right - left + 1);
        }
        
        return maxLen;
    }
    
    /// <summary>
    /// Permutation in String (Anagram Substring) - Fixed Window
    /// Time: O(n + m) where m = s1 length | Space: O(1)
    /// 
    /// 🧠 MENTAL MODEL:
    /// Fixed window of size m (s1 length).
    /// Slide through s2; check if window frequencies match s1.
    /// Optimization: track "match count" to avoid full comparison.
    /// </summary>
    public static bool CheckInclusion(string s1, string s2)
    {
        // STEP 1: Guard
        if (s1.Length > s2.Length) return false;
        if (string.IsNullOrEmpty(s1)) return true;
        
        // STEP 2: Frequency of s1
        int[] s1Freq = new int[26];
        int[] windowFreq = new int[26];
        
        for (int i = 0; i < s1.Length; i++)
        {
            s1Freq[s1[i] - 'a']++;
        }
        
        // STEP 3: Precompute first window
        for (int i = 0; i < s1.Length; i++)
        {
            windowFreq[s2[i] - 'a']++;
        }
        
        if (FrequenciesMatch(s1Freq, windowFreq))
            return true;
        
        // STEP 4: Slide window
        for (int i = s1.Length; i < s2.Length; i++)
        {
            // Add new character
            windowFreq[s2[i] - 'a']++;
            
            // Remove left character
            windowFreq[s2[i - s1.Length] - 'a']--;
            
            if (FrequenciesMatch(s1Freq, windowFreq))
                return true;
        }
        
        return false;
    }
    
    private static bool FrequenciesMatch(int[] freq1, int[] freq2)
    {
        for (int i = 0; i < 26; i++)
        {
            if (freq1[i] != freq2[i])
                return false;
        }
        return true;
    }
}
```

#### 🔴 C# Engineering Notes
- 🔴 **CRITICAL:** Character replacement: window_size - max_frequency ≤ k is the constraint, not frequency itself.
- 🟡 **PERFORMANCE:** Use array `int[26]` for frequencies (faster than Dictionary for fixed alphabet).
- 🟢 **BEST PRACTICE:** For fixed windows, precompute first state, then O(1) sliding updates.

---

### Pattern 3: Parentheses & Bracket Matching

#### 🧠 Mental Model
Stack stores open brackets. On open, push. On close, check top matches type; pop if match. If mismatch or empty on close, invalid. At end, stack must be empty.

#### ✅ When to Use This Pattern
- ✅ Valid parentheses (simple check).
- ✅ Generate parentheses (backtracking, DFS).
- ✅ Longest valid parentheses (stack indices).
- ✅ Minimum remove to make valid.

#### 💻 Core C# Implementation (Battle-Tested)

```csharp
/// <summary>
/// Parentheses & Bracket Matching - Stack-Based Solutions
/// Time: O(n) | Space: O(n)
/// 
/// 🧠 MENTAL MODEL:
/// Stack for open brackets. Match type on close.
/// Track longest valid substring (stack indices).
/// </summary>
public class ParenthesesSolution
{
    /// <summary>
    /// Valid Parentheses - Stack Type Matching
    /// Time: O(n) | Space: O(n)
    /// </summary>
    public static bool IsValid(string s)
    {
        // STEP 1: Guard
        if (string.IsNullOrEmpty(s) || s.Length % 2 != 0) return false;
        
        // STEP 2: Bracket mapping
        Dictionary<char, char> matches = new()
        {
            { ')', '(' },
            { ']', '[' },
            { '}', '{' }
        };
        
        // STEP 3: Stack for open brackets
        Stack<char> stack = new();
        
        // STEP 4: Process each character
        foreach (char c in s)
        {
            if (matches.ContainsKey(c))  // Closing bracket
            {
                // Must have matching open bracket on stack
                if (stack.Count == 0 || stack.Pop() != matches[c])
                    return false;
            }
            else  // Opening bracket
            {
                stack.Push(c);
            }
        }
        
        // Stack must be empty
        return stack.Count == 0;
    }
    
    /// <summary>
    /// Longest Valid Parentheses - Stack Indices
    /// Time: O(n) | Space: O(n)
    /// 
    /// 🧠 MENTAL MODEL:
    /// Stack stores indices of unmatched parentheses.
    /// Push base index (-1) for length calculation.
    /// On '(': push index. On ')': pop, calculate length if valid.
    /// </summary>
    public static int LongestValidParentheses(string s)
    {
        // STEP 1: Guard
        if (string.IsNullOrEmpty(s) || s.Length < 2) return 0;
        
        // STEP 2: Stack stores indices
        Stack<int> stack = new();
        stack.Push(-1);  // Base for length calculation
        int maxLen = 0;
        
        // STEP 3: Process each character
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == '(')
            {
                // Push index of open bracket
                stack.Push(i);
            }
            else  // s[i] == ')'
            {
                // Pop matching position
                stack.Pop();
                
                if (stack.Count == 0)
                {
                    // No matching open; this close is base for next valid
                    stack.Push(i);
                }
                else
                {
                    // Calculate length from last unmatched position
                    int len = i - stack.Peek();
                    maxLen = Math.Max(maxLen, len);
                }
            }
        }
        
        return maxLen;
    }
    
    /// <summary>
    /// Generate Parentheses - Backtracking (DFS)
    /// Time: O(4^n / sqrt(n)) (Catalan number) | Space: O(n) recursion depth
    /// 
    /// 🧠 MENTAL MODEL:
    /// At each position: add '(' if available, add ')' if valid (fewer closes than opens).
    /// Backtrack when complete.
    /// </summary>
    public static IList<string> GenerateParenthesis(int n)
    {
        List<string> result = new();
        Backtrack(result, "", 0, 0, n);
        return result;
    }
    
    private static void Backtrack(List<string> result, string current, int open, int close, int max)
    {
        // Base case: complete valid combination
        if (current.Length == max * 2)
        {
            result.Add(current);
            return;
        }
        
        // Add open bracket if haven't used all
        if (open < max)
        {
            Backtrack(result, current + "(", open + 1, close, max);
        }
        
        // Add close bracket if valid (fewer closes than opens)
        if (close < open)
        {
            Backtrack(result, current + ")", open, close + 1, max);
        }
    }
}
```

#### 🔴 C# Engineering Notes
- 🔴 **CRITICAL:** Longest valid: base index -1 prevents negative length. Stack pop first, then check empty.
- 🟡 **PERFORMANCE:** Backtracking generates O(Catalan(n)) solutions; inherent to problem, not avoidable.
- 🟢 **BEST PRACTICE:** Use stack for all bracket problems; avoid recursion unless necessary (backtracking).

---

### Pattern 4: String Parsing & Encoding (atoi, Roman Numerals)

#### 🧠 Mental Model
atoi: scan left-to-right, skip whitespace, handle sign, accumulate digits with overflow guard. Roman: map values to symbols; process larger values first (greedy); reverse process: iterate symbols, subtract or add.

#### ✅ When to Use This Pattern
- ✅ String to integer (atoi) with edge cases.
- ✅ Integer to string (Roman numerals).
- ✅ Character mapping and transformation.
- ✅ Zigzag encoding / compression.

#### 💻 Core C# Implementation (Battle-Tested)

```csharp
/// <summary>
/// String Parsing & Encoding - atoi, Roman Numerals, Compression
/// Time: O(n) | Space: O(1) or O(n) for result
/// 
/// 🧠 MENTAL MODEL:
/// atoi: iterate chars, accumulate digits, guard overflow.
/// Roman: map values to symbols, greedy processing.
/// </summary>
public class StringParsingSolution
{
    /// <summary>
    /// String to Integer (atoi) - Parse with Edge Cases
    /// Time: O(n) | Space: O(1)
    /// </summary>
    public static int MyAtoi(string s)
    {
        // STEP 1: Guard
        if (string.IsNullOrEmpty(s)) return 0;
        
        // STEP 2: Skip leading whitespace
        int i = 0;
        while (i < s.Length && s[i] == ' ')
            i++;
        
        // STEP 3: Handle sign
        int sign = 1;
        if (i < s.Length && (s[i] == '+' || s[i] == '-'))
        {
            if (s[i] == '-')
                sign = -1;
            i++;
        }
        
        // STEP 4: Accumulate digits
        int result = 0;
        while (i < s.Length && char.IsDigit(s[i]))
        {
            int digit = s[i] - '0';
            
            // Guard overflow BEFORE multiplying
            if (result > int.MaxValue / 10 || 
                (result == int.MaxValue / 10 && digit > 7))
            {
                return sign == 1 ? int.MaxValue : int.MinValue;
            }
            
            result = result * 10 + digit;
            i++;
        }
        
        return sign * result;
    }
    
    /// <summary>
    /// Integer to Roman Numeral - Greedy Value Mapping
    /// Time: O(1) constant iterations (max 13 symbols) | Space: O(1)
    /// </summary>
    public static string IntToRoman(int num)
    {
        // STEP 1: Guard
        if (num <= 0 || num >= 4000) return "";
        
        // STEP 2: Value-to-symbol mapping (larger first for greedy)
        int[] values = { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
        string[] symbols = { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };
        
        // STEP 3: Greedy: use largest possible symbols
        StringBuilder result = new();
        for (int i = 0; i < values.Length; i++)
        {
            while (num >= values[i])
            {
                result.Append(symbols[i]);
                num -= values[i];
            }
        }
        
        return result.ToString();
    }
    
    /// <summary>
    /// Roman Numeral to Integer - Symbol Scanning
    /// Time: O(n) | Space: O(1)
    /// </summary>
    public static int RomanToInt(string s)
    {
        // STEP 1: Guard
        if (string.IsNullOrEmpty(s)) return 0;
        
        // STEP 2: Symbol values
        Dictionary<char, int> roman = new()
        {
            { 'I', 1 }, { 'V', 5 }, { 'X', 10 }, { 'L', 50 },
            { 'C', 100 }, { 'D', 500 }, { 'M', 1000 }
        };
        
        // STEP 3: Scan right-to-left, add or subtract based on order
        int result = 0;
        for (int i = s.Length - 1; i >= 0; i--)
        {
            int value = roman[s[i]];
            
            // If current < next (to the right), subtract (e.g., IV = 4)
            if (i < s.Length - 1 && value < roman[s[i + 1]])
            {
                result -= value;
            }
            else
            {
                result += value;
            }
        }
        
        return result;
    }
    
    /// <summary>
    /// String Compression (Run-Length Encoding)
    /// Time: O(n) | Space: O(n) for result
    /// </summary>
    public static string CompressString(string s)
    {
        // STEP 1: Guard
        if (string.IsNullOrEmpty(s)) return "";
        
        // STEP 2: First pass: calculate compressed size
        int compressedSize = CountCompression(s);
        if (compressedSize >= s.Length)
            return s;  // Compression not beneficial
        
        // STEP 3: Build compressed string
        StringBuilder compressed = new();
        int count = 1;
        
        for (int i = 0; i < s.Length; i++)
        {
            // If next character different, add current + count
            if (i + 1 >= s.Length || s[i] != s[i + 1])
            {
                compressed.Append(s[i]);
                compressed.Append(count);
                count = 1;
            }
            else
            {
                count++;
            }
        }
        
        return compressed.ToString();
    }
    
    private static int CountCompression(string s)
    {
        int compressedSize = 0;
        int count = 1;
        
        for (int i = 0; i < s.Length; i++)
        {
            // If next character different, count it
            if (i + 1 >= s.Length || s[i] != s[i + 1])
            {
                compressedSize += 1 + count.ToString().Length;
                count = 1;
            }
            else
            {
                count++;
            }
        }
        
        return compressedSize;
    }
}
```

#### 🔴 C# Engineering Notes
- 🔴 **CRITICAL:** atoi: check overflow BEFORE multiplying. Use `result > int.MaxValue / 10` guard.
- 🟡 **PERFORMANCE:** Roman: greedy works because symbols are ordered by value. Compression: check size first before building.
- 🟢 **BEST PRACTICE:** Always use `StringBuilder` for string building in loops. Never concatenate in loop.

---

### Pattern 5: String Matching (Karp-Rabin Revisited)

#### 🧠 Mental Model
Polynomial rolling hash: H = c₀*b⁰ + c₁*b¹ + ... + cₙ₋₁*bⁿ⁻¹ (mod prime). Rolling update: remove leftmost, add rightmost in O(1). Verify on hash match to avoid false positives.

#### ✅ When to Use This Pattern
- ✅ Substring search (O(n+m) average).
- ✅ Multiple pattern matching in same text.
- ✅ Plagiarism detection (document hashing).
- ✅ DNA sequence matching.

#### 💻 Core C# Implementation (Battle-Tested)

```csharp
/// <summary>
/// String Matching with Rolling Hash (Karp-Rabin)
/// Time: O(n + m) average, O(n*m) worst | Space: O(1)
/// 
/// 🧠 MENTAL MODEL:
/// Polynomial rolling hash with modulo prime.
/// O(1) window update by removing old, adding new char.
/// Always verify on hash match (collision avoidance).
/// </summary>
public class RollingHashStringMatcher
{
    private const long PRIME = 101;
    private const int BASE = 256;
    
    /// <summary>
    /// Find All Occurrences of Pattern in Text - Karp-Rabin
    /// Time: O(n + m) average | Space: O(1)
    /// </summary>
    public static List<int> FindPattern(string text, string pattern)
    {
        List<int> matches = new();
        
        // STEP 1: Guard
        if (pattern.Length > text.Length || string.IsNullOrEmpty(pattern))
            return matches;
        
        // STEP 2: Precompute BASE^(m-1) mod PRIME
        long pow = 1;
        for (int i = 0; i < pattern.Length - 1; i++)
        {
            pow = (pow * BASE) % PRIME;
        }
        
        // STEP 3: Compute hash of pattern
        long patternHash = ComputeHash(pattern);
        
        // STEP 4: Compute hash of first window
        long textHash = ComputeHash(text.Substring(0, pattern.Length));
        
        // STEP 5: Roll through text
        for (int i = 0; i <= text.Length - pattern.Length; i++)
        {
            // Hash match: verify substring
            if (textHash == patternHash && VerifyMatch(text, i, pattern))
            {
                matches.Add(i);
            }
            
            // Roll to next window
            if (i < text.Length - pattern.Length)
            {
                textHash = RollHash(text, i, pattern.Length, textHash, pow);
            }
        }
        
        return matches;
    }
    
    private static long ComputeHash(string s)
    {
        long hash = 0;
        for (int i = 0; i < s.Length; i++)
        {
            hash = (hash * BASE + s[i]) % PRIME;
        }
        return hash;
    }
    
    private static long RollHash(string text, int start, int patternLen, long prevHash, long pow)
    {
        // Remove leftmost character contribution
        // Add rightmost character contribution
        long hash = (BASE * (prevHash - (text[start] * pow)) + text[start + patternLen]) % PRIME;
        
        // Ensure positive result
        if (hash < 0)
            hash = (hash + PRIME);
        
        return hash;
    }
    
    private static bool VerifyMatch(string text, int startIdx, string pattern)
    {
        // Verify actual substring match (not just hash collision)
        for (int i = 0; i < pattern.Length; i++)
        {
            if (text[startIdx + i] != pattern[i])
                return false;
        }
        return true;
    }
    
    /// <summary>
    /// Implement strStr() - Find Pattern Position
    /// Time: O(n + m) average | Space: O(1)
    /// </summary>
    public static int StrStr(string haystack, string needle)
    {
        if (string.IsNullOrEmpty(needle)) return 0;
        if (needle.Length > haystack.Length) return -1;
        
        List<int> matches = FindPattern(haystack, needle);
        return matches.Count > 0 ? matches[0] : -1;
    }
}
```

#### 🔴 C# Engineering Notes
- 🔴 **CRITICAL:** Always verify substring on hash match. Hash collisions (though rare) give false positives.
- 🟡 **PERFORMANCE:** O(n + m) average beats naive O(n*m). Use for multiple patterns; single pattern use `string.IndexOf()`.
- 🟢 **BEST PRACTICE:** Use large prime for modulo. Precompute BASE^(m-1) to avoid recalculation.

---

## SECTION 4️⃣: C# Collection Decision GUIDE

### When to Use Each Collection for Week 6 Patterns

| Use Case | Collection | Why? | When NOT to Use | Alternative |
|---|---|---|---|---|
| Palindrome two-pointer | `string` with indices | O(1) char access; no allocation | Creating reversed string (O(n) extra) | Two-pointer beats reversal |
| Sliding window frequency | `Dictionary<char,int>` or `int[26]` | O(1) updates; array faster for fixed alphabet | LinkedList (wrong structure) | Array for lowercase; Dict for Unicode |
| Parentheses matching | `Stack<char>` | LIFO for bracket pairs; O(1) push/pop | Array (use stack for clarity) | Use Stack, not List |
| atoi parsing | `string` iteration | O(1) char access; linear scan | Creating substrings (allocation) | Direct iteration beats substring |
| Roman numeral | `Dictionary<char,int>` | O(1) symbol lookup; symbol → value mapping | Array (symbols not index-based) | Dict for symbol mapping |
| StringBuilder | `StringBuilder` | O(n) concatenation vs O(n²) string concat | String concatenation in loop (TLE) | ALWAYS use StringBuilder in loops |
| Rolling hash | `string` | O(1) char access for window updates | Storing all substrings (O(n²) space) | Hash is space-efficient |

**KEY INSIGHT:**
Week 6 is string-centric. StringBuilder prevents TLE. Frequency maps enable sliding window. Stack handles brackets.

---

## SECTION 5️⃣: PROGRESSIVE PROBLEM LADDER

### 🎯 Strategy: Solve Problems in Stages

---

### 🟢 STAGE 1: CANONICAL PROBLEMS — Master Core Pattern

| # | LeetCode # | Difficulty | Pattern | C# Focus | Concept |
|---|---|---|---|---|---|
| 1 | #9 | 🟢 Easy | Palindrome Number | Two-pointer or reversal | String conversion + palindrome check |
| 2 | #125 | 🟢 Easy | Valid Palindrome | Two-pointer (skip non-alphanumeric) | Character filtering with two pointers |
| 3 | #3 | 🟢 Easy | Longest Substring Without Repeating | Variable window + char map | Sliding window with frequency |
| 4 | #20 | 🟢 Easy | Valid Parentheses | Stack type matching | Bracket validation with map |
| 5 | #5 | 🟢 Easy | Longest Palindromic Substring | Expand-around-center | Palindrome finding O(n²) |
| 6 | #8 | 🟢 Easy | String to Integer (atoi) | Parsing with guards | Overflow + sign handling |
| 7 | #12 | 🟢 Easy | Integer to Roman | Greedy value mapping | Symbol encoding with ordering |

**STAGE 1 GOAL:** Pattern fluency. Can you implement each skeleton in < 5 minutes from memory?

---

### 🟡 STAGE 2: VARIATIONS — Recognize Pattern Boundaries

| # | LeetCode # | Difficulty | Pattern + Twist | C# Focus | When Pattern Breaks |
|---|---|---|---|---|---|
| 1 | #5 | 🟡 Medium | Longest Palindromic Substring | Expand-around-center O(n²) | Manacher's O(n) advanced |
| 2 | #395 | 🟡 Medium | Longest Substring (K distinct) | Sliding window constraint | Divide-conquer for K=1 optimization |
| 3 | #32 | 🟡 Medium | Longest Valid Parentheses | Stack indices tracking | Lengths on valid substrings |
| 4 | #767 | 🟡 Medium | Reorganize String | Frequency + greedy placement | Interleaving with frequency balance |
| 5 | #438 | 🟡 Medium | Find All Anagram Substrings | Fixed window frequency | Sliding window with frequency compare |
| 6 | #22 | 🟡 Medium | Generate Parentheses | Backtracking (DFS) | Exponential but necessary |
| 7 | #28 | 🟡 Medium | Implement strStr() | Rolling hash or KMP | Karp-Rabin verification |

**STAGE 2 GOAL:** Pattern boundaries. When do variations apply?

---

### 🟠 STAGE 3: INTEGRATION — Combine Patterns

| # | LeetCode # | Difficulty | Patterns Required | C# Focus | Pattern Combination Logic |
|---|---|---|---|---|---|
| 1 | #273 | 🔴 Hard | Integer to English Words | String building + mapping | Recursion + English rules |
| 2 | #212 | 🔴 Hard | Word Search II | Trie + DFS + backtracking | Efficient pattern matching |
| 3 | #1163 | 🔴 Hard | Last Substring Lexicographically | Suffix comparison + optimization | Two-pointer on suffix comparison |

**STAGE 3 GOAL:** Real-world thinking. Professional problems combine multiple patterns.

---

## SECTION 6️⃣: WEEK 6 PITFALLS & C# GOTCHAS

### 🐛 Runtime Issues & Collection Pitfalls

| ❌ Pattern | 🐛 Bug | 💥 C# Symptom | 🔧 Quick Fix |
|---|---|---|---|
| String concat in loop | O(n²) TLE from allocation | Slow on large strings | Use `StringBuilder` always |
| Palindrome reversal | Creating new string | O(n) extra space + time | Two-pointer or expand-around |
| atoi overflow | `result > int.MaxValue / 10` not checked | Wrong result or exception | Guard BEFORE multiply: `if (result > int.MaxValue / 10)` |
| Bracket match without type check | Wrong bracket types | Accepted `[)` as valid | Use `Dictionary<char,char>` for matching |
| Frequency lookup without checking | `KeyNotFoundException` | Crash on missing char | Use `TryGetValue()` or `ContainsKey()` |
| Substring search hash collision | False positive from collision | Wrong matches reported | Always verify: `VerifyMatch()` on hit |

### 🎯 Week 6 String Gotchas

- ❌ String concatenation in loop → O(n²) TLE → Use `StringBuilder` for building strings
- ❌ Accessing string index out of bounds → `IndexOutOfRangeException` → Check `i < s.Length` before access
- ❌ Char comparison case-sensitive → Mismatches → Use `char.ToLower()` or `char.ToUpper()`
- ❌ atoi overflow not guarded → Wrong result silently → Check `result > int.MaxValue / 10` before multiply
- ❌ Substring allocation in loop → O(n²) space → Use indices, avoid `Substring()` in loops

---

## SECTION 7️⃣: QUICK REFERENCE — INTERVIEW PREPARATION

### Mental Models for Fast Recall (30 seconds before interview!)

| Pattern | 1-Liner Mental Model | Code Symbol | When You See This... |
|---|---|---|---|
| **Palindrome** | "Two-pointer from ends OR expand-around-center" | `while (left < right) if (s[left] != s[right]) return false;` | "Palindrome", "Read same both ways" |
| **Sliding Window Var** | "Expand right, shrink left on invalid constraint" | `while (invalid) left++; maxLen = Math.Max(maxLen, right - left + 1)` | "Longest substring", "At most K" |
| **Sliding Window Fixed** | "Precompute first, O(1) slide by add/remove" | `sum -= arr[left++]; sum += arr[right++]` | "K-length windows", "Fixed subarray" |
| **Bracket Matching** | "Stack for open, pop on close, type match via map" | `Stack<char>; if (closing) { if (stack.Pop() != map[c]) false; }` | "Valid parentheses", "Balanced brackets" |
| **atoi Parsing** | "Skip whitespace, handle sign, guard overflow" | `if (result > int.MaxValue / 10) return int.MaxValue` | "String to integer", "Parse with guards" |
| **Rolling Hash** | "Polynomial hash O(1) update; verify on match" | `hash = (BASE * (hash - char * pow) + newChar) % PRIME` | "Substring search", "Pattern matching" |

---

## ✅ WEEK 6 COMPLETION CHECKLIST

### Pattern Fluency — Can You Recognize & Choose?

- [ ] Recognize Palindrome signals ("read same both ways", "two-pointer", "expand-center")
- [ ] Recall palindrome patterns without notes (two-pointer, expand-around)
- [ ] Explain WHY two-pointer O(1) space beats reversal
- [ ] Explain WHEN to use each approach

- [ ] Recognize Sliding Window signals ("longest substring", "at most K", "no repeating")
- [ ] Recall sliding window patterns without notes (variable, fixed)
- [ ] Explain WHY shrinking maintains validity
- [ ] Explain WHEN frequency map needed

- [ ] Recognize Bracket signals ("valid parentheses", "matching", "balanced")
- [ ] Recall bracket stack pattern without notes (type matching, indices)
- [ ] Explain WHY stack for LIFO matching
- [ ] Explain WHEN to track indices (longest valid)

- [ ] Recognize Parsing signals ("atoi", "string to integer", "overflow", "sign")
- [ ] Recall atoi logic without notes (whitespace, sign, overflow guards)
- [ ] Explain WHY guard BEFORE multiply
- [ ] Explain WHEN each step matters

- [ ] Recognize String Match signals ("substring search", "rolling hash", "pattern", "Karp-Rabin")
- [ ] Recall rolling hash update without notes (O(1) window slide)
- [ ] Explain WHY verify on match (collision avoidance)
- [ ] Explain WHEN to use vs built-in

### Problem Solving — Can You Practice?

- [ ] Solved ALL Stage 1 canonical problems (7 problems) ✓
- [ ] Solved 80%+ Stage 2 variations (6+ of 7 problems)
- [ ] Solved 50%+ Stage 3 integration problems

### Production Code Quality — Can You Code?

- [ ] Used guard clauses (null, empty, bounds)
- [ ] Added mental model comments
- [ ] Used StringBuilder for string building
- [ ] Handled edge cases (overflow, sign, whitespace)
- [ ] Code passes code review

### Interview Ready — Can You Communicate?

- [ ] Can solve Stage 1 problem in < 5 minutes
- [ ] Can EXPLAIN your pattern choice to interviewer
- [ ] Can write PRODUCTION-GRADE code
- [ ] Can discuss tradeoffs (time/space, patterns)

---

### 🎯 Week 6 Mastery Status

- [ ] **YES - I've mastered Week 6. Ready for Week 7+.**
- [ ] **NO - Need to practice more. Focus on weak patterns.**

---

## 📚 REFERENCE MATERIALS

This file is self-contained. You have:
- **Decision framework** (SECTION 1) — Recognize which pattern
- **Anti-patterns knowledge** (SECTION 2) — What NOT to do
- **Production-grade code** (SECTION 3) — 5 complete pattern implementations
- **Collection guidance** (SECTION 4) — Which collection when
- **Progressive practice** (SECTION 5) — Easy to hard problems
- **Real gotchas** (SECTION 6) — Bugs you'll encounter
- **Quick interview reference** (SECTION 7) — 30-second recall

---

## 🚀 HOW TO USE THIS FILE

### For Learning:
1. Start with **SECTION 1** → Understand the decision tree
2. Review **SECTION 2** → Learn what NOT to do
3. Study **SECTION 3** → Understand production implementations
4. Follow **SECTION 5** → Solve problems progressively

### For Reference:
1. See a problem? → Check **SECTION 1** (decision tree)
2. Stuck? → Check **SECTION 6** (gotchas)
3. Need code? → Check **SECTION 3** (implementations)
4. Before interview? → Check **SECTION 7** (quick recall)

---

## 📊 COMPLEXITY REFERENCE

| Pattern | Time | Space | Notes |
|---------|------|-------|-------|
| Palindrome (two-pointer) | O(n) | O(1) | Simple check |
| Palindrome (expand-center) | O(n²) | O(1) | Longest substring finding |
| Sliding Window (variable) | O(n) | O(1) alphabet | Each char visited ≤2x |
| Sliding Window (fixed) | O(n) | O(1) alphabet | Precompute, O(1) slide |
| Bracket Matching | O(n) | O(n) | Stack for open brackets |
| String Parsing (atoi) | O(n) | O(1) | Character-by-character |
| Roman Numerals | O(1) constant | O(1) | Max 13 symbols |
| Rolling Hash | O(n+m) avg | O(1) | Polynomial hash modulo |

---

*End of Week 6 Extended C# Support — v13 Hybrid Format COMPLETE*

---

**Status:** ✅ WEEK 6 PRODUCTION READY & COMPREHENSIVE

This file combines:
- ✅ **Pattern selection guidance** (v11 strength) — Know WHEN/WHY to choose
- ✅ **Production-grade code** (v12 strength) — Know HOW to implement
- ✅ **Anti-patterns & gotchas** (v11 strength) — Know what to AVOID  
- ✅ **Progressive learning** (v11 strength) — Practice from easy to hard
- ✅ **Interview readiness** (v13 integration) — Pass technical interviews
- ✅ **Complete coverage** (WEEK 6 TOPICS) — All string manipulation patterns

