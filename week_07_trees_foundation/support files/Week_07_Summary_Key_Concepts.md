# 📌 Week_07_Summary_Key_Concepts.md

**Version:** v1.2 (Quick Reference Card)  
**Purpose:** Condensed essential concepts for rapid recall  
**Audience:** Active learners during problem-solving  
**Status:** ✅ PRODUCTION-READY

---

## 🌳 TREE ANATOMY (Day 1 Core)

### Basic Definitions
- **Root:** Top node, no parent
- **Parent/Child:** Hierarchical relationship
- **Leaf:** Node with no children
- **Internal Node:** Node with ≥1 child
- **Height (h):** Longest path from node to leaf (leaf = 0)
- **Depth (d):** Distance from root to node (root = 0)
- **Tree Height:** Height of root

### Tree Categories
| Category | Property | Impact |
|---|---|---|
| **Full** | Every node has 0 or 2 children | Mathematical nice properties |
| **Complete** | All levels filled except last (left-justified) | Array representation possible |
| **Balanced** | Height ≈ log(n) | Ensures O(log n) operations |
| **Degenerate** | Chain-like (each node 1 child) | Becomes O(n) linked list |

---

## 🔁 TRAVERSALS (Day 1 Core)

### Four Orders Quick Reference

```text
        1
       / \
      2   3
     / \
    4   5
```

| Traversal | Order | Pattern | Use Case | Code |
|---|---|---|---|---|
| **Preorder** | 1,2,4,5,3 | Parent first | Tree copy, expression evaluation | `Visit → Left → Right` |
| **Inorder** | 4,2,5,1,3 | Parent middle | BST sorted sequence | `Left → Visit → Right` |
| **Postorder** | 4,5,2,3,1 | Parent last | Deletion, cleanup | `Left → Right → Visit` |
| **Level-order** | 1,2,3,4,5 | Breadth-first | BFS, layer processing | Queue: enqueue parent's children |

### Implementation Keys
- **Recursive:** Clean, but O(h) stack depth (risky for h=10,000+)
- **Iterative:** Safe, explicit stack control
- **Level-order:** Always needs `Queue<T>`, capture size before loop

---

## 🌳 BST OPERATIONS (Day 2 Core)

### The BST Invariant
**Every node: All left descendants < node < all right descendants**

```
         5
       /   \
      3     7
     / \   / \
    1   4 6   9
```

This invariant enables binary search.

### Search: O(log n) avg, O(n) worst
```
Compare target with node:
- target < node.val? Go left
- target > node.val? Go right
- target == node.val? Found!
```

### Insert: Maintain invariant
```
Find correct leaf position:
- value < node? Go left
- value > node? Go right
- value == node? Skip (duplicate)
Create new node at position
```

### Delete: Three Cases
| Case | Approach |
|---|---|
| **Leaf (no children)** | Remove directly |
| **One child** | Replace node with child (bypass it) |
| **Two children** | Find inorder successor (min in right subtree), copy value, delete successor |

---

## ⚖️ BALANCED BSTs (Day 3 Core)

### Why Balance?
Unbalanced (sorted input): O(n) ← BAD  
Balanced tree: O(log n) ← GOOD

### AVL Trees
- **Balance factor:** |height(left) - height(right)|
- **Invariant:** BF ≤ 1 at every node
- **Rotations (4 cases):**
  - LL: Left-left → Single right rotation
  - RR: Right-right → Single left rotation
  - LR: Left-right → Left rotation then right rotation
  - RL: Right-left → Right rotation then left rotation

### Red-Black Trees (Production Standard)
- **5 Rules:**
  1. Every node is red or black
  2. Root is black
  3. All leaves (NIL) are black
  4. If node red, both children black (no red-red)
  5. Every path from node to NIL has same black count
  
- **Trade-off:** Less perfect than AVL but fewer rotations (better constant factors)
- **Used by:** Java (TreeMap), C++ (std::map), Linux Kernel

---

## 🧩 TREE PATTERNS (Day 4 Core)

### Path Sum (DFS + Backtracking)
```
Problem: Find all root-to-leaf paths summing to target

Approach:
1. DFS traversal
2. Add current node to path
3. If leaf and sum == target, save path
4. CRITICAL: Remove node after recursion (backtrack)
```

### Tree Diameter (DP)
```
Problem: Longest path between ANY two nodes

Insight: Diameter at node = left_height + right_height + 1
Answer: Max diameter across all nodes

DP: Return height to parent, update max diameter
```

### Lowest Common Ancestor (LCA)
```
Problem: Find deepest common ancestor of two nodes

Three cases:
1. Both nodes in left subtree → LCA in left
2. Both nodes in right subtree → LCA in right
3. One left, one right → Node itself is LCA (paths diverge here)
```

### Serialization (Preorder + Nulls)
```
Key insight: Preorder + null markers = unique encoding

Serialize: Preorder traversal, include nulls as markers
Example: [1, 2, null, null, 3] → root 1, left 2 (leaf), right 3 (leaf)

Deserialize: Queue of tokens, recursive preorder reconstruction
```

---

## 📊 COMPLEXITY TABLE

| Operation | BST Best | BST Avg | BST Worst | AVL | RB Tree |
|---|---|---|---|---|---|
| Search | O(log n) | O(log n) | O(n) | O(log n) | O(log n) |
| Insert | O(log n) | O(log n) | O(n) | O(log n) | O(log n) |
| Delete | O(log n) | O(log n) | O(n) | O(log n) | O(log n) |
| Space | O(n) | O(n) | O(n) | O(n) | O(n) |

**Key:** Balanced trees guarantee O(log n); unbalanced BST degenerates to O(n).

---

## 🔴 COMMON MISTAKES TO AVOID

| Mistake | Symptom | Fix |
|---|---|---|
| **Forget null check** | `NullReferenceException` | Add `if (node == null) return;` |
| **Wrong queue.Count usage** | Processes next level in same loop | Capture `levelSize` before loop |
| **Forget to backtrack** | All paths show same ending | `path.RemoveAt()` after recursion |
| **Wrong BST validation** | Accepts invalid trees | Use min/max range: `node.val <= min \|\| node.val >= max` |
| **Wrong successor finding** | NullRef or incorrect value | `while (node.left != null)` to find min |
| **Forget nulls in serialization** | Can't deserialize | Include "null" for every missing child |
| **Not updating parent links** | Orphaned subtrees | Return node from each operation |

---

## 🎯 INTERVIEW RED FLAGS (Pattern Signals)

When you see these phrases, IMMEDIATELY think:

| Phrase | Pattern | Approach |
|---|---|---|
| "traverse", "visit all" | Traversal | Which order? (pre/in/post/level) |
| "find in ordered" | BST Search | Binary decision tree |
| "path sum", "sum of nodes on path" | Path Sum | DFS + backtracking |
| "longest path", "diameter" | Tree Diameter | DP: left_h + right_h + 1 |
| "ancestor", "common ancestor" | LCA | Recursive: both left? both right? split? |
| "encode", "serialize" | Serialization | Preorder + nulls |
| "balanced", "maintain O(log n)" | Balancing | AVL or Red-Black concept |
| "kth smallest" | Order-Statistics | Inorder traversal counter or augmented |

---

## ✅ QUICK VALIDATION CHECKLIST

### Can You Immediately Code:
- [ ] Preorder recursive AND iterative? (2 min each)
- [ ] Inorder iterative? (2 min)
- [ ] BST search? (1 min)
- [ ] BST insert? (2 min)
- [ ] BST delete (all cases)? (3 min)
- [ ] Tree diameter? (2 min)
- [ ] Path sum with backtrack? (3 min)
- [ ] Serialize/deserialize? (3 min)

### Can You Immediately Explain:
- [ ] Why BST invariant matters?
- [ ] When degenerate case breaks it?
- [ ] Why balance needed?
- [ ] AVL vs Red-Black trade-off?
- [ ] When to use each traversal?
- [ ] How serialization ensures uniqueness?

### Time Budget:
If YES to all above → **You're ready for interviews**  
If NO to some → **Practice those specifically**

---

## 📐 FORMULAS & THEOREMS

### Tree Properties
- Height of balanced tree with n nodes: h ≈ log₂(n)
- Time per operation with balance: O(h) = O(log n)
- Time per operation without balance: O(h) = O(n) worst case

### BST Inorder Property
- Inorder traversal of BST produces sorted sequence
- Proof: left < node < right, apply recursively

### Rotation Property (AVL/RB)
- Left/right rotation preserves BST property
- Both operations O(1) pointer updates

---

## 🧠 MENTAL MODELS TO RETAIN

1. **Tree = Hierarchy:** Parent-child relationships organizing data
2. **BST = Ordered Search:** Invariant enables binary search on dynamic set
3. **Traversal = Visitor:** Systematic way to process each node
4. **Balance = Insurance:** Guarantees O(log n) by height limit
5. **Rotation = Restructuring:** Local pointer changes maintain invariant
6. **Serialization = Encoding:** Preorder + nulls uniquely represents structure
7. **Backtracking = Undo:** Remove when done exploring, try alternatives

---

## 🏆 THIS WEEK'S ESSENCE

> **"Trees are hierarchical ordered structures. Traversals visit every node. BST invariant enables searching. Balance maintains O(log n). Patterns combine traversal + algorithm. Serialization encodes structure uniquely."**

---

**Status:** ✅ QUICK REFERENCE READY
