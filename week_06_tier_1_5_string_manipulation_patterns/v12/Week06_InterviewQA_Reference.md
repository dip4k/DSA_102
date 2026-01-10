# 🎙️ Week 06 Interview Q&A Reference: 40+ Questions by Topic

**Status:** Interview Coaching Resource  
**Audience:** Students preparing for technical interviews  
**Format:** Questions only (no answers) + follow-ups to force deeper thinking  
**How to Use:** Pick 3-5 questions daily, attempt before looking up answers, discuss with partner

---

## Usage Guidelines

**The Goal:** Active learning, not passive reading. Answering practice questions builds fluency.

**Best Practice:**
1. Read question
2. Think for 2-3 minutes
3. Sketch solution on paper
4. Code if time permits
5. Review solution in instructional file
6. Discuss approach with peer

**Progressive Difficulty:**
- **Easy (🟢):** Foundation; if you struggle, review instructional file
- **Medium (🟡):** Core; you should solve these consistently
- **Hard (🔴):** Advanced; requires pattern mastery + problem-solving creativity

---

## 🎯 DAY 1: PALINDROME PATTERNS — 8 Questions

### Core Concept Questions

**Q1 (🟢 Easy):** What is a palindrome? Give 3 examples of varying lengths.
- **Follow-up:** How many potential centers are there in a string of length n?
- **Follow-up:** Why do we need to check both odd-length and even-length centers?

**Q2 (🟢 Easy):** Explain the expand-around-center algorithm in 3 sentences.
- **Follow-up:** What invariant does the algorithm maintain?
- **Follow-up:** Why is this approach O(n²) worst-case?

**Q3 (🟡 Medium):** Implement palindrome checking (is string a palindrome?). Write pseudocode.
- **Follow-up:** Can you do it with O(1) extra space?
- **Follow-up:** What are the edge cases?

**Q4 (🟡 Medium):** Given a string, find the longest palindromic substring using expand-around-center.
- **Follow-up:** How would you handle the even-length center differently?
- **Follow-up:** Trace through example "abaxyz" manually.

### Advanced & Trade-off Questions

**Q5 (🔴 Hard):** Compare three approaches to finding longest palindrome: expand-around-center O(n²), dynamic programming O(n²), Manacher O(n). Which is best and why?
- **Follow-up:** When would you sacrifice Manacher's O(n) guarantee for simpler O(n²) code?
- **Follow-up:** What's the space difference between DP and expand-around-center?

**Q6 (🟡 Medium):** If you're given a string and must return ALL palindromic substrings (not just longest), how does your approach change?
- **Follow-up:** What's the worst-case number of palindromic substrings?
- **Follow-up:** How would you avoid redundant work?

### System Design & Real-World

**Q7 (🟡 Medium):** A DNA analysis system needs to find palindromic sequences (which form loops). How would you scale expand-around-center for 3 billion base pairs?
- **Follow-up:** Would you use expand-around-center or a more complex algorithm like Manacher?
- **Follow-up:** How would you parallelize this across servers?

**Q8 (🔴 Hard):** Design a system to detect palindromic patterns in a stream of characters. You don't know the stream length in advance.
- **Follow-up:** Can you maintain results incrementally without re-scanning all history?
- **Follow-up:** What are the space constraints if stream runs for 24 hours?

---

## 🎯 DAY 2: SUBSTRING SLIDING WINDOWS — 8 Questions

### Core Concept Questions

**Q1 (🟢 Easy):** Define a sliding window. Give 2 examples of substring problems that use it.
- **Follow-up:** What's the difference between fixed-size and variable-size windows?
- **Follow-up:** Why is sliding window O(n) instead of O(n²)?

**Q2 (🟢 Easy):** Explain the "expand-shrink" pattern: when do you expand the window? When shrink?
- **Follow-up:** What data structure tracks constraints during shrinking?
- **Follow-up:** How do you maintain a frequency map efficiently?

**Q3 (🟡 Medium):** Find the longest substring without repeating characters (e.g., "abcabcbb" → "abc").
- **Follow-up:** Trace through the algorithm manually on "au" or "dvdf".
- **Follow-up:** What happens with an empty string or single character?

**Q4 (🟡 Medium):** Find the longest substring with at most K distinct characters.
- **Follow-up:** How does this differ from "longest without repeating"?
- **Follow-up:** What if K = 1? How does algorithm behave?

### Advanced & Trade-off Questions

**Q5 (🟡 Medium):** Find the minimum window substring (smallest substring containing all characters of pattern).
- **Follow-up:** Why is this harder than "longest without repeating"?
- **Follow-up:** How do you track "contains all characters of pattern"?

**Q6 (🔴 Hard):** Given string S and pattern P, find all anagrams of P in S using sliding window.
- **Follow-up:** How is this different from exact pattern matching?
- **Follow-up:** What about case sensitivity and special characters?

### System Design & Real-World

**Q7 (🟡 Medium):** An autocomplete system analyzes user typing patterns. It needs to find "longest substring of consecutive keystrokes without backspace". How would you use sliding window?
- **Follow-up:** What if users can backspace (delete previous character)?
- **Follow-up:** How would you handle special keys like Ctrl+A (select all)?

**Q8 (🔴 Hard):** A plagiarism detector needs to find longest common substring between two documents (100MB each). Using only sliding window (no hashing), how would you solve it?
- **Follow-up:** Is sliding window even the right approach here?
- **Follow-up:** How would you optimize if substring must be at least 100 characters?

---

## 🎯 DAY 3: BRACKET MATCHING — 8 Questions

### Core Concept Questions

**Q1 (🟢 Easy):** Explain what "balanced brackets" means. Give 2 valid and 2 invalid examples.
- **Follow-up:** Why is "{[}]" invalid despite having equal opens and closes?
- **Follow-up:** What does LIFO stand for and why does it apply here?

**Q2 (🟢 Easy):** Describe the stack-based bracket validation algorithm in 4 steps.
- **Follow-up:** What's the invariant the stack maintains?
- **Follow-up:** What error checks do you need before popping?

**Q3 (🟡 Medium):** Implement valid parentheses checker. Handle three types: (), [], {}.
- **Follow-up:** Trace through "()" and "([)]" manually.
- **Follow-up:** What happens with empty input? Single bracket?

**Q4 (🟡 Medium):** Find the longest valid parentheses substring in a mixed string like "())(()" .
- **Follow-up:** Why can't you just count opens and closes?
- **Follow-up:** How does this differ from "is valid"?

### Advanced & Trade-off Questions

**Q5 (🟡 Medium):** Generate all valid combinations of n pairs of parentheses. For n=2, output: ["(())", "()()"].
- **Follow-up:** Is this a stack problem or backtracking problem?
- **Follow-up:** How many valid combinations exist for n pairs? (Catalan number!)

**Q6 (🔴 Hard):** Remove minimum characters to make string valid. E.g., "lee(code)de)" → "lee(code)de" (remove 1 character).
- **Follow-up:** Is this a greedy problem or DP problem?
- **Follow-up:** Can you do it in one pass?

### System Design & Real-World

**Q7 (🟡 Medium):** A code editor highlights matching brackets in real-time. User types "while (x > 0) {" and cursor is at end. How do you highlight the matching "{"?
- **Follow-up:** What if brackets are mismatched?
- **Follow-up:** How do you handle multiline code?

**Q8 (🔴 Hard):** A compiler parses mathematical expressions: "3 * (2 + (5 - 1))". Design a system to parse and evaluate such expressions with proper bracket precedence.
- **Follow-up:** How do you handle precedence (*, / before +, -)?
- **Follow-up:** Can you do this with just a stack or do you need additional data structures?

---

## 🎯 DAY 4: STRING TRANSFORMATIONS — 8 Questions

### Core Concept Questions

**Q1 (🟢 Easy):** What is string immutability? How does it impact concatenation performance?
- **Follow-up:** In a language like Java, why is "str1 + str2" O(n) each time?
- **Follow-up:** What's the time complexity of concatenating 1000 strings naively?

**Q2 (🟢 Easy):** Explain StringBuilder. How does it avoid the O(n²) concatenation trap?
- **Follow-up:** What's the amortized time complexity?
- **Follow-up:** Why do we double capacity instead of incrementing by 1?

**Q3 (🟡 Medium):** Implement atoi (string to integer): parse "  -42  xyz" → -42.
- **Follow-up:** How do you handle leading/trailing whitespace?
- **Follow-up:** How do you detect overflow before it happens?

**Q4 (🟡 Medium):** Convert integer to Roman numeral. E.g., 1994 → "MCMXCIV".
- **Follow-up:** Why does greedy work for this problem?
- **Follow-up:** What's the key ordering for the mapping table?

### Advanced & Trade-off Questions

**Q5 (🟡 Medium):** Implement run-length encoding: "AAABBCCCCAABBBCD" → "3A3B4C2A3B1C1D".
- **Follow-up:** When does RLE expand the data instead of compress?
- **Follow-up:** How would you decode it back?

**Q6 (🔴 Hard):** Implement the zigzag conversion: arrange string in zigzag pattern, then read row-by-row. E.g., "PAYPALISHIRING" with 3 rows becomes "PAHNAPLSIIGYIR".
- **Follow-up:** Can you derive a formula for character positions instead of simulating?
- **Follow-up:** What's the time and space complexity?

### System Design & Real-World

**Q7 (🟡 Medium):** A logging system logs 10 million events per day. Each log entry builds a string by concatenation. Without StringBuilder, what's the time impact?
- **Follow-up:** Calculate: naive O(n²) vs builder O(n) for 10M strings.
- **Follow-up:** How would you refactor the logging system?

**Q8 (🔴 Hard):** Design a CSV parser that reads lines like "John,Doe,30" and must handle edge cases: quoted fields with commas ("Smith, Jr."), missing fields, special characters.
- **Follow-up:** How do you handle quoted fields?
- **Follow-up:** What about escaped quotes inside quoted fields?

---

## 🎯 DAY 5: ADVANCED PATTERN MATCHING (OPTIONAL) — 8 Questions

### Core Concept Questions

**Q1 (🟢 Easy):** Explain rolling hash: how do you compute the hash of the next window in O(1)?
- **Follow-up:** What's the "remove-shift-add" formula?
- **Follow-up:** Why must you apply modulo after each operation?

**Q2 (🟢 Easy):** What's the Karp-Rabin algorithm? Give a one-sentence summary.
- **Follow-up:** When is hash match NOT a string match?
- **Follow-up:** Why do you verify after hash match?

**Q3 (🟡 Medium):** Implement Karp-Rabin pattern matching for pattern P in text T.
- **Follow-up:** Trace through finding "BC" in "ABCDE" step-by-step.
- **Follow-up:** What modulus would you use and why?

**Q4 (🟡 Medium):** Why is collision probability negligible with large prime modulus?
- **Follow-up:** If you have 10^6 characters, what's expected number of collisions with modulus 10^9 + 7?
- **Follow-up:** Calculate: how many false positives are acceptable?

### Advanced & Trade-off Questions

**Q5 (🔴 Hard):** Compare Karp-Rabin (O(n+m) avg), KMP (O(n+m) worst), Boyer-Moore (O(n/m) best). Which would you use for each scenario?
- **Follow-up:** Plagiarism detection (multiple patterns in corpus)?
- **Follow-up:** Long pattern in short text?

**Q6 (🔴 Hard):** Implement rolling hash with multiple moduli to reduce collision probability. Use two independent large primes.
- **Follow-up:** What's the new collision probability?
- **Follow-up:** Is the O(1) update still O(1) with two hashes?

### System Design & Real-World

**Q7 (🟡 Medium):** Turnitin detects plagiarism by finding common 30-character substrings between submission and corpus (100M documents). How would rolling hash scale this?
- **Follow-up:** How many possible 30-char substrings does a 50K-char submission have?
- **Follow-up:** How do you efficiently check if a substring appears in corpus?

**Q8 (🔴 Hard):** A DNA analysis tool searches for disease sequences (100 known patterns) in a 3-billion base-pair genome. Design a system using rolling hash or Aho-Corasick.
- **Follow-up:** Can you run Karp-Rabin 100 times or is Aho-Corasick better?
- **Follow-up:** How do you parallelize across servers?

---

## 🧠 Cross-Pattern Questions: Integration & Mastery

### Q1 (🟡 Medium): Pattern Recognition
You're given a problem: "Find the longest substring that is a palindrome." Without coding, identify which days' concepts you'd use.
- **Follow-up:** Could you solve it with sliding window (Day 2)?
- **Follow-up:** Could you solve it with expand-around-center (Day 1)?
- **Follow-up:** Which is better and why?

### Q2 (🟡 Medium): Multi-Pattern Problem
Given string, find all substrings that: (a) are palindromes, AND (b) don't repeat characters.
- **Follow-up:** How do you combine Day 1 and Day 2 techniques?
- **Follow-up:** What's the overall time complexity?

### Q3 (🔴 Hard): System Design Challenge
Design a real-time comment moderation system that:
- Detects banned words (pattern matching)
- Checks for bracket balance in code snippets
- Converts URLs to links (transformation)
- Optimizes for 1 million comments/second

Which patterns would you use for each part?

### Q4 (🔴 Hard): Trade-off Analysis
You're building a text editor. Which patterns would you use for: (a) Find & replace, (b) Autocomplete suggestions, (c) Bracket matching, (d) Real-time spell check?
- **Follow-up:** Justify your choices with complexity analysis.
- **Follow-up:** How would you optimize each for 1MB files?

---

## ✅ Interview Prep Strategy: Using These Questions

**Week 1:** Read all questions (don't solve yet). Get familiar with problem landscape.

**Week 2:** Solve 3-5 questions daily without looking at instructional files. Track which you struggle with.

**Week 3:** Deep dive on struggling questions. Review corresponding instructional content.

**Interview Week:** Pick 1 question from each day daily. Solve quickly (10-15 min). Verbalize your thinking.

---

## 🎯 How to Discuss Your Answer

When answering, structure your response:

1. **Clarify:** "Is the pattern P guaranteed to appear?" "Case sensitive?" "Multiple matches or first only?"
2. **Approach:** "I would use [pattern] because [reason]."
3. **Pseudocode:** Draw / write it out.
4. **Trace:** Walk through an example manually.
5. **Complexity:** "Time O(...) because ..., Space O(...) because ..."
6. **Edge Cases:** "Edge cases: empty input, single character, all same character, [pattern] doesn't appear, boundary overflow."
7. **Optimize:** "Naive approach would be O(...). I optimize with [technique] to get O(...)."

---

**Status:** Week 06 Interview Q&A Reference Complete  
**Total Questions:** 40+ (8 per day × 5 days + 4 cross-pattern)  
**Next:** Week 06 Problem-Solving Roadmap  
**Interview Prep Time:** 2-3 hours daily during interview week