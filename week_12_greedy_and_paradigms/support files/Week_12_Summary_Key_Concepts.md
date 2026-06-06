# Week 12 Summary & Key Concepts — Greedy Algorithms & Proofs

**Week:** 12  
**Phase:** 🟧 Algorithm Paradigms  
**Focus:** Greedy algorithm design, correctness proofs, and when greedy is guaranteed (or not) to be optimal.

---

## 1. Narrative Overview of the Week

Week 12 is about learning to **trust** greedy algorithms only when you can **justify** them.

Up to now, you’ve seen greedy ideas scattered across the curriculum: 
- Dijkstra picking the next closest node, 
- Kruskal adding the next lightest edge, 
- Prim expanding the frontier of an MST,
- Caching strategies like LRU.

This week turns those scattered intuitions into a **coherent theory**. You learn:
- What exactly makes a greedy rule **safe** (greedy choice property + optimal substructure).  
- How to turn a vague idea (“pick whatever looks best now”) into a **rigorous algorithm** with an invariant.  
- How to **prove correctness** using structured arguments instead of hand-waving.

The week progresses from **generic patterns** to **canonical case studies**:
- Day 1 clarifies the greedy paradigm and core proof techniques.  
- Day 2 applies greedy to **interval scheduling** and related problems.  
- Day 3 studies **Huffman coding**, a classic tree-based greedy algorithm.  
- Day 4 compares **fractional vs 0/1 knapsack** and introduces greedy scheduling with deadlines.  
- Day 5 (optional) opens the door to greedy in **systems and approximation algorithms** (MST, caching, set cover).

By the end, greedy should feel less like a bag of tricks and more like a **design lens** you can deliberately pick up—or deliberately put down.

---

## 2. Per-Day Concept Summaries

### Day 1 — Greedy Fundamentals & Correctness Proofs

**Core ideas:**
- **Greedy algorithm concept:** At each step, choose the locally best option **without revisiting past choices**, hoping this leads to a globally optimal solution.
- **Greedy choice property:** There exists an optimal solution whose first step matches the greedy choice. If you can show this, greedy is a strong candidate.
- **Optimal substructure:** The optimal solution to the whole problem can be built from optimal solutions to subproblems.

**Key techniques:**
- **Greedy template:**
  - Identify a priority rule (sort key or heap key).  
  - Process items in that order, adding each if it remains feasible.  
  - Maintain a state capturing feasibility (capacity, last finish time, etc.).
- **Exchange argument:** Start from an optimal solution, swap one choice at a time to match the greedy solution, showing the objective never worsens.
- **Greedy stays ahead:** For any other feasible solution, the greedy solution is never “behind” in terms of some progress metric after each step.
- **Inductive proofs:** Treat each greedy step as extending a partially optimal prefix.

The main mental shift: **“I don’t just implement greedy; I prove why my choice is safe.”**

---

### Day 2 — Activity Selection & Interval Problems

**Activity selection (core problem):**
- Input: activities with start and end times.  
- Goal: choose the **maximum number of non-overlapping** activities.

**Correct greedy rule:**
- Sort by **earliest finish time**, then iterate and pick each activity that starts after the last chosen one finishes.

**Why finish time?**
- Finishing earlier leaves more room in the future for other activities.  
- The **exchange argument** shows that any optimal solution can be adjusted so that its first activity is the earliest finishing one.

**Important variations:**
- **Minimum intervals to remove:** Equivalent to “maximum non-overlapping intervals” → still solved with the same greedy strategy, but interpreted differently.  
- **Meeting rooms / classroom allocation:** Instead of maximizing count, you minimize the number of **parallel tracks**; typically solved via **sweep line + heaps** counting overlap.  
- **Weighted interval scheduling:** When intervals have weights/profits, simple greedy by finish time or weight **fails**; dynamic programming is needed.

Conceptually, Day 2 teaches you to see **interval structure + objective** and pick or reject greedy accordingly.

---

### Day 3 — Huffman Coding & Optimal Prefix Trees

**Problem:** Given symbols with frequencies, build a **prefix-free binary code** minimizing the **expected codeword length** (weighted path length).

**Huffman algorithm:**
1. Put all symbols into a min-heap keyed by frequency.
2. While there is more than one node:
   - Extract the two least frequent nodes.  
   - Merge them into a new parent node whose frequency is their sum.  
   - Insert the parent back into the heap.
3. Assign 0/1 to edges and read codewords from root to leaves.

**Why is this greedy?**
- At each step, you **greedily merge the two smallest frequencies**, pushing them deeper into the tree where cost (depth × frequency) is minimized.

**Correctness insight:**
- In some optimal code tree, the two least frequent symbols appear as siblings at the maximum depth.  
- An **exchange argument** shows that if they aren’t siblings in a candidate optimal tree, you can restructure the tree to make them siblings without increasing total cost.  
- Repeating this argument from the bottom up leads to the same structure as Huffman’s algorithm.

Day 3 teaches you that greedy is not limited to flat lists; it can also drive **bottom-up construction of optimal trees**.

---

### Day 4 — Fractional Knapsack, 0/1 Knapsack & Job Sequencing

**Fractional knapsack:**
- Input: items with value and weight, capacity W.  
- You can take **fractions of items**.  
- Goal: maximize total value.

**Greedy solution:**
- Sort items by **value/weight ratio** in descending order.  
- Take from the highest-ratio item first, filling the knapsack until capacity is exhausted.

**Why it works:**
- Any solution mixing lower-ratio items while capacity is still used for higher-ratio items can be locally improved by swapping weight from low to high ratio → **exchange argument**.

**0/1 knapsack:**
- Same inputs, but you must take items **entirely or not at all**.  
- Ratio-greedy fails because taking a single large high-ratio item might block a **combination** of smaller items with greater total value.  
- Needs **DP** over (item index, remaining capacity).

**Job sequencing with deadlines and profits:**
- Jobs have profit and a deadline (slot index). Each job takes one unit of time.  
- Goal: select a subset and schedule them into slots (before deadlines) to maximize total profit.

**Greedy solution:**
- Sort jobs by **profit descending**.  
- For each job in order, place it in the **latest available time slot** before its deadline.  
- This maximizes “room” for future jobs.

Day 4 sharpens your understanding of **when greedy is structurally safe** (fractional knapsack, job sequencing) and when it collides with combinatorial interactions (0/1 knapsack).

---

### Day 5 (Optional) — Greedy in Systems & Approximation

**Systems perspective:**
- **MST (Kruskal, Prim):** Greedy by local cheapest edge while preserving connectivity and acyclicity (cut property).  
- **Dijkstra:** Greedy by current shortest tentative distance, assuming **non-negative edges**.
- **Caching (LRU):** Greedy in time: always evict the item least recently used, assuming recent past predicts near-future use.

**Approximation algorithms:**
- **Set cover:** Greedy by “most uncovered elements per unit cost” yields an O(log n) approximation.  
- Many NP-hard problems admit **greedy approximations**: not optimal, but provably within some factor of the best.

Day 5 expands your mental map from contest problems to **production heuristics** and **theory of approximation**.

---

## 3. Concept Comparison Tables

### 3.1 Greedy vs Dynamic Programming vs Backtracking

| Aspect | Greedy | Dynamic Programming | Backtracking / Branch & Bound |
| :--- | :--- | :--- | :--- |
| Core idea | Local choice, no revisiting | Systematic reuse of subproblems | Systematic exploration with pruning |
| Memory usage | Typically low | Often high (tables) | Varies, can be exponential |
| Proof style | Exchange, stays-ahead, induction | Optimal substructure, recurrences | Correctness by exhaustive coverage/pruning safety |
| When ideal | Clear local rule + provable safety | Overlapping subproblems & optimal substructure | Small search spaces, complex constraints |
| Example | Activity selection, fractional knapsack | 0/1 knapsack, edit distance | N-Queens, subset search |

---

### 3.2 Interval Problems at a Glance

| Problem | Objective | Best Known Approach | Greedy Works? |
| :--- | :--- | :--- | :--- |
| Activity selection | Maximize count of non-overlapping intervals | Greedy by earliest finish | ✅ Yes |
| Min intervals to remove | Remove minimum intervals to avoid overlap | Same greedy as activity selection | ✅ Yes |
| Meeting rooms / classrooms | Min number of rooms | Sweep line / heap | ✅ Greedy-style (via events) |
| Weighted interval scheduling | Maximize total weight | DP with binary search on previous non-overlapping | ❌ No simple greedy |

---

### 3.3 Knapsack Variants

| Variant | Items | Decision | Best Method | Greedy by ratio? |
| :--- | :--- | :--- | :--- | :--- |
| Fractional | Divisible | Take any fraction | Greedy by value/weight | ✅ Optimal |
| 0/1 | Indivisible | Take or skip entirely | DP | ❌ Not optimal in general |

---

## 4. Key Insights To Remember (Cheat-Sheet Style)

1. **Greedy is about structure, not about “liking big numbers.”**  
   The magic is not “pick biggest X,” but that the problem’s structure lets you prove that no future choice can overturn a good local decision.

2. **Two pillars: greedy choice property + optimal substructure.**  
   If you cannot argue that some optimal solution starts with your choice, or that the remaining subproblem is of the same type, be suspicious.

3. **Exchange arguments formalize “replace local choices step by step.”**  
   Think: “Given any optimal solution, I can swap its first choice with my greedy choice and not lose quality.”

4. **Greedy stays ahead when you can define a clean progress metric.**  
   For scheduling, this is often “tasks completed by time t” or “total weight processed by step k.”

5. **Intervals love finish-time ordering.**  
   When maximizing the number of intervals or minimizing removals, earliest finish time tends to preserve maximum future flexibility.

6. **Fractional knapsack is linear; 0/1 is combinatorial.**  
   Allowing fractions turns the problem into a continuous optimization where local ratio comparisons are globally correct.

7. **Huffman’s merging of smallest frequencies is tree-structured greedy.**  
   The two least frequent symbols **must** be at maximum depth together in some optimal tree; exchange arguments prove it.

8. **Job sequencing with deadlines leans on scheduling late.**  
   Assigning high-profit jobs as late as possible keeps earlier slots free for other jobs.

9. **Negative edges break Dijkstra’s greedy guarantee.**  
   Once you finalize a node’s distance in Dijkstra, you assume it cannot improve; negative edges violate that assumption.

10. **Greedy is often the backbone of approximation algorithms.**  
   When exact optimality is NP-hard, greedy can still give a formally bounded approximation quality.

---

## 5. Misconceptions and Corrections

1. **“If it works on a few tests, it must be right.”**  
   - *Reality:* Many incorrect greedy rules pass small tests. Correctness requires proof or at least a failed attempt to build counterexamples.

2. **“Greedy is always faster than DP.”**  
   - *Reality:* Big-O may be similar; sometimes DP tables are small. Choose based on structure and correctness, not just perceived speed.

3. **“Sorting by one metric is as good as another.”**  
   - *Reality:* Sorting by start time vs finish time vs duration can completely change behavior. The right key is tightly tied to the objective.

4. **“Huffman coding is only about the heap implementation.”**  
   - *Reality:* The important part is the **optimality structure** (least frequent at deepest level), not just using a priority queue.

5. **“Fractional and 0/1 knapsack are the same problem.”**  
   - *Reality:* Allowing fractional items fundamentally changes the mathematical structure and enables true greedy optimality.

---

## 6. Concept Map (Greedy in the Larger Curriculum)

Think of Week 12 as a hub connecting multiple earlier and later topics:

- **From Week 9 (Graphs):**
  - Dijkstra, Kruskal, Prim → now seen explicitly as **greedy on graphs**.

- **From Weeks 10–11 (DP):**
  - 0/1 knapsack, weighted interval scheduling → canonical **non-greedy** problems that clarify the boundary.

- **Week 12 (Greedy):**
  - Template + proofs + canonical examples (activity selection, Huffman, fractional knapsack, job sequencing).

- **Week 13 (Backtracking / Branch & Bound):**
  - Greedy rules often become **heuristics** or **bounds** for pruning search.

Visually, picture this (textual concept map):

```text
    Graph Algorithms (W9)     Dynamic Programming (W10–11)
              \                     /
               \                   /
                \                 /
                Week 12: Greedy Paradigms
                /       |        \
               /        |         \
        Systems Heuristics   Approximation   Backtracking (W13)
```

Greedy is the **bridge** between exact optimization (DP) and practical heuristics (systems, approximations, search).

---

## 7. Quick Self-Quiz (Concept Recall)

Use these prompts to test yourself without looking at notes:

1. Describe the **greedy choice property** and give one example where it holds and one where it does not.  
2. Explain the **exchange argument** for activity selection in 3–4 sentences.  
3. Why does Huffman always merge the two least frequent nodes? What structural fact justifies this?  
4. Provide a tiny counterexample where greedy by value/weight ratio fails for 0/1 knapsack.  
5. Explain why **Dijkstra** fails with negative edges but **Bellman–Ford** does not.  
6. Describe the **job sequencing with deadlines** greedy algorithm and its intuition.  
7. When reading a new problem, what signals make you suspect a greedy solution might exist?

If you can answer these fluently, you’ve internalized the **key concepts of Week 12**.
