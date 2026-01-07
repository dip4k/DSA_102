# 🗺️ Week 04: Problem-Solving Roadmap & When to Use Each Pattern

**Purpose:** Decision guide for applying patterns to real problems  
**Audience:** Students solving Week 04 practice problems  
**Use:** Quick reference during problem-solving

---

## DECISION FLOWCHART: WHICH PATTERN?

```
RECEIVED A NEW PROBLEM
         ↓
    Read the problem statement carefully
    Ask: "What am I optimizing?" or "What structure am I exploiting?"
         ↓
    ╔════════════════════════════════════╗
    ║ Does it involve merging, pairing,  ║
    ║ or synchronizing two sequences?    ║
    ╚════════════════════════════════════╝
         ↙        (YES)              ↘ (NO)
    TWO-POINTER                    Continue
         ↓
    └─ Same-direction? (both moving right)
       └─ Yes: Merging, deduplication → TWO-POINTER
    
    └─ Opposite-direction? (moving toward each other)
       └─ Yes: Pairs, optimization → TWO-POINTER
         ↓
    ╔════════════════════════════════════╗
    ║ Does it involve a window of        ║
    ║ elements from an array/string?     ║
    ╚════════════════════════════════════╝
         ↙        (YES)              ↘ (NO)
    SLIDING WINDOW               Continue
         ↓
    └─ Is the window size fixed (k)?
       └─ Yes: Running avg, bounded subarray → FIXED WINDOW (Day 2)
    
    └─ Is the window size dynamic?
       └─ Yes: At-most-K, constraint-based → VARIABLE WINDOW (Day 3)
         ↓
    ╔════════════════════════════════════╗
    ║ Can I split this into independent  ║
    ║ subproblems and combine solutions? ║
    ╚════════════════════════════════════╝
         ↙        (YES)              ↘ (NO)
    DIVIDE & CONQUER            Continue
         ↓
    └─ Sorting, inversions, recursion?
       └─ Yes: DIVIDE & CONQUER (Day 4)
    
    └─ Overlapping subproblems?
       └─ Yes: Dynamic Programming (Week 14), not Day 4
         ↓
    ╔════════════════════════════════════╗
    ║ Am I optimizing (minimize/         ║
    ║ maximize) something subject to     ║
    ║ constraints?                       ║
    ╚════════════════════════════════════╝
         ↙        (YES)              ↘ (NO)
    BINARY SEARCH            Unknown Pattern
         ↓
    └─ Can I check if answer X is feasible?
       └─ Yes, feasibility is monotonic → BINARY SEARCH (Day 5)
    
    └─ Try different approach
         ↓
    └─ DEFAULT: Brute force, then optimize
```

---

## PATTERN DECISION TABLE

| Problem Type | Pattern | Check Questions | Complexity | Difficulty |
|---|---|---|---|---|
| **Merging sorted sequences** | Two-Pointer | Sorted input? Two sequences? | O(n) | 🟢 Easy |
| **Finding pairs with constraint** | Two-Pointer | Sorted? Pair relationship? | O(n) | 🟡 Med |
| **Running metric (sum, avg)** | Fixed Window | Fixed k? Subarray? | O(n) | 🟡 Med |
| **Max/min in subarrays** | Fixed Window + Deque | Bounded window? | O(n) | 🟡 Med |
| **Substring with constraint** | Variable Window | "At most K"? Constraint clear? | O(n) | 🟡-🔴 Hard |
| **Minimum window containing** | Variable Window | Contains specific items? | O(n) | 🔴 Hard |
| **Sorting large dataset** | Divide & Conquer | Sorting? O(n log n) target? | O(n log n) | 🟡 Med |
| **Counting inversions** | Divide & Conquer | Pairs with order violation? | O(n log n) | 🟡-🔴 Hard |
| **Optimal substructure** | Divide & Conquer | Can split into subproblems? | Varies | 🟡-🔴 Hard |
| **Resource optimization** | Binary Search | Minimize/maximize? Feasible check? | O(log n × check) | 🟡-🔴 Hard |

---

## ROADMAP BY DIFFICULTY

### EASY PROBLEMS (Foundation)

**Two-Pointer:**
- Merge two sorted arrays
- Remove duplicates in sorted array
- Two sum in sorted array

**Fixed Window:**
- Moving average of k elements
- Maximum of each k-length subarray (with deque)

**Expected:** 20-30 mins per problem; solutions are straightforward once you see the pattern

---

### MEDIUM PROBLEMS (Building Mastery)

**Two-Pointer:**
- Container with most water (optimization)
- Valid palindrome with two pointers

**Sliding Window (Fixed):**
- Subarray sum equals k
- Fruit into baskets

**Sliding Window (Variable):**
- Longest substring without repeating
- Longest substring with at most k distinct

**Divide & Conquer:**
- Merge sort implementation
- Quicksort implementation

**Binary Search:**
- Koko eating bananas (minimize time)
- Capacity to ship packages (minimize capacity)

**Expected:** 30-60 mins per problem; requires understanding pattern and adapting it

---

### HARD PROBLEMS (Mastery + Extension)

**Sliding Window:**
- Minimum window substring (complex constraint)
- Sliding window maximum of all subarrays

**Divide & Conquer:**
- Counting inversions
- Majority element via divide & conquer
- Merge k sorted lists

**Binary Search:**
- Painter's partition problem
- Aggressive cows (maximize minimum distance)
- Book allocation (minimize maximum pages)

**Combined Patterns:**
- Problems requiring two patterns together
- Real interview-style questions

**Expected:** 60-120 mins per problem; requires deep understanding and potentially multiple approaches tried

---

## WORKED EXAMPLES BY PATTERN

### EXAMPLE 1: Two-Pointer (Merging)
**Problem:** Merge two sorted arrays into one sorted array.
**Steps:**
1. Recognize: Two sorted sequences → Two-Pointer candidate
2. Design: Use left pointer on first array, right pointer on second
3. Implement: Compare, add smaller, advance corresponding pointer
4. Complexity: O(n+m) time, O(1) space (not counting output)
**Pattern Mastery:** Understand invariant—at each step, all processed elements are in sorted order

---

### EXAMPLE 2: Sliding Window (Fixed)
**Problem:** Given array and k, find max sum of any k consecutive elements.
**Steps:**
1. Recognize: Fixed k, subarray → Fixed Window candidate
2. Design: Initial window [0, k); slide by moving left and right
3. Implement: Remove arr[left], add arr[right], track max
4. Complexity: O(n) time, O(1) space
**Pattern Mastery:** Understand why O(n*k) → O(n): incremental update beats recalculation

---

### EXAMPLE 3: Sliding Window (Variable)
**Problem:** Find longest substring with at most 2 distinct characters.
**Steps:**
1. Recognize: "At most K" constraint, substring → Variable Window candidate
2. Design: Expand right until constraint violated, shrink left until satisfied
3. Implement: Use frequency map; state machine (VALID/INVALID)
4. Complexity: O(n) amortized, O(k) space
**Pattern Mastery:** Understand amortization—why O(n) despite shrinking happening variably

---

### EXAMPLE 4: Divide & Conquer
**Problem:** Implement merge sort.
**Steps:**
1. Recognize: Sorting, recursion, optimal substructure → Divide & Conquer
2. Design: Split into two halves; recursively sort; merge
3. Implement: Base case (n=1); split at mid; merge two sorted halves
4. Complexity: O(n log n) time, O(n) space
**Pattern Mastery:** Understand why merge is O(n) and how that gives O(n log n) total

---

### EXAMPLE 5: Binary Search on Answer
**Problem:** Find minimum time T such that k workers can process all n jobs.
**Steps:**
1. Recognize: "Minimum time" + constraint → Binary Search on Answer candidate
2. Design: Answer space [0, sum(jobs)]; feasibility: can k workers finish in time T?
3. Implement: Binary search; for each T, check if feasible using greedy
4. Complexity: O(log(sum) × n) time
**Pattern Mastery:** Understand monotonicity—if feasible in time T, feasible in T+1

---

## PRACTICE PROGRESSION

### WEEK 04 SUGGESTED PRACTICE SCHEDULE

**Day 1 (Two-Pointer): 4 problems**
- Easy: Merge two sorted arrays
- Easy: Remove duplicates
- Medium: Container with most water
- Medium: Two sum (sorted)

**Day 2 (Fixed Window): 4 problems**
- Easy: Moving average
- Medium: Max in window
- Medium: Subarray sum
- Medium: Longest substring length k

**Day 3 (Variable Window): 4 problems**
- Medium: Longest substring without repeating
- Medium: At most k distinct
- Hard: Minimum window substring
- Hard: Substring with all characters

**Day 4 (Divide & Conquer): 4 problems**
- Medium: Merge sort
- Medium: Quicksort
- Hard: Counting inversions
- Hard: Majority element

**Day 5 (Binary Search): 4 problems**
- Medium: Koko eating bananas
- Medium: Capacity to ship
- Hard: Aggressive cows
- Hard: Book allocation

**Week 6-7 (Mixed): 8-10 problems**
- Combine multiple patterns
- Hard interview-style problems
- Real-world scenarios

---

## COMMON MISTAKES BY PATTERN

### Two-Pointer
- ❌ Not maintaining invariants consistently
- ❌ Advancing both pointers when only one should move
- ❌ Off-by-one errors in boundary conditions
- ✅ Fix: Trace very carefully; use explicit boundary checks; state invariant before coding

### Sliding Window (Fixed)
- ❌ Confusing fixed size with variable size
- ❌ Not handling the initial window setup correctly
- ❌ Wrong index management (is window [left, right] or [left, right)?)
- ✅ Fix: Write out initial window; trace through first 3-4 steps manually

### Sliding Window (Variable)
- ❌ Not checking constraint at every step
- ❌ Shrinking too aggressively (overshrinking)
- ❌ Not recognizing when to expand vs. shrink
- ✅ Fix: Use state machine formalism; decide expand/shrink based on constraint

### Divide & Conquer
- ❌ Forgetting base case
- ❌ Not properly combining subproblem solutions
- ❌ Confusing with dynamic programming (overlapping subproblems)
- ✅ Fix: Write base case first; verify optimal substructure; trace recursion tree

### Binary Search on Answer
- ❌ Setting wrong bounds (too narrow or too wide)
- ❌ Feasibility function is not actually monotonic
- ❌ Off-by-one in binary search logic
- ✅ Fix: Verify bounds include answer; prove monotonicity; test binary search separately

---

## PATTERN CHECKLIST FOR EACH PROBLEM

Before implementing, ask yourself:

```
Pattern Recognition:
  ☐ Have I identified the pattern correctly?
  ☐ Does this pattern fit the problem structure?
  ☐ Are there any edge cases that break the pattern?

Implementation:
  ☐ Do I understand the algorithm without looking at code?
  ☐ Can I trace through a small example by hand?
  ☐ Have I handled all edge cases (empty input, single element, etc.)?

Complexity:
  ☐ Have I verified the time complexity is correct?
  ☐ Have I verified the space complexity is acceptable?
  ☐ Is there a faster solution I'm missing?

Correctness:
  ☐ Does my solution give the right answer for all test cases?
  ☐ Have I tested on edge cases?
  ☐ Do I understand *why* it works, not just that it does?
```

---

## WHEN TO MOVE TO NEXT PATTERN

**Move to Day 2 (Fixed Window) when:**
- ☐ You can solve 4/4 Two-Pointer problems correctly
- ☐ You understand invariants conceptually
- ☐ You can explain your solution without looking at code

**Move to Day 3 (Variable Window) when:**
- ☐ You can solve 4/4 Fixed Window problems
- ☐ You understand amortized analysis (at least intuitively)
- ☐ You can implement monotonic deque

**Move to Day 4 (Divide & Conquer) when:**
- ☐ You can solve 4/4 Variable Window problems
- ☐ You understand expand/shrink logic
- ☐ You can recognize variable window patterns

**Move to Day 5 (Binary Search) when:**
- ☐ You can solve 4/4 Divide & Conquer problems
- ☐ You understand recurrence relations (conceptually)
- ☐ You can trace merge sort correctly

**Start Week 5 when:**
- ☐ You can solve mixed problems (identifying pattern first)
- ☐ You understand all 5 patterns and their trade-offs
- ☐ You can explain patterns in an interview

---

**Use this roadmap to guide your learning. Pattern recognition improves with practice!**

