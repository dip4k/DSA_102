# DSA Mastery 101 — Phase 4: Greedy, Backtracking, Branch & Bound, and Amortized Analysis

## Phase identity
Phase 4 corresponds to **Phase D: Algorithm Paradigms**, covering **Weeks 12-13** of the curriculum.

This phase is where you stop thinking only in terms of data structures and start thinking in terms of **algorithmic strategy selection**.
You are no longer asking only:
- Which structure should I use?
- Which known pattern does this resemble?

You are now asking:
- Can a locally optimal choice be proven globally optimal?
- Is this really an exhaustive-search problem in disguise?
- Can I prune the search tree safely?
- Is worst-case cost the wrong lens, and do I need amortized analysis instead?

---

## Phase scope
This phase covers:
- greedy choice property and proof methods
- interval scheduling and activity selection
- Huffman coding and optimal prefix trees
- fractional knapsack and scheduling-style greedy problems
- backtracking state-space search
- pruning and constraint propagation intuition
- branch and bound for optimization
- amortized analysis using aggregate, accounting, and potential methods

This is the bridge between **problem solving** and **algorithm design reasoning**.
In earlier phases, you often selected a known technique.
In this phase, you must justify *why* that technique is valid.

---

# 1. What changes in Phase 4

Phase 3 taught you to define state clearly for trees, graphs, and DP.
Phase 4 teaches you to evaluate **decision discipline**.

That means:
- greedy asks whether the first safe-looking move can be made without regret
- backtracking asks whether you must explore a search tree of possibilities
- branch and bound asks whether optimization search can be pruned using bounds
- amortized analysis asks whether expensive operations are rare enough that average sequence cost stays small

A useful mental shift:
- **DP:** “solve all relevant subproblems systematically”
- **Greedy:** “commit early, but only with proof”
- **Backtracking:** “explore choices recursively, undoing bad branches”
- **Branch & Bound:** “optimize while pruning branches that cannot beat the current best”
- **Amortized:** “an individual operation may be expensive, but a long sequence may still be cheap on average”

---

# 2. Phase 4 operating system

## 2.1 First classification questions
Before coding, ask these in order:

```text
Q1. Is the goal optimization, feasibility, enumeration, or counting?
Q2. Can I make one local choice now and prove it never harms the optimum?
Q3. If greedy seems tempting, can I produce either a proof or a counterexample?
Q4. If not greedy, is this a search-space problem with constraints?
Q5. If search-space based, do I need all solutions, one solution, or the best solution?
Q6. If best solution, can I bound unfinished branches and prune them?
Q7. Is the question really about cost over a sequence of operations rather than one isolated operation?
```

## 2.2 Quick classifier

| Problem signal | Best first lens |
|---|---|
| “Choose maximum number of non-overlapping intervals” | Greedy |
| “Can take partial item” | Greedy |
| “Need all valid arrangements / combinations / placements” | Backtracking |
| “Need best arrangement among exponential possibilities” | Branch & Bound or DP |
| “Operation sometimes costly due to occasional rebuild/restructure” | Amortized analysis |
| “Local heuristic looks tempting, but weighted choices exist” | Suspect DP, not plain greedy |

## 2.3 Core warning
Never trust a greedy idea because it **feels** elegant.
Trust it only after one of these happens:
- you prove exchange safety
- you prove stays-ahead dominance
- you reduce the remaining problem to the same form after the greedy choice
- you fail to find a counterexample under correct modeling

---

# 3. Week 12 — Greedy algorithms and proofs

Week 12 focuses on the design of greedy algorithms and the proof techniques that justify them.
A greedy algorithm is not merely “fast and intuitive.”
It is an algorithm that commits to a local choice without backtracking, and correctness depends on proving that commitment is safe.

## 3.1 Greedy mental model
A greedy algorithm typically follows this skeleton:
1. Sort or prioritize candidates by a rule.
2. Repeatedly take the currently best-looking valid choice.
3. Never revisit earlier decisions.
4. Prove that an optimal solution can be transformed to match the greedy choice.

The power of greedy comes from **irreversibility with proof**.
If you cannot justify irreversibility, you do not yet have a greedy solution.

## 3.2 When greedy usually works
Greedy often works when:
- local decisions preserve future flexibility
- the problem has a safe ordering criterion
- the structure allows exchange of choices without making the solution worse
- the objective decomposes in a way that respects local dominance

Typical examples:
- earliest finishing interval for activity selection
- smallest frequencies first for Huffman coding
- highest value density first for fractional knapsack
- cheapest safe edge first for Kruskal-style MST construction

## 3.3 When greedy often fails
Greedy often fails when:
- weights create long-term consequences hidden by short-term gain
- taking one option blocks a better future combination
- the objective depends on interactions among later choices
- the problem is actually subset optimization in disguise

Classic failure zone:
- 0/1 knapsack is **not** solved by value/weight ratio greedily
- weighted interval scheduling is **not** solved by the same greedy rule as unweighted activity selection
- many “maximize total score” problems are DP even if a greedy heuristic looks plausible

---

# 4. Greedy proof toolkit

## 4.1 Exchange argument
This is the most important proof style in interviews.
The structure is:
1. Assume an optimal solution exists.
2. Compare its first differing choice with the greedy choice.
3. Swap in the greedy choice.
4. Show the new solution is no worse.
5. Repeat until the optimal solution aligns with greedy.

Use exchange arguments when you can say:
- “If an optimal solution chose X instead of greedy choice G, I can replace X by G and preserve feasibility and value.”

Activity selection and Huffman coding both strongly rely on this style.

## 4.2 Stays-ahead proof
This proof says the greedy solution remains at least as good as any competitor at every step.
The idea is not swapping one object at a time, but showing the greedy prefix dominates.

Use this when you can argue something like:
- after selecting `k` activities, greedy finishes no later than any other solution using `k` activities
- therefore greedy never reduces future opportunity compared with any competitor

## 4.3 Optimal substructure after greedy commitment
Sometimes the key claim is:
- after making the greedy choice, the remaining subproblem is of the same form
- solving it optimally completes an overall optimal solution

This is especially useful when the greedy choice naturally partitions the instance.

## 4.4 Counterexample method
A wrong greedy idea is often revealed by a tiny counterexample.
This is not failure — it is part of design.

Whenever you test a greedy idea, deliberately search for cases with:
- one huge weight versus many medium weights
- overlapping intervals with conflicting priorities
- equal ratios but different absolute sizes
- early local gains that destroy later flexibility

If a three- or four-item example breaks the rule, the greedy approach is invalid.

---

# 5. Anchor greedy problems

## 5.1 Activity selection
### Problem idea
Given intervals with start and finish times, pick the maximum number of non-overlapping activities.

### Greedy rule
Sort by finish time ascending, then keep the next compatible interval.

### Why it works
Finishing earlier preserves maximum room for later intervals.
Any optimal solution can be exchanged to start with the greedy-finishing interval without reducing the count.

### Pattern notes
- Objective: maximize number of compatible intervals.
- Rule: earliest finish time.
- Proof style: exchange argument or stays-ahead.
- Complexity: O(n log n) for sorting and O(n) scan.

### Common trap
Sorting by shortest duration or earliest start time is not generally correct.

---

## 5.2 Interval variants
You must separate these carefully:

| Problem | Typical method | Why |
|---|---|---|
| Max number of non-overlapping intervals | Greedy by finish time | Preserves future room |
| Minimum intervals to remove to eliminate overlap | Greedy by finish time | Equivalent complement view |
| Minimum meeting rooms needed | Sweep line / heap | Need concurrency count, not subset selection |
| Weighted interval scheduling | DP | Weights break simple greedy safety |

Interview strength comes from **distinguishing similar-looking interval problems** rather than memorizing one template.

---

## 5.3 Huffman coding
### Problem idea
Build an optimal prefix code from character frequencies.

### Greedy rule
Repeatedly combine the two lowest-frequency nodes.

### Why it works intuitively
Low-frequency symbols should be pushed deeper into the code tree because they contribute less to total weighted path length.
Combining the two smallest first creates a safe local merge.

### Implementation shape
- Use a min-heap of frequencies.
- Pop two smallest.
- Merge into a new node with summed frequency.
- Push back.
- Repeat until one tree remains.

### Pattern notes
- Data structure: min-heap.
- Complexity: O(n log n).
- Proof style: exchange argument around deepest sibling leaves.

### Common trap
Students often memorize the heap process but cannot explain *why* the two smallest can safely become siblings.
That proof idea matters more than the code.

---

## 5.4 Fractional knapsack
### Problem idea
Items have value and weight, and fractions are allowed.

### Greedy rule
Sort by value/weight ratio descending and take as much as possible in that order.

### Why it works
Because divisibility lets you continuously fill remaining capacity with the highest density value available.
There is no indivisibility penalty.

### Contrast with 0/1 knapsack
This difference is crucial:
- fractional knapsack -> greedy works
- 0/1 knapsack -> DP or branch-based reasoning is needed

### Common trap
Treating both as the same problem family is a major conceptual mistake.
Allowing fractions changes the mathematical structure completely.

---

## 5.5 Job sequencing with deadlines
### Problem idea
Each job has a deadline and profit; choose a schedule maximizing total profit under slot limits.

### Greedy rule
Sort jobs by profit descending, and place each in the latest available slot before its deadline.

### Why the “latest slot” detail matters
Placing a job as late as possible preserves earlier slots for other jobs.
This is the same theme as interval problems: preserve future flexibility.

### Common trap
Placing profitable jobs in the earliest available slot wastes scarce early capacity.

---

## 5.6 Greedy in graph context
You already saw MST earlier, but here you reinterpret it as a **proof paradigm**.
Kruskal and Prim are not just graph algorithms; they are greedy algorithms justified by cut and safe-edge reasoning.

That matters because Phase 4 is about recognizing proof structure across domains.
One week earlier you may have learned MST as a graph topic.
Now you should be able to say, “This is a greedy algorithm justified by a structural safety theorem.”

---

# 6. Greedy failure patterns

## 6.1 Signals that you should distrust greedy
- The problem asks for a weighted optimum over overlapping choices.
- A locally best score can block a better combination later.
- Taking one object changes the value of remaining objects in complicated ways.
- You cannot write a clear exchange argument.
- A tiny counterexample appears immediately.

## 6.2 Greedy vs DP diagnostic table

| Signal | Greedy likely | DP likely |
|---|---|---|
| Local order rule seems stable across instance | Yes | Maybe |
| Need proof by safe local replacement | Yes | Not enough |
| Weighted choice interactions matter heavily | No | Yes |
| Problem asks best value across many subsets | Rarely | Often |
| Partial divisibility allowed | Often | Less necessary |
| Choice now changes future state meaningfully | Risky | Strong candidate |

## 6.3 Interview habit
Whenever presenting a greedy idea, say this aloud:

```text
The rule is ___.
I would want to prove it using exchange / stays-ahead.
If that proof fails, I would suspect DP or exhaustive search instead.
```

This makes your reasoning look deliberate instead of lucky.

---

# 7. Week 13 — Backtracking fundamentals

Backtracking is DFS over a decision tree.
The key difference from plain recursion is that you are exploring **candidate choices**, checking constraints, and undoing decisions when a branch fails.

## 7.1 Core backtracking template
Every backtracking problem can be framed with four ingredients:
- state: current partial solution
- choices: what can be tried next
- constraints: what makes a partial solution invalid
- goal: when a full valid solution has been formed

Canonical pseudocode shape:

```text
backtrack(state):
    if state is complete:
        record answer
        return

    for choice in available choices:
        if choice is invalid:
            continue
        apply choice
        backtrack(new state)
        undo choice
```

The undo step is what gives backtracking its identity.
It allows a single recursive exploration to reuse the same mutable state safely.

## 7.2 Solution-tree mental model
Think of backtracking as a tree:
- root = empty partial assignment
- edge = one decision
- internal node = incomplete candidate
- leaf = full solution or dead end

That mental model helps with both correctness and complexity.
Backtracking complexity is often exponential because the state-space tree branches repeatedly.

## 7.3 What pruning means
Pruning means cutting off a branch **before** it becomes complete because you already know it cannot yield a valid or better answer.

There are two broad kinds:
- feasibility pruning: branch violates constraints, so no valid solution exists below it
- dominance / bound pruning: branch cannot beat the best known answer

The first is classic backtracking.
The second moves toward branch and bound.

---

# 8. Backtracking pattern families

## 8.1 Permutations
### Signal
Need all orderings of elements.

### State
Current partial permutation plus used/un-used markers.

### Typical bug
Forgetting to unmark an element on backtrack.

## 8.2 Combinations / subsets
### Signal
Need all subsets or all `k`-sized selections.

### State
Current index plus partial chosen set.

### Key idea
The decision is often “take / skip” or “choose next starting from index i.”

## 8.3 Constraint placement problems
Examples:
- N-Queens
- Sudoku
- graph coloring variants

### Key idea
Each new placement creates constraints on future placements.
Efficient checking structures make or break performance.

## 8.4 Grid path search with backtracking
Examples:
- Word Search
- maze exploration with revisitation restrictions

### Key idea
Mark visited, explore neighbors, then unmark on return if the path is branch-local.

## 8.5 Partition-style search
Examples:
- partition into subsets
- assign items to buckets
- target-sum style branching

### Key idea
Symmetry breaking and ordering heuristics often matter enormously.

---

# 9. Anchor backtracking problems

## 9.1 N-Queens
### State
Row index plus occupied columns and diagonals.

### Constraint logic
A queen attacks:
- same column
- main diagonal
- anti-diagonal

### Optimization insight
Do not scan the whole board each time.
Use sets or bitmasks to test attacks in O(1).

### Why it matters
This problem teaches state representation, pruning, and search-tree branching discipline.

---

## 9.2 Sudoku solver
### State
Current board with next empty cell choice.

### Constraints
Digits must be unique in row, column, and box.

### Key skill
Constraint bookkeeping is as important as recursion.
A weak state representation makes the solver dramatically slower.

### Advanced note
Heuristics like choosing the most constrained empty cell first can shrink the tree significantly.

---

## 9.3 Permutations and combinations
These are the best introductory backtracking problems because they expose the search template clearly without heavy constraint logic.
You should master them until the recursion tree feels mechanical rather than magical.

---

## 9.4 Word Search
### State
Cell position, index in target word, and visited path state.

### Core idea
A cell can be reused across different branches, but not twice in the same branch.
That is why the mark/unmark discipline matters.

### Common trap
Using a global visited set across all branches incorrectly blocks valid paths.

---

# 10. Backtracking design checklist

Before coding, fill this in:

```text
State:
Choices:
Constraint test:
Goal condition:
How do I undo?
What can I prune early?
```

If any line is fuzzy, your backtracking implementation will probably also be fuzzy.

Useful implementation questions:
- Can I sort inputs to prune earlier?
- Can I skip symmetric duplicates?
- Should I place the most constrained variable first?
- Can I maintain O(1) validity checks instead of rescanning state?

---

# 11. Complexity in backtracking

Backtracking is often exponential in the worst case.
That does **not** mean all backtracking solutions are equally bad.

Performance depends on:
- branching factor
- depth
- pruning power
- ordering heuristics
- state-check cost

A strong interview answer should mention more than “worst case exponential.”
Say what actually drives the explosion and what reduces it.

Example:
- N-Queens naive checking wastes time scanning rows/diagonals repeatedly.
- O(1) constraint sets reduce per-node cost.
- good pruning reduces the effective search tree drastically.

---

# 12. Branch and bound

Branch and bound is backtracking for **optimization**, with an extra weapon: a bound telling you whether a branch is even worth exploring.

## 12.1 Core idea
At any partial state:
- branch = decide the next possible continuations
- bound = estimate best possible outcome reachable from this partial state
- prune = abandon branch if even its best possible completion cannot beat the current best solution

This changes brute-force optimization into structured search.

## 12.2 Backtracking vs branch and bound

| Feature | Backtracking | Branch & Bound |
|---|---|---|
| Main purpose | Feasibility / enumeration | Optimization |
| Prune because invalid? | Yes | Yes |
| Prune because not competitive? | Sometimes limited | Core idea |
| Needs current best answer | Not always | Yes |
| Needs upper/lower bound | Not necessarily | Essential |

## 12.3 Bound quality matters
A weak bound prunes little.
A strong bound prunes aggressively but may cost more to compute.

This creates a trade-off:
- cheap bound -> many nodes explored
- expensive bound -> fewer nodes explored, but more work per node

That trade-off discussion is interview gold.

---

# 13. Anchor branch-and-bound problems

## 13.1 0/1 knapsack with bounds
### Branch
At each item: include it or exclude it.

### Bound
Use fractional knapsack on the remaining capacity as an optimistic upper bound.

### Prune rule
If current value + optimistic bound <= best known answer, stop exploring this branch.

### Why it is elegant
The greedy solution to fractional knapsack becomes the bound for a harder non-fractional optimization problem.
This is one of the cleanest cross-paradigm connections in the whole curriculum.

---

## 13.2 Traveling Salesman Problem intuition
### Branch
Extend a partial tour by choosing the next city.

### Bound
Use a lower-bound estimate from cheap edges, reduced costs, or MST-style reasoning on unvisited cities.

### Prune rule
If partial tour cost + lower bound >= best complete tour found, stop.

### Key insight
The whole game is not the branching.
It is the quality of the bound.

---

## 13.3 Search order matters
Branch and bound often benefits from exploring promising branches first.
Finding a good feasible solution early tightens the pruning threshold.

That is why best-first exploration with a priority queue can outperform plain DFS in some optimization settings.

---

# 14. Amortized analysis

Amortized analysis studies the cost of a **sequence** of operations.
It does not average over random inputs.
It proves a guaranteed average cost per operation over any sequence.

## 14.1 Why worst-case alone is misleading
A dynamic array append is occasionally O(n) when resizing happens.
But if resizing doubles capacity, most appends are O(1), and the expensive copies are rare.
The amortized cost per append is still O(1).

This is why amortized analysis is not hand-wavy averaging.
It is a formal guarantee over sequences.

## 14.2 Three major methods

| Method | Idea | Typical use |
|---|---|---|
| Aggregate analysis | Sum total cost of n operations, divide by n | Dynamic arrays, simple sequences |
| Accounting method | Overcharge cheap operations and save credits for expensive ones | Resizing, stack-like cleanup patterns |
| Potential method | Store prepaid work in a potential function on state | Advanced structures, rigorous proofs |

---

# 15. Aggregate analysis

This is the simplest method.
You compute the total cost of `n` operations directly.

### Dynamic array example
Suppose capacity doubles whenever full.
Across `n` appends:
- most appends cost O(1)
- copies happen at sizes 1, 2, 4, 8, ...
- total copy work is less than 2n

So total work is O(n), giving O(1) amortized per append.

### Why this matters
It teaches the core intuition that rare expensive rebuilds do not destroy average sequence efficiency.

---

# 16. Accounting method

In the accounting method, you assign each cheap operation a charge slightly above its immediate cost.
The extra credits are saved to pay for future expensive operations.

### Dynamic array intuition
Charge each append, say, 3 units:
- 1 unit pays for inserting the new element now
- extra units are stored as credit
- when resize happens, stored credits pay for copying old elements

If you can show the credit balance never goes negative, the amortized bound is valid.

### Why students like it
It feels concrete and operational.
You can almost simulate a bank account for future work.

---

# 17. Potential method

The potential method defines a function \(\Phi(	ext{state})\) representing stored work or structural tension in the data structure.
Amortized cost is:

\[
	ext{amortized cost} = 	ext{actual cost} + \Delta \Phi
\]

The sum of amortized costs over a sequence telescopes.
That is why this method becomes powerful for sophisticated data structures.

### Intuition
- when the structure becomes “messier,” potential rises
- when an expensive cleanup happens, potential drops and helps pay for it

### Why it matters later
This method prepares you for deeper topics like splay trees, Fibonacci heaps, and more advanced self-adjusting structures.

---

# 18. Amortized-analysis case studies

## 18.1 Dynamic arrays
This is the canonical case and should be fully mastered.
It also connects directly back to the curriculum’s earlier dynamic-array week.

## 18.2 Monotonic stacks / deques
Students often misread these as O(n^2) because there is a while-loop inside a for-loop.
The amortized insight is that each element is pushed once and popped once, so total stack operations stay O(n).

## 18.3 Union-Find intuition
Even if the formal inverse-Ackermann bound is beyond the current phase’s emphasis, this is a good place to appreciate that aggressive restructuring can make long sequences very efficient.

---

# 19. Phase 4 comparison map

| Paradigm | Core question | Typical output | Proof / justification style |
|---|---|---|---|
| Greedy | Can I commit now without regret? | One optimal solution | Exchange, stays-ahead, structural safety |
| Backtracking | Which valid solutions exist? | One or all feasible solutions | Search-tree completeness + pruning validity |
| Branch & Bound | Which feasible solution is best? | Best solution under objective | Bound correctness + pruning soundness |
| Amortized analysis | What is the cost over many operations? | Sequence-level complexity bound | Aggregate, accounting, potential |

This table should become automatic in your head.
A large part of Phase 4 mastery is choosing the right lens before implementation begins.

---

# 20. Interview narration playbook

## 20.1 Greedy
Say:
1. “The candidate greedy rule is ___.”
2. “I would justify it using exchange / stays-ahead reasoning.”
3. “If I cannot prove that, I would look for a DP or search formulation.”
4. “The implementation cost is dominated by sorting / heap operations.”

## 20.2 Backtracking
Say:
1. “This is DFS over a state-space tree.”
2. “My state is ___, and my choices are ___.”
3. “I prune when ___.”
4. “I undo the choice after recursion to restore the state.”

## 20.3 Branch and bound
Say:
1. “This is an optimization search problem.”
2. “I branch on ___.”
3. “I compute a bound using ___.”
4. “If the branch cannot beat the current best, I prune it.”

## 20.4 Amortized analysis
Say:
1. “A single operation may be expensive.”
2. “But over a sequence, the expensive events are rare / prepaid.”
3. “Using aggregate / accounting / potential reasoning, the amortized cost is ___.”

---

# 21. Common mistakes in Phase 4

## 21.1 Greedy mistakes
- proposing a heuristic without proof
- proving by examples instead of a general argument
- confusing unweighted and weighted interval problems
- using ratio-based greediness where indivisibility breaks it

## 21.2 Backtracking mistakes
- unclear state representation
- forgetting to undo state
- pruning valid branches incorrectly
- using expensive validity checks inside every recursive step

## 21.3 Branch-and-bound mistakes
- no true bound, just a guess
- bound not optimistic/pessimistic in the right direction
- failing to update current best aggressively
- exploring in an order that delays good incumbent solutions

## 21.4 Amortized-analysis mistakes
- confusing amortized with expected value
- claiming “usually cheap” without proof
- analyzing one operation instead of the sequence
- missing push-once/pop-once style accounting arguments

---

# 22. Practice ladder

## 22.1 Greedy
- Easy: Assign Cookies
- Easy/Medium: Non-overlapping Intervals
- Medium: Minimum Number of Arrows to Burst Balloons
- Medium: Task Scheduler variants
- Medium: Job Sequencing with Deadlines
- Medium/Hard: Huffman-style merge reasoning or MST proof reinterpretation

## 22.2 Backtracking
- Easy: Subsets
- Medium: Combination Sum
- Medium: Permutations
- Medium: Word Search
- Hard: N-Queens
- Hard: Sudoku Solver

## 22.3 Branch and bound
- Conceptual: 0/1 Knapsack with upper bound
- Conceptual/Hard: TSP with bounding
- Mixed: optimization versions of subset search

## 22.4 Amortized analysis
- Dynamic array append
- Queue via two stacks
- Monotonic stack / deque patterns
- Union-Find intuition

---

# 23. How to study this phase effectively

## Day-by-day rhythm
For each study day:
1. Learn the paradigm and one canonical proof idea.
2. Solve one base problem.
3. Solve one close variant.
4. Explain aloud why the paradigm fits better than the nearest alternative.
5. Record one failure mode in your notes.

## Best comparison habits
Always compare:
- greedy vs DP
- backtracking vs branch and bound
- worst-case vs amortized

Those comparisons are where most interview growth happens.

---

# 24. Pre-Phase-5 readiness checklist
Mark each item Yes / No.

- I can explain what makes a greedy choice “safe.”
- I know at least two greedy proof styles.
- I can distinguish activity selection from weighted interval scheduling.
- I can explain why fractional knapsack is greedy but 0/1 knapsack is not.
- I can write a clean backtracking template from memory.
- I can define state, choices, constraints, goal, and undo step clearly.
- I can explain how pruning changes practical performance.
- I understand how branch and bound differs from ordinary backtracking.
- I can give an amortized proof for dynamic-array append.
- I can explain why a nested while-loop can still lead to O(n) total work in monotonic-stack settings.

If several answers are still “No,” add one consolidation day before moving on.

---

# 25. Closing orientation
Phase 4 is not about learning a long list of unrelated tricks.
It is about developing the instinct to ask:
- “Do I need proof of a local choice?”
- “Do I need to search?”
- “Can I prune?”
- “Am I analyzing the right cost model?”

Once these questions become automatic, the next phase becomes much easier because integration topics depend heavily on choosing the right paradigm under pressure.
