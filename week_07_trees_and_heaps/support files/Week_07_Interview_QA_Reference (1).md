# 📌 Week_07_Interview_QA_Reference.md

**Version:** v1.2 (Interview Preparation Guide)  
**Purpose:** Comprehensive question bank for mock interviews and concept verification  
**Audience:** Interview preparation, self-assessment  
**Status:** ✅ PRODUCTION-READY

---

## 🎯 HOW TO USE THIS FILE

This question bank contains **40+ interview questions organized by topic**, designed to help you prepare for coding interviews and verify your understanding of Week 7 concepts. 

**Best Practices:**
- Work through questions **without looking at answers** (answers are intentionally sparse to force active thinking)
- Time yourself: 2-3 minutes per question for conceptual Qs, 5-10 minutes for implementation Qs
- Record yourself explaining answers aloud (this is how you'll be tested)
- Return to questions you struggled with after 24-48 hours
- Use follow-ups to deepen your explanation

---

## 🌳 SECTION 1: TREE ANATOMY & FUNDAMENTALS

### Question 1.1: Core Definitions
**Question:** Explain the difference between height and depth in a tree. If a tree has 7 nodes, what's the minimum and maximum possible height?

**Follow-ups:**
1. Why does a leaf node have height 0, not 1?
2. If you insert nodes in a specific order, can you guarantee a particular height?

**Difficulty:** ⭐ Easy | **Time:** 2 min

---

### Question 1.2: Tree Classification
**Question:** Define a "complete binary tree" and explain why it matters for heap implementations but not for general BSTs.

**Follow-ups:**
1. Is a complete tree always balanced? Is a balanced tree always complete?
2. How would you check if a tree is complete in code?

**Difficulty:** ⭐ Easy | **Time:** 3 min

---

### Question 1.3: Perfect vs. Full vs. Complete
**Question:** You're given three descriptions:
- Tree A: Every node has 0 or 2 children
- Tree B: All levels fully filled except last, which fills left-to-right
- Tree C: All levels fully filled

Which is full? Which is complete? Can a tree be both?

**Follow-ups:**
1. What's the benefit of knowing a tree is full?
2. Prove: In a full binary tree with n nodes, how many leaves are there?

**Difficulty:** ⭐ Easy-Medium | **Time:** 3 min

---

### Question 1.4: Tree Size & Height Relationship
**Question:** A complete binary tree has 7 nodes. What's its height? What if it has 15 nodes? What if it has 100 nodes?

**Follow-ups:**
1. Derive the formula for height given n nodes in a complete tree.
2. Why is this relationship important for algorithm analysis?

**Difficulty:** ⭐ Medium | **Time:** 4 min

---

### Question 1.5: Degenerate Trees
**Question:** Explain how a BST can become "degenerate." What input pattern causes this? What operations become slow and why?

**Follow-ups:**
1. If you insert sorted values [1, 2, 3, 4, 5] into a BST, what does it look like?
2. How does this compare to a linked list in terms of time complexity?

**Difficulty:** ⭐ Medium | **Time:** 3 min

---

## 🔁 SECTION 2: TRAVERSALS

### Question 2.1: Preorder Traversal Purpose
**Question:** You're implementing a tree copy function. Why would you use preorder traversal instead of inorder or postorder?

**Follow-ups:**
1. What if you needed to delete every node? Which traversal makes more sense?
2. How would you implement preorder iteratively using a stack?

**Difficulty:** ⭐ Medium | **Time:** 4 min

---

### Question 2.2: Inorder Traversal & Sorted Output
**Question:** Prove that inorder traversal of a BST produces values in sorted order. Use an example with values [15, 10, 20, 5, 12, 17, 25].

**Follow-ups:**
1. What if the tree wasn't a valid BST? Would inorder still work?
2. How would you find the kth smallest element using inorder?

**Difficulty:** ⭐ Medium | **Time:** 4 min

---

### Question 2.3: Postorder Traversal Use Case
**Question:** Explain why postorder traversal is used for deletion operations. Walk through deleting all nodes from a tree.

**Follow-ups:**
1. What goes wrong if you use preorder for deletion?
2. Implement a postorder traversal iteratively.

**Difficulty:** ⭐ Medium | **Time:** 5 min

---

### Question 2.4: Level-Order Traversal
**Question:** Why does level-order traversal require a queue while preorder/inorder/postorder can use just recursion or a stack?

**Follow-ups:**
1. You're given a level-order output: [1, 2, 3, 4, 5, 6, 7]. Can you reconstruct a unique tree?
2. How would you serialize a tree for transmission over a network?

**Difficulty:** ⭐ Medium | **Time:** 5 min

---

### Question 2.5: Traversal Comparisons
**Question:** For each traversal order, give a concrete use case:
- Preorder: ?
- Inorder: ?
- Postorder: ?
- Level-order: ?

**Follow-ups:**
1. Can you implement all four traversals iteratively?
2. What's the space complexity of recursive vs. iterative traversals?

**Difficulty:** ⭐ Medium | **Time:** 6 min

---

## 🌲 SECTION 3: BINARY SEARCH TREES (BSTs)

### Question 3.1: The BST Invariant
**Question:** State the BST invariant formally. Then explain what breaks if you violate it (insert a value in the wrong position).

**Follow-ups:**
1. Is the invariant local (per node) or global (entire tree)?
2. How does this invariant enable binary search?

**Difficulty:** ⭐ Easy | **Time:** 2 min

---

### Question 3.2: Search in a BST
**Question:** Implement BST search in pseudocode. What's the best-case and worst-case time complexity? Give an example of each.

**Follow-ups:**
1. What input pattern causes worst-case O(n)?
2. How does search compare to binary search on a sorted array?

**Difficulty:** ⭐ Medium | **Time:** 5 min

---

### Question 3.3: Insertion & Duplicates
**Question:** When inserting into a BST, should you:
A) Reject duplicates?
B) Insert them to the left?
C) Insert them to the right?
D) Increment a counter?

Justify your choice with a use case.

**Follow-ups:**
1. How would your implementation change if duplicates were required?
2. What data structure would you augment the BST with?

**Difficulty:** ⭐ Medium | **Time:** 4 min

---

### Question 3.4: Deletion - Three Cases
**Question:** Explain deletion in a BST. What are the three cases? For the "two children" case, why do you use the inorder successor instead of the inorder predecessor?

**Follow-ups:**
1. Implement deletion for a node with two children.
2. What happens if the successor has a right child?

**Difficulty:** ⭐ Hard | **Time:** 8 min

---

### Question 3.5: Finding Inorder Successor
**Question:** Given a node in a BST, find its inorder successor. Does your algorithm change if you have parent pointers?

**Follow-ups:**
1. What if the node is the rightmost node (no successor)?
2. Time and space complexity with/without parent pointers?

**Difficulty:** ⭐ Medium | **Time:** 5 min

---

### Question 3.6: Validating a BST
**Question:** Given a binary tree, validate whether it's a valid BST. Explain why checking `left < parent < right` at each node isn't sufficient.

**Follow-ups:**
1. How would you fix the incorrect approach?
2. What's a clean recursive solution?

**Difficulty:** ⭐ Hard | **Time:** 6 min

---

## ⚖️ SECTION 4: BALANCED BSTs - AVL & RED-BLACK

### Question 4.1: Why Balance Matters
**Question:** Explain in one sentence why balance is necessary. Then give a concrete example showing the performance difference between a balanced and unbalanced tree with 1000 nodes.

**Follow-ups:**
1. What's the height of a balanced tree with 1000 nodes?
2. What's the maximum height of an unbalanced tree with 1000 nodes?

**Difficulty:** ⭐ Easy | **Time:** 2 min

---

### Question 4.2: AVL Balance Factor
**Question:** Define the AVL balance factor. What's the invariant that keeps an AVL tree balanced? If you insert a node that violates this, what do you do?

**Follow-ups:**
1. Is BF = |h_left - h_right| <= 1 checked locally or globally?
2. How does the tree maintain this after insertion?

**Difficulty:** ⭐ Medium | **Time:** 4 min

---

### Question 4.3: AVL Rotations - When & Why
**Question:** You insert a value that causes an imbalance. List the four cases (LL, LR, RR, RL) and which rotation fixes each one.

**Follow-ups:**
1. For the LR case, why do you rotate left first, then right?
2. Can a single rotation ever fail? When do you need two?

**Difficulty:** ⭐ Hard | **Time:** 7 min

---

### Question 4.4: Single vs. Double Rotations
**Question:** Draw an example tree that requires a double rotation (LR case). Trace through both rotations step-by-step.

**Follow-ups:**
1. After the double rotation, is the tree balanced?
2. What's the time complexity of a rotation? Of rebalancing after insertion?

**Difficulty:** ⭐ Hard | **Time:** 8 min

---

### Question 4.5: Red-Black Trees Overview
**Question:** State the 5 rules of red-black trees. Why are red-black trees used in production systems (Java TreeMap) instead of AVL?

**Follow-ups:**
1. Which rule prevents long red chains?
2. How is "black-height" different from regular height?

**Difficulty:** ⭐ Medium | **Time:** 5 min

---

### Question 4.6: Red-Black vs. AVL Trade-off
**Question:** AVL is stricter in balance (smaller height) but requires more rotations on insertion. Red-black is looser but requires fewer rotations. When would you prefer each?

**Follow-ups:**
1. If you rarely insert but frequently search, which is better?
2. What does the Linux kernel use? Why?

**Difficulty:** ⭐ Hard | **Time:** 6 min

---

## 🧩 SECTION 5: TREE PATTERNS

### Question 5.1: Path Sum - Root to Leaf
**Question:** Find all root-to-leaf paths that sum to a target value. Why is backtracking necessary here?

**Follow-ups:**
1. What goes wrong if you forget the backtrack step?
2. How would you modify this for root-to-any-node paths (not just leaves)?

**Difficulty:** ⭐ Medium | **Time:** 6 min

---

### Question 5.2: Path Sum Variations
**Question:** Distinguish between:
- Paths summing to a target value
- Maximum path sum in a tree
- Path sum equals target (any nodes, not just root-to-leaf)

For each, give the algorithm and time complexity.

**Follow-ups:**
1. Which can be solved with postorder traversal?
2. Which requires tracking all paths?

**Difficulty:** ⭐ Hard | **Time:** 8 min

---

### Question 5.3: Tree Diameter
**Question:** Find the longest path between any two nodes in a tree (not necessarily passing through root). Explain why the answer is `max(left_height + right_height + 1)` across all nodes.

**Follow-ups:**
1. How would you track this during a single DFS?
2. What if you needed to return the actual path, not just the length?

**Difficulty:** ⭐ Hard | **Time:** 7 min

---

### Question 5.4: Lowest Common Ancestor (LCA)
**Question:** Find the LCA of two nodes in a BST. How does the algorithm differ from a general binary tree?

**Follow-ups:**
1. For a general tree, what if one or both nodes don't exist?
2. How would you handle this with parent pointers?

**Difficulty:** ⭐ Hard | **Time:** 7 min

---

### Question 5.5: LCA in General Trees
**Question:** In a general binary tree (not BST), find the LCA of two nodes. Explain the three cases: both in left, both in right, split.

**Follow-ups:**
1. What if the tree isn't a BST?
2. How do you handle the case where a node is its own ancestor (is the LCA)?

**Difficulty:** ⭐ Hard | **Time:** 8 min

---

### Question 5.6: Tree Serialization
**Question:** Serialize a tree into a list/string format such that you can uniquely reconstruct it. Why is including "null" markers essential?

**Follow-ups:**
1. Compare preorder vs. level-order serialization. Which is easier to deserialize?
2. How would you serialize to minimize space?

**Difficulty:** ⭐ Hard | **Time:** 8 min

---

### Question 5.7: Serialization Edge Cases
**Question:** Your serialization uses preorder + nulls. Can you deserialize using a single pass? What data structure helps?

**Follow-ups:**
1. Trace through deserialization with input [1, 2, null, null, 3].
2. What if you used level-order serialization instead?

**Difficulty:** ⭐ Hard | **Time:** 8 min

---

## 🔴 SECTION 6: ADVANCED TOPICS (Day 5 Optional)

### Question 6.1: Augmented BSTs
**Question:** Augment a BST to store subtree size at each node. How would this help you find the kth smallest element in O(log n)?

**Follow-ups:**
1. How would you maintain size after insertion/deletion?
2. What if you needed the kth largest instead?

**Difficulty:** ⭐ Hard | **Time:** 7 min

---

### Question 6.2: Order-Statistics Tree
**Question:** Design an order-statistics tree that supports:
- Insert(x)
- Delete(x)
- Rank(x) - how many elements are <= x?
- KthSmallest(k) - return kth smallest

**Follow-ups:**
1. Time complexity for each operation?
2. How would you handle duplicates?

**Difficulty:** ⭐ Very Hard | **Time:** 10 min

---

### Question 6.3: Range Count Queries
**Question:** Given a BST and a range [L, R], count how many nodes fall within this range in O(log n) time.

**Follow-ups:**
1. Would you augment the tree? If so, how?
2. What if you also needed to return the nodes in the range?

**Difficulty:** ⭐ Very Hard | **Time:** 8 min

---

## 📊 COMMON INTERVIEW PATTERNS & TEMPLATES

### Pattern 1: DFS with Backtracking
**When to Use:** Path problems, finding all solutions

**Template Skeleton:**
```
def dfs(node, state):
    if base_case:
        return result
    
    for each child:
        state.add(child)          // Make choice
        recurse on child
        state.remove(child)       // CRITICAL: Undo choice
    
    return accumulated_result
```

**Key Interview Signal:** "Find all paths" or "all solutions"

---

### Pattern 2: Node Value Tracking During Traversal
**When to Use:** Validation, finding properties

**Template Skeleton:**
```
def validate(node, min_val, max_val):
    if node is null:
        return true
    
    if not (min_val < node.val < max_val):
        return false
    
    return validate(node.left, min_val, node.val) and
           validate(node.right, node.val, max_val)
```

**Key Interview Signal:** "Validate BST" or checking properties

---

### Pattern 3: Return Both Value & Height
**When to Use:** Tree diameter, diameter-like problems

**Template Skeleton:**
```
class Result:
    height
    diameter

def compute(node):
    if node is null:
        return Result(height=0, diameter=0)
    
    left = compute(node.left)
    right = compute(node.right)
    
    new_height = 1 + max(left.height, right.height)
    new_diameter = max(left.diameter, right.diameter, 
                       left.height + right.height)
    
    return Result(height=new_height, diameter=new_diameter)
```

**Key Interview Signal:** "Find maximum" something involving paths

---

### Pattern 4: Serialization with Queue
**When to Use:** Level-order serialization/deserialization

**Template Skeleton (Serialize):**
```
def serialize(node):
    result = []
    queue = [node]
    
    while queue:
        current = queue.pop(0)
        if current is null:
            result.append("null")
        else:
            result.append(current.val)
            queue.append(current.left)
            queue.append(current.right)
    
    return result
```

**Key Interview Signal:** "Encode/decode" or "save/restore"

---

## 🚨 CRITICAL PITFALLS & FIXES

### Pitfall 1: Forgetting to Backtrack
**Mistake:** You build a path list but never remove nodes after recursion. All paths show the same ending.

**Fix:** Always call `path.remove()` after recursive call:
```
path.append(node)
dfs(node.left)
dfs(node.right)
path.remove(node)  // MUST HAVE
```

**Interview Impact:** Produces wrong output. Interviewer stops you immediately.

---

### Pitfall 2: Wrong BST Validation
**Mistake:** You check only `left < node < right` at each node, missing the global constraint.

**Fix:** Use min/max bounds that are updated as you traverse:
```
def validate(node, min_val, max_val):
    if node is null: return true
    if node.val <= min_val or node.val >= max_val: return false
    return validate(node.left, min_val, node.val) and
           validate(node.right, node.val, max_val)
```

**Interview Impact:** Produces false positives (accepts invalid trees).

---

### Pitfall 3: Not Capturing Level Size in Level-Order
**Mistake:** You process all nodes in the queue without separating by level.

**Fix:** Capture queue size before loop:
```
while queue:
    level_size = len(queue)  // CRITICAL
    for i in range(level_size):
        node = queue.pop(0)
        // process level
        queue.append(node.left)
        queue.append(node.right)
```

**Interview Impact:** Produces jumbled level-order.

---

### Pitfall 4: Including Null in BST Serialization but Not Handling It
**Mistake:** You serialize nulls but deserialize doesn't expect them, or vice versa.

**Fix:** Be explicit:
- Serialize: Include every null marker
- Deserialize: Check for "null" string explicitly

**Interview Impact:** Deserialization crashes or hangs.

---

### Pitfall 5: Comparing Nodes vs. Values
**Mistake:** In LCA or other patterns, you compare `node1 == node2` (object identity) instead of `node1.val == node2.val`.

**Fix:** Be clear about what you're comparing:
- Object identity: Use `==`
- Value equality: Use `.val == value`

**Interview Impact:** Logic fails silently; LCA returns wrong node.

---

## ✅ INTERVIEW READINESS CHECKLIST

By the end of Week 7, verify:

### Conceptual Mastery
- [ ] Can explain BST invariant and why it matters
- [ ] Can list 4 traversal orders and give 1 use case each
- [ ] Can explain AVL balance vs. red-black trade-off
- [ ] Can design an augmented tree for order-statistics

### Implementation Fluency
- [ ] Can code all 4 traversals (recursive & iterative) under 5 min each
- [ ] Can code BST search, insert, delete under 10 min
- [ ] Can code tree diameter under 10 min
- [ ] Can code serialization + deserialization under 15 min
- [ ] Can code LCA under 8 min

### Pattern Recognition
- [ ] Given a problem, identify if it's a path-sum, diameter, LCA, or serialization pattern
- [ ] Know when to backtrack, when to return (value, height), when to track bounds
- [ ] Can explain why a particular pattern fits a problem

### Production Awareness
- [ ] Can explain why Java uses red-black trees in TreeMap
- [ ] Can explain why Linux scheduler uses red-black trees
- [ ] Understand trade-offs between AVL and red-black
- [ ] Know when to augment a tree and what to augment with

---

## 📚 PRACTICE PROGRESSION

### Stage 1: Fundamentals (40 minutes)
- Question 1.1-1.5 (Tree basics)
- Question 2.1-2.5 (Traversals)
- Question 3.1-3.3 (Basic BST)

### Stage 2: Core Operations (60 minutes)
- Question 3.4-3.6 (Deletion, validation)
- Question 5.1-5.3 (Path sum, diameter)
- Question 4.1-4.2 (Balance, AVL basics)

### Stage 3: Advanced Topics (90 minutes)
- Question 4.3-4.6 (Rotations, red-black, trade-offs)
- Question 5.4-5.7 (LCA, serialization)
- Question 6.1-6.3 (Augmented trees, order-statistics)

---

**Status:** ✅ INTERVIEW QA REFERENCE COMPLETE

