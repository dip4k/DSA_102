# 🧪 Flow-Wise Tree Traversal — Problem Set (v1)
**Standard:** At least 2 LeetCode problems per subtopic + timeboxes + expected invariant.

---

# 🟢 Level 1 — Recursive DFS Basics

## 1.1 Recursive DFS skeleton
- LeetCode 144: Binary Tree Preorder Traversal
  - ⏱️ 20–30 min
  - 🧾 Invariant: when dfs(node) returns, preorder list for node’s subtree is correct.
- LeetCode 94: Binary Tree Inorder Traversal
  - ⏱️ 20–30 min
  - 🧾 Invariant: inorder output equals left-subtree, node, right-subtree.

## 1.2 Visit timing (pre/in/post)
- LeetCode 145: Binary Tree Postorder Traversal
  - ⏱️ 30–45 min
  - 🧾 Invariant: node is processed after both children are fully processed.
- LeetCode 104: Maximum Depth of Binary Tree
  - ⏱️ 20–35 min
  - 🧾 Invariant: dfs(node) returns correct depth for node’s subtree.

## 1.3 Edge cases (empty/single/skew)
- LeetCode 100: Same Tree
  - ⏱️ 20–30 min
  - 🧾 Invariant: dfs compares matching structure; null/null matches.
- LeetCode 101: Symmetric Tree
  - ⏱️ 30–45 min
  - 🧾 Invariant: mirror-dfs ensures left subtree mirrors right subtree.

---

# 🔵 Level 2 — Iterative DFS (Stack)

## 2.1 Iterative inorder (classic stack)
- LeetCode 94: Binary Tree Inorder Traversal (iterative)
  - ⏱️ 25–40 min
  - 🧾 Invariant: stack holds the path to the next unvisited node.
- LeetCode 230: Kth Smallest Element in a BST
  - ⏱️ 35–55 min
  - 🧾 Invariant: inorder visits nodes in sorted order; counter hits k at answer.

## 2.2 Frame-based / stateful traversal
- LeetCode 145: Binary Tree Postorder Traversal (iterative)
  - ⏱️ 45–70 min
  - 🧾 Invariant: each node frame progresses through states deterministically.
- LeetCode 98: Validate Binary Search Tree (iterative inorder)
  - ⏱️ 35–55 min
  - 🧾 Invariant: inorder sequence must be strictly increasing.

---

# 🟠 Level 3 — BFS / Frontier Layer

## 3.1 Level snapshot BFS
- LeetCode 102: Binary Tree Level Order Traversal
  - ⏱️ 25–40 min
  - 🧾 Invariant: queue contains only the next frontier; each loop emits exactly one level.
- LeetCode 199: Binary Tree Right Side View
  - ⏱️ 30–45 min
  - 🧾 Invariant: last node processed per level (or first from right) is visible.

## 3.2 Zigzag & per-level aggregates
- LeetCode 103: Binary Tree Zigzag Level Order Traversal
  - ⏱️ 35–55 min
  - 🧾 Invariant: each level collected fully; direction alternates by depth parity.
- LeetCode 515: Find Largest Value in Each Tree Row
  - ⏱️ 25–45 min
  - 🧾 Invariant: max for a level computed from exactly that level’s nodes.

---

# 🟣 Level 4 — Morris & Structural Tricks

## 4.1 Morris traversal (restore tree)
- LeetCode 94: Binary Tree Inorder Traversal (Morris follow-up)
  - ⏱️ 60–90 min
  - 🧾 Invariant: threads are created only when pred.right is null and removed when pred.right == curr.
- LeetCode 99: Recover Binary Search Tree
  - ⏱️ 75–120 min
  - 🧾 Invariant: inorder anomaly detection identifies swapped nodes.

## 4.2 Iterator / next pointers
- LeetCode 173: Binary Search Tree Iterator
  - ⏱️ 45–75 min
  - 🧾 Invariant: stack represents path to next smallest; next() pops correct node.
- LeetCode 116: Populating Next Right Pointers in Each Node
  - ⏱️ 45–75 min
  - 🧾 Invariant: next pointers connect nodes within the same level only.

---

# 🔴 Level 5 — Traversal as Solver (Tree DP + Backtracking)

## 5.1 Tree DP (return + global update)
- LeetCode 543: Diameter of Binary Tree
  - ⏱️ 45–75 min
  - 🧾 Invariant: dfs returns height; global diameter uses leftHeight+rightHeight.
- LeetCode 124: Binary Tree Maximum Path Sum
  - ⏱️ 60–120 min
  - 🧾 Invariant: dfs returns max gain upward; global uses left+node+right.

## 5.2 Path-based DFS (backtracking)
- LeetCode 113: Path Sum II
  - ⏱️ 45–75 min
  - 🧾 Invariant: path list equals root→current; rollback restores it for siblings.
- LeetCode 437: Path Sum III
  - ⏱️ 60–110 min
  - 🧾 Invariant: prefix-sum counts reflect current root→node path only.

---

## ✅ How to practice (recommended)
- First pass: solve Level 1 + Level 3 only (recursion + BFS).
- Second pass: re-solve with Level 2 iterative versions.
- Third pass: attempt Level 4 Morris only after you are rock-solid on inorder stack.
- Always write the invariant at the top of your solution before coding.
