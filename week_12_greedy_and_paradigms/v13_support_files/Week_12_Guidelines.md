# Week 12 Guidelines — Greedy Algorithms & Proofs

**Week:** 12  
**Phase:** 🟧 Algorithm Paradigms (Greedy & Backtracking)  
**Theme:** Learning when “locally best” is actually globally optimal — and when it is not.

---

## 1. Weekly Overview & Learning Arc

Week 12 is your deep dive into **greedy algorithms** as a first-class design paradigm, not just a bag of tricks.

You will:
- Learn how to **spot problems** where a greedy rule is promising.
- Practice designing the **right local choice** (sort key, priority rule).
- Build the habit of **proving correctness** with exchange arguments, “greedy stays ahead,” and induction.
- Contrast **true greedy successes** (activity selection, fractional knapsack, Huffman, MST) with **classic failures** (0/1 knapsack, weighted interval scheduling without DP).
- Connect the theory to **real systems** (schedulers, networks, caches, approximations like set cover).

The week is short in clock hours but dense in conceptual weight. Treat it as a “pattern recognition upgrade” for everything you’ve learned so far: graphs, DP, scheduling, compression.

High-level flow:
- **Day 1:** Greedy fundamentals, templates, and correctness proofs.  
- **Day 2:** Activity selection and interval scheduling families.  
- **Day 3:** Huffman coding and optimal prefix trees.  
- **Day 4:** Fractional knapsack and job sequencing with deadlines.  
- **Day 5 (Optional):** Greedy in systems and approximation algorithms.

Your goal: by the end of this week you should be able to **propose, stress-test, and either prove or kill a greedy idea** in under 15 minutes for a new problem.

---

## 2. Detailed Learning Objectives

By the end of Week 12, you should be able to:

1. **Explain the greedy paradigm clearly**  
   - Define what a greedy algorithm is and how it differs from dynamic programming and backtracking.
   - Articulate the **greedy choice property** and **optimal substructure** in your own words.

2. **Use a generic greedy design template**  
   - Model problems in terms of **items + local choices + feasibility constraints**.  
   - Choose sort keys or priority rules that reflect the **true objective**, not just intuition.

3. **Prove correctness (or non-correctness)**  
   - Write exchange arguments for problems like **activity selection** and **fractional knapsack**.  
   - Use “greedy stays ahead” arguments for interval scheduling and scheduling-like problems.  
   - Recognize counterexamples where naive greedy fails and explain *why*.

4. **Master interval-based greedy patterns**  
   - Implement and reason about **activity selection**, **minimum intervals to remove**, and **meeting room / classroom allocation**.  
   - Understand when you must switch to **DP** (for example, **weighted interval scheduling**).

5. **Understand Huffman coding as a greedy tree construction**  
   - Construct Huffman trees using a min-heap.  
   - Explain why **combining the two least frequent symbols** is a safe greedy move.  
   - Connect Huffman coding to general ideas of optimal trees and compression.

6. **Differentiate fractional vs 0/1 knapsack at a structural level**  
   - Show why **value/weight ratio** is the right key for fractional knapsack.  
   - Provide small counterexamples where the same strategy fails for 0/1 knapsack.  
   - Know when to apply **DP instead of greedy**.

7. **Apply greedy scheduling ideas to jobs with deadlines and profits**  
   - Implement **job sequencing with deadlines** using a greedy rule and appropriate data structures.  
   - Explain informally why “schedule each job as late as possible” preserves flexibility.

8. **Recognize greedy behavior in real systems**  
   - See **MST algorithms (Kruskal, Prim)** and **Dijkstra** as greedy.  
   - Understand **LRU caching** and simple routing heuristics as greedy in time or space.  
   - See how greedy appears in **approximation algorithms** (for example, set cover).

9. **Decide between greedy, DP, and backtracking for new problems**  
   - Use structural signals (local decisions, overlapping subproblems, small state spaces) to pick the right paradigm quickly.  
   - Be comfortable abandoning a greedy idea when you can’t support it with a proof.

10. **Communicate greedy reasoning in interviews**  
   - Present your algorithm with a clear **local rule + proof sketch**.  
   - Handle follow-up questions about corner cases, counterexamples, and complexity with confidence.

---

## 3. Day-by-Day Strategy

### Day 1 — Greedy Fundamentals & Proof Strategies

**Main topics:**
- Greedy concept, greedy choice property, optimal substructure.  
- Generic greedy algorithm template.  
- Exchange arguments, greedy stays ahead, induction-style proofs.

**How to study:**
- Read through the conceptual material slowly; **do not code first**.  
- For each proof technique (exchange argument, stays-ahead), write out a *generic pattern*.  
- Pick at least one simple problem (e.g., “max non-overlapping intervals” or a toy selection problem) and:
  - First, write a plain-English description of a candidate greedy rule.  
  - Then, write a short correctness proof sketch (5–10 lines max).  
- Finish the day by summarizing **when you would *not* trust a greedy idea** (for example, lots of coupling between decisions, complicated constraints).

### Day 2 — Activity Selection & Interval Problems

**Main topics:**
- Classic **activity selection** (maximize non-overlapping intervals).  
- Variants: **minimum removals**, **meeting rooms**, **interval coverage**.  
- Greedy stays ahead vs exchange arguments in interval settings.

**How to study:**
- Work through the **standard activity selection proof**. Make sure you can:
  - Explain *why* sorting by finish time works.  
  - Explain *why* sorting by start time or duration generally does not.  
- Solve at least 2–3 interval variants, and for each:
  - List at least **two competing greedy rules**.  
  - Intentionally try to **break** each rule with counterexamples.  
  - Choose the survivor and attempt a brief correctness argument.

### Day 3 — Huffman Coding & Optimal Trees

**Main topics:**
- Huffman coding problem (optimal prefix-free codes).  
- Building Huffman tree via **repeated merges of smallest frequencies**.  
- Exchange argument for tree restructuring.

**How to study:**
- Hand-simulate Huffman coding on a small set of characters with given frequencies.  
- Write down:
  - How the min-heap evolves.  
  - The final tree, and the codes for each symbol.  
- Then focus on the **proof idea**:
  - Why must the least frequent symbols be deepest in some optimal tree?  
  - How does that justify merging the two least frequent at every step?

### Day 4 — Fractional Knapsack & Job Sequencing

**Main topics:**
- Fractional vs 0/1 knapsack.  
- Value/weight ratio as greedy key.  
- Job sequencing with deadlines and profits.

**How to study:**
- Solve multiple knapsack instances by hand:
  - One where **ratio-greedy** is optimal (fractional).  
  - One where **ratio-greedy fails** for 0/1 knapsack.  
- Translate job sequencing into “time slots” thinking:
  - Sort by profit descending.  
  - For each job, schedule in the latest free slot before its deadline.  
- Reflect on why “placing jobs as late as possible” is a recurring idea in scheduling.

### Day 5 (Optional) — Greedy in Systems & Approximation

**Main topics:**
- Greedy in networks (MST, routing).  
- Greedy in caching (LRU).  
- Approximation algorithms (e.g., set cover).

**How to study:**
- Revisit **Kruskal / Prim** from Week 9, now explicitly labeling them as greedy.  
- Simulate **LRU** and compare with an idealized “optimal cache” on a small reference string.  
- Read about **greedy set cover** and understand at a high level why it has an **O(log n)** approximation factor.

---

## 4. Study Strategy & Time Allocation

This week is concept-heavy but relatively short in raw coding time. A good **90-minute daily block** can be split as:

- **30–40 minutes — Concept & Proofs**  
  - Read, annotate, and summarize key ideas (greedy choice property, proof templates).  
  - Write at least one proof sketch per day.

- **30–40 minutes — Hands-on Problems**  
  - Implement or pseudo-code 1–2 canonical algorithms (activity selection, Huffman, fractional knapsack, job sequencing).  
  - For each implementation, describe the **local rule** and **invariant** in comments or notes.

- **10–20 minutes — Reflection & Integration**  
  - Ask: “Could this problem be solved by DP or backtracking instead? Why did greedy work here?”  
  - Capture 1–2 “signals” that would clue you in for future problems.

If you are short on time:
- Prioritize **Days 1–4** (core theory + canonical problems).  
- Treat Day 5 as enrichment for systems and approximation awareness.

---

## 5. Common Pitfalls This Week (and How to Avoid Them)

1. **Treating greedy as a black box pattern**  
   - *Pitfall:* Memorizing “sort by finish time” or “sort by ratio” without understanding why.  
   - *Fix:* For each greedy rule, always answer: **What invariant does this ordering maintain?**

2. **Skipping the proof**  
   - *Pitfall:* Coding immediately once a greedy rule “feels right.”  
   - *Fix:* Force a 5-minute “proof sketch pause” before coding. If you cannot sketch an argument, reconsider.

3. **Ignoring counterexamples**  
   - *Pitfall:* Not actively trying to break your idea.  
   - *Fix:* For every greedy rule, **design adversarial tests**: lots of ties, nested intervals, extreme weights.

4. **Confusing fractional and 0/1 knapsack**  
   - *Pitfall:* Applying ratio-greedy to 0/1 knapsack because it works for fractional.  
   - *Fix:* Keep a clear mental note: *“Fractional → greedy; 0/1 → DP.”* Memorize one counterexample.

5. **Overlooking feasibility state**  
   - *Pitfall:* Forgetting to maintain constraints (capacity, time slots, non-overlap) while greedily selecting.  
   - *Fix:* Explicitly track state variables: `last_finish_time`, `remaining_capacity`, `occupied_slots`.

6. **Misusing interval orderings**  
   - *Pitfall:* Believing “shortest interval first” or “earliest start” is always best.  
   - *Fix:* Tie your ordering directly to the **objective** (e.g., maximizing count vs minimizing rooms).

7. **Not connecting to prior weeks**  
   - *Pitfall:* Treating greedy as a new, isolated module.  
   - *Fix:* Re-interpret Dijkstra, MST, and some DP problems through a greedy lens to see relationships.

---

## 6. Weekly Checklist

Use this checklist to confirm you have actually internalized Week 12.

### Conceptual Checklist

- [ ] I can define a greedy algorithm and contrast it with DP and backtracking.
- [ ] I understand the **greedy choice property** and **optimal substructure**.
- [ ] I can explain the **generic greedy template** (sort, iterate, select if feasible).
- [ ] I know at least **two proof strategies** for greedy correctness (exchange, stays-ahead, induction).

### Problem-Specific Checklist

- [ ] **Activity selection:** I can state the algorithm (sort by finish time) and sketch an exchange argument.  
- [ ] **Interval variants:** I can solve “minimum removals” and “meeting rooms” with appropriate greedy or sweep line methods.  
- [ ] **Huffman coding:** I can build a Huffman tree by hand and explain why merging the two smallest frequencies is safe.  
- [ ] **Fractional knapsack:** I can argue why value/weight ratio is the right key and implement the algorithm.  
- [ ] **0/1 knapsack:** I know exactly why greedy fails and why DP is needed.  
- [ ] **Job sequencing:** I can implement the “high profit, place late” rule and justify it informally.

### Proof & Reasoning Checklist

- [ ] I have written at least **one full exchange argument** this week.  
- [ ] I have written at least **one greedy stays-ahead proof**.  
- [ ] I have created at least **two counterexamples** to kill naive greedy rules.  
- [ ] I can talk through a correctness sketch for at least **two canonical greedy algorithms** without notes.

### Systems & Integration Checklist

- [ ] I can see Kruskal and Prim as greedy algorithms driven by the **cut property**.  
- [ ] I understand LRU as a greedy cache policy and why it is heuristic but effective.  
- [ ] I have a basic idea of **greedy set cover** and its O(log n) approximation flavor.  
- [ ] I can decide, for a new problem, whether to first try greedy or DP, and explain my choice.

If you can check most of these boxes by the end of the week, you have achieved the **Week 12 goal: knowing when greedy is truly safe, and being able to prove it.**
