# 🎤 Week 08 Interview Q&A Reference – Graph Fundamentals

Version: v1.0  
Filename: `Week_08_Interview_QA_Reference.md`  
Week: 08 – Graph Fundamentals: Representations, BFS, DFS & Topological Sort

> ❗ This file is **questions-only** by design. No answers. Use it for mock interviews, self-quizzing, and whiteboard practice.

---

## 🧭 1. How to Use This File

- Treat each subsection as a **mini-interview round** focused on a topic.  
- For every primary question, practice:
  - First giving a **high-level explanation**.  
  - Then answering **follow-up questions** in more depth.  
- Work in **whiteboard mode**:
  - Draw small graphs.  
  - Trace BFS/DFS/topological sort steps out loud.  
- Rotate roles with a friend:
  - One plays interviewer using this file.  
  - The other answers; then switch.

Aim to cover all questions **twice**: once early in the week and once closer to interviews.

---

## 🌐 2. Graph Models & Representations (Day 1)

### Q1: Modelling Problems as Graphs

- How would you model a **social network** as a graph?  
  - Follow-up 1: What are the **nodes** and **edges**?  
  - Follow-up 2: Are the edges directed or undirected? Why?

- How would you model a **road map with distances** between cities?  
  - Follow-up 1: Which attributes become **weights**?  
  - Follow-up 2: When might you choose to ignore weights and treat it as unweighted?

---

### Q2: Representations – Adjacency List vs Matrix vs Edge List

- Explain **adjacency list**, **adjacency matrix**, and **edge list**.  
  - Follow-up 1: Compare their **space complexity** in terms of V and E.  
  - Follow-up 2: For each, give an operation that is **fast** and one that is **slow**.

- For a graph with V = 10^5 and E = 3×10^5, which representation would you pick and why?  
  - Follow-up 1: How does **sparsity** influence your decision?  
  - Follow-up 2: What operations might still suggest adding an adjacency matrix on top?

---

### Q3: Implicit Graphs

- What is an **implicit graph**? Give two examples.  
  - Follow-up 1: How would you generate neighbors in a **2D grid** maze?  
  - Follow-up 2: Why might you avoid materializing all nodes explicitly in a puzzle search problem?

- Suppose each **game state** in a puzzle is a node and valid moves are edges.  
  - Follow-up 1: How would you explore this state space with BFS or DFS?  
  - Follow-up 2: What are the trade-offs in memory and time?

---

### Q4: Graph Types

- Contrast **directed** and **undirected** graphs with real-world examples.  
  - Follow-up 1: Give a scenario that can be modelled both ways; which would you pick and why?  
  - Follow-up 2: How does directedness affect **cycle detection**?

- Explain the difference between **weighted** and **unweighted** graphs.  
  - Follow-up 1: Why is this distinction important for choosing between BFS and Dijkstra?  
  - Follow-up 2: Can an unweighted graph be treated as a weighted graph? When would that be useful?

---

## 🚍 3. Breadth-First Search (BFS) (Day 2)

### Q5: BFS Fundamentals

- Describe the **BFS algorithm** on an adjacency-list graph starting from a source node.  
  - Follow-up 1: What data structures are used and why?  
  - Follow-up 2: What is the **time and space complexity**?

- Why does BFS in an **unweighted graph** find shortest paths in terms of number of edges?  
  - Follow-up 1: What would break if we allowed **negative edge weights**?  
  - Follow-up 2: How do **distance layers** relate to shortest paths?

---

### Q6: BFS Variations & Applications

- How would you adapt BFS to find the shortest path in a **grid maze** from `S` to `G`?  
  - Follow-up 1: How do you handle walls/blocked cells?  
  - Follow-up 2: How would you reconstruct the actual path?

- Explain how BFS can be used to perform **level-order traversal on a tree**.  
  - Follow-up 1: Why is BFS on trees simpler than BFS on general graphs?  
  - Follow-up 2: How would you mark levels explicitly during traversal?

---

### Q7: BFS Edge Cases & Multi-Source BFS

- How do you handle **disconnected graphs** when running BFS?  
  - Follow-up 1: How can BFS help you find all **connected components** conceptually?  
  - Follow-up 2: How do you represent “unreachable” nodes in your distance array?

- What is **multi-source BFS** and when would you use it?  
  - Follow-up 1: How does its initialization differ from single-source BFS?  
  - Follow-up 2: Give a real-world example where multi-source BFS is natural.

---

## 🔎 4. Depth-First Search (DFS) & Topological Sort (Day 3)

### Q8: DFS Mechanics

- Describe the **DFS algorithm** on a graph using recursion.  
  - Follow-up 1: What are the base and recursive cases?  
  - Follow-up 2: How would you implement DFS **iteratively**?

- Compare **DFS and BFS** in terms of exploration order and typical use-cases.  
  - Follow-up 1: When would DFS be preferable to BFS?  
  - Follow-up 2: When would BFS be preferable to DFS?

---

### Q9: Edge Types & Cycle Detection

- In the context of DFS on directed graphs, explain **tree, back, forward, and cross edges** conceptually.  
  - Follow-up 1: Which edge type indicates a **cycle** and why?  
  - Follow-up 2: How does the **recursion stack** help detect cycles?

- How would you use DFS to determine if a directed graph is **acyclic**?  
  - Follow-up 1: What state information do you maintain per node?  
  - Follow-up 2: How do you handle multiple components?

---

### Q10: Topological Sort – DFS Method

- Define a **topological ordering** of a DAG.  
  - Follow-up 1: Why can’t graphs with cycles have a topological order?  
  - Follow-up 2: Give an example where topo sort is useful in real systems.

- Explain how to compute a topological sort using **DFS post-order**.  
  - Follow-up 1: Why do you reverse the post-order list?  
  - Follow-up 2: What happens to your algorithm if the graph has a cycle?

---

### Q11: Topological Sort – Kahn’s Algorithm

- Describe **Kahn’s algorithm** for topological sorting.  
  - Follow-up 1: Why do you start with nodes of **in-degree 0**?  
  - Follow-up 2: How does the algorithm detect cycles?

- Compare DFS-based topological sort with Kahn’s algorithm.  
  - Follow-up 1: Which is easier to extend to **online detection** of missing dependencies?  
  - Follow-up 2: Which is easier to implement correctly under time pressure and why?

---

## 🔗 5. Connectivity & Bipartite Graphs (Day 4)

### Q12: Connected Components

- What is a **connected component** in an undirected graph?  
  - Follow-up 1: How would you count the number of components using BFS or DFS?  
  - Follow-up 2: How would you label each node with its component ID?

- Give a real-world example where connected components are meaningful.  
  - Follow-up 1: How would you interpret isolated components in a **social network**?  
  - Follow-up 2: What would a giant component signify in a **communication network**?

---

### Q13: Bipartite Graphs & 2-Coloring

- Define a **bipartite graph**.  
  - Follow-up 1: Explain why **odd-length cycles** break bipartiteness.  
  - Follow-up 2: Are all trees bipartite? Why or why not?

- How would you check if a graph is bipartite using **BFS**?  
  - Follow-up 1: What information do you store per node?  
  - Follow-up 2: How do you handle multiple components?

---

### Q14: Applications of Connectivity & Bipartiteness

- Describe a problem where checking **bipartiteness** helps solve a constraint issue (e.g., seating arrangement).  
  - Follow-up 1: How would you translate constraints into a graph?  
  - Follow-up 2: What does a “not bipartite” result tell you about the original problem?

- How might **Union–Find (Disjoint Set Union)** be used for connectivity queries?  
  - Follow-up 1: Compare its strengths and weaknesses to BFS/DFS.  
  - Follow-up 2: In what scenarios would DSU be the best choice?

---

## ♻️ 6. Strongly Connected Components (SCC) (Day 5 – Advanced)

### Q15: SCC Definitions & Intuition

- Define a **strongly connected component** in a directed graph.  
  - Follow-up 1: How is this different from a connected component in an undirected graph?  
  - Follow-up 2: Give an example of a strongly connected subgraph in a real system.

- What does it mean to **collapse SCCs into a DAG**?  
  - Follow-up 1: Why is the resulting graph always acyclic?  
  - Follow-up 2: How does this simplification help in algorithm design?

---

### Q16: Kosaraju & Tarjan – High-Level Algorithms

- Outline the **two main steps of Kosaraju’s algorithm**.  
  - Follow-up 1: Why do we need the **transpose** graph?  
  - Follow-up 2: Why do we process nodes in **decreasing finish-time order**?

- High-level: how does **Tarjan’s algorithm** use low-link values to find SCCs?  
  - Follow-up 1: What is a **low-link value**?  
  - Follow-up 2: What role does the **stack** play in Tarjan’s algorithm?

---

## 🧮 7. Complexity & Trade-Off Questions

### Q17: Complexity of Traversals

- State the **time and space complexity** of BFS on a graph represented with adjacency lists.  
  - Follow-up 1: How does this change with adjacency matrices?  
  - Follow-up 2: What does `deg(u)` mean in complexity discussions?

- State the **time and space complexity** of DFS.  
  - Follow-up 1: How do recursion depth and stack usage affect practical limits?  
  - Follow-up 2: When would you prefer iterative DFS over recursive DFS?

---

### Q18: Representation Trade-Offs

- For which types of problems would you choose an **adjacency matrix**, even if it consumes more memory?  
  - Follow-up 1: How does graph **density** factor into this choice?  
  - Follow-up 2: How does this relate to algorithms like **Floyd–Warshall**?

- Discuss the trade-offs between **storing a graph explicitly** vs **generating neighbors on the fly** (implicit graph).  
  - Follow-up 1: When might the implicit approach be infeasible?  
  - Follow-up 2: How does this choice impact memory vs CPU usage?

---

## 🧪 8. Scenario & System Design Questions

### Q19: Task Scheduling System

- You are designing a **build system** where some tasks depend on others. How would you ensure tasks execute in a valid order?  
  - Follow-up 1: How do you model tasks and dependencies as a graph?  
  - Follow-up 2: How do you detect and report cycles in dependencies?

- Suppose your build system supports **incremental builds** when only a subset of files change.  
  - Follow-up 1: How does the dependency graph help minimize rebuilt tasks?  
  - Follow-up 2: What data structures or algorithms would you adjust for this use-case?

---

### Q20: Social Network Analytics

- How could you use graph algorithms to find **influential users** in a social network?  
  - Follow-up 1: Which Week 08 concepts would form a basis for more advanced centrality algorithms?  
  - Follow-up 2: How might connected components or SCCs be relevant?

- How would you detect if a social network can be split into **two groups** such that all friendships are across groups (no intra-group friendships)?  
  - Follow-up 1: Which algorithm would you use?  
  - Follow-up 2: What does a failure to split say about the network structure?

---

## 🧑‍🏫 9. Usage Tips – Mock Interview Strategy

- **Round 1 – Warm-Up (20–30 mins):**
  - Pick 2–3 questions from each main section (Graphs, BFS, DFS/Topo, Connectivity, SCC).  
  - Focus on high-level explanations and definitions.

- **Round 2 – Deep Dive (40–60 mins):**
  - For each chosen question, answer **all follow-ups**.  
  - Draw diagrams and walk through examples explicitly.

- **Round 3 – Whiteboard Simulation (45–60 mins):**
  - Ask a friend to play interviewer using this file.  
  - Limit answers to **2–3 minutes per question**.  
  - Practice pausing to ask clarifying questions and to structure your answer.

- **Round 4 – Self-Recording:**
  - Record yourself answering 5–10 questions.  
  - Evaluate: did you explain **why**, not just **what**? Did you connect to real-world systems and performance?

Revisit this file periodically even after Week 08; these graph questions will reappear in many guises throughout your interview prep.
