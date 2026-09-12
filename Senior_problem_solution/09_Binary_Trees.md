# Phase 09: Binary Trees & BST

> **Focus:** Recursive Tree Invariants, Bottom-Up Post-Order DP, Level-Order Queue Snapshots, BST Validation Ranges, Lowest Common Ancestors, and Iterative In-Order Early Stopping.  
> **Source Curriculum:** [`Senior_dsa_question_list.md`](../Senior_dsa_question_list.md) — Phase 9 (Problems #48–#55)

---

## TreeNode Definition

```csharp
public class TreeNode
{
    public int val;
    public TreeNode left;
    public TreeNode right;
    public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
    {
        this.val = val;
        this.left = left;
        this.right = right;
    }
}
```

---

## 48. Maximum Depth of Binary Tree (LeetCode #104)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟢 Easy |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#binary-tree` `#dfs-recursion` `#bfs-queue` `#tree-depth` |
| **LeetCode Link** | [Maximum Depth of Binary Tree](https://leetcode.com/problems/maximum-depth-of-binary-tree/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given the `root` of a binary tree, return its maximum depth. Maximum depth is the number of nodes along the longest path from the root node down to the farthest leaf node.
- **Key Constraints:**
  - Number of nodes in range $[0, 10^4]$.
  - Node values in range $[-100, 100]$.
- **Senior Edge Cases to Defend:**
  - Empty tree (`root == null` $\implies 0$).
  - Degenerate single-line skewed tree (height equals $N$; risks call stack overflow in recursive DFS without deep stack capacity).
  - Single-node tree (`root.left == null && root.right == null` $\implies 1$).
  - Completely balanced full binary tree of height $H = \log_2 N$.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** The height of any node is $1 + \max(\text{Height}(left), \text{Height}(right))$. Equivalently, depth is the count of BFS concentric levels until the queue empties.
- **Sample 1:**
  - **Input:** `root = [3, 9, 20, null, null, 15, 7]`
  - **Output:** `3`
- **Sample 2:**
  - **Input:** `root = [1, null, 2]`
  - **Output:** `2`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)
- **3.1 The Intuitive Spark & Conceptual Metaphor:**
  - *Subtree Bubble-Up vs Concentric Ripple Expansion:* A parent node cannot determine its own height in isolation; it must wait for the acoustic echoes from its left and right subtrees. The left child returns the maximum depth of its territory, the right child returns the maximum depth of its territory, and the parent aggregates: $1 + \max(h_L, h_R)$.
  - *Concentric Wavefront:* In BFS, depth is measured like water ripples expanding from the epicenter (root). Each discrete wave consumes an entire generation before advancing to the next layer.
- **3.2 The Naive Bottleneck & Redundant Computation:**
  - A naive top-down path enumeration approach traverses from root to every leaf individually, repeatedly visiting ancestor nodes $O(H)$ times, yielding $O(N \cdot H)$ work.
  - By shifting to bottom-up post-order DP or level-order batching, each node and edge is traversed exactly once, eliminating redundant ancestor re-scans.
- **3.3 The Breakthrough Insight & Mathematical Invariant:**
  - **Recurrence Relation:** For any subtree rooted at $u$:
    $$\text{Depth}(u) = \begin{cases} 0 & \text{if } u = \text{null} \\ 1 + \max(\text{Depth}(u.left), \text{Depth}(u.right)) & \text{otherwise} \end{cases}$$
  - **Correctness Guarantee:** Optimal substructure holds unconditionally because the maximum path down a tree cannot loop or cross subtrees without passing through the local root.
- **3.4 Cursor Semantics & Invariant Partition Architecture:**
  ```text
  Recursive DFS Call Stack State:
  [ Unvisited Subtrees ] -> [ Active Frame: u (Suspended on left) ] -> [ Left Child Finished (h_L) ]
                         -> [ Active Frame: u (Suspended on right) ] -> [ Right Child Finished (h_R) ]
                         -> [ Frame Resolution: Return 1 + max(h_L, h_R) ]

  BFS Queue Horizon Partition:
  +-----------------------------------+-----------------------------------+
  |   Processed Horizon (Depth d-1)   |  Active Frontier (Depth d, size k)|  Pending Horizon (Depth d+1)
  |   Already dequeued and counted    |  Nodes being drained currently    |  Newly enqueued children
  +-----------------------------------+-----------------------------------+
  ```
- **3.5 State Transition Triggers & Decision Gates:**
  - **Base Gate:** `node == null` $\implies$ return `0`.
  - **Post-Order Merge Gate:** Both children evaluated $\implies$ return `1 + Math.Max(leftDepth, rightDepth)`.
  - **BFS Queue Snapshot Gate:** Outer loop records `levelSize = queue.Count`. Drain exactly `levelSize` elements before incrementing `depth`.
- **3.6 Concrete Step-by-Step State Trace:**
  - Input: `[3, 9, 20, null, null, 15, 7]`
  
  | Step | Event / Node | Call Stack / Queue State | Computed Left / Right | Returned Depth |
  | :--- | :--- | :--- | :--- | :--- |
  | 1 | Visit 3 | Call `MaxDepth(3)` | Waiting... | Pending |
  | 2 | Visit 9 | Call `MaxDepth(9)` | Left = 0, Right = 0 | $1 + \max(0,0) = 1$ |
  | 3 | Visit 20 | Call `MaxDepth(20)` | Waiting on 15, 7 | Pending |
  | 4 | Visit 15 | Call `MaxDepth(15)` | Left = 0, Right = 0 | $1 + \max(0,0) = 1$ |
  | 5 | Visit 7 | Call `MaxDepth(7)` | Left = 0, Right = 0 | $1 + \max(0,0) = 1$ |
  | 6 | Unwind 20 | Both children resolved | Left = 1, Right = 1 | $1 + \max(1,1) = 2$ |
  | 7 | Unwind 3 | Both children resolved | Left = 1, Right = 2 | $1 + \max(1,2) = 3$ |

### 4. Approach & Complexity Deconstruction
- **4.1 Anchor Points & Approach Selection Criteria:**
  - **Approach 1 (Recursive Post-Order DFS):** Ideal for general interview scenarios due to concise 4-line implementation. Trade-off: Vulnerable to `StackOverflowException` if the tree degrades into an unbranched linked list of depth $N = 10^4$.
  - **Approach 2 (Iterative BFS Queue):** Preferred in production systems processing untrusted, potentially highly unbalanced topologies. Heap-allocated queue memory is bounded by tree width ($W \le \lceil N/2 \rceil$).
  - **Approach 3 (Iterative DFS with Stack):** Emulates recursion stack on the managed heap using a explicit `Stack<(TreeNode, int)>`.
- **4.2 Step-by-Step Natural Progression Flow:**
  - *Step 1: Setup & Boundaries:* Handle empty root base case returning 0.
  - *Step 2: Main Exploration Loop:* Recurse on children (DFS) or snapshot `queue.Count` (BFS).
  - *Step 3: Invariant Maintenance & Condition Gates:* Enqueue non-null children; avoid enqueuing null pointers to prevent queue pollution.
  - *Step 4: Resolution & Return:* Aggregate depth bottom-up (DFS) or return accumulated level counter (BFS).
- **4.3 Alternative Approaches Analysis:**
  - Morris Traversal computes depth in $O(1)$ auxiliary space by modifying tree pointers, but adds substantial pointer mutation complexity with two traversals per edge.
- **4.4 Multi-Dimensional Complexity & Trade-Off Matrix:**

| Approach | Time (Best) | Time (Avg) | Time (Worst) | Aux Space (Avg) | Aux Space (Worst) | Cache Locality | Streaming Suitability |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **Recursive Post-Order DFS** | $O(N)$ | $O(N)$ | $O(N)$ | $O(\log N)$ stack | $O(N)$ stack | High (call stack) | Low (requires full tree) |
| **Iterative BFS Queue** | $O(N)$ | $O(N)$ | $O(N)$ | $O(W) \approx O(N/2)$ | $O(N/2)$ | Medium (Queue heap) | High (Level streaming) |
| **Iterative DFS Stack** | $O(N)$ | $O(N)$ | $O(N)$ | $O(\log N)$ heap | $O(N)$ heap | Medium | Low |

### 5. Production C# Implementations

```csharp
// ============================================================================
// ARCHITECTURE ANCHOR: Maximum Depth of Binary Tree
// Primary: Recursive Post-Order DFS (Minimalist, O(N) Time, O(H) Auxiliary Stack)
// Secondary: Iterative Queue BFS (Stack-Safe, O(N) Time, O(W) Auxiliary Queue)
// Invariant: Depth(u) = 1 + max(Depth(u.left), Depth(u.right))
// ============================================================================

public class Solution
{
    /// <summary>
    /// Computes maximum depth using recursive bottom-up post-order DFS.
    /// Eliminates redundant path traversals by solving subproblems bottom-up.
    /// </summary>
    public int MaxDepth(TreeNode root)
    {
        // Decision Gate: Base case: Null pointer represents an empty subtree of depth 0
        if (root == null)
        {
            return 0;
        }

        // Subproblem Exploration: Compute heights of left and right child subtrees
        int leftDepth = MaxDepth(root.left);
        int rightDepth = MaxDepth(root.right);

        // Mathematical Invariant: Current node depth is 1 plus max child branch depth
        return 1 + Math.Max(leftDepth, rightDepth);
    }
}

public class SolutionBfs
{
    /// <summary>
    /// Computes maximum depth using queue-based level order traversal (BFS).
    /// Stack-safe against degenerate skewed binary trees.
    /// </summary>
    public int MaxDepth(TreeNode root)
    {
        // Edge Gate: Empty tree contains zero levels
        if (root == null)
        {
            return 0;
        }

        // Queue holds active frontier nodes of the current level
        var queue = new Queue<TreeNode>();
        queue.Enqueue(root);
        int depth = 0;

        while (queue.Count > 0)
        {
            // Snapshot Gate: Freeze queue count to consume exactly one horizontal level
            int levelSize = queue.Count;
            depth++;

            for (int i = 0; i < levelSize; i++)
            {
                TreeNode current = queue.Dequeue();

                // Frontier Expansion: Only enqueue non-null children
                if (current.left != null)
                {
                    queue.Enqueue(current.left);
                }

                if (current.right != null)
                {
                    queue.Enqueue(current.right);
                }
            }
        }

        return depth;
    }
}
```

---

## 49. Same Tree (LeetCode #100)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟢 Easy |
| **Priority** | ⚪ Reinforcement |
| **Pattern Tags** | `#binary-tree` `#structural-dfs` `#parallel-traversal` `#isomorphism` |
| **LeetCode Link** | [Same Tree](https://leetcode.com/problems/same-tree/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given the roots of two binary trees `p` and `q`, determine if they are structurally identical and have the exact same node values.
- **Key Constraints:**
  - Number of nodes in both trees in $[0, 100]$.
  - Node values in range $[-10^4, 10^4]$.
- **Senior Edge Cases to Defend:**
  - Both trees empty (`p == null && q == null` $\implies \text{true}$).
  - Asymmetric structure (`p == null ^ q == null` $\implies \text{false}$).
  - Matching topologies with mismatched node values (`p.val != q.val` $\implies \text{false}$).
  - Mirrored structures (left/right children transposed $\implies \text{false}$).

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Simultaneous synchronized lockstep traversal over two tree topologies. Early short-circuiting on the first structural or value divergence.
- **Sample 1:**
  - **Input:** `p = [1, 2, 3]`, `q = [1, 2, 3]` $\implies$ `true`
- **Sample 2:**
  - **Input:** `p = [1, 2]`, `q = [1, null, 2]` $\implies$ `false`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)
- **3.1 The Intuitive Spark & Conceptual Metaphor:**
  - *Dual-Robot Synchronized Exploration:* Two robots start at `root(p)` and `root(q)`. At every tick, both robots inspect their current node:
    1. Do both stand in empty space? Match.
    2. Does one stand in empty space while the other stands on a node? Structural breach.
    3. Do the values on their ground match? If not, value breach.
    4. Both step left simultaneously, then both step right simultaneously.
- **3.2 The Naive Bottleneck & Redundant Computation:**
  - Serializing both trees into pre-order strings with null markers and comparing strings allocates $O(N)$ string memory and cannot short-circuit on root mismatch.
  - Direct paired traversal terminates at the exact first point of divergence in $O(1)$ best-case time.
- **3.3 The Breakthrough Insight & Mathematical Invariant:**
  - **Inductive Isomorphism:**
    $$T_1 \cong T_2 \iff (T_1 = \emptyset \land T_2 = \emptyset) \lor \Big( T_1 \neq \emptyset \land T_2 \neq \emptyset \land T_1.val = T_2.val \land (T_1.left \cong T_2.left) \land (T_1.right \cong T_2.right) \Big)$$
  - Short-circuit `&&` ensures that if the left subtrees differ, the right subtrees are never traversed.
- **3.4 Cursor Semantics & Invariant Partition Architecture:**
  ```text
  Paired Dual Pointers: (currP, currQ)
  +---------------------------------+---------------------------------------+
  | Condition                       | Semantic Interpretation & Action      |
  +---------------------------------+---------------------------------------+
  | currP == null && currQ == null  | Identical empty boundaries -> Return true |
  | currP == null || currQ == null  | Structural divergence -> Return false |
  | currP.val != currQ.val          | Value divergence -> Return false      |
  | currP.val == currQ.val          | Matched -> Recurse left AND right     |
  +---------------------------------+---------------------------------------+
  ```
- **3.5 State Transition Triggers & Decision Gates:**
  - Truth-table evaluation sequence: Both null? $\to$ One null? $\to$ Values equal? $\to$ Branch both sides.
- **3.6 Concrete Step-by-Step State Trace:**
  - Input: `p = [1, 2]`, `q = [1, null, 2]`

  | Step | Node Pair `(p, q)` | Nullity Check | Value Check | Outcome / Action |
  | :--- | :--- | :--- | :--- | :--- |
  | 1 | `(1, 1)` | Both non-null | $1 == 1$ (Pass) | Recurse left `(p.left, q.left)` |
  | 2 | `(2, null)` | `p != null, q == null` | N/A | **FAIL**: Structural divergence detected. Return `false` immediately. |

### 4. Approach & Complexity Deconstruction
- **4.1 Anchor Points & Approach Selection Criteria:**
  - **Recursive DFS:** Default choice for readability and immediate short-circuiting.
  - **Iterative BFS (Paired Queue):** Production choice when tree depth is unbounded and stack overflow is a risk.
- **4.2 Step-by-Step Natural Progression Flow:**
  - *Step 1: Setup & Boundaries:* Handle paired null termination.
  - *Step 2: Main Exploration Loop:* Compare current node values.
  - *Step 3: Invariant Maintenance & Condition Gates:* Propagate `IsSameTree` across both left and right branches.
  - *Step 4: Resolution & Return:* Conjunction of left and right boolean results.
- **4.3 Alternative Approaches Analysis:**
  - Iterative DFS with dual stacks: Behaves identically to BFS queue, with LIFO traversal order.
- **4.4 Multi-Dimensional Complexity & Trade-Off Matrix:**

| Approach | Time (Best) | Time (Avg) | Time (Worst) | Aux Space (Avg) | Aux Space (Worst) | Cache Locality |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **Recursive DFS** | $O(1)$ | $O(\min(N, M))$ | $O(\min(N, M))$ | $O(\log(\min(N, M)))$ | $O(\min(N, M))$ | High |
| **Iterative Paired BFS**| $O(1)$ | $O(\min(N, M))$ | $O(\min(N, M))$ | $O(\min(W_1, W_2))$ | $O(\min(N, M))$ | Medium |

### 5. Production C# Implementations

```csharp
// ============================================================================
// ARCHITECTURE ANCHOR: Same Tree
// Primary: Recursive Synchronized DFS (O(min(N, M)) Time, O(min(H1, H2)) Stack)
// Secondary: Iterative Paired Queue BFS (Stack-Safe, Explicit State)
// ============================================================================

public class Solution
{
    /// <summary>
    /// Evaluates topological and value identity using synchronized recursive DFS.
    /// Short-circuits at the first point of divergence.
    /// </summary>
    public bool IsSameTree(TreeNode p, TreeNode q)
    {
        // Gate 1: Both nodes null confirms identical empty boundary
        if (p == null && q == null)
        {
            return true;
        }

        // Gate 2: Exactly one node null confirms structural asymmetry
        if (p == null || q == null)
        {
            return false;
        }

        // Gate 3: Node values must match exactly
        if (p.val != q.val)
        {
            return false;
        }

        // Gate 4: Invariant induction: Both left subtrees AND both right subtrees must match
        return IsSameTree(p.left, q.left) && IsSameTree(p.right, q.right);
    }
}

public class SolutionIterative
{
    /// <summary>
    /// Evaluates tree identity iteratively using a synchronized paired FIFO queue.
    /// Completely immune to call stack overflow.
    /// </summary>
    public bool IsSameTree(TreeNode p, TreeNode q)
    {
        var queue = new Queue<(TreeNode NodeP, TreeNode NodeQ)>();
        queue.Enqueue((p, q));

        while (queue.Count > 0)
        {
            var (nodeP, nodeQ) = queue.Dequeue();

            // Matched leaves: nothing further to explore below this branch
            if (nodeP == null && nodeQ == null)
            {
                continue;
            }

            // Asymmetric presence detected
            if (nodeP == null || nodeQ == null)
            {
                return false;
            }

            // Value divergence detected
            if (nodeP.val != nodeQ.val)
            {
                return false;
            }

            // Enqueue synchronized left children and right children
            queue.Enqueue((nodeP.left, nodeQ.left));
            queue.Enqueue((nodeP.right, nodeQ.right));
        }

        return true;
    }
}
```

---

## 50. Invert Binary Tree (LeetCode #226)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟢 Easy |
| **Priority** | ⚪ Reinforcement |
| **Pattern Tags** | `#binary-tree` `#dfs-postorder` `#bfs-queue` `#pointer-rewiring` |
| **LeetCode Link** | [Invert Binary Tree](https://leetcode.com/problems/invert-binary-tree/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given the `root` of a binary tree, invert the tree (produce its mirror reflection) and return its root.
- **Key Constraints:**
  - Number of nodes in $[0, 100]$.
  - Node values in $[-100, 100]$.
- **Senior Edge Cases to Defend:**
  - Empty tree (`root == null` $\implies \text{null}$).
  - Single node tree $\implies$ return unchanged.
  - Skewed tree (all left children become all right children).

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** At every node in the tree, swap its left child reference with its right child reference, and recursively or iteratively repeat for all subtrees.
- **Sample 1:**
  - **Input:** `root = [4, 2, 7, 1, 3, 6, 9]`
  - **Output:** `[4, 7, 2, 9, 6, 3, 1]`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)
- **3.1 The Intuitive Spark & Conceptual Metaphor:**
  - *Bilateral Joint Reflection:* Imagine each node is a swivel joint connecting two mechanical arms. Inverting the tree simply means visiting every joint and flipping the left and right arms across the vertical midline.
- **3.2 The Naive Bottleneck & Redundant Computation:**
  - Constructing a new tree allocates $O(N)$ extra heap nodes and triggers GC pressure. In-place pointer rewiring transforms the tree with zero heap allocation.
- **3.3 The Breakthrough Insight & Mathematical Invariant:**
  - **Pointer Inversion Invariant:** For any node $u$:
    $$u.left \leftarrow \text{Invert}(u.right_{\text{original}}), \quad u.right \leftarrow \text{Invert}(u.left_{\text{original}})$$
  - Works identically whether executed pre-order (swap then recurse) or post-order (recurse then swap). However, executing purely in-order requires care because swapping before recursing right causes the newly swapped left branch to be visited twice!
- **3.4 Cursor Semantics & Invariant Partition Architecture:**
  ```text
  Bilateral Pointer Swap at Node u:
         (u)                            (u)
        /   \                          /   \
     [Left] [Right]   ==== SWAP ===> [Right] [Left]
       |       |                       |       |
    (Invert) (Invert)               (Invert) (Invert)
  ```
- **3.5 State Transition Triggers & Decision Gates:**
  - Base Gate: `root == null` $\implies$ return `null`.
  - Swap Action: `temp = root.left; root.left = root.right; root.right = temp;`.
- **3.6 Concrete Step-by-Step State Trace:**
  - Input: `[4, 2, 7]`

  | Step | Active Node | Pre-Swap Left / Right | Post-Swap Left / Right | Recursive Status |
  | :--- | :--- | :--- | :--- | :--- |
  | 1 | 4 | Left: 2, Right: 7 | Temp swap: Left $\to$ 7, Right $\to$ 2 | Drill into Left (7) |
  | 2 | 7 | Left: null, Right: null | Left $\to$ null, Right $\to$ null | Return 7 |
  | 3 | 2 | Left: null, Right: null | Left $\to$ null, Right $\to$ null | Return 2 |
  | 4 | 4 | Subtrees inverted | Left: 7, Right: 2 | Complete. Return 4 |

### 4. Approach & Complexity Deconstruction
- **4.1 Anchor Points & Approach Selection Criteria:**
  - **Recursive Post-Order DFS:** Most idiomatic and mathematically elegant.
  - **Iterative BFS Queue:** Recommended if the call stack limit is a concern, performing local pointer swaps level-by-level.
- **4.2 Step-by-Step Natural Progression Flow:**
  - *Step 1:* Base case check for null.
  - *Step 2:* Recursively invert left and right subtrees.
  - *Step 3:* Swap the child pointers of current node.
  - *Step 4:* Return current node.
- **4.3 Alternative Approaches Analysis:**
  - Pre-order traversal swaps pointers first, then recursively inverts `root.left` and `root.right`. Time and space are identical to post-order.
- **4.4 Multi-Dimensional Complexity & Trade-Off Matrix:**

| Approach | Time (Best/Avg/Worst) | Aux Space (Avg) | Aux Space (Worst) | In-Place Mutability |
| :--- | :--- | :--- | :--- | :--- |
| **Recursive DFS** | $O(N)$ | $O(\log N)$ stack | $O(N)$ stack | Yes (Mutates input) |
| **Iterative BFS** | $O(N)$ | $O(W) \approx O(N/2)$ | $O(N/2)$ | Yes (Mutates input) |

### 5. Production C# Implementations

```csharp
// ============================================================================
// ARCHITECTURE ANCHOR: Invert Binary Tree
// Primary: Recursive Bottom-Up DFS (O(N) Time, O(H) Stack)
// Secondary: Iterative Queue BFS (O(N) Time, O(W) Queue)
// Invariant: For every node u, u.left = Invert(oldRight) and u.right = Invert(oldLeft)
// ============================================================================

public class Solution
{
    /// <summary>
    /// Inverts binary tree in-place using recursive post-order DFS.
    /// </summary>
    public TreeNode InvertTree(TreeNode root)
    {
        // Base Gate: Empty subtree requires no transformation
        if (root == null)
        {
            return null;
        }

        // Subtree Inversion: Invert child subtrees before rewiring parent pointers
        TreeNode invertedLeft = InvertTree(root.left);
        TreeNode invertedRight = InvertTree(root.right);

        // Invariant Swap: Cross-wire the inverted child branches
        root.left = invertedRight;
        root.right = invertedLeft;

        return root;
    }
}

public class SolutionBfs
{
    /// <summary>
    /// Inverts binary tree iteratively level-by-level using a FIFO queue.
    /// </summary>
    public TreeNode InvertTree(TreeNode root)
    {
        if (root == null)
        {
            return null;
        }

        var queue = new Queue<TreeNode>();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            TreeNode current = queue.Dequeue();

            // Local In-Place Swap of child references
            TreeNode temp = current.left;
            current.left = current.right;
            current.right = temp;

            // Enqueue non-null children to continue iterative reflection
            if (current.left != null)
            {
                queue.Enqueue(current.left);
            }

            if (current.right != null)
            {
                queue.Enqueue(current.right);
            }
        }

        return root;
    }
}
```

---

## 51. Binary Tree Level Order Traversal (LeetCode #102)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#binary-tree` `#bfs` `#queue-snapshot` `#level-partitioning` |
| **LeetCode Link** | [Binary Tree Level Order Traversal](https://leetcode.com/problems/binary-tree-level-order-traversal/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given the `root` of a binary tree, return the level order traversal of its nodes' values (i.e., from left to right, level by level as a list of lists).
- **Key Constraints:**
  - Number of nodes in $[0, 2000]$.
  - Node values in $[-1000, 1000]$.
- **Senior Edge Cases to Defend:**
  - Empty tree (`root == null` $\implies$ return empty list `[]`, not `[[]]`).
  - Skewed tree (each level contains exactly one element; list of $N$ singleton lists).
  - Unbalanced tree with unequal branch depths.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Breadth-First Search with explicit queue size freezing to demarcate depth horizons.
- **Sample 1:**
  - **Input:** `root = [3, 9, 20, null, null, 15, 7]`
  - **Output:** `[[3], [9, 20], [15, 7]]`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)
- **3.1 The Intuitive Spark & Conceptual Metaphor:**
  - *Generational Staging Area:* Think of a FIFO queue as an airport terminal boarding lounge. Before boarding starts for Flight $d$, we count how many passengers are currently seated (`levelSize = queue.Count`). We process exactly that many passengers onto the plane. Any companions or children they bring are escorted to the seating area for Flight $d+1$.
- **3.2 The Naive Bottleneck & Redundant Computation:**
  - Running $K$ separate depth-targeted DFS traversals (e.g. `PrintLevel(d)`) recalculates ancestor paths repeatedly, taking $O(N \cdot H) = O(N^2)$ time.
  - A single-pass BFS with queue snapshotting achieves guaranteed $O(N)$ linear time.
- **3.3 The Breakthrough Insight & Mathematical Invariant:**
  - **Queue Horizon Freezing:** In a standard queue, nodes of level $d$ and level $d+1$ mix. By capturing `levelSize = queue.Count` at the start of each outer iteration, the inner loop processes exactly generation $d$, while all enqueued children are guaranteed to belong to generation $d+1$.
- **3.4 Cursor Semantics & Invariant Partition Architecture:**
  ```text
  Queue State Transition:
  Iteration Start: [ N_{d,1}, N_{d,2}, ..., N_{d,k} ]  (levelSize = k)
  Inner Loop:      Pop N_{d,i} -> Add to currentLevel
                   Push children of N_{d,i} -> Tail of Queue
  Iteration End:   [ N_{d+1,1}, N_{d+1,2}, ... ] (Ready for next level)
  ```
- **3.5 State Transition Triggers & Decision Gates:**
  - Outer Loop: `while (queue.Count > 0)`.
  - Level Snapshot: `int levelSize = queue.Count;`.
  - Child Gates: `if (node.left != null) queue.Enqueue(node.left);`.
- **3.6 Concrete Step-by-Step State Trace:**
  - Input: `[3, 9, 20, null, null, 15, 7]`

  | Outer Iteration | Queue Snapshot (`levelSize`) | Dequeued Values | Enqueued Children | Appended Level List |
  | :--- | :--- | :--- | :--- | :--- |
  | Iter 1 | 1 (`[3]`) | 3 | 9, 20 | `[3]` |
  | Iter 2 | 2 (`[9, 20]`) | 9, 20 | 15, 7 | `[9, 20]` |
  | Iter 3 | 2 (`[15, 7]`) | 15, 7 | None | `[15, 7]` |
  | Terminate | 0 | None | None | Return `[[3], [9, 20], [15, 7]]` |

### 4. Approach & Complexity Deconstruction
- **4.1 Anchor Points & Approach Selection Criteria:**
  - **Approach 1 (Iterative Queue BFS):** Standard industry paradigm. Highly intuitive, preserves strict left-to-right temporal order naturally.
  - **Approach 2 (Recursive DFS with Level Index):** Uses the result list index `level` to bucket values during DFS. Useful when BFS queue allocation is prohibited.
- **4.2 Step-by-Step Natural Progression Flow:**
  - *Step 1:* Guard against null root. Initialize queue and result list.
  - *Step 2:* While queue is non-empty, snapshot `levelSize`.
  - *Step 3:* Pre-allocate list with capacity `levelSize` to prevent dynamic array re-allocations.
  - *Step 4:* Dequeue `levelSize` nodes, add values, enqueue children, append level list.
- **4.3 Alternative Approaches Analysis:**
  - DFS recursion passing `level`: If `level == result.Count`, append a new empty list. Then `result[level].Add(node.val)`. Traverses left before right to maintain horizontal order.
- **4.4 Multi-Dimensional Complexity & Trade-Off Matrix:**

| Approach | Time (Best/Avg/Worst) | Aux Space (Avg) | Aux Space (Worst) | Output Space | Cache Locality |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Iterative BFS Queue** | $O(N)$ | $O(W) \approx O(N/2)$ | $O(N/2)$ | $O(N)$ | Medium |
| **Recursive DFS with Level** | $O(N)$ | $O(\log N)$ stack | $O(N)$ stack | $O(N)$ | High |

### 5. Production C# Implementations

```csharp
// ============================================================================
// ARCHITECTURE ANCHOR: Binary Tree Level Order Traversal
// Primary: Iterative Queue BFS with Frozen Level Horizon (O(N) Time, O(W) Aux)
// Secondary: Recursive DFS with Level Indexing (O(N) Time, O(H) Stack)
// ============================================================================

public class Solution
{
    /// <summary>
    /// Performs level order traversal using a FIFO queue with frozen level-size horizons.
    /// Pre-allocates inner lists to optimize GC heap allocations.
    /// </summary>
    public IList<IList<int>> LevelOrder(TreeNode root)
    {
        var result = new List<IList<int>>();
        if (root == null)
        {
            return result;
        }

        var queue = new Queue<TreeNode>();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            // Invariant Gate: Snapshot queue count to isolate generation d from d+1
            int levelSize = queue.Count;
            var currentLevel = new List<int>(capacity: levelSize);

            for (int i = 0; i < levelSize; i++)
            {
                TreeNode node = queue.Dequeue();
                currentLevel.Add(node.val);

                // Child Frontier Expansion: Left-to-right order guaranteed
                if (node.left != null)
                {
                    queue.Enqueue(node.left);
                }

                if (node.right != null)
                {
                    queue.Enqueue(node.right);
                }
            }

            result.Add(currentLevel);
        }

        return result;
    }
}

public class SolutionDfs
{
    /// <summary>
    /// Performs level order traversal using recursive pre-order DFS with level indexing.
    /// </summary>
    public IList<IList<int>> LevelOrder(TreeNode root)
    {
        var result = new List<IList<int>>();
        DfsHelper(root, 0, result);
        return result;
    }

    private static void DfsHelper(TreeNode node, int level, List<IList<int>> result)
    {
        if (node == null)
        {
            return;
        }

        // Allocate a new level bucket when visiting this depth for the first time
        if (level == result.Count)
        {
            result.Add(new List<int>());
        }

        result[level].Add(node.val);

        // Recurse left first to ensure left-to-right horizontal ordering
        DfsHelper(node.left, level + 1, result);
        DfsHelper(node.right, level + 1, result);
    }
}
```

---

## 52. Diameter of Binary Tree (LeetCode #543)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟢 Easy |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#binary-tree` `#tree-dp` `#dfs-postorder` `#bottom-up-height` |
| **LeetCode Link** | [Diameter of Binary Tree](https://leetcode.com/problems/diameter-of-binary-tree/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Return the length of the diameter of the tree. The diameter is the length of the longest path between any two nodes in a tree (measured in number of edges). The path does not necessarily need to pass through the root.
- **Key Constraints:**
  - Number of nodes in $[1, 10^4]$.
  - Node values in $[-100, 100]$.
- **Senior Edge Cases to Defend:**
  - Diameter path does NOT pass through root (e.g. concentrated in an unbalanced, deep left branch).
  - Single node tree $\implies$ diameter is $0$ edges (path between a node and itself has 0 edges).
  - Star tree where root connects to two deep leaf paths.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Tree DP with Dual Responsibility: At every node, compute its subtree height $1 + \max(h_L, h_R)$ to return upwards to its parent, while simultaneously testing the candidate diameter passing through itself ($h_L + h_R$) against the global maximum.
- **Sample 1:**
  - **Input:** `root = [1, 2, 3, 4, 5]`
  - **Output:** `3` (path length between nodes 4 and 3: $4 \to 2 \to 1 \to 3$, containing 3 edges).

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)
- **3.1 The Intuitive Spark & Conceptual Metaphor:**
  - *The Inverted Pendulum / Longest Arm Junction:* Every valid path between two nodes in a tree reaches a unique highest point: its lowest common turning ancestor. At that turning point $u$, the path consists of two downward arms: the longest downward branch into the left subtree, and the longest downward branch into the right subtree. The total edge count through $u$ is simply $\text{Height}(u.left) + \text{Height}(u.right)$.
- **3.2 The Naive Bottleneck & Redundant Computation:**
  - A brute-force approach calls `MaxDepth(node.left)` and `MaxDepth(node.right)` at every node, recalculating heights of subtrees over and over $\implies O(N^2)$ time.
  - Post-order DP computes height bottom-up in a single pass $\implies O(N)$ time.
- **3.3 The Breakthrough Insight & Mathematical Invariant:**
  - **Subtree Height Recurrence:**
    $$h(u) = 1 + \max(h(u.left), h(u.right)), \quad \text{with } h(\text{null}) = 0$$
  - **Diameter Candidate at Turning Point $u$:**
    $$\text{Diameter}(u) = h(u.left) + h(u.right)$$
  - **Global Optimal Invariant:**
    $$\text{MaxDiameter} = \max_{u \in T} \Big( h(u.left) + h(u.right) \Big)$$
- **3.4 Cursor Semantics & Invariant Partition Architecture:**
  ```text
                (u)  <--- Turning Point
               /   \
      [h_L edges] [h_R edges]
             /       \
           (L)       (R)

  Path passing through u: h_L + h_R edges
  Branch extending upward to parent: 1 + max(h_L, h_R) edges
  ```
- **3.5 State Transition Triggers & Decision Gates:**
  - Base Gate: `if (node == null) return 0;`
  - Global Max Check: `maxDiameter = Math.Max(maxDiameter, leftH + rightH);`
  - Return to Parent: `return 1 + Math.Max(leftH, rightH);`
- **3.6 Concrete Step-by-Step State Trace:**
  - Input: `[1, 2, 3, 4, 5]`

  | Step | Node | Left Height | Right Height | Turning Diameter ($h_L + h_R$) | Global `maxDiameter` | Returned Height |
  | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
  | 1 | 4 | 0 | 0 | 0 | 0 | 1 |
  | 2 | 5 | 0 | 0 | 0 | 0 | 1 |
  | 3 | 2 | 1 (from 4) | 1 (from 5) | 2 | 2 | 2 |
  | 4 | 3 | 0 | 0 | 0 | 2 | 1 |
  | 5 | 1 | 2 (from 2) | 1 (from 3) | 3 | **3** | 3 |

### 4. Approach & Complexity Deconstruction
- **4.1 Anchor Points & Approach Selection Criteria:**
  - **Approach 1 (Post-Order with `ref int` Max):** High-performance, zero heap allocation, idiomatic in C#.
  - **Approach 2 (Pure Functional Tuple Return):** Returns `(Height, Diameter)` struct/tuple. Completely thread-safe and stateless, ideal in concurrent or functional programming paradigms.
- **4.2 Step-by-Step Natural Progression Flow:**
  - *Step 1:* Initialize `maxDiameter = 0`.
  - *Step 2:* Run post-order traversal on root.
  - *Step 3:* At each node, retrieve left and right heights, update `maxDiameter`, return height.
  - *Step 4:* Return accumulated `maxDiameter`.
- **4.3 Alternative Approaches Analysis:**
  - Pure functional approach avoids mutable outer variables by returning `(int Height, int MaxDiameter)` records.
- **4.4 Multi-Dimensional Complexity & Trade-Off Matrix:**

| Approach | Time (Best/Avg/Worst) | Aux Space (Avg) | Aux Space (Worst) | Thread-Safety | Cache Locality |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Post-Order with Ref** | $O(N)$ | $O(\log N)$ stack | $O(N)$ stack | Requires local state | High |
| **Functional Tuple Return** | $O(N)$ | $O(\log N)$ stack | $O(N)$ stack | Fully Thread-Safe | High |

### 5. Production C# Implementations

```csharp
// ============================================================================
// ARCHITECTURE ANCHOR: Diameter of Binary Tree
// Primary: Post-Order DFS with Ref Global Max (O(N) Time, O(H) Stack)
// Secondary: Pure Functional Tuple Return (Thread-Safe, O(N) Time, O(H) Stack)
// Invariant: Diameter at u = Height(left) + Height(right)
// ============================================================================

public class Solution
{
    /// <summary>
    /// Computes tree diameter in a single post-order pass using a ref parameter.
    /// Avoids separate redundant height recalculations.
    /// </summary>
    public int DiameterOfBinaryTree(TreeNode root)
    {
        int maxDiameter = 0;
        ComputeHeight(root, ref maxDiameter);
        return maxDiameter;
    }

    private static int ComputeHeight(TreeNode node, ref int maxDiameter)
    {
        // Base Gate: Null leaves have zero height and contribute 0 edges
        if (node == null)
        {
            return 0;
        }

        // Post-Order Subtree Traversal: Bottom-up height calculation
        int leftHeight = ComputeHeight(node.left, ref maxDiameter);
        int rightHeight = ComputeHeight(node.right, ref maxDiameter);

        // Mathematical Invariant: Update global diameter if path turning at this node exceeds record
        maxDiameter = Math.Max(maxDiameter, leftHeight + rightHeight);

        // Return upward: Height of current subtree is 1 plus deepest branch
        return 1 + Math.Max(leftHeight, rightHeight);
    }
}

public class SolutionFunctional
{
    /// <summary>
    /// Pure functional formulation returning immutable value-tuples.
    /// Eliminates mutable reference state for full thread-safety.
    /// </summary>
    public int DiameterOfBinaryTree(TreeNode root)
    {
        return PostOrder(root).Diameter;
    }

    private static (int Height, int Diameter) PostOrder(TreeNode node)
    {
        if (node == null)
        {
            return (0, 0);
        }

        var (leftH, leftD) = PostOrder(node.left);
        var (rightH, rightD) = PostOrder(node.right);

        int height = 1 + Math.Max(leftH, rightH);
        int diameterThroughCurrent = leftH + rightH;
        int maxDiameter = Math.Max(diameterThroughCurrent, Math.Max(leftD, rightD));

        return (height, maxDiameter);
    }
}
```

---

## 53. Validate Binary Search Tree (LeetCode #98)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#bst` `#dfs-range` `#inorder-traversal` `#bst-invariant` |
| **LeetCode Link** | [Validate Binary Search Tree](https://leetcode.com/problems/validate-binary-search-tree/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given the `root` of a binary tree, determine if it is a valid binary search tree (BST).
  - The left subtree of a node contains only nodes with keys strictly less than the node's key.
  - The right subtree of a node contains only nodes with keys strictly greater than the node's key.
  - Both left and right subtrees must also be valid binary search trees.
- **Key Constraints:**
  - Number of nodes in $[1, 10^4]$.
  - Node values in range $[-2^{31}, 2^{31} - 1]$ (`int.MinValue` to `int.MaxValue`).
- **Senior Edge Cases to Defend:**
  - Nodes with values equal to `int.MinValue` or `int.MaxValue`. Initializing bounds to `int.MinValue` or `int.MaxValue` leads to false negatives. Must widen bounds to 64-bit `long` or use nullable `int?`.
  - Duplicate keys (`node.val == child.val` is invalid in strict BST definition).
  - Ancestor violations deep in subtrees (e.g. a node in the right subtree of the root whose value is smaller than the root).

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Either propagate permissible open intervals $(low, high)$ top-down, or verify that an in-order traversal produces a strictly monotonically increasing sequence.
- **Sample 1:**
  - **Input:** `root = [2, 1, 3]` $\implies$ `true`
- **Sample 2:**
  - **Input:** `root = [5, 1, 4, null, null, 3, 6]` $\implies$ `false` (node 4 is in the right subtree of 5, but $4 < 5$).

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)
- **3.1 The Intuitive Spark & Conceptual Metaphor:**
  - *The Narrowing Corridor:* Imagine walking down a tunnel where the left and right walls close in on you. At the root, the walls are at $(-\infty, +\infty)$. When you take a left turn at a node $u$, the right wall snaps inward to $u.val$. When you take a right turn at node $v$, the left wall snaps inward to $v.val$. Every node must fit comfortably inside its corridor: $low < node.val < high$.
- **3.2 The Naive Bottleneck & Redundant Computation:**
  - Checking only local child relationships ($node.left.val < node.val < node.right.val$) is a classic trap: it fails to catch global ancestor violations (e.g. $[5, 4, 6, null, null, 3, 7]$ where 3 is in 5's right subtree).
  - Computing subtree max and min for every node takes $O(N^2)$ unless done bottom-up or via top-down range restriction.
- **3.3 The Breakthrough Insight & Mathematical Invariant:**
  - **Top-Down Range Invariant:** For node $u$ with valid interval $(L, R)$:
    $$u.val \in (L, R) \implies u.left \in (L, u.val) \quad \land \quad u.right \in (u.val, R)$$
  - **In-Order Monotonicity Invariant:**
    $$\text{InOrder}(T) = [v_1, v_2, \dots, v_N] \implies v_1 < v_2 < \dots < v_N \quad (\text{strictly increasing})$$
- **3.4 Cursor Semantics & Invariant Partition Architecture:**
  ```text
  Corridor Narrowing Tree:
                 (5)   Bounds: (-inf, +inf)
                /   \
  Bounds: (-inf, 5)  Bounds: (5, +inf)
        (1)                 (4) <--- VIOLATION: 4 <= 5 (Outside valid corridor)
  ```
- **3.5 State Transition Triggers & Decision Gates:**
  - Value Gate: `if (node.val <= minBound || node.val >= maxBound) return false;`
  - Left Recurse: `Validate(node.left, minBound, node.val)`
  - Right Recurse: `Validate(node.right, node.val, maxBound)`
- **3.6 Concrete Step-by-Step State Trace:**
  - Input: `[5, 1, 4, null, null, 3, 6]`

  | Step | Node | Permissible Range `(minBound, maxBound)` | In-Range Check | Decision / Next Action |
  | :--- | :--- | :--- | :--- | :--- |
  | 1 | 5 | $(-\infty, +\infty)$ | $-\infty < 5 < +\infty$ (Pass) | Recurse left with $(-\infty, 5)$ |
  | 2 | 1 | $(-\infty, 5)$ | $-\infty < 1 < 5$ (Pass) | Left/Right null $\implies$ True |
  | 3 | 4 | $(5, +\infty)$ | $4 \le 5$ (**FAIL**) | **VIOLATION**: Node 4 violates lower bound 5. Return `false` immediately. |

### 4. Approach & Complexity Deconstruction
- **4.1 Anchor Points & Approach Selection Criteria:**
  - **Approach 1 (Top-Down Range DFS with `long`):** Extremely fast, prunes invalid subtrees high up in the tree. Defends against `int.MinValue` using 64-bit integer widening.
  - **Approach 2 (Iterative In-Order with `prev` pointer):** Avoids 64-bit integer widening completely; works purely on node-to-node comparison `prev.val < curr.val`. Supports early termination.
- **4.2 Step-by-Step Natural Progression Flow:**
  - *Step 1:* Set initial bounds to `(long.MinValue, long.MaxValue)`.
  - *Step 2:* Validate current node against open interval bounds.
  - *Step 3:* Tighten bounds when branching left and right.
  - *Step 4:* Return conjunction of recursive calls.
- **4.3 Alternative Approaches Analysis:**
  - Iterative in-order using explicit stack tracks `TreeNode prev`. Emits `false` the moment `prev != null && curr.val <= prev.val`.
- **4.4 Multi-Dimensional Complexity & Trade-Off Matrix:**

| Approach | Time (Best) | Time (Avg/Worst) | Aux Space (Avg) | Aux Space (Worst) | Integer Overflow Risk |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Top-Down Range DFS (long)** | $O(1)$ | $O(N)$ | $O(\log N)$ stack | $O(N)$ stack | None (widened to 64-bit) |
| **Iterative In-Order Stack** | $O(1)$ | $O(N)$ | $O(\log N)$ stack | $O(N)$ stack | None (direct comparison) |

### 5. Production C# Implementations

```csharp
// ============================================================================
// ARCHITECTURE ANCHOR: Validate Binary Search Tree
// Primary: Top-Down Range Inheritance (O(N) Time, O(H) Stack, 64-bit bounds)
// Secondary: Iterative In-Order Traversal (O(N) Time, O(H) Stack, Zero Widening)
// Invariant: u.val in (low, high); left in (low, u.val); right in (u.val, high)
// ============================================================================

public class Solution
{
    /// <summary>
    /// Validates BST using top-down open range propagation.
    /// Employs 64-bit long bounds to defend against int.MinValue / int.MaxValue edge cases.
    /// </summary>
    public bool IsValidBST(TreeNode root)
    {
        return Validate(root, long.MinValue, long.MaxValue);
    }

    private static bool Validate(TreeNode node, long minBound, long maxBound)
    {
        // Base Gate: Empty subtree is unconditionally valid
        if (node == null)
        {
            return true;
        }

        // Range Invariant Gate: Node value must strictly reside inside open interval (minBound, maxBound)
        if (node.val <= minBound || node.val >= maxBound)
        {
            return false;
        }

        // Subtree Range Tightening:
        // Left child bounded from above by current node value
        // Right child bounded from below by current node value
        return Validate(node.left, minBound, node.val) &&
               Validate(node.right, node.val, maxBound);
    }
}

public class SolutionInOrder
{
    /// <summary>
    /// Validates BST by asserting strictly increasing order during iterative in-order walk.
    /// Completely avoids integer widening.
    /// </summary>
    public bool IsValidBST(TreeNode root)
    {
        var stack = new Stack<TreeNode>();
        TreeNode current = root;
        TreeNode prev = null;

        while (current != null || stack.Count > 0)
        {
            // Drill down to leftmost unvisited descendant
            while (current != null)
            {
                stack.Push(current);
                current = current.left;
            }

            current = stack.Pop();

            // Monotonicity Invariant: In-order traversal of a strict BST must be strictly increasing
            if (prev != null && current.val <= prev.val)
            {
                return false;
            }

            prev = current;
            current = current.right;
        }

        return true;
    }
}
```

---

## 54. Lowest Common Ancestor of a Binary Tree (LeetCode #236)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#binary-tree` `#dfs-postorder` `#lca` `#ancestor-search` |
| **LeetCode Link** | [Lowest Common Ancestor of a Binary Tree](https://leetcode.com/problems/lowest-common-ancestor-of-a-binary-tree/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given a binary tree, find the lowest common ancestor (LCA) of two given nodes `p` and `q`. The LCA is defined between two nodes $p$ and $q$ as the lowest node $T$ that has both $p$ and $q$ as descendants (where a node can be a descendant of itself).
- **Key Constraints:**
  - Number of nodes in $[2, 10^5]$.
  - All `Node.val` are unique.
  - $p \neq q$, and both $p$ and $q$ are guaranteed to exist in the tree.
- **Senior Edge Cases to Defend:**
  - Direct ancestor-descendant relationship ($p$ is the direct parent or ancestor of $q$; LCA is $p$).
  - Target nodes reside in opposite subtrees of the root (LCA is `root`).
  - Skewed tree where targets are at the bottom of a deep linked-list chain.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Bottom-up convergence: When a node's left recursion returns non-null and right recursion returns non-null, that node is the unique fork point (LCA).
- **Sample 1:**
  - **Input:** `root = [3, 5, 1, 6, 2, 0, 8, null, null, 7, 4]`, `p = 5`, `q = 1`
  - **Output:** `3`
- **Sample 2:**
  - **Input:** `root = [3, 5, 1, 6, 2, 0, 8, null, null, 7, 4]`, `p = 5`, `q = 4`
  - **Output:** `5` (since node 5 is an ancestor of 4).

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)
- **3.1 The Intuitive Spark & Conceptual Metaphor:**
  - *Rescue Flares at River Confluence:* Two search targets $p$ and $q$ launch distress flares upward. The signals travel up the branches toward the root.
    - If a junction receives a flare from its left branch AND a flare from its right branch, it is the lowest confluence where both paths converge $\implies$ It is the LCA!
    - If a junction receives only one flare, it forwards that flare upward to its parent.
    - If a node is itself $p$ or $q$, it starts the flare signal immediately and returns itself upward.
- **3.2 The Naive Bottleneck & Redundant Computation:**
  - Finding paths from root to $p$ and root to $q$, storing them in lists, and finding the last common element requires $O(N)$ extra memory for path storage and multiple traversals.
  - Post-order propagation requires zero auxiliary collections and identifies the LCA in a single pass.
- **3.3 The Breakthrough Insight & Mathematical Invariant:**
  - **Post-Order Return Semantics:**
    $$\text{LCA}(u, p, q) = \begin{cases} u & \text{if } u \in \{p, q, \text{null}\} \\ u & \text{if } \text{LCA}(u.left) \neq \text{null} \land \text{LCA}(u.right) \neq \text{null} \\ \text{LCA}(u.left) & \text{if } \text{LCA}(u.right) = \text{null} \\ \text{LCA}(u.right) & \text{if } \text{LCA}(u.left) = \text{null} \end{cases}$$
  - **Why Discarding Further Search Below $p$ or $q$ is Safe:** If we hit $p$ first, we do not need to search below $p$. Why? If $q$ is in $p$'s subtree, $p$ is the LCA; returning $p$ upward is correct. If $q$ is NOT in $p$'s subtree, $p$ must bubble up to meet $q$'s signal at their true common ancestor; returning $p$ upward is still correct!
- **3.4 Cursor Semantics & Invariant Partition Architecture:**
  ```text
               (LCA Candidate: u)
                 /            \
        [Signal: Found p]   [Signal: Found q]
        left != null   AND   right != null  ===> u is the LCA!

  Single Signal Case:
               (Parent)
                 /    \
        [Signal: p]   [null]  ===> Parent bubbles p upward
  ```
- **3.5 State Transition Triggers & Decision Gates:**
  - Target Match Gate: `if (root == null || root == p || root == q) return root;`
  - Fork Confluence Gate: `if (left != null && right != null) return root;`
  - Bubble Gate: `return left ?? right;`
- **3.6 Concrete Step-by-Step State Trace:**
  - Input: `root = [3, 5, 1, 6, 2, 0, 8]`, `p = 5`, `q = 1`

  | Step | Node | Event / Condition | Returned Value Upward |
  | :--- | :--- | :--- | :--- |
  | 1 | 5 | Matches target $p$ | Return node `5` |
  | 2 | 1 | Matches target $q$ | Return node `1` |
  | 3 | 3 | `left = 5`, `right = 1` (Both non-null!) | Fork detected! Return node `3` (LCA) |

### 4. Approach & Complexity Deconstruction
- **4.1 Anchor Points & Approach Selection Criteria:**
  - **Approach 1 (Post-Order Recursive DFS):** The gold standard for LCA in arbitrary binary trees. $O(N)$ time, $O(H)$ stack, zero heap allocation.
  - **Approach 2 (Parent Map with Ancestor HashSet):** Used when nodes have `parent` pointers or when multiple online LCA queries must be answered over a static tree.
- **4.2 Step-by-Step Natural Progression Flow:**
  - *Step 1:* Base case: return root if null or matches $p$ or $q$.
  - *Step 2:* Recurse on left and right subtrees.
  - *Step 3:* If both branches return non-null, return root.
  - *Step 4:* Otherwise return the non-null branch.
- **4.3 Alternative Approaches Analysis:**
  - Parent pointer dictionary: Traverse with BFS/DFS recording parent pointers until both $p$ and $q$ are visited. Walk $p$ up to root populating `HashSet<TreeNode>`. Walk $q$ up until hitting the first node in the set.
- **4.4 Multi-Dimensional Complexity & Trade-Off Matrix:**

| Approach | Time (Best) | Time (Avg/Worst) | Aux Space (Avg) | Aux Space (Worst) | Allocations |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Recursive DFS** | $O(1)$ | $O(N)$ | $O(\log N)$ stack | $O(N)$ stack | Zero heap |
| **Parent Pointer Map**| $O(\text{depth})$ | $O(N)$ | $O(N)$ heap | $O(N)$ heap | Dictionary + HashSet |

### 5. Production C# Implementations

```csharp
// ============================================================================
// ARCHITECTURE ANCHOR: Lowest Common Ancestor of a Binary Tree
// Primary: Post-Order Recursive DFS (O(N) Time, O(H) Stack, Zero Allocations)
// Secondary: Parent Pointer Map + Ancestor HashSet (O(N) Time, O(N) Space)
// Invariant: If left != null && right != null, current node is the LCA confluence
// ============================================================================

public class Solution
{
    /// <summary>
    /// Finds LCA in a binary tree using bottom-up post-order signal propagation.
    /// </summary>
    public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
    {
        // Base Gate: Null reached OR current node matches one of the search targets
        if (root == null || root == p || root == q)
        {
            return root;
        }

        // Subtree Exploration: Search both left and right territories
        TreeNode left = LowestCommonAncestor(root.left, p, q);
        TreeNode right = LowestCommonAncestor(root.right, p, q);

        // Fork Confluence Gate: If both branches returned non-null, this node is the LCA
        if (left != null && right != null)
        {
            return root;
        }

        // Bubble Gate: Forward whichever target signal was discovered below
        return left ?? right;
    }
}

public class SolutionParentMap
{
    /// <summary>
    /// Finds LCA using parent pointers and an ancestor hash set.
    /// Ideal for scenarios with persistent trees or explicit parent pointers.
    /// </summary>
    public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
    {
        var parentMap = new Dictionary<TreeNode, TreeNode>();
        var stack = new Stack<TreeNode>();

        parentMap[root] = null;
        stack.Push(root);

        // Traverse until both p and q have their lineage captured
        while (!parentMap.ContainsKey(p) || !parentMap.ContainsKey(q))
        {
            TreeNode node = stack.Pop();

            if (node.left != null)
            {
                parentMap[node.left] = node;
                stack.Push(node.left);
            }

            if (node.right != null)
            {
                parentMap[node.right] = node;
                stack.Push(node.right);
            }
        }

        // Collect all ancestors of p into a hash set
        var ancestors = new HashSet<TreeNode>();
        TreeNode currP = p;
        while (currP != null)
        {
            ancestors.Add(currP);
            currP = parentMap[currP];
        }

        // Ascend from q; first intersection with p's ancestor set is the LCA
        TreeNode currQ = q;
        while (!ancestors.Contains(currQ))
        {
            currQ = parentMap[currQ];
        }

        return currQ;
    }
}
```

---

## 55. Kth Smallest Element in a BST (LeetCode #230)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | 🔥 High |
| **Pattern Tags** | `#bst` `#inorder-traversal` `#stack` `#early-stopping` `#morris-traversal` |
| **LeetCode Link** | [Kth Smallest Element in a BST](https://leetcode.com/problems/kth-smallest-element-in-a-bst/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given the `root` of a binary search tree, and an integer `k`, return the $k$-th smallest value (1-indexed) of all node values in the tree.
- **Key Constraints:**
  - Number of nodes in range $[1, 10^4]$.
  - $1 \le k \le n \le 10^4$.
  - Node values in range $[0, 10^4]$.
- **Senior Edge Cases to Defend:**
  - $k = 1$ (minimum element in BST $\implies$ leftmost descendant; should stop immediately without visiting remainder of tree).
  - $k = N$ (maximum element $\implies$ rightmost leaf).
  - Completely unbalanced linear tree (height $H = N$).
  - Frequent follow-up: Tree is modified often and $k$-th smallest queries are frequent $\implies$ Augment tree nodes with `subtreeSize` for $O(H)$ order-statistic lookups.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** In-order traversal of a BST visits nodes in strictly increasing order. Simulate in-order traversal using an explicit stack and terminate the exact instant the $k$-th node is popped.
- **Sample 1:**
  - **Input:** `root = [3, 1, 4, null, 2]`, `k = 1`
  - **Output:** `1`
- **Sample 2:**
  - **Input:** `root = [5, 3, 6, 2, 4, null, null, 1]`, `k = 3`
  - **Output:** `3`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)
- **3.1 The Intuitive Spark & Conceptual Metaphor:**
  - *The Sorted Conveyor Belt:* An in-order traversal turns a BST into a sorted conveyor belt rolling elements off one by one in ascending order. Instead of letting all $N$ items roll off into an array, stand beside the belt with a counter initialized to $k$. As each element rolls past, decrement $k$. When $k$ reaches 0, snatch the element and shut down the factory.
- **3.2 The Naive Bottleneck & Redundant Computation:**
  - Traversing the entire tree to collect all $N$ nodes into a list and returning `list[k - 1]` wastes $O(N)$ memory and spends $O(N)$ time even when $k = 1$.
  - An iterative stack traversal visits only $H + k$ nodes, terminating in $O(H + k)$ time.
- **3.3 The Breakthrough Insight & Mathematical Invariant:**
  - **Left-Spine Invariant:** Pushing all left descendants onto a stack maintains the invariant that the top of the stack is always the global minimum among all unexplored nodes in the tree.
  - **Early Termination Invariant:**
    $$\text{VisitCount} = k \implies \text{Current Node} = \text{k-th Smallest Element}$$
- **3.4 Cursor Semantics & Invariant Partition Architecture:**
  ```text
  In-Order Stack Progression:
  Left Spine Stack: [ Root, Left_1, Left_2, ..., Leftmost ]  <-- Top is current minimum
  Pop Top: Decrement k.
  If k == 0: Return val!
  Else: Move to popped.right, then drill down its left spine.
  ```
- **3.5 State Transition Triggers & Decision Gates:**
  - Drill Gate: `while (current != null) { stack.Push(current); current = current.left; }`
  - Pop & Decrement Gate: `current = stack.Pop(); k--;`
  - Early Stop Gate: `if (k == 0) return current.val;`
  - Advance Gate: `current = current.right;`
- **3.6 Concrete Step-by-Step State Trace:**
  - Input: `root = [3, 1, 4, null, 2]`, `k = 1`

  | Step | `current` | Stack Contents | Action | `k` Remaining | Output |
  | :--- | :--- | :--- | :--- | :--- | :--- |
  | 1 | 3 | `[3]` | Push 3, drill left | 1 | Pending |
  | 2 | 1 | `[3, 1]` | Push 1, drill left (hits null) | 1 | Pending |
  | 3 | Pop `1` | `[3]` | Decrement $k \implies 0$ | 0 | **Return 1 immediately** |

### 4. Approach & Complexity Deconstruction
- **4.1 Anchor Points & Approach Selection Criteria:**
  - **Approach 1 (Iterative Stack with Early Stopping):** Optimal standard approach. Halts after visiting $O(H + k)$ nodes; auxiliary space $O(H)$.
  - **Approach 2 (Morris In-Order Traversal):** Uses temporary threaded pointers to achieve $O(1)$ auxiliary space. Temporarily mutates tree, but restores original structure before completing.
- **4.2 Step-by-Step Natural Progression Flow:**
  - *Step 1:* Initialize empty stack and set `current = root`.
  - *Step 2:* Drill left down the left spine, pushing nodes.
  - *Step 3:* Pop node, decrement $k$. If $k == 0$, return value.
  - *Step 4:* Move to right child and resume.
- **4.3 Alternative Approaches Analysis:**
  - Morris Traversal finds the in-order predecessor of `current`. If `pred.right == null`, creates a temporary thread `pred.right = current` and moves left. If thread exists, removes thread, visits `current`, and moves right.
- **4.4 Multi-Dimensional Complexity & Trade-Off Matrix:**

| Approach | Time (Best) | Time (Avg) | Time (Worst) | Aux Space | Tree Mutability |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Iterative Stack DFS** | $O(H)$ (when $k=1$) | $O(H + k)$ | $O(N)$ | $O(H)$ | Read-Only |
| **Morris Traversal** | $O(N)$ | $O(N)$ | $O(N)$ | $O(1)$ | Temporary mutation |

### 5. Production C# Implementations

```csharp
// ============================================================================
// ARCHITECTURE ANCHOR: Kth Smallest Element in a BST
// Primary: Iterative Stack with Early Stopping (O(H + k) Time, O(H) Aux Stack)
// Secondary: Morris In-Order Traversal (O(N) Time, O(1) Auxiliary Space)
// Invariant: In-order traversal produces strictly ascending values; stop at k == 0
// ============================================================================

public class Solution
{
    /// <summary>
    /// Finds k-th smallest element using iterative in-order stack traversal.
    /// Halts execution the exact instant the k-th node is popped.
    /// </summary>
    public int KthSmallest(TreeNode root, int k)
    {
        var stack = new Stack<TreeNode>();
        TreeNode current = root;

        // Invariant: In-order traversal visits nodes in ascending order
        while (current != null || stack.Count > 0)
        {
            // Drill Gate: Push left spine nodes onto the stack
            while (current != null)
            {
                stack.Push(current);
                current = current.left;
            }

            // Consumption Gate: Pop the current global minimum of unexplored nodes
            current = stack.Pop();
            k--;

            // Decision Gate: Early stopping when k-th smallest element is reached
            if (k == 0)
            {
                return current.val;
            }

            // Exploration Gate: Move into the right subtree
            current = current.right;
        }

        throw new ArgumentException("k exceeds total node count in BST.");
    }
}

public class SolutionMorris
{
    /// <summary>
    /// Finds k-th smallest element in O(1) auxiliary space using Morris Threaded Traversal.
    /// Restores all modified tree pointers before returning.
    /// </summary>
    public int KthSmallest(TreeNode root, int k)
    {
        TreeNode current = root;
        int result = -1;

        while (current != null)
        {
            if (current.left == null)
            {
                // Visit Gate: No left child means current node is the next in-order element
                k--;
                if (k == 0)
                {
                    result = current.val;
                }

                current = current.right;
            }
            else
            {
                // Find in-order predecessor (rightmost node in left subtree)
                TreeNode predecessor = current.left;
                while (predecessor.right != null && predecessor.right != current)
                {
                    predecessor = predecessor.right;
                }

                if (predecessor.right == null)
                {
                    // Establish temporary back-link thread
                    predecessor.right = current;
                    current = current.left;
                }
                else
                {
                    // Thread already exists: Remove thread and visit current node
                    predecessor.right = null;
                    k--;
                    if (k == 0)
                    {
                        result = current.val;
                    }

                    current = current.right;
                }
            }
        }

        return result;
    }
}
```
