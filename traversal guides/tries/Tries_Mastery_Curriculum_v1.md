# Traversal Mastery: Tries (Prefix Trees)

## 0. Summary Table

| Level | Mental Model | Pointer/State | Drill Problems |
| :--- | :--- | :--- | :--- |
| **1. Trie Node Definition** | A tree where edges represent characters. Nodes represent boundaries between characters. | `Node`: Represents a prefix path. Contains children map and `IsEndOfWord` boolean. | None (Foundational) |
| **2. Core Operations (Insert/Search)** | Walk down the tree matching characters. Create missing nodes on insert. Check `IsEndOfWord` for exact matches. | `CurrentNode`: The trie node corresponding to the prefix seen so far. | [LeetCode 208: Implement Trie (Prefix Tree)] (Medium) |
| **3. Prefix Matching & Autocomplete** | Traverse to the end of the prefix. From there, perform DFS/BFS to collect all valid words. | `CurrentNode`: The node marking the end of the search prefix. | [LeetCode 642: Design Search Autocomplete System] (Hard) |
| **4. Trie + Backtracking (DFS)** | Prune search space on a 2D grid by walking a Trie simultaneously with grid DFS. | `(r, c, TrieNode)`: Current grid cell and corresponding Trie node. | [LeetCode 212: Word Search II] (Hard) |

---

## 1. Trie Node Implementation

**What does the state/index mean?**
*   Each node represents the state of having matched a specific prefix.
*   The edges (children) represent the next possible character in the sequence.

**What region is processed?**
*   A path from root to any node represents a string prefix.
*   A path from root to a node with `is_end_of_word = True` represents a complete word in the dictionary.

**What is the invariant/recurrence relation?**
*   `Node(Prefix + char) = Node(Prefix).Children[char]`

### Visual State Transitions

| Step | Choice/Action | State | Invariant |
| :--- | :--- | :--- | :--- |
| Init | Create empty root | `Root` node, empty children | Path from root to root is `""` |
| Add | Insert 'a' to root | `Root.Children['a'] = new Node()` | Child 'a' represents prefix `"a"` |
| Mark | Word ends at 'a' | `Node('a').is_end_of_word = True` | `"a"` is a valid word |

### Code Snippets

Problem: Define the fundamental building block of a Trie where each node represents a character boundary and stores links to subsequent characters along with a flag indicating the end of a valid word.

```python
class TrieNode:
    def __init__(self):
        # 1. Initialize a hash map to hold character edges pointing to child TrieNodes
        self.children = {}  # char -> TrieNode
        # 2. Flag to determine if the path from root to this node forms a complete word
        self.is_end_of_word = False
```

```csharp
public class TrieNode {
    // 1. Initialize a dictionary to map outgoing characters to child nodes
    public Dictionary<char, TrieNode> Children = new();
    // 2. State flag indicating if a valid dictionary word ends at this node
    public bool IsEndOfWord = false;
}
```

### ⚠️ Gotchas & Pitfalls
*   **Storing characters in nodes:** Nodes don't strictly need to store their own character. The edge (the key in the `children` map) implies the character. Storing it redundantly can lead to confusion.
*   **Memory overhead:** Using a hash map for children is space-efficient for sparse nodes, but an array of size 26 (for lowercase English) is faster and often preferred if space allows.

### Drill Problems
*   *Foundational concept, usually practiced as part of Level 2.*

---

## 2. Insert, Search, and StartsWith

**What does the state/index mean?**
*   `current_node`: The node representing the string matched up to character `i`.
*   `i`: The index of the character currently being processed in the input string.

**What region is processed?**
*   Input string `word[0...i]`.

**What is the invariant/recurrence relation?**
*   During `insert(word)`: If `word[i]` is not in `current_node.children`, create it. Move `current_node = current_node.children[word[i]]`. At the end, set `is_end_of_word = True`.
*   During `search(word)`: If `word[i]` not in `current_node.children`, return `False`. At the end, return `current_node.is_end_of_word`.
*   During `startsWith(prefix)`: Same as search, but return `True` immediately upon exhausting the prefix.

### Visual State Transitions
*Inserting "cat", then "car"*

| Step | Action | State (Current Node) | Invariant |
| :--- | :--- | :--- | :--- |
| 1 | Insert 'c' | Root -> `[c]` | Root has child 'c' |
| 2 | Insert 'a' | Node('c') -> `[a]` | Path is "ca" |
| 3 | Insert 't' | Node('a') -> `[t]` | Path is "cat" |
| 4 | End "cat" | Node('t') | `Node('t').is_end_of_word = True` |
| 5 | Insert 'c' for "car" | Node('c') | Reuses existing 'c' child |
| 6 | Insert 'a' for "car" | Node('a') | Reuses existing 'a' child |
| 7 | Insert 'r' | Node('a') -> `[r]` | Path is "car". `Node('r').is_end_of_word = True` |

### Code Snippets

Problem: Implement a Trie data structure supporting word insertion, exact word search, and prefix matching.

```python
class Trie:
    def __init__(self):
        # 1. Initialize the Trie with an empty root node
        self.root = TrieNode()

    def insert(self, word: str) -> None:
        # 1. Start from the root node
        curr = self.root
        # 2. Iterate through each character in the input word
        for char in word:
            # 3. If the path for the character doesn't exist, create a new node
            if char not in curr.children:
                curr.children[char] = TrieNode()
            # 4. Traverse down to the child node
            curr = curr.children[char]
        # 5. Mark the final node as the end of a valid dictionary word
        curr.is_end_of_word = True

    def search(self, word: str) -> bool:
        # 1. Start traversal from the root node
        curr = self.root
        # 2. Walk down the tree matching each character
        for char in word:
            # 3. If a character path is missing, the word does not exist
            if char not in curr.children:
                return False
            # 4. Move to the next node in the path
            curr = curr.children[char]
        # 5. Check if the final node represents a complete word (not just a prefix)
        return curr.is_end_of_word

    def startsWith(self, prefix: str) -> bool:
        # 1. Start traversal from the root node
        curr = self.root
        # 2. Follow the character path for the prefix
        for char in prefix:
            # 3. If the path breaks, the prefix does not exist
            if char not in curr.children:
                return False
            # 4. Advance the pointer to the next matching node
            curr = curr.children[char]
        # 5. The entire prefix matched successfully, so return true
        return True
```

```csharp
public class Trie {
    private TrieNode root;

    public Trie() {
        // 1. Initialize the root of the Trie
        root = new TrieNode();
    }
    
    public void Insert(string word) {
        // 1. Begin at the root node
        TrieNode curr = root;
        // 2. Process each character of the word
        foreach (char c in word) {
            // 3. Create a missing child node for the character if necessary
            if (!curr.Children.ContainsKey(c)) {
                curr.Children[c] = new TrieNode();
            }
            // 4. Move pointer to the child node
            curr = curr.Children[c];
        }
        // 5. Mark the terminal node as forming a complete word
        curr.IsEndOfWord = true;
    }
    
    public bool Search(string word) {
        // 1. Begin at the root node
        TrieNode curr = root;
        // 2. Traverse the Trie character by character
        foreach (char c in word) {
            // 3. Fail early if the path is broken
            if (!curr.Children.ContainsKey(c)) return false;
            // 4. Traverse to the next character node
            curr = curr.Children[c];
        }
        // 5. Return true only if this path completes a valid inserted word
        return curr.IsEndOfWord;
    }
    
    public bool StartsWith(string prefix) {
        // 1. Begin at the root node
        TrieNode curr = root;
        // 2. Attempt to trace the prefix through the Trie
        foreach (char c in prefix) {
            // 3. Return false if the prefix trace fails
            if (!curr.Children.ContainsKey(c)) return false;
            // 4. Continue tracing the prefix
            curr = curr.Children[c];
        }
        // 5. We reached the end of the prefix path successfully
        return true;
    }
}
```

### ⚠️ Gotchas & Pitfalls
*   **Search vs. StartsWith confusion:** `search` must verify `is_end_of_word == True` at the final node. `startsWith` only requires reaching the end of the prefix string without failing a child lookup.
*   **Empty string insertion:** Depending on constraints, inserting `""` means marking the root as `is_end_of_word = True`. Ensure your loop handles empty inputs correctly.

### Drill Problems
*   [LeetCode 208: Implement Trie (Prefix Tree)] (Medium)
*   [LeetCode 1268: Search Suggestions System] (Medium) - *Prefix matching core logic.*

---

## 3. Autocomplete (DFS from Prefix)

**What does the state/index mean?**
*   `prefix_node`: The node representing the end of the user's typed prefix.
*   `path`: The accumulated string during DFS from `prefix_node` to find completions.

**What region is processed?**
*   The subtree rooted at `prefix_node`.

**What is the invariant/recurrence relation?**
*   Every node visited in the DFS under `prefix_node` represents a valid extension of the original prefix.
*   If `curr_node.is_end_of_word` is true, `OriginalPrefix + path` is a valid autocomplete suggestion.

### Visual State Transitions
*Prefix = "ca", Trie has "cat", "car", "cart"*

| Step | Action | DFS State (Node, Current String) | Invariant |
| :--- | :--- | :--- | :--- |
| 1 | Navigate to Prefix | `Node('a')`, `"ca"` | `Node('a')` is root for DFS |
| 2 | DFS branch 1 | `Node('t')`, `"cat"` | `is_end_of_word` -> Yield "cat" |
| 3 | DFS branch 2 | `Node('r')`, `"car"` | `is_end_of_word` -> Yield "car" |
| 4 | DFS child of 'r' | `Node('t')`, `"cart"` | `is_end_of_word` -> Yield "cart" |

### Code Snippets

Problem: Given a prefix, navigate the Trie to the end of the prefix and perform a Depth-First Search (DFS) to find and collect all valid words that start with that prefix.

```python
class AutocompleteSystem:
    # Assuming standard Trie structure exists
    def __init__(self):
        # 1. Initialize the root for the Trie
        self.root = TrieNode()
        
    def get_completions(self, prefix: str) -> list[str]:
        # 1. Start from the root to locate the end of the typed prefix
        curr = self.root
        # 2. Traverse the Trie following the prefix characters
        for char in prefix:
            # 3. If the prefix does not exist, return an empty list (no completions)
            if char not in curr.children:
                return []
            curr = curr.children[char]
            
        results = []
        # 4. Initiate DFS from the node at the end of the prefix
        self._dfs(curr, prefix, results)
        return results
        
    def _dfs(self, node: TrieNode, current_word: str, results: list[str]):
        # 1. Base check: if current path is a valid word, add to completions
        if node.is_end_of_word:
            results.append(current_word)
            
        # 2. Recursively explore all possible next characters (edges)
        for char, child_node in node.children.items():
            # 3. Append the character to the current word and continue DFS
            self._dfs(child_node, current_word + char, results)
```

```csharp
public class AutocompleteSystem {
    // 1. Initialize the standard Trie root
    private TrieNode root = new TrieNode();
    
    public List<string> GetCompletions(string prefix) {
        // 1. Point to the root to begin matching the prefix
        TrieNode curr = root;
        // 2. Walk down the tree to exhaust the prefix string
        foreach (char c in prefix) {
            // 3. If prefix breaks, there are no autocompletions available
            if (!curr.Children.ContainsKey(c)) return new List<string>();
            curr = curr.Children[c];
        }
        
        List<string> results = new List<string>();
        // 4. Explore all branches from the end of the prefix using DFS
        Dfs(curr, prefix, results);
        return results;
    }
    
    private void Dfs(TrieNode node, string currentWord, List<string> results) {
        // 1. If we encounter a node marked as a completed word, record it
        if (node.IsEndOfWord) {
            results.Add(currentWord);
        }
        
        // 2. Iterate through all subsequent character branches
        foreach (var kvp in node.Children) {
            // 3. Build up the word state and recursively search deeper
            Dfs(kvp.Value, currentWord + kvp.Key, results);
        }
    }
}
```

### ⚠️ Gotchas & Pitfalls
*   **Sorting/Ranking requirements:** Often autocomplete requires returning top $K$ results sorted by frequency or lexicographical order. Simple DFS returns them in hash map iteration order (unordered). You may need to store pre-sorted lists or max-heaps at each node to optimize retrieval.
*   **String concatenation in DFS:** Creating new strings at every DFS step (`currentWord + char`) generates many objects. Using a mutable character array/list and joining at the end (`backtracking` style) is more memory efficient.

### Drill Problems
*   [LeetCode 642: Design Search Autocomplete System] (Hard)

---

## 4. Word Search II (DFS + Trie)

**What does the state/index mean?**
*   `r, c`: Current coordinates on the grid.
*   `trie_node`: The Trie node corresponding to the string formed by the path taken on the grid so far.

**What region is processed?**
*   Simultaneous traversal of the 2D grid and the Trie structure.

**What is the invariant/recurrence relation?**
*   `DFS(r, c, node)` only proceeds to a neighbor `(nr, nc)` if `grid[nr][nc]` exists in `node.children`.
*   This prunes the search space: we never explore grid paths that do not form prefixes of target words.

### Visual State Transitions

| Step | Action | State (r, c, Node) | Invariant |
| :--- | :--- | :--- | :--- |
| 1 | Start at cell 'o' | `(0, 0, Root)` | Match 'o' in `Root.children` |
| 2 | Move to node 'o' | `(0, 0, Node('o'))` | Valid prefix "o" |
| 3 | DFS to cell 'a' | `(0, 1, Node('o'))` | Match 'a' in `Node('o').children` |
| 4 | Move to node 'a' | `(0, 1, Node('a'))` | Valid prefix "oa". If end, add to result. |

### Code Snippets

Problem: Find all valid dictionary words in a 2D grid of characters by simultaneously running a DFS on the grid and walking a Trie to prune invalid search paths.

```python
class Solution:
    def findWords(self, board: list[list[str]], words: list[str]) -> list[str]:
        # 1. Build the Trie from the given list of target words
        root = TrieNode()
        for word in words:
            curr = root
            for char in word:
                if char not in curr.children:
                    curr.children[char] = TrieNode()
                curr = curr.children[char]
            curr.is_end_of_word = True
            # Store the full word at the terminal node for O(1) retrieval during DFS
            curr.word = word 
            
        ROWS, COLS = len(board), len(board[0])
        res, visit = set(), set()
        
        # 2. Define the DFS function to explore the grid and Trie simultaneously
        def dfs(r, c, node):
            # 3. Base cases: out of bounds, already visited, or char not in current Trie node
            if (r < 0 or c < 0 or r == ROWS or c == COLS or 
                (r, c) in visit or board[r][c] not in node.children):
                return
            
            # 4. Mark current grid cell as visited
            visit.add((r, c))
            # 5. Move the Trie pointer to the matched child node
            node = node.children[board[r][c]]
            
            # 6. If we found a completed word, add it to the results set
            if node.is_end_of_word:
                res.add(node.word)
                
            # 7. Explore all 4 adjacent grid directions with the updated Trie node
            dfs(r + 1, c, node)
            dfs(r - 1, c, node)
            dfs(r, c + 1, node)
            dfs(r, c - 1, node)
            
            # 8. Backtrack: unmark the cell to allow other paths to use it
            visit.remove((r, c))
            
        # 9. Initiate DFS from every possible starting cell on the board
        for r in range(ROWS):
            for c in range(COLS):
                dfs(r, c, root)
                
        return list(res)
```

```csharp
public class Solution {
    // Requires modified TrieNode that stores the full string at the terminal node
    public class TrieNode {
        public Dictionary<char, TrieNode> Children = new();
        public string Word = null;
    }

    public IList<string> FindWords(char[][] board, string[] words) {
        // 1. Construct the Trie from the dictionary of valid words
        TrieNode root = new TrieNode();
        foreach (string w in words) {
            TrieNode curr = root;
            foreach (char c in w) {
                if (!curr.Children.ContainsKey(c)) curr.Children[c] = new TrieNode();
                curr = curr.Children[c];
            }
            // Store the full word at the end node instead of a boolean flag
            curr.Word = w;
        }

        HashSet<string> res = new HashSet<string>();
        int rows = board.Length, cols = board[0].Length;

        // 2. Start a simultaneous grid-DFS and Trie traversal from every cell
        for (int r = 0; r < rows; r++) {
            for (int c = 0; c < cols; c++) {
                Dfs(board, r, c, root, res);
            }
        }
        return res.ToList();
    }

    private void Dfs(char[][] board, int r, int c, TrieNode node, HashSet<string> res) {
        // 3. Check boundaries of the grid
        if (r < 0 || c < 0 || r >= board.Length || c >= board[0].Length) return;
        
        char letter = board[r][c];
        // 4. Prune path: stop if cell is visited ('#') or char isn't in current Trie node
        if (letter == '#' || !node.Children.ContainsKey(letter)) return;

        // 5. Advance the Trie node pointer
        TrieNode nextNode = node.Children[letter];
        // 6. Check if the new Trie node marks the end of a valid word
        if (nextNode.Word != null) {
            res.Add(nextNode.Word);
            // Don't return here! The current word might be a prefix for a longer word
        }

        // 7. Mark the current grid cell as visited for the current DFS path
        board[r][c] = '#';

        // 8. DFS recursively into neighbors, passing the updated Trie node
        Dfs(board, r + 1, c, nextNode, res);
        Dfs(board, r - 1, c, nextNode, res);
        Dfs(board, r, c + 1, nextNode, res);
        Dfs(board, r, c - 1, nextNode, res);

        // 9. Backtrack: restore the cell's original character
        board[r][c] = letter; 
    }
}
```

### ⚠️ Gotchas & Pitfalls
*   **Duplicate words in result:** A word might be found multiple times starting from different cells. Use a `Set` to store results, or dynamically remove the word from the Trie (`node.word = null`) once found to avoid duplicates and speed up future searches.
*   **Trie Pruning optimization:** For extremely tight time limits, dynamically remove leaf nodes from the Trie once a word is found and the node has no other children. This prevents re-traversing exhausted branches.
*   **Returning early on match:** Do NOT return immediately after finding a word (`if node.is_end_of_word`). The current word might be a prefix for a longer valid word (e.g., finding "app" but "apple" is also in the Trie and grid).

### Drill Problems
*   [LeetCode 212: Word Search II] (Hard)
