# 🧠 KMP Algorithm — Prefix Table (LPS / π) Build (Index-by-Index Trace)

> Focus: **how the prefix table is built** for the pattern (not the full search yet).

---

## ✅ Summary list
- 📌 The KMP prefix table is called **LPS** (Longest Proper Prefix which is also Suffix) or **π (prefix function)**.
- 🧩 `lps[i]` stores the length of the longest *proper* prefix of `pattern[0..i]` that is also a suffix of `pattern[0..i]`.
- 🔁 When there’s a mismatch, you **fallback** using earlier prefix knowledge (`len = lps[len-1]`) instead of restarting from scratch.

---

## 🔤 Terminology (choose one naming, same idea)

### LPS (common interview naming)
- `lps[i]` = length of the **longest proper prefix** of `P[0..i]` that is also a suffix of `P[0..i]`. [web:132]

### Prefix function π (common CP naming)
- `pi[i]` = length of the longest proper prefix of `s` that is also a suffix ending at position `i` (same concept as LPS). [web:130]

> In this file, we’ll use `lps[]` and `len` (current matched prefix length), matching many standard KMP explanations. [web:132]

---

## 🧭 Build algorithm (core logic)

### Why
The prefix table lets KMP avoid re-checking characters that are guaranteed to match based on previous comparisons. [web:132]

### What
Maintain:
- `i` = current index we are computing `lps[i]` for (starts from 1)
- `len` = current best candidate prefix length for the substring ending at `i-1`

### How (step/flow)
- Start: `lps[0] = 0`, `len = 0`, `i = 1`. [web:132]
- If `P[i] == P[len]`: increment `len`, set `lps[i] = len`, move `i++`. [web:132]
- Else if `len != 0`: set `len = lps[len-1]` (fallback), **do not** increment `i` yet. [web:132]
- Else (`len == 0`): set `lps[i] = 0`, move `i++`. [web:132]

### Common pitfalls
- Incrementing `i` during fallback (you must retry the same `i` with a shorter `len`). [web:132]
- Confusing `len` (a **length**) with an index (it points to `P[len]` for comparison). [web:132]

### Tips and tricks
- Think of fallback as: “What’s the next best prefix length that could still match?” [web:130]
- The prefix function/LPS value can increase by at most 1 when moving forward by one position, which helps explain why total work is linear. [web:130]

---

# 🧪 Full index-by-index trace (classic example)

## Pattern
We’ll build LPS for:

```
P =  A  B  A  B  A  C  A
idx  0  1  2  3  4  5  6
```

Expected result:
```
lps = [0, 0, 1, 2, 3, 0, 1]
```

---

## ✅ Trace table (including fallback steps)

Legend:
- `len` = current candidate prefix length
- compare `P[i]` vs `P[len]`
- when fallback happens, **i stays the same**

| Step | i | P[i] | len (before) | Compare to P[len] | Action | len (after) | lps[i] set? |
|---:|---:|:---:|---:|:---:|:---|---:|:---:|
| 0 | 0 | A | - | - | Initialize | 0 | lps[0]=0 |
| 1 | 1 | B | 0 | A | mismatch, len==0 → set 0, i++ | 0 | lps[1]=0 |
| 2 | 2 | A | 0 | A | match → len++, i++ | 1 | lps[2]=1 |
| 3 | 3 | B | 1 | B | match → len++, i++ | 2 | lps[3]=2 |
| 4 | 4 | A | 2 | A | match → len++, i++ | 3 | lps[4]=3 |
| 5a | 5 | C | 3 | B | mismatch, fallback len=lps[2] | 1 | - |
| 5b | 5 | C | 1 | B | mismatch, fallback len=lps[0] | 0 | - |
| 5c | 5 | C | 0 | A | mismatch, len==0 → set 0, i++ | 0 | lps[5]=0 |
| 6 | 6 | A | 0 | A | match → len++, i++ | 1 | lps[6]=1 |

This trace follows the standard KMP LPS construction where mismatches trigger fallback using previously computed LPS values. [web:132]

---

## 🔎 What fallback is doing (visual intuition)

At `i = 5` we had `len = 3`, meaning we currently believe:
- prefix `P[0..2] = "ABA"` matches the suffix of `P[0..4] = "ABABA"`.

When `P[5]='C'` mismatches against `P[len]='B'`, fallback tries a shorter candidate prefix length using LPS-of-LPS:
```
len = 3  -> fallback to lps[2] = 1
len = 1  -> fallback to lps[0] = 0
len = 0  -> no more fallback, lps[5]=0
```
This “fallback chain” is exactly the repeated `len = lps[len-1]` step. [web:132][web:130]

---

# 💻 Reference implementations (C# + Python)

## C# — Build LPS
```csharp
static int[] BuildLps(string p)
{
    if (string.IsNullOrEmpty(p)) return Array.Empty<int>(); // lenient

    int n = p.Length;
    int[] lps = new int[n];

    int len = 0;   // length of previous longest prefix suffix
    int i = 1;

    while (i < n)
    {
        if (p[i] == p[len])
        {
            len++;
            lps[i] = len;
            i++;
        }
        else
        {
            if (len != 0)
            {
                len = lps[len - 1];
                // i stays
            }
            else
            {
                lps[i] = 0;
                i++;
            }
        }
    }

    return lps;
}
```
This matches the common LPS construction logic: advance on match, otherwise fallback with `len = lps[len-1]` until you can match or hit zero. [web:132]

## Python — Build LPS
```python
def build_lps(p: str) -> list[int]:
    if not p:
        return []

    n = len(p)
    lps = [0] * n

    length = 0
    i = 1

    while i < n:
        if p[i] == p[length]:
            length += 1
            lps[i] = length
            i += 1
        else:
            if length != 0:
                length = lps[length - 1]
            else:
                lps[i] = 0
                i += 1

    return lps
```
The structure mirrors the standard KMP prefix function computation described in many references. [web:129][web:132]

---

## ✅ Debugging checklist (when your LPS is wrong)
- Print `(i, len, p[i], p[len])` before each comparison (only when len < n).
- If you see an infinite loop, you probably forgot to change `len` or `i` in a mismatch branch.
- If `lps[i]` is too large, you likely incremented `i` during fallback or mixed up indices vs lengths.

---

## Next (if you want)
- 🔍 Run KMP search with a text `T` using this LPS table and do a full trace of `(ti, pi)` pointer moves.
