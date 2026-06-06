# 🔤 String Traversal Mastery (All Levels + Advanced)

> **Goal:** Master traversal and navigation on strings: cursor movement, boundaries, multi-pointer patterns, windows, parsing/state machines, Unicode-aware traversal, and performance-aware scanning.

---

## ✅ Summary list
- 🚶 **Level 1:** Basic cursor walk (forward), safe stopping.
- 🧮 **Level 2:** Index arithmetic (reverse, stride, wrap, slice thinking).
- 👀 **Level 3:** Multi-cursor traversal (two pointers, anchor+cursor, skip-runs).
- 🪟 **Level 4:** Range/window traversal (variable window with real invariants).
- 🧭 **Level 5:** Abstract traversal (state machine / search over positions).
- 🔄 **Level 6 (optional):** Enumerator-based traversal, low-index mental load.
- 🧩 **Level 7 (optional):** Token traversal patterns (lexing mindset).
- 🌍 **Level 8 (advanced):** Unicode-aware traversal (what is a “character”?).
- ⚙️ **Level 9 (advanced):** Performance traversal (allocation control, streaming mindset).

**Languages:** C# (primary) + Python (secondary)  
**No drills yet:** examples are for understanding patterns.

---

# Level 1 — Physical movement 🚶

## Pattern 1: Linear forward scan ➡️

### Why
Most bugs are movement bugs: off-by-one, missing increments, and reading past the end.

### What
Move a cursor `i` from left to right until you hit the end.

### How (step/flow)
```
s:   "a  b  c"
idx:  0  1  2
move: i→ i→ i→ stop
```

### C# snippet
```csharp
// Visit every char with index
static void Walk(string s)
{
    if (string.IsNullOrEmpty(s)) return; // lenient

    for (int i = 0; i < s.Length; i++)
        Console.WriteLine($"{i}: '{s[i]}'");
}
```

### Python snippet
```python
def walk(s: str) -> None:
    if not s:
        return
    for i in range(len(s)):
        print(i, repr(s[i]))
```

### Where and When (use cases)
- Counting, validation, first match, normalization scans.

### Common pitfalls
- Using `i <= len-1` instead of `i < len`.
- Dereferencing `s[i]` before confirming `i < len`.

### Tips and tricks
- Say the invariant out loud: “I only read `s[i]` when `0 <= i < length`.”

---

## Pattern 2: Sentinel stop (scan until condition) 🔍

### Why
Many tasks stop early (first digit, first delimiter, first invalid character).

### What
Use a `while` loop that stops when a condition is met or end is reached.

### How (step/flow)
```
while i < n and condition(s[i]) holds:
    i++
```

### C# snippet
```csharp
// Find first digit index, or -1
static int FirstDigitIndex(string s)
{
    if (string.IsNullOrEmpty(s)) return -1;

    int i = 0;
    while (i < s.Length && !char.IsDigit(s[i])) i++;

    return (i == s.Length) ? -1 : i;
}
```

### Python snippet
```python
def first_digit_index(s: str) -> int:
    if not s:
        return -1
    i = 0
    while i < len(s) and not s[i].isdigit():
        i += 1
    return -1 if i == len(s) else i
```

### Common pitfalls
- Forgetting `i += 1` in the loop body.

### Tips and tricks
- Order your condition safely: bounds check first, then `s[i]` checks.

---

## ✅ Level 1 invariants checklist
- Cursor `i` is always within `[0..n]`.
- Every read happens only when `i < n`.
- The loop always makes progress (`i` increases).

## 🪲 Level 1 debugging guide
- Print `i` and (if valid) `s[i]`.
- Infinite loop = cursor not advancing.
- Crash = bounds check missing.

---

# Level 2 — Index arithmetic 🧮

## Pattern 1: Reverse traversal ⬅️

### Why
Suffix checks and right-trimming scans become trivial.

### What
Start at `i = n-1` and move left.

### How (step/flow)
```
s:   "abcd"
idx:  0123
walk:    3→2→1→0
```

### C# snippet
```csharp
static int LastNonSpaceIndex(string s)
{
    if (string.IsNullOrEmpty(s)) return -1;

    for (int i = s.Length - 1; i >= 0; i--)
        if (!char.IsWhiteSpace(s[i])) return i;

    return -1;
}
```

### Python snippet
```python
def last_non_space_index(s: str) -> int:
    if not s:
        return -1
    for i in range(len(s) - 1, -1, -1):
        if not s[i].isspace():
            return i
    return -1
```

### Common pitfalls
- Using `i > 0` instead of `i >= 0` (skips index 0).

### Tips and tricks
- Reverse scans are great when “removing” characters logically from the end.

---

## Pattern 2: Stride traversal (i += k) 🦘

### Why
Sampling and periodic checks (every k-th char) are index-arithmetic problems.

### What
Increment by a step `k`.

### How (step/flow)
```
idx:   0 1 2 3 4 5 6
visit: ^   ^   ^   ^
step=2 => 0,2,4,6
```

### C# snippet
```csharp
static string EveryKth(string s, int step)
{
    if (string.IsNullOrEmpty(s) || step <= 0) return "";

    var sb = new System.Text.StringBuilder();
    for (int i = 0; i < s.Length; i += step)
        sb.Append(s[i]);

    return sb.ToString();
}
```

### Python snippet
```python
def every_kth(s: str, step: int) -> str:
    if not s or step <= 0:
        return ""
    return "".join(s[i] for i in range(0, len(s), step))
```

### Common pitfalls
- `step == 0` → infinite loop.

### Tips and tricks
- Validate `step > 0` as part of your traversal contract.

---

## Pattern 3: Wrap-around (circular indexing) 🔁

### Why
Useful for circular text (rotations), ring buffers, and cyclic comparisons.

### What
Use modulo to map any integer back into `[0..n-1]`.

### How (step/flow)
```
n=5
idx:  0 1 2 3 4
next: 0→1→2→3→4→0→...
```

### C# snippet
```csharp
static int Wrap(int i, int n)
{
    if (n <= 0) return 0;        // lenient
    int r = i % n;
    return (r < 0) ? r + n : r;
}
```

### Python snippet
```python
def wrap(i: int, n: int) -> int:
    if n <= 0:
        return 0
    return i % n  # Python % is already non-negative for n>0
```

### Common pitfalls
- `n == 0` modulo crash (guard it).
- Negative indices in languages where `%` can be negative.

### Tips and tricks
- Make wrap a dedicated function; it prevents subtle repeated bugs.

---

## ✅ Level 2 invariants checklist
- Every computed index is normalized to `[0..n-1]` before use.
- Stride loops guarantee step > 0.

## 🪲 Level 2 debugging guide
- Log visited indices (not only chars).
- For wrap: test with tiny `n` and negative offsets.

---

# Level 3 — Multi-cursor traversal 👀👀

## Pattern 1: Two pointers (L/R) ↔️

### Why
Best for symmetry checks and “pair from ends” logic.

### What
Maintain `L` and `R`, move inward.

### How (step/flow)
```
"racecar"
 L     R
```

### C# snippet
```csharp
static bool IsPalindromeAscii(string s)
{
    if (s == null) return false;
    int L = 0, R = s.Length - 1;

    while (L < R)
    {
        if (s[L] != s[R]) return false;
        L++; R--;
    }

    return true;
}
```

### Python snippet
```python
def is_palindrome(s: str) -> bool:
    if s is None:
        return False
    L, R = 0, len(s) - 1
    while L < R:
        if s[L] != s[R]:
            return False
        L += 1
        R -= 1
    return True
```

### Common pitfalls
- Not specifying whether to ignore spaces/case/punctuation.

### Tips and tricks
- If you ignore characters, do it with “skip while” loops on both ends.

---

## Pattern 2: Anchor + cursor (token boundaries) ⚓

### Why
Most parsing is “find a segment, process it, jump to next segment.”

### What
`start` marks the beginning of a token; `i` advances to token end.

### How (step/flow)
```
...  token_start ^......^ token_end
```

### C# snippet
```csharp
static List<string> WordsBySpaces(string s)
{
    var res = new List<string>();
    if (string.IsNullOrEmpty(s)) return res;

    int i = 0;
    while (i < s.Length)
    {
        while (i < s.Length && char.IsWhiteSpace(s[i])) i++;
        if (i == s.Length) break;

        int start = i;
        while (i < s.Length && !char.IsWhiteSpace(s[i])) i++;

        res.Add(s.Substring(start, i - start));
    }

    return res;
}
```

### Python snippet
```python
def words_by_spaces(s: str) -> list[str]:
    res = []
    if not s:
        return res
    i, n = 0, len(s)
    while i < n:
        while i < n and s[i].isspace():
            i += 1
        if i == n:
            break
        start = i
        while i < n and not s[i].isspace():
            i += 1
        res.append(s[start:i])
    return res
```

### Common pitfalls
- Missing the “skip separators” phase.

### Tips and tricks
- Always guarantee progress: either you consume separators or you consume a token.

---

## Pattern 3: Consume-runs (run-length mindset) 🧩

### Why
Runs (spaces, digits, letters) are everywhere; run-consumption is a universal traversal primitive.

### What
At each `i`, consume until the class changes; set `i = j`.

### How (step/flow)
```
"aaab  c"
 run1: [aaa]
 run2: [b]
 run3: [  ]
 run4: [c]
```

### C# snippet
```csharp
static string CollapseSpaces(string s)
{
    if (s == null) return "";
    if (s.Length == 0) return "";

    var sb = new System.Text.StringBuilder();
    int i = 0;

    while (i < s.Length)
    {
        if (s[i] != ' ')
        {
            sb.Append(s[i]);
            i++;
            continue;
        }

        while (i < s.Length && s[i] == ' ') i++; // consume run
        sb.Append(' ');
    }

    return sb.ToString();
}
```

### Python snippet
```python
def collapse_spaces(s: str) -> str:
    if s is None:
        return ""
    if s == "":
        return ""

    out = []
    i, n = 0, len(s)
    while i < n:
        if s[i] != " ":
            out.append(s[i])
            i += 1
        else:
            while i < n and s[i] == " ":
                i += 1
            out.append(" ")
    return "".join(out)
```

### Tips and tricks
- This pattern prevents “i++ scattered everywhere” mistakes.

---

## ✅ Level 3 invariants checklist
- In two-pointer loops: at least one pointer moves each iteration.
- In run/token loops: `i` always advances to `j` (end of consumed segment).

## 🪲 Level 3 debugging guide
- Log `(L, R)` or `(start, i)` and the substring you believe you consumed.

---

# Level 4 — Range/window traversal 🪟

## Pattern A: Variable sliding window (count-based invariant) 🧩

### Why
This is the most reusable string traversal: “longest/shortest substring under constraint.”

### What
Maintain window `[L..R]` and a state that can be updated by adding/removing one char.

### How (step/flow)
```
Include s[R]
While window invalid:
  Exclude s[L]; L++
Update best when valid
```

### C# snippet: Longest substring with at most K distinct
```csharp
static int LongestAtMostKDistinct(string s, int K)
{
    if (string.IsNullOrEmpty(s) || K <= 0) return 0;

    var freq = new Dictionary<char, int>();
    int distinct = 0;
    int best = 0;

    int L = 0;
    for (int R = 0; R < s.Length; R++)
    {
        char c = s[R];
        if (!freq.TryGetValue(c, out int cnt) || cnt == 0) distinct++;
        freq[c] = cnt + 1;

        while (distinct > K)
        {
            char left = s[L];
            freq[left]--;
            if (freq[left] == 0) distinct--;
            L++;
        }

        best = Math.Max(best, R - L + 1);
    }

    return best;
}
```

### Python snippet
```python
from collections import defaultdict

def longest_at_most_k_distinct(s: str, k: int) -> int:
    if not s or k <= 0:
        return 0

    freq = defaultdict(int)
    distinct = 0
    best = 0
    L = 0

    for R, ch in enumerate(s):
        if freq[ch] == 0:
            distinct += 1
        freq[ch] += 1

        while distinct > k:
            left = s[L]
            freq[left] -= 1
            if freq[left] == 0:
                distinct -= 1
            L += 1

        best = max(best, R - L + 1)

    return best
```

### Common pitfalls
- Forgetting to decrement `distinct` when a character count hits zero.

### Tips and tricks
- The only times `distinct` changes are at transitions **0→1** and **1→0**.

---

## Pattern B: Variable sliding window (coverage invariant) 🎯

### Why
Many problems require “window covers all required chars,” e.g., minimum window substring.

### What
Maintain required counts; window is valid when all required counts are met.

### C# snippet (simplified template)
```csharp
// Template: minimum window that covers required counts.
// You fill: need map, missing counter, and updates.
static int MinCoverWindowLength(string s, Dictionary<char,int> need)
{
    if (string.IsNullOrEmpty(s) || need == null || need.Count == 0) return 0;

    var have = new Dictionary<char,int>();
    int missing = need.Values.Sum(); // simple definition; can be refined

    int best = int.MaxValue;
    int L = 0;

    for (int R = 0; R < s.Length; R++)
    {
        char c = s[R];
        have.TryGetValue(c, out int hc);
        have[c] = hc + 1;

        if (need.TryGetValue(c, out int req) && hc < req) missing--;

        while (missing == 0)
        {
            best = Math.Min(best, R - L + 1);

            char left = s[L];
            int before = have[left];
            have[left] = before - 1;

            if (need.TryGetValue(left, out int lreq) && before <= lreq) missing++;
            L++;
        }
    }

    return best == int.MaxValue ? 0 : best;
}
```

### Tips and tricks
- Define “missing” precisely; update it only on the boundary conditions.

---

## ✅ Level 4 invariants checklist
- Window state matches exactly the characters in `[L..R]`.
- Shrink loop changes state + advances L every iteration.

## 🪲 Level 4 debugging guide
- Log `L, R, windowLen`, and your key metric (`distinct`, `missing`).

---

# Level 5 — Abstract traversal (state machine) 🧭

## Pattern: Parsing with states 🎛️

### Why
Once rules depend on context (quotes, escapes, numbers), cursor traversal becomes a state machine.

### What
Track `state` + cursor `i` and transition based on `s[i]`.

### How (step/flow)
```
State = Normal
if char == '"' -> InQuotes
if char == '\\' -> EscapeNext
...
```

### C# snippet: Extract integers (simple lexer)
```csharp
static List<int> ExtractInts(string s)
{
    var res = new List<int>();
    if (string.IsNullOrEmpty(s)) return res;

    int i = 0;
    while (i < s.Length)
    {
        while (i < s.Length && !char.IsDigit(s[i])) i++;
        if (i == s.Length) break;

        long val = 0;
        while (i < s.Length && char.IsDigit(s[i]))
        {
            val = val * 10 + (s[i] - '0');
            i++;
        }

        if (val > int.MaxValue) res.Add(int.MaxValue); // lenient clamp
        else res.Add((int)val);
    }

    return res;
}
```

### Python snippet
```python
def extract_ints(s: str) -> list[int]:
    res = []
    if not s:
        return res
    i, n = 0, len(s)
    while i < n:
        while i < n and not s[i].isdigit():
            i += 1
        if i == n:
            break
        val = 0
        while i < n and s[i].isdigit():
            val = val * 10 + (ord(s[i]) - ord('0'))
            i += 1
        res.append(min(val, 2**31 - 1))
    return res
```

### Tips and tricks
- State machines become easy when each state has exactly one “consume run” rule.

---

# Level 6 (optional) — Enumerator-based traversal 🔄

## Pattern: foreach traversal

### Why
If you don’t need indices, enumeration reduces off-by-one risk and boilerplate.

### C# snippet
```csharp
static int CountHexDigits(string s)
{
    if (string.IsNullOrEmpty(s)) return 0;

    int count = 0;
    foreach (char c in s)
        if (Uri.IsHexDigit(c)) count++;

    return count;
}
```

### Python snippet
```python
def count_hex_digits(s: str) -> int:
    if not s:
        return 0
    hexd = set("0123456789abcdefABCDEF")
    return sum(1 for ch in s if ch in hexd)
```

### Common pitfalls
- You later realize you need indices for slicing; then prefer `for i in range(...)` / `enumerate`.

---

# Level 7 (optional) — Token traversal (lexing mindset) 🧩

## Pattern: Tokenize identifiers and numbers

### Why
Many real-world tasks are token-level: identifiers, numbers, operators, whitespace.

### What
Repeatedly:
- skip whitespace
- read token run based on first char

### C# snippet (very small tokenizer)
```csharp
static List<string> SimpleTokens(string s)
{
    var res = new List<string>();
    if (string.IsNullOrEmpty(s)) return res;

    int i = 0;
    while (i < s.Length)
    {
        while (i < s.Length && char.IsWhiteSpace(s[i])) i++;
        if (i == s.Length) break;

        int start = i;
        if (char.IsLetter(s[i]) || s[i] == '_')
        {
            i++;
            while (i < s.Length && (char.IsLetterOrDigit(s[i]) || s[i] == '_')) i++;
        }
        else if (char.IsDigit(s[i]))
        {
            i++;
            while (i < s.Length && char.IsDigit(s[i])) i++;
        }
        else
        {
            i++; // single-char token
        }

        res.Add(s.Substring(start, i - start));
    }

    return res;
}
```

### Python snippet
```python
def simple_tokens(s: str) -> list[str]:
    res = []
    if not s:
        return res
    i, n = 0, len(s)
    while i < n:
        while i < n and s[i].isspace():
            i += 1
        if i == n:
            break
        start = i
        if s[i].isalpha() or s[i] == '_':
            i += 1
            while i < n and (s[i].isalnum() or s[i] == '_'):
                i += 1
        elif s[i].isdigit():
            i += 1
            while i < n and s[i].isdigit():
                i += 1
        else:
            i += 1
        res.append(s[start:i])
    return res
```

---

# Level 8 (advanced) — Unicode-aware traversal 🌍

## Why
A “user-perceived character” may be multiple code units/code points (e.g., emoji sequences, combining marks).

## What
You must choose your traversal unit:
- **Code units** (C# `char` indexing)
- **Code points** (Python `str` iteration is code point oriented)
- **Grapheme clusters** (what the user sees)

## How (step/flow)
- If you just need ASCII-like logic, code-unit traversal is fine.
- If you need correct user-visible character counts, consider grapheme traversal.

### C# tip
- If you need grapheme-aware processing, use a text-element approach (not raw `s[i]`).

### Python tip
- Python iterates by code points, but graphemes can still be multiple code points.

## Common pitfalls
- Slicing at arbitrary indices can split a grapheme cluster.

## Tips and tricks
- State your unit explicitly in your function contract: “counts UTF-16 code units” or “counts user-perceived characters”.

---

# Level 9 (advanced) — Performance traversal ⚙️

## Pattern 1: Avoid repeated concatenation 🧱

### Why
Repeated `result += ...` can lead to many intermediate allocations.

### What
Accumulate into a buffer (StringBuilder in C#, list-join in Python).

### C# snippet
```csharp
static string FilterLetters(string s)
{
    if (string.IsNullOrEmpty(s)) return s ?? "";

    var sb = new System.Text.StringBuilder(s.Length);
    for (int i = 0; i < s.Length; i++)
    {
        char c = s[i];
        if (char.IsLetter(c)) sb.Append(c);
    }
    return sb.ToString();
}
```

### Python snippet
```python
def filter_letters(s: str) -> str:
    if not s:
        return s or ""
    return "".join(ch for ch in s if ch.isalpha())
```

## Pattern 2: Streaming traversal mindset 📡

### Why
Sometimes the “string” is huge (file/network). Traversal becomes incremental (chunks).

### What
Process text in blocks; keep leftover partial token state between blocks.

### Tips and tricks
- Keep a small “carry” string for incomplete tokens at chunk boundaries.
- Make your parser/state machine resumable: `state + carry`.

---

# 🧠 Cross-level edge cases checklist ✅
- `null` / empty string
- all whitespace
- very long string
- delimiters at ends (leading/trailing)
- repeated delimiters (“runs”)
- mixed scripts/emoji (if Unicode correctness matters)

---

# ✅ What to deepen next (still no drills)
Pick one and I’ll expand with traced, index-by-index walkthroughs:
- 🪟 Sliding window: “exactly K distinct” built from “at most K distinct”
- 🎛️ State machine: quoted CSV-like fields with escapes
- 🌍 Unicode: safe “character counting” strategy for UI

---

# Advanced Pattern Matching Appendix (Merged)

Use this appendix as the single extension layer for pattern matching beyond cursor walking and windows.

## When to use what
- `KMP` for single-pattern matching with deterministic linear worst-case.
- `Rabin-Karp` for rolling-hash candidate filtering and repeated fixed-length pattern scans.
- `Z-algorithm` for prefix-match driven pattern search.
- `Trie` for prefix search, autocomplete, and dictionary-style matching.
- `Suffix array / suffix automaton` for substring-query heavy workloads.
- `Manacher` when the task is specifically about palindromic substrings.

## Drill ladder (merged)

### Must
- Valid palindrome / reverse vowels / remove consecutive duplicates
- Longest substring without repeating characters
- Longest substring with at most `k` distinct characters
- Anagram start indices / permutation inclusion

### Should
- Rabin-Karp all pattern matches
- KMP first occurrence / repeated substring pattern
- Z-algorithm all pattern matches / longest happy prefix
- Trie insert-search-prefix operations

### Optional advanced
- Manacher longest palindromic substring
- Suffix-array style substring reasoning
- Unicode- and performance-sensitive traversal scenarios

## KMP trace reminder
- Maintain a fallback table (`lps` / prefix function).
- On mismatch, fallback using prior prefix knowledge instead of restarting from zero.
- Debug by printing `(i, len)` or `(textIndex, patternIndex)` transitions.

This file is now the merged source for foundational string traversal, advanced pattern matching, drills, Unicode awareness, and performance guidance.
