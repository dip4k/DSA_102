# 📘 Week 13 Day 03: Branch and Bound — Engineering Guide

> 🧭 **Navigation:** [← Previous Day](file:///d:/Interview_prep/DSA_102/week_13_backtracking_and_branch_bound/Week_13_Day_02_Backtracking_Problems_Instructional.md) • [🏠 Week Overview](file:///d:/Interview_prep/DSA_102/week_13_backtracking_and_branch_bound/README.md) • [📘 Curriculum Syllabus](file:///d:/Interview_prep/DSA_102/COMPLETE_SYLLABUS.md) • [Next Day →](file:///d:/Interview_prep/DSA_102/week_13_backtracking_and_branch_bound/Week_13_Day_04_Amortized_Analysis_Instructional.md)
> 
> 💡 **Instructor Note:** *Backtracking searches for feasibility; Branch and Bound searches for optimality. Instead of blind DFS, Branch and Bound uses a heuristic bounding function and a Priority Queue to expand the most promising subproblems first.*

---

## 🎯 Learning Objectives

- ⚖️ **Distinguish** between backtracking (constraint feasibility) and Branch & Bound (discrete optimization).
- 📐 **Formulate** admissible bounding functions using problem relaxations (e.g., Fractional Knapsack for 0/1 Knapsack).
- 🚀 **Implement** Best-First Search Branch & Bound in C# (.NET 8/9) using `PriorityQueue<TElement, TPriority>` and Python (3.11+) using `heapq`.
- 🌲 **Visualize** search trees with explicit upper/lower bound pruning cutoffs.
- 🎙️ **Articulate** why Branch & Bound succeeds on problems where Dynamic Programming explodes due to large constraint capacities.

---

## 🧠 Chapter 1: The Branch & Bound Mental Model

### Feasibility vs. Optimization

```
BACKTRACKING:                    BRANCH & BOUND:
Goal: Find valid solutions       Goal: Find globally optimal solution (Min / Max)
Mechanism: DFS + Constraint prune Mechanism: Best-First Search (PQ) + Mathematical Bounds
Prune trigger: Invalid state     Prune trigger: Bound cannot beat current best known
```

### The Governing Bounding Invariant

For a **Maximization Problem** (e.g., Knapsack):
- Maintain `best_val`: the highest value of any complete feasible solution discovered so far.
- For each partial state `u`, calculate an **Upper Bound (`UB(u)`)**: the theoretical maximum value achievable by extending `u`.
- **Pruning Rule:** If `UB(u) <= best_val`, **PRUNE node `u` immediately**. No child in `u`'s subtree can ever beat `best_val`.

For a **Minimization Problem** (e.g., TSP):
- Maintain `best_cost`: the lowest cost of any valid tour found so far.
- Calculate a **Lower Bound (`LB(u)`)**.
- **Pruning Rule:** If `LB(u) >= best_cost`, **PRUNE node `u` immediately**.

```
                [Partial State Node u]
                      UB(u) = 85
                          │
          Is UB(u) <= BestKnown (90)?
                /             \
             YES               NO
              │                 │
      [X] PRUNE SUBTREE     Expand children into
      (Suboptimal bound)    Priority Queue
```

---

## 🌲 Chapter 2: Visualizing Best-First Search & Pruning

Consider a 0/1 Knapsack problem with capacity `W = 10`.

### ASCII Best-First Search Tree with Pruned Branches
```
                         Root (Level 0, Val: 0, Wt: 0)
                                   UB = 115
                        /                           \
           Take Item 1 (Val: 40, Wt: 4)     Skip Item 1 (Val: 0, Wt: 0)
                     UB = 115                         UB = 82
                    /        \                           │
        Take 2 (Val: 70, Wt: 7) Skip 2 (Val: 40, Wt: 4)  │
             UB = 110                  UB = 85           │
            /        \                     │             │
      Take 3 (Val: 100, Wt: 9) Skip 3      │             │
        Feasible Leaf         (Val: 70)    │             │
        Best = 100                         │             │
              │                            │             │
        Update Best: 100                   ▼             ▼
                                      [X] PRUNED    [X] PRUNED
                                    (UB 85 <= 100) (UB 82 <= 100)
-------------------------------------------------------------------------
[X] PRUNED: Upper bound cannot beat the current best feasible solution (100).
```

---

## ⚙️ Chapter 3: Canonical Problem — 0/1 Knapsack via Best-First B&B

### The Admissible Relaxation: Fractional Knapsack

To compute the upper bound `UB` of node `u`:
1. Greedily add remaining items in descending order of value-to-weight ratio (`val / wt`).
2. When the next item exceeds remaining capacity, take the **fraction** that fits.
3. Because the fractional solution is a mathematical relaxation of the discrete 0/1 constraint, `UB(u) >= OptimalValue(u)`.

### Node Definition

Pulled from [`Week_13_Extended_CSharp_Complete.md`](file:///d:/Interview_prep/DSA_102/week_13_backtracking_and_branch_bound/Week_13_Extended_CSharp_Complete.md) and [`Week_13_Extended_Python_Complete.md`](file:///d:/Interview_prep/DSA_102/week_13_backtracking_and_branch_bound/Week_13_Extended_Python_Complete.md):

#### C# (.NET 8/9) Implementation
```csharp
public sealed record Item(int Value, int Weight, double Ratio);

public sealed class KnapsackNode
{
    public int Level { get; init; }
    public int Value { get; init; }
    public int Weight { get; init; }
    public double Bound { get; init; }
}

public sealed class KnapsackBranchAndBound
{
    public static int SolveKnapsack(int capacity, int[] values, int[] weights)
    {
        int n = values.Length;
        var items = new Item[n];
        for (int i = 0; i < n; i++)
            items[i] = new Item(values[i], weights[i], (double)values[i] / weights[i]);

        // Sort descending by ratio
        Array.Sort(items, (a, b) => b.Ratio.CompareTo(a.Ratio));

        // PriorityQueue tracks max-bound first (using negative priority in .NET)
        var pq = new PriorityQueue<KnapsackNode, double>();

        var root = new KnapsackNode
        {
            Level = 0,
            Value = 0,
            Weight = 0,
            Bound = CalculateBound(0, 0, 0, capacity, items)
        };

        pq.Enqueue(root, -root.Bound);
        int maxProfit = 0;

        while (pq.Count > 0)
        {
            var curr = pq.Dequeue();

            // Pruning: bound cannot beat best known profit
            if (curr.Bound <= maxProfit || curr.Level == n)
                continue;

            var item = items[curr.Level];

            // Branch 1: INCLUDE current item (if capacity allows)
            if (curr.Weight + item.Weight <= capacity)
            {
                int nextVal = curr.Value + item.Value;
                int nextWt = curr.Weight + item.Weight;
                maxProfit = Math.Max(maxProfit, nextVal);

                double includeBound = CalculateBound(curr.Level + 1, nextVal, nextWt, capacity, items);
                if (includeBound > maxProfit)
                {
                    var includeNode = new KnapsackNode
                    {
                        Level = curr.Level + 1,
                        Value = nextVal,
                        Weight = nextWt,
                        Bound = includeBound
                    };
                    pq.Enqueue(includeNode, -includeBound);
                }
            }

            // Branch 2: EXCLUDE current item
            double excludeBound = CalculateBound(curr.Level + 1, curr.Value, curr.Weight, capacity, items);
            if (excludeBound > maxProfit)
            {
                var excludeNode = new KnapsackNode
                {
                    Level = curr.Level + 1,
                    Value = curr.Value,
                    Weight = curr.Weight,
                    Bound = excludeBound
                };
                pq.Enqueue(excludeNode, -excludeBound);
            }
        }

        return maxProfit;
    }

    private static double CalculateBound(int level, int val, int wt, int capacity, Item[] items)
    {
        if (wt >= capacity) return 0;
        double bound = val;
        int remainingCapacity = capacity - wt;

        for (int i = level; i < items.Length; i++)
        {
            if (items[i].Weight <= remainingCapacity)
            {
                remainingCapacity -= items[i].Weight;
                bound += items[i].Value;
            }
            else
            {
                bound += items[i].Ratio * remainingCapacity;
                break;
            }
        }
        return bound;
    }
}
```

#### Python (3.11+) Implementation
```python
import heapq
from dataclasses import dataclass

@dataclass
class Item:
    value: int
    weight: int
    ratio: float

@dataclass
class KnapsackNode:
    level: int
    value: int
    weight: int
    bound: float

def solve_knapsack_bb(capacity: int, values: list[int], weights: list[int]) -> int:
    n = len(values)
    items = [Item(v, w, v / w) for v, w in zip(values, weights)]
    items.sort(key=lambda x: x.ratio, reverse=True)

    def calculate_bound(level: int, val: int, wt: int) -> float:
        if wt >= capacity:
            return 0.0
        bound = float(val)
        rem = capacity - wt
        for i in range(level, n):
            if items[i].weight <= rem:
                rem -= items[i].weight
                bound += items[i].value
            else:
                bound += items[i].ratio * rem
                break
        return bound

    root_bound = calculate_bound(0, 0, 0)
    root = KnapsackNode(0, 0, 0, root_bound)

    # Max-heap via negative bound
    pq: list[tuple[float, int, KnapsackNode]] = [(-root_bound, 0, root)]
    entry_id = 1
    max_profit = 0

    while pq:
        _, _, curr = heapq.heappop(pq)

        if curr.bound <= max_profit or curr.level == n:
            continue

        item = items[curr.level]

        # Branch 1: INCLUDE item
        if curr.weight + item.weight <= capacity:
            nxt_val = curr.value + item.value
            nxt_wt = curr.weight + item.weight
            max_profit = max(max_profit, nxt_val)
            inc_bound = calculate_bound(curr.level + 1, nxt_val, nxt_wt)
            if inc_bound > max_profit:
                inc_node = KnapsackNode(curr.level + 1, nxt_val, nxt_wt, inc_bound)
                heapq.heappush(pq, (-inc_bound, entry_id, inc_node))
                entry_id += 1

        # Branch 2: EXCLUDE item
        exc_bound = calculate_bound(curr.level + 1, curr.value, curr.weight)
        if exc_bound > max_profit:
            exc_node = KnapsackNode(curr.level + 1, curr.value, curr.weight, exc_bound)
            heapq.heappush(pq, (-exc_bound, entry_id, exc_node))
            entry_id += 1

    return max_profit
```

---

## 📊 Chapter 4: Explicit Complexity Deconstruction

| Metric | Branch & Bound (Best-First) | Standard Backtracking | Dynamic Programming (0/1) |
| :--- | :--- | :--- | :--- |
| **Worst-Case Time** | `O(2^N)` | `O(2^N)` | `O(N * W)` (Pseudo-polynomial) |
| **Empirical Time** | Fast (prunes 90%+ of tree) | Slow (prunes only on capacity) | Predictable `O(N * W)` |
| **Space Complexity** | `O(2^N)` (PQ node storage) | `O(N)` (call stack depth) | `O(W)` (1D array optimization) |
| **When to Choose** | `W` is massive (`W = 10^9`, `N = 40`) | Need all feasible states | `W` is small (`W <= 10^5`, `N <= 1000`) |

### Key Trade-Off: Stack vs Queue Memory
- Standard Backtracking uses DFS: max memory is bounded strictly by tree depth `O(N)`.
- Branch and Bound uses Best-First Search: the Priority Queue may accumulate nodes across multiple active levels simultaneously, with worst-case memory `O(2^N)`. If memory limits are tight, **Depth-First Branch and Bound (DFBnB)** maintains `O(N)` memory while still pruning using the best known objective value.

---

## 🎙️ Chapter 5: 45-Minute Interview Verbal Script

### Step 1: Identifying Optimization Trade-Offs (00–05 min)
> *"This is a combinatorial optimization problem with capacity constraints. If capacity `W` were small, Dynamic Programming would run in pseudo-polynomial `O(N * W)` time. But if `W = 10^9`, DP fails due to memory exhaustion. A pure backtracking search runs in `O(2^N)` worst case. I will use Branch & Bound with Best-First Search to prune unpromising subtrees."*

### Step 2: Defining the Bounding Function (05–15 min)
> *"For Branch & Bound to guarantee global optimality, the upper bound must be admissible—meaning it never underestimates the maximum achievable profit. I will compute the relaxation using Fractional Knapsack by sorting items by `value / weight` ratio. Since any 0/1 assignment is a subset of fractional assignments, `UB` is an upper bound."*

### Step 3: Coding the Priority Queue Loop (15–30 min)
> *"I maintain a max-priority queue keyed by the upper bound. At each iteration, I pop the node with the highest theoretical bound. If its bound is less than or equal to our `maxProfit` so far, we prune it immediately. Otherwise, we branch into two children: include current item and exclude current item, calculating bounds for each before enqueuing."*

### Step 4: Verification & Edge Cases (30–45 min)
> *"Edge cases include `capacity = 0`, items heavier than capacity, and identical value-weight ratios. The algorithm terminates as soon as the queue is empty or the top bound falls below `maxProfit`. Average-case execution visits an infinitesimal fraction of the `2^N` state space."*

---

## ⚡ Quick Self-Check & Drill

1. **What happens if a bounding function is not admissible (underestimates optimal value in a max problem)?**
   *Answer:* The algorithm might prune the branch containing the true optimal solution, producing a suboptimal answer.
2. **Why does Best-First Search typically prune more nodes than Depth-First Search in Branch & Bound?**
   *Answer:* It immediately dives toward the most promising high-value leaves, establishing a high `best_profit` early, which allows aggressive pruning of remaining nodes.
3. **When is DP strictly superior to Branch & Bound?**
   *Answer:* When subproblems overlap heavily and the state space `N * W` is small and fits comfortably within memory.

---

> 🧭 **Navigation:** [← Previous Day](file:///d:/Interview_prep/DSA_102/week_13_backtracking_and_branch_bound/Week_13_Day_02_Backtracking_Problems_Instructional.md) • [🏠 Week Overview](file:///d:/Interview_prep/DSA_102/week_13_backtracking_and_branch_bound/README.md) • [📘 Curriculum Syllabus](file:///d:/Interview_prep/DSA_102/COMPLETE_SYLLABUS.md) • [Next Day →](file:///d:/Interview_prep/DSA_102/week_13_backtracking_and_branch_bound/Week_13_Day_04_Amortized_Analysis_Instructional.md)
