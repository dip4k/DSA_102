# 📊 WEEK 11: VISUAL CONCEPTS PLAYBOOK — HYBRID LEARNING GUIDE

**Document Type:** Visual Learning Reference & Concept Mapping
**Scope:** Week 11 (Days 01-05) — DP on Trees, DAGs, Bitmask, and Advanced Patterns
**Format:** Markdown with ASCII diagrams, flowcharts, and visual representations
**Target:** Visual learners, concept mapping, quick reference
**Updated:** January 2026

---

## 📋 TABLE OF CONTENTS

1. **Visual Framework Overview**
2. **Day 01: Tree DP Visual Concepts**
3. **Day 02: DAG DP Visual Concepts**
4. **Day 03: Bitmask DP Visual Concepts**
5. **Day 04-05: Optimization & Advanced Patterns**
6. **Comparative Analysis Charts**
7. **Algorithm Decision Flowcharts**
8. **Visual Pattern Library**

---

## 🎯 VISUAL FRAMEWORK OVERVIEW

### The DP Paradigm Hierarchy

```
                    DYNAMIC PROGRAMMING
                    /          |          \
                   /           |           \
              LINEAR DP    TREE DP      GRAPH DP
              (1D, 2D)    (Acyclic)    (General/DAG)
                |             |             |
         ┌──────┴──────┐     |        ┌────┴────┐
         |             |     |        |         |
      1D Array      2D Grid  |    Shortest   DAG DP
                              |      Paths
                              |
                    ┌─────────┴─────────┐
                    |                   |
            Single State        Multiple States
            (Aggregation)    (Selection, Coloring)
                    |
          ┌─────────┴──────────┐
          |                    |
      Tree DP            Tree Rerooting
      (Fixed Root)       (Dynamic Root)


    SPECIAL CASES:
    ├── Bitmask DP: Finite subsets
    ├── Digit DP: Number decomposition
    ├── Convex Hull Trick: Optimization
    └── Matrix Exponentiation: Linear recurrence
```

### Key Characteristics Matrix

```
┌────────────────┬──────────┬──────────┬──────────┬──────────┐
│ Property       │ Linear DP│ Tree DP  │ DAG DP   │ Bitmask  │
├────────────────┼──────────┼──────────┼──────────┼──────────┤
│ Time Complexity│ O(n²-n³) │ O(n)     │ O(V+E)   │ O(2^n·n²)│
│ Space          │ O(n)     │ O(n)     │ O(n)     │ O(2^n)   │
│ Input Type     │ Array    │ Tree     │ Graph    │ Subset   │
│ Cycles?        │ No       │ No       │ No       │ N/A      │
│ State Space    │ Bounded  │ Bounded  │ Bounded  │ Finite   │
│ Typical n      │ 1000-500K│ 100K     │ 1000     │ 20       │
└────────────────┴──────────┴──────────┴──────────┴──────────┘
```

---

## 📊 DAY 01: TREE DP VISUAL CONCEPTS

### 1.1 Tree DP Execution Model

```
CONCEPT: Post-Order Traversal Execution
═════════════════════════════════════════════

Original Tree Structure:
                    A
                   / \
                  B   C
                 / \
                D   E

EXECUTION ORDER:
Step 1: Visit D (leaf)        → Compute dp[D]
Step 2: Visit E (leaf)        → Compute dp[E]
Step 3: Visit B (has answers) → Compute dp[B] using dp[D], dp[E]
Step 4: Visit C (leaf)        → Compute dp[C]
Step 5: Visit A (has answers) → Compute dp[A] using dp[B], dp[C]

Post-Order Sequence: D → E → B → C → A ✓

RESULT: Each node computed only after all children computed
        Correct bottom-up order automatically achieved!

Why This Works:
┌─────────────────────────────────────────┐
│ Parent depends on children              │
│ Children are independent of each other  │
│ Post-order ensures dependencies solved  │
│ No revisiting, no cycles               │
└─────────────────────────────────────────┘
```

### 1.2 State Design Patterns

```
PATTERN 1: Single-State (Aggregation)
───────────────────────────────────────

Tree:
     5
    / \
   3   4

Formula: dp[node] = node.value + sum(dp[children])

dp[3] = 3
dp[4] = 4
dp[5] = 5 + 3 + 4 = 12

Visualization:
┌─────────┐
│ Node 5  │ = 5 + (3 + 4) = 12
├─────────┤
│ Value:5 │
│ Agg: +  │
└─────────┘
    / \
   /   \
┌─────┐ ┌─────┐
│  3  │ │  4  │
└─────┘ └─────┘


PATTERN 2: Dual-State (Selection)
──────────────────────────────────

Tree:
      10
      / \
     3   5
    / \
   2   7

States:
├─ dp[node][0] = max value if node EXCLUDED
└─ dp[node][1] = max value if node INCLUDED

         ┌─────────────┐
         │  Node 10    │
         ├──────┬──────┤
     exc │      │ inc  │
      [0]│  14  │  19  │[1]
         └──────┴──────┘
           / \
      exc /   \ inc
         /     \
     ┌──────┐ ┌──────┐
     │ dp[0]│ │ dp[1]│
     │  = 9 │ │  = 3 │
     └──────┘ └──────┘
      (B)


PATTERN 3: Color-State (Counting)
──────────────────────────────────

Tree with K colors:
           A
          / \
         B   C

dp[node][color] = ways to color subtree with node having this color

For K=3:
     ┌─────────────────────┐
     │       Node A        │
     ├─────┬─────┬─────────┤
  Col │  0  │  1  │   2    │
  ─── │─────┼─────┼────────┤
   0  │  4  │  4  │   4    │
   1  │  4  │  4  │   4    │
   2  │  4  │  4  │   4    │
     └─────┴─────┴────────┘

Total = Sum of all states = 12 valid colorings

Pattern: dp[node][color] = ∏(sum(dp[child][other_colors]))
```

### 1.3 Maximum Independent Set Visual Trace

```
PROBLEM: Select non-adjacent nodes to maximize value
═════════════════════════════════════════════════════

Input Tree:
          10(A)
          /    \
        3(B)   5(C)
        / \
      2(D) 7(E)

EXECUTION TRACE:
═══════════════

Step 1: Process D (Leaf)
┌─────────────────────┐
│        Node D       │
├─────────┬───────────┤
│ [exc:0] │ [inc:2]   │
└─────────┴───────────┘
Decision: Include=2, Exclude=0

Step 2: Process E (Leaf)
┌─────────────────────┐
│        Node E       │
├─────────┬───────────┤
│ [exc:0] │ [inc:7]   │
└─────────┴───────────┘

Step 3: Process B (Has children D, E)
         D=[0,2]  E=[0,7]
         
If B included: 3 + 0 + 0 = 3
If B excluded: max(0,2) + max(0,7) = 9

┌─────────────────────┐
│        Node B       │
├─────────┬───────────┤
│ [exc:9] │ [inc:3]   │
└─────────┴───────────┘

Step 4: Process C (Leaf)
┌─────────────────────┐
│        Node C       │
├─────────┬───────────┤
│ [exc:0] │ [inc:5]   │
└─────────┴───────────┘

Step 5: Process A (Root, has children B, C)
         B=[9,3]  C=[0,5]
         
If A included: 10 + 9 + 0 = 19 ✓✓✓
If A excluded: max(9,3) + max(0,5) = 14

┌──────────────────────┐
│        Node A        │
├─────────┬────────────┤
│ [exc:14]│ [inc:19]   │
└─────────┴────────────┘

ANSWER = max(14, 19) = 19

Selected Set: {A, D, E} with values {10, 2, 7}

Verification:
  Is A-D adjacent? No ✓
  Is A-E adjacent? No ✓
  Is D-E adjacent? No ✓
  All non-adjacent ✓
  Sum = 10+2+7 = 19 ✓
```

### 1.4 Tree Diameter Visual Algorithm

```
CONCEPT: Find Longest Path in Tree
═══════════════════════════════════

Key Insight:
The diameter is either:
  1. A path entirely within left subtree
  2. A path entirely within right subtree
  3. A path through the root (left + root + right)

VISUAL ALGORITHM:
─────────────────

For each node, compute:
├─ depth[node] = max distance to any leaf below
└─ diameter_through[node] = depth[left] + depth[right] + 2

Tree:
           A
          / \
         B   C
        / \
       D   E

Step 1: Compute depths (bottom-up)
┌─────┐
│  D  │ depth=0 (leaf)
└─────┘

┌─────┐
│  E  │ depth=0 (leaf)
└─────┘

       ┌─────┐
       │  B  │ depth = 1 + max(0,0) = 1
       └─────┘

┌─────┐
│  C  │ depth=0 (leaf)
└─────┘

         ┌─────┐
         │  A  │ depth = 1 + max(1,0) = 2
         └─────┘

Step 2: Compute diameter through each node

Through D: diameter = 0 (leaf, no children)

Through E: diameter = 0

Through B: 
  children depths = [0, 0]
  diameter = 0 + 0 + 2 = 2

Through C: diameter = 0

Through A:
  children depths = [1, 0] (sorted descending)
  diameter = 1 + 0 + 2 = 3 ← MAXIMUM

ANSWER = 3

Path visualization: D-B-A-C or E-B-A-C (length 3 edges)

       D
       |
       B
      /
     /
    A
     \
      \
       C

Distance: D→B (1) + B→A (1) + A→C (1) = 3 units
```

### 1.5 Tree Coloring Visualization

```
PROBLEM: Color tree with K colors, adjacent nodes different
══════════════════════════════════════════════════════════

Case: K=2 (Bipartite Coloring)

Tree:
    A
   / \
  B   C
     /
    D

Coloring constraint:
  If A is 0 → B,C must be 1
  If A is 1 → B,C must be 0

Valid colorings:
┌─────────────────┐
│ Option 1: A=0   │
│   ├─ B=1        │
│   └─ C=1        │
│       └─ D=0    │
└─────────────────┘
Representation: 0-1-1-0

┌─────────────────┐
│ Option 2: A=1   │
│   ├─ B=0        │
│   └─ C=0        │
│       └─ D=1    │
└─────────────────┘
Representation: 1-0-0-1

Total Valid Colorings = 2

DP State Table:
     ┌─────────────────────┐
     │   Node A            │
     ├─────────┬───────────┤
     │ Color 0 │ Color 1   │
     ├─────────┼───────────┤
 dp  │   1     │    1      │ (ways)
     └─────────┴───────────┘

For K=3:
     ┌─────────────────────────────┐
     │      Node A                 │
     ├────────┬────────┬───────────┤
     │ Color0 │ Color1 │ Color2    │
     ├────────┼────────┼───────────┤
 dp  │   4    │   4    │    4      │
     └────────┴────────┴───────────┘

Total = 12 valid colorings with K=3
```

### 1.6 Tree Rerooting Strategy

```
PROBLEM: For each node as root, what's the answer?
═════════════════════════════════════════════════

Naive: Run full DP n times = O(n²)
Smart: Two passes = O(n) total

Tree (original root = A):
        A(5)
       / \
      B(3) C(4)
     /
    D(2)

PASS 1: DP from original root (A)
───────────────────────────────────

Subtree sums:
  sum[D] = 2
  sum[B] = 3 + 2 = 5
  sum[C] = 4
  sum[A] = 5 + 5 + 4 = 14

Tree state:
        A(14)
       / \
      B(5) C(4)
     /
    D(2)

PASS 2: Reroot and combine
──────────────────────────

Rerooting at B:
  B gets its own subtree: sum_B = 5
  B gets parent's contribution: sum_A - sum_B - B_value = 14 - 5 - 3 = 6
  Wait, recalculate: parent_contribution = parent_answer - B_subtree_size - B_value
  Actually: When B is root, we get 5 (own) + (14-5) = 14

Rerooting at D:
  D gets its own subtree: sum_D = 2
  D gets parent's contribution: 14 - 2 = 12
  Total: 2 + 12 = 14 ✓

Pattern:
  ans[node] = own_subtree_answer + parent_contribution

Flow (Two-Pass DP):
┌──────────┐         ┌──────────┐
│ Pass 1   │         │ Pass 2   │
│ Down DP  │    →    │ Up DP    │
│ From A   │         │ Reroot   │
└──────────┘         └──────────┘

Order: A → B → D (DFS)    Order: A ← B ← D (backtrack)
       ↓   ↓   ↓                  ↑   ↑   ↑
    Compute Compute Compute   Propagate Propagate
    answers  answers  answers  parent    parent
```

---

## 📈 DAY 02: DAG DP VISUAL CONCEPTS

### 2.1 DAG Structure vs Tree vs General Graph

```
COMPARISON: Properties and Implications
════════════════════════════════════════

TREE:
    A
   / \
  B   C
 / \
D   E

Properties:
├─ No cycles
├─ Every node (except root) has exactly 1 parent
├─ No shared descendants
└─ Acyclic: guaranteed

DP implication: Perfect bottom-up ordering


DAG (Directed Acyclic Graph):
    A
   /|\
  B C D
   \|/
    E

Properties:
├─ No cycles (directed)
├─ Nodes can have multiple parents
├─ Descendants can be shared
└─ Acyclic: topological ordering exists

DP implication: Must respect topological order
               Multiple paths to same node


GENERAL GRAPH (With Cycles):
    A ─┐
   /│  │
  B C  │
   \│  │
    D ─┘

Properties:
├─ Can have cycles
├─ Multiple parents possible
├─ Cycles create dependencies
└─ May visit node multiple times

DP implication: Pure DP fails!
               Need different approaches (Bellman-Ford, etc.)

DP APPLICABILITY:
┌──────────────┬────────────┬──────────┬────────────┐
│ Structure    │ Acyclic?   │ DP Works?│ Ordering   │
├──────────────┼────────────┼──────────┼────────────┤
│ Tree         │ Yes        │ Yes      │ Post-order │
│ DAG          │ Yes        │ Yes      │ Topo-sort  │
│ General      │ No         │ No (pure)│ N/A        │
└──────────────┴────────────┴──────────┴────────────┘
```

### 2.2 Topological Ordering Visualization

```
CONCEPT: Total ordering respecting dependencies
═════════════════════════════════════════════════

DAG Example (Project Dependencies):
       Design(D)
        |    \
        v     \
    Code(C) ─→ Test(T)
        |       |
        └──────→─┘
                |
                v
             Deploy(De)

Dependency relationships:
  D → C, D → T
  C → T, C → De
  T → De

TOPOLOGICAL SORT (DFS-based):
─────────────────────────────

Post-order DFS:
1. Visit D → recursively visit children
   - Visit C → recursively visit children
     - Visit T → recursively visit children
       - Visit De (no children) → finish De → add to order
     - finish T → add to order
   - finish C → add to order
2. finish D → add to order

DFS finish order: De → T → C → D
Reversed: D → C → T → De ✓ Valid topo sort!

Verification:
  D before C? Yes (D→C) ✓
  D before T? Yes (D→T) ✓
  C before T? Yes (C→T) ✓
  C before De? Yes (C→De) ✓
  T before De? Yes (T→De) ✓

Alternative Topo Sort (Kahn's Algorithm):
──────────────────────────────────────────

Process by in-degree:
        In-degree:
D: 0 ← START HERE
C: 1 (depends on D)
T: 2 (depends on D, C)
De: 3 (depends on C, T)

Step 1: Process D (in-degree 0)
  Output: [D]
  Decrease neighbors: C in-deg 1→0, T in-deg 2→1

Step 2: Process C (in-degree 0)
  Output: [D, C]
  Decrease neighbors: T in-deg 1→0, De in-deg 3→2

Step 3: Process T (in-degree 0)
  Output: [D, C, T]
  Decrease neighbors: De in-deg 2→1

Step 4: Process De (in-degree... wait, it's still 1?)

Issue: De still has De in-degree = 1 (from earlier). Need to track carefully.
Recalculate in-degrees:
  D: 0
  C: 1 (D)
  T: 2 (D, C)
  De: 2 (C, T)

After processing D: T: 1, De: 1
After processing C: T: 0, De: 1
After processing T: De: 0
Process De

Result: D → C → T → De ✓
```

### 2.3 Longest Path in DAG

```
ALGORITHM: Find longest path using DP + Topo Sort
═════════════════════════════════════════════════

DAG with weighted edges:
       A
      /|\
   2 / 3│1 \
    /   │   \
   B    C    D
    \  /|   /
     4 2│3 /
       \│ /
        E

State: dp[node] = longest path starting from this node
Transition: dp[node] = 1 + max(dp[neighbor] for all neighbors)

Step 1: Topological sort
  Kahn's algorithm: A → B → C → D → E

Step 2: Process in reverse topo order (E to A)
─────────────────────────────────────────────

E (sink, no outgoing edges):
  dp[E] = 0 (no further path)

D:
  Neighbors: [E]
  dp[D] = 1 + dp[E] = 1 + 0 = 1

C:
  Neighbors: [D, E]
  dp[C] = max(1 + dp[D], 1 + dp[E])
        = max(1 + 1, 1 + 0)
        = max(2, 1) = 2

B:
  Neighbors: [E]
  dp[B] = 1 + dp[E] = 1 + 0 = 1

A:
  Neighbors: [B, C, D]
  dp[A] = max(1 + dp[B], 1 + dp[C], 1 + dp[D])
        = max(1 + 1, 1 + 2, 1 + 1)
        = max(2, 3, 2) = 3

Longest path = dp[A] = 3
Path reconstruction: A → C → D → E (distance 3)

COMPARISON: Naive vs DP
───────────────────────

Naive (DFS from each node):
  For each node, DFS to find longest path
  Time: O(V × (V + E)) = O(V² + VE)

DP with Topo Sort:
  1. Topological sort: O(V + E)
  2. Process each node once: O(V + E) transitions
  Total: O(V + E)

Speedup: O(V²) → O(V) for dense graphs!
```

### 2.4 DAG DP Problem Template

```
GENERIC DAG DP FRAMEWORK
════════════════════════

1. Build DAG from input
2. Check it's acyclic (optional validation)
3. Topological sort
4. Define DP state: what does dp[node] represent?
5. Determine transition: combine what?
6. Process in topo order

Example: Shortest path in DAG with negative weights
───────────────────────────────────────────────────

Can't use Dijkstra (negative edges)
Can use Bellman-Ford O(VE)
But DAG DP is faster: O(V + E)

Algorithm:
  For each node in topo order:
    For each outgoing edge (u, v) with weight w:
      dist[v] = min(dist[v], dist[u] + w)

Why it works:
  Topo order ensures dist[u] finalized before processing outgoing edges
  No need for iteration (unlike Bellman-Ford which needs V-1 passes)

Tree:
      S(dist=0)
     /  \2
   1/    \
   /      v
  A ─3──→ B
   \      |
    \6    |5
     \    |
      \   v
       \→ C

Topo order: S → A → B → C

Process S:
  Update A: dist[A] = min(∞, 0+1) = 1
  Update B: dist[B] = min(∞, 0+2) = 2

Process A:
  Update B: dist[B] = min(2, 1+3) = 2
  Update C: dist[C] = min(∞, 1+6) = 7

Process B:
  Update C: dist[C] = min(7, 2+5) = 7

Process C:
  No outgoing edges

Result: dist[C] = 7 via S→A→B→C
```

---

## 📊 DAY 03: BITMASK DP VISUAL CONCEPTS

### 3.1 Bitmask Representation and Subset Enumeration

```
CONCEPT: Representing sets as integers
═══════════════════════════════════════

Universe: {A, B, C} (3 elements)

Subset representation:
  ∅       → 000 → 0
  {A}     → 001 → 1
  {B}     → 010 → 2
  {A,B}   → 011 → 3
  {C}     → 100 → 4
  {A,C}   → 101 → 5
  {B,C}   → 110 → 6
  {A,B,C} → 111 → 7

Bit meaning:
  Bit i = 1 if element i in subset
  Bit i = 0 if element i not in subset

Operations:
──────────

Check if element i in mask:
  if (mask & (1 << i)) ...

Add element i to mask:
  mask | (1 << i)

Remove element i from mask:
  mask & ~(1 << i)

Number of elements in mask:
  __builtin_popcount(mask)

All subsets of n elements:
  for (int mask = 0; mask < (1 << n); mask++)
    Process subset represented by mask

ENUMERATION VISUALIZATION:
──────────────────────────

n=3 elements {A,B,C}
2^3 = 8 subsets

Counter sequence: 0 → 1 → 2 → 3 → 4 → 5 → 6 → 7

000 (0)   001 (1)   010 (2)   011 (3)
 ∅        {A}       {B}       {A,B}

100 (4)   101 (5)   110 (6)   111 (7)
 {C}      {A,C}     {B,C}     {A,B,C}

Subset chain (by size):
  Size 0: {∅}
  Size 1: {A}, {B}, {C}
  Size 2: {A,B}, {A,C}, {B,C}
  Size 3: {A,B,C}

Time complexity to enumerate all:
  O(2^n) subsets × O(n) work per subset = O(n × 2^n)
```

### 3.2 TSP with Bitmask DP Visualization

```
PROBLEM: Traveling Salesman Problem with DP
════════════════════════════════════════════

Find minimum-cost tour visiting all cities exactly once, returning to start.

Setup: 4 cities {0, 1, 2, 3}
Cost matrix:
      0  1  2  3
  0 [ 0  1  4  9]
  1 [ 1  0  2  3]
  2 [ 4  2  0  7]
  3 [ 9  3  7  0]

DP STATE:
──────────
dp[mask][last] = minimum cost to visit cities in 'mask', ending at 'last'

Where:
  mask = bitmask of visited cities
  last = current position

EXAMPLE COMPUTATION:
───────────────────

Start at city 0:
  dp[0001][0] = 0 (visited only 0, at 0, no cost)

From 0, visit 1:
  mask = 0011 (visited 0 and 1)
  dp[0011][1] = dp[0001][0] + cost[0][1] = 0 + 1 = 1

From 0, visit 2:
  mask = 0101
  dp[0101][2] = dp[0001][0] + cost[0][2] = 0 + 4 = 4

From 0, visit 3:
  mask = 1001
  dp[1001][3] = dp[0001][0] + cost[0][3] = 0 + 9 = 9

Continue from state dp[0011][1]:
  At city 1, visited {0,1}, can go to 2 or 3
  
  Go to 2:
    mask = 0111
    dp[0111][2] = min(dp[0111][2], dp[0011][1] + cost[1][2])
                = min(∞, 1 + 2) = 3
  
  Go to 3:
    mask = 1011
    dp[1011][3] = min(dp[1011][3], dp[0011][1] + cost[1][3])
                = min(∞, 1 + 3) = 4

Final state (visited all, at some city):
  mask = 1111 (all 4 cities visited)
  dp[1111][0] = visited all, ended at 0
  dp[1111][1] = visited all, ended at 1
  dp[1111][2] = visited all, ended at 2
  dp[1111][3] = visited all, ended at 3

TSP tour cost = min(dp[1111][i]) + cost[i][0] for all i
              (return to start)

DP TABLE VISUALIZATION (Partial):
─────────────────────────────────

mask | 0      | 1       | 2       | 3
─────┼────────┼─────────┼─────────┼─────────
0001 | 0      | ∞       | ∞       | ∞
0011 | ∞      | 1       | ∞       | ∞
0101 | ∞      | ∞       | 4       | ∞
1001 | ∞      | ∞       | ∞       | 9
0111 | ∞      | ∞       | 3       | 4
1011 | ∞      | 4       | ∞       | 4
1101 | ∞      | 6       | 5       | ∞
1111 | ?      | ?       | ?       | ?

COMPLEXITY ANALYSIS:
───────────────────

States: 2^n masks × n cities = O(n × 2^n)
Transitions: From each state, try n-1 unvisited cities = O(n)
Total: O(n² × 2^n)

For n=10: 10² × 2^10 = 100 × 1024 ≈ 100K (feasible)
For n=20: 20² × 2^20 = 400 × 1M ≈ 400M (slow but doable)
For n=25: 25² × 2^25 = 625 × 33M ≈ 20B (too slow)

Practical limit: n ≤ 20
```

### 3.3 Subset Sum with Bitmask

```
PROBLEM: Select subset of items to reach target sum
───────────────────────────────────────────────────

Items: {weights: [1,2,5], values: [2,3,7]}
Target: Sum = 6

DP with bitmask (checking all subsets):
───────────────────────────────────────

for mask = 0 to 2^n - 1:
  sum = 0
  value = 0
  
  for i = 0 to n-1:
    if bit i set in mask:
      sum += weight[i]
      value += value[i]
  
  if sum == target:
    found! value is the answer

Enumeration:
mask | Items    | Sum | Value | Valid?
────┼──────────┼─────┼───────┼────────
000 | ∅        | 0   | 0     | No
001 | {1}      | 1   | 2     | No
010 | {2}      | 2   | 3     | No
011 | {1,2}    | 3   | 5     | No
100 | {5}      | 5   | 7     | No
101 | {1,5}    | 6   | 9     | YES ✓
110 | {2,5}    | 7   | 10    | No
111 | {1,2,5}  | 8   | 12    | No

Answer: Subset {1,5} with sum 6, value 9

Time: O(n × 2^n) to enumerate all subsets and compute sums
      O(2^n) if precomputed with DP

DP Optimization:
dp[mask] = value when selecting subset 'mask'

Can also compute: reachable[i] = set of achievable sums using first i items
Then check if target in reachable[n]
```

### 3.4 Maximum Weight Independent Set (Small Graph)

```
PROBLEM: Select nodes with max total weight, no edges between
──────────────────────────────────────────────────────────────

Graph (5 nodes):
    0 ─── 1
    |     |
    2 ─── 3
         /
        4

Weights: [10, 7, 5, 8, 6]

Edge list (adjacency):
  0: [1, 2]
  1: [0, 3]
  2: [0, 3]
  3: [1, 2, 4]
  4: [3]

Approach: Check all 2^5 = 32 subsets

For each mask:
  1. Compute total weight of nodes in mask
  2. Check if it's a valid independent set (no edges)
  3. Track maximum weight

Valid independent sets:
  {0}: weight = 10 ✓
  {1}: weight = 7 ✓
  {2}: weight = 5 ✓
  {3}: weight = 8 ✓
  {4}: weight = 6 ✓
  {0,3}: weight = 18 (edge 0-1? No. 0-2? Yes!) ✗
  {0,4}: weight = 16 (edge 0-1? No. 0-2? No. 4-3? Yes!) ✓
  {1,2}: weight = 12 (edge 1-0? No. 1-3? Yes!) ✗
  {2,4}: weight = 11 (edge 2-0? No. 2-3? No. 4-3? Yes!) ✓

Maximum valid: {0,4} with weight 16

Enumeration with validity check:
mask | Nodes    | Weight | Valid? | Reason
────┼──────────┼────────┼────────┼─────────────────
00000 | ∅      | 0      | ✓      | (empty is valid)
00001 | {0}    | 10     | ✓      | No edges
00010 | {1}    | 7      | ✓      | No edges
00100 | {2}    | 5      | ✓      | No edges
01000 | {3}    | 8      | ✓      | No edges
10000 | {4}    | 6      | ✓      | No edges
00011 | {0,1}  | 17     | ✗      | Edge 0-1
00101 | {0,2}  | 15     | ✗      | Edge 0-2
01001 | {0,3}  | 18     | ✗      | Edge 0-3? (check) No edge 0-3? Wait...
       |        |        |        | (0-1? Yes) Actually 1 not in mask
       |        |        |        | (0-2? Yes) But 2 not in mask. 
       |        |        |        | (0-other?) No.
       |        |        | ✓      | Valid! weight=18 but...
       |        |        |        | Wait, let me recheck...

Actually checking {0,3}: 
  Is 0 adjacent to 3? No direct edge.
  Is 3 adjacent to 0? No.
  ✓ Valid

Continue...
01010 | {1,3}  | 15     | ✗      | Edge 1-3
10001 | {0,4}  | 16     | ✓      | No edge 0-4
10010 | {1,4}  | 13     | ✓      | No edge 1-4
10100 | {2,4}  | 11     | ✓      | No edge 2-4
...

Maximum weight = 18 from {0,3}

Time: O(2^n) to enumerate × O(n²) to check validity = O(n² × 2^n)
Space: O(2^n) if storing all subsets

Feasible for n ≤ 20
```

---

## 📊 DAY 04-05: OPTIMIZATION & ADVANCED PATTERNS

### 4.1 State Compression Visualization

```
TECHNIQUE: Reduce dimensionality of DP state
═════════════════════════════════════════════

Example 1: 2D Grid DP → 1D
─────────────────────────

Problem: Minimum path sum from top-left to bottom-right

Full 2D DP:
  dp[i][j] = minimum cost to reach (i,j)

Grid:
    1  2  3
    4  5  6
    7  8  9

Standard 2D DP table:
    dp[0][0]=1  dp[0][1]=3  dp[0][2]=6
    dp[1][0]=5  dp[1][1]=10 dp[1][2]=16
    dp[2][0]=12 dp[2][1]=20 dp[2][2]=29

Space: O(m × n)

Observation: When computing row i, we only need:
  - Current row (being computed)
  - Previous row (dp[i-1][...])

We don't need rows 0 to i-2!

Space-optimized version:
  prev[] = previous row
  curr[] = current row

for i = 0 to m-1:
  for j = 0 to n-1:
    curr[j] = grid[i][j] + min(prev[j], curr[j-1])
  swap(prev, curr)

Space: O(n) instead of O(m × n)

Visual evolution:
Initial: prev = [0, ∞, ∞]

Row 0:
  curr[0] = 1 + min(0, ∞) = 1
  curr[1] = 2 + min(∞, 1) = 3
  curr[2] = 3 + min(∞, 3) = 6
  curr = [1, 3, 6]
  swap: prev = [1, 3, 6]

Row 1:
  curr[0] = 4 + min(1, ∞) = 5
  curr[1] = 5 + min(3, 5) = 8
  curr[2] = 6 + min(6, 8) = 12
  curr = [5, 8, 12]
  swap: prev = [5, 8, 12]

Row 2:
  curr[0] = 7 + min(5, ∞) = 12
  curr[1] = 8 + min(8, 12) = 16
  curr[2] = 9 + min(12, 16) = 21
  curr = [12, 16, 21]

Answer: 21 (same as full 2D, but used O(n) space)

Example 2: 3D DP → 2D
────────────────────

Problem: DP[day][item][state] → reduce 3D

If "state" is independent across items, might compress.
If transitions only need current day, can use rolling array.

General principle:
  Only keep what you need for transitions
  Discard older states
```

### 4.2 Algorithm Decision Flowchart

```
DECISION TREE: Choosing the Right DP Variant
══════════════════════════════════════════════

    Start Problem
         |
    Is input a TREE?
    /            \
   YES            NO
   |              |
   v              v
TREE DP      Is input a DAG?
             /          \
           YES           NO
           |             |
           v             v
         DAG DP     Is input SEQUENCE/GRID?
                    /            \
                  YES            NO
                  |              |
                  v              v
            LINEAR DP        Is n ≤ 20?
         (1D, 2D, etc)      /        \
                           YES        NO
                           |          |
                           v          v
                      BITMASK DP  OTHER
                    (Subset DP)  (Heuristics,
                                  Approximation)

Once chosen:

TREE DP Path:
  1. Define state: dp[node][state_var]
  2. Post-order traversal (children before parent)
  3. Combine children's answers
  4. Time: O(n)

DAG DP Path:
  1. Topological sort
  2. Define state: dp[node]
  3. Process in topo order
  4. Time: O(V + E)

LINEAR DP Path:
  1. Define state: dp[i] or dp[i][j]
  2. Define transitions
  3. Bottom-up iteration
  4. Optimize space if needed
  5. Time: O(n) to O(n²)

BITMASK DP Path:
  1. Represent state as bitmask
  2. Iterate all 2^n subsets
  3. For each, check validity and compute answer
  4. Time: O(n × 2^n) to O(n² × 2^n)
```

### 4.3 Complexity and Feasibility Chart

```
CHART: When each DP variant is practical
═════════════════════════════════════════

┌─────────────┬──────────────┬────────────────┬──────────────┐
│ DP Type     │ Time Complex │ Max n          │ Examples     │
├─────────────┼──────────────┼────────────────┼──────────────┤
│ Tree DP     │ O(n)         │ 100K+          │ Max IS       │
│ DAG DP      │ O(V+E)       │ 1K-10K         │ Longest path │
│ Linear DP   │ O(n²) to O(n)│ 1K-100K        │ LIS, Edit    │
│ 2D Grid DP  │ O(m×n)       │ 100×100 to     │ Path sum     │
│             │              │ 1000×1000      │              │
│ Bitmask DP  │ O(n×2^n)     │ 10-20          │ TSP, subsets │
│ Bitmask DP  │ O(n²×2^n)    │ 10-15          │ TSP variant  │
│ 3D DP       │ O(n³)        │ 100-500        │ Matrix mult  │
└─────────────┴──────────────┴────────────────┴──────────────┘

ACTUAL RUNTIME ESTIMATES (Modern computers, ~10^8-10^9 ops/sec):

Tree DP (n=100K):
  O(n) = 100K ops → ~0.01ms ✓ Fast

DAG DP (V=1K, E=5K):
  O(V+E) = 6K ops → ~0.01ms ✓ Very fast

Linear 1D DP (n=1M):
  O(n) = 1M ops → ~1ms ✓ Fast

Linear 2D DP (n=1K, m=1K):
  O(n²) = 1M ops → ~1ms ✓ Fast

2D Grid DP (m=1K, n=1K):
  O(m×n) = 1M ops → ~1ms ✓ Fast
  (BUT if m=10K, n=10K → 100M ops → 100ms, still OK)

Bitmask DP (n=15):
  O(15 × 2^15) = 15 × 32K ≈ 500K ops → ~0.5ms ✓ Fast

Bitmask DP (n=20):
  O(20 × 2^20) = 20 × 1M ≈ 20M ops → ~20ms ✓ OK

Bitmask DP (n=25):
  O(25 × 2^25) = 25 × 33M ≈ 800M ops → ~800ms ✓ Borderline

TSP variant (n=15):
  O(15² × 2^15) = 225 × 32K ≈ 7M ops → ~7ms ✓ Fast

TSP variant (n=20):
  O(20² × 2^20) = 400 × 1M ≈ 400M ops → ~400ms ✓ Slow but doable

TSP variant (n=25):
  O(25² × 2^25) = 625 × 33M ≈ 20B ops → ~20s ✗ Too slow

PRACTICAL LIMITS:
  Tree DP: millions of nodes
  DAG DP: thousands to tens of thousands
  Linear DP: millions
  2D Grid: 1K×1K to 10K×10K
  Bitmask: 15-20 (occasionally 25 with optimization)
```

---

## 📌 VISUAL PATTERN LIBRARY

### Problem Recognition Guide

```
Pattern recognition based on problem statement:
════════════════════════════════════════════════

TREE DP SIGNALS:
  "For each subtree..."
  "Compute X for this tree"
  "Select non-adjacent nodes"
  "Find longest path in tree"
  "Color the tree"
  "Maximize/minimize selecting nodes"
  
  Shape clues:
  ├─ Hierarchical structure (org chart, filesystem)
  ├─ Parent-child relationships
  ├─ Binary tree structure
  └─ Forest or subtree processing

DAG DP SIGNALS:
  "Project scheduling"
  "Topological order"
  "Longest/shortest path"
  "Dependency resolution"
  "Find critical path"
  
  Shape clues:
  ├─ Directed edges (one-way relationships)
  ├─ Dependencies between tasks
  ├─ No cycles explicitly mentioned
  ├─ Process order matters
  └─ Precedence constraints

LINEAR/2D DP SIGNALS:
  "Sequence of choices"
  "Optimize over array/string"
  "Build from smaller problems"
  "Count/find ways"
  
  Shape clues:
  ├─ Array or sequence input
  ├─ String matching/manipulation
  ├─ Grid navigation
  ├─ 2D matrix operations
  └─ Bottom-up building

BITMASK DP SIGNALS:
  "Select subset"
  "Visit all cities"
  "Traveling salesman"
  "Assign items to sets"
  "Covering problems"
  
  Shape clues:
  ├─ Small n (≤20)
  ├─ 2^n possibilities
  ├─ Subset enumeration
  ├─ All-pairs something
  └─ Permutation-like problems

STATE COMPRESSION SIGNALS:
  "Optimize space"
  "Memory limit exceeded"
  "Need O(n) instead of O(n²)"
  
  Optimization clues:
  ├─ Only current/previous layer needed
  ├─ Only last k values matter
  ├─ Current state independent of old states
  └─ Rolling window pattern
```

### Transition Pattern Diagrams

```
COMMON TRANSITION PATTERNS
═══════════════════════════

Pattern 1: Inclusion/Exclusion
───────────────────────────────

State per choice:
  dp[i][0] = best without choosing i
  dp[i][1] = best with choosing i

Transition:
  Without: combine children's "best of both"
  With: combine children's "must be without"

Example: Maximum independent set, House robber, 0/1 Knapsack

     ┌─────────────┐
     │   Choice    │
     ├─────┬───────┤
     │Take │ Skip  │
     │  1  │  0    │
     └─────┴───────┘
        ↓     ↓
    Cascades to children


Pattern 2: Aggregation
──────────────────────

State:
  dp[i] = sum/product of all children

Transition:
  dp[i] = node_value + sum(dp[child])

Example: Subtree sum, Tree product

     ┌─────────────┐
     │ Node Value  │
     │   + Sum     │
     │  Children   │
     └─────────────┘
           ↑
      (accumulate)


Pattern 3: Path Optimization
─────────────────────────────

State:
  dp[i] = best metric for paths in subtree

Transition:
  Through this node: combine two best child paths
  Best overall: max of all node options

Example: Tree diameter, Maximum path sum

     ┌──────────────┐
     │ Two best     │
     │ child paths  │
     │   combine    │
     └──────────────┘
           ↑
    (pick top 2)


Pattern 4: Counting
───────────────────

State:
  dp[i][state] = number of ways

Transition:
  Multiply ways from children (if independent)
  Subtract invalid combinations

Example: K-coloring, Number of structures

     ┌────────────────┐
     │ Ways per color │
     │  multiply all  │
     │    children    │
     └────────────────┘
           ↑
      (multiplicative)
```

---

## 📚 SUMMARY & QUICK REFERENCE

### One-Page Cheat Sheet

```
WEEK 11 DP PATTERNS — QUICK REFERENCE
══════════════════════════════════════

1. TREE DP
   Time: O(n)
   Pattern: Post-order traversal
   State: dp[node][state_var]
   Combine: Children's answers + Node's value
   
   Common: Max IS, Diameter, Coloring, Rerooting

2. DAG DP
   Time: O(V+E)
   Pattern: Topological sort
   State: dp[node]
   Combine: Neighbors in topo order
   
   Common: Longest path, Scheduling, Dependencies

3. LINEAR DP
   Time: O(n) to O(n²)
   Pattern: Iteration
   State: dp[i] or dp[i][j]
   Combine: Previous states
   
   Common: LIS, Edit distance, Coin change

4. BITMASK DP
   Time: O(n × 2^n) to O(n² × 2^n)
   Pattern: Enumerate all subsets
   State: dp[mask][...] or dp[mask]
   Combine: Add/remove elements
   
   Common: TSP, Subset problems, Assignments

5. OPTIMIZATION
   Technique: Space compression (rolling array)
   Reduce: O(n²) → O(n) space
   When: Only adjacent states needed
   
   Common: Grid DP, Sequential processing

STATE DEFINITION CHECKLIST:
  ✓ What does the state represent?
  ✓ What are the state variables?
  ✓ What's the range of each variable?
  ✓ What's the total number of states?
  
TRANSITION CHECKLIST:
  ✓ How do we move from one state to another?
  ✓ What are the dependencies?
  ✓ Are transitions valid/feasible?
  ✓ What's the order of computation?
  
BASE CASE CHECKLIST:
  ✓ What's the simplest case?
  ✓ What values for the base case?
  ✓ Is there only one or multiple?
  ✓ Are they correct?
  
COMPLEXITY CHECKLIST:
  ✓ How many states total?
  ✓ Work per state?
  ✓ Total time complexity?
  ✓ Space needed?
  ✓ Is it feasible for n?

DEBUGGING SIGNALS:
  ⚠ Wrong answer: Check transitions and base cases
  ⚠ TLE (timeout): Optimize complexity or prune
  ⚠ MLE (memory): Compress state space
  ⚠ Segfault: Check array bounds and base cases
  ⚠ Off-by-one: Verify indexing carefully
```

---

**End of Week 11 Visual Concepts Playbook**

*Complete visual learning guide for Days 01-05*
*Diagrams, flowcharts, and conceptual maps throughout*
*Quick reference and pattern library included*