# 📚 Week_07_Guidelines.md

**Version:** v1.2 (Integrated with v12 Narrative Architecture)  
**Week:** 7 – Trees & Balanced Search Trees  
**Status:** ✅ PRODUCTION-READY  
**Purpose:** Complete learning framework for Week 7 mastery

---

## 🎯 WEEK 7 AT A GLANCE

### Primary Goal
Master tree structures (anatomy, traversals), BST operations (search/insert/delete), balanced BST concepts (AVL/Red-Black), and practical tree patterns (diameter, path sum, serialization).

### Why This Week Comes Here
Trees generalize linear structures into hierarchies. BSTs and balanced BSTs are CENTRAL to ordered data management, enabling O(log n) operations that hash tables cannot provide (no ordering). This week unlocks:
- Ordered dynamic sets
- Range queries ("find all elements between x and y")
- Self-balancing structures (critical for real systems like Linux kernel, Java/C++ libraries)

### MIT Alignment
- **6.006 (Introduction to Algorithms):** Trees, traversals, BST operations, basic balancing
- **6.046 (Design of Usable and Reliable Interactive Systems):** Augmented trees, order-statistics, advanced analysis

---

## 📅 WEEK 7 STRUCTURE (5 Days)

### 🌲 Day 1: Binary Trees & Traversals
**Focus:** Understanding tree anatomy and systematic node visitation.

**Core Topics:**
- Tree anatomy (root, parent, child, leaf, height, depth)
- Four traversal orders (preorder, inorder, postorder, level-order)
- Recursive vs iterative implementations
- Use cases (expression trees, serialization)

**Learning Arc:** Learn *what* trees are → Learn *how* to visit every node systematically → Master *when* to use each traversal.

**Key Insight:** Traversals are the foundation for all tree algorithms. Mastering them recursively and iteratively is non-negotiable.

---

### 🌳 Day 2: Binary Search Trees (BSTs)
**Focus:** Ordered dynamic set operations using the BST invariant.

**Core Topics:**
- BST property: left < root < right (invariant)
- Search operation (binary search on tree structure)
- Insert operation (maintaining invariant)
- Delete operation (three cases: leaf, one child, two children)
- Inorder traversal → sorted sequence
- Degenerate case (linked-list-like when input sorted)

**Learning Arc:** Understand *why* BST invariant matters → Implement *all* operations correctly → Recognize *when* degenerate case kills performance.

**Key Insight:** BST is elegant but fragile. Balance is essential for real systems.

---

### ⚖️ Day 3: Balanced BSTs (AVL & Red-Black Overview)
**Focus:** Maintaining O(log n) guarantees through rebalancing.

**Core Topics:**
- Why balance matters (height bounds)
- AVL trees (balance factor, rotations: LL, LR, RR, RL)
- Red-Black trees (5 coloring rules, black-height invariant)
- Comparison (AVL vs Red-Black trade-offs)
- Real systems (Java TreeMap/TreeSet, C++ std::map, Linux kernel)

**Learning Arc:** Understand *the problem* (degenerate BST) → Learn *one approach* (AVL) → Learn *production approach* (Red-Black) → See *real implementations*.

**Key Insight:** Balance requires careful invariant maintenance. Red-Black trees trade perfect balance for simpler rebalancing logic.

---

### 🧩 Day 4: Tree Patterns (Algorithms on Trees)
**Focus:** Solving practical problems using tree traversal + dynamic programming.

**Core Topics:**
- Path sum problems (root-to-leaf, finding paths matching criteria)
- Tree diameter (longest path between any two nodes)
- Lowest Common Ancestor (LCA) – finding deepest common ancestor
- Serialization/Deserialization (encoding/decoding tree structure)

**Learning Arc:** Master *each pattern* individually → Recognize *pattern signals* in problems → Combine patterns for complex problems.

**Key Insight:** Tree patterns are interview favorites because they combine traversal + algorithm design.

---

### 🔢 Day 5: Augmented BSTs & Order-Statistics (Optional Advanced)
**Focus:** Extending BST with additional metadata for complex queries.

**Core Topics:**
- Augmentation principle (storing metadata at each node)
- Order-statistics trees (kth smallest in O(log n))
- Rank queries ("how many ≤ x" in O(log n))
- Range count queries ("count elements in [L, R]" in O(log n))
- Real systems (database range indexes)

**Learning Arc:** Understand *augmentation concept* → See *practical application* (order-statistics) → Recognize *real-world use*.

**Key Insight:** Augmentation extends BST capabilities without changing fundamental structure.

---

## 💡 KEY CONCEPTS ACROSS THE WEEK

### Mental Models to Internalize

| Concept | Mental Model | Visual |
|---|---|---|
| **Tree Traversal** | Systematic visitor: preorder (parent first), inorder (parent middle), postorder (children first), level-order (breadth-first) | Draw tree, show visit order |
| **BST Invariant** | Ordered hierarchy: every node divides space into two regions (left < self < right) | Draw tree with annotations |
| **Tree Balance** | Height guarantee: balanced tree ensures O(log n) depth, degenerate tree becomes O(n) linked list | Show balanced vs degenerate |
| **Rotation** | Local restructuring: swap parent/child to rebalance without violating BST property | Animate rotation step-by-step |
| **Serialization** | Unique encoding: preorder + null markers can reconstruct exact tree structure uniquely | Show encoding → decoding |

### Patterns to Recognize

**Interview Red Flags:** When you see these phrases, think tree patterns:
- "traverse" → Which traversal? (preorder for copy, inorder for sorted, postorder for cleanup)
- "ancestor" → LCA pattern
- "path" → DFS + backtracking
- "distance/diameter" → DP + height calculation
- "encode/decode" → Serialization with null markers
- "kth smallest" → Augmented BST with rank

### Trade-offs to Understand

| Choice | Trade-off | Impact |
|---|---|---|
| **Recursion vs Iteration (Traversal)** | Elegance vs stack depth | Recursive fine for balanced, iterative for deep |
| **BST vs Hash Table** | Order vs speed | BST O(log n) ordered, hash O(1) unordered |
| **AVL vs Red-Black** | Perfect balance vs simpler rebalancing | AVL more balanced, RB fewer rotations |
| **Preorder vs Inorder Serialization** | Structure vs sorting | Preorder/inorder both work, different properties |

---

## 🎓 LEARNING METHODOLOGY

### Phase 1: Understanding (Days 1-2)
**Goal:** Understand concepts deeply, not just memorize algorithms.

**Approach:**
1. Read full narrative explanation (Chapter 1-3 of instructional file)
2. Study inline diagrams and trace tables
3. Ask: "What problem does this solve?" and "Why was it invented?"

**Validation:** Can you explain to someone else why you'd use this?

### Phase 2: Implementation (Days 3-4)
**Goal:** Implement operations from scratch, guided first then unguided.

**Approach:**
1. Follow guided implementation in instructional file
2. Implement each operation (search, insert, delete, rotate, serialize)
3. Trace through examples manually
4. Test with edge cases (single node, degenerate, balanced)

**Validation:** Can you code it without looking at reference?

### Phase 3: Application (Days 5-7)
**Goal:** Solve problems using tree patterns, recognize when to use trees.

**Approach:**
1. Practice problems from LeetCode (Stage 1-3 ladder)
2. Mix patterns (combine traversal + DP for complex problems)
3. Practice explaining your approach (interview style)

**Validation:** Can you solve hard problems and explain your thinking?

---

## 📊 WEEK 7 LEARNING LADDER

### 🟢 GREEN: Foundational (Stage 1)
**Problems:** Easy, canonical implementations

**You should be able to:**
- ✅ Implement all 4 traversals (recursive + iterative)
- ✅ Implement BST search, insert, delete
- ✅ Trace through operations manually
- ✅ Understand tree traversal orders and use cases
- ✅ Validate BST property

**Expected time:** 2-3 hours
**Key problems:** LeetCode #94, #102, #104, #98, #700

---

### 🟡 YELLOW: Intermediate (Stage 2)
**Problems:** Medium, pattern variations, edge cases

**You should be able to:**
- ✅ Solve BST deletion with all 3 cases
- ✅ Recognize tree diameter and path sum patterns
- ✅ Write DFS with backtracking for path problems
- ✅ Understand when degenerate BST becomes problem
- ✅ Trace rotations (AVL conceptually)

**Expected time:** 3-4 hours
**Key problems:** LeetCode #450, #543, #112, #236 (LCA)

---

### 🔴 RED: Advanced (Stage 3)
**Problems:** Hard, pattern combinations, real-world complexity

**You should be able to:**
- ✅ Serialize/deserialize trees with null markers
- ✅ Solve complex path problems (any two nodes, not just root→leaf)
- ✅ Implement balanced BST rotations correctly
- ✅ Augment BST for order-statistics queries
- ✅ Explain production trade-offs (AVL vs RB, recursion vs iteration)

**Expected time:** 4-5 hours
**Key problems:** LeetCode #297, #124, #114 (Flatten)

---

## ✅ DAILY CHECKLIST

### Day 1 Checklist: Binary Trees & Traversals
- [ ] Understand tree anatomy (root, parent, child, leaf, height, depth)
- [ ] Trace all 4 traversals by hand on example tree
- [ ] Implement preorder (recursive + iterative)
- [ ] Implement inorder (iterative with stack state machine)
- [ ] Implement postorder (recursive first, then understand iterative)
- [ ] Implement level-order (BFS with queue)
- [ ] Understand use cases (expression trees, serialization preview)
- [ ] Solve #94 (Inorder Traversal) - verify all traversals working

**Exit Criteria:** Can implement all traversals from scratch in <10 minutes total

---

### Day 2 Checklist: Binary Search Trees
- [ ] Understand BST invariant (left < root < right)
- [ ] Implement search (compare and recurse)
- [ ] Implement insert (find position, create node, maintain parent links)
- [ ] Implement delete (case 1: leaf, case 2: one child, case 3: two children with inorder successor)
- [ ] Implement validation (min/max range checking)
- [ ] Trace through insertion creating unbalanced tree
- [ ] Understand inorder traversal produces sorted sequence
- [ ] Solve #700 (Search in BST)

**Exit Criteria:** Can implement all BST operations from scratch in <15 minutes total

---

### Day 3 Checklist: Balanced BSTs (Conceptual)
- [ ] Understand why balance matters (height bound)
- [ ] Study AVL balance factor and 4 rotation cases (LL, LR, RR, RL)
- [ ] Understand Red-Black 5 coloring rules
- [ ] Understand black-height invariant (RB)
- [ ] Compare AVL vs RB (perfect balance vs simpler rebalancing)
- [ ] Research Java TreeMap, C++ std::map implementations
- [ ] Read about Linux kernel completely fair scheduler (RB trees)

**Exit Criteria:** Can explain why balance matters and how two approaches differ

---

### Day 4 Checklist: Tree Patterns
- [ ] Study path sum pattern (DFS + backtracking with removal)
- [ ] Study tree diameter pattern (DP: max path = left height + right height + 1)
- [ ] Study LCA pattern (where do paths split?)
- [ ] Study serialization pattern (preorder + nulls encoding)
- [ ] Solve #543 (Diameter)
- [ ] Solve #112 (Path Sum)
- [ ] Solve #236 (LCA)
- [ ] Solve #297 (Serialize/Deserialize)

**Exit Criteria:** Can recognize each pattern by problem statement and implement without reference

---

### Day 5 Checklist: Augmented BSTs (Optional)
- [ ] Understand augmentation principle (metadata at each node)
- [ ] Study order-statistics (kth smallest in O(log n))
- [ ] Study rank queries (how many ≤ x)
- [ ] Study range count ([L, R] in O(log n))
- [ ] Research database range indexes
- [ ] (Optional) Implement augmented BST with subtree size

**Exit Criteria:** Can explain augmentation and see application in databases/systems

---

## 🚀 INTERVIEW PREPARATION

### Must-Know Implementations
1. **Preorder Traversal (Iterative)** - Shows stack understanding
2. **Inorder Traversal (Iterative)** - Core for BST and "kth smallest" patterns
3. **BST Insert** - Fundamental operation
4. **BST Delete** - All 3 cases, especially two children (inorder successor)
5. **Serialize/Deserialize** - Common interview question

### Must-Recognize Patterns
- **"Find path where sum = X"** → DFS + backtracking
- **"Longest path in tree"** → Tree diameter (DP)
- **"Lowest common ancestor"** → LCA pattern
- **"Encode tree"** → Serialization with nulls
- **"Kth smallest in BST"** → Inorder traversal (counter) or augmented BST

### Must-Answer Questions
- "Why use tree instead of array?" → Ordered dynamic set, range queries, O(log n) ops
- "What happens if input is sorted?" → Degenerate BST becomes O(n) linked list
- "Why use Red-Black over AVL?" → Fewer rotations, better constants, used in production
- "How to serialize tree uniquely?" → Preorder + nulls (or inorder + structure info)

### Time Targets
- **Stage 1 problem:** < 3 minutes to code
- **Stage 2 problem:** < 8 minutes to code
- **Stage 3 problem:** < 15 minutes to code
- **Explanation:** < 30 seconds mental model + approach

---

## 📚 EXTERNAL RESOURCES

### Books
- "Introduction to Algorithms" (CLRS) - Chapters 12-14 (BST, RB Trees)
- "Algorithms" by Sedgewick & Wayne - Part 3 (BST, Balanced Trees)
- "The Algorithm Design Manual" - Trees section

### Online
- **Visualizations:** VisuAlgo.net (tree visualizations, rotations)
- **Competitive Programming:** Codeforces tutorials on tree traversals
- **Practice:** LeetCode tree tag (problems #94-297, organized by difficulty)

### Videos
- MIT 6.006 Lecture 8-10 (Trees, BST, Balance)
- Abdul Bari - Tree traversals and BST operations (YouTube)

---

## 🎯 SUCCESS CRITERIA FOR WEEK 7

### By End of Week, You Can:

✅ **Traversals:**
- Implement all 4 traversals (preorder, inorder, postorder, level-order)
- Explain when to use each (structure copy, sorted sequence, post-processing, BFS)
- Handle recursive vs iterative trade-off

✅ **BST Operations:**
- Search in O(log n) average, O(n) worst
- Insert maintaining invariant
- Delete all 3 cases correctly
- Validate BST property with min/max ranges
- Recognize degenerate case and impact

✅ **Patterns:**
- Path sum (DFS + backtracking)
- Tree diameter (DP)
- LCA (recursive decomposition)
- Serialization (unique encoding with nulls)

✅ **Balanced Trees (Conceptual):**
- Explain why balance matters
- Compare AVL vs Red-Black trade-offs
- Recognize in production systems

✅ **Interview Ready:**
- Solve Stage 1-2 problems in <10 minutes
- Explain approach using mental models
- Write production-grade code with edge cases
- Discuss trade-offs and alternatives

---

## 🔗 CONNECTION TO ADJACENT WEEKS

### Week 6 → Week 7
- **Hash patterns** (Week 6) meet **ordered retrieval** (Week 7)
- Hash gives O(1) lookup but no order; BST gives O(log n) with order

### Week 7 → Week 8
- **Tree foundations** (Week 7) → **Graph algorithms** (Week 8)
- Traversals (DFS, BFS) remain core; now applied to general graphs

### Week 7 → Week 10+
- **Tree patterns** enable **dynamic programming on trees** (Week 10)
- **Balanced BSTs** enable **segment trees, Fenwick trees** (Week 11+)

---

**Week 7 is foundational. Master it thoroughly. Trees unlock graph algorithms and advanced DP patterns.**

**Status:** ✅ READY FOR DEPLOYMENT
