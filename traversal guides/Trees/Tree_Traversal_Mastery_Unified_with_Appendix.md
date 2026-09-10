# 🌳 Tree Traversal Mastery — Curriculum 2.0 Unified Guide

**Goal:** Master traversal and navigation across tree variants (binary, BST, N-ary, tries, implicit/array trees, rooted trees from edges), with strong invariants and debugging instincts.

---

## 1. Quick Revision Summary

| Level | Topic | Mental Model | Pointer State / Invariant | Drill Problems |
|---|---|---|---|---|
| **1** | Basic DFS (Recursion) | "Solve node using children's solutions." | `root`: Current valid sub-problem. Invariant: Trust the recursion. | [LeetCode 144], [LeetCode 94], [LeetCode 145] |
| **2** | Representations | Trees can be memory pointers or math on array indices. | Pointer: `node`, Array: index `i`, Left:`2i+1`, Right:`2i+2`. | [LeetCode 226], [LeetCode 114] |
| **3** | Iterative & BFS Waves | Explicit state machine or Level-by-level queue waves. | Queue/Stack size boundaries. Invariant: Wave length snapshot. | [LeetCode 102], [LeetCode 103], [LeetCode 199] |
| **4** | Path & Coordinates | Nodes exist on a 2D grid `(row, col)` or a rooted path. | `(node, path)` or `(node, row, col)`. Left is `(r+1, c-1)`. | [LeetCode 112], [LeetCode 113], [LeetCode 314] |
| **5** | Tree DP & Control | Local return vs Global state updates. | `dfs(node) -> local_state`. Invariant: Parent receives valid sub-state. | [LeetCode 543], [LeetCode 124], [LeetCode 99] |
| **Appx**| Graphs & Serialization| Tree as DAG or Prefix strings (Tries). | `dfs(node, parent)`. Invariant: `child != parent` prevents cycles. | [LeetCode 834], [LeetCode 297], [LeetCode 208] |

---

## Level 1: 🟢 Recursive DFS Basics

### Mental Model & Invariants
- **What is the pointer?** The `node` argument representing the root of the current valid sub-tree.
- **What region is processed?** The entire subtree rooted at `node`.
- **What is the invariant?** The function always returns the complete result for the subtree rooted at `node`, assuming children return correctly (Trust the recursion).

### Visual State Transitions (Visit Timing)
| Timing / Step | Node Pointer | Recursive Stack | Action / Invariant |
| :--- | :--- | :--- | :--- |
| **Preorder** | `node (1)` | `[1]` | Parent -> Left -> Right. Top-down processing before children. |
| **Inorder** | `node (2)` | `[1, 2]` | Left -> Parent -> Right. Middle-out, yields sorted order in BST. |
| **Postorder**| `node (3)` | `[1, 2, 3]` | Left -> Right -> Parent. Bottom-up, aggregating height/size. |

### Code Snippets
Problem: Calculate the sum of all node values in a binary tree using recursive depth-first search.
```python
def dfs(node):
    # 1. Base case: If the current valid sub-tree is empty, return sum 0.
    if not node: return 0
    # 2. PRE-ORDER WORK: Action taken top-down before exploring children.
    # 3. Explore left: Trust the recursion to return the sum of the left subtree.
    left_val = dfs(node.left)
    # 4. IN-ORDER WORK: Action taken middle-out between exploring left and right.
    # 5. Explore right: Trust the recursion to return the sum of the right subtree.
    right_val = dfs(node.right)
    # 6. POST-ORDER WORK: Action taken bottom-up after traversing both children.
    # 7. Aggregate and return the complete result for the subtree rooted at `node`.
    return left_val + right_val + node.val
```
Problem: Calculate the sum of all node values in a binary tree using recursive depth-first search.
```csharp
public int DFS(TreeNode node) {
    // 1. Base case: If the current valid sub-tree is empty, return sum 0.
    if (node == null) return 0;
    // 2. PRE-ORDER WORK: Action taken top-down before exploring children.
    // 3. Explore left: Trust the recursion to return the sum of the left subtree.
    int leftVal = DFS(node.left);
    // 4. IN-ORDER WORK: Action taken middle-out between exploring left and right.
    // 5. Explore right: Trust the recursion to return the sum of the right subtree.
    int rightVal = DFS(node.right);
    // 6. POST-ORDER WORK: Action taken bottom-up after traversing both children.
    // 7. Aggregate and return the complete result for the subtree rooted at `node`.
    return leftVal + rightVal + node.val;
}
```

### ⚠️ Gotchas & Pitfalls
- **Null Node Crash**: Failing to handle `if not node:` as the base case leads to Null Reference Exceptions when traversing leaves.
- **Accidental Global State**: Storing path states or aggregated values in global variables without resetting them between test cases.
- **Lost Returns**: Forgetting to `return` the recursive function call's result to the parent, returning `None`/`null` by mistake.

### Drill Problems
- **Easy:**
  - [LeetCode 144: Binary Tree Preorder Traversal]
  - [LeetCode 94: Binary Tree Inorder Traversal]
  - [LeetCode 145: Binary Tree Postorder Traversal]
- **Medium:**
  - [LeetCode 104: Maximum Depth of Binary Tree]

---

## Level 2: 🔵 Representations (Pointers vs Arrays)

### Mental Model & Invariants
- **What is the pointer?** Either an object reference `node` OR an integer index `i`.
- **What region is processed?** The node in memory or array bounds.
- **What is the invariant?** Array bounds must be respected (`i < n`). Node connections are mathematically fixed.

### Visual State Transitions (Implicit Array Tree)
| Node Index `i` | Value | Left Child `2i+1` | Right Child `2i+2` | Parent `(i-1)//2` |
|---|---|---|---|---|
| `0` | `A` (Root)| `1` (`B`) | `2` (`C`) | Out of Bounds |
| `1` | `B` | `3` (`D`) | `4` (`E`) | `0` (`A`) |
| `2` | `C` | `5` (`F`) | `6` (`G`) | `0` (`A`) |

### Code Snippets (Array Tree Math)
Problem: Navigate a binary tree implicitly represented as a contiguous array where nodes are mathematically linked.
```python
# 1. Left Child: Derived mathematically as 2 * current index + 1 (0-indexed).
def left_child(i): return 2 * i + 1
# 2. Right Child: Derived mathematically as 2 * current index + 2 (0-indexed).
def right_child(i): return 2 * i + 2
# 3. Parent: Derived mathematically as (current index - 1) // 2 (0-indexed).
def parent(i): return (i - 1) // 2
```
Problem: Navigate a binary tree implicitly represented as a contiguous array where nodes are mathematically linked.
```csharp
// 1. Left Child: Derived mathematically as 2 * current index + 1 (0-indexed).
int LeftChild(int i) => 2 * i + 1;
// 2. Right Child: Derived mathematically as 2 * current index + 2 (0-indexed).
int RightChild(int i) => 2 * i + 2;
// 3. Parent: Derived mathematically as (current index - 1) / 2 (0-indexed).
int Parent(int i) => (i - 1) / 2;
```

### ⚠️ Gotchas & Pitfalls
- **Out of Bounds Errors**: Not explicitly checking if the calculated array index `2i+1` or `2i+2` exceeds the array length.
- **Missing Nodes Mapping**: In implicit array trees, missing nodes still consume indices. Skipping them can shift the entire left/right child math, ruining the structure.
- **0-Index vs 1-Index Math**: Mixing up math equations. 1-indexed math is `2i` and `2i+1`, whereas 0-indexed is `2i+1` and `2i+2`.

### Drill Problems
- **Easy:**
  - [LeetCode 226: Invert Binary Tree]
- **Medium:**
  - [LeetCode 114: Flatten Binary Tree to Linked List]

---

## Level 3: 🟠 Iterative DFS & BFS Waves

### Mental Model & Invariants
- **What is the pointer?** A dynamic frontier containing nodes: queue (BFS) or stack (DFS).
- **What region is processed?** BFS processes a strictly defined "level" or "wave".
- **What is the invariant?** In BFS waves, taking a snapshot of `queue.length` at the start of the loop guarantees we process exactly one level.

### Visual State Transitions (BFS Wave)
| Step | Queue Content | Wave Size | Action / Invariant |
|---|---|---|---|
| 1 | `[Root]` | `len=1` | Wave 1. Pop Root, Enqueue L, R. |
| 2 | `[L, R]` | `len=2` | Wave 2. Pop L, Enqueue L.left, L.right. |
| 3 | `[R, L.left, L.right]`| `len=2` (Processing) | Continuing Wave 2. Pop R, Enqueue R.left, R.right. |
| 4 | `[L.left, L.right, R.left, R.right]` | `len=4` | Wave 3 snapshot ready. |

### Code Snippets (BFS Wave)
Problem: Traverse a binary tree level-by-level (BFS) to group all node values by their depth.
```python
from collections import deque

def bfs(root):
    # 1. Handle edge case: Empty tree has no levels.
    if not root: return []
    # 2. Initialize the dynamic frontier (queue) with the root node.
    queue = deque([root])
    levels = []
    
    # 3. Process the queue until the frontier is empty.
    while queue:
        # 4. INVARIANT: Take a snapshot of the current queue length to process exactly one wave.
        level_size = len(queue)
        current_level = []
        # 5. Process all nodes in the current wave snapshot.
        for _ in range(level_size):
            # 6. Dequeue the front node and record its value.
            node = queue.popleft()
            current_level.append(node.val)
            # 7. Expand frontier: Enqueue valid left and right children for the next wave.
            if node.left: queue.append(node.left)
            if node.right: queue.append(node.right)
        # 8. Store the fully processed level.
        levels.append(current_level)
    return levels
```
Problem: Traverse a binary tree level-by-level (BFS) to group all node values by their depth.
```csharp
public IList<IList<int>> LevelOrder(TreeNode root) {
    var res = new List<IList<int>>();
    // 1. Handle edge case: Empty tree has no levels.
    if (root == null) return res;
    // 2. Initialize the dynamic frontier (queue) with the root node.
    var queue = new Queue<TreeNode>();
    queue.Enqueue(root);
    
    // 3. Process the queue until the frontier is empty.
    while (queue.Count > 0) {
        // 4. INVARIANT: Take a snapshot of the current queue length to process exactly one wave.
        int levelSize = queue.Count;
        var currentLevel = new List<int>();
        // 5. Process all nodes in the current wave snapshot.
        for (int i = 0; i < levelSize; i++) {
            // 6. Dequeue the front node and record its value.
            var node = queue.Dequeue();
            currentLevel.Add(node.val);
            // 7. Expand frontier: Enqueue valid left and right children for the next wave.
            if (node.left != null) queue.Enqueue(node.left);
            if (node.right != null) queue.Enqueue(node.right);
        }
        // 8. Store the fully processed level.
        res.Add(currentLevel);
    }
    return res;
}
```

### ⚠️ Gotchas & Pitfalls
- **Snapshot Omission**: Processing the queue `while queue:` without capturing the snapshot size `level_size = len(queue)` first, mixing up current and next levels.
- **Infinite Loops with Graphs**: Treating a cyclic graph like a tree without a `visited` set or parent tracking.
- **Queue Memory Bloat**: Queues can hold up to $N/2$ nodes at the deepest level of a balanced tree, potentially causing memory limit errors for massive trees.

### Drill Problems
- **Medium:**
  - [LeetCode 102: Binary Tree Level Order Traversal]
  - [LeetCode 103: Binary Tree Zigzag Level Order Traversal]
  - [LeetCode 199: Binary Tree Right Side View]

---

## Level 4: 🟣 Path & Coordinate Traversals

### Mental Model & Invariants
- **What is the pointer?** Extended node state: `(node, running_path)` or `(node, row, col)`.
- **What region is processed?** 2D Grid mapping of tree nodes, or specific root-to-leaf paths.
- **What is the invariant?** Child coordinates are strictly `(row + 1, col - 1)` for Left and `(row + 1, col + 1)` for Right. For paths, whatever state is ADDED must be REMOVED (Backtracking).

### Code Snippets (Backtracking Path)
Problem: Find all root-to-leaf paths in a binary tree where the sum of the node values equals a specified target sum.
```python
def find_paths(node, target_sum, current_path, res):
    # 1. Base case: Reached past a leaf node, nothing to process.
    if not node: return
    # 2. ADD to state: Include the current node's value in our running path state.
    current_path.append(node.val)
    
    # 3. Check condition: Are we at a leaf node and does the path meet the target sum?
    if not node.left and not node.right and sum(current_path) == target_sum:
        # 4. Valid path found: Deep copy the state to results so it isn't mutated later.
        res.append(list(current_path))
        
    # 5. Recursive step: Continue path exploration down the left and right subtrees.
    find_paths(node.left, target_sum, current_path, res)
    find_paths(node.right, target_sum, current_path, res)
    
    # 6. INVARIANT: BACKTRACK: Remove the current node from the state before returning to the parent.
    current_path.pop()
```
Problem: Find all root-to-leaf paths in a binary tree where the sum of the node values equals a specified target sum.
```csharp
public void FindPaths(TreeNode node, int targetSum, List<int> currentPath, List<IList<int>> res) {
    // 1. Base case: Reached past a leaf node, nothing to process.
    if (node == null) return;
    // 2. ADD to state: Include the current node's value in our running path state.
    currentPath.Add(node.val);
    
    // 3. Check condition: Are we at a leaf node and does the path meet the target sum?
    if (node.left == null && node.right == null && currentPath.Sum() == targetSum) {
        // 4. Valid path found: Deep copy the state to results so it isn't mutated later.
        res.Add(new List<int>(currentPath));
    }
    
    // 5. Recursive step: Continue path exploration down the left and right subtrees.
    FindPaths(node.left, targetSum, currentPath, res);
    FindPaths(node.right, targetSum, currentPath, res);
    
    // 6. INVARIANT: BACKTRACK: Remove the current node from the state before returning to the parent.
    currentPath.RemoveAt(currentPath.Count - 1);
}
```

### ⚠️ Gotchas & Pitfalls
- **Reference Sharing / Shallow Copy**: Appending `current_path` directly to the results instead of a deep copy (e.g., `list(current_path)`), resulting in all paths reflecting the final empty state.
- **Missing Backtrack**: Forgetting to `.pop()` the node from the path state after returning from recursive calls, leaving stale data for the next branches.
- **Negative Coordinates**: In coordinate traversal, negative columns (going left repeatedly) will crash array-based grouping unless offsets or HashMaps are used.

### Drill Problems
- **Easy:**
  - [LeetCode 112: Path Sum]
- **Medium:**
  - [LeetCode 113: Path Sum II]
  - [LeetCode 314: Binary Tree Vertical Order Traversal]

---

## Level 5: 🔴 Advanced Control & Tree DP

### Mental Model & Invariants
- **What is the pointer?** Current node computing local limits.
- **What region is processed?** Tree Sub-problems.
- **What is the invariant?** Every recursive call returns exactly the information the parent needs (local state), while selectively updating a global state across all nodes (e.g., maximum diameter).

### Visual State Transitions (Global vs Local State)
| Node Type | Local Return (to parent) | Global Variable Update | Action / Invariant |
|---|---|---|---|
| **Leaf** | `1` (height = 1) | `max(global, 0)` | Base length is 0. Local height is 1. |
| **Parent** | `max(L, R) + 1` | `max(global, L + R)`| Update global diameter using paths crossing root. |

### Code Snippets (Tree DP)
Problem: Compute the diameter of a binary tree, defined as the length of the longest path between any two nodes, which may or may not pass through the root.
```python
class Solution:
    def diameterOfBinaryTree(self, root: TreeNode) -> int:
        # 1. Initialize global state: track the maximum diameter found so far across all nodes.
        self.max_diam = 0
        
        def dfs(node):
            # 2. Base case: An empty sub-tree has a height of 0.
            if not node: return 0
            
            # 3. Explore left and right: obtain the local height of left and right subtrees.
            left_height = dfs(node.left)
            right_height = dfs(node.right)
            
            # 4. GLOBAL UPDATE: Update the global max diameter if the path crossing this node is larger.
            self.max_diam = max(self.max_diam, left_height + right_height)
            
            # 5. LOCAL RETURN: Return the strict linear height to the parent node.
            return 1 + max(left_height, right_height)
            
        # 6. Kick off the DFS starting from the root.
        dfs(root)
        # 7. Return the globally updated tracking variable.
        return self.max_diam
```
Problem: Compute the diameter of a binary tree, defined as the length of the longest path between any two nodes, which may or may not pass through the root.
```csharp
public class Solution {
    // 1. Initialize global state: track the maximum diameter found so far across all nodes.
    int maxDiam = 0;
    
    public int DiameterOfBinaryTree(TreeNode root) {
        // 2. Kick off the DFS starting from the root.
        DFS(root);
        // 3. Return the globally updated tracking variable.
        return maxDiam;
    }
    
    private int DFS(TreeNode node) {
        // 4. Base case: An empty sub-tree has a height of 0.
        if (node == null) return 0;
        
        // 5. Explore left and right: obtain the local height of left and right subtrees.
        int leftHeight = DFS(node.left);
        int rightHeight = DFS(node.right);
        
        // 6. GLOBAL UPDATE: Update the global max diameter if the path crossing this node is larger.
        maxDiam = Math.Max(maxDiam, leftHeight + rightHeight);
        
        // 7. LOCAL RETURN: Return the strict linear height to the parent node.
        return 1 + Math.Max(leftHeight, rightHeight);
    }
}
```

### ⚠️ Gotchas & Pitfalls
- **Misaligned Sub-problems**: Returning a path that forks (like a full inverted V shape) to the parent in DP, violating the strict single-path local return rule.
- **Negative Values**: In Max Path Sum DP, failing to cap negative local path returns at 0 (`max(0, dfs(node))`), which drags down the global sum.
- **Initialization Danger**: Initializing global max trackers to `0` when the tree contains only negative numbers (should initialize to `-infinity`).

### Drill Problems
- **Easy:**
  - [LeetCode 543: Diameter of Binary Tree]
- **Hard:**
  - [LeetCode 124: Binary Tree Maximum Path Sum]
  - [LeetCode 99: Recover Binary Search Tree]

---

## 🌐 Appendix: Graphs, Serialization, Tries

### Mental Model & Invariants
- **What is the pointer?** Adjacency list node or string index/Trie node.
- **What is the invariant?** In undirected trees processed as graphs, tracking `node != parent` prevents infinite loops. In serialization, nulls (e.g., `#`) precisely define missing children, allowing perfect reconstruction without ambiguity.

### ⚠️ Gotchas & Pitfalls
- **Back-edge Infinite Recursion**: Not checking `if neighbor == parent: continue` during DFS on undirected trees.
- **Double Digit Strings**: When serializing, failing to use delimiters (like commas). `1,2,3` becomes `123` otherwise, merging double-digit node values incorrectly.
- **Memory Overhead in Tries**: Allocating fixed size arrays (e.g., `[26]`) for every node in a Trie can heavily fragment memory on sparse graphs; consider HashMaps if memory bound.

### Drill Problems
- **Medium:**
  - [LeetCode 208: Implement Trie (Prefix Tree)]
  - [LeetCode 834: Sum of Distances in Tree]
- **Hard:**
  - [LeetCode 297: Serialize and Deserialize Binary Tree]
