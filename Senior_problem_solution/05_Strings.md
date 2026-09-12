# Phase 05: Strings

> **Focus:** Character Manipulation, Reader-Writer Cursors, Horizontal vs. Vertical Scanning, Expand-Around-Center Boundaries, and Palindromic Invariants.  
> **Source Curriculum:** [`Senior_dsa_question_list.md`](../Senior_dsa_question_list.md) — Phase 5 (Problems #24–#28)

---

## 24. Reverse String (LeetCode #344)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟢 Easy |
| **Priority** | ⚪ Reinforcement |
| **Pattern Tags** | `#two-pointers` `#in-place` `#opposing-pointers` `#string-manipulation` |
| **LeetCode Link** | [Reverse String](https://leetcode.com/problems/reverse-string/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Write a function that reverses a string. The input string is given as an array of characters `s`. You must do this by modifying the input array in-place with $O(1)$ extra memory.
- **Key Constraints:**
  - `1 <= s.Length <= 10^5`
  - `s[i]` is a printable ASCII character.
- **Senior Edge Cases to Defend:**
  - `s.Length == 1`: Loop condition `left < right` terminates immediately; zero operations executed.
  - Even length (e.g. `s = ["a", "b"]`): Pointers cross cleanly when `left` becomes `1` and `right` becomes `0`.
  - Odd length (e.g. `s = ["a", "b", "c"]`): Midpoint element (`'b'`) at index $1$ is never touched because `left == right == 1` terminates the loop, avoiding redundant self-swapping.
  - In-place constraint: Any allocation of a temporary array or conversion to string violates the strict $O(1)$ auxiliary space contract.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** In-place symmetric swap converging from outer boundaries to center using an opposing two-pointer pincer.
- **Sample 1:**
  - **Input:** `s = ["h","e","l","l","o"]`
  - **Step-by-Step Swaps:**
    - Swap `s[0]` ('h') and `s[4]` ('o') $\implies$ `["o","e","l","l","h"]`
    - Swap `s[1]` ('e') and `s[3]` ('l') $\implies$ `["o","l","l","e","h"]`
    - Pointers meet at index 2 ('l') $\implies$ Terminate.
  - **Output:** `["o","l","l","e","h"]`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine a mirror reflection pincer. Two hands grab opposite ends of a row of physical tiles. Simultaneously, the left hand and right hand swap their tiles, then take exactly one step inward toward each other. The process repeats until the hands meet in the middle. Because reflection across a central axis maps index $i$ directly to $N - 1 - i$, performing pairwise swaps across the axis completely reverses the sequence without needing any temporary holding area.

#### 3.2 The Naive Bottleneck & Redundant Computation
A naive approach allocates a secondary array `temp` of size $N$, reads `s` backwards into `temp`, and copies `temp` back into `s`:
- Space Complexity: $O(N)$ heap memory.
- GC Overhead: In high-throughput string pipelines (e.g. protocol deserialization), allocating $10^5$ character arrays triggers garbage collection latency spikes.
- In-place swapping completely eliminates auxiliary memory allocations.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**Opposing Convergence Invariant:**
For any array of length $N$, the reversed position of element $s[i]$ is $s[N - 1 - i]$.
By maintaining two cursors:
$$left = 0, \quad right = N - 1$$
At every step where $left < right$, swapping $s[left]$ and $s[right]$ simultaneously places both elements into their final reversed coordinates.
Advancing $left \to left + 1$ and $right \to right - 1$ preserves the invariant:
- All indices $< left$ and all indices $> right$ are already in their final reversed positions.
- The active candidate slice remains $s[left \dots right]$.
- Termination occurs in precisely $\lfloor N / 2 \rfloor$ swaps.

#### 3.4 Cursor Semantics & Invariant Partition Architecture
```text
[ 0 ......... left - 1 ]   [ left ......... right ]   [ right + 1 ......... N - 1 ]
└──────────┬───────────┘   └──────────┬───────────┘   └─────────────┬─────────────┘
     Reversed Left              Active Swap                Reversed Right
      (Finalized)                  Pincer                   (Finalized)
```
- `left`: Advances monotonically from $0 \to \lfloor N / 2 \rfloor$.
- `right`: Decrements monotonically from $N - 1 \to \lceil N / 2 \rceil$.
- Loop terminates strictly when $left \ge right$.

#### 3.5 State Transition Triggers & Decision Gates
1. **Convergence Gate:** While $left < right$:
   - Swap $s[left]$ and $s[right]$ (using C# tuple deconstruction or scalar temp).
   - `left++`.
   - `right--`.
2. **Termination:** Array is fully reversed in-place.

#### 3.6 Concrete Step-by-Step State Trace
Input: `s = ["h", "e", "l", "l", "o"]`.

| Step | `left` | `right` | `s[left]` | `s[right]` | Action | Array State | Invariant Maintained |
| :---: | :---: | :---: | :---: | :---: | :---: | :---: | :---: |
| **0** | `0` | `4` | `'h'` | `'o'` | Swap $0 \leftrightarrow 4$ | `["o", "e", "l", "l", "h"]` | Slices `[0]` and `[4]` settled |
| **1** | `1` | `3` | `'e'` | `'l'` | Swap $1 \leftrightarrow 3$ | `["o", "l", "l", "e", "h"]` | Slices `[0..1]` and `[3..4]` settled |
| **2** | `2` | `2` | `'l'` | `'l'` | $left \not< right \implies$ Exit | `["o", "l", "l", "e", "h"]` | Center unchanged; fully reversed |

Total swaps executed: $2 = \lfloor 5 / 2 \rfloor$.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Two-Pointer In-Place Pincer - Optimal):** $O(N)$ time, $O(1)$ space. Exactly $N / 2$ operations, zero memory allocations.
- **Approach 2 (Recursive Reversal):** Reverse outer pair, recurse on inner slice. Consumes $O(N)$ call-stack memory, risking stack overflow for $N = 10^5$. Never use in production; iterative two-pointers is strictly superior.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Input Validation:** If array is null or has length $\le 1$, return immediately.
- **Step 2: Boundary Setup:** Set `left = 0`, `right = s.Length - 1`.
- **Step 3: Convergence Loop:** While `left < right`, perform swap and shift pointers.
- **Step 4: Exit:** Method completes with array modified in-place.

#### 4.3 Alternative Approaches Analysis
- **C# Tuple Swap vs Temp Variable:** `(s[left], s[right]) = (s[right], s[left])` compiles to efficient IL register instructions equivalent to a classic 3-line temp swap, combining expressive clarity with hardware-level efficiency.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Dimension | Approach 1: In-Place Two Pointers (Optimal) | Approach 2: Recursive In-Place |
| :--- | :--- | :--- |
| **Time Complexity** | `O(N)` ($\lfloor N/2 \rfloor$ swaps) | `O(N)` ($\lfloor N/2 \rfloor$ swaps) |
| **Auxiliary Space** | `O(1)` strictly scalar | `O(N)` call stack frames ($10^5$ frames $\implies$ StackOverflow) |
| **GC Pressure** | Zero allocations | Zero heap allocations |
| **Cache Locality** | Sequential inward streaming from both ends | Sequential inward streaming |
| **Interview Defensibility**| 100% standard production pattern | Educational demonstration only |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Reverse String (#344)
// Core Pattern: Opposing Two-Pointer Inward Pincer
// Target Complexity: O(N) Time, O(1) Auxiliary Space
// Key Invariant: Slices [0..left-1] and [right+1..N-1] are fully reversed
// ============================================================================

public class Solution
{
    public void ReverseString(char[] s)
    {
        // --------------------------------------------------------------------
        // Defensive Guard: Handle null and trivial single-element arrays
        // --------------------------------------------------------------------
        if (s == null || s.Length <= 1)
        {
            return;
        }

        // Initialize inward converging pointers at array boundaries
        int left = 0;
        int right = s.Length - 1;

        // --------------------------------------------------------------------
        // Convergence Loop: Execute exactly Floor(N / 2) pairwise swaps
        // --------------------------------------------------------------------
        while (left < right)
        {
            // Invariant: Swap symmetric mirror elements across midpoint
            // C# tuple deconstruction emits optimal IL register swaps without heap churn
            (s[left], s[right]) = (s[right], s[left]);

            // Advance left toward center, decrement right toward center
            left++;
            right--;
        }
    }
}
```

---

## 25. Longest Common Prefix (LeetCode #14)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟢 Easy |
| **Priority** | ⚪ Reinforcement |
| **Pattern Tags** | `#string` `#vertical-scanning` `#horizontal-scanning` `#early-termination` |
| **LeetCode Link** | [Longest Common Prefix](https://leetcode.com/problems/longest-common-prefix/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Write a function to find the longest common prefix string amongst an array of strings. If there is no common prefix, return an empty string `""`.
- **Key Constraints:**
  - `1 <= strs.Length <= 200`
  - `0 <= strs[i].Length <= 200`
  - `strs[i]` consists of only lowercase English letters.
- **Senior Edge Cases to Defend:**
  - `strs.Length == 1`: The only string is its own common prefix; return `strs[0]`.
  - Empty string present in array (e.g. `strs = ["", "b"]`): Common prefix is immediately `""`.
  - No common prefix (e.g. `strs = ["dog", "racecar", "car"]`): First character mismatch returns `""`.
  - Variable string lengths: Must guard against `IndexOutOfRangeException` when one word is shorter than others.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Inspect characters column by column across all strings simultaneously (Vertical Scanning), terminating at the very first mismatch or string boundary.
- **Sample 1:**
  - **Input:** `strs = ["flower","flow","flight"]` $\implies$ `"fl"`
- **Sample 2:**
  - **Input:** `strs = ["dog","racecar","car"]` $\implies$ `""`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine a filing cabinet filled with punch cards, where each card contains a word. You stack all cards vertically so their first characters align at column 0. You drop an imaginary vertical alignment rod through column 0. If every single card has a matching hole for character `'f'` at column 0, the rod passes through, and you slide the rod to column 1. The very instant any single card either runs out of length or exhibits a mismatched letter, the rod is blocked: the common prefix ends immediately at that column.

#### 3.2 The Naive Bottleneck & Redundant Computation
**Horizontal Scanning Bottleneck:**
Horizontal scanning compares word 0 with word 1 to find common prefix $P_1$, then compares $P_1$ with word 2 to find $P_2$, and so on.
- Worst Case Flaw: Suppose `strs` contains 1,000 strings of length 1,000 that all match (`"aaaaa...aaaa"`), but the very last string is `"baaaaa..."`.
- Horizontal scanning performs $999 \times 1,000 \approx 10^6$ character comparisons across the first 999 strings before reaching the final string and discovering that the global common prefix is `""`.
- Vertical scanning inspects column 0 across all strings. When it reaches the last string at index 999 on column 0, it discovers the mismatch immediately and terminates after just 1,000 operations—a **$1,000\times$ reduction in redundant computation**.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**Vertical Scanning with Early-Exit Invariant:**
Let $S_0 = strs[0]$. For any column index $col \in [0, |S_0| - 1]$:
1. Target character: $c = S_0[col]$.
2. For all $row \in [1, strs.Length - 1]$:
   - If $col == strs[row].Length$: Word $row$ has ended; no longer prefix can exist.
   - If $strs[row][col] \ne c$: A character mismatch has occurred.
3. If either condition triggers, $S_0[0 \dots col - 1]$ is strictly the maximal common prefix.
4. If loop finishes through all columns of $S_0$, $S_0$ itself is the common prefix.

#### 3.4 Cursor Semantics & Invariant Partition Architecture
```text
           col: 0   1   2   3   4   5
 strs[0]:     [ f | l | o | w | e | r ]
 strs[1]:     [ f | l | o | w ]
 strs[2]:     [ f | l | i | g | h | t ]
                │   │   │
                │   │   └─ col = 2: 'o' != 'i' => TERMINATE! Return strs[0][0..1] = "fl"
                │   └───── col = 1: 'l' == 'l' == 'l' => Continue
                └───────── col = 0: 'f' == 'f' == 'f' => Continue
```
- `col`: Advances across columns of reference word `strs[0]`.
- `row`: Scans rows $1 \dots N - 1$ at the fixed column index `col`.

#### 3.5 State Transition Triggers & Decision Gates
1. Outer Loop: $col = 0 \dots strs[0].Length - 1$.
   - $targetChar = strs[0][col]$.
2. Inner Loop: $row = 1 \dots strs.Length - 1$.
   - **Boundary / Mismatch Gate:**
     If $col == strs[row].Length$ OR $strs[row][col] \ne targetChar$:
     Return $strs[0].Substring(0, col)$.
3. **Full Match Gate:** If outer loop completes, return $strs[0]$.

#### 3.6 Concrete Step-by-Step State Trace
Input: `strs = ["flower", "flow", "flight"]`. Reference: `"flower"`.

| Column $col$ | $targetChar$ | Word Checked | Word Char | Comparison | Result |
| :---: | :---: | :---: | :---: | :---: | :---: |
| **0** | `'f'` | `strs[1]` (`"flow"`) | `'f'` | `'f' == 'f'` | Match |
| **0** | `'f'` | `strs[2]` (`"flight"`) | `'f'` | `'f' == 'f'` | Match (Column 0 passed) |
| **1** | `'l'` | `strs[1]` (`"flow"`) | `'l'` | `'l' == 'l'` | Match |
| **1** | `'l'` | `strs[2]` (`"flight"`) | `'l'` | `'l' == 'l'` | Match (Column 1 passed) |
| **2** | `'o'` | `strs[1]` (`"flow"`) | `'o'` | `'o' == 'o'` | Match |
| **2** | `'o'` | `strs[2]` (`"flight"`) | `'i'` | `'o' \ne 'i'` | **MISMATCH!** |

Terminate immediately at $col = 2$.
Return `strs[0].Substring(0, 2)` $\implies$ `"fl"`.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Vertical Scanning - Optimal):** Optimal when common prefix is short or when mismatches occur early. Best-case time is $O(N \cdot \text{minLen})$, where $\text{minLen}$ is the length of the shortest string.
- **Approach 2 (Horizontal Scanning):** Progressively reduces candidate prefix using `prefix = prefix.Substring(...)`. Good for conceptual introduction, but performs redundant comparisons when prefix collapses late.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Input Validation:** Return `""` if array is empty; return `strs[0]` if length is 1.
- **Step 2: Outer Column Loop:** Iterate `col` from $0$ to `strs[0].Length - 1`.
- **Step 3: Inner Row Check:** For each string in `strs[1..N-1]`, verify length and character equality.
- **Step 4: Early Return:** On any mismatch, slice `strs[0]` up to `col`.
- **Step 5: Full Return:** If all columns pass, return `strs[0]`.

#### 4.3 Alternative Approaches Analysis
- **Divide and Conquer:** $\text{LCP}(S_1 \dots S_N) = \text{LCP}(\text{LCP}(S_1 \dots S_{N/2}), \text{LCP}(S_{N/2+1} \dots S_N))$. Time $O(S)$, Space $O(M \log N)$. Unnecessary recursion overhead.
- **Binary Search on Prefix Length:** Binary search on $L \in [0, \text{minLen}]$. Checks if all strings share a prefix of length $L$ in $O(N \cdot L)$. Total time $O(S \log M)$. Sub-optimal compared to vertical scanning.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Dimension | Approach 1: Vertical Scanning (Optimal) | Approach 2: Horizontal Scanning |
| :--- | :--- | :--- |
| **Time (Worst Case)** | `O(S)` where $S$ is sum of all characters | `O(S)` |
| **Time (Best Case)** | `O(N)` (terminates on first column mismatch) | `O(S)` (if mismatch is at last word) |
| **Auxiliary Space** | `O(1)` auxiliary (excluding return string) | `O(1)` auxiliary |
| **String Slicing Ops** | Exactly 1 substring operation at exit | Up to $N$ substring allocations |
| **Cache Locality** | Strided across strings (pointer dereferencing) | Sequential per string pair |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Longest Common Prefix (#14)
// Core Pattern: Vertical Column Scanning with Instant Early Exit
// Target Complexity: O(S) Worst Case, O(N * minLen) Best Case, O(1) Auxiliary Space
// Key Invariant: All examined columns [0 .. col-1] match across all strings
// ============================================================================

public class Solution
{
    // ------------------------------------------------------------------------
    // Approach 1: Vertical Scanning (Production Optimal)
    // ------------------------------------------------------------------------
    public string LongestCommonPrefix(string[] strs)
    {
        // Guard against null and empty input collections
        if (strs == null || strs.Length == 0)
        {
            return string.Empty;
        }

        // Single-string shortcut: String is identically its own prefix
        if (strs.Length == 1)
        {
            return strs[0];
        }

        // Iterate vertically: Column by column across the reference string strs[0]
        for (int col = 0; col < strs[0].Length; col++)
        {
            char targetChar = strs[0][col];

            // Compare column character across all remaining strings
            for (int row = 1; row < strs.Length; row++)
            {
                // Invariant Gate:
                // 1. Current string is shorter than col (boundary reached)
                // 2. Character at current column mismatches targetChar
                if (col == strs[row].Length || strs[row][col] != targetChar)
                {
                    // Instant early exit: Substring [0 .. col - 1] is the longest common prefix
                    return strs[0].Substring(0, col);
                }
            }
        }

        // If no mismatch was encountered, the entire reference string is the prefix
        return strs[0];
    }
}

// ----------------------------------------------------------------------------
// Approach 2: Horizontal Scanning (Iterative Prefix Shrinking)
// ----------------------------------------------------------------------------
public class SolutionHorizontal
{
    public string LongestCommonPrefix(string[] strs)
    {
        if (strs == null || strs.Length == 0) return string.Empty;

        string prefix = strs[0];

        for (int i = 1; i < strs.Length; i++)
        {
            // Progressively trim prefix until strs[i] starts with prefix
            while (!strs[i].StartsWith(prefix))
            {
                prefix = prefix.Substring(0, prefix.Length - 1);
                if (string.IsNullOrEmpty(prefix))
                {
                    return string.Empty;
                }
            }
        }

        return prefix;
    }
}
```

---

## 26. String Compression (LeetCode #443)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | 🔥 High |
| **Pattern Tags** | `#two-pointers` `#in-place-compaction` `#run-length-encoding` `#reader-writer` |
| **LeetCode Link** | [String Compression](https://leetcode.com/problems/string-compression/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given an array of characters `chars`, compress it using the following algorithm: Begin with an empty string `s`. For each group of consecutive repeating characters in `chars`:
  - If the group's length is `1`, append the character to `s`.
  - Otherwise, append the character followed by the group's length.
  The compressed string `s` should not be returned separately, but instead, be stored in the input character array `chars`. Note that group lengths that are 10 or longer will be split into multiple characters in `chars`. You must write an algorithm that uses only $O(1)$ extra space.
- **Key Constraints:**
  - `1 <= chars.Length <= 2000`
  - `chars[i]` is a lowercase English letter, uppercase English letter, digit, or symbol.
- **Senior Edge Cases to Defend:**
  - Groups of length 1 (e.g. `['a', 'b', 'c']`): Count is NOT appended; array remains `['a', 'b', 'c']`.
  - Multi-digit counts (e.g. 12 `'a'`s): Must serialize digits individually: `['a', '1', '2']`.
  - In-place overwrite safety: Scribe pointer `write` must NEVER overwrite an unread element at `read`.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Two-pointer Reader-Writer architecture: `read` scans consecutive runs, while `write` overwrites compressed characters and count digits in-place.
- **Sample 1:**
  - **Input:** `chars = ["a","a","b","b","c","c","c"]`
  - **Output:** Return `6`, with `chars[0..5] = ["a","2","b","2","c","3"]`.
- **Sample 2:**
  - **Input:** `chars = ["a","b","b","b","b","b","b","b","b","b","b","b","b"]` (one 'a', twelve 'b's)
  - **Output:** Return `4`, with `chars[0..3] = ["a","b","1","2"]`.

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine a magnetic tape head mechanism with two independently moving heads on the same tape: a **Reader Head** and a **Writer Head**.
- The Reader Head races forward to measure the length of each contiguous block of identical symbols.
- The Writer Head follows behind, stamping down the symbol and its count digits onto the tape.
- Because a block of $L$ identical characters is always compressed into $\le L$ slots, the tape behind the Reader Head always has enough slack for the Writer Head. The Writer Head never overtakes the Reader Head, guaranteeing zero data corruption.

#### 3.2 The Naive Bottleneck & Redundant Computation
A naive approach allocates a `StringBuilder`, appends characters and counts, and copies them back into `chars`:
- Auxiliary Space: $O(N)$ heap memory.
- Violates the explicit $O(1)$ space constraint of the problem specification.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**The Non-Overwriting Reader-Writer Invariant ($write \le read$):**
Let a run of identical characters have length $L \ge 1$.
The compressed representation requires:
$$\text{Slots Needed}(L) = \begin{cases} 1 & \text{if } L = 1 \\ 1 + \lfloor \log_{10} L \rfloor + 1 & \text{if } L \ge 2 \end{cases}$$
Analyzing each case:
- For $L = 1$: Slots needed = $1 \le 1$.
- For $L \in [2, 9]$: Slots needed = $1 + 1 = 2 \le L$.
- For $L \in [10, 99]$: Slots needed = $1 + 2 = 3 \le L$ (since $3 \le 10$).
- For $L \ge 100$: Slots needed = $1 + 3 = 4 \le 100$.
In all possible cases:
$$\text{Slots Needed}(L) \le L$$
Since every group of length $L$ consumes at most $L$ write positions, the invariant:
$$write \le read$$
holds strictly at every moment during execution. The writer head will **never overwrite an unread input character**.

#### 3.4 Cursor Semantics & Invariant Partition Architecture
```text
[ 0 ......... write - 1 ]   [ write ... read - 1 ]   [ read ......... N - 1 ]
└───────────┬───────────┘   └──────────┬─────────┘   └──────────┬───────────┘
   Finalized Compressed           Dead Slack Area          Unread Pending
      Output Stream              (Safe to Overwrite)         Input Stream
```
- `read`: Explorer cursor identifying contiguous character boundaries.
- `write`: Scribe cursor writing characters and serialized digits.
- `write <= read`: Universal safety invariant.

#### 3.5 State Transition Triggers & Decision Gates
1. Record current character: `currentChar = chars[read]`.
2. Count run length: While `read < N && chars[read] == currentChar`, `read++`, `count++`.
3. Commit character: `chars[write++] = currentChar`.
4. **Digit Serialization Gate:**
   - If `count > 1`: Convert `count` to digits and write each into `chars[write++]`.
5. Return `write` (the new logical length of the array).

#### 3.6 Concrete Step-by-Step State Trace
Input: `chars = ["a","a","b","b","c","c","c"]`. $N = 7$.

| Run | `currentChar` | `read` Start | `read` End | `count` | Written to `chars[write]` | `write` Position | Slack $(read - write)$ |
| :---: | :---: | :---: | :---: | :---: | :---: | :---: | :---: |
| **1** | `'a'` | 0 | 2 | 2 | `'a'`, `'2'` | $0 \to 1 \to 2$ | $2 - 2 = 0$ |
| **2** | `'b'` | 2 | 4 | 2 | `'b'`, `'2'` | $2 \to 3 \to 4$ | $4 - 4 = 0$ |
| **3** | `'c'` | 4 | 7 | 3 | `'c'`, `'3'` | $4 \to 5 \to 6$ | $7 - 6 = 1$ |

Final `write` = 6. Array prefix `chars[0..5]` = `["a","2","b","2","c","3"]`.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Two-Pointer In-Place Compaction - Optimal):** $O(N)$ single pass, $O(1)$ space. Operates directly on the input buffer; zero heap allocations.
- **Digit Formatting Note:** Converting `count` to digits can be done via `count.ToString()` (small allocation) or branchless integer arithmetic (zero allocation). In production, integer arithmetic with a small stack buffer (`Span<char>`) provides absolute zero GC pressure.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Setup:** Initialize `write = 0`, `read = 0`.
- **Step 2: Group Exploration:** While `read < chars.Length`:
  - Store `currentChar = chars[read]`.
  - Inner while loop counts occurrences of `currentChar`.
- **Step 3: Write Character:** `chars[write++] = currentChar`.
- **Step 4: Serialize Count:** If count > 1, extract digits and write them into `chars`.
- **Step 5: Return:** Return `write`.

#### 4.3 Alternative Approaches Analysis
- **Stack-Based Digit Formatting:**
  To write multi-digit counts without `ToString()`:
  Extract digits via `% 10` into a small stack buffer (e.g. `stackalloc char[10]`), reverse them, and copy to `chars`. Completely avoids string instantiation.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Dimension | Approach 1: In-Place Compaction (Zero-Alloc) | Approach 2: StringBuilder Intermediate |
| :--- | :--- | :--- |
| **Time Complexity** | `O(N)` strictly linear | `O(N)` |
| **Auxiliary Space** | `O(1)` (stack buffer $\le 10$ chars) | `O(N)` heap memory |
| **GC Pressure** | Zero garbage generated | High heap allocations |
| **Cache Locality** | Optimal single-pass cache-line reuse | Multiple buffer copies |
| **Streaming Suitability** | Yes (can stream blocks in real-time) | No (must buffer entire output) |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: String Compression (#443)
// Core Pattern: Two-Pointer Reader-Writer In-Place Compaction
// Target Complexity: O(N) Time, O(1) Auxiliary Space
// Key Invariant: write <= read at all times (preventing premature overwrite)
// ============================================================================

public class Solution
{
    public int Compress(char[] chars)
    {
        // --------------------------------------------------------------------
        // Defensive Guard: Validate input existence
        // --------------------------------------------------------------------
        if (chars == null || chars.Length == 0)
        {
            return 0;
        }

        int write = 0; // Scribe pointer: writes compressed output
        int read = 0;  // Explorer pointer: scans consecutive runs

        // --------------------------------------------------------------------
        // Main Exploration Loop: Process contiguous character runs
        // --------------------------------------------------------------------
        while (read < chars.Length)
        {
            char currentChar = chars[read];
            int groupStart = read;

            // Invariant: Scan forward until a different character or end of array is reached
            while (read < chars.Length && chars[read] == currentChar)
            {
                read++;
            }

            int count = read - groupStart;

            // Always write the character header
            chars[write++] = currentChar;

            // ----------------------------------------------------------------
            // Invariant: If count > 1, serialize count digits in-place
            // Use stackalloc buffer to achieve STRICT ZERO HEAP ALLOCATION
            // ----------------------------------------------------------------
            if (count > 1)
            {
                // Temporary buffer for integer serialization (max 10 digits for 32-bit int)
                Span<char> digitsBuffer = stackalloc char[10];
                int digitCount = 0;

                // Extract digits in reverse order (least significant first)
                int temp = count;
                while (temp > 0)
                {
                    digitsBuffer[digitCount++] = (char)('0' + (temp % 10));
                    temp /= 10;
                }

                // Write digits in correct most-significant-first order
                for (int d = digitCount - 1; d >= 0; d--)
                {
                    chars[write++] = digitsBuffer[d];
                }
            }
        }

        // Return the new logical length of the compressed array
        return write;
    }
}
```

---

## 27. Longest Palindromic Substring (LeetCode #5)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#expand-around-center` `#2d-dp` `#palindrome` `#string` |
| **LeetCode Link** | [Longest Palindromic Substring](https://leetcode.com/problems/longest-palindromic-substring/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given a string `s`, return the longest palindromic substring in `s`.
- **Key Constraints:**
  - `1 <= s.Length <= 1000`
  - `s` consists of only digits and English letters.
- **Senior Edge Cases to Defend:**
  - `s.Length == 1`: Entire string is a palindrome; return `s`.
  - All identical characters (e.g. `"aaaa"`): Expands to full string length.
  - Even length palindromes (e.g. `"cbbd"` $\implies$ `"bb"`): Must correctly test centers between adjacent characters.
  - Multiple palindromes of same max length: Any one valid maximal palindrome is acceptable per LeetCode specification.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Expand symmetrically outward from all $2N - 1$ potential symmetry centers (both single-character and between-character pairs) in $O(1)$ auxiliary space.
- **Sample 1:**
  - **Input:** `s = "babad"` $\implies$ `"bab"` (or `"aba"`)
- **Sample 2:**
  - **Input:** `s = "cbbd"` $\implies$ `"bb"`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine dropping a pebble into a calm pond. Concentric ripples radiate outward from the impact point. A palindromic substring possesses bilateral symmetry radiating from a central axis:
- If the palindrome length is odd (e.g. `"aba"`), the axis is a single character (`'b'`).
- If the palindrome length is even (e.g. `"abba"`), the axis is the empty boundary between two adjacent identical characters (`'b' | 'b'`).
Across a string of length $N$, there are exactly $N$ single-character centers and $N - 1$ between-character centers, giving **$2N - 1$ total candidate centers**. By expanding outward from each center like concentric ripples, we identify all maximal palindromes.

#### 3.2 The Naive Bottleneck & Redundant Computation
A brute-force scan tests every substring $s[i..j]$ ($0 \le i \le j < N$):
- Substring count: $\frac{N(N+1)}{2} = O(N^2)$.
- Palindrome validation per substring: $O(N)$.
- Total Time: $O(N^3)$.
- For $N = 1000$, $N^3 = 10^9$ operations $\implies$ guaranteed TLE.
- Redundancy: Testing `"abacaba"` from scratch repeats the work already done when testing `"bacab"` and `"aca"`.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**Expand Around Center Invariant:**
A substring $s[L \dots R]$ is a palindrome if and only if:
1. The inner substring $s[L + 1 \dots R - 1]$ is a palindrome, AND
2. $s[L] == s[R]$.
By starting at the center and expanding outward:
$$\text{While } left \ge 0 \land right < N \land s[left] == s[right] \implies left--, \quad right++$$
The very first time $s[left] \ne s[right]$, **expansion terminates immediately**.
- Why is early termination safe? If $s[left] \ne s[right]$, NO larger substring centered at this axis can ever be a palindrome. We safely skip all further expansions for this center.
- **Palindrome Length Formula:** When the while loop breaks, the valid palindrome boundaries were $[left + 1 \dots right - 1]$. Its length is:
  $$\text{Length} = (right - 1) - (left + 1) + 1 = right - left - 1$$

#### 3.4 Cursor Semantics & Invariant Partition Architecture
```text
s: ... [ left - 1 ]  [ left ... center ... right ]  [ right + 1 ] ...
            ▲        └─────────────┬─────────────┘        ▲
       Mismatch /               Validated             Mismatch /
       Boundary                Palindrome              Boundary
```
- `left`, `right`: Outward expanding cursors.
- `start`, `maxLen`: Global anchors recording the best palindrome slice found.

#### 3.5 State Transition Triggers & Decision Gates
1. Outer Loop: $i = 0 \dots N - 1$.
2. **Odd Expansion Gate:** Call `Expand(i, i)`.
3. **Even Expansion Gate:** Call `Expand(i, i + 1)`.
4. In `Expand`:
   - While $left \ge 0 \land right < N \land s[left] == s[right]$: $left--$, $right++$.
   - Valid length: $len = right - left - 1$.
   - If $len > maxLen$: `maxLen = len`, `bestStart = left + 1`.

#### 3.6 Concrete Step-by-Step State Trace
Input: `s = "babad"`.

| Center $i$ | Type | Initial $(L, R)$ | Expansion Steps $(s[L] == s[R])$ | Loop Termination $(L, R)$ | Length ($R - L - 1$) | Substring | $maxLen$ |
| :---: | :---: | :---: | :---: | :---: | :---: | :---: | :---: |
| **0** | Odd | (0, 0) | `'b' == 'b'` | (-1, 1) | $1 - (-1) - 1 = 1$ | `"b"` | 1 |
| **0** | Even | (0, 1) | `'b' \ne 'a'` | (0, 1) | $1 - 0 - 1 = 0$ | — | 1 |
| **1** | Odd | (1, 1) | `'a'=='a'`, then `'b'=='b'` | (-1, 3) | $3 - (-1) - 1 = 3$ | **`"bab"`** | **3** |
| **1** | Even | (1, 2) | `'a' \ne 'b'` | (1, 2) | 0 | — | 3 |
| **2** | Odd | (2, 2) | `'b'=='b'`, then `'a'=='a'` | (0, 4) | $4 - 0 - 1 = 3$ | `"aba"` | 3 |
| **2** | Even | (2, 3) | `'b' \ne 'a'` | (2, 3) | 0 | — | 3 |
| **3** | Odd | (3, 3) | `'a' == 'a'` | (2, 4) | 1 | `"a"` | 3 |
| **4** | Odd | (4, 4) | `'d' == 'd'` | (3, 5) | 1 | `"d"` | 3 |

Final Result: `"bab"` (or `"aba"`), length 3.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Expand Around Center - Optimal Production Standard):** $O(N^2)$ time, $O(1)$ auxiliary space. Zero heap memory allocation during search; highly cache-friendly.
- **Approach 2 (2D Dynamic Programming):** Computes $dp[i, j] = (s[i] == s[j] \land dp[i+1, j-1])$. Consumes $O(N^2)$ auxiliary memory (a $1000 \times 1000$ bool matrix = 1 MB heap allocation). Suffers from cache misses.
- **Approach 3 (Manacher's Algorithm - Theoretical Advanced):** Achieves $O(N)$ linear time by reusing symmetry across previously computed palindrome boundaries. For $N \le 1000$, Expand Around Center executes in $\approx 2 \text{ ms}$ and is far easier to write and defend in an interview without bug risk.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Input Validation:** Return `s` if length $\le 1$.
- **Step 2: Initialize Anchors:** Set `start = 0`, `maxLen = 0`.
- **Step 3: Center Traversal:** Loop $i = 0 \dots N - 1$:
  - Expand odd center: `Expand(s, i, i, ref start, ref maxLen)`.
  - Expand even center: `Expand(s, i, i + 1, ref start, ref maxLen)`.
- **Step 4: Substring Extraction:** Return `s.Substring(start, maxLen)`.

#### 4.3 Alternative Approaches Analysis
- **Dynamic Programming Table Formulation:**
  Base cases: substrings of length 1 are true; substrings of length 2 are true if $s[i] == s[i+1]$. For lengths $3 \dots N$: $dp[i, j] = dp[i+1, j-1] \land (s[i] == s[j])$. Space complexity $O(N^2)$ is significantly worse than $O(1)$.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Dimension | Approach 1: Expand Around Center (Optimal) | Approach 2: 2D Dynamic Programming | Approach 3: Manacher's Algorithm |
| :--- | :--- | :--- | :--- |
| **Time Complexity** | `O(N^2)` worst-case | `O(N^2)` deterministic | `O(N)` linear |
| **Auxiliary Space** | `O(1)` strictly scalar | `O(N^2)` boolean matrix | `O(N)` radius array |
| **Implementation Complexity**| Low (20 lines, intuitive) | Medium (table construction) | High (virtual delimiter insertion) |
| **Cache Overhead** | Near-zero (local L1 access) | High (strided matrix indexing) | Low |
| **Interview Defensibility** | Highest | Good theoretical value | Overkill for $N \le 1000$ |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Longest Palindromic Substring (#5)
// Core Pattern: Expand Around Center (2N - 1 Symmetry Axes)
// Target Complexity: O(N^2) Worst-Case Time, O(1) Auxiliary Space
// Key Invariant: Expansion stops immediately upon first character mismatch
// ============================================================================

public class Solution
{
    // ------------------------------------------------------------------------
    // Approach 1: Expand Around Center (Production Optimal)
    // ------------------------------------------------------------------------
    public string LongestPalindrome(string s)
    {
        // Guard against null and trivial single-character strings
        if (string.IsNullOrEmpty(s) || s.Length <= 1)
        {
            return s ?? string.Empty;
        }

        int start = 0;
        int maxLen = 0;

        // Traverse all 2N - 1 candidate symmetry axes
        for (int i = 0; i < s.Length; i++)
        {
            // Case 1: Odd length palindrome (centered at single character i)
            Expand(s, i, i, ref start, ref maxLen);

            // Case 2: Even length palindrome (centered between i and i + 1)
            Expand(s, i, i + 1, ref start, ref maxLen);
        }

        // Substring extraction occurs exactly once at method return
        return s.Substring(start, maxLen);
    }

    private static void Expand(string s, int left, int right, ref int bestStart, ref int maxLen)
    {
        // Invariant: Symmetrically expand outward as long as characters match
        while (left >= 0 && right < s.Length && s[left] == s[right])
        {
            left--;
            right++;
        }

        // Mathematical length derivation:
        // Valid palindrome boundaries are [left + 1 .. right - 1]
        // Length = (right - 1) - (left + 1) + 1 = right - left - 1
        int length = right - left - 1;

        if (length > maxLen)
        {
            maxLen = length;
            bestStart = left + 1;
        }
    }
}

// ----------------------------------------------------------------------------
// Approach 2: 2D Dynamic Programming Table (O(N^2) Time, O(N^2) Space)
// ----------------------------------------------------------------------------
public class Solution2DDP
{
    public string LongestPalindrome(string s)
    {
        if (string.IsNullOrEmpty(s) || s.Length <= 1) return s ?? string.Empty;

        int n = s.Length;
        bool[,] dp = new bool[n, n];
        int start = 0;
        int maxLen = 1;

        // Base Case 1: Single characters are palindromes
        for (int i = 0; i < n; i++) dp[i, i] = true;

        // Base Case 2: Substrings of length 2
        for (int i = 0; i < n - 1; i++)
        {
            if (s[i] == s[i + 1])
            {
                dp[i, i + 1] = true;
                start = i;
                maxLen = 2;
            }
        }

        // Inductive Step: Substrings of length 3 to n
        for (int len = 3; len <= n; len++)
        {
            for (int i = 0; i <= n - len; i++)
            {
                int j = i + len - 1;

                // Substring s[i..j] is palindrome if outer chars match AND inner slice is palindrome
                if (s[i] == s[j] && dp[i + 1, j - 1])
                {
                    dp[i, j] = true;
                    start = i;
                    maxLen = len;
                }
            }
        }

        return s.Substring(start, maxLen);
    }
}
```

---

## 28. Palindromic Substrings (LeetCode #647)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | 🔥 High |
| **Pattern Tags** | `#expand-around-center` `#palindrome-counting` `#dp` |
| **LeetCode Link** | [Palindromic Substrings](https://leetcode.com/problems/palindromic-substrings/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given a string `s`, return the number of palindromic substrings in it. A substring is a contiguous sequence of characters within the string. A string is a palindrome when it reads the same backward as forward.
- **Key Constraints:**
  - `1 <= s.Length <= 1000`
  - `s` consists of lowercase English letters.
- **Senior Edge Cases to Defend:**
  - Single character string (e.g. `s = "a"`): Returns `1`.
  - All identical characters (e.g. `s = "aaa"`): Total substrings is $\frac{N(N+1)}{2} = \frac{3 \times 4}{2} = 6$. All are palindromes; must count every one.
  - No multi-character palindromes (e.g. `s = "abc"`): Every single character is an odd palindrome of length 1; returns `3`.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Count total palindromic substrings by accumulating the number of valid expansion steps across all $2N - 1$ symmetry centers in $O(1)$ auxiliary space.
- **Sample 1:**
  - **Input:** `s = "abc"` $\implies$ `3` (`"a"`, `"b"`, `"c"`)
- **Sample 2:**
  - **Input:** `s = "aaa"` $\implies$ `6` (`"a"`, `"a"`, `"a"`, `"aa"`, `"aa"`, `"aaa"`)

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Picture concentric rings radiating from a symmetry pole. Each time you stand at a center and step one character outward to the left and one character to the right, and discover that $s[left] == s[right]$, you have discovered **exactly one brand-new, unique palindromic substring**. 
For example, if you stand at center `'b'` in `"abcba"`:
- Step 0: `"b"` is a valid palindrome (+1).
- Step 1: $s[left] == s[right]$ ('c' == 'c') $\implies$ `"cbc"` is a valid palindrome (+1).
- Step 2: $s[left] == s[right]$ ('a' == 'a') $\implies$ `"abcba"` is a valid palindrome (+1).
Because every step outwards from any of the $2N - 1$ centers produces a uniquely bounded substring $[left, right]$, no duplicate counting can ever occur.

#### 3.2 The Naive Bottleneck & Redundant Computation
A brute-force scan enumerates all $\frac{N(N+1)}{2}$ substrings and checks each for palindrome symmetry:
- Substring generation: $O(N^2)$.
- Verification: $O(N)$.
- Total Time: $O(N^3)$.
- For $N = 1000$, $10^9$ operations cause a TLE.
- Redundancy: Checking if $s[i..j]$ is a palindrome ignores the fact that if $s[i+1..j-1]$ is not a palindrome, $s[i..j]$ cannot possibly be one either.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**Exhaustive Center Partitioning Invariant:**
Every palindromic substring in $s$ has a unique center:
- An odd-length palindrome of length $2k + 1$ has center at integer index $c$.
- An even-length palindrome of length $2k$ has center between indices $c$ and $c + 1$.
Since there are exactly $N$ odd centers and $N - 1$ even centers:
$$\text{Total Centers} = 2N - 1$$
Expanding outward from each center:
- If $s[left] == s[right]$: Add 1 to total palindrome count, expand further.
- If $s[left] \ne s[right]$: Terminate expansion for this center.
Since each valid expansion corresponds to a unique $(left, right)$ coordinate pair, summing the successful expansion steps across all $2N - 1$ centers yields the exact total count without sets or hash tables.

#### 3.4 Cursor Semantics & Invariant Partition Architecture
```text
Center (i, i) or (i, i + 1):
Radius r = 0: s[center] == s[center]                     => count++
Radius r = 1: s[center - 1] == s[center + 1]             => count++
Radius r = 2: s[center - 2] != s[center + 2]             => MISMATCH! Stop.
```
- `i`: Sweeps all center candidates $0 \dots N - 1$.
- `CountAroundCenter(left, right)`: Counts and returns valid expansion layers for that specific axis.

#### 3.5 State Transition Triggers & Decision Gates
1. Outer Loop: $i = 0 \dots N - 1$.
2. For each $i$:
   - `totalCount += CountAroundCenter(s, i, i)` (Odd palindromes)
   - `totalCount += CountAroundCenter(s, i, i + 1)` (Even palindromes)
3. In `CountAroundCenter`:
   - While $left \ge 0 \land right < N \land s[left] == s[right]$:
     - `count++`.
     - `left--`, `right++`.
   - Return `count`.

#### 3.6 Concrete Step-by-Step State Trace
Input: `s = "aaa"`. $N = 3$.

| Center $i$ | Type | Initial $(L, R)$ | Expansion Steps | Palindromes Found | Cumulative Total |
| :---: | :---: | :---: | :--- | :--- | :---: |
| **0** | Odd $(0, 0)$ | $(0, 0)$ | $s[0]=='a' \implies$ (0,0) valid | `"a"` | 1 |
| **0** | Even $(0, 1)$ | $(0, 1)$ | $s[0]==s[1]=='a' \implies$ (0,1) valid | `"aa"` (indices 0..1) | 2 |
| **1** | Odd $(1, 1)$ | $(1, 1)$ | $s[1]=='a'$ (+1), $s[0]==s[2]=='a'$ (+1) | `"a"`, `"aaa"` | 4 |
| **1** | Even $(1, 2)$ | $(1, 2)$ | $s[1]==s[2]=='a' \implies$ (1,2) valid | `"aa"` (indices 1..2) | 5 |
| **2** | Odd $(2, 2)$ | $(2, 2)$ | $s[2]=='a' \implies$ (2,2) valid | `"a"` | **6** |
| **2** | Even $(2, 3)$ | $(2, 3)$ | $right = 3 \ge N \implies$ Invalid | — | **6** |

Total Palindromes: `6`.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Expand Around Center - Optimal):** $O(N^2)$ time, $O(1)$ space. Strict scalar tracking; zero heap memory allocations.
- **Approach 2 (2D Dynamic Programming):** Boolean table $dp[i, j]$ where `count` increments each time $dp[i, j]$ is evaluated to `true`. Space complexity is $O(N^2)$. Approach 1 is strongly preferred for $O(1)$ space.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Input Validation:** Return 0 if string is empty.
- **Step 2: Initialize Accumulator:** `totalCount = 0`.
- **Step 3: Dual Expansion Loop:** For $i = 0 \dots N - 1$:
  - Accumulate odd expansions: `CountAroundCenter(s, i, i)`.
  - Accumulate even expansions: `CountAroundCenter(s, i, i + 1)`.
- **Step 4: Return:** Return `totalCount`.

#### 4.3 Alternative Approaches Analysis
- **Manacher's Algorithm Extension:** Palindromic radii array from Manacher's algorithm can count palindromes in $O(N)$ time: for each position, the count of palindromes centered there is $\lceil \text{radius} / 2 \rceil$. While asymptotically optimal ($O(N)$), the Expand Around Center approach ($O(N^2)$) runs in under 3 ms on $N \le 1000$ and is significantly less error-prone.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Dimension | Approach 1: Expand Around Center (Optimal) | Approach 2: 2D Dynamic Programming |
| :--- | :--- | :--- |
| **Time Complexity** | `O(N^2)` worst-case | `O(N^2)` |
| **Auxiliary Space** | `O(1)` strictly scalar | `O(N^2)` boolean matrix |
| **Allocation Footprint**| Zero heap memory | Allocates $10^6$ booleans for $N = 1000$ |
| **Early Termination** | Stops expansion on first mismatch | Evaluates full length DP transitions |
| **Cache Locality** | Sequential inward/outward expansion | Strided 2D matrix traversal |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Palindromic Substrings (#647)
// Core Pattern: Symmetry Center Expansion & Palindrome Layer Counting
// Target Complexity: O(N^2) Time, O(1) Auxiliary Space
// Key Invariant: Each valid expansion step corresponds to exactly one unique palindrome
// ============================================================================

public class Solution
{
    public int CountSubstrings(string s)
    {
        // --------------------------------------------------------------------
        // Defensive Guard: Handle null and empty inputs
        // --------------------------------------------------------------------
        if (string.IsNullOrEmpty(s))
        {
            return 0;
        }

        int totalPalindromes = 0;

        // --------------------------------------------------------------------
        // Sweep all 2N - 1 candidate symmetry axes
        // --------------------------------------------------------------------
        for (int i = 0; i < s.Length; i++)
        {
            // Case 1: Odd-length palindromes (single-character axis at index i)
            totalPalindromes += CountAroundCenter(s, i, i);

            // Case 2: Even-length palindromes (between-character axis at i and i + 1)
            totalPalindromes += CountAroundCenter(s, i, i + 1);
        }

        return totalPalindromes;
    }

    private static int CountAroundCenter(string s, int left, int right)
    {
        int count = 0;

        // Invariant:
        // While left and right boundaries are within string bounds and characters match,
        // we have confirmed exactly ONE new palindromic substring enclosing the center.
        while (left >= 0 && right < s.Length && s[left] == s[right])
        {
            count++;

            // Expand outward to test next concentric symmetry layer
            left--;
            right++;
        }

        return count;
    }
}
```
