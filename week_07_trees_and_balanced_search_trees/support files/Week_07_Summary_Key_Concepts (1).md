# 📌 Week_07_Summary_Key_Concepts.md

**Version:** v1.2 (Concept Summary & Quick Reference)  
**Purpose:** Condensed essential concepts for rapid recall and concept verification  
**Audience:** Active learners during problem-solving and interview prep  
**Status:** ✅ PRODUCTION-READY

---

## 🎯 HOW TO USE THIS FILE

This file is your **quick reference card** for Week 7 concepts. Use it to:
- **Refresh concepts** before coding
- **Verify understanding** of definitions
- **Quick-check** during problem-solving
- **Explain to yourself** why each concept matters

**Best Practice:** Read each section, close the file, and reproduce the definition from memory. Return if you struggle.

---

## 🌳 SECTION 1: TREE BASICS

### Height & Depth (Foundational)

**Height(node):** Distance from node to its furthest descendant leaf.
- Leaf height = 0
- Height(parent) = 1 + max(height(left), height(right))
- **Tree Height:** Height of root node

**Depth(node):** Distance from root to node.
- Root depth = 0
- Depth(node) = depth(parent) + 1

**Key Relationship:**
- For a balanced tree with n nodes: height ≈ log₂(n)
- For a degenerate tree with n nodes: height = n - 1

**Why It Matters:** Tree operations (search, insert, delete) visit O(height) nodes. Balanced ≈ log n; degenerate ≈ n. **1000x difference on 1M items.**

---

### Tree Classifications (Conceptual)

**Full Binary Tree**
- Definition: Every node has 0 or 2 children (no single-child nodes)
- Property: Nice mathematical properties; # of leaves = # of internal nodes + 1
- Use: Theoretical analysis

**Complete Binary Tree**
- Definition: All levels fully filled except last; last level fills left-to-right
- Property: Can be stored in array (no pointers needed)
- Use: Heap implementation, array-based trees

**Balanced Binary Tree**
- Definition: Height ≈ log₂(n) for n nodes (height difference between left & right ≤ constant)
- Property: O(log n) per operation guaranteed
- Use: Search trees (BST, AVL, red-black)

**Degenerate Tree**
- Definition: Chain-like; each node has at most 1 child
- Property: Height = n (worst case)
- Use: What to avoid; motivates balancing

---

### Visual Reference: Tree Terminology

```
        1 (root, height=2, depth=0)
       / \
      2   3 (leaf, height=0, depth=1)
     /
    4 (leaf, height=0, depth=2)

Height breakdown:
- height(4) = 0 (leaf)
- height(2) = 1 (parent of height-0 node)
- height(1) = 2 (parent of max height-1 nodes)

Complete? No (last level not left-filled)
Full? No (node 3 has 0 children, others have 1-2)
Balanced? No (left subtree height=2, right=1)
```

---

## 🔁 SECTION 2: TRAVERSALS

### The Four Orders (Quick Reference)

| Order | Sequence | Pattern | Use Case |
|---|---|---|---|
| **Preorder** | Parent first | NLR (Node-Left-Right) | Copy tree, prefix notation |
| **Inorder** | Parent middle | LNR (Left-Node-Right) | BST sorted output |
| **Postorder** | Parent last | LRN (Left-Right-Node) | Delete tree, postfix |
| **Level-order** | Layer by layer | BFS with queue | Serialization, layer tasks |

### Example: Tracing All Orders

```
Tree:
      1
     / \
    2   3
   /
  4

Preorder (NLR):   1, 2, 4, 3
Inorder (LNR):    4, 2, 1, 3
Postorder (LRN):  4, 2, 3, 1
Level-order:      1, 2, 3, 4
```

### Template: Recursive Preorder

```
function preorder(node):
    if node is null: return
    visit(node)              // N
    preorder(node.left)      // L
    preorder(node.right)     // R
```

### Key Insight: Inorder on BST

**Property:** Inorder traversal of a valid BST produces values in **sorted ascending order**.

```
BST:          5
            /   \
           3     7
          / \   / \
         1   4 6   9

Inorder: 1, 3, 4, 5, 6, 7, 9 ← Sorted!
```

---

### Iterative Traversals (Key Patterns)

**Iterative Inorder (Using Stack):**
```
function inorderIterative(root):
    stack = []
    current = root
    while current or stack not empty:
        while current:
            stack.push(current)
            current = current.left
        current = stack.pop()
        visit(current)
        current = current.right
```

**Iterative Level-Order (Using Queue):**
```
function levelorder(root):
    queue = [root]
    while queue not empty:
        levelSize = queue.size()
        for i = 0 to levelSize - 1:
            node = queue.pop()
            visit(node)
            queue.push(node.left)      // if exists
            queue.push(node.right)     // if exists
```

**Critical:** Capture `levelSize` before loop to separate levels.

---

## 🌲 SECTION 3: BINARY SEARCH TREES (BSTs)

### The BST Invariant (Critical)

**Definition:** For every node:
- ALL values in left subtree < node.value
- ALL values in right subtree > node.value
- **This is GLOBAL, not just local comparison**

**Why it Matters:** Enables binary search. At each node, eliminate half the tree.

**Visual:**
```
Valid BST:      5          Invalid:       5
              /   \                      / \
             3     7                    3   4 (4 < 5, wrong side!)
            / \   / \
           1   4 6   9
```

### BST Operations Summary

| Operation | Best | Avg | Worst | Notes |
|---|---|---|---|---|
| **Search** | O(log n) | O(log n) | O(n) | Compare & go left/right |
| **Insert** | O(log n) | O(log n) | O(n) | Find leaf position |
| **Delete** | O(log n) | O(log n) | O(n) | Successor logic (2-child) |

---

### Deletion: Three Cases (Detailed)

**Case 1: Leaf (no children)**
```
Delete 4:    5          →    5
           /   \              \
          3     7              7
         /     / \
        1     6   9

Just remove node.
```

**Case 2: One Child**
```
Delete 3:    5          →    5
           /   \              \
          2     7              7
           \   / \
            3 6   9

Child replaces parent. Set parent.left = node.right.
```

**Case 3: Two Children (Hardest)**
```
Delete 5:    5          →    6
           /   \              / \
          3     7      (copy 6) 3   7
         / \   / \            / \
        1   4 6   9          1   4 9

Step 1: Find inorder successor (min in right subtree) = 6
Step 2: Copy 6's value to 5's position
Step 3: Delete 6 from right subtree (now has 0 or 1 child)
```

**Why Successor Works:**
- It's the smallest value > current node
- It has at most 1 right child (no left children by definition)
- Easy to remove

---

### Inorder Successor Finding (Critical)

**Definition:** Inorder successor of node = smallest value greater than node.value

**Algorithm:**
```
function findSuccessor(node):
    if node.right exists:
        return leftmost(node.right)    // min in right subtree
    else:
        // successor is ancestor where node is in left subtree
        return traverseUp(node)
```

**Example:**
```
Tree:        5
           /   \
          3     7
         / \   / \
        1   4 6   9

Successor of 4? → 5 (go right, find leftmost)
Successor of 5? → 6 (right exists, go to min)
Successor of 6? → 7 (go right, find leftmost)
Successor of 9? → null (rightmost node)
```

---

### Validating a BST (Common Mistake)

❌ **Wrong:** Check only left < parent < right at each node.

```
Invalid tree passes wrong check:
        10
       /  \
      5    15
          /  \
         6    20  ← 6 < 10, but in right subtree! Wrong.
```

✅ **Correct:** Use min/max bounds updated as you traverse.

```
function isBST(node, minVal, maxVal):
    if node is null: return true
    if node.value <= minVal or node.value >= maxVal: return false
    return isBST(node.left, minVal, node.value) and
           isBST(node.right, node.value, maxVal)

// Start: isBST(root, -∞, +∞)
```

---

## ⚖️ SECTION 4: BALANCED BSTs

### Why Balance? (The Crisis)

**Problem:** Sorted input [1, 2, 3, 4, 5] → degenerate BST → O(n) operations.

**Solution:** Maintain height ≈ log₂(n) via rotations.

**Impact (for 1M items):**
- Unbalanced: ~1M comparisons per operation
- Balanced: ~20 comparisons per operation
- **Difference: 50,000x faster**

---

### AVL Trees (Balance Factor)

**Balance Factor:** BF(node) = height(left) - height(right)

**Invariant:** |BF(node)| ≤ 1 at every node

**Height Guarantee:** h ≤ 1.44 * log₂(n) for n nodes (still O(log n))

**Rotations (4 Cases):**

| Case | Condition | Fix |
|---|---|---|
| **LL** | Left-left imbalance | Single right rotation |
| **RR** | Right-right imbalance | Single left rotation |
| **LR** | Left-right imbalance | Left rotate (child), right rotate (parent) |
| **RL** | Right-left imbalance | Right rotate (child), left rotate (parent) |

**Example: LL Case**
```
Before (BF=2):     3          After rotation:    2
                  /                             / \
                 2                             1   3
                /
               1

Single right rotation. Node 3 rotates right; node 2 becomes new root.
```

---

### Red-Black Trees (Production Standard)

**5 Rules:**
1. Every node is red or black
2. Root is black
3. All leaves (NIL) are black
4. If node is red, both children are black (no red-red)
5. Every path from node to NIL has same black-height

**Trade-off:**
- Less perfect balance than AVL
- **Fewer rotations** per insertion (better constant factors)
- Used by: Java TreeMap, C++ std::map, Linux kernel

**When to Choose:**
- **AVL:** Frequent searches, rare insertions (tighter balance helps)
- **Red-Black:** Balanced insert/delete load (fewer rotations = faster updates)

---

### Rotations (O(1) Operation)

**Why O(1)?** Just pointer changes; no data movement.

**Left Rotation Example:**
```
Before:          A              After:           B
                  \                            / \
                   B                          A   C
                    \
                     C

Pointers updated:
- A.right = B.left (if B.left exists)
- B.left = A
- Parent.child = B
```

---

## 🧩 SECTION 5: TREE PATTERNS

### Pattern 1: Path Sum (Backtracking)

**Problem:** Find all root-to-leaf paths with sum = target

**Template:**
```
function pathSum(node, target, currentPath, result):
    if node is null: return
    currentPath.add(node.value)
    
    if node is leaf and sum(currentPath) == target:
        result.add(copy of currentPath)
    
    pathSum(node.left, target, currentPath, result)
    pathSum(node.right, target, currentPath, result)
    
    currentPath.remove()  // BACKTRACK - CRITICAL
```

**Why Backtrack?** To explore ALL paths. Without remove(), only one path is recorded.

---

### Pattern 2: Tree Diameter (Return Two Values)

**Problem:** Find longest path between any two nodes

**Key Insight:** At each node, diameter = left_height + right_height

**Template:**
```
class Result:
    height, diameter

function dfs(node):
    if node is null:
        return Result(height=0, diameter=0)
    
    left = dfs(node.left)
    right = dfs(node.right)
    
    newHeight = 1 + max(left.height, right.height)
    newDiameter = max(left.diameter, right.diameter,
                      left.height + right.height)
    
    return Result(height=newHeight, diameter=newDiameter)
```

**Why Return Both?**
- Height needed for parent's calculation
- Diameter is the final answer
- Tracking both enables O(n) solution

---

### Pattern 3: Lowest Common Ancestor (LCA)

**Definition:** Deepest node that is ancestor of both target nodes

**Three Cases:**
```
function lca(node, p, q):
    if node is null or node == p or node == q:
        return node
    
    left = lca(node.left, p, q)
    right = lca(node.right, p, q)
    
    if left and right:
        return node           // p and q on different sides
    return left if left else right  // both on same side
```

**BST Shortcut:**
```
function lcaBST(node, p, q):
    if p.value < node.value and q.value < node.value:
        return lcaBST(node.left, p, q)
    if p.value > node.value and q.value > node.value:
        return lcaBST(node.right, p, q)
    return node  // p and q on different sides; node is LCA
```

---

### Pattern 4: Serialization/Deserialization

**Problem:** Encode tree uniquely; rebuild it

**Key:** **Null markers are essential.** Without them, ambiguous.

**Preorder Serialization:**
```
Tree:      1              Serialization: [1, 2, null, null, 3, null, null]
          / \
         2   3  (leaves)

Explanation:
- 1 (root)
- 2 (left child of 1)
- null (left child of 2)
- null (right child of 2)
- 3 (right child of 1)
- null (left child of 3)
- null (right child of 3)
```

**Deserialization (Queue-based):**
```
function deserialize(data):
    queue = queue(data)
    
    function buildTree():
        val = queue.pop()
        if val == "null":
            return null
        node = Node(val)
        node.left = buildTree()
        node.right = buildTree()
        return node
    
    return buildTree()

// Trace: [1, 2, null, null, 3, null, null]
// buildTree(): val=1, node=1, left=buildTree(), right=buildTree()
//   left: val=2, node=2, left=buildTree(), right=buildTree()
//     left: val=null, return null
//     right: val=null, return null
//   right: val=3, node=3, left=buildTree(), right=buildTree()
//     left: val=null, return null
//     right: val=null, return null
```

**Why Preorder?** Parent visited first; recursive construction matches preorder naturally.

---

## ➕ SECTION 6: AUGMENTED TREES (Advanced)

### Augmentation Concept

**Idea:** Store extra metadata at each node. Enable O(log n) specialized queries.

**Common Augmentations:**
- **Subtree size:** Count of nodes in subtree
- **Subtree sum:** Sum of values in subtree
- **Max/min:** Maximum or minimum in subtree

**Maintenance:** Update O(log n) per insertion/deletion (bottom-up from changed node).

---

### Order-Statistics Queries (Kth Smallest)

**Problem:** Find kth smallest element in O(log n)

**Solution:** Augment with subtree sizes.

**Algorithm:**
```
function kthSmallest(node, k):
    if node is null:
        return null
    
    leftSize = node.left.subtreeSize
    
    if k == leftSize + 1:
        return node.value
    else if k < leftSize + 1:
        return kthSmallest(node.left, k)
    else:
        return kthSmallest(node.right, k - leftSize - 1)
```

**Example:**
```
Tree with subtree sizes:
          5(7)
         /    \
       3(3)    8(3)
      /  \    / \
    1(1) 4(1) 6(1) 9(1)

Find 4th smallest:
- leftSize = 3
- k = 4, leftSize + 1 = 4
- Return node 5 (4th smallest is 5)

Find 6th smallest:
- At 5: leftSize = 3, k = 6
- k > leftSize + 1, go right with k' = 6 - 3 - 1 = 2
- At 8: leftSize = 1, k' = 2
- k' == leftSize + 1, return 8 (6th smallest is 8)
```

---

### Range Count Queries

**Problem:** Count elements in range [L, R] efficiently

**Solution:** Augment with subtree sizes; traverse tree using bounds

**Algorithm:**
```
function countInRange(node, L, R):
    if node is null:
        return 0
    
    count = 0
    if L <= node.value <= R:
        count = 1
    
    if L < node.value:
        count += countInRange(node.left, L, R)
    if node.value < R:
        count += countInRange(node.right, L, R)
    
    return count
```

---

## 📊 QUICK REFERENCE TABLES

### Complexity Summary

| Structure | Search | Insert | Delete | Space |
|---|---|---|---|---|
| **BST** | O(log n) avg, O(n) worst | O(log n) avg, O(n) worst | O(log n) avg, O(n) worst | O(n) |
| **AVL** | O(log n) | O(log n) | O(log n) | O(n) |
| **Red-Black** | O(log n) | O(log n) | O(log n) | O(n) |
| **Unbalanced** | O(n) | O(n) | O(n) | O(n) |

### When to Use Each Traversal

| Traversal | When | Example |
|---|---|---|
| **Preorder** | Need parent first | Copy tree, prefix expression |
| **Inorder** | Need sorted order | Print BST in order |
| **Postorder** | Need parent last | Delete tree, postfix expression |
| **Level-order** | Need layer-by-layer | Serialize, print by level |

---

## 🎓 CONCEPT VERIFICATION CHECKLIST

**Use this to self-test:**

### Definitions
- [ ] Height: Distance to furthest leaf (leaf = 0)
- [ ] Depth: Distance from root (root = 0)
- [ ] BST invariant: ALL left < node < ALL right (global)
- [ ] Balance factor (AVL): BF = h(left) - h(right)
- [ ] Red-black rule 4: No red-red adjacent nodes
- [ ] Inorder successor: Smallest value > current node

### Algorithms
- [ ] Preorder: Parent → Left → Right
- [ ] Inorder: Left → Parent → Right
- [ ] Postorder: Left → Right → Parent
- [ ] BST delete (2-child): Find successor, copy, delete successor
- [ ] LCA: Three cases—both left, both right, or split
- [ ] Path sum: Add → Recurse → Remove (backtrack)
- [ ] Diameter: Return (height, max_diameter)

### Trade-offs
- [ ] Why balance matters: O(log n) vs. O(n)
- [ ] AVL vs. Red-black: Tighter balance vs. fewer rotations
- [ ] Augmentation: Space for speed trade-off
- [ ] Preorder vs. Level-order serialization: Simplicity vs. layer structure

---

## 🚀 PRODUCTION FACTS

**Java TreeMap:** Red-black tree
- Why? Balanced insert/delete; fewer rotations than AVL

**Linux CFS Scheduler:** Red-black tree
- Why? O(log n) min-finding (leftmost) + efficient updates

**Databases:** B-trees (not binary trees)
- Why? Each node has 100+ children; minimizes disk I/O

**PostgreSQL:** B-tree indexes
- Why? Minimize disk I/O; each tree level = one disk access

---

**Status:** ✅ SUMMARY KEY CONCEPTS COMPLETE

