# 🧪 Graph Traversal Drills (No Solutions) — Drivers in C# + Python

This file contains **two runnable drivers** (C# and Python) for the same set of drills.

- Each drill function is a **TODO** (no solutions).
- Tests include **inputs + expected outputs** so you can practice directly.

---

## ✅ Option A: Run in C#

### Setup
```bash
dotnet new console -n GraphDrills
cd GraphDrills
```
Replace `Program.cs` with the code below and run:
```bash
dotnet run
# or
# dotnet run G12
```

### Program.cs

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

public sealed class Program
{
    public static void Main(string[] args)
    {
        try
        {
            var id = args.Length > 0 ? args[0].Trim().ToUpperInvariant() : "ALL";
            new Runner().Run(id);
            Console.WriteLine("✅ Done");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("❌ " + ex.Message);
            Environment.ExitCode = 1;
        }
    }
}

public static class AssertEx
{
    public static void True(bool cond, string msg)
    {
        if (!cond) throw new Exception(msg);
    }

    public static void Equal<T>(T expected, T actual, string msg) where T : IEquatable<T>
    {
        if (!expected.Equals(actual)) throw new Exception(msg + $"\nExpected: {expected}\nActual:   {actual}");
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
            ["G1"] = T_G1_BuildAdjUndirected,
            ["G2"] = T_G2_BuildAdjDirected,
            ["G3"] = T_G3_BfsDistances,
            ["G4"] = T_G4_BfsShortestPath,
            ["G5"] = T_G5_DfsTinTout,
            ["G6"] = T_G6_ConnectedComponents,
            ["G7"] = T_G7_CycleUndirected,
            ["G8"] = T_G8_CycleDirected,
            ["G9"] = T_G9_Bipartite,
            ["G10"] = T_G10_TopoKahnLexi,
            ["G11"] = T_G11_TopoDfs,
            ["G12"] = T_G12_ZeroOneBfs,
            ["G13"] = T_G13_DijkstraDistances,
            ["G14"] = T_G14_DijkstraPath,
            ["G15"] = T_G15_MultiSourceBfsGrid,
            ["G16"] = T_G16_NumberOfIslands,
            ["G17"] = T_G17_SccCountKosaraju,
            ["G18"] = T_G18_Bridges,
            ["G19"] = T_G19_MstKruskal,
            ["G20"] = T_G20_DsuQueries,
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

        if (!tests.TryGetValue(id, out var runOne))
            throw new Exception($"Unknown id: {id}. Use ALL or G1..G20");

        runOne();
        Console.WriteLine($"✅ {id} passed");
    }

    // ------------------ Tests ------------------

    private static void T_G1_BuildAdjUndirected()
    {
        int n = 4;
        var edges = new (int u,int v)[] { (0,1),(1,2),(1,3) };
        var adj = Drills.BuildAdjUndirected(n, edges, sortNeighbors: true);
        AssertEx.SeqEqual(new List<int>{1}, adj[0], "G1 adj[0]");
        AssertEx.SeqEqual(new List<int>{0,2,3}, adj[1], "G1 adj[1]");
        AssertEx.SeqEqual(new List<int>{1}, adj[2], "G1 adj[2]");
        AssertEx.SeqEqual(new List<int>{1}, adj[3], "G1 adj[3]");
    }

    private static void T_G2_BuildAdjDirected()
    {
        int n = 4;
        var edges = new (int u,int v)[] { (0,1),(0,2),(2,3) };
        var adj = Drills.BuildAdjDirected(n, edges, sortNeighbors: true);
        AssertEx.SeqEqual(new List<int>{1,2}, adj[0], "G2 adj[0]");
        AssertEx.SeqEqual(new List<int>{}, adj[1], "G2 adj[1]");
        AssertEx.SeqEqual(new List<int>{3}, adj[2], "G2 adj[2]");
        AssertEx.SeqEqual(new List<int>{}, adj[3], "G2 adj[3]");
    }

    private static void T_G3_BfsDistances()
    {
        int n = 6;
        var edges = new (int u,int v)[] { (0,1),(0,2),(1,3),(2,3),(3,4),(4,5) };
        var dist = Drills.BfsDistances(n, edges, src: 0);
        AssertEx.SeqEqual(new List<int>{0,1,1,2,3,4}, dist, "G3 dist");
    }

    private static void T_G4_BfsShortestPath()
    {
        int n = 6;
        var edges = new (int u,int v)[] { (0,1),(0,2),(1,3),(2,3),(3,4),(4,5) };
        var (d, path) = Drills.BfsShortestPath(n, edges, src: 0, dst: 5);
        AssertEx.Equal(4, d, "G4 dist");
        // Tie-break: smallest parent first due to sorted adjacency traversal.
        AssertEx.SeqEqual(new List<int>{0,1,3,4,5}, path, "G4 path");
    }

    private static void T_G5_DfsTinTout()
    {
        int n = 5;
        var edges = new (int u,int v)[] { (0,1),(1,2),(1,3),(3,4) };
        var (tin, tout) = Drills.DfsTinTout(n, edges, root: 0);
        AssertEx.SeqEqual(new List<int>{0,1,2,4,5}, tin, "G5 tin");
        AssertEx.SeqEqual(new List<int>{9,8,3,7,6}, tout, "G5 tout");
    }

    private static void T_G6_ConnectedComponents()
    {
        int n = 7;
        var edges = new (int u,int v)[] { (0,1),(1,2),(3,4),(5,6) };
        AssertEx.Equal(3, Drills.CountConnectedComponents(n, edges), "G6");
    }

    private static void T_G7_CycleUndirected()
    {
        int n = 4;
        var edges = new (int u,int v)[] { (0,1),(1,2),(2,0),(2,3) };
        AssertEx.True(Drills.HasCycleUndirected(n, edges), "G7 expected true");
        n = 4;
        edges = new (int u,int v)[] { (0,1),(1,2),(2,3) };
        AssertEx.True(!Drills.HasCycleUndirected(n, edges), "G7 expected false");
    }

    private static void T_G8_CycleDirected()
    {
        int n = 4;
        var edges = new (int u,int v)[] { (0,1),(1,2),(2,0),(2,3) };
        AssertEx.True(Drills.HasCycleDirected(n, edges), "G8 expected true");
        n = 4;
        edges = new (int u,int v)[] { (0,1),(1,2),(2,3) };
        AssertEx.True(!Drills.HasCycleDirected(n, edges), "G8 expected false");
    }

    private static void T_G9_Bipartite()
    {
        int n = 4;
        var edges = new (int u,int v)[] { (0,1),(1,2),(2,3) };
        AssertEx.True(Drills.IsBipartite(n, edges), "G9 expected true");
        n = 3;
        edges = new (int u,int v)[] { (0,1),(1,2),(2,0) };
        AssertEx.True(!Drills.IsBipartite(n, edges), "G9 expected false");
    }

    private static void T_G10_TopoKahnLexi()
    {
        // DAG edges: 5->2, 5->0, 4->0, 4->1, 2->3, 3->1
        int n = 6;
        var edges = new (int u,int v)[] { (5,2),(5,0),(4,0),(4,1),(2,3),(3,1) };
        var order = Drills.TopoSortKahnLexi(n, edges);
        AssertEx.SeqEqual(new List<int>{4,5,0,2,3,1}, order, "G10");
    }

    private static void T_G11_TopoDfs()
    {
        int n = 6;
        var edges = new (int u,int v)[] { (5,2),(5,0),(4,0),(4,1),(2,3),(3,1) };
        var order = Drills.TopoSortDfs(n, edges);
        // DFS topo order is deterministic given node iteration 0..n-1 and sorted adjacency.
        AssertEx.SeqEqual(new List<int>{5,4,2,3,1,0}, order, "G11");
    }

    private static void T_G12_ZeroOneBfs()
    {
        int n = 4;
        var edges = new (int u,int v,int w)[] { (0,1,0),(0,2,1),(1,2,0),(1,3,1),(2,3,0) };
        var dist = Drills.ZeroOneBfs(n, edges, src: 0);
        AssertEx.SeqEqual(new List<int>{0,0,0,0}, dist, "G12 case1");

        n = 4;
        edges = new (int u,int v,int w)[] { (0,1,1),(0,2,1),(1,3,1),(2,3,0) };
        dist = Drills.ZeroOneBfs(n, edges, src: 0);
        AssertEx.SeqEqual(new List<int>{0,1,1,1}, dist, "G12 case2");
    }

    private static void T_G13_DijkstraDistances()
    {
        int n = 5;
        var edges = new (int u,int v,int w)[] { (0,1,2),(0,2,5),(1,2,1),(1,3,2),(2,3,1),(3,4,3) };
        var dist = Drills.DijkstraDistances(n, edges, src: 0);
        AssertEx.SeqEqual(new List<int>{0,2,3,4,7}, dist, "G13");
    }

    private static void T_G14_DijkstraPath()
    {
        int n = 5;
        var edges = new (int u,int v,int w)[] { (0,1,2),(0,2,5),(1,2,1),(1,3,2),(2,3,1),(3,4,3) };
        var (dist, path) = Drills.DijkstraPath(n, edges, src: 0, dst: 4);
        AssertEx.Equal(7, dist, "G14 dist");
        // One shortest path is 0->1->2->3->4
        AssertEx.SeqEqual(new List<int>{0,1,2,3,4}, path, "G14 path");
    }

    private static void T_G15_MultiSourceBfsGrid()
    {
        int[][] grid = {
            new []{0,0,0},
            new []{0,1,0},
            new []{1,1,1},
        };
        var dist = Drills.MultiSourceBfsNearestOnes(grid);
        // Expected distances to nearest 1
        // [ [2,1,2],
        //   [1,0,1],
        //   [0,0,0] ]
        AssertEx.SeqEqual(new List<int>{2,1,2}, dist[0], "G15 row0");
        AssertEx.SeqEqual(new List<int>{1,0,1}, dist[1], "G15 row1");
        AssertEx.SeqEqual(new List<int>{0,0,0}, dist[2], "G15 row2");
    }

    private static void T_G16_NumberOfIslands()
    {
        char[][] grid = {
            new []{'1','1','0','0'},
            new []{'1','0','0','1'},
            new []{'0','0','1','1'},
            new []{'0','0','0','0'},
        };
        AssertEx.Equal(2, Drills.NumIslands(grid), "G16");
    }

    private static void T_G17_SccCountKosaraju()
    {
        // SCCs: {0,1,2}, {3}, {4,5}
        int n = 6;
        var edges = new (int u,int v)[] { (0,1),(1,2),(2,0),(2,3),(4,5),(5,4) };
        AssertEx.Equal(3, Drills.SccCountKosaraju(n, edges), "G17");
    }

    private static void T_G18_Bridges()
    {
        // Graph: 0-1-2-0 forms triangle, plus edge 1-3 is a bridge.
        int n = 4;
        var edges = new (int u,int v)[] { (0,1),(1,2),(2,0),(1,3) };
        var bridges = Drills.Bridges(n, edges);
        // Expect sorted pairs.
        AssertEx.SeqEqual(new List<string>{"1-3"}, bridges.Select(e => $"{e.u}-{e.v}").ToList(), "G18");
    }

    private static void T_G19_MstKruskal()
    {
        int n = 4;
        var edges = new (int u,int v,int w)[] { (0,1,1),(1,2,2),(2,3,1),(0,3,4),(0,2,3) };
        AssertEx.Equal(4, Drills.MstKruskalWeight(n, edges), "G19");
    }

    private static void T_G20_DsuQueries()
    {
        int n = 5;
        var ops = new (string op,int a,int b)[]
        {
            ("union",0,1),
            ("union",1,2),
            ("connected",0,2),
            ("connected",0,3),
            ("union",3,4),
            ("connected",4,3),
        };
        var got = Drills.RunDsuQueries(n, ops);
        AssertEx.SeqEqual(new List<bool>{true,false,true}, got, "G20");
    }
}

public static class Drills
{
    // G1
    public static List<int>[] BuildAdjUndirected(int n, (int u,int v)[] edges, bool sortNeighbors)
        => throw new NotImplementedException("TODO G1");

    // G2
    public static List<int>[] BuildAdjDirected(int n, (int u,int v)[] edges, bool sortNeighbors)
        => throw new NotImplementedException("TODO G2");

    // G3
    public static List<int> BfsDistances(int n, (int u,int v)[] edges, int src)
        => throw new NotImplementedException("TODO G3");

    // G4
    public static (int dist, List<int> path) BfsShortestPath(int n, (int u,int v)[] edges, int src, int dst)
        => throw new NotImplementedException("TODO G4");

    // G5
    public static (List<int> tin, List<int> tout) DfsTinTout(int n, (int u,int v)[] edges, int root)
        => throw new NotImplementedException("TODO G5");

    // G6
    public static int CountConnectedComponents(int n, (int u,int v)[] edges)
        => throw new NotImplementedException("TODO G6");

    // G7
    public static bool HasCycleUndirected(int n, (int u,int v)[] edges)
        => throw new NotImplementedException("TODO G7");

    // G8
    public static bool HasCycleDirected(int n, (int u,int v)[] edges)
        => throw new NotImplementedException("TODO G8");

    // G9
    public static bool IsBipartite(int n, (int u,int v)[] edges)
        => throw new NotImplementedException("TODO G9");

    // G10
    public static List<int> TopoSortKahnLexi(int n, (int u,int v)[] edges)
        => throw new NotImplementedException("TODO G10");

    // G11
    public static List<int> TopoSortDfs(int n, (int u,int v)[] edges)
        => throw new NotImplementedException("TODO G11");

    // G12
    public static List<int> ZeroOneBfs(int n, (int u,int v,int w)[] edges, int src)
        => throw new NotImplementedException("TODO G12");

    // G13
    public static List<int> DijkstraDistances(int n, (int u,int v,int w)[] edges, int src)
        => throw new NotImplementedException("TODO G13");

    // G14
    public static (int dist, List<int> path) DijkstraPath(int n, (int u,int v,int w)[] edges, int src, int dst)
        => throw new NotImplementedException("TODO G14");

    // G15
    public static List<List<int>> MultiSourceBfsNearestOnes(int[][] grid)
        => throw new NotImplementedException("TODO G15");

    // G16
    public static int NumIslands(char[][] grid)
        => throw new NotImplementedException("TODO G16");

    // G17
    public static int SccCountKosaraju(int n, (int u,int v)[] edges)
        => throw new NotImplementedException("TODO G17");

    // G18
    public static List<(int u,int v)> Bridges(int n, (int u,int v)[] edges)
        => throw new NotImplementedException("TODO G18");

    // G19
    public static int MstKruskalWeight(int n, (int u,int v,int w)[] edges)
        => throw new NotImplementedException("TODO G19");

    // G20
    public static List<bool> RunDsuQueries(int n, (string op,int a,int b)[] ops)
        => throw new NotImplementedException("TODO G20");
}
```

---

## ✅ Option B: Run in Python

### Setup
Create a file `drills.py` and run:
```bash
python drills.py
# or
python drills.py G12
```

### drills.py

```python
import sys
from collections import deque
import heapq


def assert_eq(exp, act, msg=""):
    if exp != act:
        raise AssertionError(f"{msg}\nExpected: {exp}\nActual:   {act}")


def run(drill_id="ALL"):
    tests = {
        "G1": t_g1_build_adj_undirected,
        "G2": t_g2_build_adj_directed,
        "G3": t_g3_bfs_distances,
        "G4": t_g4_bfs_shortest_path,
        "G5": t_g5_dfs_tin_tout,
        "G6": t_g6_components,
        "G7": t_g7_cycle_undirected,
        "G8": t_g8_cycle_directed,
        "G9": t_g9_bipartite,
        "G10": t_g10_topo_kahn_lexi,
        "G11": t_g11_topo_dfs,
        "G12": t_g12_zero_one_bfs,
        "G13": t_g13_dijkstra_dist,
        "G14": t_g14_dijkstra_path,
        "G15": t_g15_multi_source_grid,
        "G16": t_g16_num_islands,
        "G17": t_g17_scc_count,
        "G18": t_g18_bridges,
        "G19": t_g19_mst_kruskal,
        "G20": t_g20_dsu_queries,
    }

    if drill_id == "ALL":
        for k in sorted(tests.keys()):
            tests[k]()
            print(f"✅ {k} passed")
        return

    if drill_id not in tests:
        raise ValueError("Unknown id. Use ALL or G1..G20")

    tests[drill_id]()
    print(f"✅ {drill_id} passed")


# ---------------- Tests ----------------

def t_g1_build_adj_undirected():
    n = 4
    edges = [(0,1),(1,2),(1,3)]
    adj = build_adj_undirected(n, edges, sort_neighbors=True)
    assert_eq([1], adj[0], "G1 adj[0]")
    assert_eq([0,2,3], adj[1], "G1 adj[1]")
    assert_eq([1], adj[2], "G1 adj[2]")
    assert_eq([1], adj[3], "G1 adj[3]")


def t_g2_build_adj_directed():
    n = 4
    edges = [(0,1),(0,2),(2,3)]
    adj = build_adj_directed(n, edges, sort_neighbors=True)
    assert_eq([1,2], adj[0], "G2 adj[0]")
    assert_eq([], adj[1], "G2 adj[1]")
    assert_eq([3], adj[2], "G2 adj[2]")
    assert_eq([], adj[3], "G2 adj[3]")


def t_g3_bfs_distances():
    n = 6
    edges = [(0,1),(0,2),(1,3),(2,3),(3,4),(4,5)]
    dist = bfs_distances(n, edges, src=0)
    assert_eq([0,1,1,2,3,4], dist, "G3")


def t_g4_bfs_shortest_path():
    n = 6
    edges = [(0,1),(0,2),(1,3),(2,3),(3,4),(4,5)]
    d, path = bfs_shortest_path(n, edges, src=0, dst=5)
    assert_eq(4, d, "G4 dist")
    assert_eq([0,1,3,4,5], path, "G4 path")


def t_g5_dfs_tin_tout():
    n = 5
    edges = [(0,1),(1,2),(1,3),(3,4)]
    tin, tout = dfs_tin_tout(n, edges, root=0)
    assert_eq([0,1,2,4,5], tin, "G5 tin")
    assert_eq([9,8,3,7,6], tout, "G5 tout")


def t_g6_components():
    n = 7
    edges = [(0,1),(1,2),(3,4),(5,6)]
    assert_eq(3, count_connected_components(n, edges), "G6")


def t_g7_cycle_undirected():
    n = 4
    edges = [(0,1),(1,2),(2,0),(2,3)]
    assert_eq(True, has_cycle_undirected(n, edges), "G7 true")
    n = 4
    edges = [(0,1),(1,2),(2,3)]
    assert_eq(False, has_cycle_undirected(n, edges), "G7 false")


def t_g8_cycle_directed():
    n = 4
    edges = [(0,1),(1,2),(2,0),(2,3)]
    assert_eq(True, has_cycle_directed(n, edges), "G8 true")
    n = 4
    edges = [(0,1),(1,2),(2,3)]
    assert_eq(False, has_cycle_directed(n, edges), "G8 false")


def t_g9_bipartite():
    n = 4
    edges = [(0,1),(1,2),(2,3)]
    assert_eq(True, is_bipartite(n, edges), "G9 true")
    n = 3
    edges = [(0,1),(1,2),(2,0)]
    assert_eq(False, is_bipartite(n, edges), "G9 false")


def t_g10_topo_kahn_lexi():
    n = 6
    edges = [(5,2),(5,0),(4,0),(4,1),(2,3),(3,1)]
    order = topo_kahn_lexi(n, edges)
    assert_eq([4,5,0,2,3,1], order, "G10")


def t_g11_topo_dfs():
    n = 6
    edges = [(5,2),(5,0),(4,0),(4,1),(2,3),(3,1)]
    order = topo_dfs(n, edges)
    assert_eq([5,4,2,3,1,0], order, "G11")


def t_g12_zero_one_bfs():
    n = 4
    edges = [(0,1,0),(0,2,1),(1,2,0),(1,3,1),(2,3,0)]
    dist = zero_one_bfs(n, edges, src=0)
    assert_eq([0,0,0,0], dist, "G12 case1")

    n = 4
    edges = [(0,1,1),(0,2,1),(1,3,1),(2,3,0)]
    dist = zero_one_bfs(n, edges, src=0)
    assert_eq([0,1,1,1], dist, "G12 case2")


def t_g13_dijkstra_dist():
    n = 5
    edges = [(0,1,2),(0,2,5),(1,2,1),(1,3,2),(2,3,1),(3,4,3)]
    dist = dijkstra_distances(n, edges, src=0)
    assert_eq([0,2,3,4,7], dist, "G13")


def t_g14_dijkstra_path():
    n = 5
    edges = [(0,1,2),(0,2,5),(1,2,1),(1,3,2),(2,3,1),(3,4,3)]
    d, path = dijkstra_path(n, edges, src=0, dst=4)
    assert_eq(7, d, "G14 dist")
    assert_eq([0,1,2,3,4], path, "G14 path")


def t_g15_multi_source_grid():
    grid = [
        [0,0,0],
        [0,1,0],
        [1,1,1],
    ]
    dist = multi_source_bfs_nearest_ones(grid)
    assert_eq([[2,1,2],[1,0,1],[0,0,0]], dist, "G15")


def t_g16_num_islands():
    grid = [
        list("1100"),
        list("1001"),
        list("0011"),
        list("0000"),
    ]
    assert_eq(2, num_islands(grid), "G16")


def t_g17_scc_count():
    n = 6
    edges = [(0,1),(1,2),(2,0),(2,3),(4,5),(5,4)]
    assert_eq(3, scc_count_kosaraju(n, edges), "G17")


def t_g18_bridges():
    n = 4
    edges = [(0,1),(1,2),(2,0),(1,3)]
    bridges = bridges(n, edges)
    assert_eq([(1,3)], bridges, "G18")


def t_g19_mst_kruskal():
    n = 4
    edges = [(0,1,1),(1,2,2),(2,3,1),(0,3,4),(0,2,3)]
    assert_eq(4, mst_kruskal_weight(n, edges), "G19")


def t_g20_dsu_queries():
    n = 5
    ops = [
        ("union",0,1),
        ("union",1,2),
        ("connected",0,2),
        ("connected",0,3),
        ("union",3,4),
        ("connected",4,3),
    ]
    got = run_dsu_queries(n, ops)
    assert_eq([True, False, True], got, "G20")


# ---------------- TODO drills (NO SOLUTIONS) ----------------

def build_adj_undirected(n, edges, sort_neighbors):
    raise NotImplementedError("TODO G1")


def build_adj_directed(n, edges, sort_neighbors):
    raise NotImplementedError("TODO G2")


def bfs_distances(n, edges, src):
    raise NotImplementedError("TODO G3")


def bfs_shortest_path(n, edges, src, dst):
    raise NotImplementedError("TODO G4")


def dfs_tin_tout(n, edges, root):
    raise NotImplementedError("TODO G5")


def count_connected_components(n, edges):
    raise NotImplementedError("TODO G6")


def has_cycle_undirected(n, edges):
    raise NotImplementedError("TODO G7")


def has_cycle_directed(n, edges):
    raise NotImplementedError("TODO G8")


def is_bipartite(n, edges):
    raise NotImplementedError("TODO G9")


def topo_kahn_lexi(n, edges):
    raise NotImplementedError("TODO G10")


def topo_dfs(n, edges):
    raise NotImplementedError("TODO G11")


def zero_one_bfs(n, edges, src):
    raise NotImplementedError("TODO G12")


def dijkstra_distances(n, edges, src):
    raise NotImplementedError("TODO G13")


def dijkstra_path(n, edges, src, dst):
    raise NotImplementedError("TODO G14")


def multi_source_bfs_nearest_ones(grid):
    raise NotImplementedError("TODO G15")


def num_islands(grid):
    raise NotImplementedError("TODO G16")


def scc_count_kosaraju(n, edges):
    raise NotImplementedError("TODO G17")


def bridges(n, edges):
    raise NotImplementedError("TODO G18")


def mst_kruskal_weight(n, edges):
    raise NotImplementedError("TODO G19")


def run_dsu_queries(n, ops):
    raise NotImplementedError("TODO G20")


if __name__ == "__main__":
    drill_id = "ALL" if len(sys.argv) == 1 else sys.argv[1].strip().upper()
    run(drill_id)
```
