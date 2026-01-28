# 📘 WEEK 12: GREEDY ALGORITHMS & PROOFS
## Phase D: Algorithm Paradigms | 25 Hours | Mastery-Focused

---

## 🎯 WEEKLY GOAL

**Master greedy algorithm design paradigm and learn rigorous correctness proofs.**

By end of Week 12, you will:
- ✅ Understand greedy algorithm concept and template
- ✅ Write exchange argument proofs rigorously
- ✅ Identify greedy choice property and optimal substructure
- ✅ Solve activity selection and interval problems
- ✅ Understand Huffman coding optimality
- ✅ Apply greedy to knapsack and scheduling
- ✅ Recognize when greedy fails and why

---

# 📅 DAY 1: GREEDY FUNDAMENTALS & PROOFS
## 5 Hours | Topics: Concept, Template, Correctness Proofs

---

## 🎓 PART 1: GREEDY ALGORITHM CONCEPT (90 minutes)

### What is a Greedy Algorithm?

A **greedy algorithm** is an algorithmic paradigm that makes **locally optimal choices at each step** with the hope that this sequence of local choices will lead to a **globally optimal solution**.

### Why "Greedy"?

The term reflects the strategy: at each decision point, the algorithm greedily picks what seems best **right now**, without reconsidering previous choices.

```
┌─────────────────────────────────────────────────┐
│        GREEDY ALGORITHM MENTAL MODEL            │
└─────────────────────────────────────────────────┘

Problem State: [Options to choose from]
                        ↓
            Pick "best-looking" option NOW
                        ↓
            Move to new state
                        ↓
            Repeat until solution found
                        ↓
        Hope: Local optimality → Global optimality
```

### The Key Insight and Risk

```
┌──────────────────────────────────────────────────────┐
│  Why Greedy is DANGEROUS (and Powerful):            │
│                                                      │
│  ✓ NO backtracking - once choice made, move on      │
│  ✓ Very fast - often O(n log n) or better           │
│  ✗ NOT always optimal - greedy can fail!            │
│  ✗ MUST prove correctness per problem               │
└──────────────────────────────────────────────────────┘
```

**Critical Understanding:**
- ❌ Greedy is NOT a magical solution
- ❌ Greedy does NOT always work
- ✅ Greedy ONLY works if problem has special structure
- ✅ MUST rigorously prove greedy is correct

### Simple Example: Coin Change (Change-making Problem)

**Problem:** Give change for amount X using minimum number of coins (denominations: 1, 5, 10, 25)

**Greedy Approach:** Always pick largest denomination that fits

```
Amount = 30 cents
├─ Pick 25¢ (largest ≤ 30)  → Remaining: 5¢
├─ Pick 5¢ (largest ≤ 5)    → Remaining: 0¢
└─ Total: 2 coins ✓ OPTIMAL
```

**Why greedy works here:**
- Coin system is "canonical" (greedy property holds)
- Larger coins never worse than smaller ones

**But with non-standard denominations (1, 3, 4):**

```
Amount = 6 cents
┌─ Greedy:        Pick 4 → Pick 1 → Pick 1 = 3 coins
└─ Optimal:       Pick 3 → Pick 3 = 2 coins ✗ GREEDY FAILS
```

---

## 🎓 PART 2: WHEN GREEDY WORKS - THE TWO PROPERTIES (90 minutes)

### Property 1: Optimal Substructure

**Definition:** An optimal solution to a problem contains optimal solutions to subproblems.

```
┌──────────────────────────────────────────────────────┐
│              OPTIMAL SUBSTRUCTURE                    │
│                                                      │
│  Original Problem → [Greedy Choice] + Subproblem    │
│                                                      │
│  If Opt(Original) = [Greedy Choice] + Opt(Sub)      │
│  Then we can build optimal from greedy choice       │
└──────────────────────────────────────────────────────┘
```

**Visual Example: Activity Selection**

```
Activities: A B C D E (sorted by finish time)
┌────┐
│ A  │
└────┘
      ┌────┐
      │ B  │
      └────┘
              ┌────┐
              │ C  │
              └────┘
                     ┌────┐
                     │ D  │
                     └────┘
                          ┌────┐
                          │ E  │
                          └────┘

Optimal solution: [A selected] + Optimal of remaining non-overlapping

The key: picking A doesn't prevent optimal solution to rest!
```

### Property 2: Greedy Choice Property

**Definition:** A globally optimal solution can be reached by making a greedy choice at each step.

```
┌──────────────────────────────────────────────────────┐
│           GREEDY CHOICE PROPERTY                     │
│                                                      │
│  There exists an optimal solution where:            │
│  • First choice IS the greedy choice                │
│  • No other choice could be first in ANY optimal    │
└──────────────────────────────────────────────────────┘
```

**Visual: Why Greedy Choice Matters**

```
Activity Selection Problem:

ALL possible orderings to consider:
  Option 1: Start with A
  Option 2: Start with B
  Option 3: Start with C
  ... millions of possibilities

Greedy Choice Property says:
  "Starting with earliest-finishing activity (A)
   is GUARANTEED to be optimal - don't even
   consider other starting points!"

This reduces search space exponentially.
```

### The Two Properties Together

```
┌────────────────────────────────────────────────────────┐
│      WHEN GREEDY WORKS: BOTH PROPERTIES NEEDED        │
│                                                        │
│  1️⃣ Optimal Substructure:                             │
│     Remaining problem after greedy choice             │
│     is same type of problem                           │
│                                                        │
│  2️⃣ Greedy Choice Property:                           │
│     Greedy choice MUST be part of                     │
│     SOME optimal solution                            │
│                                                        │
│  ✓ Both properties present → Greedy works!           │
│  ✗ Missing either property → Greedy may fail!        │
└────────────────────────────────────────────────────────┘
```

---

## 🎓 PART 3: GREEDY ALGORITHM TEMPLATE (60 minutes)

### Universal Greedy Pattern

```
┌──────────────────────────────────────────────────────┐
│         GREEDY ALGORITHM TEMPLATE                    │
│                                                      │
│  Input: Problem instance P                           │
│  Output: Solution S                                  │
│                                                      │
│  1. Sort input by selection criterion                │
│  2. Initialize S = empty                             │
│  3. FOR each element in sorted order:                │
│       IF element feasible to add:                    │
│          Add to S                                    │
│  4. Return S                                         │
│                                                      │
│  KEY: "Feasible" means respects constraints          │
│       "Selection criterion" is problem-specific      │
└──────────────────────────────────────────────────────┘
```

### Template Application: Activity Selection

```
┌─────────────────────────────────────────────────────┐
│           ACTIVITY SELECTION INSTANTIATION          │
│                                                     │
│  Input: Activities with (start, finish) times      │
│  Output: Maximum set of non-overlapping activities  │
│                                                     │
│  1. Sort by FINISH TIME (earliest first)           │
│  2. S = empty                                        │
│  3. FOR each activity in finish-time order:        │
│       IF activity doesn't overlap with S:          │
│          Add to S                                   │
│  4. Return S                                        │
│                                                     │
│  Selection criterion: Earliest finish time         │
│  Feasibility: Non-overlapping constraint           │
└─────────────────────────────────────────────────────┘
```

---

## 🎓 PART 4: EXCHANGE ARGUMENT - PROOF TECHNIQUE (90 minutes)

### What is an Exchange Argument?

The exchange argument is the **primary technique for proving greedy correctness**. The idea:

```
Start with ANY optimal solution
        ↓
Find a difference from greedy
        ↓
Swap one element with greedy choice
        ↓
Show solution is still optimal (or better)
        ↓
Repeat until solution becomes greedy solution
        ↓
Conclusion: Greedy solution is optimal
```

### Visual: Exchange Argument for Activity Selection

```
Step 1: Start with Optimal Solution O
┌─────────────────────────────────────────┐
│ O = [A₁, A₂, A₃, A₄]  (activities)     │
│ where A₁ is NOT the earliest finish     │
└─────────────────────────────────────────┘

Step 2: Greedy would pick A (earliest finish)
┌─────────────────────────────────────────┐
│ Greedy choice: A (earliest finish time) │
│ O's choice: A₁ (NOT earliest)           │
└─────────────────────────────────────────┘

Step 3: Exchange A₁ with A
┌─────────────────────────────────────────┐
│ O' = [A, A₂, A₃, A₄]                    │
│                                         │
│ Key observation:                        │
│ Since A finishes EARLIER than A₁:      │
│ - A doesn't overlap with A₂, A₃, A₄   │
│ - All original constraints still met    │
└─────────────────────────────────────────┘

Step 4: Conclude O' is also optimal
┌─────────────────────────────────────────┐
│ O' has same size as O                   │
│ O' has same or fewer conflicts           │
│ Therefore O' is also optimal            │
└─────────────────────────────────────────┘

Step 5: Repeat for next element
┌─────────────────────────────────────────┐
│ Continue exchanging A₂, A₃, A₄...       │
│ Until O' = G (greedy solution)          │
│ Therefore: Greedy solution is optimal   │
└─────────────────────────────────────────┘
```

### Key Insight of Exchange Argument

```
┌──────────────────────────────────────────────────────┐
│    WHY EXCHANGE ARGUMENT PROVES OPTIMALITY          │
│                                                      │
│  The exchange argument shows:                        │
│                                                      │
│  • For ANY optimal solution O                       │
│  • We can transform it into greedy G                │
│  • Without losing optimality                        │
│  • Therefore G is also optimal                      │
│  • But G is greedy by construction                  │
│  • So greedy approach IS optimal!                   │
└──────────────────────────────────────────────────────┘
```

### Exchange Argument Structure (General)

```
┌────────────────────────────────────────────────────────┐
│    GENERAL EXCHANGE ARGUMENT PROOF TEMPLATE           │
│                                                        │
│  Theorem: Greedy algorithm G produces optimal sol.   │
│  Proof:                                               │
│    1. Let O be ANY optimal solution                  │
│    2. IF O ≠ G:                                      │
│       a. Find first position i where O ≠ G          │
│       b. G has element g_i, O has element o_i       │
│       c. SHOW: Swapping g_i for o_i doesn't hurt   │
│       d. Result O' is still optimal                 │
│    3. Continue until O becomes G                    │
│    4. Therefore G is optimal (Q.E.D.)               │
└────────────────────────────────────────────────────────┘
```

### Why This Proof Works

The power of the exchange argument:

```
❌ Showing greedy is optimal directly is hard
   (need to consider all possible solutions)

✅ Showing ANY optimal can be transformed to greedy
   (only need to show local swaps preserve optimality)

This is much more tractable!
```

---

## 🎓 PART 5: CORRECTNESS PROOF STRATEGY (60 minutes)

### Five-Step Strategy to Prove Greedy Correctness

```
┌──────────────────────────────────────────────────────┐
│     5-STEP GREEDY CORRECTNESS PROOF STRATEGY        │
└──────────────────────────────────────────────────────┘

Step 1: Identify Greedy Choice
├─ What is the local choice?
├─ Why does it seem optimal?
└─ Example: Activity Selection chooses earliest finish

Step 2: Show Optimal Substructure
├─ Show problem can be split into:
│  └─ [Greedy Choice] + [Remaining subproblem]
├─ Remaining subproblem is same type
└─ Example: After choosing activity A,
            remaining activities form same problem

Step 3: Show Greedy Choice Property
├─ Show there EXISTS an optimal solution
│  with greedy choice as first element
├─ Use exchange argument if needed
└─ Example: Earliest-finish activity IS in
            some optimal solution

Step 4: Show Subproblems are Independent
├─ Greedy choice doesn't affect
│  optimality of remaining subproblems
└─ Example: Picking activity A limits future
            choices but doesn't make remaining
            choices worse

Step 5: Conclude by Induction
├─ If greedy choice is optimal for first step
├─ And subproblems follow same pattern
├─ Then entire greedy solution is optimal
└─ By induction on problem size
```

### Proof Structure Visualization

```
┌─────────────────────────────────────────────────────┐
│  GREEDY CORRECTNESS PROOF ARCHITECTURE              │
│                                                     │
│  ┌────────────────────────────────────────────┐    │
│  │ Greedy Choice Property                     │    │
│  │ (greedy choice in SOME optimal)            │    │
│  └────────────────────────────────────────────┘    │
│                  ↓                                   │
│  ┌────────────────────────────────────────────┐    │
│  │ Optimal Substructure                       │    │
│  │ (after greedy choice, same type problem)   │    │
│  └────────────────────────────────────────────┘    │
│                  ↓                                   │
│  ┌────────────────────────────────────────────┐    │
│  │ Inductive Step                             │    │
│  │ (greedy optimal for rest by induction)     │    │
│  └────────────────────────────────────────────┘    │
│                  ↓                                   │
│  ┌────────────────────────────────────────────┐    │
│  │ Conclusion: Greedy is Optimal              │    │
│  └────────────────────────────────────────────┘    │
└─────────────────────────────────────────────────────┘
```

### Example: Proving Activity Selection

```
┌──────────────────────────────────────────────────────┐
│      ACTIVITY SELECTION CORRECTNESS PROOF           │
│                                                      │
│  Problem: Select maximum non-overlapping activities │
│                                                      │
│  STEP 1: Identify Greedy Choice                    │
│  ├─ Greedy choice: Activity with earliest finish   │
│  └─ Intuition: Leaves most room for future        │
│                                                      │
│  STEP 2: Show Optimal Substructure                 │
│  ├─ After selecting earliest-finish activity A     │
│  ├─ Remaining activities form same type problem    │
│  │  (select max non-overlapping after A)           │
│  └─ Optimal = {A} ∪ Optimal(remaining)             │
│                                                      │
│  STEP 3: Show Greedy Choice Property               │
│  ├─ Suppose O is an optimal solution not           │
│  │  starting with earliest-finish activity         │
│  ├─ Let A_m = earliest-finish activity             │
│  ├─ Let O = {O₁, O₂, ...}                          │
│  ├─ Where O₁ ≠ A_m                                 │
│  ├─ EXCHANGE: Replace O₁ with A_m                  │
│  │  Since A_m finishes ≤ O₁ finish:                │
│  │  A_m doesn't overlap with O₂, O₃, ...           │
│  │  So O' = {A_m} ∪ {O₂, O₃, ...} is feasible     │
│  │  And |O'| = |O|, so O' is optimal               │
│  └─ Therefore: Greedy choice in optimal solution   │
│                                                      │
│  STEP 4: Show Independence                         │
│  ├─ Picking A_m (earliest finish) means:           │
│  │  Next activity must start after A_m finish      │
│  ├─ No other earlier choice would start later      │
│  └─ So A_m doesn't artificially limit future       │
│                                                      │
│  STEP 5: Inductive Conclusion                      │
│  ├─ Base: Single activity is optimal               │
│  ├─ Inductive step: If greedy optimal for         │
│  │  remaining subproblem, and greedy choice       │
│  │  optimal for first step...                      │
│  └─ Then entire greedy sequence is optimal         │
│                                                      │
│  CONCLUSION: ✓ Greedy activity selection works!    │
└──────────────────────────────────────────────────────┘
```

---

## 📋 DAY 1 SUMMARY

```
┌──────────────────────────────────────────────────────┐
│           DAY 1: KEY TAKEAWAYS                       │
│                                                      │
│  ✓ Greedy makes locally optimal choices             │
│  ✓ NOT always globally optimal (must prove!)        │
│                                                      │
│  ✓ TWO properties needed:                           │
│    1. Optimal substructure                          │
│    2. Greedy choice property                        │
│                                                      │
│  ✓ EXCHANGE ARGUMENT proves greedy works            │
│    by showing ANY optimal can become greedy         │
│                                                      │
│  ✓ 5-STEP proof strategy:                           │
│    1. Identify choice   2. Substructure             │
│    3. Choice property   4. Independence             │
│    5. Inductive conclusion                          │
│                                                      │
│  ✓ Greedy is fast (usually O(n log n))              │
│  ✓ But MUST verify with rigorous proof              │
└──────────────────────────────────────────────────────┘
```

---

# 📅 DAY 2: ACTIVITY SELECTION & INTERVAL PROBLEMS
## 5 Hours | Canonical Greedy Application

---

## 🎓 PART 1: ACTIVITY SELECTION PROBLEM (90 minutes)

### Problem Definition

```
┌──────────────────────────────────────────────────────┐
│         ACTIVITY SELECTION PROBLEM                   │
│                                                      │
│  Input: n activities, each with:                    │
│         - Start time: s_i                           │
│         - Finish time: f_i                          │
│         (ordered by finish time: f_i ≤ f_{i+1})    │
│                                                      │
│  Goal: Select maximum-size subset of               │
│         non-overlapping activities                  │
│                                                      │
│  Two activities i,j compatible if:                  │
│  f_i ≤ s_j  or  f_j ≤ s_i                          │
│  (activities don't overlap)                         │
└──────────────────────────────────────────────────────┘
```

### Visual Example

```
Timeline Visualization:

Activities on timeline:
┌─────────────────────────────────────────────┐
│                                             │
│  1[━━━━━━] 
│     2[────────]
│  3[━━]
│       4[──────]
│            5[──]
│              6[────────]
│                     7[──]
│
└─────────────────────────────────────────────┘

Problem: Pick max activities with NO overlaps
```

### Visualizing Different Choices

```
┌────────────────────────────────────────────┐
│       CHOOSING ACTIVITIES: THREE SCENARIOS │
└────────────────────────────────────────────┘

Scenario 1: Pick longest duration
[Activity starts early, runs long]
Result: Can only fit 1 activity → NOT OPTIMAL

Scenario 2: Pick earliest start
[Pick earliest starting]
Result: 2-3 activities → NOT OPTIMAL

Scenario 3: Pick earliest FINISH ✓
[Pick finishes earliest, frees time]
Result: 4 activities → OPTIMAL!

Greedy Insight:
The earlier an activity finishes,
the more time remains for future activities!
```

---

## 🎓 PART 2: GREEDY SELECTION BY FINISH TIME (90 minutes)

### The Greedy Strategy

```
┌──────────────────────────────────────────────────────┐
│    GREEDY ACTIVITY SELECTION ALGORITHM              │
│                                                      │
│  Input: Activities with finish times                │
│  Output: Maximum set S of non-overlapping activities│
│                                                      │
│  GREEDY CHOICE:                                     │
│  Always pick activity with EARLIEST FINISH TIME     │
│                                                      │
│  WHY?                                               │
│  • Earliest finishing leaves most room later        │
│  • Minimizes "time consumed"                        │
│  • Maximizes "time left" for others                 │
└──────────────────────────────────────────────────────┘
```

### Step-by-Step Example

```
Given activities (already sorted by finish time):
┌─────────────────────────────────────────────┐
│ Activity │ Start │ Finish │ Duration       │
├─────────────────────────────────────────────┤
│ 1        │ 1     │ 4      │ 3 units        │
│ 2        │ 3     │ 5      │ 2 units        │
│ 3        │ 0     │ 6      │ 6 units        │
│ 4        │ 5     │ 7      │ 2 units        │
│ 5        │ 3     │ 8      │ 5 units        │
│ 6        │ 5     │ 9      │ 4 units        │
│ 7        │ 6     │ 10     │ 4 units        │
└─────────────────────────────────────────────┘

Step 1: Select earliest-finishing activity (1)
├─ Pick: Activity 1 (finish: 4)
├─ S = {1}
└─ Next must start ≥ 4

Step 2: From remaining, pick earliest-finishing
├─ Compatible: 2(finish 5), 4(finish 7), 6(finish 9), 7(finish 10)
├─ Pick: Activity 4 (finish: 7) - earliest compatible
├─ S = {1, 4}
└─ Next must start ≥ 7

Step 3: From remaining, pick earliest-finishing
├─ Compatible: 7(finish 10)
├─ Pick: Activity 7 (finish: 10)
├─ S = {1, 4, 7}
└─ Next must start ≥ 10

Step 4: No more compatible activities
└─ Final: S = {1, 4, 7} with size 3
```

### Why Greedy Finish-Time Works: Intuitive Argument

```
┌──────────────────────────────────────────────────────┐
│    "GREEDY STAYS AHEAD" INTUITION                   │
│                                                      │
│  Claim: Greedy finishes NO LATER than any optimal   │
│                                                      │
│  Proof sketch:                                       │
│  • G₁ = greedy's first activity (earliest finish)   │
│  • O₁ = optimal's first activity                    │
│  • By definition: f(G₁) ≤ f(O₁)                     │
│                                                      │
│  So greedy finishes at time f(G₁)                   │
│  Optimal finishes at time ≥ f(O₁) ≥ f(G₁)          │
│                                                      │
│  This means:                                         │
│  • Greedy has MORE TIME left than optimal!          │
│  • Whatever optimal can fit after its first activity│
│  • Greedy can also fit (same or more!)              │
│                                                      │
│  By repeating this argument:                        │
│  • Greedy stays ahead at each step                  │
│  • So greedy can accommodate ≥ activities than opt  │
│  • Therefore greedy is optimal!                     │
└──────────────────────────────────────────────────────┘
```

### Visual Proof: "Greedy Stays Ahead"

```
Timeline comparison:

Optimal solution:
○ O₁ [finishes at time t₁]
        ○ O₂ [finishes at time t₂]
              ○ O₃ [finishes at time t₃]

Greedy solution:
● G₁ [finishes at time t₁'≤t₁]
  
  ← More room for next activity!
  
        ● G₂ [finishes at time t₂'≤t₂]
        
        ← More room for next activity!
        
              ● G₃ [finishes at time t₃'≤t₃]

At each stage, greedy finishes ≤ optimal
So greedy has at least as much room for future activities
Therefore greedy achieves ≥ activities as optimal
```

---

## 🎓 PART 3: INTERVAL SCHEDULING VARIATIONS (60 minutes)

### Variation 1: Weighted Activity Selection

```
┌──────────────────────────────────────────────────────┐
│   VARIATION: WEIGHTED ACTIVITY SELECTION             │
│                                                      │
│  Problem: Each activity has WEIGHT (value)           │
│  Goal: Maximize TOTAL WEIGHT (not count)             │
│                                                      │
│  Example:                                            │
│  Activity 1: [1,4]  weight=10                       │
│  Activity 2: [3,5]  weight=8                        │
│  Activity 3: [0,6]  weight=6                        │
│                                                      │
│  Greedy by finish time gives: 1+2+... = 22          │
│  But optimal might be: Just 3 = 6 (WORSE!)          │
│  Or optimal might be: 1+3 = 16 (BETTER!)            │
│                                                      │
│  ✗ GREEDY FAILS for weighted version!               │
│  ✓ Dynamic Programming needed instead                │
└──────────────────────────────────────────────────────┘
```

### Variation 2: Room Assignment (Interval Coloring)

```
┌──────────────────────────────────────────────────────┐
│   VARIATION: ROOM ASSIGNMENT PROBLEM                 │
│                                                      │
│  Problem: Assign activities to rooms                │
│  Constraint: Overlapping activities → different room│
│  Goal: Minimize number of rooms needed               │
│                                                      │
│  Greedy approach: Process activities by start time  │
│  • Assign to room with earliest free time          │
│  • If no room free, create new room                │
│                                                      │
│  Visual:                                             │
│  Activity 1: [0, 3]    → Room A                     │
│  Activity 2: [1, 4]    → Room B (overlaps with 1)  │
│  Activity 3: [2, 5]    → Room C (overlaps 1,2)     │
│  Activity 4: [3, 6]    → Room A (after 1 ends)     │
│  Activity 5: [3, 7]    → Room B (after 2 ends)     │
│                                                      │
│  Result: 3 rooms needed                              │
│  This equals max overlapping activities             │
│  ✓ GREEDY WORKS here!                               │
└──────────────────────────────────────────────────────┘
```

### Variation 3: Interval Partitioning

```
┌──────────────────────────────────────────────────────┐
│   VARIATION: INTERVAL PARTITIONING                   │
│                                                      │
│  Problem: Partition intervals into minimum groups    │
│  Constraint: No overlapping intervals in same group  │
│  Goal: Minimize number of groups                     │
│                                                      │
│  Same as room assignment!                            │
│  Answer = maximum number of overlapping intervals   │
│                                                      │
│  Example:                                            │
│  Max overlap at any point = 3                        │
│  So minimum 3 groups needed                          │
│                                                      │
│  ✓ Greedy processes by start time                    │
│  ✓ Assigns to group with latest end time            │
│  ✓ Always optimal!                                   │
└──────────────────────────────────────────────────────┘
```

### Decision Tree: Which Greedy for Intervals?

```
┌─────────────────────────────────────────────────────┐
│  INTERVAL PROBLEMS: WHICH GREEDY APPROACH?         │
│                                                     │
│  Want: Max count of activities?                    │
│  └─→ Greedy by FINISH TIME ✓                       │
│                                                     │
│  Want: Min rooms/groups needed?                    │
│  └─→ Greedy by START TIME ✓                        │
│      (sweep line algorithm)                        │
│                                                     │
│  Want: Max weight (weighted)?                      │
│  └─→ NOT GREEDY! Use DP instead ✗                  │
│                                                     │
│  Want: Min weight? (weighted interval scheduling)  │
│  └─→ NOT GREEDY! Use DP instead ✗                  │
└─────────────────────────────────────────────────────┘
```

---

## 🎓 PART 4: GREEDY STAYS AHEAD PRINCIPLE (60 minutes)

### The Core Principle

```
┌──────────────────────────────────────────────────────┐
│   "GREEDY STAYS AHEAD" PROOF PRINCIPLE              │
│                                                      │
│  Used for many greedy proofs, not just activity sel │
│                                                      │
│  Pattern:                                            │
│  1. Compare greedy solution G with optimal O        │
│  2. Show: G achieves something faster/earlier/less  │
│  3. At each step, G ≤ O                             │
│  4. Therefore G can achieve ≥ O                     │
│  5. So G is also optimal                            │
└──────────────────────────────────────────────────────┘
```

### Key Insight: Why Staying Ahead Means Optimal

```
┌──────────────────────────────────────────────────────┐
│       WHY "STAYING AHEAD" IMPLIES OPTIMALITY        │
│                                                      │
│  Suppose greedy G always finishes before optimal O  │
│                                                      │
│  At each step i:                                    │
│  • G has completed action A_i by time t_g          │
│  • O has completed action A'_i by time t_o         │
│  • We show: t_g ≤ t_o                              │
│                                                      │
│  Why this matters:                                  │
│  • If G finishes earlier, G has more time left     │
│  • For next action, G can handle anything O can    │
│  • So G can fit ≥ actions than O                   │
│  • Since O is optimal with size k                  │
│  • G must also have size k (G is optimal too!)     │
└──────────────────────────────────────────────────────┘
```

### Abstract Template: Stay Ahead Pattern

```
┌────────────────────────────────────────────────────────┐
│    STAY-AHEAD PROOF TEMPLATE                          │
│                                                        │
│  Theorem: Greedy algorithm G is optimal               │
│                                                        │
│  Proof:                                                │
│    Let O = any optimal solution                       │
│    Let measure M = some metric we care about          │
│                                                        │
│    Claim 1: After first step, M(G) ≤ M(O)             │
│    Proof: [Show greedy choice minimizes/maximizes M]  │
│                                                        │
│    Claim 2: If M(G) ≤ M(O) at step i                  │
│             Then M(G) ≤ M(O) at step i+1              │
│    Proof: [Show greedy stays ahead one more step]     │
│                                                        │
│    Conclusion: By induction, M(G) ≤ M(O) always       │
│               So G is as good as O on key metric      │
│               Since O is optimal, G is optimal!       │
└────────────────────────────────────────────────────────┘
```

---

## 📋 DAY 2 SUMMARY

```
┌──────────────────────────────────────────────────────┐
│           DAY 2: KEY TAKEAWAYS                       │
│                                                      │
│  ✓ Activity Selection: classic greedy problem        │
│  ✓ Greedy by FINISH TIME is optimal                  │
│                                                      │
│  ✓ "Greedy Stays Ahead" proof pattern:               │
│    Shows greedy finishes ≤ optimal at each step      │
│    Therefore greedy can fit ≥ activities             │
│    So greedy is optimal                              │
│                                                      │
│  ✓ Interval problems have multiple variants:         │
│    • Max activities → finish time greedy             │
│    • Min rooms → start time greedy                   │
│    • Weighted → need DP, not greedy                  │
│                                                      │
│  ✓ Key insight: Earlier finish = more room later     │
│  ✓ Choosing "best for now" = best overall            │
└──────────────────────────────────────────────────────┘
```

---

# 📅 DAY 3: HUFFMAN CODING & OPTIMAL PREFIX TREES
## 5 Hours | Optimal Tree Construction

---

## 🎓 PART 1: PREFIX CODES AND THE HUFFMAN PROBLEM (90 minutes)

### What is a Prefix Code?

```
┌──────────────────────────────────────────────────────┐
│              PREFIX CODES: DEFINITION                │
│                                                      │
│  Code: Assignment of binary strings to characters   │
│  Example: 'A'→0, 'B'→1, 'C'→00 (NO - violates prefix)│
│                                                      │
│  Prefix Code: No codeword is prefix of another      │
│  Example: 'A'→0, 'B'→10, 'C'→110 ✓ Valid            │
│                                                      │
│  Property: Can decode uniquely without delimiters  │
│  String: 01011 can only be A,B,A,C (001-0-11)      │
└──────────────────────────────────────────────────────┘
```

### Why Prefix Codes Matter

```
String: "ABAC"

Option 1: Fixed-length codes
'A'=00, 'B'=01, 'C'=10
Encoded: 00 01 00 10 (8 bits)

Option 2: Variable-length codes (prefix)
'A'=0, 'B'=10, 'C'=11
Encoded: 0 10 0 11 (6 bits) ← 25% savings!

BUT: Need code with no prefix property
Otherwise can't decode uniquely!
```

### The Huffman Coding Problem

```
┌──────────────────────────────────────────────────────┐
│         HUFFMAN CODING PROBLEM                       │
│                                                      │
│  Input: Characters with frequencies (prob of use)   │
│  Example:                                            │
│    'A': frequency 45%  (most common)                 │
│    'B': frequency 13%  (less common)                 │
│    'C': frequency 12%  (less common)                 │
│    'D': frequency 16%  (less common)                 │
│    'E': frequency 9%   (least common)                │
│                                                      │
│  Goal: Find prefix code that minimizes expected     │
│        encoded message length                        │
│                                                      │
│  Expected length = Σ (frequency × codeword length)  │
│                                                      │
│  Minimize: 0.45×1 + 0.13×2 + 0.12×2 + 0.16×2 + 0.09×2│
└──────────────────────────────────────────────────────┘
```

### Representing Codes as Binary Trees

```
Key insight: Prefix codes ↔ Binary trees with chars at leaves

Example encoding tree:
         ┌─────────────┐
         │ ROOT        │
         └──────┬──────┘
               / \
              0   1
            /       \
     ┌──────┐    ┌──────┐
     │ 'A'  │    │ ROOT │
     └──────┘    └──┬───┘
                   / \
                  0   1
                /       \
         ┌────┐       ┌────┐
         │'B' │       │'C' │
         └────┘       └────┘

Decoding tree (left=0, right=1):
- 0 → 'A'
- 10 → 'B'
- 11 → 'C'

Property: All chars at leaves (prefix-free!)
Cost: (frequency×depth) summed for all chars

Tree structure determines expected length!
```

### Cost of a Prefix Code Tree

```
┌──────────────────────────────────────────────────────┐
│         COST OF PREFIX CODE TREE                     │
│                                                      │
│  Definition: Cost = Σ (frequency × depth)           │
│              for each character                      │
│                                                      │
│  Meaning: Expected number of bits per character     │
│                                                      │
│  Example:                                            │
│  'A': freq=45, depth=1  → cost = 45×1 = 45         │
│  'B': freq=13, depth=2  → cost = 13×2 = 26         │
│  'C': freq=12, depth=2  → cost = 12×2 = 24         │
│  'D': freq=16, depth=2  → cost = 16×2 = 32         │
│  'E': freq= 9, depth=3  → cost =  9×3 = 27         │
│                                                      │
│  Total: 45+26+24+32+27 = 154                        │
│  Average: 154/100 = 1.54 bits per character         │
│                                                      │
│  Goal: Minimize this cost!                          │
└──────────────────────────────────────────────────────┘
```

---

## 🎓 PART 2: THE HUFFMAN ALGORITHM (90 minutes)

### The Greedy Strategy

```
┌──────────────────────────────────────────────────────┐
│      HUFFMAN ALGORITHM: GREEDY STRATEGY              │
│                                                      │
│  Goal: Build optimal prefix tree by greedily        │
│        combining lowest-frequency nodes             │
│                                                      │
│  Intuition: High-frequency chars → shallow (short)  │
│            Low-frequency chars → deep (long)        │
│            This minimizes expected length!          │
│                                                      │
│  Greedy choice: Always combine two nodes with       │
│                 smallest frequencies next            │
└──────────────────────────────────────────────────────┘
```

### Algorithm Steps

```
┌──────────────────────────────────────────────────────┐
│       HUFFMAN ALGORITHM: STEP-BY-STEP                │
│                                                      │
│  Input: n characters with frequencies               │
│  Output: Optimal prefix tree                        │
│                                                      │
│  STEP 1: Create leaf node for each character        │
│          with its frequency                          │
│                                                      │
│  STEP 2: WHILE more than 1 node in forest:          │
│          a. Extract 2 nodes with min frequency      │
│          b. Create parent node with combined freq   │
│          c. Add parent back to forest               │
│                                                      │
│  STEP 3: Return remaining node as tree root         │
└──────────────────────────────────────────────────────┘
```

### Detailed Example: Building Huffman Tree

```
Start: Characters with frequencies
┌─────┐  ┌─────┐  ┌─────┐  ┌─────┐  ┌─────┐
│ 'A' │  │ 'B' │  │ 'C' │  │ 'D' │  │ 'E' │
│ 45  │  │ 13  │  │ 12  │  │ 16  │  │  9  │
└─────┘  └─────┘  └─────┘  └─────┘  └─────┘

Iteration 1: Combine two smallest (12, 9)
Merge C(12) + E(9) = 21

State:
┌─────┐  ┌─────┐  ┌──────┐  ┌──────┐
│ 'A' │  │ 'B' │  │  X   │  │ 'D' │
│ 45  │  │ 13  │  │ 21   │  │ 16  │
└─────┘  └─────┘  └──────┘  └──────┘
           /  \
         'C'  'E'
         12    9

Iteration 2: Combine two smallest (13, 16)
Merge B(13) + D(16) = 29

State:
┌─────┐  ┌──────┐  ┌──────┐
│ 'A' │  │  X   │  │  Y   │
│ 45  │  │ 21   │  │ 29   │
└─────┘  └──────┘  └──────┘
          /  \      /  \
        'C'  'E'   'B'  'D'
        12    9    13   16

Iteration 3: Combine two smallest (21, 29)
Merge 21 + 29 = 50

State:
┌─────┐  ┌──────┐
│ 'A' │  │  Z   │
│ 45  │  │ 50   │
└─────┘  └──────┘
          /      \
         X        Y
        / \      / \
       C   E    B   D
      12   9   13  16

Iteration 4: Combine two smallest (45, 50)
Merge 45 + 50 = 95 (FINAL ROOT)

Final tree:
        ┌──────┐
        │ ROOT │
        │ 95   │
        └──┬───┘
          / \
         /   \
        /     \
    ┌───┐   ┌──────┐
    │A  │   │  X   │
    │45 │   │ 50   │
    └───┘   └──┬───┘
              / \
             /   \
        ┌───┐   ┌──────┐
        │X  │   │  Y   │
        │21 │   │ 29   │
        └─┬─┘   └──┬───┘
         / \      / \
        /   \    /   \
       C     E  B     D
       12    9  13   16

Resulting codes:
'A': 0 (depth 1)
'C': 100 (depth 3)
'E': 101 (depth 3)
'B': 110 (depth 3)
'D': 111 (depth 3)

Total cost: 45×1 + 12×3 + 9×3 + 13×3 + 16×3
         = 45 + 36 + 27 + 39 + 48 = 195
```

### Why This Greedy Works: Intuitive Argument

```
┌──────────────────────────────────────────────────────┐
│    WHY HUFFMAN GREEDY IS OPTIMAL                     │
│                                                      │
│  Key insight:                                        │
│  If we're combining two nodes into parent,           │
│  we should combine the ones we'll visit least often  │
│  (least frequently used) to minimize depth impact    │
│                                                      │
│  Why combine smallest frequencies?                   │
│  • If we combine, they go deeper in final tree       │
│  • Depth × frequency = cost increase                 │
│  • So we WANT least-frequent chars deep              │
│  • Therefore: combine smallest frequencies          │
│                                                      │
│  If we combined large frequencies instead:          │
│  Those chars would end up deep                       │
│  But they appear often!                              │
│  So cost would be huge (freq × depth)                │
│  ✗ MUCH worse than huffman!                         │
│                                                      │
│  ✓ HUFFMAN greedy choice minimizes cost              │
└──────────────────────────────────────────────────────┘
```

### Mathematical Proof Sketch

```
┌──────────────────────────────────────────────────────┐
│    HUFFMAN OPTIMALITY PROOF SKETCH                   │
│                                                      │
│  Theorem: Huffman tree minimizes cost                │
│                                                      │
│  Proof idea (exchange argument):                     │
│                                                      │
│  Let O = any optimal tree                            │
│  Let H = Huffman tree                                │
│                                                      │
│  Step 1: In O, there must be two leaf siblings       │
│          with smallest frequencies f₁, f₂ (or else  │
│          we could rearrange to put small depths      │
│          on largest frequencies, improving O)        │
│                                                      │
│  Step 2: Huffman combines two smallest frequencies  │
│          creating parent node                        │
│                                                      │
│  Step 3: If O's smallest-freq pair differs from H's  │
│          Replace them with Huffman's pair           │
│          Result: new tree O' with cost ≤ O's cost   │
│          (swapping smaller frequencies improves!)    │
│                                                      │
│  Step 4: Recursively apply to remaining tree        │
│          Eventually transform O into H              │
│          showing H is optimal                        │
│                                                      │
│  Conclusion: Huffman is optimal!                     │
└──────────────────────────────────────────────────────┘
```

---

## 🎓 PART 3: HUFFMAN TREE PROPERTIES (60 minutes)

### Optimality Guarantee

```
┌──────────────────────────────────────────────────────┐
│      HUFFMAN TREE: OPTIMALITY PROPERTIES             │
│                                                      │
│  Property 1: Optimal Substructure                    │
│  ├─ Remove any internal node from Huffman tree      │
│  ├─ Remaining subtree is Huffman for its chars      │
│  └─ No better tree possible for that subset         │
│                                                      │
│  Property 2: Greedy Choice Property                  │
│  ├─ Two smallest-frequency nodes MUST be siblings   │
│  │  in any optimal tree                             │
│  ├─ Huffman puts them as siblings                   │
│  └─ So Huffman's first choice is in some optimal    │
│                                                      │
│  Property 3: Local Optimality → Global              │
│  ├─ Once we make greedy choice (combine two)        │
│  ├─ Remaining problem is same type                  │
│  ├─ Solve recursively                               │
│  └─ Gives globally optimal solution                 │
│                                                      │
│  Result: ✓ Huffman guaranteed optimal for any       │
│           frequency distribution                    │
└──────────────────────────────────────────────────────┘
```

### Cost Analysis

```
┌──────────────────────────────────────────────────────┐
│         HUFFMAN TREE: COST ANALYSIS                  │
│                                                      │
│  Input: n characters with frequencies               │
│                                                      │
│  Time Complexity: O(n log n)                        │
│  ├─ Create n nodes: O(n)                            │
│  ├─ Extract min + insert: O(log n) each             │
│  ├─ Do this n-1 times (combine until 1 node)       │
│  ├─ Total: O(n log n) with heap                     │
│  └─ Optimal: can't do better in general             │
│                                                      │
│  Space Complexity: O(n)                             │
│  ├─ Store tree with n leaves                        │
│  ├─ Need n-1 internal nodes                         │
│  ├─ Total: 2n-1 nodes                               │
│  └─ O(n) space                                       │
│                                                      │
│  Compression Ratio Achieved:                        │
│  ├─ Depends on character frequency distribution     │
│  ├─ Uniform distribution: ~log₂(n) average length  │
│  ├─ Skewed distribution: can approach 1 bit         │
│  ├─ Best case: highly skewed (few high freq)        │
│  └─ Typically: 10-40% compression vs fixed-length   │
└──────────────────────────────────────────────────────┘
```

### Huffman in Practice

```
┌──────────────────────────────────────────────────────┐
│        HUFFMAN CODING: REAL-WORLD USE                │
│                                                      │
│  Applications:                                       │
│  • JPEG compression (combines with other methods)    │
│  • ZIP/DEFLATE file compression                      │
│  • Data transmission (minimize bits)                 │
│  • Text compression                                  │
│                                                      │
│  Limitations:                                        │
│  • Requires knowing character frequencies upfront    │
│  • Must transmit/store code tree with data           │
│  • Static Huffman not adaptive                       │
│  • Better compression with context modeling         │
│                                                      │
│  Extensions:                                         │
│  • Adaptive Huffman: update tree as data processes  │
│  • Canonical Huffman: compact representation         │
│  • Arithmetic coding: better than Huffman            │
└──────────────────────────────────────────────────────┘
```

---

## 📋 DAY 3 SUMMARY

```
┌──────────────────────────────────────────────────────┐
│           DAY 3: KEY TAKEAWAYS                       │
│                                                      │
│  ✓ Prefix codes enable unique decoding               │
│  ✓ Can represent codes as binary trees               │
│  ✓ Cost = Σ (frequency × depth)                      │
│                                                      │
│  ✓ Huffman algorithm: greedy, builds tree bottom-up  │
│  ✓ Always combines two smallest frequencies          │
│                                                      │
│  ✓ Intuition: low-freq chars go deep (minimize cost)│
│  ✓ Result: optimal prefix code                       │
│                                                      │
│  ✓ Optimality proven via exchange argument           │
│  ✓ Time: O(n log n), Space: O(n)                    │
│                                                      │
│  ✓ Used in JPEG, ZIP, data compression               │
└──────────────────────────────────────────────────────┘
```

---

# 📅 DAY 4: FRACTIONAL KNAPSACK & JOB SCHEDULING
## 5 Hours | Practical Greedy Applications

---

## 🎓 PART 1: FRACTIONAL KNAPSACK PROBLEM (90 minutes)

### Problem Definition

```
┌──────────────────────────────────────────────────────┐
│       FRACTIONAL KNAPSACK PROBLEM                    │
│                                                      │
│  Given:                                              │
│  • n items, each with weight w_i and value v_i      │
│  • Knapsack capacity W (weight limit)                │
│  • Can take FRACTIONS of items (0 ≤ x_i ≤ 1)       │
│                                                      │
│  Goal: Maximize total value subject to:              │
│  Σ (x_i × w_i) ≤ W                                   │
│                                                      │
│  Maximize: Σ (x_i × v_i)                             │
│                                                      │
│  Key difference from 0/1 knapsack:                   │
│  ├─ 0/1: Take whole item or nothing                  │
│  ├─ Fractional: Can take partial item                │
│  └─ This makes greedy work!                          │
└──────────────────────────────────────────────────────┘
```

### Example Problem

```
Knapsack capacity: 15 kg

Items:
┌──────┬────────┬────────┬──────────────┐
│ Item │ Weight │ Value  │ Value/Weight │
├──────┼────────┼────────┼──────────────┤
│ A    │ 5 kg   │ $50    │ $10/kg       │
│ B    │ 10 kg  │ $60    │ $6/kg        │
│ C    │ 3 kg   │ $12    │ $4/kg        │
└──────┴────────┴────────┴──────────────┘

Greedy strategy: Take by VALUE/WEIGHT ratio (best bang for buck)

Selection order: A ($10/kg) → B ($6/kg) → C ($4/kg)

Step 1: Take all of A (5 kg, value $50)
├─ Weight used: 5 kg
├─ Value gained: $50
└─ Capacity left: 10 kg

Step 2: Take all of B (10 kg, value $60)
├─ Weight used: 10 kg
├─ Value gained: $60
├─ Total weight: 15 kg
└─ Capacity left: 0 kg

Final result:
└─ Total value: $50 + $60 = $110 ✓ OPTIMAL
```

### Why Greedy by Value/Weight Ratio Works

```
┌──────────────────────────────────────────────────────┐
│   FRACTIONAL KNAPSACK: WHY GREEDY WORKS              │
│                                                      │
│  Greedy property: Take items by value/weight ratio  │
│  (highest first)                                     │
│                                                      │
│  Why this is optimal:                                │
│                                                      │
│  1. We want maximum value using W weight            │
│  2. Each kg of item i gives value v_i/w_i          │
│  3. To maximize total value:                        │
│     "Spend weight on items giving best value/kg"   │
│  4. Since we can take fractions:                    │
│     Top off knapsack with highest-ratio items       │
│  5. Any other strategy would use weight on           │
│     lower-ratio items (losing value!)                │
│  6. Therefore greedy is optimal!                    │
│                                                      │
│  Key insight: Because fractions allowed,             │
│  we can ALWAYS fill knapsack to capacity            │
│  with highest-ratio items (at least partially)      │
└──────────────────────────────────────────────────────┘
```

### Visual: Why Fractions Make Greedy Work

```
Fractional knapsack (GREEDY WORKS):
┌─────────────────────────────┐
│ Item A: ████████ (full)     │
│ Item B: ████████ (full)     │
│ Item C: ████ (partial)      │  ← Can take fraction!
│         ════ Knapsack full  │
└─────────────────────────────┘
Result: Optimal!

0/1 Knapsack (GREEDY FAILS):
┌─────────────────────────────┐
│ If highest-ratio is small:  │
│ Item A: ███ (must take full)│
│ But now can't fit others!   │
│ Remaining capacity wasted   │
│         ════ Knapsack       │
└─────────────────────────────┘
Result: NOT always optimal! (DP needed)

Key difference:
Fractional: Fill gaps with partial items
0/1: Can't fill gaps → can be suboptimal
```

### Proof: Why Fractional Greedy is Optimal

```
┌──────────────────────────────────────────────────────┐
│   FRACTIONAL KNAPSACK OPTIMALITY PROOF              │
│                                                      │
│  Theorem: Greedy (by value/weight) is optimal       │
│                                                      │
│  Proof sketch (exchange argument):                  │
│                                                      │
│  1. Let O = any optimal solution                    │
│  2. Let G = greedy solution                         │
│  3. Sort items by value/weight: A, B, C, ...        │
│     (descending order)                              │
│                                                      │
│  4. CASE: O takes different item first than G      │
│     ├─ G takes A (highest ratio) with amount a     │
│     ├─ O takes something else                       │
│     ├─ Since A has best ratio, swapping in A       │
│     │  improves value without increasing weight    │
│     └─ So O wasn't optimal! Contradiction.         │
│                                                      │
│  5. CASE: O takes same items as G but different    │
│     amounts for some items                          │
│     ├─ Similar argument: O could increase          │
│     │  lower-ratio items by higher-ratio items     │
│     │  getting better value                        │
│     └─ Contradiction.                               │
│                                                      │
│  6. Therefore: G must be optimal!                   │
└──────────────────────────────────────────────────────┘
```

---

## 🎓 PART 2: 0/1 vs FRACTIONAL KNAPSACK (60 minutes)

### The Critical Difference

```
┌──────────────────────────────────────────────────────┐
│    0/1 KNAPSACK VS FRACTIONAL KNAPSACK               │
│                                                      │
│  0/1 Knapsack:                                       │
│  • Must take entire item OR nothing                  │
│  • Cannot divide items                               │
│  • NP-hard problem (no known polynomial algorithm)   │
│  • Requires Dynamic Programming                      │
│  • Example: Can't split a painting                   │
│                                                      │
│  Fractional Knapsack:                                │
│  • Can take fractions of items                       │
│  • Can divide items arbitrarily                      │
│  • Polynomial-time (greedy works!)                   │
│  • O(n log n) with sorting                           │
│  • Example: Can split grain/liquid                   │
└──────────────────────────────────────────────────────┘
```

### Why Greedy Fails for 0/1 Knapsack

```
Counterexample: Greedy by value/weight FAILS for 0/1

Capacity: 10 kg

Items:
┌──────┬────────┬────────┬──────────────┐
│ Item │ Weight │ Value  │ Value/Weight │
├──────┼────────┼────────┼──────────────┤
│ A    │ 6 kg   │ $30    │ $5/kg        │
│ B    │ 3 kg   │ $14    │ $4.67/kg     │
│ C    │ 4 kg   │ $16    │ $4/kg        │
└──────┴────────┴────────┴──────────────┘

Greedy approach (by ratio):
1. A ($5/kg): Take (6 kg, value $30)  → Remaining: 4 kg
2. B ($4.67/kg): Can't fit (needs 3 kg, have 4, OK!)
   Take (3 kg, value $14) → Remaining: 1 kg
3. C ($4/kg): Can't fit (needs 4 kg, have 1)
   Skip
Total: $30 + $14 = $44

Optimal solution:
Take B (3 kg, $14) + C (4 kg, $16) = 7 kg, $30
Wait, that's $44. Let me recalculate...

Actually, let me use different example:

Items:
┌──────┬────────┬────────┬──────────────┐
│ Item │ Weight │ Value  │ Value/Weight │
├──────┼────────┼────────┼──────────────┤
│ A    │ 5 kg   │ $100   │ $20/kg       │
│ B    │ 4 kg   │ $80    │ $20/kg       │
│ C    │ 3 kg   │ $40    │ $13.33/kg    │
└──────┴────────┴────────┴──────────────┘

Capacity: 7 kg

Greedy approach:
1. A ($20/kg): Take (5 kg, value $100) → Remaining: 2 kg
2. B ($20/kg): Can't fit (needs 4 kg)
3. C ($13.33/kg): Can't fit (needs 3 kg)
Total: $100

Optimal: A (5 kg, $100) OR B+C (7 kg, $120) 
✓ Optimal is $120 (B+C), not $100!
✗ GREEDY FAILS! It picks A first and can't fit others.
```

### Key Insight: Why Structure Breaks

```
┌──────────────────────────────────────────────────────┐
│   WHY GREEDY FAILS FOR 0/1 KNAPSACK                  │
│                                                      │
│  For Fractional Knapsack:                            │
│  ✓ Can take partial items                            │
│  ✓ Always fill knapsack to capacity                  │
│  ✓ No wasted space if all high-ratio items taken    │
│  ✓ Greedy choice property holds                      │
│                                                      │
│  For 0/1 Knapsack:                                   │
│  ✗ Cannot take partial items                         │
│  ✗ May have wasted space after high-ratio items     │
│  ✗ Lower-ratio combinations might fit better        │
│  ✗ Greedy choice property FAILS!                     │
│                                                      │
│  Example: High-ratio item too large                  │
│  Takes up space, leaves gap no other item fits      │
│  But multiple lower-ratio items would fill gap      │
│  giving better total value!                         │
└──────────────────────────────────────────────────────┘
```

---

## 🎓 PART 3: JOB SEQUENCING WITH DEADLINES (90 minutes)

### Problem Definition

```
┌──────────────────────────────────────────────────────┐
│  JOB SEQUENCING WITH DEADLINES PROBLEM               │
│                                                      │
│  Given:                                              │
│  • n jobs, each with:                                │
│    - Deadline d_i (must complete by this time)       │
│    - Profit p_i (gain if completed by deadline)      │
│  • Each job takes 1 unit of time                     │
│  • Can do at most 1 job at a time                    │
│                                                      │
│  Goal: Select subset of jobs to maximize profit      │
│        while meeting all deadlines                   │
│                                                      │
│  Constraints:                                        │
│  • If select job i with deadline d_i,                │
│    must schedule it in slot ≤ d_i                    │
│  • No two jobs in same time slot                     │
└──────────────────────────────────────────────────────┘
```

### Visual Example

```
Jobs with deadlines and profits:
┌──────┬──────────┬─────────┐
│ Job  │ Deadline │ Profit  │
├──────┼──────────┼─────────┤
│ J1   │ 2        │ $100    │
│ J2   │ 2        │ $80     │
│ J3   │ 1        │ $30     │
│ J4   │ 3        │ $60     │
└──────┴──────────┴─────────┘

Timeline visualization (3 time slots):
Time:  [Slot 1] [Slot 2] [Slot 3]
        ━━━━━━  ━━━━━━  ━━━━━━

Greedy: Select by highest profit first
├─ Select J1 (profit $100, deadline 2)
├─ Select J2 (profit $80, deadline 2)
├─ Select J4 (profit $60, deadline 3)
└─ Skip J3 (no room by deadline 1)

Scheduling:
Time 1: [J3] ← Only job with deadline 1
Time 2: [J1] ← One of jobs with deadline 2
Time 3: [J4] ← One of jobs with deadline 3

Note: J2 ($80) cannot be scheduled (deadline 2 only)
Total profit: $30 + $100 + $60 = $190

Wait, let's try greedy by profit:
Sorted by profit: J1($100), J2($80), J4($60), J3($30)

Step 1: Select J1 (deadline 2, profit $100)
├─ Schedule J1 at latest feasible slot before deadline 2
├─ Latest slot: 2
└─ Schedule: Time 2 ← J1

Step 2: Select J2 (deadline 2, profit $80)
├─ Schedule at latest feasible slot before deadline 2
├─ Latest slot: 1 (slot 2 occupied)
└─ Schedule: Time 1 ← J2

Step 3: Select J4 (deadline 3, profit $60)
├─ Schedule at latest feasible slot before deadline 3
├─ Latest slot: 3 (slots 1,2 occupied)
└─ Schedule: Time 3 ← J4

Step 4: Select J3 (deadline 1, profit $30)
├─ Schedule at latest feasible slot before deadline 1
├─ No slots available before/at time 1
└─ Cannot include J3

Result:
Time 1: [J2] ($80)
Time 2: [J1] ($100)
Time 3: [J4] ($60)
Total profit: $240 ✓ OPTIMAL
```

### The Greedy Strategy

```
┌──────────────────────────────────────────────────────┐
│    JOB SEQUENCING GREEDY STRATEGY                    │
│                                                      │
│  Step 1: Sort jobs by PROFIT (descending)            │
│                                                      │
│  Step 2: FOR each job in profit order:               │
│          Find latest time slot ≤ deadline            │
│          where no job scheduled yet                  │
│          If found: schedule job there                │
│          Else: skip job (don't include)              │
│                                                      │
│  Step 3: Return all scheduled jobs                   │
│                                                      │
│  Intuition:                                           │
│  • Want to maximize profit → pick high-profit first  │
│  • For each job, give it latest possible slot       │
│  • This leaves earlier slots for others              │
│  • Others might have tighter deadlines               │
│  → best to fill latest slots with high-profit       │
└──────────────────────────────────────────────────────┘
```

### Why "Latest Slot" Strategy Works

```
┌──────────────────────────────────────────────────────┐
│   WHY SCHEDULE AT LATEST FEASIBLE SLOT               │
│                                                      │
│  Greedy insight: When scheduling job i with         │
│  deadline d_i, place it at LATEST feasible slot     │
│  ≤ d_i                                               │
│                                                      │
│  Why? (Intuitive argument)                           │
│                                                      │
│  If we schedule job i at earlier slot:              │
│  • Later slots still available                       │
│  • Future jobs might have later deadlines            │
│  • OR might need this job's slot!                    │
│  • We waste scheduling flexibility                   │
│                                                      │
│  If we schedule job i at latest slot:                │
│  • Earlier slots remain free                         │
│  • Future jobs have full range of options            │
│  • Maximizes flexibility for others                  │
│  • More jobs can fit overall                         │
│                                                      │
│  Analogy: Parking lot                                │
│  • New car arrives                                   │
│  • Park at back (latest) spot                        │
│  • Leaves front spots free for others                │
│  • Fits more cars!                                   │
└──────────────────────────────────────────────────────┘
```

### Correctness Argument

```
┌──────────────────────────────────────────────────────┐
│   JOB SEQUENCING CORRECTNESS PROOF SKETCH            │
│                                                      │
│  Theorem: Greedy (sort by profit, place at          │
│           latest slot) gives optimal profit         │
│                                                      │
│  Proof idea:                                         │
│                                                      │
│  1. Suppose greedy solution G differs from           │
│     optimal solution O                               │
│                                                      │
│  2. Both have same number of jobs (both feasible     │
│     with deadlines), but different jobs              │
│                                                      │
│  3. Let job J_i be first where G and O differ       │
│     ├─ G includes job with profit p_g              │
│     ├─ O includes job with profit p_o < p_g        │
│     └─ (Since G sorted by profit, p_g ≥ p_o)       │
│                                                      │
│  4. SWAP: Replace O's job with G's job              │
│     ├─ G's job has deadline that fits in O's sched │
│     ├─ Because G can fit it, O can too              │
│     ├─ But G's job worth more!                       │
│     └─ O' now has ≥ profit than O                   │
│                                                      │
│  5. Continue replacing: O becomes G                  │
│     While maintaining ≥ profit                       │
│                                                      │
│  6. Therefore: G is optimal!                         │
└──────────────────────────────────────────────────────┘
```

---

## 📋 DAY 4 SUMMARY

```
┌──────────────────────────────────────────────────────┐
│           DAY 4: KEY TAKEAWAYS                       │
│                                                      │
│  ✓ Fractional Knapsack: greedy by value/weight      │
│  ✓ Works because CAN take partial items             │
│  ✓ Always fills knapsack to capacity                │
│                                                      │
│  ✓ 0/1 Knapsack: greedy FAILS                        │
│  ✓ Can't divide items → wasted space                │
│  ✓ Needs DP, not greedy                              │
│                                                      │
│  ✓ Job Sequencing: greedy by profit                  │
│  ✓ Schedule at LATEST feasible slot (maximize flex) │
│  ✓ Proven optimal via exchange argument              │
│                                                      │
│  ✓ KEY INSIGHT: Problem structure determines        │
│    whether greedy works!                             │
│  ✓ Fractional vs 0/1 shows this clearly             │
└──────────────────────────────────────────────────────┘
```

---

# 📅 DAY 5: GREEDY IN SYSTEMS & INTEGRATION
## 5 Hours | Real-World Applications and Synthesis

---

## 🎓 PART 1: GREEDY IN NETWORK ALGORITHMS (90 minutes)

### Minimum Spanning Tree (MST): Kruskal's Algorithm

```
┌──────────────────────────────────────────────────────┐
│       MINIMUM SPANNING TREE (MST) PROBLEM            │
│                                                      │
│  Given: Connected weighted undirected graph          │
│                                                      │
│  Goal: Find spanning tree (connects all vertices)    │
│        with MINIMUM total edge weight                │
│                                                      │
│  Application: Road networks, airlines, fiber optics  │
│  ├─ Vertices = cities                                │
│  ├─ Edges = roads/routes with distances              │
│  └─ Find: Minimum total distance connecting all      │
│                                                      │
│  Greedy approach: Kruskal's Algorithm                │
│  ├─ Sort edges by weight (ascending)                │
│  ├─ Add edges one-by-one if no cycle formed         │
│  ├─ Stop when n-1 edges added (tree complete)       │
│  └─ Result: MST!                                     │
└──────────────────────────────────────────────────────┘
```

### Kruskal's Algorithm Visualization

```
Graph with weighted edges:
     ┌─── A ─── B ───┐
     │    3    2     4
     │   /      \   /
     │  C ────── D
     │        1
     
    Edges (sorted by weight):
    1. C-D (1)
    2. A-B (2)
    3. A-C (3)
    4. B-D (4)

Kruskal's steps:
Step 1: Add C-D (weight 1)
        └─ No cycle, so add it
        
        C ─── D (1)
        
Step 2: Add A-B (weight 2)
        └─ No cycle, so add it
        
        A ─── B (2)
        C ─── D (1)
        
Step 3: Add A-C (weight 3)
        └─ No cycle, connects components
        
          A ─── B
          │ (3)
          C ─── D (1)
        
Step 4: Try B-D (weight 4)
        └─ WOULD CREATE CYCLE! Skip it
           All vertices connected anyway
           
Final MST:
  Total weight: 1 + 2 + 3 = 6
  Edges: {C-D, A-B, A-C}
```

### Why Kruskal's Greedy Works

```
┌──────────────────────────────────────────────────────┐
│    WHY KRUSKAL'S GREEDY IS OPTIMAL                   │
│                                                      │
│  Greedy choice property:                             │
│  • Always pick smallest edge that doesn't form cycle │
│  • This edge MUST be in some MST                     │
│  • Why? If we remove it from any MST,                │
│    we disconnect the graph                           │
│  • To reconnect, need another edge                   │
│  • That edge weighs ≥ original (else would be picked)│
│  • So original edge is in some MST                   │
│                                                      │
│  Optimal substructure:                               │
│  • After adding edge, remaining graph is same type   │
│  • Find MST for remaining (forest)                   │
│  • Together = MST for original                       │
│                                                      │
│  Result: ✓ Kruskal's is optimal!                     │
└──────────────────────────────────────────────────────┘
```

### Prim's Algorithm (Alternative Greedy MST)

```
┌──────────────────────────────────────────────────────┐
│         PRIM'S ALGORITHM: ALTERNATIVE MST             │
│                                                      │
│  Different greedy approach (but also optimal!)        │
│                                                      │
│  Strategy: Build MST incrementally starting from     │
│           any vertex, always adding cheapest edge    │
│           connecting tree to outside                 │
│                                                      │
│  Steps:                                               │
│  1. Start with arbitrary vertex (e.g., A)            │
│  2. WHILE tree incomplete:                           │
│     Find minimum-weight edge connecting             │
│     current tree to a vertex not yet in tree         │
│     Add that edge (and new vertex)                   │
│  3. Continue until all vertices in tree              │
│                                                      │
│  Visual:                                              │
│  Start: Tree = {A}                                    │
│         Candidates = edges from A to {B,C,D,...}    │
│         Pick min: say A-B (weight 2)                │
│                                                      │
│  Next: Tree = {A, B}                                 │
│        Candidates = edges from {A,B} to {C,D,...}   │
│        Pick min: say B-C (weight 3)                 │
│                                                      │
│  Continue until tree complete                        │
│                                                      │
│  Comparison with Kruskal's:                          │
│  • Kruskal's: Sort all edges, build forest           │
│  • Prim's: Grow tree from seed vertex                │
│  • Both greedy, both optimal!                        │
│  • Different strategies, same result                 │
└──────────────────────────────────────────────────────┘
```

---

## 🎓 PART 2: GREEDY IN CACHING & SYSTEMS (60 minutes)

### LRU Cache: Eviction Policy

```
┌──────────────────────────────────────────────────────┐
│         LRU CACHE: EVICTION GREEDY                   │
│                                                      │
│  Problem: Cache has limited size                     │
│  When new item arrives, must evict something         │
│  Which item to evict?                                │
│                                                      │
│  LRU Strategy (GREEDY):                              │
│  "Evict Least Recently Used item"                    │
│                                                      │
│  Intuition:                                           │
│  • If item hasn't been used recently,                │
│    likely won't be used soon                         │
│  • So evicting it wastes nothing                     │
│  • Keep recently-used items (more likely to reuse)   │
│  • Local choice (evict LRU) seems good               │
│                                                      │
│  Note: NOT optimal for all access patterns!          │
│  But works well in practice (locality of reference)  │
└──────────────────────────────────────────────────────┘
```

### LRU Cache Example

```
Cache size: 3 items
Access sequence: A, B, C, D, A, E, C, D

Step 1: Access A
        Cache: [A]
        
Step 2: Access B
        Cache: [B, A]
        
Step 3: Access C
        Cache: [C, B, A]
        
Step 4: Access D (cache full!)
        LRU item: A (accessed at time 1)
        Evict A, insert D
        Cache: [D, C, B]
        
Step 5: Access A (NOT in cache - cache miss)
        LRU item: B (accessed at time 2)
        Evict B, insert A
        Cache: [A, D, C]
        
Step 6: Access E (cache full!)
        LRU item: C (accessed at time 3)
        Evict C, insert E
        Cache: [E, A, D]
        
Step 7: Access C (NOT in cache - cache miss)
        LRU item: D (accessed at time 4)
        Evict D, insert C
        Cache: [C, E, A]
        
Step 8: Access D (NOT in cache - cache miss)
        LRU item: E (accessed at time 6)
        Evict E, insert D
        Cache: [D, C, A]

Result: 3 cache misses out of 8 accesses
Performance depends on access pattern!
```

### Why LRU is "Greedy"

```
┌──────────────────────────────────────────────────────┐
│      LRU AS GREEDY HEURISTIC (Not optimal)            │
│                                                      │
│  Local greedy choice: "Evict item least likely      │
│                      to be used soon"                │
│                                                      │
│  Why it's not guaranteed optimal:                    │
│  • Doesn't predict future accesses                   │
│  • Only reacts to past behavior                      │
│  • Could have long gap between current & next use   │
│                                                      │
│  Why it works well in practice:                      │
│  • Real access patterns have locality                │
│  • If used recently, likely to use again soon        │
│  • Temporal locality common in programs              │
│  • Programs access same data repeatedly              │
│                                                      │
│  Optimal (offline) algorithm:                        │
│  "Evict item used furthest in future"                │
│  But requires knowing future! (impossible online)    │
│                                                      │
│  LRU is good online approximation!                   │
└──────────────────────────────────────────────────────┘
```

---

## 🎓 PART 3: WHEN GREEDY FAILS (60 minutes)

### Examples Where Greedy Fails

```
┌──────────────────────────────────────────────────────┐
│     WHEN GREEDY FAILS: IMPORTANT PATTERNS            │
└──────────────────────────────────────────────────────┘

Example 1: Coin Change with Non-Standard Denominations
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

Coins: 1, 3, 4
Target: 6 cents

Greedy (largest first):
Pick 4 → Remaining 2
Pick 1 → Remaining 1
Pick 1 → Remaining 0
Total: 3 coins

Optimal:
Pick 3 → Remaining 3
Pick 3 → Remaining 0
Total: 2 coins ← BETTER!

Why fails: Picking largest coin creates bad remainder


Example 2: Maximum Path in DAG
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

Graph:     A ─10→ B ─1→ C
           └──2──→ C

Greedy (always pick heaviest edge from current):
Start A: Pick 10 → reach B
From B: Pick 1 → reach C
Total: 11

Optimal: 
A → C directly: 2
Wait, that's worse. Let me recalculate...

Actually: A → B (10) → C (1) = 11, which is optimal here.

Let me use better example:
           A ─1→ B ─10→ C
           └──2──→ C

Greedy (always pick locally best):
From A: Pick edge to B (1)? Or to C (2)?
If greedy picks "longest looking" (max remaining potential):
- B path: 1 + potential(10) = promising
- C path: 2 direct
Greedy might pick B→C (1+10=11)

But alternative: A→C (2) direct
For this problem, (11 > 2), so greedy wins.

Real example that fails:
           A ─100→ B ─1→ C
           └──100→ D ─101→ C

Greedy maximizes locally:
Pick A→B (100) → Pick B→C (1) = 101 total

Optimal:
Pick A→D (100) → Pick D→C (101) = 201 total ← BETTER!

Why fails: Greedy doesn't look ahead enough


Example 3: Coloring Graph with Min Colors
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

Graph: Triangle (3 vertices, all connected)
Minimum colors needed: 3

Greedy (color vertices in order):
Vertex 1: Color 1
Vertex 2: Color 2 (can't use 1)
Vertex 3: Color 3 (can't use 1 or 2)
Total: 3 colors ← Optimal by luck

Different graph:
Vertex 1 connects to: 2, 3, 4, 5, 6 (star)
Others don't connect to each other

Minimum colors: 2 (center 1 color, all others another color)

Greedy (color in order 1,2,3,4,5,6):
Vertex 1: Color 1
Vertices 2,3,4,5,6: All connect to 1
Greedy colors them Color 2 (since they don't connect to each other)
Total: 2 colors ← Optimal

But change ordering:
Greedy (in order 6,5,4,3,2,1):
Vertex 6: Color 1
Vertex 5: Color 1 (doesn't connect to 6)
Vertex 4: Color 1 (doesn't connect to 5,6)
... all get color 1 except need to separate from center
Actually this also works...

Real failing example (Welsh-Powell):
Greedy graph coloring with bad vertex ordering
Can use 4 colors when 3 suffice
```

### Pattern: When Greedy Fails

```
┌──────────────────────────────────────────────────────┐
│    RECOGNIZING WHEN GREEDY FAILS                     │
│                                                      │
│  Red flag 1: Problem is NP-hard                      │
│  ├─ 0/1 Knapsack: NP-hard                            │
│  ├─ TSP: NP-hard                                     │
│  ├─ Vertex coloring: NP-hard                         │
│  ├─ Greedy usually fails for these                   │
│  └─ Need DP or heuristics instead                    │
│                                                      │
│  Red flag 2: Local choice conflicts with global      │
│  ├─ Example: Coin change                             │
│  ├─ Greedy coin too large                            │
│  ├─ Creates bad remainder                            │
│  ├─ Other coins are better globally                  │
│  └─ Greedy choice property fails                     │
│                                                      │
│  Red flag 3: Problem lacks optimal substructure      │
│  ├─ After greedy choice, remaining problem          │
│  ├─ is NOT same type or                              │
│  ├─ is NOT independent of greedy choice              │
│  └─ Can't recurse to build optimal                   │
│                                                      │
│  Red flag 4: Problem requires lookahead              │
│  ├─ Optimal choice depends on future                 │
│  ├─ Greedy can't predict future                      │
│  ├─ Example: Maximum path in graph                   │
│  └─ Need DP for lookahead                            │
│                                                      │
│  When to try greedy:                                 │
│  ✓ Problem is polynomial-time (P)                    │
│  ✓ Has greedy choice property (provable!)            │
│  ✓ Has optimal substructure                          │
│  ✓ Can prove or show with examples                   │
│                                                      │
│  When to use DP instead:                             │
│  ✗ Problem is NP-hard                                │
│  ✗ Greedy fails on examples                          │
│  ✗ Overlapping subproblems present                   │
│  ✗ Need to try multiple choices (not just one)       │
└──────────────────────────────────────────────────────┘
```

---

## 🎓 PART 4: APPROXIMATION ALGORITHMS & GREEDY HEURISTICS (60 minutes)

### When Optimal is Hard: Use Greedy Approximation

```
┌──────────────────────────────────────────────────────┐
│   GREEDY AS APPROXIMATION FOR NP-HARD                │
│                                                      │
│  Problem: Many real problems are NP-hard             │
│  ├─ 0/1 Knapsack                                     │
│  ├─ Traveling Salesman Problem (TSP)                │
│  ├─ Set Cover                                        │
│  ├─ Vertex Coloring                                  │
│  └─ No known polynomial-time optimal algorithms      │
│                                                      │
│  Solution: Use APPROXIMATION ALGORITHMS              │
│  ├─ Greedy algorithm runs fast                       │
│  ├─ Gives "good enough" solution (not optimal)       │
│  ├─ Can prove it's within factor of optimal         │
│  └─ Example: "Guarantee ≤ 2× optimal"                │
│                                                      │
│  Practical approach:                                  │
│  ├─ For small inputs: try exact (DP, brute force)    │
│  ├─ For large inputs: use greedy approximation       │
│  └─ Often good enough in practice                    │
└──────────────────────────────────────────────────────┘
```

### Example: Set Cover Problem

```
┌──────────────────────────────────────────────────────┐
│         SET COVER PROBLEM                            │
│                                                      │
│  Given: Universe U of n elements                     │
│         Collection of m sets S₁, S₂, ..., S_m        │
│         Each set covers some elements                │
│                                                      │
│  Goal: Select minimum number of sets such that      │
│        their union equals U (covers all elements)    │
│                                                      │
│  Application: TV channels (sets) that cover           │
│             viewers' interests (elements)            │
│  ├─ Channel A covers interests: {sports, news}       │
│  ├─ Channel B covers interests: {news, drama}        │
│  ├─ Channel C covers interests: {sports, drama}      │
│  └─ Select min channels covering all interests       │
│                                                      │
│  NP-hard: No known polynomial algorithm              │
│                                                      │
│  Greedy Approximation:                               │
│  1. Start with uncovered elements U                  │
│  2. WHILE uncovered elements remain:                 │
│     a. Pick set covering most uncovered elements    │
│     b. Mark its elements as covered                  │
│     c. Add set to solution                           │
│  3. Return solution                                  │
└──────────────────────────────────────────────────────┘
```

### Set Cover Example

```
Universe: {A, B, C, D, E, F}

Sets:
S₁ = {A, B, C}
S₂ = {B, D, E}
S₃ = {E, F}
S₄ = {A, D, F}
S₅ = {C, E, F}

Greedy approach (pick max coverage each step):

Step 1: Which set covers most elements?
├─ S₁: 3 elements {A,B,C}
├─ S₂: 3 elements {B,D,E}
├─ S₃: 2 elements
├─ S₄: 3 elements
├─ S₅: 3 elements
Pick S₁ (or any with 3): covers {A,B,C}
Covered: {A,B,C}
Uncovered: {D,E,F}

Step 2: Which set covers most UNCOVERED?
├─ S₁: 0 new elements (already picked)
├─ S₂: 2 new {D,E}
├─ S₃: 1 new {F}
├─ S₄: 2 new {D,F}
├─ S₅: 2 new {E,F}
Pick S₂ (or S₄, S₅): covers 2 new
Covered: {A,B,C,D,E}
Uncovered: {F}

Step 3: Which set covers most UNCOVERED?
├─ S₃: 1 new {F}
├─ S₄: 1 new {F}
├─ S₅: 1 new {F}
Pick any: S₃
Covered: All
Uncovered: {}

Greedy solution: S₁, S₂, S₃
Total sets: 3

Optimal solution:
S₁ {A,B,C} + S₂ {B,D,E} + S₃ {E,F}? No, just need 2:
S₂ {B,D,E} + S₄ {A,D,F} + ??? need C
Actually: S₁ {A,B,C} + S₂ {B,D,E} covers all but F
No single set covers F without covering others already covered,
so need 3 sets minimum

Greedy matches optimal here!

Theorem: Greedy gives ≤ ln(n) × optimal for set cover
For n=6: ≤ 2.59 × optimal
Usually much closer in practice!
```

---

## 🎓 PART 5: INTEGRATION & PARADIGM SYNTHESIS (60 minutes)

### Greedy Algorithm Design Checklist

```
┌──────────────────────────────────────────────────────┐
│      WHEN TO USE GREEDY: DECISION CHECKLIST          │
│                                                      │
│  ✓ Can I identify a clear local choice?              │
│  ✓ Do I have intuition it leads to global optimum?   │
│  ✓ Can I prove greedy choice property?               │
│    (Does some optimal solution contain this choice?) │
│  ✓ Does optimal substructure hold?                   │
│    (After choice, remaining is same-type problem?)   │
│  ✓ Can I prove it rigorously? Exchange argument?     │
│                                                      │
│  If YES to most → Try greedy                         │
│  If NO to most → Try DP or other paradigm            │
│                                                      │
│  If NP-hard proven → Greedy approximation acceptable │
│  If not proven NP → Could still try greedy           │
└──────────────────────────────────────────────────────┘
```

### Comparing Greedy with Other Paradigms

```
┌─────────────────────────────────────────────────────┐
│  ALGORITHM PARADIGMS: WHEN TO USE EACH              │
├─────────────────────────────────────────────────────┤
│ GREEDY                                              │
│ ├─ Use when: Optimal substructure + greedy choice  │
│ ├─ Examples: Activity selection, Huffman, MST      │
│ ├─ Time: Usually O(n log n) or better              │
│ ├─ Space: Usually O(n)                              │
│ └─ Proof: Exchange argument, stay-ahead            │
│                                                     │
│ DYNAMIC PROGRAMMING                                │
│ ├─ Use when: Overlapping subproblems               │
│ ├─ Examples: 0/1 knapsack, LCS, coin change        │
│ ├─ Time: Often O(n²) to O(n³)                      │
│ ├─ Space: Often O(n²)                              │
│ └─ Proof: Induction on DP recurrence               │
│                                                     │
│ BACKTRACKING                                        │
│ ├─ Use when: Search all possibilities               │
│ ├─ Examples: N-queens, permutations, sudoku        │
│ ├─ Time: O(2ⁿ) worst case (exponential)            │
│ ├─ Space: O(n) recursion depth                      │
│ └─ Proof: Completeness of DFS                      │
│                                                     │
│ DIVIDE & CONQUER                                    │
│ ├─ Use when: Problem breaks into independent parts │
│ ├─ Examples: Merge sort, quick sort, FFT           │
│ ├─ Time: Usually O(n log n)                        │
│ ├─ Space: Often O(n)                               │
│ └─ Proof: Induction on recursion depth             │
└─────────────────────────────────────────────────────┘
```

### Common Mistakes and How to Avoid

```
┌──────────────────────────────────────────────────────┐
│    COMMON GREEDY MISTAKES & FIXES                    │
│                                                      │
│ Mistake 1: Assume greedy works without proving      │
│ ├─ Many problems LOOK greedy but aren't optimal    │
│ ├─ Example: Coin change, 0/1 knapsack               │
│ ├─ Fix: Always prove or test on examples            │
│ └─ Test: Try counterexample before coding           │
│                                                      │
│ Mistake 2: Choosing wrong greedy criterion          │
│ ├─ Example: Sort by weight instead of value/weight │
│ ├─ Different criterion → different result            │
│ ├─ Fix: Understand problem intimately               │
│ └─ Think: What metric should we optimize?           │
│                                                      │
│ Mistake 3: Not handling ties/edge cases             │
│ ├─ When multiple elements have same metric          │
│ ├─ Different choices → potentially different results│
│ ├─ Fix: Ensure algorithm works for all orderings   │
│ └─ Think: Does choice of tie-breaker matter?        │
│                                                      │
│ Mistake 4: Assuming polynomial time means greedy   │
│ ├─ Just because problem is polynomial               │
│ ├─ Doesn't mean greedy works!                       │
│ ├─ Example: Some scheduling problems are polynomial │
│                but need DP                          │
│ ├─ Fix: Prove specific problem has greedy properties│
│ └─ Think: Does this particular problem allow it?    │
│                                                      │
│ Mistake 5: Not considering complexity               │
│ ├─ Greedy trade-off: simple but not always optimal │
│ ├─ DP more powerful but slower                      │
│ ├─ Fix: Consider input size and time limits         │
│ └─ Think: Is correctness or speed more important?   │
└──────────────────────────────────────────────────────┘
```

---

## 📋 DAY 5 & WEEK 12 SUMMARY

```
┌──────────────────────────────────────────────────────┐
│     WEEK 12: COMPLETE SUMMARY                        │
│     GREEDY ALGORITHMS & PROOFS MASTERY               │
└──────────────────────────────────────────────────────┘

DAY 1: GREEDY FUNDAMENTALS
✓ Greedy makes locally optimal choices at each step
✓ NOT always globally optimal (must prove!)
✓ TWO properties needed for correctness:
  1. Optimal substructure
  2. Greedy choice property
✓ Exchange argument: proves greedy works
✓ 5-step proof strategy for correctness

DAY 2: ACTIVITY SELECTION & INTERVALS
✓ Activity Selection: classic greedy problem
✓ Sort by finish time, greedily select non-overlapping
✓ "Greedy stays ahead" proof pattern
✓ Variations: weighted (fails), room assignment (works)
✓ Different problems need different greedy criteria

DAY 3: HUFFMAN CODING
✓ Prefix codes enable unique decoding
✓ Codes represented as binary trees
✓ Cost = Σ(frequency × depth)
✓ Huffman: greedily combine two smallest frequencies
✓ Bottom-up tree construction gives optimal prefix code
✓ O(n log n) time, O(n) space

DAY 4: FRACTIONAL KNAPSACK & JOB SCHEDULING
✓ Fractional Knapsack: greedy by value/weight ratio
✓ Works because CAN take fractions (fills to capacity)
✓ 0/1 Knapsack: greedy FAILS (can't divide)
✓ Job Scheduling: greedy by profit, place at latest slot
✓ Latest slot maximizes flexibility for remaining jobs

DAY 5: REAL-WORLD APPLICATIONS & INTEGRATION
✓ Kruskal's & Prim's: MST via greedy
✓ LRU cache: greedy eviction heuristic (not optimal but good)
✓ When greedy fails: NP-hard problems, no greedy property
✓ Greedy approximation: O(log n) guarantee for set cover
✓ Choose paradigm: greedy fast, DP powerful, backtrack complete

KEY INSIGHTS:
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

1. GREEDY IS POWERFUL BUT DANGEROUS
   • Can solve problems optimally in polynomial time
   • But ONLY if problem has right structure
   • Must verify with proof or counterexamples

2. PROBLEM STRUCTURE DETERMINES APPROACH
   • Same-looking problems need different algorithms
   • Example: Fractional (greedy) vs 0/1 (DP) knapsack
   • Understand problem deeply!

3. PROOF IS ESSENTIAL
   • Never assume greedy works without proving
   • Exchange argument is standard technique
   • "Stay ahead" pattern common in proofs

4. WHEN TO USE:
   • Problem polynomial-time solvable ✓
   • Can identify clear local choice ✓
   • Can prove greedy choice property ✓
   • Can prove optimal substructure ✓
   • Then: Try greedy!

5. REAL-WORLD USE:
   • Some problems: want optimal (greedy/DP)
   • Some problems: want fast approximation (greedy)
   • Some problems: need exact exponential search
   • Choose based on problem needs!

MASTERY CHECKLIST:
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

□ Understand greedy algorithm template
□ Can write exchange argument proofs
□ Know: optimal substructure + greedy choice property
□ Solve activity selection variations
□ Understand Huffman coding completely
□ Know when fractional knapsack greedy works
□ Can prove job scheduling greedy is optimal
□ Recognize MST problems (Kruskal/Prim)
□ Understand LRU cache as greedy heuristic
□ Know patterns where greedy fails
□ Can choose between greedy/DP/backtrack

NEXT STEPS (Week 13):
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

→ Backtracking: Systematic search with pruning
→ Branch & Bound: Optimization via search
→ Amortized Analysis: Cost analysis across operations
→ Integration: When to combine paradigms
```

---

# 📊 WEEK 12 LEARNING PROGRESSION

```
┌──────────────────────────────────────────────────────┐
│         DIFFICULTY PROGRESSION BY DAY                │
│                                                      │
│ Day 1: 🟢 GREEN (Fundamentals)                       │
│ ├─ Greedy concept: intuitive, easy to understand     │
│ ├─ Template and properties: pattern recognition      │
│ └─ Exchange argument: proof technique (challenging)  │
│                                                      │
│ Day 2: 🟡 YELLOW (Application)                       │
│ ├─ Activity selection: clean, canonical problem      │
│ ├─ "Stay ahead" pattern: elegant proof               │
│ └─ Interval variations: deeper understanding         │
│                                                      │
│ Day 3: 🟡 YELLOW (Specialized)                       │
│ ├─ Huffman coding: concrete, visual                  │
│ ├─ Tree construction: bottom-up greedy               │
│ └─ Optimality: exchange argument again               │
│                                                      │
│ Day 4: 🟠 ORANGE (Advanced Applications)             │
│ ├─ Fractional vs 0/1: contrast understanding         │
│ ├─ Job scheduling: optimal slot selection            │
│ └─ Problem structure importance: key insight         │
│                                                      │
│ Day 5: 🔴 RED (Integration & Analysis)               │
│ ├─ MST algorithms: multiple greedy approaches        │
│ ├─ Greedy failures: recognize patterns               │
│ ├─ Approximation: use greedy when optimal hard       │
│ └─ Synthesis: choose right paradigm                  │
│                                                      │
│ MASTERY OUTCOME:                                     │
│ ✓ Understand greedy deeply                           │
│ ✓ Can design greedy algorithms                       │
│ ✓ Can write rigorous proofs                          │
│ ✓ Know limitations and failures                      │
│ ✓ Ready for backtracking & advanced topics!          │
└──────────────────────────────────────────────────────┘
```

---

## 📚 RECOMMENDED STUDY MATERIALS

### Conceptual Resources
- Exchange argument deep-dive: practice transforming optimal solutions
- Visual debugging: draw trees, timelines, priority queues
- Pattern recognition: study multiple greedy problems to see connections

### Practice Approach
1. Study concept first (why and what)
2. Draw diagrams and visualizations
3. Trace through examples step-by-step
4. Identify greedy choice criterion
5. Attempt informal correctness argument
6. Read formal proof
7. Try to write proof independently

### Common Pitfalls to Avoid
- ❌ Don't assume greedy without proof
- ❌ Don't confuse greedy with DP
- ❌ Don't pick wrong optimality criterion
- ❌ Don't skip tie-breaking rules
- ❌ Don't ignore edge cases

---

**Week 12 Complete! Ready for Week 13: Backtracking & Branch & Bound**
