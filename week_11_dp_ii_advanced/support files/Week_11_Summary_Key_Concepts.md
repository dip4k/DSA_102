# 📘 WEEK 11 SUMMARY: KEY CONCEPTS & QUICK REFERENCE

**Week:** 11 | **Purpose:** One-page reference for all Week 11 concepts  
**For:** Quick lookup during problem-solving or review

---

## 📌 WEEK 11 AT A GLANCE

**Weekly Theme:** Dynamic Programming on Complex Structures

| Day | Topic | Key Concept | Time | Must-Know |
| :--- | :--- | :--- | :--- | :--- |
| 1 | DP on Trees | Post-order + 2-state DP | 120 min | Maximum independent set |
| 2 | DP on DAGs | Topological sort + DP | 120 min | Critical path method |
| 3 | Bitmask DP | Subset enumeration | 120 min | TSP solver |
| 4 | Optimization | Space compression | 90 min | Sliding window |
| 5 | Mixed DP | Problem recognition | 90 min | 5-step strategy |

---

## 🔑 CORE CONCEPTS

### CONCEPT 1: Tree DP Framework

**Pattern:**
```
1. Solve for each subtree (bottom-up)
2. Combine children's answers at parent
3. Post-order traversal (children before parent)

Time: O(n)
Space: O(h) where h = height
```

**State Definition:**
```
dp[node][0] = answer if node is EXCLUDED
dp[node][1] = answer if node is INCLUDED
```

**Transition:**
```
dp[node][0] = sum(max(dp[child][0], dp[child][1]))
dp[node][1] = node.value + sum(dp[child][0])
```

**Problems:**
- Maximum independent set
- Tree diameter
- House robber (tree variant)
- Path sum problems

---

### CONCEPT 2: DAG DP Framework

**Pattern:**
```
1. Compute topological sort
2. Process nodes in topo order
3. Apply DP transitions
4. Extract answer

Time: O(V + E)
Space: O(V)
```

**Critical Requirement:**
```
Process node ONLY AFTER all predecessors processed
Violating this → WRONG ANSWER
```

**Applications:**
- Longest/shortest paths
- Critical path method
- Dependency resolution
- Game state evaluation

**Formula:**
```
dp[node] = aggregate(dp[predecessor] + edge_cost)

For longest path:
  dp[u] = max(dp[v] + 1) for v in predecessors(u)
```

---

### CONCEPT 3: Bitmask DP Framework

**Pattern:**
```
1. Represent subset as bitmask (integer)
2. Iterate all 2^n masks: 0 to 2^n - 1
3. For each mask, try transitions
4. DP state: dp[mask] or dp[mask][position]

Time: O(2^n × n²)
Space: O(2^n × n)
Feasible: n ≤ 20
```

**Key Operations:**
```
Check bit:    (mask & (1 << i)) != 0
Set bit:      mask | (1 << i)
Clear bit:    mask & ~(1 << i)
Toggle bit:   mask ^ (1 << i)
Popcount:     __builtin_popcount(mask)
```

**Classic Problems:**
- TSP (Traveling Salesman)
- Subset sum
- Independent set (small graphs)
- Graph coloring
- Crew scheduling

---

### CONCEPT 4: State Compression

**Technique 1: Sliding Window**
```
Original:  dp[i][j] = O(m × n) space
Optimized: dp[j] = O(n) space

How: Only keep current and previous row
Example: LCS, edit distance
```

**Technique 2: Dimension Reduction**
```
Original:  dp[a][b][c] = O(a × b × c)
Optimized: dp[b][c] = O(b × c)

How: Eliminate dimension that's not in transitions
```

**Technique 3: Lazy Evaluation**
```
Memoization: Compute only reachable states
Benefit: Space proportional to reachable_states, not total_states
```

**Technique 4: Pruning**
```
Branch-and-bound: Skip branches that can't improve
Compute: upper_bound for remaining search
Skip if: current + upper_bound < best_found
Effect: 10-50× speedup (problem-dependent)
```

---

## ⚡ QUICK REFERENCE: WHEN TO USE WHAT

### Tree DP? Use when:
- ✅ Problem structure is a tree
- ✅ Can combine children's answers
- ✅ Answer depends on subtree, not whole tree
- ❌ Graph has cycles (use general DP)

### DAG DP? Use when:
- ✅ Graph is acyclic (can be verified)
- ✅ Need path/ordering computation
- ✅ Must process in specific order
- ❌ Graph has cycles (no topological order)

### Bitmask DP? Use when:
- ✅ n ≤ 20 (at most)
- ✅ Need to enumerate subsets
- ✅ Problem requires "visited" tracking
- ❌ n > 25 (state space explodes)

### Space Compression? Use when:
- ✅ DP table too large for memory
- ✅ Only need previous states (sliding window)
- ✅ Dimension truly independent
- ❌ All states needed (can't compress)

---

## 🔀 PROBLEM-TO-ALGORITHM MAPPING

| Problem Class | Algorithm | Time | Space | Example |
| :--- | :--- | :--- | :--- | :--- |
| Tree paths | Tree DP | O(n) | O(h) | Diameter, independent set |
| DAG paths | Topo sort + DP | O(V+E) | O(V) | Longest path, CPM |
| Subset selection | Bitmask DP | O(2^n × n²) | O(2^n × n) | TSP, subset sum |
| String matching | 2D DP → 1D | O(m×n) | O(n) | LCS, edit distance |
| Knapsack | 1D DP | O(n×W) | O(W) | 0/1 knapsack |
| Path counting | DP | O(n²) or O(V+E) | O(n) | Paths in grid/DAG |

---

## 🧩 STATE DESIGN PATTERNS

### Pattern 1: Optimization with Include/Exclude
```
dp[i][0] = best answer excluding element i
dp[i][1] = best answer including element i

Use: Tree DP, independent set, knapsack variants
```

### Pattern 2: Path Length in Trees
```
dp[node] = longest path going DOWN from node

Use: Tree diameter, depth computation
```

### Pattern 3: Count of Configurations
```
dp[node][color] = number of valid colorings if node has color

Use: Tree coloring, constraint satisfaction
```

### Pattern 4: Position + Subset (Bitmask)
```
dp[mask][last] = optimal cost visiting cities in mask, ending at last

Use: TSP, Hamiltonian path
```

### Pattern 5: Sequential Processing
```
dp[i] = optimal answer for elements 0..i

Use: LIS, coin change, knapsack
```

---

## 📊 COMPLEXITY CHEAT SHEET

```
┌─────────────────────────────────────────────────────────┐
│           DP COMPLEXITY QUICK REFERENCE                 │
├─────────────────────────┬───────────┬──────────────────┤
│ Problem                 │ Time      │ Space            │
├─────────────────────────┼───────────┼──────────────────┤
│ Tree DP (n nodes)       │ O(n)      │ O(h)             │
│ DAG DP (V,E)            │ O(V+E)    │ O(V)             │
│ Bitmask DP (n≤20)       │ O(2^n·n²) │ O(2^n·n)         │
│ LCS (m,n lengths)       │ O(m·n)    │ O(min(m,n)) opt  │
│ 0/1 Knapsack (n,W)      │ O(n·W)    │ O(W) optimized   │
│ Coin change (n,W)       │ O(n·W)    │ O(W)             │
│ LIS (n elements)        │ O(n²)     │ O(n)             │
│ Fibonacci (n)           │ O(n)      │ O(1) optimized   │
│ Tree coloring (n,k)     │ O(n·k)    │ O(n·k)           │
└─────────────────────────┴───────────┴──────────────────┘
```

---

## 🎯 RECOGNITION CHECKLIST

**Is this a DP problem?**

1. **Overlapping subproblems?**
   - Will same subproblem be computed multiple times?
   - If computing recursively, are there repeated calls?
   - ✅ YES → Continue
   - ❌ NO → Not DP

2. **Optimal substructure?**
   - Can optimal solution be built from optimal subproblems?
   - Does answer for n depend on answer for n-1?
   - ✅ YES → DP likely works
   - ❌ NO → Try different approach

3. **Reasonable state space?**
   - Number of unique subproblems manageable?
   - Estimated states ≤ 10^8?
   - ✅ YES → Implement DP
   - ❌ NO → Use approximation/heuristic

---

## 🚨 COMMON MISTAKES & FIXES

| Mistake | Symptom | Fix |
| :--- | :--- | :--- |
| **Wrong DP order** | Accessing uncomputed states | Verify dependencies before transition |
| **Uninitialized base case** | Nonsensical results | Initialize dp[0] or dp[base] explicitly |
| **Off-by-one error** | Answer off by 1 | Trace manually for small input |
| **Wrong transition formula** | Wrong answer on examples | Re-verify formula logic |
| **State space too large** | Memory exceeded/TLE | Use compression or different approach |
| **Bitmask operations** | Incorrect subset interpretation | Test bit operations on paper first |
| **Forgetting to memoize** | Time limit exceeded | Add memo dictionary for top-down |
| **Modifying DP array during iteration** | Incorrect values | Use two separate arrays or proper order |

---

## 💡 STATE DESIGN IN 60 SECONDS

**When stuck on state design:**

1. **Ask yourself:** "What parameters fully define a subproblem?"
   - Example: "Position i and whether node is included?"

2. **Write it down:** "dp[i][include] = ..."

3. **Verify completeness:** "Given these parameters, can I compute the answer?"

4. **Test on example:** Compute by hand for small input

5. **Check transitions:** "Can I compute dp[i+1] from dp[i]?"

---

## 📈 PROGRESSION THROUGH WEEK 11

### Day 1 Mastery Level
- Can solve tree DP in 15 minutes
- Understand post-order traversal
- Can trace 2-state DP manually

### Day 2 Mastery Level
- Can implement topological sort from scratch
- Understand why topo order matters
- Can apply DP on DAG correctly

### Day 3 Mastery Level
- Master bitmask operations
- Can implement TSP for n ≤ 15
- Understand feasibility (n ≤ 20)

### Day 4 Mastery Level (Optional)
- Can optimize 2D DP to 1D
- Understand space-time tradeoffs
- Can implement pruning strategy

### Day 5 Mastery Level (Optional)
- Can recognize DP in disguise
- Can design state for novel problem
- Can solve under time pressure

---

## 🔗 PREREQUISITE CONCEPTS

**Must Know:**
- Basic DP (Week 10): Memoization, tabulation, recurrence
- Trees (Week 8-9): Traversal, representation, properties
- Graphs (Week 8-9): Representation, traversal, topological sort
- Bit manipulation (Week 4-5): Bitwise operations

**Should Review:**
- Recursion (Week 2-3): DFS, backtracking
- Sorting (Week 6-7): Merge sort, quick sort (for sorting DAG)

**Nice to Have:**
- Greedy (Week 7): When to choose greedy over DP
- Advanced graphs (Week 9): Complex graph problems

---

## 🎓 ONE-PAGE DP CHEAT SHEET

### Tree DP
- **State:** dp[node][include] for two choices
- **Order:** Post-order (children before parent)
- **Time:** O(n)

### DAG DP
- **State:** dp[node] = best for this node
- **Order:** Topological sort (prerequisites first)
- **Time:** O(V + E)

### Bitmask DP
- **State:** dp[mask][position] for subset + current location
- **Order:** Iterate masks 0 to 2^n - 1
- **Time:** O(2^n × n²), feasible for n ≤ 20

### Optimization
- **Sliding window:** Keep only necessary rows (2D → 1D)
- **Pruning:** Skip branches that can't improve
- **Early termination:** Stop when answer found

### Recognition
- Overlapping? ✓
- Optimal structure? ✓
- Reasonable space? ✓
- → Use DP

---

## 📚 ADDITIONAL RESOURCES

**For conceptual understanding:**
- Week_11_FULL_PLAYBOOK.md (complete guide)
- Week_11_Day_0X_Instructional.md (specific topic)

**For implementation:**
- Week_11_Extended_CSharp_Complete_v13.md (C# code)

**For practice:**
- Week_11_Daily_Progress_Checklist.md (problem list)
- LeetCode: Filter by "dynamic programming" tag

**For interview prep:**
- This summary + problem-specific details
- Practice explaining solutions aloud

---

