# 🟧 WEEK 12: GREEDY ALGORITHMS & PROOFS
## Complete Course Playbook | No-Code Concept Mastery
**Duration:** 25 hours | **Focus:** Concept Understanding & Visual Reasoning

---

## 📚 WEEK OVERVIEW

### What You'll Master
```
✓ Greedy Algorithm Design & Structure
✓ Correctness Proofs (Exchange Argument)
✓ Optimal Substructure Recognition
✓ Activity Selection & Interval Problems
✓ Huffman Coding & Tree Optimization
✓ Fractional Knapsack & Scheduling
✓ When Greedy Works vs When It Fails
✓ Problem-Solving Methodology
```

### Weekly Learning Path
```
DAY 1 (90 min) → Greedy Fundamentals
DAY 2 (90 min) → Activity Selection
DAY 3 (90 min) → Huffman Coding
DAY 4 (90 min) → Knapsack & Scheduling
DAY 5 (90 min) → Integration & Failures
```

---

# 📅 DAY 1: GREEDY FUNDAMENTALS & PROOFS

## 🎯 Day Objective
Understand the greedy algorithm paradigm, when it guarantees optimality, and how to prove correctness.

### ⏱️ Time Allocation
- Concept Mastery: 50 minutes
- Visual Learning: 20 minutes
- Problem Understanding: 20 minutes

---

## 🔍 PART 1: WHAT IS A GREEDY ALGORITHM?

### The Core Idea

**Definition:** A greedy algorithm makes locally optimal choices at each step, hoping to find a globally optimal solution.

```
GREEDY ALGORITHM STRUCTURE:
═════════════════════════════════════════════════════

1. CHOICE PHASE
   └─ At each step, make the choice that looks best RIGHT NOW
   └─ Don't consider future consequences
   └─ Make irreversible decision

2. REDUCTION PHASE
   └─ Problem reduced to smaller instance
   └─ Continue until problem is solved

3. FEASIBILITY CHECK
   └─ Ensure choice doesn't violate constraints
   └─ Backtrack if necessary (but rarely)

REPEAT until solution complete
```

### Why "Greedy"?

The algorithm is **greedy** because it **greedily chooses** the best option available at each moment without looking ahead. It's like:

- 🏪 Grocery shopping: Pick lowest prices without considering budget
- 🛣️ Navigation: Always take the nearest next step toward destination
- 💰 Coin changing: Always pick the largest coin that fits

### Greedy vs Dynamic Programming

```
COMPARISON MATRIX:
═════════════════════════════════════════════════════

DIMENSION          │ GREEDY              │ DYNAMIC PROGRAMMING
───────────────────┼─────────────────────┼────────────────────
Decision-making    │ Local (current)     │ Global (future)
Subproblems        │ Don't solve before  │ Solve all first
Memoization        │ Not needed          │ Essential
Proof of optimality│ Must prove          │ Recurrence relation
Time complexity    │ Usually O(n log n)  │ Usually O(n²) or more
Space complexity   │ O(1) typically      │ O(n) for memo
When to use        │ Limited problems    │ Optimal substructure

EXAMPLE: Coin Changing
Greedy: Always pick largest coin → Wrong! (e.g., 1¢, 10¢, 25¢)
DP: Try all coins systematically → Correct!

EXAMPLE: Activity Selection
Greedy: Sort by finish time → Correct! ✓
DP: Also works but overkill (slower)
```

### ASCll Flowchart: Greedy Algorithm Decision

```
                    START
                      │
                      ▼
            Is problem feasible?
                    /  \
                  YES  NO
                  /      \
                 ▼        ▼
         Make greedy   FAIL
         choice       (no solution)
                  \      /
                   ▼    ▼
              Is solution complete?
                 /        \
               YES        NO
               /            \
              ▼              ▼
           RETURN          Continue
           SOLUTION        (repeat)
```

---

## 🔍 PART 2: WHEN DOES GREEDY WORK? (THE THEORY)

### The Two Necessary Conditions

For a greedy algorithm to guarantee the optimal solution, the problem MUST have two properties:

#### ✅ Condition 1: Optimal Substructure

**What it means:** If you have an optimal solution to the whole problem, removing the first greedy choice leaves you with an optimal solution to the remaining subproblem.

**Visual Explanation:**
```
OPTIMAL SOLUTION for entire problem
═════════════════════════════════════════════════════

┌──────────────┬────────────────────────────┐
│  GREEDY      │  REMAINING PROBLEM         │
│  CHOICE      │  (must also be optimal)    │
├──────────────┼────────────────────────────┤
│   Choice A   │   Remaining choices B,C,D  │
└──────────────┴────────────────────────────┘
   (step 1)          (steps 2, 3, 4...)

KEY: The remaining problem B,C,D MUST also be optimal
     Otherwise the whole solution wouldn't be optimal
```

**Real Example: Activity Selection**

```
Activities: (start, finish)
(1,4), (3,5), (0,6), (5,7), (3,8), (5,9), (6,10), (8,11), (8,12), (2,14), (12,16)

Sort by finish time: (1,4), (3,5), (0,6), (5,7), (6,10), (8,11), (12,16)

OPTIMAL SOLUTION (selecting by finish time):
═════════════════════════════════════════════════════
Activity 1: (1,4)      ← GREEDY CHOICE
           └─ Leaves earliest finish time
                         ▼
Activity 2: (5,7)      ← REMAINS OPTIMAL for rest
Activity 3: (8,11)
Activity 4: (12,16)

Remove first choice (1,4):
Problem: Select max from (3,5), (0,6), (5,7), (6,10), (8,11), (12,16)
Best solution: (5,7), (8,11), (12,16)
✓ This is indeed optimal for the subproblem
```

#### ✅ Condition 2: Greedy Choice Property

**What it means:** There exists a greedy choice that is part of SOME optimal solution (not necessarily the unique optimal).

**Visual Explanation:**
```
MULTIPLE POSSIBLE OPTIMAL SOLUTIONS
═════════════════════════════════════════════════════

                 OPTIMAL 1
                 /        \
         Greedy Choice  Choice B
         (Finish 1st)   (Different)
            │              │
            ▼              ▼
    Sub-optimal 1A    Sub-optimal 1B
    (still optimal)   (still optimal)

GUARANTEE: At least ONE optimal solution contains
           the greedy choice

PROOF STRATEGY: If optimal doesn't have greedy choice,
                SWAP it in and show solution stays optimal
```

**Real Example: Activity Selection Again**

```
Claim: "Always pick activity that finishes earliest"
       is a greedy choice that's in SOME optimal

Proof:
1. Consider ANY optimal solution OPT
2. Let A be the activity that finishes earliest overall
3. If A ∈ OPT: Done! ✓
4. If A ∉ OPT:
   - Let B be first activity in OPT
   - Since A finishes earlier: finish(A) ≤ finish(B)
   - Replace B with A: new solution still valid
   - New solution has ≤ same size
   - So A was in some optimal ✓
```

---

## 🔍 PART 3: EXCHANGE ARGUMENT (PROOF TECHNIQUE)

### What is an Exchange Argument?

**Definition:** A proof technique that shows a greedy choice is optimal by starting with ANY optimal solution and systematically replacing parts with the greedy choice, showing we never get worse.

### Exchange Argument Template

```
EXCHANGE ARGUMENT PROOF
═════════════════════════════════════════════════════

THEOREM: Greedy algorithm produces optimal solution

PROOF:
1. ASSUME arbitrary optimal solution OPT exists
   OPT = (c₁*, c₂*, c₃*, ..., cₙ*)

2. IDENTIFY greedy choice
   G = (greedy chooses g₁ for first step)

3. COMPARE & EXCHANGE
   - If c₁* = g₁: Already matches! Move to step 2.
   - If c₁* ≠ g₁: EXCHANGE c₁* with g₁
     OPT' = (g₁, c₂*, c₃*, ..., cₙ*)

4. VERIFY OPTIMALITY
   Prove: cost(OPT') ≤ cost(OPT)
   (Usually: cost(OPT') = cost(OPT))

5. REPEAT
   Apply same argument to OPT' with g₂, g₃, etc.
   Each exchange preserves optimality

6. CONCLUDE
   After n exchanges, we have greedy solution
   which has same cost as original OPT
   Therefore, greedy is optimal ✓
```

### Why This Works: Intuitive Explanation

```
VISUAL EXCHANGE PROCESS:
═════════════════════════════════════════════════════

Optimal Solution OPT:
┌────────┬────────┬────────┬────────┐
│ BAD    │ CHOICE │ CHOICE │ CHOICE │
│ CHOICE │   2    │   3    │   4    │
└────────┴────────┴────────┴────────┘
  (not greedy)

Replace with GREEDY choice:
┌────────┬────────┬────────┬────────┐
│GREEDY  │ CHOICE │ CHOICE │ CHOICE │
│CHOICE  │   2    │   3    │   4    │
└────────┴────────┴────────┴────────┘

If cost same or better: Continue ✓
If cost worse: Contradiction! (OPT wasn't optimal)

Repeat for positions 2, 3, 4...
Final result: Greedy solution with same cost as OPT
```

### Example: Activity Selection Exchange Argument

```
PROBLEM: Select maximum non-overlapping activities
         Activities have (start, finish) times
         Greedy: Sort by finish time, pick greedily

THEOREM: Greedy produces optimal selection

PROOF:
═════════════════════════════════════════════════════

1. Let OPT = optimal solution of size k
   OPT = (a₁, a₂, ..., aₖ) sorted by finish

2. Let G = greedy solution
   G starts by picking activity that finishes first

3. Exchange:
   If OPT's first activity ≠ greedy first activity:
   - Greedy picks activity that finishes earliest (g₁)
   - OPT's first activity (a₁) finishes later
   - Replace a₁ with g₁ in OPT
   - New solution: (g₁, a₂, a₃, ..., aₖ)
   - Since g₁ finishes earlier: can fit same remaining activities
   - New solution valid and same size ✓

4. Repeat:
   After choosing g₁, remaining problem is:
   "Select max activities from those starting after finish(g₁)"
   - This is same problem structure (optimal substructure ✓)
   - By induction, greedy also optimal for subproblem
   - Therefore greedy optimal for entire problem ✓

CONCLUSION: Greedy produces optimal solution ✓
```

---

## 🔍 PART 4: GREEDY ALGORITHM TEMPLATE

### The Generic Structure

```
GREEDY ALGORITHM TEMPLATE
═════════════════════════════════════════════════════

STEP 1: PREPROCESSING
   └─ Sort input (usually by some criteria)
   └─ Initialize data structures
   └─ Set baseline/empty solution

STEP 2: GREEDY SELECTION LOOP
   for each element in sorted input:
       if element can be added to solution:
           ADD element to solution
           UPDATE relevant tracking data
       endif
   endfor

STEP 3: RETURN SOLUTION
   └─ Return accumulated solution
```

### Key Design Choices

```
WHEN IMPLEMENTING GREEDY, DECIDE:
═════════════════════════════════════════════════════

1. SORTING CRITERION
   - By value? By weight? By ratio?
   - By deadline? By finish time?
   - Ascending or descending?
   
   This is THE MOST IMPORTANT choice!
   Wrong choice = wrong algorithm

2. SELECTION RULE
   - Always pick top? (best value)
   - Pick if feasible? (fits constraints)
   - Pick if improves solution? (meets criteria)

3. DATA STRUCTURE
   - Simple array? (after sorted)
   - Priority queue? (for dynamic sorting)
   - Set/map? (for quick lookup)

4. FEASIBILITY CHECK
   - What makes a choice valid?
   - Must always check constraints
   - If invalid: skip or backtrack?
```

### Visualization: Template in Action

```
INPUT: Collection of items with properties

PREPROCESSING:
   Sort by criterion
   │
   ▼
   sorted_items = [item1, item2, item3, ...]

GREEDY LOOP:
   solution = []
   
   for each item in sorted_items:
       ├─ Is item feasible?
       │  └─ Check constraints
       │
       ├─ Can add to solution?
       │  └─ Check space/capacity/validity
       │
       └─ If feasible:
          └─ Add to solution
             solution.append(item)

RETURN: solution with greedy selections
```

---

## 🔍 PART 5: CORRECTNESS PROOF STRATEGY

### Five-Step Proof Structure

```
PROVING GREEDY IS CORRECT
═════════════════════════════════════════════════════

STEP 1: Show Problem has OPTIMAL SUBSTRUCTURE
        ├─ Assume optimal solution exists
        ├─ Remove first greedy choice
        └─ Show remainder is optimal for subproblem

STEP 2: Show Problem has GREEDY CHOICE PROPERTY
        ├─ Show greedy choice in SOME optimal solution
        ├─ Use exchange argument
        └─ Prove: can always exchange non-greedy for greedy

STEP 3: Prove GREEDY LEAVES OPTIMAL SUBPROBLEM
        ├─ After greedy choice, subproblem is same type
        ├─ Subproblem is independent (no cross-constraints)
        └─ Can apply greedy to subproblem

STEP 4: Use INDUCTION or EXCHANGE ARGUMENT
        ├─ Base case: 1 element → greedy obviously optimal
        ├─ Inductive step: if greedy works for k, works for k+1
        └─ Conclusion: works for all n elements

STEP 5: Compute COMPLEXITY & OPTIMALITY BOUND
        ├─ Time complexity of greedy
        ├─ Compare to known lower bounds
        └─ Confirm greedy achieves optimality
```

### Example: Correctness Proof for Activity Selection

```
THEOREM: Greedy Activity Selection is Optimal

PROOF:
═════════════════════════════════════════════════════

Notation:
  - Activities: (start_i, finish_i) for i = 1..n
  - Sorted by finish time: f₁ ≤ f₂ ≤ ... ≤ fₙ
  - Greedy: repeatedly pick activity with earliest finish time

PART 1: OPTIMAL SUBSTRUCTURE ✓
────────────────────────────────
If OPT = optimal selection including activity 1:
  Then OPT - {activity 1} = optimal selection from
       {activities starting after finish(activity 1)}
  
Why: If remainder wasn't optimal, could replace with better
     selection, making OPT not optimal (contradiction)

PART 2: GREEDY CHOICE PROPERTY ✓
────────────────────────────────
Greedy picks activity 1 (earliest finish)

Claim: Activity 1 is in SOME optimal solution

Proof by Exchange:
  1. Consider ANY optimal solution OPT
  2. If 1 ∈ OPT: Done ✓
  3. If 1 ∉ OPT:
     - Let k = first activity in OPT
     - f₁ ≤ fₖ (activity 1 finishes no later than k)
     - Replace k with 1: OPT' = (1, rest of OPT)
     - OPT' still valid (1 finishes no later than k)
     - |OPT'| = |OPT|
     - So 1 is in optimal solution ✓

PART 3: GREEDY LEAVES OPTIMAL SUBPROBLEM ✓
──────────────────────────────────────────
After choosing activity i:
  Subproblem: activities starting after finish(i)
  Same structure: can recursively apply greedy
  No constraints link chosen to unchosen

PART 4: INDUCTION ✓
───────────────────
Base case: n=1 → greedy picks it → optimal (1 activity)

Inductive step: Assume greedy optimal for n-1 activities
  For n activities:
  - Greedy picks activity 1 (by property from Part 2)
  - Remaining = n-1 activities (from Part 3)
  - By inductive hypothesis: greedy optimal for remaining
  - Therefore greedy optimal for n activities ✓

By induction, greedy is optimal for all n ✓

CONCLUSION: Greedy Activity Selection produces optimal solution
```

---

## 🎯 DAY 1 SUMMARY

### What You Learned

```
✓ Greedy algorithms make local optimal choices
✓ Work only when problem has specific structure
✓ Must have: optimal substructure AND greedy choice property
✓ Exchange argument proves optimality
✓ Generic template: sort → loop → select
✓ Proof strategy: establish structure, then prove choice works
```

### Key Concepts Map

```
                  GREEDY ALGORITHM
                        │
                ┌───────┼───────┐
                ▼       ▼       ▼
            WHEN WORKS  PROOF   STRUCTURE
                │        │        │
         ┌──────┴──┐     │        │
         ▼         ▼     ▼        ▼
      OPTIMAL   GREEDY  EXCHANGE SORT
      SUBST     CHOICE  ARGUMENT │
                         │      SELECT
              ┌──────────┴──────┐ │
              ▼                 ▼ ▼
         INDUCTION        REPEAT
```

### Critical Insights

1. **Greedy is NOT universal** - Only works for special problems
2. **PROOF REQUIRED** - Always prove greedy is optimal for your problem
3. **STRUCTURE MATTERS** - Optimal substructure is THE key property
4. **EXCHANGE ARGUMENT** - Most common proof technique
5. **SORTING IS CRITICAL** - Correct sort criterion determines everything

---

# 📅 DAY 2: ACTIVITY SELECTION & INTERVAL PROBLEMS

## 🎯 Day Objective
Master interval scheduling problems, understand "greedy stays ahead" technique, and learn variations.

### ⏱️ Time Allocation
- Core Problem: 35 minutes
- Greedy Stays Ahead: 20 minutes
- Variations: 25 minutes
- Problem Analysis: 10 minutes

---

## 🔍 PART 1: ACTIVITY SELECTION DEEP DIVE

### Problem Definition

```
ACTIVITY SELECTION PROBLEM (Classic)
═════════════════════════════════════════════════════

INPUT:
  - n activities
  - Each activity i has: start_time(i), finish_time(i)
  - Activities can overlap in time

OUTPUT:
  - Select maximum number of activities
  - Such that NO TWO selected activities overlap
  - Activities don't overlap if: finish(i) ≤ start(j)

CONSTRAINT:
  - Maximize the COUNT, not value or weight
  - Simpler version of weighted interval scheduling

EXAMPLE:
  Activities:  (1,4),  (3,5),  (0,6),  (5,7),  (3,8),  (5,9),  (6,10), (8,11), (8,12), (2,14), (12,16)
  
  One optimal: (1,4), (5,7), (8,11), (12,16)      → 4 activities
  Another:     (3,5), (6,10), (12,16)            → 3 activities
  Bad:         (0,6)                              → 1 activity
```

### Why Not Dynamic Programming?

```
ALTERNATIVE APPROACH: Dynamic Programming
═════════════════════════════════════════════════════

Could solve with DP:
  dp[i] = max activities from first i activities
  dp[i] = max(dp[i-1], 1 + dp[j]) where j = latest activity ending before i

Time: O(n² ) or O(n log n) with preprocessing
Space: O(n)

But GREEDY is simpler!
  Greedy approach:
  - Sort by finish time
  - Greedily pick activities with earliest finish
  - Time: O(n log n) for sort only
  - Space: O(1)

Why is greedy better?
✓ Faster (linear after sort vs quadratic)
✓ Simpler logic
✓ Less memory
✓ Same optimal result
```

### The Greedy Choice: Finish Time

```
WHY SORT BY FINISH TIME?
═════════════════════════════════════════════════════

The key insight: Once you pick an activity, you want
                to leave as much time as possible
                for future activities

REASONING:
  - If you pick activity finishing early: more time left
  - If you pick activity finishing late: less time left
  - Activity with earliest finish allows most future picks

VISUALIZATION:
  Time:  0──┬──┬──┬──┬──┬──┬──┬──┬──┬──┬──┬──┬──┬──┬──16
            1  2  3  4  5  6  7  8  9 10 11 12 13 14 15

  Activity A: [1──────4]          ← Finishes early
  Activity B: [3────────────5]    ← Finishes later

  If pick A: Time [4──────────16] = 12 units left
  If pick B: Time [5──────────16] = 11 units left

  Greedy picks A to maximize future opportunities
```

### The Algorithm in Action

```
ALGORITHM: GREEDY ACTIVITY SELECTION
═════════════════════════════════════════════════════

INPUT: Activities with (start, finish) times

STEP 1: SORT by finish time (ascending)
        └─ If tied, sort by start time

STEP 2: INITIALIZE
        selected = []
        last_finish = 0   (nothing selected yet)

STEP 3: LOOP through sorted activities
        for each activity i (in finish order):
            if start(i) ≥ last_finish:
                selected.append(i)
                last_finish = finish(i)
            endif
        endfor

STEP 4: RETURN selected activities

COMPLEXITY:
  Time: O(n log n) due to sorting
  Space: O(1) extra space (excluding input/output)
```

### Step-by-Step Example

```
EXAMPLE EXECUTION
═════════════════════════════════════════════════════

Input Activities: (1,4), (3,5), (0,6), (5,7), (3,8), (5,9), (6,10), (8,11), (8,12), (2,14), (12,16)

STEP 1: SORT BY FINISH TIME
        (1,4), (3,5), (0,6), (5,7), (6,10), (8,11), (3,8), (5,9), (8,12), (2,14), (12,16)

STEP 2: INITIALIZE
        selected = []
        last_finish = 0

STEP 3: PROCESS EACH ACTIVITY

  Activity (1,4):
    start=1 ≥ last_finish=0? YES ✓
    selected = [(1,4)]
    last_finish = 4

  Activity (3,5):
    start=3 ≥ last_finish=4? NO ✗
    Skip (overlaps)

  Activity (0,6):
    start=0 ≥ last_finish=4? NO ✗
    Skip (overlaps)

  Activity (5,7):
    start=5 ≥ last_finish=4? YES ✓
    selected = [(1,4), (5,7)]
    last_finish = 7

  Activity (6,10):
    start=6 ≥ last_finish=7? NO ✗
    Skip (overlaps)

  Activity (8,11):
    start=8 ≥ last_finish=7? YES ✓
    selected = [(1,4), (5,7), (8,11)]
    last_finish = 11

  Activity (3,8):
    start=3 ≥ last_finish=11? NO ✗
    Skip

  Activity (5,9):
    start=5 ≥ last_finish=11? NO ✗
    Skip

  Activity (8,12):
    start=8 ≥ last_finish=11? NO ✗
    Skip

  Activity (2,14):
    start=2 ≥ last_finish=11? NO ✗
    Skip

  Activity (12,16):
    start=12 ≥ last_finish=11? YES ✓
    selected = [(1,4), (5,7), (8,11), (12,16)]
    last_finish = 16

RESULT: 4 activities selected ✓
```

---

## 🔍 PART 2: GREEDY STAYS AHEAD TECHNIQUE

### What Does "Greedy Stays Ahead" Mean?

```
DEFINITION: "GREEDY STAYS AHEAD"
═════════════════════════════════════════════════════

The greedy algorithm (or greedy solution) "stays ahead" of
any other solution by maintaining the earliest possible finish
time at each step.

This guarantees greedy can accommodate anything optimal can.
```

### Formal Statement

```
THEOREM: Greedy Stays Ahead for Activity Selection
═════════════════════════════════════════════════════

Let G = (g₁, g₂, ..., gₖ) = greedy selection (sorted by finish)
Let O = (o₁, o₂, ..., oₘ) = any other valid selection (sorted by finish)

Claim: For each i, finish(gᵢ) ≤ finish(oᵢ)

In other words: At each step, greedy finishes ≤ optimal
                Greedy "stays ahead" by having more time left
```

### Proof by Induction

```
PROOF: GREEDY STAYS AHEAD
═════════════════════════════════════════════════════

Base Case (i=1):
  g₁ = activity with earliest finish time overall
  o₁ = any valid selection's first activity
  
  By definition: finish(g₁) ≤ finish(o₁) ✓

Inductive Step (assume true for i, prove for i+1):
  Assume: finish(gᵢ) ≤ finish(oᵢ)
  
  Must show: finish(gᵢ₊₁) ≤ finish(oᵢ₊₁)
  
  Consider activities after time finish(gᵢ):
    - Since finish(gᵢ) ≤ finish(oᵢ):
      All activities available for greedy include all available for O
    
    - gᵢ₊₁ = activity with earliest finish among available
    - oᵢ₊₁ = some activity available for O
    
    - By definition of greedy: finish(gᵢ₊₁) ≤ finish(oᵢ₊₁) ✓

By induction: finish(gᵢ) ≤ finish(oᵢ) for all i ✓
```

### Why This Implies Optimality

```
IMPLICATION: GREEDY IS OPTIMAL
═════════════════════════════════════════════════════

Since greedy stays ahead:

1. Greedy always finishes ≤ every other solution
2. This means greedy leaves more time for future choices
3. Greedy can fit everything optimal can fit, PLUS potentially more
4. No solution can beat greedy

Visual Proof:
═════════════════════════════════════════════════════

Timeline 0────┬────┬────┬────┬────┬────┬────┬────100

Greedy:   [g₁]  [g₂]  [g₃]  [g₄]
          0─4   5─7   8─11  12─16

Optimal:  [o₁]  [o₂]  [o₃]
          0─6   7─9   10─15

Greedy finishes g₁ at 4 ≤ optimal finishes o₁ at 6
                ↓
Greedy has time 4-100 available vs optimal has time 6-100
                ↓
Greedy can fit o₁'s remaining activities PLUS more

Same reasoning for g₂ vs o₂, g₃ vs o₃, etc.
```

---

## 🔍 PART 3: INTERVAL SCHEDULING VARIATIONS

### Variation 1: Weighted Activity Selection

```
VARIATION 1: MAXIMIZE TOTAL WEIGHT (Not Count)
═════════════════════════════════════════════════════

Difference from original:
  Original: Maximize NUMBER of activities
  Weighted: Each activity has VALUE/WEIGHT
            Maximize TOTAL VALUE of selected

KEY INSIGHT: Greedy DOESN'T work here!
═════════════════════════════════════════════════════

Counterexample:
  Activity A: time [0,10], weight 10
  Activity B: time [0,5], weight 6
  Activity C: time [5,10], weight 6
  
  Greedy (by finish time): Pick B, then C → weight 12 ✓ Optimal
  But with different weights:
  
  Activity A: time [0,10], weight 100
  Activity B: time [0,5], weight 10
  Activity C: time [5,10], weight 10
  
  Greedy (by finish time): Pick B, then C → weight 20 ✗ WRONG
  Optimal: Pick A → weight 100 ✓ CORRECT
  
WHY GREEDY FAILS:
  - No greedy choice property
  - Can't decide between high-weight, long activity vs
    multiple low-weight, short activities
  
SOLUTION: Use Dynamic Programming
  dp[i] = max weight using first i activities
  dp[i] = max(dp[i-1], weight[i] + dp[j])
          where j = latest activity ending before i
  
  Time: O(n²) or O(n log n)
  Space: O(n)
```

### Variation 2: Interval Partitioning (Minimum Rooms)

```
VARIATION 2: MINIMIZE ROOMS NEEDED
═════════════════════════════════════════════════════

Problem:
  - Each activity needs a room during its time
  - Activities overlap = need different rooms
  - Minimize total rooms needed

Example:
  Activities: (1,3), (2,4), (3,5), (1,2), (4,6)
  
  Room 1: (1,3), (4,6)
  Room 2: (2,4), (5,?)  ← Need 2 rooms, then 1, then...
  Room 3: (3,5)
  
  Minimum rooms needed: 3

KEY INSIGHT: Use SWEEP LINE / INTERVAL GRAPH algorithm
═════════════════════════════════════════════════════

Algorithm:
1. Create events for each activity:
   - START event at start time
   - END event at finish time

2. Sort events by time
   (If tied: process END before START
    so room can be reused)

3. Track rooms in use:
   - At each START: increment room count
   - At each END: decrement room count

4. Max rooms ever needed = answer

Visualization:

Time:  0──┬──┬──┬──┬──┬──┬──6
       1  2  3  4  5

Activity (1,3):  START at 1, END at 3
Activity (2,4):  START at 2, END at 4
Activity (3,5):  START at 3, END at 5
Activity (1,2):  START at 1, END at 2
Activity (4,6):  START at 4, END at 6

Timeline of events:
  Time 1: START(1,3), START(1,2)     → rooms = 2
  Time 2: END(1,2), START(2,4)       → rooms = 2
  Time 3: END(1,3), START(3,5)       → rooms = 2
  Time 4: END(2,4), START(4,6)       → rooms = 2
  Time 5: END(3,5)                   → rooms = 1
  Time 6: END(4,6)                   → rooms = 0

Max rooms = 3

Time: O(n log n)
Space: O(n)
```

### Variation 3: Interval Scheduling with Weights

```
VARIATION 3: MAXIMIZE PROFIT (Different Deadline Variant)
═════════════════════════════════════════════════════

Problem: Same as Activity Selection but with WEIGHTS
         Select non-overlapping activities to maximize total weight

Different from above: Activities don't need rooms
                      Just want max value, non-overlapping

Example:
  Activity 1: time [0,3], value 10
  Activity 2: time [1,2], value 5
  Activity 3: time [2,5], value 7
  
  Option A: Activity 1 → value 10 ✓
  Option B: Activities 2,3 → value 12 ✓ Better!
  
Solution: DYNAMIC PROGRAMMING (not greedy)
  dp[i] = max value using activities 1..i
  dp[i] = max(dp[i-1],                    ← Don't take i
              value[i] + dp[j])           ← Take i + best before
                        where j = latest ending before i

WHY NOT GREEDY:
  - Can't decide: high value but long?
                 or multiple low values but short?
  - No greedy choice property
  - Must consider all combinations
```

### Variation 4: Job Sequencing with Deadlines

```
VARIATION 4: JOBS WITH DEADLINES AND PROFITS
═════════════════════════════════════════════════════

Problem:
  - n jobs, each with:
    * deadline (by when it must complete)
    * profit (revenue if completed)
  - Each job takes 1 unit time
  - One job at a time
  - Job must complete by deadline to earn profit
  - Maximize total profit

Example:
  Job 1: deadline=2, profit=100
  Job 2: deadline=1, profit=19
  Job 3: deadline=2, profit=27
  Job 4: deadline=1, profit=25
  Job 5: deadline=3, profit=15
  Job 6: deadline=3, profit=20

Schedule to maximize profit:

GREEDY APPROACH:
1. Sort jobs by PROFIT (descending)
2. For each job, place as late as possible before deadline

Execution:
  Jobs sorted by profit: 1(100,d=2), 3(27,d=2), 6(20,d=3), 4(25,d=1), 5(15,d=3), 2(19,d=1)
  
  Jobs: [_, _, _]  (time slots 1, 2, 3)
  
  Job 1 (deadline=2): Place at time 2
  Jobs: [_, 1, _]
  
  Job 3 (deadline=2): Place at time 1
  Jobs: [3, 1, _]
  
  Job 6 (deadline=3): Place at time 3
  Jobs: [3, 1, 6]
  
  Job 4 (deadline=1): Can't place (slot 1 full) ✗
  
  Job 5 (deadline=3): Can't place (all slots full) ✗
  
  Job 2 (deadline=1): Can't place ✗
  
  Total profit: 100 + 27 + 20 = 147

Visualization:

Time:  1    2    3
      ┌────┬────┬────┐
Slot: │ 3  │ 1  │ 6  │
      └────┴────┴────┘
       27   100  20  = 147 profit

Time: O(n² ) or O(n log n) with better data structure
Space: O(n)
```

---

## 🎯 DAY 2 SUMMARY

### What You Learned

```
✓ Activity selection problem structure
✓ Greedy by finish time is optimal
✓ "Greedy stays ahead" proof technique
✓ Multiple interval variations and when greedy works
✓ When to use greedy vs DP
✓ Sweep line algorithm for room allocation
✓ Job scheduling with deadlines
```

### Comparison Table: Interval Problems

```
PROBLEM           │ CRITERION     │ APPROACH      │ COMPLEXITY
──────────────────┼───────────────┼───────────────┼──────────
Max Activities    │ Count         │ Greedy        │ O(n log n)
Max Weight        │ Total Value   │ DP            │ O(n²)
Min Rooms         │ Rooms needed  │ Sweep line    │ O(n log n)
Job Scheduling    │ Total Profit  │ Greedy        │ O(n²)
Weighted Interval │ Max Value     │ DP            │ O(n log n)
```

---

# 📅 DAY 3: HUFFMAN CODING & OPTIMAL PREFIX TREES

## 🎯 Day Objective
Understand optimal prefix codes, Huffman algorithm, and tree construction proof.

### ⏱️ Time Allocation
- Prefix Codes Concept: 25 minutes
- Huffman Algorithm: 30 minutes
- Correctness Proof: 20 minutes
- Applications: 15 minutes

---

## 🔍 PART 1: PREFIX CODES AND ENCODING

### What is a Prefix Code?

```
DEFINITION: PREFIX CODE
═════════════════════════════════════════════════════

A prefix code is a set of binary strings (codewords) where
NO codeword is a prefix of any other codeword.

Why this matters: Enables UNIQUE DECODING without delimiters

EXAMPLE:
  ✗ BAD Codes (not prefix-free):
    a=0, b=01
    Problem: Can't decode "01" - is it "a then b" or just "b"?
    
  ✓ GOOD Codes (prefix-free):
    a=00, b=01, c=10, d=11
    No code starts with another
    "0011" unambiguously = "a then b" (00, then 01)

Non-prefix code problem:

String: 0 1 0 1
       ↓
       Is it: [0, 1, 0, 1] = a, ?, a, ?
              [01, 01] = b, b
              [0, 10, 1] = a, c, ?
       
AMBIGUOUS! Can't decode uniquely

Prefix-free code solution:

With a=00, b=01, c=10, d=11:
String: 0 0 0 1 1 0
       ↓ ↓ ↓ ↓ ↓ ↓
       [00] [01] [10] = a, b, c
       
UNAMBIGUOUS! Decode left to right
```

### The Prefix Code Tree

```
REPRESENTING CODES AS A BINARY TREE
═════════════════════════════════════════════════════

Each prefix code can be represented as a BINARY TREE:
  - Leaf nodes = characters to encode
  - Edge labels = 0 (left) or 1 (right)
  - Path from root to leaf = codeword for that character

Example:

              ROOT
              /  \
            0/    \1
            /      \
           A        B
                   / \
                 0/   \1
                /      \
               C        D

Decoding tree:
  A = 0 (one step left)
  B path doesn't end here, keep going
  C = 10 (right, then left)
  D = 11 (right, then right)
  
Codeword lengths:
  A: length 1 (1 bit)
  C: length 2 (2 bits)
  D: length 2 (2 bits)

Decoding "01011":
  0 → Check left: Found A ✓ (codeword "0")
  1 → Check right: Go to B node
  0 → Check left: Found C ✓ (codeword "10")
  1 → Check right: Go to D node
  1 → Check right: Found D ✓ (codeword "11")
  
Decoded: A, C, D
```

### Fixed-Length vs Variable-Length Codes

```
COMPARISON: FIXED vs VARIABLE LENGTH
═════════════════════════════════════════════════════

Problem: Encode text with characters {A, B, C, D}

FIXED-LENGTH CODE:
  A=00, B=01, C=10, D=11
  
Text: "AAABCD"
Encoding: 00 00 00 01 10 11 = 12 bits
Average code length: 2 bits per character

Variable-length code (tailored to frequency):
  If A appears 50%, B 25%, C 15%, D 10%:
  A=0 (1 bit), B=10 (2 bits), C=110 (3 bits), D=111 (3 bits)
  
Text: "AAABCD" (if frequencies hold: "AAAABBBCCD" in 10 chars)
Encoding: 0 0 0 0 10 10 10 110 110 111 = 10 bits (vs 20 for fixed)
Average: (0.50×1 + 0.25×2 + 0.15×3 + 0.10×3) = 1.75 bits per character

BENEFIT OF VARIABLE-LENGTH:
  - Frequent characters = short codes
  - Rare characters = longer codes
  - Savings especially with skewed frequencies
  - Average code length reduced
  
BUT: Need to know frequencies in advance!
```

### Optimal Prefix Code Problem

```
OPTIMAL PREFIX CODE PROBLEM
═════════════════════════════════════════════════════

INPUT:
  - n characters
  - frequency f[c] = how often character c appears

OUTPUT:
  - Prefix code (represented as binary tree)
  - Minimizes TOTAL BITS needed to encode all text

FORMULATION:
  Minimize: Σ(f[c] × length[code(c)])
            for all characters c
  
  Subject to: Codes form prefix-free set

INSIGHT:
  - More frequent character → shorter code
  - Less frequent character → longer code
  - This minimizes total bits

Example with frequencies:
  Character | Frequency | Ideal code length
  ──────────┼───────────┼─────────────────
  A         | 0.45      | 1 (short)
  B         | 0.13      | 3-4 (long)
  C         | 0.12      | 3-4 (long)
  D         | 0.16      | 3
  E         | 0.09      | 4 (longer)
  F         | 0.05      | 4 (longest)
```

---

## 🔍 PART 2: HUFFMAN CODING ALGORITHM

### The Algorithm

```
HUFFMAN CODING ALGORITHM
═════════════════════════════════════════════════════

INPUT: n characters with frequencies

OUTPUT: Optimal prefix code (represented as tree)

ALGORITHM:
1. Create leaf node for each character
   Label with its frequency

2. REPEAT n-1 times:
   - Find two nodes with MINIMUM frequency (not yet combined)
   - Create new internal node with frequency = sum of two minimums
   - Link two nodes as children (left/right arbitrary)
   - Remove two nodes, add new node

3. Last node remaining is ROOT of code tree

DATA STRUCTURE: Priority Queue (Min-Heap)
  - Extract min: O(log n)
  - Insert: O(log n)
  - n-1 iterations
  - Total: O(n log n)
```

### Step-by-Step Example

```
HUFFMAN ALGORITHM EXECUTION
═════════════════════════════════════════════════════

Characters with frequencies:
  A:5  B:9  C:12  D:13  E:16  F:45

Step 0 - Initialize:
Heap (priority = frequency):
  [A:5, B:9, C:12, D:13, E:16, F:45]

Step 1 - Extract 2 minimums: A:5, B:9
Create new node AB:14 = 5+9
Heap: [C:12, D:13, AB:14, E:16, F:45]

Step 2 - Extract 2 minimums: C:12, D:13
Create new node CD:25 = 12+13
Heap: [AB:14, E:16, CD:25, F:45]

Step 3 - Extract 2 minimums: AB:14, E:16
Create new node ABE:30 = 14+16
Heap: [CD:25, ABE:30, F:45]

Step 4 - Extract 2 minimums: CD:25, ABE:30
Create new node CDABE:55 = 25+30
Heap: [F:45, CDABE:55]

Step 5 - Extract 2 minimums: F:45, CDABE:55
Create new node ROOT:100 = 45+55
Heap: [ROOT:100]

Done! ROOT is final tree

RESULTING TREE:
═════════════════════════════════════════════════════

              ROOT:100
              /        \
            0/          \1
            /            \
          F:45       CDABE:55
                      /     \
                    0/       \1
                    /         \
                CD:25      ABE:30
                /   \      /     \
              0/     \1  0/       \1
              /       \  /         \
            C:12   D:13 AB:14    E:16
                        /  \
                      0/    \1
                      /      \
                    A:5     B:9

RESULTING CODES:
  F = 0 (length 1)
  C = 100 (length 3)
  D = 101 (length 3)
  A = 1100 (length 4)
  B = 1101 (length 4)
  E = 111 (length 3)

CODE LENGTH VERIFICATION:
  char | freq | code     | length | freq×length
  ─────┼──────┼──────────┼────────┼───────────
  A    | 5    | 1100     | 4      | 20
  B    | 9    | 1101     | 4      | 36
  C    | 12   | 100      | 3      | 36
  D    | 13   | 101      | 3      | 39
  E    | 16   | 111      | 3      | 48
  F    | 45   | 0        | 1      | 45
      ───────────────────────────────────
      Total: |100              | 224 bits for encoding

Average bits per character: 224/100 = 2.24 bits

Comparison:
  Fixed-length (for 6 chars): 3 bits each = 300 bits total
  Huffman: 2.24 bits average = 224 bits total
  Savings: 25% reduction!
```

### Visualization: Building Tree Step-by-Step

```
VISUAL TREE CONSTRUCTION
═════════════════════════════════════════════════════

Initial: 6 separate nodes (each character)

  A:5   B:9   C:12  D:13  E:16  F:45

Step 1: Combine A and B (smallest)

        AB:14
        /     \
      A:5    B:9
      
  Rest: C:12  D:13  E:16  F:45  AB:14

Step 2: Combine C and D (next smallest)

        CD:25
        /     \
      C:12   D:13
      
  Rest: E:16  F:45  AB:14  CD:25

Step 3: Combine AB and E (next smallest)

          ABE:30
          /      \
        AB:14    E:16
        /  \
      A:5  B:9
      
  Rest: F:45  CD:25  ABE:30

Step 4: Combine CD and ABE (next smallest)

              CDABE:55
              /        \
            CD:25      ABE:30
            /   \      /      \
          C:12  D:13  AB:14  E:16
                      /  \
                    A:5  B:9
                    
  Rest: F:45  CDABE:55

Step 5: Combine F and CDABE (final)

                    ROOT:100
                    /         \
                  F:45     CDABE:55
                           /        \
                         CD:25      ABE:30
                        /    \      /      \
                      C:12  D:13  AB:14  E:16
                              /   \
                            A:5   B:9

FINAL TREE ✓
```

---

## 🔍 PART 3: CORRECTNESS PROOF

### Why Huffman is Optimal

```
THEOREM: HUFFMAN CODING PRODUCES OPTIMAL PREFIX CODE
═════════════════════════════════════════════════════

PROOF STRATEGY: Exchange Argument + Greedy Choice Property

LEMMA 1: GREEDY CHOICE PROPERTY
────────────────────────────────

Claim: In optimal solution, two nodes with minimum frequencies
       should be combined first (deepest in tree, longest paths)

Intuition:
  - Frequently used chars should be near root (short paths)
  - Infrequently used chars should be deep (long paths)
  - Minimize total bits = Σ(freq × depth)
  
Exchange Argument:
  1. Suppose optimal tree doesn't combine min-freq nodes
  2. Let x, y be minimum frequency nodes
  3. Let a, b be nodes combined in optimal tree
  4. Swap positions: put x,y where a,b were, vice versa
  5. Show: new tree is better or equal
     - Putting light nodes deep: +cost
     - Putting heavy nodes shallow: -cost
     - Since x,y minimal: -cost ≥ +cost
     - New tree ≤ old tree in cost
  6. Therefore optimal must combine min nodes ✓

LEMMA 2: OPTIMAL SUBSTRUCTURE
──────────────────────────────

Claim: After combining two minimum-frequency nodes into parent,
       remaining problem (minus those two, plus parent) is same structure
       and must have optimal solution

Proof:
  - After combining x,y into parent p with freq(p) = freq(x)+freq(y)
  - Remaining problem: build optimal tree for n-1 nodes (x,y replaced by p)
  - Solution structure same
  - Must use optimal tree for remaining
  - Otherwise, could improve original (contradiction)

MAIN THEOREM:
─────────────

Huffman procedure:
1. By LEMMA 1: Combines optimal pair at each step
2. By LEMMA 2: Leaves optimal subproblem
3. By induction on remaining nodes:
   - Huffman makes optimal first choice
   - Remaining problem is optimal substructure
   - Recursively, Huffman builds optimal for remaining
   - Base case (1 node): trivially optimal
4. Therefore Huffman produces optimal tree ✓

FORMAL INDUCTION:
─────────────────

Let n = number of characters

Base: n=1
  Single node is optimal prefix code (codeword empty or "0")
  Huffman produces single node ✓

Inductive: Assume Huffman optimal for n-1 characters
  For n characters:
  - By LEMMA 1: Optimal must combine min-freq nodes x,y
  - Huffman combines x,y
  - Creates parent p with freq(p) = freq(x)+freq(y)
  - Problem reduces to n-1 characters (x,y replaced by p)
  - By LEMMA 2: This is optimal substructure
  - By inductive hypothesis: Huffman optimal for n-1
  - Therefore Huffman optimal for n ✓

By induction: Huffman optimal for all n ✓

COST ANALYSIS:
──────────────

Why is minimizing Σ(freq × depth) optimal?

Total bits = Σ(freq[c] × length[code(c)])
           = Σ(freq[c] × depth[c])    (depth = code length in tree)

Each merge operation reduces levels for some nodes
Huffman always merges minimum-frequency subtrees
This minimizes increase in total cost per merge
Result: minimum total cost ✓
```

### Intuitive Explanation

```
WHY HUFFMAN WORKS: INTUITIVE EXPLANATION
═════════════════════════════════════════════════════

Core Insight:
  "Expensive" nodes (high frequency) should have SHORT paths
  "Cheap" nodes (low frequency) can have LONG paths

Why combine minimums first?
  - Start with 6 separate nodes
  - Need to combine them into tree
  - Two nodes with min frequency matter least
  - Putting them deep costs least
  - So combine them first (they go deepest naturally)
  
Cascading effect:
  - Min nodes go deepest
  - Max nodes go shallowest
  - All intermediate nodes sorted by frequency
  
Total cost analysis:
  Every bit of depth = extra cost for that character
  Huffman minimizes bits-weighted-by-frequency
  = Minimum possible total cost

Greedy property:
  - Local decision: combine minimum pair
  - Global guarantee: minimum total cost
  - Why? Because depth cost is multiplicative with frequency
  - Minimum depths for minimum frequencies = minimum total
```

---

## 🔍 PART 4: HUFFMAN CODING APPLICATIONS

### Real-World Uses

```
APPLICATIONS OF HUFFMAN CODING
═════════════════════════════════════════════════════

1. TEXT COMPRESSION
   - Used in ZIP, GZIP, BZIP2
   - Creates variable-length codes for letters
   - Smaller file size (25-30% compression typical)

2. IMAGE COMPRESSION
   - JPEG uses Huffman as final stage
   - After quantization, uses Huffman for lossless compression
   - Reduces file size while preserving image quality

3. VIDEO COMPRESSION
   - MPEG, H.264 standards use Huffman
   - Encodes motion vectors and coefficients
   - Part of multi-stage compression pipeline

4. AUDIO COMPRESSION
   - MP3 uses Huffman in encoding stage
   - Compresses psychoacoustic data
   - Human can't hear compressed frequencies anyway

5. COMMUNICATION
   - Morse code is manually-designed prefix code
   - More common letters (E, T) have shorter codes
   - Less common letters (Q, Z) have longer codes
   - Huffman is optimal version of this idea

6. DATA TRANSMISSION
   - Reduces bandwidth needed
   - Faster transmission
   - Less energy consumption
```

### Huffman vs Arithmetic Coding

```
COMPARISON: HUFFMAN vs ARITHMETIC CODING
═════════════════════════════════════════════════════

HUFFMAN CODING:
  - Each character gets exact integer number of bits
  - Codes are prefix-free
  - Time: O(n log n)
  - Space: O(n)
  - Compression: Good (typically 20-30%)
  - Implementation: Simple
  - Overhead: Tree must be stored

ARITHMETIC CODING:
  - Entire message encoded as single fraction [0,1)
  - Characters get fractional bits (e.g., 2.3 bits)
  - Better compression (1-5% better than Huffman)
  - Time: O(n) encoding, O(n) decoding
  - Space: O(n)
  - Implementation: Complex
  - Patents: Historically patent-encumbered
  - Modern: Some patents expired, but complex

WHY HUFFMAN STILL USED:
  ✓ Simple to understand and implement
  ✓ Fast encoding/decoding
  ✓ Good compression for most data
  ✓ No patent issues
  ✓ Hybrid approach: Huffman for simple cases, arithmetic for complex
```

---

## 🎯 DAY 3 SUMMARY

### What You Learned

```
✓ Prefix codes and why they enable unique decoding
✓ Representing codes as binary trees
✓ Variable-length vs fixed-length codes
✓ Huffman algorithm and greedy tree construction
✓ Step-by-step tree building process
✓ Correctness proof using exchange argument
✓ Optimal substructure in Huffman
✓ Real-world applications and compression results
```

### Key Insights

```
1. Huffman is GREEDY: combines minimum frequencies
2. Greedy is OPTIMAL: proved by exchange argument
3. Optimal substructure: subproblems are same type
4. Depth matters: determines code length
5. Frequency weighted: minimize total bits
6. Tree structure: determines codes
7. Applications: everywhere (ZIP, JPEG, MPEG, MP3)
```

---

# 📅 DAY 4: FRACTIONAL KNAPSACK & JOB SCHEDULING

## 🎯 Day Objective
Master knapsack variants, scheduling problems, and understand why greedy works for some and fails for others.

### ⏱️ Time Allocation
- Knapsack Problem Family: 25 minutes
- Fractional Knapsack: 20 minutes
- Job Scheduling: 25 minutes
- Analysis & Comparison: 20 minutes

---

## 🔍 PART 1: KNAPSACK PROBLEM FAMILY

### The Three Variants

```
KNAPSACK PROBLEM VARIANTS
═════════════════════════════════════════════════════

Imagine: Hiker with knapsack capacity W
         Items with weight w[i] and value v[i]
         Maximize total value without exceeding capacity

THREE VARIANTS:

1. UNBOUNDED KNAPSACK
   ├─ Unlimited copies of each item
   ├─ Can take same item multiple times
   ├─ Solution: DP
   ├─ Time: O(n×W)
   └─ Greedy: FAILS (might need multiple copies)

2. FRACTIONAL KNAPSACK
   ├─ Can take FRACTION of each item
   ├─ Can take 0.5 kg of 1 kg item
   ├─ Solution: GREEDY works!
   ├─ Time: O(n log n)
   └─ Greedy: Sort by value/weight ratio, pick greedily

3. 0-1 KNAPSACK (Most famous)
   ├─ Either take entire item or leave it
   ├─ Can't split items
   ├─ Solution: DP
   ├─ Time: O(n×W)
   └─ Greedy: FAILS (can't split, decisions complex)

KEY DIFFERENCE: Fractional allows splitting → greedy works!
                0-1 doesn't allow splitting → DP needed
```

### Visual Comparison

```
EXAMPLE: Capacity = 10 kg

Item A: weight=5 kg, value=10 (value/weight = 2.0)
Item B: weight=4 kg, value=5  (value/weight = 1.25)
Item C: weight=3 kg, value=8  (value/weight = 2.67)

UNBOUNDED KNAPSACK:
  Greedy by value/weight: C (2.67), A (2.0), B (1.25)
  Pick 2×C + partial A? Or 2×A + partial C?
  Actually optimal: 2×A + 1×C = value 28, weight 13... TOO HEAVY!
  Optimal: 2×A + nothing = value 20, weight 10
  But greedy might: C + A + C = too heavy (11 kg)
  Solution: DP needed

FRACTIONAL KNAPSACK:
  Greedy by value/weight: C (2.67), A (2.0), B (1.25)
  Pick full C: weight=3, value=8, remaining=7
  Pick full A: weight=5, value=10, total weight=8, value=18, remaining=2
  Pick partial B: 2/4 of B = 1 kg at 1.25 ratio = value 2.5
  Total: weight=10, value=20.5 ✓ Optimal!

0-1 KNAPSACK:
  Greedy by value/weight: C (2.67), A (2.0), B (1.25)
  Pick C: weight=3, value=8, remaining=7
  Pick A: weight=5, value=10, total=8, value=18, remaining=2
  Can't pick B (weight 4 > remaining 2)
  Result: weight=8, value=18
  But optimal: A+B = weight=9, value=15... WORSE!
               C+B = weight=7, value=13... WORSE!
               A alone = weight=5, value=10... WORSE!
  Actually optimal is A+C = weight 8, value 18
  So greedy worked here, but not always...
```

---

## 🔍 PART 2: FRACTIONAL KNAPSACK - GREEDY WORKS!

### The Algorithm

```
FRACTIONAL KNAPSACK ALGORITHM
═════════════════════════════════════════════════════

INPUT:
  - n items with weight w[i] and value v[i]
  - Knapsack capacity W

OUTPUT:
  - Selection of items (possibly fractional) with max value
  - Total weight ≤ W

ALGORITHM:
1. Compute ratio[i] = v[i] / w[i] for each item
   (value per unit weight)

2. Sort items by ratio in DESCENDING order
   (highest value per unit first)

3. Greedily select:
   for each item (in sorted order):
       if remaining capacity ≥ weight[i]:
           Take entire item
           capacity -= weight[i]
       else:
           Take partial item (fill knapsack)
           capacity = 0
           break
       endif
   endfor

4. Return selected items

TIME COMPLEXITY:
  Sorting: O(n log n)
  Selection: O(n)
  Total: O(n log n)

SPACE: O(n)
```

### Step-by-Step Example

```
FRACTIONAL KNAPSACK EXAMPLE
═════════════════════════════════════════════════════

Items:
  Item A: weight=2, value=10    → ratio=5.0
  Item B: weight=3, value=6     → ratio=2.0
  Item C: weight=5, value=12    → ratio=2.4
  Item D: weight=7, value=8     → ratio≈1.14
  Item E: weight=1, value=4     → ratio=4.0

Knapsack capacity: W = 10

STEP 1: Compute ratios
        (shown above)

STEP 2: Sort by ratio (descending)
        A:5.0, E:4.0, C:2.4, B:2.0, D:1.14

STEP 3: Greedily select

        capacity = 10, value = 0

Item A (weight=2, value=10, ratio=5.0):
  - Remaining capacity 10 ≥ weight 2? YES
  - Take entire item
  - capacity = 10 - 2 = 8
  - value = 0 + 10 = 10
  - selected: [A (full)]

Item E (weight=1, value=4, ratio=4.0):
  - Remaining capacity 8 ≥ weight 1? YES
  - Take entire item
  - capacity = 8 - 1 = 7
  - value = 10 + 4 = 14
  - selected: [A (full), E (full)]

Item C (weight=5, value=12, ratio=2.4):
  - Remaining capacity 7 ≥ weight 5? YES
  - Take entire item
  - capacity = 7 - 5 = 2
  - value = 14 + 12 = 26
  - selected: [A (full), E (full), C (full)]

Item B (weight=3, value=6, ratio=2.0):
  - Remaining capacity 2 ≥ weight 3? NO
  - Take PARTIAL item: 2/3 of item B
  - Partial value = (2/3) × 6 = 4.0
  - capacity = 2 - 2 = 0
  - value = 26 + 4 = 30
  - selected: [A (full), E (full), C (full), B (2/3)]

Item D: capacity = 0, can't take anything

FINAL RESULT:
  Total weight: 2 + 1 + 5 + 2 = 10 ✓
  Total value: 10 + 4 + 12 + 4 = 30 ✓

VERIFICATION: This is indeed optimal for fractional knapsack
```

### Why Greedy Works: Proof

```
THEOREM: GREEDY IS OPTIMAL FOR FRACTIONAL KNAPSACK
═════════════════════════════════════════════════════

PROOF:
─────

Claim: Greedy algorithm produces maximum value

Proof by contradiction using Exchange Argument:

1. Assume greedy solution G is not optimal
   Let O = some optimal solution with higher value than G
   
2. Since G and O are different:
   Must exist item i where:
   - G takes item i (fully or partially)
   - O doesn't take i, OR takes different amount
   
3. Let item j have highest ratio (value/weight) among:
   - Items in O but not in G, OR
   - Items taken partially differently
   
4. Since greedy sorts by ratio:
   j must have lower ratio than items greedy took before stopping
   
   Actually, let's think differently...
   
CLEANER PROOF:
──────────────

Consider ANY optimal solution O

Observation 1: O cannot beat greedy by taking "bad" items
              (low value/weight ratio items)
              
              Reason: Can always swap low-ratio item from O
                      with high-ratio item from greedy
                      Would only increase O's value
                      Contradicting O being optimal or different

Observation 2: O fills knapsack to same total weight as greedy
              (if not, could add more to O, increasing value)

Observation 3: Greedy takes items in ratio order
              If O takes same items in different order,
              total value same
              If O takes different items with same total weight:
              - O must be missing some high-ratio items from greedy
              - O must have some low-ratio items
              - Swap would increase value
              - Contradicts O being optimal

Therefore: O cannot have higher value than G ✓
Greedy is optimal!

INTUITION:
──────────

Why is this intuitive?
  - Fractional means no "packing problem"
  - Can always fill exactly to capacity
  - To maximize value with fixed weight:
    Choose items with best bang-for-buck (value/weight)
  - Simple as that!
  - Greedy choice property: highest ratio belongs in solution
  - Optimal substructure: after taking item, remaining weight
                          is independent subproblem of same type
```

---

## 🔍 PART 3: JOB SCHEDULING WITH DEADLINES

### The Problem

```
JOB SCHEDULING WITH DEADLINES
═════════════════════════════════════════════════════

PROBLEM STATEMENT:

INPUT:
  - n jobs
  - Each job i has:
    * deadline d[i]: must complete by this time
    * profit p[i]: reward if completed by deadline
  - Each job takes 1 unit of time
  - Can process one job per time unit
  - If job completes after deadline: profit = 0

OUTPUT:
  - Schedule to maximize total profit
  - Job schedule = permutation of jobs

CONSTRAINTS:
  - Each job done in unit time (1 hour, 1 day, etc.)
  - Can't parallelize
  - Can't split jobs
  - Must complete by deadline to earn profit
  - Optimal to use earliest possible deadlines

EXAMPLE:

Jobs:
  Job 1: deadline=2, profit=100
  Job 2: deadline=1, profit=19
  Job 3: deadline=2, profit=27
  Job 4: deadline=1, profit=25
  Job 5: deadline=3, profit=15
  Job 6: deadline=3, profit=20

Can do 3 jobs maximum (deadlines span 1-3, one per unit time)

Possible schedule 1: Do jobs 1,2,3 at times 1,2,3
  Job 2 at time 1 (deadline 1): Profit 19 ✓
  Job 1 at time 2 (deadline 2): Profit 100 ✓
  Job 3 at time 3 (deadline 2): Profit 0 ✗ (too late!)
  Total: 119

Possible schedule 2: Do jobs 1,3,6 at times 1,2,3
  Job 3 at time 1 (deadline 2): Profit 27 ✓
  Job 1 at time 2 (deadline 2): Profit 100 ✓
  Job 6 at time 3 (deadline 3): Profit 20 ✓
  Total: 147

Schedule 2 is better! But how to find it?
```

### Greedy Algorithm: Sort by Profit

```
JOB SCHEDULING GREEDY ALGORITHM
═════════════════════════════════════════════════════

INTUITION:
  - Profit is what we want to maximize
  - So prioritize high-profit jobs
  - Schedule them as late as possible (before deadline)
  - This leaves room for other jobs

ALGORITHM:

1. Sort jobs by profit in DESCENDING order
   (highest profit first)

2. For each job (in order):
   - Find latest time slot before deadline where can place it
   - If found: schedule job at that time
   - If not found: skip job (can't fit by deadline)

3. Return schedule

TIME:
  Sorting: O(n log n)
  Scheduling: O(n²) with simple array (for each job, find slot)
              O(n log n) with better data structure
  Total: O(n²) simple, O(n log n) advanced

SPACE: O(n)
```

### Step-by-Step Example

```
JOB SCHEDULING EXECUTION
═════════════════════════════════════════════════════

Jobs:
  Job 1: deadline=2, profit=100
  Job 2: deadline=1, profit=19
  Job 3: deadline=2, profit=27
  Job 4: deadline=1, profit=25
  Job 5: deadline=3, profit=15
  Job 6: deadline=3, profit=20

STEP 1: Sort by profit (descending)
        Job 1 (100), Job 3 (27), Job 6 (20), Job 4 (25), Job 5 (15), Job 2 (19)
        
Wait, Job 4 (25) > Job 6 (20), so correct order:
        Job 1 (100), Job 4 (25), Job 3 (27), Job 6 (20), Job 2 (19), Job 5 (15)

Actually, let me sort correctly:
        Job 1 (100), Job 3 (27), Job 4 (25), Job 6 (20), Job 2 (19), Job 5 (15)

STEP 2: Schedule each job

Slots available: [_, _, _]  (times 1, 2, 3)

Job 1 (deadline=2, profit=100):
  - Latest slot before deadline 2: time 2
  - Time 2 free? YES
  - Schedule at time 2
  - Slots: [_, 1, _]

Job 3 (deadline=2, profit=27):
  - Latest slot before deadline 2: time 2
  - Time 2 free? NO (Job 1 there)
  - Next latest: time 1
  - Time 1 free? YES
  - Schedule at time 1
  - Slots: [3, 1, _]

Job 4 (deadline=1, profit=25):
  - Latest slot before deadline 1: time 1
  - Time 1 free? NO (Job 3 there)
  - No other slots before deadline 1
  - Skip Job 4 ✗

Job 6 (deadline=3, profit=20):
  - Latest slot before deadline 3: time 3
  - Time 3 free? YES
  - Schedule at time 3
  - Slots: [3, 1, 6]

Job 2 (deadline=1, profit=19):
  - Latest slot before deadline 1: time 1
  - Time 1 free? NO
  - Skip Job 2 ✗

Job 5 (deadline=3, profit=15):
  - Latest slot before deadline 3: time 3
  - Times 3 free? NO (Job 6 there)
  - Time 2? NO (Job 1 there)
  - Time 1? NO (Job 3 there)
  - Skip Job 5 ✗

FINAL SCHEDULE:
  Time 1: Job 3 (deadline 2, profit 27) ✓
  Time 2: Job 1 (deadline 2, profit 100) ✓
  Time 3: Job 6 (deadline 3, profit 20) ✓
  
Total profit: 27 + 100 + 20 = 147 ✓

VERIFICATION: Is this optimal?
  Maximum possible (3 jobs, deadlines span 1-3): One job per time unit
  We selected: Jobs 1, 3, 6 all meet deadlines
  Total: 147
  
  Can we do better? Other combinations:
  Jobs 1,4,5: 100+25+15 = 140 < 147
  Jobs 1,4,6: 100+25+20 = 145 < 147
  Jobs 1,3,4: Would need times 1,2,2 but only one job per time!
  
  147 is indeed optimal! ✓
```

### Why Greedy Works

```
THEOREM: GREEDY JOB SCHEDULING IS OPTIMAL
═════════════════════════════════════════════════════

PROOF:
─────

Key Observation: Maximum jobs = number of distinct deadlines
                 (or less, if conflicts)
                 
Once we know which jobs to select:
  How to schedule them to maximize profit?
  Answer: REVERSE order of deadlines
         Schedule latest-deadline job in latest slot
         Schedule next-latest in next slot
         Etc.

Why greedy by profit works:

1. We want k highest-profit jobs that can fit
   (where k is maximum achievable)

2. Any valid schedule of n jobs has same total profit
   regardless of order (payment doesn't change if deadline met)

3. To maximize profit, we want highest-profit jobs in any valid schedule

4. Greedy property: We should definitely take highest-profit job
   because:
   - If don't take it, must take some lower-profit job instead
   - Value decreased
   - Solution can't be optimal

5. After taking highest-profit job:
   - Subproblem: schedule remaining jobs with remaining slots
   - Same structure: find highest-profit schedulable jobs
   - By induction: greedy optimal for remaining

6. Therefore: greedy produces optimal schedule ✓

Exchange Argument:
─────────────────

Suppose optimal solution O doesn't include highest-profit job J

Case 1: O has fewer jobs than greedy
        Then greedy can fit J (greedy found valid schedule)
        So greedy beats O (higher profit)
        Contradiction with O being optimal

Case 2: O has same number of jobs as greedy but not J
        Then O has some job J' with profit < J's profit
        Replace J' with J in O (if deadline allows)
        New schedule has higher profit
        Contradiction with O being optimal

Case 3: J can't fit because all slots have earlier-deadline jobs
        But greedy found valid schedule including J
        Contradiction (J fits for greedy)

In all cases: O can't beat greedy ✓
Greedy is optimal!
```

---

## 🔍 PART 4: COMPARISON & ANALYSIS

### When Greedy Works vs Fails

```
GREEDY ALGORITHM EFFECTIVENESS MATRIX
═════════════════════════════════════════════════════

PROBLEM TYPE              │ GREEDY WORKS? │ REASON/NOTES
──────────────────────────┼───────────────┼─────────────────────
Activity Selection        │ ✓ YES         │ Greedy stays ahead
Huffman Coding            │ ✓ YES         │ Optimal substructure
Fractional Knapsack       │ ✓ YES         │ Can fill to capacity
Job Scheduling (deadline) │ ✓ YES         │ Choose high profit
0-1 Knapsack              │ ✗ NO          │ Can't split items
Weighted Interval Sched.  │ ✗ NO          │ Complex dependencies
MST (Kruskal, Prim)       │ ✓ YES         │ Matroid properties
Dijkstra Shortest Path    │ ✓ YES         │ Triangle inequality
Coin Change (any denoms)  │ ✗ NO          │ Not always optimal
Coin Change (canonical)   │ ✓ YES         │ By design
```

### Key Differences: Fractional vs 0-1

```
FRACTIONAL vs 0-1 KNAPSACK: DETAILED COMPARISON
═════════════════════════════════════════════════════

DIMENSION              │ FRACTIONAL     │ 0-1 KNAPSACK
──────────────────────┼────────────────┼──────────────
Splitting items       │ ✓ Allowed      │ ✗ Not allowed
Greedy works?         │ ✓ YES          │ ✗ NO
Algorithm             │ Greedy         │ Dynamic Programming
Time complexity       │ O(n log n)     │ O(n × W)
Space complexity      │ O(n)           │ O(n × W)
Why greedy works      │ Fill exactly   │ Can't fill exactly
                      │ to capacity    │ due to discrete items
Optimal value         │ Always fills   │ May leave space
                      │ to W           │
Mathematical proof    │ Exchange arg   │ Complex recurrence
Best solution quality │ Always optimal │ Always optimal
Easy to implement     │ ✓ Very easy    │ ✓ Medium difficulty

EXAMPLE: Capacity 10, items A(w=6,v=30), B(w=5,v=25), C(w=5,v=24)

FRACTIONAL:
  - Ratios: A(5.0), B(5.0), C(4.8)
  - Take A fully: w=6, v=30
  - Take B fully: w=11... TOO HEAVY, but take 4/5 of B: w=10, v=30+20=50
  - Fractional optimal: 50

0-1:
  - Try A+B: w=11... TOO HEAVY
  - Try A alone: w=6, v=30
  - Try B+C: w=10, v=49
  - Try others...
  - 0-1 optimal: 49 (not fractional 50)
  
Difference: 50 vs 49 (fractional allows optimal filling)
```

---

## 🎯 DAY 4 SUMMARY

### What You Learned

```
✓ Knapsack problem family (unbounded, fractional, 0-1)
✓ Why greedy works for fractional but not 0-1
✓ Fractional knapsack: sort by value/weight ratio
✓ Job scheduling with deadlines: sort by profit
✓ Greedy scheduling by placing jobs before deadlines
✓ Correctness proofs for both algorithms
✓ Exchange argument for job scheduling
✓ When greedy works vs when DP needed
✓ Complexity analysis and comparisons
```

### Key Takeaways

```
1. Greedy by ratio works when can split (fractional)
2. Greedy by profit works when scheduling with deadlines
3. Can't split = must use DP (0-1 knapsack)
4. Fill to capacity = greedy works
5. Exchange argument = standard proof technique
6. Optimal substructure = required for both
7. Different problems need different sorting criteria
8. Proof determines confidence in algorithm
```

---

# 📅 DAY 5: GREEDY IN SYSTEMS & ADVANCED TOPICS

## 🎯 Day Objective
Understand greedy in networks, caching, approximation algorithms, and when greedy fails.

### ⏱️ Time Allocation
- Greedy in Networks: 20 minutes
- Caching and LRU: 20 minutes
- Approximation Algorithms: 20 minutes
- When Greedy Fails: 20 minutes
- Integration & Reflection: 10 minutes

---

## 🔍 PART 1: GREEDY IN NETWORKS - MST

### Minimum Spanning Tree Problem

```
MINIMUM SPANNING TREE (MST)
═════════════════════════════════════════════════════

INPUT:
  - Connected graph G with weighted edges
  - Edge weight = cost to connect two vertices

OUTPUT:
  - Spanning tree (n vertices, n-1 edges)
  - Total edge weight = minimum possible

DEFINITION:
  Spanning tree: connects all n vertices with n-1 edges, no cycles
  Minimum: sum of edge weights is smallest possible

WHY IMPORTANT:
  - Network design: minimize total cable length
  - Road construction: minimize total road distance
  - Electrical grids: minimize wire length
  - Many optimization problems reduce to MST

EXAMPLE:

      1
    /   \
   2  -  3   Edge weights: 1-2=7, 1-3=9, 2-3=2
   
MST possibilities:
  Option A: edges 1-2 and 2-3: weight 7+2 = 9 ✓ Minimal
  Option B: edges 1-2 and 1-3: weight 7+9 = 16
  Option C: edges 1-3 and 2-3: weight 9+2 = 11

Minimum spanning tree: Option A (weight 9)
```

### Kruskal's Algorithm - Greedy MST

```
KRUSKAL'S ALGORITHM (Greedy approach)
═════════════════════════════════════════════════════

ALGORITHM:
1. Sort all edges by weight (ascending)

2. Initialize: Each vertex is its own component

3. For each edge (u,v) in sorted order:
   - If u and v in different components:
       Add edge to MST
       Merge their components
   - Else: Skip edge (would create cycle)

4. Continue until n-1 edges added

TIME:
  Sorting: O(E log E)
  Union-Find operations: O(E α(V)) where α is inverse Ackermann
  Total: O(E log E)

DATA STRUCTURE:
  Union-Find (Disjoint Set Union) for component tracking

WHY IT'S GREEDY:
  - Greedy choice: always pick cheapest available edge
  - Doesn't create cycle (enforced by union-find)
  - Hope: cheapest edges build optimal tree
```

### Step-by-Step Kruskal Example

```
KRUSKAL'S ALGORITHM EXECUTION
═════════════════════════════════════════════════════

Graph:
      1
     /|\
    7 | 5
   /  |  \
  2 - 2 - 3
   \     /
    6   4

Edges and weights:
  2-3: weight 2
  1-3: weight 4
  1-2: weight 5
  1-3: weight 5
  2-1: weight 6
  1-2: weight 7

Sorted edges by weight:
  2-3:2, 1-3:4, 1-3:5, 2-1:6, 1-2:7

STEP 1: Sort done (listed above)

STEP 2: Initialize
        Components: {1}, {2}, {3}
        MST edges: []
        Weight: 0

STEP 3: Process each edge

Edge 2-3 (weight 2):
  - Vertices 2,3 in different components? YES ({2} and {3})
  - Add to MST
  - Merge components: {1}, {2,3}
  - MST edges: [2-3]
  - Weight: 2

Edge 1-3 (weight 4):
  - Vertices 1,3 in different components? YES ({1} and {2,3})
  - Add to MST
  - Merge components: {1,2,3}
  - MST edges: [2-3, 1-3]
  - Weight: 6

Edge 1-3 (weight 5):
  - Vertices 1,3 in different components? NO (both in {1,2,3})
  - Skip (would create cycle)

Edge 2-1 (weight 6):
  - Vertices 2,1 in different components? NO (both in {1,2,3})
  - Skip (would create cycle)

Edge 1-2 (weight 7):
  - Vertices 1,2 in different components? NO (both in {1,2,3})
  - Skip (would create cycle)

DONE: 3 vertices need 2 edges, we have 2 edges

FINAL MST:
  Edges: 2-3, 1-3
  Total weight: 2 + 4 = 6
  
Visual MST:
      1
      |
      | (weight 4)
    3 -
      | (weight 2)
      2
```

### Why Kruskal is Optimal

```
THEOREM: KRUSKAL'S ALGORITHM IS OPTIMAL (Produces MST)
═════════════════════════════════════════════════════

PROOF: Exchange Argument
────────────────────────

Key Insight: Greedy choice (cheapest edge not creating cycle)
             is in SOME optimal solution

Lemma (Greedy Choice Property):
  For any connected graph, there exists an MST containing
  the cheapest edge that doesn't create a cycle

Proof:
  1. Let e = cheapest non-cycle edge
  2. Consider ANY MST T
  3. If e ∈ T: Done ✓
  4. If e ∉ T:
     - Adding e to T creates exactly one cycle (property of trees)
     - e connects two components (else would create cycle)
     - In the cycle, there must be another edge e' connecting
       the two components that e connects
     - e' ≥ e in weight (e is cheapest non-cycle edge)
     - Remove e' from T, add e
     - New tree has weight ≤ old tree's weight
     - So e is in some optimal solution ✓

Optimal Substructure:
  After adding edge e to growing MST:
  - Remaining edges form subgraph
  - Must select MST from remaining edges
  - Can recursively apply Kruskal
  - Subproblem is same type (smaller graph MST)

Conclusion by Induction:
  - Greedy picks optimal first edge (by lemma)
  - Leaves optimal substructure (smaller MST problem)
  - Recursively, Kruskal builds optimal MST
  - Proven ✓
```

### Prim's Algorithm - Alternative Greedy

```
PRIM'S ALGORITHM (Alternative greedy MST)
═════════════════════════════════════════════════════

ALGORITHM:
1. Start with any vertex (e.g., vertex 1)

2. Maintain:
   - IN_TREE: vertices already in MST
   - OUT_TREE: vertices not in MST yet

3. Repeat n-1 times:
   - Find cheapest edge from IN_TREE to OUT_TREE
   - Add that edge and vertex to MST
   - Move vertex from OUT_TREE to IN_TREE

TIME:
  With binary heap: O(E log V)
  With Fibonacci heap: O(E + V log V)

WHY IT'S GREEDY:
  - Greedy choice: cheapest edge leaving current tree
  - Builds tree incrementally from one vertex
  - Always adds best next edge to extend tree

COMPARISON: Kruskal vs Prim
─────────────────────────────
Kruskal:
  ✓ Works on disconnected graphs
  ✓ Easier to understand and code
  ✗ Must sort all edges

Prim:
  ✓ Better for dense graphs
  ✓ Better with Fibonacci heap
  ✗ Must maintain priority queue

Both are correct, both greedy, both produce MST
Choose based on graph properties and efficiency needs
```

---

## 🔍 PART 2: CACHING AND LRU

### The Caching Problem

```
CACHING PROBLEM
═════════════════════════════════════════════════════

SITUATION:
  - Limited cache (memory) of size k
  - Sequence of page requests comes over time
  - Page in cache: instant access (cost = 0)
  - Page not in cache: must fetch from disk (cost = 1)
  - Cache is full: must EVICT one page to make room

GOAL:
  Minimize total cache misses (page faults)

EXAMPLE: Cache size = 3, Request sequence = [1,2,3,1,2,4,1,2,3,4]

If we choose evictions wisely:
  Request 1: miss, cache = [1]
  Request 2: miss, cache = [1,2]
  Request 3: miss, cache = [1,2,3]
  Request 1: hit (in cache)
  Request 2: hit (in cache)
  Request 4: miss, evict ? , cache = [?,2,3] or [1,?,3] or [1,2,?]
  ...

Different eviction policies lead to different hit rates
```

### LRU Caching Policy

```
LRU (Least Recently Used) CACHING
═════════════════════════════════════════════════════

POLICY: When cache full and need to evict:
        Remove page that was LEAST RECENTLY USED

INTUITION:
  - Recently used pages likely to be used again soon
  - Pages not used in long time less likely needed soon
  - Heuristic: temporal locality

ALGORITHM:
1. Maintain: list of pages in cache (most-recent to least-recent)

2. On page request:
   - If page in cache:
       Move to front (most-recent position)
       Update access time
   - If page not in cache:
       If cache full:
           Remove page from back (least-recent)
       Add new page to front (most-recent)

TIME: O(1) with proper data structure (hash map + doubly linked list)

WHY IT'S GREEDY:
  - Greedy choice: evict page least likely to be needed
  - Assumption: temporal locality (recent = future)
  - Works well in practice

EXAMPLE WITH LRU:

Cache size = 3
Requests: [1, 2, 3, 1, 2, 4, 1, 2, 3, 4]

Request 1: miss, cache = [1], size=1
Request 2: miss, cache = [2,1], size=2
Request 3: miss, cache = [3,2,1], size=3, FULL

Request 1: HIT, cache = [1,3,2] (1 to front)
Request 2: HIT, cache = [2,1,3] (2 to front)

Request 4: MISS, evict LRU = 3
          cache = [4,2,1], size=3

Request 1: HIT, cache = [1,4,2] (1 to front)
Request 2: HIT, cache = [2,1,4] (2 to front)

Request 3: MISS, evict LRU = 4
          cache = [3,2,1], size=3

Request 4: MISS, evict LRU = 1
          cache = [4,3,2], size=3

Total hits: 4
Total misses: 6

With oracle knowledge of future requests, could do better
But LRU is practical and works well in real systems
```

### LRU Optimality (Limited Context)

```
LRU vs OPTIMAL CACHING
═════════════════════════════════════════════════════

Optimal policy (with future knowledge):
  When must evict, remove page needed FURTHEST in future
  (if we knew the future)

Comparison:
  LRU: "Evict least recently used" (look to past)
  OPT: "Evict needed furthest in future" (look to future)

Approximation Quality:
  LRU doesn't achieve OPT in general
  But good practical performance

Why LRU not optimal:
  Example: Cache=2, requests = [1,2,1,2,3,1,2,3,4]
  
  Optimal (knowing future):
    1: miss, cache=[1]
    2: miss, cache=[1,2]
    1: hit
    2: hit
    3: miss, evict 1 (next used at position 6, after 2's at 5)
          cache=[2,3]
    1: miss, evict 2 (next at 8, after 3's at 7)
          cache=[3,1]
    ... continues ...
    Misses: 3
    
  LRU:
    1: miss, cache=[1]
    2: miss, cache=[2,1]
    1: hit, cache=[1,2]
    2: hit, cache=[2,1]
    3: miss, evict LRU=1, cache=[2,3]
    1: miss, evict LRU=2, cache=[1,3]
    2: miss, evict LRU=3, cache=[2,1]
    3: miss, evict LRU=1, cache=[2,3]
    4: miss, evict LRU=2, cache=[3,4]
    Misses: 7

LRU loses here, but only because OPT has future knowledge

In real systems without future knowledge:
  LRU is reasonable greedy approximation
```

---

## 🔍 PART 3: APPROXIMATION ALGORITHMS

### What is Approximation?

```
APPROXIMATION ALGORITHMS
═════════════════════════════════════════════════════

PROBLEM: Some problems are NP-hard
        Computing optimal solution is intractable
        Have unlimited time, can't solve exactly

SOLUTION: Approximation algorithm
         Solves quickly (polynomial time)
         Guaranteed near-optimal solution
         Not optimal, but provably good

QUALITY METRIC: Approximation ratio
               Actual/Optimal ≤ ratio (or ratio × Optimal ≥ Actual)
               
Example: 2-approximation
         Algorithm produces solution at most 2× worse than optimal
         Could be 1× (optimal) or 2× (worst case)

GOALS:
  ✓ Fast algorithm (polynomial time)
  ✓ Guaranteed quality (approximation ratio)
  ✓ Simple to understand
  ✗ Don't need perfect solution
```

### Greedy Set Cover

```
SET COVER PROBLEM (NP-hard)
═════════════════════════════════════════════════════

PROBLEM:
  - Universe U of n elements
  - Collection S of m subsets of U
  - Each subset has a cost
  - Find minimum-cost subcollection covering all elements

EXAMPLE:
  Elements: {1,2,3,4,5,6,7,8,9,10}
  
  Subsets:
    S1 = {1,2,3}, cost = 5
    S2 = {4,5,6}, cost = 5
    S3 = {7,8,9}, cost = 5
    S4 = {1,4,7,10}, cost = 8
    S5 = {2,5,8}, cost = 6
    S6 = {3,6,9}, cost = 4
    S7 = {10}, cost = 1
  
  Must cover all 10 elements with minimum cost

Optimal solution: S1 + S2 + S3 + S7 = cost 16 (or other combos)
                  Or: S4 + S5 + S6 + S7 = cost 19

Actually optimal: S1 + S2 + S3 + S7 = {1..9} ∪ {10} = all, cost 16
```

### Greedy Set Cover Algorithm

```
GREEDY SET COVER ALGORITHM
═════════════════════════════════════════════════════

ALGORITHM:
1. While not all elements covered:
   - Select set with best COST-EFFECTIVENESS
   - Cost-effectiveness = number of NEW uncovered elements / cost
   - Add that set to solution
   - Mark its elements as covered

2. Return collection of selected sets

TIME: O(m × n) for m sets, n elements

WHY IT'S GREEDY:
  - Greedy choice: best cost-effectiveness
  - Assumption: best local choice leads to good global
  - Not guaranteed optimal, but good in practice

EXAMPLE WITH GREEDY:

Elements to cover: {1,2,3,4,5,6,7,8,9,10}
Uncovered: {1,2,3,4,5,6,7,8,9,10}

Iteration 1:
  Cost-effectiveness:
    S1: 3 elements / 5 = 0.6
    S2: 3 elements / 5 = 0.6
    S3: 3 elements / 5 = 0.6
    S4: 4 elements / 8 = 0.5
    S5: 3 elements / 6 = 0.5
    S6: 3 elements / 4 = 0.75 ← Best
    S7: 1 element / 1 = 1.0 ← Even better!
  
  Select S7 (cost = 1, covers {10})
  Uncovered: {1,2,3,4,5,6,7,8,9}

Iteration 2:
  Cost-effectiveness (considering only uncovered):
    S1: 3 / 5 = 0.6
    S2: 3 / 5 = 0.6
    S3: 3 / 5 = 0.6
    S4: 3 / 8 = 0.375 (only 1,4,7 uncovered)
    S5: 3 / 6 = 0.5
    S6: 3 / 4 = 0.75 ← Best
  
  Select S6 (cost = 4, covers {3,6,9})
  Uncovered: {1,2,4,5,7,8}

Iteration 3:
  Cost-effectiveness:
    S1: 2 / 5 = 0.4 (1,2 uncovered)
    S2: 2 / 5 = 0.4 (4,5 uncovered)
    S3: 2 / 5 = 0.4 (7,8 uncovered)
    S4: 2 / 8 = 0.25
    S5: 2 / 6 = 0.33
  
  Select any of S1, S2, S3 (tie, say S1)
  Cost = 5, covers {1,2}
  Uncovered: {4,5,7,8}

Iteration 4:
  Cost-effectiveness:
    S2: 2 / 5 = 0.4 (4,5 uncovered)
    S3: 2 / 5 = 0.4 (7,8 uncovered)
    S4: 2 / 8 = 0.25
    S5: 1 / 6 = 0.16
  
  Select S2, cost = 5, covers {4,5}
  Uncovered: {7,8}

Iteration 5:
  Cost-effectiveness:
    S3: 2 / 5 = 0.4 (7,8 uncovered)
    S4: 1 / 8 = 0.125
    S5: 1 / 6 = 0.16
  
  Select S3, cost = 5, covers {7,8}
  Uncovered: {}

GREEDY SOLUTION:
  S7 + S6 + S1 + S2 + S3 = cost 1+4+5+5+5 = 20

Optimal was: S1 + S2 + S3 + S7 = cost 16

Greedy ratio: 20 / 16 = 1.25 (not optimal, but close)

Note: With different tie-breaking or costs, could be worse
Greedy Set Cover has O(log n) approximation ratio
```

### Approximation Ratio Guarantee

```
THEORETICAL GUARANTEE: GREEDY SET COVER
═════════════════════════════════════════════════════

THEOREM: Greedy Set Cover achieves O(log n) approximation
         where n = number of elements

This means:
  Greedy cost ≤ (ln n + 1) × Optimal cost
  
For n=100 elements:
  ln(100) ≈ 4.6
  Greedy ≤ 5.6 × Optimal
  
For n=1000 elements:
  ln(1000) ≈ 6.9
  Greedy ≤ 7.9 × Optimal

PROOF SKETCH:
  - After k iterations, some optimal solution needs ≥ 1 set
    to cover remaining elements
  - Therefore, best available set covers ≥ 1/k fraction
  - Greedy picks this (approximately)
  - In k iterations where k = O(m), covers all
  - Total cost ≤ (1 + 1/2 + 1/3 + ... + 1/k) × OPT
  - Sum of reciprocals ≤ log n
  - Therefore O(log n) approximation ✓

PRACTICAL QUALITY:
  - Theory says O(log n) worst-case
  - Practice often much better
  - For many real instances, greedy ≤ 1.3 × Optimal
```

---

## 🔍 PART 4: WHEN GREEDY FAILS

### Classic Failures

```
SITUATIONS WHERE GREEDY FAILS
═════════════════════════════════════════════════════

1. 0-1 KNAPSACK
   Greedy: Sort by value/weight, pick highest ratio
   Fails: Can't split items, local choice ≠ global optimal
   
   Example:
   Capacity = 10, Items: A(w=6,v=30), B(w=5,v=20), C(w=5,v=19)
   
   Greedy (by ratio):
     A: 30/6 = 5.0
     B: 20/5 = 4.0
     C: 19/5 = 3.8
   
   Pick A fully (w=6, v=30), remaining capacity = 4
   Can't fit B (w=5) or C (w=5)
   Greedy result: v=30
   
   Optimal: Pick B + C (w=10, v=39)
   Greedy FAILED! (30 < 39)

2. COIN CHANGING (Non-canonical denominations)
   Greedy: Always use largest coin that fits
   Fails: Not always minimal coins
   
   Example: Denominations = {1, 3, 4}, amount = 6
   
   Greedy (by size):
     Use 4: remaining 2
     Use 1: remaining 1
     Use 1: remaining 0
     Total coins: 3 (coins: 4,1,1)
   
   Optimal: Use 3 twice
     Total coins: 2 (coins: 3,3)
   Greedy FAILED! (3 coins > 2 coins)

3. GRAPH COLORING
   Greedy: Color vertices in order, use smallest available color
   Fails: Order matters, global optimum unknown
   
   Example:
     Triangle (3-cycle): needs 3 colors minimum
     Greedy (wrong order): might use 4 colors
     Optimal: 3 colors
   
   Greedy FAILED with poor vertex order!

4. TRAVELLING SALESMAN (TSP)
   Greedy: Nearest neighbor heuristic
   Fails: Local nearest choice ≠ global shortest tour
   
   Example:
     4 cities, find shortest tour visiting all
     Nearest neighbor might pick path: A→B→C→D
     with total length 100
     Optimal tour: A→C→B→D
     with total length 80
   Greedy FAILED! (100 > 80)

5. SCHEDULING (Job sequencing weighted)
   Greedy: Sort by profit/time ratio
   Fails: Dependencies and constraints
   
   If jobs can block each other, greedy choice might
   prevent better overall solution
```

### Why Greedy Fails: Root Causes

```
ROOT CAUSES OF GREEDY FAILURE
═════════════════════════════════════════════════════

CAUSE 1: No Optimal Substructure
──────────────────────────────────
  Greedy choice doesn't guarantee subproblem is optimal
  
  Example: 0-1 Knapsack
  After taking item A, remaining capacity C'
  BUT best use of C' with remaining items might be
  different from optimal use of C with all items
  (because A takes space)

CAUSE 2: No Greedy Choice Property
────────────────────────────────────
  Greedy choice not guaranteed in ANY optimal solution
  
  Example: TSP Nearest Neighbor
  Nearest next city not in all optimal tours
  Could be in some, but no guarantee
  
CAUSE 3: Discrete/Combinatorial Constraints
─────────────────────────────────────────────
  Can't split items, must make binary choices
  
  Example: 0-1 Knapsack
  Can't take 0.5 of item A
  Greedy needs flexibility (fractional knapsack has it)

CAUSE 4: Multiple Objectives or Constraints
─────────────────────────────────────────────
  Different criteria conflict
  
  Example: Scheduling with deadlines AND weights
  High weight but tight deadline vs
  Low weight but flexible deadline
  Greedy can't balance both well

CAUSE 5: Future Dependence
──────────────────────────
  Current choice affects future options badly
  
  Example: Graph Coloring (wrong order)
  Greedy colors vertex A with available color
  But this limits colors for vertex B's neighbors
  Future choice becomes worse
```

### How to Recognize if Greedy Will Fail

```
CHECKLIST: WILL GREEDY WORK?
═════════════════════════════════════════════════════

Before implementing greedy, ask:

1. OPTIMAL SUBSTRUCTURE?
   □ After making greedy choice, is remaining problem
     same structure and must be optimal?
   □ If NO → Greedy likely fails

2. GREEDY CHOICE PROPERTY?
   □ Is greedy choice guaranteed in SOME optimal solution?
   □ Or can I prove exchange argument that it should be?
   □ If NO → Greedy likely fails

3. NO DISCRETE BLOCKING?
   □ Can I split items or is choice binary?
   □ Fractional = greedy OK, binary = DP likely needed
   □ If binary → Check more carefully

4. INDEPENDENT SUBPROBLEMS?
   □ After choosing, do subproblems remain independent?
   □ Or do they interact (constraints link them)?
   □ If linked → Greedy likely fails

5. KNOWN PROBLEM CLASS?
   □ Is this activity selection? Huffman? MST?
   □ Or is it novel problem?
   □ Known class → Existing algorithms, research it
   □ Novel → Risky, prove or test carefully

RESULT:
  ✓ YES to all → Greedy probably works
  ? Maybe some → Be cautious, prove carefully
  ✗ NO to most → Use DP or other approach
```

---

## 🎯 DAY 5 SUMMARY

### What You Learned

```
✓ Greedy in networks: Kruskal and Prim's MST algorithms
✓ Both greedy MST algorithms produce optimal solutions
✓ Caching and LRU policy
✓ LRU as greedy approximation in practice
✓ Approximation algorithms for NP-hard problems
✓ Greedy set cover with O(log n) approximation
✓ Root causes of greedy failure
✓ How to recognize when greedy will or won't work
✓ 0-1 knapsack as classic greedy failure
✓ Integration across all greedy problems
```

### Week 12 Complete Mastery Map

```
GREEDY ALGORITHMS COMPLETE FRAMEWORK
═════════════════════════════════════════════════════

                    GREEDY ALGORITHMS
                           │
            ┌──────────────┬┴──────────────┐
            ▼              ▼               ▼
        WHEN WORKS    ALGORITHM          FAILURES
            │         TEMPLATES              │
        ┌───┴───┐         │            ┌────┴────┐
        │       │         │            │         │
      OPT SUB  GREEDY     │        NO OPT    NO GREEDY
      STRUCT  CHOICE      │        SUBST    CHOICE
        │       │         ▼            │         │
        │       │    ┌─────────────┐   │         │
        │       │    │1. SORT      │   │     0-1 KNP
        │       │    │2. INIT      │   │     TSP
        └───┬───┘    │3. LOOP      │   │     Graph Color
            ▼        │4. SELECT    │   │     Weighted Sched
         PROOF       │5. RETURN    │   │
            │        └─────────────┘   │
        ┌───┴──────┬──────────────┐    └─────────┘
        ▼          ▼              ▼
     EXCHANGE  INDUCTION    GREEDY STAYS
     ARGUMENT             AHEAD TECHNIQUE

APPLICATIONS:
Activity Selection → Exchange Argument
Huffman → Optimal substructure
Fractional KNP → Greedy by ratio
Job Scheduling → Greedy by profit
MST → Exchange Argument (both algorithms)
Set Cover → O(log n) approximation
```

---

# 🏆 WEEK 12 COMPLETE SUMMARY

## 📊 Topics Covered

```
DAY 1: FUNDAMENTALS
  ✓ What is a greedy algorithm
  ✓ Optimal substructure
  ✓ Greedy choice property
  ✓ Exchange argument (main proof technique)
  ✓ Correctness proof strategy (5-step process)
  ✓ Generic greedy template

DAY 2: INTERVAL PROBLEMS
  ✓ Activity selection (core problem)
  ✓ Greedy stays ahead technique
  ✓ Greedy by finish time works
  ✓ Weighted interval scheduling (DP, not greedy)
  ✓ Minimum rooms (sweep line algorithm)
  ✓ Job scheduling with deadlines

DAY 3: HUFFMAN CODING
  ✓ Prefix codes and unique decoding
  ✓ Optimal prefix code problem
  ✓ Binary tree representation
  ✓ Variable-length vs fixed-length codes
  ✓ Huffman algorithm (greedy tree construction)
  ✓ Correctness proof (exchange + substructure)
  ✓ Real-world applications (ZIP, JPEG, MPEG, MP3)

DAY 4: KNAPSACK & SCHEDULING
  ✓ Knapsack problem family (unbounded, fractional, 0-1)
  ✓ Fractional knapsack (greedy by ratio works)
  ✓ 0-1 knapsack (DP needed, greedy fails)
  ✓ Job scheduling with deadlines (greedy by profit)
  ✓ Proof: greedy optimal via exchange argument
  ✓ Complexity analysis and comparisons

DAY 5: ADVANCED TOPICS
  ✓ Minimum spanning tree (Kruskal greedy)
  ✓ Prim's algorithm (alternative greedy MST)
  ✓ Caching and LRU policy
  ✓ Approximation algorithms (NP-hard)
  ✓ Greedy set cover (O(log n) approximation)
  ✓ When greedy fails: root causes
  ✓ Checklist: will greedy work?
```

## 🎯 Key Insights

```
INSIGHT 1: GREEDY IS NOT UNIVERSAL
  - Only works for specific problem structures
  - Must have optimal substructure AND greedy choice property
  - Many problems require DP or other approaches

INSIGHT 2: PROOF IS ESSENTIAL
  - Never assume greedy works without proof
  - Exchange argument is most common proof technique
  - Induction completes the argument

INSIGHT 3: SORTING DETERMINES EVERYTHING
  - Correct sort criterion is THE critical choice
  - Wrong sort = wrong algorithm
  - Activity selection: by finish time ✓
  - Job scheduling: by profit ✓
  - Huffman: by frequency ✓

INSIGHT 4: OPTIMAL SUBSTRUCTURE IS KEY
  - After greedy choice, subproblem must be optimal
  - Subproblem same structure as original
  - Allows recursive/inductive reasoning

INSIGHT 5: GREEDY VS DP DECISION
  - Fractional items → Greedy (can fill exactly)
  - Discrete items → DP (can't split)
  - Weighted→DP, unweighted→ Greedy often
  - Small capacity → DP, large → Greedy

INSIGHT 6: APPROXIMATION IS OK SOMETIMES
  - NP-hard problems don't need exact solutions
  - O(log n) approximation better than no solution
  - Greedy often practical even if not optimal
  - Set cover: 20% worse than optimal typical
  - LRU cache: reasonable in practice

INSIGHT 7: FAILURES ARE INSTRUCTIVE
  - 0-1 knapsack shows why discrete breaks greedy
  - TSP shows why no greedy choice property
  - Graph coloring shows order dependence
  - Understand failures → understand prerequisites
```

## 💡 Problem-Solving Checklist

```
FOR ANY PROBLEM, ASK:

1. STRUCTURE ANALYSIS
   □ What is being maximized/minimized?
   □ Are choices independent or linked?
   □ Can I verify optimal substructure?

2. GREEDY FEASIBILITY
   □ Is there obvious "best" choice?
   □ Does choice belong in all optima?
   □ Can I exchange to prove greedy choice?

3. PROOF STRATEGY
   □ Identify optimal substructure
   □ Prove greedy choice property (exchange arg)
   □ Show inductive structure
   □ Complete induction proof

4. IMPLEMENTATION
   □ Choose sort criterion
   □ Implement template
   □ Add feasibility checks
   □ Verify on examples

5. VALIDATION
   □ Test on provided examples
   □ Test on edge cases
   □ Trace execution
   □ Compare to known solutions

RESULT:
  ✓ If all confirmed → Greedy likely works
  ✗ If any fails → Try DP or exhaustive search
```

---

# 🎓 WEEK 12 LEARNING OUTCOMES

By end of Week 12, you should be able to:

### Conceptual Mastery
```
✓ Define greedy algorithm precisely
✓ Explain optimal substructure and greedy choice property
✓ Recognize when problem is amenable to greedy
✓ Understand exchange argument proof technique
✓ Explain "greedy stays ahead" reasoning
✓ Compare greedy to dynamic programming
✓ Understand approximation algorithms
✓ Know when and why greedy fails
```

### Problem-Solving Skills
```
✓ Identify optimal substructure in problem
✓ Design greedy solution for structured problems
✓ Choose correct sorting criterion
✓ Implement greedy template efficiently
✓ Prove correctness via exchange argument
✓ Analyze time and space complexity
✓ Verify solution on examples
✓ Recognize when greedy won't work
```

### Specific Algorithms
```
✓ Activity selection: greedy by finish time
✓ Huffman coding: greedy by frequency
✓ Fractional knapsack: greedy by value/weight
✓ Job scheduling: greedy by profit
✓ Kruskal's MST: greedy by edge weight
✓ Prim's MST: greedy by minimum edge to tree
✓ LRU caching: greedy by recency
✓ Set cover approximation: greedy by cost-effectiveness
```

### Transfer Skills
```
✓ Recognize problem structure in new problems
✓ Apply greedy template to new domains
✓ Adapt existing algorithms to variations
✓ Combine greedy with other techniques
✓ Estimate approximation quality
✓ Choose appropriate algorithm paradigm
```

---

**Status: WEEK 12 COMPLETE - Ready for Week 13**

**Generate Week 13 in next prompt**
