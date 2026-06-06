# Week 13 Day 03: Branch & Bound — Engineering Guide

**📂 Metadata**
- **Week:** 13  
- **Day:** 03  
- **Phase:** 🟧 Algorithm Paradigms  
- **Category:** Optimization & Systematic Search  
- **Difficulty:** Advanced  
- **Real-World Impact:** Core technique for solving NP-hard optimization problems (TSP, knapsack, job scheduling, circuit design, resource allocation) where approximate or optimal solutions are needed within practical time bounds.  
- **Prerequisites:** Week 13 Day 01-02 (Backtracking), Graph Algorithms (Week 9-10), Dynamic Programming (Week 11-12), Priority Queues (Week 6)

---

## 🎯 Learning Objectives

By the end of this chapter, you will be able to:

1. **Understand Branch & Bound Framework**: Master the conceptual difference between backtracking (feasibility) and branch & bound (optimization), and recognize when branch & bound provides advantages over pure backtracking or dynamic programming.

2. **Implement Bounding Functions**: Design effective upper and lower bound functions for optimization problems, understanding the trade-off between bound tightness and computation cost.

3. **Apply Best-First Search Strategy**: Use priority queues to explore the most promising branches first, achieving better pruning and often finding optimal solutions earlier in the search.

4. **Solve Classic Optimization Problems**: Implement branch & bound solutions for Traveling Salesman Problem (TSP), 0/1 Knapsack, Assignment Problem, and Job Scheduling with detailed bounding strategies.

5. **Analyze Pruning Effectiveness**: Measure and optimize the pruning ratio in branch & bound algorithms, understanding when the technique is practical versus when it degrades to exhaustive search.

---

# Chapter 1: Context & Motivation — From Feasibility to Optimality

## The Problem: Beyond "Any Solution" to "Best Solution"

Days 01-02 covered **backtracking** for finding *any* solution satisfying constraints:
- N-Queens: Find *a* valid placement
- Sudoku: Find *a* valid completion
- Word Search: Find *if* word exists

But many real-world problems require finding the **best** solution:
- **Traveling Salesman**: Not just *a* tour, but the **shortest** tour
- **Knapsack**: Not just *a* valid selection, but **maximum value** selection
- **Job Scheduling**: Not just *a* schedule, but schedule with **minimum completion time**

### The Engineering Reality

**When Backtracking Isn't Enough**:

| Problem Type | Backtracking Approach | Limitation |
|--------------|----------------------|------------|
| **Feasibility** | Find any valid solution | Works well, prunes invalid branches |
| **Counting** | Count all valid solutions | Works, explores entire space |
| **Optimization** | Find best solution among all valid | Inefficient—must explore ALL solutions to confirm optimality |

**Example**: TSP with 10 cities
- **Backtracking**: Generates all (10-1)!/2 = 181,440 tours
- **Branch & Bound**: With good bounds, prunes 95%+ of branches → explores ~5,000-10,000 nodes
- **Speedup**: 18-36× faster

### The Challenge: Why Not Just Use Dynamic Programming?

**DP Requirements**:
1. **Optimal substructure**: Optimal solution contains optimal sub-solutions
2. **Overlapping subproblems**: Same subproblems solved multiple times
3. **Polynomial state space**: States must be enumerable in reasonable time

**Many problems fail these requirements**:
- **TSP**: State space is exponential (which cities visited + current city)
- **Constrained optimization**: DP can't efficiently handle complex constraints
- **Non-decomposable objectives**: Some objectives don't have clean substructure

**Branch & Bound fills the gap**: Works on problems where DP is impractical but exhaustive search is too slow.

### The Innovation: Intelligent Pruning Through Bounds

**Core Insight**: 
> "If we can prove that the best solution in a sub-tree is worse than a solution we already found, we can skip the entire sub-tree without exploring it."

**Mechanism**:
1. **Branching**: Divide solution space into smaller subspaces (like backtracking)
2. **Bounding**: Compute best possible solution in each subspace (new!)
3. **Pruning**: Eliminate subspaces that can't improve current best (aggressive!)

**Visual: Backtracking vs. Branch & Bound**

```
BACKTRACKING (Feasibility):
                    Root
                   /    \
              Valid?   Valid?
              /   \    /   \
           Found! X   X   Continue
           
Prunes: Invalid branches only
Explores: All valid paths until solution found

BRANCH & BOUND (Optimization):
                    Root (bound=100)
                   /              \
              Bound=60          Bound=45
              /      \          /      \
         Bound=55  Bound=48  X (pruned: 45 < 50)
         Found=50!    \
                    X (pruned: 48 < 50)
                    
Prunes: Invalid branches + suboptimal branches
Explores: Only promising branches (much fewer!)
```

### Real-World Impact: Where Branch & Bound Dominates

**Industry Applications**:

| Domain | Problem | Impact |
|--------|---------|--------|
| **Logistics** | Vehicle routing optimization | UPS saves $400M/year with optimized routes |
| **Manufacturing** | Production scheduling | 15-30% increase in throughput |
| **VLSI Design** | Circuit layout optimization | Reduces chip area by 20-40% |
| **Finance** | Portfolio optimization with constraints | Beats greedy approaches by 5-15% |
| **Telecommunications** | Network design & bandwidth allocation | Reduces infrastructure cost by 10-25% |

**Why it matters in interviews**:
- Tests understanding of optimization vs. feasibility
- Requires designing custom bounding functions (creativity!)
- Shows ability to balance exactness with practicality
- Common in system design discussions (e.g., "How would you optimize delivery routes?")

---

# Chapter 2: Building the Mental Model — The Framework

## The Branch & Bound Recipe

### Four Essential Components

**1. State Representation**
- **Current partial solution**: What decisions made so far
- **Remaining choices**: What decisions still need to be made
- **Bound value**: Best possible objective value from this state

**Example** (TSP):
```
State:
  - Path: [City0, City2, City5]  (visited cities)
  - Remaining: [City1, City3, City4, City6, City7]
  - Bound: 150 (lower bound on completing this tour)
```

**2. Branching Strategy**
- How to divide state space into subspaces
- Each branch represents a choice/decision

**Common strategies**:
- **Binary branching**: Include/exclude item (Knapsack)
- **N-ary branching**: Choose next item from N options (TSP)
- **Constraint-based**: Branch on violated constraints (Job Scheduling)

**3. Bounding Function**
- Computes **optimistic estimate** of objective value
- Must be:
  - **Admissible**: Never underestimates cost (minimization) or overestimates value (maximization)
  - **Efficient**: Computable quickly (should be much faster than solving subproblem exactly)
  - **Tight**: Close to actual optimal (loose bounds → less pruning)

**4. Selection Strategy** (Search Order)
- Which branch to explore next?

**Options**:
- **Best-First Search**: Explore state with best bound (uses priority queue)
- **Depth-First Search**: Explore deepest state first (uses stack)
- **Breadth-First Search**: Explore by level (uses queue)

**Trade-offs**:

| Strategy | Advantage | Disadvantage |
|----------|-----------|--------------|
| **Best-First** | Finds good solutions early, prunes more | Requires priority queue (memory overhead) |
| **Depth-First** | Memory efficient, works well with tight bounds | May explore bad branches deeply |
| **Breadth-First** | Guaranteed shortest path (unweighted) | High memory, no early solution |

**In practice**: Best-First Search is most common for optimization.

## Visual: The Branch & Bound Tree

```
┌─────────────────────────────────────────────────────────────┐
│          Branch & Bound State Space Tree (TSP)              │
└─────────────────────────────────────────────────────────────┘

                      ○ Root
                   /  |  \  \
              To:1  To:2  To:3  To:4
              Bound:45  Bound:50  Bound:48  Bound:60
                /         |         \
           To:2        To:1      PRUNED (48 already > bestSoFar=45)
         Bound:42    Bound:55
            /          PRUNED (55 > bestSoFar=42)
        To:3
      Bound:40
      Complete!
      bestSoFar = 40

Legend:
○ = State (node)
Bound = Lower bound on tour length from this state
PRUNED = Branch skipped because bound exceeds current best
```

**Key Observations**:
1. **Early best solutions**: Finding a good complete solution early improves pruning
2. **Bound tightness matters**: Loose bound (60) prunes poorly; tight bound (40) prunes aggressively
3. **Pruning cascades**: Pruning one branch can trigger pruning of its children

## The Bounding Function: The Heart of Branch & Bound

### What Makes a Good Bound?

**Three properties** (in order of importance):

1. **Correctness (Admissibility)**
   - **Minimization**: bound ≤ actual optimal
   - **Maximization**: bound ≥ actual optimal
   - Violating this → wrong answer!

2. **Tightness**
   - How close to actual optimal?
   - Tighter bounds → more pruning
   - Measured by: `(bound - optimal) / optimal`

3. **Efficiency**
   - How fast to compute?
   - Should be O(n) or O(n log n), rarely O(n²)
   - If bound computation is O(n³), might as well solve subproblem!

**Visual: Bound Tightness Spectrum**

```
Problem: TSP with actual optimal tour = 100

Trivial Bound (useless):
└─ Bound = 0 (always admissible, never prunes)

Loose Bound:
└─ Bound = 50 (admissible, prunes half of bad branches)

Tight Bound:
└─ Bound = 95 (admissible, prunes 95% of bad branches)

Exact Solution (defeats the purpose):
└─ Bound = 100 (perfect, but too expensive to compute)

Sweet Spot: Tight enough to prune well, fast enough to compute
```

### Common Bounding Techniques

**1. Relaxation (Most Common)**
- Remove some constraints to get easier problem
- Solve relaxed problem → use as bound

**Examples**:
- **0/1 Knapsack** → Fractional Knapsack (allow partial items)
- **TSP** → Minimum Spanning Tree (remove tour requirement)
- **Integer Programming** → Linear Programming (allow fractional values)

**2. Greedy Heuristic**
- Use fast greedy algorithm to get feasible solution
- Use as upper bound (maximization) or lower bound (minimization)

**3. Subproblem Decomposition**
- Break into independent subproblems
- Sum optimal solutions (ignoring interactions)

**4. Dual Problem**
- Formulate dual optimization problem
- Dual optimal bounds primal optimal (LP duality)

---

# Chapter 3: Mechanics & Implementation — Classic Problems

## Problem 1: Traveling Salesman Problem (TSP)

### Problem Statement

**Given**: 
- N cities with distances between every pair
- Distance matrix: `dist[i][j]` = distance from city i to city j

**Find**: 
- Shortest tour visiting all cities exactly once and returning to start

**Example** (4 cities):
```
Distance Matrix:
     0   1   2   3
0 [  0  10  15  20 ]
1 [ 10   0  35  25 ]
2 [ 15  35   0  30 ]
3 [ 20  25  30   0 ]

Optimal Tour: 0 → 1 → 3 → 2 → 0
Total Distance: 10 + 25 + 30 + 15 = 80
```

### Mental Model

**State Representation**:
```
State {
    path: List of cities visited so far
    visited: Set of visited cities (for O(1) lookup)
    cost: Total distance traveled so far
    bound: Lower bound on completing this tour
}
```

**Branching**: From current city, branch to each unvisited city.

**Bounding Function** (MST-based lower bound):

**Intuition**: 
- Remaining tour must connect unvisited cities
- Minimum cost to do this ≥ MST cost of unvisited cities
- Add cost to return to start city

**Formula**:
```
bound = currentCost 
      + MST(unvisited cities) 
      + min edge from current to unvisited 
      + min edge from any unvisited to start
```

**Why it's admissible**: 
- MST is lower bound on connecting cities (tour ≥ MST)
- Adding minimum edges gives optimistic estimate

### Implementation (C#)

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

public class TSPSolver
{
    private int n;  // Number of cities
    private int[,] dist;  // Distance matrix
    private int bestCost;  // Best tour found so far
    private List<int> bestPath;  // Best tour path
    private int nodesExplored;  // For performance tracking
    
    // State in search tree
    private class State : IComparable<State>
    {
        public List<int> Path { get; set; }
        public HashSet<int> Visited { get; set; }
        public int Cost { get; set; }
        public int Bound { get; set; }
        
        public int CompareTo(State other)
        {
            // Priority queue: lower bound = higher priority
            return this.Bound.CompareTo(other.Bound);
        }
    }
    
    public (int cost, List<int> path, int nodesExplored) SolveTSP(int[,] distanceMatrix)
    {
        this.n = distanceMatrix.GetLength(0);
        this.dist = distanceMatrix;
        this.bestCost = int.MaxValue;
        this.bestPath = null;
        this.nodesExplored = 0;
        
        // Priority queue for best-first search
        var pq = new SortedSet<State>(new StateComparer());
        
        // Initial state: start at city 0
        var initialState = new State
        {
            Path = new List<int> { 0 },
            Visited = new HashSet<int> { 0 },
            Cost = 0,
            Bound = ComputeBound(new List<int> { 0 }, new HashSet<int> { 0 }, 0)
        };
        
        pq.Add(initialState);
        
        // Best-first search
        while (pq.Count > 0)
        {
            var current = pq.Min;
            pq.Remove(current);
            nodesExplored++;
            
            // Pruning: if bound exceeds best known solution, skip
            if (current.Bound >= bestCost)
            {
                continue;
            }
            
            // If all cities visited, complete the tour
            if (current.Path.Count == n)
            {
                int tourCost = current.Cost + dist[current.Path[n - 1], 0];
                
                if (tourCost < bestCost)
                {
                    bestCost = tourCost;
                    bestPath = new List<int>(current.Path);
                    bestPath.Add(0);  // Return to start
                }
                continue;
            }
            
            // Branch: try each unvisited city
            int currentCity = current.Path[current.Path.Count - 1];
            
            for (int nextCity = 0; nextCity < n; nextCity++)
            {
                if (current.Visited.Contains(nextCity))
                {
                    continue;
                }
                
                // Create new state
                var newPath = new List<int>(current.Path);
                newPath.Add(nextCity);
                
                var newVisited = new HashSet<int>(current.Visited);
                newVisited.Add(nextCity);
                
                int newCost = current.Cost + dist[currentCity, nextCity];
                
                // Compute bound for new state
                int newBound = ComputeBound(newPath, newVisited, newCost);
                
                // Pruning: only add if bound is promising
                if (newBound < bestCost)
                {
                    var newState = new State
                    {
                        Path = newPath,
                        Visited = newVisited,
                        Cost = newCost,
                        Bound = newBound
                    };
                    
                    pq.Add(newState);
                }
            }
        }
        
        return (bestCost, bestPath, nodesExplored);
    }
    
    // Compute MST-based lower bound
    private int ComputeBound(List<int> path, HashSet<int> visited, int currentCost)
    {
        if (path.Count == n)
        {
            // All cities visited, just add return cost
            return currentCost + dist[path[n - 1], 0];
        }
        
        // 1. Start with current cost
        int bound = currentCost;
        
        // 2. Add MST cost of unvisited cities
        var unvisited = Enumerable.Range(0, n)
            .Where(i => !visited.Contains(i))
            .ToList();
        
        if (unvisited.Count > 0)
        {
            bound += ComputeMST(unvisited);
            
            // 3. Add minimum edge from current city to any unvisited
            int currentCity = path[path.Count - 1];
            int minToCurrent = int.MaxValue;
            
            foreach (int city in unvisited)
            {
                minToCurrent = Math.Min(minToCurrent, dist[currentCity, city]);
            }
            
            bound += minToCurrent;
            
            // 4. Add minimum edge from any unvisited city back to start
            int minToStart = int.MaxValue;
            
            foreach (int city in unvisited)
            {
                minToStart = Math.Min(minToStart, dist[city, 0]);
            }
            
            bound += minToStart;
        }
        else
        {
            // Only need to return to start
            bound += dist[path[path.Count - 1], 0];
        }
        
        return bound;
    }
    
    // Compute MST cost using Prim's algorithm
    private int ComputeMST(List<int> cities)
    {
        if (cities.Count <= 1)
        {
            return 0;
        }
        
        int mstCost = 0;
        var inMST = new HashSet<int>();
        var minCost = new Dictionary<int, int>();
        
        // Initialize
        foreach (int city in cities)
        {
            minCost[city] = int.MaxValue;
        }
        
        // Start with first city
        int startCity = cities[0];
        minCost[startCity] = 0;
        
        // Prim's algorithm
        for (int i = 0; i < cities.Count; i++)
        {
            // Find minimum cost city not in MST
            int minCity = -1;
            int minValue = int.MaxValue;
            
            foreach (int city in cities)
            {
                if (!inMST.Contains(city) && minCost[city] < minValue)
                {
                    minValue = minCost[city];
                    minCity = city;
                }
            }
            
            // Add to MST
            inMST.Add(minCity);
            mstCost += minValue;
            
            // Update costs to other cities
            foreach (int city in cities)
            {
                if (!inMST.Contains(city))
                {
                    minCost[city] = Math.Min(minCost[city], dist[minCity, city]);
                }
            }
        }
        
        return mstCost;
    }
    
    // Custom comparer for priority queue (handles ties)
    private class StateComparer : IComparer<State>
    {
        public int Compare(State x, State y)
        {
            int cmp = x.Bound.CompareTo(y.Bound);
            
            if (cmp == 0)
            {
                // Break ties by cost (prefer lower cost)
                cmp = x.Cost.CompareTo(y.Cost);
            }
            
            if (cmp == 0)
            {
                // Break ties by path length (prefer longer paths - closer to solution)
                cmp = y.Path.Count.CompareTo(x.Path.Count);
            }
            
            if (cmp == 0)
            {
                // Final tie-breaker: arbitrary ordering to allow duplicates
                cmp = x.GetHashCode().CompareTo(y.GetHashCode());
            }
            
            return cmp;
        }
    }
}

// Usage Example
class Program
{
    static void Main()
    {
        int[,] dist = new int[,]
        {
            {  0, 10, 15, 20 },
            { 10,  0, 35, 25 },
            { 15, 35,  0, 30 },
            { 20, 25, 30,  0 }
        };
        
        var solver = new TSPSolver();
        var (cost, path, nodes) = solver.SolveTSP(dist);
        
        Console.WriteLine($"Optimal Tour Cost: {cost}");
        Console.WriteLine($"Optimal Path: {string.Join(" → ", path)}");
        Console.WriteLine($"Nodes Explored: {nodes}");
        
        // Compare with exhaustive search
        int totalTours = Factorial(dist.GetLength(0) - 1) / 2;
        Console.WriteLine($"Total Possible Tours: {totalTours}");
        Console.WriteLine($"Pruning Ratio: {(1 - (double)nodes / totalTours) * 100:F1}%");
    }
    
    static int Factorial(int n)
    {
        int result = 1;
        for (int i = 2; i <= n; i++)
        {
            result *= i;
        }
        return result;
    }
}
```

### Detailed Execution Trace (4 Cities)

```
┌─────────────────────────────────────────────────────────────┐
│        TSP Branch & Bound Execution (4 cities)              │
└─────────────────────────────────────────────────────────────┘

Distance Matrix:
     0   1   2   3
0 [  0  10  15  20 ]
1 [ 10   0  35  25 ]
2 [ 15  35   0  30 ]
3 [ 20  25  30   0 ]

STEP 1: Initialize
Priority Queue: [(Path:[0], Cost:0, Bound:60)]
BestSoFar: ∞

STEP 2: Expand [0]
Current: [0], Cost:0, Bound:60
Branch to: 1, 2, 3

New States:
- [0,1]: Cost=10, Bound=10+MST(2,3)+min(1→{2,3})+min({2,3}→0)
        = 10 + 30 + 25 + 15 = 80
- [0,2]: Cost=15, Bound=15+30+15+10 = 70
- [0,3]: Cost=20, Bound=20+30+20+10 = 80

Priority Queue: [(Path:[0,2], Bound:70), (Path:[0,1], Bound:80), (Path:[0,3], Bound:80)]
BestSoFar: ∞

STEP 3: Expand [0,2] (best bound)
Current: [0,2], Cost:15, Bound:70
Branch to: 1, 3

New States:
- [0,2,1]: Cost=15+35=50, Bound=50+dist(1,3)+dist(3,0) = 50+25+20 = 95
- [0,2,3]: Cost=15+30=45, Bound=45+dist(3,1)+dist(1,0) = 45+25+10 = 80

Priority Queue: [(Path:[0,1], Bound:80), (Path:[0,3], Bound:80), 
                 (Path:[0,2,3], Bound:80), (Path:[0,2,1], Bound:95)]
BestSoFar: ∞

STEP 4: Expand [0,1] (same bound, but chosen)
Current: [0,1], Cost:10, Bound:80
Branch to: 2, 3

New States:
- [0,1,2]: Cost=10+35=45, Bound=45+30+15 = 90
- [0,1,3]: Cost=10+25=35, Bound=35+30+15 = 80

Priority Queue: [(Path:[0,3], Bound:80), (Path:[0,2,3], Bound:80), 
                 (Path:[0,1,3], Bound:80), (Path:[0,1,2], Bound:90), 
                 (Path:[0,2,1], Bound:95)]
BestSoFar: ∞

STEP 5: Expand [0,3] (ties broken by implementation)
Current: [0,3], Cost:20, Bound:80
Branch to: 1, 2

New States:
- [0,3,1]: Cost=20+25=45, Bound=45+35+15 = 95
- [0,3,2]: Cost=20+30=50, Bound=50+35+10 = 95

Priority Queue: [(Path:[0,2,3], Bound:80), (Path:[0,1,3], Bound:80), 
                 (Path:[0,1,2], Bound:90), (Path:[0,2,1], Bound:95), 
                 (Path:[0,3,1], Bound:95), (Path:[0,3,2], Bound:95)]
BestSoFar: ∞

STEP 6: Expand [0,2,3]
Current: [0,2,3], Cost:45, Bound:80
Branch to: 1

New State:
- [0,2,3,1]: Cost=45+25=70, Bound=70+dist(1,0) = 70+10 = 80
  COMPLETE TOUR: 0→2→3→1→0, Total: 70+10 = 80

Priority Queue: [(Path:[0,1,3], Bound:80), (Path:[0,2,3,1], Bound:80), 
                 (Path:[0,1,2], Bound:90), ...]
BestSoFar: 80 ✓

STEP 7: Expand [0,1,3]
Current: [0,1,3], Cost:35, Bound:80
Branch to: 2

New State:
- [0,1,3,2]: Cost=35+30=65, Bound=65+dist(2,0) = 65+15 = 80
  COMPLETE TOUR: 0→1→3→2→0, Total: 65+15 = 80

BestSoFar: 80 (no improvement)

STEP 8: Expand [0,2,3,1]
Already complete, skip.

STEP 9: Process remaining states
All remaining states have Bound ≥ 80, PRUNE!

FINAL RESULT:
Optimal Tour: 0→1→3→2→0 (or 0→2→3→1→0, both cost 80)
Nodes Explored: ~8-10 (vs. 12 exhaustive)
Pruning: ~20-30%
```

### Complexity Analysis

**Time Complexity**:
- **Worst case**: O((n-1)!) — explores all tours (no pruning)
- **Average case**: O(b^d) where b = branching factor (~n/2 with pruning), d = depth (n)
- **With good bounds**: Often O(n² × 2^n) — explores fraction of state space
- **Bound computation**: O(n²) per state (Prim's MST)

**Space Complexity**:
- **Priority queue**: O(n × n!) worst case, O(n × b^d) average
- **State storage**: O(n) per state
- **Total**: O(n² × b^d)

**Pruning Effectiveness**:

| Problem Size | Exhaustive Tours | Nodes Explored (B&B) | Pruning Ratio |
|--------------|------------------|----------------------|---------------|
| N=5 | 12 | 8-10 | 20-30% |
| N=8 | 2,520 | 150-300 | 88-94% |
| N=10 | 181,440 | 2,000-5,000 | 97-99% |
| N=15 | 43B | 500K-2M | 99.999% |

**Key Insight**: Pruning effectiveness increases exponentially with problem size.

---

## Problem 2: 0/1 Knapsack with Branch & Bound

### Problem Statement

**Given**:
- N items, each with weight `w[i]` and value `v[i]`
- Knapsack capacity W

**Find**: 
- Subset of items with maximum total value such that total weight ≤ W

**Example**:
```
Items:
ID  Weight  Value  Value/Weight
0     2      10      5.0
1     3      15      5.0
2     5      21      4.2
3     7      28      4.0

Capacity: 10

Optimal: Items {0, 1, 2}, Total Value = 10+15+21 = 46, Total Weight = 2+3+5 = 10
```

### Mental Model

**State Representation**:
```
State {
    level: Current item being considered (0 to n-1)
    weight: Total weight of items included so far
    value: Total value of items included so far
    bound: Upper bound on maximum value achievable from this state
}
```

**Branching**: Binary decision for each item (include or exclude)

**Bounding Function** (Fractional Knapsack Upper Bound):

**Intuition**:
- If we could take fractions of items, how much value could we get?
- Sort remaining items by value/weight ratio (greedy)
- Take items greedily until knapsack full
- This gives **upper bound** on any feasible solution

**Formula**:
```
bound = currentValue 
      + Σ(value of items taken completely)
      + (remaining capacity / weight of next item) × value of next item
```

**Why it's admissible** (for maximization):
- Fractional knapsack ≥ 0/1 knapsack (more relaxed)
- If bound ≤ bestSoFar, can't improve, prune

### Implementation (C#)

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

public class KnapsackBranchBound
{
    // Item structure
    private class Item
    {
        public int Index { get; set; }
        public int Weight { get; set; }
        public int Value { get; set; }
        public double Ratio { get; set; }  // Value/Weight
    }
    
    // State in search tree
    private class State : IComparable<State>
    {
        public int Level { get; set; }  // Item index being considered
        public int Weight { get; set; }  // Total weight so far
        public int Value { get; set; }   // Total value so far
        public double Bound { get; set; }  // Upper bound on value
        public List<bool> Included { get; set; }  // Which items included
        
        public int CompareTo(State other)
        {
            // Priority queue: higher bound = higher priority (maximization)
            return other.Bound.CompareTo(this.Bound);
        }
    }
    
    private List<Item> items;
    private int capacity;
    private int bestValue;
    private List<bool> bestSolution;
    private int nodesExplored;
    
    public (int value, List<int> items, int nodesExplored) Solve(int[] weights, int[] values, int capacity)
    {
        int n = weights.Length;
        this.capacity = capacity;
        this.bestValue = 0;
        this.bestSolution = new List<bool>(new bool[n]);
        this.nodesExplored = 0;
        
        // Create items and sort by value/weight ratio (descending)
        items = new List<Item>();
        for (int i = 0; i < n; i++)
        {
            items.Add(new Item
            {
                Index = i,
                Weight = weights[i],
                Value = values[i],
                Ratio = (double)values[i] / weights[i]
            });
        }
        
        items = items.OrderByDescending(item => item.Ratio).ToList();
        
        // Priority queue for best-first search
        var pq = new SortedSet<State>(new StateComparer());
        
        // Initial state
        var initialState = new State
        {
            Level = 0,
            Weight = 0,
            Value = 0,
            Bound = ComputeBound(0, 0, 0),
            Included = new List<bool>(new bool[n])
        };
        
        pq.Add(initialState);
        
        // Best-first search
        while (pq.Count > 0)
        {
            var current = pq.Min;
            pq.Remove(current);
            nodesExplored++;
            
            // Pruning: if bound doesn't exceed best, skip
            if (current.Bound <= bestValue)
            {
                continue;
            }
            
            // If all items considered
            if (current.Level >= n)
            {
                if (current.Value > bestValue)
                {
                    bestValue = current.Value;
                    bestSolution = new List<bool>(current.Included);
                }
                continue;
            }
            
            // Branch 1: Include current item (if fits)
            if (current.Weight + items[current.Level].Weight <= capacity)
            {
                var newIncluded = new List<bool>(current.Included);
                newIncluded[items[current.Level].Index] = true;
                
                int newWeight = current.Weight + items[current.Level].Weight;
                int newValue = current.Value + items[current.Level].Value;
                double newBound = ComputeBound(current.Level + 1, newWeight, newValue);
                
                if (newBound > bestValue)
                {
                    var includeState = new State
                    {
                        Level = current.Level + 1,
                        Weight = newWeight,
                        Value = newValue,
                        Bound = newBound,
                        Included = newIncluded
                    };
                    
                    pq.Add(includeState);
                    
                    // Update best if complete solution
                    if (newValue > bestValue && current.Level + 1 == n)
                    {
                        bestValue = newValue;
                        bestSolution = newIncluded;
                    }
                }
            }
            
            // Branch 2: Exclude current item
            var excludeIncluded = new List<bool>(current.Included);
            excludeIncluded[items[current.Level].Index] = false;
            
            double excludeBound = ComputeBound(current.Level + 1, current.Weight, current.Value);
            
            if (excludeBound > bestValue)
            {
                var excludeState = new State
                {
                    Level = current.Level + 1,
                    Weight = current.Weight,
                    Value = current.Value,
                    Bound = excludeBound,
                    Included = excludeIncluded
                };
                
                pq.Add(excludeState);
            }
        }
        
        // Extract solution items
        var solutionItems = new List<int>();
        for (int i = 0; i < n; i++)
        {
            if (bestSolution[i])
            {
                solutionItems.Add(i);
            }
        }
        
        return (bestValue, solutionItems, nodesExplored);
    }
    
    // Compute fractional knapsack upper bound
    private double ComputeBound(int level, int weight, int value)
    {
        if (weight >= capacity)
        {
            return 0;  // Infeasible
        }
        
        double bound = value;
        int remainingCapacity = capacity - weight;
        
        // Add items greedily (already sorted by ratio)
        for (int i = level; i < items.Count && remainingCapacity > 0; i++)
        {
            if (items[i].Weight <= remainingCapacity)
            {
                // Take entire item
                bound += items[i].Value;
                remainingCapacity -= items[i].Weight;
            }
            else
            {
                // Take fraction of item
                bound += remainingCapacity * items[i].Ratio;
                break;
            }
        }
        
        return bound;
    }
    
    // Custom comparer for priority queue
    private class StateComparer : IComparer<State>
    {
        public int Compare(State x, State y)
        {
            int cmp = y.Bound.CompareTo(x.Bound);  // Higher bound first
            
            if (cmp == 0)
            {
                cmp = y.Value.CompareTo(x.Value);  // Break ties by value
            }
            
            if (cmp == 0)
            {
                cmp = x.Level.CompareTo(y.Level);  // Break ties by level
            }
            
            if (cmp == 0)
            {
                cmp = x.GetHashCode().CompareTo(y.GetHashCode());
            }
            
            return cmp;
        }
    }
}

// Usage Example
class Program
{
    static void Main()
    {
        int[] weights = { 2, 3, 5, 7 };
        int[] values = { 10, 15, 21, 28 };
        int capacity = 10;
        
        var solver = new KnapsackBranchBound();
        var (value, items, nodes) = solver.Solve(weights, values, capacity);
        
        Console.WriteLine($"Maximum Value: {value}");
        Console.WriteLine($"Items Selected: {string.Join(", ", items)}");
        Console.WriteLine($"Nodes Explored: {nodes}");
        
        // Compare with exhaustive search
        int totalSubsets = (int)Math.Pow(2, weights.Length);
        Console.WriteLine($"Total Possible Subsets: {totalSubsets}");
        Console.WriteLine($"Pruning Ratio: {(1 - (double)nodes / totalSubsets) * 100:F1}%");
    }
}
```

### Visualization: Search Tree

```
┌─────────────────────────────────────────────────────────────┐
│         Knapsack Branch & Bound Tree (Capacity=10)          │
└─────────────────────────────────────────────────────────────┘

Items sorted by ratio: [0(w=2,v=10), 1(w=3,v=15), 2(w=5,v=21), 3(w=7,v=28)]

                           Root
                    (W=0, V=0, B=56.8)
                     /              \
              Include 0           Exclude 0
           (W=2,V=10,B=56.8)    (W=0,V=0,B=46.8)
              /        \            /        \
         Include 1  Exclude 1  Include 1  Exclude 1
       (W=5,V=25,B=56.8)  ...       ...        ...
          /        \
     Include 2  Exclude 2
   (W=10,V=46,B=46)  (W=5,V=25,B=51)
      LEAF!          /        \
   bestValue=46  Include 2  Exclude 2
              (W=10,V=46,B=46)  (W=5,V=25,B=43)
                 LEAF!         PRUNED (43≤46)
               no improvement

Legend:
W = Weight, V = Value, B = Bound
PRUNED = Bound ≤ bestValue, skip subtree
LEAF = All items considered, update best if better
```

**Key Observations**:
1. **Fractional bound**: Root bound (56.8) assumes we can take 70% of item 3
2. **Tight bound enables pruning**: When bound ≤ bestValue, prune aggressively
3. **Best-first finds optimal early**: First complete solution often close to optimal

### Complexity Analysis

**Time Complexity**:
- **Worst case**: O(2^n) — no pruning (degenerate case)
- **Average case**: O(n × 2^(n/2)) — prunes roughly half of tree
- **Bound computation**: O(n) per state
- **With tight bounds**: Often explores < 5% of state space

**Space Complexity**:
- **Priority queue**: O(2^n) worst case, O(n × 2^(n/2)) average
- **State storage**: O(n) per state

**Pruning Effectiveness**:

| Problem Size | Exhaustive Subsets | Nodes Explored (B&B) | Pruning Ratio |
|--------------|-------------------|----------------------|---------------|
| N=10 | 1,024 | 50-100 | 90-95% |
| N=20 | 1,048,576 | 5,000-10,000 | 99-99.5% |
| N=30 | 1,073,741,824 | 500K-1M | 99.95-99.99% |

---

## Problem 3: Assignment Problem (Job Scheduling)

### Problem Statement

**Given**:
- N workers and N jobs
- Cost matrix: `cost[i][j]` = cost of assigning worker i to job j

**Find**:
- Assignment of workers to jobs (one-to-one) minimizing total cost

**Example** (3 workers, 3 jobs):
```
Cost Matrix:
       Job0  Job1  Job2
Worker0 [ 9    2    7   ]
Worker1 [ 6    4    3   ]
Worker2 [ 5    8    1   ]

Optimal Assignment:
Worker0 → Job1 (cost 2)
Worker1 → Job2 (cost 3)
Worker2 → Job0 (cost 5)
Total Cost: 2 + 3 + 5 = 10
```

### Mental Model

**State Representation**:
```
State {
    workerLevel: Current worker being assigned (0 to n-1)
    assignedJobs: Set of jobs already assigned
    cost: Total cost of assignments so far
    bound: Lower bound on completing this assignment
}
```

**Branching**: For current worker, branch to each unassigned job.

**Bounding Function** (Row/Column Reduction):

**Intuition**:
- For each remaining worker, must assign to some remaining job
- Lower bound = current cost + minimum cost for each remaining worker among remaining jobs

**Formula**:
```
bound = currentCost 
      + Σ(min cost for each unassigned worker among unassigned jobs)
```

**Enhanced**: Use Hungarian algorithm preprocessing for tighter bound.

### Implementation (C#)

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

public class AssignmentProblemSolver
{
    private int n;
    private int[,] cost;
    private int bestCost;
    private int[] bestAssignment;
    private int nodesExplored;
    
    private class State : IComparable<State>
    {
        public int WorkerLevel { get; set; }
        public HashSet<int> AssignedJobs { get; set; }
        public int Cost { get; set; }
        public int Bound { get; set; }
        public int[] Assignment { get; set; }
        
        public int CompareTo(State other)
        {
            return this.Bound.CompareTo(other.Bound);  // Lower bound first
        }
    }
    
    public (int cost, int[] assignment, int nodesExplored) Solve(int[,] costMatrix)
    {
        this.n = costMatrix.GetLength(0);
        this.cost = costMatrix;
        this.bestCost = int.MaxValue;
        this.bestAssignment = null;
        this.nodesExplored = 0;
        
        var pq = new SortedSet<State>(new StateComparer());
        
        var initialState = new State
        {
            WorkerLevel = 0,
            AssignedJobs = new HashSet<int>(),
            Cost = 0,
            Bound = ComputeBound(0, new HashSet<int>(), 0),
            Assignment = new int[n]
        };
        
        pq.Add(initialState);
        
        while (pq.Count > 0)
        {
            var current = pq.Min;
            pq.Remove(current);
            nodesExplored++;
            
            // Pruning
            if (current.Bound >= bestCost)
            {
                continue;
            }
            
            // Complete assignment
            if (current.WorkerLevel >= n)
            {
                if (current.Cost < bestCost)
                {
                    bestCost = current.Cost;
                    bestAssignment = (int[])current.Assignment.Clone();
                }
                continue;
            }
            
            // Branch: assign current worker to each unassigned job
            int worker = current.WorkerLevel;
            
            for (int job = 0; job < n; job++)
            {
                if (current.AssignedJobs.Contains(job))
                {
                    continue;
                }
                
                var newAssigned = new HashSet<int>(current.AssignedJobs);
                newAssigned.Add(job);
                
                int newCost = current.Cost + cost[worker, job];
                int newBound = ComputeBound(worker + 1, newAssigned, newCost);
                
                if (newBound < bestCost)
                {
                    var newAssignment = (int[])current.Assignment.Clone();
                    newAssignment[worker] = job;
                    
                    var newState = new State
                    {
                        WorkerLevel = worker + 1,
                        AssignedJobs = newAssigned,
                        Cost = newCost,
                        Bound = newBound,
                        Assignment = newAssignment
                    };
                    
                    pq.Add(newState);
                }
            }
        }
        
        return (bestCost, bestAssignment, nodesExplored);
    }
    
    private int ComputeBound(int workerLevel, HashSet<int> assignedJobs, int currentCost)
    {
        int bound = currentCost;
        
        // For each remaining worker, add minimum cost among remaining jobs
        for (int worker = workerLevel; worker < n; worker++)
        {
            int minCost = int.MaxValue;
            
            for (int job = 0; job < n; job++)
            {
                if (!assignedJobs.Contains(job))
                {
                    minCost = Math.Min(minCost, cost[worker, job]);
                }
            }
            
            bound += minCost;
        }
        
        return bound;
    }
    
    private class StateComparer : IComparer<State>
    {
        public int Compare(State x, State y)
        {
            int cmp = x.Bound.CompareTo(y.Bound);
            
            if (cmp == 0)
            {
                cmp = x.Cost.CompareTo(y.Cost);
            }
            
            if (cmp == 0)
            {
                cmp = y.WorkerLevel.CompareTo(x.WorkerLevel);
            }
            
            if (cmp == 0)
            {
                cmp = x.GetHashCode().CompareTo(y.GetHashCode());
            }
            
            return cmp;
        }
    }
}

// Usage Example
class Program
{
    static void Main()
    {
        int[,] cost = new int[,]
        {
            { 9, 2, 7 },
            { 6, 4, 3 },
            { 5, 8, 1 }
        };
        
        var solver = new AssignmentProblemSolver();
        var (totalCost, assignment, nodes) = solver.Solve(cost);
        
        Console.WriteLine($"Minimum Total Cost: {totalCost}");
        Console.WriteLine("Assignment:");
        for (int i = 0; i < assignment.Length; i++)
        {
            Console.WriteLine($"  Worker{i} → Job{assignment[i]} (cost: {cost[i, assignment[i]]})");
        }
        Console.WriteLine($"Nodes Explored: {nodes}");
    }
}
```

### Complexity Analysis

**Time Complexity**:
- **Worst case**: O(n!) — explores all permutations
- **With bounding**: O(n² × n!) → O(n² × b^n) where b < n (pruning factor)
- **Bound computation**: O(n²) per state

**Space Complexity**:
- **Priority queue**: O(n!)
- **State storage**: O(n) per state

**Pruning Effectiveness**:

| Problem Size | Exhaustive Assignments | Nodes Explored (B&B) | Pruning Ratio |
|--------------|------------------------|----------------------|---------------|
| N=5 | 120 | 30-50 | 60-75% |
| N=8 | 40,320 | 500-1,000 | 98-99% |
| N=10 | 3,628,800 | 5,000-10,000 | 99.7-99.9% |

---

# Chapter 4: Performance, Trade-offs & Real Systems

## Comparing Branch & Bound with Other Approaches

| Approach | TSP (N=15) | Knapsack (N=30) | Assignment (N=10) | When to Use |
|----------|-----------|-----------------|-------------------|-------------|
| **Exhaustive** | 87B nodes | 1B nodes | 3.6M nodes | N ≤ 10, guaranteed optimal |
| **Backtracking** | 87B nodes | 1B nodes | 3.6M nodes | Feasibility, not optimization |
| **Branch & Bound** | 500K-2M nodes | 500K-1M nodes | 5K-10K nodes | Medium N, need optimal |
| **Dynamic Programming** | 2^15 × 15 = 491K | 30 × W | Not applicable | Overlapping subproblems |
| **Greedy + Local Search** | < 1K nodes | < 1K nodes | < 1K nodes | Large N, approximate OK |

**Key Insights**:
1. **DP dominates when applicable**: Knapsack with small W → use DP
2. **B&B for NP-hard without DP structure**: TSP, Assignment
3. **Greedy for scale**: N > 100, tolerate 5-15% suboptimality

## Real-World Systems

### Story 1: UPS ORION (Route Optimization)

**System**: UPS's On-Road Integrated Optimization and Navigation system uses branch & bound for daily route planning.

**Problem**: 
- Optimize delivery routes for 55,000 drivers
- 100-200 stops per route
- Constraints: time windows, truck capacity, fuel, driver breaks

**Implementation**:
- **Core algorithm**: Branch & bound with custom bounds
- **Bounding**: Lower bound using 1-tree relaxation (like TSP MST)
- **Heuristic**: Initial solution from sweep algorithm
- **Optimization**: Solve each route in 2-5 minutes

**Impact**:
- Saves 100M miles/year
- $300-400M annual savings
- Reduces CO2 emissions by 100K tons/year

**Why B&B over alternatives**:
- Need near-optimal solutions (greedy leaves 10-15% on table)
- Time constraints (must solve in minutes)
- DP impractical (state space too large)

### Story 2: Google's AdWords Auction (Assignment Problem)

**System**: Google assigns ads to search query slots using assignment problem variant.

**Problem**:
- Match K ads to N slots on page
- Maximize total revenue (bids × click-through-rates)
- Constraints: quality scores, advertiser budgets, diversity

**Implementation**:
- **Core**: Branch & bound for optimal assignment
- **Scale trick**: Solve per-query (N ≤ 20), not globally
- **Bounding**: Linear programming relaxation
- **Timeout**: 50ms deadline → switch to greedy if needed

**Impact**:
- $150B+ annual revenue
- 3-7% revenue improvement over greedy
- Handles billions of queries/day

### Story 3: IBM's Job Shop Scheduling

**System**: IBM uses branch & bound for scheduling manufacturing jobs on machines.

**Problem**:
- N jobs, M machines
- Each job has sequence of operations on specific machines
- Minimize makespan (total completion time)

**Implementation**:
- **Branching**: Order jobs on each machine
- **Bounding**: Critical path lower bound + idle time
- **Pruning**: Dominance rules (never schedule job if another strictly better)

**Impact**:
- 20-30% reduction in makespan vs. greedy scheduling
- Saves millions in production downtime
- Handles 100-200 job problems in < 1 hour

---

# Chapter 5: Integration & Mastery

## When to Use Branch & Bound: Decision Framework

```
Problem Characteristics Checklist:

□ Need optimal solution (not just feasible)?
  ├─ No → Use backtracking or greedy
  └─ Yes → Continue

□ DP applicable (optimal substructure + overlapping subproblems)?
  ├─ Yes → Use DP (usually faster)
  └─ No → Continue

□ Problem size N?
  ├─ N ≤ 20 → B&B feasible, try it
  ├─ 20 < N ≤ 50 → B&B possible with tight bounds
  ├─ 50 < N ≤ 100 → B&B + heuristics (hybrid)
  └─ N > 100 → Use approximation algorithms (greedy, local search)

□ Can you design good bounding function?
  ├─ Yes (tight, fast) → B&B likely effective
  └─ No (loose, slow) → B&B may degrade to exhaustive

□ Time constraints?
  ├─ Can wait minutes/hours → Pure B&B
  ├─ Need answer in seconds → B&B with timeout → greedy
  └─ Real-time (milliseconds) → Skip B&B, use heuristics
```

## Designing Custom Bounding Functions

### Pattern 1: Relaxation

**Recipe**:
1. Identify hard constraints
2. Remove one or more constraints
3. Solve relaxed problem (should be polynomial time)
4. Use relaxed solution as bound

**Examples**:
- **0/1 Knapsack** → Fractional Knapsack (remove integer constraint)
- **TSP** → MST (remove tour requirement, allow tree)
- **Integer Linear Programming** → Linear Programming (remove integer constraint)

### Pattern 2: Decomposition

**Recipe**:
1. Break problem into independent subproblems
2. Solve each subproblem optimally (ignoring interactions)
3. Sum solutions

**Examples**:
- **Multi-machine scheduling** → Solve per-machine (ignore dependencies)
- **Multi-depot vehicle routing** → Solve per-depot (ignore cross-depot interactions)

### Pattern 3: Dual Bounds

**Recipe**:
1. Formulate dual optimization problem
2. Solve dual (often easier)
3. Use dual optimal as primal bound (LP duality theory)

**Requires**: Problem formulated as linear or convex program.

### Pattern 4: Greedy Heuristic

**Recipe**:
1. Apply fast greedy algorithm
2. Use greedy solution value as bound

**Note**: Gives feasible solution (useful as initial best), but may be loose bound.

## Advanced Techniques

### 1. Parallel Branch & Bound

**Strategy**: Distribute state space exploration across multiple processors.

**Challenges**:
- **Load balancing**: Some branches finish quickly, others slowly
- **Shared best solution**: Must synchronize updates to global best
- **Priority queue distribution**: Maintain global priority order

**Implementation Pattern**:
```
Master-Worker Architecture:
- Master maintains priority queue
- Workers request states, explore, report results
- Periodically broadcast improved bounds to prune workers' queues
```

### 2. Learning-Based Bounds

**Idea**: Use machine learning to predict bounds.

**Approach**:
1. Generate training data: (state features) → (actual optimal value from state)
2. Train regression model (random forest, neural network)
3. Use model predictions as bounds

**Advantage**: Can learn complex bound functions from data.

**Disadvantage**: Requires training data, may not be admissible (overfit).

### 3. Constraint Propagation

**Idea**: When branching, propagate implications of choice to reduce remaining choices.

**Example** (Assignment Problem):
```
Worker0 assigned to Job2
→ No other worker can be assigned to Job2 (constraint propagation)
→ Reduce state space for subsequent workers
```

**Impact**: Reduces branching factor, tighter state representation.

---

# 🧠 5 Cognitive Lenses

## 1. The Hardware Lens: Priority Queue Performance

**Priority Queue Choice Matters**:
- **Heap (Binary)**: O(log n) insert/extract, good for dynamic priorities
- **Fibonacci Heap**: O(1) amortized insert, O(log n) extract → better for B&B
- **Pairing Heap**: Simpler than Fibonacci, similar performance

**Bottleneck**: Priority queue operations dominate B&B runtime.

**Optimization**: Use batch processing—insert multiple states, then process batch.

## 2. The Trade-off Lens: Bound Tightness vs. Computation Cost

**Spectrum**:
```
Trivial Bound (cost=0, tightness=0%)
  → No pruning, degenerates to exhaustive

Simple Bound (cost=O(n), tightness=50-70%)
  → Moderate pruning, practical

Complex Bound (cost=O(n²), tightness=80-95%)
  → Aggressive pruning, often best overall

Exact Bound (cost=solve subproblem, tightness=100%)
  → Perfect pruning, but defeats purpose of B&B!
```

**Guideline**: Aim for O(n) or O(n log n) bound computation with 70-90% tightness.

## 3. The Learning Lens: Common Misconceptions

| Misconception | Reality |
|---------------|---------|
| **"B&B always finds optimal"** | Only if you explore entire tree or prove bounds eliminate alternatives. Can timeout and return best found (suboptimal). |
| **"Tighter bound always better"** | Not if computation cost exceeds pruning benefit. Profile to verify. |
| **"B&B is always slow"** | With good bounds, often 100-1000× faster than exhaustive. Can be practical up to N=30-50. |
| **"Priority queue order doesn't matter"** | Best-first finds optimal earlier → more pruning. DFS uses less memory but may explore bad branches deeply. |

## 4. The AI/ML Lens: Connections to Modern AI

**Branch & Bound ↔ Monte Carlo Tree Search (MCTS)**:
- **MCTS** (used in AlphaGo): Builds search tree guided by value estimates
- **Similarity**: Both explore promising branches first
- **Difference**: MCTS uses stochastic rollouts, B&B uses deterministic bounds

**Branch & Bound ↔ Beam Search**:
- **Beam Search** (used in NLP): Keep top-K states at each level
- **Similarity**: Both prune based on scores
- **Difference**: Beam Search loses optimality guarantee

**Modern Integration**: AlphaGo Zero uses neural network to predict bounds for game tree search.

## 5. The Historical Lens: Evolution of Branch & Bound

**1960**: Land & Doig introduce B&B for integer programming  
**1970s**: Applied to TSP, knapsack, scheduling  
**1980s**: Parallel B&B on supercomputers  
**1990s**: Integration with constraint programming  
**2000s**: Branch-and-cut (B&B + cutting planes) dominates optimization  
**2010s**: GPU acceleration, learning-based bounds  
**2020s**: Quantum computing experiments for B&B

**Key Insight**: B&B hasn't been replaced—it's been enhanced with modern techniques (parallelism, learning, hybrid algorithms).

---

# 📚 Supplementary Outcomes

## Practice Problems (10)

| # | Problem | Source | Difficulty | Key Concept | Time Estimate |
|---|---------|--------|-----------|-------------|---------------|
| 1 | TSP with 8 cities | Classic | Hard | MST-based bounding | 60 min |
| 2 | 0/1 Knapsack (N=20, W=50) | Classic | Medium | Fractional bound | 45 min |
| 3 | Assignment problem (5×5) | Classic | Medium | Row/column reduction | 40 min |
| 4 | Job sequencing with deadlines | LC 1235 | Hard | Deadline-aware bounding | 50 min |
| 5 | Partition Equal Subset Sum (optimization variant) | LC 416 variant | Medium | Subset sum with B&B | 45 min |
| 6 | Graph coloring (minimize colors) | Classic | Hard | Chromatic bound | 60 min |
| 7 | Set cover problem | Classic | Hard | Greedy bound | 55 min |
| 8 | Hamiltonian path (minimize cost) | Classic | Hard | Path-based bounding | 50 min |
| 9 | Bin packing (minimize bins) | Classic | Hard | Lower bound via item sizes | 50 min |
| 10 | Multi-knapsack problem | Classic | Hard | Independent knapsack bounds | 60 min |

## Interview Questions (8)

1. **Q**: Explain the difference between backtracking and branch & bound. When would you use each?
   - **Follow-up**: Can you convert any backtracking algorithm to branch & bound? What changes?

2. **Q**: You're solving TSP with 12 cities. Your MST-based bound is pruning only 50% of branches. How would you improve the bounding function?
   - **Follow-up**: What if bound computation becomes too expensive?

3. **Q**: In 0/1 knapsack branch & bound, why do we sort items by value/weight ratio before starting?
   - **Follow-up**: What happens if items are not sorted?

4. **Q**: Design a branch & bound algorithm for scheduling N jobs on 2 machines minimizing total completion time. What's your bounding function?
   - **Follow-up**: How does it generalize to M machines?

5. **Q**: You have a branch & bound algorithm that explores 10M nodes for a 20-item problem. How would you reduce this?
   - **Follow-up**: Trade-offs between bound tightness and computation cost?

6. **Q**: Compare branch & bound with dynamic programming for the knapsack problem. When is each better?
   - **Follow-up**: Can you combine them? (Hint: yes, hybrid algorithms exist)

7. **Q**: Your branch & bound times out on large inputs. How do you convert it to an anytime algorithm that returns best-so-far?
   - **Follow-up**: How do you measure solution quality (optimality gap)?

8. **Q**: Explain how parallel branch & bound works. What are the synchronization challenges?
   - **Follow-up**: Design a work-stealing strategy for load balancing.

## Common Misconceptions (5)

| Misconception | Why It Seems Right | Reality | Memory Aid |
|---------------|-------------------|---------|------------|
| **"B&B is just backtracking with bounds"** | They look similar (tree exploration) | B&B targets optimization, backtracking targets feasibility. Different goals → different pruning logic. | B&B prunes suboptimal, backtracking prunes invalid |
| **"Priority queue always required"** | Best-first is most common | DFS and BFS also work. Best-first often best, but not mandatory. | Best-first = common, not required |
| **"Bound must be exact to prune correctly"** | Tighter seems better | Bound must be admissible, not exact. 80% tight often better than 100% tight if faster. | Admissible > Exact, Fast > Tight (within reason) |
| **"B&B always finds optimal within reasonable time"** | It's an exact algorithm | For large N or loose bounds, can degrade to exhaustive. Not a silver bullet. | B&B = smart exhaustive, still exponential worst-case |
| **"Bounding function is problem-specific magic"** | Each problem needs custom bound | Many use standard patterns: relaxation, decomposition, dual. Learn patterns, apply broadly. | 3 patterns cover 80% of bounds |

## Advanced Concepts (5)

### 1. Branch-and-Cut

**Idea**: Combine branch & bound with cutting planes (linear constraints that tighten feasible region).

**Process**:
1. Solve LP relaxation at each node
2. If solution fractional, add cutting plane (constraint that cuts off fractional solution but preserves integer solutions)
3. Resolve LP, repeat
4. If still fractional, branch

**Impact**: State-of-the-art for integer programming (CPLEX, Gurobi use this).

### 2. Column Generation

**Idea**: For problems with exponentially many variables, generate variables (columns) on-the-fly only when needed.

**Example**: Airline crew scheduling—one variable per crew schedule (exponentially many schedules).

**Integration with B&B**: Generate columns at each node of B&B tree.

### 3. Lagrangian Relaxation

**Idea**: Move hard constraints into objective function with penalty (Lagrange multipliers).

**Result**: Easier problem, solution provides bound.

**Example**: Knapsack with additional constraints → Move extra constraints to objective with penalties.

### 4. Dominance Rules

**Idea**: Identify states that are strictly worse than others, prune without bounding.

**Example** (TSP):
```
State A: Path [0,1,2], Cost=50
State B: Path [0,1,2], Cost=60
→ State B dominated by A (same partial tour, higher cost)
→ Prune B without computing bound
```

**Impact**: Faster pruning, reduces bound computation overhead.

### 5. Adaptive Bounding

**Idea**: Adjust bounding function during search based on pruning effectiveness.

**Strategy**:
- If bound pruning < 50% of generated states → switch to tighter (more expensive) bound
- If bound pruning > 95% → switch to looser (faster) bound

**Implementation**: Monitor pruning ratio every N states, adjust dynamically.

---

## External Resources

- **Book**: *Integer Programming* by Wolsey — Chapter on Branch & Bound  
  **Why**: Comprehensive treatment of B&B for optimization, with proofs and complexity analysis.

- **Paper**: "Branch-and-Bound Methods: A Survey" (Lawler & Wood, 1966)  
  **Why**: Classic paper introducing the framework, historical perspective.

- **Tool**: Google OR-Tools (CP-SAT, MIP solvers)  
  **Why**: Production-grade B&B implementation. Study source code for industrial techniques.

- **Course**: MIT 15.053 (Optimization Methods) — Lectures on B&B  
  **Why**: Theoretical foundations, covers bounding function design rigorously.

- **Visualization**: B&B Visualizer (http://www.dsalgorithms.com)  
  **Why**: Interactive visualization of TSP and knapsack B&B trees.

---

**End of Week 13, Day 03 Instructional File**


---

## Quick Self-Check

Before moving to Day 4 (Amortized Analysis), ensure you can:

- [ ] Explain the difference between backtracking (feasibility) and branch & bound (optimization)
- [ ] Design bounding functions using relaxation, decomposition, or greedy heuristics
- [ ] Implement branch & bound for TSP with MST-based bounds
- [ ] Implement branch & bound for 0/1 knapsack with fractional bounds
- [ ] Implement branch & bound for assignment problem with row-reduction bounds
- [ ] Choose between best-first, depth-first, and breadth-first search strategies
- [ ] Analyze pruning effectiveness (nodes explored vs. exhaustive)
- [ ] Recognize when branch & bound is practical vs. when to use approximations

**Challenges to Test Mastery**:
1. Solve TSP for N=10 cities with custom distance matrix in < 2 seconds
2. Implement knapsack B&B and compare nodes explored with DP approach
3. Design a bounding function for graph coloring problem (minimize chromatic number)
4. Modify TSP B&B to find k-best tours (not just optimal)

If you can complete 3/4 challenges, you're ready for **Day 4: Amortized Analysis** (understanding average-case complexity over operation sequences).

---

**Retention Hook: The One-Liner**

> **"Branch & bound is backtracking with an oracle: bounding functions predict 'best possible from here,' enabling aggressive pruning of suboptimal branches before exploring them."**

---

**Next Steps**:
- Day 4 covers amortized analysis (dynamic arrays, splay trees, Fibonacci heaps)
- Day 5 (optional) explores hybrid approaches combining multiple paradigms
- Week 14 shifts to string algorithms (pattern matching, tries, suffix structures)

**Congratulations on mastering branch & bound optimization!** 🎉
