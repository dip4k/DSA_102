# 🚀 Advanced Graphs Drill Pack (v1) — 25 drills

**Goal:** take you beyond traversal into **advanced shortest paths, matchings, and flows** while keeping the same Flow‑Wise discipline: frontier + metadata + invariant.  
Algorithms emphasized: **Bellman–Ford, Floyd–Warshall, A\*, Dinic max flow, Hopcroft–Karp matching**, plus advanced Dijkstra patterns.

**Run policy:** implement in **Python or C# first**, pass all tests, then port to the other language.  
**No solutions included** — only TODO stubs + expected outputs.

---

## Drill index (suggested order)

### Weighted shortest paths
- **A1** Bellman–Ford distances (detect negative cycle reachable)
- **A2** Bellman–Ford: shortest path with negative edges, return path
- **A3** Floyd–Warshall all‑pairs shortest paths
- **A4** Floyd–Warshall with path reconstruction (next matrix)
- **A5** Dijkstra: count #shortest paths (mod M)
- **A6** Dijkstra: lexicographically smallest shortest path
- **A7** Dijkstra on grid: min effort / min max edge (minimax path)
- **A8** Multi‑source Dijkstra

### Heuristic search
- **B1** A\* on grid (Manhattan heuristic) — shortest path length
- **B2** A\* return path (coordinate list)

### Flows + matchings
- **C1** Dinic: max flow value (directed)
- **C2** Dinic: min cut partition (S-side nodes)
- **C3** Max bipartite matching via Hopcroft–Karp
- **C4** Minimum vertex cover in bipartite graph (via Konig, using matching)

### Constraint graphs / advanced structure
- **D1** Detect negative cycle anywhere (in a component) using super-source trick
- **D2** Shortest path with exactly K edges (DP on edges)
- **D3** K-shortest distances to target in DAG (topo DP)
- **D4** 2‑SAT satisfiable (SCC)

### Connectivity + offline
- **E1** DSU offline connectivity with edge additions
- **E2** DSU: number of components after each union

### Extras (optional but great)
- **F1** Eulerian path in directed graph
- **F2** Eulerian path in undirected graph
- **F3** Minimum arborescence (Edmonds) — OPTIONAL (skip unless you want)

This pack includes **A1..A8, B1..B2, C1..C4, D1..D4, E1..E2, F1..F2** = **24 drills**, plus **X1** (stress harness) = **25**.

---

# ✅ Shared input conventions

## Graph formats
- Unweighted edges: `(u, v)`
- Weighted edges: `(u, v, w)`
- Directed unless explicitly stated.

## Determinism rules (for consistent expected outputs)
- Always iterate nodes in ascending order.
- Sort adjacency lists by neighbor id (then weight if needed).

---

# Option A: Python driver (runnable)

Create `advanced_drills.py` and run:
```bash
python advanced_drills.py
python advanced_drills.py A1
```

```python
import sys
from collections import deque
import heapq


def assert_eq(exp, act, msg=""):
    if exp != act:
        raise AssertionError(f"{msg}\nExpected: {exp}\nActual:   {act}")


def run(drill_id="ALL"):
    tests = {
        "A1": t_a1_bellman_ford_dist,
        "A2": t_a2_bellman_ford_path,
        "A3": t_a3_floyd_dist,
        "A4": t_a4_floyd_path,
        "A5": t_a5_dijkstra_count_paths,
        "A6": t_a6_dijkstra_lexi_path,
        "A7": t_a7_grid_minimax,
        "A8": t_a8_multi_source_dijkstra,
        "B1": t_b1_astar_len,
        "B2": t_b2_astar_path,
        "C1": t_c1_dinic_flow,
        "C2": t_c2_min_cut,
        "C3": t_c3_hopcroft_karp,
        "C4": t_c4_min_vertex_cover_bipartite,
        "D1": t_d1_neg_cycle_anywhere,
        "D2": t_d2_shortest_exact_k_edges,
        "D3": t_d3_dag_k_best_to_target,
        "D4": t_d4_two_sat,
        "E1": t_e1_dsu_offline,
        "E2": t_e2_components_after_unions,
        "F1": t_f1_euler_directed,
        "F2": t_f2_euler_undirected,
        "X1": t_x1_stress_small,
    }

    if drill_id == "ALL":
        for k in sorted(tests.keys()):
            tests[k]()
            print(f"✅ {k} passed")
        return

    if drill_id not in tests:
        raise ValueError("Unknown id. Use ALL or A1..X1")

    tests[drill_id]()
    print(f"✅ {drill_id} passed")


# ---------------------- Tests ----------------------


def t_a1_bellman_ford_dist():
    # Graph has a reachable negative cycle from src=0: 1->2->3->1 total -1.
    n = 4
    edges = [(0,1,1),(1,2,1),(2,3,1),(3,1,-4)]
    dist, has_neg = bellman_ford_dist(n, edges, src=0)
    assert_eq(True, has_neg, "A1 neg cycle")


def t_a2_bellman_ford_path():
    n = 5
    edges = [(0,1,4),(0,2,5),(1,2,-2),(2,3,3),(1,3,6),(3,4,1)]
    d, path = bellman_ford_path(n, edges, src=0, dst=4)
    assert_eq(6, d, "A2 dist")
    assert_eq([0,1,2,3,4], path, "A2 path")


def t_a3_floyd_dist():
    INF = 10**9
    mat = [
        [0, 3, INF, 7],
        [8, 0, 2, INF],
        [5, INF, 0, 1],
        [2, INF, INF, 0],
    ]
    dist = floyd_warshall(mat)
    assert_eq(6, dist[0][3], "A3 0->3")  # 0->1->2->3
    assert_eq(5, dist[3][2], "A3 3->2")  # 3->0->1->2


def t_a4_floyd_path():
    INF = 10**9
    mat = [
        [0, 3, INF, 7],
        [8, 0, 2, INF],
        [5, INF, 0, 1],
        [2, INF, INF, 0],
    ]
    dist, nxt = floyd_warshall_path(mat)
    path = fw_reconstruct_path(nxt, 0, 3)
    assert_eq([0,1,2,3], path, "A4 path 0->3")
    assert_eq(6, dist[0][3], "A4 dist 0->3")


def t_a5_dijkstra_count_paths():
    MOD = 10**9+7
    n = 5
    edges = [(0,1,1),(0,2,1),(1,3,1),(2,3,1),(3,4,1)]
    dist, ways = dijkstra_count_shortest_paths(n, edges, src=0, mod=MOD)
    assert_eq(3, dist[4], "A5 dist to 4")
    assert_eq(2, ways[4], "A5 ways to 4")


def t_a6_dijkstra_lexi_path():
    n = 6
    # Two shortest paths to 5 (length 3): 0-1-3-5 and 0-2-4-5
    # Lexicographically smaller path is [0,1,3,5]
    edges = [(0,1,1),(0,2,1),(1,3,1),(2,4,1),(3,5,1),(4,5,1)]
    d, path = dijkstra_lexi_shortest_path(n, edges, src=0, dst=5)
    assert_eq(3, d, "A6 dist")
    assert_eq([0,1,3,5], path, "A6 path")


def t_a7_grid_minimax():
    grid = [
        [1, 2, 2],
        [3, 8, 2],
        [5, 3, 5],
    ]
    # Classic minimax / minimum effort path -> expected 2
    assert_eq(2, grid_minimax_effort(grid), "A7")


def t_a8_multi_source_dijkstra():
    n = 6
    edges = [(0,3,4),(1,3,1),(2,4,2),(3,4,3),(4,5,1)]
    sources = [0,1,2]
    dist = multi_source_dijkstra(n, edges, sources)
    assert_eq([0,0,0,1,2,3], dist, "A8")


def t_b1_astar_len():
    grid = [
        [0,0,0,0],
        [1,1,0,1],
        [0,0,0,0],
        [0,1,1,0],
    ]
    start = (0,0)
    goal = (3,3)
    assert_eq(6, astar_shortest_path_len(grid, start, goal), "B1")


def t_b2_astar_path():
    grid = [
        [0,0,0],
        [1,1,0],
        [0,0,0],
    ]
    start = (0,0)
    goal = (2,2)
    path = astar_shortest_path(grid, start, goal)
    assert_eq((0,0), path[0], "B2 start")
    assert_eq((2,2), path[-1], "B2 goal")
    assert_eq(4, len(path)-1, "B2 length")


def t_c1_dinic_flow():
    n = 4
    # s=0, t=3
    edges = [
        (0,1,3),
        (0,2,2),
        (1,2,1),
        (1,3,2),
        (2,3,3),
    ]
    assert_eq(5, dinic_max_flow(n, edges, s=0, t=3), "C1")


def t_c2_min_cut():
    n = 4
    edges = [
        (0,1,3),
        (0,2,2),
        (1,3,2),
        (2,3,3),
    ]
    flow, side = dinic_min_cut_partition(n, edges, s=0, t=3)
    assert_eq(5, flow, "C2 flow")
    # With these capacities, after maxflow, s-side should include only {0}.
    assert_eq([0], side, "C2 side")


def t_c3_hopcroft_karp():
    # Left: 0..2, Right: 0..2
    # Matching size = 3
    L, R = 3, 3
    edges = [(0,0),(0,1),(1,1),(1,2),(2,0),(2,2)]
    msize, matchL = hopcroft_karp(L, R, edges)
    assert_eq(3, msize, "C3 size")
    assert_eq(3, sum(1 for x in matchL if x != -1), "C3 matchL filled")


def t_c4_min_vertex_cover_bipartite():
    # Same graph as C3, min vertex cover size should equal max matching size = 3.
    L, R = 3, 3
    edges = [(0,0),(0,1),(1,1),(1,2),(2,0),(2,2)]
    coverL, coverR = min_vertex_cover_bipartite(L, R, edges)
    assert_eq(3, len(coverL) + len(coverR), "C4 size")


def t_d1_neg_cycle_anywhere():
    n = 3
    edges = [(0,1,1),(1,2,-2),(2,1,-2)]
    assert_eq(True, has_negative_cycle_anywhere(n, edges), "D1")


def t_d2_shortest_exact_k_edges():
    n = 4
    edges = [(0,1,1),(1,2,1),(0,2,10),(2,3,1)]
    # Exactly k=3 edges: 0->1->2->3 cost 3
    assert_eq(3, shortest_path_exact_k_edges(n, edges, src=0, dst=3, k=3), "D2")


def t_d3_dag_k_best_to_target():
    n = 6
    edges = [(0,1,1),(0,2,1),(1,3,1),(2,3,2),(3,5,1),(1,4,5),(4,5,1)]
    # DAG; k best distances from 0 to 5
    got = dag_k_best_distances(n, edges, src=0, dst=5, k=3)
    assert_eq([3,4,7], got, "D3")


def t_d4_two_sat():
    # (x0 OR x1) AND (!x0 OR x1) AND (!x1 OR x0) is satisfiable.
    nvars = 2
    clauses = [(0, True, 1, True), (0, False, 1, True), (1, False, 0, True)]
    sat, assign = two_sat(nvars, clauses)
    assert_eq(True, sat, "D4 sat")
    assert_eq(2, len(assign), "D4 assign len")


def t_e1_dsu_offline():
    n = 4
    ops = [
        ("add",0,1),
        ("query",0,2),
        ("add",2,3),
        ("add",1,2),
        ("query",0,3),
    ]
    got = dsu_offline_connectivity(n, ops)
    assert_eq([False, True], got, "E1")


def t_e2_components_after_unions():
    n = 5
    unions = [(0,1),(1,2),(3,4),(2,3)]
    got = components_after_unions(n, unions)
    assert_eq([4,3,2,1], got, "E2")


def t_f1_euler_directed():
    n = 4
    edges = [(0,1),(1,2),(2,0),(0,3)]
    path = euler_path_directed(n, edges)
    assert_eq(len(edges)+1, len(path), "F1 length")


def t_f2_euler_undirected():
    n = 3
    edges = [(0,1),(1,2),(2,0)]
    path = euler_path_undirected(n, edges)
    assert_eq(len(edges)+1, len(path), "F2 length")


def t_x1_stress_small():
    # Minimal stress harness: just verify your functions don't crash on tiny inputs.
    # Keep it simple, you can expand later.
    n = 1
    assert_eq(([-1], False), bellman_ford_dist(n, [], 0), "X1 bellman")


# ---------------------- TODO stubs (NO SOLUTIONS) ----------------------

# A1
def bellman_ford_dist(n, edges, src):
    """Return (dist[], has_negative_cycle_reachable_from_src). Use INF for unreachable; tests accept any large INF."""
    raise NotImplementedError("TODO A1")

# A2
def bellman_ford_path(n, edges, src, dst):
    """Return (distance, path_nodes). If dst unreachable, return (-1, []). Assume no reachable negative cycle affecting dst."""
    raise NotImplementedError("TODO A2")

# A3
def floyd_warshall(mat):
    """mat is NxN; return dist NxN."""
    raise NotImplementedError("TODO A3")

# A4
def floyd_warshall_path(mat):
    """Return (dist, nxt) where nxt[i][j] gives next hop from i to j, or -1 if none."""
    raise NotImplementedError("TODO A4")

def fw_reconstruct_path(nxt, i, j):
    raise NotImplementedError("TODO A4b")

# A5
def dijkstra_count_shortest_paths(n, edges, src, mod):
    """Undirected weighted with w>0. Return (dist[], ways[])."""
    raise NotImplementedError("TODO A5")

# A6
def dijkstra_lexi_shortest_path(n, edges, src, dst):
    """Unweighted or weighted positive; return (dist, path) with lexicographically smallest path among shortest."""
    raise NotImplementedError("TODO A6")

# A7
def grid_minimax_effort(grid):
    """Return minimum possible maximum absolute diff along a path (minimax)."""
    raise NotImplementedError("TODO A7")

# A8
def multi_source_dijkstra(n, edges, sources):
    """Undirected nonnegative. Return dist[] to nearest source."""
    raise NotImplementedError("TODO A8")

# B1
def astar_shortest_path_len(grid, start, goal):
    raise NotImplementedError("TODO B1")

# B2
def astar_shortest_path(grid, start, goal):
    raise NotImplementedError("TODO B2")

# C1
def dinic_max_flow(n, edges, s, t):
    raise NotImplementedError("TODO C1")

# C2
def dinic_min_cut_partition(n, edges, s, t):
    """Return (maxflow, sorted_list_of_nodes_reachable_from_s_in_residual)."""
    raise NotImplementedError("TODO C2")

# C3
def hopcroft_karp(L, R, edges):
    """Return (matching_size, matchL) where matchL[u] is matched right node or -1."""
    raise NotImplementedError("TODO C3")

# C4
def min_vertex_cover_bipartite(L, R, edges):
    """Return (coverL, coverR) as sorted node indices on left/right."""
    raise NotImplementedError("TODO C4")

# D1
def has_negative_cycle_anywhere(n, edges):
    raise NotImplementedError("TODO D1")

# D2
def shortest_path_exact_k_edges(n, edges, src, dst, k):
    raise NotImplementedError("TODO D2")

# D3
def dag_k_best_distances(n, edges, src, dst, k):
    raise NotImplementedError("TODO D3")

# D4
def two_sat(nvars, clauses):
    """clauses: (a, a_is_pos, b, b_is_pos). Return (is_sat, assignment[bool])."""
    raise NotImplementedError("TODO D4")

# E1
def dsu_offline_connectivity(n, ops):
    raise NotImplementedError("TODO E1")

# E2
def components_after_unions(n, unions):
    raise NotImplementedError("TODO E2")

# F1
def euler_path_directed(n, edges):
    raise NotImplementedError("TODO F1")

# F2
def euler_path_undirected(n, edges):
    raise NotImplementedError("TODO F2")


if __name__ == "__main__":
    drill_id = "ALL" if len(sys.argv) == 1 else sys.argv[1].strip().upper()
    run(drill_id)
```

---

# Option B: C# driver (runnable)

Create a project:
```bash
dotnet new console -n AdvancedGraphDrills
cd AdvancedGraphDrills
```
Replace `Program.cs` with the code below.

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

public sealed class Program
{
    public static void Main(string[] args)
    {
        var id = args.Length == 0 ? "ALL" : args[0].Trim().ToUpperInvariant();
        new Runner().Run(id);
        Console.WriteLine("✅ Done");
    }
}

public static class AssertEx
{
    public static void Equal<T>(T expected, T actual, string msg) where T : IEquatable<T>
    {
        if (!expected.Equals(actual))
            throw new Exception(msg + $"\nExpected: {expected}\nActual:   {actual}");
    }

    public static void SeqEqual<T>(IList<T> expected, IList<T> actual, string msg)
    {
        if (expected.Count != actual.Count)
            throw new Exception(msg + $"\nExpected: [{string.Join(",", expected)}]\nActual:   [{string.Join(",", actual)}]");
        for (int i = 0; i < expected.Count; i++)
            if (!EqualityComparer<T>.Default.Equals(expected[i], actual[i]))
                throw new Exception(msg + $"\nMismatch i={i} expected={expected[i]} actual={actual[i]}\nExpected: [{string.Join(",", expected)}]\nActual:   [{string.Join(",", actual)}]");
    }
}

public sealed class Runner
{
    public void Run(string id)
    {
        var tests = new Dictionary<string, Action>(StringComparer.OrdinalIgnoreCase)
        {
            ["A1"] = T_A1,
            ["A2"] = T_A2,
            ["A3"] = T_A3,
            ["A4"] = T_A4,
            ["A5"] = T_A5,
            ["A6"] = T_A6,
            ["A7"] = T_A7,
            ["A8"] = T_A8,
            ["B1"] = T_B1,
            ["B2"] = T_B2,
            ["C1"] = T_C1,
            ["C2"] = T_C2,
            ["C3"] = T_C3,
            ["C4"] = T_C4,
            ["D1"] = T_D1,
            ["D2"] = T_D2,
            ["D3"] = T_D3,
            ["D4"] = T_D4,
            ["E1"] = T_E1,
            ["E2"] = T_E2,
            ["F1"] = T_F1,
            ["F2"] = T_F2,
            ["X1"] = T_X1,
        };

        if (id == "ALL")
        {
            foreach (var kv in tests.OrderBy(k => k.Key))
            {
                kv.Value();
                Console.WriteLine($"✅ {kv.Key} passed");
            }
            return;
        }

        if (!tests.TryGetValue(id, out var one))
            throw new Exception("Unknown id. Use ALL or A1..X1");

        one();
        Console.WriteLine($"✅ {id} passed");
    }

    private static void T_A1()
    {
        int n = 4;
        var edges = new (int u,int v,int w)[] { (0,1,1),(1,2,1),(2,3,1),(3,1,-4) };
        var (dist, hasNeg) = Drills.BellmanFordDist(n, edges, 0);
        AssertEx.Equal(true, hasNeg, "A1 neg");
    }

    private static void T_A2()
    {
        int n = 5;
        var edges = new (int u,int v,int w)[] { (0,1,4),(0,2,5),(1,2,-2),(2,3,3),(1,3,6),(3,4,1) };
        var (d, path) = Drills.BellmanFordPath(n, edges, 0, 4);
        AssertEx.Equal(6, d, "A2 dist");
        AssertEx.SeqEqual(new List<int>{0,1,2,3,4}, path, "A2 path");
    }

    private static void T_A3()
    {
        int INF = 1_000_000_000;
        int[][] mat = {
            new []{0,3,INF,7},
            new []{8,0,2,INF},
            new []{5,INF,0,1},
            new []{2,INF,INF,0},
        };
        var dist = Drills.FloydWarshall(mat);
        AssertEx.Equal(6, dist[0][3], "A3 0->3");
        AssertEx.Equal(5, dist[3][2], "A3 3->2");
    }

    private static void T_A4()
    {
        int INF = 1_000_000_000;
        int[][] mat = {
            new []{0,3,INF,7},
            new []{8,0,2,INF},
            new []{5,INF,0,1},
            new []{2,INF,INF,0},
        };
        var (dist, nxt) = Drills.FloydWarshallPath(mat);
        var path = Drills.FwReconstructPath(nxt, 0, 3);
        AssertEx.SeqEqual(new List<int>{0,1,2,3}, path, "A4 path");
        AssertEx.Equal(6, dist[0][3], "A4 dist");
    }

    private static void T_A5()
    {
        int MOD = 1_000_000_007;
        int n = 5;
        var edges = new (int u,int v,int w)[] { (0,1,1),(0,2,1),(1,3,1),(2,3,1),(3,4,1) };
        var (dist, ways) = Drills.DijkstraCountShortestPaths(n, edges, 0, MOD);
        AssertEx.Equal(3, dist[4], "A5 dist");
        AssertEx.Equal(2, ways[4], "A5 ways");
    }

    private static void T_A6()
    {
        int n = 6;
        var edges = new (int u,int v,int w)[] { (0,1,1),(0,2,1),(1,3,1),(2,4,1),(3,5,1),(4,5,1) };
        var (d, path) = Drills.DijkstraLexiShortestPath(n, edges, 0, 5);
        AssertEx.Equal(3, d, "A6 dist");
        AssertEx.SeqEqual(new List<int>{0,1,3,5}, path, "A6 path");
    }

    private static void T_A7()
    {
        int[][] grid = {
            new []{1,2,2},
            new []{3,8,2},
            new []{5,3,5},
        };
        AssertEx.Equal(2, Drills.GridMinimaxEffort(grid), "A7");
    }

    private static void T_A8()
    {
        int n = 6;
        var edges = new (int u,int v,int w)[] { (0,3,4),(1,3,1),(2,4,2),(3,4,3),(4,5,1) };
        var dist = Drills.MultiSourceDijkstra(n, edges, new []{0,1,2});
        AssertEx.SeqEqual(new List<int>{0,0,0,1,2,3}, dist, "A8");
    }

    private static void T_B1()
    {
        int[][] grid = {
            new []{0,0,0,0},
            new []{1,1,0,1},
            new []{0,0,0,0},
            new []{0,1,1,0},
        };
        AssertEx.Equal(6, Drills.AStarShortestPathLen(grid, (0,0), (3,3)), "B1");
    }

    private static void T_B2()
    {
        int[][] grid = {
            new []{0,0,0},
            new []{1,1,0},
            new []{0,0,0},
        };
        var path = Drills.AStarShortestPath(grid, (0,0), (2,2));
        AssertEx.Equal((0,0), path[0], "B2 start");
        AssertEx.Equal((2,2), path[^1], "B2 goal");
        AssertEx.Equal(4, path.Count - 1, "B2 len");
    }

    private static void T_C1()
    {
        int n = 4;
        var edges = new (int u,int v,int cap)[] { (0,1,3),(0,2,2),(1,2,1),(1,3,2),(2,3,3) };
        AssertEx.Equal(5, Drills.DinicMaxFlow(n, edges, 0, 3), "C1");
    }

    private static void T_C2()
    {
        int n = 4;
        var edges = new (int u,int v,int cap)[] { (0,1,3),(0,2,2),(1,3,2),(2,3,3) };
        var (flow, side) = Drills.DinicMinCutPartition(n, edges, 0, 3);
        AssertEx.Equal(5, flow, "C2 flow");
        AssertEx.SeqEqual(new List<int>{0}, side, "C2 side");
    }

    private static void T_C3()
    {
        int L = 3, R = 3;
        var edges = new (int u,int v)[] { (0,0),(0,1),(1,1),(1,2),(2,0),(2,2) };
        var (msize, matchL) = Drills.HopcroftKarp(L, R, edges);
        AssertEx.Equal(3, msize, "C3 size");
        AssertEx.Equal(3, matchL.Count(x => x != -1), "C3 filled");
    }

    private static void T_C4()
    {
        int L = 3, R = 3;
        var edges = new (int u,int v)[] { (0,0),(0,1),(1,1),(1,2),(2,0),(2,2) };
        var (coverL, coverR) = Drills.MinVertexCoverBipartite(L, R, edges);
        AssertEx.Equal(3, coverL.Count + coverR.Count, "C4 size");
    }

    private static void T_D1()
    {
        int n = 3;
        var edges = new (int u,int v,int w)[] { (0,1,1),(1,2,-2),(2,1,-2) };
        AssertEx.Equal(true, Drills.HasNegativeCycleAnywhere(n, edges), "D1");
    }

    private static void T_D2()
    {
        int n = 4;
        var edges = new (int u,int v,int w)[] { (0,1,1),(1,2,1),(0,2,10),(2,3,1) };
        AssertEx.Equal(3, Drills.ShortestPathExactKEdges(n, edges, 0, 3, 3), "D2");
    }

    private static void T_D3()
    {
        int n = 6;
        var edges = new (int u,int v,int w)[] { (0,1,1),(0,2,1),(1,3,1),(2,3,2),(3,5,1),(1,4,5),(4,5,1) };
        var got = Drills.DagKBestDistances(n, edges, 0, 5, 3);
        AssertEx.SeqEqual(new List<int>{3,4,7}, got, "D3");
    }

    private static void T_D4()
    {
        int nvars = 2;
        var clauses = new (int a, bool aPos, int b, bool bPos)[]
        {
            (0,true, 1,true),
            (0,false,1,true),
            (1,false,0,true),
        };
        var (sat, assign) = Drills.TwoSat(nvars, clauses);
        AssertEx.Equal(true, sat, "D4 sat");
        AssertEx.Equal(2, assign.Count, "D4 len");
    }

    private static void T_E1()
    {
        int n = 4;
        var ops = new (string op,int a,int b)[]
        {
            ("add",0,1),
            ("query",0,2),
            ("add",2,3),
            ("add",1,2),
            ("query",0,3),
        };
        var got = Drills.DsuOfflineConnectivity(n, ops);
        AssertEx.SeqEqual(new List<bool>{false,true}, got, "E1");
    }

    private static void T_E2()
    {
        int n = 5;
        var unions = new (int a,int b)[] { (0,1),(1,2),(3,4),(2,3) };
        var got = Drills.ComponentsAfterUnions(n, unions);
        AssertEx.SeqEqual(new List<int>{4,3,2,1}, got, "E2");
    }

    private static void T_F1()
    {
        int n = 4;
        var edges = new (int u,int v)[] { (0,1),(1,2),(2,0),(0,3) };
        var path = Drills.EulerPathDirected(n, edges);
        AssertEx.Equal(edges.Length + 1, path.Count, "F1 len");
    }

    private static void T_F2()
    {
        int n = 3;
        var edges = new (int u,int v)[] { (0,1),(1,2),(2,0) };
        var path = Drills.EulerPathUndirected(n, edges);
        AssertEx.Equal(edges.Length + 1, path.Count, "F2 len");
    }

    private static void T_X1()
    {
        int n = 1;
        var (dist, hasNeg) = Drills.BellmanFordDist(n, Array.Empty<(int,int,int)>(), 0);
        AssertEx.Equal(false, hasNeg, "X1 hasNeg");
    }
}

public static class Drills
{
    // TODO: Implement all functions below (NO SOLUTIONS)

    public static (List<long> dist, bool hasNegCycleReachable) BellmanFordDist(int n, (int u,int v,int w)[] edges, int src)
        => throw new NotImplementedException("TODO A1");

    public static (int dist, List<int> path) BellmanFordPath(int n, (int u,int v,int w)[] edges, int src, int dst)
        => throw new NotImplementedException("TODO A2");

    public static int[][] FloydWarshall(int[][] mat)
        => throw new NotImplementedException("TODO A3");

    public static (int[][] dist, int[][] nxt) FloydWarshallPath(int[][] mat)
        => throw new NotImplementedException("TODO A4");

    public static List<int> FwReconstructPath(int[][] nxt, int i, int j)
        => throw new NotImplementedException("TODO A4b");

    public static (List<int> dist, List<int> ways) DijkstraCountShortestPaths(int n, (int u,int v,int w)[] edges, int src, int mod)
        => throw new NotImplementedException("TODO A5");

    public static (int dist, List<int> path) DijkstraLexiShortestPath(int n, (int u,int v,int w)[] edges, int src, int dst)
        => throw new NotImplementedException("TODO A6");

    public static int GridMinimaxEffort(int[][] grid)
        => throw new NotImplementedException("TODO A7");

    public static List<int> MultiSourceDijkstra(int n, (int u,int v,int w)[] edges, int[] sources)
        => throw new NotImplementedException("TODO A8");

    public static int AStarShortestPathLen(int[][] grid, (int r,int c) start, (int r,int c) goal)
        => throw new NotImplementedException("TODO B1");

    public static List<(int r,int c)> AStarShortestPath(int[][] grid, (int r,int c) start, (int r,int c) goal)
        => throw new NotImplementedException("TODO B2");

    public static int DinicMaxFlow(int n, (int u,int v,int cap)[] edges, int s, int t)
        => throw new NotImplementedException("TODO C1");

    public static (int maxflow, List<int> sSide) DinicMinCutPartition(int n, (int u,int v,int cap)[] edges, int s, int t)
        => throw new NotImplementedException("TODO C2");

    public static (int msize, List<int> matchL) HopcroftKarp(int L, int R, (int u,int v)[] edges)
        => throw new NotImplementedException("TODO C3");

    public static (List<int> coverL, List<int> coverR) MinVertexCoverBipartite(int L, int R, (int u,int v)[] edges)
        => throw new NotImplementedException("TODO C4");

    public static bool HasNegativeCycleAnywhere(int n, (int u,int v,int w)[] edges)
        => throw new NotImplementedException("TODO D1");

    public static int ShortestPathExactKEdges(int n, (int u,int v,int w)[] edges, int src, int dst, int k)
        => throw new NotImplementedException("TODO D2");

    public static List<int> DagKBestDistances(int n, (int u,int v,int w)[] edges, int src, int dst, int k)
        => throw new NotImplementedException("TODO D3");

    public static (bool sat, List<bool> assign) TwoSat(int nvars, (int a,bool aPos,int b,bool bPos)[] clauses)
        => throw new NotImplementedException("TODO D4");

    public static List<bool> DsuOfflineConnectivity(int n, (string op,int a,int b)[] ops)
        => throw new NotImplementedException("TODO E1");

    public static List<int> ComponentsAfterUnions(int n, (int a,int b)[] unions)
        => throw new NotImplementedException("TODO E2");

    public static List<int> EulerPathDirected(int n, (int u,int v)[] edges)
        => throw new NotImplementedException("TODO F1");

    public static List<int> EulerPathUndirected(int n, (int u,int v)[] edges)
        => throw new NotImplementedException("TODO F2");
}
```
