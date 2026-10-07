# 📘 Week 8 Day 5: Strongly Connected Components (SCC) — Engineering Guide

> 🧭 **Navigation:** [← Previous Day](Week_08_Day_04_Connectivity_and_Bipartite_Graphs_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Week →](../week_09_graph_algorithms_i/README.md)
> 
> 💡 **Instructor Note:** *Strongly Connected Components (SCCs) are maximal subgraphs in directed graphs where every vertex is mutually reachable from every other vertex. Collapsing SCCs into meta-nodes transforms any cyclic directed graph into a Directed Acyclic Graph (DAG) called the **Condensation Graph**, unlocking topological sorting and dynamic programming on general directed graphs.*

---

## 🎯 LEARNING OBJECTIVES

By the end of this chapter, you will be able to:

- **Formalize** mutual reachability and contract cyclic components into a Condensation DAG.
- **Implement** Kosaraju's two-pass DFS algorithm using graph transposition in C# (.NET 8/9) and Python (3.11+).
- **Implement** Tarjan's single-pass DFS algorithm with discovery indices and low-link values in C# (.NET 8/9) and Python (3.11+).
- **Deconstruct** time and space complexity: strict `O(V + E)` time, `O(V + E)` vs `O(V)` auxiliary space.
- **Deliver** a structured 45-minute technical interview script defending Tarjan vs. Kosaraju trade-offs.

---

## 📖 CHAPTER 1: CONTEXT & MOTIVATION

### The Problem of Cycles in Directed Systems

In real-world networks, directed relationships often contain cycles that break linear DAG algorithms:
1. **Microservice Dependency Deadlocks:** Services calling each other transitively create circular dependencies where failures cascade infinitely.
2. **Web Page Rank & Crawlers:** Pages linking to each other in mutual loops form "spider traps" that must be identified as single cohesive clusters.
3. **Compiler Loop Optimization:** Optimizing compilers collapse strongly connected basic blocks into natural loop nests for vectorization.
4. **2-SAT Constraint Solvers:** In 2-Satisfiability, variables and their negations form an implication graph; the formula is satisfiable if and only if no variable `x` shares an SCC with `not x`.

### The Core Architectural Insight

Any directed graph can be cleanly partitioned into two layers:
1. **Internal Cycles:** Captured completely inside individual Strongly Connected Components.
2. **Inter-Component Flow:** A strictly acyclic Directed Acyclic Graph (the **Condensation DAG**) connecting the SCCs.

Once SCCs are collapsed into meta-nodes, we can run topological sort and dynamic programming on the resulting DAG.

---

## 🧠 CHAPTER 2: BUILDING THE MENTAL MODEL

### Condensation DAG & Cycle Contraction

```
Original Directed Graph:
       ( 0 ) <-----> ( 1 )
         |             |
         v             v
       ( 2 ) <-----> ( 3 ) ------> ( 4 )
                      ^             |
                      |             v
                      +---------- ( 5 )

Component Decomposition:
  SCC A: { 0, 1 }  (Mutual cycle between 0 and 1)
  SCC B: { 2, 3 }  (Mutual cycle between 2 and 3)
  SCC C: { 4, 5 }  (Mutual cycle between 4 and 5)

Condensation DAG (Acyclic Meta-Node Flow):
       [ SCC A ]
           |
           v
       [ SCC B ] <-----> [ SCC C ]? Wait: If B <-> C, they merge into ONE SCC!
       If B -> C and C -> B, SCC B and SCC C form a single SCC { 2, 3, 4, 5 }.
       The condensation graph between distinct components NEVER has cycles:
       [ Meta-Node A ] ---> [ Meta-Node B ]
```

### Tarjan's Low-Link Mechanism

Tarjan's algorithm tracks two numbers per vertex during a single DFS traversal:
- `index[u]`: Timestamp when node `u` was first discovered.
- `lowLink[u]`: Smallest `index` reachable from `u` via tree edges and at most one back-edge to an active ancestor on the call stack.

```
Tarjan Low-Link Invariant:
  - If a descendant v can reach an ancestor w on the call stack, lowLink[u] <= index[w].
  - If after exploring all neighbors, lowLink[u] == index[u]:
    ===> Vertex u is the ROOT of an SCC!
    ===> Pop all nodes from the stack down to u; they form a complete SCC.
```

---

## ⚙️ CHAPTER 3: MECHANICS & DUAL-LANGUAGE IMPLEMENTATIONS

### 1. Kosaraju's Algorithm (Two-Pass DFS)

1. **Pass 1:** Run DFS on original graph `adj`; push finished vertices onto a stack (post-order finish).
2. **Transpose:** Construct transposed graph `revAdj` by reversing every edge.
3. **Pass 2:** Pop vertices from stack in decreasing finish time. For each unvisited vertex, run DFS on `revAdj` to peel off one SCC.

#### C# (.NET 8/9) Implementation

```csharp
using System;
using System.Collections.Generic;

public sealed class KosarajuScc
{
    public static (int SccCount, int[] SccIds) ComputeScc(int n, List<int>[] adj)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(n);
        var revAdj = new List<int>[n];
        for (int i = 0; i < n; i++) revAdj[i] = new List<int>();

        for (int u = 0; u < n; u++)
        {
            foreach (int v in adj[u])
            {
                revAdj[v].Add(u); // Transpose edge
            }
        }

        var visited = new bool[n];
        var finishStack = new Stack<int>(n);

        // PASS 1: Record finishing order
        void Dfs1(int u)
        {
            visited[u] = true;
            foreach (int v in adj[u])
            {
                if (!visited[v]) Dfs1(v);
            }
            finishStack.Push(u);
        }

        for (int i = 0; i < n; i++)
        {
            if (!visited[i]) Dfs1(i);
        }

        // PASS 2: Traverse transpose graph in reverse finish order
        Array.Fill(visited, false);
        var sccIds = new int[n];
        Array.Fill(sccIds, -1);
        int sccCount = 0;

        void Dfs2(int u, int id)
        {
            visited[u] = true;
            sccIds[u] = id;
            foreach (int v in revAdj[u])
            {
                if (!visited[v]) Dfs2(v, id);
            }
        }

        while (finishStack.Count > 0)
        {
            int u = finishStack.Pop();
            if (!visited[u])
            {
                Dfs2(u, sccCount);
                sccCount++;
            }
        }

        return (sccCount, sccIds);
    }
}
```

#### Python (3.11+) Implementation

```python
from typing import List, Tuple

def kosaraju_scc(n: int, adj: List[List[int]]) -> Tuple[int, List[int]]:
    """Kosaraju's two-pass DFS using graph transposition."""
    rev_adj: List[List[int]] = [[] for _ in range(n)]
    for u in range(n):
        for v in adj[u]:
            rev_adj[v].append(u)

    visited = [False] * n
    finish_stack: List[int] = []

    # Pass 1: Original graph DFS
    def dfs1(u: int) -> None:
        visited[u] = True
        for v in adj[u]:
            if not visited[v]:
                dfs1(v)
        finish_stack.append(u)

    for i in range(n):
        if not visited[i]:
            dfs1(i)

    # Pass 2: Transposed graph DFS
    visited = [False] * n
    scc_ids = [-1] * n
    scc_count = 0

    def dfs2(u: int, scc_id: int) -> None:
        visited[u] = True
        scc_ids[u] = scc_id
        for v in rev_adj[u]:
            if not visited[v]:
                dfs2(v, scc_id)

    while finish_stack:
        u = finish_stack.pop()
        if not visited[u]:
            dfs2(u, scc_count)
            scc_count += 1

    return scc_count, scc_ids
```

---

### 2. Tarjan's Algorithm (Single-Pass Low-Link DFS)

A single DFS pass maintains `index`, `lowLink`, and an active call stack. When `lowLink[u] == index[u]`, an SCC is peeled off.

#### C# (.NET 8/9) Implementation

```csharp
public sealed class TarjanScc
{
    public static (int SccCount, int[] SccIds) ComputeScc(int n, List<int>[] adj)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(n);

        var index = new int[n];
        var lowLink = new int[n];
        var onStack = new bool[n];
        var sccIds = new int[n];
        Array.Fill(index, -1);
        Array.Fill(sccIds, -1);

        var stack = new Stack<int>();
        int timer = 0;
        int sccCount = 0;

        void Dfs(int u)
        {
            index[u] = timer;
            lowLink[u] = timer;
            timer++;

            stack.Push(u);
            onStack[u] = true;

            foreach (int v in adj[u])
            {
                if (index[v] == -1)
                {
                    // Tree edge
                    Dfs(v);
                    lowLink[u] = Math.Min(lowLink[u], lowLink[v]);
                }
                else if (onStack[v])
                {
                    // Back edge to an active ancestor in current SCC
                    lowLink[u] = Math.Min(lowLink[u], index[v]);
                }
            }

            // Root of an SCC identified
            if (lowLink[u] == index[u])
            {
                while (true)
                {
                    int w = stack.Pop();
                    onStack[w] = false;
                    sccIds[w] = sccCount;
                    if (w == u) break;
                }
                sccCount++;
            }
        }

        for (int i = 0; i < n; i++)
        {
            if (index[i] == -1) Dfs(i);
        }

        return (sccCount, sccIds);
    }
}
```

#### Python (3.11+) Implementation

```python
from typing import List, Tuple

def tarjan_scc(n: int, adj: List[List[int]]) -> Tuple[int, List[int]]:
    """Tarjan's single-pass DFS with low-link values and stack tracking."""
    index = [-1] * n
    low_link = [-1] * n
    on_stack = [False] * n
    scc_ids = [-1] * n

    stack: List[int] = []
    timer = 0
    scc_count = 0

    def dfs(u: int) -> None:
        nonlocal timer, scc_count
        index[u] = low_link[u] = timer
        timer += 1
        stack.append(u)
        on_stack[u] = True

        for v in adj[u]:
            if index[v] == -1:
                dfs(v)
                low_link[u] = min(low_link[u], low_link[v])
            elif on_stack[v]:
                low_link[u] = min(low_link[u], index[v])

        # If u is root of an SCC, pop all members
        if low_link[u] == index[u]:
            while True:
                w = stack.pop()
                on_stack[w] = False
                scc_ids[w] = scc_count
                if w == u:
                    break
            scc_count += 1

    for i in range(n):
        if index[i] == -1:
            dfs(i)

    return scc_count, scc_ids
```

---

## 📊 CHAPTER 4: COMPLEXITY DECONSTRUCTION

| Algorithm | Time Complexity | Auxiliary Space | Graph Transpose Required? | Number of DFS Passes |
| :--- | :--- | :--- | :--- | :--- |
| **Kosaraju's Algorithm** | `O(V + E)` | `O(V + E)` | ✅ Yes (`revAdj` storage) | 2 Passes |
| **Tarjan's Algorithm** | `O(V + E)` | `O(V)` | ❌ No (Original graph only) | 1 Pass |
| **Condensation DAG Build**| `O(V + E)` | `O(V + E)` | ❌ No | 1 Pass over edge set |

### Engineering Trade-Off: Kosaraju vs. Tarjan

- **Memory Efficiency:** Tarjan uses strictly `O(V)` auxiliary space because it traverses the graph in place. Kosaraju requires building the full transpose graph, taking an extra `O(V + E)` memory allocation.
- **Mental Simplicity:** Kosaraju is easier to explain and verify during an interview: it is just two standard DFS traversals (finish order + reversed graph).
- **Constant Factor:** Tarjan executes in roughly half the wall-clock time in cache-sensitive environments because it performs only one pass over the edges.

---

## 🎙️ CHAPTER 5: 45-MINUTE INTERVIEW VERBAL SCRIPT

### Phase 1: Clarification & Constraint Scoping (0–5 Mins)
- **Candidate:** "Before picking an SCC algorithm, let me verify the problem scope:
  1. Is the graph directed or undirected? (SCC only applies to directed graphs).
  2. Can the graph contain self-loops or duplicate edges?
  3. Are we asked to return the count of SCCs, the component mapping of each vertex, or the contracted Condensation DAG?
  4. What are the constraints on `V` and `E`?"
- **Interviewer:** "Directed graph, `V <= 10^5, E <= 2 * 10^5`, return the count and component ID for each vertex."

### Phase 2: High-Level Approach & Trade-Offs (5–12 Mins)
- **Candidate:** "To identify Strongly Connected Components in `O(V + E)` time, there are two primary algorithms:
  - **Kosaraju's Algorithm:** Two DFS passes using the transposed graph. Conceptually straightforward, but requires allocating `O(V + E)` memory for the reverse edges.
  - **Tarjan's Algorithm:** Single DFS pass using `lowLink` values and a stack. Requires only `O(V)` auxiliary memory and avoids allocating a reversed adjacency structure.
  I will implement **Tarjan's algorithm** to achieve optimal single-pass runtime and zero transpose allocation."

### Phase 3: Live Coding Walkthrough (12–32 Mins)
- **Candidate:** "I'll implement Tarjan's algorithm:
  1. Initialize `index`, `lowLink`, and `onStack` arrays.
  2. In the DFS function, assign `index[u] = lowLink[u] = timer++`, push `u` to `stack`, and set `onStack[u] = true`.
  3. For each neighbor `v`: if unvisited, recurse and update `lowLink[u] = min(lowLink[u], lowLink[v])`. If `onStack[v]` is true, this is a back-edge, so update `lowLink[u] = min(lowLink[u], index[v])`.
  4. Finally, if `lowLink[u] == index[u]`, pop vertices until `u` and assign the current `sccCount++`."

### Phase 4: Edge Cases & Verification (32–40 Mins)
- **Candidate:** "Let's test edge cases:
  1. DAG with no cycles: Every vertex is its own SCC; `lowLink[u] == index[u]` for every vertex; returns `V` distinct SCCs.
  2. Single giant cycle (`0 -> 1 -> ... -> V-1 -> 0`): `lowLink` bubbles down to `0` for all nodes; pops entire graph as 1 SCC.
  3. Disconnected components: Outer loop visits all unvisited roots cleanly."

---

## 🛠️ CHAPTER 6: COMMON PITFALLS & DECISION FRAMEWORK

### Common Pitfalls
1. **Omitting `onStack[v]` Check:** Checking `index[v] != -1` without checking `onStack[v]`. A node might be visited but belong to a **previously closed SCC**. Updating `lowLink` using a closed SCC corrupts component boundaries.
2. **Updating with `lowLink[v]` instead of `index[v]` on Back-Edges:** In Tarjan's algorithm, for a node already on the stack, updating `lowLink[u] = min(lowLink[u], index[v])` is standard. Using `lowLink[v]` is technically valid for SCCs, but breaks bridge/articulation point detection.
3. **Forgetting `onStack[w] = false` Upon Pop:** Failing to reset the `onStack` flag allows future DFS branches to falsely link back to completed components.

### SCC Algorithm Decision Tree

```
                          [ Directed Graph SCC ]
                                     |
               +---------------------+---------------------+
               |                                           |
       [ Memory Constrained ]                     [ Code Readability Focus ]
               |                                           |
       Tarjan's Algorithm                          Kosaraju's Algorithm
       Aux Space: O(V)                             Aux Space: O(V + E)
       1 DFS Pass                                  2 DFS Passes + Transpose
       No transpose allocation                     Simpler mental model
```

---

## 🏋️ PRACTICE LADDER

| Problem | LeetCode # | Difficulty | Pattern | Primary Invariant |
| :--- | :--- | :--- | :--- | :--- |
| **Critical Connections in a Network** | #1192 | 🔴 Hard | Tarjan Bridges | `lowLink[v] > index[u]` isolates bridge |
| **Maximum Employees to Be Invited** | #2127 | 🔴 Hard | Directed Cycle + Trees | Find 2-cycles vs larger cycles |
| **Course Schedule III** | #630 | 🔴 Hard | Greedy + DAG Scheduling | Prerequisite cycle contraction |
| **Strongly Connected Components (Kosaraju)** | GfK | 🟡 Medium | 2-Pass DFS | Reverse graph finishes source SCCs |

---

> 🧭 **Navigation:** [← Previous Day](Week_08_Day_04_Connectivity_and_Bipartite_Graphs_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Week →](../week_09_graph_algorithms_i/README.md)
