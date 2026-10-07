# 📘 Week 06 Day 5: Advanced String Matching — Rabin-Karp & Rolling Hash — Engineering Guide

> 🧭 **Navigation:** [← Previous Day](Week_06_Day_04_String_Transformations_Building_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Week Playbook →](WEEK_06_FULL_PLAYBOOK.md)
> 
> 💡 **Instructor Note:** *Rabin-Karp turns string matching into rolling arithmetic. By treating substrings as base-B polynomials, sliding a window of length M takes O(1) time instead of O(M). Always verify matches character-by-character to eliminate hash collisions.*

---

## 🎯 LEARNING OBJECTIVES

By the end of this chapter, you will be able to:

- **Internalize** the rolling hash mechanism as incremental polynomial evaluation updated in `O(1)` time per slide.
- **Implement** production Rabin-Karp substring search with collision verification (Las Vegas guarantee).
- **Protect** against modular underflow using canonical remainder normalization: `((hash % MOD) + MOD) % MOD`.
- **Evaluate** algorithmic trade-offs between Rabin-Karp, Knuth-Morris-Pratt (KMP), and Boyer-Moore.
- **Deliver** a structured 45-minute technical interview script defending hash base selection and collision mitigation.

---

## 📖 CHAPTER 1: CONTEXT & MOTIVATION

### The Engineering Challenge

Pattern search across large text documents is foundational to infrastructure systems:
1. **Plagiarism & Code Clone Detection:** Scanning student submissions against a multi-gigabyte corpus requires searching for thousands of fixed-length sentences.
2. **Genomic Sequence Matching:** Searching for known viral or restriction patterns across genome sequences consisting of billions of base pairs.
3. **Network Signature Matching:** Deep-packet inspection firewalls scan streaming byte flows against known malicious signatures.

A naive matching approach checks `N - M + 1` window positions, testing each character-by-character in `O(M)` time, leading to `O(N * M)` worst-case complexity. For a 1-megabyte text and a 10,000-character pattern, that requires `10^10` character comparisons. Rabin-Karp reduces average-case matching to `O(N + M)` by comparing integer hash signatures in `O(1)` time.

> [!NOTE]
> **Interview & Systems Context:** In technical interviews and production search engines, Rabin-Karp excels when searching for **multiple patterns of the same length** simultaneously (by indexing pattern hashes in a hash set). For single-pattern searches, modern standard libraries use Boyer-Moore or vectorized SIMD instructions. Always implement full character verification on hash match (the Las Vegas approach) to guard against adversarial hash collision attacks.

---

## 🧠 CHAPTER 2: BUILDING THE MENTAL MODEL

### The Core Analogy: Rolling Digits in Base-10

Imagine sliding a 3-digit window across a number stream: `1 2 3 4 5`.
To slide the window from `123` to `234`:
1. Subtract the high-order digit: `123 - (1 * 10^2) = 23`.
2. Shift the remaining digits left by multiplying by the base: `23 * 10 = 230`.
3. Add the incoming right-hand digit: `230 + 4 = 234`.

This update occurs in `O(1)` independent of the window length `M`!

```
Base-B Polynomial Rolling Hash:
  Window S[0 .. M-1]:
    H = (S[0] * B^(M-1) + S[1] * B^(M-2) + ... + S[M-1] * B^0) mod MOD

  Sliding to S[1 .. M]:
    H_next = ( (H_prev - S[left] * B^(M-1)) * B + S[right] ) mod MOD
```

### Visualizing the Window Roll & Collision Verification

```
Text:     [ A   B   C ]   D   E      (M = 3, Base = 256, Mod = 10^9 + 7)
Pattern:  "BCD"

Step 1: Compute Pattern Hash: H("BCD") = 158,230
        Compute Window Hash:  H("ABC") = 142,110
        Compare: 142,110 != 158,230 -> Mismatch!

Step 2: Roll Window to [ B C D ]:
        Remove 'A':  (142,110 - 'A' * 256^2)
        Shift Base:  (...) * 256
        Add 'D':     (...) + 'D'  mod MOD = 158,230
        Compare: 158,230 == 158,230 -> HASH HIT!

Step 3: Verification (Las Vegas Check):
        text[1..3] == "BCD"? YES -> Match confirmed at index 1!
```

### String Matching Taxonomy & Comparisons

| Algorithm | Preprocessing Time | Search Time (Avg) | Search Time (Worst) | Auxiliary Space | Best Production Use Case |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Naive Search** | `O(1)` | `O(N * M)` | `O(N * M)` | `O(1)` | Short strings (`N < 50`) |
| **Rabin-Karp** | `O(M)` | `O(N + M)` | `O(N * M)` (collisions) | `O(1)` | Multiple pattern search of equal length |
| **Knuth-Morris-Pratt (KMP)** | `O(M)` | `O(N + M)` | `O(N + M)` | `O(M)` (LPS table) | Streaming text without lookahead |
| **Boyer-Moore** | `O(M + Sigma)` | `O(N / M)` (sub-linear) | `O(N * M)` | `O(Sigma)` | Long patterns in natural language |

---

## ⚙️ CHAPTER 3: MECHANICS & IMPLEMENTATION

### Operation 1: Precomputing Parameters

1. Choose a prime modulus `MOD` (e.g., `1_000_000_007` or `10^9 + 9`) and a base `BASE` larger than the alphabet (e.g., `256` for ASCII).
2. Precompute highest-order multiplier: `pow = BASE^(M-1) mod MOD`.
3. Compute initial hashes for pattern and first text window `text[0..M-1]`.

### Operation 2: The Sliding Loop & Modular Arithmetic

1. At each window index `i` from `0` to `N - M`:
   - If `textHash == patternHash`: verify substring character-by-character.
   - If `i < N - M`: roll hash to position `i + 1`:
     `textHash = (BASE * (textHash - text[i] * pow) + text[i + M]) % MOD`
     If `textHash < 0`: `textHash += MOD`.

---

### 💻 Production-Grade Implementations

#### C# (.NET 8/9 — Production Rabin-Karp with `ReadOnlySpan<char>`)

```csharp
using System;
using System.Collections.Generic;

public static class RabinKarpSolutions
{
    private const long Mod = 1_000_000_007;
    private const long Base = 256;

    /// <summary>
    /// Searches for all starting indices of pattern in text using Rabin-Karp.
    /// Time Complexity: O(N + M) Average | Space Complexity: O(1) Auxiliary
    /// </summary>
    public static List<int> Search(ReadOnlySpan<char> text, ReadOnlySpan<char> pattern)
    {
        List<int> matches = new();
        int n = text.Length;
        int m = pattern.Length;

        if (m == 0 || n < m) return matches;

        // Step 1: Precompute (Base^(m - 1)) % Mod
        long highPow = 1;
        for (int i = 0; i < m - 1; i++)
        {
            highPow = (highPow * Base) % Mod;
        }

        // Step 2: Compute initial hashes
        long patternHash = 0;
        long textHash = 0;

        for (int i = 0; i < m; i++)
        {
            patternHash = (patternHash * Base + pattern[i]) % Mod;
            textHash = (textHash * Base + text[i]) % Mod;
        }

        // Step 3: Slide window across text
        for (int i = 0; i <= n - m; i++)
        {
            // On hash match, perform exact character verification (Las Vegas)
            if (textHash == patternHash)
            {
                if (text.Slice(i, m).SequenceEqual(pattern))
                {
                    matches.Add(i);
                }
            }

            // Roll hash to next position
            if (i < n - m)
            {
                long removedCharContribution = (text[i] * highPow) % Mod;
                textHash = (textHash - removedCharContribution) % Mod;
                textHash = (textHash * Base + text[i + m]) % Mod;

                // Handle negative remainder
                if (textHash < 0)
                {
                    textHash += Mod;
                }
            }
        }

        return matches;
    }

    /// <summary>
    /// Finds the index of the first occurrence of needle in haystack (strStr).
    /// </summary>
    public static int StrStr(string haystack, string needle)
    {
        if (string.IsNullOrEmpty(needle)) return 0;
        if (haystack.Length < needle.Length) return -1;

        List<int> results = Search(haystack.AsSpan(), needle.AsSpan());
        return results.Count > 0 ? results[0] : -1;
    }
}
```

#### Python (3.11+ — Idiomatic & Clean)

```python
class RabinKarpSolutions:
    @staticmethod
    def search(text: str, pattern: str) -> list[int]:
        """
        Finds all occurrences of pattern in text using polynomial rolling hash.
        Time: O(N + M) Average | Auxiliary Space: O(1)
        """
        n, m = len(text), len(pattern)
        if m == 0 or n < m:
            return []

        base = 256
        mod = 1_000_000_007

        # Precompute base^(m - 1) % mod
        high_pow = pow(base, m - 1, mod)

        pattern_hash = 0
        text_hash = 0

        for i in range(m):
            pattern_hash = (pattern_hash * base + ord(pattern[i])) % mod
            text_hash = (text_hash * base + ord(text[i])) % mod

        matches: list[int] = []

        for i in range(n - m + 1):
            if text_hash == pattern_hash:
                # Las Vegas character-by-character verification
                if text[i : i + m] == pattern:
                    matches.append(i)

            if i < n - m:
                # Remove left char, shift left, add right char
                text_hash = (text_hash - ord(text[i]) * high_pow) % mod
                text_hash = (text_hash * base + ord(text[i + m])) % mod

        return matches

    @staticmethod
    def repeated_dna_sequences(s: str) -> list[str]:
        """
        Finds all 10-letter DNA sequences that appear more than once (LeetCode 187).
        Time: O(N) | Auxiliary Space: O(N)
        """
        if len(s) <= 10:
            return []

        m = 10
        base = 4
        mod = 1_000_000_007
        to_int = {"A": 0, "C": 1, "G": 2, "T": 3}

        high_pow = pow(base, m - 1, mod)
        curr_hash = 0

        for i in range(m):
            curr_hash = (curr_hash * base + to_int[s[i]]) % mod

        seen: set[int] = {curr_hash}
        output: set[str] = set()

        for i in range(len(s) - m):
            curr_hash = (curr_hash - to_int[s[i]] * high_pow) % mod
            curr_hash = (curr_hash * base + to_int[s[i + m]]) % mod

            sub = s[i + 1 : i + 1 + m]
            if curr_hash in seen:
                output.add(sub)
            else:
                seen.add(curr_hash)

        return list(output)
```

---

## ⚖️ CHAPTER 4: COMPLEXITY DECONSTRUCTION

### Explicit Complexity Breakdown

| Scenario / Operation | Time Complexity | Auxiliary Space | Output Space | Algorithmic Bottleneck |
| :--- | :--- | :--- | :--- | :--- |
| **Average Case (Distinct Chars)** | `O(N + M)` | `O(1)` | `O(K)` | Single hash calculation per character |
| **Worst Case (Pathological)** | `O(N * M)` | `O(1)` | `O(K)` | High collision rate (e.g., text="AAAA", pat="AA" with poor prime) |
| **DNA 10-mer Hashing (LC 187)** | `O(N)` | `O(N)` (seen set) | `O(K)` | Hash set lookup per 10-character sliding window |
| **KMP Search** | `O(N + M)` (Deterministic) | `O(M)` | `O(K)` | LPS precomputation array |

> [!TIP]
> **Collision Probability Analysis:**
> With a large prime modulus `MOD = 10^9 + 7`, the probability of two arbitrary non-matching substrings colliding is `1 / MOD ≈ 10^(-9)`. For a text of length `N = 10^6`, the expected number of spurious collisions across the entire text is `N / MOD ≈ 10^6 / 10^9 = 0.001`. Thus, the verification step executes almost exclusively on true positive matches.

---

## 🎙️ CHAPTER 5: 45-MINUTE INTERVIEW VERBAL SCRIPT

### Phase 1: Clarification & Choosing Rabin-Karp (0–5 Mins)
- **Candidate:** "Let's clarify the pattern search problem: Are we searching for a single pattern in a text, or are we searching for multiple patterns of length `M` simultaneously? Also, what is the alphabet size?"
- **Interviewer:** "Single pattern first, but consider how this extends to multiple patterns."
- **Candidate:** "Understood. For a single pattern, Knuth-Morris-Pratt or Boyer-Moore are standard. But Rabin-Karp is particularly powerful: by rolling a polynomial hash, we can search for multiple patterns in `O(N + k*M)` time by putting pattern hashes in a hash set. I will implement Rabin-Karp with full Las Vegas verification."

### Phase 2: Explaining the Rolling Hash Math (5–12 Mins)
- **Candidate:** "The key insight is treating the string as a base-256 polynomial modulo `10^9 + 7`.
When our window slides from `i` to `i + 1`, we don't recalculate the hash from scratch in `O(M)`. Instead, we drop the outgoing leftmost character `text[i]` by subtracting `text[i] * Base^(M-1)`. We multiply the remaining hash by `Base` to shift all character exponents up by one, and add the incoming rightmost character `text[i + M]`. This rolling update runs in strictly `O(1)` time."

### Phase 3: Live Implementation & Modulo Safety (12–32 Mins)
- **Candidate:** "Notice two crucial implementation details in my code:
1. In C# and Python, subtracting a positive term can yield a negative number before the modulo operation. In C#, `(-5) % 10` is `-5`, not `+5`. Therefore, we must normalize with `if (textHash < 0) textHash += Mod;`.
2. When `textHash == patternHash`, we perform `text.Slice(i, m).SequenceEqual(pattern)` to prevent false positives from hash collisions."

### Phase 4: Edge Cases & Verification (32–40 Mins)
- **Candidate:** "Let's verify edge cases:
  1. Pattern longer than text (`M > N`): Returns empty list immediately.
  2. Empty pattern or text: Guard clauses handle safely.
  3. Identical characters (e.g., text `"AAAA"`, pattern `"AA"`): Correctly finds matches at indices 0, 1, 2.
  4. Collision handling: If two different strings produce the same hash, `SequenceEqual` fails and rejects the false hit."

---

## 🛠️ CHAPTER 6: COMMON PITFALLS & DECISION FRAMEWORK

### Common Pitfalls
1. **Negative Modulo Bug:** Forgetting to add `MOD` when `hash` becomes negative after subtraction.
2. **Skipping Verification (Monte Carlo Mistake):** Assuming equal hashes guarantee identical strings. In an interview, skipping string verification is an immediate red flag.
3. **Integer Overflow in Exponentiation:** Computing `Base^(M-1)` with standard 32-bit math instead of modular exponentiation (`pow = (pow * Base) % MOD`).

### Decision Framework

```
                       [ String Matching Task ]
                                  |
         +------------------------+------------------------+
         |                                                 |
  [ Single Pattern ]                              [ Multiple Patterns / Sets ]
         |                                                 |
   Pattern Length M?                                 Equal Length M?
         |                                                 |
   +-----+-----+                                     +-----+-----+
   |           |                                     |           |
 M Small     M Large                                YES          NO
 KMP         Boyer-Moore                         Rabin-Karp    Aho-Corasick
(O(N+M))    (O(N/M) avg)                        (Hash Set)     (Trie Automaton)
```

---

## 🏋️ PRACTICE LADDER

| Problem | LeetCode # | Difficulty | Key Pattern | Primary Challenge |
| :--- | :--- | :--- | :--- | :--- |
| **Find the Index of the First Occurrence in a String** | #28 | 🟢 Easy | Rabin-Karp / strStr | Rolling hash implementation |
| **Repeated DNA Sequences** | #187 | 🟡 Medium | Rolling Hash (Base 4) | 10-mer rolling hash set |
| **Longest Duplicate Substring** | #1044 | 🔴 Hard | Binary Search + Rabin-Karp | Double rolling hash to eliminate collisions |
| **Shortest Palindrome** | #214 | 🔴 Hard | Rolling Hash / KMP | Prefix vs reverse suffix matching |
| **Repeated String Match** | #686 | 🟡 Medium | Rabin-Karp | Cyclic text repetition count |

---

> 🧭 **Navigation:** [← Previous Day](Week_06_Day_04_String_Transformations_Building_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Week Playbook →](WEEK_06_FULL_PLAYBOOK.md)
