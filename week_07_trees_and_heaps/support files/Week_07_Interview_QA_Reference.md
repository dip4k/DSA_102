# 🎙️ Week_07_Interview_QA_Reference.md

**Version:** v1.0 (Comprehensive Question Bank)  
**Purpose:** Interview preparation with 50+ questions, follow-ups, and strategic answers  
**Audience:** Interview prep, mock interview practice  
**Status:** ✅ PRODUCTION-READY

---

## 📋 HOW TO USE THIS REFERENCE

### Interview Prep Strategy

**Self-Assessment Phase (Days 1-3):**
- Read through questions without looking at hints
- Identify which you can answer immediately vs. which require thinking
- Mark difficult questions for revisited practice

**Active Practice Phase (Days 4-5):**
- Set a timer (10-15 min per question)
- Answer out loud (simulating real interview)
- Explain your reasoning before coding
- Address follow-ups without hesitation

**Mock Interview Phase (After Week 7):**
- Have a friend ask 2-3 random questions from each section
- Record yourself answering
- Review for clarity and depth

---

## 🌲 SECTION 1: TREE ANATOMY & TRAVERSALS (Day 1 Focus)

### 1.1 Foundational Concepts

**Q1:** What is the difference between height and depth in a tree?
- **Expected Elements:** Definition of each, example tree with labeled heights/depths
- **Interview Depth:** Explain on a 5-node tree; connect to complexity analysis
- **Follow-up:** For a node at depth d with height h, what can you say about the tree structure?

**Q2:** Define a complete binary tree. Why is this definition useful?
- **Expected Elements:** All levels filled except possibly last (left-justified); array representation benefits
- **Interview Depth:** Explain why complete trees enable O(1) child lookup via indices
- **Follow-up:** How would you check if a tree is complete? What's the time complexity?

**Q3:** Explain the difference between a full binary tree and a complete binary tree.
- **Expected Elements:** Full = every node has 0 or 2 children; Complete = all levels filled left-justified
- **Interview Depth:** Provide examples of trees that are one but not the other
- **Follow-up:** Prove that a full binary tree with n nodes has (n+1)/2 leaves

**Q4:** What is a balanced binary tree? Give three different definitions.
- **Expected Elements:** AVL (balance factor ≤1), Red-Black (path property), general (height ≈ log n)
- **Interview Depth:** Explain why each definition matters in different contexts
- **Follow-up:** For each definition, give an example of a balanced tree that wouldn't satisfy the others

**Q5:** When would you use a tree data structure instead of an array or linked list?
- **Expected Elements:** Hierarchical data, efficient search (log n), ordered operations
- **Interview Depth:** Discuss trade-offs explicitly; mention use cases (filesystems, DOM, databases)
- **Follow-up:** Design a data structure for storing user comments (replies to replies). How would you represent it?

---

### 1.2 Traversal Orders

**Q6:** Implement preorder traversal recursively. Then implement it iteratively.
- **Expected Elements:** Both implementations; explanation of why order differs
- **Interview Depth:** Write both with comments; discuss space complexity (O(h) vs explicit stack)
- **Follow-up:** Why do we push right child before left child in the iterative version?

**Q7:** Explain inorder traversal. Why does it produce sorted output for BSTs?
- **Expected Elements:** Left → Node → Right; proof that left < node < right applies recursively
- **Interview Depth:** Implement iteratively; draw a trace table on a 5-node BST
- **Follow-up:** How would you find the predecessor of a given node using inorder?

**Q8:** When would you use postorder traversal? Give three specific examples.
- **Expected Elements:** Freeing trees (children first); calculating tree sizes; prefix-to-postfix
- **Interview Depth:** Code a tree-deletion function that uses postorder logic
- **Follow-up:** Can you implement postorder with a single stack? (Yes, with two-stack trick or marker approach)

**Q9:** Implement level-order traversal (BFS). Why must you capture queue size before processing a level?
- **Expected Elements:** Queue-based approach; explanation of why direct queue.Count fails
- **Interview Depth:** Trace through a tree showing the subtle bug if you don't capture size
- **Follow-up:** How would you modify level-order to group results by level? (Already done in your implementation)

**Q10:** Compare all four traversals: time, space, and order of processing.
- **Expected Elements:** All O(n) time; space O(h) to O(n) depending on tree shape and traversal; order diagrams
- **Interview Depth:** Create a single tree and show all four orderings; discuss practical implications
- **Follow-up:** Which traversal would you choose for each use case? (Serialize, delete, sort, print by level)

**Q11:** What is Morris traversal? Why is it useful?
- **Expected Elements:** O(1) space inorder traversal using tree threading; no extra stack
- **Interview Depth:** Conceptual understanding (not code); when you'd recommend it
- **Follow-up:** For what types of problems is Morris most valuable? (Streaming data, memory-constrained devices)

---

### 1.3 Recursion Depth & Edge Cases

**Q12:** What happens if you recursively traverse a very deep tree (height = 100,000)? How would you fix it?
- **Expected Elements:** Stack overflow risk; iterative solution with explicit stack
- **Interview Depth:** Discuss call stack limits on typical systems (10k to 100k depth)
- **Follow-up:** How would you test your code to ensure it handles deep trees correctly?

**Q13:** What if a tree node is null? How do you handle it in traversals?
- **Expected Elements:** Base case check at start of recursive function
- **Interview Depth:** Show the impact of forgetting this (infinite recursion or crash)
- **Follow-up:** What if the entire tree is null? Should your function return a valid result? (Yes, empty list)

**Q14:** Design a function that counts nodes at depth d. How would you implement it?
- **Expected Elements:** Recursive: recurse with depth+1; Iterative: level-order with depth tracking
- **Interview Depth:** Implement both; compare complexity and clarity
- **Follow-up:** What if d is negative or larger than tree height? Handle edge cases

---

## 🌳 SECTION 2: BINARY SEARCH TREES (Day 2 Focus)

### 2.1 BST Fundamentals

**Q15:** Explain the BST invariant and why it matters.
- **Expected Elements:** Left < root < right; enables binary search; enables O(log n) operations
- **Interview Depth:** Draw a valid and invalid BST; prove inorder is sorted
- **Follow-up:** Is a BST with all equal values valid? (Depends on problem spec; usually no duplicates)

**Q16:** Implement BST search. What's the time complexity?
- **Expected Elements:** Recursive comparison; O(log n) average, O(n) worst (degenerate)
- **Interview Depth:** Code implementation; analyze best/average/worst cases
- **Follow-up:** How would you search for a range [L, R] in a BST efficiently?

**Q17:** Implement BST insert while maintaining the invariant.
- **Expected Elements:** Find correct leaf position; create new node; return root to maintain parent links
- **Interview Depth:** Handle duplicates (specify decision); discuss balancing implications
- **Follow-up:** What if you insert many duplicate values? How would you modify to allow multiset?

**Q18:** Implement BST delete. Discuss the three cases.
- **Expected Elements:** Leaf (remove), one child (replace), two children (inorder successor)
- **Interview Depth:** Code all three cases; trace through examples
- **Follow-up:** Why use inorder successor instead of inorder predecessor? (Either works; be consistent)

**Q19:** How do you validate a binary tree is a valid BST?
- **Expected Elements:** Recursive range checking: node must be in (min, max)
- **Interview Depth:** Implement; show a common mistake (only checking left < node < right)
- **Follow-up:** What if values can be Integer.MIN_VALUE or Integer.MAX_VALUE? Use long bounds

**Q20:** What does "degenerate BST" mean? How does it affect performance?
- **Expected Elements:** Chain-like structure from sorted insertions; O(n) operations instead of O(log n)
- **Interview Depth:** Show an example (inserting 1,2,3,4,5 creates a chain)
- **Follow-up:** How would you detect if a BST is degenerate? Check if height ≈ n

---

### 2.2 BST Operations & Efficiency

**Q21:** Find the inorder successor of a given node in a BST.
- **Expected Elements:** If right child exists, go right once then left until null; otherwise go up until finding parent
- **Interview Depth:** Implement both cases; discuss space complexity
- **Follow-up:** Find the kth smallest element using successor logic

**Q22:** Find the lowest common ancestor of two nodes in a BST.
- **Expected Elements:** Compare both targets with current node; recurse left/right/both based on comparison
- **Interview Depth:** Explain why BST property makes this simpler than general trees
- **Follow-up:** What if one or both nodes don't exist in the tree?

**Q23:** Convert a sorted array to a balanced BST.
- **Expected Elements:** Recursively use middle element as root; left/right halves as subtrees
- **Interview Depth:** Code implementation; verify result is balanced
- **Follow-up:** Why is using the middle element important? (Ensures balanced height)

**Q24:** Flatten a BST into a sorted linked list (in-place if possible).
- **Expected Elements:** Inorder traversal approach; or reverse inorder for right-to-left linking
- **Interview Depth:** Implement using prev node tracking; explain space trade-off
- **Follow-up:** How would you do this with O(1) space without modifying the original tree?

**Q25:** Count pairs in a BST that sum to a target value.
- **Expected Elements:** Inorder (sorted) + two-pointer approach; or hash set with DFS
- **Interview Depth:** Both implementations; time/space trade-offs
- **Follow-up:** Extend to k-sum problem; use combinations/subset approach

---

### 2.3 BST Construction & Analysis

**Q26:** Reconstruct a BST from preorder traversal.
- **Expected Elements:** Use range constraints; or use boundaries (min/max valid values)
- **Interview Depth:** Code with both approaches; verify correctness
- **Follow-up:** What if the preorder is not a valid BST? Detect and return error

**Q27:** How many structurally different BSTs can be formed with n distinct values?
- **Expected Elements:** This is the Catalan number! C(n) = (2n)! / ((n+1)! * n!)
- **Interview Depth:** Explain the DP recurrence; compute for small n (1,2,3,4)
- **Follow-up:** Generate all different structures (LeetCode #95)

---

## ⚖️ SECTION 3: BALANCED TREES (Day 3 Focus)

### 3.1 AVL Trees

**Q28:** What is a balance factor? When must it be corrected?
- **Expected Elements:** BF = height(left) - height(right); must stay in [-1, 0, 1]
- **Interview Depth:** Calculate BF for unbalanced tree; identify which node violates
- **Follow-up:** How often do imbalances occur with random insertions?

**Q29:** Explain the four rotation cases in AVL trees.
- **Expected Elements:** LL (left-left), LR (left-right), RR (right-right), RL (right-left)
- **Interview Depth:** Draw each case; explain why each requires specific rotation(s)
- **Follow-up:** Implement at least one rotation with pointer updates

**Q30:** Why does AVL require rebalancing after insertion/deletion?
- **Expected Elements:** Insertion can create imbalance at ancestors; rotations fix it locally while preserving BST property
- **Interview Depth:** Trace insertion that requires multiple rotations
- **Follow-up:** Is a single rotation always sufficient? (Sometimes yes, sometimes requires 2 rotations)

**Q31:** Compare AVL vs BST: when would you use each?
- **Expected Elements:** BST: flexible, simpler; AVL: guaranteed O(log n) at cost of rebalancing
- **Interview Depth:** Discuss when degenerate case is problematic (e.g., adversarial insertions)
- **Follow-up:** In practice, random data rarely creates degenerate BSTs. Does this matter?

---

### 3.2 Red-Black Trees

**Q32:** State the five Red-Black tree properties.
- **Expected Elements:** Color constraint, red-red property, black-height property, root/NIL colors
- **Interview Depth:** Verify a tree satisfies all five; identify violation
- **Follow-up:** Which property is the most "important" for enforcing balance?

**Q33:** Why are Red-Black trees preferred over AVL in production systems?
- **Expected Elements:** Fewer rotations per operation; simpler constant factors
- **Interview Depth:** Discuss trade-off: AVL more balanced (better search) vs RB fewer updates (better insertion/deletion)
- **Follow-up:** What applications benefit most from RB over AVL?

**Q34:** How do Red-Black rotations differ from AVL rotations?
- **Expected Elements:** Same pointer manipulations; RB also requires color repainting
- **Interview Depth:** Show that color changes affect rebalancing logic
- **Follow-up:** Implement a rotation with color updates

**Q35:** What guarantees do Red-Black trees provide?
- **Expected Elements:** Height ≤ 2 * log(n+1); O(log n) for search/insert/delete
- **Interview Depth:** Sketch proof that black-height property implies logarithmic height
- **Follow-up:** Can RB be perfectly balanced? (No; more flexible than AVL)

---

### 3.3 Real-World Balanced Trees

**Q36:** Describe how TreeMap in Java uses Red-Black trees.
- **Expected Elements:** Internal structure; sorting guarantees; O(log n) operations
- **Interview Depth:** Discuss implications: ordered iteration, range queries
- **Follow-up:** What about TreeSet? (Same underlying structure, just keys)

**Q37:** How does the Linux kernel use Red-Black trees?
- **Expected Elements:** Process scheduling (task struct); virtual memory (vm_area_struct)
- **Interview Depth:** Why RB and not AVL for kernel? (Fewer rotations under frequent updates)
- **Follow-up:** What other systems use RB? (C++ std::map, Java, databases)

**Q38:** Design a database index using balanced BSTs. What queries would be efficient?
- **Expected Elements:** Point lookup, range queries, sorted iteration
- **Interview Depth:** Discuss augmentations (subtree size for rank queries)
- **Follow-up:** How would you handle insertions/deletions at scale with rebalancing costs?

---

## 🧩 SECTION 4: TREE PATTERNS (Day 4 Focus)

### 4.1 Path Sum Problems

**Q39:** Find all root-to-leaf paths that sum to a target value.
- **Expected Elements:** DFS + backtracking; critical to RemoveAt after recursion
- **Interview Depth:** Code with detailed explanation of backtracking; trace through example
- **Follow-up:** Extend to *any* path (not just root-to-leaf). How does this change complexity?

**Q40:** Find the maximum path sum in a tree (any two nodes, not just root-to-leaf).
- **Expected Elements:** Postorder DP; at each node, return max path starting at that node
- **Interview Depth:** Carefully explain the return value vs. the answer we track
- **Follow-up:** What if the path must go through a specific node? (Different approach)

**Q41:** Check if there's a path in the tree equal to a given sum.
- **Expected Elements:** Simple DFS with running sum
- **Interview Depth:** Code recursive solution; discuss base case (leaf vs null)
- **Follow-up:** Find *all* paths summing to target (similar to Q39 but any path)

**Q42:** Find the minimum depth of a tree (shortest path to a leaf).
- **Expected Elements:** BFS (early termination when leaf found); or DFS with tracking
- **Interview Depth:** Explain why BFS might be better (early termination)
- **Follow-up:** What if tree is very unbalanced? How does algorithm perform?

---

### 4.2 Tree Diameter & Distances

**Q43:** Find the diameter of a binary tree (longest path between any two nodes).
- **Expected Elements:** Postorder DP; return height to parent, track max diameter
- **Interview Depth:** Carefully distinguish between "diameter through this node" vs "maximum diameter in subtree"
- **Follow-up:** What if you need to return the actual path (not just length)?

**Q44:** Find the distance between two nodes in a tree.
- **Expected Elements:** Find LCA first; then distance = dist(LCA, node1) + dist(LCA, node2)
- **Interview Depth:** Implement both finding LCA and calculating distance
- **Follow-up:** For a given node, find all nodes at distance d

**Q45:** Check if a tree is balanced (for AVL definition).
- **Expected Elements:** Postorder DFS; return height; if any subtree unbalanced, return -1
- **Interview Depth:** Optimize to avoid recalculating heights (single pass)
- **Follow-up:** What if tree is very deep? Can you do this iteratively?

---

### 4.3 Lowest Common Ancestor (LCA)

**Q46:** Find the LCA of two nodes in a BST.
- **Expected Elements:** Compare both values with current; if split, current is LCA; otherwise recurse
- **Interview Depth:** Code for BST (simpler) and explain why binary search helps
- **Follow-up:** Extend to general binary tree (three cases: left, right, both)

**Q47:** Find LCA in a general binary tree (not necessarily BST).
- **Expected Elements:** Recursive: left and right subtree results; if both found, node is LCA
- **Interview Depth:** Handle edge cases (one or both nodes not in tree)
- **Follow-up:** If one node is guaranteed to be ancestor of other, simplify

**Q48:** Find LCA with a parent pointer in each node.
- **Expected Elements:** Hash set to mark path from one node to root; check where other node's path intersects
- **Interview Depth:** Compare to LCA without parent pointers; discuss space/time trade-offs
- **Follow-up:** Can you do this with O(1) space? (Only if trees are height-balanced, compute depths)

---

### 4.4 Serialization & Reconstruction

**Q49:** Serialize and deserialize a binary tree.
- **Expected Elements:** Preorder + null markers; deserialize using queue of tokens
- **Interview Depth:** Full code for both; verify roundtrip correctness
- **Follow-up:** How would you handle very large trees? (Streaming, chunking)

**Q50:** Serialize a BST to string and reconstruct.
- **Expected Elements:** Can use fewer markers (BST property); or preorder with range validation
- **Interview Depth:** Compare complexity with general tree serialization
- **Follow-up:** Design a compact serialization for millions of nodes

**Q51:** Encode and decode a tree from level-order traversal.
- **Expected Elements:** Precompute structure info or use null markers between levels
- **Interview Depth:** Discuss when level-order is preferable to preorder
- **Follow-up:** What metadata must you preserve to uniquely encode structure?

---

## 🔢 SECTION 5: AUGMENTED BSTs & ADVANCED (Day 5 Optional)

### 5.1 Order-Statistics & Range Queries

**Q52:** Find the kth smallest element in a BST.
- **Expected Elements:** Inorder traversal with counter; or augmented BST with subtree sizes
- **Interview Depth:** Both approaches; augmented approach is O(log n) with preprocessing
- **Follow-up:** If tree is modified frequently, how do you maintain subtree sizes efficiently?

**Q53:** Count how many elements in a BST fall in range [L, R].
- **Expected Elements:** DFS with range filtering; or with subtree size augmentation
- **Interview Depth:** Implement basic version; discuss optimization with augmentation
- **Follow-up:** If you have many range queries, would augmentation be worth the overhead?

**Q54:** Implement an order-statistics tree supporting rank and select queries.
- **Expected Elements:** BST augmented with subtree sizes; rank(x) returns how many ≤ x
- **Interview Depth:** Full implementation with insert/delete maintaining sizes
- **Follow-up:** Real systems like C++ Pbds; discuss when this is worth the complexity

---

### 5.2 Advanced Patterns

**Q55:** Design a data structure for autocomplete using a tree.
- **Expected Elements:** Trie (prefix tree) or BST with prefix-based queries
- **Interview Depth:** Discuss trade-offs; trie is more standard for autocomplete
- **Follow-up:** How would you rank suggestions by frequency? (Augment with counts)

**Q56:** Check if a tree is a subtree of another tree.
- **Expected Elements:** Preorder serialization comparison; or recursive structure matching
- **Interview Depth:** Code both; discuss complexity implications
- **Follow-up:** How would you handle duplicate values or very large trees?

**Q57:** Convert a sorted linked list to a balanced BST.
- **Expected Elements:** Recursively use middle element as root; left/right sublists as subtrees
- **Interview Depth:** Careful handling of pointers in linked list; verify balance
- **Follow-up:** Is it possible to do this in-place with only O(log n) extra space?

---

## 🧠 SECTION 6: META-INTERVIEW SKILLS (Cross-Week)

### Depth vs Breadth

**Q58:** You're given 15 minutes. How would you approach a tree problem you've never seen?
- **Expected Elements:** Read problem carefully; identify pattern; start with brute force; optimize
- **Interview Depth:** Discuss communication with interviewer (ask clarifying questions)
- **Follow-up:** What tree patterns should you immediately recognize?

**Q59:** When would you use DFS vs BFS on a tree?
- **Expected Elements:** DFS for paths/existence; BFS for shortest distance or layer processing
- **Interview Depth:** Give specific examples; discuss memory usage (DFS = O(h), BFS = O(w))
- **Follow-up:** Can some problems be solved more naturally with one vs the other?

---

### Edge Cases & Robustness

**Q60:** Common edge cases in tree problems. How do you handle each?
- **Expected Elements:** Null tree, single node, skewed trees, deep trees, all duplicates
- **Interview Depth:** For your implementations, which edge cases are tricky?
- **Follow-up:** How would you test your code to ensure it handles edge cases?

**Q61:** Design a tree problem and solve it under interview conditions.
- **Expected Elements:** Problem statement, example, solution with explanation
- **Interview Depth:** Self-created problem should be non-trivial but solvable in 20 min
- **Follow-up:** Would you ask clarifying questions before starting?

---

## ✅ QUICK SELF-ASSESSMENT

### Can You Answer Immediately (< 3 min):

- [ ] Q1-Q5: Tree anatomy concepts
- [ ] Q6-Q11: All traversal algorithms
- [ ] Q15-Q20: BST fundamentals
- [ ] Q28-Q35: Balanced tree concepts
- [ ] Q39, Q43, Q46, Q49: Core patterns

### Can You Code in 5-10 Minutes:

- [ ] Q6, Q7, Q9: Traversals
- [ ] Q16, Q17, Q18: BST operations
- [ ] Q39, Q43: Path sum and diameter
- [ ] Q46, Q49: LCA and serialization

### Harder (10-20 Minutes):

- [ ] Q24, Q27: Advanced BST operations
- [ ] Q40, Q45: Tricky DP patterns
- [ ] Q52, Q53: Augmented trees
- [ ] Q56, Q57: Difficult variations

---

## 📊 INTERVIEW QUESTION DISTRIBUTION

| Category | # Questions | Frequency in Interviews |
|---|---|---|
| Traversals | 6 | ⭐⭐⭐⭐⭐ (Very Common) |
| BST Operations | 12 | ⭐⭐⭐⭐⭐ (Very Common) |
| Balanced Trees | 8 | ⭐⭐⭐ (Common in System Design) |
| Patterns (Paths, Diameter, LCA) | 15 | ⭐⭐⭐⭐ (Common) |
| Serialization | 4 | ⭐⭐⭐⭐ (Common) |
| Augmented/Advanced | 10 | ⭐⭐ (Advanced Rounds) |
| Meta Skills | 4 | ⭐⭐⭐⭐⭐ (Always Evaluated) |

---

## 🎯 PREPARATION TIMELINE

**Day 1:** Sections 1.1-1.3 (Fundamentals)  
**Day 2:** Sections 2.1-2.3 (BST)  
**Day 3:** Sections 3.1-3.3 (Balanced)  
**Day 4:** Sections 4.1-4.4 (Patterns)  
**Day 5:** Sections 5.1-5.2 (Advanced)  
**Finalization:** Section 6 (Meta Skills) + Mock Interviews  

---

**Status:** ✅ Interview QA Reference Complete

