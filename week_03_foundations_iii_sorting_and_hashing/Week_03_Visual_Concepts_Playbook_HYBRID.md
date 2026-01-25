# 📊 WEEK 03 VISUAL CONCEPTS PLAYBOOK (HYBRID)

**Week:** 3 | **Tier:** Foundations III – Sorting, Heaps, Hashing  
**Theme:** Elementary Sorts, Merge/Quick Sort, Heaps, Hash Tables, String Hashing  
**Format:** Hybrid (Enhanced ASCII + Web Resource Links + Reference Tools)  
**Purpose:** Visual-first concept explanation with embedded professional resources

---

## 🎨 VISUAL LEGEND & RESOURCE GUIDE

### Symbol Reference
| Symbol | Meaning |
|--------|---------|
| `i` / `j` | Index pointers (array position) |
| `┌─┐` | Array element or cell |
| `█` | Sorted element |
| `░` | Unsorted element |
| `▶` | Direction of movement |
| `⇄` | Swap operation |
| `✓` | Valid state |
| `✗` | Invalid state |
| 🔗 | Link to interactive visualization |

### Professional Visualization Resources

| Tool | Resource | Best For |
|------|----------|----------|
| **VisuAlgo** | https://visualgo.net/en/sorting | Sorting algorithm animations |
| **Sorting Visualizer** | https://www.toptal.com/developers/sorting-visualizer | Side-by-side sort comparison |
| **Heap Visualizer** | https://www.cs.usfca.edu/~galles/visualization/Heap.html | Heap operations step-by-step |
| **GeeksforGeeks Sorting** | https://www.geeksforgeeks.org/sorting-algorithms/ | Comprehensive sorting guide |
| **GeeksforGeeks Heaps** | https://www.geeksforgeeks.org/heap-data-structure/ | Heap operations and problems |
| **GeeksforGeeks Hashing** | https://www.geeksforgeeks.org/hashing-data-structure/ | Hash tables and functions |

---

## 📅 DAY 1: ELEMENTARY SORTS

### Pattern Map: Elementary Sorts Family Tree

```
ELEMENTARY SORTS
├─ Bubble Sort (Sinking)
│  ├─ Adjacent element swaps
│  ├─ Repeatedly bubbles largest to end
│  └─ O(n²) time, O(1) space
│
├─ Selection Sort (Finding)
│  ├─ Find min/max element
│  ├─ Place at correct position
│  └─ O(n²) time, O(1) space
│
└─ Insertion Sort (Growing)
   ├─ Build sorted prefix
   ├─ Insert next element
   └─ O(n²) but adaptive for nearly sorted
```

---

### Pattern 1.1: Bubble Sort - Adjacent Comparisons

**Interactive Resource:** 🔗 [VisuAlgo Sorting Visualization](https://visualgo.net/en/sorting)

#### Visual 1: Pass-by-Pass Evolution

```
ARRAY: [5, 2, 8, 1, 9]

PASS 1: Compare adjacent pairs, bubble largest right
─────────────────────────────────────────────────

STEP 1: Compare [5,2]
┌─┬─┬─┬─┬─┐
│5│2│8│1│9│
└─┴─┴─┴─┴─┘
 ↑↑
Swap: 5 > 2

STEP 2: Compare [5,8]
┌─┬─┬─┬─┬─┐
│2│5│8│1│9│
└─┴─┴─┴─┴─┘
   ↑↑
No swap: 5 < 8

STEP 3: Compare [8,1]
┌─┬─┬─┬─┬─┐
│2│5│8│1│9│
└─┴─┴─┴─┴─┘
     ↑↑
Swap: 8 > 1

STEP 4: Compare [8,9]
┌─┬─┬─┬─┬─┐
│2│5│1│8│9│
└─┴─┴─┴─┴─┘
       ↑↑
No swap: 8 < 9

AFTER PASS 1: [2, 5, 1, 8, 9]
              └──────────────┘
              Largest (9) is in place

PASS 2: Repeat but exclude rightmost
─────────────────────────────────────

Compare: (2,5) → no swap
Compare: (5,1) → swap
Compare: (5,8) → no swap
Result: [2, 1, 5, 8, █9█]

Continue until sorted...

FINAL: [1, 2, 5, 8, 9] ✓

INVARIANT:
┌─────────────────────────────────┐
│ After pass k:                  │
│ [unsorted] │ [k largest sorted] │
│ Last k elements in place        │
└─────────────────────────────────┘

TIME: O(n²) worst/average
SPACE: O(1) in-place
```

---

### Pattern 1.2: Selection Sort - Find and Place

#### Visual 1: Selection Process

```
ARRAY: [5, 2, 8, 1, 9]

ITERATION 1: Find min in entire array
─────────────────────────────────────

Scan all elements:
[5, 2, 8, 1, 9]
      ↑
Min element: 1 at index 3

Swap with position 0:
[1, 2, 8, 5, 9]
│
└─ Placed (sorted)

ITERATION 2: Find min in [2..end]
─────────────────────────────────

Scan from index 1:
[1, │2, 8, 5, 9│]
    └──────────┘
    Min: 2 at index 1 (already in place)

ITERATION 3: Find min in [5,8,9]
─────────────────────────────────

[1, 2, │8, 5, 9│]
        └─────┘
Min: 5 at index 3

Swap with index 2:
[1, 2, 5, 8, 9]
│  │  │
└──┴──┘
All sorted

INVARIANT:
┌──────────────────────────────────┐
│ [0..i-1]     = sorted elements  │
│ [i..n-1]     = unsorted region  │
│ Each iteration grows sorted zone│
└──────────────────────────────────┘

TIME: O(n²) - always full scans
SPACE: O(1) in-place
```

---

### Pattern 1.3: Insertion Sort - Growing Sorted Prefix

#### Visual 1: Insert into Sorted Prefix

```
ARRAY: [5, 2, 8, 1, 9]

ITERATION 1: Insert element at index 1
────────────────────────────────────────

Before: [│5│  2  8  1  9]
        └─┘sorted prefix
        
Element to insert: 2

Compare 2 with 5: 2 < 5, shift 5 right
[5, 5, 8, 1, 9]
Insert 2 at position 0:
[│2, 5│  8  1  9]
 └────┘sorted prefix

ITERATION 2: Insert element at index 2
────────────────────────────────────────

Before: [│2, 5│  8  1  9]
Element: 8

Compare 8 with 5: 8 > 5, no shift needed
Insert 8 at position 2:
[│2, 5, 8│  1  9]
 └──────┘ sorted prefix

ITERATION 3: Insert element at index 3
────────────────────────────────────────

Before: [│2, 5, 8│  1  9]
Element: 1

Compare 1 with 8: 1 < 8, shift 8
[2, 5, 8, 8, 9]

Compare 1 with 5: 1 < 5, shift 5
[2, 5, 5, 8, 9]

Compare 1 with 2: 1 < 2, shift 2
[2, 2, 5, 8, 9]

Insert 1 at position 0:
[│1, 2, 5, 8│  9]
 └────────┘ sorted prefix

ITERATION 4: Insert element at index 4
────────────────────────────────────────

Before: [│1, 2, 5, 8│  9]
Element: 9

Compare 9 with 8: 9 > 8, no shift
Insert 9 at position 4:
[│1, 2, 5, 8, 9│]
 └────────────┘ sorted

FINAL: [1, 2, 5, 8, 9] ✓

KEY INSIGHT:
┌─────────────────────────────────┐
│ [0..i-1] = sorted             │
│ [i] = element to insert        │
│ [i+1..n-1] = unprocessed       │
│                                 │
│ Cost per insertion: O(i) shifts│
│ Total: O(1+2+...+n) = O(n²)   │
│ But: Nearly sorted → adaptive! │
└─────────────────────────────────┘

TIME: O(n²) worst, O(n) best (sorted)
SPACE: O(1) in-place
STABILITY: ✓ Stable (equal order preserved)
```

---

### Common Failure Modes (Day 1)

#### Failure 1: Comparing Already Sorted Elements

```
❌ WRONG (Bubble Sort):
for i in range(n):
  for j in range(n-1):    ← Rechecks sorted elements!
    if arr[j] > arr[j+1]:
      swap

Result: Unnecessarily compares already-sorted portion

✓ CORRECT:
for i in range(n):
  for j in range(n-1-i):  ← Exclude sorted suffix
    if arr[j] > arr[j+1]:
      swap

Result: Each pass ignores already-placed elements
```

#### Failure 2: Off-by-One in Insertion Sort

```
❌ WRONG:
for i in range(n):
  key = arr[i]
  j = i - 1
  while j >= 0 and arr[j] > key:
    arr[j+1] = arr[j]
    j--
  arr[j+1] = key  ← May write beyond if j=-1

❌ Issue: When j becomes -1, arr[0] = key still works
         But is susceptible to boundary bugs

✓ CORRECT (same logic, clearer):
for i in range(1, n):  ← Start from index 1
  key = arr[i]
  j = i - 1
  while j >= 0 and arr[j] > key:
    arr[j+1] = arr[j]
    j--
  arr[j+1] = key  ← j+1 always in range [0, i]
```

---

### Mini Review Quiz (Day 1)

**Q1:** Why is Insertion Sort adaptive but Bubble Sort is not?

```
A) Bubble Sort is faster on nearly sorted arrays
B) Insertion Sort stops early if array is sorted
C) Bubble Sort doesn't compare neighbors
D) Insertion Sort inherently skips sorted elements
```
**✅ Answer:** B (Best case O(n) for mostly sorted, vs O(n²) for bubble)

**Q2:** Which sort is stable?

```
A) Bubble Sort only
B) All three (bubble, selection, insertion)
C) Bubble and Insertion, but not Selection
D) None are stable
```
**✅ Answer:** C (Selection breaks stability by direct placement)

**Q3:** For nearly sorted array with k inversions (k << n), which is best?

```
A) Bubble Sort O(n + k) passes
B) Selection Sort O(n²) always
C) Insertion Sort O(n + k) shifts
D) All equally bad
```
**✅ Answer:** C (Insertion adapts to inversions, others don't)

---

## ⚙️ DAY 2: MERGE SORT & QUICK SORT

### Pattern Map: Advanced Sorting Family Tree

```
ADVANCED SORTS (Divide & Conquer)
├─ Merge Sort
│  ├─ Divide into halves
│  ├─ Recursively sort both
│  ├─ Merge sorted halves
│  └─ O(n log n) guaranteed, stable
│
└─ Quick Sort
   ├─ Partition around pivot
   ├─ Recursively sort partitions
   ├─ In-place with excellent cache
   └─ O(n log n) average, O(n²) worst
```

---

### Pattern 2.1: Merge Sort Tree Structure

**Interactive Resource:** 🔗 [VisuAlgo Merge Sort](https://visualgo.net/en/sorting)

#### Visual 1: Recursion Tree with Merges

```
ARRAY: [38, 27, 43, 3, 9, 82, 10]

DIVIDE PHASE:
─────────────

Level 0:        [38,27,43,3,9,82,10]
                /               \
               /                 \
Level 1:    [38,27,43,3]      [9,82,10]
            /       \          /      \
Level 2:  [38,27]  [43,3]   [9,82]  [10]
          /    \    /   \    /   \     |
Level 3: [38] [27][43] [3] [9] [82]  (base)


MERGE PHASE (Bottom-Up Reconstruction):
───────────────────────────────────────

Level 3: [38] [27] → [27,38]
         [43] [3]  → [3,43]
         [9]  [82] → [9,82]
                     [10]

Level 2: [27,38]  [3,43]  → [3,27,38,43]
         [9,82]   [10]    → [9,10,82]

Level 1: [3,27,38,43]  [9,10,82]  → [3,9,10,27,38,43,82]

RESULT: [3,9,10,27,38,43,82] ✓

COMPLEXITY ANALYSIS:
┌──────────────────────────────────┐
│ Dividing:                        │
│ Level 0: 1 array of 7 = 7 ops   │
│ Level 1: 2 arrays merge = 7 ops │
│ Level 2: 4 arrays merge = 7 ops │
│ Level 3: 8 single = 0 ops       │
│                                  │
│ Total: 7 × log₂(7) ≈ 7 × 3 = 21 │
│ = O(n log n) guaranteed!        │
│                                  │
│ BENEFIT: Always O(n log n)      │
│ even in worst case!             │
└──────────────────────────────────┘

TIME: O(n log n) guaranteed
SPACE: O(n) for merge space
STABLE: ✓ Yes (preserves equal order)
```

---

### Pattern 2.2: Quick Sort Partitioning

#### Visual 1: Partition Around Pivot

```
PROBLEM: Partition array around pivot (Lomuto scheme)
ARRAY: [3, 7, 8, 5, 2, 1, 9, 5, 4]
PIVOT: Last element = 4

PARTITION PROCESS:
──────────────────

Initial:
┌─┬─┬─┬─┬─┬─┬─┬─┬─┐
│3│7│8│5│2│1│9│5│4│
└─┴─┴─┴─┴─┴─┴─┴─┴─┘
                  ↑
              pivot = 4

Two pointers: i (marks ≤ boundary), j (scans)

i=-1, j=0: arr[0]=3 < 4 pivot
  i++ → i=0
  swap arr[0] and arr[0]
  [3│7│8│5│2│1│9│5│4]
     ↑(i)

i=0, j=1: arr[1]=7 > 4 pivot
  No action, j++

i=0, j=2: arr[2]=8 > 4 pivot
  No action, j++

i=0, j=3: arr[3]=5 > 4 pivot
  No action, j++

i=0, j=4: arr[4]=2 < 4 pivot
  i++ → i=1
  swap arr[1] and arr[4]
  [3│2│8│5│7│1│9│5│4]
     ↑(i) ↑(j)

i=1, j=5: arr[5]=1 < 4 pivot
  i++ → i=2
  swap arr[2] and arr[5]
  [3│2│1│5│7│8│9│5│4]
       ↑(i)     ↑(j)

i=2, j=6: arr[6]=9 > 4 pivot
  No action, j++

i=2, j=7: arr[7]=5 > 4 pivot
  No action, j++

i=2, j=8: Reached pivot, stop

FINAL SWAP (pivot to position i+1):
swap arr[i+1] and arr[n-1]:
[3│2│1│4│7│8│9│5│8]
       ↑(pivot placed)

RESULT ZONES:
┌──────────────────────────────────┐
│ [≤4]    [4]    [>4]             │
│ 3,2,1   4      7,8,9,5,5        │
│                                  │
│ Partition index = 3             │
│ Left side: recurse [3,2,1]      │
│ Right side: recurse [7,8,9,5,5] │
└──────────────────────────────────┘

TIME: O(n) for partition
SPACE: O(1) partition is in-place
STABILITY: ✗ Not stable
```

---

### Common Failure Modes (Day 2)

#### Failure 1: Forgetting Merge Space

```
❌ WRONG (Merge Sort claiming O(1) space):
void mergeSort(arr):
  if n <= 1: return
  mid = n/2
  mergeSort(arr[0..mid-1])
  mergeSort(arr[mid..n-1])
  merge(arr, 0, mid, n-1)  ← Where does merge temp go?

Result: No space for temporary array, corrupts data

✓ CORRECT:
void mergeSort(arr, l, r, temp):
  if l < r:
    mid = (l + r) / 2
    mergeSort(arr, l, mid, temp)
    mergeSort(arr, mid+1, r, temp)
    merge(arr, l, mid, r, temp)  ← Use temp array

Merge fills temp[], then copies back to arr[]
Result: O(n) space used properly
```

#### Failure 2: Bad Pivot Choice in Quick Sort

```
❌ WRONG:
Always choose first/last element as pivot

For already sorted array [1,2,3,4,5]:
Pivot = 5
Partition: [1,2,3,4] | [5]
Recurse left: Pivot = 4
Partition: [1,2,3] | [4]
...
Result: O(n²) degenerate behavior!

✓ CORRECT (Randomized Pivot):
Random pivot selection
Or: Median-of-three pivot
Or: Randomized quick select

Expected: O(n log n) even for sorted arrays
Probability of bad pivot decreases exponentially
```

---

## 📦 DAY 3: HEAPS, HEAPIFY & HEAP SORT

### Pattern Map: Heap Operations Family

```
HEAP STRUCTURES
├─ Binary Heap Array Representation
│  ├─ Min-heap (parent ≤ children)
│  ├─ Max-heap (parent ≥ children)
│  └─ Complete binary tree in array
│
├─ Core Operations
│  ├─ Insert (bubble up)
│  ├─ Extract-min/max (bubble down)
│  └─ Build-heap (heapify all)
│
└─ Applications
   ├─ Heap sort
   ├─ Priority queues
   └─ Top-k problems
```

---

### Pattern 3.1: Array Representation & Parent-Child

**Interactive Resource:** 🔗 [Heap Visualizer](https://www.cs.usfca.edu/~galles/visualization/Heap.html)

#### Visual 1: Array Index Mapping

```
MIN-HEAP STRUCTURE:
        1
       / \
      3   2
     / \ /
    7  4 5

ARRAY REPRESENTATION (0-indexed):
Index:  0  1  2  3  4  5
Value: [1, 3, 2, 7, 4, 5]

PARENT-CHILD MAPPING:
For node at index i:
  parent_index = (i - 1) / 2
  left_child_index = 2*i + 1
  right_child_index = 2*i + 2

Example: Node at index 1 (value=3)
  parent = (1-1)/2 = 0 → arr[0]=1 ✓ (parent < child)
  left = 2*1+1 = 3 → arr[3]=7 ✓ (parent < child)
  right = 2*1+2 = 4 → arr[4]=4 ✓ (parent < child)

HEAP PROPERTY (Min-Heap):
┌──────────────────────────────┐
│ parent ≤ left child          │
│ parent ≤ right child         │
│                               │
│ Recursively true for all     │
│ subtrees                     │
└──────────────────────────────┘

BENEFITS OF ARRAY REPRESENTATION:
✓ Cache-friendly (sequential memory)
✓ No pointer overhead
✓ O(1) parent/child lookup
✓ No fragmentation
```

---

### Pattern 3.2: Insert Operation (Bubble Up)

#### Visual 1: Maintain Heap Property During Insert

```
MIN-HEAP: [1, 3, 2, 7, 4, 5]
INSERT: 0 (new minimum)

STEP 1: Append to end
┌───┐
│ 0 │ ← New element
└───┘
[1, 3, 2, 7, 4, 5, 0]
 0  1  2  3  4  5  6  (indices)

STEP 2: Bubble up (swap with parent if smaller)

Node at index 6 (value=0):
Parent at (6-1)/2 = 2 → arr[2]=2
0 < 2? YES, swap:
[1, 3, 0, 7, 4, 5, 2]
        ↑  ↑
     swapped

Node now at index 2 (value=0):
Parent at (2-1)/2 = 0 → arr[0]=1
0 < 1? YES, swap:
[0, 3, 1, 7, 4, 5, 2]
 ↑  ↑
swapped

Node now at index 0 (value=0):
No parent, STOP

RESULT: [0, 3, 1, 7, 4, 5, 2] ✓ Heap maintained

TREE VIEW:
        0            (was 1)
       / \
      3   1          (was 2)
     / \ /
    7  4 5


TIME: O(log n) - height of heap
SPACE: O(1) just swaps
```

---

### Pattern 3.3: Extract-Min (Bubble Down)

#### Visual 1: Remove Root and Restore

```
MIN-HEAP: [1, 3, 2, 7, 4, 5]
EXTRACT MIN (remove root = 1):

STEP 1: Save root
min_value = arr[0] = 1  ← To return

STEP 2: Move last to root
[5, 3, 2, 7, 4, 5]  ← Move arr[5] to arr[0]
 ↑
 5 is now root (may violate heap property)

STEP 3: Bubble down (sink to correct position)

Node at index 0 (value=5):
Children:
  Left: index 1 → arr[1] = 3
  Right: index 2 → arr[2] = 2

Find smaller child: min(3, 2) = 2 at index 2
5 > 2? YES, swap with smaller child:
[2, 3, 5, 7, 4]
 ↑     ↑
swapped

Node now at index 2 (value=5):
Children:
  Left: index 5 → out of bounds
  Right: index 6 → out of bounds

No children, STOP

RESULT: [2, 3, 5, 7, 4] ✓ Heap maintained
Returned: 1

TREE VIEW:
        2            (was 5)
       / \
      3   5          (was 2)
     / \
    7   4


TIME: O(log n) - height of heap
SPACE: O(1) just swaps
```

---

### Pattern 3.4: Heap Sort

#### Visual 1: Build-Heap then Extract

```
ARRAY: [38, 27, 43, 3, 9, 82, 10]

PHASE 1: BUILD-HEAP (bottom-up heapify)
──────────────────────────────────────

Start from last non-leaf node: index = (7-1)/2 = 3

Heapify index 3 (value=3):
  Children: 9, 82
  Min child: 3
  3 < min? No

Heapify index 2 (value=43):
  Children: 82, 10
  Min child: 10
  43 > 10? Yes, swap
  [38, 27, 10, 3, 9, 82, 43]

Heapify index 1 (value=27):
  Children: 3, 10
  Min child: 3
  27 > 3? Yes, swap
  [38, 3, 10, 27, 9, 82, 43]

Heapify index 0 (value=38):
  Children: 3, 10
  Min child: 3
  38 > 3? Yes, swap
  [3, 38, 10, 27, 9, 82, 43]
  Continue bubbling down...

RESULT HEAP: [3, 9, 10, 27, 38, 82, 43] (min-heap built)

PHASE 2: EXTRACT ONE-BY-ONE
──────────────────────────

Extract 1: min=3, move last to root
  [43, 9, 10, 27, 38, 82]
  Bubble down to [9, 27, 10, 43, 38, 82]

Extract 2: min=9, continue...
  Result: [10, 27, 38, 43, 82]

... repeat until empty

SORTED: [3, 9, 10, 27, 38, 43, 82] ✓

COMPLEXITY:
┌──────────────────────────┐
│ Build-heap: O(n)        │
│ Extract × n: O(n log n) │
│ Total: O(n log n)       │
│                          │
│ Space: O(1) in-place!   │
│ Better than Merge Sort! │
└──────────────────────────┘

TIME: O(n log n)
SPACE: O(1) in-place
STABILITY: ✗ Not stable
```

---

## #️⃣ DAY 4: HASH TABLES (SEPARATE CHAINING)

### Pattern Map: Hash Table Design

```
HASH TABLES
├─ Hash Function
│  ├─ Map keys to bucket indices
│  ├─ Uniformity: avoid collisions
│  └─ Fast to compute
│
├─ Separate Chaining
│  ├─ Chain collisions with lists
│  ├─ Load factor control
│  └─ Resizing strategy
│
└─ Collision Handling
   ├─ Good hash function
   ├─ Adequate bucket count
   └─ Monitor load factor
```

---

### Pattern 4.1: Hash Function & Collisions

**Interactive Resource:** 🔗 [GeeksforGeeks Hashing](https://www.geeksforgeeks.org/hashing-data-structure/)

#### Visual 1: Bucket Distribution

```
HASH FUNCTION: h(key) = key % 10
CAPACITY: 10 buckets

INSERTING: [21, 31, 14, 25, 33, 44, 12]

Bucket Index Calculation:
21 → 21 % 10 = 1
31 → 31 % 10 = 1  (COLLISION with 21!)
14 → 14 % 10 = 4
25 → 25 % 10 = 5
33 → 33 % 10 = 3
44 → 44 % 10 = 4  (COLLISION with 14!)
12 → 12 % 10 = 2

HASH TABLE (Separate Chaining):
Bucket 0: []
Bucket 1: [21 → 31]        ← Chain of collided items
Bucket 2: [12]
Bucket 3: [33]
Bucket 4: [14 → 44]        ← Another collision chain
Bucket 5: [25]
Bucket 6: []
Bucket 7: []
Bucket 8: []
Bucket 9: []

LOAD FACTOR:
λ = n / buckets = 7 / 10 = 0.7

If λ > threshold (e.g., 0.75):
  Double buckets to 20
  Rehash all entries

SEARCH PROCESS:
Looking for 31:
  h(31) = 1
  Scan bucket 1: [21 → 31]
  Found 31 ✓

Looking for 44:
  h(44) = 4
  Scan bucket 4: [14 → 44]
  Found 44 ✓

COMPLEXITY:
┌──────────────────────────────────┐
│ Good hash + load factor < 1:    │
│   Search: O(1) average          │
│   Insert: O(1) average          │
│   Delete: O(1) average          │
│                                  │
│ Bad hash + collisions:          │
│   Degenerate to O(n)            │
│   All items in same chain!      │
└──────────────────────────────────┘

STRATEGY:
  Prime number of buckets
  Good hash function
  Monitor load factor
  Rehash when needed
```

---

### Pattern 4.2: Resizing Strategy

#### Visual 1: Rehashing Process

```
SCENARIO: Load factor exceeds 0.75

BEFORE RESIZE:
Buckets: 10
Items: 8 (λ = 0.8 > 0.75 threshold)

Bucket 0: []
Bucket 1: [21 → 31]
Bucket 2: [12]
Bucket 3: [33]
Bucket 4: [14 → 44]
Bucket 5: [25]
Bucket 6: []
...

RESIZE ACTION:
─────────────

Double buckets: 10 → 20

NEW HASH FUNCTION: h(key) = key % 20

REHASH ALL ITEMS:
21 → 21 % 20 = 1
31 → 31 % 20 = 11
12 → 12 % 20 = 12
33 → 33 % 20 = 13
14 → 14 % 20 = 14
44 → 44 % 20 = 4
25 → 25 % 20 = 5

AFTER RESIZE:
Buckets: 20
Items: 8 (λ = 0.4 < threshold)

Bucket 0: []
Bucket 1: [21]         ← Separated!
Bucket 2: []
Bucket 3: []
Bucket 4: [44]
Bucket 5: [25]
...
Bucket 11: [31]        ← Found new bucket
Bucket 12: [12]
Bucket 13: [33]
Bucket 14: [14]
...

COLLISION REDUCTION:
Before: Bucket 1 had 2 items (chain)
After: Items separated into buckets 1 and 11

AMORTIZED COST:
┌────────────────────────────────────┐
│ Resize happens at n = capacity × α│
│ where α = load factor threshold    │
│                                     │
│ Resize cost: O(n) to rehash all    │
│ But happens rarely (exponentially) │
│                                     │
│ Amortized insert: O(1)             │
│ (Same as dynamic arrays!)          │
└────────────────────────────────────┘
```

---

## 🔐 DAY 5: ROLLING HASH & KARP-RABIN

### Pattern Map: String Hashing

```
STRING MATCHING PATTERNS
├─ Naive: O(nm) compare
│
├─ Rolling Hash (Karp-Rabin)
│  ├─ Compute hash once
│  ├─ Update in O(1) per position
│  ├─ Compare hashes instead of strings
│  └─ O(n+m) expected time
│
└─ Applications
   ├─ Substring search
   ├─ Plagiarism detection
   └─ DNA sequence matching
```

---

### Pattern 5.1: Rolling Hash Window

**Interactive Resource:** 🔗 [GeeksforGeeks Karp-Rabin](https://www.geeksforgeeks.org/rabin-karp-algorithm-for-pattern-searching/)

#### Visual 1: Hash Update Formula

```
STRING: "ABCDDE"
PATTERN: "CD"

HASH FUNCTION:
  hash(s) = (s[0]×p^(m-1) + s[1]×p^(m-2) + ... + s[m-1]) % q
  where p = 31, q = 101 (prime)

WINDOW SIZE: m = 2

WINDOW 0: "AB"
────────
hash("AB") = (A×31 + B×1) % 101
           = (0×31 + 1×1) % 101
           = 1

WINDOW 1: "BC"
────────
Naive: recalculate from scratch = O(m)

ROLLING UPDATE (O(1)):
  old_hash = 1
  Remove first: A×31 = 0×31 = 0
    old_hash - 0 = 1
  Divide by base: (1) / 31 = 0 (integer division issues!)
  
  Better formula:
  new_hash = (old_hash - first_char×p^(m-1)) × p + last_char
  new_hash = (1 - 0×1) × 31 + C
           = 1 × 31 + 2
           = 33

WINDOW 2: "CD"
────────
new_hash = (33 - 1×31) × 31 + D
         = (33 - 31) × 31 + 3
         = 2 × 31 + 3
         = 65

Check: hash("CD") = (C×31 + D) % 101
                  = (2×31 + 3) % 101
                  = 65 ✓ Matches!

HASH PATTERN:
pattern = "CD"
hash(pattern) = 65

SEARCH PROCESS:
Window 0: hash("AB") = 1 ≠ 65 ✗
Window 1: hash("BC") = 33 ≠ 65 ✗
Window 2: hash("CD") = 65 = 65 ✓ Match found at position 2!

ALGORITHM:
┌──────────────────────────────────────┐
│ 1. Compute pattern hash: O(m)      │
│ 2. Compute first window hash: O(m) │
│ 3. Roll through string:            │
│    - Update hash: O(1)             │
│    - Compare hashes: O(1)          │
│    - If match, verify string: O(m) │
│ 4. Total: O(n + m) expected        │
│    Worst: O(nm) if many collisions │
└──────────────────────────────────────┘

TIME: O(n + m) expected
SPACE: O(1) just hash values
```

---

### Common Failure Modes (Day 5)

#### Failure 1: Overflow in Hash Calculation

```
❌ WRONG:
hash = 0
for char in string:
  hash = hash × p + ord(char)

For large strings, hash overflows integer!

✓ CORRECT:
hash = 0
for char in string:
  hash = (hash × p + ord(char)) % q

Keep intermediate results modulo q (prime)
Prevents overflow while maintaining equivalence
```

#### Failure 2: Not Handling Modular Arithmetic

```
❌ WRONG (Rolling update):
new_hash = (old_hash - first_char × p^(m-1)) × p + last_char

If (old_hash - first_char×p^(m-1)) is negative:
  Result may be negative!

✓ CORRECT:
new_hash = ((old_hash - first_char × p^(m-1)) × p + last_char) % q
         = ((old_hash - first_char × p^(m-1)) % q × p + last_char) % q

If negative:  new_hash = (new_hash + q) % q

Ensures result always in range [0, q-1]
```

---

## 🎯 WEEK 03 VISUAL SUMMARY TABLE

```
┌────────────────────────────────────────────────────────────┐
│ DAY │ PATTERN          │ Complexity    │ Best/Worst Use  │
├────────────────────────────────────────────────────────────┤
│ 1   │ Elementary Sorts │ O(n²) / O(1)  │ Small n,        │
│     │ (Bubble, Select, │ stable vary   │ nearly sorted   │
│     │ Insertion)       │               │ (Insertion)     │
│     │                  │               │                 │
│ 2   │ Merge Sort       │ O(n log n) /  │ Stability       │
│     │ Quick Sort       │ O(n log n) av │ needed / cache   │
│     │                  │ O(n²) worst   │ efficiency      │
│     │                  │               │                 │
│ 3   │ Heaps & Heap     │ O(log n) each │ Priority        │
│     │ Sort             │ O(n log n)    │ queues, top-k   │
│     │                  │ sort, O(1) sp │                 │
│     │                  │               │                 │
│ 4   │ Hash Tables      │ O(1) avg /    │ O(1) lookup,    │
│     │ (Chaining)       │ O(n) worst    │ dynamic sets    │
│     │                  │               │                 │
│ 5   │ Rolling Hash     │ O(n+m) exp /  │ Substring       │
│     │ (Karp-Rabin)     │ O(nm) worst   │ search, pattern │
│     │                  │               │ matching        │
└────────────────────────────────────────────────────────────┘
```

---

## 📋 COMMON PATTERNS QUICK REFERENCE

```
Pattern              │ When to Use              │ Time/Space
─────────────────────┼──────────────────────────┼──────────
Bubble Sort          │ Educational, tiny n     │ O(n²) / O(1)
Insertion Sort       │ Nearly sorted, small n  │ O(n²) O(n) best/O(1)
Merge Sort           │ Need stable sort        │ O(n log n) / O(n)
Quick Sort           │ Cache efficiency needed │ O(n log n) avg / O(1)
Heap Sort            │ In-place, no extra mem  │ O(n log n) / O(1)
Hash Table (chain)   │ Fast lookup/insert      │ O(1) avg / O(n) chain
Priority Queue (heap)│ Extract min/max quickly │ O(log n) / O(1)
Karp-Rabin           │ Substring patterns      │ O(n+m) / O(1)
```

---

## 🔗 RECOMMENDED LEARNING RESOURCES

### Interactive Visualizations
1. **VisuAlgo Sorting** (https://visualgo.net/en/sorting) — Compare all algorithms side-by-side
2. **Toptal Sorting Visualizer** (https://www.toptal.com/developers/sorting-visualizer) — Hear the sorts!
3. **Heap Visualizer** (https://www.cs.usfca.edu/~galles/visualization/Heap.html) — Step through heap ops
4. **GeeksforGeeks Sorting** (https://www.geeksforgeeks.org/sorting-algorithms/) — Complete reference
5. **GeeksforGeeks Heaps** (https://www.geeksforgeeks.org/heap-data-structure/) — Heap deep dive
6. **GeeksforGeeks Hashing** (https://www.geeksforgeeks.org/hashing-data-structure/) — Hash concepts

### Video Tutorials
- "Sorting Algorithms Explained" — Visual walkthrough of all sorts
- "Heap and Priority Queue" — Animations and use cases
- "Hash Tables Explained" — Collision handling strategies

---

## 📝 HOW TO USE THIS PLAYBOOK

### Quick Revision (30 mins)
1. Scan pattern maps (5 mins)
2. Read one day's main visuals (5 mins per day)
3. Answer mini quiz (3 mins per day)
4. Review failure modes (2 mins per day)

### Deep Learning (2-3 hours)
1. Read playbook + extended subtopics guide
2. Visit web resource links for interactive animations
3. Implement code from main instructional files
4. Solve practice problems using visuals as reference

### Interview Prep
1. Open playbook for quick pattern reminders
2. Use resource links for visual refresh
3. Mentally trace algorithm using playbook diagrams
4. Code from memory with confidence

---

**Version:** 1.0 Hybrid Approach | **Generated:** Friday, January 09, 2026, 1:10 AM IST  
**System:** v12 Visual Concepts Framework + Web Resources  
**Status:** ✅ PRODUCTION-READY WITH EMBEDDED REFERENCES

**Use web resource links for interactive visualizations while studying!**

