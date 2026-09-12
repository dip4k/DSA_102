# Phase 18: Trie / String Search

> **Focus:** Prefix Trees, $O(L)$ Word Insertion & Lookup, Wildcard Dot Searching via Branching DFS, and Trie-Guided Grid Backtracking with Dynamic Leaf Pruning.  
> **Source Curriculum:** [`Senior_dsa_question_list.md`](../Senior_dsa_question_list.md) — Phase 18 (Problems #99–#101)

---
## 99. Implement Trie (Prefix Tree) (LeetCode #208)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#trie` `#prefix-tree` `#system-design` `#tree-traversal` |
| **LeetCode Link** | [Implement Trie](https://leetcode.com/problems/implement-trie-prefix-tree/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Implement a `Trie` class with `Insert`, `Search`, and `StartsWith` methods:
  - `void Insert(string word)`: Inserts `word` into the trie.
  - `bool Search(string word)`: Returns `true` if `word` exists in the trie (exact full word match).
  - `bool StartsWith(string prefix)`: Returns `true` if any previously inserted word has the given prefix.
- **Key Constraints:**
  - $1 \le word.Length, prefix.Length \le 2000$.
  - All inputs consist of lowercase English letters `['a'..'z']`.
  - At most $3 \times 10^4$ total calls made to `Insert`, `Search`, and `StartsWith`.
- **Senior Edge Cases to Defend:**
  - Distinguishing between prefix existence vs full word termination via `IsEndOfWord`.
  - Inserting a word that is a strict prefix of an already existing word (e.g., `"app"` inserted after `"apple"`).
  - Inserting duplicate words (idempotency: `IsEndOfWord` remains `true`).

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** 26-Way Tree Pointer Traversal: Nodes store an array of 26 child references (`TrieNode[26]`) and an `IsEndOfWord` boolean flag. Character $c$ determines child index via direct pointer arithmetic: $c - \text{'a'}$.
- **Sample 1:**
  - `Trie trie = new Trie();`
  - `trie.Insert("apple");`
  - `trie.Search("apple");   // returns true`
  - `trie.Search("app");     // returns false (prefix exists, but not a full word)`
  - `trie.StartsWith("app"); // returns true`
  - `trie.Insert("app");`
  - `trie.Search("app");     // returns true`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine a massive library organized by an interconnected tree of hallways. At the central rotunda (`_root`), there are 26 doorways labeled `'a'` through `'z'`.
- Walking through doorway `'a'` leads to a room containing 26 more doorways for the second letter.
- Traveling down hallway `'a' \to 'p' \to 'p' \to 'l' \to 'e'` traces the exact spelling of `"apple"`.
- At the room corresponding to the final letter of a word, a plaque on the wall is turned to **`IsEndOfWord = true`**.
- Notice the profound spatial synergy: `"app"`, `"apple"`, and `"application"` share the exact same initial 3 chambers! Unlike a Hash Set where strings are stored redundantly, a Prefix Tree collapses common prefixes into a single shared path, enabling prefix queries in $O(L)$ time independent of dictionary size.

#### 3.2 The Naive Bottleneck & Redundant Computation
Comparing strings in a flat list takes $O(N \times L)$ time for $N$ dictionary words.
While a `HashSet<string>` allows $O(L)$ exact word searches, it completely fails on prefix queries (`StartsWith("app")`). To support prefix lookups in a hash set, you would need to insert all $L$ prefixes of every word, requiring $O(N \times L^2)$ auxiliary storage and lacking alphabetical traversal or wildcard matching. A Trie provides both exact search and prefix search in strict $O(L)$ time.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**Direct Index Pointer Traversal:**
Because the alphabet is strictly lowercase English letters ($|\Sigma| = 26$), each node allocates an array of 26 pointers:
$$idx = c - \text{'a'} \in [0 \dots 25]$$
- **Insertion Invariant:** Starting from `curr = _root`, for each character $c$: if `curr.Children[idx] == null`, allocate a new `TrieNode`. Advance `curr = curr.Children[idx]`. Set `curr.IsEndOfWord = true` at the final node.
- **Search Invariant:** Follow existing child pointers. If at any character `curr.Children[idx] == null`, the string does not exist $\implies$ return `false`. If the full string is traversed, return `curr.IsEndOfWord`.
- **StartsWith Invariant:** Follow child pointers. If all prefix characters exist, return `true` regardless of `curr.IsEndOfWord`.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
TRIE PREFIX TREE ARCHITECTURE:
_root
 ├── ['a' - 'a' = 0] ──> Node('a')
                          └── ['p' - 'a' = 15] ──> Node('p')
                                                    └── ['p' - 'a' = 15] ──> Node('p') [IsEndOfWord: true] ("app")
                                                                              └── ['l' - 'a' = 11] ──> Node('l')
                                                                                                        └── ['e' - 'a' = 4] ──> Node('e') [IsEndOfWord: true] ("apple")
```

- `_root`: Dummy sentinel head representing the empty prefix `""`.
- `curr`: Pointer cursor advancing from parent to child: `curr = curr.Children[c - 'a']`.
- `Children[26]`: Direct pointer table providing $O(1)$ child transition.
- `IsEndOfWord`: State flag distinguishing between complete dictionary entries and incidental prefix nodes.

#### 3.5 State Transition Triggers & Decision Gates
For an input string of length $L$:
1. **Navigation Gate:** For step $k = 0 \dots L - 1$, compute $idx = word[k] - 'a'$.
   - In `Insert`: If `curr.Children[idx] == null`, allocate `new TrieNode()`.
   - In `Search`/`StartsWith`: If `curr.Children[idx] == null`, abort and return `false`.
2. **Step Advance:** `curr = curr.Children[idx]`.
3. **Terminal Gate:**
   - In `Insert`: `curr.IsEndOfWord = true`.
   - In `Search`: Return `curr.IsEndOfWord`.
   - In `StartsWith`: Return `true` (valid prefix confirmed).

#### 3.6 Concrete Step-by-Step State Trace
Trace operations: `Insert("app")`, `Insert("apple")`, `Search("app")`, `Search("appl")`, `StartsWith("appl")`.

| Operation | Input | Traversal Path | Terminal Node Status | Return Value |
| :--- | :--- | :--- | :--- | :---: |
| `Insert` | `"app"` | Root $\to$ 'a' $\to$ 'p' $\to$ 'p' | Mark `IsEndOfWord = true` | `void` |
| `Insert` | `"apple"` | Reuses 'a'-'p'-'p' $\to$ 'l' $\to$ 'e' | Mark `IsEndOfWord = true` | `void` |
| `Search` | `"app"` | Root $\to$ 'a' $\to$ 'p' $\to$ 'p' | Reached node with `IsEndOfWord == true` | **True** |
| `Search` | `"appl"` | Root $\to$ 'a' $\to$ 'p' $\to$ 'p' $\to$ 'l' | Node exists, but `IsEndOfWord == false` | **False** |
| `StartsWith`| `"appl"` | Root $\to$ 'a' $\to$ 'p' $\to$ 'p' $\to$ 'l' | Node exists! Prefix valid | **True** |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Fixed 26-Array TrieNode):** Industry standard for fixed English alphabets. Provides maximum CPU instruction pipelining, contiguous memory strides, and zero hash-collision overhead.
- **Approach 2 (Hash Map Child Pointers):** Choose when the character set is arbitrary (Unicode, UTF-8, full ASCII) or when memory constraints are tight on sparse alphabets where most of the 26 pointers would be null.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Node Definition:** Define `TrieNode` containing `TrieNode[] Children = new TrieNode[26]` and `bool IsEndOfWord`.
- **Step 2: Helper Resolution:** Implement `FindNode(string str)` to share traversal logic between `Search` and `StartsWith`.
- **Step 3: Concrete Implementations:** `Search` tests `node != null && node.IsEndOfWord`; `StartsWith` tests `node != null`.

#### 4.3 Alternative Approaches Analysis
- **Approach 2: Dictionary Child Pointers:**
  - Replace `TrieNode[26]` with `Dictionary<char, TrieNode>`.
  - Saves memory on sparse trees, but introduces dictionary hashing overhead, GC references, and pointer indirection on every character transition.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: Fixed 26-Array TrieNode | Approach 2: Dictionary Child Pointers |
| :--- | :--- | :--- |
| **Time Complexity (Insert / Search / Prefix)** | $O(L)$ / $O(L)$ / $O(L)$ | $O(L)$ amortized / $O(L)$ / $O(L)$ |
| **Auxiliary Space** | $O(26 \times \sum L)$ pointers | $O(\sum L)$ dynamic dictionary nodes |
| **CPU Cache Locality** | High (fixed offset indexing) | Moderate (hash table bucket chasing) |
| **Alphabet Adaptability** | Low (fixed to 26 lowercase English) | High (supports all Unicode characters) |
| **In-Place Mutability** | Non-destructive structural tree | Non-destructive structural tree |
| **Streaming Suitability** | High (processes stream character by character) | High |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Problem #99 - Implement Trie (Prefix Tree)
// Core Pattern: Multi-Way Prefix Tree / Direct Array Indexing
// Primary Invariant: Node at depth k represents unique prefix s[0 .. k - 1].
// Cache Defense: Fixed 26-element array provides O(1) child lookups via CPU pointer arithmetic.
// Prefix Defense: IsEndOfWord cleanly separates prefix substrings from terminal words.
// ============================================================================
```

#### Implementation 1: Fixed 26-Array TrieNode (Production Standard)
```csharp
public class Trie
{
    private class TrieNode
    {
        // 26 child pointers for lowercase English letters 'a' through 'z'
        public readonly TrieNode[] Children = new TrieNode[26];

        // Indicates whether a complete word terminates at this specific node
        public bool IsEndOfWord { get; set; }
    }

    private readonly TrieNode _root;

    public Trie()
    {
        _root = new TrieNode();
    }

    public void Insert(string word)
    {
        if (string.IsNullOrEmpty(word)) return;

        TrieNode curr = _root;

        for (int i = 0; i < word.Length; i++)
        {
            // Direct index calculation: 'a' -> 0, 'b' -> 1, ..., 'z' -> 25
            int idx = word[i] - 'a';

            // Allocate child node only on first traversal through this character branch
            if (curr.Children[idx] == null)
            {
                curr.Children[idx] = new TrieNode();
            }

            curr = curr.Children[idx];
        }

        // Seal the final node as a valid complete word
        curr.IsEndOfWord = true;
    }

    public bool Search(string word)
    {
        if (string.IsNullOrEmpty(word)) return false;

        TrieNode node = FindNode(word);

        // Word exists iff all prefix nodes exist AND the terminal node is flagged as end of word
        return node != null && node.IsEndOfWord;
    }

    public bool StartsWith(string prefix)
    {
        if (string.IsNullOrEmpty(prefix)) return false;

        // Prefix exists iff the sequence of characters successfully traces a valid path
        return FindNode(prefix) != null;
    }

    private TrieNode FindNode(string str)
    {
        TrieNode curr = _root;

        for (int i = 0; i < str.Length; i++)
        {
            int idx = str[i] - 'a';

            // If path terminates prematurely, string is not present
            if (curr.Children[idx] == null)
            {
                return null;
            }

            curr = curr.Children[idx];
        }

        return curr;
    }
}
```

#### Implementation 2: Hash Map Child Pointers (Unicode / Dynamic Alphabet)
```csharp
public class TrieHashMap
{
    private class TrieNode
    {
        public readonly Dictionary<char, TrieNode> Children = new();
        public bool IsEndOfWord { get; set; }
    }

    private readonly TrieNode _root = new();

    public void Insert(string word)
    {
        if (string.IsNullOrEmpty(word)) return;

        TrieNode curr = _root;
        foreach (char c in word)
        {
            if (!curr.Children.TryGetValue(c, out var next))
            {
                next = new TrieNode();
                curr.Children[c] = next;
            }
            curr = next;
        }

        curr.IsEndOfWord = true;
    }

    public bool Search(string word)
    {
        TrieNode node = FindNode(word);
        return node != null && node.IsEndOfWord;
    }

    public bool StartsWith(string prefix)
    {
        return FindNode(prefix) != null;
    }

    private TrieNode FindNode(string str)
    {
        TrieNode curr = _root;
        foreach (char c in str)
        {
            if (!curr.Children.TryGetValue(c, out var next))
            {
                return null;
            }
            curr = next;
        }
        return curr;
    }
}
```

---

## 100. Design Add and Search Words Data Structure (LeetCode #211)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | 🔥 High |
| **Pattern Tags** | `#trie` `#wildcard-search` `#dfs-backtracking` |
| **LeetCode Link** | [Design Add and Search Words](https://leetcode.com/problems/design-add-and-search-words-data-structure/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Design a data structure that supports adding new words and finding if a string matches any previously added string, where `word` may contain dots `'.'` that match any character.
- **Key Constraints:**
  - $1 \le word.Length \le 25$.
  - At most 2 dots in search queries.
  - At most $10^4$ total calls made to `AddWord` and `Search`.
  - All words consist of lowercase English letters and `'.'`.
- **Senior Edge Cases to Defend:**
  - Queries consisting solely of dots (e.g. `Search("...")` matches any 3-letter word).
  - Dot matching at the very end of a word: Must verify `IsEndOfWord` on the matched child.
  - Multiple dots in succession (e.g. `".."`).

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Trie with Wildcard Branching DFS: Standard character transitions follow direct deterministic child pointers in $O(1)$; wildcard `'.'` triggers non-deterministic branching across all 26 child references.
- **Sample 1:**
  - `WordDictionary dict = new WordDictionary();`
  - `dict.AddWord("bad"); dict.AddWord("dad"); dict.AddWord("mad");`
  - `dict.Search("pad"); // returns false`
  - `dict.Search("bad"); // returns true`
  - `dict.Search(".ad"); // returns true (matches 'bad', 'dad', 'mad')`
  - `dict.Search("b.."); // returns true (matches 'bad')`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine an automated directory assistant at an enterprise phone switchboard.
- When an exact name like `"alice"` is spelled out, the switchboard follows a single direct wire at each letter: `'a' \to 'l' \to 'i' \to 'c' \to 'e'`.
- But when a wildcard symbol `'.'` is encountered (like saying *"any letter here"*), the switchboard splits the inquiry signal into 26 parallel lines, testing every non-empty corridor simultaneously.
- If any one of those 26 exploratory signals reaches a verified employee extension (`IsEndOfWord == true`) at the end of the query, the call connects successfully (`return true`).
This is an **NFA (Non-Deterministic Finite Automaton)** executed atop a Prefix Tree.

#### 3.2 The Naive Bottleneck & Redundant Computation
Searching a flat array of words using Regular Expressions evaluates every stored word ($O(N \times L)$).
Generating all combinations of words with dots pre-computed into a Hash Set explodes combinatorially: a 25-letter word has $2^{25} \approx 3.35 \times 10^7$ wildcard configurations.
A Trie with branching DFS executes exact searches in strict $O(L)$ time, and restricts wildcard branching strictly to active branches that actually exist in the dictionary.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**Deterministic vs Non-Deterministic Branching Invariant:**
At character index $k$ of `word`:
1. **Deterministic Branch ($word[k] \in ['a'..'z']$):**
   - Lookup $idx = word[k] - 'a'$.
   - If `node.Children[idx] == null`, return `false`.
   - Step forward: `DfsSearch(word, k + 1, node.Children[idx])`.
2. **Non-Deterministic Branch ($word[k] == '.'$):**
   - Iterate all 26 possible children $i \in [0 \dots 25]$.
   - For every non-null child `node.Children[i]`:
     $$\text{if } DfsSearch(word, k + 1, node.Children[i]) == \text{true} \implies \text{return true immediately!}$$
   - Short-circuit evaluation ensures remaining branches are skipped once a single matching path is discovered.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
WILDCARD BRANCHING SEARCH ARCHITECTURE:
Target: ".ad"
Root
 ├── Node('b') ──> Node('a') ──> Node('d') [IsEnd: true]
 ├── Node('d') ──> Node('a') ──> Node('d') [IsEnd: true]
 └── Node('m') ──> Node('a') ──> Node('d') [IsEnd: true]

At index 0 ('.'):
Branch DFS simultaneously into 'b', 'd', 'm'!
First child 'b' succeeds: matches 'a' -> 'd' (IsEnd=true)
Short-circuit return: true!
```

- `index`: Active character position in the query string `word` ($0 \le index \le word.Length$).
- `node`: Active TrieNode in the tree hierarchy.
- **Base Case Invariant:** When `index == word.Length`, query string has been fully consumed. Return `node.IsEndOfWord`.

#### 3.5 State Transition Triggers & Decision Gates
At each call `DfsSearch(word, index, node)`:
1. **Null Gate:** If `node == null`, return `false`.
2. **Terminal Gate:** If `index == word.Length`, return `node.IsEndOfWord`.
3. **Character Gate:**
   - If $c \neq '.'$: Recurse `DfsSearch(word, index + 1, node.Children[c - 'a'])`.
   - If $c == '.'$: Loop $i = 0 \dots 25$. For each `node.Children[i] != null`, if recursive call returns `true`, return `true`.
4. **Failure Gate:** Return `false` if no child path succeeded.

#### 3.6 Concrete Step-by-Step State Trace
Trace words added: `"bad"`, `"dad"`. Query: `Search(".ad")`.

| Call Frame | `index` | Query Char | Candidate Node | Action Taken | Result |
| :---: | :---: | :---: | :---: | :--- | :---: |
| **1** | 0 | `'.'` | `_root` | Branch into non-null children: 'b', 'd' | — |
| **2** | 1 | `'a'` | Child `'b'` | Direct step to child `'a'` | — |
| **3** | 2 | `'d'` | Child `'a'` | Direct step to child `'d'` | — |
| **4** | 3 | Base | Child `'d'` | `index == 3`, `IsEndOfWord == true` | **True** |
| **1** | — | — | — | Short-circuit! Returns `true` immediately | **True** |

Path for `'d'` is never even explored due to early exit!

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Trie with Branching DFS):** The definitive standard. $O(L)$ for `AddWord`. For `Search`, executes in $O(L)$ for exact queries, and $O(26^D \cdot L)$ where $D$ is the number of dots. With $D \le 2$, $26^2 = 676$, running in under a millisecond.
- **Approach 2 (Length-Grouped Word Lists):** Group words by length in a `Dictionary<int, List<string>>`. On search, scan all words of matching length and compare character by character. Degrades to $O(N \cdot L)$ when dictionary contains thousands of words of the same length.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Data Structure:** Standard `TrieNode` with 26 child pointers and `IsEndOfWord`.
- **Step 2: AddWord:** Standard iterative pointer insertion.
- **Step 3: Search Dispatch:** Delegate search to recursive `DfsSearch(word, 0, _root)`.
- **Step 4: Branching Logic:** Check for dot vs letter, short-circuit on match.

#### 4.3 Alternative Approaches Analysis
- **Approach 2: Length-Bucket String Scanning:**
  - Group words into `Dictionary<int, List<string>>`.
  - To search, iterate through all words in `buckets[word.Length]`.
  - For each word, check if every non-dot character matches.
  - While simple, it scales poorly when $N = 10^5$.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: Trie + Branching DFS (Optimal) | Approach 2: Length-Bucket Linear Scan |
| :--- | :--- | :--- |
| **Time Complexity (AddWord)** | $O(L)$ | $O(1)$ |
| **Time Complexity (Search)** | $O(L)$ exact / $O(26^D \cdot L)$ with dots | $O(K \cdot L)$ where $K$ = words of length $L$ |
| **Auxiliary Space** | $O(26 \times \sum L)$ | $O(\sum L)$ list storage |
| **Output Space** | $O(1)$ boolean | $O(1)$ boolean |
| **Cache Locality** | High | High |
| **In-Place Mutability** | Non-destructive | Non-destructive |
| **Streaming Suitability** | High | Low |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Problem #100 - Design Add and Search Words Data Structure
// Core Pattern: Trie with Non-Deterministic Branching DFS
// Primary Invariant: Exact characters follow O(1) child edges; '.' branches across all 26 children.
// Short-Circuit Defense: First true child return immediately halts further DFS branches.
// Space Defense: Fixed 26-array nodes eliminate dictionary rehashing overhead.
// ============================================================================
```

#### Implementation 1: Trie with Branching DFS (Production Standard)
```csharp
public class WordDictionary
{
    private class TrieNode
    {
        public readonly TrieNode[] Children = new TrieNode[26];
        public bool IsEndOfWord { get; set; }
    }

    private readonly TrieNode _root;

    public WordDictionary()
    {
        _root = new TrieNode();
    }

    public void AddWord(string word)
    {
        if (string.IsNullOrEmpty(word)) return;

        TrieNode curr = _root;
        for (int i = 0; i < word.Length; i++)
        {
            int idx = word[i] - 'a';
            if (curr.Children[idx] == null)
            {
                curr.Children[idx] = new TrieNode();
            }
            curr = curr.Children[idx];
        }

        curr.IsEndOfWord = true;
    }

    public bool Search(string word)
    {
        if (string.IsNullOrEmpty(word)) return false;
        return DfsSearch(word, 0, _root);
    }

    private static bool DfsSearch(string word, int index, TrieNode node)
    {
        // Guard: Reached dead end
        if (node == null)
        {
            return false;
        }

        // Base Case: Entire query string consumed; verify word termination flag
        if (index == word.Length)
        {
            return node.IsEndOfWord;
        }

        char c = word[index];

        // CASE 1: Deterministic single character transition
        if (c != '.')
        {
            int idx = c - 'a';
            return DfsSearch(word, index + 1, node.Children[idx]);
        }

        // CASE 2: Wildcard '.' non-deterministic branch across all 26 children
        for (int i = 0; i < 26; i++)
        {
            TrieNode child = node.Children[i];

            // Invariant Gate: Only explore paths that actually exist in the Trie
            if (child != null && DfsSearch(word, index + 1, child))
            {
                // Short-Circuit Optimization: Return immediately on first valid match
                return true;
            }
        }

        // None of the 26 branches yielded a valid terminal match
        return false;
    }
}
```

---

## 101. Word Search II (LeetCode #212)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🔴 Hard |
| **Priority** | 💡 Advanced |
| **Pattern Tags** | `#trie` `#grid-backtracking` `#prefix-pruning` `#leaf-pruning` |
| **LeetCode Link** | [Word Search II](https://leetcode.com/problems/word-search-ii/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given an $m \times n$ `board` of characters and a list of strings `words`, return all words on the board. Each word must be constructed from letters of sequentially adjacent cells. No cell may be reused within the same word.
- **Key Constraints:**
  - $m == board.Length, n == board[i].Length \in [1, 12]$.
  - $1 \le words.Length \le 3 \times 10^4$.
  - $1 \le words[i].Length \le 10$.
  - `board` and `words[i]` consist of lowercase English letters.
  - All strings in `words` are unique.
- **Senior Edge Cases to Defend:**
  - Duplicate words on board: The same word may be formed along multiple distinct grid paths. Must collect it exactly once.
  - Giant dictionary ($30,000$ words): Running Word Search I for each word times out severely ($30,000 \times 144 \times 4^{10}$).
  - Subtree exhaustion: Once all words descending from a Trie prefix have been found, that Trie branch must be pruned dynamically so future grid explorers do not waste time re-traversing it.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Trie-Guided Grid Backtracking with Dynamic Subtree Pruning: Insert all target words into a Prefix Tree. Traverse grid cells while stepping down the Trie. Prune grid exploration immediately when a prefix does not exist in the Trie.
- **Sample 1:**
  - **Input:** `board = [["o","a","a","n"],["e","t","a","e"],["i","h","k","r"],["i","f","l","v"]], words = ["oath","pea","eat","rain"]`
  - **Output:** `["eat","oath"]`
- **Sample 2:**
  - **Input:** `board = [["a","b"],["c","d"]], words = ["abcb"]`
  - **Output:** `[]`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine an expedition through an ancient cavern grid looking for $30,000$ known treasure names.
- Instead of carrying $30,000$ separate maps and walking the cavern $30,000$ times, you compile all treasure names into a single **Master Compass (The Trie)**.
- You step into room $(r, c)$ with letter $L$. You consult the Master Compass: Does the compass have an arrow for $L$?
  - **No arrow:** You immediately turn back! No treasure in the universe starts with this sequence of rooms.
  - **Arrow exists:** You follow the arrow, advance your compass needle to the child node, mask the room floor with breadcrumbs (`board[r][c] = '#'`), and explore adjacent rooms.
- If your compass needle lands on a room containing a treasure name, you collect the treasure, **erase it from your compass** (deduplication), and if the compass branch now has no remaining treasures, you **snip off the branch** (dynamic leaf pruning) so future cavern explorations never wander down this path again!

#### 3.2 The Naive Bottleneck & Redundant Computation
Running individual Word Search I queries evaluates:
$$T = O(W \times M \times N \times 4^L) = 30,000 \times 144 \times 4^{10} \approx 4.53 \times 10^{12} \text{ operations}$$
By reversing the perspective—searching the board once guided by the Trie—every step on the grid simultaneously filters all $30,000$ words in parallel. The search complexity collapses to $O(M \times N \times 4 \times 3^{L-1})$ bounded by the maximum word length $L \le 10$.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**Synchronous Dual-Graph Traversal Invariants:**
1. **Direct String Embedding:** Store the full string reference `TrieNode.Word` at terminal nodes. When a match is found, add `currNode.Word` directly to `result`, eliminating all dynamic `StringBuilder` allocation churn!
2. **Deduplication via In-Place Nullification:** After collecting a word, set `currNode.Word = null`. If another path reaches this same node later, it sees `Word == null` and avoids adding duplicates.
3. **Dynamic Leaf Pruning (Subtree Trimming):**
   Track `childCount` in each `TrieNode`. When a leaf node is matched or has all its children pruned:
   $$\text{parent.Children}[idx] = null$$
   Pruning empty subtrees permanently removes dead branches from the Trie, drastically accelerating subsequent grid scans!

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
SYNCHRONOUS GRID-TRIE BACKTRACKING:
Grid Cell (r, c) ['o']  <====== Matches ======>  Trie Node root.Children['o' - 'a']
       │                                                      │
       ▼                                                      ▼
Mask board[r][c] = '#'                                Advance Trie cursor to child
Explore 4 neighbors (r±1, c±2)                        Verify parent.Children[neighbor] exists
Restore board[r][c] = 'o'                             Prune child if child becomes empty leaf
```

- `board[r][c]`: Active grid room being explored.
- `currNode`: Active node in the Prefix Tree corresponding to prefix spelled so far.
- `result`: Aggregate collection of unique words discovered.

#### 3.5 State Transition Triggers & Decision Gates
At cell $(r, c)$ with Trie node `parent`:
1. **Existence Gate:** Let $idx = board[r][c] - 'a'$. Check `currNode = parent.Children[idx]`. If `currNode == null`, prune immediately!
2. **Match Gate:** If `currNode.Word != null`, add `currNode.Word` to `result`, then set `currNode.Word = null` (deduplicate).
3. **Choose Gate:** Mask `board[r][c] = '#'`.
4. **Explore Gate:** Recurse across 4 adjacent orthogonal cells.
5. **Undo Gate:** Restore `board[r][c] = letter`.
6. **Prune Gate:** If `currNode` has 0 children remaining, set `parent.Children[idx] = null`.

#### 3.6 Concrete Step-by-Step State Trace
Trace: `board = [["o","a"],["e","t"]]`, `words = ["oa", "oat"]`.
Trie built: Root $\to$ 'o' $\to$ 'a' (Word="oa") $\to$ 't' (Word="oat").

| Step | Grid Cell $(r, c)$ | Letter | Trie Node Reached | Action Taken | Words Found |
| :---: | :---: | :---: | :---: | :--- | :--- |
| **1** | $(0, 0)$ | `'o'` | Node('o') | Matches! Mask with `'#'` | — |
| **2** | $(0, 1)$ | `'a'` | Node('a') | **Match! `Word == "oa"`** $\implies$ Add `"oa"`, set `Word = null` | `["oa"]` |
| **3** | $(1, 1)$ | `'t'` | Node('t') | **Match! `Word == "oat"`** $\implies$ Add `"oat"`, set `Word = null` | `["oa", "oat"]` |
| **4** | Backtrack | — | Node('t') | Leaf has 0 children; prune Node('t') from Node('a') | — |
| **5** | Backtrack | — | Node('a') | Leaf now has 0 children; prune Node('a') from Node('o') | — |

Future scans from other cells instantly see `root.Children['o']` pruned!

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Trie with Dynamic Leaf Pruning):** Elite senior implementation. $10\times$ faster than standard Trie backtracking on large benchmarks. Eliminates redundant work as words are discovered.
- **Approach 2 (Standard Trie without Pruning):** Simpler to write, but wastes time re-traversing subtrees for words that have already been discovered.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Trie Construction:** Insert all words from dictionary into Trie. Store full word string at terminal nodes.
- **Step 2: Grid Initiation:** Iterate through all $(r, c)$ cells. If `root.Children[board[r][c] - 'a'] != null`, launch DFS.
- **Step 3: Synchronous Traversal & Masking:** Mask cell, explore 4 neighbors, unmask.
- **Step 4: Pruning & Return:** Prune dead leaf nodes on backtrack unwind. Return `result`.

#### 4.3 Alternative Approaches Analysis
- **Approach 2: Standard Trie Traversal:**
  - Omits leaf pruning. Uses `currNode.Word = null` for deduplication, but leaves empty nodes in the Trie, causing future grid searches to traverse dead ends repeatedly.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: Trie with Leaf Pruning (Optimal) | Approach 2: Standard Trie Traversal |
| :--- | :--- | :--- |
| **Time Complexity (Best / Avg / Worst)** | $O(\sum L + M \cdot N \cdot 4 \cdot 3^{L-1})$ | $O(\sum L + M \cdot N \cdot 4 \cdot 3^{L-1})$ (higher constant factor) |
| **Auxiliary Space** | $O(\sum L)$ Trie + $O(L)$ stack depth | $O(\sum L)$ Trie + $O(L)$ stack depth |
| **Output Space** | $O(W)$ matched words | $O(W)$ matched words |
| **Cache Locality** | High | High |
| **In-Place Mutability** | Temporary board masking (restored) | Temporary board masking (restored) |
| **Streaming Suitability** | High | High |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Problem #101 - Word Search II
// Core Pattern: Trie-Guided Grid Backtracking / Dynamic Subtree Pruning
// Primary Invariant: board DFS and Trie node navigation advance synchronously; prune on null child.
// Deduplication Defense: Nullifying currNode.Word upon match eliminates duplicate collections.
// Pruning Defense: Pruning empty leaf nodes on backtrack unwind accelerates subsequent grid scans.
// ============================================================================
```

#### Implementation 1: Trie with Dynamic Leaf Pruning (Production Optimal)
```csharp
public class Solution
{
    private class TrieNode
    {
        public readonly TrieNode[] Children = new TrieNode[26];
        public string Word { get; set; } // Stores complete word string at terminal node
        public int ChildCount { get; set; } // Tracks active child branches for dynamic pruning
    }

    public IList<string> FindWords(char[][] board, string[] words)
    {
        var result = new List<string>();

        // Guard Clauses
        if (board == null || board.Length == 0 || board[0].Length == 0 || words == null || words.Length == 0)
        {
            return result;
        }

        // STEP 1: Construct Prefix Trie from target word list
        var root = new TrieNode();
        foreach (string w in words)
        {
            TrieNode curr = root;
            foreach (char c in w)
            {
                int idx = c - 'a';
                if (curr.Children[idx] == null)
                {
                    curr.Children[idx] = new TrieNode();
                    curr.ChildCount++;
                }
                curr = curr.Children[idx];
            }
            curr.Word = w;
        }

        int rows = board.Length;
        int cols = board[0].Length;

        // STEP 2: Launch Trie-guided DFS from each grid cell
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                int idx = board[r][c] - 'a';
                if (root.Children[idx] != null)
                {
                    Dfs(board, r, c, root, result);
                }
            }
        }

        return result;
    }

    private static void Dfs(char[][] board, int r, int c, TrieNode parent, List<string> result)
    {
        char letter = board[r][c];
        int idx = letter - 'a';
        TrieNode currNode = parent.Children[idx];

        // INVARIANT PRUNING GATE:
        // If current character does not exist as a child in Trie, abort immediately!
        if (currNode == null)
        {
            return;
        }

        // MATCH GATE:
        // Full word found! Add to result and nullify to prevent duplicate collection
        if (currNode.Word != null)
        {
            result.Add(currNode.Word);
            currNode.Word = null; // Deduplicate: word collected, seal terminal flag
        }

        // 1. Choose: Mask grid cell in-place with breadcrumb
        board[r][c] = '#';

        // 2. Explore: 4-directional orthogonal traversal
        int[] dr = { 0, 1, 0, -1 };
        int[] dc = { 1, 0, -1, 0 };

        for (int d = 0; d < 4; d++)
        {
            int nr = r + dr[d];
            int nc = c + dc[d];

            // Verify boundaries and unvisited state
            if (nr >= 0 && nr < board.Length && nc >= 0 && nc < board[0].Length && board[nr][nc] != '#')
            {
                Dfs(board, nr, nc, currNode, result);
            }
        }

        // 3. Undo: Restore cell character
        board[r][c] = letter;

        // DYNAMIC LEAF PRUNING:
        // If currNode has no active children and does not terminate any remaining word,
        // prune it from parent to permanently stop future explorers from entering this dead branch
        if (currNode.ChildCount == 0 && currNode.Word == null)
        {
            parent.Children[idx] = null;
            parent.ChildCount--;
        }
    }
}
```
