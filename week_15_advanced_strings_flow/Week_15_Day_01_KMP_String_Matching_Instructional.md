# WEEK 15 DAY 01: KMP STRING MATCHING — ENGINEERING GUIDE

**Metadata:**
- **Week:** 15 | **Day:** 01
- **Phase:** E — Integration & Extensions (Weeks 14-15)
- **Category:** Advanced Strings
- **Difficulty:** 🟡 Intermediate → 🔴 Advanced
- **Duration:** 90 minutes
- **Prerequisites:** String basics, arrays, recursion/induction, core pattern-matching intuition.

---

## 🎯 LEARNING OBJECTIVES
- Understand why naive string matching becomes O(nm) in the worst case.
- Build the LPS array correctly and explain what each value means.
- Trace KMP matching without moving the text pointer backward.
- Explain why KMP runs in O(n + m).
- Apply KMP to substring search, overlapping matches, and streaming-style scanning.

---

## 📖 CHAPTER 1: CONTEXT & MOTIVATION

### The engineering problem
Suppose you need to search for a short pattern inside a very large text: logs, DNA sequences, source code, or packet payloads. A naive approach compares the pattern at every possible starting position in the text, and after a mismatch it throws away all partial work.

That wasted work becomes expensive on repetitive inputs. For example, if the text is full of repeated characters, the naive method may keep re-checking almost the same characters again and again, causing O(nm) behavior.

### The KMP idea
KMP fixes this by learning from the pattern itself. When a mismatch happens after some successful matches, KMP uses the pattern's internal structure to decide how far the pattern can shift without losing valid information.

> **💡 Insight:** KMP never moves backward in the text; it only repositions the pattern index intelligently.

---

## 🧠 CHAPTER 2: THE LPS ARRAY

### Definition
For pattern `P`, `LPS[i]` is the length of the **longest proper prefix** of `P[0..i]` that is also a suffix of `P[0..i]`.

A proper prefix means a prefix that is not the whole string itself.

### Example
```text
Pattern: A B A B D A B A C D
Index:   0 1 2 3 4 5 6 7 8 9
LPS:     0 0 1 2 0 1 2 3 0 0
```

### Visual intuition
```text
Substring: A B A B
Prefix:    A B
Suffix:    A B
LPS value: 2
```

If a mismatch happens after matching `j` characters, then `LPS[j-1]` tells us how many matched characters can still be reused.

### Why this works
If a prefix is also a suffix, then after a mismatch we do not need to restart from pattern index 0. We can jump to the longest border already guaranteed by earlier matches.

---

## ⚙️ CHAPTER 3: BUILDING THE LPS ARRAY

### C# implementation
```csharp
public static int[] BuildLps(string pattern)
{
    int m = pattern.Length;
    int[] lps = new int[m];
    int len = 0;
    int i = 1;

    while (i < m)
    {
        if (pattern[i] == pattern[len])
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

### Trace table
```text
Pattern: A B A B D A B A C D

| i | len before | compare | action                  | lps[i] |
|---|------------|---------|-------------------------|--------|
| 1 | 0          | B vs A  | mismatch, set 0         | 0      |
| 2 | 0          | A vs A  | match, len -> 1         | 1      |
| 3 | 1          | B vs B  | match, len -> 2         | 2      |
| 4 | 2          | D vs A  | fallback to lps[1] = 0  |        |
| 4 | 0          | D vs A  | mismatch, set 0         | 0      |
| 5 | 0          | A vs A  | match, len -> 1         | 1      |
| 6 | 1          | B vs B  | match, len -> 2         | 2      |
| 7 | 2          | A vs A  | match, len -> 3         | 3      |
| 8 | 3          | C vs B  | fallback to lps[2] = 1  |        |
| 8 | 1          | C vs B  | fallback to lps[0] = 0  |        |
| 8 | 0          | C vs A  | mismatch, set 0         | 0      |
| 9 | 0          | D vs A  | mismatch, set 0         | 0      |
```

Final LPS:
```text
[0, 0, 1, 2, 0, 1, 2, 3, 0, 0]
```

---

## ⚙️ CHAPTER 4: KMP MATCHING

### C# implementation
```csharp
using System.Collections.Generic;

public static List<int> KmpSearch(string text, string pattern)
{
    var matches = new List<int>();
    if (string.IsNullOrEmpty(pattern)) return matches;

    int[] lps = BuildLps(pattern);
    int i = 0;
    int j = 0;

    while (i < text.Length)
    {
        if (text[i] == pattern[j])
        {
            i++;
            j++;

            if (j == pattern.Length)
            {
                matches.Add(i - j);
                j = lps[j - 1];
            }
        }
        else if (j != 0)
        {
            j = lps[j - 1];
        }
        else
        {
            i++;
        }
    }

    return matches;
}
```

### Worked example
Search pattern `ABABCAB` in text `ABABDABABCAB`.

```text
Text:    A B A B D A B A B C A B
Pattern: A B A B C A B
LPS:     0 0 1 2 0 1 2
```

Key moment:
- Match first 4 characters: `ABAB`
- Mismatch at text `D` vs pattern `C`
- Instead of restarting, KMP falls back from `j = 4` to `j = 2`, then to `j = 0` if needed
- Text index stays where it is until all valid fallback positions are tested

Result: match starts at index `5`.

### Visual state movement
```text
Text:    A B A B D A B A B C A B
Index:   0 1 2 3 4 5 6 7 8 9 10 11

Pattern: A B A B C A B
          0 1 2 3 4 5 6

Mismatch at text[4] = D, pattern[4] = C
Fallback: j = 4 -> lps[3] = 2
Try again without moving i
```

---

## ⚖️ CHAPTER 5: COMPLEXITY & CORRECTNESS

### Why KMP is O(n + m)
- Building the LPS array takes O(m).
- During matching, the text pointer `i` only moves forward, so it advances at most `n` times.
- The pattern pointer `j` may fall back, but each fallback removes previously accumulated progress, so total fallback work is still linear.

This gives total time O(n + m).

### Complexity table
| Phase | Time | Space |
| :--- | :--- | :--- |
| Build LPS | O(m) | O(m) |
| Search | O(n) | O(1) extra |
| Total | O(n + m) | O(m) |

### Correctness idea
KMP is correct because after a mismatch it only skips pattern positions that are already proven impossible. The LPS array guarantees that any reusable prefix-suffix alignment is preserved.

---

## 🏭 CHAPTER 6: APPLICATIONS

### 1. Substring search
KMP is a classic exact substring search algorithm with guaranteed linear performance.

### 2. Streaming search
Since the text pointer never moves backward, KMP works well when characters arrive incrementally, such as logs or network data.

### 3. Overlapping matches
After one full match, setting `j = lps[j - 1]` lets KMP continue immediately and detect overlapping occurrences.

### 4. Period detection
The LPS array helps detect repeated structure in strings, including shortest repeating units.

---

## 🔗 CHAPTER 7: KMP VS NAIVE VS Z-ALGORITHM

| Algorithm | Core idea | Time | Best use |
| :--- | :--- | :--- | :--- |
| Naive | Restart comparisons at each text shift | O(nm) worst case | Tiny inputs, simplest baseline |
| KMP | Failure function / LPS fallback | O(n + m) | Exact search with strong guarantees |
| Z-Algorithm | Prefix-match lengths over string positions | O(n + m) | Exact search, border/period analysis |

### Philosophy difference
- **KMP:** thinks in terms of the pattern's failure structure.
- **Z-algorithm:** thinks in terms of prefix matches at every position.
- Both avoid redundant comparisons, but they organize information differently.

---

## 🧩 DECISION FRAMEWORK
- **Use KMP when:** you need exact matching with worst-case linear guarantees.
- **Use naive when:** input is tiny and implementation simplicity matters more than asymptotic guarantees.
- **Use Z-algorithm when:** you also care about prefix-match structure, borders, or periodicity in a more direct way.

**🚩 Interview signals:**
- "Find all occurrences of a pattern in linear time."
- "Do not move backward in the text."
- "Use preprocessing on the pattern."

---

## 🧪 SOCRATIC REFLECTION
1. Why is `LPS[i]` about the pattern alone and not the text?
2. Why can KMP keep `i` fixed during fallback without missing a valid match?
3. What kind of input makes naive matching behave badly but KMP still linear?

---

## 📌 RETENTION HOOK
> **The Essence:** "KMP remembers how the pattern overlaps with itself, so it never rechecks text work it already earned."

---

## 🧠 5 COGNITIVE LENSES
1. **Hardware lens:** sequential text scanning is cache-friendly.
2. **Trade-off lens:** extra O(m) preprocessing saves large matching cost.
3. **Learning lens:** the hardest part is understanding fallback without moving `i`.
4. **AI/ML lens:** LPS behaves like reusable structure, similar to cached partial state.
5. **Historical lens:** KMP was one of the foundational breakthroughs proving exact string matching could be linear.

---

## ⚔️ SUPPLEMENTARY OUTCOMES

### Practice problems
| Problem | Difficulty | Key concept |
| :--- | :--- | :--- |
| Build LPS for a pattern | Easy | Prefix-suffix structure |
| Implement KMP search | Medium | Matching loop |
| Find overlapping matches | Medium | Post-match fallback |
| Shortest repeating unit of a string | Medium | LPS at last index |
| Rotation check using KMP | Medium | Concatenation trick |

### Interview questions
1. What does `LPS[i]` mean exactly?
2. Why does KMP never move the text pointer backward?
3. Why is the total fallback work still linear?
4. How do overlapping matches work in KMP?
5. How is KMP different from the Z-algorithm?

### Common misconceptions
- **Myth:** KMP moves backward in the text.
  - **Reality:** only the pattern index falls back.
- **Myth:** LPS tells you where the pattern appears in the text.
  - **Reality:** LPS only describes the pattern's own internal border structure.
- **Myth:** fallback means redoing lots of work.
  - **Reality:** fallback reuses previously computed structure and preserves linear total work.

### Advanced concepts
- Border arrays and prefix functions.
- Connection between LPS and string periodicity.
- Streaming state machines for online matching.
- Multi-pattern extension ideas via Aho-Corasick.

---

## ✅ SELF-CHECK
1. All referenced values in the LPS example match the stated pattern.
2. The logic flow from naive matching to LPS to KMP search is consistent.
3. Complexity counts are internally consistent: preprocessing O(m), search O(n), total O(n + m).
4. State transitions preserve the KMP invariant: text index never moves backward.
5. Termination is correct because either `i` increases or `j` strictly decreases toward zero.
6. Red flags checked: no input mismatch, no invalid indices, no inconsistent fallback logic, no unsupported language examples beyond C#.
7. Any ambiguity was resolved in favor of explicit invariant-based explanation.
8. File content is ready for instructional delivery.
