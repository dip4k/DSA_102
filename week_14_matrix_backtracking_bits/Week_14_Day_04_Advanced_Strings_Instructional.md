# 📅 Week 14, Day 4: Advanced Preprocessed Strings, KMP Matching, Tries & Radix Sorting

Welcome to Day 4. Today, we bridge the gap between high-level string manipulation patterns and low-level physical byte stream engines. We study non-backtracking text searches, prefix tree representations, suffix-based index arrays, non-comparison radix sorting layouts, and multi-pattern concurrent compilation state automatons.

---

## 🎯 Learning Objectives
*   Master the Knuth-Morris-Pratt (KMP) linear scan matching algorithm and precalculate LPS failure tables.
*   Implement character Tries and understand compressed path representations (Radix Trees).
*   Deconstruct Radix Sorting mechanisms (LSD / MSD layouts) and implement 3-way String Quicksort.
*   Analyze Suffix Arrays and Suffix Trees, including LCP array construction using Kasai's algorithm.
*   Build the Aho-Corasick multi-pattern state machine using BFS failure links.

---

## 📘 Chapter 1: Context and Motivation

### 1. The Core Engineering Challenge
In standard computer systems, strings are contiguous arrays of character codes (bytes or double-byte words). Looking for a pattern P of length M inside a text T of length N using naive sliding iterations can degenerate to O(N * M) time complexity. When scanning gigabytes of text streams, genome files, or network buffers under high-throughput requirements, quadratic delays are completely unacceptable. 

To achieve optimal performance, we must construct algorithms that complete matching passes in linear O(N + M) time. By preprocessing patterns into static transition states, we compile our search constraints into mathematical state transitions. This avoids backing up the main text pointer.

Additionally, standard comparison-based sorting algorithms (like quicksort or mergesort) are bound by the lower limit of O(N log N) comparison steps. For strings of length L, these operations degrade to O(L * N log N) because comparing two strings requires comparing their character bytes. We can bypass these sorting limits entirely by using digit-by-digit or character-by-character non-comparison bucketing methods, such as Radix Sorting.

### 2. Naive Pitfalls
A naive search algorithm shifts the pattern forward by exactly one position on a mismatch and re-scans the text from the beginning of the alignment. For deeply repeating patterns like:
*   `Text` = `"AAAAAAAB"`
*   `Pattern` = `"AAAAB"`

The naive scanner repeatedly advances a few characters, encounters a mismatch on `'B'`, and then backtracks the main text pointer, matching the same characters over and over again.

### 3. Real-world Anchor: Grep Engines & Router Forwarding
*   **Command Line Tools**: Modern command line search tools (such as `grep` or `ripgrep`) combine KMP, Boyer-Moore, or Aho-Corasick state machines to scan large files without pointer backups.
*   **Networking Hardware**: High-frequency internet routers match destination IP packages against routing tables containing thousands of network prefixes using compressed tries (Radix Trees) implemented directly in physical registers.

---

## 📘 Chapter 2: Mental Model

### 1. State Jumps and Prefix Collapsing Trees
*   **The KMP State Machine**: Instead of backtracking the search pointer in the main text on a mismatch, compile the search pattern into a state transition table (LPS Table). The table tracks the length of matching suffixes and points the scanner index to the next best fallback state.

```text
Pattern:  "A  B  A  B  C"
Index:     0  1  2  3  4
LPS:      [0, 0, 1, 2, 0]  <-- LPS table stores length of longest prefix matching proper suffix
```

*   **Trie Prefix Trees**: Merges shared prefixes of multiple words into a single search tree to save space and speed up matches.

```text
                     ( Root )
                        |
                       [C]   - Node C
                        |
                       [A]   - Shared Prefix Node "CA"
                      / | \
                    [T][B][R] - Child Leaf word pointers ("CAT", "CAB", "CAR")
```

*   **Radix Trees (Compressed Tries)**: Merges adjacent nodes with single children into a single string path, reducing the total node count and pointer sizes.

```text
                     ( Root )
                        |
                     [  C A  ]   - Shared compressed path
                       / | \
                     [T][B][R]
```

*   **Suffix Arrays**: Under large-scale indexing requirements (like multiple pattern searches in a static document T), we represent all suffix substrings of a string in sorted alphabetical order. This lets us locate patterns inside the static document in logarithmic time using binary search.

*   **Aho-Corasick Automaton**: Integrates a prefix search Trie with KMP failure links. It uses a Breadth-First Search (BFS) queue to construct fallback paths, allowing you to search for multiple patterns simultaneously in a single linear pass over the text stream.

---

### 🧵 Advanced Preprocessed Strings Taxonomy

| Pattern / Structure | Time Complexity | Auxiliary Space | Key Capabilities |
| :--- | :--- | :--- | :--- |
| **KMP Matching** | O(N + M) | O(M) | Checks single pattern P iteratively without backing up text scan pointer |
| **Trie (Prefix)** | Insertion: O(L) | O(sum L_i) | Preforms fast dictionary auto-complete and spell checks |
| **Radix Sort** | O(N * L) | O(N + \Sigma) | Sorts fixed-width digits or strings stably without direct comparisons |
| **3-Way String Quicksort** | O(N * L) average | O(L + log N) | Recursive 3-way partitioning based on character column pivots |
| **Suffix Array** | Construction: O(N log N) | O(N) | Compact index representing all sorted suffix positions in a document |
| **Aho-Corasick** | O(N + sum(M_i) + Z) | O(sum(M_i)) | Matches multiple pattern candidates simultaneously in a single stream pass |

---

## ⚙️ CHAPTER 3: MECHANICS & STEP-BY-STEP IMPLEMENTATIONS

### 1. Knuth-Morris-Pratt (KMP) Mechanism

Let's trace building the **Longest Prefix Suffix (LPS)** table for the pattern `"ABABC"`.

*   `lps[0] = 0` (The single character proper suffix cannot have a prefix match). Initialize state trackers `len = 0` (previous prefix length) and index pointer `i = 1`.
*   `i = 1`: `pattern[1]` (`'B'`) != `pattern[len]` (`pattern[0] = 'A'`). Since `len == 0`, set `lps[1] = 0` and increment `i` to 2.
*   `i = 2`: `pattern[2]` (`'A'`) == `pattern[len]` (`pattern[0] = 'A'`). Match found! Increment `len` to 1, assign `lps[2] = 1`, and increment `i` to 3.
*   `i = 3`: `pattern[3]` (`'B'`) == `pattern[len]` (`pattern[1] = 'B'`). Match found! Increment `len` to 2, assign `lps[3] = 2`, and increment `i` to 4.
*   `i = 4`: `pattern[4]` (`'C'`) != `pattern[len]` (`pattern[2] = 'A'`). Mismatch. Since `len > 0`, we shift `len` back to the precalculated fallback value: `len = lps[len - 1] = lps[1] = 0`.
*   `i = 4`: Compare again with `len = 0`. `pattern[4]` (`'C'`) != `pattern[0]` (`'A'`). Set `lps[4] = 0` and increment `i` to 5.
*   **Resulting LPS State Table**: `[0, 0, 1, 2, 0]`

---

### KMP Pattern Matcher Implementation (C#)
```csharp
using System;
using System.Collections.Generic;

public static class KmpMatcher {
    public static int[] BuildLps(string pattern) {
        int[] lps = new int[pattern.Length];
        int len = 0; // Length of previous longest prefix suffix
        int i = 1;
        lps[0] = 0;

        while (i < pattern.Length) {
            if (pattern[i] == pattern[len]) {
                len++;
                lps[i] = len;
                i++;
            } else {
                if (len > 0) {
                    len = lps[len - 1]; // Step fallback back-link
                } else {
                    lps[i] = 0;
                    i++;
                }
            }
        }
        return lps;
    }

    public static List<int> Search(string text, string pattern) {
        var matchIndices = new List<int>();
        if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(pattern)) return matchIndices;

        int[] lps = BuildLps(pattern);
        int t = 0; // Text pointer
        int p = 0; // Pattern prefix pointer

        while (t < text.Length) {
            if (text[t] == pattern[p]) {
                t++;
                p++;
                if (p == pattern.Length) {
                    matchIndices.Add(t - p); // Complete match found!
                    p = lps[p - 1]; // Reset pattern pointer to precalculated fallback index
                }
            } else {
                if (p > 0) {
                    p = lps[p - 1]; // Step back along fallback path
                } else {
                    t++;
                }
            }
        }
        return matchIndices;
    }
}
```

---

### 2. Prefix Trie & TrieNode Representations (Python)
```python
class TrieNode:
    def __init__(self):
        self.children: dict[str, TrieNode] = {}
        self.is_end_of_word: bool = False


class Trie:
    def __init__(self):
        self.root = TrieNode()

    def insert(self, word: str) -> None:
        """Inserts string path into the Trie.
        
        Time Complexity: O(L)
        """
        if not word:
            return
        current = self.root
        for ch in word:
            if ch not in current.children:
                current.children[ch] = TrieNode()
            current = current.children[ch]
        current.is_end_of_word = True

    def search(self, word: str) -> bool:
        """Searches for exact match string in the prefix tree.
        
        Time Complexity: O(L)
        """
        if not word:
            return False
        node = self._get_node(word)
        return node is not None and node.is_end_of_word

    def starts_with(self, prefix: str) -> bool:
        """Verifies if a prefix exists in the Trie.
        
        Time Complexity: O(L)
        """
        if not prefix:
            return False
        return self._get_node(prefix) is not None

    def _get_node(self, target: str) -> TrieNode | None:
        current = self.root
        for ch in target:
            if ch not in current.children:
                return None
            current = current.children[ch]
        return current
```

---

### 3. Suffix Arrays & LCP Precomputation

A **Suffix Array** stores the starting indices of all sorted suffixes of a string alphabetically.
For example, for the string `"banana$"`:
*   All suffixes: `"banana"`, `"anana"`, `"nana"`, `"ana"`, `"na"`, `"a"`, `"$"`
*   Sorted suffixes: `""` (index 6), `"a"` (index 5), `"ana"` (index 3), `"anana"` (index 1), `"banana"` (index 0), `"na"` (index 4), `"nana$"` (index 2)
*   **Suffix Array (SA)**: `[6, 5, 3, 1, 0, 4, 2]`

The **Longest Common Prefix (LCP) Array** stores the lengths of the longest common prefixes between consecutive suffixes in the sorted Suffix Array. Suffix arrays and LCP arrays are used together to perform advanced string analysis, such as locating the longest repeated substring in a document.

We use **Kasai's Algorithm** to compute the LCP array in linear O(N) time, utilizing the matching overlaps of adjacent suffixes.

```csharp
// Suffix Array & LCP Array (Kasai's Algorithm) Implementation (C#)
public static class SuffixArrayEngine {
    public static int[] ConstructSuffixArray(string s) {
        int n = s.Length;
        var suffixes = new List<(int index, string value)>();
        for (int i = 0; i < n; i++) {
            suffixes.Add((i, s.Substring(i)));
        }
        
        // Sort suffixes alphabetically
        suffixes.Sort((a, b) => string.Compare(a.value, b.value, StringComparison.Ordinal));
        
        int[] sa = new int[n];
        for (int i = 0; i < n; i++) {
            sa[i] = suffixes[i].index;
        }
        return sa;
    }

    public static int[] BuildLcpArray(string s, int[] sa) {
        int n = s.Length;
        int[] lcp = new int[n];
        int[] rank = new int[n];

        // Rank stores the position of suffix s[i...n-1] in the Suffix Array
        for (int i = 0; i < n; i++) {
            rank[sa[i]] = i;
        }

        int h = 0; // Current LCP overlap helper length
        for (int i = 0; i < n; i++) {
            if (rank[i] > 0) {
                int j = sa[rank[i] - 1]; // Suffix immediately preceding in Suffix Array
                while (i + h < n && j + h < n && s[i + h] == s[j + h]) {
                    h++;
                }
                lcp[rank[i]] = h;
                if (h > 0) h--; // Decrease overlay length by 1 for next step
            }
        }
        return lcp;
    }
}
```

---

### 4. Non-Comparison Radix Sorting

*   **LSD (Least Significant Digit) Radix Sort**: Sorts input keys column by column from the rightmost digit (index L-1) moving left, using a stable sorting algorithm (Counting Sort) for each pass. This is highly efficient for sorting fixed-length arrays or strings.
*   **MSD (Most Significant Digit) Radix Sort**: Sorts input keys column by column from the leftmost digit (index 0) moving right. It recursively groups elements into buckets matching their current character codes, making it perfect for sorting variable-length strings alphabetically.
*   **3-way String Quicksort**: Combines MSD Radix Sort with quicksort partitioning. It partitions the array into three segments: elements with characters smaller than the pivot, equal to the pivot, and larger than the pivot. This is highly efficient and uses less memory.

```csharp
// Radix Sorting & 3-way String Quicksort Implementation (C#)
public static class RadixSorter {
    // LSD Radix Sort for fixed-length strings
    public static void LsdRadixSort(string[] arr, int stringLength) {
        int n = arr.Length;
        int R = 256; // Standard Extended-ASCII Radix alphabet capacity
        string[] aux = new string[n];

        // Iterate character positions from right (stringLength-1) to left (0)
        for (int d = stringLength - 1; d >= 0; d--) {
            int[] count = new int[R + 1];

            // 1. Calculate frequency counters
            for (int i = 0; i < n; i++) {
                count[arr[i][d] + 1]++;
            }

            // 2. Compute prefix cumulative offsets
            for (int r = 0; r < R; r++) {
                count[r + 1] += count[r];
            }

            // 3. Populate stable sorted output to aux array
            for (int i = 0; i < n; i++) {
                aux[count[arr[i][d]]++] = arr[i];
            }

            // 4. Copied back to original array
            Array.Copy(aux, arr, n);
        }
    }

    // 3-way String Quicksort
    public static void Sort3Way(string[] arr) {
        Sort3Way(arr, 0, arr.Length - 1, 0);
    }

    private static void Sort3Way(string[] arr, int lo, int hi, int d) {
        if (hi <= lo) return;

        int lt = lo;
        int gt = hi;
        int v = GetCharAt(arr[lo], d); // Pivot character
        int i = lo + 1;

        while (i <= gt) {
            int t = GetCharAt(arr[i], d);
            if (t < v) {
                Swap(arr, lt++, i++);
            } else if (t > v) {
                Swap(arr, i, gt--);
            } else {
                i++;
            }
        }

        // Recursively sort subdivided segments
        Sort3Way(arr, lo, lt - 1, d);
        if (v >= 0) Sort3Way(arr, lt, gt, d + 1); // Sort next character column
        Sort3Way(arr, gt + 1, hi, d);
    }

    private static int GetCharAt(string s, int d) {
        return d < s.Length ? s[d] : -1;
    }

    private static void Swap(string[] arr, int i, int j) {
        string temp = arr[i];
        arr[i] = arr[j];
        arr[j] = temp;
    }
}
```

---

### 5. Aho-Corasick Multi-Pattern Matching Engine

The Aho-Corasick algorithm builds a state machine by adding failure links to a standard prefix search Trie. If a character mismatch occurs while scanning a node, the failure link redirects the pointer of the state machine to the longest valid suffix of the pattern traversed so far. This lets you search for a set of dictionary patterns simultaneously in O({text length}) time.

```python
# Aho-Corasick Multi-Pattern Matching Engine (Python)
from collections import deque

class AhoCorasickNode:
    def __init__(self):
        self.children: dict[str, AhoCorasickNode] = {}
        self.fail: AhoCorasickNode | None = None
        self.output: list[str] = [] # Stores patterns that end at this state


class AhoCorasick:
    def __init__(self):
        self.root = AhoCorasickNode()

    def insert(self, pattern: str) -> None:
        current = self.root
        for ch in pattern:
            if ch not in current.children:
                current.children[ch] = AhoCorasickNode()
            current = current.children[ch]
        current.output.append(pattern)

    def build_automaton(self) -> None:
        """Constructs failure links using Breadth-First Search (BFS) queue."""
        queue = deque()
        
        # Level 1 Node failure paths fallback directly to root
        for child in self.root.children.values():
            child.fail = self.root
            queue.append(child)

        while queue:
            current = queue.popleft()

            for ch, child_node in current.children.items():
                fail_state = current.fail
                
                # Sift fallback path until matching transition is located
                while fail_state is not None and ch not in fail_state.children:
                    fail_state = fail_state.fail

                child_node.fail = fail_state.children[ch] if fail_state else self.root
                
                # Append fallback out targets to active output matches
                child_node.output.extend(child_node.fail.output)
                queue.append(child_node)

    def search_text(self, text: str) -> dict[str, list[int]]:
        """Scans raw streaming text sequentially returning list of match indices."""
        results = {}
        current = self.root

        for idx, ch in enumerate(text):
            while current is not None and ch not in current.children:
                current = current.fail

            current = current.children[ch] if current else self.root

            # Check matches ending at this state
            for pattern in current.output:
                start_index = idx - len(pattern) + 1
                if pattern not in results:
                    results[pattern] = []
                results[pattern].append(start_index)

        return results
```

---

## 📘 CHAPTER 4: PERFORMANCE & SYSTEMS

### 1. In-Depth Complexity Comparison

| Algorithm / Structure | Preprocessing Cost | Match Time Complexity | Space Overhead | Key Production Trade-off |
| :--- | :--- | :--- | :--- | :--- |
| **KMP** | O(M) | O(N) | O(M) | Minimal auxiliary index overhead; locks searching to a singular pattern |
| **Trie** | O(sum L_i) insert | O(L) lookups | O(sum L_i * \Sigma) | Fast prefix searches; high memory footprint because of child pointer array sizes |
| **Aho-Corasick**| O(sum(M_i)) states | O(N + Z) | O(sum(M_i)) | Matches multiple pattern candidates simultaneously in a single stream pass; relatively high compilation cost |
| **Suffix Array** | O(N log N) sorting | O(M log N) | O(N) indices | Extremely compact; fits entire document strings into flat index spaces |

### 2. Physical Memory Footprint Optimization (Double-Array Tries)
Standard pointer-based Tries have a high memory overhead because each node stores pointers to its child nodes (e.g. 26 pointers for lowercase English, or a dynamic hash map). Each pointer takes 8 bytes on 64-bit systems, and node metadata adds significant overhead, resulting in poor cache performance.
To minimize this memory footprint, high-performance systems use:
1.  **Lexicographical Radix Tries (Double-Array Tries)**: Pack child node transitions into two parallel arrays (`base` and `check`) of size A. This represents the entire Trie as a flat memory space, replacing pointer tree walks with fast index lookups:
    {pos\_child} = {base}[{pos\_parent}] + {code\_character}
    {check}[{pos\_child}] == {pos\_parent}
2.  **Compressed Paths (Radix Trees)**: Merges adjacent nodes with single children into a single string path, reducing the total node count and memory footprint. This is the primary routing structure used in core internet router switches.

---

## 📘 CHAPTER 5: INTEGRATION & MASTERY

### 1. Multi-Pattern Matching Selection Rules
*   *Use KMP when*: You need to find occurrences of a single string pattern P inside a streaming text.
*   *Use Aho-Corasick when*: You have a static dictionary of words and need to scan input documents for any occurrences of those words simultaneously.
*   *Use Suffix Arrays & LCP Arrays when*: You need to analyze a large static document (like a chromosome string, or an old book index) to answer multiple custom substring queries efficiently.

### 🧗 Progressive Practice Ladder
1.  **Implement Trie (Prefix Tree)** ([LeetCode 208](https://leetcode.com/problems/implement-trie-prefix-tree/)): Implement a prefix tree containing insertion, search, and prefix matching.
2.  **Find the Index of the First Occurrence in a String** ([LeetCode 28](https://leetcode.com/problems/find-the-index-of-the-first-occurrence-in-a-string/)): Build KMP precalc tables.
3.  **Shortest Palindrome** ([LeetCode 214](https://leetcode.com/problems/shortest-palindrome/)): Find the shortest palindrome by adding characters to the front of a string (using KMP's LPS table).
4.  **Aho-Corasick Engine**: Build failure links using BFS to match multiple patterns simultaneously in a single text pass.
5.  **Longest Repeated Substring**: Find the longest repeated substring in a document using Suffix Arrays and LCP arrays.

---

## 🛠️ Supplementary Material

### Practice Problems
1.  **3-way String Quicksort**: Implement the MSD 3-way string quicksort.
2.  **Design Add and Search Words Data Structure** ([LeetCode 211](https://leetcode.com/problems/design-add-and-search-words-data-structure/)): Implement prefix tries supporting wildcard wildcard characters.
3.  **LSD Integers Radix Sort**: Write an LSD counting sorting algorithm to sort variable integers.

### Misconceptions and Corrections
*   *Incorrect Idea*: Assuming that Tries are always faster than Hash Tables for word searches.
    *   *Correction*: While Tries are faster for prefix matching, they use significantly more memory because they store pointers for each character transition. In some cases, Hash Tables search routines are faster because of better cache locality.
