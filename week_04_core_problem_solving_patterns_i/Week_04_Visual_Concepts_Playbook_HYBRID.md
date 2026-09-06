# 📊 WEEK 04 VISUAL CONCEPTS PLAYBOOK (HYBRID)

**Week:** 4 | **Tier:** Core Problem-Solving Patterns I  
**Theme:** Two Pointers, Sliding Windows, Divide & Conquer, Binary Search  
**Format:** Hybrid (Enhanced ASCII + Web Resource Links + Reference Tools)  
**Purpose:** Visual-first concept explanation with embedded professional resources

---

## 🎨 VISUAL LEGEND & RESOURCE GUIDE

### Symbol Reference
| Symbol | Meaning |
|--------|---------|
| `L` / `R` | Left and right pointers |
| `lo` / `hi` / `mid` | Binary search bounds |
| `⬅️ ➡️` | Pointer movement |
| `✓` | Valid state |
| `✗` | Invalid state |
| 🔗 | Link to interactive visualization |

### Professional Visualization Resources

| Tool | Resource | Best For |
|------|----------|----------|
| **VisuAlgo** | https://visualgo.net | Binary search trees, data structures |
| **LabulaDong** | https://labuladong.online/algo/en/essential-technique/sliding-window-framework/ | Sliding window with interactive panels |
| **HelloInterview** | https://www.hellointerview.com/learn/code/two-pointers/overview | Two-pointer with real-time coding |
| **GeeksforGeeks Two-Pointers** | https://www.geeksforgeeks.org/dsa/two-pointers-technique/ | Comprehensive two-pointer examples |
| **GeeksforGeeks Sliding Window** | https://www.geeksforgeeks.org/dsa/window-sliding-technique/ | Sliding window technique patterns |
| **ByteByteGo** | https://bytebytego.com/courses/coding-patterns/two-pointers | Visual coding patterns course |

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

**Interactive Resource:** 🔗 [GeeksforGeeks Two-Pointers Examples](https://www.geeksforgeeks.org/dsa/two-pointers-technique/)

#### Visual 1: Array State Evolution

```
INITIAL:  [1, 0, 2, 0, 3]
          
STEP 1: Read pointer at 0 (value=1)
        ┌─ Read pointer
        ▼
        [1, 0, 2, 0, 3]
        ▲
        └─ Write pointer (not yet moved)
        
        1 ≠ 0, so advance both pointers
        
STEP 2: Read pointer at 1 (value=0)
           ┌─ Read pointer
           ▼
        [1, 0, 2, 0, 3]
        ▲
        └─ Write pointer (stays at 1)
        
        0 == 0, so read++ but write stays

STEP 3: Read pointer at 2 (value=2)
              ┌─ Read pointer
              ▼
        [1, 2, 0, 0, 3]    ← After swap
        ▲
        └─ Write pointer (now at 1)
        
        2 ≠ 0, so swap and advance both

STEP 4: Continue until read reaches end

FINAL:     [1, 2, 3, 0, 0]

INVARIANT:
┌─────────────────────────────────────────────────┐
│ [0..writePos)  = All non-zero elements (processed)
│ [writePos..end] = All zero elements (pushed back)
└─────────────────────────────────────────────────┘

TIME: O(n) | SPACE: O(1)
```

#### Visual 2: Write Pointer as Safe Zone Boundary

```
CONCEPT: Write pointer tracks "where next non-zero goes"

ZONE DEFINITION:
┌─────────────────────────────────────────────────┐
│ [0..writePos-1]  ││ [writePos..readPos]  ││ [readPos+1..n]
│  SAFE (all non-0) ││ BUFFER (mixed)      ││ TO PROCESS
└─────────────────────────────────────────────────┘
                      ↑
                 Write pointer position
                 
When readPos element is non-zero:
  - Copy to writePos
  - Increment writePos
  - Readd is now guaranteed non-zero in final position

This guarantees O(1) space: in-place rearrangement
```

---

### Pattern 1.2: Opposite-Direction Pointers (Container with Most Water)

**Interactive Resource:** 🔗 [ByteByteGo - Two Pointers Pattern](https://bytebytego.com/courses/coding-patterns/two-pointers/introduction-to-two-pointers?fpr=javarevisited)

#### Visual 1: Greedy Pointer Movement Proof

```
PROBLEM: Find max water container [heights], pointers at ends

PRINCIPLE: Always move the SHORTER pointer inward

ARRAY: [1, 8, 6, 2, 5, 4, 8, 3, 7]
        0  1  2  3  4  5  6  7  8  (indices)

Initial:
L=0(h=1)                           R=8(h=7)
├─────────────────────────────────────┤
Area = min(1, 7) × 8 = 8  ← Current

Decision Tree:
h[L]=1 < h[R]=7  (LEFT is shorter)
  └─→ MOVE LEFT (not right!)

WHY NOT MOVE RIGHT?
  If we move R to 7:
    New area = min(1, 3) × 7 = 7  ← WORSE or EQUAL
  
  If we move L to 1:
    New area = min(8, 7) × 7 = 49  ← BETTER!

INSIGHT:
┌────────────────────────────────────────┐
│ Moving shorter pointer:                │
│  - Width decreases by 1                │
│  - Height might increase (GAIN!)       │
│  - Worth the risk                      │
│                                        │
│ Moving taller pointer:                 │
│  - Width decreases by 1                │
│  - Height CANNOT increase (LOSS!)      │
│  - Guaranteed to be worse or same      │
└────────────────────────────────────────┘

TIME: O(n) | SPACE: O(1)
```

#### Visual 2: Iteration Trace

```
STATE TRACE:
Iteration │ L  │ h[L] │ R  │ h[R] │ Area │ Action
──────────┼────┼──────┼────┼──────┼──────┼─────────────
0         │ 0  │ 1    │ 8  │ 7    │ 8    │ L<R, move L→
1         │ 1  │ 8    │ 8  │ 7    │ 49   │ L>R, move R←
2         │ 1  │ 8    │ 7  │ 3    │ 24   │ L>R, move R←
3         │ 1  │ 8    │ 6  │ 8    │ 40   │ L<R, move L→
4         │ 2  │ 6    │ 6  │ 8    │ 30   │ L<R, move L→
5         │ 3  │ 2    │ 6  │ 8    │ 10   │ L<R, move L→
6         │ 4  │ 5    │ 6  │ 8    │ 8    │ L<R, move L→
7         │ 5  │ 4    │ 6  │ 8    │ 4    │ L≥R, STOP

RESULT: MAX = 49 (when L=1, R=8)
```

---

### Common Failure Modes (Visual)

#### Failure 1: Off-by-One in Invariant

```
❌ WRONG INVARIANT (closed interval):
[0..writePos] = processed
     ↑ includes writePos

Problem: Inconsistent boundary
  - Is writePos processed? (unclear!)
  - Leads to off-by-one errors

✓ CORRECT INVARIANT (half-open interval):
[0..writePos) = processed
     ↑ excludes writePos (half-open)

Benefit: Clear boundary
  - Everything before writePos is safe
  - writePos is next position to fill
  - No ambiguity!
```

#### Failure 2: Wrong Direction Logic

```
❌ Problem: Container with water

If you move the TALLER pointer inward:
  [1, 8, 6, 2, 5, 4, 8, 3, 7]
   L(1)                    R(7)
   
   Move R ← to 3:
   Area = min(1, 3) × 7 = 7
          (from 8) ↓ WORSE!

✓ Always move SHORTER pointer:
  
  Move L → to 8:
  Area = min(8, 7) × 7 = 49  ↑ BETTER!
```

#### Failure 3: Confusing Converging with Sliding

```
❌ WRONG: Using converging logic for sliding window

Converging (Two-Sum):
  L at start, R at end
  Move INWARD based on sum comparison
  Pointers never cross

Sliding (Subarray):
  L at start, R scanning right
  Move OUTWARD to grow window
  Move L inward to shrink
  Pointers can cross multiple times

❌ Mixing them causes wrong results
✓ Know which pattern you're solving!
```

---

### Mini Review Quiz (Day 1)

**Q1:** In same-direction pointers, what invariant must hold?

```
A) [0..writePos] = processed
B) [0..writePos) = non-zeros | [writePos..end) = zeros
C) [0..i) = processed | [i..end) = unprocessed  
D) Pointers never cross
```
**✅ Answer:** B (half-open interval, explicit zones!)

**Q2:** Why do we move the shorter pointer in container water?

```
A) Alphabetically, 'L' comes before 'R'
B) Moving taller pointer guarantees worse area
C) It's always correct for two-pointer
D) Random choice works
```
**✅ Answer:** B (mathematical guarantee!)

**Q3:** What's the main benefit of write pointer?

```
A) Faster than using temporary array
B) Uses O(1) extra space (in-place)
C) Always finds lexicographically smallest result
D) Makes code shorter
```
**✅ Answer:** B (in-place transformation!)

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

### Pattern 2.1: Fixed Window Mechanics & Sliding

**Interactive Resource:** 🔗 [LabulaDong - Sliding Window Framework](https://labuladong.online/algo/en/essential-technique/sliding-window-framework/) (with visualization panel!)

#### Visual 1: Window Slide Storyboard

```
ARRAY: [1, 2, 3, 4, 5]  k=3  (find max sum k consecutive)

═══════════════════════════════════════════════════════
FRAME 1: BUILD INITIAL WINDOW
═══════════════════════════════════════════════════════

┌──────────┐
│ 1  2  3 │ 4  5
└──────────┘
 ↑window[0,2]
 
Initial sum = 1+2+3 = 6
Record: MAX = 6

═══════════════════════════════════════════════════════
FRAME 2: SLIDE RIGHT (remove 1, add 4)
═══════════════════════════════════════════════════════

     ┌──────────┐
 1 │ 2  3  4 │ 5
     └──────────┘
    window[1,3]
    
New sum = 6 - 1 + 4 = 9
Update: MAX = 9

═══════════════════════════════════════════════════════
FRAME 3: SLIDE RIGHT (remove 2, add 5)
═══════════════════════════════════════════════════════

        ┌──────────┐
 1  2 │ 3  4  5 │
        └──────────┘
       window[2,4]
       
New sum = 9 - 2 + 5 = 12
Update: MAX = 12

═══════════════════════════════════════════════════════

RESULT: Maximum sum = 12

KEY OPERATION:
┌─────────────────────────────────┐
│ newSum = oldSum - A[i-k] + A[i] │
│ (remove left, add right)        │
└─────────────────────────────────┘

TIME: O(n) vs O(n*k) naive!
SPACE: O(1)
```

#### Visual 2: Complexity Comparison

```
APPROACH COMPARISON:

NAIVE O(n*k):
┌──────────────────────┐
│ for i=0 to n-1:      │
│   sum = 0            │
│   for j=i to i+k-1:  │ ← Recalculate every time!
│     sum += A[j]      │
│   update max         │
└──────────────────────┘
Redundant work: recompute shared elements

SLIDING WINDOW O(n):
┌──────────────────────┐
│ sum = initial[0..k]  │
│ for i=k to n-1:      │
│   sum -= A[i-k]      │ ← 2 operations
│   sum += A[i]        │ ← per position
│   update max         │
└──────────────────────┘
Efficient: reuse previous sum

TIME SAVED:
┌─────────────────────────┐
│ n=1000, k=100          │
│ Naive: 1000 × 100 = 100K ops
│ Sliding: 1000 ops       │
│ Speedup: 100× FASTER!   │
└─────────────────────────┘
```

---

### Pattern 2.2: Monotonic Deque for Max/Min Window

**Interactive Resource:** 🔗 [GeeksforGeeks Sliding Window](https://www.geeksforgeeks.org/dsa/window-sliding-technique/)

#### Visual 1: Monotonic Deque State Evolution

```
PROBLEM: Find max value in every window [1,3,-1,-3,5,3,6,7], k=3

KEY INSIGHT: Deque maintains DECREASING order by VALUE
  └─ Front = current window's maximum
  └─ Back = candidates for future windows

TRACE:

Window [1, 3, -1]:
  Process 1: deque=[1]
  Process 3: 3>1, remove 1, deque=[3]
  Process -1: -1<3, append, deque=[3,-1]
  Output: 3 (front of deque)

Window [3, -1, -3]:
  Remove 1 (out of window)
  deque=[3,-1]
  Process -3: -3<-1, append, deque=[3,-1,-3]
  Output: 3

Window [-1, -3, 5]:
  Remove 3 (out of window)
  deque=[-1,-3]
  Process 5: 5>-3, 5>-1, clear, deque=[5]
  Output: 5

Window [-3, 5, 3]:
  deque=[5]
  Process 3: 3<5, append, deque=[5,3]
  Output: 5

Window [5, 3, 6]:
  Remove -3
  deque=[5,3]
  Process 6: 6>3, 6>5, clear, deque=[6]
  Output: 6

Window [3, 6, 7]:
  Remove 5
  deque=[6]
  Process 7: 7>6, remove 6, deque=[7]
  Output: 7

RESULT: [3, 3, 5, 5, 6, 7]

DEQUE INVARIANT:
┌──────────────────────────────┐
│ All elements in decreasing   │
│ order by VALUE               │
│ Front = current window max   │
│ Back = future candidates     │
└──────────────────────────────┘

WHY IT WORKS:
  If X is smaller than Y and enters after Y,
  X can never be max before Y leaves
  → Safe to remove X from consideration
  
TIME: O(n) amortized
SPACE: O(k) for deque
```

---

### Common Failure Modes (Day 2)

#### Failure 1: Not Checking Window Boundary

```
❌ WRONG:
for i=0 to n-1:
  deque.addLast(i)
  output deque.front()  ← Window not valid yet!

Result: Wrong output at beginning

✓ CORRECT:
for i=0 to n-1:
  deque.addLast(i)
  if i >= k-1:  ← Only output when window is full
    output deque.front()

Result: Correct output starting at index k-1
```

#### Failure 2: Forgetting to Remove Out-of-Window

```
❌ WRONG:
deque.addLast(i)
  (no removal of old indices)

Result: Deque grows beyond window size

✓ CORRECT:
while !deque.empty() && deque.front() <= i - k:
  deque.removeFirst()  ← Remove indices outside window
deque.addLast(i)

Result: Deque only contains current window indices
```

#### Failure 3: Wrong Pruning Condition

```
❌ WRONG:
while nums[deque.back()] < nums[i]:
  deque.removeLast()

Result: May remove elements needed for future windows

✓ CORRECT (monotonic decreasing):
while !deque.empty() && nums[deque.back()] <= nums[i]:
  deque.removeLast()

Result: Maintains strict decreasing order properly
```

---

### Performance Comparison Table (Day 2)

```
┌──────────────────────────────────────────────────────────────┐
│ Algorithm                 │ Time       │ Space │ Best For   │
├──────────────────────────────────────────────────────────────┤
│ Naive (recalc each)       │ O(n×k)     │ O(1)  │ k very small
│ Fixed window sum          │ O(n)       │ O(1)  │ Aggregation 
│ Prefix sum + queries      │ O(n+q)     │ O(n)  │ Batch queries
│ Monotonic deque           │ O(n)       │ O(k)  │ Max/min window
│ Segment tree              │ O(n log n) │ O(n)  │ Range queries
└──────────────────────────────────────────────────────────────┘
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

**Interactive Resource:** 🔗 [HelloInterview - Sliding Window Patterns](https://www.hellointerview.com/learn/code/two-pointers/overview)

#### Visual 1: Two-Phase Decision Flow

```
PROBLEM: "Minimum window substring"
Target: "ABC", String: "ADOBECODEBANC"

PHASE 1: EXPAND (move right)
┌─────────────────────────────┐
│ EXPAND until VALID:        │
│                             │
│ right = 0: "A" (need B,C)   │
│ right = 1: "AD" (need B,C)  │
│ right = 2: "ADO" (need B,C) │
│ right = 3: "ADOB" (need C)  │
│ right = 4: "ADOBE" (have all!) ✓ VALID
└─────────────────────────────┘

PHASE 2: CONTRACT (move left)
┌─────────────────────────────┐
│ While VALID, shrink:        │
│                             │
│ left=0: "ADOBE" (have all) ✓
│   → Remove A? "DOBE" (lost A) ✗ INVALID
└─────────────────────────────┘

Back to PHASE 1: EXPAND
┌─────────────────────────────┐
│ right = 5: "DOBEC" ✓ VALID  │
│ CONTRACT: Remove D? "OBEC" ✓ Still valid!
│ CONTRACT: Remove O? "BEC" ✗ Lost B
│ Back to EXPAND              │
└─────────────────────────────┘

Continue until right reaches end...

RESULT: MIN WINDOW = "BANC"

DECISION LOGIC:
┌──────────────────────────────┐
│ if valid:                   │
│   record result             │
│   try shrink (left++)       │
│ else:                       │
│   expand (right++)          │
└──────────────────────────────┘
```

---

### Pattern 3.2: At Most K Distinct Characters

#### Visual 1: Constraint Zones

```
PROBLEM: Longest substring with AT MOST k=2 distinct chars
STRING: "eceba"

ZONE VISUALIZATION:

[0:0] "e" (1 distinct) ✓
[0:1] "ec" (2 distinct) ✓  MAX so far = 2
[0:2] "ece" (2 distinct) ✓  MAX so far = 3
[0:3] "eceb" (3 distinct) ✗ TOO MANY!

Shrink: Remove "e" from left
[1:3] "ceb" (3 distinct) ✗ Still too many

Shrink: Remove "c"
[2:3] "eb" (2 distinct) ✓  Can expand again

[2:4] "eba" (3 distinct) ✗ Too many again

Shrink: Remove "e"
[3:4] "ba" (2 distinct) ✓

RESULT: Longest = 3 (substring "ece")

CONTRACT CONDITION:
┌──────────────────────────────┐
│ while distinct_count > k:    │
│   remove A[left]             │
│   if count becomes 0:        │
│     remove from map          │
│   left++                     │
└──────────────────────────────┘
```

---

### Common Failure Modes (Day 3)

#### Failure 1: Not Maintaining Valid Window Properly

```
❌ WRONG: Record result before checking validity

for right in array:
  add A[right]
  record result  ← May not be valid!
  while not valid:
    shrink

Result: Invalid results recorded

✓ CORRECT: Check validity BEFORE recording

for right in array:
  add A[right]
  while not valid:
    shrink
  record result  ← Now guaranteed valid

Result: Only valid results recorded
```

#### Failure 2: Forgetting to Update Frequency Map

```
❌ WRONG: Modify sum but not frequency map

remove A[left]:
  sum -= A[left]
  left++
  (forgot to update charCount)

Result: charCount is stale, wrong decisions

✓ CORRECT: Always sync frequency map

remove A[left]:
  charCount[A[left]]--
  if charCount[A[left]] == 0:
    remove(A[left])  ← Clean up
  sum -= A[left]
  left++

Result: charCount always accurate
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

#### Visual 1: Tree Structure & Levels

```
ARRAY: [38, 27, 43, 3, 9, 82, 10]

                [38,27,43,3,9,82,10]
                /                 \
              /                     \
        [38,27,43,3]          [9,82,10]
        /         \            /      \
      /             \        /          \
    [38,27]    [43,3]   [9,82]      [10]
    /    \      /    \   /    \       |
  [38]  [27]  [43]  [3][9]  [82]  (base)

MERGE PHASE:
  [27,38]  [3,43]    [9,82]   [10]
     |        |         |       |
     └────────┴─────────┴───────┘
     |
  [3,27,38,43]   [9,10,82]
     |              |
     └──────┬───────┘
            |
        [3,9,10,27,38,43,82]

LEVEL ANALYSIS:
  Level 0 (unsorted): 1 array of n elements, work = n
  Level 1: 2 arrays of n/2 each, total work = 2×(n/2) = n
  Level 2: 4 arrays of n/4 each, total work = 4×(n/4) = n
  ...
  Level log₂(n): n base cases, each O(1)

TOTAL WORK: n × log₂(n) = O(n log n)

KEY INSIGHT:
┌───────────────────────────────┐
│ Each level does O(n) work    │
│ There are O(log n) levels    │
│ Total: O(n log n) GUARANTEED │
│ (unlike quicksort worst case)│
└───────────────────────────────┘

TIME: O(n log n) | SPACE: O(n)
```

---

### Pattern 4.2: Counting Inversions via Merge

#### Visual 1: Inversion Detection

```
PROBLEM: Count pairs (i,j) where i<j but arr[i]>arr[j]
ARRAY: [2, 4, 1, 3, 5]

MERGE SORT + COUNT:

[2,4,1,3,5]
  |     |
  ↓     ↓
[2,4]  [1,3,5]  ← Split
  |     |
  ↓     ↓
[2][4] [1][3][5]

MERGE [2] and [4]:
  → No inversions, result [2,4]

MERGE [1] and [3]:
  → No inversions, result [1,3]

MERGE [2,4] and [1]:
  When 1 < 2:
    → 1 is smaller, take 1
    → 2 and 4 remain, both > 1
    → Add 2 inversions: (2,1) and (4,1)
  Result: [1,2,4], inversions = 2

MERGE [1,2,4] and [3,5]:
  Compare 1 vs 3: 1<3, take 1
  Compare 2 vs 3: 2<3, take 2
  Compare 4 vs 3: 4>3, take 3
    → 4 and remaining elements > 3
    → Add 1 inversion: (4,3)
  Continue...

TOTAL INVERSIONS: 2 + 1 + ... = 4

THE MAGIC:
┌──────────────────────────────┐
│ When right < left during     │
│ merge, ALL remaining left    │
│ elements are inversions:     │
│                              │
│ inversions += (mid - i + 1)  │
│                              │
│ This counts efficiently in   │
│ O(n log n) vs O(n²) brute   │
└──────────────────────────────┘

TIME: O(n log n) | SPACE: O(n)
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

### Pattern 5.1: Binary Search Invariant & Overflow Safety

#### Visual 1: Range Narrows at Each Step

```
ARRAY: [-3, -1, 0, 2, 4, 6, 8, 10]
TARGET: 4

ITERATION 0:
lo=0                                hi=7
├────────────────────────────────────┤
mid = 0 + (7-0)/2 = 3
arr[3] = 2 < 4 → target is to the right
lo = 4  (mid+1, not mid!)

ITERATION 1:
           lo=4           hi=7
           ├──────────────┤
           mid = 4 + (7-4)/2 = 5
           arr[5] = 6 > 4 → target is to the left
           hi = 4  (mid-1, not mid!)

ITERATION 2:
           lo=4   hi=4
           ├──────┤
           mid = 4 + (4-4)/2 = 4
           arr[4] = 4 == 4 → FOUND!

INVARIANT MAINTAINED:
┌────────────────────────────────┐
│ Target always in [lo, hi]      │
│ Each step: range shrinks to 1/2│
│ Total steps: O(log n)          │
└────────────────────────────────┘

OVERFLOW SAFETY:
❌ WRONG: mid = (lo + hi) / 2
   If lo=2^31-1, hi=2^31-1:
     lo+hi = 2^32-2 → OVERFLOW!

✓ CORRECT: mid = lo + (hi - lo) / 2
   If lo=2^31-1, hi=2^31-1:
     hi-lo = 0
     mid = 2^31-1 + 0 = 2^31-1 → Safe!

TIME: O(log n) | SPACE: O(1)
```

---

### Pattern 5.2: Binary Search on Answer Space

#### Visual 1: Feasibility Curve (Monotonic Property)

```
PROBLEM: Minimum capacity to ship packages in k days
PACKAGES: [1,2,3,4,5]  DAYS: 3

FEASIBILITY vs CAPACITY:
Capacity │ Feasible in 3 days?
──────────┼─────────────────
1        │ ✗ (can't ship [1,2,3,4,5] at all)
2        │ ✗ (need multiple days for each)
3        │ ✗
4        │ ✗
5        │ ✗
6        │ ✗
7        │ ✗
8        │ ✗
9        │ ✗
10       │ ✗
11       │ ✗
12       │ ✗
13       │ ✗
14       │ ✗
15 (total)│ ✓  ← Can ship everything in 1 day

MONOTONIC PROPERTY:
┌──────────────────────────────────┐
│ ✗ ✗ ✗ ✗ ✗ ✗ ✓ ✓ ✓ ✓ ✓       │
│              ↑ Boundary!        │
│                                 │
│ Once feasible, all larger also  │
│ feasible → Monotonic increasing │
└──────────────────────────────────┘

BINARY SEARCH FINDS BOUNDARY:
lo = 1  (too small)
hi = 15 (sum of all)

mid = 1 + (15-1)/2 = 8
  Can ship in 3 days with capacity 8?
    [1,2,3],[4],[5] = 2 days ✓
  → Answer might be smaller, hi = 7

mid = 1 + (7-1)/2 = 4
  Can ship in 3 days with capacity 4?
    [1,2],[3],[4],[5] = 4 days ✗
  → Answer is larger, lo = 5

mid = 5 + (7-5)/2 = 6
  Can ship in 3 days with capacity 6?
    [1,2,3],[4],[5] = 2 days ✓
  → Answer might be smaller, hi = 5

lo >= hi → ANSWER = 5

This is why binary search on answer space works!
```

---

### Pattern 5.3: Binary Search Templates & Edge Cases

#### Visual 1: First vs Last Occurrence Template

```
ARRAY: [1, 2, 2, 2, 3, 4]
TARGET: 2

FIND FIRST (leftmost 2):
Template:
  if arr[mid] >= target:
    hi = mid  (answer could be here or left)
  else:
    lo = mid + 1

Trace:
  lo=0, hi=5
  mid=2: arr[2]=2 >= 2, hi=2
  lo=0, hi=2
  mid=1: arr[1]=2 >= 2, hi=1
  lo=0, hi=1
  mid=0: arr[0]=1 < 2, lo=1
  lo=1, hi=1
  Result: arr[1] = 2 (first occurrence)

FIND LAST (rightmost 2):
Template:
  if arr[mid] <= target:
    lo = mid + 1  (answer could be here or right)
  else:
    hi = mid

Trace:
  lo=0, hi=5
  mid=2: arr[2]=2 <= 2, lo=3
  lo=3, hi=5
  mid=4: arr[4]=3 > 2, hi=4
  lo=3, hi=4
  mid=3: arr[3]=2 <= 2, lo=4
  lo=4, hi=4
  Result: arr[3] = 2 (last occurrence)
```

---

### Common Failure Modes (Day 5)

#### Failure 1: Infinite Loop from No Progress

```
❌ WRONG:
if arr[mid] < target:
  lo = mid  ← No progress! lo doesn't advance

This creates infinite loop if mid = lo

✓ CORRECT:
if arr[mid] < target:
  lo = mid + 1  ← Always make progress

This ensures lo and hi eventually converge
```

#### Failure 2: Wrong Comparison for First vs Last

```
❌ WRONG (confusing templates):
Find first: if (arr[mid] == target) hi = mid-1
  This skips the target entirely!

✓ CORRECT:
Find first: if (arr[mid] >= target) hi = mid
  This narrows to leftmost boundary

Find last: if (arr[mid] <= target) lo = mid+1
  This narrows to rightmost boundary
```

---

## 🎯 WEEK 04 VISUAL SUMMARY TABLE

```
┌────────────────────────────────────────────────────────────────┐
│ DAY │ PATTERN            │ Key Visual Type  │ Complexity       │
├────────────────────────────────────────────────────────────────┤
│ 1   │ Two-Pointer        │ Pointer zones    │ O(n) / O(1)      │
│     │ Opposite-dir/      │ Convergence      │ space            │
│     │ Same-dir           │ diagrams         │                  │
│     │                    │                  │                  │
│ 2   │ Sliding Window     │ Window slide     │ O(n) / O(k)      │
│     │ Fixed Size         │ storyboard       │ space            │
│     │ + Deque            │ Monotonic deque  │                  │
│     │                    │                  │                  │
│ 3   │ Sliding Window     │ Expand-contract  │ O(n) /           │
│     │ Variable Size      │ Frequency map    │ O(charset)       │
│     │                    │ Constraint zones │ space            │
│     │                    │                  │                  │
│ 4   │ Divide & Conquer   │ Recursion tree   │ O(n log n) /     │
│     │ (Merge Sort,       │ Level analysis   │ O(n) space       │
│     │ Inversions)        │ Inversion count  │                  │
│     │                    │                  │                  │
│ 5   │ Binary Search      │ Range narrowing  │ O(log n) /       │
│     │ (Classic +         │ Feasibility      │ O(1) space       │
│     │ Answer Space)      │ curve            │                  │
│     │                    │ Peak finding     │                  │
└────────────────────────────────────────────────────────────────┘
```

---

## 📋 COMMON PATTERNS QUICK REFERENCE

```
Pattern              │ Use When                           │ Time/Space
─────────────────────┼────────────────────────────────────┼──────────────
Two-pointer opposite │ Find pair sum, container, 3-sum    │ O(n) / O(1)
Two-pointer same-dir │ In-place remove/partition          │ O(n) / O(1)
Fixed window         │ Max/min k consecutive              │ O(n) / O(k)
Monotonic deque      │ Sliding max/min with all values    │ O(n) / O(k)
Variable window      │ At most k distinct, min window     │ O(n) / O(k)
Merge sort           │ Sort + count inversions            │ O(n logn)/O(n)
Partition sort       │ Find kth smallest, sort            │ O(n) avg/O(n)
Binary search        │ Search in sorted, answer space     │ Time O(log n), Space O(1)
Peak finding         │ Local max in unsorted array        │ Time O(log n), Space O(1)
```

---

## 🔗 RECOMMENDED LEARNING RESOURCES

### Interactive Visualizations
1. **VisuAlgo** (https://visualgo.net) — Best for data structure visualization
2. **LabulaDong** (https://labuladong.online) — Sliding window framework with panels
3. **HelloInterview** (https://www.hellointerview.com) — Real-time coding practice
4. **ByteByteGo** (https://bytebytego.com) — Professional coding pattern courses

### Comprehensive Guides
- **GeeksforGeeks Two-Pointers** - Full technique explanations
- **GeeksforGeeks Sliding Window** - Sliding window patterns
- **USACO Guide** - Two-pointer problems and solutions

### Video Tutorials
- Sliding Window in 7 minutes (AlgoMaster) — Quick visual intro
- Binary Search visualizations — Recursion and range narrowing

---

## 📝 HOW TO USE THIS PLAYBOOK

### Quick Revision (30 mins)
1. Scan pattern maps (5 mins)
2. Read one day's main visuals (5 mins per day)
3. Answer mini quiz (3 mins per day)
4. Review failure modes (2 mins per day)

### Deep Learning (2-3 hours)
1. Read playbook + extended subtopics guide
2. Visit web resource links for interactive visualizations
3. Implement code from main instructional files
4. Solve practice problems using visuals as reference

### Interview Prep
1. Open playbook for quick pattern reminders
2. Use resource links for visual refresh
3. Mentally trace algorithm using playbook diagrams
4. Code from memory with confidence

---

**Use web resource links for interactive visualizations while studying!**
