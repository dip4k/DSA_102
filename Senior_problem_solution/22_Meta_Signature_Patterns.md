# Phase 22: Meta Signature Patterns

> **Focus:** Column-Indexed BFS Tree Sweeps, Parent-Pointer LCA Cycles, Reverse 3-Pointer Array Mutation, CDF Binary Search Sampling, Sparse Vector Dot Products, Two-Pass Parenthesis Balancing, and Depth-Weighted Recursive Evaluation.  
> **Source Curriculum:** [`Senior_dsa_question_list.md`](../Senior_dsa_question_list.md) — Phase 22 (Problems #123–#132)

---

## 123. Binary Tree Vertical Order Traversal (LeetCode #314)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#bfs` `#binary-tree` `#hash-table` `#column-indexing` `#level-order` |
| **LeetCode Link** | [Binary Tree Vertical Order Traversal](https://leetcode.com/problems/binary-tree-vertical-order-traversal/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given the `root` of a binary tree, return the vertical order traversal of its nodes' values (from top to bottom, column by column). If two nodes are in the same row and column, the order should be from left to right.
- **Assumptions & Contracts:**
  - The root node is assigned coordinates `(row = 0, col = 0)`.
  - For any node at coordinate `(row, col)`:
    - Its left child is positioned at `(row + 1, col - 1)`.
    - Its right child is positioned at `(row + 1, col + 1)`.
  - Columns must be returned strictly ordered from leftmost (`minCol`) to rightmost (`maxCol`).
  - Within each column, nodes must be ordered strictly from top (`row = 0`) to bottom (`row = H`).
  - When two nodes share both the identical `row` and `col`, tie-breaking is strictly **left-to-right insertion order** (the node encountered earlier in horizontal level scan takes precedence).
  - An empty tree (`root == null`) returns an empty list `[]`.
- **Key Constraints:**
  - The number of nodes in the tree is in the range $[0, 100]$ (LeetCode standard), scaling to $N \le 10^5$ in production systems.
  - $-100 \le Node.val \le 100$.
- **Senior Edge Cases to Defend:**
  - `root == null`: Guard clause must return an empty list immediately without instantiating queue buffers.
  - Single node tree: Returns a single list containing only `[root.val]`.
  - Strictly left-skewed tree: Every node decrements column index; $col \in [-(N - 1), 0]$.
  - Strictly right-skewed tree: Every node increments column index; $col \in [0, N - 1]$.
  - Complete coordinate collisions: Multiple nodes converging at identical `(row, col)` coordinates (e.g., node A's right child and node B's left child). Left-to-right parent visitation must guarantee left node appears first.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Assign a virtual integer Cartesian grid to the tree. Breadth-First Search (BFS) naturally traverses nodes row-by-row top-to-bottom and left-to-right within each row. By pairing each node with its column index in the BFS queue and collecting node values into column buckets, the BFS traversal order automatically satisfies both ordering invariants (top-to-bottom and left-to-right tie breaking) without requiring post-traversal multi-key sorting.
- **Sample 1:**
  - **Input:** `root = [3, 9, 20, null, null, 15, 7]`
  - **Output:** `[[9], [3, 15], [20], [7]]`
  - **Explanation:**
    - Node 9: `col = -1` $\implies$ `[9]`
    - Node 3: `col = 0`, Node 15: `col = 0` $\implies$ `[3, 15]`
    - Node 20: `col = 1` $\implies$ `[20]`
    - Node 7: `col = 2` $\implies$ `[7]`
- **Sample 2:**
  - **Input:** `root = [3, 9, 8, 4, 0, 1, 7]`
  - **Output:** `[[4], [9], [3, 0, 1], [8], [7]]`
  - **Explanation:**
    - `col = -2`: `[4]`
    - `col = -1`: `[9]`
    - `col = 0`: Node 3 (`row 0`), Node 0 (`row 2`, left of 8), Node 1 (`row 2`, right of 9) $\implies$ `[3, 0, 1]`
    - `col = 1`: `[8]`
    - `col = 2`: `[7]`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine shining a directional spotlight from directly above a physical mobile sculpture hanging from the ceiling. Every sphere in the mobile casts a shadow onto a single measuring tape stretched across the floor. As you lower an elevator floor-by-floor (breadth-first level sweep), every time the elevator passes a sphere, that sphere drops straight down into its corresponding centimeter slot on the floor tape. Because the elevator descends level-by-level, spheres higher up hit the floor first. If two spheres are at the exact same elevator height and cast shadows onto the exact same centimeter mark, the one on the left was seen first by the elevator operator and drops first.

#### 3.2 The Naive Bottleneck & Redundant Computation
A naive attempt employs Depth-First Search (DFS):
1. Traverse recursively, collecting a tuple `(col, row, val)` for every node.
2. Sort all $N$ tuples using a composite comparator: primary key `col` ascending, secondary key `row` ascending.
- **Computational Bottleneck:** Sorting $N$ elements incurs $O(N \log N)$ time complexity.
- **Subtle Bug in DFS:** In DFS, a left branch explores deep rows before a shallower right branch. If node $X$ is at `(row 2, col 0)` in the left subtree and node $Y$ is at `(row 2, col 0)` in the right subtree, DFS might visit them out of horizontal order unless an additional global timestamp or traversal sequence index is tracked.
- **BFS Elimination:** Breadth-First Search eliminates sorting entirely. Because BFS processes nodes strictly in increasing `row` order, and within each row strictly from left to right, inserting into column buckets during BFS guarantees top-to-bottom and left-to-right order intrinsically in $O(N)$ linear time!

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Dual Invariant of Level-Order Queue:**
  1. *Row Monotonicity:* If node $u$ is dequeued before node $v$, then $row(u) \le row(v)$.
  2. *Intra-Row Left-to-Right Preservation:* If $row(u) == row(v)$ and $u$ was dequeued before $v$, then $u$ was positioned to the left of $v$.
- **Min/Max Column Bounding Invariant:**
  Instead of using a `SortedDictionary<int, List<int>>` which incurs $O(\log K)$ overhead per insertion (where $K$ is the number of distinct columns), we track two scalar integer cursors: `minCol` and `maxCol`.
  At tree completion, the range of columns is the contiguous closed interval $[\text{minCol}, \text{maxCol}]$. We iterate $c$ from $\text{minCol}$ to $\text{maxCol}$ in $O(K) \le O(N)$ time, pulling buckets directly from a standard $O(1)$ `Dictionary<int, List<int>>`.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
CARTESIAN COLUMN PROJECTION VIA BFS:

        Coordinates: (row, col)
                 (0, 0)
                  [3]
                /     \\
       (-1, 1)        (1, 1)
         [9]           [20]
                     /      \\
                  (0, 2)    (2, 2)
                   [15]       [7]

Column Buckets Projection:
  Col -1          Col 0           Col 1          Col 2
+---------+    +---------+    +---------+    +---------+
|   [9]   |    |   [3]   |    |  [20]   |    |   [7]   |
+---------+    |  [15]   |    +---------+    +---------+
               +---------+
```

BFS Queue State Invariant:
- `queue`: Holds elements as value tuples `(TreeNode Node, int Col)`.
- At any point during BFS, the queue contains nodes from at most two consecutive rows: $R$ and $R + 1$.
- `columnMap`: Maps `col -> List<int>`. Appending to `columnMap[col]` preserves insertion chronology.

#### 3.5 State Transition Triggers & Decision Gates
1. **Dequeue Gate:** Extract `(currNode, col)` from queue head.
2. **Column Registration Gate:**
   - If `!columnMap.ContainsKey(col)`, initialize `columnMap[col] = new List<int>()`.
   - Append `currNode.val` to `columnMap[col]`.
   - Update `minCol = Math.Min(minCol, col)`, `maxCol = Math.Max(maxCol, col)`.
3. **Child Expansion Gates:**
   - If `currNode.left != null`: Enqueue `(currNode.left, col - 1)`.
   - If `currNode.right != null`: Enqueue `(currNode.right, col + 1)`.
4. **Assembly Gate:** Iterate $c \in [\text{minCol}, \text{maxCol}]$, append `columnMap[c]` to output list.

#### 3.6 Concrete Step-by-Step State Trace
Tree: `root = [3, 9, 20, null, null, 15, 7]`

| Step | Dequeued `(Node, Col)` | `minCol` | `maxCol` | `columnMap` State After Dequeue | Enqueued Children |
| :---: | :---: | :---: | :---: | :--- | :--- |
| **Init** | — | `0` | `0` | `{}` | `(3, 0)` |
| **1** | `(3, 0)` | `0` | `0` | `{ 0: [3] }` | `(9, -1)`, `(20, 1)` |
| **2** | `(9, -1)` | `-1` | `0` | `{ 0: [3], -1: [9] }` | None |
| **3** | `(20, 1)` | `-1` | `1` | `{ 0: [3], -1: [9], 1: [20] }` | `(15, 0)`, `(7, 2)` |
| **4** | `(15, 0)` | `-1` | `1` | `{ 0: [3, 15], -1: [9], 1: [20] }` | None |
| **5** | `(7, 2)` | `-1` | `2` | `{ 0: [3, 15], -1: [9], 1: [20], 2: [7] }` | None |

Queue is empty. Iterate $c$ from $-1$ to $2$:
- $c = -1 \implies [9]$
- $c = 0 \implies [3, 15]$
- $c = 1 \implies [20]$
- $c = 2 \implies [7]$
Final Result: `[[9], [3, 15], [20], [7]]`.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (BFS with Hash Map + Min/Max Column Tracking):** Optimal production approach. Operates in $O(N)$ time with $O(N)$ space. Eliminates all sorting and handles arbitrary tree shapes.
- **Approach 2 (DFS with Coordinate Sorting):** Visits nodes via DFS, assigns `(col, row, val)`, and sorts. Requires $O(N \log N)$ time and requires tracking visitation index to resolve row/col collisions. Suboptimal for production.
- **Selection Rule:** Always select Approach 1. BFS naturally aligns with the problem's vertical and horizontal ordering invariants.

#### 4.2 Step-by-Step Natural Progression Flow
1. **Setup & Guard:** Validate `root != null`. Initialize queue with `(root, 0)`.
2. **State Tracking:** Maintain `Dictionary<int, List<int>>`, and tracking integers `minCol = 0, maxCol = 0`.
3. **Queue Processing:** Standard FIFO loop. Dequeue, record value, conditionally enqueue left child with `col - 1`, right child with `col + 1`.
4. **Result Materialization:** Loop from `minCol` to `maxCol`, packing buckets into `IList<IList<int>>`.

#### 4.3 Alternative Approaches Analysis
- *SortedDictionary / TreeMap:* Using `SortedDictionary<int, List<int>>` avoids manually tracking `minCol` and `maxCol`, but costs $O(N \log K)$ due to red-black tree rebalancing on each new column. Tracking min/max column bounds reduces this to $O(N)$ with zero log factors.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Approach | Time (Best/Avg/Worst) | Auxiliary Space | Output Space | Cache Locality | In-Place Mutability | Streaming Suitability |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **1. BFS + Min/Max Bounds** | $O(N) / O(N) / O(N)$ | $O(N)$ | $O(N)$ | High (contiguous lists) | Non-mutating | High (level-by-level) |
| **2. BFS + SortedDictionary** | $O(N \log K)$ | $O(N)$ | $O(N)$ | Moderate (tree nodes) | Non-mutating | High |
| **3. DFS + Multi-Key Sort** | $O(N \log N)$ | $O(N)$ | $O(N)$ | Low (sorting tuples) | Non-mutating | Poor |

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: Binary Tree Vertical Order Traversal (LeetCode #314)
 * ============================================================================
 * Core Pattern      : Breadth-First Search (BFS) with Column Indexing & Min/Max Tracking
 * Time Complexity   : O(N) single pass visiting every tree node exactly once
 * Space Complexity  : O(N) auxiliary space for BFS queue and column hash map
 * Selection Rule    : BFS guarantees top-to-bottom and left-to-right ordering naturally,
 *                     completely eliminating the O(N log N) sorting step required by DFS.
 * Defensive Traps   : Guard against root == null immediately.
 *                     Track minCol and maxCol to avoid O(K log K) SortedDictionary overhead.
 * ============================================================================
 */

using System;
using System.Collections.Generic;

public class TreeNode
{
    public int val;
    public TreeNode? left;
    public TreeNode? right;
    public TreeNode(int val = 0, TreeNode? left = null, TreeNode? right = null)
    {
        this.val = val;
        this.left = left;
        this.right = right;
    }
}

public class Solution
{
    /// <summary>
    /// Computes the vertical order traversal of a binary tree in optimal O(N) time.
    /// Employs level-order traversal (BFS) to maintain top-to-bottom and left-to-right order.
    /// </summary>
    /// <param name="root">The root of the binary tree.</param>
    /// <returns>A list of lists containing vertical column values from left to right.</returns>
    public IList<IList<int>> VerticalOrder(TreeNode? root)
    {
        // Guard Clause: Trivial empty tree contract
        if (root == null)
        {
            return Array.Empty<IList<int>>();
        }

        var columnMap = new Dictionary<int, List<int>>();
        // ValueTuple in Queue avoids heap allocation per node during BFS traversal
        var queue = new Queue<(TreeNode Node, int Col)>();

        int minCol = 0;
        int maxCol = 0;

        queue.Enqueue((root, 0));

        // Invariant: BFS processes nodes row by row from top to bottom,
        // and left to right within each row.
        while (queue.Count > 0)
        {
            var (currentNode, col) = queue.Dequeue();

            // Register node value in its respective vertical column bucket
            if (!columnMap.TryGetValue(col, out var bucket))
            {
                bucket = new List<int>();
                columnMap[col] = bucket;
            }
            bucket.Add(currentNode.val);

            // Dynamically maintain the horizontal column bounds
            if (col < minCol) minCol = col;
            if (col > maxCol) maxCol = col;

            // Enqueue left child with decrementing column coordinate
            if (currentNode.left != null)
            {
                queue.Enqueue((currentNode.left, col - 1));
            }

            // Enqueue right child with incrementing column coordinate
            if (currentNode.right != null)
            {
                queue.Enqueue((currentNode.right, col + 1));
            }
        }

        // Assemble result by reading contiguous column interval [minCol, maxCol]
        var result = new List<IList<int>>(capacity: maxCol - minCol + 1);
        for (int c = minCol; c <= maxCol; c++)
        {
            if (columnMap.TryGetValue(c, out var columnList))
            {
                result.Add(columnList);
            }
        }

        return result;
    }
}
```

---

### 6. Senior Pitfalls & Defensive Traps
- **Pitfall 1: Confusing LeetCode #314 with LeetCode #987:**
  - In LC #314, nodes in the same row and column MUST appear in **left-to-right insertion order**.
  - In LC #987 (Vertical Order Traversal of a Binary Tree), nodes in the same row and column MUST be sorted in **ascending numerical value order**. Applying LC #987's value sorting to LC #314 yields wrong answers on duplicate/unsorted values!
- **Pitfall 2: DFS Ordering Trap:**
  - Using DFS causes nodes in deeper left subtrees to be visited before shallower right subtrees that share the same column. Recovering the correct order requires tracking `(col, row, timestamp)` and sorting, degrading performance from $O(N)$ to $O(N \log N)$.
- **Pitfall 3: Using `SortedDictionary` unnecessarily:**
  - In .NET, `SortedDictionary<int, List<int>>` is backed by a red-black tree ($O(\log K)$ per operation). Tracking `minCol` and `maxCol` integers during standard `Dictionary` operations is $O(1)$ and provides direct contiguous range iteration.
- **Pitfall 4: Queue Boxing Overhead:**
  - Using class tuples `Tuple<TreeNode, int>` allocates a managed object on the heap for every single node in the tree. Using C# 7+ value tuples `(TreeNode Node, int Col)` allocates zero heap memory for queue elements.

---

## 124. Minimum Remove to Make Valid Parentheses (LeetCode #1249)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#string` `#stack` `#two-pass` `#greedy-balance` `#in-place` |
| **LeetCode Link** | [Minimum Remove to Make Valid Parentheses](https://leetcode.com/problems/minimum-remove-to-make-valid-parentheses/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given a string `s` of `'('`, `')'` and lowercase English characters. Your task is to remove the minimum number of parentheses (`'('` or `')'`), in any positions, so that the resulting parentheses string is valid and return any valid string.
- **Assumptions & Contracts:**
  - A parentheses string is valid if and only if:
    1. It is the empty string, or contains only lowercase characters, or
    2. It can be written as $AB$ (concatenation of $A$ and $B$), where $A$ and $B$ are valid strings, or
    3. It can be written as $(A)$, where $A$ is a valid string.
  - Return **any** valid string resulting from the minimum number of deletions.
- **Key Constraints:**
  - $1 \le s.Length \le 10^5$.
  - `s[i]` is either `'('`, `')'`, or lowercase English letters.
- **Senior Edge Cases to Defend:**
  - All closing parentheses: `s = ")))"` $\implies$ returns `""`.
  - All opening parentheses: `s = "((("` $\implies$ returns `""`.
  - Interleaved invalid parentheses: `s = "))(("` $\implies$ returns `""`.
  - Balanced interior with dangling boundaries: `s = "a)b(c)d"` $\implies$ returns `"ab(c)d"`.
  - String containing zero parentheses: `s = "code"` $\implies$ returns `"code"`.
  - Already valid parentheses with nested structures: `s = "(a(b)c)"` $\implies$ returns `"(a(b)c)"`.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Two-Phase Balance Sweep: A closing parenthesis `')'` is invalid if and only if there is no preceding unmatched opening parenthesis `'('`. An opening parenthesis `'('` is invalid if it remains unmatched after scanning the entire string. By storing indices of unmatched `'('` in a stack and marking illegal `')'` directly during a forward scan, we can eliminate both invalid sets in a single reconstruction pass.
- **Sample 1:**
  - **Input:** `s = "lee(t(c)o)de)"`
  - **Output:** `"lee(t(c)o)de"`
  - **Explanation:** The trailing `')'` at index 11 has no matching `'('` and is removed.
- **Sample 2:**
  - **Input:** `s = "a)b(c)d"`
  - **Output:** `"ab(c)d"`
  - **Explanation:** The `')'` at index 1 is invalid.
- **Sample 3:**
  - **Input:** `s = "))(("`
  - **Output:** `""`
  - **Explanation:** All four parentheses are invalid.

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Think of opening parentheses `'('` as issuing loan vouchers, and closing parentheses `')'` as loan redemptions.
- If a customer arrives with a redemption voucher `')'` when the bank has zero open loans (`balance == 0`), that redemption is fraudulent—it must be confiscated and discarded on the spot.
- When the business day closes, if the bank holds loan vouchers `'('` that were never redeemed, those specific loans are bad debt. Crucially, the loans issued *latest in the day* (at the highest string indices) are the ones that went unfulfilled. Discarding them restores absolute balance.

#### 3.2 The Naive Bottleneck & Redundant Computation
- A backtracking or recursion approach that branches on "keep or remove" explores $2^K$ combinations where $K$ is the count of parentheses. For $N = 10^5$, this results in an immediate exponential blowup.
- Repeatedly deleting characters from strings inside a loop causes $O(N^2)$ array reallocation and copying overhead in memory.
- The linear invariant states that invalidity is deterministic and local: invalid `')'` can be flagged immediately upon arrival, and invalid `'('` are simply whatever remains on the index stack at string termination.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Prefix Sum Invariant of Parentheses:**
  Let $P[i]$ be the prefix balance where `'(' = +1` and `')' = -1`. A string is valid if and only if:
  $$\forall i \in [0, N-1]: P[i] \ge 0 \quad \text{AND} \quad P[N-1] == 0$$
- **Immediate Rejection Criterion:**
  Whenever a `')'` is encountered at index $i$ while the active balance of `'('` is 0, keeping this `')'` would cause $P[i] = -1 < 0$. No downstream `'('` can ever repair a negative prefix in the past. Therefore, this `')'` **must** be removed.
- **Terminal Rejection Criterion:**
  If after the entire scan, $M$ unclosed `'('` remain, exactly $M$ opening parentheses must be removed. By removing the rightmost $M$ occurrences of `'('`, all preceding valid pairings remain unperturbed.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
MARK-AND-SWEEP STACK INVARIANT:

Input:   l  e  e  (  t  (  c  )  o  )  d  e  )
Index:   0  1  2  3  4  5  6  7  8  9 10 11 12

Pass 1 (Forward Scan):
i = 3:  '(' -> Push index 3 to stack. Stack: [3]
i = 5:  '(' -> Push index 5 to stack. Stack: [3, 5]
i = 7:  ')' -> Match found! Pop stack. Stack: [3]
i = 9:  ')' -> Match found! Pop stack. Stack: []
i = 12: ')' -> Stack empty! Illegal ')'. Mark index 12 as INVALID.

Pass 2 (Builder Sweep):
Collect all chars whose indices are NOT in (Stack + InvalidSet):
Indices to exclude: { 12 }
Output: "lee(t(c)o)de"
```

#### 3.5 State Transition Triggers & Decision Gates
1. **Character Discriminator Gate:**
   - Case `'('`: Push current index $i$ onto `openIndicesStack`.
   - Case `')'`:
     - If `openIndicesStack.Count > 0`: Pop top index (match finalized).
     - If `openIndicesStack.Count == 0`: Mark index $i$ as invalid (`invalidIndices[i] = true`).
   - Case letter `[a-z]`: No-op (always valid).
2. **End-of-String Flush Gate:**
   - While `openIndicesStack.Count > 0`: Pop index $idx$, mark `invalidIndices[idx] = true`.
3. **Reconstruction Gate:**
   - Allocate `StringBuilder` (or `char[]`). Append characters where `!invalidIndices[i]`.

#### 3.6 Concrete Step-by-Step State Trace
Input: `s = "a)b(c)d"`

| Index $i$ | `s[i]` | Action / Condition | `openIndicesStack` | `invalidIndices` Set |
| :---: | :---: | :---: | :---: | :--- |
| **0** | `'a'` | Letter $\implies$ keep | `[]` | `{}` |
| **1** | `')'` | Stack empty $\implies$ Invalid! | `[]` | `{ 1 }` |
| **2** | `'b'` | Letter $\implies$ keep | `[]` | `{ 1 }` |
| **3** | `'('` | Open bracket $\implies$ push | `[3]` | `{ 1 }` |
| **4** | `'c'` | Letter $\implies$ keep | `[3]` | `{ 1 }` |
| **5** | `')'` | Match $\implies$ pop 3 | `[]` | `{ 1 }` |
| **6** | `'d'` | Letter $\implies$ keep | `[]` | `{ 1 }` |
| **End** | — | Stack is empty | `[]` | `{ 1 }` |

Filter pass drops index 1: `'a'`, `'b'`, `'('`, `'c'`, `')'`, `'d'` $\implies$ `"ab(c)d"`.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Stack + Boolean Array / Sentinel):** Standard senior approach. Uses `char[]` and an index stack. Replaces invalid characters with a sentinel (e.g. `'\0'`) or tracks via `bool[]`. Single reconstruction pass. Optimal $O(N)$ time and $O(N)$ memory.
- **Approach 2 (Two-Pass Balance Scan without Stack):** Pass 1 left-to-right: removes illegal `')'` using a balance counter. Pass 2 right-to-left: removes illegal `'('` from the intermediate string using a reverse balance counter. Uses $O(1)$ auxiliary memory (excluding string builder buffers).
- **Selection Rule:** Approach 1 with in-place sentinel replacement is the fastest in C# due to minimal GC allocations.

#### 4.2 Step-by-Step Natural Progression Flow
1. Convert string `s` to mutable `char[] chars`.
2. Traverse forward with stack tracking unmatched `'('` indices.
3. Replace illegal `')'` directly with `'\0'` in `chars`.
4. Empty stack at end, replacing unmatched `'('` with `'\0'`.
5. Compact non-null characters into a final string using a two-pointer write cursor or `new string(chars, 0, writeLen)`.

#### 4.3 Alternative Approaches Analysis
- *Regex / Substring replacement:* Repeatedly replacing `"()"` via regex takes $O(N^2)$ and does not handle arbitrary lowercase letter placement.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Approach | Time (Best/Avg/Worst) | Auxiliary Space | Output Space | Cache Locality | In-Place Mutability | Streaming Suitability |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **1. In-Place Sentinel Mutation** | $O(N) / O(N) / O(N)$ | $O(N)$ (index stack) | $O(N)$ | Optimal (contiguous array) | Yes (mutates char buffer) | Moderate |
| **2. Two-Pass StringBuilder** | $O(N) / O(N) / O(N)$ | $O(1)$ aux | $O(N)$ | High | No (allocates new SB) | High |
| **3. Stack + HashSet** | $O(N) / O(N) / O(N)$ | $O(N)$ (hash table) | $O(N)$ | Moderate | No | Moderate |

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: Minimum Remove to Make Valid Parentheses (LeetCode #1249)
 * ============================================================================
 * Core Pattern      : Two-Phase Balance Invariant with Sentinel Character Compaction
 * Time Complexity   : O(N) linear time across two passes
 * Space Complexity  : O(N) space for character array and primitive stack buffer
 * Selection Rule    : In-place sentinel mutation on char[] avoids intermediate StringBuilder
 *                     resizing and reduces garbage collection pressure in .NET.
 * Defensive Traps   : Ensure unmatched '(' are popped from stack and purged from right-to-left.
 *                     Handle strings with zero parentheses with zero mutation.
 * ============================================================================
 */

using System;
using System.Collections.Generic;

public class Solution
{
    private const char Sentinel = '\0';

    /// <summary>
    /// Removes the minimum number of parentheses to make the string valid.
    /// Employs an index stack and in-place sentinel marking to achieve O(N) runtime.
    /// </summary>
    /// <param name="s">Input string containing parentheses and lowercase letters.</param>
    /// <returns>A valid parentheses string with minimum removals.</returns>
    public string MinRemoveToMakeValid(string s)
    {
        // Guard Clause: Null or empty input
        if (string.IsNullOrEmpty(s))
        {
            return string.Empty;
        }

        char[] chars = s.ToCharArray();
        int n = chars.Length;

        // Stack to store indices of unmatched opening parentheses '('
        // Using Stack<int> pre-allocated or integer array
        var openStack = new Stack<int>();

        // Pass 1: Identify all invalid ')' and collect all candidate '('
        for (int i = 0; i < n; i++)
        {
            char c = chars[i];

            if (c == '(')
            {
                openStack.Push(i);
            }
            else if (c == ')')
            {
                if (openStack.Count > 0)
                {
                    // Valid pair formed; pop matching '('
                    openStack.Pop();
                }
                else
                {
                    // Unmatched ')'; mark as invalid via Sentinel
                    chars[i] = Sentinel;
                }
            }
        }

        // Pass 2: Mark all unclosed '(' as invalid
        // Invariant: Any index remaining in openStack has no matching ')'
        while (openStack.Count > 0)
        {
            int invalidOpenIndex = openStack.Pop();
            chars[invalidOpenIndex] = Sentinel;
        }

        // Pass 3: In-place array compaction using two-pointer write cursor
        int writeIndex = 0;
        for (int readIndex = 0; readIndex < n; readIndex++)
        {
            if (chars[readIndex] != Sentinel)
            {
                chars[writeIndex++] = chars[readIndex];
            }
        }

        // Materialize string directly from compacted char array segment
        return new string(chars, 0, writeIndex);
    }
}
```

---

### 6. Senior Pitfalls & Defensive Traps
- **Pitfall 1: Naive String Concatenation in Loop:**
  - Writing `result += s[i]` inside an $N = 10^5$ loop allocates $O(N^2)$ bytes on the heap, triggering catastrophic Gen 0/1 GC pauses. Always use `char[]` compaction or `StringBuilder`.
- **Pitfall 2: Removing Leftmost `'('` Instead of Rightmost:**
  - For input `"(a(b)"`, balance is +1. Removing the first `'('` yields `"a(b)"` (valid). Removing the second yields `"(ab)"` (also valid). However, the index stack naturally pops the **rightmost** unmatched `'('` first, ensuring minimal perturbation and strictly predictable deterministic output.
- **Pitfall 3: Array Bounds During Compaction:**
  - When compacting with `chars[writeIndex++] = chars[readIndex]`, remember that `new string(chars, 0, writeIndex)` creates the exact string without trailing garbage.
- **Pitfall 4: Memory Footprint of `Stack<int>`:**
  - While `Stack<int>` is acceptable, for ultra-low latency scenarios, an `int[] stack = new int[n]` with a primitive `top` pointer provides zero heap allocation and 100% L1 cache locality.

---

## 125. Valid Word Abbreviation (LeetCode #408)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟢 Easy |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#two-pointers` `#string` `#parsing` `#leading-zero-guard` `#overflow-prevention` |
| **LeetCode Link** | [Valid Word Abbreviation](https://leetcode.com/problems/valid-word-abbreviation/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** A string can be abbreviated by replacing any number of non-adjacent, non-empty substrings with their lengths. The lengths should not have leading zeros. Given a string `word` and an abbreviation `abbr`, return `true` if the string matches the given abbreviation, or `false` otherwise.
- **Assumptions & Contracts:**
  - Non-adjacent replacement rule: Two numerical skip values cannot be directly adjacent in `abbr` (they would have merged into a single number).
  - No leading zeros rule: Any numerical span starting with `'0'` (such as `"01"`, `"0"`, `"09"`) is strictly **invalid** and invalidates the entire abbreviation.
  - Character matching: Non-digit characters in `abbr` must match characters in `word` character-for-character at the corresponding aligned position.
- **Key Constraints:**
  - $1 \le word.Length \le 20$.
  - $1 \le abbr.Length \le 20$.
  - `word` consists of only lowercase English letters.
  - `abbr` consists of lowercase English letters and digits.
- **Senior Edge Cases to Defend:**
  - Leading zero in skip number: `word = "a", abbr = "01"` $\implies$ `false`.
  - Lone zero digit: `word = "a", abbr = "0"` $\implies$ `false`.
  - Skip count exceeds remaining characters: `word = "hi", abbr = "3"` $\implies$ `false`.
  - Multi-digit numbers: `word = "internationalization", abbr = "i12iz4n"` $\implies$ `true`.
  - `abbr` exhausted while `word` has remaining characters: `word = "apple", abbr = "a2"` $\implies$ `false`.
  - `word` exhausted while `abbr` has remaining characters: `word = "apple", abbr = "a5e"` $\implies$ `false`.
  - Integer overflow: If constraints were enlarged to $N > 10^9$, `num = num * 10 + digit` could overflow standard 32-bit signed integers.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Coordinated Dual-Pointer Alignment. Maintain pointer `wPtr` in `word` and `aPtr` in `abbr`. At each step: if `abbr[aPtr]` is a letter, assert character equality and advance both. If `abbr[aPtr]` is a digit, guard against leading `'0'`, parse the entire multi-digit number $K$, advance `wPtr` by $K$, and continue. At termination, both cursors must have reached the exact ends of their respective strings simultaneously.
- **Sample 1:**
  - **Input:** `word = "internationalization"`, `abbr = "i12iz4n"`
  - **Output:** `true`
  - **Explanation:**
    - Match `'i'` with `'i'`.
    - Skip 12 letters (`"nternational"`).
    - Match `'i'` with `'i'`, `'z'` with `'z'`.
    - Skip 4 letters (`"atio"`).
    - Match `'n'` with `'n'`.
    - Both strings cleanly exhausted.
- **Sample 2:**
  - **Input:** `word = "apple"`, `abbr = "a2e"`
  - **Output:** `false`
  - **Explanation:** `'a'` matches `'a'`. Skip 2 letters (`"pp"`). Next char in `word` is `'l'`, but next char in `abbr` is `'e'`. Mismatch!

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine two reading heads advancing over parallel audio tracks.
- Track 1 is the full raw audio tape (`word`).
- Track 2 is a compressed cue sheet (`abbr`).
When Track 2 encounters an explicit sound note (letter), both heads must play identical notes. When Track 2 encounters a fast-forward directive like `"12"`, the Track 1 head must jump forward 12 notches while the Track 2 head simply finishes reading the digits of the directive. If a directive begins with a corrupted `"0"` or commands a jump past the end of the tape, the tape player aborts immediately.

#### 3.2 The Naive Bottleneck & Redundant Computation
- Expanding `abbr` into a regex string (e.g. converting `"12"` into `.{12}`) and executing a regular expression engine allocates objects, compiles nondeterministic finite automata (NFA), and runs in $O(N)$ with heavy constant overhead.
- Generating the full uncompressed string from `abbr` allocates extra strings.
- A dual-pointer linear sweep performs $O(1)$ auxiliary work per character and checks validity in a single pass of at most $\max(|word|, |abbr|)$ steps.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Leading-Zero Invalidation Gate:**
  If `char.IsDigit(abbr[aPtr]) && abbr[aPtr] == '0'`, return `false` immediately. There are no exceptions in the grammar specification.
- **Atomic Multi-Digit Ingestion Invariant:**
  Digits must be parsed as a contiguous unit:
  $$\text{skip} = \sum_{k=0}^{M-1} d_k \cdot 10^{M - 1 - k}$$
  Once parsed, `wPtr` jumps by $\text{skip}$.
- **Simultaneous Boundary Termination:**
  Valid abbreviation holds if and only if upon loop termination:
  $$wPtr == word.Length \quad \land \quad aPtr == abbr.Length$$

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
DUAL-POINTER ALIGNMENT ARCHITECTURE:

word:  i  n  t  e  r  n  a  t  i  o  n  a  l  i  z  a  t  i  o  n
       ^
      wPtr = 0

abbr:  i  1  2  i  z  4  n
       ^
      aPtr = 0

Step 1: 'i' == 'i' -> wPtr = 1, aPtr = 1
Step 2: Parse "12" -> skip = 12.
        wPtr += 12  -> wPtr = 13 ('i')
        aPtr += 2   -> aPtr = 3 ('i')
Step 3: 'i' == 'i' -> wPtr = 14, aPtr = 4
Step 4: 'z' == 'z' -> wPtr = 15, aPtr = 5
Step 5: Parse "4"  -> skip = 4.
        wPtr += 4   -> wPtr = 19 ('n')
        aPtr += 1   -> aPtr = 6 ('n')
Step 6: 'n' == 'n' -> wPtr = 20, aPtr = 7. Both at EOF!
```

#### 3.5 State Transition Triggers & Decision Gates
1. **Letter Matching Gate:**
   - If `char.IsLetter(abbr[aPtr])`:
     - If `wPtr >= word.Length || word[wPtr] != abbr[aPtr]`, return `false`.
     - Advance: `wPtr++`, `aPtr++`.
2. **Leading Zero Gate:**
   - If `abbr[aPtr] == '0'`, return `false` immediately.
3. **Number Parsing Gate:**
   - Initialize `skip = 0`.
   - While `aPtr < abbr.Length && char.IsDigit(abbr[aPtr])`:
     - `skip = skip * 10 + (abbr[aPtr] - '0')`.
     - Defensive check: if `skip > word.Length`, return `false` immediately (prevents integer overflow and excessive jumping).
     - Advance `aPtr++`.
   - Advance: `wPtr += skip`.
4. **Loop Exit Gate:**
   - Return `wPtr == word.Length && aPtr == abbr.Length`.

#### 3.6 Concrete Step-by-Step State Trace
Input: `word = "apple"`, `abbr = "a2e"`

| Step | `wPtr` | `aPtr` | `abbr[aPtr]` | Condition / Action | Next `wPtr` | Next `aPtr` | Status |
| :---: | :---: | :---: | :---: | :--- | :---: | :---: | :---: |
| **0** | 0 (`'a'`) | 0 (`'a'`) | Letter | `word[0] == abbr[0]` $\implies$ match | 1 | 1 | OK |
| **1** | 1 (`'p'`) | 1 (`'2'`) | Digit | Non-zero $\implies$ parse `"2"` | $1 + 2 = 3$ | 2 | OK |
| **2** | 3 (`'l'`) | 2 (`'e'`) | Letter | `word[3] ('l') != abbr[2] ('e')` | — | — | **Mismatch! Return false** |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Two-Pointer Greedy Linear Scan):** Optimal $O(N + M)$ time and $O(1)$ space. Zero heap allocations. Handles all constraints and edge cases directly.
- **Approach 2 (Regex / Pattern Expansion):** Converts abbreviations into regex tokens and tests against word. High overhead, slow execution, poor memory profile.
- **Selection Rule:** Always use Approach 1.

#### 4.2 Step-by-Step Natural Progression Flow
1. Initialize `wPtr = 0, aPtr = 0`.
2. Loop while `wPtr < word.Length && aPtr < abbr.Length`.
3. If digit: check `'0'`, accumulate skip, add to `wPtr`.
4. If letter: compare equality, increment both pointers.
5. Post-loop: verify both pointers exhausted their entire respective strings.

#### 4.3 Alternative Approaches Analysis
- *String Splitting / Tokenization:* Splitting `abbr` into tokens (letters vs numbers) allocates array segments and garbage collector pressure. The two-pointer in-place cursor avoids all allocations.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Approach | Time Complexity | Auxiliary Space | Output Space | Cache Locality | In-Place Mutability | Streaming Suitability |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **1. Dual Pointers** | $O(N + M)$ | $O(1)$ | $O(1)$ | Optimal (string chars) | Non-mutating | High |
| **2. Regex Match** | $O(N + M)$ | $O(M)$ | $O(1)$ | Poor | Non-mutating | Low |
| **3. String Expansion** | $O(N)$ | $O(N)$ | $O(1)$ | Moderate | Non-mutating | Low |

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: Valid Word Abbreviation (LeetCode #408)
 * ============================================================================
 * Core Pattern      : Dual-Pointer String Alignment with In-Place Integer Parsing
 * Time Complexity   : O(N + M) where N = word.Length, M = abbr.Length
 * Space Complexity  : O(1) auxiliary space, zero heap allocations
 * Selection Rule    : Two pointers provide single-pass zero-allocation verification.
 * Defensive Traps   : Must reject leading zeros ('0') immediately.
 *                     Must guard against wPtr + skip exceeding word.Length (overflow/out of bounds).
 *                     Must assert both wPtr == word.Length and aPtr == abbr.Length at termination.
 * ============================================================================
 */

using System;

public class Solution
{
    /// <summary>
    /// Validates whether an abbreviation correctly matches the original word.
    /// Operates in O(N + M) time and O(1) auxiliary space without string allocations.
    /// </summary>
    /// <param name="word">Original uncompressed word.</param>
    /// <param name="abbr">Candidate abbreviated string.</param>
    /// <returns>True if abbreviation is strictly valid; otherwise false.</returns>
    public bool ValidWordAbbreviation(string word, string abbr)
    {
        // Guard Clause: Contracts state non-empty strings
        if (string.IsNullOrEmpty(word) || string.IsNullOrEmpty(abbr))
        {
            return false;
        }

        int wPtr = 0;
        int aPtr = 0;
        int wordLen = word.Length;
        int abbrLen = abbr.Length;

        while (wPtr < wordLen && aPtr < abbrLen)
        {
            char abbrChar = abbr[aPtr];

            if (char.IsLetter(abbrChar))
            {
                // Direct character comparison
                if (word[wPtr] != abbrChar)
                {
                    return false;
                }
                wPtr++;
                aPtr++;
            }
            else if (char.IsDigit(abbrChar))
            {
                // Leading Zero Invariant: Numbers starting with '0' are strictly invalid
                if (abbrChar == '0')
                {
                    return false;
                }

                // Parse multi-digit integer
                int skip = 0;
                while (aPtr < abbrLen && char.IsDigit(abbr[aPtr]))
                {
                    int digit = abbr[aPtr] - '0';

                    // Defensive overflow guard: if skip exceeds word length, cannot possibly match
                    if (skip > (wordLen - digit) / 10)
                    {
                        return false;
                    }

                    skip = skip * 10 + digit;
                    aPtr++;
                }

                // Advance word pointer by the parsed skip amount
                wPtr += skip;
            }
            else
            {
                // Illegal non-alphanumeric character encountered
                return false;
            }
        }

        // Invariant: Both strings must be completely exhausted simultaneously
        return wPtr == wordLen && aPtr == abbrLen;
    }
}
```

---

### 6. Senior Pitfalls & Defensive Traps
- **Pitfall 1: Overlooking the Leading Zero Clause:**
  - Abbreviations like `"a01b"` or `"0"` are invalid. Forgetting `if (abbr[aPtr] == '0') return false;` is the #1 failure mode in Meta interviews for this problem.
- **Pitfall 2: Premature Termination when `wPtr > word.Length`:**
  - If `abbr` has a skip number that jumps beyond `word.Length` (e.g. `word = "hi", abbr = "5"`), `wPtr` becomes $0 + 5 = 5 > 2$. If you only check `wPtr == word.Length` at the end, this correctly returns `false`, BUT if there are remaining letters in `abbr`, the loop might terminate early without reading them. Checking `wPtr > wordLen` prevents unnecessary parsing.
- **Pitfall 3: Not Checking Both Pointers at End:**
  - If `word = "apple", abbr = "app"`, loop exits with `aPtr == 3 == abbrLen`, but `wPtr == 3 < 5`. Checking only one pointer causes false positives!
- **Pitfall 4: Integer Overflow Defense:**
  - While constraints state lengths $\le 20$, production parsers must protect against large digit sequences like `"9999999999999"` which overflow 32-bit signed integers. Using `skip > (wordLen - digit) / 10` defends against arithmetic overflow.

---

## 126. Valid Palindrome II (LeetCode #680)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟢 Easy |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#two-pointers` `#greedy` `#palindrome` `#one-mismatch-branch` |
| **LeetCode Link** | [Valid Palindrome II](https://leetcode.com/problems/valid-palindrome-ii/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given a string `s`, return `true` if the `s` can be palindrome after deleting at most one character from it.
- **Assumptions & Contracts:**
  - Deleting **at most** one character means 0 deletions (already a palindrome) or exactly 1 deletion is allowed.
  - Return boolean `true` if valid, `false` otherwise.
  - The string consists solely of lowercase English letters.
- **Key Constraints:**
  - $1 \le s.Length \le 10^5$.
  - `s` consists of lowercase English letters.
- **Senior Edge Cases to Defend:**
  - Already a strict palindrome: `s = "racecar"` $\implies$ `true` (0 deletions used).
  - Single character: `s = "a"` $\implies$ `true`.
  - Two characters: `s = "ab"` $\implies$ `true` (delete either `'a'` or `'b'`).
  - Deletion at the extreme boundaries: `s = "abca"` $\implies$ `true` (delete `'c'` or `'b'`); `s = "deeee"` $\implies$ `true`.
  - Deletion in the exact center: `s = "abccba"` (0 deletions), `s = "abcxcba"` (0 deletions), `s = "abcyxcba"` (delete `'y'` or `'x'`).
  - Ambiguous mismatch trap: where deleting the left character matches the right, BUT leads to a dead end downstream, whereas deleting the right character leads to a full palindrome (or vice versa). Example: `s = "cupucu"`, where deleting `'p'` yields palindrome `"cucu"`? No, `"cuucu"` is palindrome! Test: at `left=1 ('u'), right=4 ('c')`, deleting `left` tests `"puc"` (false), deleting `right` tests `"upu"` (true!). Algorithm must explore both branches via logical OR!

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Converging Opposing Two Pointers with Single-Branch Disjunction. March `left` from $0$ and `right` from $N - 1$ inward. As long as `s[left] == s[right]`, advance both. Upon discovering the first mismatch `s[left] != s[right]`, at most one deletion is remaining. We greedily branch into exactly two sub-problems: check if `s[left + 1 ... right]` is a strict palindrome OR check if `s[left ... right - 1]` is a strict palindrome.
- **Sample 1:**
  - **Input:** `s = "aba"`
  - **Output:** `true`
- **Sample 2:**
  - **Input:** `s = "abca"`
  - **Output:** `true`
  - **Explanation:** Deleting `'c'` leaves `"aba"`, which is a palindrome.
- **Sample 3:**
  - **Input:** `s = "abc"`
  - **Output:** `false`
  - **Explanation:** Deleting any single character yields a 2-character non-palindrome.

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine an ancient stone bridge being restored from both riverbanks simultaneously. Two engineers start at opposite shores (`left` and `right`) and walk toward the center, inspecting symmetrical stones.
As long as stones match in shape and weight, the bridge structure is sound.
Suddenly, they encounter an irregular stone where `stone[left] != stone[right]`.
Because you are granted a budget of removing **at most one** irregular stone, you have exactly two alternatives:
1. Bulldoze `stone[left]`, and check if the remaining span between `left + 1` and `right` fits seamlessly with zero further defects.
2. Bulldoze `stone[right]`, and check if the remaining span between `left` and `right - 1` fits seamlessly with zero further defects.
If either alternative produces a flawless span, the bridge is saved. If both alternatives reveal another mismatch, the bridge cannot be salvaged within budget.

#### 3.2 The Naive Bottleneck & Redundant Computation
- A brute-force approach iterates through every index $k \in [0, N - 1]$, constructs a new string with character $k$ removed, and checks if the new string is a palindrome.
  - Creating $N$ strings of length $N - 1$ takes $O(N^2)$ memory and time.
  - For $N = 10^5$, $N^2 = 10^{10}$ operations $\implies$ TLE (Time Limit Exceeded) and Out-Of-Memory.
- The greedy invariant recognizes that all outer matching characters `s[0...left-1]` and `s[right+1...N-1]` are already symmetrical. Deleting any character from the outer matching segments cannot fix the internal mismatch between `s[left]` and `s[right]`. Hence, the deletion MUST be either `s[left]` or `s[right]`.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Greedy Matching Invariant:**
  If $s[0 \dots k] = \text{reverse}(s[N - 1 - k \dots N - 1])$, then in any valid single-deletion palindrome, these boundary pairs must match each other. The single permissible deletion cannot be applied to these outer matched characters, because doing so would immediately create an unmatchable deficit on the opposite flank.
- **Decision Disjunction (Branching Invariant):**
  At the very first index pair $(i, j)$ where $s[i] \neq s[j]$:
  $$\text{Valid}(s) \iff \text{IsStrictPalindrome}(s, i + 1, j) \lor \text{IsStrictPalindrome}(s, i, j - 1)$$
  Because the budget of deletions drops from 1 to 0, both sub-checks are purely linear with zero further branching!
  Total operations: at most $2N$ character comparisons $\implies O(N)$ time, $O(1)$ space.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
TWO-POINTER CONVERGENCE & BRANCHING:

String:     r  a  d  [a]  r  x  d  a  r
Indices:    0  1  2   3   4  5  6  7  8
            ^                         ^
           left                      right

Step 1: s[0] == s[8] ('r' == 'r') -> left=1, right=7
Step 2: s[1] == s[7] ('a' == 'a') -> left=2, right=6
Step 3: s[2] == s[6] ('d' == 'd') -> left=3, right=5
Step 4: s[3] != s[5] ('a' != 'x') -> MISMATCH!

Branch 1 (Delete Left):
Test substring s[4...5]: "rx" -> 'r' != 'x' -> False

Branch 2 (Delete Right):
Test substring s[3...4]: "ar" -> 'a' != 'r' -> False
Result: False (cannot form palindrome with 1 deletion)
```

#### 3.5 State Transition Triggers & Decision Gates
1. **Convergence Loop Gate:** While `left < right`:
   - If `s[left] == s[right]`: Advance `left++`, `right--`.
   - If `s[left] != s[right]`:
     - Evaluate `IsPalindromeRange(s, left + 1, right)`. If true, return `true`.
     - Evaluate `IsPalindromeRange(s, left, right - 1)`. If true, return `true`.
     - If both return false, return `false`.
2. **Exhaustion Gate:** If loop terminates without finding any mismatch, string is already a strict palindrome $\implies$ return `true`.

#### 3.6 Concrete Step-by-Step State Trace
Input: `s = "cupucu"`

| Step | `left` | `right` | `s[left]` | `s[right]` | Match? | Action |
| :---: | :---: | :---: | :---: | :---: | :---: | :--- |
| **0** | 0 | 5 | `'c'` | `'u'` | **No!** | Branch into Left / Right deletion |
| **Branch L** | 1 | 5 | — | — | — | Test `s[1...5] = "upucu"`: |
| — | 1 | 5 | `'u'` | `'u'` | Yes | Advance: `left=2, right=4` |
| — | 2 | 4 | `'p'` | `'c'` | **No!** | Branch L fails (returns false) |
| **Branch R** | 0 | 4 | — | — | — | Test `s[0...4] = "cupuc"`: |
| — | 0 | 4 | `'c'` | `'c'` | Yes | Advance: `left=1, right=3` |
| — | 1 | 3 | `'u'` | `'u'` | Yes | Advance: `left=2, right=2` |
| — | 2 | 2 | `'p'` | `'p'` | Loop exit | **Branch R succeeds (returns true)!** |

Overall Result: `true` (deleting `'u'` at index 5 yields `"cupuc"`, a palindrome).

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Two Pointers with Branching Substring Check):** Optimal $O(N)$ time and $O(1)$ space. Checks at most $2N$ comparisons. Zero heap allocations.
- **Approach 2 (Recursive $K$-mismatch generalization):** In an interview follow-up ("What if you can delete up to $K$ characters?"), generalize to DFS with parameter `k`:
  `Dfs(s, left, right, k)` which branches whenever `s[left] != s[right]`. Time: $O(2^K \cdot N)$, Space: $O(K)$.
- **Selection Rule:** Approach 1 is the definitive production solution for $K = 1$.

#### 4.2 Step-by-Step Natural Progression Flow
1. Set `left = 0, right = s.Length - 1`.
2. Loop while `left < right`:
   - If `s[left] == s[right]`, move both cursors inward.
   - If `s[left] != s[right]`, return `IsPalindromeRange(s, left + 1, right) || IsPalindromeRange(s, left, right - 1)`.
3. If no mismatch encountered, return `true`.

#### 4.3 Alternative Approaches Analysis
- *Substring allocations:* Calling `s.Substring(left + 1, right - left)` creates new heap objects. Passing primitive integer indices to a helper method `IsPalindromeRange(s, l, r)` guarantees $O(1)$ auxiliary space and avoids GC churn.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Approach | Time (Best/Avg/Worst) | Auxiliary Space | Output Space | Cache Locality | In-Place Mutability | Streaming Suitability |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **1. Two Pointers + Range Check** | $O(N) / O(N) / O(N)$ | $O(1)$ | $O(1)$ | Optimal (string chars) | Non-mutating | Low (requires both ends) |
| **2. Substring Allocation** | $O(N) / O(N) / O(N)$ | $O(N)$ | $O(1)$ | Moderate (heap GC) | Non-mutating | Low |
| **3. Brute Force (Delete Each)** | $O(N^2) / O(N^2) / O(N^2)$| $O(N)$ | $O(1)$ | Poor | Non-mutating | Low |

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: Valid Palindrome II (LeetCode #680)
 * ============================================================================
 * Core Pattern      : Opposing Two Pointers with Single-Level Greedy Branching
 * Time Complexity   : O(N) single pass with at most one auxiliary linear verification
 * Space Complexity  : O(1) auxiliary space, zero heap memory allocated
 * Selection Rule    : Two pointers converge until first mismatch; test both deletion
 *                     branches in-place using index boundaries without substring cloning.
 * Defensive Traps   : Do NOT assume deleting left is always preferred over right.
 *                     You MUST check both branches via logical OR.
 * ============================================================================
 */

using System;

public class Solution
{
    /// <summary>
    /// Determines whether string s can be a palindrome after deleting at most one character.
    /// Operates in guaranteed O(N) time and O(1) auxiliary memory.
    /// </summary>
    /// <param name="s">Input lowercase string.</param>
    /// <returns>True if at most one deletion yields a palindrome; otherwise false.</returns>
    public bool ValidPalindrome(string s)
    {
        // Guard Clause: Single-char or empty strings are trivially palindromes
        if (string.IsNullOrEmpty(s) || s.Length <= 2)
        {
            return true;
        }

        int left = 0;
        int right = s.Length - 1;

        // Converge opposing pointers
        while (left < right)
        {
            if (s[left] == s[right])
            {
                left++;
                right--;
            }
            else
            {
                // Decision Invariant: The single allowable deletion MUST be either
                // the character at index 'left' OR the character at index 'right'.
                return IsPalindromeRange(s, left + 1, right) ||
                       IsPalindromeRange(s, left, right - 1);
            }
        }

        // Entire string matched with zero deletions
        return true;
    }

    /// <summary>
    /// Verifies whether the substring span s[low...high] is a strict palindrome.
    /// Operates purely on index boundaries to avoid heap allocations.
    /// </summary>
    private static bool IsPalindromeRange(string s, int low, int high)
    {
        while (low < high)
        {
            if (s[low] != s[high])
            {
                return false;
            }
            low++;
            high--;
        }

        return true;
    }
}
```

---

### 6. Senior Pitfalls & Defensive Traps
- **Pitfall 1: The Greedy Asymmetry Trap (Premature Single-Branch Selection):**
  - Upon mismatch `s[left] != s[right]`, checking if `s[left + 1] == s[right]` and only advancing `left` without checking the other branch fails cases like `"cupucu"`. Both `s[left+1] == s[right]` and `s[left] == s[right-1]` can be true, but only ONE leads to a valid downstream palindrome. Always evaluate `BranchA || BranchB`!
- **Pitfall 2: Allocating Substrings:**
  - Writing `IsPalindrome(s.Substring(left + 1, right - left))` allocates a new string on the managed heap. For $N = 10^5$, this allocates ~100 KB and triggers garbage collection. Pass `(string s, int low, int high)` to maintain zero allocation.
- **Pitfall 3: Senior Follow-up — Generalize to $K$ Deletions:**
  - If the interviewer asks: *"What if you can delete up to $K$ characters?"*
  - State the exponential complexity $O(2^K \cdot N)$. For $K = 2$, $4N$ is still linear. For large $K$, transition the interviewer to Dynamic Programming: Longest Palindromic Subsequence (LPS), where the answer is `true` if $N - \text{LPS}(s) \le K$ in $O(N^2)$ time.

---

## 127. Buildings With an Ocean View (LeetCode #1762)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#monotonic-stack` `#right-to-left-scan` `#running-max` `#greedy` |
| **LeetCode Link** | [Buildings With an Ocean View](https://leetcode.com/problems/buildings-with-an-ocean-view/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** There are `n` buildings in a line. You are given an integer array `heights` of size `n` that represents the heights of the buildings in the line. The ocean is to the right of the buildings. A building has an ocean view if the building can see the ocean without obstruction. Formally, building `i` has an ocean view if all the buildings to its right have a smaller height: $heights[i] > heights[j]$ for all $j > i$. Return a list of indices of buildings that have an ocean view, sorted in increasing order.
- **Assumptions & Contracts:**
  - The ocean is situated strictly to the right (beyond index $n - 1$).
  - A view requires strict inequality: $heights[i] > heights[j]$ for all $j > i$. If an eastern building has equal height ($heights[j] == heights[i]$), building $i$'s view is completely obstructed.
  - The rightmost building at index $n - 1$ **always** has an ocean view because there are no buildings to its right.
  - Output indices must be returned in strictly increasing numerical order.
- **Key Constraints:**
  - $1 \le heights.Length \le 10^5$.
  - $1 \le heights[i] \le 10^9$.
- **Senior Edge Cases to Defend:**
  - Strictly increasing heights: `heights = [1, 2, 3, 4]` $\implies$ only the last building `[3]` has a view.
  - Strictly decreasing heights: `heights = [4, 3, 2, 1]` $\implies$ every building `[0, 1, 2, 3]` has a view.
  - All equal heights: `heights = [2, 2, 2, 2]` $\implies$ only the last building `[3]` has a view.
  - Single building: `heights = [5]` $\implies$ `[0]`.
  - Massive heights ($10^9$): values exceed standard display thresholds, requiring comparison via 32-bit signed integers without overflow.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Suffix Maximum / Monotonic Filtering. A building at index $i$ has an ocean view if and only if its height is strictly greater than the maximum height of all buildings to its right: $heights[i] > \max_{j > i} heights[j]$. Scanning from right to left maintains this running maximum scalar in $O(1)$ space. Alternatively, scanning left to right with a Monotonic Decreasing Stack pops any western building that is shorter than or equal to an incoming eastern building.
- **Sample 1:**
  - **Input:** `heights = [4, 2, 3, 1]`
  - **Output:** `[0, 2, 3]`
  - **Explanation:**
    - Building 0 (height 4) sees over 2, 3, 1. (Has view)
    - Building 1 (height 2) is blocked by building 2 (height 3).
    - Building 2 (height 3) sees over 1. (Has view)
    - Building 3 (height 1) sees the ocean directly. (Has view)
- **Sample 2:**
  - **Input:** `heights = [4, 3, 2, 1]`
  - **Output:** `[0, 1, 2, 3]`
- **Sample 3:**
  - **Input:** `heights = [1, 3, 2, 4]`
  - **Output:** `[3]`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine standing on the eastern seaboard at dusk. The ocean sends a horizontal searchlight beam westward inland across the rooftops.
- The first building the beam strikes (building $n - 1$) is fully illuminated.
- As the light sweeps westward, any building shorter than or equal to the tallest building the light has already illuminated remains buried in deep shadow.
- Only when the light encounters a building that towers strictly higher than the current highest rooftop does its crest catch the light. That building now becomes the new benchmark height.
By walking backwards from the ocean towards the inland mountains, we only need to remember one single number: the maximum height seen so far.

#### 3.2 The Naive Bottleneck & Redundant Computation
- A brute-force algorithm tests every building $i \in [0, n - 1]$ by running an inner loop over all $j \in [i + 1, n - 1]$.
  - Total comparisons: $\frac{n(n - 1)}{2} = O(N^2)$.
  - For $N = 10^5$, $10^{10}$ comparisons will time out.
- The bottleneck is repeated recalculation of the right-side maximum. Storing or maintaining the running maximum collapses the entire check into an $O(1)$ comparison per building.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Suffix Maximum Invariant:**
  $$\text{HasOceanView}(i) \iff heights[i] > \text{SuffixMax}(i + 1)$$
  where $\text{SuffixMax}(k) = \max_{j = k}^{n-1} heights[j]$.
- **Running Backward Invariant:**
  By maintaining a scalar `maxHeightSoFar` initialized to 0 (since all heights are $\ge 1$) and iterating $i$ from $n - 1$ down to $0$:
  1. If $heights[i] > maxHeightSoFar$:
     - Building $i$ has an ocean view.
     - Add index $i$ to our candidate list.
     - Update $maxHeightSoFar = heights[i]$.
  2. If $heights[i] \le maxHeightSoFar$:
     - Building $i$ is completely obstructed; discard it.
- **Monotonic Decreasing Stack Equivalence (Streaming Invariant):**
  If data arrives as an online stream from left to right, we maintain a monotonic decreasing stack of building indices. When building $i$ arrives with height $H$, any building $k$ on top of the stack with $heights[k] \le H$ is permanently blocked by building $i$ and popped!

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
RIGHT-TO-LEFT SUFFIX MAX SWEEP:

Heights:     [ 4 ,   2 ,   3 ,   1 ]   Ocean ===>
Indices:       0     1     2     3

Scan Direction: <=================== (Right to Left)

i = 3: heights[3] = 1 > 0  --> HAS VIEW!  maxHeight = 1. Candidates: [3]
i = 2: heights[2] = 3 > 1  --> HAS VIEW!  maxHeight = 3. Candidates: [3, 2]
i = 1: heights[1] = 2 <= 3 --> BLOCKED!   maxHeight = 3. Candidates: [3, 2]
i = 0: heights[0] = 4 > 3  --> HAS VIEW!  maxHeight = 4. Candidates: [3, 2, 0]

Reverse Candidates to restore ascending order: [0, 2, 3]
```

#### 3.5 State Transition Triggers & Decision Gates
1. **Backward Iteration Gate:** Set `maxHeight = 0`. Iterate $i = n - 1$ down to $0$:
   - **View Condition Gate:** If `heights[i] > maxHeight`:
     - Collect $i$.
     - Set `maxHeight = heights[i]`.
   - **Obstructed Condition Gate:** If `heights[i] <= maxHeight`:
     - Skip (do not collect).
2. **Order Rectification Gate:** Since indices were collected in reverse order ($[3, 2, 0]$), reverse the result list or populate an array backwards to output $[0, 2, 3]$.

#### 3.6 Concrete Step-by-Step State Trace
Input: `heights = [4, 2, 3, 1]`

| Step | Index $i$ | `heights[i]` | `maxHeight` Before | Condition ($> \text{maxHeight}$) | Action | `maxHeight` After | Result Buffer |
| :---: | :---: | :---: | :---: | :---: | :---: | :---: | :--- |
| **0** | 3 | 1 | 0 | $1 > 0$ (True) | Keep index 3 | 1 | `[3]` |
| **1** | 2 | 3 | 1 | $3 > 1$ (True) | Keep index 2 | 3 | `[3, 2]` |
| **2** | 1 | 2 | 3 | $2 > 3$ (False) | Obstructed; skip | 3 | `[3, 2]` |
| **3** | 0 | 4 | 3 | $4 > 3$ (True) | Keep index 0 | 4 | `[3, 2, 0]` |

Reverse result buffer: `[0, 2, 3]`.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Right-to-Left Sweep with Running Max):** Optimal batch processing algorithm. $O(N)$ time, $O(1)$ auxiliary space (excluding output array). Minimal CPU instructions, maximum cache efficiency.
- **Approach 2 (Left-to-Right Monotonic Decreasing Stack):** Processes input from left to right. Maintains indices with strictly decreasing heights. When incoming building is $\ge$ stack top, pop stack top.
  - *When to Use Approach 2:* When buildings are streaming continuously from the west and output must be updated dynamically, or when random-access reverse indexing is unavailable.
- **Selection Rule:** Use Approach 1 for batch in-memory arrays; use Approach 2 if asked for streaming input.

#### 4.2 Step-by-Step Natural Progression Flow
1. Check null or empty array.
2. Initialize `maxHeight = 0` and temporary `List<int>`.
3. Loop $i$ from $n - 1$ down to 0: if $heights[i] > maxHeight$, record $i$ and update $maxHeight$.
4. Reverse the collected list and return as `int[]`.

#### 4.3 Alternative Approaches Analysis
- *Prefix Scan with Suffix Array:* Precomputing an array `suffixMax[n]` requires $O(N)$ extra space and two full array sweeps. Running scalar max achieves the same result in a single pass with $O(1)$ auxiliary memory.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Approach | Time (Best/Avg/Worst) | Auxiliary Space | Output Space | Cache Locality | In-Place Mutability | Streaming Suitability |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **1. Right-to-Left Running Max**| $O(N) / O(N) / O(N)$ | $O(1)$ aux | $O(N)$ | Optimal (backward vector) | Non-mutating | Poor (requires end) |
| **2. Monotonic Stack (L-to-R)** | $O(N) / O(N) / O(N)$ | $O(N)$ (stack) | $O(N)$ | High | Non-mutating | Optimal (streaming) |
| **3. Brute Force Forward Scan** | $O(N^2) / O(N^2) / O(N^2)$| $O(1)$ | $O(N)$ | Moderate | Non-mutating | Poor |

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: Buildings With an Ocean View (LeetCode #1762)
 * ============================================================================
 * Primary Approach  : Right-to-Left Suffix Maximum Sweep (O(N) Time, O(1) Aux Space)
 * Secondary Approach: Monotonic Decreasing Stack (O(N) Time, O(N) Space, Streaming-Safe)
 * Invariant         : Building i has view iff heights[i] > max(heights[i+1...n-1])
 * Defensive Traps   : Strict inequality is required (heights[i] > maxHeight).
 *                     Equal height blocks the view.
 *                     Reverse collected indices to return strictly ascending order.
 * ============================================================================
 */

using System;
using System.Collections.Generic;

public class Solution
{
    /// <summary>
    /// Finds all building indices with an ocean view using an optimal backward sweep.
    /// Operates in O(N) time and O(1) auxiliary space (excluding output buffer).
    /// </summary>
    /// <param name="heights">Array of building heights from west to east.</param>
    /// <returns>Array of 0-indexed building indices sorted in ascending order.</returns>
    public int[] FindBuildings(int[] heights)
    {
        // Guard Clause: Trivial input validation
        if (heights == null || heights.Length == 0)
        {
            return Array.Empty<int>();
        }

        int n = heights.Length;
        var viewIndices = new List<int>();
        int maxHeightSoFar = 0;

        // Backward Sweep: Scan eastward to westward (right to left)
        // Invariant: maxHeightSoFar stores the maximum height among all buildings to the right of index i
        for (int i = n - 1; i >= 0; i--)
        {
            int currentHeight = heights[i];

            // Strict Inequality Gate: Equal height obstructs view
            if (currentHeight > maxHeightSoFar)
            {
                viewIndices.Add(i);
                maxHeightSoFar = currentHeight;
            }
        }

        // Output Rectification: Indices were collected descending; invert into ascending order
        int count = viewIndices.Count;
        int[] result = new int[count];
        for (int i = 0; i < count; i++)
        {
            result[i] = viewIndices[count - 1 - i];
        }

        return result;
    }
}

/// <summary>
/// Alternative Implementation: Monotonic Decreasing Stack.
/// Suitable for online streaming scenarios where buildings arrive sequentially from west to east.
/// </summary>
public class SolutionStreaming
{
    public int[] FindBuildings(int[] heights)
    {
        if (heights == null || heights.Length == 0)
        {
            return Array.Empty<int>();
        }

        int n = heights.Length;
        // Stack holds indices of buildings with strictly decreasing heights
        var stack = new Stack<int>();

        for (int i = 0; i < n; i++)
        {
            int currentHeight = heights[i];

            // Invariant Gate: Any building on top of stack with height <= currentHeight
            // is permanently blocked from seeing the ocean by current building
            while (stack.Count > 0 && heights[stack.Peek()] <= currentHeight)
            {
                stack.Pop();
            }

            stack.Push(i);
        }

        // Extract remaining buildings from stack (they emerge in descending order)
        int total = stack.Count;
        int[] result = new int[total];
        for (int i = total - 1; i >= 0; i--)
        {
            result[i] = stack.Pop();
        }

        return result;
    }
}
```

---

### 6. Senior Pitfalls & Defensive Traps
- **Pitfall 1: Non-Strict Inequality ($>$ vs $\ge$):**
  - The problem states: *"all the buildings to its right have a smaller height"*. If `heights = [2, 2]`, building 0 is blocked by building 1. Writing `heights[i] >= maxHeightSoFar` mistakenly includes blocked buildings.
- **Pitfall 2: Forgetting to Reverse the Backward Sweep:**
  - Scanning right-to-left appends indices in descending order ($[3, 2, 0]$). The contract specifies: *"sorted in increasing order"*. Returning without inverting violates the output contract.
- **Pitfall 3: In-Place Output Construction vs `Reverse()`:**
  - In high-throughput C# systems, writing directly into `result[count - 1 - i]` eliminates the extra allocation and call overhead of `viewIndices.Reverse()`.
- **Pitfall 4: Interviewer Pivot — Streaming Input:**
  - Always proactively mention: *"If the data arrives as a real-time stream from left to right, we cannot scan backwards. We pivot to a Monotonic Decreasing Stack that pops obstructed predecessors in $O(N)$ amortized time."* This demonstrates senior architectural agility.

---

## 128. Random Pick with Weight (LeetCode #528)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#binary-search` `#prefix-sum` `#probability` `#cumulative-density-function` `#random-sampling` |
| **LeetCode Link** | [Random Pick with Weight](https://leetcode.com/problems/random-pick-with-weight/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** You are given a 0-indexed array of positive integers `w` where `w[i]` describes the weight of the $i$-th index. You need to implement the function `PickIndex()`, which randomly picks an index in the range $[0, w.Length - 1]$ (inclusive) and returns it. The probability of picking an index $i$ is proportional to its weight:
  $$P(\text{index } i) = \frac{w[i]}{\sum_{j=0}^{n-1} w[j]}$$
- **Assumptions & Contracts:**
  - All weights are strictly positive integers: $w[i] \ge 1$.
  - Multiple calls to `PickIndex()` must maintain the theoretical probability distribution across large sample sizes.
  - Pre-processing time in constructor can be $O(N)$, but each query to `PickIndex()` must be highly efficient ($O(\log N)$ or $O(1)$).
- **Key Constraints:**
  - $1 \le w.Length \le 10^4$.
  - $1 \le w[i] \le 10^5$.
  - `PickIndex()` will be called at most $10^4$ times.
  - The maximum sum of weights can reach $10^4 \times 10^5 = 10^9$, which safely fits within a 32-bit signed integer (`int.MaxValue` $\approx 2.14 \times 10^9$).
- **Senior Edge Cases to Defend:**
  - Single weight: `w = [5]` $\implies$ `PickIndex()` must always return index 0 with probability $1.0$.
  - Highly skewed distribution: `w = [1, 99999]` $\implies$ index 0 picked with $0.001\%$ chance, index 1 with $99.999\%$ chance.
  - Identical weights: `w = [1, 1, 1, 1]` $\implies$ uniform random selection ($25\%$ each).
  - Off-by-one errors in random number generation: selecting target in range $[1, \text{totalWeight}]$ vs $[0, \text{totalWeight} - 1]$.
  - Thread-safety in concurrent environments: `System.Random` is NOT thread-safe and can enter infinite zero-loops under multi-threaded contention.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Cumulative Distribution Function (CDF) + Binary Search (Bisect Left). Transform weights into a contiguous prefix sum array where each index $i$ occupies a segment of length $w[i]$. Generate a uniformly distributed random integer $T \in [1, \text{totalSum}]$. Use binary search to find the first prefix sum $\ge T$.
- **Sample 1:**
  - **Input:** `["Solution", "pickIndex"], [[[1]], []]`
  - **Output:** `[null, 0]`
  - **Explanation:** Only index 0 exists, probability = 1.0.
- **Sample 2:**
  - **Input:** `["Solution", "pickIndex", "pickIndex", "pickIndex"], [[[1, 3]], [], [], []]`
  - **Output:** `[null, 1, 1, 0]` (probabilistic sample)
  - **Explanation:** Total weight = 4. Target drawn uniformly from $[1, 4]$.
    - If target $= 1 \implies$ index 0 (prob = $1/4 = 25\%$).
    - If target $\in \{2, 3, 4\} \implies$ index 1 (prob = $3/4 = 75\%$).

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine a continuous meter-long ribbon.
- If $w = [1, 3]$, you cut the ribbon into two segments:
  - Segment 0 covers interval $(0, 1]$ (length 1).
  - Segment 1 covers interval $(1, 4]$ (length 3).
- You blindfold an archer and let them shoot an arrow at a random point along the total ribbon length $[1, 4]$.
- The arrow hits a single coordinate $T$. To determine which segment the arrow pierced, you do not measure every millimeter from the start; you look at the segment boundary markers $[1, 4]$ and binary search for the first boundary that is greater than or equal to $T$.

#### 3.2 The Naive Bottleneck & Redundant Computation
- **Naive Array Expansion:** Allocate an array of size $\sum w[i]$ and populate it with $w[i]$ copies of index $i$, then pick a random index uniformly.
  - Memory explosion: $\sum w[i]$ can reach $10^9$ integers $\approx 4 \text{ GB}$ of RAM! This triggers an `OutOfMemoryException`.
- **Linear CDF Scan:** Compute prefix sums, draw random $T$, and iterate linearly from $i = 0$ to $N - 1$ until $prefix[i] \ge T$.
  - Per query: $O(N)$ time.
  - Across $Q = 10^4$ calls, total time is $O(N \cdot Q) = 10^8$ operations.
- **Breakthrough:** Because all weights $w[i] > 0$, the prefix sum array is **strictly monotonically increasing**. Monotonicity enables binary search (lower bound) in $O(\log N)$ time per query.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Strict Monotonicity Invariant:**
  $$\forall i \in [0, n - 2]: prefix[i] < prefix[i + 1] \quad (\because w[i+1] \ge 1)$$
- **Interval Partition Mapping:**
  Index $i$ owns the semi-closed integer range:
  $$\text{Interval}(i) = (\text{prefix}[i - 1], \text{prefix}[i]] \quad \text{with } prefix[-1] = 0$$
  The count of integers in this range is exactly:
  $$\text{prefix}[i] - \text{prefix}[i - 1] = w[i]$$
- **Lower Bound Search Invariant:**
  For any random target $T \in [1, \text{totalWeight}]$, the unique index $k$ whose interval covers $T$ is the smallest index $k$ such that:
  $$\text{prefix}[k] \ge T$$
  This is the exact definition of `lower_bound` / `bisect_left`.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
CUMULATIVE DISTRIBUTION FUNCTION (CDF) RIBBON:

Weights:        w = [ 2 ,   5 ,   3 ]
Prefix Sums:    P = [ 2 ,   7 ,  10 ]
Total Sum:      10

Interval Mapping for T in [1, 10]:
   1   2   |   3   4   5   6   7   |   8   9   10
+----------+-----------------------+--------------+
| Index 0  |        Index 1        |   Index 2    |
| (len 2)  |        (len 5)        |   (len 3)    |
+----------+-----------------------+--------------+
            ^
       Random Target T = 4
       First P[k] >= 4 is P[1] = 7  ===> Return Index 1
```

Binary Search Invariant:
- `low = 0, high = n - 1`
- `mid = low + (high - low) / 2`
- If `P[mid] >= target`: target is in `mid` or to its left $\implies high = mid$.
- If `P[mid] < target`: target is strictly to the right $\implies low = mid + 1$.
- Loop terminates when `low == high`, pointing precisely to the target index.

#### 3.5 State Transition Triggers & Decision Gates
1. **Target Generation Gate:**
   - Draw random integer $T \in [1, \text{totalWeight}]$.
   - In C#: `Random.Shared.Next(1, totalWeight + 1)`.
2. **Binary Search Search Space Partition:**
   - While `low < high`:
     - If `prefixSums[mid] >= target`: `high = mid`.
     - Else: `low = mid + 1`.
3. **Terminal Gate:**
   - Return `low`.

#### 3.6 Concrete Step-by-Step State Trace
Weights: `w = [2, 5, 3]`, Prefix Sums: `P = [2, 7, 10]`. Drawn `target = 6`.

| Iteration | `low` | `high` | `mid` | `P[mid]` | Evaluation (`P[mid] >= 6`) | Next Boundary |
| :---: | :---: | :---: | :---: | :---: | :---: | :--- |
| **0** | 0 | 2 | 1 | 7 | $7 \ge 6$ (True) | `high = mid = 1` |
| **1** | 0 | 1 | 0 | 2 | $2 \ge 6$ (False) | `low = mid + 1 = 1` |
| **End** | 1 | 1 | — | — | `low == high` | **Return index 1** |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Prefix Sum + Binary Search / CDF):** The industry standard for discrete weighted sampling. $O(N)$ initialization time, $O(\log N)$ sampling time, $O(N)$ auxiliary memory.
- **Approach 2 (Walker's Alias Method):** Advanced production pattern. Uses two tables (`Prob` and `Alias`) to achieve $O(N)$ initialization and guaranteed $O(1)$ sampling time.
  - *When to Use Alias Method:* When `PickIndex()` is called billions of times in ultra-high-throughput real-time ad selection or ML reinforcement learning environments.
- **Selection Rule:** Approach 1 (Binary Search) is the standard expected interview solution. Discuss Approach 2 (Alias Method) for senior architect differentiation.

#### 4.2 Step-by-Step Natural Progression Flow
1. **Constructor:** Allocate `prefixSums` of length $N$.
2. Compute running sum: `prefixSums[i] = prefixSums[i - 1] + w[i]`. Store `totalSum = prefixSums[n - 1]`.
3. **PickIndex:** Draw random integer in $[1, totalSum]$.
4. Binary search for first index with `prefixSums[mid] >= target`. Return index.

#### 4.3 Alternative Approaches Analysis
- *`Array.BinarySearch`:* .NET provides `Array.BinarySearch(prefixSums, target)`.
  - If exact match is found, returns index $\ge 0$.
  - If not found, returns bitwise complement `~index` (the insertion point of the first element greater than target).
  - Both map directly to the correct index!

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Approach | Init Time | Query Time | Auxiliary Space | Query Cache Locality | Streaming Friendly |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **1. CDF Binary Search** | $O(N)$ | $O(\log N)$ | $O(N)$ | Optimal (binary jump in array) | No (static weights) |
| **2. Walker's Alias Method** | $O(N)$ | $O(1)$ | $O(N)$ | Optimal (table lookup) | No |
| **3. Linear CDF Scan** | $O(N)$ | $O(N)$ | $O(N)$ | High | No |
| **4. Expanded Array** | $O(\sum w)$ | $O(1)$ | $O(\sum w)$ (Gigabytes) | Optimal | No |

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: Random Pick with Weight (LeetCode #528)
 * ============================================================================
 * Core Pattern      : Cumulative Distribution Function (CDF) + Lower-Bound Binary Search
 * Time Complexity   : O(N) Initialization, O(log N) Query per PickIndex()
 * Space Complexity  : O(N) auxiliary space to store prefix sums
 * Selection Rule    : CDF Binary Search balances O(N) memory with lightning O(log N) queries.
 * Defensive Traps   : Random range must be 1-indexed [1, totalWeight] to align with prefix sums.
 *                     Use Random.Shared (.NET 6+) for thread safety in multi-threaded services.
 *                     Guard against 32-bit integer overflow when summing weights.
 * ============================================================================
 */

using System;

public class Solution
{
    private readonly int[] _prefixSums;
    private readonly int _totalWeight;

    /// <summary>
    /// Pre-computes the Cumulative Distribution Function (CDF) prefix sums.
    /// Operates in O(N) time and O(N) space.
    /// </summary>
    /// <param name="w">Array of positive weights.</param>
    public Solution(int[] w)
    {
        if (w == null || w.Length == 0)
        {
            throw new ArgumentException("Weight array must contain at least one positive weight.", nameof(w));
        }

        int n = w.Length;
        _prefixSums = new int[n];
        int runningSum = 0;

        for (int i = 0; i < n; i++)
        {
            // Defensive Check: Contract specifies positive weights
            if (w[i] <= 0)
            {
                throw new ArgumentException($"Weight at index {i} must be positive, found {w[i]}.", nameof(w));
            }

            // Checked arithmetic protects against integer overflow
            checked
            {
                runningSum += w[i];
            }
            _prefixSums[i] = runningSum;
        }

        _totalWeight = runningSum;
    }

    /// <summary>
    /// Randomly picks an index proportional to its weight in O(log N) time.
    /// </summary>
    /// <returns>Selected 0-based index.</returns>
    public int PickIndex()
    {
        // Thread-safe uniform random integer in the closed interval [1, _totalWeight]
        // Random.Shared avoids lock contention and thread corruption in concurrent .NET runtimes
        int target = Random.Shared.Next(1, _totalWeight + 1);

        // Binary Search for Lower Bound (first index where _prefixSums[idx] >= target)
        int low = 0;
        int high = _prefixSums.Length - 1;

        while (low < high)
        {
            // Safe midpoint calculation avoiding overflow
            int mid = low + (high - low) / 2;

            if (_prefixSums[mid] >= target)
            {
                // Invariant: Target falls within mid's interval or an earlier interval
                high = mid;
            }
            else
            {
                // Invariant: Target strictly exceeds mid's interval boundary
                low = mid + 1;
            }
        }

        return low;
    }
}
```

---

### 6. Senior Pitfalls & Defensive Traps
- **Pitfall 1: Zero-Indexed Random Range Bug:**
  - Generating `target = Random.Next(totalWeight)` produces values in $[0, totalWeight - 1]$. If $w = [1]$, `target` could be $0$. But `prefixSums[0] = 1`. If you map ranges as $[0, prefix[0] - 1]$, index 0 is $[0, 0]$ which works, but combining 0-indexed randoms with 1-indexed prefix sums causes off-by-one errors on boundary values. Generating $T \in [1, totalWeight]$ maps cleanly to $[1, prefix[0]], [prefix[0] + 1, prefix[1]]$, etc.
- **Pitfall 2: `System.Random` Multi-Threading Bug:**
  - Instantiating `private readonly Random _rand = new Random();` is dangerous in server environments (ASP.NET Core / microservices). `System.Random` is NOT thread-safe. Concurrent calls from multiple request threads corrupt its internal seed state, causing it to return `0` forever! Always use `Random.Shared` (.NET 6+) or `ThreadLocal<Random>`.
- **Pitfall 3: Integer Overflow on Total Weight:**
  - With $N = 10^4$ and $w[i] = 10^5$, total sum is $10^9$, which fits inside signed 32-bit `int` (max $2.14 \times 10^9$). However, if weights scale to $10^6$ or $N = 10^5$, sum reaches $10^{11}$, requiring `long[]` prefix sums. Mentioning `long` boundaries showcases senior defensiveness.
- **Pitfall 4: Walker's Alias Method Follow-Up:**
  - If the interviewer asks: *"How can we optimize `PickIndex()` to $O(1)$ runtime?"*
  - Be ready to explain the Alias Method: We scale all probabilities by $N$ so the average weight is 1. We partition items into a "smaller" pile (prob $< 1$) and a "larger" pile (prob $\ge 1$), and pair them up so each bucket has exactly one primary index and at most one alias index. Sampling requires picking a uniform random bucket $k \in [0, N - 1]$ and tossing a biased coin to choose between $k$ and $alias[k]$ in guaranteed $O(1)$ time!

---

## 129. Dot Product of Two Sparse Vectors (LeetCode #1570)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#sparse-representation` `#two-pointers` `#hash-table` `#binary-search` `#system-design` |
| **LeetCode Link** | [Dot Product of Two Sparse Vectors](https://leetcode.com/problems/dot-product-of-two-sparse-vectors/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given two sparse vectors, compute their dot product. Implement class `SparseVector`:
  - `SparseVector(int[] nums)`: Initializes the object with the vector `nums`.
  - `int DotProduct(SparseVector vec)`: Computes the dot product between two sparse vectors.
  A sparse vector is a vector that has mostly zero values. You should store the sparse vector efficiently and compute the dot product between two `SparseVector` instances.
- **Assumptions & Contracts:**
  - The dot product of two vectors $A$ and $B$ of length $n$ is defined as:
    $$A \cdot B = \sum_{i=0}^{n-1} A[i] \cdot B[i]$$
  - Any product where either $A[i] = 0$ or $B[i] = 0$ contributes $0$ to the sum and should be bypassed entirely.
  - Both vectors are guaranteed to have identical underlying dimension $n$.
- **Key Constraints:**
  - $n = nums.Length \le 10^5$ (can scale to $n = 10^9$ in real-world ML recommendation systems).
  - $0 \le nums[i] \le 100$.
  - Number of non-zero elements $L \ll n$.
- **Senior Edge Cases to Defend:**
  - Zero non-zero elements: One or both vectors consist entirely of zeros $\implies$ returns 0 immediately.
  - Disjoint non-zero coordinates: $A$ has non-zeros at $\{1, 3, 5\}$ and $B$ has non-zeros at $\{2, 4, 6\} \implies$ returns 0.
  - Extreme sparsity asymmetry: Vector $A$ has $L_1 = 1$ non-zero element, while Vector $B$ has $L_2 = 10^5$ non-zero elements. Linear two-pointer scan over $B$ is wasteful ($O(L_1 + L_2)$); binary searching $A$'s index in $B$ takes $O(L_1 \log L_2) = O(\log L_2)$.
  - Large dimensional vectors: Vector dimension $10^9$, making dense array instantiation impossible.
  - Arithmetic overflow on summation: Sum of products could exceed 32-bit signed integers if values scale up (use checked arithmetic or 64-bit accumulators).

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Coordinate List (COO) Sparse Encoding + Two-Pointer Intersection. Instead of storing $N$ numbers in memory, compress the vector into a sequence of sorted index-value pairs `(index, value)` containing only non-zero coordinates. Computing the dot product simplifies to finding the intersection of two sorted index lists: advance two pointers, multiply values whenever indices match, and increment the pointer with the smaller index otherwise.
- **Sample 1:**
  - **Input:** `nums1 = [1, 0, 0, 2, 3]`, `nums2 = [0, 3, 0, 4, 0]`
  - **Output:** `8`
  - **Explanation:**
    - `v1` non-zeros: `[(0, 1), (3, 2), (4, 3)]`
    - `v2` non-zeros: `[(1, 3), (3, 4)]`
    - Matching index is 3: $v1[3] \times v2[3] = 2 \times 4 = 8$.
- **Sample 2:**
  - **Input:** `nums1 = [0, 1, 0, 0, 0]`, `nums2 = [0, 0, 0, 0, 0]`
  - **Output:** `0`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine two astronomers logging rare comet sightings across a calendar of 100,000 days.
- Astronomer A observed comets on days $[12, 500, 91000]$.
- Astronomer B observed comets on days $[45, 500, 80000, 91000]$.
A naive clerk creates two massive 100,000-page calendars, checks each day one-by-one, and realizes 99,996 days are completely blank on both books.
The senior engineer only looks at their pocket datebooks. Two bookmarks step through the dates. If Date A $<$ Date B, advance Bookmark A. If Date B $<$ Date A, advance Bookmark B. If Date A $==$ Date B, an overlap occurred—multiply their recorded telescope intensities and add to the running total.

#### 3.2 The Naive Bottleneck & Redundant Computation
- Dense Array Representation:
  - Space: $O(N)$ per vector.
  - Dot Product Time: $O(N)$.
  - Bottleneck: For $N = 10^7$ with $10$ non-zeros, dense storage requires 40 MB per vector and 10 million multiplication operations, $99.999\%$ of which multiply by zero.
- Hash Map Representation:
  - Store `{ index -> value }` in a `Dictionary<int, int>`.
  - Iterate over the smaller dictionary: for each key, probe the larger dictionary in $O(1)$ amortized time.
  - Bottleneck: High memory overhead (each dictionary entry in .NET costs ~32 bytes plus hashing overhead and GC tracking) and poor CPU cache locality due to pointer chasing across hash buckets.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Coordinate List (COO) Representation:**
  Store non-zero entries as an array of structs:
  $$\text{Pairs} = [(idx_0, val_0), (idx_1, val_1), \dots, (idx_{L-1}, val_{L-1})]$$
  where $idx_0 < idx_1 < \dots < idx_{L-1}$ is strictly monotonically increasing.
- **Cache-Optimal Two-Pointer Intersection:**
  Because the indices are strictly sorted during construction, finding matching indices requires a single linear sweep using two cursors $p_1$ and $p_2$.
  - If $idx_1[p_1] == idx_2[p_2]$: $\text{sum} += val_1[p_1] \cdot val_2[p_2]; \quad p_1++; \quad p_2++;$
  - If $idx_1[p_1] < idx_2[p_2]$: $p_1++;$
  - If $idx_1[p_1] > idx_2[p_2]$: $p_2++;$
- **Asymmetric Sparsity Adaptive Invariant (Senior System Design):**
  If $|L_1| \ll |L_2|$ (e.g. $L_1 = 2$, $L_2 = 100,000$):
  Running two pointers costs $O(L_1 + L_2) \approx 100,000$ steps.
  Instead, iterate through each of the $L_1$ elements and **binary search** its index inside $L_2$'s sorted array!
  Time: $O(L_1 \log L_2) = 2 \times 17 \approx 34$ steps $\implies 3,000\times$ faster!

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
TWO-POINTER SPARSE INTERSECTION:

v1.Pairs:  [ (0, 1) ,  (3, 2) ,  (4, 3) ]   (L1 = 3)
               ^
               p1

v2.Pairs:  [ (1, 3) ,  (3, 4) ]             (L2 = 2)
               ^
               p2

Step 1: idx1(0) < idx2(1)  ===> p1++
Step 2: idx1(3) > idx2(1)  ===> p2++
Step 3: idx1(3) == idx2(3) ===> MATCH! sum += 2 * 4 = 8. p1++, p2++
Step 4: p2 reaches end of v2. Loop terminates.
Total Result = 8.
```

#### 3.5 State Transition Triggers & Decision Gates
1. **Dimension Parity Gate:** If either vector has 0 non-zero elements, return 0 immediately.
2. **Sparsity Optimization Selector:**
   - If $L_1 \cdot \log_2(L_2) < L_1 + L_2$ and $L_2 > 32$: trigger Binary Search branch.
   - Else: trigger Two-Pointer Linear Merge branch.
3. **Two-Pointer Merge Gates:**
   - While $p_1 < L_1 \land p_2 < L_2$:
     - If $idx_1 == idx_2$: accumulate product, advance both.
     - Else if $idx_1 < idx_2$: advance $p_1$.
     - Else: advance $p_2$.
4. **Return Result:** Return accumulated sum.

#### 3.6 Concrete Step-by-Step State Trace
Vector 1: `[(0, 1), (3, 2), (4, 3)]`  
Vector 2: `[(1, 3), (3, 4)]`

| Step | $p_1$ | $p_2$ | $v_1[p_1]$ | $v_2[p_2]$ | Comparison | Action | Running Sum |
| :---: | :---: | :---: | :---: | :---: | :---: | :---: | :---: |
| **0** | 0 | 0 | `(0, 1)` | `(1, 3)` | $0 < 1$ | $p_1 \leftarrow 1$ | 0 |
| **1** | 1 | 0 | `(3, 2)` | `(1, 3)` | $3 > 1$ | $p_2 \leftarrow 1$ | 0 |
| **2** | 1 | 1 | `(3, 2)` | `(3, 4)` | $3 == 3$ (Hit!) | $2 \times 4 = 8; p_1 \leftarrow 2, p_2 \leftarrow 2$ | 8 |
| **End**| 2 | 2 | — | — | $p_2 == L_2$ | Loop terminates | **8** |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Index-Value Struct Array + Two Pointers):** The enterprise standard. Minimal memory footprint, zero GC heap fragmentation, optimal hardware L1/L2 data prefetching. Time: $O(L_1 + L_2)$, Space: $O(L)$.
- **Approach 2 (Hash Map / Dictionary):** Store non-zero indices in a hash map. Probe smaller map against larger map. Time: $O(\min(L_1, L_2))$ average. Space: $O(L)$ with high constant factor ($4\times$ memory usage of struct array).
- **Approach 3 (Binary Search on Asymmetric Vectors):** When $L_1 \ll L_2$, binary search each index of $L_1$ in $L_2$. Time: $O(L_1 \log L_2)$.

#### 4.2 Step-by-Step Natural Progression Flow
1. **Construction:** Count non-zero elements or scan `nums`, appending struct `(index, value)` to a packed array.
2. **DotProduct:** Access both vector struct lists.
3. Apply two-pointer convergence loop to compute accumulated scalar product.
4. Return scalar result.

#### 4.3 Alternative Approaches Analysis
- *Direct Array Storage:* Fails instantly when $N = 10^9$ (sparse recommender systems / embedding spaces). COO representation is the exact standard utilized by SciPy (`scipy.sparse.coo_matrix`) and PyTorch Sparse.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Approach | Constructor Time | Query Time | Auxiliary Memory | Cache Locality | Handles Asymmetry ($L_1 \ll L_2$) |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **1. Struct Array + Two Pointers** | $O(N)$ | $O(L_1 + L_2)$ | $O(L)$ (8 bytes/elem) | Optimal (contiguous RAM) | Good |
| **2. Struct Array + Binary Search**| $O(N)$ | $O(L_1 \log L_2)$ | $O(L)$ | Optimal | Excellent ($L_1 \ll L_2$) |
| **3. Dictionary / Hash Map** | $O(N)$ | $O(\min(L_1, L_2))$ avg | $O(L)$ (32+ bytes/elem)| Poor (pointer chasing) | Moderate |
| **4. Dense Array** | $O(1)$ | $O(N)$ | $O(N)$ | Optimal | Catastrophic for large $N$ |

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: Dot Product of Two Sparse Vectors (LeetCode #1570)
 * ============================================================================
 * Core Pattern      : Coordinate List (COO) Struct Array with Two-Pointer Intersection
 * Time Complexity   : Construction: O(N); DotProduct: O(L1 + L2) or O(L1 log L2)
 * Space Complexity  : O(L) where L is the number of non-zero elements (L << N)
 * Selection Rule    : Contiguous struct array (readonly record struct) provides
 *                     maximum memory density (8 bytes/entry) and 100% CPU L1 cache hits.
 * Defensive Traps   : Use long accumulator to prevent integer overflow during multiplication.
 *                     Support asymmetric vectors via binary search when L1 << L2.
 * ============================================================================
 */

using System;
using System.Collections.Generic;

public class SparseVector
{
    // Readonly struct guarantees zero per-element heap allocation overhead
    // Size: 4 bytes (Index) + 4 bytes (Value) = 8 bytes total per non-zero entry
    public readonly struct Element
    {
        public readonly int Index;
        public readonly int Value;

        public Element(int index, int value)
        {
            Index = index;
            Value = value;
        }
    }

    // Exposed internally for efficient dot product access
    public readonly Element[] Elements;

    /// <summary>
    /// Initializes sparse vector by compressing dense array into non-zero COO struct array.
    /// Operates in O(N) time and allocates memory strictly proportional to non-zero count L.
    /// </summary>
    /// <param name="nums">Input dense integer vector.</param>
    public SparseVector(int[] nums)
    {
        if (nums == null || nums.Length == 0)
        {
            Elements = Array.Empty<Element>();
            return;
        }

        // Count non-zeros first to allocate exact array capacity with zero dynamic resizing churn
        int nonZeroCount = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] != 0) nonZeroCount++;
        }

        Elements = new Element[nonZeroCount];
        int writeIdx = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] != 0)
            {
                Elements[writeIdx++] = new Element(i, nums[i]);
            }
        }
    }

    /// <summary>
    /// Computes dot product between this vector and another sparse vector.
    /// Adaptively selects between Two-Pointer Merge and Binary Search based on sparsity asymmetry.
    /// </summary>
    /// <param name="vec">Second sparse vector.</param>
    /// <returns>Computed scalar dot product.</returns>
    public int DotProduct(SparseVector vec)
    {
        ArgumentNullException.ThrowIfNull(vec);

        Element[] listA = this.Elements;
        Element[] listB = vec.Elements;

        int lenA = listA.Length;
        int lenB = listB.Length;

        // Trivial Disjoint / Empty Gate
        if (lenA == 0 || lenB == 0)
        {
            return 0;
        }

        // Asymmetric Optimization: If one vector is dramatically sparser than the other,
        // binary search each index of the sparser vector in the larger vector
        if (lenA > lenB)
        {
            // Ensure listA is always the shorter vector
            (listA, listB) = (listB, listA);
            (lenA, lenB) = (lenB, lenA);
        }

        // Theoretical threshold: If lenA * log2(lenB) is significantly smaller than lenA + lenB
        if (lenA * 16 < lenB)
        {
            return DotProductBinarySearch(listA, listB);
        }

        // Standard Two-Pointer Merge
        int pA = 0;
        int pB = 0;
        long dotProductSum = 0;

        while (pA < lenA && pB < lenB)
        {
            int idxA = listA[pA].Index;
            int idxB = listB[pB].Index;

            if (idxA == idxB)
            {
                dotProductSum += (long)listA[pA].Value * listB[pB].Value;
                pA++;
                pB++;
            }
            else if (idxA < idxB)
            {
                pA++;
            }
            else
            {
                pB++;
            }
        }

        return (int)dotProductSum;
    }

    /// <summary>
    /// Computes dot product by binary searching each coordinate of smaller vector inside larger vector.
    /// Runtime: O(lenA * log(lenB)).
    /// </summary>
    private static int DotProductBinarySearch(Element[] smaller, Element[] larger)
    {
        long sum = 0;

        foreach (var elem in smaller)
        {
            int targetIdx = elem.Index;
            int low = 0;
            int high = larger.Length - 1;

            while (low <= high)
            {
                int mid = low + (high - low) / 2;
                int midIdx = larger[mid].Index;

                if (midIdx == targetIdx)
                {
                    sum += (long)elem.Value * larger[mid].Value;
                    break;
                }
                else if (midIdx < targetIdx)
                {
                    low = mid + 1;
                }
                else
                {
                    high = mid - 1;
                }
            }
        }

        return (int)sum;
    }
}
```

---

### 6. Senior Pitfalls & Defensive Traps
- **Pitfall 1: Blindly Choosing `Dictionary<int, int>`:**
  - When asked how to represent sparse vectors, junior candidates immediately blurt out "Hash Map!".
  - In a senior Meta interview, explain the memory and performance reality: A .NET `Dictionary<int, int>` incurs internal entry structs (`int hashCode, int next, int key, int value` = 16 bytes), a bucket array pointer, object overhead, and GC references $\to \approx 32\text{--}40$ bytes per non-zero entry. In contrast, an array of `readonly struct (int Index, int Value)` uses exactly 8 bytes per entry, zero GC references, and packs 8 entries per 64-byte CPU L1 cache line!
- **Pitfall 2: Neglecting Vector Asymmetry ($L_1 \ll L_2$):**
  - If the interviewer asks: *"What if Vector A has 2 non-zeros, but Vector B has 500,000 non-zeros?"*
  - The two-pointer approach scans all 500,000 elements of $B$. Binary searching the 2 elements of $A$ in $B$ takes $2 \times \lceil\log_2 500000\rceil \approx 38$ operations! Implementing the adaptive threshold showcases world-class systems maturity.
- **Pitfall 3: Summation Overflow:**
  - Multiplying large components $10^5 \times 10^5 = 10^{10}$ exceeds 32-bit `int`. Storing the accumulator as `long` and casting at the boundary defends against arithmetic corruption.

---

## 130. Lowest Common Ancestor of a Binary Tree III (LeetCode #1650)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#two-pointers` `#linked-list-cycle` `#tree` `#parent-pointer` `#lowest-common-ancestor` |
| **LeetCode Link** | [Lowest Common Ancestor of a Binary Tree III](https://leetcode.com/problems/lowest-common-ancestor-of-a-binary-tree-iii/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given two nodes of a binary tree `p` and `q`, return their lowest common ancestor (LCA). You have a reference to each node, and each node has a reference to its parent:
  ```csharp
  public class Node {
      public int val;
      public Node left;
      public Node right;
      public Node parent;
  }
  ```
  The lowest common ancestor is defined between two nodes `p` and `q` as the lowest node in `T` that has both `p` and `q` as descendants (where we allow a node to be a descendant of itself).
- **Assumptions & Contracts:**
  - All node values are unique.
  - `p != q`.
  - Both `p` and `q` exist in the same tree.
  - The tree root has `root.parent == null`.
  - You are given direct references to `p` and `q` (the root reference is **not** provided!).
- **Key Constraints:**
  - The number of nodes in the tree is in the range $[2, 10^5]$.
  - $-10^9 \le Node.val \le 10^9$.
- **Senior Edge Cases to Defend:**
  - One node is the direct ancestor of the other: `p` is the parent of `q` $\implies$ LCA is `p`.
  - Sibling nodes: `p` and `q` share the same immediate parent $\implies$ LCA is `p.parent`.
  - The LCA is the root of the tree: `p` and `q` reside in opposite main subtrees $\implies$ LCA is root.
  - Asymmetric depths: `p` is at depth 1, `q` is at depth $10^4$.
  - Disjoint tree trap (Defensive verification): If `p` and `q` were in separate disconnected trees, cycle-switch would loop infinitely unless guarded.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Isomorphism to Intersection of Two Linked Lists (LeetCode #160). Because each node possesses a unique `parent` reference, following the `parent` pointers from any node upwards toward the root forms a singly linked list terminating at `null`. Finding the LCA of `p` and `q` is mathematically identical to finding the intersection node of two converging linked lists. By running two pointers that switch heads upon reaching `null`, both pointers traverse identical total path lengths ($d_p + d_q$) and collide at the LCA in $O(H)$ time and $O(1)$ space.
- **Sample 1:**
  - **Input:** `root = [3,5,1,6,2,0,8,null,null,7,4], p = 5, q = 1`
  - **Output:** `3`
  - **Explanation:** Node 3 is the lowest common ancestor of 5 and 1.
- **Sample 2:**
  - **Input:** `root = [3,5,1,6,2,0,8,null,null,7,4], p = 5, q = 4`
  - **Output:** `5`
  - **Explanation:** Node 5 is the ancestor of node 4 (4 is child of 2, 2 is child of 5).

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine two mountain climbers starting at different base camps on a mountain:
- Climber A starts at altitude $p$.
- Climber B starts at altitude $q$.
Both climbers ascend along single-track trails marked by `parent` cairns. Eventually, their two trails merge at a ridge shelter (the LCA), and from that shelter, a single unified trail leads to the summit (the root).
If Climber A reaches the summit, descends by helicopter to Climber B's base camp, and resumes climbing, while Climber B reaches the summit, flies to Climber A's base camp, and resumes climbing:
Both climbers will hike the exact same total distance:
$$\text{Trail}_A + \text{SharedTrail} + \text{Trail}_B$$
Because their speeds are identical, they will walk into the ridge shelter at the exact same minute!

#### 3.2 The Naive Bottleneck & Redundant Computation
- **HashSet Ancestor Logging:**
  Traverse from `p` upwards to the root, inserting every ancestor into a `HashSet<Node>`. Then traverse upwards from `q`; the first node found in the hash set is the LCA.
  - Time: $O(H)$ where $H$ is tree height.
  - Space: $O(H)$ heap memory.
  - Bottleneck: For a skewed tree where $H = 10^5$, allocating a hash set of $10^5$ nodes consumes several megabytes of heap memory and incurs garbage collection churn.
- **Root-Finding + Standard LCA (LC #236):**
  Ascend from `p` to find `root`, then call classic recursive LCA from `root` downwards.
  - Time: $O(N)$ because downward LCA visits every node in the tree!
  - Degradation: Visits $N$ nodes instead of being bounded by tree height $H$.
- **Two-Pointer Constant-Space Invariant:**
  Path length equalization achieves $O(H)$ time with guaranteed $O(1)$ auxiliary memory.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Path Length Equalization Invariant:**
  Let:
  - $a$ = distance from $p$ to LCA.
  - $b$ = distance from $q$ to LCA.
  - $c$ = distance from LCA to root.
  - Distance from root to `null` = 1.
  Path traversed by Pointer $A$:
  $$\text{Path}_A = a + c + 1 \quad (\text{switches to } q) \quad + b = a + b + c + 1$$
  Path traversed by Pointer $B$:
  $$\text{Path}_B = b + c + 1 \quad (\text{switches to } p) \quad + a = a + b + c + 1$$
  Since $\text{Path}_A = \text{Path}_B$, after traversing exactly $a + b + c$ edges, both pointers point to the exact same node: the Lowest Common Ancestor!

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
PATH EQUALIZATION TOPOLOGY:

               Root (3)
                /   \
              [5]   [1]
             /   \
            6    [2]
                /   \
               7    [4]

Ascending Parent Paths:
p = 4:  4 -> 2 -> 5 -> 3 -> null   (len = 4)
q = 5:  5 -> 3 -> null             (len = 2)

Cursor A: 4 -> 2 -> 5 -> 3 -> null -> [Switch to q=5] -> 5
Cursor B: 5 -> 3 -> null -> [Switch to p=4] -> 4 -> 2 -> 5
Both cursors arrive at Node 5 simultaneously!
Node 5 is the LCA!
```

#### 3.5 State Transition Triggers & Decision Gates
1. Initialize `currA = p, currB = q`.
2. **Convergence Loop Gate:** While `currA != currB`:
   - `currA = (currA == null) ? q : currA.parent;`
   - `currB = (currB == null) ? p : currB.parent;`
3. **Termination Gate:** Loop exits when `currA == currB`. Return `currA`.

#### 3.6 Concrete Step-by-Step State Trace
Tree: `p = 4`, `q = 5`, LCA = `5`. Path from 4: `4 -> 2 -> 5 -> 3 -> null`. Path from 5: `5 -> 3 -> null`.

| Step | `currA` | `currB` | `currA == currB` | Next `currA` | Next `currB` |
| :---: | :---: | :---: | :---: | :---: | :---: |
| **0** | Node 4 | Node 5 | False | Node 2 (`4.parent`) | Node 3 (`5.parent`) |
| **1** | Node 2 | Node 3 | False | Node 5 (`2.parent`) | `null` (`3.parent`) |
| **2** | Node 5 | `null` | False | Node 3 (`5.parent`) | Node 4 (Switches to `p`) |
| **3** | Node 3 | Node 4 | False | `null` (`3.parent`) | Node 2 (`4.parent`) |
| **4** | `null` | Node 2 | False | Node 5 (Switches to `q`) | Node 5 (`2.parent`) |
| **5** | **Node 5** | **Node 5** | **True!** | **Match found: Return Node 5** | — |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Two-Pointer Path Equalization):** The most elegant algorithm. $O(H)$ time, $O(1)$ auxiliary space. Zero data structure allocations.
- **Approach 2 (Depth Alignment / Lift):** Compute depth $d_p$ and $d_q$ by counting steps to root. Lift the deeper node by $|d_p - d_q|$ steps so both nodes are at the identical depth. Then advance both upwards synchronously until they meet.
  - *Advantage:* Highly intuitive and mirrors the Lowest Common Ancestor algorithm in Operating System process trees and Git commit DAGs. $O(H)$ time, $O(1)$ space.
- **Approach 3 (HashSet of Ancestors):** Store ancestors of `p` in a hash set; find first hit from `q`. $O(H)$ time, $O(H)$ space.
- **Selection Rule:** Present Approach 1 as primary for code elegance; implement Approach 2 as secondary to demonstrate multi-paradigm mastery.

#### 4.2 Step-by-Step Natural Progression Flow
1. Check if `p == q` (return `p`).
2. Initialize two pointer variables `currA = p, currB = q`.
3. Loop while `currA != currB`. Advance each to parent, or wrap to opposite head if null.
4. Return `currA`.

#### 4.3 Alternative Approaches Analysis
- *Depth Alignment Mechanics:*
  - `GetDepth(node)`: loops `curr = curr.parent` while incrementing count.
  - `while (depthA > depthB) currA = currA.parent;`
  - `while (currA != currB) { currA = currA.parent; currB = currB.parent; }`
  - Completely immune to infinite loops even if trees were disjoint.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Approach | Time (Best/Avg/Worst) | Auxiliary Space | Code Footprint | Disjoint Tree Resilient | Cache Locality |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **1. Two-Pointer Cycle Switch**| $O(1) / O(H) / O(H)$ | $O(1)$ | 8 lines | Needs guard | High |
| **2. Depth Normalization (Lift)**| $O(1) / O(H) / O(H)$ | $O(1)$ | 20 lines | Yes | High |
| **3. Ancestor HashSet** | $O(1) / O(H) / O(H)$ | $O(H)$ (heap set) | 12 lines | Yes | Low |

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: Lowest Common Ancestor of a Binary Tree III (LeetCode #1650)
 * ============================================================================
 * Primary Approach  : Dual-Pointer Path Equalization (O(H) Time, O(1) Space)
 * Secondary Approach: Depth Normalization & Synchronous Lift (O(H) Time, O(1) Space)
 * Invariant         : Both pointers traverse d_p + d_q edges, arriving at LCA simultaneously
 * Defensive Traps   : Guard against p == q immediately.
 *                     Null redirection must switch to the OPPOSITE starting node:
 *                     currA = (currA == null) ? q : currA.parent
 * ============================================================================
 */

using System;

public class Node
{
    public int val;
    public Node? left;
    public Node? right;
    public Node? parent;

    public Node(int val)
    {
        this.val = val;
    }
}

public class Solution
{
    /// <summary>
    /// Finds the lowest common ancestor of two nodes in O(H) time and O(1) auxiliary space.
    /// Exploits the isomorphism between parent pointer trees and converging linked lists.
    /// </summary>
    /// <param name="p">First tree node reference.</param>
    /// <param name="q">Second tree node reference.</param>
    /// <returns>The Lowest Common Ancestor node.</returns>
    public Node? LowestCommonAncestor(Node? p, Node? q)
    {
        // Guard Clause: Contract checks
        if (p == null || q == null) return null;
        if (p == q) return p;

        Node? currA = p;
        Node? currB = q;

        // Path Equalization Invariant:
        // Pointer A traverses: Path(p -> root) + Path(q -> LCA)
        // Pointer B traverses: Path(q -> root) + Path(p -> LCA)
        // Both path lengths are mathematically equal: d_p + d_q
        while (currA != currB)
        {
            // Switch heads upon reaching null
            currA = (currA == null) ? q : currA.parent;
            currB = (currB == null) ? p : currB.parent;
        }

        return currA;
    }
}

/// <summary>
/// Secondary Approach: Depth Normalization & Synchronous Lift.
/// Highly resilient and explicit; mirrors OS process hierarchy LCA resolution.
/// </summary>
public class SolutionDepthNormalization
{
    public Node? LowestCommonAncestor(Node? p, Node? q)
    {
        if (p == null || q == null) return null;

        int depthP = GetDepth(p);
        int depthQ = GetDepth(q);

        // Normalize depths: lift the deeper node so both are at identical altitude
        while (depthP > depthQ)
        {
            p = p!.parent;
            depthP--;
        }

        while (depthQ > depthP)
        {
            q = q!.parent;
            depthQ--;
        }

        // Ascend synchronously until pointers collide
        while (p != q)
        {
            p = p!.parent;
            q = q!.parent;
        }

        return p;
    }

    private static int GetDepth(Node node)
    {
        int depth = 0;
        Node? curr = node;
        while (curr != null)
        {
            depth++;
            curr = curr.parent;
        }
        return depth;
    }
}
```

---

### 6. Senior Pitfalls & Defensive Traps
- **Pitfall 1: Confusing LC #1650 with LC #236:**
  - LC #236 gives `root`, `p`, and `q`, but **no** parent pointers $\implies$ requires recursive post-order tree traversal from root.
  - LC #1650 provides direct node references with parent pointers, but **no** root $\implies$ starting a DFS search from root requires traversing to root first and degrades complexity to $O(N)$ instead of $O(H)$!
- **Pitfall 2: Infinite Loop under Disjoint Forests:**
  - If `p` and `q` belong to two completely separate trees (a disconnected forest), neither pointer will ever meet, and they will cycle through `p` and `q` indefinitely. In an enterprise setting, defend by limiting head switches to at most 2, or checking `GetRoot(p) == GetRoot(q)`.
- **Pitfall 3: Null Redirection Logic Bug:**
  - Writing `currA = (currA.parent == null) ? q : currA.parent` skips the `null` step and desynchronizes the total step count by 1! The pointer MUST transition through `null` before switching to `q`, ensuring both paths include the root's null terminator.

---

## 131. Nested List Weight Sum (LeetCode #339)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#bfs` `#dfs` `#recursion` `#level-order` `#nested-structure` |
| **LeetCode Link** | [Nested List Weight Sum](https://leetcode.com/problems/nested-list-weight-sum/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** You are given a nested list of integers `nestedList`. Each element is either an integer, or a list whose elements may also be integers or other lists. The depth of an integer is the number of lists that it is inside of. For example, the nested list `[1, [2, 2], [[3], 2], 1]` has each integer's value and depth as follows:
  - Four `1`'s at depth 1.
  - Three `2`'s at depth 2.
  - One `3` at depth 3.
  Return the sum of each integer in `nestedList` multiplied by its depth.
- **Interface Contract (`NestedInteger`):**
  ```csharp
  public interface NestedInteger {
      // @return true if this NestedInteger holds a single integer, rather than a nested list.
      bool IsInteger();
      // @return the single integer that this NestedInteger holds, if it holds a single integer.
      // Return null if this NestedInteger holds a nested list.
      int GetInteger();
      // Set this NestedInteger to hold a single integer.
      void SetInteger(int value);
      // Set this NestedInteger to hold a nested list and adds a nested integer to it.
      void Add(NestedInteger ni);
      // @return the nested list that this NestedInteger holds, if it holds a nested list.
      // Return null if this NestedInteger holds a single integer.
      IList<NestedInteger> GetList();
  }
  ```
- **Key Constraints:**
  - $1 \le nestedList.Count \le 50$.
  - The values of integers in the nested list are in the range $[-100, 100]$.
  - The maximum depth of any integer is less than or equal to $50$.
- **Senior Edge Cases to Defend:**
  - Empty top-level list: `nestedList = []` $\implies$ returns 0.
  - Lists containing empty nested lists: `nestedList = [[]]` $\implies$ returns 0.
  - Flat list with zero nesting: `nestedList = [1, 2, 3]` $\implies$ all at depth 1; sum = $1(1) + 2(1) + 3(1) = 6$.
  - Single deeply nested integer: `nestedList = [[[[5]]]]` $\implies$ depth 4; sum = $5 \times 4 = 20$.
  - Negative numbers with deep weights: negative values must subtract proportionally: $-10 \times 4 = -40$.
  - Enterprise stack depth defense: in real-world JSON/YAML deserialization engines, depth can exceed thousands ($D > 10^4$), causing `StackOverflowException` in naive recursive DFS.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Depth-Weighted Structural Traversal. The problem represents an arbitrary rose-tree (n-ary tree) where leaves hold integers and internal nodes represent grouping brackets.
  - In DFS: Traverse recursively, carrying an integer `depth` accumulator (initialized to 1). When encountering a leaf (`IsInteger() == true`), contribute `GetInteger() * depth` to the running sum. When encountering an internal node, recursively invoke on the sublist with `depth + 1`.
  - In BFS: Queue elements level-by-level using queue snapshot sweeps. All integers dequeued at level $d$ are multiplied by scalar $d$, while sublists are unpacked and enqueued for level $d + 1$.
- **Sample 1:**
  - **Input:** `nestedList = [[1, 1], 2, [1, 1]]`
  - **Output:** `10`
  - **Explanation:**
    - Four `1`'s at depth 2: $4 \times (1 \times 2) = 8$.
    - One `2` at depth 1: $1 \times (2 \times 1) = 2$.
    - Total sum = $8 + 2 = 10$.
- **Sample 2:**
  - **Input:** `nestedList = [1, [4, [6]]]`
  - **Output:** `27`
  - **Explanation:**
    - `1` at depth 1 $\implies 1 \times 1 = 1$.
    - `4` at depth 2 $\implies 4 \times 2 = 8$.
    - `6` at depth 3 $\implies 6 \times 3 = 18$.
    - Total sum = $1 + 8 + 18 = 27$.

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine a set of Russian Matryoshka nesting dolls.
- When an integer sits out on the open table (outside all dolls), it has depth 1.
- If it is placed inside 1 doll, its weight doubles (multiplier 2).
- If placed inside 2 nested dolls, its weight triples (multiplier 3).
Whether you crack open each doll completely down to its core before touching the next doll (Depth-First Search), or open all outer shells simultaneously level-by-level across the table (Breadth-First Search), every integer leaf is assigned an unambiguous integer multiplier equal to the number of enclosing shells.

#### 3.2 The Naive Bottleneck & Redundant Computation
- A naive attempt flattens the nested list into a 1D string or linear collection:
  - Flaw: Flattening destroys the nesting metadata. To recover depth, you must count bracket depths, which adds an unnecessary intermediate serialization pass.
- In-place recursive traversal or queue-based level processing visits every element and list wrapper **exactly once**, achieving optimal $O(N)$ linear time where $N$ is the total count of nested elements (integers + list wrappers).

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Recursive Decomposition Invariant:**
  For any nested list $L$ at depth $d$:
  $$\text{WeightSum}(L, d) = \sum_{x \in L} \begin{cases} x.\text{GetInteger}() \cdot d & \text{if } x.\text{IsInteger}() \\ \text{WeightSum}(x.\text{GetList}(), d + 1) & \text{otherwise} \end{cases}$$
- **Level-Order Queue Invariant (BFS):**
  At the beginning of iteration $d$, the queue contains **only** elements that reside strictly at structural depth $d$.
  By snapshotting `levelSize = queue.Count`, we process all depth-$d$ elements in a batch:
  - If element is integer: $\text{totalSum} += \text{val} \cdot d$.
  - If element is list: enqueue each child into the queue (they will be processed at depth $d + 1$).
- **Contrast with LeetCode #364 (Nested List Weight Sum II):**
  In LC #364, depth weight is reversed: depth is measured from the deepest leaf upwards. In LC #364, BFS maintains a cumulative running sum without knowing max depth upfront. In LC #339, standard depth weight is applied downwards, making both DFS and BFS equally straightforward.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
RECURSION TREE / LEVEL-ORDER DECOMPOSITION:

Input: [ 1 , [ 4 , [ 6 ] ] ]

Depth 1:  [1]           [ List A ]
          val = 1       children: [4, List B]
          sum += 1 * 1

Depth 2:                [4]           [ List B ]
                        val = 4       children: [6]
                        sum += 4 * 2

Depth 3:                              [6]
                                      val = 6
                                      sum += 6 * 3

Final Sum: 1 + 8 + 18 = 27
```

#### 3.5 State Transition Triggers & Decision Gates
1. **DFS Transition Gates:**
   - Base Case / Leaf Gate: `if (elem.IsInteger()) return elem.GetInteger() * depth;`
   - Recursive Internal Gate: Loop through `elem.GetList()`, accumulate `Dfs(child, depth + 1)`.
2. **BFS Transition Gates:**
   - Initialize `depth = 1`, enqueue all top-level `NestedInteger` items.
   - While `queue.Count > 0`:
     - Snapshot `levelSize = queue.Count`.
     - For $i = 0 \dots levelSize - 1$:
       - Dequeue `curr`.
       - If `curr.IsInteger()`: `totalSum += curr.GetInteger() * depth`.
       - Else: Enqueue all items in `curr.GetList()`.
     - Increment `depth++`.

#### 3.6 Concrete Step-by-Step State Trace
Input: `[[1, 1], 2, [1, 1]]`

| Level / Depth | Queue Snapshot Size | Dequeued Element | `IsInteger()` | Value / Action | Level Multiplier | Running Total Sum |
| :---: | :---: | :---: | :---: | :---: | :---: | :---: |
| **Depth 1** | 3 items | `List [1, 1]` | False | Enqueue two `1`s | — | 0 |
| — | — | `Integer 2` | True | $2 \times 1 = 2$ | 1 | 2 |
| — | — | `List [1, 1]` | False | Enqueue two `1`s | — | 2 |
| **Depth 2** | 4 items | `Integer 1` | True | $1 \times 2 = 2$ | 2 | 4 |
| — | — | `Integer 1` | True | $1 \times 2 = 2$ | 2 | 6 |
| — | — | `Integer 1` | True | $1 \times 2 = 2$ | 2 | 8 |
| — | — | `Integer 1` | True | $1 \times 2 = 2$ | 2 | **10** |
| **Depth 3** | 0 items | Queue empty | — | Loop terminates | — | **10** |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Depth-First Search / Recursion):** Highly compact and idiomatic. $O(N)$ time, $O(D)$ auxiliary call stack space where $D$ is maximum nesting depth.
- **Approach 2 (Breadth-First Search / Level Queue):** Iterative and heap-safe. Eliminates recursive call stack frames, preventing stack overflow on deep inputs. $O(N)$ time, $O(W)$ auxiliary memory where $W$ is maximum level width.
- **Selection Rule:** DFS is preferred in live interviews for concise implementation. Mention BFS as the production-safe defense against stack overflow when maximum depth is unbounded.

#### 4.2 Step-by-Step Natural Progression Flow
1. **DFS Progression:**
   - Define recursive helper `CalculateSum(IList<NestedInteger> list, int depth)`.
   - Iterate over items; sum integer products and recursive calls.
   - Invoke `CalculateSum(nestedList, 1)` and return.
2. **BFS Progression:**
   - Validate non-empty list. Enqueue elements into `Queue<NestedInteger>`.
   - Loop with level size tracking, multiplying integers by running `depth`.
   - Increment `depth` each iteration.

#### 4.3 Alternative Approaches Analysis
- *Iterative DFS with Explicit Stack:* Use `Stack<(NestedInteger Node, int Depth)>`. Combines DFS depth-first order with heap-allocated stack memory, combining the benefits of both worlds.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Approach | Time (Best/Avg/Worst) | Auxiliary Space | Call Stack Risk | Cache Locality | Streaming Suitability |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **1. Recursive DFS** | $O(N) / O(N) / O(N)$ | $O(D)$ (call stack) | StackOverflow if $D > 10^4$ | High | Poor |
| **2. Level-Order BFS** | $O(N) / O(N) / O(N)$ | $O(W)$ (heap queue) | Zero (heap allocated) | Moderate | High (level-by-level) |
| **3. Iterative DFS Stack** | $O(N) / O(N) / O(N)$ | $O(D)$ (heap stack) | Zero (heap allocated) | High | Poor |

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: Nested List Weight Sum (LeetCode #339)
 * ============================================================================
 * Primary Approach  : Recursive Depth-First Search with Depth Accumulator (O(N) Time, O(D) Space)
 * Secondary Approach: Level-Order Breadth-First Search (O(N) Time, O(W) Space, Stack-Safe)
 * Invariant         : Contribution of each leaf integer is value * depth.
 * Defensive Traps   : Guard against empty nested sublists.
 *                     Validate depth starts strictly at 1.
 *                     Never call GetInteger() without verifying IsInteger() is true.
 * ============================================================================
 */

using System;
using System.Collections.Generic;

// LeetCode's provided interface contract
public interface NestedInteger
{
    bool IsInteger();
    int GetInteger();
    void SetInteger(int value);
    void Add(NestedInteger ni);
    IList<NestedInteger> GetList();
}

public class Solution
{
    /// <summary>
    /// Computes the depth-weighted sum of a nested list using recursive Depth-First Search.
    /// Operates in O(N) time where N is the total number of integers and nested lists.
    /// </summary>
    /// <param name="nestedList">List of NestedInteger elements.</param>
    /// <returns>The calculated weighted sum.</returns>
    public int DepthSum(IList<NestedInteger>? nestedList)
    {
        if (nestedList == null || nestedList.Count == 0)
        {
            return 0;
        }

        return Dfs(nestedList, depth: 1);
    }

    private static int Dfs(IList<NestedInteger> list, int depth)
    {
        int totalSum = 0;

        foreach (var nestedItem in list)
        {
            // Defensive Contract Gate: Check variant type before extracting value
            if (nestedItem.IsInteger())
            {
                // Invariant: Integer contribution is scaled by its structural nesting depth
                totalSum += nestedItem.GetInteger() * depth;
            }
            else
            {
                // Recursive Step: Descend into nested sublist with incremented depth
                var subList = nestedItem.GetList();
                if (subList != null && subList.Count > 0)
                {
                    totalSum += Dfs(subList, depth + 1);
                }
            }
        }

        return totalSum;
    }
}

/// <summary>
/// Secondary Approach: Iterative Breadth-First Search (Level-Order Queue).
/// Recommended for enterprise environments where arbitrary input depth could overflow the OS stack.
/// </summary>
public class SolutionBfs
{
    public int DepthSum(IList<NestedInteger>? nestedList)
    {
        if (nestedList == null || nestedList.Count == 0)
        {
            return 0;
        }

        var queue = new Queue<NestedInteger>();

        // Ingest initial top-level elements (Depth = 1)
        foreach (var item in nestedList)
        {
            if (item != null) queue.Enqueue(item);
        }

        int depth = 1;
        int totalSum = 0;

        while (queue.Count > 0)
        {
            int levelCount = queue.Count;

            // Invariant: All elements in this batch reside strictly at current 'depth'
            for (int i = 0; i < levelCount; i++)
            {
                var current = queue.Dequeue();

                if (current.IsInteger())
                {
                    totalSum += current.GetInteger() * depth;
                }
                else
                {
                    var children = current.GetList();
                    if (children != null)
                    {
                        foreach (var child in children)
                        {
                            if (child != null) queue.Enqueue(child);
                        }
                    }
                }
            }

            depth++;
        }

        return totalSum;
    }
}
```

---

### 6. Senior Pitfalls & Defensive Traps
- **Pitfall 1: Calling `GetInteger()` on a List Node:**
  - Invoking `GetInteger()` when `IsInteger()` returns `false` violates the interface contract and can throw an exception or return garbage values. Always branch on `IsInteger()` first.
- **Pitfall 2: Confusing with LeetCode #364 (Nested List Weight Sum II):**
  - In LC #364, weights are assigned in reverse: leaves at maximum depth have weight 1, and root elements have weight `maxDepth`.
  - In LC #364, computing max depth requires two passes, OR a brilliant single-pass BFS trick: at each level, add all leaf values to an `unweightedSum`, and add `unweightedSum` to `weightedSum` at every level (since earlier levels get added repeatedly, naturally weighting them by their distance to the bottom!).
  - Clearly articulating this distinction demonstrates elite mastery to a Meta interviewer.
- **Pitfall 3: Stack Overflow on Deep Nesting:**
  - Operating system thread stacks in .NET default to 1 MB (or 256 KB on 32-bit / musl Linux). A nested JSON payload with depth $10^4$ will trigger an unrecoverable `StackOverflowException`. Mentioning BFS or an explicit `Stack<(NestedInteger, int)>` on the heap is a standard senior engineering differentiator.

---

## 132. Merge Sorted Array (LeetCode #88)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟢 Easy |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#two-pointers` `#reverse-in-place` `#array-mutation` `#three-pointers` |
| **LeetCode Link** | [Merge Sorted Array](https://leetcode.com/problems/merge-sorted-array/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** You are given two integer arrays `nums1` and `nums2`, sorted in non-decreasing order, and two integers `m` and `n`, representing the number of elements in `nums1` and `nums2` respectively. Merge `nums1` and `nums2` into a single array sorted in non-decreasing order. The final sorted array should not be returned by the function, but instead be stored inside the array `nums1`. To accommodate this, `nums1` has a length of $m + n$, where the first $m$ elements denote the elements that should be merged, and the last $n$ elements are set to 0 and should be ignored. `nums2` has a length of $n$.
- **Assumptions & Contracts:**
  - `nums1` has allocated capacity exactly equal to $m + n$.
  - Both input arrays are pre-sorted in non-decreasing order.
  - The modification must occur **strictly in-place** inside `nums1`.
  - Return type is `void`.
- **Key Constraints:**
  - $nums1.Length == m + n$.
  - $nums2.Length == n$.
  - $0 \le m, n \le 200$.
  - $1 \le m + n \le 200$.
  - $-10^9 \le nums1[i], nums2[j] \le 10^9$.
- **Senior Edge Cases to Defend:**
  - $m = 0$ (`nums1` has 0 elements, only buffer space): copy all of `nums2` into `nums1`.
  - $n = 0$ (`nums2` is empty): `nums1` is already fully merged; 0 operations required.
  - All elements in `nums2` strictly smaller than `nums1`: `nums1 = [4, 5, 6, 0, 0, 0], nums2 = [1, 2, 3]`. All `nums1` elements shift to the right, followed by `nums2` flushing into the front.
  - All elements in `nums2` strictly larger than `nums1`: `nums1 = [1, 2, 3, 0, 0, 0], nums2 = [4, 5, 6]`. `nums2` elements copy directly into the rear without displacing `nums1`.
  - Duplicate values across arrays: `nums1 = [2, 2, 0], nums2 = [2]`. Non-decreasing stability must be preserved.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Reverse Three-Pointer In-Place Merge. Merging from the front requires shifting elements rightward ($O(M \cdot N)$) or allocating an auxiliary array of size $M$ ($O(M)$ extra space). However, because the empty buffer of size $N$ is positioned at the very tail of `nums1`, merging **from the back** (`writeIndex = m + n - 1`) guarantees that the write cursor can NEVER overwrite an unprocessed element of `nums1`. Compare the largest remaining elements at `p1 = m - 1` and `p2 = n - 1`, write the larger to `nums1[writeIndex]`, and decrement cursors.
- **Sample 1:**
  - **Input:** `nums1 = [1, 2, 3, 0, 0, 0], m = 3`, `nums2 = [2, 5, 6], n = 3`
  - **Output:** `nums1` mutated to `[1, 2, 2, 3, 5, 6]`
  - **Explanation:** The arrays merged are `[1, 2, 3]` and `[2, 5, 6]`.
- **Sample 2:**
  - **Input:** `nums1 = [1], m = 1`, `nums2 = [], n = 0`
  - **Output:** `nums1` remains `[1]`
- **Sample 3:**
  - **Input:** `nums1 = [0], m = 0`, `nums2 = [1], n = 1`
  - **Output:** `nums1` mutated to `[1]`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine a parking garage with $M + N$ bays in a single line.
- The first $M$ bays are filled with cars sorted by size.
- The last $N$ bays are completely empty.
A delivery flatbed arrives with $N$ new cars, also sorted by size.
If you try to park the smallest new car into Bay 0, you would have to move every single existing car one bay down—a logistical nightmare.
Instead, you look at the **largest** cars: compare the biggest car in the garage (at Bay $M - 1$) with the biggest car on the flatbed (at Bay $N - 1$). Park the absolute biggest into the very last empty bay (Bay $M + N - 1$).
Because you are filling the garage from the back, you will never crush a car that hasn't been parked yet!

#### 3.2 The Naive Bottleneck & Redundant Computation
- **Append & Re-Sort:** Copy `nums2` into `nums1[m ... m + n - 1]` and call `Array.Sort(nums1)`.
  - Time: $O((M + N) \log(M + N))$.
  - Flaw: Throws away the crucial precondition that both `nums1` and `nums2` were **already sorted**.
- **Forward Merge with Auxiliary Buffer:** Copy `nums1[0 ... m - 1]` into a temporary array `temp` of size $M$, then merge `temp` and `nums2` into `nums1`.
  - Space: $O(M)$ auxiliary memory.
  - Flaw: Fails the strict $O(1)$ space requirement.
- **Backward Three Pointers:** Achieves $O(M + N)$ linear time with zero heap allocations ($O(1)$ auxiliary memory).

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **The Non-Collision Invariant:**
  At any step, let:
  - $p_1$: index of the next unread element in `nums1` (initially $m - 1$).
  - $p_2$: index of the next unread element in `nums2` (initially $n - 1$).
  - $w$: index of the next write target in `nums1` (initially $m + n - 1$).
  The number of elements written to the back of `nums1` is:
  $$\text{written} = (m + n - 1) - w$$
  The number of elements written that came from `nums2` is at most $n - 1 - p_2 \ge 0$.
  Therefore, the index $w$ is strictly bounded:
  $$w = p_1 + (p_2 + 1) \ge p_1$$
  Because $w \ge p_1$ is an absolute mathematical invariant for all execution steps, the write cursor $w$ can **never** overtake or overwrite $p_1$!
  Unprocessed elements in `nums1` are 100% safe.
- **The Early Termination Invariant:**
  If $p_2 < 0$, all elements from `nums2` have been successfully placed into `nums1`.
  Any remaining elements in `nums1[0 ... p_1]` are **already in their correct sorted positions**! The merge can terminate immediately.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
REVERSE THREE-POINTER ARCHITECTURE:

nums1: [ 1 ,  2 ,  3 ,  0 ,  0 ,  0 ]    m = 3
                   ^              ^
                   p1 = 2         write = 5

nums2: [ 2 ,  5 ,  6 ]                   n = 3
                   ^
                   p2 = 2

Step 1: nums2[2] (6) > nums1[2] (3)  ===> nums1[5] = 6, p2=1, write=4
Step 2: nums2[1] (5) > nums1[2] (3)  ===> nums1[4] = 5, p2=0, write=3
Step 3: nums1[2] (3) > nums2[0] (2)  ===> nums1[3] = 3, p1=1, write=2
Step 4: nums1[1] (2) == nums2[0] (2) ===> nums1[2] = 2, p2=-1, write=1
p2 < 0: nums2 exhausted. Elements [1, 2] in nums1 are already in place!
Merged: [ 1 ,  2 ,  2 ,  3 ,  5 ,  6 ]
```

#### 3.5 State Transition Triggers & Decision Gates
1. **Convergence Loop Gate:** While `p2 >= 0`:
   - If `p1 >= 0 && nums1[p1] > nums2[p2]`:
     - `nums1[write--] = nums1[p1--];`
   - Else:
     - `nums1[write--] = nums2[p2--];`
2. **Termination Gate:** As soon as `p2 < 0`, exit immediately.

#### 3.6 Concrete Step-by-Step State Trace
Input: `nums1 = [1, 2, 3, 0, 0, 0], m = 3`, `nums2 = [2, 5, 6], n = 3`

| Step | `p1` | `p2` | `write` | `nums1[p1]` | `nums2[p2]` | Decision (`p1 >= 0 && nums1[p1] > nums2[p2]`) | Target Written | Mutated `nums1` |
| :---: | :---: | :---: | :---: | :---: | :---: | :---: | :---: | :--- |
| **Init**| 2 | 2 | 5 | 3 | 6 | $3 > 6$ (False) | `nums1[5] = 6`, `p2--` | `[1, 2, 3, 0, 0, 6]` |
| **1** | 2 | 1 | 4 | 3 | 5 | $3 > 5$ (False) | `nums1[4] = 5`, `p2--` | `[1, 2, 3, 0, 5, 6]` |
| **2** | 2 | 0 | 3 | 3 | 2 | $3 > 2$ (True) | `nums1[3] = 3`, `p1--` | `[1, 2, 3, 3, 5, 6]` |
| **3** | 1 | 0 | 2 | 2 | 2 | $2 > 2$ (False) | `nums1[2] = 2`, `p2--` | `[1, 2, 2, 3, 5, 6]` |
| **Exit**| 1 | -1| 1 | — | — | `p2 < 0` $\implies$ Terminate | None | `[1, 2, 2, 3, 5, 6]` |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Reverse Three Pointers):** The undisputed optimal algorithm. $O(M + N)$ time, strictly $O(1)$ auxiliary space, in-place mutation.
- **Approach 2 (Forward Merge + Auxiliary Buffer):** Allocates $O(M)$ extra array. $O(M + N)$ time, $O(M)$ space. Violates $O(1)$ space constraint.
- **Approach 3 (Append + Sort):** Copy `nums2` and run QuickSort/IntroSort. $O((M + N) \log(M + N))$ time. Discards sorting invariant.
- **Selection Rule:** Approach 1 is the mandatory production implementation.

#### 4.2 Step-by-Step Natural Progression Flow
1. Set `p1 = m - 1, p2 = n - 1, write = m + n - 1`.
2. While `p2 >= 0`:
   - If `p1 >= 0` and `nums1[p1] > nums2[p2]`, assign `nums1[write--] = nums1[p1--]`.
   - Else assign `nums1[write--] = nums2[p2--]`.
3. Return (void).

#### 4.3 Alternative Approaches Analysis
- *Redundant `p1` Copy Loop:* Writing a secondary loop `while (p1 >= 0) nums1[write--] = nums1[p1--];` is functionally benign but logically redundant: each `nums1[p1]` would merely be copied to index `p1`. Recognizing that this loop is unnecessary is a classic mark of senior precision.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Approach | Time Complexity | Auxiliary Space | Mutates Input | Memory Locality | Precondition Exploit |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **1. Reverse Three Pointers** | $O(M + N)$ | $O(1)$ | Yes (`nums1`) | Optimal (sequential back-scan) | Full |
| **2. Forward Auxiliary Buffer** | $O(M + N)$ | $O(M)$ | Yes (`nums1`) | High | Full |
| **3. Append & Sort** | $O((M+N) \log(M+N))$| $O(1)$ | Yes (`nums1`) | High | None (discards order)|

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: Merge Sorted Array (LeetCode #88)
 * ============================================================================
 * Core Pattern      : Reverse Three-Pointer In-Place Array Mutation
 * Time Complexity   : O(M + N) linear scan where each element is moved at most once
 * Space Complexity  : O(1) auxiliary space, zero heap memory allocated
 * Selection Rule    : Filling nums1 from the rear (m + n - 1) leverages the empty buffer
 *                     and guarantees unprocessed elements at p1 are never overwritten.
 * Defensive Traps   : Loop condition MUST be driven by p2 >= 0.
 *                     If nums2 is exhausted first, nums1 is already in sorted position.
 *                     If nums1 is exhausted first (p1 < 0), remaining nums2 MUST be flushed.
 * ============================================================================
 */

using System;

public class Solution
{
    /// <summary>
    /// Merges nums2 into nums1 in-place as a single sorted array.
    /// Operates in O(M + N) time and strictly O(1) auxiliary memory.
    /// </summary>
    /// <param name="nums1">Destination array of size m + n.</param>
    /// <param name="m">Number of initial valid elements in nums1.</param>
    /// <param name="nums2">Source array of size n.</param>
    /// <param name="n">Number of valid elements in nums2.</param>
    public void Merge(int[] nums1, int m, int[] nums2, int n)
    {
        // Guard Clauses
        if (nums1 == null || nums2 == null)
        {
            throw new ArgumentNullException(nums1 == null ? nameof(nums1) : nameof(nums2));
        }

        // Pointer initializations at valid array endpoints
        int p1 = m - 1;          // Cursor pointing to latest unmerged element in nums1
        int p2 = n - 1;          // Cursor pointing to latest unmerged element in nums2
        int writeIndex = m + n - 1; // Write cursor filling nums1 from the rear

        // Invariant: Fill from the rear. Since writeIndex >= p1 at all times,
        // writeIndex will never overwrite an unread element at p1.
        while (p2 >= 0)
        {
            // If nums1 still has elements and its current element is strictly greater,
            // write nums1[p1] to the rear.
            if (p1 >= 0 && nums1[p1] > nums2[p2])
            {
                nums1[writeIndex--] = nums1[p1--];
            }
            else
            {
                // Otherwise write nums2[p2] to the rear.
                // This branch also covers the case where p1 < 0 (nums1 fully exhausted).
                nums1[writeIndex--] = nums2[p2--];
            }
        }

        // Note: No 'while (p1 >= 0)' is needed!
        // If p2 < 0, all elements from nums2 are placed, and any remaining elements
        // in nums1[0...p1] are already in their correct sorted positions.
    }
}
```

---

### 6. Senior Pitfalls & Defensive Traps
- **Pitfall 1: Driving Loop by `p1 >= 0 && p2 >= 0`:**
  - If written as `while (p1 >= 0 && p2 >= 0)`, when `p1` hits $-1$ while `p2` still has elements (e.g. `nums1 = [0], m = 0, nums2 = [1], n = 1`), the loop exits prematurely! You must remember to write the second cleanup loop: `while (p2 >= 0) nums1[write--] = nums2[p2--];`.
  - Driving the primary loop by `while (p2 >= 0)` and checking `if (p1 >= 0 && nums1[p1] > nums2[p2])` inside solves this cleanly in a single unified loop!
- **Pitfall 2: Adding a Redundant `p1` Flush Loop:**
  - Writing `while (p1 >= 0) nums1[write--] = nums1[p1--];` demonstrates a lack of deep invariant awareness. At that point, $write == p_1$, so every assignment does `nums1[p1] = nums1[p1]`—a completely redundant no-op.
- **Pitfall 3: Forward Merge Overwrite Disaster:**
  - Trying to merge forward starting at index 0 immediately destroys the original elements of `nums1` unless shifted, turning an $O(M + N)$ algorithm into an $O(M \cdot N)$ disaster.

---
