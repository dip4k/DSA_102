# 🗺️ WEEK 4.75 PROBLEM SOLVING ROADMAP

**Purpose:** A guided progression from easy to hard, ensuring you build skills layer by layer rather than jumping into the deep end.

---

## 🚦 STAGE 1: THE BASICS (Syntax & Logic)
*Goal: Get comfortable with string manipulation APIs and basic pointers.*

1.  **Reverse String** (LeetCode #344)
    *   *Focus:* Two pointers, in-place swap.
2.  **Valid Palindrome** (LeetCode #125)
    *   *Focus:* Handling non-alphanumeric, case sensitivity.
3.  **Valid Parentheses** (LeetCode #20)
    *   *Focus:* Basic Stack usage.
4.  **Reverse Words in a String III** (LeetCode #557)
    *   *Focus:* Reversing segments within a string.

---

## 🚦 STAGE 2: PATTERN MASTERY (The "Window" & "Center")
*Goal: Master O(N) linear scans and expansion.*

5.  **Longest Palindromic Substring** (LeetCode #5)
    *   *Focus:* Expand Around Center. (Don't memorize Manacher's).
6.  **Longest Substring Without Repeating Characters** (LeetCode #3)
    *   *Focus:* Variable Sliding Window + HashSet.
7.  **Permutation in String** (LeetCode #567)
    *   *Focus:* Fixed Sliding Window + Frequency Array (int[26]).
8.  **String to Integer (atoi)** (LeetCode #8)
    *   *Focus:* Edge case handling hell (Overflow, spaces, signs).

---

## 🚦 STAGE 3: INTERVIEW READY (Combinations & Optimization)
*Goal: Handle complex constraints and combinations of patterns.*

9.  **Generate Parentheses** (LeetCode #22)
    *   *Focus:* Backtracking with validity checks.
10. **Minimum Window Substring** (LeetCode #76)
    *   *Focus:* The "Ultimate" Sliding Window problem. Track "required" vs "formed".
11. **Encode and Decode Strings** (LeetCode #271)
    *   *Focus:* Designing a protocol (length-prefixing).
12. **Longest Repeating Character Replacement** (LeetCode #424)
    *   *Focus:* Valid window condition: `WindowLength - MaxCount <= K`.

---

## 🚦 STAGE 4: ADVANCED / HARD (Differentiation)
*Goal: Push boundaries for top-tier roles.*

13. **Longest Valid Parentheses** (LeetCode #32)
    *   *Focus:* Stack with Indices OR Dynamic Programming.
14. **Minimum Remove to Make Valid Parentheses** (LeetCode #1249)
    *   *Focus:* Two-pass greedy or Stack with Index tracking.
15. **Integer to English Words** (LeetCode #273)
    *   *Focus:* Massive implementation detail; modularizing code.

---

## 🛑 COMMON PITFALLS TO AVOID

1.  **The "Split" Trap:** Using `split()` creates an array of strings. This is O(N) space. If asked for O(1) space, you must iterate manually.
2.  **The "Concatenation" Trap:** Using `+=` inside a loop makes your solution O(N²).
3.  **The "Palindrome" Trap:** Forgetting that palindromes can be even length (`abba`). Center expansion must check `(i, i)` AND `(i, i+1)`.
4.  **The "Stack" Trap:** Using a stack for `Longest Valid Parentheses` but storing `char` instead of `index`. You need indices to calculate length!

---

**Status:** 🚀 Follow this order to avoid frustration!