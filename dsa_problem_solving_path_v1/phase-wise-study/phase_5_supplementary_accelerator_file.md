# Phase 5 Supplementary Accelerator File
## Weeks 14-15 Fast-Track Execution Guide

## What this file is for
This accelerator file is the **high-ROI companion** to the Phase 5 main file.
It is designed for fast execution, interview prep, revision loops, and targeted retention for **Phase E: Integration & Extensions**.

Phase E covers **Weeks 14-15**, focusing on matrices, bitmasks, number theory, advanced strings, range-query structures, and network flow. The curriculum positions this phase as the move from broad competence to advanced integration, with expected interview coverage around **90-95%** by the end of the phase.

Use this file when:
- you want the shortest path to usable mastery
- you are revising before interviews
- you need a daily execution plan instead of a long conceptual narrative
- you want to separate must-know material from stretch topics

---

# 1. Phase 5 mission

The mission of Phase 5 is not to memorize six unrelated advanced topics.
The mission is to learn **translation**.

You should become able to look at a problem and quickly say:
- this grid is really a graph problem
- this subset state should be encoded with bits
- this arithmetic can be collapsed with gcd or modulo
- this string problem is about prefix/suffix reuse
- this repeated-query workload justifies preprocessing
- this assignment problem can be modeled as flow

If Phase 4 taught paradigm choice, Phase 5 teaches **representation choice**.
That is the whole accelerator mindset.

---

# 2. Must-know outcomes

By the end of this phase, you should be able to:
- solve matrix traversal and transformation problems safely
- use bitwise operators and bitmasks deliberately
- apply gcd, lcm, primes, and modular arithmetic in coding questions
- explain KMP, Z, trie, and suffix-family intuition at a useful level
- choose between prefix sums, Fenwick trees, and segment trees
- explain residual graphs, augmenting paths, and bipartite matching via flow

If you can do those six things under moderate pressure, the phase worked.

---

# 3. Priority map

## Tier 1 — Absolute must
Study these until you can explain and implement them cleanly:
- matrix traversal and boundary handling
- rotate / transpose / spiral patterns
- bit operations and subset masks
- gcd, lcm, modular arithmetic, sieve basics
- KMP intuition and Z intuition
- trie basics and when trie beats hash set
- prefix sums vs Fenwick vs segment tree trade-offs
- residual graph and augmenting path intuition
- bipartite matching as flow model

## Tier 2 — Strong should-know
Know these conceptually and solve at least one problem each:
- submask enumeration
- binary exponentiation under modulo
- lazy propagation intuition
- suffix arrays/trees awareness
- Aho-Corasick awareness
- Dinic awareness

## Tier 3 — Stretch / optional
Do these only after Tier 1 and Tier 2 feel stable:
- full lazy propagation implementation from memory
- advanced suffix structures
- min-cost max-flow details
- CRT / Euler totient depth
- radix-tree compression details

---

# 4. Time-boxed study plan

The curriculum gives Phase E about **25-30 hours** over **10 study days**.
For accelerator mode, use this compressed split:

| Block | Focus | Time |
|---|---|---|
| Block 1 | Matrices + grid graph recognition | 3-4 hrs |
| Block 2 | Bits + bitmask state compression | 3-4 hrs |
| Block 3 | GCD / primes / modular arithmetic | 3 hrs |
| Block 4 | KMP + Z + trie family | 4-5 hrs |
| Block 5 | Prefix sums / Fenwick / segment tree | 4-5 hrs |
| Block 6 | Flow foundations + matching applications | 4-5 hrs |
| Block 7 | Mixed revision + mock explanation | 3 hrs |

If time is very tight, do Blocks 1, 2, 4, 5, and 6 first.

---

# 5. Week 14 accelerator

## Day A — Matrix essentials
Focus on:
- transpose
- rotate image
- spiral traversal
- grid BFS/DFS recognition

Execution goals:
- solve one transformation problem
- solve one traversal problem
- solve one grid-as-graph problem

Verbal checkpoint:
- “This is not really a matrix trick; it is traversal / coordinate transform / graph search.”

### Matrix cheat rules
- square matrix transform -> check transpose + reverse pattern
- spiral -> track `top`, `bottom`, `left`, `right`
- island/path/grid spread -> think BFS/DFS immediately
- DP on grid -> define cell dependency clearly before coding

---

## Day B — Bitwise and bitmask essentials
Focus on:
- set / clear / toggle / check bit
- power of two test
- lowest set bit
- subset enumeration
- bitmask as compact state

Execution goals:
- write the six core bit operations from memory
- solve one simple bit trick problem
- solve one subset-mask problem

Verbal checkpoint:
- “Bit i represents ____. The mask is a compressed set.”

### Bit cheat rules
- if state is yes/no across a small set -> consider mask
- if you need all subsets of small `n` -> consider mask loop
- if performance matters and flags are frequent -> bits may help
- if `n` is too large -> stop forcing bitmasking

---

## Day C — Number theory essentials
Focus on:
- gcd / lcm
- sieve intuition
- modular add / multiply / subtract
- modular exponentiation awareness

Execution goals:
- implement gcd from memory
- explain when to use sieve vs trial division
- solve one gcd-based question
- solve one modulo arithmetic question

Verbal checkpoint:
- “The arithmetic structure is stronger than brute force here.”

### Number-theory cheat rules
- divisibility or periodic alignment -> try gcd/lcm
- many prime queries up to a bound -> sieve
- huge powers modulo m -> binary exponentiation
- modular division is special; do not treat it as normal division

---

## Day D — Advanced strings bridge
The content guidance recommends pairing Week 14 advanced strings with Week 15 Day 1 as a mini-track.
Do that.

Focus on:
- KMP intuition
- trie usage
- suffix-family awareness
- Aho-Corasick awareness

Execution goals:
- understand failure-function meaning
- explain when trie beats hash set
- identify at least one case where many patterns suggest Aho-Corasick

Verbal checkpoint:
- “This string problem is about reusing prefix/suffix structure instead of restarting comparisons.”

---

# 6. Week 15 accelerator

## Day E — Z-algorithm and string matching
Focus on:
- Z-array intuition
- Z vs KMP differences
- pattern matching via prefix reuse
- periodic string use cases

Execution goals:
- explain Z-window idea in plain language
- compare KMP and Z without memorized jargon
- solve one string matching problem using one of them

Verbal checkpoint:
- “KMP uses failure fallback, Z uses prefix-match windows.”

### String selection cheat table

| Problem signal | First tool to consider |
|---|---|
| Single pattern in one text | KMP or Z |
| Prefix lookup across dictionary | Trie |
| Many patterns in one text | Aho-Corasick |
| Repeated substring / suffix order | Suffix structure awareness |
| Tiny custom check | Plain scan / two pointers / hash |

---

## Day F — Range queries
Focus on:
- prefix sums
- Fenwick trees
- segment trees
- lazy propagation intuition

Execution goals:
- explain when preprocessing is worth it
- compare prefix sums, Fenwick, and segment tree aloud
- solve one point-update range-sum problem

Verbal checkpoint:
- “The workload is repeated queries plus updates, so a range structure is justified.”

### Range-query cheat table

| Situation | Best default |
|---|---|
| Static array, many sum queries | Prefix sums |
| Point updates + prefix/range sums | Fenwick tree |
| Range min/max/sum with flexibility | Segment tree |
| Range updates + rich queries | Segment tree + lazy propagation |

### Fenwick memory hook
Think: **prefix structure with low-bit jumps**.

### Segment-tree memory hook
Think: **binary decomposition of intervals**.

### Lazy propagation memory hook
Think: **record the update promise now, push it later only if needed**.

---

## Day G — Network flow basics
Focus on:
- source / sink / capacity / conservation
- augmenting path
- residual graph
- max-flow intuition

Execution goals:
- explain residual graph in your own words
- explain why reverse edges exist
- solve one conceptual max-flow example

Verbal checkpoint:
- “This is constrained movement through a network.”

### Flow cheat rules
- assignment with capacities -> maybe flow
- one-to-one matching -> likely bipartite matching / flow
- routing under limits -> flow
- minimum cut / disconnect question -> think max-flow min-cut family

---

## Day H — Flow applications
Focus on:
- bipartite matching
- task assignment
- disjoint paths intuition
- cut-based thinking

Execution goals:
- build one matching model from plain English
- label left side, right side, source, sink, and capacities correctly
- explain what the max-flow value means in that model

Verbal checkpoint:
- “Nodes represent entities, edges represent allowed assignments, capacities encode limits.”

---

# 7. Cross-topic classifier

Use this when a problem feels weird:

```text
Is it a grid? -> traversal / graph / DP on cells
Is it a subset state? -> bitmask
Is arithmetic the bottleneck? -> gcd / modulo / prime reasoning
Is string reuse the key? -> KMP / Z / trie / suffix family
Are many queries happening? -> preprocessing / range structure
Is there assignment or capacity? -> flow
```

This classifier alone will save you a lot of interview time.

---

# 8. High-frequency interview traps

## Matrices
- boundary mistakes in spiral traversal
- row/column confusion
- revisiting cells incorrectly in BFS/DFS
- mutating board state too early or too late

## Bitmasks
- unclear meaning of each bit
- precedence bugs with shifts and bitwise ops
- using masks when `n` is too large
- forgetting restore/undo in recursive use

## Number theory
- brute forcing divisibility when gcd solves it
- overflow before modulo
- modular subtraction bugs
- treating modular division as normal division

## Strings
- defaulting to naive scan when prefix reuse is obvious
- not knowing when trie is overkill
- mixing KMP and Z intuitions into confusion
- overengineering suffix structures

## Range queries
- building segment trees for static data
- not understanding why Fenwick is simpler
- lazy propagation bugs from poor interval conventions

## Flow
- wrong edge directions
- forgetting residual reverse edges
- confusing original graph with residual graph
- not converting matching problems into the standard source-left-right-sink model

---

# 9. The minimum viable problem set

If you are short on time, do this set.

## Matrix set
- Rotate Image
- Spiral Matrix
- Number of Islands

## Bit / bitmask set
- Counting Bits
- Subsets
- one small bitmask-state problem

## Number-theory set
- one gcd-based problem
- one sieve-based problem
- one modular exponentiation or mod-counting problem

## Advanced-string set
- Implement Trie
- one KMP or Z matching problem
- one Word Search II style trie-integration awareness problem

## Range-query set
- Range Sum Query with updates
- one Fenwick implementation problem
- one segment-tree conceptual problem

## Flow set
- one bipartite matching reduction
- one simple max-flow conceptual walkthrough

This is the fastest defensible Phase 5 coverage set.

---

# 10. Rapid-recall templates

## Matrix template prompts
- What are the boundaries?
- Is this transformation or traversal?
- Can I mutate in place?
- Is this actually a graph-on-grid problem?

## Bitmask template prompts
- What does each bit represent?
- Is `n` small enough?
- Am I enumerating masks or tracking state?
- What operation do I need: check, set, clear, toggle?

## Number-theory template prompts
- Do gcd/lcm simplify the relation?
- Is modulo required for size or arithmetic structure?
- Are many prime queries present?
- Is binary exponentiation needed?

## String template prompts
- Am I restarting comparisons wastefully?
- Is this prefix-oriented?
- Is this multi-pattern matching?
- Do I need a dictionary structure or just one pattern check?

## Range-query template prompts
- Static or dynamic?
- Query only, update only, or both?
- Sum only or richer operator?
- Is preprocessing justified by workload size?

## Flow template prompts
- What are source and sink?
- What does each edge mean?
- What capacity does each edge encode?
- Is this really matching, assignment, routing, or cut?

---

# 11. 60-second explanation drills

Practice answering these aloud.

## Drill 1
Why is a grid often really a graph?

## Drill 2
What makes a bitmask useful instead of a boolean array?

## Drill 3
When do you use gcd instead of brute force?

## Drill 4
How is KMP different from Z-algorithm conceptually?

## Drill 5
When is a Fenwick tree enough, and when do you need a segment tree?

## Drill 6
What is a residual graph, and why do reverse edges exist?

If you cannot answer these quickly, revisit the relevant block.

---

# 12. Compression notes by topic

## Matrices in one line
Most matrix questions are really boundary management, coordinate transformation, DP on cells, or graph search on a grid.

## Bitmasks in one line
A bitmask is just a compact set where bit operations become fast membership and state updates.

## Number theory in one line
Use arithmetic structure to avoid brute force, especially through gcd, primes, and modulo laws.

## Advanced strings in one line
Reuse known prefix/suffix structure instead of re-comparing characters from scratch.

## Range queries in one line
If queries and updates repeat enough, preprocess the array into a structure that trades setup cost for faster repeated operations.

## Flow in one line
Convert constrained assignment or routing into a capacity network and solve it in the graph domain.

---

# 13. One-page revision map

| Topic | Core insight | Default pattern |
|---|---|---|
| Matrices | Surface 2D often hides traversal/graph structure | boundaries, BFS/DFS, DP |
| Bits | compressed yes/no state | mask operations, subset loops |
| Number theory | algebra beats brute force | gcd, sieve, mod arithmetic |
| Strings | reuse structure across comparisons | KMP, Z, trie |
| Range queries | repeated work justifies preprocessing | prefix sums, Fenwick, segment tree |
| Flow | capacity network models assignment/routing | max-flow, matching, min-cut |

---

# 14. Final readiness checklist

Mark each Yes / No.

- I can solve a spiral traversal problem without boundary bugs.
- I can explain when a grid is really a graph.
- I can use bit operations without looking them up.
- I can explain subset masks clearly.
- I can use gcd/lcm/modulo intentionally.
- I know when sieve preprocessing is justified.
- I can explain KMP and Z in plain language.
- I know when trie is the right tool.
- I can choose among prefix sums, Fenwick, and segment tree.
- I understand lazy propagation at an intuition level.
- I can explain augmenting paths and residual graphs.
- I can model bipartite matching as flow.

If you have at least 10 “Yes” answers, you are in strong Phase 5 shape.

---

# 15. Final execution advice

Do not try to become encyclopedic in Phase 5.
Become **selective and sharp**.

The winning strategy is:
- identify the hidden representation quickly
- justify the specialized tool only when needed
- keep comparison tables in your head
- practice verbal explanation as much as implementation

That is what turns Phase 5 from a pile of advanced topics into a practical interview advantage.
