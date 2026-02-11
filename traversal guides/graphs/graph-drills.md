# 🕸️ Graph Traversal Drills Pack (v1)

This is a **drills-only** pack (no solutions) containing **32 traversal drills**.

- Each drill includes a **function stub** (TODO) and a **driver with assertions**.
- Languages included: **Python** and **C#**.
- The graphs used in tests are intentionally small and have **deterministic neighbor order** to make traversal outputs testable.

---

## How to use

1. Pick one drill at a time.
2. Implement ONLY that function.
3. Run the driver; all assertions should pass.
4. Move to the next drill.

---

# Part A — Python (Drills + Driver)

> Requirement assumptions for deterministic tests:
> - **BFS**: mark `visited` when **enqueuing**.
> - **DFS (recursive)**: iterate neighbors **in the given adjacency list order**.
> - **DFS (iterative)**: to match recursive order, push neighbors in **reverse order**.

```python
from __future__ import annotations

from collections import deque
from typing import Deque, Dict, List, Optional, Set, Tuple


# -------------------------
# Helpers (use in drills)
# -------------------------

def build_adj_undirected(n: int, edges: List[Tuple[int, int]]) -> Dict[int, List[int]]:
    """Build an undirected adjacency list.

    Requirements for this pack:
    - Nodes are 0..n-1
    - Neighbor order must follow the order edges are added.
    """
    # TODO
    raise NotImplementedError


def build_adj_directed(n: int, edges: List[Tuple[int, int]]) -> Dict[int, List[int]]:
    """Build a directed adjacency list."""
    # TODO
    raise NotImplementedError


def grid_in_bounds(r: int, c: int, R: int, C: int) -> bool:
    return 0 <= r < R and 0 <= c < C


# -------------------------
# Drill 01 — Build undirected adjacency list
# -------------------------

def drill01_build_adj_undirected(n: int, edges: List[Tuple[int, int]]) -> Dict[int, List[int]]:
    # TODO: call build_adj_undirected or implement directly
    raise NotImplementedError


# -------------------------
# Drill 02 — Build directed adjacency list
# -------------------------

def drill02_build_adj_directed(n: int, edges: List[Tuple[int, int]]) -> Dict[int, List[int]]:
    # TODO
    raise NotImplementedError


# -------------------------
# Drill 03 — BFS order (deterministic)
# -------------------------

def drill03_bfs_order(adj: Dict[int, List[int]], start: int) -> List[int]:
    """Return BFS visit order from start."""
    # TODO
    raise NotImplementedError


# -------------------------
# Drill 04 — DFS recursive order (deterministic)
# -------------------------

def drill04_dfs_recursive_order(adj: Dict[int, List[int]], start: int) -> List[int]:
    """Return DFS recursive preorder visit order from start."""
    # TODO
    raise NotImplementedError


# -------------------------
# Drill 05 — DFS iterative order (match recursive order)
# -------------------------

def drill05_dfs_iterative_order(adj: Dict[int, List[int]], start: int) -> List[int]:
    """Return DFS visit order using an explicit stack.

    To match Drill 04's order, push neighbors in reverse.
    """
    # TODO
    raise NotImplementedError


# -------------------------
# Drill 06 — Reachable set from a source
# -------------------------

def drill06_reachable_set(adj: Dict[int, List[int]], start: int) -> Set[int]:
    # TODO
    raise NotImplementedError


# -------------------------
# Drill 07 — Path exists (undirected) between u and v
# -------------------------

def drill07_path_exists(adj: Dict[int, List[int]], u: int, v: int) -> bool:
    # TODO
    raise NotImplementedError


# -------------------------
# Drill 08 — Count connected components (undirected)
# -------------------------

def drill08_count_components(adj: Dict[int, List[int]]) -> int:
    # TODO
    raise NotImplementedError


# -------------------------
# Drill 09 — Largest component size (undirected)
# -------------------------

def drill09_largest_component_size(adj: Dict[int, List[int]]) -> int:
    # TODO
    raise NotImplementedError


# -------------------------
# Drill 10 — Is graph connected? (undirected)
# -------------------------

def drill10_is_connected(adj: Dict[int, List[int]]) -> bool:
    # TODO
    raise NotImplementedError


# -------------------------
# Drill 11 — Shortest path length in unweighted graph (BFS)
# -------------------------

def drill11_shortest_path_length(adj: Dict[int, List[int]], start: int, target: int) -> int:
    """Return number of edges in shortest path, or -1 if unreachable."""
    # TODO
    raise NotImplementedError


# -------------------------
# Drill 12 — Reconstruct one shortest path (BFS parent)
# -------------------------

def drill12_reconstruct_shortest_path(adj: Dict[int, List[int]], start: int, target: int) -> List[int]:
    """Return one shortest path as node list [start..target], or [] if unreachable."""
    # TODO
    raise NotImplementedError


# -------------------------
# Drill 13 — BFS distances array (unweighted)
# -------------------------

def drill13_bfs_distances(adj: Dict[int, List[int]], start: int) -> Dict[int, int]:
    """Return dist dict for all nodes, unreachable => -1."""
    # TODO
    raise NotImplementedError


# -------------------------
# Drill 14 — Nodes at exact distance K (unweighted)
# -------------------------

def drill14_nodes_at_distance_k(adj: Dict[int, List[int]], start: int, k: int) -> Set[int]:
    # TODO
    raise NotImplementedError


# -------------------------
# Drill 15 — Count number of shortest paths (unweighted)
# -------------------------

def drill15_count_shortest_paths(adj: Dict[int, List[int]], start: int, target: int) -> int:
    """Return count of distinct shortest paths from start to target."""
    # TODO
    raise NotImplementedError


# -------------------------
# Drill 16 — Bipartite check (BFS/DFS coloring)
# -------------------------

def drill16_is_bipartite(adj: Dict[int, List[int]]) -> bool:
    # TODO
    raise NotImplementedError


# -------------------------
# Drill 17 — Detect cycle in undirected graph
# -------------------------

def drill17_has_cycle_undirected(adj: Dict[int, List[int]]) -> bool:
    # TODO
    raise NotImplementedError


# -------------------------
# Drill 18 — Detect cycle in directed graph (3-color DFS)
# -------------------------

def drill18_has_cycle_directed(adj: Dict[int, List[int]]) -> bool:
    # TODO
    raise NotImplementedError


# -------------------------
# Drill 19 — Topological sort (Kahn) for a DAG (unique order in test)
# -------------------------

def drill19_topo_sort_kahn(n: int, edges: List[Tuple[int, int]]) -> List[int]:
    """Return topo ordering, or [] if cycle."""
    # TODO
    raise NotImplementedError


# -------------------------
# Drill 20 — SCC count (Kosaraju or Tarjan)
# -------------------------

def drill20_scc_count(n: int, edges: List[Tuple[int, int]]) -> int:
    """Return number of strongly connected components."""
    # TODO
    raise NotImplementedError


# -------------------------
# Grid Drills (implicit graphs)
# -------------------------

# -------------------------
# Drill 21 — Number of islands (grid DFS/BFS)
# -------------------------

def drill21_num_islands(grid: List[List[str]]) -> int:
    # TODO
    raise NotImplementedError


# -------------------------
# Drill 22 — Max area of island
# -------------------------

def drill22_max_area_island(grid: List[List[int]]) -> int:
    # TODO
    raise NotImplementedError


# -------------------------
# Drill 23 — Flood fill
# -------------------------

def drill23_flood_fill(image: List[List[int]], sr: int, sc: int, new_color: int) -> List[List[int]]:
    # TODO
    raise NotImplementedError


# -------------------------
# Drill 24 — 01 Matrix (multi-source BFS distances)
# -------------------------

def drill24_update_matrix(mat: List[List[int]]) -> List[List[int]]:
    # TODO
    raise NotImplementedError


# -------------------------
# Drill 25 — Rotting Oranges (multi-source BFS minutes)
# -------------------------

def drill25_rotting_oranges(grid: List[List[int]]) -> int:
    # TODO
    raise NotImplementedError


# -------------------------
# Drill 26 — Number of enclaves
# -------------------------

def drill26_num_enclaves(grid: List[List[int]]) -> int:
    # TODO
    raise NotImplementedError


# -------------------------
# Drill 27 — Shortest path in binary matrix (8 directions)
# -------------------------

def drill27_shortest_path_binary_matrix(grid: List[List[int]]) -> int:
    # TODO
    raise NotImplementedError


# -------------------------
# Drill 28 — Word Ladder length (BFS on implicit word graph)
# -------------------------

def drill28_word_ladder_length(begin_word: str, end_word: str, word_list: List[str]) -> int:
    # TODO
    raise NotImplementedError


# -------------------------
# Drill 29 — 0-1 BFS on a 0/1 weighted graph (min cost)
# -------------------------

def drill29_zero_one_bfs_min_cost(n: int, edges: List[Tuple[int, int, int]], start: int, target: int) -> int:
    """Edges are (u, v, w) with w in {0,1}. Graph is undirected for this drill."""
    # TODO
    raise NotImplementedError


# -------------------------
# Drill 30 — Validate tree (connected + acyclic)
# -------------------------

def drill30_is_valid_tree(n: int, edges: List[Tuple[int, int]]) -> bool:
    # TODO
    raise NotImplementedError


# -------------------------
# Drill 31 — Topological sort (DFS finish order) on unique DAG
# -------------------------

def drill31_topo_sort_dfs(n: int, edges: List[Tuple[int, int]]) -> List[int]:
    """Return topo ordering, or [] if cycle."""
    # TODO
    raise NotImplementedError


# -------------------------
# Drill 32 — Directed reachability (DFS/BFS)
# -------------------------

def drill32_directed_reachable(adj: Dict[int, List[int]], start: int, target: int) -> bool:
    # TODO
    raise NotImplementedError


# -------------------------
# Driver with assertions
# -------------------------

def _assert_eq(actual, expected, msg: str = ""):
    assert actual == expected, f"{msg}\nExpected: {expected}\nActual:   {actual}"


def run_all_tests():
    # Shared deterministic graph for traversal order drills
    # Undirected Graph A (neighbor order is important):
    # 0: [1,2]
    # 1: [0,3]
    # 2: [0,3]
    # 3: [1,2,4]
    # 4: [3]
    nA = 5
    edgesA = [(0,1), (0,2), (1,3), (2,3), (3,4)]

    # Drill 01
    adjA_expected = {0:[1,2], 1:[0,3], 2:[0,3], 3:[1,2,4], 4:[3]}
    adjA = drill01_build_adj_undirected(nA, edgesA)
    _assert_eq(adjA, adjA_expected, "Drill 01")

    # Drill 02
    nD = 4
    edgesD = [(0,1), (0,2), (2,3)]
    adjD_expected = {0:[1,2], 1:[], 2:[3], 3:[]}
    adjD = drill02_build_adj_directed(nD, edgesD)
    _assert_eq(adjD, adjD_expected, "Drill 02")

    # Drill 03
    bfs_order = drill03_bfs_order(adjA, 0)
    _assert_eq(bfs_order, [0,1,2,3,4], "Drill 03")

    # Drill 04
    dfs_rec = drill04_dfs_recursive_order(adjA, 0)
    _assert_eq(dfs_rec, [0,1,3,2,4], "Drill 04")

    # Drill 05
    dfs_it = drill05_dfs_iterative_order(adjA, 0)
    _assert_eq(dfs_it, [0,1,3,2,4], "Drill 05")

    # Drill 06
    reach = drill06_reachable_set(adjA, 0)
    _assert_eq(reach, {0,1,2,3,4}, "Drill 06")

    # Drill 07
    _assert_eq(drill07_path_exists(adjA, 0, 4), True, "Drill 07a")
    _assert_eq(drill07_path_exists(adjA, 4, 0), True, "Drill 07b")

    # Drill 08/09/10 on a disconnected graph
    # Graph B: 0-1-2 and 3-4
    nB = 5
    edgesB = [(0,1), (1,2), (3,4)]
    adjB = build_adj_undirected(nB, edgesB)  # helper allowed

    _assert_eq(drill08_count_components(adjB), 2, "Drill 08")
    _assert_eq(drill09_largest_component_size(adjB), 3, "Drill 09")
    _assert_eq(drill10_is_connected(adjB), False, "Drill 10")

    # Drill 11/12
    _assert_eq(drill11_shortest_path_length(adjA, 0, 4), 3, "Drill 11")
    _assert_eq(drill12_reconstruct_shortest_path(adjA, 0, 4), [0,1,3,4], "Drill 12")

    # Drill 13
    distA = drill13_bfs_distances(adjA, 0)
    _assert_eq(distA, {0:0, 1:1, 2:1, 3:2, 4:3}, "Drill 13")

    # Drill 14
    _assert_eq(drill14_nodes_at_distance_k(adjA, 0, 2), {3}, "Drill 14")

    # Drill 15 — count shortest paths in Graph A from 0 to 3
    # shortest length is 2: paths are 0-1-3 and 0-2-3 => 2 paths
    _assert_eq(drill15_count_shortest_paths(adjA, 0, 3), 2, "Drill 15")

    # Drill 16 bipartite true and false
    # Square cycle (bipartite)
    nSq = 4
    edgesSq = [(0,1), (1,2), (2,3), (3,0)]
    adjSq = build_adj_undirected(nSq, edgesSq)
    _assert_eq(drill16_is_bipartite(adjSq), True, "Drill 16a")

    # Triangle (not bipartite)
    nTri = 3
    edgesTri = [(0,1), (1,2), (2,0)]
    adjTri = build_adj_undirected(nTri, edgesTri)
    _assert_eq(drill16_is_bipartite(adjTri), False, "Drill 16b")

    # Drill 17 undirected cycle
    _assert_eq(drill17_has_cycle_undirected(adjTri), True, "Drill 17a")
    _assert_eq(drill17_has_cycle_undirected(adjA), True, "Drill 17b")
    # A tree
    nTree = 5
    edgesTree = [(0,1), (0,2), (0,3), (1,4)]
    adjTree = build_adj_undirected(nTree, edgesTree)
    _assert_eq(drill17_has_cycle_undirected(adjTree), False, "Drill 17c")

    # Drill 18 directed cycle
    nCyc = 3
    edgesCyc = [(0,1), (1,2), (2,0)]
    adjCyc = build_adj_directed(nCyc, edgesCyc)
    _assert_eq(drill18_has_cycle_directed(adjCyc), True, "Drill 18a")

    nDag = 4
    edgesDag = [(0,1), (1,2), (2,3)]
    adjDag = build_adj_directed(nDag, edgesDag)
    _assert_eq(drill18_has_cycle_directed(adjDag), False, "Drill 18b")

    # Drill 19 topo sort (unique)
    _assert_eq(drill19_topo_sort_kahn(4, edgesDag), [0,1,2,3], "Drill 19a")
    _assert_eq(drill19_topo_sort_kahn(3, edgesCyc), [], "Drill 19b")

    # Drill 20 SCC count
    # SCCs: {0,1,2}, {3,4}
    nS = 5
    edgesS = [(0,1), (1,2), (2,0), (2,3), (3,4), (4,3)]
    _assert_eq(drill20_scc_count(nS, edgesS), 2, "Drill 20")

    # Drill 21 islands
    grid_islands = [
        list("11000"),
        list("11000"),
        list("00100"),
        list("00011"),
    ]
    _assert_eq(drill21_num_islands(grid_islands), 3, "Drill 21")

    # Drill 22 max area
    grid_area = [
        [0,0,1,0,0],
        [0,1,1,1,0],
        [0,0,1,0,0],
        [1,1,0,0,0],
    ]
    _assert_eq(drill22_max_area_island(grid_area), 5, "Drill 22")

    # Drill 23 flood fill
    image = [
        [1,1,1],
        [1,1,0],
        [1,0,1],
    ]
    filled = drill23_flood_fill(image, 1, 1, 2)
    _assert_eq(filled, [[2,2,2],[2,2,0],[2,0,1]], "Drill 23")

    # Drill 24 01 matrix
    mat = [
        [0,0,0],
        [0,1,0],
        [1,1,1],
    ]
    _assert_eq(drill24_update_matrix(mat), [[0,0,0],[0,1,0],[1,2,1]], "Drill 24")

    # Drill 25 rotting oranges
    oranges = [
        [2,1,1],
        [1,1,0],
        [0,1,1],
    ]
    _assert_eq(drill25_rotting_oranges(oranges), 4, "Drill 25")

    # Drill 26 enclaves
    encl = [
        [0,0,0,0],
        [1,0,1,0],
        [0,1,1,0],
        [0,0,0,0],
    ]
    _assert_eq(drill26_num_enclaves(encl), 3, "Drill 26")

    # Drill 27 shortest path in binary matrix (8-dir)
    binmat = [
        [0,1],
        [1,0],
    ]
    _assert_eq(drill27_shortest_path_binary_matrix(binmat), 2, "Drill 27")

    # Drill 28 word ladder
    _assert_eq(
        drill28_word_ladder_length("hit", "cog", ["hot","dot","dog","lot","log","cog"]),
        5,
        "Drill 28"
    )

    # Drill 29 0-1 BFS min cost
    nW = 5
    edgesW = [
        (0,1,0),
        (0,2,1),
        (1,2,0),
        (1,3,1),
        (2,3,0),
        (3,4,1),
    ]
    _assert_eq(drill29_zero_one_bfs_min_cost(nW, edgesW, 0, 4), 1, "Drill 29")

    # Drill 30 validate tree
    _assert_eq(drill30_is_valid_tree(5, edgesTree), True, "Drill 30a")
    edgesInvalid = [(0,1), (1,2), (2,3), (1,3), (1,4)]
    _assert_eq(drill30_is_valid_tree(5, edgesInvalid), False, "Drill 30b")

    # Drill 31 topo sort via DFS
    _assert_eq(drill31_topo_sort_dfs(4, edgesDag), [0,1,2,3], "Drill 31")

    # Drill 32 directed reachability
    _assert_eq(drill32_directed_reachable(adjD, 0, 3), True, "Drill 32a")
    _assert_eq(drill32_directed_reachable(adjD, 1, 3), False, "Drill 32b")


if __name__ == "__main__":
    run_all_tests()
    print("All graph traversal drill assertions passed.")
```

---

# Part B — C# (Drills + Driver)

> Determinism assumptions are the same as Python.

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

public static class GraphTraversalDrills
{
    // -------------------------
    // Helpers
    // -------------------------
    public static Dictionary<int, List<int>> BuildAdjUndirected(int n, List<(int u, int v)> edges)
    {
        // TODO: build undirected adjacency list with neighbor order preserved
        throw new NotImplementedException();
    }

    public static Dictionary<int, List<int>> BuildAdjDirected(int n, List<(int u, int v)> edges)
    {
        // TODO
        throw new NotImplementedException();
    }

    private static void AssertEq<T>(T actual, T expected, string name)
    {
        if (!EqualityComparer<T>.Default.Equals(actual, expected))
            throw new Exception($"{name} failed. Expected: {expected} | Actual: {actual}");
    }

    private static void AssertSeqEq<T>(IList<T> actual, IList<T> expected, string name)
    {
        if (actual.Count != expected.Count)
            throw new Exception($"{name} failed. Length expected {expected.Count}, got {actual.Count}");
        for (int i = 0; i < actual.Count; i++)
        {
            if (!EqualityComparer<T>.Default.Equals(actual[i], expected[i]))
                throw new Exception($"{name} failed at index {i}. Expected {expected[i]}, got {actual[i]}");
        }
    }

    private static void AssertSetEq(HashSet<int> actual, HashSet<int> expected, string name)
    {
        if (!actual.SetEquals(expected))
            throw new Exception($"{name} failed. Expected: {{{string.Join(",", expected)}}} | Actual: {{{string.Join(",", actual)}}}");
    }

    private static void AssertMatrixEq(int[][] actual, int[][] expected, string name)
    {
        if (actual.Length != expected.Length)
            throw new Exception($"{name} failed. Row mismatch.");
        for (int r = 0; r < actual.Length; r++)
        {
            if (actual[r].Length != expected[r].Length)
                throw new Exception($"{name} failed. Col mismatch at row {r}.");
            for (int c = 0; c < actual[r].Length; c++)
            {
                if (actual[r][c] != expected[r][c])
                    throw new Exception($"{name} failed at ({r},{c}). Expected {expected[r][c]}, got {actual[r][c]}");
            }
        }
    }

    // -------------------------
    // Drill 01 — Build undirected adjacency list
    // -------------------------
    public static Dictionary<int, List<int>> Drill01BuildAdjUndirected(int n, List<(int u, int v)> edges)
    {
        // TODO
        throw new NotImplementedException();
    }

    // -------------------------
    // Drill 02 — Build directed adjacency list
    // -------------------------
    public static Dictionary<int, List<int>> Drill02BuildAdjDirected(int n, List<(int u, int v)> edges)
    {
        // TODO
        throw new NotImplementedException();
    }

    // Drill 03 — BFS order
    public static IList<int> Drill03BfsOrder(Dictionary<int, List<int>> adj, int start)
    {
        // TODO
        throw new NotImplementedException();
    }

    // Drill 04 — DFS recursive order
    public static IList<int> Drill04DfsRecursiveOrder(Dictionary<int, List<int>> adj, int start)
    {
        // TODO
        throw new NotImplementedException();
    }

    // Drill 05 — DFS iterative order (match recursive)
    public static IList<int> Drill05DfsIterativeOrder(Dictionary<int, List<int>> adj, int start)
    {
        // TODO
        throw new NotImplementedException();
    }

    // Drill 06 — Reachable set
    public static HashSet<int> Drill06ReachableSet(Dictionary<int, List<int>> adj, int start)
    {
        // TODO
        throw new NotImplementedException();
    }

    // Drill 07 — Path exists (undirected)
    public static bool Drill07PathExists(Dictionary<int, List<int>> adj, int u, int v)
    {
        // TODO
        throw new NotImplementedException();
    }

    // Drill 08 — Count components
    public static int Drill08CountComponents(Dictionary<int, List<int>> adj)
    {
        // TODO
        throw new NotImplementedException();
    }

    // Drill 09 — Largest component size
    public static int Drill09LargestComponentSize(Dictionary<int, List<int>> adj)
    {
        // TODO
        throw new NotImplementedException();
    }

    // Drill 10 — Is connected
    public static bool Drill10IsConnected(Dictionary<int, List<int>> adj)
    {
        // TODO
        throw new NotImplementedException();
    }

    // Drill 11 — Shortest path length (unweighted)
    public static int Drill11ShortestPathLength(Dictionary<int, List<int>> adj, int start, int target)
    {
        // TODO
        throw new NotImplementedException();
    }

    // Drill 12 — Reconstruct shortest path
    public static IList<int> Drill12ReconstructShortestPath(Dictionary<int, List<int>> adj, int start, int target)
    {
        // TODO
        throw new NotImplementedException();
    }

    // Drill 13 — BFS distances
    public static Dictionary<int, int> Drill13BfsDistances(Dictionary<int, List<int>> adj, int start)
    {
        // TODO
        throw new NotImplementedException();
    }

    // Drill 14 — Nodes at distance K
    public static HashSet<int> Drill14NodesAtDistanceK(Dictionary<int, List<int>> adj, int start, int k)
    {
        // TODO
        throw new NotImplementedException();
    }

    // Drill 15 — Count shortest paths
    public static int Drill15CountShortestPaths(Dictionary<int, List<int>> adj, int start, int target)
    {
        // TODO
        throw new NotImplementedException();
    }

    // Drill 16 — Is bipartite
    public static bool Drill16IsBipartite(Dictionary<int, List<int>> adj)
    {
        // TODO
        throw new NotImplementedException();
    }

    // Drill 17 — Cycle undirected
    public static bool Drill17HasCycleUndirected(Dictionary<int, List<int>> adj)
    {
        // TODO
        throw new NotImplementedException();
    }

    // Drill 18 — Cycle directed (3-color)
    public static bool Drill18HasCycleDirected(Dictionary<int, List<int>> adj)
    {
        // TODO
        throw new NotImplementedException();
    }

    // Drill 19 — Topo sort Kahn
    public static IList<int> Drill19TopoSortKahn(int n, List<(int u, int v)> edges)
    {
        // TODO
        throw new NotImplementedException();
    }

    // Drill 20 — SCC count
    public static int Drill20SccCount(int n, List<(int u, int v)> edges)
    {
        // TODO
        throw new NotImplementedException();
    }

    // Drill 21 — Num islands
    public static int Drill21NumIslands(char[][] grid)
    {
        // TODO
        throw new NotImplementedException();
    }

    // Drill 22 — Max area island
    public static int Drill22MaxAreaIsland(int[][] grid)
    {
        // TODO
        throw new NotImplementedException();
    }

    // Drill 23 — Flood fill
    public static int[][] Drill23FloodFill(int[][] image, int sr, int sc, int newColor)
    {
        // TODO
        throw new NotImplementedException();
    }

    // Drill 24 — 01 Matrix
    public static int[][] Drill24UpdateMatrix(int[][] mat)
    {
        // TODO
        throw new NotImplementedException();
    }

    // Drill 25 — Rotting oranges
    public static int Drill25RottingOranges(int[][] grid)
    {
        // TODO
        throw new NotImplementedException();
    }

    // Drill 26 — Enclaves
    public static int Drill26NumEnclaves(int[][] grid)
    {
        // TODO
        throw new NotImplementedException();
    }

    // Drill 27 — Shortest path binary matrix (8-dir)
    public static int Drill27ShortestPathBinaryMatrix(int[][] grid)
    {
        // TODO
        throw new NotImplementedException();
    }

    // Drill 28 — Word ladder length
    public static int Drill28WordLadderLength(string beginWord, string endWord, IList<string> wordList)
    {
        // TODO
        throw new NotImplementedException();
    }

    // Drill 29 — 0-1 BFS min cost
    public static int Drill29ZeroOneBfsMinCost(int n, List<(int u, int v, int w)> edges, int start, int target)
    {
        // TODO
        throw new NotImplementedException();
    }

    // Drill 30 — Validate tree
    public static bool Drill30IsValidTree(int n, List<(int u, int v)> edges)
    {
        // TODO
        throw new NotImplementedException();
    }

    // Drill 31 — Topo sort via DFS finish order
    public static IList<int> Drill31TopoSortDfs(int n, List<(int u, int v)> edges)
    {
        // TODO
        throw new NotImplementedException();
    }

    // Drill 32 — Directed reachability
    public static bool Drill32DirectedReachable(Dictionary<int, List<int>> adj, int start, int target)
    {
        // TODO
        throw new NotImplementedException();
    }


    // -------------------------
    // Driver
    // -------------------------
    public static void RunAllTests()
    {
        int nA = 5;
        var edgesA = new List<(int u, int v)> { (0,1), (0,2), (1,3), (2,3), (3,4) };

        var adjAExpected = new Dictionary<int, List<int>>
        {
            {0, new List<int>{1,2}},
            {1, new List<int>{0,3}},
            {2, new List<int>{0,3}},
            {3, new List<int>{1,2,4}},
            {4, new List<int>{3}},
        };

        var adjA = Drill01BuildAdjUndirected(nA, edgesA);
        // Basic structural check (keys + sequence equality)
        foreach (var kv in adjAExpected)
        {
            if (!adjA.ContainsKey(kv.Key)) throw new Exception("Drill 01 failed: missing key " + kv.Key);
            AssertSeqEq(adjA[kv.Key], kv.Value, "Drill 01 key " + kv.Key);
        }

        int nD = 4;
        var edgesD = new List<(int u, int v)> { (0,1), (0,2), (2,3) };
        var adjD = Drill02BuildAdjDirected(nD, edgesD);
        AssertSeqEq(adjD[0], new List<int>{1,2}, "Drill 02 - adj[0]");
        AssertSeqEq(adjD[1], new List<int>{}, "Drill 02 - adj[1]");
        AssertSeqEq(adjD[2], new List<int>{3}, "Drill 02 - adj[2]");
        AssertSeqEq(adjD[3], new List<int>{}, "Drill 02 - adj[3]");

        AssertSeqEq(Drill03BfsOrder(adjA, 0), new List<int>{0,1,2,3,4}, "Drill 03");
        AssertSeqEq(Drill04DfsRecursiveOrder(adjA, 0), new List<int>{0,1,3,2,4}, "Drill 04");
        AssertSeqEq(Drill05DfsIterativeOrder(adjA, 0), new List<int>{0,1,3,2,4}, "Drill 05");

        AssertSetEq(Drill06ReachableSet(adjA, 0), new HashSet<int>{0,1,2,3,4}, "Drill 06");
        AssertEq(Drill07PathExists(adjA, 0, 4), true, "Drill 07a");
        AssertEq(Drill07PathExists(adjA, 4, 0), true, "Drill 07b");

        int nB = 5;
        var edgesB = new List<(int u, int v)> { (0,1), (1,2), (3,4) };
        var adjB = BuildAdjUndirected(nB, edgesB);
        AssertEq(Drill08CountComponents(adjB), 2, "Drill 08");
        AssertEq(Drill09LargestComponentSize(adjB), 3, "Drill 09");
        AssertEq(Drill10IsConnected(adjB), false, "Drill 10");

        AssertEq(Drill11ShortestPathLength(adjA, 0, 4), 3, "Drill 11");
        AssertSeqEq(Drill12ReconstructShortestPath(adjA, 0, 4), new List<int>{0,1,3,4}, "Drill 12");

        var distA = Drill13BfsDistances(adjA, 0);
        AssertEq(distA[0], 0, "Drill 13a");
        AssertEq(distA[1], 1, "Drill 13b");
        AssertEq(distA[2], 1, "Drill 13c");
        AssertEq(distA[3], 2, "Drill 13d");
        AssertEq(distA[4], 3, "Drill 13e");

        AssertSetEq(Drill14NodesAtDistanceK(adjA, 0, 2), new HashSet<int>{3}, "Drill 14");
        AssertEq(Drill15CountShortestPaths(adjA, 0, 3), 2, "Drill 15");

        var edgesSq = new List<(int u, int v)> { (0,1), (1,2), (2,3), (3,0) };
        var adjSq = BuildAdjUndirected(4, edgesSq);
        AssertEq(Drill16IsBipartite(adjSq), true, "Drill 16a");

        var edgesTri = new List<(int u, int v)> { (0,1), (1,2), (2,0) };
        var adjTri = BuildAdjUndirected(3, edgesTri);
        AssertEq(Drill16IsBipartite(adjTri), false, "Drill 16b");

        AssertEq(Drill17HasCycleUndirected(adjTri), true, "Drill 17a");
        var edgesTree = new List<(int u, int v)> { (0,1), (0,2), (0,3), (1,4) };
        var adjTree = BuildAdjUndirected(5, edgesTree);
        AssertEq(Drill17HasCycleUndirected(adjTree), false, "Drill 17b");

        var edgesCyc = new List<(int u, int v)> { (0,1), (1,2), (2,0) };
        var adjCyc = BuildAdjDirected(3, edgesCyc);
        AssertEq(Drill18HasCycleDirected(adjCyc), true, "Drill 18a");

        var edgesDag = new List<(int u, int v)> { (0,1), (1,2), (2,3) };
        var adjDag = BuildAdjDirected(4, edgesDag);
        AssertEq(Drill18HasCycleDirected(adjDag), false, "Drill 18b");

        AssertSeqEq(Drill19TopoSortKahn(4, edgesDag), new List<int>{0,1,2,3}, "Drill 19a");
        AssertSeqEq(Drill19TopoSortKahn(3, edgesCyc), new List<int>{}, "Drill 19b");

        var edgesS = new List<(int u, int v)> { (0,1), (1,2), (2,0), (2,3), (3,4), (4,3) };
        AssertEq(Drill20SccCount(5, edgesS), 2, "Drill 20");

        char[][] islands = new char[][]
        {
            "11000".ToCharArray(),
            "11000".ToCharArray(),
            "00100".ToCharArray(),
            "00011".ToCharArray(),
        };
        AssertEq(Drill21NumIslands(islands), 3, "Drill 21");

        int[][] areaGrid = new int[][]
        {
            new []{0,0,1,0,0},
            new []{0,1,1,1,0},
            new []{0,0,1,0,0},
            new []{1,1,0,0,0},
        };
        AssertEq(Drill22MaxAreaIsland(areaGrid), 5, "Drill 22");

        int[][] image = new int[][]
        {
            new []{1,1,1},
            new []{1,1,0},
            new []{1,0,1},
        };
        int[][] filled = Drill23FloodFill(image, 1, 1, 2);
        AssertMatrixEq(filled, new int[][]
        {
            new []{2,2,2},
            new []{2,2,0},
            new []{2,0,1},
        }, "Drill 23");

        int[][] mat = new int[][]
        {
            new []{0,0,0},
            new []{0,1,0},
            new []{1,1,1},
        };
        AssertMatrixEq(Drill24UpdateMatrix(mat), new int[][]
        {
            new []{0,0,0},
            new []{0,1,0},
            new []{1,2,1},
        }, "Drill 24");

        int[][] oranges = new int[][]
        {
            new []{2,1,1},
            new []{1,1,0},
            new []{0,1,1},
        };
        AssertEq(Drill25RottingOranges(oranges), 4, "Drill 25");

        int[][] encl = new int[][]
        {
            new []{0,0,0,0},
            new []{1,0,1,0},
            new []{0,1,1,0},
            new []{0,0,0,0},
        };
        AssertEq(Drill26NumEnclaves(encl), 3, "Drill 26");

        int[][] bin = new int[][]
        {
            new []{0,1},
            new []{1,0},
        };
        AssertEq(Drill27ShortestPathBinaryMatrix(bin), 2, "Drill 27");

        AssertEq(Drill28WordLadderLength("hit", "cog", new List<string>{"hot","dot","dog","lot","log","cog"}), 5, "Drill 28");

        var edgesW = new List<(int u, int v, int w)>
        {
            (0,1,0),
            (0,2,1),
            (1,2,0),
            (1,3,1),
            (2,3,0),
            (3,4,1),
        };
        AssertEq(Drill29ZeroOneBfsMinCost(5, edgesW, 0, 4), 1, "Drill 29");

        AssertEq(Drill30IsValidTree(5, edgesTree), true, "Drill 30a");
        var edgesInvalid = new List<(int u, int v)> { (0,1), (1,2), (2,3), (1,3), (1,4) };
        AssertEq(Drill30IsValidTree(5, edgesInvalid), false, "Drill 30b");

        AssertSeqEq(Drill31TopoSortDfs(4, edgesDag), new List<int>{0,1,2,3}, "Drill 31");

        AssertEq(Drill32DirectedReachable(adjD, 0, 3), true, "Drill 32a");
        AssertEq(Drill32DirectedReachable(adjD, 1, 3), false, "Drill 32b");

        Console.WriteLine("All graph traversal drill assertions passed.");
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        GraphTraversalDrills.RunAllTests();
    }
}
```

---

## Notes on scope

These drills intentionally focus on **traversal mechanics** (frontier + visited + level snapshots + parent reconstruction + implicit graph boundaries). More advanced graph topics (Dijkstra, MST, DSU-heavy tasks) are excluded by design.
