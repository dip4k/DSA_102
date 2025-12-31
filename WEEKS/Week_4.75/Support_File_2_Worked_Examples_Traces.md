# 🎬 SUPPORT FILE 2: WORKED EXAMPLES & TRACES — WEEK 4.75 STRING MASTERY

**Purpose:** Deep traces through multiple examples for each Day's core topic  
**Use Case:** Study guide for internalizing mechanics before interviews  
**Format:** Detailed step-by-step traces with visual state tables

---

## DAY 1: PALINDROMES — WORKED EXAMPLES

### Example 1: Valid Palindrome (Normalized) — "A man, a plan, a canal: Panama"

**Task:** Check if palindrome, ignoring non-alphanumeric and case.

**Trace:**

| Step | L Index | R Index | L Char | R Char | L Char (normalized) | R Char (normalized) | Match? | Action | L/R Move |
|------|---------|---------|--------|--------|---------------------|---------------------|--------|--------|----------|
| 0 | 0 | 29 | 'A' | 'a' | 'a' | 'a' | ✅ | Compare | L++, R-- |
| 1 | 1 | 28 | ' ' | 'm' | skip | (after skip L=3) | — | Skip space | L++ |
| 2 | 2 | 28 | 'm' | 'm' | 'm' | 'm' | ✅ | Compare | L++, R-- |
| 3 | 4 | 27 | 'a' | 'a' | 'a' | 'a' | ✅ | Compare | L++, R-- |
| ... | ... | ... | ... | ... | ... | ... | ... | (continue) | ... |
| Final | L > R | — | — | — | — | — | ✅ | Stack empty | **VALID** |

**Output:** `True` (entire string is a palindrome after normalization)

---

### Example 2: Longest Palindromic Substring — "babad"

**Task:** Find longest palindromic substring (can be "bab" or "aba").

**Trace (Expand Around Centers):**

| Center | Type | L | R | Expansion | s[L] | s[R] | Match? | Max Length | Best |
|--------|------|---|---|-----------|------|------|--------|------------|------|
| 0 | Odd | 0 | 0 | Start | 'b' | 'b' | — | 1 | 'b' |
| — | — | -1 | 1 | Boundary | OUT | 'a' | Stop | — | — |
| 1 | Odd | 1 | 1 | Start | 'a' | 'a' | — | 1 | 'a' |
| — | — | 0 | 2 | Expand | 'b' | 'b' | ✅ | 3 | **'bab'** ← Current best |
| — | — | -1 | 3 | Boundary | OUT | — | Stop | — | — |
| 2 | Odd | 2 | 2 | Start | 'b' | 'b' | — | 1 | 'b' |
| — | — | 1 | 3 | Expand | 'a' | 'a' | ✅ | 3 | **'aba'** (ties best) |
| — | — | 0 | 4 | Expand | 'b' | 'd' | ❌ | Stop | — |
| (continue...) | — | — | — | — | — | — | — | — | — |

**Output:** "bab" or "aba" (length 3)

---

### Example 3: Valid Palindrome II (Delete at Most One) — "abca"

**Task:** Check if valid palindrome by deleting at most one character.

**Trace:**

```
L=0 'a', R=3 'a' → Match ✅, L=1, R=2
L=1 'b', R=2 'c' → Mismatch ❌

Branch 1: Skip L (delete 'b')
  Check range [2..2]: 'c' == 'c'? Single char = VALID ✅

Branch 2: Skip R (delete 'c')
  Check range [1..1]: 'b' == 'b'? Single char = VALID ✅

Either branch succeeds → return True
```

**Output:** `True` (delete either 'b' or 'c')

---

## DAY 2: SUBSTRING SLIDING WINDOW — WORKED EXAMPLES

### Example 1: Longest Substring Without Repeating — "abcabcbb"

**Task:** Find longest substring with no repeating characters.

**Trace:**

| R | Char | L | Char | Set | Valid? | Action | Window | Max Len |
|---|------|---|------|-----|--------|--------|--------|---------|
| 0 | 'a' | 0 | 'a' | {a} | ✅ | Add | "a" | 1 |
| 1 | 'b' | 0 | 'a' | {a,b} | ✅ | Add | "ab" | 2 |
| 2 | 'c' | 0 | 'a' | {a,b,c} | ✅ | Add | "abc" | **3** |
| 3 | 'a' | 0 | 'a' | {a,b,c,a} | ❌ | 'a' repeats (counts[a]=2) | — | 3 |
| — | — | 1 | 'b' | {b,c,a} | ✅ | Remove s[0]='a', shrink | "bca" | 3 |
| 4 | 'b' | 1 | 'b' | {b,c,a,b} | ❌ | 'b' repeats | — | 3 |
| — | — | 2 | 'c' | {c,a,b} | ✅ | Remove s[1]='b', shrink | "cab" | 3 |
| ... (continue) | — | — | — | — | — | — | — | — |

**Output:** 3 (substring "abc", "bca", or "cab")

---

### Example 2: Permutation in String — Text "ab", Pattern "eidbaaacb"

**Task:** Check if pattern "ab" is a permutation (anagram) substring in text.

**Trace (Fixed Window of size 2):**

| Window (L..R) | Window Str | Char Counts | Matches? | Action |
|---|---|---|---|---|
| [0..1] | "ei" | {e:1, i:1} ≠ {a:1, b:1} | ❌ | Slide |
| [1..2] | "id" | {i:1, d:1} ≠ {a:1, b:1} | ❌ | Slide |
| [2..3] | "db" | {d:1, b:1} ≠ {a:1, b:1} | ❌ | Slide |
| [3..4] | "ba" | {b:1, a:1} = {a:1, b:1} | ✅ | **FOUND** |

**Output:** `True` (found at index 3-4)

---

## DAY 3: PARENTHESES & BRACKETS — WORKED EXAMPLES

### Example 1: Valid Parentheses — "([{}])"

**Task:** Validate parentheses.

**Trace (Stack):**

| Char | Type | Stack Before | Action | Stack After | Valid? |
|------|------|--------------|--------|-------------|--------|
| '(' | Open | [] | Push '(' | ['('] | — |
| '[' | Open | ['('] | Push '[' | ['(', '['] | — |
| '{' | Open | ['(', '['] | Push '{' | ['(', '[', '{'] | — |
| '}' | Close | ['(', '[', '{'] | Top='{', matches | Pop | ['(', '['] | ✅ |
| ']' | Close | ['(', '['] | Top='[', matches | Pop | ['('] | ✅ |
| ')' | Close | ['('] | Top='(', matches | Pop | [] | ✅ |
| (end) | — | [] | Stack empty | — | **VALID** ✅ |

**Output:** `True`

---

### Example 2: Longest Valid Parentheses (Index Stack) — ")()())"

**Task:** Find longest valid parentheses substring.

**Trace:**

```
Initialize: stack = [-1]
Scan:
i=0 ')': Stack top -1. Pop -1. Stack empty. Push i=0 as base. stack=[0]
i=1 '(': Push 1. stack=[0,1]
i=2 ')': Pop 1. Top now 0. Length = 2-0 = 2. Max=2. stack=[0]
i=3 '(': Push 3. stack=[0,3]
i=4 ')': Pop 3. Top now 0. Length = 4-0 = 4. Max=4. stack=[0]
i=5 ')': Stack top 0. Pop 0. Stack empty. Push i=5 as base. stack=[5]
Final: Max length = 4 (substring from index 1-4: "()()")
```

**Output:** 4

---

## DAY 4: STRING TRANSFORMATIONS — WORKED EXAMPLES

### Example 1: String to Integer — "  +0 43"

**Task:** Convert to integer with overflow checking.

**Trace:**

| Step | i | Char | Action | i | sign | result | Notes |
|------|---|------|--------|---|------|--------|-------|
| 0 | 0 | ' ' | Skip space | →1 | 1 | 0 | Leading whitespace |
| 1 | 1 | ' ' | Skip space | →2 | 1 | 0 | Continue skipping |
| 2 | 2 | '+' | Process sign | →3 | 1 | 0 | Positive |
| 3 | 3 | '0' | Extract digit 0 | →4 | 1 | 0 | 0*10+0=0 |
| 4 | 4 | ' ' | Not a digit, STOP | — | 1 | 0 | End of number |

**Output:** 0 (stops at space; trailing "43" ignored)

---

### Example 2: String Reversal (In-Place) — "hello"

**Task:** Reverse string in-place.

**Trace:**

| L | R | s[L] | s[R] | Action | String After | L | R |
|---|---|------|------|--------|--------------|---|---|
| 0 | 4 | 'h' | 'o' | Swap | "olleh" | →1 | →3 |
| 1 | 3 | 'e' | 'l' | Swap | "olheh"→"olleh" | →2 | →2 |
| 2 | 2 | 'l' | 'l' | L==R, STOP | "olleh" | — | — |

**Output:** "olleh"

---

### Example 3: Zigzag Conversion — "PAYPALISHIRING", 3 rows

**Task:** Convert to zigzag pattern across 3 rows.

**Trace:**

```
Row tracking with direction flag:
Index | Char | Row | Dir | Action
0     | P    | 0   | ↓   | Row 0: [P]
1     | A    | 1   | ↓   | Row 1: [A]
2     | Y    | 2   | ↓   | Row 2: [Y]
3     | P    | 1   | ↑   | Hit boundary, reverse direction
4     | A    | 0   | ↑   | Row 0: [P,A]
5     | L    | 1   | ↓   | Hit boundary, reverse direction
6     | I    | 2   | ↓   | Row 2: [Y,I]
7     | S    | 1   | ↑   | Row 1: [A,S]
8     | H    | 0   | ↑   | Row 0: [P,A,H]
9     | I    | 1   | ↓   | Row 1: [A,S,I]
10    | R    | 2   | ↓   | Row 2: [Y,I,R]
11    | I    | 1   | ↑   | Row 1: [A,S,I,I]
12    | N    | 0   | ↑   | Row 0: [P,A,H,N]
13    | G    | 1   | ↓   | Row 1: [A,S,I,I,G]

Result by concatenating rows:
Row 0: PAHN
Row 1: ASIIG
Row 2: YIR
→ "PAHNASIIGYIR"
```

**Output:** "PAHNASIIGYIR"

---

## 🎬 MINI CASE STUDY: Interview Scenario

### Scenario: "Longest Substring Without Repeating Characters"

**Interviewer:** "Given a string s, find the length of the longest substring without repeating characters. Optimize for time."

**Candidate Approach:**

1. **Problem Understanding (30 seconds):**
   - "I need to find a contiguous substring."
   - "No character can repeat within that substring."
   - "I want the maximum length."

2. **Algorithm Selection (1 minute):**
   - "This is a sliding window problem."
   - "I'll expand right greedily, shrink left only when I see a repeat."

3. **Pseudocode (2 minutes):**
   ```
   Use a Set or Map to track characters in current window
   L = 0
   for R in 0..n-1:
     while s[R] in window:
       remove s[L] from window
       L++
     add s[R] to window
     ans = max(ans, R - L + 1)
   ```

4. **Trace on "dvdf" (3 minutes):**
   ```
   R=0, 'd': window={d}, ans=1
   R=1, 'v': window={d,v}, ans=2
   R=2, 'd': 'd' in window! Shrink: remove s[0]='d', L=1. window={v,d}, ans=2
   R=3, 'f': window={v,d,f}, ans=3
   Answer: 3 (substring "vdf")
   ```

5. **Complexity Analysis (1 minute):**
   - "Time: O(n) because each character is visited by L and R exactly once."
   - "Space: O(min(n, 26)) for the character set (or 128 for ASCII)."

6. **Edge Cases (1 minute):**
   - Empty string: return 0.
   - Single char: return 1.
   - All same: return 1.
   - All different: return n.

**Total Time:** ~10 minutes. **Outcome:** Strong hire (correct logic, clear communication, edge case awareness).

---

**Last Updated:** Dec 31, 2025  
**Format:** Study guide with detailed traces  
**Suggested Use:** Work through each example before attempting practice problems