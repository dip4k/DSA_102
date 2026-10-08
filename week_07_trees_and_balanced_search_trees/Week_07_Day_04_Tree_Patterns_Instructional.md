# 📘 WEEK 7 DAY 4: Tree Patterns — Path Sum, Diameter, LCA & Serialization — Engineering Guide

> 🧭 **Navigation:** [← Previous Day](Week_07_Day_03_Balanced_BSTs_AVL_And_RedBlack_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_07_Day_05_Augmented_BSTs_OrderStatistics_Instructional.md)
> 
> 💡 **Instructor Note:** *Not all sections or topics are mandatory. Feel free to adapt your pace and skim or skip sections based on your current focus and interview timeline.*

---

## 🎯 LEARNING OBJECTIVES

*By the end of this chapter, you will be able to:*

- 🎯 **Internalize** the recursive tree decomposition pattern: decomposing problems into independent left and right subtree subproblems, then aggregating results at the parent.
- ⚙️ **Implement** four canonical tree patterns—Tree Diameter, Path Sum with backtracking, Lowest Common Ancestor (LCA), and Tree Serialization/Deserialization—in both C# and Python.
- ⚖️ **Evaluate** trade-offs between bottom-up postorder DFS (computing attributes from children up) and top-down DFS with state backtracking.
- 🏭 **Connect** tree patterns to production systems: distributed RPC tree marshalling, `git merge-base` calculation, and network packet propagation limits.

---

## 📖 CHAPTER 1: CONTEXT & MOTIVATION

### The Engineering Challenge

In production software, hierarchical data structures rarely just store items for lookup. They answer complex structural questions:
- **Network Optimization:** In an unweighted peer-to-peer network topology, what is the maximum latency between any two client machines? (This is the **Tree Diameter** problem).
- **Git Version Control:** When merging branch `feature` into branch `main`, git must locate the exact commit where the branches originally split to generate a three-way diff (`git merge-base`). (This is the **Lowest Common Ancestor** problem).
- **Financial Compliance:** In a multi-tiered corporate ownership hierarchy, we must find all reporting paths whose cumulative risk exposure equals a regulatory threshold. (This is the **Path Sum** problem).
- **Microservice RPC & Caching:** To transmit an in-memory syntax tree or configuration hierarchy over HTTP or persist it in Redis, we must encode pointer relationships into a linear byte stream and reconstruct it identically. (This is **Serialization & Deserialization**).

These diverse domains reduce to four foundational algorithmic tree patterns.

### The Solution: Recursive Subtree Decomposition

Because a binary tree is recursively self-similar (each subtree is itself a complete binary tree), any global question can be decomposed into:
1. **Base Case:** The trivial answer for `null` or a single leaf.
2. **Subproblem Concurrency:** Query the left child and right child independently.
3. **Parent Synthesis:** Combine child results with the current node's value.

> [!TIP]
> **Core Insight:** Tree problems divide into two archetypes:
> - **Bottom-Up Postorder:** Children compute their local answers and return them up to the parent (e.g., Maximum Depth, Diameter, LCA).
> - **Top-Down Preorder with Backtracking:** Parents pass state downward into children and undo mutations upon backtracking (e.g., Path Sum).

---

## 🧠 CHAPTER 2: BUILDING THE MENTAL MODEL

### The Core Analogies & ASCII Layouts

#### 1. Tree Diameter: The Longest Journey with a Turning Point
The diameter is the longest path between **any two nodes** in the tree. It does not need to pass through the root!
Every path in a tree has a unique **turning point** (the highest ancestor on the path). At that turning point node `X`:
```
Longest Path through X = Height(X.left) + Height(X.right) edges
```

```
           [ 1 ]
          /     \
       [ 2 ]   [ 3 ]
      /     \
   [ 4 ]   [ 5 ]          <-- Path [4 -> 2 -> 5] has length 2
  /           \
[ 8 ]         [ 9 ]       <-- Path [8 -> 4 -> 2 -> 5 -> 9] has length 4!
                              Turning point is Node 2, NOT Root (1).
```

#### 2. Lowest Common Ancestor (LCA): The Fork in the Road
The Lowest Common Ancestor of nodes `p` and `q` is the deepest node that has both `p` and `q` as descendants.
```
           [ Root ]
          /        \
       [ LCA ]     ...
      /       \
   [ p ]     [ q ]        <-- Paths diverge here!
```

#### 3. Tree Serialization: Linear Flattening with Explicit Nulls
Pointers cannot be saved to disk. To preserve structure, we traverse preorder and record an explicit sentinel (e.g., `"#"`) for every missing child:

```
      1
     / \          Preorder with Nulls:
    2   3   ===>  "1,2,#,#,3,4,#,#,5,#,#"
       / \
      4   5
```

---

## ⚙️ CHAPTER 3: MECHANICS & PATTERN STATE MACHINES

### 🔧 Pattern 1: Tree Diameter (Postorder Depth DP)
- **Mechanics:** While computing the standard tree depth via postorder DFS (`1 + max(leftH, rightH)`), calculate the diameter through the current node as `leftH + rightH`. Maintain a running maximum.
- **Why it works:** Every candidate path has exactly one highest node. By testing every node as a potential turning point, the global maximum is guaranteed to be discovered in a single `O(N)` pass.

### 🔧 Pattern 2: Path Sum with Backtracking
- **Mechanics:** Maintain an active path buffer `List<int>`. When visiting a node:
  1. Add `node.val` to path and subtract from `targetSum`.
  2. If node is a leaf and `remainingSum == 0`, clone path into result.
  3. Recurse into children.
  4. **Backtrack:** Remove `node.val` from path (`path.RemoveAt(path.Count - 1)`) so siblings do not see contaminated state.

### 🔧 Pattern 3: Lowest Common Ancestor (Postorder Split)
- **Base Case:** If current node is `null`, `p`, or `q`, return current node.
- **Recursive Step:** Search left and right subtrees.
- **Decision:**
  - If both left and right return non-null, `p` and `q` reside in separate subtrees -> current node is the LCA!
  - If only one side returns non-null, both targets reside in that subtree -> propagate non-null result upward.

---

## 💻 CHAPTER 4: PRODUCTION-GRADE IMPLEMENTATIONS (C# & PYTHON)

### Problem 1: Diameter of Binary Tree (LeetCode 543)

#### 🎙️ 45-Minute Interview Talk Track
> *"The diameter of a binary tree is the length of the longest path between any two nodes, measured in edges. This path may or may not pass through the root. At each node, the longest path that uses this node as its highest turning point is the height of its left subtree plus the height of its right subtree. We use a postorder DFS that computes the height of each subtree bottom-up. As each node computes its left and right heights, it updates a global maximum diameter before returning its own height `1 + max(left, right)` to its parent. This evaluates all possible turning points in a single O(N) pass with O(H) auxiliary space."*

#### C# Primary Implementation (.NET 8/9 — Production-Grade)
```csharp
public static class TreeDiameterSolver
{
    /// <summary>
    /// Computes the diameter (longest edge path) of a binary tree.
    /// Time Complexity: O(N) | Auxiliary Space: O(H)
    /// </summary>
    public static int DiameterOfBinaryTree(TreeNode? root)
    {
        int maxDiameter = 0;
        ComputeHeight(root, ref maxDiameter);
        return maxDiameter;

        static int ComputeHeight(TreeNode? node, ref int maxDiameter)
        {
            if (node is null) return 0;

            int leftHeight = ComputeHeight(node.left, ref maxDiameter);
            int rightHeight = ComputeHeight(node.right, ref maxDiameter);

            // Path through current node as turning point
            maxDiameter = Math.Max(maxDiameter, leftHeight + rightHeight);

            // Return height of current subtree to caller
            return 1 + Math.Max(leftHeight, rightHeight);
        }
    }
}
```

#### Python Secondary Implementation (3.11+ — Idiomatic)
```python
def diameter_of_binary_tree(root: Optional[TreeNode]) -> int:
    """Computes the diameter of a binary tree in O(N) time.
    
    Time Complexity: O(N) | Auxiliary Space: O(H)
    """
    max_diameter = 0

    def get_height(node: Optional[TreeNode]) -> int:
        nonlocal max_diameter
        if not node:
            return 0

        left_h = get_height(node.left)
        right_h = get_height(node.right)

        max_diameter = max(max_diameter, left_h + right_h)
        return 1 + max(left_h, right_h)

    get_height(root)
    return max_diameter
```

#### 📊 Explicit Complexity Deconstruction
- **Time Complexity:** `O(N)` — Each node is visited exactly once during bottom-up postorder traversal.
- **Auxiliary Space:** `O(H)` — Call stack depth corresponds to tree height (`O(log N)` balanced, `O(N)` skewed).
- **Output Space:** `O(1)` — Returns a single integer.

---

### Problem 2: Path Sum II (LeetCode 113)

#### 🎙️ 45-Minute Interview Talk Track
> *"Path Sum II requires finding all root-to-leaf paths whose node values sum to `targetSum`. We employ top-down DFS with backtracking. We maintain a dynamic path buffer and a remaining sum. At each node, we append `node.val` and subtract it from the running sum. If the node is a leaf and the remainder equals zero, we allocate a snapshot copy of the current path into our result list. Crucially, before unwinding to the parent frame, we remove the current node from the path buffer. This ensures sibling branches are explored with a clean path buffer without allocating separate arrays at every recursive step."*

#### C# Primary Implementation (.NET 8/9 — Backtracking Buffer)
```csharp
public static class PathSumSolver
{
    /// <summary>
    /// Finds all root-to-leaf paths that sum to targetSum.
    /// Time Complexity: O(N * H) | Auxiliary Space: O(H) path buffer
    /// </summary>
    public static IList<IList<int>> PathSum(TreeNode? root, int targetSum)
    {
        var result = new List<IList<int>>();
        var currentPath = new List<int>();
        Dfs(root, targetSum, currentPath, result);
        return result;

        static void Dfs(TreeNode? node, int remainingSum, List<int> path, List<IList<int>> result)
        {
            if (node is null) return;

            path.Add(node.val);
            remainingSum -= node.val;

            // Check if leaf node matches target sum
            if (node.left is null && node.right is null && remainingSum == 0)
            {
                result.Add(new List<int>(path)); // Snapshot copy
            }
            else
            {
                Dfs(node.left, remainingSum, path, result);
                Dfs(node.right, remainingSum, path, result);
            }

            // BACKTRACK: Remove current node before returning to parent frame
            path.RemoveAt(path.Count - 1);
        }
    }
}
```

#### Python Secondary Implementation (3.11+ — Idiomatic)
```python
def path_sum(root: Optional[TreeNode], target_sum: int) -> list[list[int]]:
    """Finds all root-to-leaf paths matching target_sum using backtracking.
    
    Time Complexity: O(N * H) | Auxiliary Space: O(H)
    """
    result: list[list[int]] = []
    current_path: list[int] = []

    def dfs(node: Optional[TreeNode], remaining: int) -> None:
        if not node:
            return

        current_path.append(node.val)
        remaining -= node.val

        if not node.left and not node.right and remaining == 0:
            result.append(list(current_path))
        else:
            dfs(node.left, remaining)
            dfs(node.right, remaining)

        current_path.pop()  # Backtrack

    dfs(root, target_sum)
    return result
```

#### 📊 Explicit Complexity Deconstruction
- **Time Complexity:** `O(N * H)` — In the worst case (complete binary tree where every path sums to target), there are `O(N)` leaves, and copying each path takes `O(H)` time.
- **Auxiliary Space:** `O(H)` — Call stack and the single shared `path` buffer both use `O(H)` space.
- **Output Space:** `O(N * H)` — Holds the matched path lists.

---

### Problem 3: Lowest Common Ancestor of a Binary Tree (LeetCode 236)

#### 🎙️ 45-Minute Interview Talk Track
> *"To find the Lowest Common Ancestor of nodes `p` and `q` in a general binary tree, we use postorder divide-and-conquer. If the current node is null, or matches either `p` or `q`, we return it immediately. Otherwise, we recursively search both left and right subtrees. If both recursive calls return non-null values, it means `p` and `q` reside in separate subtrees branching from the current node—making this node their lowest common ancestor. If only one side returns non-null, both targets reside on that side, so we pass that non-null node up the call stack."*

#### C# Primary Implementation (.NET 8/9 — Divide and Conquer)
```csharp
public static class LcaSolver
{
    /// <summary>
    /// Finds the Lowest Common Ancestor of p and q in a Binary Tree.
    /// Time Complexity: O(N) | Auxiliary Space: O(H)
    /// </summary>
    public static TreeNode? LowestCommonAncestor(TreeNode? root, TreeNode p, TreeNode q)
    {
        // Base Case: null hit or target matched
        if (root is null || root == p || root == q)
        {
            return root;
        }

        TreeNode? left = LowestCommonAncestor(root.left, p, q);
        TreeNode? right = LowestCommonAncestor(root.right, p, q);

        // If p and q found in opposite subtrees, current node is the LCA
        if (left is not null && right is not null)
        {
            return root;
        }

        // Otherwise return the branch that found a target
        return left ?? right;
    }
}
```

#### Python Secondary Implementation (3.11+ — Idiomatic)
```python
def lowest_common_ancestor(root: Optional[TreeNode], p: TreeNode, q: TreeNode) -> Optional[TreeNode]:
    """Finds the Lowest Common Ancestor of p and q.
    
    Time Complexity: O(N) | Auxiliary Space: O(H)
    """
    if not root or root is p or root is q:
        return root

    left = lowest_common_ancestor(root.left, p, q)
    right = lowest_common_ancestor(root.right, p, q)

    if left and right:
        return root

    return left or right
```

#### 📊 Explicit Complexity Deconstruction
- **Time Complexity:** `O(N)` — Every node is visited at most once in the worst case where targets are located at the bottom leaves.
- **Auxiliary Space:** `O(H)` — Call stack space bounded by tree height.
- **Output Space:** `O(1)` — Returns a single node reference.

---

### Problem 4: Serialize and Deserialize Binary Tree (LeetCode 297)

#### 🎙️ 45-Minute Interview Talk Track
> *"To serialize a binary tree into a string and reconstruct it losslessly, standard preorder traversal without null markers is ambiguous. By including an explicit sentinel `#` for every null child, preorder traversal becomes 1-to-1 with tree topology. For serialization, we traverse preorder, appending `node.val` and `,` for nodes, and `#,` for nulls into a StringBuilder. For deserialization, we split the string by commas into a FIFO Queue. We recursively consume tokens: if the token is `#`, we return null; otherwise, we construct a new TreeNode and recursively reconstruct its left and right subtrees in order."*

#### C# Primary Implementation (.NET 8/9 — Preorder Stream)
```csharp
using System.Text;

public sealed class Codec
{
    private const string NullMarker = "#";
    private const char Delimiter = ',';

    /// <summary>
    /// Serializes a binary tree to a single comma-separated string.
    /// Time Complexity: O(N) | Auxiliary Space: O(N)
    /// </summary>
    public string serialize(TreeNode? root)
    {
        var sb = new StringBuilder();
        BuildString(root, sb);
        return sb.ToString();

        static void BuildString(TreeNode? node, StringBuilder sb)
        {
            if (node is null)
            {
                sb.Append(NullMarker).Append(Delimiter);
                return;
            }

            sb.Append(node.val).Append(Delimiter);
            BuildString(node.left, sb);
            BuildString(node.right, sb);
        }
    }

    /// <summary>
    /// Deserializes a string back to the original binary tree.
    /// Time Complexity: O(N) | Auxiliary Space: O(N)
    /// </summary>
    public TreeNode? deserialize(string data)
    {
        if (string.IsNullOrEmpty(data)) return null;

        string[] tokens = data.Split(Delimiter, StringSplitOptions.RemoveEmptyEntries);
        var queue = new Queue<string>(tokens);
        return BuildTree(queue);

        static TreeNode? BuildTree(Queue<string> queue)
        {
            if (queue.Count == 0) return null;

            string token = queue.Dequeue();
            if (token == NullMarker)
            {
                return null;
            }

            var node = new TreeNode(int.Parse(token));
            node.left = BuildTree(queue);
            node.right = BuildTree(queue);
            return node;
        }
    }
}
```

#### Python Secondary Implementation (3.11+ — Idiomatic)
```python
class Codec:
    def serialize(self, root: Optional[TreeNode]) -> str:
        """Encodes a tree to a single string using preorder traversal.
        
        Time Complexity: O(N) | Auxiliary Space: O(N)
        """
        tokens: list[str] = []

        def build(node: Optional[TreeNode]) -> None:
            if not node:
                tokens.append("#")
                return
            tokens.append(str(node.val))
            build(node.left)
            build(node.right)

        build(root)
        return ",".join(tokens)

    def deserialize(self, data: str) -> Optional[TreeNode]:
        """Decodes string data back to the original binary tree.
        
        Time Complexity: O(N) | Auxiliary Space: O(N)
        """
        if not data:
            return None

        tokens = iter(data.split(","))

        def parse() -> Optional[TreeNode]:
            val = next(tokens)
            if val == "#":
                return None
            node = TreeNode(int(val))
            node.left = parse()
            node.right = parse()
            return node

        return parse()
```

#### 📊 Explicit Complexity Deconstruction
- **Time Complexity:** `O(N)` — Every node and null leaf is processed once during serialization and once during deserialization.
- **Auxiliary Space:** `O(N)` — Holds token arrays, the FIFO token queue, and recursive stack frames.

---

### 🧠 Pattern 5: Morris Inorder Traversal — Zero-Stack `O(1)` Space Walking

> [!IMPORTANT]
> **The Senior Bar-Raiser Curveball:**
> *"Can you traverse a binary tree in-order in `O(N)` time and strictly `O(1)` auxiliary space without recursion and without allocating a `Stack`?"*
> Standard DFS requires `O(H)` call-stack space. Morris Traversal eliminates the call stack entirely by temporarily threading the **right pointer of the inorder predecessor** back to the current node.

#### The Mental Model & Threading Mechanics
For any node `curr` with a left child:
1. Find its **inorder predecessor**: the rightmost node in `curr.left`.
2. If `predecessor.right == null`:
   - Create a temporary thread: `predecessor.right = curr`.
   - Move `curr = curr.left`.
3. If `predecessor.right == curr`:
   - The left subtree has already been visited! Break the thread: `predecessor.right = null`.
   - Visit `curr` (e.g. record `curr.val`).
   - Move `curr = curr.right`.
4. If `curr` has no left child:
   - Visit `curr`.
   - Move `curr = curr.right`.

```text
Tree Threading Visual:
        4 (curr)                         4 (curr)
       /                                /
      2                                2
     / \                              / \
    1   3 (predecessor)              1   3 ----+ (Thread pointing back to 4!)
                                               |
                                               v
                                               4
```

#### Dual-Language Implementation (Morris Inorder Traversal)

##### C# (.NET 8/9 Strictly `O(1)` Auxiliary Memory)

```csharp
using System.Collections.Generic;

public static class MorrisTraversalSolver
{
    /// <summary>
    /// Traverses binary tree in-order in O(N) time and O(1) auxiliary space using Morris Threading.
    /// Restores original tree structure before returning.
    /// </summary>
    public static IList<int> MorrisInorder(TreeNode? root)
    {
        var result = new List<int>();
        var curr = root;

        while (curr != null)
        {
            if (curr.left == null)
            {
                result.Add(curr.val);
                curr = curr.right;
            }
            else
            {
                // Find inorder predecessor: rightmost node in left subtree
                var predecessor = curr.left;
                while (predecessor.right != null && predecessor.right != curr)
                {
                    predecessor = predecessor.right;
                }

                if (predecessor.right == null)
                {
                    // Create thread back to current node
                    predecessor.right = curr;
                    curr = curr.left;
                }
                else
                {
                    // Thread already exists: left subtree finished! Remove thread and visit curr
                    predecessor.right = null;
                    result.Add(curr.val);
                    curr = curr.right;
                }
            }
        }

        return result;
    }
}
```

##### Python 3.11+ Idiomatic Implementation

```python
from typing import Optional, List

class Solution:
    def morrisInorderTraversal(self, root: Optional[TreeNode]) -> List[int]:
        """
        Inorder traversal in O(N) time and O(1) auxiliary memory using Morris threading.
        """
        result = []
        curr = root

        while curr:
            if not curr.left:
                result.append(curr.val)
                curr = curr.right
            else:
                # Find inorder predecessor
                pre = curr.left
                while pre.right and pre.right is not curr:
                    pre = pre.right

                if not pre.right:
                    # Construct temporary thread
                    pre.right = curr
                    curr = curr.left
                else:
                    # Dissolve thread and visit current node
                    pre.right = None
                    result.append(curr.val)
                    curr = curr.right

        return result
```

#### 📊 Explicit Complexity Deconstruction
- **Time Complexity:** `O(N)` amortized. Every edge is traversed at most 3 times (once to find predecessor, once to traverse, once to dissolve thread).
- **Auxiliary Space:** `O(1)` strictly! No call stack frames, no explicit `Stack` object. The tree is temporarily modified and completely restored to its original state.
- **Output Space:** `O(N)` for the returned sequence.

---

## ⚖️ CHAPTER 5: PERFORMANCE, TRADE-OFFS & FAANG PATTERN SIGNALS

### 🏭 Real-World Systems Context

> [!NOTE]
> **Production Context — Git Commit History & `git merge-base`:** Git's commit DAG uses Lowest Common Ancestor logic to perform three-way merges. `git merge-base commitA commitB` finds the latest shared ancestor commit. The diff between that LCA commit and the two branch tips determines clean auto-merges or merge conflicts.

> [!NOTE]
> **Production Context — Network Diameter & Packet Routing:** In unweighted network topologies and P2P routing overlays, the tree diameter dictates the worst-case packet propagation latency. If the diameter grows beyond acceptable thresholds, routing algorithms trigger topology re-rooting to minimize packet hop limits.

> [!NOTE]
> **Production Context — Distributed RPC Marshalling & Serialization:** When microservices communicate hierarchical query ASTs or protobuf configuration trees across network boundaries, they cannot pass raw memory pointers. Compact preorder serialization with delimiter sentinels minimizes wire payloads and ensures predictable streaming deserialization.

### Failure Modes & Edge Cases

| Failure Mode | Root Cause | Engineering Mitigation |
| :--- | :--- | :--- |
| **Path Sum Sibling Contamination** | Reusing a mutable `path` list without popping during backtrack | Ensure `path.RemoveAt(path.Count - 1)` / `path.pop()` executes in `finally` or after child calls |
| **Missing Snapshot Allocation** | Appending the mutable `path` reference directly into `result` | Always snapshot: `result.Add(new List<int>(path))` |
| **Diameter Root Assumption** | Assuming the longest path must pass through the tree root | Test every node as a turning point; track global max separately from height |
| **Malformed Serialization Tokens** | Using string concatenation (`+`) in a loop causing `O(N^2)` memory thrashing | Use `StringBuilder` in C# or `",".join()` in Python |

---

## ⚔️ SUPPLEMENTARY OUTCOMES

### 🏋️ Practice Problems

| # | Problem | Source | Difficulty | Target Pattern |
| :---: | :--- | :--- | :---: | :--- |
| 1 | Diameter of Binary Tree | LeetCode #543 | 🟢 Easy | Postorder bottom-up height & diameter |
| 2 | Path Sum | LeetCode #112 | 🟢 Easy | Top-down subtraction to leaf |
| 3 | Path Sum II | LeetCode #113 | 🟡 Medium | Backtracking path buffer snapshot |
| 4 | Lowest Common Ancestor of Binary Tree | LeetCode #236 | 🟡 Medium | Postorder target split propagation |
| 5 | Serialize and Deserialize Binary Tree | LeetCode #297 | 🔴 Hard | Preorder null sentinel streaming |
| 6 | Binary Tree Maximum Path Sum | LeetCode #124 | 🔴 Hard | Diameter pattern with negative value clamping |
| 7 | All Nodes Distance K in Binary Tree | LeetCode #863 | 🟡 Medium | Parent mapping + BFS graph conversion |
| 8 | Flatten Binary Tree to Linked List | LeetCode #114 | 🟡 Medium | Reverse postorder pointer rewriting |
| 9 | Sum Root to Leaf Numbers | LeetCode #129 | 🟡 Medium | Preorder accumulator |
| 10 | Step-By-Step Directions Between Nodes | LeetCode #2096 | 🟡 Medium | LCA + path prefix pruning |

### 🎙️ Interview Questions & Follow-ups
1. **Q: How does Binary Tree Maximum Path Sum (LeetCode 124) differ from Tree Diameter?**
   - *Follow-up:* In diameter, edge lengths are positive integers (1 each). In Max Path Sum, node values can be negative. When computing subtree gains, we must clamp negative gains to zero: `Math.Max(0, leftGain)` to prevent reducing the parent's sum.
2. **Q: How would you perform LCA queries in `O(log N)` time if given 100,000 queries on a static tree?**
   - *Follow-up:* Preprocess the tree using **Binary Lifting** (storing `2^k` ancestors for each node) in `O(N log N)` time and space, reducing each LCA query to `O(log N)` binary steps.

---

## 📊 COMPLEXITY RECAP

| Pattern | Time Complexity | Auxiliary Space | Output Space | Key Mechanics |
| :--- | :---: | :---: | :---: | :--- |
| **Tree Diameter** | `O(N)` | `O(H)` | `O(1)` | Height return + max diameter update |
| **Path Sum II** | `O(N * H)` | `O(H)` | `O(N * H)` | Backtracking + snapshot copy |
| **Lowest Common Ancestor** | `O(N)` | `O(H)` | `O(1)` | Divide-and-conquer postorder split |
| **Serialization** | `O(N)` | `O(N)` | `O(N)` | Preorder stream + sentinel queue |

---

> 🧭 **Navigation:** [← Previous Day](Week_07_Day_03_Balanced_BSTs_AVL_And_RedBlack_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_07_Day_05_Augmented_BSTs_OrderStatistics_Instructional.md)
