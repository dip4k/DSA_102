# 📘 WEEK 12 DAY 4: FRACTIONAL KNAPSACK & SCHEDULING — ENGINEERING GUIDE

> 🧭 **Navigation:** [← Previous Day](Week_12_Day_03_Huffman_Coding_And_Optimal_Trees_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_12_Day_05_Greedy_In_Systems_Instructional.md)
> 
> 💡 **Instructor Note:** *Today centers on two classic greedy paradigms: continuous optimization via density sorting (Fractional Knapsack) and deadline slot backward placement (Job Sequencing). Understand why item divisibility makes greedy optimal, whereas discrete items force dynamic programming.*

---

## 🎯 LEARNING OBJECTIVES

*By the end of this chapter, you will be able to:*

- 🎯 **Distinguish** between Fractional Knapsack (Greedy optimal) and 0/1 Knapsack (Greedy fails, DP required).
- ⚙️ **Implement** Fractional Knapsack and Job Sequencing with Deadlines in modern C# (.NET 8/9) and Python (3.11+).
- ⚖️ **Explain** why sorting by value-to-weight ratio (`Value / Weight`) guarantees optimality for divisible items.
- 🧠 **Prove** why placing jobs into the *latest available slot* before their deadline maximizes future flexibility.
- 🏭 **Connect** these scheduling algorithms to real systems: network bandwidth sharing and compute cluster spot pricing.

---

## 📖 CHAPTER 1: CONTEXT & MOTIVATION

### The Engineering Challenge

Imagine you are designing a resource manager for a high-performance network proxy. You have a fixed egress bandwidth cap of `W = 50 MB/s`. Multiple data streams compete for transmission:
- Stream 1: Real-time telemetry (`Value = 60`, `Bandwidth = 10 MB/s`).
- Stream 2: Video chunk (`Value = 100`, `Bandwidth = 20 MB/s`).
- Stream 3: Database snapshot (`Value = 120`, `Bandwidth = 30 MB/s`).

Network packets are **divisible**: you can transmit 100% of Stream 1, 100% of Stream 2, and 66.7% of Stream 3. This is the **Fractional Knapsack** problem.

Contrast this with shipping physical cargo containers or database migrations:
- You cannot ship 66% of a server or migrate 50% of an atomic transaction.
- Items are strictly binary (0 or 1).
- This is the **0/1 Knapsack** problem.

When decisions are continuous, greedy density sorting yields the provably optimal answer in `O(N log N)`. When decisions are discrete, greedy fails and we must pay the computational cost of Dynamic Programming (`O(N * W)`).

---

## 🧠 CHAPTER 2: THE GREEDY MENTAL MODEL

### Fractional Knapsack: Greedy by Value Density

The greedy criterion is **Value Density** (value per unit weight):
```
Density = Value / Weight
```

```
ASCII Density Bar Layout (Capacity W = 50):

Item 1: [Val: 60,  Wt: 10] -> Density = 6.0 |■■■■■■|
Item 2: [Val: 100, Wt: 20] -> Density = 5.0 |■■■■■|
Item 3: [Val: 120, Wt: 30] -> Density = 4.0 |■■■■|

Greedy Filling Timeline:
Remaining Capacity: 50
1. Take 100% of Item 1 (Wt 10): Capacity left = 40. Value = 60.
2. Take 100% of Item 2 (Wt 20): Capacity left = 20. Value = 60 + 100 = 160.
3. Take 20/30 (66.7%) of Item 3: Capacity left = 0. Value = 160 + (2/3 * 120) = 240.

Total Optimal Value = 240.0
```

### Why Greedy Fails for 0/1 Knapsack

In 0/1 Knapsack, you cannot slice Item 3. Let's trace the identical input with integer items:

```
Greedy by Ratio (0/1):
1. Take Item 1 (Ratio 6.0, Wt 10): Capacity left = 40, Value = 60.
2. Take Item 2 (Ratio 5.0, Wt 20): Capacity left = 20, Value = 160.
3. Item 3 (Wt 30) exceeds remaining capacity 20 -> SKIPPED!
Greedy Result: Value = 160, Weight Used = 30 (20 capacity wasted!).

Optimal DP Solution:
Take Item 2 (Wt 20) + Item 3 (Wt 30) = Wt 50 (100% capacity filled!).
Optimal Value = 100 + 120 = 220.

Conclusion:
220 (DP) > 160 (Greedy).
Greedy by ratio is strictly SUBOPTIMAL for 0/1 knapsack because of Capacity Fragmentation!
```

---

### Job Sequencing with Deadlines: The "Latest Slot" Strategy

**Problem Statement:**
Given `N` jobs where each job takes 1 unit of time and has a `Profit` and a `Deadline` (1-based slot). Only one job can execute per time slot. Maximize total profit.

**Greedy Insight:**
1. Jobs with the **highest profit** must be scheduled first.
2. **Where should a job be scheduled?** At its **latest possible available slot** `<= Deadline`.
   - Why not as early as possible? Scheduling early consumes slot 1, which blocks jobs that *must* execute at slot 1.
   - Pushing the job as late as possible preserves earlier slots for future jobs with urgent deadlines.

```
ASCII Slot Allocation Trace:
Jobs: A (Profit 100, d=2), B (Profit 19, d=1), C (Profit 27, d=2), D (Profit 25, d=1), E (Profit 15, d=3)

Sorted by Profit Descending:
1. A: Profit 100, Deadline 2
2. C: Profit 27,  Deadline 2
3. D: Profit 25,  Deadline 1
4. B: Profit 19,  Deadline 1
5. E: Profit 15,  Deadline 3

Time Slots: [ Slot 1 ] [ Slot 2 ] [ Slot 3 ]
Initial:    [   -    ] [   -    ] [   -    ]

Step 1: Job A (P=100, d=2). Latest free slot <= 2 is Slot 2.
        Slots: [ - ] [ A ] [ - ], Total Profit = 100

Step 2: Job C (P=27, d=2). Slot 2 occupied. Latest free slot <= 2 is Slot 1.
        Slots: [ C ] [ A ] [ - ], Total Profit = 100 + 27 = 127

Step 3: Job D (P=25, d=1). Slot 1 occupied. No slot available -> Skip D.

Step 4: Job B (P=19, d=1). Slot 1 occupied. No slot available -> Skip B.

Step 5: Job E (P=15, d=3). Latest free slot <= 3 is Slot 3.
        Slots: [ C ] [ A ] [ E ], Total Profit = 127 + 15 = 142

Final Schedule: Slot 1: C, Slot 2: A, Slot 3: E -> Total Profit = 142
```

---

## 🔬 CHAPTER 3: SIMPLIFIED EXCHANGE PROOF

```
Exchange Proof for Fractional Knapsack:
1. Let G be the greedy solution sorted by density r_i = v_i / w_i.
2. Let OPT be an optimal solution that differs from G.
3. Because OPT differs from G, there must exist an item 'a' where G took more fraction
   than OPT, and an item 'b' where OPT took more fraction than G.
4. By greedy definition: Density(a) >= Density(b).
5. We can take an incremental weight epsilon from item 'b' in OPT and replace it with
   weight epsilon of item 'a'.
   - Capacity constraint remains exactly satisfied.
   - Change in value = epsilon * (Density(a) - Density(b)) >= 0.
6. The modified solution OPT' is at least as valuable as OPT.
7. Continuing this exchange transforms OPT into G without ever reducing total value.
   Therefore, the greedy density choice is globally optimal.
```

---

## ⚙️ CHAPTER 4: PRODUCTION IMPLEMENTATIONS

### Pattern 1: Fractional Knapsack

#### Production C# (.NET 8/9)

```csharp
namespace DsaMastery.Greedy;

using System;
using System.Collections.Generic;

public readonly record struct KnapsackItem(double Value, double Weight, string Id)
{
    public double Density => Weight > 0 ? Value / Weight : 0;
}

public static class FractionalKnapsack
{
    /// <summary>
    /// Computes the maximum value achievable with fractional items.
    /// Time Complexity: O(N log N)
    /// Space Complexity: O(1) auxiliary (in-place sort on Array/Span)
    /// </summary>
    public static double Solve(KnapsackItem[] items, double capacity)
    {
        ArgumentNullException.ThrowIfNull(items);
        if (capacity <= 0 || items.Length == 0) return 0.0;

        // Sort items by density descending
        Array.Sort(items, static (a, b) => b.Density.CompareTo(a.Density));

        double totalValue = 0.0;
        double remainingCapacity = capacity;

        foreach (var item in items)
        {
            if (remainingCapacity <= 0) break;

            if (item.Weight <= remainingCapacity)
            {
                // Take 100% of the item
                remainingCapacity -= item.Weight;
                totalValue += item.Value;
            }
            else
            {
                // Take fraction that exhausts remaining capacity
                double fraction = remainingCapacity / item.Weight;
                totalValue += item.Value * fraction;
                remainingCapacity = 0;
            }
        }

        return totalValue;
    }
}
```

#### Production Python (3.11+)

```python
from dataclasses import dataclass
from typing import List, Tuple

@dataclass
class KnapsackItem:
    value: float
    weight: float
    item_id: str

    @property
    def density(self) -> float:
        return self.value / self.weight if self.weight > 0 else 0.0


def fractional_knapsack(items: List[KnapsackItem], capacity: float) -> Tuple[float, List[Tuple[str, float]]]:
    """
    Computes optimal fractional knapsack value and item fractions taken.
    Time Complexity: O(N log N)
    Space Complexity: O(N)
    """
    if capacity <= 0 or not items:
        return 0.0, []

    # Sort descending by value density
    sorted_items = sorted(items, key=lambda x: x.density, reverse=True)

    total_value = 0.0
    remaining_capacity = capacity
    fractions_taken: List[Tuple[str, float]] = []

    for item in sorted_items:
        if remaining_capacity <= 0:
            break

        if item.weight <= remaining_capacity:
            # Take entire item
            remaining_capacity -= item.weight
            total_value += item.value
            fractions_taken.append((item.item_id, 1.0))
        else:
            # Take fraction
            fraction = remaining_capacity / item.weight
            total_value += item.value * fraction
            fractions_taken.append((item.item_id, fraction))
            remaining_capacity = 0.0

    return total_value, fractions_taken
```

---

### Pattern 2: Job Sequencing with Deadlines

#### Production C# (.NET 8/9)

```csharp
namespace DsaMastery.Greedy;

using System;
using System.Collections.Generic;
using System.Linq;

public readonly record struct Job(string Id, int Profit, int Deadline);

public sealed record JobScheduleResult(int TotalProfit, IReadOnlyList<string?> Schedule);

public static class JobScheduler
{
    /// <summary>
    /// Greedily schedules jobs into latest available deadline slots.
    /// Time Complexity: O(N log N + N * max_deadline)
    /// Space Complexity: O(max_deadline)
    /// </summary>
    public static JobScheduleResult Schedule(Job[] jobs)
    {
        ArgumentNullException.ThrowIfNull(jobs);
        if (jobs.Length == 0) return new JobScheduleResult(0, []);

        // Sort by profit descending
        Array.Sort(jobs, static (a, b) => b.Profit.CompareTo(a.Profit));

        int maxDeadline = jobs.Max(j => j.Deadline);
        var slots = new string?[maxDeadline + 1]; // 1-based indexing for slots
        int totalProfit = 0;

        foreach (var job in jobs)
        {
            // Search backwards from job's deadline for latest free slot
            for (int t = job.Deadline; t >= 1; t--)
            {
                if (slots[t] is null)
                {
                    slots[t] = job.Id;
                    totalProfit += job.Profit;
                    break;
                }
            }
        }

        // Return scheduled jobs from slot 1 to maxDeadline
        var resultSlots = new string?[maxDeadline];
        Array.Copy(slots, 1, resultSlots, 0, maxDeadline);

        return new JobScheduleResult(totalProfit, resultSlots);
    }
}
```

#### Production Python (3.11+)

```python
from typing import List, Tuple, Optional

def schedule_jobs_with_deadlines(jobs: List[Tuple[str, int, int]]) -> Tuple[int, List[Optional[str]]]:
    """
    Schedules jobs with deadlines to maximize profit.
    Input jobs: list of (job_id, profit, deadline)
    Time Complexity: O(N log N + N * max_deadline)
    Space Complexity: O(max_deadline)
    """
    if not jobs:
        return 0, []

    # Sort descending by profit
    sorted_jobs = sorted(jobs, key=lambda x: x[1], reverse=True)

    max_deadline = max(job[2] for job in sorted_jobs)
    # 1-based indexing: slots[0] unused, slots[1..max_deadline]
    slots: List[Optional[str]] = [None] * (max_deadline + 1)
    total_profit = 0

    for job_id, profit, deadline in sorted_jobs:
        # Search backward from deadline for latest free slot
        for t in range(deadline, 0, -1):
            if slots[t] is None:
                slots[t] = job_id
                total_profit += profit
                break

    return total_profit, slots[1:]
```

---

## ⚖️ CHAPTER 5: PERFORMANCE & COMPLEXITY DECONSTRUCTION

### Complexity Comparison

| Algorithm | Time Complexity | Auxiliary Space | Bottleneck | Optimization Potential |
| :--- | :--- | :--- | :--- | :--- |
| **Fractional Knapsack** | `O(N log N)` | `O(1)` (in-place) | Sorting items by ratio | Quickselect median `O(N)` |
| **0/1 Knapsack (DP)** | `O(N * W)` | `O(W)` | Pseudo-polynomial DP table | Branch and Bound |
| **Job Sequencing (Naive)** | `O(N log N + N * D)` | `O(D)` | Backward slot linear scan | DSU slot lookup |
| **Job Sequencing (DSU)** | `O(N log N + N * α(D))`| `O(D)` | Initial sort | Disjoint Set Union `O(α(D))` |

#### The Disjoint Set Union (DSU) Optimization for Job Sequencing:
When max deadline `D` is large (e.g., `D = 10^5`), scanning backwards linearly takes `O(N * D) = 10^10` operations (TLE).
- We can initialize a DSU where each slot points to itself.
- When slot `t` is filled, we perform `Union(t, t - 1)`.
- `Find(deadline)` instantly returns the latest available free slot in amortized `O(α(D))` time!

---

## 🎙️ CHAPTER 6: 45-MINUTE INTERVIEW VERBAL SCRIPT

```
[00:00 - 05:00] Clarifying Knapsack & Scheduling Semantics
"Let's clarify the rules:
 1. For Knapsack: Can items be divided fractionally?
    If yes, greedy by value/weight ratio is provably optimal.
    If items are discrete (0/1), greedy fails and we need Dynamic Programming.
 2. For Job Scheduling: Does each job take exactly 1 unit of time?
    Are deadlines 1-based integers?
    The goal is to maximize total profit under deadline constraints."

[05:00 - 12:00] Explaining the Greedy Intuition
"For Fractional Knapsack, our metric is value density: value per unit weight.
 Because items can be split arbitrarily, every unit of capacity should be spent on the
 highest available density. Capacity fragmentation does not exist.
 
 For Job Sequencing, we sort by profit descending.
 For each job, we place it in the latest available slot <= deadline.
 Why the latest slot? If a job is due at time 3, placing it at time 1 blocks urgent jobs
 that are due at time 1. Placing it at time 3 leaves times 1 and 2 open.
 This maximizes future flexibility."

[12:00 - 25:00] Walkthrough with Concrete Trace
"Let's trace: Item 1 (60, 10), Item 2 (100, 20), Item 3 (120, 30), Capacity 50.
 Densities are 6.0, 5.0, 4.0.
 We take 100% of Item 1 (wt 10, val 60).
 We take 100% of Item 2 (wt 20, val 100).
 We take 20/30 of Item 3 (wt 20, val 80). Total value = 240.
 In 0/1 knapsack, this yields 160 whereas taking items 2 and 3 gives 220, proving
 why 0/1 requires DP."

[25:00 - 35:00] Coding Implementation
"In C#, I'll use records with computed density properties.
 In Python, we sort by lambda x: x.value / x.weight.
 For job sequencing, we use an array of size max_deadline + 1..."

[35:00 - 42:00] Complexity Deconstruction
"Fractional Knapsack runs in O(N log N) time and O(1) auxiliary space.
 Job Sequencing runs in O(N log N + N * D) time and O(D) space.
 If D is large, we can optimize the slot search to O(N log N) using Disjoint Set Union."

[42:00 - 45:00] Edge Cases & Systems Defenses
"Edge cases covered:
 - Zero or negative capacity -> Return 0.0 immediately.
 - Job deadline exceeds max timeline -> Bound slots to max_deadline.
 - Jobs with identical profit -> Tie-break arbitrarily; optimal value preserved."
```

---

## ⚔️ SUPPLEMENTARY OUTCOMES

### 🏋️ Practice Problems

| # | Problem | Source | Difficulty | Key Concept |
| :--- | :--- | :--- | :--- | :--- |
| 1 | Fractional Knapsack | GeeksForGeeks | Easy | Density sorting |
| 2 | Job Sequencing with Deadlines | GeeksForGeeks | Medium | Backward slot assignment |
| 3 | Maximum Units on a Truck | LeetCode 1710 | Easy | Fractional Knapsack variant |
| 4 | Minimum Cost to Connect Sticks | LeetCode 1167 | Medium | Greedy min-heap combination |
| 5 | Course Schedule III | LeetCode 630 | Hard | Max-heap deadline adjustment |

### 🎙️ Interview Questions

1. **Q:** Can Fractional Knapsack be solved in `O(N)` without sorting?
   - *Answer:* Yes. Using the Quickselect algorithm (Median of Medians), we can partition items around the median density in `O(N)` average time.
2. **Q:** Why does Job Sequencing sort by profit descending rather than deadline ascending?
   - *Answer:* Sorting by deadline forces low-profit jobs into early slots, permanently blocking high-profit jobs with overlapping deadlines.
3. **Q:** How does Disjoint Set Union (DSU) optimize Job Sequencing?
   - *Answer:* Parent pointers in DSU point to the next available free slot. `Find(deadline)` resolves the free slot in `O(α(D))` time instead of a linear backward scan.

---

**End of Week 12 Day 04 Instructional File**

---
> 🧭 **Navigation:** [← Previous Day](Week_12_Day_03_Huffman_Coding_And_Optimal_Trees_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_12_Day_05_Greedy_In_Systems_Instructional.md)
