# ✅ Week 08 Daily Progress Checklist – Graph Fundamentals

Version: v1.0  
Filename: `Week_08_Daily_Progress_Checklist.md`  
Week: 08 – Graph Fundamentals: Representations, BFS, DFS & Topological Sort

> Use this as your **daily planner**. Each day has concrete concepts, activities, and reflection prompts.

---

## 📅 Day 1 – Graph Models & Representations

### 🎯 Core Concepts to Understand

- [ ] Difference between **directed** and **undirected** graphs (with examples).  
- [ ] Difference between **weighted** and **unweighted** graphs.  
- [ ] Three main representations: **adjacency list**, **adjacency matrix**, **edge list**.  
- [ ] What makes a graph **sparse** vs **dense** and why it matters.  
- [ ] What an **implicit graph** is, especially in grids and puzzles.

### 🧪 Activities

- [ ] Draw at least **three real-world scenarios** and model them as graphs:
  - Social network.  
  - Road network between 6–8 cities.  
  - Course prerequisite structure.

- [ ] For each scenario, decide:
  - [ ] Nodes and edges.  
  - [ ] Directed or undirected?  
  - [ ] Weighted or unweighted?  
  - [ ] Best representation and why.

- [ ] Implement or pseudo-code **adjacency list construction** from an edge list.

### 🤔 Reflection

- [ ] Can you explain to a friend **why adjacency list is usually better** for sparse graphs?  
- [ ] Can you think of a case where **adjacency matrix** is clearly superior?

---

## 📅 Day 2 – Breadth-First Search (BFS)

### 🎯 Core Concepts to Understand

- [ ] BFS as **layered exploration** using a queue.  
- [ ] Why BFS gives **shortest paths in unweighted graphs**.  
- [ ] How to maintain **distance** and **parent** arrays.  
- [ ] How BFS behaves on **trees** versus general graphs.

### 🧪 Activities

- [ ] Hand-trace BFS on a small graph (6–8 nodes):
  - [ ] Write down queue contents at each step.  
  - [ ] Record the order of visits and the layer numbers.

- [ ] Practice BFS on a **grid**:
  - [ ] Draw a 5×5 grid with some blocked cells.  
  - [ ] Manually simulate BFS from a start cell to a goal cell.

- [ ] Write pseudo-code for BFS and **annotate** where you:
  - [ ] Initialize distances.  
  - [ ] Mark visited nodes.  
  - [ ] Enqueue and dequeue.

### 🤔 Reflection

- [ ] Can you clearly explain why **marking visited on enqueue** is important?  
- [ ] Can you identify **two problems** where BFS is the obvious choice?

---

## 📅 Day 3 – DFS & Topological Sort

### 🎯 Core Concepts to Understand

- [ ] DFS mechanics (recursive and with explicit stack).  
- [ ] Concept of **DFS tree** and notion of **back edges** in directed graphs.  
- [ ] How DFS can be used to detect **cycles**.  
- [ ] What a **topological ordering** is and why it only exists for DAGs.  
- [ ] Two topo sort methods: **DFS post-order reverse** and **Kahn’s algorithm**.

### 🧪 Activities

- [ ] On paper, run DFS on a small directed graph:
  - [ ] Record discovery and finish times.  
  - [ ] Identify any back edges.

- [ ] Implement or pseudo-code **DFS-based topo sort** and **Kahn’s algorithm**.  
- [ ] For a small DAG representing course prerequisites, compute a **topological order** by hand.

### 🤔 Reflection

- [ ] Can you explain **why reversing DFS post-order** gives a topological order?  
- [ ] Can you describe a real-world system where you would use topological sort?

---

## 📅 Day 4 – Connectivity & Bipartite Graphs

### 🎯 Core Concepts to Understand

- [ ] Definition of **connected component** in an undirected graph.  
- [ ] How to use BFS/DFS to label components.  
- [ ] Definition of a **bipartite** graph and the role of **odd cycles**.  
- [ ] How **2-coloring via BFS/DFS** tests bipartiteness.  
- [ ] High-level understanding of **Union–Find** and when it helps.

### 🧪 Activities

- [ ] Draw an undirected graph with at least **three components** and identify them.  
- [ ] Run a conceptual BFS/DFS from each unvisited node and label components with IDs.

- [ ] Create one **bipartite** and one **non-bipartite** graph:
  - [ ] Perform BFS 2-coloring on both.  
  - [ ] Highlight the **odd cycle** causing conflict in the second graph.

- [ ] Outline how Union–Find would handle connectivity for a sequence of **edge additions**.

### 🤔 Reflection

- [ ] Can you explain why **only odd cycles** break bipartiteness?  
- [ ] Can you think of a scheduling or grouping problem naturally expressed as a bipartite test?

---

## 📅 Day 5 – Strongly Connected Components (SCC) & Integration

### 🎯 Core Concepts to Understand

- [ ] Definition of **strongly connected component** (mutual reachability).  
- [ ] High-level steps of **Kosaraju’s algorithm**.  
- [ ] Idea of **low-link values** in Tarjan’s algorithm.  
- [ ] Why **condensation graph** of SCCs is always a DAG.  
- [ ] How Week 08 concepts feed into **Week 09+ graph algorithms**.

### 🧪 Activities

- [ ] Draw a directed graph with at least **three SCCs** and label them.  
- [ ] Manually perform Kosaraju’s algorithm:
  - [ ] First DFS pass to compute finishing order.  
  - [ ] Second pass on transposed graph to extract SCCs.

- [ ] Sketch the condensation DAG and verify it is acyclic.

- [ ] Write a short note: “How BFS, DFS, topo, components, and SCCs connect to Dijkstra/MST/flow in later weeks.”

### 🤔 Reflection

- [ ] Can you explain in plain language **why condensation removes cycles** between SCCs?  
- [ ] Can you think of a real example where grouping nodes into SCCs simplifies reasoning?

---

## 🔄 Weekly Integration & Retrospective

### 🧩 Integration Tasks (Any Day After Day 3)

- [ ] Take a small **task dependency** example and:
  - [ ] Build the graph.  
  - [ ] Detect if a cycle exists.  
  - [ ] If acyclic, compute a topological order.

- [ ] On a sample social network graph:
  - [ ] Find connected components.  
  - [ ] Check if the friendship graph is bipartite.  
  - [ ] Interpret what each result means.

- [ ] For a grid-based pathfinding problem:
  - [ ] Treat each cell as a node, moves as edges.  
  - [ ] Run BFS to find shortest path.  
  - [ ] Consider how you would adapt this if obstacles changed dynamically.

### 🧠 Weekly Reflection Prompts

Answer these at the end of the week (in a notebook or doc):

1. **Modelling:** Which two problems this week became much easier once you thought “this is actually a graph”?  
2. **Patterns:** When faced with a new problem, how quickly can you identify whether it is BFS, DFS/topo, connectivity, bipartite, or SCC-based?  
3. **Trade-offs:** Where did choice of **representation** (list vs matrix vs implicit) clearly affect performance or simplicity?  
4. **Confidence:** Which concept do you feel strongest in (BFS, DFS/topo, connectivity, bipartite, SCC)? Which needs another dedicated session?  
5. **Forward link:** How do you expect Week 09’s shortest-path and MST algorithms to build on what you learned here?

---

## 🧾 End-of-Week Checklist (Quick Scan)

- [ ] I can **model** common systems as graphs with clear nodes and edges.  
- [ ] I can choose between **adjacency list/matrix/edge list** and justify my choice.  
- [ ] I can hand-trace **BFS** and **DFS** and explain their differences.  
- [ ] I can perform and implement **topological sort** on DAGs.  
- [ ] I can find **connected components** and **test bipartiteness**.  
- [ ] I have at least a **conceptual understanding** of SCCs and condensation DAGs.  
- [ ] I have attempted a reasonable mix of **Stage 1–3 problems** from the roadmap.  
- [ ] I can explain at least two **real-world scenarios** where these graph concepts appear.

If you can check most of these, you are ready to move into Week 09’s **graph algorithms** with confidence.
