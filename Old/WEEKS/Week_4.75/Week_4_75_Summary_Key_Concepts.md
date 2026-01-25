# 🔑 WEEK 4.75 SUMMARY — KEY CONCEPTS & CHEAT SHEET

**Purpose:** Rapid review of all core string patterns and techniques covered this week.

---

## 📌 CONCEPT MAP

```
STRING PROBLEMS
│
├── 🔍 SEARCHING / SUBSTRINGS
│   ├── Sliding Window (Variable) ─── "Longest Substring..." (Day 2)
│   ├── Sliding Window (Fixed) ────── "Anagrams / Permutations" (Day 2)
│   └── Expansion ─────────────────── "Longest Palindrome" (Day 1)
│
├── 🏗️ STRUCTURE / VALIDATION
│   ├── Stack (LIFO) ──────────────── "Valid Parentheses" (Day 3)
│   ├── Two Pointers (Inward) ─────── "Is Palindrome?" (Day 1)
│   └── Backtracking ──────────────── "Generate Parentheses" (Day 3)
│
└── 🔄 TRANSFORMATIONS (Day 4)
    ├── Parsing (atoi) ────────────── Overflow & Type Safety
    ├── Reversal ──────────────────── In-place O(1) Space
    └── Building ──────────────────── StringBuilder (Amortized O(1))
```

---

## 📅 HIGHLIGHTS PER DAY

### **Day 1: Palindromes**
*   **Core Idea:** Symmetry. Check form ends (Inward) or expand from center (Outward).
*   **Key Techniques:**
    *   **Inward Pointers:** `L=0, R=n-1`. Good for *checking* validity.
    *   **Center Expansion:** `Expand(i, i)` (Odd) and `Expand(i, i+1)` (Even). Good for *finding* longest.
*   **Edge Cases:** Empty string, single char, even length (`abba`), mixed case/punctuation.

### **Day 2: Sliding Window**
*   **Core Idea:** Maintain a valid "window" state while greedily expanding `R`. Shrink `L` only when invalid.
*   **Key Techniques:**
    *   **Shrinkable Window:** `while (invalid) { remove s[L]; L++; }`.
    *   **State Tracking:** Use `int[128]` for ASCII or `HashMap` for Unicode.
*   **Edge Cases:** No valid window exists, window size > string length, all chars unique vs all same.

### **Day 3: Parentheses**
*   **Core Idea:** Nested validity requires LIFO (Stack). `(` must wait for `)`.
*   **Key Techniques:**
    *   **Stack:** Push opens, pop/check matches on close.
    *   **Min Remove:** Two-pass approach (stack for bad opens, immediate mark for bad closes).
    *   **Backtracking:** Generate valid combos by tracking `open_count` vs `close_count`.
*   **Trap:** `Counter` works for single type `()` but fails for multiple `([)]`.

### **Day 4: Transformations**
*   **Core Idea:** Efficient manipulation and robust parsing.
*   **Key Techniques:**
    *   **StringBuilder:** Avoid `s += c` in loops (O(N²) trap).
    *   **`atoi`:** Handle whitespace → sign → digits → overflow check.
    *   **Reversal:** In-place `swap(L, R)` for O(1) space.
*   **Trap:** Overflow check `res * 10 + d > MAX` is unsafe. Use `res > (MAX - d) / 10`.

---

## 🧠 CORE ALGORITHMS (PSEUDOCODE)

### 1. Sliding Window Template
```text
L = 0, Ans = 0
State = {}
For R = 0 to N-1:
    Add s[R] to State
    While State is Invalid:
        Remove s[L] from State
        L++
    Ans = max(Ans, R - L + 1)
```

### 2. Center Expansion (Palindrome)
```text
Function Expand(L, R):
    While L >= 0 AND R < N AND s[L] == s[R]:
        L--, R++
    Return Len(L, R)
```

### 3. Valid Parentheses (Stack)
```text
Stack S
For char c in s:
    If c is Open: Push c
    Else:
        If S empty OR S.top doesn't match c: Return False
        S.pop()
Return S.empty()
```

### 4. Atoi (Safe Parsing)
```text
Res = 0
While i < N AND isDigit(s[i]):
    Digit = s[i] - '0'
    If Res > (MAX - Digit) / 10: Return MAX/MIN
    Res = Res * 10 + Digit
```

---

## ⚡ QUICK TIPS FOR REVISION

*   **Palindrome:** Always consider **ODD** vs **EVEN** length centers.
*   **Substring:** **Contiguous** means Sliding Window; **Non-contiguous** means DP/Subsequence.
*   **Parentheses:** If solving "Longest Valid", you usually need indices in the Stack, not just chars.
*   **Reverse:** Reverse string O(N) time, O(1) space. Reverse words = Reverse string + Reverse each word.

---

**Status:** ✅ Use this file for last-minute review before an interview!