# 📌 Week_07_Guidelines.md

**Version:** v1.2 (Comprehensive Learning Guide)  
**Purpose:** Strategic overview and navigation guide for Week 7  
**Audience:** Learners planning their week  
**Status:** ✅ PRODUCTION-READY

---

## 🎯 WEEK 7 AT A GLANCE

**Theme:** From Linear to Hierarchical Thinking

This week marks a fundamental shift in your DSA journey. You move beyond arrays and linked lists (linear structures) into **hierarchical data structures**—trees—which model relationships, dependencies, and ordered collections elegantly. By week's end, you'll understand why trees power databases, file systems, and search engines.

**Big Picture Learning Arc:**
```
Day 1: Understand tree shape          → Mental model: Parent-child hierarchy
Day 2: Search efficiently             → Mental model: BST invariant enables binary search
Day 3: Guarantee efficiency           → Mental model: Balance ensures height ≈ log n
Day 4: Solve complex problems         → Mental model: Patterns (paths, diameter, LCA)
Day 5: Optimize further               → Mental model: Augmentation trades space for speed
```

---

## 📊 WEEK OVERVIEW & TIME ALLOCATION

| Day | Topic | Duration | Key Activities |
|---|---|---|---|
| **Day 1** | Tree Anatomy & Traversals | 120 min | Hand-trace 4 orders; understand O(height) |
| **Day 2** | BST Operations | 150 min | Search, insert, delete (3 cases); successor logic |
| **Day 3** | Balanced BSTs & Iterations | 150 min | Rotations, AVL vs. RB; iterative traversals |
| **Day 4** | Tree Patterns | 180 min | Diameter, LCA, path sum, serialization |
| **Day 5** | Advanced (Optional) | 120 min | Augmented trees, order-statistics |
| **Review & Integration** | 60 min | Mock interview, mixed problems |
| **TOTAL** | **Week 7** | **780 min (13 hours)** | |

**Pacing:** ~2.5 hours/day (Days 1-3), 3 hours (Day 4), flexible (Day 5)

---

## 🌳 DAY-BY-DAY CONCEPT OVERVIEW

### DAY 1: TREE ANATOMY & TRAVERSALS

**Core Concepts (Understand These First):**

- **Height vs. Depth** – Height measures downward (longest path to leaf); Depth measures upward (distance from root). Leaf height = 0; Root depth = 0.
  - *Why it matters:* Height determines operation time. Balanced tree: O(log n). Degenerate: O(n).

- **Full/Complete/Balanced Trees**
  - **Full:** Every node has 0 or 2 children (mathematical properties)
  - **Complete:** All levels filled except last (left-justified). Enables array storage.
  - **Balanced:** Height ≈ log₂(n). Guarantees O(log n) operations.

- **Four Traversal Orders**
  - **Preorder (NLR):** Parent → Left → Right. Use: Tree copy, expression evaluation.
  - **Inorder (LNR):** Left → Parent → Right. Use: BST sorted sequence.
  - **Postorder (LRN):** Left → Right → Parent. Use: Deletion, cleanup.
  - **Level-order (BFS):** Layer-by-layer. Use: Layer-specific processing, tree serialization.

**Key Insight:** Each traversal encodes structural information. Inorder reveals sorted data in BST. Preorder identifies root first (useful for reconstruction).

**Activities:**
- Trace all 4 orders on 3 different trees (balanced, left-skewed, right-skewed)
- Calculate height/depth for each node
- Understand: Why leaf has height 0, not 1? Why root depth = 0?

**Signal of Mastery:** Trace any traversal correctly in <2 minutes per order.

---

### DAY 2: BINARY SEARCH TREES (BSTs)

**Core Concepts:**

- **The BST Invariant** – For every node: ALL left descendants < node < ALL right descendants. This global constraint (not just local) enables binary search.
  - *Why global?* Left subtree of right child must be > parent. Local checks miss this.

- **Search, Insert, Delete**
  - **Search:** O(log n) best (balanced), O(n) worst (degenerate). Binary decision at each node.
  - **Insert:** Maintain invariant; find leaf position; create node.
  - **Delete:** Three cases—
    - Leaf: Remove directly.
    - One child: Bypass (child replaces parent).
    - Two children: Find inorder successor, copy value, delete successor recursively.

- **Inorder Successor** – Smallest value > current node. Located at: leftmost node in right subtree.
  - *Why it works:* It's immediately larger; right subtree has no smaller values; no left children means easy removal.

- **Degenerate Trees** – Sorted input [1,2,3,4,5] creates linked-list-like tree. Height = n. Operations become O(n). **Motivation for balancing.**

**Key Insight:** The invariant is elegant but fragile. Deletion is complex because two-child case requires successor logic. Balanced trees fix the degenerate case.

**Activities:**
- Insert sequence [5, 3, 7, 2, 4, 6, 8]; observe balanced tree (height ≈ 3)
- Insert sequence [1, 2, 3, 4, 5]; observe degenerate tree (height = 5)
- Delete all three cases; verify invariant maintained
- Validate BST using min/max bounds (not just local comparison)

**Signal of Mastery:** Code BST search, insert, delete cleanly in <10 min total (no notes).

---

### DAY 3: BALANCED BSTs & ITERATIVE TRAVERSALS

**Core Concepts:**

- **Balance Factor (AVL)** – BF = height(left) - height(right). Invariant: |BF| ≤ 1 at every node.
  - *Why this bound?* Ensures height ≤ 1.44 * log₂(n). Still O(log n) guaranteed.

- **Rotations** – Local rebalancing without changing BST property.
  - **LL case:** Left-left imbalance → Single right rotation.
  - **RR case:** Right-right imbalance → Single left rotation.
  - **LR case:** Left-right imbalance → Left rotate (child), right rotate (parent).
  - **RL case:** Right-left imbalance → Right rotate (child), left rotate (parent).
  - *Key:* Each rotation is O(1). Rebalancing is O(log n) per insertion.

- **Red-Black Trees** – Production standard. 5 rules ensure balance without perfect AVL constraint.
  - Rules: (1) Every node is red or black. (2) Root is black. (3) Nulls are black. (4) No red-red. (5) Same black-height on all paths.
  - *Trade-off:* Less perfect balance than AVL but **fewer rotations** (better constants in practice).
  - Used by: Java TreeMap, C++ std::map, Linux kernel scheduler.

- **Iterative Traversals** – Use explicit stack to simulate recursion.
  - **Inorder Iterative:** Go left until null → pop and process → go right. Clean, easy pattern.
  - **Postorder Iterative:** Trickier. Need previous pointer to know when right child is done.

**Key Insight:** Balance is about *guarantees*. Degenerate tree worst-case is unbounded. Balanced tree bounds height to log n. Red-black wins in practice because fewer rotations = smaller constant factors.

**Activities:**
- Identify imbalance type (LL, RR, LR, RL) from 5 unbalanced trees
- Apply correct rotations; verify balance restored + BST property maintained
- Trace iterative inorder on 10-node tree
- Research: Why does Java use red-black instead of AVL?

**Signal of Mastery:** Rotate 4-5 imbalanced trees correctly. Understand RB vs. AVL trade-off.

---

### DAY 4: TREE PATTERNS

**Core Concepts:**

- **Path Sum Pattern** – DFS + backtracking. Find all root-to-leaf paths with target sum.
  - *Template:* Add node → Recurse left & right → Remove node (backtrack).
  - *Why backtrack?* To explore ALL paths; undo choice before trying next branch.

- **Tree Diameter Pattern** – Longest path between ANY two nodes (not necessarily through root).
  - *Key insight:* At each node, diameter = left_height + right_height (+ 1 for node itself).
  - *Return strategy:* Return both (height, max_diameter) from DFS. Height needed for parent; diameter is answer.

- **Lowest Common Ancestor (LCA)** – Deepest common ancestor of two nodes.
  - *Three cases:* Both in left subtree → recurse left. Both in right → recurse right. One left, one right → current node is LCA.
  - *BST shortcut:* LCA is first node where value falls between two targets (v1 < node < v2).

- **Serialization/Deserialization** – Encode tree uniquely; rebuild it.
  - *Key:* Null markers are ESSENTIAL. Without them, can't distinguish structure from traversal.
  - *Preorder approach:* Visit parent first; queue-based deserialization reconstructs cleanly.
  - *Why preorder?* Parent comes before children; recursive reconstruction is natural.

**Key Insight:** Patterns are combinations of DFS + clever return values. Path sum = DFS + backtracking. Diameter = DFS + tracking both height & max. LCA = DFS + logical case distinction. Serialization = DFS + null markers.

**Activities:**
- Trace path sum on tree with target; verify no duplicate paths; understand backtracking necessity
- Calculate diameter by hand (at each node: left_h + right_h); compare to longest actual path
- Find LCA pairs in BST (use shortcut); find in general tree (use 3-case logic)
- Serialize a tree [1, 2, null, null, 3]; deserialize step-by-step using queue

**Signal of Mastery:** Code all 4 patterns cleanly without notes. Recognize pattern in new problem instantly.

---

### DAY 5: ADVANCED & INTEGRATION (Optional)

**Core Concepts:**

- **Augmented Trees** – Store extra metadata at each node (subtree size, count, sum, etc.).
  - *Benefit:* Enable O(log n) queries on custom properties.
  - *Maintenance:* Update metadata bottom-up after insertion/deletion.

- **Order-Statistics Queries** – Find kth smallest in O(log n) using subtree sizes.
  - *Strategy:* If k ≤ left.size, go left. Else k = k - left.size - 1; go right.
  - *Why log n?* Binary search-like tree traversal; size comparison at each node.

- **Rank Queries** – Count elements ≤ x efficiently.
  - *Augmentation:* Store subtree size. Count = left subtree count + 1 (if node ≤ x) + right subtree count (adjusted).

- **Pattern Integration** – Mix concepts (e.g., diameter + path sum, LCA + rank).
  - Complex problems often require combining 2-3 patterns.

**Key Insight:** Augmentation trades **space for speed**. More storage (per node) enables faster queries (query time unchanged, but augmentation info available). Know when to augment and what to store.

**Activities:**
- Augment BST with subtree sizes; find kth smallest in O(log n)
- Count elements in range [L, R] using augmented tree
- Mix two patterns: diameter + path sum, LCA + count
- Research: When is augmentation overkill? (Rare queries, frequent updates = bad trade-off)

**Signal of Mastery:** Understand when/how to augment. Code 2-3 augmented operations. Recognize augmentation opportunities in new problems.

---

## 🔗 HOW TOPICS CONNECT

```
Day 1: Tree Shape
  ↓
Day 2: BST (exploits shape for search)
  ↓
Day 3: Balance (maintains shape ≈ log n)
  ↓
Day 4: Patterns (elegant solutions on stable shape)
  ↓
Day 5: Augmentation (enhance stable shape with metadata)
```

**Key Relationships:**

| Topic | Builds On | Enables |
|---|---|---|
| **Height/Depth** | Tree basics | Understanding O(height) complexity |
| **Traversals** | Tree navigation | All subsequent patterns |
| **BST Invariant** | Ordering principle | Binary search; sorted iteration |
| **Balanced Trees** | Height guarantee | O(log n) guarantees in production |
| **Rotations** | Pointer manipulations | Rebalancing without data movement |
| **Tree Patterns** | DFS + clever returns | Complex problem-solving |
| **Augmentation** | Extra metadata | Specialized queries |

---

## 📚 COMPARISON TABLES

### Traversal Orders: When to Use Each

| Order | Sequence | Use Case | Example |
|---|---|---|---|
| **Preorder** | Parent → Left → Right | Tree copy, expression evaluation, prefix notation | Copy a tree node-by-node |
| **Inorder** | Left → Parent → Right | BST sorted sequence, symmetric structure | Print BST in sorted order |
| **Postorder** | Left → Right → Parent | Deletion, cleanup, postfix notation | Delete tree bottom-up |
| **Level-order** | Breadth-first, layer-by-layer | Layer processing, tree serialization, BFS | Serialize tree for transmission |

### Tree Types: Properties & Use Cases

| Type | Definition | Property | Use Case |
|---|---|---|---|
| **Full** | 0 or 2 children per node | Mathematical elegance | Theoretical analysis |
| **Complete** | All levels filled except last (left-justified) | Array representation | Heap implementation |
| **Balanced** | Height ≈ log₂(n) | O(log n) operations | Search trees |
| **Degenerate** | Chain-like (1 child per node) | Height = n | Worst-case scenario (avoid!) |

### Balanced Trees: AVL vs. Red-Black

| Aspect | AVL | Red-Black |
|---|---|---|
| **Balance Factor** | \|h(left) - h(right)\| ≤ 1 | Looser (color-based rules) |
| **Height Bound** | h ≤ 1.44 * log₂(n) | h ≤ 2 * log₂(n) |
| **Rotations per Insert** | More (stricter balance) | Fewer (looser balance) |
| **Constant Factors** | Larger (more rotations) | Smaller (fewer rotations) |
| **When to Use** | Frequent searches, rare inserts | Balanced insert/delete load |
| **Production** | Rare (except educational) | Common (Java, C++, Linux) |

### BST Operations: Complexity & Cases

| Operation | Best | Average | Worst | Notes |
|---|---|---|---|---|
| **Search** | O(log n) | O(log n) | O(n) | Worst = degenerate tree |
| **Insert** | O(log n) | O(log n) | O(n) | Maintain invariant |
| **Delete** | O(log n) | O(log n) | O(n) | Successor finding is key |
| **Successor Find** | O(log n) | O(log n) | O(n) | Balanced = O(log n) |
| **Balanced Insert** | O(log n) | O(log n) | O(log n) | Rotation cost included |

---

## 🎯 KEY INSIGHTS TO REMEMBER

### 1. Height = Operation Time
**Insight:** Every tree operation (search, insert, delete) visits O(height) nodes. Balanced tree: height ≈ log n. Unbalanced tree: height = n. **Factor of 1000x difference on 1M items.**

### 2. The BST Invariant is Global
**Insight:** Can't just check left < parent < right locally. Must enforce globally. Use min/max bounds during validation.

### 3. Deletion is the Complex Case
**Insight:** Leaf deletion is trivial. One-child deletion is straightforward. Two-child deletion requires **inorder successor** logic—smallest value larger than deleted node.

### 4. Balance Guarantees Everything
**Insight:** Unbalanced BST degenerates to O(n) linked list on sorted input. AVL or red-black guarantee O(log n) always. Production systems use red-black because rotations are faster despite looser balance.

### 5. Patterns are Combinations
**Insight:** Tree patterns aren't magic. They're combinations of:
- DFS traversal
- Clever return values (both height + diameter, both value + count)
- Backtracking (explore all paths)
- Null markers (encode structure)

### 6. Augmentation Trades Space for Speed
**Insight:** Store extra metadata at each node (size, sum, count). Enables O(log n) specialized queries instead of O(n) traversal. When: many queries, few updates. When not: few queries, many updates.

---

## 🚨 COMMON PITFALLS & HOW TO AVOID THEM

| Pitfall | Symptom | Fix |
|---|---|---|
| **Wrong BST Validation** | Accepts invalid trees | Use min/max bounds; enforce globally |
| **Forget to Backtrack** | All paths show same ending | Call remove() after recursion |
| **Not Capturing Level Size** | Processes multiple levels in one loop | Capture queue.size() before loop |
| **Forget Null Markers** | Can't deserialize; ambiguous structure | Include "null" for every missing child |
| **Confuse Height/Depth** | Off-by-one errors; wrong complexity | Height = down from node; Depth = up from root |
| **Successor Finding** | NullRef or wrong value | while (node.left != null); returns min |
| **Not Returning Both Values** | Loses height or diameter info | Return (height, max_diameter) from DFS |

---

## 💡 PRODUCTION INSIGHTS

### Java TreeMap Uses Red-Black Trees
**Why?** Frequent insertions + deletions in balanced state. Red-black requires fewer rotations than AVL. Constant factors matter at scale.

### Linux Kernel Scheduler Uses Red-Black Trees
**Why?** CFS (Completely Fair Scheduler) stores processes in RB tree, keyed by virtual runtime. Needs O(log n) min-finding (leftmost) and efficient updates. RB tree provides both with minimal rotations.

### Databases Use B-Trees (Not Binary Trees)
**Why?** B-trees have 100+ children per node. Minimizes disk I/O (each level = one disk access). Binary trees have high tree height; B-trees reduce depth dramatically.

---

## 📖 LEARNING METHODOLOGY

**Effective Learning Sequence for Trees:**

1. **Visualize First** – Draw trees, trace traversals by hand. Build intuition.
2. **Understand Invariants** – Why does BST work? Why does balance matter? Understand the "why," not just the mechanism.
3. **Code Incrementally** – Start with search (1 comparison per level). Add insert. Add delete (hardest). Don't try all at once.
4. **Test Thoroughly** – Edge cases: null, single node, degenerate trees. Test all three deletion cases separately.
5. **Connect to Production** – See trees in Java, Linux, databases. Understand why each system chose their implementation.

---

## 🎓 WEEK 7 LEARNING OBJECTIVES

**By end of week, you should:**

✅ **Understand Conceptually:**
- Why height ≈ log n is critical for performance
- How BST invariant enables binary search
- Why balance is necessary (degenerate case)
- How rotations maintain balance
- When to use each traversal order
- How tree patterns combine DFS + clever design
- When to augment trees and why

✅ **Implement Fluently:**
- All 4 traversals (recursive + at least 2 iterative)
- BST search, insert, delete (all 3 cases)
- Tree validation with bounds
- Tree diameter, LCA, path sum, serialization
- Iterative inorder (at least)

✅ **Apply Strategically:**
- Recognize patterns in problems
- Choose right data structure (BST vs. balanced vs. augmented)
- Trace complex scenarios (insertion, deletion, rebalancing)
- Explain real-world systems using trees
- Design augmented trees for custom queries

---

## 📝 WEEK 7 CHECKLIST

### Pre-Week Preparation
- [ ] Review linked lists and recursion (prerequisites)
- [ ] Ensure you understand O(log n) and tree height relationship
- [ ] Set up IDE with tree node class template

### Daily Completion
- [ ] Day 1: Trace all 4 traversals correctly
- [ ] Day 2: Code BST operations cleanly
- [ ] Day 3: Understand rotations and RB trees
- [ ] Day 4: Implement all 4 patterns
- [ ] Day 5: (Optional) Explore augmented trees

### Week Completion
- [ ] All daily checklists done (Day 1-5 from Daily_Progress_Checklist.md)
- [ ] Answered 30+ questions from Interview_QA_Reference.md
- [ ] Solved 15+ problems from Problem_Solving_Roadmap.md
- [ ] Can code all major operations without notes
- [ ] Explained a tree concept to someone else
- [ ] Reviewed production systems (Java, Linux, databases)

---

**Status:** ✅ GUIDELINES COMPLETE

