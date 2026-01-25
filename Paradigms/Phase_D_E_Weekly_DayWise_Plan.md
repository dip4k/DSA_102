# 📅 PHASE D & E: WEEKLY & DAYWISE DETAILED LEARNING PLAN v13

**Duration:** 4 weeks (Weeks 12-15)  
**Format:** Optimized daily topics with learning outcomes  
**Status:** ✅ Ready for implementation

---

# 🟧 PHASE D: WEEKS 12-13 (Algorithm Paradigms)

---

## 📌 WEEK 12: GREEDY ALGORITHMS & PROOF TECHNIQUES

### **Week Goal:** Master greedy paradigm and exchange argument proofs

| Day | Topic | Duration | Key Concepts | Outcomes | Difficulty |
|-----|-------|----------|-------------|----------|-----------|
| **Day 1** | Greedy Paradigm Foundations | 90 min | Greedy choice property, optimal substructure, problem classification | Distinguish greedy from DP, identify greedy-suitable problems | ⭐⭐ |
| **Day 2** | Interval Scheduling (Canonical Example) | 120 min | Earliest finish strategy, exchange argument proof, weighted variant failure | Prove greedy algorithm correct, identify greedy failure patterns | ⭐⭐⭐ |
| **Day 3** | Minimum Spanning Trees | 120 min | Kruskal's algorithm, Prim's algorithm, cut property justification | Compare graph greedy algorithms, understand theoretical foundation | ⭐⭐⭐ |
| **Day 4** | Huffman Coding & Greedy on Trees | 90 min | Optimal prefix codes, tree construction via greedy merging, information theory | Prove Huffman optimal, design greedy on structures | ⭐⭐⭐ |
| **Day 5** | When Greedy Fails & Decision Framework | 120 min | Counterexample patterns, greedy vs DP comparison, paradigm selection | Recognize greedy failure, choose correct paradigm | ⭐⭐⭐⭐ |

---

## 📌 WEEK 13: BACKTRACKING, BRANCH & BOUND & AMORTIZED ANALYSIS

### **Week Goal:** Master exploration paradigms and formal cost analysis

| Day | Topic | Duration | Key Concepts | Outcomes | Difficulty |
|-----|-------|----------|-------------|----------|-----------|
| **Day 1** | Backtracking Foundations | 90 min | State space trees, constraint-driven pruning, DFS exploration | Design backtracking algorithms, implement pruning | ⭐⭐ |
| **Day 2** | Backtracking Applications | 120 min | N-Queens, Sudoku solver, Boggle, graph coloring, heuristics (MRV, forward checking) | Implement real backtracking with heuristics, optimize pruning | ⭐⭐⭐ |
| **Day 3** | Branch and Bound Paradigm | 120 min | Bounding functions, node selection strategies (FIFO/LIFO/best-first), relaxation bounds | Design B&B algorithms, understand optimization vs feasibility | ⭐⭐⭐ |
| **Day 4** | Branch and Bound Applications | 120 min | 0/1 Knapsack with greedy bound, TSP with MST bound, assignment problem | Implement B&B for optimization, design tight bounds | ⭐⭐⭐⭐ |
| **Day 5** | Algorithm Selection Framework | 120 min | Paradigm comparison matrix, problem-to-algorithm mapping, decision flowchart | Master paradigm selection, solve using correct approach | ⭐⭐⭐⭐ |
| **Day 6** | Amortized Analysis (Formal Methods) | 120 min | Aggregate method, accounting method, potential method | Prove amortized bounds rigorously for data structures | ⭐⭐⭐⭐ |

---

# 🟪 PHASE E: WEEKS 14-15 (Integration & Extensions)

---

## 📌 WEEK 14: MATRIX ALGORITHMS, BACKTRACKING & INTEGRATION

### **Week Goal:** Apply paradigms to specific problem types (matrices, combinatorics)

| Day | Topic | Duration | Key Concepts | Outcomes | Difficulty |
|-----|-------|----------|-------------|----------|-----------|
| **Day 1** | Matrix Algorithms | 90 min | Standard traversals (row/spiral/diagonal), matrix search (staircase), transformations (rotation) | Solve matrix problems efficiently, use space-aware algorithms | ⭐⭐ |
| **Day 2** | Backtracking on Grids | 120 min | Word search/Boggle DFS, Sudoku with constraint propagation, path finding | Apply backtracking to grid CSPs, optimize with propagation | ⭐⭐⭐ |
| **Day 3** | Bitmask Tricks & Bitmask DP | 120 min | Bit operations, subset representation, bitmask DP (TSP example) | Use bitmask for subsets, solve TSP in O(2^n × n²) | ⭐⭐⭐ |
| **Day 4** | Number Theory & Modular Arithmetic | 90 min | GCD (Euclid), LCM, fast exponentiation, modular inverse | Implement number algorithms, avoid overflow with modulo | ⭐⭐ |
| **Day 5** | Probability & Sampling | 90 min | Expected value, linearity of expectation, reservoir sampling (uniform/weighted) | Use probabilistic algorithms, sample from streams | ⭐⭐⭐ |

---

## 📌 WEEK 15: ADVANCED STRINGS & NETWORK FLOW

### **Week Goal:** Master string pattern matching and network optimization

| Day | Topic | Duration | Key Concepts | Outcomes | Difficulty |
|-----|-------|----------|-------------|----------|-----------|
| **Day 1** | KMP String Matching | 120 min | Failure function (LPS array), linear-time pattern matching, KMP algorithm | Implement KMP, explain why O(n+m) is optimal | ⭐⭐⭐ |
| **Day 2** | Z-Algorithm & String Analysis | 120 min | Z-function computation, pattern matching via Z, periodicity/rotation detection | Implement Z-algorithm, apply to string problems | ⭐⭐⭐ |
| **Day 3** | Manacher's Algorithm (Optional) | 90 min | Longest palindromic substring in O(n), center/radius expansion, symmetry exploitation | Implement Manacher, understand time optimization via structure | ⭐⭐⭐⭐ |
| **Day 4** | Suffix Structures (Optional) | 90 min | Suffix arrays (sorted indices), suffix trees (compressed trie), applications | Understand suffix structures, know when to use | ⭐⭐⭐⭐ |
| **Day 5** | Network Flow: Max-Flow Min-Cut | 120 min | Flow networks, augmenting paths, Ford-Fulkerson, Edmonds-Karp, max-flow min-cut theorem | Implement max-flow algorithm, understand theoretical guarantees | ⭐⭐⭐⭐ |
| **Day 6** | Matching & Assignment via Flow | 120 min | Bipartite matching as max-flow, assignment problem as min-cost max-flow, reductions | Reduce matching problems to flow, solve via algorithms | ⭐⭐⭐⭐ |

---

---

# 📚 DETAILED DAILY BREAKDOWNS

## WEEK 12, DAY 1: GREEDY PARADIGM FOUNDATIONS

**Time:** 90 minutes

### Learning Sequence:
1. **Define Greedy Choice Property** (15 min)
   - Local best choice in some optimal solution
   - Visual: Why earliest finish leaves room for future
   - Counterexample: Why earliest start doesn't work

2. **Define Optimal Substructure** (15 min)
   - Optimal solution from optimal sub-solutions
   - Contrast with DP (both need this, but different subproblems)
   - Example: MST's cut property

3. **Problem Classification** (15 min)
   - Optimization vs decision vs enumeration
   - Which problems have greedy choice property
   - Checklist: Is this problem "greedy-like"?

4. **Greedy Paradigm Template** (15 min)
   - Generic pseudocode
   - Compare to DP pseudocode
   - Compare to backtracking pseudocode

5. **Real-World Connections** (15 min)
   - Scheduling (job ordering)
   - Routing (shortest path)
   - Resource allocation (activity selection)

### Deliverables:
- [ ] Summarize greedy choice property in own words
- [ ] List 3 problems with/without greedy property
- [ ] Outline template for greedy algorithm

### Resources:
- Visualizations: why earliest finish
- Comparison chart: greedy vs DP vs backtracking
- Template: pseudocode

---

## WEEK 12, DAY 2: INTERVAL SCHEDULING (CANONICAL EXAMPLE)

**Time:** 120 minutes

### Learning Sequence:
1. **Problem Statement** (20 min)
   - Real applications: room booking, event scheduling
   - Input/output format
   - Simple example (3-4 intervals)

2. **Why Greedy?** (20 min)
   - Earliest start fails (visual example)
   - Shortest duration fails (visual example)
   - Earliest finish works (intuition)

3. **The Algorithm** (20 min)
   - Sort by finish time
   - Greedy selection (pick earliest, skip overlapping)
   - Time/space complexity
   - Implementation (pseudocode + code)

4. **Exchange Argument Proof** (25 min)
   - Lemma: greedy first interval swappable with OPT
   - Induction on remaining intervals
   - Why swap preserves optimality
   - Conclusion: greedy is optimal

5. **Weighted Variant (Why Greedy Fails)** (15 min)
   - Problem: maximize total value
   - Counterexample: one heavy blocks two lighter
   - Why greedy by value/duration fails
   - Fix: dynamic programming (next week)

### Deliverables:
- [ ] Solve 3-4 interval scheduling examples
- [ ] Write exchange argument proof outline
- [ ] Find counterexample for weighted variant

### Code Exercise:
```
function intervalSchedule(intervals):
  sort by finish time
  selected = [intervals[0]]
  for i = 1 to n-1:
    if intervals[i].start >= selected[-1].finish:
      selected.append(intervals[i])
  return selected
```

---

## WEEK 12, DAY 3: MINIMUM SPANNING TREES

**Time:** 120 minutes

### Learning Sequence:
1. **MST Problem Revisited** (20 min)
   - Why greedy perspective?
   - How cut property justifies greedy
   - Difference from shortest path

2. **Kruskal's Algorithm** (30 min)
   - Sort edges by weight
   - Greedy edge selection via union-find
   - Cycle detection
   - Time/space complexity O(E log E + E α(n))
   - Implementation walkthrough

3. **Prim's Algorithm** (30 min)
   - Start from vertex
   - Greedy edge from tree to non-tree
   - Priority queue for candidate edges
   - Time/space complexity O((V+E) log V)
   - Implementation walkthrough

4. **Comparison & Trade-offs** (15 min)
   - Kruskal best for sparse graphs
   - Prim best for dense graphs
   - When to use which
   - Parallelization opportunities

5. **Cut Property (Theoretical Justification)** (15 min)
   - Definition: partition into (S, T), lightest crossing edge
   - Proof: lightest edge in some MST
   - Why both algorithms satisfy cut property

### Deliverables:
- [ ] Implement Kruskal's algorithm
- [ ] Implement Prim's algorithm
- [ ] Trace both on 5-vertex example
- [ ] Explain cut property in own words

---

## WEEK 12, DAY 4: HUFFMAN CODING

**Time:** 90 minutes

### Learning Sequence:
1. **Prefix Code Problem** (15 min)
   - Motivation: compression
   - Unique decoding requirement
   - Prefix-free property

2. **Building Huffman Trees** (15 min)
   - Binary tree representation
   - Code length = depth
   - Why shorter paths for frequent symbols

3. **Huffman's Greedy Algorithm** (20 min)
   - Maintain priority queue
   - Extract two smallest, merge
   - Insert parent back
   - Time O(n log n)

4. **Correctness Proof** (20 min)
   - Lemma 1: two minimums are siblings
   - Lemma 2: merging two minimums is optimal
   - Induction on merged node
   - Conclusion: greedy is optimal

5. **Information-Theoretic Optimality** (10 min)
   - Shannon entropy bound
   - Huffman achieves within 1 bit/symbol
   - Real-world: gzip, JPEG use Huffman

### Deliverables:
- [ ] Build Huffman tree for example string
- [ ] Implement Huffman encoding/decoding
- [ ] Prove Lemma 1 and Lemma 2

---

## WEEK 12, DAY 5: WHEN GREEDY FAILS & SELECTION FRAMEWORK

**Time:** 120 minutes

### Learning Sequence:
1. **Weighted Interval Scheduling Failure** (15 min)
   - One heavy interval blocks two lighter
   - Why greedy by value fails
   - Why greedy by value/duration fails
   - Solution: DP (overlap subproblems)

2. **Coin Change Failure** (15 min)
   - Coins {1,3,4}, amount 6
   - Greedy {4,1,1} vs optimal {3,3}
   - Why denominations matter
   - Solution: DP

3. **Graph Coloring (NP-Hard)** (15 min)
   - Greedy first-fit color assignment
   - May use many colors; optimal uses far fewer
   - No polynomial algorithm known
   - Why exchange argument impossible

4. **Longest Path Variant** (15 min)
   - Shortest path: greedy works (Dijkstra)
   - Longest path: greedy fails (NP-hard in general)
   - Why different? Negative weight intuition
   - DAGs solvable via DP

5. **Decision Framework** (30 min)
   - Paradigm comparison matrix (greedy vs DP vs backtracking)
   - Problem-solving strategy checklist
   - Hybrid approaches (greedy + DP)
   - Real-world examples of paradigm selection

6. **Meta-Lesson** (15 min)
   - Problem understanding trumps algorithm
   - Greedy's elegance tempts overuse
   - Exchange arguments force rigor
   - "Prove it works" is discipline

### Deliverables:
- [ ] Find counterexample for each failure pattern
- [ ] Complete paradigm comparison matrix
- [ ] Solve 3 decision framework problems
- [ ] Outline when to prove via exchange vs induction

---

## WEEK 13, DAY 1: BACKTRACKING FOUNDATIONS

**Time:** 90 minutes

### Learning Sequence:
1. **Backtracking Definition** (15 min)
   - Systematic DFS exploration of solution space
   - Build incrementally
   - Prune when constraints violated
   - Find ANY or ALL feasible solutions

2. **State Space Trees** (20 min)
   - Nodes = partial solutions
   - Edges = choices
   - Leaves = complete solutions
   - Depth = decisions made
   - Breadth = choices per decision

3. **Generic Algorithm & Pseudocode** (20 min)
   - Build solution incrementally
   - Check feasibility (pruning)
   - Recurse or backtrack
   - Complexity analysis: O(b^d) worst, O(pruned) typical

4. **Pruning Strategies** (20 min)
   - Constraint checking (hard constraints)
   - Feasibility checking (domain reduction)
   - Bound checking (preview of branch & bound)
   - Variable ordering (MRV heuristic)

5. **Space Complexity** (10 min)
   - O(d) recursion stack
   - O(d) solution storage
   - Much better than storing all solutions

### Deliverables:
- [ ] Draw state space tree for simple problem
- [ ] Implement generic backtracking template
- [ ] Design 3 different pruning strategies

---

## WEEK 13, DAY 2: BACKTRACKING APPLICATIONS

**Time:** 120 minutes

### Learning Sequence:
1. **N-Queens Problem** (30 min)
   - Place N queens, no attacks
   - State: current row decision
   - Constraints: column, diagonal conflicts
   - Feasibility check: safe_place function
   - Algorithm walkthrough
   - Complexity: O(N!) worst, O(N) typical

2. **Sudoku Solver** (30 min)
   - 9×9 grid, 1-9 unique per row/column/box
   - State: current grid fill
   - Choices: valid digits for empty cell
   - Constraint propagation optimization
   - MRV heuristic (choose most constrained cell)
   - Complexity analysis with optimizations

3. **Word Search / Boggle** (20 min)
   - Find word path in letter grid
   - DFS from each cell
   - Visited tracking (no reuse)
   - Early termination (letter mismatch)
   - Trie optimization for multiple words

4. **Heuristics Summary** (15 min)
   - MRV: choose most constrained first
   - LCV: try least constraining value first
   - Constraint propagation: deduce implications
   - Forward checking: reduce future domains

5. **Pattern Recognition** (15 min)
   - When backtracking applies
   - Pruning critical for practicality
   - Heuristics transform exponential to practical

### Deliverables:
- [ ] Implement N-Queens with backtracking
- [ ] Implement Sudoku solver with propagation
- [ ] Trace Word Search on 4x4 grid
- [ ] Design custom heuristic for problem

---

## WEEK 13, DAY 3: BRANCH AND BOUND PARADIGM

**Time:** 120 minutes

### Learning Sequence:
1. **Distinction from Backtracking** (20 min)
   - Backtracking: prune when infeasible
   - B&B: prune when suboptimal
   - Both use decision trees
   - B&B adds numerical comparison

2. **Node Structure & Bounding** (25 min)
   - Level: depth (decisions made)
   - Value: actual cost/benefit
   - Bound: estimated best achievable
   - Lower bound (minimization): can't do better
   - Upper bound (maximization): can't exceed

3. **Generic B&B Algorithm** (25 min)
   - Maintain frontier (queue, stack, priority queue)
   - Select node (strategy-dependent)
   - Check bound: can't improve → prune
   - Leaf: complete solution → update best
   - Otherwise: expand → add to frontier

4. **Queue Selection Strategies** (20 min)
   - FIFO (breadth-first): medium pruning, high memory
   - LIFO (depth-first): medium pruning, low memory, faster
   - Best-first: excellent pruning, high memory
   - Trade-offs and when to use

5. **Bounding Function Design** (20 min)
   - Relaxation: remove constraints, solve relaxed
   - Greedy: use greedy on remaining
   - LP relaxation: solve linear program
   - Problem-specific: domain knowledge
   - Tight vs loose bounds, computation cost

### Deliverables:
- [ ] Implement generic B&B template
- [ ] Design bounding function for toy problem
- [ ] Compare queue strategies on example
- [ ] Analyze when bound pruning effective

---

## WEEK 13, DAY 4: BRANCH AND BOUND APPLICATIONS

**Time:** 120 minutes

### Learning Sequence:
1. **0/1 Knapsack via B&B** (30 min)
   - State: items 0..i, included/excluded
   - Bound: greedy relaxation on remaining
   - Sort by value/weight ratio
   - Fill fractional knapsack as bound
   - Algorithm walkthrough
   - Practical for n ≤ 30-40

2. **TSP via B&B** (35 min)
   - State: partial tour
   - Bound: MST of unvisited + current cost
   - Why MST is valid lower bound
   - Algorithm walkthrough
   - Good initial bound via nearest neighbor
   - Practical for n ≤ 20-25

3. **Assignment Problem** (15 min)
   - n workers to n jobs
   - Min-cost matching
   - Hungarian algorithm as bounding
   - Better bounds than TSP

4. **Comparison: Backtracking vs B&B** (15 min)
   - Purpose: feasible vs optimal
   - Pruning: constraints vs bounds
   - Complexity: exponential (pruning-dependent)
   - Proof: bounds guarantee optimality

5. **Real-World Considerations** (15 min)
   - Practical vs theoretical complexity
   - Tuning bounds tightness
   - Hybrid approaches
   - When B&B practical vs approximation

### Deliverables:
- [ ] Implement knapsack B&B solver
- [ ] Implement TSP B&B solver
- [ ] Compare on various inputs
- [ ] Design problem-specific bound

---

## WEEK 13, DAY 5: ALGORITHM SELECTION FRAMEWORK

**Time:** 120 minutes

### Learning Sequence:
1. **Paradigm Comparison Matrix** (20 min)
   - All paradigms learned so far
   - Use cases, complexity, space, optimality
   - Difficulty levels
   - Visual comparison chart

2. **Problem-to-Paradigm Mapping** (25 min)
   - N-Queens → Backtracking
   - MST → Greedy
   - Knapsack → DP
   - TSP → B&B
   - Shortest path → Greedy (Dijkstra)
   - 15+ problem types mapped

3. **Decision Flowchart** (20 min)
   - NP-hard? Size? Structure?
   - Overlapping subproblems? (DP)
   - Greedy choice property? (Greedy)
   - Monotone property? (Binary search)
   - Interactive decision process

4. **Case Studies** (30 min)
   - Weighted interval scheduling (why DP)
   - Fractional vs 0/1 knapsack (why different)
   - Shortest path variants (why algorithms differ)
   - Greedy failure patterns

5. **Meta-Skills** (15 min)
   - Understand problem structure
   - Identify problem type
   - Check for problem properties
   - Select paradigm confidently
   - Prove correctness
   - Analyze complexity

### Deliverables:
- [ ] Complete paradigm comparison matrix
- [ ] Map 10 problems to paradigms
- [ ] Trace decision flowchart on new problem
- [ ] Design decision checklist for yourself

---

## WEEK 13, DAY 6: AMORTIZED ANALYSIS (FORMAL METHODS)

**Time:** 120 minutes

### Learning Sequence:
1. **Motivation & Definition** (15 min)
   - Some operations expensive, others cheap
   - Amortized: average over sequence
   - Not probabilistic; guaranteed average
   - Why not naive worst-case analysis

2. **Aggregate Method** (25 min)
   - Total cost of sequence / #operations
   - Dynamic array push: O(n) total cost / n operations = O(1) amortized
   - Stack with multipop: O(n) total / n operations = O(1)
   - Simple, intuitive, straightforward

3. **Accounting Method** (25 min)
   - Assign "amortized cost" to operations
   - Some overpay, some underpay
   - Maintain credit invariant
   - Dynamic array: charge 3 per push (1 store + 2 future resize)
   - Build credit pool, use for expensive operations
   - Trace through examples

4. **Potential Method** (25 min)
   - Define potential function Φ(state)
   - Amortized cost = actual + ΔΦ
   - Prove potential never negative
   - Dynamic array example: Φ = 2n - c
   - Elegant, mathematically sophisticated
   - Best for complex cases

5. **Applications & Comparison** (20 min)
   - When to use each method
   - Union-Find with path compression (amortized α(n))
   - Fibonacci heaps (cascading cuts, amortized O(1) decrease-key)
   - Choosing method for problem

### Deliverables:
- [ ] Prove dynamic array amortized O(1) using all 3 methods
- [ ] Analyze stack multipop using amortized analysis
- [ ] Design potential function for custom problem
- [ ] Compare analysis methods on example

---

## WEEK 14, DAY 1: MATRIX ALGORITHMS

**Time:** 90 minutes

### Learning Sequence:
1. **Standard Traversals** (20 min)
   - Row-major order
   - Column-major order
   - Diagonal order (left-to-right or zigzag)
   - Spiral order (clockwise or counterclockwise)
   - Applications: image processing, cache optimization

2. **Matrix Search Patterns** (20 min)
   - Staircase search: O(m+n) in sorted matrix
   - Why not binary search: not globally sorted
   - Why staircase works: monotone structure
   - Example: search in matrix with sorted rows/columns

3. **Matrix Transformations** (20 min)
   - 90° rotation in-place: O(n²) time, O(1) space
   - Transpose in-place: swap (i,j) with (j,i)
   - Spiral traversal: layer-by-layer approach
   - Real applications: image rotation, coordinate transforms

4. **Practical Considerations** (15 min)
   - Cache-friendly iteration patterns
   - When to transpose vs iterate differently
   - Space vs time trade-offs

### Deliverables:
- [ ] Implement spiral traversal
- [ ] Implement 90° rotation in-place
- [ ] Solve matrix search problem
- [ ] Analyze cache locality for traversals

---

## WEEK 14, DAY 2: BACKTRACKING ON GRIDS

**Time:** 120 minutes

### Learning Sequence:
1. **Word Search / Boggle** (35 min)
   - DFS from each cell
   - Visited tracking (no reuse)
   - Build word character-by-character
   - Early termination when letter mismatch
   - Complexity: O(N × M × 4^L)
   - Optimization: trie for multiple words

2. **Sudoku Solver** (35 min)
   - Constraint propagation: deduce forced assignments
   - Naked singles: cell with one choice
   - Hidden singles: digit only fits one cell
   - MRV heuristic: choose cell with fewest options
   - Forward checking: reduce domains
   - Complexity: O(10^6) with optimizations

3. **Rat in Maze Variants** (20 min)
   - Find path from start to end
   - Obstacles, multiple targets, weighted
   - BFS for shortest path
   - DFS for any path
   - Backtracking for all paths

4. **Optimization Techniques** (20 min)
   - Constraint propagation reduces branching
   - MRV heuristic: most constrained first
   - Early termination: impossible state detected
   - Problem-specific pruning

### Deliverables:
- [ ] Implement word search with DFS
- [ ] Implement Sudoku solver with propagation
- [ ] Solve maze with BFS and DFS
- [ ] Compare performance with/without optimizations

---

## WEEK 14, DAY 3: BITMASK TRICKS & BITMASK DP

**Time:** 120 minutes

### Learning Sequence:
1. **Essential Bit Operations** (25 min)
   - Check bit: (x >> i) & 1
   - Set bit: x | (1 << i)
   - Clear bit: x & ~(1 << i)
   - Toggle bit: x ^ (1 << i)
   - Isolate rightmost set bit: x & (-x)
   - Check power of 2: x & (x-1) == 0
   - Count set bits: Brian Kernighan's algorithm

2. **Subset Representation** (20 min)
   - Integer as bitmask for subset
   - i-th bit = whether element i in subset
   - Iterate all 2^n subsets: for(int mask = 0; mask < (1<<n); mask++)
   - Iterate all submasks: loop via (mask-1) & original
   - Total submask operations: O(3^n)

3. **Bitmask DP: TSP Example** (35 min)
   - State: dp[mask][i] = shortest path visiting cities in mask, ending at i
   - Transition: try extending to each unvisited city
   - Base case: single cities
   - Complexity: O(2^n × n^2) time, O(2^n × n) space
   - Practical for n ≤ 20

4. **Other Bitmask DP Applications** (15 min)
   - Subset selection with constraints
   - Job assignment (assigning n jobs to m workers)
   - Matrix sum over subset paths
   - Recognizing problems suitable for bitmask DP

5. **Trade-offs & Limitations** (15 min)
   - Exponential in n: practical for n ≤ 20
   - Space intensive: O(2^n × n)
   - Compared to greedy/DP approaches

### Deliverables:
- [ ] Implement 15 bit operation tricks
- [ ] Implement TSP bitmask DP
- [ ] Solve subset selection problem
- [ ] Estimate complexity for TSP with n=15 vs n=20

---

## WEEK 14, DAY 4: NUMBER THEORY & MODULAR ARITHMETIC

**Time:** 90 minutes

### Learning Sequence:
1. **Euclid's Algorithm (GCD)** (20 min)
   - gcd(a, b) = gcd(b, a mod b)
   - Time: O(log(min(a,b)))
   - Proof: via remainder property
   - LCM = a × b / gcd(a,b)
   - Extended GCD: find x,y such that ax + by = gcd(a,b)

2. **Fast Exponentiation** (20 min)
   - Compute a^b mod m in O(log b)
   - Binary representation of exponent
   - Repeated squaring
   - Applications: cryptography, modular arithmetic

3. **Modular Arithmetic** (20 min)
   - (a + b) mod m = ((a mod m) + (b mod m)) mod m
   - (a × b) mod m = ((a mod m) × (b mod m)) mod m
   - Avoiding overflow with large numbers
   - Modular inverse (when it exists)

4. **Applications & Real-World Use** (20 min)
   - Hash functions
   - Random number generation
   - Cryptographic protocols
   - Competitive programming (avoid overflow)

### Deliverables:
- [ ] Implement GCD and extended GCD
- [ ] Implement fast exponentiation
- [ ] Solve modular arithmetic problems
- [ ] Use number theory in interview problem

---

## WEEK 14, DAY 5: PROBABILITY & SAMPLING

**Time:** 90 minutes

### Learning Sequence:
1. **Expected Value & Linearity** (20 min)
   - E[X+Y] = E[X] + E[Y] (even if dependent!)
   - Application: quicksort expected analysis
   - Application: randomized algorithm complexity
   - Simple yet powerful tool

2. **Reservoir Sampling (Uniform)** (25 min)
   - Select k items uniformly from unknown-size stream
   - Algorithm: keep first k, then probability k/n for n-th item
   - Implementation via random index
   - Time: O(n), Space: O(k)
   - Proof: each item has probability k/n

3. **Weighted Reservoir Sampling** (20 min)
   - Items have weights, sample with probability proportional to weight
   - Algorithm: use exponential weights e^(log(weight)/u) for uniform random u
   - Select k items with highest weighted keys
   - Application: biased sampling from streams

4. **Applications & Real-World Use** (15 min)
   - Big data processing (sampling from logs)
   - Approximate statistics
   - Randomized algorithms
   - Probabilistic data structures

### Deliverables:
- [ ] Implement uniform reservoir sampling
- [ ] Implement weighted reservoir sampling
- [ ] Prove sampling probability in writing
- [ ] Apply to real data stream problem

---

## WEEK 15, DAY 1: KMP STRING MATCHING

**Time:** 120 minutes

### Learning Sequence:
1. **Problem & Naive Approach** (20 min)
   - Find pattern in text
   - Naive: O(nm) with character re-checking
   - Example: text "ABCABCD", pattern "ABCD"
   - Why re-checking happens

2. **Failure Function / LPS Array** (35 min)
   - LPS[i] = longest proper prefix of P[0..i] that is also suffix
   - Example: P = "ABAB" → LPS = [0,0,1,2]
   - Computation: O(n) using previous values
   - Intuition: avoid re-checking known characters

3. **KMP Algorithm** (35 min)
   - Use LPS to skip characters when mismatch
   - Maintain pointers in text and pattern
   - When pattern character doesn't match, use LPS to jump
   - Time: O(n+m), Space: O(n)
   - Implementation walkthrough with example

4. **Complexity Analysis** (15 min)
   - Why O(n+m) is optimal
   - Cannot do better than scanning entire text
   - Compare to naive O(nm)
   - Practical constant factors

5. **Real-World Applications** (10 min)
   - DNA sequence matching
   - Text searching in editors
   - Plagiarism detection
   - Network packet inspection

### Deliverables:
- [ ] Compute LPS array for patterns
- [ ] Implement KMP algorithm
- [ ] Trace KMP on text-pattern pair
- [ ] Analyze complexity carefully

---

## WEEK 15, DAY 2: Z-ALGORITHM & STRING ANALYSIS

**Time:** 120 minutes

### Learning Sequence:
1. **Z-Function Definition** (20 min)
   - Z[i] = length of prefix of string starting at i
   - Example: "aabxaabxaab" → Z = [11, 1, 0, 0, 7, 1, 0, 0, 3, 1, 0]
   - Intuition: how much of string repeats at position i

2. **Z-Function Computation** (30 min)
   - Naive: O(n²) computing each Z[i] independently
   - Linear-time: O(n) using previous computations
   - Maintain window [L, R] of rightmost match
   - Use previously computed Z values to skip work

3. **Pattern Matching via Z-Function** (25 min)
   - Construct string: pattern + "$" + text
   - Compute Z-function
   - Z[i] = length of pattern match at position i in text
   - Time: O(n+m), Space: O(n+m)
   - Advantage over KMP: simpler implementation

4. **Other Z-Function Applications** (20 min)
   - Periodicity detection (Z[i] = n-i for period)
   - Rotation detection (double string contains rotation)
   - Border detection (Z[i] = something for borders)
   - String compression patterns

5. **Comparison: KMP vs Z-Algorithm** (15 min)
   - Similar O(n+m) complexity
   - Z-algorithm often simpler to implement
   - Both avoid pattern re-checking
   - Choose based on familiarity

### Deliverables:
- [ ] Compute Z-function for strings
- [ ] Implement Z-function computation
- [ ] Use Z-algorithm for pattern matching
- [ ] Solve periodicity problem via Z-function

---

## WEEK 15, DAY 3: MANACHER'S ALGORITHM (OPTIONAL)

**Time:** 90 minutes

### Learning Sequence:
1. **Longest Palindromic Substring Problem** (15 min)
   - Find longest substring that is palindrome
   - Naive: O(n²) with expand around center
   - Optimal: O(n) using Manacher

2. **Manacher's Algorithm Intuition** (25 min)
   - Use symmetry of palindromes
   - Track center and right boundary of rightmost palindrome
   - When expanding past boundary, use mirror information
   - Reduce work via symmetry

3. **Algorithm Details** (30 min)
   - Handle even/odd palindromes with padding
   - Insert '#' between characters
   - Track center C, right boundary R
   - For position i, use mirror to get initial length
   - Expand from initial if possible
   - Update C and R when palindrome extends right

4. **Complexity & Proof** (15 min)
   - Time: O(n) amortized
   - Proof: right boundary only increases (never left)
   - Each character examined constant times

### Deliverables:
- [ ] Understand palindrome symmetry
- [ ] Implement Manacher's algorithm
- [ ] Trace on example strings
- [ ] Compare naive vs Manacher time

---

## WEEK 15, DAY 4: SUFFIX STRUCTURES (OPTIONAL)

**Time:** 90 minutes

### Learning Sequence:
1. **Suffix Arrays** (30 min)
   - Sorted array of suffix indices
   - Space-efficient (O(n log n) time to build, O(n) space)
   - Pattern matching: binary search on sorted suffixes
   - LCP array for longest common prefix

2. **Suffix Trees** (30 min)
   - Compressed trie of all suffixes
   - Space: O(n) (constant alphabet) to O(n log n) (general)
   - Pattern matching: O(|pattern|) time (no binary search needed)
   - Specialized use: highly optimized for certain operations

3. **Comparison & Trade-offs** (15 min)
   - Suffix arrays: simpler, more space-efficient
   - Suffix trees: faster queries, complex construction
   - When to use which

4. **Applications** (10 min)
   - Exact pattern matching
   - LCS across multiple strings
   - Text compression
   - Bioinformatics (DNA analysis)

### Deliverables:
- [ ] Build suffix array for string
- [ ] Use suffix array for pattern matching
- [ ] Understand suffix tree structure
- [ ] Identify when suffix structures useful

---

## WEEK 15, DAY 5: NETWORK FLOW - MAX FLOW MIN-CUT

**Time:** 120 minutes

### Learning Sequence:
1. **Flow Network Fundamentals** (20 min)
   - Vertices, edges with capacities
   - Source s, sink t
   - Flow conservation: flow in = flow out (except s, t)
   - Feasible flow: respects capacity constraints

2. **Ford-Fulkerson Method** (30 min)
   - Find augmenting paths in residual graph
   - Augment flow by min capacity along path
   - Repeat until no augmenting path exists
   - Complexity: depends on how paths found

3. **Edmonds-Karp Algorithm** (25 min)
   - Use BFS to find augmenting paths
   - Guaranteed O(VE²) convergence
   - Each augmentation increases shortest path length
   - Practical for moderate-sized graphs

4. **Max-Flow Min-Cut Theorem** (20 min)
   - Max flow = min cut value
   - Cut: partition vertices into (S, T) containing s and t
   - Cut value: sum of capacities leaving S
   - Intuition: bottleneck = max flow

5. **Implementation & Optimizations** (15 min)
   - Adjacency list representation
   - Residual graph management
   - Push-relabel algorithm (advanced)

### Deliverables:
- [ ] Implement Ford-Fulkerson with DFS
- [ ] Implement Edmonds-Karp with BFS
- [ ] Trace max-flow on graph example
- [ ] Identify min-cut from max-flow solution

---

## WEEK 15, DAY 6: MATCHING & ASSIGNMENT VIA FLOW

**Time:** 120 minutes

### Learning Sequence:
1. **Bipartite Matching Problem** (25 min)
   - Match nodes in left set to right set (at most 1 edge per node)
   - Maximum matching: find matching with max edges
   - Construction: flow network with source, sink, capacity 1
   - Max matching = max flow value

2. **Bipartite Matching via Max Flow** (25 min)
   - Add source s to left vertices (capacity 1)
   - Add right vertices to sink t (capacity 1)
   - Left-right edges have capacity 1
   - Max flow finds maximum matching

3. **Assignment Problem** (25 min)
   - Assign n workers to n jobs with costs
   - Minimize total cost while matching all
   - Min-cost max-flow algorithm
   - Use shortest paths (Bellman-Ford) for augmentation

4. **Real-World Applications** (20 min)
   - Personnel assignment
   - Resource allocation
   - Task scheduling
   - Network routing

5. **Implementation & Practical Considerations** (15 min)
   - Reduction techniques
   - Complexity of various algorithms
   - When flow-based approach practical vs combinatorial

### Deliverables:
- [ ] Reduce matching to max-flow problem
- [ ] Implement bipartite matching via flow
- [ ] Solve assignment problem
- [ ] Identify min-cost max-flow applications

---

---

# 🎓 PHASE D & E COMPLETION CHECKLIST

### Algorithmic Paradigms Mastered:
- [ ] Greedy (with exchange argument proofs)
- [ ] Backtracking (with constraint propagation)
- [ ] Branch & Bound (with bounding functions)
- [ ] Dynamic Programming (integrated from earlier)
- [ ] Divide & Conquer (integrated from earlier)
- [ ] String Matching (KMP, Z-algorithm)
- [ ] Network Flow (max-flow min-cut)
- [ ] Amortized Analysis (aggregate, accounting, potential)
- [ ] Bitmask DP (subset-based optimization)
- [ ] Number Theory & Modular Arithmetic
- [ ] Probability & Sampling

### Problem-Solving Skills:
- [ ] Identify problem structure and constraints
- [ ] Select appropriate paradigm from 8+ options
- [ ] Design and prove algorithm correctness
- [ ] Analyze time and space complexity
- [ ] Optimize via heuristics, pruning, bounds
- [ ] Integrate multiple paradigms for complex problems
- [ ] Implement efficiently in code

### Real-World Readiness:
- [ ] Interview-ready on algorithm questions
- [ ] Understand trade-offs between approaches
- [ ] Can adapt algorithms to problem variations
- [ ] Confident in algorithm selection process

---

**Status:** ✅ **COMPLETE WEEKLY & DAYWISE PLAN FOR PHASE D & E**

Ready for implementation, tracking, and instruction.
