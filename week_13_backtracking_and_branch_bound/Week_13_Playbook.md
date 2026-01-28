# 🔵 WEEK 13: BACKTRACKING & BRANCH & BOUND
## Complete Course Playbook | No-Code Concept Mastery
**Duration:** 25 hours | **Focus:** Search Paradigm & Optimization

---

## 📚 WEEK OVERVIEW

### What You'll Master
```
✓ Backtracking Algorithm Structure & Template
✓ State Space Trees & Pruning Strategies
✓ Permutations, Combinations, Subsets
✓ Complex Backtracking (N-Queens, Sudoku)
✓ Branch & Bound Optimization Framework
✓ Best-First Search & Bounding Techniques
✓ Amortized Analysis (3 Methods)
✓ Dynamic Array & Self-Adjusting Structures
✓ Combining Multiple Paradigms
```

### Weekly Learning Path
```
DAY 1 (90 min) → Backtracking Fundamentals
DAY 2 (90 min) → Complex Backtracking Problems
DAY 3 (90 min) → Branch & Bound Optimization
DAY 4 (90 min) → Amortized Analysis (3 Methods)
DAY 5 (90 min) → Integration & Mixed Paradigms
```

---

# 📅 DAY 1: BACKTRACKING FUNDAMENTALS

## 🎯 Day Objective
Master backtracking algorithm structure, state space trees, and systematic exploration with pruning.

### ⏱️ Time Allocation
- Backtracking Concept: 25 minutes
- State Space Trees: 25 minutes
- Pruning Strategies: 20 minutes
- Template & Examples: 20 minutes

---

## 🔍 PART 1: WHAT IS BACKTRACKING?

### Core Definition

```
BACKTRACKING ALGORITHM
═════════════════════════════════════════════════════

Definition: Backtracking is a general-purpose algorithm technique for
            finding solutions to problems that satisfy constraints

Key Principle: Build solution incrementally, one piece at a time
              - If current partial solution CAN'T lead to valid solution
              - UNDO last choice (backtrack)
              - TRY next alternative

Paradigm: Depth-First Search (DFS) on solution tree
         - Explore deeply into partial solutions
         - When stuck, backtrack to try alternatives
         - Systematic exploration with pruning

ALGORITHM STRUCTURE:
═════════════════════════════════════════════════════

1. STATE REPRESENTATION
   └─ Current partial solution
   └─ What's been decided
   └─ What's still to decide

2. CHOICE POINT
   └─ At each step: what are next options?
   └─ What decisions can we make?
   └─ Different choices = different branches

3. CONSTRAINT CHECK
   └─ Is this choice valid?
   └─ Doesn't violate constraints?
   └─ Could lead to solution?

4. RECURSIVE EXPLORATION
   └─ If valid: recurse with new state
   └─ If recursion finds solution: return
   └─ If not: try next choice

5. BACKTRACK
   └─ If no choice works: undo last decision
   └─ Return to previous state
   └─ Try alternative

WHY "BACKTRACKING"?
  - Move forward: make choice, recurse
  - Get stuck: no valid choices
  - Move backward: undo last choice (backtrack)
  - Try next: different choice
```

### Comparison: Backtracking vs Brute Force

```
BRUTE FORCE:
  └─ Generate ALL possible solutions
  └─ Check each one to see if valid
  └─ Return valid ones
  └─ Time: Worst case (exponential all)

BACKTRACKING:
  └─ Build solution incrementally
  └─ PRUNE when can't possibly be valid
  └─ Skip entire subtrees of invalid solutions
  └─ Time: Better than brute force (pruning helps)

EXAMPLE: Find all permutations of [1,2,3] valid under constraints

Brute force:
  Generate all sequences: [1,2,3], [1,3,2], [2,1,3], [2,3,1], [3,1,2], [3,2,1]
  6 sequences, check each

Backtracking:
  Build one at a time:
    Place 1: [1, ?, ?]
      Place 2: [1, 2, ?]
        Place 3: [1, 2, 3] ← Valid? Check
      Place 3: [1, 3, ?]
        Place 2: [1, 3, 2] ← Valid? Check
    Place 2: [2, ?, ?]
      Similar...
    Place 3: [3, ?, ?]
      Similar...
  
  If at some point: [X, Y, ?] violates constraints
  Skip all completions of [X, Y, ?]
  Huge savings if many pruned
```

### When to Use Backtracking

```
IDEAL PROBLEMS FOR BACKTRACKING
═════════════════════════════════════════════════════

CHARACTERISTICS:
  ✓ Need to find solutions among large search space
  ✓ Solutions satisfy specific constraints
  ✓ Partial solutions can be pruned
  ✓ Solutions can be built incrementally
  ✓ Problem has optimal substructure for partial solutions

EXAMPLES:
  ✓ Permutations/Combinations: all valid orderings
  ✓ N-Queens: place n queens, no attacks
  ✓ Sudoku: fill grid with constraints
  ✓ Word search: find word in grid
  ✓ Maze solving: find path from start to end
  ✓ Graph coloring: color with minimum colors
  ✓ Subset sum: find subset summing to target
  ✓ Traveling salesman: find shortest tour
  ✓ Knapsack variants: select items under constraint
  ✓ Constraint satisfaction: general framework

NOT IDEAL:
  ✗ Optimization without clear constraints
  ✗ No partial solution pruning possible
  ✗ All solutions needed to find best
```

---

## 🔍 PART 2: STATE SPACE TREES

### Understanding State Space Tree

```
STATE SPACE TREE CONCEPT
═════════════════════════════════════════════════════

Definition: Tree representing all possible states during search
           - Nodes = partial solutions
           - Edges = choices/decisions
           - Path from root to node = current state
           - Leaves = complete solutions (valid or invalid)

STRUCTURE:
  Root: Initial (empty) state
  │
  ├─ Branch 1: First choice = "A"
  │  ├─ Branch 1.1: Second choice = "1"
  │  │  ├─ Leaf: Complete solution [A,1]
  │  │  └─ Leaf: Complete solution [A,2]
  │  └─ Branch 1.2: Second choice = "2"
  │     └─ Leaf: Complete solution [A,3]
  │
  └─ Branch 2: First choice = "B"
     ├─ Leaf: Complete solution [B,1]
     └─ Leaf: Complete solution [B,2]

TREE TRAVERSAL:
  Backtracking = DFS traversal of this tree
  With pruning: skip branches that can't work
```

### Visualizing State Space

```
EXAMPLE: Generate all permutations of [1, 2, 3]
═════════════════════════════════════════════════════

                         ROOT: []
                    (no choices made yet)
                    /            |            \
                   /             |             \
              1st=1          1st=2          1st=3
               /              |              \
              [1]            [2]             [3]
             /  \           /  \            /  \
        2nd=2  2nd=3   2nd=1  2nd=3    2nd=1  2nd=2
         /      \       /      \        /       \
      [1,2]   [1,3]  [2,1]   [2,3]  [3,1]   [3,2]
       /        |      |       |      |       |
    3rd=3    3rd=2  3rd=3  3rd=1  3rd=2  3rd=1
     /         |      |       |      |       |
  [1,2,3]  [1,3,2]  [2,1,3] [2,3,1] [3,1,2] [3,2,1]
  VALID    VALID    VALID   VALID   VALID   VALID

EXECUTION TRACE (DFS):
1. [1] → [1,2] → [1,2,3] FOUND → backtrack
2. [1] → [1,2] no more 3rd choices → backtrack
3. [1] → [1,3] → [1,3,2] FOUND → backtrack
4. [1] → [1,3] no more 3rd choices → backtrack
5. [1] no more 2nd choices → backtrack
6. [2] → [2,1] → [2,1,3] FOUND → backtrack
... (continue for all branches)

TIME COMPLEXITY:
  n! nodes in tree (one per permutation)
  DFS visits each node: O(n!)
  Work per node: O(n) to check
  Total: O(n! × n)
```

### Depth vs Breadth in State Space

```
DEPTH-FIRST (Backtracking) vs BREADTH-FIRST
═════════════════════════════════════════════════════

DEPTH-FIRST (Backtracking):
  └─ Go deep into solution quickly
  └─ Find first solution fast
  └─ Space: O(depth) in recursion stack
  └─ Can use backtracking/pruning
  └─ Find one solution, then continue for others
  
  Execution order for [1,2,3] permutations:
    [1,2,3] → [1,3,2] → [2,1,3] → [2,3,1] → [3,1,2] → [3,2,1]

BREADTH-FIRST:
  └─ Explore all at current level
  └─ Find shortest solution first
  └─ Space: O(width) in queue
  └─ Can use bounding (if have limits)
  
  Execution order for [1,2,3] permutations:
    All [1,?,?] → All [2,?,?] → All [3,?,?]

FOR BACKTRACKING: Always DFS (naturally recursive)
FOR BRANCH & BOUND: Can use best-first (priority queue)
```

---

## 🔍 PART 3: PRUNING STRATEGIES

### What is Pruning?

```
PRUNING CONCEPT
═════════════════════════════════════════════════════

Pruning: Eliminating branches of search tree that CAN'T
         possibly lead to valid solutions

Impact: Skips exploring entire subtrees
       Reduces time exponentially
       Key to making backtracking practical

VISUALIZATION:

Full tree (no pruning):
                 ROOT
              /  |  |  \
            A   B   C   D
          / | \ | ... search continues

Pruned tree:
                 ROOT
              /  |     \
            A   B       (C pruned)
          / \         (no D, E subtrees)

Pruning decided at B, so all B's children pruned

PRUNING RULE:
  If at current state S:
    - Constraint will NEVER be satisfied
    - Or solution IMPOSSIBLE
    - Or condition is UNMET
  Then: PRUNE (don't explore S's children)
```

### Types of Pruning

```
PRUNING STRATEGIES
═════════════════════════════════════════════════════

TYPE 1: CONSTRAINT VIOLATION CHECK
────────────────────────────────────
  If current partial solution violates hard constraint:
    Prune immediately
  
  Example (N-Queens):
    Place queen at (row, col)
    Check: Does it attack existing queens?
    If YES: Prune (don't place more queens)
    If NO: Continue

TYPE 2: FEASIBILITY CHECK
──────────────────────────
  If remaining options CAN'T complete solution:
    Prune
  
  Example (Sudoku):
    Filled cell (row, col) = 5
    Check: In row, can we still place other numbers?
    If NO valid numbers left: Prune

TYPE 3: BOUNDING (OPTIMIZATION)
─────────────────────────────────
  If best possible from current state ≤ current best known:
    Prune
  
  Example (TSP):
    Partial tour cost so far = 50
    Best complete tour found = 60
    Lower bound for completing = 15
    If 50 + 15 ≥ 60: Prune (can't beat current best)

TYPE 4: SYMMETRY BREAKING
──────────────────────────
  If current state equivalent to already-explored state:
    Prune
  
  Example (N-Queens):
    Solution (Q1 at row 1, Q2 at row 3)
    Same as (Q1 at row 3, Q2 at row 1) due to symmetry
    Skip one of them

TYPE 5: EARLY TERMINATION
──────────────────────────
  If found valid solution and only need one:
    Stop immediately
  
  Example (Maze):
    Found path to exit: Return immediately
    Don't explore remaining paths
```

### Pruning Effectiveness

```
PRUNING IMPACT ON PERFORMANCE
═════════════════════════════════════════════════════

NO PRUNING:
  Permutations of 10 items: 10! = 3,628,800 nodes
  Time: ~seconds on modern computer

WITH SMART PRUNING:
  N-Queens 8: ~100,000 nodes without pruning
             ~~50,000 nodes with pruning (50%)
  
  Sudoku: ~10¹⁵ nodes without pruning
          ~10⁶ nodes with smart pruning (10⁹ reduction!)

PRUNING EFFICIENCY FACTORS:
  ✓ Early pruning (high in tree) = exponential savings
  ✓ Tight constraints = more pruning possible
  ✓ Good heuristics = predict pruning well
  ✓ Bad heuristics = little pruning, slow

EXAMPLE:
  Find 4-digit number where digits ∈ {1,2,3} and sum = 6
  
  Without pruning:
    3⁴ = 81 possibilities to check
  
  With pruning (partial sum check):
    [1,1,1,1]: sum=4, need 2 more, can't use 1 alone
    Prune all [1,1,1,?] where ? doesn't work
    [1,1,1,3]: sum=6 ✓
    [1,1,2,2]: sum=6 ✓
    [1,2,1,2]: sum=6 ✓
    [1,2,2,1]: sum=6 ✓
    [2,1,1,2]: sum=6 ✓
    [2,1,2,1]: sum=6 ✓
    [2,2,1,1]: sum=6 ✓
    [3,1,1,1]: sum=6 ✓
    
    Only ~8 valid, rest pruned
```

---

## 🔍 PART 4: BACKTRACKING TEMPLATE

### Generic Backtracking Algorithm

```
BACKTRACKING TEMPLATE
═════════════════════════════════════════════════════

// Main entry point
function Solve(problem):
    solution = []
    Backtrack(solution, problem)
    return solution

// Recursive backtracking
function Backtrack(partial_solution, problem):
    
    // BASE CASE: Found complete solution
    if IsSolution(partial_solution, problem):
        ProcessSolution(partial_solution)
        return true  // or continue to find more
    
    // RECURSIVE CASE: Try all choices
    for each choice in GetChoices(partial_solution, problem):
        
        // CONSTRAINT CHECK: Is choice valid?
        if IsValid(choice, partial_solution, problem):
            
            // MAKE CHOICE
            AddChoice(partial_solution, choice)
            
            // RECURSE
            if Backtrack(partial_solution, problem):
                return true  // or continue
            
            // BACKTRACK: Undo choice
            RemoveChoice(partial_solution, choice)
        
        // PRUNE: Skip invalid choice
    
    // No valid choice found
    return false

// HELPER FUNCTIONS:

function IsSolution(partial, problem):
    return all decisions made and valid

function GetChoices(partial, problem):
    return list of next options to try

function IsValid(choice, partial, problem):
    return choice doesn't violate constraints

function ProcessSolution(solution):
    // Handle found solution
    // Print, store, count, etc.

function AddChoice(partial, choice):
    partial.append(choice)

function RemoveChoice(partial, choice):
    partial.remove(choice)
```

### Visualization: Template Execution

```
BACKTRACKING TEMPLATE IN ACTION
═════════════════════════════════════════════════════

Backtrack([]):
  IsSolution([])? No (need 3 digits)
  GetChoices([])? [1,2,3]
  
  Try choice 1:
    IsValid(1,[])? Yes
    AddChoice: [1]
    
    Backtrack([1]):
      IsSolution([1])? No
      GetChoices([1])? [1,2,3]
      
      Try choice 1:
        IsValid(1,[1])? Yes
        AddChoice: [1,1]
        
        Backtrack([1,1]):
          IsSolution([1,1])? No
          GetChoices([1,1])? [1,2,3]
          
          Try choice 1:
            IsValid(1,[1,1])? Check constraint...
            IfYes: AddChoice, Backtrack([1,1,1])
              ...continue recursion...
            IfNo: Prune, try next
          
          Try choice 2:
            ...
          
          Try choice 3:
            ...
        
        RemoveChoice: back to [1]
      
      Try choice 2:
        ...
      
      Try choice 3:
        ...
    
    RemoveChoice: back to []
  
  Try choice 2:
    ...
  
  Try choice 3:
    ...

Pattern: Depth-first exploration with backtracking
```

---

## 🎯 DAY 1 SUMMARY

### What You Learned

```
✓ Backtracking algorithm definition and structure
✓ When to use backtracking vs other approaches
✓ State space tree representation
✓ DFS traversal with backtracking
✓ Pruning strategies and impact
✓ Generic backtracking template
✓ Implementation patterns
✓ Constraint checking and validation
```

### Key Concepts

```
BACKTRACKING CORE:
  1. Build incrementally
  2. Check constraints at each step
  3. Prune invalid branches
  4. Backtrack when stuck
  5. Continue searching alternatives

STATE SPACE:
  - Tree of all possible states
  - Nodes = partial solutions
  - Edges = choices
  - Root = empty solution
  - Leaves = complete solutions

PRUNING:
  - Constraint violations
  - Infeasibility checks
  - Bounding (optimization)
  - Symmetry breaking
  - Early termination
```

---

# 📅 DAY 2: COMPLEX BACKTRACKING PROBLEMS

## 🎯 Day Objective
Master N-Queens, Sudoku, permutations, combinations, and word search problems.

### ⏱️ Time Allocation
- N-Queens Problem: 25 minutes
- Sudoku Solver: 25 minutes
- Permutations & Combinations: 20 minutes
- Word Search & Maze: 20 minutes

---

## 🔍 PART 1: N-QUEENS PROBLEM

### Problem Definition

```
N-QUEENS PROBLEM
═════════════════════════════════════════════════════

Classic puzzle: Place n queens on n×n chessboard
               Such that NO two queens attack each other

ATTACK DEFINITION (Chess):
  Two queens attack if they share:
  - Same row
  - Same column
  - Same diagonal (↗️ or ↖️)

CONSTRAINT:
  - Exactly n queens (one per row usually)
  - No two queens attacking

EXAMPLES:

4-Queens problem:
  Valid solution:
    . Q . .
    . . . Q
    Q . . .
    . . Q .
  
  Not valid:
    Q Q . .  ← Same row (conflict)
    . . . .
    . . . .
    . . . .

8-Queens problem:
  Multiple solutions exist (92 total)
  One solution:
    . Q . . . . . .
    . . . . Q . . .
    . . . . . . . Q
    . . . . . Q . .
    . . Q . . . . .
    Q . . . . . . .
    . . . Q . . . .
    . . . . . . Q .
```

### N-Queens Solution Strategy

```
N-QUEENS BACKTRACKING APPROACH
═════════════════════════════════════════════════════

KEY OBSERVATION:
  In any valid solution, each row has exactly 1 queen
  And each column has exactly 1 queen
  (This is because n queens on n×n, no row attacks)

STRATEGY:
  Place queens row by row from top to bottom
  In each row, try each column
  Skip columns that would cause attack

PRUNING:
  For each row, only try columns where:
    - No queen in same column already (below)
    - No queen on same diagonal already
    - No queen on same diagonal already

DATA STRUCTURE:
  col[i] = column position of queen in row i
  Or: position[i] = column of queen in row i
  
  board[i][j] = 1 if queen at (i,j), 0 otherwise

ALGORITHM:

function SolveNQueens(n):
    board = n×n array of 0s
    solutions = []
    
    function Backtrack(row):
        if row == n:
            solutions.append(copy of board)  // Found solution
            return
        
        for col in 0 to n-1:
            if IsSafe(board, row, col):
                board[row][col] = 1  // Place queen
                Backtrack(row + 1)   // Recurse to next row
                board[row][col] = 0  // Backtrack
    
    function IsSafe(board, row, col):
        // Check column (above current row)
        for i in 0 to row-1:
            if board[i][col] == 1:
                return false
        
        // Check diagonal ↖️ (upper-left)
        for i, j in diagonal towards upper-left:
            if board[i][j] == 1:
                return false
        
        // Check diagonal ↗️ (upper-right)
        for i, j in diagonal towards upper-right:
            if board[i][j] == 1:
                return false
        
        return true
    
    Backtrack(0)
    return solutions
```

### N-Queens Execution Example (4-Queens)

```
N-QUEENS 4: STEP-BY-STEP EXECUTION
═════════════════════════════════════════════════════

Backtrack(row=0):
  Try col=0:
    board[0][0] = 1
    . . . .      ← Row 0, place at col 0
    . . . .
    . . . .
    . . . .
    
    Backtrack(row=1):
      Try col=0: Conflict! (same column)
      Try col=1: Conflict! (diagonal)
      Try col=2:
        board[1][2] = 1
        Q . . .
        . . Q .
        . . . .
        . . . .
        
        Backtrack(row=2):
          Try col=0: Conflict! (diagonal)
          Try col=1: 
            board[2][1] = 1
            Q . . .
            . . Q .
            . Q . .
            . . . .
            
            Backtrack(row=3):
              Try col=0: Conflict
              Try col=1: Conflict
              Try col=2: Conflict
              Try col=3: Conflict
              No valid column, backtrack
          
          Try col=3:
            board[2][3] = 1
            Q . . .
            . . Q .
            . . . Q
            . . . .
            
            Backtrack(row=3):
              Try col=0:
                board[3][0] = 1
                Q . . .
                . . Q .
                . . . Q
                Q . . .
                
                row=4 (== n): SOLUTION FOUND!
                Store this solution
              
              Try col=1: SOLUTION FOUND:
                Q . . .
                . . Q .
                . . . Q
                . Q . .
              
              Try remaining cols...
        
        Try remaining cols for row 2...
    
    Try remaining cols for row 1...
  
  Try col=1: (continue similar)
  Try col=2: (continue similar)
  Try col=3: (continue similar)

For 4-Queens: 2 solutions total
For 8-Queens: 92 solutions total
```

### Complexity and Pruning

```
N-QUEENS COMPLEXITY
═════════════════════════════════════════════════════

WITHOUT PRUNING:
  n^n possibilities (n choices for each row)
  4-Queens: 4^4 = 256 possibilities
  8-Queens: 8^8 = 16.7 million possibilities

WITH PRUNING (IsSafe checks):
  Massive reduction from pruning
  4-Queens: ~50 states explored (vs 256)
  8-Queens: ~114,688 states explored (vs 16.7 million)
  Pruning: ~99.3% reduction!

REASON FOR PRUNING EFFECTIVENESS:
  - Column constraint eliminates many states early
  - Diagonal constraints further prune
  - By row 3, usually very few columns valid
  - Exponential reduction from early pruning

OPTIMIZATION:
  Use set for columns already occupied
  Use set for diagonals already occupied
  O(1) check instead of O(row) scan
  
  Diagonals:
    Upper-left diagonal: row - col (constant)
    Upper-right diagonal: row + col (constant)
    Use sets: { (row-col), (row+col) }
```

---

## 🔍 PART 2: SUDOKU SOLVER

### Sudoku Problem Structure

```
SUDOKU PUZZLE
═════════════════════════════════════════════════════

Grid: 9×9 board divided into 9 3×3 boxes

Constraints:
  1. Each row must contain digits 1-9 exactly once
  2. Each column must contain digits 1-9 exactly once
  3. Each 3×3 box must contain digits 1-9 exactly once

Given: Some cells pre-filled (clues)
Goal: Fill remaining cells satisfying constraints

EXAMPLE:

Given puzzle:
  5 3 . | . 7 . | . . .
  6 . . | 1 9 5 | . . .
  . 9 8 | . . . | . 6 .
  ------+-------+------
  8 . . | . 6 . | . . 3
  4 . . | 8 . 3 | . . 1
  7 . . | . 2 . | . . 6
  ------+-------+------
  . 6 . | . . . | 2 8 .
  . . . | 4 1 9 | . . 5
  . . . | . 8 . | . 7 9

Solution:
  5 3 4 | 6 7 8 | 9 1 2
  6 7 2 | 1 9 5 | 3 4 8
  1 9 8 | 3 4 2 | 5 6 7
  ------+-------+------
  8 5 9 | 7 6 1 | 4 2 3
  4 2 6 | 8 5 3 | 7 9 1
  7 1 3 | 9 2 4 | 8 5 6
  ------+-------+------
  9 6 1 | 5 3 7 | 2 8 4
  2 8 7 | 4 1 9 | 6 3 5
  3 4 5 | 2 8 6 | 1 7 9
```

### Sudoku Backtracking Approach

```
SUDOKU SOLVER BACKTRACKING
═════════════════════════════════════════════════════

STRATEGY:
  Find empty cell
  Try each digit 1-9
  Check if digit valid (doesn't violate constraints)
  If valid: place digit, recurse
  If recursion succeeds: return
  Otherwise: backtrack (remove digit)
  Try next digit

DATA STRUCTURES:
  board[9][9] = grid values (0 = empty)
  rows[i] = set of digits in row i
  cols[j] = set of digits in column j
  boxes[box_id] = set of digits in box
  
  Lookup: O(1) instead of O(9) scanning

ALGORITHM:

function SolveSudoku(board):
    // Precompute existing values in rows/cols/boxes
    InitializeConstraints(board)
    
    if Backtrack():
        return board  // Solved
    else:
        return null   // No solution

function Backtrack():
    // Find next empty cell
    cell = FindEmptyCell()
    if cell == null:
        return true  // All cells filled, solved!
    
    row, col = cell
    box_id = (row // 3) * 3 + (col // 3)
    
    // Try each digit
    for digit in 1 to 9:
        if IsValid(digit, row, col, box_id):
            
            // Place digit
            board[row][col] = digit
            rows[row].add(digit)
            cols[col].add(digit)
            boxes[box_id].add(digit)
            
            // Recurse
            if Backtrack():
                return true
            
            // Backtrack
            board[row][col] = 0
            rows[row].remove(digit)
            cols[col].remove(digit)
            boxes[box_id].remove(digit)
    
    return false  // No digit worked

function IsValid(digit, row, col, box_id):
    return (digit not in rows[row] AND
            digit not in cols[col] AND
            digit not in boxes[box_id])

function FindEmptyCell():
    for i in 0 to 8:
        for j in 0 to 8:
            if board[i][j] == 0:
                return (i, j)
    return null  // No empty cell
```

### Sudoku Optimization: Most Constrained Variable

```
OPTIMIZATION: MOST CONSTRAINED VARIABLE (MCV)
═════════════════════════════════════════════════════

BASIC APPROACH:
  Find ANY empty cell
  Try digits 1-9

BETTER APPROACH (MCV):
  Find empty cell with FEWEST possible values
  This creates more pruning earlier

REASONING:
  Cell with 1 possible value: prune immediately if invalid
  Cell with 9 possible values: many branches to explore
  Process constrained cells first: exponential savings

EXAMPLE:

Partial Sudoku:
  1 _ _ | _ _ _ | _ _ _
  _ _ _ | _ _ _ | _ _ _
  ...

Cell A (row 0, col 1):
  Row 0 has: {1}
  Col 1 has: {2, 3, 4}
  Box 0 has: {5, 6}
  Possible: {7, 8, 9} (3 options)

Cell B (row 5, col 5):
  Row 5 has: {1,2,3,4}
  Col 5 has: {5,6,7}
  Box 4 has: {8,9}
  Possible: {} (0 options!) ← Can't fill

Choose Cell B first: immediately detect impossible
Save exploring Cell B's empty branches

ALGORITHM:

function FindEmptyCell():
    best_cell = null
    min_options = 10
    
    for i in 0 to 8:
        for j in 0 to 8:
            if board[i][j] == 0:
                possible = CountPossible(i, j)
                if possible < min_options:
                    min_options = possible
                    best_cell = (i, j)
    
    return best_cell

This heuristic dramatically speeds up solver
```

---

## 🔍 PART 3: PERMUTATIONS & COMBINATIONS

### Generating Permutations

```
PERMUTATIONS VIA BACKTRACKING
═════════════════════════════════════════════════════

PERMUTATION: All orderings of elements where each element used once

Example: Permutations of [1, 2, 3]
  [1, 2, 3], [1, 3, 2], [2, 1, 3], [2, 3, 1], [3, 1, 2], [3, 2, 1]
  Total: 3! = 6

BACKTRACKING APPROACH 1: Using position
───────────────────────────────

function GeneratePermutations(elements):
    permutations = []
    
    function Backtrack(current):
        if len(current) == len(elements):
            permutations.append(copy(current))
            return
        
        for element in elements:
            if element not in current:  // Not used yet
                current.append(element)
                Backtrack(current)
                current.pop()
    
    Backtrack([])
    return permutations

BACKTRACKING APPROACH 2: Using indices
──────────────────────────────────────

function GeneratePermutations(elements):
    permutations = []
    used = [false] * len(elements)
    current = []
    
    function Backtrack():
        if len(current) == len(elements):
            permutations.append(copy(current))
            return
        
        for i in 0 to len(elements)-1:
            if not used[i]:
                used[i] = true
                current.append(elements[i])
                Backtrack()
                current.pop()
                used[i] = false
    
    Backtrack()
    return permutations

APPROACH 2 BETTER because:
  - O(1) used check vs O(n) in-set check
  - Index-based marking is efficient
```

### Generating Combinations

```
COMBINATIONS VIA BACKTRACKING
═════════════════════════════════════════════════════

COMBINATION: Subset of k elements (order doesn't matter)

Example: 2-combinations of [1,2,3]
  [1,2], [1,3], [2,3]
  Total: C(3,2) = 3

vs Permutations (order matters):
  [1,2], [2,1], [1,3], [3,1], [2,3], [3,2]
  Total: P(3,2) = 6

COMBINATION KEY: Avoid duplicates by maintaining index order

ALGORITHM:

function GenerateCombinations(elements, k):
    combinations = []
    
    function Backtrack(current, start_index):
        if len(current) == k:
            combinations.append(copy(current))
            return
        
        // Only consider elements from start_index onward
        for i in start_index to len(elements)-1:
            current.append(elements[i])
            Backtrack(current, i+1)  // i+1 ensures no duplicates
            current.pop()
    
    Backtrack([], 0)
    return combinations

WHY start_index?
  Without it: [1,2] and [2,1] treated as different
  With it: only [1,2] generated (start from 1, then 2)
  Ensures C(n,k) = n!/(k!(n-k)!) not P(n,k)

EXAMPLE: Generate 2-combinations of [1,2,3]

Backtrack([], 0):
  i=0: [1]
    Backtrack([1], 1):
      i=1: [1,2]
        Backtrack([1,2], 2): len==k, found!
      i=2: [1,3]
        Backtrack([1,3], 3): len==k, found!
  i=1: [2]
    Backtrack([2], 2):
      i=2: [2,3]
        Backtrack([2,3], 3): len==k, found!
  i=2: [3]
    Backtrack([3], 3): no more elements

Result: [1,2], [1,3], [2,3]  ✓
```

### Combinations vs Permutations

```
COMPARISON: PERMUTATIONS vs COMBINATIONS
═════════════════════════════════════════════════════

DIMENSION          │ PERMUTATION      │ COMBINATION
───────────────────┼──────────────────┼─────────────────
Order matters      │ YES (A,B ≠ B,A)  │ NO (A,B = B,A)
Total count        │ P(n,k) = n!/(n-k)│ C(n,k) = n!/(k!(n-k)!)
Backtracking       │ Try all unused   │ Try all ≥ current
skip duplicates    │ Using set/used[] │ Using start_index
Example (n=3,k=2)  │ 6 total          │ 3 total

ALGORITHM DIFFERENCE:

Permutation:
  for each element not yet used:
      add element
      recurse (all remaining unused)

Combination:
  for each element from start_index onward:
      add element
      recurse (start from next index)

Subsets (all combinations):
  k ranges from 0 to n
  Generate all C(n,0) + C(n,1) + ... + C(n,n)
  Algorithm: Similar to combinations but try k=0,1,2,...,n
```

---

## 🔍 PART 4: WORD SEARCH & MAZE PROBLEMS

### Word Search in Grid

```
WORD SEARCH PROBLEM
═════════════════════════════════════════════════════

Problem: Given word and 2D grid of letters
         Find if word exists in grid
         Can move in 4 directions (up/down/left/right)
         Each cell used at most once

Example:

Grid:
  B M P D
  C Z L H
  I N J T
  O K M R

Find word: "BOLD"

Path:
  B (0,0) → O (3,0)? Can't reach in one step
  Actually need to search...

Valid example (4-letter word):
  Start B (0,0) → M (0,1) → P (0,2) → D (0,3)
  Path exists, found!

BACKTRACKING ALGORITHM:

function FindWord(grid, word):
    for i in 0 to rows-1:
        for j in 0 to cols-1:
            if grid[i][j] == word[0]:
                if Backtrack(grid, word, 0, i, j, visited):
                    return true
    return false

function Backtrack(grid, word, char_index, row, col, visited):
    if char_index == len(word):
        return true  // Found entire word
    
    if row < 0 or col < 0 or row >= rows or col >= cols:
        return false  // Out of bounds
    
    if visited[row][col]:
        return false  // Already used
    
    if grid[row][col] != word[char_index]:
        return false  // Character doesn't match
    
    // Mark as visited
    visited[row][col] = true
    
    // Try all 4 directions
    directions = [(0,1), (1,0), (0,-1), (-1,0)]
    for dr, dc in directions:
        new_row, new_col = row + dr, col + dc
        if Backtrack(grid, word, char_index+1, new_row, new_col, visited):
            visited[row][col] = false  // Backtrack
            return true
    
    // Backtrack
    visited[row][col] = false
    return false
```

### Maze Solving

```
MAZE SOLVING VIA BACKTRACKING
═════════════════════════════════════════════════════

Problem: 2D grid where 0=path, 1=wall
         Find path from start (s,s) to end (e,e)
         Can move in 4 directions

Example:

Maze:
  S . . .
  . # . #
  . . # .
  # . . E

S = start, E = end, # = wall, . = path

Solution: S → (0,1) → (0,2) → (1,2) → (2,1) → (2,2) → (2,3) E

ALGORITHM:

function FindPath(maze, start, end):
    visited = 2D array of false
    path = []
    
    if Backtrack(maze, start, end, visited, path):
        return path  // Found solution
    else:
        return null  // No path exists

function Backtrack(maze, current, end, visited, path):
    if current == end:
        path.append(current)
        return true  // Reached end
    
    if not IsValid(current, maze, visited):
        return false  // Can't move here
    
    // Mark as visited
    visited[current] = true
    path.append(current)
    
    // Try all 4 directions
    for each neighbor in GetNeighbors(current):
        if Backtrack(maze, neighbor, end, visited, path):
            return true
    
    // Backtrack
    path.pop()
    visited[current] = false
    return false

function IsValid(pos, maze, visited):
    row, col = pos
    if row < 0 or col < 0 or row >= rows or col >= cols:
        return false  // Out of bounds
    if maze[row][col] == 1:
        return false  // Wall
    if visited[row][col]:
        return false  // Already visited
    return true
```

---

## 🎯 DAY 2 SUMMARY

### What You Learned

```
✓ N-Queens problem formulation and solution
✓ Constraint checking for N-Queens
✓ Sudoku solver via backtracking
✓ Most constrained variable heuristic
✓ Generating permutations via backtracking
✓ Generating combinations (avoiding duplicates)
✓ Word search in grid
✓ Maze solving with path finding
✓ Visited tracking and backtracking patterns
```

### Algorithms Mastered

```
N-QUEENS:
  - Row-by-row placement
  - Column and diagonal conflict checking
  - 92 solutions for 8-Queens

SUDOKU:
  - Find empty cell, try digits
  - Row/column/box constraints
  - Most constrained variable optimization

PERMUTATIONS:
  - Try each unused element
  - Backtrack after recursion
  - n! total solutions

COMBINATIONS:
  - Use start_index to avoid duplicates
  - Maintain sorted order
  - C(n,k) solutions

WORD SEARCH & MAZE:
  - Grid traversal with visited tracking
  - 4-directional movement
  - Backtrack when stuck
```

---

# 📅 DAY 3: BRANCH & BOUND OPTIMIZATION

## 🎯 Day Objective
Master branch & bound technique for optimization problems with bounding strategies.

### ⏱️ Time Allocation
- Branch & Bound Concept: 20 minutes
- Bounding Techniques: 20 minutes
- Best-First Search: 15 minutes
- TSP & Knapsack Applications: 20 minutes
- Analysis & Comparison: 15 minutes

---

## 🔍 PART 1: BRANCH & BOUND FRAMEWORK

### What is Branch & Bound?

```
BRANCH & BOUND ALGORITHM
═════════════════════════════════════════════════════

Definition: Optimization algorithm using systematic search
           BRANCH: explore solution space (like backtracking)
           BOUND: compute limits on best possible solution
           PRUNE: skip branches that can't improve best known

Goal: Find OPTIMAL solution (not just any solution)
Time: Better than exhaustive search due to pruning

KEY INSIGHT:
  If best possible from state S ≤ current best known:
    Prune S (can't improve current best)

WHEN TO USE:
  ✓ Optimization problems (maximize/minimize)
  ✓ Upper and lower bounds computable
  ✓ Tight bounds enable pruning
  ✗ Feasibility problems (just find solution)
  ✗ All solutions needed
```

### Comparison: Backtracking vs Branch & Bound

```
BACKTRACKING vs BRANCH & BOUND
═════════════════════════════════════════════════════

BACKTRACKING:
  Goal: Find ANY valid solution or ALL solutions
  Constraints: Hard (must satisfy)
  Pruning: Constraint violation only
  Order: DFS (depth-first)

BRANCH & BOUND:
  Goal: Find OPTIMAL solution
  Constraints: Hard (must satisfy)
        Soft (optimization criterion)
  Pruning: Constraint violation + bounding
  Order: Often best-first or BFS
  Data structure: Priority queue (for best-first)

PRUNING DIFFERENCE:

Backtracking:
  if violates hard constraint:
      prune

Branch & Bound:
  if violates hard constraint:
      prune
  if best_possible_from(state) <= current_best:
      prune

The second condition is powerful for optimization
```

### Branch & Bound Algorithm Template

```
BRANCH & BOUND TEMPLATE
═════════════════════════════════════════════════════

function BranchAndBound(problem):
    best_solution = null
    best_value = infinity  // For minimization
    // (or -infinity for maximization)
    
    // Use priority queue for best-first search
    queue = PriorityQueue()  // min-heap by bound
    
    // Start with root state
    root_state = InitializeState(problem)
    root_bound = ComputeBound(root_state, problem)
    queue.add(root_state, priority=root_bound)
    
    while queue not empty:
        state = queue.extract_min()
        
        // PRUNING: Can't improve best
        if Bound(state) >= best_value:
            continue  // Skip this branch
        
        if IsSolution(state):
            // Found feasible solution
            value = Evaluate(state)
            if value < best_value:  // Better than current best
                best_value = value
                best_solution = state
        else:
            // Not solution yet, branch
            for each child in GetChildren(state):
                bound = ComputeBound(child, problem)
                if bound < best_value:  // Worth exploring
                    queue.add(child, priority=bound)
    
    return best_solution, best_value

KEY COMPONENTS:

1. STATE: Partial solution
2. BOUND: Upper limit on best possible from this state
3. PRUNING: if bound >= best_known, skip
4. BRANCHING: Generate children for exploration
5. FEASIBILITY: Check if complete solution found
```

---

## 🔍 PART 2: BOUNDING STRATEGIES

### Upper and Lower Bounds

```
BOUND TYPES IN OPTIMIZATION
═════════════════════════════════════════════════════

For MINIMIZATION problem:

UPPER BOUND:
  Actual feasible solution value
  Example: "I found a tour of length 100"
  Current best known: 100
  Can't do worse than this
  Updated when better feasible solution found

LOWER BOUND:
  Best we could hope for (relaxation)
  Example: "Even with no edges, connects 10 cities = ≥ 50"
  Can't do better than this
  Used to prune: if lower_bound >= upper_bound, skip

BOUND INEQUALITY:
  lower_bound ≤ optimal ≤ upper_bound
                          (if feasible solution)

PRUNING RULE:
  if lower_bound >= best_upper_bound_found:
      Can't beat current best
      Prune this branch

EXAMPLE: TSP with 4 cities

City distances:
  1-2: 10, 1-3: 5, 1-4: 8
  2-3: 3, 2-4: 12, 3-4: 7

Best tour found so far: 1→2→3→4→1 = 10+3+7+8 = 28
Upper bound: 28

For partial tour 1→3:
  Cost so far: 5
  Minimum to complete: ≥ 8 (1-4) + 10 (2-4) + 2 (2-1 edge to close)
  Lower bound: 5 + 15 = 20 (loose)
  
  Actually better estimate:
  Need 2 more cities: 2,4
  Cheapest edges: 2-4=12, 4-1=8
  Lower bound: 5 + 12 + 8 = 25
  
  25 < 28 (current best), so worth exploring!
```

### Computing Bounds for TSP

```
TRAVELING SALESMAN PROBLEM (TSP) BOUNDING
═════════════════════════════════════════════════════

PROBLEM: Find minimum-cost tour visiting all cities once

BOUNDS FOR PRUNING:

1. MINIMUM SPANNING TREE (MST) BOUND
   ───────────────────────────────────
   MST connects all cities with minimum edges
   But MST isn't a tour (may have degree > 2)
   
   Lower bound: Minimum weight to visit all cities
               ≥ MST weight
   
   But tour must visit each city once → edges in tour > MST
   Better bound: MST + cheapest edge from low-degree vertex

2. REDUCED COST BOUND
   ────────────────────
   For partial tour:
     Cost = edges already selected
     Lower bound = cost + minimum to complete
   
   To complete:
     - Need n-k more edges (k already selected)
     - Each unvisited city needs one outgoing edge
     - Minimum: sum of cheapest outgoing edges

3. HOLDING'S BOUND
   ────────────────
   Similar idea but more sophisticated
   For each city: if not visited, reserve cheapest edge to reach

EXAMPLE:

Partial tour: 1 → 3 → 4 (cost so far = 5 + 7 = 12)

Remaining cities: 2

To complete:
  4 → 2 or 4 → ? → 2 → 1
  Need to return to 1
  Minimum edges needed:
    - Depart 4: cheapest to unvisited = 4→2 = 12
    - From 2 to 1: 2→1 = 10
  Lower bound: 12 + 12 + 10 = 34

If current best tour = 28, then 34 > 28
PRUNE this branch! Can't beat current best.
```

### Fractional Relaxations for Knapsack

```
KNAPSACK BOUND VIA FRACTIONAL RELAXATION
═════════════════════════════════════════════════════

0-1 KNAPSACK:
  Select items to maximize value subject to weight ≤ W

FRACTIONAL KNAPSACK (Relaxation):
  Allows taking fractions of items
  Upper bound on 0-1 knapsack (can do better with fractions)
  Computable greedily

BOUNDING STRATEGY:

For partial solution (some items selected, some remaining):
  Current value = value of selected items
  Remaining capacity = W - weight of selected
  
  Upper bound = current value + 
                fractional knapsack(remaining items, capacity)
  
  If upper bound ≤ best known:
      Prune (can't beat current best)

EXAMPLE:

Capacity: 10
Items: A(w=5, v=10), B(w=4, v=5), C(w=3, v=8)
Best known solution: value 13

Partial: Selected A (w=5, v=10)
Remaining capacity: 5
Remaining items: B(w=4, v=5), C(w=3, v=8)

Fractional knapsack of remaining:
  Ratios: B = 5/4 = 1.25, C = 8/3 = 2.67
  Take C fully: w=3, v=8, remaining capacity = 2
  Take partial B: 2/4 of B = 2kg at ratio 1.25 = value 2.5
  Fractional value: 8 + 2.5 = 10.5

Upper bound for this partial: 10 + 10.5 = 20.5

Is 20.5 > best known 13? YES, worth exploring
So DON'T prune this branch
```

---

## 🔍 PART 3: BEST-FIRST SEARCH

### Priority Queue Strategy

```
BEST-FIRST SEARCH in BRANCH & BOUND
═════════════════════════════════════════════════════

Unlike DFS (backtracking):
  Explores most promising states first
  Uses priority queue (min-heap)
  Best bound = highest priority

ADVANTAGES:
  1. Find good solutions early
  2. Better pruning in later stages
  3. Can often find near-optimal quick
  4. Better anytime performance

DISADVANTAGES:
  1. More memory (queue vs stack)
  2. More overhead (heap operations)
  3. Slower for feasibility (DFS better)

BEST-FIRST vs DEPTH-FIRST:

DFS (Backtracking):
  └─ Goes deep quickly
  └─ Uses stack (implicit recursion)
  └─ Space: O(depth)
  └─ Time: varies, may explore many dead ends

BFS (Best-First):
  └─ Explores promising branches first
  └─ Uses priority queue
  └─ Space: O(width)
  └─ Time: Often faster for optimization

EXAMPLE COMPARISON (TSP):

DFS approach:
  1 → 2 → 3 → 4 → ...
  Get solution early, update bound
  But may explore many bad branches first

BFS approach:
  Explore states by bound value
  Process: most promising → least promising
  Find good solutions faster
  Better bound pruning later
```

### Implementation Strategy

```
BEST-FIRST BRANCH & BOUND IMPLEMENTATION
═════════════════════════════════════════════════════

Data Structure: Min-Heap Priority Queue

Heap ordered by: Lower bound (ascending)
  State with best (lowest) bound: front of queue
  Explored first

Algorithm Flow:

1. Initialize:
   best_solution = null
   best_value = ∞
   queue.add(root_state, priority=root_bound)

2. Loop while queue not empty:
   state = queue.extract_min()  // Best bound first
   
   if state.bound >= best_value:
       continue  // Prune
   
   if IsSolution(state):
       if state.value < best_value:
           best_solution = state
           best_value = state.value
   else:
       for child in Generate(state):
           if child.bound < best_value:
               queue.add(child, priority=child.bound)

3. Return best_solution

KEY: Process by bound value, not depth
     Exponentially fewer states explored
     Better solutions found earlier
```

---

## 🔍 PART 4: TSP & KNAPSACK WITH BRANCH & BOUND

### TSP Branch & Bound

```
TSP: TRAVELING SALESMAN PROBLEM
═════════════════════════════════════════════════════

BRANCH:
  Partial tour: 1 → i₁ → i₂ → ...
  Next city: try all unvisited
  State = (current city, visited set, cost so far)

BOUND:
  Lower bound = cost so far + MST(unvisited + current)
  Or: cost so far + sum of cheapest edges from each unvisited

PRUNING:
  if lower_bound >= best_tour_found:
      skip this branch

ALGORITHM:

function TSP_BB(cities, distances):
    best_tour = null
    best_distance = ∞
    
    queue = PriorityQueue()
    initial_state = (city=0, visited={0}, cost=0)
    bound = ComputeBound(initial_state, distances)
    queue.add(initial_state, priority=bound)
    
    while queue not empty:
        state = queue.extract_min()
        current, visited, cost = state
        
        if cost >= best_distance:
            continue  // Prune
        
        if len(visited) == n:  // All cities visited
            // Return to start
            total = cost + distances[current][0]
            if total < best_distance:
                best_distance = total
                best_tour = state
        else:
            // Try next city
            for next_city in unvisited:
                new_visited = visited ∪ {next_city}
                new_cost = cost + distances[current][next_city]
                new_bound = new_cost + ComputeBound(...)
                
                if new_bound < best_distance:
                    new_state = (next_city, new_visited, new_cost)
                    queue.add(new_state, priority=new_bound)
    
    return best_tour, best_distance

COMPLEXITY:
  Worst case: Still exponential
  But pruning significantly reduces explored states
  Real instances: 100-1000x speedup vs brute force
```

### Knapsack with Branch & Bound

```
0-1 KNAPSACK: BRANCH & BOUND
═════════════════════════════════════════════════════

BRANCH:
  For each item: include or exclude
  State = (items considered, weight so far, value so far)

BOUND:
  Upper bound = value so far + 
                fractional knapsack(remaining items, remaining capacity)

PRUNING:
  if upper_bound <= best_value_found:
      skip this branch

ALGORITHM:

function Knapsack_BB(items, capacity):
    best_value = 0
    best_selection = []
    
    queue = PriorityQueue()
    initial_state = (item_idx=0, weight=0, value=0, selected=[])
    bound = ComputeBound_Knapsack(initial_state, items, capacity)
    queue.add(initial_state, priority=-bound)  // Max-heap
    
    while queue not empty:
        state = queue.extract_min()  // Best bound
        idx, weight, value, selected = state
        
        if -Bound(state) <= best_value:
            continue  // Prune (can't improve)
        
        if idx == n:  // All items considered
            if value > best_value:
                best_value = value
                best_selection = selected
        else:
            item = items[idx]
            
            // Branch 1: Include item
            if weight + item.weight <= capacity:
                new_state_1 = (idx+1, 
                              weight + item.weight,
                              value + item.value,
                              selected + [idx])
                bound_1 = ComputeBound_Knapsack(new_state_1, ...)
                queue.add(new_state_1, priority=-bound_1)
            
            // Branch 2: Exclude item
            new_state_2 = (idx+1, weight, value, selected)
            bound_2 = ComputeBound_Knapsack(new_state_2, ...)
            queue.add(new_state_2, priority=-bound_2)
    
    return best_value, best_selection
```

---

## 🎯 DAY 3 SUMMARY

### What You Learned

```
✓ Branch & Bound algorithm framework
✓ Optimal solution finding (not just feasible)
✓ Bounding techniques (upper and lower bounds)
✓ Pruning with bounds
✓ Best-first search strategy
✓ Priority queue for state ordering
✓ TSP with branch & bound
✓ Knapsack with branch & bound
✓ Complexity reduction through pruning
✓ Real-world optimization applications
```

### Key Concepts

```
BRANCH & BOUND CORE:
  1. Systematic search (branching)
  2. Compute bounds (upper/lower)
  3. Prune unpromising branches
  4. Find optimal solution

BOUNDING STRATEGIES:
  - MST for TSP
  - Fractional relaxation for knapsack
  - Reduced cost methods
  - Holding's bound

SEARCH ORDER:
  - Best-first (by bound) better for optimization
  - DFS (backtracking) better for feasibility
  - Priority queue enables best-first

PRUNING POWER:
  - Exponential speedup vs brute force
  - Tight bounds enable more pruning
  - Early good solutions enable later pruning
```

---

# 📅 DAY 4: AMORTIZED ANALYSIS

## 🎯 Day Objective
Master three amortized analysis methods: aggregate, accounting, and potential.

### ⏱️ Time Allocation
- Amortized Complexity Concept: 15 minutes
- Aggregate Method: 20 minutes
- Accounting Method: 20 minutes
- Potential Method: 20 minutes
- Applications: 15 minutes

---

## 🔍 PART 1: AMORTIZED ANALYSIS INTRODUCTION

### What is Amortized Analysis?

```
AMORTIZED COMPLEXITY ANALYSIS
═════════════════════════════════════════════════════

Definition: Analyze AVERAGE cost per operation over a SEQUENCE
           Some operations expensive, many cheap
           Amortized cost = total cost / operations

WHY AMORTIZED?
  Standard analysis sometimes too pessimistic
  Worst case: all operations expensive
  Reality: some expensive, most cheap
  
EXAMPLE: Dynamic Array with Doubling

Append operation:
  Usually: O(1) — just add to end
  Sometimes: O(n) — array full, reallocate, copy all

Standard worst-case: O(n) per append
Amortized: O(1) per append!

INTUITION:
  n appends:
    ~n operations at O(1)
    ~log(n) reallocations at O(n)
  Total: O(n + n + n/2 + n/4 + ...) = O(n)
  Per operation: O(n) / n = O(1) amortized

KEY INSIGHT:
  Expensive operations are INFREQUENT
  Their cost AMORTIZED over many cheap operations
```

### When Expensive Meets Frequent

```
AMORTIZED vs WORST CASE
═════════════════════════════════════════════════════

SCENARIO 1: Expensive but INFREQUENT
  └─ Amortized analysis applies
  └─ Example: Dynamic array doubling
               Operations: mostly O(1), rare O(n)
               Amortized: O(1)
  └─ Explains why arrays are practical despite occasional realloc

SCENARIO 2: Expensive and FREQUENT
  └─ Amortized analysis doesn't help
  └─ Example: Linked list iteration
               Operations: mostly O(1), but EACH node
               Amortized: O(1) per traversal, but total O(n)
  └─ No averaging possible, linear is fundamental

SCENARIO 3: Variable cost, need guarantee
  └─ Amortized analysis useful
  └─ Example: Splay trees
               Operations: O(log n) amortized
               Some O(n), but rare
               Guaranteed good average over sequence
```

---

## 🔍 PART 2: AGGREGATE METHOD

### Aggregate Analysis Technique

```
AGGREGATE METHOD
═════════════════════════════════════════════════════

Technique: Calculate TOTAL cost for n operations
          Divide by n for average cost per operation

PROCESS:

1. Analyze worst-case SEQUENCE of n operations
2. Compute total cost: T(n)
3. Amortized cost = T(n) / n

EXAMPLE 1: DYNAMIC ARRAY WITH DOUBLING
───────────────────────────────────────

Append to dynamic array:
  Size n, capacity c
  If n < c: O(1) — just append
  If n == c: O(n) — reallocate, double capacity, copy
  After realloc: capacity = 2n

Sequence of n appends starting from empty array:

Appends 1-1: $1 each, no reallocation (cost 1)
Appends 2-2: $1 + reallocate ($2 to copy + alloc) = $3
Appends 3-4: $1 each, no reallocation (cost 2)
Appends 5-8: $1 each until append 5
             Append 5: $1 + reallocate ($4 copy) = $5
             (costs 2+5 = 7)
Appends 9-16: $1 each until append 9
              Append 9: $1 + reallocate ($8 copy) = $9
...

Pattern:
  Without realloc: 1+1+1+1 = 4 (appends 3-4)
  With realloc: $5 (append 5, includes doubling 4→8)
               $9 (append 9, includes doubling 8→16)
               $17 (append 17, includes doubling 16→32)

Total cost for n appends:
  Expensive appends: 3 + 5 + 9 + 17 + ... + (2^k)
                   = 3 + 5 + 9 + 17 + ... + 2^(ceil(log n)+1)
                   ≤ 2 + 4 + 8 + 16 + ... + 2^(k+1)
                   ≤ 2 × 2^(k+1) = 2 × n (approximately)
  
  Cheap appends: n - log n
  
  Total: ~n + 2n = 3n

Amortized cost: 3n / n = O(1) per append ✓

EXAMPLE 2: BINARY COUNTER
──────────────────────────

Counter using array of bits
Increment operation:
  Flip bits from right until find 0, flip 0 to 1

Example:
  0000 → 0001 (flip 1 bit) — cost 1
  0001 → 0010 (flip 2 bits) — cost 2
  0010 → 0011 (flip 1 bit) — cost 1
  0011 → 0100 (flip 3 bits) — cost 3
  0100 → 0101 (flip 1 bit) — cost 1
  ...

n increments:
  Bit 0 flips: n times
  Bit 1 flips: ⌊n/2⌋ times
  Bit 2 flips: ⌊n/4⌋ times
  Bit k flips: ⌊n/2^k⌋ times
  
  Total flips: n + n/2 + n/4 + ... < 2n

Amortized cost: 2n / n = O(1) per increment
```

### Aggregate Analysis Strengths

```
AGGREGATE METHOD STRENGTHS & WEAKNESSES
═════════════════════════════════════════════════════

STRENGTHS:
  ✓ Simple to apply
  ✓ Direct calculation
  ✓ Often gives good bounds

WEAKNESSES:
  ✗ Treats ALL operations the same
  ✗ Can't distinguish expensive vs cheap operations
  ✗ Doesn't provide per-operation insight
  ✗ Less useful when need operation-specific bounds

BETTER WHEN:
  - All operations behave similarly
  - Want one average cost per operation
  - Simple analysis sufficient

USE WITH CAUTION:
  - Different operations with different costs
  - Need operation-specific analysis
  - Complex data structure with varied behavior
```

---

## 🔍 PART 3: ACCOUNTING METHOD

### Accounting Method Explanation

```
ACCOUNTING METHOD
═════════════════════════════════════════════════════

Idea: Assign (possibly fictitious) costs to operations
     "Bank account" of prepaid work
     Expensive operations draw from account
     Cheap operations deposit to account
     Show account never goes negative

ANALOGY:
  Each operation has:
    - Actual cost (real time)
    - Amortized cost (charged to user)
  
  Difference: credit (+) or debt (-)
  Total credit in account: must be ≥ total debt
  If credit always ≥ 0: amortized cost is upper bound

PROCESS:

1. Assign amortized cost to each operation
2. Show actual cost ≤ amortized cost + debit from account
3. Prove account never negative
4. Conclusion: amortized cost = assigned cost

FORMULA:

Let:
  cᵢ = actual cost of operation i
  ĉᵢ = amortized cost assigned to operation i (charge to user)
  Dᵢ = balance after operation i

Then:
  Dᵢ = Dᵢ₋₁ + ĉᵢ - cᵢ
  
  If ĉᵢ ≥ cᵢ: adding to account (Dᵢ ≥ Dᵢ₋₁)
  If ĉᵢ < cᵢ: drawing from account (Dᵢ < Dᵢ₋₁)
  
  Must prove: Dᵢ ≥ 0 for all i
```

### Accounting Method Example: Dynamic Array

```
DYNAMIC ARRAY ACCOUNTING ANALYSIS
═════════════════════════════════════════════════════

Operations: Append and Delete

Assign amortized costs:
  Append: ĉ_append = 3
  Delete: ĉ_delete = 1

Array state tracking:
  size = number of elements
  capacity = allocated space
  load factor = size / capacity

Actual costs:
  Append when not full: $1
  Append when full: $size (copy all elements)
  Delete when above threshold: $1 (or reallocate if shrinking)
  Delete when below threshold: $size (reallocate)

ANALYSIS:

When appending and not full:
  actual_cost = 1
  amortized_cost = 3
  credit = 3 - 1 = 2 (deposit to account)

When appending and full (size = capacity):
  actual_cost = size + 1 (reallocate, copy size elements, append 1)
             = size + 1
  amortized_cost = 3
  
  But wait, account has:
    2 + 2 + ... + 2 credits from previous appends
    = 2 × size (from size previous non-realloc appends)
  
  Used: size + 1
  Remaining: 2 × size - (size + 1) = size - 1 ≥ 0 ✓

Pattern:
  - Each of first (size-1) appends deposits 2
  - Total: 2 × (size - 1) = 2 × size - 2
  - Realloc costs: size + 1
  - Balance: 2 × size - 2 - (size + 1) = size - 3 ≥ 0 ✓

Conclusion: Account never negative
           Amortized cost = 3 per append ✓
```

### Accounting Method Strengths

```
ACCOUNTING METHOD ADVANTAGES
═════════════════════════════════════════════════════

STRENGTHS:
  ✓ Operation-specific costs
  ✓ Intuitive "bank account" model
  ✓ Can vary cost per operation type
  ✓ Visualizes credit flow
  ✓ Good for mixed operations

WEAKNESSES:
  ✗ Must guess correct amortized costs
  ✗ If guess too low: can't prove no negative balance
  ✗ If guess too high: loose bound
  ✗ Less systematic than potential method

BETTER WHEN:
  - Different operations have different amortized costs
  - Intuitive credit assignment possible
  - Want per-operation analysis

EXAMPLES:
  - Stack with multipop
  - Queue with variable operations
  - Dynamic arrays with append and delete
```

---

## 🔍 PART 4: POTENTIAL METHOD

### Potential Function Concept

```
POTENTIAL METHOD
═════════════════════════════════════════════════════

Idea: Define potential function Φ(state)
     Measures "readiness" for future expensive operations

FORMULA:

Amortized cost = actual cost + ΔΦ
               = actual cost + (Φ_after - Φ_before)

Intuition:
  If Φ decreases: potential "released" to pay for cost
  If Φ increases: potential "stored" for future operations

CONSTRAINTS:
  Φ(initial) = 0 (no credit initially)
  Φ(final) ≥ 0 (no negative credit)
  For all i: Φᵢ ≥ 0 (never go negative)

ANALYSIS:

Sum of amortized costs for n operations:
  Σ ĉᵢ = Σ(cᵢ + ΔΦᵢ)
       = Σ cᵢ + (Φ_final - Φ_initial)
       ≤ Σ cᵢ + Φ_final  (since Φ_initial = 0)

If Φ_final ≥ 0:
  Σ ĉᵢ ≥ Σ cᵢ (amortized ≥ actual)
  
Amortized per operation: (Σ ĉᵢ) / n
```

### Potential Method Example: Dynamic Array

```
DYNAMIC ARRAY POTENTIAL ANALYSIS
═════════════════════════════════════════════════════

Define potential function:
  Φ(state) = 2 × size - capacity
  
  size = elements in array
  capacity = allocated space
  
  "Slack" for doubling: potential energy stored

Initial state (empty array):
  Φ(0) = 2 × 0 - 0 = 0 ✓

After appending n elements (array full, size = capacity = n):
  Φ(n) = 2 × n - n = n ≥ 0 ✓

ANALYSIS:

Case 1: Append when not full (size < capacity)
  Actual cost: 1
  Before: size = s, capacity = c (s < c)
          Φ_before = 2s - c
  After:  size = s+1, capacity = c
          Φ_after = 2(s+1) - c = 2s - c + 2
  ΔΦ = +2
  Amortized: 1 + 2 = 3 ✓

Case 2: Append when full (size = capacity = n)
  Actual cost: n + 1 (copy n elements + append 1)
  Before: size = n, capacity = n
          Φ_before = 2n - n = n
  After:  size = n+1, capacity = 2n
          Φ_after = 2(n+1) - 2n = 2
  ΔΦ = 2 - n (NEGATIVE! potential released)
  Amortized: (n+1) + (2 - n) = 3 ✓

Analysis:
  n appends, starting from empty
  Append 1-1: amortized 3
  Append 2-2: actual 2, amortized 3
  Append 3-4: amortized 3 each
  Append 5-8: amortized 3 each, with one expensive (5)
  
  Total amortized: 3n
  Amortized per operation: 3 ✓

Key insight:
  Potential increases during cheap appends (storing energy)
  Potential decreases during expensive appends (releasing energy)
  Difference = amortized cost stays constant!
```

### Choosing Potential Functions

```
POTENTIAL FUNCTION DESIGN
═════════════════════════════════════════════════════

GOOD POTENTIAL FUNCTIONS:

1. Measures "imbalance" or "disorder"
   Example: Array load factor (how full)
   Indicator of future reallocation need

2. Captures unused capacity
   Example: 2 × size - capacity
   Quantifies room for growth

3. Monotonic with work done
   Should increase with cheap ops, decrease with expensive

4. Non-negative throughout
   No negative potential allowed

EXAMPLES:

Dynamic Array:
  Φ = 2 × size - capacity
  Increases: +2 per cheap append (room for later realloc)
  Decreases: -(capacity/2) when reallocate (half new space)

Stack with Multipop:
  Φ = size (number of elements)
  Push: Φ increases by 1
  Pop: Φ decreases by 1
  Multipop(k): Φ decreases by k
  Amortized: Push $1, Multipop $0 (draw from potential)

Splay Trees:
  Φ = Σ rank(v) where rank(v) = log(subtree_size(v))
  Complex analysis, but shows O(log n) amortized
```

---

## 🔍 PART 5: AMORTIZED APPLICATIONS

### Splay Trees

```
SPLAY TREES: AMORTIZED ANALYSIS
═════════════════════════════════════════════════════

Self-adjusting binary search tree
Move accessed elements to root

Operations: Search, Insert, Delete — all O(log n) AMORTIZED

The trick: Splay operation (rotation up)
  Expensive per operation but amortized to O(log n)

Amortized Analysis:
  Potential function Φ = Σ rank(v)
  rank(v) = ⌊log(subtree_size(v))⌋
  
  Splay operation at depth d:
    Actual cost: O(d) rotations
    Potential change: O(log n) (reduces rank of many nodes)
    Amortized: O(log n)
  
  Therefore: Search O(log n) amortized
             Insert O(log n) amortized
             Delete O(log n) amortized

Why expensive operations are infrequent:
  - Deep trees get restructured quickly
  - Future operations likely on accessed elements
  - Recently used element at root (cache-friendly)
```

### Fibonacci Heaps

```
FIBONACCI HEAPS: AMORTIZED BOUNDS
═════════════════════════════════════════════════════

Advanced data structure with excellent amortized bounds:

Operation          │ Worst Case    │ Amortized
───────────────────┼───────────────┼──────────
Insert             │ O(1)          │ O(1)
Decrease-Key       │ O(log n)      │ O(1)
Extract-Min        │ O(n)          │ O(log n)
Delete             │ O(n)          │ O(log n)

Key insight: Decrease-Key is O(1) amortized!
             Most operations cheap, extract-min expensive but rare

Used in Dijkstra's shortest path:
  n inserts + m decrease-keys + n extract-mins
  Heap approach: O((n+m) log n)
  Fibonacci: O(m + n log n)
             
             Huge improvement for sparse graphs!

The trick: Lazy consolidation
  When extract-min: consolidate trees (O(log n))
  Decrease-key: just mark (O(1))
  Cost of consolidation amortized over many operations
```

### Self-Adjusting Structures

```
SELF-ADJUSTING DATA STRUCTURE PATTERN
═════════════════════════════════════════════════════

Many data structures use amortized analysis:

1. MOVE-TO-FRONT LIST
   Operation: Access element
   Actual: O(position) to find, O(1) to move
   Amortized: O(1) in many scenarios
   
   Potential: Distance from front
   Moving to front costs position but future access cheap

2. UNION-FIND (Disjoint Set Union)
   Operations: Union, Find
   Amortized: O(α(n)) where α is inverse Ackermann
   (α(n) ≤ 5 for all practical n)
   
   Potential: Related to rank
   Path compression gives amazing amortized bounds

3. DYNAMIC ARRAYS (Vector)
   Append: O(1) amortized
   Doubling strategy maintains potential
   
4. HASH TABLES
   Insert/Delete/Lookup: O(1) amortized
   Rehashing expensive but infrequent
   Amortized over many operations

COMMON PATTERN:
  Expensive operations restructure data
  This restructuring enables cheap future operations
  Amortized analysis shows cost is reasonable over time
```

---

## 🎯 DAY 4 SUMMARY

### What You Learned

```
✓ Amortized complexity definition and motivation
✓ Aggregate method: total cost / operations
✓ Accounting method: credit/debit with bank account
✓ Potential method: potential function and ΔΦ
✓ Dynamic arrays analyzed three ways
✓ Binary counter amortized analysis
✓ Splay trees and self-adjusting structures
✓ Fibonacci heaps and specialized bounds
✓ Choosing good potential functions
✓ Real-world applications and impact
```

### Key Insights

```
AMORTIZED ANALYSIS CORE:
  1. Some operations expensive, most cheap
  2. Expensive operations infrequent
  3. Cost amortized over sequence of operations
  4. Shows algorithm practical despite occasional expensive ops

THREE METHODS:
  - Aggregate: Total cost / n (simplest)
  - Accounting: Credit/debit with balance (intuitive)
  - Potential: Potential function (most rigorous)

APPLICATIONS:
  - Dynamic arrays: O(1) per append
  - Binary counters: O(1) per increment
  - Union-find: O(α(n)) per operation
  - Self-adjusting structures: O(log n) per operation
  - Splay trees & Fibonacci heaps: Complex but excellent bounds

DESIGN PRINCIPLES:
  - Expensive operations restructure data
  - Restructuring enables cheap future operations
  - Amortization spreads cost evenly
  - Tight analysis shows true efficiency
```

---

# 📅 DAY 5: INTEGRATION & MIXED PARADIGMS

## 🎯 Day Objective
Understand how to combine backtracking, branch & bound, and other paradigms for complex problems.

### ⏱️ Time Allocation
- Paradigm Selection Guide: 20 minutes
- Combining Techniques: 25 minutes
- Mixed Paradigm Examples: 25 minutes
- Problem-Solving Strategy: 20 minutes

---

## 🔍 PART 1: PARADIGM SELECTION GUIDE

### Decision Framework

```
ALGORITHM PARADIGM SELECTION
═════════════════════════════════════════════════════

When given a problem, ask:

1. PROBLEM TYPE?
   ├─ Feasibility (find ANY solution)
   │  └─ BACKTRACKING likely good
   ├─ Optimization (find BEST solution)
   │  └─ BRANCH & BOUND or DP
   ├─ Enumeration (find ALL solutions)
   │  └─ BACKTRACKING with tracking
   └─ Counting (HOW MANY solutions)
      └─ DP with counting or backtracking

2. CONSTRAINTS STRUCTURE?
   ├─ Hard constraints (must satisfy)
   │  └─ Use constraint checking (backtracking)
   ├─ Soft constraints (optimization criterion)
   │  └─ Use bounding (branch & bound)
   └─ Mixed constraints
      └─ Hybrid approach

3. SEARCH SPACE SIZE?
   ├─ Small (< 2^20)
   │  └─ Backtracking might work
   ├─ Medium (2^20 to 2^40)
   │  └─ Need good pruning
   ├─ Large (> 2^40)
   │  └─ Must use heuristics or approximation
   └─ Polynomial (< n^4)
      └─ DP or greedy

4. SOLUTION STRUCTURE?
   ├─ Incremental (build piece by piece)
   │  └─ BACKTRACKING natural
   ├─ Stateless (solution independent of path)
   │  └─ DP or greedy
   ├─ Recursive with overlap
   │  └─ DP or memoization
   └─ No clear structure
      └─ May need heuristics

5. PRUNING POTENTIAL?
   ├─ Strong (many branches prunable)
   │  └─ Backtracking or B&B good
   ├─ Medium (some pruning possible)
   │  └─ May work, or try DP
   ├─ Weak (little pruning possible)
   │  └─ Probably need DP or approximation
   └─ Unknown
      └─ Experiment with multiple approaches
```

### Paradigm Characteristics

```
ALGORITHM PARADIGM COMPARISON MATRIX
═════════════════════════════════════════════════════

PARADIGM          │ BEST FOR        │ WORST FOR      │ TIME
──────────────────┼─────────────────┼────────────────┼──────────
Backtracking      │ Feasibility     │ Dense search   │ O(2^n)
                  │ Constraint sat  │ Minimal pruning│
                  │ All solutions   │                │
──────────────────┼─────────────────┼────────────────┼──────────
Branch & Bound    │ Optimization    │ Complex bounds │ O(2^n)
                  │ Best solution   │ Weak pruning   │
                  │ Some solutions  │                │
──────────────────┼─────────────────┼────────────────┼──────────
Dynamic Prog      │ Opt substructure│ No overlap     │ O(n*W)
                  │ Overlapping     │ Discrete only  │
                  │ Polynomial time │ High memory    │
──────────────────┼─────────────────┼────────────────┼──────────
Greedy            │ Optimal subst   │ Greedy choice  │ O(nlogn)
                  │ Greedy property │ Complex depend │
                  │ Simple solution │                │
──────────────────┼─────────────────┼────────────────┼──────────
BFS/DFS           │ Shortest path   │ Exponential    │ O(V+E)
                  │ Connected?      │ Not sparse     │
                  │ Graph traversal │                │
──────────────────┼─────────────────┼────────────────┼──────────
Divide & Conquer  │ Independent     │ Overlap        │ O(n*logn)
                  │ Merging easy    │ Merge complex  │
                  │ Balanced splits │                │
```

---

## 🔍 PART 2: COMBINING TECHNIQUES

### Backtracking + Pruning with Heuristics

```
BACKTRACKING + GREEDY HEURISTIC PRUNING
═════════════════════════════════════════════════════

Pattern: Use greedy choice to guide backtracking

Idea:
  1. Build solution with backtracking
  2. At each choice point: try LIKELY good choices first
  3. Use greedy criterion to order choices
  4. Find good solution early → better pruning

EXAMPLE: Sudoku Solver

Pure backtracking:
  Find any empty cell (arbitrary order)
  Try digits 1-9 (arbitrary order)
  
With greedy ordering:
  Find MOST CONSTRAINED empty cell (MCV heuristic)
  Try MOST LIKELY digits (based on row/column/box constraints)
  
Result: Exponentially faster (same actual algorithm, better order)

GENERAL PATTERN:

function BacktrackWithHeuristic(state):
    if IsGoal(state):
        return state
    
    // Different from pure backtracking:
    // Order choices by heuristic
    choices = GetChoices(state)
    choices.sort(by_heuristic)  // GREEDY ORDER
    
    for choice in choices:
        if IsValid(choice, state):
            new_state = ApplyChoice(choice, state)
            result = BacktrackWithHeuristic(new_state)
            if result found:
                return result
    
    return not_found

GREEDY ORDERINGS:
  - Smallest domain first (MCV)
  - Most constrained first
  - Highest benefit first
  - Lowest cost first
```

### Branch & Bound + Dynamic Programming

```
BRANCH & BOUND + DP FOR BOUNDS
═════════════════════════════════════════════════════

Pattern: Use DP to compute tight bounds for B&B

Idea:
  1. B&B framework for optimal search
  2. DP to compute lower/upper bounds tightly
  3. Better bounds → more pruning
  4. Exponentially faster search

EXAMPLE: 0-1 Knapsack with B&B

B&B search:
  Branch: include/exclude each item
  Bound: upper bound on best possible
  
Better bounds via DP:
  For subproblems: DP solves exactly
  Lower bound: take DP solution for remaining capacity
  Upper bound: fractional relaxation
  
Result: Fewer states explored

ADVANTAGE:
  B&B gives optimal solution guarantee
  DP makes B&B practical through tight bounds
  Hybrid combines best of both
```

### Backtracking + Local Search

```
BACKTRACKING + LOCAL SEARCH (Hybrid Heuristic)
═════════════════════════════════════════════════════

Pattern: Combine exact search with heuristic refinement

Idea:
  1. Backtracking finds feasible solution
  2. Local search refines solution locally
  3. Backtracking continues from improved solution
  4. Alternates until convergence

EXAMPLE: TSP (Traveling Salesman)

Pure backtracking:
  Explore all tours systematically
  Find optimal but slow

Backtracking + Local Search:
  1. Backtracking finds initial tour (feasible)
  2. Local search (2-opt) improves tour
  3. Backtrack with updated bound = improved cost
  4. Skip branches exceeding new bound
  5. Find better solutions → better pruning
  6. Backtracking completes with guarantee

HYBRID ADVANTAGES:
  ✓ Faster than pure backtracking (good bounds from LS)
  ✓ Guaranteed optimal (backtracking doesn't stop)
  ✓ Practical for medium instances
```

---

## 🔍 PART 3: MIXED PARADIGM PROBLEMS

### NP-Hard Problem Portfolio

```
MIXED PARADIGM APPLICATIONS
═════════════════════════════════════════════════════

PROBLEM 1: CONSTRAINT SATISFACTION (CSP)
──────────────────────────────────────────

Problem: Assign values to variables satisfying constraints

Example: Sudoku, Graph Coloring, N-Queens

Approach:
  BACKTRACKING: Build assignment incrementally
  HEURISTICS: Most constrained variable (MCV)
  CONSTRAINT PROPAGATION: Eliminate impossible values
  LOCAL SEARCH: Refine solution
  
Result: Hybrid solver is state-of-the-art

PROBLEM 2: INTEGER LINEAR PROGRAMMING (ILP)
────────────────────────────────────────────

Problem: Optimize linear objective subject to linear constraints

Example: Knapsack variants, Scheduling, Network design

Approach:
  BRANCH & BOUND: Systematic search
  LINEAR RELAXATION: DP/LP for bounds
  HEURISTICS: Greedy for initial solution
  CUTTING PLANES: Tighten LP relaxation
  
Result: Solvers like CPLEX use hybrid strategy

PROBLEM 3: TRAVELING SALESMAN (TSP)
───────────────────────────────────

Problem: Find minimum tour visiting all cities

Approaches:
  EXACT: Branch & bound with TSP bounds
  HEURISTIC: Greedy nearest neighbor + local search
  HYBRID: B&B with local search improving bounds
  META-HEURISTIC: Genetic algorithm, simulated annealing
  
Challenge: NP-hard, need practical solutions

PROBLEM 4: GRAPH COLORING
──────────────────────────

Problem: Color vertices with minimum colors, no conflicts

Approach:
  BACKTRACKING: Try assignments incrementally
  GREEDY HEURISTIC: Welsh-Powell (degree-based ordering)
  HEURISTICS: Largest degree first (guides choices)
  LOCAL SEARCH: Swap colors for improvement
  
Challenge: NP-hard, heuristics give practical results
```

### Real-World Hybrid Systems

```
INDUSTRIAL OPTIMIZATION SYSTEMS
═════════════════════════════════════════════════════

CPLEX (IBM):
  BRANCH & CUT: B&B + cutting planes + LP relaxation
  Combines: Integer programming, LP, heuristics, B&B
  Used for: Supply chain, scheduling, logistics
  
GUROBI:
  BRANCH & CUT: Similar to CPLEX
  + Parallel processing
  + Modern heuristics
  Used for: Similar applications

SCIP (Open Source):
  Constraint integer programming
  BRANCH & BOUND: With many enhancements
  Combines: CSP, ILP, heuristics
  Used for: Research and practical problems

MINIZINC:
  Modeling language for CSP
  Compiles to: Multiple solvers
  Uses: Backtracking, constraint propagation, local search
  User: Researchers, industry

COMMON PATTERN:
  1. Model: Express problem formally
  2. Preprocess: Simplify, reformulate
  3. Relax: Compute bounds (LP, Lagrangian)
  4. Search: B&B with heuristics
  5. Improve: Local search on incumbents
  6. Validate: Verify optimality
```

---

## 🔍 PART 4: PROBLEM-SOLVING STRATEGY

### Algorithm Selection Process

```
PROBLEM-SOLVING METHODOLOGY
═════════════════════════════════════════════════════

STEP 1: UNDERSTAND PROBLEM
────────────────────────────
□ Read carefully, understand constraints
□ Identify objective (feasibility, optimization, counting)
□ Estimate search space size
□ Check for special structure

STEP 2: CLASSIFY PROBLEM TYPE
──────────────────────────────
□ Graph problem? (BFS/DFS, MST, shortest path)
□ Optimization problem? (DP, greedy, B&B)
□ Feasibility problem? (CSP, backtracking)
□ NP-hard? (Expect need for heuristics)
□ Polynomial? (Efficient exact algorithm)

STEP 3: ANALYZE STRUCTURE
──────────────────────────
□ Optimal substructure? (DP candidate)
□ Greedy choice property? (Greedy candidate)
□ Constraints prunable? (Backtracking candidate)
□ Bounds computable? (B&B candidate)
□ Independent subproblems? (D&C candidate)

STEP 4: CHOOSE INITIAL APPROACH
────────────────────────────────
□ Clear polynomial solution? Implement it
□ NP-hard, small instance? Try exact (backtracking/B&B)
□ NP-hard, large instance? Try heuristic
□ Unsure? Start with backtracking (general purpose)

STEP 5: OPTIMIZE CHOSEN APPROACH
─────────────────────────────────
□ Add pruning/bounding rules
□ Add heuristics for ordering choices
□ Add preprocessing to simplify
□ Measure performance, identify bottlenecks

STEP 6: HYBRID IF NEEDED
────────────────────────
□ If slow: add heuristics for initial solution
□ If inaccurate: add exact phase after heuristic
□ If branches explode: add smarter bounds
□ If feasible solution expensive: try approximation

STEP 7: VALIDATE
────────────────
□ Test on provided examples
□ Test on edge cases
□ Verify correctness
□ Measure performance (time, memory)
□ Compare to baselines if available
```

### Common Problem Patterns

```
RECOGNITION PATTERNS FOR COMMON PROBLEMS
═════════════════════════════════════════════════════

PATTERN 1: "Find [subset/arrangement] satisfying [constraints]"
  └─ BACKTRACKING: Build incrementally, check constraints
  └─ Example: Subset sum, N-Queens, Sudoku

PATTERN 2: "Find [best] [subset/arrangement] of [objective]"
  └─ BRANCH & BOUND: Systematic search with bounding
  └─ DP: If optimal substructure
  └─ GREEDY: If greedy choice property
  └─ Example: TSP, Knapsack, Scheduling

PATTERN 3: "Count [solutions/arrangements] satisfying [constraints]"
  └─ BACKTRACKING: Count valid solutions
  └─ DP: If counting overlaps possible
  └─ COMBINATORICS: If closed-form formula
  └─ Example: Permutations with constraints

PATTERN 4: "Maximize/minimize [objective] subject to [constraints]"
  └─ BRANCH & BOUND: Guaranteed optimal
  └─ DP: If overlapping subproblems
  └─ GREEDY: If choice property
  └─ HEURISTIC: If too hard or large
  └─ Example: General optimization

PATTERN 5: "[Path/tour/route] from [start] to [goal]"
  └─ BFS/DFS: If unweighted or simple
  └─ Dijkstra: If non-negative weights
  └─ A*: If goal known, heuristic available
  └─ B&B: If optimization on path property
  └─ Example: Routing, navigation, maze

DECISION FLOWCHART:
  
  Is polynomial-time solution known?
    YES → Implement exact algorithm
    NO → Problem likely NP-hard, continue
  
  Is search space small (< 2^20)?
    YES → Try backtracking/B&B
    NO → Continue
  
  Does problem have optimal substructure?
    YES → Try DP
    NO → Continue
  
  Does problem have greedy choice property?
    YES → Try greedy
    NO → Continue
  
  Must find optimal solution?
    YES → Try B&B with heuristics
    NO → Use heuristic/approximation
```

---

## 🎯 DAY 5 SUMMARY

### What You Learned

```
✓ Paradigm selection framework
✓ When to use backtracking vs B&B vs DP vs greedy
✓ Combining backtracking with greedy heuristics
✓ Combining B&B with DP bounds
✓ Backtracking with local search hybrid
✓ CSP solving strategies
✓ Integer linear programming approaches
✓ Real-world optimization systems (CPLEX, Gurobi)
✓ Problem classification and pattern recognition
✓ Complete problem-solving methodology
```

### Key Principles

```
PARADIGM COMBINATION PRINCIPLES:

1. UNDERSTANDING FIRST
   Know the problem deeply before choosing algorithm
   Right algorithm only comes from understanding

2. HYBRID WHEN NEEDED
   No single best algorithm for all problems
   Often best solution combines multiple techniques
   Exact + heuristic, theoretical + practical

3. RECOGNITION MATTERS
   Pattern recognition accelerates problem-solving
   Common problems have established approaches
   Learn patterns from existing solutions

4. MEASURE & OPTIMIZE
   Theory is starting point, measurement confirms
   Profile code, identify bottlenecks
   Optimize what matters

5. COMPLEXITY AWARENESS
   P vs NP-hard changes approach fundamentally
   Small vs large instances need different strategies
   Memory vs time tradeoffs

6. PRACTICAL THINKING
   Sometimes near-optimal fast beats optimal slow
   Sometimes approximation sufficient
   Know when to use heuristics vs exact

7. CONTINUOUS IMPROVEMENT
   Start simple, optimize incrementally
   Don't over-engineer initially
   Measure before and after improvements
```

---

# 🏆 WEEK 13 COMPLETE SUMMARY

## 📊 Topics Covered

```
DAY 1: BACKTRACKING FUNDAMENTALS
  ✓ What is backtracking and when to use
  ✓ State space trees and DFS
  ✓ Pruning strategies (5 types)
  ✓ Generic backtracking template
  ✓ Constraint checking
  ✓ Feasibility vs optimality

DAY 2: COMPLEX BACKTRACKING
  ✓ N-Queens problem formulation
  ✓ Sudoku solver with optimization heuristics
  ✓ Permutations and combinations
  ✓ Word search in grid
  ✓ Maze solving with path tracking
  ✓ Visited marking and backtracking patterns

DAY 3: BRANCH & BOUND
  ✓ Branch & bound framework (B, B, prune)
  ✓ Optimal solution finding
  ✓ Upper and lower bounds
  ✓ Bounding techniques for TSP/Knapsack
  ✓ Best-first search strategy
  ✓ Priority queue state ordering
  ✓ Pruning with bounds

DAY 4: AMORTIZED ANALYSIS
  ✓ Amortized complexity concept
  ✓ Aggregate method (simple average)
  ✓ Accounting method (credit/debit)
  ✓ Potential method (potential function)
  ✓ Dynamic arrays analysis (all 3 methods)
  ✓ Binary counters
  ✓ Splay trees and Fibonacci heaps
  ✓ Self-adjusting structures

DAY 5: INTEGRATION & MIXED PARADIGMS
  ✓ Paradigm selection framework
  ✓ When to use which algorithm
  ✓ Combining backtracking + greedy
  ✓ Combining B&B + DP
  ✓ Backtracking + local search hybrid
  ✓ CSP, ILP, TSP approaches
  ✓ Industrial optimization systems
  ✓ Complete problem-solving methodology
  ✓ Pattern recognition for problems
```

## 🎯 Key Insights

```
INSIGHT 1: BACKTRACKING IS GENERAL PURPOSE
  - Works for feasibility and all-solutions problems
  - Requires constraint checking and pruning
  - State space tree representation
  - Exponential worst case but pruning helps

INSIGHT 2: BRANCH & BOUND FOR OPTIMIZATION
  - Systematic search with optimality guarantee
  - Upper and lower bounds enable pruning
  - Better bounds = more pruning
  - Best-first search often better than DFS

INSIGHT 3: AMORTIZED ANALYSIS EXPLAINS EFFICIENCY
  - Expensive operations infrequent
  - Cost amortized over cheap operations
  - Three methods for rigorous analysis
  - Justifies practical data structures

INSIGHT 4: COMBINING PARADIGMS IS KEY
  - No single best algorithm for all problems
  - Hybrid approaches combine strengths
  - Backtracking + heuristics practical
  - B&B + DP gives tight bounds

INSIGHT 5: PROBLEM UNDERSTANDING FIRST
  - Right algorithm comes from understanding structure
  - Pattern recognition accelerates selection
  - Classify problem type correctly
  - Different problem types need different approaches

INSIGHT 6: PRACTICAL ALGORITHMS MATTER
  - Theory is foundation, practice is application
  - Real systems combine many techniques
  - Measurement guides optimization
  - Sometimes approximate answer fast beats exact slow

INSIGHT 7: HEURISTICS MAKE THINGS PRACTICAL
  - NP-hard problems need heuristics for large instances
  - Good heuristics improve performance dramatically
  - Heuristic ordering guides search
  - Local search refines solutions
```

## 💡 Algorithm Selection Quick Reference

```
PROBLEM TYPE         │ BEST APPROACH         │ COMPLEXITY
─────────────────────┼──────────────────────┼─────────────
Feasibility (CSP)    │ Backtracking + MCV   │ O(2^n)
All solutions (enum) │ Backtracking         │ O(n!)
Optimization         │ B&B or DP            │ O(2^n) or O(n*W)
Shortest path        │ Dijkstra, A*         │ O(n log n)
Minimum spanning tree│ Kruskal, Prim        │ O(n log n)
Sorting              │ QuickSort, MergeSort │ O(n log n)
Searching            │ Binary search, hash  │ O(log n), O(1)
String matching      │ KMP, Boyer-Moore     │ O(n+m)
Pattern matching     │ Regex, Trie          │ Varies
DP subproblems       │ Memoization, tables  │ O(subproblems)
Linear system        │ Gaussian elimination │ O(n³)
Linear program       │ Simplex              │ O(n³) avg
```

---

# 🎓 WEEK 13 LEARNING OUTCOMES

By end of Week 13, you should be able to:

### Conceptual Mastery
```
✓ Explain backtracking algorithm and when useful
✓ Describe state space trees and DFS traversal
✓ Identify pruning opportunities in problems
✓ Understand branch & bound optimization
✓ Explain upper/lower bounds and pruning
✓ Define amortized complexity precisely
✓ Apply three amortized analysis methods
✓ Recognize when paradigms should combine
✓ Classify problems by type and structure
```

### Problem-Solving Skills
```
✓ Solve constraint satisfaction problems (backtracking)
✓ Find optimal solutions (branch & bound)
✓ Analyze amortized complexity of algorithms
✓ Choose appropriate algorithm paradigm
✓ Design efficient pruning strategies
✓ Combine multiple algorithmic paradigms
✓ Optimize practical algorithm implementations
✓ Recognize common problem patterns
✓ Design hybrid algorithms for hard problems
```

### Specific Algorithms
```
✓ Backtracking template and applications
✓ N-Queens and Sudoku solvers
✓ Permutation and combination generation
✓ Branch & bound for TSP and knapsack
✓ Best-first search with priority queues
✓ Aggregate, accounting, and potential methods
✓ Dynamic array amortized analysis
✓ Splay tree and Fibonacci heap concepts
✓ Constraint satisfaction problem solving
```

### Transfer Skills
```
✓ Identify problem structure quickly
✓ Apply algorithm templates to new problems
✓ Design heuristics for algorithm guidance
✓ Combine algorithms for better performance
✓ Analyze and prove algorithm correctness
✓ Measure and optimize implementation
✓ Recognize NP-hard problems
✓ Use approximation when exact too slow
✓ Communicate algorithm choices
```

---

**Status: WEEK 13 COMPLETE**

**PHASE D (Weeks 12-13) COMPLETE: ALGORITHM PARADIGMS MASTERED**

**50 HOURS OF CONTENT DELIVERED ACROSS 10 DAYS**
