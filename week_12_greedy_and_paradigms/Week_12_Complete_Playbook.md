# 🟧 WEEK 12: GREEDY ALGORITHMS & PROOFS

**Duration:** 5 days | 450 minutes | Algorithm Paradigm Mastery  
**Focus:** Greedy algorithm design, correctness proofs, and when greedy is optimal  
**Prerequisites:** Basic recursion, sorting, algorithm analysis  

---

## 🎯 WEEK 12 LEARNING OUTCOMES

By end of Week 12, you will be able to:

1. **Design greedy algorithms** using template and best practices
2. **Prove greedy correctness** using exchange arguments and induction
3. **Solve interval problems** using greedy selection criteria
4. **Implement Huffman coding** for optimal prefix trees
5. **Analyze trade-offs** between greedy and dynamic programming approaches
6. **Recognize patterns** where greedy is provably optimal

---

---

## 📅 DAY 1: GREEDY FUNDAMENTALS

### ⏱️ Duration: 90 minutes

---

## 🎓 CONCEPT 1: What is Greedy?

### The Core Idea

**Greedy Algorithm:** At each step of solving a problem, make the choice that looks best **at that moment**, without reconsidering earlier choices.

Think of this real-world analogy:

```
Making Change Problem:
Goal: Give change of $2.41 using minimum coins

Coins Available: $1, 25¢, 10¢, 5¢, 1¢

Greedy Approach:
- Take as many $1 coins as possible → 2 coins ($2.00)
- Remaining: $0.41
- Take as many 25¢ coins as possible → 1 coin ($0.25)
- Remaining: $0.16
- Take as many 10¢ coins as possible → 1 coin ($0.10)
- Remaining: $0.06
- Take as many 5¢ coins as possible → 1 coin ($0.05)
- Remaining: $0.01
- Take 1¢ coins → 1 coin ($0.01)

Total: 6 coins
Result: OPTIMAL ✓
```

However, greedy doesn't always work!

```
Counter-Example:
Coins: $1, 6¢, 4¢, 1¢
Make: $0.12

Greedy approach:
- Take 6¢ → 2 coins ($0.12) → DONE
- Total: 2 coins

But there's also:
- Take 4¢ → 3 coins (4¢ + 4¢ + 4¢)

Now try to make $0.13:
Greedy: 6¢ + 6¢ + 1¢ = $0.13 → 3 coins
Optimal: 4¢ + 4¢ + 4¢ + 1¢ = $0.13 → 4 coins

Greedy failed! ✗
```

### Why Greedy is Appealing

✅ **Simple to implement**  
✅ **Fast execution** (often O(n) or O(n log n))  
✅ **Intuitive thinking**  
✅ **When correct, very elegant**  

### Why Greedy is Dangerous

❌ **Can produce suboptimal solutions**  
❌ **Looks correct but isn't**  
❌ **Requires careful proof for each problem**  
❌ **Easy to get wrong in interviews**  

---

## 🎓 CONCEPT 2: When Does Greedy Work?

### Necessary Conditions

For greedy to be guaranteed optimal, a problem must satisfy **TWO KEY PROPERTIES**:

#### 1️⃣ **Optimal Substructure**

An optimal solution to the problem contains optimal solutions to subproblems.

```
Definition:
If OPT(problem) uses greedy choice, then
OPT(subproblem) must also be optimal.

Example (Activity Selection):
If greedy picks activity A, and optimal solution includes A,
then the remaining activities must form an optimal solution 
for the remaining time slots.

Visual:
┌─────────────────────────────────┐
│ Overall Optimal Solution         │
│  ┌──────────────────────────┐    │
│  │ Greedy Choice (Activity) │    │
│  │  ┌──────────────────┐    │    │
│  │  │ Subproblem       │ ← Must be Optimal
│  │  │ (Remaining Time) │    │    │
│  │  └──────────────────┘    │    │
│  └──────────────────────────┘    │
└─────────────────────────────────┘
```

#### 2️⃣ **Greedy Choice Property**

A globally optimal solution can always be arrived at by making a **locally optimal (greedy) choice**.

```
Definition:
There exists a greedy choice such that:
- The choice is feasible (satisfies constraints)
- Making it leads to a subproblem of the same structure
- Combining greedy choice + optimal subproblem = optimal solution

Example (Activity Selection):
Greedy Choice: Pick activity with earliest finish time
Why? Because it leaves the maximum time for remaining activities

Timeline:
┼─────────────────────────────────────────────────────┼
0                          Time                      END

Activity A: ├─────┤ (Finishes early)
Activity B:   ├─────────┤ (Finishes late)

Greedy picks A (finishes earliest)
Remaining time: Maximum! ├─────────────────────────────┤
Now apply greedy recursively...
```

### Visual Summary

```
GREEDY WORKS IF:

┌─────────────────────────────────────────┐
│  OPTIMAL SUBSTRUCTURE                   │
│  (Solution contains optimal subsolutions)│
│              ∧                          │
│              │                          │
│              │ Both must                │
│              │ be true!                 │
│              │                          │
│              ∨                          │
│  GREEDY CHOICE PROPERTY                 │
│  (Greedy choice always safe)            │
└─────────────────────────────────────────┘

If one is missing → Greedy may fail
```

---

## 🎓 CONCEPT 3: Greedy Algorithm Template

### The Standard Pattern

```
GREEDY ALGORITHM TEMPLATE:

┌──────────────────────────────────────────┐
│  1. CHOOSE A SORTING CRITERION           │
│     (How to compare choices)             │
│                                          │
│  2. SORT the input by criterion          │
│                                          │
│  3. ITERATE through sorted input:        │
│     IF choice is feasible:               │
│        SELECT it (commit forever)        │
│     ELSE:                                │
│        SKIP it                           │
│                                          │
│  4. RETURN accumulated selections        │
│                                          │
│  5. PROVE correctness of criterion       │
│     (Most important part!)               │
└──────────────────────────────────────────┘
```

### Generic Algorithm Structure

```
Algorithm Steps:
1. Determine best sorting criterion
2. Sort input by criterion
3. Initialize solution = empty set
4. For each item in sorted order:
     If item is feasible:
         Add item to solution
5. Return solution

Key Insight:
The sorting criterion IS THE ALGORITHM
Choose wisely! It determines everything.
```

---

## 🎓 CONCEPT 4: Exchange Argument (Correctness Proof)

### The Key Proof Technique

**Exchange Argument:** Prove greedy is optimal by showing that any optimal solution can be transformed into the greedy solution without losing optimality.

### How It Works

```
EXCHANGE ARGUMENT TECHNIQUE:

Step 1: Assume there exists an optimal solution OPT
Step 2: If OPT ≠ Greedy solution:
        - Find first position where they differ
        - OPT chose X, Greedy chose G
Step 3: Exchange X ↔ G in OPT
Step 4: Prove the solution is still feasible
Step 5: Prove the solution is still at least as good
Step 6: Result: OPT' is still optimal but closer to Greedy
Step 7: Repeat until OPT = Greedy

Conclusion: Greedy = Optimal
```

### Visual Example: Activity Selection

```
Given Activities with Start-End Times:
A₁: [1, 4]
A₂: [3, 5]
A₃: [5, 7]
A₄: [6, 9]

Greedy Solution (sort by finish time):
Pick A₁ [1,4] ✓
Pick A₃ [5,7] ✓ (earliest finish after A₁)
Total: 2 activities {A₁, A₃}

Suppose OPT = {A₂, A₃} (also 2 activities)

Exchange Argument:
OPT: [A₂[3,5], A₃[5,7]]
Greedy: [A₁[1,4], A₃[5,7]]

Position 1: OPT chose A₂, Greedy chose A₁

Exchange A₂ ↔ A₁:
- A₁ finishes at 4 (before A₂ finishes at 5) ✓
- A₁ doesn't conflict with A₃ ✓
- Remaining problem is identical

Result: OPT' = {A₁, A₃} = Greedy solution

Since OPT was optimal and OPT' is equally good:
Greedy solution is optimal!
```

### Why Exchange Argument is Powerful

```
It proves:
✓ Greedy choice is always safe
✓ Greedy doesn't prevent optimal subproblems
✓ Iterating greedy recovers optimal solution
✓ No need to consider alternative choices
```

---

## 🎓 CONCEPT 5: Proof by Induction

### Alternative to Exchange Argument

```
INDUCTION PROOF STRATEGY:

Base Case:
  n = 1 (single element)
  Greedy trivially optimal

Inductive Step:
  Assume: Greedy is optimal for n-1 elements
  Prove: Greedy is optimal for n elements
  
  Method:
  1. Greedy chooses element g₁
  2. Remove g₁, leaving n-1 elements
  3. By induction, greedy is optimal on remaining
  4. Since g₁ was best choice, combined solution is optimal
  5. Therefore greedy is optimal for n elements

Conclusion:
  By induction, greedy is optimal for all n
```

### Why Both Work

```
Exchange Argument:        Induction:
↓                         ↓
Shows optimal can be      Shows optimal
transformed into greedy   must be greedy

Both valid, choose based on problem structure
```

---

## 📊 Visual: Greedy Decision Tree

```
At each step, greedy picks THE BEST option:

Level 1:        ┌─ Choice 1 (BEST)
                ├─ Choice 2
                ├─ Choice 3
                └─ Choice 4

Level 2:        ┌─ Choice 1.1 (BEST)
                └─ Choice 1.2

Level 3:        └─ Choice 1.1.1 (BEST)

Result: Path down BEST choices at each level
        (Not necessarily globally optimal)

However, IF problem satisfies greedy choice property:
Result: Path IS globally optimal!
```

---

## 🔑 KEY INSIGHTS FOR DAY 1

| Concept | Key Point |
|---------|-----------|
| **Greedy Essence** | Make locally best choice, commit forever |
| **When It Works** | Must have optimal substructure + greedy choice property |
| **Must Prove** | Cannot assume greedy is optimal - must prove for each problem |
| **Exchange Argument** | Shows any optimal can be transformed to greedy |
| **Induction Proof** | Shows greedy must be optimal by cases |
| **Template** | Sort by criterion → iterate → select greedily → return |

---

---

## 📅 DAY 2: ACTIVITY SELECTION & INTERVAL PROBLEMS

### ⏱️ Duration: 90 minutes

---

## 🎓 CONCEPT 1: The Activity Selection Problem

### Problem Statement

```
ACTIVITY SELECTION PROBLEM:

Input:
  - n activities
  - Each activity i has:
    - Start time: sᵢ
    - End time: fᵢ (finish time)
  - Constraint: Must use same resource (classroom, room, etc.)

Output:
  - Select MAXIMUM NUMBER of non-overlapping activities
  - Activities don't overlap if: fᵢ ≤ sⱼ (one ends before other starts)

Question:
  Which activities should we select?
```

### Example

```
Activities:
┌────────┬───────┬───────┐
│ Act ID │ Start │ Finish│
├────────┼───────┼───────┤
│   1    │   1   │   4   │
│   2    │   3   │   5   │
│   3    │   0   │   6   │
│   4    │   5   │   7   │
│   5    │   3   │   8   │
│   6    │   5   │   9   │
│   7    │   6   │  10   │
│   8    │   8   │  11   │
└────────┴───────┴───────┘

Timeline Visualization:
Time:    0  1  2  3  4  5  6  7  8  9  10 11
Act 1:   ├─────┤
Act 2:        ├─────┤
Act 3:   ├──────────┤
Act 4:                 ├─────┤
Act 5:        ├──────────────┤
Act 6:                 ├──────────┤
Act 7:                    ├──────────┤
Act 8:                          ├──────────┤

Goal: Find the maximum set of non-overlapping activities
```

---

## 🎓 CONCEPT 2: Naive Approach (Wrong!)

### Why Naive Fails

```
Attempt 1: Pick by earliest start time

Time:    0  1  2  3  4  5  6  7  8  9  10 11
Act 3:   ├──────────┤                        ← Earliest start: 0
Act 2:        ├─────┤                        Can't pick (overlaps)
Act 1:   ├─────┤                             Can't pick (overlaps)
Act 4:                 ├─────┤                Pick (no overlap)
Act 8:                          ├──────────┤  Pick (no overlap)

Result: 3 activities {3, 4, 8}

Attempt 2: Pick by shortest duration

Act 1: [1,4] = 3 units (short)
Act 2: [3,5] = 2 units (SHORTER)
Act 4: [5,7] = 2 units
...

This is messy and doesn't guarantee optimal.

Both approaches fail on some examples.

WHY? They don't consider the fundamental constraint:
    "I want to leave maximum time for remaining activities"
```

---

## 🎓 CONCEPT 3: Greedy Solution (Correct!)

### The Key Insight

```
OPTIMAL CRITERION: Sort by EARLIEST FINISH TIME

Why? Because:
  - An activity that finishes early leaves more time
  - More time = more room for additional activities
  - Maximizes remaining time for subproblem

Analogy:
  You have a 4-hour class period
  Multiple activities can fit
  
  Activity A: 2:00 PM - 2:30 PM (finishes earliest)
  Activity B: 2:00 PM - 3:00 PM (finishes later)
  
  If you pick A: remaining time = 2:30 - 4:00 = 1.5 hours
  If you pick B: remaining time = 3:00 - 4:00 = 1 hour
  
  Greedy picks A (leaves more time for others)
```

### Algorithm

```
ACTIVITY_SELECTION(activities):
  1. Sort activities by finish time (earliest first)
  2. selected = {first activity}
  3. last_finish = finish time of first activity
  4. For each remaining activity i:
       If start time of i ≥ last_finish:
           Add activity i to selected
           Update last_finish
  5. Return selected

Time Complexity: O(n log n) for sorting
                O(n) for selection
                Total: O(n log n)
```

### Step-by-Step Example

```
Activities sorted by finish time:

┌────────┬───────┬───────┐
│ Act ID │ Start │ Finish│
├────────┼───────┼───────┤
│   1    │   1   │   4   │  ← Sort key = finish time
│   2    │   3   │   5   │
│   4    │   5   │   7   │
│   6    │   5   │   9   │
│   7    │   6   │  10   │
│   5    │   3   │   8   │
│   3    │   0   │   6   │
│   8    │   8   │  11   │
└────────┴───────┴───────┘

Greedy Selection Process:

Step 1: Select Activity 1 [1, 4]
        Last finish = 4
        Selected = {1}
        
Time:    0  1  2  3  4  5  6  7  8  9  10 11
Act 1:   ├─────┤ ✓ SELECTED

Step 2: Check Activity 2 [3, 5]
        Start = 3, Last finish = 4
        3 ≥ 4? NO → Skip (overlaps)

Step 3: Check Activity 4 [5, 7]
        Start = 5, Last finish = 4
        5 ≥ 4? YES → Select
        Last finish = 7
        Selected = {1, 4}

Time:    0  1  2  3  4  5  6  7  8  9  10 11
Act 1:   ├─────┤ ✓
Act 4:                 ├─────┤ ✓

Step 4: Check Activity 6 [5, 9]
        Start = 5, Last finish = 7
        5 ≥ 7? NO → Skip (overlaps)

Step 5: Check Activity 7 [6, 10]
        Start = 6, Last finish = 7
        6 ≥ 7? NO → Skip (overlaps)

Step 6: Check Activity 5 [3, 8]
        Start = 3, Last finish = 7
        3 ≥ 7? NO → Skip (overlaps)

Step 7: Check Activity 3 [0, 6]
        Start = 0, Last finish = 7
        0 ≥ 7? NO → Skip (overlaps)

Step 8: Check Activity 8 [8, 11]
        Start = 8, Last finish = 7
        8 ≥ 7? YES → Select
        Last finish = 11
        Selected = {1, 4, 8}

Time:    0  1  2  3  4  5  6  7  8  9  10 11
Act 1:   ├─────┤ ✓
Act 4:                 ├─────┤ ✓
Act 8:                          ├──────────┤ ✓

FINAL RESULT: 3 activities {1, 4, 8}
```

---

## 🎓 CONCEPT 4: Proof of Correctness

### Exchange Argument for Activity Selection

```
THEOREM: Greedy algorithm for activity selection is optimal.

PROOF (by exchange argument):

Step 1: Let G = greedy solution (sorted by finish time)
        Let O = some optimal solution
        Assume G ≠ O

Step 2: Let k = first position where G and O differ
        G has activities g₁, g₂, ..., gₖ, ...
        O has activities o₁, o₂, ..., oₖ, ...
        where g₁...gₖ₋₁ = o₁...oₖ₋₁ but gₖ ≠ oₖ

Step 3: Since greedy chose gₖ, it has earliest finish time
        among all activities compatible with g₁...gₖ₋₁
        
        Therefore: finish(gₖ) ≤ finish(oₖ)

Step 4: Replace oₖ with gₖ in O:
        O' = {o₁, ..., oₖ₋₁, gₖ, oₖ₊₁, ...}

Step 5: Is O' valid (no overlaps)?
        - o₁...oₖ₋₁ don't overlap (by assumption)
        - gₖ doesn't overlap with oₖ₋₁ 
          (because finish(gₖ) ≤ finish(oₖ) < start(oₖ₊₁))
        - oₖ₊₁... don't overlap with gₖ
          (because finish(gₖ) ≤ finish(oₖ), and oₖ₊₁ doesn't overlap with oₖ)

Step 6: |O'| = |O| (same size, just replaced one activity)
        O was optimal, O' is valid and same size
        Therefore O' is also optimal

Step 7: O' matches G in first k positions
        Can repeat this process until O = G
        
        Therefore: G is optimal

CONCLUSION: Greedy algorithm produces an optimal solution. ✓
```

### Why Finish Time Works

```
KEY INSIGHT:

Among all compatible choices, picking the one that finishes
EARLIEST is always safe because:

1. It leaves maximum time for remaining activities
2. Whatever we could fit after a later-finishing activity,
   we can also fit after an earlier-finishing one
3. We might fit MORE after an earlier-finishing activity

Visual Proof:
┌─────────────────────────────────────┐
│ Remaining Time:                      │
├─────────────────────────────────────┤

Option A (Greedy):  ├──┤
                       └──────────────┘ More room

Option B (Not):        ├──────┤
                              └────┘ Less room

A finishes earlier → leaves more time for remaining activities
```

---

## 🎓 CONCEPT 5: Interval Problem Variations

### Variation 1: Minimize Resources Needed

```
PROBLEM: Minimum number of rooms/resources for all activities

Example:
Activities: [1,4], [2,5], [3,6], [7,9]

Visual:
Time: 1  2  3  4  5  6  7  8  9
      ├─────┤
         ├─────┤
            ├─────┤
                     ├─────┤

How many rooms needed?
At time 3: Activities [1,4], [2,5], [3,6] all running
→ Need 3 rooms

Solution Approach (NOT greedy by finish time):
  Use sweep line algorithm:
  1. Create events: +1 for start, -1 for end
  2. Sort events by time
  3. Track running count of activities
  4. Maximum count = rooms needed

Why not greedy by finish time?
  Because we're not selecting a subset,
  we're finding resource constraints
```

### Variation 2: Maximum Weight Selection

```
PROBLEM: Select non-overlapping activities with maximum TOTAL WEIGHT

Example:
Activity 1 [1,4]: weight = 100
Activity 2 [3,5]: weight = 50
Activity 3 [5,7]: weight = 80
Activity 4 [6,9]: weight = 70

Greedy by finish time:
  Pick 1 [1,4] weight 100
  Pick 3 [5,7] weight 80
  Total: 180

But what about:
  Pick 2 [3,5] weight 50
  Pick 4 [6,9] weight 70
  Total: 120

Greedy gives 180, others give less...
Is it always optimal?

Actually, NO! Greedy doesn't work here.

Counter-example:
Activity 1 [1,2]: weight = 100
Activity 2 [2,10]: weight = 200

Greedy by finish time picks 1 (finishes earlier):
  Result: 100

Optimal is to pick 2:
  Result: 200

Why doesn't greedy work?
  Because greedy choice property fails
  Picking earliest finish doesn't guarantee
  you leave room for high-weight activities

Solution: Use DYNAMIC PROGRAMMING instead of greedy
```

---

## 📊 Visual: Interval Problem Decision Tree

```
WHICH ALGORITHM TO USE FOR INTERVAL PROBLEMS?

┌─────────────────────────────────────┐
│ What are we optimizing?             │
└────────────────┬────────────────────┘
                 │
      ┌──────────┴──────────┐
      │                     │
      ▼                     ▼
┌─────────────┐    ┌─────────────────┐
│ COUNT       │    │ WEIGHT/VALUE    │
│ (Select max │    │ (Maximize total │
│  number)    │    │  weight)        │
└─────────────┘    └─────────────────┘
      │                     │
      ▼                     ▼
  GREEDY ✓            DYNAMIC PROG ✓
  Sort by             (Greedy fails)
  finish time
```

---

## 🔑 KEY INSIGHTS FOR DAY 2

| Concept | Key Point |
|---------|-----------|
| **Activity Selection** | Choose by earliest finish time |
| **Why Finish Time?** | Leaves maximum time for remaining |
| **Exchange Proof** | Shows early-finish choice always safe |
| **Greedy Works** | Because optimal substructure + greedy choice property |
| **Variations** | Different problems need different approaches |
| **Weight Selection** | Greedy FAILS (use DP instead) |

---

---

## 📅 DAY 3: HUFFMAN CODING & OPTIMAL TREES

### ⏱️ Duration: 90 minutes

---

## 🎓 CONCEPT 1: Prefix Codes and Huffman Coding

### Why Prefix Codes Matter

```
PROBLEM: Encode text efficiently

Example: Encoding the word "HELLO"

ASCII encoding (standard):
H = 01001000
E = 01000101
L = 01001100
O = 01001111

Total: 5 characters × 8 bits = 40 bits

But can we do better?

Key Insight: Some letters appear more frequently!
In English text:
  E appears very frequently
  Q appears very rarely

GREEDY IDEA:
  Give short codes to frequent letters
  Give long codes to rare letters

Example Custom Code:
H = 01
E = 0
L = 10
O = 11

Text "HELLO" = 01 + 0 + 10 + 10 + 11
             = 0 1 0 10 10 11
             
But wait... ambiguity!
  "010" could be:
    - H + E        = 01 + 0
    - E + H + E    = 0 + 1 + 0

SOLUTION: Use PREFIX CODES
(No code is a prefix of another code)

Example:
H = 00
E = 01
L = 10
O = 11

Now "00 01 10 10 11" = "HELLO" unambiguously!

Better example:
For frequently used letters, shorter codes:

E (frequent) = 0       (1 bit)
A (frequent) = 10      (2 bits)
X (rare)     = 11000   (5 bits)
Z (rare)     = 11001   (5 bits)
...

HUFFMAN CODING: Finds optimal prefix code
                 (minimum total bits for text)
```

### Visual: Prefix Code Property

```
PREFIX CODE CONSTRAINT: No code is prefix of another

Valid:
├─ 0
├─ 10
├─ 110
├─ 111

Invalid (10 is prefix of 101):
├─ 0
├─ 10  ← This is a prefix...
├─ 101 ← ...of this!

Visual Tree (Valid):
        root
        /  \
       0    1
      /    / \
     A    1   1
         / \ / \
        0  1 0  1
        B  C D  E

Reading codes:
  A: follow left  → 0
  B: follow right, left, left → 10
  C: follow right, left, right → 101
  D: follow right, right, left → 110
  E: follow right, right, right → 111

No code is a prefix of another ✓
```

---

## 🎓 CONCEPT 2: Huffman Coding Algorithm

### The Key Greedy Insight

```
GREEDY CHOICE:
"Build the tree BOTTOM-UP"
"Combine the TWO LEAST-FREQUENT characters first"

Why?
- Least frequent characters get longest codes
- Most frequent characters get shortest codes
- Overall bits minimized

Analogy:
You have 4 items with weights:
A = 5 kg
B = 7 kg
C = 8 kg
D = 10 kg

Want to arrange them in tree to minimize depth for heavy items

Put heavy items near root (short path)
Put light items far from root (long path)

So combine light items first!
```

### Algorithm Step-by-Step

```
HUFFMAN_CODING(characters, frequencies):

Input: Set of characters with their frequencies

1. CREATE LEAF NODE for each character
   Initial forest: {A:5, B:7, C:8, D:10}

2. WHILE more than 1 tree in forest:
     a) SELECT the two trees with minimum frequency
     b) CREATE a new internal node
        Frequency = sum of two selected frequencies
     c) MAKE first tree left child, second tree right child
     d) REMOVE the two trees from forest
     e) ADD new tree to forest

3. ROOT of remaining tree = Huffman tree

4. ASSIGN codes:
     Left child = 0
     Right child = 1
     Path from root to leaf = code for that character
```

### Example Walkthrough

```
Characters and frequencies:
A: 5
B: 7
C: 8
D: 10

Step 1: Create leaf nodes
┌───┐  ┌───┐  ┌───┐  ┌────┐
│ A │  │ B │  │ C │  │ D  │
│ 5 │  │ 7 │  │ 8 │  │ 10 │
└───┘  └───┘  └───┘  └────┘

Step 2: Combine two smallest (A:5, B:7)
                ┌────┐
                │ 12 │
               /    \
            A(5)    B(7)
            
Remaining forest:
            ┌────┐  ┌───┐  ┌────┐
            │ 12 │  │ C │  │ D  │
            │AB │  │ 8 │  │ 10 │
            └────┘  └───┘  └────┘

Step 3: Combine two smallest (C:8, AB:12)
                    ┌────┐
                    │ 20 │
                   /    \
                C(8)    AB(12)
                        /    \
                      A(5)   B(7)
                      
Remaining forest:
            ┌────┐  ┌────┐
            │ 20 │  │ D  │
            │CAB │  │ 10 │
            └────┘  └────┘

Step 4: Combine two smallest (D:10, CAB:20)
                        ┌────┐
                        │ 30 │
                       /    \
                    D(10)   CAB(20)
                            /     \
                        C(8)      AB(12)
                                 /     \
                              A(5)    B(7)

Remaining forest: Just one tree!

HUFFMAN TREE:
                        root
                        /  \
                      0    1
                     /      \
                    D        *
                           /   \
                          0     1
                         /       \
                        C        *
                               /   \
                              0     1
                             /       \
                            A         B

Final Codes:
D = 0       (1 bit)
C = 10      (2 bits)
A = 110     (3 bits)
B = 111     (3 bits)

Total bits for text "AABCDDD":
A(2) + A(2) + B(3) + C(2) + D(1) + D(1) + D(1)
= 2 + 2 + 3 + 2 + 1 + 1 + 1
= 12 bits

With fixed 3-bit codes: 7 × 3 = 21 bits
Huffman saves: 21 - 12 = 9 bits! ✓
```

---

## 🎓 CONCEPT 3: Why Huffman is Optimal

### Correctness Proof (Exchange Argument)

```
THEOREM: Huffman coding produces optimal prefix code

PROOF:

Lemma 1: Optimal tree has least-frequent characters at deepest levels
Proof: If not, swap them upward. Same or shorter total cost.

Lemma 2: In optimal tree, two least-frequent characters
        are siblings at deepest level
Proof: By Lemma 1, they're deep. If not siblings,
       swap one with parent's sibling. Same or shorter cost.

Main Proof:
Let G = Greedy (Huffman) solution
Let O = Some optimal solution

Case 1: G = O
        Then G is optimal. ✓

Case 2: G ≠ O
        By Lemma 2, O has two least-frequent chars as siblings
        Huffman chooses them as siblings (Lemma 2)
        
        Remove these two characters from both trees.
        Subproblem: {remaining chars + combined char}
        
        Both G and O must solve subproblem optimally
        (otherwise we could improve them)
        
        Therefore: G_subproblem = O_subproblem
        Therefore: G = O (contradiction)
        
        So Case 2 is impossible.

CONCLUSION: G = O, so Huffman is optimal. ✓
```

### Visual Proof Intuition

```
KEY INSIGHT: If we combine the two least-frequent characters,
             we DON'T harm optimality.

Why?
- They appear least, so their code length affects total least
- By combining them first, we force them deeper in tree
- Longer codes for low-frequency chars = good trade-off

Intuitive Trade-off:
┌────────────────────────────────────────────┐
│ If we use long codes for rare letters      │
│ We can use short codes for common letters  │
│ Net effect: OVERALL SHORTER                │
└────────────────────────────────────────────┘
```

---

## 🎓 CONCEPT 4: Optimality of Prefix Codes

### Why Huffman Outperforms Other Schemes

```
COMPARISON:

Fixed-length code (e.g., ASCII):
A = 00
B = 01
C = 10
D = 11
(All 2 bits regardless of frequency)

Variable-length (Non-prefix):
A = 0
B = 01     ← Problem: B is prefix of 011, etc.
C = 011
D = 0111

Variable-length (Huffman):
D = 0      (Most frequent = shortest)
C = 10
B = 110
A = 111    (Least frequent = longest)

For text heavy in D, lighter in A:
Fixed:     40 bits (always)
Huffman:   Better depending on distribution
```

---

## 📊 Visual: Huffman Algorithm Decision Tree

```
HUFFMAN CODING ALGORITHM:

Input: Character frequencies
   │
   ▼
┌─────────────────────────────┐
│ Create leaf for each char   │
└──────────────┬──────────────┘
               │
               ▼
    ┌─────────────────────┐
    │ While > 1 tree:     │
    │  1. Pick 2 minimum  │
    │  2. Combine them    │
    │  3. Insert back     │
    └──────────────┬──────┘
                   │
                   ▼
         ┌─────────────────┐
         │ Root = Huffman  │
         │ tree            │
         └────────┬────────┘
                  │
                  ▼
        ┌──────────────────┐
        │ Assign codes:    │
        │ Left = 0         │
        │ Right = 1        │
        │ Path = code      │
        └──────────────────┘
```

---

## 🔑 KEY INSIGHTS FOR DAY 3

| Concept | Key Point |
|---------|-----------|
| **Prefix Code** | No code is prefix of another |
| **Huffman Idea** | Combine least-frequent first |
| **Greedy Choice** | Always safe for optimal substructure |
| **Why Optimal** | Least-frequent chars get longest codes |
| **Exchange Proof** | Swapping doesn't harm optimality |
| **Application** | Data compression, file formats |

---

---

## 📅 DAY 4: FRACTIONAL KNAPSACK & SCHEDULING

### ⏱️ Duration: 90 minutes

---

## 🎓 CONCEPT 1: The Knapsack Problem

### Two Variations: 0/1 vs Fractional

```
KNAPSACK PROBLEM (General):
- Knapsack capacity: W (e.g., 100 kg)
- n items, each with:
  - Weight: wᵢ
  - Value: vᵢ
- Goal: Maximize total value while staying within capacity

VARIATION 1: 0/1 Knapsack (Discrete)
- Can either TAKE the entire item or LEAVE it
- Cannot take partial item
- Example: selecting art pieces (can't take half a painting)

VARIATION 2: Fractional Knapsack (Continuous)
- Can take ANY FRACTION of an item
- Example: sand, gold, grain (can take half a bag)

KEY DIFFERENCE:
- 0/1: DYNAMIC PROGRAMMING required
- Fractional: GREEDY algorithm works! ✓
```

### Example Comparison

```
Capacity: W = 10 kg

Items:
┌─────┬────────┬───────┬────────────┐
│ ID  │ Weight │ Value │ Value/Wt   │
├─────┼────────┼───────┼────────────┤
│  1  │   6    │  30   │    5.0     │
│  2  │   3    │  14   │   4.67     │
│  3  │   4    │  16   │    4.0     │
└─────┴────────┴───────┴────────────┘

0/1 KNAPSACK (choose whole items):
Option 1: Take items 1 + 2 = 9 kg, value = 44
Option 2: Take items 1 + 3 = 10 kg, value = 46 ✓ OPTIMAL
Option 3: Take items 2 + 3 = 7 kg, value = 30

GREEDY by value/weight fails here!
Greedy would pick: 1 (5.0) + 2 (4.67) = 9 kg, value = 44
But optimal is 1 + 3 = value 46

FRACTIONAL KNAPSACK (take fractions):
Greedy by value/weight:
1. Take item 1 (value/wt = 5.0): full = 6 kg, value = 30
2. Remaining capacity: 4 kg
   Take item 2 (value/wt = 4.67): 
   Can't fit all 3 kg, so take 4/3 kg worth
   Fraction = 4/3 ÷ 3 = 4/9 of item 2
   Value = (4/9) × 14 = 6.22

Total: 30 + 6.22 = 36.22 value in 10 kg ✓ OPTIMAL

GREEDY WORKS FOR FRACTIONAL!
```

---

## 🎓 CONCEPT 2: Fractional Knapsack Algorithm

### The Greedy Approach

```
FRACTIONAL_KNAPSACK(items, capacity):

1. Calculate value/weight ratio for each item

2. Sort items by ratio in DESCENDING order
   (highest value per unit weight first)

3. For each item in sorted order:
   IF weight of item ≤ remaining capacity:
       Take entire item
       Reduce remaining capacity
   ELSE:
       Take fractional part
       remaining_value = (remaining capacity / weight) × value
       Add to total
       Break (knapsack full)

4. Return total value

Time Complexity: O(n log n) for sorting
                O(n) for selection
                Total: O(n log n)
```

### Step-by-Step Example

```
Items:
┌─────┬────────┬───────┬────────────┐
│ ID  │ Weight │ Value │ Value/Wt   │
├─────┼────────┼───────┼────────────┤
│  A  │   5    │  60   │   12.0     │ ← Highest ratio
│  B  │   3    │  30   │   10.0     │
│  C  │   4    │  20   │    5.0     │
└─────┴────────┴───────┴────────────┘

Capacity = 10 kg

Step 1: Sort by value/weight (descending)
        A (12.0), B (10.0), C (5.0)

Step 2: Take item A
        Weight: 5 kg (≤ capacity 10)
        Take all of A
        Total value: 60
        Remaining capacity: 5 kg
        
Step 3: Take item B
        Weight: 3 kg (≤ capacity 5)
        Take all of B
        Total value: 60 + 30 = 90
        Remaining capacity: 2 kg

Step 4: Take item C
        Weight: 4 kg (> capacity 2)
        Can't take all
        Take fraction: 2/4 = 0.5 of C
        Value from C: 0.5 × 20 = 10
        Total value: 90 + 10 = 100
        Remaining capacity: 0 kg (full!)

RESULT:
- Take 5 kg of A (entire item)
- Take 3 kg of B (entire item)
- Take 2 kg of C (half the item)
- Total weight: 10 kg
- Total value: 100

Visual:
Knapsack [5 kg A | 3 kg B | 2 kg C]
         ├──────┼─────┼───┤
         |  60  | 30  | 10| = 100 value
         └──────┴─────┴───┘
```

---

## 🎓 CONCEPT 3: Why Greedy Works for Fractional

### Exchange Argument

```
THEOREM: Greedy by value/weight ratio is optimal for fractional knapsack

PROOF (exchange argument):

Let G = Greedy solution (by ratio)
Let O = Some optimal solution

Suppose G ≠ O:

Case 1: Different items selected
        G includes item i, O doesn't
        i has highest ratio among non-selected in O
        
        O includes item j instead
        Since i's ratio > j's ratio: value(i)/weight(i) > value(j)/weight(j)
        
        Replace j with i in O:
        Same weight, higher value → O' is better than O
        Contradiction (O was optimal)

Case 2: Same items, different fractions
        Let i be first item where they differ
        G takes fraction x of i
        O takes fraction y of i, where x > y
        
        O must have more of some lower-ratio item j
        Swap portion of j for portion of i in O:
        - Remove 1 unit of j: lose value(j)/weight(j)
        - Add 1 unit of i: gain value(i)/weight(i)
        - Since i's ratio is higher: net gain!
        - O' is better than O
        Contradiction
        
Therefore: G = O (greedy is optimal)

KEY: Fractional allows perfect swapping at any granularity
     0/1 doesn't (can't partially swap items)
```

### Visual: Why Fractional Allows Greedy

```
FRACTIONAL KNAPSACK:

Can swap at any granularity:
┌──────────────────────────────┐
│ Swap small amount of item B  │
│ ↓                            │
│ For small amount of item A   │
│ (A has higher ratio)         │
└──────────────────────────────┘

Even if current solution has mostly B,
can improve by replacing with A.

0/1 KNAPSACK:

Can only swap whole items:
┌──────────────────────────────┐
│ If solution has item B       │
│ And we want item A           │
│ But weights don't match      │
│ → Can't swap!                │
└──────────────────────────────┘

Example: A = 5 kg, B = 3 kg
If O has B (3 kg) and G wants A (5 kg),
can't just swap (different weights)

May need to remove B and C to fit A.
This creates complex trade-offs
→ DP needed
```

---

## 🎓 CONCEPT 4: Job Sequencing with Deadlines

### Problem Definition

```
JOB SEQUENCING WITH DEADLINES:

Input:
- n jobs
- Each job i has:
  - Profit: pᵢ (value if completed)
  - Deadline: dᵢ (must finish by this time)
  - Duration: 1 time unit (all jobs same length)

Constraint:
- Can do 1 job per time unit
- Must finish job before deadline

Output:
- Schedule that maximizes total profit

Example:
┌────────┬────────┬──────────┐
│ Job ID │ Profit │ Deadline │
├────────┼────────┼──────────┤
│   1    │  100   │    2     │
│   2    │   80   │    3     │
│   3    │   60   │    2     │
│   4    │   50   │    4     │
└────────┴────────┴──────────┘

Possible schedules:
Schedule 1: Job 1, Job 2, Job 4
            Finish times: 1, 2, 3
            Profit: 100 + 80 + 50 = 230

Schedule 2: Job 1, Job 3, Job 2
            Finish times: 1, 2, 3
            Profit: 100 + 60 + 80 = 240 ✓ Better

Goal: Find scheduling that maximizes profit
```

---

## 🎓 CONCEPT 5: Greedy Solution for Job Sequencing

### Algorithm

```
JOB_SEQUENCING(jobs):

1. Sort jobs by PROFIT in DESCENDING order
   (highest profit first)

2. Initialize: time_slots = [empty, empty, ..., empty] for 1 to max_deadline

3. For each job in sorted order:
   a) Find latest available slot at or before job's deadline
   b) If found:
       Schedule job in that slot
       Add profit to total
   c) Else:
       Skip this job (can't meet deadline)

4. Return total profit and schedule

Time Complexity: O(n²) worst case (finding slots)
                Can optimize to O(n log n) with disjoint set
```

### Step-by-Step Example

```
Jobs sorted by profit (descending):
┌────────┬────────┬──────────┐
│ Job ID │ Profit │ Deadline │
├────────┼────────┼──────────┤
│   1    │  100   │    2     │ ← Highest profit
│   2    │   80   │    3     │
│   4    │   50   │    4     │
│   3    │   60   │    2     │
└────────┴────────┴──────────┘

Time slots (1 to 4):
Slot: [ _ , _ , _ , _ ]
       1   2   3   4

Step 1: Try Job 1 (profit 100, deadline 2)
        Available slot at or before time 2?
        Time 2 is available ✓
        Schedule: [ _ , 1, _ , _ ]
        Profit: 100

Step 2: Try Job 2 (profit 80, deadline 3)
        Available slot at or before time 3?
        Time 3 is available ✓
        Schedule: [ _ , 1, 2, _ ]
        Profit: 100 + 80 = 180

Step 3: Try Job 4 (profit 50, deadline 4)
        Available slot at or before time 4?
        Time 4 is available ✓
        Schedule: [ _ , 1, 2, 4]
        Profit: 100 + 80 + 50 = 230

Step 4: Try Job 3 (profit 60, deadline 2)
        Available slot at or before time 2?
        Time 2 has Job 1 ✗
        Time 1 is available ✓
        Schedule: [3, 1, 2, 4]
        Profit: 230 + 60 = 290

FINAL SCHEDULE:
Slot 1: Job 3 (profit 60)
Slot 2: Job 1 (profit 100)
Slot 3: Job 2 (profit 80)
Slot 4: Job 4 (profit 50)

Total Profit: 290
All deadlines met: ✓
```

---

## 🎓 CONCEPT 6: Why Greedy Works Here

### Intuitive Explanation

```
KEY INSIGHT:
"Schedule highest-profit jobs first,
 fit them into latest available slots"

Why latest slots?
- Leaves earlier slots for future jobs
- Maximum flexibility for remaining jobs

Why profit order?
- If a job can't fit (deadline too early),
  better to skip low-profit job than high-profit
- High-profit jobs are "worth the effort"
- Even if we can't fit all, we get maximum profit

Visual Intuition:
┌─────────────────────────────────┐
│ Greedy picks high-profit jobs   │
│ Places them in latest valid     │
│ position to leave room for rest │
└─────────────────────────────────┘

Compare: Early scheduling
┌─────────────────────────────────┐
│ If we place high-profit early   │
│ We might block later slots      │
│ Can't fit other profitable jobs │
└─────────────────────────────────┘
```

---

## 📊 Visual Comparison: Greedy Applications

```
WHEN TO USE GREEDY:

Fractional Knapsack:     ✓ Greedy works
Activity Selection:       ✓ Greedy works
Job Sequencing:          ✓ Greedy works
Huffman Coding:          ✓ Greedy works

0/1 Knapsack:            ✗ Greedy fails → DP
Longest Path:            ✗ Greedy fails → DP
Maximum Spanning Tree:   ✓ Greedy works

KEY: Must prove for each problem!
```

---

## 🔑 KEY INSIGHTS FOR DAY 4

| Concept | Key Point |
|---------|-----------|
| **Fractional Knapsack** | Greedy by value/weight ratio works |
| **0/1 Knapsack** | Greedy fails, need dynamic programming |
| **Job Sequencing** | Greedy by profit with latest-slot placement |
| **Exchange Proof** | Shows greedy choice always safe |
| **Key Difference** | Fractional allows continuous swapping |
| **Why Sort** | Makes greedy choice obvious and optimal |

---

---

## 📋 WEEK 12 SUMMARY & MASTERY CHECKLIST

### 🎯 Concepts Covered

1. ✅ **Greedy Fundamentals**
   - Locally optimal choice at each step
   - Requires optimal substructure + greedy choice property
   - Exchange argument proves correctness

2. ✅ **Activity Selection**
   - Sort by earliest finish time
   - Greedy "stays ahead" of optimal
   - Maximum non-overlapping intervals

3. ✅ **Huffman Coding**
   - Combine least-frequent characters first
   - Build optimal prefix codes
   - Proves optimality by exchange argument

4. ✅ **Fractional Knapsack**
   - Sort by value/weight ratio
   - Take items greedily until full
   - Continuous fractions allow perfect swapping

5. ✅ **Job Sequencing**
   - Sort by profit descending
   - Schedule in latest available slot
   - Maximizes profit within deadlines

---

### 📊 Problem-Solving Patterns Identified

| Pattern | Key Characteristic | Greedy Criterion |
|---------|-------------------|------------------|
| **Activity** | Max count, fixed slots | Earliest finish |
| **Huffman** | Minimize code length | Least frequent first |
| **Knapsack (Frac)** | Max value, weight limit | Max value/weight |
| **Job Sequencing** | Max profit, time slots | Max profit |

---

### ✅ MASTERY CHECKLIST FOR WEEK 12

- [ ] Can explain greedy algorithm concept to someone else
- [ ] Understand when greedy WORKS vs when it FAILS
- [ ] Can construct exchange argument for any greedy problem
- [ ] Know activity selection algorithm perfectly (can implement from scratch)
- [ ] Understand Huffman coding tree construction
- [ ] Can explain why combining least-frequent is optimal
- [ ] Know fractional knapsack algorithm (can implement)
- [ ] Understand difference between 0/1 and fractional knapsack
- [ ] Can solve job sequencing without looking up algorithm
- [ ] Can design greedy solution for new interval-based problem
- [ ] Can recognize when a problem requires DP instead of greedy
- [ ] Can prove correctness using exchange argument
- [ ] Can prove correctness using induction

---

### 🔑 TOP 5 THINGS TO REMEMBER

1. **Not all locally optimal choices lead to global optimum**
2. **Must prove greedy is correct for each specific problem**
3. **Exchange argument is the KEY proof technique**
4. **Sorting is critical - choose right criterion**
5. **Greedy works when problem has greedy choice property + optimal substructure**

---

**End of Week 12: Greedy Algorithms & Proofs**

*Comprehensive concept explanations with no code*  
*Visual diagrams, examples, and intuitive proofs*  
*Ready for implementation in any language*
