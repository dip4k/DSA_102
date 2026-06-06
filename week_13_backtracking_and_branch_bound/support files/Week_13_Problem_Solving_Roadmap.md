# 🗺️ WEEK 13 PROBLEM SOLVING ROADMAP

**Phase:** D – Algorithm Paradigms  
**Week Theme:** Backtracking & Branch & Bound  
**Syllabus Source:** COMPLETE_SYLLABUS_v13_FINAL.md  
**Tone:** Training Coach

---

## 🎯 ROADMAP OVERVIEW

This roadmap provides a **3-stage progressive problem ladder** designed to build mastery of backtracking, branch & bound, and amortized analysis through deliberate practice.

### Learning Philosophy

**Stage 1: Canonical Problems (Foundation)**
- Master core patterns with classic problems
- Build template muscle memory
- Understand fundamental mechanics

**Stage 2: Variations (Pattern Recognition)**
- Apply patterns to modified problems
- Recognize disguised applications
- Handle constraint variations

**Stage 3: Integration (Mastery)**
- Combine multiple patterns
- Solve novel complex problems
- Apply to real-world scenarios

### Success Metrics

- **Beginner:** Complete Stage 1 (5-8 problems)
- **Intermediate:** Complete Stages 1-2 (12-15 problems)
- **Advanced:** Complete all stages (20+ problems)

---

## 📋 BACKTRACKING PATTERN FRAMEWORK

### Pattern Recognition Triggers

| Problem Signal | Pattern | Template to Apply |
|----------------|---------|-------------------|
| "Generate all..." | Exhaustive enumeration | Backtracking with recording |
| "Find all valid..." | Constraint satisfaction | Backtracking with pruning |
| "Placement with constraints" | State space search | Backtracking with constraint checking |
| "Path finding in grid" | DFS with backtracking | Mark visited, unmark on return |
| "Combinations/permutations" | Combinatorial generation | Used array or start index |

### Universal Backtracking Template

```
function backtrack(state, choices, result):
    # Base case: complete solution
    if is_complete(state):
        result.add(copy(state))  # Must copy!
        return
    
    # Recursive case: try each choice
    for choice in choices:
        if is_valid(state, choice):  # Constraint check
            # Make choice
            make_choice(state, choice)
            
            # Recurse
            backtrack(state, remaining_choices, result)
            
            # Undo choice (CRITICAL!)
            undo_choice(state, choice)
```

**Critical Components:**
1. **State:** Current partial solution
2. **Choices:** Decisions available at this step
3. **Constraints:** Validity checks (`is_valid`)
4. **Restoration:** Undo choice after recursion

---

## 🚀 STAGE 1: CANONICAL PROBLEMS (Foundation)

### Problem 1.1: Generate All Subsets
**Difficulty:** Easy  
**Pattern:** Exhaustive enumeration with backtracking  
**LeetCode:** #78

**Problem Statement:**
Given an integer array `nums` of unique elements, return all possible subsets (the power set).

**Example:**
```
Input: nums = [1,2,3]
Output: [[],[1],[2],[1,2],[3],[1,3],[2,3],[1,2,3]]
```

**Pattern Application:**
- State: current subset
- Choices: include or exclude each element
- Complete: all elements considered
- No constraints (all subsets valid)

**Key Learning:**
- Basic backtracking template
- State modification and restoration
- Recording solutions (copy required)

**Time Complexity:** O(2^n × n)  
**Space Complexity:** O(n) for recursion stack

---

### Problem 1.2: Generate All Permutations
**Difficulty:** Medium  
**Pattern:** Combinatorial generation with used tracking  
**LeetCode:** #46

**Problem Statement:**
Given an array `nums` of distinct integers, return all possible permutations.

**Example:**
```
Input: nums = [1,2,3]
Output: [[1,2,3],[1,3,2],[2,1,3],[2,3,1],[3,1,2],[3,2,1]]
```

**Pattern Application:**
- State: current permutation + used array
- Choices: any unused element
- Complete: all elements used (length = n)
- Constraint: element not already used

**Key Learning:**
- Used array for tracking
- Difference from combinations (order matters)

**Time Complexity:** O(n! × n)  
**Space Complexity:** O(n)

---

### Problem 1.3: Combinations
**Difficulty:** Medium  
**Pattern:** Combinatorial generation with start index  
**LeetCode:** #77

**Problem Statement:**
Given two integers `n` and `k`, return all possible combinations of `k` numbers chosen from the range [1, n].

**Example:**
```
Input: n = 4, k = 2
Output: [[1,2],[1,3],[1,4],[2,3],[2,4],[3,4]]
```

**Pattern Application:**
- State: current combination + start index
- Choices: numbers from start to n
- Complete: k elements selected
- Constraint: no duplicates (use start index)

**Key Learning:**
- Start index prevents duplicate combinations
- Difference from permutations (order doesn't matter)

**Time Complexity:** O(C(n,k) × k)  
**Space Complexity:** O(k)

---

### Problem 1.4: N-Queens
**Difficulty:** Hard  
**Pattern:** Constraint satisfaction with pruning  
**LeetCode:** #51

**Problem Statement:**
Place `n` queens on an `n×n` chessboard so that no two queens attack each other (same row, column, or diagonal).

**Example:**
```
Input: n = 4
Output: [
  [".Q..","...Q","Q...","..Q."],
  ["..Q.","Q...","...Q",".Q.."]
]
```

**Pattern Application:**
- State: queen positions (column-by-column)
- Choices: rows for current column
- Complete: n queens placed (all columns filled)
- Constraints: row, diagonal conflicts

**Key Learning:**
- Column-by-column placement strategy
- Diagonal tracking with row±col
- Efficient constraint checking (O(1) with sets)

**Time Complexity:** O(n!)  
**Space Complexity:** O(n)

---

### Problem 1.5: Sudoku Solver
**Difficulty:** Hard  
**Pattern:** Constraint satisfaction with three constraints  
**LeetCode:** #37

**Problem Statement:**
Fill a 9×9 Sudoku grid so that each row, column, and 3×3 sub-box contains digits 1-9 exactly once.

**Pattern Application:**
- State: partially filled grid
- Choices: digits 1-9 for empty cell
- Complete: all cells filled
- Constraints: row, column, box uniqueness

**Key Learning:**
- Three independent constraints
- Efficient constraint checking with sets
- Early termination (return on first solution)

**Time Complexity:** O(9^(empty_cells))  
**Space Complexity:** O(empty_cells)

---

### Problem 1.6: Word Search
**Difficulty:** Medium  
**Pattern:** Grid DFS with backtracking  
**LeetCode:** #79

**Problem Statement:**
Given a 2D board and a word, find if the word exists in the grid. The word can be constructed from adjacent cells (up, down, left, right).

**Example:**
```
Input: board = [["A","B","C"],["S","F","E"],["A","D","E"]], word = "SEE"
Output: true
```

**Pattern Application:**
- State: current position + word index
- Choices: 4 directions (up, down, left, right)
- Complete: entire word matched
- Constraints: in bounds, not visited, character match

**Key Learning:**
- Mark visited to prevent cycles
- Unmark during backtracking (allow reuse in other paths)
- Character-by-character matching

**Time Complexity:** O(rows × cols × 4^word_length)  
**Space Complexity:** O(word_length)

---

### Problem 1.7: Combination Sum
**Difficulty:** Medium  
**Pattern:** Backtracking with repeated elements  
**LeetCode:** #39

**Problem Statement:**
Given an array of distinct integers `candidates` and a target `target`, return all unique combinations where the candidate numbers sum to `target`. Same number may be chosen unlimited times.

**Example:**
```
Input: candidates = [2,3,6,7], target = 7
Output: [[2,2,3],[7]]
```

**Pattern Application:**
- State: current combination + remaining target
- Choices: all candidates (can reuse)
- Complete: target = 0 (exact sum reached)
- Constraint: sum ≤ target (prune if exceeds)

**Key Learning:**
- Allow repeated element selection
- Pruning with remaining target
- Avoiding duplicates with start index

**Time Complexity:** O(2^target)  
**Space Complexity:** O(target)

---

### Problem 1.8: Palindrome Partitioning
**Difficulty:** Medium  
**Pattern:** Backtracking with substring validation  
**LeetCode:** #131

**Problem Statement:**
Given a string `s`, partition `s` such that every substring is a palindrome. Return all possible palindrome partitioning.

**Example:**
```
Input: s = "aab"
Output: [["a","a","b"],["aa","b"]]
```

**Pattern Application:**
- State: current partition (list of substrings)
- Choices: all possible next substrings (from current index)
- Complete: entire string partitioned
- Constraint: each substring is palindrome

**Key Learning:**
- Substring partitioning pattern
- Palindrome checking as constraint
- Start index to track progress

**Time Complexity:** O(2^n × n)  
**Space Complexity:** O(n)

---

## 🔄 STAGE 2: VARIATIONS (Pattern Recognition)

### Problem 2.1: Subsets II (With Duplicates)
**Difficulty:** Medium  
**Pattern:** Backtracking with duplicate handling  
**LeetCode:** #90

**Variation:** Input array may contain duplicates; result must not contain duplicate subsets.

**Key Twist:**
- Sort array first
- Skip duplicate elements at same recursion level
- `if i > start and nums[i] == nums[i-1]: continue`

**Learning Focus:**
- Handling duplicates in backtracking
- Sorting as preprocessing step

---

### Problem 2.2: Permutations II (With Duplicates)
**Difficulty:** Medium  
**Pattern:** Permutations with duplicate element handling  
**LeetCode:** #47

**Variation:** Input array may contain duplicates; return unique permutations only.

**Key Twist:**
- Sort array + skip duplicates
- Check `used[i-1] == False` before using duplicate

**Learning Focus:**
- Duplicate handling in permutations
- Used array with additional constraint

---

### Problem 2.3: Letter Combinations of Phone Number
**Difficulty:** Medium  
**Pattern:** Multi-choice backtracking  
**LeetCode:** #17

**Variation:** Each digit maps to multiple letters (phone keypad).

**Key Twist:**
- Multiple choices per position (not binary)
- Mapping from digit to letters

**Learning Focus:**
- Variable branching factor
- Mapping-based choice generation

---

### Problem 2.4: Generate Parentheses
**Difficulty:** Medium  
**Pattern:** Constraint-based generation  
**LeetCode:** #22

**Variation:** Generate all valid combinations of `n` pairs of parentheses.

**Key Twist:**
- Track open and close counts
- Constraint: close ≤ open at all times

**Learning Focus:**
- Abstract constraint checking
- Pruning with counts

---

### Problem 2.5: Restore IP Addresses
**Difficulty:** Medium  
**Pattern:** String partitioning with constraints  
**LeetCode:** #93

**Variation:** Partition string into 4 segments forming valid IP address.

**Key Twist:**
- Fixed number of partitions (4 segments)
- Each segment: 0-255, no leading zeros

**Learning Focus:**
- Partitioning with fixed count
- Multiple constraints per partition

---

### Problem 2.6: Word Search II
**Difficulty:** Hard  
**Pattern:** Grid DFS + Trie optimization  
**LeetCode:** #212

**Variation:** Find all words from a dictionary in the grid.

**Key Twist:**
- Multiple words simultaneously (use Trie)
- Prefix-based pruning

**Learning Focus:**
- Optimizing multiple searches
- Trie integration with backtracking

---

### Problem 2.7: Combination Sum II
**Difficulty:** Medium  
**Pattern:** Backtracking with duplicates + no reuse  
**LeetCode:** #40

**Variation:** Each candidate used at most once; input may have duplicates.

**Key Twist:**
- Can't reuse element (unlike #39)
- Must handle duplicates

**Learning Focus:**
- Combining multiple constraints
- Start index + duplicate skipping

---

### Problem 2.8: Partition to K Equal Sum Subsets
**Difficulty:** Medium  
**Pattern:** Subset partitioning optimization  
**LeetCode:** #698

**Variation:** Partition array into k subsets with equal sum.

**Key Twist:**
- Track k buckets simultaneously
- Pruning with sum constraints

**Learning Focus:**
- Multiple simultaneous partitions
- Optimization with sorting (descending order)

---

## 🏆 STAGE 3: INTEGRATION (Mastery)

### Problem 3.1: N-Queens II (Count Only)
**Difficulty:** Hard  
**Pattern:** Optimized backtracking  
**LeetCode:** #52

**Integration:**
- N-Queens pattern + optimization
- Count solutions instead of recording
- Bit manipulation for constraint tracking

**Learning Focus:**
- Space optimization
- Bit manipulation techniques

---

### Problem 3.2: Sudoku Solver (Optimized)
**Difficulty:** Hard  
**Pattern:** Constraint satisfaction + heuristics  
**LeetCode:** #37 (optimized version)

**Integration:**
- Sudoku pattern + most constrained variable heuristic
- Choose cell with fewest valid digits first

**Learning Focus:**
- Variable ordering heuristics
- Constraint propagation

---

### Problem 3.3: Expression Add Operators
**Difficulty:** Hard  
**Pattern:** Backtracking with arithmetic evaluation  
**LeetCode:** #282

**Integration:**
- String partitioning + constraint checking
- Track cumulative value and last operand

**Learning Focus:**
- State tracking for evaluation
- Handling operator precedence

---

### Problem 3.4: Remove Invalid Parentheses
**Difficulty:** Hard  
**Pattern:** BFS + backtracking hybrid  
**LeetCode:** #301

**Integration:**
- Backtracking for generation
- BFS for minimum removals

**Learning Focus:**
- Combining BFS and backtracking
- Optimality with level-order traversal

---

### Problem 3.5: Shortest Path Visiting All Nodes
**Difficulty:** Hard  
**Pattern:** State space search + DP  
**LeetCode:** #847

**Integration:**
- Backtracking structure + memoization
- Bitmask for visited state

**Learning Focus:**
- Backtracking + DP combination
- State compression with bitmasks

---

### Problem 3.6: Traveling Salesman (Small n)
**Difficulty:** Hard  
**Pattern:** Branch & bound optimization  
**Custom Problem**

**Integration:**
- Backtracking + bounds-based pruning
- MST lower bound calculation

**Learning Focus:**
- Branch & bound implementation
- Bounding function design

---

### Problem 3.7: 0/1 Knapsack (Branch & Bound)
**Difficulty:** Hard  
**Pattern:** Optimization with bounds  
**Custom Problem**

**Integration:**
- Backtracking + fractional upper bound
- Best-first search with priority queue

**Learning Focus:**
- Fractional relaxation
- Best-first node selection

---

### Problem 3.8: Dynamic Array Implementation
**Difficulty:** Medium  
**Pattern:** Amortized analysis application  
**Custom Problem**

**Integration:**
- Implement dynamic array with doubling strategy
- Prove O(1) amortized append using all three methods

**Learning Focus:**
- Data structure design
- Amortized analysis practice

---

## 📊 BRANCH & BOUND PROBLEMS

### Problem B1: Traveling Salesman (4 Cities)
**Difficulty:** Medium  
**Pattern:** Branch & bound with MST bound  
**Custom/Interview**

**Approach:**
- State: partial tour + cities visited
- Bound: MST of remaining cities + min edge to tour
- Prune: if partial_cost + bound > best, skip

**Learning Focus:**
- MST lower bound calculation
- Best-first search implementation

---

### Problem B2: 0/1 Knapsack (Branch & Bound)
**Difficulty:** Medium  
**Pattern:** Branch & bound with fractional bound  
**Custom/Interview**

**Approach:**
- State: items included + remaining capacity
- Bound: fractional knapsack on remaining items
- Prune: if partial_value + bound ≤ best, skip

**Learning Focus:**
- Fractional relaxation
- Upper bound calculation

---

### Problem B3: Job Assignment Problem
**Difficulty:** Hard  
**Pattern:** Branch & bound for optimization  
**Custom/Interview**

**Approach:**
- Assign n jobs to n workers (minimize total cost)
- Bound: Hungarian algorithm relaxation
- Prune: partial assignment + bound > best

**Learning Focus:**
- Assignment problem structure
- Linear programming bounds

---

## 📈 AMORTIZED ANALYSIS PROBLEMS

### Problem A1: Dynamic Array Analysis
**Difficulty:** Medium  
**Pattern:** Aggregate analysis  
**Conceptual**

**Task:**
Prove dynamic array append is O(1) amortized using:
1. Aggregate analysis
2. Accounting method
3. Potential method

**Learning Focus:**
- Three analysis methods comparison
- Geometric series summation

---

### Problem A2: Stack with Multipop
**Difficulty:** Medium  
**Pattern:** Accounting method  
**Conceptual**

**Task:**
Analyze stack supporting push, pop, multipop(k) using accounting method.

**Learning Focus:**
- Credit assignment
- Proving non-negative credit

---

### Problem A3: Binary Counter Increment
**Difficulty:** Medium  
**Pattern:** Potential method  
**Conceptual**

**Task:**
Analyze n-bit binary counter increment using potential method.

**Learning Focus:**
- Potential function design
- Amortized cost formula (actual + ΔΦ)

---

## 🎯 PROBLEM-SOLVING STRATEGY

### Strategy 1: Pattern Recognition

**Step 1:** Read problem carefully
- Identify input/output format
- Note constraints (time, space, value ranges)

**Step 2:** Recognize pattern signals
- "Generate all..." → Backtracking
- "Find optimal..." → Branch & bound (if bounds computable)
- "Placement with constraints" → Backtracking with pruning
- "Path in grid" → DFS with backtracking

**Step 3:** Choose template
- Use universal backtracking template
- Customize state, choices, constraints components

---

### Strategy 2: Implementation Checklist

**Before Coding:**
- [ ] Define state representation clearly
- [ ] List all choices at each step
- [ ] Identify constraints for pruning
- [ ] Plan state modification/restoration

**During Coding:**
- [ ] Implement base case (complete solution)
- [ ] Implement constraint checking
- [ ] Make choice, recurse, undo choice
- [ ] Copy solution before recording

**After Coding:**
- [ ] Test with small example
- [ ] Verify state restoration happening
- [ ] Check solution copying (not reference)
- [ ] Analyze time/space complexity

---

### Strategy 3: Debugging Common Errors

**Error 1: All solutions identical**
- **Cause:** Recording reference instead of copy
- **Fix:** `result.add(copy(state))`

**Error 2: Missing solutions**
- **Cause:** Forgetting to restore state
- **Fix:** Add `undo_choice()` after recursive call

**Error 3: Infinite recursion**
- **Cause:** Not marking visited (grid problems)
- **Fix:** Mark before recursing, unmark after

**Error 4: Duplicate solutions**
- **Cause:** Not handling duplicates in input
- **Fix:** Sort + skip duplicates at same level

**Error 5: Wrong count/subset**
- **Cause:** Incorrect constraint checking
- **Fix:** Review is_valid() function logic

---

## 📅 WEEKLY PRACTICE SCHEDULE

### Day 1-2: Canonical Problems (Stage 1)
- Complete Problems 1.1 - 1.4 (subsets, permutations, combinations, N-Queens)
- Focus on template mastery
- **Goal:** 4 problems, 2-3 hours

### Day 3-4: More Canonical + Variations (Stage 1-2)
- Complete Problems 1.5 - 1.8 (Sudoku, word search, etc.)
- Start Problem 2.1 - 2.3 (variations)
- **Goal:** 5-6 problems, 3-4 hours

### Day 5: Variations (Stage 2)
- Complete Problems 2.4 - 2.8
- **Goal:** 4-5 problems, 2-3 hours

### Day 6-7: Integration + Review (Stage 3)
- Attempt Problems 3.1 - 3.5
- Review weak problems from earlier stages
- **Goal:** 3-4 integration problems, 2-3 hours

### Ongoing: Branch & Bound + Amortized
- Conceptual problems (A1-A3) during study sessions
- Implementation problems (B1-B3) as practice

---

## ✅ MASTERY CHECKLIST

**Stage 1 Completion:**
- [ ] Implemented backtracking template from scratch
- [ ] Solved subsets, permutations, combinations
- [ ] Solved N-Queens (n=4 minimum)
- [ ] Solved word search or maze problem
- [ ] Can explain state restoration importance

**Stage 2 Completion:**
- [ ] Handled duplicates in backtracking
- [ ] Optimized with constraint ordering
- [ ] Solved 5+ variation problems
- [ ] Recognized disguised patterns

**Stage 3 Completion:**
- [ ] Combined backtracking with other paradigms
- [ ] Implemented branch & bound (TSP or Knapsack)
- [ ] Analyzed amortized complexity using all three methods
- [ ] Solved 3+ integration problems

**Interview Readiness:**
- [ ] Can implement backtracking template in 5 minutes
- [ ] Can solve N-Queens on whiteboard
- [ ] Can explain permutations vs combinations clearly
- [ ] Can calculate bounds for TSP/Knapsack
- [ ] Can prove dynamic array O(1) amortized

---

**Format:** 3-Stage Progressive Problem Ladder  
**Next:** Week_13_Daily_Progress_Checklist.md
