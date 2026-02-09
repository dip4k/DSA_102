# 📘 WEEK 12 DAY 5: GREEDY IN SYSTEMS — ENGINEERING GUIDE

**Metadata:**
- **Week:** 12 | **Day:** 05 (Optional Systems Day)
- **Category:** Algorithm Paradigms (Greedy in Practice)
- **Difficulty:** 🟡 Intermediate → 🔴 Advanced (systems & theory blend)
- **Real-World Impact:** Greedy thinking underlies how networks are built (MST), how packets are routed, how caches behave, and how we approximate hard optimization problems in production.
- **Prerequisites:**
  - Week 12 Days 01–04 (greedy fundamentals, intervals, Huffman, knapsack, scheduling)
  - Week 9 (Minimum Spanning Trees, Dijkstra) — for MST and network context
  - Basic understanding of caches (from OS / systems background)

---

## 🎯 LEARNING OBJECTIVES

*By the end of this chapter, you will be able to:*

- 🌐 **Explain** how classic network algorithms like Kruskal and Prim are greedy algorithms, and why greediness is safe for MSTs.
- 🛰️ **Understand** greedy routing: local decisions at each hop using limited information.
- 🧠 **Connect** cache replacement strategies (especially LRU) to greedy heuristics over temporal locality.
- 📉 **Recognize** when optimal solutions are NP-hard and greedy is used instead as an approximation.
- 🧪 **Articulate** the strengths and limits of greedy methods in real systems (correctness vs performance vs simplicity).

---

## 📖 CHAPTER 1: CONTEXT & MOTIVATION

### Greedy Algorithms Leave the Classroom

So far this week, greedy algorithms were used on **clean, well-specified problems**:

- Activity selection, interval scheduling.
- Huffman coding for optimal prefix codes.
- Fractional knapsack and job sequencing.

In each case, you had:

- A precise mathematical formulation.
- A provable correctness argument (exchange, stays-ahead, or structural induction).

Real-world systems are messier:

- Networks are dynamic; link latencies and capacities change.
- Caches face unpredictable access patterns.
- Many optimization problems are NP-hard — exact optimal algorithms may be infeasible.

Yet engineers **still rely heavily on greedy thinking**:

- Build a cheap but well-connected network (MST).
- Route packets hop-by-hop with limited knowledge.
- Evict the “least useful” cache entry without looking into the future.
- Design approximation algorithms that are good enough for NP-hard problems.

Today focuses on how these greedy ideas show up in **systems design**, and how the theory you learned earlier helps you understand their behavior and limits.

### Three System-Level Faces of Greedy

Day 5 highlights three major system contexts:

1. **Greedy in Networks**
   - **Minimum Spanning Trees (MST):** Kruskal and Prim greedily pick cheapest edges while preserving connectivity.
   - **Routing Protocols:** Greedy forwarding using local metrics like hop-count, distance, or geographic position.

2. **Cache Replacement**
   - **LRU (Least Recently Used):** Greedy heuristic that evicts the item least recently accessed.
   - Captures temporal locality well but is not always globally optimal.

3. **Approximation Algorithms**
   - For NP-hard problems, greedy gives bounded approximations.
   - **Set cover** as a canonical example: simple greedy achieves O(log n) approximation.

All three share a theme:

> Use a **simple, local rule** to make progress, either because it is provably optimal (MST) or because exact optimality is computationally unreasonable (caching, set cover).

---

## 🧠 CHAPTER 2: GREEDY IN NETWORKS

### A. Minimum Spanning Trees as Greedy Structures

A **Minimum Spanning Tree (MST)** of a connected, weighted, undirected graph is a subset of edges that:

- Connects all vertices (is a spanning tree).
- Has minimum possible total edge weight among all spanning trees.

Two classic algorithms are:

- **Kruskal’s Algorithm (Edge-Based Greedy)**
- **Prim’s Algorithm (Vertex-Based Greedy)**

Both are **greedy** in different ways.

#### 1. Kruskal’s Algorithm — Greedy by Global Edge Weight

**Greedy idea:**

> Always pick the **smallest-weight edge** that does **not** create a cycle.

**Conceptual Steps:**

1. Sort all edges by weight in non-decreasing order.
2. Start with an empty forest (no edges, all nodes separate).
3. For each edge in sorted order:
   - If the edge connects two different components (no cycle), **add it** to the forest.
   - Otherwise, skip it.
4. Stop when you have `V - 1` edges; that forest is an MST.

This uses a **global** ordering of edges.

**Visual Sketch:**

Imagine a graph:

```text
   (1)---2---(2)
    | \      /
   3|  4\  /6
    |    (3)
    5 \  /
       (4)
```

Edges (weight in parentheses):

- (1-2): 2
- (1-3): 4
- (1-4): 5
- (2-3): 6
- (2-4): 3

Sorted by weight: 2, 3, 4, 5, 6.

Kruskal picks:

1. Edge (1-2) with weight 2.
2. Edge (2-4) with weight 3.
3. Edge (1-3) with weight 4.

Now we have 3 edges, 4 nodes → spanning tree. Heavier edge (2-3) weight 6 is never chosen.

**Why Greedy is Safe (Cut Property):**

- Consider any **cut** of the vertices (split nodes into two non-empty groups).
- The **lightest edge crossing that cut** is always in **some** MST.
- Kruskal’s algorithm, at each step, effectively chooses such a lightest crossing edge for some cut of the still-disconnected components.

This cut property is the greedy-choice justification for MSTs.

#### 2. Prim’s Algorithm — Greedy by Growing a Tree

**Greedy idea:**

> Start from an arbitrary node and repeatedly attach the **cheapest edge** that expands the current tree to a new vertex.

**Conceptual Steps:**

1. Pick any start vertex; mark it as part of the MST.
2. Among all edges that connect a vertex in the tree to a vertex outside, choose the smallest.
3. Add that edge and the new vertex to the tree.
4. Repeat until all vertices are included.

This is similar to **Dijkstra’s algorithm** in flavor, but applied to undirected graphs and edge weights for constructing an MST instead of shortest paths.

**Visual Growth:**

- Initially, the MST is just the start vertex.
- Each step, one new vertex and one new edge are added.
- The tree “grows” outward greedily along the cheapest frontier edges.

#### 3. Why Greedy Works for MSTs

Two key properties make MST problems greedy-friendly:

1. **Optimal Substructure:**
   - Any segment of an MST is also an MST for its vertices.
   - Removing any edge from an MST splits it into two smaller MSTs for their respective components.

2. **Cut Property (Greedy Choice Property):**
   - For any cut, the minimum-weight edge crossing it belongs to **some** MST.
   - Kruskal and Prim both repeatedly apply this property.

Thus, MST problems are a powerful demonstration: **local cheapest choices can be globally optimal** when the structure (cut property) holds.

### B. Greedy Routing in Networks

Now move from building the network (MST) to **routing packets** through it.

Ideal routing would minimize:

- Latency (time delay), or
- Hop count (number of edges), or
- Congestion (load on links).

Exact global optimization often requires full network knowledge, which is unrealistic at routers.

**Greedy routing principle:**

> Each router makes a **local forwarding decision** based on simple metrics, often:
>
> - Next hop with smallest distance to destination.
> - Next hop with lowest latency estimate.
> - Next hop with highest available bandwidth.

#### 1. Greedy Geographic Routing (Conceptual)

In some wireless or sensor networks, each node knows:

- Its own coordinates (x, y)
- The coordinates of its neighbors
- Destination coordinate

Greedy rule:

- Forward packet to the neighbor **closest (in Euclidean distance)** to the destination.

**Visualization:**

```text
S •----• A
        \
         • B
          \
           • D (destination)
```

At `S`, among neighbors {A}, pick A (closest to D). At A, pick B; at B, pick D.

This is a greedy walk downhill on the distance function.

**Caveats:**

- Can get stuck in local minima: a node that has no neighbor closer to D even though a path exists.
- Real protocols use fallback strategies (e.g., perimeter routing) to recover.

#### 2. Greedy Metrics in Classical Routing

In IP networks, protocols like OSPF (Open Shortest Path First) and IS-IS use:

- Dijkstra’s algorithm centrally at each router (with global topology info from link-state advertisements).

Even here, **forwarding** is local and greedy:

- Forward along the next hop on the shortest path tree (from Dijkstra’s result).

While Dijkstra itself is not a purely local greedy hop-by-hop algorithm (it requires global knowledge), the **forwarding behavior** at each router is a simple greedy choice: 

> Send packet to neighbor that leads to the shortest path to destination.

---

## 🧠 CHAPTER 3: GREEDY IN CACHE REPLACEMENT

### A. What is a Cache?

A **cache** is a smaller, faster storage that keeps a subset of data items from a larger, slower storage (e.g., RAM vs disk, CPU cache vs RAM, CDN vs origin server).

When a requested item is **in cache**, we get a fast **hit**; otherwise, a slow **miss** occurs and we must fetch from lower memory.

**Cache replacement problem:**

- Cache can only hold `K` items.
- Items are requested over time: `a1, a2, a3, ...`
- When cache is full and a miss occurs on item `x`, we must **evict** some existing item to bring `x` in.
- Which item should we evict to minimize future misses?

The optimal strategy (Belady’s algorithm) would be:

> Always evict the item whose **next use** is **farthest in the future**.

But this requires **knowing the future sequence** — impossible in real systems.

### B. LRU (Least Recently Used) as a Greedy Temporal Heuristic

**LRU idea:**

> Evict the item that was **least recently accessed** — the one you haven’t used for the longest time.

This embodies the **principle of temporal locality**:

- Data used recently is more likely to be used again soon.
- Data not used for a long time is less likely to be used again soon.

LRU approximates Belady’s optimal rule by greedily assuming:

- "Past is a proxy for the (unknown) future."

#### 1. Visual Example

Consider a cache of size 3 and access sequence:

```text
Accesses: A, B, C, A, D, B, E
Cache size: K = 3
```

We track cache contents after each access using LRU.

Initial: cache is empty `[]`.

1. Access A → miss
   - Cache: `[A]` (A is most recently used)

2. Access B → miss
   - Cache: `[A, B]` (B is most recent)

3. Access C → miss
   - Cache: `[A, B, C]` (C most recent, A least recent)

4. Access A → hit
   - Cache before: `[A, B, C]` (A least recent)
   - After access, A becomes most recent
   - Cache (recency order): `[B, C, A]` (B LRU, A MRU)

5. Access D → miss, cache full
   - LRU item is B (least recently used)
   - Evict B, insert D as MRU
   - Cache: `[C, A, D]` (C LRU, D MRU)

6. Access B → miss
   - LRU is C → evict C, insert B as MRU
   - Cache: `[A, D, B]` (A LRU, B MRU)

7. Access E → miss
   - LRU is A → evict A, insert E
   - Cache: `[D, B, E]`

**State consistency:**

- Cache size never exceeds 3.
- At each step, LRU victim is the least recently used among current items.

### C. Why LRU is Greedy but Not Optimal

**Greedy nature:**

- At eviction time, LRU uses **only local history** (recent access times) to decide.
- It does not look ahead or globally optimize over the access sequence.

**Optimal but impossible strategy (Belady):**

- If the future access sequence were known, evict the item with farthest next use.
- This is globally optimal but unrealizable.

**Counterexample where LRU is suboptimal:**

Consider cache size 2 and access sequence:

```text
A, B, C, A, B, C, ...
```

- LRU often oscillates badly depending on initial state.
- Belady’s optimal (with full future knowledge) can do strictly better.

Key point:

> LRU is a **greedy heuristic** that often works well in practice due to locality, but it has **no global optimality guarantee** on arbitrary sequences.

Other greedy-ish policies exist:

- **LFU (Least Frequently Used):** Evict item with lowest access count.
- **MRU (Most Recently Used):** Evict the most recently used item (surprisingly good for some workloads).

But LRU is the most common in general-purpose systems because temporal locality appears in many real workloads.

---

## 🧠 CHAPTER 4: GREEDY AS APPROXIMATION (SET COVER)

### A. When Optimal is NP-Hard

Many natural optimization problems that arise in systems and data engineering are **NP-hard**. Examples include:

- Set Cover
- Vertex Cover
- Traveling Salesman Problem (TSP)
- Facility Location

For these, we typically **cannot** hope for an efficient exact algorithm for all inputs (unless P=NP).

Instead, we aim for:

> **Approximation algorithms:** Efficient algorithms that are not optimal, but guarantee to come within a known factor of optimal.

Greedy is often the right tool here.

### B. Set Cover: Greedy O(log n) Approximation

**Set Cover Problem (simplified):**

- Universe `U` of `n` elements.
- Collection of subsets `S1, S2, ..., Sk` whose union is `U`.
- Each set `Si` has a cost (often 1 for the simplest version).
- Goal: pick a minimum number of sets whose union covers all elements in `U`.

This is NP-hard. But there is a classic greedy approximation:

**Greedy Algorithm:**

1. Initially, no elements are covered.
2. While some elements remain uncovered:
   - Pick the set that covers the **largest number of currently uncovered elements** (break ties arbitrarily).
   - Add it to the solution and mark those elements as covered.

**Approximation Guarantee:**

- This greedy algorithm achieves an **O(log n)** approximation factor.
- Meaning: total number of sets chosen by greedy is at most O(log n) times the optimal number.

We won’t go through the full proof here, but the core idea uses:

- A charging argument, or
- Comparing progress of greedy to an optimal solution over geometric phases.

### C. Why This Matters for Systems

In large-scale systems, you often have problems like:

- **Cache/replica placement:** choose which servers store which content to minimize latency.
- **Monitoring & logging:** choose where to place monitors or log collectors to cover all traffic paths.
- **Test selection:** choose minimal tests that cover all features or components.

These map to variants of set cover or related NP-hard problems.

Exact optimization is impractical, but **greedy approximations**:

- Are conceptually simple.
- Are easy to implement and reason about.
- Come with **mathematical guarantees**: “We’re within this factor of optimal.”

Greedy here is not about being perfect; it’s about being **predictably good**.

---

## 🔗 CHAPTER 5: INTEGRATION & MASTERY

### Connecting the Dots

Across Week 12, you have seen three faces of greedy:

1. **Exactly optimal on structured problems**
   - Activity selection, MST, Huffman, fractional knapsack.
   - Strong structural properties (optimal substructure, greedy choice property).

2. **Heuristic, no guarantee, but good in practice**
   - LRU caching, some routing heuristics.
   - Rely on patterns in real workloads (locality, typical topologies).

3. **Provably good approximations for hard problems**
   - Set cover and other NP-hard problems.
   - Greedy achieves bounded approximation ratios.

As a systems-oriented engineer or DSA practitioner, you should be able to:

- Recognize when a problem is likely **greedy-friendly** in the strong sense (like MST).
- Recognize when greedy is a **pragmatic heuristic** with no guarantee (like LRU).
- Recognize when greedy is used for **approximation with formal bounds** (like set cover).

### Pattern Recognition Cheat Sheet

When you see a problem:

1. **Is there a clear cut or frontier property?**
   - MST-style cut property often invites greedy.

2. **Is the problem continuous/linear with fractions allowed?**
   - Fractional knapsack-style greedy by density may work.

3. **Is the problem NP-hard (or suspected to be)?**
   - Think in terms of approximation; greedy may give good bounds.

4. **Is the system online (sequence of requests with no future knowledge)?**
   - Greedy heuristics like LRU are often used; evaluate them via competitive analysis or empirical results.

### Socratic Reflection

1. **Why does the MST cut property make greedy safe, whereas no analogous simple property exists for shortest paths with negative weights?**

2. **In caching, why does a “future-aware” but impossible policy (Belady’s) help us reason about the quality of practical greedy policies like LRU?**

3. **For set cover, why does picking the set that covers the most uncovered elements “feel” greedy but still give strong theoretical guarantees?**

### Retention Hook

> **The Essence:** "Greedy in systems is about making fast, local decisions: pick the lightest edge to grow your network, route packets in the most promising direction, evict the least recently used data, or cover the most elements per step. Sometimes theory guarantees these choices are optimal. Sometimes they’re just good-enough heuristics. Mastery is knowing which is which."

---

## 🧠 5 COGNITIVE LENSES

### 1. 💻 The Hardware & Systems Lens

- MST algorithms (Kruskal/Prim) are implemented with efficient data structures (heaps, union-find) and run at the core of network design tools.
- LRU caches are realized via linked lists + hash maps or approximations like CLOCK in OS kernels.
- Greedy algorithms are often **branch-light** and cache-friendly, making them attractive for performance-critical code.

### 2. 📉 The Trade-off Lens

- **Exact Greedy (MST):**
  - Great when structural properties hold.
  - Provides both correctness and speed.

- **Heuristic Greedy (LRU, routing):**
  - Simple and fast but might be far from optimal in worst cases.
  - Often chosen because alternatives are too complex or require impossible information.

- **Approximate Greedy (Set Cover):**
  - Balances tractability with guaranteed closeness to optimal.
  - Crucial when problem is NP-hard.

### 3. 👶 The Learning Lens

Students often:

- Overgeneralize: believing greedy is always good or always bad.
- Fail to distinguish between **provably optimal greedy** vs **heuristic greedy**.
- Struggle to connect abstract greedy proofs to concrete systems (networks, caches).

This day is meant to close that gap.

### 4. 🤖 The AI/ML Lens

- MST-like structures appear in clustering (e.g., single-linkage clustering builds an MST).
- Greedy feature selection in ML can be seen as a set cover-like process.
- Memory and parameter caches in large ML systems often approximate LRU.

Understanding greedy’s behavior in these contexts helps in debugging and optimizing ML pipelines.

### 5. 📜 The Historical Lens

- Kruskal (1956) and Prim (1957) provided early, elegant greedy algorithms for MST.
- Belady’s optimal caching policy (1966) gave a theoretical gold standard for replacement strategies.
- Approximation algorithms for set cover and related problems matured in the 1970s–1980s, solidifying greedy as a central tool in theoretical CS.

---

## ⚔️ SUPPLEMENTARY OUTCOMES

### 🏋️ Practice Problems (8–10)

| # | Problem | Source | Difficulty | Key Concept |
| :--- | :--- | :--- | :--- | :--- |
| 1 | Kruskal’s MST | Standard textbook / online judges | Easy | Edge-based greedy MST |
| 2 | Prim’s MST | Standard | Easy-Medium | Vertex-based greedy MST |
| 3 | Network Design (Min Cost) | Custom | Medium | Apply MST to real network cost models |
| 4 | LRU Cache | LeetCode 146 | Medium | Design LRU data structure |
| 5 | LFU Cache | LeetCode 460 | Hard | Explore alternative greedy caching |
| 6 | Set Cover (small instances) | Custom | Medium | Implement greedy and compare with brute force |
| 7 | Greedy Routing Simulation | Custom | Medium | Simulate greedy forwarding in a small graph |
| 8 | Cache Miss Analysis | Custom | Medium | Compare LRU vs random vs FIFO on sequences |

### 🎙️ Interview Questions (6–8)

1. **Q:** Explain why Kruskal’s and Prim’s algorithms are considered greedy. What properties of MST make greedy safe here?

2. **Q:** Describe LRU cache replacement. In what sense is LRU a greedy strategy? Can you give a scenario where it performs poorly?

3. **Q:** What is the set cover problem, and how does the greedy algorithm approximate it? What guarantee does it provide?

4. **Q:** Contrast a greedy heuristic used for caching with a greedy algorithm that is provably optimal (like Huffman or MST). How should an engineer treat these differently?

5. **Q:** In network routing, what information is needed for purely local greedy forwarding? What are the risks of relying solely on local information?

6. **Q:** How would you argue to a stakeholder that an approximation algorithm is acceptable for a production system where exact optimization is infeasible?

### ❌ Common Misconceptions (3–5)

1. **Myth:** If greedy works for MST and Huffman, it will also be optimal for most other problems.
   - **Reality:** MST and Huffman have special structure (cut property, tree representation). Many problems (like TSP or general set cover) do not.

2. **Myth:** LRU caching is always close to optimal.
   - **Reality:** LRU can be arbitrarily bad on adversarial sequences; it just tends to work well on real workloads.

3. **Myth:** Approximation algorithms are “just heuristics” with no theoretical backing.
   - **Reality:** Many greedy approximations come with provable bounds (e.g., O(log n) for set cover), offering strong guarantees.

4. **Myth:** Greedy routing is equivalent to running a global shortest-path algorithm.
   - **Reality:** Greedy routing typically uses only local information and can get stuck in local minima; shortest-path algorithms use global topology.

### 🚀 Advanced Concepts (3–5)

1. **Online Algorithms & Competitive Analysis:**
   - Framework for analyzing algorithms like LRU where input arrives over time.
   - Compare cost of online algorithm to optimal offline algorithm.

2. **Primal-Dual Methods for Approximation:**
   - Many greedy approximations (like set cover) can be interpreted via primal-dual linear programming.

3. **Greedy Spanners & Network Design:**
   - Building sparse subgraphs (spanners) that approximately preserve distances using greedy edge selection.

4. **Hierarchical Caching & Multi-Level LRU:**
   - Real systems use multi-level caches; combining local greedy decisions at each level leads to complex global behavior.

5. **Greedy in Clustering (e.g., MST-based clustering):**
   - Use MST and cut edges to form clusters; greedy choices affect final partitioning.

### 📚 External Resources

- **CLRS — Chapters on Greedy Algorithms & MST:** For formal MST proofs and cut property.
- **Algorithm Design (Kleinberg & Tardos):** Sections on approximation algorithms and set cover.
- **Operating Systems Texts (e.g., Tanenbaum, Silberschatz):** For cache replacement policies and LRU variants.
- **Networking Texts (e.g., Kurose & Ross):** For routing protocols and local vs global routing decisions.
- **Online Lectures (MIT, Stanford) on Approximation Algorithms:** For deeper treatment of greedy as approximation.

---

**End of Week 12 Day 05 Instructional File**  
**Status:** ✅ Complete | **Scope:** Optional Greedy in Systems (Networks, Caches, Approximation)
