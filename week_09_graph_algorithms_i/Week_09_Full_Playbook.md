# WEEK 09 FULL PLAYBOOK

## Shortest Paths, MST, and Union-Find

Week 09 is the bridge from graph traversal to graph optimization.

Core outcomes:
- understand when shortest-path algorithms differ by edge constraints
- know when MST solves a connectivity-cost problem instead of a path problem
- use Union-Find as the supporting structure for connectivity and Kruskal-style greedy design

---

## Daily path

### Day 1: Dijkstra
- single-source shortest paths
- non-negative weights only
- priority-queue frontier
- stale-entry skipping

### Day 2: Bellman-Ford
- negative edge handling
- relaxation over all edges
- negative-cycle detection

### Day 3: Floyd-Warshall
- all-pairs shortest paths
- DP over intermediate vertices
- dense-graph use case

### Day 4: Kruskal and Prim
- MST as minimum-cost connectivity
- cut property and greedy safety
- edge-centric vs vertex-centric MST growth

### Day 5: Union-Find
- path compression
- union by rank/size
- near-constant amortized set operations

---

## Pattern map

| Problem signal | Pattern |
|---|---|
| "shortest path from one source" + non-negative weights | Dijkstra |
| "negative edges" | Bellman-Ford |
| "all pairs" | Floyd-Warshall |
| "connect all nodes with minimum total cost" | MST |
| "avoid cycles while adding cheapest edges" | Kruskal + DSU |
| "connectivity under repeated unions" | Union-Find |

---

## Common confusions

- BFS is not for weighted shortest paths.
- MST is not the same as shortest path.
- Dijkstra fails with negative weights.
- Floyd-Warshall is elegant but too expensive for large sparse graphs.
- Union-Find answers connectivity structure questions, not path-length questions.

---

## Study order inside this folder

1. `Week_09_Day_01_Dijkstra_Single_Source_Shortest_Paths_Instructional.md`
2. `Week_09_Day_02_Bellman_Ford_Negative_Weights_Instructional.md`
3. `Week_09_Day_03_Floyd_Warshall_All_Pairs_Shortest_Paths_Instructional.md`
4. `Week_09_Day_04_Minimum_Spanning_Trees_Kruskal_Prim_Instructional.md`
5. `Week_09_Day_05_Union_Find_DSU_In_Depth_Instructional.md`
6. `Week_09_Extended_CSharp_Complete_v13.md`
7. `Week_09_Extended_Python_Complete_v13.md`
8. `Week_09_Visual_Concepts_Playbook_HYBRID.md`
9. `support files/`
10. `WEEK_09_START_HERE.md` for orientation and pacing

---

## Interview checklist

- Can I distinguish Dijkstra vs Bellman-Ford in one sentence?
- Can I explain why MST is about total connectivity cost, not source-target distance?
- Can I write the Union-Find invariants clearly?
- Can I justify which graph representation I picked?
- Can I articulate time and space complexity for all five core topics?
