# 🎤 WEEK 4.75 INTERVIEW QA REFERENCE — QUESTIONS ONLY

**Purpose:** Simulate a real interview environment. Practice explaining your approach aloud before coding. Solutions are deliberately omitted to encourage deep thinking.

---

## 🟢 WARM-UP QUESTIONS (Basic Proficiency)

### Q1: Valid Palindrome
"Given a string, determine if it is a palindrome, considering only alphanumeric characters and ignoring cases."
*   **Clarification:** Empty string? Symbols?
*   **Follow-up:** Can you solve this with O(1) memory?

### Q2: Valid Parentheses
"Given a string containing just the characters `(`, `)`, `{`, `}`, `[` and `]`, determine if the input string is valid."
*   **Clarification:** Is `([)]` valid? (No).
*   **Follow-up:** What if the string is mutable? Can we optimize space?

### Q3: Reverse String (In-Place)
"Write a function that reverses a string. The input string is given as an array of characters. Do not allocate extra space for another array."
*   **Follow-up:** How would you handle this if the input was a UTF-8 encoded string with Emojis?

---

## 🟡 CORE PATTERN QUESTIONS (Standard Interview)

### Q4: Longest Substring Without Repeating Characters
"Given a string `s`, find the length of the longest substring without repeating characters."
*   **Constraint:** `s` consists of English letters, digits, symbols and spaces.
*   **Follow-up:** What if we want the substring itself, not just the length?

### Q5: Longest Palindromic Substring
"Given a string `s`, return the longest palindromic substring in `s`."
*   **Constraint:** Max length 1000.
*   **Follow-up:** Can you explain the difference between O(N²) center expansion and O(N) Manacher's algorithm? Which one would you implement in 45 mins?

### Q6: String to Integer (atoi)
"Implement the `myAtoi(string s)` function, which converts a string to a 32-bit signed integer."
*   **Requirements:** Handle whitespace, sign, and overflow/underflow (clamping).
*   **Follow-up:** How do you detect overflow *before* it happens?

### Q7: Generate Parentheses
"Given `n` pairs of parentheses, write a function to generate all combinations of well-formed parentheses."
*   **Follow-up:** What is the time complexity of your solution? (Hint: Catalan numbers).

### Q8: Group Anagrams
"Given an array of strings, group the anagrams together."
*   **Follow-up:** What should be the key for your HashMap? Sorted string vs Count array?

---

## 🔴 ADVANCED / SYSTEM DESIGN QUESTIONS (Differentiation)

### Q9: Minimum Window Substring
"Given two strings `s` and `t`, return the minimum window substring of `s` such that every character in `t` (including duplicates) is included in the window."
*   **Follow-up:** If `s` is huge (stream) and `t` is small, how does your approach change?

### Q10: Longest Valid Parentheses
"Given a string containing just the characters `(` and `)`, find the length of the longest valid (well-formed) parentheses substring."
*   **Hint:** It's contiguous. Stack or DP?
*   **Follow-up:** Can this be done in O(1) space?

### Q11: Encode/Decode Strings
"Design an algorithm to encode a list of strings to a string. The encoded string is then sent over the network and is decoded back to the original list of strings."
*   **Constraint:** The strings can contain any character (including delimiters).
*   **Follow-up:** How does your encoding handle empty strings?

---

## 💬 BEHAVIORAL / META-COGNITIVE CHECKS

1.  **Trade-offs:** "Why did you choose a StringBuilder over string concatenation?"
2.  **Debugging:** "If your output was off by one index, where would you look first?"
3.  **Optimization:** "Your solution is O(N). Is it possible to do better? Why or why not?"

**Status:** 🚀 Use this list for mock interviews!