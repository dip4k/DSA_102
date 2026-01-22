# 📅 Week_07_Daily_Progress_Checklist.md

**Version:** v1.0 (Day-by-Day Action Plan)  
**Purpose:** Concrete daily actions and reflection prompts  
**Audience:** Active learners with structured daily schedule  
**Status:** ✅ PRODUCTION-READY

---

## 🎯 WEEK 7 OVERVIEW

**Primary Goal:** Master tree structures, traversals, BST operations, and foundational tree patterns.

**Weekly Time Allocation:**
- **Reading/Understanding:** 60-90 min/day (instructional file + examples)
- **Tracing/Drawing:** 30-45 min/day (hand-trace examples, draw diagrams)
- **Problem Solving:** 60-90 min/day (code 3-5 problems per day)
- **Review/Reflection:** 15-30 min/day (check understanding, identify gaps)
- **Total:** 4-5 hours per day, ~25-30 hours for the week

**Success Criteria:** By Friday end, you should be able to:
- [ ] Implement all four traversals (recursive AND iterative) from memory in < 5 min
- [ ] Solve BST search/insert/delete problems without hints in < 10 min
- [ ] Identify tree pattern type in new problem and sketch approach in < 3 min
- [ ] Explain trade-offs (recursion vs iteration, AVL vs Red-Black, etc.) fluently

---

## 📅 MONDAY: TREE ANATOMY & TRAVERSALS (Day 1)

### Morning Session (90 min): Concept Mastery

**Read & Understand (45 min):**
- [ ] Read Week 07 Day 1 Instructional file: Chapters 1-2 (Context & Mental Model)
- [ ] Focus on the core analogy section; understand why each traversal is natural
- [ ] Study the visualization section; draw a tree and label height/depth for 3 random nodes
- [ ] Read tree anatomy definitions; write definitions in your own words

**Conceptual Exercises (45 min):**
- [ ] Draw a 5-node tree; manually execute preorder, inorder, postorder, level-order
- [ ] For each, verify the output matches expected order
- [ ] Pick one traversal and explain WHY that order makes sense for a use case (e.g., preorder for tree copy)

**Key Concepts to Internalize:**
- [ ] Height vs Depth: Clear definitions with examples
- [ ] Complete vs Full vs Balanced: Definitions and why each matters
- [ ] Why height matters: Recursion depth limits, complexity analysis

### Midday Session (90 min): Implementation & Tracing

**Code Implementation (60 min):**
- [ ] Implement preorder traversal (recursive) - should take 2-3 min
- [ ] Test on a 5-node tree; trace execution mentally
- [ ] Implement preorder traversal (iterative) - should take 3-4 min
- [ ] Trace both side-by-side; verify identical output
- [ ] **Reflection:** Why does iterative push right *before* left?

**Trace Exercises (30 min):**
- [ ] Trace inorder traversal by hand on paper (given tree)
- [ ] Trace level-order BFS on same tree
- [ ] Mark each node with its visit order; verify against expected sequence
- [ ] Identify the "control structure" (stack, queue) for each

**Quality Check:**
- [ ] Both preorder implementations produce identical output
- [ ] You can explain (out loud) why each step is necessary
- [ ] Traces match expected outputs exactly

### Afternoon Session (90 min): Deep Dives & Edge Cases

**Chapter 3 Study (45 min):**
- [ ] Read Instructional file Chapter 3: Mechanics & Implementation
- [ ] Study all four traversal implementations with inline traces
- [ ] Pay attention to the state machine diagrams and trace tables

**Implement Remaining Traversals (45 min):**
- [ ] Implement inorder traversal (iterative) - most complex
- [ ] Implement level-order traversal (BFS)
- [ ] **Critical:** Capture queue size before loop; test this
- [ ] Create a helper function to print traversal results nicely

**Edge Cases to Test:**
- [ ] Null tree → should return empty list
- [ ] Single node → all traversals return [value]
- [ ] Chain-like tree (all left children) → verify deep recursion works
- [ ] Wide tree (many children at one level) → level-order should group correctly

### Evening Session (60 min): Reflection & Problem Solving

**Read Chapter 4 Summary (20 min):**
- [ ] Skim Performance & Real Systems section
- [ ] Understand the memory reality (stack vs heap, recursion limits)
- [ ] Note one real-world system using tree traversals (DOM, filesystem, etc.)

**Solve First 3 Problems (40 min):**
- [ ] LeetCode #144: Preorder Traversal → should be < 2 min
- [ ] LeetCode #94: Inorder Traversal → should be < 2 min
- [ ] LeetCode #102: Level-Order Traversal → should be < 3 min
- [ ] If you get stuck, re-read implementation details, don't peek at solutions

### End-of-Day Reflection (15 min):

**Concept Check (answer without notes):**
- [ ] What is the difference between height and depth?
- [ ] Why does level-order require capturing queue size before the loop?
- [ ] Why do we push right before left in iterative preorder?

**Emotional Check:**
- [ ] How confident do you feel about traversals? (1-10)
- [ ] Which traversal feels most intuitive? Which is hardest?
- [ ] What's one thing you'll practice again tomorrow?

**Tomorrow's Focus:** "Tomorrow I need to focus on ________"

---

## 📅 TUESDAY: BINARY SEARCH TREES (Day 2)

### Morning Session (90 min): BST Fundamentals

**Read & Understand (45 min):**
- [ ] Read Day 1 instructional (if not yet: Chapters 1-2 for context)
- [ ] **NEW:** Read Day 2 Instructional file: Chapters 1-2
- [ ] Focus on BST invariant: "Left < Root < Right" — explain it 5 times aloud
- [ ] Understand why this invariant enables binary search

**Conceptual Exercises (45 min):**
- [ ] Draw 3 valid BSTs with 5 nodes each
- [ ] Draw an invalid BST; identify which invariant is violated
- [ ] For one BST, perform inorder traversal and verify output is sorted
- [ ] Answer: "Why is inorder traversal on BST always sorted?"

**Key Concepts:**
- [ ] BST Invariant and why it matters (enables O(log n) operations)
- [ ] Balanced vs Degenerate: Show examples of both with same values
- [ ] Inorder traversal produces sorted output (proof via recursion)

### Midday Session (90 min): BST Operations

**Study Chapter 3 (30 min):**
- [ ] Read Day 2 Instructional Chapter 3: Mechanics & Implementation
- [ ] Study search, insert, delete operations
- [ ] Pay special attention to delete (three cases)

**Implement BST Operations (60 min):**
- [ ] **Search:** Implement in 1-2 min; test with values in/not in tree
- [ ] **Insert:** Implement in 2-3 min; build a tree from [5, 3, 7, 1, 4, 6, 8]
- [ ] **Delete (Leaf):** Implement and test with leaf node
- [ ] **Delete (One Child):** Implement and test with single-child node
- [ ] **Delete (Two Children):** Implement full version; test all cases
- [ ] **Helper:** Find inorder successor (min in right subtree)

**Trace Exercises:**
- [ ] Trace deletion of node with two children on your test tree
- [ ] Verify tree remains valid BST after deletion
- [ ] Trace search for existing and non-existing values

### Afternoon Session (90 min): Validation & Edge Cases

**BST Validation (30 min):**
- [ ] Implement tree validation using range checking (NOT just left < node < right)
- [ ] Test on valid BSTs → returns true
- [ ] Test on invalid BSTs → returns false
- [ ] Test edge case: tree with Integer.MinValue or MaxValue nodes

**Advanced Operations (30 min):**
- [ ] Implement: Find inorder successor of a given node
- [ ] Implement: Find lowest common ancestor (LCA) in BST
- [ ] Implement: Find kth smallest element (using inorder with counter)

**Edge Cases (30 min):**
- [ ] Empty tree
- [ ] Single node (both search and operations)
- [ ] Degenerate tree (linked list shape from sorted insert)
- [ ] Tree with duplicate insertions (how do you handle?)

### Evening Session (60 min): Problem Solving

**Solve BST Problems (50 min):**
- [ ] LeetCode #700: Search in BST → 1 min (trivial)
- [ ] LeetCode #701: Insert into BST → 2-3 min
- [ ] LeetCode #450: Delete from BST → 5-7 min (most complex)
- [ ] LeetCode #98: Validate BST → 4-5 min (watch range checking)
- [ ] LeetCode #285: Inorder Successor → 3-4 min (two-case approach)

**If time permits:**
- [ ] LeetCode #235: LCA of BST → 2 min (uses invariant)

### End-of-Day Reflection (15 min):

**Concept Check:**
- [ ] Explain the BST invariant in one sentence
- [ ] Draw a degenerate BST (chain) with 5 nodes; explain why operations are O(n)
- [ ] What are the three cases for delete? Explain why two children case is hardest

**Emotional Check:**
- [ ] Confidence on BST operations? (1-10)
- [ ] Which operation (search/insert/delete) is clearest?
- [ ] What will you drill again tomorrow?

**Tomorrow's Focus:** "BST deletion is/isn't clear to me because ________"

---

## 📅 WEDNESDAY: BALANCED TREES (Day 3)

### Morning Session (90 min): Why Balance Matters

**Read & Understand (45 min):**
- [ ] Read Day 3 Instructional file: Chapters 1-2
- [ ] Understand the problem: Degenerate BST → O(n) operations
- [ ] Understand the solution: Balance constraints → O(log n) guaranteed
- [ ] Review Chapter 4: Real Systems (AVL/Red-Black in practice)

**Conceptual Exercises (45 min):**
- [ ] Compare balanced vs degenerate trees:
  - Draw same 5 values in balanced tree
  - Draw same 5 values in degenerate tree
  - Count path length for a search operation in each
  - Show the performance difference visually
- [ ] Answer: "When would you encounter a degenerate BST in practice?"

**Key Concepts:**
- [ ] AVL Balance Factor definition and invariant (|BF| ≤ 1)
- [ ] Red-Black coloring rules (state all five)
- [ ] Why each balance constraint ensures height ≈ log(n)

### Midday Session (90 min): Tree Rotations & Concepts

**Study Rotations (30 min):**
- [ ] Read Chapter 3 on rotations (if included in instructional)
- [ ] Understand the four rotation cases: LL, RR, LR, RL
- [ ] For each case, study WHY that specific rotation(s) fixes the imbalance
- [ ] Draw before/after diagrams for each case

**Trace Rotations (30 min):**
- [ ] Practice single right rotation on an imbalanced tree (LL case)
- [ ] Practice single left rotation (RR case)
- [ ] Practice double rotations (LR: left then right)
- [ ] Verify BST property is maintained after each rotation

**AVL vs Red-Black Comparison (30 min):**
- [ ] Create comparison chart:
  - | Property | AVL | Red-Black |
  - | Strictness | Strict balance | Flexible |
  - | Rotations per insert | Can be multiple | At most 3 |
  - | Height bound | ≤ 1.44 * log(n) | ≤ 2 * log(n) |
  - | Best use | Frequent searches | Frequent updates |
- [ ] Answer: "Why would PostgreSQL use Red-Black instead of AVL?"

### Afternoon Session (90 min): Validation & Real Systems

**Balance Validation (30 min):**
- [ ] Implement: Check if tree is balanced (AVL definition)
- [ ] Test on balanced trees → true
- [ ] Test on unbalanced trees → false
- [ ] Optimize: Single pass vs recalculating heights

**Real Systems Research (30 min):**
- [ ] Research: How Java's TreeMap uses Red-Black trees
- [ ] Research: How Linux kernel uses Red-Black trees
- [ ] Summarize: One system per index card (how balanced trees enable what?)
- [ ] Connect: Why does that system prefer RB over AVL?

**Conceptual Integration (30 min):**
- [ ] Answer: "When inserting into a balanced tree, when do rotations happen?"
- [ ] Answer: "Why is balance important for tree-based database indexes?"
- [ ] Draw: A scenario where insertion requires 2 rotations

### Evening Session (60 min): Problem Solving

**Tree Structure Problems (50 min):**
- [ ] LeetCode #110: Balanced Binary Tree validation → 5 min
- [ ] LeetCode #105: Construct from Preorder+Inorder → 6-8 min
- [ ] LeetCode #106: Construct from Inorder+Postorder → 6-8 min
- [ ] LeetCode #226: Invert Binary Tree → 2 min (warm-up)

**Conceptual Exercise:**
- [ ] Design: How would you rebalance a degenerate tree?

### End-of-Day Reflection (15 min):

**Concept Check:**
- [ ] AVL balance factor definition and invariant?
- [ ] Red-Black five rules (state all)?
- [ ] Why is balance important for complexity guarantees?

**Honest Assessment:**
- [ ] Do I understand rotation mechanics? (Yes/Somewhat/No)
- [ ] Can I explain why balance ensures O(log n)? (Yes/Somewhat/No)
- [ ] Confidence on balanced trees? (1-10)

**Tomorrow's Focus:** "I need to understand rotations better / I'm ready to move on"

---

## 📅 THURSDAY: TREE PATTERNS (Day 4)

### Morning Session (90 min): Path Sum & Diameter

**Read & Understand (45 min):**
- [ ] Read Day 4 Instructional file: Chapters 1-2
- [ ] Focus on path sum with backtracking (critical pattern)
- [ ] Understand tree diameter DP approach

**Conceptual Exercises (45 min):**
- [ ] Draw a tree; find all root-to-leaf paths summing to target
- [ ] Draw a tree; find the longest path (diameter)
- [ ] Explain (aloud): Why is backtracking necessary for path sum?
- [ ] Explain: How does postorder help with diameter calculation?

**Key Concepts:**
- [ ] Backtracking: Add to path, recurse, remove from path (CRITICAL ORDER)
- [ ] Diameter DP: left_height + right_height + 1, track global max
- [ ] Path sum variants: Root-to-leaf vs any path

### Midday Session (90 min): Implementation & Tracing

**Implement Path Sum Patterns (60 min):**
- [ ] **Path Sum I:** Find if any root-to-leaf path sums to target (simple DFS)
- [ ] **Path Sum II:** Find all such paths (with backtracking) ← **CRITICAL**
  - Draw step-by-step execution on paper
  - Show what happens if you forget RemoveAt()
  - Show correct execution with backtracking
- [ ] **Tree Diameter:** Find longest path between any two nodes
  - Implement postorder DP
  - Trace on your test tree
  - Verify result makes sense

**Trace Exercises (30 min):**
- [ ] Manually trace path sum with backtracking:
  - Use a tree with 5-7 nodes
  - Mark each step (add, recurse, remove)
  - Verify all paths are found exactly once
  - Show what happens without backtracking (all paths would be identical)

### Afternoon Session (90 min): LCA & Serialization

**LCA Implementation (45 min):**
- [ ] **LCA in General Tree:** Recursive three-case approach
  - If both nodes in left subtree, recurse left
  - If both in right, recurse right
  - If split, current node is LCA
- [ ] Test on tree with various node positions
- [ ] Compare to LCA in BST (much simpler with invariant)

**Serialization Implementation (45 min):**
- [ ] **Serialize:** Preorder traversal with null markers
  - Create string like "1,2,null,null,3,null,null"
  - Verify no information is lost
- [ ] **Deserialize:** Reconstruct from string
  - Use queue of tokens
  - Recursively build preorder
  - Verify roundtrip (serialize → deserialize → compare)

**Edge Case Testing:**
- [ ] Null tree → empty string/list
- [ ] Single node
- [ ] Chain-like tree
- [ ] Complete binary tree

### Evening Session (60 min): Problem Solving

**Pattern Problems (50 min):**
- [ ] LeetCode #112: Path Sum I → 3 min
- [ ] LeetCode #113: Path Sum II → 4-5 min (**BACKTRACKING**)
- [ ] LeetCode #543: Diameter → 4-5 min
- [ ] LeetCode #236: LCA → 4-5 min
- [ ] LeetCode #297: Serialize/Deserialize → 8-10 min

**Challenge (if time):**
- [ ] LeetCode #124: Maximum Path Sum (harder, harder DP logic)

### End-of-Day Reflection (15 min):

**Concept Check:**
- [ ] Explain backtracking in path sum. Why is RemoveAt() necessary?
- [ ] Explain diameter DP. What does the return value mean vs global answer?
- [ ] Explain LCA three cases. What does it mean if both nodes are in left subtree?

**Confidence Self-Assessment:**
- [ ] Confidence on pattern identification? (1-10)
- [ ] Can you code path sum with backtracking? (Yes/With hints/No)
- [ ] Can you code diameter? (Yes/With hints/No)

**Tomorrow's Focus:** "Patterns I've mastered: _____ | Patterns I need more work: _____"

---

## 📅 FRIDAY: ADVANCED & INTEGRATION (Day 5)

### Morning Session (60 min): Advanced Topics (Optional)

**Read Optional Advanced Section (30 min):**
- [ ] Skim Day 5 Instructional (Augmented BSTs, Order-Statistics)
- [ ] Understand concept of augmenting trees with subtree size
- [ ] Understand kth smallest query efficiency

**Conceptual Understanding (30 min):**
- [ ] Answer: "How would you find kth smallest in O(log n) with preprocessing?"
- [ ] Answer: "Why would a database augment trees with subtree sizes?"
- [ ] Note: This is advanced; may revisit in later weeks

### Midday Session (120 min): Integration Problems

**Solve Mixed-Concept Problems (90 min):**
- [ ] LeetCode #199: Binary Tree Right Side View (level-order variant) → 5 min
- [ ] LeetCode #101: Symmetric Tree (tree comparison) → 4 min
- [ ] LeetCode #572: Subtree of Another Tree (pattern matching) → 5 min
- [ ] LeetCode #116: Populating Next Right Pointers (level-order + linking) → 8 min
- [ ] LeetCode #337: House Robber III (DP on trees) → 8 min

**Timed Practice (30 min):**
- [ ] Pick 2-3 problems you found hard this week
- [ ] Re-solve under 10-minute time pressure
- [ ] No looking at solutions mid-way
- [ ] Reflect on what you remember from Day 1-4

### Afternoon Session (90 min): Weak Area Review & Testing

**Identify Weak Areas (20 min):**
- [ ] Review your notes from Monday-Thursday
- [ ] List topics that felt hard or unclear
- [ ] Rate confidence on each pattern (1-10)

**Targeted Review (40 min):**
- [ ] Pick your lowest-confidence topic
- [ ] Re-read that section from instructional file
- [ ] Re-trace an example by hand
- [ ] Re-code the implementation
- [ ] Solve a related problem from Stage 1

**Mock Interview (30 min):**
- [ ] Pick 3 random questions from Interview QA Reference
- [ ] Answer without looking up anything
- [ ] Record yourself (audio or video) or explain to a friend
- [ ] Time yourself (realistic interview pace)
- [ ] Assess: clarity, correctness, depth

### Evening Session (60 min): Weekly Synthesis

**Create Your Personal Summary (30 min):**
- [ ] One-page summary of the week (write by hand if possible)
- [ ] Include:
  - Top 5 insights/aha-moments
  - 3 patterns you've mastered
  - 2 patterns that need more work
  - Your personal mental model for trees (draw it)

**Plan Next Week (20 min):**
- [ ] Review Day 5 optional topics (you'll see augmented trees in more detail later)
- [ ] Note any topics to revisit before Week 8
- [ ] Plan how to maintain tree knowledge (periodic review every week)

**Celebrate & Reflect (10 min):**
- [ ] What was the most interesting thing you learned this week?
- [ ] What surprised you about trees?
- [ ] Rate your Week 7 mastery: 1-10
- [ ] If < 7/10: What one thing could you practice this weekend?

---

## 🎯 WEEKLY CHECKPOINT QUIZ (Friday Evening)

Answer these WITHOUT looking at notes or solutions:

### Conceptual Questions (40 points)
1. **Height vs Depth:** Explain difference, give example (5 pts)
2. **BST Invariant:** State it, explain why it enables binary search (5 pts)
3. **Traversal Orders:** List all 4, explain when you'd use each (8 pts)
4. **Backtracking:** Explain with path sum example (5 pts)
5. **Balance Importance:** Why does a degenerate BST become O(n)? (5 pts)
6. **Diameter DP:** Explain calculation and what you track (5 pts)
7. **Serialization:** What information must you preserve? (2 pts)

### Implementation Questions (40 points)
1. **Code preorder iterative:** 4 pts
2. **Code BST delete (two-child case):** 6 pts
3. **Code path sum with backtracking:** 6 pts
4. **Code tree diameter:** 6 pts
5. **Code LCA in general tree:** 6 pts
6. **Code serialize/deserialize:** 10 pts

### Problem-Solving (20 points)
1. **Given new problem:** Identify pattern type (3 pts)
2. **Sketch approach:** Algorithm outline (5 pts)
3. **Complexity analysis:** Time and space (3 pts)
4. **Edge cases:** What could break your solution? (5 pts)
5. **Trade-offs:** Alternative approach and why you chose your approach (4 pts)

---

## 🏆 END-OF-WEEK ASSESSMENT

### Skills Mastery (Self-Rate 1-10)

| Skill | Mon | Tue | Wed | Thu | Fri |
|---|---|---|---|---|---|
| Understand tree structure | — | — | — | — | _ |
| Implement all 4 traversals | — | — | — | — | _ |
| BST search | — | — | — | — | _ |
| BST insert | — | — | — | — | _ |
| BST delete | — | — | — | — | _ |
| Path sum with backtracking | — | — | — | — | _ |
| Tree diameter | — | — | — | — | _ |
| LCA | — | — | — | — | _ |
| Serialization | — | — | — | — | _ |
| Pattern recognition | — | — | — | — | _ |

**Target:** All items ≥ 7/10 by Friday

### Problems Solved This Week

| Stage | Count | Completed |
|---|---|---|
| Stage 1: Canonical | 30 | _ |
| Stage 2: Variations | 12 | _ |
| Stage 3: Integration | 12 | _ |
| **Total** | **54** | **_** |

**Target:** Complete at least 40-45 problems this week

### Time Tracking

| Day | Planned | Actual | Notes |
|---|---|---|---|
| Monday | 4.5 hrs | _ hrs | _ |
| Tuesday | 4.5 hrs | _ hrs | _ |
| Wednesday | 4.5 hrs | _ hrs | _ |
| Thursday | 4.5 hrs | _ hrs | _ |
| Friday | 4.5 hrs | _ hrs | _ |
| **Total** | **22.5 hrs** | **_ hrs** | **_ |

---

## 🔍 DAILY HABITS FOR RETENTION

### Every Day (Non-Negotiable)

- [ ] Start with a 5-min warmup: Implement one simple traversal from memory
- [ ] End with 15-min reflection: What did you learn today? What's unclear?
- [ ] Trace by hand: At least one problem without coding
- [ ] Explain out loud: Each key concept (use your voice to internalize)

### Weak Area Drill (If struggling)

- [ ] Re-read that section (different author perspective helps)
- [ ] Trace a simpler example (go from complex → simple)
- [ ] Implement in a new file (fresh start, fewer copied lines)
- [ ] Explain to someone else (forces clarity)
- [ ] Ask for hints, not solutions (keep thinking)

### Strength Amplification (If confident)

- [ ] Challenge: Solve hard version of pattern (LeetCode Hard)
- [ ] Optimize: Improve space/time of your solution
- [ ] Teach: Explain your approach to someone
- [ ] Extend: Create your own variation of a problem

---

## 🚀 BEYOND WEEK 7

**This Weekend:**
- [ ] Review any weak areas from the week
- [ ] Attempt 1-2 problems from Stage 3 (Integration) that you skipped

**Next Week (Week 8):**
- [ ] Graphs extend tree concepts (DFS, BFS as traversals)
- [ ] Many graph problems use tree-like thinking
- [ ] Maintain tree knowledge with quick weekly review

**Long-term:**
- [ ] Augmented trees (Week 9+)
- [ ] Self-balancing implementations (advanced)
- [ ] Tree DP patterns (Week 10+)
- [ ] Periodic review: Every 2 weeks, re-solve one Stage 1 problem

---

## 📞 TROUBLESHOOTING GUIDE

**Problem: "I keep forgetting to backtrack in path sum"**  
→ Solution: Write a comment before recursion: `// TODO: Remove after recursion`. Check comments before submitting.

**Problem: "Serialization is confusing"**  
→ Solution: Do roundtrip on paper first. Serialize a 5-node tree by hand, then deserialize. Verify you get same tree back.

**Problem: "I don't know which traversal to use"**  
→ Solution: Reference the decision flowchart in Problem-Solving Roadmap. For every problem, identify WHY that traversal (not just accept the pattern).

**Problem: "Tree DP (like diameter) feels magic"**  
→ Solution: Always explicitly write what your function returns. e.g., "height() returns the height of the subtree rooted at this node." Know this before coding.

**Problem: "I'm running out of time during practice"**  
→ Solution: Reduce problem count (quality > quantity). Spend more time on Stage 1 until fluent, then move to Stage 2.

**Problem: "I understand examples but can't solve new problems"**  
→ Solution: You're pattern-matching, not understanding. Go back to Stage 1, solve problems without looking at examples. Explain the approach before coding.

---

**Status:** ✅ Daily Progress Checklist Complete

*Remember: This is your personal learning roadmap. Adapt it to your pace. If Monday feels rushed, extend to Tuesday. If you finish early, advance to Stage 2. The goal is mastery, not speed.*

