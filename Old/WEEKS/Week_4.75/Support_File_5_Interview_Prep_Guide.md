# 🎯 SUPPORT FILE 5: INTERVIEW PREP GUIDE & FOLLOW-UPS — WEEK 4.75 STRING MASTERY

**Purpose:** Realistic interview scenarios with common follow-ups and assessment rubric  
**Use Case:** Prepare for actual interviews; understand what interviewers are evaluating  
**Format:** Mock questions → expected answers → follow-ups → strong/weak patterns

---

## 📋 INTERVIEW QUESTION DATABASE

### QUESTION BANK 1: PALINDROMES

#### Q1.1 (Easy Warm-up): "Check if a string is a palindrome"

**What Interviewer Is Assessing:**
- ✅ Can you implement two-pointer logic correctly?
- ✅ Do you think about boundary conditions?
- ✅ Do you clarify edge cases before coding?

**Strong Answer Structure (5-10 minutes):**

1. **Clarification (1 min):**
   - "Does case matter?" → Likely no
   - "Should I ignore non-alphanumeric?" → Likely yes
   - "Should I handle Unicode?" → Depends on role; usually ASCII OK

2. **Approach (2 min):**
   - "I'll use two pointers, one from each end."
   - "I'll skip non-alphanumeric characters and compare case-insensitively."
   - "Time O(n), Space O(1)."

3. **Code/Pseudocode (3 min):**
   ```
   L = 0, R = n-1
   while L < R:
     skip non-alphanumeric at L
     skip non-alphanumeric at R
     if lowercase(s[L]) != lowercase(s[R]):
       return false
     L++, R--
   return true
   ```

4. **Examples (1 min):**
   - "abc" → false
   - "A man, a plan, a canal: Panama" → true

5. **Edge Cases (1 min):**
   - Empty → true
   - Single char → true
   - All punctuation → true (no valid chars)

**Follow-up 1.1a:** "Can you do it without modifying the input?"
- **Weak:** "Uh, I already did..."
- **Strong:** "Yes, I never modified the original string; I only used pointers."

**Follow-up 1.1b:** "What if the string is 1 billion characters?"
- **Weak:** No response
- **Strong:** "Time O(n) is already optimal for checking every character. Space O(1) is good. If it's on disk, seeking could be the bottleneck, not the algorithm."

**Follow-up 1.1c:** "What if we add Unicode emoji?"
- **Weak:** "My solution breaks..."
- **Strong:** "For emoji, we'd need to handle surrogate pairs or grapheme clusters. In UTF-8, I'd need to be careful about multi-byte sequences. For now, ASCII is fine; real system would use a library."

**Rubric:**
- ✅ **Strong Hire:** Correct logic, thinks about edge cases, explains trade-offs, answers follow-ups
- ⚠️ **Hire:** Correct logic, minor edge case miss, answers some follow-ups
- ❌ **No Hire:** Logic errors, doesn't handle basic edge cases, or can't explain

---

#### Q1.2 (Medium): "Find the longest palindromic substring"

**What Interviewer Is Assessing:**
- ✅ Do you know when to expand vs shrink pointers?
- ✅ Do you handle even/odd palindromes differently?
- ✅ Do you optimize correctly (center expansion vs DP)?

**Strong Answer Structure (10-15 minutes):**

1. **Clarification (1 min):**
   - "Is the string empty? → handle → return empty"
   - "Can there be multiple answers? → return any one"

2. **Approach Comparison (2 min):**
   - "Option A: Expand around center (O(n²), O(1) space) — good for this problem"
   - "Option B: DP (O(n²), O(n²) space) — overkill here"
   - "Option C: Manacher (O(n), O(n)) — advanced; not needed for interviews"

3. **Choose & Explain (2 min):**
   - "I'll use center expansion because:"
   - "Simple to code and explain"
   - "O(1) extra space (good if memory is tight)"
   - "Works well for typical constraints"

4. **Pseudocode (3 min):**
   ```
   best_start = 0, best_len = 0
   for i in 0..n-1:
     len_odd = expand(s, i, i)
     len_even = expand(s, i, i+1)
     len = max(len_odd, len_even)
     if len > best_len:
       best_len = len
       best_start = i - (len - 1) / 2
   return s[best_start:best_start+best_len]
   
   expand(s, L, R):
     while L >= 0 and R < n and s[L] == s[R]:
       L--, R++
     return R - L - 1
   ```

5. **Trace (2 min):**
   - Input: "babad"
   - Center i=1: expand odd → "bab" (len=3)
   - Center i=2: expand odd → "aba" (len=3)
   - Answer: "bab" or "aba"

6. **Complexity (1 min):**
   - Time O(n²) worst case (expansion per center)
   - Space O(1) extra (just pointers)

**Follow-up 1.2a:** "What if we need to handle multiple queries (find palindrome for 1000 strings)?"
- **Strong:** "Precompute a 2D DP table once (O(n²) time, O(n²) space) so each query is O(1). Overall better for multiple queries."

**Follow-up 1.2b:** "What if n = 10^6?"
- **Strong:** "Center expansion might TLE due to O(n²). Could use Manacher or rolling hash. Or check if problem has other constraints."

**Follow-up 1.2c:** "What about even-length palindromes? Can you explain why we need (i, i+1)?"
- **Strong:** "Odd: 'aba' has center at 'b'. Even: 'abba' has center between the two 'b's. Without checking (i, i+1), we'd miss all even palindromes."

**Rubric:**
- ✅ **Strong Hire:** Correct approach, handles even/odd, answers follow-ups on Manacher or DP trade-offs
- ⚠️ **Hire:** Correct approach, minor confusion on index calculation, handles follow-ups
- ❌ **No Hire:** Misses even centers or has off-by-one bugs

---

### QUESTION BANK 2: SLIDING WINDOW

#### Q2.1 (Easy): "Longest substring without repeating characters"

**What Interviewer Is Assessing:**
- ✅ Do you understand shrinkable window (while loop, not if)?
- ✅ Can you maintain state (set/map) efficiently?
- ✅ Do you update answer at the right time?

**Strong Answer (8-10 minutes):**

1. **Clarification:** ASCII only? Assume yes unless told.

2. **Approach:** "Sliding window. Expand right, shrink left when we see a repeat."

3. **Pseudocode:**
   ```
   L = 0, ans = 0
   seen = set()
   for R in 0..n-1:
     while s[R] in seen:
       seen.remove(s[L])
       L++
     seen.add(s[R])
     ans = max(ans, R - L + 1)
   return ans
   ```

4. **Trace:** "dvdf" → at 'd' repeat, shrink L → window is "vd" → "vdf" (ans=3)

5. **Complexity:** Time O(n), Space O(min(n, 26))

**Follow-up 2.1a:** "What if the string has Unicode?"
- **Strong:** "Still O(n) time. Space becomes O(min(n, 2^20)) for all possible Unicode. Could use HashMap instead of array."

**Follow-up 2.1b:** "Can you use a Counter/HashMap to solve 'Longest with at most K distinct characters'?"
- **Strong:** "Yes. Check `if len(counter) > K` instead of `if char in set`. Still O(n)."

**Rubric:**
- ✅ **Strong:** Correct logic, good explanation of why while not if
- ⚠️ **Hire:** Correct, minor explanation gaps
- ❌ **No Hire:** Off-by-one in window size, or uses if instead of while

---

#### Q2.2 (Hard): "Minimum window substring"

**What Interviewer Is Assessing:**
- ✅ Can you track "required" vs "have" counts separately?
- ✅ Do you update answer only for valid windows?
- ✅ Can you handle the window shrinking logic correctly?

**Strong Answer (12-15 minutes):**

1. **Clarification:** Case-sensitive? Character or word? Assume characters.

2. **Approach:**
   - "Use a sliding window with a HashMap."
   - "Track two maps: required (from target), have (from window)."
   - "Expand right until we have all characters."
   - "Then shrink left while still valid, updating minimum."

3. **State Tracking:**
   - `required`: count of each char in target
   - `formed`: count of unique chars in window that match required count
   - `window_counts`: current window's char counts

4. **Pseudocode (simplified):**
   ```
   required = Counter(t)
   L = 0, formed = 0
   min_len = inf, ans = ""
   
   for R in 0..s.length-1:
     char_R = s[R]
     window[char_R]++
     if char_R in required and window[char_R] == required[char_R]:
       formed++
     
     while formed == len(required) and L <= R:
       if R - L + 1 < min_len:
         min_len = R - L + 1
         ans = s[L:R+1]
       
       char_L = s[L]
       window[char_L]--
       if char_L in required and window[char_L] < required[char_L]:
         formed--
       L++
   
   return ans
   ```

5. **Trace:** s="ADOBECODEBANC", t="ABC"
   - Expand until we have all A, B, C
   - Shrink from left to find minimum
   - Answer: "BANC"

**Follow-up 2.2a:** "What's the time complexity?"
- **Strong:** "O(|s| + |t|) because each character is visited by L and R exactly once."

**Follow-up 2.2b:** "Can you optimize space?"
- **Strong:** "For fixed alphabets (e.g., 26 letters), use int[26] instead of HashMap, reducing constant factors."

**Rubric:**
- ✅ **Strong:** Correct algorithm, handles all cases, explains formed/required tracking
- ⚠️ **Hire:** Correct approach, minor bugs fixed during discussion
- ❌ **No Hire:** Confused state tracking, incorrect window conditions

---

### QUESTION BANK 3: PARENTHESES

#### Q3.1 (Easy): "Valid parentheses"

**What Interviewer Is Assessing:**
- ✅ Do you immediately recognize "stack"?
- ✅ Do you handle the matching logic correctly?
- ✅ Can you avoid the Counter trap (for multiple types)?

**Strong Answer (5-7 minutes):**

1. **Recognition:** "This is a classic stack problem (LIFO)."

2. **Approach:**
   - "Use a stack to track open brackets."
   - "For each close bracket, check if it matches the top of the stack."
   - "If stack is empty at the end, it's valid."

3. **Pseudocode:**
   ```
   stack = []
   map = { ')': '(', ']': '[', '}': '{' }
   for c in s:
     if c in map:  // closing bracket
       if stack.empty() or stack.top() != map[c]:
         return false
       stack.pop()
     else:
       stack.push(c)
   return stack.empty()
   ```

4. **Trace:** "([{}])" → correctly matched and stack empty

5. **Counter Warning:**
   - "Can't use a Counter for multiple types. '([)]' has balanced counts but is invalid."
   - "Stack guarantees LIFO matching."

**Follow-up 3.1a:** "What if all brackets are the same type '(())'?"
- **Strong:** "Could use a counter, but stack is more general."

**Rubric:**
- ✅ **Strong:** Mentions stack immediately, explains Counter limitation
- ⚠️ **Hire:** Correct logic, maybe doesn't mention Counter trap
- ❌ **No Hire:** Tries Counter approach or has matching logic errors

---

#### Q3.2 (Medium): "Generate parentheses"

**What Interviewer Is Assessing:**
- ✅ Do you recognize backtracking?
- ✅ Do you prune invalid branches early?
- ✅ Can you track open/close counts separately?

**Strong Answer (10-12 minutes):**

1. **Approach:** "Backtracking with pruning."

2. **Key Rules:**
   - Add '(' if `open_count < n`
   - Add ')' if `close_count < open_count`
   - Backtrack when both are exhausted

3. **Pseudocode:**
   ```
   def backtrack(open_count, close_count, current, result):
     if open_count == n and close_count == n:
       result.append(current)
       return
     if open_count < n:
       backtrack(open_count + 1, close_count, current + "(", result)
     if close_count < open_count:
       backtrack(open_count, close_count + 1, current + ")", result)
   
   result = []
   backtrack(0, 0, "", result)
   return result
   ```

4. **Trace:** n=2
   - "(" (open=1)
   - "()" (open=1, close=1)
   - ")()" (close=2) → STOP (can't add more)
   - "((" (open=2)
   - "(()" (open=2, close=1)
   - "(())" (open=2, close=2) → RESULT
   - ...result: ["(())", "()()"]

5. **Complexity:** O(4^n / sqrt(n)) (Catalan number)

**Follow-up 3.2a:** "Why does pruning work?"
- **Strong:** "We never explore invalid states (close > open) because the if condition prevents it."

**Rubric:**
- ✅ **Strong:** Correct backtracking, explains pruning, handles Catalan complexity mention
- ⚠️ **Hire:** Correct logic, maybe doesn't mention pruning explicitly
- ❌ **No Hire:** Tries to generate all 2^(2n) combinations without pruning

---

### QUESTION BANK 4: TRANSFORMATIONS

#### Q4.1 (Medium): "String to integer (atoi)"

**What Interviewer Is Assessing:**
- ✅ Do you handle overflow correctly?
- ✅ Do you think about the order of operations?
- ✅ Do you clarify edge cases?

**Strong Answer (10-12 minutes):**

1. **Clarification:**
   - "Do we handle 32-bit or 64-bit overflow?" → 32-bit (INT_MIN/MAX)
   - "Do we handle leading spaces?" → Yes
   - "Do we handle '+'?" → Yes

2. **Approach:**
   - "Skip leading whitespace."
   - "Handle optional '+' or '-'."
   - "Extract digits and build number."
   - "Check overflow before multiplying."

3. **Overflow Check (KEY):**
   - "Instead of `result * 10 + digit > INT_MAX`, use `result > (INT_MAX - digit) / 10` to avoid overflow."

4. **Pseudocode:**
   ```
   i = 0
   sign = 1
   result = 0
   
   while i < len(s) and s[i] == ' ':
     i++
   if s[i] in "+-":
     sign = -1 if s[i] == '-' else 1
     i++
   
   while i < len(s) and isDigit(s[i]):
     digit = s[i] - '0'
     if result > (INT_MAX - digit) / 10:
       return INT_MAX if sign > 0 else INT_MIN
     result = result * 10 + digit
     i++
   
   return sign * result
   ```

5. **Trace:** "  +0 43" → 0 (stops at space after '0')

6. **Edge Cases:**
   - "999999999999999999" → INT_MAX (overflow)
   - "   " → 0 (no digits)
   - "+abc" → 0 (no digits after sign)

**Follow-up 4.1a:** "Why can't we just use `result * 10 + digit > INT_MAX`?"
- **Strong:** "`result * 10` itself could overflow before we check, giving us undefined behavior."

**Follow-up 4.1b:** "What if the language has arbitrary-precision integers?"
- **Strong:** "We wouldn't need overflow checking; result would just grow. But for C/Java (fixed-size ints), we need the check."

**Rubric:**
- ✅ **Strong:** Correct overflow check, handles all edge cases, explains the algebra
- ⚠️ **Hire:** Correct logic, maybe overcomplicated or misses one edge case
- ❌ **No Hire:** Incorrect overflow handling or doesn't check at all

---

## 🏆 INTERVIEW RUBRIC (How Interviewers Score)

### Scoring Breakdown

| Category | Weight | Strong | Hire | No Hire |
|----------|--------|--------|------|---------|
| **Correctness** | 40% | Correct on all test cases, edge cases | 1-2 edge case misses | Multiple failures |
| **Approach** | 25% | Optimal approach, explains choice | Good approach, minor trade-off misses | Brute force or inefficient |
| **Code Quality** | 15% | Clean, readable, no bugs | Works but slightly messy | Hard to follow, bugs |
| **Communication** | 15% | Explains clearly, asks clarifications | OK explanation, minor gaps | Unclear or no explanation |
| **Follow-ups** | +10% | Handles all, discusses trade-offs | Answers most | Struggles or silent |

### Example Scoring

**Candidate A (Strong Hire):**
- Palindrome problem: Correct logic (40/40), Optimal approach (25/25), Clean code (15/15), Explains well (15/15), Handles follow-ups (+10) = 105/100 → **STRONG HIRE**

**Candidate B (Hire):**
- Palindrome problem: Correct but one edge case missed (35/40), Good approach (24/25), Code OK (12/15), Explains OK (13/15), Answers some follow-ups (+5) = 89/100 → **HIRE**

**Candidate C (No Hire):**
- Palindrome problem: Logic errors (20/40), Brute force approach (10/25), Messy code (8/15), Hard to follow (8/15) = 46/100 → **NO HIRE**

---

## 🎤 COMMUNICATION TIPS

### What to Say (Good)
- "Let me think through this..."
- "Can I clarify whether...?"
- "I think the optimal approach is... because..."
- "Here's how I'd trace through this example..."
- "One trade-off to consider..."

### What NOT to Say (Bad)
- "I'm not sure..." (then say nothing)
- "Um... uh... hmm..." (long silence)
- "Let me look this up in my head..." (sounds uncertain)
- "This is hard." (no context)

### If You Get Stuck
1. **Admit:** "I'm not immediately seeing the solution. Can I think for a moment?"
2. **Reason:** "Here's what I know... what I'm stuck on is..."
3. **Ask:** "Can I use a hint?" or "Should I try a different approach?"
4. **Recover:** Move to pseudocode, discuss complexity, ask follow-ups.

---

## 🛠️ PRE-INTERVIEW CHECKLIST (24 HOURS BEFORE)

- [ ] Solve 1 easy problem from each day (4 total) without looking at solutions
- [ ] Trace through 2 examples by hand (not on IDE)
- [ ] Review Support File 1 (Quick Reference Card)
- [ ] Review your "mistake journal" from practice
- [ ] Sleep well; eat breakfast
- [ ] Have water + light snack during interview

---

**Last Updated:** Dec 31, 2025  
**Format:** Comprehensive interview prep  
**Use:** Review 1-2 days before the interview