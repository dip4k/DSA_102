# 🧪 SUPPORT FILE 3: EDGE CASES & DEBUGGING GUIDE — WEEK 4.75 STRING MASTERY

**Purpose:** Comprehensive edge case checklist + debugging strategies for string problems  
**Use Case:** Catch bugs before submitting; test robustness  
**Format:** Organized by topic, with test cases and expected outputs

---

## DAY 1: PALINDROMES — EDGE CASES

### Valid Palindrome (Strict)

| 🧪 Test Case | Input | Expected Output | 💡 Trap | ✅ Fix |
|---|---|---|---|---|
| Empty | `""` | `True` | Empty is palindrome | Return early if n==0 |
| Single char | `"a"` | `True` | Trivial | Covered by loop condition L < R |
| Two same | `"aa"` | `True` | Simple | L=0, R=1: L < R true, s[0]==s[1] true |
| Two different | `"ab"` | `False` | Early mismatch | L=0, R=1: mismatch, return false |
| All spaces | `"   "` | `True` | Palindrome (no chars) | Works naturally |
| Mixed case | `"Aa"` | Depends on rule | Not always palindrome | Define case sensitivity first |
| Punctuation | `"A.,a"` | Depends on rule | Must skip non-alphanumeric | Implement skip logic carefully |

### Valid Palindrome II (Delete One)

| 🧪 Test Case | Input | Expected Output | 💡 Trap | ✅ Fix |
|---|---|---|---|---|
| No deletion needed | `"abc"` | `False` | Skip branch logic | Helper only checks if remaining is valid |
| Delete first char | `"dcba"` | `False` | Even deleting first doesn't help | Correctly returns false |
| Delete middle | `"abcd"` | `False` | No single deletion helps | Both branches return false |
| Single char | `"a"` | `True` | Already palindrome | L < R fails immediately, return true |
| Length 2, same | `"aa"` | `True` | Trivial | L=0, R=1: match, both empty checks true |
| Length 2, different | `"ab"` | `True` | Delete either | Skip L or skip R both work |

### Longest Palindromic Substring

| 🧪 Test Case | Input | Expected Length | 💡 Trap | ✅ Fix |
|---|---|---|---|---|
| Empty | `""` | 0 | No centers to expand | Initialize bestLen=0, return "" |
| Single | `"a"` | 1 | One center at i=0 | Odd center expands, finds len=1 |
| Repeated chars | `"aaaa"` | 4 | O(n²) worst case | Expand from i=0,1,2,3; all expand fully |
| Even-length | `"abba"` | 4 | Only even center (i=1) | Check both odd (i,i) and even (i,i+1) |
| No palindrome > 1 | `"abc"` | 1 | Single chars only | Max length stays 1 |
| Multiple equal | `"babad"` | 3 | Can return any (bab or aba) | Track first found |

---

## DAY 2: SUBSTRING SLIDING WINDOW — EDGE CASES

### Longest Substring Without Repeating

| 🧪 Test Case | Input | Expected | 💡 Trap | ✅ Fix |
|---|---|---|---|---|
| Empty | `""` | 0 | No characters | Loop doesn't run, ans=0 |
| Single char | `"a"` | 1 | One character | R=0: add to set, ans=max(0,1)=1 |
| All unique | `"abcd"` | 4 | Never shrink | L stays 0, R reaches 3, ans=4 |
| All same | `"aaaa"` | 1 | Shrink aggressively | Each 'a' sees prior 'a', shrinks L |
| Early repeat | `"au"` | 1 | 'a' repeats at start | Shrink L past first 'a', get "u" |
| Long repetition | `"abcdefghijkl...xyz"` then repeat | 26 (ASCII) | Max length limited by alphabet | Correct |

### Longest Substring with K Distinct

| 🧪 Test Case | Input | K | Expected | 💡 Trap | ✅ Fix |
|---|---|---|---|---|---|
| K > distinct chars | `"ab"` | 3 | 2 | Map.size() never > K | Works fine |
| K = 0 | `"abc"` | 0 | 0 | Can't have any chars | Early loop never finds valid window |
| K = 1 | `"aabbc"` | 1 | 2 | Max consecutive same | Correctly limits to one char type |
| Shrink from left | `"ccaabbb"` | 2 | 5 ("aabbb") | Shrink until only 2 types remain | Check map.size() ≤ K |

---

## DAY 3: PARENTHESES & BRACKETS — EDGE CASES

### Valid Parentheses

| 🧪 Test Case | Input | Expected | 💡 Trap | ✅ Fix |
|---|---|---|---|---|
| Empty | `""` | `True` | No brackets = valid | Stack empty at end |
| Single open | `"("` | `False` | Unmatched | Stack not empty at end |
| Single close | `")"` | `False` | Nothing to close | Stack empty, attempt pop fails |
| Mismatched | `"([)]"` | `False` | Types swapped | Stack top '[' doesn't match ')' |
| Correct nesting | `"([])"` | `True` | Proper nesting | Stack empties correctly |
| Duplicates | `"(())"` | `True` | Nested same type | Stack handles multiple of same |

### Generate Parentheses (N=2)

| 🧪 Test Case | N | Expected | 💡 Trap | ✅ Fix |
|---|---|---|---|---|
| N = 0 | 0 | `[""]` | Empty result? | Return `[""]` (empty string is valid) |
| N = 1 | 1 | `["()"]` | Only one valid | Backtrack: add '(', then ')', done |
| N = 2 | 2 | `["(())", "()()"]` | 2 valid out of 2^4 combinations | Pruning eliminates invalid early |
| N = 3 | 3 | 5 strings (Catalan[3]=5) | Catalan growth is fast | Exponential but pruned heavily |

### Longest Valid Parentheses

| 🧪 Test Case | Input | Expected | 💡 Trap | ✅ Fix |
|---|---|---|---|---|
| Empty | `""` | 0 | No characters | Stack or DP returns 0 |
| No valid | `"((("` | 0 | All open | Stack never pops completely |
| All valid | `"(())"` | 4 | Entire string | DP/stack tracks full length |
| Mixed | `")()())"` | 4 (substring "()()" at 1-4) | Can't use from start if invalid | Index stack or DP needed |

---

## DAY 4: STRING TRANSFORMATIONS — EDGE CASES

### String to Integer (atoi)

| 🧪 Test Case | Input | Expected | 💡 Trap | ✅ Fix |
|---|---|---|---|---|
| Empty | `""` | 0 | No digits | Loop doesn't run, return 0 |
| Spaces only | `"   "` | 0 | Whitespace doesn't produce digit | i advances, loop exits, return 0 |
| No digits | `"+abc"` | 0 | Sign but no digits | Skip sign, no digits, return 0 |
| Leading zeros | `"00123"` | 123 | Zeros don't change value | Correct: 0*10+0*10+...+1*10+2*10+3 |
| INT_MAX | `"2147483647"` | 2147483647 | Edge of 32-bit | Correct with overflow check |
| INT_MAX+1 | `"2147483648"` | 2147483647 | Clamp to INT_MAX | Overflow check triggers correctly |
| INT_MIN | `"-2147483648"` | -2147483648 | Edge of 32-bit (negative) | Correct with sign handling |
| INT_MIN-1 | `"-2147483649"` | -2147483648 | Clamp to INT_MIN | Overflow check on negative |
| Leading spaces | `"  +42"` | 42 | Skip whitespace first | i advances correctly |
| Trailing text | `"42 with text"` | 42 | Stop at non-digit | Correct: '4', '2', space → stop |

### String Reversal

| 🧪 Test Case | Input | Expected | 💡 Trap | ✅ Fix |
|---|---|---|---|---|
| Empty | `""` | `""` | L starts at 0, R at -1, L < R false | Never enters loop |
| Single | `"a"` | `"a"` | L=0, R=0, L < R false | Never enters loop |
| Two same | `"aa"` | `"aa"` | L=0, R=1, swap, L++, R--, L < R false | One swap, then exit |
| ASCII | `"hello"` | `"olleh"` | Straightforward reversal | Pointer approach works |
| Unicode (Emoji) | `"😀ab"` | `"ba😀"` OR BROKEN? | 😀 is multi-byte in UTF-8 | Swap bytes, breaks emoji. Need char-level reversal |
| Null bytes | `"a\0b"` | `"b\0a"` | Null in middle | Depends on language; C strings break, but algorithm still swaps |

### ZigZag Conversion

| 🧪 Test Case | Input | N Rows | Expected | 💡 Trap | ✅ Fix |
|---|---|---|---|---|---|
| N = 1 | `"abcd"` | 1 | `"abcd"` | No zigzag, just linear | Single row, no direction change |
| Length < N | `"ab"` | 3 | `"ab"` | Characters in row 0, row 1, never row 2 | Works: only placed in valid rows |
| Exact fit | `"ABCDE"` | 3 | `"ADBEC"` | Rows: A,D; B,E; C. Concat: ADB EC | Correct |

---

## 🐛 DEBUGGING CHECKLIST

### For All Problems

- [ ] Did I handle empty input?
- [ ] Did I check single-character input?
- [ ] Did I trace through an example by hand first?
- [ ] Are my loop bounds correct (L < R vs L <= R)?
- [ ] Do I have any off-by-one errors?
- [ ] Did I check the final state of my data structure (stack empty, counter at zero)?

### For Palindromes

- [ ] Am I checking both odd (i, i) and even (i, i+1) centers?
- [ ] Did I normalize correctly (case, non-alphanumeric)?
- [ ] For "delete one": did I branch exactly once at first mismatch?
- [ ] Did I handle the symmetric property correctly?

### For Sliding Window

- [ ] Is my shrinking condition a while loop, not an if?
- [ ] Do I update the state correctly when adding/removing?
- [ ] Did I handle the case where L pointer doesn't move (all unique)?
- [ ] Did I update the answer after shrinking, not before?

### For Parentheses

- [ ] Did I use a Stack, not just a counter, for multiple types?
- [ ] Am I checking stack.empty() before calling stack.top()?
- [ ] Did I pop when closing matches, not before?
- [ ] Is the final check stack.empty() (or counter == 0)?

### For Transformations

- [ ] Did I check overflow before applying the operation?
- [ ] For reversal: am I swapping correctly (L and R, not L and L+1)?
- [ ] Did I handle the sign separately from the digits?
- [ ] For StringBuilder: did I append in a loop, not concatenate?
- [ ] For Unicode: did I handle multi-byte sequences (not byte-by-byte)?

---

## 🔧 DEBUGGING STRATEGIES

### Strategy 1: Trace by Hand
- Pick a small input (3-5 characters).
- Run through your algorithm step-by-step on paper.
- Does the output match expectations?

### Strategy 2: Test Boundaries
- Empty input, single char, length 2.
- Max size inputs (if algorithm is O(n²), test small max).
- Overflow cases, sign changes.

### Strategy 3: Add Print Statements
- Print state after each major operation.
- Print decision branches ("Took left branch", "Shrank window").
- Print final result and intermediate results.

### Strategy 4: Validate Invariants
- Assert that invariants hold at each step.
- Example: `assert(L <= R)` for two-pointer problems.
- Example: `assert(stack.size() <= window_size)` for stack problems.

### Strategy 5: Compare to Brute Force
- Implement a slow, obviously correct version.
- Compare outputs on random inputs.
- Find the first mismatch and debug that case.

---

**Last Updated:** Dec 31, 2025  
**Format:** Comprehensive edge case reference  
**Suggested Use:** Before each interview, scan relevant section; use during debugging