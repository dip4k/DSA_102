# 📋 WEEK 5 GUIDELINES & LEARNING STRATEGY

**Week 5: Tier 1 Critical Patterns — Master High-Frequency Interview Patterns**

---

## 🎯 Week Overview

Week 5 marks a critical turning point in the DSA curriculum. You are now transitioning from **foundational data structures** (Arrays, Linked Lists, Stacks, Queues) to **high-frequency algorithmic patterns** that dominate technical interviews.

This week introduces six **elite patterns** that collectively solve approximately **65-75% of medium-difficulty interview problems**:

1. **Hash Map / Hash Set** — Complement tracking, frequency counting, caching foundations
2. **Monotonic Stack** — Range queries, next/previous element problems, histogram logic
3. **Merge Operations & Intervals** — Scheduling, coordinate merging, overlap resolution
4. **Partition & Cyclic Sort** — In-place manipulations, constrained sorting
5. **Kadane's Algorithm** — Contiguous subarray optimization, DP principles
6. **Fast & Slow Pointers** — Cycle detection, geometry-based algorithms, O(1) space solutions

**Theme:** These patterns demonstrate **mathematical elegance and engineering pragmatism**. They show that small insights (monotonic property, relative speed, cycle geometry) unlock O(N) solutions to problems that naively cost O(N²) or O(N log N).

---

## 🔗 How Week 5 Connects

**Builds On:** Week 4 (Basic Patterns), Weeks 1-3 (Foundations)
- Week 4 taught you basic two-pointer, sliding window, and prefix-sum thinking.
- Week 5 **deepens and specializes** these concepts into distinct, powerful patterns.

**Foundation for:** Week 6 (Trees), Week 7 (Graphs), Week 8 (Dynamic Programming)
- Trees use monotonic stack for traversal optimization.
- Graphs use cycle detection and component finding (Floyd's extension).
- DP uses Kadane-like state transitions and optimization techniques.

---

## 🎓 Weekly Learning Objectives

By the end of Week 5, you will:

1. ✅ **Recognize** pattern signatures in interview questions (keywords, constraints, data structure hints).
2. ✅ **Apply** each pattern to 5-10 variations and constraint modifications.
3. ✅ **Explain** the mechanical invariants and trade-offs (time/space, simplicity/optimality).
4. ✅ **Trace** algorithms by hand on paper for examples and edge cases (no coding required initially).
5. ✅ **Compare** approaches: when to use pattern A vs. pattern B given constraints.
6. ✅ **Extend** patterns to novel problems (e.g., Kadane to 2D, cycle detection to graphs).
7. ✅ **Articulate** real-world uses and performance implications of each pattern.

---

## 📚 Key Concepts Overview

| Day | 🎯 Core Concept | 🔑 Key Variations | 💡 Key Insight |
|:---|:---|:---|:---|
| **1** | Hash Maps for Complements | Two Sum, Anagrams, Frequency | O(1) lookup replaces brute force |
| **2** | Monotonic Invariants | Next Greater, Histograms, Water | Single pass maintains order |
| **3** | Sort & Sweep Strategy | Intervals, Scheduling, Merges | Sorting collapses complexity |
| **4A** | In-Place Segregation | Dutch Flag, Cyclic Sort, Missing | O(1) space via clever indexing |
| **4B** | Greedy DP (Kadane) | Max Subarray, Max Product, Circular | Reset negative prefixes |
| **5** | Relative Motion Geometry | Cycle Detection, Midpoint, Duplicates | Two speeds collide at cycles |

---

## 🧭 Learning Approach & Methodology

### Phase 1: Understanding (Days 1-5, ~30 mins per day)
1. **Read the Core Analogy** (Section 2). Draw it on paper.
2. **Trace one example** using the state table (Section 4). Understand state transitions.
3. **Ask yourself:** Why does this work? What invariant holds?

### Phase 2: Mechanical Mastery (Days 1-5, ~45 mins per day)
1. **Trace 2-3 more examples** (provided in Section 4) without looking at the answer.
2. **Identify edge cases** and trace them (empty, single element, all same value, etc.).
3. **Draw the algorithm** as a flowchart or state diagram.

### Phase 3: Pattern Recognition (Days 2-5, ~20 mins per day)
1. **Read problem statements** from Section ⚔ (Practice Problems).
2. **Do NOT code yet.** Identify: Is this a [Hash] problem? [Monotonic]? [Kadane]?
3. **Explain aloud** why you chose that pattern.

### Phase 4: Problem Solving (Days 3-5, ~60 mins per day)
1. **Solve 2-3 problems** using the pattern from your instructional file.
2. **Write pseudocode first.** Only then implement.
3. **Compare your approach** to the mental model in the file. Did you use the right invariants?

### Phase 5: Deep Dives (End of Week, ~40 mins)
1. **Pick one pattern** that felt hardest (probably Monotonic Stack or Floyd's).
2. **Watch a video explanation** (see External Resources in instructional files).
3. **Teach it to someone** (or explain to yourself on recording). Teaching reveals gaps.

---

## 🧱 Common Mistakes & Pitfalls

### Mistake 1: "I'll just code it and see if it works"
**Why it's wrong:** Interview problems are testing your **understanding**, not your ability to guess syntax. Rushing to code without mental models leads to off-by-one errors and poor time/space trades.

**Fix:** Trace your algorithm on paper first. Explain invariants. **THEN** code.

---

### Mistake 2: "Monotonic Stack is too hard. I'll skip it."
**Why it's wrong:** Monotonic Stack shows up in 30+ LeetCode problems. Skipping it is like skipping binary search.

**Fix:** The trick isn't the stack—it's understanding **why** you reset when popping. The "Movie Theater" analogy makes it intuitive.

---

### Mistake 3: "I memorized the solution to this problem."
**Why it's wrong:** Interviews vary constraints. "Remove Nth Node" becomes a cycle detection problem if the list is circular. Memorization doesn't transfer.

**Fix:** Focus on **pattern recognition** and **invariants**. Solutions are byproducts.

---

### Mistake 4: "Kadane's is just dynamic programming. I don't need to learn it separately."
**Why it's wrong:** Kadane's teaches a specific **DP strategy**: Greedy local decisions that lead to global optimality. This intuition is foundational for week 8.

**Fix:** Understand why resetting negative prefixes is both greedy AND optimal.

---

### Mistake 5: "Floyd's Cycle Detection is too mathematical. I'll use a hash set."
**Why it's wrong:** Your solution works but uses O(N) space. Interview constraint: "O(1) space". Now you're stuck.

**Fix:** Learn Floyd's because it shows **geometry-based thinking**—a skill interviewers deeply respect.

---

## 🕒 Time & Practice Strategy

### Daily Time Budget: 2-3 Hours

```text
Daily Schedule (2.5 hours target):

09:00-09:20 (20 min)  → Read Instructional File (Section 1-2)
09:20-10:00 (40 min)  → Trace Examples + Mechanical Walkthrough (Section 3-4)
10:00-10:20 (20 min)  → Break / Recap core insight

10:20-11:00 (40 min)  → Practice Problem 1 (Pattern Recognition)
11:00-11:40 (40 min)  → Practice Problem 2 (Implementation)
11:40-12:00 (20 min)  → Reflection: Did I use the right invariants?

12:00-12:30 (30 min)  → Review prior day or prepare for next day
```

### Weekly Integration Strategy

| Day | Focus | Activities |
|:---|:---|:---|
| **1** | Hash Maps | Core concept + 2 problems |
| **2** | Monotonic Stack | Core concept + 2 problems + 1 harder (histogram) |
| **3** | Intervals | Core concept + 2 problems + connecting to Day 1 |
| **4A** | Partition/Cyclic Sort | Core concept + 2 problems |
| **4B** | Kadane | Core concept + 2 problems + 1 circular variation |
| **5** | Fast/Slow | Core concept + 2 problems + Floyd's derivation |
| **End** | Integration | 1 mixed problem (uses 2+ patterns from the week) |

---

## 📋 Weekly Checklist

Complete this checklist by end of week:

### Understanding Phase
- [ ] Day 1: Read Hash Map file. Understand "Complement" analogy.
- [ ] Day 2: Read Monotonic Stack file. Trace one example by hand.
- [ ] Day 3: Read Intervals file. Draw a merge diagram.
- [ ] Day 4A: Read Partition file. Trace Dutch National Flag.
- [ ] Day 4B: Read Kadane file. Understand "reset negative prefix" logic.
- [ ] Day 5: Read Fast/Slow file. Draw the collision scenario.

### Mechanical Phase
- [ ] Trace 2 examples per day without looking at answer tables.
- [ ] Identify and trace 1 edge case per day (empty, single element, all negatives, etc.).
- [ ] Create your own "cheat sheet" with key invariants for each pattern.

### Pattern Recognition Phase
- [ ] Read 15 problem statements from all 5 days' problem lists.
- [ ] For each, write down: "This is a [Pattern] problem because [reason]."
- [ ] Do this WITHOUT coding first.

### Implementation Phase
- [ ] Solve 8 problems total (1-2 per day) using your patterns.
- [ ] For each, write pseudocode first, trace it, then code.
- [ ] Compare your solution to the mental model: Did you respect the invariants?

### Deep Dive Phase
- [ ] Pick the 2 hardest patterns (likely Monotonic Stack and Floyd's).
- [ ] Watch 1 external video per pattern.
- [ ] Teach or explain it (even if to your rubber duck 🦆).

### Mixed Pattern Phase
- [ ] Attempt 1-2 problems that combine patterns (e.g., Kadane on rotated array + cycle detection).
- [ ] Reflect: How did you recognize multiple patterns in one problem?

### Reflection Phase
- [ ] Write 3-5 sentences on: "What was the hardest pattern and why did it finally click?"
- [ ] Review the 5 Cognitive Lenses for your hardest pattern. Which lens helped most?

---

## 🎯 Success Metrics

By end of Week 5, measure yourself:

| Metric | 🟢 Mastery | 🟡 Progress | 🔴 Needs Work |
|:---|:---|:---|:---|
| **Recognizing Patterns** | Identify pattern in <1 min per problem | 1-2 minutes | >2 min / guessing |
| **Tracing Algorithms** | Trace complex example by hand | Trace simple examples | Can't trace without help |
| **Explaining Invariants** | Explain 3+ invariants per pattern clearly | Explain 1-2 invariants | Vague or incorrect |
| **Time/Space Analysis** | Explain why O(N), not O(N²) + hidden costs | Recite Big-O correctly | Confuse worst/average case |
| **Problem Solving** | 70%+ solve rate on 8-10 problems | 50-70% solve rate | <50% solve rate |
| **Extending Patterns** | Solve novel variations (e.g., Kadane 2D) | Solve standard form | Stuck on variations |

---

## 🔄 Integration with Prior Weeks

### From Week 1 (Foundations)
- **Stacks & Queues:** Week 5 Day 2 uses Stack explicitly.
- **Linked Lists:** Week 5 Day 5 applies Fast/Slow to linked list cycle detection.
- **Pointers:** All 6 patterns use pointer/index mechanics.

### From Week 2 (Intermediate)
- **Two Pointers:** Week 3 taught sliding window; Week 5 specializes to monotonic, cycle, and interval variants.
- **Arrays:** All patterns operate on arrays or array-like structures.

### From Week 3 (Sorting & Searching)
- **Sorting:** Week 5 Day 3 uses sorting as the first step ("Sort & Sweep").
- **Binary Search:** Not directly, but Week 5 teaches efficient algorithms that compete with binary search.

### From Week 4 (Intro Patterns)
- **Greedy:** Week 5 Day 4B (Kadane) is a greedy DP algorithm.
- **Simple Two Pointers:** Week 5 Day 5 extends to cycle detection.

---

## 🚀 Next Week Preview (Week 6: Trees)

Week 5 patterns will directly appear:

- **Monotonic Stack** → Finding next greater element in BST (O(N) traversal).
- **Kadane** → Maximum path sum in a binary tree.
- **Fast/Slow** → Finding LCA (Lowest Common Ancestor) via pointer techniques.
- **Hash Maps** → Frequency counting and component finding in trees.

---

**End of Week 5 Guidelines**  
*Designed for deep understanding, not memorization. Master the invariants, and the problems will follow.*