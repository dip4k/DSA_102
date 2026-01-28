# 🟧 WEEK 12: GREEDY ALGORITHMS & PROOFS

## Comprehensive Concept-Based Learning Playbook

**Duration:** 25 hours | **Days:** 5 | **Focus:** Concept Mastery, No-Code Understanding

---

# 📖 TABLE OF CONTENTS

1. [Weekly Overview](#weekly-overview)
2. [Day 1: Greedy Fundamentals](#day-1-greedy-fundamentals)
3. [Day 2: Activity Selection & Interval Problems](#day-2-activity-selection--interval-problems)
4. [Day 3: Huffman Coding & Optimal Trees](#day-3-huffman-coding--optimal-trees)
5. [Day 4: Fractional Knapsack & Scheduling](#day-4-fractional-knapsack--scheduling)
6. [Day 5: Greedy Systems & Real-World Applications](#day-5-greedy-systems--real-world-applications)
7. [Assessment & Checkpoints](#assessment--checkpoints)

---

# 📊 WEEKLY OVERVIEW

## What is a Greedy Algorithm?

### Core Philosophy

A **greedy algorithm** is a problem-solving strategy that makes locally optimal choices at each step with the hope of finding a globally optimal solution. The algorithm never reconsiders previous choices.

```
┌─────────────────────────────────────────────────────────────┐
│          GREEDY VS OTHER PARADIGMS                          │
├─────────────────────────────────────────────────────────────┤
│                                                             │
│  DYNAMIC PROGRAMMING:                                       │
│  ┌─────────────────────────────────────────────────────┐    │
│  │ Tries all possibilities → finds optimal             │    │
│  │ Remembers previous decisions                        │    │
│  │ Time: Usually O(n²) or worse                        │    │
│  └─────────────────────────────────────────────────────┘    │
│                                                             │
│  BACKTRACKING:                                              │
│  ┌─────────────────────────────────────────────────────┐    │
│  │ Explores & revises choices                          │    │
│  │ Abandons path when impossible                       │    │
│  │ Time: Exponential worst-case                        │    │
│  └─────────────────────────────────────────────────────┘    │
│                                                             │
│  GREEDY:                                                    │
│  ┌─────────────────────────────────────────────────────┐    │
│  │ Makes best LOCAL choice NOW                         │    │
│  │ Never revises decision                              │    │
│  │ Time: Usually O(n log n)                            │    │
│  └─────────────────────────────────────────────────────┘    │
│                                                             │
│  TRADE-OFF: Speed vs Optimality (must PROVE!)               │
└─────────────────────────────────────────────────────────────┘
```

## The Greedy Guarantee Question

**The Critical Question:** "When does greedy give optimal solution?"

### The Answer Requires Two Properties:

```
┌──────────────────────────────────────────────────────────────┐
│   PROPERTY 1: OPTIMAL SUBSTRUCTURE                           │
├──────────────────────────────────────────────────────────────┤
│                                                              │
│  Definition: Optimal solution contains optimal solutions     │
│  to subproblems                                              │
│                                                              │
│  Example - Activity Selection:                               │
│  ┌────────────────────────────────────────────────────────┐  │
│  │ If optimal solution includes activity i:               │  │
│  │                                                        │  │
│  │    [Activity₁ ... Activityᵢ ... Activityₙ]              |  |
│  │           ↓                    ↓                       │  │
│  │   Before i must be         After i must be             │  │
│  │   optimal for that         optimal for that            │  │
│  │   time interval            time interval               │  │
│  └────────────────────────────────────────────────────────┘  │
│                                                              │
│  This allows us to use INDUCTION in proofs                   │
│                                                              │
└──────────────────────────────────────────────────────────────┘

┌──────────────────────────────────────────────────────────────┐
│   PROPERTY 2: GREEDY CHOICE PROPERTY                         │
├──────────────────────────────────────────────────────────────┤
│                                                              │
│  Definition: Making greedy choice (best locally) leads       │
│  to globally optimal solution                                │
│                                                              │
│  This is what we must PROVE for each problem!                │
│                                                              │
│  Example - Activity Selection:                               │
│  ┌────────────────────────────────────────────────────────┐  │
│  │ Greedy Choice: Pick activity finishing first (min f)   │  │
│  │                                                        │  │
│  │ Why optimal? Leaves maximum room for remaining         │  │
│  │ activities! Cannot do better.                          │  │
│  │                                                        │  │
│  │ Proof idea: If optimal doesn't pick this               │  │
│  │ activity, SWAP IT IN → still valid, same count         │  │
│  └────────────────────────────────────────────────────────┘  │
│                                                              │
└──────────────────────────────────────────────────────────────┘
```

## Weekly Learning Architecture

```
DAY 1: Foundations
  └─ What makes greedy work?
      └─ How to prove greedy is optimal?
          └─ Greedy algorithm template

DAY 2: Applying to Intervals
  └─ Activity selection (greedy DOES work)
      └─ Interval scheduling variations
          └─ Why greedy beats other approaches

DAY 3: Prefix Trees
  └─ Huffman coding introduction
      └─ Why greedy tree construction is optimal
          └─ Proof of Huffman correctness

DAY 4: Resource Allocation
  └─ Fractional knapsack (greedy works!)
      └─ Job scheduling (greedy works!)
          └─ Understanding why 0/1 knapsack is different

DAY 5: Systems & Applications
  └─ MST algorithms (Kruskal, Prim - both greedy!)
      └─ Cache replacement strategies
          └─ Approximation algorithms
```

---

# 📅 DAY 1: GREEDY FUNDAMENTALS

## Time Allocation: 5 hours (300 minutes)

### Segment 1: Greedy Algorithm Concept (60 min)

#### What is "Greedy"?

**Etymology:** The algorithm is "greedy" because it always takes what looks best at the moment without considering future consequences.

**Analogy:**

```
┌─────────────────────────────────────────────────────────┐
│  REAL WORLD ANALOGY: MAKING CHANGE                      │
├─────────────────────────────────────────────────────────┤
│                                                         │
│  Problem: Make $0.41 using fewest coins                 │
│  Available: Quarters, Dimes, Nickels, Pennies           │
│                                                         │
│  GREEDY CHOICE:                                         │
│  ┌──────────────────────────────────────────────────┐   │
│  │ Take 1 Quarter ($0.25)        Remaining: $0.16   │   │
│  │ Take 1 Dime ($0.10)           Remaining: $0.06   │   │
│  │ Take 1 Nickel ($0.05)         Remaining: $0.01   │   │
│  │ Take 1 Penny ($0.01)          Remaining: $0.00   │   │
│  │                                                  │   │
│  │ Total: 4 coins ✓ OPTIMAL!                        │   │
│  └──────────────────────────────────────────────────┘   │
│                                                         │
│  Why greedy works here:                                 │
│  - Each coin is "worth its weight"                      │
│  - Taking maximum value coin leaves                     │
│    smaller optimal subproblem                           │
│                                                         │
└─────────────────────────────────────────────────────────┘
```

#### When Greedy FAILS

**Critical:** Greedy doesn't always work! Must prove for each problem.

```
┌─────────────────────────────────────────────────────────┐
│  COUNTER-EXAMPLE: COIN PROBLEM VARIANT                  │
├─────────────────────────────────────────────────────────┤
│                                                         │
│  Problem: Make $0.30 with fewest coins                  │
│  Coins: {25¢, 10¢, 1¢} (no 5¢!)                         │
│                                                         │
│  GREEDY (WRONG):                                        │
│  ┌──────────────────────────────────────────────────┐   │
│  │ Take 1 Quarter ($0.25)        Remaining: $0.05   │   │
│  │ Take 5 Pennies ($0.05)        Remaining: $0.00   │   │
│  │                                                  │   │
│  │ Total: 6 coins ✗ SUBOPTIMAL!                     │   │
│  └──────────────────────────────────────────────────┘   │
│                                                         │
│  OPTIMAL:                                               │
│  ┌──────────────────────────────────────────────────┐   │
│  │ Take 3 Dimes ($0.30)          Remaining: $0.00   │   │
│  │                                                  │   │
│  │ Total: 3 coins ✓ BETTER!                         │   │
│  └──────────────────────────────────────────────────┘   │
│                                                         │
│  Lesson: Greedy for change works with specific          │
│  coin systems (US, Euro) but not universally!           │
│                                                         │
└─────────────────────────────────────────────────────────┘
```

#### The Greedy Algorithm Template

```
┌──────────────────────────────────────────────────────────────┐
│           GREEDY ALGORITHM GENERAL TEMPLATE                  │
├──────────────────────────────────────────────────────────────┤
│                                                              │
│  1. IDENTIFY CHOICE CRITERION                                │
│     ↓                                                        │
│     What metric makes a choice "best" locally?               │
│     Examples: Maximum profit, minimum cost, earliest time    │
│                                                              │
│  2. SORT/ORGANIZE                                            │
│     ↓                                                        │
│     Order candidates by choice criterion                     │
│     Time: Usually O(n log n)                                 │
│                                                              │
│  3. ITERATE & SELECT                                         │
│     ↓                                                        │
│     Loop through: If choice is valid → Select it             │
│     Time: Usually O(n)                                       │
│                                                              │
│  4. VERIFY CONSTRAINT                                        │
│     ↓                                                        │
│     Check new choice doesn't violate constraints             │
│     Time: Varies by problem                                  │
│                                                              │
│  TOTAL: O(n log n) typically                                 │
│                                                              │
│  KEY: After selecting → problem reduces to same type!        │
│  This enables INDUCTION proofs                               │
│                                                              │
└──────────────────────────────────────────────────────────────┘
```

### Segment 2: When Does Greedy Work? (60 min)

#### Property 1: Optimal Substructure

**Definition:** A problem has optimal substructure if an optimal solution contains optimal solutions to subproblems.

```
┌──────────────────────────────────────────────────────────┐
│  WHAT THIS MEANS                                         │
├──────────────────────────────────────────────────────────┤
│                                                          │
│  If we have optimal solution S for problem P             │
│  Then removing one element from S gives                  │
│  optimal solution for subproblem P'                      │
│                                                          │
│  Visual representation:                                  │
│  ┌────────────────────────────────────────────────────┐  │
│  │ [Optimal Solution for Full Problem]                │  │
│  │                                                    │  │
│  │ ┌──────┐ ┌──────┐ ┌──────┐ ┌──────┐                │  │
│  │ │Elem 1│ │Elem 2│ │Elem 3│ │Elem 4│                │  │
│  │ └──────┘ └──────┘ └──────┘ └──────┘                │  │
│  │    │                                               │  │
│  │    └─────── Remove Element 1                       │  │
│  │                                                    │  │
│  │ ┌──────┐ ┌──────┐ ┌──────┐                         │  │
│  │ │Elem 2│ │Elem 3│ │Elem 4│                         │  │
│  │ └──────┘ └──────┘ └──────┘                         │  │
│  │                                                    │  │
│  │ This is STILL OPTIMAL for subproblem!              │  │
│  │ (If it weren't, we could improve original)         │  │
│  └────────────────────────────────────────────────────┘  │
│                                                          │
│  Why it matters for greedy:                              │
│  → After making greedy choice, we get a subproblem       │
│     of SAME TYPE (same structure, smaller size)          │
│  → Can apply greedy again to subproblem                  │
│  → Enables INDUCTION proof!                              │
│                                                          │
└──────────────────────────────────────────────────────────┘
```

#### Property 2: Greedy Choice Property

**Definition:** Making the greedy choice (locally optimal) is safe and leads to globally optimal solution.

```
┌──────────────────────────────────────────────────────────┐
│  WHAT THIS MEANS                                         │
├──────────────────────────────────────────────────────────┤
│                                                          │
│  The greedy choice must be part of SOME optimal solution │
│                                                          │
│  More specifically:                                      │
│  → Make greedy choice                                    │
│  → Combine with optimal solution to subproblem           │
│  → Get optimal solution to full problem                  │
│                                                          │
│  Visual:                                                 │
│  ┌────────────────────────────────────────────────────┐  │
│  │ Full Problem                                       │  │
│  │ ┌──────────────────────────────────────────────┐   │  │
│  │ │ ┌─────────────────────────────────────────┐  │   │  │
│  │ │ │ Greedy Choice                           │  │   │  │
│  │ │ ┌─────────────────────────────────────────┐  │   │  │
│  │ │ │ Optimal Subproblem Solution             │  │   │  │
│  │ │ └─────────────────────────────────────────┘  │   │  │
│  │ │ = OPTIMAL for Full Problem                   │   │  │
│  │ └──────────────────────────────────────────────┘   │  │
│  └────────────────────────────────────────────────────┘  │
│                                                          │
│  This is the crucial step to PROVE!                      │
│                                                          │
└──────────────────────────────────────────────────────────┘
```

### Segment 3: Exchange Argument Proof Technique (90 min)

#### The Exchange Argument: The Proof Technique for Greedy

**Goal:** Prove that greedy choice property holds

**The Technique:**

```
┌──────────────────────────────────────────────────────────┐
│           EXCHANGE ARGUMENT STEPS                        │
├──────────────────────────────────────────────────────────┤
│                                                          │
│  STEP 1: Assume optimal solution exists                  │
│          and it does NOT contain greedy choice           │
│                                                          │
│  STEP 2: Take the "first" different element              │
│          (call it OPT_elem vs GREEDY_elem)               │
│                                                          │
│  STEP 3: Exchange OPT_elem with GREEDY_elem              │
│          in the optimal solution                         │
│                                                          │
│  STEP 4: Show the new solution is still valid            │
│          (satisfies all constraints)                     │
│                                                          │
│  STEP 5: Show the new solution is at least               │
│          as good as original                             │
│          (same cost, same quality)                       │
│                                                          │
│  STEP 6: By induction, can exchange ALL elements         │
│          → Transform OPT into GREEDY                     │
│                                                          │
│  CONCLUSION: GREEDY must be optimal!                     │
│                                                          │
└──────────────────────────────────────────────────────────┘
```

**Visual Flow:**

```
┌───────────────────────────────────────────────────────────────┐
│                  EXCHANGE ARGUMENT FLOW                       │
├───────────────────────────────────────────────────────────────┤
│                                                               │
│  Assume OPT = [A, B, C, D, E] where A ≠ GREEDY₁               │
│              GREEDY = [G₁, G₂, G₃, G₄, G₅]                    │
│                                                               │
│  ITERATION 1:                                                 │
│  ┌──────────────────────────────────────────────────────────┐ │
│  │ Exchange A ← G₁                                          │ │
│  │ OPT' = [G₁, B, C, D, E]                                  │ │
│  │ Cost(OPT') ≥ Cost(OPT)  [Show this!]                     │ │
│  └──────────────────────────────────────────────────────────┘ │
│                                                               │
│  ITERATION 2:                                                 │
│  ┌──────────────────────────────────────────────────────────┐ │
│  │ Exchange B ← G₂                                          │ │
│  │ OPT'' = [G₁, G₂, C, D, E]                                │ │
│  │ Cost(OPT'') ≥ Cost(OPT')  [Show this!]                   │ │
│  └──────────────────────────────────────────────────────────┘ │
│                                                               │
│  ... continue for all elements ...                            │
│                                                               │
│  FINAL:                                                       │
│  ┌──────────────────────────────────────────────────────────┐ │
│  │ OPT* = [G₁, G₂, G₃, G₄, G₅] = GREEDY                     │ │
│  │ Cost(OPT*) ≥ Cost(OPT)  [from chain of steps]            │ │
│  │                                                          │ │
│  │ Therefore: GREEDY is optimal! ✓                          │ │
│  └──────────────────────────────────────────────────────────┘ │
│                                                               │
└───────────────────────────────────────────────────────────────┘
```

#### Why Exchange Argument Works

```
┌────────────────────────────────────────────────────────────┐
│  THE LOGIC CHAIN                                           │
├────────────────────────────────────────────────────────────┤
│                                                            │
│  1. Assume: OPT is optimal, but different from GREEDY      │
│                                                            │
│  2. Exchange one element: OPT' still valid (good as OPT)   │
│     Why? We chose G₁ because it satisfies all constraints  │
│     BETTER than A while keeping everything else OK         │
│                                                            │
│  3. Repeat: OPT'' still valid (good as OPT')               │
│     Same reasoning                                         │
│                                                            │
│  4. After n exchanges: GREEDY is valid and good as OPT     │
│     But GREEDY was just GREEDY from the start!             │
│                                                            │
│  5. Therefore: GREEDY = OPT (both optimal)                 │
│                                                            │
└────────────────────────────────────────────────────────────┘
```

### Segment 4: Correctness Proof Strategy (60 min)

#### General Strategy for Proving Greedy is Optimal

```
┌────────────────────────────────────────────────────────────┐
│           PROOF STRUCTURE FOR ANY GREEDY ALGORITHM         │
├────────────────────────────────────────────────────────────┤
│                                                            │
│  PART 1: PROVE GREEDY CHOICE PROPERTY                      │
│  ═════════════════════════════════════════                 │
│  "Making this greedy choice leads to global optimal"       │
│                                                            │
│  Technique: Exchange argument or direct proof              │
│  └─ Show any optimal solution with different choice        │
│  └─ Can be swapped to include greedy choice                │
│  └─ Without losing optimality                              │
│                                                            │
│  ─────────────────────────────────────────────             │
│                                                            │
│  PART 2: PROVE OPTIMAL SUBSTRUCTURE                        │
│  ═══════════════════════════════════                       │
│  "After greedy choice, subproblem is similar type"         │
│                                                            │
│  Technique: Show subproblem is identical structure         │
│  └─ Same constraints                                       │
│  └─ Same optimization goal                                 │
│  └─ Just smaller input                                     │
│                                                            │
│  ─────────────────────────────────────────────             │
│                                                            │
│  PART 3: CONCLUDE WITH INDUCTION                           │
│  ═════════════════════════════════                         │
│  "Therefore greedy is always optimal"                      │
│                                                            │
│  Technique: Proof by strong induction                      │
│  └─ Base case: n=1, greedy is trivially optimal            │
│  └─ Inductive step: If greedy optimal for n-1,             │
│     then optimal for n (from parts 1+2)                    │
│                                                            │
└────────────────────────────────────────────────────────────┘
```

#### Example: Simple Proof Template

```
┌────────────────────────────────────────────────────────────┐
│           PROOF TEMPLATE (FILL IN BLANKS)                  │
├────────────────────────────────────────────────────────────┤
│                                                            │
│  THEOREM: Greedy algorithm solves [PROBLEM NAME] optimally │
│                                                            │
│  PROOF:                                                    │
│                                                            │
│  1. GREEDY CHOICE PROPERTY:                                │
│     ─────────────────────────────                          │
│     Claim: [DESCRIBE GREEDY CHOICE] is safe                │
│                                                            │
│     Proof: Let OPT be an optimal solution.                 │
│             If OPT ≠ GREEDY choice:                        │
│             ├─ [DESCRIBE EXCHANGE]                         │
│             ├─ New solution still valid because [REASON]   │
│             └─ Cost ≥ OPT because [REASON]                 │
│                                                            │
│  2. OPTIMAL SUBSTRUCTURE:                                  │
│     ────────────────────────                               │
│     After greedy choice → Problem becomes [DESCRIBE]       │
│     This is [SAME STRUCTURE] with [SMALLER INPUT]          │
│                                                            │
│  3. INDUCTION:                                             │
│     ───────────                                            │
│     Base: n=1, trivial                                     │
│     Step: n-1 → n by (1) and (2)                           │
│     ∴ Greedy is optimal ✓                                  │
│                                                            │
└────────────────────────────────────────────────────────────┘
```

### Segment 5: Summary & Checkpoint (30 min)

#### Key Concepts Day 1

```
┌────────────────────────────────────────────────────────────┐
│           DAY 1 CONCEPTUAL SUMMARY                         │
├────────────────────────────────────────────────────────────┤
│                                                                │
│  ✓ Greedy = make best local choice at each step             │
│  ✓ Greedy NOT always optimal (must prove!)                 │
│  ✓ Two properties needed for greedy to work:                │
│    1. Optimal substructure (problem reduces to same type)   │
│    2. Greedy choice property (locally optimal → globally)   │
│                                                              │
│  ✓ Exchange argument is primary proof technique             │
│    - Assume optimal exists ≠ greedy                         │
│    - Swap elements one by one                               │
│    - Show still valid & cost ≥ original                     │
│    - Induction finishes proof                               │
│                                                              │
│  ✓ Template works for all greedy problems                   │
│    1. Sort by criterion                                     │
│    2. Iterate & greedily select                             │
│    3. Verify constraints                                    │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

#### Checkpoint Questions

1. **Why can't we always use greedy?**
    - Because greedy choice may not have optimal substructure
    - Or greedy choice property may not hold
    - Must prove both properties for each problem

2. **What does "optimal substructure" mean?**
    - After removing greedy choice, remainder is still optimal
    - Enables induction proof
    - Allows recursive reduction

3. **How do we prove greedy choice property?**
    - Exchange argument: show any optimal can be transformed to use greedy choice without losing optimality

4. **When is greedy fast?**
    - Usually O(n log n): sorting + O(n) selection
    - Much faster than DP or backtracking

---

# 📅 DAY 2: ACTIVITY SELECTION & INTERVAL PROBLEMS

## Time Allocation: 5 hours (300 minutes)

### Segment 1: Activity Selection Problem (90 min)

#### Problem Statement

```
┌────────────────────────────────────────────────────────────┐
│           ACTIVITY SELECTION PROBLEM                        │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  INPUT:                                                      │
│  ──────                                                      │
│  Set of n activities, each with:                            │
│  • sᵢ = start time                                          │
│  • fᵢ = finish time                                         │
│                                                              │
│  CONSTRAINT:                                                 │
│  ───────────                                                 │
│  Activities are compatible if sⱼ ≥ fᵢ (no overlap)         │
│  (Activity can start exactly when another ends)             │
│                                                              │
│  GOAL:                                                       │
│  ────                                                        │
│  Select MAXIMUM number of mutually compatible activities    │
│                                                              │
│  Example:                                                    │
│  ┌─────────────────────────────────────────────────────┐    │
│  │ Activities (start, finish):                         │    │
│  │ A₁: (1, 4)    A₂: (3, 5)    A₃: (0, 6)             │    │
│  │ A₄: (5, 7)    A₅: (3, 8)    A₆: (5, 9)             │    │
│  │ A₇: (6, 10)   A₈: (8, 11)   A₉: (2, 13)            │    │
│  │                                                     │    │
│  │ MAXIMUM set: {A₁, A₄, A₈}                          │    │
│  │ Count: 3 activities ✓                              │    │
│  │                                                     │    │
│  │ Why not others?                                     │    │
│  │ {A₁, A₂, A₄, A₈} - NO! A₁ (1,4) and A₂ (3,5)       │    │
│  │                   overlap (3 is in both)            │    │
│  │                                                     │    │
│  │ {A₃, A₄, A₈} - only 3 (same as above)              │    │
│  │                                                     │    │
│  └─────────────────────────────────────────────────────┘    │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

#### Visual Timeline Representation

```
┌────────────────────────────────────────────────────────────┐
│           TIMELINE VISUALIZATION                            │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  Time: 0   1   2   3   4   5   6   7   8   9  10  11  12  13
│        ├───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┼───┤
│                                                              │
│  A₁:   └───────[=========]                                  │ (1,4)
│  A₂:       └───────[=========]                              │ (3,5)
│  A₃:   └───────────────────[===============]                │ (0,6)
│  A₄:                       └───────[===]                    │ (5,7)
│  A₅:       └───────[==================]                     │ (3,8)
│  A₆:                       └───────[===]                    │ (5,9)
│  A₇:                       └───────[======]                 │ (6,10)
│  A₈:                               └───────[====]           │ (8,11)
│  A₉:   └───[=========================]                      │ (2,13)
│                                                              │
│  COMPATIBLE PAIRS:                                           │
│  ┌──────────────────────────────────────────────────────┐   │
│  │ A₁ (end 4) → A₄ (start 5) ✓ Compatible             │   │
│  │ A₁ (end 4) → A₂ (start 3) ✗ Overlaps                │   │
│  │ A₄ (end 7) → A₈ (start 8) ✓ Compatible             │   │
│  └──────────────────────────────────────────────────────┘   │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

#### Greedy Choice: Why Pick "Earliest Finish"?

```
┌────────────────────────────────────────────────────────────┐
│        WHY GREEDY BY FINISH TIME?                           │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  INTUITION:                                                  │
│  ──────────                                                  │
│  By finishing earliest, we leave maximum time for           │
│  remaining activities!                                      │
│                                                              │
│  VISUAL PROOF:                                               │
│  ─────────────                                               │
│  Suppose we pick activity that finishes LATER:              │
│                                                              │
│  ┌─────────────────────────────────────────────────────┐    │
│  │ Option A: Pick activity finishing late              │    │
│  │                                                     │    │
│  │ Time: 0                                         20  │    │
│  │       ├─────────────────────────────────────────┤  │    │
│  │       │ [===CHOSEN ACTIVITY===]                 │  │    │
│  │       0                            15           20 │    │
│  │                                    ↓              │    │
│  │              WASTED SPACE: Could fit 2 activities │    │
│  │                                                     │    │
│  └─────────────────────────────────────────────────────┘    │
│                                                              │
│  ┌─────────────────────────────────────────────────────┐    │
│  │ Option B: Pick activity finishing EARLY             │    │
│  │                                                     │    │
│  │ Time: 0                                         20  │    │
│  │       ├─────────────────────────────────────────┤  │    │
│  │       │ [=ACT1=] [=ACT2=]    [=ACT3=]         │  │    │
│  │       0  3      5  8         15  18     20    │    │
│  │                                                     │    │
│  │              MORE ACTIVITIES FIT!                  │    │
│  │                                                     │    │
│  └─────────────────────────────────────────────────────┘    │
│                                                              │
│  CONCLUSION: Earliest finish leaves most room for next!    │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

### Segment 2: "Greedy Stays Ahead" Proof Technique (90 min)

#### The Technique

```
┌────────────────────────────────────────────────────────────┐
│      "GREEDY STAYS AHEAD" PROOF TECHNIQUE                   │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  CONCEPT:                                                    │
│  ────────                                                    │
│  Greedy solution finishes activities BEFORE or EQUAL to     │
│  any other solution.                                        │
│                                                              │
│  This means: Greedy can always accommodate what others      │
│              can accommodate, PLUS potentially more!        │
│                                                              │
│  Therefore: Greedy is optimal                               │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

#### Formal "Stays Ahead" Argument

```
┌────────────────────────────────────────────────────────────┐
│          STAYS AHEAD FORMALIZED                             │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  Let GREEDY = {g₁, g₂, g₃, ...}  (sorted by finish time)   │
│  Let ANY_OPT = {a₁, a₂, a₃, ...} (arbitrary optimal)        │
│                                                              │
│  CLAIM: f(gᵢ) ≤ f(aᵢ) for all i                            │
│  (Greedy's i-th activity finishes ≤ than any other's i-th)  │
│                                                              │
│  PROOF by INDUCTION:                                         │
│  ──────────────────                                          │
│                                                              │
│  Base case (i=1):                                            │
│  ┌────────────────────────────────────────────────────────┐ │
│  │ g₁ = activity with earliest finish time              │ │
│  │ a₁ = first activity in ANY_OPT                        │ │
│  │                                                        │ │
│  │ By definition: f(g₁) ≤ f(a₁) ✓                        │ │
│  │                                                        │ │
│  │ (because g₁ is earliest finishing among ALL)          │ │
│  └────────────────────────────────────────────────────────┘ │
│                                                              │
│  Inductive step (assume f(gᵢ) ≤ f(aᵢ)):                     │
│  ┌────────────────────────────────────────────────────────┐ │
│  │ After using gᵢ and aᵢ:                                │ │
│  │ Remaining candidates for gᵢ₊₁:                         │ │
│  │   • Activities starting ≥ f(gᵢ)                       │ │
│  │                                                        │ │
│  │ Remaining candidates for aᵢ₊₁:                         │ │
│  │   • Activities starting ≥ f(aᵢ)                       │ │
│  │                                                        │ │
│  │ Since f(gᵢ) ≤ f(aᵢ):                                  │ │
│  │ Remaining for g includes ALL remaining for a!         │ │
│  │                                                        │ │
│  │ So gᵢ₊₁ (earliest from greedy's pool)                 │ │
│  │    ≤ aᵢ₊₁ (earliest from ANY_OPT's pool)              │ │
│  │                                                        │ │
│  │ Therefore: f(gᵢ₊₁) ≤ f(aᵢ₊₁) ✓                        │ │
│  └────────────────────────────────────────────────────────┘ │
│                                                              │
│  CONCLUSION:                                                 │
│  ───────────                                                 │
│  Greedy stays ahead at every step!                           │
│                                                              │
│  If |GREEDY| < |ANY_OPT|, greedy could add one more         │
│  (contradiction that ANY_OPT is optimal)                    │
│                                                              │
│  Therefore: |GREEDY| = |ANY_OPT| (both maximal) ✓           │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

#### Visual "Stays Ahead" Illustration

```
┌────────────────────────────────────────────────────────────┐
│         VISUAL: HOW GREEDY STAYS AHEAD                      │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  GREEDY SOLUTION:                                            │
│  ┌────────────────────────────────────────────────────────┐ │
│  │ Time: 0  1  2  3  4  5  6  7  8  9 10 11 12           │ │
│  │       ├──┼──┼──┼──┼──┼──┼──┼──┼──┼──┼──┼──┤           │ │
│  │       │ [=g₁=] [=g₂=]    [=g₃=] [g₄] │                │ │
│  │       │  0  3   4  7      8  11  12-13│                │ │
│  │       │                              │                │ │
│  │       │ Finishes: g₁@3, g₂@7, g₃@11, g₄@13            │ │
│  │       └────────────────────────────────────────────────┘ │
│  │                                                              │
│  │ ANY OPTIMAL SOLUTION:                                       │
│  │ ┌────────────────────────────────────────────────────────┐ │
│  │ │ Time: 0  1  2  3  4  5  6  7  8  9 10 11 12           │ │
│  │ │       ├──┼──┼──┼──┼──┼──┼──┼──┼──┼──┼──┼──┤           │ │
│  │ │       │ [===a₁===] [===a₂===]    [a₃]    │            │ │
│  │ │       │  0    5     6     10      11-13   │            │ │
│  │ │       │                                  │            │ │
│  │ │       │ Finishes: a₁@5, a₂@10, a₃@13     │            │ │
│  │ │       └────────────────────────────────────────────────┘ │
│  │                                                              │
│  │ COMPARISON:                                                 │
│  │ ┌────────────────────────────────────────────────────────┐ │
│  │ │ Position  g_finishes  a_finishes  Comparison          │ │
│  │ │ ───────────────────────────────────────────────────    │ │
│  │ │ 1st       g₁@3        a₁@5        3 < 5 ✓             │ │
│  │ │ 2nd       g₂@7        a₂@10       7 < 10 ✓            │ │
│  │ │ 3rd       g₃@11       a₃@13       11 < 13 ✓           │ │
│  │ │ 4th       g₄@13       -           Greedy gets extra!  │ │
│  │ │                                                        │ │
│  │ │ GREEDY STAYS AHEAD → Can fit more! → More optimal ✓   │ │
│  │ └────────────────────────────────────────────────────────┘ │
│  │                                                              │
│  └────────────────────────────────────────────────────────────┘ │
│                                                                  │
└────────────────────────────────────────────────────────────────┘
```

### Segment 3: Interval Scheduling Variations (60 min)

#### Variation 1: Weighted Activity Selection

```
┌────────────────────────────────────────────────────────────┐
│      VARIATION: WEIGHTED ACTIVITY SELECTION                 │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  CHANGE:                                                     │
│  ──────                                                      │
│  Each activity now has a WEIGHT/VALUE                        │
│  Goal: Maximize TOTAL WEIGHT (not count!)                   │
│                                                              │
│  Example:                                                    │
│  ┌──────────────────────────────────────────────────────┐   │
│  │ Activity  Start  Finish  Weight                       │   │
│  │ ─────────────────────────────────────────            │   │
│  │ A₁        1      4       5                           │   │
│  │ A₂        3      5       6                           │   │
│  │ A₃        0      6       10                          │   │
│  │ A₄        5      7       3                           │   │
│  │ A₅        6      10      8                           │   │
│  │                                                      │   │
│  │ GREEDY BY EARLIEST FINISH:                           │   │
│  │ {A₁(w=5), A₄(w=3), A₅(w=8)} = Total 16             │   │
│  │                                                      │   │
│  │ OPTIMAL:                                              │   │
│  │ {A₃(w=10), A₅(w=8)} = Total 18 ✓ BETTER!             │   │
│  │                                                      │   │
│  │ Lesson: Greedy by finish time FAILS for weighted!    │   │
│  │         Must use DYNAMIC PROGRAMMING instead         │   │
│  │                                                      │   │
│  └──────────────────────────────────────────────────────┘   │
│                                                              │
│  KEY INSIGHT:                                                │
│  ────────────                                                │
│  Different problem → Different greedy choice needed!        │
│  Unweighted: Finish time works                              │
│  Weighted: Finish time FAILS                                │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

#### Variation 2: Minimum Rooms Needed

```
┌────────────────────────────────────────────────────────────┐
│        VARIATION: MINIMUM ROOMS FOR ACTIVITIES              │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  NEW GOAL:                                                   │
│  ────────                                                    │
│  We MUST accept all activities                              │
│  Find MINIMUM number of rooms (parallel slots)              │
│                                                              │
│  Example:                                                    │
│  ┌──────────────────────────────────────────────────────┐   │
│  │ Same activities as before:                            │   │
│  │ A₁: (1, 4)    A₂: (3, 5)    A₃: (0, 6)              │   │
│  │ A₄: (5, 7)    A₅: (3, 8)    A₆: (5, 9)              │   │
│  │ A₇: (6, 10)   A₈: (8, 11)   A₉: (2, 13)             │   │
│  │                                                      │   │
│  │ GREEDY APPROACH: Sweep line algorithm                │   │
│  │ ┌──────────────────────────────────────────────────┐ │   │
│  │ │ 1. Extract all START and END events              │ │   │
│  │ │    START: 1, 3, 0, 5, 3, 5, 6, 8, 2              │ │   │
│  │ │    END:   4, 5, 6, 7, 8, 9, 10, 11, 13          │ │   │
│  │ │                                                   │ │   │
│  │ │ 2. Sort events by time                           │ │   │
│  │ │ 3. Maintain counter of active activities         │ │   │
│  │ │    On START: increment                           │ │   │
│  │ │    On END: decrement                             │ │   │
│  │ │ 4. Max counter = minimum rooms needed             │ │   │
│  │ │                                                   │ │   │
│  │ │ TIME:  0  1  2  3  4  5  6  7  8  9 10 11 12 13 │ │   │
│  │ │       ──┬──┬──┬──┬──┬──┬──┬──┬──┬──┬──┬──┬──┬──┤ │   │
│  │ │ EVENT: S     S  S  E  E  S  E  S  E  S  E  E  E  │ │   │
│  │ │        (A₃)  (A₉) (A₁,A₂)    (A₄) (A₅)  (A₆)    │ │   │
│  │ │ ROOM#: 1     2   3   2      2    3     2    1  │ │   │
│  │ │                                                   │ │   │
│  │ │ Max rooms needed: 3 ✓                            │ │   │
│  │ │                                                   │ │   │
│  │ └──────────────────────────────────────────────────┘ │   │
│  │                                                      │   │
│  └──────────────────────────────────────────────────────┘   │
│                                                              │
│  KEY INSIGHT:                                                │
│  ────────────                                                │
│  Problem changed → Solution strategy changed!               │
│  Not selecting anymore, but scheduling                      │
│  Uses sweep line instead of greedy select                   │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

### Segment 4: Summary & Practice Problems (60 min)

#### Day 2 Summary

```
┌────────────────────────────────────────────────────────────┐
│          DAY 2 CONCEPTUAL SUMMARY                           │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  ACTIVITY SELECTION:                                         │
│  ──────────────────                                          │
│  ✓ Problem: Select max non-overlapping activities           │
│  ✓ Greedy choice: Pick earliest finish time                │
│  ✓ Why it works: Leaves maximum room for remaining          │
│  ✓ Proof technique: "Greedy stays ahead"                    │
│    - Greedy finishes before any other solution              │
│    - Therefore can always fit more or equal                 │
│    - Therefore must be optimal                              │
│                                                              │
│  KEY VARIATIONS:                                             │
│  ───────────────                                             │
│  ✓ Weighted activities: Greedy fails, need DP               │
│    → Different problem needs different solution             │
│    → Can't just apply same greedy always                    │
│                                                              │
│  ✓ Minimum rooms: Use sweep line, not selection             │
│    → Problem structure determines approach                   │
│    → Same input, different goal = different algorithm       │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

---

# 📅 DAY 3: HUFFMAN CODING & OPTIMAL TREES

## Time Allocation: 5 hours (300 minutes)

### Segment 1: Prefix Codes & Optimal Trees (75 min)

#### Problem Setup

```
┌────────────────────────────────────────────────────────────┐
│          PREFIX CODE PROBLEM SETUP                          │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  GOAL: Encode characters with binary strings                │
│        Minimize AVERAGE codeword length                      │
│        Based on character frequencies                       │
│                                                              │
│  CONSTRAINT: Prefix-free codes                              │
│              No codeword is prefix of another               │
│              Ensures unique decoding                        │
│                                                              │
│  Example:                                                    │
│  ┌──────────────────────────────────────────────────────┐   │
│  │ Text: "BANANA"                                        │   │
│  │ Character frequencies:                                │   │
│  │   B: 1 time                                            │   │
│  │   A: 3 times                                           │   │
│  │   N: 2 times                                           │   │
│  │                                                        │   │
│  │ NAIVE FIXED-LENGTH CODE:                               │   │
│  │   A = 00  (3 chars)                                   │   │
│  │   B = 01  (1 char)                                    │   │
│  │   N = 10  (2 chars)                                   │   │
│  │                                                        │   │
│  │   Total bits: 3*2 + 1*2 + 2*2 = 12 bits              │   │
│  │                                                        │   │
│  │ HUFFMAN VARIABLE-LENGTH CODE:                          │   │
│  │   A = 0   (3 chars, frequent)                         │   │
│  │   N = 10  (2 chars, medium)                           │   │
│  │   B = 11  (1 char, rare)                              │   │
│  │                                                        │   │
│  │   Total bits: 3*1 + 2*2 + 1*2 = 9 bits ✓ BETTER!     │   │
│  │                                                        │   │
│  │ SAVINGS: 25% reduction!                                │   │
│  │                                                        │   │
│  └──────────────────────────────────────────────────────┘   │
│                                                              │
│  KEY INSIGHT:                                                │
│  ────────────                                                │
│  Frequent chars → shorter codes                             │
│  Rare chars → longer codes                                  │
│  Minimizes average length                                   │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

#### Why Prefix Codes Matter

```
┌────────────────────────────────────────────────────────────┐
│       PREFIX-FREE CODE PROPERTY                             │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  PROBLEM: Decoding ambiguity                                │
│  ────────                                                    │
│  Message: "0011010"                                          │
│                                                              │
│  ✗ BAD CODE (NOT prefix-free):                              │
│    A = 0, B = 01, C = 011                                   │
│                                                              │
│    Decoding ambiguous:                                       │
│    • Read "0" = A, continue: "011010"?                      │
│    • Read "01" = B, continue: "1010"?                       │
│    CANNOT DECIDE!                                            │
│                                                              │
│  ✓ GOOD CODE (prefix-free):                                 │
│    A = 0, B = 10, C = 11                                    │
│                                                              │
│    Decoding unambiguous:                                     │
│    • See "0" → must be A (not "00", "01" prefix)           │
│    • See "1" → check next: "10" = B or "11" = C            │
│    "0-11-0-10" = A-C-A-B ✓ UNIQUE!                         │
│                                                              │
│  WHY PREFIX-FREE WORKS:                                      │
│  ─────────────────────                                       │
│  When we see a complete code, we KNOW it's complete         │
│  No need to look ahead                                      │
│  Can decode immediately, left-to-right                      │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

#### Representation as Binary Tree

```
┌────────────────────────────────────────────────────────────┐
│    BINARY TREE REPRESENTATION OF CODES                      │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  CODE: A=0, B=10, C=11                                      │
│                                                              │
│  TREE:                                                       │
│                                                              │
│              ┌─────┐                                         │
│              │Root │                                         │
│              └──┬──┘                                         │
│                 │                                            │
│          ┌──────┴──────┐                                     │
│          │             │                                     │
│          0             1                                     │
│          │             │                                     │
│        ┌─┴─┐        ┌──┴──┐                                  │
│        │ A │        │     │                                  │
│        └───┘        │     │                                  │
│                     0     1                                  │
│                     │     │                                  │
│                   ┌─┴─┐ ┌─┴─┐                                │
│                   │ B │ │ C │                                │
│                   └───┘ └───┘                                │
│                                                              │
│  KEY INSIGHT:                                                │
│  ────────────                                                │
│  • Every character is a LEAF node                            │
│  • Path from root = codeword                                 │
│  • Left = 0, Right = 1                                       │
│  • Prefix-free automatically satisfied:                      │
│    If A is leaf, no other code starts with path to A        │
│                                                              │
│  • Depth of leaf = codeword length                           │
│  • Shallow leaves = frequent chars                           │
│  • Deep leaves = rare chars                                  │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

### Segment 2: Huffman Algorithm (90 min)

#### The Algorithm

```
┌────────────────────────────────────────────────────────────┐
│           HUFFMAN ALGORITHM STEPS                           │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  1. START WITH LEAVES                                        │
│     Create node for each character                           │
│     Weight = frequency of character                          │
│                                                              │
│  2. REPEATEDLY COMBINE TWO MINIMUM TREES                     │
│     While > 1 tree remains:                                 │
│     a) Select two trees with smallest total weight          │
│     b) Create new parent node                                │
│     c) Weight of parent = sum of children weights            │
│     d) Add to pool                                           │
│                                                              │
│  3. FINAL TREE                                               │
│     When one tree remains → Huffman tree complete           │
│                                                              │
│  KEY PROPERTY:                                               │
│  ─────────────                                               │
│  Greedy choice: Always combine SMALLEST frequencies!        │
│  Why? Puts large-weight subtrees closer to root             │
│  → They have shorter depth → shorter codewords              │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

#### Detailed Example

```
┌────────────────────────────────────────────────────────────┐
│         HUFFMAN CODING EXAMPLE: "MISSISSIPPI"              │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  STEP 0: COUNT FREQUENCIES                                   │
│  ════════════════════════════════                            │
│  M: 1  I: 4  S: 4  P: 2                                      │
│                                                              │
│  ────────────────────────────────────────────────────────   │
│                                                              │
│  STEP 1: CREATE LEAF NODES                                   │
│  ═══════════════════════════                                 │
│                                                              │
│    [M:1]  [P:2]  [I:4]  [S:4]                               │
│                                                              │
│  ────────────────────────────────────────────────────────   │
│                                                              │
│  STEP 2: COMBINE TWO SMALLEST (M:1 and P:2)                │
│  ═══════════════════════════════════════════                │
│                                                              │
│           ┌──────┐                                           │
│          [3]     [I:4]  [S:4]                               │
│         /   \                                                │
│      [M:1] [P:2]                                            │
│                                                              │
│  ────────────────────────────────────────────────────────   │
│                                                              │
│  STEP 3: COMBINE NEXT TWO SMALLEST ([3] and [I:4])          │
│  ═══════════════════════════════════════════════            │
│                                                              │
│           ┌──────┐                                           │
│          [7]     [S:4]                                      │
│         /   \                                                │
│      [3]    [I:4]                                            │
│     /   \                                                    │
│  [M:1] [P:2]                                                │
│                                                              │
│  ────────────────────────────────────────────────────────   │
│                                                              │
│  STEP 4: COMBINE REMAINING TWO ([7] and [S:4])              │
│  ═════════════════════════════════════════════              │
│                                                              │
│            ┌────────┐                                        │
│           [11]                                               │
│          /      \                                            │
│        [7]      [S:4]                                        │
│       /   \                                                  │
│     [3]   [I:4]                                              │
│    /   \                                                     │
│ [M:1] [P:2]                                                 │
│                                                              │
│  ────────────────────────────────────────────────────────   │
│                                                              │
│  STEP 5: ASSIGN CODES (left=0, right=1)                     │
│  ═════════════════════════════════════════                  │
│                                                              │
│              [11]                                            │
│            /      \                                          │
│           /        \  1                                      │
│          0          S:4 = "1"                               │
│         /     \                                              │
│        /       \                                             │
│      [7]      [I:4]                                          │
│     /   \         = "01"                                     │
│    /     \                                                   │
│  [3]    [I:4]                                                │
│  /  \ 1   = "01"                                             │
│ /    \                                                       │
│0      1                                                      │
│/      \                                                      │
│M      P                                                      │
│= "000" = "001"                                              │
│                                                              │
│  ────────────────────────────────────────────────────────   │
│                                                              │
│  FINAL CODES:                                                │
│  ═════════════                                               │
│  M = 000 (depth 3, rare)                                     │
│  P = 001 (depth 3, rare)                                     │
│  I = 01  (depth 2, common)                                   │
│  S = 1   (depth 1, common)                                   │
│                                                              │
│  ENCODING "MISSISSIPPI":                                     │
│  ═══════════════════════                                     │
│  M I S S I S S I P P I                                       │
│  000-01-1-1-01-1-1-01-001-001-01                            │
│                                                              │
│  Length: 3+2+1+1+2+1+1+2+3+3+2 = 21 bits                     │
│                                                              │
│  SAVINGS vs FIXED-LENGTH:                                    │
│  ════════════════════════════                                │
│  Fixed (2 bits per char): 11 * 2 = 22 bits                  │
│  Huffman: 21 bits                                            │
│  Savings: 1 bit ≈ 4.5% ✓                                    │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

### Segment 3: Proof of Huffman Optimality (75 min)

#### Why Huffman is Optimal: Greedy Choice Property

```
┌────────────────────────────────────────────────────────────┐
│      HUFFMAN OPTIMALITY PROOF                               │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  THEOREM: Huffman algorithm produces optimal prefix code   │
│                                                              │
│  PROOF STRATEGY:                                             │
│  ───────────────                                             │
│  Exchange argument: Show combining minimum-weight nodes     │
│  is always safe and leads to optimal solution               │
│                                                              │
│  ─────────────────────────────────────────────────────────  │
│                                                              │
│  KEY LEMMA 1: Combining Two Smallest is Safe               │
│  ════════════════════════════════════════════               │
│                                                              │
│  Claim: In optimal tree, two minimum-weight leaves can be   │
│         combined into one internal node (not separately)    │
│                                                              │
│  Proof:                                                      │
│  ┌─────────────────────────────────────────────────────┐   │
│  │ Let a, b = two chars with smallest frequencies      │   │
│  │ Assume optimal tree T does NOT combine them         │   │
│  │                                                     │   │
│  │ In tree T:                                          │   │
│  │ • a is at depth dₐ                                  │   │
│  │ • b is at depth d_b                                 │   │
│  │ • Some other pair (c, d) has parents p_cd            │   │
│  │                                                     │   │
│  │ Cost(T) = Σ freq(x) * depth(x)                      │   │
│  │         = ... + freq(a)*dₐ + freq(b)*d_b + ...     │   │
│  │         + freq(c)*d_c + freq(d)*d_d + ...         │   │
│  │                                                     │   │
│  │ Now swap positions: (a replaces c, b replaces d)   │   │
│  │                                                     │   │
│  │ Cost(T') = ... + freq(a)*d_c + freq(b)*d_d + ...  │   │
│  │           + freq(c)*dₐ + freq(d)*d_b + ...        │   │
│  │                                                     │   │
│  │ Difference:                                         │   │
│  │ Cost(T') - Cost(T)                                  │   │
│  │ = freq(a)*(d_c-dₐ) + freq(b)*(d_d-d_b)            │   │
│  │ + freq(c)*(dₐ-d_c) + freq(d)*(d_b-d_d)            │   │
│  │                                                     │   │
│  │ = (freq(a)-freq(c))*(d_c-dₐ)                        │   │
│  │ + (freq(b)-freq(d))*(d_d-d_b)                       │   │
│  │                                                     │   │
│  │ Since a,b are smallest: freq(a) ≤ freq(c)          │   │
│  │                          freq(b) ≤ freq(d)          │   │
│  │                                                     │   │
│  │ So: (freq(a)-freq(c)) ≤ 0                           │   │
│  │     (freq(b)-freq(d)) ≤ 0                           │   │
│  │                                                     │   │
│  │ If d_c > dₐ and d_d > d_b:                          │   │
│  │ Then Cost(T') ≤ Cost(T) ✓                           │   │
│  │                                                     │   │
│  │ Therefore: Can SWAP without losing optimality       │   │
│  │            Two smallest CAN be combined safely!     │   │
│  │                                                     │   │
│  └─────────────────────────────────────────────────────┘   │
│                                                              │
│  ─────────────────────────────────────────────────────────  │
│                                                              │
│  KEY LEMMA 2: If Subproblem Optimal, Tree is Optimal       │
│  ══════════════════════════════════════════════════════     │
│                                                              │
│  After combining two minimum nodes:                         │
│  • New problem = same structure (still prefix tree coding)  │
│  • One less character (combined node)                       │
│  • If we solve this optimally → can reconstruct optimal    │
│    solution to original                                     │
│                                                              │
│  ─────────────────────────────────────────────────────────  │
│                                                              │
│  INDUCTION:                                                  │
│  ═════════                                                   │
│  Base: n=1 character → tree is single node, trivially      │
│        optimal                                              │
│                                                              │
│  Step: Assume optimal for n-1 characters                    │
│        For n characters:                                    │
│        1. Combine two smallest (Lemma 1)                    │
│        2. Solve n-1 problem optimally (IH)                  │
│        3. Expand combined node back                         │
│        4. Result is optimal for n ✓                         │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

### Segment 4: Summary & Applications (60 min)

#### Why Huffman Matters

```
┌────────────────────────────────────────────────────────────┐
│        HUFFMAN IN REAL WORLD                                │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  Data Compression Formats:                                   │
│  • GZIP, ZIP: Uses Huffman + other techniques               │
│  • JPEG: Huffman for entropy coding                         │
│  • MP3: Huffman for encoding audio data                     │
│  • PNG: Huffman coding in deflate                           │
│                                                              │
│  Typical Savings:                                            │
│  • English text: 5-10% reduction                             │
│  • Binary data: 20-40% reduction (highly skewed freq)       │
│  • Already compressed: <1% (data already optimized)         │
│                                                              │
│  Key Advantage:                                              │
│  • Simple to implement                                       │
│  • Fast to decode (single pass, left-to-right)             │
│  • Provably optimal                                         │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

#### Day 3 Summary

```
┌────────────────────────────────────────────────────────────┐
│          DAY 3 CONCEPTUAL SUMMARY                           │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  PREFIX CODES:                                               │
│  ─────────────                                               │
│  ✓ Unique decoding without delimiters                       │
│  ✓ Represented as binary trees                              │
│  ✓ Depth = codeword length                                  │
│                                                              │
│  HUFFMAN ALGORITHM:                                          │
│  ────────────────                                            │
│  ✓ Greedy: Combine two minimum-weight trees                 │
│  ✓ Build bottom-up                                          │
│  ✓ Assign codes based on tree structure                     │
│                                                              │
│  OPTIMALITY PROOF:                                           │
│  ────────────────                                            │
│  ✓ Exchange argument: Combining minimums is safe            │
│  ✓ Induction: If subproblem optimal, full tree optimal      │
│  ✓ Result: Huffman produces minimum-cost prefix code        │
│                                                              │
│  APPLICATIONS:                                               │
│  ────────────                                                │
│  ✓ Data compression (ZIP, GZIP, PNG, JPEG, MP3)            │
│  ✓ Real-world compression: 5-40% savings                    │
│  ✓ Optimal entropy coding                                   │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

---

# 📅 DAY 4: FRACTIONAL KNAPSACK & SCHEDULING

## Time Allocation: 5 hours (300 minutes)

### Segment 1: Fractional Knapsack Problem (90 min)

#### Problem Statement

```
┌────────────────────────────────────────────────────────────┐
│          FRACTIONAL KNAPSACK PROBLEM                        │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  INPUT:                                                      │
│  ──────                                                      │
│  • Knapsack capacity: W                                      │
│  • n items, each with:                                       │
│    - wᵢ = weight                                             │
│    - vᵢ = value                                              │
│                                                              │
│  SPECIAL FEATURE:                                            │
│  ────────────────                                            │
│  Can take FRACTIONS of items                                 │
│  xᵢ ∈ [0, 1] = fraction of item i                           │
│                                                              │
│  CONSTRAINT:                                                 │
│  ───────────                                                 │
│  Total weight ≤ W:   Σ xᵢ * wᵢ ≤ W                          │
│                                                              │
│  GOAL:                                                        │
│  ────                                                        │
│  Maximize total value: Σ xᵢ * vᵢ                            │
│                                                              │
│  Example:                                                    │
│  ┌──────────────────────────────────────────────────────┐   │
│  │ Capacity: W = 10 kg                                  │   │
│  │                                                      │   │
│  │ Items:                                               │   │
│  │ Item   Weight  Value  Value/Weight (ratio)           │   │
│  │ ────   ──────  ─────  ─────────────────              │   │
│  │ 1      5 kg    100    20 ← BEST ratio                │   │
│  │ 2      10 kg   200    20 ← TIE (same ratio)          │   │
│  │ 3      15 kg   300    20 ← TIE (same ratio)          │   │
│  │                                                      │   │
│  │ GREEDY: Take by best value/weight ratio              │   │
│  │ ┌────────────────────────────────────────────────┐   │   │
│  │ │ 1. Take full Item 1: 5 kg, +100 value        │   │   │
│  │ │    Remaining capacity: 5 kg                   │   │   │
│  │ │                                               │   │   │
│  │ │ 2. Take 0.5 × Item 2: 5 kg, +100 value       │   │   │
│  │ │    (0.5 * 10 kg = 5 kg, 0.5 * 200 = 100)    │   │   │
│  │ │    Remaining capacity: 0                      │   │   │
│  │ │                                               │   │   │
│  │ │ Total: 5+5=10 kg, value = 100+100 = 200 ✓   │   │   │
│  │ └────────────────────────────────────────────────┘   │   │
│  │                                                      │   │
│  └──────────────────────────────────────────────────────┘   │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

#### Greedy Strategy: Value/Weight Ratio

```
┌────────────────────────────────────────────────────────────┐
│      WHY VALUE/WEIGHT RATIO IS OPTIMAL                      │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  INTUITION:                                                  │
│  ──────────                                                  │
│  Want maximum VALUE per WEIGHT used                         │
│  So optimize by value-to-weight ratio!                      │
│                                                              │
│  ┌─ Item A: 10 kg, 100 value                              │
│  │ Ratio = 10 value/kg                                     │
│  │                                                          │
│  │ Item B: 10 kg, 200 value                               │
│  │ Ratio = 20 value/kg                                     │
│  │                                                          │
│  │ With 10 kg: Item B is 2x better! (200 vs 100)          │
│  │             Because ratio is 2x better                  │
│  └─ Greedy: Always pick item with best ratio               │
│                                                              │
│  ─────────────────────────────────────────────────────────  │
│                                                              │
│  FORMAL PROOF SKETCH:                                        │
│  ────────────────────                                        │
│                                                              │
│  Theorem: Greedy by value/weight ratio is optimal          │
│                                                              │
│  Proof idea:                                                │
│  Suppose optimal solution OPT doesn't follow greedy order  │
│                                                              │
│  OPT = [... Item A ... Item B ...]                          │
│        where ratio(A) < ratio(B)                            │
│                                                              │
│  But since capacity is continuous and we can take          │
│  fractions:                                                  │
│  SWAP: Replace bit of A with bit of B                       │
│  • Remove ε weight of A: lose ε * ratio(A) value           │
│  • Add ε weight of B: gain ε * ratio(B) value              │
│  • Net change: ε * (ratio(B) - ratio(A)) > 0 ✓             │
│                                                              │
│  Since ratio(B) > ratio(A), swapping IMPROVES solution     │
│  Therefore: OPT must follow greedy order!                  │
│                                                              │
│  ─────────────────────────────────────────────────────────  │
│                                                              │
│  KEY PROPERTY: Fractional items are crucial!                │
│  Without fractions (0/1 knapsack) greedy FAILS             │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

### Segment 2: 0/1 Knapsack vs Fractional (60 min)

#### The Critical Difference

```
┌────────────────────────────────────────────────────────────┐
│     0/1 KNAPSACK: GREEDY FAILS!                             │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  CONSTRAINT CHANGE:                                          │
│  ──────────────────                                          │
│  xᵢ ∈ {0, 1}  (take whole item or nothing)                 │
│  NOT xᵢ ∈ [0, 1]                                             │
│                                                              │
│  COUNTER-EXAMPLE:                                            │
│  ────────────────                                            │
│  Capacity: W = 50                                            │
│                                                              │
│  Items:                                                      │
│  Item   Weight  Value  Ratio                                │
│  ────   ──────  ─────  ─────────                            │
│  A      10      60     6.0  ← BEST ratio                   │
│  B      20      100    5.0                                  │
│  C      30      120    4.0                                  │
│                                                              │
│  GREEDY (by ratio):                                          │
│  ┌──────────────────────────────────────────────────────┐   │
│  │ 1. Take A: 10 kg, 60 value, remaining: 40 kg       │   │
│  │ 2. Take B: 20 kg, 100 value, remaining: 20 kg      │   │
│  │ 3. Cannot take C (need 30 kg, have 20 kg)          │   │
│  │                                                     │   │
│  │ Total: 30 kg, value = 160 ✗ SUBOPTIMAL             │   │
│  └──────────────────────────────────────────────────────┘   │
│                                                              │
│  OPTIMAL:                                                    │
│  ┌──────────────────────────────────────────────────────┐   │
│  │ 1. Take B: 20 kg, 100 value                         │   │
│  │ 2. Take C: 30 kg, 120 value                         │   │
│  │ 3. Cannot fit more (50 kg used)                     │   │
│  │                                                     │   │
│  │ Total: 50 kg, value = 220 ✓ BETTER!                │   │
│  │                                                     │   │
│  │ Why? Because best ratio doesn't matter if you      │   │
│  │      can't subdivide! Must consider combinations   │   │
│  │                                                     │   │
│  └──────────────────────────────────────────────────────┘   │
│                                                              │
│  LESSON:                                                     │
│  ══════                                                      │
│  0/1 knapsack requires DYNAMIC PROGRAMMING                  │
│  Greedy DOES NOT WORK                                       │
│                                                              │
│  The discrete constraint changes everything!                │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

#### Why Fractional Works But 0/1 Doesn't

```
┌────────────────────────────────────────────────────────────┐
│  MATHEMATICAL REASON                                        │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  FRACTIONAL PROBLEM: Linear objective function              │
│  ─────────────────────────────────────────                  │
│  Maximize: Σ xᵢ * vᵢ                                        │
│  Subject to: Σ xᵢ * wᵢ ≤ W,  xᵢ ∈ [0,1]                   │
│                                                              │
│  This is LINEAR PROGRAMMING                                 │
│  → Optimal at EXTREME POINT                                 │
│  → Extreme point: fill greedily by ratio                    │
│  → Greedy works!                                             │
│                                                              │
│  ─────────────────────────────────────────────────────────  │
│                                                              │
│  0/1 KNAPSACK PROBLEM: Integer objective                    │
│  ────────────────────────────────────────────                │
│  Maximize: Σ xᵢ * vᵢ                                        │
│  Subject to: Σ xᵢ * wᵢ ≤ W,  xᵢ ∈ {0,1}                   │
│                                                              │
│  This is INTEGER LINEAR PROGRAMMING (NP-hard!)            │
│  → Optimal NOT at simple extreme point                      │
│  → Must consider all 2ⁿ combinations                        │
│  → Greedy fails!                                             │
│  → DP solves it in O(n*W)                                   │
│                                                              │
│  ─────────────────────────────────────────────────────────  │
│                                                              │
│  KEY INSIGHT:                                                │
│  ────────────                                                │
│  Continuous relaxation → Greedy works                        │
│  Integer constraint → Greedy fails                          │
│                                                              │
│  The presence/absence of ONE WORD ("or nothing")            │
│  changes problem from polynomial to NP-hard!                │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

### Segment 3: Job Scheduling with Deadlines (90 min)

#### Problem Setup

```
┌────────────────────────────────────────────────────────────┐
│     JOB SEQUENCING WITH DEADLINES PROBLEM                   │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  INPUT:                                                      │
│  ──────                                                      │
│  • n jobs, each with:                                        │
│    - deadline dᵢ (time by which must complete)              │
│    - profit pᵢ (earned if completed by deadline)            │
│  • Processing: each job takes 1 unit of time                │
│  • Sequencing: decide order to do jobs                      │
│                                                              │
│  GOAL:                                                        │
│  ────                                                        │
│  Maximize total profit                                       │
│  (jobs finished after deadline = 0 profit)                  │
│                                                              │
│  Example:                                                    │
│  ┌──────────────────────────────────────────────────────┐   │
│  │ Jobs:                                                │   │
│  │ Job   Deadline  Profit                               │   │
│  │ ────  ────────  ──────                               │   │
│  │ A     2         60                                   │   │
│  │ B     1         100                                  │   │
│  │ C     2         40                                   │   │
│  │ D     3         20                                   │   │
│  │                                                      │   │
│  │ Feasible sequences:                                  │   │
│  │ • [B, A, C, D]: Profits 100+60+0+0 = 160            │   │
│  │   Time: B(1), A(2), C(3), D(4)                      │   │
│  │   C finishes at time 3 > deadline 2 ✗               │   │
│  │                                                      │   │
│  │ • [B, A, C]: Profits 100+60+0 = 160                 │   │
│  │   Time: B(1), A(2), C(3)                            │   │
│  │   C misses deadline ✗                                │   │
│  │                                                      │   │
│  │ • [B, C, A]: Profits 100+0+60 = 160                 │   │
│  │   Time: B(1), C(2), A(3)                            │   │
│  │   C meets deadline, A misses ✗                       │   │
│  │                                                      │   │
│  │ • [B, A, D]: Profits 100+60+20 = 180 ✓ BEST!        │   │
│  │   Time: B(1), A(2), D(3)                            │   │
│  │   All meet deadlines ✓                               │   │
│  │                                                      │   │
│  └──────────────────────────────────────────────────────┘   │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

#### Greedy Strategy

```
┌────────────────────────────────────────────────────────────┐
│        GREEDY STRATEGY: PROFIT-FIRST SCHEDULING             │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  ALGORITHM:                                                  │
│  ══════════                                                  │
│  1. Sort jobs by profit (descending)                        │
│  2. For each job:                                            │
│     a) Try to schedule it as late as possible               │
│        (but before its deadline)                            │
│     b) If slot available → schedule it                      │
│     c) Otherwise → skip it                                  │
│                                                              │
│  INTUITION:                                                  │
│  ─────────                                                   │
│  High-profit jobs MUST be done!                             │
│  Leave flexibility (earlier slots) for lower-profit jobs    │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

#### Detailed Scheduling Algorithm

```
┌────────────────────────────────────────────────────────────┐
│      JOB SCHEDULING EXAMPLE STEP-BY-STEP                    │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  STEP 0: SORT BY PROFIT (descending)                        │
│  ═════════════════════════════════════                      │
│  B(profit=100, deadline=1)                                  │
│  A(profit=60, deadline=2)                                   │
│  C(profit=40, deadline=2)                                   │
│  D(profit=20, deadline=3)                                   │
│                                                              │
│  ──────────────────────────────────────────────────────────  │
│                                                              │
│  STEP 1: SCHEDULE B (profit=100, deadline=1)                │
│  ════════════════════════════════════════════                │
│  Latest slot before deadline 1: slot 1                       │
│  Place B in slot 1                                          │
│                                                              │
│  Slots: [1:B] _ _ _ ...                                     │
│                                                              │
│  ──────────────────────────────────────────────────────────  │
│                                                              │
│  STEP 2: SCHEDULE A (profit=60, deadline=2)                 │
│  ════════════════════════════════════════════                │
│  Latest slot before deadline 2: slot 2                       │
│  Slot 2 available? YES                                       │
│  Place A in slot 2                                          │
│                                                              │
│  Slots: [1:B] [2:A] _ _ ...                                 │
│                                                              │
│  ──────────────────────────────────────────────────────────  │
│                                                              │
│  STEP 3: SCHEDULE C (profit=40, deadline=2)                 │
│  ════════════════════════════════════════════                │
│  Latest slot before deadline 2: slot 2                       │
│  Slot 2 occupied by A                                        │
│  Try slot 1: occupied by B                                  │
│  No available slot! SKIP C                                  │
│                                                              │
│  Slots: [1:B] [2:A] _ _ ...                                 │
│                                                              │
│  ──────────────────────────────────────────────────────────  │
│                                                              │
│  STEP 4: SCHEDULE D (profit=20, deadline=3)                 │
│  ════════════════════════════════════════════                │
│  Latest slot before deadline 3: slot 3                       │
│  Slot 3 available? YES                                       │
│  Place D in slot 3                                          │
│                                                              │
│  Slots: [1:B] [2:A] [3:D] ...                               │
│                                                              │
│  ──────────────────────────────────────────────────────────  │
│                                                              │
│  FINAL SOLUTION:                                             │
│  ═══════════════                                             │
│  Schedule: B (time 1), A (time 2), D (time 3)               │
│  Total profit: 100 + 60 + 20 = 180 ✓                        │
│                                                              │
│  Verification:                                               │
│  • B finishes at time 1 ≤ deadline 1 ✓                      │
│  • A finishes at time 2 ≤ deadline 2 ✓                      │
│  • D finishes at time 3 ≤ deadline 3 ✓                      │
│  • All profits earned!                                       │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

#### Why Greedy Works

```
┌────────────────────────────────────────────────────────────┐
│      WHY PROFIT-FIRST GREEDY IS OPTIMAL                     │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  INTUITION:                                                  │
│  ──────────                                                  │
│  We want to fit as much HIGH PROFIT as possible             │
│  Scheduling constraint: must meet deadlines                 │
│                                                              │
│  Key insight: If we can fit k jobs total:                   │
│  Which k jobs maximize profit?                              │
│  Answer: The k highest-profit jobs that CAN be fitted!     │
│                                                              │
│  Greedy does exactly this:                                  │
│  Sort by profit → try to fit highest profit first          │
│  Place each as late as possible → leaves room for others    │
│                                                              │
│  ─────────────────────────────────────────────────────────  │
│                                                              │
│  FORMAL PROOF SKETCH:                                        │
│  ────────────────────                                        │
│                                                              │
│  Exchange argument:                                          │
│  Suppose OPT includes lower-profit job j instead of        │
│  higher-profit job i (from greedy order)                   │
│                                                              │
│  OPT = [... j ... i' ...]                                   │
│        where profit(j) < profit(i) and i' ≠ i              │
│                                                              │
│  If we can SWAP j ← i in OPT:                              │
│  • i can be scheduled at j's slot (or earlier) if j can    │
│    (because i has deadline ≥ j, due to greedy order)      │
│  • New solution has profit(i) instead of profit(j)         │
│  • New solution is at least as good (better!)              │
│                                                              │
│  By repeated swaps: can transform OPT into GREEDY          │
│  Therefore: GREEDY is optimal ✓                            │
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
│  FRACTIONAL KNAPSACK:                                        │
│  ─────────────────                                           │
│  ✓ Can take fractions of items                              │
│  ✓ Greedy by value/weight ratio is optimal                  │
│  ✓ Why: Linear programming problem                          │
│  ✓ Continuous = greedy works                                │
│                                                              │
│  0/1 KNAPSACK:                                               │
│  ────────────                                                │
│  ✓ Cannot subdivide items                                   │
│  ✓ Greedy by ratio FAILS                                    │
│  ✓ Why: Integer programming (NP-hard)                       │
│  ✓ Discrete = greedy doesn't work                           │
│  ✓ Solution: Dynamic programming needed                     │
│                                                              │
│  JOB SCHEDULING:                                             │
│  ───────────────                                             │
│  ✓ Schedule jobs to maximize profit                         │
│  ✓ Each job has deadline                                    │
│  ✓ Greedy: Sort by profit, place jobs late                  │
│  ✓ Why: High-profit jobs must be fit first                  │
│  ✓ Placing late leaves flexibility for others               │
│                                                              │
│  KEY LESSON:                                                 │
│  ───────────                                                 │
│  Problem structure determines greedy choice:                │
│  • Continuous problem → one greedy choice                   │
│  • Discrete problem → different choice or DP                │
│  • Resource allocation → ratio-based                        │
│  • Scheduling → priority-based                              │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

---

# 📅 DAY 5: GREEDY SYSTEMS & REAL-WORLD APPLICATIONS

## Time Allocation: 5 hours (300 minutes)

### Segment 1: Greedy in Minimum Spanning Trees (90 min)

#### Kruskal's Algorithm: Edge-Based Greedy

```
┌────────────────────────────────────────────────────────────┐
│         KRUSKAL'S ALGORITHM (EDGE-GREEDY)                   │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  PROBLEM: Minimum Spanning Tree (MST)                       │
│  Given: Connected weighted undirected graph                 │
│  Goal: Find tree connecting all vertices                    │
│        with MINIMUM total edge weight                       │
│                                                              │
│  KRUSKAL'S GREEDY CHOICE:                                   │
│  ═════════════════════════                                  │
│  1. Sort ALL edges by weight (ascending)                    │
│  2. For each edge (in sorted order):                        │
│     If edge connects two different components:              │
│        Add edge to tree                                     │
│        (Use Union-Find to track components)                 │
│     Else: Skip (would create cycle)                         │
│                                                              │
│  Why greedy works:                                           │
│  • Adding minimum-weight edge safe (can always include it)  │
│  • Never creates cycles (uses Union-Find)                   │
│  • Result connects all vertices = spanning tree             │
│  • Minimum weight (always took minimum available)           │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

#### Kruskal's Example

```
┌────────────────────────────────────────────────────────────┐
│          KRUSKAL'S ALGORITHM EXAMPLE                        │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  GRAPH:                                                      │
│                                                              │
│     4───────3───────2                                        │
│     │\      │      /│                                        │
│     │ \2    │ 7   / │4                                       │
│     │  \    │   /   │                                        │
│     1  5│   │  /1   │                                        │
│     │   \   │/      │                                        │
│     6   1\  │/      │                                        │
│     │      \│      /│                                        │
│     │       1      / 3                                       │
│      \      │    /                                           │
│        \    │  /                                             │
│         \   │/                                               │
│          5─────────                                          │
│             3                                                │
│                                                              │
│  EDGES (sorted by weight):                                   │
│  ════════════════════════════                                │
│  (1,5):1  (2,3):1  (1,2):2  (3,4):3  (1,3):5  (2,4):7      │
│                                                              │
│  ──────────────────────────────────────────────────────────  │
│                                                              │
│  STEP 1: Add (1,5):1 weight                                  │
│  Components: {1,5} {2} {3} {4}                              │
│  Tree edges: 1                                               │
│                                                              │
│      1———5                                                   │
│                                                              │
│  ──────────────────────────────────────────────────────────  │
│                                                              │
│  STEP 2: Add (2,3):1                                         │
│  Components: {1,5} {2,3} {4}                                │
│  Tree edges: 2                                               │
│                                                              │
│      1———5    2———3                                          │
│                                                              │
│  ──────────────────────────────────────────────────────────  │
│                                                              │
│  STEP 3: Add (1,2):2                                         │
│  Connects {1,5} and {2,3}                                    │
│  Components: {1,2,3,5} {4}                                  │
│  Tree edges: 3                                               │
│                                                              │
│      1───2                                                   │
│     /     \                                                  │
│    5       3                                                 │
│                                                              │
│  ──────────────────────────────────────────────────────────  │
│                                                              │
│  STEP 4: Add (3,4):3                                         │
│  Connects {1,2,3,5} and {4}                                  │
│  Components: {1,2,3,4,5}  ← All connected!                  │
│  Tree edges: 4 (complete tree for 5 vertices)               │
│                                                              │
│      1───2                                                   │
│     /     \                                                  │
│    5       3───4                                             │
│                                                              │
│  ──────────────────────────────────────────────────────────  │
│                                                              │
│  FINAL MST:                                                   │
│  ════════════                                                │
│  Edges: (1,5):1 + (2,3):1 + (1,2):2 + (3,4):3              │
│  Total weight: 1+1+2+3 = 7 ✓ MINIMUM!                       │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

#### Prim's Algorithm: Vertex-Based Greedy

```
┌────────────────────────────────────────────────────────────┐
│         PRIM'S ALGORITHM (VERTEX-GREEDY)                    │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  ALTERNATIVE APPROACH to MST:                                │
│  ══════════════════════════                                  │
│  1. Start with any vertex (e.g., vertex 1)                   │
│  2. Maintain frontier = vertices in tree                     │
│  3. Repeatedly:                                              │
│     Find minimum-weight edge from frontier to outside        │
│     Add that edge (and new vertex) to tree                   │
│     Update frontier                                          │
│                                                              │
│  Why greedy works:                                           │
│  • At each step: add minimum-weight edge that grows tree    │
│  • Tree property: adding edge between tree/non-tree safe    │
│  • Eventually connects all vertices                          │
│  • Minimum weight (always took minimum available)            │
│                                                              │
│  COMPARISON:                                                 │
│  ───────────                                                 │
│  Kruskal: Think globally (consider all edges), greedy pick   │
│  Prim: Think locally (grow tree from seed), greedy pick      │
│  Result: Both produce MST! (same weight, possibly same tree) │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

### Segment 2: Cache Replacement Strategies (75 min)

#### LRU Cache: Greedy Temporal Heuristic

```
┌────────────────────────────────────────────────────────────┐
│          LRU CACHE REPLACEMENT STRATEGY                     │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  PROBLEM: Cache of limited size                             │
│  When cache full and new item arrives:                      │
│  Which item to evict?                                       │
│                                                              │
│  LRU GREEDY CHOICE:                                          │
│  ════════════════                                            │
│  Evict the Least Recently Used item                         │
│  (item not accessed for longest time)                       │
│                                                              │
│  INTUITION:                                                  │
│  ─────────                                                   │
│  Items used recently likely to be used again soon           │
│  Items not used recently probably won't be used soon        │
│  So evict recently-unused items                             │
│                                                              │
│  ─────────────────────────────────────────────────────────  │
│                                                              │
│  Example: Cache size = 3                                     │
│  ┌──────────────────────────────────────────────────────┐   │
│  │ Access sequence: A, B, C, D, E, B, F, A, G          │   │
│  │                                                      │   │
│  │ A: Cache [A]                                         │   │
│  │ B: Cache [A, B]                                      │   │
│  │ C: Cache [A, B, C]     (full)                        │   │
│  │ D: Evict A (least recent)                            │   │
│  │    Cache [D, B, C]                                   │   │
│  │ E: Evict C (least recent now)                        │   │
│  │    Cache [D, B, E]                                   │   │
│  │ B: Access B (already in cache)                       │   │
│  │    Cache [D, B, E]  (B marked as recently used)      │   │
│  │ F: Evict D (least recent now)                        │   │
│  │    Cache [F, B, E]                                   │   │
│  │ A: Evict E (least recent)                            │   │
│  │    Cache [F, B, A]                                   │   │
│  │ G: Evict F (least recent)                            │   │
│  │    Cache [G, B, A]                                   │   │
│  │                                                      │   │
│  │ Cache hits: B (step 6), total = 1 hit               │   │
│  │ Cache misses: 8                                      │   │
│  │ Hit rate: 1/9 ≈ 11%                                  │   │
│  │                                                      │   │
│  └──────────────────────────────────────────────────────┘   │
│                                                              │
│  IMPORTANT NOTE:                                             │
│  ─────────────                                               │
│  LRU is heuristic (greedy heuristic)                        │
│  NOT always optimal!                                        │
│  Example: Specific access patterns can defeat LRU           │
│                                                              │
│  But in practice: LRU works well (locality of reference)    │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

#### When LRU Fails

```
┌────────────────────────────────────────────────────────────┐
│       LRU NOT ALWAYS OPTIMAL                                │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  EXAMPLE: Cache size = 2                                     │
│  ┌────────────────────────────────────────────────────────┐ │
│  │ Access sequence: 1, 2, 3, 1, 2, 3, 1, 2, 3, 1, 2, 3  │ │
│  │                                                        │ │
│  │ LRU STRATEGY:                                          │ │
│  │ 1: Cache [1]                                          │ │
│  │ 2: Cache [1, 2]                                       │ │
│  │ 3: Evict 1 (LRU), Cache [3, 2]  → MISS                │ │
│  │ 1: Evict 2 (LRU), Cache [3, 1]  → MISS                │ │
│  │ 2: Evict 3 (LRU), Cache [1, 2]  → MISS                │ │
│  │ 3: Evict 1 (LRU), Cache [2, 3]  → MISS                │ │
│  │ 1: Evict 2 (LRU), Cache [3, 1]  → MISS                │ │
│  │ 2: Evict 3 (LRU), Cache [1, 2]  → MISS                │ │
│  │ ...                                                    │ │
│  │                                                        │ │
│  │ Hit rate: 2/12 = 17%  (first two are hits)            │ │
│  │                                                        │ │
│  │ ─────────────────────────────────────────────────────  │ │
│  │                                                        │ │
│  │ OPTIMAL (FUTURE KNOWLEDGE):                            │ │
│  │ Evict the item needed FURTHEST in future              │ │
│  │ Sequence repeats, so all 3 equally far                │ │
│  │ But cache hits at positions 4,5,6,7... = 9 hits      │ │
│  │                                                        │ │
│  │ Better cache strategy exists!                          │ │
│  │ LRU not optimal, just good heuristic                   │ │
│  │                                                        │ │
│  └────────────────────────────────────────────────────────┘ │
│                                                              │
│  KEY INSIGHT:                                                │
│  ────────────                                                │
│  Greedy heuristics are practical, not always theoretically  │
│  optimal. They work well in practice due to locality        │
│  of reference (real programs reuse recent data).            │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

### Segment 3: Approximation Algorithms (60 min)

#### When Optimal is Too Hard: Approximation

```
┌────────────────────────────────────────────────────────────┐
│       APPROXIMATION ALGORITHMS CONCEPT                      │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  MOTIVATION:                                                 │
│  ───────────                                                 │
│  Some problems are NP-hard (likely no polynomial algo)      │
│  Examples: TSP (exact), Set Cover, Scheduling              │
│                                                              │
│  Options:                                                    │
│  1. Use exponential algorithm (slow, small inputs)          │
│  2. Use approximation algorithm (fast, "good" solution)     │
│  3. Use heuristic (might be far from optimal)               │
│                                                              │
│  APPROXIMATION = provable guarantee on solution quality     │
│                                                              │
│  Definition:                                                │
│  ───────────                                                │
│  Algorithm A is c-approximation if:                         │
│  Solution(A) ≤ c * Optimal                                  │
│  (for minimization)                                         │
│                                                              │
│  Or equivalently:                                            │
│  Solution(A) ≥ Optimal / c                                  │
│  (for maximization)                                         │
│                                                              │
│  Typical values: c = 2, 1.5, 1.1, etc.                      │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

#### Set Cover Problem & Greedy Approximation

```
┌────────────────────────────────────────────────────────────┐
│         SET COVER PROBLEM & GREEDY                          │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  PROBLEM:                                                    │
│  ────────                                                    │
│  Given: Universe U of n elements                             │
│         Collection S of m subsets of U                       │
│  Goal: Find minimum number of subsets whose union = U        │
│                                                              │
│  This is NP-hard (no known polynomial algorithm)             │
│                                                              │
│  ─────────────────────────────────────────────────────────  │
│                                                              │
│  GREEDY APPROXIMATION:                                       │
│  ══════════════════════                                      │
│  While elements uncovered:                                   │
│    Pick subset covering MOST uncovered elements             │
│    Add to solution                                          │
│                                                              │
│  ─────────────────────────────────────────────────────────  │
│                                                              │
│  EXAMPLE:                                                    │
│  ┌────────────────────────────────────────────────────────┐ │
│  │ Universe: {1, 2, 3, 4, 5, 6}                           │ │
│  │ Subsets:                                               │ │
│  │   S₁ = {1, 2, 3}                                        │ │
│  │   S₂ = {2, 3, 4}                                        │ │
│  │   S₃ = {4, 5, 6}                                        │ │
│  │   S₄ = {1, 6}                                           │ │
│  │                                                        │ │
│  │ GREEDY:                                                │ │
│  │ Step 1: S₁ covers 3 elements {1,2,3}                  │ │
│  │         Remaining: {4,5,6}                             │ │
│  │ Step 2: S₃ covers 3 elements {4,5,6}                  │ │
│  │         Remaining: {}                                  │ │
│  │ Solution: {S₁, S₃} = 2 subsets                         │ │
│  │                                                        │ │
│  │ Greedy chose optimal here! But not always...           │ │
│  │                                                        │ │
│  │ APPROXIMATION GUARANTEE:                               │ │
│  │ Greedy is O(log n) approximation                        │ │
│  │ Meaning: Greedy ≤ OPT * ln(n)                         │ │
│  │                                                        │ │
│  └────────────────────────────────────────────────────────┘ │
│                                                              │
│  Why O(log n)?                                               │
│  ─────────────                                               │
│  When best subset covers k new elements:                    │
│  • If OPT uses m subsets, covers n elements                 │
│  • Average elements per subset = n/m                        │
│  • Greedy gets at least n/m elements per step              │
│  • Greedy needs at most m * ln(n) steps (harmonic series)  │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

### Segment 4: Summary & Synthesis (60 min)

#### When Does Greedy Work?

```
┌────────────────────────────────────────────────────────────┐
│      CHECKLIST: WHEN TO USE GREEDY?                         │
├────────────────────────────────────────────────────────────┤
│                                                              │
│  ✓ Use Greedy If:                                            │
│  ═══════════════                                             │
│                                                              │
│  1. Problem has OPTIMAL SUBSTRUCTURE                         │
│     └─ Optimal solution contains optimal subproblems        │
│                                                              │
│  2. GREEDY CHOICE PROPERTY likely holds                      │
│     └─ Greedy choice seems locally best                     │
│     └─ Intuition: gives room for remaining choices          │
│                                                              │
│  3. Can PROVE greedy choice property                         │
│     └─ Exchange argument, or                                 │
│     └─ Mathematical inequality, or                          │
│     └─ Induction on subproblems                             │
│                                                              │
│  4. Problem is NP-hard & need approximation                 │
│     └─ Greedy may give provable approximation               │
│     └─ Better than heuristic without guarantees             │
│                                                              │
│  ─────────────────────────────────────────────────────────  │
│                                                              │
│  ✗ DON'T use Greedy If:                                      │
│  ════════════════════                                        │
│                                                              │
│  1. Can DISPROVE greedy choice property                      │
│     └─ Found counter-example                                │
│     └─ Greedy suboptimal in some cases                      │
│                                                              │
│  2. Problem has no clear local optimum                       │
│     └─ Multiple criteria conflicts                          │
│     └─ No obvious "best choice"                             │
│                                                              │
│  3. Constraints are globally interdependent                 │
│     └─ Decision here affects far-away decisions             │
│     └─ Need global view (DP or search)                      │
│                                                              │
│  ─────────────────────────────────────────────────────────  │
│                                                              │
│  ? MAY USE GREEDY (heuristic):                               │
│  ═════════════════════════════                               │
│                                                              │
│  • When practical solution needed, not proven optimal        │
│  • Example: LRU cache, routing protocols                    │
│  • Performance good in practice even if not guaranteed      │
│                                                              │
└────────────────────────────────────────────────────────────┘
```

#### Week 12 Complete Summary

```
┌────────────────────────────────────────────────────────────┐
│           WEEK 12 COMPLETE SYNTHESIS                       │
├────────────────────────────────────────────────────────────┤
│                                                            │
│  CORE CONCEPTS MASTERED:                                   │
│  ═══════════════════════                                   │
│                                                            │
│  1. GREEDY ALGORITHM FUNDAMENTALS                          │
│     • Make locally optimal choice at each step             │
│     • Hope leads to globally optimal solution              │
│     • NOT always correct (must prove!)                     │
│                                                            │
│  2. TWO PROPERTIES FOR CORRECTNESS                         │
│     • Optimal substructure: subproblems are same type      │
│     • Greedy choice property: local optimal → global       │
│                                                            │
│  3. PROOF TECHNIQUES                                       │
│     • Exchange argument: swap elements, show still valid   │
│     • Induction: base + step using properties 1&2          │
│     • Stays ahead: greedy always at least as good          │
│                                                            │
│  4. CLASSIC PROBLEMS & SOLUTIONS                           │
│     • Activity selection: earliest finish wins             │
│     • Huffman coding: combine min-weight trees             │
│     • Fractional knapsack: best value/weight ratio         │
│     • Job scheduling: sort by profit, place late           │
│                                                            │
│  5. CRITICAL DISTINCTIONS                                  │
│     • Fractional KP: greedy works (continuous)             │
│     • 0/1 KP: greedy fails (discrete → use DP)             │
│     • Weighted activities: greedy fails (use DP)           │
│     • Unweighted: greedy works (finish time)               │
│                                                            │
│  6. REAL-WORLD APPLICATIONS                                │
│     • MST: Kruskal & Prim both greedy                      │
│     • Caching: LRU heuristic (good but not optimal)        │
│     • NP-hard: Greedy approximation (provable bounds)      │
│                                                            │
│  ───────────────────────────────────────────────────────── │
│                                                            │
│  KEY INSIGHTS:                                             │
│  ═════════════                                             │
│                                                            │
│  ► Greedy fast but risky (O(n log n) usually)              │
│  ► ALWAYS prove before implementing!                       │
│  ► Small change in problem → different solution needed     │
│    (Fractional vs 0/1, weighted vs unweighted)             │
│  ► When optimal too hard → approximation with bound        │
│  ► When guarantee not needed → practical heuristics        │
│                                                            │
│  ───────────────────────────────────────────────────────── │
│                                                            │
│  DECISION TREE FOR ALGORITHM CHOICE:                       │
│  ═══════════════════════════════════                       │
│                                                            │
│  Problem given                                             │
│    ↓                                                       │
│  Is greedy choice obvious?                                 │
│    ├─ NO → Try DP or other paradigm                        │
│    └─ YES ↓                                                │
│      Can you prove greedy choice property?                 │
│        ├─ NO → Counter-example exists, don't use greedy    │
│        └─ YES ↓                                            │
│          Are there edge cases / failures?                  │
│            ├─ YES → Refine proof, may not hold             │
│            └─ NO ↓                                         │
│              GREEDY WORKS! ✓                               │
│                                                            │
└────────────────────────────────────────────────────────────┘
```

---

# 📋 ASSESSMENT & CHECKPOINTS

## Day-by-Day Checkpoint Questions

### Day 1 Checkpoint

1. What two properties must a problem have for greedy to work?
2. Explain exchange argument with an example
3. Why does greedy NOT always work?

### Day 2 Checkpoint

1. Why is earliest finish time the correct greedy choice for activity selection?
2. What does "greedy stays ahead" mean?
3. Why does greedy fail for weighted activity selection?

### Day 3 Checkpoint

1. Why do we prefer prefix-free codes?
2. Why is combining minimum-weight nodes a safe greedy choice in Huffman?
3. What tree structure represents Huffman codes?

### Day 4 Checkpoint

1. Why does greedy work for fractional knapsack but not 0/1 knapsack?
2. For job scheduling, why is profit-first the correct greedy choice?
3. Can you construct a counter-example where greedy fails?

### Day 5 Checkpoint

1. Name two algorithms that use greedy for MST
2. Is LRU cache replacement always optimal? Why or why not?
3. What does O(log n) approximation mean for Set Cover?

## Weekly Assessment Rubric

**Concept Mastery (80 points):**

- Understand optimal substructure (20)
- Understand greedy choice property (20)
- Prove using exchange argument (20)
- Apply induction correctly (20)

**Problem Recognition (20 points):**

- Identify when greedy applies (10)
- Identify when greedy fails (10)

---

**Week 12 Complete! Ready for Week 13: Backtracking & Branch & Bound**

---
