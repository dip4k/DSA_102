# 🧠 Advanced String Pattern Matching Algorithms (Concepts + Traces + Code)

> **Goal:** Build intuition for the major “advanced” string matching families, with **index-by-index traces** and **C# + Python snippets**.

---

## ✅ Summary list
- 🧩 **KMP / Prefix function**: linear-time single-pattern matching via fallback table. [web:130][web:132]
- 🧷 **Z-algorithm**: linear-time preprocessing that makes pattern matching easy on `P + '#' + T`. [web:152]
- 🧮 **Rabin–Karp / Rolling hash**: hash windows to find candidates quickly; verify on hash hits. [web:159][web:166]
- 🦘 **Boyer–Moore / Horspool**: compare from the end and skip ahead using shift rules. [web:173][web:191]
- 🌲 **Aho–Corasick**: multi-pattern matching using a trie + failure links. [web:153][web:158]
- 🧱 **Suffix array (+ LCP)**: sorted suffixes allow fast substring queries and many analytics tasks. [web:168]
- 🤖 **Suffix automaton**: a compact automaton for substring existence and many substring queries. [web:151]
- ⚡ **Bitap / Shift-Or**: bit-parallel matching for short patterns (and approximate matching variants). [web:175]
- 🪞 **Manacher**: specialized “pattern matching” for palindromes (find palindromic substrings in linear time). [web:206]

---

## 🧭 Quick decision guide (what to use when)

| Need | Use | Why |
|---|---|---|
| Single pattern, many searches, must be O(n) worst-case | ✅ KMP | Deterministic linear scan with fallback table. [web:132] |
| Single pattern, want a simpler linear preproc than KMP | ✅ Z algorithm | Z-array on `P#T` gives matches where Z == |P|. [web:152] |
| Single pattern, average fast, ok with probabilistic collisions + verify | ✅ Rabin–Karp | Rolling hash compares windows quickly. [web:166][web:159] |
| Pattern is relatively long, alphabet large, want fast skips in practice | ✅ Boyer–Moore / Horspool | Compares from end and jumps forward. [web:173][web:191] |
| Many patterns at once (dictionary matching) | ✅ Aho–Corasick | One pass over text with automaton transitions. [web:153] |
| Lots of substring queries / lexicographic tasks | ✅ Suffix array | Sorted suffixes + LCP enable fast searches and analytics. [web:168] |
| Many substring checks, count distinct substrings, etc. | ✅ Suffix automaton | Traverse transitions to test substring presence. [web:151] |
| Very short patterns (fit in machine word bits) | ✅ Bitap | Uses fast bit operations; predictable for short m. [web:175] |
| Palindromic substring tasks | ✅ Manacher | Linear-time palindrome radii computation. [web:206] |

---

# 1) 🧩 KMP (Prefix function / LPS)

## Why
KMP avoids re-checking characters by using a precomputed fallback table (LPS / prefix function). [web:132]

## What
- `lps[i]` = length of longest proper prefix of `P[0..i]` that is also a suffix. [web:132]

## How (step/flow)
- Build `lps[]` for pattern.
- Scan text with `(ti, pi)`; on mismatch, set `pi = lps[pi-1]` instead of resetting to 0. [web:132]

## Trace: matching step (tiny)
Pattern `P = "abab"`, lps = `[0,0,1,2]`
Text    `T = "abacabab"`

We trace `(ti, pi)` moves (pi = index in pattern):

```
T: a b a c a b a b
   0 1 2 3 4 5 6 7
P: a b a b
   0 1 2 3

Start: ti=0, pi=0
```

| Step | ti | T[ti] | pi | P[pi] | Result | Next (ti,pi) |
|---:|---:|:---:|---:|:---:|:---|:---|
| 1 | 0 | a | 0 | a | match | (1,1) |
| 2 | 1 | b | 1 | b | match | (2,2) |
| 3 | 2 | a | 2 | a | match | (3,3) |
| 4 | 3 | c | 3 | b | mismatch → fallback pi=lps[2]=1 | (3,1) |
| 5 | 3 | c | 1 | b | mismatch → fallback pi=lps[0]=0 | (3,0) |
| 6 | 3 | c | 0 | a | mismatch → advance ti | (4,0) |
| 7 | 4 | a | 0 | a | match | (5,1) |
| 8 | 5 | b | 1 | b | match | (6,2) |
| 9 | 6 | a | 2 | a | match | (7,3) |
| 10 | 7 | b | 3 | b | match → pi==m => found at ti-m (=4) | (8,2) |

Note: after a match, KMP commonly sets `pi = lps[m-1]` to continue searching for overlaps. [web:132]

## C# snippet (core)
```csharp
static int[] BuildLps(string p)
{
    if (string.IsNullOrEmpty(p)) return Array.Empty<int>();

    int n = p.Length;
    int[] lps = new int[n];

    int len = 0;
    int i = 1;
    while (i < n)
    {
        if (p[i] == p[len])
        {
            len++;
            lps[i] = len;
            i++;
        }
        else if (len != 0)
        {
            len = lps[len - 1];
        }
        else
        {
            lps[i] = 0;
            i++;
        }
    }

    return lps;
}
```

## Python snippet (core)
```python
def build_lps(p: str) -> list[int]:
    if not p:
        return []
    lps = [0] * len(p)
    length = 0
    i = 1
    while i < len(p):
        if p[i] == p[length]:
            length += 1
            lps[i] = length
            i += 1
        elif length != 0:
            length = lps[length - 1]
        else:
            lps[i] = 0
            i += 1
    return lps
```

## Pitfalls
- Advancing `i` during fallback in LPS build.
- Mixing up “length” vs “index”.

## Tips and tricks
- Print `(i, len, p[i], p[len])` when debugging.

## Edge cases ✅
- Empty pattern: define as “match everywhere” or “no-op”; be explicit.

---

# 2) 🧷 Z Algorithm

## Why
Z-array provides a fast way to know how much of the prefix matches at each position, enabling pattern matching in linear time. [web:152]

## What
`z[i]` = length of the longest substring starting at `i` that matches the prefix of the string. [web:152]

## How (step/flow)
For pattern search, build:
```
S = P + '#' + T
```
A match of P at text position k corresponds to a position in S where `z[pos] == |P|`. [web:152]

## Trace: building z[] (small)
Let `S = "abacaba"`

```
idx: 0 1 2 3 4 5 6
S:   a b a c a b a
z:   0 ? ? ? ? ? ?
```

We compute `z[i]` by comparing `S[0..]` with `S[i..]`:
- i=1: compare `a` vs `b` → z[1]=0
- i=2: compare `a` vs `a` (1), then `b` vs `c` stop → z[2]=1
- i=3: `a` vs `c` → 0
- i=4: compare `a b a` with `a b a` → z[4]=3
- i=5: `a` vs `b` → 0
- i=6: `a` vs `a` → 1

So:
```
z = [0,0,1,0,3,0,1]
```

## C# snippet (simple Z build; good for learning)
```csharp
static int[] BuildZ(string s)
{
    if (string.IsNullOrEmpty(s)) return Array.Empty<int>();

    int n = s.Length;
    int[] z = new int[n];

    int l = 0, r = 0;
    for (int i = 1; i < n; i++)
    {
        if (i < r) z[i] = Math.Min(r - i, z[i - l]);

        while (i + z[i] < n && s[z[i]] == s[i + z[i]])
            z[i]++;

        if (i + z[i] > r)
        {
            l = i;
            r = i + z[i];
        }
    }

    return z;
}
```
This uses the standard [l, r) “Z-box” optimization described in common Z-function explanations. [web:152]

## Python snippet
```python
def build_z(s: str) -> list[int]:
    if not s:
        return []
    n = len(s)
    z = [0] * n
    l = r = 0
    for i in range(1, n):
        if i < r:
            z[i] = min(r - i, z[i - l])
        while i + z[i] < n and s[z[i]] == s[i + z[i]]:
            z[i] += 1
        if i + z[i] > r:
            l, r = i, i + z[i]
    return z
```

## Pitfalls
- Confusing r as inclusive vs exclusive; above uses `[l, r)`.

## Tips and tricks
- Z is excellent for “pattern in text” and string similarity tasks.

## Edge cases ✅
- All same char (e.g., "aaaaa") causes big z-values—good stress test.

---

# 3) 🧮 Rabin–Karp (Rolling Hash)

## Why
You compare hashes of length-m windows to find candidate matches quickly, then verify to avoid false positives. [web:166]

## What
Compute:
- hash(pattern)
- rolling hashes of text windows of the same length (rolling update from previous window). [web:166]

## How (step/flow)
```
window = T[i..i+m-1]
if hash(window) == hash(P): verify characters
```

## Trace: rolling window moves (concept)
Text `T = "abcd..."`, pattern length m=3

Window positions:
```
[abc]d...
 a[bcd]...
 ab[cde]...
```
The key is updating hash from window i to i+1 in O(1) rather than rehashing. [web:166][web:159]

## C# snippet (teaching version; uses modular rolling hash)
```csharp
static List<int> RabinKarp(string text, string pat)
{
    var res = new List<int>();
    if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(pat)) return res;
    if (pat.Length > text.Length) return res;

    const long mod = 1_000_000_007;
    const long baseB = 911382323; // any fixed base; for teaching only

    int n = text.Length, m = pat.Length;

    long Pow(long a, long e)
    {
        long r = 1;
        while (e > 0)
        {
            if ((e & 1) == 1) r = (r * a) % mod;
            a = (a * a) % mod;
            e >>= 1;
        }
        return r;
    }

    long basePow = Pow(baseB, m - 1);

    long Hash(string s)
    {
        long h = 0;
        for (int i = 0; i < s.Length; i++)
            h = (h * baseB + s[i]) % mod;
        return h;
    }

    long hp = Hash(pat);
    long ht = Hash(text.Substring(0, m));

    bool VerifyAt(int i)
    {
        for (int k = 0; k < m; k++)
            if (text[i + k] != pat[k]) return false;
        return true;
    }

    for (int i = 0; i <= n - m; i++)
    {
        if (ht == hp && VerifyAt(i)) res.Add(i);

        if (i < n - m)
        {
            // roll: remove text[i], add text[i+m]
            long left = (text[i] * basePow) % mod;
            ht = (ht - left) % mod;
            if (ht < 0) ht += mod;
            ht = (ht * baseB + text[i + m]) % mod;
        }
    }

    return res;
}
```
Rabin–Karp proceeds by hashing and using rolling updates across windows, then verifying on hash equality. [web:166]

## Python snippet (teaching version)
```python
def rabin_karp(text: str, pat: str) -> list[int]:
    if not text or not pat or len(pat) > len(text):
        return []

    mod = 1_000_000_007
    baseB = 911382323

    n, m = len(text), len(pat)

    def modpow(a, e):
        r = 1
        while e:
            if e & 1:
                r = (r * a) % mod
            a = (a * a) % mod
            e >>= 1
        return r

    basePow = modpow(baseB, m - 1)

    def h(s):
        v = 0
        for ch in s:
            v = (v * baseB + ord(ch)) % mod
        return v

    hp = h(pat)
    ht = h(text[:m])

    out = []
    for i in range(0, n - m + 1):
        if ht == hp and text[i:i+m] == pat:
            out.append(i)
        if i < n - m:
            left = (ord(text[i]) * basePow) % mod
            ht = (ht - left) % mod
            ht = (ht * baseB + ord(text[i + m])) % mod
    return out
```

## Pitfalls
- Forgetting the final verification step on hash match. [web:166]
- Weak hash settings cause more collisions.

## Tips and tricks
- In practice, double hashing reduces collision probability.

## Edge cases ✅
- Pattern longer than text → no matches.

---

# 4) 🦘 Boyer–Moore (and Horspool)

## Why
It often runs fast in practice by comparing from the **end** of the pattern and skipping forward using preprocessed shift information. [web:173]

## What
Boyer–Moore compares from right-to-left and shifts the pattern by multiple characters on mismatch. [web:173]

## How (step/flow)
- Preprocess pattern for shift rules.
- Align pattern with text; compare from the end; shift on mismatch. [web:173]

## Trace: Horspool-style bad-character shift (small)
Text `T = "here is a simple example"`
Pattern `P = "example"`

Horspool idea: on mismatch, shift based on the text char aligned with the pattern’s last char (using a precomputed table). [web:191]

(We keep this trace conceptual because full Boyer–Moore has multiple rules; Horspool is the simpler “practical” version.) [web:191]

## C# snippet (Boyer–Moore–Horspool core)
```csharp
static int IndexOfHorspool(string text, string pat)
{
    if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(pat)) return -1;
    int n = text.Length, m = pat.Length;
    if (m > n) return -1;

    // shift table: default shift = m
    var shift = new Dictionary<char, int>();
    for (int i = 0; i < m - 1; i++)
        shift[pat[i]] = m - 1 - i;

    int iText = m - 1;
    while (iText < n)
    {
        int k = 0;
        while (k < m && pat[m - 1 - k] == text[iText - k]) k++;
        if (k == m) return iText - (m - 1);

        char c = text[iText];
        iText += shift.TryGetValue(c, out int sh) ? sh : m;
    }

    return -1;
}
```
Horspool is a simplified member of the Boyer–Moore family, using bad-character based shifts. [web:191]

## Python snippet
```python
def index_of_horspool(text: str, pat: str) -> int:
    if not text or not pat or len(pat) > len(text):
        return -1

    n, m = len(text), len(pat)
    shift = {pat[i]: m - 1 - i for i in range(m - 1)}

    i = m - 1
    while i < n:
        k = 0
        while k < m and pat[m - 1 - k] == text[i - k]:
            k += 1
        if k == m:
            return i - (m - 1)
        i += shift.get(text[i], m)

    return -1
```

## Pitfalls
- Worst-case performance still exists; it’s mainly a “fast in practice” tool. [web:173]

## Tips and tricks
- Great when you reuse the same pattern many times (preprocessing amortizes). [web:173]

---

# 5) 🌲 Aho–Corasick (multi-pattern)

## Why
You can search for many patterns at once by building an automaton from a trie and adding failure links. [web:153]

## What
Aho–Corasick builds a finite-state machine (trie + links) so mismatches jump to the next best suffix state without backtracking. [web:153][web:158]

## How (step/flow)
- Build trie of patterns.
- Build failure links.
- Scan text; follow transitions; report matches via output links. [web:158]

## Trace: tiny dictionary
Patterns: `he`, `she`, `his`, `hers` (classic)
Text: `ushers`

As you scan `u s h e r s`, the automaton state moves through trie edges; on missing edge, it follows failure links to keep “longest suffix that is also a prefix” matched. [web:153][web:158]

## C# snippet (shape of the traversal)
```csharp
// Teaching snippet: shows the traversal loop shape.
// Full AC implementation is longer (node struct, trie build, BFS failure links).
static void AhoCorasickScan(string text /*, Node root */)
{
    // Node v = root;
    for (int i = 0; i < text.Length; i++)
    {
        char c = text[i];
        // while (v != root && !v.Next.ContainsKey(c)) v = v.Fail;
        // if (v.Next.TryGetValue(c, out var to)) v = to;
        // else v = root;

        // for (var u = v; u != root; u = u.OutputLink)
        //    report all patterns ending at u
    }
}
```
The key AC idea is failure transitions that avoid backtracking and allow linear scanning with outputs. [web:153]

## Python snippet (same idea)
```python
# Teaching skeleton only
def aho_scan(text: str):
    # state = root
    for i, ch in enumerate(text):
        # while state != root and ch not in state.next: state = state.fail
        # state = state.next.get(ch, root)
        # emit matches from state (and output links)
        pass
```

## Pitfalls
- Forgetting output links (you’ll miss patterns that are suffixes of other patterns). [web:158]

## Tips and tricks
- AC is the go-to when you have a dictionary of patterns (spam/malware signatures, keyword scanning). [web:153]

---

# 6) 🧱 Suffix Array (+ LCP)

## Why
A suffix array sorts all suffixes of a string; then substring search becomes binary search over suffixes. [web:168]

## What
- `SA`: indices of suffixes in sorted order.
- `LCP`: longest common prefix lengths between adjacent suffixes.

## How (step/flow)
- Build SA for text `T`.
- To check pattern `P`, binary search the range of suffixes where prefix matches `P`.

## Trace: "banana"
All suffixes:
```
0 banana
1 anana
2 nana
3 ana
4 na
5 a
```
Sorted suffixes (SA):
```
5 a
3 ana
1 anana
0 banana
4 na
2 nana
```
This sorted order is what enables binary search for any pattern prefix. [web:168]

## C# snippet (pattern check via binary search, assuming SA exists)
```csharp
static bool StartsWithAt(string t, int pos, string p)
{
    if (pos + p.Length > t.Length) return false;
    for (int i = 0; i < p.Length; i++)
        if (t[pos + i] != p[i]) return false;
    return true;
}

// Teaching: binary-search range check using SA
static bool ContainsWithSA(string t, int[] sa, string p)
{
    if (string.IsNullOrEmpty(t) || sa == null || string.IsNullOrEmpty(p)) return false;

    int L = 0, R = sa.Length - 1;
    while (L <= R)
    {
        int mid = L + (R - L) / 2;
        int pos = sa[mid];

        // Compare t[pos..] with p lexicographically
        int cmp = 0;
        for (int i = 0; i < p.Length; i++)
        {
            if (pos + i >= t.Length) { cmp = -1; break; }
            char a = t[pos + i], b = p[i];
            if (a != b) { cmp = (a < b) ? -1 : 1; break; }
        }

        if (cmp == 0) return true;
        if (cmp < 0) L = mid + 1;
        else R = mid - 1;
    }

    return false;
}
```

## Python snippet (same idea)
```python
def contains_with_sa(t: str, sa: list[int], p: str) -> bool:
    if not t or sa is None or not p:
        return False
    L, R = 0, len(sa) - 1
    while L <= R:
        mid = (L + R) // 2
        pos = sa[mid]
        suf = t[pos:pos+len(p)]
        if suf == p:
            return True
        if suf < p:
            L = mid + 1
        else:
            R = mid - 1
    return False
```

## Pitfalls
- Building SA is non-trivial; often you use a library / tested implementation.

## Tips and tricks
- SA shines when you need many substring queries or lexicographic operations, not just one match. [web:168]

---

# 7) 🤖 Suffix Automaton

## Why
A suffix automaton can test whether a pattern appears as a substring by following transitions from the start state. [web:151]

## What
Build automaton for text `T` in linear time; then for pattern `P`, walk transitions by each character; failure means not a substring. [web:151]

## How (step/flow)
```
state = start
for ch in P:
  if no transition: not found
  else state = transition
```

## C# snippet (membership check shape)
```csharp
// Teaching snippet: assumes you already built a SAM with Next transitions.
// Node has Dictionary<char,int> Next; 0 is start.
static bool SamContains(string p, List<Dictionary<char,int>> next)
{
    if (string.IsNullOrEmpty(p) || next == null || next.Count == 0) return false;

    int v = 0;
    foreach (char ch in p)
    {
        if (!next[v].TryGetValue(ch, out int to))
            return false;
        v = to;
    }

    return true;
}
```
The substring existence check via transition-following is the standard SAM membership test. [web:151]

## Python snippet
```python
def sam_contains(p: str, nxt: list[dict[str,int]]) -> bool:
    if not p or not nxt:
        return False
    v = 0
    for ch in p:
        if ch not in nxt[v]:
            return False
        v = nxt[v][ch]
    return True
```

## Pitfalls
- SAM construction is subtle (clone states, suffix links). Use a known-good implementation.

## Tips and tricks
- Think of SAM as a “substring query engine,” not just a pattern matcher. [web:151]

---

# 8) ⚡ Bitap / Shift-Or (bit-parallel)

## Why
Bitap uses bit operations to simulate matching progress for short patterns extremely fast on typical machines. [web:175]

## What
Precompute a bitmask per character and update a bitset state each text character.

## How (step/flow) — exact Shift-Or idea
- Pattern length `m` must fit in machine word bits (commonly <= 63 for 64-bit with one spare bit).
- Maintain `state` bitset; after processing a character, check a specific bit to detect a match.

## Trace (tiny)
Pattern `P = "aba"` (m=3)
Text    `T = "ababa"`

You update `state` on each char; when the “match bit” becomes 0/1 (depending on variant), you found an occurrence.

(Implementation details vary by shift-or vs shift-and; this section is about the traversal mindset and bit-parallel state update.) [web:175]

## C# snippet (teaching exact shift-and style)
```csharp
static List<int> ShiftAndExact(string text, string pat)
{
    var res = new List<int>();
    if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(pat)) return res;

    int m = pat.Length;
    if (m > 63) return res; // lenient: refuse patterns too long for 64-bit mask

    // mask[c] has 1s where pat has c
    var mask = new Dictionary<char, ulong>();
    for (int i = 0; i < m; i++)
    {
        char c = pat[i];
        mask.TryGetValue(c, out ulong v);
        v |= 1UL << i;
        mask[c] = v;
    }

    ulong state = 0;
    ulong matchBit = 1UL << (m - 1);

    for (int i = 0; i < text.Length; i++)
    {
        mask.TryGetValue(text[i], out ulong mc);
        state = ((state << 1) | 1UL) & mc;
        if ((state & matchBit) != 0)
            res.Add(i - m + 1);
    }

    return res;
}
```
Bitap/Shift-And uses bit operations with precomputed masks to track matching progress. [web:175]

## Python snippet
```python
def shift_and_exact(text: str, pat: str) -> list[int]:
    if not text or not pat:
        return []
    m = len(pat)
    if m > 63:
        return []

    mask = {}
    for i, ch in enumerate(pat):
        mask[ch] = mask.get(ch, 0) | (1 << i)

    state = 0
    match_bit = 1 << (m - 1)
    out = []

    for i, ch in enumerate(text):
        mc = mask.get(ch, 0)
        state = ((state << 1) | 1) & mc
        if state & match_bit:
            out.append(i - m + 1)

    return out
```

## Pitfalls
- Pattern too long for the chosen word size.

## Tips and tricks
- This is ideal for short patterns and small alphabets; for long patterns, use KMP/BM/SA. [web:175]

---

# 9) 🪞 Manacher (palindromic pattern matching)

## Why
Manacher finds palindromic substrings (especially the longest palindromic substring) in linear time. [web:206]

## What
Compute palindrome radii around each center using symmetry reuse.

## How (step/flow)
Typical trick: transform string by inserting separators so every palindrome is odd-length.

Example:
```
S = "abba"
T = "#a#b#b#a#"
```
Now each center in T has a radius value.

## Trace (tiny)
For `S="aba"` → `T="#a#b#a#"`
- center at `b` expands to cover `#a#b#a#` (largest radius)

## C# snippet (skeleton)
```csharp
// Teaching skeleton: full Manacher is a bit longer.
// Key idea: keep current center C and right boundary R.
static int LongestPalindromeLength(string s)
{
    if (string.IsNullOrEmpty(s)) return 0;

    // Transform: #a#b#...
    var t = new System.Text.StringBuilder();
    t.Append('#');
    foreach (char c in s) { t.Append(c); t.Append('#'); }

    int n = t.Length;
    int[] p = new int[n];

    int C = 0, R = 0;
    int best = 0;

    for (int i = 0; i < n; i++)
    {
        int mir = 2 * C - i;
        if (i < R) p[i] = Math.Min(R - i, p[mir]);

        while (i - 1 - p[i] >= 0 && i + 1 + p[i] < n && t[i - 1 - p[i]] == t[i + 1 + p[i]])
            p[i]++;

        if (i + p[i] > R) { C = i; R = i + p[i]; }
        best = Math.Max(best, p[i]);
    }

    return best; // in transformed space; maps to original length
}
```
Manacher is known for linear-time palindromic substring processing. [web:206]

## Python snippet (skeleton)
```python
def manacher_longest_len(s: str) -> int:
    if not s:
        return 0

    t = ['#']
    for ch in s:
        t.append(ch)
        t.append('#')

    n = len(t)
    p = [0] * n
    C = R = 0
    best = 0

    for i in range(n):
        mir = 2 * C - i
        if i < R:
            p[i] = min(R - i, p[mir])

        while i - 1 - p[i] >= 0 and i + 1 + p[i] < n and t[i - 1 - p[i]] == t[i + 1 + p[i]]:
            p[i] += 1

        if i + p[i] > R:
            C, R = i, i + p[i]

        best = max(best, p[i])

    return best
```

## Pitfalls
- Forgetting the transform mapping when converting radius back to original indices.

## Tips and tricks
- Use Manacher when palindromes are the core; don’t force it for general pattern matching. [web:206]

---

# ✅ General edge cases checklist
- Empty pattern/text (define contract)
- Pattern longer than text
- Repeated characters (worst-case stress for naive methods)
- Unicode: decide if you match by code units, code points, or graphemes

---

## Next step
Tell me your real target workload:
- Single pattern once?
- Single pattern many times?
- Many patterns?
- Lots of substring queries?
And I’ll recommend the smallest subset of algorithms to truly master first.
