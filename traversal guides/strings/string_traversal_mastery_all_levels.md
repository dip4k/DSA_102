# 🔤 String Traversal Mastery (Curriculum 2.0)

> **Goal:** Master string traversal and navigation—cursor movement, boundaries, multi-pointer patterns, windows, state machines, and advanced text processing.

---

## 🗺️ Mastery Summary Table

| Level | Topic | Mental Model | Pointer State | Drill Problems |
| :--- | :--- | :--- | :--- | :--- |
| **1. Walk** | Linear Scans | Process elements 1-by-1 left-to-right. `i` points to the next unprocessed char. | `[0, i-1]` processed, `i` current. | [LeetCode 344], [LeetCode 709] |
| **2. Math** | Index Math | Movement is dictated by arithmetic (reverse, step, wrap). | `i = formula(step)` | [LeetCode 58], [LeetCode 189] |
| **3. Pointers**| Multi-Pointer | Track two bounds. `L` and `R` define a region or anchor/cursor boundary. | `[L, R]` active window or bounds | [LeetCode 125], [LeetCode 151], [LeetCode 443] |
| **4. Windows** | Sliding Window | Dynamic range constrained by conditions. | `[L, R]` valid state window | [LeetCode 3], [LeetCode 76], [LeetCode 340] |
| **5. States** | State Machines | Behavior dictated by historical context. | `state_enum` + `i` | [LeetCode 8], [LeetCode 65] |
| **6. Enum** | Enumerators | Index-free iteration. Focus on element identity. | `ch` active, no `i` | [LeetCode 771] |
| **7. Lexing** | Token Parsing | Group chars into atomic tokens. | `[token_start, i-1]` token | [LeetCode 150], [LeetCode 224] |
| **8. Unicode** | Text Encoding | Code points vs grapheme clusters. | Logical text units | [LeetCode 387] |
| **9. Perf** | Performance | In-place mutations vs `StringBuilder` / streaming. | Memory buffers | [LeetCode 14], [LeetCode 415] |
| **Adv.** | Pattern Match | Lookaheads, hashes, fallback graphs. | Table-driven `i`, `j` | [LeetCode 28], [LeetCode 5] |

---

## 🚶 Level 1 — Physical Movement (Linear Scans)

**Mental Model:** `i` represents the physical index of the *next* character to evaluate. The region `[0, i-1]` represents the fully evaluated prefix.
**Invariant:** `0 <= i < len(s)`. Always bounds-check *before* evaluating `s[i]`.

**Visual State Transition (Forward Scan until non-space):**
| Step | `i` Position | `s[i]` | Action / Invariant |
| :--- | :--- | :--- | :--- |
| Init | `0` | `' '` | `i < len`, is space. `i++`. |
| 1 | `1` | `' '` | `i < len`, is space. `i++`. |
| 2 | `2` | `'A'` | `i < len`, not space. Halt. Result found at `2`. |

Problem: Find the index of the first digit in a string, or return -1 if none exists.
```python
def first_digit(s: str) -> int:
    # 1. Initialize pointer to the start of the string
    i = 0
    # 2. Linear scan: check bounds first, then evaluate character property
    # Invariant: safe to check s[i] only if i < len(s)
    while i < len(s) and not s[i].isdigit():
        i += 1 # 3. Advance pointer to the next unprocessed character
    # 4. Return the valid index if found, otherwise -1
    return i if i < len(s) else -1
```

Problem: Find the index of the first digit in a string, or return -1 if none exists.
```csharp
static int FirstDigit(string s) {
    // 1. Initialize pointer to the start of the string
    int i = 0;
    // 2. Linear scan: check bounds first, then evaluate character property
    // Invariant: bounds check precedes element access
    while (i < s.Length && !char.IsDigit(s[i])) i++; // 3. Advance pointer to next char
    // 4. Return valid index or -1 if the string was exhausted
    return i < s.Length ? i : -1;
}
```

### ⚠️ Gotchas & Pitfalls
- **Out of bounds access:** Failing to check `i < len(s)` before evaluating `s[i]` during inner `while` loops.
- **Infinite loops:** Forgetting to increment `i` inside a conditional branch when processing characters one-by-one.

**Drill Problems:**
- 🟢 Easy: [LeetCode 709: To Lower Case]
- 🟢 Easy: [LeetCode 344: Reverse String]

---

## 🧮 Level 2 — Index Arithmetic

**Mental Model:** Logical movement isn't strictly sequential. `i` maps a logical sequence (e.g., backwards, alternating) to a physical memory address.
**Invariant:** `i` must safely map to `0 <= i < len(s)` even when derived from formulas like modulo `(i % n + n) % n` or reverse iteration.

**Visual State Transition (Reverse Scan for Last Char):**
| Step | `i` (Physical) | `s[i]` | Action / Invariant |
| :--- | :--- | :--- | :--- |
| Init | `len-1` | `' '` | `i >= 0`, is space. `i--`. |
| 1 | `len-2` | `'d'` | Char found. Halt. |

Problem: Find the index of the last non-space character in a string, or return -1 if it's entirely spaces.
```python
def last_non_space(s: str) -> int:
    # 1. Start from the physical end of the string (len - 1)
    # 2. Iterate backwards towards 0
    # Invariant: iterate strictly downwards to 0
    for i in range(len(s) - 1, -1, -1):
        # 3. Check character property and return immediately upon finding
        if not s[i].isspace(): return i
    # 4. Fallback if entire string is processed without finding a match
    return -1
```

Problem: Find the index of the last non-space character in a string, or return -1 if it's entirely spaces.
```csharp
static int LastNonSpace(string s) {
    // 1. Start from the physical end (Length - 1) and step backwards to 0
    for (int i = s.Length - 1; i >= 0; i--)
        // 2. Check character property and return physical index if matched
        if (!char.IsWhiteSpace(s[i])) return i;
    // 3. Fallback if loop finishes with no non-space chars found
    return -1;
}
```

### ⚠️ Gotchas & Pitfalls
- **Off-by-one errors:** In languages like Python, `range(len(s) - 1, 0, -1)` stops at `1`, missing the `0`th index.
- **Modulo wrapping bugs:** `(i - step) % n` can evaluate to a negative number in C-like languages; proper wrapping requires `(x % n + n) % n`.

**Drill Problems:**
- 🟢 Easy: [LeetCode 58: Length of Last Word]
- 🟡 Medium: [LeetCode 189: Rotate Array]

---

## 👀 Level 3 — Multi-Cursor Traversal

**Mental Model:** Multiple pointers operate on the same array. 
- **Two-Pointer:** `L` and `R` converge from ends.
- **Anchor-Cursor:** `anchor` marks the start of a sequence, `i` finds the end. The region `[anchor, i-1]` contains the identified sequence.
**Invariant:** At least one pointer must physically move forward/backward every iteration to prevent infinite loops. `L <= R`.

**Visual State Transition (Run-Length / Collapse Spaces):**
| Step | `anchor` | `i` | Chunk `[anchor, i-1]` | Action / Invariant |
| :--- | :--- | :--- | :--- | :--- |
| 1 | `0` | `0` | Empty | Start of run. |
| 2 | `0` | `2` | `"  "` | Process spaces. `i++`. |
| 3 | `2` | `2` | `"word"`| Anchor at new word. |

Problem: Collapse all consecutive spaces in a string into a single space.
```python
def collapse_spaces(s: str) -> str:
    # 1. Initialize result list, cursor `i`, and cached string length `n`
    res, i, n = [], 0, len(s)
    # 2. Traverse the entire string bounds
    while i < n:
        if s[i] != ' ':
            # 3a. Process normal characters directly
            res.append(s[i])
            i += 1
        else:
            # 3b. Handle sequences of spaces (multi-cursor logic)
            # Anchor at i, consume chunk of identical chars
            while i < n and s[i] == ' ':
                i += 1 # 4. Move inner cursor to skip all consecutive spaces
            # 5. Append only one space to represent the collapsed chunk
            res.append(' ')
    # 6. Join accumulated characters into final string
    return "".join(res)
```

### ⚠️ Gotchas & Pitfalls
- **Pointer crossover:** Allowing `L` to surpass `R` (`L > R`) before breaking, resulting in double-processing elements or swapping back.
- **Stuck pointers:** Missing the bounds check `i < len(s)` inside the inner advance loop, leading to `IndexError` when scanning for the end of a chunk.

**Drill Problems:**
- 🟢 Easy: [LeetCode 125: Valid Palindrome]
- 🟡 Medium: [LeetCode 151: Reverse Words in a String]
- 🟡 Medium: [LeetCode 443: String Compression]

---

## 🪟 Level 4 — Range & Window Traversal

**Mental Model:** The region `[L, R]` acts as an accordion, expanding (`R++`) to satisfy a condition, and contracting (`L++`) to restore validity when the condition is breached.
**Invariant:** The aggregate state variables (sums, frequencies) must *perfectly* match the elements currently inside `[L, R]`.

**Visual State Transition (Longest Substring <= K distinct):**
| Step | `L` | `R` | Window `[L, R]` | State Check | Action / Invariant |
| :--- | :--- | :--- | :--- | :--- | :--- |
| Expand | `0` | `3` | `"abcc"` | > K distinct | Invalid. Must shrink `L`. |
| Shrink | `1` | `3` | `"bcc"` | <= K distinct | Valid. Update `best`. |

Problem: Find the length of the longest substring containing at most k distinct characters.
```python
def longest_k_distinct(s: str, k: int) -> int:
    # 1. Initialize state dictionary (freq), left window bound (L), and result tracker (best)
    freq, L, best = {}, 0, 0
    # 2. Iterate right window bound (R) over the entire string
    for R in range(len(s)):
        # 3. Expand window by incorporating s[R] into state
        freq[s[R]] = freq.get(s[R], 0) + 1  # Expand window
        
        # 4. Check if invariant is breached (more than k distinct characters)
        while len(freq) > k:               # Invariant breached
            # 5. Shrink window from the left by removing s[L] from state
            freq[s[L]] -= 1                # Shrink window
            if freq[s[L]] == 0:
                del freq[s[L]]
            L += 1 # 6. Advance left bound
            
        # 7. Capture the valid state once the window condition is satisfied
        best = max(best, R - L + 1)        # Capture valid state
    return best
```

### ⚠️ Gotchas & Pitfalls
- **Premature state capture:** Updating the `best` answer inside the expansion phase *before* the inner `while` loop restores the validity of the window.
- **Stale frequency maps:** Decrementing a frequency to `0` but forgetting to delete the key from the dictionary, artificially inflating the unique element count.

**Drill Problems:**
- 🟡 Medium: [LeetCode 3: Longest Substring Without Repeating Characters]
- 🟡 Medium: [LeetCode 340: Longest Substring with At Most K Distinct Characters]
- 🔴 Hard: [LeetCode 76: Minimum Window Substring]

---

## 🧭 Level 5 — Abstract Traversal (State Machines)

**Mental Model:** `s[i]` meaning changes based on the "state" we are in. For example, a quote mark `"` transitions the parser into an `InQuotes` state where spaces are ignored.
**Invariant:** `i` only advances according to the valid state transitions. Every state must correctly handle EOF (End of File/String).

**Visual State Transition (Atoi / String to Integer):**
| Step | State | `char` | Next State | Action / Invariant |
| :--- | :--- | :--- | :--- | :--- |
| 1 | `START` | `' '` | `START` | Skip space. |
| 2 | `START` | `'-'` | `SIGN` | Record negative sign. |
| 3 | `SIGN` | `'9'` | `DIGIT` | Accumulate value. |
| 4 | `DIGIT` | `'a'` | `END` | Non-digit found. Halt. |

### ⚠️ Gotchas & Pitfalls
- **Incomplete End-of-File handling:** Reaching the end of the string without processing the final token, because the EOF logic is only checked inside the loop body.
- **Implicit state transitions:** Failing to explicitly code invalid transition paths (e.g., encountering a second `-` symbol), keeping the machine in a valid state incorrectly.

**Drill Problems:**
- 🟡 Medium: [LeetCode 8: String to Integer (atoi)]
- 🔴 Hard: [LeetCode 65: Valid Number]

---

## 🔄 Level 6 — Enumeration Walk

**Mental Model:** Iterating without an index pointer. You care entirely about the *identity* of elements, not their sequence or adjacency.
**Invariant:** Collection guarantees each item is yielded exactly once from start to finish.

Problem: Count how many stones you have that are also considered jewels.
```csharp
static int CountJewels(string jewels, string stones) {
    // 1. Initialize counter for matches
    int count = 0;
    // 2. Enumerate through items, focusing solely on the item's value, not index
    foreach (char stone in stones) {
        // 3. Evaluate identity of current element against our target set
        if (jewels.Contains(stone)) count++;
    }
    // 4. Return total matches
    return count;
}
```

### ⚠️ Gotchas & Pitfalls
- **Index erasure:** Needing positional data later in the loop body but not having an index pointer available, leading to messy workarounds or `.IndexOf()` calls.
- **Concurrent modification exception:** Attempting to modify the underlying string or collection while looping through it using an enumerator `foreach`.

**Drill Problems:**
- 🟢 Easy: [LeetCode 771: Jewels and Stones]

---

## 🧩 Level 7 — Lexing & Token Traversal

**Mental Model:** Raw strings are grouped into logical blocks (Tokens). The traversal hops from token to token rather than character to character.
**Invariant:** `i` jumps over whitespace, finds start of a token, scans to its end, and yields `[token_start, i-1]`.

### ⚠️ Gotchas & Pitfalls
- **Multi-digit token truncation:** Assuming all operators or operands are single characters (e.g., reading `"12"` as `"1"` and `"2"` instead of twelve).
- **Sign vs. Operator confusion:** Misinterpreting a negative sign (`-`) token as subtraction because context from the previous token was not tracked.

**Drill Problems:**
- 🟡 Medium: [LeetCode 150: Evaluate Reverse Polish Notation]
- 🔴 Hard: [LeetCode 224: Basic Calculator]

---

## 🌍 Level 8 — Unicode-Aware Traversal

**Mental Model:** A "perceived character" (grapheme) may be composed of multiple underlying code units (like bytes or chars).
**Invariant:** Text length and logical iterations must align with the encoding level (Bytes vs Chars vs Graphemes).
- Example: Iterating standard ASCII via `s[i]` works safely, but Emojis or accented chars may split across indices if iterating bytes.

### ⚠️ Gotchas & Pitfalls
- **Emoji string splitting:** Indexing blindly into a UTF-16 string array (e.g., in C# or Java) causing surrogate pairs to split and corrupt the text.
- **Byte length vs. logical length:** Slicing strings based on byte offsets rather than logical character counts, producing malformed characters at the boundaries.

**Drill Problems:**
- 🟢 Easy: [LeetCode 387: First Unique Character in a String]

---

## ⚙️ Level 9 — High-Performance Traversal

**Mental Model:** String concatenation is `O(N^2)` because strings are immutable. Treat modifications as buffered appends.
**Invariant:** Collect parts into a mutable structure (e.g., `StringBuilder` or `List`) and allocate the final string exactly once.

Problem: Concatenate string parts efficiently without causing quadratic time complexity allocations.
```python
# GOOD
# 1. Initialize a mutable list to act as a buffer
parts = []
# 2. Append pieces to buffer (O(1) amortized time)
parts.append("hello")
# 3. Join the buffer into a final string in one pass (O(N) total allocation)
result = "".join(parts)
```

Problem: Concatenate string parts efficiently without causing quadratic time complexity allocations.
```csharp
// GOOD
// 1. Initialize a StringBuilder instance to buffer mutations
var sb = new StringBuilder();
// 2. Append parts without creating intermediate string allocations
sb.Append("hello");
// 3. Perform a single final string allocation
string result = sb.ToString();
```

### ⚠️ Gotchas & Pitfalls
- **Hidden O(N^2) allocations:** Using `+=` concatenation inside a loop, silently recreating a new string object and thrashing memory each iteration.
- **Over-allocating memory:** Creating dynamic arrays or builders without an estimated capacity, leading to frequent and costly internal resize operations.

**Drill Problems:**
- 🟢 Easy: [LeetCode 14: Longest Common Prefix]
- 🟢 Easy: [LeetCode 415: Add Strings]

---

## 🔍 Advanced — Pattern Matching

**Mental Model:** Finding substrings efficiently requires mathematical hashes or precomputed fallback graphs to avoid `O(N * M)` backtracking.
- **KMP:** Precomputes a fallback table (`lps`). When `text[i] != pattern[j]`, `j` rewinds via `lps` without resetting `i`.
- **Rabin-Karp:** Rolling hashes update in `O(1)` time as window slides.
- **Trie:** Tree structured dictionary matching.

### ⚠️ Gotchas & Pitfalls
- **Hash collisions:** Assuming matching rolling hashes guarantees string equality without doing a secondary physical character-by-character check.
- **Degraded performance:** Naive implementations or poor hashing functions causing KMP/Rabin-Karp to devolve back into `O(N * M)` time complexity.

**Drill Problems:**
- 🟢 Easy: [LeetCode 28: Find the Index of the First Occurrence in a String] (KMP)
- 🟡 Medium: [LeetCode 5: Longest Palindromic Substring] (Manacher's / Expand Around Center)
- 🟡 Medium: [LeetCode 187: Repeated DNA Sequences] (Rabin-Karp)
- 🟡 Medium: [LeetCode 208: Implement Trie (Prefix Tree)] (Trie)
