# 🟧 WEEK 13: BACKTRACKING & BRANCH & BOUND
## Comprehensive Concept-Based Learning Playbook
**Duration:** 25 hours | **Days:** 5 | **Focus:** Concept Mastery, No-Code Understanding

---

# 📖 TABLE OF CONTENTS
1. [Weekly Overview](#weekly-overview)
2. [Day 1: Backtracking Fundamentals](#day-1-backtracking-fundamentals)
3. [Day 2: Complex Backtracking Problems](#day-2-complex-backtracking-problems)
4. [Day 3: Branch & Bound Optimization](#day-3-branch--bound-optimization)
5. [Day 4: Amortized Analysis](#day-4-amortized-analysis)
6. [Day 5: Mixed Paradigm Integration](#day-5-mixed-paradigm-integration)
7. [Assessment & Checkpoints](#assessment--checkpoints)

---

# 📊 WEEKLY OVERVIEW

## Paradigm Shift: From Greedy to Backtracking

### Philosophical Difference

```
┌────────────────────────────────────────────────────────────┐
│        PARADIGM COMPARISON: GREEDY vs BACKTRACKING          │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  GREEDY (Week 12):                                           │
│  ┌───────────────────────────────────────────────────────┐  │
│  │ Make one choice → move forward FOREVER                │  │
│  │ Never reconsider                                       │  │
│  │ Fast: O(n log n)                                      │  │
│  │ Optimal only for special problems                     │  │
│  │                                                       │  │
│  │ Choice Flow:                                           │  │
│  │ [Choose 1] ──→ [Choose 2] ──→ [Choose 3]             │  │
│  │   (stick)        (stick)       (stick)                │  │
│  │                                                       │  │
│  └───────────────────────────────────────────────────────┘  │
│                                                              │
│  BACKTRACKING (Week 13):                                     │
│  ┌───────────────────────────────────────────────────────┐  │
│  │ Try a choice → explore all possibilities              │  │
│  │ If dead end → backtrack & try different              │  │
│  │ Can reconsider all choices                            │  │
│  │ Slower: up to O(n!) exponential                       │  │
│  │ Finds ANY/ALL solutions to hard problems              │  │
│  │                                                       │  │
│  │ Choice Flow:                                           │  │
│  │ [Try 1a] ──→ [Try 2a] ──→ [Dead!]                    │  │
│  │    │           (backtrack)                            │  │
│  │    ├─→ [Try 2b] ──→ [Solution!] ✓                    │  │
│  │    │                                                  │  │
│  │ [Try 1b] ──→ [Try 2c] ──→ [Try 3] ──→ [Solution!] ✓ │  │
│  │                                                       │  │
│  └───────────────────────────────────────────────────────┘  │
│                                                              │
│  KEY DIFFERENCE:                                             │
│  ───────────────                                             │
│  Greedy: COMMIT to choices                                  │
│  Backtracking: EXPLORE then COMMIT                          │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

## When Do We Need Backtracking?

```
┌────────────────────────────────────────────────────────────┐
│      PROBLEMS THAT NEED BACKTRACKING                        │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  PROBLEM CHARACTERISTICS:                                    │
│                                                              │
│  1. Many valid choices at each step                         │
│     └─ Can't decide locally which is best                   │
│     └─ Need to explore all                                  │
│                                                              │
│  2. Constraints eliminate many paths                        │
│     └─ Deep into exploration, hit constraint                │
│     └─ Can't continue, must backtrack                       │
│                                                              │
│  3. Goal is ANY or ALL solutions                            │
│     └─ Not just one optimal solution                        │
│     └─ Find all valid combinations                          │
│                                                              │
│  EXAMPLES:                                                   │
│  ────────                                                    │
│  • N-Queens: Place n queens on n×n board                    │
│            No two attack each other                         │
│            Find ALL valid placements                        │
│                                                              │
│  • Sudoku: Fill 9×9 grid with digits 1-9                   │
│           Constraints: rows, cols, 3×3 boxes              │
│           Find THE solution                                 │
│                                                              │
│  • Maze: Find path from start to exit                       │
│         Follow walls, avoid dead ends                       │
│         Explore until solution found                        │
│                                                              │
│  • Permutations: Generate all orderings of n items         │
│                 Try each position, recurse                  │
│                                                              │
│  • Combinations: Generate all k-subsets of n items         │
│                Make choices, backtrack, explore            │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

## Weekly Learning Architecture

```
DAY 1: Foundations
  └─ What is backtracking fundamentally?
      └─ State space tree concept
          └─ Pruning to avoid dead ends
              └─ Universal backtracking template

DAY 2: Apply to Combinatorics
  └─ Generate all permutations (all orderings)
      └─ Generate all combinations (all subsets)
          └─ Solve N-Queens (all solutions)
              └─ Solve Sudoku (single solution)

DAY 3: Optimization Focus
  └─ Branch & Bound combines backtracking + optimization
      └─ Keep track of best solution so far
          └─ Prune branches that can't improve
              └─ Apply to TSP and knapsack problems

DAY 4: Analysis Techniques
  └─ How to analyze algorithm complexity?
      └─ Different analysis method: Amortized
          └─ Three techniques: aggregate, accounting, potential
              └─ Apply to dynamic arrays and structures

DAY 5: Integration
  └─ When to use which paradigm?
      └─ Combining greedy + backtracking + DP
          └─ Hybrid algorithms for hard problems
```

---

# 📅 DAY 1: BACKTRACKING FUNDAMENTALS

## Time Allocation: 5 hours (300 minutes)

### Segment 1: What is Backtracking? (75 min)

#### Core Definition

```
┌────────────────────────────────────────────────────────────┐
│          BACKTRACKING ALGORITHM DEFINITION                  │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  Backtracking is a systematic way to explore ALL possible   │
│  solutions to a problem by:                                 │
│                                                              │
│  1. Building partial solutions incrementally                │
│  2. Testing constraints at each step                        │
│  3. Abandoning paths that violate constraints              │
│  4. Exhaustively exploring remaining valid paths           │
│  5. Collecting all valid complete solutions                │
│                                                              │
│  ─────────────────────────────────────────────────────────  │
│                                                              │
│  EQUIVALENT VIEW: DFS on Solution Tree                      │
│  ──────────────────────────────────────                     │
│  • Each node = partial solution state                       │
│  • Edges = choices (next decision)                          │
│  • Leaves = complete solutions (or dead ends)               │
│  • DFS traversal = trying all possibilities                 │
│  • Pruning = skipping invalid branches                      │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

#### Why "Backtrack"?

```
┌────────────────────────────────────────────────────────────┐
│           WHY THE NAME "BACKTRACKING"?                      │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  ANALOGY: Exploring a Maze                                  │
│  ──────────────────────────                                 │
│                                                              │
│  You enter maze:                                             │
│  • Go down path 1 → hit dead end                            │
│  • BACKTRACK to last choice                                 │
│  • Try path 2 → hit dead end                                │
│  • BACKTRACK again                                           │
│  • Try path 3 → find exit!                                  │
│                                                              │
│  Visual:                                                     │
│  ┌──────────────────────────────────────────────────────┐   │
│  │              MAZE EXPLORATION                        │   │
│  │                                                      │   │
│  │  Start ──→ [Choice]                                 │   │
│  │              ├→ Path A ──→ Dead End ✗               │   │
│  │              │   (BACKTRACK ↑)                      │   │
│  │              ├→ Path B ──→ Dead End ✗               │   │
│  │              │   (BACKTRACK ↑)                      │   │
│  │              └→ Path C ──→ EXIT ✓                   │   │
│  │                                                      │   │
│  │  KEY: Undo Path A, Undo Path B, Try Path C         │   │
│  │       This "going back" is "backtracking"           │   │
│  │                                                      │   │
│  └──────────────────────────────────────────────────────┘   │
│                                                              │
│  In algorithm terms:                                         │
│  • Move forward = make a choice, explore it                 │
│  • Hit constraint = can't continue                          │
│  • BACKTRACK = undo choice, try next one                    │
│  • Repeat until all possibilities explored                  │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

### Segment 2: State Space Tree Concept (90 min)

#### The Tree Structure

```
┌────────────────────────────────────────────────────────────┐
│          STATE SPACE TREE VISUALIZATION                     │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  PROBLEM: Generate all permutations of {A, B, C}            │
│                                                              │
│  STATE SPACE TREE:                                           │
│                                                              │
│                       (empty)                                │
│                         │                                    │
│         ┌───────────────┼───────────────┐                    │
│         │               │               │                    │
│       (A)             (B)             (C)                    │
│       /  \            /  \            /  \                   │
│     (AB) (AC)       (BA) (BC)       (CA) (CB)               │
│     /     /         /     /         /     /                  │
│  (ABC)  (ACB)   (BAC) (BCA)     (CAB) (CBA)                │
│   ✓       ✓        ✓     ✓        ✓     ✓                    │
│                                                              │
│  LEVELS:                                                     │
│  ──────                                                      │
│  Level 0: Empty solution {}                                 │
│  Level 1: First choice {A}, {B}, or {C}                    │
│  Level 2: Second choice given first                         │
│  Level 3: Third choice (complete permutation)              │
│                                                              │
│  NODES (States):                                             │
│  ───────────────                                             │
│  Each node represents partial solution                       │
│  Edges represent choices (decisions)                        │
│                                                              │
│  LEAVES (Terminal States):                                   │
│  ─────────────────────────                                   │
│  All 6 leaves are complete valid permutations               │
│  No constraints to violate in this example                  │
│                                                              │
│  PATH = Solution:                                            │
│  ────────────────                                            │
│  Root → (A) → (AB) → (ABC) ✓                               │
│  Root → (B) → (BC) → (BCA) ✓                               │
│  etc.                                                        │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

#### Constraints & Pruning

```
┌────────────────────────────────────────────────────────────┐
│       STATE SPACE WITH CONSTRAINTS & PRUNING                │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  PROBLEM: N-Queens on 3×3 board                             │
│           Constraint: No two queens attack                  │
│                                                              │
│  STATE SPACE TREE (WITH PRUNING):                           │
│                                                              │
│                       (empty)                                │
│                         │                                    │
│         ┌───────────────┼───────────────┐                    │
│         │               │               │                    │
│       Q@0             Q@1             Q@2                    │
│       / \             / \             / \                    │
│    Q@1 Q@2 ✗       Q@0 Q@2 ✗      Q@0 Q@1                   │
│    /    ✗         /    ✗          /    /                     │
│  Q@2  (prune)   Q@0 (prune)     Q@1 Q@2                     │
│  ✗             ✗              ✗    ✗                         │
│                                                              │
│  KEY DIFFERENCE:                                             │
│  ────────────────                                            │
│  Instead of 3! = 6 leaves, we PRUNE invalid branches        │
│  After placing first queen, many positions for 2nd invalid  │
│  Backtrack and avoid exploring those branches               │
│                                                              │
│  PRUNING BENEFIT:                                            │
│  ────────────────                                            │
│  Without pruning: Explore 3! = 6 permutations              │
│  With pruning: Explore far fewer (actual solutions only)    │
│                                                              │
│  Example for 8-Queens:                                       │
│  Without pruning: 8! = 40,320 permutations                 │
│  With pruning: ~15,720 explored (92 solutions checked)      │
│  Savings: 60% reduction via pruning!                        │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

### Segment 3: Backtracking Template (75 min)

#### Universal Template

```
┌────────────────────────────────────────────────────────────┐
│      BACKTRACKING ALGORITHM TEMPLATE                        │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  FUNCTION backtrack(partial_solution, constraints):         │
│  ═════════════════════════════════════════════             │
│                                                              │
│      STEP 1: CHECK BASE CASE                                 │
│      ───────────────────────                                │
│      If partial_solution is COMPLETE:                       │
│          Record as valid solution ✓                         │
│          Return (solution found!)                           │
│                                                              │
│      STEP 2: TRY ALL POSSIBLE NEXT CHOICES                   │
│      ─────────────────────────────────────                  │
│      For each possible next choice:                         │
│                                                              │
│          STEP 2A: CHECK CONSTRAINT                           │
│          ──────────────────────────                         │
│          If choice VIOLATES any constraint:                 │
│              Skip this choice (prune)                       │
│                                                              │
│          STEP 2B: MAKE CHOICE & RECURSE                      │
│          ────────────────────────────                       │
│          Add choice to partial_solution                     │
│          Call backtrack(updated_solution, constraints)      │
│          (May find more solutions recursively)              │
│                                                              │
│          STEP 2C: UNDO CHOICE (BACKTRACK!)                   │
│          ────────────────────────────────                   │
│          Remove choice from partial_solution                │
│          (Restore state for next iteration)                 │
│                                                              │
│      STEP 3: RETURN                                          │
│      ──────────────                                          │
│      Done exploring all possibilities for this level        │
│      (Recursive caller will handle backtracking)             │
│                                                              │
│  KEY PATTERN:                                                │
│  ────────────                                                │
│  Try choice → Recurse → Undo choice → Try next              │
│  This cycle = BACKTRACKING                                  │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

#### Template Illustrated

```
┌────────────────────────────────────────────────────────────┐
│       TEMPLATE EXECUTION FLOW (EXAMPLE)                     │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  backtrack([], constraint):                                  │
│  ┌──────────────────────────────────────────────────────┐   │
│  │ Is [] complete? NO → continue                        │   │
│  │                                                      │   │
│  │ Try choice #1 = 'A':                                 │   │
│  │  ├─ Violates constraint? NO                          │   │
│  │  ├─ Add 'A': partial = [A]                           │   │
│  │  ├─ Call: backtrack([A], constraint)  ← RECURSE     │   │
│  │  │                                                   │   │
│  │  │  backtrack([A], constraint):                      │   │
│  │  │  ┌──────────────────────────────────────────────┐ │   │
│  │  │  │ Try choice #1 = 'B':                          │ │   │
│  │  │  │  ├─ Violates constraint? NO                   │ │   │
│  │  │  │  ├─ Add 'B': partial = [A, B]                 │ │   │
│  │  │  │  ├─ Call: backtrack([A, B], constraint)       │ │   │
│  │  │  │  │ (...finds solution [A, B, C] or more)      │ │   │
│  │  │  │  └─ Remove 'B': partial = [A]  ← UNDO        │ │   │
│  │  │  │                                                │ │   │
│  │  │  │ Try choice #2 = 'C':                           │ │   │
│  │  │  │  ├─ Violates constraint? YES → Skip          │ │   │
│  │  │  │                                                │ │   │
│  │  │  │ Return (done with level)                       │ │   │
│  │  │  └──────────────────────────────────────────────┘ │   │
│  │  │                                                   │   │
│  │  └─ Remove 'A': partial = []  ← UNDO              │   │
│  │                                                      │   │
│  │ Try choice #2 = 'B':                                 │   │
│  │  ├─ Violates constraint? NO                          │   │
│  │  ├─ Add 'B': partial = [B]                           │   │
│  │  ├─ Call: backtrack([B], constraint)  ← RECURSE     │   │
│  │  │ (explores all paths starting with B)             │   │
│  │  └─ Remove 'B': partial = []  ← UNDO              │   │
│  │                                                      │   │
│  │ ... and so on for all choices                        │   │
│  │                                                      │   │
│  └──────────────────────────────────────────────────────┘   │
│                                                              │
│  TRACE OF UNDO/REDO:                                         │
│  ────────────────────                                        │
│  Add A   → Recurse  → Remove A  (restored to initial)      │
│  Add B   → Recurse  → Remove B  (restored again)             │
│  Add C   → Recurse  → Remove C  (restored again)             │
│                                                              │
│  This undo/redo pattern enables exploration of ALL paths    │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

### Segment 4: Pruning Strategies (60 min)

#### How & When to Prune

```
┌────────────────────────────────────────────────────────────┐
│           PRUNING STRATEGIES                                │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  WHAT IS PRUNING?                                            │
│  ════════════════                                            │
│  Avoid exploring branches that CANNOT lead to solution      │
│  By checking constraints BEFORE recursing                   │
│                                                              │
│  WHY PRUNE?                                                  │
│  ══════════                                                  │
│  Backtracking can explore exponential branches              │
│  Pruning eliminates huge swaths of search space             │
│  Can reduce from O(n!) to much smaller                      │
│                                                              │
│  ─────────────────────────────────────────────────────────  │
│                                                              │
│  PRUNING STRATEGY 1: Constraint Satisfaction                │
│  ═══════════════════════════════════════════                │
│  Check hard constraints immediately                         │
│  If violated → prune (don't recurse)                        │
│                                                              │
│  Example: N-Queens                                          │
│  • Constraint: No two queens on same row/column/diagonal   │
│  • When placing next queen: check all 3 constraints         │
│  • If any violated → skip (prune)                           │
│                                                              │
│  ┌─────────────────────────────────────────────────────┐   │
│  │ Current board: Q at (0,0)                           │   │
│  │ Trying to place Q at (1, col):                      │   │
│  │                                                     │   │
│  │ (1,0) → Same column as (0,0) ✗ Prune              │   │
│  │ (1,1) → Diagonal from (0,0) ✗ Prune               │   │
│  │ (1,2) → No conflict ✓ Explore                      │   │
│  │                                                     │   │
│  │ Without pruning: try all 3                          │   │
│  │ With pruning: skip 2 invalid, explore 1 valid       │   │
│  │                                                     │   │
│  └─────────────────────────────────────────────────────┘   │
│                                                              │
│  ─────────────────────────────────────────────────────────  │
│                                                              │
│  PRUNING STRATEGY 2: Feasibility Analysis                   │
│  ════════════════════════════════════════                   │
│  Before recursing, check if goal POSSIBLE given current    │
│  state                                                      │
│                                                              │
│  Example: Sudoku                                            │
│  Current state: Partially filled grid                       │
│  Before placing digit in empty cell:                        │
│  • Check if value valid in row/column/box                   │
│  • Check if any empty cells now have 0 choices (dead end)  │
│  • If dead end detected → prune entire subtree!             │
│                                                              │
│  ┌─────────────────────────────────────────────────────┐   │
│  │ After placing some digits:                          │   │
│  │ Cell X has row {1,2,3,4,5,6,7,8,9} taken           │   │
│  │ Cell X needs digit from {1-9} - {others}           │   │
│  │ Result: Cell X must be 9? → Forced!                 │   │
│  │ Or: Cell X has NO valid digits? → Dead End!         │   │
│  │                                                     │   │
│  │ Detect this EARLY → Prune entire subtree from X    │   │
│  │                                                     │   │
│  └─────────────────────────────────────────────────────┘   │
│                                                              │
│  ─────────────────────────────────────────────────────────  │
│                                                              │
│  PRUNING STRATEGY 3: Bounds & Heuristics (see Day 3)        │
│  ═════════════════════════════════════════════════          │
│  For optimization: track best solution so far               │
│  Prune if current partial can't beat best                   │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

### Segment 5: Summary (30 min)

#### Day 1 Summary

```
┌────────────────────────────────────────────────────────────┐
│          DAY 1 CONCEPTUAL SUMMARY                           │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  WHAT IS BACKTRACKING?                                      │
│  ──────────────────                                         │
│  ✓ Systematic exploration of ALL possibilities              │
│  ✓ Build solution incrementally                             │
│  ✓ Undo & try alternatives when stuck                       │
│  ✓ Equivalent to DFS on solution tree                       │
│                                                              │
│  STATE SPACE TREE:                                           │
│  ────────────────                                           │
│  ✓ Nodes = partial solutions (states)                       │
│  ✓ Edges = choices (decisions)                              │
│  ✓ Leaves = complete solutions or dead ends                 │
│  ✓ DFS traversal = backtracking                             │
│                                                              │
│  TEMPLATE:                                                   │
│  ════════                                                    │
│  1. Check if complete → record solution                     │
│  2. For each choice:                                         │
│     a. Check constraint (prune if invalid)                  │
│     b. Make choice & recurse                                │
│     c. Undo choice (restore state)                          │
│  3. Return (done exploring)                                 │
│                                                              │
│  PRUNING:                                                    │
│  ═══════                                                     │
│  ✓ Eliminate branches that can't lead to solutions          │
│  ✓ Critical for performance                                 │
│  ✓ Check constraints BEFORE recursing                       │
│  ✓ Detect dead ends early                                   │
│                                                              │
│  KEY INSIGHT:                                                │
│  ────────────                                                │
│  Backtracking trades TIME for COMPLETENESS                  │
│  Guaranteed to find ALL solutions                           │
│  Speed depends on pruning effectiveness                     │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

---

# 📅 DAY 2: COMPLEX BACKTRACKING PROBLEMS

## Time Allocation: 5 hours (300 minutes)

### Segment 1: N-Queens Problem (90 min)

#### Problem Statement

```
┌────────────────────────────────────────────────────────────┐
│          N-QUEENS PROBLEM DEFINITION                        │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  GOAL: Place n queens on n×n chessboard                     │
│        Such that no two queens attack each other            │
│                                                              │
│  ATTACK DEFINITION: Two queens attack if:                   │
│  ───────────────────────────────────────                    │
│  • Same row, OR                                              │
│  • Same column, OR                                           │
│  • Same diagonal (either direction)                         │
│                                                              │
│  CONSTRAINT: One queen per row (implicit from placing order)│
│             At most one queen per column                    │
│             At most one queen per diagonal                  │
│                                                              │
│  FIND: ALL valid placements (or just one)                   │
│                                                              │
│  Example: 4-Queens on 4×4 board                             │
│  ┌────────────────────────────────────────┐                │
│  │ Solution 1:                             │                │
│  │  . Q . .    (row 0: Queen at column 1) │                │
│  │  . . . Q    (row 1: Queen at column 3) │                │
│  │  Q . . .    (row 2: Queen at column 0) │                │
│  │  . . Q .    (row 3: Queen at column 2) │                │
│  │                                        │                │
│  │ Solution 2: (different arrangement)    │                │
│  │  . . Q .                               │                │
│  │  Q . . .                               │                │
│  │  . . . Q                               │                │
│  │  . Q . .                               │                │
│  │                                        │                │
│  │ For 4-Queens: 2 distinct solutions     │                │
│  │              (8 if mirror images count)│                │
│  │                                        │                │
│  └────────────────────────────────────────┘                │
│                                                              │
│  COMPLEXITY:                                                 │
│  ───────────                                                 │
│  Naive: Try all n^n placements                              │
│  With backtracking & pruning: Much smaller                 │
│                                                              │
│  For n=8: 16 million placements, ~15k explored             │
│  For n=20: Exponential but backtracking needed             │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

#### Solving Approach

```
┌────────────────────────────────────────────────────────────┐
│        N-QUEENS BACKTRACKING APPROACH                       │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  KEY INSIGHT:                                                │
│  ────────────                                                │
│  Place one queen per row (ensures no row conflicts)         │
│  For each row: try each column until valid placement        │
│                                                              │
│  ALGORITHM:                                                  │
│  ═════════                                                   │
│  backtrack(board, row):                                     │
│    IF row == n:                                              │
│      RECORD valid solution ✓                                │
│      RETURN                                                  │
│                                                              │
│    FOR col = 0 TO n-1:                                      │
│      IF can_place_queen(board, row, col):                   │
│        Place queen at (row, col)                            │
│        backtrack(board, row + 1)                            │
│        Remove queen (undo)                                  │
│                                                              │
│  ─────────────────────────────────────────────────────────  │
│                                                              │
│  CONSTRAINT CHECKS (can_place_queen):                        │
│  ═══════════════════════════════════                         │
│  Check column:                                               │
│    Is column 'col' empty in all previous rows?              │
│    Return FALSE if any queen found                          │
│                                                              │
│  Check diagonal (top-left to bottom-right):                 │
│    Start from (row, col) → go up-left                       │
│    Is path empty?                                            │
│    Return FALSE if any queen found                          │
│                                                              │
│  Check anti-diagonal (top-right to bottom-left):            │
│    Start from (row, col) → go up-right                      │
│    Is path empty?                                            │
│    Return FALSE if any queen found                          │
│                                                              │
│  If all three checks pass → place queen ✓                   │
│                                                              │
│  ─────────────────────────────────────────────────────────  │
│                                                              │
│  EXAMPLE TRACE (4-Queens):                                   │
│  ════════════════════════════                                │
│  Row 0:                                                      │
│    Try col 0: Place Q → check rows 1,2,3 for Q             │
│    Try col 1: Q @ (0,1) → continue to row 1                │
│                                                              │
│  Row 1 (after Q @ (0,1)):                                    │
│    Try col 0: Conflict? Row 0, col 0 safe. Diagonals?      │
│               (0,1)-(1,0) = diagonal. Conflict! Skip       │
│    Try col 1: Conflict? Same column as (0,1). Skip         │
│    Try col 2: Conflict? Check all. No conflict. Place Q    │
│                                                              │
│  Row 2 (after Q @ (0,1), Q @ (1,2)):                         │
│    Try col 0: Conflicts? Check row 0,1. Check diagonals.   │
│    Try col 1: Conflicts? No → Place Q                       │
│                                                              │
│  Row 3 (after Q @ (0,1), (1,2), (2,1)):                      │
│    No valid placement → BACKTRACK                           │
│    Remove Q @ (2,1), try next column for row 2              │
│                                                              │
│  Continues until all solutions found or no more options     │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

### Segment 2: Sudoku Solver (90 min)

#### Problem Setup

```
┌────────────────────────────────────────────────────────────┐
│          SUDOKU PROBLEM DEFINITION                          │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  INPUT: 9×9 grid with some cells filled (1-9)               │
│         Some cells empty                                    │
│                                                              │
│  GOAL: Fill empty cells with digits 1-9                     │
│        Such that constraints satisfied                      │
│                                                              │
│  CONSTRAINTS:                                                │
│  ────────────                                                │
│  1. Each row: contains digits 1-9 exactly once              │
│  2. Each column: contains digits 1-9 exactly once           │
│  3. Each 3×3 box: contains digits 1-9 exactly once          │
│                                                              │
│  Example (simplified):                                       │
│  ┌──────────┐                                                │
│  │ 5 3 . │. 7 . │         Each row has 1-9                  │
│  │ 6 . . │1 9 5 │         Each column has 1-9               │
│  │ . 9 8 │. 6 . │         Each box has 1-9                  │
│  │───────────────│                                            │
│  │ . . . │. . . │         Fill dots to satisfy all           │
│  │ 1 . . │. 6 . │                                             │
│  │ . . . │. . . │                                             │
│  │───────────────│                                            │
│  │ . . . │. . . │         Result: unique solution            │
│  │ . . . │. . . │                                             │
│  │ . . . │. . . │                                             │
│  └──────────┘                                                │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

#### Backtracking Solution

```
┌────────────────────────────────────────────────────────────┐
│       SUDOKU BACKTRACKING APPROACH                          │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  KEY INSIGHT:                                                │
│  ────────────                                                │
│  Fill cells one by one                                      │
│  For each empty cell: try digits 1-9                        │
│  Check constraints before recursing                         │
│  Prune infeasible branches early                            │
│                                                              │
│  ALGORITHM:                                                  │
│  ═════════                                                   │
│  solve(grid):                                                │
│    Find next empty cell (row, col)                          │
│                                                              │
│    IF no empty cell found:                                   │
│      ALL cells filled → SOLUTION ✓                          │
│      RETURN TRUE                                             │
│                                                              │
│    FOR digit = 1 TO 9:                                       │
│      IF is_valid(grid, row, col, digit):                    │
│        Place digit at (row, col)                            │
│        IF solve(grid):  ← RECURSE                           │
│          RETURN TRUE  ← Solution found deeper               │
│        Remove digit (BACKTRACK)                             │
│                                                              │
│    RETURN FALSE  ← No solution from this state              │
│                                                              │
│  ─────────────────────────────────────────────────────────  │
│                                                              │
│  CONSTRAINT CHECK (is_valid):                                │
│  ════════════════════════════                                │
│  1. Check row: Is digit already in same row?                │
│                If YES → invalid                              │
│                                                              │
│  2. Check column: Is digit already in same column?          │
│                    If YES → invalid                          │
│                                                              │
│  3. Check 3×3 box: Is digit already in same box?           │
│                     If YES → invalid                         │
│                                                              │
│  If all three checks pass → digit valid ✓                   │
│                                                              │
│  ─────────────────────────────────────────────────────────  │
│                                                              │
│  OPTIMIZATION: Maintain Candidates                           │
│  ═════════════════════════════════════                       │
│                                                              │
│  Instead of trying 1-9 each time:                            │
│  • For each empty cell: track possible digits               │
│  • Only try candidates (not all 1-9)                        │
│  • When placing digit: update candidates for affected rows  │
│  • If any cell has 0 candidates → detected dead end!        │
│    Backtrack immediately (PRUNING!)                         │
│                                                              │
│  ┌────────────────────────────────────────────────────────┐ │
│  │ Example:                                                │ │
│  │ Cell (0,0) candidates initially: {1,2,3,4,5,6,7,8,9}  │ │
│  │ Row 0 has 5,3: candidates: {1,2,4,6,7,8,9}            │ │
│  │ Col 0 has 6,1: candidates: {2,4,7,8,9}                │ │
│  │ Box has 5,3,6: candidates: {1,2,4,7,8,9}              │ │
│  │ Intersection: {2,4,7,8,9}                              │ │
│  │                                                        │ │
│  │ Only try these 5 candidates instead of 1-9!            │ │
│  │                                                        │ │
│  │ If another constraint fills (e.g., row later):         │ │
│  │ Update candidates again                                │ │
│  │ If candidates become empty → Dead end! Backtrack!      │ │
│  │                                                        │ │
│  └────────────────────────────────────────────────────────┘ │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

### Segment 3: Permutations & Combinations (60 min)

#### Generate All Permutations

```
┌────────────────────────────────────────────────────────────┐
│    GENERATING ALL PERMUTATIONS (ALL ORDERINGS)              │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  PROBLEM: Given array [1, 2, 3]                             │
│           Generate ALL orderings:                           │
│           [1,2,3], [1,3,2], [2,1,3], [2,3,1], [3,1,2], ... │
│                                                              │
│  BACKTRACKING APPROACH:                                      │
│  ═════════════════════                                       │
│  Use "available" set: which elements haven't been used yet  │
│  At each step: try each available element                   │
│               Add to current permutation                    │
│               Recurse with remaining available              │
│               Remove from current (backtrack)               │
│                                                              │
│  Algorithm:                                                  │
│  permute(current, available):                               │
│    IF available is empty:                                    │
│      Record current as valid permutation ✓                  │
│      RETURN                                                  │
│                                                              │
│    FOR each element in available:                            │
│      Add element to current                                 │
│      Remove element from available                          │
│      permute(current, available)  ← RECURSE                │
│      Remove element from current (UNDO)                     │
│      Restore element to available                           │
│                                                              │
│  ─────────────────────────────────────────────────────────  │
│                                                              │
│  EXAMPLE TRACE [1,2,3]:                                      │
│  ═════════════════════                                       │
│  permute([], [1,2,3]):                                       │
│    Try 1: permute([1], [2,3])                                │
│      Try 2: permute([1,2], [3])                              │
│        Try 3: permute([1,2,3], [])                           │
│          Empty! Record [1,2,3] ✓                             │
│        Remove 3: permute([1,2], [3])                         │
│      Remove 2: permute([1], [2,3])                           │
│      Try 3: permute([1,3], [2])                              │
│        Try 2: permute([1,3,2], [])                           │
│          Empty! Record [1,3,2] ✓                             │
│    Remove 1: permute([], [1,2,3])                            │
│    Try 2: permute([2], [1,3])                                │
│      Try 1: permute([2,1], [3])                              │
│        Try 3: permute([2,1,3], [])                           │
│          Empty! Record [2,1,3] ✓                             │
│    ... continue for all 6 permutations                       │
│                                                              │
│  RESULT: All 3! = 6 permutations generated                  │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

#### Generate All Combinations

```
┌────────────────────────────────────────────────────────────┐
│    GENERATING ALL COMBINATIONS (K-SUBSETS)                  │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  PROBLEM: Given array [1, 2, 3, 4]                          │
│           Generate all 2-element subsets:                   │
│           [1,2], [1,3], [1,4], [2,3], [2,4], [3,4]         │
│           (C(4,2) = 6 combinations)                          │
│                                                              │
│  DIFFERENCE FROM PERMUTATIONS:                               │
│  ────────────────────────────────                            │
│  [1,2] and [2,1] are SAME combination                        │
│  [1,2,3] and [2,1,3] are SAME (just reordered)              │
│                                                              │
│  To avoid duplicates:                                        │
│  Use INDEX-BASED approach (ensure order)                     │
│                                                              │
│  Algorithm:                                                  │
│  combine(array, current, k, start):                          │
│    IF current.length == k:                                  │
│      Record current as valid combination ✓                  │
│      RETURN                                                  │
│                                                              │
│    FOR i = start TO array.length - 1:                        │
│      Add array[i] to current                                │
│      combine(array, current, k, i + 1)  ← start from i+1   │
│      Remove array[i] from current (UNDO)                    │
│                                                              │
│  Key: start = i + 1 prevents (1,2) and (2,1) both          │
│        Always go forward in array                           │
│                                                              │
│  ─────────────────────────────────────────────────────────  │
│                                                              │
│  EXAMPLE TRACE [1,2,3,4] choose 2:                           │
│  ════════════════════════════════════                        │
│  combine(..., [], 2, 0):                                     │
│    i=0: Add 1 → combine(..., [1], 2, 1)                      │
│      i=1: Add 2 → combine(..., [1,2], 2, 2)                  │
│        length==2! Record [1,2] ✓                             │
│      i=2: Add 3 → combine(..., [1,3], 2, 3)                  │
│        length==2! Record [1,3] ✓                             │
│      i=3: Add 4 → combine(..., [1,4], 2, 4)                  │
│        length==2! Record [1,4] ✓                             │
│    i=1: Add 2 → combine(..., [2], 2, 2)                      │
│      i=2: Add 3 → combine(..., [2,3], 2, 3)                  │
│        length==2! Record [2,3] ✓                             │
│      i=3: Add 4 → combine(..., [2,4], 2, 4)                  │
│        length==2! Record [2,4] ✓                             │
│    i=2: Add 3 → combine(..., [3], 2, 3)                      │
│      i=3: Add 4 → combine(..., [3,4], 2, 4)                  │
│        length==2! Record [3,4] ✓                             │
│    i=3: Add 4 → combine(..., [4], 2, 4)                      │
│      start > array.length, no i to try                       │
│                                                              │
│  RESULT: All C(4,2) = 6 combinations generated              │
│          Without duplicates (order doesn't matter)          │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

### Segment 4: Summary (30 min)

#### Day 2 Summary

```
┌────────────────────────────────────────────────────────────┐
│          DAY 2 CONCEPTUAL SUMMARY                           │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  N-QUEENS PROBLEM:                                           │
│  ────────────────                                            │
│  ✓ Place n queens on n×n board                              │
│  ✓ Constraint: no two queens attack                         │
│  ✓ Backtracking: one queen per row                          │
│  ✓ Prune by checking column & diagonals                     │
│                                                              │
│  SUDOKU SOLVER:                                              │
│  ───────────                                                 │
│  ✓ Fill 9×9 grid with constraints                           │
│  ✓ Constraints: rows, columns, 3×3 boxes                   │
│  ✓ Backtracking: fill cell by cell                          │
│  ✓ Pruning: track candidates to detect dead ends early     │
│                                                              │
│  PERMUTATIONS:                                               │
│  ─────────────                                               │
│  ✓ Generate ALL orderings of n elements                     │
│  ✓ Use "available" set to track what's left                 │
│  ✓ At each step: try each available element                 │
│  ✓ Total: n! permutations                                   │
│                                                              │
│  COMBINATIONS:                                               │
│  ─────────────                                               │
│  ✓ Generate all k-subsets of n elements                     │
│  ✓ Avoid duplicates: use INDEX-based approach               │
│  ✓ Start from index + 1 (ensure order)                      │
│  ✓ Total: C(n,k) combinations                               │
│                                                              │
│  KEY VARIATIONS:                                             │
│  ────────────                                                │
│  ✓ Permutations: Order matters, all elements used           │
│  ✓ Combinations: Order doesn't matter, subset of elements   │
│  ✓ Different constraints → different pruning strategies     │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

---

# 📅 DAY 3: BRANCH & BOUND OPTIMIZATION

## Time Allocation: 5 hours (300 minutes)

### Segment 1: Branch & Bound Concept (90 min)

#### Definition & Key Idea

```
┌────────────────────────────────────────────────────────────┐
│        BRANCH & BOUND ALGORITHM CONCEPT                     │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  PROBLEM TYPE:                                               │
│  ─────────────                                               │
│  OPTIMIZATION problem: Minimize or Maximize some value      │
│  Example: Traveling Salesman (minimize distance)            │
│           0/1 Knapsack (maximize value)                     │
│                                                              │
│  WHAT IS BRANCH & BOUND?                                    │
│  ═════════════════════════                                  │
│  Backtracking + Optimization                                │
│                                                              │
│  BRANCH: Like backtracking → explore tree of choices        │
│  BOUND: Track best solution found so far                    │
│         Skip branches that CAN'T improve best               │
│                                                              │
│  ─────────────────────────────────────────────────────────  │
│                                                              │
│  ALGORITHM OUTLINE:                                          │
│  ══════════════════                                          │
│  best_so_far = ∞ (or -∞ for maximization)                   │
│                                                              │
│  search(partial_solution):                                  │
│    1. Compute BOUND on best possible from here              │
│    2. If bound ≤ best_so_far: PRUNE (skip!)                │
│       (This branch can't beat current best)                 │
│                                                              │
│    3. If partial_solution is complete:                      │
│       Update best_so_far = this solution's value            │
│       Return                                                │
│                                                              │
│    4. For each choice:                                       │
│       Make choice                                            │
│       search(extended_solution)  ← recurse                  │
│       Undo choice                                            │
│                                                              │
│  KEY DIFFERENCE FROM BACKTRACKING:                           │
│  ──────────────────────────────────                          │
│  Backtracking: Prune if constraints violated                │
│  Branch & Bound: ALSO prune if can't improve best known    │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

#### Bounding Techniques

```
┌────────────────────────────────────────────────────────────┐
│         BOUNDING TECHNIQUES                                 │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  WHAT IS A BOUND?                                            │
│  ════════════════                                            │
│  An estimate of best possible solution from current state   │
│                                                              │
│  For minimization (e.g., TSP):                              │
│  • Bound = minimum cost we can achieve                      │
│  • If bound > best_so_far → can't improve, prune           │
│                                                              │
│  For maximization (e.g., knapsack):                         │
│  • Bound = maximum value we can achieve                     │
│  • If bound < best_so_far → can't improve, prune           │
│                                                              │
│  ─────────────────────────────────────────────────────────  │
│                                                              │
│  BOUND TIGHTNESS:                                            │
│  ════════════════                                            │
│  • Tight bound: Close to actual best → More pruning!       │
│  • Loose bound: Far from actual best → Less pruning        │
│                                                              │
│  Trade-off:                                                  │
│  • Tighter bound → Better pruning but takes time to compute │
│  • Looser bound → Quick but poor pruning                    │
│                                                              │
│  ─────────────────────────────────────────────────────────  │
│                                                              │
│  TECHNIQUE 1: Relaxation                                     │
│  ═════════════════════                                       │
│  Remove constraints → problem becomes easier                │
│  Solve easier problem optimally (fast)                      │
│  Optimal of easier ≥ optimal of harder (lower bound)        │
│                                                              │
│  Example TSP:                                                │
│  • Hard version: Complete tour visiting all cities once     │
│  • Easy version: Minimum spanning tree (no tour cycle req)  │
│  • MST cost ≤ TSP cost (always)                            │
│  • So use MST as lower bound!                               │
│                                                              │
│  ─────────────────────────────────────────────────────────  │
│                                                              │
│  TECHNIQUE 2: Greedy Estimate                               │
│  ════════════════════════════                               │
│  Use greedy algorithm from current state                    │
│  Greedy gives rough estimate (may not be optimal)           │
│  But fast to compute → usable as bound                      │
│                                                              │
│  Example Knapsack:                                           │
│  • From current state: compute greedy value                 │
│  • Greedy by value/weight ratio                             │
│  • Use as upper bound (greedy may not be optimal)           │
│                                                              │
│  ─────────────────────────────────────────────────────────  │
│                                                              │
│  TECHNIQUE 3: Partial Solution Analysis                     │
│  ═══════════════════════════════════════                    │
│  Analyze current partial solution to estimate completion    │
│                                                              │
│  Example Knapsack:                                           │
│  • Current value = v                                         │
│  • Current capacity used = w                                 │
│  • Remaining capacity = W - w                                │
│  • Compute greedy value for remaining items in W - w        │
│  • Bound = v + greedy(remaining)                            │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

### Segment 2: Traveling Salesman Problem (B&B) (90 min)

#### Problem Statement & B&B Approach

```
┌────────────────────────────────────────────────────────────┐
│    TRAVELING SALESMAN PROBLEM (B&B APPROACH)                │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  PROBLEM:                                                    │
│  ────────                                                    │
│  Given: n cities with distances between each pair           │
│  Find: Shortest tour visiting all cities exactly once       │
│        and returning to start                               │
│                                                              │
│  Example (4 cities):                                         │
│  ┌─────────────────────────────────────────┐                │
│  │ Cities: A, B, C, D                       │                │
│  │ Start at A, visit B,C,D, return to A    │                │
│  │ Distance matrix:                         │                │
│  │       A   B   C   D                      │                │
│  │ A  [  0   2   9   10]                    │                │
│  │ B  [  2   0   6   4 ]                    │                │
│  │ C  [  9   6   0   5 ]                    │                │
│  │ D  [  10  4   5   0 ]                    │                │
│  │                                          │                │
│  │ Possible tours:                          │                │
│  │ A→B→C→D→A: 2+6+5+10 = 23               │                │
│  │ A→B→D→C→A: 2+4+5+9  = 20               │                │
│  │ A→C→B→D→A: 9+6+4+10 = 29               │                │
│  │ ... (n-1)!/2 = 3 unique tours)          │                │
│  │                                          │                │
│  │ Best: A→B→D→C→A with cost 20           │                │
│  │                                          │                │
│  └─────────────────────────────────────────┘                │
│                                                              │
│  WHY HARD?                                                   │
│  ─────────                                                   │
│  Naive: Try all (n-1)! permutations                         │
│  n=10: 362,880 tours                                         │
│  n=20: 121 quintillion tours (impossible!)                  │
│  NP-hard problem                                            │
│                                                              │
│  B&B STRATEGY:                                               │
│  ══════════════                                              │
│  Explore tour-building tree:                                │
│  Root: empty tour                                            │
│  Level i: tour with i cities selected                       │
│  Leaves: complete tours (n cities)                          │
│                                                              │
│  At each node: Compute LOWER BOUND                           │
│  Bound = minimum cost this path can achieve                 │
│  If bound > best_so_far → prune entire subtree!            │
│                                                              │
│  ─────────────────────────────────────────────────────────  │
│                                                              │
│  LOWER BOUND TECHNIQUE: Minimum Spanning Tree                │
│  ═══════════════════════════════════════════                │
│  At partial tour:                                            │
│  • Have partial path visiting some cities                    │
│  • Need to visit remaining cities and return                │
│                                                              │
│  Relax to MST problem:                                       │
│  • Build MST on remaining unvisited cities                  │
│  • Connect to current tour                                  │
│  • Cost(MST path) ≤ Cost(TSP completion)                    │
│                                                              │
│  Lower bound = current_path_cost + MST_cost                 │
│                                                              │
│  Example:                                                    │
│  ┌─────────────────────────────────────────┐                │
│  │ Current tour: A→B (cost = 2)            │                │
│  │ Remaining: C, D                          │                │
│  │                                          │                │
│  │ MST on {C, D, return point}              │                │
│  │ MST cost: 5 (connect C-D) + min edges    │                │
│  │ back to tour                             │                │
│  │                                          │                │
│  │ Lower bound = 2 + (MST_cost)            │                │
│  │                                          │                │
│  │ If lower bound > best_so_far → prune!   │                │
│  │                                          │                │
│  └─────────────────────────────────────────┘                │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

### Segment 3: 0/1 Knapsack with B&B (60 min)

#### B&B for Knapsack

```
┌────────────────────────────────────────────────────────────┐
│     0/1 KNAPSACK WITH BRANCH & BOUND                        │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  PROBLEM RECAP:                                              │
│  ───────────                                                 │
│  Capacity W, n items with weight wᵢ and value vᵢ            │
│  Select items (include/exclude) to maximize value           │
│  such that total weight ≤ W                                 │
│                                                              │
│  BRANCH & BOUND APPROACH:                                    │
│  ═════════════════════════                                  │
│  Decision tree: Include item i? (Yes/No choice)            │
│  Best solution so far: best_value                           │
│                                                              │
│  At each node:                                               │
│  1. Compute UPPER BOUND on value achievable                │
│  2. If bound ≤ best_value → prune (can't improve)          │
│  3. Otherwise: try both choices (include/exclude)           │
│                                                              │
│  ─────────────────────────────────────────────────────────  │
│                                                              │
│  UPPER BOUND TECHNIQUE: Fractional Knapsack                 │
│  ═══════════════════════════════════════════                │
│  Current state:                                              │
│  • Items already included (fixed)                            │
│  • Current value = v                                        │
│  • Current capacity used = w                                │
│  • Remaining capacity = W - w                                │
│                                                              │
│  For remaining items:                                        │
│  • Use FRACTIONAL knapsack (greedy by ratio)               │
│  • This is easier → gives value ≥ 0/1 optimal              │
│  • Use fractional value as upper bound                      │
│                                                              │
│  Upper bound = v + fractional_knapsack(remaining)           │
│                                                              │
│  ┌────────────────────────────────────────────────────────┐ │
│  │ Example:                                                │ │
│  │ Current: v=50, w=6 (out of W=10)                       │ │
│  │ Remaining capacity: 4                                  │ │
│  │ Remaining items: {(w=3,v=40), (w=2,v=20), (w=1,v=5)} │ │
│  │                                                        │ │
│  │ Greedy by value/weight ratio:                          │ │
│  │ (40/3=13.3), (20/2=10), (5/1=5)                        │ │
│  │                                                        │ │
│  │ Take full (w=3,v=40): v+=40, w+=3 (w=9)               │ │
│  │ Capacity left: 1                                       │ │
│  │ Take fraction of (w=2,v=20): 0.5*20 = 10              │ │
│  │                                                        │ │
│  │ Upper bound = 50 + 40 + 10 = 100                       │ │
│  │                                                        │ │
│  │ 0/1 optimal ≤ 100 (fractional ≥ integer)              │ │
│  │                                                        │ │
│  │ If best_value = 95:                                    │ │
│  │ Lower bound 95 ≤ Upper bound 100 → can improve?       │ │
│  │ Explore this branch                                    │ │
│  │                                                        │ │
│  │ If best_value = 100:                                   │ │
│  │ No improvement possible → PRUNE!                       │ │
│  │                                                        │ │
│  └────────────────────────────────────────────────────────┘ │
│                                                              │
│  PRUNING BENEFIT:                                            │
│  ════════════════                                            │
│  Naive DP: O(nW) = O(n * capacity)                          │
│  B&B: Often much faster in practice                         │
│       When bounds are tight, huge pruning                   │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

### Segment 4: Summary (45 min)

#### Day 3 Summary

```
┌────────────────────────────────────────────────────────────┐
│          DAY 3 CONCEPTUAL SUMMARY                           │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  BRANCH & BOUND CONCEPT:                                    │
│  ──────────────────────                                     │
│  ✓ Backtracking + Optimization                              │
│  ✓ Track best solution found so far                         │
│  ✓ Prune branches that can't improve                        │
│  ✓ Key: Compute bounds efficiently                          │
│                                                              │
│  BOUNDING STRATEGIES:                                        │
│  ───────────────────                                        │
│  ✓ Relaxation: Remove constraints, solve optimally         │
│  ✓ Greedy: Fast estimate from current state                │
│  ✓ Partial analysis: Complete & extrapolate                │
│  ✓ Tradeoff: Tight vs loose bounds                         │
│                                                              │
│  TSP EXAMPLE:                                                │
│  ──────────                                                  │
│  ✓ Decision tree: Which cities to visit next                │
│  ✓ Lower bound: MST of remaining cities                     │
│  ✓ Prune when bound > best tour                             │
│                                                              │
│  KNAPSACK EXAMPLE:                                           │
│  ──────────────                                              │
│  ✓ Decision tree: Include/exclude items                     │
│  ✓ Upper bound: Fractional knapsack value                   │
│  ✓ Prune when bound ≤ best value                            │
│                                                              │
│  KEY INSIGHT:                                                │
│  ────────────                                                │
│  Good bounds → Heavy pruning → Fast algorithm               │
│  Bad bounds → Little pruning → Slow (backtracking)          │
│                                                              │
│  WHEN TO USE:                                                │
│  ────────────                                                │
│  • NP-hard optimization problems                            │
│  • When bounds are tight (tight → good pruning)            │
│  • Better than naive backtracking                           │
│  • Often beats DP for specific instances                    │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

---

# 📅 DAY 4: AMORTIZED ANALYSIS

## Time Allocation: 5 hours (300 minutes)

### Segment 1: Why Amortized Analysis? (90 min)

#### Regular Analysis Limitations

```
┌────────────────────────────────────────────────────────────┐
│      WHY AMORTIZED ANALYSIS?                                │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  PROBLEM WITH REGULAR ANALYSIS:                             │
│  ═════════════════════════════════                          │
│  Some operations are expensive sometimes, cheap other times │
│  Regular complexity: "Worst case for single operation"      │
│  May be pessimistic for sequence of operations              │
│                                                              │
│  EXAMPLE: Dynamic Array Append                              │
│  ──────────────────────────────                             │
│  Array size: 4, next space: 5                               │
│                                                              │
│  Append 5: O(1) - just add to next slot                    │
│  Append 6: O(1) - just add                                  │
│  Append 7: O(1) - just add                                  │
│  Append 8: O(1) - just add (now full)                      │
│  Append 9: O(n) - COSTLY!                                   │
│           Need to allocate new array (double size)          │
│           Copy all 8 elements                               │
│           Takes 8 operations, not 1!                        │
│                                                              │
│  ┌──────────────────────────────────────────────────────┐   │
│  │ Naive worst-case analysis:                           │   │
│  │ Single append: O(n)  ← worst case                    │   │
│  │ n appends: n × O(n) = O(n²)  ← LOOKS BAD            │   │
│  │                                                     │   │
│  │ But in reality:                                      │   │
│  │ • Most appends O(1)                                  │   │
│  │ • Doubling rare (only n/2 operations are expansions)│   │
│  │ • Total work: O(n)  ← BETTER!                       │   │
│  │                                                     │   │
│  │ Regular analysis pessimistic!                        │   │
│  │ Amortized analysis gives accurate average!          │   │
│  │                                                     │   │
│  └──────────────────────────────────────────────────────┘   │
│                                                              │
│  WHAT AMORTIZED ANALYSIS DOES:                              │
│  ═══════════════════════════════                            │
│  Analyzes SEQUENCE of operations                            │
│  Computes AVERAGE cost over sequence                        │
│  Even if individual operations vary                        │
│                                                              │
│  Result: amortized_cost per operation                       │
│  Multiply by n operations → total cost accurate             │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

### Segment 2: Three Amortized Analysis Methods (120 min)

#### Method 1: Aggregate Analysis

```
┌────────────────────────────────────────────────────────────┐
│      AGGREGATE ANALYSIS METHOD                              │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  PROCESS:                                                    │
│  ═══════                                                     │
│  1. Compute TOTAL cost for n operations                     │
│  2. Divide by n                                              │
│  3. Result = amortized cost per operation                   │
│                                                              │
│  Total cost / n = Amortized cost per op                    │
│                                                              │
│  ─────────────────────────────────────────────────────────  │
│                                                              │
│  DYNAMIC ARRAY EXAMPLE:                                      │
│  ═══════════════════════                                     │
│  Start: Array size 1                                        │
│  Do n append operations                                      │
│  Compute total cost                                         │
│                                                              │
│  Cost analysis:                                              │
│  ┌────────────────────────────────────────────────────────┐ │
│  │ Operation     Cost           Frequency                 │ │
│  │                                                        │ │
│  │ Append (no   O(1)   n - O(log n) times              │ │
│  │ resize)                                                │ │
│  │                                                        │ │
│  │ Resize       O(n)   O(log n) times                   │ │
│  │              (copy all items)                         │ │
│  │                                                        │ │
│  │ Resize costs: 1 + 2 + 4 + 8 + ... + n                │ │
│  │             = 2n - 1 ≈ O(n)                          │ │
│  │                                                        │ │
│  │ Total cost:                                            │ │
│  │ = (n - log n) × 1  +  (2n - 1)                       │ │
│  │ ≈ n + 2n                                              │ │
│  │ = O(n)                                                │ │
│  │                                                        │ │
│  │ Amortized cost per append:                             │ │
│  │ = Total O(n) / n operations                           │ │
│  │ = O(1)                                                │ │
│  │                                                        │ │
│  └────────────────────────────────────────────────────────┘ │
│                                                              │
│  KEY INSIGHT:                                                │
│  ────────────                                                │
│  Although single append can be O(n),                        │
│  average over n appends is O(1)!                            │
│                                                              │
│  Reason: Expensive resizes rare, cost amortized             │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

#### Method 2: Accounting Method

```
┌────────────────────────────────────────────────────────────┐
│         ACCOUNTING METHOD                                   │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  IDEA:                                                       │
│  ════                                                        │
│  Assign "credit" to each operation                          │
│  Credit ≥ actual cost                                        │
│  Use credit to pay for future expensive operations          │
│  Keep "bank account" never negative                         │
│                                                              │
│  PROCESS:                                                    │
│  ───────                                                     │
│  1. Assign amortized cost to each operation                 │
│  2. If amortized > actual → save credit (deposit)           │
│  3. If amortized < actual → use saved credit (withdraw)    │
│  4. If balance never goes negative → analysis valid ✓       │
│                                                              │
│  ─────────────────────────────────────────────────────────  │
│                                                              │
│  DYNAMIC ARRAY EXAMPLE:                                      │
│  ═══════════════════════                                     │
│  Assign amortized cost = 3 per append                        │
│                                                              │
│  ┌────────────────────────────────────────────────────────┐ │
│  │ Operation        Actual Cost   Amortized   Credit    │ │
│  │                                                       │ │
│  │ Append to size 1       1          3         +2       │ │
│  │ Bank: 2 credits                                      │ │
│  │                                                       │ │
│  │ Append to size 2       1          3         +2       │ │
│  │ Bank: 4 credits                                      │ │
│  │                                                       │ │
│  │ Append to size 3       1          3         +2       │ │
│  │ Bank: 6 credits                                      │ │
│  │                                                       │ │
│  │ Append → Resize        4          3         -1 (use) │ │
│  │ (copy 4 items)                                       │ │
│  │ Bank: 5 credits  ✓ still positive                    │ │
│  │                                                       │ │
│  │ Append to size 5       1          3         +2       │ │
│  │ Bank: 7 credits                                      │ │
│  │                                                       │ │
│  │ ... continue ...                                      │ │
│  │                                                       │ │
│  │ Invariant: Bank ≥ 0 throughout                       │ │
│  │ Amortized cost 3 per operation valid! ✓              │ │
│  │                                                       │ │
│  └────────────────────────────────────────────────────────┘ │
│                                                              │
│  INTUITION:                                                  │
│  ──────────                                                  │
│  • Cheap operations prepay for expensive ones               │
│  • Like "savings account" for future costs                  │
│  • If balance never negative → bound is valid               │
│                                                              │
│  PROVING:                                                    │
│  ───────                                                     │
│  1. Pick amortized cost estimate                            │
│  2. For each operation: amortized ≥ actual + impact on     │
│     future (change in state)                               │
│  3. Show bank balance ≥ 0 after each operation             │
│  4. If successful → amortized cost is correct ✓            │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

#### Method 3: Potential Method

```
┌────────────────────────────────────────────────────────────┐
│         POTENTIAL METHOD                                    │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  IDEA:                                                       │
│  ════                                                        │
│  Define POTENTIAL FUNCTION on data structure state          │
│  Potential measures "stored energy" or readiness for future │
│                                                              │
│  Amortized cost = Actual cost + Change in potential         │
│                                                              │
│  ─────────────────────────────────────────────────────────  │
│                                                              │
│  FORMAL DEFINITION:                                          │
│  ═══════════════════                                         │
│  Potential function: Φ(state) → real number                 │
│                                                              │
│  aᵢ = cᵢ + Φ(stateᵢ) - Φ(stateᵢ₋₁)                           │
│                                                              │
│  where:                                                      │
│  cᵢ = actual cost of operation i                            │
│  Φ(stateᵢ) = potential after operation i                    │
│  Φ(stateᵢ₋₁) = potential before operation i                 │
│  aᵢ = amortized cost of operation i                         │
│                                                              │
│  Total amortized cost:                                       │
│  Σ aᵢ = Σ cᵢ + (Φ(final) - Φ(initial))                      │
│  = Total actual + Potential change                          │
│                                                              │
│  ─────────────────────────────────────────────────────────  │
│                                                              │
│  DYNAMIC ARRAY EXAMPLE:                                      │
│  ═══════════════════════                                     │
│  Define Φ(state) = 2 × (number of elements) - (array size) │
│                                                              │
│  Initially: 0 elements, size 1                              │
│  Φ(initial) = 2(0) - 1 = -1                                │
│                                                              │
│  ┌────────────────────────────────────────────────────────┐ │
│  │ After 1 append: 1 element, size 1                    │ │
│  │ Φ = 2(1) - 1 = 1                                     │ │
│  │ ΔΦ = 1 - (-1) = 2                                    │ │
│  │ c₁ = 1 (just add element)                            │ │
│  │ a₁ = 1 + 2 = 3                                       │ │
│  │                                                       │ │
│  │ After 2 appends: 2 elements, size 2                  │ │
│  │ Φ = 2(2) - 2 = 2                                     │ │
│  │ ΔΦ = 2 - 1 = 1                                       │ │
│  │ c₂ = 1 (just add element, no resize)                 │ │
│  │ a₂ = 1 + 1 = 2                                       │ │
│  │                                                       │ │
│  │ After 4 appends (resize occurs): 4 elements, size 4  │ │
│  │ Previous: 3 elements, size 2                         │ │
│  │ Previous Φ = 2(3) - 2 = 4                            │ │
│  │ After Φ = 2(4) - 4 = 4                               │ │
│  │ ΔΦ = 4 - 4 = 0                                       │ │
│  │ c₄ = 3 (copy 3 items + add 1)                        │ │
│  │ a₄ = 3 + 0 = 3                                       │ │
│  │                                                       │ │
│  │ Amortized cost = 3 per operation on average          │ │
│  │                                                       │ │
│  └────────────────────────────────────────────────────────┘ │
│                                                              │
│  KEY INSIGHT:                                                │
│  ────────────                                                │
│  Potential increases when array has room (cheap appends)    │
│  Potential decreases when array fills (expensive resize)    │
│  Change in potential "pays for" expensive operations        │
│                                                              │
│  PROPERTIES NEEDED:                                          │
│  ──────────────────                                          │
│  1. Φ(initial) ≤ Φ(final) in steady state                  │
│     (potential doesn't decrease overall)                    │
│  2. Φ(state) ≥ 0 for all states                            │
│     (potential never negative)                              │
│  3. aᵢ > 0 for all i                                        │
│     (amortized cost positive)                               │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

### Segment 3: Comparison of Methods (60 min)

#### When to Use Which Method

```
┌────────────────────────────────────────────────────────────┐
│      COMPARISON OF THREE METHODS                            │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  AGGREGATE ANALYSIS:                                         │
│  ══════════════════                                          │
│  Simplest approach                                           │
│  Good for: Overall cost is straightforward to compute       │
│                                                              │
│  Strengths:                                                  │
│  • Easiest to understand                                     │
│  • Direct computation                                        │
│  • No auxiliary concepts                                     │
│                                                              │
│  Weaknesses:                                                 │
│  • Only gives single amortized cost for all ops            │
│  • Can't analyze different operations separately             │
│  • Doesn't reveal structure of costs                         │
│                                                              │
│  Best for: All operations have similar "profile"           │
│                                                              │
│  ─────────────────────────────────────────────────────────  │
│                                                              │
│  ACCOUNTING METHOD:                                          │
│  ═════════════════                                           │
│  Intuitive, like a savings account                          │
│  Good for: Separating cheap vs expensive ops                │
│                                                              │
│  Strengths:                                                  │
│  • Assigns different costs to different operations         │
│  • Intuitive "bank account" analogy                         │
│  • Easy to verify (check bank stays ≥ 0)                   │
│                                                              │
│  Weaknesses:                                                 │
│  • Need to correctly assign amortized costs                 │
│  • More bookkeeping                                          │
│  • Might need trial-and-error to find right costs           │
│                                                              │
│  Best for: Multiple operation types, data structure changes │
│                                                              │
│  ─────────────────────────────────────────────────────────  │
│                                                              │
│  POTENTIAL METHOD:                                           │
│  ════════════════                                            │
│  Most powerful but requires insight                          │
│  Good for: Complex state changes                            │
│                                                              │
│  Strengths:                                                  │
│  • Works for any state measure (potential function)         │
│  • Can handle complex interdependencies                     │
│  • Mathematically elegant                                    │
│                                                              │
│  Weaknesses:                                                 │
│  • Requires finding right potential function                │
│  • Hardest to understand initially                          │
│  • More theoretical/abstract                                │
│                                                              │
│  Best for: Splay trees, Fibonacci heaps, complex structures │
│                                                              │
│  ─────────────────────────────────────────────────────────  │
│                                                              │
│  DECISION GUIDE:                                             │
│  ═══════════════                                             │
│  Simple problem → Aggregate                                 │
│  Multiple op types → Accounting                             │
│  Complex structure → Potential                              │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

### Segment 4: Summary (30 min)

#### Day 4 Summary

```
┌────────────────────────────────────────────────────────────┐
│          DAY 4 CONCEPTUAL SUMMARY                           │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  AMORTIZED ANALYSIS PURPOSE:                                │
│  ──────────────────────────                                 │
│  ✓ Analyze AVERAGE cost over sequence                       │
│  ✓ More accurate than worst-case per operation              │
│  ✓ Some ops expensive, others cheap                         │
│  ✓ Total amortized more realistic                           │
│                                                              │
│  AGGREGATE ANALYSIS:                                         │
│  ──────────────────                                         │
│  ✓ Total cost / number of operations                        │
│  ✓ Simplest approach                                        │
│  ✓ Single amortized cost for all                            │
│  ✓ Example: Dynamic array O(1) amortized                   │
│                                                              │
│  ACCOUNTING METHOD:                                          │
│  ────────────────                                           │
│  ✓ Assign credit to each operation                          │
│  ✓ Bank account analogy                                     │
│  ✓ Cheap ops save credit, expensive ops spend              │
│  ✓ Balance stays ≥ 0 → analysis valid                      │
│                                                              │
│  POTENTIAL METHOD:                                           │
│  ────────────────                                           │
│  ✓ Define potential function on state                       │
│  ✓ Amortized = actual + Δ potential                         │
│  ✓ Most general and powerful                                │
│  ✓ Potential "energy" of structure                          │
│                                                              │
│  KEY EXAMPLES:                                               │
│  ────────────                                                │
│  • Dynamic array: O(1) amortized append                     │
│  • Stack push/pop: O(1) amortized                           │
│  • Splay tree: O(log n) amortized                           │
│  • Fibonacci heap: O(1) amortized operations                │
│                                                              │
│  WHEN USEFUL:                                                │
│  ────────────                                                │
│  • Justifies design choices (doubling vs linear growth)     │
│  • Explains performance in practice                         │
│  • More accurate than worst-case analysis                   │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

---

# 📅 DAY 5: MIXED PARADIGM INTEGRATION

## Time Allocation: 5 hours (300 minutes)

### Segment 1: When to Use Which Paradigm (90 min)

#### Decision Framework

```
┌────────────────────────────────────────────────────────────┐
│     PARADIGM SELECTION DECISION TREE                        │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  Problem given                                               │
│    ↓                                                         │
│  Goal: Single optimal or ALL solutions?                      │
│  ├─ All solutions, no optimization goal                     │
│  │   └─ Use BACKTRACKING                                    │
│  │       Example: N-Queens (all placements)                 │
│  │                 Permutations, Combinations               │
│  │                                                           │
│  └─ Single optimal solution                                 │
│     ↓                                                         │
│     Can you make greedy choice locally?                      │
│     ├─ YES (obvious locally best)                            │
│     │   └─ TRY GREEDY                                        │
│     │       Can you prove greedy property?                  │
│     │       ├─ YES → Use GREEDY ✓                            │
│     │       └─ NO → Try DP or B&B                           │
│     │                                                         │
│     └─ NO (no clear local choice)                            │
│        ├─ Overlapping subproblems?                           │
│        │   └─ YES → Use DYNAMIC PROGRAMMING                 │
│        │       Example: 0/1 Knapsack, LCS, LDS             │
│        │                                                     │
│        └─ Combinatorial search needed                        │
│            └─ Use BRANCH & BOUND                            │
│                Example: TSP, Large 0/1 Knapsack            │
│                Backtrack with pruning & bounds              │
│                                                              │
│  Special case: NP-hard optimization                         │
│    └─ Need approximation → Greedy approximation             │
│        Example: Set Cover with O(log n) guarantee           │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

#### Problem Classification Matrix

```
┌────────────────────────────────────────────────────────────┐
│       PROBLEM CLASSIFICATION MATRIX                         │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  PROBLEM STRUCTURE  │ GOAL           │ BEST PARADIGM        │
│  ─────────────────────────────────────────────────────────  │
│                                                              │
│  Local choice       │ Any solution   │ Greedy (if provable) │
│  obvious            │                │                      │
│                     │                │                      │
│  No local choice    │ Single optimal │ DP (if subproblems) │
│  overlapping        │                │                      │
│  subproblems        │                │                      │
│                     │                │                      │
│  No local choice    │ Single optimal │ B&B (with bounds)   │
│  unique             │                │                      │
│  subproblems        │                │ Or DP if small      │
│                     │                │                      │
│  Multiple choices   │ All solutions  │ Backtracking        │
│  to try             │                │ (with pruning)       │
│                     │                │                      │
│  Constraint         │ Any solution   │ CSP solver or       │
│  satisfaction       │ satisfying     │ Backtracking        │
│  problem            │ constraints    │                      │
│                     │                │                      │
│  NP-hard            │ Approximate    │ Greedy              │
│  optimization       │ + bound        │ Approximation       │
│                     │                │                      │
│  Fast response      │ Good enough    │ Heuristic (greedy)  │
│  needed             │ (not optimal)  │ or relaxation       │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

### Segment 2: Hybrid Algorithms (90 min)

#### Combining Paradigms

```
┌────────────────────────────────────────────────────────────┐
│       HYBRID ALGORITHMS: COMBINING PARADIGMS                │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  CONCEPT:                                                    │
│  ═══════                                                     │
│  Use multiple paradigms together                            │
│  Each handles different aspect of problem                   │
│                                                              │
│  ─────────────────────────────────────────────────────────  │
│                                                              │
│  PATTERN 1: GREEDY + BACKTRACKING                            │
│  ═══════════════════════════════════                         │
│  Use greedy to prune backtracking                            │
│                                                              │
│  Example: N-Queens optimization                             │
│  • Goal: Find ANY solution (not all)                        │
│  • Use greedy heuristic: place queen in least-conflicted   │
│    column                                                    │
│  • If greedy fails: backtrack                               │
│  • Often finds solution faster than pure backtracking       │
│                                                              │
│  ┌────────────────────────────────────────────────────────┐ │
│  │ Standard backtracking: O(n!) worst case                │ │
│  │ Greedy-guided: Often finds answer quickly              │ │
│  │ Not optimal but good heuristic                         │ │
│  │                                                        │ │
│  └────────────────────────────────────────────────────────┘ │
│                                                              │
│  ─────────────────────────────────────────────────────────  │
│                                                              │
│  PATTERN 2: DP + GREEDY                                      │
│  ═════════════════════════                                   │
│  Use greedy choices, verify with DP                          │
│                                                              │
│  Example: Weighted activity selection                       │
│  • Greedy by profit/weight doesn't work                    │
│  • Use DP to compute optimal                               │
│  • Greedy gives lower bound for verification               │
│                                                              │
│  ─────────────────────────────────────────────────────────  │
│                                                              │
│  PATTERN 3: B&B + DP                                         │
│  ════════════════════                                        │
│  Use DP to compute bounds for B&B                           │
│                                                              │
│  Example: Traveling Salesman with B&B                       │
│  • Partial tour cost: known exactly                         │
│  • Remaining cities: use DP approximation as lower bound    │
│  • Tighter bound → better pruning                           │
│                                                              │
│  ─────────────────────────────────────────────────────────  │
│                                                              │
│  PATTERN 4: BACKTRACKING + DP                                │
│  ════════════════════════════                               │
│  Use DP for subproblems within backtracking                 │
│                                                              │
│  Example: Sudoku with candidate sets                        │
│  • Backtrack cell by cell                                   │
│  • For each cell: use constraint propagation (form of DP)   │
│  • Reduces candidates to try                                │
│                                                              │
│  ─────────────────────────────────────────────────────────  │
│                                                              │
│  PATTERN 5: GREEDY + DP + BACKTRACKING                       │
│  ═════════════════════════════════════                       │
│  Full hybrid for hard problems                              │
│                                                              │
│  Example: Resource scheduling                               │
│  • Greedy for initial schedule                              │
│  • DP for subproblem optimization                           │
│  • Backtrack if greedy+DP violates constraint               │
│                                                              │
│  ┌────────────────────────────────────────────────────────┐ │
│  │ Algorithm structure:                                   │ │
│  │ 1. Try greedy choice                                   │ │
│  │ 2. Compute DP for remaining subproblem                 │ │
│  │ 3. If infeasible or suboptimal: backtrack             │ │
│  │ 4. Try next alternative                                │ │
│  │                                                        │ │
│  │ Combines strengths of all three paradigms!             │ │
│  │                                                        │ │
│  └────────────────────────────────────────────────────────┘ │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

### Segment 3: Complex Problem Solving (90 min)

#### Case Study: Large-Scale TSP

```
┌────────────────────────────────────────────────────────────┐
│    CASE STUDY: TRAVELING SALESMAN (HYBRID APPROACH)         │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  PROBLEM:                                                    │
│  ────────                                                    │
│  50 cities, find shortest tour                              │
│  Exact DP: O(2^n * n²) = prohibitive                       │
│  Exact B&B: May work with good bounds                       │
│  Greedy: Fast but suboptimal                                │
│                                                              │
│  HYBRID SOLUTION:                                            │
│  ════════════════                                            │
│                                                              │
│  Step 1: Initial Greedy Solution                             │
│  ────────────────────────────                               │
│  • Use nearest-neighbor greedy                              │
│  • Pick random starting city                                │
│  • Always go to nearest unvisited city                      │
│  • Complete tour in O(n²)                                   │
│  • Result: feasible but suboptimal tour                     │
│                                                              │
│  Step 2: Local Optimization with DP                         │
│  ────────────────────────────────────                       │
│  • Take greedy tour                                         │
│  • Divide into overlapping segments                         │
│  • Use DP to re-optimize each segment                       │
│  • 2-opt: try reversing segments (local search)            │
│  • Improves tour iteratively                                │
│                                                              │
│  Step 3: B&B for Refinement                                  │
│  ───────────────────────────                                │
│  • Use greedy solution as upper bound                       │
│  • Run B&B with this bound                                  │
│  • Lower bounds: MST of remaining cities                    │
│  • Heavy pruning because greedy bound tight                 │
│  • Finds good/optimal solution                              │
│                                                              │
│  ┌────────────────────────────────────────────────────────┐ │
│  │ Algorithm flow:                                         │ │
│  │                                                        │ │
│  │ Greedy      →  DP local      →  B&B refinement         │ │
│  │ O(n²)          O(n³)             Varies                │ │
│  │ Feasible       Better            Near-optimal          │ │
│  │ Time:          Fast              Slower                │ │
│  │                                                        │ │
│  │ Result: Good solution in reasonable time               │ │
│  │                                                        │ │
│  └────────────────────────────────────────────────────────┘ │
│                                                              │
│  WHY HYBRID WORKS:                                           │
│  ═════════════════                                           │
│  • Greedy provides structure quickly                        │
│  • DP improves locally without full exploration             │
│  • B&B uses greedy as strong bound, prunes heavily         │
│  • Combined: reasonable time, good quality                  │
│                                                              │
│  WITHOUT HYBRID:                                             │
│  ───────────────                                             │
│  • Pure greedy: Fast but poor quality                        │
│  • Pure DP: Too slow for n=50                              │
│  • Pure B&B: Too slow without good bounds                   │
│                                                              │
│  WITH HYBRID:                                                │
│  ───────────                                                 │
│  • Takes best from each paradigm                            │
│  • Fast and good quality achieved                           │
│  • Practical for real-world problems                        │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

### Segment 4: Summary & Week Synthesis (60 min)

#### Week 13 Complete Summary

```
┌────────────────────────────────────────────────────────────┐
│         WEEK 13 COMPLETE SYNTHESIS                          │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  DAY 1: BACKTRACKING FUNDAMENTALS                            │
│  ──────────────────────────────────                          │
│  ✓ Systematic exploration of ALL solutions                  │
│  ✓ State space tree representation                          │
│  ✓ DFS traversal with pruning                               │
│  ✓ Universal template: try/recurse/undo                    │
│                                                              │
│  DAY 2: COMPLEX BACKTRACKING                                 │
│  ──────────────────────────                                 │
│  ✓ N-Queens: row-by-row placement, diagonal checking       │
│  ✓ Sudoku: cell-by-cell filling, candidate tracking        │
│  ✓ Permutations: all orderings with available set          │
│  ✓ Combinations: all k-subsets with index-based approach   │
│                                                              │
│  DAY 3: BRANCH & BOUND                                       │
│  ──────────────────────                                     │
│  ✓ Backtracking + optimization                              │
│  ✓ Track best solution, prune branches that can't improve   │
│  ✓ Bounding strategies: relaxation, greedy, partial        │
│  ✓ TSP & knapsack examples                                  │
│                                                              │
│  DAY 4: AMORTIZED ANALYSIS                                   │
│  ─────────────────────────                                  │
│  ✓ Average cost over sequence (not worst case)              │
│  ✓ Aggregate: total / n operations                          │
│  ✓ Accounting: credit bank account analogy                  │
│  ✓ Potential: state energy function                         │
│                                                              │
│  DAY 5: PARADIGM INTEGRATION                                 │
│  ────────────────────────────                               │
│  ✓ Decision tree for algorithm selection                    │
│  ✓ Hybrid algorithms combining multiple paradigms           │
│  ✓ Case study: TSP with greedy + DP + B&B                  │
│                                                              │
│  ─────────────────────────────────────────────────────────  │
│                                                              │
│  WEEK 12-13 SYNTHESIS: Algorithm Paradigms                  │
│  ═══════════════════════════════════════                    │
│                                                              │
│  WEEK 12 (GREEDY):                                           │
│  → Fast, commit to choices                                   │
│  → Optimal only for special problems (must prove!)          │
│  → Applications: MST, activity selection, knapsack          │
│                                                              │
│  WEEK 13 (BACKTRACKING & B&B):                               │
│  → Systematic exploration, can undo choices                 │
│  → Finds any/all solutions                                  │
│  → Applications: N-Queens, Sudoku, combinatorics            │
│  → Branch & Bound: adds optimization to backtracking       │
│                                                              │
│  PARADIGM SELECTION:                                         │
│  ────────────────                                            │
│  • Greedy: Obvious local choice, can prove correctness     │
│  • DP: Overlapping subproblems, optimal substructure        │
│  • Backtracking: All solutions, constraints                │
│  • B&B: Optimization with large search space                │
│  • Hybrid: Combine for complex problems                     │
│                                                              │
│  ─────────────────────────────────────────────────────────  │
│                                                              │
│  PROBLEM-SOLVING METHODOLOGY:                                │
│  ═════════════════════════════                              │
│                                                              │
│  1. CHARACTERIZE PROBLEM                                     │
│     • What is goal? (optimal, all solutions, satisfactory)  │
│     • What is structure? (constraints, subproblems)         │
│                                                              │
│  2. SELECT PARADIGM                                          │
│     • Check decision tree                                    │
│     • Consider problem size/time constraints                 │
│                                                              │
│  3. DESIGN ALGORITHM                                         │
│     • Use appropriate template                              │
│     • Plan pruning/optimization                             │
│                                                              │
│  4. IMPLEMENT & TEST                                         │
│     • Code carefully                                        │
│     • Test edge cases                                       │
│                                                              │
│  5. ANALYZE COMPLEXITY                                       │
│     • Time & space                                          │
│     • Best/avg/worst case                                   │
│     • Amortized if applicable                               │
│                                                              │
│  ─────────────────────────────────────────────────────────  │
│                                                              │
│  KEY INSIGHTS:                                               │
│  ═════════════                                               │
│                                                              │
│  ► Greedy FAST but risky                                    │
│    Must prove before using                                  │
│                                                              │
│  ► Backtracking COMPLETE but slow                            │
│    Pruning critical for performance                         │
│                                                              │
│  ► B&B OPTIMIZED backtracking                               │
│    Combines exploration with optimization                   │
│                                                              │
│  ► Amortized analysis REVEALS true cost                     │
│    Different from worst-case per operation                  │
│                                                              │
│  ► Hybrid algorithms LEVERAGE paradigms                     │
│    Fast initial + optimization + refinement                │ │
│                                                              │
│  ► Different problems → Different solutions                 │
│    No one-size-fits-all algorithm!                          │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

---

# 📋 ASSESSMENT & CHECKPOINTS

## Day-by-Day Checkpoint Questions

### Day 1 Checkpoint
1. What is backtracking fundamentally?
2. Explain state space tree with an example
3. How does pruning improve performance?

### Day 2 Checkpoint
1. How does N-Queens use backtracking?
2. What constraints must be checked?
3. How do permutations differ from combinations?

### Day 3 Checkpoint
1. What distinguishes B&B from pure backtracking?
2. How does a lower bound improve pruning?
3. Why is MST useful for TSP?

### Day 4 Checkpoint
1. Why is amortized analysis needed?
2. Explain aggregate analysis process
3. How does potential method work?

### Day 5 Checkpoint
1. When would you use each paradigm?
2. How can greedy and backtracking combine?
3. Why does hybrid approach work for TSP?

## Weekly Assessment Rubric

**Concept Mastery (80 points):**
- Understand backtracking template (15)
- Understand branch & bound (15)
- Understand amortized analysis methods (20)
- Understand paradigm selection (15)
- Apply to problems (15)

**Problem Solving (20 points):**
- Identify algorithm type for problem (10)
- Design appropriate solution (10)

---

**Week 13 Complete! PHASE D (Algorithm Paradigms) Mastered!**

---

# 🎓 50-HOUR COURSE COMPLETION

**Weeks 12-13 comprehensive learning achieved!**

This playbook represents complete conceptual mastery preparation for:
- Coding interviews at FAANG companies
- Advanced data structures courses
- Algorithm design competitions
- Real-world optimization problems

---

**Next Phase: Advanced Topics (Weeks 14-15) - Graph Algorithms & Advanced DP**

---
