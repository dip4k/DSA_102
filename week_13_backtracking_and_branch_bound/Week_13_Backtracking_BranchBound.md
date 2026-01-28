# 🟧 WEEK 13: BACKTRACKING & BRANCH & BOUND

**Duration:** 5 days | 450 minutes | Advanced Algorithm Paradigms  
**Focus:** Backtracking for combinatorial problems, Branch & Bound for optimization, Amortized analysis  
**Prerequisites:** Recursion, DFS, greedy algorithms, complexity analysis  

---

## 🎯 WEEK 13 LEARNING OUTCOMES

By end of Week 13, you will be able to:

1. **Design backtracking algorithms** with proper pruning strategies
2. **Solve N-Queens, Sudoku, Maze** problems systematically
3. **Apply Branch & Bound** to optimization problems
4. **Analyze amortized complexity** using accounting and potential methods
5. **Combine paradigms** (greedy + backtracking, DP + B&B)
6. **Recognize when to use** each algorithmic paradigm

---

---

## 📅 DAY 1: BACKTRACKING FUNDAMENTALS

### ⏱️ Duration: 90 minutes

---

## 🎓 CONCEPT: What is Backtracking?

### Core Idea

**Backtracking:** Build solution incrementally, one piece at a time. If current piece leads to invalid state, undo it and try alternative.

Think of it like solving a maze:

```
Exploring a Maze:

START ○
  │
  ├─→ Dead end ✗ → BACKTRACK
  │
  ├─→ Path continues
  │    │
  │    ├─→ Dead end ✗ → BACKTRACK
  │    │
  │    └─→ Path continues
  │         │
  │         └─→ GOAL! ○ SUCCESS

Key: When stuck, undo recent choices and try alternatives
```

### Relationship to DFS

```
BACKTRACKING = DFS on Solution Space Tree

Example: Permutations of [1, 2, 3]

Solution Tree:
                    root (empty)
           /          |          \
         [1]        [2]         [3]
        /  \        /  \        /  \
      [1,2] [1,3] [2,1] [2,3] [3,1] [3,2]
       |     |     |     |     |     |
     [1,2,3][1,3,2][2,1,3][2,3,1][3,1,2][3,2,1]

DFS traversal visits all leaves = all permutations
Backtracking naturally prunes invalid branches
```

---

## 🎓 CONCEPT: State Space vs Solution Space

### Two Different Views

```
STATE SPACE: All possible problem states

Example: N-Queens board
State = current board configuration (with some queens placed)

Solution Space: Only valid final states

Example: N-Queens solution
Solution = board with N queens placed, no conflicts

┌──────────────────────────────────────┐
│ Solution Space Tree:                 │
│                                      │
│ Root: Empty board (0 queens)         │
│   ├─ Queen at row 1, col 1          │
│   │   ├─ Queen at row 2, col 3      │
│   │   │   ├─ Queen at row 3, col 5  │
│   │   │   │   ...                    │
│   │   │   └─ Conflict → Prune        │
│   │   └─ Queen at row 2, col 4      │
│   │       ...                        │
│   └─ Queen at row 1, col 2          │
│       ...                            │
│                                      │
│ Leaves: Valid complete solutions    │
└──────────────────────────────────────┘
```

---

## 🎓 CONCEPT: Backtracking Template

### Generic Structure

```
BACKTRACKING_TEMPLATE:

┌────────────────────────────────────────┐
│ BACKTRACK(state, remaining choices):   │
│                                        │
│  1. IF state is complete solution:    │
│     Record solution                    │
│     Return                             │
│                                        │
│  2. IF state is invalid:               │
│     Return (prune this branch)         │
│                                        │
│  3. FOR each possible next choice:     │
│     a) Add choice to state             │
│     b) Recursively explore            │
│     c) Remove choice from state        │
│        (backtrack)                     │
│                                        │
└────────────────────────────────────────┘
```

### Key Components

```
1. STATE: Current partial solution
   - What constraints are satisfied?
   - What variables assigned?
   - What's left to assign?

2. CHOICES: Possible next decisions
   - What values can next variable take?
   - Are some choices prunable?

3. CONSTRAINTS: Validity rules
   - Which partial states are invalid?
   - When to prune?

4. BASE CASE: Complete solution
   - When is state complete?
   - Did we reach goal?
```

---

## 🎓 CONCEPT: Pruning (Critical for Efficiency)

### Why Pruning Matters

```
WITHOUT PRUNING:
Exponential search space explored completely
→ Very slow for large n

Example: Permutations of n elements
Without pruning: Explore all n! paths
With pruning: Skip invalid branches early

Speedup: Can be dramatic (100x+ for reasonable n)

┌──────────────────────────────────────┐
│ Pruning Strategy:                    │
│                                      │
│ ✗ = Invalid state                    │
│                                      │
│         root                         │
│        / | \                         │
│       A  B  C                        │
│      / \ ✗  / \                      │
│     AA AB   CA CB                    │
│    /| ✗      ✗  ✗                    │
│   AAA AAB    (pruned!)               │
│                                      │
│ Without pruning: Explore all        │
│ With pruning: Stop at ✗             │
└──────────────────────────────────────┘
```

### Types of Pruning

```
1. VALIDITY PRUNING: Constraint violation
   "This partial solution violates constraint"
   Example: Two queens on same row in N-Queens
   
   if (isInvalid(current_state)):
       return  // Skip this entire branch

2. OPTIMALITY PRUNING: Lower/upper bounds (Branch & Bound)
   "This branch can't possibly beat current best"
   Example: If partial value < best found, skip
   
   if (lowerBound(partial) < bestSoFar):
       return  // Skip this branch

3. REDUNDANCY PRUNING: Equivalent states
   "This state is equivalent to one we've seen"
   Example: Using symmetry
   
   if (isRedundant(state)):
       return  // Skip this branch
```

---

## 📊 Visual: Backtracking Execution Tree

```
DEPTH-FIRST TRAVERSAL with BACKTRACKING:

DFS Order (with pruning):

                root
              /  |  \
             1   2   3
           / \ / \ / \
          a  b c d e f

Visit: root → 1 → a → (leaf)
       back to: 1 → b → (leaf)
       back to: 1 → (done with 1)
       back to: root → 2 → c (pruned, skip d)
       back to: 2 → (done with 2)
       back to: root → 3 → e → (leaf)
       back to: 3 → f → (leaf)

Each branch that's pruned
saves exploring subtree
```

---

## 🔑 KEY INSIGHTS FOR DAY 1

| Concept | Key Point |
|---------|-----------|
| **Backtracking** | Build solution incrementally, undo when stuck |
| **Solution Tree** | DFS on state space tree |
| **State** | Current partial solution |
| **Base Case** | Solution found or invalid |
| **Pruning** | Skip entire subtree if invalid |
| **Efficiency** | Pruning can reduce exponential to manageable |

---

---

## 📅 DAY 2: BACKTRACKING PROBLEMS

### ⏱️ Duration: 120 minutes

---

## 🎓 CONCEPT: N-Queens Problem

### Problem Statement

```
N-QUEENS PROBLEM:

Place n queens on n × n chessboard such that:
- No two queens on same row
- No two queens on same column
- No two queens on same diagonal

Example (4-Queens):
   0 1 2 3
0  . Q . .
1  . . . Q
2  Q . . .
3  . . Q .

No conflicts! ✓

Goal: Find all valid placements (or one solution)
```

### Solution Representation

```
APPROACH: Place one queen per row

State = [col₀, col₁, col₂, ..., col_{n-1}]
where colᵢ = column of queen in row i

Example (4-Queens):
State = [1, 3, 0, 2]
Meaning:
  Row 0: Queen at column 1
  Row 1: Queen at column 3
  Row 2: Queen at column 0
  Row 3: Queen at column 2

Why one per row?
- Guarantees no row conflicts (by design)
- Reduces search space from n² positions to n^n states
  (instead of n^(n²))
```

### Algorithm

```
N_QUEENS(n):
  solutions = []
  state = []
  
  BACKTRACK(0, state, solutions):
    1. If placed n queens (state length = n):
       Record state as solution
       Return
    
    2. For each column 0 to n-1:
       a) Is this column safe (no conflicts)?
          - Check column conflicts
          - Check diagonal conflicts
       
       b) If safe:
          - Place queen at (current_row, column)
          - Add column to state
          - Recursively solve for next row
          - Remove column from state (backtrack)
       
       c) If not safe:
          - Skip to next column

  Call: BACKTRACK(0, [], solutions)
```

### Step-by-Step Example (4-Queens)

```
Initial: Row 0, State = []

Row 0:
  Try Col 0: Safe? Yes → State = [0]
    
    Row 1, State = [0]:
      Try Col 0: Same column as queen above → Prune
      Try Col 1: Adjacent diagonal → Prune
      Try Col 2: Safe? Yes → State = [0, 2]
      
        Row 2, State = [0, 2]:
          Try Col 0: Same column → Prune
          Try Col 1: Safe? Yes → State = [0, 2, 1]
          
            Row 3, State = [0, 2, 1]:
              Try Col 0: Same column → Prune
              Try Col 1: Same column → Prune
              Try Col 2: Same column → Prune
              Try Col 3: Safe? Yes → State = [0, 2, 1, 3]
              
                Row 4 = n, solution found! ✓
                But wait, let's verify [0,2,1,3]:
                
                Visual:
                   0 1 2 3
                0  Q . . .
                1  . . Q .
                2  . Q . .
                3  . . . Q
                
                Check conflicts:
                Row conflicts: None (one per row) ✓
                Column conflicts: None (all different) ✓
                Diagonal conflicts: None ✓
                
                SOLUTION #1: [0, 2, 1, 3]
              
              Backtrack from [0, 2, 1, 3]
              
          Backtrack from [0, 2, 1]
          Try Col 2, 3, ... (more attempts)
      
      Backtrack from [0, 2]
      Try Col 3: Safe? Yes → State = [0, 3]
      ... (continue exploring)
  
  Backtrack from [0]
  Try Col 1, 2, 3: ... (more attempts)

Continue until all possibilities exhausted
```

### Conflict Detection

```
DIAGONAL CONFLICT CHECK:

Two queens conflict if they're on same diagonal:
- Main diagonal: row - col = constant
- Anti-diagonal: row + col = constant

For queen at (row1, col1) and (row2, col2):
if (row1 - col1 == row2 - col2):
    Same main diagonal conflict ✗

if (row1 + col1 == row2 + col2):
    Same anti-diagonal conflict ✗

Example: Queen at (0, 0) and queen at (2, 2)
  row - col: 0 - 0 = 0, 2 - 2 = 0 → Same! Conflict ✗
  
Example: Queen at (0, 1) and queen at (2, 3)
  row - col: 0 - 1 = -1, 2 - 3 = -1 → Same! Conflict ✗
  row + col: 0 + 1 = 1, 2 + 3 = 5 → Different → OK
```

---

## 🎓 CONCEPT: Sudoku Solver

### Problem Structure

```
SUDOKU PUZZLE:

9 × 9 grid, divided into 3 × 3 boxes

Constraints:
- Each row: digits 1-9 exactly once
- Each column: digits 1-9 exactly once
- Each 3×3 box: digits 1-9 exactly once

┌───┬───┬───┐
│5 3│   │   │
│6  │ 9 │ 8 │
│  9│   │   │
├───┼───┼───┤
│   │6  │   │
│ 4 │ 8 │ 3 │
│ 7 │   │ 6 │
├───┼───┼───┤
│   │   │6  │
│ 3 │ 9 │ 4 │
│   │   │   │
└───┴───┴───┘

Goal: Fill empty cells to satisfy constraints
```

### Backtracking Approach

```
SUDOKU_SOLVE():

1. Find first empty cell (row, col)
2. If no empty cell → Puzzle solved! Return true
3. For each digit 1-9:
   a) Is digit valid at (row, col)?
      - Check row: digit not already present
      - Check column: digit not already present
      - Check 3×3 box: digit not already present
   
   b) If valid:
      - Place digit
      - Recursively solve rest
      - If recursive call succeeds → Return true
      - If recursive call fails → Remove digit (backtrack)
   
   c) If not valid:
      - Skip this digit

4. If all digits tried and none work → Return false
```

### Key Optimization: Constraint Propagation

```
NAIVE BACKTRACKING: Try all 1-9 for each cell
- Explores many dead ends

SMARTER APPROACH: Constraint Propagation
- Before trying numbers, reduce possibilities
- Cell can only have digits that don't violate constraints

Example:
Cell (0,0) already has 5, 6, 9 in its row
Can only try: 1, 2, 3, 4, 7, 8
Reduces from 9 tries to 6 tries ✓

With column and box constraints:
Further reduces possibilities
Some cells may have only 1 valid choice!
```

---

## 🎓 CONCEPT: Permutations & Combinations

### Permutations (Order Matters)

```
PERMUTATION BACKTRACKING:

Generate all orderings of a set

Example: Permutations of [1, 2, 3]
Result: [1,2,3], [1,3,2], [2,1,3], [2,3,1], [3,1,2], [3,2,1]

┌──────────────────────────────────────┐
│ PERMUTE(available, current):         │
│                                      │
│ 1. If available is empty:            │
│    Record current as permutation     │
│    Return                            │
│                                      │
│ 2. For each element in available:    │
│    a) Add element to current         │
│    b) Remove element from available  │
│    c) Recursively permute            │
│    d) Restore element to available   │
│                                      │
└──────────────────────────────────────┘

Example execution:
Start: available=[1,2,3], current=[]
  Pick 1: available=[2,3], current=[1]
    Pick 2: available=[3], current=[1,2]
      Pick 3: available=[], current=[1,2,3] → Solution!
    Pick 3: available=[2], current=[1,3]
      Pick 2: available=[], current=[1,3,2] → Solution!
  Pick 2: available=[1,3], current=[2]
    ... (similar)
  Pick 3: available=[1,2], current=[3]
    ... (similar)
```

### Combinations (Order Doesn't Matter)

```
COMBINATION BACKTRACKING:

Generate all subsets of size k

Example: Combinations of [1,2,3,4] choose 2
Result: [1,2], [1,3], [1,4], [2,3], [2,4], [3,4]

Key Difference from Permutations:
- Avoid duplicates by only considering elements after current

┌──────────────────────────────────────┐
│ COMBINE(start_idx, remaining, curr): │
│                                      │
│ 1. If remaining == 0:                │
│    Record current as combination     │
│    Return                            │
│                                      │
│ 2. For i = start_idx to n-1:         │
│    a) Add array[i] to current        │
│    b) Recursively combine from i+1   │
│    c) Remove array[i] from current   │
│                                      │
└──────────────────────────────────────┘

Example execution:
Start: idx=0, remaining=2, current=[]
  Pick 1 (idx=0): current=[1], remaining=1
    Pick 2 (idx=1): current=[1,2], remaining=0 → Solution!
    Pick 3 (idx=2): current=[1,3], remaining=0 → Solution!
    Pick 4 (idx=3): current=[1,4], remaining=0 → Solution!
  Pick 2 (idx=1): current=[2], remaining=1
    Pick 3 (idx=2): current=[2,3], remaining=0 → Solution!
    Pick 4 (idx=3): current=[2,4], remaining=0 → Solution!
  Pick 3 (idx=2): current=[3], remaining=1
    Pick 4 (idx=3): current=[3,4], remaining=0 → Solution!
```

---

## 🎓 CONCEPT: Word Search & Maze Problems

### Word Search

```
WORD SEARCH PROBLEM:

Find if a word exists in grid
- Horizontally, vertically, or diagonally
- Adjacent cells only (no jumping)
- Each cell used at most once per word search

Visual:
┌───┬───┬───┐
│C A T│
│O D R│
│G E D│
└───┴───┴───┘

Search: "CAT"
Path: C(0,0) → A(0,1) → T(0,2) → Found! ✓

Search: "CODE"
Path: C(0,0) → O(1,0) → D(1,1) → E(2,1) → Found! ✓

Search: "CAD"
Path: C(0,0) → A(0,1) → D(1,1) → Found! ✓

├──────────────────────────────────────┐
│ WORD_SEARCH(board, word):            │
│                                      │
│ 1. For each cell in board:           │
│    IF cell matches word[0]:          │
│      Start DFS from this cell        │
│      Try to find rest of word        │
│                                      │
│ DFS(row, col, word_index, visited):  │
│   1. If word_index == word.length:   │
│      Found entire word! Return true  │
│   2. For each adjacent cell:         │
│      IF not visited AND              │
│         cell == word[word_index]:    │
│        Mark as visited               │
│        Recursive DFS from adjacent   │
│        Unmark as visited (backtrack) │
│   3. If none work → Return false     │
│                                      │
└──────────────────────────────────────┘
```

### Maze Solving

```
MAZE SOLVING:

Find path from START to EXIT

Visual:
┌─────────────┐
│S . # . . E│  S = Start
│# . # . #│  E = Exit
│. . . . . .│  . = Path
│# # . # # #│  # = Wall
│. . . . . .│
└─────────────┘

One solution:
S → → ↓ → → E
    ↓     ↓
    → → → ↓
        ↓
    → → → E

┌──────────────────────────────────────┐
│ SOLVE_MAZE(start, exit):             │
│                                      │
│ DFS(row, col, visited):              │
│   1. If (row, col) == exit:          │
│      Found solution! Return true     │
│   2. If out of bounds or wall:       │
│      Return false                    │
│   3. If already visited:             │
│      Return false                    │
│   4. Mark (row, col) as visited      │
│   5. Try all 4 directions:           │
│      UP, DOWN, LEFT, RIGHT           │
│      If any succeeds → Return true   │
│   6. Unmark (row, col) (backtrack)   │
│   7. Return false                    │
│                                      │
└──────────────────────────────────────┘
```

---

## 🔑 KEY INSIGHTS FOR DAY 2

| Concept | Key Point |
|---------|-----------|
| **N-Queens** | Place one queen per row, check conflicts |
| **Sudoku** | Fill cells respecting 3 constraints |
| **Permutations** | Order matters, generate all orderings |
| **Combinations** | Order doesn't matter, start_idx prevents duplicates |
| **Word Search** | DFS with visited tracking |
| **Maze** | Find path avoiding walls, backtrack on dead ends |
| **Optimization** | Constraint propagation reduces search space |

---

---

## 📅 DAY 3: BRANCH & BOUND

### ⏱️ Duration: 120 minutes

---

## 🎓 CONCEPT: Branch & Bound Fundamentals

### Core Idea

```
BRANCH & BOUND:

Systematic search with pruning based on bounds

Key Components:
1. BRANCH: Partition problem into subproblems
2. BOUND: Compute upper/lower bound for subproblem
3. PRUNE: Skip subproblems that can't improve best

Why?
Backtracking prunes when state is invalid.
Branch & Bound prunes when state can't be optimal.
More aggressive pruning → faster!

Visual:

Search Tree:
                 root
                / | \
               /  |  \
              a   b   c
            / \   |  / \
           d   e  f g   h

Without B&B:
Explore all branches (if valid)

With B&B:
- Compute bound for 'a'
- If bound(a) < best_found → Skip entire subtree
- Continue only for promising branches
```

### Bounding Functions

```
UPPER BOUND: Best possible solution in subproblem

Example (TSP): 
Current partial tour has cost C.
Remaining cities must be visited.
Upper bound = C + minimum_spanning_tree_of_remaining

Why upper bound?
- If it's less than best found, we can skip
- No way to achieve better value

LOWER BOUND: Worst possible solution in subproblem

Example (Knapsack):
Current partial value is V.
Can we add more items?
Lower bound = V + (feasible additional value)

Why lower bound?
- If it's better than best found, might be worth exploring
```

### Best-First Search

```
BEST_FIRST_SEARCH for Branch & Bound:

Use PRIORITY QUEUE ordered by bound

Algorithm:
1. Add root to priority queue
2. While queue not empty:
   a) Pop node with best bound
   b) If solution found → Return
   c) If bound < best_found → Prune
   d) Otherwise, branch into children
   e) Add children to queue

Why better than DFS?
- Explores most promising nodes first
- Often finds good solutions early
- Can prune more subtrees
```

---

## 🎓 CONCEPT: Branch & Bound for TSP

### Traveling Salesman Problem

```
TRAVELING SALESMAN PROBLEM (TSP):

Given n cities with distances between all pairs.
Find shortest tour visiting all cities exactly once
and returning to start.

NP-hard! No known polynomial algorithm.
Branch & Bound can find optimal for small-medium n.

Example (4 cities):
      1   2   3   4
   ┌────┬────┬────┬────┐
1  │ 0  │ 10 │ 15 │ 20 │
2  │ 10 │ 0  │ 35 │ 25 │
3  │ 15 │ 35 │ 0  │ 30 │
4  │ 20 │ 25 │ 30 │ 0  │
└────┴────┴────┴────┘

Possible tours:
[1,2,3,4,1]: 10+35+30+20 = 95
[1,2,4,3,1]: 10+25+30+15 = 80 ← Optimal
[1,3,2,4,1]: 15+35+25+20 = 95
[1,3,4,2,1]: 15+30+25+10 = 80 ← Optimal
...

There are (n-1)!/2 = 3 tours (for n=4)
For n=20: Over 60 quadrillion tours!
Branch & Bound needed to find optimal.
```

### Branch & Bound for TSP

```
APPROACH:

1. Start with partial tour (just starting city)

2. For each unvisited city:
   a) Extend tour to include that city
   b) Compute lower bound on tour length
      Lower bound = (partial cost) + (MST of remaining)
   c) If lower bound < best_tour found → Explore
   d) Otherwise → Prune

3. When all cities visited → Compare with best_tour

Example (starting at city 1):

Step 1: Tour = [1]
        Lower bound = 0 + MST{2,3,4}
        MST{2,3,4} minimum distance to visit all = ?
        Via 2→3→4: 35+30 = 65
        Via 2→4→3: 25+30 = 55
        Via 3→2→4: 35+25 = 60
        Minimum MST = 55
        Lower bound = 0 + 55 = 55

Step 2: Try extending to each city:
        Tour = [1,2]
        Cost so far = 10
        Lower bound = 10 + MST{3,4} + (return to 1)
        Need to visit 3, 4, and return
        MST{3,4} = 30 (direct 3-4)
        Return distance: min(3→1, 4→1) = min(15, 20) = 15
        Lower bound = 10 + 30 + 15 = 55

        Tour = [1,3]
        Cost so far = 15
        Lower bound = 15 + MST{2,4} + (return)
        MST{2,4} = 25 (direct 2-4)
        Return = min(2→1, 4→1) = min(10, 20) = 10
        Lower bound = 15 + 25 + 10 = 50

        Tour = [1,4]
        Cost so far = 20
        Lower bound = 20 + MST{2,3} + (return)
        MST{2,3} = 35 (direct 2-3)
        Return = min(2→1, 3→1) = min(10, 15) = 10
        Lower bound = 20 + 35 + 10 = 65

Step 3: Best lower bound is [1,3] with 50
        Explore that first (best-first search)
        Continue...
```

---

## 🎓 CONCEPT: Branch & Bound for 0/1 Knapsack

### Setup

```
0/1 KNAPSACK WITH BRANCH & BOUND:

Items with weights and values.
Maximize value subject to weight ≤ W.

Key Insight:
UPPER BOUND = Current value + (Fractional Knapsack of remaining)

Why?
- Fractional knapsack is optimal for continuous problem
- 0/1 can't do better than fractional
- So fractional gives an upper bound

If upper bound < best_found → Can prune
```

### Algorithm

```
KNAPSACK_B&B(items, capacity):

1. Sort items by value/weight ratio (descending)

2. best_value = 0
   best_selection = none

3. DFS_BRANCH_AND_BOUND(item_idx, current_weight, 
                         current_value, remaining_capacity):
   
   a) If item_idx == n:
      If current_value > best_value:
         Update best_value and best_selection
      Return

   b) Compute upper bound:
      upper_bound = current_value + 
                    fractional_knapsack(item_idx, 
                                       remaining_capacity)

   c) If upper_bound ≤ best_value:
      Prune this branch (can't improve)
      Return

   d) Try including current item:
      If weight[item_idx] ≤ remaining_capacity:
         Include it
         Recursively solve
         Exclude it (backtrack)

   e) Try excluding current item:
      Don't include it
      Recursively solve next item

Example execution:

Items sorted by ratio:
A: w=2, v=12, ratio=6
B: w=4, v=20, ratio=5  
C: w=6, v=18, ratio=3
D: w=3, v=8, ratio=2.67

Capacity: 10

[Try including A]:
  Current: w=2, v=12, remaining=8
  Upper bound = 12 + fractional(B,C,D with 8 units)
              = 12 + 20 + (4/6)*18
              = 12 + 20 + 12 = 44
  44 > best_found (0) → Explore
  
  [Try including B]:
    Current: w=6, v=32, remaining=4
    Upper bound = 32 + (4/6)*18 = 32 + 12 = 44
    44 > best_found → Explore
    
    [Try including C]:
      w=12 > capacity → Can't include
      
    [Try including D]:
      Current: w=9, v=40, remaining=1
      Upper bound = 40 + 0 = 40
      40 > best_found → Explore
      
      [Try including nothing else]:
        All items considered
        Value = 40
        Best_found = 40
        
    [Done exploring from B]
  
  [Try excluding B]:
    Current: w=2, v=12, remaining=8
    Upper bound = 12 + fractional(C,D with 8)
                = 12 + (6/6)*18 + (2/3)*8
                = 12 + 18 + 5.33 = 35.33
    35.33 < 40 → Prune!

Final: Best value = 40 (items A + B + partial D)
```

---

## 🔑 KEY INSIGHTS FOR DAY 3

| Concept | Key Point |
|---------|-----------|
| **Branch & Bound** | Systematically search with bounds |
| **Upper Bound** | Best possible value in subproblem |
| **Pruning** | Skip if bound worse than best found |
| **Best-First** | Use priority queue for promising nodes |
| **TSP Application** | MST as lower bound |
| **Knapsack** | Fractional as upper bound |
| **Efficiency** | Often faster than backtracking |

---

---

## 📅 DAY 4: AMORTIZED ANALYSIS

### ⏱️ Duration: 120 minutes

---

## 🎓 CONCEPT: Amortized Complexity

### Why Amortized Analysis?

```
PROBLEM: Single operation can be expensive

Example: Dynamic Array

Append to array:
┌─────────────────────────┐
│ Usually O(1)            │
│ But sometimes O(n) when │
│ array is full and needs  │
│ to resize               │
└─────────────────────────┘

How to analyze?
- Can't just say "O(n)" (pessimistic)
- Most operations are O(1)
- Occasional O(n) resize

SOLUTION: Amortized analysis
Average cost over sequence of operations
Even with expensive operations, amortized is O(1)
```

### Accounting Method

```
INTUITION: "Savings Account" for future work

Each operation gets assigned a cost (amortized cost)
Some cost is used immediately
Some cost is "saved" for later
Account balance never goes negative

Example: Append to dynamic array

Initial array: [a, b, c, d] (size 4)

Operation 1: Append(e) [size 4, count 5 - OVERFLOW]
Actual cost: 1 + 4 (copy) = 5 operations
Assigned amortized cost: 3
  Used immediately: 1 (append)
  Saved: 2 (toward future resize)
Account: +2

Operation 2: Append(f) [size 8, count 6]
Actual cost: 1 (just append)
Assigned amortized cost: 3
  Used immediately: 1 (append)
  Saved: 2
Account: +2 + 2 = 4

Operation 3: Append(g) [size 8, count 7]
Actual cost: 1
Assigned amortized cost: 3
Account: +4 + 2 = 6

Later operations...
Account grows as we save for potential resize.
When next resize happens:
Actual cost: 7 + 8 (copy) = 15 operations
Account: Sufficient to cover!
```

### Potential Method

```
DEFINITION:

Amortized cost = Actual cost + Change in potential

Where potential is function of data structure state.

Example: Dynamic array with doubling

Potential function: Φ = 2 × (number of elements) - (array size)

Append when not full:
Actual cost = 1
Change in potential = 2 × 1 - 0 = 2
Amortized = 1 + 2 = 3

Append when full (resize):
Actual cost = n + 1 (copy n elements + 1 append)
Change in potential = 2×(n+1) - 2n = 2
Amortized = (n+1) + 2 = n+3 ??? Wrong!

Wait, let me recalculate...
Before: n elements, size n
        Potential = 2n - n = n

After: n+1 elements, size 2n
       Potential = 2(n+1) - 2n = 2n + 2 - 2n = 2

Change = 2 - n = 2 - n (negative!)

Amortized = (n+1) + (2-n) = 3

Interesting! Expensive resize has amortized cost 3!
Because potential was high before, decreases after.
```

---

## 🎓 CONCEPT: Amortized Analysis of Dynamic Arrays

### Doubling Strategy Analysis

```
DOUBLING STRATEGY:

When array full:
- Create new array of double size
- Copy all elements
- Append new element

Operations sequence:

Append 1: array = [a], size = 1
Append 2: array = [a,b], size = 2  
Append 3: RESIZE! array = [a,b,c,_], size = 4
          Actual cost: 2 (copy) + 1 (append) = 3
Append 4: array = [a,b,c,d], size = 4
Append 5: RESIZE! array = [a,b,c,d,e,_,_,_], size = 8
          Actual cost: 4 (copy) + 1 (append) = 5
Append 6: array = [...,f,...], size = 8
...
Append 9: RESIZE! array = [...,_,...], size = 16
          Actual cost: 8 (copy) + 1 (append) = 9

Total cost for n appends:
= 1 + 1 + 3 + 1 + 5 + 1 + 1 + 1 + 9 + ...
= n + (cost of all resizes)

Cost of resizes:
When array size goes from k to 2k:
Cost = k

Resizes happen at: size 1→2, 2→4, 4→8, ..., 2^k→2^(k+1)
Total resize cost = 1 + 2 + 4 + 8 + ... + 2^k
                  = 2^(k+1) - 1

For n elements:
k ≈ log n
Total cost ≈ n + 2n - 1 = 3n - 1

Average per operation = (3n - 1) / n ≈ 3 = O(1) ✓

AMORTIZED COST: O(1) per append
```

### Accounting Method Proof

```
THEOREM: Amortized cost of append is O(1)

PROOF (using accounting):

Assign amortized cost = 3 to each append

Cost accounting:
- 1 unit: for immediate append
- 2 units: savings toward future resize

When array doubles from size k to 2k:
- Before: k elements, k units saved (1 per element × k elements)
- Wait, only last element saved from its operation...

Actually, more careful analysis:
- k-1 appends (no resize): 3(k-1) cost assigned
  Used: k-1 (immediate), saved: 2(k-1)
  Account: 2(k-1)

- 1 append (with resize): cost assigned = 3
  Used: 1 (immediate) + k (copy from old array)
  Saved: 2
  Need from account: k - 1 = k-1
  Have in account: 2(k-1) = 2k - 2
  
  Since k-1 ≤ 2k-2 (true for k ≥ 1):
  Account never goes negative ✓

- After resize, continue...
  Account now has: (2(k-1)) - (k-1) + 2 = k + 1
  
Thus: Amortized cost is always 3 = O(1)
```

---

## 🎓 CONCEPT: Other Data Structures

### Self-Adjusting Binary Search Tree (Splay)

```
SPLAY TREES:

Access a frequently-used element:
Tree restructures ("splays") that element to root

Analysis: O(log n) amortized per operation

Why?
- Sometimes splay is expensive (many rotations)
- But afterwards, frequently-used element is near root
- Future accesses are cheap
- Over sequence, amortizes to O(log n)
```

### Fibonacci Heaps

```
FIBONACCI HEAPS:

Complex data structure with amortized:
- Insert: O(1) amortized
- Find min: O(1) amortized
- Extract min: O(log n) amortized
- Decrease key: O(1) amortized

Total n operations: O(n log n) total
Compare with binary heap: O(n log n) per operation

For certain algorithms (Dijkstra):
Using Fibonacci heap can improve complexity!
But high constant factors, rarely used in practice.
```

---

## 🔑 KEY INSIGHTS FOR DAY 4

| Concept | Key Point |
|---------|-----------|
| **Amortized** | Average cost over sequence of operations |
| **Accounting** | Each operation pays for future work |
| **Potential** | Amortized = Actual + Change in potential |
| **Dynamic Array** | O(1) amortized despite O(n) resize |
| **Account Balance** | Never goes negative (proof of correctness) |
| **Key Insight** | Expensive operations are infrequent |

---

---

## 📅 DAY 5 (OPTIONAL): MIXED PARADIGM PROBLEMS

### ⏱️ Duration: 90 minutes

---

## 🎓 CONCEPT: Combining Paradigms

### Backtracking + Greedy

```
PROBLEM: Maximize selections with constraints

Example: Job Scheduling (like Day 4, Week 12)
but with multiple machines

Approach:
1. Greedy ordering: Sort jobs by profit
2. Backtracking: Try assigning to machines
   If assignment leads to infeasible state → Backtrack

Hybrid: Best of both
- Greedy narrows choices (profit order)
- Backtracking explores remaining possibilities
```

### Branch & Bound + DP

```
PROBLEM: Optimization with subproblem sharing

Approach:
1. Branch & Bound: Systematically explore
2. Dynamic Programming: Cache subproblem results
   
When bounding function encounters known subproblem:
- Use DP value instead of recomputing
- Prune more aggressively

Example: TSP with memorized distances
- Compute MST once, reuse for all bounds
```

### Backtracking + Constraint Propagation + Greedy

```
PROBLEM: Complex constraint satisfaction

Approach:
1. Constraint propagation: Reduce variable domains
2. Greedy heuristic: Choose variable with smallest domain (MRV)
3. Backtracking: Search when needed

Example: Sudoku solver
- Propagate constraints (cell can't have number in row)
- Choose cell with fewest possibilities first (MRV)
- Backtrack when stuck

Results in massive speedup compared to naive backtracking
```

---

## 🎓 CONCEPT: Choosing the Right Paradigm

### Decision Tree

```
ALGORITHM PARADIGM SELECTION:

Problem → Question → Answer → Paradigm

Q1: Is optimal substructure present?
  NO → Might not have standard solution
       Try: Heuristics, approximation
  YES → Continue

Q2: Can problem be decomposed?
  NO → Try: Iteration, simulation
  YES → Continue

Q3: Overlapping subproblems?
  YES → Dynamic Programming
  NO → Continue

Q4: Greedy choice property?
  YES → Greedy Algorithm
  NO → Continue

Q5: Need to try multiple choices?
  YES → Backtracking
  NO → Continue

Q6: Need optimization pruning?
  YES → Branch & Bound
  NO → Continue

Q7: Is solution amortized?
  YES → Amortized analysis
  NO → Regular complexity analysis
```

---

## 🔑 KEY INSIGHTS FOR DAY 5

| Concept | Key Point |
|---------|-----------|
| **Hybrid Approaches** | Combine paradigms for complex problems |
| **Constraint Prop** | Reduce search space before backtracking |
| **MRV Heuristic** | Variable with smallest domain first |
| **Paradigm Choice** | Different problems need different approaches |
| **Real World** | Most complex problems use multiple techniques |

---

---

## 📋 WEEK 13 SUMMARY & MASTERY CHECKLIST

### 🎯 Concepts Covered

1. ✅ **Backtracking Fundamentals**
   - Incrementally build solutions
   - Prune invalid branches
   - DFS on solution space tree

2. ✅ **Backtracking Problems**
   - N-Queens: one per row, check conflicts
   - Sudoku: constraint satisfaction with propagation
   - Permutations: order matters, generate all
   - Combinations: order doesn't matter, avoid duplicates
   - Word Search: DFS with visited tracking
   - Maze: find path, backtrack on dead ends

3. ✅ **Branch & Bound**
   - Systematic search with bounding
   - Prune based on upper/lower bounds
   - TSP with MST bounds
   - 0/1 Knapsack with fractional upper bound

4. ✅ **Amortized Analysis**
   - Accounting method (savings account)
   - Potential method (state function)
   - Dynamic array doubling: O(1) amortized
   - Other structures: splay trees, Fibonacci heaps

5. ✅ **Mixed Paradigms**
   - Combine backtracking + greedy
   - Combine B&B + DP
   - Constraint propagation with backtracking
   - Algorithm selection decision tree

---

### 📊 Problem Patterns

| Pattern | Key Technique | Example |
|---------|---------------|---------|
| **Exhaustive Search** | Backtracking | Permutations |
| **Constraint Satisfaction** | Constraint prop + backtrack | Sudoku |
| **Optimization** | Branch & Bound | TSP, Knapsack |
| **Complexity Analysis** | Amortized | Dynamic arrays |

---

### ✅ MASTERY CHECKLIST FOR WEEK 13

- [ ] Can implement N-Queens without looking up algorithm
- [ ] Understand why constraint propagation helps Sudoku
- [ ] Can generate all permutations and combinations from scratch
- [ ] Know how to detect when to use backtracking vs DP
- [ ] Can explain Branch & Bound pruning strategy
- [ ] Understand upper/lower bounds in optimization
- [ ] Can trace TSP branch & bound execution
- [ ] Know accounting method for amortized analysis
- [ ] Can prove dynamic array amortized cost
- [ ] Can combine multiple paradigms for complex problems
- [ ] Can choose right paradigm for new problems
- [ ] Understand trade-offs between paradigms

---

### 🔑 TOP 5 THINGS TO REMEMBER

1. **Backtracking = DFS with pruning on solution tree**
2. **Constraint propagation dramatically reduces search space**
3. **Branch & Bound prunes with bounds, not just validity**
4. **Amortized cost: expensive operations are infrequent**
5. **Choose paradigm based on problem structure**

---

**End of Week 13: Backtracking & Branch & Bound**

*Comprehensive concept explanations with no code*  
*Visual diagrams, decision trees, and detailed examples*  
*Ready for implementation in any language*

