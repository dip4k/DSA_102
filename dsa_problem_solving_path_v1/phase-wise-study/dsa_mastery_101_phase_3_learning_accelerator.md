# DSA Mastery 101 — Phase 3 Learning Accelerator

## Purpose
This supplementary file upgrades **Phase 3: Trees, Graphs, and Dynamic Programming (Weeks 7-11)** into a stronger learning system before moving to Phase 4.

It is designed to improve:
- tree, graph, and DP state narration
- pattern classification under interview pressure
- transition from traversal intuition to formal invariants
- error diagnosis for weighted graphs and dynamic programming
- retention through structured re-solves and adaptive review
- ability to explain trade-offs, not just code solutions

This directly matches the syllabus goal for Weeks 7-11: **tree/graph/DP state narration, trade-off defense, and complexity articulation**.

---

# 1. What Phase 3 changes mentally

Phase 1 and 2 mostly teach you to spot patterns in arrays and strings.
Phase 3 is different.
Now you must think in terms of **structure + state**.

That means:
- in trees, the answer often depends on what must be returned upward versus what should update globally
- in graphs, the traversal depends on direction, weight, and whether you need reachability, order, shortest path, or connectivity
- in DP, success depends on defining state precisely before writing transitions

If earlier phases were about “which pattern?”, Phase 3 is about “what information must this node / state / subproblem represent?”

---

# 2. Phase 3 decision system

## 2.1 Fast classification tree
Use this before coding.

```text
Q1. Is the structure a tree?
- Yes -> ask whether the answer is local, path-based, subtree-based, or combine-left-right based.
- No  -> continue

Q2. Is the structure a graph or a grid behaving like a graph?
- Yes -> ask whether you need reachability, shortest path, ordering, connected components, bipartite check, or MST.
- No  -> continue

Q3. Are all edges unweighted and each step costs the same?
- Yes -> BFS is a strong candidate for shortest steps.
- No  -> continue

Q4. Are edge weights non-negative and you need shortest path from one source?
- Yes -> Dijkstra.
- No  -> continue

Q5. Can weights be negative or is path length bounded by number of edges/stops?
- Yes -> Bellman-Ford style relaxation is a candidate.
- No  -> continue

Q6. Do you need to connect all nodes with minimum total cost, not shortest path from one source?
- Yes -> MST (Prim or Kruskal).
- No  -> continue

Q7. Is the problem about best answer over subproblems with overlap?
- Yes -> DP.
- No  -> continue

Q8. For DP, what does a state mean?
- prefix position?
- grid cell?
- interval?
- ending-at-index?
- tree node with include/exclude?
- node in DAG?
- visited subset mask?
```

## 2.2 One-sentence recognition rules
- **Tree traversal:** hierarchical structure with parent-child recursion.
- **Tree DP:** subtree returns a value that combines children.
- **General graph traversal:** arbitrary connections, maybe disconnected.
- **BFS shortest path:** equal edge costs, minimum number of steps.
- **Topological sort:** dependency ordering in a directed acyclic graph.
- **Dijkstra:** cheapest path with non-negative weights.
- **Bellman-Ford:** repeated edge relaxations; useful with negative edges or stop limits.
- **Floyd-Warshall:** all-pairs shortest paths on smaller graphs.
- **MST:** minimum total connection cost for the whole network.
- **DSU:** dynamic connectivity and cycle detection.
- **DP:** optimal answer or count from overlapping subproblems.
- **Bitmask DP:** small `n`, subset membership is part of the state.

---

# 3. Tree-specific learning system

## 3.1 The four main tree question types
Every tree problem should first be classified into one of these:

| Type | Core question | Typical return value |
|---|---|---|
| Traversal | Visit nodes in some order | list / processing side-effect |
| Path-state DFS | Carry information from root to node | updated path state |
| Subtree aggregation | Compute something from children | height / sum / validity / pair |
| Global answer + local return | Update global best while returning local contribution | one-sided gain / height / include-exclude pair |

## 3.2 Upward-return drill
Before writing code for a tree problem, complete this sentence:

```text
My DFS(node) returns: ____________________
My global answer tracks: _________________
```

Examples:
- Diameter: DFS returns height; global answer tracks best left+right path.
- Max path sum: DFS returns best one-branch gain; global answer tracks best full path through a node.
- Balanced tree: DFS returns height or failure sentinel.
- House Robber III: DFS returns `(take, skip)`.

## 3.3 Tree invariant bank
Memorize these:
- Inorder BST traversal must be strictly increasing.
- Height of a node is `1 + max(left, right)`.
- A path returned to parent cannot fork in two directions.
- Any subtree answer must depend only on its node and descendants.
- Global tree answers often combine both children; upward returns usually cannot.

## 3.4 Tree confusion matrix

| Confused pair | How to distinguish |
|---|---|
| Height vs depth | Depth goes downward from root; height goes upward from node to leaf. |
| Diameter vs max path sum | Diameter counts path length, often edges; max path sum uses node values and may ignore negative branches. |
| BST validation local vs global | Parent-child checks are insufficient; ancestor bounds matter. |
| General-tree LCA vs BST LCA | BST LCA uses order; general tree LCA uses subtree discovery. |
| Traversal vs tree DP | Traversal just visits; tree DP asks what value each subtree should return. |

## 3.5 Tree dry-run template

| Node | Left result | Right result | Return upward | Global update |
|---|---|---|---|---|
| root | ... | ... | ... | ... |

Use this for:
- Diameter of Binary Tree
- Binary Tree Maximum Path Sum
- House Robber III
- Balanced Binary Tree
- Lowest Common Ancestor of Binary Tree

---

# 4. Graph-specific learning system

## 4.1 The graph question ladder
Ask these in order:
1. Directed or undirected?
2. Weighted or unweighted?
3. One component or many?
4. Need reachability, ordering, shortest path, cheapest full connection, or cycle detection?
5. Static graph or dynamic edge additions?

That single ladder prevents a huge number of wrong starts.

## 4.2 Graph decision table

| Need | Best first candidate | Why |
|---|---|---|
| Reachability / connected components | DFS or BFS | Need to explore everything reachable |
| Minimum edges / steps in unweighted graph | BFS | First time reached is shortest |
| Directed dependency order | Topological sort | Must process prerequisites first |
| Directed cycle detection | DFS coloring or Kahn's failure | Need back-edge / indegree logic |
| Bipartite check | BFS/DFS coloring | Alternating colors expose odd cycle conflict |
| Single-source shortest path, non-negative weights | Dijkstra | Greedy frontier is valid |
| Negative edges or bounded relaxations | Bellman-Ford | Repeated relaxations remain safe |
| All-pairs shortest path | Floyd-Warshall | Uses every node as intermediate |
| Cheapest total network | Prim / Kruskal MST | Optimize full connection, not route |
| Dynamic connectivity / cycle when adding edges | DSU | Fast component merges and checks |

## 4.3 Graph modeling drills
For every graph problem, write these before coding:

```text
Nodes represent: __________________
Edges represent: __________________
Directed or undirected: ___________
Weight means: _____________________
Goal is: reachability / order / shortest / MST / component count / cycle / coloring
```

This is especially important for:
- Number of Islands
- Rotting Oranges
- Course Schedule
- Reorder Routes
- Cheapest Flights Within K Stops
- Min Cost to Connect All Points

## 4.4 Weighted-graph confusion matrix

| Confused pair | Distinction |
|---|---|
| BFS vs Dijkstra | BFS assumes equal edge cost; Dijkstra handles varying non-negative weights. |
| Dijkstra vs Bellman-Ford | Dijkstra needs non-negative weights; Bellman-Ford tolerates negative edges and bounded relaxations. |
| Shortest path vs MST | Shortest path optimizes one route; MST optimizes total cost to connect all vertices. |
| Topological sort vs BFS | Both may use queue, but topo sort is about indegrees and DAG ordering, not distances. |
| DFS visited vs DFS recursion-state coloring | Simple visited detects reachability; 3-color state detects directed cycles. |

## 4.5 Multi-source BFS trigger list
Use multi-source BFS when:
- several starting points spread simultaneously
- each time step expands equally in all directions
- you want minimum time until all reachable cells/nodes are affected

Canonical examples:
- Rotting Oranges
- Walls and Gates
- 01 Matrix

## 4.6 Graph debugging checklist
- Did I build edges in the correct direction?
- Am I forgetting disconnected components?
- Is the graph weighted?
- Am I revisiting nodes when I should mark them earlier?
- Does queue level mean time or just traversal order?
- Did I confuse “all nodes reachable” with “all nodes connected cheaply”?

---

# 5. DP-specific learning system

## 5.1 The DP formula before code
For every DP problem, fill this out first:

```text
State:
Transition:
Base case:
Answer location:
Iteration order / recursion dependency:
Can I reduce space?
```

If you cannot fill these six fields, do not start coding yet.

## 5.2 DP state families

| Family | State shape | Example question |
|---|---|---|
| 1D prefix DP | `dp[i]` | best answer up to index `i` |
| End-at-index DP | `dp[i]` means answer ending exactly at `i` | LIS |
| Grid DP | `dp[r][c]` | paths or min cost to cell |
| Two-string DP | `dp[i][j]` over prefixes | LCS, Edit Distance |
| Interval DP | `dp[i][j]` over substring/interval | Longest Palindromic Subsequence |
| Tree DP | return value per node | House Robber III |
| DAG DP | best answer from node or to node in topo order | Longest Increasing Path style DAG logic |
| Bitmask DP | `dp[mask]` or `dp[node][mask]` | visit subsets, assignment, TSP-style states |

## 5.3 DP recognition prompts
- Is brute force recomputing the same subproblems?
- Does the answer depend on smaller prefixes, smaller coordinates, or smaller subsets?
- Can I describe the answer at one state using a small number of previous states?
- Is the problem asking for count, best value, min cost, or possibility?
- Does order matter, contiguity matter, or only subset membership matter?

## 5.4 DP confusion matrix

| Confused pair | Distinction |
|---|---|
| Substring vs subsequence DP | Substring is contiguous; subsequence can skip characters. |
| House Robber style vs knapsack style | House Robber uses adjacency exclusion on sequence; knapsack uses capacity / subset constraints. |
| Memoization vs tabulation | Memo starts from the target state recursively; tabulation builds prerequisite states iteratively. |
| Count DP vs optimization DP | Count adds possibilities; optimization uses min/max transitions. |
| Prefix DP vs ending-at-index DP | Prefix means answer for whole prefix; ending-at-index means answer must end exactly here. |
| Interval DP vs two-string DP | Interval DP shrinks inward on one sequence; two-string DP compares prefixes across two sequences. |

## 5.5 DP narration template
Speak this aloud during practice:

```text
State meaning:
To compute this state, I consider these previous states:
The recurrence is correct because every valid solution ends in one of these cases:
Base cases are:
The final answer is located at:
```

That narration habit directly trains interview execution.

---

# 6. Phase 3 active recall deck

## 6.1 Trees
- Why is local parent-child BST validation insufficient?
- What does DFS return in Diameter of Binary Tree?
- Why can a tree path returned upward not include both left and right branches?
- When is inorder traversal enough, and when do you need ancestor bounds?
- Why does postorder often fit tree DP better than preorder?

## 6.2 Graphs
- Why does BFS give shortest path only in unweighted graphs?
- Why does Dijkstra fail with negative weights?
- What does indegree represent in topological sorting?
- Why does an odd cycle imply not bipartite?
- What is the conceptual difference between MST and shortest path?

## 6.3 DP
- What exactly is the state in House Robber?
- Why does reverse iteration matter in 0/1 knapsack compression?
- How do you know whether a 2D DP is over prefixes or intervals?
- Why is LCS not the same as longest common substring?
- What makes a matrix problem a DAG DP problem?

## 6.4 Output-first drills
For each Phase 3 anchor problem, say in under 20 seconds:
- the structure type
- the state meaning
- the transition or traversal rule
- one alternate method
- one likely bug
- time and space complexity

---

# 7. Adaptive error log for Phase 3

## 7.1 Expanded mistake taxonomy

| Code | Mistake type | Meaning |
|---|---|---|
| T1 | Wrong tree return value | Returned wrong quantity upward |
| T2 | Wrong tree global update | Mixed local return with global answer |
| G1 | Wrong graph modeling | Built wrong nodes/edges/directions |
| G2 | Wrong traversal choice | Used DFS/BFS/Dijkstra/DSU incorrectly |
| G3 | Disconnected component miss | Started from one node and ignored others |
| G4 | Weighted-graph misunderstanding | Used BFS or wrong relaxation model |
| D1 | Wrong DP state | State meaning was not well-defined |
| D2 | Wrong transition | State relation incomplete or invalid |
| D3 | Wrong base case | Initialization incorrect |
| D4 | Wrong iteration order | Used states before they were ready |
| D5 | Compression bug | Space optimization changed semantics |
| C1 | Complexity mismatch | Correct idea but asymptotically too slow |
| N1 | Narration failure | Could code but could not justify correctness |

## 7.2 Reflection template

```text
Problem:
Category:
What I thought it was:
What it actually was:
Mistake tags:
Correct state / invariant:
Wrong assumption I made:
One tiny example that exposes my mistake:
Next review date:
```

## 7.3 Adaptive repair rules
- **T1 / T2:** fill the “DFS returns / global tracks” sentence before re-coding.
- **G1:** redraw the graph model on paper first.
- **G2 / G4:** classify the graph using the five graph-ladder questions before touching code.
- **G3:** explicitly loop through all nodes and ask whether a fresh traversal is needed.
- **D1 / D2:** write state and recurrence in plain English before any Python.
- **D3 / D4:** build a 3-row or 3-state example table manually.
- **D5:** verify compressed DP against the full DP on a tiny case.
- **N1:** explain the solution aloud without code.

---

# 8. Spaced revision system for Phase 3

## 8.1 Review cadence
Use this for every `Must` problem in Phase 3:
- same day: explain the state and algorithm in plain English
- 24 hours: solve from scratch without notes
- 3 days: solve one variant in the same family
- 7 days: explain the trade-off with one alternate approach
- 14 days: timed re-solve with full complexity narration
- 30 days: mixed-category blind selection from tree + graph + DP

## 8.2 What to re-test specifically

| Category | Re-test skill |
|---|---|
| Trees | what DFS returns vs what updates globally |
| BST | ancestor-bound reasoning |
| BFS/DFS | reachability vs layer-distance vs cycle logic |
| Topological sort | dependency direction and indegree meaning |
| Dijkstra | heap + relaxation correctness |
| MST/DSU | component merge reasoning |
| 1D DP | state recurrence and compressed space |
| 2D DP | base row/column and iteration order |
| Sequence DP | prefix vs interval vs ending-at-index state |
| Bitmask DP | mask meaning and transition legality |

---

# 9. Variant ladders for adaptability

## 9.1 Trees
- Level A: #104 Maximum Depth of Binary Tree
- Level B: #543 Diameter of Binary Tree
- Level C: #124 Binary Tree Maximum Path Sum

## 9.2 BST
- Level A: #700 Search in a BST
- Level B: #98 Validate Binary Search Tree
- Level C: #230 Kth Smallest Element in a BST

## 9.3 Graph traversal
- Level A: #1971 Find if Path Exists in Graph
- Level B: #200 Number of Islands
- Level C: #994 Rotting Oranges

## 9.4 Topological / cycle
- Level A: #207 Course Schedule
- Level B: #210 Course Schedule II
- Level C: #802 Find Eventual Safe States

## 9.5 Shortest paths
- Level A: #743 Network Delay Time
- Level B: #787 Cheapest Flights Within K Stops
- Level C: #1334 Find the City With the Smallest Number of Neighbors at a Threshold Distance

## 9.6 MST / DSU
- Level A: #684 Redundant Connection
- Level B: #1319 Number of Operations to Make Network Connected
- Level C: #1584 Min Cost to Connect All Points

## 9.7 DP fundamentals
- Level A: #70 Climbing Stairs
- Level B: #198 House Robber
- Level C: #416 Partition Equal Subset Sum

## 9.8 2D and sequence DP
- Level A: #62 Unique Paths
- Level B: #72 Edit Distance
- Level C: #300 Longest Increasing Subsequence

## 9.9 Advanced DP
- Level A: #337 House Robber III
- Level B: #329 Longest Increasing Path in a Matrix
- Level C: #847 Shortest Path Visiting All Nodes

---

# 10. Interview narration playbook

## 10.1 Trees
Use this structure:
1. “This is a tree DFS problem.”
2. “My recursive function returns ___ for each subtree.”
3. “Separately, I maintain ___ as the global answer.”
4. “Each node combines left and right like this: ___.”
5. “Time is O(n), space is O(h).”

## 10.2 Graphs
Use this structure:
1. “This graph is directed/undirected and weighted/unweighted.”
2. “The task is reachability / ordering / shortest path / MST / components.”
3. “That suggests BFS / DFS / topo sort / Dijkstra / DSU because ___.”
4. “I’ll build adjacency as ___.”
5. “Time is O(V+E) or O((V+E) log V), depending on the algorithm.”

## 10.3 DP
Use this structure:
1. “I’ll define `dp[...]` to mean ___.”
2. “The transition is ___ because the last decision must be one of these cases.”
3. “The base cases are ___.”
4. “The order of computation is ___ because those states are needed first.”
5. “I can / cannot compress space because ___.”

---

# 11. Phase 3 mixed diagnostic drill

## 11.1 Six-problem test set
Do one session with these mixed roles:
- 1 tree traversal or subtree-aggregation problem
- 1 BST or LCA problem
- 1 graph traversal / topo problem
- 1 weighted graph / DSU / MST problem
- 1 basic DP problem
- 1 advanced DP problem

## 11.2 What to record
For each problem, track:
- Did I classify the category correctly in under 30 seconds?
- Did I state the invariant or state before coding?
- Did I choose the right traversal/algorithm family immediately?
- Did I explain complexity clearly?
- Was the bug conceptual or implementation-level?

## 11.3 Scorecard

| Skill | 0 | 1 | 2 |
|---|---|---|---|
| Classification speed | Wrong | Slow / hinted | Immediate |
| State definition | Missing | Partial | Precise |
| Correct method choice | Wrong family | Mostly right | Clearly right |
| Code stability | Broke often | Minor bugs | Clean |
| Explanation quality | Weak | Adequate | Interview-ready |

A strong Phase 3 learner should score mostly 2s before moving on.

---

# 12. Content-generation improvements for later files

## 12.1 Add these fields to every Phase 3 style problem block
- **Structure type:** tree / graph / DP / hybrid
- **State meaning:** exact definition of return value or DP cell
- **Wrong instinct:** the common wrong algorithm family
- **Edge-direction note:** for graph problems
- **Transition legality:** for DP and shortest path problems
- **Interview narration line:** one sentence to say aloud

## 12.2 Best artifacts to include in future phases
- DFS-return vs global-answer mini tables for tree DP
- graph classification cards: weighted/unweighted, directed/undirected, static/dynamic
- DP state worksheets before code
- mistake tag boxes under each solution
- alternate-algorithm comparison notes
- “why this algorithm, not that one” callouts

---

# 13. 90-minute Phase 3 review session

## Session plan
- 10 min: classify 8 old problems by category and state type only
- 20 min: solve 1 tree problem
- 20 min: solve 1 graph problem
- 20 min: solve 1 DP problem
- 10 min: explain alternate approaches and complexities aloud
- 10 min: log mistakes and next review dates

## High-value rule
Never end a Phase 3 session with only code written.
Always end with at least one of these:
- a state summary
- a graph-model note
- a tree return-value note
- a corrected complexity explanation

That is what converts practice into transfer.

---

# 14. Pre-Phase-4 checklist
Mark each item Yes / No.

- I can explain what a DFS returns in at least three tree problems.
- I can distinguish BST validation by bounds from simple local checks.
- I know when BFS is enough and when Dijkstra is required.
- I can explain the difference between shortest path and MST.
- I can implement DSU without looking up the template.
- I can define DP state before coding.
- I can distinguish prefix DP, ending-at-index DP, interval DP, and bitmask DP.
- I can explain why reverse iteration is needed in 0/1 knapsack-style compression.
- I can narrate one tree DP and one graph shortest-path solution clearly.
- I know my weakest Phase 3 category before moving to paradigms.

If more than two items are “No,” do one more consolidation day before Phase 4.

---

# 15. Recommended next move
Before Phase 4, do one **Phase 3 consolidation day**:
- 45 minutes: tree return-value drill
- 45 minutes: graph classification and weighted/unweighted decision drill
- 60 minutes: DP state-definition drill
- 30 minutes: mixed timed re-solve of one medium problem from each category
- 15 minutes: update error log and confidence tracker

That single day will make greedy, backtracking, and advanced paradigms much easier to absorb.
