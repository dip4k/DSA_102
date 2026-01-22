# 🧭 Week_07_Problem_Solving_Roadmap.md

**Version:** v1.0 (Progressive Practice Strategy)  
**Purpose:** Structured progression from basic to advanced tree problems  
**Audience:** Active learners solving practice problems  
**Status:** ✅ PRODUCTION-READY

---

## 🎯 OVERALL STRATEGY FOR WEEK 7

### The Three-Stage Philosophy

**Stage 1: Canonical Problems (Mastery)**
- Solve bread-and-butter tree problems that test fundamental understanding
- These are patterns that appear repeatedly in variations
- Goal: 100% fluency, solve in < 3 minutes without thinking

**Stage 2: Variations & Constraints (Recognition)**
- Same core pattern, but with twists (additional constraints, different input format, edge cases)
- Goal: Recognize the underlying pattern despite surface differences
- Identify what changed vs. what stayed the same

**Stage 3: Integration & Mixed Concepts (Synthesis)**
- Problems combining multiple patterns or concepts
- Typically harder, closer to "real" interview problems
- Goal: Integrate knowledge, handle ambiguity, design under time pressure

---

## 🟢 STAGE 1: CANONICAL PROBLEMS (Mastery via Repetition)

### Day 1 Focus: Traversals

#### Problem Set 1.1: Basic Traversal Orders

**P1.1.1: Preorder Traversal (LeetCode #144 Easy)**
- **Pattern:** Visit parent, then left, then right
- **Key Challenge:** Understand recursive base case and order
- **Solution Skeleton:**
  ```
  1. Define recursive function(node, result)
  2. If node is null, return
  3. Add node.val to result
  4. Recurse left subtree
  5. Recurse right subtree
  ```
- **Implementation Time:** 2-3 minutes
- **Self-Check:**
  - [ ] Handles null tree
  - [ ] Correct ordering (parent first)
  - [ ] Both children processed

**P1.1.2: Preorder Traversal Iteratively**
- **Pattern:** Use explicit stack; push right before left
- **Key Challenge:** Understanding why right is pushed first
- **Solution Skeleton:**
  ```
  1. Initialize stack with root
  2. While stack not empty:
     a. Pop node, add to result
     b. Push right child (if exists)
     c. Push left child (if exists)
  ```
- **Why This Matters:** Shows you understand LIFO and can simulate recursion
- **Implementation Time:** 3-4 minutes

**P1.1.3: Inorder Traversal Iteratively (LeetCode #94 Easy)**
- **Pattern:** Go all the way left, then process, then go right
- **Key Challenge:** Knowing WHEN to process (not immediately)
- **Solution Skeleton:**
  ```
  1. Initialize stack, current = root
  2. While current or stack not empty:
     a. While current: push and go left
     b. Pop and process
     c. Go right
  ```
- **Why This Matters:** Inorder is crucial for BSTs; requires different control flow
- **Implementation Time:** 3-4 minutes
- **Variant:** Find inorder successor (next element in inorder sequence)

**P1.1.4: Level-Order Traversal (LeetCode #102 Easy)**
- **Pattern:** BFS with queue; process one level at a time
- **Key Challenge:** Capturing queue size before processing level
- **Solution Skeleton:**
  ```
  1. Initialize queue with root
  2. While queue not empty:
     a. Capture levelSize = queue.Count
     b. For i = 0 to levelSize-1:
        - Dequeue, add to current level
        - Enqueue children
     c. Add current level to result
  ```
- **Why This Matters:** Demonstrates BFS understanding; level grouping is common
- **Implementation Time:** 4-5 minutes

#### Problem Set 1.2: Traversal Variants

**P1.2.1: Maximum Depth of Binary Tree (LeetCode #104 Easy)**
- **Related To:** Tree height calculation via postorder
- **Variant Twist:** Instead of visiting nodes, return a value
- **Key Insight:** Height = 1 + max(left height, right height)
- **Time Complexity:** O(n) single pass, O(h) stack
- **Implementation Time:** 2 minutes
- **Extension:** Minimum depth (watch for single-child nodes)

**P1.2.2: Balanced Binary Tree (LeetCode #110 Easy)**
- **Related To:** Postorder traversal + validation
- **Variant Twist:** Not just calculate height, but validate balance
- **Key Insight:** Return -1 if unbalanced (to avoid recalculating); compare subtree heights
- **Common Mistake:** Recalculating heights multiple times (inefficient)
- **Implementation Time:** 5 minutes
- **Extension:** Return balance factor at each node

**P1.2.3: Invert Binary Tree (LeetCode #226 Easy)**
- **Related To:** Preorder traversal + structure modification
- **Variant Twist:** Swap children at each node
- **Key Insight:** Can be done top-down (process node, then recurse) or bottom-up
- **Implementation Time:** 2 minutes
- **Extension:** Invert at specific depth or subtree

**P1.2.4: Path Sum I (LeetCode #112 Easy)**
- **Related To:** Preorder traversal with accumulated state
- **Variant Twist:** Accumulate sum while traversing
- **Key Insight:** Leaf is node with no children; check sum only at leaves
- **Common Mistake:** Checking sum at null node instead of leaf
- **Implementation Time:** 3 minutes
- **Difficulty Spike:** Remove leaf nodes with negative values to reach target

---

### Day 2 Focus: BST Operations

#### Problem Set 2.1: Core BST Operations

**P2.1.1: Search in a Binary Search Tree (LeetCode #700 Easy)**
- **Pattern:** Use BST property to eliminate half the search space
- **Solution Skeleton:**
  ```
  1. If node is null, return null
  2. If val == node.val, return node
  3. Else if val < node.val, search left
  4. Else search right
  ```
- **Implementation Time:** 1 minute (trivial if you understand invariant)
- **Key Insight:** BST property enables binary search

**P2.1.2: Insert into a Binary Search Tree (LeetCode #701 Medium)**
- **Pattern:** Find correct position, create node, maintain invariant
- **Key Challenge:** Returning node to update parent pointers
- **Solution Skeleton:**
  ```
  1. If node is null, create new node with val
  2. Else if val < node.val, node.left = Insert(node.left, val)
  3. Else node.right = Insert(node.right, val)
  4. Return node
  ```
- **Implementation Time:** 2 minutes
- **Variant:** Handle duplicates (insert multiple times vs. count)

**P2.1.3: Delete Node in a BST (LeetCode #450 Medium)**
- **Pattern:** Handle three cases: leaf, one child, two children
- **Key Challenge:** Inorder successor for two-child case
- **Solution Skeleton:**
  ```
  1. If node is null, return null
  2. If target < node.val, node.left = Delete(node.left, target)
  3. Else if target > node.val, node.right = Delete(node.right, target)
  4. Else (found):
     a. If no left child, return right child
     b. If no right child, return left child
     c. Else: find inorder successor, copy value, delete successor
  5. Return node
  ```
- **Implementation Time:** 5-7 minutes (most complex)
- **Helper Function:** Find minimum in subtree

**P2.1.4: Validate Binary Search Tree (LeetCode #98 Medium)**
- **Pattern:** Recursive range checking
- **Key Challenge:** Not just checking left < node < right locally
- **Solution Skeleton:**
  ```
  1. Define helper(node, min, max)
  2. If node is null, return true
  3. If node.val <= min OR node.val >= max, return false
  4. Return helper(left, min, node.val) AND helper(right, node.val, max)
  ```
- **Implementation Time:** 4-5 minutes
- **Common Mistake:** Only checking immediate children, not entire subtree bounds

#### Problem Set 2.2: BST Queries

**P2.2.1: Inorder Successor in BST (LeetCode #285 Medium)**
- **Pattern:** Find next element in sorted order from BST
- **Two Cases:**
  - If right child exists: leftmost node in right subtree
  - Else: first ancestor where node is in left subtree
- **Implementation Time:** 4 minutes
- **Variant:** Inorder predecessor (mirror logic)

**P2.2.2: Kth Smallest Element in BST (LeetCode #230 Medium)**
- **Pattern:** Inorder traversal with counter
- **Solution Skeleton:**
  ```
  1. Do inorder traversal (left, node, right)
  2. Count nodes visited
  3. When count == k, return node value
  ```
- **Implementation Time:** 3-4 minutes
- **Optimization:** Augment BST with subtree sizes for O(log n) query

**P2.2.3: Two Sum IV - Input is a BST (LeetCode #653 Easy)**
- **Pattern:** Two-pointer approach on inorder sequence
- **Solution Skeleton:**
  ```
  1. Inorder traversal → sorted array
  2. Two pointers: left and right
  3. Move pointers based on sum vs target
  ```
- **Implementation Time:** 4-5 minutes
- **Alternative:** Hash set with DFS (doesn't use BST property)

**P2.2.4: Lowest Common Ancestor of a BST (LeetCode #235 Easy)**
- **Pattern:** Use BST property to narrow search
- **Solution Skeleton:**
  ```
  1. If both values < node, LCA in left
  2. If both values > node, LCA in right
  3. Else (split), node is LCA
  ```
- **Implementation Time:** 2 minutes
- **Key Insight:** Much simpler than general tree LCA

---

### Day 3 Focus: Balanced Trees (Conceptual + Limited Coding)

#### Problem Set 3.1: Balance Validation & Concepts

**P3.1.1: Balanced Binary Tree (LeetCode #110 Easy)**
- **Pattern:** Validate AVL property (|height diff| ≤ 1)
- **Already Covered in Stage 1.2, but here's the connection:**
  - This validates the AVL invariant
  - After studying rotations, return to this problem
- **Implementation Time:** 5 minutes

**P3.1.2: Largest Values From Labels (Conceptual)**
- **Pattern:** Understanding why balance matters
- **Conceptual Exercise:** Given a degenerate tree (linked list) and a balanced tree with same values
  - Show that many operations on degenerate tree are O(n)
  - Show same operations on balanced tree are O(log n)
- **No Implementation Required**
- **Value:** Builds intuition for why companies use Red-Black trees

#### Problem Set 3.2: Tree Reconstruction

**P3.2.1: Construct Binary Tree from Preorder and Inorder (LeetCode #105 Medium)**
- **Pattern:** Use inorder to split subtrees; preorder to identify roots
- **Solution Skeleton:**
  ```
  1. Preorder first element is root
  2. Find that element in inorder
  3. Left of inorder is left subtree, right is right subtree
  4. Recurse on left and right portions
  ```
- **Implementation Time:** 6-8 minutes
- **Key Challenge:** Index management and recursion bounds

**P3.2.2: Construct Binary Tree from Inorder and Postorder (LeetCode #106 Medium)**
- **Pattern:** Similar to above, but postorder last element is root
- **Key Difference:** Postorder is left, right, parent (vs. preorder parent, left, right)
- **Implementation Time:** 6-8 minutes
- **Extension:** Why can't you reconstruct from preorder + postorder alone?

---

### Day 4 Focus: Tree Patterns

#### Problem Set 4.1: Path Problems

**P4.1.1: Path Sum I (LeetCode #112 Easy)**
- **Pattern:** Root-to-leaf path sum matching
- **Already Covered:** But here's the strategy:
  - Simple DFS with accumulated sum
  - Check at leaf nodes (not null)
- **Implementation Time:** 3 minutes

**P4.1.2: Path Sum II (LeetCode #113 Medium)**
- **Pattern:** Find ALL paths (not just existence)
- **Key Challenge:** Backtracking - remove node after recursion
- **Solution Skeleton:**
  ```
  1. DFS with path list
  2. Add node to path
  3. If leaf and sum matches, copy path to result
  4. Recurse left and right
  5. CRITICAL: Remove node from path (backtrack)
  ```
- **Implementation Time:** 4-5 minutes
- **Common Mistake:** Forgetting to backtrack → all results identical

**P4.1.3: Path Sum III (LeetCode #437 Medium)**
- **Pattern:** ANY path in tree (not just root-to-leaf)
- **Key Challenge:** Consider every node as potential start
- **Solution Strategy:**
  ```
  1. For each node as potential start
  2. DFS to find paths starting from this node
  3. Use prefix sum + hash map to count
  ```
- **Implementation Time:** 6-8 minutes (harder concept)
- **Alternative:** Global function tracking all paths

**P4.1.4: Binary Tree Maximum Path Sum (LeetCode #124 Hard)**
- **Pattern:** Maximum sum of ANY path (not constrained to root-leaf or root)
- **Key Challenge:** Distinguish return value (max path starting at node) vs answer (max path overall)
- **Solution Skeleton:**
  ```
  1. Postorder: calculate max path starting at node
  2. At each node: max_ending = max(val, val + left_max, val + right_max)
  3. Update global max: val + left_max + right_max (path through this node)
  4. Return max_ending to parent
  ```
- **Implementation Time:** 7-10 minutes (complex DP logic)

#### Problem Set 4.2: Tree Diameter

**P4.2.1: Diameter of Binary Tree (LeetCode #543 Easy)**
- **Pattern:** Longest path between any two nodes
- **Solution Skeleton:**
  ```
  1. Postorder DFS
  2. At each node: left_h = height(left), right_h = height(right)
  3. Diameter through node = left_h + right_h + 1 (edges, not nodes)
  4. Update global max
  5. Return height to parent
  ```
- **Implementation Time:** 4-5 minutes
- **Key Insight:** Height calculation via postorder

**P4.2.2: Distance Between Two Nodes (Conceptual Extension)**
- **Pattern:** Once you have diameter, find actual path length
- **Approach:** Find LCA, calculate dist(LCA, node1) + dist(LCA, node2)
- **Implementation Time:** 3-4 minutes (after LCA mastery)

#### Problem Set 4.3: LCA Problems

**P4.3.1: Lowest Common Ancestor of a Binary Tree (LeetCode #236 Medium)**
- **Pattern:** Find deepest common ancestor of two nodes
- **Solution Skeleton:**
  ```
  1. If node is null, return null
  2. If node is one of the targets, return node
  3. Recurse left and right
  4. If both left and right non-null, node is LCA
  5. If only one non-null, return that one
  6. If both null, return null
  ```
- **Implementation Time:** 4-5 minutes
- **Key Insight:** Recursive decomposition based on where targets are

**P4.3.2: LCA with Parent Pointers**
- **Pattern:** Use parent links to traverse upward
- **Solution Skeleton:**
  ```
  1. Hash set to mark path from node1 to root
  2. Traverse from node2 upward until finding marked node
  ```
- **Implementation Time:** 3-4 minutes
- **Trade-off:** Extra space (hash set) vs different traversal

**P4.3.3: Lowest Common Ancestor in BST (LeetCode #235 Easy)**
- **Pattern:** Use BST property to narrow search
- **Already Covered:** Much simpler with invariant
- **Implementation Time:** 2 minutes

#### Problem Set 4.4: Serialization

**P4.4.1: Serialize and Deserialize Binary Tree (LeetCode #297 Hard)**
- **Pattern:** Convert tree to string and back
- **Solution Skeleton:**
  ```
  Serialize (Preorder):
  1. Preorder traversal: node, left, right
  2. Include "null" for missing children
  3. Delimit with commas
  
  Deserialize (Queue reconstruction):
  1. Split string to tokens
  2. Preorder recursive reconstruction using queue
  3. Check for "null" markers
  ```
- **Implementation Time:** 8-10 minutes (both functions)
- **Key Challenge:** Ensuring unique encoding and correct reconstruction

**P4.4.2: Codec for Serialization (Extension)**
- **Pattern:** Optimize serialization (fewer bytes)
- **Approaches:**
  - Use integer IDs instead of values
  - Compress null markers
  - Use binary encoding
- **Implementation Time:** 10+ minutes

---

## 🟡 STAGE 2: VARIATIONS & CONSTRAINTS (Recognition)

### Recognizing Patterns Despite Variations

#### Set 2.1: Traversals with Twists

**V2.1.1: Binary Tree Right Side View (LeetCode #199 Medium)**
- **Core Pattern:** Level-order traversal
- **Variation:** Keep only rightmost element of each level
- **Insight:** Last element of each level (after processing level)
- **Implementation Time:** 4-5 minutes
- **Alternative Approaches:**
  - Level-order with tracking
  - Reverse preorder (right, left, root) with level tracking

**V2.1.2: Vertical Order Traversal (LeetCode #987 Medium)**
- **Core Pattern:** Level-order traversal
- **Variation:** Group by column; order by row within column
- **Insight:** Track (row, col) coordinates; sort by (col, row)
- **Implementation Time:** 6-8 minutes

**V2.1.3: All Paths From Root to Leaves (LeetCode #257 Easy)**
- **Core Pattern:** DFS with path accumulation
- **Variation:** String output format; all paths, not filtered
- **Implementation Time:** 3-4 minutes

#### Set 2.2: BST Variations

**V2.2.1: Trim a Binary Search Tree (LeetCode #669 Medium)**
- **Core Pattern:** BST traversal with validation
- **Variation:** Remove subtrees outside range [low, high]
- **Insight:** If node out of range, move to appropriate child
- **Implementation Time:** 4-5 minutes

**V2.2.2: Convert BST to Greater Sum Tree (LeetCode #538 Medium)**
- **Core Pattern:** Inorder traversal (but modified order)
- **Variation:** REVERSE inorder (right, node, left) to accumulate sum
- **Insight:** In reverse order, each node sees sum of all larger nodes
- **Implementation Time:** 4-5 minutes
- **Key Insight:** Problem changes traversal order

**V2.2.3: Flatten Binary Search Tree to Sorted List (LeetCode, no number)**
- **Core Pattern:** Inorder traversal + linking
- **Variation:** Modify tree to be right-skewed linked list in-place
- **Implementation Time:** 5-6 minutes

#### Set 2.3: Path Problems with Constraints

**V2.3.1: Sum of Left Leaves (LeetCode #404 Easy)**
- **Core Pattern:** Path sum + DFS
- **Variation:** Only count leaves on left side
- **Constraint:** Must track whether node is left or right child
- **Implementation Time:** 3 minutes

**V2.3.2: Maximum Path Sum with Negative Values (Extension of #124)**
- **Core Pattern:** Path sum maximum
- **Variation:** How do negative values affect decisions?
- **Insight:** May skip negative nodes entirely
- **Implementation Time:** 5-6 minutes

**V2.3.3: Path Sum Equals K with Multiplicity (Conceptual)**
- **Core Pattern:** Path sum counting
- **Variation:** Count ALL paths (not just existence)
- **Challenge:** Paths can start/end anywhere
- **Implementation Time:** 6-8 minutes

#### Set 2.4: Tree Structure Variations

**V2.4.1: Symmetric Tree (LeetCode #101 Easy)**
- **Core Pattern:** Tree comparison
- **Variation:** Check if tree is mirror of itself
- **Insight:** Compare node with its mirror counterpart
- **Implementation Time:** 3-4 minutes

**V2.4.2: Same Tree (LeetCode #100 Easy)**
- **Core Pattern:** Tree comparison
- **Variation:** Check if two trees are identical
- **Implementation Time:** 2-3 minutes

**V2.4.3: Subtree of Another Tree (LeetCode #572 Easy)**
- **Core Pattern:** Tree matching
- **Variation:** Find if one tree is subtree of another
- **Approach:** For each node in large tree, check if subtree rooted here matches small tree
- **Implementation Time:** 4-5 minutes

---

## 🔴 STAGE 3: INTEGRATION & MIXED CONCEPTS (Synthesis)

### Combining Multiple Patterns

#### Set 3.1: Traversal + Tree Modification

**I3.1.1: Invert Binary Tree Recursively (LeetCode #226, but with timing emphasis)**
- **Patterns Combined:** Traversal + mutation
- **Challenge:** Do it in one pass
- **Implementation Time:** 2 minutes

**I3.1.2: Populating Next Right Pointers (LeetCode #116 Medium)**
- **Patterns Combined:** Level-order traversal + pointer linking
- **Challenge:** Do it in O(1) space (using existing pointers)
- **Insight:** Process level by level; use already-linked nodes to traverse
- **Implementation Time:** 6-8 minutes

**I3.1.3: Binary Tree Pruning (LeetCode #814 Medium)**
- **Patterns Combined:** Postorder traversal + validation
- **Challenge:** Remove subtrees that don't contain a target value
- **Insight:** Recursively prune children, then check if current node should be pruned
- **Implementation Time:** 4-5 minutes

#### Set 3.2: BST + Additional Constraints

**I3.2.1: Most Frequent Subtree Sum (LeetCode #508 Medium)**
- **Patterns Combined:** Postorder traversal + hash map counting
- **Challenge:** Find subtree sum that appears most often
- **Insight:** Traverse and compute subtree sums; track frequencies
- **Implementation Time:** 5-6 minutes

**I3.2.2: Range Sum of BST (LeetCode #938 Medium)**
- **Patterns Combined:** BST traversal + range filtering
- **Challenge:** Efficiently sum values in range [low, high]
- **Insight:** Use BST property to prune branches outside range
- **Implementation Time:** 4-5 minutes

**I3.2.3: Count of Range Sum (LeetCode #327 Hard)**
- **Patterns Combined:** BST property + prefix sum
- **Challenge:** Count paths with sum in range [lower, upper]
- **This is Advanced:** Merge sort + prefix sum technique
- **Implementation Time:** 10+ minutes

#### Set 3.3: Serialization + Reconstruction with Constraints

**I3.3.1: Serialize and Deserialize N-ary Tree (LeetCode #428)**
- **Patterns Combined:** Serialization + generalized tree
- **Challenge:** Handle variable number of children
- **Insight:** Include child count in serialization
- **Implementation Time:** 8-10 minutes

**I3.3.2: Construct Binary Tree from String (LeetCode #536 Medium)**
- **Patterns Combined:** String parsing + tree reconstruction
- **Challenge:** Parse "(val (left) (right))" format
- **Insight:** Recursive parsing with index tracking
- **Implementation Time:** 6-8 minutes

#### Set 3.4: Advanced DP Patterns on Trees

**I3.4.1: House Robber III (LeetCode #337 Medium)**
- **Patterns Combined:** Tree DP + decision making
- **Challenge:** Rob nodes (not adjacent) for max sum
- **Insight:** DP state: rob or don't rob current node
- **Implementation Time:** 6-8 minutes

**I3.4.2: Delete Nodes and Return Forest (LeetCode #1110 Medium)**
- **Patterns Combined:** DFS + tree modification + forest creation
- **Challenge:** Remove specified nodes; return remaining trees as list
- **Insight:** Postorder: process children, then decide about current node
- **Implementation Time:** 6-8 minutes

**I3.4.3: Recover Binary Search Tree (LeetCode #99 Hard)**
- **Patterns Combined:** BST property + inorder traversal + validation
- **Challenge:** Find and fix two swapped nodes in BST
- **Insight:** Inorder traversal should be sorted; detect violations
- **Implementation Time:** 8-10 minutes

---

## 📊 PROGRESSION MAP

### Recommended Problem-Solving Order

```
STAGE 1 (Days 1-4, ~40-50 min per day):
  Day 1: P1.1.1-4, P1.2.1-4 (8 problems)
  Day 2: P2.1.1-4, P2.2.1-4 (8 problems)
  Day 3: P3.1.1-2, P3.2.1-2 (4 problems)
  Day 4: P4.1.1-4, P4.2.1-2, P4.3.1-3, P4.4.1 (10 problems)
  
  Total: 30 problems, ~2-3 hours

STAGE 2 (Day 5 morning, ~30-40 min):
  V2.1.1-3, V2.2.1-3, V2.3.1-3, V2.4.1-3
  Total: 12 problems, ~1.5 hours

STAGE 3 (Day 5 afternoon + Weekend, ~60-90 min):
  I3.1.1-3, I3.2.1-3, I3.3.1-2, I3.4.1-3
  Total: 12 problems, ~2-3 hours
```

### Time Budget Allocation

| Stage | Problems | Time/Problem | Total |
|---|---|---|---|
| **Stage 1: Canonical** | 30 | 4-5 min | 2.5-2.5 hrs |
| **Stage 2: Variations** | 12 | 5-7 min | 1-1.5 hrs |
| **Stage 3: Integration** | 12 | 8-10 min | 1.5-2 hrs |
| **Total** | 54 | — | 5-6 hours |

---

## 🧠 PATTERN DECISION FLOWCHART

When you encounter a tree problem:

```
READ PROBLEM
    ↓
Does it mention "traverse" or "visit all"?
  YES → Which order needed? (pre/in/post/level) → Go to STAGE 1 Traversals
  NO  → Continue
    ↓
Does it involve a BST or "sorted"?
  YES → Search/insert/delete/validate? → Go to STAGE 1 BST Operations
  NO  → Continue
    ↓
Does it ask for a path or sum?
  YES → Root-to-leaf or any path? → Go to STAGE 1 Path Problems
  NO  → Continue
    ↓
Does it ask for "longest path" or "distance"?
  YES → Diameter or LCA variant? → Go to STAGE 1 Diameter/LCA
  NO  → Continue
    ↓
Does it ask to "encode", "serialize", "reconstruct"?
  YES → Serialization approach → Go to STAGE 1 Serialization
  NO  → Continue
    ↓
Is there an additional constraint or twist?
  YES → Recognize core pattern + variation → Go to STAGE 2
  NO  → Continue
    ↓
Does it combine multiple concepts (DP + tree, etc.)?
  YES → Integration problem → Go to STAGE 3
  NO  → Unknown pattern - research or ask for hints
```

---

## 🎯 COMMON TRAPS & HOW TO AVOID

### Trap 1: Forgetting Base Cases

**Symptom:** Stack overflow or NullReferenceException  
**Root Cause:** Missing `if (node == null) return;`  
**Prevention:** Always write base case FIRST before recursive case  

### Trap 2: Wrong Backtracking

**Symptom:** All paths show the same ending; all results identical  
**Root Cause:** Forgot to `path.RemoveAt()` after recursion  
**Prevention:** Remember: Recursive call modifies shared state; undo after returning  

### Trap 3: Level-Order Processing Wrong Level

**Symptom:** Results grouped incorrectly; next level mixed with current  
**Root Cause:** Using `queue.Count` directly in loop condition  
**Prevention:** `int levelSize = queue.Count;` BEFORE the loop  

### Trap 4: BST Validation Too Shallow

**Symptom:** Accepts invalid BSTs (only checks immediate children)  
**Root Cause:** Comparing only `left.val < node.val < right.val`  
**Prevention:** Use range checking: `min < node.val < max` propagated down tree  

### Trap 5: Height vs Depth Confusion

**Symptom:** Wrong complexity analysis or off-by-one errors  
**Root Cause:** Mixing definitions (height = edges below; depth = edges above)  
**Prevention:** Explicitly define at problem start; label diagrams  

---

## ✅ SELF-ASSESSMENT CHECKLIST

### After Stage 1 (Can you solve in < 5 min without hints?):
- [ ] All 4 traversal orders (recursive AND iterative for preorder/inorder)
- [ ] BST search, insert, delete (all three cases)
- [ ] BST validation with proper range checking
- [ ] Path sum I & II (with backtracking)
- [ ] Tree diameter with postorder DP
- [ ] LCA in general tree (three cases)
- [ ] Serialization and deserialization
- [ ] Tree reconstruction from traversal pairs

### After Stage 2 (Can you recognize patterns with variations?):
- [ ] Identify core pattern despite surface difference
- [ ] Explain how variation modifies the approach
- [ ] Solve in 5-8 minutes with minor thinking
- [ ] Anticipate edge cases specific to variation

### After Stage 3 (Can you integrate and synthesize?):
- [ ] Combine multiple concepts (traversal + DP, BST + constraints, etc.)
- [ ] Design solution from scratch under time pressure
- [ ] Explain trade-offs and alternative approaches
- [ ] Optimize for time/space iteratively

---

## 📈 WEEK 7 MASTERY ROADMAP

```
Monday:    Stage 1 - Traversals (P1.1, P1.2)
Tuesday:   Stage 1 - BST Operations (P2.1, P2.2)
Wednesday: Stage 1 - Balance & Reconstruction (P3.1, P3.2)
Thursday:  Stage 1 - Patterns (P4.1-4.4)
Friday AM: Stage 2 - Variations (V2.1-2.4)
Friday PM: Stage 3 - Integration (I3.1-3.4)
Weekend:   Mock Interviews + Weak Area Review
```

---

## 🚀 BEYOND WEEK 7

After mastering Week 7 problems, you're ready for:
- **Week 8:** Graph patterns (DFS/BFS on graphs extend tree traversals)
- **Week 10:** Tree DP patterns (combine DP with tree structure)
- **Week 9:** Augmented BSTs and order-statistics trees
- **Advanced:** Self-balancing tree implementations (AVL, Red-Black coding)

---

**Status:** ✅ Problem-Solving Roadmap Complete

