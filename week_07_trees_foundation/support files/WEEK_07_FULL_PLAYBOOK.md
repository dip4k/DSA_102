# 🌳 WEEK_07_FULL_PLAYBOOK.md

**Version:** v3.0 (Complete Curriculum Edition + Enhancements)  
**Status:** ✅ PRODUCTION-READY  
**Syllabus Alignment:** COMPLETE_SYLLABUS_v12_FINAL.md  
**Course Philosophy:** Narrative-First, MIT 6.006/6.046 Aligned, Pattern-Centric  
**Generated:** January 23, 2026  

---

## 📖 WEEK 7 ORIENTATION

### 🎯 Primary Goal

Understand tree structures, traversals, BST operations, and balanced BSTs conceptually and practically. Build strong mental models of how trees solve real-world problems, from maintaining sorted data to enabling fast ordered queries.

### 🌍 Why This Week Comes Here

Trees generalize linear structures into hierarchies. BSTs and balanced BSTs are central to ordered data processing. After mastering arrays, linked lists, and hash tables (Weeks 1-3), trees provide the next structural abstraction: how to organize data hierarchically while maintaining invariants that enable O(log n) operations. Trees are also the foundation for graphs (Week 8), which extend the same traversal patterns to more general structures.

### 📚 MIT 6.006 / 6.046 Alignment

- **6.006 Core:** Tree anatomy, traversals (preorder, inorder, postorder, level-order), BST operations (search, insert, delete), and balanced trees (AVL, red-black) for O(log n) guarantees.
- **6.046 Advanced:** Augmented trees, order-statistics trees, and range queries (Day 5 optional), demonstrating how to extend BSTs for more complex queries.

### 📊 Week 7 Structure

| **Day** | **Topic** | **Type** | **Learning Hours** | **Key Deliverable** |
|---------|----------|---------|-------------------|-------------------|
| Day 1 | Binary Trees & Traversals | Core ✅ | 3-4 hours | Trace all 4 traversals; code recursive + iterative |
| Day 2 | Binary Search Trees (BSTs) | Core ✅ | 4-5 hours | Code search, insert, delete (all 3 cases) |
| Day 3 | Balanced BSTs (AVL & Red-Black) | Core ✅ | 4-5 hours | Understand rotations; AVL vs Red-Black trade-offs |
| Day 4 | Tree Patterns (LCA, Diameter, Paths) | Core ✅ | 4-5 hours | Code diameter, LCA, path sum, serialization |
| Day 5 | Augmented Trees & Order-Statistics (Optional) | Advanced 🎓 | 3-4 hours | Design augmented trees; find kth smallest in O(log n) |

---

## 🗂️ COMPLETE WEEK 7 CURRICULUM

---

## 📅 DAY 1: BINARY TREES & TRAVERSALS

### 🎯 Learning Objectives

By end of Day 1, you should be able to:
- ✅ Draw and label trees with correct height and depth for every node
- ✅ Trace all 4 traversals (preorder, inorder, postorder, level-order) by hand
- ✅ Explain when and why to use each traversal order
- ✅ Code recursive and iterative traversals
- ✅ Analyze the relationship between tree height and node count (especially for balanced trees)
- ✅ Recognize full, complete, and balanced trees by sight
- ✅ Understand tree properties and their implications for algorithm complexity

#### 📚 Conceptual Foundation

##### 🌲 Tree Anatomy Essentials

A **tree** is a connected acyclic graph with a distinguished **root** node. Every node except the root has exactly one parent. This creates a hierarchy.

**Core Definitions:**

| Term | Definition | Example |
|------|-----------|---------|
| **Root** | Node with no parent (depth 0) | The topmost node in the tree |
| **Leaf** | Node with no children (height 0) | Bottommost nodes |
| **Parent/Child** | Directed relationship; each non-root has one parent | Node 5's children are 3 and 7 |
| **Subtree** | Node + all its descendants | Right subtree of root contains all right descendants |
| **Height (h)** | Longest path from node to a leaf; leaf has height 0 | Root of 5-node balanced tree has height ≈ 2 |
| **Depth (d)** | Distance from root to node; root has depth 0 | All nodes at level L have depth L |
| **Level** | Set of nodes at same depth | Level 0: just root; Level 1: root's children |
| **Ancestor/Descendant** | Path relationship (parent is ancestor of child) | Root is ancestor of all nodes |

**Critical Insight – Why Height Matters:**

For any tree operation (search, insert, delete):
- **Complexity = O(height)**
- Balanced tree: height ≈ log₂(n) → O(log n) per operation ✅
- Degenerate tree: height ≈ n → O(n) per operation ❌

**Example with 1 million nodes:**
- Balanced tree: height ≈ 20 → ~20 comparisons
- Degenerate tree: height ≈ 1,000,000 → ~1,000,000 comparisons

This is why **balance is non-negotiable** for performance.

---

##### 🌲 Tree Classifications

| Classification | Definition | Visual | Characteristics | Balance? |
|---|---|---|---|---|
| **Full (Proper)** | Every node has 0 or 2 children | Node with 2 kids, another with 0, none with 1 | No single-child nodes | Usually |
| **Complete** | All levels filled except possibly last; last level fills left-to-right | All slots packed left, no gaps on right | Used in heaps; compact layout | Guaranteed balanced |
| **Perfect (Full Complete)** | All levels completely filled; every leaf at same depth | Perfect pyramid shape | Rare in practice; theoretical ideal | Perfectly balanced |
| **Balanced** | Height ≈ log₂(n) for n nodes | Height constraint, not structure | AVL, red-black trees maintain this | By definition |
| **Degenerate** | Looks like a linked list; height ≈ n | Chain-like structure; very unbalanced | Result of sorted input into unbalanced BST | No |

**Visual Examples:**

```
Perfect Tree (height 2):        Complete Tree (height 2):      Degenerate (height 4):
        5                              5                              5
       / \                            / \                              \
      3   7                          3   7                              6
     / \                            /                                    \
    2   4                          2                                      7
                                                                          \
                                                                           8
Perfect: all levels full          Complete: last level         Degenerate: like linked list
8 nodes, h=2                       left-filled; 5 nodes         4 nodes, h=3
```

**Key Insight:** Complete and perfect trees guarantee O(log n) height. This is why we use balanced trees in practice.

---

##### 🔁 The Four Traversals

A **traversal** is an algorithm that visits every node exactly once in a specific order. Different orders are useful for different tasks.

| **Traversal** | **Visit Order** | **Recursive Pattern** | **Use Case** | **Key Property** |
|---|---|---|---|---|
| **Preorder** | Parent → Left → Right | Process, recurse left, recurse right | Copy tree, prefix notation, root-first analysis | Parent processed before children |
| **Inorder** | Left → Parent → Right | Recurse left, process, recurse right | **Sorted output from BST**, infix notation | For BST: yields ascending sequence |
| **Postorder** | Left → Right → Parent | Recurse left, recurse right, process | Delete tree (children first!), postfix notation | Children processed before parent |
| **Level-Order (BFS)** | All depth 0, then depth 1, etc. | Queue-based iteration, left-to-right | Tree width, layer analysis, connectivity | Breadth-first exploration |

**Why Each Matters:**

**Preorder Example – Copying a Tree:**
```
Original:        Copy:
    5      →        5
   / \             / \
  3   7    →      3   7
 /            →  /
2                2
```
We need **parent first** to create the parent node before copying children. Preorder: [5, 3, 2, 7].

**Inorder Example – Sorted Output from BST:**
```
BST:           Inorder traversal:
    5          Left → Parent → Right
   / \         (5):  [3, 2] → 5 → [7]
  3   7        = [2, 3, 5, 7]
 /
2
```
**Critical property:** Inorder traversal of ANY BST always produces ascending sequence. This is guaranteed by the BST invariant.

**Postorder Example – Deleting a Tree:**
```
Delete:            Postorder: Left → Right → Parent
    5              Delete 2, then 3, then 7, then 5
   / \             Safe because children deleted first
  3   7            (can't delete parent while children exist)
 /
2
```

**Level-Order Example – Width Analysis:**
```
Tree:          Level-order:
    1          Level 0: [1]
   / \         Level 1: [2, 3]
  2   3        Level 2: [4, 5, 6, 7]
 / \ / \       Useful for: tree width, BFS problems
4 5 6 7
```

---

##### 🧩 Recursive vs Iterative Traversals

**Recursive Traversals (Simple, Elegant):**

```python
# Preorder: Parent → Left → Right
def preorder(node):
    if node is None:
        return []
    return [node.val] + preorder(node.left) + preorder(node.right)

# Inorder: Left → Parent → Right
def inorder(node):
    if node is None:
        return []
    return inorder(node.left) + [node.val] + inorder(node.right)

# Postorder: Left → Right → Parent
def postorder(node):
    if node is None:
        return []
    return postorder(node.left) + postorder(node.right) + [node.val]

# Level-Order (BFS): Queue-based
def level_order(root):
    if root is None:
        return []
    result = []
    queue = [root]
    while queue:
        result.append(queue[0].val)
        node = queue.pop(0)
        if node.left:
            queue.append(node.left)
        if node.right:
            queue.append(node.right)
    return result
```

**Why Recursion Works:**
- Base case: null node → return
- Recursive case: process node, combine results from subtrees
- Return combines results in desired order

**Pros:** Clear, concise, matches tree structure  
**Cons:** O(height) stack space; stack overflow on deep trees

---

**Iterative Traversals (Space-Efficient, Stack Management):**

**Iterative Preorder with Stack:**
```python
def preorder_iterative(root):
    if not root:
        return []
    result = []
    stack = [root]
    while stack:
        node = stack.pop()
        result.append(node.val)  # Process before children
        # Push right first (so left pops first)
        if node.right:
            stack.append(node.right)
        if node.left:
            stack.append(node.left)
    return result
```

**Key Insight:** Stack is LIFO. To process left before right, push right first.

**Iterative Inorder with Stack (Most Important):**
```python
def inorder_iterative(root):
    result = []
    stack = []
    node = root
    while node or stack:
        # Go left as far as possible
        while node:
            stack.append(node)
            node = node.left
        # node is None; pop the last node
        node = stack.pop()
        result.append(node.val)  # Process
        # Move right; repeat
        node = node.right
    return result
```

**Pattern Essence:**
1. Go left until null
2. Pop and process
3. Go right
4. Repeat

This is the cleanest iterative pattern and most commonly asked in interviews.

**Iterative Postorder with Stack (Hardest):**
```python
def postorder_iterative(root):
    if not root:
        return []
    result = []
    stack = [root]
    prev = None  # Track previous node to know if right child done
    while stack:
        curr = stack[-1]
        # If no children or coming back from a child
        if curr.left is None and curr.right is None or prev is curr.left or prev is curr.right:
            result.append(curr.val)
            stack.pop()
            prev = curr
        else:
            # Push right then left (so left processes first)
            if curr.right:
                stack.append(curr.right)
            if curr.left:
                stack.append(curr.left)
    return result
```

**Difficulty:** Need to track when we've processed right child (postorder processes parent last).

---

##### 📈 Height Bounds and Tree Balance

**Height Formula:**
- **Leaf:** height(leaf) = 0
- **Internal node:** height(node) = 1 + max(height(left), height(right))

**Height Bounds for n Nodes:**

| Tree Type | Min Height | Max Height | Why |
|-----------|-----------|-----------|-----|
| **Any tree** | ⌈log₂(n+1)⌉ - 1 | n - 1 | Min: perfectly balanced; Max: degenerate (linked list) |
| **Complete tree** | ⌊log₂(n)⌋ | ⌊log₂(n)⌋ | Always balanced (by definition) |
| **Full tree** | ⌈log₂(n)⌋ | n - 1 | Depends on balance; full doesn't guarantee balance |
| **Perfect tree** | log₂(n+1) - 1 | log₂(n+1) - 1 | All leaves at same depth; perfectly balanced |

**Examples:**

```
100 nodes:
- Min height (balanced): ~6-7 comparisons
- Max height (degenerate): 99 comparisons
- 14x difference! This is why balance matters.

1 million nodes:
- Min height (balanced): ~20 comparisons
- Max height (degenerate): 1 million comparisons
- Massive difference in practice.
```

---

#### 🔧 Core Implementation: All Traversals

**Complete Implementation Reference:**

```python
class TreeNode:
    def __init__(self, val=0, left=None, right=None):
        self.val = val
        self.left = left
        self.right = right

# ============= RECURSIVE TRAVERSALS =============

def preorder_recursive(node):
    """Parent → Left → Right"""
    if node is None:
        return []
    return [node.val] + preorder_recursive(node.left) + preorder_recursive(node.right)

def inorder_recursive(node):
    """Left → Parent → Right"""
    if node is None:
        return []
    return inorder_recursive(node.left) + [node.val] + inorder_recursive(node.right)

def postorder_recursive(node):
    """Left → Right → Parent"""
    if node is None:
        return []
    return postorder_recursive(node.left) + postorder_recursive(node.right) + [node.val]

def level_order_bfs(root):
    """Breadth-first; all nodes at depth d before d+1"""
    if root is None:
        return []
    result = []
    queue = [root]
    while queue:
        result.append(queue[0].val)
        node = queue.pop(0)
        if node.left:
            queue.append(node.left)
        if node.right:
            queue.append(node.right)
    return result

# ============= ITERATIVE TRAVERSALS =============

def preorder_iterative(root):
    """Stack-based; process before children"""
    if not root:
        return []
    result = []
    stack = [root]
    while stack:
        node = stack.pop()
        result.append(node.val)
        if node.right:  # Push right first
            stack.append(node.right)
        if node.left:   # Push left second (pops first)
            stack.append(node.left)
    return result

def inorder_iterative(root):
    """Stack-based; go left, pop, go right"""
    result = []
    stack = []
    node = root
    while node or stack:
        while node:
            stack.append(node)
            node = node.left
        node = stack.pop()
        result.append(node.val)
        node = node.right
    return result

def postorder_iterative(root):
    """Stack-based; track previous to know if right done"""
    if not root:
        return []
    result = []
    stack = [root]
    prev = None
    while stack:
        curr = stack[-1]
        # Leaf or coming back from child
        if curr.left is None and curr.right is None or prev is curr.left or prev is curr.right:
            result.append(curr.val)
            stack.pop()
            prev = curr
        else:
            if curr.right:
                stack.append(curr.right)
            if curr.left:
                stack.append(curr.left)
    return result

def level_order_iterative(root):
    """Queue-based BFS (same as recursive version)"""
    if not root:
        return []
    result = []
    queue = [root]
    while queue:
        node = queue.pop(0)
        result.append(node.val)
        if node.left:
            queue.append(node.left)
        if node.right:
            queue.append(node.right)
    return result
```

---

##### 📋 Common Mistakes & Fixes

**❌ Mistake 1: Confusing height and depth, or off-by-one in height**

```python
WRONG: "Leaf has height 1"
CORRECT: "Leaf has height 0; height is longest path to a leaf"

WRONG: "Height of 3-node tree is 3"
CORRECT: "Height of a path of 3 nodes (1 → 2 → 3) is 2 (two edges)"
```

**Why:** Height is the number of edges, not nodes. It matters for understanding recursive formulas: `height(node) = 1 + max(height(left), height(right))`.

**Fix:** Always define: height(null) = -1 or height(leaf) = 0. Be consistent.

---

**❌ Mistake 2: Iterative traversals with stack – wrong order of pushing children**

```python
WRONG (for preorder):
    stack.push(left)
    stack.push(right)
    # This pops right first, processes right before left ❌

CORRECT:
    stack.push(right)  # Push right first
    stack.push(left)   # Push left second → pops first ✅
```

**Why:** Stack is LIFO. To process left before right, push right first.

**Fix:** Visualize the stack. Remember: LIFO = Last In, First Out.

---

**❌ Mistake 3: Forgetting that inorder on BST gives sorted output**

```
BST:       Inorder:
    5      Left → Parent → Right
   / \     (left subtree) → 5 → (right subtree)
  3   7    [1, 2, 3] → 5 → [6, 7]
 / \   \   = [1, 2, 3, 5, 6, 7] ✅ SORTED!
1   4   6
```

**Why:** BST invariant (left < parent < right) guarantees inorder produces sorted sequence.

**Fix:** Use this invariant in problems like "validate BST" (inorder should be ascending) and "kth smallest element" (inorder finds all in order).

---

**❌ Mistake 4: Level-order off-by-one or enqueuing children incorrectly**

```python
WRONG: Don't check if children exist
    for child in [node.left, node.right]:  # Could add None
        queue.append(child)  # Crashes later

CORRECT: Check before adding
    if node.left:
        queue.append(node.left)
    if node.right:
        queue.append(node.right)
```

**Fix:** Always null-check before enqueueing.

---

##### 📝 Hand-Tracing Exercise

**Given this tree:**
```
        5
       / \
      3   7
     / \   \
    2   4   8
   /
  1
```

**Trace all 4 traversals by hand:**

1. **Preorder (Parent → Left → Right):**
   - Visit 5 (root)
   - Left subtree: 3, then 2, then 1, then 4
   - Right subtree: 7, then 8
   - **Result: [5, 3, 2, 1, 4, 7, 8]**

2. **Inorder (Left → Parent → Right):**
   - Go left: 1, 2, 3, 4, 5, 7, 8
   - **Result: [1, 2, 3, 4, 5, 7, 8]** (sorted!)

3. **Postorder (Left → Right → Parent):**
   - Children first: 1, 2, 4, 3, 8, 7, 5
   - **Result: [1, 2, 4, 3, 8, 7, 5]**

4. **Level-Order (BFS):**
   - Depth 0: [5]
   - Depth 1: [3, 7]
   - Depth 2: [2, 4, 8]
   - Depth 3: [1]
   - **Result: [5, 3, 7, 2, 4, 8, 1]**

---

##### 🧪 Day 1 Checkpoint Quiz

1. **For a balanced binary tree with n nodes, what's the height?**
   - Answer: O(log n), specifically ⌈log₂(n)⌉

2. **Which traversal is used to get sorted output from a BST?**
   - Answer: Inorder (Left → Parent → Right)

3. **If a tree has 7 nodes in level-order sequence [A, B, C, D, E, F, G], draw the tree structure.**
   - Answer: A is root; B,C are children of A; D,E are children of B; F,G are children of C
   ```
         A
        / \
       B   C
      / \ / \
     D E F G
   ```

4. **Write iterative inorder without looking at notes. What's the key insight?**
   - Answer: "Go left until null, pop and process, go right"

5. **If a tree is degenerate (like a linked list) with 1 million nodes, estimate search time for a traversal.**
   - Answer: O(n) = 1 million operations; compare to balanced O(log n) ≈ 20 operations

---

##### 📚 Real-World Applications

**Expression Trees:**
```
Infix:     2 + 3 * 4
Preorder:  + 2 * 3 4    (prefix notation)
Postorder: 2 3 4 * +    (postfix notation)
```
Parsing: preorder for building, postorder for evaluation.

**File Systems:**
```
/
├── Documents/
│   ├── Resume.pdf
│   └── Cover.txt
└── Photos/
    ├── Vacation/
    └── Family/
```
Preorder: list files (parent first). Postorder: delete (children first).

**DOM Tree (HTML):**
Browsers traverse the DOM tree in level-order to render elements layer-by-layer.

---

#### ✅ Day 1 Completion Checklist

- [ ] Understand tree anatomy (root, leaf, height, depth, level)
- [ ] Can draw trees and label every node's height and depth
- [ ] Trace all 4 traversals on 3 different trees (one balanced, one left-skewed, one right-skewed)
- [ ] Code recursive versions of all 4 traversals
- [ ] Code iterative inorder cleanly (key interview pattern)
- [ ] Code iterative preorder (stack management practice)
- [ ] Understand why height determines operation complexity
- [ ] Know the use case for each traversal
- [ ] Pass Day 1 Checkpoint Quiz (5/5 correct)

---

---

## 📅 DAY 2: BINARY SEARCH TREES (BSTs)

### 🎯 Learning Objectives

By end of Day 2, you should be able to:
- ✅ State and explain the BST invariant (left < parent < right)
- ✅ Understand why the invariant enables O(log n) search in balanced BSTs
- ✅ Code search, insert, and delete (all 3 deletion cases)
- ✅ Find inorder successor (critical for deletion)
- ✅ Validate a BST using bounds (not just local checks)
- ✅ Recognize degenerate BSTs and their cost
- ✅ Understand insertion, deletion, and rebalancing implications

#### 📚 The BST Invariant

##### 🌳 Definition and Global Property

**BST Invariant (Global):** For any node in a BST:
- **All values in left subtree < node's value**
- **All values in right subtree > node's value**
- This applies recursively: the invariant holds for all subtrees

**Critical:** This is not a local property (just checking immediate children). It's a **global constraint** on the entire tree structure.

**Why This Matters:**
- Enables binary search: at each comparison, eliminate half the remaining tree
- In-order traversal produces sorted sequence (guaranteed)
- Average case: O(log n) search, insert, delete for balanced trees
- Worst case: O(n) if tree is degenerate (unbalanced)

**Example – Valid BST:**
```
        5
       / \
      3   7
     / \   \
    2   4   8
   /
  1

Check invariant:
- Left of 5: [1, 2, 3, 4] all < 5 ✅
- Right of 5: [7, 8] all > 5 ✅
- Left of 3: [1, 2] all < 3 ✅
- Right of 3: [4] all > 3 ✅
- And so on... ALL VALID ✅
```

**Example – Invalid BST:**
```
        5
       / \
      3   7
     /   /
    2   4  <- INVALID! 4 < 7 locally, but 4 < 5, so it violates global constraint

Check invariant:
- Left of 5: [2, 3] all < 5 ✅
- Right of 5: [4, 7] 
  - But 4 < 5! This violates the invariant ❌
```

---

##### 🔍 BST Operations: Search, Insert, Delete

**Search (Recursive):**

```python
def search(node, target):
    """Find target in BST; return True if found"""
    if node is None:
        return False
    
    if node.val == target:
        return True
    elif target < node.val:
        return search(node.left, target)  # Go left
    else:
        return search(node.right, target)  # Go right
```

**Complexity:**
- **Best case:** O(1) if target is root
- **Average case:** O(log n) for balanced tree
- **Worst case:** O(n) for degenerate tree

**Trace Example – Search 4:**
```
Tree:        5
            / \
           3   7
          / \
         2   4

Search 4:
- Is 4 == 5? No. 4 < 5? Yes → go left
- Is 4 == 3? No. 4 < 3? No → go right
- Is 4 == 4? Yes → return True ✅
Comparisons: 3 (3 nodes visited)
```

---

**Insert (Recursive):**

```python
def insert(node, val):
    """Insert val into BST; return root"""
    if node is None:
        return TreeNode(val)  # Create new node
    
    if val < node.val:
        node.left = insert(node.left, val)  # Go left
    else:
        node.right = insert(node.right, val)  # Go right (handles ==, depending on policy)
    
    return node
```

**Why Return Node?**
- We need to update the parent's pointer
- If we just modified left/right without returning, parent wouldn't see the change

**Trace Example – Insert [5, 3, 7, 2, 4]:**
```
Insert 5: [5]

Insert 3: 3 < 5 → left of 5
       5
      /
     3

Insert 7: 7 > 5 → right of 5
       5
      / \
     3   7

Insert 2: 2 < 5 → left; 2 < 3 → left of 3
       5
      / \
     3   7
    /
   2

Insert 4: 4 < 5 → left; 4 > 3 → right of 3
       5
      / \
     3   7
    / \
   2   4
```

---

**Delete (All Three Cases):**

Deletion is complex because it depends on how many children the node has.

| **Case** | **Condition** | **Action** | **Why Safe** |
|----------|--------------|-----------|-------------|
| **Leaf** | No children | Remove directly | No pointers to update |
| **One Child** | Has left OR right child | Replace node with its child | Child takes deleted node's place |
| **Two Children** | Has both children | Find successor, copy value, delete successor | Successor is smallest in right subtree; has ≤1 child |

```python
def delete(node, val):
    """Delete val from BST; return updated root"""
    if node is None:
        return None
    
    if val < node.val:
        node.left = delete(node.left, val)
    elif val > node.val:
        node.right = delete(node.right, val)
    else:  # Found node to delete
        # Case 1: No children (leaf)
        if node.left is None and node.right is None:
            return None
        
        # Case 2: Only right child
        if node.left is None:
            return node.right
        
        # Case 3: Only left child
        if node.right is None:
            return node.left
        
        # Case 4: Two children (hardest)
        # Find inorder successor (smallest in right subtree)
        successor = node.right
        while successor.left:
            successor = successor.left
        
        # Copy successor's value to current node
        node.val = successor.val
        
        # Delete the successor (now a duplicate; it has ≤ 1 child)
        node.right = delete(node.right, successor.val)
    
    return node
```

---

##### 🔑 Inorder Successor – Why It Matters

**Definition:** The **inorder successor** of a node is the next value in sorted order (the smallest value greater than the node's value).

**Key Property:** In a BST, the inorder successor is always the **leftmost node in the right subtree**.

**Why?**
- Right subtree contains all values > current node
- Leftmost in right subtree is the smallest among those > current node
- Therefore, it's the next value in sorted order

**Example:**
```
       5
      / \
     3   7  <- Inorder successor of 5 is 6
    / \   \
   2   4   8
                 (if tree was [... 5, 6, 7, ...])

Actually, this tree doesn't have 6, so successor of 5 is 7.

Another example:
       5
      / \
     3   8
    / \ / \
   2  4 6  9
        \
         7

Inorder successor of 5:
- Right subtree: [6, 7, 8, 9]
- Leftmost: 6
- Successor = 6 ✅
```

**Finding Successor in Code:**
```python
def find_successor(node):
    """Find inorder successor of node"""
    # Successor is in right subtree; go left until null
    current = node.right
    while current.left:
        current = current.left
    return current
```

**Why Successor Works for Deletion:**
1. Successor is the next value in sorted order
2. It's guaranteed to be in the right subtree (by definition)
3. It has **at most one child** (the right child; no left child)
4. Because it has ≤1 child, deleting it is easy (Cases 1-3)
5. So we simplify two-children deletion to a simpler case

---

##### ✅ BST Validation – Bounds Method (Not Local Checks)

**❌ WRONG Approach – Local checks only:**

```python
def is_valid_bst_wrong(node):
    """This doesn't work!"""
    if node is None:
        return True
    
    # Check only immediate children
    if node.left and node.left.val >= node.val:
        return False
    if node.right and node.right.val <= node.val:
        return False
    
    return is_valid_bst_wrong(node.left) and is_valid_bst_wrong(node.right)

# This FAILS on:
#       5
#      / \
#     3   7
#    /
#   2
#    \
#     4  <- INVALID! 4 should not be in left subtree of 5

# Locally, 4 < 7 ✅, 4 > 2 ✅, but 4 > 5, violating global invariant ❌
```

**✅ CORRECT Approach – Bounds (min/max propagated):**

```python
def is_valid_bst(node, min_val=float('-inf'), max_val=float('inf')):
    """Validate BST using bounds; global constraint check"""
    if node is None:
        return True
    
    # Check if current node violates bounds
    if node.val <= min_val or node.val >= max_val:
        return False
    
    # Recursively validate:
    # - Left subtree: all values in [min_val, node.val)
    # - Right subtree: all values in (node.val, max_val]
    return (is_valid_bst(node.left, min_val, node.val) and
            is_valid_bst(node.right, node.val, max_val))
```

**Key Insight:** At each level, tighten the bounds:
- Left child must be in range [min_val, node.val)
- Right child must be in range (node.val, max_val]

**Trace Example – Valid BST:**
```
       5  (valid if min=-∞, max=+∞)
      / \
     3   7  (3 valid if min=-∞, max=5; 7 valid if min=5, max=+∞)
    / \
   2   4  (2 valid if min=-∞, max=3; 4 valid if min=3, max=5)
  
  All nodes within bounds → Valid ✅
```

**Trace Example – Invalid BST:**
```
       5  (bounds: -∞, +∞)
      / \
     3   7  (3: -∞, 5; 7: 5, +∞)
    /     /
   2     4  (4: 5, +∞... but 4 < 5! ❌)

4 is NOT in range (5, +∞), so invalid ❌
```

---

##### 🎯 Degenerate BSTs and the Cost

**Problem:** If you insert sorted input into an unbalanced BST, you get a linked list.

**Example:**
```
Insert [1, 2, 3, 4, 5, 6, 7]:

1
 \
  2
   \
    3
     \
      4
       \
        5
         \
          6
           \
            7

Height = 6 (same as n-1)
Search for 7: must visit all 7 nodes → O(n) ❌
Compare to balanced tree: height ≈ 2.8, search ≈ 3 operations → O(log n) ✅

With 1 million sorted values:
- Degenerate: 1 million operations
- Balanced: ~20 operations
- 50,000x difference!
```

**This is why we need balancing (Week 3, Day 3).**

---

#### 🔧 Complete BST Implementation

```python
class TreeNode:
    def __init__(self, val=0, left=None, right=None):
        self.val = val
        self.left = left
        self.right = right

class BST:
    def __init__(self):
        self.root = None
    
    # ============= SEARCH =============
    def search(self, target):
        def helper(node):
            if node is None:
                return False
            if node.val == target:
                return True
            elif target < node.val:
                return helper(node.left)
            else:
                return helper(node.right)
        return helper(self.root)
    
    # ============= INSERT =============
    def insert(self, val):
        def helper(node):
            if node is None:
                return TreeNode(val)
            if val < node.val:
                node.left = helper(node.left)
            else:
                node.right = helper(node.right)
            return node
        self.root = helper(self.root)
    
    # ============= DELETE =============
    def delete(self, val):
        def helper(node):
            if node is None:
                return None
            
            if val < node.val:
                node.left = helper(node.left)
            elif val > node.val:
                node.right = helper(node.right)
            else:  # Found
                # Case 1: Leaf
                if node.left is None and node.right is None:
                    return None
                # Case 2: Only right
                if node.left is None:
                    return node.right
                # Case 3: Only left
                if node.right is None:
                    return node.left
                # Case 4: Two children
                successor = node.right
                while successor.left:
                    successor = successor.left
                node.val = successor.val
                node.right = helper(node.right)  # Delete successor
            
            return node
        self.root = helper(self.root)
    
    # ============= VALIDATE =============
    def is_valid(self):
        def helper(node, min_val, max_val):
            if node is None:
                return True
            if node.val <= min_val or node.val >= max_val:
                return False
            return (helper(node.left, min_val, node.val) and
                    helper(node.right, node.val, max_val))
        return helper(self.root, float('-inf'), float('inf'))
    
    # ============= INORDER (sorted) =============
    def inorder(self):
        result = []
        def helper(node):
            if node:
                helper(node.left)
                result.append(node.val)
                helper(node.right)
        helper(self.root)
        return result
```

---

##### 📋 Common Mistakes & Fixes

**❌ Mistake 1: Forgetting about duplicates**

```python
WRONG: Just insert all values (no duplicate handling)
CORRECT: Decide upfront on duplicate policy
  - Skip duplicates (don't insert)
  - Insert to the left: if val <= node.val
  - Insert to the right: if val < node.val
  - Count occurrences: store count at each node
```

**Fix:** In production, decide on duplicate handling and document it clearly.

---

**❌ Mistake 2: Deleting two-child node incorrectly**

```python
WRONG:
    node.val = node.left.val  # Just copy left child value
    node.left = node.left.left  # Delete left child
    # Problem: This breaks BST invariant!
    # Left subtree should contain values < node, not ==

CORRECT:
    successor = find_successor(node)  # Smallest in right subtree
    node.val = successor.val
    node.right = delete(node.right, successor.val)  # Delete successor
    # Why: Successor is next value in order; safe to copy
```

**Why Successor Matters:** Successor is guaranteed to have ≤ 1 child, making deletion simple.

---

**❌ Mistake 3: Validating locally instead of with bounds**

```python
WRONG: Only check immediate children
    if node.left.val < node.val and node.right.val > node.val:
        return True
    # Misses violations in deeper subtrees!

CORRECT: Propagate min/max bounds down the tree
    def is_valid(node, min_val, max_val):
        if node.val <= min_val or node.val >= max_val:
            return False
        return is_valid(node.left, min_val, node.val) and \
               is_valid(node.right, node.val, max_val)
```

---

**❌ Mistake 4: Not returning from recursive insert/delete**

```python
WRONG:
    def insert(node, val):
        if node is None:
            node = TreeNode(val)  # Creates local variable, parent doesn't see
        # ... rest of code
    # Parent's pointer not updated!

CORRECT:
    def insert(node, val):
        if node is None:
            return TreeNode(val)  # Return new node
        # ... recursive calls update pointers ...
        return node  # Return updated subtree
```

**Fix:** Always return the (possibly new) subtree from insert/delete.

---

##### 📝 Deletion Walkthrough

**Tree:**
```
          5
         / \
        3   7
       / \   \
      2   4   8
```

**Delete 3 (two children case):**

```
Step 1: Find successor (inorder next)
- Successor is leftmost in right subtree of 3
- Right subtree of 3: [4]
- Leftmost: 4
- Successor = 4

Step 2: Copy successor's value to node 3
- Node 3.val = 4
- Tree becomes:
        5
       / \
      4   7
     /     \
    2       8
(Note: duplicate 4, will delete next)

Step 3: Delete successor from right subtree
- Delete 4 from right subtree of node 4
- Node 4 has left child 2, no right child
- This is Case 3 (one child) → return node.left
- Result:
        5
       / \
      4   7
      /     \
     2       8
```

---

##### 🧪 Day 2 Checkpoint Quiz

1. **State the BST invariant in one sentence.**
   - Answer: All left descendants < node < all right descendants (globally, not just children)

2. **Why does inorder traversal of a BST produce sorted output?**
   - Answer: Inorder visits left subtree (smaller), then node, then right (larger); BST invariant guarantees order

3. **For a 100-node balanced BST, estimate search time.**
   - Answer: O(log 100) ≈ 7 comparisons

4. **In deletion with two children, why can we safely delete the successor?**
   - Answer: Successor has at most one child (the right child; no left by definition), so it's a simple deletion case

5. **What input causes BST degeneration?**
   - Answer: Sorted input [1, 2, 3, ...], which creates a linked-list-like structure with height n

---

##### 📚 Real-World Applications

**Ordered Data Management:**
- Maintaining leaderboards (sorted by score)
- Database indexes (B-trees, variants of BSTs)
- Priority queues (priority = sort key)

**Auto-Complete Systems:**
- Trie + BST: at each node, maintain BST of word suggestions
- Query: traverse trie, collect all words in subtrie using BST order

**File Systems:**
- inode structures sorted by inode number
- Directory entries sorted by name (case-insensitive BST)

---

#### ✅ Day 2 Completion Checklist

- [ ] Understand BST invariant (global property, not just local)
- [ ] Can explain why invariant enables O(log n) search
- [ ] Code search, insert cleanly
- [ ] Code delete for all 3 cases (leaf, one child, two children)
- [ ] Understand inorder successor and why it works
- [ ] Validate BST using bounds method (not local checks)
- [ ] Trace deletion with two children (multiple examples)
- [ ] Recognize degenerate BSTs from sorted input
- [ ] Pass Day 2 Checkpoint Quiz (5/5 correct)

---

---

## 📅 DAY 3: BALANCED BSTs – AVL & RED-BLACK OVERVIEW

### 🎯 Learning Objectives

By end of Day 3, you should be able to:
- ✅ Understand why balance matters (height bounds and O(log n) guarantees)
- ✅ Explain AVL balance factor and rotations (LL, RR, LR, RL)
- ✅ Explain red-black tree rules and why fewer rotations occur
- ✅ Compare AVL vs. red-black trade-offs (search-heavy vs. balanced)
- ✅ Name production systems using each
- ✅ Trace rebalancing after insertions
- ✅ Understand when each balancing strategy is preferred

#### 📚 Why Balance?

For a BST with n nodes:

| **Scenario** | **Height** | **Search Cost** | **100 Nodes** | **1M Nodes** |
|---|---|---|---|---|
| **Balanced** | ≈ log₂(n) | O(log n) | ≈ 7 ops | ≈ 20 ops |
| **Degenerate** | ≈ n | O(n) | ≈ 100 ops | ≈ 1M ops |
| **Difference** | - | - | 14x | 50,000x |

**Impact:** With 1 million nodes, balanced trees are 50,000 times faster!

**Balance Goal:** Maintain height ≈ log n through automatic rebalancing on insert/delete.

**Trade-off:** Rebalancing requires rotations, which take time. But the benefit (reduced search time) outweighs the rebalancing cost in most workloads.

---

#### 🔄 Rotations – The Core Rebalancing Tool

A **rotation** is a local restructuring that:
1. Maintains BST invariant (left < parent < right)
2. Reduces tree height
3. Takes O(1) time (just pointer changes)

**Right Rotation (LL Case):**

```
Before (Left-Left imbalance):    After (Right rotation):
      B                              A
     /                             / \
    A                             ?   B
   / \                               / \
  ?   ?                             ?   ?

What happens:
- A (left child) becomes new parent
- B becomes right child of A
- A's right subtree becomes B's left subtree
```

**Left Rotation (RR Case):**

```
Before (Right-Right imbalance):  After (Left rotation):
    A                              B
     \                            / \
      B                          A   ?
     / \                        / \
    ?   ?                      ?   ?

Mirror of right rotation
```

**Why Rotations Work:**
1. **O(1) time:** Just 4-5 pointer changes
2. **Preserve BST invariant:** If BST before, still BST after
3. **Reduce height:** Moves taller subtree closer to root

**Rotation Code:**
```python
def right_rotate(node):
    """Rotate right around node"""
    left_child = node.left
    node.left = left_child.right
    left_child.right = node
    return left_child  # New root

def left_rotate(node):
    """Rotate left around node"""
    right_child = node.right
    node.right = right_child.left
    right_child.left = node
    return right_child  # New root
```

---

#### 🌲 AVL Trees (Adelson-Velsky and Landis)

**AVL Invariant:** At every node, |height(left) - height(right)| ≤ 1

**Balance Factor (BF):** BF = height(left) - height(right)

**Valid BF ∈ {-1, 0, 1}**

**Height Guarantee:** Height ≤ 1.44 * log₂(n) for AVL (still O(log n), but with larger constant)

**Rebalancing Cases:**

| **Imbalance Type** | **Node BF** | **Child BF** | **Rotation(s)** | **When** |
|---|---|---|---|---|
| **LL (Left-Left)** | 2 | ≥ 0 | Single right | Left child heavy, balanced |
| **LR (Left-Right)** | 2 | < 0 | Left, then right | Left child heavy, right-skewed |
| **RR (Right-Right)** | -2 | ≤ 0 | Single left | Right child heavy, balanced |
| **RL (Right-Left)** | -2 | > 0 | Right, then left | Right child heavy, left-skewed |

**LL Imbalance Example:**
```
Before (BF = 2):           After right rotation:
      5                          3
     /                          / \
    3                          1   5
   /
  1

Why: Left child (3) is too heavy; rotate right to lift it
```

**LR Imbalance Example (Trickier):**
```
Before (BF = 2, but child has BF < 0):
      5
     /
    2
     \
      4

The issue: Inserting in right subtree of left child creates zig-zag
Solution: First left-rotate the left child, then right-rotate parent

Step 1 - Left rotate left child:
      5
     /
    4
   /
  2

Step 2 - Right rotate parent:
      4
     / \
    2   5

Result: Balanced ✅
```

**RR and RL:** Mirror images of LL and LR.

---

#### 🔴 Red-Black Trees (Production Standard)

**Why RB instead of AVL?**
- AVL maintains perfect balance (|BF| ≤ 1 always)
- Red-black allows slightly more imbalance (height ≤ 2 * log₂(n))
- Result: Fewer rotations on insert/delete → better constants in practice
- Trade-off: Slightly taller trees, but much simpler to maintain

**Red-Black Invariants (5 Rules):**

1. **Every node is red or black**
2. **Root is black**
3. **Leaves (NIL) are black**
4. **No two red nodes adjacent** (no red-red parent-child)
5. **Every path from node to leaves has same number of black nodes** (black-height property)

**Why These Rules Work:**

Rule 5 (black-height) ensures height ≤ 2 * log₂(n). The coloring prevents worst-case linear trees.

**Visual Example:**
```
       B(5)          <-- Black root
      /   \
    R(3)  B(7)       <-- Red children, Black children
   /  \    /  \
  B   B   R    B     <-- Mix of Red and Black
  1   4   6    8

Check rules:
1. All nodes colored ✓
2. Root is black ✓
3. All leaves (NIL) are black ✓
4. No R-R adjacent (red children have black parents) ✓
5. Black-height: every path has 2-3 black nodes (consistent) ✓
```

---

#### ⚖️ Comparison: AVL vs. Red-Black

| **Feature** | **AVL** | **Red-Black** |
|---|---|---|
| **Height bound** | ≤ 1.44 * log n | ≤ 2 * log n |
| **Balance check** | More frequent | Less frequent |
| **Rotations per insert** | More (strict balance) | Fewer (color flip often) |
| **Search complexity** | Faster (tighter bound) | Slightly slower (taller) |
| **Insert/delete** | More rebalancing work | Simpler (often just recolor) |
| **Use case** | Search-heavy workloads | Balanced insert/delete |
| **Production examples** | Some specialized systems | **Java TreeMap, Linux rbtree, C++ std::map** |
| **Code complexity** | Moderate | More complex (5 rules) |
| **Memory overhead** | height + BF per node | height + color per node |

**Real Production Systems:**

- **Java TreeMap/TreeSet:** Red-black trees
  - Balanced insert/delete is priority
  - Used in HashMap where collisions occur
  
- **Linux Kernel (CFS Scheduler):** Red-black trees
  - Maintain runqueue of processes
  - Need fast insert/delete (processes arrive/leave frequently)
  
- **C++ std::map, std::set:** Red-black trees
  - Standard library choice
  
- **PostgreSQL Indexes:** Variants of B-trees (generalization of balanced BSTs)
  - Multiple keys per node (reduces height)
  - Optimized for disk I/O
  
- **MySQL InnoDB:** B+ trees
  - Leaf nodes contain actual data
  - Non-leaf nodes are indexes

---

#### 📋 Common Mistakes & Fixes

**❌ Mistake 1: Confusing rotation direction**

```python
WRONG: "LL imbalance (left-heavy) → left rotate"
CORRECT: "LL imbalance (left-heavy) → right rotate to lift left child"

Mnemonic: Rotation opposes the imbalance direction
- Left-heavy (LL) → rotate right
- Right-heavy (RR) → rotate left
```

---

**❌ Mistake 2: Forgetting that balance is maintained during each operation**

```python
WRONG: "Insert into BST, then rebalance afterward"
CORRECT: "Insert, then check balance and rebalance on way back up recursion"

In code:
def insert(node, val):
    if node is None:
        return TreeNode(val)
    
    if val < node.val:
        node.left = insert(node.left, val)
    else:
        node.right = insert(node.right, val)
    
    # Check balance on return (bottom-up)
    bf = balance_factor(node)
    if bf > 1:  # Left-heavy
        # Rotate...
    if bf < -1:  # Right-heavy
        # Rotate...
    
    return node
```

---

**❌ Mistake 3: Not tracking black-height in red-black trees**

```python
WRONG: Just check colors locally
CORRECT: Validate every path from node to nil has same black-height

def is_valid_rbtree(node):
    _, valid = check_rbtree(node)
    return valid

def check_rbtree(node):
    if node is None:
        return (1, True)  # 1 black node (nil), valid
    
    if node.color == RED:
        if node.left and node.left.color == RED:
            return (0, False)  # Red-red violation
        if node.right and node.right.color == RED:
            return (0, False)
    
    left_height, left_valid = check_rbtree(node.left)
    right_height, right_valid = check_rbtree(node.right)
    
    if not (left_valid and right_valid):
        return (0, False)
    
    if left_height != right_height:
        return (0, False)  # Black-height violation
    
    new_height = left_height + (1 if node.color == BLACK else 0)
    return (new_height, True)
```

---

#### 🔄 Insertion with Rebalancing Example

**Inserting [5, 3, 7, 2, 4, 6, 8, 1] into AVL tree:**

```
Insert 5: [5]  BF(5) = 0 ✓

Insert 3: [5, 3]
    5
   /
  3
BF(5) = 1, BF(3) = 0 ✓

Insert 7: [5, 3, 7]
      5
     / \
    3   7
BF(5) = 0 ✓

Insert 2: [5, 3, 7, 2]
      5
     / \
    3   7
   /
  2
BF(5) = 1, BF(3) = 1 ✓

Insert 4: [5, 3, 7, 2, 4]
      5
     / \
    3   7
   / \
  2   4
BF(5) = 1, BF(3) = 0 ✓

Insert 6: [5, 3, 7, 2, 4, 6]
      5
     / \
    3   7
   / \  /
  2  4 6
BF(5) = 0 ✓

Insert 8: [5, 3, 7, 2, 4, 6, 8]
      5
     / \
    3   7
   / \ / \
  2  4 6  8
BF(5) = 0 ✓

Insert 1: [5, 3, 7, 2, 4, 6, 8, 1]
        5
       / \
      3   7
     / \ / \
    2  4 6  8
   /
  1
BF(3) = 2! (Height(left=2) - height(right=-1) = 2)
BF(2) = 1 ✓ so this is LL case → right rotate at 3

After rotation:
        5
       / \
      2   7
     / \ / \
    1  3 6  8
        \
         4
BF(5) = 0 ✓ All balanced!
```

---

#### 🧪 Day 3 Checkpoint Quiz

1. **Define AVL balance factor and the AVL invariant.**
   - Answer: BF = height(left) - height(right); |BF| ≤ 1 at every node

2. **Describe an LL imbalance and the fix.**
   - Answer: Left child is heavy; right rotate the parent to lift left child

3. **Why do red-black trees use fewer rotations than AVL?**
   - Answer: RB allows height up to 2 log n (vs. AVL 1.44 log n); RB often recolors instead of rotating

4. **Name a production system using red-black trees.**
   - Answer: Java TreeMap, Linux kernel, C++ std::map

5. **Compare search time: balanced tree vs. degenerate for 1 million nodes.**
   - Answer: Balanced ≈ 20 ops, degenerate ≈ 1 million ops (50,000x difference)

---

#### ✅ Day 3 Completion Checklist

- [ ] Understand why balance matters (height bounds)
- [ ] Know AVL balance factor and invariant
- [ ] Can identify all 4 rotation cases (LL, RR, LR, RL)
- [ ] Understand right and left rotations
- [ ] Know 5 red-black rules
- [ ] Understand why RB uses fewer rotations than AVL
- [ ] Compare AVL vs. RB trade-offs
- [ ] Name 3+ production systems using balanced trees
- [ ] Pass Day 3 Checkpoint Quiz (5/5 correct)

---

---

## 📅 DAY 4: TREE PATTERNS (CORE PROBLEM-SOLVING)

### 🎯 Learning Objectives

By end of Day 4, you should be able to:
- ✅ Solve path sum problems with backtracking
- ✅ Compute tree diameter efficiently
- ✅ Find lowest common ancestor (LCA)
- ✅ Serialize and deserialize trees
- ✅ Recognize pattern families in new problems
- ✅ Explain why each pattern works
- ✅ Combine patterns for complex problems

#### 📚 Pattern 1: Path Sum with Backtracking

**Problem:** Find all root-to-leaf paths that sum to a target value.

**Key Insight:** Use DFS + backtracking (add to path, recurse, remove from path).

**Why Backtrack?**
Because we need to explore **ALL paths**, not just one branch. After exploring left subtree, we undo (pop) to explore right subtree with a clean state.

```python
def path_sum_all(node, target, current_path, result):
    """Find all root-to-leaf paths summing to target"""
    if node is None:
        return
    
    # Add current node to path
    current_path.append(node.val)
    
    # Check if we're at a leaf and sum matches
    if node.left is None and node.right is None:
        if sum(current_path) == target:
            result.append(list(current_path))  # Copy path
    else:
        # Recurse to children
        path_sum_all(node.left, target, current_path, result)
        path_sum_all(node.right, target, current_path, result)
    
    # Backtrack: remove current node (undo choice)
    current_path.pop()

# Usage:
result = []
path_sum_all(root, target_sum, [], result)
print(result)
```

**Trace Example – Target = 7:**
```
Tree:        1
            / \
           2   3
          /
         4

Paths:
1 → 2 → 4 = 7 ✓
1 → 3 = 4 ✗

result = [[1, 2, 4]]
```

**Why Pop?**
```
Trace:
Visit 1: path=[1], recurse left and right
Visit 2: path=[1,2], recurse left
  Visit 4: path=[1,2,4], leaf, sum=7 ✓ add to result
  Pop 4: path=[1,2], ready to explore right (if exists)
Visit 3: path=[1,3], leaf, sum=4 ✗ don't add
Pop 1: done

Without pop:
Visit 1: path=[1]
Visit 2: path=[1,2]
Visit 4: path=[1,2,4]
(pop 4, but forgot! path still [1,2,4])
Visit 3: path=[1,2,4,3] (WRONG! 3 should follow 1, not 4)
```

**Complexity:** O(n) to visit all nodes; O(n) space for recursion + path storage.

**Variant – Count Paths Instead of Printing:**
```python
def count_paths(node, target, current_sum):
    """Count paths (not root-to-leaf) summing to target"""
    if node is None:
        return 0
    
    current_sum += node.val
    count = 0
    
    if current_sum == target:
        count += 1  # Found a path
    
    # Continue exploring (don't reset sum)
    count += count_paths(node.left, target, current_sum)
    count += count_paths(node.right, target, current_sum)
    
    return count
```

Note: This counts all paths (not just root-to-leaf), including partial paths.

---

#### 📚 Pattern 2: Tree Diameter

**Definition:** Longest path between ANY two nodes (not necessarily through root).

**Key Insight:** At each node, diameter is the maximum of:
1. Longest path passing through current node: left_height + right_height
2. Longest path in left subtree: diameter(left)
3. Longest path in right subtree: diameter(right)

```python
def tree_diameter(node):
    """Returns (height, max_diameter)"""
    if node is None:
        return (-1, 0)  # Height -1 for null, diameter 0
    
    left_height, left_diameter = tree_diameter(node.left)
    right_height, right_diameter = tree_diameter(node.right)
    
    # Diameter is either:
    # 1. Passing through current node
    # 2. In left subtree
    # 3. In right subtree
    current_diameter = max(
        left_height + right_height + 2,  # +2 for edges to children
        left_diameter,
        right_diameter
    )
    
    current_height = 1 + max(left_height, right_height)
    
    return (current_height, current_diameter)

# Usage:
height, diameter = tree_diameter(root)
print(diameter)  # Returns the diameter (longest path length)
```

**Why Return Both?**
- **Height** is needed by parent to compute its diameter
- **Diameter** is what we want for the answer

**Trace Example:**
```
Tree:           5
               / \
              3   7
             / \
            2   4

At node 2 (leaf):
  left_h = -1, right_h = -1
  diameter = max(-1 + -1 + 2, 0, 0) = 0
  height = 1 + (-1) = 0
  return (0, 0)

At node 3:
  left_h = 0 (from 2), right_h = 0 (from 4)
  diameter = max(0 + 0 + 2, 0, 0) = 2  (path: 2-3-4)
  height = 1 + 0 = 1
  return (1, 2)

At node 7 (leaf):
  height = 0, diameter = 0
  return (0, 0)

At node 5 (root):
  left_h = 1 (from 3), right_h = 0 (from 7)
  diameter = max(1 + 0 + 2, 2, 0) = 3  (path: 2-3-5-7)
  height = 1 + 1 = 2
  return (2, 3)

Final answer: diameter = 3 (path 2-3-5-7)
```

---

#### 📚 Pattern 3: Lowest Common Ancestor (LCA)

**Definition:** The deepest node that's an ancestor of both given nodes p and q.

**Key Insight – Three Cases:**
1. Both p and q are in left subtree → LCA is in left subtree
2. Both p and q are in right subtree → LCA is in right subtree
3. p in left, q in right (or vice versa) → LCA is current node

**For BST (Uses Ordering):**
```python
def lca_bst(node, p, q):
    """Find LCA in BST (uses BST property)"""
    if node is None:
        return None
    
    # If both p and q are less than current, LCA is in left
    if p.val < node.val and q.val < node.val:
        return lca_bst(node.left, p, q)
    
    # If both p and q are greater than current, LCA is in right
    if p.val > node.val and q.val > node.val:
        return lca_bst(node.right, p, q)
    
    # Otherwise, current node is LCA
    # (or one of p, q is equal to current node)
    return node
```

**Complexity:** O(height) = O(log n) for balanced tree

**For General Binary Tree (No Ordering):**
```python
def lca_general(node, p, q):
    """Find LCA in general tree"""
    if node is None:
        return None
    
    # If current node is p or q, it might be LCA
    if node == p or node == q:
        return node
    
    # Search in left and right
    left_lca = lca_general(node.left, p, q)
    right_lca = lca_general(node.right, p, q)
    
    # If both found → current is LCA
    if left_lca and right_lca:
        return node
    
    # If one found → it's the LCA
    return left_lca if left_lca else right_lca
```

**Complexity:** O(n) worst case (visit all nodes)

**Trace Example – General Tree:**
```
Tree:           5
               / \
              3   7
             / \
            2   4

Find LCA(2, 4):
- At 5: left_lca = lca(3, 2, 4), right_lca = lca(7, 2, 4)
  - lca(3, 2, 4):
    - node=3, p=2, q=4, not equal
    - left_lca = lca(2, 2, 4) = 2
    - right_lca = lca(4, 2, 4) = 4
    - Both found! Return 3 ✓
  - right_lca = None
- At 5: left_lca=3, right_lca=None
- Return left_lca = 3 ✓

Result: LCA(2, 4) = 3
```

---

#### 📚 Pattern 4: Serialization & Deserialization

**Idea:** Encode tree to string/list, then rebuild. Requires structural information (null markers).

**Preorder with Nulls:**
```python
def serialize_preorder(node):
    """Serialize using preorder + nulls"""
    def helper(node):
        if node is None:
            return ['null']
        return [str(node.val)] + helper(node.left) + helper(node.right)
    
    return ','.join(helper(node))

def deserialize_preorder(data):
    """Deserialize from preorder + nulls"""
    def helper(tokens):
        val = tokens.pop(0)
        if val == 'null':
            return None
        node = TreeNode(int(val))
        node.left = helper(tokens)
        node.right = helper(tokens)
        return node
    
    tokens = data.split(',')
    return helper(tokens)

# Example:
#       1
#      / \
#     2   3
# 
# Serialized: "1,2,null,null,3,null,null"
# Deserialized: Same tree
```

**Why Preorder?** Preorder visits parent first, which makes deserialization straightforward.

**Level-Order (BFS) Variant:**
```python
from collections import deque

def serialize_levelorder(node):
    """Serialize using level-order (BFS)"""
    if not node:
        return "null"
    result = []
    queue = deque([node])
    while queue:
        node = queue.popleft()
        if node:
            result.append(str(node.val))
            queue.append(node.left)
            queue.append(node.right)
        else:
            result.append("null")
    return ",".join(result)

def deserialize_levelorder(data):
    """Deserialize from level-order"""
    if data == "null":
        return None
    values = data.split(",")
    root = TreeNode(int(values[0]))
    queue = deque([root])
    i = 1
    while queue:
        node = queue.popleft()
        if i < len(values) and values[i] != "null":
            node.left = TreeNode(int(values[i]))
            queue.append(node.left)
        i += 1
        if i < len(values) and values[i] != "null":
            node.right = TreeNode(int(values[i]))
            queue.append(node.right)
        i += 1
    return root

# Example:
#       1
#      / \
#     2   3
# 
# Serialized: "1,2,3,null,null,null,null"
# Deserialized: Same tree
```

**Why Include Nulls?**
```
Without nulls:
Tree 1:     1
           / \
          2   3

Serialized: "1,2,3"

Tree 2:     1
             \
              2
             /
            3

Serialized: "1,2,3"

SAME STRING! Can't distinguish → ambiguous ❌

With nulls:
Tree 1: "1,2,null,null,3,null,null"
Tree 2: "1,null,2,3,null,null,null"

Different strings → unambiguous ✓
```

---

##### 📋 Common Mistakes & Fixes

**❌ Mistake 1: Forgetting to backtrack in path sum**

```python
WRONG:
def path_sum(node, target, current_path, result):
    if node is None:
        return
    current_path.append(node.val)
    if node.left is None and node.right is None:
        if sum(current_path) == target:
            result.append(list(current_path))
    path_sum(node.left, target, current_path, result)
    path_sum(node.right, target, current_path, result)
    # FORGOT TO POP!

CORRECT:
def path_sum(node, target, current_path, result):
    if node is None:
        return
    current_path.append(node.val)
    if node.left is None and node.right is None:
        if sum(current_path) == target:
            result.append(list(current_path))
    else:
        path_sum(node.left, target, current_path, result)
        path_sum(node.right, target, current_path, result)
    current_path.pop()  # BACKTRACK!
```

**Consequence:** Explores only one branch instead of all paths.

---

**❌ Mistake 2: Computing diameter as just height sum**

```python
WRONG:
def diameter(node):
    if node is None:
        return 0
    left_h = height(node.left)
    right_h = height(node.right)
    return left_h + right_h  # INCOMPLETE!

Problem: Doesn't check if diameter is entirely in a subtree

Correct:
def diameter(node):
    if node is None:
        return (-1, 0)  # (height, diameter)
    # ... recursively compute both ...
    return (height, max(...))
```

---

**❌ Mistake 3: Assuming null markers are obvious**

```python
WRONG:
def serialize(node):
    if node is None:
        return ""
    return str(node.val) + "," + serialize(node.left) + "," + serialize(node.right)

# Result: "1,2,3" (lost structure information)

CORRECT:
def serialize(node):
    if node is None:
        return "null"
    return str(node.val) + "," + serialize(node.left) + "," + serialize(node.right)

# Result: "1,2,null,null,3,null,null" (structure preserved)
```

---

#### 🧪 Day 4 Checkpoint Quiz

1. **Write the backtracking pattern for path sum in one minute.**
   - Answer: add node, recurse left/right, pop node

2. **Why does diameter return both height and max_diameter?**
   - Answer: height for parent's diameter calculation; diameter for answer

3. **In LCA, when is the current node the answer?**
   - Answer: When p and q are on different sides of current, or one of them is current

4. **Why include null markers in serialization?**
   - Answer: Without them, multiple trees serialize identically (ambiguous)

5. **Can you serialize with level-order instead of preorder?**
   - Answer: Yes, but deserialization is trickier (requires queue + indexing)

---

#### ✅ Day 4 Completion Checklist

- [ ] Code path sum with backtracking (no notes)
- [ ] Code diameter returning (height, max_diameter) (no notes)
- [ ] Find LCA in BST using the shortcut (no notes)
- [ ] Find LCA in general tree using 3-case logic (no notes)
- [ ] Serialize and deserialize with nulls (no notes)
- [ ] Understand why backtracking is needed
- [ ] Understand why diameter needs both values
- [ ] Pass Day 4 Checkpoint Quiz (5/5 correct)

---

---

## 📅 DAY 5: AUGMENTED TREES & ORDER-STATISTICS (OPTIONAL, ADVANCED)

### 🎯 Learning Objectives (Advanced – 6.046 Flavor)

By end of Day 5, you should be able to:
- ✅ Augment BSTs with metadata (subtree size, sum, count)
- ✅ Answer kth smallest queries in O(log n)
- ✅ Count elements in range [L, R] efficiently
- ✅ Maintain augmentation invariant during insertions/deletions
- ✅ Design augmented tree for custom queries
- ✅ Understand when augmentation is worthwhile
- ✅ Extend tree operations with augmented data

#### 📚 Augmentation Fundamentals

**Idea:** Store extra information at each node to enable efficient queries.

**Common Augmentations:**

| **Augmentation** | **Stored** | **Enables** | **Complexity** |
|---|---|---|---|
| **Subtree size** | Count of nodes in subtree | Find kth smallest; count in range | O(log n) |
| **Subtree sum** | Sum of all values in subtree | Range sum queries | O(log n) |
| **Min/Max in subtree** | Minimum/maximum in subtree | Range min/max queries | O(log n) |
| **Count by property** | Count of nodes with certain property | Filtering + counting | O(log n) |

**Invariant:** If we update a node, we must propagate updates up the tree (O(height) = O(log n) for balanced tree).

---

#### 🔧 Order-Statistics Tree with Subtree Sizes

**Goal:** Find kth smallest element in O(log n).

**Augmentation:** Store subtree_size at each node.

```python
class AVLNode:
    def __init__(self, val):
        self.val = val
        self.left = None
        self.right = None
        self.size = 1  # Number of nodes in this subtree (including self)

def kth_smallest(root, k):
    """Find the kth smallest value (1-indexed)"""
    def helper(node):
        if node is None:
            return None
        
        left_size = node.left.size if node.left else 0
        
        if k == left_size + 1:
            # Current node is kth smallest
            return node
        elif k <= left_size:
            # kth is in left subtree
            return helper(node.left)
        else:
            # kth is in right subtree; adjust k
            k -= (left_size + 1)
            return helper(node.right)
    
    return helper(root).val if helper(root) else None
```

**Intuition:**
- If left subtree has L nodes, current node is (L+1)-th smallest
- If k ≤ L: kth smallest is in left subtree
- If k = L+1: kth smallest is current node
- If k > L+1: kth smallest is in right subtree at position (k - L - 1)

**Example:**
```
Tree:           5
               / \
              3   7
             / \   \
            2   4   8
(Inorder: [2, 3, 4, 5, 7, 8])

Sizes:      5(6)
           /    \
          3(3)  7(2)
         / \     \
        2(1)4(1) 8(1)

Find 4th smallest:
- At 5: left_size=3
  - 4 > 3+1? No, 4 > 3? Yes
  - k = 4 - 3 - 1 = 0... wait, that's wrong.
  
Actually:
- At 5: left_size=3, k=4
  - Is k == 3+1? Yes! Return 5 ✓

Find 2nd smallest:
- At 5: left_size=3, k=2
  - Is k <= 3? Yes, go left
  - At 3: left_size=1, k=2
    - Is k == 1+1? Yes! Return 3 ✓
```

**Complexity:** O(log n) search; O(log n) insertion (with size updates).

---

#### 🔧 Range Count Queries

**Goal:** Count elements in range [L, R] using augmented tree.

```python
def count_in_range(root, L, R):
    """Count elements in range [L, R]"""
    
    def count_up_to(node, val):
        """Count elements <= val"""
        if node is None:
            return 0
        
        if node.val <= val:
            # Current node and all left subtree are <= val
            left_count = node.left.size if node.left else 0
            return 1 + left_count + count_up_to(node.right, val)
        else:
            # Only left subtree might be <= val
            return count_up_to(node.left, val)
    
    # Count [L, R] = count(<= R) - count(< L)
    return count_up_to(root, R) - count_up_to(root, L - 1)
```

**Intuition:**
- count_up_to(R): count all elements ≤ R
- count_up_to(L-1): count all elements ≤ L-1 (i.e., < L)
- Range [L, R] = count(≤ R) - count(< L)

**Example:**
```
Tree (BST):     5
               / \
              3   7
             / \   \
            2   4   8

Count in range [3, 7]:

count_up_to(5, 7):
- At 5: 5 <= 7? Yes
  - left_count = 2 (nodes 2, 3)
  - return 1 + 2 + count_up_to(7, 7)
    - At 7: 7 <= 7? Yes
      - left_count = 0
      - return 1 + 0 + count_up_to(8, 7)
        - At 8: 8 <= 7? No
          - return count_up_to(nil, 7) = 0
      - return 1
  - return 1 + 2 + 1 = 4
  - Nodes: [2, 3, 5, 7]

count_up_to(5, 2):
- At 5: 5 <= 2? No
  - return count_up_to(3, 2)
    - At 3: 3 <= 2? No
      - return count_up_to(2, 2)
        - At 2: 2 <= 2? Yes
          - left_count = 0
          - return 1 + 0 + count_up_to(nil, 2) = 1
      - return 1
  - return 1
  - Nodes: [2]

count_in_range(5, 7) = 4 - 1 = 3
Nodes in range: [3, 5, 7] ✓
```

**Complexity:** O(log n) per query.

---

#### 📋 Trade-offs: When to Augment

| **Factor** | **Augment?** | **Why** |
|---|---|---|
| **Frequent queries, rare updates** | ✅ YES | Precompute at query time via augmentation |
| **Frequent updates, rare queries** | ❌ NO | Update overhead outweighs query benefit |
| **Balanced workload** | ⚖️ MAYBE | Depends on constants and specific workload |
| **Simple augmentation (size, sum)** | ✅ YES | Low overhead; standard in practice |
| **Complex augmentation** | ❌ MAYBE | More complex, more updates, harder to maintain |
| **Cache-friendly access** | ✅ YES | Reduces cache misses vs. external data structure |

**Real-World Decision:**
```
Scenario 1: Database index
- Many reads, few writes
- Augment: YES (kth smallest, range count help query performance)

Scenario 2: Real-time stock prices
- Constant updates, occasional queries
- Augment: NO (updates bottleneck; simpler BST sufficient)

Scenario 3: Leaderboard (competitive gaming)
- Balanced read/write
- Augment: MAYBE (depends on workload specifics)
```

---

#### 🌳 Augmentation Examples in Real Systems

**Databases:**
- **B-tree variants** with aggregate data (min, max, count in subtree)
- **Range indexes** for efficient range queries

**Competitive Programming:**
- **Policy-based data structures (C++ PBDS)** for order-statistics
- **Segment trees** (specialized augmented trees for ranges)

**Game Engines & Graphics:**
- **KD-trees** (augmented with bounding boxes for range queries)
- **Quadtrees/Octrees** (spatial augmented trees)

**Machine Learning:**
- **Decision trees** augmented with split information
- **Neural tree networks** with learned augmentations

---

#### 🧪 Day 5 Checkpoint Quiz

1. **Define the augmentation invariant.**
   - Answer: If you modify a node's data, update all ancestors

2. **How does subtree_size enable kth smallest in O(log n)?**
   - Answer: At each node, check if k <= left_size; if yes, recurse left; else adjust k and recurse right

3. **When should you augment a tree?**
   - Answer: When queries are frequent and updates are rare; simple augmentation overhead is worth query savings

4. **Design an augmentation to count elements in range [L, R].**
   - Answer: Augment with subtree_size; count(<= R) - count(<= L-1)

5. **Can you augment a BST without balancing?**
   - Answer: Technically yes, but queries become O(n) worst case on degenerate trees; balance is essential for O(log n) guarantee

---

#### ✅ Day 5 Completion Checklist

- [ ] Understand augmentation fundamental concept
- [ ] Implement subtree_size augmentation
- [ ] Code kth smallest using sizes (no notes)
- [ ] Code range count queries (no notes)
- [ ] Understand augmentation invariant
- [ ] Know when to augment (trade-offs)
- [ ] Can design custom augmentations
- [ ] Pass Day 5 Checkpoint Quiz (5/5 correct)

---

---

## 🔄 WEEKLY SYNTHESIS & INTEGRATION

### 📊 Week 7 Concept Map

```
TREES (Foundation)
├── Anatomy (Structure)
│   ├── Root, parent, child, leaf
│   ├── Height (longest path to leaf)
│   ├── Depth (distance from root)
│   ├── Tree types: full, complete, perfect, balanced, degenerate
│   └── Height bounds: O(n) worst, O(log n) balanced
│
├── Traversals (Exploration Orders)
│   ├── Preorder (parent first) → copy tree, prefix notation
│   ├── Inorder (middle) → **sorted in BST**
│   ├── Postorder (children first) → delete tree, postfix
│   └── Level-order (BFS) → tree width, layer analysis
│
├── Binary Search Trees (BSTs) (Ordered Structure)
│   ├── Invariant: left < parent < right (global!)
│   ├── Operations: search, insert, delete
│   ├── Deletion cases: leaf, one child, two children (successor)
│   ├── Validation: bounds method
│   ├── Inorder gives sorted sequence
│   └── Problem: degenerates with sorted input
│
├── Balanced BSTs (Performance Guarantee)
│   ├── AVL Trees
│   │   ├── Balance factor constraint: |BF| ≤ 1
│   │   ├── Height: ≤ 1.44 log n
│   │   ├── Rotations: LL, RR, LR, RL
│   │   ├── More rotations (strict balance)
│   │   └── Use case: search-heavy workloads
│   │
│   └── Red-Black Trees
│       ├── 5 coloring rules
│       ├── Height: ≤ 2 log n
│       ├── Fewer rotations (recoloring often)
│       ├── Production standard
│       └── Use case: balanced insert/delete
│
├── Tree Patterns (Problem-Solving)
│   ├── Path Sum (DFS + backtracking)
│   │   └── Add, recurse, pop pattern
│   ├── Diameter (return height + diameter)
│   │   └── Max of: left_h + right_h, left_d, right_d
│   ├── LCA (three cases: left, right, split)
│   │   └── For BST: use ordering; for general: find split point
│   ├── Serialization (preorder + nulls)
│   │   └── Nulls essential for structure
│   └── Combined patterns (diameter + count, LCA + rank)
│
└── Augmentation (Advanced 6.046)
    ├── Metadata at each node
    ├── Order-statistics: kth smallest in O(log n)
    ├── Range queries: count in [L, R]
    └── Trade-off: update cost vs. query speedup

KEY INSIGHTS:
1. Height determines operation time: O(height)
2. Balance ensures height ≈ log n
3. Inorder traversal of BST = sorted sequence
4. Successor for deletion has ≤ 1 child (safe)
5. Rotations are O(1); they maintain BST invariant
6. AVL: perfect balance; RB: fewer rotations
7. Backtracking explores ALL paths
8. Null markers essential for serialization
9. Augmentation trades space for query time
```

---

### 🎯 Learning Progression

| **Phase** | **Day** | **Focus** | **Outcome** | **Readiness Check** |
|---|---|---|---|---|
| **Foundation** | Day 1 | Tree structure, anatomy, traversals | Understand how to explore trees in different orders | Trace 4 traversals on 3 trees |
| **Core Operations** | Day 2 | BST invariant, operations, validation | Implement search, insert, delete; understand invariant | Code BST ops; pass Quiz |
| **Scalability** | Day 3 | Balancing, rotations, production systems | Ensure O(log n) height via automatic balancing | Compare AVL/RB; name systems |
| **Problem-Solving** | Day 4 | Pattern recognition, complex problems | Solve diverse tree problems using patterns | Code 4 patterns; detect new problems |
| **Mastery** | Day 5 | Augmentation, advanced queries | Extend trees for specialized queries | Design augmented tree |

---

### 📋 Interview Readiness Checklist

#### ✅ Core Knowledge (Days 1-4)

- [ ] Draw tree with height/depth for every node (no notes, < 2 min)
- [ ] Trace all 4 traversals on a 10-node tree (no notes, < 3 min)
- [ ] Code BST search, insert, delete (all 3 cases) in < 10 min
- [ ] Find inorder successor; explain why it works
- [ ] Validate BST using bounds method (not just local checks)
- [ ] Explain AVL rotations: LL, RR, LR, RL (draw and rotate)
- [ ] Explain red-black invariants (5 rules)
- [ ] Code tree diameter (return both height and diameter)
- [ ] Code LCA for both BST and general trees
- [ ] Code path sum with backtracking (understand why backtrack)
- [ ] Code serialization + deserialization (preorder or level-order)
- [ ] Identify pattern in new problem (path sum? diameter? LCA?)

#### 📚 Production Knowledge

- [ ] Name 3+ production systems using BSTs/trees
- [ ] Explain trade-offs: AVL vs. red-black
- [ ] Discuss when to augment trees and what to augment
- [ ] Explain real-world use cases: databases, schedulers, graphics

#### 💡 Problem-Solving Habits

- [ ] For any tree problem, ask: "inorder sorted? backtracking needed? diameter/LCA pattern?"
- [ ] Always validate BST using bounds, not just local checks
- [ ] Recognize when iterative traversal is needed (large trees, stack limits)
- [ ] Test edge cases: single node, null root, degenerate tree

---

### ✅ You're Ready to Move to Week 8 When:

1. ✅ All daily checklists are complete (even optional Day 5)
2. ✅ You scored 4+ on most rubric categories
3. ✅ You can code all major operations without notes
4. ✅ You explained a tree concept aloud (to friend, recording, or mock interviewer) without major gaps
5. ✅ You feel confident recognizing patterns in new problems
6. ✅ You understand production systems and trade-offs

---

### ⚠️ If Not Ready, Spend Extra Time On:

- **Weakest topic:** Review that day's checklist, re-code 2-3 problems
- **Interview questions:** Do 10+ more from problem ladder
- **Hard problems:** Solve 2-3 hard problems from roadmap

---

## 🎓 WEEK 7 PRACTICE LADDER

### Tier 1: Fundamentals (Day 1-2 Material)

1. **Binary Tree Inorder Traversal** (LC 94) – Recursive + iterative
2. **Same Tree** (LC 100) – Simple tree comparison
3. **Binary Tree Level Order Traversal** (LC 102) – BFS practice
4. **Search in a Binary Search Tree** (LC 700) – BST search
5. **Insert into a Binary Search Tree** (LC 701) – BST insert

### Tier 2: BST Operations (Day 2-3 Material)

6. **Delete Node in a BST** (LC 450) – All 3 deletion cases
7. **Validate Binary Search Tree** (LC 98) – Bounds validation
8. **Kth Smallest Element in a BST** (LC 230) – Inorder + early exit
9. **Lowest Common Ancestor of a BST** (LC 235) – BST property shortcut
10. **Balanced Binary Tree** (LC 110) – Check AVL-like balance

### Tier 3: Tree Patterns (Day 4 Material)

11. **Path Sum** (LC 112) – Root-to-leaf with backtracking
12. **Path Sum II** (LC 113) – All root-to-leaf paths
13. **Diameter of Binary Tree** (LC 543) – Return height + diameter
14. **Lowest Common Ancestor of a Binary Tree** (LC 236) – General tree LCA
15. **Serialize and Deserialize Binary Tree** (LC 297) – Preorder + nulls
16. **Binary Tree Maximum Path Sum** (LC 124) – Complex diameter variant

### Tier 4: Advanced Patterns & Augmentation (Day 5 Material)

17. **Kth Smallest Element with Augmentation** (LC 230 variant) – Use size augmentation
18. **Count of Range Sum** (LC 327) – Prefix sum + augmentation concept
19. **Binary Search Tree Iterator** (LC 173) – Augmented, in-order on demand
20. **Vertical Order Traversal** (LC 987) – Combining position + traversal

---

## 📚 RECOMMENDED LEARNING RESOURCES

### 📖 Textbooks & References

| **Resource** | **Best For** | **How to Use** |
|---|---|---|
| **CLRS (Intro to Algorithms), Chapters 12-13** | Formal foundations (proofs, complexity analysis) | Read Chapters 12-13 (BST, RB-trees) after coding patterns |
| **MIT 6.006 Lecture Notes** | MIT-aligned depth (exactly matches curriculum) | Watch Lectures 6-7 (trees); read notes; solve problems |
| **CP-Algorithms (Tree Algorithms)** | Pattern recognition and tricks | Reference for specific patterns (LCA binary lifting, etc.) |

### 🎥 Video Resources

| **Resource** | **Best For** | **How to Use** |
|---|---|---|
| **MIT OpenCourseWare 6.006 (Lectures 6-7)** | Foundational understanding with proofs | Watch before coding; pause and trace examples yourself |
| **Abdul Bari (YouTube) – Binary Trees & BSTs** | Clear, step-by-step walkthroughs | Watch for rotations, deletion cases, traversals |
| **Errichto (Codeforces) – Tree Problems** | Advanced problem-solving techniques | Study for pattern recognition in hard problems |

### 💻 Interactive Platforms

| **Resource** | **Best For** | **How to Use** |
|---|---|---|
| **LeetCode Tree Tag** | Practice problems with solutions | Solve Tier 1-4 in order; review solutions |
| **GeeksforGeeks Trees** | Quick reference + code templates | Reference for specific algorithms |
| **VisuAlgo.net** | Visualizing tree operations | Use for insertions, deletions, rotations, traversals |

---

## 🧪 SELF-ASSESSMENT RUBRIC

### Rate Yourself 1-5 on Each Category

| **Category** | **1 (Lost)** | **2 (Struggling)** | **3 (Solid)** | **4 (Strong)** | **5 (Mastery)** |
|---|---|---|---|---|---|
| **Tree Anatomy & Height** | Can't define height/depth | Confuse definitions | Define correctly sometimes | Always correct; explain relationships | Explain impact on operations |
| **Traversals** | Don't know 4 orders | Get ~50% correct | Get ~80% correct; mix up 1-2 | All correct; know use cases | Explain inorder's sorted property |
| **BST Operations** | Don't understand invariant | Understand but can't code | Code search/insert; delete hard | Code all 3 deletion cases cleanly | Explain successor finding from memory |
| **Balancing (AVL/RB)** | Don't know why balance matters | Know rules but can't apply | Identify cases; struggle with rotations | Rotate correctly; understand trade-offs | Explain why production uses RB |
| **Tree Patterns** | Can't recognize patterns | Recognize but stuck on code | Code 2-3 patterns; others hard | Code diameter, LCA, paths cleanly | Combine patterns for complex problems |
| **Interview Readiness** | Scared to interview | Nervous; might freeze | Confident on BST ops; patterns fuzzy | Strong across board; occasional slips | Explain, code, optimize like pro |

**Target:** Aim for 4+ in most categories by end of week.

### Reflection Questions

1. **Which topic felt hardest?** (Answer honestly – decide if you need extra practice or if it's normal growth)
2. **Can you explain to a friend why inorder traversal of a BST produces sorted output?** (If not, review Day 2)
3. **Can you draw and rotate an imbalanced AVL tree correctly?** (If not, practice 3 more by hand)
4. **Can you code path sum with backtracking without looking at notes?** (If not, practice once more)
5. **Can you name 3 production systems using trees and why they chose them?** (If not, research today)

---

## 🏁 WEEK 7 COMPLETION SIGNAL

### ✅ You're Ready to Move to Week 8 When:

1. **Daily Checklists Complete:** All 5 days' concept internalization, activities, and self-assessments done
2. **Rubric Scores:** 4+ in most categories; 3+ in all
3. **Coding Fluency:** Can code BST operations, traversals, diameter, LCA in < 15 min total (combined)
4. **Problem Recognition:** Can identify pattern in 10+ new tree problems (path sum? diameter? LCA?)
5. **Interview Confidence:** Explained a tree concept aloud (to friend, recording, or in mock) without significant gaps
6. **Production Understanding:** Can name systems using trees and explain trade-offs (AVL vs. RB, when to augment)

### ⚠️ If Not Ready, Spend Extra Time On:

- **Weakest topic:** Review that day's checklist, re-code 2-3 problems
- **Interview questions:** Do 10+ more from problem ladder
- **Hard problems:** Solve 2-3 hard problems (Tier 4) to stretch understanding

---

## 📌 ADDITIONAL RESOURCES & NOTES

### 🔍 Debugging Guide for Common Errors

**Error: "My tree is valid locally but fails global BST check"**
→ Use bounds method (min/max); don't just check left < parent < right

**Error: "Stack overflow on deep trees with iterative traversal"**
→ Iterative is O(n) space anyway; if tree degenerate, consider other approaches (threaded trees, iterators)

**Error: "Can't figure out which rotation to apply"**
→ Draw the imbalance; identify child's BF; match to LL, RR, LR, RL table

**Error: "Deletion not working in my BST"**
→ Double-check successor finding (leftmost in right subtree); trace deletion case-by-case

**Error: "Serialization deserializes differently"**
→ Check null markers; verify order (preorder means nodes visited before children)

---

### 💭 Mental Models to Internalize

1. **Height as a Lens:** Many tree problems reduce to "what's the height?" If height is O(log n), operation is O(log n).

2. **Traversal as Exploration Order:** Choose traversal based on when you need to process nodes. Need sorted? Inorder. Need to delete? Postorder.

3. **Invariants Enable Speed:** BST invariant (left < parent < right) enables binary search. AVL invariant (|BF| ≤ 1) enables O(log n) height. Respect invariants.

4. **Rotations Are Local:** Rotations only touch O(1) nodes; no subtree rebuilding. This is why they're efficient for rebalancing.

5. **Backtracking Explores All:** If you need all solutions (paths, subsets), backtrack. If you just need one, early exit.

6. **Augmentation Trades Space for Speed:** More memory per node → faster queries; but update overhead must be justified by query frequency.

---

### 📚 Extended Reading (Optional)

- **Persistent Data Structures:** How to make tree modifications non-destructive (functional trees)
- **Self-Balancing Trees (Other Variants):** Splay trees, B-trees, 2-3 trees, Scapegoat trees
- **Specialized Trees:** Segment trees, Fenwick trees (Binary Indexed Trees), Treaps, Skip lists
- **Concurrent Trees:** Lock-free trees, optimistic locking (for multi-threaded systems)

---

## 📖 WEEK 7 SUMMARY TABLE

| **Day** | **Main Topic** | **Key Concepts** | **Must-Know Algorithms** | **Complexity** | **Common Interview Q** |
|---|---|---|---|---|---|
| **1** | Traversals | Height, depth, 4 orders | Preorder, inorder, postorder, level-order (recursive + iterative) | O(n) time, O(h) space | "Code iterative inorder" |
| **2** | BSTs | Invariant, search, insert, delete | Delete with successor, validation with bounds | O(h) avg, O(n) worst | "Delete node with two children" |
| **3** | Balancing | AVL rotations, red-black rules | LL, RR, LR, RL rotations; AVL vs RB tradeoffs | O(log n) guaranteed | "When to use AVL vs RB?" |
| **4** | Patterns | Path sum, diameter, LCA, serialization | Backtracking, DFS + return multiple values, queue-based reconstruction | O(n) or O(log n) depending on pattern | "Find tree diameter" |
| **5** | Augmentation | Metadata, order-statistics, range queries | kth smallest, range count, augmentation invariant | O(log n) with augmentation | "Design augmented tree" |

---

**Status:** ✅ WEEK 7 FULL PLAYBOOK COMPLETE

**Next Steps:**
- Complete all daily checklists
- Solve practice problems (Tier 1-4 ladder)
- Review production systems and real-world applications
- Move to Week 8 (Graphs) when ready
- Reference this playbook throughout interview prep

---