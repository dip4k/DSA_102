# 📘 WEEK 13: BACKTRACKING & BRANCH & BOUND
## Phase D: Algorithm Paradigms | 25 Hours | Search-Based Problem Solving

---

## 🎯 WEEKLY GOAL

**Master backtracking for combinatorial problems and branch & bound for systematic optimization.**

By end of Week 13, you will:
- ✅ Understand backtracking concept and state space tree
- ✅ Design backtracking algorithms with pruning
- ✅ Solve N-queens, Sudoku, permutations, combinations
- ✅ Understand branch & bound optimization strategy
- ✅ Apply branch & bound to TSP and knapsack
- ✅ Master amortized analysis (all three methods)
- ✅ Know when to combine multiple paradigms

---

# 📅 DAY 1: BACKTRACKING FUNDAMENTALS & STATE SPACE TREES
## 5 Hours | Topics: Concept, Template, Pruning Strategies

---

## 🎓 PART 1: WHAT IS BACKTRACKING? (90 minutes)

### Backtracking: The Core Idea

```
┌──────────────────────────────────────────────────────┐
│            BACKTRACKING DEFINITION                   │
│                                                      │
│  Backtracking is a general problem-solving technique│
│  that considers searching every possible combo:      │
│                                                      │
│  1. Build solution incrementally                     │
│  2. At each step, try all valid next choices        │
│  3. If choice leads nowhere (dead end):              │
│     BACKTRACK and try next choice                    │
│  4. If all choices lead nowhere: fail that branch    │
│  5. If reach complete solution: success!             │
│                                                      │
│  Key: Explores decision tree via DFS                │
│       Prunes branches that can't succeed             │
└──────────────────────────────────────────────────────┘
```

### Backtracking vs Brute Force

```
┌──────────────────────────────────────────────────────┐
│    BACKTRACKING ≠ SIMPLE BRUTE FORCE                │
│                                                      │
│  Brute Force:                                        │
│  • Generate ALL possible solutions                   │
│  • Test each one                                     │
│  • Very slow: O(2ⁿ) or worse                         │
│  • No optimization                                   │
│  Example: Try all n! permutations                    │
│                                                      │
│  Backtracking:                                       │
│  • Generate solutions INTELLIGENTLY                  │
│  • Prune dead ends EARLY                             │
│  • Still exponential worst case, but MUCH faster     │
│  • Can solve larger problems                         │
│  Example: N-queens: prune based on attack positions │
│                                                      │
│  Key difference: PRUNING                             │
│  Backtracking asks: "Can this lead to solution?"     │
│  If NO → stop exploring this branch                  │
│  If YES → continue deeper                            │
└──────────────────────────────────────────────────────┘
```

### Backtracking Template

```
┌──────────────────────────────────────────────────────┐
│         UNIVERSAL BACKTRACKING TEMPLATE              │
│                                                      │
│  function backtrack(current_solution, state):        │
│    1. BASE CASE: Is current_solution complete?       │
│       If YES: record solution, return               │
│                                                      │
│    2. PRUNING: Can current_solution lead to valid?  │
│       If NO: prune this branch, return              │
│                                                      │
│    3. EXPLORE: For each valid next choice:          │
│       a. Add choice to solution                     │
│       b. Recursively backtrack with new state       │
│       c. Remove choice from solution (undo)         │
│                                                      │
│  Key aspects:                                        │
│  • Recursive structure (natural for exploration)     │
│  • Explicit backtracking (remove choice)             │
│  • Early pruning (avoid dead ends)                   │
│  • Base case (found solution or impossible)         │
└──────────────────────────────────────────────────────┘
```

### Why "Backtracking"?

```
┌──────────────────────────────────────────────────────┐
│       WHY THE NAME "BACKTRACKING"?                   │
│                                                      │
│  Visualization of search process:                    │
│                                                      │
│  Start at root of decision tree:
│        ┌─ Root ─┐
│        │ [ ]   │
│        └───────┘
│           / | \
│          /  |  \
│         /   |   \
│      ┌─'  ┌─'  ┌─'
│    Node1 Node2 Node3
│      /     X    / \
│     /           /   \
│    [...]     Leaf   [...]
│    
│  Exploration process:
│  1. Go deep: [ ] → Node1 → ...
│  2. Hit dead end or find solution
│  3. BACKTRACK: return to previous choice
│  4. TRY NEXT: explore Node2
│  5. SKIP: Node2 pruned (X)
│  6. BACKTRACK again: try Node3
│
│  Like hiking in forest:
│  • Go down trail (Node1, Node2, ...)
│  • Hit dead end (cliff)
│  • BACKTRACK to junction
│  • Try different path
│  • Mark failed paths to avoid retry
│
│  In code: UNDO the choice (remove from solution)
└──────────────────────────────────────────────────────┘
```

---

## 🎓 PART 2: STATE SPACE TREE (90 minutes)

### What is State Space Tree?

```
┌──────────────────────────────────────────────────────┐
│          STATE SPACE TREE CONCEPT                    │
│                                                      │
│  Definition: Tree representing all possible states   │
│  of solving a problem                                │
│                                                      │
│  Nodes: Partial solutions                            │
│  Edges: Decisions/choices                            │
│  Leaves: Complete solutions or dead ends             │
│                                                      │
│  Example: 3-Queens (place 3 queens on 3×3 board)    │
│  State space tree for column-by-column placement:    │
│                                                      │
│         ┌─ Root ─┐                                   │
│         │ [] (no queens)                             │
│         └───┬────────────┬─────────────┘             │
│             |            |             |             │
│        ┌────┘            |       ┌─────┘             │
│        |                 |       |                    │
│    [Q₁ at         [Q₁ at     [Q₁ at                  │
│     row0]         row1]      row2]                   │
│       / \            |          / \                  │
│      /   \           |         /   \                 │
│  [Q₁,Q₂  [Q₁,Q₂  [Q₁,Q₂    [Q₁,Q₂ [Q₁,Q₂          │
│   rows   rows    rows       rows    rows              │
│   0,1]   0,2]    1,0]       1,2]    2,0]            │
│    X      ✓       X         X       X (continue)     │
│   (Q's           (valid                              │
│    attack)      so far)                              │
│                                                      │
│  ✓ = Valid partial solution (can continue)          │
│  X = Dead end (violates constraint)                 │
│  (continue) = Check next level                       │
│                                                      │
│  PRUNING: Don't explore X branches!                 │
└──────────────────────────────────────────────────────┘
```

### Visual Example: Complete State Space for 3-Queens

```
If we only consider valid-so-far states:

                    ┌─ Root: [] ─┐
                    │ (0 queens)  │
                    └──────┬──────┘
                           │
        ┌──────────────────┼──────────────────┐
        │                  │                  │
    [Q at 0]          [Q at 1]           [Q at 2]
        │                  │                  │
    ┌───┴───┐              │              ┌───┴───┐
    │       │              │              │       │
[Q Q at  [Q Q at      [Q Q at       [Q Q at  [Q Q at
 00 12]   00 20]      01 10]       02 10]   02 20]
    │       │              │              │       │
    X       ✓              X              X       X
  (Q₂     (valid)      (Q's          (Q's     (Q's
 attacks)             attack)       attack)  attack)

Only continue from ✓ states to depth 3:

[Q Q Q at:
 0 1 2] = three queens at rows 0,1,2 of columns 0,1,2
 
Check final state: valid or not?

Actually for 3-queens: NO valid solution exists
(too small board)

The state space tree shows this through exhaustive search!
```

### Node Classification

```
┌──────────────────────────────────────────────────────┐
│      STATE SPACE TREE NODE CLASSIFICATIONS          │
│                                                      │
│  Root Node:                                          │
│  • Empty solution [ ]                                │
│  • Starting point for backtracking                   │
│                                                      │
│  Internal Nodes:                                     │
│  • Partial solutions (some choices made)             │
│  • Can potentially lead to complete solution        │
│  • Need to explore further                           │
│                                                      │
│  Leaf Nodes:                                         │
│  • Option A: Complete solution (ALL choices made)   │
│    Example: All queens placed, valid config         │
│    ACCEPT: Record this solution                      │
│                                                      │
│  • Option B: Dead end (can't continue validly)      │
│    Example: Queen conflicts, no valid next move     │
│    REJECT: Prune (don't explore deeper)             │
│                                                      │
│  • Option C: Pruned node (violates early constraint)│
│    Example: Two queens already attacking            │
│    SKIP: Never add to frontier                      │
│                                                      │
│  Pruning = Avoiding exploring subtrees              │
│           under dead-end nodes!                      │
└──────────────────────────────────────────────────────┘
```

---

## 🎓 PART 3: PRUNING STRATEGIES (90 minutes)

### What is Pruning?

```
┌──────────────────────────────────────────────────────┐
│              PRUNING DEFINITION                      │
│                                                      │
│  Pruning: Not exploring branches that can't         │
│           possibly lead to valid solutions           │
│                                                      │
│  Question: Why explore when we know it'll fail?     │
│  Answer: We don't always "know"... but we can GUESS │
│                                                      │
│  Pruning works if:                                   │
│  • Can quickly check: "Is this feasible?"            │
│  • If not feasible: all deeper choices also fail     │
│  • So we can skip entire subtree                     │
│                                                      │
│  Example: N-Queens                                   │
│  • If two queens attack each other NOW               │
│  • Then adding more queens WON'T fix it              │
│  • So we can prune this branch                       │
│                                                      │
│  Benefit: Exponentially fewer nodes explored!        │
└──────────────────────────────────────────────────────┘
```

### Pruning Strategy 1: Constraint Checking

```
┌──────────────────────────────────────────────────────┐
│       PRUNING STRATEGY: CONSTRAINT CHECK             │
│                                                      │
│  At each step, verify constraints:                  │
│                                                      │
│  N-Queens Example:                                   │
│  • After placing queen i:                            │
│    - Check: Does it attack any previous queen?      │
│    - If YES: Violates constraint, PRUNE             │
│    - If NO: Can continue, explore deeper            │
│                                                      │
│  Sudoku Example:                                     │
│  • After placing digit d in cell (i,j):             │
│    - Check: Is d unique in row i?                    │
│    - Check: Is d unique in column j?                 │
│    - Check: Is d unique in 3×3 box?                  │
│    - If any check fails: PRUNE                       │
│    - If all pass: Can continue                       │
│                                                      │
│  General pattern:                                    │
│                                                      │
│  if constraint_violated(current_state):             │
│      return  // Prune: don't explore subtree        │
│  else:                                               │
│      continue exploring                              │
└──────────────────────────────────────────────────────┘
```

### Pruning Strategy 2: Bound-Based Pruning

```
┌──────────────────────────────────────────────────────┐
│      PRUNING STRATEGY: BOUNDING PRUNING             │
│                                                      │
│  Estimate: Can this path achieve goal?               │
│  How? Compute bound (upper/lower estimate)           │
│                                                      │
│  Optimization example: Maximize value               │
│  • Current partial solution value: 50               │
│  • Optimistic bound: +100 possible                  │
│  • Best we can get from this path: 150              │
│                                                      │
│  Compare with global best:                           │
│  • Global best achieved so far: 140                 │
│  • Our bound of 150 > 140: KEEP exploring          │
│                                                      │
│  But if:                                             │
│  • Global best: 200                                  │
│  • Our bound: 180                                    │
│  • Can't beat global best: PRUNE!                   │
│                                                      │
│  Key insight:                                        │
│  Even in best case, can't improve → give up!         │
│                                                      │
│  Requires:                                           │
│  1. Ability to compute upper/lower bound             │
│  2. Track best solution found so far                │
│  3. Compare: if bound ≤ best, prune                 │
└──────────────────────────────────────────────────────┘
```

### Pruning Strategy 3: Monotonicity

```
┌──────────────────────────────────────────────────────┐
│      PRUNING STRATEGY: MONOTONIC BOUNDS             │
│                                                      │
│  Observation: If property is monotonic decreasing,  │
│  we can prune early                                  │
│                                                      │
│  Example: Sudoku solving                             │
│  • Define: f(state) = number of empty cells         │
│  • Property: f always decreases as we fill cells    │
│  • If f(state) = 0: solved or invalid               │
│  • If f(state) = 81: completely full (check valid)  │
│                                                      │
│  Example: TSP                                        │
│  • Define: f(tour) = edges added so far             │
│  • Property: f always increases (tour grows)        │
│  • If f(tour) = n: complete tour (calculate cost)  │
│  • If f(tour) < n: partial tour                     │
│                                                      │
│  Monotonic property enables:                         │
│  • Predictable progress                              │
│  • Early termination conditions                      │
│  • Pruning based on state properties                 │
└──────────────────────────────────────────────────────┘
```

---

## 📋 DAY 1 SUMMARY

```
┌──────────────────────────────────────────────────────┐
│           DAY 1: KEY TAKEAWAYS                       │
│                                                      │
│  ✓ Backtracking = incremental solution building     │
│  ✓ Recurse deeply, backtrack when dead end          │
│  ✓ Key: PRUNING makes it efficient                   │
│                                                      │
│  ✓ STATE SPACE TREE represents all possibilities     │
│  ✓ Nodes = partial solutions                         │
│  ✓ Edges = choices/decisions                         │
│  ✓ Leaves = complete solutions or dead ends          │
│                                                      │
│  ✓ PRUNING STRATEGIES:                               │
│    1. Constraint checking (violates rule → prune)    │
│    2. Bound-based (can't improve → prune)            │
│    3. Monotonic properties (predictable progress)    │
│                                                      │
│  ✓ Backtracking ≠ brute force (pruning is key!)      │
│  ✓ Template: recurse, prune, explore, backtrack      │
└──────────────────────────────────────────────────────┘
```

---

# 📅 DAY 2: BACKTRACKING PROBLEMS - N-QUEENS & SUDOKU
## 5 Hours | Classic Problem Applications

---

## 🎓 PART 1: N-QUEENS PROBLEM (90 minutes)

### Problem Definition

```
┌──────────────────────────────────────────────────────┐
│            N-QUEENS PROBLEM                          │
│                                                      │
│  Given: n×n chessboard                               │
│  Goal: Place n queens such that NO two attack each   │
│                                                      │
│  Queens attack: same row, column, or diagonal        │
│                                                      │
│  Example: 4-Queens board (one valid solution)        │
│                                                      │
│      Col: 0 1 2 3                                    │
│    ┌────────────────┐                                │
│  0 │   Q   .   .   . │                                │
│  1 │   .   .   .   Q │                                │
│  2 │   Q   .   .   . │  INVALID! Queens in            │
│  3 │   .   .   Q   . │  different columns            │
│    └────────────────┘     but let me try again:      │
│                                                      │
│      Col: 0 1 2 3                                    │
│    ┌────────────────┐                                │
│  0 │   .   Q   .   . │                                │
│  1 │   .   .   .   Q │                                │
│  2 │   Q   .   .   . │  VALID!                        │
│  3 │   .   .   Q   . │  No two queens                 │
│    └────────────────┘   attack each other            │
│                                                      │
│  Note: Exactly one queen per row, per column         │
│        (by definition of placement)                  │
│        Only need to check DIAGONALS!                 │
└──────────────────────────────────────────────────────┘
```

### How Backtracking Solves N-Queens

```
┌──────────────────────────────────────────────────────┐
│    N-QUEENS BACKTRACKING STRATEGY                    │
│                                                      │
│  Key insight: Place one queen per row                │
│  (automatically ensures row uniqueness)              │
│                                                      │
│  Decision point: Which COLUMN in each row?           │
│                                                      │
│  Algorithm:                                          │
│  1. Start at row 0                                   │
│  2. Try each column 0 to n-1 for queen in row 0     │
│  3. For each column c:                               │
│     a. Place queen at (row 0, column c)              │
│     b. Check: Is it safe? (not attacking prev)       │
│     c. If safe: Move to row 1 (recurse)              │
│     d. If unsafe: Try next column                    │
│  4. When reach row n: Found solution! Record it     │
│  5. Backtrack: try previous row's next column        │
│                                                      │
│  Pruning:                                            │
│  • Check diagonal conflicts with previous queens    │
│  • If conflict: don't even try this column          │
│  • Move to next column immediately                  │
└──────────────────────────────────────────────────────┘
```

### Visual Example: Solving 4-Queens

```
State space exploration (showing only valid states):

                  ┌─ Row 0 ─┐
                  │ [ ]     │
                  └────┬────┘
         ┌─────────────┼─────────────┐
         │             │             │
    Col 0         Col 1 ✓       Col 2
    Queen         Queen         Invalid
   (valid)      (safe so far)   (diagonal)
         │             │             X
    ┌─ R1 ─┐      ┌─ R1 ─┐
    │[Q..]│      │[.Q..]│
    └──┬──┘      └──┬──┘
       │             │
   Col 0,1,2     Col 0,1,2,3
   (invalid)     ...
       X
   (conflict)
              │
         ┌─ R2 ─┐
         │[.Q..]│ at col 3 in R1
         └──┬──┘
            │
        Try cols...
            │
        ┌─ R3 ─┐
        │[.Q.Q]│ at col 3 in R1, col 0 in R3
        └──┬──┘
           │
       Check: Valid solution!
       Record: [.Q.Q] (actually [1,3,0,2] positions)

Continue: Backtrack from R3, try other configs...
```

### Diagonal Conflict Checking

```
┌──────────────────────────────────────────────────────┐
│      DIAGONAL CONFLICT IN N-QUEENS                   │
│                                                      │
│  Two queens attack diagonally if:                    │
│  |row₁ - row₂| = |col₁ - col₂|                      │
│  (same distance vertically and horizontally)         │
│                                                      │
│  Example:                                            │
│  Queen 1 at (0, 0)                                   │
│  Queen 2 at (2, 2)                                   │
│  |0-2| = 2, |0-2| = 2  → Diagonal! Conflict!       │
│                                                      │
│  Queen 1 at (1, 1)                                   │
│  Queen 2 at (2, 2)                                   │
│  |1-2| = 1, |1-2| = 1  → Diagonal! Conflict!       │
│                                                      │
│  But:                                                │
│  Queen 1 at (0, 0)                                   │
│  Queen 2 at (2, 1)                                   │
│  |0-2| = 2, |0-1| = 1  → Not diagonal! Safe!       │
│                                                      │
│  Pruning condition:                                  │
│  For each previously placed queen at (r_prev, c_prev)│
│  and new queen candidate at (r_new, c_new):         │
│                                                      │
│  if |r_prev - r_new| = |c_prev - c_new|:            │
│      PRUNE (don't place queen here)                  │
│  else:                                               │
│      SAFE (can explore this subtree)                 │
└──────────────────────────────────────────────────────┘
```

---

## 🎓 PART 2: SUDOKU SOLVER VIA BACKTRACKING (90 minutes)

### Sudoku Constraints Review

```
┌──────────────────────────────────────────────────────┐
│          SUDOKU CONSTRAINTS                          │
│                                                      │
│  Rules:                                              │
│  • 9×9 grid divided into 3×3 boxes                   │
│  • Fill with digits 1-9                              │
│  • Constraint 1: Each row has digits 1-9 (unique)   │
│  • Constraint 2: Each column has 1-9 (unique)       │
│  • Constraint 3: Each 3×3 box has 1-9 (unique)      │
│                                                      │
│  Example cell (3, 4):                                │
│  • Row 3: can't repeat any digit in row 3           │
│  • Column 4: can't repeat any digit in column 4     │
│  • Box containing (3,4): 3×3 box starting at...     │
│    Row: 3 // 3 * 3 = 3 (rows 3-5)                  │
│    Col: 4 // 3 * 3 = 3 (cols 3-5)                  │
│    So cell (3,4) in box starting at (3,3)          │
│                                                      │
│  Three constraints = high pruning potential!         │
└──────────────────────────────────────────────────────┘
```

### Sudoku Backtracking Strategy

```
┌──────────────────────────────────────────────────────┐
│    SUDOKU BACKTRACKING APPROACH                      │
│                                                      │
│  Key insight: Find empty cell, try digits 1-9       │
│                                                      │
│  Algorithm:                                          │
│  1. Find first empty cell (value = 0)                │
│  2. If no empty cell: Found solution! Return         │
│  3. For each digit d = 1 to 9:                       │
│     a. Check: Is d valid in this cell?              │
│        (not in row, column, or 3×3 box)             │
│     b. If valid: Place d in cell (tentatively)       │
│     c. Recursively solve rest (backtrack call)       │
│     d. If recursive call succeeds: Return TRUE       │
│     e. If fails: Remove d (undo), try next digit    │
│  4. If no digit works: Return FALSE (dead end)       │
│                                                      │
│  Pruning:                                            │
│  • Before placing digit: check 3 constraints        │
│  • If ANY constraint violated: skip this digit      │
│  • Only place if ALL constraints satisfied          │
│                                                      │
│  Optimization opportunities:                        │
│  • Choose cell with minimum candidates first        │
│  • (most constrained variable heuristic)            │
│  • Prunes more aggressively                          │
└──────────────────────────────────────────────────────┘
```

### Sudoku Example: Partial Solution

```
Starting Sudoku (partially filled):

  5 3 _ | _ 7 _ | _ _ _
  6 _ _ | 1 9 5 | _ _ _
  _ 9 8 | _ _ _ | _ 6 _
  ------+-------+------
  8 _ _ | _ 6 _ | _ _ 3
  4 _ _ | 8 _ 3 | _ _ 1
  7 _ _ | _ 2 _ | _ _ 6
  ------+-------+------
  _ 6 _ | _ _ _ | 2 8 _
  _ _ _ | 4 1 9 | _ _ 5
  _ _ _ | _ 8 _ | _ 7 9

Backtracking process:

1. Find first empty cell: (0, 2) - can be 1,2,3,4,...
2. Check constraints for position (0,2):
   - Row 0: has 5,3,7 → can't use these
   - Col 2: has 8 → can't use this
   - Box (0,0): has 5,3,6,9,8 → can't use these
   - Candidates: {1,2,4}
3. Try 1: place at (0,2), recurse → fails (eventually)
4. Backtrack: remove 1, try 2 → fails
5. Backtrack: remove 2, try 4 → succeeds! Continue...

This exploration continues recursively until entire grid filled.
```

---

## 🎓 PART 3: OTHER BACKTRACKING PROBLEMS (90 minutes)

### Permutations and Combinations

```
┌──────────────────────────────────────────────────────┐
│    GENERATING PERMUTATIONS VIA BACKTRACKING          │
│                                                      │
│  Problem: Generate all permutations of {1,2,3}       │
│                                                      │
│  State space tree:                                   │
│         [Empty]                                      │
│         /  |  \                                      │
│        1   2   3                                     │
│       / \ / \ / \                                    │
│      2 3 1 3 1 2                                     │
│      |   |   |                                       │
│      3   3   2                                       │
│    [1,2,3] [2,1,3] [3,2,1] etc.                     │
│                                                      │
│  Backtracking approach:                              │
│  1. Start with empty sequence                        │
│  2. Track "used" set (which elements already picked) │
│  3. At each step: try each unused element           │
│  4. Add to sequence, mark as used                    │
│  5. Recurse (build longer permutation)               │
│  6. When sequence length = n: record solution       │
│  7. Backtrack: remove element, mark as unused       │
│                                                      │
│  Pruning: Natural (can't use same element twice)     │
│  No constraint checking needed!                      │
└──────────────────────────────────────────────────────┘
```

### Combinations Generation

```
┌──────────────────────────────────────────────────────┐
│    GENERATING COMBINATIONS VIA BACKTRACKING          │
│                                                      │
│  Problem: Generate all 2-combinations of {1,2,3,4}   │
│  Result: {1,2}, {1,3}, {1,4}, {2,3}, {2,4}, {3,4}   │
│                                                      │
│  Difference from permutations:                       │
│  • Don't track "used" globally                       │
│  • Instead: only consider elements ≥ last picked    │
│  • Prevents duplicates (e.g., {1,2} vs {2,1})       │
│                                                      │
│  Backtracking approach:                              │
│  1. Start with empty sequence                        │
│  2. Track minimum element to consider (start = 1)    │
│  3. At each step: try each element ≥ minimum        │
│  4. Add to sequence                                  │
│  5. Recurse: with minimum = current element + 1     │
│  6. When sequence length = k: record solution       │
│  7. Backtrack: remove element                        │
│                                                      │
│  Example process:                                    │
│  Pick 1: min becomes 2                               │
│    Pick 2: sequence [1,2], record, backtrack        │
│    Pick 3: sequence [1,3], record, backtrack        │
│    Pick 4: sequence [1,4], record, backtrack        │
│  Backtrack from 1, pick 2: min becomes 3            │
│    Pick 3: sequence [2,3], record, backtrack        │
│    Pick 4: sequence [2,4], record, backtrack        │
│  Backtrack from 2, pick 3: min becomes 4            │
│    Pick 4: sequence [3,4], record, backtrack        │
│                                                      │
│  Result: {1,2}, {1,3}, {1,4}, {2,3}, {2,4}, {3,4}  │
│  No duplicates!                                      │
└──────────────────────────────────────────────────────┘
```

### Word Search in Grid

```
┌──────────────────────────────────────────────────────┐
│      WORD SEARCH: BACKTRACKING APPROACH              │
│                                                      │
│  Problem: Find word "BACKTRACK" in 2D grid           │
│                                                      │
│  Grid:                                               │
│    B A X C D                                         │
│    K C K T O                                         │
│    A R O P A                                         │
│    C T E E R                                         │
│    T A K K C                                         │
│                                                      │
│  Goal: Find path spelling "BACKTRACK"                │
│  Path must be adjacent cells (not diagonal)          │
│                                                      │
│  Backtracking:                                       │
│  1. Start at any cell matching first letter (B)     │
│  2. For each neighbor: does it match next letter?   │
│  3. If yes: add to path, mark visited, recurse      │
│  4. If reach end of word: SUCCESS!                   │
│  5. Backtrack: unmark visited, try other neighbor   │
│  6. If no neighbors match: dead end, backtrack      │
│                                                      │
│  Path found:                                         │
│  B(0,0) → A(0,1) → C(1,1) → K(1,0) → T(4,0)        │
│  → R(3,1) → A(2,0) → C(3,0) → K(4,2)                │
│  = "BACKTRACK" ✓                                     │
│                                                      │
│  Pruning: Track visited cells to avoid cycles       │
│           If next letter not available adjacent: skip│
└──────────────────────────────────────────────────────┘
```

---

## 📋 DAY 2 SUMMARY

```
┌──────────────────────────────────────────────────────┐
│           DAY 2: KEY TAKEAWAYS                       │
│                                                      │
│  ✓ N-Queens: place one queen per row, check diagonals│
│  ✓ Sudoku: find empty cell, try digits with constraints│
│  ✓ Permutations: track used elements                 │
│  ✓ Combinations: only consider elements ≥ last      │
│  ✓ Word Search: find path in graph with DFS         │
│                                                      │
│  ✓ All use same backtracking template:               │
│    1. Find choice point                              │
│    2. Try each option                                │
│    3. Recursively solve subproblem                   │
│    4. Backtrack if fails                             │
│    5. Try next option                                │
│                                                      │
│  ✓ Key: Effective pruning makes it fast              │
│  ✓ Pruning depends on problem constraints            │
└──────────────────────────────────────────────────────┘
```

---

# 📅 DAY 3: BRANCH & BOUND OPTIMIZATION
## 5 Hours | Systematic Optimization Strategy

---

## 🎓 PART 1: BRANCH & BOUND CONCEPT (90 minutes)

### What is Branch & Bound?

```
┌──────────────────────────────────────────────────────┐
│         BRANCH & BOUND DEFINITION                    │
│                                                      │
│  Algorithm design technique for optimization        │
│  problems (maximization or minimization)             │
│                                                      │
│  Components:                                         │
│  • BRANCHING: Divide problem into subproblems       │
│  • BOUNDING: Compute upper/lower bounds on solutions│
│  • PRUNING: Eliminate branches that can't improve   │
│                                                      │
│  Process:                                            │
│  1. Branch: Systematically explore solution space   │
│     (similar to backtracking)                        │
│  2. Bound: Calculate best/worst case for each node  │
│  3. Prune: If bound can't beat current best,        │
│     don't explore subtree                            │
│                                                      │
│  Result: Optimal solution found FAST                 │
│          (prunes massive portions of search space)   │
└──────────────────────────────────────────────────────┘
```

### Branch & Bound vs Backtracking

```
┌──────────────────────────────────────────────────────┐
│   BRANCH & BOUND vs BACKTRACKING                    │
│                                                      │
│  BACKTRACKING:                                       │
│  • Test FEASIBILITY: Is solution valid?              │
│  • Prune infeasible branches                         │
│  • Find ANY valid solution (or all solutions)        │
│  • Example: N-Queens (feasible = no attacks)         │
│                                                      │
│  BRANCH & BOUND:                                     │
│  • Test OPTIMALITY: Is solution better than best?    │
│  • Prune sub-optimal branches                        │
│  • Find OPTIMAL solution                             │
│  • Track best solution found so far                  │
│  • Example: TSP (optimal = shortest tour)            │
│                                                      │
│  Key difference:                                      │
│  Backtracking: "Is this valid?" → feasibility       │
│  B&B: "Can this beat current best?" → optimality    │
│                                                      │
│  Overlap: B&B can use feasibility pruning too!       │
│           But primary purpose is optimality          │
└──────────────────────────────────────────────────────┘
```

### The B&B Template

```
┌──────────────────────────────────────────────────────┐
│    BRANCH & BOUND ALGORITHM TEMPLATE                 │
│                                                      │
│  function branchAndBound():                          │
│    best_solution = null                              │
│    best_value = -∞ (for maximization)                │
│    frontier = [root_node]                            │
│                                                      │
│    while frontier not empty:                         │
│      node = frontier.pop()                           │
│                                                      │
│      # Bounding                                       │
│      upper_bound = compute_upper_bound(node)         │
│      if upper_bound ≤ best_value:                    │
│        continue  # Prune: can't improve              │
│                                                      │
│      # Check if complete solution                     │
│      if node is complete_solution:                   │
│        if value(node) > best_value:                  │
│          best_value = value(node)                    │
│          best_solution = node                        │
│      else:                                            │
│        # Branching: generate children                 │
│        for each child in expand(node):               │
│          frontier.add(child)                         │
│                                                      │
│    return best_solution                              │
└──────────────────────────────────────────────────────┘
```

---

## 🎓 PART 2: BOUNDING TECHNIQUES (90 minutes)

### Upper Bounds for Maximization

```
┌──────────────────────────────────────────────────────┐
│     UPPER BOUND: MAXIMIZATION PROBLEMS               │
│                                                      │
│  Goal: Find maximum value we could POSSIBLY achieve  │
│                                                      │
│  Strategy: Relax problem to something easier         │
│           Solve relaxed version (gives upper bound)  │
│                                                      │
│  Property: Relaxed value ≥ actual optimal value     │
│           (because we relaxed constraints)          │
│                                                      │
│  Knapsack Example:                                   │
│  • 0/1 Knapsack: must take whole item or nothing     │
│  • Fractional knapsack: can take fractions (relax)   │
│  • Fractional knapsack value ≥ 0/1 value            │
│  • So: fractional solution = upper bound for 0/1    │
│                                                      │
│  Usage in B&B:                                       │
│  if upper_bound(partial) ≤ current_best:             │
│      prune (can't find better even in best case)     │
│                                                      │
│  Example:                                            │
│  Current best: value 100                             │
│  Node's upper bound: 95                              │
│  Even if perfect luck, can only get 95 < 100        │
│  → Prune this node                                  │
└──────────────────────────────────────────────────────┘
```

### Lower Bounds for Maximization

```
┌──────────────────────────────────────────────────────┐
│     LOWER BOUND: MAXIMIZATION PROBLEMS               │
│                                                      │
│  Goal: Find minimum value we know we can achieve     │
│                                                      │
│  Strategy: ANY feasible solution gives lower bound   │
│                                                      │
│  Property: Actual optimal ≥ lower bound              │
│           (because lower bound is feasible)         │
│                                                      │
│  Usage in B&B:                                       │
│  1. Initially: lower_bound = 0 (or first solution)   │
│  2. When find complete solution: might update lower  │
│     if value better than current best                │
│  3. Use lower bound to prune: discard nodes with     │
│     upper_bound < lower_bound                        │
│                                                      │
│  Example:                                            │
│  Lower bound (best found so far): 80                 │
│  Node's upper bound: 75                              │
│  Even in best case, can't beat lower → Prune        │
└──────────────────────────────────────────────────────┘
```

### Computing Bounds: TSP Example

```
┌──────────────────────────────────────────────────────┐
│       TSP BOUNDING: LOWER BOUND EXAMPLE              │
│                                                      │
│  Traveling Salesman Problem:                         │
│  • Visit all n cities exactly once                   │
│  • Return to start                                   │
│  • Minimize total distance                           │
│                                                      │
│  Lower bound technique: Minimum spanning tree        │
│                                                      │
│  Reasoning:                                          │
│  • Any tour connects all cities                      │
│  • Tour ≥ MST (MST is minimum to connect all)       │
│  • So: MST_cost ≤ optimal_tour_cost                  │
│  • Therefore: MST_cost is lower bound                │
│                                                      │
│  Better lower bound:                                 │
│  • MST has n-1 edges                                 │
│  • Tour has n edges                                  │
│  • MST_cost + min_edge_cost ≥ tour cost             │
│  • So: (MST_cost + min_edge_to_close) is tighter     │
│                                                      │
│  Upper bound: Any complete tour                      │
│  • Use greedy heuristic: nearest neighbor           │
│  • Find valid tour quickly                           │
│  • Greedy_tour ≥ optimal_tour (worst case)          │
│  • Use as upper bound for pruning                    │
└──────────────────────────────────────────────────────┘
```

---

## 🎓 PART 3: BEST-FIRST SEARCH & OPTIMIZATION (90 minutes)

### Best-First Search Strategy

```
┌──────────────────────────────────────────────────────┐
│       BEST-FIRST SEARCH FOR B&B                      │
│                                                      │
│  Strategy: Explore most promising nodes first        │
│                                                      │
│  Queue: Priority queue (ordered by bounds)           │
│                                                      │
│  For maximization:                                   │
│  • Sort by upper_bound (descending)                  │
│  • Process highest upper_bound first                 │
│  • Rationale: most likely to find improvements       │
│                                                      │
│  Algorithm:                                          │
│  1. Initialize: frontier = [root]                    │
│  2. best_value = -∞                                  │
│  3. While frontier not empty:                        │
│     a. Pop node with highest bound                   │
│     b. If bound ≤ best_value: skip (prune)          │
│     c. If complete: update best_value                │
│     d. Else: add children with bounds to frontier    │
│  4. Return best solution found                       │
│                                                      │
│  Benefit: Often finds good solution early            │
│          Allows aggressive pruning later             │
└──────────────────────────────────────────────────────┘
```

### Example: 0/1 Knapsack with B&B

```
Knapsack capacity: 10 kg
Items:
  A: weight 5, value $50 (ratio 10)
  B: weight 6, value $60 (ratio 10)
  C: weight 2, value $15 (ratio 7.5)

Branch & Bound tree (simplified):

                [Root: no items]
                upper_bound = 100 (fractional)
                /            \
        Include A        Exclude A
        [A]:5kg,$50      []:0kg,$0
        bound:80          bound:75
         /        \       /      \
      Include B  Exclude B  Include B  Exclude B
      [A,B]:11   [A]:5,$50  [B]:6,$60   []:0
      INFEASIBLE  bound:60   bound:73
                  prune(60<80)  /    \
              Include C  Exclude C
              [B,C]:8    [B]:6,$60
              complete    bound:75
              $75 ✓       (leaf)

Best found: [B,C] with value $75

Pruning saves exploring many nodes!
```

---

## 📋 DAY 3 SUMMARY

```
┌──────────────────────────────────────────────────────┐
│           DAY 3: KEY TAKEAWAYS                       │
│                                                      │
│  ✓ Branch & Bound = optimization via search + bounds │
│  ✓ Branch: explore subproblems systematically        │
│  ✓ Bound: compute upper/lower bound for each node   │
│  ✓ Prune: if bound can't beat best, skip subtree     │
│                                                      │
│  ✓ Upper bound (maximization):                       │
│    Relaxed problem, can't be beaten                  │
│  ✓ Lower bound (maximization):                       │
│    Feasible solution, known to be achievable         │
│                                                      │
│  ✓ Best-first search: explore promising nodes first  │
│  ✓ Often finds good solution early                   │
│  ✓ Enables aggressive pruning                        │
│                                                      │
│  ✓ Key applications: TSP, 0/1 knapsack, assignment  │
└──────────────────────────────────────────────────────┘
```

---

# 📅 DAY 4: AMORTIZED ANALYSIS
## 5 Hours | Analyzing Cost Over Operations

---

## 🎓 PART 1: AMORTIZED COMPLEXITY CONCEPT (90 minutes)

### What is Amortized Analysis?

```
┌──────────────────────────────────────────────────────┐
│         AMORTIZED ANALYSIS CONCEPT                   │
│                                                      │
│  Question: What's the typical cost of an operation? │
│           Not worst case for single operation        │
│           But average over sequence of operations    │
│                                                      │
│  Definition: Amortized cost = total cost / # ops    │
│                                                      │
│  Use case: Some ops expensive, many ops cheap      │
│           Single op might cost O(n)                  │
│           But over k ops, amortized might be O(1)   │
│                                                      │
│  Example: Dynamic array append                       │
│  • Array size: 1                                     │
│  • Append (size fits): O(1) – 100 appends           │
│  • Append (array full): O(n) – must resize          │
│                                                      │
│  Question: Average cost per append?                  │
│  Answer: Via amortized analysis!                     │
│                                                      │
│  Key insight:                                        │
│  Expensive operations (resize) happen rarely         │
│  Cheap operations (normal append) happen frequently  │
│  So average is heavily weighted toward cheap!        │
└──────────────────────────────────────────────────────┘
```

### Why Amortized Analysis Matters

```
┌──────────────────────────────────────────────────────┐
│    WHY NOT JUST WORST-CASE ANALYSIS?                │
│                                                      │
│  Worst-case per operation:                           │
│  • Dynamic array append: O(n)                        │
│  • Binary search tree insert: O(n)                   │
│  • Splay tree operation: O(n)                        │
│  • Sounds terrible!                                  │
│                                                      │
│  But in practice:                                    │
│  • Most operations are fast!                         │
│  • Slow operations are rare                          │
│  • Amortized cost is much better                     │
│                                                      │
│  Example: Dynamic array                              │
│  • 1000 appends total                                │
│  • 999 at O(1)                                       │
│  • 1 resize at O(1000)                               │
│  • Total: 999 + 1000 = 1999 ≈ O(1000)               │
│  • Average: 1999/1000 ≈ O(2) ≈ O(1) ✓               │
│                                                      │
│  Conclusion: Worst-case misleads!                    │
│             Amortized gives realistic picture        │
└──────────────────────────────────────────────────────┘
```

---

## 🎓 PART 2: AGGREGATE ANALYSIS METHOD (60 minutes)

### Aggregate Analysis Approach

```
┌──────────────────────────────────────────────────────┐
│        AGGREGATE ANALYSIS METHOD                     │
│                                                      │
│  Technique: Calculate TOTAL cost for n operations    │
│            Divide by n for amortized cost            │
│                                                      │
│  Steps:                                              │
│  1. Analyze worst-case sequence of n operations      │
│  2. Compute total cost: T(n)                         │
│  3. Amortized cost = T(n) / n                        │
│                                                      │
│  Formula:                                            │
│  amortized_cost = total_cost_for_n_ops / n           │
│                                                      │
│  Example: Dynamic Array Doubling                     │
│  ──────────────────────────────                     │
│                                                      │
│  Size progression: 1 → 2 → 4 → 8 → 16 → ...         │
│                                                      │
│  Append operations:                                  │
│  • Elements 1-1: no resize (1 cheap op)              │
│  • Element 2: resize (2→4, costs 2)                  │
│  • Elements 3-4: no resize (2 cheap)                 │
│  • Element 5: resize (4→8, costs 4)                  │
│  • Elements 6-8: no resize (3 cheap)                 │
│  • Element 9: resize (8→16, costs 8)                 │
│                                                      │
│  Total cost for n appends:                           │
│  • Resizes happen at: 1, 2, 4, 8, ...                │
│  • Total resize cost: 1+2+4+...+2^k ≈ 2*n           │
│  • Total append ops: n                               │
│  • Total cost: n (appends) + 2n (resizes) = 3n       │
│                                                      │
│  Amortized: 3n/n = O(3) = O(1) per operation        │
└──────────────────────────────────────────────────────┘
```

### Aggregate Analysis Visualization

```
Operation costs over time:

Cost
  |
  |                    ┌─────┐
  |                    │ 8   │
  |        ┌───┐       │     │
  |        │ 4 │       │     │
  |    ┌─┐ │   │   ┌─┐ │     │
  |    │2│ │   │   │2│ │     │
  |  1 │ │ │   │ 1 │ │ │     │ (resizes)
  | ┌──┴─┴─┴───┴───┴─┴─┴─────┴──── (cheap appends)
  |
Total cost: 1 + (2+2+2+2) + (4+4+4+4) + (8+8+...+8) + cheap_ops
         = sum of resizes + sum of cheap ops
         ≈ 2n for total

So: amortized = 2n/n = O(1) per append
```

---

## 🎓 PART 3: ACCOUNTING AND POTENTIAL METHODS (90 minutes)

### Accounting Method

```
┌──────────────────────────────────────────────────────┐
│          ACCOUNTING METHOD                           │
│                                                      │
│  Idea: "Bank account" for future costs               │
│                                                      │
│  Setup:                                              │
│  • Assign amortized cost to each operation           │
│  • Some ops are "cheap" (use amortized cost)         │
│  • Some ops are "expensive" (also use amortized)     │
│  • When expensive op occurs:                         │
│    pay actual cost from account                      │
│  • If account never negative: amortized cost valid! │
│                                                      │
│  Dynamic Array Example:                              │
│  ────────────────────────                           │
│  Amortized cost per append: 3 units                  │
│                                                      │
│  • Cheap append: costs 1 unit actual                 │
│    Pay 3: contribute 2 to account                    │
│  • Resize: costs i units actual (i = old size)      │
│    Pay 3: need 3 ≥ i? No! But account has balance   │
│    Accumulated balance from previous appends         │
│    Pay from balance                                  │
│                                                      │
│  Detailed trace (capacity starts at 1):              │
│  ──────────────────────────────────────             │
│                                                      │
│  Op 1 (append, size→2): actual=2, pay=3, balance=1 │
│  Op 2 (append): actual=1, pay=3, balance=1+3-1=3   │
│  Op 3 (resize, 2→4): actual=2, pay=3, balance=3+3-2│
│  Op 4 (append): actual=1, pay=3, balance=3+3-1=5   │
│  Op 5 (append): actual=1, pay=3, balance=5+3-1=7   │
│  ...                                                 │
│                                                      │
│  Balance never goes negative! ✓                      │
│  So amortized cost of 3 per operation is valid!     │
└──────────────────────────────────────────────────────┘
```

### Potential Method

```
┌──────────────────────────────────────────────────────┐
│          POTENTIAL METHOD                            │
│                                                      │
│  Idea: Define "potential" function on data structure │
│        Measure "disorder" or "unfinished work"        │
│                                                      │
│  Formula:                                            │
│  amortized_cost_i = actual_cost_i +                 │
│                     Δ_potential                       │
│                                                      │
│  Where Δ_potential = potential_after - potential_before│
│                                                      │
│  Proof technique:                                    │
│  1. Define potential function Φ                      │
│  2. Show Φ(initial) = 0                              │
│  3. Show Φ(final) ≥ 0                                │
│  4. Sum amortized costs:                             │
│     Σ amortized = Σ actual + Σ Δ_potential          │
│                 = Σ actual + (Φ_final - Φ_initial)  │
│                 = Σ actual + (Φ_final - 0)          │
│                 ≥ Σ actual                           │
│  5. So amortized upper bounds actual                 │
│                                                      │
│  Example: Dynamic Array                              │
│  ─────────────────────────────────                  │
│  Potential = number_of_filled_spaces                 │
│  (excess capacity relative to "ideal")               │
│                                                      │
│  φ = num_elements - (current_capacity/2)             │
│                                                      │
│  Normal append (no resize):                          │
│  • actual_cost = 1 (copy element)                    │
│  • φ_before = n - (2n/2) = 0                         │
│  • φ_after = (n+1) - (2n/2) = 1                      │
│  • Δφ = 1                                             │
│  • amortized = 1 + 1 = 2                             │
│                                                      │
│  Resize (when full):                                 │
│  • actual_cost = n (copy all elements)               │
│  • φ_before = n - (n/2) = n/2                        │
│  • φ_after = n - (2n/2) = 0 (capacity doubles!)     │
│  • Δφ = 0 - n/2 = -n/2                               │
│  • amortized = n - n/2 = n/2                         │
│                                                      │
│  For n+1 operations: total ≈ 2n, amortized = 2      │
│  (similar to aggregate analysis)                     │
└──────────────────────────────────────────────────────┘
```

---

## 🎓 PART 4: ADVANCED AMORTIZED ANALYSIS (60 minutes)

### Splay Trees: Complex Amortized Analysis

```
┌──────────────────────────────────────────────────────┐
│   SPLAY TREES: O(log n) AMORTIZED OPERATIONS        │
│                                                      │
│  What are splay trees?                               │
│  • Binary search trees that reorganize on access    │
│  • Move accessed element to root (splay)             │
│  • Single operation: O(n) worst case                 │
│  • But: O(log n) amortized per operation!            │
│                                                      │
│  Why amortized O(log n)?                             │
│  • Potential function: Σ log(subtree_size)           │
│  • When splay: reorganize tree structure             │
│  • Zigzag rotations decrease potential               │
│  • Amortized cost bounded despite expensive rotations│
│                                                      │
│  Key insight:                                        │
│  • Expensive operations improve tree balance        │
│  • Future operations benefit from improved balance  │
│  • Cost spread across sequence of operations        │
│  • Amortized analysis captures this!                 │
└──────────────────────────────────────────────────────┘
```

### Fibonacci Heaps: Amortized Operations

```
┌──────────────────────────────────────────────────────┐
│   FIBONACCI HEAPS: AMORTIZED OPERATIONS              │
│                                                      │
│  Fibonacci heaps achieve:                            │
│  • Extract-min: O(log n) amortized                   │
│  • Insert: O(1) amortized                            │
│  • Decrease-key: O(1) amortized                      │
│  • Delete: O(log n) amortized                        │
│                                                      │
│  How?                                                │
│  • Lazy merging: delay consolidation                 │
│  • Cascading cuts: mark nodes, cut when needed       │
│  • Potential function captures "work owed"           │
│  • Amortized analysis proves efficiency              │
│                                                      │
│  Without amortized analysis:                         │
│  • Worst-case operations look terrible!              │
│  • Insert seems cheap but cascading cuts cost O(n)  │
│  • Delete seems expensive                            │
│                                                      │
│  With amortized analysis:                            │
│  • Operations are O(1) or O(log n)                   │
│  • Used in Dijkstra's, Prim's for MST               │
│  • Among the most efficient heaps                    │
└──────────────────────────────────────────────────────┘
```

---

## 📋 DAY 4 SUMMARY

```
┌──────────────────────────────────────────────────────┐
│           DAY 4: KEY TAKEAWAYS                       │
│                                                      │
│  ✓ Amortized analysis = average cost per operation   │
│  ✓ Over sequence of operations (not single op)       │
│  ✓ Some ops expensive, many cheap                    │
│                                                      │
│  ✓ THREE METHODS:                                    │
│    1. Aggregate: T(n)/n = amortized cost             │
│    2. Accounting: bank account model                 │
│    3. Potential: define Φ, track changes             │
│                                                      │
│  ✓ Dynamic array: O(1) amortized per append          │
│  ✓ Splay trees: O(log n) amortized                   │
│  ✓ Fibonacci heaps: O(1) insert, O(log n) delete     │
│                                                      │
│  ✓ Key insight: Expensive ops improve future ops    │
│  ✓ Cost naturally spreads across sequence            │
│  ✓ Amortized analysis captures this reality!         │
└──────────────────────────────────────────────────────┘
```

---

# 📅 DAY 5: INTEGRATION & MIXED PARADIGMS
## 5 Hours | Combining Techniques and Synthesis

---

## 🎓 PART 1: PARADIGM SELECTION GUIDE (90 minutes)

### Decision Tree: Which Paradigm?

```
┌────────────────────────────────────────────────────────┐
│       ALGORITHM PARADIGM SELECTION GUIDE               │
│                                                        │
│  Question 1: Can we make greedy choice?                │
│  └─→ YES: Try greedy                                   │
│      └─ Provable choice property? → GREEDY WORKS      │
│      └─ No proof? → Try DP or backtracking             │
│  └─→ NO: Continue to Q2                                │
│                                                        │
│  Question 2: Does problem have optimal substructure?   │
│  └─→ YES: Consider DP                                  │
│      └─ Overlapping subproblems? → DP OPTIMAL         │
│      └─ No overlap? → Maybe greedy/divide-conquer     │
│  └─→ NO: Continue to Q3                                │
│                                                        │
│  Question 3: Need ALL solutions (or find ALL)?         │
│  └─→ YES: Consider backtracking                        │
│      └─ Constraints high? → Backtrack (with pruning)  │
│      └─ Constraints low? → Brute force enumeration    │
│  └─→ NO: Continue to Q4                                │
│                                                        │
│  Question 4: Need OPTIMAL solution (minimize/maximize)│
│  └─→ YES: Consider B&B                                │
│      └─ Can compute bounds? → Branch & Bound         │
│      └─ No bounds? → Backtracking (find all, pick best)│
│  └─→ NO: Continue to Q5                                │
│                                                        │
│  Question 5: Problem breaks into independent parts?    │
│  └─→ YES: Consider divide-conquer                      │
│      └─ Can solve parts independently? → DC WORKS    │
│  └─→ NO: Problem solved! Use heuristic/approximation  │
└────────────────────────────────────────────────────────┘
```

### Problem Type Classification

```
┌──────────────────────────────────────────────────────┐
│     PROBLEM CLASSIFICATION & ALGORITHM CHOICE        │
│                                                      │
│  Search & Enumeration Problems:                      │
│  • All permutations/combinations                     │
│  • All valid configurations (N-queens)               │
│  → Backtracking with good pruning                    │
│                                                      │
│  Optimization Problems (one solution):               │
│  • Shortest path                                     │
│  • Minimum spanning tree                             │
│  • Knapsack variants                                 │
│  → Try greedy first                                  │
│  → If fails: DP or B&B                               │
│                                                      │
│  Path-Finding Problems:                              │
│  • Sudoku solver                                     │
│  • Maze solving                                      │
│  • Constraint satisfaction                          │
│  → Backtracking with constraint pruning              │
│                                                      │
│  Combinatorial Optimization:                         │
│  • TSP (find best tour)                              │
│  • Weighted knapsack                                 │
│  • Job scheduling with weights                       │
│  → Dynamic programming or B&B                        │
│                                                      │
│  Interval/Sequence Problems:                         │
│  • Activity selection                                │
│  • LCS, LIS                                          │
│  • Huffman coding                                    │
│  → Greedy (if structure permits) or DP              │
└──────────────────────────────────────────────────────┘
```

---

## 🎓 PART 2: COMBINING PARADIGMS (90 minutes)

### Greedy + Backtracking: Intelligent Search

```
┌──────────────────────────────────────────────────────┐
│   COMBINING GREEDY + BACKTRACKING                    │
│                                                      │
│  Use case: Backtracking but want to try promising    │
│           branches first                             │
│                                                      │
│  Strategy:                                           │
│  1. At each choice point: order options by greedy    │
│     heuristic (most promising first)                 │
│  2. Try options in that order (backtrack if needed)  │
│  3. Often finds solution much faster                 │
│                                                      │
│  Example: Sudoku solving                             │
│  • Backtracking: try each digit 1-9 randomly        │
│  • Smart: try digits that appear least in row/col   │
│  • Greedy heuristic + backtrack = faster             │
│                                                      │
│  Example: N-Queens                                   │
│  • Backtracking: try each column 0-n                │
│  • Smart: prioritize columns with fewer conflicts   │
│  • Result: find solution faster                      │
│                                                      │
│  Key insight:                                        │
│  • Greedy chooses order of exploration               │
│  • Backtracking ensures correctness (can retry)      │
│  • Together: fast AND complete                       │
└──────────────────────────────────────────────────────┘
```

### Dynamic Programming + Greedy: Hybrid Optimization

```
┌──────────────────────────────────────────────────────┐
│   COMBINING DP + GREEDY                              │
│                                                      │
│  Use case: Top-level greedy, subproblems DP          │
│                                                      │
│  Strategy:                                           │
│  1. Make greedy choice at highest level              │
│  2. Subproblem might not have greedy structure       │
│  3. Solve subproblem via DP                          │
│  4. Combine: greedy_choice + DP_solution             │
│                                                      │
│  Example: Weighted activity selection                │
│  • Greedy by finish time: doesn't work (weighted)    │
│  • Top-level greedy: decide to include activity i    │
│  • Subproblem DP: max value in activities before i   │
│  • Result: optimal weighted solution                 │
│                                                      │
│  Example: Interval scheduling with costs            │
│  • Greedy: select maximum profit non-overlapping     │
│  • For each selection: DP on remaining intervals     │
│  • Together: optimal sequence with profit            │
│                                                      │
│  Key insight:                                        │
│  • Greedy at top level for efficiency                │
│  • DP for subproblem complexity                      │
│  • Often beats pure DP (faster)                      │
└──────────────────────────────────────────────────────┘
```

### Branch & Bound + Greedy Heuristics

```
┌──────────────────────────────────────────────────────┐
│   COMBINING B&B + GREEDY HEURISTICS                  │
│                                                      │
│  Use case: B&B needs upper/lower bounds              │
│           Greedy provides quick heuristic bounds     │
│                                                      │
│  Strategy:                                           │
│  1. Use greedy to find quick initial solution        │
│  2. Use greedy value as lower/upper bound            │
│  3. B&B prunes more aggressively with tighter bounds │
│  4. B&B finds optimal (by exploring remaining)       │
│                                                      │
│  Example: TSP with B&B                               │
│  • Greedy nearest-neighbor: finds decent tour       │
│  • Use greedy_cost as upper bound                    │
│  • B&B: only explore if bound > greedy_cost         │
│  • Result: optimal TSP tour found faster             │
│                                                      │
│  Example: 0/1 Knapsack                               │
│  • Greedy fractional solution: upper bound           │
│  • Start B&B: initial lower bound = 0                │
│  • As B&B progresses, tighten both bounds            │
│  • Converges to optimal                              │
│                                                      │
│  Key insight:                                        │
│  • Greedy provides quick initial bounds              │
│  • B&B refines to optimality                         │
│  • Hybrid: fast + guaranteed optimal                 │
└──────────────────────────────────────────────────────┘
```

### Divide & Conquer + Dynamic Programming

```
┌──────────────────────────────────────────────────────┐
│   COMBINING DIVIDE-CONQUER + DP                      │
│                                                      │
│  Use case: D&C structure exists but subproblems      │
│           overlap (classic DP indicator)             │
│                                                      │
│  Strategy:                                           │
│  1. Recognize D&C structure                          │
│  2. But subproblems computed multiple times          │
│  3. Memoize subproblem solutions                     │
│  4. Use DP (or memoization) with D&C structure       │
│                                                      │
│  Example: Fibonacci                                  │
│  • D&C: F(n) = F(n-1) + F(n-2)                      │
│  • Subproblems overlap (F(n-2) computed many times)  │
│  • DP/Memoization: store computed values             │
│  • Result: O(n) instead of O(2^n)                    │
│                                                      │
│  Example: Quicksort + DP for selection               │
│  • D&C: partition + recurse on parts                 │
│  • DP: remember partition info for future queries    │
│  • Result: faster repeated queries                   │
│                                                      │
│  Key insight:                                        │
│  • D&C structure = independent subproblems           │
│  • But if overlap: add DP on top                     │
│  • Hybrid: clean structure + efficiency              │
└──────────────────────────────────────────────────────┘
```

---

## 🎓 PART 3: PROBLEM-SOLVING METHODOLOGY (90 minutes)

### Approaching an Unknown Problem

```
┌──────────────────────────────────────────────────────┐
│   PROBLEM-SOLVING METHODOLOGY: STEPS                 │
│                                                      │
│  Step 1: UNDERSTAND THE PROBLEM                      │
│  ├─ Read carefully                                   │
│  ├─ Identify: input, output, constraints             │
│  ├─ Do you want: one solution, all solutions,        │
│  │  best solution?                                   │
│  └─ Examples: work through by hand                   │
│                                                      │
│  Step 2: IDENTIFY PROBLEM STRUCTURE                  │
│  ├─ Is there a greedy choice?                        │
│  ├─ Are there optimal substructure indicators?       │
│  ├─ Do subproblems overlap?                          │
│  ├─ Is it a search problem (permutations, configs)?  │
│  └─ Is it an optimization (min/max)?                 │
│                                                      │
│  Step 3: TRY GREEDY FIRST                            │
│  ├─ Identify natural greedy choice                   │
│  ├─ Try it on examples                               │
│  ├─ Does it work?                                    │
│  ├─ If YES: Try to prove (exchange arg, etc)        │
│  └─ If NO: Next approach                             │
│                                                      │
│  Step 4: IF GREEDY FAILS, TRY DP                     │
│  ├─ Define optimal substructure                      │
│  ├─ Write recurrence relation                        │
│  ├─ Identify overlapping subproblems                 │
│  ├─ Implement (top-down memoization or bottom-up)   │
│  └─ Check complexity and space                       │
│                                                      │
│  Step 5: IF DP OVERKILL, TRY BACKTRACKING            │
│  ├─ Do you need to explore many possibilities?       │
│  ├─ Can you prune effectively?                       │
│  ├─ Design pruning strategy                          │
│  └─ Implement with recursion                         │
│                                                      │
│  Step 6: IF OPTIMIZATION CRITICAL, TRY B&B           │
│  ├─ Can you compute bounds?                          │
│  ├─ Do bounds improve as you search?                 │
│  ├─ Implement best-first search                      │
│  └─ Use greedy for initial bounds if needed          │
│                                                      │
│  Step 7: REVIEW & OPTIMIZE                           │
│  ├─ Check time complexity: acceptable?               │
│  ├─ Check space: acceptable?                         │
│  ├─ Consider hybrid approaches                       │
│  └─ Test edge cases                                  │
└──────────────────────────────────────────────────────┘
```

### Examples of Paradigm Choices

```
┌──────────────────────────────────────────────────────┐
│   EXAMPLES: WHICH PARADIGM FOR CLASSIC PROBLEMS?     │
│                                                      │
│  Activity Selection (unweighted):                    │
│  └─ Greedy (sort finish time) ✓                      │
│                                                      │
│  Activity Selection (weighted):                      │
│  └─ DP (optimal substructure) ✓                      │
│                                                      │
│  N-Queens:                                           │
│  └─ Backtracking (place one/row, check diagonals) ✓  │
│                                                      │
│  Sudoku:                                             │
│  └─ Backtracking + constraint checking ✓             │
│                                                      │
│  TSP:                                                │
│  └─ B&B (if small), DP (if medium), heuristic (large)│
│                                                      │
│  0/1 Knapsack:                                       │
│  └─ DP (no greedy choice property) ✓                 │
│                                                      │
│  Fractional Knapsack:                                │
│  └─ Greedy (by value/weight ratio) ✓                 │
│                                                      │
│  Huffman Coding:                                     │
│  └─ Greedy (combine smallest frequencies) ✓          │
│                                                      │
│  LCS (Longest Common Subsequence):                   │
│  └─ DP (optimal substructure, overlaps) ✓            │
│                                                      │
│  Graph Coloring:                                     │
│  └─ Backtracking (try colors, backtrack) ✓           │
│                                                      │
│  MST:                                                │
│  └─ Greedy (Kruskal, Prim) ✓                         │
│                                                      │
│  Shortest Path:                                      │
│  └─ Greedy (Dijkstra) or DP (Bellman-Ford) ✓         │
└──────────────────────────────────────────────────────┘
```

---

## 📋 DAY 5 & WEEK 13 SUMMARY

```
┌──────────────────────────────────────────────────────┐
│     WEEK 13: COMPLETE SUMMARY                        │
│     BACKTRACKING & BRANCH & BOUND MASTERY            │
└──────────────────────────────────────────────────────┘

DAY 1: BACKTRACKING FUNDAMENTALS
✓ Backtracking = incremental solution building
✓ State space tree represents all possibilities
✓ Pruning avoids exploring dead-end branches
✓ Three pruning strategies: constraint, bound, monotonic
✓ Key: Backtrack and UNDO choices

DAY 2: BACKTRACKING APPLICATIONS
✓ N-Queens: place queen per row, check diagonals
✓ Sudoku: find empty cell, try digits with constraints
✓ Permutations: track used elements
✓ Combinations: only consider elements ≥ last picked
✓ Word Search: DFS path finding in grid

DAY 3: BRANCH & BOUND
✓ B&B = optimization via search + bounds
✓ Branch: explore subproblems systematically
✓ Bound: compute upper/lower bound per node
✓ Prune: if bound can't beat best, skip subtree
✓ Best-first search: explore promising nodes first

DAY 4: AMORTIZED ANALYSIS
✓ Amortized = average cost over sequence of operations
✓ Some ops expensive, many cheap
✓ Three methods: aggregate, accounting, potential
✓ Dynamic arrays: O(1) amortized append
✓ Splay trees: O(log n) amortized per operation

DAY 5: PARADIGM INTEGRATION
✓ Decision tree: greedy → DP → backtrack → B&B
✓ Hybrid approaches: greedy + backtrack, DP + greedy, B&B + greedy
✓ Problem-solving methodology: understand → structure → try approaches
✓ Examples: know classic problems and best paradigms
✓ Choose based on problem properties

KEY INSIGHTS:
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

1. BACKTRACKING IS SYSTEMATIC SEARCH
   • Explores all possibilities intelligently
   • Prunes dead ends early
   • Guarantees finding all solutions
   • BUT: exponential worst case without good pruning

2. BRANCH & BOUND IS OPTIMIZATION SEARCH
   • Like backtracking but for optimization
   • Uses bounds to prune sub-optimal branches
   • Finds optimal solution faster than brute force
   • Critical for TSP, knapsack, assignment problems

3. AMORTIZED ANALYSIS EXPLAINS PERFORMANCE
   • Single expensive operation is misleading
   • Sequence of operations often cheap on average
   • Dynamic arrays, splay trees, Fibonacci heaps
   • Essential for modern data structures

4. PARADIGMS ARE TOOLS, NOT ALGORITHMS
   • Greedy: fast, requires proof of correctness
   • DP: powerful, requires optimal substructure
   • Backtrack: complete, explores all options
   • B&B: optimized backtrack, needs bounds
   • Choose based on problem structure!

5. HYBRIDS ARE OFTEN BEST
   • Greedy ordering for backtracking
   • DP for subproblems in B&B
   • Greedy heuristics for B&B bounds
   • Combine tools wisely!

MASTERY CHECKLIST:
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

□ Understand backtracking completely
□ Can solve N-Queens and variants
□ Can solve Sudoku systematically
□ Understand pruning strategies
□ Know state space tree concept
□ Understand branch & bound completely
□ Can compute bounds (upper/lower)
□ Know best-first search strategy
□ Master amortized analysis (all 3 methods)
□ Can choose right paradigm for problem
□ Can combine paradigms effectively
□ Know classic problems and best algorithms

PROGRESSION OUTCOME:
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

✓ Phase D (Weeks 12-13): Algorithm Paradigms Complete
✓ Mastered 4 major design approaches:
  • Greedy (fast, requires proof)
  • Dynamic Programming (powerful, overlaps)
  • Backtracking (complete search, pruned)
  • Branch & Bound (optimized search, bounds)
✓ Can recognize problem structure
✓ Can select appropriate algorithm
✓ Can combine approaches effectively
✓ Ready for advanced topics or real-world problems!

NEXT PATHS:
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

→ Graph Algorithms: DFS, BFS, topological sort
→ Shortest Path: Dijkstra, Bellman-Ford, Floyd-Warshall
→ NP-Completeness: understanding limits
→ Approximation Algorithms: dealing with hard problems
→ Interview Preparation: applying paradigms to coding problems
```

---

# 📊 WEEK 13 LEARNING PROGRESSION

```
┌──────────────────────────────────────────────────────┐
│         DIFFICULTY PROGRESSION BY DAY                │
│                                                      │
│ Day 1: 🟢 GREEN (Fundamentals)                       │
│ ├─ Backtracking concept: intuitive vs brute force   │
│ ├─ State space trees: visual, concrete              │
│ └─ Pruning strategies: pattern recognition          │
│                                                      │
│ Day 2: 🟡 YELLOW (Application)                       │
│ ├─ N-Queens: diagonal conflict checking             │
│ ├─ Sudoku: constraint satisfaction                  │
│ └─ Permutations/Combinations: algorithmic variations│
│                                                      │
│ Day 3: 🟠 ORANGE (Optimization)                      │
│ ├─ Branch & Bound: systematic optimization          │
│ ├─ Bounds: upper/lower computation                  │
│ └─ Best-first search: priority-based exploration    │
│                                                      │
│ Day 4: 🟠 ORANGE (Analysis)                          │
│ ├─ Amortized analysis: sequence perspective         │
│ ├─ Three methods: aggregate, accounting, potential  │
│ └─ Complex examples: splay trees, Fibonacci heaps   │
│                                                      │
│ Day 5: 🔴 RED (Integration & Meta-Learning)          │
│ ├─ Paradigm selection: decision tree                │
│ ├─ Hybrid approaches: combining techniques          │
│ ├─ Problem-solving methodology: systematic approach │
│ └─ Synthesis: knowing when/where to apply each tool │
│                                                      │
│ MASTERY OUTCOME:                                     │
│ ✓ Complete mastery of search & optimization         │
│ ✓ Can solve hard combinatorial problems             │
│ ✓ Understand performance analysis deeply            │
│ ✓ Know algorithmic paradigms completely             │
│ ✓ Ready for advanced algorithms or interviews!      │
└──────────────────────────────────────────────────────┘
```

---

## 📚 RECOMMENDED STUDY MATERIALS

### Concept Mastery
- Trace through backtracking examples on paper
- Draw state space trees completely
- Implement N-Queens and Sudoku solver
- Understand pruning at visceral level

### B&B Deep Dive
- Compute bounds by hand for small examples
- Visualize search tree with best-first ordering
- See how bounds prune aggressively
- Practice on 0/1 knapsack by hand

### Amortized Analysis
- Work through accounting method step-by-step
- Define potential functions and verify
- Trace dynamic array operations with potential
- Practice aggregation calculations

### Integration Practice
- Solve mix of problems using different paradigms
- Classify unknown problems correctly
- Design hybrid approaches
- Reason about which paradigm is best

### Common Pitfalls to Avoid
- ❌ Don't assume backtracking is slow (with pruning, it's fast!)
- ❌ Don't forget to undo choices (critical for correctness!)
- ❌ Don't compute wrong bounds (bounds must be valid!)
- ❌ Don't confuse worst-case with amortized (very different!)
- ❌ Don't use wrong paradigm (know the structure first!)

---

**Week 13 Complete! Weeks 12-13: Algorithm Paradigms Complete!**

**You now have complete mastery of:**
- ✓ Greedy algorithms & proofs
- ✓ Backtracking & search
- ✓ Branch & bound optimization
- ✓ Amortized analysis
- ✓ Paradigm selection & combination

**Ready for interview prep, real-world problems, or advanced algorithms!**
