# 📌 Week_07_Problem_Solving_Roadmap.md

**Version:** v1.2 (Progressive Practice Strategy)  
**Purpose:** Structured progression from basic to advanced problem-solving  
**Audience:** Active learners working through problems  
**Status:** ✅ PRODUCTION-READY

---

## 🎯 WEEK 7 PROBLEM-SOLVING OVERVIEW

This roadmap structures your week into three progressive stages:

| Stage | Focus | Goals | Time |
|---|---|---|---|
| **Stage 1: Foundations** | Tree basics, traversals, BST ops | Understand mental models | Days 1-2 |
| **Stage 2: Mastery** | Balanced BSTs, deletion, validation | Implement cleanly | Days 2-3 |
| **Stage 3: Integration** | Tree patterns, advanced problems | Mix concepts elegantly | Days 4-5 |

Each stage has a **problem ladder**: canonical examples → variations → edge cases → integration.

---

## 🌳 STAGE 1: FOUNDATIONS (Days 1-2)

### Goal: Build Mental Models Without Code Yet

Before coding, you should be able to:
- Draw a tree and label height/depth
- Trace traversals by hand
- Explain BST search in English
- Predict what an unbalanced tree looks like

### Problems - Canonical (Warm-up)

**Problem 1.1: Tree Traversals By Hand**

*Pattern:* Preorder, inorder, postorder, level-order

*Difficulty:* ⭐ Easy | *Time Budget:* 5 min each

**What to do:**
1. Draw a sample tree with 7-10 nodes
2. Trace each traversal order by hand on paper
3. Verify your trace against the definition
4. Move nodes around; redo it (builds fluency)

**Example:**
```
Tree:
      1
     / \
    2   3
   / \
  4   5
```

- Preorder: 1, 2, 4, 5, 3
- Inorder: 4, 2, 5, 1, 3
- Postorder: 4, 5, 2, 3, 1
- Level-order: 1, 2, 3, 4, 5

**Signal:** If you can trace correctly without hesitation, move on.

---

**Problem 1.2: Height & Depth Calculations**

*Pattern:* Structural properties

*Difficulty:* ⭐ Easy | *Time Budget:* 5 min

**What to do:**
1. Given a tree with n nodes, what's min/max height?
2. Complete binary tree with 100 nodes—what's height?
3. For each node, calculate depth from root

**Example:**
Tree with 100 nodes (complete):
- Height = ⌊log₂(100)⌋ = 6
- Root depth = 0
- Leaf depth = 6

**Signal:** Understand the log(n) relationship.

---

**Problem 1.3: BST vs. Regular Tree**

*Pattern:* Invariant recognition

*Difficulty:* ⭐ Easy | *Time Budget:* 3 min each

**What to do:**
1. Look at 3 sample trees (1 valid BST, 2 invalid)
2. Explain why each is valid or invalid
3. State the invariant clearly

**Example:**
```
Tree A:       5          Tree B:       5
             / \                      / \
            3   7                     3   4

Valid BST                    Invalid: 4 is not > 5
```

**Signal:** You can distinguish BSTs from regular trees instantly.

---

### Problems - Variations (Deepen Understanding)

**Problem 1.4: Tree Construction from Traversals**

*Pattern:* Reconstruction

*Difficulty:* ⭐ Medium | *Time Budget:* 10 min

**What to do:**
1. Given preorder: [3, 9, 20, 15, 7] and inorder: [9, 3, 15, 20, 7]
2. Reconstruct the tree
3. Explain why inorder + one other order is sufficient

**Key Insight:** Preorder gives root first. Inorder partitions left/right subtrees.

**Signal:** Understand that inorder is the "partition key."

---

**Problem 1.5: Degenerate Tree Worst Case**

*Pattern:* Complexity analysis

*Difficulty:* ⭐ Medium | *Time Budget:* 5 min

**What to do:**
1. Insert [1, 2, 3, 4, 5] into a BST
2. Draw the resulting tree
3. Predict time complexity of search
4. Compare to balanced tree

**Expected:**
- Unbalanced: O(n) per operation
- Balanced: O(log n)

**Signal:** Visceral understanding of why balance matters.

---

## 🔍 STAGE 2: MASTERY (Days 2-3)

### Goal: Implement Cleanly, Handle Edge Cases

Now code the core operations. Focus on **correctness first, elegance second.**

### Problems - Canonical (Core Implementation)

**Problem 2.1: BST Search & Insert**

*Pattern:* Core operations

*Difficulty:* ⭐ Medium | *Time Budget:* 15 min (total)

**What to do:**
1. Implement BST search in pseudocode
2. Implement insert
3. Test with: [5, 3, 7, 2, 4, 6, 8]

**Key Edge Cases:**
- Searching for non-existent value
- Inserting duplicate
- Inserting into empty tree
- Inserting at boundary (leftmost/rightmost)

**Code Skeleton:**
```
class Node:
    val, left, right

function search(node, target):
    if node is null: return false
    if target == node.val: return true
    if target < node.val: return search(node.left, target)
    else: return search(node.right, target)

function insert(node, val):
    if node is null: return Node(val)
    if val < node.val: node.left = insert(node.left, val)
    else if val > node.val: node.right = insert(node.right, val)
    return node
```

**Signal:** Can code without referring to notes.

---

**Problem 2.2: BST Deletion (All Cases)**

*Pattern:* Complex operation

*Difficulty:* ⭐ Hard | *Time Budget:* 25 min

**What to do:**
1. Implement deletion handling 3 cases:
   - Leaf: remove directly
   - One child: bypass
   - Two children: find inorder successor
2. Test deletions in sequence from [5, 3, 7, 2, 4, 6, 8]

**Key Edge Cases:**
- Deleting root with two children
- Deleting node whose successor has a right child
- Deleting from single-node tree
- Deleting from empty tree

**Trace Example:**
```
Tree: 5 (root)
     / \
    3   7
   / \
  2   4

Delete 3 (two children):
- Find successor: 4
- Copy 4 to 3's position
- Delete 4 from right subtree
- Result: 5, 4 (root), 2, 7
```

**Signal:** Understand why successor logic handles right subtree correctly.

---

**Problem 2.3: Tree Validation (BST Check)**

*Pattern:* Constraint checking

*Difficulty:* ⭐ Hard | *Time Budget:* 15 min

**What to do:**
1. Implement invalid approach first (left < root < right only)
2. Test on this tree and see it fail:
```
    10
   /  \
  5    15
      /  \
     6   20  ← 6 < 10 but in right subtree! Invalid BST.
```

3. Fix with min/max bounds
4. Re-test

**Correct Approach:**
```
function isBST(node, min_val, max_val):
    if node is null: return true
    if node.val <= min_val or node.val >= max_val: return false
    return isBST(node.left, min_val, node.val) and
           isBST(node.right, node.val, max_val)
```

**Signal:** Understand why local checks aren't enough; need global bounds.

---

### Problems - Variations (Cement Understanding)

**Problem 2.4: Iterative Traversals**

*Pattern:* Simulation of recursion

*Difficulty:* ⭐ Medium | *Time Budget:* 20 min (all three)

**What to do:**
1. Implement inorder iterative (using stack)
2. Implement preorder iterative
3. Implement postorder iterative (trickiest)

**Inorder Iterative Skeleton:**
```
function inorderIterative(root):
    stack = []
    current = root
    result = []
    
    while current or stack:
        while current:
            stack.push(current)
            current = current.left
        current = stack.pop()
        result.append(current.val)
        current = current.right
    
    return result
```

**Postorder Insight:** You need to track the previous node to know if you've just processed a right child.

**Signal:** Comfortable with stack-based iteration.

---

**Problem 2.5: Inorder Successor & Predecessor**

*Pattern:* Navigation without parent pointers

*Difficulty:* ⭐ Hard | *Time Budget:* 15 min

**What to do:**
1. Given a node, find inorder successor
2. Given a node, find inorder predecessor
3. Assume no parent pointers

**Successor Logic:**
- If right child exists: leftmost in right subtree
- If no right child: traverse up until you're a left child

**Example:**
```
Tree:        5
           /   \
          3     7
         / \   / \
        2   4 6   8

Successor of 3? → 4
Successor of 4? → 5
Successor of 5? → 6
```

**Signal:** Understand the two cases fluently.

---

## 🧩 STAGE 3: INTEGRATION (Days 4-5)

### Goal: Mix Concepts, Solve Complex Patterns

These problems combine multiple ideas. Focus on **pattern recognition** first, then implementation.

### Problems - Canonical (Pattern Recognition)

**Problem 3.1: Tree Diameter**

*Pattern:* DP on tree, returning (height, diameter)

*Difficulty:* ⭐ Hard | *Time Budget:* 20 min

**What to do:**
1. Understand the key insight: At each node, diameter = left_height + right_height
2. Implement DFS that returns (height, max_diameter_found)
3. Test on tree with diameter NOT through root

**Example:**
```
Tree:        1
           /   \
          2     3
         / \
        4   5
           /
          6

Diameter = 10 nodes (4 → 2 → 1 → ... → 6) [need actual measurement]
But at node 2: left_h=1, right_h=2, so 1+2=3 here
Need to track max across all nodes.
```

**Key Code Pattern:**
```
function dfs(node):
    if node is null: return (height=0, diameter=0)
    
    left = dfs(node.left)
    right = dfs(node.right)
    
    new_height = 1 + max(left.height, right.height)
    new_diameter = max(left.diameter, right.diameter, 
                       left.height + right.height)
    
    return (new_height, new_diameter)
```

**Signal:** Understand why you return BOTH values, not just one.

---

**Problem 3.2: Path Sum (Root-to-Leaf)**

*Pattern:* DFS + Backtracking

*Difficulty:* ⭐ Medium | *Time Budget:* 15 min

**What to do:**
1. Find all root-to-leaf paths summing to a target
2. CRITICAL: Backtrack after recursion
3. Verify you don't duplicate paths

**Example:**
```
Tree:        5
           /   \
          4     8
         /     / \
        11    13  4
       /  \        \
      7    2        1

Target = 22
Paths: [5,4,11,2] and [5,8,4,5]
```

**Template:**
```
function pathSum(node, target, current_path, result):
    if node is null: return
    
    current_path.append(node.val)
    
    if node is leaf and sum(current_path) == target:
        result.append(copy of current_path)
    
    pathSum(node.left, target, current_path, result)
    pathSum(node.right, target, current_path, result)
    
    current_path.pop()  // BACKTRACK - must have
```

**Common Mistake:** Forget the `pop()`. Result shows same path multiple times.

**Signal:** Backtracking feels natural.

---

**Problem 3.3: Lowest Common Ancestor (LCA)**

*Pattern:* Recursive binary decision (left/right/split)

*Difficulty:* ⭐ Hard | *Time Budget:* 20 min

**What to do:**
1. Find LCA of two nodes in a BST (simple)
2. Find LCA in general binary tree (harder)
3. Handle case where one or both nodes don't exist

**BST Version (Simple):**
```
function lcaBST(node, p, q):
    if p.val < node.val and q.val < node.val:
        return lcaBST(node.left, p, q)
    if p.val > node.val and q.val > node.val:
        return lcaBST(node.right, p, q)
    return node  // split case
```

**General Tree Version (Harder):**
```
function lca(node, p, q):
    if node is null or node == p or node == q: return node
    
    left = lca(node.left, p, q)
    right = lca(node.right, p, q)
    
    if left and right: return node  // split
    return left if left else right  // both in one subtree
```

**Key Insight:** Three cases—both left, both right, split—each returns a different answer.

**Signal:** Understand why the general tree version is trickier.

---

**Problem 3.4: Tree Serialization**

*Pattern:* Encoding/decoding with markers

*Difficulty:* ⭐ Hard | *Time Budget:* 25 min

**What to do:**
1. Serialize tree to string/list using preorder + nulls
2. Deserialize back to tree
3. Verify symmetry: deserialize(serialize(tree)) == tree

**Example:**
```
Tree:      1
          / \
         2   3

Serialization: [1, 2, null, null, 3, null, null]
              preorder: 1 (root)
                        2 (left child)
                          null (2's left)
                          null (2's right)
                        3 (right child)
                          null (3's left)
                          null (3's right)
```

**Serialize Template:**
```
function serialize(node):
    result = []
    
    function dfs(n):
        if n is null:
            result.append("null")
            return
        result.append(n.val)
        dfs(n.left)
        dfs(n.right)
    
    dfs(node)
    return result
```

**Deserialize Template:**
```
function deserialize(data):
    data_queue = queue(data)
    
    function dfs():
        val = data_queue.pop()
        if val == "null": return null
        node = Node(val)
        node.left = dfs()
        node.right = dfs()
        return node
    
    return dfs()
```

**Signal:** Understand why nulls are essential; why single pass works.

---

### Problems - Variations & Edge Cases

**Problem 3.5: Balanced BST Maintenance**

*Pattern:* Understanding rotations (not implementing)

*Difficulty:* ⭐ Very Hard | *Time Budget:* 30 min (conceptual)

**What to do:**
1. Understand AVL balance factor at each node
2. Identify which rotation fixes imbalance
3. Don't code—just trace through examples

**Example Imbalance (LL case):**
```
Before:        20                After:       10
              /                             /  \
             10                           5    20
            /
           5

Right rotation fixes this.
```

**Example Imbalance (LR case):**
```
Before:        20               After:       15
              /                            /  \
             10                          10    20
               \
                15

Left-Right: Rotate left (10 & 15), then rotate right (20 & 15).
```

**Signal:** Recognize imbalance patterns without code.

---

**Problem 3.6: Mixed Tree Operations**

*Pattern:* Combining insertion, deletion, validation

*Difficulty:* ⭐ Very Hard | *Time Budget:* 30 min

**What to do:**
1. Start with empty tree
2. Insert sequence: [5, 3, 7, 2, 4, 6, 8]
3. Validate tree after each operation
4. Delete 3; verify tree is still valid BST
5. Delete 5 (root); verify tree is still valid

**Trace Format:**
```
Step 1: Insert 5
        Tree: [5]
        Valid? Yes

Step 2: Insert 3
        Tree:   5
               /
              3
        Valid? Yes

... continue through all steps ...

Step 8: Delete 3
        Tree:    5
               /   \
              2     7
               \   / \
                4 6   8
        Valid? Yes
```

**Signal:** Understand how operations interact.

---

## 📋 DAILY CHECKLIST

### Day 1: Tree Anatomy & Traversals
**Concepts to Understand:**
- [ ] Height vs. depth definitions
- [ ] Full, complete, balanced trees
- [ ] Preorder, inorder, postorder, level-order purposes

**Activities:**
- [ ] Trace 5 traversals by hand on 2-3 different trees
- [ ] Problem 1.1-1.3 (canonical fundamentals)
- [ ] Draw degenerate tree; compare to balanced

**Reflection:**
- Why is BST height important for performance?
- Why does inorder give sorted output?

---

### Day 2: BST Operations
**Concepts to Understand:**
- [ ] BST invariant (left < root < right globally)
- [ ] Search, insert, delete algorithms
- [ ] Why deletion is complex (three cases)

**Activities:**
- [ ] Problem 2.1-2.2 (BST ops)
- [ ] Code search & insert from scratch
- [ ] Manually trace deletion for all 3 cases
- [ ] Problem 2.3 (validation with bounds)

**Reflection:**
- Why does inorder successor work for deletion?
- Why is the two-children case the hardest?

---

### Day 3: Balanced BSTs & Iterative Traversals
**Concepts to Understand:**
- [ ] AVL balance factor and invariant
- [ ] Why balance guarantees O(log n)
- [ ] Stack-based iterative traversal

**Activities:**
- [ ] Problem 2.4-2.5 (iterative traversals, successor)
- [ ] Understand 4 rotation cases (LL, RR, LR, RL)
- [ ] Recognize red-black vs. AVL trade-off
- [ ] Read about Java TreeMap (uses red-black)

**Reflection:**
- Why are red-black trees used in production?
- When would you prefer AVL over red-black?

---

### Day 4: Tree Patterns (Core)
**Concepts to Understand:**
- [ ] DFS with return value (height, diameter)
- [ ] Backtracking pattern (add, recurse, remove)
- [ ] LCA three-case logic
- [ ] Serialization + nulls

**Activities:**
- [ ] Problem 3.1-3.3 (diameter, path sum, LCA)
- [ ] Problem 3.4 (serialization)
- [ ] Code all four from scratch, no notes

**Reflection:**
- Why is backtracking essential for path sum?
- Why return (height, diameter), not just one?

---

### Day 5: Advanced & Integration (Optional)
**Concepts to Understand:**
- [ ] Augmented trees (storing subtree size)
- [ ] Order-statistics queries (kth smallest)
- [ ] Range count queries

**Activities:**
- [ ] Problem 3.5 (AVL rotations—conceptual)
- [ ] Problem 3.6 (mixed operations)
- [ ] Design augmented BST for kth smallest
- [ ] Code complete order-statistics solution

**Reflection:**
- How would you support concurrent inserts?
- What if you needed fast range updates?

---

## 🎯 WEEKLY INTEGRATION

### By End of Week, You Should:

1. **Draw & Understand:**
   - Any tree structure from scratch
   - All traversals by hand
   - Rotation cases from memory

2. **Code & Test:**
   - All 4 traversals (recursive & iterative)
   - BST search, insert, delete
   - Tree diameter, path sum, LCA, serialization
   - Validation with bounds

3. **Analyze & Explain:**
   - Why balance matters (log n vs. n)
   - When each traversal is used
   - Trade-offs between AVL and red-black
   - How deletion finds inorder successor

4. **Recognize & Apply:**
   - Patterns in new problems (path sum, diameter, LCA)
   - When to backtrack, when to return values
   - Production systems using trees (Java, Linux)

---

## ⏱️ TIME ALLOCATION STRATEGY

| Activity | Time | Day |
|---|---|---|
| Understand fundamentals | 60 min | Day 1 |
| Implement BST ops | 90 min | Day 2 |
| Iterative traversals | 45 min | Day 2-3 |
| Balanced BSTs (conceptual) | 60 min | Day 3 |
| Tree patterns (diameter, LCA) | 120 min | Day 4 |
| Serialization | 45 min | Day 4 |
| Review & mixed problems | 60 min | Day 5 |
| **TOTAL** | **480 min** | **Week** |

---

## 🔍 PROGRESS SIGNALS

**Day 1-2 Complete?** You can trace traversals and implement BST search/insert without notes.

**Day 3 Complete?** You can code iterative inorder and understand rotation cases.

**Day 4 Complete?** You can implement diameter, LCA, and serialization from scratch.

**Day 5 Complete?** You can design an augmented tree and explain trade-offs confidently.

---

**Status:** ✅ PROBLEM-SOLVING ROADMAP COMPLETE

