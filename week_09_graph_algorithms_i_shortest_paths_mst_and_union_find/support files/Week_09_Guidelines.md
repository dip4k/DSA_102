# 📋 Week 09 Guidelines — Graph Algorithms I: Shortest Paths, MST & Union–Find

**Version:** 1.0  
**Status:** ✅ ACTIVE  
**Last Updated:** January 23, 2026  
**Target Audience:** DSA Mastery Course Students (Intermediate-Advanced)

---

## 🎯 WEEK OVERVIEW

### Primary Goal
Master foundational graph algorithms for optimization and connectivity: shortest paths (single-source, all-pairs), minimum spanning trees, and disjoint set union for dynamic connectivity queries.

### Why This Week Matters
- **Ubiquitous in production:** Dijkstra powers GPS navigation (billions of queries daily). Kruskal's algorithm optimizes network infrastructure. DSU detects cycles in social networks.
- **Foundational patterns:** Relaxation, greedy choice, dynamic programming, and forest data structures appear in advanced topics (Weeks 10-18).
- **Interview high-frequency:** 40%+ of senior-level graph interview questions involve shortest paths or MST. DSU is asked in 20%+ of graph problems.
- **Real systems:** Understanding trade-offs (Dijkstra vs. Bellman–Ford, Floyd–Warshall vs. single-source) is critical for system design.

### Learning Arc
```
Week 08: Graph Basics (BFS, DFS, connectivity)
        ↓
Week 09: Optimizations (shortest paths, MST, connectivity data structures)
        ↓
Week 10+: Advanced Applications (DP on graphs, advanced flows, etc.)
```

---

## 📅 DAILY LEARNING STRUCTURE

### Day 1: Dijkstra's Algorithm (Core)
**Time Allocation:** 3-4 hours  
**Depth:** Mental model → Implementation → Optimization variants

**Learning Path:**
1. **Conceptual (30 min):** Understand shortest-path problem; why priority queue is needed; relaxation principle.
2. **Visual Walkthrough (40 min):** Trace algorithm on 5-vertex graph; observe frontier expansion; understand stale-entry mechanism.
3. **Implementation (60 min):** Code Dijkstra; test on provided examples; verify distances match manual calculation.
4. **Optimization Deep Dive (30 min):** Learn Fibonacci heap variant, bidirectional search, A* heuristic (conceptually).
5. **Practice (60 min):** Solve 2-3 LeetCode problems; focus on path reconstruction and edge cases.
6. **Real-World Context (30 min):** Study GPS navigation algorithm; understand preprocessing techniques (A*, landmarks).

**Key Checkpoint:**
- [ ] Can trace Dijkstra on a graph without notes?
- [ ] Can implement in < 15 minutes?
- [ ] Can explain why it fails on negative weights?
- [ ] Can identify when Dijkstra is appropriate vs. BFS vs. Bellman–Ford?

---

### Day 2: Bellman–Ford & Negative Weights (Core)
**Time Allocation:** 3-4 hours  
**Depth:** Why negatives break Dijkstra; relaxation as DP; cycle detection

**Learning Path:**
1. **Motivation (20 min):** Currency exchange arbitrage; why negative weights appear; limitations of greedy.
2. **Algorithm Mechanics (50 min):** Understand V-1 passes; relaxation over all edges; why it works without priority queue.
3. **Visual Walkthrough (40 min):** Trace on graph with negative edges; show how path improves over passes; demonstrate cycle detection.
4. **Implementation (60 min):** Code Bellman–Ford; test on negative-weight examples; verify cycle detection.
5. **Comparison (30 min):** Head-to-head with Dijkstra; understand O(VE) vs. O((V+E) log V); when Bellman–Ford is competitive.
6. **Practice (60 min):** Solve problems with negatives; practice cycle detection; understand reachability of cycles.

**Key Checkpoint:**
- [ ] Can implement Bellman–Ford from memory?
- [ ] Can detect and handle negative cycles?
- [ ] Can explain why V-1 passes suffice?
- [ ] Can compare trade-offs: Dijkstra vs. Bellman–Ford vs. Floyd–Warshall?

---

### Day 3: Floyd–Warshall & All-Pairs Shortest Paths (Core, Enhanced)
**Time Allocation:** 3-4 hours  
**Depth:** DP over intermediate vertices; all-pairs perspective; when it beats single-source approaches

**Learning Path:**
1. **Problem Statement (20 min):** Understand APSP; why all-pairs matters; applications in network analysis.
2. **DP Formulation (40 min):** Understand k-dimension as intermediate vertices; recurrence relation; why k-loop must be outermost.
3. **Visual Walkthrough (50 min):** Trace on 4-vertex graph; show k-iterations; observe distance matrix evolution.
4. **Implementation (60 min):** Code Floyd–Warshall; implement path reconstruction; test on small graphs.
5. **Optimization (20 min):** Learn variations (Johnson's algorithm for sparse graphs); discuss matrix multiplication approaches (conceptually).
6. **Practice (60 min):** Solve all-pairs problems; practice transitive closure; solve network reliability queries.

**Key Checkpoint:**
- [ ] Can trace Floyd–Warshall without notes?
- [ ] Understand why k-loop order is critical?
- [ ] Can detect negative cycles from diagonal?
- [ ] Know when Floyd–Warshall is better than running Dijkstra V times?

---

### Day 4: Minimum Spanning Trees: Kruskal & Prim (Core)
**Time Allocation:** 4-5 hours  
**Depth:** MST theory (cut property); two canonical greedy algorithms; DSU integration

**Learning Path:**
1. **MST Definition & Theory (30 min):** Understand spanning trees; why minimum weight matters; cut property intuition.
2. **Kruskal's Algorithm (60 min):** Sort edges; greedy edge addition; DSU for cycle detection. Trace on example. Implement.
3. **Prim's Algorithm (60 min):** Grow tree from a start node; priority queue frontier (similar to Dijkstra). Trace. Implement.
4. **Comparison (20 min):** Kruskal O(E log E) vs. Prim O((V+E) log V); when each is preferred.
5. **Applications (30 min):** Network design; clustering; Steiner trees (conceptually).
6. **Practice (90 min):** Solve 3-4 MST problems; mix of Kruskal and Prim; understand edge cases (duplicate weights, disconnected graphs).

**Key Checkpoint:**
- [ ] Can implement both Kruskal and Prim?
- [ ] Understand cut property and why it ensures correctness?
- [ ] Can trace MST algorithms on graphs?
- [ ] Know when to use Kruskal vs. Prim?

---

### Day 5: Disjoint Set Union (DSU) / Union–Find in Depth (Optional Advanced)
**Time Allocation:** 3-4 hours  
**Depth:** Forest data structure; optimizations (union-by-rank, path compression); inverse Ackermann complexity

**Learning Path:**
1. **Motivation (20 min):** Why connectivity queries are expensive in explicit graphs; DSU as lightweight alternative.
2. **Data Structure (40 min):** Parent pointers; forest representation; roots as representatives; rank concept.
3. **Operations (80 min):** Implement make_set, find (with path compression), union (with union-by-rank). Trace. Test.
4. **Optimizations (30 min):** Understand union-by-rank prevents tall trees; path compression flattens structure; both together achieve O(α(n)).
5. **Theory (20 min):** Inverse Ackermann intuition; why it's essentially constant for practical n.
6. **Applications (60 min):** Cycle detection in forests; connected components; Kruskal's MST; offline LCA queries (conceptually).

**Key Checkpoint:**
- [ ] Can implement DSU with both optimizations?
- [ ] Understand path compression effects?
- [ ] Know union-by-rank prevents degeneration?
- [ ] Can use DSU for cycle detection?

---

## 🏗️ LEARNING STRATEGIES & TIPS

### Strategy 1: Mental Models Before Code
- **Days 1-3:** Spend first hour visualizing algorithms on paper. Understand *why* each step works. Then code.
- **Don't memorize:** Understand the principles (relaxation, greedy cuts, forest structure). Code flows naturally.

### Strategy 2: Trace-Before-Code Protocol
- For each algorithm: (1) Read pseudocode, (2) Trace on provided graph, (3) Verify distances/tree manually, (4) Code implementation, (5) Test code against manual trace.
- **This prevents bugs:** ~70% of coding errors disappear if you trace first.

### Strategy 3: Complexity Analysis as a Skill
- Don't just memorize O((V+E) log V). Derive it: why O(E log V) for insert/extract? Why O(V) extractions? Build the analysis step by step.
- **This prepares for interviews:** Interviewers value derivation over memorization.

### Strategy 4: Comparative Thinking
- Always ask: "When is this algorithm better than alternatives?"
- Dijkstra vs. Bellman–Ford vs. Floyd–Warshall: Each has a niche. Understand the niches.
- This mental exercise embeds the material.

### Strategy 5: Real-World Motivation
- Each algorithm has compelling real-world use: GPS, arbitrage detection, network reliability, clustering, social networks.
- Connect algorithms to systems you use daily. Motivation improves retention.

### Strategy 6: Implementation Variants
- Days 1-3: Implement basic versions (iterative, no optimizations).
- Day 5: Revisit and optimize (e.g., add path compression to DSU).
- This shows how optimizations layer on top of basic ideas.

---

## 🎙️ INTERVIEW PREPARATION

### High-Frequency Interview Patterns (Ranked)

| Rank | Pattern | Frequency | Example | Solution |
|:---|:---|:---|:---|:---|
| 1 | Shortest path; identify algorithm | 25% | "Find min-cost route" | Dijkstra or BFS |
| 2 | MST; Kruskal vs. Prim | 15% | "Connect all cities with min cost" | Kruskal (with DSU) or Prim |
| 3 | Cycle detection | 12% | "Detect cycle in undirected graph" | DSU or DFS |
| 4 | Connected components | 10% | "How many groups?" | DSU or BFS/DFS |
| 5 | Negative weights; Bellman–Ford | 8% | "Shortest path with debits?" | Bellman–Ford |
| 6 | All-pairs shortest path | 6% | "Distance matrix for all pairs" | Floyd–Warshall or Dijkstra×V |
| 7 | Bipartite checking | 6% | "Can graph be 2-colored?" | BFS/DFS with coloring |
| 8 | Topological sort (graph review) | 5% | "Dependency resolution" | DFS post-order or Kahn |
| 9 | Advanced (A*, bidirectional, etc.) | 5% | "Optimize pathfinding" | A* or bidirectional search |
| 10 | Transitive closure; DSU offline | 4% | "Is A reachable from B?" | DFS or DSU |

### Interview Strategy by Difficulty

**Easy (20% of questions):**
- Basic Dijkstra implementation
- Simple MST problem
- Connected component counting
- Cycle detection on small graph
- **Time:** 15-20 min; focus on correct implementation

**Medium (50% of questions):**
- Dijkstra with obstacles or constraints
- Kruskal's MST with tie-breaking
- All-pairs shortest path (when?which?)
- Cycle detection with negative weights
- DSU for connectivity in dynamic graph
- **Time:** 30-45 min; need optimization trade-offs discussion

**Hard (30% of questions):**
- Combine shortest paths + MST (e.g., "min-cost spanning tree with diameter constraint")
- Dijkstra + DP (e.g., "k shortest paths")
- DSU + advanced techniques (e.g., "offline LCA queries")
- System design (e.g., "design social network recommendation engine")
- **Time:** 45-60 min; deep understanding + system thinking

### Pre-Interview Checklist

- [ ] **Dijkstra:** Code in < 10 min; understand when it fails; know time complexity derivation
- [ ] **Bellman–Ford:** Understand V-1 passes; know cycle detection; compare with Dijkstra
- [ ] **Floyd–Warshall:** Understand k-dimension; know when it beats single-source approaches
- [ ] **Kruskal's:** Understand DSU integration; know edge sorting strategy
- [ ] **Prim's:** Understand priority queue frontier; compare with Dijkstra
- [ ] **DSU:** Implement with both optimizations; understand path compression + union-by-rank
- [ ] **Trade-offs:** For any problem, can you articulate pros/cons of different algorithms?
- [ ] **System thinking:** Can you apply these algorithms to real problems (routing, clustering, etc.)?

---

## 🧪 PRACTICE STRUCTURE

### Problem Tier System

**Tier 1: Foundational (Do First)**
- Implement basic Dijkstra; trace on examples
- Implement DSU; use for cycle detection
- Sort edges; build MST via Kruskal
- Count connected components

**Tier 2: Integration (Do Second)**
- Combine algorithms: Dijkstra + MST, shortest path + constraints
- Handle edge cases: disconnected graphs, duplicate weights, negative cycles
- Optimize: when is each algorithm appropriate?

**Tier 3: Advanced (Do Third)**
- System design: GPS navigation, recommendation engine, network optimization
- Combine with DP: k shortest paths, constrained MST
- Offline queries: LCA via DSU, batch processing

### Recommended Problem Sets

**LeetCode (by algorithm):**
- **Dijkstra:** #743 (Network Delay), #1631 (Path Effort), #778 (Swim in Water)
- **Bellman–Ford:** #787 (Cheapest Flights), #1334 (Floyd–Warshall variant)
- **Floyd–Warshall:** Custom or #1334
- **Kruskal/MST:** #1135 (Connecting Cities)
- **DSU:** #684 (Redundant Connection), #721 (Accounts Merge), #323 (Connected Components)

**LeetCode (by difficulty):**
- Easy: #743, #323, #684
- Medium: #787, #547 (Friend Circles), #1202 (Smallest Swaps)
- Hard: #1135, #685 (Redundant II), #959 (Regions by Slashes)

---

## 📊 SELF-ASSESSMENT & PROGRESS

### Knowledge Checkpoint: Daily

**Day 1 (Dijkstra):**
- [ ] Understand priority queue frontier concept
- [ ] Can trace Dijkstra on a 5-vertex graph without notes
- [ ] Can implement in code within 15 minutes
- [ ] Can explain why negatives break it
- [ ] Know O((V+E) log V) derivation, not just memorization

**Day 2 (Bellman–Ford):**
- [ ] Understand why V-1 passes suffice
- [ ] Can implement V-1 relaxation passes
- [ ] Can detect negative cycles
- [ ] Know when Bellman–Ford is better than Dijkstra
- [ ] Understand O(VE) complexity trade-off

**Day 3 (Floyd–Warshall):**
- [ ] Understand k-dimension as intermediate vertices
- [ ] Can trace through k-iterations
- [ ] Understand why k-loop must be outermost
- [ ] Can detect negative cycles from diagonal
- [ ] Know when all-pairs Floyd–Warshall beats single-source approaches

**Day 4 (MST: Kruskal & Prim):**
- [ ] Understand MST definition and cut property
- [ ] Can implement Kruskal's algorithm (with DSU)
- [ ] Can implement Prim's algorithm (with priority queue)
- [ ] Know when to use Kruskal vs. Prim
- [ ] Can trace MST construction on example

**Day 5 (DSU):**
- [ ] Understand parent pointers as tree representation
- [ ] Can implement make_set, find, union
- [ ] Can apply path compression correctly
- [ ] Can apply union-by-rank correctly
- [ ] Know O(α(n)) amortized complexity (and why it's O(1) in practice)

### Skill Progression Model

```
Week Start:
├─ Conceptual Understanding: 0%
├─ Implementation Ability: 0%
├─ Trade-off Analysis: 0%
└─ Interview Readiness: 0%

After Day 1 (Dijkstra):
├─ Conceptual: 20% (1 algorithm understood deeply)
├─ Implementation: 30% (can code Dijkstra)
├─ Trade-off: 15% (understand Dijkstra's place)
└─ Interview: 20% (can solve Dijkstra problem)

After Day 3:
├─ Conceptual: 70% (3 shortest-path algorithms + theory)
├─ Implementation: 70% (can code all three)
├─ Trade-off: 50% (understand when each is used)
└─ Interview: 60% (can solve shortest-path problems)

After Day 5:
├─ Conceptual: 95% (all core algorithms understood)
├─ Implementation: 90% (can code all; understand optimizations)
├─ Trade-off: 90% (articulate pros/cons of each approach)
└─ Interview: 85% (ready for graph algorithm questions)

Week End Target:
├─ Conceptual: 100% (mastered all topics)
├─ Implementation: 95% (code is natural; optimizations understood)
├─ Trade-off: 95% (can recommend algorithm for any scenario)
└─ Interview: 95% (confident answering graph questions; system thinking applied)
```

---

## 🔗 CONNECTIONS TO OTHER WEEKS

### Prerequisites (Review if Needed)
- **Week 08 (Graph Basics):** BFS, DFS, connectivity — ensure comfortable with graph representations
- **Week 03 (Priority Queues):** Heap operations — needed for Dijkstra and Prim
- **Week 01 (Complexity Analysis):** Big-O notation — needed to understand O((V+E) log V), O(α(n)), etc.

### Forward Links (Upcoming Applications)
- **Week 10 (DP I):** DP formulation appears in Bellman–Ford; reinforced in Floyd–Warshall
- **Week 11 (DP II):** Shortest paths in DAGs as DP; tree/graph DP extends MST concepts
- **Week 12 (Greedy):** Kruskal and Prim as canonical greedy algorithms; cut property generalized
- **Week 13 (Amortized Analysis):** DSU's inverse Ackermann bound is a classic amortized analysis example
- **Week 16 (Advanced DS):** Segment trees, Fenwick trees solve range queries; complement shortest paths for constrained queries

---

## 🎓 LEARNING OUTCOMES (By End of Week)

### What You'll Know
- [ ] **5 canonical algorithms:** Dijkstra, Bellman–Ford, Floyd–Warshall, Kruskal, Prim
- [ ] **3 data structures:** Priority queues (already used), DSU for connectivity, spanning trees
- [ ] **Complexity trade-offs:** When each algorithm is optimal; how to choose for a given problem
- [ ] **Real-world applications:** GPS navigation, network routing, arbitrage detection, MST clustering
- [ ] **Advanced optimizations:** Path compression, union-by-rank, bidirectional search, A*

### What You'll Be Able to Do
- [ ] **Implement from scratch:** All 5 algorithms; code them in 10-15 min each under time pressure
- [ ] **Trace algorithms:** Understand the step-by-step execution; debug by hand
- [ ] **Optimize:** Identify bottlenecks; apply standard optimizations
- [ ] **Interview:** Solve graph problems confidently; articulate trade-offs; extend algorithms for constraints
- [ ] **System design:** Apply algorithms to real problems; understand limitations and alternatives

### What You'll Understand
- [ ] **Greedy correctness:** Why cut property ensures greedy choices work for MST
- [ ] **DP structure:** Bellman–Ford and Floyd–Warshall as DP; state transitions over paths/intermediates
- [ ] **Amortized analysis:** DSU's O(α(n)) bound; why amortized analysis is powerful for complexity
- [ ] **Trade-offs:** Time vs. space, implementation simplicity vs. optimization complexity, generality vs. specificity

---

## 📚 RECOMMENDED RESOURCES

### Primary (Use Throughout Week)
- **CLRS (Introduction to Algorithms), Chapters 24-25:** Shortest paths, all-pairs, MST
- **Sedgewick & Wayne, Algorithms (4th ed.), Chapters 4-5:** Graph algorithms with practical focus
- **MIT OpenCourseWare 6.006:** Lectures on shortest paths, MST, DSU; excellent visualizations

### Supplementary (Deep Dives)
- **Competitive Programming (Halim & Halim):** Practical implementations and tricks
- **Research Papers:** Tarjan's inverse Ackermann analysis (DSU); Dijkstra's original paper (1959)
- **Visualization Tools:** Visualgo (graph algorithm animations), CLRS textbook examples

### Problem Repositories
- **LeetCode:** 800+ graph problems; filter by algorithm and difficulty
- **Codeforces:** Contest problems; good for practice under time pressure
- **InterviewBit:** Graph problems with solutions and explanations

---

## ⚠️ COMMON PITFALLS & HOW TO AVOID

| Pitfall | Why It Happens | How to Avoid |
|:---|:---|:---|
| **Dijkstra on negative weights** | Greediness breaks; doesn't re-visit vertices | Always check: are weights non-negative? If not, use Bellman–Ford |
| **Forgetting path compression in DSU** | Works but O(log n) per operation instead of O(α(n)) | Always implement: `parent[x] = Find(parent[x])` |
| **Wrong loop order in Floyd–Warshall** | K-loop must be outermost; other orders use unfinalized distances | Remember: k controls intermediate vertices; it's the outermost loop |
| **MST with tie-breaking** | If multiple edges have same weight, order matters for tie-breaking | Sort edges consistently; tie-breaking rule is implementation choice |
| **Disconnected graph components** | Shortest paths to unreachable vertices are ∞; MST for disconnected graph is a forest | Check for connectivity; handle unreachable vertices explicitly |
| **Floating-point precision** | Comparing floating-point distances via `==` is unreliable | Use epsilon-based comparisons: `abs(a - b) < 1e-9` |
| **Stale entries in priority queue** | Multiple entries for same vertex after updates | Check if vertex already processed before processing; ignore stale entries |

---

## 📅 TIME MANAGEMENT & PACE

### Recommended Daily Schedule

**Day 1 (Dijkstra): 3.5 hours total**
- Read/understand: 0.75 hrs
- Trace: 0.75 hrs
- Code: 1 hr
- Optimize + practice: 1 hr

**Day 2 (Bellman–Ford): 3.5 hours total**
- Read/understand: 0.75 hrs
- Trace: 0.75 hrs
- Code: 1 hr
- Compare + practice: 1 hr

**Day 3 (Floyd–Warshall): 3.5 hours total**
- Read/understand: 0.75 hrs
- Trace: 0.75 hrs
- Code: 1 hr
- Applications + practice: 1 hr

**Day 4 (Kruskal & Prim): 4.5 hours total**
- Read/understand: 1 hr
- Trace Kruskal: 0.75 hrs
- Code Kruskal: 1 hr
- Trace Prim: 0.75 hrs
- Code Prim + practice: 0.5 hrs

**Day 5 (DSU): 3.5 hours total**
- Read/understand: 0.75 hrs
- Trace: 0.75 hrs
- Code: 1 hr
- Optimizations + practice: 1 hr

**Total: ~18.5 hours for Week 09**
- Adjustable: 1.5-2.5 hours per day for 5 days of dedicated learning

### Flexibility Notes
- If shorter on time: Skip Day 5 (optional advanced); focus Days 1-4.
- If more time: Add Day 4 (MST) before Day 5; spend extra time on trade-offs and system design.
- If familiar with some topics: Skip ahead to challenging material; use time for interview prep.

---

## ✅ WEEK-END SELF-CHECK

**Before moving to Week 10, confirm:**

- [ ] Implemented all 5 core algorithms (Dijkstra, Bellman–Ford, Floyd–Warshall, Kruskal, Prim)
- [ ] Can trace each algorithm on a 5-vertex graph without notes
- [ ] Understand complexity of each: O((V+E) log V), O(VE), O(V³), O(E log E), O((V+E) log V), O(α(n))
- [ ] Solved at least 3 problems per algorithm (15+ problems total)
- [ ] Can articulate when to use each algorithm and why
- [ ] Understand trade-offs: time, space, simplicity, flexibility
- [ ] Understand real-world applications: GPS, arbitrage, MST clustering, connectivity queries
- [ ] Ready for graph algorithm interview questions
- [ ] Confident explaining algorithm decisions to a peer

**If not confident on any above:** Review that section before moving forward.

---

**Status:** ✅ Week 09 Guidelines — COMPLETE

