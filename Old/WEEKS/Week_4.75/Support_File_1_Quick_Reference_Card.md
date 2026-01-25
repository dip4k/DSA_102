# 📋 SUPPORT FILE 1: QUICK REFERENCE CARD — WEEK 4.75 STRING MASTERY

**Purpose:** One-page condensed guide for all Week 4.75 topics (Days 1-4)  
**Use Case:** Quick lookup during problem-solving or revision  
**Format:** Emojis + minimal text for fast scanning

---

## 🎯 DECISION FLOWCHART AT A GLANCE

```
String Problem
├─ ✅ Boolean Validity?
│  ├─ Palindrome? → Two pointers inward + center expansion
│  ├─ Parentheses? → Stack or Counter (single type)
│  └─ Substring rule? → Sliding window
│
├─ 🔤 Transform/Build?
│  ├─ Reverse → Two pointers swap (O(1) space)
│  ├─ Parse number → atoi (overflow check!)
│  ├─ Concatenate loop → StringBuilder (NOT +=)
│  └─ Zigzag/Rotate → Pattern tracking
│
└─ 📊 Optimize?
   ├─ Longest valid → Stack or DP (indices)
   └─ Minimum remove → Two-pass or stack tracking
```

---

## 📋 CORE CONCEPTS QUICK TABLE

| 📌 Topic (Day) | 🎯 Core Problem | 🔧 Main Technique | ⏱️ Time | 💾 Space | 🔑 Trap |
|---|---|---|---|---|---|
| **Palindromes (D1)** | Is palindrome? Longest? | 2-pointers (valid), expand center (longest) | O(n²) worst | O(1) or O(n) | Even-length centers, mismatch branch |
| **Substrings (D2)** | Longest substring w/ constraint | Sliding window (shrink on invalid) | O(n) | O(min(A,N)) | A=alphabet; counter not enough for types |
| **Parentheses (D3)** | Valid? Generate? Longest? | Stack, Backtrack, Index stack | O(n) to O(4^n) | O(n) | Single mismatch needs controlled branch |
| **Transformations (D4)** | Parse int? Reverse? Build? | atoi (bounds), 2-ptr reverse, StringBuilder | O(n) | O(1) to O(n) | Overflow before check, Unicode multi-byte |

---

## 🚨 COMMON PITFALLS & FIXES

| ❌ Mistake | 🔍 Symptom | ✅ Fix |
|-----------|-----------|-------|
| **Counter for `()[]`** | Works for `()` but fails `([)]` | Use Stack (LIFO guarantees) |
| **Missing even centers** | Palindrome "abba" missed | Check both (i, i) and (i, i+1) |
| **Shrinking logic wrong** | Window never shrinks or overshrinks | `while (INVALID) { shrink_L; }` NOT `if` |
| **Overflow in atoi** | Result wraps silently | Check BEFORE multiply: `result > (MAX - digit) / 10` |
| **String concat in loop** | O(n²) performance | Use StringBuilder (Java), f-strings (Python), concat at end |
| **Forgetting bounds check** | Off-by-one errors | Always: L < R, L <= n-1, etc. |
| **UTF-8 byte reversal** | Emoji breaks: 😀 → (scrambled) | Reverse at character boundaries, not bytes |

---

## 💡 MNEMONIC DEVICES

**Palindromes: MIC**
- **M**irror: Inward pointers ↔
- **I**nside: Must be valid before outside
- **C**enter: Expand outward from centers

**Sliding Window: EV**
- **E**xpand right greedily (try longest)
- **V**alidate after each step; shrink if invalid

**Parentheses: LOC**
- **L**ast opened
- **O**pened deeply
- **C**losed first (LIFO / Stack)

**Transformations: ASB**
- **A**toi: Bounds checking
- **S**tringBuilder: Batch not loop
- **B**yte-aware: Encoding matters

---

## 🧊 ALGORITHM TEMPLATES (PSEUDOCODE)

### Template 1: Two-Pointer Inward (Validity)
```
Check(s, L=0, R=n-1):
  while L < R:
    if s[L] != s[R]:
      return false / handle mismatch
    L++, R--
  return true
```

### Template 2: Expand Around Center
```
ExpandCenter(s, center_L, center_R):
  while center_L >= 0 AND center_R < n AND s[center_L] == s[center_R]:
    center_L--
    center_R++
  return center_R - center_L - 1  // length
```

### Template 3: Sliding Window (Shrinkable)
```
Window(s):
  L=0, Ans=0, State={}
  for R in 0..n-1:
    Add s[R] to State
    while State is INVALID:
      Remove s[L] from State
      L++
    Ans = max(Ans, R - L + 1)
  return Ans
```

### Template 4: Stack Pattern (Matching)
```
ValidParentheses(s):
  Stack stack
  for c in s:
    if c is Open: stack.push(c)
    else:  // c is Close
      if stack.empty() OR stack.top() != match[c]:
        return false
      stack.pop()
  return stack.empty()
```

### Template 5: atoi with Overflow
```
StringToInteger(s):
  i=0, sign=1, result=0
  while i < len(s) AND s[i] == ' ': i++
  if s[i] in "+-": sign = -1 if s[i]=='-' else 1; i++
  while i < len(s) AND isDigit(s[i]):
    digit = s[i] - '0'
    if result > (INT_MAX - digit) / 10:
      return INT_MAX if sign>0 else INT_MIN
    result = result * 10 + digit
    i++
  return sign * result
```

---

## 📊 COMPLEXITY CHEAT SHEET

| Operation | 🟢 Best | 🟡 Typical | 🔴 Worst | Notes |
|-----------|---------|-----------|---------|-------|
| Valid palindrome | O(n) | O(n) | O(n) | Constant factors matter (skip spaces) |
| Longest palindromic substring (expand) | O(n) | O(n²) | O(n²) | Repetitive chars = worst |
| Valid parentheses (stack) | O(n) | O(n) | O(n) | Stack push/pop is O(1) |
| Sliding window | O(n) | O(n) | O(n) | Each char visited at most 2x |
| String reversal (in-place) | O(n) | O(n) | O(n) | Swaps O(n/2) |
| StringBuilder | O(n) amortized | O(n) | O(n) | Doubling hides single allocations |
| atoi | O(n) | O(n) | O(n) | Bounded by string length |

---

## 🎯 INTERVIEW PREP CHECKLIST

- [ ] Can you write valid parentheses in < 5 min?
- [ ] Do you immediately think "StringBuilder" for string loops?
- [ ] Can you explain overflow detection without looking it up?
- [ ] Do you check both (i, i) and (i, i+1) centers for palindromes?
- [ ] Can you differentiate substring (window) from subsequence (DP)?
- [ ] Do you know that Counter won't work for `([)]`?
- [ ] Can you implement atoi from scratch with all edge cases?
- [ ] Do you trace through examples before coding?

---

## 🔗 QUICK LINKS TO FULL FILES

1. **Week 4.75 Day 1:** Palindrome Patterns (Center expansion, Two-pointers, Delete-one)
2. **Week 4.75 Day 2:** Substring Sliding Window (Longest no-repeats, K-distinct, Permutation)
3. **Week 4.75 Day 3:** Parentheses & Brackets (Stack, Backtrack, Min-remove, Longest-valid)
4. **Week 4.75 Day 4:** String Transformations (atoi, Reverse, Zigzag, StringBuilder)

---

**Last Updated:** Dec 31, 2025  
**Difficulty:** 🟡 Medium (Tier 1.5-2.0 String Mastery)  
**Time to Review:** 5-10 minutes