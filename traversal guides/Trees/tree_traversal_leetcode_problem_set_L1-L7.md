# LeetCode Problem Set — Tree Traversal Mastery (Levels 1→7)

**Scope:** Binary trees (Levels 1–6) + optional DS-specific traversal patterns (Level 7+).  
**Contract:** LENIENT (empty input returns empty output / safe defaults).

Last updated: 2026-02-10

---

## How to use this set

- Do Level 1 → Level 6 in order.
- For each problem: write the invariant first, then implement.
- If you can’t dry-run the frontier, you’re not ready to code it.

---

## Level 1 (core orders)

- 144. Binary Tree Preorder Traversal
- 94. Binary Tree Inorder Traversal
- 145. Binary Tree Postorder Traversal
- 102. Binary Tree Level Order Traversal
- 104. Maximum Depth of Binary Tree
- 111. Minimum Depth of Binary Tree

---

## Level 2 (iterative control)

- 144 / 94 / 145 again, but **iterative only**
- 173. Binary Search Tree Iterator (inorder as an iterator)
- 230. Kth Smallest Element in a BST (inorder reasoning)
- 112. Path Sum (DFS with early stopping)

---

## Level 3 (levels, zigzag, and views)

- 103. Binary Tree Zigzag Level Order Traversal
- 107. Binary Tree Level Order Traversal II
- 199. Binary Tree Right Side View
- 637. Average of Levels in Binary Tree
- 515. Find Largest Value in Each Tree Row
- 662. Maximum Width of Binary Tree

---

## Level 4 (vertical/diagonal/boundary-style)

- 987. Vertical Order Traversal of a Binary Tree
- 314. Binary Tree Vertical Order Traversal (if available in your LC list)
- 863. All Nodes Distance K in Binary Tree (BFS from target; traversal on an undirected view)
- 872. Leaf-Similar Trees (leaf-only traversal)
- 543. Diameter of Binary Tree (postorder-style aggregation)

---

## Level 5 (serialization / reconstruction / traversal as transform)

- 297. Serialize and Deserialize Binary Tree
- 105. Construct Binary Tree from Preorder and Inorder Traversal
- 106. Construct Binary Tree from Inorder and Postorder Traversal
- 114. Flatten Binary Tree to Linked List
- 129. Sum Root to Leaf Numbers

---

## Level 6 (space-optimized mindset)

LeetCode rarely *requires* Morris explicitly, but use these as targets to implement inorder without stack/recursion:

- 94. Binary Tree Inorder Traversal (implement with Morris)
- 230. Kth Smallest Element in a BST (Morris inorder)

---

## Level 7+ (optional DS-specific traversal patterns)

### Segment tree

- 307. Range Sum Query — Mutable
- 715. Range Module

### Fenwick (BIT)

- 307. Range Sum Query — Mutable (alternative solution)
- 315. Count of Smaller Numbers After Self (Fenwick or segment tree)

### Trie

- 208. Implement Trie (Prefix Tree)
- 211. Design Add and Search Words Data Structure
- 212. Word Search II
- 1268. Search Suggestions System

---

## Notes on ordering contracts

For “derived order” problems (vertical/top/bottom), always state tie-break rules explicitly in your solution writeup.
