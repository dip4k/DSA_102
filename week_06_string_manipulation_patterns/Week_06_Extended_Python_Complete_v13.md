# Week 06 Extended Python Complete v13

Purpose: build Python fluency for string-specific patterns: palindromes, windows, parentheses, transformations, and pattern matching.

## Focus tags
- Must: palindrome checks, string sliding windows, bracket matching, parsing/building
- Should: Rabin-Karp, KMP comparison, string-builder discipline
- Optional: advanced matching and deep Unicode/performance concerns

## Pattern 1: palindrome with two pointers
```python
def is_palindrome(s):
    left, right = 0, len(s) - 1
    while left < right:
        while left < right and not s[left].isalnum():
            left += 1
        while left < right and not s[right].isalnum():
            right -= 1
        if s[left].lower() != s[right].lower():
            return False
        left += 1
        right -= 1
    return True
```

## Pattern 2: longest substring without repeating
```python
def longest_no_repeat(s):
    seen = {}
    left = 0
    best = 0
    for right, ch in enumerate(s):
        if ch in seen and seen[ch] >= left:
            left = seen[ch] + 1
        seen[ch] = right
        best = max(best, right - left + 1)
    return best
```

## Pattern 3: valid parentheses
```python
def valid_parentheses(s):
    match = {')': '(', ']': '[', '}': '{'}
    stack = []
    for ch in s:
        if ch in '([{':
            stack.append(ch)
        elif ch in match:
            if not stack or stack.pop() != match[ch]:
                return False
    return not stack
```

## Pattern 4: string building
```python
def compress_runs(s):
    if not s:
        return ''
    out = []
    count = 1
    for i in range(1, len(s) + 1):
        if i < len(s) and s[i] == s[i - 1]:
            count += 1
        else:
            out.append(s[i - 1])
            out.append(str(count))
            count = 1
    return ''.join(out)
```

## Pattern 5: Rabin-Karp
```python
def rabin_karp(text, pattern):
    if not pattern or len(pattern) > len(text):
        return []
    base = 257
    mod = 1_000_000_007
    m = len(pattern)
    high = pow(base, m - 1, mod)
    ph = th = 0
    for i in range(m):
        ph = (ph * base + ord(pattern[i])) % mod
        th = (th * base + ord(text[i])) % mod
    out = []
    for i in range(len(text) - m + 1):
        if ph == th and text[i:i+m] == pattern:
            out.append(i)
        if i + m < len(text):
            th = (th - ord(text[i]) * high) % mod
            th = (th * base + ord(text[i + m])) % mod
    return out
```

## Quick practice ladder
- Must: valid palindrome, longest substring without repeats, minimum window substring, valid parentheses, decode string
- Should: find all anagrams, permutation in string, atoi, run-length encoding, Rabin-Karp
- Optional: KMP, Boyer-Moore discussion, advanced Unicode-safe processing

## Common Python pitfalls
- O(n^2) concatenation in loops instead of list-plus-join.
- Forgetting to shrink window state when moving the left boundary.
- Hash-match false positives if Rabin-Karp results are not verified.
