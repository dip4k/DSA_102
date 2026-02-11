# 🔍 String Traversal Deep Dives (No Drills)

C# 🟦 + Python 🐍  
Topics you chose:
- 🪟 Sliding window: **Exactly K distinct** built from **At most K distinct**
- 🎛️ State machine: **Quoted CSV-like fields** with escapes
- 🌍 Unicode: **Safe “character counting” strategy** for UI

---

## ✅ Summary list
- 🪟 Exactly K distinct = AtMost(K) − AtMost(K−1) (count windows).
- 🎛️ CSV parsing is traversal + state: Normal vs InQuotes vs Escape.
- 🌍 UI “character count” should be **grapheme/text-element** aware; raw indexing can miscount.

---

# 🪟 1) Sliding window: Exactly K distinct

## Why
“Exactly K distinct” is tricky to maintain directly; it’s easier and safer to count windows with **at most K distinct** and subtract.

## What
- `AtMost(K)` = number of substrings whose distinct character count ≤ K.
- `Exactly(K)` = `AtMost(K) - AtMost(K-1)`.

## How (step/flow)
### Key insight
If you fix `R` and maintain the smallest `L` such that window `[L..R]` has **≤ K** distinct, then:
- All substrings ending at `R` and starting anywhere in `[L..R]` are valid.
- Count added at this `R` is: `R - L + 1`.

### Visual
```
For each R:
[L................R] is the smallest valid window (≤K distinct)
All starts in L..R are valid => count += (R-L+1)
```

---

## A) Guided walk-through (index-by-index)

### Example
`s = "pqpqs"`, K = 2

We’ll compute:
- `AtMost(2)`
- `AtMost(1)`
- `Exactly(2) = AtMost(2) - AtMost(1)`

---

### Walk-through for AtMost(2)
Maintain:
- `L`, `R`
- `freq` map
- `distinct`
- `count` (total substrings)

String:
```
idx:  0 1 2 3 4
s:    p q p q s
```

Trace table:
| Step | R | s[R] | Action | L after shrink | distinct | Valid substrings added (R-L+1) | Total |
|---:|---:|:---:|:---|---:|---:|---:|---:|
| 1 | 0 | p | add p | 0 | 1 | 1 | 1 |
| 2 | 1 | q | add q | 0 | 2 | 2 | 3 |
| 3 | 2 | p | add p | 0 | 2 | 3 | 6 |
| 4 | 3 | q | add q | 0 | 2 | 4 | 10 |
| 5 | 4 | s | add s, distinct=3 → shrink | 3 | 2 | 2 | 12 |

How step 5 shrink happens:
- Window before shrink: `[0..4] = p q p q s` has 3 distinct.
- Move L until distinct ≤ 2:
  - remove p (L=0) → still 3
  - remove q (L=1) → still 3
  - remove p (L=2) → now only {q,s} → distinct=2, stop at L=3.

Result:
- `AtMost(2) = 12`

---

### Walk-through for AtMost(1)
Same logic, K=1.

Trace table:
| Step | R | s[R] | Action | L after shrink | distinct | Added | Total |
|---:|---:|:---:|:---|---:|---:|---:|---:|
| 1 | 0 | p | add p | 0 | 1 | 1 | 1 |
| 2 | 1 | q | add q (distinct=2) → shrink | 1 | 1 | 1 | 2 |
| 3 | 2 | p | add p (distinct=2) → shrink | 2 | 1 | 1 | 3 |
| 4 | 3 | q | add q (distinct=2) → shrink | 3 | 1 | 1 | 4 |
| 5 | 4 | s | add s (distinct=2) → shrink | 4 | 1 | 1 | 5 |

Result:
- `AtMost(1) = 5`

---

### Exactly(2)
```
Exactly(2) = AtMost(2) - AtMost(1) = 12 - 5 = 7
```

---

## B) C# code (lenient)

### AtMost(K) count
```csharp
static long CountSubstringsAtMostKDistinct(string s, int K)
{
    if (string.IsNullOrEmpty(s) || K <= 0) return 0;

    var freq = new Dictionary<char, int>();
    int distinct = 0;
    int L = 0;
    long count = 0;

    for (int R = 0; R < s.Length; R++)
    {
        char c = s[R];
        if (!freq.TryGetValue(c, out int cur) || cur == 0) distinct++;
        freq[c] = cur + 1;

        while (distinct > K)
        {
            char left = s[L];
            freq[left]--;
            if (freq[left] == 0) distinct--;
            L++;
        }

        count += (R - L + 1);
    }

    return count;
}

static long CountSubstringsExactlyKDistinct(string s, int K)
{
    if (string.IsNullOrEmpty(s) || K <= 0) return 0;
    return CountSubstringsAtMostKDistinct(s, K) - CountSubstringsAtMostKDistinct(s, K - 1);
}
```

### Tips and tricks
- Use `long` for counts (number of substrings can be ~ n²/2).
- Exactly-K by subtraction avoids complex “exactly” maintenance logic.

### Edge cases ✅
- K <= 0 → 0.
- K > number of distinct chars in s → Exactly(K) = 0.
- Very large strings → O(n) time but freq map can still be heavy; reduce key space if you know charset.

---

## C) Python code (clear + short)
```python
from collections import defaultdict

def at_most_k_distinct(s: str, k: int) -> int:
    if not s or k <= 0:
        return 0
    freq = defaultdict(int)
    distinct = 0
    L = 0
    total = 0

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

        total += (R - L + 1)

    return total

def exactly_k_distinct(s: str, k: int) -> int:
    if not s or k <= 0:
        return 0
    return at_most_k_distinct(s, k) - at_most_k_distinct(s, k - 1)
```

---

# 🎛️ 2) State machine: quoted CSV-like fields with escapes

## Why
CSV-like parsing fails with naive `Split(',')` because commas and quotes can appear inside quoted fields.

## What
We traverse with:
- `state = Normal` or `InQuotes`
- A field buffer
- Output list of fields

Escape policy (pick one; we’ll implement both patterns):
- **Double-quote escaping**: inside quotes, `""` means a literal `"`.
- **Backslash escaping**: inside quotes, `\"` means a literal `"`.

> We’ll implement **double-quote escaping** (closer to RFC-style CSV) and optionally accept backslash escapes too.

---

## How (step/flow)
### States
- **Normal**:
  - comma `,` ends the field
  - quote `"` starts quoted field
  - other chars go to field buffer
- **InQuotes**:
  - `""` → append `"` and continue in quotes
  - `"` → end quotes
  - other chars go to field buffer

### Visual trace snippet
```
Normal:  read until comma, or enter quotes
Quotes:  everything literal, until closing quote
```

---

## Guided example (index-by-index)
Input:
```
line = a,"b,c","d""e",f
idx:    0123456789....
```
Expected fields:
- `a`
- `b,c`
- `d"e`
- `f`

### Trace highlights (important turning points)
We track: `i`, `ch`, `state`, and action.

| i | ch | state before | action | state after | buffer | fields |
|---:|:---:|:---|:---|:---|:---|:---|
| 0 | a | Normal | append | Normal | a | [] |
| 1 | , | Normal | end field | Normal | (reset) | [a] |
| 2 | " | Normal | enter quotes | InQuotes |  | [a] |
| 3 | b | InQuotes | append | InQuotes | b | [a] |
| 4 | , | InQuotes | append (literal comma) | InQuotes | b, | [a] |
| 5 | c | InQuotes | append | InQuotes | b,c | [a] |
| 6 | " | InQuotes | exit quotes | Normal | b,c | [a] |
| 7 | , | Normal | end field | Normal | (reset) | [a, b,c] |
| 8 | " | Normal | enter quotes | InQuotes |  | [a, b,c] |
| 9 | d | InQuotes | append | InQuotes | d | ... |
| 10 | " | InQuotes | lookahead sees next is " => append literal quote, consume both | InQuotes | d" | ... |
| 12 | e | InQuotes | append | InQuotes | d"e | ... |
| 13 | " | InQuotes | exit quotes | Normal | d"e | ... |
| 14 | , | Normal | end field | Normal | reset | [a, b,c, d"e] |
| 15 | f | Normal | append | Normal | f | ... |
| end |  | Normal | flush field |  |  | [a, b,c, d"e, f] |

---

## C# code (lenient CSV-like parser)
```csharp
static List<string> ParseCsvLike(string line)
{
    var fields = new List<string>();
    if (line == null) return fields;          // LENIENT

    var sb = new System.Text.StringBuilder();
    bool inQuotes = false;

    int i = 0;
    while (i < line.Length)
    {
        char ch = line[i];

        if (!inQuotes)
        {
            if (ch == ',')
            {
                fields.Add(sb.ToString());
                sb.Clear();
                i++;
            }
            else if (ch == '"')
            {
                inQuotes = true;
                i++;
            }
            else
            {
                sb.Append(ch);
                i++;
            }
        }
        else
        {
            // InQuotes
            if (ch == '"')
            {
                // Double-quote escape: "" => literal "
                if (i + 1 < line.Length && line[i + 1] == '"')
                {
                    sb.Append('"');
                    i += 2;
                }
                else
                {
                    inQuotes = false;
                    i++;
                }
            }
            else if (ch == '\\')
            {
                // Optional: backslash escape inside quotes
                if (i + 1 < line.Length)
                {
                    sb.Append(line[i + 1]);
                    i += 2;
                }
                else
                {
                    // trailing backslash, lenient: treat as literal
                    sb.Append('\\');
                    i++;
                }
            }
            else
            {
                sb.Append(ch);
                i++;
            }
        }
    }

    // Flush last field
    fields.Add(sb.ToString());

    // LENIENT policy: if line ends while still inQuotes, we keep what we collected.
    return fields;
}
```

### Tips and tricks
- The parser is a traversal: every branch must advance `i`.
- Keep the buffer as a mutable builder, not repeated string concatenation.

### Edge cases ✅
- Empty line → returns `[""]` (single empty field) with this implementation.
- Trailing comma `"a,b,"` → last field becomes empty.
- Unclosed quotes `"a,\"b"` (missing end quote) → leniently returns collected buffer.
- Spaces around commas: decide whether they’re significant (CSV usually treats them as data unless you trim).

---

## Python code (same behavior)
```python
def parse_csv_like(line: str) -> list[str]:
    if line is None:
        return []

    fields = []
    buf = []
    in_quotes = False
    i = 0

    while i < len(line):
        ch = line[i]

        if not in_quotes:
            if ch == ',':
                fields.append(''.join(buf))
                buf.clear()
                i += 1
            elif ch == '"':
                in_quotes = True
                i += 1
            else:
                buf.append(ch)
                i += 1
        else:
            if ch == '"':
                if i + 1 < len(line) and line[i + 1] == '"':
                    buf.append('"')
                    i += 2
                else:
                    in_quotes = False
                    i += 1
            elif ch == '\\':
                if i + 1 < len(line):
                    buf.append(line[i + 1])
                    i += 2
                else:
                    buf.append('\\')
                    i += 1
            else:
                buf.append(ch)
                i += 1

    fields.append(''.join(buf))
    return fields
```

---

# 🌍 3) Unicode: safe “character counting” for UI

## Why
UI “character counts” typically mean **user-perceived characters** (grapheme clusters / text elements), not:
- UTF-16 code units (`char` in C# indexing)
- Unicode code points (Python iteration)

Examples where naive counting surprises users:
- Emoji with skin-tone modifier: may be multiple code points.
- Letters with combining marks: `e` + combining accent.
- Flag emoji sequences.

---

## What to count (pick your unit) 🎯
- **Unit A:** C# `char` count (`s.Length`) → counts UTF-16 code units.
- **Unit B:** Python code point count (`len(s)`) → counts Unicode code points.
- **Unit C:** UI text elements (grapheme clusters) → what users perceive as “characters.”

For UI, choose **Unit C**.

---

## C# strategy (recommended for UI): StringInfo text elements ✅

### Why
.NET provides APIs to work with a string as a series of **textual elements** rather than individual `char` units.

### What
- `StringInfo.LengthInTextElements` gives number of text elements.
- `StringInfo.GetTextElementEnumerator` lets you iterate text elements.

### How (step/flow)
```
Create StringInfo
Count LengthInTextElements
Or enumerate text elements to slice safely
```

### C# snippet: count text elements
```csharp
using System.Globalization;

static int UiCharCount(string s)
{
    if (string.IsNullOrEmpty(s)) return 0;
    return new StringInfo(s).LengthInTextElements;
}
```

### C# snippet: safe truncation by text elements
```csharp
using System.Globalization;

static string TruncateByUiChars(string s, int maxElements)
{
    if (string.IsNullOrEmpty(s) || maxElements <= 0) return "";

    var e = StringInfo.GetTextElementEnumerator(s, 0);
    int taken = 0;
    int lastIndex = 0;

    while (taken < maxElements && e.MoveNext())
    {
        // TextElementEnumerator exposes the element; we use indices to slice
        lastIndex = e.ElementIndex + e.GetTextElement().Length;
        taken++;
    }

    if (taken == 0) return "";
    if (lastIndex > s.Length) lastIndex = s.Length; // lenient clamp
    return s.Substring(0, lastIndex);
}
```

### Tips and tricks
- Use text-element counting only when UI correctness matters (it’s more work than code-unit counts).

### Edge cases ✅
- Empty/null string → 0.
- maxElements larger than available → returns original.

---

## Python strategy for UI counting

### Why
Python’s `len(s)` counts code points, not grapheme clusters; grapheme-aware counting typically needs a specialized approach.

### What
- If you only need code points: `len(s)` is fine.
- For UI grapheme clusters: use a library that follows Unicode grapheme cluster rules.

### How (step/flow)
- Decide if you truly need UI grapheme counts.
- If yes, use a grapheme-aware iterator.

### Python snippet (code point count)
```python
def code_point_count(s: str) -> int:
    return 0 if not s else len(s)
```

### Python pseudo-snippet (grapheme cluster count)
```python
# PSEUDO: requires a grapheme-aware library.
# count = sum(1 for _ in grapheme_clusters(s))
```

### Tips and tricks
- For UI text boxes, it’s often better to enforce limits by graphemes to match user expectations.

---

## ✅ Unicode debugging guide
- Test with strings containing:
  - combining marks: "e\u0301"
  - emoji sequences: "👩🏽‍🔬"
  - flags: "🇮🇳"
- Compare:
  - C# `s.Length`
  - UI text elements count (StringInfo)
  - Python `len(s)`

---

## Next step
If you want, we can add:
- ✅ “Minimum window substring” traced walk-through (coverage invariant)
- ✅ CSV state machine extended with CRLF, trimming policy, and strict vs lenient modes
- ✅ A unified “cursor toolkit” page: run-consume, anchor-cursor, window, state-machine templates
