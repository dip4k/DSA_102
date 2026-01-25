# 📘 DSA COMPLETE SYLLABUS v13 - REMAINING CONTENT
## PHASE C (Weeks 10-11) through PHASE G (Week 19)

---

# 🟨 PHASE C (continued): DYNAMIC PROGRAMMING
## Weeks 10-11 | 30-35 hours | Mastering Optimization via DP

---

## 📌 WEEK 10: DYNAMIC PROGRAMMING I - FUNDAMENTALS

### Weekly Goal
Master DP fundamentals from recursion + memoization to tabulation, solving 1D, 2D, and sequence problems.

### Weekly Outcomes
- Identify overlapping subproblems and optimal substructure
- Implement top-down and bottom-up DP
- Solve knapsack family problems
- Master grid and edit distance problems
- Apply DP to sequences

### Summary
Dynamic programming is a powerful optimization technique. This week teaches the mindset: break into subproblems, cache results, build up solution. Students learn the fundamental patterns that apply across hundreds of problems.

---

### 📅 DAY 1: DP AS RECURSION + MEMOIZATION | 90 min

**Topics:**
- **Overlapping Subproblems Concept**
  - Same subproblem solved repeatedly in recursion
  - Fibonacci: fib(5) = fib(4) + fib(3), fib(4) = fib(3) + fib(2)
  - fib(3) computed multiple times
  - Exponential time without caching

- **Memoization = Recursion + Cache**
  - Check cache first before computing
  - If cached: return immediately
  - Else: compute, store in cache, return
  - Dramatically reduces time (exponential to polynomial)

- **Fibonacci with Memoization**
  - memo[n] = fib(n) result
  - Base: memo[0]=0, memo[1]=1
  - Recursive: if n in memo return memo[n], else compute
  - Time: O(n) instead of O(2^n)

- **Top-Down DP (Memoization)**
  - Start with full problem
  - Recurse on subproblems
  - Cache results
  - Natural from problem structure
  - Can be inefficient if not all subproblems needed

- **Bottom-Up DP (Tabulation)**
  - Solve small problems first
  - Build up to full problem
  - No recursion, just iteration
  - More efficient (fewer function calls)
  - Need to identify order of computation

- **Recursive vs Iterative**
  - Recursive: cleaner code, intuitive
  - Iterative: faster (less overhead)
  - Choose based on problem and preference

- **DP Table & State Definition**
  - State: what does each cell represent?
  - Transitions: how to compute from other cells?
  - Base case: what are simplest subproblems?
  - Answer: which cell contains solution?

- **Example: Climbing Stairs**
  - Problem: climb n stairs, can take 1 or 2 steps per move
  - State: ways[i] = number of ways to reach stair i
  - Base: ways[0]=1, ways[1]=1
  - Transition: ways[i] = ways[i-1] + ways[i-2]
  - Answer: ways[n]

---

### 📅 DAY 2: 1D DP & KNAPSACK FAMILY | 120 min

**Topics:**
- **1D DP Patterns**
  - Array dp[i] representing answer for problem of size i
  - Transition: dp[i] depends on dp[j] for j < i
  - Time: O(n), Space: O(n) or O(1) if optimizable

- **Climbing Stairs Variants**
  - Basic: ways to reach stair n taking 1 or 2 steps
  - With cost: minimum cost to reach stair n
  - With constraints: can't take consecutive 2-step moves
  - General: given step sizes, compute answer

- **House Robber (Non-Adjacent Sum)**
  - Given array of house values, rob non-adjacent houses
  - Maximize: rob[i] = max(rob[i-1], rob[i-2] + arr[i])
  - Intuition: at each house, either rob it or skip
  - Time: O(n), Space: O(1) optimized

- **House Robber II (Circular)**
  - Houses in circle: can't rob first and last together
  - Solution: max(rob excluding first, rob excluding last)

- **Coin Change**
  - Problem: minimum coins to make amount
  - State: dp[i] = min coins for amount i
  - Transition: dp[i] = min(dp[i-coin] + 1 for each coin)
  - Time: O(amount × coins)

- **Coin Change II (Number of Ways)**
  - Count ways to make amount
  - Different DP: dp[i] = number of ways for amount i
  - Transition: dp[i] += dp[i-coin] for each coin
  - Careful: order matters (different from permutations)

- **0/1 Knapsack (Single Item)**
  - Given items with weight and value, capacity W
  - Maximize value without exceeding weight
  - Classic DP problem
  - dp[i][w] = max value with first i items and weight w
  - Transition: take or skip item
  - Time: O(n × W), Space: O(W) optimized

- **Unbounded Knapsack (Infinite Items)**
  - Each item available unlimited times
  - dp[i] = max value with capacity i
  - Transition: dp[i] = max(dp[i-weight[j]] + value[j] for all items j)
  - Time: O(W × items)

---

### 📅 DAY 3: 2D DP - GRIDS & EDIT DISTANCE | 120 min

**Topics:**
- **2D Grid DP**
  - State: dp[i][j] = answer for cell (i, j)
  - Often: number of ways or minimum cost
  - Transition: combine from previous cells

- **Unique Paths in Grid**
  - Given grid with obstacles, count paths from top-left to bottom-right
  - Can move right or down
  - State: dp[i][j] = ways to reach (i, j)
  - Base: dp[0][0] = 1 if no obstacle
  - Transition: dp[i][j] = dp[i-1][j] + dp[i][j-1]
  - Time: O(m × n), Space: O(n) optimized

- **Minimum Path Sum**
  - Given grid with costs, find minimum cost path
  - dp[i][j] = minimum cost to reach (i, j)
  - Transition: dp[i][j] = cost[i][j] + min(dp[i-1][j], dp[i][j-1])

- **Edit Distance (Levenshtein)**
  - Transform word1 to word2 with insert/delete/replace
  - State: dp[i][j] = min edits to transform word1[0..i-1] to word2[0..j-1]
  - Transitions:
    - If characters match: dp[i][j] = dp[i-1][j-1]
    - Else: min(dp[i-1][j], dp[i][j-1], dp[i-1][j-1]) + 1
  - Time: O(m × n)

- **Longest Common Subsequence (LCS)**
  - Longest sequence present in both strings
  - State: dp[i][j] = length of LCS of word1[0..i-1] and word2[0..j-1]
  - Transitions:
    - If match: dp[i][j] = dp[i-1][j-1] + 1
    - Else: dp[i][j] = max(dp[i-1][j], dp[i][j-1])
  - Time: O(m × n)

- **Matrix Chain Multiplication**
  - Given matrices, find optimal order to multiply
  - State: dp[i][j] = min operations to multiply matrices i to j
  - Transition: try all split points
  - Time: O(n³)

- **Space Optimization**
  - 2D DP often reducible to 1D if only previous row needed
  - Save space from O(m × n) to O(n)

---

### 📅 DAY 4: DP ON SEQUENCES | 120 min

**Topics:**
- **Longest Increasing Subsequence (LIS)**
  - Find longest subsequence with increasing values
  - O(n²) DP: dp[i] = longest ending at i
    - Transition: dp[i] = max(dp[j] + 1 for all j < i where arr[j] < arr[i])
  - O(n log n) binary search: maintain tail array
  - Time: O(n²) or O(n log n)

- **Longest Decreasing Subsequence**
  - Symmetric to LIS
  - Same approaches apply

- **Maximum Subarray Sum (Kadane)**
  - dp[i] = max sum ending at i
  - Transition: dp[i] = max(arr[i], dp[i-1] + arr[i])
  - Answer: max(dp[i])

- **Weighted Interval Scheduling**
  - Intervals with weights, select non-overlapping to maximize weight
  - Sort by end time
  - dp[i] = max weight using intervals up to i
  - Transition: include i or exclude i
  - Find previous compatible interval via binary search
  - Time: O(n log n)

- **Distinct Subsequences**
  - Count number of distinct subsequences
  - State: dp[i][j] = distinct subsequences of str1[0..i] using str2[0..j]

- **Delete Minimum Characters to Make Palindrome**
  - Make string palindrome by deleting minimum characters
  - Related to LCS with reverse string

---

### 📅 DAY 5 (OPTIONAL): STORY-DRIVEN DP | 90 min

**Topics:**
- **Problem Interpretation**
  - Complex DP problems often have story/real-world context
  - Key: translate story into state and transitions
  - Example: text justification, blackjack, etc.

- **Text Justification**
  - Format text with width constraint
  - Minimize "badness" (wasted space penalty)
  - DP: dp[i] = min badness for words 0..i
  - Transition: try all ending positions for current line

- **State Design Principles**
  - State should capture: what matters for future decisions?
  - Example: in activity scheduling, only finish time matters
  - Example: in coin change, only remaining amount matters
  - Careful: too specific → huge state space; too general → can't recover answer

---

## 📌 WEEK 11: DYNAMIC PROGRAMMING II - TREES, DAGS & ADVANCED

### Weekly Goal
Extend DP to more complex structures. Master tree DP, DAG DP, bitmask DP, and advanced patterns.

### Weekly Outcomes
- Solve tree DP problems (diameter, maximum independent set)
- Apply DP to DAGs (longest path)
- Solve bitmask DP problems (TSP, subset selection)
- Optimize DP using state compression

### Summary
DP is incredibly powerful. This week extends basic patterns to more complex structures. Students learn when to apply DP, how to design states for different problems, and when to combine DP with other techniques.

---

### 📅 DAY 1: DP ON TREES | 120 min

**Topics:**
- **Tree DP Framework**
  - Solve for each subtree, combine into parent
  - Post-order traversal (solve children before parent)
  - State: what's the answer for this subtree?

- **Maximum Independent Set**
  - Select nodes with no edges between them
  - Maximize value of selected nodes
  - dp[node][0] = max value excluding node
  - dp[node][1] = max value including node
  - Transition: combine children's answers

- **Tree Diameter via DP**
  - Longest path in tree
  - dp[node] = max distance down from node
  - Answer: max(left_depth + right_depth + 1)
  - Time: O(n)

- **Subtree Computation**
  - Simple: sum all nodes in subtree
  - Post-order: solve children, add to node

- **Tree Coloring (K Colors)**
  - Color nodes with K colors, adjacent different
  - dp[node][color] = count of ways
  - Transition: children must use different colors

- **Tree Rerooting (Change Root)**
  - Compute answer when root changes
  - First pass: DP from original root
  - Second pass: reroot and combine

- **Practical Considerations**
  - Space: often work with adjacency list (no explicit table)
  - Time: often O(n) if each node processed once
  - Recursion depth: can hit stack limits for very deep trees

---

### 📅 DAY 2: DP ON DAGS | 120 min

**Topics:**
- **DAG (Directed Acyclic Graph)**
  - Directed graph with no cycles
  - Enables topological ordering
  - DP naturally applies to DAG

- **Longest Path in DAG**
  - No cycles → DP works
  - Topological sort order → compute in order
  - dp[node] = longest path starting at node
  - Transition: max(1 + dp[neighbor] for all neighbors)
  - Time: O(V + E)

- **Shortest Path in DAG with Negative Weights**
  - Bellman-Ford O(VE) needed for general graphs
  - DAG allows O(V + E) via topo sort
  - Process nodes in topo order, relax edges

- **Vertex Weight Sum Paths**
  - Path value = sum of vertex weights (not edge weights)
  - Similar DP but sum vertices instead of edges

- **All Paths in DAG**
  - Count or enumerate paths from s to t
  - DP: dp[node] = number of paths from node to t
  - Transition: sum paths through neighbors

- **Topo Sort Ordering**
  - Critical: must process nodes where all predecessors processed
  - Failure to respect order → wrong answer

- **Longest Path in DAG Applications**
  - Project scheduling: find critical path
  - Game theory: minimax with DAG game state
  - Dependency resolution: find longest dependency chain

---

### 📅 DAY 3: BITMASK & SUBSET DP | 120 min

**Topics:**
- **Bitmask Representation**
  - Set of n elements → bitmask of 2^n states
  - Bit i = 1 if element i in subset
  - Iterate over subsets: 0 to 2^n - 1

- **Subset Enumeration**
  - For each mask: iterate elements to see which included
  - Iterate submasks: clever bit manipulation

- **TSP with DP (Traveling Salesman)**
  - Visit n cities exactly once, minimum cost
  - Naive: O(n!) permutations
  - DP: state = (visited_cities, current_city)
  - dp[mask][last] = min cost visiting cities in mask, ending at last
  - Transition: visit next unvisited city
  - Time: O(2^n × n²)

- **Subset Sum Problems**
  - Count/find subsets with specific sum
  - dp[i] = ways to make sum i
  - Transition: include/exclude elements

- **Maximum Weight Independent Set (Small Graph)**
  - Can use bitmask if vertices few (≤ 20)
  - dp[mask] = max weight with vertices in mask, no edges between
  - Enumerate all masks, check if valid independent set

- **Complexity Analysis**
  - Time: O(2^n × f(n)) where f(n) is work per state
  - Space: O(2^n) for DP table
  - Feasible: n ≤ 20, marginal for n ≤ 25

- **Bitmask Optimization Techniques**
  - Precompute which masks are valid (no edges)
  - Use bit manipulation tricks (popcount, lowbit, etc.)
  - Prune search space early

---

### 📅 DAY 4 (OPTIONAL): STATE COMPRESSION & OPTIMIZATIONS | 90 min

**Topics:**
- **Space Optimization**
  - Sliding window: keep only current and previous states
  - Example: 2D → 1D if only need previous row
  - Saves from O(m × n) to O(n)

- **Dimension Reduction**
  - 3D DP → 2D by observing what matters
  - Example: coin change with denominations → doesn't need 3D

- **Memoization vs Tabulation Trade-off**
  - Memoization: compute only needed states
  - Tabulation: compute all, more cache-friendly
  - Choose based on state space sparsity

- **Pruning in DP**
  - Skip invalid states
  - Early termination if answer found
  - Example: if current cost exceeds best known, prune

---

### 📅 DAY 5 (OPTIONAL): MIXED DP PROBLEMS | 90 min

**Topics:**
- **Multi-Concept Problems**
  - Combining DP with other algorithms
  - Example: DP on graph shortest paths
  - Example: DP with greedy choices

- **Recognition & Intuition**
  - Overlapping subproblems → DP candidate
  - Optimal substructure → DP candidate
  - Practice: solve many problems builds intuition

- **Problem Solving Strategy**
  - 1. Define state clearly
  - 2. Identify transitions
  - 3. Determine base cases
  - 4. Implement (bottom-up preferred for efficiency)
  - 5. Optimize space if needed

---

# 🟧 PHASE D: ALGORITHM PARADIGMS
## Weeks 12-13 | 25-30 hours | Greedy, Backtracking, B&B, Analysis

### Phase Goals & Outcomes

**Goals:** Master different algorithm design paradigms and learn when to apply each.

**Learning Outcomes:**
- Design greedy algorithms and prove correctness
- Solve backtracking problems systematically
- Apply branch & bound for optimization
- Analyze amortized complexity
- Choose paradigm appropriate for problem

---

## 📌 WEEK 12: GREEDY ALGORITHMS & PROOFS

### Weekly Goal
Learn greedy algorithm design and when greedy is guaranteed optimal.

### Weekly Topics
- Greedy algorithm template and correctness proofs
- Activity selection and interval problems
- Huffman coding and optimal prefix trees
- Fractional knapsack and scheduling
- Matroid theory (optional)

---

### 📅 DAY 1: GREEDY FUNDAMENTALS | 90 min

**Topics:**
- **Greedy Algorithm Concept**
  - Make locally optimal choice at each step
  - Hope it leads to globally optimal solution
  - NOT always optimal (must prove for each problem)

- **When Greedy Works**
  - Problem has optimal substructure
  - Greedy choice property: greedy choice → optimal solution
  - No need to solve subproblems before choosing

- **Greedy Algorithm Template**
  - Sort by some criterion
  - Iterate and greedily select
  - Verify correctness with exchange argument or induction

- **Exchange Argument**
  - Start with optimal solution
  - Replace one element with greedy choice
  - Show result is at least as good
  - Recursively replace all

- **Correctness Proof Strategy**
  - Show problem has greedy choice property
  - Show problem has optimal substructure
  - Prove greedy leaves subproblems of same type
  - Induction completes proof

---

### 📅 DAY 2: ACTIVITY SELECTION & INTERVAL PROBLEMS | 90 min

**Topics:**
- **Activity Selection**
  - Given activities with start/end times
  - Select maximum number of non-overlapping activities
  - Greedy: sort by finish time, greedily select
  - Exchange argument: if swap, still get same or fewer activities

- **Interval Scheduling Variations**
  - Maximize number of activities: greedy by finish time
  - Maximize weight: different (DP better)
  - Minimize rooms needed: sweep line algorithm

- **Greedy Stays Ahead**
  - Greedy solution finishes before/at optimal
  - So greedy can always accommodate what optimal accommodates
  - Plus potentially more

---

### 📅 DAY 3: HUFFMAN CODING & OPTIMAL TREES | 90 min

**Topics:**
- **Huffman Coding**
  - Optimal prefix code for character frequencies
  - Greedy: combine two smallest-frequency nodes
  - Build bottom-up tree
  - Assign 0/1 to branches

- **Correctness of Huffman**
  - Exchange argument: swapping subtrees doesn't improve
  - Greedy choice (smallest frequencies) is always safe

---

### 📅 DAY 4: FRACTIONAL KNAPSACK & SCHEDULING | 90 min

**Topics:**
- **Fractional Knapsack**
  - Can take fractions of items
  - Greedy: sort by value/weight ratio
  - Take items in order by ratio
  - Time: O(n log n)

- **Difference from 0/1 Knapsack**
  - 0/1: must take entire item (DP needed)
  - Fractional: can take partial (greedy works)

- **Job Sequencing with Deadlines**
  - Jobs have deadlines and profits
  - Schedule to maximize profit
  - Greedy: sort by profit descending
  - Place each job at latest available slot before deadline

---

### 📅 DAY 5 (OPTIONAL): GREEDY IN SYSTEMS | 90 min

**Topics:**
- **Greedy in Networks**
  - Minimum spanning tree: Kruskal and Prim are greedy
  - Routing protocols: greedy hops

- **Cache Replacement**
  - LRU cache: evict least recently used (greedy temporal heuristic)
  - Not always optimal but good in practice

- **Approximation Algorithms**
  - When optimal is NP-hard, greedy gives approximation
  - Example: set cover (O(log n) approximation)

---

## 📌 WEEK 13: BACKTRACKING & BRANCH & BOUND

### Weekly Goal
Master backtracking for combinatorial problems and branch & bound for optimization.

### Weekly Topics
- Backtracking template and pruning
- N-queens, sudoku, permutations
- Branch & bound for optimization
- Amortized analysis

---

### 📅 DAY 1: BACKTRACKING FUNDAMENTALS | 90 min

**Topics:**
- **Backtracking Concept**
  - Build solution incrementally
  - At each step: try all valid choices
  - If choice doesn't lead to solution: backtrack
  - Equivalent to DFS on solution tree

- **Backtracking Template**
  - State: current partial solution
  - Choices: next decisions to try
  - Constraints: which choices valid
  - DFS: recursively explore
  - Prune: skip invalid branches

- **State Space Tree**
  - Root: empty solution
  - Internal node: partial solution
  - Leaf: complete solution or pruned node
  - DFS traversal explores tree

---

### 📅 DAY 2: BACKTRACKING PROBLEMS | 120 min

**Topics:**
- **N-Queens Problem**
  - Place n queens on n×n board, no attacks
  - DFS with pruning: place queens column by column
  - Check diagonals and rows

- **Sudoku Solver**
  - Fill grid with digits 1-9
  - Constraints: rows, columns, 3×3 boxes
  - Backtracking: try digits, backtrack if conflict

- **Permutations & Combinations**
  - Generate all permutations: backtracking
  - Generate all combinations: backtracking with skip

- **Word Search**
  - Find word in grid (adjacent cells)
  - DFS with backtracking
  - Mark visited to avoid cycles

- **Maze Solving**
  - Find path from start to exit
  - DFS with backtracking
  - Mark visited cells

---

### 📅 DAY 3: BRANCH & BOUND | 120 min

**Topics:**
- **Branch & Bound Concept**
  - Systematic search for optimization
  - Branch: explore sub-problem spaces
  - Bound: compute upper/lower bounds
  - Prune: skip branches that can't improve best

- **Best-First Search**
  - Priority queue ordered by bound
  - Process most promising nodes first
  - Often finds good solution early

- **TSP with Branch & Bound**
  - Lower bound: minimum spanning tree
  - Branch: expand tour by adding cities
  - Prune: if partial tour + bound > best, skip

- **Knapsack with Branch & Bound**
  - Upper bound: fractional knapsack value
  - Branch: include/exclude items
  - Prune: if partial + bound ≤ best, skip

---

### 📅 DAY 4: AMORTIZED ANALYSIS | 120 min

**Topics:**
- **Amortized Complexity Concept**
  - Average cost over sequence of operations
  - Some operations expensive, many cheap
  - Amortized = total cost / operations

- **Aggregate Analysis**
  - Total cost for n operations
  - Divide by n for amortized cost

- **Accounting Method**
  - Assign cost to operations
  - "Bank account" of future work
  - Show account never goes negative

- **Potential Method**
  - Define potential function on data structure
  - Amortized cost = actual + change in potential
  - Sum amortized costs bounds total actual

- **Dynamic Arrays**
  - Doubling strategy: O(1) amortized append
  - Prove using accounting or potential

- **Self-Adjusting Structures**
  - Splay trees: O(log n) amortized
  - Fibonacci heaps: complex amortized bounds

---

### 📅 DAY 5 (OPTIONAL): MIXED PARADIGM PROBLEMS | 90 min

**Topics:**
- **Combining Paradigms**
  - Backtracking with pruning (greedy heuristics)
  - Branch & bound with greedy estimates
  - DP for subproblem optimization

---

# 🟪 PHASE E: INTEGRATION & EXTENSIONS
## Weeks 14-15 | 25-30 hours | Specialized Techniques

### Phase Goals & Outcomes

**Goals:** Master specialized techniques and integrate learned concepts.

**Learning Outcomes:**
- Handle matrix and 2D array problems efficiently
- Solve bitwise operation problems
- Apply number theory (GCD, primes, modular arithmetic)
- Solve advanced string problems
- Understand network flow basics

---

## 📌 WEEK 14: MATRICES, BITMASKS & NUMBER THEORY

### Weekly Goal
Master specialized problem domains: matrices, bitwise operations, and number theory.

### Weekly Topics
- Matrix operations and patterns
- Bitwise operations and tricks
- Number theory: GCD, primes, modular arithmetic
- Applications in competitive programming

---

### 📅 DAY 1: MATRIX OPERATIONS | 90 min

**Topics:**
- **Matrix Rotation (90 degrees)**
  - Clockwise: transpose then reverse each row
  - Counter-clockwise: reverse each row then transpose

- **Matrix Transpose**
  - Swap elements at [i][j] and [j][i]

- **Spiral Traversal**
  - Layer by layer from outside in
  - Track boundaries: top, bottom, left, right

- **Matrix Multiplication**
  - DP for optimal order of matrix products
  - Strassen algorithm for faster multiplication

- **Determinant & Inverse**
  - Gaussian elimination
  - Applications: solving systems of equations

---

### 📅 DAY 2: BITWISE OPERATIONS | 120 min

**Topics:**
- **Basic Operations**
  - AND, OR, XOR, NOT
  - Bit shifts: left (multiply by 2), right (divide by 2)

- **Common Tricks**
  - Check if power of 2: n & (n-1) == 0
  - Check i-th bit: (n >> i) & 1
  - Set i-th bit: n |= (1 << i)
  - Clear i-th bit: n &= ~(1 << i)
  - Toggle i-th bit: n ^= (1 << i)
  - Lowest set bit: n & -n

- **Subset Enumeration**
  - For each submask: mask = (mask - 1) & original_mask

- **Gray Code**
  - Binary representation with single bit difference
  - Gray = n ^ (n >> 1)

- **Popcount (Count Bits)**
  - Number of 1s in binary representation
  - O(log n) or O(1) with instruction

---

### 📅 DAY 3: NUMBER THEORY BASICS | 90 min

**Topics:**
- **GCD & LCM**
  - Euclidean algorithm: O(log min(a,b))
  - LCM = a×b / GCD(a,b)

- **Prime Numbers**
  - Sieve of Eratosthenes: find all primes up to n in O(n log log n)
  - Prime checking: trial division up to sqrt(n)

- **Modular Arithmetic**
  - (a + b) mod m = ((a mod m) + (b mod m)) mod m
  - (a × b) mod m = ((a mod m) × (b mod m)) mod m
  - Modular inverse: find x such that (a × x) ≡ 1 (mod m)

- **Fermat's Little Theorem**
  - If p prime and gcd(a,p)=1, then a^(p-1) ≡ 1 (mod p)
  - Application: modular exponentiation

- **Applications**
  - Hashing (mod large prime)
  - Random number generation
  - Cryptography

---

### 📅 DAY 4: ADVANCED STRING PROBLEMS | 120 min

**Topics:**
- **KMP (Knuth-Morris-Pratt)**
  - Pattern matching in O(n + m)
  - Build failure function
  - Applications: string matching, period detection

- **Suffix Arrays & Trees**
  - Build in O(n log^2 n) or O(n) with DC3
  - Answer range min queries for LCP
  - Applications: longest repeated substring, multiple pattern search

- **Trie (Prefix Tree)**
  - Build in O(n × m) where m = word length
  - Auto-complete, spell checking
  - Compressed trie (radix tree) for space efficiency

- **Aho-Corasick Algorithm**
  - Multi-pattern matching
  - Combine AC automaton with failure functions
  - Time: O(n + m + z) where z = matches

---

### 📅 DAY 5 (OPTIONAL): NUMBER THEORY DEPTH & ADVANCED | 90 min

**Topics:**
- **Euler's Totient Function**
  - Count numbers coprime to n
  - φ(n) = n × ∏(1 - 1/p) for all prime p dividing n

- **Chinese Remainder Theorem**
  - Solve system of congruences
  - Applications: combining results modulo different primes

---

## 📌 WEEK 15: ADVANCED STRINGS & NETWORK FLOW

### Weekly Goal
Master advanced string algorithms and introduction to network flow.

### Weekly Topics
- Z-algorithm and string matching
- Segment trees and range queries
- Network flow basics and algorithms
- Real-world applications

---

### 📅 DAY 1: Z-ALGORITHM & ADVANCED STRING MATCHING | 120 min

**Topics:**
- **Z-Algorithm**
  - Compute Z-array: longest substring matching prefix
  - O(n) string matching
  - Find all pattern occurrences in O(n + m)

- **Z-Array Construction**
  - Use "window" of known matches
  - Avoid recomputing matches already known

- **Applications**
  - String matching
  - Periodic string detection
  - Longest repeating substring

- **Comparison with KMP**
  - Z-algorithm: forward direction
  - KMP: failure function approach
  - Both O(n) but different philosophy

---

### 📅 DAY 2: SEGMENT TREES & RANGE QUERIES | 120 min

**Topics:**
- **Segment Tree Concept**
  - Binary tree for range queries
  - Build in O(n)
  - Query/update in O(log n)

- **Range Sum Query**
  - Query: sum of range [L, R] in O(log n)
  - Update: change element in O(log n)

- **Range Minimum Query**
  - Similar structure for min/max

- **Lazy Propagation**
  - Range updates in O(log n)
  - Defer updates until needed
  - Complex but powerful

- **Fenwick Tree (Binary Indexed Tree)**
  - Alternative to segment tree
  - Simpler implementation
  - Same O(log n) operations

---

### 📅 DAY 3: NETWORK FLOW BASICS | 120 min

**Topics:**
- **Flow Network Concept**
  - Directed graph with capacities
  - Source and sink nodes
  - Conservation of flow (flow in = flow out)

- **Maximum Flow Problem**
  - Find maximum total flow from source to sink
  - Subject to capacity constraints

- **Ford-Fulkerson Algorithm**
  - Augmenting path: path with available capacity
  - While augmenting path exists: send flow, update residual capacities
  - Time: O(E × |maximum flow|) - can be slow

- **Edmonds-Karp Algorithm**
  - Use BFS for augmenting path (shortest path)
  - Time: O(V × E²)

- **Dinic's Algorithm**
  - Level graph and blocking flow
  - Time: O(V² × E)

- **Min-Cost Max-Flow**
  - Find flow that minimizes cost
  - Applications: transportation, scheduling

---

### 📅 DAY 4: NETWORK FLOW APPLICATIONS | 90 min

**Topics:**
- **Bipartite Matching**
  - Maximum matching in bipartite graph
  - Reduce to max flow (source → left nodes → right nodes → sink)

- **Assignment Problem**
  - Assign workers to jobs optimally
  - Min-cost max-flow

- **Circulation Problems**
  - Flow with lower and upper bounds on edges

---

### 📅 DAY 5 (OPTIONAL): SPECIAL TOPICS | 90 min

**Topics:**
- **Geometry Algorithms**
  - Convex hull
  - Line intersection
  - Point in polygon

- **Randomized Algorithms**
  - Quicksort with random pivot
  - Monte Carlo vs Las Vegas
  - Fingerprinting

---

# 🟫 PHASE F: ADVANCED DEEP DIVES (Optional)
## Weeks 16-18 | 35-40 hours | Competitive Programming

### Phase Goals

**Goals:** Deep mastery in competitive programming and advanced algorithms.

**Learning Outcomes:**
- Master advanced data structures (skip lists, treaps, link-cut trees)
- Solve geometry and computational problems
- Advanced DP patterns
- Competitive programming techniques

---

## 📌 WEEK 16: ADVANCED DATA STRUCTURES

### Weekly Goal
Study data structures beyond standard libraries.

### Weekly Topics
- Skip lists and treaps
- Link-cut trees for dynamic trees
- Persistent data structures
- Cache-oblivious algorithms

---

### 📅 DAY 1: SKIP LISTS & TREAPS | 120 min

**Topics:**
- **Skip Lists**
  - Probabilistic balanced search structure
  - O(log n) expected time for all operations
  - Simpler than red-black trees
  - Good cache locality

- **Treap (Tree + Heap)**
  - Binary search tree ordered by key
  - Heap property on random priority
  - Maintains balance via randomization
  - Rotation-based operations

- **Comparison with RB-Trees**
  - Simpler randomized structures
  - Competitive performance
  - Some problems prefer one or other

---

### 📅 DAY 2: LINK-CUT TREES | 120 min

**Topics:**
- **Dynamic Tree Problem**
  - Trees that change structure
  - Queries on tree paths
  - Link and cut operations

- **Splay Trees in Link-Cut Trees**
  - Use splay tree internally
  - O(log n) amortized for link/cut/query

- **Applications**
  - Network dynamic connectivity
  - Graph connectivity queries

---

### 📅 DAY 3: PERSISTENT DATA STRUCTURES | 90 min

**Topics:**
- **Persistent vs Ephemeral**
  - Ephemeral: old version lost on update
  - Persistent: old versions preserved

- **Path Copying**
  - Copy affected nodes on update
  - O(log n) space per update
  - O(log n) time per query

- **Persistent Segment Trees**
  - Query over any historical version in O(log n)

---

### 📅 DAY 4-5: ADVANCED TOPICS | 180 min

**Topics:**
- **Cache-Oblivious Algorithms**
  - Design without knowing cache parameters
  - Still achieve optimal cache behavior
  - B-trees, transpose algorithms

- **Randomized Data Structures**
  - Hash tables with good worst-case
  - Cuckoo hashing
  - Perfect hashing

---

## 📌 WEEK 17: ADVANCED ALGORITHMS & DP

### Weekly Goal
Master advanced algorithmic techniques used in competitive programming.

### Weekly Topics
- Advanced DP (convex hull trick, slope optimization)
- Game theory and nimbers
- Combinatorics and counting

---

### 📅 DAY 1: CONVEX HULL TRICK & CHT | 120 min

**Topics:**
- **Linear DP Optimization**
  - Some DP recurrences have special structure
  - Naive: O(n²) for computing dp[i] = min(dp[j] + a[i]*b[j])

- **Convex Hull Trick**
  - Geometric interpretation: lines and queries
  - Monotone queries: CHT in O(n)
  - General: CHT with persistent structure

- **Applications**
  - Knapsack optimizations
  - Distance problems
  - Minimize cost problems

---

### 📅 DAY 2: SLOPE TRICK | 120 min

**Topics:**
- **Slope Trick Concept**
  - Piecewise linear function minimization
  - Maintain slope changes
  - Efficient DP on weighted trees

---

### 📅 DAY 3: GAME THEORY & NIMBERS | 90 min

**Topics:**
- **Impartial Games**
  - Nim: take stones, last player wins
  - Nim-value (Grundy number) characterizes position
  - XOR of Grundy numbers for multiple games

- **Nimber Arithmetic**
  - Compute Grundy numbers for game states
  - Strategy: move to losing position for opponent

---

### 📅 DAY 4-5: COMBINATORICS & COUNTING | 180 min

**Topics:**
- **Counting Techniques**
  - Inclusion-exclusion
  - Burnside's lemma for symmetries
  - Stars and bars

- **Catalan Numbers**
  - Appear in many combinatorial problems
  - C(n) = C(2n, n) / (n+1)
  - Applications: binary trees, parentheses, paths

---

## 📌 WEEK 18: COMPETITIVE PROGRAMMING DEEP DIVE

### Weekly Goal
Master techniques and patterns from competitive programming.

### Weekly Topics
- Meet-in-the-middle
- Heavy-light decomposition
- Square root decomposition
- Additional advanced patterns

---

### 📅 DAY 1: MEET-IN-THE-MIDDLE | 120 min

**Topics:**
- **Concept**
  - Split problem in half
  - Solve each half
  - Combine results efficiently

- **Subset Sum in O(2^(n/2))**
  - Split n elements into two halves
  - Generate all subset sums for each half
  - Meet in middle to find target sum

- **Applications**
  - Subset matching problems
  - 4-sum and related problems (with hashing)

---

### 📅 DAY 2: SQUARE ROOT DECOMPOSITION | 120 min

**Topics:**
- **Sqrt Decomposition**
  - Divide array into sqrt(n) blocks
  - Precompute aggregate for each block
  - Query: combine full blocks and partial edges

- **Range Updates**
  - Mark block as needing update
  - Lazy update on individual elements as needed

---

### 📅 DAY 3: HEAVY-LIGHT DECOMPOSITION | 90 min

**Topics:**
- **Tree Decomposition**
  - Decompose tree into paths
  - Handle queries/updates on tree paths efficiently

- **Applications**
  - Path queries in trees
  - Tree DP on paths

---

### 📅 DAY 4-5: FINAL COMPETITIVE TECHNIQUES | 180 min

**Topics:**
- **Two Pointers on Multiple Dimensions**
- **Monotone Deque Optimization**
- **Math-Based Optimizations**
- **Specialized Data Structures for Specific Problems**

---

# 🔴 PHASE G: MOCK INTERVIEWS & FINAL REVIEW
## Week 19 | 10-12 hours | Interview Preparation

### Phase Goals

**Goals:** Prepare for technical interviews with realistic practice and assessment.

**Learning Outcomes:**
- Perform well under interview conditions
- Communicate solutions clearly
- Handle edge cases and debug
- Improve weak areas

---

## 📌 WEEK 19: MOCK INTERVIEWS & FINAL REVIEW

### Weekly Goal
Conduct mock interviews and identify remaining weak areas.

### Weekly Topics
- Mock interview sessions
- Weak area diagnosis and targeted practice
- Meta-skills (communication, thinking aloud)
- Final assessment

---

### 📅 DAY 1: MOCK INTERVIEW SESSION A - ARRAYS & STRINGS

**Duration:** 120 minutes

**Problems:**
1. Two-pointer or sliding window variant
   - Example: "Trapping Rain Water II" or "Substring with K Distinct"
   - Tests pattern recognition and implementation

2. Sorting/searching optimization
   - Example: "Merge K Sorted Lists" or "Search in Rotated Sorted Array II"
   - Tests algorithm choice and complexity analysis

3. String manipulation
   - Example: "Valid Parentheses" or "Regular Expression Matching"
   - Tests pattern matching and state machines

**Interview Format:**
- Read and clarify (5 min)
- Discuss approach (5 min)
- Implement (40 min)
- Test and debug (20 min)
- Optimize (20 min)
- Discuss alternatives (10 min)

**Key Skills Tested:**
- Pattern recognition
- Complexity analysis
- Clean implementation
- Communication

---

### 📅 DAY 2: MOCK INTERVIEW SESSION B - TREES & GRAPHS

**Duration:** 120 minutes

**Problems:**
1. Tree DP + traversal
   - Example: "Diameter of Binary Tree" or "Maximum Path Sum"
   - Combines DFS, DP state, aggregate computation

2. Graph algorithm choice
   - Example: "Number of Islands II" with Union-Find
   - Combines dynamic connectivity, coordinate system

3. BFS/DFS application
   - Example: "Minimum Knight Moves" or "Word Ladder"
   - Combines BFS, state representation, shortest path

---

### 📅 DAY 3: MOCK INTERVIEW SESSION C - DYNAMIC PROGRAMMING & OPTIMIZATION

**Duration:** 120 minutes

**Problems:**
1. DP + greedy choice
   - Example: "Jump Game II" (greedy within DP context)
   - Combines DP formulation, greedy optimization insight

2. DP on graph/grid
   - Example: "Cherry Pickup" or "Maximum Path Sum in Matrix"
   - Combines grid traversal, DP state design, constraint handling

3. DP with constraints
   - Example: "Burst Balloons" or "Remove Boxes"
   - Combines DP decomposition, interval DP, state definition

---

### 📅 DAY 4: MIXED PROBLEM SOLVING

**Duration:** 90 minutes

**Problems:**
1. Combining multiple patterns
   - Example: "Median of Two Sorted Arrays"
   - Combines binary search, array handling, edge cases

2. System design / algorithmic
   - Example: "Design LRU Cache"
   - Combines data structure choice, efficiency requirements, implementation

---

### 📅 DAY 5: FINAL ASSESSMENT & WEAK AREA DIAGNOSIS

**Duration:** 90 minutes

**Activities:**
1. Review of previous mock sessions
   - Identify patterns of mistakes
   - Categorize: conceptual gaps, implementation bugs, time pressure

2. Targeted practice
   - Focus on weak areas identified
   - 2-3 problems in specific category
   - Build confidence

3. Personal review plan
   - Map remaining weak spots to weeks/topics
   - Design targeted practice schedule
   - Set goals for continued improvement

4. Meta-skills practice
   - Asking clarifying questions
   - Thinking aloud effectively
   - Handling "I don't know"
   - Graceful pivot on wrong approach

---

### 📅 DAY 6 (OPTIONAL): INTERVIEW SPECIFIC TIPS & FINAL DRILLS

**Duration:** 90 minutes

**Topics:**
- Handling unknowns gracefully
- Time management in interviews
- Choosing between multiple approaches
- Communicating trade-offs
- Real interview walkthrough

---

## 🎓 CURRICULUM COMPLETE

**Total Content:**
- 19 weeks
- 95+ study days
- 500+ unique topic subtopics
- 235-270 hours of learning
- 20+ data structures
- 15+ algorithm paradigms
- 50+ techniques

**You're Now Ready For:**
- Technical interviews at top companies
- Systems design discussions
- Algorithm competition
- Real-world problem-solving
- Advanced CS courses

**Next Steps:**
1. Complete all daily deliverables
2. Practice problems on LeetCode/HackerRank
3. Review weak areas
4. Do mock interviews
5. Continuous practice and refinement

---

**Version:** 13.0 COMPLETE  
**Status:** ✅ ALL 19 WEEKS FULLY DETAILED  
**Date:** January 25, 2026

**This is your comprehensive roadmap to DSA mastery. Every day brings you closer. Keep showing up. 🚀**

