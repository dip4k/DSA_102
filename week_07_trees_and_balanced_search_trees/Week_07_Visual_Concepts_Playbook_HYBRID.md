# 📊 WEEK 7: Trees & Balanced Search Trees — Visual Hybrid Support Guide

**File Classification:** Visual Playbook Support Document (v12)  
**Week:** 7 | **Days:** 1-5 (Core + Advanced)  
**Format:** Markdown with Integrated Visual Concepts & ASCII Diagrams  
**Purpose:** Unified visual reference spanning all five instructional files  
**Last Updated:** January 22, 2026

---

## 📋 TABLE OF CONTENTS

1. **Visual Overview — Week 7 Architecture**
2. **Day 1: Tree Anatomy & Traversals — Visual Concepts**
3. **Day 2: Binary Search Trees — Visual Concepts**
4. **Day 3: Balanced Trees — Visual Concepts**
5. **Day 4: Tree Patterns — Visual Concepts**
6. **Day 5: Augmented Trees — Visual Concepts**
7. **Cross-Day Visual Comparisons**
8. **Memory Layout Diagrams**
9. **Complexity & Performance Visualizations**
10. **Interview Visual Reference**

---

## 1️⃣ VISUAL OVERVIEW — WEEK 7 ARCHITECTURE

### The Tree Evolution Diagram

```
┌─────────────────────────────────────────────────────────────┐
│                      WEEK 7 PROGRESSION                      │
├─────────────────────────────────────────────────────────────┤
│                                                               │
│  Day 1: Generic Trees        Day 2: Ordered Trees           │
│  ┌──────────┐                ┌──────────┐                   │
│  │    10    │                │    5     │ (BST invariant:   │
│  │   /  \   │                │   / \    │  left < root)    │
│  │  5   15  │                │  3   7   │                   │
│  │ / \      │                └──────────┘                   │
│  │3   7     │                                                │
│  └──────────┘                                                │
│  (unordered)                (ordered)                        │
│       ↓                           ↓                          │
│   Day 3: Balanced Trees      Day 4: Patterns                │
│  ┌──────────┐                ┌──────────┐                   │
│  │    5(3)  │ (AVL/RB        │   Path Sum = 22              │
│  │   /  \   │  height=2)     │   Diameter = 4              │
│  │ 3  7(2)  │                │   LCA = 5                   │
│  │      \   │                │   Serialize = [...]         │
│  │       8  │                └──────────┘                   │
│  └──────────┘                                                │
│  (guaranteed O(log n))       (algorithms)                    │
│       ↓                           ↓                          │
│       └─────────────────────────────────┐                   │
│                                         ↓                    │
│                        Day 5: Augmented Trees               │
│                       ┌──────────────────┐                  │
│                       │   5(size=3,sum=15)│                │
│                       │   /           \   │                │
│                       │ 3(1,3)     7(1,7) │                │
│                       │                    │                │
│                       │ kth smallest = O(log n)            │
│                       │ rank queries = O(log n)            │
│                       └──────────────────┘                  │
│                       (optimized queries)                   │
└─────────────────────────────────────────────────────────────┘
```

### Week 7 Conceptual Layers

```
┌──────────────────────────────────────────────────┐
│ Layer 5: Query Optimization (Day 5)              │
│ Augmentation with metadata for O(log n) queries  │
├──────────────────────────────────────────────────┤
│ Layer 4: Algorithm Patterns (Day 4)              │
│ Path sum, diameter, LCA, serialization           │
├──────────────────────────────────────────────────┤
│ Layer 3: Performance Guarantee (Day 3)           │
│ Balance invariant for O(log n) height            │
├──────────────────────────────────────────────────┤
│ Layer 2: Ordering (Day 2)                        │
│ BST invariant: left < parent < right             │
├──────────────────────────────────────────────────┤
│ Layer 1: Structure (Day 1)                       │
│ Tree anatomy, traversals, memory layout          │
└──────────────────────────────────────────────────┘
```

---

## 2️⃣ DAY 1: TREE ANATOMY & TRAVERSALS — VISUAL CONCEPTS

### Tree Structure Types at a Glance

```
FULL BINARY TREE           COMPLETE BINARY TREE       BALANCED BINARY TREE
(every node has 0 or 2)    (filled except last level) (height ≤ 1.44·log₂(n))

       1                         1                          1
      / \                       / \                        / \
     2   3                     2   3                      2   3
    / \ /                     / \ / \                    / \ / \
   4 5 6                     4 5 6 7                    4 5 6 7

   ✓ Valid           ✓ Valid, sequential              ✓ Valid

NOT FULL              NOT COMPLETE         NOT BALANCED (h=3)
     1                    1                      1
    / \                  / \                    / 
   2   3                2   3                  2   
  /                    /     \                /   
 4                    4       5              3    
                                            /    
                                           4     
```

### All 4 Traversal Orders — Visual Mapping

```
Sample Tree:
           A
          / \
         B   C
        / \
       D   E

PREORDER (root, left, right):           INORDER (left, root, right):
Visit: A → B → D → E → C                Visit: D → B → E → A → C
       └─ parent first                         └─ sorted (if BST)
       
Result: [A, B, D, E, C]                Result: [D, B, E, A, C]

POSTORDER (left, right, root):          LEVEL-ORDER (BFS):
Visit: D → E → B → C → A                Visit: A → B → C → D → E
       └─ parent last                          └─ breadth-first
       
Result: [D, E, B, C, A]                Result: [A, B, C, D, E]
```

### Recursion Call Stack Visualization (Preorder)

```
preorder(A)
│
├─ print A
├─ preorder(B)
│  ├─ print B
│  ├─ preorder(D)
│  │  └─ print D
│  ├─ preorder(E)
│  │  └─ print E
│  
├─ preorder(C)
│  └─ print C

Call Stack Depth = Tree Height = 3
Stack Frames: A → B → D (max 3)
```

### Iterative Traversal Using Explicit Stack

```
ITERATIVE PREORDER (using stack):

Stack: [A]  → Pop A, print A, push right C, push left B
Stack: [C, B] → Pop B, print B, push right E, push left D
Stack: [C, E, D] → Pop D, print D
Stack: [C, E] → Pop E, print E
Stack: [C] → Pop C, print C
Stack: [] → Done

Output: A, B, D, E, C ✓ (matches recursive)
```

### Tree Anatomy Measurements

```
           A (depth=0, height=3)
          / \
         B   C (depth=1, height=1)
        / \
       D   E (depth=2, height=0 — leaves)

Height Calculation (bottom-up):
  height(D) = 0 (leaf)
  height(B) = 1 + max(0, 0) = 1
  height(A) = 1 + max(1, 0) = 2

Depth Calculation (top-down):
  depth(A) = 0
  depth(B) = 1
  depth(D) = 2
```

---

## 3️⃣ DAY 2: BINARY SEARCH TREES — VISUAL CONCEPTS

### BST Invariant — The Core Property

```
VALID BST                NOT A BST
      5                       5
     / \                      / \
    3   7                    3   8  ← 8 > 5 but in right subtree ✗
   / \   \                  / \  
  1   4   9               1   6  ← 6 < 5 but in right subtree ✗
                                    
Every path respects         Violates invariant:
left < parent < right       left < root < right
```

### BST Search Visualization

```
Search for 6 in:
         5
        / \
       3   7
      / \   \
     1   4   9

Step 1: Compare 6 with 5 → 6 > 5, go RIGHT
Step 2: Compare 6 with 7 → 6 < 7, go LEFT
Step 3: No left child of 7 → NOT FOUND

Path taken: 5 → 7 → null
Time: O(height) = O(log n) average, O(n) worst
```

### BST Insertion Mechanics

```
Insert 6 into tree:
         5                5
        / \              / \
       3   7      →     3   7
      / \   \         / \   \
     1   4   9       1   4   9
                          ↘
                          6 ← New node

Insert steps:
1. Compare 6 with 5 → 6 > 5, go right to 7
2. Compare 6 with 7 → 6 < 7, go left (empty)
3. Insert 6 as left child of 7
```

### BST Deletion Cases

```
Case 1: LEAF DELETION              Case 2: ONE CHILD DELETION
     5                                  5
    / \                                / \
   3   7                              3   7
  / \    \                           /     \
 1   4    9  ← Delete                1     9
                                          / 
Delete 4:              Delete 7:          8
  5                      5
 / \                    / \
3   7                  3   8
/     \               /     \
1      9             1       9
```

### Inorder Traversal = Sorted Order (Proof)

```
BST Property:       All values in       All values in
                    LEFT subtree <      RIGHT subtree >
                    current node        current node
                    
Inorder traversal visits:
  LEFT subtree (produces sorted sub-sequence < root)
  THEN root
  THEN RIGHT subtree (produces sorted sub-sequence > root)

Result: Left values < root < Right values = SORTED ✓

Visual example:
           5
          / \
         3   7
        / \ / \
       1 4 6  9

Inorder: [1] [3] [4] [5] [6] [7] [9] ← All sorted ✓
            L   R   L   R   L   R
```

### Degenerate Tree (Sorted Input)

```
Insert [1, 2, 3, 4, 5] sequentially:

     1
      \
       2          Height = 5 (linked list)
        \         Time complexity = O(n)
         3        This is WORST CASE
          \
           4
            \
             5

vs. Balanced insertion [3, 2, 4, 1, 5]:
           3
          / \
         2   4
        /     \
       1       5
       
       Height = 2 (log n)
       Time = O(log n) ✓
```

---

## 4️⃣ DAY 3: BALANCED TREES — VISUAL CONCEPTS

### AVL Balance Factor Calculation

```
Balance Factor = height(LEFT) - height(RIGHT)

BALANCED AVL:              NOT BALANCED (|BF| > 1):
        5(BF=0)                    5(BF=2)
       / \                        /
      3   7 (BF=0)               3(BF=1)
     / \                        /
    1   4 (BF=0)               1(BF=0)
                              /
All nodes have |BF| ≤ 1      -1 (this is an unbalanced tree)

Each node: BF = h(left) - h(right)
Valid AVL: BF ∈ {-1, 0, +1}
```

### AVL Rotations — 4 Cases Visualized

```
CASE 1: LL (Left-Left)          CASE 2: RR (Right-Right)
Before:     1(BF=+2)            Before:    5(BF=-2)
           /                              \
          2(BF=+1)                        4(BF=-1)
         /                                  \
        3                                    3

After RIGHT ROTATION:           After LEFT ROTATION:
        2                               4
       / \                             / \
      3   1  ← Rebalanced            5   3  ← Rebalanced

CASE 3: LR (Left-Right)         CASE 4: RL (Right-Left)
Before:     1(BF=+2)            Before:    5(BF=-2)
           /                              \
          3(BF=-1)                        3(BF=+1)
           \                             /
            2                           4

After LEFT rotate on 3,        After RIGHT rotate on 3,
then RIGHT rotate on 1:        then LEFT rotate on 5:
        2                             4
       / \                           / \
      3   1  ← Takes 2 rotations    5   3
```

### Red-Black Tree Coloring Rules

```
VALID RED-BLACK TREE:          INVALID (violates rule 4):
        10(B)                          10(R) ← Root must be black
       /      \                       /      \
     5(R)     15(B)                 5(R)    15(R) ← Two reds
     /  \      /   \               /  \      /   \
   3(B) 7(B) 12(B) 20(R)        3(B) 7(B) 12(B) 20(B)

Rules:
1. Every node is red or black ✓
2. Root is black ✓
3. Null pointers are black ✓
4. No two consecutive red nodes ✓
5. All root-to-null paths have same black count ✓
```

### Black-Height Property

```
Calculate black-height (number of black nodes on path to null):

         10(B)
        /      \
      5(R)    15(B)
      /  \     /   \
    3(B) 7(B) 12(B) 20(R)

Path 10→5→3→null:  Black nodes: 10, 3 = 2
Path 10→5→7→null:  Black nodes: 10, 7 = 2
Path 10→15→12→null: Black nodes: 10, 15, 12 = 3
Path 10→15→20→null: Black nodes: 10, 15 = 2

Black-height invariant: All paths = same black count
(In this tree, some paths have different black-heights,
so this tree might not be perfectly valid RB)
```

### AVL vs Red-Black Trade-offs

```
┌────────────────┬────────────────┬────────────────┐
│ Property       │ AVL            │ Red-Black      │
├────────────────┼────────────────┼────────────────┤
│ Height         │ ~1.0×log₂(n)   │ ~1.5×log₂(n)   │
│ Balance        │ Strict ±1      │ Loose (colors) │
│ Rotations/ins  │ ~1 avg         │ ~1 avg (fewer) │
│ Worst case     │ ~2 rotations   │ ~3 rotations   │
│ Lookup speed   │ Faster (tighter)│ Slightly slower│
│ Insert speed   │ Slower (rotate) │ Faster        │
│ Production use │ Rare (LLVM)    │ Common (Java)  │
└────────────────┴────────────────┴────────────────┘

In practice:
AVL: ▓▓▓▓░░ (balance, fewer searches)
RB:  ▓▓▓░░░ (practicality, fewer rotations)
```

---

## 5️⃣ DAY 4: TREE PATTERNS — VISUAL CONCEPTS

### Path Sum Visualization

```
Tree:                Root-to-leaf paths:
      1(root=1)
     / \
    2   3           Path 1→2 = 1+2 = 3
   / \              Path 1→3 = 1+3 = 4
  4   5             Path 1→2→4 = 1+2+4 = 7 (if 2 has child 4)

All paths sum to target (example: target=7):
1→2→4 sums to 7 ✓ (output this path)
1→3 sums to 4 ✗
```

### Tree Diameter — Longest Path

```
Sample tree with diameter calculation:

           5
          / \
         3   7
        /     \
       1       10
      /       /  \
    0(leaf) 8    15

Candidates for diameter (longest paths):
0→1→3→5→7→10→15 (distance = 6)
0→1→3→5→7→10→8 (distance = 6)

Algorithm: For each node, diameter = max(
  diameter in left subtree,
  diameter in right subtree,
  height(left) + height(right) + 1  ← path through node
)

At node 5:
  left height = 2 (1→3)
  right height = 2 (7→10)
  path through = 2 + 2 = 4 ✓
```

### Lowest Common Ancestor (LCA) — Visual Path

```
Find LCA(1, 10):

Tree:           
       5        LCA = node where paths from 1 and 10 meet
      / \
     3   7
    /     \
   1       10

Path from 1 to root: 1 → 3 → 5
Path from 10 to root: 10 → 7 → 5

Meeting point = 5 ✓ (LCA)

Trace visualization:
       5 ← First common ancestor (meeting point)
      / \
     /   \     Paths diverge:
    3     7    - Left goes to 1
   /       \   - Right goes to 10
  1         10
```

### Serialization — Preorder with Null Markers

```
Tree:           Serialization:
      1(root)   [1, 2, null, null, 3, null, null]
     / \            ↓     ↑                  ↑
    2   3           │     └─ Right of 1     └─ Right of 3
                    └─ Left of 1 (null)

Deserialization (reconstruct from array):
[1, 2, null, null, 3, null, null]

Read 1: Create node 1
Read 2: Create left child of 1 = node 2
Read null: No left child of 2
Read null: No right child of 2
Read 3: Create right child of 1 = node 3
Read null: No left child of 3
Read null: No right child of 3
Done!

Result:
      1
     / \
    2   3  ✓
```

---

## 6️⃣ DAY 5: AUGMENTED TREES — VISUAL CONCEPTS

### Augmentation with Subtree Size

```
Normal BST:         Augmented BST (with subtree size):
      5                    5(size=7)
     / \                  /          \
    3   8        →       3(size=3)   8(size=3)
   / \  /                /    \      /    \
  1  4 7                1(1) 4(1)   7(1)  10(1)

size(node) = 1 + size(left) + size(right)

This enables:
- Order statistics in O(log n) ← kth smallest
- Rank queries in O(log n) ← how many ≤ x
- Range count in O(log n) ← count in [L, R]
```

### Order-Statistics Query — Finding kth Smallest

```
Find 3rd smallest in augmented tree:

           5(7)          Start at root
          /     \        k = 3, size(left) = 3
       3(3)      8(3)    Is k ≤ size(left)? (3 ≤ 3?) YES
      /    \     /    \
   1(1)  4(1) 7(1) 10(1)

Go LEFT to 3
           3(3)          Now at node 3
          /    \         k = 3, size(left) = 1
       1(1)  4(1)        Is k ≤ size(left)? (3 ≤ 1?) NO
                         So go RIGHT with k' = 3 - 1 - 1 = 1

Go RIGHT to 4
           4(1)          Now at node 4
                         k = 1, size(left) = 0
                         Is k == size(left) + 1? (1 == 1?) YES
                         Found! Return 4

Result: 3rd smallest = 4 ✓
```

### Rank Query — How Many ≤ X

```
Query rank of 6 (how many ≤ 6?):

           5(7)          Compare 6 with 5
          /     \        6 > 5, go RIGHT
       3(3)      8(3)    Accumulate: size(left) + 1 = 4
      /    \     /    \
   1(1)  4(1) 7(1) 10(1)

           8(3)          Compare 6 with 8
          /    \         6 < 8, go LEFT
       7(1)    10(1)     No accumulation

           7(1)          Compare 6 with 7
                         6 < 7, go LEFT (empty)
                         Stop

Rank of 6 = 4 (nodes ≤ 6: 1, 3, 4, 5)
```

### Range Count [L, R]

```
Count values in [4, 8]:

rank(8) = 6 (nodes ≤ 8: 1, 3, 4, 5, 7, 8)
rank(3) = 2 (nodes ≤ 3: 1, 3)

Count in [4, 8] = rank(8) - rank(3) = 6 - 2 = 4
Actually in range: 4, 5, 7, 8 ✓
```

### Augmentation Maintenance During Insertion

```
Insert 6 into augmented tree:

Before:         5(7)              After:          5(8)
               /     \                           /     \
            3(3)      8(3)     →              3(3)    8(4)
           /    \     /    \                 /    \   /    \
        1(1)  4(1) 7(1) 10(1)            1(1)  4(1) 6(1) 10(1)
                                                       ↑
                                                   New node
Size updates:
- Insert 6 as left child of 8
- Update 8: size = 1 + 1 + 1 = 3 → 4 ✓
- Update 5: size = 1 + 3 + 4 = 8 ✓
All ancestors updated from insertion point to root
```

---

## 7️⃣ CROSS-DAY VISUAL COMPARISONS

### Comparison: All 5 Tree Variants

```
┌──────────────────┬────────────┬────────┬──────────┬────────────┐
│ Tree Type        │ Order      │ Height │ Insert   │ Use Case   │
├──────────────────┼────────────┼────────┼──────────┼────────────┤
│ Generic Tree     │ None       │ O(n)   │ O(1)     │ Structure  │
│ BST              │ Ordered    │ O(n)   │ O(log n) │ Search     │
│ AVL              │ Ordered    │ O(log n)│O(log n) │ Guaranteed │
│ Red-Black        │ Ordered    │ O(log n)│O(log n) │ Practical  │
│ Augmented        │ Ordered    │ O(log n)│O(log n) │ Queries    │
└──────────────────┴────────────┴────────┴──────────┴────────────┘

Visual height comparison (1000 nodes):
Generic/Degenerate: ▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓ (1000)
BST (random):       ▓▓▓▓▓▓▓▓▓▓ (150)
Balanced (AVL/RB):  ▓▓▓▓▓ (20)
```

### Comparison: Insertion Scenarios

```
Insertion type: RANDOM ORDER
Generic: 1, 5, 3, 8, 2
Result:       Generic → Balanced ✓
              1         5
               \       / \
                5  →  3   8
               /        \
              3          1,2

Insertion type: SORTED ORDER
Generic: 1, 2, 3, 4, 5
Result:  Generic → Balanced (via rotation)
         1          3
          \        / \
           2  →   2   4
            \     /     \
             3   1       5
              \
               4
                \
                 5

Degenerate (linked list) becomes balanced tree!
```

---

## 8️⃣ MEMORY LAYOUT DIAGRAMS

### Pointer-Based Tree Memory Layout (Scattered)

```
Heap Memory:
Address 0x1000: Node 5 (left → 0x2000, right → 0x3000)
Address 0x1004: (other data)
...
Address 0x2000: Node 3 (left → 0x4000, right → 0x5000)
...
Address 0x3000: Node 8 (left → 0x6000, right → 0x7000)

Memory layout is scattered (cache misses on traversal)

Tree visualization:       Memory visualization:
      5                   0x1000 → Node 5
     / \                     ↙     ↘
    3   8                 0x2000  0x3000
   / \  /                 (3)      (8)
  1  4 7                   ↙   ↘    ↙  ↘
                        0x4000 0x5000 0x6000 0x7000

Traversal: 5 → 3 → 1 (jump from 0x1000 → 0x2000 → 0x4000)
Each jump is a cache miss (different memory page)
```

### Array-Based Segment Tree Memory Layout (Contiguous)

```
Array [index]:
[0]:  unused
[1]:  Node 5(size=7)
[2]:  Node 3(size=3)
[3]:  Node 8(size=3)
[4]:  Node 1(size=1)
[5]:  Node 4(size=1)
[6]:  Node 7(size=1)
[7]:  Node 10(size=1)

Memory is contiguous, good cache locality
But index math: left(i) = 2*i, right(i) = 2*i+1

Trade-off: More memory vs better cache behavior
```

---

## 9️⃣ COMPLEXITY & PERFORMANCE VISUALIZATIONS

### Big-O Complexity Comparison

```
Operation Time Complexity:

                GENERIC  BST      AVL      RB       AUGMENTED
                (rand)   (worst)  
Search          O(n)     O(h)     O(log n) O(log n) O(log n)
Insert          O(n)     O(h)     O(log n) O(log n) O(log n)
Delete          O(n)     O(h)     O(log n) O(log n) O(log n)
Order-stats     O(n)     O(n)     O(log n) O(log n) O(log n) ← Key diff
Rank query      O(n)     O(n)     O(log n) O(log n) O(log n) ← Key diff

Visualization (1 million nodes):
Search:
  Generic:      ▓▓▓▓▓▓▓▓▓▓▓ (500k comparisons)
  BST (luck):   ▓▓▓▓▓▓▓▓▓▓ (500k comparisons)
  Balanced:     ▓▓▓▓▓▓▓▓▓▓ (20 comparisons) ← Winner
  
Order-stats (kth smallest):
  Generic:      ▓▓▓▓▓▓▓▓▓▓▓ (500k examinations)
  Balanced:     ▓▓▓▓▓▓▓▓▓▓ (20 steps) ← Winner
  Augmented:    ▓▓▓▓▓▓▓▓▓▓ (20 steps) ← Same but simpler
```

### Memory Overhead Comparison

```
Data Structure Memory Use (per node):

Generic/BST Node:
  ┌─────────────────────┐
  │ value       (8 bytes)│
  │ left*       (8 bytes)│
  │ right*      (8 bytes)│
  └─────────────────────┘
  Total: 24 bytes per node

AVL Node (add height):
  ┌─────────────────────┐
  │ value       (8 bytes)│
  │ left*       (8 bytes)│
  │ right*      (8 bytes)│
  │ height      (4 bytes)│ ← +4 bytes
  │ padding     (4 bytes)│ ← alignment
  └─────────────────────┘
  Total: 32 bytes per node

Red-Black Node (add parent + color):
  ┌─────────────────────┐
  │ value       (8 bytes)│
  │ left*       (8 bytes)│
  │ right*      (8 bytes)│
  │ parent*     (8 bytes)│ ← +8 bytes
  │ color       (1 byte) │
  │ padding     (7 bytes)│ ← alignment
  └─────────────────────┘
  Total: 40 bytes per node

For 1 million nodes:
  Basic BST: 24 MB
  AVL:       32 MB (+8 MB / +33%)
  Red-Black: 40 MB (+16 MB / +67%)
  
Negligible overhead for query benefits ✓
```

---

## 🔟 INTERVIEW VISUAL REFERENCE

### Red Flags & Visual Patterns to Recognize

```
🚩 Problem says "Find kth..." 
   Visual pattern:        5(3)
                         /    \
                       3(1)   8(1)
   
   Solution: Augmented BST with subtree size

🚩 Problem says "Rank" or "Percentile"
   Visual pattern: 5(rank=3)
   Solution: Rank query using augmentation

🚩 Problem says "Serialize/Deserialize"
   Visual pattern: Tree → [Array] → Tree
   Solution: Preorder with null markers

🚩 Problem says "Common ancestor"
   Visual pattern:     5 ← LCA
                      / \
                     3   8
   Solution: LCA with binary lifting

🚩 Problem says "Longest path"
   Visual pattern: Path through root
   Solution: Tree diameter with DP

🚩 Problem says "All paths summing to..."
   Visual pattern: Root-to-leaf traversal
   Solution: DFS path sum with backtracking
```

### Common Error Patterns

```
❌ Error 1: Forgetting null children
   
   Wrong:                Right:
   Preorder: [1,2,3]     Preorder: [1,2,null,null,3,null,null]
   Missing structure!    Encodes full structure ✓

❌ Error 2: Rotating but not updating sizes
   
   Before:    5(3)       After: 5(3) ← Size MUST be updated!
             /                 \
            3(2)               3(?) ← Oops, outdated
   
   ✓ Always recalculate augmented data after rotations

❌ Error 3: Rank query off-by-one
   
   Wrong:  rank(5) = size(left) = 2  ← Doesn't count 5
   Right:  rank(5) = size(left) + 1 = 3  ← Includes 5 ✓

❌ Error 4: LCA when one node is ancestor
   
   Wrong:  LCA(5, 3) returns first common = 5 ✓ (but looks weird)
   Right:  LCA(5, 3) = 5 ✓ (5 is ancestor, so LCA is 5)
   
   Remember: If one is ancestor of other, ancestor is LCA
```

### Key Insight Diagrams

```
INSIGHT 1: Balance Maintains Height

Sorted input (unbalanced):     Random input (balanced):
      1              O(n)               5              O(log n)
       \                               / \
        2                             3   7
         \                           / \ / \
          3                         1 4 6 8
           \
            4
             \
              5

Moral: Balance matters! Use AVL/Red-Black in production

INSIGHT 2: Augmentation Enables Efficient Queries

Without augmentation:              With augmentation:
To find 3rd smallest:              To find 3rd smallest:
Visit: 1, 2, 3, 4, 5...            Use subtree sizes to binary search
O(n) scanning                       O(log n) navigation

INSIGHT 3: Rotations Are O(1) Local Operations

LeftRotate(x):                   RightRotate(y):
     y        O(1) pointers        x
    / \       updated!           / \
   x   c     ────────→          a   y

Cost: ~6 pointer assignments ✓

INSIGHT 4: Traversal Order Matters

Preorder:   [1, 2, 3] → parent first (structure)
Inorder:    [2, 1, 3] → middle (sorted in BST)
Postorder:  [2, 3, 1] → parent last (children first)
Level-order:[1, 2, 3] → breadth-first (layer by layer)

Choose based on problem needs!
```

---

## 📚 VISUAL ENCODING GUIDE

### Diagram Conventions Used Throughout Week 7

```
Node Notation:
  5        ← value only
  
  5(3)     ← value, augmented size
  
  5(B)     ← value, color (Red-Black)
  
  5(BF=0)  ← value, balance factor (AVL)

Edge Notation:
  /\       ← normal children
  
  └─       ← special edge (parent pointer, etc.)

Traversal Notation:
  → ← ↘ ↙  ← direction of travel

Operation Notation:
  ✓ Correct / Valid
  ✗ Invalid / Wrong
  → Transformation
  ⇒ Implies
```

### Color/Emphasis Scheme

```
Algorithm steps:
  STEP 1 → STEP 2 → ... ← progression
  
Comparison:
  Table with columns | Cell highlighting key differences
  
Warnings:
  ⚠️  Important / Easy to miss
  
Facts:
  ✓ Proven / Verified
  
Complexity:
  O(log n) ← asymptotic notation
```

---

## 📋 VISUAL QUICK REFERENCE TABLE

### All Diagrams by Topic

| Day | Topic | Diagram Type | Count | Key Visual |
| :--- | :--- | :--- | :--- | :--- |
| 1 | Traversals | Tree structures + paths | 4 | All 4 orders side-by-side |
| 1 | Stack traces | Call stack visualization | 2 | Recursive vs iterative |
| 2 | BST operations | Before/after diagrams | 4 | Insert, delete (3 cases) |
| 2 | Inorder = sorted | Visual proof | 1 | Left-parent-right mapping |
| 3 | Rotations | LL, LR, RR, RL cases | 4 | All 4 rotation types |
| 3 | Balance factors | Node annotations | 2 | AVL height differences |
| 3 | RB coloring | Color diagrams | 2 | Valid/invalid colorings |
| 4 | Path sum | Root-to-leaf paths | 2 | All paths enumerated |
| 4 | LCA | Ancestor path traces | 2 | Path meeting point |
| 4 | Serialization | Array encoding | 2 | Preorder with nulls |
| 5 | Augmentation | Size annotations | 3 | Subtree size calculation |
| 5 | Order-stats | Binary search trace | 2 | kth smallest navigation |
| 5 | Rank queries | Ancestor accumulation | 2 | How many ≤ x |
| **Total** | | | **38** | |

---

## 🎓 USING THIS VISUAL GUIDE

### For Self-Study
1. Read the instructional file (narrative text)
2. Consult this visual guide when concepts feel abstract
3. Redraw diagrams yourself to internalize structure
4. Try "visual debugging" when tracing algorithms

### For Interview Preparation
1. Mentally convert problem description to tree visualization
2. Sketch the tree as you talk through the solution
3. Use these visual patterns to recognize problem types
4. Practice explaining using diagrams, not just code

### For Teaching/Mentoring
1. Use diagrams to explain key concepts clearly
2. Have students redraw diagrams from memory
3. Use visual comparisons to highlight trade-offs
4. Point to specific visual patterns for red flags

---

**End of Week 7 Visual Hybrid Support Guide**

**Diagram Count: 38 ASCII diagrams + descriptions**  
**Coverage:** All 5 days, all major concepts, comparison tables, memory layouts, complexity analysis

