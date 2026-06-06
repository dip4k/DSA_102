# Week 12 Daily Progress Checklist — Greedy Algorithms & Proofs

**Week:** 12  
**Phase:** 🟧 Algorithm Paradigms  
**Focus:** Daily execution plan for mastering greedy algorithms and their proofs.

Use this checklist as a **planner and tracker**. Each day has:
- Target concepts
- Concrete actions (reading, tracing, implementation, proofs)
- Reflection prompts

Tick items as you complete them.

---

## Day 1 — Greedy Fundamentals & Proof Templates

**Goal for the day:** Understand what makes an algorithm “greedy,” and internalize the main proof strategies.

### 1. Concepts to Cover

- [ ] Definition of a **greedy algorithm**.  
- [ ] **Greedy choice property** and **optimal substructure**.  
- [ ] Generic **greedy algorithm template** (sort → iterate → select if feasible).  
- [ ] **Exchange argument** proof pattern.  
- [ ] **Greedy stays ahead** proof pattern.  
- [ ] Using **induction** with greedy solutions.

### 2. Core Reading / Watching

- [ ] Read the Week 12 overview and Day 1 instructional material on greedy fundamentals.  
- [ ] As you read, highlight sentences that describe **why** a particular greedy rule is safe (not just how to implement it).

### 3. Hands-On Activities

- [ ] Write down your own **generic greedy skeleton** in pseudocode.
- [ ] Pick a simple selection problem (for example: “from a list of intervals, pick the most that do not overlap”) and:
  - [ ] Propose at least **two different greedy rules**.  
  - [ ] For each rule, generate **two or three small test cases**, including corner cases (ties, nested intervals).

### 4. Proof Practice

- [ ] Choose one of your candidate rules that appears to work.  
- [ ] Write a **5–10 line exchange argument or stays-ahead proof sketch** for it.  
- [ ] Identify clearly:  
  - [ ] What is the **local greedy choice**?  
  - [ ] What is the **progress metric** or **prefix position** used in your proof?

### 5. Reflection

- [ ] In your own words, answer: *“What kinds of problems seem structurally suitable for greedy?”*  
- [ ] Write down at least **two reasons** greedy might fail for a problem, even if it feels intuitive.

---

## Day 2 — Activity Selection & Interval Problems

**Goal for the day:** Become comfortable applying greedy ideas to interval scheduling and related variants.

### 1. Concepts to Cover

- [ ] Formal statement of **activity selection** (maximize non-overlapping intervals).  
- [ ] Greedy rule: **sort by finish time** and select compatible activities.  
- [ ] Why sorting by **start time** or **duration** is not generally correct.  
- [ ] Relationship between **“max intervals kept”** and **“min intervals removed”**.  
- [ ] High-level idea of **meeting rooms / minimum number of classrooms** via sweep line.

### 2. Core Reading / Tracing

- [ ] Read the Day 2 material on activity selection and interval problems.  
- [ ] Trace the classic greedy algorithm on at least **two example interval sets** by hand:
  - [ ] One with clear gaps.  
  - [ ] One with heavy overlap and ties.

### 3. Implementation Tasks

- [ ] Implement the **activity selection greedy algorithm** (pseudo-code or code).  
- [ ] Add comments describing:  
  - [ ] The **sort key** (finish time).  
  - [ ] The **state** you maintain (`last_finish_time`).

### 4. Variant Exploration

- [ ] Consider the **“minimum intervals to remove”** problem:  
  - [ ] Restate it as “maximum intervals to keep” and use activity selection as core.  
- [ ] Read or recall the **meeting rooms** problem:  
  - [ ] Understand events (start/end) and how a counter tracks active meetings.

### 5. Reflection

- [ ] Write a short paragraph explaining the **exchange argument** for activity selection in your own words.  
- [ ] Note at least **one situation** where your intuition might incorrectly suggest a different rule (like “shortest interval first”).

---

## Day 3 — Huffman Coding & Optimal Trees

**Goal for the day:** Understand Huffman coding as a canonical example of tree-based greedy optimization.

### 1. Concepts to Cover

- [ ] Definition and goal of **Huffman coding** (optimal prefix-free code).  
- [ ] Prefix-free codes and why they matter for decoding.  
- [ ] Huffman algorithm steps: use of **min-heap** and bottom-up tree construction.  
- [ ] Idea that **two least frequent symbols** become siblings at maximal depth in some optimal tree.

### 2. Manual Simulation

- [ ] Choose a small set of symbols with frequencies (e.g., 4–6 symbols).  
- [ ] Build the Huffman tree by hand:  
  - [ ] Maintain a list or min-heap of current nodes.  
  - [ ] At each step, pick two smallest, merge, reinsert.  
- [ ] Write down final codes for each symbol and compute:  
  - [ ] Average code length = sum (frequency × length).

### 3. Implementation Tasks

- [ ] Sketch or implement the Huffman algorithm using a priority queue.  
- [ ] Ensure you understand:  
  - [ ] Node structure (symbol / frequency / left / right).  
  - [ ] Tree traversal to generate bitstrings.

### 4. Proof Intuition

- [ ] Write **2–3 bullet points** describing why merging the two least frequent nodes is safe:  
  - [ ] Think in terms of “pushing rare symbols deeper” and **exchange arguments** on tree structure.

### 5. Reflection

- [ ] Compare Huffman coding to any other tree algorithm you know (e.g., BST or AVL).  
- [ ] Answer: *“Why does Huffman admit a clean greedy solution, while optimal BSTs usually require DP?”*

---

## Day 4 — Fractional Knapsack, 0/1 Knapsack & Job Sequencing

**Goal for the day:** Separate where greedy is truly optimal (fractional knapsack, job sequencing) from where it fails (0/1 knapsack).

### 1. Concepts to Cover

- [ ] Definition of **fractional knapsack**.  
- [ ] Definition of **0/1 knapsack**.  
- [ ] Greedy rule for fractional knapsack: **sort by value/weight ratio**.  
- [ ] Why this rule fails for 0/1 knapsack (combinatorial interactions).  
- [ ] Job sequencing with deadlines and profits: schedule jobs in slots.

### 2. Manual Problem Solving

- [ ] Solve at least **two fractional knapsack** instances by hand:  
  - [ ] Sort items by ratio.  
  - [ ] Fill capacity and compute total value.  
- [ ] Construct or review a tiny **counterexample** where ratio-greedy fails for 0/1 knapsack.

### 3. Implementation Tasks

- [ ] Implement fractional knapsack (pseudo or code), documenting:  
  - [ ] Sorting by ratio.  
  - [ ] Handling partial inclusion of the last item.
- [ ] Outline algorithm for **job sequencing with deadlines**:  
  - [ ] Sort jobs by profit descending.  
  - [ ] For each job, place it in the latest free slot before its deadline.

### 4. Proof/Reasoning Practice

- [ ] Write a short (5–8 line) **exchange argument** for why value/weight ratio is optimal in the fractional case.  
- [ ] Summarize in 2–3 sentences why the same reasoning does **not** apply to 0/1 knapsack.

### 5. Reflection

- [ ] In one paragraph, compare **“fractional → greedy”** vs **“0/1 → DP”** to solidify the distinction.  
- [ ] Note at least one real-world scenario that maps naturally to each variant.

---

## Day 5 (Optional) — Greedy in Systems & Approximation

**Goal for the day:** Recognize how greedy ideas appear in real-world systems and approximation algorithms.

### 1. Concepts to Cover

- [ ] MST algorithms (Kruskal, Prim) as greedy.
- [ ] Dijkstra as greedy with non-negative edges.
- [ ] LRU (Least Recently Used) as a greedy cache eviction policy.
- [ ] Greedy set cover and O(log n) approximation idea (high level).

### 2. Systems-Oriented Activities

- [ ] Revisit MST and shortest path algorithms from earlier weeks and explicitly **label the greedy step** in each.  
- [ ] Take a short page reference sequence and simulate **LRU**, noting when and why pages are evicted.

### 3. Approximation Thinking (Lightweight)

- [ ] Read a short description of greedy set cover:  
  - [ ] At each step, pick the set covering the largest number of uncovered elements per unit cost.
- [ ] Think about how this is similar in spirit to other greedy rules (local benefit per resource).

### 4. Reflection

- [ ] Identify **one system** you use daily (e.g., OS scheduler, cache, load balancer) and describe what might be “greedy” about its behavior.  
- [ ] Answer: *“Why might engineers accept a greedy heuristic over an exact but slow algorithm here?”*

---

## Weekly Wrap-Up Checklist

At the end of Week 12, confirm:

### Conceptual Mastery

- [ ] I can define a greedy algorithm and distinguish it from DP and backtracking.  
- [ ] I can explain the **greedy choice property** and **optimal substructure** clearly.  
- [ ] I recognize at least **two proof techniques** for greedy algorithms (exchange, stays-ahead, induction).

### Canonical Problems

- [ ] I can solve and explain **activity selection** with an exchange argument.  
- [ ] I can describe **Huffman coding** and why merging the two least frequent nodes is optimal.  
- [ ] I can implement **fractional knapsack** and understand the failure of greedy for **0/1 knapsack**.  
- [ ] I can outline the greedy algorithm for **job sequencing with deadlines** and its intuition.

### Structural Judgment

- [ ] Given a new optimization problem, I can articulate **why** greedy might or might not be suitable.  
- [ ] I can produce at least **one counterexample** when a naive greedy rule fails.  
- [ ] I know when to prefer **DP** or **backtracking** over greedy.

If these boxes are checked, you’ve completed Week 12 with a solid, interview-ready understanding of greedy algorithms and their proofs.
