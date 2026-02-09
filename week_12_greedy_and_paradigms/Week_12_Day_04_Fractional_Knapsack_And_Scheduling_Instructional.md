# 📘 WEEK 12 DAY 4: FRACTIONAL KNAPSACK & SCHEDULING — ENGINEERING GUIDE

**Metadata:**
- **Week:** 12 | **Day:** 04
- **Category:** Algorithm Paradigms (Greedy)
- **Difficulty:** 🟡 Intermediate
- **Real-World Impact:** Fractional knapsack and greedy scheduling ideas are core to resource allocation, bandwidth throttling, job scheduling, and finance-style portfolio slices.
- **Prerequisites:**
  - Week 12 Day 01 – Greedy Fundamentals (greedy template, exchange arguments)
  - Comfort with arrays, sorting, and basic complexity analysis
  - Basic understanding of 0/1 knapsack as a DP problem (prior DP week)

---

## 🎯 LEARNING OBJECTIVES

*By the end of this chapter, you will be able to:*

- 🎯 **Formulate** the fractional knapsack problem and distinguish it clearly from the 0/1 version.
- ⚖️ **Explain** why greedy by value/weight ratio is optimal for fractional knapsack, but not for 0/1 knapsack.
- ⚙️ **Implement** (conceptually) the greedy fractional knapsack algorithm using sorting.
- 🧠 **Understand** job sequencing with deadlines and design the classic greedy scheduling solution.
- 🧪 **Reason** about correctness using exchange arguments and structure of optimal solutions.

---

## 📖 CHAPTER 1: CONTEXT & MOTIVATION

### The Engineering Challenge: Limited Capacity, Many Options

Imagine you are operating a video streaming platform. You have:

- A fixed bandwidth budget for a user’s connection (e.g., 10 Mbps).
- Many potential video tracks or quality layers you could stream (1080p, 720p, 480p, audio-only).
- Each track consumes some bandwidth ("weight") and provides some perceived quality or revenue ("value").

You can **mix and match** quality layers fractionally, for instance:

- Part of the time at 1080p,
- Part of the time at 720p,
- The rest at 480p.

Your goal is to **maximize total value** under the bandwidth cap.

This is the essence of the **fractional knapsack** problem: you have a capacity (knapsack size) and items with values and weights, and you are allowed to take **fractions** of items.

In many resource allocation settings (e.g., splitting CPU time, network bandwidth, money), fractional allocation is perfectly reasonable.

In other contexts, however, decisions are binary:

- You either ship an entire physical product or you don’t.
- You either run a job or not; you can’t run "half a job" and still claim full profit.

For those, you must use **0/1 knapsack**, which requires dynamic programming.

Today’s goal is to understand:

1. Why **greedy by value/weight ratio** is perfect for the fractional variant.
2. Why it **fails** for the 0/1 variant.
3. How similar greedy thinking powers another scheduling problem: **job sequencing with deadlines**.

### The Scheduling Challenge: Jobs, Deadlines, and Profits

Consider another scenario: an online marketplace that runs paid promotional slots (like a "deal of the day"). You have:

- A fixed number of discrete time slots (e.g., 24 hourly slots in a day).
- A set of jobs (promotions), each with:
  - A **deadline** (latest slot by which it must be shown).
  - A **profit** earned if the job is scheduled.
- Each job takes exactly one slot.

You want to **maximize total profit**.

A natural greedy idea: schedule the highest-profit jobs first. But **where** in the schedule should they go?

- Greedy solution: sort jobs by **profit descending**, and for each job, schedule it at the **latest available slot before or at its deadline**.

This yields an optimal solution and is another classic greedy success story.

Both fractional knapsack and job sequencing use the same greedy pattern:

- Sort by a meaningful **priority** (ratio or profit).
- Use a simple, local rule to place each item or job.

---

## 🧠 CHAPTER 2: BUILDING THE MENTAL MODEL

### Fractional Knapsack vs 0/1 Knapsack

#### 1. Problem Definitions

**Fractional Knapsack:**

- Input:
  - Capacity `W` (maximum weight the knapsack can hold)
  - `n` items, each with value `v[i]` and weight `w[i]`
- Constraint:
  - Can take **any fraction** `x[i]` of item `i` where `0 ≤ x[i] ≤ 1`
- Objective:
  - Maximize total value `Σ v[i] × x[i]` such that `Σ w[i] × x[i] ≤ W`

**0/1 Knapsack:**

- Same inputs and capacity.
- Constraint:
  - Can only take **0 or 1** unit of each item (no fractions)
- Objective:
  - Maximize total value `Σ v[i] × x[i]` with `x[i] ∈ {0,1}` and `Σ w[i] × x[i] ≤ W`

#### 2. Geometric Intuition (Fractional Case)

Think of each item as a line segment in value-weight space:

- The **slope** of the segment is `v[i] / w[i]` — value per unit weight.

Fractional knapsack is equivalent to:

> Fill a container of capacity `W` with a fluid mixture of items. Each item type gives you a linear trade-off: for each extra unit of weight you add, you gain `v[i] / w[i]` value.

To maximize total value:

- Always take from the item with **highest slope** (value/weight ratio) first.
- When that item is exhausted, move to the next highest slope, etc.

This is exactly the greedy rule.

For 0/1 knapsack, the geometry becomes **combinatorial**—you can’t slide continuously along each segment; you either take it all or none. That breaks the convex, linear structure that greedy relies on.

### 🖼 Visual: Fractional Knapsack Timeline

Take this example:

```text
Capacity W = 50

Item   Value   Weight   Ratio (value/weight)
  1      60      10         6.0
  2     100      20         5.0
  3     120      30         4.0
```

Visualize items as bars of height `ratio`:

```text
Ratio
  6.0 |■■■■■ Item 1
  5.0 |■■■■  Item 2
  4.0 |■■■   Item 3
       -----------------
        (descending order)
```

Your knapsack has **50 units** of capacity. Greedy strategy:

1. Take all of item 1 (weight 10)
2. Take all of item 2 (weight 20; total used = 30)
3. Take as much of item 3 as fits: `50 - 30 = 20` units (out of 30)

Total value:

- Item 1: 60
- Item 2: 100
- Item 3: `(20/30) × 120 = 80`
- Total = 60 + 100 + 80 = **240**

Try any other combination (e.g., mixing fractions in another order) and you will never beat this — because you always want to spend capacity on the highest ratio available first.

### Why Greedy Works Here (High-Level)

Fractional knapsack is essentially a **continuous optimization** problem over a convex feasible region:

- The objective is linear in `x[i]`.
- The constraints are linear: `x[i] ≥ 0`, `x[i] ≤ 1`, `Σ w[i] x[i] ≤ W`.

Greedy by ratio corresponds to the solution of a **linear program**:

- The optimal solution always lies at an extreme point (vertex) of the feasible polytope.
- For this particular linear program, that vertex is precisely what the greedy algorithm constructs.

You don’t need to know linear programming to use this; the key takeaway:

> Because the problem is continuous and linear in the fractions, local best choices (by ratio) combine to a global optimum.

### Why the Same Greedy Idea Fails for 0/1 Knapsack

Consider a 0/1 knapsack example:

```text
Capacity W = 50

Item   Value   Weight   Ratio
  1      60      10      6.0
  2     100      20      5.0
  3     120      30      4.0
```

Greedy by ratio, taking whole items:

1. Take item 1 (weight 10, value 60) → remaining capacity 40
2. Take item 2 (weight 20, value 100) → remaining capacity 20
3. Item 3 (weight 30) does not fit → skip

Total value = 60 + 100 = **160**.

But consider items 2 and 3 only:

- Combined weight = 20 + 30 = 50 (fits exactly)
- Combined value = 100 + 120 = **220**

220 > 160, so greedy by ratio is **suboptimal** in the 0/1 setting.

This example shows:

- Local ratio-optimal choices can block a globally better combination when you cannot split items.
- 0/1 knapsack must be solved with **dynamic programming** (or other combinatorial techniques), not this simple greedy.

---

## ⚙️ CHAPTER 3: MECHANICS & IMPLEMENTATION

### Fractional Knapsack: State & Algorithm

**Input:**

- Capacity `W`
- `n` items with value `v[i]` and weight `w[i]`

**Goal:** Maximize total value with fractional selection.

**Algorithm (Conceptual):**

1. For each item `i`, compute `ratio[i] = v[i] / w[i]`.
2. Sort items in **descending order** of `ratio[i]`.
3. Initialize:
   - `currentWeight = 0`
   - `totalValue = 0`
4. For each item in sorted order:
   - If adding the full item doesn’t exceed capacity:
     - Take the whole item.
     - `currentWeight += w[i]`
     - `totalValue += v[i]`
   - Else:
     - Compute remaining capacity: `remain = W - currentWeight`.
     - Take fraction `remain / w[i]` of this item.
     - `totalValue += v[i] × (remain / w[i])`.
     - Set `currentWeight = W` and **stop** (knapsack is full).
5. Output `totalValue` and fractions used.

**Time Complexity:**

- Computing ratios: O(n)
- Sorting by ratio: O(n log n)
- Single pass: O(n)
- Overall: **O(n log n)**

**Space Complexity:**

- O(n) for item list and sorting.

### 🖼 Detailed Trace: Fractional Knapsack Example

Revisit the earlier example:

```text
Capacity W = 50

Item   Value   Weight   Ratio
  1      60      10      6.0
  2     100      20      5.0
  3     120      30      4.0
```

**Step 1: Sort by ratio**

Order: Item 1 (6.0), Item 2 (5.0), Item 3 (4.0)

**Step 2: Greedy fill**

Initial state:

```text
currentWeight = 0
totalValue = 0

fractions: x1 = 0, x2 = 0, x3 = 0
```

**Process Item 1:**

- `currentWeight + w1 = 0 + 10 = 10 ≤ 50` → take full item 1
- Update:
  - `currentWeight = 10`
  - `totalValue = 60`
  - `x1 = 1`

**Process Item 2:**

- `currentWeight + w2 = 10 + 20 = 30 ≤ 50` → take full item 2
- Update:
  - `currentWeight = 30`
  - `totalValue = 160`
  - `x2 = 1`

**Process Item 3:**

- `currentWeight + w3 = 30 + 30 = 60 > 50` → cannot take full item 3
- Remaining capacity: `remain = 50 - 30 = 20`
- Fraction: `x3 = remain / w3 = 20 / 30 ≈ 0.6667`
- Value gained: `v3 × x3 = 120 × (20/30) = 80`
- Final update:
  - `currentWeight = 50`
  - `totalValue = 160 + 80 = 240`

Final state:

```text
x1 = 1, x2 = 1, x3 = 2/3
Total value = 240
Total weight used = 10 + 20 + (2/3)×30 = 50
```

All references (items, capacities, fractions) are consistent.

### Correctness via Exchange Argument

The high-level proof that this greedy algorithm is optimal for fractional knapsack uses an **exchange argument**:

1. Consider any optimal solution `O` with fractions `x[i]`.
2. Suppose `O` uses some capacity on an item `j` with lower ratio than another item `k` that is **not fully used**.
3. Then you can improve (or keep equal) total value by moving some capacity from `j` to `k`:
   - Reduce `x[j]` by a small amount `ε`.
   - Increase `x[k]` by `ε × (w[j]/w[k])` to keep total weight constant.
   - Value change:
     - Lost: `ε × v[j]`
     - Gained: `ε × (w[j]/w[k]) × v[k]`
     - Since `v[k]/w[k] > v[j]/w[j]`, the gain exceeds the loss.
4. Repeating this exchange process, any optimal solution can be transformed into one that:
   - Takes all capacity from highest-ratio items first,
   - Then moves to next highest, etc.

This aligns exactly with our greedy construction, so the greedy solution is optimal.

### Job Sequencing with Deadlines: State & Algorithm

**Input:**

- `n` jobs.
- Each job `i` has:
  - A profit `p[i]` (earned if completed).
  - A deadline `d[i]` (integer time slot; job takes exactly 1 slot, must finish by `d[i]`).
- Assume time slots are `[1, 2, ..., T]`, where `T = max(d[i])`.

**Goal:**

- Schedule at most one job per time slot.
- Each scheduled job `i` occupies one slot at some time `t` with `1 ≤ t ≤ d[i]`.
- Maximize total profit of scheduled jobs.

**Greedy Strategy:**

1. Sort jobs in **descending order of profit**.
2. Initialize an array of size `T` representing slots, all initially empty.
3. For each job in sorted order:
   - Try to schedule it in the **latest free slot** `t` such that `1 ≤ t ≤ d[i]`.
   - If you find such a slot, place the job there; add its profit to total profit.
   - If no such slot exists, skip the job.

**Intuition:**

- Give **priority** to more profitable jobs.
- By placing each job as late as possible, you leave earlier slots open for other jobs that might have earlier deadlines.

### 🖼 Visual: Job Sequencing Example

Consider this example:

```text
Jobs:   A   B   C   D   E
Profit: 100  19  27  25  15
Deadline:2   1   2   1   3
```

Step 1: Sort by profit descending:

```text
Job   Profit  Deadline
 A     100      2
 C      27      2
 D      25      1
 B      19      1
 E      15      3
```

Max deadline `T = 3` → Slots `[1, 2, 3]`, all empty initially.

**Process A (100, d=2):**

- Try slot 2 → empty → assign A to slot 2.

Slots: `[ -, A, - ]` (using 1-based indexing)
Total profit: 100

**Process C (27, d=2):**

- Try slot 2 → occupied by A
- Try slot 1 → empty → assign C to slot 1.

Slots: `[ C, A, - ]`
Total profit: 100 + 27 = 127

**Process D (25, d=1):**

- Try slot 1 → occupied by C → cannot schedule D.

Slots remain `[ C, A, - ]`
Total profit: 127

**Process B (19, d=1):**

- Try slot 1 → occupied → cannot schedule B.

Slots remain `[ C, A, - ]`
Total profit: 127

**Process E (15, d=3):**

- Try slot 3 → empty → assign E to slot 3.

Slots: `[ C, A, E ]`
Total profit: 127 + 15 = **142**

Final schedule:

```text
Time Slot: 1    2    3
          C(27) A(100) E(15)

Total profit: 142
```

You can check that no other schedule yields higher profit without violating deadlines.

### Correctness Sketch for Job Sequencing

The proof again uses an **exchange argument**:

- Let `G` be the greedy schedule, jobs filled by highest profit, as late as possible.
- Let `O` be an optimal schedule.
- Consider jobs one by one in greedy order and show:
  - For each job that greedy schedules, there exists an optimal schedule where that job also appears (possibly in a different slot), without reducing total profit.
  - If `O` doesn’t have that job, you can swap it with a lower-profit job or move jobs around in `O` to include it.

By repeatedly "repairing" `O` to include greedy choices without reducing profit, you eventually transform `O` into a schedule identical to `G`, proving optimality.

**Key intuition:**

- Placing each high-profit job as late as possible retains maximum flexibility for other jobs.
- Any schedule that doesn’t follow this strategy can be rearranged to one that does, without hurting profit.

**Complexity:**

- Sorting jobs: O(n log n)
- For each job, scanning backwards from its deadline to find a free slot:
  - Worst-case O(T) per job → O(nT) total
- With an efficient data structure (e.g., Disjoint Set Union), you can reduce scheduling to **almost O(n log n)**.

---

## ⚖️ CHAPTER 4: PERFORMANCE, TRADE-OFFS & REAL SYSTEMS

### Comparing Fractional vs 0/1 Knapsack

| Aspect | Fractional Knapsack | 0/1 Knapsack |
| :--- | :--- | :--- |
| Decisions | Fractions allowed (0–1) | All or nothing (0 or 1) |
| Algorithm | Greedy by value/weight ratio | Dynamic Programming |
| Time | O(n log n) | O(nW) or O(n × value-range) typical DP |
| Space | O(n) | O(nW) (can optimize to O(W)) |
| Optimal? | **Yes** under fractional model | DP gives exact optimum; greedy fails |

Trade-off:

- If fractions are allowed, use greedy; it is fast and optimal.
- If only full items allowed, prefer DP (or approximation/heuristics for large W).

### Real-World Uses: Fractional Knapsack

1. **Bandwidth Allocation:**
   - Splitting a fixed bandwidth budget among competing streams or services.
   - Each stream has marginal utility (value per unit bandwidth).
   - Greedy by value/weight ratio approximates optimal allocation when utility is linear.

2. **Portfolio Fragmentation (Simplified Model):**
   - Allocating capital across assets with simple risk/return trade-offs (ignoring complex finance realities).
   - Under linear models, taking fractions of each asset by return per unit risk/capital mimics fractional knapsack.

3. **Resource Shares in Cloud Systems:**
   - CPU time slices among services, where each service yields some utility per unit CPU.
   - Greedy allocation by highest utility-per-CPU first is fractional knapsack-like.

### Real-World Uses: Job Sequencing with Deadlines

1. **Ad Slot Scheduling:**
   - Ads have deadlines (campaign end dates) and profits (per impression or click).
   - For each discrete time slot, show one ad.
   - Greedy job sequencing selects most profitable ads while respecting deadlines.

2. **Backup / Maintenance Windows:**
   - Maintenance tasks with deadlines and value (e.g., priority of patching critical systems).
   - Limited number of maintenance windows.

3. **Manufacturing / Production Lines:**
   - Jobs with deadlines (delivery dates) and profits.
   - Each machine can process one job at a time.

In all of these, the greedy algorithm provides a clean, efficient scheduling policy.

### Failure Modes & Edge Cases

**Fractional Knapsack:**

- If items cannot be split in reality, applying fractional knapsack greedily can produce over-optimistic estimates.
- If ratios are equal for many items, any tie-breaking is fine; multiple optimal solutions exist.
- Numerical precision issues when dealing with floating-point ratios and fractions.

**Job Sequencing:**

- If some jobs have deadlines beyond the considered horizon, you must define the maximum time window carefully.
- If preparation/setup times vary, the "1 slot per job" assumption breaks; problem becomes more complex.
- Need to ensure deadlines and time slots are consistent (e.g., no job with deadline 0).

---

## 🔗 CHAPTER 5: INTEGRATION & MASTERY

### Connections (Precursors & Successors)

**Builds on:**

- Week 12 Day 01 – Greedy correctness patterns.
- DP Week – 0/1 knapsack and understanding when greedy fails.
- Basic sorting and priority logic from earlier weeks.

**Prepares for:**

- More complex scheduling problems (with release times, varying durations, precedence constraints).
- Approximation algorithms where greedy is used for NP-hard problems.
- Understanding the gap between continuous relaxations (fractional) and integral solutions (0/1).

### 🧩 Pattern Recognition & Decision Framework

When facing a resource allocation or scheduling problem, ask:

1. **Can I take fractions?**
   - If **yes**, suspect a **fractional knapsack** or related continuous problem—greedy by ratio may be optimal.
   - If **no**, consider 0/1 knapsack or combinatorial search/DP.

2. **How is value defined?**
   - If value accumulates linearly with resource used → fractional models make sense.
   - If value is all-or-nothing per job/item → 0/1 models are appropriate.

3. **Are there deadlines and discrete slots?**
   - If each job takes one slot and has a deadline and profit → think **job sequencing with deadlines**.

### 🧪 Socratic Reflection

1. **Why does the ability to take fractions fundamentally change the problem’s difficulty?** How does it connect to linear programming?

2. **In the job sequencing algorithm, why do we place each job at its latest available slot rather than the earliest?** What goes wrong if we always choose the earliest slot ≤ deadline?

3. **For 0/1 knapsack, can you think of any greedy rule that always works?** If not, why is DP necessary?

### 📌 Retention Hook

> **The Essence:** "If you can slice items, greedy by value density (value/weight) fills your capacity optimally. If you can’t slice items, you step into the combinatorial world of 0/1 knapsack, where dynamic programming takes over. For jobs with deadlines and profits, greedily grab the most valuable jobs first and tuck each one as late as possible before its deadline, leaving room for the rest."

---

## 🧠 5 COGNITIVE LENSES

### 1. 💻 The Hardware Lens

- Fractional knapsack is dominated by sorting (cache-friendly) and a single linear pass.
- Job sequencing is also sorting-heavy; slot assignment can be optimized with union-find-like structures.
- Both patterns map well to large-scale, streaming data: sort once, scan once.

### 2. 📉 The Trade-off Lens

- **Fractional Knapsack:**
  - Pros: Fast, optimal under fractional assumption.
  - Cons: Inapplicable when items must be whole.

- **0/1 Knapsack:**
  - Pros: Exact optimum.
  - Cons: DP complexity grows with capacity or value; may be too slow/large.

- **Job Sequencing:**
  - Pros: Simple greedy with strong optimality; easy to reason about.
  - Cons: Assumes equal job lengths and discrete slots.

### 3. 👶 The Learning Lens

Students often:

- Mix up fractional and 0/1 knapsack, applying greedy where DP is needed.
- Forget that job sequencing places jobs as **late** as possible.
- Confuse different scheduling objectives (e.g., maximize number of jobs vs maximize profit vs minimize lateness).

Clarity on **constraints** (fractional vs whole, deadlines vs none) is key.

### 4. 🤖 The AI/ML Lens

- Fractional resource allocation is analogous to tuning hyperparameters like learning rate or data sampling ratios based on marginal utility.
- Job sequencing with deadlines resembles scheduling training jobs on limited hardware with time budgets and relative importance.

### 5. 📜 The Historical Lens

- Knapsack problems are classic in operations research and combinatorial optimization.
- Fractional knapsack was among the early examples demonstrating how linear programming and greedy strategies align.
- Job sequencing with deadlines appears in early scheduling theory and is a staple example in algorithm textbooks alongside MST, Dijkstra, and Huffman.

---

## ⚔️ SUPPLEMENTARY OUTCOMES

### 🏋️ Practice Problems (8–10)

| # | Problem | Source | Difficulty | Key Concept |
| :--- | :--- | :--- | :--- | :--- |
| 1 | Fractional Knapsack | GeeksForGeeks / others | Easy | Greedy by value/weight ratio |
| 2 | Fractional Knapsack Variant (Different Units) | Custom | Medium | Handling different unit scales |
| 3 | 0/1 Knapsack vs Fractional Comparison | Custom | Medium | Counterexamples for greedy in 0/1 |
| 4 | Job Sequencing with Deadlines | GeeksForGeeks / SPOJ | Medium | Greedy scheduling by profit |
| 5 | Scheduling Advertisements | Custom | Medium | Job sequencing variant with slots |
| 6 | Maximize Tasks Before Deadline | LeetCode-style | Medium | Variants with different objectives |
| 7 | Course Schedule with Deadlines & Profits | Harder custom | Hard | Combine greedy with heap/DSU |
| 8 | Continuous Resource Allocation | Conceptual | Medium | Fractional knapsack generalization |

### 🎙️ Interview Questions (6–8)

1. **Q:** Define fractional knapsack and 0/1 knapsack. Why does greedy work for one but not the other?

2. **Q:** Describe the algorithm for fractional knapsack and prove its correctness informally.

3. **Q:** Give an example where greedy by value/weight ratio fails for 0/1 knapsack.

4. **Q:** Explain the job sequencing with deadlines problem and outline the greedy solution.

5. **Q:** Why do we schedule each job at the latest available slot before its deadline?

6. **Q:** How would you optimize the slot search in job sequencing beyond the naive O(T) per job?

### ❌ Common Misconceptions (3–5)

1. **Myth:** Greedy by value/weight ratio always works for any knapsack problem.
   - **Reality:** It only works when fractional selection is allowed. For 0/1 knapsack, it can be arbitrarily bad.

2. **Myth:** In job sequencing, placing each job in the earliest possible slot is equivalent to placing it in the latest.
   - **Reality:** Earliest placement can block high-profit jobs with late deadlines; latest placement preserves flexibility.

3. **Myth:** Fractional knapsack is just a special case of 0/1 knapsack.
   - **Reality:** Mathematically, it is a **relaxation** of 0/1 knapsack. It is easier and has different algorithmic properties.

4. **Myth:** Any scheduling problem with deadlines can be solved by this job sequencing greedy strategy.
   - **Reality:** This strategy assumes equal job lengths and single-slot jobs. When durations vary or dependencies exist, more complex methods are needed.

### 🚀 Advanced Concepts (3–5)

1. **Lagrangian Relaxation of Knapsack:**
   - Relax capacity constraints with a penalty term; connects to dual problems and fractional solutions.

2. **Multiple Knapsack & Bin Packing:**
   - Extensions where you have multiple knapsacks or need to pack items efficiently — typically NP-hard, often require approximations.

3. **Weighted Interval Scheduling with Profits and Deadlines:**
   - Generalization combining interval structure with profits; solved via DP.

4. **Union-Find Optimization for Job Sequencing:**
   - Use Disjoint Set Union to find latest available slot efficiently.

5. **Approximation Schemes for Knapsack:**
   - Polynomial-time approximation schemes (PTAS/FPTAS) for large 0/1 knapsack instances.

### 📚 External Resources

- **CLRS — Greedy Algorithms Chapter:** Sections on fractional knapsack and job sequencing with deadlines.
- **Kleinberg & Tardos — Algorithm Design:** Excellent explanations with visual proofs.
- **Competitive Programming Books (e.g., CP3):** Implementation-focused variants of these problems.
- **MIT/Stanford Algorithm Lectures:** Video lectures often include detailed walkthroughs of these greedy patterns.

---

**End of Week 12 Day 04 Instructional File**  
**Status:** ✅ Complete | **Structure:** v12 Narrative (5 Chapters + Lenses + Supplementary)
