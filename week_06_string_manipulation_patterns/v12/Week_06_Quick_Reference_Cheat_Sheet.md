# Week 06 Quick Reference & Cheat Sheet

**Week:** 06 | **Use:** During review sessions and interview prep | **Format:** Quick lookup

---

## 🎯 ALGORITHM QUICK REFERENCE

### Day 1: Palindrome Patterns

**Two-Pointer Check**
```
Input: "racecar"
left=0, right=6
Compare s[left] vs s[right], move inward
Time: O(n) | Space: O(1)
```

**Expand-Around-Center**
```
For each center (odd and even length):
  Expand while s[left] == s[right]
  Track longest
Time: O(n²) | Space: O(1)
```

**Key Equations:**
- Odd-length palindrome center: single character at index i
- Even-length palindrome center: between characters i and i+1
- Expansion boundary: check 0 ≤ left AND right < n

---

### Day 2: Substring Sliding Window

**Variable-Size Window**
```
right: expand to include new characters
left: contract when constraint breaks
Constraint check: at each step
Time: O(n) | Space: O(k) alphabet size
```

**Frequency Map Pattern**
```
charCount[char]++ when right moves
charCount[char]-- when left moves
Check if constraint satisfied (e.g., all keys in map)
```

**Key Invariants:**
- Window always maintains valid constraint
- Left pointer never moves backward
- Each character visited at most twice (once by right, once by left)

---

### Day 3: Parentheses & Bracket Matching

**Stack Validation**
```
push opening bracket
on closing bracket: check stack top matches type
if yes: pop; if no or empty: invalid
end: stack must be empty
Time: O(n) | Space: O(n) worst, O(d) typical (d=nesting depth)
```

**Bracket Type Map**
```
'(' ↔ ')'
'[' ↔ ']'
'{' ↔ '}'
```

**Key Principle:** LIFO (Last In First Out) is exact for nested structures

---

### Day 4: String Transformations

**String to Integer (atoi)**
```
1. Skip whitespace
2. Check for sign
3. Read digits
4. Check for overflow before multiplying: value > INT_MAX / 10
5. Stop at non-digit
```

**Integer to Roman**
```
Greedy table (descending, includes subtractive):
1000→M, 900→CM, 500→D, 400→CD, 100→C, 90→XC, 50→L, 40→XL, 10→X, 9→IX, 5→V, 4→IV, 1→I
While n > 0: subtract largest value, append symbol
```

**String Builder Pattern**
```
builder.append() instead of str + str
Time: O(n) amortized | Space: O(n)
Avoids O(n²) naive concatenation trap
```

---

### Day 5: Advanced String Matching

**Rolling Hash Update**
```
hash = ((hash - leftChar * basePower) mod modulus * base + rightChar) mod modulus
Precompute: basePower = base^(patternLen-1) mod modulus
Time per update: O(1)
```

**Collision Handling**
```
If hash matches: verify actual strings (O(m) verification)
Expected false positives: O(n/modulus) with large prime
```

**Algorithm Comparison:**
- Naive: O(nm)
- Rabin-Karp: O(n+m) average, O(nm) worst with collisions
- KMP: O(n+m) guaranteed
- Boyer-Moore: O(n/m) average for long patterns

---

## 🔍 PATTERN RECOGNITION FLOWCHART

```
Is the problem about STRINGS?

├─ PALINDROME-like?
│  ├─ Check if string is palindrome? → Two-pointer or expand-around
│  ├─ Find longest palindrome? → Expand-around-center
│  └─ Partition into palindromes? → Backtracking + DP

├─ SUBSTRING with constraints?
│  ├─ Variable constraints? → Sliding window (expand/contract)
│  ├─ Fixed-size match? → Fixed window (slide by one)
│  └─ Multiple patterns? → Rabin-Karp or Aho-Corasick

├─ BRACKET-like (nesting)?
│  ├─ Validate? → Stack (LIFO)
│  ├─ Find longest valid? → Stack indices or DP
│  └─ Generate all? → Backtracking

├─ TRANSFORMATION (format change)?
│  ├─ Parse/validate input? → Character-by-character with state
│  ├─ Greedy mapping? → Table-based approach
│  └─ Build/concatenate? → StringBuilder

└─ PATTERN MATCHING at scale?
   ├─ Multiple patterns? → Rabin-Karp or Aho-Corasick
   ├─ Large text? → Rolling hash
   └─ Approximate matching? → Rabin-Karp with threshold
```

---

## 📊 COMPLEXITY CHEAT SHEET

| Algorithm | Time | Space | When |
|-----------|------|-------|------|
| Two-pointer palindrome | O(n) | O(1) | Simple validation |
| Expand-around-center | O(n²) | O(1) | Find longest |
| Sliding window | O(n) | O(k) | Substring constraints |
| Stack bracket check | O(n) | O(n) | Validate nesting |
| atoi with overflow check | O(n) | O(1) | Parse integers |
| Integer to Roman | O(log n) | O(1) | Table-based greedy |
| Rolling hash | O(n+m) | O(1) | Multiple patterns |

---

## 🛑 COMMON PITFALLS & FIXES

| Pitfall | Problem | Fix |
|---------|---------|-----|
| Forgetting odd/even palindrome | Expand-around-center misses half | Check both single-char and two-char centers |
| Moving pointers backward | Sliding window invariant broken | Only move right, never backward |
| Empty stack pop | Crash/undefined behavior | Check stack.isEmpty() before pop |
| Integer overflow | Silent wraparound | Check before multiplying: val > INT_MAX/10 |
| Hash collision assumption | Wrong matches | Verify actual strings after hash match |
| O(n²) concatenation | Performance catastrophe | Use StringBuilder, never string + in loop |
| Off-by-one boundary | Wrong substring extracted | Use inclusive [left, right] or exclusive [left, right) consistently |
| Missing edge cases | Fails on empty/single element | Test "", "a", "ab", extreme lengths |

---

## 🧠 MENTAL MODELS

### Model 1: Two-Pointer Synchronization
"Two pointers moving in sync, maintaining an invariant as they go"
- Palindrome check: pointers move inward, characters must match
- Sliding window: pointers move rightward, constraint always satisfied

### Model 2: Stack as Memory for Nesting
"Stack remembers the most recent unmatched open, perfect for LIFO"
- Closing bracket pops the matching open
- Empty stack at end = all matched

### Model 3: Rolling Window as Sliding Hash
"Compute once, update incrementally as window slides"
- Remove old, shift left, add new in O(1)
- Perfect for pattern matching at scale

### Model 4: Greedy as Table Lookup
"Match largest first, repeatedly, until done"
- Works when structure allows (roman numerals, canonical forms)
- Verify by proving invariant maintained

---

## 📝 SOLUTION TEMPLATE BY DAY

### Day 1: Palindrome
```
def solve(s):
    # Option 1: Two-pointer
    left, right = 0, len(s) - 1
    while left < right:
        if s[left] != s[right]:
            return False
        left += 1
        right -= 1
    return True
    
    # Option 2: Expand-around-center
    max_len = 0
    for center in range(2 * len(s) - 1):
        left = center // 2
        right = left + center % 2
        while left >= 0 and right < len(s) and s[left] == s[right]:
            left -= 1
            right += 1
        max_len = max(max_len, right - left - 1)
    return max_len
```

### Day 2: Substring Sliding Window
```
def solve(s, constraints):
    left = 0
    freq = {}
    for right in range(len(s)):
        freq[s[right]] = freq.get(s[right], 0) + 1
        while not is_valid(freq, constraints):
            freq[s[left]] -= 1
            if freq[s[left]] == 0:
                del freq[s[left]]
            left += 1
        # Record result at [left, right]
    return result
```

### Day 3: Bracket Matching
```
def solve(s):
    stack = []
    pairs = {'(': ')', '[': ']', '{': '}'}
    for char in s:
        if char in pairs:  # Opening
            stack.append(char)
        else:  # Closing
            if not stack or pairs[stack.pop()] != char:
                return False
    return len(stack) == 0
```

### Day 4: String Transformation
```
def solve(s):
    result = []
    for char in s:
        # Transform char
        transformed = transform(char)
        result.append(transformed)
    return ''.join(result)  # Not += in loop
```

### Day 5: Rolling Hash
```
def solve(pattern, text):
    pattern_hash = compute_hash(pattern)
    text_hash = compute_hash(text[:len(pattern)])
    for i in range(len(text) - len(pattern) + 1):
        if text_hash == pattern_hash:
            if text[i:i+len(pattern)] == pattern:
                return i
        # Update rolling hash
        text_hash = update_hash(text_hash, text[i], text[i+len(pattern)])
    return -1
```

---

## ⚠️ BOUNDARY CONDITIONS CHECKLIST

- [ ] Empty string: ""
- [ ] Single character: "a"
- [ ] Two characters: "ab"
- [ ] All same characters: "aaaa"
- [ ] Alternating characters: "abab"
- [ ] Very long string: 10^6 characters
- [ ] Special characters: "!@#$%"
- [ ] Whitespace: "a b c"
- [ ] Numbers in string: "a1b2c3"

---

## 🚀 OPTIMIZATION CHECKLIST

- [ ] Can I avoid nested loops? (often → sliding window or hash map)
- [ ] Am I computing same thing multiple times? (→ memoization or precomputation)
- [ ] Is my space proportional to output? (if not, can optimize)
- [ ] Can I use a hash map to trade space for time?
- [ ] Can I precompute something to speed up queries?
- [ ] Is my string building causing O(n²) behavior?
- [ ] Am I missing a two-pointer or sliding window optimization?

---


