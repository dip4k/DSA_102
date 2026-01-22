# 🔧 Week 06 Problem-Solving Roadmap Extended C#: Production-Grade Implementations

**Status:** C# Engineering Guide  
**Version:** v12 Production-Grade Code Standards  
**Audience:** C# developers mastering string patterns with professional implementations  
**Focus:** Logic-first narrative code; production patterns; common pitfalls  

---

## 🎯 Week 06 C# Mental Models

### Pattern 1: Expand-Around-Center (Palindromes)

**C# Mental Model:** "Two pointers diverging from center while maintaining character symmetry. Expand in a helper method to handle both odd/even cases elegantly."

**When to Use:**
- Find longest palindromic substring in O(n²) worst-case
- Check if string is palindrome in O(n) time, O(1) space
- DNA pattern detection (looking for symmetric sequences)

**Core C# Skeleton:**

```csharp
public class PalindromeDetector
{
    // Mental Model: Expand from each center (odd and even positions)
    // to find palindromes. Track the longest one found.
    
    public string FindLongestPalindrome(string s)
    {
        if (string.IsNullOrEmpty(s) || s.Length == 1)
            return s;
        
        int maxStart = 0, maxLength = 1;
        
        // Try each position as a center (2n-1 total centers)
        for (int center = 0; center < 2 * s.Length - 1; center++)
        {
            // Expand around center (handles both odd/even automatically)
            int length = ExpandAroundCenter(s, center);
            
            if (length > maxLength)
            {
                maxLength = length;
                maxStart = center / 2 - (length - 1) / 2;
            }
        }
        
        return s.Substring(maxStart, maxLength);
    }
    
    private int ExpandAroundCenter(string s, int center)
    {
        // Helper method: expand outward while chars match
        // center = 0,1 → checks between s[0] and s[1] (even-length)
        // center = 2 → checks around s[1] (odd-length)
        
        int left = center / 2;
        int right = left + center % 2;
        
        while (left >= 0 && right < s.Length && s[left] == s[right])
        {
            left--;
            right++;
        }
        
        // Return length of palindrome found
        return right - left - 1;
    }
}
```

**C# Notes:**
- Use `string.IsNullOrEmpty()` for guard clauses upfront
- Helper methods avoid code duplication and improve readability
- Integer math `center / 2` and `center % 2` elegantly handle odd/even distinction
- `Substring(start, length)` is cleaner than substring operations

---

### Pattern 2: Sliding Window (Substrings)

**C# Mental Model:** "Two-pointer window that expands to include more characters, shrinks when constraints violated. Use a Dictionary to track character frequencies in O(1) updates."

**When to Use:**
- Find longest substring without repeating characters
- Find minimum window containing all characters of pattern
- Find longest substring with at most K distinct characters

**Core C# Skeleton:**

```csharp
public class SlidingWindowFinder
{
    // Mental Model: Window expands while valid, shrinks when constraint violated.
    // Dictionary tracks character frequency efficiently.
    
    public string FindLongestSubstringWithoutRepeating(string s)
    {
        if (string.IsNullOrEmpty(s))
            return string.Empty;
        
        // Guard Clause: Edge cases
        if (s.Length == 1)
            return s;
        
        var charIndex = new Dictionary<char, int>(); // char → most recent index
        int maxStart = 0, maxLength = 0;
        int left = 0;
        
        // Expand window: right pointer moves through string
        for (int right = 0; right < s.Length; right++)
        {
            char currentChar = s[right];
            
            // Shrink: if char already in window, move left past previous occurrence
            if (charIndex.ContainsKey(currentChar) && charIndex[currentChar] >= left)
            {
                left = charIndex[currentChar] + 1;
            }
            
            // Update: latest position of current character
            charIndex[currentChar] = right;
            
            // Track best window
            int windowLength = right - left + 1;
            if (windowLength > maxLength)
            {
                maxLength = windowLength;
                maxStart = left;
            }
        }
        
        return s.Substring(maxStart, maxLength);
    }
    
    public string FindMinimumWindowSubstring(string s, string t)
    {
        // Mental Model: Two-pointer window that shrinks when all chars found.
        
        // Guard Clauses
        if (string.IsNullOrEmpty(s) || string.IsNullOrEmpty(t) || s.Length < t.Length)
            return string.Empty;
        
        var required = new Dictionary<char, int>();
        foreach (char c in t)
        {
            if (required.ContainsKey(c))
                required[c]++;
            else
                required[c] = 1;
        }
        
        int formed = 0; // Count of unique chars in window with desired frequency
        var windowCounts = new Dictionary<char, int>();
        int minStart = 0, minLength = int.MaxValue;
        int left = 0;
        
        for (int right = 0; right < s.Length; right++)
        {
            char charRight = s[right];
            
            // Add character to window
            if (windowCounts.ContainsKey(charRight))
                windowCounts[charRight]++;
            else
                windowCounts[charRight] = 1;
            
            // If frequency matches requirement, increment formed count
            if (required.ContainsKey(charRight) && 
                windowCounts[charRight] == required[charRight])
            {
                formed++;
            }
            
            // Shrink window while valid
            while (formed == required.Count && left <= right)
            {
                char charLeft = s[left];
                
                // Update minimum window
                if (right - left + 1 < minLength)
                {
                    minLength = right - left + 1;
                    minStart = left;
                }
                
                // Remove left character
                windowCounts[charLeft]--;
                if (required.ContainsKey(charLeft) && 
                    windowCounts[charLeft] < required[charLeft])
                {
                    formed--;
                }
                
                left++;
            }
        }
        
        return minLength == int.MaxValue ? string.Empty : s.Substring(minStart, minLength);
    }
}
```

**C# Notes:**
- Use `Dictionary<char, int>` for O(1) frequency lookups (vs array for all 256 ASCII values)
- Guard clauses handle null/empty/length-1 immediately
- `ContainsKey()` before access prevents KeyNotFoundException
- Two separate loops (expand/shrink) with clear intent is cleaner than nested logic

---

### Pattern 3: Stack-Based Matching (Brackets)

**C# Mental Model:** "Stack holds unmatched opening brackets. For each closing bracket, verify it matches the top; pop if valid, else invalid. LIFO discipline enforces nesting order."

**When to Use:**
- Validate that all brackets are properly matched and nested
- Find longest valid parentheses substring
- Generate all valid bracket combinations

**Core C# Skeleton:**

```csharp
public class BracketMatcher
{
    // Mental Model: Stack maintains unmatched opening brackets.
    // LIFO matching ensures nesting order.
    
    public bool IsValidBrackets(string s)
    {
        // Guard Clause: Odd-length strings can't be valid
        if (string.IsNullOrEmpty(s) || s.Length % 2 != 0)
            return false;
        
        var stack = new Stack<char>();
        var matching = new Dictionary<char, char>
        {
            { ')', '(' },
            { ']', '[' },
            { '}', '{' }
        };
        
        foreach (char c in s)
        {
            if (matching.ContainsKey(c))
            {
                // Closing bracket: check stack
                // Guard: Stack must not be empty; top must match
                if (stack.Count == 0 || stack.Peek() != matching[c])
                    return false;
                
                stack.Pop();
            }
            else if (matching.Values.Contains(c))
            {
                // Opening bracket: push
                stack.Push(c);
            }
            else
            {
                // Invalid character
                return false;
            }
        }
        
        // Valid only if all brackets matched (stack empty)
        return stack.Count == 0;
    }
    
    public int FindLongestValidParentheses(string s)
    {
        // Mental Model: Use stack indices to track boundaries of valid segments.
        // Stack stores indices of unmatched brackets.
        
        if (string.IsNullOrEmpty(s) || s.Length < 2)
            return 0;
        
        var stack = new Stack<int>();
        stack.Push(-1); // Base for calculation
        int maxLength = 0;
        
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == '(')
            {
                stack.Push(i);
            }
            else // s[i] == ')'
            {
                stack.Pop();
                
                if (stack.Count == 0)
                {
                    // Current ')' is unmatched; use as new base
                    stack.Push(i);
                }
                else
                {
                    // Valid parentheses: calculate length
                    maxLength = Math.Max(maxLength, i - stack.Peek());
                }
            }
        }
        
        return maxLength;
    }
    
    public List<string> GenerateValidParentheses(int n)
    {
        // Mental Model: Backtracking with constraints:
        // - Open count ≤ n (can't exceed n opens)
        // - Close count ≤ open count (can't close without open)
        
        var result = new List<string>();
        Backtrack(result, new StringBuilder(), 0, 0, n);
        return result;
    }
    
    private void Backtrack(List<string> result, StringBuilder current, int open, int close, int max)
    {
        // Base Case: generated max opens and closes
        if (open == max && close == max)
        {
            result.Add(current.ToString());
            return;
        }
        
        // Add opening bracket if haven't used max yet
        if (open < max)
        {
            current.Append('(');
            Backtrack(result, current, open + 1, close, max);
            current.Length--; // Backtrack: remove last char
        }
        
        // Add closing bracket if unmatched opens exist
        if (close < open)
        {
            current.Append(')');
            Backtrack(result, current, open, close + 1, max);
            current.Length--; // Backtrack
        }
    }
}
```

**C# Notes:**
- `Stack<int>` for indices (indices track boundaries, not just characters)
- `Dictionary<char, char>` maps closing → opening for type matching
- `StringBuilder` for backtracking (more efficient than string concatenation)
- Guard clauses check `stack.Count == 0` before peeking/popping
- `Math.Max()` vs if/else for cleaner code

---

### Pattern 4: StringBuilder Building (Transformations)

**C# Mental Model:** "Accumulate characters in mutable StringBuilder buffer. Convert to immutable string only at end. Avoids O(n²) concatenation trap."

**When to Use:**
- Any string concatenation in a loop
- Parsing user input with defensive checks
- Format conversion (integer to Roman, compression)

**Core C# Skeleton:**

```csharp
public class StringTransformer
{
    // Mental Model: Build in mutable StringBuilder; convert to immutable at end.
    // This avoids O(n²) naive concatenation.
    
    public int StringToInteger(string s)
    {
        // Guard Clauses
        if (string.IsNullOrEmpty(s))
            return 0;
        
        int i = 0;
        int sign = 1;
        long result = 0;
        
        const int INT_MAX = int.MaxValue;     // 2^31 - 1
        const int INT_MIN = int.MinValue;     // -2^31
        const int INT_MAX_LAST_DIGIT = 7;     // last digit of 2^31 - 1
        
        // Skip whitespace
        while (i < s.Length && s[i] == ' ')
            i++;
        
        // Check sign
        if (i < s.Length && (s[i] == '+' || s[i] == '-'))
        {
            sign = s[i] == '-' ? -1 : 1;
            i++;
        }
        
        // Parse digits with overflow check
        while (i < s.Length && char.IsDigit(s[i]))
        {
            int digit = s[i] - '0';
            
            // Check overflow BEFORE performing operation
            // Condition: result > INT_MAX / 10 OR 
            //           (result == INT_MAX / 10 AND digit > INT_MAX_LAST_DIGIT)
            if (result > INT_MAX / 10 || 
                (result == INT_MAX / 10 && digit > INT_MAX_LAST_DIGIT))
            {
                return sign == 1 ? INT_MAX : INT_MIN;
            }
            
            result = result * 10 + digit;
            i++;
        }
        
        return (int)(sign * result);
    }
    
    public string IntegerToRoman(int num)
    {
        // Mental Model: Greedy mapping from largest to smallest.
        // Order must be descending, including subtractive pairs.
        
        // Guard Clause
        if (num <= 0 || num >= 4000)
            return string.Empty;
        
        var mapping = new (int value, string symbol)[]
        {
            (1000, "M"),
            (900, "CM"),
            (500, "D"),
            (400, "CD"),
            (100, "C"),
            (90, "XC"),
            (50, "L"),
            (40, "XL"),
            (10, "X"),
            (9, "IX"),
            (5, "V"),
            (4, "IV"),
            (1, "I")
        };
        
        var result = new StringBuilder();
        
        foreach (var (value, symbol) in mapping)
        {
            // Append symbol as many times as possible
            while (num >= value)
            {
                result.Append(symbol);
                num -= value;
            }
        }
        
        return result.ToString();
    }
    
    public string CompressString(string s)
    {
        // Mental Model: Run-length encoding with early termination
        // if compressed version is longer than original.
        
        // Guard Clause
        if (string.IsNullOrEmpty(s) || s.Length <= 2)
            return s;
        
        // Calculate compressed length first
        int compressedLength = CountCompressed(s);
        if (compressedLength >= s.Length)
            return s; // Compression doesn't help
        
        var compressed = new StringBuilder(compressedLength);
        int count = 1;
        
        for (int i = 0; i < s.Length; i++)
        {
            // Check if next character is different
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
    
    private int CountCompressed(string s)
    {
        // Helper: Calculate compressed length without building string
        // Optimization: avoid building if compression doesn't help
        
        int count = 1;
        int compressedLength = 0;
        
        for (int i = 0; i < s.Length; i++)
        {
            if (i + 1 >= s.Length || s[i] != s[i + 1])
            {
                compressedLength += 1 + count.ToString().Length;
                count = 1;
            }
            else
            {
                count++;
            }
        }
        
        return compressedLength;
    }
}
```

**C# Notes:**
- `StringBuilder` is **mandatory** for any string building loop
- Overflow checks happen **before** operations: `if (result > MAX / 10)`
- Use tuples `(int, string)` for cleaner key-value pairs vs Dictionary when order matters
- Helper methods (`CountCompressed`) optimize logic and improve readability
- Early returns (`if (compressedLength >= s.Length) return s`) avoid unnecessary work

---

### Pattern 5: Rolling Hash (Pattern Matching — Optional Advanced)

**C# Mental Model:** "Compute polynomial hash of pattern once. For each text window, update hash in O(1): remove old coefficient, multiply by base, add new. Verify on match to handle collisions."

**When to Use:**
- Multiple patterns in same text (hash each pattern once)
- Plagiarism detection (find repeated substrings)
- DNA sequence matching (find disease sequences in genome)

**Core C# Skeleton:**

```csharp
public class RollingHashMatcher
{
    // Mental Model: Polynomial evaluation with O(1) rolling updates.
    // Remove-Shift-Add formula: (hash - left*basePower) * base + right
    
    private const long MOD = 1_000_000_007; // Large prime for modulo
    private const int BASE = 31;            // Prime base for polynomial
    
    public List<int> FindOccurrences(string text, string pattern)
    {
        // Guard Clauses
        var result = new List<int>();
        if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(pattern) || 
            pattern.Length > text.Length)
            return result;
        
        int patternLen = pattern.Length;
        
        // Precompute: basePower = BASE^(patternLen-1) mod MOD
        long basePower = ComputeBasePower(patternLen - 1);
        
        // Precompute: hash of pattern
        long patternHash = ComputeHash(pattern, 0, patternLen);
        
        // Precompute: hash of first window of text
        long textHash = ComputeHash(text, 0, patternLen);
        
        // Compare first window
        if (textHash == patternHash && VerifyMatch(text, 0, pattern))
            result.Add(0);
        
        // Roll through remaining windows
        for (int i = patternLen; i < text.Length; i++)
        {
            // Remove leftmost character contribution: hash - left*basePower
            // Note: Add MOD before mod to handle negative results
            textHash = (textHash - (text[i - patternLen] - 'A') * basePower) % MOD;
            textHash = (textHash + MOD) % MOD;
            
            // Shift (multiply by base)
            textHash = (textHash * BASE) % MOD;
            
            // Add rightmost character
            textHash = (textHash + (text[i] - 'A')) % MOD;
            
            // Check for match
            if (textHash == patternHash && VerifyMatch(text, i - patternLen + 1, pattern))
                result.Add(i - patternLen + 1);
        }
        
        return result;
    }
    
    private long ComputeHash(string s, int start, int length)
    {
        // Compute hash for substring s[start..start+length)
        long hash = 0;
        
        for (int i = 0; i < length; i++)
        {
            // hash = hash * BASE + charValue
            // Apply modulo after each operation to prevent overflow
            hash = (hash * BASE + (s[start + i] - 'A')) % MOD;
        }
        
        return hash;
    }
    
    private long ComputeBasePower(int power)
    {
        // Compute BASE^power mod MOD using modular exponentiation
        long result = 1;
        long base_ = BASE;
        
        while (power > 0)
        {
            if ((power & 1) == 1)
                result = (result * base_) % MOD;
            
            base_ = (base_ * base_) % MOD;
            power >>= 1;
        }
        
        return result;
    }
    
    private bool VerifyMatch(string text, int start, string pattern)
    {
        // Verify actual substring match (handle hash collisions)
        for (int i = 0; i < pattern.Length; i++)
        {
            if (text[start + i] != pattern[i])
                return false;
        }
        return true;
    }
}
```

**C# Notes:**
- **CRITICAL:** Apply `% MOD` after **every** arithmetic operation (prevents overflow)
- For subtraction: use `(value - x + MOD) % MOD` to handle negatives correctly
- `ComputeBasePower()` uses binary exponentiation (avoid overflow from direct pow)
- Always verify on hash match (collision handling)
- Use `const long MOD = 1_000_000_007;` (large prime for better distribution)

---

## 📊 Week 06 C# Collections Guide

### When to Use Each Collection

| Collection | Lookup | Insert | Delete | Use Case |
| :--- | :--- | :--- | :--- | :--- |
| `Dictionary<K,V>` | O(1) avg | O(1) avg | O(1) avg | Character frequency, index tracking |
| `HashSet<T>` | O(1) avg | O(1) avg | O(1) avg | Unique elements, membership test |
| `Stack<T>` | O(1) peek | O(1) push | O(1) pop | Bracket matching, DFS, undo/redo |
| `Queue<T>` | O(1) peek | O(1) enqueue | O(1) dequeue | BFS, level-order, streaming |
| `List<T>` | O(n) | O(1) amortized | O(n) | Dynamic array, results collection |
| `StringBuilder` | — | O(1) amortized | — | String building (mandatory in loops) |

**Week 06 Specific:**
- **Palindromes:** `int` counters, `string.Substring()`
- **Sliding Window:** `Dictionary<char, int>` for frequency
- **Brackets:** `Stack<char>` for validation, `Stack<int>` for indices
- **Transformations:** `StringBuilder` for building
- **Rolling Hash:** `long` for hash values (prevents overflow vs `int`)

---

## ⚠️ Week 06 C# Pitfalls & Quick Fixes

### Pitfall 1: StringBuffer Concatenation Loop

**WRONG:**
```csharp
string result = "";
for (int i = 0; i < 1000; i++)
{
    result = result + "x";  // Creates 1000 new strings! O(n²)
}
```

**CORRECT:**
```csharp
var result = new StringBuilder();
for (int i = 0; i < 1000; i++)
{
    result.Append("x");  // O(n) amortized
}
return result.ToString();
```

**Why:** String immutability means each `+` allocates new object, copies data, discards old. Do this 1000 times = disaster.

---

### Pitfall 2: Stack Pop Without Empty Check

**WRONG:**
```csharp
stack.Pop();  // Throws InvalidOperationException if empty!
```

**CORRECT:**
```csharp
if (stack.Count == 0)
    return false;  // Guard clause first

stack.Pop();
```

**Why:** Bracket matching fails when closing bracket appears before opening. Always check `Count == 0` upfront.

---

### Pitfall 3: Integer Overflow in Parsing

**WRONG:**
```csharp
result = result * 10 + digit;  // Can overflow!
```

**CORRECT:**
```csharp
if (result > int.MaxValue / 10 || 
    (result == int.MaxValue / 10 && digit > 7))
{
    return int.MaxValue;  // Clamp to MAX
}
result = result * 10 + digit;
```

**Why:** User input can be huge. Check bounds **before** operation, not after.

---

### Pitfall 4: Dictionary KeyNotFoundException

**WRONG:**
```csharp
int freq = charFreq[c];  // Throws if 'c' not in dict!
```

**CORRECT:**
```csharp
if (charFreq.ContainsKey(c))
    charFreq[c]++;
else
    charFreq[c] = 1;

// Or: Use TryGetValue
if (!charFreq.TryGetValue(c, out int freq))
    freq = 0;
```

**Why:** Dictionary doesn't auto-create missing keys. Always check or use `TryGetValue`.

---

### Pitfall 5: Modulo Arithmetic Errors in Rolling Hash

**WRONG:**
```csharp
hash = (hash - leftValue * basePower) % MOD;  // Can be negative!
```

**CORRECT:**
```csharp
hash = (hash - leftValue * basePower + MOD) % MOD;  // Add MOD, then mod
```

**Why:** Subtraction can result in negative number. `% MOD` of negative stays negative in C#. Add `MOD` first.

---

## 📈 Week 06 Problem Ladder: C# Specific

### Stage 1: Canonical Problems (Foundation)

```csharp
// Day 1: Palindromes
LeetCode #5: LongestPalindromicSubstring()  // Your ExpandAroundCenter helper
LeetCode #9: IsPalindrome()                 // Two-pointer or naive

// Day 2: Sliding Windows
LeetCode #3: LongestSubstringWithoutRepeating()   // Dictionary<char, int>
LeetCode #76: MinimumWindowSubstring()           // Two-pointer + dict
LeetCode #438: FindAllAnagrams()                 // Fixed window

// Day 3: Brackets
LeetCode #20: IsValidBrackets()              // Stack<char>
LeetCode #32: LongestValidParentheses()      // Stack<int> indices
LeetCode #22: GenerateParentheses()          // Backtracking + StringBuilder

// Day 4: Transformations
LeetCode #8: StringToInteger(atoi)           // Overflow checks
LeetCode #12: IntegerToRoman()               // Greedy mapping
LeetCode #443: CompressString()              // StringBuilder

// Day 5 (Optional): Rolling Hash
LeetCode #28: ImplementStrStr()              // Naive or rolling hash
LeetCode #187: RepeatedDNASequences()        // Rolling hash + HashSet
```

**Implementation Notes:**
- Day 1-2: Focus on understanding two-pointer logic
- Day 3: Emphasize `stack.Count == 0` guards
- Day 4: Use `StringBuilder` without exception
- Day 5: Be careful with modulo arithmetic

---

### Stage 2: Variations (Flexibility)

```csharp
// Day 1 Variations
LeetCode #131: PalindromePartitioning()      // Backtracking + palindrome check
LeetCode #647: PalindromicSubstrings()       // Count all, not just longest

// Day 2 Variations
LeetCode #159: LongestSubstringWithAtMostTwoDistinct()  // K=2 variant
LeetCode #567: PermutationInString()                     // Anagram variant
LeetCode #424: LongestRepeatingCharacterReplacement()    // K changes allowed

// Day 3 Variations
LeetCode #301: RemoveInvalidParentheses()    // Minimum removals (BFS)
LeetCode #1541: MinimumInsertionsToBalanceParentheses() // Insertions needed

// Day 4 Variations
LeetCode #68: TextJustification()            // Complex layout (StringBuilder heavy)
LeetCode #65: ValidNumber()                  // All edge cases (state machine)

// Day 5 Variations (Optional)
LeetCode #30: SubstringWithConcatenation()   // Multiple pattern variants
```

**C# Implementation Concerns:**
- Day 2: Handle K=2 special case vs general K
- Day 3: Two variations of BFS/removal logic
- Day 4: Complex formatting with `StringBuilder.PadRight()`
- Day 5: Multiple hash updates per iteration

---

### Stage 3: Integration (Mastery)

```csharp
// Mixed-pattern problems requiring multiple techniques

LeetCode #214: ShortestPalindrome()          // Palindrome + KMP/rolling hash
LeetCode #394: DecodeString()                // Stack + StringBuilder + recursion
LeetCode #432: AllOOne()                     // Complex data structure

// Pseudo-System Design

DesignAutocompleteSystem()
// Uses: Trie + frequency tracking + StringBuilder
// C# Concerns: Efficient trie with Dictionary nesting

DesignRealTimeCodeFormatter()
// Uses: Bracket matching + transformation + StringBuilder
// C# Concerns: Incremental updates, multi-line handling

DesignPlagiarismDetector()
// Uses: Rolling hash + multi-pattern detection + HashSet
// C# Concerns: Parallelization, corpus indexing
```

---

## 🎓 Decision Tree: When to Apply Each Pattern in C#

```
String Problem Analysis

├─ Is it about SYMMETRY?
│  └─ Use Expand-Around-Center (Day 1)
│     └─ Return longest palindrome
│     └─ Helper method for odd/even centers
│
├─ Is it about FINDING SUBSTRING with PROPERTY?
│  ├─ Fixed-size window?
│  │  └─ Use simple two-pointer (Day 2 simple)
│  └─ Variable-size window?
│     └─ Use sliding window + Dictionary (Day 2 complex)
│        └─ Expand/shrink based on constraint
│
├─ Is it about VALIDATION/NESTING?
│  ├─ Check if valid?
│  │  └─ Use Stack<char> + guard clauses (Day 3)
│  └─ Find longest valid?
│     └─ Use Stack<int> indices (Day 3 advanced)
│
├─ Is it about TRANSFORMATION?
│  ├─ Parse input?
│  │  └─ Guard clauses, overflow checks (Day 4)
│  ├─ Build output?
│  │  └─ MANDATORY: StringBuilder (Day 4)
│  └─ Convert format?
│     └─ Greedy mapping or character-by-character (Day 4)
│
└─ Is it about SCALE/MULTIPLE PATTERNS?
   └─ Use rolling hash + verify (Day 5 optional)
      └─ Hash each pattern once
      └─ Single pass through text
```

---

## 📝 Week 06 C# Code Quality Checklist

### Before Submitting Code

- [ ] **Guard Clauses First:** Check for null, empty, boundary cases upfront
- [ ] **No String Concatenation Loops:** Used `StringBuilder` for any `result += ` or similar
- [ ] **Stack Safety:** Always check `Count > 0` before pop/peek
- [ ] **Dictionary Safety:** Used `ContainsKey()` or `TryGetValue()` before access
- [ ] **Overflow Checks:** Verified bounds **before** arithmetic operations
- [ ] **Modulo Correctness:** Applied `% MOD` after each operation in rolling hash
- [ ] **Comments on Why:** Explained engineering decisions, not just syntax
- [ ] **Helper Methods:** Extracted complex logic into named helper methods
- [ ] **Const Values:** Named magic numbers (`MOD`, `BASE`, `INT_MAX_LAST_DIGIT`)
- [ ] **Efficiency:** O(n) or better for Week 06 patterns (no O(n²) naive approaches)

---

## 🏆 Week 06 Mastery Signals: C# Edition

You've mastered Week 06 when you can:

✅ **Implement expand-around-center without reference** (both odd/even centers)  
✅ **Write sliding window with Dictionary tracking** (no bugs in expand/shrink logic)  
✅ **Validate brackets with Stack<char>** (guard clauses upfront)  
✅ **Find longest valid parentheses** (use Stack<int> indices correctly)  
✅ **Generate all valid parentheses** (backtracking + StringBuilder)  
✅ **Parse integer defensively** (overflow checks before operations)  
✅ **Build strings with StringBuilder** (never use naive concatenation in loops)  
✅ **Convert formats greedily** (Roman numerals with correct ordering)  
✅ **Implement rolling hash** (remove-shift-add formula with modulo safety)  
✅ **Verify matches** (after hash collision to handle false positives)  

---

**Status:** Week 06 Extended C# Support Complete  
**Total Coverage:** 5 patterns with production-grade C# implementations  
**Code Style:** Logic-first narrative comments, guard clauses, helper methods  
**Performance:** O(n) or O(n²) at worst, avoiding naive O(n²) traps  
**Production Ready:** All code handles edge cases, overflow, and collisions