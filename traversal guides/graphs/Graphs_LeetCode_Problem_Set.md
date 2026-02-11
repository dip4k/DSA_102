# 📚 Graphs — LeetCode Problem Set (Traversal → Advanced)

Use this set after you finish the drills. It’s organized by skill level/pattern.

---

## Level 1: Representation + basic traversal
- 133 Clone Graph
- 997 Find the Town Judge (graph degree thinking)

## Level 2: BFS shortest paths (unweighted)
- 1091 Shortest Path in Binary Matrix
- 127 Word Ladder
- 994 Rotting Oranges (multi-source BFS)
- 542 01 Matrix (multi-source BFS)

## Level 3: DFS flood fill + grid graphs
- 200 Number of Islands
- 695 Max Area of Island
- 733 Flood Fill
- 130 Surrounded Regions
- 417 Pacific Atlantic Water Flow

## Level 4: Components, cycles, bipartite
- 547 Number of Provinces (components)
- 785 Is Graph Bipartite?
- 684 Redundant Connection (cycle/DSU)
- 802 Find Eventual Safe States (directed graph reasoning)

## Level 5: Topological sorting (DAG)
- 207 Course Schedule
- 210 Course Schedule II

## Level 6: Shortest paths (weighted)
- 743 Network Delay Time (Dijkstra)
- 1631 Path With Minimum Effort (Dijkstra-style on grid)
- 1976 Number of Ways to Arrive at Destination (Dijkstra + counts)

## Level 7: MST / DSU / bridges
- 1584 Min Cost to Connect All Points (MST)
- 1135 Connecting Cities With Minimum Cost (MST)
- 1192 Critical Connections in a Network (bridges)

## Level 8: Advanced decompositions
- 399 Evaluate Division (graph + DFS/BFS with weights)
- 310 Minimum Height Trees (topology + trimming queue)

---

## Notes
- For shortest path: always confirm edge weights before picking BFS vs 0–1 BFS vs Dijkstra.
- For deterministic outputs in practice: sort adjacency lists and define tie-break rules.
