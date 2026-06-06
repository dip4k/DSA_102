# 📊 WEEK 04 VISUAL CONCEPTS PLAYBOOK

**Week:** 4 | **Tier:** Core Problem-Solving Patterns I  
**Theme:** Two Pointers, Sliding Windows, Divide & Conquer, Binary Search  
**Purpose:** Visual-first concept explanation for rapid revision and intuitive understanding  
**Format:** Diagrams, flowcharts, trace tables, comparison charts (minimal text)

---

## 🎨 VISUAL LEGEND

| Symbol | Meaning |
|--------|---------|
| `L` / `R` | Left and right pointers (opposite-direction) |
| `read` / `write` | Read pointer (scanning) and write pointer (placement) |
| `[...]` | Current window or active range |
| `lo` / `hi` / `mid` | Binary search bounds |
| `⬅️ ➡️` | Pointer movement direction |
| `✓` | Valid state / condition met |
| `✗` | Invalid state / condition violated |
| `→` | Next step / update |
| `▢` | Array element / window cell |
| `█` | Active/processed element |
| `░` | Processed/valid zone |
| `◀▶` | Deque front/back |

---

## 📅 DAY 1: TWO-POINTER PATTERNS

### Pattern Map: Two-Pointer Family Tree

```
TWO-POINTER PATTERNS
├─ Same-Direction (Read-Write)
│  ├─ Move Zeroes / Remove Duplicates
│  ├─ Partition Operations
│  └─ In-place array transformations
│
├─ Opposite-Direction (Converging)
│  ├─ Two-Sum (sorted array)
│  ├─ Container with Most Water
│  └─ Three-Sum family
│
└─ Slow-Fast (Cycle Detection)
   ├─ Linked list cycles
   ├─ Happy numbers
   └─ Floyd's algorithm
```

---

### Pattern 1.1: Same-Direction Pointers (Move Zeroes)

#### Visual 1: Array State Evolution

```
INITIAL:  [1, 0, 2, 0, 3]
          ↑ read
          ↑ write

AFTER iter 0:  [1, 0, 2, 0, 3]
               ↑ read → 1 ≠ 0, swap not needed
               ↑ write → advance to 1

AFTER iter 1:  [1, 0, 2, 0, 3]
                  ↑ read → 0 == 0, skip
                  ↑ write → stay at 1

AFTER iter 2:  [1, 2, 0, 0, 3]
                     ↑ read → 2 ≠ 0, swap with write
                     ↑ write → advance to 2

FINAL:     [1, 2, 3, 0, 0]
           
INVARIANT:
[0..writePos) = all non-zero elements (✓ valid)
[writePos..end) = all zero elements (✓ valid)

TIME: O(n) | SPACE: O(1)
```

#### Visual 2: Invariant Zones

```
INVARIANT DIAGRAM:
┌─────────────────────────────────────────┐
│ [0..writePos) │ [writePos..i] │ [i..end] │
│  PROCESSED    │  UNSCANNED   │ BUFFERED │
│  (non-zero)   │  (no info)   │ (to do)  │
└─────────────────────────────────────────┘
       ↑                 ↑           ↑
    SAFE ZONE        SCAN HEAD   BOUNDARY
```

---

### Pattern 1.2: Opposite-Direction Pointers (Container with Most Water)

#### Visual 1: Greedy Movement Decision

```
ARRAY: [1, 8, 6, 2, 5, 4, 8, 3, 7]
INDEX:  0  1  2  3  4  5  6  7  8

INITIAL:
L=0(h=1)                    R=8(h=7)
│                               │
├───────────────────────────────┤
Area = min(1,7) × 8 = 8

DECISION AT EACH STEP:
height[L]=1 < height[R]=7  →  Move L (shorter pointer)
                              (Moving R only decreases area)

L=1(h=8)                       R=8(h=7)
│                              │
├──────────────────────────────┤
Area = min(8,7) × 7 = 49 ✓ BETTER

height[L]=8 > height[R]=7  →  Move R (shorter pointer)

L=1(h=8)                   R=7(h=3)
│                          │
├──────────────────────────┤
Area = min(8,3) × 6 = 18   ✗ WORSE

...continue until L ≥ R...

KEY INSIGHT:
┌─────────────────────────────────┐
│ ALWAYS move the SHORTER pointer │
│ (Moving taller cannot help area)│
└─────────────────────────────────┘

TIME: O(n) | SPACE: O(1)
```

#### Visual 2: Greedy Justification Proof

```
CURRENT STATE:
height[L] < height[R]   (L is shorter)

IF WE MOVE R (WRONG):
  New Width = R' - L = (R-1) - L = (R-L) - 1  ↓ (smaller)
  New Height ≤ min(height[L], height[R']) 
            ≤ height[L]  (at best unchanged, likely smaller)
  New Area ≤ height[L] × (R-L) - ... ↓ WORSE OR EQUAL

IF WE MOVE L (CORRECT):
  New Width = R - L' = R - (L+1) = (R-L) - 1  ↓ (smaller)
  New Height = min(height[L'], height[R])
             Can be > height[L]  ↑ (POSSIBLE GAIN!)
  New Area = might be larger if height[L'] > height[L]

CONCLUSION: Always move shorter → maximize chance of gain
```

---

### Pattern 1.3: Two-Sum in Sorted Array

#### Visual 1: Convergence Trace

```
TARGET = 9
ARRAY: [-3, -1, 0, 2, 4, 6, 8, 10]

L=0(-3)                    R=7(10)
├────────────────────────────────┤
Sum = -3+10 = 7 < 9  → Move L (need larger sum)

     L=1(-1)             R=7(10)
     ├──────────────────────┤
     Sum = -1+10 = 9 ✓ FOUND! → Record, skip duplicates

              L=2(0)      R=7(10)
              ├──────────────┤
              Sum = 0+10 = 10 > 9  → Move R (need smaller sum)

              L=2(0)   R=6(8)
              ├────────────┤
              Sum = 0+8 = 8 < 9  → Move L

                   L=3(2) R=6(8)
                   ├──────┤
                   Sum = 2+8 = 10 > 9  → Move R

                   L=3(2) R=5(6)
                   ├────────┤
                   Sum = 2+6 = 8 < 9  → Move L

                      L=4(4) R=5(6)
                      ├──┤
                      Sum = 4+6 = 10 > 9  → Move R

L ≥ R → DONE

RESULTS: [(-1,10), (2,6)]  ✓ All unique pairs

TIME: O(n) | SPACE: O(1)
```

#### Visual 2: State Machine

```
┌─────────────────────────────────────────┐
│      TWO-POINTER STATE MACHINE          │
├─────────────────────────────────────────┤
│                                         │
│  START: L at 0, R at end                │
│    │                                    │
│    ▼                                    │
│  ┌─────────────────────┐                │
│  │ Compute sum = A[L] +│                │
│  │         A[R]        │                │
│  └──────────┬──────────┘                │
│             │                          │
│    ┌────────┼────────┐                 │
│    ▼        ▼        ▼                 │
│  sum<tgt  sum=tgt  sum>tgt             │
│   (need   (found)   (need              │
│   larger)           smaller)            │
│    │        │        │                 │
│    ▼        ▼        ▼                 │
│   L++    RECORD    R--                 │
│          L++/R--                        │
│    │      │        │                   │
│    └──────┴────┬───┘                   │
│               │                        │
│               ▼                        │
│           L ≥ R?                       │
│            /  \                        │
│           Y    N                       │
│           │    │                       │
│           ▼    ▼                       │
│         DONE  (loop)                   │
│                                        │
└─────────────────────────────────────────┘
```

---

### Common Failure Modes (Visual)

#### Failure 1: Off-by-One in Invariant

```
❌ WRONG:
┌─────────────────────────────────────────┐
│ [0..writePos] │ [writePos+1..end]      │
│  PROCESSED    │  UNPROCESSED            │
└─────────────────────────────────────────┘
                 ↑ OVERLAP! (writePos included twice)

✓ CORRECT:
┌─────────────────────────────────────────┐
│ [0..writePos) │ [writePos..end]        │
│  PROCESSED    │  UNPROCESSED            │
└─────────────────────────────────────────┘
                 ↑ Clean boundary (half-open interval)
```

#### Failure 2: Wrong Pointer Moves

```
❌ Problem: Container with Most Water

If height[L] < height[R] and we MOVE R:
  [1, 8, 6, 2, 5, 4, 8, 3, 7]
   L(1)                    R(7)
   
   New state: L(1)      R'(3)
   min(1,3)×(8-1) = 7  ← Worse than min(1,7)×8=8

✓ Solution: Always move the SHORTER pointer
```

#### Failure 3: Forgetting to Skip Duplicates

```
❌ WRONG: Two-Sum with duplicates [1, 1, 2, 2, 3]
Result: [(1,3), (1,3), (2,2), (2,2)]  ← Duplicates!

✓ CORRECT: After finding pair, skip all duplicates
Target: 4, Array: [1, 1, 2, 2, 3]
Found (1,3) at indices (1,4)
  → While L < R and A[L] == A[L+1]: L++
  → While L < R and A[R] == A[R-1]: R--
Result: [(1,3)]  ← Unique only
```

---

### Mini Review Quiz (Day 1)

**Q1:** In same-direction pointers (move zeroes), what zone invariant must hold after each iteration?
```
A) [0..writePos) = non-zeros | [writePos..i) = zeros
B) [0..i) = processed | [i..end) = unprocessed
C) [0..writePos] = non-zeros | [writePos..end] = zeros
D) None of the above
```
**Answer:** A (half-open intervals critical!)

**Q2:** For container with most water, why do we always move the shorter pointer?
```
A) It's random; either can work
B) Moving the taller pointer guarantees area doesn't increase
C) Moving shorter always finds the answer
D) We alternate for fairness
```
**Answer:** B (mathematical justification!)

**Q3:** If array has duplicates in two-sum, how do we avoid duplicate results?
```
A) Use a HashSet to filter
B) Skip duplicate pointers after recording a pair
C) Only keep first occurrence
D) It doesn't matter
```
**Answer:** B (efficiency over filtering)

---

## 🪟 DAY 2: SLIDING WINDOW (FIXED SIZE)

### Pattern Map: Fixed Window Family

```
FIXED WINDOW PATTERNS
├─ Simple Aggregation
│  ├─ Max/Min sum k consecutive
│  ├─ Average of k elements
│  └─ Counting patterns in window
│
├─ Complex Aggregation
│  ├─ Max sliding window (with deque)
│  ├─ Min sliding window (with deque)
│  └─ Constraint checking (state machine)
│
└─ Multi-Window Queries
   ├─ All windows of size k
   ├─ Prefix/suffix cache pre-computation
   └─ Range aggregate queries
```

---

### Pattern 2.1: Fixed Window Mechanics (Max Sum k Consecutive)

#### Visual 1: Slide Storyboard

```
ARRAY: [1, 2, 3, 4, 5]  k=3

STEP 1: Build initial window [0..2]
┌─────────┐
│ 1 2 3 │ 4  5
└─────────┘
window[0,1] = 1+2+3 = 6   ← Record


STEP 2: Slide right by 1
        Remove A[0]=1, Add A[3]=4
        window[1,2,3] = 6 - 1 + 4 = 9
┌─────────────┐
  1 │ 2 3 4 │ 5
      └─────────┘
      window[1,3] = 9  ← Record


STEP 3: Slide right by 1
        Remove A[1]=2, Add A[4]=5
        window[2,3,4] = 9 - 2 + 5 = 12
      ┌─────────────┐
      2 3 │ 4 5 │
          └─────┘
          window[2,4] = 12  ← Record


RESULT: MAX = 12

TIME: O(n) | SPACE: O(1)

vs NAIVE: O(n×k) ← Quadratic!
```

#### Visual 2: Window Shift Visualization

```
OPERATION PER SLIDE:
┌──────────────────────────────┐
│  windowSum -= A[i-k]         │
│  windowSum += A[i]           │
│  maxSum = max(maxSum, sum)   │
└──────────────────────────────┘

                ↓

BEFORE:  ▓▓▓
         i-k     i
         
AFTER:     ▓▓▓
           i-k+1 i+1
           
KEY: Only 2 arithmetic ops per position → O(n) total
```

---

### Pattern 2.2: Max Sliding Window with Monotonic Deque

#### Visual 1: Deque State Evolution

```
ARRAY: [1, 3, -1, -3, 5, 3, 6, 7]  k=3

WINDOW 0 [1, 3, -1]:
Deque (indices): [1]  ← index 1 (value 3, max of window)
                 [0, 1, -1]  ← Before pruning
                 └─ Remove 0 (1<3)
                 └─ Add 1

WINDOW 1 [3, -1, -3]:
Deque: [1]  ← Remove out-of-window indices (if i-k < front)
       [1, 2]  ← Add 2 (-1), but 2 not last because we move next

WINDOW 2 [-1, -3, 5]:
Deque: [6]  ← Remove 1,2 (both < 5)
       [6]  ← Add 4 (value 5)

WINDOW 3 [-3, 5, 3]:
Deque: [6, 7]  ← 7 (value 3) > 8 (if existed), so append

WINDOW 4 [5, 3, 6]:
Deque: [6]  ← Remove 7 (3 < 6)
       [6]  ← Add 8 (value 6, equal to front so append)

RESULT MAX VALUES: [3, 3, 5, 5, 6, 7]

DEQUE INVARIANT:
┌────────────────────────────────────┐
│ Deque elements are in DECREASING   │
│ order of VALUE (by index, but we   │
│ track indices in deque)            │
│                                    │
│ Front = Current window max         │
│ Back = Candidates for future max   │
└────────────────────────────────────┘

TIME: O(n) amortized | SPACE: O(k)
```

#### Visual 2: Deque Operations

```
MONOTONIC DEQUE OPERATIONS:

1) REMOVE OUT-OF-WINDOW (front):
   if front_index ≤ i - k:
      deque.RemoveFirst()

2) REMOVE SMALLER ELEMENTS (back):
   while deque NOT empty AND arr[deque.back] < arr[i]:
      deque.RemoveLast()

3) ADD CURRENT ELEMENT:
   deque.AddLast(i)

4) RECORD MAX:
   max_values[...] = arr[deque.front]

WHY THIS WORKS:
┌─────────────────────────────────────┐
│ If element X is smaller than Y and  │
│ X enters after Y, then X cannot be  │
│ max before Y leaves → remove X      │
│ (pruning maintains decreasing order)│
└─────────────────────────────────────┘
```

---

### Pattern 2.3: State Machine in Fixed Window

#### Visual 1: Pattern Detection State Flow

```
PROBLEM: Detect pattern in k-length window
(e.g., "all ACGT" in DNA sequence)

┌─────────────────────────────────────────┐
│        FIXED WINDOW STATE MACHINE       │
├─────────────────────────────────────────┤
│                                         │
│  BUILD WINDOW [0..k):                   │
│  validCount = count(valid chars)        │
│    │                                    │
│    ▼                                    │
│  validCount == k?  ──YES──→ RETURN TRUE │
│    │                                    │
│   NO                                    │
│    │                                    │
│    ▼                                    │
│  SLIDE WINDOW [i-k+1..i]:               │
│  Remove A[i-k]: if valid, validCount--  │
│  Add A[i]: if valid, validCount++       │
│    │                                    │
│    ▼                                    │
│  validCount == k?  ──YES──→ RETURN TRUE │
│    │                                    │
│   NO, i++ & LOOP                        │
│    │                                    │
│    ▼                                    │
│  i > end?  ──YES──→ RETURN FALSE        │
│    │                                    │
│   NO, LOOP                              │
│                                         │
└─────────────────────────────────────────┘
```

---

### Performance Comparison Table (Day 2)

```
┌────────────────────────────────────────────────────────────┐
│  ALGORITHM         │  TIME      │ SPACE │ WHEN TO USE    │
├────────────────────────────────────────────────────────────┤
│ Naive k-window     │ O(n×k)     │ O(1)  │ Small k only   │
│ Fixed window sum   │ O(n)       │ O(1)  │ Aggregation    │
│ Monotonic deque    │ O(n)       │ O(k)  │ Max/min window │
│ Precomputed prefix │ O(n + q)   │ O(n)  │ Batch queries  │
└────────────────────────────────────────────────────────────┘

DECISION TREE:
Query window max/min in all windows of size k?
  ├─ Use MONOTONIC DEQUE (amortized O(n), optimal for single pass)
  └─ If multiple independent queries: precompute segment tree

Just need sum/avg in windows of size k?
  ├─ Use FIXED WINDOW SUM (O(n), simplest)

Pattern/constraint checking?
  ├─ Use STATE MACHINE with fixed window
```

---

### Common Failure Modes (Day 2)

#### Failure 1: Incorrect Deque Initialization

```
❌ WRONG: Add all elements first, then slide
Deque becomes: [all indices]  ← Not monotonic!

✓ CORRECT: Build window AND maintain deque simultaneously
As you add each element to window, immediately prune from back
```

#### Failure 2: Forgetting Window Boundary Check

```
❌ WRONG:
if (deque.front() <= i - k)  ← Uses ≤ when should use <
  // Off-by-one: keeps element just outside window

✓ CORRECT:
while (!deque.empty() && deque.front() <= i - k)
  deque.RemoveFirst();
  // Removes indices [i-k+1, i] stays in deque
```

#### Failure 3: Not Skipping Out-of-Window Check

```
❌ WRONG: Iterate k-1 times before first result
Result array has wrong alignment

✓ CORRECT: First result at index k-1
for i = k-1 to n-1:
  output arr[deque.front()]
```

---

## 📏 DAY 3: SLIDING WINDOW (VARIABLE SIZE)

### Pattern Map: Variable Window Family

```
VARIABLE WINDOW PATTERNS
├─ Expand/Contract Mechanics
│  ├─ At most K distinct
│  ├─ Min window substring
│  ├─ Longest subarray constraint
│  └─ Permutation/anagram search
│
├─ Frequency-Based Constraints
│  ├─ Exactly K distinct = AtMost(K) - AtMost(K-1)
│  ├─ Character count matching
│  └─ Duplicate handling
│
└─ Optimization Goals
   ├─ Maximize valid window (longest)
   ├─ Minimize valid window (shortest)
   └─ Find first occurrence
```

---

### Pattern 3.1: Expand-Contract Mechanics

#### Visual 1: Two-Phase Decision

```
TARGET: Minimum window containing all chars from "ABC" in "ADOBECODEBANC"

PHASE 1: EXPAND (extend right until valid)
┌──────────────┐
A D O B E C O D E B A N C
↑              ↑
L              R
Window: "ADOBEC"  → Has A, B, C? ✓ YES

PHASE 2: CONTRACT (shrink left while valid)
      ┌──────────────┐
A D O B E C O D E B A N C
      ↑        ↑
      L        R
Window: "DOBEC"  → Has A, B, C? ✗ NO (lost A)

PHASE 1 (repeat): EXPAND
          ┌──────────────────────────┐
A D O B E C O D E B A N C
            ↑                 ↑
            L                 R
Window: "ODEBANC"  → Has all? ✓ YES

PHASE 2 (repeat): CONTRACT
              ┌──────┐
A D O B E C O D E B A N C
              ↑      ↑
              L      R
Window: "BANC"  → Has all? ✓ YES

              ┌────┐
A D O B E C O D E B A N C
                ↑  ↑
                L  R
Window: "ANC"  → Has A, B, C? ✗ NO (lost B)

STOP (L > R)

RESULT: MIN WINDOW = "BANC" (length 4)

KEY INVARIANT:
┌──────────────────────────────────────────┐
│ [L..R] = current window (expanding)      │
│ When valid: try to shrink (move L→)      │
│ When invalid: expand (move R→)           │
│ Record minimum width valid window found  │
└──────────────────────────────────────────┘

TIME: O(|s| + |t|) | SPACE: O(|charset|)
```

#### Visual 2: Frequency Map State

```
TARGET: "ABC"
target_freq = {A:1, B:1, C:1}  ← What we need

STEP 1: Build window [A D O B E C]
window_freq = {A:1, D:1, O:1, B:1, E:1, C:1}
formed_count = 3  ← We have all 3 target chars

STEP 2: Check if valid
formed_count == target.size? (3 == 3)  ✓ YES

STEP 3: Try to shrink
Remove A: window_freq[A] = 0
formed_count = 2  ✗ No longer valid

Can't shrink further → Expand

STEP N: After many steps, window = [B A N C]
window_freq = {B:1, A:1, N:1, C:1}
formed_count = 3  ✓ Valid

Try shrink:
Remove B: formed_count = 2  ✗ Stop

DECISION LOGIC:
┌────────────────────────────────────────┐
│ IF formed_count == target_count:       │
│   Record window length               │
│   Try to shrink left  ────────────┐    │
│ ELSE:                             │    │
│   Expand right        ────────────┤    │
│                                   ▼    │
│         Continue until L > R      LOOP │
└────────────────────────────────────────┘
```

---

### Pattern 3.2: At Most K Distinct Characters

#### Visual 1: Window Constraint

```
PROBLEM: Longest substring with AT MOST k=2 distinct chars
STRING: "eceba"

┌─────┐
e c e b a
├─────┤
Window "ec": char_count = {e:1, c:1}  |chars| = 2 ≤ 2 ✓
Length = 2

┌─────────┐
e c e b a
├─────────┤
Window "ece": char_count = {e:2, c:1}  |chars| = 2 ≤ 2 ✓
Length = 3  ← NEW MAX

    ┌──────────┐
    c e b a
    ├──────────┤
Window "ceba": char_count = {c:1, e:1, b:1}  |chars| = 3 > 2 ✗
Must shrink

      ┌────────┐
      e b a
      ├────────┤
Window "eba": char_count = {e:1, b:1, a:1}  |chars| = 3 > 2 ✗
Must shrink

        ┌──────┐
        b a
        ├──────┤
Window "ba": char_count = {b:1, a:1}  |chars| = 2 ≤ 2 ✓
Length = 2

RESULT: MAX LENGTH = 3 (substring "ece")

SHRINK CONDITION:
┌──────────────────────────────────────┐
│ while char_count.Count > k:          │
│   windowFreq[s[left]]--              │
│   if windowFreq[s[left]] == 0:       │
│      Remove from map                │
│   left++                            │
└──────────────────────────────────────┘

TIME: O(n) | SPACE: O(k)
```

---

### Pattern 3.3: Exactly K = AtMost(K) - AtMost(K-1)

#### Visual 1: Mathematical Decomposition

```
GOAL: Count substrings with EXACTLY k distinct chars

INSIGHT:
exactly(k) = at_most(k) - at_most(k-1)

EXAMPLE: "aaab"  k=2

AT_MOST(2):  {"aa", "aaa", "aaab", "ab", "aab", "b"}  = 6
AT_MOST(1):  {"a", "aa", "aaa", "aaab", "b"}  = 5

EXACTLY(2) = 6 - 5 = 1  (substring "aaab" only... wait, wrong!)

Let me recalculate:
AT_MOST(2) includes:
- "a" (1 distinct) ✓
- "aa" (1 distinct) ✓
- "aaa" (1 distinct) ✓
- "aaab" (2 distinct) ← Count this
- "aab" (2 distinct) ← Count this
- "ab" (2 distinct) ← Count this
- "b" (1 distinct) ✓
= 7 total

AT_MOST(1) includes:
- "a", "aa", "aaa", "b" = 4

EXACTLY(2) = 7 - 4 = 3  ← "aaab", "aab", "ab"

CODE:
return at_most(s, k) - at_most(s, k-1)
```

---

### Pattern 3.4: Permutation/Anagram Window Search

#### Visual 1: Frequency Matching

```
PROBLEM: Find all indices where anagram of pattern occurs
PATTERN: "abc"
STRING: "cbaebabacd"

pattern_freq = {a:1, b:1, c:1}
window_size = 3

WINDOW 0: "cba" (indices 0-2)
window_freq = {c:1, b:1, a:1}
Matches pattern_freq? ✓ YES → Record index 0

WINDOW 1: "bae" (indices 1-3)
window_freq = {b:1, a:1, e:1}
Matches? ✗ NO (has 'e', missing 'c')

WINDOW 2: "aeb" (indices 2-4)
window_freq = {a:1, e:1, b:1}
Matches? ✗ NO

...continue...

WINDOW 6: "bac" (indices 6-8)
window_freq = {b:1, a:1, c:1}
Matches? ✓ YES → Record index 6

RESULT: [0, 6]  (anagrams at positions 0 and 6)

KEY:
┌────────────────────────────────────┐
│ Fixed window size = pattern length │
│ Remove A[i-k], Add A[i] each step  │
│ Compare maps: O(1) amortized       │
│ (if charset is bounded)            │
└────────────────────────────────────┘

TIME: O(n) | SPACE: O(charset)
```

---

### Common Failure Modes (Day 3)

#### Failure 1: Forgetting When to Shrink

```
❌ WRONG: Only expand, never contract
result = ""  ← Never finds valid window

✓ CORRECT: Alternate expand/contract
while right < end:
  expand (add A[right])
  while valid:
    record result
    shrink (remove A[left])
  right++
```

#### Failure 2: Not Checking Valid Before Recording

```
❌ WRONG:
For each i:
  Remove A[left]
  Record (might be invalid!)
  if valid: continue shrinking

✓ CORRECT:
For each i:
  Add A[i]
  while valid:
    Record (definitely valid)
    Remove A[left]
  if not valid: expand again
```

#### Failure 3: Forgetting to Rebuild Map After Removal

```
❌ WRONG:
Remove A[left]: charCount[A[left]]--
if charCount[A[left]] == 0: DELETE ENTRY
// But later check still references old entry

✓ CORRECT:
charCount[A[left]]--
if charCount[A[left]] == 0: remove(A[left])
// Now map is clean
```

---

## ✂️ DAY 4: DIVIDE & CONQUER

### Pattern Map: D&C Family

```
DIVIDE & CONQUER PATTERNS
├─ Sorting & Merging
│  ├─ Merge Sort
│  ├─ Counting Inversions
│  └─ Merge K Lists
│
├─ Search & Selection
│  ├─ Binary Search Variants
│  ├─ Kth Smallest
│  └─ Majority Element
│
└─ Computation
   ├─ Expression Evaluation
   ├─ Matrix Multiplication
   └─ Closest Pair Problem
```

---

### Pattern 4.1: Merge Sort Recursion Tree

#### Visual 1: Tree Structure

```
ARRAY: [38, 27, 43, 3, 9, 82, 10]

                    [38,27,43,3,9,82,10]
                    /                  \
                  /                      \
        [38,27,43,3]                [9,82,10]
         /        \                  /      \
      /            \              /          \
    [38,27]      [43,3]        [9,82]      [10]
    /    \       /    \        /    \        |
  [38]  [27]   [43]  [3]    [9]  [82]     (base)
  
MERGE PHASE (bottom-up):
  [27,38]    [3,43]      [9,82]     [10]
      \        /            \       /
       [3,27,38,43]    [9,10,82]
           \              /
           [3,9,10,27,38,43,82]

RECURSION LEVELS:
Level 0: n/1 = 7 elements, 1 array
Level 1: n/2 = 3.5 → 2 arrays, each doing n/2 work each → total n
Level 2: n/4 = 1.75 → 4 arrays, each doing n/4 work each → total n
...
Level log₂(n): n base cases

Total work: n × log₂(n) = O(n log n)

KEY INSIGHT:
┌────────────────────────────────┐
│ Each level does O(n) work      │
│ There are O(log n) levels      │
│ Total: O(n log n) guaranteed   │
└────────────────────────────────┘

TIME: O(n log n) | SPACE: O(n) ← Extra merge array
```

#### Visual 2: Master Theorem Visualization

```
RECURRENCE: T(n) = 2T(n/2) + O(n)

MASTER THEOREM: T(n) = aT(n/b) + f(n)
Here: a=2, b=2, f(n)=O(n)

Compare:
n^(log_b a) = n^(log_2 2) = n^1 = n
f(n) = O(n)

CASE 2: f(n) = Θ(n^(log_b a))
→ T(n) = Θ(n^(log_b a) × log n) = Θ(n log n)

APPLICATION:
┌────────────────────────────────────┐
│ Merge Sort:                        │
│ 2 subproblems, each 1/2 size      │
│ Merge cost: O(n)                  │
│ → T(n) = O(n log n)               │
└────────────────────────────────────┘
```

---

### Pattern 4.2: Counting Inversions

#### Visual 1: Inversion Detection via Merge

```
ARRAY: [2, 4, 1, 3, 5]
GOAL: Count pairs (i,j) where i < j but arr[i] > arr[j]

MERGE SORT + COUNT:

Split: [2, 4, 1] and [3, 5]
  └─ Split again: [2], [4, 1]
     └─ Merge [2] and [4, 1]:
        Split [4, 1]: [4], [1]
        Merge [4] and [1]:
          1 < 4 → Take 1, add inversions: (4 > 1) = 1 inversion

Merged: [1, 4]  inversions so far: 1

     Back to merging [2] and [1, 4]:
       2 > 1 → Take 1, add inversions: (2 > 1) = 1
       2 < 4 → Take 2
       4 remains
       
Merged: [1, 2, 4]  inversions so far: 2

Split [3, 5]: Merge → [3, 5]  no inversions

FINAL MERGE [1, 2, 4] and [3, 5]:
  1 < 3 → Take 1
  2 < 3 → Take 2
  4 > 3 → Take 3, add: remaining left subarray = [4,5]
          All 2 elements > 3 → 2 inversions
  5 remains → Take 5
  
Merged: [1, 2, 3, 4, 5]  total inversions: 2 + 2 = 4

KEY INSIGHT:
┌────────────────────────────────┐
│ When right < left during merge,│
│ ALL remaining left elements    │
│ are greater → each forms an    │
│ inversion with right           │
│                                │
│ Count: (mid - i + 1)           │
└────────────────────────────────┘

TIME: O(n log n) | SPACE: O(n)
```

---

### Pattern 4.3: D&C vs DP Decision Tree

#### Visual 1: Decision Framework

```
PROBLEM ANALYSIS:

Q1: Do subproblems overlap?
├─ YES → DP (memoization/tabulation)
│  Example: Fibonacci(n) calls Fibonacci(n-1) calls Fibonacci(n-2)...
│  Multiple paths to same subproblem
│
└─ NO → D&C (fresh recursion)
   Example: Merge sort each subarray processed once

Q2: Can we combine results easily?
├─ YES → D&C
│  Example: Merge two sorted arrays easily
│
└─ NO → Might need different approach

Q3: Is problem naturally hierarchical?
├─ YES → D&C
│  Example: Tree problems, divide by structure
│
└─ MAYBE → DP could work too
   Example: Can represent as stages

DECISION GRID:
┌─────────────────────────────────────────────┐
│ Overlapping? │ Easy Combine? │ Approach     │
├─────────────────────────────────────────────┤
│ YES          │ N/A           │ DP (cache)   │
│ NO           │ YES           │ D&C          │
│ NO           │ NO            │ Other method │
└─────────────────────────────────────────────┘
```

---

### Common Failure Modes (Day 4)

#### Failure 1: Forgetting to Merge (Just Divide)

```
❌ WRONG:
result_left = solve(left)
result_right = solve(right)
return ???  ← Forgot to combine!

✓ CORRECT:
result_left = solve(left)
result_right = solve(right)
return MERGE(result_left, result_right)
```

#### Failure 2: Wrong Base Case

```
❌ WRONG:
if (left >= right) return [arr[left]];  ← Includes right!
// Results in half-open vs closed interval confusion

✓ CORRECT:
if (left == right) return [arr[left]];
// or
if (left > right) return [];  ← Empty if invalid
```

#### Failure 3: Off-by-One in Split

```
❌ WRONG:
mid = (left + right) / 2;  ← Integer division (biased)
// Could overflow if left + right is huge

✓ CORRECT:
mid = left + (right - left) / 2;  ← Avoids overflow
```

---

## 🔍 DAY 5: BINARY SEARCH AS A PATTERN

### Pattern Map: Binary Search Variants

```
BINARY SEARCH PATTERNS
├─ Classic Search
│  ├─ Standard binary search
│  ├─ First/last occurrence
│  └─ Rotated sorted array
│
├─ Answer Space Search (Feasibility)
│  ├─ Minimize capacity needed
│  ├─ Maximize minimum distance
│  └─ Minimize maximum load
│
└─ Geometric Search
   ├─ Peak finding
   ├─ Bitonic search
   └─ Closest value
```

---

### Pattern 5.1: Binary Search Invariant

#### Visual 1: Search Range Evolution

```
ARRAY: [-3, -1, 0, 2, 4, 6, 8, 10]
TARGET: 4

ITERATION 0:
lo=0                                    hi=7
├────────────────────────────────────────┤
mid = 0 + (7-0)/2 = 3
arr[3] = 2 < 4 → Move lo to mid+1

        lo=4                            hi=7
        ├────────────────────────────────┤
        mid = 4 + (7-4)/2 = 5
        arr[5] = 6 > 4 → Move hi to mid-1

        lo=4            hi=4
        ├────────────────┤
        mid = 4 + (4-4)/2 = 4
        arr[4] = 4 == 4 ✓ FOUND!

INVARIANT MAINTAINED:
┌──────────────────────────────────────┐
│ Target always in range [lo, hi]      │
│ Each iteration narrows by half       │
│ Terminates when lo >= hi             │
└──────────────────────────────────────┘

TIME: O(log n) | SPACE: O(1)
```

#### Visual 2: Midpoint Calculation Safety

```
WRONG CALCULATION:
mid = (lo + hi) / 2
If lo = 2^31-1, hi = 2^31-1:
  sum = 2^31 - 1 + 2^31 - 1 = 2^32 - 2
  OVERFLOW! → negative or wrap

CORRECT CALCULATION:
mid = lo + (hi - lo) / 2
If lo = 2^31-1, hi = 2^31-1:
  diff = (2^31-1) - (2^31-1) = 0
  mid = (2^31-1) + 0/2 = 2^31-1
  NO OVERFLOW ✓
```

---

### Pattern 5.2: Binary Search on Answer Space

#### Visual 1: Feasibility Curve

```
PROBLEM: Minimum capacity to ship all packages in d days
PACKAGES: [1, 2, 3, 4, 5]  days = 3

FEASIBILITY CURVE:
Capacity   Can ship in 3 days?
1                  ✗
2                  ✗
3                  ✗
4                  ✗
5    ← Boundary    ✗
6                  ✓ ← First feasible
7                  ✓
8                  ✓
...
15 (sum)           ✓

MONOTONIC PROPERTY:
┌─────────────────────────────────────┐
│ Once capacity becomes feasible,     │
│ all larger capacities are feasible  │
│ Pattern: ✗ ✗ ✗ ✗ ✓ ✓ ✓ ✓ ✓       │
└─────────────────────────────────────┘

BINARY SEARCH:
lo = max(packages) = 5
hi = sum(packages) = 15

mid = 10 → Can ship in 3 days? [1,2,3,4], [5] = 2 days ✓
  → Try smaller, hi = 9

mid = 7 → Can ship? [1,2,3], [4], [5] = 3 days ✓
  → Try smaller, hi = 6

mid = 5 → Can ship? [1,2], [3], [4], [5] = 4 days ✗
  → Try larger, lo = 6

lo >= hi → ANSWER = 6

VERIFICATION:
Capacity 6: [1,2,3], [4], [5] = 2 days ✓ (actually better!)
```

---

### Pattern 5.3: Binary Search Templates

#### Visual 1: First vs Last Occurrence

```
ARRAY: [1, 2, 2, 2, 3, 4]
TARGET: 2

FIND FIRST (leftmost):
┌────────────────────────────────────┐
│ If arr[mid] >= target:             │
│   hi = mid  (answer could be here) │
│ Else:                              │
│   lo = mid + 1                     │
│                                    │
│ At end: lo points to first target  │
└────────────────────────────────────┘

lo=0                              hi=5
mid = 2: arr[2]=2 >= 2 → hi=2

lo=0        hi=2
mid = 1: arr[1]=2 >= 2 → hi=1

lo=0    hi=1
mid = 0: arr[0]=1 < 2 → lo=1

lo=1    hi=1  → FOUND index 1 ✓

---

FIND LAST (rightmost):
┌────────────────────────────────────┐
│ If arr[mid] <= target:             │
│   lo = mid + 1  (answer could be   │
│                  further right)    │
│ Else:                              │
│   hi = mid                         │
│                                    │
│ At end: lo-1 points to last target │
└────────────────────────────────────┘

lo=0                              hi=5
mid = 2: arr[2]=2 <= 2 → lo=3

lo=3                        hi=5
mid = 4: arr[4]=3 > 2 → hi=4

lo=3            hi=4
mid = 3: arr[3]=2 <= 2 → lo=4

lo=4    hi=4  → FOUND index 3 (last) ✓
```

---

### Pattern 5.4: Peak Finding

#### Visual 1: Local Extremum Search

```
PROBLEM: Find any local peak
ARRAY: [1, 3, 5, 4, 2]  (unsorted, guaranteed to have peak)

DEFINITION:
arr[i] is a peak if:
  arr[i-1] < arr[i] AND arr[i] < arr[i+1]
  (or boundary conditions)

BINARY SEARCH STRATEGY:
Compare mid with right neighbor

lo=0                        hi=4
mid = 2: arr[2]=5
  Compare right: arr[3]=4
  5 > 4 → Peak could be at mid or left
  hi = mid = 2

lo=0        hi=2
mid = 1: arr[1]=3
  Compare right: arr[2]=5
  3 < 5 → Peak must be to the right
  lo = mid + 1 = 2

lo=2    hi=2  → ANSWER = 2 (arr[2]=5)

INVARIANT:
┌──────────────────────────────────────┐
│ If arr[mid] > arr[mid+1]:            │
│   Peak in [lo..mid] (maybe mid)      │
│                                      │
│ If arr[mid] < arr[mid+1]:            │
│   Peak must be in [mid+1..hi]        │
│   (arr increasing, so something      │
│    higher must exist to the right)   │
└──────────────────────────────────────┘

TIME: O(log n) | SPACE: O(1)
```

---

### Common Failure Modes (Day 5)

#### Failure 1: Infinite Loop from Wrong Update

```
❌ WRONG:
if (arr[mid] < target)
  lo = mid  ← Infinite loop! lo never advances

✓ CORRECT:
if (arr[mid] < target)
  lo = mid + 1  ← Strict progress
```

#### Failure 2: Wrong Comparison for First vs Last

```
❌ WRONG (confusing first and last):
First occurrence: if (arr[mid] == target) lo = mid + 1
  → Skips all matching elements, finds wrong position

✓ CORRECT:
First occurrence: if (arr[mid] >= target) hi = mid
  → Narrows to left boundary
```

#### Failure 3: Incorrect Feasibility Check

```
❌ WRONG (feasibility doesn't satisfy monotonicity):
Feasibility checks: is_feasible(mid)
But is_feasible(mid) doesn't increase with capacity
  → Binary search finds wrong answer

✓ CORRECT: Verify
if feasible(mid) → feasible(mid+1)  ✓
if not feasible(mid) → not feasible(mid-1)  ✓
```

---

### Mini Review Quiz (Day 5)

**Q1:** For binary search on answer space, what property must hold?
```
A) Answer space must be sorted
B) Feasibility must be monotonic
C) Target must exist in array
D) Answer space must be contiguous
```
**Answer:** B

**Q2:** When finding a peak, why do we compare mid with right neighbor?
```
A) Random choice
B) If mid < right, peak must be to the right
C) If mid > right, peak must be to the left
D) Both B and C are valid reasoning
```
**Answer:** D

**Q3:** What overflow risk exists in mid calculation?
```
A) Integer division error
B) Signed/unsigned mismatch
C) lo + hi sum overflow
D) Array index out of bounds
```
**Answer:** C (mitigated by `mid = lo + (hi-lo)/2`)

---

## 🎯 WEEK 04 VISUAL SUMMARY TABLE

```
┌─────────────────────────────────────────────────────────────┐
│ DAY │ PATTERN              │ KEY VISUAL          │ COMPLEXITY│
├─────────────────────────────────────────────────────────────┤
│ 1   │ Two-Pointer          │ Pointer convergence │ O(n)      │
│     │ (same & opposite)    │ Invariant zones     │ O(1) space│
│     │                      │                     │           │
│ 2   │ Sliding Window        │ Window slide        │ O(n)      │
│     │ (fixed size)         │ Monotonic deque     │ O(k) space│
│     │                      │ State machine       │           │
│     │                      │                     │           │
│ 3   │ Sliding Window        │ Expand-contract     │ O(n)      │
│     │ (variable size)      │ Frequency mapping   │ O(charset)│
│     │                      │ Permutation search  │ space     │
│     │                      │                     │           │
│ 4   │ Divide & Conquer     │ Recursion tree      │ O(n logn) │
│     │                      │ Master theorem      │ O(n) space│
│     │                      │ Inversion counting  │           │
│     │                      │                     │           │
│ 5   │ Binary Search        │ Range narrows       │ O(log n)  │
│     │ (pattern + answer)   │ Feasibility curve   │ O(1) space│
│     │                      │ Peak finding        │           │
└─────────────────────────────────────────────────────────────┘
```

---

## 📋 COMMON PATTERNS QUICK REFERENCE

```
Pattern              │ Use When                        │ Time/Space
─────────────────────┼─────────────────────────────────┼──────────────
Two-pointer opposite │ Find pair sum, container, 3-sum │ O(n) / O(1)
Two-pointer same-dir │ In-place remove/partition       │ O(n) / O(1)
Fixed window         │ Max/min k consecutive          │ O(n) / O(k)
Monotonic deque      │ Sliding max/min with all values│ O(n) / O(k)
Variable window      │ At most k distinct, min window │ O(n) / O(k)
Merge sort           │ Sort + count inversions        │ O(n logn)/O(n)
Partition sort       │ Find kth smallest, sort        │ O(n) avg/O(n)
Binary search        │ Search in sorted, answer space │ O(log n)/O(1)
Peak finding         │ Local max in unsorted array    │ O(log n)/O(1)
```

---

