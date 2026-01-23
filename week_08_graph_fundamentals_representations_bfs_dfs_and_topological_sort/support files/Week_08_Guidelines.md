# 🌐 Week 08 Guidelines – Graph Fundamentals: Representations, BFS, DFS & Topological Sort

Version: v1.0  
Filename: `Week_08_Guidelines.md`  
Week: 08 – Phase C: Trees, Graphs, Dynamic Programming  
Theme: **From trees to general graphs – modelling, traversing, and ordering complex systems**

---

## 🎯 1. Week Overview – Why Graphs, Why Now?

Week 08 is your **gateway into graph thinking**. Up to Week 07, you worked mostly with **linear structures** (arrays, lists) and **tree hierarchies**. Graphs generalize both: any set of objects with relationships can be expressed as a graph.

This week you will:
- Learn how to **model real problems as graphs** (social networks, maps, dependency systems).  
- Master **BFS** and **DFS**, the two fundamental graph traversal paradigms.  
- Understand **topological sort** for ordering tasks with dependencies.  
- Build strong intuition for **connectivity**, **bipartite graphs**, and **SCCs**.

Everything from **shortest paths (Week 09)** to **flow and matching (later phases)** depends on what you learn here.

---

## 🎯 2. Learning Objectives (8–10 Clear Targets)

By the end of Week 08, you should be able to:

1. **Model** real-world scenarios as graphs by identifying **nodes** and **edges**, including implicit graphs (grids, puzzles, state spaces).  
2. **Compare** adjacency list, adjacency matrix, and edge list representations and choose the right one based on **sparsity** and **operations**.  
3. **Implement BFS** on adjacency lists and implicit grids, and explain why BFS finds **shortest paths in unweighted graphs**.  
4. **Use BFS** to compute not just distances, but also **paths**, **levels**, and simple **connected components**.  
5. **Implement DFS** both recursively and with an explicit stack, and trace its behaviour on small graphs.  
6. **Classify edges** in DFS trees conceptually as tree, back, forward, or cross edges and relate them to **cycle detection**.  
7. **Compute a topological ordering** of a DAG using both DFS post-order and Kahn’s in-degree algorithm; explain where each shines.  
8. **Find connected components** in undirected graphs and **check bipartiteness** using BFS/DFS with 2-coloring.  
9. Build intuition for **Union–Find** as another way to reason about connectivity and when to prefer it over BFS/DFS.  
10. Conceptually understand **Strongly Connected Components (SCCs)** and how they collapse a directed graph into a **DAG of components**.

Keep these 10 objectives visible. By the end of the week, you should be able to **teach each one** to someone else.

---

## 📅 3. Day-by-Day Concept Overview

### 📅 Day 1 – Graph Models & Representations

Focus: **How do we even store a graph?**

Key ideas:
- **Graph types:** directed vs undirected, weighted vs unweighted.  
- **Representations:**
  - **Adjacency list** – neighbors per node.  
  - **Adjacency matrix** – dense V×V connectivity table.  
  - **Edge list** – list of (u, v) (and maybe weight).  
- **Sparsity vs density:** when `E ≈ V` vs `E ≈ V²`.  
- **Implicit graphs:** grids, puzzles, and state spaces where edges are defined by rules instead of explicit storage.

Your goals:
- Be able to **look at a problem** and say: “This is a graph. The nodes are X, the edges are Y.”  
- Choose the **right representation** given constraints (memory, edge lookups, traversal patterns).

---

### 📅 Day 2 – Breadth-First Search (BFS)

Focus: **Layered exploration and shortest paths in unweighted graphs.**

Key ideas:
- BFS as a **queue-based frontier** that expands level by level.  
- **Distance layers**: all nodes at distance 1, then 2, etc.  
- **Shortest paths** in unweighted graphs: distance in edges.  
- Applications:
  - Shortest route in unweighted networks.  
  - Level-order traversal in trees.  
  - Conceptual basis for multi-source BFS and bipartite checks.

Your goals:
- Implement BFS and maintain arrays for **distances** and **parents**.  
- Explain why **first visit = shortest path** in unweighted graphs.

---

### 📅 Day 3 – DFS & Topological Sort

Focus: **Depth-first exploration, cycles, and dependency ordering.**

Key ideas:
- DFS mechanics: **recursive** vs **explicit stack**.
- DFS tree & edge types (conceptual): tree, back, forward, cross.  
- Cycle detection in directed graphs via **back edges** or recursion stack.  
- Topological sort:
  - DFS post-order reverse method.  
  - Kahn’s algorithm using in-degrees and a queue.  
- Use-cases:
  - Task scheduling and dependency resolution.  
  - Basis for DAG DP, SCC algorithms, and more.

Your goals:
- Trace DFS and label nodes with **discovery/finish times**.  
- Implement both **DFS-based** and **Kahn’s** topological sort and know when to use which.

---

### 📅 Day 4 – Connectivity & Bipartite Graphs

Focus: **Graph structure as islands and 2-colorable relationships.**

Key ideas:
- Connected components via BFS/DFS on undirected graphs.  
- Bipartite testing via **2-coloring**; odd cycles as obstruction.  
- Differences in **cycle detection** between undirected and directed graphs.  
- High-level idea of **articulation points** and robustness.  
- **Union–Find/Disjoint Set** for offline connectivity queries.  
- Real-world: network reliability, clustering, simple constraint satisfaction.

Your goals:
- Implement a routine that returns the **number of components** and component labels.  
- Run a **bipartite check** and recognize **odd cycles** conceptually.  
- Understand when **DSU** is preferable to repeated BFS/DFS.

---

### 📅 Day 5 – Strongly Connected Components (Optional Advanced)

Focus: **Mutual reachability and component DAGs.**

Key ideas:
- Intuition for SCCs: `u` and `v` are in the same SCC if `u` can reach `v` and `v` can reach `u`.  
- **Kosaraju**: two-pass DFS, transpose graph.  
- **Tarjan**: low-link values and a stack (conceptual).  
- Component DAG: each SCC collapses into a node; result is always a **DAG**.

Your goals:
- Be able to describe **why SCC condensation gives a DAG**.  
- Recognize patterns where SCCs help (2-SAT, dependency cycles, modularization).

---

## 🧠 4. How to Study This Week (Mental Models → Mechanics → Practice)

### Step 1 – Start with Modelling

Before code or algorithms, ask:
- What are the **entities**? (cities, users, tasks, cells).  
- What are the **relationships**? (roads, follows, prerequisites, adjacency).  
- Are relationships **directional**? Do they have **weights**?  
- Is the graph **explicit** (given edges) or **implicit** (derived by rules)?

Write this down in a notebook. Treat it like designing the **schema** of a database or API.

### Step 2 – Build Visual Intuition

For each new concept:
- Draw a **small graph** (5–7 nodes) and label edges.  
- For BFS: mark **layers** with different colors.  
- For DFS: annotate nodes with **discovery and finish times**.  
- For components: encircle each **island**.  
- For bipartite: color nodes in **two colors**, watch where it fails.

Your brain will remember the **pictures**, not the pseudo-code.

### Step 3 – Mechanize on Paper Before IDE

For BFS/DFS/topo:
- Write the **core algorithm** in high-level pseudo-code.  
- Trace it on 2–3 small examples **by hand**.  
- Maintain small tables for `queue`, `stack`, `visited`, `dist`, `parent`.

Only after this should you move to coding; the goal is to **mirror** your mental execution in code.

### Step 4 – Practice With Increasing Constraints

- Start with **unweighted graphs** and clean DAGs.  
- Add constraints: multiple sources, disconnected graphs, cycles.  
- For bipartite: try both bipartite and non-bipartite examples.  
- For SCCs: construct small graphs with obvious SCC “clusters” and test if your method groups them correctly.

---

## ⚠️ 5. Common Pitfalls (and How to Avoid Them)

1. **Forgetting to mark visited on enqueue in BFS**  
   - Symptom: duplicates in queue, wrong distances.  
   - Fix: set `visited[v] = true` the moment you enqueue `v`.

2. **Using BFS for weighted shortest paths**  
   - Symptom: path has few edges but too much total weight.  
   - Fix: BFS is only for **unweighted or equal-weight** edges; use Dijkstra/Bellman–Ford for weights.

3. **Confusing undirected and directed adjacency**  
   - Symptom: cycle detection and topo sort results are nonsense.  
   - Fix: be explicit: `A ↔ B` vs `A → B`. Never “forget” the arrow.

4. **Running DFS/BFS once and assuming whole graph is explored**  
   - Symptom: “invisible” components, incorrect component counts.  
   - Fix: always wrap traversals in an outer loop over all nodes; restart when you see an unvisited node.

5. **Using adjacency matrix for very large sparse graphs**  
   - Symptom: memory exhaustion or very slow performance.  
   - Fix: matrix only for small or dense graphs; default to adjacency lists.

6. **Expecting topological sort to work on cyclic graphs**  
   - Symptom: algorithm “fails” or returns incomplete order.  
   - Fix: topo sort assumes a **DAG**; detect cycles first and treat them as input errors or SCCs.

7. **Thinking SCCs are needed for undirected graphs**  
   - Symptom: unnecessary complexity; confusion.  
   - Fix: in undirected graphs, SCCs ≡ connected components; SCC algorithms are for **directed** graphs.

---

## ⏱️ 6. Suggested Time Allocation Strategy

Assuming ~10–12 focused hours this week:

- **Day 1 – Graph Models & Representations (2–2.5 hours)**  
  - 45 min: lecture/reading on graph types + representations.  
  - 45 min: drawing 4–5 real-world graphs and choosing representations.  
  - 30 min: small implementation or pseudo-code for building adjacency lists.

- **Day 2 – BFS (2–2.5 hours)**  
  - 45 min: concept + mechanics (distances, parents).  
  - 45 min: trace BFS on 2–3 examples including a grid.  
  - 45 min: code implementation + one practice problem.

- **Day 3 – DFS & Topological Sort (3 hours)**  
  - 1 hr: DFS recursion/stack, edge types, cycle detection.  
  - 1 hr: DFS-based topo + Kahn’s algorithm with examples.  
  - 1 hr: implement both, test on small DAG/cyclic graphs.

- **Day 4 – Connectivity & Bipartite (2–2.5 hours)**  
  - 45 min: components via BFS/DFS; island counting.  
  - 45 min: bipartite 2-coloring; odd cycle examples.  
  - 45 min: practice problems (components, bipartite checks).

- **Day 5 – SCC & Integration (optional, 1.5–2 hours)**  
  - 45 min: SCC intuition, Kosaraju, component DAG.  
  - 45 min: integration: discuss how Week 08 feeds Week 09–11.  
  - Optional: code a small SCC routine or step through pseudo-code.

Adjust based on your schedule, but try to **touch graphs daily**.

---

## ✅ 7. Weekly Checklist

By the end of the week, you should be able to check:

- [ ] I can describe at least **three real-world systems** as graphs, with nodes and edges identified.  
- [ ] I can explain the trade-offs between **adjacency list**, **matrix**, and **edge list**.  
- [ ] I can implement **BFS** and use it to compute distances and reconstruct paths.  
- [ ] I can implement **DFS** and use it to explore components and detect directed cycles conceptually.  
- [ ] I can compute a **topological ordering** using both DFS-post-order and **Kahn’s algorithm**.  
- [ ] I can find **connected components** of an undirected graph and label each node with its component ID.  
- [ ] I can determine whether a graph is **bipartite** using BFS/DFS with 2-coloring.  
- [ ] I understand, at least conceptually, what **SCCs** are and why collapsing them yields a **DAG**.  
- [ ] I know at least **two scenarios** where BFS is preferred, and **two scenarios** where DFS/topo is preferred.  
- [ ] I feel comfortable enough with these concepts that I could **explain them in an interview** without code.

If 80% of these are true, you have built a solid Week 08 foundation. The remaining items can be targeted in revision sessions or in problem-solving practice in subsequent weeks.
