# Week 12 Problem Solving Roadmap — Greedy Algorithms & Proofs

**Week:** 12  
**Phase:** 🟧 Algorithm Paradigms  
**Focus:** Progressive practice strategy for mastering greedy algorithm design and correctness proofs.

---

## How to use this roadmap

This file is your **training coach** for Week 12. It guides you through a **three-stage progression** from basic understanding to integration-level mastery of greedy algorithms.

### The three stages

**Stage 1: Basic Understanding (Direct Applications)**  
You're learning to recognize canonical greedy patterns and apply standard solutions. Focus: correctness intuition, template recognition, and basic proofs.

**Stage 2: Variations & Constraints**  
You're adapting greedy strategies to variants, debugging incorrect greedy rules, and handling edge cases. Focus: proof techniques, counterexample generation, and boundary reasoning.

**Stage 3: Integration & Mixed Paradigms**  
You're deciding when greedy is appropriate vs DP/backtracking, combining multiple greedy ideas, and applying greedy to systems problems. Focus: paradigm selection, hybrid solutions, and real-world trade-offs.

### Weekly rhythm

- **Days 1–2:** Stage 1 problems (foundations + activity selection)
- **Days 3–4:** Stage 2 problems (Huffman, fractional knapsack, scheduling variations)
- **Day 5 + Review:** Stage 3 problems (systems greedy, approximation, paradigm mixing)

---

## Overall problem-solving approach for greedy algorithms

### The greedy decision framework

When you encounter an optimization problem, ask these questions in order:

**1. Can I sort the input by some criterion?**  
Most greedy algorithms begin with sorting. If yes, what key aligns with the objective?

**2. What is the local greedy choice at each step?**  
Define precisely: "At step k, I choose X because..."

**3. Does this choice leave a subproblem of the same type?**  
Greedy requires that after making a choice, the remaining problem has the same structure.

**4. Can I prove the greedy choice is safe?**  
Use one of:
- **Exchange argument:** Show swapping greedy choice into an optimal solution doesn't worsen it.
- **Greedy stays ahead:** Show greedy solution is at least as good as optimal at every prefix.
- **Induction:** Prove prefix optimality implies full optimality.

**5. What's my termination condition and complexity?**  
Define when the algorithm stops and analyze time/space.

### Red flags: When greedy likely fails

- **Weighted versions** of problems where unweighted greedy works (e.g., weighted interval scheduling).
- **Global interactions:** Local choices block globally better combinations (0/1 knapsack).
- **No clear sorting key:** If multiple criteria conflict, greedy may not resolve them correctly.
- **You can't sketch a proof:** If you can't outline an exchange argument or stays-ahead proof in 2 minutes, greedy is suspect.

### Debugging strategy for incorrect greedy rules

1. **Generate small adversarial cases** (n ≤ 5):
   - All tied values
   - Nested structures (intervals within intervals)
   - Extreme ratios or edge weights
   - Boundary violations

2. **Compare greedy output to brute-force optimal** on these cases.

3. **Identify the structural failure:** Why does the greedy choice block a better global solution?

4. **Pivot to DP or backtracking** if greedy is fundamentally incompatible.

---

## Stage 1: Basic Understanding (Direct Applications)

**Goal:** Recognize canonical greedy patterns and apply standard solutions confidently.

### Core patterns in Stage 1

| Pattern | When to Use | Greedy Rule | Proof Strategy |
|---------|-------------|-------------|----------------|
| **Activity Selection** | Maximize non-overlapping intervals | Sort by finish time, select compatible | Exchange argument or stays-ahead |
| **Fractional Knapsack** | Maximize value with divisible items | Sort by value/weight ratio, fill greedily | Exchange argument on ratios |
| **Huffman Coding** | Optimal prefix-free code | Merge two smallest frequencies repeatedly | Exchange argument on tree structure |
| **Job Sequencing** | Maximize profit with deadlines | Sort by profit descending, place in latest free slot | Greedy choice property |

### Practice problems — Stage 1

**Activity Selection & Intervals**

1. **Classic Activity Selection**  
   *Problem:* Given n activities with start and end times, find the maximum number of non-overlapping activities.  
   *Focus:* Implement greedy-by-finish-time. Trace on 6–8 activities. Prove correctness via exchange argument.  
   *Edge cases:* Ties in finish time, all activities overlapping, no overlaps.

2. **Minimum Intervals to Remove**  
   *Problem:* Given intervals, remove the minimum number to make the rest non-overlapping.  
   *Focus:* Recognize this is `n - activity_selection(n)`. Implement and verify.  
   *Edge cases:* One interval, all identical intervals.

3. **Meeting Rooms (Minimum Rooms Needed)**  
   *Problem:* Given meeting start/end times, find minimum rooms needed.  
   *Focus:* Implement sweep line with start/end events. Maintain active count.  
   *Edge cases:* Start time equals previous end time (inclusive vs exclusive).

**Fractional Knapsack**

4. **Basic Fractional Knapsack**  
   *Problem:* Given items with weights and values, maximize total value with capacity W (can take fractions).  
   *Focus:* Sort by value/weight ratio, fill knapsack. Trace on 5 items.  
   *Edge cases:* All items fit, one item has capacity = W, items with zero weight.

5. **Counterexample for 0/1 Knapsack**  
   *Problem:* Construct a small instance where greedy-by-ratio fails for 0/1 knapsack.  
   *Focus:* Show global combination beats greedy local choice.  
   *Example:* Items {(w=5, v=50), (w=10, v=60), (w=15, v=100)}, W=25. Greedy chooses ratios 10, 12, ~6.67, but optimal is items 2+3.

**Huffman Coding**

6. **Build Huffman Tree Manually**  
   *Problem:* Given character frequencies {A:5, B:9, C:12, D:13, E:16, F:45}, construct Huffman tree.  
   *Focus:* Use min-heap simulation, draw tree, compute average code length.  
   *Edge cases:* Two symbols only, all equal frequencies.

7. **Huffman Implementation**  
   *Problem:* Implement Huffman coding algorithm with priority queue.  
   *Focus:* Handle internal nodes correctly, generate codes via tree traversal.  
   *Edge cases:* Single symbol (assign code "0"), empty input.

**Job Sequencing**

8. **Job Sequencing with Deadlines**  
   *Problem:* Given jobs with profits and deadlines, schedule to maximize profit (each job takes 1 unit time).  
   *Focus:* Sort by profit descending, place each job in latest free slot before deadline.  
   *Edge cases:* All jobs have deadline 1, deadlines exceed available slots.

### Stage 1 checklist

After completing Stage 1 problems:

- [ ] I can implement activity selection and explain why finish-time sorting works.
- [ ] I can trace Huffman coding on paper and compute average code length.
- [ ] I can solve fractional knapsack and articulate the proof via exchange argument.
- [ ] I can distinguish when greedy works (fractional) vs fails (0/1 knapsack).
- [ ] I can sketch a high-level exchange argument or stays-ahead proof for each pattern.

---

## Stage 2: Variations & Constraints

**Goal:** Adapt greedy strategies to problem variants, debug incorrect rules, and handle edge cases rigorously.

### Key skills in Stage 2

- **Proof construction:** Write out exchange arguments and stays-ahead proofs in detail.
- **Counterexample generation:** Given a proposed greedy rule, quickly find adversarial cases.
- **Tie-breaking analysis:** Understand when ties matter for correctness vs just solution uniqueness.
- **Boundary handling:** Off-by-one errors in intervals (inclusive vs exclusive ends), deadline slots, capacity limits.

### Practice problems — Stage 2

**Activity Selection Variants**

9. **Weighted Interval Scheduling (Recognize DP Need)**  
   *Problem:* Given intervals with weights, maximize total weight of non-overlapping intervals.  
   *Focus:* Show greedy-by-finish-time fails. Construct counterexample. Outline DP solution (sort by finish, dp[i] = max(dp[i-1], w[i] + dp[j]) where j < i and compatible).  
   *Key insight:* Weights break greedy choice property.

10. **Interval Coverage (Minimum Intervals to Cover Range)**  
    *Problem:* Given intervals and a target range [0, T], find minimum number of intervals to cover [0, T].  
    *Focus:* Greedy rule: sort by start, at each step pick interval that extends coverage farthest. Prove via exchange argument.  
    *Edge cases:* Gap in coverage (return -1), intervals don't reach T.

11. **Non-Overlapping Intervals with Different Tie-Breaking**  
    *Problem:* Activity selection with ties in finish time—compare different tie-breaking rules.  
    *Focus:* Show that tie-breaking by earliest start (among same finish) doesn't affect count optimality but changes which activities are selected.  
    *Proof focus:* Exchange argument tolerates arbitrary tie resolution.

**Huffman Variants**

12. **Ternary Huffman (3-ary Tree)**  
    *Problem:* Modify Huffman to use ternary codes (each node has 3 children).  
    *Focus:* At each step, merge 3 smallest frequencies. Handle case where (n-1) mod 2 ≠ 0 (may need dummy nodes with frequency 0).  
    *Edge cases:* Number of symbols not compatible with ternary structure.

13. **Optimal Binary Search Tree vs Huffman**  
    *Problem:* Explain why greedy (Huffman-like merging) doesn't work for optimal BST.  
    *Focus:* Identify dependency: In BST, middle element choice affects left and right subtree structures (not independent). In Huffman, merging two leaves doesn't constrain other merges.

**Fractional Knapsack Variants**

14. **Fractional Knapsack with Item Limits**  
    *Problem:* Each item can be taken at most k_i times (still fractionally). Capacity W.  
    *Focus:* Sort by value/weight ratio. At each step, take min(remaining_capacity, k_i * w_i) of highest-ratio item.  
    *Edge cases:* Item limits exhausted before capacity, limits = 1 (becomes selection problem).

15. **Continuous Knapsack with Constraints (e.g., Budget + Weight)**  
    *Problem:* Items have weights, values, and costs. Maximize value subject to weight ≤ W and cost ≤ C.  
    *Focus:* Recognize this may not admit pure greedy (two constraints). Consider LP relaxation or heuristic greedy.  
    *Key insight:* Multiple constraints often break greedy optimality.

**Job Sequencing Variants**

16. **Job Sequencing with Priorities (Different Objectives)**  
    *Problem:* Minimize maximum lateness—each job has processing time and deadline.  
    *Focus:* Greedy rule: Earliest Deadline First (EDF). Sort by deadline, schedule in that order. Prove via exchange argument.  
    *Edge cases:* Jobs with zero processing time, negative slack.

17. **Job Sequencing with Non-Unit Times**  
    *Problem:* Jobs have durations d_i, deadlines D_i, and profits p_i. Maximize profit.  
    *Focus:* This is NP-hard in general (like weighted activity selection). Recognize greedy alone won't solve optimally—need DP or approximation.  
    *Key insight:* Unit-time assumption is crucial for greedy optimality in job sequencing.

**Counterexample Construction**

18. **Generate Counterexamples for Wrong Greedy Rules**  
    *Problem:* For activity selection, prove each of these greedy rules is incorrect by constructing minimal counterexamples:
    - (A) Sort by earliest start time
    - (B) Sort by shortest duration
    - (C) Sort by latest finish time (for maximizing count)  
    *Focus:* For each, create 3–5 activities where the rule fails. Trace both greedy and optimal solutions.  
    *Key insight:* Systematic adversarial case design.

### Stage 2 checklist

After completing Stage 2 problems:

- [ ] I can write a detailed exchange argument for activity selection (300+ words).
- [ ] I can identify when a problem variant breaks the greedy choice property and requires DP.
- [ ] I can generate counterexamples for at least 3 incorrect greedy rules.
- [ ] I can handle boundary cases (ties, empty input, capacity exactly filled) confidently.
- [ ] I understand how multiple constraints or weights often necessitate DP over greedy.

---

## Stage 3: Integration & Mixed Paradigms

**Goal:** Decide when greedy is appropriate vs DP/backtracking, combine multiple greedy ideas, and apply greedy reasoning to systems problems.

### Key skills in Stage 3

- **Paradigm selection:** Given a problem description, rapidly determine if greedy, DP, backtracking, or hybrid is needed.
- **Proof strategy choice:** Decide between exchange argument, stays-ahead, or induction based on problem structure.
- **Systems reasoning:** Recognize greedy heuristics in real-world systems (caching, scheduling, routing).
- **Approximation awareness:** Understand when greedy gives approximate solutions for NP-hard problems (set cover, vertex cover).

### Practice problems — Stage 3

**Paradigm Mixing: Greedy + DP**

19. **Best Time to Buy and Sell Stock (Multiple Transactions)**  
    *Problem:* Given stock prices, maximize profit with unlimited transactions (buy-sell pairs).  
    *Focus:* Greedy insight: sum all positive differences (price[i+1] - price[i] > 0). Prove via decomposition.  
    *Variant:* With transaction fee (becomes DP).

20. **Minimum Cost to Merge Stones (Greedy Substructure)**  
    *Problem:* Given piles of stones, merge adjacent piles with cost = sum of stones. Minimize total cost.  
    *Focus:* Recognize this is *not* greedy (like Huffman)—requires DP because merge order is constrained by adjacency. Contrast with Huffman where any two can merge.  
    *Key insight:* Spatial constraints break greedy freedom.

**Graph Greedy: MST and Shortest Path**

21. **Kruskal's Algorithm Proof**  
    *Problem:* Prove Kruskal's algorithm (greedy by edge weight, adding if no cycle) produces MST.  
    *Focus:* Use cut property: for any cut, the lightest edge crossing the cut is in some MST. Greedy choice = lightest available edge.  
    *Proof:* Exchange argument or induction on number of edges added.

22. **Dijkstra's Algorithm Correctness**  
    *Problem:* Prove Dijkstra (greedy by current distance estimate) is correct for non-negative edge weights.  
    *Focus:* Greedy choice = finalize node with smallest tentative distance. Invariant: finalized distances are optimal. Proof: assume contradiction—if first finalized node u is incorrect, derive contradiction via path structure.  
    *Edge case:* Why negative edges break the invariant.

**Systems Greedy: Caching and Scheduling**

23. **LRU Cache Implementation and Analysis**  
    *Problem:* Implement LRU (Least Recently Used) cache. Analyze when LRU is optimal vs when it fails.  
    *Focus:* LRU is greedy temporal heuristic. Optimal (Bélády's algorithm) requires future knowledge. LRU approximates well for temporal locality.  
    *Real-world:* OS page replacement, CPU caches.

24. **Task Scheduling on Multiple Machines**  
    *Problem:* Given n tasks with processing times and m machines, minimize makespan (time when last task finishes).  
    *Focus:* Greedy: assign each task to machine with smallest current load. This is 2-approximation for makespan minimization (not optimal but provably close).  
    *Proof sketch:* Longest task defines lower bound; greedy load balancing stays within 2x.

**Approximation Algorithms**

25. **Greedy Set Cover**  
    *Problem:* Given universe U and subsets S_1, ..., S_m, find minimum number of subsets to cover U.  
    *Focus:* Greedy: repeatedly pick subset covering most uncovered elements. This is O(log n)-approximation.  
    *Proof intuition:* Counting argument—each iteration covers a substantial fraction of remaining elements.  
    *Real-world:* Facility location, network coverage.

26. **Greedy Vertex Cover (2-Approximation)**  
    *Problem:* Given graph, find minimum vertex set such that every edge is incident to at least one vertex in set.  
    *Focus:* Greedy: pick an uncovered edge, add both endpoints to vertex cover, remove covered edges. This is 2-approximation.  
    *Proof:* Optimal must cover each edge → optimal ≥ number of disjoint edges picked. Greedy uses 2x that.

**Paradigm Decision Framework**

27. **Meta-Problem: Paradigm Selection**  
    *Given problem descriptions, classify as:*
    - (A) Pure greedy
    - (B) DP (greedy fails)
    - (C) Greedy + DP hybrid
    - (D) Backtracking / branch-and-bound
    - (E) NP-hard (approximate with greedy)

    *Examples to classify:*
    - Maximize non-overlapping intervals (A)
    - Maximize weighted non-overlapping intervals (B)
    - Fractional knapsack (A)
    - 0/1 knapsack (B)
    - Huffman coding (A)
    - Optimal BST (B)
    - Job sequencing with unit times (A)
    - Job sequencing with arbitrary durations (E, use greedy heuristic)
    - TSP (E)
    - Dijkstra on non-negative graphs (A)
    - Bellman-Ford on graphs with negative edges (B/C)

**Integration Problems**

28. **Gas Station Circuit (Greedy Feasibility Check)**  
    *Problem:* Given circular route with gas stations, each station has gas[i] and cost[i] to reach next. Find starting station to complete circuit, or determine impossible.  
    *Focus:* Greedy insight: if total gas < total cost, impossible. Otherwise, start from station where cumulative surplus first becomes non-negative again after deficit.  
    *Proof:* Single-pass greedy with cumulative balance tracking.

29. **Jump Game (Greedy Reachability)**  
    *Problem:* Given array where arr[i] = max jump length from i, determine if you can reach the last index.  
    *Focus:* Greedy: maintain max reachable index. If current index > max reachable, return false.  
    *Variant:* Minimum jumps (becomes DP or greedy with BFS-like levels).

30. **Container with Most Water (Greedy Two-Pointer)**  
    *Problem:* Given heights, find two lines that form container with maximum area.  
    *Focus:* Greedy two-pointer: start at both ends, move pointer with smaller height inward. Prove this doesn't miss optimal.  
    *Proof:* Moving taller pointer can't improve (width decreases, height limited by shorter).

### Stage 3 checklist

After completing Stage 3 problems:

- [ ] I can distinguish problems where greedy is optimal vs approximate vs inappropriate.
- [ ] I can explain MST and Dijkstra's algorithms as greedy and sketch their proofs.
- [ ] I can identify greedy heuristics in real systems (LRU, load balancing, routing).
- [ ] I can decide between exchange argument, stays-ahead, and induction based on problem structure.
- [ ] I can construct hybrid solutions combining greedy with DP or other paradigms.

---

## Common problem-solving pitfalls (and how to avoid them)

### Pitfall 1: Assuming greedy works without proof

**Symptom:** "This greedy rule feels right, so I'll code it."  
**Why it fails:** Many intuitive greedy rules are incorrect (e.g., shortest duration for activity selection).  
**Fix:** Always sketch a 2-minute proof outline before coding. If you can't, generate test cases to try to break your rule.

### Pitfall 2: Using wrong sorting key

**Symptom:** Sorting by start time instead of finish time in activity selection.  
**Why it fails:** Start time doesn't align with objective (maximizing count of non-overlapping activities).  
**Fix:** Ask: "What property of this choice makes it safe? Why does finishing early matter?" Justify sort key with respect to objective.

### Pitfall 3: Not handling ties or boundary cases

**Symptom:** Greedy algorithm works on main cases but fails on edge cases (all same finish time, capacity exactly filled, etc.).  
**Why it fails:** Proof may be correct in general but implementation misses boundary logic.  
**Fix:** Explicitly enumerate edge cases before coding. Test on tiny inputs (n=1, n=2, all equal values, ties, empty input).

### Pitfall 4: Confusing fractional and 0/1 variants

**Symptom:** Applying ratio-greedy to 0/1 knapsack.  
**Why it fails:** 0/1 has combinatorial structure—local greedy choice can block globally better combination.  
**Fix:** Check if items are divisible. If must take entire item, suspect DP. Fractional → greedy; 0/1 → DP.

### Pitfall 5: Not recognizing when DP is needed

**Symptom:** Spending 30 minutes trying to find a greedy rule for weighted interval scheduling or optimal BST.  
**Why it fails:** These problems have overlapping subproblems that greedy can't navigate (local choice has global ripple effects).  
**Fix:** If you can't sketch a proof in 5 minutes and counterexamples emerge, pivot to DP. Look for "weighted", "maximize sum/product", or "dependencies between choices" signals.

### Pitfall 6: Ignoring termination or feasibility

**Symptom:** Greedy loop runs forever or selects infeasible elements.  
**Why it fails:** No explicit check for "can I still make progress?" or "is this choice valid?"  
**Fix:** Define termination condition clearly (all items processed, capacity full, no more compatible activities). Add feasibility predicate to each greedy step.

### Pitfall 7: Over-complicating the proof

**Symptom:** Writing pages of formal proof when a simple exchange argument suffices.  
**Why it fails:** Interview time is limited; verbosity obscures the core idea.  
**Fix:** Structure proof in 3 parts: (1) Greedy choice property statement, (2) Exchange step (swap greedy into optimal), (3) Induction/recursion to full solution. Keep each part to 3-5 sentences.

---

## Pattern templates (pseudocode skeletons)

### Template 1: Greedy by sorting

```
Algorithm Greedy_Selection(items, objective)
  1. Sort items by criterion aligned with objective
  2. Initialize solution = empty
  3. Initialize state (e.g., last_finish_time, capacity_used)
  4. For each item in sorted order:
       If item is feasible given current state:
         Add item to solution
         Update state
  5. Return solution

Time: O(n log n) for sort + O(n) for iteration = O(n log n)
Space: O(1) or O(n) depending on solution representation
```

**Examples:** Activity selection (sort by finish), fractional knapsack (sort by ratio), job sequencing (sort by profit).

### Template 2: Greedy with priority queue (online/streaming)

```
Algorithm Greedy_PQ(stream)
  1. Initialize priority queue PQ (min-heap or max-heap)
  2. Initialize solution
  3. While stream has elements or PQ not empty:
       If new element arrives:
         Insert into PQ
       Extract top element from PQ
       If feasible:
         Add to solution
         Update state
  4. Return solution

Time: O(n log n) for n insertions/extractions
Space: O(k) for PQ size k (often k << n)
```

**Examples:** Huffman coding (merge two smallest repeatedly), Dijkstra (extract min distance node), event-driven scheduling.

### Template 3: Greedy stays ahead proof structure

```
Proof Structure (Greedy Stays Ahead)

Goal: Show greedy solution G is at least as good as any optimal solution O.

1. Define progress metric m(S, k) for solution S at step k
   Example: For activity selection, m(S, k) = finish time of k-th activity in S

2. Base case: m(G, 1) <= m(O, 1)
   (Greedy's first choice finishes no later than optimal's first choice)

3. Inductive step: Assume m(G, k) <= m(O, k). Show m(G, k+1) <= m(O, k+1)
   Argument: Since G_k finishes no later than O_k, G can select at least as many 
   compatible activities from remaining set as O can.

4. Conclusion: G contains at least as many activities as O (or achieves same objective).

Typical use: Scheduling, interval problems where "being ahead" is well-defined.
```

### Template 4: Exchange argument proof structure

```
Proof Structure (Exchange Argument)

Goal: Show greedy solution G is optimal.

1. Let O be any optimal solution.
2. Transform O into G step-by-step, maintaining optimality at each step.
3. Exchange step:
   - Identify first position i where O and G differ.
   - Replace O[i] with G[i] (the greedy choice).
   - Verify: Objective value doesn't decrease, feasibility preserved.
4. Recursively apply to remaining positions.
5. Conclusion: O can be transformed into G without loss, so G is optimal.

Typical use: Activity selection, Huffman coding, fractional knapsack, MST.
```

---

## Decision matrix: When to use which greedy approach

| Problem Characteristics | Greedy Likely Works | Proof Strategy | Example |
|-------------------------|---------------------|----------------|---------|
| **Optimize count/number of items** | ✅ Yes | Exchange or stays-ahead | Activity selection |
| **Optimize sum with divisible items** | ✅ Yes | Exchange on ratios | Fractional knapsack |
| **Optimize tree structure with local merges** | ✅ Yes | Exchange on tree | Huffman coding |
| **Optimize with deadlines/slots** | ✅ Yes (if unit-time) | Greedy choice property | Job sequencing |
| **Optimize weighted sum (non-divisible)** | ❌ No (DP needed) | - | Weighted interval scheduling, 0/1 knapsack |
| **Optimize with spatial constraints (adjacency)** | ❌ No (DP needed) | - | Optimal BST, merge stones |
| **Optimize with multiple conflicting constraints** | ❌ No (LP/DP needed) | - | Multi-dimensional knapsack |
| **Graph: MST** | ✅ Yes | Cut property, exchange | Kruskal, Prim |
| **Graph: Shortest path (non-negative)** | ✅ Yes | Induction on finalized nodes | Dijkstra |
| **Graph: Shortest path (negative edges)** | ❌ No (DP needed) | - | Bellman-Ford |
| **NP-hard (want approximation)** | ⚠️ Greedy heuristic | Approximation ratio analysis | Set cover, vertex cover, TSP |

### Quick decision heuristic

1. **Can you sort by a single criterion that aligns with the objective?** → Try greedy
2. **Are items divisible or decisions independent?** → Try greedy
3. **Do you see "weighted" or "maximize sum with constraints"?** → Suspect DP
4. **Can you sketch a proof (exchange/stays-ahead) in 2 minutes?** → Greedy is safe
5. **Do counterexamples emerge quickly?** → Abandon greedy, use DP/backtracking

---

## Weekly integration: Combining greedy with prior weeks

### Greedy + Sorting (Week 7)

- Many greedy algorithms begin with sorting. Complexity often O(n log n) dominated by sort.
- Custom comparators: e.g., sort intervals by finish time, jobs by profit.

### Greedy + Heaps (Week 8)

- Huffman coding uses min-heap for efficient "find two smallest" operations.
- Dijkstra uses min-heap for "extract next closest node."
- Event-driven greedy (sweep line) often uses heap for next event.

### Greedy + Graphs (Week 10-11)

- MST algorithms (Kruskal, Prim) are greedy graph algorithms.
- Dijkstra is greedy single-source shortest path for non-negative weights.
- Greedy heuristics for NP-hard graph problems (vertex cover, set cover).

### Greedy vs DP (Week 13-14)

- Key distinction: DP needed when greedy choice property fails (global interactions, weights, overlapping subproblems with dependencies).
- Fractional knapsack (greedy) vs 0/1 knapsack (DP).
- Activity selection (greedy) vs weighted interval scheduling (DP).

---

## Reflection prompts

After completing Week 12, reflect on these questions:

1. **Paradigm intuition:** When you see a new optimization problem, what are the first three signals you look for to decide if greedy might work?

2. **Proof confidence:** Can you now write a coherent exchange argument for activity selection in under 10 minutes? What about Huffman coding?

3. **Counterexample generation:** How quickly can you generate a small adversarial case to test a proposed greedy rule? What's your systematic approach?

4. **Real-world connection:** Identify one greedy algorithm you use daily (OS scheduler, cache eviction, network routing). What's the greedy choice and why is it reasonable?

5. **Integration with DP:** When you solve a DP problem in future weeks, can you articulate why greedy *doesn't* work for it? What structural property breaks the greedy choice property?

---

## Summary: Your Week 12 problem-solving journey

**By the end of this week, you should be able to:**

- ✅ Recognize canonical greedy patterns (activity selection, Huffman, fractional knapsack, job sequencing).
- ✅ Apply standard greedy algorithms confidently and correctly.
- ✅ Construct exchange arguments and stays-ahead proofs for greedy correctness.
- ✅ Generate counterexamples to incorrect greedy rules rapidly.
- ✅ Distinguish when greedy is optimal vs when DP or approximation is needed.
- ✅ Combine greedy with other paradigms (sorting, heaps, graphs) effectively.

**Progress through the stages at your own pace.** If Stage 1 takes 2 days, that's fine—depth matters more than speed. If you breeze through Stage 2, dive into Stage 3 early.

The goal is **interview-ready mastery**: you can solve, explain, and prove greedy solutions confidently, and you know when to pivot to DP or backtracking.

---

**Next Steps:**  
- Start with Stage 1 problems (activity selection, fractional knapsack, Huffman).  
- Use Week_12_Interview_QA_Reference.md for conceptual review (30-50 questions, no answers).  
- Use Week_12_Daily_Progress_Checklist.md for daily execution tracking.  
- Use Week_12_Summary_Key_Concepts.md for quick reference and insights.

Good luck, and remember: **Greedy is elegant when it works, but proving it works is the real skill.**
